-- ProductAttributeType
-- Ürün özellik tanımlarını getiren sorgu

SELECT ItemTypeCode
		, AttributeTypeCode
		, AttributeTypeDescription
		, ProductHierarchyFilter
		, IsRequired
		, IsBlocked
	FROM ItemAttributeType({LangCode})
	WHERE ItemTypeCode = 1 
	AND ItemTypeCode <> 0
