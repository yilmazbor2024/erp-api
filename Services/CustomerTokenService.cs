using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ErpMobile.Api.Models.Customer;
using ErpMobile.Api.Models.Requests;
using ErpMobile.Api.Models.Responses;
using ErpMobile.Api.Interfaces;
using ErpMobile.Api.Data;
using Dapper;
using System.Linq;

namespace ErpMobile.Api.Services
{
    public class CustomerTokenService : ICustomerTokenService
    {
        private readonly ILogger<CustomerTokenService> _logger;
        private readonly string _connectionString;
        private readonly ErpDbContext _context;
        private readonly ICustomerServiceNew _customerServiceNew;

        public CustomerTokenService(ILogger<CustomerTokenService> logger, IConfiguration configuration, ErpDbContext context, ICustomerServiceNew customerServiceNew)
        {
            _logger = logger;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _context = context;
            _customerServiceNew = customerServiceNew;
        }

        /// <summary>
        /// Token ile müşteri oluşturma - CustomerServiceNew.CreateCustomerAsync ile aynı mantıkta çalışır
        /// </summary>
        /// <param name="request">Müşteri oluşturma isteği</param>
        /// <returns>Oluşturulan müşteri bilgileri</returns>
        public async Task<CustomerCreateResponse> CreateCustomerWithTokenAsync(CustomerCreateRequestNew request)
        {
            _logger.LogInformation("[CustomerTokenService.CreateCustomerWithTokenAsync] - Müşteri oluşturma işlemi başlatıldı");
            
            // CustomerServiceNew.CreateCustomerAsync metodunu çağır
            // Bu sayede kod tekrarından kaçınmış oluruz
            var resultNew = await _customerServiceNew.CreateCustomerAsync(request);
            
            // CustomerCreateResponseNew'i CustomerCreateResponse'a dönüştür
            var response = new CustomerCreateResponse
            {
                CustomerCode = resultNew.CustomerCode,
                CustomerName = resultNew.CustomerName,
                TaxNumber = resultNew.TaxNumber,
                TaxOfficeCode = resultNew.TaxOfficeCode,
                CreatedDate = resultNew.CreatedDate,
                CreatedBy = resultNew.CreatedUsername,
                Success = resultNew.Success,
                Message = resultNew.Message
            };
            
            return response;
        }
    }
}
