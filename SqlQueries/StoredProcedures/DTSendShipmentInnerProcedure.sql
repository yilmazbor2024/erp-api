-- DTSendShipmentInner Stored Procedure
-- İç sevkiyat verilerini veri transferi için hazırlayan stored procedure

 
GO
/****** Object:  StoredProcedure [dbo].[qry_DTSendShipmentInner]    Script Date: 5/9/2025 2:22:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[qry_DTSendShipmentInner] (
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
	SELECT CAST(CAST(NEWID() AS BINARY(10)) + CAST(GETDATE() AS BINARY(6)) AS UNIQUEIDENTIFIER) ID, @Suffix, @TransferName, NULL, NULL, trInnerHeader.InnerHeaderID
	FROM  trInnerHeader WITH(NOLOCK)
		INNER JOIN tpInnerHeaderExtension WITH(NOLOCK) ON tpInnerHeaderExtension.InnerHeaderID = trInnerHeader.InnerHeaderID AND tpInnerHeaderExtension.ShipmentTypeCode IN(0,1,2)
	WHERE trInnerHeader.IsCompleted = 1 AND trInnerHeader.IsTransferApproved = 1
	AND trInnerHeader.LastUpdatedDate BETWEEN @StartDate AND @EndDate
	{FilterString}
	'
	SET @sql = REPLACE(@sql, '{FilterString}', @FilterString)

	EXEC sp_ExecuteSQL @sql, N'@Suffix Char60, @TransferName Char30, @StartDate DATETIME, @EndDate DATETIME', @Suffix, @TransferName, @StartDate, @EndDate

	SELECT   
		  Suffix								= dtSendingData.Suffix	
		------------------------------------------------------------------------
		, TransTypeCode							= trInnerHeader.TransTypeCode
		, InnerProcessCode						= trInnerHeader.InnerProcessCode
		, InnerNumber							= trInnerHeader.InnerNumber
		, IsReturn								= trInnerHeader.IsReturn
		, OperationDate							= trInnerHeader.OperationDate
		, OperationTime							= trInnerHeader.OperationTime
		, JournalDate							= trInnerHeader.JournalDate
		------------------------------------------------------------------------
		, Series								= trInnerHeader.Series
		, SeriesNumber							= trInnerHeader.SeriesNumber
		------------------------------------------------------------------------
		, Description							= trInnerHeader.Description
		------------------------------------------------------------------------
		, CurrAccTypeCode						= trInnerHeader.CurrAccTypeCode
		, CurrAccCode							= trInnerHeader.CurrAccCode
		, SubCurrAccCode						= prSubCurrAcc.SubCurrAccCode	
		------------------------------------------------------------------------
		, CompanyCode							= trInnerHeader.CompanyCode
		, OfficeCode							= trInnerHeader.OfficeCode
		, StoreTypeCode							= trInnerHeader.StoreTypeCode
		, StoreCode								= trInnerHeader.StoreCode
		, WarehouseCode							= trInnerHeader.WarehouseCode
		, ToOfficeCode							= trInnerHeader.ToOfficeCode	
		, ToStoreCode							= trInnerHeader.ToStoreCode	
		, ToWarehouseCode						= trInnerHeader.ToWarehouseCode
		------------------------------------------------------------------------
		, ServicemanCode						= trInnerHeader.ServicemanCode
		, RoundsmanCode							= trInnerHeader.RoundsmanCode
		------------------------------------------------------------------------
		, ImportFileNumber						= trInnerHeader.ImportFileNumber	
		, ExportFileNumber						= trInnerHeader.ExportFileNumber
		, CustomsDocumentNumber					= trInnerHeader.CustomsDocumentNumber
		------------------------------------------------------------------------
		, IsInnerOrderBase						= trInnerHeader.IsInnerOrderBase
		, IsSectionTransfer						= trInnerHeader.IsSectionTransfer
		, IsTransferApproved					= trInnerHeader.IsTransferApproved	
		, TransferApprovedDate					= trInnerHeader.TransferApprovedDate
		------------------------------------------------------------------------
		, ShipmentMethodCode					= tpInnerHeaderExtension.ShipmentMethodCode			
		, DeliveryCompanyCode					= tpInnerHeaderExtension.DeliveryCompanyCode	
		, ShipmentTypeCode						= tpInnerHeaderExtension.ShipmentTypeCode
		, EShipmentNumber						= tpInnerHeaderExtension.EShipmentNumber
		, EShipmentStatusCode					= tpInnerHeaderExtension.EShipmentStatusCode
		, LogisticsCompanyBOL					= tpInnerHeaderExtension.LogisticsCompanyBOL
		-------------------------------------------------------------------------
		, InnerHeaderID							= trInnerHeader.InnerHeaderID
		-------------------------------------------------------------------------
		, IsValidated							= 1
		, MasterKey								= trInnerHeader.InnerHeaderID
	FROM  dtSendingData WITH(NOLOCK)
		INNER JOIN trInnerHeader WITH(NOLOCK)
			ON trInnerHeader.InnerHeaderID = dtSendingData.ID  
		LEFT OUTER JOIN tpInnerHeaderExtension WITH(NOLOCK)
			ON tpInnerHeaderExtension.InnerHeaderID = trInnerHeader.InnerHeaderID  
		LEFT OUTER JOIN prSubCurrAcc WITH(NOLOCK)
			ON prSubCurrAcc.SubCurrAccID = trInnerHeader.SubCurrAccID  
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
