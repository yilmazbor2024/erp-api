CREATE PROCEDURE [dbo].[sp_MS_InsertCountingHeader]  (
		  @CompanyCode			ComNum
		, @OfficeCode			Office
		, @StoreCode			Char30
		, @WarehouseCode		Char30
		, @CountingDate			datetime
		, @Description			Char200 = ''
		, @CreatedUserName		Char20
		, @CountingType			tinyint = 0  -- 0: Normal Sayım, 1: Hızlı Sayım, 2: Periyodik Sayım
)
AS
BEGIN TRY

	SET @CompanyCode	 = ISNULL(@CompanyCode		, (1))
	SET @OfficeCode		 = ISNULL(@OfficeCode		, SPACE(0))
	SET @StoreCode		 = ISNULL(@StoreCode		, SPACE(0))
	SET @WarehouseCode	 = ISNULL(@WarehouseCode	, SPACE(0))
	SET @CountingDate	 = ISNULL(@CountingDate		, GETDATE())
	SET @Description	 = ISNULL(@Description		, SPACE(0))
	SET @CreatedUserName = ISNULL(@CreatedUserName	, SPACE(0))
	SET @CountingType	 = ISNULL(@CountingType		, 0)

	DECLARE @TXDate				datetime = GETDATE()
	DECLARE @CountingHeaderID	uniqueidentifier = NEWID()
	DECLARE @CountingNumber		Char30
	DECLARE @Table				TABLE(CountingNumber nvarchar(30))

	INSERT @Table
	EXEC sp_LastCodeCounting @CompanyCode, @OfficeCode, @StoreCode, @WarehouseCode

	SELECT @CountingNumber = CountingNumber
	FROM @Table

	BEGIN TRAN

	INSERT trCountingHeader(
		  CountingHeaderID
		, CompanyCode
		, OfficeCode
		, StoreCode
		, WarehouseCode
		, CountingNumber
		, CountingDate
		, CountingTime
		, Description
		, CountingType
		, IsCompleted
		, IsCancelled
		, IsLocked
		, CreatedUserName
		, CreatedDate
		, LastUpdatedUserName
		, LastUpdatedDate
	)
	VALUES (
		  @CountingHeaderID
		, @CompanyCode
		, @OfficeCode
		, @StoreCode
		, @WarehouseCode
		, @CountingNumber
		, CONVERT(date, @CountingDate)
		, CONVERT(time, @CountingDate)
		, @Description
		, @CountingType
		, 0  -- IsCompleted
		, 0  -- IsCancelled
		, 0  -- IsLocked
		, @CreatedUserName
		, @TXDate
		, @CreatedUserName
		, @TXDate
	)

	COMMIT TRAN

	SELECT CountingHeaderID = @CountingHeaderID, CountingNumber = @CountingNumber
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
