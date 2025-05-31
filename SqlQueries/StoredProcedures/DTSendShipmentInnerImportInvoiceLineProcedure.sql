-- DTSendShipmentInnerImportInvoiceLine Stored Procedure
-- İç sevkiyat ithalat fatura satırlarını veri transferi için hazırlayan stored procedure

 
/****** Object:  StoredProcedure [dbo].[qry_DTSendShipmentInnerImportInvoiceLine]    Script Date: 5/9/2025 2:24:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[qry_DTSendShipmentInnerImportInvoiceLine] (
		  @Suffix			Char60
		, @TransferName		Char100
)
AS
BEGIN TRY

	SELECT   
		  Suffix				= dtSendingData.Suffix
		---------------------------------------------------------------
		, InnerProcessCode		= trInnerHeader.InnerProcessCode
		, InnerNumber			= trInnerHeader.InnerNumber
		, SortOrder				= trInnerLine.SortOrder
		, InnerLineSumID		= trInnerLine.InnerLineSumID
		, InvoiceLineID			= tpInnerCustomsTransferImportInvoiceLine.InvoiceLineID
		---------------------------------------------------------------
		, InnerLineID			= trInnerLine.InnerLineID
		---------------------------------------------------------------
		, IsValidated			= 1
		, MasterKey				= trInnerHeader.InnerHeaderID
	FROM  dtSendingData WITH(NOLOCK)
		INNER JOIN trInnerLine  WITH(NOLOCK)
			ON trInnerLine.InnerHeaderID = dtSendingData.ID  
		INNER JOIN trInnerHeader WITH(NOLOCK)
			ON trInnerHeader.InnerHeaderID = trInnerLine.InnerHeaderID
		INNER JOIN tpInnerCustomsTransferImportInvoiceLine WITH(NOLOCK)
			ON tpInnerCustomsTransferImportInvoiceLine.InnerLineID = trInnerLine.InnerLineID  
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
