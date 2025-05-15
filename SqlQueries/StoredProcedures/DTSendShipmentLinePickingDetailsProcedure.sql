-- DTSendShipmentLinePickingDetails Stored Procedure
-- Sevkiyat satırlarının toplama detaylarını veri transferi için hazırlayan stored procedure

USE [DENEME]
GO
/****** Object:  StoredProcedure [dbo].[qry_DTSendShipmentLinePickingDetails]    Script Date: 5/9/2025 2:18:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[qry_DTSendShipmentLinePickingDetails] (
		  @Suffix			Char60
		, @TransferName		Char100
)
AS
BEGIN TRY

	SELECT 
		  Suffix					= dtSendingData.Suffix
		---------------------------------------------------------------
		, ProcessCode				= trShipmentHeader.ProcessCode
		, ShippingNumber			= trShipmentHeader.ShippingNumber
		, SortOrder					= trShipmentLine.SortOrder
		, ShipmentLineSumID			= trShipmentLine.ShipmentLineSumID
		---------------------------------------------------------------
		, PickingDate				= tpShipmentLinePickingDetails.PickingDate				
		, PackageNumber				= tpShipmentLinePickingDetails.PackageNumber				
		, PackagingTypeCode			= tpShipmentLinePickingDetails.PackagingTypeCode			
		, PackageBrandCode			= tpShipmentLinePickingDetails.PackageBrandCode			
		, PackageVolumeCode			= tpShipmentLinePickingDetails.PackageVolumeCode			
		, WeightUnitOfMeasureCode	= tpShipmentLinePickingDetails.WeightUnitOfMeasureCode	
		, PackedWeight				= tpShipmentLinePickingDetails.PackedWeight				
		, UnitOfMeasureCode			= tpShipmentLinePickingDetails.UnitOfMeasureCode			
		, Qty						= tpShipmentLinePickingDetails.Qty											
		---------------------------------------------------------------
		, ShipmentHeaderID			= trShipmentHeader.ShipmentHeaderID
		, ShipmentLineID			= trShipmentLine.ShipmentLineID
		---------------------------------------------------------------
		, IsValidated				= 1
		, MasterKey					= trShipmentHeader.ShipmentHeaderID			
	FROM dtSendingData WITH(NOLOCK)
		INNER JOIN tpShipmentLinePickingDetails WITH(NOLOCK)
			ON tpShipmentLinePickingDetails.ShipmentHeaderID = dtSendingData.ID  
		INNER JOIN trShipmentHeader WITH(NOLOCK)
			ON trShipmentHeader.ShipmentHeaderID = tpShipmentLinePickingDetails.ShipmentHeaderID  
		INNER JOIN trShipmentLine WITH(NOLOCK)
			ON trShipmentLine.ShipmentLineID = tpShipmentLinePickingDetails.ShipmentLineID
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
