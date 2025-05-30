-- AllInvoiceLinesWithITAttributes
-- Tüm fatura satırlarını ve ilişkili satır özniteliklerini (IT Attributes) getiren sorgu

SELECT        dbo.AllInvoiceLines.InvoiceHeaderID, dbo.AllInvoiceLines.TransTypeCode, dbo.AllInvoiceLines.ProcessCode, dbo.AllInvoiceLines.InvoiceNumber, dbo.AllInvoiceLines.IsReturn, dbo.AllInvoiceLines.InvoiceDate, 
                         dbo.AllInvoiceLines.InvoiceTime, dbo.AllInvoiceLines.OperationDate, dbo.AllInvoiceLines.OperationTime, dbo.AllInvoiceLines.Series, dbo.AllInvoiceLines.SeriesNumber, 
                         dbo.AllInvoiceLines.IsEInvoice, dbo.AllInvoiceLines.EInvoiceNumber, dbo.AllInvoiceLines.EInvoiceStatusCode, dbo.AllInvoiceLines.PaymentTerm, dbo.AllInvoiceLines.AverageDueDate, 
                         dbo.AllInvoiceLines.Description, dbo.AllInvoiceLines.InternalDescription, dbo.AllInvoiceLines.CurrAccTypeCode, dbo.AllInvoiceLines.CurrAccCode, dbo.AllInvoiceLines.SubCurrAccID, dbo.AllInvoiceLines.ContactID, 
                         dbo.AllInvoiceLines.EInvoiceAliasCode, dbo.AllInvoiceLines.CompanyCreditCardCode, dbo.AllInvoiceLines.ShipmentMethodCode, dbo.AllInvoiceLines.ShippingPostalAddressID, dbo.AllInvoiceLines.BillingPostalAddressID, 
                         dbo.AllInvoiceLines.GuarantorContactID, dbo.AllInvoiceLines.GuarantorContactID2, dbo.AllInvoiceLines.RoundsmanCode, dbo.AllInvoiceLines.DeliveryCompanyCode, dbo.AllInvoiceLines.DeliveryCompanyBarcode, 
                         dbo.AllInvoiceLines.TaxTypeCode, dbo.AllInvoiceLines.TaxExemptionCode, dbo.AllInvoiceLines.CompanyCode, dbo.AllInvoiceLines.OfficeCode, dbo.AllInvoiceLines.StoreTypeCode, dbo.AllInvoiceLines.StoreCode, 
                         dbo.AllInvoiceLines.POSTerminalID, dbo.AllInvoiceLines.WarehouseCode, dbo.AllInvoiceLines.FormType, dbo.AllInvoiceLines.InvoiceGiftCard, dbo.AllInvoiceLines.DigitalMarketingServiceCode, 
                         dbo.AllInvoiceLines.LocalCurrencyCode, dbo.AllInvoiceLines.ExchangeRate, dbo.AllInvoiceLines.TDisRate1, dbo.AllInvoiceLines.TDisRate2, dbo.AllInvoiceLines.TDisRate3, dbo.AllInvoiceLines.TDisRate4, 
                         dbo.AllInvoiceLines.TDisRate5, dbo.AllInvoiceLines.DiscountReasonCode, dbo.AllInvoiceLines.StoppageRate, dbo.AllInvoiceLines.TaxRefund, dbo.AllInvoiceLines.CustomsDocumentNumber, dbo.AllInvoiceLines.IncotermCode1, 
                         dbo.AllInvoiceLines.IncotermCode2, dbo.AllInvoiceLines.PaymentMethodCode, dbo.AllInvoiceLines.DocumentTypeCode, dbo.AllInvoiceLines.IsInclutedVat, dbo.AllInvoiceLines.IsCreditSale, dbo.AllInvoiceLines.IsShipmentBase, 
                         dbo.AllInvoiceLines.IsReportedSaleBase, dbo.AllInvoiceLines.IsOrderBase, dbo.AllInvoiceLines.IsSuspended, dbo.AllInvoiceLines.IsCompleted, dbo.AllInvoiceLines.IsPrinted, dbo.AllInvoiceLines.IsLocked, 
                         dbo.AllInvoiceLines.IsProforma, dbo.AllInvoiceLines.IsDelivered, dbo.AllInvoiceLines.FiscalPrintedState, dbo.AllInvoiceLines.IsSalesViaInternet, dbo.AllInvoiceLines.IsProposalBased, dbo.AllInvoiceLines.IsPostingJournal, 
                         dbo.AllInvoiceLines.JournalDate, dbo.AllInvoiceLines.SendInvoiceByEMail, dbo.AllInvoiceLines.EMailAddress, dbo.AllInvoiceLines.SendInvoiceBySMS, dbo.AllInvoiceLines.GSMNo, dbo.AllInvoiceLines.ApplicationCode, 
                         dbo.AllInvoiceLines.ApplicationID, dbo.AllInvoiceLines.CreatedUserName, dbo.AllInvoiceLines.CreatedDate, dbo.AllInvoiceLines.LastUpdatedUserName, dbo.AllInvoiceLines.LastUpdatedDate, dbo.AllInvoiceLines.InvoiceLineID, 
                         dbo.AllInvoiceLines.SortOrder, dbo.AllInvoiceLines.ItemTypeCode, dbo.AllInvoiceLines.ItemCode, dbo.AllInvoiceLines.ColorCode, dbo.AllInvoiceLines.ItemDim1Code, dbo.AllInvoiceLines.ItemDim2Code, 
                         dbo.AllInvoiceLines.ItemDim3Code, dbo.AllInvoiceLines.Qty1, dbo.AllInvoiceLines.Qty2, dbo.AllInvoiceLines.BatchCode, dbo.AllInvoiceLines.SectionCode, dbo.AllInvoiceLines.SalespersonCode, 
                         dbo.AllInvoiceLines.PaymentPlanCode, dbo.AllInvoiceLines.PurchasePlanCode, dbo.AllInvoiceLines.ReturnReasonCode, dbo.AllInvoiceLines.GLTypeCode, dbo.AllInvoiceLines.CostCenterCode, 
                         dbo.AllInvoiceLines.LineDescription, dbo.AllInvoiceLines.UsedBarcode, dbo.AllInvoiceLines.SerialNumber, dbo.AllInvoiceLines.IsTransformed, dbo.AllInvoiceLines.DeliveryDate, dbo.AllInvoiceLines.PlannedDateOfLading, 
                         dbo.AllInvoiceLines.OrderDeliveryDate, dbo.AllInvoiceLines.ManufactureDate, dbo.AllInvoiceLines.ExpiryDate, dbo.AllInvoiceLines.ImportFileNumber, dbo.AllInvoiceLines.ExportFileNumber, dbo.AllInvoiceLines.VatCode, 
                         dbo.AllInvoiceLines.VatRate, dbo.AllInvoiceLines.WithHoldingTaxTypeCode, dbo.AllInvoiceLines.DovCode, dbo.AllInvoiceLines.PCTCode, dbo.AllInvoiceLines.PCTRate, dbo.AllInvoiceLines.LDisRate1, 
                         dbo.AllInvoiceLines.LDisRate2, dbo.AllInvoiceLines.LDisRate3, dbo.AllInvoiceLines.LDisRate4, dbo.AllInvoiceLines.LDisRate5, dbo.AllInvoiceLines.DocCurrencyCode, dbo.AllInvoiceLines.PriceCurrencyCode, 
                         dbo.AllInvoiceLines.PriceExchangeRate, dbo.AllInvoiceLines.Price, dbo.AllInvoiceLines.IsImmutable, dbo.AllInvoiceLines.SupportRequestHeaderID, dbo.AllInvoiceLines.PurchaseRequisitionLineID, 
                         dbo.AllInvoiceLines.ShipmentLineID, dbo.AllInvoiceLines.ReserveLineID, dbo.AllInvoiceLines.DispOrderLineID, dbo.AllInvoiceLines.PickingLineID, dbo.AllInvoiceLines.OrderAsnLineID, dbo.AllInvoiceLines.OrderLineID, 
                         dbo.AllInvoiceLines.PriceListLineID, dbo.AllInvoiceLines.InvoiceLineLinkedProductID, dbo.AllInvoiceLines.InvoiceLineSumID, dbo.AllInvoiceLines.InvoiceLineSerialSumID, dbo.AllInvoiceLines.InvoiceLineBOMID, 
                         dbo.InvoiceITAttributes.ITAtt01, dbo.InvoiceITAttributes.ITAtt02, dbo.InvoiceITAttributes.ITAtt03, dbo.InvoiceITAttributes.ITAtt04, dbo.InvoiceITAttributes.ITAtt05, dbo.InvoiceITAttributes.ITAtt06, dbo.InvoiceITAttributes.ITAtt07, 
                         dbo.InvoiceITAttributes.ITAtt08, dbo.InvoiceITAttributes.ITAtt09, dbo.InvoiceITAttributes.ITAtt10
FROM            dbo.AllInvoiceLines WITH (NOLOCK) INNER JOIN
                         dbo.InvoiceITAttributes WITH (NOLOCK) ON dbo.AllInvoiceLines.InvoiceLineID = dbo.InvoiceITAttributes.InvoiceLineID
