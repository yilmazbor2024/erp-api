WITH AllBarcodes AS (
    SELECT
        prItemBarcode.ItemTypeCode,
        prItemBarcode.ItemCode,
        prItemBarcode.ColorCode,
        prItemBarcode.ItemDim1Code,
        UsedBarcode = prItemBarcode.Barcode,
        SortOrder = 1
    FROM prItemBarcode WITH (NOLOCK)
    INNER JOIN cdItem WITH (NOLOCK)
        ON prItemBarcode.ItemTypeCode = cdItem.ItemTypeCode
        AND prItemBarcode.ItemCode = cdItem.ItemCode
    WHERE cdItem.IsBlocked = 0
    -- AND prItemBarcode.Barcode = 'TamBarkodDeğeri' -- İsteğe bağlı erken filtreleme

    UNION ALL

    SELECT
        prItemBatchBarcode.ItemTypeCode,
        prItemBatchBarcode.ItemCode,
        prItemBatchBarcode.ColorCode,
        prItemBatchBarcode.ItemDim1Code,
        UsedBarcode = prItemBatchBarcode.BatchBarcode,
        SortOrder = 2
    FROM prItemBatchBarcode WITH (NOLOCK)
    INNER JOIN cdItem WITH (NOLOCK)
        ON prItemBatchBarcode.ItemTypeCode = cdItem.ItemTypeCode
        AND prItemBatchBarcode.ItemCode = cdItem.ItemCode
    WHERE cdItem.IsBlocked = 0
    -- AND prItemBatchBarcode.BatchBarcode = 'TamBarkodDeğeri' -- İsteğe bağlı erken filtreleme

    UNION ALL

    SELECT
        cdRoll.ItemTypeCode,
        cdRoll.ItemCode,
        cdRoll.ColorCode,
        cdRoll.ItemDim1Code,
        UsedBarcode = cdRoll.RollNumber,
        SortOrder = 3
    FROM cdRoll WITH (NOLOCK)
    INNER JOIN cdItem WITH (NOLOCK)
        ON cdRoll.ItemCode = cdItem.ItemCode
        AND cdRoll.ItemTypeCode = cdItem.ItemTypeCode
    WHERE cdItem.IsBlocked = 0
    -- AND cdRoll.RollNumber = 'TamBarkodDeğeri' -- İsteğe bağlı erken filtreleme
),
GroupedBarcodes AS (
    SELECT
        ItemTypeCode,
        ItemCode,
        ColorCode,
        ItemDim1Code,
        AllUsedBarcodes = STRING_AGG(UsedBarcode, ', ') WITHIN GROUP (ORDER BY SortOrder, UsedBarcode)
    FROM AllBarcodes
    GROUP BY ItemTypeCode, ItemCode, ColorCode, ItemDim1Code
),
FilteredWarehouses AS (
    SELECT DISTINCT
        w.WarehouseCode,
        w.OfficeCode -- OfficeCode'u da buraya ekleyelim
    FROM cdWarehouse AS w WITH (NOLOCK)
    INNER JOIN cdCurrAcc AS ca WITH (NOLOCK)
        ON ca.CurrAccTypeCode = w.CurrAccTypeCode
        AND ca.CurrAccCode = w.CurrAccCode
    WHERE
        (CASE WHEN 0 = 1 THEN ca.CustomerTypeCode ELSE 1 END = 2
        OR CASE WHEN 0 = 1 THEN ca.CustomerTypeCode ELSE 2 END = 1
        OR ((w.WarehouseOwnerCode IN ( 1, 2, 3 ) OR ca.CustomerTypeCode = 0)
            AND w.WarehouseOwnerCode <> 4))
),
InventoryRaw AS (
    SELECT
        trStock.CompanyCode,
        trStock.OfficeCode,
        trStock.StoreCode,
        trStock.WarehouseCode,
        ProductCode = trStock.ItemCode,
        trStock.ColorCode,
        trStock.ItemDim1Code,
        InventoryQty1 = SUM(TRY_CAST(trStock.In_Qty1 AS FLOAT) - TRY_CAST(trStock.Out_Qty1 AS FLOAT)),
        TransferNotApprovedQty1 = 0
    FROM trStock WITH (NOLOCK)
    WHERE
        trStock.ItemTypeCode = 1
        AND trStock.OperationDate <= CAST(GETDATE() AS DATETIME)
        AND trStock.WarehouseCode IN (SELECT WarehouseCode FROM FilteredWarehouses)
    GROUP BY
        trStock.CompanyCode,
        trStock.OfficeCode,
        trStock.StoreCode,
        trStock.WarehouseCode,
        trStock.ItemCode,
        trStock.ColorCode,
        trStock.ItemDim1Code

    UNION ALL

    SELECT
        ITNA.CompanyCode,
        ITNA.OfficeCode,
        ITNA.StoreCode,
        ITNA.WarehouseCode,
        ProductCode = ITNA.ItemCode,
        ITNA.ColorCode,
        ITNA.ItemDim1Code,
        InventoryQty1 = 0,
        TransferNotApprovedQty1 = SUM(TRY_CAST(ITNA.TransferNotApprovedQty1 AS FLOAT))
    FROM ItemTransferNotApprovedByDate(CAST('20250528' AS DATETIME)) AS ITNA
    WHERE
        ITNA.ItemTypeCode = 1
        AND ITNA.WarehouseCode IN (SELECT WarehouseCode FROM FilteredWarehouses)
    GROUP BY
        ITNA.CompanyCode,
        ITNA.OfficeCode,
        ITNA.StoreCode,
        ITNA.WarehouseCode,
        ITNA.ItemCode,
        ITNA.ColorCode,
        ITNA.ItemDim1Code

    UNION ALL

    SELECT
        tf.CompanyCode,
        Stores.OfficeCode,
        StoreCode = Stores.CurrAccCode,
        WarehouseCode = tf.WarehouseCode,
        ProductCode = ISNULL(tsl.ItemCode, til.ItemCode),
        ColorCode = ISNULL(tsl.ColorCode, til.ColorCode),
        ItemDim1Code = ISNULL(tsl.ItemDim1Code, til.ItemDim1Code),
        InventoryQty1 = 0,
        TransferNotApprovedQty1 = SUM(ISNULL(TRY_CAST(tsl.Qty1 AS FLOAT), TRY_CAST(til.Qty1 AS FLOAT)))
    FROM trForthcomingItems AS tf WITH (NOLOCK)
    INNER JOIN cdCurrAcc AS Stores WITH (NOLOCK)
        ON Stores.CurrAccTypeCode = 5
        AND Stores.CurrAccCode = tf.StoreCode
        AND Stores.CurrAccCode <> SPACE(0)
    LEFT JOIN trInvoiceLine AS til WITH (NOLOCK)
        ON til.InvoiceHeaderID = tf.HeaderID
        AND til.ItemTypeCode = 1
        AND tf.ProcessFlowCode = 7
    LEFT JOIN trShipmentLine AS tsl WITH (NOLOCK)
        ON tsl.ShipmentHeaderID = tf.HeaderID
        AND tsl.ItemTypeCode = 1
        AND tf.ProcessFlowCode = 6
    WHERE
        tf.IsPosted = 0
        AND (til.InvoiceHeaderID IS NOT NULL OR tsl.ShipmentHeaderID IS NOT NULL)
        AND tf.TransactionDate <= CAST('20250528' AS DATETIME)
        AND 0 = 1 -- Bu koşulun ne anlama geldiğini kontrol edin ve gerekiyorsa düzeltin.
        AND tf.WarehouseCode IN (SELECT WarehouseCode FROM FilteredWarehouses)
    GROUP BY
        tf.CompanyCode,
        Stores.OfficeCode,
        Stores.CurrAccCode,
        tf.WarehouseCode,
        ISNULL(tsl.ItemCode, til.ItemCode),
        ISNULL(tsl.ColorCode, til.ColorCode),
        ISNULL(tsl.ItemDim1Code, til.ItemDim1Code)

    UNION ALL

    SELECT
        trf.CompanyCode,
        Stores.OfficeCode,
        StoreCode = CASE WHEN Stores.CurrAccTypeCode = 5 THEN Stores.CurrAccCode ELSE SPACE(0) END,
        WarehouseCode = trf.WarehouseCode,
        ProductCode = ISNULL(tsl.ItemCode, til.ItemCode),
        ColorCode = ISNULL(tsl.ColorCode, til.ColorCode),
        ItemDim1Code = ISNULL(tsl.ItemDim1Code, til.ItemDim1Code),
        InventoryQty1 = 0,
        TransferNotApprovedQty1 = SUM(ISNULL(TRY_CAST(tsl.Qty1 AS FLOAT), TRY_CAST(til.Qty1 AS FLOAT)))
    FROM trReturnedForthcomingItems AS trf WITH (NOLOCK)
    INNER JOIN cdWarehouse AS Stores WITH (NOLOCK)
        ON Stores.WarehouseCode = trf.WarehouseCode
    LEFT JOIN trInvoiceLine AS til WITH (NOLOCK)
        ON til.InvoiceHeaderID = trf.HeaderID
        AND til.ItemTypeCode = 1
        AND trf.ProcessFlowCode = 7
    LEFT JOIN trShipmentLine AS tsl WITH (NOLOCK)
        ON tsl.ShipmentHeaderID = trf.HeaderID
        AND tsl.ItemTypeCode = 1
        AND trf.ProcessFlowCode = 6
    WHERE
        trf.IsPosted = 0
        AND (til.InvoiceHeaderID IS NOT NULL OR tsl.ShipmentHeaderID IS NOT NULL)
        AND trf.TransactionDate <= CAST('20250528' AS DATETIME)
        AND 0 = 1 -- Bu koşulun ne anlama geldiğini kontrol edin ve gerekiyorsa düzeltin.
        AND trf.WarehouseCode IN (SELECT WarehouseCode FROM FilteredWarehouses)
    GROUP BY
        trf.CompanyCode,
        Stores.OfficeCode,
        Stores.CurrAccTypeCode,
        Stores.CurrAccCode,
        trf.WarehouseCode,
        ISNULL(tsl.ItemCode, til.ItemCode),
        ISNULL(tsl.ColorCode, til.ColorCode),
        ISNULL(tsl.ItemDim1Code, til.ItemDim1Code)
),
AggregatedInventory AS (
    SELECT
        CompanyCode = 1,
        OfficeCode = IR.OfficeCode, -- Orjinal OfficeCode'u koruyoruz
        WarehouseCode = IR.WarehouseCode, -- Orjinal WarehouseCode'u koruyoruz
        ProductCode = IR.ProductCode,
        ColorCode = IR.ColorCode,
        ItemDim1Code = IR.ItemDim1Code,
        InventoryQty1 = SUM(IR.InventoryQty1),
        TransferNotApprovedQty1 = SUM(IR.TransferNotApprovedQty1)
    FROM InventoryRaw AS IR
    WHERE TRY_CAST(IR.CompanyCode AS INT) = 1
    GROUP BY
        IR.OfficeCode,
        IR.WarehouseCode,
        IR.ProductCode,
        IR.ColorCode,
        IR.ItemDim1Code
)
SELECT
    CompanyCode           = 1,
    OfficeCode            = ISNULL(AI.OfficeCode, 'N/A'), -- Nihai seçimde ISNULL kullan
    OfficeDescription     = ISNULL(cdOfficeDesc.OfficeDescription, SPACE(0)),
    WarehouseCode         = ISNULL(AI.WarehouseCode, 'N/A'), -- Nihai seçimde ISNULL kullan
    WareHouseDescription  = ISNULL(cdWareHouseDesc.WareHouseDescription, SPACE(0)),
    ProductCode           = piv.ItemCode,
    ProductDescription    = ISNULL(cdItemDesc.ItemDescription, SPACE(0)),
    ColorCode             = piv.ColorCode,
    ColorDescription      = ISNULL(cdColorDesc.ColorDescription, SPACE(0)),
    Beden                 = piv.ItemDim1Code,
    UnitOfMeasureCode1    = cdItem.UnitOfMeasureCode1,
    EnvanterMiktar        = ISNULL(AI.InventoryQty1 + AI.TransferNotApprovedQty1, 0),
    AllBarcodesForVariant = ISNULL(GB.AllUsedBarcodes, SPACE(0))
FROM
    prItemVariant AS piv WITH (NOLOCK)
INNER JOIN
    cdItem WITH (NOLOCK) ON piv.ItemTypeCode = cdItem.ItemTypeCode
                        AND piv.ItemCode = cdItem.ItemCode
LEFT JOIN
    cdItemDesc WITH (NOLOCK) ON piv.ItemTypeCode = cdItemDesc.ItemTypeCode
                            AND piv.ItemCode = cdItemDesc.ItemCode
                            AND cdItemDesc.LangCode = N'TR'
LEFT JOIN
    cdColorDesc WITH (NOLOCK) ON piv.ColorCode = cdColorDesc.ColorCode
                             AND cdColorDesc.LangCode = N'TR'
LEFT JOIN
    AggregatedInventory AS AI ON piv.ItemCode = AI.ProductCode
                              AND piv.ColorCode = AI.ColorCode
                              AND piv.ItemDim1Code = AI.ItemDim1Code
LEFT JOIN
    GroupedBarcodes AS GB ON piv.ItemTypeCode = GB.ItemTypeCode
                          AND piv.ItemCode = GB.ItemCode
                          AND piv.ColorCode = GB.ColorCode
                          AND piv.ItemDim1Code = GB.ItemDim1Code
LEFT JOIN -- Bu birleşimi ana sorguya taşıdık
    cdOfficeDesc WITH (NOLOCK) ON AI.OfficeCode = cdOfficeDesc.OfficeCode
                              AND cdOfficeDesc.LangCode = N'TR'
LEFT JOIN -- Bu birleşimi ana sorguya taşıdık
    cdWareHouseDesc WITH (NOLOCK) ON AI.WarehouseCode = cdWareHouseDesc.WarehouseCode
                                  AND cdWareHouseDesc.LangCode = N'TR'
--WHERE
--ISNULL(GB.AllUsedBarcodes, '') LIKE '8693052270280'; -- Barkod filtresi burada
    -- VEYA, tam eşleşme arıyorsanız:
    -- ISNULL(GB.AllUsedBarcodes, '') = 'TamBarkodDeğeri';
    -- Eğer birden fazla barkoddan herhangi birini arıyorsanız:
    -- ISNULL(GB.AllUsedBarcodes, '') LIKE '%Barkod1%' OR ISNULL(GB.AllUsedBarcodes, '') LIKE '%Barkod2%';
    -- VEYA, daha okunabilir bir şekilde:
    -- EXISTS (SELECT 1 FROM STRING_SPLIT(ISNULL(GB.AllUsedBarcodes, ''), ', ') WHERE value = 'BarkodDeğeri')