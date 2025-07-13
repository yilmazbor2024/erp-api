using System;

namespace ErpMobile.Api.Models.DatabaseBackup
{
    /// <summary>
    /// Veritabanı tablo bilgilerini içeren model
    /// </summary>
    public class TableInfo
    {
        /// <summary>
        /// Tablo adı
        /// </summary>
        public string TableName { get; set; }
        
        /// <summary>
        /// Şema adı
        /// </summary>
        public string SchemaName { get; set; }
        
        /// <summary>
        /// Tam tablo adı (şema.tablo)
        /// </summary>
        public string FullName { get; set; }
        
        /// <summary>
        /// Satır sayısı
        /// </summary>
        public long RowCount { get; set; }
        
        /// <summary>
        /// Tablo boyutu (MB)
        /// </summary>
        public double SizeMB { get; set; }
    }
}
