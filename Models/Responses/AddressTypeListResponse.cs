using System.Collections.Generic;

namespace erp_api.Models.Responses
{
    public class AddressTypeListResponse
    {
        public int Id { get; set; }
        public string AddressTypeCode { get; set; }
        public string AddressTypeName { get; set; }
        public bool IsActive { get; set; }
        public List<AddressTypeListResponse> AddressTypes { get; set; }
    }
} 