-- OrderType Function
-- Sipariş tiplerini getiren fonksiyon

USE [DENEME]
GO
/****** Object:  UserDefinedFunction [dbo].[OrderType]    Script Date: 5/9/2025 2:26:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

		ALTER FUNCTION [dbo].[OrderType] ( @LangCode Char5) 
		RETURNS TABLE 
		
		AS RETURN
		(
			SELECT
				   bsOrderType.OrderTypeCode
				 , bsOrderType.IsBlocked
				 , LangCode	= @LangCode
				 , OrderTypeDescription = RTRIM(LTRIM(ISNULL(OrderTypeDescription, SPACE(0)))) 
				FROM bsOrderType WITH (NOLOCK) 
					LEFT OUTER JOIN bsOrderTypeDesc WITH (NOLOCK) 
						ON	bsOrderTypeDesc.OrderTypeCode = bsOrderType.OrderTypeCode
						AND bsOrderTypeDesc.LangCode = @LangCode
		)
	
