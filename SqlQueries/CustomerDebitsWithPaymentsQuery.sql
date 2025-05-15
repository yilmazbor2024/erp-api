-- CurrAccDebitsWithPayments
-- Müşteri ve tedarikçilerin borçlarını ve bu borçlara yapılan ödemeleri getiren sorgu

SELECT        CurrAccCode = AllPayments.CurrAccCode, CurrAccTypeCode = AllPayments.CurrAccTypeCode, SortOrder = ROW_NUMBER() OVER (PARTITION BY AllPayments.DebitLineID
ORDER BY COALESCE (AllCashs.DocumentDate, AllBanks.DocumentDate, AllCreditCardPayments.PaymentDate, AllGiftCardPayments.PaymentDate, AllOtherPayments.PaymentDate, AllCheques.DocumentDate, 
                         ReverseDebit.DocumentDate, SPACE(0))), AllDebits.DebitLineID, PaymentNumber = AllPayments.PaymentNumber, PaymentTypeCode = AllPayments.PaymentTypeCode, 
PaymentRefNumber = COALESCE (AllCashs.CashTransNumber, AllBanks.BankTransNumber, AllCreditCardPayments.CreditCardPaymentNumber, AllGiftCardPayments.GiftCardPaymentNumber, AllOtherPayments.OtherPaymentNumber, 
AllCheques.ChequeTransNumber, ReverseDebit.DebitNumber, SPACE(0)), PaymentDocumentNumber = COALESCE (AllCashs.DocumentNumber, AllBanks.DocumentNumber, AllCreditCardPayments.DocumentNumber, 
AllGiftCardPayments.DocumentNumber, AllOtherPayments.DocumentNumber, AllCheques.DocumentNumber, ReverseDebit.DocumentNumber, SPACE(0)), PaymentDueDate = COALESCE (AllBanks.DueDate, cdCheque.DueDate, 
ReverseDebit.DueDate, AllCashs.DocumentDate, SPACE(0)), PaymentDate = COALESCE (AllCashs.DocumentDate, AllBanks.DocumentDate, AllCreditCardPayments.PaymentDate, AllGiftCardPayments.PaymentDate, 
AllOtherPayments.PaymentDate, AllCheques.DocumentDate, ReverseDebit.DocumentDate, SPACE(0)), Loc_CurrencyCode = AllPayments.Loc_CurrencyCode, Loc_Payment = AllPayments.Loc_Payment, 
Doc_CurrencyCode = AllPayments.Doc_CurrencyCode, Doc_Payment = AllPayments.Doc_Payment, RefNumber = AllDebits.RefNumber, DocumentDate = AllDebits.DocumentDate, DocumentNumber = AllDebits.DocumentNumber, 
DueDate = AllDebits.DueDate, Loc_Debit = AllDebits.Loc_Debit, Doc_Debit = AllDebits.Doc_Debit, ApplicationCode = AllDebits.ApplicationCode, CompanyCode = AllPayments.CompanyCode, OfficeCode = AllPayments.OfficeCode, 
StoreCode = AllPayments.StoreCode, ATAtt01 = AllDebits.ATAtt01, ATAtt02 = AllDebits.ATAtt02, ATAtt03 = AllDebits.ATAtt03, ATAtt04 = AllDebits.ATAtt04, ATAtt05 = AllDebits.ATAtt05
FROM            AllPaymentsWithAttributes AllPayments INNER JOIN
                         AllDebitsWithAttributes AllDebits ON AllDebits.DebitLineID = AllPayments.DebitLineID AND AllDebits.CurrAccTypeCode = AllPayments.CurrAccTypeCode AND AllDebits.CurrAccCode = AllPayments.CurrAccCode LEFT OUTER JOIN
                         AllCashs ON AllCashs.CashLineID = AllPayments.CashLineID AND AllCashs.CurrAccTypeCode = AllPayments.CurrAccTypeCode AND AllCashs.CurrAccCode = AllPayments.CurrAccCode LEFT OUTER JOIN
                         AllBanks ON AllBanks.BankLineID = AllPayments.BankLineID AND AllBanks.CurrAccTypeCode = AllPayments.CurrAccTypeCode AND AllBanks.CurrAccCode = AllPayments.CurrAccCode LEFT OUTER JOIN
                         AllCreditCardPayments ON AllCreditCardPayments.CreditCardPaymentLineID = AllPayments.CreditCardPaymentLineID AND AllCreditCardPayments.CurrAccTypeCode = AllPayments.CurrAccTypeCode AND 
                         AllCreditCardPayments.CurrAccCode = AllPayments.CurrAccCode LEFT OUTER JOIN
                         AllGiftCardPayments ON AllGiftCardPayments.GiftCardPaymentLineID = AllPayments.GiftCardPaymentLineID AND AllGiftCardPayments.CurrAccTypeCode = AllPayments.CurrAccTypeCode AND 
                         AllGiftCardPayments.CurrAccCode = AllPayments.CurrAccCode LEFT OUTER JOIN
                         AllOtherPayments ON AllOtherPayments.OtherPaymentLineID = AllPayments.OtherPaymentLineID AND AllOtherPayments.CurrAccTypeCode = AllPayments.CurrAccTypeCode AND 
                         AllOtherPayments.CurrAccCode = AllPayments.CurrAccCode LEFT OUTER JOIN
                         AllCheques ON AllCheques.ChequeLineID = AllPayments.ChequeLineID AND AllCheques.CurrAccTypeCode = AllPayments.CurrAccTypeCode AND AllCheques.CurrAccCode = AllPayments.CurrAccCode LEFT OUTER JOIN
                         cdCheque WITH (NOLOCK) ON cdCheque.ChequeTypeCode = AllCheques.ChequeTypeCode AND cdCheque.ChequeCode = AllCheques.ChequeCode AND cdCheque.BankCode = AllCheques.BankCode AND 
                         cdCheque.BankBranchCode = AllCheques.BankBranchCode LEFT OUTER JOIN
                         AllDebits AS ReverseDebit ON ReverseDebit.DebitLineID = AllPayments.ReverseDebitLineID AND ReverseDebit.CurrAccTypeCode = AllPayments.CurrAccTypeCode AND 
                         ReverseDebit.CurrAccCode = AllPayments.CurrAccCode
WHERE        AllPayments.CurrAccTypeCode = 3 AND Loc_Payment <> 0
