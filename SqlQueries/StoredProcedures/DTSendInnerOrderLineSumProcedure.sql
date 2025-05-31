-- DTSendInnerOrderLineSum Stored Procedure
-- İç sipariş satır toplamlarını veri transferi için hazırlayan stored procedure

 
/****** Object:  StoredProcedure [dbo].[qry_DTSendInnerOrderLineSum]    Script Date: 5/9/2025 2:35:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[qry_DTSendInnerOrderLineSum] (
		  @Suffix			Char60
		, @TransferName		Char100
)
AS
BEGIN TRY

	SELECT   
		  Suffix				= dtSendingData.Suffix
		---------------------------------------------------------------
		, InnerOrderNumber		= trInnerOrderHeader.InnerOrderNumber
		, InnerOrderLineSumID	= trInnerOrderLineSum.InnerOrderLineSumID
		, LotCode				= trInnerOrderLineSum.LotCode
		, LotQty				= trInnerOrderLineSum.LotQty
		---------------------------------------------------------------
		, InnerOrderHeaderID	= trInnerOrderHeader.InnerOrderHeaderID
		---------------------------------------------------------------
		, IsValidated			= 1
		, MasterKey				= trInnerOrderHeader.InnerOrderNumber
	FROM  dtSendingData WITH(NOLOCK)
		INNER JOIN trInnerOrderHeader  WITH(NOLOCK)
			ON dtSendingData.ID = trInnerOrderHeader.InnerOrderHeaderID
		INNER JOIN trInnerOrderLineSum WITH(NOLOCK)
			ON trInnerOrderHeader.InnerOrderHeaderID = trInnerOrderLineSum.InnerOrderHeaderID
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
