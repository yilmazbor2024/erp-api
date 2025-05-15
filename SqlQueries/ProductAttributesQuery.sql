-- ProductAttributes
-- Ürün özniteliklerini PIVOT kullanarak tek bir satırda getiren sorgu

SELECT        ItemTypeCode, ItemCode, ProductAtt01 = MAX(ISNULL([1], SPACE(0))), ProductAtt02 = MAX(ISNULL([2], SPACE(0))), ProductAtt03 = MAX(ISNULL([3], SPACE(0))), ProductAtt04 = MAX(ISNULL([4], SPACE(0))), 
                         ProductAtt05 = MAX(ISNULL([5], SPACE(0))), ProductAtt06 = MAX(ISNULL([6], SPACE(0))), ProductAtt07 = MAX(ISNULL([7], SPACE(0))), ProductAtt08 = MAX(ISNULL([8], SPACE(0))), ProductAtt09 = MAX(ISNULL([9], SPACE(0))), 
                         ProductAtt10 = MAX(ISNULL([10], SPACE(0))), ProductAtt11 = MAX(ISNULL([11], SPACE(0))), ProductAtt12 = MAX(ISNULL([12], SPACE(0))), ProductAtt13 = MAX(ISNULL([13], SPACE(0))), ProductAtt14 = MAX(ISNULL([14], SPACE(0))), 
                         ProductAtt15 = MAX(ISNULL([15], SPACE(0))), ProductAtt16 = MAX(ISNULL([16], SPACE(0))), ProductAtt17 = MAX(ISNULL([17], SPACE(0))), ProductAtt18 = MAX(ISNULL([18], SPACE(0))), ProductAtt19 = MAX(ISNULL([19], SPACE(0))), 
                         ProductAtt20 = MAX(ISNULL([20], SPACE(0)))
FROM            (SELECT        cdItem.ItemTypeCode, cdItem.ItemCode, AttributeTypeCode, AttributeCode
                          FROM            cdItem WITH (NOLOCK) LEFT OUTER JOIN
                                                    prItemAttribute WITH (NOLOCK) ON 1 = 1 AND cdItem.ItemTypeCode = prItemAttribute.ItemTypeCode AND cdItem.ItemCode = prItemAttribute.ItemCode
                          WHERE        cdItem.ItemTypeCode = 1) AS DataTable PIVOT (MAX(AttributeCode) FOR AttributeTypeCode IN ([1], [2], [3], [4], [5], [6], [7], [8], [9], [10], [11], [12], [13], [14], [15], [16], [17], [18], [19], [20])) AS PIVOTTABLE
GROUP BY ItemTypeCode, ItemCode
