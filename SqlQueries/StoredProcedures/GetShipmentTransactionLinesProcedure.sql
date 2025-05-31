-- GetShipmentTransactionLines Stored Procedure
-- Sevkiyat işlem satırlarını getiren stored procedure

 
/****** Object:  StoredProcedure [dbo].[qry_GetShipmentTransactionLines]    Script Date: 5/9/2025 2:11:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[qry_GetShipmentTransactionLines]  (
		  @ProcessFlowCode tinyint, @LangCode char5 = 'TR'
)

AS
	BEGIN TRY

		SELECT 
		     ProcessCode				= trShipmentHeader.ProcessCode
			, ShippingNumber			= trShipmentHeader.ShippingNumber
			, ShippingDate				= trShipmentHeader.ShippingDate
			, ShippingTime				= trShipmentHeader.ShippingTime
			, CompanyCode				= trShipmentHeader.CompanyCode
			, OfficeCode				= trShipmentHeader.OfficeCode
			, StoreCode					= trShipmentHeader.StoreCode
			, WarehouseCode				= trShipmentHeader.WarehouseCode
			, ToWarehouseCode			= trShipmentHeader.ToWarehouseCode
			, CurrAccTypeCode			= trShipmentHeader.CurrAccTypeCode
			, CurrAccCode				= trShipmentHeader.CurrAccCode
			, SubCurrAccID				= trShipmentHeader.SubCurrAccID
			, ContactID					= trShipmentHeader.ContactID
			, Description				= trShipmentHeader.Description
			, CustomerASNNumber			= trShipmentHeader.CustomerASNNumber 
			, IsCompleted				= trShipmentHeader.IsCompleted
			, IsLocked					= trShipmentHeader.IsLocked
			, IsPrinted					= trShipmentHeader.IsPrinted
			, ExportFileNumber			= LTRIM(RTRIM(trShipmentHeader.ExportFileNumber))	
			, ImportFileNumber			= LTRIM(RTRIM(trShipmentHeader.ImportFileNumber))	

			, OrderNumber				= ISNULL(trOrderHeader.OrderNumber		, SPACE(0))	
			, OrderDate					= ISNULL(trOrderHeader.OrderDate		, SPACE(0))
			, OrderTime					= ISNULL(trOrderHeader.OrderTime		, '00:00:00')
			, OrdererOfficeCode			= ISNULL(trOrderHeader.OrdererOfficeCode, SPACE(0))
			, OrdererStoreCode			= ISNULL(trOrderHeader.OrdererStoreCode	, SPACE(0))
			, TDisRate1					= ISNULL(trOrderHeader.TDisRate1		, 0)
			, TDisRate2					= ISNULL(trOrderHeader.TDisRate2		, 0)
			, TDisRate3					= ISNULL(trOrderHeader.TDisRate3		, 0)
			, TDisRate4					= ISNULL(trOrderHeader.TDisRate4		, 0)
			, TDisRate5					= ISNULL(trOrderHeader.TDisRate5		, 0)
			, LocalCurrencyCode			= ISNULL(trOrderHeader.LocalCurrencyCode, SPACE(0))	
			, GLTypeCode				= ISNULL(trOrderHeader.GLTypeCode		, SPACE(0))   

			, ItemTypeCode				= trShipmentLine.ItemTypeCode
			, ItemCode					= trShipmentLine.ItemCode
			, ItemDescription			= ISNULL(cdItemDesc.ItemDescription, SPACE(0))
			, ItemDim1Code				= trShipmentLine.ItemDim1Code
			, ItemDim2Code				= trShipmentLine.ItemDim2Code
			, ItemDim3Code				= trShipmentLine.ItemDim3Code
			, ColorCode					= trShipmentLine.ColorCode
			, ColorDescription			= ISNULL(cdColorDesc.ColorDescription, SPACE(0))		
			, LineDescription			= trShipmentLine.LineDescription
			, OrderQty1					= ISNULL(trOrderLine.Qty1, 0)
			, OrderQty2					= ISNULL(trOrderLine.Qty2, 0)
			, Qty1						= trShipmentLine.Qty1
			, Qty2						= trShipmentLine.Qty2
			, UsedBarcode				= trShipmentLine.UsedBarcode	
			
			, ProductSerialNumber		= COALESCE(stItemSerialNumber_DS.SerialNumber, stItemSerialNumber.SerialNumber, SPACE(0))
			
			, BatchCode					= ISNULL(trShipmentLine.BatchCode, SPACE(0))
			, SectionCode				= ISNULL(trShipmentLine.SectionCode, SPACE(0))
			, ITAtt01					= ISNULL(ITAtt01, SPACE(0))
			, ITAtt02					= ISNULL(ITAtt02, SPACE(0))
			, ITAtt03					= ISNULL(ITAtt03, SPACE(0))
			, ITAtt04					= ISNULL(ITAtt04, SPACE(0))
			, ITAtt05					= ISNULL(ITAtt05, SPACE(0))
			, ITAtt06					= ISNULL(ITAtt06, SPACE(0))
			, ITAtt07					= ISNULL(ITAtt07, SPACE(0))
			, ITAtt08					= ISNULL(ITAtt08, SPACE(0))
			, ITAtt09					= ISNULL(ITAtt09, SPACE(0))
			, ITAtt10					= ISNULL(ITAtt10, SPACE(0))
			, IsSalesViaInternet		= ISNULL(trOrderHeader.IsSalesViaInternet, 0)
			, SalesURL					= ISNULL(tpOrdersViaInternetInfo.SalesURL, SPACE(0))
			, PaymentTypeCode			= ISNULL(tpOrdersViaInternetInfo.PaymentTypeCode, SPACE(0))
			, PaymentTypeDescription	= ISNULL(tpOrdersViaInternetInfo.PaymentTypeDescription, SPACE(0))
			, PaymentAgent				= ISNULL(tpOrdersViaInternetInfo.PaymentAgent, SPACE(0))
			, PaymentDate				= ISNULL(tpOrdersViaInternetInfo.PaymentDate, SPACE(0))
			, SendDate					= ISNULL(tpOrdersViaInternetInfo.SendDate, SPACE(0))
			, SendTime					= ISNULL(tpOrdersViaInternetInfo.SendTime, '00:00:00')
			, SalespersonCode			= trShipmentLine.SalespersonCode
			, ReturnReasonCode			= trShipmentLine.ReturnReasonCode
			, CostCenterCode			= trOrderLine.CostCenterCode
			, LDisRate1					= ISNULL(trOrderLine.LDisRate1, 0)	
			, LDisRate2					= ISNULL(trOrderLine.LDisRate2, 0)
			, LDisRate3					= ISNULL(trOrderLine.LDisRate3, 0)
			, LDisRate4					= ISNULL(trOrderLine.LDisRate4, 0)
			, LDisRate5					= ISNULL(trOrderLine.LDisRate5, 0)
			, ActualPrice				= ISNULL(trOrderLine.Price, 0)
			, ActualPriceCurrencyCode	= ISNULL(trOrderLine.PriceCurrencyCode, SPACE(0))
			, ActualPriceExchangeRate	= ISNULL(trOrderLine.PriceExchangeRate, 0)
			, ShipmentPrice				= trShipmentLine.Price
			, ShipmentPriceCurrencyCode	= trShipmentLine.PriceCurrencyCode
			, VatCode					= ISNULL(trOrderLine.VatCode, 0)
			, PCTCode					= ISNULL(trOrderLine.PCTCode, 0)
			, VatRate					= ISNULL(trOrderLine.VatRate, 0)
			, PCTRate					= ISNULL(trOrderLine.PCTRate, 0)
			, PaymentPlanCode			= ISNULL(trOrderLine.PaymentPlanCode , SPACE(0))
			, PurchasePlanCode			= ISNULL(trOrderLine.PurchasePlanCode, SPACE(0))
			
			, DocCurrencyCode			= ISNULL(trOrderLine.DocCurrencyCode, SPACE(0))
			, LocCurrencyCode			= ISNULL(trOrderHeader.LocalCurrencyCode, SPACE(0))
			, ExchangeRate				= ISNULL(trOrderLineCurrencyLoc.ExchangeRate, 0)
			, LDiscount1				= ISNULL(trOrderLineCurrencyDoc.LDiscount1, 0)
			, LDiscount2				= ISNULL(trOrderLineCurrencyDoc.LDiscount2, 0)
			, LDiscount3				= ISNULL(trOrderLineCurrencyDoc.LDiscount3, 0)
			, LDiscount4				= ISNULL(trOrderLineCurrencyDoc.LDiscount4, 0)
			, LDiscount5				= ISNULL(trOrderLineCurrencyDoc.LDiscount5, 0)
			, TDiscount1				= ISNULL(trOrderLineCurrencyDoc.TDiscount1, 0)
			, TDiscount2				= ISNULL(trOrderLineCurrencyDoc.TDiscount2, 0)
			, TDiscount3				= ISNULL(trOrderLineCurrencyDoc.TDiscount3, 0)
			, TDiscount4				= ISNULL(trOrderLineCurrencyDoc.TDiscount4, 0)
			, TDiscount5				= ISNULL(trOrderLineCurrencyDoc.TDiscount5, 0)
			, Price						= ISNULL(trOrderLineCurrencyDoc.Price	  , 0)	
			, PriceVI					= ISNULL(trOrderLineCurrencyDoc.PriceVI	  , 0)
			, Amount					= ISNULL(trOrderLineCurrencyDoc.Amount	  , 0)
			, AmountVI					= ISNULL(trOrderLineCurrencyDoc.AmountVI  , 0)
			, TaxBase					= ISNULL(trOrderLineCurrencyDoc.TaxBase	  , 0)
			, VatAmount					= ISNULL(trOrderLineCurrencyDoc.Vat		  , 0)	
			, PCTAmount					= ISNULL(trOrderLineCurrencyDoc.Pct		  , 0)	
			, NetAmount					= ISNULL(trOrderLineCurrencyDoc.NetAmount , 0)	

			, ShipmentLineID			= trShipmentLine.ShipmentLineID
			, ShipmentHeaderID			= trShipmentLine.ShipmentHeaderID
			, PickingLineID				= trShipmentLine.PickingLineID
			, DispOrderLineID			= trShipmentLine.DispOrderLineID
			, ReserveLineID				= trShipmentLine.ReserveLineID
			, OrderAsnLineID			= trShipmentLine.OrderAsnLineID
			, OrderLineID				= trOrderLine.OrderLineID
			, OrderHeaderID				= trOrderLine.OrderHeaderID
			, SupportRequestHeaderID	= trOrderLine.SupportRequestHeaderID

		FROM #TransactionLineIDsForAutoTransaction TransactionLineIDs
				INNER JOIN trShipmentLine WITH(NOLOCK)
					ON trShipmentLine.ShipmentLineID = TransactionLineIDs.TransactionLineID
				INNER JOIN trShipmentHeader WITH(NOLOCK)
					ON trShipmentHeader.ShipmentHeaderID = trShipmentLine.ShipmentHeaderID


				LEFT OUTER JOIN stItemSerialNumber WITH(NOLOCK)
					ON stItemSerialNumber.ShipmentLineID = trShipmentLine.ShipmentLineID
					AND trShipmentHeader.ProcessCode <> 'DS'
							
				LEFT OUTER JOIN trStock WITH(NOLOCK)
					ON trStock.ApplicationCode = 'Shipm'
					AND trStock.ApplicationID = trShipmentLine.ShipmentLineID
					AND trStock.ProcessCode = 'DS'
					AND ((trStock.TransTypeCode = 2 AND trStock.IsReturn = 0)
						OR
						(trStock.TransTypeCode = 1 AND trStock.IsReturn = 1)
						)
				LEFT OUTER JOIN stItemSerialNumber AS stItemSerialNumber_DS WITH(NOLOCK)
					ON stItemSerialNumber_DS.ShipmentLineID = trStock.StockID

				LEFT OUTER JOIN trOrderLine WITH(NOLOCK)
					ON trOrderLine.OrderLineID = trShipmentLine.OrderLineID
				LEFT OUTER JOIN trOrderHeader WITH(NOLOCK)
					ON trOrderHeader.OrderHeaderID = trOrderLine.OrderHeaderID
				LEFT OUTER JOIN trOrderLineCurrency AS trOrderLineCurrencyDoc WITH (NOLOCK) 
					ON trOrderLineCurrencyDoc.OrderLineID = trOrderLine.OrderLineID 
					AND trOrderLineCurrencyDoc.CurrencyCode = trOrderLine.DocCurrencyCode 
				LEFT OUTER JOIN trOrderLineCurrency AS trOrderLineCurrencyLoc WITH (NOLOCK) 
					ON trOrderLineCurrencyLoc.OrderLineID = trOrderLine.OrderLineID 
					AND trOrderLineCurrencyLoc.CurrencyCode = trOrderHeader.LocalCurrencyCode  
				LEFT OUTER JOIN cdItemDesc WITH(NOLOCK)
					ON cdItemDesc.ITemTypeCode = trShipmentLine.ItemTypeCode
					AND cdItemDesc.ItemCode = trShipmentLine.ItemCode
					AND cdItemDesc.LangCode = @LangCode
				LEFT OUTER JOIN ShipmentITAttributesFilter
					ON ShipmentITAttributesFilter.ShipmentLineID = trShipmentLine.ShipmentLineID
				LEFT OUTER JOIN tpOrdersViaInternetInfo WITH(NOLOCK)
					ON tpOrdersViaInternetInfo.OrderHeaderID = trOrderLine.OrderHeaderID
				LEFT OUTER JOIN cdColorDesc WITH(NOLOCK)
					ON cdColorDesc.ColorCode = trShipmentLine.ColorCode
					AND cdColorDesc.LangCode = @LangCode


		SELECT   
			  PickingDate				= tpShipmentLinePickingDetails.PickingDate				
			, PackageNumber				= tpShipmentLinePickingDetails.PackageNumber				
			, PackagingTypeCode			= tpShipmentLinePickingDetails.PackagingTypeCode			
			, PackageBrandCode			= tpShipmentLinePickingDetails.PackageBrandCode			
			, PackageVolumeCode			= tpShipmentLinePickingDetails.PackageVolumeCode			
			, WeightUnitOfMeasureCode	= tpShipmentLinePickingDetails.WeightUnitOfMeasureCode	
			, PackedWeight				= tpShipmentLinePickingDetails.PackedWeight				
			, UnitOfMeasureCode			= tpShipmentLinePickingDetails.UnitOfMeasureCode			
			, Qty						= tpShipmentLinePickingDetails.Qty						
			, ShipmentHeaderID			= tpShipmentLinePickingDetails.ShipmentHeaderID
			, ShipmentLineID			= tpShipmentLinePickingDetails.ShipmentLineID
		FROM  #TransactionLineIDsForAutoTransaction TransactionLineIDs
				INNER JOIN tpShipmentLinePickingDetails WITH(NOLOCK)
					ON tpShipmentLinePickingDetails.ShipmentLineID = TransactionLineIDs.TransactionLineID
		
	END TRY
	BEGIN CATCH
		IF (XACT_STATE()) <> 0 	ROLLBACK TRANSACTION;
		DECLARE @ErrorMessage	NVARCHAR(4000)
		DECLARE @ErrorSeverity	INT
		DECLARE @ErrorState		INT

		SELECT  @ErrorMessage	= ERROR_MESSAGE(),
				@ErrorSeverity	= ERROR_SEVERITY(),
				@ErrorState		= ERROR_STATE()
			
		RAISERROR (@ErrorMessage,	-- Message text
				   @ErrorSeverity,	-- Severity
				   @ErrorState		-- State  
				   )
	END CATCH
