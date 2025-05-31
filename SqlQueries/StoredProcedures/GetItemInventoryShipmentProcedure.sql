-- GetItemInventory_Shipment Stored Procedure
-- Ürün stok miktarını ve kullanılabilir stok miktarını hesaplayan stored procedure

 
/****** Object:  StoredProcedure [dbo].[qry_GetItemInventory_Shipment]    Script Date: 5/9/2025 2:09:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[qry_GetItemInventory_Shipment] (
		  @WarehouseCode			Char10 
		, @ItemTypeCode				tinyint
		, @ItemCode					Char30
		, @ColorCode				Char10
		, @ItemDim1Code				Char10
		, @ItemDim2Code				Char10
		, @ItemDim3Code				Char10
		, @CurrAccTypeCode			tinyint
		, @CurrAccCode				Char30
		, @SubCurrAccID				uniqueidentifier
		, @ProcessFlowCode			tinyint
		, @BatchCode				Char20
		, @SectionCode				Char10
		, @InnerOrderLineID			uniqueidentifier = null
		, @UseSection				bit = 0
		, @UseBatch					bit = 0
		, @AvailableInventoryQty1	float OUTPUT
)	 
AS
	
	IF @UseBatch = 0 AND @UseSection = 0 
	BEGIN
		SELECT @AvailableInventoryQty1 = 
			CASE  
				WHEN @ProcessFlowCode = 3	  THEN SUM(InventoryQty1 - (BalanceReserveQty1 + BalanceDispOrderQty1 + BalancePickingQty1 + BalancePickingQty1Customer + BalanceDispOrderQty1Customer + BalanceReserveQty1Customer + BalanceReserveQty1ForOpenOrder + BalanceReserveQty1CustomerForOpenOrder)) 
				WHEN @ProcessFlowCode = 4	  THEN SUM(InventoryQty1 - (BalanceReserveQty1 + BalanceDispOrderQty1 + BalancePickingQty1 + BalancePickingQty1Customer + BalanceDispOrderQty1Customer)) 
				WHEN @ProcessFlowCode = 5	  THEN SUM(InventoryQty1 - (BalanceReserveQty1 + BalanceDispOrderQty1 + BalancePickingQty1 + BalancePickingQty1Customer)) 
				WHEN @ProcessFlowCode IN(6,7) THEN SUM(InventoryQty1 - (BalanceReserveQty1 + BalanceDispOrderQty1 + BalancePickingQty1)) 
				ELSE							   SUM(InventoryQty1)
			END	
		FROM
		(	
			SELECT InventoryQty1								= SUM(In_Qty1 - Out_Qty1) 
					, BalanceReserveQty1						= 0
					, BalanceReserveQty1Customer				= 0
					, BalanceReserveQty1ForOpenOrder			= 0
					, BalanceReserveQty1CustomerForOpenOrder	= 0
					, BalanceDispOrderQty1						= 0
					, BalanceDispOrderQty1Customer				= 0
					, BalancePickingQty1						= 0
					, BalancePickingQty1Customer				= 0	
			FROM trStock WITH(NOLOCK)
			WHERE trStock.WarehouseCode	 = @WarehouseCode 
 				AND trStock.ItemTypeCode = @ItemTypeCode AND trStock.ItemCode = @ItemCode AND trStock.ColorCode = @ColorCode AND trStock.ItemDim1Code= @ItemDim1Code AND trStock.ItemDim2Code= @ItemDim2Code AND trStock.ItemDim3Code = @ItemDim3Code
			UNION ALL
			SELECT 
				  InventoryQty1							 = SUM (InventoryQty1)
				, BalanceReserveQty1					 = SUM (BalanceReserveQty1)
				, BalanceReserveQty1Customer			 = SUM (BalanceReserveQty1Customer)
				, BalanceReserveQty1ForOpenOrder		 = SUM (BalanceReserveQty1ForOpenOrder)
				, BalanceReserveQty1CustomerForOpenOrder = SUM (BalanceReserveQty1CustomerForOpenOrder)
													   
				, BalanceDispOrderQty1					 = SUM (BalanceDispOrderQty1)
				, BalanceDispOrderQty1Customer			 = SUM (BalanceDispOrderQty1Customer)
				, BalancePickingQty1					 = SUM (BalancePickingQty1)
				, BalancePickingQty1Customer			 = SUM (BalancePickingQty1Customer)
			FROM
			(
				SELECT		  InventoryQty1								= 0
							, BalanceReserveQty1						= ISNULL(SUM(stReserve.Qty1 * (CASE WHEN ReserveHead.ReserveTypeCode = 3 THEN 0 ELSE 1 END) * (CASE WHEN ReserveHead.CurrAccTypeCode = @CurrAccTypeCode AND ReserveHead.CurrAccCode = @CurrAccCode AND ISNULL(ReserveHead.SubCurrAccID, '00000000-0000-0000-0000-000000000000') = ISNULL(@SubCurrAccID, '00000000-0000-0000-0000-000000000000') THEN 0 ELSE 1 END)), 0)
							, BalanceReserveQty1Customer				= ISNULL(SUM(stReserve.Qty1 * (CASE WHEN ReserveHead.ReserveTypeCode = 3 THEN 0 ELSE 1 END) * (CASE WHEN ReserveHead.CurrAccTypeCode = @CurrAccTypeCode AND ReserveHead.CurrAccCode = @CurrAccCode AND ISNULL(ReserveHead.SubCurrAccID, '00000000-0000-0000-0000-000000000000') = ISNULL(@SubCurrAccID, '00000000-0000-0000-0000-000000000000') THEN 1 ELSE 0 END)), 0)
							, BalanceReserveQty1ForOpenOrder			= ISNULL(SUM(stReserve.Qty1 * (CASE WHEN ReserveHead.ReserveTypeCode = 3 THEN 1 ELSE 0 END) * (CASE WHEN ReserveHead.CurrAccTypeCode = @CurrAccTypeCode AND ReserveHead.CurrAccCode = @CurrAccCode AND ISNULL(ReserveHead.SubCurrAccID, '00000000-0000-0000-0000-000000000000') = ISNULL(@SubCurrAccID, '00000000-0000-0000-0000-000000000000') THEN 0 ELSE 1 END)), 0)
							, BalanceReserveQty1CustomerForOpenOrder	= ISNULL(SUM(stReserve.Qty1 * (CASE WHEN ReserveHead.ReserveTypeCode = 3 THEN 1 ELSE 0 END) * (CASE WHEN ReserveHead.CurrAccTypeCode = @CurrAccTypeCode AND ReserveHead.CurrAccCode = @CurrAccCode AND ISNULL(ReserveHead.SubCurrAccID, '00000000-0000-0000-0000-000000000000') = ISNULL(@SubCurrAccID, '00000000-0000-0000-0000-000000000000') THEN 1 ELSE 0 END)), 0)
							
							, BalanceDispOrderQty1						= 0
							, BalanceDispOrderQty1Customer				= 0
							, BalancePickingQty1						= 0
							, BalancePickingQty1Customer				= 0
				
				FROM trOrderLine OrderLines WITH (NOLOCK)
					INNER JOIN trOrderHeader WITH (NOLOCK)
						ON OrderLines.OrderHeaderID = trOrderHeader.OrderHeaderID
							AND trOrderHeader.OrderTypeCode = 1
						LEFT OUTER JOIN stReserve WITH (NOLOCK)
						ON stReserve.OrderLineID = OrderLines.OrderLineID
					LEFT OUTER JOIN trReserveLine ReserveLine WITH (NOLOCK)
						ON stReserve.ReserveLineID = ReserveLine.ReserveLineID
					LEFT OUTER JOIN trReserveHeader ReserveHead WITH (NOLOCK)
						ON ReserveHead.ReserveHeaderID = ReserveLine.ReserveHeaderID
				WHERE ReserveHead.WarehouseCode			= @WarehouseCode
						AND OrderLines.ItemTypeCode		= @ItemTypeCode
						AND OrderLines.ItemCode			= @ItemCode
						AND OrderLines.ColorCode			= @ColorCode
						AND OrderLines.ItemDim1Code		= @ItemDim1Code
						AND OrderLines.ItemDim2Code		= @ItemDim2Code
						AND OrderLines.ItemDim3Code		= @ItemDim3Code
					--  AND OrderLines.IsClosed			= 0
						AND @ProcessFlowCode IN(3,5,6,7) -- 4 olamayacak, sevk emmrinde onceki process reserve ise reservin env den dusmemesi gerekiyor, reserve degilse zaten 0 gelcek sorun olmaz (117462)
				GROUP BY trOrderHeader.CurrAccTypeCode, trOrderHeader.CurrAccCode, trOrderHeader.SubCurrAccID, ReserveHead.ReserveTypeCode, OrderLines.OrderLineID
				UNION ALL
				SELECT		  InventoryQty1								= 0
							, BalanceReserveQty1						= 0
							, BalanceReserveQty1Customer				= 0
							, BalanceReserveQty1ForOpenOrder			= 0
							, BalanceReserveQty1CustomerForOpenOrder	= 0
							
							, BalanceDispOrderQty1						= ISNULL(SUM(stDispOrder.Qty1 * (CASE WHEN DispHead.CurrAccTypeCode = @CurrAccTypeCode AND DispHead.CurrAccCode = @CurrAccCode AND ISNULL(DispHead.SubCurrAccID, '00000000-0000-0000-0000-000000000000') = ISNULL(@SubCurrAccID, '00000000-0000-0000-0000-000000000000') THEN 0 ELSE 1 END)), 0)
							, BalanceDispOrderQty1Customer				= ISNULL(SUM(stDispOrder.Qty1 * (CASE WHEN DispHead.CurrAccTypeCode = @CurrAccTypeCode AND DispHead.CurrAccCode = @CurrAccCode AND ISNULL(DispHead.SubCurrAccID, '00000000-0000-0000-0000-000000000000') = ISNULL(@SubCurrAccID, '00000000-0000-0000-0000-000000000000') THEN 1 ELSE 0 END)), 0)
							, BalancePickingQty1						= 0
							, BalancePickingQty1Customer				= 0
				FROM trOrderLine OrderLines WITH (NOLOCK)
					INNER JOIN trOrderHeader WITH (NOLOCK)
						ON OrderLines.OrderHeaderID = trOrderHeader.OrderHeaderID
							AND trOrderHeader.OrderTypeCode = 1
					LEFT OUTER JOIN stDispOrder WITH (NOLOCK)
						ON stDispOrder.OrderLineID = OrderLines.OrderLineID
					LEFT JOIN trDispOrderLine DispLine WITH (NOLOCK)
						ON stDispOrder.DispOrderLineID = DispLine.DispOrderLineID
					LEFT OUTER JOIN trDispOrderHeader DispHead WITH (NOLOCK)
						ON DispHead.DispOrderHeaderID = DispLine.DispOrderHeaderID
				WHERE DispHead.WarehouseCode			= @WarehouseCode
						AND OrderLines.ItemTypeCode		= @ItemTypeCode
						AND OrderLines.ItemCode			= @ItemCode
						AND OrderLines.ColorCode			= @ColorCode
						AND OrderLines.ItemDim1Code		= @ItemDim1Code
						AND OrderLines.ItemDim2Code		= @ItemDim2Code
						AND OrderLines.ItemDim3Code		= @ItemDim3Code
						-- AND OrderLines.IsClosed			= 0
						AND @ProcessFlowCode IN(3,4,5,6,7)
				GROUP BY trOrderHeader.CurrAccTypeCode, trOrderHeader.CurrAccCode, trOrderHeader.SubCurrAccID,OrderLines.OrderLineID
				UNION ALL
				SELECT		  InventoryQty1								= 0
							, BalanceReserveQty1						= 0
							, BalanceReserveQty1Customer				= 0
							, BalanceReserveQty1ForOpenOrder			= 0
							, BalanceReserveQty1CustomerForOpenOrder	= 0
							
							, BalanceDispOrderQty1						= 0
							, BalanceDispOrderQty1Customer				= 0
							, BalancePickingQty1						= ISNULL(SUM(stPicking.Qty1 * (CASE WHEN PickHead.CurrAccTypeCode = @CurrAccTypeCode AND PickHead.CurrAccCode = @CurrAccCode AND ISNULL(PickHead.SubCurrAccID, '00000000-0000-0000-0000-000000000000') = ISNULL(@SubCurrAccID, '00000000-0000-0000-0000-000000000000') THEN 0 ELSE 1 END)), 0)
							, BalancePickingQty1Customer				= ISNULL(SUM(stPicking.Qty1 * (CASE WHEN PickHead.CurrAccTypeCode = @CurrAccTypeCode AND PickHead.CurrAccCode = @CurrAccCode AND ISNULL(PickHead.SubCurrAccID, '00000000-0000-0000-0000-000000000000') = ISNULL(@SubCurrAccID, '00000000-0000-0000-0000-000000000000') THEN 1 ELSE 0 END)), 0)
				
				FROM trOrderLine OrderLines WITH (NOLOCK)
					INNER JOIN trOrderHeader WITH (NOLOCK)
						ON OrderLines.OrderHeaderID = trOrderHeader.OrderHeaderID
							AND trOrderHeader.OrderTypeCode = 1
					LEFT OUTER JOIN stPicking WITH (NOLOCK)
						ON stPicking.OrderLineID = OrderLines.OrderLineID
					LEFT JOIN trPickingLine PickLine WITH (NOLOCK)
						ON stPicking.PickingLineID = PickLine.PickingLineID
					LEFT OUTER JOIN trPickingHeader PickHead WITH (NOLOCK)
						ON PickHead.PickingHeaderID = PickLine.PickingHeaderID
				WHERE PickHead.WarehouseCode			= @WarehouseCode
						AND OrderLines.ItemTypeCode		= @ItemTypeCode
						AND OrderLines.ItemCode			= @ItemCode
						AND OrderLines.ColorCode			= @ColorCode
						AND OrderLines.ItemDim1Code		= @ItemDim1Code
						AND OrderLines.ItemDim2Code		= @ItemDim2Code
						AND OrderLines.ItemDim3Code		= @ItemDim3Code
						--AND OrderLines.IsClosed			= 0
						AND @ProcessFlowCode				IN(3,4,5,6,7)
				GROUP BY trOrderHeader.CurrAccTypeCode, trOrderHeader.CurrAccCode, trOrderHeader.SubCurrAccID,OrderLines.OrderLineID
			) 
			AS TB
		) 
		AS Inventory
	END

	IF (@UseBatch = 1 OR @UseSection = 1) 
	BEGIN
		SELECT @AvailableInventoryQty1 = 	
			CASE  
				WHEN @ProcessFlowCode = 5	  THEN SUM(InventoryQty1 - (BalancePickingQty1 + BalancePickingQty1Customer + SectionTransferQty1)) 
				WHEN @ProcessFlowCode IN(6,7) THEN SUM(InventoryQty1 - (BalancePickingQty1 + SectionTransferQty1)) 
				ELSE							   SUM(InventoryQty1)
			END	
		FROM
		(	
			SELECT 
				  InventoryQty1				= SUM(In_Qty1 - Out_Qty1)
				, BalancePickingQty1		= 0
				, BalancePickingQty1Customer= 0
				, SectionTransferQty1		= 0
			FROM trStock WITH(NOLOCK)
			WHERE trStock.WarehouseCode					= @WarehouseCode 
			  AND ISNULL(trStock.SectionCode, SPACE(0)) = @SectionCode
 			  AND trStock.ItemTypeCode					= @ItemTypeCode 
			  AND trStock.ItemCode						= @ItemCode 
			  AND trStock.ColorCode						= @ColorCode 
			  AND trStock.ItemDim1Code					= @ItemDim1Code 
			  AND trStock.ItemDim2Code					= @ItemDim2Code 
			  AND trStock.ItemDim3Code					= @ItemDim3Code
			  AND ISNULL(trStock.BatchCode, SPACE(0))	= @BatchCode
			UNION ALL
			SELECT 
				  InventoryQty1				= 0
				, BalancePickingQty1		= ISNULL(SUM(Pick.Qty1 * (CASE WHEN Pick.CurrAccTypeCode = @CurrAccTypeCode AND Pick.CurrAccCode = @CurrAccCode AND ISNULL(Pick.SubCurrAccID, dbo.fn_GetNullUniqueidentifier()) = ISNULL(@SubCurrAccID, dbo.fn_GetNullUniqueidentifier()) THEN 0 ELSE 1 END)), 0)
				, BalancePickingQty1Customer= ISNULL(SUM(Pick.Qty1 * (CASE WHEN Pick.CurrAccTypeCode = @CurrAccTypeCode AND Pick.CurrAccCode = @CurrAccCode AND ISNULL(Pick.SubCurrAccID, dbo.fn_GetNullUniqueidentifier()) = ISNULL(@SubCurrAccID, dbo.fn_GetNullUniqueidentifier()) THEN 1 ELSE 0 END)), 0)
				, SectionTransferQty1		= 0
			FROM 
			(
				SELECT AllPickingLines.CurrAccTypeCode, AllPickingLines.CurrAccCode, AllPickingLines.SubCurrAccID, Qty1 = SUM(stPicking.Qty1) 
				FROM AllPickingLines, stPicking WITH(NOLOCK) 
				WHERE AllPickingLines.PickingLineID					= stPicking.PickingLineID	 
				  AND AllPickingLines.WarehouseCode					= @WarehouseCode
				  AND ISNULL(AllPickingLines.SectionCode, SPACE(0))	= @SectionCode
				  AND AllPickingLines.ItemTypeCode					= @ItemTypeCode 
				  AND AllPickingLines.ItemCode						= @ItemCode 
				  AND AllPickingLines.ColorCode						= @ColorCode 
				  AND AllPickingLines.ItemDim1Code					= @ItemDim1Code 
				  AND AllPickingLines.ItemDim2Code					= @ItemDim2Code 
				  AND AllPickingLines.ItemDim3Code					= @ItemDim3Code
				  AND ISNULL(AllPickingLines.BatchCode, SPACE(0))	= @BatchCode
				  AND @ProcessFlowCode								IN(5,6,7)
				GROUP BY AllPickingLines.CurrAccTypeCode, AllPickingLines.CurrAccCode, AllPickingLines.SubCurrAccID
			)
			AS Pick
			UNION ALL
			SELECT 
				  InventoryQty1				= 0
				, BalancePickingQty1		= 0
				, BalancePickingQty1Customer= 0
				, SectionTransferQty1		= SUM(stInnerOrder.Qty1)
			FROM trInnerOrderLine WITH(NOLOCK)
				INNER JOIN stInnerOrder WITH(NOLOCK)
					ON stInnerOrder.InnerOrderLineID = trInnerOrderLine.InnerOrderLineID
					AND stInnerOrder.Qty1 > 0
				INNER JOIN trInnerOrderHeader WITH(NOLOCK)
					ON trInnerOrderHeader.InnerOrderHeaderID = trInnerOrderLine.InnerOrderHeaderID
					AND trInnerOrderHeader.InnerProcessCode  = N'WT'
					AND trInnerOrderHeader.WarehouseCode = @WarehouseCode
					AND trInnerOrderHeader.IsSectionTransfer = 1
			WHERE @SectionCode <> SPACE(0) 
			AND trInnerOrderLine.InnerOrderLineID <> @InnerOrderLineID
			AND SectionCode = @SectionCode 
			AND ItemTypeCode = @ItemTypeCode AND ItemCode = @ItemCode AND ColorCode = @ColorCode AND ItemDim1Code= @ItemDim1Code AND ItemDim2Code= @ItemDim2Code AND ItemDim3Code = @ItemDim3Code 
		) 
		AS Inventory
	END

	SET @AvailableInventoryQty1 = ISNULL(@AvailableInventoryQty1, 0)
