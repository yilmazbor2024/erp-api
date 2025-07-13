using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ErpMobile.Api.Data;
using ErpMobile.Api.Entities;
using ErpMobile.Api.Models.DatabaseBackup;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ErpMobile.Api.Services
{
    public class DatabaseBackupService : IDatabaseBackupService
    {
        private readonly ILogger<DatabaseBackupService> _logger;
        private readonly IConfiguration _configuration;
        private readonly NanoServiceDbContext _context;
        private readonly IGlobalSettingsService _globalSettings;
        private readonly INotificationService _notificationService;
        private string _backupBasePath;
        private string _connectionString;

        public DatabaseBackupService(
            ILogger<DatabaseBackupService> logger,
            IConfiguration configuration,
            NanoServiceDbContext context,
            IGlobalSettingsService globalSettings,
            INotificationService notificationService)
        {
            _logger = logger;
            _configuration = configuration;
            _context = context;
            _globalSettings = globalSettings;
            _notificationService = notificationService;
            _connectionString = _configuration.GetConnectionString("NanoServiceConnection");
            
            _logger.LogInformation("Veritabanı bağlantı dizesi: {0}", _connectionString);
            
            // Yedekleme dizinini asenkron olarak başlat
            InitializeBackupPathAsync().GetAwaiter().GetResult();
        }

        /// <summary>
        /// Yedekleme dizinini asenkron olarak başlatır
        /// </summary>
        private async Task InitializeBackupPathAsync()
        {
            try
            {
                var backupPath = await _globalSettings.GetSettingAsync<string>("DatabaseBackup:Path", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Backups"));
                _backupBasePath = backupPath;
                _logger.LogInformation("Yedekleme dizini ayarlandı: {Path}", _backupBasePath);
                if (!Directory.Exists(_backupBasePath))
                {
                    try
                    {
                        Directory.CreateDirectory(_backupBasePath);
                        _logger.LogInformation("Yedekleme dizini oluşturuldu: {Path}", _backupBasePath);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Yedekleme dizini oluşturulurken hata oluştu: {Path}", _backupBasePath);
                        _backupBasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Backups");
                        Directory.CreateDirectory(_backupBasePath);
                        _logger.LogInformation("Varsayılan yedekleme dizini oluşturuldu: {Path}", _backupBasePath);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Yedekleme dizini yüklenirken hata oluştu");
                _backupBasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Backups");
                if (!Directory.Exists(_backupBasePath))
                {
                    Directory.CreateDirectory(_backupBasePath);
                }
            }
        }

        /// <summary>
        /// Sistemdeki veritabanlarını listeler
        /// </summary>
        /// <returns>Veritabanı listesi</returns>
        public async Task<List<DatabaseInfo>> GetDatabasesAsync()
        {
            try
            {
                _logger.LogInformation("Veritabanları listeleniyor... Bağlantı dizesi: {0}", _connectionString);
                
                var databases = new List<DatabaseInfo>();
                
                if (string.IsNullOrEmpty(_connectionString))
                {
                    _logger.LogError("Bağlantı dizesi boş veya null. Veritabanları listelenemiyor.");
                    return databases;
                }
                
                try
                {
                    using (var connection = new SqlConnection(_connectionString))
                    {
                        _logger.LogInformation("Veritabanı bağlantısı açılıyor...");
                        await connection.OpenAsync();
                        _logger.LogInformation("Veritabanı bağlantısı başarıyla açıldı.");
                        
                        // Tüm veritabanlarını listele, sistem veritabanlarını da dahil et
                        string sql = "SELECT name, database_id, create_date FROM sys.databases ORDER BY name";
                        _logger.LogInformation("SQL sorgusu çalıştırılıyor: {0}", sql);
                        
                        using (var command = new SqlCommand(sql, connection))
                        {
                            using (var reader = await command.ExecuteReaderAsync())
                            {
                                _logger.LogInformation("SQL sorgusu çalıştırıldı, sonuçlar okunuyor...");
                                
                                while (await reader.ReadAsync())
                                {
                                    var dbName = reader["name"].ToString();
                                    var dbId = Guid.NewGuid(); // Gerçek bir sistemde bu ID'ler veritabanında saklanmalı
                                    
                                    _logger.LogInformation("Veritabanı bulundu: {0}", dbName);
                                    
                                    databases.Add(new DatabaseInfo
                                    {
                                        Id = dbId,
                                        DatabaseName = dbName,
                                        CreatedAt = reader["create_date"] is DBNull ? DateTime.Now : (DateTime)reader["create_date"]
                                    });
                                }
                            }
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    _logger.LogError(sqlEx, "SQL bağlantısı sırasında hata oluştu: {0}", sqlEx.Message);
                    throw;
                }
                
                _logger.LogInformation("{0} veritabanı bulundu.", databases.Count);
                return databases;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Veritabanları listelenirken hata oluştu.");
                return new List<DatabaseInfo>();
            }
        }
        
        /// <summary>
        /// Tam yedekleme işlemi yapar
        /// </summary>
        /// <param name="databaseId">Veritabanı ID</param>
        /// <returns>Yedekleme sonucu</returns>
        public async Task<BackupResult> CreateFullBackupAsync(Guid databaseId)
        {
            try
            {
                _logger.LogInformation("Tam yedekleme işlemi başlatılıyor...");
                
                _logger.LogInformation("Veritabanı bilgisi aranıyor. DatabaseId: {0}", databaseId);
                
                // Önce veritabanı listesini al
                var databases = await GetDatabasesAsync();
                _logger.LogInformation("{0} veritabanı bulundu.", databases.Count);
                
                // Veritabanı ID'sine göre eşleşen veritabanını bul
                var databaseInfo = databases.FirstOrDefault(d => d.Id == databaseId);
                
                if (databaseInfo == null)
                {
                    _logger.LogError("Veritabanı bulunamadı. DatabaseId: {0}", databaseId);
                    _logger.LogInformation("Mevcut veritabanları: {0}", string.Join(", ", databases.Select(d => d.DatabaseName)));
                    return new BackupResult
                    {
                        Success = false,
                        Message = $"Veritabanı bulunamadı. ID: {databaseId}",
                        BackupFileName = null,
                        BackupPath = null,
                        BackupType = "Full",
                        CreatedAt = DateTime.Now
                    };
                }
                
                // Yedekleme ayarlarını al
                var minFreeSpaceGB = await _globalSettings.GetSettingAsync<double>("DatabaseBackup:MinimumFreeSpaceGB", 5.0);
                var maxBackupsPerDatabase = await _globalSettings.GetSettingAsync<int>("DatabaseBackup:MaxBackupsPerDatabase", 10);
                
                // Disk alanı kontrolü
                var diskSpaceOk = await CheckDiskSpaceAsync(_backupBasePath, minFreeSpaceGB);
                if (!diskSpaceOk)
                {
                    _logger.LogError("Yedekleme için yeterli disk alanı yok. Gerekli: {RequiredGB} GB", minFreeSpaceGB);
                    return new BackupResult
                    {
                        Success = false,
                        Message = $"Yedekleme için yeterli disk alanı yok. En az {minFreeSpaceGB} GB boş alan gerekli.",
                        BackupFileName = null,
                        BackupPath = null,
                        BackupType = "Full",
                        CreatedAt = DateTime.Now
                    };
                }
                
                var databaseName = databaseInfo.DatabaseName;
                var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                var backupFileName = $"{databaseName}_FULL_{timestamp}.bak";
                var backupFilePath = Path.Combine(_backupBasePath, backupFileName);
                
                _logger.LogInformation("Veritabanı {DatabaseName} için tam yedekleme başlatılıyor. Dosya: {FilePath}", 
                    databaseName, backupFilePath);
                
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    // Yedekleme SQL komutunu oluştur
                    var backupQuery = $"BACKUP DATABASE [{databaseName}] TO DISK = '{backupFilePath}' WITH FORMAT, INIT, NAME = '{databaseName}-Full Database Backup', SKIP, NOREWIND, NOUNLOAD, STATS = 10";
                    
                    using (var command = new SqlCommand(backupQuery, connection))
                    {
                        command.CommandTimeout = 300; // 5 dakika
                        await command.ExecuteNonQueryAsync();
                    }
                }
                
                // Yedekleme kaydını veritabanına ekle
                var backup = new DatabaseBackup
                {
                    Id = Guid.NewGuid(),
                    DatabaseId = databaseId,
                    BackupType = "Full",
                    BackupFileName = backupFileName,
                    BackupPath = backupFilePath,
                    SizeInMB = GetFileSizeInMB(backupFilePath),
                    CreatedAt = DateTime.Now,
                    CreatedBy = "System"
                };
                
                _context.DatabaseBackups.Add(backup);
                await _context.SaveChangesAsync();
                
                _logger.LogInformation("Tam yedekleme başarıyla tamamlandı. Dosya: {FilePath}, Boyut: {Size} MB", 
                    backupFilePath, backup.SizeInMB);
                
                // Yedekleme tamamlandı bildirimi gönder
                try
                {
                    await _notificationService.SendBackupCompletedNotificationAsync(
                        databaseName, 
                        "Full", 
                        true, 
                        $"Tam yedekleme başarıyla tamamlandı. Dosya: {backupFileName}, Boyut: {backup.SizeInMB} MB");
                }
                catch (Exception notifyEx)
                {
                    _logger.LogWarning(notifyEx, "Yedekleme tamamlandı bildirimi gönderilirken hata oluştu.");
                    // Bildirim gönderme hatası yedekleme işlemini etkilememeli
                }
                
                return new BackupResult
                {
                    Success = true,
                    Message = "Tam yedekleme başarıyla tamamlandı.",
                    BackupFileName = backupFileName,
                    BackupPath = backupFilePath,
                    BackupType = "Full",
                    CreatedAt = DateTime.Now,
                    SizeInMB = backup.SizeInMB
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Tam yedekleme sırasında hata oluştu.");
                
                // Yedekleme hatası bildirimi gönder
                try
                {
                    // Veritabanı adını almaya çalış
                    string databaseName = "Bilinmeyen Veritabanı";
                    var database = _context.Databases.FirstOrDefault(d => d.Id == databaseId);
                    if (database != null)
                    {
                        databaseName = database.DatabaseName;
                    }
                    
                    await _notificationService.SendBackupCompletedNotificationAsync(
                        databaseName, 
                        "Full", 
                        false, 
                        $"Tam yedekleme sırasında hata oluştu: {ex.Message}");
                }
                catch (Exception notifyEx)
                {
                    _logger.LogWarning(notifyEx, "Yedekleme hata bildirimi gönderilirken hata oluştu.");
                }
                
                return new BackupResult
                {
                    Success = false,
                    Message = $"Tam yedekleme sırasında hata oluştu: {ex.Message}",
                    BackupFileName = null,
                    BackupPath = null,
                    BackupType = "Full",
                    CreatedAt = DateTime.Now
                };
            }
        }
        
        /// <summary>
        /// Diferansiyel yedekleme işlemi yapar
        /// </summary>
        /// <param name="databaseId">Veritabanı ID</param>
        /// <returns>Yedekleme sonucu</returns>
        public async Task<BackupResult> CreateDifferentialBackupAsync(Guid databaseId)
        {
            try
            {
                _logger.LogInformation("Diferansiyel yedekleme işlemi başlatılıyor...");
                
                // Veritabanı bilgisini al
                var database = await _context.Databases.FindAsync(databaseId);
                if (database == null)
                {
                    _logger.LogError("Veritabanı bulunamadı: {DatabaseId}", databaseId);
                    return new BackupResult
                    {
                        Success = false,
                        Message = "Veritabanı bulunamadı.",
                        BackupFileName = null,
                        BackupPath = null,
                        BackupType = "Differential",
                        CreatedAt = DateTime.Now
                    };
                }
                
                // Önce tam yedekleme olup olmadığını kontrol et
                var lastFullBackup = await _context.DatabaseBackups
                    .Where(b => b.DatabaseId == databaseId && b.BackupType == "Full")
                    .OrderByDescending(b => b.CreatedAt)
                    .FirstOrDefaultAsync();
                
                if (lastFullBackup == null)
                {
                    _logger.LogError("Diferansiyel yedekleme için önce tam yedekleme gereklidir.");
                    return new BackupResult
                    {
                        Success = false,
                        Message = "Diferansiyel yedekleme için önce tam yedekleme gereklidir.",
                        BackupFileName = null,
                        BackupPath = null,
                        BackupType = "Differential",
                        CreatedAt = DateTime.Now
                    };
                }
                
                var databaseName = database.DatabaseName;
                var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                var backupFileName = $"{databaseName}_DIFF_{timestamp}.bak";
                var backupFilePath = Path.Combine(_backupBasePath, backupFileName);
                
                _logger.LogInformation("Veritabanı {DatabaseName} için diferansiyel yedekleme başlatılıyor. Dosya: {FilePath}", 
                    databaseName, backupFilePath);
                
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    // Yedekleme SQL komutunu oluştur
                    var backupQuery = $"BACKUP DATABASE [{databaseName}] TO DISK = '{backupFilePath}' WITH DIFFERENTIAL, INIT, NAME = '{databaseName}-Differential Database Backup', SKIP, NOREWIND, NOUNLOAD, STATS = 10";
                    
                    using (var command = new SqlCommand(backupQuery, connection))
                    {
                        command.CommandTimeout = 300; // 5 dakika
                        await command.ExecuteNonQueryAsync();
                    }
                }
                
                // Yedekleme kaydını veritabanına ekle
                var backup = new DatabaseBackup
                {
                    Id = Guid.NewGuid(),
                    DatabaseId = databaseId,
                    BackupType = "Differential",
                    BackupFileName = backupFileName,
                    BackupPath = backupFilePath,
                    SizeInMB = GetFileSizeInMB(backupFilePath),
                    CreatedAt = DateTime.Now,
                    CreatedBy = "System"
                };
                
                _context.DatabaseBackups.Add(backup);
                await _context.SaveChangesAsync();
                
                _logger.LogInformation("Diferansiyel yedekleme başarıyla tamamlandı. Dosya: {FilePath}, Boyut: {Size} MB", 
                    backupFilePath, backup.SizeInMB);
                
                return new BackupResult
                {
                    Success = true,
                    Message = "Diferansiyel yedekleme başarıyla tamamlandı.",
                    BackupFileName = backupFileName,
                    BackupPath = backupFilePath,
                    BackupType = "Differential",
                    CreatedAt = DateTime.Now,
                    SizeInMB = backup.SizeInMB
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Diferansiyel yedekleme sırasında hata oluştu.");
                return new BackupResult
                {
                    Success = false,
                    Message = $"Diferansiyel yedekleme sırasında hata oluştu: {ex.Message}",
                    BackupFileName = null,
                    BackupPath = null,
                    BackupType = "Differential",
                    CreatedAt = DateTime.Now
                };
            }
        }
        
        /// <summary>
        /// Veritabanı yedeğini geri yükler
        /// </summary>
        /// <param name="databaseId">Veritabanı ID</param>
        /// <param name="backupId">Yedek ID</param>
        /// <returns>Geri yükleme sonucu</returns>
        public async Task<RestoreResult> RestoreBackupAsync(Guid databaseId, Guid backupId)
        {
            try
            {
                _logger.LogInformation("Yedek geri yükleme işlemi başlatılıyor...");
                
                // Veritabanı bilgisini al
                var database = await _context.Databases.FindAsync(databaseId);
                if (database == null)
                {
                    _logger.LogError("Veritabanı bulunamadı: {DatabaseId}", databaseId);
                    return new RestoreResult
                    {
                        Success = false,
                        Message = "Veritabanı bulunamadı.",
                        DatabaseName = null,
                        CompletedAt = DateTime.Now
                    };
                }
                
                // Yedek bilgisini al
                var backup = await _context.DatabaseBackups.FindAsync(backupId);
                if (backup == null)
                {
                    _logger.LogError("Yedek bulunamadı: {BackupId}", backupId);
                    return new RestoreResult
                    {
                        Success = false,
                        Message = "Yedek bulunamadı.",
                        DatabaseName = database.DatabaseName,
                        CompletedAt = DateTime.Now
                    };
                }
                
                // Yedek dosyasının var olduğunu kontrol et
                if (!File.Exists(backup.BackupPath))
                {
                    _logger.LogError("Yedek dosyası bulunamadı: {FilePath}", backup.BackupPath);
                    return new RestoreResult
                    {
                        Success = false,
                        Message = "Yedek dosyası bulunamadı.",
                        DatabaseName = database.DatabaseName,
                        CompletedAt = DateTime.Now
                    };
                }
                
                var databaseName = database.DatabaseName;
                
                _logger.LogInformation("Veritabanı {DatabaseName} için yedek geri yükleme başlatılıyor. Dosya: {FilePath}", 
                    databaseName, backup.BackupPath);
                
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    // Veritabanını single user moduna al
                    var singleUserQuery = $"ALTER DATABASE [{databaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE";
                    using (var command = new SqlCommand(singleUserQuery, connection))
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                    
                    // Geri yükleme SQL komutunu oluştur
                    var restoreQuery = $"RESTORE DATABASE [{databaseName}] FROM DISK = '{backup.BackupPath}' WITH REPLACE, RECOVERY";
                    
                    using (var command = new SqlCommand(restoreQuery, connection))
                    {
                        command.CommandTimeout = 600; // 10 dakika
                        await command.ExecuteNonQueryAsync();
                    }
                    
                    // Veritabanını multi user moduna al
                    var multiUserQuery = $"ALTER DATABASE [{databaseName}] SET MULTI_USER";
                    using (var command = new SqlCommand(multiUserQuery, connection))
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                }
                
                _logger.LogInformation("Yedek geri yükleme başarıyla tamamlandı. Veritabanı: {DatabaseName}", databaseName);
                
                return new RestoreResult
                {
                    Success = true,
                    Message = "Yedek geri yükleme başarıyla tamamlandı.",
                    DatabaseName = databaseName,
                    CompletedAt = DateTime.Now
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Yedek geri yükleme sırasında hata oluştu.");
                return new RestoreResult
                {
                    Success = false,
                    Message = $"Yedek geri yükleme sırasında hata oluştu: {ex.Message}",
                    DatabaseName = null,
                    CompletedAt = DateTime.Now
                };
            }
        }
        
        /// <summary>
        /// Veritabanı yedeklerini listeler
        /// </summary>
        /// <param name="databaseId">Veritabanı ID</param>
        /// <param name="days">Kaç günlük yedekleri listeleyeceğimiz</param>
        /// <returns>Yedek listesi</returns>
        public async Task<List<DatabaseBackupDto>> GetBackupsAsync(Guid databaseId, int days = 7)
        {
            try
            {
                _logger.LogInformation("Veritabanı yedekleri listeleniyor. DatabaseId: {DatabaseId}, Days: {Days}", databaseId, days);
                
                var cutoffDate = DateTime.Now.AddDays(-days);
                
                var backups = await _context.DatabaseBackups
                    .Where(b => b.DatabaseId == databaseId && b.CreatedAt >= cutoffDate)
                    .OrderByDescending(b => b.CreatedAt)
                    .Select(b => new DatabaseBackupDto
                    {
                        Id = b.Id,
                        DatabaseId = b.DatabaseId,
                        BackupType = b.BackupType,
                        BackupFileName = b.BackupFileName,
                        SizeInMB = b.SizeInMB,
                        CreatedAt = b.CreatedAt,
                        CreatedBy = b.CreatedBy
                    })
                    .ToListAsync();
                
                _logger.LogInformation("{0} yedek bulundu.", backups.Count);
                return backups;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Yedekler listelenirken hata oluştu.");
                return new List<DatabaseBackupDto>();
            }
        }
        
        /// <summary>
        /// Veritabanı tablolarını listeler
        /// </summary>
        /// <param name="databaseId">Veritabanı ID</param>
        /// <returns>Tablo listesi</returns>
        public async Task<List<TableInfo>> GetTablesAsync(Guid databaseId)
        {
            try
            {
                _logger.LogInformation("Veritabanı tabloları listeleniyor. DatabaseId: {DatabaseId}", databaseId);
                
                // Veritabanı bilgisini al
                var database = await _context.Databases.FindAsync(databaseId);
                if (database == null)
                {
                    _logger.LogError("Veritabanı bulunamadı: {DatabaseId}", databaseId);
                    return new List<TableInfo>();
                }
                
                var databaseName = database.DatabaseName;
                var tables = new List<TableInfo>();
                
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    // Veritabanını seç
                    using (var command = new SqlCommand($"USE [{databaseName}]", connection))
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                    
                    // Tabloları listele
                    var query = @"
                        SELECT 
                            t.name AS TableName,
                            s.name AS SchemaName,
                            p.rows AS RowCount,
                            SUM(a.total_pages) * 8 / 1024.0 AS TotalSizeMB
                        FROM 
                            sys.tables t
                        INNER JOIN 
                            sys.schemas s ON t.schema_id = s.schema_id
                        INNER JOIN 
                            sys.indexes i ON t.object_id = i.object_id
                        INNER JOIN 
                            sys.partitions p ON i.object_id = p.object_id AND i.index_id = p.index_id
                        INNER JOIN 
                            sys.allocation_units a ON p.partition_id = a.container_id
                        WHERE 
                            t.is_ms_shipped = 0
                        GROUP BY 
                            t.name, s.name, p.rows
                        ORDER BY 
                            t.name";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var tableName = reader["TableName"].ToString();
                                var schemaName = reader["SchemaName"].ToString();
                                var rowCount = Convert.ToInt64(reader["RowCount"]);
                                var sizeMB = Convert.ToDouble(reader["TotalSizeMB"]);
                                
                                tables.Add(new TableInfo
                                {
                                    TableName = tableName,
                                    SchemaName = schemaName,
                                    FullName = $"{schemaName}.{tableName}",
                                    RowCount = rowCount,
                                    SizeMB = Math.Round(sizeMB, 2)
                                });
                            }
                        }
                    }
                }
                
                _logger.LogInformation("{Count} tablo bulundu.", tables.Count);
                return tables;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Tablolar listelenirken hata oluştu.");
                return new List<TableInfo>();
            }
        }
        
        /// <summary>
        /// SQL sorgusu çalıştırır
        /// </summary>
        /// <param name="databaseId">Veritabanı ID</param>
        /// <param name="sqlQuery">SQL sorgusu</param>
        /// <returns>Sorgu sonucu</returns>
        public async Task<QueryResult> ExecuteQueryAsync(Guid databaseId, string sqlQuery)
        {
            try
            {
                _logger.LogInformation("SQL sorgusu çalıştırılıyor. DatabaseId: {DatabaseId}", databaseId);
                
                // Veritabanı bilgisini al
                var database = await _context.Databases.FindAsync(databaseId);
                if (database == null)
                {
                    _logger.LogError("Veritabanı bulunamadı: {DatabaseId}", databaseId);
                    return new QueryResult
                    {
                        Success = false,
                        Message = "Veritabanı bulunamadı.",
                        Data = null
                    };
                }
                
                // SQL enjeksiyonu kontrolü - sadece SELECT sorgularına izin ver
                if (!sqlQuery.Trim().ToUpper().StartsWith("SELECT"))
                {
                    _logger.LogWarning("Sadece SELECT sorgularına izin verilir.");
                    return new QueryResult
                    {
                        Success = false,
                        Message = "Sadece SELECT sorgularına izin verilir.",
                        Data = null
                    };
                }
                
                var databaseName = database.DatabaseName;
                var dataTable = new DataTable();
                
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    // Veritabanını seç
                    using (var command = new SqlCommand($"USE [{databaseName}]", connection))
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                    
                    // Sorguyu çalıştır
                    using (var command = new SqlCommand(sqlQuery, connection))
                    {
                        command.CommandTimeout = 30; // 30 saniye
                        using (var adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
                
                _logger.LogInformation("Sorgu başarıyla çalıştırıldı. {RowCount} satır döndü.", dataTable.Rows.Count);
                
                return new QueryResult
                {
                    Success = true,
                    Message = $"{dataTable.Rows.Count} satır döndü.",
                    Data = dataTable
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sorgu çalıştırılırken hata oluştu.");
                return new QueryResult
                {
                    Success = false,
                    Message = $"Sorgu çalıştırılırken hata oluştu: {ex.Message}",
                    Data = null
                };
            }
        }
        
        /// <summary>
        /// Yedek ID'sine göre yedek bilgisini getirir
        /// </summary>
        /// <param name="backupId">Yedek ID</param>
        /// <returns>Yedek bilgisi</returns>
        public async Task<ErpMobile.Api.Entities.DatabaseBackup> GetBackupByIdAsync(Guid backupId)
        {
            try
            {
                _logger.LogInformation("Yedek bilgisi getiriliyor. BackupId: {BackupId}", backupId);
                
                var backup = await _context.DatabaseBackups.FindAsync(backupId);
                if (backup == null)
                {
                    _logger.LogWarning("Yedek bulunamadı: {BackupId}", backupId);
                }
                
                return backup;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Yedek bilgisi getirilirken hata oluştu.");
                return null;
            }
        }
        
        /// <summary>
        /// Zamanlanmış yedekleme işlemlerini gerçekleştirir
        /// </summary>
        /// <returns>Yedekleme sonuçları</returns>
        public async Task<List<BackupResult>> PerformScheduledBackupsAsync()
        {
            try
            {
                _logger.LogInformation("Zamanlanmış yedekleme işlemleri başlatılıyor...");
                
                // Ayarlardan otomatik yedekleme aktif mi kontrol et
                var autoBackupEnabled = await _globalSettings.GetSettingAsync<bool>("DatabaseBackup:AutoBackupEnabled", false);
                if (!autoBackupEnabled)
                {
                    _logger.LogInformation("Otomatik yedekleme devre dışı.");
                    return new List<BackupResult>();
                }
                
                // Tüm veritabanlarını al
                var databases = await GetDatabasesAsync();
                var results = new List<BackupResult>();
                
                foreach (var database in databases)
                {
                    try
                    {
                        _logger.LogInformation("Veritabanı {DatabaseName} için zamanlanmış yedekleme başlatılıyor.", database.DatabaseName);
                        
                        // Haftanın gününe göre tam veya diferansiyel yedekleme yap
                        var dayOfWeek = DateTime.Now.DayOfWeek;
                        BackupResult result;
                        
                        if (dayOfWeek == DayOfWeek.Sunday) // Pazar günleri tam yedekleme
                        {
                            result = await CreateFullBackupAsync(database.Id);
                        }
                        else // Diğer günler diferansiyel yedekleme
                        {
                            result = await CreateDifferentialBackupAsync(database.Id);
                        }
                        
                        results.Add(result);
                        
                        if (result.Success)
                        {
                            _logger.LogInformation("Veritabanı {DatabaseName} için zamanlanmış yedekleme başarılı.", database.DatabaseName);
                        }
                        else
                        {
                            _logger.LogWarning("Veritabanı {DatabaseName} için zamanlanmış yedekleme başarısız: {Message}", 
                                database.DatabaseName, result.Message);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Veritabanı {DatabaseName} için zamanlanmış yedekleme sırasında hata oluştu.", database.DatabaseName);
                        
                        results.Add(new BackupResult
                        {
                            Success = false,
                            Message = $"Hata oluştu: {ex.Message}",
                            BackupFileName = null,
                            BackupPath = null,
                            BackupType = "Scheduled",
                            CreatedAt = DateTime.Now
                        });
                    }
                }
                
                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Zamanlanmış yedekleme işlemleri sırasında hata oluştu.");
                return new List<BackupResult>();
            }
        }
        
        /// <summary>
        /// Eski yedekleri temizler
        /// </summary>
        /// <returns>Silinen yedek sayısı</returns>
        public async Task<int> CleanupOldBackupsAsync()
        {
            try
            {
                _logger.LogInformation("Eski yedekleri temizleme işlemi başlatılıyor...");
                
                // Ayarlardan saklama süresini al
                var retentionDays = await _globalSettings.GetSettingAsync<int>("DatabaseBackup:RetentionDays", 30);
                if (retentionDays <= 0)
                {
                    _logger.LogInformation("Saklama süresi 0 veya daha az. Temizleme yapılmayacak.");
                    return 0;
                }
                
                var cutoffDate = DateTime.Now.AddDays(-retentionDays);
                
                // Silinecek yedekleri bul
                var backupsToDelete = await _context.DatabaseBackups
                    .Where(b => b.CreatedAt < cutoffDate)
                    .ToListAsync();
                
                _logger.LogInformation("{Count} eski yedek bulundu.", backupsToDelete.Count);
                
                int deletedCount = 0;
                
                foreach (var backup in backupsToDelete)
                {
                    try
                    {
                        // Dosyayı sil
                        if (File.Exists(backup.BackupPath))
                        {
                            File.Delete(backup.BackupPath);
                            _logger.LogInformation("Yedek dosyası silindi: {FilePath}", backup.BackupPath);
                        }
                        
                        // Veritabanı kaydını sil
                        _context.DatabaseBackups.Remove(backup);
                        deletedCount++;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Yedek silinirken hata oluştu: {BackupId}", backup.Id);
                    }
                }
                
                if (deletedCount > 0)
                {
                    await _context.SaveChangesAsync();
                }
                
                _logger.LogInformation("{Count} eski yedek başarıyla silindi.", deletedCount);
                return deletedCount;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Eski yedekleri temizlerken hata oluştu.");
                return 0;
            }
        }
        
        /// <summary>
        /// Dosya boyutunu MB cinsinden hesaplar
        /// </summary>
        /// <param name="filePath">Dosya yolu</param>
        /// <returns>Dosya boyutu (MB)</returns>
        private double GetFileSizeInMB(string filePath)
        {
            if (!File.Exists(filePath))
                return 0;
                
            var fileInfo = new FileInfo(filePath);
            return Math.Round(fileInfo.Length / (1024.0 * 1024.0), 2); // Byte -> MB
        }
        
        /// <summary>
        /// Toplam yedek sayısını getirir
        /// </summary>
        /// <returns>Toplam yedek sayısı</returns>
        public async Task<int> GetTotalBackupCountAsync()
        {
            try
            {
                return await _context.DatabaseBackups.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Toplam yedek sayısı alınırken hata oluştu");
                return 0;
            }
        }
        
        /// <summary>
        /// Yedekleme istatistiklerini getirir
        /// </summary>
        /// <returns>Yedekleme istatistikleri</returns>
        public async Task<BackupStatistics> GetBackupStatisticsAsync()
        {
            try
            {
                var now = DateTime.Now;
                var stats = new BackupStatistics();
                
                // Toplam yedek sayısı
                stats.TotalBackupCount = await _context.DatabaseBackups.CountAsync();
                
                // Son 24 saatteki yedekler
                stats.BackupsLast24Hours = await _context.DatabaseBackups
                    .Where(b => b.CreatedAt >= now.AddHours(-24))
                    .CountAsync();
                    
                // Son 7 gündeki yedekler
                stats.BackupsLast7Days = await _context.DatabaseBackups
                    .Where(b => b.CreatedAt >= now.AddDays(-7))
                    .CountAsync();
                    
                // Son 30 gündeki yedekler
                stats.BackupsLast30Days = await _context.DatabaseBackups
                    .Where(b => b.CreatedAt >= now.AddDays(-30))
                    .CountAsync();
                    
                // Toplam yedek boyutu
                var backups = await _context.DatabaseBackups.ToListAsync();
                stats.TotalBackupSizeMB = backups.Sum(b => b.SizeInMB);
                
                // Veritabanı başına yedek sayısı
                var backupsByDatabase = backups.GroupBy(b => b.DatabaseId);
                foreach (var group in backupsByDatabase)
                {
                    var dbInfo = await _context.Databases
                        .FirstOrDefaultAsync(d => d.Id == group.Key);
                        
                    if (dbInfo != null)
                    {
                        stats.BackupCountPerDatabase[dbInfo.DatabaseName] = group.Count();
                    }
                }
                
                // Son başarılı yedekleme
                var lastBackup = await _context.DatabaseBackups
                    .OrderByDescending(b => b.CreatedAt)
                    .FirstOrDefaultAsync();
                    
                if (lastBackup != null)
                {
                    stats.LastSuccessfulBackup = lastBackup.CreatedAt;
                }
                
                return stats;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Yedekleme istatistikleri alınırken hata oluştu");
                return new BackupStatistics();
            }
        }
        
        /// <summary>
        /// Disk alanını kontrol eder
        /// </summary>
        /// <param name="path">Kontrol edilecek dizin</param>
        /// <param name="requiredSpaceGB">Gerekli boş alan (GB)</param>
        /// <returns>Yeterli alan varsa true</returns>
        public async Task<bool> CheckDiskSpaceAsync(string path, double requiredSpaceGB)
        {
            try
            {
                if (string.IsNullOrEmpty(path))
                {
                    path = _backupBasePath;
                }
                
                if (!Directory.Exists(path))
                {
                    _logger.LogWarning("Disk alanı kontrolü için dizin bulunamadı: {Path}", path);
                    return false;
                }
                
                var driveInfo = new DriveInfo(new DirectoryInfo(path).Root.FullName);
                var availableSpaceGB = driveInfo.AvailableFreeSpace / (1024.0 * 1024 * 1024);
                
                _logger.LogInformation("Disk alanı kontrolü: {Path}, Mevcut: {Available:F2} GB, Gerekli: {Required:F2} GB", 
                    path, availableSpaceGB, requiredSpaceGB);
                    
                return availableSpaceGB >= requiredSpaceGB;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Disk alanı kontrolü sırasında hata oluştu: {Path}", path);
                return false;
            }
        }
        
        /// <summary>
        /// Veritabanı başına maksimum yedek sayısını yönetir
        /// </summary>
        /// <param name="databaseId">Veritabanı ID</param>
        /// <param name="maxBackupsPerDatabase">Maksimum yedek sayısı</param>
        /// <returns>İşlem başarılı ise true</returns>
        public async Task<bool> ManageBackupLimitsAsync(Guid databaseId, int maxBackupsPerDatabase)
        {
            try
            {
                if (maxBackupsPerDatabase <= 0)
                {
                    _logger.LogInformation("Maksimum yedek sınırı devre dışı bırakılmış (MaxBackupsPerDatabase <= 0)");
                    return true;
                }
                
                // Veritabanına ait yedekleri al
                var backups = await _context.DatabaseBackups
                    .Where(b => b.DatabaseId == databaseId)
                    .OrderByDescending(b => b.CreatedAt)
                    .ToListAsync();
                    
                if (backups.Count <= maxBackupsPerDatabase)
                {
                    // Sınır aşılmamış
                    return true;
                }
                
                _logger.LogInformation("Veritabanı {DatabaseId} için yedek sınırı aşıldı. Mevcut: {Current}, Maksimum: {Max}", 
                    databaseId, backups.Count, maxBackupsPerDatabase);
                    
                // Silinecek yedekleri belirle (en eskiler)
                var backupsToDelete = backups.Skip(maxBackupsPerDatabase).ToList();
                
                foreach (var backup in backupsToDelete)
                {
                    try
                    {
                        // Dosyayı sil
                        if (File.Exists(backup.BackupPath))
                        {
                            File.Delete(backup.BackupPath);
                            _logger.LogInformation("Yedek dosyası silindi: {FilePath}", backup.BackupPath);
                        }
                        
                        // Veritabanı kaydını sil
                        _context.DatabaseBackups.Remove(backup);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Yedek silinirken hata oluştu: {BackupId}", backup.Id);
                    }
                }
                
                await _context.SaveChangesAsync();
                _logger.LogInformation("{Count} eski yedek başarıyla silindi.", backupsToDelete.Count);
                
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Yedek sınırları yönetilirken hata oluştu. DatabaseId: {DatabaseId}", databaseId);
                return false;
            }
        }
    }
}
