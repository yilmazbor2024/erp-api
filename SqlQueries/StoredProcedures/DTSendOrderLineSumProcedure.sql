-- DTSendOrderLineSum Stored Procedure
-- Sipariş satır toplamlarını veri transferi için hazırlayan stored procedure

 
/****** Object:  StoredProcedure [dbo].[qry_DTSendOrderLineSum]    Script Date: 5/9/2025 2:35:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[qry_DTSendOrderLineSum] (
		  @Suffix			Char60
		, @TransferName		Char100
)
AS
BEGIN TRY

	SELECT   
		  Suffix				= dtSendingData.Suffix
		---------------------------------------------------------------
		, OrderNumber			= trOrderHeader.OrderNumber
		, OrderLineSumID		= trOrderLineSum.OrderLineSumID
		, LotCode				= trOrderLineSum.LotCode
		, LotQty				= trOrderLineSum.LotQty
		---------------------------------------------------------------
		, OrderHeaderID			= trOrderHeader.OrderHeaderID
		---------------------------------------------------------------
		, IsValidated			= 1
		, MasterKey				= trOrderHeader.OrderNumber
	FROM  dtSendingData WITH(NOLOCK)
		INNER JOIN trOrderHeader  WITH(NOLOCK)
			ON dtSendingData.ID = trOrderHeader.OrderHeaderID
		INNER JOIN trOrderLineSum WITH(NOLOCK)
			ON trOrderHeader.OrderHeaderID = trOrderLineSum.OrderHeaderID
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
