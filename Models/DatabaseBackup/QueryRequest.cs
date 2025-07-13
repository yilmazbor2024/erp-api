using System;

namespace ErpMobile.Api.Models.DatabaseBackup
{
    /// <summary>
    /// SQL sorgu isteği modeli
    /// </summary>
    public class QueryRequest
    {
        /// <summary>
        /// Veritabanı ID
        /// </summary>
        public Guid DatabaseId { get; set; }
        
        /// <summary>
        /// SQL sorgusu (sadece SELECT sorgularına izin verilir)
        /// </summary>
        public string SqlQuery { get; set; }
    }
}
