SELECT InvoiceNumber		= trInvoiceHeader.InvoiceNumber
	 , IsReturn				= trInvoiceHeader.IsReturn
	 , IsEInvoice			= trInvoiceHeader.IsEInvoice
	 , ProcessCode			= trInvoiceHeader.ProcessCode
	 , InvoiceDate			= trInvoiceHeader.InvoiceDate
	 , InvoiceTime			= trInvoiceHeader.InvoiceTime 
	 , CurrAccTypeCode		= trInvoiceHeader.CurrAccTypeCode
	 
	 , VendorCode			= CASE trInvoiceHeader.CurrAccTypeCode	WHEN 1	THEN trInvoiceHeader.CurrAccCode			ELSE SPACE(0) END
	 , VendorDescription	= CASE trInvoiceHeader.CurrAccTypeCode	WHEN 1	THEN ISNULL(CurrAccDescription, SPACE(0))	ELSE SPACE(0) END	 
	 , CustomerCode			= CASE trInvoiceHeader.CurrAccTypeCode	WHEN 3	THEN trInvoiceHeader.CurrAccCode			ELSE SPACE(0) END
	 , CustomerDescription	= CASE trInvoiceHeader.CurrAccTypeCode	WHEN 3	THEN ISNULL(CurrAccDescription, SPACE(0))	ELSE SPACE(0) END
	 , RetailCustomerCode	= CASE trInvoiceHeader.CurrAccTypeCode	WHEN 4	THEN trInvoiceHeader.CurrAccCode			ELSE SPACE(0) END
	 , StoreCurrAccCode		= CASE trInvoiceHeader.CurrAccTypeCode	WHEN 5	THEN trInvoiceHeader.CurrAccCode			ELSE SPACE(0) END
	 , StoreDescription		= CASE trInvoiceHeader.CurrAccTypeCode	WHEN 5	THEN ISNULL(CurrAccDescription, SPACE(0))	ELSE SPACE(0) END
	 , EmployeeCode			= CASE trInvoiceHeader.CurrAccTypeCode	WHEN 8	THEN trInvoiceHeader.CurrAccCode			ELSE SPACE(0) END
	 , FirstLastName		= CASE WHEN trInvoiceHeader.CurrAccTypeCode	IN (4,8)
								   THEN ISNULL((SELECT FirstLastName FROM tpInvoicePostalAddress 
												WHERE trInvoiceHeader.InvoiceHeaderID = tpInvoicePostalAddress.InvoiceHeaderID), 
													(SELECT FirstLastName FROM cdCurrAcc 
														 WHERE trInvoiceHeader.CurrAccCode = cdCurrAcc.CurrAccCode 
															AND trInvoiceHeader.CurrAccTypeCode = cdCurrAcc.CurrAccTypeCode)) 
																 ELSE SPACE(0) END	
	 , SubCurrAccCode		= ISNULL (SubCurrAccCode , SPACE(0))
	 , SubCurrAccCompanyName= ISNULL(prSubCurrAcc.CompanyName , SPACE(0))
	 , IsCreditSale			= trInvoiceHeader.IsCreditSale
	 , TransTypeCode		= trInvoiceHeader.TransTypeCode
	 , DocCurrencyCode		= trInvoiceHeader.DocCurrencyCode
	 , Series				= trInvoiceHeader.Series
	 , SeriesNumber			= trInvoiceHeader.SeriesNumber
	 , EInvoiceNumber		= trInvoiceHeader.EInvoiceNumber
	 , CompanyCode			= trInvoiceHeader.CompanyCode
	 , OfficeCode			= trInvoiceHeader.OfficeCode
	 , StoreCode			= trInvoiceHeader.StoreCode
	 , WarehouseCode		= trInvoiceHeader.WarehouseCode
	 , ImportFileNumber     = trInvoiceHeader.ImportFileNumber
	 , ExportFileNumber     = trInvoiceHeader.ExportFileNumber
	 , ExportTypeCode		= ISNULL(ExportTypeCode, SPACE(0))
	 , PosTerminalID		= trInvoiceHeader.PosTerminalID
	 , TaxTypeCode			= trInvoiceHeader.TaxTypeCode
	 , IsCompleted			= trInvoiceHeader.IsCompleted
	 , IsSuspended			= trInvoiceHeader.IsSuspended
	 , IsLocked				= trInvoiceHeader.IsLocked
	 , IsOrderBase			= trInvoiceHeader.IsOrderBase
	 , IsShipmentBase		= trInvoiceHeader.IsShipmentBase
	 , IsPostingJournal		= trInvoiceHeader.IsPostingJournal
	 , JournalNumber		= CASE trInvoiceHeader.IsPostingJournal WHEN 0 THEN SPACE(0) ELSE ISNULL(JournalNumber, SPACE(0)) END
	 , InvoiceHeaderID		= trInvoiceHeader.InvoiceHeaderID
FROM trInvoiceHeader WITH (NOLOCK)
LEFT OUTER JOIN cdCurrAccDesc WITH (NOLOCK)
	ON cdCurrAccDesc.CurrAccTypeCode = trInvoiceHeader.CurrAccTypeCode 
	AND cdCurrAccDesc.CurrAccCode = trInvoiceHeader.CurrAccCode
	AND cdCurrAccDesc.LangCode = {LangCode}
LEFT OUTER JOIN prSubCurrAcc WITH (NOLOCK)
	ON prSubCurrAcc.SubCurrAccCode = trInvoiceHeader.SubCurrAccCode
WHERE 1=1
AND trInvoiceHeader.IsShipmentBase = 1 --İrsaliye bazlı
{ProcessCodeFilter}
{InvoiceNumberFilter}
{CustomerCodeFilter}
{VendorCodeFilter}
{CompanyCodeFilter}
{StoreCodeFilter}
{WarehouseCodeFilter}
{StartDateFilter}
{EndDateFilter}
ORDER BY trInvoiceHeader.InvoiceDate DESC
OFFSET {Offset} ROWS
FETCH NEXT {PageSize} ROWS ONLY
