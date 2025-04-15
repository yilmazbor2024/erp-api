using System;

namespace erp_api.Models.Common
{
    /// <summary>
    /// Generic API response wrapper for standardized API responses
    /// </summary>
    /// <typeparam name="T">The type of data being returned in the response</typeparam>
    public class ApiResponse<T>
    {
        /// <summary>
        /// The data payload of the response
        /// </summary>
        public T Data { get; set; }
        
        /// <summary>
        /// Indicates if the API request was successful
        /// </summary>
        public bool Success { get; set; }
        
        /// <summary>
        /// A message describing the result of the API request
        /// </summary>
        public string Message { get; set; }
        
        /// <summary>
        /// An optional error message, typically provided when Success is false
        /// </summary>
        public string Error { get; set; }
        
        /// <summary>
        /// Creates a new instance of ApiResponse
        /// </summary>
        public ApiResponse()
        {
        }
        
        /// <summary>
        /// Creates a new instance of ApiResponse with the specified parameters
        /// </summary>
        public ApiResponse(T data, bool success, string message, string error = null)
        {
            Data = data;
            Success = success;
            Message = message;
            Error = error;
        }
        
        /// <summary>
        /// Creates a successful response with the specified data and message
        /// </summary>
        public static ApiResponse<T> Ok(T data, string message = "Operation completed successfully")
        {
            return new ApiResponse<T>(data, true, message);
        }
        
        /// <summary>
        /// Creates a failed response with the specified error message
        /// </summary>
        public static ApiResponse<T> Fail(string errorMessage, string message = "Operation failed")
        {
            return new ApiResponse<T>(default, false, message, errorMessage);
        }
    }
} 