using System;
using System.Collections.Generic;

namespace ErpMobile.Api.Models.Common
{
    /// <summary>
    /// ApiResponse sınıfı için uzantı metodları
    /// </summary>
    public static class ApiResponseExtensions
    {
        /// <summary>
        /// Başarılı bir ApiResponse oluşturur
        /// </summary>
        public static ApiResponse<T> Success<T>(this T data, string message = "İşlem başarıyla tamamlandı")
        {
            return new ApiResponse<T>(data, true, message);
        }

        /// <summary>
        /// Başarısız bir ApiResponse oluşturur
        /// </summary>
        public static ApiResponse<T> Failure<T>(string message, string error = null)
        {
            return new ApiResponse<T>(default, false, message, error);
        }
    }
}
