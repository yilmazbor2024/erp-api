using System;

namespace ErpMobile.Api.Models.Responses
{
    /// <summary>
    /// Response model for warehouse information.
    /// </summary>
    public class WarehouseResponse
    {
        /// <summary>
        /// Gets or sets the warehouse code.
        /// </summary>
        public string WarehouseCode { get; set; }

        /// <summary>
        /// Gets or sets the warehouse owner code.
        /// </summary>
        public string WarehouseOwnerCode { get; set; }

        /// <summary>
        /// Gets or sets the warehouse type code.
        /// </summary>
        public string WarehouseTypeCode { get; set; }

        /// <summary>
        /// Gets or sets the warehouse type description.
        /// </summary>
        public string WarehouseTypeDescription { get; set; }

        /// <summary>
        /// Gets or sets the warehouse category code.
        /// </summary>
        public string WarehouseCategoryCode { get; set; }

        /// <summary>
        /// Gets or sets the warehouse category description.
        /// </summary>
        public string WarehouseCategoryDescription { get; set; }

        /// <summary>
        /// Gets or sets the office code.
        /// </summary>
        public string OfficeCode { get; set; }

        /// <summary>
        /// Gets or sets the warehouse description.
        /// </summary>
        public string WarehouseDescription { get; set; }

        /// <summary>
        /// Gets or sets whether this is the default warehouse.
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// Gets or sets whether the warehouse is blocked.
        /// </summary>
        public bool IsBlocked { get; set; }
    }
} 