-- AllVendorPriceListLines
-- Tüm tedarikçi fiyat listesi satırlarını getiren sorgu

SELECT        dbo.trVendorPriceListHeader.VendorPriceListHeaderID, dbo.trVendorPriceListHeader.VendorPriceListNumber, dbo.trVendorPriceListHeader.VendorPriceListDate, dbo.trVendorPriceListHeader.VendorPriceListTime, 
                         dbo.trVendorPriceListHeader.CompanyCode, dbo.trVendorPriceListHeader.VendorTypeCode, dbo.trVendorPriceListHeader.VendorCode, dbo.trVendorPriceListHeader.ValidDate, dbo.trVendorPriceListHeader.Description, 
                         dbo.trVendorPriceListHeader.IsTaxIncluded, dbo.trVendorPriceListHeader.IsCompleted, dbo.trVendorPriceListHeader.IsPrinted, dbo.trVendorPriceListHeader.IsLocked, dbo.trVendorPriceListHeader.IsConfirmed, 
                         dbo.trVendorPriceListHeader.ConfirmedUserName, dbo.trVendorPriceListHeader.ConfirmedDate, dbo.trVendorPriceListHeader.ApplicationCode, dbo.trVendorPriceListHeader.ApplicationID, 
                         dbo.trVendorPriceListHeader.CreatedUserName, dbo.trVendorPriceListHeader.CreatedDate, dbo.trVendorPriceListHeader.LastUpdatedUserName, dbo.trVendorPriceListHeader.LastUpdatedDate, 
                         dbo.trVendorPriceListLine.VendorPriceListLineID, dbo.trVendorPriceListLine.SortOrder, dbo.trVendorPriceListLine.ItemTypeCode, dbo.trVendorPriceListLine.ItemCode, dbo.trVendorPriceListLine.ColorCode, 
                         dbo.trVendorPriceListLine.ItemDim1Code, dbo.trVendorPriceListLine.ItemDim2Code, dbo.trVendorPriceListLine.ItemDim3Code, dbo.trVendorPriceListLine.UnitOfMeasureCode, dbo.trVendorPriceListLine.PurchasePlanCode, 
                         dbo.trVendorPriceListLine.LineDescription, dbo.trVendorPriceListLine.DocCurrencyCode, dbo.trVendorPriceListLine.Price, dbo.trVendorPriceListLine.IsDisabled, dbo.trVendorPriceListLine.DisableDate
FROM            dbo.trVendorPriceListHeader WITH (NOLOCK) INNER JOIN
                         dbo.trVendorPriceListLine WITH (NOLOCK) ON dbo.trVendorPriceListLine.VendorPriceListHeaderID = dbo.trVendorPriceListHeader.VendorPriceListHeaderID
