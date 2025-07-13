using System;
using System.Data;

namespace ErpMobile.Api.Models.DatabaseBackup
{
    /// <summary>
    /// SQL sorgu sonucunu içeren model
    /// </summary>
    public class QueryResult
    {
        /// <summary>
        /// İşlem başarılı mı
        /// </summary>
        public bool Success { get; set; }
        
        /// <summary>
        /// İşlem mesajı
        /// </summary>
        public string Message { get; set; }
        
        /// <summary>
        /// Sorgu sonucu veri tablosu
        /// </summary>
        public DataTable Data { get; set; }
    }
}
