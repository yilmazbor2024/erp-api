using System.Collections.Generic;

namespace ErpMobile.Api.Models.Common
{
    /// <summary>
    /// Generic paged response model for API endpoints that return paginated lists
    /// </summary>
    /// <typeparam name="T">The type of data being returned in the paged response</typeparam>
    public class PagedResponse<T>
    {
        /// <summary>
        /// The current page number
        /// </summary>
        public int PageNumber { get; set; }
        
        /// <summary>
        /// The size of each page
        /// </summary>
        public int PageSize { get; set; }
        
        /// <summary>
        /// The total number of pages available
        /// </summary>
        public int TotalPages { get; set; }
        
        /// <summary>
        /// The total count of items across all pages
        /// </summary>
        public int TotalCount { get; set; }
        
        /// <summary>
        /// The total number of records (same as TotalCount)
        /// </summary>
        public int TotalRecords { get; set; }
        
        /// <summary>
        /// Indicates if there is a previous page available
        /// </summary>
        public bool HasPreviousPage { get; set; }
        
        /// <summary>
        /// Indicates if there is a next page available
        /// </summary>
        public bool HasNextPage { get; set; }
        
        /// <summary>
        /// The data for the current page
        /// </summary>
        public IReadOnlyList<T> Items { get; set; }
        
        /// <summary>
        /// The data for the current page (alternative property name)
        /// </summary>
        public List<T> Data { get; set; }
        
        /// <summary>
        /// Creates a new instance of PagedResponse
        /// </summary>
        public PagedResponse()
        {
            Items = new List<T>();
            Data = new List<T>();
        }
        
        /// <summary>
        /// Creates a new instance of PagedResponse with the specified parameters
        /// </summary>
        public PagedResponse(IReadOnlyList<T> items, int totalCount, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalRecords = totalCount;
            TotalPages = (totalCount + pageSize - 1) / pageSize;
            Items = items;
            Data = items as List<T> ?? new List<T>(items);
        }
    }
}