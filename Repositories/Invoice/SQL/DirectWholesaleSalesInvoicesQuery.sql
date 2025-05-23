SELECT InvoiceNumber		= trInvoiceHeader.InvoiceNumber
	 , IsReturn				= trInvoiceHeader.IsReturn
	 , IsEInvoice			= trInvoiceHeader.IsEInvoice
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
	 , ProcessCode			= trInvoiceHeader.ProcessCode
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
	 , JournalNumber		= CASE trInvoiceHeader.IsPostingJournal WHEN 0 THEN SPACE(0) ELSE 
	                              	   ISNULL(REPLACE(N'QWERTY'+(SELECT DISTINCT N', ' +  trJournalHeader.JournalNumber 
														FROM trJournalHeader WITH(NOLOCK)  
														WHERE trJournalHeader.ApplicationCode = N'Invoi'
													 AND trJournalHeader.ApplicationID	 = trInvoiceHeader.InvoiceHeaderID
														 FOR XML PATH('')), N'QWERTY, ', SPACE(0)),SPACE(0)) END
	 , IsPrinted			= trInvoiceHeader.IsPrinted
	 , ApplicationCode		= trInvoiceHeader.ApplicationCode
	 , ApplicationDescription = ISNULL(bsApplicationDesc.ApplicationDescription , SPACE(0))
	 , ApplicationID		= trInvoiceHeader.ApplicationID	 	 			 
	 , InvoiceHeaderID		= trInvoiceHeader.InvoiceHeaderID	
	 , FormType				= trInvoiceHeader.FormType
	 , DocumentTypeCode		= trInvoiceHeader.DocumentTypeCode
	FROM trInvoiceHeader WITH (NOLOCK) 
		LEFT OUTER JOIN prSubCurrAcc WITH (NOLOCK)
			ON prSubCurrAcc.SubCurrAccID			= trInvoiceHeader.SubCurrAccID			
		LEFT OUTER JOIN bsApplicationDesc WITH(NOLOCK)
			ON	bsApplicationDesc.ApplicationCode	= trInvoiceHeader.ApplicationCode
			AND bsApplicationDesc.LangCode			= {LangCode}
	    LEFT OUTER JOIN cdCurrAccDesc WITH (NOLOCK)
			ON	cdCurrAccDesc.CurrAccTypeCode		= trInvoiceHeader.CurrAccTypeCode 
			AND cdCurrAccDesc.CurrAccCode			= trInvoiceHeader.CurrAccCode
			AND cdCurrAccDesc.LangCode				= {LangCode}	
		LEFT OUTER JOIN tpInvoiceHeaderExtension WITH(NOLOCK)
			ON tpInvoiceHeaderExtension.InvoiceHeaderID = trInvoiceHeader.InvoiceHeaderID
