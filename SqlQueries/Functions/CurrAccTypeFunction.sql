-- CurrAccType Function
-- Müşteri/Tedarikçi tiplerini getiren fonksiyon

 
/****** Object:  UserDefinedFunction [dbo].[CurrAccType]    Script Date: 5/9/2025 2:05:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

		ALTER FUNCTION [dbo].[CurrAccType] ( @LangCode Char5) 
		RETURNS TABLE 
		
		AS RETURN
		(
			SELECT
				   bsCurrAccType.CurrAccTypeCode
				 , bsCurrAccType.IsBlocked
				 , LangCode	= @LangCode
				 , CurrAccTypeDescription = RTRIM(LTRIM(ISNULL(CurrAccTypeDescription, SPACE(0)))) 
				FROM bsCurrAccType WITH (NOLOCK) 
					LEFT OUTER JOIN bsCurrAccTypeDesc WITH (NOLOCK) 
						ON	bsCurrAccTypeDesc.CurrAccTypeCode = bsCurrAccType.CurrAccTypeCode
						AND bsCurrAccTypeDesc.LangCode = @LangCode
		)
