using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using erp_api.Models.Responses;
using erp_api.Models.Requests;
using erp_api.Models.Contact;
using erp_api.Models.Common;
using ErpMobile.Api.Models.Customer;
using ErpAPI.Models.Requests;

namespace ErpMobile.Api.Interfaces
{
    public interface ICustomerService
    {
        Task<PagedResponse<CustomerListResponse>> GetCustomerListAsync(CustomerFilterRequest filter);
        Task<CustomerDetailResponse> GetCustomerByCodeAsync(string customerCode);
        Task<CustomerDetailResponse> GetCustomerByIdAsync(string id);
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
        Task<AddressResponse> CreateAddressAsync(string customerCode, AddressCreateRequest request);
        Task<AddressResponse> UpdateAddressAsync(string customerCode, string addressTypeCode, AddressUpdateRequest request);
        Task<bool> DeleteAddressAsync(string customerCode, string addressTypeCode);
        Task<List<erp_api.Models.Responses.ContactResponse>> GetContactsAsync(string customerCode);
        Task<erp_api.Models.Responses.ContactResponse> GetContactByIdAsync(string customerCode, string contactId);
        Task<erp_api.Models.Responses.ContactResponse> CreateContactAsync(string customerCode, ContactCreateRequest request);
        Task<erp_api.Models.Responses.ContactResponse> UpdateContactAsync(string customerCode, string contactTypeCode, ContactUpdateRequest request);
        Task<bool> DeleteContactAsync(string customerCode, string contactTypeCode);
        Task<List<CustomerTypeResponse>> GetCustomerTypesAsync();
        Task<List<CustomerDiscountGroupResponse>> GetCustomerDiscountGroupsAsync();
        Task<List<CustomerPaymentPlanGroupResponse>> GetCustomerPaymentPlanGroupsAsync();
        Task<List<RegionResponse>> GetRegionsAsync();
        Task<List<StateResponse>> GetStatesAsync(string countryCode = null);
        Task<List<CityResponse>> GetCitiesAsync();
        Task<List<CityResponse>> GetCitiesByStateAsync(string stateCode);
        Task<List<CityResponse>> GetCitiesByRegionAsync(string regionCode);
        Task<List<DistrictResponse>> GetDistrictsByCityAsync(string cityCode);
        Task<List<DistrictResponse>> GetAllDistrictsAsync();
        Task<List<ContactTypeResponse>> GetContactTypesAsync();
        Task<ContactTypeResponse> GetContactTypeByCodeAsync(string code);
    }
} 