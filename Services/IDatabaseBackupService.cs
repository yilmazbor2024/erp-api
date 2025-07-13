using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using ErpMobile.Api.Entities;
using ErpMobile.Api.Models.DatabaseBackup;

namespace ErpMobile.Api.Services
{
    public interface IDatabaseBackupService
    {
        Task<List<DatabaseInfo>> GetDatabasesAsync();
        Task<BackupResult> CreateFullBackupAsync(Guid databaseId);
        Task<BackupResult> CreateDifferentialBackupAsync(Guid databaseId);
        Task<List<DatabaseBackupDto>> GetBackupsAsync(Guid databaseId, int days = 7);
        Task<RestoreResult> RestoreBackupAsync(Guid databaseId, Guid backupId);
        Task<ErpMobile.Api.Entities.DatabaseBackup> GetBackupByIdAsync(Guid backupId);
        Task<List<TableInfo>> GetTablesAsync(Guid databaseId);
        Task<QueryResult> ExecuteQueryAsync(Guid databaseId, string sqlQuery);
        Task<List<BackupResult>> PerformScheduledBackupsAsync();
        Task<int> CleanupOldBackupsAsync();
        
        // Yeni metotlar
        Task<int> GetTotalBackupCountAsync();
        Task<BackupStatistics> GetBackupStatisticsAsync();
        Task<bool> CheckDiskSpaceAsync(string path, double requiredSpaceGB);
        Task<bool> ManageBackupLimitsAsync(Guid databaseId, int maxBackupsPerDatabase);
    }
}
