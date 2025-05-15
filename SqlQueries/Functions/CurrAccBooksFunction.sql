-- CurrAccBooks Function
-- Müşteri/Tedarikçi borç alacak hareketlerini getiren fonksiyon

USE [DENEME]
GO
/****** Object:  UserDefinedFunction [dbo].[CurrAccBooks]    Script Date: 5/9/2025 2:01:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER FUNCTION [dbo].[CurrAccBooks](@LangCode Char5)
RETURNS TABLE

AS RETURN
(
	SELECT wvBook.* 
		 , Type = COALESCE(DebitTypeDescription , BankTransTypeDescription , CashTransTypeDescription , CreditCardPaymentTypeDescription , GiftCardPaymentTypeDescription , ChequeTransTypeDescription , SPACE(0))
		 , ChequeAttribute01		= ChequeAttributes.ChequeAtt01			
		 , ChequeAttribute01Desc	= ChequeAttributes.ChequeAtt01Desc		
		 , ChequeAttribute02		= ChequeAttributes.ChequeAtt02			
		 , ChequeAttribute02Desc	= ChequeAttributes.ChequeAtt02Desc	         
	FROM AllCurrAccBooksWithAttributes AS wvBook 
		LEFT OUTER JOIN (SELECT ChequeLineID
							 , ChequeAtt01		= ISNULL(prChequeAttribute_1.AttributeCode, SPACE(0)) 
							 , ChequeAtt01Desc	= ISNULL((SELECT AttributeDescription FROM cdChequeAttributeDesc WITH(NOLOCK) WHERE cdChequeAttributeDesc.ChequeTypeCode = prChequeAttribute_1.ChequeTypeCode AND cdChequeAttributeDesc.AttributeTypeCode = prChequeAttribute_1.AttributeTypeCode AND cdChequeAttributeDesc.AttributeCode = prChequeAttribute_1.AttributeCode AND cdChequeAttributeDesc.LangCode = @LangCode), SPACE(0))
							 , ChequeAtt02		= ISNULL(prChequeAttribute_2.AttributeCode, SPACE(0)) 
							 , ChequeAtt02Desc	= ISNULL((SELECT AttributeDescription FROM cdChequeAttributeDesc WITH(NOLOCK) WHERE cdChequeAttributeDesc.ChequeTypeCode = prChequeAttribute_2.ChequeTypeCode AND cdChequeAttributeDesc.AttributeTypeCode = prChequeAttribute_2.AttributeTypeCode AND cdChequeAttributeDesc.AttributeCode = prChequeAttribute_2.AttributeCode AND cdChequeAttributeDesc.LangCode = @LangCode), SPACE(0))
							FROM trChequeLine WITH(NOLOCK)
							LEFT OUTER JOIN prChequeAttribute AS prChequeAttribute_1 WITH(NOLOCK) ON prChequeAttribute_1.ChequeTypeCode = trChequeLine.ChequeTypeCode AND prChequeAttribute_1.ChequeCode = trChequeLine.ChequeCode AND prChequeAttribute_1.BankCode = trChequeLine.BankCode AND prChequeAttribute_1.BankBranchCode = trChequeLine.BankBranchCode AND prChequeAttribute_1.AttributeTypeCode = 1
							LEFT OUTER JOIN prChequeAttribute AS prChequeAttribute_2 WITH(NOLOCK) ON prChequeAttribute_2.ChequeTypeCode = trChequeLine.ChequeTypeCode AND prChequeAttribute_2.ChequeCode = trChequeLine.ChequeCode AND prChequeAttribute_2.BankCode = trChequeLine.BankCode AND prChequeAttribute_2.BankBranchCode = trChequeLine.BankBranchCode AND prChequeAttribute_2.AttributeTypeCode = 2
			) AS  ChequeAttributes
			ON ChequeAttributes.ChequeLineID = wvBook.ApplicationID AND wvBook.ApplicationCode = N'Chequ'		
		LEFT OUTER JOIN bsDebitTypeDesc				WITH(NOLOCK) ON 	wvBook.DebitTypeCode <> 0 AND bsDebitTypeDesc.DebitTypeCode = wvBook.DebitTypeCode AND bsDebitTypeDesc.LangCode = @LangCode
		LEFT OUTER JOIN bsBankTransTypeDesc			WITH(NOLOCK) ON 	wvBook.BankTransTypeCode <> 0 AND bsBankTransTypeDesc.BankTransTypeCode = wvBook.BankTransTypeCode AND bsBankTransTypeDesc.LangCode = @LangCode
		LEFT OUTER JOIN bsCashTransTypeDesc			WITH(NOLOCK) ON 	wvBook.CashTransTypeCode <> 0 AND bsCashTransTypeDesc.CashTransTypeCode = wvBook.CashTransTypeCode AND bsCashTransTypeDesc.LangCode = @LangCode
		LEFT OUTER JOIN bsCreditCardPaymentTypeDesc WITH(NOLOCK) ON 	wvBook.CreditCardPaymentTypeCode <> 0 AND bsCreditCardPaymentTypeDesc.CreditCardPaymentTypeCode = wvBook.CreditCardPaymentTypeCode AND bsCreditCardPaymentTypeDesc.LangCode = @LangCode
		LEFT OUTER JOIN bsGiftCardPaymentTypeDesc	WITH(NOLOCK) ON 	wvBook.GiftCardPaymentTypeCode <> 0 AND bsGiftCardPaymentTypeDesc.GiftCardPaymentTypeCode = wvBook.GiftCardPaymentTypeCode AND bsGiftCardPaymentTypeDesc.LangCode = @LangCode
		LEFT OUTER JOIN bsChequeTransTypeDesc		WITH(NOLOCK) ON 	wvBook.ChequeTransTypeCode <> 0 AND bsChequeTransTypeDesc.ChequeTransTypeCode = wvBook.ChequeTransTypeCode AND bsChequeTransTypeDesc.LangCode = @LangCode
)
