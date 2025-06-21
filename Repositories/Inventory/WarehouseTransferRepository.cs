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
        public async Task<List<WarehouseTransferItemResponse>> GetWarehouseTransferItemsAsync(string transferNumber)
        {
            try
            {
                if (string.IsNullOrEmpty(transferNumber))
                {
                    throw new ArgumentNullException(nameof(transferNumber), "Transfer numarası boş olamaz.");
                }

                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@TransferNumber", transferNumber)
                };

                var query = $@"
                SELECT Lines.* FROM (
                SELECT    SortOrder				= CASE WHEN 1 = 1 AND 1 = 1 THEN trInnerLine.SortOrder ELSE 0 END
		, ProductCode			= CASE WHEN trInnerLine.ItemTypeCode = 1 THEN trInnerLine.ItemCode ELSE SPACE(0) END
		, ProductDescription	= ISNULL((SELECT ItemDescription FROM cdItemDesc WITH(NOLOCK) WHERE cdItemDesc.ItemTypeCode = 1 AND cdItemDesc.ItemCode = trInnerLine.ItemCode AND cdItemDesc.LangCode = N'TR'), SPACE(0))
		, ItemTypeCode			= trInnerLine.ItemTypeCode
		, ItemTypeDescription	= ISNULL((SELECT ItemTypeDescription FROM bsItemTypeDesc WITH(NOLOCK) WHERE bsItemTypeDesc.ItemTypeCode = trInnerLine.ItemTypeCode AND bsItemTypeDesc.LangCode = N'TR'), SPACE(0))
		, ItemCode				= trInnerLine.ItemCode
		, ItemDescription		= ISNULL((SELECT ItemDescription FROM cdItemDesc WITH(NOLOCK) WHERE cdItemDesc.ItemTypeCode = trInnerLine.ItemTypeCode AND cdItemDesc.ItemCode =trInnerLine.ItemCode AND cdItemDesc.LangCode =N'TR'), SPACE(0))
		, ColorCode				= CASE WHEN 1 = 1 THEN trInnerLine.ColorCode ELSE SPACE(0) END
		, ColorDescription		= ISNULL((SELECT ColorDescription FROM cdColorDesc WITH(NOLOCK) WHERE cdColorDesc.ColorCode = CASE WHEN 1 = 1 THEN trInnerLine.ColorCode ELSE SPACE(0) END AND cdColorDesc.LangCode = N'TR'), SPACE(0))
		, ItemDim1Code			= CASE WHEN 1 = 1 THEN trInnerLine.ItemDim1Code ELSE SPACE(0) END
		, ItemDim2Code			= CASE WHEN 1 = 1 THEN trInnerLine.ItemDim2Code ELSE SPACE(0) END
		, ItemDim3Code			= CASE WHEN 1 = 1 THEN trInnerLine.ItemDim3Code ELSE SPACE(0) END
		, UnitOfMeasureCode1	= cdItem.UnitOfMeasureCode1		
	    , UnitOfMeasureCode2	= cdItem.UnitOfMeasureCode2		
	    , Barcode				= ISNULL((SELECT MAX(Barcode) FROM prItemBarcode WITH(NOLOCK)
											WHERE	prItemBarcode.BarcodeTypeCode	= N''
												AND prItemBarcode.ItemTypeCode		= trInnerLine.ItemTypeCode
												AND prItemBarcode.ItemCode			= trInnerLine.ItemCode
												AND prItemBarcode.ColorCode			= CASE WHEN 1 = 1 THEN trInnerLine.ColorCode ELSE SPACE(0) END 
												AND prItemBarcode.ItemDim1Code		= CASE WHEN 1 = 1 THEN trInnerLine.ItemDim1Code ELSE SPACE(0) END
												AND prItemBarcode.ItemDim2Code		= CASE WHEN 1 = 1 THEN trInnerLine.ItemDim2Code ELSE SPACE(0) END
												AND prItemBarcode.ItemDim3Code		= CASE WHEN 1 = 1 THEN trInnerLine.ItemDim3Code ELSE SPACE(0) END
												AND 1 = 1 AND 1 = 1) , SPACE(0))

		, Quantity			        = SUM(trInnerLine.Qty1)				
		, Qty2				        = SUM(trInnerLine.Qty2)		
		, MissingStock				= SUM(ISNULL(rpTransferApproved.MissingStock, 0))
	    , OverStock					= SUM(ISNULL(rpTransferApproved.OverStock	, 0))

	    , LineDescription				= CASE WHEN 1 = 1 AND 1 = 1 THEN trInnerLine.LineDescription ELSE SPACE(0) END
		, HeaderID				= trInnerLine.InnerHeaderID	
	 
FROM trInnerLine WITH(NOLOCK)	
	INNER JOIN cdItem WITH(NOLOCK)
		ON cdItem.ItemTypeCode = trInnerLine.ItemTypeCode
		AND cdItem.ItemCode = trInnerLine.ItemCode	
	LEFT OUTER JOIN ProductHierarchy(N'TR')
		ON ProductHierarchy.ProductHierarchyID = cdItem.ProductHierarchyID
	LEFT OUTER JOIN ProductCollection(N'TR')
		ON ProductCollection.ProductCollectionGrCode = cdItem.ProductCollectionGrCode
	LEFT OUTER JOIN ProductAttributesFilter 
		ON ProductAttributesFilter.ItemTypeCode = 1 
		AND ProductAttributesFilter.ItemCode = cdItem.ItemCode
	LEFT OUTER JOIN InnerITAttributesFilter 
		ON InnerITAttributesFilter.InnerLineID = trInnerLine.InnerLineID
	LEFT OUTER JOIN rpTransferApproved WITH(NOLOCK)
		ON rpTransferApproved.ApplicationCode		= N'Inner'
		AND rpTransferApproved.InnerProcessCode		= 'WT'
		AND rpTransferApproved.ApplicationID		= trInnerLine.InnerHeaderID
		AND rpTransferApproved.ApplicationLineID	= trInnerLine.InnerLineID
GROUP BY  CASE WHEN 1 = 1 AND 1 = 1 THEN trInnerLine.SortOrder ELSE 0 END
		, trInnerLine.ItemTypeCode
		, trInnerLine.ItemCode
		, CASE WHEN 1 = 1 THEN trInnerLine.ColorCode ELSE SPACE(0) END
		, CASE WHEN 1 = 1 THEN trInnerLine.ItemDim1Code ELSE SPACE(0) END
		, CASE WHEN 1 = 1 THEN trInnerLine.ItemDim2Code ELSE SPACE(0) END
		, CASE WHEN 1 = 1 THEN trInnerLine.ItemDim3Code ELSE SPACE(0) END
		, cdItem.UnitOfMeasureCode1	
		, cdItem.UnitOfMeasureCode2	
		, CASE WHEN 1 = 1 AND 1 = 1 THEN trInnerLine.LineDescription ELSE SPACE(0) END
		, ProductAttributesFilter.ProductAtt01
		, ProductAttributesFilter.ProductAtt02
		, ProductAttributesFilter.ProductAtt03
		, ProductAttributesFilter.ProductAtt04
		, ProductAttributesFilter.ProductAtt05
		, ProductAttributesFilter.ProductAtt06
		, ProductAttributesFilter.ProductAtt07
		, ProductAttributesFilter.ProductAtt08
		, ProductAttributesFilter.ProductAtt09
		, ProductAttributesFilter.ProductAtt10
		, ProductAttributesFilter.ProductAtt11
		, ProductAttributesFilter.ProductAtt12
		, ProductAttributesFilter.ProductAtt13
		, ProductAttributesFilter.ProductAtt14
		, ProductAttributesFilter.ProductAtt15
		, trInnerLine.InnerHeaderID
                ) Lines
                WHERE EXISTS (
SELECT * FROM (
SELECT * FROM (
SELECT    InnerNumberWT				= trInnerHeader.InnerNumber
		, OperationDate
		, OperationTime
		, InnerProcessCode
		, InnerProcessDescription	= ISNULL((SELECT InnerProcessDescription FROM bsInnerProcessDesc WITH(NOLOCK) WHERE bsInnerProcessDesc.InnerProcessCode = trInnerHeader.InnerProcessCode AND bsInnerProcessDesc.LangCode = N'TR'), SPACE(0))
		
		, Series
		, SeriesNumber
		, Description

		, CompanyCode
		, OfficeCode
		, OfficeDescription			= ISNULL((SELECT OfficeDescription FROM cdOfficeDesc WITH(NOLOCK) WHERE cdOfficeDesc.OfficeCode = trInnerHeader.OfficeCode AND cdOfficeDesc.LangCode = N'TR'), SPACE(0))
		, StoreCode	
		, StoreDescription			= ISNULL((SELECT CurrAccDescription FROM cdCurrAccDesc WITH(NOLOCK) WHERE cdCurrAccDesc.CurrAccTypeCode = trInnerHeader.StoreTypeCode AND cdCurrAccDesc.CurrAccCode = trInnerHeader.StoreCode AND cdCurrAccDesc.LangCode = N'TR') ,SPACE(0))				 
		, CustomerCode				= trInnerHeader.CurrAccCode  
		, CustomerDescription		= ISNULL((SELECT CurrAccDescription FROM cdCurrAccDesc WITH(NOLOCK) WHERE cdCurrAccDesc.CurrAccTypeCode = trInnerHeader.CurrAccTypeCode AND cdCurrAccDesc.CurrAccCode = trInnerHeader.CurrAccCode AND cdCurrAccDesc.LangCode = N'TR') ,SPACE(0))
		, WarehouseCode
		, WarehouseDescription		= ISNULL((SELECT WarehouseDescription FROM cdWarehouseDesc WITH(NOLOCK) WHERE cdWarehouseDesc.WarehouseCode = trInnerHeader.WarehouseCode AND cdWarehouseDesc.LangCode= N'TR'), SPACE(0))
		 
		, ToOfficeCode				
		, ToWarehouseCode			
		, ToWarehouseDescription	= ISNULL((SELECT WarehouseDescription FROM cdWarehouseDesc WITH(NOLOCK) WHERE cdWarehouseDesc.WarehouseCode = trInnerHeader.ToWarehouseCode AND cdWarehouseDesc.LangCode= N'TR'), SPACE(0))

		, IsCompleted
		, IsLocked
		, IsTransferApproved
		, TransferApprovedDate

		, InnerHeaderID				= trInnerHeader.InnerHeaderID
    FROM trInnerHeader WITH(NOLOCK)
    WHERE trInnerHeader.InnerProcessCode = 'WT'
    AND trInnerHeader.InnerNumber = @TransferNumber
    ) Query WHERE CompanyCode = 1
    ) ReportTable 
    WHERE ReportTable.InnerHeaderID = Lines.HeaderID )
                ";

                var items = new List<WarehouseTransferItemResponse>();
                
                using (var reader = await _context.ExecuteReaderAsync(query, parameters.ToArray()))
                {
                    while (await reader.ReadAsync())
                    {
                        items.Add(new WarehouseTransferItemResponse
                        {
                            ItemCode = reader["ItemCode"].ToString(),
                            ItemName = reader["ItemDescription"].ToString(),
                            ColorCode = reader["ColorCode"].ToString(),
                            ColorName = reader["ColorDescription"].ToString(),
                            ItemDim1Code = reader["ItemDim1Code"].ToString(),
                            ItemDim1Name = string.Empty,
                            Quantity = Convert.ToDouble(reader["Quantity"]),
                            UnitCode = reader["UnitOfMeasureCode1"].ToString(),
                            Barcode = reader["Barcode"].ToString(),
                            LineDescription = reader["LineDescription"].ToString()
                        });
                    }
                }

                return items;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting warehouse transfer items for transfer number {TransferNumber}", transferNumber);
                throw new Exception($"Depolar arası sevk satırları getirilirken bir hata oluştu. Transfer No: {transferNumber}", ex);
            }
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<WarehouseTransferResponse>> GetWarehouseTransfersAsync(
            DateTime? startDate = null,
            DateTime? endDate = null,
            string warehouseCode = null,
            string targetWarehouseCode = null)
        {
            try
            {
                var whereConditions = new List<string>();
                var parameters = new List<SqlParameter>();

                if (startDate.HasValue)
                {
                    whereConditions.Add("trInnerHeader.OperationDate >= @StartDate");
                    parameters.Add(new SqlParameter("@StartDate", startDate.Value.Date));
                }
                else
                {
                    // Default son 30 gün
                    whereConditions.Add("trInnerHeader.OperationDate >= @StartDate");
                    parameters.Add(new SqlParameter("@StartDate", DateTime.Now.AddDays(-30).Date));
                }

                if (endDate.HasValue)
                {
                    whereConditions.Add("trInnerHeader.OperationDate <= @EndDate");
                    parameters.Add(new SqlParameter("@EndDate", endDate.Value.Date.AddDays(1).AddSeconds(-1)));
                }
                else
                {
                    // Default bugün
                    whereConditions.Add("trInnerHeader.OperationDate <= @EndDate");
                    parameters.Add(new SqlParameter("@EndDate", DateTime.Now.Date.AddDays(1).AddSeconds(-1)));
                }

                if (!string.IsNullOrEmpty(warehouseCode))
                {
                    whereConditions.Add("trInnerHeader.WarehouseCode = @WarehouseCode");
                    parameters.Add(new SqlParameter("@WarehouseCode", warehouseCode));
                }

                if (!string.IsNullOrEmpty(targetWarehouseCode))
                {
                    whereConditions.Add("trInnerHeader.ToWarehouseCode = @TargetWarehouseCode");
                    parameters.Add(new SqlParameter("@TargetWarehouseCode", targetWarehouseCode));
                }

                string whereClause = whereConditions.Count > 0 ? $"AND {string.Join(" AND ", whereConditions)}" : string.Empty;

                var query = $@"
                SELECT * FROM (
                SELECT    InnerNumberWT				= trInnerHeader.InnerNumber
                        , OperationDate
                        , OperationTime
                        , InnerProcessCode
                        , InnerProcessDescription	= ISNULL((SELECT InnerProcessDescription FROM bsInnerProcessDesc WITH(NOLOCK) WHERE bsInnerProcessDesc.InnerProcessCode = trInnerHeader.InnerProcessCode AND bsInnerProcessDesc.LangCode = N'TR'), SPACE(0))
                        
                        , Series
                        , SeriesNumber
                        , Description

                        , CompanyCode
                        , OfficeCode
                        , OfficeDescription			= ISNULL((SELECT OfficeDescription FROM cdOfficeDesc WITH(NOLOCK) WHERE cdOfficeDesc.OfficeCode = trInnerHeader.OfficeCode AND cdOfficeDesc.LangCode = N'TR'), SPACE(0))
                        , StoreCode	
                        , StoreDescription			= ISNULL((SELECT CurrAccDescription FROM cdCurrAccDesc WITH(NOLOCK) WHERE cdCurrAccDesc.CurrAccTypeCode = trInnerHeader.StoreTypeCode AND cdCurrAccDesc.CurrAccCode = trInnerHeader.StoreCode AND cdCurrAccDesc.LangCode = N'TR') ,SPACE(0))				 
                        , CustomerCode				= trInnerHeader.CurrAccCode  
                        , CustomerDescription		= ISNULL((SELECT CurrAccDescription FROM cdCurrAccDesc WITH(NOLOCK) WHERE cdCurrAccDesc.CurrAccTypeCode = trInnerHeader.CurrAccTypeCode AND cdCurrAccDesc.CurrAccCode = trInnerHeader.CurrAccCode AND cdCurrAccDesc.LangCode = N'TR') ,SPACE(0))
                        , WarehouseCode
                        , WarehouseDescription		= ISNULL((SELECT WarehouseDescription FROM cdWarehouseDesc WITH(NOLOCK) WHERE cdWarehouseDesc.WarehouseCode = trInnerHeader.WarehouseCode AND cdWarehouseDesc.LangCode= N'TR'), SPACE(0))
                        
                        , ToOfficeCode				
                        , ToWarehouseCode			
                        , ToWarehouseDescription	= ISNULL((SELECT WarehouseDescription FROM cdWarehouseDesc WITH(NOLOCK) WHERE cdWarehouseDesc.WarehouseCode = trInnerHeader.ToWarehouseCode AND cdWarehouseDesc.LangCode= N'TR'), SPACE(0))

                        , IsCompleted
                        , IsLocked
                        , IsTransferApproved
                        , TransferApprovedDate
                        , TotalQty = ISNULL((SELECT SUM(Qty1) FROM trInnerLine WITH(NOLOCK) WHERE trInnerLine.InnerHeaderID = trInnerHeader.InnerHeaderID), 0)

                        , InnerHeaderID				= trInnerHeader.InnerHeaderID
                FROM trInnerHeader WITH(NOLOCK)
                WHERE trInnerHeader.InnerProcessCode = 'WT'
                {whereClause}
                ) Query WHERE CompanyCode = 1
                ORDER BY OperationDate DESC, OperationTime DESC
                ";

                var transfers = new List<WarehouseTransferResponse>();
                
                using (var reader = await _context.ExecuteReaderAsync(query, parameters.ToArray()))
                {
                    while (await reader.ReadAsync())
                    {
                        transfers.Add(new WarehouseTransferResponse
                        {
                            TransferNumber = reader["InnerNumberWT"].ToString(),
                            OperationDate = Convert.ToDateTime(reader["OperationDate"]),
                            OperationTime = reader["OperationTime"] != DBNull.Value ? (TimeSpan)reader["OperationTime"] : TimeSpan.Zero,
                            Series = reader["Series"].ToString(),
                            SeriesNumber = reader["SeriesNumber"].ToString(),
                            InnerProcessCode = reader["InnerProcessCode"].ToString(),
                            IsReturn = false, // Varsayılan değer
                            CompanyCode = reader["CompanyCode"].ToString(),
                            OfficeCode = reader["OfficeCode"].ToString(),
                            ToOfficeCode = reader["ToOfficeCode"].ToString(),
                            StoreCode = reader["StoreCode"].ToString(),
                            SourceWarehouseCode = reader["WarehouseCode"].ToString(),
                            SourceWarehouseName = reader["WarehouseDescription"].ToString(),
                            TargetWarehouseCode = reader["ToWarehouseCode"].ToString(),
                            TargetWarehouseName = reader["ToWarehouseDescription"].ToString(),
                            ToStoreCode = string.Empty, // Varsayılan değer
                            CurrAccTypeCode = string.Empty, // Varsayılan değer
                            VendorCode = string.Empty, // Varsayılan değer
                            CustomerCode = reader["CustomerCode"].ToString(),
                            RetailCustomerCode = string.Empty, // Varsayılan değer
                            EmployeeCode = string.Empty, // Varsayılan değer
                            TotalQty = reader["TotalQty"] != DBNull.Value ? Convert.ToDouble(reader["TotalQty"]) : 0,
                            Description = reader["Description"].ToString(),
                            ImportFileNumber = string.Empty, // Varsayılan değer
                            IsCompleted = Convert.ToBoolean(reader["IsCompleted"]),
                            IsLocked = Convert.ToBoolean(reader["IsLocked"]),
                            IsApproved = Convert.ToBoolean(reader["IsTransferApproved"]),
                            IsTransferApproved = Convert.ToBoolean(reader["IsTransferApproved"]),
                            IsInnerOrderBase = false, // Varsayılan değer
                            IsSectionTransfer = false, // Varsayılan değer
                            ApplicationCode = string.Empty, // Varsayılan değer
                            ApplicationDescription = string.Empty, // Varsayılan değer
                            ApplicationID = Guid.Empty, // Varsayılan değer
                            ShipmentMethodCode = string.Empty, // Varsayılan değer
                            ShipmentMethodName = string.Empty, // Varsayılan değer
                            ApprovalDate = reader["TransferApprovedDate"] != DBNull.Value ? Convert.ToDateTime(reader["TransferApprovedDate"]) : null,
                            TransferApprovedDate = reader["TransferApprovedDate"] != DBNull.Value ? Convert.ToDateTime(reader["TransferApprovedDate"]) : null,
                            CreatedUserName = string.Empty, // Varsayılan değer
                            CreatedDate = DateTime.Now // Varsayılan değer
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
                    SELECT InnerNumber = h.InnerNumber
                        , OperationDate = h.OperationDate
                        , OperationTime = h.OperationTime 
                        , Series = h.Series
                        , SeriesNumber = h.SeriesNumber
                        , InnerProcessCode = h.InnerProcessCode
                        , IsReturn = h.IsReturn
                        , CompanyCode = h.CompanyCode
                        , OfficeCode = h.OfficeCode
                        , ToOfficeCode = h.ToOfficeCode
                        , StoreCode = h.StoreCode
                        , WarehouseCode = h.WarehouseCode
                        , ToWarehouseCode = h.ToWarehouseCode
                        , ToStoreCode = h.ToStoreCode
                        , CurrAccTypeCode = h.CurrAccTypeCode
                        , VendorCode = CASE CurrAccTypeCode WHEN 1 THEN h.CurrAccCode ELSE SPACE(0) END
                        , CustomerCode = CASE CurrAccTypeCode WHEN 3 THEN h.CurrAccCode ELSE SPACE(0) END
                        , RetailCustomerCode = CASE CurrAccTypeCode WHEN 4 THEN h.CurrAccCode ELSE SPACE(0) END
                        , EmployeeCode = CASE CurrAccTypeCode WHEN 8 THEN h.CurrAccCode ELSE SPACE(0) END
                        , TotalQty = ISNULL(InnerLines.Qty1, 0)
                        , Description = h.Description
                        , ImportFileNumber = h.ImportFileNumber
                        , IsCompleted = h.IsCompleted
                        , IsLocked = h.IsLocked
                        , IsTransferApproved = h.IsTransferApproved
                        , IsInnerOrderBase = h.IsInnerOrderBase
                        , IsSectionTransfer = h.IsSectionTransfer
                        , ApplicationCode = h.ApplicationCode
                        , ApplicationDescription = ISNULL(bsApplicationDesc.ApplicationDescription, SPACE(0))
                        , h.ApplicationID
                        , h.InnerHeaderID
                        , SourceWarehouseName = ISNULL((SELECT WarehouseDescription FROM cdWarehouseDesc WITH(NOLOCK) WHERE cdWarehouseDesc.WarehouseCode = h.WarehouseCode AND cdWarehouseDesc.LangCode= N'TR'), SPACE(0))
                        , TargetWarehouseName = ISNULL((SELECT WarehouseDescription FROM cdWarehouseDesc WITH(NOLOCK) WHERE cdWarehouseDesc.WarehouseCode = h.ToWarehouseCode AND cdWarehouseDesc.LangCode= N'TR'), SPACE(0))
                        , ShipmentMethodCode = ISNULL(ext.ShipmentMethodCode, '')
                        , ShipmentMethodName = ISNULL((SELECT ShipmentMethodDescription FROM cdShipmentMethodDesc WITH(NOLOCK) WHERE cdShipmentMethodDesc.ShipmentMethodCode = ext.ShipmentMethodCode AND cdShipmentMethodDesc.LangCode = N'TR'), SPACE(0))
                        , h.CreatedUserName
                        , h.CreatedDate
                    FROM trInnerHeader h WITH(NOLOCK) 
                        LEFT OUTER JOIN bsApplicationDesc WITH(NOLOCK)  
                            ON bsApplicationDesc.ApplicationCode = h.ApplicationCode
                            AND bsApplicationDesc.LangCode = 'tr'
                        LEFT JOIN tpInnerHeaderExtension ext WITH (NOLOCK) ON h.InnerHeaderID = ext.InnerHeaderID
                        LEFT JOIN (SELECT InnerHeaderID, Qty1 = SUM(Qty1) 
                                FROM trInnerLine WITH(NOLOCK)
                                GROUP BY InnerHeaderID) AS InnerLines
                            ON InnerLines.InnerHeaderID = h.InnerHeaderID
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
                            TransferNumber = reader["InnerNumber"].ToString(),
                            SourceWarehouseCode = reader["WarehouseCode"].ToString(),
                            SourceWarehouseName = reader["SourceWarehouseName"].ToString(),
                            TargetWarehouseCode = reader["ToWarehouseCode"].ToString(),
                            TargetWarehouseName = reader["TargetWarehouseName"].ToString(),
                            OperationDate = Convert.ToDateTime(reader["OperationDate"]),
                            OperationTime = reader["OperationTime"] != DBNull.Value ? (TimeSpan)reader["OperationTime"] : TimeSpan.Zero,
                            Description = reader["Description"].ToString(),
                            ShipmentMethodCode = reader["ShipmentMethodCode"].ToString(),
                            ShipmentMethodName = reader["ShipmentMethodName"].ToString(),
                            IsApproved = Convert.ToBoolean(reader["IsTransferApproved"]),
                            ApprovalDate = reader["IsTransferApproved"] != DBNull.Value && Convert.ToBoolean(reader["IsTransferApproved"]) ? Convert.ToDateTime(reader["OperationDate"]) : (DateTime?)null,
                            CreatedUserName = reader["CreatedUserName"].ToString(),
                            CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                            TotalQty = reader["TotalQty"] != DBNull.Value ? Convert.ToDouble(reader["TotalQty"]) : 0,
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
                        SortOrder = CASE WHEN 1 = 1 AND 1 = 1 THEN l.SortOrder ELSE 0 END,
                        ProductCode = CASE WHEN l.ItemTypeCode = 1 THEN l.ItemCode ELSE SPACE(0) END,
                        ProductDescription = ISNULL((SELECT ItemDescription FROM cdItemDesc WITH(NOLOCK) WHERE cdItemDesc.ItemTypeCode = 1 AND cdItemDesc.ItemCode = l.ItemCode AND cdItemDesc.LangCode = N'TR'), SPACE(0)),
                        l.ItemTypeCode,
                        ItemTypeDescription = ISNULL((SELECT ItemTypeDescription FROM bsItemTypeDesc WITH(NOLOCK) WHERE bsItemTypeDesc.ItemTypeCode = l.ItemTypeCode AND bsItemTypeDesc.LangCode = N'TR'), SPACE(0)),
                        l.ItemCode,
                        ItemDescription = ISNULL((SELECT ItemDescription FROM cdItemDesc WITH(NOLOCK) WHERE cdItemDesc.ItemTypeCode = l.ItemTypeCode AND cdItemDesc.ItemCode = l.ItemCode AND cdItemDesc.LangCode = N'TR'), SPACE(0)),
                        l.ColorCode,
                        ColorDescription = ISNULL((SELECT ColorDescription FROM cdColorDesc WITH(NOLOCK) WHERE cdColorDesc.ColorCode = l.ColorCode AND cdColorDesc.LangCode = N'TR'), SPACE(0)),
                        l.ItemDim1Code,
                        ItemDim1Description = ISNULL((SELECT ItemDim1Description FROM cdItemDim1Desc WITH(NOLOCK) WHERE cdItemDim1Desc.ItemDim1Code = l.ItemDim1Code AND cdItemDim1Desc.LangCode = N'TR'), SPACE(0)),
                        UnitOfMeasureCode1 = i.UnitOfMeasureCode1,
                        UnitOfMeasureCode2 = i.UnitOfMeasureCode2,
                        Barcode = ISNULL((SELECT MAX(Barcode) FROM prItemBarcode WITH(NOLOCK)
                                WHERE prItemBarcode.BarcodeTypeCode = N''
                                AND prItemBarcode.ItemTypeCode = l.ItemTypeCode
                                AND prItemBarcode.ItemCode = l.ItemCode
                                AND prItemBarcode.ColorCode = l.ColorCode
                                AND prItemBarcode.ItemDim1Code = l.ItemDim1Code), SPACE(0)),
                        Quantity = l.Qty1,
                        l.LineDescription
                    FROM trInnerLine l WITH (NOLOCK)
                    LEFT JOIN cdItem i WITH (NOLOCK) ON l.ItemTypeCode = i.ItemTypeCode AND l.ItemCode = i.ItemCode
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
                            ItemName = reader["ItemDescription"].ToString(),
                            ColorCode = reader["ColorCode"].ToString(),
                            ColorName = reader["ColorDescription"].ToString(),
                            ItemDim1Code = reader["ItemDim1Code"].ToString(),
                            ItemDim1Name = reader["ItemDim1Description"].ToString(),
                            Quantity = Convert.ToDouble(reader["Quantity"]),
                            UnitCode = reader["UnitOfMeasureCode1"].ToString(),
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
