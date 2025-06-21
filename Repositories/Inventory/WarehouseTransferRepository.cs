using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using ErpMobile.Api.Data;
using ErpMobile.Api.Models.Common;
using ErpMobile.Api.Models.Inventory;

namespace ErpMobile.Api.Repositories.Inventory
{
    /// <summary>
    /// Depolar arası sevk işlemleri için repository implementasyonu
    /// </summary>
    public class WarehouseTransferRepository : IWarehouseTransferRepository
    {
        private readonly ErpDbContext _context;
        private readonly ILogger<WarehouseTransferRepository> _logger;

        public WarehouseTransferRepository(
            ErpDbContext context,
            ILogger<WarehouseTransferRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<List<WarehouseTransferResponse>> GetWarehouseTransfersAsync(
            string sourceWarehouseCode = null, 
            string targetWarehouseCode = null, 
            DateTime? startDate = null, 
            DateTime? endDate = null)
        {
            try
            {
                var whereConditions = new List<string>();
                var parameters = new List<SqlParameter>();

                if (!string.IsNullOrEmpty(sourceWarehouseCode))
                {
                    whereConditions.Add("h.WarehouseCode = @SourceWarehouseCode");
                    parameters.Add(new SqlParameter("@SourceWarehouseCode", sourceWarehouseCode));
                }

                if (!string.IsNullOrEmpty(targetWarehouseCode))
                {
                    whereConditions.Add("h.ToWarehouseCode = @TargetWarehouseCode");
                    parameters.Add(new SqlParameter("@TargetWarehouseCode", targetWarehouseCode));
                }

                if (startDate.HasValue)
                {
                    whereConditions.Add("h.OperationDate >= @StartDate");
                    parameters.Add(new SqlParameter("@StartDate", startDate.Value));
                }

                if (endDate.HasValue)
                {
                    whereConditions.Add("h.OperationDate <= @EndDate");
                    parameters.Add(new SqlParameter("@EndDate", endDate.Value));
                }

                // Sadece WT (Warehouse Transfer) işlem kodlu kayıtları getir
                whereConditions.Add("h.InnerProcessCode = 'WT'");

                var whereClause = whereConditions.Count > 0 
                    ? "WHERE " + string.Join(" AND ", whereConditions) 
                    : string.Empty;

                var query = $@"
                    SELECT 
                        TransferNumber = h.InnerNumber,
                        h.OperationDate,
                        h.OperationTime,
                        h.Series,
                        h.SeriesNumber,
                        h.InnerProcessCode,
                        h.IsReturn,
                        h.CompanyCode,
                        h.OfficeCode,
                        h.ToOfficeCode,
                        h.StoreCode,
                        SourceWarehouseCode = h.WarehouseCode,
                        SourceWarehouseName = sw.WarehouseName,
                        TargetWarehouseCode = h.ToWarehouseCode,
                        TargetWarehouseName = tw.WarehouseName,
                        h.ToStoreCode,
                        h.CurrAccTypeCode,
                        VendorCode = CASE h.CurrAccTypeCode WHEN 1 THEN h.CurrAccCode ELSE '' END,
                        CustomerCode = CASE h.CurrAccTypeCode WHEN 3 THEN h.CurrAccCode ELSE '' END,
                        RetailCustomerCode = CASE h.CurrAccTypeCode WHEN 4 THEN h.CurrAccCode ELSE '' END,
                        EmployeeCode = CASE h.CurrAccTypeCode WHEN 8 THEN h.CurrAccCode ELSE '' END,
                        TotalQty = ISNULL(InnerLines.Qty1, 0),
                        h.Description,
                        h.ImportFileNumber,
                        h.IsCompleted,
                        h.IsLocked,
                        h.IsTransferApproved,
                        h.IsInnerOrderBase,
                        h.IsSectionTransfer,
                        h.ApplicationCode,
                        ApplicationDescription = ISNULL(app.ApplicationDescription, ''),
                        h.ApplicationID,
                        h.InnerHeaderID,
                        ShipmentMethodCode = ISNULL(ext.ShipmentMethodCode, ''),
                        ShipmentMethodName = ISNULL(sm.ShipmentMethodName, ''),
                        h.TransferApprovedDate,
                        h.CreatedUserName,
                        h.CreatedDate
                    FROM trInnerHeader h WITH (NOLOCK)
                    LEFT JOIN tpInnerHeaderExtension ext WITH (NOLOCK) ON h.InnerHeaderID = ext.InnerHeaderID
                    LEFT JOIN cdWarehouse sw WITH (NOLOCK) ON h.WarehouseCode = sw.WarehouseCode
                    LEFT JOIN cdWarehouse tw WITH (NOLOCK) ON h.ToWarehouseCode = tw.WarehouseCode
                    LEFT JOIN cdShipmentMethod sm WITH (NOLOCK) ON ext.ShipmentMethodCode = sm.ShipmentMethodCode
                    LEFT JOIN bsApplicationDesc app WITH (NOLOCK) ON app.ApplicationCode = h.ApplicationCode AND app.LangCode = 'TR'
                    INNER JOIN (SELECT InnerHeaderID, Qty1 = SUM(Qty1) 
                               FROM trInnerLine WITH (NOLOCK)
                               GROUP BY InnerHeaderID) AS InnerLines ON InnerLines.InnerHeaderID = h.InnerHeaderID
                    {whereClause}
                    ORDER BY h.OperationDate DESC, h.OperationTime DESC
                ";

                var transfers = new List<WarehouseTransferResponse>();
                
                using (var reader = await _context.ExecuteReaderAsync(query, parameters.ToArray()))
                {
                    while (await reader.ReadAsync())
                    {
                        transfers.Add(new WarehouseTransferResponse
                        {
                            TransferNumber = reader["TransferNumber"].ToString(),
                            OperationDate = Convert.ToDateTime(reader["OperationDate"]),
                            OperationTime = reader["OperationTime"] != DBNull.Value ? (TimeSpan)reader["OperationTime"] : TimeSpan.Zero,
                            Series = reader["Series"].ToString(),
                            SeriesNumber = reader["SeriesNumber"].ToString(),
                            InnerProcessCode = reader["InnerProcessCode"].ToString(),
                            IsReturn = Convert.ToBoolean(reader["IsReturn"]),
                            CompanyCode = reader["CompanyCode"].ToString(),
                            OfficeCode = reader["OfficeCode"].ToString(),
                            ToOfficeCode = reader["ToOfficeCode"].ToString(),
                            StoreCode = reader["StoreCode"].ToString(),
                            SourceWarehouseCode = reader["SourceWarehouseCode"].ToString(),
                            SourceWarehouseName = reader["SourceWarehouseName"].ToString(),
                            TargetWarehouseCode = reader["TargetWarehouseCode"].ToString(),
                            TargetWarehouseName = reader["TargetWarehouseName"].ToString(),
                            ToStoreCode = reader["ToStoreCode"].ToString(),
                            CurrAccTypeCode = reader["CurrAccTypeCode"].ToString(),
                            VendorCode = reader["VendorCode"].ToString(),
                            CustomerCode = reader["CustomerCode"].ToString(),
                            RetailCustomerCode = reader["RetailCustomerCode"].ToString(),
                            EmployeeCode = reader["EmployeeCode"].ToString(),
                            TotalQty = Convert.ToDouble(reader["TotalQty"]),
                            Description = reader["Description"].ToString(),
                            ImportFileNumber = reader["ImportFileNumber"].ToString(),
                            IsCompleted = Convert.ToBoolean(reader["IsCompleted"]),
                            IsLocked = Convert.ToBoolean(reader["IsLocked"]),
                            IsApproved = Convert.ToBoolean(reader["IsTransferApproved"]),
                            IsInnerOrderBase = Convert.ToBoolean(reader["IsInnerOrderBase"]),
                            IsSectionTransfer = Convert.ToBoolean(reader["IsSectionTransfer"]),
                            ApplicationCode = reader["ApplicationCode"].ToString(),
                            ApplicationDescription = reader["ApplicationDescription"].ToString(),
                            ApplicationID = reader["ApplicationID"] != DBNull.Value ? (Guid?)reader["ApplicationID"] : null,
                            InnerHeaderID = (Guid)reader["InnerHeaderID"],
                            ShipmentMethodCode = reader["ShipmentMethodCode"].ToString(),
                            ShipmentMethodName = reader["ShipmentMethodName"].ToString(),
                            ApprovalDate = reader["TransferApprovedDate"] != DBNull.Value ? Convert.ToDateTime(reader["TransferApprovedDate"]) : (DateTime?)null,
                            CreatedUserName = reader["CreatedUserName"].ToString(),
                            CreatedDate = Convert.ToDateTime(reader["CreatedDate"])
                        });
                    }
                }
                
                return transfers;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Depolar arası sevk listesi getirilirken hata oluştu");
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<WarehouseTransferDetailResponse> GetWarehouseTransferByNumberAsync(string transferNumber)
        {
            try
            {
                // Önce başlık bilgilerini getir
                var headerQuery = @"
                    SELECT 
                        TransferNumber = h.InnerNumber,
                        SourceWarehouseCode = h.WarehouseCode,
                        SourceWarehouseName = sw.WarehouseName,
                        TargetWarehouseCode = h.ToWarehouseCode,
                        TargetWarehouseName = tw.WarehouseName,
                        h.OperationDate,
                        h.OperationTime,
                        h.Description,
                        ShipmentMethodCode = ISNULL(ext.ShipmentMethodCode, ''),
                        ShipmentMethodName = ISNULL(sm.ShipmentMethodName, ''),
                        h.IsTransferApproved,
                        h.TransferApprovedDate,
                        h.CreatedUserName,
                        h.CreatedDate,
                        h.InnerHeaderID
                    FROM trInnerHeader h WITH (NOLOCK)
                    LEFT JOIN tpInnerHeaderExtension ext WITH (NOLOCK) ON h.InnerHeaderID = ext.InnerHeaderID
                    LEFT JOIN cdWarehouse sw WITH (NOLOCK) ON h.WarehouseCode = sw.WarehouseCode
                    LEFT JOIN cdWarehouse tw WITH (NOLOCK) ON h.ToWarehouseCode = tw.WarehouseCode
                    LEFT JOIN cdShipmentMethod sm WITH (NOLOCK) ON ext.ShipmentMethodCode = sm.ShipmentMethodCode
                    WHERE h.InnerNumber = @TransferNumber AND h.InnerProcessCode = 'WT'
                ";

                var parameters = new[] { new SqlParameter("@TransferNumber", transferNumber) };
                WarehouseTransferDetailResponse transfer = null;
                Guid innerHeaderId = Guid.Empty;

                using (var reader = await _context.ExecuteReaderAsync(headerQuery, parameters))
                {
                    if (await reader.ReadAsync())
                    {
                        transfer = new WarehouseTransferDetailResponse
                        {
                            TransferNumber = reader["TransferNumber"].ToString(),
                            SourceWarehouseCode = reader["SourceWarehouseCode"].ToString(),
                            SourceWarehouseName = reader["SourceWarehouseName"].ToString(),
                            TargetWarehouseCode = reader["TargetWarehouseCode"].ToString(),
                            TargetWarehouseName = reader["TargetWarehouseName"].ToString(),
                            OperationDate = Convert.ToDateTime(reader["OperationDate"]),
                            OperationTime = reader["OperationTime"] != DBNull.Value ? (TimeSpan)reader["OperationTime"] : TimeSpan.Zero,
                            Description = reader["Description"].ToString(),
                            ShipmentMethodCode = reader["ShipmentMethodCode"].ToString(),
                            ShipmentMethodName = reader["ShipmentMethodName"].ToString(),
                            IsApproved = Convert.ToBoolean(reader["IsTransferApproved"]),
                            ApprovalDate = reader["TransferApprovedDate"] != DBNull.Value ? Convert.ToDateTime(reader["TransferApprovedDate"]) : (DateTime?)null,
                            CreatedUserName = reader["CreatedUserName"].ToString(),
                            CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                            Items = new List<WarehouseTransferItemResponse>()
                        };

                        innerHeaderId = (Guid)reader["InnerHeaderID"];
                    }
                    else
                    {
                        return null; // Sevk bulunamadı
                    }
                }

                // Şimdi satır detaylarını getir
                var linesQuery = @"
                    SELECT 
                        l.ItemCode,
                        ItemName = ISNULL(i.ItemName, ''),
                        l.ColorCode,
                        ColorName = ISNULL(c.ColorName, ''),
                        l.ItemDim1Code,
                        ItemDim1Name = ISNULL(d1.ItemDim1Name, ''),
                        Quantity = l.Qty1,
                        l.LineDescription,
                        Barcode = l.UsedBarcode
                    FROM trInnerLine l WITH (NOLOCK)
                    LEFT JOIN cdItem i WITH (NOLOCK) ON l.ItemCode = i.ItemCode
                    LEFT JOIN cdColor c WITH (NOLOCK) ON l.ColorCode = c.ColorCode
                    LEFT JOIN cdItemDim1 d1 WITH (NOLOCK) ON l.ItemDim1Code = d1.ItemDim1Code
                    WHERE l.InnerHeaderID = @InnerHeaderID
                    ORDER BY l.SortOrder
                ";

                var lineParameters = new[] { new SqlParameter("@InnerHeaderID", innerHeaderId) };

                using (var reader = await _context.ExecuteReaderAsync(linesQuery, lineParameters))
                {
                    while (await reader.ReadAsync())
                    {
                        transfer.Items.Add(new WarehouseTransferItemResponse
                        {
                            ItemCode = reader["ItemCode"].ToString(),
                            ItemName = reader["ItemName"].ToString(),
                            ColorCode = reader["ColorCode"].ToString(),
                            ColorName = reader["ColorName"].ToString(),
                            ItemDim1Code = reader["ItemDim1Code"].ToString(),
                            ItemDim1Name = reader["ItemDim1Name"].ToString(),
                            Quantity = Convert.ToDouble(reader["Quantity"]),
                            LineDescription = reader["LineDescription"].ToString(),
                            Barcode = reader["Barcode"].ToString()
                        });
                    }
                }

                return transfer;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Depolar arası sevk detayları getirilirken hata oluştu. Fiş No: {TransferNumber}", transferNumber);
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<string> GenerateTransferNumberAsync()
        {
            try
            {
                // sp_LastRefNumInnerProcess stored procedure'ünü kullanarak son referans numarasını al
                var query = "EXEC sp_LastRefNumInnerProcess @CompanyCode=1, @InnerProcessCode=N'WT'";
                
                int lastSequence = 0;
                using (var reader = await _context.ExecuteReaderAsync(query))
                {
                    if (await reader.ReadAsync())
                    {
                        // Stored procedure'den gelen son referans numarası
                        if (reader["LastRefNo"] != DBNull.Value)
                        {
                            lastSequence = Convert.ToInt32(reader["LastRefNo"]);
                        }
                    }
                }
                
                // Yeni sıra numarası
                int sequence = lastSequence + 1;
                
                // Yeni fiş numarası formatı: Ay-WT-Sıra
                var today = DateTime.Today;
                return $"{today.Month}-WT-{sequence}";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sevk fiş numarası oluşturulurken hata oluştu");
                throw;
            }
        }
        
        /// <inheritdoc/>
        public async Task<List<WarehouseResponse>> GetWarehousesAsync()
        {
            try
            {
                var query = @"
                    SELECT * FROM (
                    SELECT cdWarehouse.WarehouseCode
                        , WarehouseDescription = ISNULL((SELECT WarehouseDescription FROM cdWarehouseDesc WITH(NOLOCK) 
                                                    WHERE cdWarehouseDesc.WarehouseCode = cdWarehouse.WarehouseCode 
                                                    AND cdWarehouseDesc.LangCode = N'TR'), '')
                        , cdOffice.CompanyCode
                        , cdWarehouse.OfficeCode
                        , OfficeDescription = ISNULL((SELECT OfficeDescription FROM cdOfficeDesc WITH(NOLOCK) 
                                                WHERE cdOfficeDesc.OfficeCode = cdOffice.OfficeCode 
                                                AND cdOfficeDesc.LangCode = N'TR'), '')
                        , cdWarehouse.WarehouseOwnerCode
                        , WarehouseOwnerDescription = ISNULL((SELECT WarehouseOwnerDescription FROM bsWarehouseOwnerDesc WITH(NOLOCK) 
                                                        WHERE bsWarehouseOwnerDesc.WarehouseOwnerCode = cdWarehouse.WarehouseOwnerCode 
                                                        AND bsWarehouseOwnerDesc.LangCode = N'TR'), '')
                        , cdWarehouse.WarehouseTypeCode
                        , WarehouseTypeDescription = ISNULL((SELECT WarehouseTypeDescription FROM cdWarehouseTypeDesc WITH(NOLOCK) 
                                                        WHERE cdWarehouseTypeDesc.WarehouseTypeCode = cdWarehouse.WarehouseTypeCode 
                                                        AND cdWarehouseTypeDesc.LangCode = N'TR'), '')
                        , CurrAccTypeCode = cdWarehouse.CurrAccTypeCode
                        , StoreCode = CASE cdWarehouse.WarehouseOwnerCode WHEN 2 THEN cdWarehouse.CurrAccCode ELSE '' END
                        , StoreDescription = CASE cdWarehouse.WarehouseOwnerCode WHEN 2 THEN ISNULL(CurrAccDescription, '') ELSE '' END
                        , CityDescription = ISNULL((SELECT CityDescription FROM cdCityDesc WITH(NOLOCK) 
                                                WHERE cdCityDesc.CityCode = prWarehousePostalAddress.CityCode 
                                                AND cdCityDesc.LangCode = N'TR'), '')
                        , Address = ISNULL(prWarehousePostalAddress.Address, '')
                        , ZipCode = ISNULL(prWarehousePostalAddress.ZipCode, '')
                        , CustomerCode = CASE cdWarehouse.WarehouseOwnerCode WHEN 4 THEN cdWarehouse.CurrAccCode ELSE '' END
                        , CustomerDescription = CASE cdWarehouse.WarehouseOwnerCode WHEN 4 THEN ISNULL(CurrAccDescription, '') ELSE '' END
                        , CustomerTypeCode = cdCurrAcc.CustomerTypeCode
                        , SubCurrAccID = cdWarehouse.SubCurrAccID
                        , SubCurrAccCompanyName = ISNULL(prSubCurrAcc.CompanyName, '')
                        , VendorCode = CASE cdWarehouse.WarehouseOwnerCode WHEN 3 THEN cdWarehouse.CurrAccCode ELSE '' END
                        , VendorDescription = CASE cdWarehouse.WarehouseOwnerCode WHEN 3 THEN ISNULL(CurrAccDescription, '') ELSE '' END
                        , cdWarehouse.TotalArea
                        , cdWarehouse.PermitNegativeStock
                        , cdWarehouse.ControlStockLevel
                        , cdWarehouse.PermitRetailSubsequentDelivery
                        , cdWarehouse.IsDefault
                        , cdWarehouse.UseSection
                        , cdWarehouse.IsBlocked
                    FROM cdWarehouse WITH(NOLOCK)
                        INNER JOIN cdOffice WITH(NOLOCK) ON cdWarehouse.OfficeCode = cdOffice.OfficeCode
                        LEFT OUTER JOIN cdCurrAccDesc WITH(NOLOCK) ON cdWarehouse.CurrAccTypeCode = cdCurrAccDesc.CurrAccTypeCode 
                                                                AND cdCurrAccDesc.CurrAccCode = cdWarehouse.CurrAccCode 
                                                                AND cdCurrAccDesc.LangCode = N'TR'
                        INNER JOIN cdCurrAcc WITH(NOLOCK) ON cdWarehouse.CurrAccTypeCode = cdCurrAcc.CurrAccTypeCode 
                                                        AND cdCurrAcc.CurrAccCode = cdWarehouse.CurrAccCode
                        LEFT OUTER JOIN prSubCurrAcc WITH(NOLOCK) ON cdWarehouse.SubCurrAccID = prSubCurrAcc.SubCurrAccID
                        LEFT OUTER JOIN prWarehousePostalAddress WITH(NOLOCK) ON cdWarehouse.WarehouseCode = prWarehousePostalAddress.WarehouseCode
                    WHERE cdWarehouse.WarehouseCode <> '') AS QueryTable 
                    WHERE QueryTable.CurrAccTypeCode = 0
                    ORDER BY QueryTable.WarehouseCode
                ";
                
                var warehouses = new List<WarehouseResponse>();
                
                using (var reader = await _context.ExecuteReaderAsync(query))
                {
                    while (await reader.ReadAsync())
                    {
                        warehouses.Add(new WarehouseResponse
                        {
                            WarehouseCode = reader["WarehouseCode"].ToString(),
                            WarehouseDescription = reader["WarehouseDescription"].ToString(),
                            CompanyCode = reader["CompanyCode"].ToString(),
                            OfficeCode = reader["OfficeCode"].ToString(),
                            OfficeDescription = reader["OfficeDescription"].ToString(),
                            WarehouseOwnerCode = reader["WarehouseOwnerCode"].ToString(),
                            WarehouseOwnerDescription = reader["WarehouseOwnerDescription"].ToString(),
                            WarehouseTypeCode = reader["WarehouseTypeCode"].ToString(),
                            WarehouseTypeDescription = reader["WarehouseTypeDescription"].ToString(),
                            CurrAccTypeCode = reader["CurrAccTypeCode"].ToString(),
                            StoreCode = reader["StoreCode"].ToString(),
                            StoreDescription = reader["StoreDescription"].ToString(),
                            CityDescription = reader["CityDescription"].ToString(),
                            Address = reader["Address"].ToString(),
                            ZipCode = reader["ZipCode"].ToString(),
                            CustomerCode = reader["CustomerCode"].ToString(),
                            CustomerDescription = reader["CustomerDescription"].ToString(),
                            CustomerTypeCode = reader["CustomerTypeCode"].ToString(),
                            SubCurrAccID = reader["SubCurrAccID"] != DBNull.Value ? (Guid?)reader["SubCurrAccID"] : null,
                            SubCurrAccCompanyName = reader["SubCurrAccCompanyName"].ToString(),
                            VendorCode = reader["VendorCode"].ToString(),
                            VendorDescription = reader["VendorDescription"].ToString(),
                            TotalArea = reader["TotalArea"] != DBNull.Value ? Convert.ToDecimal(reader["TotalArea"]) : (decimal?)null,
                            PermitNegativeStock = Convert.ToBoolean(reader["PermitNegativeStock"]),
                            ControlStockLevel = Convert.ToBoolean(reader["ControlStockLevel"]),
                            PermitRetailSubsequentDelivery = Convert.ToBoolean(reader["PermitRetailSubsequentDelivery"]),
                            IsDefault = Convert.ToBoolean(reader["IsDefault"]),
                            UseSection = Convert.ToBoolean(reader["UseSection"]),
                            IsBlocked = Convert.ToBoolean(reader["IsBlocked"])
                        });
                    }
                }
                
                return warehouses;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Depo listesi getirilirken hata oluştu");
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<string> CreateWarehouseTransferAsync(WarehouseTransferRequest request, string userName)
        {
            try
            {
                // Yeni fiş numarası oluştur
                string transferNumber = await GenerateTransferNumberAsync();
                
                // InnerHeader için ID oluştur
                Guid innerHeaderId = Guid.NewGuid();
                
                // İşlem tarihini ve saatini al
                DateTime now = DateTime.Now;
                var minDate = new DateTime(1900, 1, 1);
                
                // trInnerHeader tablosuna kayıt ekle
                var headerQuery = @"
                    INSERT INTO trInnerHeader (
                        InnerHeaderID, TransTypeCode, InnerProcessCode,
                        InnerNumber, OperationDate, OperationTime,
                        Description, StoreTypeCode, StoreCode,
                        ToStoreCode, WarehouseCode, ToWarehouseCode,
                        ImportFileNumber, ExportFileNumber, ToOfficeCode,
                        CurrAccTypeCode, CurrAccCode, SubCurrAccID,
                        Series, SeriesNumber, IsTransferApproved,
                        TransferApprovedDate, IsPostingJournal, JournalDate,
                        RoundsmanCode, ServicemanCode, IsReturn,
                        CustomsDocumentNumber, IsInnerOrderBase, IsSectionTransfer,
                        OfficeCode, ApplicationCode, ApplicationID,
                        IsCompleted, IsPrinted, IsLocked,
                        CompanyCode, CreatedUserName, CreatedDate,
                        LastUpdatedUserName, LastUpdatedDate
                    ) VALUES (
                        @InnerHeaderID, 3, 'WT',
                        @InnerNumber, @OperationDate, @OperationTime,
                        @Description, 5, '',
                        '', @SourceWarehouseCode, @TargetWarehouseCode,
                        '', '', 'M',
                        3, '', NULL,
                        '', 0, 0,
                        @MinDate, 0, @MinDate,
                        '', '', 0,
                        '', 0, 0,
                        'M', 'Inner', @InnerHeaderID,
                        0, 0, 0,
                        1, @UserName, GETDATE(),
                        @UserName, GETDATE()
                    )
                ";
                
                // minDate değişkeni yukarıda tanımlandı
                var headerParameters = new[]
                {
                    new SqlParameter("@InnerHeaderID", innerHeaderId),
                    new SqlParameter("@InnerNumber", transferNumber),
                    new SqlParameter("@SourceWarehouseCode", request.SourceWarehouseCode),
                    new SqlParameter("@TargetWarehouseCode", request.TargetWarehouseCode),
                    new SqlParameter("@OperationDate", request.OperationDate),
                    new SqlParameter("@OperationTime", request.OperationDate.HasValue ? request.OperationDate.Value.TimeOfDay : (object)DBNull.Value),
                    new SqlParameter("@Description", request.Description ?? string.Empty),
                    new SqlParameter("@MinDate", minDate),
                    new SqlParameter("@UserName", userName)
                };
                
                await _context.ExecuteNonQueryAsync(headerQuery, headerParameters);
                
                // tpInnerHeaderExtension tablosuna kayıt ekle
                if (!string.IsNullOrEmpty(request.ShipmentMethodCode))
                {
                    var extensionQuery = @"
                        INSERT INTO tpInnerHeaderExtension (
                            InnerHeaderID, ShipmentMethodCode, RoundsmanCode,
                            CompanyCode, DeliveryCompanyCode, LogisticsCompanyBOL,
                            ShipmentTypeCode, EShipmentNumber, MainEShipmentNumber,
                            MainInnerHeaderID, EShipmentStatusCode, CreatedUserName,
                            CreatedDate, LastUpdatedUserName, LastUpdatedDate
                        ) VALUES (
                            @InnerHeaderID, @ShipmentMethodCode, '',
                            1, '', '',
                            0, '', '',
                            NULL, 0, @UserName,
                            GETDATE(), @UserName, GETDATE()
                        )
                    ";
                    
                    var extensionParameters = new[]
                    {
                        new SqlParameter("@InnerHeaderID", innerHeaderId),
                        new SqlParameter("@ShipmentMethodCode", request.ShipmentMethodCode),
                        new SqlParameter("@UserName", userName)
                    };
                    
                    await _context.ExecuteNonQueryAsync(extensionQuery, extensionParameters);
                }
                
                // Ürün satırlarını ekle
                int sortOrder = 1;
                foreach (var item in request.Items)
                {
                    // Her satır için benzersiz ID oluştur
                    Guid innerLineId = Guid.NewGuid();
                    
                    // trInnerLine tablosuna kayıt ekle
                    var lineQuery = @"
                        INSERT INTO trInnerLine (
                            InnerLineID, InnerHeaderID, SortOrder,
                            SerialNumber, ItemTypeCode, ItemCode,
                            ColorCode, ItemDim1Code, ItemDim2Code,
                            ItemDim3Code, Qty1, Qty2,
                            LineDescription, UsedBarcode, CurrencyCode,
                            CostPrice, CostAmount, SupportRequestHeaderID,
                            InnerLineSumID, InnerLineSerialSumID, InnerOrderLineID,
                            BatchCode, ScrapReasonCode, CostCenterCode,
                            GLTypeCode, SectionCode, ManufactureDate,
                            ExpiryDate, CreatedUserName, CreatedDate,
                            LastUpdatedUserName, LastUpdatedDate
                        ) VALUES (
                            @InnerLineID, @InnerHeaderID, @SortOrder,
                            @SerialNumber, @ItemTypeCode, @ItemCode,
                            @ColorCode, @ItemDim1Code, @ItemDim2Code,
                            @ItemDim3Code, @Quantity, @Qty2,
                            @LineDescription, @Barcode, @CurrencyCode,
                            @CostPrice, @CostAmount, NULL,
                            @InnerLineSumID, @InnerLineSerialSumID, NULL,
                            @BatchCode, @ScrapReasonCode, @CostCenterCode,
                            @GLTypeCode, NULL, @ManufactureDate,
                            @ExpiryDate, @UserName, GETDATE(),
                            @UserName, GETDATE()
                        )
                    ";
                    
                    // minDate değişkeni yukarıda tanımlandı
                    var lineParameters = new[]
                    {
                        new SqlParameter("@InnerLineID", innerLineId),
                        new SqlParameter("@InnerHeaderID", innerHeaderId),
                        new SqlParameter("@SortOrder", sortOrder++),
                        new SqlParameter("@SerialNumber", string.Empty),
                        new SqlParameter("@ItemTypeCode", 1), // Varsayılan ürün tipi
                        new SqlParameter("@ItemCode", item.ItemCode),
                        new SqlParameter("@ColorCode", item.ColorCode ?? string.Empty),
                        new SqlParameter("@ItemDim1Code", item.ItemDim1Code ?? string.Empty),
                        new SqlParameter("@ItemDim2Code", string.Empty),
                        new SqlParameter("@ItemDim3Code", string.Empty),
                        new SqlParameter("@Quantity", item.Quantity),
                        new SqlParameter("@Qty2", 0),
                        new SqlParameter("@LineDescription", item.LineDescription ?? string.Empty),
                        new SqlParameter("@Barcode", item.Barcode ?? string.Empty),
                        new SqlParameter("@CurrencyCode", "TRY"), // Varsayılan para birimi
                        new SqlParameter("@CostPrice", 0),
                        new SqlParameter("@CostAmount", 0),
                        new SqlParameter("@InnerLineSumID", 0),
                        new SqlParameter("@InnerLineSerialSumID", 0),
                        new SqlParameter("@BatchCode", string.Empty),
                        new SqlParameter("@ScrapReasonCode", string.Empty),
                        new SqlParameter("@CostCenterCode", string.Empty),
                        new SqlParameter("@GLTypeCode", string.Empty),
                        new SqlParameter("@ManufactureDate", minDate),
                        new SqlParameter("@ExpiryDate", minDate),
                        new SqlParameter("@UserName", userName)
                    };
                    
                    await _context.ExecuteNonQueryAsync(lineQuery, lineParameters);
                }
                
                return transferNumber;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Depolar arası sevk oluşturulurken hata oluştu");
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<bool> ApproveWarehouseTransferAsync(string transferNumber, string userName)
        {
            try
            {
                // Önce sevk kaydını kontrol et
                var checkQuery = @"
                    SELECT h.InnerHeaderID, h.IsTransferApproved, h.SourceWarehouseCode, h.TargetWarehouseCode
                    FROM trInnerHeader h WITH (NOLOCK)
                    WHERE h.InnerNumber = @TransferNumber 
                      AND h.InnerProcessCode = 'WT'
                ";
                
                var checkParameters = new[] { new SqlParameter("@TransferNumber", transferNumber) };
                Guid? innerHeaderId = null;
                bool isAlreadyApproved = false;
                string sourceWarehouseCode = null;
                string targetWarehouseCode = null;
                
                using (var reader = await _context.ExecuteReaderAsync(checkQuery, checkParameters))
                {
                    if (await reader.ReadAsync())
                    {
                        innerHeaderId = (Guid)reader["InnerHeaderID"];
                        isAlreadyApproved = Convert.ToBoolean(reader["IsTransferApproved"]);
                        sourceWarehouseCode = reader["SourceWarehouseCode"].ToString();
                        targetWarehouseCode = reader["TargetWarehouseCode"].ToString();
                    }
                }
                
                if (innerHeaderId == null)
                {
                    _logger.LogWarning("Onaylanacak sevk kaydı bulunamadı. Fiş No: {TransferNumber}", transferNumber);
                    return false;
                }
                
                if (isAlreadyApproved)
                {
                    _logger.LogWarning("Sevk kaydı zaten onaylanmış. Fiş No: {TransferNumber}", transferNumber);
                    return true; // Zaten onaylanmış, başarılı kabul et
                }
                
                // Sevk kalemlerini al
                var linesQuery = @"
                    SELECT ItemCode, ColorCode, ItemDim1Code, Qty1 as Quantity
                    FROM trInnerLine WITH (NOLOCK)
                    WHERE InnerHeaderID = @InnerHeaderID
                ";
                
                var linesParameters = new[] { new SqlParameter("@InnerHeaderID", innerHeaderId) };
                var items = new List<(string ItemCode, string ColorCode, string ItemDim1Code, double Quantity)>();
                
                using (var reader = await _context.ExecuteReaderAsync(linesQuery, linesParameters))
                {
                    while (await reader.ReadAsync())
                    {
                        items.Add((
                            reader["ItemCode"].ToString(),
                            reader["ColorCode"].ToString(),
                            reader["ItemDim1Code"].ToString(),
                            Convert.ToDouble(reader["Quantity"])
                        ));
                    }
                }
                
                // Kaynak depodaki stok bakiyelerini kontrol et
                foreach (var item in items)
                {
                    // qry_GetItemBalance stored procedure ile stok bakiyesini kontrol et
                    var stockQuery = @"
                        DECLARE @Balance decimal(18,6);
                        
                        EXEC sp_executesql N'qry_GetItemBalance @p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12, @p13, @p14, @p15, @p16, @p17, @p18',
                        N'@p0 nvarchar(2),@p1 decimal(1,0),@p2 nvarchar(1),@p3 nvarchar(4000),@p4 nvarchar(3),@p5 bit,@p6 tinyint,@p7 nvarchar(3),@p8 nvarchar(3),@p9 nvarchar(3),@p10 nvarchar(4000),@p11 nvarchar(4000),@p12 datetime,@p13 bit,@p14 tinyint,@p15 nvarchar(4000),@p16 uniqueidentifier,@p17 tinyint,@p18 bit',
                        @p0=N'WS',@p1=1,@p2=N'M',@p3=@ItemCode,@p4=@SourceWarehouse,@p5=1,@p6=1,@p7=@ColorCode,@p8=@ItemDim1Code,@p9=N'STN',@p10=N'',@p11=N'',@p12=@Date,@p13=1,@p14=0,@p15=N'',@p16='00000000-0000-0000-0000-000000000000',@p17=6,@p18=0;
                        
                        SELECT @Balance as StockBalance;
                    ";
                    
                    var stockParameters = new[]
                    {
                        new SqlParameter("@ItemCode", item.ItemCode),
                        new SqlParameter("@SourceWarehouse", sourceWarehouseCode),
                        new SqlParameter("@ColorCode", string.IsNullOrEmpty(item.ColorCode) ? DBNull.Value : (object)item.ColorCode),
                        new SqlParameter("@ItemDim1Code", string.IsNullOrEmpty(item.ItemDim1Code) ? DBNull.Value : (object)item.ItemDim1Code),
                        new SqlParameter("@Date", DateTime.Today)
                    };
                    
                    double stockBalance = 0;
                    using (var reader = await _context.ExecuteReaderAsync(stockQuery, stockParameters))
                    {
                        if (await reader.ReadAsync() && reader["StockBalance"] != DBNull.Value)
                        {
                            stockBalance = Convert.ToDouble(reader["StockBalance"]);
                        }
                    }
                    
                    // Stok miktarı yetersizse hata fırlat
                    if (stockBalance < item.Quantity)
                    {
                        throw new InvalidOperationException(
                            $"Yetersiz stok: Ürün {item.ItemCode}, Depo {sourceWarehouseCode}, " +
                            $"Mevcut {stockBalance}, Gerekli {item.Quantity}");
                    }
                }
                
                // Şimdi sevk kaydını onayla - kullanıcının paylaştığı kapsamlı güncelleme sorgusu
                var updateQuery = @"
                    UPDATE trInnerHeader 
                    SET 
                    IsTransferApproved = 1,
                    TransferApprovedDate = @ApprovalDate,
                    LastUpdatedUserName = @UserName,
                    LastUpdatedDate = GETDATE(),
                    IsCompleted = 1
                    WHERE InnerHeaderID = @InnerHeaderID
                ";
                
                var now = DateTime.Now;
                var updateParameters = new[]
                {
                    new SqlParameter("@InnerHeaderID", innerHeaderId),
                    new SqlParameter("@ApprovalDate", now),
                    new SqlParameter("@UserName", userName)
                };
                
                await _context.ExecuteNonQueryAsync(updateQuery, updateParameters);
                
                // Stok hareketlerini oluştur
                await CreateStockMovementsAsync(innerHeaderId.Value, userName);
                
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sevk onaylanırken hata oluştu. Fiş No: {TransferNumber}", transferNumber);
                throw;
            }
        }
        
        /// <summary>
        /// Onaylanan sevk için stok hareketlerini oluşturur
        /// </summary>
        private async Task CreateStockMovementsAsync(Guid innerHeaderId, string userName)
        {
            try
            {
                // Sevk başlık bilgilerini al
                var headerQuery = @"
                    SELECT 
                        h.InnerNumber,
                        h.WarehouseCode AS SourceWarehouseCode,
                        h.ToWarehouseCode AS TargetWarehouseCode,
                        h.OperationDate
                    FROM trInnerHeader h WITH (NOLOCK)
                    WHERE h.InnerHeaderID = @InnerHeaderID
                ";
                
                var headerParameters = new[] { new SqlParameter("@InnerHeaderID", innerHeaderId) };
                string transferNumber = null;
                string sourceWarehouseCode = null;
                string targetWarehouseCode = null;
                DateTime operationDate = DateTime.Now;
                
                using (var reader = await _context.ExecuteReaderAsync(headerQuery, headerParameters))
                {
                    if (await reader.ReadAsync())
                    {
                        transferNumber = reader["InnerNumber"].ToString();
                        sourceWarehouseCode = reader["SourceWarehouseCode"].ToString();
                        targetWarehouseCode = reader["TargetWarehouseCode"].ToString();
                        operationDate = Convert.ToDateTime(reader["OperationDate"]);
                    }
                    else
                    {
                        throw new Exception($"Sevk başlık bilgileri bulunamadı. InnerHeaderID: {innerHeaderId}");
                    }
                }
                
                // Sevk satırlarını al
                var linesQuery = @"
                    SELECT 
                        InnerLineID,
                        ItemCode,
                        ColorCode,
                        ItemDim1Code,
                        Qty1 AS Quantity
                    FROM trInnerLine WITH (NOLOCK)
                    WHERE InnerHeaderID = @InnerHeaderID
                    ORDER BY SortOrder
                ";
                
                var linesParameters = new[] { new SqlParameter("@InnerHeaderID", innerHeaderId) };
                var lines = new List<(Guid InnerLineId, string ItemCode, string ColorCode, string ItemDim1Code, double Quantity)>();
                
                using (var reader = await _context.ExecuteReaderAsync(linesQuery, linesParameters))
                {
                    while (await reader.ReadAsync())
                    {
                        lines.Add((
                            (Guid)reader["InnerLineID"],
                            reader["ItemCode"].ToString(),
                            reader["ColorCode"].ToString(),
                            reader["ItemDim1Code"].ToString(),
                            Convert.ToDouble(reader["Quantity"])
                        ));
                    }
                }
                
                // Her satır için stok hareketlerini oluştur
                foreach (var line in lines)
                {
                    // Kaynak depodan çıkış hareketi
                    Guid outStockId = Guid.NewGuid();
                    var outStockQuery = @"
                        INSERT INTO trStock (
                            StockID, CompanyCode, TransTypeCode, ProcessCode, InnerProcessCode,
                            IsReturn, DocumentDate, DocumentTime, OperationDate, OperationTime,
                            DocumentNumber, ItemCode, ItemTypeCode, ColorCode, ItemDim1Code,
                            ItemDim2Code, ItemDim3Code, CurrAccTypeCode, CurrAccCode, SubCurrAccID,
                            OfficeCode, WarehouseCode, In_Qty1, In_Qty2, Out_Qty1, Out_Qty2,
                            FromOfficeCode, FromWarehouseCode, LineDescription, ApplicationCode,
                            ApplicationID, LocalCurrencyCode, DocCurrencyCode, StoreCode,
                            StoreTypeCode, FromStoreCode, FromStoreTypeCode, BatchCode,
                            SectionCode, ManufactureDate, ExpiryDate, CreatedUserName,
                            CreatedDate, LastUpdatedUserName, LastUpdatedDate
                        ) VALUES (
                            @StockID, 1, 2, '', @InnerProcessCode, 0,
                            @DocumentDate, @DocumentTime, @OperationDate, @OperationTime,
                            @DocumentNumber, @ItemCode, 1, @ColorCode, @ItemDim1Code,
                            '', '', 3, '', NULL,
                            @OfficeCode, @WarehouseCode, 0, 0, @Quantity, 0,
                            @OfficeCode, @FromWarehouseCode, '', 'Inner',
                            @ApplicationID, '', '', '',
                            5, '', 5, '',
                            NULL, @MinDate, @MinDate, @UserName,
                            GETDATE(), @UserName, GETDATE()
                        )
                    ";
                    
                    var now = DateTime.Now;
                    var minDate = new DateTime(1900, 1, 1);
                    var outStockParameters = new[]
                    {
                        new SqlParameter("@StockID", outStockId),
                        new SqlParameter("@InnerProcessCode", "WT"),
                        new SqlParameter("@DocumentDate", operationDate.Date),
                        new SqlParameter("@DocumentTime", operationDate.TimeOfDay),
                        new SqlParameter("@OperationDate", operationDate.Date),
                        new SqlParameter("@OperationTime", operationDate.TimeOfDay),
                        new SqlParameter("@DocumentNumber", transferNumber),
                        new SqlParameter("@ItemCode", line.ItemCode),
                        new SqlParameter("@ColorCode", line.ColorCode),
                        new SqlParameter("@ItemDim1Code", line.ItemDim1Code),
                        new SqlParameter("@OfficeCode", "M"), // Varsayılan ofis kodu
                        new SqlParameter("@WarehouseCode", sourceWarehouseCode),
                        new SqlParameter("@Quantity", line.Quantity),
                        new SqlParameter("@FromWarehouseCode", targetWarehouseCode),
                        new SqlParameter("@ApplicationID", innerHeaderId),
                        new SqlParameter("@MinDate", minDate),
                        new SqlParameter("@UserName", userName)
                    };
                    
                    await _context.ExecuteNonQueryAsync(outStockQuery, outStockParameters);
                    
                    // Hedef depoya giriş hareketi
                    Guid inStockId = Guid.NewGuid();
                    var inStockQuery = @"
                        INSERT INTO trStock (
                            StockID, CompanyCode, TransTypeCode, ProcessCode, InnerProcessCode,
                            IsReturn, DocumentDate, DocumentTime, OperationDate, OperationTime,
                            DocumentNumber, ItemCode, ItemTypeCode, ColorCode, ItemDim1Code,
                            ItemDim2Code, ItemDim3Code, CurrAccTypeCode, CurrAccCode, SubCurrAccID,
                            OfficeCode, WarehouseCode, In_Qty1, In_Qty2, Out_Qty1, Out_Qty2,
                            FromOfficeCode, FromWarehouseCode, LineDescription, ApplicationCode,
                            ApplicationID, LocalCurrencyCode, DocCurrencyCode, StoreCode,
                            StoreTypeCode, FromStoreCode, FromStoreTypeCode, BatchCode,
                            SectionCode, ManufactureDate, ExpiryDate, CreatedUserName,
                            CreatedDate, LastUpdatedUserName, LastUpdatedDate
                        ) VALUES (
                            @StockID, 1, 1, '', @InnerProcessCode, 0,
                            @DocumentDate, @DocumentTime, @OperationDate, @OperationTime,
                            @DocumentNumber, @ItemCode, 1, @ColorCode, @ItemDim1Code,
                            '', '', 3, '', NULL,
                            @OfficeCode, @WarehouseCode, @Quantity, 0, 0, 0,
                            @OfficeCode, @FromWarehouseCode, '', 'Inner',
                            @ApplicationID, '', '', '',
                            5, '', 5, '',
                            NULL, @MinDate, @MinDate, @UserName,
                            GETDATE(), @UserName, GETDATE()
                        )
                    ";
                    
                    var inStockParameters = new[]
                    {
                        new SqlParameter("@StockID", inStockId),
                        new SqlParameter("@InnerProcessCode", "WT"),
                        new SqlParameter("@DocumentDate", operationDate.Date),
                        new SqlParameter("@DocumentTime", operationDate.TimeOfDay),
                        new SqlParameter("@OperationDate", operationDate.Date),
                        new SqlParameter("@OperationTime", operationDate.TimeOfDay),
                        new SqlParameter("@DocumentNumber", transferNumber),
                        new SqlParameter("@ItemCode", line.ItemCode),
                        new SqlParameter("@ColorCode", line.ColorCode),
                        new SqlParameter("@ItemDim1Code", line.ItemDim1Code),
                        new SqlParameter("@OfficeCode", "M"), // Varsayılan ofis kodu
                        new SqlParameter("@WarehouseCode", targetWarehouseCode),
                        new SqlParameter("@Quantity", line.Quantity),
                        new SqlParameter("@FromWarehouseCode", sourceWarehouseCode),
                        new SqlParameter("@ApplicationID", innerHeaderId),
                        new SqlParameter("@MinDate", minDate),
                        new SqlParameter("@UserName", userName)
                    };
                    
                    await _context.ExecuteNonQueryAsync(inStockQuery, inStockParameters);
                    
                    // Stok hareketleri arasındaki bağlantıyı kur
                    var crossQuery = @"
                        INSERT INTO tpStockCross (
                            StockID, CrossStockID,
                            CreatedUserName, CreatedDate,
                            LastUpdatedUserName, LastUpdatedDate
                        ) VALUES (
                            @StockID, @CrossStockID,
                            @UserName, GETDATE(),
                            @UserName, GETDATE()
                        )
                    ";
                    
                    var crossParameters = new[]
                    {
                        new SqlParameter("@StockID", outStockId),
                        new SqlParameter("@CrossStockID", inStockId),
                        new SqlParameter("@UserName", userName)
                    };
                    
                    await _context.ExecuteNonQueryAsync(crossQuery, crossParameters);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Stok hareketleri oluşturulurken hata oluştu. InnerHeaderID: {InnerHeaderID}", innerHeaderId);
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<bool> CancelWarehouseTransferAsync(string transferNumber, string userName)
        {
            try
            {
                // Önce sevk kaydını kontrol et
                var checkQuery = @"
                    SELECT InnerHeaderID, IsTransferApproved 
                    FROM trInnerHeader WITH (NOLOCK)
                    WHERE InnerNumber = @TransferNumber 
                      AND InnerProcessCode = 'WT'
                ";
                
                var checkParameters = new[] { new SqlParameter("@TransferNumber", transferNumber) };
                Guid? innerHeaderId = null;
                bool isApproved = false;
                
                using (var reader = await _context.ExecuteReaderAsync(checkQuery, checkParameters))
                {
                    if (await reader.ReadAsync())
                    {
                        innerHeaderId = (Guid)reader["InnerHeaderID"];
                        isApproved = Convert.ToBoolean(reader["IsTransferApproved"]);
                    }
                }
                
                if (innerHeaderId == null)
                {
                    _logger.LogWarning("İptal edilecek sevk kaydı bulunamadı. Fiş No: {TransferNumber}", transferNumber);
                    return false;
                }
                
                if (isApproved)
                {
                    _logger.LogWarning("Onaylanmış sevk kaydı iptal edilemez. Fiş No: {TransferNumber}", transferNumber);
                    return false; // Onaylanmış kayıtlar iptal edilemez
                }
                
                // İptal işlemi için sevk kaydını sil (soft delete)
                var updateQuery = @"
                    UPDATE trInnerHeader 
                    SET IsDeleted = 1,
                        DeletedDate = @DeletedDate,
                        DeletedUserName = @UserName,
                        ModifiedDate = @ModifiedDate,
                        ModifiedUserName = @UserName
                    WHERE InnerHeaderID = @InnerHeaderID
                ";
                
                var now = DateTime.Now;
                var updateParameters = new[]
                {
                    new SqlParameter("@InnerHeaderID", innerHeaderId),
                    new SqlParameter("@DeletedDate", now),
                    new SqlParameter("@UserName", userName),
                    new SqlParameter("@ModifiedDate", now)
                };
                
                await _context.ExecuteNonQueryAsync(updateQuery, updateParameters);
                
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sevk iptal edilirken hata oluştu. Fiş No: {TransferNumber}", transferNumber);
                throw;
            }
        }
        
        /// <inheritdoc/>
        public async Task<bool> LockWarehouseTransferAsync(string transferNumber, string userName, string comment = null)
        {
            try
            {
                // Önce sevk kaydını kontrol et
                var checkQuery = @"
                    SELECT h.InnerHeaderID, h.IsLocked
                    FROM trInnerHeader h WITH (NOLOCK)
                    WHERE h.InnerNumber = @TransferNumber 
                      AND h.InnerProcessCode = 'WT'
                ";
                
                var checkParameters = new[] { new SqlParameter("@TransferNumber", transferNumber) };
                Guid? innerHeaderId = null;
                bool isAlreadyLocked = false;
                
                using (var reader = await _context.ExecuteReaderAsync(checkQuery, checkParameters))
                {
                    if (await reader.ReadAsync())
                    {
                        innerHeaderId = (Guid)reader["InnerHeaderID"];
                        isAlreadyLocked = Convert.ToBoolean(reader["IsLocked"]);
                    }
                }
                
                if (innerHeaderId == null)
                {
                    _logger.LogWarning("Kilitlenecek sevk kaydı bulunamadı. Fiş No: {TransferNumber}", transferNumber);
                    return false;
                }
                
                if (isAlreadyLocked)
                {
                    // Kilit durumunu kontrol et
                    var lockStatusResult = await CheckWarehouseTransferLockStatusAsync(transferNumber);
                    if (lockStatusResult.IsLocked && lockStatusResult.LockedByUser != userName)
                    {
                        _logger.LogWarning("Sevk kaydı başka bir kullanıcı tarafından kilitlenmiş. Fiş No: {TransferNumber}, Kilitleyen: {LockedByUser}", 
                            transferNumber, lockStatusResult.LockedByUser);
                        return false;
                    }
                }
                
                // trInnerHeader tablosunda IsLocked alanını güncelle
                var updateHeaderQuery = @"
                    UPDATE trInnerHeader 
                    SET IsLocked = 1,
                        LastUpdatedUserName = @UserName,
                        LastUpdatedDate = GETDATE()
                    WHERE InnerHeaderID = @InnerHeaderID
                ";
                
                var updateHeaderParameters = new[]
                {
                    new SqlParameter("@InnerHeaderID", innerHeaderId),
                    new SqlParameter("@UserName", userName)
                };
                
                await _context.ExecuteNonQueryAsync(updateHeaderQuery, updateHeaderParameters);
                
                // auTransactionCheckInOutTrace tablosuna kayıt ekle
                var now = DateTime.Now;
                var minDate = new DateTime(1900, 1, 1); // SQL Server'da minimum tarih
                var insertTraceQuery = @"
                    INSERT INTO auTransactionCheckInOutTrace (
                        TableName, HeaderID, UserName, 
                        CheckOutDate, Comment, CheckInDate, CheckOutReasonCode
                    ) VALUES (
                        'trInnerHeader', @HeaderID, @UserName, 
                        @CheckOutDate, @Comment, @CheckInDate, ''
                    )
                ";
                
                var insertTraceParameters = new[]
                {
                    new SqlParameter("@HeaderID", innerHeaderId),
                    new SqlParameter("@UserName", userName),
                    new SqlParameter("@CheckOutDate", now),
                    new SqlParameter("@Comment", (object)comment ?? DBNull.Value),
                    new SqlParameter("@CheckInDate", minDate)
                };
                
                await _context.ExecuteNonQueryAsync(insertTraceQuery, insertTraceParameters);
                
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sevk kaydı kilitlenirken hata oluştu. Fiş No: {TransferNumber}", transferNumber);
                throw;
            }
        }
        
        /// <inheritdoc/>
        public async Task<bool> UnlockWarehouseTransferAsync(string transferNumber, string userName)
        {
            try
            {
                // Önce sevk kaydını kontrol et
                var checkQuery = @"
                    SELECT h.InnerHeaderID, h.IsLocked
                    FROM trInnerHeader h WITH (NOLOCK)
                    WHERE h.InnerNumber = @TransferNumber 
                      AND h.InnerProcessCode = 'WT'
                ";
                
                var checkParameters = new[] { new SqlParameter("@TransferNumber", transferNumber) };
                Guid? innerHeaderId = null;
                bool isLocked = false;
                
                using (var reader = await _context.ExecuteReaderAsync(checkQuery, checkParameters))
                {
                    if (await reader.ReadAsync())
                    {
                        innerHeaderId = (Guid)reader["InnerHeaderID"];
                        isLocked = Convert.ToBoolean(reader["IsLocked"]);
                    }
                }
                
                if (innerHeaderId == null)
                {
                    _logger.LogWarning("Kilidi kaldırılacak sevk kaydı bulunamadı. Fiş No: {TransferNumber}", transferNumber);
                    return false;
                }
                
                if (!isLocked)
                {
                    _logger.LogWarning("Sevk kaydı zaten kilitli değil. Fiş No: {TransferNumber}", transferNumber);
                    return true; // Zaten kilitli değil, başarılı kabul et
                }
                
                // Kilit durumunu kontrol et
                var lockStatusResult = await CheckWarehouseTransferLockStatusAsync(transferNumber);
                if (lockStatusResult.IsLocked && lockStatusResult.LockedByUser != userName)
                {
                    _logger.LogWarning("Sevk kaydının kilidi başka bir kullanıcı tarafından açılamaz. Fiş No: {TransferNumber}, Kilitleyen: {LockedByUser}", 
                        transferNumber, lockStatusResult.LockedByUser);
                    return false;
                }
                
                // trInnerHeader tablosunda IsLocked alanını güncelle
                var updateHeaderQuery = @"
                    UPDATE trInnerHeader 
                    SET IsLocked = 0,
                        LastUpdatedUserName = @UserName,
                        LastUpdatedDate = GETDATE()
                    WHERE InnerHeaderID = @InnerHeaderID
                ";
                
                var updateHeaderParameters = new[]
                {
                    new SqlParameter("@InnerHeaderID", innerHeaderId),
                    new SqlParameter("@UserName", userName)
                };
                
                await _context.ExecuteNonQueryAsync(updateHeaderQuery, updateHeaderParameters);
                
                // auTransactionCheckInOutTrace tablosunu güncelle
                var now = DateTime.Now;
                var updateTraceQuery = @"
                    UPDATE auTransactionCheckInOutTrace 
                    SET CheckInDate = @CheckInDate,
                        UserName = @UserName
                    WHERE TableName = 'trInnerHeader'
                      AND HeaderID = @HeaderID
                      AND CheckInDate IS NULL
                ";
                
                var updateTraceParameters = new[]
                {
                    new SqlParameter("@HeaderID", innerHeaderId),
                    new SqlParameter("@UserName", userName),
                    new SqlParameter("@CheckInDate", now)
                };
                
                await _context.ExecuteNonQueryAsync(updateTraceQuery, updateTraceParameters);
                
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sevk kaydının kilidi kaldırılırken hata oluştu. Fiş No: {TransferNumber}", transferNumber);
                throw;
            }
        }
        
        /// <inheritdoc/>
        public async Task<(bool IsLocked, string LockedByUser, DateTime? LockDate)> CheckWarehouseTransferLockStatusAsync(string transferNumber)
        {
            try
            {
                // Sevk kaydının ID'sini bul
                var headerQuery = @"
                    SELECT h.InnerHeaderID, h.IsLocked
                    FROM trInnerHeader h WITH (NOLOCK)
                    WHERE h.InnerNumber = @TransferNumber 
                      AND h.InnerProcessCode = 'WT'
                ";
                
                var headerParameters = new[] { new SqlParameter("@TransferNumber", transferNumber) };
                Guid? innerHeaderId = null;
                bool isLocked = false;
                
                using (var reader = await _context.ExecuteReaderAsync(headerQuery, headerParameters))
                {
                    if (await reader.ReadAsync())
                    {
                        innerHeaderId = (Guid)reader["InnerHeaderID"];
                        isLocked = Convert.ToBoolean(reader["IsLocked"]);
                    }
                }
                
                if (innerHeaderId == null || !isLocked)
                {
                    return (false, null, null);
                }
                
                // Kilit bilgilerini al
                var traceQuery = @"
                    SELECT TOP 1 UserName, CheckOutDate
                    FROM auTransactionCheckInOutTrace WITH (NOLOCK)
                    WHERE TableName = 'trInnerHeader'
                      AND HeaderID = @HeaderID
                      AND CheckInDate IS NULL
                    ORDER BY CheckOutDate DESC
                ";
                
                var traceParameters = new[] { new SqlParameter("@HeaderID", innerHeaderId) };
                string lockedByUser = null;
                DateTime? lockDate = null;
                
                using (var reader = await _context.ExecuteReaderAsync(traceQuery, traceParameters))
                {
                    if (await reader.ReadAsync())
                    {
                        lockedByUser = reader["UserName"].ToString();
                        lockDate = reader["CheckOutDate"] != DBNull.Value ? (DateTime?)reader["CheckOutDate"] : null;
                    }
                }
                
                return (isLocked, lockedByUser, lockDate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sevk kaydının kilit durumu kontrol edilirken hata oluştu. Fiş No: {TransferNumber}", transferNumber);
                throw;
            }
        }
    }
}
