-- AllInvoiceLines
-- Tüm fatura satırlarını getiren sorgu (para birimi detayları olmadan)

SELECT        dbo.trInvoiceHeader.InvoiceHeaderID, dbo.trInvoiceHeader.TransTypeCode, dbo.trInvoiceHeader.ProcessCode, dbo.trInvoiceHeader.InvoiceNumber, dbo.trInvoiceHeader.IsReturn, dbo.trInvoiceHeader.InvoiceDate, 
                         dbo.trInvoiceHeader.InvoiceTime, dbo.trInvoiceHeader.OperationDate, dbo.trInvoiceHeader.OperationTime, dbo.trInvoiceHeader.Series, dbo.trInvoiceHeader.SeriesNumber, dbo.trInvoiceHeader.InvoiceTypeCode, 
                         dbo.trInvoiceHeader.ExpenseTypeCode, dbo.trInvoiceHeader.IsEInvoice, dbo.trInvoiceHeader.EInvoiceNumber, dbo.trInvoiceHeader.EInvoiceStatusCode, dbo.trInvoiceHeader.PaymentTerm, 
                         dbo.trInvoiceHeader.AverageDueDate, dbo.trInvoiceHeader.Description, dbo.trInvoiceHeader.InternalDescription, dbo.trInvoiceHeader.CurrAccTypeCode, dbo.trInvoiceHeader.CurrAccCode, dbo.trInvoiceHeader.SubCurrAccID, 
                         dbo.trInvoiceHeader.ContactID, dbo.trInvoiceHeader.EInvoiceAliasCode, dbo.trInvoiceHeader.CompanyCreditCardCode, dbo.trInvoiceHeader.ShipmentMethodCode, dbo.trInvoiceHeader.ShippingPostalAddressID, 
                         dbo.trInvoiceHeader.BillingPostalAddressID, dbo.trInvoiceHeader.GuarantorContactID, dbo.trInvoiceHeader.GuarantorContactID2, dbo.trInvoiceHeader.RoundsmanCode, dbo.trInvoiceHeader.DeliveryCompanyCode, 
                         dbo.trInvoiceHeader.DeliveryCompanyBarcode, dbo.trInvoiceHeader.TaxTypeCode, dbo.trInvoiceHeader.TaxExemptionCode, dbo.trInvoiceHeader.CompanyCode, dbo.trInvoiceHeader.OfficeCode, 
                         dbo.trInvoiceHeader.StoreTypeCode, dbo.trInvoiceHeader.StoreCode, dbo.trInvoiceHeader.POSTerminalID, dbo.trInvoiceHeader.WarehouseCode, dbo.trInvoiceHeader.FormType, 
                         CAST((CASE WHEN FormType = 10 THEN 1 ELSE 0 END) AS BIT) AS InvoiceGiftCard, dbo.trInvoiceHeader.DigitalMarketingServiceCode, dbo.trInvoiceHeader.LocalCurrencyCode, dbo.trInvoiceHeader.ExchangeRate, 
                         dbo.trInvoiceHeader.TDisRate1, dbo.trInvoiceHeader.TDisRate2, dbo.trInvoiceHeader.TDisRate3, dbo.trInvoiceHeader.TDisRate4, dbo.trInvoiceHeader.TDisRate5, dbo.trInvoiceHeader.DiscountReasonCode, 
                         dbo.trInvoiceHeader.StoppageRate, dbo.trInvoiceHeader.TaxRefund, dbo.trInvoiceHeader.CustomsDocumentNumber, dbo.trInvoiceHeader.IncotermCode1, dbo.trInvoiceHeader.IncotermCode2, 
                         dbo.trInvoiceHeader.PaymentMethodCode, dbo.trInvoiceHeader.DocumentTypeCode, dbo.trInvoiceHeader.IsInclutedVat, dbo.trInvoiceHeader.IsCreditSale, dbo.trInvoiceHeader.IsShipmentBase, 
                         dbo.trInvoiceHeader.IsReportedSaleBase, dbo.trInvoiceHeader.IsOrderBase, dbo.trInvoiceHeader.IsSuspended, dbo.trInvoiceHeader.IsCompleted, dbo.trInvoiceHeader.IsPrinted, dbo.trInvoiceHeader.IsLocked, 
                         dbo.trInvoiceHeader.IsProforma, dbo.trInvoiceHeader.IsDelivered, dbo.trInvoiceHeader.FiscalPrintedState, dbo.trInvoiceHeader.IsSalesViaInternet, dbo.trInvoiceHeader.IsProposalBased, dbo.trInvoiceHeader.IsPostingJournal, 
                         dbo.trInvoiceHeader.JournalDate, dbo.trInvoiceHeader.SendInvoiceByEMail, dbo.trInvoiceHeader.EMailAddress, dbo.trInvoiceHeader.SendInvoiceBySMS, dbo.trInvoiceHeader.GSMNo, dbo.trInvoiceHeader.ApplicationCode, 
                         dbo.trInvoiceHeader.ApplicationID, dbo.trInvoiceHeader.CreatedUserName, dbo.trInvoiceHeader.CreatedDate, dbo.trInvoiceHeader.LastUpdatedUserName, dbo.trInvoiceHeader.LastUpdatedDate, dbo.trInvoiceLine.InvoiceLineID, 
                         dbo.trInvoiceLine.SortOrder, dbo.trInvoiceLine.ItemTypeCode, dbo.trInvoiceLine.ItemCode, dbo.trInvoiceLine.ColorCode, dbo.trInvoiceLine.ItemDim1Code, dbo.trInvoiceLine.ItemDim2Code, dbo.trInvoiceLine.ItemDim3Code, 
                         dbo.trInvoiceLine.Qty1, dbo.trInvoiceLine.Qty2, dbo.trInvoiceLine.BatchCode, dbo.trInvoiceLine.SectionCode, dbo.trInvoiceLine.SalespersonCode, dbo.trInvoiceLine.PaymentPlanCode, dbo.trInvoiceLine.PurchasePlanCode, 
                         dbo.trInvoiceLine.ReturnReasonCode, dbo.trInvoiceLine.GLTypeCode, dbo.trInvoiceLine.CostCenterCode, dbo.trInvoiceLine.LineDescription, dbo.trInvoiceLine.UsedBarcode, dbo.trInvoiceLine.SerialNumber, 
                         dbo.trInvoiceLine.IsTransformed, dbo.trInvoiceLine.DeliveryDate, dbo.trInvoiceLine.PlannedDateOfLading, dbo.trInvoiceLine.OrderDeliveryDate, dbo.trInvoiceLine.ManufactureDate, dbo.trInvoiceLine.ExpiryDate, 
                         dbo.trInvoiceLine.ImportFileNumber, dbo.trInvoiceLine.ExportFileNumber, dbo.trInvoiceLine.VatCode, dbo.trInvoiceLine.VatRate, 
                         CASE WHEN trInvoiceHeader.TaxTypeCode = 7 THEN trInvoiceLine.WithHoldingTaxTypeCode ELSE trInvoiceHeader.WithHoldingTaxTypeCode END AS WithHoldingTaxTypeCode, 
                         CASE WHEN trInvoiceHeader.TaxTypeCode = 7 THEN trInvoiceLine.DovCode ELSE trInvoiceHeader.DovCode END AS DovCode, dbo.trInvoiceLine.PCTCode, dbo.trInvoiceLine.PCTRate, dbo.trInvoiceLine.LDisRate1, 
                         dbo.trInvoiceLine.LDisRate2, dbo.trInvoiceLine.LDisRate3, dbo.trInvoiceLine.LDisRate4, dbo.trInvoiceLine.LDisRate5, dbo.trInvoiceLine.DocCurrencyCode, dbo.trInvoiceLine.PriceCurrencyCode, 
                         dbo.trInvoiceLine.PriceExchangeRate, dbo.trInvoiceLine.Price, dbo.trInvoiceLine.IsImmutable, dbo.trInvoiceLine.SupportRequestHeaderID, dbo.trInvoiceLine.PurchaseRequisitionLineID, dbo.trInvoiceLine.ShipmentLineID, 
                         dbo.trInvoiceLine.ReserveLineID, dbo.trInvoiceLine.DispOrderLineID, dbo.trInvoiceLine.PickingLineID, dbo.trInvoiceLine.OrderAsnLineID, dbo.trInvoiceLine.OrderLineID, dbo.trInvoiceLine.PriceListLineID, 
                         dbo.trInvoiceLine.InvoiceLineLinkedProductID, dbo.trInvoiceLine.InvoiceLineSumID, dbo.trInvoiceLine.InvoiceLineSerialSumID, dbo.trInvoiceLine.InvoiceLineBOMID
FROM            dbo.trInvoiceHeader WITH (NOLOCK) INNER JOIN
                         dbo.trInvoiceLine WITH (NOLOCK) ON dbo.trInvoiceLine.InvoiceHeaderID = dbo.trInvoiceHeader.InvoiceHeaderID
