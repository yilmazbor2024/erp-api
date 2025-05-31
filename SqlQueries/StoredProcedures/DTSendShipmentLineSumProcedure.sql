-- DTSendShipmentLineSum Stored Procedure
-- Sevkiyat satır toplamlarını veri transferi için hazırlayan stored procedure

 
/****** Object:  StoredProcedure [dbo].[qry_DTSendShipmentLineSum]    Script Date: 5/9/2025 2:19:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[qry_DTSendShipmentLineSum] (
		  @Suffix			Char60
		, @TransferName		Char100
)
AS
BEGIN TRY

	SELECT   
		  Suffix				= dtSendingData.Suffix
		---------------------------------------------------------------
		, ProcessCode			= trShipmentHeader.ProcessCode
		, ShippingNumber		= trShipmentHeader.ShippingNumber
		, ShipmentLineSumID		= trShipmentLineSum.ShipmentLineSumID
		, LotCode				= trShipmentLineSum.LotCode
		, LotQty				= trShipmentLineSum.LotQty
		---------------------------------------------------------------
		, ShipmentHeaderID		= trShipmentHeader.ShipmentHeaderID
		---------------------------------------------------------------
		, IsValidated			= 1
		, MasterKey				= trShipmentHeader.ShipmentHeaderID
	FROM  dtSendingData WITH(NOLOCK)
		INNER JOIN trShipmentHeader WITH(NOLOCK)
			ON trShipmentHeader.ShipmentHeaderID = dtSendingData.ID
		INNER JOIN trShipmentLineSum WITH(NOLOCK)
			ON trShipmentLineSum.ShipmentHeaderID = trShipmentHeader.ShipmentHeaderID
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
