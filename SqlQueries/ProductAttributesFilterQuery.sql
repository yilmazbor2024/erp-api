-- ProductAttributesFilter
-- Ürün özniteliklerini CASE WHEN kullanarak tek bir satırda getiren alternatif sorgu

SELECT        ItemTypeCode, ItemCode, MAX(CASE AttributeTypeCode WHEN 1 THEN AttributeCode ELSE NULL END) AS ProductAtt01, MAX(CASE AttributeTypeCode WHEN 2 THEN AttributeCode ELSE NULL END) AS ProductAtt02, 
                         MAX(CASE AttributeTypeCode WHEN 3 THEN AttributeCode ELSE NULL END) AS ProductAtt03, MAX(CASE AttributeTypeCode WHEN 4 THEN AttributeCode ELSE NULL END) AS ProductAtt04, 
                         MAX(CASE AttributeTypeCode WHEN 5 THEN AttributeCode ELSE NULL END) AS ProductAtt05, MAX(CASE AttributeTypeCode WHEN 6 THEN AttributeCode ELSE NULL END) AS ProductAtt06, 
                         MAX(CASE AttributeTypeCode WHEN 7 THEN AttributeCode ELSE NULL END) AS ProductAtt07, MAX(CASE AttributeTypeCode WHEN 8 THEN AttributeCode ELSE NULL END) AS ProductAtt08, 
                         MAX(CASE AttributeTypeCode WHEN 9 THEN AttributeCode ELSE NULL END) AS ProductAtt09, MAX(CASE AttributeTypeCode WHEN 10 THEN AttributeCode ELSE NULL END) AS ProductAtt10, 
                         MAX(CASE AttributeTypeCode WHEN 11 THEN AttributeCode ELSE NULL END) AS ProductAtt11, MAX(CASE AttributeTypeCode WHEN 12 THEN AttributeCode ELSE NULL END) AS ProductAtt12, 
                         MAX(CASE AttributeTypeCode WHEN 13 THEN AttributeCode ELSE NULL END) AS ProductAtt13, MAX(CASE AttributeTypeCode WHEN 14 THEN AttributeCode ELSE NULL END) AS ProductAtt14, 
                         MAX(CASE AttributeTypeCode WHEN 15 THEN AttributeCode ELSE NULL END) AS ProductAtt15, MAX(CASE AttributeTypeCode WHEN 16 THEN AttributeCode ELSE NULL END) AS ProductAtt16, 
                         MAX(CASE AttributeTypeCode WHEN 17 THEN AttributeCode ELSE NULL END) AS ProductAtt17, MAX(CASE AttributeTypeCode WHEN 18 THEN AttributeCode ELSE NULL END) AS ProductAtt18, 
                         MAX(CASE AttributeTypeCode WHEN 19 THEN AttributeCode ELSE NULL END) AS ProductAtt19, MAX(CASE AttributeTypeCode WHEN 20 THEN AttributeCode ELSE NULL END) AS ProductAtt20
FROM            dbo.prItemAttribute WITH (NOLOCK)
WHERE        (ItemTypeCode = 1)
GROUP BY ItemTypeCode, ItemCode
