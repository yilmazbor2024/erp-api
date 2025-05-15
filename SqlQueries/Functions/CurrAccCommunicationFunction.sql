-- CurrAccCommunication Function
-- Müşteri/Tedarikçi iletişim bilgilerini getiren fonksiyon

USE [DENEME]
GO
/****** Object:  UserDefinedFunction [dbo].[CurrAccCommunication]    Script Date: 5/9/2025 2:03:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER FUNCTION [dbo].[CurrAccCommunication]( @LangCode Char10 )
RETURNS TABLE

AS RETURN
(
SELECT    CommunicationID
		, CurrAccCode					= prCurrAccCommunication.CurrAccCode 
		, CurrAccTypeCode				= prCurrAccCommunication.CurrAccTypeCode
		, SubCurrAccID					= prCurrAccCommunication.SubCurrAccID
		, ContactID						= prCurrAccCommunication.ContactID	
		, CommunicationTypeCode			= prCurrAccCommunication.CommunicationTypeCode
		, CommunicationTypeDescription	= ISNULL((SELECT CommunicationTypeDescription FROM cdCommunicationTypeDesc WITH(NOLOCK) WHERE cdCommunicationTypeDesc.CommunicationTypeCode = prCurrAccCommunication.CommunicationTypeCode AND cdCommunicationTypeDesc.LangCode = @LangCode) , SPACE(0))
		, SubCurrAccCode				= ISNULL(prSubCurrAcc.SubCurrAccCode , SPACE(0)) 
		, CompanyName					= ISNULL(prSubCurrAcc.CompanyName , SPACE(0))
		, ContactTypeCode				= ISNULL(ContactTypeCode , SPACE(0)) 
		, ContactFirstName				= ISNULL(prCurrAccContact.FirstName , SPACE(0)) 
		, ContactLastName				= ISNULL(prCurrAccContact.LastName , SPACE(0))
		, CommAddress					= prCurrAccCommunication.CommAddress
		, LangCode						= @LangCode
	FROM  prCurrAccCommunication WITH (NOLOCK)
		LEFT OUTER JOIN prSubCurrAcc		WITH (NOLOCK) ON prSubCurrAcc.SubCurrAccID = prCurrAccCommunication.SubCurrAccID
		LEFT OUTER JOIN prCurrAccContact	WITH (NOLOCK) ON prCurrAccContact.ContactID = prCurrAccCommunication.ContactID		
)
