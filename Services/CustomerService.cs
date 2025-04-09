using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;
using Api.Models.Results;
using Api.Repositories;
using Microsoft.Extensions.Logging;

namespace Api.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly CustomerRepository _customerRepository;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(CustomerRepository customerRepository, ILogger<CustomerService> logger)
        {
            _customerRepository = customerRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<CustomerListDto>> GetCustomerList()
        {
            try
            {
                return await _customerRepository.GetCustomerList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting customer list");
                throw;
            }
        }

        public async Task<CustomerListDto> GetCustomerById(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    throw new ArgumentException("Customer ID cannot be null or empty", nameof(id));
                }

                var customer = await _customerRepository.GetCustomerById(id);
                if (customer == null)
                {
                    throw new KeyNotFoundException($"Customer with ID {id} not found");
                }

                return customer;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting customer by ID: {CustomerId}", id);
                throw;
            }
        }

        public async Task<IEnumerable<CustomerTypeResult>> GetCustomerTypes(string langCode)
        {
            try
            {
                if (string.IsNullOrEmpty(langCode))
                {
                    langCode = "TR"; // Default language code
                }

                return await _customerRepository.GetCustomerTypes(langCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting customer types for language: {LangCode}", langCode);
                throw;
            }
        }

        public async Task<IEnumerable<CustomerDiscountGrResult>> GetCustomerDiscountGroups(string langCode)
        {
            try
            {
                if (string.IsNullOrEmpty(langCode))
                {
                    langCode = "TR"; // Default language code
                }

                return await _customerRepository.GetCustomerDiscountGroups(langCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting customer discount groups for language: {LangCode}", langCode);
                throw;
            }
        }

        public async Task<IEnumerable<CustomerPaymentPlanGrResult>> GetCustomerPaymentPlanGroups(string langCode)
        {
            try
            {
                if (string.IsNullOrEmpty(langCode))
                {
                    langCode = "TR"; // Default language code
                }

                return await _customerRepository.GetCustomerPaymentPlanGroups(langCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting customer payment plan groups for language: {LangCode}", langCode);
                throw;
            }
        }
    }
} 