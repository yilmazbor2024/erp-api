-- CurrAccPostalAddress Function
-- Müşteri/Tedarikçi adres bilgilerini getiren fonksiyon

 
/****** Object:  UserDefinedFunction [dbo].[CurrAccPostalAddress]    Script Date: 5/9/2025 2:05:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER FUNCTION [dbo].[CurrAccPostalAddress]( @LangCode Char10 )
RETURNS TABLE

AS RETURN
(
SELECT	  PostalAddressID 
		, CurrAccCode				= prCurrAccPostalAddress.CurrAccCode							
		, CurrAccTypeCode			= prCurrAccPostalAddress.CurrAccTypeCode 
		, SubCurrAccID				= prCurrAccPostalAddress.SubCurrAccID
		, ContactID					= prCurrAccPostalAddress.ContactID
		, DrivingDirections			= prCurrAccPostalAddress.DrivingDirections
		, Address					= prCurrAccPostalAddress.Address
		, ZipCode					= prCurrAccPostalAddress.ZipCode
		, AddressTypeCode			= prCurrAccPostalAddress.AddressTypeCode
		, AddressTypeDescription	= ISNULL((SELECT AddressTypeDescription FROM cdAddressTypeDesc WITH(NOLOCK) WHERE cdAddressTypeDesc.AddressTypeCode = prCurrAccPostalAddress.AddressTypeCode AND cdAddressTypeDesc.LangCode = @LangCode) , SPACE(0))
		, DistrictCode				= prCurrAccPostalAddress.DistrictCode	
		, DistrictDescription		= ISNULL((SELECT DistrictDescription FROM cdDistrictDesc WITH(NOLOCK) WHERE cdDistrictDesc.DistrictCode = prCurrAccPostalAddress.DistrictCode AND cdDistrictDesc.LangCode = @LangCode) , SPACE(0))
		, CityCode					= prCurrAccPostalAddress.CityCode		
		, CityDescription			= ISNULL((SELECT CityDescription FROM cdCityDesc WITH(NOLOCK) WHERE cdCityDesc.CityCode = prCurrAccPostalAddress.CityCode AND cdCityDesc.LangCode = @LangCode) , SPACE(0))
		, StateCode					= prCurrAccPostalAddress.StateCode		
		, StateDescription			= ISNULL((SELECT StateDescription FROM cdStateDesc WITH(NOLOCK) WHERE cdStateDesc.StateCode = prCurrAccPostalAddress.StateCode AND cdStateDesc.LangCode = @LangCode) , SPACE(0))
		, CountryCode				= prCurrAccPostalAddress.CountryCode	
		, CountryDescription		= ISNULL((SELECT CountryDescription FROM cdCountryDesc WITH(NOLOCK) WHERE cdCountryDesc.CountryCode = prCurrAccPostalAddress.CountryCode AND cdCountryDesc.LangCode = @LangCode) , SPACE(0))
		, TaxOfficeCode				= prCurrAccPostalAddress.TaxOfficeCode	
		, TaxOfficeDescription		= ISNULL((SELECT TaxOfficeDescription FROM cdTaxOfficeDesc WITH(NOLOCK) WHERE cdTaxOfficeDesc.TaxOfficeCode = prCurrAccPostalAddress.TaxOfficeCode AND cdTaxOfficeDesc.LangCode = @LangCode) , SPACE(0))
		, TaxNumber					= prCurrAccPostalAddress.TaxNumber

		, SiteName	
		, BuildingName	
		, BuildingNum	
		, FloorNum	
		, DoorNum	
		, QuarterName	
		, Boulevard	
		, Street	
		, Road				
		, LangCode					= @LangCode	
	FROM prCurrAccPostalAddress WITH (NOLOCK)
			
)
