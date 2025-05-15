-- CurrAccContact Function
-- Müşteri/Tedarikçi kontak bilgilerini getiren fonksiyon

USE [DENEME]
GO
/****** Object:  UserDefinedFunction [dbo].[CurrAccContact]    Script Date: 5/9/2025 2:03:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER FUNCTION [dbo].[CurrAccContact]( @LangCode Char10 )
RETURNS TABLE

AS RETURN
(
SELECT    ContactID
		, CurrAccCode				= prCurrAccContact.CurrAccCode 
		, CurrAccTypeCode			= prCurrAccContact.CurrAccTypeCode
		, SubCurrAccID				= prCurrAccContact.SubCurrAccID				
		, ContactTypeCode			= prCurrAccContact.ContactTypeCode
		, ContactTypeDescription	= ISNULL((SELECT ContactTypeDescription FROM cdContactTypeDesc WITH(NOLOCK) WHERE  cdContactTypeDesc.ContactTypeCode = prCurrAccContact.ContactTypeCode AND cdContactTypeDesc.LangCode = @LangCode	),SPACE(0))
		, SubCurrAccCode			= ISNULL(prSubCurrAcc.SubCurrAccCode , SPACE(0)) , CompanyName = ISNULL(prSubCurrAcc.CompanyName , SPACE(0))
		, TitleCode					= prCurrAccContact.TitleCode 
		, TitleDescription			= ISNULL((SELECT TitleDescription FROM cdTitleDesc WITH(NOLOCK) WHERE  cdTitleDesc.TitleCode = prCurrAccContact.TitleCode AND cdTitleDesc.LangCode = @LangCode	),SPACE(0))
		, JobTitleCode				= prCurrAccContact.JobTitleCode 
		, JobTitleDescription		= ISNULL((SELECT JobTitleDescription FROM cdJobTitleDesc WITH(NOLOCK) WHERE  cdJobTitleDesc.JobTitleCode = prCurrAccContact.JobTitleCode AND cdJobTitleDesc.LangCode = @LangCode	),SPACE(0))
		, FirstName , LastName , FirstLastName, IsAuthorized , prCurrAccContact.IdentityNum 
		, LangCode = @LangCode
	FROM  prCurrAccContact WITH (NOLOCK)
			LEFT OUTER JOIN dbo.prSubCurrAcc WITH (NOLOCK) ON prSubCurrAcc.SubCurrAccID = prCurrAccContact.SubCurrAccID
		
)
