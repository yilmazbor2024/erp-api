-- DTSendShipment Stored Procedure
-- Sevkiyat verilerini veri transferi için hazırlayan stored procedure

USE [DENEME]
GO
/****** Object:  StoredProcedure [dbo].[qry_DTSendShipment]    Script Date: 5/9/2025 2:20:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[qry_DTSendShipment] (
		  @Suffix			Char60
		, @TransferName		Char100
		, @LastDayCount		smallint = 0
		, @FilterString		nvarchar(max) = N''		
)
AS
BEGIN TRY

	DECLARE @sql			NVARCHAR(MAX)
	DECLARE @StartDate		DATETIME = DATEADD(DAY, @LastDayCount * - 1, GETDATE())
	DECLARE @EndDate		DATETIME = GETDATE()

	SET @sql = N'
	INSERT dtSendingData(SendingDataID, Suffix, TransferName, CodeType, Code,ID)
	SELECT CAST(CAST(NEWID() AS BINARY(10)) + CAST(GETDATE() AS BINARY(6)) AS UNIQUEIDENTIFIER) ID, @Suffix, @TransferName, NULL, NULL, trShipmentHeader.ShipmentHeaderID
	FROM  trShipmentHeader WITH(NOLOCK)	
		LEFT OUTER JOIN tpShipmentHeaderExtension WITH(NOLOCK) ON tpShipmentHeaderExtension.ShipmentHeaderID = trShipmentHeader.ShipmentHeaderID
	WHERE trShipmentHeader.IsCompleted = 1 AND trShipmentHeader.IsTransferApproved = 1
	AND trShipmentHeader.LastUpdatedDate BETWEEN @StartDate AND @EndDate
	{FilterString}
	'
	SET @sql = REPLACE(@sql, '{FilterString}', @FilterString)

	EXEC sp_ExecuteSQL @sql, N'@Suffix Char60, @TransferName Char30, @StartDate DATETIME, @EndDate DATETIME', @Suffix, @TransferName, @StartDate, @EndDate

	SELECT   
		  Suffix								= dtSendingData.Suffix	
		------------------------------------------------------------------------
		, TransTypeCode							= trShipmentHeader.TransTypeCode
		, ProcessCode							= trShipmentHeader.ProcessCode
		, ShippingNumber						= trShipmentHeader.ShippingNumber
		, IsReturn								= trShipmentHeader.IsReturn
		, ShippingDate							= trShipmentHeader.ShippingDate
		, ShippingTime							= trShipmentHeader.ShippingTime
		, OperationDate							= trShipmentHeader.OperationDate
		, OperationTime							= trShipmentHeader.OperationTime
		, JournalDate							= trShipmentHeader.JournalDate
		------------------------------------------------------------------------
		, Series								= trShipmentHeader.Series
		, SeriesNumber							= trShipmentHeader.SeriesNumber
		------------------------------------------------------------------------
		, Description							= trShipmentHeader.Description
		, InternalDescription					= trShipmentHeader.InternalDescription
		------------------------------------------------------------------------
		, CurrAccTypeCode						= trShipmentHeader.CurrAccTypeCode
		, CurrAccCode							= trShipmentHeader.CurrAccCode
		, SubCurrAccCode						= prSubCurrAcc.SubCurrAccCode
		, ContactID								= trShipmentHeader.ContacTID
		, ContactTypeCode						= prCurrAccContact.ContactTypeCode		
		, FirstName								= prCurrAccContact.FirstName		
		, LastName								= prCurrAccContact.LastName		
		, IdentityNum							= prCurrAccContact.IdentityNum	
		------------------------------------------------------------------------
		, ShipmentMethodCode					= trShipmentHeader.ShipmentMethodCode
		, RoundsmanCode							= trShipmentHeader.RoundsmanCode
		, DeliveryCompanyCode					= trShipmentHeader.DeliveryCompanyCode
		, LogisticsCompanyBOL					= trShipmentHeader.LogisticsCompanyBOL
		, CustomerASNNumber						= trShipmentHeader.CustomerASNNumber
		------------------------------------------------------------------------
		, CompanyCode							= trShipmentHeader.CompanyCode
		, OfficeCode							= trShipmentHeader.OfficeCode
		, StoreTypeCode							= trShipmentHeader.StoreTypeCode
		, StoreCode								= trShipmentHeader.StoreCode
		, WarehouseCode							= trShipmentHeader.WarehouseCode
		, ToWarehouseCode						= trShipmentHeader.ToWarehouseCode
		------------------------------------------------------------------------
		, ImportFileNumber						= trShipmentHeader.ImportFileNumber	
		, ExportFileNumber						= trShipmentHeader.ExportFileNumber		
		------------------------------------------------------------------------
		, IsOrderBase							= trShipmentHeader.IsOrderBase
		, IsTransferApproved					= trShipmentHeader.IsTransferApproved	
		, TransferApprovedDate					= trShipmentHeader.TransferApprovedDate
		------------------------------------------------------------------------
		, ShipmentTypeCode						= tpShipmentHeaderExtension.ShipmentTypeCode
		, EShipmentNumber						= tpShipmentHeaderExtension.EShipmentNumber
		, EShipmentStatusCode					= tpShipmentHeaderExtension.EShipmentStatusCode
		-------------------------------------------------------------------------
		, ShipmentHeaderID						= trShipmentHeader.ShipmentHeaderID
		-------------------------------------------------------------------------
		, IsValidated							= 1
		, MasterKey								= trShipmentHeader.ShipmentHeaderID
	FROM  dtSendingData WITH(NOLOCK)
		INNER JOIN trShipmentHeader WITH(NOLOCK)
			ON trShipmentHeader.ShipmentHeaderID = dtSendingData.ID  
		LEFT OUTER JOIN tpShipmentHeaderExtension WITH(NOLOCK)
			ON tpShipmentHeaderExtension.ShipmentHeaderID = trShipmentHeader.ShipmentHeaderID  
		LEFT OUTER JOIN prCurrAccContact WITH(NOLOCK)
			ON prCurrAccContact.ContactID = trShipmentHeader.ContactID  
		LEFT OUTER JOIN prSubCurrAcc WITH(NOLOCK)
			ON prSubCurrAcc.SubCurrAccID = trShipmentHeader.SubCurrAccID  
	WHERE dtSendingData.Suffix  = @Suffix 
	AND dtSendingData.TransferName = @TransferName

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
