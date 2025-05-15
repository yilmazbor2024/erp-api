-- GetShipmentFromInvoice Stored Procedure
-- Fatura ID'sine göre sevkiyat ID'lerini getiren stored procedure

USE [DENEME]
GO
/****** Object:  StoredProcedure [dbo].[qry_GetShipmentFromInvoice]    Script Date: 5/9/2025 2:11:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[qry_GetShipmentFromInvoice]  (
		@InvoiceHeaderID			uniqueidentifier		
)

AS
	SELECT DISTINCT ShipmentHeaderID
		FROM trShipmentLine WITH(NOLOCK) 
		WHERE EXISTS (SELECT * FROM trInvoiceLine WITH(NOLOCK) 
							WHERE trInvoiceLine.InvoiceHEaderID = @InvoiceHeaderID
							AND trInvoiceLine.ShipmentLineID = trShipmentLine.ShipmentLineID)
