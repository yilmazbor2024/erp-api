-- ProductList
-- Ürün listesi için temel bilgileri getiren sorgu

SELECT ProductCode            = cdItem.ItemCode
		, ProductDescription     = ISNULL(ItemDescription, SPACE(0))
		, cdItem.ProductTypeCode
		, ProductTypeDescription = ISNULL((SELECT ProductTypeDescription FROM bsProductTypeDesc WITH(NOLOCK) WHERE bsProductTypeDesc.ProductTypeCode = cdItem.ProductTypeCode AND bsProductTypeDesc.LangCode = {LangCode}),SPACE(0))
		, cdItem.ItemDimTypeCode
		, ItemDimTypeDescription = ISNULL((SELECT ItemDimTypeDescription FROM ItemDimType({LangCode}) WHERE ItemDimType.ItemDimTypeCode = cdItem.ItemDimTypeCode),SPACE(0))
		, UnitOfMeasureCode1
		, UnitOfMeasureCode2
		, CompanyBrandCode = ISNULL((SELECT N'{' + CompanyBrandCode + N'}'
									FROM (SELECT DISTINCT CompanyBrandCode
											FROM prItemCompanyBrand WITH(NOLOCK) 														
											WHERE prItemCompanyBrand.ItemTypeCode	= cdItem.ItemTypeCode
											AND prItemCompanyBrand.ItemCode			= cdItem.ItemCode	
											AND prItemCompanyBrand.IsBlocked = 0	
											AND prItemCompanyBrand.CompanyBrandCode <> SPACE(0)
											) AS Sizes 									
										ORDER BY CompanyBrandCode FOR XML PATH('')), SPACE(0))
		, cdItem.UsePOS
		, cdItem.UseStore
		, cdItem.UseRoll
		, cdItem.UseBatch
		, cdItem.GenerateSerialNumber
		, cdItem.UseSerialNumber
		, cdItem.IsUTSDeclaratedItem
		, cdItem.CreatedDate 
		, cdItem.LastUpdatedDate
		, cdItem.IsBlocked
	FROM cdItem WITH(NOLOCK)
			LEFT OUTER JOIN cdItemDesc WITH(NOLOCK) ON cdItemDesc.ItemTypeCode = cdItem.ItemTypeCode AND cdItemDesc.ItemCode = cdItem.ItemCode AND cdItemDesc.LangCode = {LangCode}			

	WHERE cdItem.ItemTypeCode      = 1 
	AND cdItem.ItemCode <> SPACE(0)
