-- CurrAccCreditsByDocCurrency
-- Müşteri ve tedarikçilerin alacak detaylarını getiren sorgu

SELECT        CurrAccTypeCode = trCurrAccBook.CurrAccTypeCode, CurrAccCode = trCurrAccBook.CurrAccCode, SubCurrAccID = trCurrAccBook.SubCurrAccID, CompanyCode = trCurrAccBook.CompanyCode, 
                         OfficeCode = trCurrAccBook.OfficeCode, StoreTypeCode = trCurrAccBook.StoreTypeCode, StoreCode = trCurrAccBook.StoreCode, DocumentDate = trCurrAccBook.DocumentDate, 
                         DocumentNumber = trCurrAccBook.DocumentNumber, RefNumber = trCurrAccBook.RefNumber, DueDate = trCurrAccBook.DueDate, LineDescription = trCurrAccBook.LineDescription, 
                         Doc_CurrencyCode = trCurrAccBook.DocCurrencyCode, Doc_Amount = (ISNULL(trCurrAccBookCurrencyDoc.Debit, 0) + ISNULL(trCurrAccBookCurrencyDoc.Credit, 0)), Loc_CurrencyCode = trCurrAccBook.LocalCurrencyCode, 
                         Loc_ExchangeRate = ISNULL(trCurrAccBookCurrencyLoc.ExchangeRate, 0), Loc_Amount = (ISNULL(trCurrAccBookCurrencyLoc.Debit, 0) + ISNULL(trCurrAccBookCurrencyLoc.Credit, 0)), 
                         Loc_Balance = (ISNULL(trCurrAccBookCurrencyLoc.Debit, 0) + ISNULL(trCurrAccBookCurrencyLoc.Credit, 0)) - ISNULL(AllPayments.Loc_Payment, 0), ApplicationCode = trCurrAccBook.ApplicationCode, 
                         ApplicationLineID = trCurrAccBook.ApplicationID, CurrAccBookID = trCurrAccBook.CurrAccBookID, PaymentTypeCode = 5, ATAtt01, ATAtt02, ATAtt03, ATAtt04, ATAtt05
FROM            trCurrAccBook WITH (NOLOCK) LEFT OUTER JOIN
                         trCurrAccBookCurrency AS trCurrAccBookCurrencyDoc WITH (NOLOCK) ON trCurrAccBookCurrencyDoc.CurrAccBookID = trCurrAccBook.CurrAccBookID AND 
                         trCurrAccBook.DocCurrencyCode = trCurrAccBookCurrencyDoc.CurrencyCode LEFT OUTER JOIN
                         trCurrAccBookCurrency AS trCurrAccBookCurrencyLoc WITH (NOLOCK) ON trCurrAccBookCurrencyLoc.CurrAccBookID = trCurrAccBook.CurrAccBookID AND 
                         trCurrAccBook.LocalCurrencyCode = trCurrAccBookCurrencyLoc.CurrencyCode LEFT OUTER JOIN
                             (SELECT        CurrAccTypeCode, CurrAccCode, ReverseDebitLineID, Loc_CurrencyCode, Loc_Payment = SUM(Loc_Payment)
                               FROM            (SELECT        trPaymentHeader.CurrAccTypeCode, trPaymentHeader.CurrAccCode, trPaymentLine.ReverseDebitLineID, Loc_CurrencyCode = trPaymentHeader.LocalCurrencyCode, 
                                                                                   Loc_Payment = ABS(SUM(trPaymentLineCurrency.Payment))
                                                         FROM            trPaymentLine WITH (NOLOCK) INNER JOIN
                                                                                   trPaymentHeader WITH (NOLOCK) ON trPAymentHeader.PaymentHeaderID = trPaymentLine.PaymentHeaderID INNER JOIN
                                                                                   trPaymentLineCurrency WITH (NOLOCK) ON trPaymentLineCurrency.PaymentLineID = trPaymentLine.PaymentLineID AND 
                                                                                   trPaymentLineCurrency.CurrencyCode = trPaymentHeader.LocalCurrencyCode
                                                         WHERE        ReverseDebitLineID IS NOT NULL
                                                         GROUP BY trPaymentHeader.CurrAccTypeCode, trPaymentHeader.CurrAccCode, trPaymentLine.ReverseDebitLineID, trPaymentHeader.LocalCurrencyCode
                                                         UNION ALL
                                                         SELECT        trPaymentHeader.CurrAccTypeCode, trPaymentHeader.CurrAccCode, trPaymentLine.DebitLineID, Loc_CurrencyCode = trPaymentHeader.LocalCurrencyCode, 
                                                                                  Loc_Payment = ABS(SUM(trPaymentLineCurrency.Payment))
                                                         FROM            trPaymentLine WITH (NOLOCK) INNER JOIN
                                                                                  trPaymentHeader WITH (NOLOCK) ON trPAymentHeader.PaymentHeaderID = trPaymentLine.PaymentHeaderID INNER JOIN
                                                                                  trPaymentLineCurrency WITH (NOLOCK) ON trPaymentLineCurrency.PaymentLineID = trPaymentLine.PaymentLineID AND 
                                                                                  trPaymentLineCurrency.CurrencyCode = trPaymentHeader.LocalCurrencyCode
                                                         WHERE        DebitLineID IS NOT NULL
                                                         GROUP BY trPaymentHeader.CurrAccTypeCode, trPaymentHeader.CurrAccCode, trPaymentLine.DebitLineID, trPaymentHeader.LocalCurrencyCode) AS DATA
                               GROUP BY CurrAccTypeCode, CurrAccCode, ReverseDebitLineID, Loc_CurrencyCode) AS AllPayments ON AllPayments.ReverseDebitLineID = trCurrAccBook.ApplicationID AND 
                         AllPayments.CurrAccTypeCode = trCurrAccBook.CurrAccTypeCode AND AllPayments.CurrAccCode = trCurrAccBook.CurrAccCode LEFT OUTER JOIN
                         CurrAccBookATAttributesFilter CurrAccBookATAttributes ON CurrAccBookATAttributes.CurrAccBookID = trCurrAccBook.CurrAccBookID
WHERE        trCurrAccBook.ApplicationCode = N'Debit' AND CASE WHEN trCurrAccBook.CurrAccTypeCode IN (1) THEN ISNULL(trCurrAccBookCurrencyLoc.Credit, 0) ELSE ISNULL(trCurrAccBookCurrencyLoc.Debit, 0) END = 0 AND 
                         CASE WHEN trCurrAccBook.CurrAccTypeCode IN (1) THEN ISNULL(trCurrAccBookCurrencyLoc.Debit, 0) ELSE ISNULL(trCurrAccBookCurrencyLoc.Credit, 0) END <> 0 AND (ISNULL(trCurrAccBookCurrencyLoc.Debit, 0) 
                         + ISNULL(trCurrAccBookCurrencyLoc.Credit, 0)) - ISNULL(AllPayments.Loc_Payment, 0) > 0 AND trCurrAccBook.CurrAccTypeCode IN (1, 2, 3, 4, 5, 6, 7, 8, 9)
