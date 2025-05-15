-- CurrAccDebitsByDocCurrency
-- Müşteri ve tedarikçilerin döviz bazında borç detaylarını getiren sorgu

SELECT        AllDebits.CurrAccTypeCode, AllDebits.CurrAccCode, AllDebits.SubCurrAccID, AllDebits.CompanyCode, AllDebits.OfficeCode, AllDebits.StoreTypeCode, AllDebits.StoreCode, AllDebits.DocumentDate, AllDebits.DocumentNumber, 
                         AllDebits.DueDate, AllDebits.LineDescription, AllDebits.RefNumber, AllDebits.Doc_CurrencyCode, AllDebits.Doc_Amount AS Doc_Debit, AllDebits.Doc_Amount - ISNULL(AllPayments_Doc.Doc_Payment, 0) AS Doc_Balance, 
                         AllDebits.Loc_CurrencyCode, AllDebits.Loc_ExchangeRate, AllDebits.Loc_Amount AS Loc_Debit, AllDebits.Loc_Amount - ISNULL(AllPayments.Loc_Payment, 0) AS Loc_Balance, AllDebits.DebitTypeCode, 
                         AllDebits.DebitReasonCode, AllDebits.DebitLineID, AllDebits.DebitHeaderID, AllDebits.ApplicationCode, AllDebits.ApplicationID, AllDebits.ATAtt01, AllDebits.ATAtt02, AllDebits.ATAtt03, AllDebits.ATAtt04, AllDebits.ATAtt05
FROM            (SELECT        dbo.AllDebits.CurrAccTypeCode, dbo.AllDebits.CurrAccCode, dbo.AllDebits.SubCurrAccID, dbo.AllDebits.CompanyCode, dbo.AllDebits.OfficeCode, dbo.AllDebits.StoreTypeCode, dbo.AllDebits.StoreCode, 
                                                    dbo.AllDebits.DocumentDate, dbo.AllDebits.DocumentNumber, dbo.AllDebits.DueDate, dbo.AllDebits.LineDescription, dbo.AllDebits.RefNumber, dbo.AllDebits.Doc_CurrencyCode, dbo.AllDebits.Loc_CurrencyCode, 
                                                    dbo.AllDebits.Loc_ExchangeRate, dbo.AllDebits.DebitLineID, dbo.AllDebits.DebitHeaderID, dbo.AllDebits.DebitTypeCode, dbo.AllDebits.DebitReasonCode, dbo.AllDebits.ApplicationCode, dbo.AllDebits.ApplicationID, 
                                                    CASE WHEN CurrAccTypeCode = 1 THEN Doc_Credit ELSE Doc_Debit END AS Doc_Amount, CASE WHEN CurrAccTypeCode = 1 THEN Loc_Credit ELSE Loc_Debit END AS Loc_Amount, 
                                                    ISNULL(dbo.DebitATAttributesFilter.ATAtt01, SPACE(0)) AS ATAtt01, ISNULL(dbo.DebitATAttributesFilter.ATAtt02, SPACE(0)) AS ATAtt02, ISNULL(dbo.DebitATAttributesFilter.ATAtt03, SPACE(0)) AS ATAtt03, 
                                                    ISNULL(dbo.DebitATAttributesFilter.ATAtt04, SPACE(0)) AS ATAtt04, ISNULL(dbo.DebitATAttributesFilter.ATAtt05, SPACE(0)) AS ATAtt05
                          FROM            dbo.AllDebits LEFT OUTER JOIN
                                                    dbo.DebitATAttributesFilter ON dbo.DebitATAttributesFilter.DebitHeaderID = dbo.AllDebits.DebitHeaderID
                          WHERE        (dbo.AllDebits.DebitTypeCode <> 7)) AS AllDebits LEFT OUTER JOIN
                             (SELECT        DebitLineID, Loc_CurrencyCode, ABS(SUM(Loc_Payment)) AS Loc_Payment
                               FROM            dbo.AllPayments
                               GROUP BY DebitLineID, Loc_CurrencyCode) AS AllPayments ON AllPayments.DebitLineID = AllDebits.DebitLineID LEFT OUTER JOIN
                             (SELECT        DebitLineID, Doc_CurrencyCode, ABS(SUM(Doc_Payment)) AS Doc_Payment
                               FROM            dbo.AllPayments
                               GROUP BY DebitLineID, Doc_CurrencyCode) AS AllPayments_Doc ON AllPayments_Doc.DebitLineID = AllDebits.DebitLineID AND AllPayments_Doc.Doc_CurrencyCode = AllDebits.Doc_CurrencyCode
WHERE        (AllDebits.Doc_Amount - ISNULL(AllPayments_Doc.Doc_Payment, 0) > 0) AND (AllDebits.Doc_Amount > 0)
