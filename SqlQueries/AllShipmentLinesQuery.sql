SELECT        dbo.trShipmentHeader.ShipmentHeaderID, dbo.trShipmentHeader.TransTypeCode, dbo.trShipmentHeader.ProcessCode, dbo.trShipmentHeader.ShippingNumber, dbo.trShipmentHeader.IsReturn, 
                         dbo.trShipmentHeader.ShippingDate, dbo.trShipmentHeader.ShippingTime, dbo.trShipmentHeader.OperationDate, dbo.trShipmentHeader.OperationTime, dbo.trShipmentHeader.Series, dbo.trShipmentHeader.SeriesNumber, 
                         dbo.trShipmentHeader.Description, dbo.trShipmentHeader.InternalDescription, dbo.trShipmentHeader.CurrAccTypeCode, dbo.trShipmentHeader.CurrAccCode, dbo.trShipmentHeader.SubCurrAccID, 
                         dbo.trShipmentHeader.ContactID, dbo.trShipmentHeader.ShipmentMethodCode, dbo.trShipmentHeader.ShippingPostalAddressID, dbo.trShipmentHeader.BillingPostalAddressID, dbo.trShipmentHeader.RoundsmanCode, 
                         dbo.trShipmentHeader.DeliveryCompanyCode, dbo.trShipmentHeader.LogisticsCompanyBOL, dbo.trShipmentHeader.CustomerASNNumber, dbo.trShipmentHeader.CompanyCode, dbo.trShipmentHeader.OfficeCode, 
                         dbo.trShipmentHeader.StoreTypeCode, dbo.trShipmentHeader.StoreCode, dbo.trShipmentHeader.WarehouseCode, dbo.trShipmentHeader.ToWarehouseCode, dbo.trShipmentHeader.IsOrderBase, 
                         dbo.trShipmentHeader.IsCompleted, dbo.trShipmentHeader.IsPrinted, dbo.trShipmentHeader.IsLocked, dbo.trShipmentHeader.IsTransferApproved, dbo.trShipmentHeader.TransferApprovedDate, 
                         dbo.trShipmentHeader.IsPostingJournal, dbo.trShipmentHeader.JournalDate, dbo.trShipmentHeader.ApplicationCode, dbo.trShipmentHeader.ApplicationID, dbo.trShipmentHeader.CreatedUserName, 
                         dbo.trShipmentHeader.CreatedDate, dbo.trShipmentHeader.LastUpdatedUserName, dbo.trShipmentHeader.LastUpdatedDate, dbo.trShipmentLine.ShipmentLineID, dbo.trShipmentLine.SortOrder, 
                         dbo.trShipmentLine.ItemTypeCode, dbo.trShipmentLine.ItemCode, dbo.trShipmentLine.ColorCode, dbo.trShipmentLine.ItemDim1Code, dbo.trShipmentLine.ItemDim2Code, dbo.trShipmentLine.ItemDim3Code, 
                         dbo.trShipmentLine.Qty1, dbo.trShipmentLine.Qty2, dbo.trShipmentLine.BatchCode, dbo.trShipmentLine.SectionCode, dbo.trShipmentLine.SalespersonCode, dbo.trShipmentLine.PaymentPlanCode, 
                         dbo.trShipmentLine.PurchasePlanCode, dbo.trShipmentLine.ReturnReasonCode, dbo.trShipmentLine.LineDescription, dbo.trShipmentLine.UsedBarcode, dbo.trShipmentLine.OrderDeliveryDate, 
                         dbo.trShipmentLine.DeliveryCompanyBarcode, dbo.trShipmentLine.LogisticsPackageNumber, dbo.trShipmentLine.ImportFileNumber, dbo.trShipmentLine.ExportFileNumber, dbo.trShipmentLine.ManufactureDate, 
                         dbo.trShipmentLine.ExpiryDate, dbo.trShipmentLine.ReserveLineID, dbo.trShipmentLine.DispOrderLineID, dbo.trShipmentLine.PickingLineID, dbo.trShipmentLine.OrderAsnLineID, dbo.trShipmentLine.OrderLineID, 
                         dbo.trShipmentLine.PriceCurrencyCode, dbo.trShipmentLine.Price, dbo.trShipmentLine.PriceListLineID, dbo.trShipmentLine.IsInvoiced, dbo.trShipmentLine.SupportRequestHeaderID, dbo.trShipmentLine.ShipmentLineSumID, 
                         dbo.trShipmentLine.ShipmentLineSerialSumID, dbo.trShipmentLine.ShipmentLineBOMID
FROM            dbo.trShipmentHeader WITH (NOLOCK) INNER JOIN
                         dbo.trShipmentLine WITH (NOLOCK) ON dbo.trShipmentLine.ShipmentHeaderID = dbo.trShipmentHeader.ShipmentHeaderID
