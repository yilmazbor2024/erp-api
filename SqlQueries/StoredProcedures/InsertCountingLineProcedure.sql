CREATE PROCEDURE [dbo].[sp_MS_InsertCountingLine]  
(
	  @CountingHeaderID				uniqueidentifier
	, @ItemTypeCode					Tinyint 
	, @ItemCode						Char30	
	, @ColorCode					Char10	
	, @ItemDim1Code					Char10	
	, @ItemDim2Code					Char10	
	, @ItemDim3Code					Char10		    
	, @BatchCode   					Char20	
	, @Barcode						Char30
	, @SerialNumber					Char100
	, @Qty1							float
	, @Action						Char10
	, @ItAtt01						Char20
	, @ItAtt02						Char20
	, @ItAtt03						Char20
	, @ItAtt04						Char20
	, @ItAtt05 						Char20
	, @CreatedUserName				Char20
	, @LangCode						Char5 = N'TR'
)
AS
BEGIN TRY

	SET @ItemTypeCode		= ISNULL(@ItemTypeCode			, SPACE(0))
	SET @ItemCode			= ISNULL(@ItemCode				, SPACE(0))
	SET @ColorCode			= ISNULL(@ColorCode				, SPACE(0))
	SET @ItemDim1Code		= ISNULL(@ItemDim1Code			, SPACE(0))
	SET @ItemDim2Code		= ISNULL(@ItemDim2Code			, SPACE(0))
	SET @ItemDim3Code		= ISNULL(@ItemDim3Code			, SPACE(0)) 
	SET @Barcode			= ISNULL(@Barcode				, SPACE(0))
	SET @BatchCode   		= ISNULL(@BatchCode   			, SPACE(0))
	SET @SerialNumber		= ISNULL(@SerialNumber			, SPACE(0))
	SET @Qty1				= ISNULL(@Qty1					, SPACE(0))
	SET @Action				= ISNULL(@Action				, SPACE(0))
	SET @ItAtt01			= ISNULL(@ItAtt01				, SPACE(0))
	SET @ItAtt02			= ISNULL(@ItAtt02				, SPACE(0))
	SET @ItAtt03			= ISNULL(@ItAtt03				, SPACE(0))
	SET @ItAtt04			= ISNULL(@ItAtt04				, SPACE(0))
	SET @ItAtt05 			= ISNULL(@ItAtt05 				, SPACE(0))
	SET @CreatedUserName	= ISNULL(@CreatedUserName		, SPACE(0))

	IF @Action = SPACE(0) OR @Action NOT IN('Add', 'Remove')
		RAISERROR ('MSGActionTypeIsNotValid : Add/Remove' , 16, 2)

	IF @Action = N'Add'
	BEGIN
		DECLARE @CountingLineID				uniqueidentifier= CAST (CAST(NEWID() AS BINARY(10)) + CAST (GETDATE() AS BINARY(6)) AS UNIQUEIDENTIFIER)
		DECLARE @WarehouseCode				Char30			= (SELECT WarehouseCode FROM trCountingHeader WITH(NOLOCK) WHERE CountingHeaderID = @CountingHeaderID)
		DECLARE @CreatedDate				datetime		= GETDATE()
		DECLARE @MaxSortOrder				int				= ISNULL((SELECT MAX(SortOrder) FROM trCountingLine WITH(NOLOCK) WHERE CountingHeaderID = @CountingHeaderID), 0)
		DECLARE @ValidationInfo				nvarchar(200)	= N''
		
		IF OBJECT_ID('tempdb..#ValidateItemWarehouse') IS NOT NULL DROP TABLE #ValidateItemWarehouse
		CREATE TABLE #ValidateItemWarehouse(Permit bit, ValidationType tinyint, ValidationInfo nvarchar(200))
		INSERT #ValidateItemWarehouse
		EXEC sp_ValidateItemWarehouse
			@WarehouseCode		= @WarehouseCode,
			@ItemTypeCode		= @ItemTypeCode,
			@ItemCode			= @ItemCode

		SET @ValidationInfo = ISNULL((SELECT TOP 1 ValidationInfo FROM #ValidateItemWarehouse WHERE Permit = 0), SPACE(0))

		IF @ValidationInfo <> SPACE(0)
			RAISERROR (@ValidationInfo , 16, 2)

		BEGIN TRAN

		INSERT trCountingLine
		(
			  CountingLineID	
			, SortOrder		
			, ItemTypeCode	
			, ItemCode		
			, ColorCode		
			, ItemDim1Code	
			, ItemDim2Code	
			, ItemDim3Code	
			, Qty1			
			, Qty2			
			, BatchCode
			, SectionCode
			, SerialNumber
			, UsedBarcode
			, CountingLineSumID
			, CountingHeaderID
			, CreatedDate
			, CreatedUserName
			, LastUpdatedDate
			, LastUpdatedUserName
		)
		SELECT 
			  CountingLineID		= @CountingLineID
			, SortOrder				= @MaxSortOrder + 1
			, ItemTypeCode			= @ItemTypeCode
			, ItemCode				= @ItemCode
			, ColorCode				= @ColorCode
			, ItemDim1Code			= @ItemDim1Code
			, ItemDim2Code			= @ItemDim2Code
			, ItemDim3Code			= @ItemDim3Code
			, Qty1					= @Qty1
			, Qty2					= 0
			, BatchCode				= @BatchCode
			, SectionCode			= SPACE(0)
			, SerialNumber			= @SerialNumber
			, UsedBarcode			= @Barcode
			, CountingLineSumID		= NULL
			, CountingHeaderID		= @CountingHeaderID
			, CreatedDate			= @CreatedDate
			, CreatedUserName		= @CreatedUserName
			, LastUpdatedDate		= @CreatedDate
			, LastUpdatedUserName	= @CreatedUserName


		INSERT tpCountingITAttribute(CountingLineID, AttributeTypeCode, AttributeCode, CreatedDate, CreatedUserName, LastUpdatedDate, LastUpdatedUserName)
		SELECT @CountingLineID, 1, @ItAtt01, @CreatedDate, @CreatedUserName, @CreatedDate, @CreatedUserName
		WHERE @ItAtt01 <> SPACE(0)

		INSERT tpCountingITAttribute(CountingLineID, AttributeTypeCode, AttributeCode, CreatedDate, CreatedUserName, LastUpdatedDate, LastUpdatedUserName)
		SELECT @CountingLineID, 2, @ItAtt02, @CreatedDate, @CreatedUserName, @CreatedDate, @CreatedUserName
		WHERE @ItAtt02 <> SPACE(0)

		INSERT tpCountingITAttribute(CountingLineID, AttributeTypeCode, AttributeCode, CreatedDate, CreatedUserName, LastUpdatedDate, LastUpdatedUserName)
		SELECT @CountingLineID, 3, @ItAtt03, @CreatedDate, @CreatedUserName, @CreatedDate, @CreatedUserName
		WHERE @ItAtt03 <> SPACE(0)

		INSERT tpCountingITAttribute(CountingLineID, AttributeTypeCode, AttributeCode, CreatedDate, CreatedUserName, LastUpdatedDate, LastUpdatedUserName)
		SELECT @CountingLineID, 4, @ItAtt04, @CreatedDate, @CreatedUserName, @CreatedDate, @CreatedUserName
		WHERE @ItAtt04 <> SPACE(0)

		INSERT tpCountingITAttribute(CountingLineID, AttributeTypeCode, AttributeCode, CreatedDate, CreatedUserName, LastUpdatedDate, LastUpdatedUserName)
		SELECT @CountingLineID, 5, @ItAtt05, @CreatedDate, @CreatedUserName, @CreatedDate, @CreatedUserName
		WHERE @ItAtt05 <> SPACE(0)

		COMMIT TRAN

		SELECT
			  CountingHeaderID		= trCountingLine.CountingHeaderID
			, CountingLineID		= trCountingLine.CountingLineID
			, ItemTypeCode			= trCountingLine.ItemTypeCode
			, ItemCode				= trCountingLine.ItemCode
			, ItemDescription		= ISNULL(cdItemDesc.ItemDescription, SPACE(0))
			, ColorCode				= trCountingLine.ColorCode
			, ItemDim1Code			= trCountingLine.ItemDim1Code
			, ItemDim2Code			= trCountingLine.ItemDim2Code
			, ItemDim3Code			= trCountingLine.ItemDim3Code
			, Qty1					= trCountingLine.Qty1
			, Qty2					= 0
			, BatchCode				= trCountingLine.BatchCode
			, UsedBarcode			= trCountingLine.UsedBarcode
			, SerialNumber			= trCountingLine.SerialNumber
		FROM trCountingLine WITH(NOLOCK)
			LEFT OUTER JOIN cdItemDesc WITH(NOLOCK)
				ON cdItemDesc.ItemTypeCode = trCountingLine.ItemTypeCode
				AND cdItemDesc.ItemCode = trCountingLine.ItemCode
				AND cdItemDesc.LangCode = @LangCode
		WHERE trCountingLine.CountingLineID = @CountingLineID
	END
	ELSE
	BEGIN
		IF OBJECT_ID('tempdb..#CountingLineIDList') IS NOT NULL DROP TABLE #CountingLineIDList
		CREATE TABLE #CountingLineIDList
		(
			ID					int,
			CountingHeaderID	uniqueidentifier, 
			CountingLineID		uniqueidentifier, 
			Qty1				float, 
			RemoveQty1			float
		)

		BEGIN TRAN

			INSERT #CountingLineIDList(ID, CountingHeaderID, CountingLineID, Qty1, RemoveQty1)
			SELECT DISTINCT ROW_NUMBER() OVER (ORDER BY Qty1 ASC) - 1, CountingHeaderID, CountingLineID, Qty1, 0
			FROM trCountingLine WITH(NOLOCK)
			WHERE trCountingLine.CountingHeaderID = @CountingHeaderID
			AND trCountingLine.ItemTypeCode = @ItemTypeCode
			AND trCountingLine.ItemCode = @ItemCode
			AND trCountingLine.ColorCode = @ColorCode
			AND trCountingLine.ItemDim1Code = @ItemDim1Code
			AND trCountingLine.ItemDim2Code = @ItemDim2Code
			AND trCountingLine.ItemDim3Code = @ItemDim3Code
			AND ISNULL(trCountingLine.BatchCode, N'') = @BatchCode
			AND ISNULL(trCountingLine.SerialNumber, N'') = @SerialNumber

			DECLARE @Counter		int = 0
			DECLARE @RecordCount	int = ISNULL((SELECT COUNT(*) FROM #CountingLineIDList), 0)
			DECLARE @LineQty1		float = 0
			DECLARE @RemoveQty1		float = 0

			WHILE @Counter < @RecordCount AND @Qty1 > 0 
			BEGIN
				SELECT @LineQty1 = Qty1 
				FROM #CountingLineIDList WHERE ID = @Counter

				SELECT @RemoveQty1 = CASE WHEN @LineQty1 <= @Qty1 THEN @LineQty1 ELSE @Qty1 END

				UPDATE #CountingLineIDList
				SET RemoveQty1 = @RemoveQty1
				FROM #CountingLineIDList
				WHERE ID = @Counter
				
				SET @Qty1 = @Qty1 - @RemoveQty1
				SET @Counter = @Counter + 1
			END

			UPDATE trCountingLine
			SET Qty1 = CountingLineIDList.Qty1 - CountingLineIDList.RemoveQty1
			FROM trCountingLine WITH(NOLOCK)
				INNER JOIN #CountingLineIDList CountingLineIDList 
					ON CountingLineIDList.CountingLineID = trCountingLine.CountingLineID
					AND CountingLineIDList.Qty1 - CountingLineIDList.RemoveQty1 > 0

			DELETE 
			FROM tpCountingITAttribute 
			FROM tpCountingITAttribute WITH(NOLOCK)
				INNER JOIN #CountingLineIDList CountingLineIDList 
					ON CountingLineIDList.CountingLineID = tpCountingITAttribute.CountingLineID
					AND CountingLineIDList.Qty1 - CountingLineIDList.RemoveQty1 = 0

			DELETE 
			FROM trCountingLine 
			FROM trCountingLine WITH(NOLOCK)
				INNER JOIN #CountingLineIDList CountingLineIDList 
					ON CountingLineIDList.CountingLineID = trCountingLine.CountingLineID
					AND CountingLineIDList.Qty1 - CountingLineIDList.RemoveQty1 = 0

		COMMIT TRAN		
		
		SELECT
			  CountingHeaderID		= CountingHeaderID
			, CountingLineID		= CountingLineID
			, ItemTypeCode			= @ItemTypeCode		
			, ItemCode				= @ItemCode
			, ItemDescription		= ISNULL(cdItemDesc.ItemDescription, SPACE(0))
			, ColorCode				= @ColorCode
			, ItemDim1Code			= @ItemDim1Code
			, ItemDim2Code			= @ItemDim2Code
			, ItemDim3Code			= @ItemDim3Code
			, Qty1					= Qty1
			, Qty2					= 0
			, BatchCode				= @BatchCode
			, UsedBarcode			= @Barcode
			, SerialNumber			= @SerialNumber
		FROM #CountingLineIDList 
			LEFT OUTER JOIN cdItemDesc WITH(NOLOCK)
				ON cdItemDesc.ItemTypeCode = @ItemTypeCode
				AND cdItemDesc.ItemCode = @ItemCode
				AND cdItemDesc.LangCode = @LangCode

	END
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
