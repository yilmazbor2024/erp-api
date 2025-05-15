using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Data.SqlClient;
using ErpMobile.Api.Models.Common;
using ErpMobile.Api.Models.Office;
using ErpMobile.Api.Data;

namespace ErpMobile.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OfficeController : ControllerBase
    {
        private readonly ILogger<OfficeController> _logger;
        private readonly ErpDbContext _context;

        public OfficeController(ILogger<OfficeController> logger, ErpDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// Ofis (şube) listesini getirir
        /// </summary>
        /// <param name="companyCode">Şirket kodu (isteğe bağlı)</param>
        /// <param name="langCode">Dil kodu (varsayılan: TR)</param>
        /// <returns>Ofis listesi</returns>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<OfficeModel>>>> GetOffices(
            [FromQuery] string companyCode = null,
            [FromQuery] string langCode = "TR")
        {
            try
            {
                var whereClause = string.IsNullOrEmpty(companyCode) ? "" : "WHERE CompanyCode = @CompanyCode";
                
                var query = $@"
                    SELECT 
                        OfficeCode,
                        CompanyCode,
                        IsExecutiveOffice,
                        IsBlocked,
                        LangCode,
                        OfficeDescription
                    FROM dbo.Office(@LangCode)
                    {whereClause}
                    ORDER BY OfficeCode";

                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@LangCode", langCode)
                };

                if (!string.IsNullOrEmpty(companyCode))
                {
                    parameters.Add(new SqlParameter("@CompanyCode", companyCode));
                }

                var offices = new List<OfficeModel>();
                
                using (var reader = await _context.ExecuteReaderAsync(query, parameters.ToArray()))
                {
                    while (await reader.ReadAsync())
                    {
                        offices.Add(new OfficeModel
                        {
                            OfficeCode = reader["OfficeCode"].ToString(),
                            OfficeDescription = reader["OfficeDescription"].ToString(),
                            CompanyCode = reader["CompanyCode"].ToString(),
                            IsExecutiveOffice = Convert.ToBoolean(reader["IsExecutiveOffice"]),
                            IsBlocked = Convert.ToBoolean(reader["IsBlocked"])
                        });
                    }
                }
                
                return Ok(new ApiResponse<List<OfficeModel>>
                {
                    Success = true,
                    Message = "Ofis listesi başarıyla getirildi",
                    Data = offices
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ofis listesi getirilirken hata oluştu");
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "Ofis listesi getirilirken bir hata oluştu",
                    Error = ex.Message
                });
            }
        }

        /// <summary>
        /// Ofis adres bilgilerini getirir
        /// </summary>
        /// <param name="officeCode">Ofis kodu</param>
        /// <param name="langCode">Dil kodu (varsayılan: TR)</param>
        /// <returns>Ofis adres bilgileri</returns>
        [HttpGet("{officeCode}/address")]
        public async Task<ActionResult<ApiResponse<OfficeAddressModel>>> GetOfficeAddress(
            string officeCode,
            [FromQuery] string langCode = "TR")
        {
            try
            {
                var query = @"
                    SELECT 
                        OfficeCode,
                        Address,
                        SiteName,
                        BuildingName,
                        BuildingNum,
                        FloorNum,
                        DoorNum,
                        QuarterName,
                        Boulevard,
                        Street,
                        Road,
                        CountryCode,
                        CountryDescription,
                        StateCode,
                        StateDescription,
                        CityCode,
                        CityDescription,
                        DistrictCode,
                        DistrictDescription,
                        ZipCode,
                        DrivingDirections,
                        LangCode
                    FROM dbo.OfficePostalAddress(@LangCode)
                    WHERE OfficeCode = @OfficeCode";

                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@LangCode", langCode),
                    new SqlParameter("@OfficeCode", officeCode)
                };

                using (var reader = await _context.ExecuteReaderAsync(query, parameters.ToArray()))
                {
                    if (await reader.ReadAsync())
                    {
                        var officeAddress = new OfficeAddressModel
                        {
                            OfficeCode = reader["OfficeCode"].ToString(),
                            Address = reader["Address"].ToString(),
                            SiteName = reader["SiteName"].ToString(),
                            BuildingName = reader["BuildingName"].ToString(),
                            BuildingNum = reader["BuildingNum"].ToString(),
                            FloorNum = reader["FloorNum"].ToString(),
                            DoorNum = reader["DoorNum"].ToString(),
                            QuarterName = reader["QuarterName"].ToString(),
                            Boulevard = reader["Boulevard"].ToString(),
                            Street = reader["Street"].ToString(),
                            Road = reader["Road"].ToString(),
                            CountryCode = reader["CountryCode"].ToString(),
                            CountryDescription = reader["CountryDescription"].ToString(),
                            StateCode = reader["StateCode"].ToString(),
                            StateDescription = reader["StateDescription"].ToString(),
                            CityCode = reader["CityCode"].ToString(),
                            CityDescription = reader["CityDescription"].ToString(),
                            DistrictCode = reader["DistrictCode"].ToString(),
                            DistrictDescription = reader["DistrictDescription"].ToString(),
                            ZipCode = reader["ZipCode"].ToString(),
                            DrivingDirections = reader["DrivingDirections"].ToString()
                        };
                        
                        return Ok(new ApiResponse<OfficeAddressModel>
                        {
                            Success = true,
                            Message = "Ofis adres bilgileri başarıyla getirildi",
                            Data = officeAddress
                        });
                    }
                    else
                    {
                        return NotFound(new ApiResponse<object>
                        {
                            Success = false,
                            Message = $"Ofis bulunamadı: {officeCode}",
                            Data = null
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ofis adres bilgileri getirilirken hata oluştu. Ofis Kodu: {OfficeCode}", officeCode);
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "Ofis adres bilgileri getirilirken bir hata oluştu",
                    Error = ex.Message
                });
            }
        }
    }
}
