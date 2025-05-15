SELECT        dbo.trOrderHeader.OrderHeaderID, dbo.trOrderHeader.OrderTypeCode, dbo.trOrderHeader.ProcessCode, dbo.trOrderHeader.OrderNumber, dbo.trOrderHeader.OrderDate, dbo.trOrderHeader.OrderTime, 
                         dbo.trOrderHeader.DocumentNumber, dbo.trOrderHeader.PaymentTerm, dbo.trOrderHeader.AverageDueDate, dbo.trOrderHeader.Description, dbo.trOrderHeader.InternalDescription, dbo.trOrderHeader.CurrAccTypeCode, 
                         dbo.trOrderHeader.CurrAccCode, dbo.trOrderHeader.SubCurrAccID, dbo.trOrderHeader.ContactID, dbo.trOrderHeader.ShipmentMethodCode, dbo.trOrderHeader.ShippingPostalAddressID, 
                         dbo.trOrderHeader.BillingPostalAddressID, dbo.trOrderHeader.GuarantorContactID, dbo.trOrderHeader.GuarantorContactID2, dbo.trOrderHeader.RoundsmanCode, dbo.trOrderHeader.DeliveryCompanyCode, 
                         dbo.trOrderHeader.TaxTypeCode, dbo.trOrderHeader.TaxExemptionCode, dbo.trOrderHeader.CompanyCode, dbo.trOrderHeader.OfficeCode, dbo.trOrderHeader.StoreTypeCode, dbo.trOrderHeader.StoreCode, 
                         dbo.trOrderHeader.POSTerminalID, dbo.trOrderHeader.WarehouseCode, dbo.trOrderHeader.ToWarehouseCode, dbo.trOrderHeader.OrdererCompanyCode, dbo.trOrderHeader.OrdererOfficeCode, 
                         dbo.trOrderHeader.OrdererStoreCode, dbo.trOrderHeader.GLTypeCode, dbo.trOrderHeader.ExchangeRate, dbo.trOrderHeader.TDisRate1, dbo.trOrderHeader.TDisRate2, dbo.trOrderHeader.TDisRate3, 
                         dbo.trOrderHeader.TDisRate4, dbo.trOrderHeader.TDisRate5, dbo.trOrderHeader.DiscountReasonCode, dbo.trOrderHeader.SurplusOrderQtyToleranceRate, dbo.trOrderHeader.ImportFileNumber, 
                         dbo.trOrderHeader.ExportFileNumber, dbo.trOrderHeader.IncotermCode1, dbo.trOrderHeader.IncotermCode2, dbo.trOrderHeader.LettersOfCreditNumber, dbo.trOrderHeader.PaymentMethodCode, dbo.trOrderHeader.IsInclutedVat, 
                         dbo.trOrderHeader.IsCreditSale, dbo.trOrderHeader.IsCreditableConfirmed, dbo.trOrderHeader.CreditableConfirmedUser, dbo.trOrderHeader.CreditableConfirmedDate, dbo.trOrderHeader.IsSalesViaInternet, 
                         dbo.trOrderHeader.IsProposalBased, dbo.trOrderHeader.IsSuspended, dbo.trOrderHeader.IsCompleted, dbo.trOrderHeader.IsPrinted, dbo.trOrderHeader.IsLocked, dbo.trOrderHeader.UserLocked, 
                         dbo.trOrderHeader.ApplicationCode, dbo.trOrderHeader.ApplicationID, dbo.trOrderHeader.CreatedUserName, dbo.trOrderHeader.CreatedDate, dbo.trOrderHeader.LastUpdatedUserName, dbo.trOrderHeader.LastUpdatedDate, 
                         dbo.trOrderLine.OrderLineID, dbo.trOrderLine.SortOrder, dbo.trOrderLine.ItemTypeCode, dbo.trOrderLine.ItemCode, dbo.trOrderLine.ColorCode, dbo.trOrderLine.ItemDim1Code, dbo.trOrderLine.ItemDim2Code, 
                         dbo.trOrderLine.ItemDim3Code, dbo.trOrderLine.Qty1, dbo.trOrderLine.Qty2, dbo.trOrderLine.CancelQty1, dbo.trOrderLine.CancelQty2, dbo.trOrderLine.CancelDate, dbo.trOrderLine.OrderCancelReasonCode, 
                         dbo.trOrderLine.ClosedDate, dbo.trOrderLine.IsClosed, dbo.trOrderLine.SalespersonCode, dbo.trOrderLine.PaymentPlanCode, dbo.trOrderLine.PurchasePlanCode, dbo.trOrderLine.DeliveryDate, 
                         dbo.trOrderLine.PlannedDateOfLading, dbo.trOrderLine.LineDescription, dbo.trOrderLine.UsedBarcode, dbo.trOrderLine.CostCenterCode, 
                         CASE WHEN trOrderHeader.TaxTypeCode = 7 THEN trOrderLine.WithHoldingTaxTypeCode ELSE trOrderHeader.WithHoldingTaxTypeCode END AS WithHoldingTaxTypeCode, 
                         CASE WHEN trOrderHeader.TaxTypeCode = 7 THEN trOrderLine.DovCode ELSE trOrderHeader.DovCode END AS DovCode, dbo.trOrderLine.VatCode, dbo.trOrderLine.VatRate, dbo.trOrderLine.PCTCode, dbo.trOrderLine.PCTRate, 
                         dbo.trOrderLine.LDisRate1, dbo.trOrderLine.LDisRate2, dbo.trOrderLine.LDisRate3, dbo.trOrderLine.LDisRate4, dbo.trOrderLine.LDisRate5, dbo.trOrderLine.DocCurrencyCode, dbo.trOrderLine.PriceCurrencyCode, 
                         dbo.trOrderLine.PriceExchangeRate, dbo.trOrderLine.Price, dbo.trOrderLine.PriceListLineID, dbo.trOrderLine.BaseProcessCode, dbo.trOrderLine.BaseOrderNumber, dbo.trOrderLine.BaseCustomerTypeCode, 
                         dbo.trOrderLine.BaseCustomerCode, dbo.trOrderLine.BaseSubCurrAccID, dbo.trOrderLine.BaseStoreCode, dbo.trOrderLine.SupportRequestHeaderID, dbo.trOrderLine.PurchaseRequisitionLineID, 
                         dbo.trOrderLine.OrderLineLinkedProductID, dbo.trOrderLine.SurplusOrderQtyToleranceRate AS LineSurplusOrderQtyToleranceRate, dbo.trOrderLine.OrderLineSumID, dbo.trOrderLine.OrderLineBOMID, 
                         dbo.trOrderLine.DocCurrencyCode AS Doc_CurrencyCode, ISNULL(trOrderLineCurrencyDoc.PriceVI, 0) AS Doc_PriceVI, ISNULL(trOrderLineCurrencyDoc.AmountVI, 0) AS Doc_AmountVI, ISNULL(trOrderLineCurrencyDoc.Price, 0) 
                         AS Doc_Price, ISNULL(trOrderLineCurrencyDoc.Amount, 0) AS Doc_Amount, ISNULL(trOrderLineCurrencyDoc.LDiscount1, 0) AS Doc_LDiscount1, ISNULL(trOrderLineCurrencyDoc.LDiscount2, 0) AS Doc_LDiscount2, 
                         ISNULL(trOrderLineCurrencyDoc.LDiscount3, 0) AS Doc_LDiscount3, ISNULL(trOrderLineCurrencyDoc.LDiscount4, 0) AS Doc_LDiscount4, ISNULL(trOrderLineCurrencyDoc.LDiscount5, 0) AS Doc_LDiscount5, 
                         ISNULL(trOrderLineCurrencyDoc.LDiscountVI1, 0) AS Doc_LDiscountVI1, ISNULL(trOrderLineCurrencyDoc.LDiscountVI2, 0) AS Doc_LDiscountVI2, ISNULL(trOrderLineCurrencyDoc.LDiscountVI3, 0) AS Doc_LDiscountVI3, 
                         ISNULL(trOrderLineCurrencyDoc.LDiscountVI4, 0) AS Doc_LDiscountVI4, ISNULL(trOrderLineCurrencyDoc.LDiscountVI5, 0) AS Doc_LDiscountVI5, ISNULL(trOrderLineCurrencyDoc.TDiscount1, 0) AS Doc_TDiscount1, 
                         ISNULL(trOrderLineCurrencyDoc.TDiscount2, 0) AS Doc_TDiscount2, ISNULL(trOrderLineCurrencyDoc.TDiscount3, 0) AS Doc_TDiscount3, ISNULL(trOrderLineCurrencyDoc.TDiscount4, 0) AS Doc_TDiscount4, 
                         ISNULL(trOrderLineCurrencyDoc.TDiscount5, 0) AS Doc_TDiscount5, ISNULL(trOrderLineCurrencyDoc.TDiscountVI1, 0) AS Doc_TDiscountVI1, ISNULL(trOrderLineCurrencyDoc.TDiscountVI2, 0) AS Doc_TDiscountVI2, 
                         ISNULL(trOrderLineCurrencyDoc.TDiscountVI3, 0) AS Doc_TDiscountVI3, ISNULL(trOrderLineCurrencyDoc.TDiscountVI4, 0) AS Doc_TDiscountVI4, ISNULL(trOrderLineCurrencyDoc.TDiscountVI5, 0) AS Doc_TDiscountVI5, 
                         ISNULL(trOrderLineCurrencyDoc.TaxBase, 0) AS Doc_TaxBase, ISNULL(trOrderLineCurrencyDoc.Pct, 0) AS Doc_Pct, ISNULL(trOrderLineCurrencyDoc.Vat, 0) AS Doc_Vat, ISNULL(trOrderLineCurrencyDoc.VatDeducation, 0) 
                         AS Doc_VatDeducation, ISNULL(trOrderLineCurrencyDoc.NetAmount, 0) AS Doc_NetAmount, 
                         ISNULL(trOrderLineCurrencyDoc.TDiscount1 + trOrderLineCurrencyDoc.TDiscount2 + trOrderLineCurrencyDoc.TDiscount3 + trOrderLineCurrencyDoc.TDiscount4 + trOrderLineCurrencyDoc.TDiscount5, 0) AS Doc_TDiscountTotal, 
                         ISNULL(trOrderLineCurrencyDoc.TDiscountVI1 + trOrderLineCurrencyDoc.TDiscountVI2 + trOrderLineCurrencyDoc.TDiscountVI3 + trOrderLineCurrencyDoc.TDiscountVI4 + trOrderLineCurrencyDoc.TDiscountVI5, 0) 
                         AS Doc_TDiscountVITotal, ISNULL(trOrderLineCurrencyDoc.LDiscount1 + trOrderLineCurrencyDoc.LDiscount2 + trOrderLineCurrencyDoc.LDiscount3 + trOrderLineCurrencyDoc.LDiscount4 + trOrderLineCurrencyDoc.LDiscount5, 
                         0) AS Doc_LDiscountTotal, 
                         ISNULL(trOrderLineCurrencyDoc.LDiscountVI1 + trOrderLineCurrencyDoc.LDiscountVI2 + trOrderLineCurrencyDoc.LDiscountVI3 + trOrderLineCurrencyDoc.LDiscountVI4 + trOrderLineCurrencyDoc.LDiscountVI5, 0) 
                         AS Doc_LDiscountVITotal, dbo.trOrderHeader.LocalCurrencyCode AS Loc_CurrencyCode, ISNULL(trOrderLineCurrencyLoc.ExchangeRate, 0) AS Loc_ExchangeRate, ISNULL(trOrderLineCurrencyLoc.PriceVI, 0) AS Loc_PriceVI, 
                         ISNULL(trOrderLineCurrencyLoc.AmountVI, 0) AS Loc_AmountVI, ISNULL(trOrderLineCurrencyLoc.Price, 0) AS Loc_Price, ISNULL(trOrderLineCurrencyLoc.Amount, 0) AS Loc_Amount, ISNULL(trOrderLineCurrencyLoc.LDiscount1, 0) 
                         AS Loc_LDiscount1, ISNULL(trOrderLineCurrencyLoc.LDiscount2, 0) AS Loc_LDiscount2, ISNULL(trOrderLineCurrencyLoc.LDiscount3, 0) AS Loc_LDiscount3, ISNULL(trOrderLineCurrencyLoc.LDiscount4, 0) AS Loc_LDiscount4, 
                         ISNULL(trOrderLineCurrencyLoc.LDiscount5, 0) AS Loc_LDiscount5, ISNULL(trOrderLineCurrencyLoc.LDiscountVI1, 0) AS Loc_LDiscountVI1, ISNULL(trOrderLineCurrencyLoc.LDiscountVI2, 0) AS Loc_LDiscountVI2, 
                         ISNULL(trOrderLineCurrencyLoc.LDiscountVI3, 0) AS Loc_LDiscountVI3, ISNULL(trOrderLineCurrencyLoc.LDiscountVI4, 0) AS Loc_LDiscountVI4, ISNULL(trOrderLineCurrencyLoc.LDiscountVI5, 0) AS Loc_LDiscountVI5, 
                         ISNULL(trOrderLineCurrencyLoc.TDiscount1, 0) AS Loc_TDiscount1, ISNULL(trOrderLineCurrencyLoc.TDiscount2, 0) AS Loc_TDiscount2, ISNULL(trOrderLineCurrencyLoc.TDiscount3, 0) AS Loc_TDiscount3, 
                         ISNULL(trOrderLineCurrencyLoc.TDiscount4, 0) AS Loc_TDiscount4, ISNULL(trOrderLineCurrencyLoc.TDiscount5, 0) AS Loc_TDiscount5, ISNULL(trOrderLineCurrencyLoc.TDiscountVI1, 0) AS Loc_TDiscountVI1, 
                         ISNULL(trOrderLineCurrencyLoc.TDiscountVI2, 0) AS Loc_TDiscountVI2, ISNULL(trOrderLineCurrencyLoc.TDiscountVI3, 0) AS Loc_TDiscountVI3, ISNULL(trOrderLineCurrencyLoc.TDiscountVI4, 0) AS Loc_TDiscountVI4, 
                         ISNULL(trOrderLineCurrencyLoc.TDiscountVI5, 0) AS Loc_TDiscountVI5, ISNULL(trOrderLineCurrencyLoc.TaxBase, 0) AS Loc_TaxBase, ISNULL(trOrderLineCurrencyLoc.Pct, 0) AS Loc_Pct, ISNULL(trOrderLineCurrencyLoc.Vat, 0) 
                         AS Loc_Vat, ISNULL(trOrderLineCurrencyLoc.VatDeducation, 0) AS Loc_VatDeducation, ISNULL(trOrderLineCurrencyLoc.NetAmount, 0) AS Loc_NetAmount, 
                         ISNULL(trOrderLineCurrencyLoc.TDiscount1 + trOrderLineCurrencyLoc.TDiscount2 + trOrderLineCurrencyLoc.TDiscount3 + trOrderLineCurrencyLoc.TDiscount4 + trOrderLineCurrencyLoc.TDiscount5, 0) AS Loc_TDiscountTotal, 
                         ISNULL(trOrderLineCurrencyLoc.TDiscountVI1 + trOrderLineCurrencyLoc.TDiscountVI2 + trOrderLineCurrencyLoc.TDiscountVI3 + trOrderLineCurrencyLoc.TDiscountVI4 + trOrderLineCurrencyLoc.TDiscountVI5, 0) 
                         AS Loc_TDiscountVITotal, ISNULL(trOrderLineCurrencyLoc.LDiscount1 + trOrderLineCurrencyLoc.LDiscount2 + trOrderLineCurrencyLoc.LDiscount3 + trOrderLineCurrencyLoc.LDiscount4 + trOrderLineCurrencyLoc.LDiscount5, 0) 
                         AS Loc_LDiscountTotal, 
                         ISNULL(trOrderLineCurrencyLoc.LDiscountVI1 + trOrderLineCurrencyLoc.LDiscountVI2 + trOrderLineCurrencyLoc.LDiscountVI3 + trOrderLineCurrencyLoc.LDiscountVI4 + trOrderLineCurrencyLoc.LDiscountVI5, 0) 
                         AS Loc_LDiscountVITotal, dbo.dfGlobalDefault.CompanyCurrencyCode AS Com_CurrencyCode, ISNULL(trOrderLineCurrencyCom.ExchangeRate, 0) AS Com_ExchangeRate, ISNULL(trOrderLineCurrencyCom.PriceVI, 0) 
                         AS Com_PriceVI, ISNULL(trOrderLineCurrencyCom.AmountVI, 0) AS Com_AmountVI, ISNULL(trOrderLineCurrencyCom.Price, 0) AS Com_Price, ISNULL(trOrderLineCurrencyCom.Amount, 0) AS Com_Amount, 
                         ISNULL(trOrderLineCurrencyCom.LDiscount1, 0) AS Com_LDiscount1, ISNULL(trOrderLineCurrencyCom.LDiscount2, 0) AS Com_LDiscount2, ISNULL(trOrderLineCurrencyCom.LDiscount3, 0) AS Com_LDiscount3, 
                         ISNULL(trOrderLineCurrencyCom.LDiscount4, 0) AS Com_LDiscount4, ISNULL(trOrderLineCurrencyCom.LDiscount5, 0) AS Com_LDiscount5, ISNULL(trOrderLineCurrencyCom.LDiscountVI1, 0) AS Com_LDiscountVI1, 
                         ISNULL(trOrderLineCurrencyCom.LDiscountVI2, 0) AS Com_LDiscountVI2, ISNULL(trOrderLineCurrencyCom.LDiscountVI3, 0) AS Com_LDiscountVI3, ISNULL(trOrderLineCurrencyCom.LDiscountVI4, 0) AS Com_LDiscountVI4, 
                         ISNULL(trOrderLineCurrencyCom.LDiscountVI5, 0) AS Com_LDiscountVI5, ISNULL(trOrderLineCurrencyCom.TDiscount1, 0) AS Com_TDiscount1, ISNULL(trOrderLineCurrencyCom.TDiscount2, 0) AS Com_TDiscount2, 
                         ISNULL(trOrderLineCurrencyCom.TDiscount3, 0) AS Com_TDiscount3, ISNULL(trOrderLineCurrencyCom.TDiscount4, 0) AS Com_TDiscount4, ISNULL(trOrderLineCurrencyCom.TDiscount5, 0) AS Com_TDiscount5, 
                         ISNULL(trOrderLineCurrencyCom.TDiscountVI1, 0) AS Com_TDiscountVI1, ISNULL(trOrderLineCurrencyCom.TDiscountVI2, 0) AS Com_TDiscountVI2, ISNULL(trOrderLineCurrencyCom.TDiscountVI3, 0) AS Com_TDiscountVI3, 
                         ISNULL(trOrderLineCurrencyCom.TDiscountVI4, 0) AS Com_TDiscountVI4, ISNULL(trOrderLineCurrencyCom.TDiscountVI5, 0) AS Com_TDiscountVI5, ISNULL(trOrderLineCurrencyCom.TaxBase, 0) AS Com_TaxBase, 
                         ISNULL(trOrderLineCurrencyCom.Pct, 0) AS Com_Pct, ISNULL(trOrderLineCurrencyCom.Vat, 0) AS Com_Vat, ISNULL(trOrderLineCurrencyCom.VatDeducation, 0) AS Com_VatDeducation, 
                         ISNULL(trOrderLineCurrencyCom.NetAmount, 0) AS Com_NetAmount, 
                         ISNULL(trOrderLineCurrencyCom.TDiscount1 + trOrderLineCurrencyCom.TDiscount2 + trOrderLineCurrencyCom.TDiscount3 + trOrderLineCurrencyCom.TDiscount4 + trOrderLineCurrencyCom.TDiscount5, 0) 
                         AS Com_TDiscountTotal, 
                         ISNULL(trOrderLineCurrencyCom.TDiscountVI1 + trOrderLineCurrencyCom.TDiscountVI2 + trOrderLineCurrencyCom.TDiscountVI3 + trOrderLineCurrencyCom.TDiscountVI4 + trOrderLineCurrencyCom.TDiscountVI5, 0) 
                         AS Com_TDiscountVITotal, 
                         ISNULL(trOrderLineCurrencyCom.LDiscount1 + trOrderLineCurrencyCom.LDiscount2 + trOrderLineCurrencyCom.LDiscount3 + trOrderLineCurrencyCom.LDiscount4 + trOrderLineCurrencyCom.LDiscount5, 0) 
                         AS Com_LDiscountTotal, 
                         ISNULL(trOrderLineCurrencyCom.LDiscountVI1 + trOrderLineCurrencyCom.LDiscountVI2 + trOrderLineCurrencyCom.LDiscountVI3 + trOrderLineCurrencyCom.LDiscountVI4 + trOrderLineCurrencyCom.LDiscountVI5, 0) 
                         AS Com_LDiscountVITotal
FROM            dbo.trOrderLine WITH (NOLOCK) INNER JOIN
                         dbo.trOrderHeader WITH (NOLOCK) ON 1 = 1 AND dbo.trOrderHeader.OrderHeaderID = dbo.trOrderLine.OrderHeaderID INNER JOIN
                         dbo.dfGlobalDefault WITH (NOLOCK) ON dbo.dfGlobalDefault.GlobalDefaultCode = 1 LEFT OUTER JOIN
                         dbo.trOrderLineCurrency AS trOrderLineCurrencyDoc WITH (NOLOCK) ON trOrderLineCurrencyDoc.OrderLineID = dbo.trOrderLine.OrderLineID AND 
                         dbo.trOrderLine.DocCurrencyCode = trOrderLineCurrencyDoc.CurrencyCode LEFT OUTER JOIN
                         dbo.trOrderLineCurrency AS trOrderLineCurrencyLoc WITH (NOLOCK) ON trOrderLineCurrencyLoc.OrderLineID = dbo.trOrderLine.OrderLineID AND 
                         dbo.trOrderHeader.LocalCurrencyCode = trOrderLineCurrencyLoc.CurrencyCode LEFT OUTER JOIN
                         dbo.trOrderLineCurrency AS trOrderLineCurrencyCom WITH (NOLOCK) ON trOrderLineCurrencyCom.OrderLineID = dbo.trOrderLine.OrderLineID AND 
                         dbo.dfGlobalDefault.CompanyCurrencyCode = trOrderLineCurrencyCom.CurrencyCode
