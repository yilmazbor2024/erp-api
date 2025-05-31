-- OrderStatus Function
-- Sipariş durumlarını ve kalan miktarları getiren fonksiyon

 
/****** Object:  UserDefinedFunction [dbo].[OrderStatus]    Script Date: 5/9/2025 2:28:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER FUNCTION [dbo].[OrderStatus]()
RETURNS TABLE

AS RETURN
(
SELECT O.*
	 , RemainingReserveQty1		 = ISNULL(ReserveQty1,0)
	 , RemainingDispOrderQty1	 = ISNULL(DispOrderQty1,0)
	 , RemainingPickingQty1		 = ISNULL(PickingQty1,0)
	 , RemainingOrderAsnQty1	 = ISNULL(OrderAsnQty1,0)
	 , ShipmentQty1				 = ISNULL(ShipmentQty1 	, 0 ) + ISNULL(InvoiceQty1 	, 0 )
	 , RemainingOrderQty1		 = (O.Qty1 - O.CancelQty1) - (ISNULL(ShipmentQty1 	, 0 ) + ISNULL(InvoiceQty1 	, 0 ))
	FROM AllOrdersWithAttributes AS O
		LEFT OUTER JOIN ( SELECT OrderLineID,	ReserveQty1		= SUM(stReserve.Qty1)	FROM stReserve 	WITH(NOLOCK)		GROUP BY OrderLineID) AS R ON O.OrderLineID = R.OrderLineID 
		LEFT OUTER JOIN ( SELECT OrderLineID,	DispOrderQty1	= SUM(stDispOrder.Qty1) FROM stDispOrder WITH(NOLOCK)		GROUP BY OrderLineID) AS D ON O.OrderLineID = D.OrderLineID 
		LEFT OUTER JOIN ( SELECT OrderLineID,	PickingQty1		= SUM(stPicking.Qty1)	FROM stPicking	WITH(NOLOCK)		GROUP BY OrderLineID) AS P ON O.OrderLineID = P.OrderLineID 
		LEFT OUTER JOIN ( SELECT OrderLineID,	OrderAsnQty1	= SUM(stOrderAsn.Qty1)	FROM stOrderAsn	WITH(NOLOCK)		GROUP BY OrderLineID) AS OA ON O.OrderLineID = OA.OrderLineID 
		LEFT OUTER JOIN ( SELECT OrderLineID ,	ShipmentQty1	= SUM(Qty1)				FROM trShipmentLine	WITH(NOLOCK)	GROUP BY OrderLineID) AS S ON O.OrderLineID = S.OrderLineID 
		LEFT OUTER JOIN ( SELECT OrderLineID ,  InvoiceQty1		= SUM(Qty1)				FROM trInvoiceLine 	WITH(NOLOCK)	WHERE ShipmentLineID IS NULL GROUP BY OrderLineID) AS I ON O.OrderLineID = I.OrderLineID    
)
