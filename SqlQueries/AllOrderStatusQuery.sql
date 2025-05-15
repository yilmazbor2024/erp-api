SELECT        IDs.OrderLineID, IDs.OrderQty1, IDs.OrderAsnQty1, IDs.ReserveQty1, IDs.DispOrderQty1, IDs.PickingQty1, IDs.ShipmentQty1, IDs.InvoiceQty1, ISNULL(R.ReserveQty1, 0) AS RemainingReserveQty1, ISNULL(D.DispOrderQty1, 
                         0) AS RemainingDispOrderQty1, ISNULL(P.PickingQty1, 0) AS RemainingPickingQty1, ISNULL(OA.OrderAsnQty1, 0) AS RemainingOrderAsnQty1, IDs.OrderQty1 - IDs.ShipmentQty1 AS RemainingOrderQty1
FROM            (SELECT        OrderLineID, SUM(OrderQty1) AS OrderQty1, SUM(OrderAsnQty1) AS OrderAsnQty1, SUM(ReserveQty1) AS ReserveQty1, SUM(DispOrderQty1) AS DispOrderQty1, SUM(PickingQty1) AS PickingQty1, 
                                                    SUM(ShipmentQty1) + SUM(InvoiceQty1) AS ShipmentQty1, SUM(InvoiceQty1) AS InvoiceQty1
                          FROM            (SELECT        OrderLineID, SUM(Qty1 - CancelQty1) AS OrderQty1, 0 AS OrderAsnQty1, 0 AS ReserveQty1, 0 AS DispOrderQty1, 0 AS PickingQty1, 0 AS ShipmentQty1, 0 AS InvoiceQty1
                                                    FROM            dbo.trOrderLine WITH (NOLOCK)
                                                    GROUP BY OrderLineID
                                                    UNION ALL
                                                    SELECT        OrderLineID, 0 AS OrderQty1, SUM(Qty1) AS OrderAsnQty1, 0 AS ReserveQty1, 0 AS DispOrderQty1, 0 AS PickingQty1, 0 AS ShipmentQty1, 0 AS InvoiceQty1
                                                    FROM            dbo.trOrderAsnLine WITH (NOLOCK)
                                                    GROUP BY OrderLineID
                                                    UNION ALL
                                                    SELECT        OrderLineID, 0 AS OrderQty1, 0 AS OrderAsnQty1, SUM(Qty1) AS ReserveQty1, 0 AS DispOrderQty1, 0 AS PickingQty1, 0 AS ShipmentQty1, 0 AS InvoiceQty1
                                                    FROM            dbo.trReserveLine WITH (NOLOCK)
                                                    GROUP BY OrderLineID
                                                    UNION ALL
                                                    SELECT        OrderLineID, 0 AS OrderQty1, 0 AS OrderAsnQty1, 0 AS ReserveQty1, SUM(Qty1) AS DispOrderQty1, 0 AS PickingQty1, 0 AS ShipmentQty1, 0 AS InvoiceQty1
                                                    FROM            dbo.trDispOrderLine WITH (NOLOCK)
                                                    GROUP BY OrderLineID
                                                    UNION ALL
                                                    SELECT        OrderLineID, 0 AS OrderQty1, 0 AS OrderAsnQty1, 0 AS ReserveQty1, 0 AS DispOrderQty1, SUM(Qty1) AS PickingQty1, 0 AS ShipmentQty1, 0 AS InvoiceQty1
                                                    FROM            dbo.trPickingLine WITH (NOLOCK)
                                                    GROUP BY OrderLineID
                                                    UNION ALL
                                                    SELECT        OrderLineID, 0 AS OrderQty1, 0 AS OrderAsnQty1, 0 AS ReserveQty1, 0 AS DispOrderQty1, 0 AS PickingQty1, SUM(Qty1) AS ShipmentQty1, 0 AS InvoiceQty1
                                                    FROM            dbo.trShipmentLine WITH (NOLOCK)
                                                    GROUP BY OrderLineID
                                                    UNION ALL
                                                    SELECT        OrderLineID, 0 AS OrderQty1, 0 AS OrderAsnQty1, 0 AS ReserveQty1, 0 AS DispOrderQty1, 0 AS PickingQty1, 0 AS ShipmentQty1, SUM(Qty1) AS InvoiceQty1
                                                    FROM            dbo.trInvoiceLine WITH (NOLOCK)
                                                    WHERE        (ShipmentLineID IS NULL)
                                                    GROUP BY OrderLineID) AS IDs
                          GROUP BY OrderLineID) AS IDs LEFT OUTER JOIN
                             (SELECT        OrderLineID, SUM(Qty1) AS ReserveQty1
                               FROM            dbo.stReserve WITH (NOLOCK)
                               GROUP BY OrderLineID) AS R ON IDs.OrderLineID = R.OrderLineID LEFT OUTER JOIN
                             (SELECT        OrderLineID, SUM(Qty1) AS DispOrderQty1
                               FROM            dbo.stDispOrder WITH (NOLOCK)
                               GROUP BY OrderLineID) AS D ON IDs.OrderLineID = D.OrderLineID LEFT OUTER JOIN
                             (SELECT        OrderLineID, SUM(Qty1) AS PickingQty1
                               FROM            dbo.stPicking WITH (NOLOCK)
                               GROUP BY OrderLineID) AS P ON IDs.OrderLineID = P.OrderLineID LEFT OUTER JOIN
                             (SELECT        OrderLineID, SUM(Qty1) AS OrderAsnQty1
                               FROM            dbo.stOrderAsn WITH (NOLOCK)
                               GROUP BY OrderLineID) AS OA ON IDs.OrderLineID = OA.OrderLineID
