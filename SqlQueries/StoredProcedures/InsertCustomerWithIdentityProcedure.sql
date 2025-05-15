CREATE PROCEDURE [dbo].[sp_MS_InsertCustomer]  (
		  @CurrAccTypeCode		tinyint
		, @FirstName			Char60
		, @LastName				Char60
		, @Email				Char100
		, @PhoneNumber			Char100
		, @CompanyCode			ComNum
		, @OfficeCode			Office
		, @StoreCode			Char30
		, @CreatedUserName		Char20
		, @LangCode				Char5 = N'TR'
		, @IdentityNum			Char20= N''
)
AS
BEGIN TRY

	SET @CurrAccTypeCode = ISNULL(@CurrAccTypeCode	, (0))
	SET @FirstName		 = ISNULL(@FirstName		, SPACE(0))
	SET @LastName		 = ISNULL(@LastName			, SPACE(0))
	SET @Email			 = ISNULL(@Email			, SPACE(0))
	SET @PhoneNumber	 = ISNULL(@PhoneNumber		, SPACE(0))
	SET @CompanyCode	 = ISNULL(@CompanyCode		, (1))
	SET @OfficeCode		 = ISNULL(@OfficeCode		, SPACE(0))
	SET @StoreCode		 = ISNULL(@StoreCode		, SPACE(0))
	SET @CreatedUserName = ISNULL(@CreatedUserName	, SPACE(0))
	SET @IdentityNum	 = ISNULL(@IdentityNum		, SPACE(0))

	DECLARE @TXDate				datetime = GETDATE()
	DECLARE @CurrAccCode		Char30
	DECLARe @PostalAddressID	uniqueidentifier = NEWID()
	DECLARE @CityCode			Char10
	DECLARE @DistrictCode		Char10 
	DECLARE @Table				TABLE(CurrAccCode nvarchar(30))
	DECLARE @CurrencyCode		Char10 

	INSERT @Table
	EXEC sp_LastCodeCurrAcc @CompanyCode, @CurrAccTypeCode, @OfficeCode, @StoreCode

	SELECT @CurrAccCode = CurrAccCode
	FROM @Table

	SELECT @CurrencyCode = LocalCurrencyCode FROM dfOfficeDefault WITH(NOLOCK) WHERE OfficeCode = @OfficeCode

	BEGIN TRAN

	INSERT cdCurrAcc(CompanyCode, OfficeCode, CurrAccTypeCode, CurrAccCode, IdentityNum, FirstName, LastName,CurrencyCode, CreatedDate, CreatedUserName, LastUpdatedDate, LastUpdatedUserName)
	SELECT @CompanyCode, @OfficeCode, @CurrAccTypeCode, @CurrAccCode, @IdentityNum, @FirstName, @LastName,@CurrencyCode ,@TXDate, @CreatedUserName, @TXDate, @CreatedUserName

	INSERT cdCurrAccDesc(CurrAccTypeCode, CurrAccCode, LangCode, CurrAccDescription, CreatedDate, CreatedUserName, LastUpdatedDate, LastUpdatedUserName)
	SELECT @CurrAccTypeCode, @CurrAccCode, @LangCode, RTRIM(@FirstName + N' ' + @LastName), @TXDate, @CreatedUserName, @TXDate, @CreatedUserName

	SELECt @CityCode = prCurrAccPostalAddress.CityCode, @DistrictCode = prCurrAccPostalAddress.DistrictCode
	FROM prCurrAccDefault WITH(NOLOCK)
			INNER JOIN prCurrAccPostalAddress WITH(NOLOCK)
				ON prCurrAccDefault.PostalAddressID = prCurrAccPostalAddress.PostalAddressID
	WHERE prCurrAccDefault.CurrAccTypeCode = 5
	AND prCurrAccDefault.CurrAccCode = @StoreCode

	SET @CityCode = ISNULL(@CityCode, SPACE(0))
	SET @DistrictCode = ISNULL(@DistrictCode, SPACE(0))

	IF ISNULL(@CityCode, SPACE(0)) <> SPACE(0)
	BEGIN
		EXEC sp_MS_UpdateCustomerAddress 
			  @AddressID		= @PostalAddressID
			, @CurrAccTypeCode	= @CurrAccTypeCode
			, @CurrAccCode		= @CurrAccCode
			, @CityCode			= @CityCode
			, @DistrictCode		= @DistrictCode
			, @UpdatedUserName	= @CreatedUserName
	END

	DECLARE @EmailCommunicationTypeCode Char20 
	DECLARe @MobileCommunicationTypeCode Char20

	SELECT @EmailCommunicationTypeCode = EmailCommunicationTypeCode, @MobileCommunicationTypeCode = MobileCommunicationTypeCode
	FROM dfGuidedSalesCustomerParameters WITH(NOLOCK)

	SET @EmailCommunicationTypeCode = ISNULL(@EmailCommunicationTypeCode, SPACE(0))
	SET @MobileCommunicationTypeCode= ISNULL(@MobileCommunicationTypeCode, SPACE(0))

	IF @Email <> SPACE(0)
	BEGIN
		INSERT prCurrAccCommunication(CommunicationID, CurrAccTypeCode, CurrAccCode, CommunicationTypeCode, CommAddress, CanSendAdvert ,IsBlocked
									, CreatedDate, CreatedUserName, LastUpdatedDate, LastUpdatedUserName)
		SELECT CAST(CAST(NEWID() AS BINARY(10)) + CAST(GETDATE() AS BINARY(6)) AS UNIQUEIDENTIFIER) 
			 , @CurrAccTypeCode
			 , @CurrAccCode
			 , @EmailCommunicationTypeCode
			 , @Email
			 , 1
			 , 0
			 , @TXDate
			 , @CreatedUserName
			 , @TXDate
			 , @CreatedUserName
	END 

	IF @PhoneNumber <> SPACE(0)
	BEGIN
		INSERT prCurrAccCommunication(CommunicationID, CurrAccTypeCode, CurrAccCode, CommunicationTypeCode, CommAddress, CanSendAdvert ,IsBlocked
									, CreatedDate, CreatedUserName, LastUpdatedDate, LastUpdatedUserName)
		SELECT CAST(CAST(NEWID() AS BINARY(10)) + CAST(GETDATE() AS BINARY(6)) AS UNIQUEIDENTIFIER) 
			, @CurrAccTypeCode
			, @CurrAccCode
			, @MobileCommunicationTypeCode
			, @PhoneNumber
			, 1
			, 0
			, @TXDate
			, @CreatedUserName
			, @TXDate
			, @CreatedUserName
	END

	COMMIT TRAN

	SELECT CurrAccTypeCode = @CurrAccTypeCode, CurrAccCode = @CurrAccCode
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
