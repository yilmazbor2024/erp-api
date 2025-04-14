using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using erp_api.Models.Responses;
using erp_api.Models.Requests;
using erp_api.Models.Contact;

namespace ErpMobile.Api.Interfaces
{
    public interface ICustomerService
    {
        Task<List<CustomerListResponse>> GetCustomerListAsync(CustomerFilterRequest request);
        Task<CustomerDetailResponse> GetCustomerByCodeAsync(string customerCode);
        Task<CustomerResponse> CreateCustomerAsync(CustomerCreateRequest request);
        Task<List<CustomerAddressResponse>> GetCustomerAddressesAsync(string customerCode);
        Task<List<CustomerContactResponse>> GetCustomerContactsAsync(string customerCode);
        Task<List<CustomerCommunicationResponse>> GetCustomerCommunicationsAsync(string customerCode);
        Task<List<AddressTypeResponse>> GetAddressTypesAsync();
        Task<AddressTypeResponse> GetAddressTypeByCodeAsync(string code);
        Task<AddressTypeResponse> CreateAddressTypeAsync(AddressTypeCreateRequest request);
        Task<AddressTypeResponse> UpdateAddressTypeAsync(string code, AddressTypeUpdateRequest request);
        Task<bool> DeleteAddressTypeAsync(string code);
        Task<List<AddressResponse>> GetAddressesAsync(string customerCode);
        Task<AddressResponse> GetAddressByIdAsync(string customerCode, string addressId);
        Task<AddressResponse> CreateAddressAsync(AddressCreateRequest request);
        Task<AddressResponse> UpdateAddressAsync(string customerCode, string addressTypeCode, AddressUpdateRequest request);
        Task<bool> DeleteAddressAsync(string customerCode, string addressTypeCode);
        Task<List<erp_api.Models.Responses.ContactResponse>> GetContactsAsync(string customerCode);
        Task<erp_api.Models.Responses.ContactResponse> GetContactByIdAsync(string customerCode, string contactId);
        Task<erp_api.Models.Responses.ContactResponse> CreateContactAsync(ContactCreateRequest request);
        Task<erp_api.Models.Responses.ContactResponse> UpdateContactAsync(string customerCode, string contactTypeCode, ContactUpdateRequest request);
        Task<bool> DeleteContactAsync(string customerCode, string contactTypeCode);
        Task<List<CustomerTypeResponse>> GetCustomerTypesAsync();
        Task<List<CustomerDiscountGroupResponse>> GetCustomerDiscountGroupsAsync();
        Task<List<CustomerPaymentPlanGroupResponse>> GetCustomerPaymentPlanGroupsAsync();
        Task<List<RegionResponse>> GetRegionsAsync();
        Task<List<CityResponse>> GetCitiesByRegionAsync(string regionCode);
        Task<List<DistrictResponse>> GetDistrictsByCityAsync(string cityCode);
        Task<List<ContactTypeResponse>> GetContactTypesAsync();
        Task<ContactTypeResponse> GetContactTypeByCodeAsync(string code);
        Task<CustomerDetailResponse> GetCustomerByIdAsync(string id);
        Task<PagedResponse<CustomerListResponse>> GetCustomerListAsync(CustomerFilterRequest filter);
        Task<CustomerCreateResponse> CreateCustomerAsync(CustomerCreateRequest request);
    }
} 