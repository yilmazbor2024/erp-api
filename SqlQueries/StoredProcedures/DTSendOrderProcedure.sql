-- DTSendOrder Stored Procedure
-- Sipariş verilerini veri transferi için hazırlayan stored procedure

 
/****** Object:  StoredProcedure [dbo].[qry_DTSendOrder]    Script Date: 5/9/2025 2:30:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[qry_DTSendOrder] (
		  @Suffix			Char60
		, @LastDayCount		smallint = 0
		, @FilterString		nvarchar(max) = N''
		, @TransferName		Char100
		, @ElementName		Char100
)
AS
BEGIN TRY

	DECLARE @ProcessCode Process, @sql NVARCHAR(MAX)
	DECLARE @StartDate DATETIME, @EndDate DATETIME

	IF @TransferName NOT IN(N'OrderES', N'OrderIP')
		GOTO lblExit;

	SELECT @ProcessCode = RIGHT(@TransferName, 2)
	SELECT @StartDate	= DATEADD(DAY, @LastDayCount * -1, GETDATE())
	SELECT @EndDate		= GETDATE()

	SET @FilterString = REPLACE(@FilterString, N'VendorCode'		, 'CurrAccCode')
	SET @FilterString = REPLACE(@FilterString, N'CustomerCode'		, 'CurrAccCode')
	SET @FilterString = REPLACE(@FilterString, N'EmployeeCode'		, 'CurrAccCode')
	SET @FilterString = REPLACE(@FilterString, N'RetailCustomerCode', 'CurrAccCode')
	SET @FilterString = REPLACE(@FilterString, N'OrderNumberES'		, 'OrderNumber')
	SET @FilterString = REPLACE(@FilterString, N'OrderNumberIP'		, 'OrderNumber')

	SET @sql = N'
	DECLARE @tmpOrder TABLE(OrderHeaderID uniqueidentifier)

	INSERT @tmpOrder
	SELECT DISTINCT trOrderHeader.OrderHeaderID
	FROM   trOrderHeader WITH(NOLOCK)
			LEFT OUTER JOIN OrderATAttributesFilter 
					ON trOrderHeader.OrderHeaderID = OrderATAttributesFilter.OrderHeaderID
	WHERE trOrderHeader.ProcessCode = @ProcessCode 
	AND trOrderHeader.IsCompleted = 1
	AND trOrderHeader.LastUpdatedDate BETWEEN @StartDate AND @EndDate
	{FilterString}

	INSERT dtSendingData(SendingDataID, Suffix, TransferName, CodeType, Code,ID)
	SELECT CAST(CAST(NEWID() AS BINARY(10)) + CAST(GETDATE() AS BINARY(6)) AS UNIQUEIDENTIFIER) ID, @Suffix, @TransferName, NULL, NULL, OrderHeaderID
	FROM @tmpOrder
	'

	SET @sql = REPLACE(@sql, '{FilterString}', @FilterString)

	EXEC sp_ExecuteSQL @sql, N'@Suffix Char60, @TransferName Char30, @StartDate DATETIME, @EndDate DATETIME, @ProcessCode Process', @Suffix, @TransferName, @StartDate, @EndDate, @ProcessCode

	lblExit:

	SELECT   
		  Suffix						= dtSendingData.Suffix
		, OrderHeaderID					= trOrderHeader.OrderHeaderID					
		, OrderTypeCode				   	= trOrderHeader.OrderTypeCode				   	
		, ProcessCode					= trOrderHeader.ProcessCode					
		, OrderNumber					= trOrderHeader.OrderNumber					
		, IsCancelOrder					= trOrderHeader.IsCancelOrder					
		, OrderDate						= trOrderHeader.OrderDate						
		, OrderTime						= trOrderHeader.OrderTime						
		, DocumentNumber				= trOrderHeader.DocumentNumber				
		, PaymentTerm					= trOrderHeader.PaymentTerm					
		, AverageDueDate				= trOrderHeader.AverageDueDate				
		, Description					= trOrderHeader.Description					
		, InternalDescription			= trOrderHeader.InternalDescription			
		, CurrAccTypeCode				= trOrderHeader.CurrAccTypeCode				
		, CurrAccCode					= trOrderHeader.CurrAccCode					
		, SubCurrAccID					= trOrderHeader.SubCurrAccID
		, SubCurrAccCode				= prSubCurrAcc.SubCurrAccCode
		, ContactID						= trOrderHeader.ContactID	
		, ContactTypeCode				= prCurrAccContact.ContactTypeCode		
		, FirstName						= prCurrAccContact.FirstName		
		, LastName						= prCurrAccContact.LastName		
		, IdentityNum					= prCurrAccContact.IdentityNum	
		, ShipmentMethodCode			= trOrderHeader.ShipmentMethodCode			
		, ShippingPostalAddressID		= NULL	
		, BillingPostalAddressID		= NULL	
		, GuarantorContactID			= NULL		
		, GuarantorContactID2			= NULL		
		, RoundsmanCode					= trOrderHeader.RoundsmanCode					
		, DeliveryCompanyCode			= trOrderHeader.DeliveryCompanyCode			
		, TaxTypeCode					= trOrderHeader.TaxTypeCode					
		, WithHoldingTaxTypeCode		= trOrderHeader.WithHoldingTaxTypeCode		
		, DOVCode						= trOrderHeader.DOVCode						
		, TaxExemptionCode				= trOrderHeader.TaxExemptionCode				
		, CompanyCode					= trOrderHeader.CompanyCode					
		, OfficeCode					= trOrderHeader.OfficeCode					
		, StoreTypeCode					= trOrderHeader.StoreTypeCode					
		, StoreCode						= trOrderHeader.StoreCode						
		, POSTerminalID					= trOrderHeader.POSTerminalID					
		, WarehouseCode					= trOrderHeader.WarehouseCode					
		, ToWarehouseCode				= trOrderHeader.ToWarehouseCode				      
		, OrdererCompanyCode			= trOrderHeader.OrdererCompanyCode			
		, OrdererOfficeCode				= trOrderHeader.OrdererOfficeCode				
		, OrdererStoreCode				= trOrderHeader.OrdererStoreCode				
		, GLTypeCode					= trOrderHeader.GLTypeCode					
		, DocCurrencyCode				= trOrderHeader.DocCurrencyCode				
		, LocalCurrencyCode				= trOrderHeader.LocalCurrencyCode				
		, ExchangeRate					= trOrderHeader.ExchangeRate					
		, TDisRate1						= trOrderHeader.TDisRate1						
		, TDisRate2						= trOrderHeader.TDisRate2						
		, TDisRate3						= trOrderHeader.TDisRate3						
		, TDisRate4						= trOrderHeader.TDisRate4						
		, TDisRate5						= trOrderHeader.TDisRate5						
		, DiscountReasonCode			= trOrderHeader.DiscountReasonCode			
		, SurplusOrderQtyToleranceRate	= trOrderHeader.SurplusOrderQtyToleranceRate	
		, ImportFileNumber				= trOrderHeader.ImportFileNumber				
		, ExportFileNumber				= trOrderHeader.ExportFileNumber				
		, IncotermCode1					= trOrderHeader.IncotermCode1					
		, IncotermCode2					= trOrderHeader.IncotermCode2					
		, LettersOfCreditNumber			= trOrderHeader.LettersOfCreditNumber			
		, PaymentMethodCode				= trOrderHeader.PaymentMethodCode				
		, IsInclutedVat					= trOrderHeader.IsInclutedVat					
		, IsCreditSale					= trOrderHeader.IsCreditSale					
		, IsCreditableConfirmed			= trOrderHeader.IsCreditableConfirmed			
		, CreditableConfirmedUser		= trOrderHeader.CreditableConfirmedUser		
		, CreditableConfirmedDate		= trOrderHeader.CreditableConfirmedDate		
		, IsSalesViaInternet			= trOrderHeader.IsSalesViaInternet			
		, IsProposalBased				= trOrderHeader.IsProposalBased				
		, IsSuspended					= trOrderHeader.IsSuspended					
		, IsCompleted					= trOrderHeader.IsCompleted					
		, IsPrinted						= trOrderHeader.IsPrinted						
		, IsLocked						= trOrderHeader.IsLocked						
		, UserLocked					= trOrderHeader.UserLocked					
		, IsClosed						= trOrderHeader.IsClosed						
		, ApplicationCode				= trOrderHeader.ApplicationCode				
		, ApplicationID					= trOrderHeader.ApplicationID
		, InsuranceAgencyCode			= tpOrderHeaderExtension.InsuranceAgencyCode
		, IsInstantReserve   			= tpOrderHeaderExtension.IsInstantReserve
		, OrderCashRegisterInfoID		= tpOrderCashRegisterInfo.OrderCashRegisterInfoID	
		, CashRegisterSerialNumber		= tpOrderCashRegisterInfo.CashRegisterSerialNumber	
		, CashRegisterzNo				= tpOrderCashRegisterInfo.zNo						
		, CashRegisterDocumentNumber	= tpOrderCashRegisterInfo.DocumentNumber			
		, CashRegisterDocumentDate		= tpOrderCashRegisterInfo.DocumentDate				
		, CashRegisterDocumentTime		= tpOrderCashRegisterInfo.DocumentTime				
		, CashRegisterEJNumber			= tpOrderCashRegisterInfo.EJNumber		
		, CashRegisterIsManualRecord	= tpOrderCashRegisterInfo.IsManualRecord	
		--ATAttribute
		, ATAtt01						= ATAtt01
		, ATAtt02						= ATAtt02
		, ATAtt03						= ATAtt03
		, ATAtt04						= ATAtt04
		, ATAtt05						= ATAtt05
		--FTAttribute
		, FTAtt01						= FTAtt01
		, FTAtt02						= FTAtt02
		, FTAtt03						= FTAtt03
		, FTAtt04						= FTAtt04
		, FTAtt05						= FTAtt05
		, IsValidated					= 1
		, MasterKey						= trOrderHeader.OrderNumber	
	FROM   dtSendingData AS dtSendingData WITH(NOLOCK)
		INNER JOIN trOrderHeader WITH(NOLOCK)
				ON dtSendingData.ID = trOrderHeader.OrderHeaderID
		LEFT OUTER JOIN tpOrderHeaderExtension WITH(NOLOCK)
				ON trOrderHeader.OrderHeaderID = tpOrderHeaderExtension.OrderHeaderID   
		LEFT OUTER JOIN tpOrderCashRegisterInfo WITH(NOLOCK)
				ON trOrderHeader.OrderHeaderID = tpOrderCashRegisterInfo.OrderHeaderID
		LEFT OUTER JOIN prSubCurrAcc WITH(NOLOCK)
				ON trOrderHeader.SubCurrAccID = prSubCurrAcc.SubCurrAccID
		LEFT OUTER JOIN prCurrAccContact WITH(NOLOCK)
				ON trOrderHeader.ContactID = prCurrAccContact.ContactID
		LEFT OUTER JOIN OrderATAttributesFilter 
				ON trOrderHeader.OrderHeaderID = OrderATAttributesFilter.OrderHeaderID
		LEFT OUTER JOIN OrderFTAttributesFilter 
				ON trOrderHeader.OrderHeaderID = OrderFTAttributesFilter.OrderHeaderID
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
