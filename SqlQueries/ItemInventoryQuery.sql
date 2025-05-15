-- ItemInventory
-- Stok miktarlarını ve kullanılabilir stok miktarlarını getiren sorgu

SELECT        Inventory.CompanyCode, Inventory.OfficeCode, Inventory.StoreTypeCode, Inventory.StoreCode, Inventory.WarehouseCode, Inventory.ItemTypeCode, Inventory.ItemCode, Inventory.ColorCode, Inventory.ItemDim1Code, 
                         Inventory.ItemDim2Code, Inventory.ItemDim3Code, ROUND(SUM(Inventory.InventoryQty1), dbo.cdUnitOfMeasure.RoundDigit) AS InventoryQty1, ROUND(SUM(Inventory.ReserveQty1), dbo.cdUnitOfMeasure.RoundDigit) 
                         AS RemainingReserveQty1, ROUND(SUM(Inventory.DispOrderQty1), dbo.cdUnitOfMeasure.RoundDigit) AS RemainingDispOrderQty1, ROUND(SUM(Inventory.PickingQty1), dbo.cdUnitOfMeasure.RoundDigit) 
                         AS RemainingPickingQty1, ROUND(SUM(Inventory.InventoryQty1) - (SUM(Inventory.ReserveQty1) + SUM(Inventory.DispOrderQty1) + SUM(Inventory.PickingQty1)), dbo.cdUnitOfMeasure.RoundDigit) 
                         AS AvailableInventoryQty1
FROM            dbo.cdItem WITH (NOLOCK) INNER JOIN
                             (SELECT        CompanyCode, OfficeCode, StoreTypeCode, StoreCode, WarehouseCode, ItemTypeCode, ItemCode, ColorCode, ItemDim1Code, ItemDim2Code, ItemDim3Code, Qty1 AS PickingQty1, 0 AS ReserveQty1, 
                                                         0 AS DispOrderQty1, 0 AS InventoryQty1
                               FROM            dbo.PickingStates
                               UNION
                               SELECT        CompanyCode, OfficeCode, StoreTypeCode, StoreCode, WarehouseCode, ItemTypeCode, ItemCode, ColorCode, ItemDim1Code, ItemDim2Code, ItemDim3Code, 0 AS PickingQty1, Qty1 AS ReserveQty1, 
                                                        0 AS DispOrderQty1, 0 AS InventoryQty1
                               FROM            dbo.ReserveStates
                               UNION
                               SELECT        CompanyCode, OfficeCode, StoreTypeCode, StoreCode, WarehouseCode, ItemTypeCode, ItemCode, ColorCode, ItemDim1Code, ItemDim2Code, ItemDim3Code, 0 AS PickingQty1, 0 AS ReserveQty1, 
                                                        Qty1 AS DispOrderQty1, 0 AS InventoryQty1
                               FROM            dbo.DispOrderStates
                               UNION
                               SELECT        CompanyCode, OfficeCode, StoreTypeCode, StoreCode, WarehouseCode, ItemTypeCode, ItemCode, ColorCode, ItemDim1Code, ItemDim2Code, ItemDim3Code, 0 AS PickingQty1, 0 AS ReserveQty1, 
                                                        0 AS DispOrderQty1, SUM(In_Qty1 - Out_Qty1) AS InventoryQty1
                               FROM            dbo.trStock WITH (NOLOCK)
                               GROUP BY CompanyCode, OfficeCode, StoreTypeCode, StoreCode, WarehouseCode, ItemTypeCode, ItemCode, ColorCode, ItemDim1Code, ItemDim2Code, ItemDim3Code) AS Inventory ON 
                         dbo.cdItem.ItemTypeCode = Inventory.ItemTypeCode AND dbo.cdItem.ItemCode = Inventory.ItemCode INNER JOIN
                         dbo.cdUnitOfMeasure WITH (NOLOCK) ON dbo.cdItem.UnitOfMeasureCode1 = dbo.cdUnitOfMeasure.UnitOfMeasureCode
GROUP BY Inventory.CompanyCode, Inventory.OfficeCode, Inventory.StoreTypeCode, Inventory.StoreCode, Inventory.WarehouseCode, Inventory.ItemTypeCode, Inventory.ItemCode, Inventory.ColorCode, Inventory.ItemDim1Code, 
                         Inventory.ItemDim2Code, Inventory.ItemDim3Code, dbo.cdUnitOfMeasure.RoundDigit
