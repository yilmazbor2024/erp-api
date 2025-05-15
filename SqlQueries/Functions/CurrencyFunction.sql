-- Currency Function
-- Para birimi bilgilerini getiren fonksiyon

USE [DENEME]
GO
/****** Object:  UserDefinedFunction [dbo].[Currency]    Script Date: 5/9/2025 2:06:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

		ALTER FUNCTION [dbo].[Currency] ( @LangCode Char5) 
		RETURNS TABLE 
		
		AS RETURN
		(
			SELECT
				   CurrencyCode = RTRIM(LTRIM(cdCurrency.CurrencyCode))
				 , cdCurrency.IsBlocked
				 , LangCode	= @LangCode
				 , CurrencyDescription = RTRIM(LTRIM(ISNULL(CurrencyDescription, SPACE(0)))) 
				FROM cdCurrency WITH (NOLOCK) 
					LEFT OUTER JOIN cdCurrencyDesc WITH (NOLOCK) 
						ON	cdCurrencyDesc.CurrencyCode = cdCurrency.CurrencyCode
						AND cdCurrencyDesc.LangCode = @LangCode
		)
