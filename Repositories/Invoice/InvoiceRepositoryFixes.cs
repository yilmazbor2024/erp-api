using System;
using System.Data.Common;

namespace erp_api.Repositories.Invoice
{
    /// <summary>
    /// Yardımcı metotları içeren sınıf
    /// </summary>
    public static class InvoiceRepositoryFixes
    {
        /// <summary>
        /// Veritabanından okunan değeri güvenli bir şekilde nullable int'e dönüştürür
        /// </summary>
        /// <param name="value">Dönüştürülecek değer</param>
        /// <returns>Nullable int değeri veya null</returns>
        public static int? SafeConvertToNullableInt(object value)
        {
            if (value == null || value == DBNull.Value)
                return null;

            // String değerini kontrol et
            if (value is string strValue)
            {
                if (string.IsNullOrWhiteSpace(strValue))
                    return null;

                if (int.TryParse(strValue, out int result))
                    return result;
                
                return null;
            }

            // Diğer tip dönüşümleri
            try
            {
                return Convert.ToInt32(value);
            }
            catch
            {
                return null;
            }
        }
    }
}
