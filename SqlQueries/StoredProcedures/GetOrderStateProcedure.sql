-- GetOrderState Stored Procedure
-- Sipariş durumlarını getiren stored procedure

 
/****** Object:  StoredProcedure [dbo].[qry_GetOrderState]    Script Date: 5/9/2025 2:28:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[qry_GetOrderState] (
	      @CurrAccTypeCode		tinyint			
		, @CurrAccCode			Char30	
		, @SubCurrAccID			uniqueidentifier		
		, @ProcessCode			Process	
		, @WarehouseCode		Char10			
		, @ProcessFlowCode		tinyint		
		, @FilterSubCurrAcc		bit	
		, @CustomerASNNumber	Char20 = null
		, @ToWarehouseCode		Char10 = null
)	
  
AS
	DECLARE @NullUniqueidentifier uniqueidentifier = '00000000-0000-0000-0000-000000000000'
	IF @SubCurrAccID IS NULL SET @SubCurrAccID = @NullUniqueidentifier

	SET @CustomerASNNumber = ISNULL(@CustomerASNNumber, SPACE(0))
	SET @ToWarehouseCode   = ISNULL(@ToWarehouseCode, SPACE(0))

	DECLARE @CustomerASNNumberIsRequiredForShipments bit 
	SELECT @CustomerASNNumberIsRequiredForShipments = CustomerASNNumberIsRequiredForShipments 
		FROM cdCurrAcc WITH(NOLOCK) 
		WHERE CurrAccTypeCode = @CurrAccTypeCode AND CurrAccCode = @CurrAccCode

	
	DECLARE @StoreCode		Char30 = ISNULL((SELECT CurrAccCode FROM cdWarehouse WITH(NOLOCK) WHERE WarehouseCode = @WarehouseCode AND CurrAccTypeCode = 5), SPACE(0))

	SELECT OrderNumber				= AllOrderLines.OrderNumber
		 , OrderDate				= AllOrderLines.OrderDate
		 , ReserveNumber			= SPACE(0)
		 , ReserveDate				= SPACE(0)
		 , DispOrderNumber			= SPACE(0)
		 , DispOrderDate			= SPACE(0)
		 , PickingNumber			= SPACE(0)
		 , PickingDate				= SPACE(0)
		 , OrderAsnNumber			= SPACE(0)
		 , OrderAsnDate				= SPACE(0)
		 , ShippingNumber			= SPACE(0)
		 , ShippingDate				= SPACE(0)
		 , ExportFileNumber			= AllOrderLines.ExportFileNumber
		 , Qty						= SUM(ISNULL(OrderDeliveryLocations.Qty1, stOrder.Qty1))
		 , SubCurrAccCode			= ISNULL(prSubCurrAcc.SubCurrAccCode , SPACE(0))
	     , SubCurrAccCompanyName	= ISNULL(prSubCurrAcc.CompanyName , SPACE(0))
		 , CustomerASNNumber		= SPACE(0)
		 , ToWarehouseCode			= AllOrderLines.ToWarehouseCode
		 , HeaderID					= AllOrderLines.OrderHeaderID
		FROM AllOrderLines
			INNER JOIN stOrder WITH(NOLOCK) ON AllOrderLines.OrderLineID		= stOrder.OrderLineID
			LEFT OUTER JOIN prSubCurrAcc WITH(NOLOCK)  ON prSubCurrAcc.SubCurrAccID = AllOrderLines.SubCurrAccID
			LEFT OUTER JOIN (SELECT OrderLineID 
							  , CompanyCode
							  , OfficeCode
							  , StoreTypeCode
							  , StoreCode
							  , WarehouseCode
							  , Qty1 = SUM(Qty1)
							FROM tpOrderDeliveryDetail WITH(NOLOCK)
							GROUP BY OrderLineID 
								, CompanyCode
								, OfficeCode
								, StoreTypeCode
								, StoreCode
								, WarehouseCode
							HAVING SUM(Qty1) > 0
							) AS OrderDeliveryLocations ON OrderDeliveryLocations.OrderLineID = AllOrderLines.OrderLineID
		WHERE	AllOrderLines.IsClosed			= 0	
			AND AllOrderLines.ProcessCode		= @ProcessCode
			AND AllOrderLines.CurrAccTypeCode	= @CurrAccTypeCode
			AND AllOrderLines.CurrAccCode		= @CurrAccCode
			AND AllOrderLines.IsCompleted		= 1
			AND CASE WHEN @FilterSubCurrAcc = 1 THEN ISNULL(AllOrderLines.SubCurrAccID, @NullUniqueidentifier) ELSE @SubCurrAccID END = @SubCurrAccID	
			AND CASE WHEN @ProcessCode IN(N'R', N'RI', N'RSD') THEN AllOrderLines.IsCreditableConfirmed ELSE 1 END = 1
			AND ((OrderDeliveryLocations.WarehouseCode IS NULL AND (AllOrderLines.WarehouseCode = SPACE(0) OR AllOrderLines.WarehouseCode = @WarehouseCode))
				OR 
				 (OrderDeliveryLocations.WarehouseCode IS NOT NULL AND OrderDeliveryLocations.WarehouseCode = @WarehouseCode)
				 )
			AND 
			(
				@ToWarehouseCode = SPACE(0)
				OR
				(
					@ToWarehouseCode <> SPACE(0) AND AllOrderLines.ToWarehouseCode IN(N'', @ToWarehouseCode)
				)
			)
			AND ((OrderDeliveryLocations.StoreCode IS NULL AND AllOrderLines.StoreCode = @StoreCode)
				OR 
				 (OrderDeliveryLocations.StoreCode IS NOT NULL AND OrderDeliveryLocations.StoreCode = @StoreCode)
				 )
			AND @ProcessFlowCode = 2	
	GROUP BY AllOrderLines.OrderNumber	
		   , AllOrderLines.OrderDate
		   , AllOrderLines.OrderHeaderID
		   , AllOrderLines.ExportFileNumber
		   , ISNULL(prSubCurrAcc.SubCurrAccCode , SPACE(0))
		   , ISNULL(prSubCurrAcc.CompanyName , SPACE(0))
		   , AllOrderLines.ToWarehouseCode
	UNION ALL
	SELECT OrderNumber				= SPACE(0)
		 , OrderDate				= SPACE(0)
		 , ReserveNumber			= AllReserveLines.ReserveNumber
		 , ReserveDate				= AllReserveLines.ReserveDate
		 , DispOrderNumber			= SPACE(0)
		 , DispOrderDate			= SPACE(0)
		 , PickingNumber			= SPACE(0)
		 , PickingDate				= SPACE(0)
		 , OrderAsnNumber			= SPACE(0)
		 , OrderAsnDate				= SPACE(0)
		 , ShippingNumber			= SPACE(0)
		 , ShippingDate				= SPACE(0)
		 , ExportFileNumber			= AllReserveLines.ExportFileNumber
		 , Qty						= SUM(stReserve.Qty1)
		 , SubCurrAccCode			= ISNULL(prSubCurrAcc.SubCurrAccCode , SPACE(0))
	     , SubCurrAccCompanyName	= ISNULL(prSubCurrAcc.CompanyName , SPACE(0))
		 , CustomerASNNumber		= SPACE(0)
		 , ToWarehouseCode			= AllReserveLines.ToWarehouseCode
		 , HeaderID					= AllReserveLines.ReserveHeaderID
		FROM AllReserveLines
			INNER JOIN stReserve WITH(NOLOCK) ON AllReserveLines.ReserveLineID		= stReserve.ReserveLineID
			LEFT OUTER JOIN prSubCurrAcc WITH(NOLOCK)  ON prSubCurrAcc.SubCurrAccID = AllReserveLines.SubCurrAccID
		WHERE AllReserveLines.ProcessCode			= @ProcessCode
			AND AllReserveLines.CurrAccTypeCode		= @CurrAccTypeCode
			AND AllReserveLines.CurrAccCode			= @CurrAccCode
			AND AllReserveLines.IsCompleted			= 1
			AND CASE WHEN @FilterSubCurrAcc = 1 THEN ISNULL(AllReserveLines.SubCurrAccID, @NullUniqueidentifier) ELSE @SubCurrAccID END = @SubCurrAccID		
			AND AllReserveLines.WarehouseCode		= @WarehouseCode
			AND 
			(
				@ToWarehouseCode = SPACE(0)
				OR
				(
					@ToWarehouseCode <> SPACE(0) AND AllReserveLines.ToWarehouseCode IN(N'', @ToWarehouseCode)
				)
			)
			AND @ProcessFlowCode = 3	
	GROUP BY AllReserveLines.ReserveNumber
		   , AllReserveLines.ReserveDate
		   , AllReserveLines.ReserveHeaderID
		   , AllReserveLines.ExportFileNumber
		   , ISNULL(prSubCurrAcc.SubCurrAccCode , SPACE(0))
		   , ISNULL(prSubCurrAcc.CompanyName , SPACE(0))
		   , AllReserveLines.ToWarehouseCode
	UNION ALL
	SELECT OrderNumber				= SPACE(0)
		 , OrderDate				= SPACE(0)
		 , ReserveNumber			= SPACE(0)
		 , ReserveDate				= SPACE(0)
		 , DispOrderNumber			= AllDispOrderLines.DispOrderNumber
		 , DispOrderDate			= AllDispOrderLines.DispOrderDate
		 , PickingNumber			= SPACE(0)
		 , PickingDate				= SPACE(0)
		 , OrderAsnNumber			= SPACE(0)
		 , OrderAsnDate				= SPACE(0)
		 , ShippingNumber			= SPACE(0)
		 , ShippingDate				= SPACE(0)
		 , ExportFileNumber			= AllDispOrderLines.ExportFileNumber
		 , Qty						= SUM(stDispOrder.Qty1)
		 , SubCurrAccCode			= ISNULL(prSubCurrAcc.SubCurrAccCode , SPACE(0))
	     , SubCurrAccCompanyName	= ISNULL(prSubCurrAcc.CompanyName , SPACE(0))
		 , CustomerASNNumber		= ISNULL(CustomerASNNumber, SPACE(0))
		 , ToWarehouseCode			= AllDispOrderLines.ToWarehouseCode
		 , HeaderID					= AllDispOrderLines.DispOrderHeaderID
		FROM AllDispOrderLines
			INNER JOIN stDispOrder WITH(NOLOCK)ON AllDispOrderLines.DispOrderLineID	= stDispOrder.DispOrderLineID
			LEFT OUTER JOIN prSubCurrAcc WITH(NOLOCK)  ON prSubCurrAcc.SubCurrAccID = AllDispOrderLines.SubCurrAccID
		WHERE AllDispOrderLines.ProcessCode				= @ProcessCode
			AND AllDispOrderLines.CurrAccTypeCode		= @CurrAccTypeCode
			AND AllDispOrderLines.CurrAccCode			= @CurrAccCode
			AND AllDispOrderLines.IsCompleted			= 1
			AND CASE WHEN @FilterSubCurrAcc = 1 THEN ISNULL(AllDispOrderLines.SubCurrAccID, @NullUniqueidentifier) ELSE @SubCurrAccID END = @SubCurrAccID		
			AND AllDispOrderLines.WarehouseCode			= @WarehouseCode
			AND 
			(
				@ToWarehouseCode = SPACE(0)
				OR
				(
					@ToWarehouseCode <> SPACE(0) AND AllDispOrderLines.ToWarehouseCode IN(N'', @ToWarehouseCode)
				)
			)
			AND @ProcessFlowCode = 4	
			AND CASE WHEN @CustomerASNNumberIsRequiredForShipments = 1 THEN AllDispOrderLines.CustomerASNNumber ELSE @CustomerASNNumber END = @CustomerASNNumber
	GROUP BY AllDispOrderLines.DispOrderNumber
		   , AllDispOrderLines.DispOrderDate
		   , AllDispOrderLines.DispOrderHeaderID
		   , AllDispOrderLines.ExportFileNumber
		   , ISNULL(prSubCurrAcc.SubCurrAccCode , SPACE(0))
		   , ISNULL(prSubCurrAcc.CompanyName , SPACE(0))
		   , ISNULL(CustomerASNNumber, SPACE(0))
		   , AllDispOrderLines.ToWarehouseCode
	UNION ALL
	SELECT OrderNumber				= SPACE(0)
		 , OrderDate				= SPACE(0)
		 , ReserveNumber			= SPACE(0)
		 , ReserveDate				= SPACE(0)
		 , DispOrderNumber			= SPACE(0)
		 , DispOrderDate			= SPACE(0)
		 , PickingNumber			= AllPickingLines.PickingNumber
		 , PickingDate				= AllPickingLines.PickingDate
		 , OrderAsnNumber			= SPACE(0)
		 , OrderAsnDate				= SPACE(0)
		 , ShippingNumber			= SPACE(0)
		 , ShippingDate				= SPACE(0)
		 , ExportFileNumber			= AllPickingLines.ExportFileNumber
		 , Qty						= SUM(stPicking.Qty1)
		 , SubCurrAccCode			= ISNULL(prSubCurrAcc.SubCurrAccCode , SPACE(0))
	     , SubCurrAccCompanyName	= ISNULL(prSubCurrAcc.CompanyName , SPACE(0))
		 , CustomerASNNumber		= ISNULL(CustomerASNNumber, SPACE(0))
		 , ToWarehouseCode			= AllPickingLines.ToWarehouseCode
		 , HeaderID					= AllPickingLines.PickingHeaderID
		FROM AllPickingLines
			INNER JOIN stPicking WITH(NOLOCK)ON AllPickingLines.PickingLineID		= stPicking.PickingLineID
			LEFT OUTER JOIN prSubCurrAcc WITH(NOLOCK)  ON prSubCurrAcc.SubCurrAccID = AllPickingLines.SubCurrAccID
		WHERE AllPickingLines.ProcessCode			= @ProcessCode
			AND AllPickingLines.CurrAccTypeCode		= @CurrAccTypeCode
			AND AllPickingLines.CurrAccCode			= @CurrAccCode
			AND AllPickingLines.IsCompleted			= 1
			AND CASE WHEN @FilterSubCurrAcc = 1 THEN ISNULL(AllPickingLines.SubCurrAccID, @NullUniqueidentifier) ELSE @SubCurrAccID END = @SubCurrAccID		
			AND AllPickingLines.WarehouseCode		= @WarehouseCode
			AND 
			(
				@ToWarehouseCode = SPACE(0)
				OR
				(
					@ToWarehouseCode <> SPACE(0) AND AllPickingLines.ToWarehouseCode IN(N'', @ToWarehouseCode)
				)
			)
			AND @ProcessFlowCode = 5	
			AND CASE WHEN @CustomerASNNumberIsRequiredForShipments = 1 THEN AllPickingLines.CustomerASNNumber ELSE @CustomerASNNumber END = @CustomerASNNumber
	GROUP BY AllPickingLines.PickingNumber
		   , AllPickingLines.PickingDate
		   , AllPickingLines.PickingHeaderID
		   , AllPickingLines.ExportFileNumber
		   , ISNULL(prSubCurrAcc.SubCurrAccCode , SPACE(0))
		   , ISNULL(prSubCurrAcc.CompanyName , SPACE(0))
		   , ISNULL(CustomerASNNumber, SPACE(0))
		   , AllPickingLines.ToWarehouseCode
	UNION ALL
	SELECT OrderNumber				= SPACE(0)
		 , OrderDate				= SPACE(0)
		 , ReserveNumber			= SPACE(0)
		 , ReserveDate				= SPACE(0)
		 , DispOrderNumber			= SPACE(0)
		 , DispOrderDate			= SPACE(0)
		 , PickingNumber			= SPACE(0)
		 , PickingDate				= SPACE(0)
		 , OrderAsnNumber			= AllOrderASNlines.OrderAsnNumber
		 , OrderAsnDate				= AllOrderASNlines.OrderAsnDate
		 , ShippingNumber			= SPACE(0)
		 , ShippingDate				= SPACE(0)
		 , ExportFileNumber			= SPACE(0)
		 , Qty						= SUM(stOrderAsn.Qty1)
		 , SubCurrAccCode			= SPACE(0)
	     , SubCurrAccCompanyName	= SPACE(0)
		 , CustomerASNNumber		= SPACE(0)
		 , ToWarehouseCode			= SPACE(0)
		 , HeaderID					= AllOrderAsnLines.OrderAsnHeaderID
		FROM AllOrderAsnLines
			INNER JOIN stOrderAsn WITH(NOLOCK)ON AllOrderAsnLines.OrderAsnLineID = stOrderAsn.OrderAsnLineID
		WHERE AllOrderAsnLines.ProcessCode			= @ProcessCode
			AND AllOrderAsnLines.CurrAccTypeCode	= @CurrAccTypeCode
			AND AllOrderAsnLines.CurrAccCode		= @CurrAccCode
			AND AllOrderAsnLines.WarehouseCode		= @WarehouseCode
			AND AllOrderAsnLines.IsCompleted		= 1
			AND @ProcessFlowCode = 99	
	GROUP BY AllOrderASNlines.OrderAsnNumber
		   , AllOrderASNlines.OrderAsnDate
		   , AllOrderAsnLines.OrderAsnHeaderID
	UNION ALL
	SELECT OrderNumber				= SPACE(0)
		 , OrderDate				= SPACE(0)
		 , ReserveNumber			= SPACE(0)
		 , ReserveDate				= SPACE(0)
		 , DispOrderNumber			= SPACE(0)
		 , DispOrderDate			= SPACE(0)
		 , PickingNumber			= SPACE(0)
		 , PickingDate				= SPACE(0)
		 , OrderAsnNumber			= SPACE(0)
		 , OrderAsnDate				= SPACE(0)
		 , ShippingNumber			= AllShipmentLines.ShippingNumber
		 , ShippingDate				= AllShipmentLines.ShippingDate
		 , ExportFileNumber			= AllShipmentLines.ExportFileNumber
		 , Qty						= SUM(stShipment.Qty1)
		 , SubCurrAccCode			= ISNULL(prSubCurrAcc.SubCurrAccCode , SPACE(0))
	     , SubCurrAccCompanyName	= ISNULL(prSubCurrAcc.CompanyName , SPACE(0))
		 , CustomerASNNumber		= ISNULL(CustomerASNNumber, SPACE(0))
		 , ToWarehouseCode			= AllShipmentLines.ToWarehouseCode
		 , HeaderID					= AllShipmentLines.ShipmentHeaderID
		FROM AllShipmentLines
			INNER JOIN stShipment WITH(NOLOCK)ON AllShipmentLines.ShipmentLineID		= stShipment.ShipmentLineID
			LEFT OUTER JOIN prSubCurrAcc WITH(NOLOCK)  ON prSubCurrAcc.SubCurrAccID = AllShipmentLines.SubCurrAccID
		WHERE AllShipmentLines.ProcessCode			= @ProcessCode
			AND AllShipmentLines.CurrAccTypeCode	= @CurrAccTypeCode
			AND AllShipmentLines.CurrAccCode		= @CurrAccCode
			AND AllShipmentLines.WarehouseCode		= @WarehouseCode
			AND AllShipmentLines.IsCompleted		= 1
			AND 
			(
				@ToWarehouseCode = SPACE(0)
				OR
				(
					@ToWarehouseCode <> SPACE(0) AND AllShipmentLines.ToWarehouseCode IN(N'', @ToWarehouseCode)
				)
			)
			AND @ProcessFlowCode = 6	
			AND CASE WHEN @CustomerASNNumberIsRequiredForShipments = 1 THEN AllShipmentLines.CustomerASNNumber ELSE @CustomerASNNumber END = @CustomerASNNumber
	GROUP BY AllShipmentLines.ShippingNumber
		   , AllShipmentLines.ShippingDate
		   , AllShipmentLines.ShipmentHeaderID
		   , AllShipmentLines.ExportFileNumber
		   , ISNULL(prSubCurrAcc.SubCurrAccCode , SPACE(0))
		   , ISNULL(prSubCurrAcc.CompanyName , SPACE(0))
		   , ISNULL(CustomerASNNumber, SPACE(0))
		   , AllShipmentLines.ToWarehouseCode
