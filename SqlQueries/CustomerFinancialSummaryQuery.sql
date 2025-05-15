-- Müşteri ve tedarikçilerin finansal özet bilgilerini getiren sorgu
-- Bu sorgu, trCurrAccBook tablosundan toplam borç, alacak ve bakiye bilgilerini getirir

SELECT 
    cab.CurrAccCode,
    SUM(ISNULL(cabcLoc.Debit, 0)) AS TotalDebit,
    SUM(ISNULL(cabcLoc.Credit, 0)) AS TotalCredit,
    SUM(ISNULL(cabcLoc.Debit, 0) - ISNULL(cabcLoc.Credit, 0)) AS Balance
FROM trCurrAccBook cab WITH(NOLOCK)
INNER JOIN dfGlobalDefault gd WITH(NOLOCK) ON gd.GlobalDefaultCode = 1
LEFT OUTER JOIN trCurrAccBookCurrency cabcDoc WITH(NOLOCK) ON cabcDoc.CurrAccBookID = cab.CurrAccBookID AND cab.DocCurrencyCode = cabcDoc.CurrencyCode
LEFT OUTER JOIN trCurrAccBookCurrency cabcLoc WITH(NOLOCK) ON cabcLoc.CurrAccBookID = cab.CurrAccBookID AND cab.LocalCurrencyCode = cabcLoc.CurrencyCode
LEFT OUTER JOIN trCurrAccBookCurrency cabcCom WITH(NOLOCK) ON cabcCom.CurrAccBookID = cab.CurrAccBookID AND gd.CompanyCurrencyCode = cabcCom.CurrencyCode
WHERE cab.CurrAccCode = @CustomerCode
GROUP BY cab.CurrAccCode
