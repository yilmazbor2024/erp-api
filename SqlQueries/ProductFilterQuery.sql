-- ProductFilter
-- Ürün filtreleme için kullanılan sorgu, ürün hiyerarşisi, koleksiyon ve öznitelik bilgilerini içerir

SELECT        dbo.cdItem.ItemCode AS ProductCode, dbo.cdItem.ProductTypeCode, dbo.cdItem.ItemDimTypeCode, dbo.cdItem.UnitOfMeasureCode1, dbo.cdItem.UnitOfMeasureCode2, dbo.cdItem.UnitConvertRate, dbo.cdItem.UsePOS, 
                         dbo.cdItem.UseInternet, dbo.cdItem.UseSerialNumber, dbo.cdItem.UseRoll, dbo.cdItem.UseBatch, dbo.cdItem.ShelfLife, dbo.cdItem.ItemTaxGrCode, dbo.cdItem.ItemPaymentPlanGrCode, dbo.cdItem.ItemDiscountGrCode, 
                         dbo.cdItem.PromotionGroupCode, dbo.cdItem.PromotionGroupCode2, dbo.cdItem.ItemVendorGrCode, dbo.cdItem.ItemAccountGrCode, dbo.cdItem.StorePriceLevelCode, dbo.cdItem.PerceptionOfFashionCode, 
                         dbo.cdItem.CommercialRoleCode, dbo.cdItem.IsUTSDeclaratedItem, dbo.cdItem.ProductHierarchyID, ProductHierarchy.ProductHierarchyLevelCode01, ProductHierarchy.ProductHierarchyLevelCode02, 
                         ProductHierarchy.ProductHierarchyLevelCode03, ProductHierarchy.ProductHierarchyLevelCode04, ProductHierarchy.ProductHierarchyLevelCode05, ProductHierarchy.ProductHierarchyLevelCode06, 
                         ProductHierarchy.ProductHierarchyLevelCode07, ProductHierarchy.ProductHierarchyLevelCode08, ProductHierarchy.ProductHierarchyLevelCode09, ProductHierarchy.ProductHierarchyLevelCode10, 
                         ProductCollection.SeasonCode, ProductCollection.CollectionCode, ProductCollection.StoryBoardCode, ISNULL(prItemAttribute_01.AttributeCode, SPACE(0)) AS ProductAtt01, ISNULL(prItemAttribute_02.AttributeCode, SPACE(0)) 
                         AS ProductAtt02, ISNULL(prItemAttribute_03.AttributeCode, SPACE(0)) AS ProductAtt03, ISNULL(prItemAttribute_04.AttributeCode, SPACE(0)) AS ProductAtt04, ISNULL(prItemAttribute_05.AttributeCode, SPACE(0)) AS ProductAtt05, 
                         ISNULL(prItemAttribute_06.AttributeCode, SPACE(0)) AS ProductAtt06, ISNULL(prItemAttribute_07.AttributeCode, SPACE(0)) AS ProductAtt07, ISNULL(prItemAttribute_08.AttributeCode, SPACE(0)) AS ProductAtt08, 
                         ISNULL(prItemAttribute_09.AttributeCode, SPACE(0)) AS ProductAtt09, ISNULL(prItemAttribute_10.AttributeCode, SPACE(0)) AS ProductAtt10, ISNULL(prItemAttribute_11.AttributeCode, SPACE(0)) AS ProductAtt11, 
                         ISNULL(prItemAttribute_12.AttributeCode, SPACE(0)) AS ProductAtt12, ISNULL(prItemAttribute_13.AttributeCode, SPACE(0)) AS ProductAtt13, ISNULL(prItemAttribute_14.AttributeCode, SPACE(0)) AS ProductAtt14, 
                         ISNULL(prItemAttribute_15.AttributeCode, SPACE(0)) AS ProductAtt15, ISNULL(prItemAttribute_16.AttributeCode, SPACE(0)) AS ProductAtt16, ISNULL(prItemAttribute_17.AttributeCode, SPACE(0)) AS ProductAtt17, 
                         ISNULL(prItemAttribute_18.AttributeCode, SPACE(0)) AS ProductAtt18, ISNULL(prItemAttribute_19.AttributeCode, SPACE(0)) AS ProductAtt19, ISNULL(prItemAttribute_20.AttributeCode, SPACE(0)) AS ProductAtt20, 
                         dbo.cdItem.CreatedDate
FROM            dbo.cdItem WITH (NOLOCK) INNER JOIN
                         dbo.dfProductHierarchy AS ProductHierarchy WITH (NOLOCK) ON dbo.cdItem.ProductHierarchyID = ProductHierarchy.ProductHierarchyID INNER JOIN
                         dbo.cdProductCollectionGr AS ProductCollection WITH (NOLOCK) ON dbo.cdItem.ProductCollectionGrCode = ProductCollection.ProductCollectionGrCode LEFT OUTER JOIN
                         dbo.prItemAttribute AS prItemAttribute_01 WITH (NOLOCK) ON prItemAttribute_01.ItemTypeCode = dbo.cdItem.ItemTypeCode AND prItemAttribute_01.ItemCode = dbo.cdItem.ItemCode AND 
                         prItemAttribute_01.AttributeTypeCode = 01 LEFT OUTER JOIN
                         dbo.prItemAttribute AS prItemAttribute_02 WITH (NOLOCK) ON prItemAttribute_02.ItemTypeCode = dbo.cdItem.ItemTypeCode AND prItemAttribute_02.ItemCode = dbo.cdItem.ItemCode AND 
                         prItemAttribute_02.AttributeTypeCode = 02 LEFT OUTER JOIN
                         dbo.prItemAttribute AS prItemAttribute_03 WITH (NOLOCK) ON prItemAttribute_03.ItemTypeCode = dbo.cdItem.ItemTypeCode AND prItemAttribute_03.ItemCode = dbo.cdItem.ItemCode AND 
                         prItemAttribute_03.AttributeTypeCode = 03 LEFT OUTER JOIN
                         dbo.prItemAttribute AS prItemAttribute_04 WITH (NOLOCK) ON prItemAttribute_04.ItemTypeCode = dbo.cdItem.ItemTypeCode AND prItemAttribute_04.ItemCode = dbo.cdItem.ItemCode AND 
                         prItemAttribute_04.AttributeTypeCode = 04 LEFT OUTER JOIN
                         dbo.prItemAttribute AS prItemAttribute_05 WITH (NOLOCK) ON prItemAttribute_05.ItemTypeCode = dbo.cdItem.ItemTypeCode AND prItemAttribute_05.ItemCode = dbo.cdItem.ItemCode AND 
                         prItemAttribute_05.AttributeTypeCode = 05 LEFT OUTER JOIN
                         dbo.prItemAttribute AS prItemAttribute_06 WITH (NOLOCK) ON prItemAttribute_06.ItemTypeCode = dbo.cdItem.ItemTypeCode AND prItemAttribute_06.ItemCode = dbo.cdItem.ItemCode AND 
                         prItemAttribute_06.AttributeTypeCode = 06 LEFT OUTER JOIN
                         dbo.prItemAttribute AS prItemAttribute_07 WITH (NOLOCK) ON prItemAttribute_07.ItemTypeCode = dbo.cdItem.ItemTypeCode AND prItemAttribute_07.ItemCode = dbo.cdItem.ItemCode AND 
                         prItemAttribute_07.AttributeTypeCode = 07 LEFT OUTER JOIN
                         dbo.prItemAttribute AS prItemAttribute_08 WITH (NOLOCK) ON prItemAttribute_08.ItemTypeCode = dbo.cdItem.ItemTypeCode AND prItemAttribute_08.ItemCode = dbo.cdItem.ItemCode AND 
                         prItemAttribute_08.AttributeTypeCode = 08 LEFT OUTER JOIN
                         dbo.prItemAttribute AS prItemAttribute_09 WITH (NOLOCK) ON prItemAttribute_09.ItemTypeCode = dbo.cdItem.ItemTypeCode AND prItemAttribute_09.ItemCode = dbo.cdItem.ItemCode AND 
                         prItemAttribute_09.AttributeTypeCode = 09 LEFT OUTER JOIN
                         dbo.prItemAttribute AS prItemAttribute_10 WITH (NOLOCK) ON prItemAttribute_10.ItemTypeCode = dbo.cdItem.ItemTypeCode AND prItemAttribute_10.ItemCode = dbo.cdItem.ItemCode AND 
                         prItemAttribute_10.AttributeTypeCode = 10 LEFT OUTER JOIN
                         dbo.prItemAttribute AS prItemAttribute_11 WITH (NOLOCK) ON prItemAttribute_11.ItemTypeCode = dbo.cdItem.ItemTypeCode AND prItemAttribute_11.ItemCode = dbo.cdItem.ItemCode AND 
                         prItemAttribute_11.AttributeTypeCode = 11 LEFT OUTER JOIN
                         dbo.prItemAttribute AS prItemAttribute_12 WITH (NOLOCK) ON prItemAttribute_12.ItemTypeCode = dbo.cdItem.ItemTypeCode AND prItemAttribute_12.ItemCode = dbo.cdItem.ItemCode AND 
                         prItemAttribute_12.AttributeTypeCode = 12 LEFT OUTER JOIN
                         dbo.prItemAttribute AS prItemAttribute_13 WITH (NOLOCK) ON prItemAttribute_13.ItemTypeCode = dbo.cdItem.ItemTypeCode AND prItemAttribute_13.ItemCode = dbo.cdItem.ItemCode AND 
                         prItemAttribute_13.AttributeTypeCode = 13 LEFT OUTER JOIN
                         dbo.prItemAttribute AS prItemAttribute_14 WITH (NOLOCK) ON prItemAttribute_14.ItemTypeCode = dbo.cdItem.ItemTypeCode AND prItemAttribute_14.ItemCode = dbo.cdItem.ItemCode AND 
                         prItemAttribute_14.AttributeTypeCode = 14 LEFT OUTER JOIN
                         dbo.prItemAttribute AS prItemAttribute_15 WITH (NOLOCK) ON prItemAttribute_15.ItemTypeCode = dbo.cdItem.ItemTypeCode AND prItemAttribute_15.ItemCode = dbo.cdItem.ItemCode AND 
                         prItemAttribute_15.AttributeTypeCode = 15 LEFT OUTER JOIN
                         dbo.prItemAttribute AS prItemAttribute_16 WITH (NOLOCK) ON prItemAttribute_16.ItemTypeCode = dbo.cdItem.ItemTypeCode AND prItemAttribute_16.ItemCode = dbo.cdItem.ItemCode AND 
                         prItemAttribute_16.AttributeTypeCode = 16 LEFT OUTER JOIN
                         dbo.prItemAttribute AS prItemAttribute_17 WITH (NOLOCK) ON prItemAttribute_17.ItemTypeCode = dbo.cdItem.ItemTypeCode AND prItemAttribute_17.ItemCode = dbo.cdItem.ItemCode AND 
                         prItemAttribute_17.AttributeTypeCode = 17 LEFT OUTER JOIN
                         dbo.prItemAttribute AS prItemAttribute_18 WITH (NOLOCK) ON prItemAttribute_18.ItemTypeCode = dbo.cdItem.ItemTypeCode AND prItemAttribute_18.ItemCode = dbo.cdItem.ItemCode AND 
                         prItemAttribute_18.AttributeTypeCode = 18 LEFT OUTER JOIN
                         dbo.prItemAttribute AS prItemAttribute_19 WITH (NOLOCK) ON prItemAttribute_19.ItemTypeCode = dbo.cdItem.ItemTypeCode AND prItemAttribute_19.ItemCode = dbo.cdItem.ItemCode AND 
                         prItemAttribute_19.AttributeTypeCode = 19 LEFT OUTER JOIN
                         dbo.prItemAttribute AS prItemAttribute_20 WITH (NOLOCK) ON prItemAttribute_20.ItemTypeCode = dbo.cdItem.ItemTypeCode AND prItemAttribute_20.ItemCode = dbo.cdItem.ItemCode AND 
                         prItemAttribute_20.AttributeTypeCode = 20
WHERE        (dbo.cdItem.ItemTypeCode = 1) AND (dbo.cdItem.ItemCode <> SPACE(0)) AND (dbo.cdItem.IsBlocked = 0)
