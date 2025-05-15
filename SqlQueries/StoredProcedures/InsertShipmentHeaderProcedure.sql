USE [DENEME]
GO
/****** Object:  StoredProcedure [dbo].[sp_MS_InsertShipmentHeader]    Script Date: 5/9/2025 2:53:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_MS_InsertShipmentHeader]  
(
	  @ProcessCode					Process = N'S'
	, @IsReturn						bit = 0
	, @ShippingDate					Date	
	, @ShippingTime					Time(0)	
	, @OperationDate				Date	
	, @OperationTime				Time(0)	
	, @Description					Char200
	, @InternalDescription			Char200
	, @StoreCode					Char30
	, @ToStoreCode					Char30
	, @WarehouseCode				Char10
	, @ToWarehouseCode				Char10
	, @ShipmentMethodCode			Char10
	, @ShippingPostalAddressID		uniqueidentifier
	, @RoundsmanCode				Char10
	, @DeliveryCompanyCode			Char10
	, @VehicleCode					Char10
	, @DriverCode					nvarchar(max)
	, @IsOrderBase					bit
	, @CreatedUserName				Char20
	, @CurrAccTypeCode				tinyint = null   
	, @CurrAccCode					Char30  = null
)
AS
BEGIN TRY

	SET @ProcessCode			= ISNULL(@ProcessCode			, N'S')
	SET @IsReturn				= ISNULL(@IsReturn				, 0)
	SET @ShippingDate			= ISNULL(@ShippingDate			, SPACE(0))
	SET @ShippingTime			= ISNULL(@ShippingTime			, SPACE(0))
	SET @OperationDate			= ISNULL(@OperationDate			, SPACE(0))
	SET @OperationTime			= ISNULL(@OperationTime			, SPACE(0))
	SET @Description			= ISNULL(@Description			, SPACE(0))
	SET @InternalDescription	= ISNULL(@InternalDescription	, SPACE(0))
	SET @StoreCode				= ISNULL(@StoreCode				, SPACE(0))
	SET @ToStoreCode			= ISNULL(@ToStoreCode			, SPACE(0))
	SET @WarehouseCode			= ISNULL(@WarehouseCode			, SPACE(0))
	SET @ToWarehouseCode		= ISNULL(@ToWarehouseCode		, SPACE(0))
	SET @ShipmentMethodCode		= ISNULL(@ShipmentMethodCode	, SPACE(0))
	SET @RoundsmanCode			= ISNULL(@RoundsmanCode			, SPACE(0))
	SET @DeliveryCompanyCode	= ISNULL(@DeliveryCompanyCode	, SPACE(0))
	SET @IsOrderBase			= ISNULL(@IsOrderBase			, SPACE(0))
	SET @CreatedUserName		= ISNULL(@CreatedUserName		, SPACE(0))
	SET @CurrAccTypeCode		= ISNULL(@CurrAccTypeCode		, SPACE(0))
	SET @CurrAccCode			= ISNULL(@CurrAccCode			, SPACE(0))
	
	DECLARE @ShipmentHeaderID		uniqueidentifier	= CAST (CAST(NEWID() AS BINARY(10)) + CAST (GETDATE() AS BINARY(6)) AS UNIQUEIDENTIFIER)
	DECLARE @TransTypeCode			tinyint				= CASE WHEN @ProcessCode IN(N'IP', N'CP', N'BP') AND @IsReturn = 1 THEN 1 ELSE 3 END
	DECLARE @ShipmentTypeCode		tinyint				= 0
	DECLARE @ShippingNumber			UQNumber			= N''
	DECLARE @CompanyCode			ComNum				= 1
	DECLARE @OfficeCode				Office				= N''
	DECLARE @ToOfficeCode			Office				= N''
	DECLARE @IsTransferApproved		bit					= 1
	DECLARE @TransferApprovedDate	date				= N''
	DECLARE @ApplicationCode		Char5				= N'Shipm'
	DECLARE @CreatedDate			datetime			= GETDATE()

	IF @ProcessCode NOT IN('S', 'RFS', 'CP', 'BP', 'IP')
		RAISERROR ('MSGProcessCodeIsNotValid' , 16, 2)
	
	IF @StoreCode = SPACE(0) AND @ProcessCode = N'S' AND @IsReturn = 0
		RAISERROR ('MSGStoreCodeIsNotEmpty' , 16, 2)

	IF @ToStoreCode = SPACE(0) AND @ProcessCode = N'S' AND @IsReturn = 0
		RAISERROR ('MSGToStoreCodeIsNotEmpty' , 16, 2)

	IF @ProcessCode = N'S' AND @IsReturn = 0
	BEGIN
		SELECT @CompanyCode = CompanyCode, @OfficeCode = OfficeCode
		FROM cdCurrAcc WITH(NOLOCK)
		WHERE CurrAccTypeCode = 5 AND CurrAccCode = @StoreCode

		SELECT @ToOfficeCode = OfficeCode		
		FROM cdCurrAcc WITH(NOLOCK)
		WHERE CurrAccTypeCode = 5 AND CurrAccCode = @ToStoreCode
	END
	IF @ProcessCode = N'S' AND @IsReturn = 1
	BEGIN
		SELECT @CompanyCode = cdOffice.CompanyCode ,@OfficeCode = cdOffice.OfficeCode
		FROM cdWarehouse WITH(NOLOCK)
			INNER JOIN cdOffice WITH(NOLOCK) ON cdOffice.OfficeCode = cdWarehouse.OfficeCode
		WHERE cdWarehouse.WarehouseCode = @WarehouseCode

		SELECT @ToOfficeCode = OfficeCode		
		FROM cdWarehouse WITH(NOLOCK)
		WHERE cdWarehouse.WarehouseCode = @WarehouseCode
	END
	IF @ProcessCode = N'RFS' AND @IsReturn = 0
	BEGIN
		SELECT @CompanyCode = cdOffice.CompanyCode ,@OfficeCode = cdOffice.OfficeCode
		FROM cdWarehouse WITH(NOLOCK)
			INNER JOIN cdOffice WITH(NOLOCK) ON cdOffice.OfficeCode = cdWarehouse.OfficeCode
		WHERE cdWarehouse.WarehouseCode = @WarehouseCode

		SELECT @ToOfficeCode = OfficeCode		
		FROM cdWarehouse WITH(NOLOCK)
		WHERE cdWarehouse.WarehouseCode = @WarehouseCode
	END

	IF @ProcessCode IN(N'IP', N'CP', N'BP') AND @IsReturn = 1
	BEGIN
		SET @WarehouseCode   = @ToWarehouseCode
		SET @ToWarehouseCode = SPACE(0)		

		SELECT @CompanyCode = cdOffice.CompanyCode ,@OfficeCode = cdOffice.OfficeCode
		FROM cdWarehouse WITH(NOLOCK)
			INNER JOIN cdOffice WITH(NOLOCK) ON cdOffice.OfficeCode = cdWarehouse.OfficeCode
		WHERE cdWarehouse.WarehouseCode = @WarehouseCode
	END

	IF ISNULL(@CompanyCode, 0) = 0
		RAISERROR ('MSGCompanyCodeIsNotEmpty' , 16, 2)

	IF ISNULL(@OfficeCode, N'') = N''
		RAISERROR ('MSGOfficeCodeIsNotEmpty' , 16, 2)

	IF EXISTS(SELECT TOP 1 1 FROM dfCompanyDefault  WHERE CompanyCode = @CompanyCode AND IsCompanySubjectToEShipment = 1)
		SELECT @ShipmentTypeCode = 1

	DECLARE @RefNumber TABLE (ShippingNumber UQNumber)

	INSERT @RefNumber
	EXEC sp_LastRefNumProcess @CompanyCode = @CompanyCode, @ProcessCode = @ProcessCode, @ProcessFlowCode = 6

	SELECT @ShippingNumber = ShippingNumber FROM @RefNumber

	IF EXISTS(SELECT TOP 1 1 FROM dfOfficeDefault WITH(NOLOCK) WHERE OfficeCode = @ToOfficeCode AND UseTransferApproving = 1)
	OR EXISTS(SELECT TOP 1 1 FROM dfStoreDefault  WITH(NOLOCK) WHERE StoreCurrAccCode  = @ToStoreCode  AND UseTransferApproving = 1 AND @ToStoreCode<> SPACE(0))
	BEGIN
		SET @IsTransferApproved = 0
	END 

	IF @ProcessCode IN(N'IP', N'CP', N'BP') AND @IsReturn = 1
	BEGIN
		SET @IsTransferApproved = 1
	END

	IF @IsTransferApproved = 1
		SET @TransferApprovedDate = GETDATE()

	IF (@ProcessCode = N'S' AND @IsReturn = 1) OR (@ProcessCode = N'RFS' AND @IsReturn = 0)
		SET @ShippingPostalAddressID = (SELECT TOP 1 PostalAddressID 
										FROM prCurrAccDefault WITH(NOLOCK) 
										WHERE CurrAccTypeCode = 5 AND CurrAccCode = @StoreCode)

	IF @CurrAccCode <> SPACE(0)
		SET @ShippingPostalAddressID = (SELECT TOP 1 PostalAddressID 
										FROM prCurrAccDefault WITH(NOLOCK) 
										WHERE CurrAccTypeCode = @CurrAccTypeCode AND CurrAccCode = @CurrAccCode)

	DECLARE @DriverCodeList TABLE(DriverCode nvarchar(10))
	INSERT @DriverCodeList
	SELECT StrCol
	FROM dbo.DelimetedStringToTable(@DriverCode, N';')
	WHERE StrCol <> SPACE(0)

	BEGIN TRAN

	INSERT trShipmentHeader
	(
		  ShipmentHeaderID
		, TransTypeCode
		, ProcessCode
		, IsReturn
		, SeriesNumber
		, ShippingNumber
		, ShippingDate	
		, ShippingTime	
		, OperationDate	
		, OperationTime	
		, Description
		, InternalDescription
		, CurrAccTypeCode
		, CurrAccCode
		, ShipmentMethodCode
		, ShippingPostalAddressID
		, BillingPostalAddressID
		, RoundsmanCode		
		, DeliveryCompanyCode	
		, CompanyCode		
		, OfficeCode		
		, StoreTypeCode	
		, StoreCode		
		, WarehouseCode
		, ToWarehouseCode
		, IsTransferApproved
		, TransferApprovedDate
		, IsOrderBase
		, CustomerASNNumber
		, ApplicationCode
		, ApplicationID	
		, CreatedDate
		, CreatedUserName
		, LastUpdatedDate
		, LastUpdatedUserName
	)
	SELECT 
		  ShipmentHeaderID			= @ShipmentHeaderID
		, TransTypeCode				= @TransTypeCode
		, ProcessCode				= @ProcessCode
		, IsReturn					= @IsReturn
		, SeriesNumber				= 0
		, ShippingNumber			= @ShippingNumber
		, ShippingDate				= @ShippingDate		
		, ShippingTime				= @ShippingTime		
		, OperationDate				= @OperationDate	
		, OperationTime				= @OperationTime	
		, Description				= @Description
		, InternalDescription		= @InternalDescription
		, CurrAccTypeCode			= CASE WHEN @CurrAccTypeCode > 0 THEN @CurrAccTypeCode 
										   ELSE 5
									  END
		, CurrAccCode				= CASE WHEN @CurrAccCode <> SPACE(0)			  THEN @CurrAccCode 
										   WHEN @ProcessCode = N'S' AND @IsReturn = 0 THEN @ToStoreCode 
										   ELSE @StoreCode 
									  END
		, ShipmentMethodCode		= @ShipmentMethodCode
		, ShippingPostalAdddressID	= @ShippingPostalAddressID
		, BillingPostalAddressID    = CASE WHEN @ProcessCode IN(N'IP', N'CP', N'BP') AND @IsReturn = 1 THEN @ShippingPostalAddressID ELSE NULL END
		, RoundsmanCode				= @RoundsmanCode
		, DeliveryCompanyCode		= @DeliveryCompanyCode
		, CompanyCode				= @CompanyCode		
		, OfficeCode				= @OfficeCode		
		, StoreTypeCode				= 5		
		, StoreCode					= CASE WHEN @ProcessCode = N'S' AND @IsReturn = 0					THEN @StoreCode 
										   WHEN @ProcessCode IN(N'IP', N'CP', N'BP') AND @IsReturn = 1	THEN @StoreCode
										   ELSE SPACE(0) 
									  END		
		, WarehouseCode				= @WarehouseCode		
		, ToWarehouseCode			= @ToWarehouseCode
		, IsTransferApproved		= @IsTransferApproved
		, TransferApprovedDate		= @TransferApprovedDate
		, IsOrderBase				= @IsOrderBase
		, CustomerASNNumber			= SPACE(0)
		, ApplicationCode			= @ApplicationCode
		, ApplicationID				= @ShipmentHeaderID
		, CreatedDate				= @CreatedDate
		, CreatedUserName			= @CreatedUserName
		, LastUpdatedDate			= @CreatedDate
		, LastUpdatedUserName		= @CreatedUserName

	IF NOT EXISTS(SELECT * FROM dfCompanyDefault WITH(NOLOCK) WHERE CompanyCode = @Companycode AND IsCompanySubjectToEShipment  = 0)
	INSERT tpShipmentHeaderExtension(ShipmentHeaderID, ShipmentTypeCode, EShipmentNumber, EShipmentStatusCode, CreatedDate, CreatedUserName, LastUpdatedDate, LastUpdatedUserName)
	SELECT @ShipmentHeaderID, @ShipmentTypeCode, SPACE(0), 0, @CreatedDate, @CreatedUserName, @CreatedDate, @CreatedUserName

	INSERT tpShipmentTransportModeDetail(ShipmentHeaderID, VehicleCode, LicensePlateID, CreatedDate, CreatedUserName, LastUpdatedDate, LastUpdatedUserName)
	SELECT @ShipmentHeaderID, @VehicleCode, LicensePlate, @CreatedDate, @CreatedUserName, @CreatedDate, @CreatedUserName
	FROM cdVehicle WITH(NOLOCK)
	WHERE VehicleCode = @VehicleCode

	INSERT tpShipmentVehicleDrivers(ShipmentHeaderID, DriverCode, CreatedDate, CreatedUserName, LastUpdatedDate, LastUpdatedUserName)
	SELECT @ShipmentHeaderID, DriverCode, @CreatedDate, @CreatedUserName, @CreatedDate, @CreatedUserName
	FROM @DriverCodeList

	COMMIT TRAN

	SELECT ShipmentHeaderID = @ShipmentHeaderID

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
