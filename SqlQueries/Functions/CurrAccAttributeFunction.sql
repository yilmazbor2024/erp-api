-- CurrAccAttribute Function
-- Müşteri/Tedarikçi özelliklerini getiren fonksiyon

 

/****** Object:  UserDefinedFunction [dbo].[CurrAccAttribute]    Script Date: 5/9/2025 1:54:43 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


		CREATE FUNCTION [dbo].[CurrAccAttribute] ( @LangCode Char5) 
		RETURNS TABLE 
		
		AS RETURN
		(
			SELECT
				   cdCurrAccAttribute.CurrAccTypeCode
				 , cdCurrAccAttribute.AttributeTypeCode
				 , AttributeCode = RTRIM(LTRIM(cdCurrAccAttribute.AttributeCode))
				 , Description = RTRIM(LTRIM(cdCurrAccAttribute.Description))
				 , cdCurrAccAttribute.IsBlocked
				 , LangCode	= @LangCode
				 , AttributeDescription = RTRIM(LTRIM(ISNULL(AttributeDescription, SPACE(0)))) 
				FROM cdCurrAccAttribute WITH (NOLOCK) 
					LEFT OUTER JOIN cdCurrAccAttributeDesc WITH (NOLOCK) 
						ON	cdCurrAccAttributeDesc.CurrAccTypeCode = cdCurrAccAttribute.CurrAccTypeCode
						AND cdCurrAccAttributeDesc.AttributeTypeCode = cdCurrAccAttribute.AttributeTypeCode
						AND cdCurrAccAttributeDesc.AttributeCode = cdCurrAccAttribute.AttributeCode
						AND cdCurrAccAttributeDesc.LangCode = @LangCode
		)
	
GO

EXEC sys.sp_addextendedproperty @name=N'NotCustomizable', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'FUNCTION',@level1name=N'CurrAccAttribute'
GO
