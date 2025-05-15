-- ProductColorAttributes
-- Ürün renk özniteliklerini PIVOT kullanarak tek bir satırda getiren sorgu

SELECT        ItemTypeCode, ItemCode, ColorCode, ProductColorAtt01 = ISNULL([1], SPACE(0)), ProductColorAtt02 = ISNULL([2], SPACE(0)), ProductColorAtt03 = ISNULL([3], SPACE(0)), ProductColorAtt04 = ISNULL([4], SPACE(0)), 
                         ProductColorAtt05 = ISNULL([5], SPACE(0)), ProductColorAtt06 = ISNULL([6], SPACE(0)), ProductColorAtt07 = ISNULL([7], SPACE(0)), ProductColorAtt08 = ISNULL([8], SPACE(0)), ProductColorAtt09 = ISNULL([9], SPACE(0)), 
                         ProductColorAtt10 = ISNULL([10], SPACE(0)), ProductColorAtt11 = ISNULL([11], SPACE(0)), ProductColorAtt12 = ISNULL([12], SPACE(0)), ProductColorAtt13 = ISNULL([13], SPACE(0)), ProductColorAtt14 = ISNULL([14], SPACE(0)), 
                         ProductColorAtt15 = ISNULL([15], SPACE(0))
FROM            (SELECT        prItemVariant.ItemTypeCode, prItemVariant.ItemCode, prItemVariant.ColorCode, AttributeTypeCode, AttributeCode
                          FROM            (SELECT DISTINCT ItemTypeCode, ItemCode, ColorCode
                                                    FROM            prItemVariant WITH (NOLOCK)
                                                    WHERE        ItemTypeCode = 1) AS prItemVariant LEFT OUTER JOIN
                                                    prProductColorAttribute WITH (NOLOCK) ON 1 = 1 AND prItemVariant.ItemTypeCode = prProductColorAttribute.ProductTypeCode AND prItemVariant.ItemCode = prProductColorAttribute.ProductCode AND 
                                                    prItemVariant.ColorCode = prProductColorAttribute.ColorCode) AS DataTable PIVOT (MAX(AttributeCode) FOR AttributeTypeCode IN ([1], [2], [3], [4], [5], [6], [7], [8], [9], [10], [11], [12], [13], [14], [15])) AS PIVOTTABLE
