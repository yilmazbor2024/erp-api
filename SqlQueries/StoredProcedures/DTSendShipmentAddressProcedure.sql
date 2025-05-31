-- DTSendShipmentAddress Stored Procedure
-- Sevkiyat adres bilgilerini veri transferi için hazırlayan stored procedure
 
/****** Object:  StoredProcedure [dbo].[qry_DTSendShipmentAddress]    Script Date: 5/9/2025 2:20:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[qry_DTSendShipmentAddress] (
		  @Suffix			Char60
		, @TransferName		Char100
)
AS
BEGIN TRY

	SELECT 
		  Suffix			= dtSendingData.Suffix
		----------------------------------------------------------------
		, ProcessCode		= trShipmentHeader.ProcessCode
		, ShippingNumber	= trShipmentHeader.ShippingNumber
		, RecordType		= N'BillingPostalAddress'
		, PostalAddressID	= trShipmentHeader.BillingPostalAddressID
		, CurrAccTypeCode	= prCurrAccPostalAddress.CurrAccTypeCode
		, CurrAccCode		= prCurrAccPostalAddress.CurrAccCode
		, SubCurrAccCode	= prSubCurrAcc.SubCurrAccCode
		, ContactID			= prCurrAccPostalAddress.ContactID				
		, ContactTypeCode	= prCurrAccContact.ContactTypeCode		
		, FirstName			= prCurrAccContact.FirstName		
		, LastName			= prCurrAccContact.LastName		
		, IdentityNum		= prCurrAccContact.IdentityNum	
		, AddressTypeCode	= prCurrAccPostalAddress.AddressTypeCode    
		, AddressID 		= prCurrAccPostalAddress.AddressID		
		, Address			= prCurrAccPostalAddress.Address	
		, SiteName			= prCurrAccPostalAddress.SiteName	
		, BuildingName		= prCurrAccPostalAddress.BuildingName	
		, BuildingNum		= prCurrAccPostalAddress.BuildingNum	
		, FloorNum			= prCurrAccPostalAddress.FloorNum		
		, DoorNum			= prCurrAccPostalAddress.DoorNum			
		, QuarterCode 		= prCurrAccPostalAddress.QuarterCode	
		, QuarterName 		= prCurrAccPostalAddress.QuarterName	
		, Boulevard			= prCurrAccPostalAddress.Boulevard	
		, StreetCode 		= prCurrAccPostalAddress.StreetCode	
		, Street			= prCurrAccPostalAddress.Street		
		, Road				= prCurrAccPostalAddress.Road			
		, CountryCode		= prCurrAccPostalAddress.CountryCode	
		, StateCode			= prCurrAccPostalAddress.StateCode	
		, CityCode			= prCurrAccPostalAddress.CityCode		
		, DistrictCode		= prCurrAccPostalAddress.DistrictCode	
		, ZipCode			= prCurrAccPostalAddress.ZipCode		
		, DrivingDirections	= prCurrAccPostalAddress.DrivingDirections	
		, TaxOfficeCode		= prCurrAccPostalAddress.TaxOfficeCode
		, TaxNumber			= prCurrAccPostalAddress.TaxNumber	
		----------------------------------------------------------------
		, InvoiceHeaderID	= trShipmentheader.ShipmentHeaderID
		----------------------------------------------------------------
		, IsValidated		= 1
		, MasterKey			= trShipmentheader.ShipmentHeaderID	
	FROM dtSendingData WITH(NOLOCK)
		INNER JOIN trShipmentHeader WITH(NOLOCK)
			ON trShipmentHeader.ShipmentHeaderID = dtSendingData.ID  
		INNER JOIN prCurrAccPostalAddress WITH(NOLOCK)
			ON prCurrAccPostalAddress.PostalAddressID = trShipmentHeader.BillingPostalAddressID  
		LEFT OUTER JOIN prCurrAccContact WITH(NOLOCK)
			ON prCurrAccContact.ContactID = prCurrAccPostalAddress.ContactID  
		LEFT OUTER JOIN prSubCurrAcc WITH(NOLOCK)
			ON prSubCurrAcc.SubCurrAccID = prCurrAccPostalAddress.SubCurrAccID  
	WHERE dtSendingData.Suffix  = @Suffix
	AND	dtSendingData.TransferName = @TransferName
	UNION ALL
	SELECT 
		  Suffix			= dtSendingData.Suffix
		----------------------------------------------------------------
		, ProcessCode		= trShipmentHeader.ProcessCode
		, ShippingNumber	= trShipmentHeader.ShippingNumber
		, RecordType		= N'ShippingPostalAddress'
		, PostalAddressID	= trShipmentHeader.ShippingPostalAddressID
		, CurrAccTypeCode	= prCurrAccPostalAddress.CurrAccTypeCode
		, CurrAccCode		= prCurrAccPostalAddress.CurrAccCode
		, SubCurrAccCode	= prSubCurrAcc.SubCurrAccCode
		, ContactID			= prCurrAccContact.ContactID				
		, ContactTypeCode	= prCurrAccContact.ContactTypeCode		
		, FirstName			= prCurrAccContact.FirstName		
		, LastName			= prCurrAccContact.LastName		
		, IdentityNum		= prCurrAccContact.IdentityNum	
		, AddressTypeCode	= prCurrAccPostalAddress.AddressTypeCode    
		, AddressID 		= prCurrAccPostalAddress.AddressID		
		, Address			= prCurrAccPostalAddress.Address	
		, SiteName			= prCurrAccPostalAddress.SiteName	
		, BuildingName		= prCurrAccPostalAddress.BuildingName	
		, BuildingNum		= prCurrAccPostalAddress.BuildingNum	
		, FloorNum			= prCurrAccPostalAddress.FloorNum		
		, DoorNum			= prCurrAccPostalAddress.DoorNum			
		, QuarterCode 		= prCurrAccPostalAddress.QuarterCode	
		, QuarterName 		= prCurrAccPostalAddress.QuarterName	
		, Boulevard			= prCurrAccPostalAddress.Boulevard	
		, StreetCode 		= prCurrAccPostalAddress.StreetCode	
		, Street			= prCurrAccPostalAddress.Street		
		, Road				= prCurrAccPostalAddress.Road			
		, CountryCode		= prCurrAccPostalAddress.CountryCode	
		, StateCode			= prCurrAccPostalAddress.StateCode	
		, CityCode			= prCurrAccPostalAddress.CityCode		
		, DistrictCode		= prCurrAccPostalAddress.DistrictCode	
		, ZipCode			= prCurrAccPostalAddress.ZipCode		
		, DrivingDirections	= prCurrAccPostalAddress.DrivingDirections	
		, TaxOfficeCode		= prCurrAccPostalAddress.TaxOfficeCode
		, TaxNumber			= prCurrAccPostalAddress.TaxNumber
		----------------------------------------------------------------
		, InvoiceHeaderID	= trShipmentheader.ShipmentHeaderID	
		----------------------------------------------------------------
		, IsValidated		= 1
		, MasterKey			= trShipmentheader.ShipmentHeaderID	
	FROM dtSendingData WITH(NOLOCK)
		INNER JOIN trShipmentHeader WITH(NOLOCK)
			ON trShipmentHeader.ShipmentHeaderID = dtSendingData.ID  
		INNER JOIN prCurrAccPostalAddress WITH(NOLOCK)
			ON prCurrAccPostalAddress.PostalAddressID = trShipmentHeader.ShippingPostalAddressID  
		LEFT OUTER JOIN prCurrAccContact WITH(NOLOCK)
			ON prCurrAccContact.ContactID = prCurrAccPostalAddress.ContactID  
		LEFT OUTER JOIN prSubCurrAcc WITH(NOLOCK)
			ON prSubCurrAcc.SubCurrAccID = prCurrAccPostalAddress.SubCurrAccID  
	WHERE dtSendingData.Suffix  = @Suffix
	AND	dtSendingData.TransferName = @TransferName

END TRY

BEGIN CATCH

	IF (XACT_STATE()) <> 0 	ROLLBACK TRANSACTION;
	DECLARE @ErrorMessage	NVARCHAR(4000)
	DECLARE @ErrorSeverity	INT
	DECLARE @ErrorState		INT

	SELECT  @ErrorMessage	= ERROR_MESSAGE(),
			@ErrorSeverity	= ERROR_SEVERITY(),
			@ErrorState		= ERROR_STATE()
			
	RAISERROR (@ErrorMessage,	-- Message text
				@ErrorSeverity,	-- Severity
				@ErrorState		-- State  
				) 

END CATCH
