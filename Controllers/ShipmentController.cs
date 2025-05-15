using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Data.SqlClient;
using ErpMobile.Api.Models.Common;
using ErpMobile.Api.Models.Shipment;
using ErpMobile.Api.Data;

namespace ErpMobile.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ShipmentController : ControllerBase
    {
        private readonly ILogger<ShipmentController> _logger;
        private readonly ErpDbContext _context;

        public ShipmentController(ILogger<ShipmentController> logger, ErpDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// Sevkiyat bilgilerini getirir
        /// </summary>
        /// <param name="customerCode">Müşteri kodu (isteğe bağlı)</param>
        /// <param name="langCode">Dil kodu (varsayılan: TR)</param>
        /// <returns>Sevkiyat bilgileri listesi</returns>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<ShipmentAddressModel>>>> GetShipmentAddresses(
            [FromQuery] string customerCode = null,
            [FromQuery] string langCode = "TR")
        {
            try
            {
                var whereClause = string.IsNullOrEmpty(customerCode) ? "" : "WHERE cdCurrAccShipmentAddress.CurrAccCode = @CustomerCode";
                
                var query = $@"
                    SELECT 
                        ShipmentAddressCode = RTRIM(LTRIM(cdCurrAccShipmentAddress.ShipmentAddressCode)),
                        CurrAccCode = RTRIM(LTRIM(cdCurrAccShipmentAddress.CurrAccCode)),
                        AddressCode = RTRIM(LTRIM(cdCurrAccShipmentAddress.AddressCode)),
                        AddressDescription = RTRIM(LTRIM(ISNULL(cdCurrAccAddressDesc.AddressDescription, ''))),
                        Address = RTRIM(LTRIM(ISNULL(cdCurrAccAddress.Address, ''))),
                        City = RTRIM(LTRIM(ISNULL(cdCurrAccAddress.City, ''))),
                        District = RTRIM(LTRIM(ISNULL(cdCurrAccAddress.District, ''))),
                        PostalCode = RTRIM(LTRIM(ISNULL(cdCurrAccAddress.PostalCode, ''))),
                        CountryCode = RTRIM(LTRIM(ISNULL(cdCurrAccAddress.CountryCode, ''))),
                        IsDefault = cdCurrAccShipmentAddress.IsDefault,
                        IsBlocked = cdCurrAccShipmentAddress.IsBlocked
                    FROM cdCurrAccShipmentAddress WITH (NOLOCK)
                    LEFT JOIN cdCurrAccAddress WITH (NOLOCK) 
                        ON cdCurrAccShipmentAddress.CurrAccCode = cdCurrAccAddress.CurrAccCode 
                        AND cdCurrAccShipmentAddress.AddressCode = cdCurrAccAddress.AddressCode
                    LEFT JOIN cdCurrAccAddressDesc WITH (NOLOCK) 
                        ON cdCurrAccAddress.CurrAccCode = cdCurrAccAddressDesc.CurrAccCode 
                        AND cdCurrAccAddress.AddressCode = cdCurrAccAddressDesc.AddressCode 
                        AND cdCurrAccAddressDesc.LangCode = @LangCode
                    {whereClause}
                    ORDER BY cdCurrAccShipmentAddress.IsDefault DESC, cdCurrAccShipmentAddress.ShipmentAddressCode";

                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@LangCode", langCode)
                };

                if (!string.IsNullOrEmpty(customerCode))
                {
                    parameters.Add(new SqlParameter("@CustomerCode", customerCode));
                }

                var shipmentAddresses = new List<ShipmentAddressModel>();
                
                using (var reader = await _context.ExecuteReaderAsync(query, parameters.ToArray()))
                {
                    while (await reader.ReadAsync())
                    {
                        shipmentAddresses.Add(new ShipmentAddressModel
                        {
                            ShipmentAddressCode = reader["ShipmentAddressCode"].ToString(),
                            CurrAccCode = reader["CurrAccCode"].ToString(),
                            AddressCode = reader["AddressCode"].ToString(),
                            AddressDescription = reader["AddressDescription"].ToString(),
                            Address = reader["Address"].ToString(),
                            City = reader["City"].ToString(),
                            District = reader["District"].ToString(),
                            PostalCode = reader["PostalCode"].ToString(),
                            CountryCode = reader["CountryCode"].ToString(),
                            IsDefault = Convert.ToBoolean(reader["IsDefault"]),
                            IsBlocked = Convert.ToBoolean(reader["IsBlocked"])
                        });
                    }
                }
                
                return Ok(new ApiResponse<List<ShipmentAddressModel>>
                {
                    Success = true,
                    Message = "Sevkiyat bilgileri başarıyla getirildi",
                    Data = shipmentAddresses
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sevkiyat bilgileri getirilirken hata oluştu. Müşteri Kodu: {CustomerCode}", customerCode);
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "Sevkiyat bilgileri getirilirken bir hata oluştu",
                    Error = ex.Message
                });
            }
        }
    }
}
