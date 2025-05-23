-- AllInvoices
-- Tüm faturaların detaylarını getiren sorgu

SELECT        dbo.trInvoiceHeader.InvoiceHeaderID, dbo.trInvoiceHeader.TransTypeCode, dbo.trInvoiceHeader.ProcessCode, dbo.trInvoiceHeader.InvoiceNumber, dbo.trInvoiceHeader.IsReturn, dbo.trInvoiceHeader.InvoiceDate, 
                         dbo.trInvoiceHeader.InvoiceTime, dbo.trInvoiceHeader.OperationDate, dbo.trInvoiceHeader.OperationTime, dbo.trInvoiceHeader.Series, dbo.trInvoiceHeader.SeriesNumber,
                        dbo.trInvoiceHeader.IsEInvoice, dbo.trInvoiceHeader.EInvoiceNumber, dbo.trInvoiceHeader.EInvoiceStatusCode, dbo.trInvoiceHeader.PaymentTerm, 
                         dbo.trInvoiceHeader.AverageDueDate, dbo.trInvoiceHeader.Description, dbo.trInvoiceHeader.InternalDescription, dbo.trInvoiceHeader.CurrAccTypeCode, dbo.trInvoiceHeader.CurrAccCode, dbo.trInvoiceHeader.SubCurrAccID, 
                         dbo.trInvoiceHeader.ContactID, dbo.trInvoiceHeader.EInvoiceAliasCode, dbo.trInvoiceHeader.CompanyCreditCardCode, dbo.trInvoiceHeader.ShipmentMethodCode, dbo.trInvoiceHeader.ShippingPostalAddressID, 
                         dbo.trInvoiceHeader.BillingPostalAddressID, dbo.trInvoiceHeader.GuarantorContactID, dbo.trInvoiceHeader.GuarantorContactID2, dbo.trInvoiceHeader.RoundsmanCode, dbo.trInvoiceHeader.DeliveryCompanyCode, 
                         dbo.trInvoiceHeader.DeliveryCompanyBarcode, dbo.trInvoiceHeader.TaxTypeCode, dbo.trInvoiceHeader.TaxExemptionCode, dbo.trInvoiceHeader.CompanyCode, dbo.trInvoiceHeader.OfficeCode, 
                         dbo.trInvoiceHeader.StoreTypeCode, dbo.trInvoiceHeader.StoreCode, dbo.trInvoiceHeader.POSTerminalID, dbo.trInvoiceHeader.WarehouseCode, dbo.trInvoiceHeader.FormType, 
                         CAST((CASE WHEN FormType = 10 THEN 1 ELSE 0 END) AS BIT) AS InvoiceGiftCard, dbo.trInvoiceHeader.DigitalMarketingServiceCode, dbo.trInvoiceHeader.ExchangeRate, dbo.trInvoiceHeader.TDisRate1, 
                         dbo.trInvoiceHeader.TDisRate2, dbo.trInvoiceHeader.TDisRate3, dbo.trInvoiceHeader.TDisRate4, dbo.trInvoiceHeader.TDisRate5, dbo.trInvoiceHeader.DiscountReasonCode, dbo.trInvoiceHeader.StoppageRate, 
                         dbo.trInvoiceHeader.TaxRefund, dbo.trInvoiceHeader.CustomsDocumentNumber, dbo.trInvoiceHeader.IncotermCode1, dbo.trInvoiceHeader.IncotermCode2, dbo.trInvoiceHeader.PaymentMethodCode, 
                         dbo.trInvoiceHeader.DocumentTypeCode, dbo.trInvoiceHeader.IsInclutedVat, dbo.trInvoiceHeader.IsCreditSale, dbo.trInvoiceHeader.IsShipmentBase, dbo.trInvoiceHeader.IsReportedSaleBase, dbo.trInvoiceHeader.IsOrderBase, 
                         dbo.trInvoiceHeader.IsSuspended, dbo.trInvoiceHeader.IsCompleted, dbo.trInvoiceHeader.IsPrinted, dbo.trInvoiceHeader.IsLocked, dbo.trInvoiceHeader.IsProforma, dbo.trInvoiceHeader.IsDelivered, 
                         dbo.trInvoiceHeader.FiscalPrintedState, dbo.trInvoiceHeader.IsSalesViaInternet, dbo.trInvoiceHeader.IsProposalBased, dbo.trInvoiceHeader.IsPostingJournal, dbo.trInvoiceHeader.JournalDate, 
                         dbo.trInvoiceHeader.SendInvoiceByEMail, dbo.trInvoiceHeader.EMailAddress, dbo.trInvoiceHeader.SendInvoiceBySMS, dbo.trInvoiceHeader.GSMNo, dbo.trInvoiceHeader.ApplicationCode, dbo.trInvoiceHeader.ApplicationID, 
                         dbo.trInvoiceHeader.CreatedUserName, dbo.trInvoiceHeader.CreatedDate, dbo.trInvoiceHeader.LastUpdatedUserName, dbo.trInvoiceHeader.LastUpdatedDate, dbo.trInvoiceLine.InvoiceLineID, dbo.trInvoiceLine.SortOrder, 
                         dbo.trInvoiceLine.ItemTypeCode, dbo.trInvoiceLine.ItemCode, dbo.trInvoiceLine.ColorCode, dbo.trInvoiceLine.ItemDim1Code, dbo.trInvoiceLine.ItemDim2Code, dbo.trInvoiceLine.ItemDim3Code, dbo.trInvoiceLine.Qty1, 
                         dbo.trInvoiceLine.Qty2, dbo.trInvoiceLine.BatchCode, dbo.trInvoiceLine.SectionCode, dbo.trInvoiceLine.SalespersonCode, dbo.trInvoiceLine.PaymentPlanCode, dbo.trInvoiceLine.PurchasePlanCode, 
                         dbo.trInvoiceLine.ReturnReasonCode, dbo.trInvoiceLine.GLTypeCode, dbo.trInvoiceLine.CostCenterCode, dbo.trInvoiceLine.LineDescription, dbo.trInvoiceLine.UsedBarcode, dbo.trInvoiceLine.SerialNumber, 
                         dbo.trInvoiceLine.IsTransformed, dbo.trInvoiceLine.DeliveryDate, dbo.trInvoiceLine.PlannedDateOfLading, dbo.trInvoiceLine.OrderDeliveryDate, dbo.trInvoiceLine.ManufactureDate, dbo.trInvoiceLine.ExpiryDate, 
                         dbo.trInvoiceLine.ImportFileNumber, dbo.trInvoiceLine.ExportFileNumber, dbo.trInvoiceLine.VatCode, dbo.trInvoiceLine.VatRate, 
                         CASE WHEN trInvoiceHeader.TaxTypeCode = 7 THEN trInvoiceLine.WithHoldingTaxTypeCode ELSE trInvoiceHeader.WithHoldingTaxTypeCode END AS WithHoldingTaxTypeCode, 
                         CASE WHEN trInvoiceHeader.TaxTypeCode = 7 THEN trInvoiceLine.DovCode ELSE trInvoiceHeader.DovCode END AS DovCode, dbo.trInvoiceLine.PCTCode, dbo.trInvoiceLine.PCTRate, dbo.trInvoiceLine.LDisRate1, 
                         dbo.trInvoiceLine.LDisRate2, dbo.trInvoiceLine.LDisRate3, dbo.trInvoiceLine.LDisRate4, dbo.trInvoiceLine.LDisRate5, dbo.trInvoiceLine.DocCurrencyCode, dbo.trInvoiceLine.PriceCurrencyCode, 
                         dbo.trInvoiceLine.PriceExchangeRate, dbo.trInvoiceLine.Price, dbo.trInvoiceLine.IsImmutable, dbo.trInvoiceLine.SupportRequestHeaderID, dbo.trInvoiceLine.PurchaseRequisitionLineID, dbo.trInvoiceLine.ShipmentLineID, 
                         dbo.trInvoiceLine.ReserveLineID, dbo.trInvoiceLine.DispOrderLineID, dbo.trInvoiceLine.PickingLineID, dbo.trInvoiceLine.OrderAsnLineID, dbo.trInvoiceLine.OrderLineID, dbo.trInvoiceLine.PriceListLineID, 
                         dbo.trInvoiceLine.InvoiceLineLinkedProductID, dbo.trInvoiceLine.InvoiceLineSumID, dbo.trInvoiceLine.InvoiceLineSerialSumID, dbo.trInvoiceLine.InvoiceLineBOMID, dbo.trInvoiceLine.DocCurrencyCode AS Doc_CurrencyCode, 
                         ISNULL(trInvoiceLineCurrencyDoc.PriceVI, 0) AS Doc_PriceVI, ISNULL(trInvoiceLineCurrencyDoc.AmountVI, 0) AS Doc_AmountVI, ISNULL(trInvoiceLineCurrencyDoc.Price, 0) AS Doc_Price, 
                         ISNULL(trInvoiceLineCurrencyDoc.Amount, 0) AS Doc_Amount, ISNULL(trInvoiceLineCurrencyDoc.LDiscount1, 0) AS Doc_LDiscount1, ISNULL(trInvoiceLineCurrencyDoc.LDiscount2, 0) AS Doc_LDiscount2, 
                         ISNULL(trInvoiceLineCurrencyDoc.LDiscount3, 0) AS Doc_LDiscount3, ISNULL(trInvoiceLineCurrencyDoc.LDiscount4, 0) AS Doc_LDiscount4, ISNULL(trInvoiceLineCurrencyDoc.LDiscount5, 0) AS Doc_LDiscount5, 
                         ISNULL(trInvoiceLineCurrencyDoc.LDiscountVI1, 0) AS Doc_LDiscountVI1, ISNULL(trInvoiceLineCurrencyDoc.LDiscountVI2, 0) AS Doc_LDiscountVI2, ISNULL(trInvoiceLineCurrencyDoc.LDiscountVI3, 0) AS Doc_LDiscountVI3, 
                         ISNULL(trInvoiceLineCurrencyDoc.LDiscountVI4, 0) AS Doc_LDiscountVI4, ISNULL(trInvoiceLineCurrencyDoc.LDiscountVI5, 0) AS Doc_LDiscountVI5, ISNULL(trInvoiceLineCurrencyDoc.TDiscount1, 0) AS Doc_TDiscount1, 
                         ISNULL(trInvoiceLineCurrencyDoc.TDiscount2, 0) AS Doc_TDiscount2, ISNULL(trInvoiceLineCurrencyDoc.TDiscount3, 0) AS Doc_TDiscount3, ISNULL(trInvoiceLineCurrencyDoc.TDiscount4, 0) AS Doc_TDiscount4, 
                         ISNULL(trInvoiceLineCurrencyDoc.TDiscount5, 0) AS Doc_TDiscount5, ISNULL(trInvoiceLineCurrencyDoc.TDiscountVI1, 0) AS Doc_TDiscountVI1, ISNULL(trInvoiceLineCurrencyDoc.TDiscountVI2, 0) AS Doc_TDiscountVI2, 
                         ISNULL(trInvoiceLineCurrencyDoc.TDiscountVI3, 0) AS Doc_TDiscountVI3, ISNULL(trInvoiceLineCurrencyDoc.TDiscountVI4, 0) AS Doc_TDiscountVI4, ISNULL(trInvoiceLineCurrencyDoc.TDiscountVI5, 0) AS Doc_TDiscountVI5, 
                         ISNULL(trInvoiceLineCurrencyDoc.TaxBase, 0) AS Doc_TaxBase, ISNULL(trInvoiceLineCurrencyDoc.Pct, 0) AS Doc_Pct, ISNULL(trInvoiceLineCurrencyDoc.Vat, 0) AS Doc_Vat, ISNULL(trInvoiceLineCurrencyDoc.VatDeducation, 0) 
                         AS Doc_VatDeducation, ISNULL(trInvoiceLineCurrencyDoc.StoppageAmount, 0) AS Doc_StoppageAmount, ISNULL(trInvoiceLineCurrencyDoc.NetAmount, 0) AS Doc_NetAmount, 
                         ISNULL(trInvoiceLineCurrencyDoc.TDiscount1 + trInvoiceLineCurrencyDoc.TDiscount2 + trInvoiceLineCurrencyDoc.TDiscount3 + trInvoiceLineCurrencyDoc.TDiscount4 + trInvoiceLineCurrencyDoc.TDiscount5, 0) 
                         AS Doc_TDiscountTotal, 
                         ISNULL(trInvoiceLineCurrencyDoc.TDiscountVI1 + trInvoiceLineCurrencyDoc.TDiscountVI2 + trInvoiceLineCurrencyDoc.TDiscountVI3 + trInvoiceLineCurrencyDoc.TDiscountVI4 + trInvoiceLineCurrencyDoc.TDiscountVI5, 0) 
                         AS Doc_TDiscountVITotal, 
                         ISNULL(trInvoiceLineCurrencyDoc.LDiscount1 + trInvoiceLineCurrencyDoc.LDiscount2 + trInvoiceLineCurrencyDoc.LDiscount3 + trInvoiceLineCurrencyDoc.LDiscount4 + trInvoiceLineCurrencyDoc.LDiscount5, 0) 
                         AS Doc_LDiscountTotal, 
                         ISNULL(trInvoiceLineCurrencyDoc.LDiscountVI1 + trInvoiceLineCurrencyDoc.LDiscountVI2 + trInvoiceLineCurrencyDoc.LDiscountVI3 + trInvoiceLineCurrencyDoc.LDiscountVI4 + trInvoiceLineCurrencyDoc.LDiscountVI5, 0) 
                         AS Doc_LDiscountVITotal, dbo.trInvoiceHeader.LocalCurrencyCode AS Loc_CurrencyCode, ISNULL(trInvoiceLineCurrencyLoc.ExchangeRate, 0) AS Loc_ExchangeRate, ISNULL(trInvoiceLineCurrencyLoc.PriceVI, 0) AS Loc_PriceVI,
                          ISNULL(trInvoiceLineCurrencyLoc.AmountVI, 0) AS Loc_AmountVI, ISNULL(trInvoiceLineCurrencyLoc.Price, 0) AS Loc_Price, ISNULL(trInvoiceLineCurrencyLoc.Amount, 0) AS Loc_Amount, 
                         ISNULL(trInvoiceLineCurrencyLoc.LDiscount1, 0) AS Loc_LDiscount1, ISNULL(trInvoiceLineCurrencyLoc.LDiscount2, 0) AS Loc_LDiscount2, ISNULL(trInvoiceLineCurrencyLoc.LDiscount3, 0) AS Loc_LDiscount3, 
                         ISNULL(trInvoiceLineCurrencyLoc.LDiscount4, 0) AS Loc_LDiscount4, ISNULL(trInvoiceLineCurrencyLoc.LDiscount5, 0) AS Loc_LDiscount5, ISNULL(trInvoiceLineCurrencyLoc.LDiscountVI1, 0) AS Loc_LDiscountVI1, 
                         ISNULL(trInvoiceLineCurrencyLoc.LDiscountVI2, 0) AS Loc_LDiscountVI2, ISNULL(trInvoiceLineCurrencyLoc.LDiscountVI3, 0) AS Loc_LDiscountVI3, ISNULL(trInvoiceLineCurrencyLoc.LDiscountVI4, 0) AS Loc_LDiscountVI4, 
                         ISNULL(trInvoiceLineCurrencyLoc.LDiscountVI5, 0) AS Loc_LDiscountVI5, ISNULL(trInvoiceLineCurrencyLoc.TDiscount1, 0) AS Loc_TDiscount1, ISNULL(trInvoiceLineCurrencyLoc.TDiscount2, 0) AS Loc_TDiscount2, 
                         ISNULL(trInvoiceLineCurrencyLoc.TDiscount3, 0) AS Loc_TDiscount3, ISNULL(trInvoiceLineCurrencyLoc.TDiscount4, 0) AS Loc_TDiscount4, ISNULL(trInvoiceLineCurrencyLoc.TDiscount5, 0) AS Loc_TDiscount5, 
                         ISNULL(trInvoiceLineCurrencyLoc.TDiscountVI1, 0) AS Loc_TDiscountVI1, ISNULL(trInvoiceLineCurrencyLoc.TDiscountVI2, 0) AS Loc_TDiscountVI2, ISNULL(trInvoiceLineCurrencyLoc.TDiscountVI3, 0) AS Loc_TDiscountVI3, 
                         ISNULL(trInvoiceLineCurrencyLoc.TDiscountVI4, 0) AS Loc_TDiscountVI4, ISNULL(trInvoiceLineCurrencyLoc.TDiscountVI5, 0) AS Loc_TDiscountVI5, ISNULL(trInvoiceLineCurrencyLoc.TaxBase, 0) AS Loc_TaxBase, 
                         ISNULL(trInvoiceLineCurrencyLoc.Pct, 0) AS Loc_Pct, ISNULL(trInvoiceLineCurrencyLoc.Vat, 0) AS Loc_Vat, ISNULL(trInvoiceLineCurrencyLoc.VatDeducation, 0) AS Loc_VatDeducation, 
                         ISNULL(trInvoiceLineCurrencyLoc.StoppageAmount, 0) AS Loc_StoppageAmount, ISNULL(trInvoiceLineCurrencyLoc.NetAmount, 0) AS Loc_NetAmount, 
                         ISNULL(trInvoiceLineCurrencyLoc.TDiscount1 + trInvoiceLineCurrencyLoc.TDiscount2 + trInvoiceLineCurrencyLoc.TDiscount3 + trInvoiceLineCurrencyLoc.TDiscount4 + trInvoiceLineCurrencyLoc.TDiscount5, 0) 
                         AS Loc_TDiscountTotal, 
                         ISNULL(trInvoiceLineCurrencyLoc.TDiscountVI1 + trInvoiceLineCurrencyLoc.TDiscountVI2 + trInvoiceLineCurrencyLoc.TDiscountVI3 + trInvoiceLineCurrencyLoc.TDiscountVI4 + trInvoiceLineCurrencyLoc.TDiscountVI5, 0) 
                         AS Loc_TDiscountVITotal, 
                         ISNULL(trInvoiceLineCurrencyLoc.LDiscount1 + trInvoiceLineCurrencyLoc.LDiscount2 + trInvoiceLineCurrencyLoc.LDiscount3 + trInvoiceLineCurrencyLoc.LDiscount4 + trInvoiceLineCurrencyLoc.LDiscount5, 0) 
                         AS Loc_LDiscountTotal, 
                         ISNULL(trInvoiceLineCurrencyLoc.LDiscountVI1 + trInvoiceLineCurrencyLoc.LDiscountVI2 + trInvoiceLineCurrencyLoc.LDiscountVI3 + trInvoiceLineCurrencyLoc.LDiscountVI4 + trInvoiceLineCurrencyLoc.LDiscountVI5, 0) 
                         AS Loc_LDiscountVITotal, dbo.dfGlobalDefault.CompanyCurrencyCode AS Com_CurrencyCode, ISNULL(trInvoiceLineCurrencyCom.ExchangeRate, 0) AS Com_ExchangeRate, ISNULL(trInvoiceLineCurrencyCom.PriceVI, 0) 
                         AS Com_PriceVI, ISNULL(trInvoiceLineCurrencyCom.AmountVI, 0) AS Com_AmountVI, ISNULL(trInvoiceLineCurrencyCom.Price, 0) AS Com_Price, ISNULL(trInvoiceLineCurrencyCom.Amount, 0) AS Com_Amount, 
                         ISNULL(trInvoiceLineCurrencyCom.LDiscount1, 0) AS Com_LDiscount1, ISNULL(trInvoiceLineCurrencyCom.LDiscount2, 0) AS Com_LDiscount2, ISNULL(trInvoiceLineCurrencyCom.LDiscount3, 0) AS Com_LDiscount3, 
                         ISNULL(trInvoiceLineCurrencyCom.LDiscount4, 0) AS Com_LDiscount4, ISNULL(trInvoiceLineCurrencyCom.LDiscount5, 0) AS Com_LDiscount5, ISNULL(trInvoiceLineCurrencyCom.LDiscountVI1, 0) AS Com_LDiscountVI1, 
                         ISNULL(trInvoiceLineCurrencyCom.LDiscountVI2, 0) AS Com_LDiscountVI2, ISNULL(trInvoiceLineCurrencyCom.LDiscountVI3, 0) AS Com_LDiscountVI3, ISNULL(trInvoiceLineCurrencyCom.LDiscountVI4, 0) AS Com_LDiscountVI4, 
                         ISNULL(trInvoiceLineCurrencyCom.LDiscountVI5, 0) AS Com_LDiscountVI5, ISNULL(trInvoiceLineCurrencyCom.TDiscount1, 0) AS Com_TDiscount1, ISNULL(trInvoiceLineCurrencyCom.TDiscount2, 0) AS Com_TDiscount2, 
                         ISNULL(trInvoiceLineCurrencyCom.TDiscount3, 0) AS Com_TDiscount3, ISNULL(trInvoiceLineCurrencyCom.TDiscount4, 0) AS Com_TDiscount4, ISNULL(trInvoiceLineCurrencyCom.TDiscount5, 0) AS Com_TDiscount5, 
                         ISNULL(trInvoiceLineCurrencyCom.TDiscountVI1, 0) AS Com_TDiscountVI1, ISNULL(trInvoiceLineCurrencyCom.TDiscountVI2, 0) AS Com_TDiscountVI2, ISNULL(trInvoiceLineCurrencyCom.TDiscountVI3, 0) AS Com_TDiscountVI3, 
                         ISNULL(trInvoiceLineCurrencyCom.TDiscountVI4, 0) AS Com_TDiscountVI4, ISNULL(trInvoiceLineCurrencyCom.TDiscountVI5, 0) AS Com_TDiscountVI5, ISNULL(trInvoiceLineCurrencyCom.TaxBase, 0) AS Com_TaxBase, 
                         ISNULL(trInvoiceLineCurrencyCom.Pct, 0) AS Com_Pct, ISNULL(trInvoiceLineCurrencyCom.Vat, 0) AS Com_Vat, ISNULL(trInvoiceLineCurrencyCom.VatDeducation, 0) AS Com_VatDeducation, 
                         ISNULL(trInvoiceLineCurrencyCom.StoppageAmount, 0) AS Com_StoppageAmount, ISNULL(trInvoiceLineCurrencyCom.NetAmount, 0) AS Com_NetAmount, 
                         ISNULL(trInvoiceLineCurrencyCom.TDiscount1 + trInvoiceLineCurrencyCom.TDiscount2 + trInvoiceLineCurrencyCom.TDiscount3 + trInvoiceLineCurrencyCom.TDiscount4 + trInvoiceLineCurrencyCom.TDiscount5, 0) 
                         AS Com_TDiscountTotal, 
                         ISNULL(trInvoiceLineCurrencyCom.TDiscountVI1 + trInvoiceLineCurrencyCom.TDiscountVI2 + trInvoiceLineCurrencyCom.TDiscountVI3 + trInvoiceLineCurrencyCom.TDiscountVI4 + trInvoiceLineCurrencyCom.TDiscountVI5, 0) 
                         AS Com_TDiscountVITotal, 
                         ISNULL(trInvoiceLineCurrencyCom.LDiscount1 + trInvoiceLineCurrencyCom.LDiscount2 + trInvoiceLineCurrencyCom.LDiscount3 + trInvoiceLineCurrencyCom.LDiscount4 + trInvoiceLineCurrencyCom.LDiscount5, 0) 
                         AS Com_LDiscountTotal, 
                         ISNULL(trInvoiceLineCurrencyCom.LDiscountVI1 + trInvoiceLineCurrencyCom.LDiscountVI2 + trInvoiceLineCurrencyCom.LDiscountVI3 + trInvoiceLineCurrencyCom.LDiscountVI4 + trInvoiceLineCurrencyCom.LDiscountVI5, 0) 
                         AS Com_LDiscountVITotal
FROM            dbo.trInvoiceLine WITH (NOLOCK) INNER JOIN
                         dbo.trInvoiceHeader WITH (NOLOCK) ON 1 = 1 AND dbo.trInvoiceHeader.InvoiceHeaderID = dbo.trInvoiceLine.InvoiceHeaderID INNER JOIN
                         dbo.dfGlobalDefault WITH (NOLOCK) ON dbo.dfGlobalDefault.GlobalDefaultCode = 1 LEFT OUTER JOIN
                         dbo.trInvoiceLineCurrency AS trInvoiceLineCurrencyDoc WITH (NOLOCK) ON trInvoiceLineCurrencyDoc.InvoiceLineID = dbo.trInvoiceLine.InvoiceLineID AND 
                         dbo.trInvoiceLine.DocCurrencyCode = trInvoiceLineCurrencyDoc.CurrencyCode LEFT OUTER JOIN
                         dbo.trInvoiceLineCurrency AS trInvoiceLineCurrencyLoc WITH (NOLOCK) ON trInvoiceLineCurrencyLoc.InvoiceLineID = dbo.trInvoiceLine.InvoiceLineID AND 
                         dbo.trInvoiceHeader.LocalCurrencyCode = trInvoiceLineCurrencyLoc.CurrencyCode LEFT OUTER JOIN
                         dbo.trInvoiceLineCurrency AS trInvoiceLineCurrencyCom WITH (NOLOCK) ON trInvoiceLineCurrencyCom.InvoiceLineID = dbo.trInvoiceLine.InvoiceLineID AND 
                         dbo.dfGlobalDefault.CompanyCurrencyCode = trInvoiceLineCurrencyCom.CurrencyCode
