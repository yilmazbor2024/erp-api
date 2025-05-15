-- CurrAccCreditsByDocCurrency
-- Müşteri ve tedarikçilerin döviz bazında alacak detaylarını getiren sorgu

SELECT        CurrAccTypeCode = AllCurrAccBooks.CurrAccTypeCode, CurrAccCode = AllCurrAccBooks.CurrAccCode, SubCurrAccID = AllCurrAccBooks.SubCurrAccID, CompanyCode = AllCurrAccBooks.CompanyCode, 
                         OfficeCode = AllCurrAccBooks.OfficeCode, StoreTypeCode = AllCurrAccBooks.StoreTypeCode, StoreCode = AllCurrAccBooks.StoreCode, DocumentDate = AllCurrAccBooks.DocumentDate, 
                         DocumentNumber = AllCurrAccBooks.DocumentNumber, RefNumber = AllCurrAccBooks.RefNumber, DueDate = AllCurrAccBooks.DueDate, LineDescription = AllCurrAccBooks.LineDescription, 
                         Doc_CurrencyCode = AllCurrAccBooks.Doc_CurrencyCode, Doc_Amount = (AllCurrAccBooks.Doc_Debit + AllCurrAccBooks.Doc_Credit), Doc_Balance = (AllCurrAccBooks.Doc_Debit + AllCurrAccBooks.Doc_Credit) 
                         - ISNULL(AllPayments_Doc.Doc_Payment, 0), Loc_CurrencyCode = AllCurrAccBooks.Loc_CurrencyCode, Loc_ExchangeRate = AllCurrAccBooks.Loc_ExchangeRate, 
                         Loc_Amount = (AllCurrAccBooks.Loc_Debit + AllCurrAccBooks.Loc_Credit), Loc_Balance = (AllCurrAccBooks.Loc_Debit + AllCurrAccBooks.Loc_Credit) - ISNULL(AllPayments.Loc_Payment, 0), 
                         ApplicationCode = AllCurrAccBooks.ApplicationCode, ApplicationLineID = AllCurrAccBooks.ApplicationID, CurrAccBookID = AllCurrAccBooks.CurrAccBookID, PaymentTypeCode = 5, ATAtt01, ATAtt02, ATAtt03, ATAtt04, ATAtt05
FROM            AllCurrAccBooks LEFT OUTER JOIN
                             (SELECT        CurrAccTypeCode, CurrAccCode, ReverseDebitLineID, Loc_CurrencyCode, Loc_Payment = SUM(Loc_Payment)
                               FROM            (SELECT        CurrAccTypeCode, CurrAccCode, ReverseDebitLineID, Loc_CurrencyCode, Loc_Payment = ABS(SUM(Loc_Payment))
                                                         FROM            AllPayments
                                                         GROUP BY CurrAccTypeCode, CurrAccCode, ReverseDebitLineID, Loc_CurrencyCode
                                                         UNION ALL
                                                         SELECT        CurrAccTypeCode, CurrAccCode, ReverseDebitLineID = DebitLineID, Loc_CurrencyCode, Loc_Payment = ABS(SUM(Loc_Payment))
                                                         FROM            AllPayments
                                                         GROUP BY CurrAccTypeCode, CurrAccCode, DebitLineID, Loc_CurrencyCode) AS DATA
                               GROUP BY CurrAccTypeCode, CurrAccCode, ReverseDebitLineID, Loc_CurrencyCode) AS AllPayments ON AllPayments.ReverseDebitLineID = AllCurrAccBooks.ApplicationID AND 
                         AllPayments.CurrAccTypeCode = AllCurrAccBooks.CurrAccTypeCode AND AllPayments.CurrAccCode = AllCurrAccBooks.CurrAccCode LEFT OUTER JOIN
                             (SELECT        CurrAccTypeCode, CurrAccCode, ReverseDebitLineID, Doc_CurrencyCode, Doc_Payment = SUM(Doc_Payment)
                               FROM            (SELECT        CurrAccTypeCode, CurrAccCode, ReverseDebitLineID, Doc_CurrencyCode, Doc_Payment = ABS(SUM(Doc_Payment))
                                                         FROM            AllPayments
                                                         GROUP BY CurrAccTypeCode, CurrAccCode, ReverseDebitLineID, Doc_CurrencyCode
                                                         UNION ALL
                                                         SELECT        CurrAccTypeCode, CurrAccCode, ReverseDebitLineID = DebitLineID, Doc_CurrencyCode, Doc_Payment = ABS(SUM(Doc_Payment))
                                                         FROM            AllPayments
                                                         GROUP BY CurrAccTypeCode, CurrAccCode, DebitLineID, Doc_CurrencyCode) AS DATA
                               GROUP BY CurrAccTypeCode, CurrAccCode, ReverseDebitLineID, Doc_CurrencyCode) AS AllPayments_Doc ON AllPayments_Doc.ReverseDebitLineID = AllCurrAccBooks.ApplicationID AND 
                         AllPayments_Doc.CurrAccTypeCode = AllCurrAccBooks.CurrAccTypeCode AND AllPayments_Doc.CurrAccCode = AllCurrAccBooks.CurrAccCode LEFT OUTER JOIN
                         CurrAccBookATAttributesFilter CurrAccBookATAttributes ON CurrAccBookATAttributes.CurrAccBookID = AllCurrAccBooks.CurrAccBookID
WHERE        AllCurrAccBooks.ApplicationCode = 'Debit' AND CASE WHEN AllCurrAccBooks.CurrAccTypeCode IN (1) THEN AllCurrAccBooks.Doc_Credit ELSE AllCurrAccBooks.Doc_Debit END = 0 AND 
                         CASE WHEN AllCurrAccBooks.CurrAccTypeCode IN (1) THEN AllCurrAccBooks.Doc_Debit ELSE AllCurrAccBooks.Doc_Credit END <> 0 AND (AllCurrAccBooks.Doc_Debit + AllCurrAccBooks.Doc_Credit) 
                         - ISNULL(AllPayments_Doc.Doc_Payment, 0) > 0
UNION ALL
SELECT        CurrAccTypeCode = AllCurrAccBooks.CurrAccTypeCode, CurrAccCode = AllCurrAccBooks.CurrAccCode, SubCurrAccID = AllCurrAccBooks.SubCurrAccID, CompanyCode = AllCurrAccBooks.CompanyCode, 
                         OfficeCode = AllCurrAccBooks.OfficeCode, StoreTypeCode = AllCurrAccBooks.StoreTypeCode, StoreCode = AllCurrAccBooks.StoreCode, DocumentDate = AllCurrAccBooks.DocumentDate, 
                         DocumentNumber = AllCurrAccBooks.DocumentNumber, RefNumber = AllCurrAccBooks.RefNumber, DueDate = AllCurrAccBooks.DueDate, LineDescription = AllCurrAccBooks.LineDescription, 
                         Doc_CurrencyCode = AllCurrAccBooks.Doc_CurrencyCode, Doc_Amount = (AllCurrAccBooks.Doc_Debit + AllCurrAccBooks.Doc_Credit), Doc_Balance = (AllCurrAccBooks.Doc_Debit + AllCurrAccBooks.Doc_Credit) 
                         - ISNULL(AllPayments_Doc.Doc_Payment, 0), Loc_CurrencyCode = AllCurrAccBooks.Loc_CurrencyCode, Loc_ExchangeRate = AllCurrAccBooks.Loc_ExchangeRate, 
                         Loc_Amount = (AllCurrAccBooks.Loc_Debit + AllCurrAccBooks.Loc_Credit), Loc_Balance = (AllCurrAccBooks.Loc_Debit + AllCurrAccBooks.Loc_Credit) - ISNULL(AllPayments.Loc_Payment, 0), 
                         ApplicationCode = AllCurrAccBooks.ApplicationCode, ApplicationLineID = AllCurrAccBooks.ApplicationID, CurrAccBookID = AllCurrAccBooks.CurrAccBookID, PaymentTypeCode = 2, ATAtt01, ATAtt02, ATAtt03, ATAtt04, ATAtt05
FROM            AllCurrAccBooks LEFT OUTER JOIN
                             (SELECT        CreditCardPaymentLineID, Loc_CurrencyCode, ABS(SUM(Loc_Payment)) AS Loc_Payment
                               FROM            AllPayments
                               GROUP BY CreditCardPaymentLineID, Loc_CurrencyCode) AS AllPayments ON AllPayments.CreditCardPaymentLineID = AllCurrAccBooks.ApplicationID LEFT OUTER JOIN
                             (SELECT        CreditCardPaymentLineID, Doc_CurrencyCode, ABS(SUM(Doc_Payment)) AS Doc_Payment
                               FROM            AllPayments
                               GROUP BY CreditCardPaymentLineID, Doc_CurrencyCode) AS AllPayments_Doc ON AllPayments_Doc.CreditCardPaymentLineID = AllCurrAccBooks.ApplicationID LEFT OUTER JOIN
                         CurrAccBookATAttributesFilter CurrAccBookATAttributes ON CurrAccBookATAttributes.CurrAccBookID = AllCurrAccBooks.CurrAccBookID
WHERE        AllCurrAccBooks.ApplicationCode = 'CCPay' AND CASE WHEN AllCurrAccBooks.CurrAccTypeCode IN (1) THEN AllCurrAccBooks.Doc_Credit ELSE AllCurrAccBooks.Doc_Debit END = 0 AND 
                         CASE WHEN AllCurrAccBooks.CurrAccTypeCode IN (1) THEN AllCurrAccBooks.Doc_Debit ELSE AllCurrAccBooks.Doc_Credit END <> 0 AND (AllCurrAccBooks.Doc_Debit + AllCurrAccBooks.Doc_Credit) 
                         - ISNULL(AllPayments_Doc.Doc_Payment, 0) > 0
UNION ALL
SELECT        CurrAccTypeCode = AllCurrAccBooks.CurrAccTypeCode, CurrAccCode = AllCurrAccBooks.CurrAccCode, SubCurrAccID = AllCurrAccBooks.SubCurrAccID, CompanyCode = AllCurrAccBooks.CompanyCode, 
                         OfficeCode = AllCurrAccBooks.OfficeCode, StoreTypeCode = AllCurrAccBooks.StoreTypeCode, StoreCode = AllCurrAccBooks.StoreCode, DocumentDate = AllCurrAccBooks.DocumentDate, 
                         DocumentNumber = AllCurrAccBooks.DocumentNumber, RefNumber = AllCurrAccBooks.RefNumber, DueDate = AllCurrAccBooks.DueDate, LineDescription = AllCurrAccBooks.LineDescription, 
                         Doc_CurrencyCode = AllCurrAccBooks.Doc_CurrencyCode, Doc_Amount = (AllCurrAccBooks.Doc_Debit + AllCurrAccBooks.Doc_Credit), Doc_Balance = (AllCurrAccBooks.Doc_Debit + AllCurrAccBooks.Doc_Credit) 
                         - ISNULL(AllPayments_Doc.Doc_Payment, 0), Loc_CurrencyCode = AllCurrAccBooks.Loc_CurrencyCode, Loc_ExchangeRate = AllCurrAccBooks.Loc_ExchangeRate, 
                         Loc_Amount = (AllCurrAccBooks.Loc_Debit + AllCurrAccBooks.Loc_Credit), Loc_Balance = (AllCurrAccBooks.Loc_Debit + AllCurrAccBooks.Loc_Credit) - ISNULL(AllPayments.Loc_Payment, 0), 
                         ApplicationCode = AllCurrAccBooks.ApplicationCode, ApplicationLineID = AllCurrAccBooks.ApplicationID, CurrAccBookID = AllCurrAccBooks.CurrAccBookID, PaymentTypeCode = 3, ATAtt01, ATAtt02, ATAtt03, ATAtt04, ATAtt05
FROM            AllCurrAccBooks LEFT OUTER JOIN
                             (SELECT        GiftCardPaymentLineID, Loc_CurrencyCode, ABS(SUM(Loc_Payment)) AS Loc_Payment
                               FROM            AllPayments
                               GROUP BY GiftCardPaymentLineID, Loc_CurrencyCode) AS AllPayments ON AllPayments.GiftCardPaymentLineID = AllCurrAccBooks.ApplicationID LEFT OUTER JOIN
                             (SELECT        GiftCardPaymentLineID, Doc_CurrencyCode, ABS(SUM(Doc_Payment)) AS Doc_Payment
                               FROM            AllPayments
                               GROUP BY GiftCardPaymentLineID, Doc_CurrencyCode) AS AllPayments_Doc ON AllPayments_Doc.GiftCardPaymentLineID = AllCurrAccBooks.ApplicationID LEFT OUTER JOIN
                         CurrAccBookATAttributesFilter CurrAccBookATAttributes ON CurrAccBookATAttributes.CurrAccBookID = AllCurrAccBooks.CurrAccBookID
WHERE        AllCurrAccBooks.ApplicationCode = 'GCPay' AND CASE WHEN AllCurrAccBooks.CurrAccTypeCode IN (1) THEN AllCurrAccBooks.Doc_Credit ELSE AllCurrAccBooks.Doc_Debit END = 0 AND 
                         CASE WHEN AllCurrAccBooks.CurrAccTypeCode IN (1) THEN AllCurrAccBooks.Doc_Debit ELSE AllCurrAccBooks.Doc_Credit END <> 0 AND (AllCurrAccBooks.Doc_Debit + AllCurrAccBooks.Doc_Credit) 
                         - ISNULL(AllPayments_Doc.Doc_Payment, 0) > 0
UNION ALL
SELECT        CurrAccTypeCode = AllCurrAccBooks.CurrAccTypeCode, CurrAccCode = AllCurrAccBooks.CurrAccCode, SubCurrAccID = AllCurrAccBooks.SubCurrAccID, CompanyCode = AllCurrAccBooks.CompanyCode, 
                         OfficeCode = AllCurrAccBooks.OfficeCode, StoreTypeCode = AllCurrAccBooks.StoreTypeCode, StoreCode = AllCurrAccBooks.StoreCode, DocumentDate = AllCurrAccBooks.DocumentDate, 
                         DocumentNumber = AllCurrAccBooks.DocumentNumber, RefNumber = AllCurrAccBooks.RefNumber, DueDate = AllCurrAccBooks.DueDate, LineDescription = AllCurrAccBooks.LineDescription, 
                         Doc_CurrencyCode = AllCurrAccBooks.Doc_CurrencyCode, Doc_Amount = (AllCurrAccBooks.Doc_Debit + AllCurrAccBooks.Doc_Credit), Doc_Balance = (AllCurrAccBooks.Doc_Debit + AllCurrAccBooks.Doc_Credit) 
                         - ISNULL(AllPayments_Doc.Doc_Payment, 0), Loc_CurrencyCode = AllCurrAccBooks.Loc_CurrencyCode, Loc_ExchangeRate = AllCurrAccBooks.Loc_ExchangeRate, 
                         Loc_Amount = (AllCurrAccBooks.Loc_Debit + AllCurrAccBooks.Loc_Credit), Loc_Balance = (AllCurrAccBooks.Loc_Debit + AllCurrAccBooks.Loc_Credit) - ISNULL(AllPayments.Loc_Payment, 0), 
                         ApplicationCode = AllCurrAccBooks.ApplicationCode, ApplicationLineID = AllCurrAccBooks.ApplicationID, CurrAccBookID = AllCurrAccBooks.CurrAccBookID, PaymentTypeCode = 70, ATAtt01, ATAtt02, ATAtt03, ATAtt04, 
                         ATAtt05
FROM            AllCurrAccBooks LEFT OUTER JOIN
                             (SELECT        OtherPaymentLineID, Loc_CurrencyCode, ABS(SUM(Loc_Payment)) AS Loc_Payment
                               FROM            AllPayments
                               GROUP BY OtherPaymentLineID, Loc_CurrencyCode) AS AllPayments ON AllPayments.OtherPaymentLineID = AllCurrAccBooks.ApplicationID LEFT OUTER JOIN
                             (SELECT        OtherPaymentLineID, Doc_CurrencyCode, ABS(SUM(Doc_Payment)) AS Doc_Payment
                               FROM            AllPayments
                               GROUP BY OtherPaymentLineID, Doc_CurrencyCode) AS AllPayments_Doc ON AllPayments_Doc.OtherPaymentLineID = AllCurrAccBooks.ApplicationID LEFT OUTER JOIN
                         CurrAccBookATAttributesFilter CurrAccBookATAttributes ON CurrAccBookATAttributes.CurrAccBookID = AllCurrAccBooks.CurrAccBookID
WHERE        AllCurrAccBooks.ApplicationCode = 'OPay' AND CASE WHEN AllCurrAccBooks.CurrAccTypeCode IN (1) THEN AllCurrAccBooks.Doc_Credit ELSE AllCurrAccBooks.Doc_Debit END = 0 AND 
                         CASE WHEN AllCurrAccBooks.CurrAccTypeCode IN (1) THEN AllCurrAccBooks.Doc_Debit ELSE AllCurrAccBooks.Doc_Credit END <> 0 AND (AllCurrAccBooks.Doc_Debit + AllCurrAccBooks.Doc_Credit) 
                         - ISNULL(AllPayments_Doc.Doc_Payment, 0) > 0
UNION ALL
SELECT        CurrAccTypeCode = AllCurrAccBooks.CurrAccTypeCode, CurrAccCode = AllCurrAccBooks.CurrAccCode, SubCurrAccID = AllCurrAccBooks.SubCurrAccID, CompanyCode = AllCurrAccBooks.CompanyCode, 
                         OfficeCode = AllCurrAccBooks.OfficeCode, StoreTypeCode = AllCurrAccBooks.StoreTypeCode, StoreCode = AllCurrAccBooks.StoreCode, DocumentDate = AllCurrAccBooks.DocumentDate, 
                         DocumentNumber = AllCurrAccBooks.DocumentNumber, RefNumber = AllCurrAccBooks.RefNumber, DueDate = AllCurrAccBooks.DueDate, LineDescription = AllCurrAccBooks.LineDescription, 
                         Doc_CurrencyCode = AllCurrAccBooks.Doc_CurrencyCode, Doc_Amount = (AllCurrAccBooks.Doc_Debit + AllCurrAccBooks.Doc_Credit), Doc_Balance = (AllCurrAccBooks.Doc_Debit + AllCurrAccBooks.Doc_Credit) 
                         - ISNULL(AllPayments_Doc.Doc_Payment, 0), Loc_CurrencyCode = AllCurrAccBooks.Loc_CurrencyCode, Loc_ExchangeRate = AllCurrAccBooks.Loc_ExchangeRate, 
                         Loc_Amount = (AllCurrAccBooks.Loc_Debit + AllCurrAccBooks.Loc_Credit), Loc_Balance = (AllCurrAccBooks.Loc_Debit + AllCurrAccBooks.Loc_Credit) - ISNULL(AllPayments.Loc_Payment, 0), 
                         ApplicationCode = AllCurrAccBooks.ApplicationCode, ApplicationLineID = AllCurrAccBooks.ApplicationID, CurrAccBookID = AllCurrAccBooks.CurrAccBookID, PaymentTypeCode = 1, ATAtt01, ATAtt02, ATAtt03, ATAtt04, ATAtt05
FROM            AllCurrAccBooks LEFT OUTER JOIN
                             (SELECT        CashLineID, Loc_CurrencyCode, ABS(SUM(Loc_Payment)) AS Loc_Payment
                               FROM            AllPayments
                               GROUP BY CashLineID, Loc_CurrencyCode) AS AllPayments ON AllPayments.CashLineID = AllCurrAccBooks.ApplicationID LEFT OUTER JOIN
                             (SELECT        CashLineID, Doc_CurrencyCode, ABS(SUM(Doc_Payment)) AS Doc_Payment
                               FROM            AllPayments
                               GROUP BY CashLineID, Doc_CurrencyCode) AS AllPayments_Doc ON AllPayments_Doc.CashLineID = AllCurrAccBooks.ApplicationID LEFT OUTER JOIN
                         CurrAccBookATAttributesFilter CurrAccBookATAttributes ON CurrAccBookATAttributes.CurrAccBookID = AllCurrAccBooks.CurrAccBookID
WHERE        AllCurrAccBooks.ApplicationCode = 'Cash' AND CASE WHEN AllCurrAccBooks.CurrAccTypeCode IN (1) THEN AllCurrAccBooks.Doc_Credit ELSE AllCurrAccBooks.Doc_Debit END = 0 AND 
                         CASE WHEN AllCurrAccBooks.CurrAccTypeCode IN (1) THEN AllCurrAccBooks.Doc_Debit ELSE AllCurrAccBooks.Doc_Credit END <> 0 AND (AllCurrAccBooks.Doc_Debit + AllCurrAccBooks.Doc_Credit) 
                         - ISNULL(AllPayments_Doc.Doc_Payment, 0) > 0
UNION ALL
SELECT        CurrAccTypeCode = AllCurrAccBooks.CurrAccTypeCode, CurrAccCode = AllCurrAccBooks.CurrAccCode, SubCurrAccID = AllCurrAccBooks.SubCurrAccID, CompanyCode = AllCurrAccBooks.CompanyCode, 
                         OfficeCode = AllCurrAccBooks.OfficeCode, StoreTypeCode = AllCurrAccBooks.StoreTypeCode, StoreCode = AllCurrAccBooks.StoreCode, DocumentDate = AllCurrAccBooks.DocumentDate, 
                         DocumentNumber = AllCurrAccBooks.DocumentNumber, RefNumber = AllCurrAccBooks.RefNumber, DueDate = AllCurrAccBooks.DueDate, LineDescription = AllCurrAccBooks.LineDescription, 
                         Doc_CurrencyCode = AllCurrAccBooks.Doc_CurrencyCode, Doc_Amount = (AllCurrAccBooks.Doc_Debit + AllCurrAccBooks.Doc_Credit), Doc_Balance = (AllCurrAccBooks.Doc_Debit + AllCurrAccBooks.Doc_Credit) 
                         - ISNULL(AllPayments_Doc.Doc_Payment, 0), Loc_CurrencyCode = AllCurrAccBooks.Loc_CurrencyCode, Loc_ExchangeRate = AllCurrAccBooks.Loc_ExchangeRate, 
                         Loc_Amount = (AllCurrAccBooks.Loc_Debit + AllCurrAccBooks.Loc_Credit), Loc_Balance = (AllCurrAccBooks.Loc_Debit + AllCurrAccBooks.Loc_Credit) - ISNULL(AllPayments.Loc_Payment, 0), 
                         ApplicationCode = AllCurrAccBooks.ApplicationCode, ApplicationLineID = AllCurrAccBooks.ApplicationID, CurrAccBookID = AllCurrAccBooks.CurrAccBookID, PaymentTypeCode = 4, ATAtt01, ATAtt02, ATAtt03, ATAtt04, ATAtt05
FROM            AllCurrAccBooks LEFT OUTER JOIN
                             (SELECT        BankLineID, Loc_CurrencyCode, ABS(SUM(Loc_Payment)) AS Loc_Payment
                               FROM            AllPayments
                               GROUP BY BankLineID, Loc_CurrencyCode) AS AllPayments ON AllPayments.BankLineID = AllCurrAccBooks.ApplicationID LEFT OUTER JOIN
                             (SELECT        BankLineID, Doc_CurrencyCode, ABS(SUM(Doc_Payment)) AS Doc_Payment
                               FROM            AllPayments
                               GROUP BY BankLineID, Doc_CurrencyCode) AS AllPayments_Doc ON AllPayments_Doc.BankLineID = AllCurrAccBooks.ApplicationID LEFT OUTER JOIN
                         CurrAccBookATAttributesFilter CurrAccBookATAttributes ON CurrAccBookATAttributes.CurrAccBookID = AllCurrAccBooks.CurrAccBookID
WHERE        AllCurrAccBooks.ApplicationCode = 'Bank' AND CASE WHEN AllCurrAccBooks.CurrAccTypeCode IN (1) THEN AllCurrAccBooks.Doc_Credit ELSE AllCurrAccBooks.Doc_Debit END = 0 AND 
                         CASE WHEN AllCurrAccBooks.CurrAccTypeCode IN (1) THEN AllCurrAccBooks.Doc_Debit ELSE AllCurrAccBooks.Doc_Credit END <> 0 AND (AllCurrAccBooks.Doc_Debit + AllCurrAccBooks.Doc_Credit) 
                         - ISNULL(AllPayments_Doc.Doc_Payment, 0) > 0
UNION ALL
SELECT        CurrAccTypeCode = AllCurrAccBooks.CurrAccTypeCode, CurrAccCode = AllCurrAccBooks.CurrAccCode, SubCurrAccID = AllCurrAccBooks.SubCurrAccID, CompanyCode = AllCurrAccBooks.CompanyCode, 
                         OfficeCode = AllCurrAccBooks.OfficeCode, StoreTypeCode = AllCurrAccBooks.StoreTypeCode, StoreCode = AllCurrAccBooks.StoreCode, DocumentDate = AllCurrAccBooks.DocumentDate, 
                         DocumentNumber = AllCurrAccBooks.DocumentNumber, RefNumber = AllCurrAccBooks.RefNumber, DueDate = AllCurrAccBooks.DueDate, LineDescription = AllCurrAccBooks.LineDescription, 
                         Doc_CurrencyCode = AllCurrAccBooks.Doc_CurrencyCode, Doc_Amount = (AllCurrAccBooks.Doc_Debit + AllCurrAccBooks.Doc_Credit), Doc_Balance = (AllCurrAccBooks.Doc_Debit + AllCurrAccBooks.Doc_Credit) 
                         - ISNULL(AllPayments_Doc.Doc_Payment, 0), Loc_CurrencyCode = AllCurrAccBooks.Loc_CurrencyCode, Loc_ExchangeRate = AllCurrAccBooks.Loc_ExchangeRate, 
                         Loc_Amount = (AllCurrAccBooks.Loc_Debit + AllCurrAccBooks.Loc_Credit), Loc_Balance = (AllCurrAccBooks.Loc_Debit + AllCurrAccBooks.Loc_Credit) - ISNULL(AllPayments.Loc_Payment, 0), 
                         ApplicationCode = AllCurrAccBooks.ApplicationCode, ApplicationLineID = AllCurrAccBooks.ApplicationID, CurrAccBookID = AllCurrAccBooks.CurrAccBookID, 
                         PaymentTypeCode = CASE trChequeLine.ChequeTypeCode WHEN 1 THEN 20 WHEN 2 THEN 10 WHEN 3 THEN 50 WHEN 4 THEN 40 END, ATAtt01, ATAtt02, ATAtt03, ATAtt04, ATAtt05
FROM            AllCurrAccBooks LEFT OUTER JOIN
                             (SELECT        ChequeLineID, Loc_CurrencyCode, ABS(SUM(Loc_Payment)) AS Loc_Payment
                               FROM            AllPayments
                               GROUP BY ChequeLineID, Loc_CurrencyCode) AS AllPayments ON AllPayments.ChequeLineID = AllCurrAccBooks.ApplicationID LEFT OUTER JOIN
                             (SELECT        ChequeLineID, Doc_CurrencyCode, ABS(SUM(Doc_Payment)) AS Doc_Payment
                               FROM            AllPayments
                               GROUP BY ChequeLineID, Doc_CurrencyCode) AS AllPayments_Doc ON AllPayments_Doc.ChequeLineID = AllCurrAccBooks.ApplicationID INNER JOIN
                         trChequeLine WITH (NOLOCK) ON trChequeLine.ChequeLineID = AllCurrAccBooks.ApplicationID LEFT OUTER JOIN
                         CurrAccBookATAttributesFilter CurrAccBookATAttributes ON CurrAccBookATAttributes.CurrAccBookID = AllCurrAccBooks.CurrAccBookID
WHERE        AllCurrAccBooks.ApplicationCode = 'Chequ' AND CASE WHEN AllCurrAccBooks.CurrAccTypeCode IN (1) THEN AllCurrAccBooks.Doc_Credit ELSE AllCurrAccBooks.Doc_Debit END = 0 AND 
                         CASE WHEN AllCurrAccBooks.CurrAccTypeCode IN (1) THEN AllCurrAccBooks.Doc_Debit ELSE AllCurrAccBooks.Doc_Credit END <> 0 AND (AllCurrAccBooks.Doc_Debit + AllCurrAccBooks.Doc_Credit) 
                         - ISNULL(AllPayments_Doc.Doc_Payment, 0) > 0 AND NOT EXISTS
                             (SELECT        *
                               FROM            AllCurrAccBooks AS RETURNTable
                               WHERE        RETURNTable.ReturnApplicationID = AllCurrAccBooks.ApplicationID AND RETURNTable.ApplicationCode = AllCurrAccBooks.ApplicationCode)
