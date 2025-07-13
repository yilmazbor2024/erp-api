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
    /// Üretim fişi işlemleri için repository implementasyonu
    /// </summary>
    public class ProductionOrderRepository : IProductionOrderRepository
    {
        private readonly ErpDbContext _context;
        private readonly ILogger<ProductionOrderRepository> _logger;

        public ProductionOrderRepository(
            ErpDbContext context,
            ILogger<ProductionOrderRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<List<ProductionOrderItemResponse>> GetProductionOrderItemsAsync(string orderNumber)
        {
            try
            {
                if (string.IsNullOrEmpty(orderNumber))
                {
                    throw new ArgumentNullException(nameof(orderNumber), "Üretim fiş numarası boş olamaz.");
                }

                var parameters = new[]
                {
                    new SqlParameter("@OrderNumber", orderNumber)
                };

                var query = $@"
                SELECT Lines.* FROM (
                SELECT    SortOrder= CASE WHEN 1 = 1 AND 1 = 1 THEN trInnerLine.SortOrder ELSE 0 END
                , ProductCode= CASE WHEN trInnerLine.ItemTypeCode = 1 THEN trInnerLine.ItemCode ELSE SPACE(0) END
                , ProductDescription= ISNULL((SELECT ItemDescription FROM cdItemDesc WITH(NOLOCK) WHERE cdItemDesc.ItemTypeCode = 1 AND cdItemDesc.ItemCode = trInnerLine.ItemCode AND cdItemDesc.LangCode = N'TR'), SPACE(0))
                , ItemTypeCode= trInnerLine.ItemTypeCode
                , ItemTypeDescription= ISNULL((SELECT ItemTypeDescription FROM bsItemTypeDesc WITH(NOLOCK) WHERE bsItemTypeDesc.ItemTypeCode = trInnerLine.ItemTypeCode AND bsItemTypeDesc.LangCode = N'TR'), SPACE(0))
                , ItemCode= trInnerLine.ItemCode
                , ItemDescription= ISNULL((SELECT ItemDescription FROM cdItemDesc WITH(NOLOCK) WHERE cdItemDesc.ItemTypeCode = trInnerLine.ItemTypeCode AND cdItemDesc.ItemCode = trInnerLine.ItemCode AND cdItemDesc.LangCode = N'TR'), SPACE(0))
                , UnitCode= trInnerLine.UnitCode
                , UnitDescription= ISNULL((SELECT UnitDescription FROM bsUnitDesc WITH(NOLOCK) WHERE bsUnitDesc.UnitCode = trInnerLine.UnitCode AND bsUnitDesc.LangCode = N'TR'), SPACE(0))
                , Quantity= trInnerLine.Quantity
                , Price= trInnerLine.Price
                , WarehouseCode= trInnerLine.WarehouseCode
                , WarehouseDescription= ISNULL((SELECT WarehouseDescription FROM cdWarehouseDesc WITH(NOLOCK) WHERE cdWarehouseDesc.WarehouseCode = trInnerLine.WarehouseCode AND cdWarehouseDesc.LangCode = N'TR'), SPACE(0))
                , LineDescription= trInnerLine.LineDescription
                , LineStatus= trInnerLine.LineStatus
                , LineStatusDescription= CASE trInnerLine.LineStatus 
                                            WHEN 0 THEN N'Bekliyor' 
                                            WHEN 1 THEN N'Tamamlandı' 
                                            WHEN 2 THEN N'İptal' 
                                            ELSE N'Bilinmiyor' END
                FROM trInner WITH(NOLOCK)
                INNER JOIN trInnerLine WITH(NOLOCK) ON trInner.InnerRefID = trInnerLine.InnerRefID
                WHERE trInner.InnerRef = @OrderNumber
                ) AS Lines
                ORDER BY Lines.SortOrder
                ";

                var items = new List<ProductionOrderItemResponse>();

                using (var reader = await _context.ExecuteReaderAsync(query, parameters))
                {
                    while (await reader.ReadAsync())
                    {
                        items.Add(new ProductionOrderItemResponse
                        {
                            SortOrder = Convert.ToInt32(reader["SortOrder"]),
                            ProductCode = reader["ProductCode"].ToString(),
                            ProductDescription = reader["ProductDescription"].ToString(),
                            ItemTypeCode = Convert.ToInt32(reader["ItemTypeCode"]),
                            ItemTypeDescription = reader["ItemTypeDescription"].ToString(),
                            ItemCode = reader["ItemCode"].ToString(),
                            ItemDescription = reader["ItemDescription"].ToString(),
                            UnitCode = reader["UnitCode"].ToString(),
                            UnitDescription = reader["UnitDescription"].ToString(),
                            Quantity = Convert.ToDecimal(reader["Quantity"]),
                            Price = Convert.ToDecimal(reader["Price"]),
                            WarehouseCode = reader["WarehouseCode"].ToString(),
                            WarehouseDescription = reader["WarehouseDescription"].ToString(),
                            LineDescription = reader["LineDescription"].ToString(),
                            LineStatus = Convert.ToInt32(reader["LineStatus"]),
                            LineStatusDescription = reader["LineStatusDescription"].ToString()
                        });
                    }
                }

                return items;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Üretim fişi kalemleri getirilirken hata oluştu. Fiş No: {OrderNumber}", orderNumber);
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<ProductionOrderResponse>> GetProductionOrdersAsync(
            DateTime? startDate = null, 
            DateTime? endDate = null, 
            string warehouseCode = null)
        {
            try
            {
                var whereConditions = new List<string>();
                var parameters = new List<SqlParameter>();

                if (startDate.HasValue)
                {
                    whereConditions.Add("trInner.OperationDate >= @StartDate");
                    parameters.Add(new SqlParameter("@StartDate", startDate.Value.Date));
                }
                else
                {
                    // Default son 30 gün
                    whereConditions.Add("trInner.OperationDate >= @StartDate");
                    parameters.Add(new SqlParameter("@StartDate", DateTime.Now.AddDays(-30).Date));
                }

                if (endDate.HasValue)
                {
                    whereConditions.Add("trInner.OperationDate <= @EndDate");
                    parameters.Add(new SqlParameter("@EndDate", endDate.Value.Date.AddDays(1).AddSeconds(-1)));
                }
                else
                {
                    // Default bugün
                    whereConditions.Add("trInner.OperationDate <= @EndDate");
                    parameters.Add(new SqlParameter("@EndDate", DateTime.Now.Date.AddDays(1).AddSeconds(-1)));
                }

                if (!string.IsNullOrEmpty(warehouseCode))
                {
                    whereConditions.Add("trInner.WarehouseCode = @WarehouseCode");
                    parameters.Add(new SqlParameter("@WarehouseCode", warehouseCode));
                }

                string whereClause = whereConditions.Count > 0 ? $"AND {string.Join(" AND ", whereConditions)}" : string.Empty;

                var query = $@"
                SELECT * FROM (
                SELECT    OrderNumber = trInner.InnerRef
                        , OperationDate
                        , OperationTime
                        , InnerProcessCode
                        , InnerProcessDescription= ISNULL((SELECT InnerProcessDescription FROM bsInnerProcessDesc WITH(NOLOCK) WHERE bsInnerProcessDesc.InnerProcessCode = trInner.InnerProcessCode AND bsInnerProcessDesc.LangCode = N'TR'), SPACE(0))
                        
                        , Series
                        , SeriesNumber
                        , Description

                        , CompanyCode
                        , OfficeCode
                        , OfficeDescription= ISNULL((SELECT OfficeDescription FROM cdOfficeDesc WITH(NOLOCK) WHERE cdOfficeDesc.OfficeCode = trInner.OfficeCode AND cdOfficeDesc.LangCode = N'TR'), SPACE(0))
                        , StoreCode
                        , StoreDescription= ISNULL((SELECT CurrAccDescription FROM cdCurrAccDesc WITH(NOLOCK) WHERE cdCurrAccDesc.CurrAccTypeCode = trInner.StoreTypeCode AND cdCurrAccDesc.CurrAccCode = trInner.StoreCode AND cdCurrAccDesc.LangCode = N'TR') ,SPACE(0)) 
                        , CustomerCode= trInner.CurrAccCode  
                        , CustomerDescription= ISNULL((SELECT CurrAccDescription FROM cdCurrAccDesc WITH(NOLOCK) WHERE cdCurrAccDesc.CurrAccTypeCode = trInner.CurrAccTypeCode AND cdCurrAccDesc.CurrAccCode = trInner.CurrAccCode AND cdCurrAccDesc.LangCode = N'TR') ,SPACE(0))
                        , WarehouseCode
                        , WarehouseDescription= ISNULL((SELECT WarehouseDescription FROM cdWarehouseDesc WITH(NOLOCK) WHERE cdWarehouseDesc.WarehouseCode = trInner.WarehouseCode AND cdWarehouseDesc.LangCode= N'TR'), SPACE(0))
                        
                        , Status
                        , StatusDescription = CASE trInner.Status 
                                            WHEN 0 THEN N'Bekliyor' 
                                            WHEN 1 THEN N'Tamamlandı' 
                                            WHEN 2 THEN N'İptal' 
                                            ELSE N'Bilinmiyor' END
                        , CreatedBy
                        , CreatedDate
                        , ModifiedBy
                        , ModifiedDate
                        , ApprovedBy
                        , ApprovedDate
                        , CanceledBy
                        , CanceledDate
                        , IsLocked = CASE WHEN EXISTS (SELECT 1 FROM trInnerLock WITH(NOLOCK) WHERE trInnerLock.InnerRef = trInner.InnerRef AND trInnerLock.IsActive = 1) THEN 1 ELSE 0 END
                        , TotalQty = ISNULL((SELECT SUM(Quantity) FROM trInnerLine WITH(NOLOCK) WHERE trInnerLine.InnerRefID = trInner.InnerRefID), 0)
                FROM trInner WITH(NOLOCK)
                WHERE trInner.InnerProcessCode = 'PR' -- Üretim Fişi
                {whereClause}
                ) Query WHERE CompanyCode = 1
                ORDER BY OperationDate DESC, OperationTime DESC
                ";

                var orders = new List<ProductionOrderResponse>();

                using (var reader = await _context.ExecuteReaderAsync(query, parameters.ToArray()))
                {
                    while (await reader.ReadAsync())
                    {
                        orders.Add(new ProductionOrderResponse
                        {
                            OrderNumber = reader["OrderNumber"].ToString(),
                            OperationDate = Convert.ToDateTime(reader["OperationDate"]),
                            OperationTime = reader["OperationTime"].ToString(),
                            WarehouseCode = reader["WarehouseCode"].ToString(),
                            WarehouseDescription = reader["WarehouseDescription"].ToString(),
                            Description = reader["Description"].ToString(),
                            Status = Convert.ToInt32(reader["Status"]),
                            StatusDescription = reader["StatusDescription"].ToString(),
                            CreatedBy = reader["CreatedBy"].ToString(),
                            CreatedDate = reader["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedDate"]) : (DateTime?)null,
                            ModifiedBy = reader["ModifiedBy"].ToString(),
                            ModifiedDate = reader["ModifiedDate"] != DBNull.Value ? Convert.ToDateTime(reader["ModifiedDate"]) : (DateTime?)null,
                            ApprovedBy = reader["ApprovedBy"].ToString(),
                            ApprovedDate = reader["ApprovedDate"] != DBNull.Value ? Convert.ToDateTime(reader["ApprovedDate"]) : (DateTime?)null,
                            CanceledBy = reader["CanceledBy"].ToString(),
                            CanceledDate = reader["CanceledDate"] != DBNull.Value ? Convert.ToDateTime(reader["CanceledDate"]) : (DateTime?)null,
                            IsLocked = Convert.ToBoolean(reader["IsLocked"])
                        });
                    }
                }

                return orders;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Üretim fişi listesi getirilirken hata oluştu");
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<ProductionOrderResponse> GetProductionOrderByNumberAsync(string orderNumber)
        {
            try
            {
                if (string.IsNullOrEmpty(orderNumber))
                {
                    throw new ArgumentNullException(nameof(orderNumber), "Üretim fiş numarası boş olamaz.");
                }

                var parameters = new[]
                {
                    new SqlParameter("@OrderNumber", orderNumber)
                };

                var query = $@"
                SELECT    OrderNumber = trInner.InnerRef
                        , OperationDate
                        , OperationTime
                        , InnerProcessCode
                        , InnerProcessDescription= ISNULL((SELECT InnerProcessDescription FROM bsInnerProcessDesc WITH(NOLOCK) WHERE bsInnerProcessDesc.InnerProcessCode = trInner.InnerProcessCode AND bsInnerProcessDesc.LangCode = N'TR'), SPACE(0))
                        
                        , Series
                        , SeriesNumber
                        , Description

                        , CompanyCode
                        , OfficeCode
                        , OfficeDescription= ISNULL((SELECT OfficeDescription FROM cdOfficeDesc WITH(NOLOCK) WHERE cdOfficeDesc.OfficeCode = trInner.OfficeCode AND cdOfficeDesc.LangCode = N'TR'), SPACE(0))
                        , StoreCode
                        , StoreDescription= ISNULL((SELECT CurrAccDescription FROM cdCurrAccDesc WITH(NOLOCK) WHERE cdCurrAccDesc.CurrAccTypeCode = trInner.StoreTypeCode AND cdCurrAccDesc.CurrAccCode = trInner.StoreCode AND cdCurrAccDesc.LangCode = N'TR') ,SPACE(0)) 
                        , CustomerCode= trInner.CurrAccCode  
                        , CustomerDescription= ISNULL((SELECT CurrAccDescription FROM cdCurrAccDesc WITH(NOLOCK) WHERE cdCurrAccDesc.CurrAccTypeCode = trInner.CurrAccTypeCode AND cdCurrAccDesc.CurrAccCode = trInner.CurrAccCode AND cdCurrAccDesc.LangCode = N'TR') ,SPACE(0))
                        , WarehouseCode
                        , WarehouseDescription= ISNULL((SELECT WarehouseDescription FROM cdWarehouseDesc WITH(NOLOCK) WHERE cdWarehouseDesc.WarehouseCode = trInner.WarehouseCode AND cdWarehouseDesc.LangCode= N'TR'), SPACE(0))
                        
                        , Status
                        , StatusDescription = CASE trInner.Status 
                                            WHEN 0 THEN N'Bekliyor' 
                                            WHEN 1 THEN N'Tamamlandı' 
                                            WHEN 2 THEN N'İptal' 
                                            ELSE N'Bilinmiyor' END
                        , CreatedBy
                        , CreatedDate
                        , ModifiedBy
                        , ModifiedDate
                        , ApprovedBy
                        , ApprovedDate
                        , CanceledBy
                        , CanceledDate
                        , IsLocked = CASE WHEN EXISTS (SELECT 1 FROM trInnerLock WITH(NOLOCK) WHERE trInnerLock.InnerRef = trInner.InnerRef AND trInnerLock.IsActive = 1) THEN 1 ELSE 0 END
                        , TotalQty = ISNULL((SELECT SUM(Quantity) FROM trInnerLine WITH(NOLOCK) WHERE trInnerLine.InnerRefID = trInner.InnerRefID), 0)
                FROM trInner WITH(NOLOCK)
                WHERE trInner.InnerRef = @OrderNumber AND trInner.InnerProcessCode = 'PR'
                ";

                ProductionOrderResponse order = null;

                using (var reader = await _context.ExecuteReaderAsync(query, parameters))
                {
                    if (await reader.ReadAsync())
                    {
                        order = new ProductionOrderResponse
                        {
                            OrderNumber = reader["OrderNumber"].ToString(),
                            OperationDate = Convert.ToDateTime(reader["OperationDate"]),
                            OperationTime = reader["OperationTime"].ToString(),
                            WarehouseCode = reader["WarehouseCode"].ToString(),
                            WarehouseDescription = reader["WarehouseDescription"].ToString(),
                            Description = reader["Description"].ToString(),
                            Status = Convert.ToInt32(reader["Status"]),
                            StatusDescription = reader["StatusDescription"].ToString(),
                            CreatedBy = reader["CreatedBy"].ToString(),
                            CreatedDate = reader["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedDate"]) : (DateTime?)null,
                            ModifiedBy = reader["ModifiedBy"].ToString(),
                            ModifiedDate = reader["ModifiedDate"] != DBNull.Value ? Convert.ToDateTime(reader["ModifiedDate"]) : (DateTime?)null,
                            ApprovedBy = reader["ApprovedBy"].ToString(),
                            ApprovedDate = reader["ApprovedDate"] != DBNull.Value ? Convert.ToDateTime(reader["ApprovedDate"]) : (DateTime?)null,
                            CanceledBy = reader["CanceledBy"].ToString(),
                            CanceledDate = reader["CanceledDate"] != DBNull.Value ? Convert.ToDateTime(reader["CanceledDate"]) : (DateTime?)null,
                            IsLocked = Convert.ToBoolean(reader["IsLocked"])
                        };
                    }
                }

                if (order == null)
                {
                    throw new InvalidOperationException($"Üretim fişi bulunamadı: {orderNumber}");
                }

                return order;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Üretim fişi detayları getirilirken hata oluştu. Fiş No: {OrderNumber}", orderNumber);
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<ProductionOrderDetailResponse> GetProductionOrderDetailAsync(string orderNumber)
        {
            try
            {
                var order = await GetProductionOrderByNumberAsync(orderNumber);
                var items = await GetProductionOrderItemsAsync(orderNumber);

                var detail = new ProductionOrderDetailResponse
                {
                    // Temel özellikleri ProductionOrderResponse'dan kopyala
                    OrderNumber = order.OrderNumber,
                    OperationDate = order.OperationDate,
                    OperationTime = order.OperationTime,
                    Series = order.Series,
                    SeriesNumber = order.SeriesNumber,
                    InnerProcessCode = order.InnerProcessCode,
                    CompanyCode = order.CompanyCode,
                    OfficeCode = order.OfficeCode,
                    WarehouseCode = order.WarehouseCode,
                    WarehouseDescription = order.WarehouseDescription,
                    CustomerCode = order.CustomerCode,
                    CustomerDescription = order.CustomerDescription,
                    TotalQty = order.TotalQty,
                    Description = order.Description,
                    Status = order.Status,
                    StatusDescription = order.StatusDescription,
                    IsLocked = order.IsLocked,
                    CreatedBy = order.CreatedBy,
                    CreatedDate = order.CreatedDate,
                    ModifiedBy = order.ModifiedBy,
                    ModifiedDate = order.ModifiedDate,
                    ApprovedBy = order.ApprovedBy,
                    ApprovedDate = order.ApprovedDate,
                    CanceledBy = order.CanceledBy,
                    CanceledDate = order.CanceledDate,
                    
                    // Kalem listesini ekle
                    Items = items.ToList()
                };

                return detail;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Üretim fişi detayları getirilirken hata oluştu. Fiş No: {OrderNumber}", orderNumber);
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<string> GetNextProductionOrderNumberAsync()
        {
            try
            {
                var query = @"
                    DECLARE @NextNumber VARCHAR(50)
                    EXEC sp_GetNextInnerRef 'PR', @NextNumber OUTPUT
                    SELECT @NextNumber AS NextNumber
                ";

                string nextNumber = null;

                using (var reader = await _context.ExecuteReaderAsync(query))
                {
                    if (await reader.ReadAsync())
                    {
                        nextNumber = reader["NextNumber"].ToString();
                    }
                }

                if (string.IsNullOrEmpty(nextNumber))
                {
                    throw new InvalidOperationException("Yeni üretim fiş numarası oluşturulamadı.");
                }

                return nextNumber;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Yeni üretim fiş numarası oluşturulurken hata oluştu");
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<ProductionOrderResponse> CreateProductionOrderAsync(ProductionOrderRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "Üretim fişi isteği boş olamaz.");
            }

            if (request.Items == null || request.Items.Count == 0)
            {
                throw new ArgumentException("Üretim fişinde en az bir kalem olmalıdır.", nameof(request.Items));
            }

            // ErpDbContext'in GetConnectionAsync metodunu kullanarak bağlantı oluşturuyoruz
            // Bu metot private olduğu için reflection kullanmamız gerekiyor
            var connectionMethod = typeof(ErpDbContext).GetMethod("GetConnectionAsync", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            
            if (connectionMethod == null)
            {
                throw new InvalidOperationException("ErpDbContext.GetConnectionAsync metodu bulunamadı.");
            }

            var task = connectionMethod.Invoke(_context, null) as Task<SqlConnection>;
            if (task == null)
            {
                throw new InvalidOperationException("ErpDbContext.GetConnectionAsync metodu çağrılamadı.");
            }

            var connection = await task;

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string orderNumber = null;

                    // Yeni üretim fiş numarası al
                    orderNumber = await GetNextProductionOrderNumberAsync();

                    // İşlem tarihi
                    var operationDate = request.OperationDate ?? DateTime.Now.Date;

                    // Üretim fişi başlık kaydı
                    var headerParameters = new[]
                    {
                        new SqlParameter("@InnerRef", orderNumber),
                        new SqlParameter("@InnerProcessCode", "PR"),
                        new SqlParameter("@OperationDate", operationDate),
                        new SqlParameter("@OperationTime", DateTime.Now.TimeOfDay),
                        new SqlParameter("@WarehouseCode", request.WarehouseCode),
                        new SqlParameter("@Description", request.Description ?? string.Empty),
                        new SqlParameter("@CustomerCode", request.CustomerCode ?? string.Empty),
                        new SqlParameter("@CreatedBy", "API"),
                        new SqlParameter("@CreatedDate", DateTime.Now)
                    };

                    var headerQuery = @"
                        INSERT INTO trInner (
                            InnerRef, InnerProcessCode, OperationDate, OperationTime, 
                            CompanyCode, OfficeCode, WarehouseCode, 
                            CurrAccTypeCode, CurrAccCode, Description, 
                            Status, CreatedBy, CreatedDate
                        )
                        VALUES (
                            @InnerRef, @InnerProcessCode, @OperationDate, @OperationTime, 
                            1, '01', @WarehouseCode, 
                            'C', @CustomerCode, @Description, 
                            0, @CreatedBy, @CreatedDate
                        );
                        
                        SELECT SCOPE_IDENTITY() AS InnerRefID;
                    ";

                    int innerRefID;
                    using (var command = new SqlCommand(headerQuery, connection, transaction))
                    {
                        command.Parameters.AddRange(headerParameters);
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (!await reader.ReadAsync())
                            {
                                throw new InvalidOperationException("Üretim fişi başlık kaydı oluşturulamadı.");
                            }

                            innerRefID = Convert.ToInt32(reader["InnerRefID"]);
                        }
                    }

                    // Üretim fişi kalemleri
                    int sortOrder = 1;
                    foreach (var item in request.Items)
                    {
                        var lineParameters = new[]
                        {
                            new SqlParameter("@InnerRefID", innerRefID),
                            new SqlParameter("@SortOrder", item.SortOrder > 0 ? item.SortOrder : sortOrder),
                            new SqlParameter("@ItemTypeCode", item.ItemTypeCode),
                            new SqlParameter("@ItemCode", item.ItemCode),
                            new SqlParameter("@UnitCode", item.UnitCode),
                            new SqlParameter("@Quantity", SqlDbType.Float) { Value = item.Quantity }, // Adet veya metre olarak virgüllü sayı olabilir (2,34 veya 45,34 gibi)
                            new SqlParameter("@Price", item.Price), // Birim fiyat
                            new SqlParameter("@WarehouseCode", !string.IsNullOrEmpty(item.WarehouseCode) ? item.WarehouseCode : request.WarehouseCode),
                            new SqlParameter("@LineDescription", item.LineDescription ?? string.Empty),
                            new SqlParameter("@CurrencyCode", item.CurrencyCode ?? "TRY"), // Para birimi, default TRY
                            new SqlParameter("@CostPrice", item.CostPrice), // Birim maliyet
                            new SqlParameter("@CostAmount", item.CostAmount), // Toplam maliyet tutarı
                            new SqlParameter("@CreatedBy", "API"),
                            new SqlParameter("@CreatedDate", DateTime.Now)
                        };

                        var lineQuery = @"
                            INSERT INTO trInnerLine (
                                InnerRefID, SortOrder, ItemTypeCode, ItemCode, 
                                UnitCode, Quantity, Price, WarehouseCode, 
                                LineDescription, LineStatus, CurrencyCode, CostPrice, CostAmount,
                                CreatedBy, CreatedDate
                            )
                            VALUES (
                                @InnerRefID, @SortOrder, @ItemTypeCode, @ItemCode, 
                                @UnitCode, @Quantity, @Price, @WarehouseCode, 
                                @LineDescription, 0, @CurrencyCode, @CostPrice, @CostAmount,
                                @CreatedBy, @CreatedDate
                            );
                        ";

                        using (var command = new SqlCommand(lineQuery, connection, transaction))
                        {
                            command.Parameters.AddRange(lineParameters);
                            await command.ExecuteNonQueryAsync();
                        }
                        
                        // Üretim emri için stok giriş hareketi oluştur (OP işleminde depoya giriş olur)
                        _logger.LogInformation("Üretim emri için stok giriş hareketi oluşturuluyor. Ürün: {ItemCode}, Depo: {WarehouseCode}, Miktar: {Quantity}",
                            item.ItemCode, !string.IsNullOrEmpty(item.WarehouseCode) ? item.WarehouseCode : request.WarehouseCode, item.Quantity);
                        
                        Guid stockId = Guid.NewGuid();
                        _logger.LogInformation("Stok giriş hareketi için StockID oluşturuldu: {StockID}", stockId);
                        
                        var stockQuery = @"
                            INSERT INTO trStock (
                                StockID, CompanyCode, TransTypeCode,
                                ProcessCode, InnerProcessCode, IsReturn,
                                DocumentDate, DocumentTime, OperationDate,
                                OperationTime, DocumentNumber, ItemCode,
                                ItemTypeCode, ColorCode, ItemDim1Code,
                                ItemDim2Code, ItemDim3Code, CurrAccTypeCode,
                                CurrAccCode, SubCurrAccID, OfficeCode,
                                WarehouseCode, In_Qty1, In_Qty2,
                                Out_Qty1, Out_Qty2, FromOfficeCode,
                                FromWarehouseCode, LineDescription, ApplicationCode,
                                ApplicationID, LocalCurrencyCode, DocCurrencyCode,
                                StoreCode, StoreTypeCode, FromStoreCode,
                                FromStoreTypeCode, BatchCode, SectionCode,
                                ManufactureDate, ExpiryDate, CreatedUserName,
                                CreatedDate, LastUpdatedUserName, LastUpdatedDate
                            ) VALUES (
                                @StockID, 1, 1,
                                '', 'OP', 0,
                                @OperationDate, @OperationTime, @OperationDate,
                                @OperationTime, @DocumentNumber, @ItemCode,
                                @ItemTypeCode, @ColorCode, @ItemDim1Code,
                                @ItemDim2Code, @ItemDim3Code, 3,
                                '', NULL, 'M',
                                @WarehouseCode, @In_Qty1, @In_Qty2,
                                @Out_Qty1, @Out_Qty2, 'M',
                                '', @LineDescription, 'Inner',
                                @ApplicationID, @LocalCurrencyCode, @DocCurrencyCode,
                                '', 5, '',
                                5, @BatchCode, NULL,
                                @ManufactureDate, @ExpiryDate, @CreatedBy,
                                GETDATE(), @CreatedBy, GETDATE()
                            )
                        ";
                        
                        var stockParameters = new[]
                        {
                            new SqlParameter("@StockID", stockId),
                            new SqlParameter("@OperationDate", request.OrderDate ?? DateTime.Now),
                            new SqlParameter("@OperationTime", (request.OrderDate ?? DateTime.Now).TimeOfDay),
                            new SqlParameter("@DocumentNumber", orderNumber),
                            new SqlParameter("@ItemCode", item.ItemCode),
                            new SqlParameter("@ItemTypeCode", item.ItemTypeCode),
                            new SqlParameter("@ColorCode", item.ColorCode ?? string.Empty),
                            new SqlParameter("@ItemDim1Code", item.ItemDim1Code ?? string.Empty), // Boş string olarak bırakılmalı, "0" ile değiştirilmemeli
                            new SqlParameter("@ItemDim2Code", item.ItemDim2Code ?? string.Empty),
                            new SqlParameter("@ItemDim3Code", item.ItemDim3Code ?? string.Empty),
                            new SqlParameter("@WarehouseCode", !string.IsNullOrEmpty(item.WarehouseCode) ? item.WarehouseCode : request.WarehouseCode),
                            new SqlParameter("@In_Qty1", SqlDbType.Float) { Value = item.Quantity }, // Üretim emrinde depoya giriş olur (adet veya metre olarak virgüllü sayı olabilir)
                            new SqlParameter("@In_Qty2", SqlDbType.Float) { Value = 0 }, // İkinci giriş miktarı sıfır
                            new SqlParameter("@Out_Qty1", SqlDbType.Float) { Value = 0 }, // Üretim emrinde depoya giriş olduğu için çıkış sıfır
                            new SqlParameter("@Out_Qty2", SqlDbType.Float) { Value = 0 }, // İkinci çıkış miktarı sıfır
                            new SqlParameter("@LineDescription", item.LineDescription ?? string.Empty),
                            new SqlParameter("@ApplicationID", innerRefID),
                            new SqlParameter("@LocalCurrencyCode", item.CurrencyCode ?? "TRY"), // Para birimi, default TRY
                            new SqlParameter("@DocCurrencyCode", item.CurrencyCode ?? "TRY"), // Para birimi, default TRY
                            new SqlParameter("@BatchCode", string.Empty),
                            new SqlParameter("@ManufactureDate", DateTime.Parse("1900-01-01")),
                            new SqlParameter("@ExpiryDate", DateTime.Parse("1900-01-01")),
                            new SqlParameter("@CreatedBy", "API")
                        };
                        
                        using (var stockCommand = new SqlCommand(stockQuery, connection, transaction))
                        {
                            stockCommand.Parameters.AddRange(stockParameters);
                            await stockCommand.ExecuteNonQueryAsync();
                            _logger.LogInformation("Stok giriş hareketi başarıyla oluşturuldu. StockID: {StockID}", stockId);
                        }
                        
                        sortOrder++;
                    }

                    // İşlemi tamamla
                    transaction.Commit();

                    // Oluşturulan üretim fişini döndür
                    return await GetProductionOrderByNumberAsync(orderNumber);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    _logger.LogError(ex, "Üretim fişi oluşturulurken hata oluştu");
                    throw;
                }
                finally
                {
                    // Bağlantıyı kapat
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<WarehouseResponse>> GetWarehousesAsync()
        {
            try
            {
                var query = @"
                    SELECT 
                        WarehouseCode, 
                        WarehouseDescription = ISNULL((SELECT WarehouseDescription FROM cdWarehouseDesc WITH(NOLOCK) WHERE cdWarehouseDesc.WarehouseCode = cdWarehouse.WarehouseCode AND cdWarehouseDesc.LangCode = N'TR'), SPACE(0))
                    FROM cdWarehouse WITH(NOLOCK)
                    WHERE IsActive = 1
                    ORDER BY WarehouseCode
                ";

                var warehouses = new List<WarehouseResponse>();

                using (var reader = await _context.ExecuteReaderAsync(query))
                {
                    while (await reader.ReadAsync())
                    {
                        warehouses.Add(new WarehouseResponse
                        {
                            WarehouseCode = reader["WarehouseCode"].ToString(),
                            WarehouseDescription = reader["WarehouseDescription"].ToString()
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
    }
}
