-- ProductAttributeValues
-- Ürün özelliklerinin değerlerini getiren sorgu

SELECT ItemAttribute.AttributeTypeCode
		, AttributeTypeDescription
		, AttributeCode
		, AttributeDescription
		, ItemAttribute.ProductHierarchyFilter
		, ItemAttribute.IsBlocked
	FROM ItemAttribute({LangCode}), ItemAttributeType({LangCode})
	WHERE ItemAttribute.AttributeTypeCode = ItemAttributeType.AttributeTypeCode
	AND ItemAttribute.ItemTypeCode = ItemAttributeType.ItemTypeCode
	AND ItemAttribute.ItemTypeCode = 1
	AND AttributeCode <> SPACE(0)
