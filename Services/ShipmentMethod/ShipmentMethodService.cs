using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ErpMobile.Api.Models.Common;
using ErpMobile.Api.Models.Responses;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ErpMobile.Api.Services.ShipmentMethod
{
    public class ShipmentMethodService : IShipmentMethodService
    {
        private readonly ILogger<ShipmentMethodService> _logger;
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly string _langCode = "TR";

        public ShipmentMethodService(
            ILogger<ShipmentMethodService> logger,
            IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("ErpConnection");
        }

        public async Task<ApiResponse<List<ShipmentMethodResponse>>> GetShipmentMethodsAsync()
        {
            try
            {
                var shipmentMethods = new List<ShipmentMethodResponse>();
                
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    var query = @"
                        SELECT ShipmentMethodCode
                            , ShipmentMethodDescription
                            , TransportModeDescription = ISNULL(bsTransportModeDesc.TransportModeDescription, SPACE(0))
                            , ShipmentMethod.TransportModeCode
                            , ShipmentMethod.IsBlocked
                        FROM ShipmentMethod(@LangCode)
                                LEFT OUTER JOIN bsTransportModeDesc WITH(NOLOCK)
                                    ON ShipmentMethod.TransportModeCode = bsTransportModeDesc.TransportModeCode
                                    AND bsTransportModeDesc.LangCode = @LangCode
                        WHERE ShipmentMethodCode <> SPACE(0)";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LangCode", _langCode);
                        
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                shipmentMethods.Add(new ShipmentMethodResponse
                                {
                                    ShipmentMethodCode = reader["ShipmentMethodCode"].ToString(),
                                    ShipmentMethodDescription = reader["ShipmentMethodDescription"].ToString(),
                                    TransportModeDescription = reader["TransportModeDescription"].ToString(),
                                    TransportModeCode = reader["TransportModeCode"].ToString(),
                                    IsBlocked = Convert.ToBoolean(reader["IsBlocked"])
                                });
                            }
                        }
                    }
                }
                
                return new ApiResponse<List<ShipmentMethodResponse>>
                {
                    Success = true,
                    Message = "Sevkiyat yöntemleri başarıyla getirildi",
                    Data = shipmentMethods
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sevkiyat yöntemleri getirilirken hata oluştu");
                return new ApiResponse<List<ShipmentMethodResponse>>
                {
                    Success = false,
                    Message = "Sevkiyat yöntemleri getirilirken bir hata oluştu: " + ex.Message
                };
            }
        }
    }
}
