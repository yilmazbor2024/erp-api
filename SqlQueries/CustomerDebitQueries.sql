-- CurrAccDebitsByDocCurrency
-- Müşteri ve tedarikçilerin borç detaylarını getiren sorgu

SELECT        AllDebits.CurrAccTypeCode, AllDebits.CurrAccCode, AllDebits.SubCurrAccID, AllDebits.CompanyCode, AllDebits.OfficeCode, AllDebits.StoreTypeCode, AllDebits.StoreCode, AllDebits.DocumentDate, AllDebits.DocumentNumber, 
                         AllDebits.DueDate, AllDebits.LineDescription, AllDebits.RefNumber, AllDebits.Doc_CurrencyCode, AllDebits.Doc_Amount AS Doc_Debit, AllDebits.Loc_CurrencyCode, AllDebits.Loc_ExchangeRate, 
                         AllDebits.Loc_Amount AS Loc_Debit, AllDebits.Loc_Amount - ISNULL(AllPayments.Loc_Payment, 0) AS Loc_Balance, AllDebits.DebitTypeCode, AllDebits.DebitReasonCode, AllDebits.DebitLineID, AllDebits.DebitHeaderID, 
                         AllDebits.ApplicationCode, AllDebits.ApplicationID, AllDebits.ATAtt01, AllDebits.ATAtt02, AllDebits.ATAtt03, AllDebits.ATAtt04, AllDebits.ATAtt05
FROM            (SELECT        dbo.trDebitHeader.CurrAccTypeCode, dbo.trDebitHeader.CurrAccCode, dbo.trDebitHeader.SubCurrAccID, dbo.trDebitHeader.CompanyCode, dbo.trDebitHeader.OfficeCode, dbo.trDebitHeader.StoreTypeCode, 
                                                    dbo.trDebitHeader.StoreCode, dbo.trDebitHeader.DocumentDate, dbo.trDebitHeader.DocumentNumber, dbo.trDebitLine.DueDate, dbo.trDebitLine.LineDescription, dbo.trDebitHeader.RefNumber, 
                                                    dbo.trDebitLine.DocCurrencyCode AS Doc_CurrencyCode, dbo.trDebitHeader.LocalCurrencyCode AS Loc_CurrencyCode, ISNULL(trDebitLineCurrencyLoc.ExchangeRate, 0) AS Loc_ExchangeRate, 
                                                    dbo.trDebitLine.DebitLineID, dbo.trDebitLine.DebitHeaderID, dbo.trDebitHeader.DebitTypeCode, dbo.trDebitLine.DebitReasonCode, dbo.trDebitHeader.ApplicationCode, dbo.trDebitHeader.ApplicationID, 
                                                    CASE WHEN CurrAccTypeCode = 1 THEN ISNULL(trDebitLineCurrencyDoc.Credit, 0) ELSE ISNULL(trDebitLineCurrencyDoc.Debit, 0) END AS Doc_Amount, 
                                                    CASE WHEN CurrAccTypeCode = 1 THEN ISNULL(trDebitLineCurrencyLoc.Credit, 0) ELSE ISNULL(trDebitLineCurrencyLoc.Debit, 0) END AS Loc_Amount, ISNULL(dbo.DebitATAttributesFilter.ATAtt01, SPACE(0)) 
                                                    AS ATAtt01, ISNULL(dbo.DebitATAttributesFilter.ATAtt02, SPACE(0)) AS ATAtt02, ISNULL(dbo.DebitATAttributesFilter.ATAtt03, SPACE(0)) AS ATAtt03, ISNULL(dbo.DebitATAttributesFilter.ATAtt04, SPACE(0)) AS ATAtt04, 
                                                    ISNULL(dbo.DebitATAttributesFilter.ATAtt05, SPACE(0)) AS ATAtt05
                          FROM            dbo.trDebitLine WITH (NOLOCK) INNER JOIN
                                                    dbo.trDebitHeader WITH (NOLOCK) ON dbo.trDebitHeader.DebitHeaderID = dbo.trDebitLine.DebitHeaderID LEFT OUTER JOIN
                                                    dbo.trDebitLineCurrency AS trDebitLineCurrencyDoc WITH (NOLOCK) ON trDebitLineCurrencyDoc.DebitLineID = dbo.trDebitLine.DebitLineID AND 
                                                    dbo.trDebitLine.DocCurrencyCode = trDebitLineCurrencyDoc.CurrencyCode LEFT OUTER JOIN
                                                    dbo.trDebitLineCurrency AS trDebitLineCurrencyLoc WITH (NOLOCK) ON trDebitLineCurrencyLoc.DebitLineID = dbo.trDebitLine.DebitLineID AND 
                                                    dbo.trDebitHeader.LocalCurrencyCode = trDebitLineCurrencyLoc.CurrencyCode LEFT OUTER JOIN
                                                    dbo.DebitATAttributesFilter ON dbo.DebitATAttributesFilter.DebitHeaderID = dbo.trDebitLine.DebitHeaderID
                          WHERE        (dbo.trDebitHeader.DebitTypeCode <> 7)) AS AllDebits LEFT OUTER JOIN
                             (SELECT        dbo.trPaymentLine.DebitLineID, ABS(SUM(dbo.trPaymentLineCurrency.Payment)) AS Loc_Payment
                               FROM            dbo.trPaymentLine WITH (NOLOCK) INNER JOIN
                                                         dbo.trPaymentHeader WITH (NOLOCK) ON dbo.trPaymentHeader.PaymentHeaderID = dbo.trPaymentLine.PaymentHeaderID INNER JOIN
                                                         dbo.trPaymentLineCurrency WITH (NOLOCK) ON dbo.trPaymentLineCurrency.PaymentLineID = dbo.trPaymentLine.PaymentLineID AND 
                                                         dbo.trPaymentLineCurrency.CurrencyCode = dbo.trPaymentHeader.LocalCurrencyCode
                               WHERE        (dbo.trPaymentLine.DebitLineID IS NOT NULL)
                               GROUP BY dbo.trPaymentLine.DebitLineID) AS AllPayments ON AllPayments.DebitLineID = AllDebits.DebitLineID
WHERE        (AllDebits.Loc_Amount - ISNULL(AllPayments.Loc_Payment, 0) > 0) AND (AllDebits.Loc_Amount > 0)
