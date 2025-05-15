-- CurrAcc Function
-- Müşteri/Tedarikçi bilgilerini getiren fonksiyon

USE [DENEME]
GO

/****** Object:  UserDefinedFunction [dbo].[CurrAcc]    Script Date: 5/9/2025 1:53:41 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


		CREATE FUNCTION [dbo].[CurrAcc] ( @LangCode Char5) 
		RETURNS TABLE 
		
		AS RETURN
		(
			SELECT
				   cdCurrAcc.CurrAccTypeCode
				 , CurrAccCode = RTRIM(LTRIM(cdCurrAcc.CurrAccCode))
				 , cdCurrAcc.CompanyCode
				 , OfficeCode = RTRIM(LTRIM(cdCurrAcc.OfficeCode))
				 , TitleCode = RTRIM(LTRIM(cdCurrAcc.TitleCode))
				 , FirstName = RTRIM(LTRIM(cdCurrAcc.FirstName))
				 , Patronym = RTRIM(LTRIM(cdCurrAcc.Patronym))
				 , LastName = RTRIM(LTRIM(cdCurrAcc.LastName))
				 , FirstLastName = RTRIM(LTRIM(cdCurrAcc.FirstLastName))
				 , FullName = RTRIM(LTRIM(cdCurrAcc.FullName))
				 , cdCurrAcc.IsIndividualAcc
				 , TaxOfficeCode = RTRIM(LTRIM(cdCurrAcc.TaxOfficeCode))
				 , TaxNumber = RTRIM(LTRIM(cdCurrAcc.TaxNumber))
				 , IdentityNum = RTRIM(LTRIM(cdCurrAcc.IdentityNum))
				 , MersisNum = RTRIM(LTRIM(cdCurrAcc.MersisNum))
				 , cdCurrAcc.IsSubjectToEInvoice
				 , cdCurrAcc.IsSubjectToEShipment
				 , cdCurrAcc.IsArrangeCommercialInvoice
				 , cdCurrAcc.AgreementDate
				 , cdCurrAcc.PaymentTerm
				 , DueDateFormulaCode = RTRIM(LTRIM(cdCurrAcc.DueDateFormulaCode))
				 , cdCurrAcc.UseManufacturing
				 , cdCurrAcc.PermitCreditBalance
				 , DataLanguageCode = RTRIM(LTRIM(cdCurrAcc.DataLanguageCode))
				 , CurrencyCode = RTRIM(LTRIM(cdCurrAcc.CurrencyCode))
				 , cdCurrAcc.CreditLimit
				 , cdCurrAcc.ExchangeTypeCode
				 , cdCurrAcc.AllowOnlySelectedCurrency
				 , cdCurrAcc.IsVIP
				 , cdCurrAcc.IsSendAdvertSMS
				 , cdCurrAcc.IsSendAdvertMail
				 , cdCurrAcc.CustomerTypeCode
				 , cdCurrAcc.VendorTypeCode
				 , cdCurrAcc.StoreHierarchyID
				 , CustomerDiscountGrCode = RTRIM(LTRIM(cdCurrAcc.CustomerDiscountGrCode))
				 , CustomerMarkupGrCode = RTRIM(LTRIM(cdCurrAcc.CustomerMarkupGrCode))
				 , CurrAccLotGrCode = RTRIM(LTRIM(cdCurrAcc.CurrAccLotGrCode))
				 , CustomerPaymentPlanGrCode = RTRIM(LTRIM(cdCurrAcc.CustomerPaymentPlanGrCode))
				 , RetailSalePriceGroupCode = RTRIM(LTRIM(cdCurrAcc.RetailSalePriceGroupCode))
				 , WholesalePriceGroupCode = RTRIM(LTRIM(cdCurrAcc.WholesalePriceGroupCode))
				 , PromotionGroupCode = RTRIM(LTRIM(cdCurrAcc.PromotionGroupCode))
				 , SalesChannelCode = RTRIM(LTRIM(cdCurrAcc.SalesChannelCode))
				 , BarcodeTypeCode = RTRIM(LTRIM(cdCurrAcc.BarcodeTypeCode))
				 , CostCenterCode = RTRIM(LTRIM(cdCurrAcc.CostCenterCode))
				 , GLTypeCode = RTRIM(LTRIM(cdCurrAcc.GLTypeCode))
				 , cdCurrAcc.CustomerASNNumberIsRequiredForShipments
				 , BankCode = RTRIM(LTRIM(cdCurrAcc.BankCode))
				 , BankBranchCode = RTRIM(LTRIM(cdCurrAcc.BankBranchCode))
				 , cdCurrAcc.BankAccTypeCode
				 , IBAN = RTRIM(LTRIM(cdCurrAcc.IBAN))
				 , SWIFTCode = RTRIM(LTRIM(cdCurrAcc.SWIFTCode))
				 , BankAccNo = RTRIM(LTRIM(cdCurrAcc.BankAccNo))
				 , cdCurrAcc.MinBalance
				 , cdCurrAcc.UseBankAccOnStore
				 , cdCurrAcc.AccountOpeningDate
				 , cdCurrAcc.AccountClosingDate
				 , cdCurrAcc.EInvoiceStartDate
				 , cdCurrAcc.EShipmentStartDate
				 , cdCurrAcc.EInvoiceConfirmationRuleID
				 , cdCurrAcc.PurchaseRequisitionRequired
				 , cdCurrAcc.UseDBSIntegration
				 , DBSAccountCode = RTRIM(LTRIM(cdCurrAcc.DBSAccountCode))
				 , cdCurrAcc.UseSerialNumberTracking
				 , cdCurrAcc.IsBlocked
				 , cdCurrAcc.IsLocked
				 , cdCurrAcc.LockedDate
				 , VendorPaymentPlanGrCode = RTRIM(LTRIM(cdCurrAcc.VendorPaymentPlanGrCode))
				 , LangCode	= @LangCode
				 , CurrAccDescription = RTRIM(LTRIM(ISNULL(CurrAccDescription, SPACE(0)))) 
				FROM cdCurrAcc WITH (NOLOCK) 
					LEFT OUTER JOIN cdCurrAccDesc WITH (NOLOCK) 
						ON	cdCurrAccDesc.CurrAccTypeCode = cdCurrAcc.CurrAccTypeCode
						AND cdCurrAccDesc.CurrAccCode = cdCurrAcc.CurrAccCode
						AND cdCurrAccDesc.LangCode = @LangCode
		)
	
GO

EXEC sys.sp_addextendedproperty @name=N'NotCustomizable', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'FUNCTION',@level1name=N'CurrAcc'
GO
