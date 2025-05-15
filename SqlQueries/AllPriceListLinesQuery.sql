-- AllPriceListLines
-- Tüm fiyat listesi satırlarını getiren sorgu

SELECT        dbo.trPriceListHeader.PriceListHeaderID, dbo.trPriceListHeader.PriceListNumber, dbo.trPriceListHeader.PriceListDate, dbo.trPriceListHeader.PriceListTime, dbo.trPriceListHeader.PriceListTypeCode, 
                         dbo.trPriceListHeader.Description, dbo.trPriceListHeader.IsTaxIncluded, dbo.trPriceListHeader.IsCompleted, dbo.trPriceListHeader.IsPrinted, dbo.trPriceListHeader.IsLocked, dbo.trPriceListHeader.IsConfirmed, 
                         dbo.trPriceListHeader.ConfirmedUserName, dbo.trPriceListHeader.ConfirmedDate, dbo.trPriceListHeader.ApplicationCode, dbo.trPriceListHeader.ApplicationID, dbo.trPriceListHeader.CreatedUserName, 
                         dbo.trPriceListHeader.CreatedDate, dbo.trPriceListHeader.LastUpdatedUserName, dbo.trPriceListHeader.LastUpdatedDate, dbo.trPriceListLine.PriceListLineID, dbo.trPriceListLine.SortOrder, dbo.trPriceListLine.ItemTypeCode, 
                         dbo.trPriceListLine.ItemCode, dbo.trPriceListLine.ColorCode, dbo.trPriceListLine.ItemDim1Code, dbo.trPriceListLine.ItemDim2Code, dbo.trPriceListLine.ItemDim3Code, dbo.trPriceListLine.UnitOfMeasureCode, 
                         dbo.trPriceListLine.PaymentPlanCode, dbo.trPriceListLine.LineDescription, dbo.trPriceListLine.DocCurrencyCode, dbo.trPriceListLine.Price, dbo.trPriceListLine.IsDisabled, dbo.trPriceListLine.DisableDate, 
                         dbo.trPriceListLine.CompanyCode, dbo.trPriceListLine.PriceGroupCode, dbo.trPriceListLine.ValidDate, dbo.trPriceListLine.ValidTime
FROM            dbo.trPriceListHeader WITH (NOLOCK) INNER JOIN
                         dbo.trPriceListLine WITH (NOLOCK) ON dbo.trPriceListLine.PriceListHeaderID = dbo.trPriceListHeader.PriceListHeaderID
