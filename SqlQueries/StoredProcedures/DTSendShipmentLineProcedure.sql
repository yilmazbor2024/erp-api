-- DTSendShipmentLine Stored Procedure
-- Sevkiyat satırlarını veri transferi için hazırlayan stored procedure
 
/****** Object:  StoredProcedure [dbo].[qry_DTSendShipmentLine]    Script Date: 5/9/2025 2:17:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[qry_DTSendShipmentLine] (
		  @Suffix			Char60
		, @TransferName		Char100
		, @BarcodeTypeCode	Char20 = N''
		
)
AS
BEGIN TRY

	SELECT   
		  Suffix				= dtSendingData.Suffix
		-----------------------------------------------------------------
		, ProcessCode			= trShipmentHeader.ProcessCode
		, ShippingNumber		= trShipmentHeader.ShippingNumber
		, SortOrder				= trShipmentLine.SortOrder
		, ShipmentLineSumID		= trShipmentLine.ShipmentLineSumID
		-----------------------------------------------------------------
		, Barcode				= prItemBarcode.Barcode
		-----------------------------------------------------------------
		, ItemTypeCode			= trShipmentLine.ItemTypeCode
		, ItemCode				= trShipmentLine.ItemCode
		, ColorCode				= trShipmentLine.ColorCode
		, ItemDim1Code			= trShipmentLine.ItemDim1Code
		, ItemDim2Code			= trShipmentLine.ItemDim2Code
		, ItemDim3Code			= trShipmentLine.ItemDim3Code
		-----------------------------------------------------------------
		, Qty1					= trShipmentLine.Qty1
		, Qty2					= trShipmentLine.Qty2
		-----------------------------------------------------------------
		, BatchCode				= trShipmentLine.BatchCode
		, SectionCode			= trShipmentLine.SectionCode
		-----------------------------------------------------------------
		, SalespersonCode		= trShipmentLine.SalespersonCode			
		, PaymentPlanCode		= trShipmentLine.PaymentPlanCode			
		, PurchasePlanCode		= trShipmentLine.PurchasePlanCode		
		, ReturnReasonCode		= trShipmentLine.ReturnReasonCode		
		, LineDescription		= trShipmentLine.LineDescription			
		, UsedBarcode			= trShipmentLine.UsedBarcode				
		, OrderDeliveryDate		= trShipmentLine.OrderDeliveryDate			
		, DeliveryCompanyBarcode= trShipmentLine.DeliveryCompanyBarcode		
		, LogisticsPackageNumber= trShipmentLine.LogisticsPackageNumber		
		-----------------------------------------------------------------
		, ManufactureDate 		= trShipmentLine.ManufactureDate
		-----------------------------------------------------------------
		, ImportFileNumber		= trShipmentLine.ImportFileNumber
		, ExportFileNumber		= trShipmentLine.ExportFileNumber
		-----------------------------------------------------------------
		, PriceCurrencyCode		= trShipmentLine.PriceCurrencyCode	
		, Price					= trShipmentLine.Price	
		-----------------------------------------------------------------
		, ITAtt01				= ShipmentITAttributesFilter.ITAtt01
		, ITAtt02				= ShipmentITAttributesFilter.ITAtt02
		, ITAtt03				= ShipmentITAttributesFilter.ITAtt03
		, ITAtt04				= ShipmentITAttributesFilter.ITAtt04
		, ITAtt05				= ShipmentITAttributesFilter.ITAtt05
		-----------------------------------------------------------------
		, ShipmentHeaderID		= trShipmentLine.ShipmentHeaderID
		, ShipmentLineID		= trShipmentLine.ShipmentLineID
		, PriceListLineID		= trShipmentLine.PriceListLineID
		-----------------------------------------------------------------
		, IsValidated			= 1
		, MasterKey				= trShipmentHeader.ShipmentHeaderID
	FROM   dtSendingData WITH(NOLOCK)
		INNER JOIN trShipmentHeader WITH(NOLOCK)
			ON trShipmentHeader.ShipmentHeaderID = dtSendingData.ID  
		INNER JOIN trShipmentLine WITH(NOLOCK) 
			ON trShipmentLine.ShipmentHeaderID = trShipmentHeader.ShipmentHeaderID  
		LEFT OUTER JOIN ShipmentITAttributesFilter
			ON ShipmentITAttributesFilter.ShipmentLineID = trShipmentLine.ShipmentLineID
		LEFT OUTER JOIN prItemBarcode WITH(NOLOCK)
			ON prItemBarcode.ItemTypeCode = trShipmentLine.ItemTypeCode
			AND prItemBarcode.ItemCode = trShipmentLine.ItemCode
			AND prItemBarcode.ColorCode = trShipmentLine.ColorCode
			AND prItemBarcode.ItemDim1Code = trShipmentLine.ItemDim1Code
			AND prItemBarcode.ItemDim2Code = trShipmentLine.ItemDim2Code
			AND prItemBarcode.ItemDim3Code = trShipmentLine.ItemDim3Code
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
