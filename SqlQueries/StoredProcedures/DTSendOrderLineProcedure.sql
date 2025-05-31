-- DTSendOrderLine Stored Procedure
-- Sipariş satırlarını veri transferi için hazırlayan stored procedure

 
/****** Object:  StoredProcedure [dbo].[qry_DTSendOrderLine]    Script Date: 5/9/2025 2:33:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[qry_DTSendOrderLine] (
		  @Suffix			Char60
		, @BarcodeTypeCode	Char20 = N''
		, @TransferName		Char100
		, @ElementName		Char100
)
AS
BEGIN TRY

	DECLARE @sql NVARCHAR(MAX)

	SELECT   
		  Suffix						= dtSendingData.Suffix
		, OrderNumber					= trOrderHeader.OrderNumber
		, OrderLineID					= trOrderLine.OrderLineID				
		, SortOrder						= trOrderLine.SortOrder					
		, ItemTypeCode					= trOrderLine.ItemTypeCode				
		, ItemCode						= trOrderLine.ItemCode					
		, ColorCode						= trOrderLine.ColorCode					
		, ItemDim1Code					= trOrderLine.ItemDim1Code				
		, ItemDim2Code					= trOrderLine.ItemDim2Code				
		, ItemDim3Code					= trOrderLine.ItemDim3Code				
		, Qty1							= trOrderLine.Qty1						
		, Qty2							= trOrderLine.Qty2						
		, CancelQty1					= trOrderLine.CancelQty1				
		, CancelQty2					= trOrderLine.CancelQty2				
		, CancelDate					= trOrderLine.CancelDate				
		, OrderCancelReasonCode			= trOrderLine.OrderCancelReasonCode		
		, ClosedDate					= trOrderLine.ClosedDate				 					
		, SalespersonCode				= trOrderLine.SalespersonCode			
		, PaymentPlanCode				= trOrderLine.PaymentPlanCode			
		, PurchasePlanCode				= trOrderLine.PurchasePlanCode			
		, DeliveryDate					= trOrderLine.DeliveryDate				
		, PlannedDateOfLading			= trOrderLine.PlannedDateOfLading		
		, LineDescription				= trOrderLine.LineDescription			
		, Barcode						= prItemBarcode.Barcode
		, UsedBarcode					= trOrderLine.UsedBarcode					
		, CostCenterCode				= trOrderLine.CostCenterCode				
		, VatCode						= trOrderLine.VatCode						
		, VatRate						= trOrderLine.VatRate						
		, PCTCode						= trOrderLine.PCTCode						
		, PCTRate						= trOrderLine.PCTRate						
		, LDisRate1						= trOrderLine.LDisRate1						
		, LDisRate2						= trOrderLine.LDisRate2						
		, LDisRate3						= trOrderLine.LDisRate3						
		, LDisRate4						= trOrderLine.LDisRate4						
		, LDisRate5						= trOrderLine.LDisRate5										
		, PriceCurrencyCode				= trOrderLine.PriceCurrencyCode				
		, PriceExchangeRate				= trOrderLine.PriceExchangeRate				
		, Price							= trOrderLine.Price							
		, PriceListLineID				= trOrderLine.PriceListLineID				
		, BaseProcessCode				= trOrderLine.BaseProcessCode				
		, BaseOrderNumber				= trOrderLine.BaseOrderNumber				
		, BaseCustomerTypeCode			= trOrderLine.BaseCustomerTypeCode			
		, BaseCustomerCode				= trOrderLine.BaseCustomerCode				
		, BaseSubCurrAccID				= trOrderLine.BaseSubCurrAccID				
		, BaseStoreCode					= trOrderLine.BaseStoreCode					
		, SupportRequestHeaderID		= NULL		
		, SurplusOrderQtyToleranceRate	= trOrderLine.SurplusOrderQtyToleranceRate	
		, OrderHeaderID					= trOrderLine.OrderHeaderID					
		, OrderLineSumID				= trOrderLine.OrderLineSumID				
		, OrderLineBOMID				= NULL
		--DocCurrencyCode
		, DocCurrencyCode				= trOrderLine.DocCurrencyCode			
		, DocExchangeRate				= trOrderLineCurrencyDoc.ExchangeRate			
		, DocRelationCurrencyCode	 	= trOrderLineCurrencyDoc.RelationCurrencyCode	
		, DocPriceVI					= trOrderLineCurrencyDoc.PriceVI				
		, DocAmountVI					= trOrderLineCurrencyDoc.AmountVI				
		, DocPrice						= trOrderLineCurrencyDoc.Price					
		, DocAmount						= trOrderLineCurrencyDoc.Amount					
		, DocLDiscount1					= trOrderLineCurrencyDoc.LDiscount1				
		, DocLDiscount2					= trOrderLineCurrencyDoc.LDiscount2				
		, DocLDiscount3					= trOrderLineCurrencyDoc.LDiscount3				
		, DocLDiscount4					= trOrderLineCurrencyDoc.LDiscount4				
		, DocLDiscount5					= trOrderLineCurrencyDoc.LDiscount5				
		, DocLDiscountVI1				= trOrderLineCurrencyDoc.LDiscountVI1			
		, DocLDiscountVI2				= trOrderLineCurrencyDoc.LDiscountVI2			
		, DocLDiscountVI3				= trOrderLineCurrencyDoc.LDiscountVI3			
		, DocLDiscountVI4				= trOrderLineCurrencyDoc.LDiscountVI4			
		, DocLDiscountVI5				= trOrderLineCurrencyDoc.LDiscountVI5			
		, DocTDiscount1					= trOrderLineCurrencyDoc.TDiscount1				
		, DocTDiscount2					= trOrderLineCurrencyDoc.TDiscount2				
		, DocTDiscount3					= trOrderLineCurrencyDoc.TDiscount3				
		, DocTDiscount4					= trOrderLineCurrencyDoc.TDiscount4				
		, DocTDiscount5					= trOrderLineCurrencyDoc.TDiscount5				
		, DocTDiscountVI1				= trOrderLineCurrencyDoc.TDiscountVI1			
		, DocTDiscountVI2				= trOrderLineCurrencyDoc.TDiscountVI2			
		, DocTDiscountVI3				= trOrderLineCurrencyDoc.TDiscountVI3			
		, DocTDiscountVI4				= trOrderLineCurrencyDoc.TDiscountVI4			
		, DocTDiscountVI5				= trOrderLineCurrencyDoc.TDiscountVI5			
		, DocTaxBase					= trOrderLineCurrencyDoc.TaxBase				
		, DocPct						= trOrderLineCurrencyDoc.Pct					
		, DocVat						= trOrderLineCurrencyDoc.Vat					
		, DocVatDeducation				= trOrderLineCurrencyDoc.VatDeducation			
		, DocNetAmount					= trOrderLineCurrencyDoc.NetAmount				
		--LocCurrencyCode
		, LocCurrencyCode				= trOrderLineCurrencyLoc.CurrencyCode			
		, LocExchangeRate				= trOrderLineCurrencyLoc.ExchangeRate			
		, LocRelationCurrencyCode	 	= trOrderLineCurrencyLoc.RelationCurrencyCode	
		, LocPriceVI					= trOrderLineCurrencyLoc.PriceVI				
		, LocAmountVI					= trOrderLineCurrencyLoc.AmountVI				
		, LocPrice						= trOrderLineCurrencyLoc.Price					
		, LocAmount						= trOrderLineCurrencyLoc.Amount					
		, LocLDiscount1					= trOrderLineCurrencyLoc.LDiscount1				
		, LocLDiscount2					= trOrderLineCurrencyLoc.LDiscount2				
		, LocLDiscount3					= trOrderLineCurrencyLoc.LDiscount3				
		, LocLDiscount4					= trOrderLineCurrencyLoc.LDiscount4				
		, LocLDiscount5					= trOrderLineCurrencyLoc.LDiscount5				
		, LocLDiscountVI1				= trOrderLineCurrencyLoc.LDiscountVI1			
		, LocLDiscountVI2				= trOrderLineCurrencyLoc.LDiscountVI2			
		, LocLDiscountVI3				= trOrderLineCurrencyLoc.LDiscountVI3			
		, LocLDiscountVI4				= trOrderLineCurrencyLoc.LDiscountVI4			
		, LocLDiscountVI5				= trOrderLineCurrencyLoc.LDiscountVI5			
		, LocTDiscount1					= trOrderLineCurrencyLoc.TDiscount1				
		, LocTDiscount2					= trOrderLineCurrencyLoc.TDiscount2				
		, LocTDiscount3					= trOrderLineCurrencyLoc.TDiscount3				
		, LocTDiscount4					= trOrderLineCurrencyLoc.TDiscount4				
		, LocTDiscount5					= trOrderLineCurrencyLoc.TDiscount5				
		, LocTDiscountVI1				= trOrderLineCurrencyLoc.TDiscountVI1			
		, LocTDiscountVI2				= trOrderLineCurrencyLoc.TDiscountVI2			
		, LocTDiscountVI3				= trOrderLineCurrencyLoc.TDiscountVI3			
		, LocTDiscountVI4				= trOrderLineCurrencyLoc.TDiscountVI4			
		, LocTDiscountVI5				= trOrderLineCurrencyLoc.TDiscountVI5			
		, LocTaxBase					= trOrderLineCurrencyLoc.TaxBase				
		, LocPct						= trOrderLineCurrencyLoc.Pct					
		, LocVat						= trOrderLineCurrencyLoc.Vat					
		, LocVatDeducation				= trOrderLineCurrencyLoc.VatDeducation			
		, LocNetAmount					= trOrderLineCurrencyLoc.NetAmount
		--ITAttribute
		, ITAtt01						= ITAtt01
		, ITAtt02						= ITAtt02
		, ITAtt03						= ITAtt03
		, ITAtt04						= ITAtt04
		, ITAtt05						= ITAtt05
		--OTAttribute
		, OTAtt01						= OTAtt01
		, OTAtt02						= OTAtt02
		, OTAtt03						= OTAtt03
		, OTAtt04						= OTAtt04
		, OTAtt05						= OTAtt05
		, IsValidated					= 1
		, MasterKey						= trOrderHeader.OrderNumber
	FROM   dtSendingData AS dtSendingData WITH(NOLOCK)
		INNER JOIN trOrderHeader WITH(NOLOCK)
				ON dtSendingData.ID = trOrderHeader.OrderHeaderID
		INNER JOIN trOrderLine WITH(NOLOCK) 
				ON trOrderHeader.OrderHeaderID = trOrderLine.OrderHeaderID 
		LEFT OUTER JOIN trOrderLineCurrency AS trOrderLineCurrencyDoc WITH (NOLOCK) 
				ON trOrderLineCurrencyDoc.OrderLineID = trOrderLine.OrderLineID 
				AND trOrderLine.DocCurrencyCode = trOrderLineCurrencyDoc.CurrencyCode   
		LEFT OUTER JOIN trOrderLineCurrency AS trOrderLineCurrencyLoc WITH (NOLOCK) 
				ON trOrderLineCurrencyLoc.OrderLineID = trOrderLine.OrderLineID 
				AND trOrderHeader.LocalCurrencyCode = trOrderLineCurrencyLoc.CurrencyCode  
		LEFT OUTER JOIN OrderITAttributesFilter 
				ON trOrderLine.OrderLineID = OrderITAttributesFilter.OrderLineID
		LEFT OUTER JOIN OrderOTAttributesFilter 
				ON trOrderLine.OrderLineID = OrderOTAttributesFilter.OrderLineID
		LEFT OUTER JOIN prItemBarcode WITH(NOLOCK)
				ON trOrderLine.ItemTypeCode = prItemBarcode.ItemTypeCode
				AND trOrderLine.ItemCode = prItemBarcode.ItemCode
				AND trOrderLine.ColorCode = prItemBarcode.ColorCode
				AND trOrderLine.ItemDim1Code = prItemBarcode.ItemDim1Code
				AND trOrderLine.ItemDim2Code = prItemBarcode.ItemDim2Code
				AND trOrderLine.ItemDim3Code = prItemBarcode.ItemDim3Code
				AND prItemBarcode.BarcodeTypeCode = @BarcodeTypeCode  
	WHERE dtSendingData.Suffix  = @Suffix
	AND	dtSendingData.TransferName = @TransferName

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
