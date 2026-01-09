Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Public Class MSIProductionSaleReport
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim Slot1FD As DateTime = Nothing
    Dim Slot1TD As DateTime = Nothing
    Dim Slot2FD As DateTime = Nothing
    Dim Slot2TD As DateTime = Nothing
    Dim Slot3FD As DateTime = Nothing
    Dim Slot3TD As DateTime = Nothing
    Dim arrLoc As String = Nothing
    Dim Loc_Desc_Code As New StringBuilder()
    Dim Loc_Desc_Name As New StringBuilder()
#End Region

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        FromDate.Value = clsCommon.GETSERVERDATE()
        txtLocation.Value = Nothing
        lblLocation.Text = ""
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            Dim dt As DataTable = Nothing

            If clsCommon.myLen(txtLocation.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Plz Select Location.", Me.Text)
                txtLocation.Focus()
                Exit Sub
            End If

            Dim whr As String = ""
            If txtLocation.Value IsNot Nothing AndAlso txtLocation.Value.Count > 0 Then
                whr += " and LOCATION_CODE IN ('" + clsCommon.myCstr(txtLocation.Value) + "') "
            Else
                'whr += " and TSPL_LOCATION_MASTER.Location_Type='Physical'  "
                'If clsCommon.myLen(arrLoc) > 0 Then
                'whr += "  and  LOCATION_CODE IN (" + clsCommon.GetMulcallStringWithComma(TxtMultiLocation.arrValueMember) + ") "
                'End If
            End If
            Dim Status As String = ""
            Dim Status1 As String = ""
            Dim FG As String = ""
            Dim SFG As String = ""
            Dim FGSFG As String = ""
            Dim StatusInvoice As String = ""
            Dim StatusReturn As String = ""
            Dim Stocktransferdispatch As String = ""
            Dim stocktransferinvoice As String = ""
            Dim statusScrap As String = ""
            Dim statusScrapInvoice As String = ""
            If rdbPosted.IsChecked = True Then
                Status = " AND TSPL_SD_SHIPMENT_HEAD.Status=1 "
                Status1 = " AND TSPL_SPP_PRODUCTION_ENTRY.posted=1 "
                StatusInvoice = " AND TSPL_SD_SALE_INVOICE_HEAD.Status=1 "
                StatusReturn = " AND TSPL_SD_SALE_RETURN_HEAD.Status=1 "
                statusScrap = " AND TSPL_SCRAPSALE_HEAD.ispost=1 "
                statusScrapInvoice = " AND TSPL_SCRAPINVOICE_HEAD.ispost=0 "
            ElseIf rdbUnposted.IsChecked = True Then
                Status = " AND TSPL_SD_SHIPMENT_HEAD.Status=0 "
                Status1 = " AND TSPL_SPP_PRODUCTION_ENTRY.posted=0 "
                StatusInvoice = " AND TSPL_SD_SALE_INVOICE_HEAD.Status=0 "
                StatusReturn = " AND TSPL_SD_SALE_RETURN_HEAD.Status=0 "
                statusScrap = " AND TSPL_SCRAPSALE_HEAD.ispost=0 "
                statusScrapInvoice = " AND TSPL_SCRAPINVOICE_HEAD.ispost=0 "
            ElseIf rdbAll.IsChecked = True Then

            End If

            If rdbStockTransfer.IsChecked = True Then
                Stocktransferdispatch = " and TSPL_SD_SHIPMENT_HEAD.Inter_unit_sale=1 "
                stocktransferinvoice = " and TSPL_SD_SALE_INVOICE_HEAD.Inter_unit_sale=1 "
            End If
            If rdbSaleTransfer.IsChecked = True Then
                Stocktransferdispatch = " and TSPL_SD_SHIPMENT_HEAD.Inter_unit_sale=0 "
                stocktransferinvoice = " and TSPL_SD_SALE_INVOICE_HEAD.Inter_unit_sale=0 "
            End If

            If rdbFG.IsChecked = True Then
                FG = " and TSPL_Item_Master.FG_for_CF_RPT=1 "
            ElseIf rdbSFG.IsChecked = True Then
                SFG = " and TSPL_Item_Master.SFG_for_CF=1 "
            ElseIf rdbfgsfg.IsChecked = True Then
                FGSFG = " and TSPL_Item_Master.FG_for_CF=1 "
            End If
            Dim DailySalesrptqry As String = ""
            Dim DailySalesrptperiodicallyqry As String = ""
            If rdbDispatch.IsChecked = True AndAlso rdbSaleTransfer.IsChecked = True Then
                DailySalesrptqry = "select sum(isnull(yy.qty,0)) qty from (
                                    Select xx.DocDate,(XX.SaleQty-XX.ReturnQty)Qty from
                                    (Select max(DocDate)DocDate,Sum(SaleQty)SaleQty,sum(ReturnQty)ReturnQty,Bill_To_Location from
                                    (select max(TSPL_SD_SHIPMENT_HEAD.Document_Date) as DocDate,sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SD_SHIPMENT_DETAIL.Qty)/max(ToUOM.Conversion_Factor) as SaleQty,0 as ReturnQty,max(TSPL_SD_SHIPMENT_HEAD.Bill_To_Location)Bill_To_Location
                                          from TSPL_SD_SHIPMENT_DETAIL
                                          left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code =TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE
                                          left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code
                                          left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code
                                          LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code 
                                          AND TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_SD_SHIPMENT_DETAIL.Unit_code
                                          left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SD_SHIPMENT_DETAIL.item_code and ToUOM.UOM_Code='MT'
                                          where convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103)=convert(date,'" + clsCommon.GetPrintDate(FromDate.Value) + "',103) 
                                          AND Location IN ('" + clsCommon.myCstr(txtLocation.Value) + "') "
                DailySalesrptqry += " " + FG + " " + SFG + " " + FGSFG + " " + Status + "  "
                DailySalesrptqry += "    union all
										  SELECT max(TSPL_SCRAPSALE_HEAD.shipment_Date)DocDate,SUM((isnull(TSPL_SCRAPSALE_DETAIL.shipped_Qty,0)*FromUOM.Conversion_Factor)/ToUOM.Conversion_Factor) AS Qty,0 as ReturnQty,max(TSPL_SCRAPSALE_HEAD.Loc_Code) as Bill_To_Location  FROM 
                        TSPL_SCRAPSALE_DETAIL left join 
                        TSPL_SCRAPSALE_HEAD on TSPL_SCRAPSALE_HEAD.shipment_No=TSPL_SCRAPSALE_DETAIL.shipment_No
                        LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SCRAPSALE_DETAIL.ITEM_CODE
                         left outer join TSPL_ITEM_UOM_DETAIL FromUOM on FromUOM.Item_Code =TSPL_SCRAPSALE_DETAIL.Item_Code 
						AND FromUOM.UOM_Code=TSPL_SCRAPSALE_DETAIL.Unit_code
						left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SCRAPSALE_DETAIL.item_code and ToUOM.UOM_Code='MT'
                        WHERE convert(date,TSPL_SCRAPSALE_HEAD.shipment_Date,103)=convert(date,'" + clsCommon.GetPrintDate(FromDate.Value) + "',103) 
                                          AND Loc_Code IN ('" + clsCommon.myCstr(txtLocation.Value) + "') "
                DailySalesrptqry += " " + FG + " " + SFG + " " + FGSFG + " " + statusScrap + "  "
                DailySalesrptqry += " )XX Group by XX.Bill_To_Location "

                DailySalesrptqry += " UNION ALL
										    select max(TSPL_SD_SALE_RETURN_HEAD.Document_Date) as DocDate,0 as SaleQty,sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SD_SALE_RETURN_DETAIL.Qty)/max(ToUOM.Conversion_Factor) as ReturnQty,max(TSPL_SD_SALE_RETURN_HEAD.Bill_To_Location) as Bill_To_Location
                                          from TSPL_SD_SALE_RETURN_DETAIL
                                          left outer join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Document_Code =TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE
                                          left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_RETURN_HEAD.Customer_Code
                                          left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code
                                          LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code 
                                          AND TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_SD_SALE_RETURN_DETAIL.Unit_code
                                          left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SD_SALE_RETURN_DETAIL.item_code and ToUOM.UOM_Code='MT'
                                          where convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103)=convert(date,'" + clsCommon.GetPrintDate(FromDate.Value) + "',103) 
                                          AND Location IN ('" + clsCommon.myCstr(txtLocation.Value) + "')"
                DailySalesrptqry += " " + FG + " " + SFG + " " + FGSFG + " " + StatusReturn + " "
                DailySalesrptqry += ")XX )yy"
            ElseIf rdbInvoice.IsChecked = True AndAlso rdbSaleTransfer.IsChecked = True Then
                DailySalesrptqry = "select sum(isnull(yy.qty,0)) qty from (
                                    Select xx.DocDate,(XX.SaleQty-XX.ReturnQty)Qty from
                                    (Select max(DocDate)DocDate,Sum(SaleQty)SaleQty,sum(ReturnQty)ReturnQty,Bill_To_Location from
                                    (select max(TSPL_SD_SALE_INVOICE_HEAD.Document_Date) as DocDate,sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SD_SALE_INVOICE_DETAIL.Qty)/max(ToUOM.Conversion_Factor) as SaleQty,0 as ReturnQty,max(TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location)Bill_To_Location
                                          from TSPL_SD_SALE_INVOICE_DETAIL
                                          left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE
                                          left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
                                          left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
                                          LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code 
                                          AND TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_SD_SALE_INVOICE_DETAIL.Unit_code
                                          left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SD_SALE_INVOICE_DETAIL.item_code and ToUOM.UOM_Code='MT'
                                          where convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)=convert(date,'" + clsCommon.GetPrintDate(FromDate.Value) + "',103) 
                                          AND Location IN ('" + clsCommon.myCstr(txtLocation.Value) + "')"
                DailySalesrptqry += " " + FG + " " + SFG + " " + FGSFG + " " + StatusInvoice + " "

                DailySalesrptqry += "    union all
										  SELECT max(TSPL_SCRAPINVOICE_HEAD.shipment_Date)shipment_Date,SUM((isnull(TSPL_SCRAPINVOICE_DETAIL.shipped_Qty,0)*FromUOM.Conversion_Factor)/ToUOM.Conversion_Factor) AS Qty,0 as ReturnQty,max(TSPL_SCRAPINVOICE_HEAD.Loc_Code) as Bill_To_Location  FROM 
                        TSPL_SCRAPINVOICE_DETAIL left join 
                        TSPL_SCRAPINVOICE_HEAD on TSPL_SCRAPINVOICE_HEAD.invoice_No=TSPL_SCRAPINVOICE_DETAIL.invoice_No
                        LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SCRAPINVOICE_DETAIL.ITEM_CODE
                         left outer join TSPL_ITEM_UOM_DETAIL FromUOM on FromUOM.Item_Code =TSPL_SCRAPINVOICE_DETAIL.Item_Code 
						AND FromUOM.UOM_Code=TSPL_SCRAPINVOICE_DETAIL.Unit_code
						left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SCRAPINVOICE_DETAIL.item_code and ToUOM.UOM_Code='MT'
                        WHERE convert(date,TSPL_SCRAPINVOICE_HEAD.shipment_Date,103)=convert(date,'" + clsCommon.GetPrintDate(FromDate.Value) + "',103) 
                                          AND Loc_Code IN ('" + clsCommon.myCstr(txtLocation.Value) + "') "
                DailySalesrptqry += " " + FG + " " + SFG + " " + FGSFG + " " + statusScrapInvoice + "  "
                DailySalesrptqry += " )XX Group by XX.Bill_To_Location "

                DailySalesrptqry += " UNION ALL
										    select max(TSPL_SD_SALE_RETURN_HEAD.Document_Date) as DocDate,0 as SaleQty,sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SD_SALE_RETURN_DETAIL.Qty)/max(ToUOM.Conversion_Factor) as ReturnQty,max(TSPL_SD_SALE_RETURN_HEAD.Bill_To_Location) as Bill_To_Location
                                          from TSPL_SD_SALE_RETURN_DETAIL
                                          left outer join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Document_Code =TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE
                                          left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_RETURN_HEAD.Customer_Code
                                          left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code
                                          LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code 
                                          AND TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_SD_SALE_RETURN_DETAIL.Unit_code
                                          left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SD_SALE_RETURN_DETAIL.item_code and ToUOM.UOM_Code='MT'
                                          where convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103)=convert(date,'" + clsCommon.GetPrintDate(FromDate.Value) + "',103) 
                                          AND Location IN ('" + clsCommon.myCstr(txtLocation.Value) + "')"
                DailySalesrptqry += " " + FG + " " + SFG + " " + FGSFG + " " + StatusReturn + " "
                DailySalesrptqry += ")XX )yy"

            ElseIf rdbDispatch.IsChecked = True Then
                DailySalesrptqry = " Select max(xx.DocDate)DocDate,sum(xx.Qty)Qty from (select max(TSPL_SD_SHIPMENT_HEAD.Document_Date) as DocDate,sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SD_SHIPMENT_DETAIL.Qty)/max(ToUOM.Conversion_Factor) as Qty,max(TSPL_SD_SHIPMENT_HEAD.Bill_To_Location) as Bill_To_Location
                                          from TSPL_SD_SHIPMENT_DETAIL
                                          left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code =TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE
                                          left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code
                                          left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code
                                          LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code 
                                          AND TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_SD_SHIPMENT_DETAIL.Unit_code
                                          left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SD_SHIPMENT_DETAIL.item_code and ToUOM.UOM_Code='MT'
				                          where convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103)=convert(date,'" + clsCommon.GetPrintDate(FromDate.Value) + "',103) 
                                          AND Location IN ('" + clsCommon.myCstr(txtLocation.Value) + "')  "
                DailySalesrptqry += " " + FG + " " + SFG + " " + FGSFG + " " + Status + " "
                If rdbDispatch.IsChecked = True AndAlso rdbSale.IsChecked = True Then
                    DailySalesrptqry += " and TSPL_SD_SHIPMENT_HEAD.Inter_unit_sale=0 "
                End If
                If rdbStockTransfer.IsChecked = True Then
                    DailySalesrptqry += "" + Stocktransferdispatch + ""
                End If
                DailySalesrptqry += " union all
										  SELECT max(TSPL_SCRAPSALE_HEAD.shipment_Date)shipment_Date,SUM((isnull(TSPL_SCRAPSALE_DETAIL.shipped_Qty,0)*FromUOM.Conversion_Factor)/ToUOM.Conversion_Factor) AS Qty,max(TSPL_SCRAPSALE_HEAD.Loc_Code) as Bill_To_Location  FROM 
                        TSPL_SCRAPSALE_DETAIL left join 
                        TSPL_SCRAPSALE_HEAD on TSPL_SCRAPSALE_HEAD.shipment_No=TSPL_SCRAPSALE_DETAIL.shipment_No
                        LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SCRAPSALE_DETAIL.ITEM_CODE
                         left outer join TSPL_ITEM_UOM_DETAIL FromUOM on FromUOM.Item_Code =TSPL_SCRAPSALE_DETAIL.Item_Code 
						AND FromUOM.UOM_Code=TSPL_SCRAPSALE_DETAIL.Unit_code
						left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SCRAPSALE_DETAIL.item_code and ToUOM.UOM_Code='MT'
                        WHERE convert(date,TSPL_SCRAPSALE_HEAD.shipment_Date,103)=convert(date,'" + clsCommon.GetPrintDate(FromDate.Value) + "',103) 
                                          AND Loc_Code IN ('" + clsCommon.myCstr(txtLocation.Value) + "')  "
                DailySalesrptqry += " " + FG + " " + SFG + " " + FGSFG + " " + statusScrap + " "
                If rdbDispatch.IsChecked = True AndAlso rdbSale.IsChecked = True Then
                    DailySalesrptqry += " and TSPL_SCRAPSALE_HEAD.Inter_unit_sale=0 "
                End If
                If rdbStockTransfer.IsChecked = True Then
                    DailySalesrptqry += " and TSPL_SCRAPSALE_HEAD.Inter_unit_sale=1 "
                End If
                DailySalesrptqry += "  ) XX group by Bill_To_Location "

            ElseIf rdbInvoice.IsChecked = True Then
                DailySalesrptqry = "  Select max(xx.DocDate)DocDate,sum(xx.Qty)Qty from (select max(TSPL_SD_SALE_INVOICE_HEAD.Document_Date) as DocDate,sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SD_SALE_INVOICE_DETAIL.Qty)/max(ToUOM.Conversion_Factor) as Qty,max(TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location)Bill_To_Location
                                          from TSPL_SD_SALE_INVOICE_DETAIL
                                          left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE
                                          left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
                                          left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
                                          LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code 
                                          AND TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_SD_SALE_INVOICE_DETAIL.Unit_code
                                          left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SD_SALE_INVOICE_DETAIL.item_code and ToUOM.UOM_Code='MT'
				                          where convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)=convert(date,'" + clsCommon.GetPrintDate(FromDate.Value) + "',103) 
                                          AND Location IN ('" + clsCommon.myCstr(txtLocation.Value) + "')  "
                DailySalesrptqry += " " + FG + " " + SFG + " " + FGSFG + " " + StatusInvoice + " "
                If rdbInvoice.IsChecked = True AndAlso rdbSale.IsChecked = True Then
                    DailySalesrptqry += " and TSPL_SD_SALE_INVOICE_HEAD.Inter_unit_sale=0 "
                End If
                If rdbStockTransfer.IsChecked = True Then
                    DailySalesrptqry += "" + stocktransferinvoice + ""
                End If

                DailySalesrptqry += " union all
                        SELECT max(TSPL_SCRAPINVOICE_HEAD.shipment_Date)shipment_Date,SUM((isnull(TSPL_SCRAPINVOICE_DETAIL.shipped_Qty,0)*FromUOM.Conversion_Factor)/ToUOM.Conversion_Factor) AS Qty,max(TSPL_SCRAPINVOICE_HEAD.Loc_Code) as Bill_To_Location  FROM 
                        TSPL_SCRAPINVOICE_DETAIL left join 
                        TSPL_SCRAPINVOICE_HEAD on TSPL_SCRAPINVOICE_HEAD.invoice_No=TSPL_SCRAPINVOICE_DETAIL.invoice_No
                        LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SCRAPINVOICE_DETAIL.ITEM_CODE
                         left outer join TSPL_ITEM_UOM_DETAIL FromUOM on FromUOM.Item_Code =TSPL_SCRAPINVOICE_DETAIL.Item_Code 
						AND FromUOM.UOM_Code=TSPL_SCRAPINVOICE_DETAIL.Unit_code
						left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SCRAPINVOICE_DETAIL.item_code and ToUOM.UOM_Code='MT'
                        WHERE convert(date,TSPL_SCRAPINVOICE_HEAD.shipment_Date,103)=convert(date,'" + clsCommon.GetPrintDate(FromDate.Value) + "',103) 
                                          AND Loc_Code IN ('" + clsCommon.myCstr(txtLocation.Value) + "')  "
                DailySalesrptqry += " " + FG + " " + SFG + " " + FGSFG + " " + statusScrapInvoice + " "
                'If rdbDispatch.IsChecked = True AndAlso rdbSale.IsChecked = True Then
                '    DailySalesrptqry += " and TSPL_SCRAPSALE_HEAD.Inter_unit_sale=0 "
                'End If
                'If rdbStockTransfer.IsChecked = True Then
                '    DailySalesrptqry += " and TSPL_SCRAPSALE_HEAD.Inter_unit_sale=1 "
                'End If
                DailySalesrptqry += "  ) XX group by Bill_To_Location "

            ElseIf rdbSaleReturn.IsChecked = True Then
                DailySalesrptqry = "select max(TSPL_SD_SALE_RETURN_HEAD.Document_Date) as DocDate,sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SD_SALE_RETURN_DETAIL.Qty)/max(ToUOM.Conversion_Factor) as Qty
                                          from TSPL_SD_SALE_RETURN_DETAIL
                                          left outer join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Document_Code =TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE
                                          left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_RETURN_HEAD.Customer_Code
                                          left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code
                                          LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code 
                                          AND TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_SD_SALE_RETURN_DETAIL.Unit_code
                                          left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SD_SALE_RETURN_DETAIL.item_code and ToUOM.UOM_Code='MT'
                                          where convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103)=convert(date,'" + clsCommon.GetPrintDate(FromDate.Value) + "',103) 
                                          AND Location IN ('" + clsCommon.myCstr(txtLocation.Value) + "')"
                DailySalesrptqry += " " + FG + " " + SFG + " " + FGSFG + " " + StatusReturn + " "
            End If


            'and FG_for_CF_RPT=1 "
            Dim dtsalesdaily As DataTable = clsDBFuncationality.GetDataTable(DailySalesrptqry)

            If rdbDispatch.IsChecked = True AndAlso rdbSaleTransfer.IsChecked = True Then
                DailySalesrptperiodicallyqry = "select sum(isnull(yy.qty,0)) qty from (
                                    Select xx.DocDate,(XX.SaleQty-XX.ReturnQty)Qty from
                                    (Select max(DocDate)DocDate,Sum(SaleQty)SaleQty,sum(ReturnQty)ReturnQty,Bill_To_Location from
                                    (select max(TSPL_SD_SHIPMENT_HEAD.Document_Date) as DocDate,sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SD_SHIPMENT_DETAIL.Qty)/max(ToUOM.Conversion_Factor) as SaleQty,0 as ReturnQty,max(TSPL_SD_SHIPMENT_HEAD.Bill_To_Location)Bill_To_Location
                                          from TSPL_SD_SHIPMENT_DETAIL
                                          left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code =TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE
                                          left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code
                                          left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code
                                          LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code 
                                          AND TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_SD_SHIPMENT_DETAIL.Unit_code
                                          left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SD_SHIPMENT_DETAIL.item_code and ToUOM.UOM_Code='MT'
                                          where convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(Slot1FD) + "',103) 
				                          and convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(FromDate.Value) + "',103)
                                          AND Location IN ('" + clsCommon.myCstr(txtLocation.Value) + "') "
                DailySalesrptperiodicallyqry += " " + FG + " " + SFG + " " + FGSFG + " " + Status + "  "
                DailySalesrptperiodicallyqry += "    union all
										  SELECT max(TSPL_SCRAPSALE_HEAD.shipment_Date)shipment_Date,SUM((isnull(TSPL_SCRAPSALE_DETAIL.shipped_Qty,0)*FromUOM.Conversion_Factor)/ToUOM.Conversion_Factor) AS Qty,0 as ReturnQty,max(TSPL_SCRAPSALE_HEAD.Loc_Code) as Bill_To_Location  FROM 
                        TSPL_SCRAPSALE_DETAIL left join 
                        TSPL_SCRAPSALE_HEAD on TSPL_SCRAPSALE_HEAD.shipment_No=TSPL_SCRAPSALE_DETAIL.shipment_No
                        LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SCRAPSALE_DETAIL.ITEM_CODE
                         left outer join TSPL_ITEM_UOM_DETAIL FromUOM on FromUOM.Item_Code =TSPL_SCRAPSALE_DETAIL.Item_Code 
						AND FromUOM.UOM_Code=TSPL_SCRAPSALE_DETAIL.Unit_code
						left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SCRAPSALE_DETAIL.item_code and ToUOM.UOM_Code='MT'
                        WHERE convert(date,TSPL_SCRAPSALE_HEAD.shipment_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(Slot1FD) + "',103) 
				                          and convert(date,TSPL_SCRAPSALE_HEAD.shipment_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(FromDate.Value) + "',103)
                                          AND Loc_Code IN ('" + clsCommon.myCstr(txtLocation.Value) + "') "
                DailySalesrptperiodicallyqry += " " + FG + " " + SFG + " " + FGSFG + " " + statusScrap + "  "
                DailySalesrptperiodicallyqry += " )XX Group by XX.Bill_To_Location "

                DailySalesrptperiodicallyqry += " UNION ALL
										    select max(TSPL_SD_SALE_RETURN_HEAD.Document_Date) as DocDate,0 as SaleQty,sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SD_SALE_RETURN_DETAIL.Qty)/max(ToUOM.Conversion_Factor) as ReturnQty,max(TSPL_SD_SALE_RETURN_HEAD.Bill_To_Location)Bill_To_Location
                                          from TSPL_SD_SALE_RETURN_DETAIL
                                          left outer join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Document_Code =TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE
                                          left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_RETURN_HEAD.Customer_Code
                                          left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code
                                          LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code 
                                          AND TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_SD_SALE_RETURN_DETAIL.Unit_code
                                          left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SD_SALE_RETURN_DETAIL.item_code and ToUOM.UOM_Code='MT'
                                          where convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(Slot1FD) + "',103) 
				                          and convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(FromDate.Value) + "',103)
                                          AND Location IN ('" + clsCommon.myCstr(txtLocation.Value) + "')"
                DailySalesrptperiodicallyqry += " " + FG + " " + SFG + " " + FGSFG + " " + StatusReturn + " "
                DailySalesrptperiodicallyqry += ")XX )yy"


            ElseIf rdbInvoice.IsChecked = True AndAlso rdbSaleTransfer.IsChecked = True Then
                DailySalesrptperiodicallyqry = "select sum(isnull(yy.qty,0)) qty from (
                                    Select xx.DocDate,(XX.SaleQty-XX.ReturnQty)Qty from
                                    (Select max(DocDate)DocDate,Sum(SaleQty)SaleQty,sum(ReturnQty)ReturnQty,Bill_To_Location from
                                    (select max(TSPL_SD_SALE_INVOICE_HEAD.Document_Date) as DocDate,sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SD_SALE_INVOICE_DETAIL.Qty)/max(ToUOM.Conversion_Factor) as SaleQty,0 as ReturnQty,max(TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location)Bill_To_Location
                                          from TSPL_SD_SALE_INVOICE_DETAIL
                                          left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE
                                          left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
                                          left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
                                          LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code 
                                          AND TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_SD_SALE_INVOICE_DETAIL.Unit_code
                                          left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SD_SALE_INVOICE_DETAIL.item_code and ToUOM.UOM_Code='MT'
                                          where convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(Slot1FD) + "',103) 
				                          and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(FromDate.Value) + "',103)
                                         AND Location IN ('" + clsCommon.myCstr(txtLocation.Value) + "')"
                DailySalesrptperiodicallyqry += " " + FG + " " + SFG + " " + FGSFG + " " + StatusInvoice + " "

                DailySalesrptperiodicallyqry += "    union all
										  SELECT max(TSPL_SCRAPINVOICE_HEAD.shipment_Date)shipment_Date,SUM((isnull(TSPL_SCRAPINVOICE_DETAIL.shipped_Qty,0)*FromUOM.Conversion_Factor)/ToUOM.Conversion_Factor) AS Qty,0 as ReturnQty,max(TSPL_SCRAPINVOICE_HEAD.Loc_Code) as Bill_To_Location  FROM 
                        TSPL_SCRAPINVOICE_DETAIL left join 
                        TSPL_SCRAPINVOICE_HEAD on TSPL_SCRAPINVOICE_HEAD.invoice_No=TSPL_SCRAPINVOICE_DETAIL.invoice_No
                        LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SCRAPINVOICE_DETAIL.ITEM_CODE
                         left outer join TSPL_ITEM_UOM_DETAIL FromUOM on FromUOM.Item_Code =TSPL_SCRAPINVOICE_DETAIL.Item_Code 
						AND FromUOM.UOM_Code=TSPL_SCRAPINVOICE_DETAIL.Unit_code
						left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SCRAPINVOICE_DETAIL.item_code and ToUOM.UOM_Code='MT'
                        WHERE convert(date,TSPL_SCRAPINVOICE_HEAD.shipment_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(Slot1FD) + "',103) 
                        and convert(date,TSPL_SCRAPINVOICE_HEAD.shipment_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(FromDate.Value) + "',103)
                         AND Loc_Code IN ('" + clsCommon.myCstr(txtLocation.Value) + "') "
                DailySalesrptperiodicallyqry += " " + FG + " " + SFG + " " + FGSFG + " " + statusScrapInvoice + "  "
                DailySalesrptperiodicallyqry += " )XX Group by XX.Bill_To_Location "

                DailySalesrptperiodicallyqry += " UNION ALL
										    select max(TSPL_SD_SALE_RETURN_HEAD.Document_Date) as DocDate,0 as SaleQty,sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SD_SALE_RETURN_DETAIL.Qty)/max(ToUOM.Conversion_Factor) as ReturnQty,max(TSPL_SD_SALE_RETURN_HEAD.Bill_To_Location) as Bill_To_Location 
                                          from TSPL_SD_SALE_RETURN_DETAIL
                                          left outer join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Document_Code =TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE
                                          left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_RETURN_HEAD.Customer_Code
                                          left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code
                                          LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code 
                                          AND TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_SD_SALE_RETURN_DETAIL.Unit_code
                                          left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SD_SALE_RETURN_DETAIL.item_code and ToUOM.UOM_Code='MT'
                                          where convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(Slot1FD) + "',103) 
				                          and convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(FromDate.Value) + "',103)
                                         AND Location IN ('" + clsCommon.myCstr(txtLocation.Value) + "')"
                DailySalesrptperiodicallyqry += " " + FG + " " + SFG + " " + FGSFG + " " + StatusReturn + " "
                DailySalesrptperiodicallyqry += ")XX )yy"

            ElseIf rdbDispatch.IsChecked = True Then
                DailySalesrptperiodicallyqry = " Select max(xx.DocDate)DocDate,sum(xx.Qty)Qty from(select max(TSPL_SD_SHIPMENT_HEAD.Document_Date) as DocDate,sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SD_SHIPMENT_DETAIL.Qty)/max(ToUOM.Conversion_Factor) as Qty,max(TSPL_SD_SHIPMENT_HEAD.Bill_To_Location) as Bill_To_Location
                                          from TSPL_SD_SHIPMENT_DETAIL
                                          left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code =TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE
                                          left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code
                                          left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code
                                          LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code 
                                          AND TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_SD_SHIPMENT_DETAIL.Unit_code
                                          left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SD_SHIPMENT_DETAIL.item_code and ToUOM.UOM_Code='MT'
				                          where convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(Slot1FD) + "',103) 
				                          and convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(FromDate.Value) + "',103)
                                          AND Location IN ('" + clsCommon.myCstr(txtLocation.Value) + "')    "
                DailySalesrptperiodicallyqry += " " + FG + " " + SFG + " " + FGSFG + " " + Status + " "
                If rdbDispatch.IsChecked = True AndAlso rdbSale.IsChecked = True Then
                    DailySalesrptqry += " and TSPL_SD_SHIPMENT_HEAD.Inter_unit_sale=0 "
                End If
                If rdbStockTransfer.IsChecked = True Then
                    DailySalesrptperiodicallyqry += "" + Stocktransferdispatch + ""
                End If
                DailySalesrptperiodicallyqry += "   union all
										  SELECT max(TSPL_SCRAPSALE_HEAD.shipment_Date)shipment_Date,SUM((isnull(TSPL_SCRAPSALE_DETAIL.shipped_Qty,0)*FromUOM.Conversion_Factor)/ToUOM.Conversion_Factor) AS Qty,max(TSPL_SCRAPSALE_HEAD.Loc_Code) as Bill_To_Location  FROM 
                        TSPL_SCRAPSALE_DETAIL left join 
                        TSPL_SCRAPSALE_HEAD on TSPL_SCRAPSALE_HEAD.shipment_No=TSPL_SCRAPSALE_DETAIL.shipment_No
                        LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SCRAPSALE_DETAIL.ITEM_CODE
                         left outer join TSPL_ITEM_UOM_DETAIL FromUOM on FromUOM.Item_Code =TSPL_SCRAPSALE_DETAIL.Item_Code 
						AND FromUOM.UOM_Code=TSPL_SCRAPSALE_DETAIL.Unit_code
						left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SCRAPSALE_DETAIL.item_code and ToUOM.UOM_Code='MT'
                        WHERE convert(date,TSPL_SCRAPSALE_HEAD.shipment_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(Slot1FD) + "',103) 
				                          and convert(date,TSPL_SCRAPSALE_HEAD.shipment_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(FromDate.Value) + "',103)
                                          AND Loc_Code IN ('" + clsCommon.myCstr(txtLocation.Value) + "')   "
                DailySalesrptperiodicallyqry += " " + FG + " " + SFG + " " + FGSFG + " " + statusScrap + " "
                If rdbDispatch.IsChecked = True AndAlso rdbSale.IsChecked = True Then
                    DailySalesrptqry += " and TSPL_SCRAPSALE_HEAD.Inter_unit_sale=0 "
                End If
                If rdbStockTransfer.IsChecked = True Then
                    DailySalesrptperiodicallyqry += " and TSPL_SCRAPSALE_HEAD.Inter_unit_sale=1 "
                End If
                DailySalesrptperiodicallyqry += " ) XX group by Bill_To_Location "
            ElseIf rdbInvoice.IsChecked = True Then
                DailySalesrptperiodicallyqry = " Select max(xx.DocDate)DocDate,sum(xx.Qty)Qty from (select max(TSPL_SD_SALE_INVOICE_HEAD.Document_Date) as DocDate,sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SD_SALE_INVOICE_DETAIL.Qty)/max(ToUOM.Conversion_Factor) as Qty,max(TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location)Bill_To_Location
                                          from TSPL_SD_SALE_INVOICE_DETAIL
                                          left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE
                                          left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
                                          left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
                                          LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code 
                                          AND TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_SD_SALE_INVOICE_DETAIL.Unit_code
                                          left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SD_SALE_INVOICE_DETAIL.item_code and ToUOM.UOM_Code='MT'
				                          where convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(Slot1FD) + "',103) 
				                          and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(FromDate.Value) + "',103)
                                          AND Location IN ('" + clsCommon.myCstr(txtLocation.Value) + "')  "
                DailySalesrptperiodicallyqry += " " + FG + " " + SFG + " " + FGSFG + " " + StatusInvoice + " "
                If rdbInvoice.IsChecked = True AndAlso rdbSale.IsChecked = True Then
                    DailySalesrptqry += " and TSPL_SD_SALE_INVOICE_HEAD.Inter_unit_sale=0 "
                End If
                If rdbStockTransfer.IsChecked = True Then
                    DailySalesrptperiodicallyqry += "" + stocktransferinvoice + ""
                End If

                DailySalesrptperiodicallyqry += "   union all
										  SELECT max(TSPL_SCRAPINVOICE_HEAD.shipment_Date)shipment_Date,SUM((isnull(TSPL_SCRAPINVOICE_DETAIL.shipped_Qty,0)*FromUOM.Conversion_Factor)/ToUOM.Conversion_Factor) AS Qty,max(TSPL_SCRAPINVOICE_HEAD.Loc_Code) as Bill_To_Location  FROM 
                        TSPL_SCRAPINVOICE_DETAIL left join 
                        TSPL_SCRAPINVOICE_HEAD on TSPL_SCRAPINVOICE_HEAD.invoice_No=TSPL_SCRAPINVOICE_DETAIL.invoice_No
                        LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SCRAPINVOICE_DETAIL.ITEM_CODE
                         left outer join TSPL_ITEM_UOM_DETAIL FromUOM on FromUOM.Item_Code =TSPL_SCRAPINVOICE_DETAIL.Item_Code 
						AND FromUOM.UOM_Code=TSPL_SCRAPINVOICE_DETAIL.Unit_code
						left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SCRAPINVOICE_DETAIL.item_code and ToUOM.UOM_Code='MT'
                        WHERE convert(date,TSPL_SCRAPINVOICE_HEAD.shipment_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(Slot1FD) + "',103) 
				                          and convert(date,TSPL_SCRAPINVOICE_HEAD.shipment_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(FromDate.Value) + "',103)
                                          AND Loc_Code IN ('" + clsCommon.myCstr(txtLocation.Value) + "')   "
                DailySalesrptperiodicallyqry += " " + FG + " " + SFG + " " + FGSFG + " " + statusScrapInvoice + " "
                'If rdbDispatch.IsChecked = True AndAlso rdbSale.IsChecked = True Then
                '    DailySalesrptqry += " and TSPL_SCRAPSALE_HEAD.Inter_unit_sale=0 "
                'End If
                'If rdbStockTransfer.IsChecked = True Then
                '    DailySalesrptperiodicallyqry += " and TSPL_SCRAPSALE_HEAD.Inter_unit_sale=1 "
                'End If
                DailySalesrptperiodicallyqry += " ) XX group by Bill_To_Location "

            ElseIf rdbSaleReturn.IsChecked = True Then
                DailySalesrptperiodicallyqry = "select max(TSPL_SD_SALE_RETURN_HEAD.Document_Date) as DocDate,sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SD_SALE_RETURN_DETAIL.Qty)/max(ToUOM.Conversion_Factor) as Qty
                                          from TSPL_SD_SALE_RETURN_DETAIL
                                          left outer join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Document_Code =TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE
                                          left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_RETURN_HEAD.Customer_Code
                                          left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code
                                          LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code 
                                          AND TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_SD_SALE_RETURN_DETAIL.Unit_code
                                          left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SD_SALE_RETURN_DETAIL.item_code and ToUOM.UOM_Code='MT'
                                          where convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(Slot1FD) + "',103) 
				                          and convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(FromDate.Value) + "',103)
                                          AND Location IN ('" + clsCommon.myCstr(txtLocation.Value) + "')"
                DailySalesrptperiodicallyqry += " " + FG + " " + SFG + " " + FGSFG + " " + StatusReturn + " "
            End If

            Dim dtsalesperiodically As DataTable = clsDBFuncationality.GetDataTable(DailySalesrptperiodicallyqry)

            Dim Inventoryreport As String = "select SUM(CASE WHEN INOUT='I' THEN 1 ELSE -1 END * stock_Qty)/1000 AS QTY from tspl_inventory_movement
                                         INNER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=tspl_inventory_movement.Item_Code 
                                         where  FG_for_CF_RPT=1 AND Location_Code IN ('" + clsCommon.myCstr(txtLocation.Value) + "') AND convert(date,tspl_inventory_movement.Source_Doc_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(FromDate.Value) + "',103) "

            Dim dtinventory As DataTable = clsDBFuncationality.GetDataTable(Inventoryreport)

            Dim ShiftOperated As String = "SELECT COUNT(DISTINCT Shift_Code) as ShiftOperated FROM TSPL_SPP_PRODUCTION_ENTRY 
                                       WHERE convert(date,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103)=convert(date,'" + clsCommon.GetPrintDate(FromDate.Value) + "',103) AND LOCATION_CODE IN ('" + clsCommon.myCstr(txtLocation.Value) + "')"

            Dim dtshift As DataTable = clsDBFuncationality.GetDataTable(ShiftOperated)

            Dim Productionrptdaily As String = " Select Sum(XX.Qty)Qty FROM ( SELECT "
            If Productionchk.IsChecked = True Then
                'Productionrptdaily += " SUM(FINAL_PRODUCTION_QTY)/1000 AS QTY "
                Productionrptdaily += " SUM(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*FINAL_PRODUCTION_QTY/TSPL_ITEM_UOM_MT.Conversion_Factor) AS QTY "
            ElseIf RePrdntchk.IsChecked = True Then
                'Productionrptdaily += " SUM(FINAL_PRODUCTION_QTY-Reprocess_Qty)/1000 AS QTY "
                Productionrptdaily += " SUM(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*(FINAL_PRODUCTION_QTY-isnull(Reprocess_Qty,0))/TSPL_ITEM_UOM_MT.Conversion_Factor) AS QTY "
            ElseIf Prdncreallchk.IsChecked = True Then
                'Productionrptdaily += " SUM(Reprocess_Qty)/1000 AS QTY "
                Productionrptdaily += " SUM(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*isnull(Reprocess_Qty,0)/TSPL_ITEM_UOM_MT.Conversion_Factor) AS QTY "
            End If
            Productionrptdaily += "  FROM TSPL_SPP_PRODUCTION_ENTRY_DETAIL 
                                         LEFT OUTER JOIN TSPL_SPP_PRODUCTION_ENTRY ON TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE
                                         LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.Item_Code
                                         LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.Item_Code 
                                
AND TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_SPP_PRODUCTION_ENTRY_DETAIL.Unit_code
						                  LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_MT ON  TSPL_ITEM_UOM_MT.Item_Code=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.Item_Code 
                                          AND TSPL_ITEM_UOM_MT.UOM_Code= 'MT'
                                         WHERE convert(date,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103)=convert(date,'" + clsCommon.GetPrintDate(FromDate.Value) + "',103) AND TSPL_SPP_PRODUCTION_ENTRY_DETAIL.LOCATION_CODE IN ('" + clsCommon.myCstr(txtLocation.Value) + "')  AND FG_for_CF_RPT=1

union all
									 Select sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_ADJUSTMENT_DETAIL.Item_Quantity/TSPL_ITEM_UOM_MT.Conversion_Factor) as Qty  from TSPL_ADJUSTMENT_DETAIL
						  left join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.Adjustment_No=TSPL_ADJUSTMENT_DETAIL.Adjustment_No
						  LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_ADJUSTMENT_DETAIL.ITEM_CODE
                          LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_ADJUSTMENT_DETAIL.Item_Code 
                          AND TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_ADJUSTMENT_DETAIL.Unit_code
						  LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_MT ON  TSPL_ITEM_UOM_MT.Item_Code=TSPL_ADJUSTMENT_DETAIL.Item_Code 
                          AND TSPL_ITEM_UOM_MT.UOM_Code= 'MT' 
						  where    TSPL_Item_Master.FG_for_CF_RPT=1  and convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103)=convert(date,'" + clsCommon.GetPrintDate(FromDate.Value) + "',103)
                          and TSPL_ADJUSTMENT_HEADER.Loc_Code In ('" + clsCommon.myCstr(txtLocation.Value) + "') )XX
"

            Dim dtproductiondaily As DataTable = clsDBFuncationality.GetDataTable(Productionrptdaily)

            Dim Productionrptperiodically As String = " Select Sum(XX.Qty)Qty FROM ( SELECT "

            If Productionchk.IsChecked = True Then
                'Productionrptperiodically += " SUM(FINAL_PRODUCTION_QTY)/1000 AS QTY "
                Productionrptperiodically += " SUM(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*FINAL_PRODUCTION_QTY/TSPL_ITEM_UOM_MT.Conversion_Factor) AS QTY "
            ElseIf RePrdntchk.IsChecked = True Then
                'Productionrptperiodically += " SUM(FINAL_PRODUCTION_QTY-Reprocess_Qty)/1000 AS QTY "
                Productionrptperiodically += " SUM(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*(FINAL_PRODUCTION_QTY-isnull(Reprocess_Qty,0))/TSPL_ITEM_UOM_MT.Conversion_Factor) AS QTY "
            ElseIf Prdncreallchk.IsChecked = True Then
                'Productionrptperiodically += " SUM(Reprocess_Qty)/1000 AS QTY "
                Productionrptperiodically += " SUM(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*isnull(Reprocess_Qty,0)/TSPL_ITEM_UOM_MT.Conversion_Factor) AS QTY "
            End If

            Productionrptperiodically += " FROM TSPL_SPP_PRODUCTION_ENTRY_DETAIL 
                                            LEFT OUTER JOIN TSPL_SPP_PRODUCTION_ENTRY ON TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE
                                            LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.Item_Code
                                            LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.Item_Code 
                                          AND TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_SPP_PRODUCTION_ENTRY_DETAIL.Unit_code
						                  LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_MT ON  TSPL_ITEM_UOM_MT.Item_Code=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.Item_Code 
                                          AND TSPL_ITEM_UOM_MT.UOM_Code= 'MT'
                                            WHERE   FG_for_CF_RPT=1  AND convert(date,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103)>=convert(date,'" + clsCommon.GetPrintDate(Slot1FD) + "',103) 
                                            AND convert(date,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103)<=convert(date,'" + clsCommon.GetPrintDate(FromDate.Value) + "',103) 
                                            AND TSPL_SPP_PRODUCTION_ENTRY_DETAIL.LOCATION_CODE IN ('" + clsCommon.myCstr(txtLocation.Value) + "')
union all
									
						  Select sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_ADJUSTMENT_DETAIL.Item_Quantity/TSPL_ITEM_UOM_MT.Conversion_Factor) as Qty
						  from TSPL_ADJUSTMENT_DETAIL
						  left join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.Adjustment_No=TSPL_ADJUSTMENT_DETAIL.Adjustment_No
						  LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_ADJUSTMENT_DETAIL.ITEM_CODE
                          LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_ADJUSTMENT_DETAIL.Item_Code 
                          AND TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_ADJUSTMENT_DETAIL.Unit_code
						  LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_MT ON  TSPL_ITEM_UOM_MT.Item_Code=TSPL_ADJUSTMENT_DETAIL.Item_Code 
                          AND TSPL_ITEM_UOM_MT.UOM_Code= 'MT' 
						  where    TSPL_Item_Master.FG_for_CF_RPT=1  and convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(Slot1FD) + "',103)
                         and convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(FromDate.Value) + "',103)
						 and TSPL_ADJUSTMENT_HEADER.Loc_Code In ('" + clsCommon.myCstr(txtLocation.Value) + "') )XX "

            Dim dtproductionperiodically As DataTable = clsDBFuncationality.GetDataTable(Productionrptperiodically)

            Dim Shortitemqry As String = "SELECT '" + clsCommon.GetPrintDate(FromDate.Value) + "' as Date, FINAL.Location_Code,FINAL.Location_Desc,FINAL.Add1,FINAL.Add2,FINAL.Add3,FINAL.Add4,FINAL.ITEM_CODE, FINAL.Item_Desc,FINAL.Unit,FINAL.STOCK_QTY,FINAL.QTY_FOR_DAYS FROM 
                                      (select xx.Item_Desc,xx.ITEM_CODE,xx.Location_Code,xx.Location_Desc,xx.Add1,xx.Add2,xx.Add3,xx.Add4,
                                      case when xx.UOM='KG' THEN CASE WHEN ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)=0 THEN xx.UOM ELSE TSPL_ITEM_UOM_DETAIL.UOM_Code END 
	                                  WHEN xx.UOM='GM' THEN CASE WHEN ISNULL(KG_UOM_DETAIL.Conversion_Factor,0)=0 THEN xx.UOM ELSE KG_UOM_DETAIL.UOM_Code END
                                      ELSE xx.UOM END AS 'Unit',CAST((case when xx.UOM='KG' THEN CASE WHEN ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)<>0 THEN (xx.STOCK_QTY/ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1)) ELSE xx.STOCK_QTY END 
	                                  WHEN xx.UOM='GM' THEN CASE WHEN ISNULL(KG_UOM_DETAIL.Conversion_Factor,0)<>0 THEN (xx.STOCK_QTY/ISNULL(KG_UOM_DETAIL.Conversion_Factor,1)) ELSE xx.STOCK_QTY END
                                      ELSE xx.STOCK_QTY END) AS numeric(10,0)) AS 'STOCK_QTY',cast(xx.QTY_FOR_DAYS as integer)  as QTY_FOR_DAYS  
                                      from (SELECT max(RM_STOCK_DAYS.Location_Code)Location_Code,max(RM_STOCK_DAYS.Location_Desc)Location_Desc,max(RM_STOCK_DAYS.Add1)Add1,max(RM_STOCK_DAYS.Add2)Add2,max(RM_STOCK_DAYS.Add3)Add3,max(RM_STOCK_DAYS.Add4)Add4,
                                      RM_STOCK_DAYS.ITEM_CODE,max(RM_STOCK_DAYS.Item_Desc) as Item_Desc,max(RM_STOCK_DAYS.UOM)  AS 'UOM',SUM(ISNULL(RM_STOCK_DAYS.STOCK_QTY,0))  AS 'STOCK_QTY',
                                      SUM(ISNULL(RM_STOCK_DAYS.REQ_STOCK,0)) AS REQ_STOCK,SUM(ISNULL(RM_STOCK_DAYS.MIN_LEVEL,0)) AS MIN_LEVEL,
	                                  CASE WHEN SUM(ISNULL(RM_STOCK_DAYS.REQ_STOCK,0))<>0 THEN SUM(ISNULL(RM_STOCK_DAYS.STOCK_QTY,0))/SUM(ISNULL(RM_STOCK_DAYS.REQ_STOCK,0)) ELSE 
	                                  CASE WHEN SUM(ISNULL(RM_STOCK_DAYS.MIN_LEVEL,0))<>0 THEN SUM(ISNULL(RM_STOCK_DAYS.STOCK_QTY,0))/SUM(ISNULL(RM_STOCK_DAYS.MIN_LEVEL,0)) ELSE 0 END END AS 'QTY_FOR_DAYS' 
	                                  FROM  (SELECT max(RM_STOCK.Location_Code)Location_Code,max(RM_STOCK.Location_Desc)Location_Desc,max(RM_STOCK.Add1)Add1,max(RM_STOCK.Add2)Add2,max(RM_STOCK.Add3)Add3,max(RM_STOCK.Add4)Add4,
                                      RM_STOCK.ITEM_CODE,max(RM_STOCK.Item_Desc) as Item_Desc,MAX(RM_STOCK.UOM) AS 'UOM',SUM(ISNULL(RM_STOCK.IN_STOCK_QTY,0))-SUM(ISNULL(RM_STOCK.OUT_STOCK_QTY,0)) AS 'STOCK_QTY',0 AS 'REQ_STOCK', 0 AS 'MIN_LEVEL' FROM (
	                                  SELECT TSPL_LOCATION_MASTER.Location_Code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_LOCATION_MASTER.Add1,TSPL_LOCATION_MASTER.Add2,TSPL_LOCATION_MASTER.Add3,TSPL_LOCATION_MASTER.Add4,
                                      TSPL_INVENTORY_MOVEMENT.Item_Code AS 'ITEM_CODE',TSPL_ITEM_MASTER.Short_Description AS 'ITEM_DESC',TSPL_ITEM_MASTER.Unit_Code AS 'UOM',
	                                  CASE WHEN INOUT='I' THEN STOCK_QTY END AS 'IN_STOCK_QTY',CASE WHEN INOUT='O' THEN STOCK_QTY END AS 'OUT_STOCK_QTY' FROM TSPL_INVENTORY_MOVEMENT
	                                  LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT.Item_Code
                                      left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_INVENTORY_MOVEMENT.Location_Code
	                                  WHERE 2=2 And TSPL_INVENTORY_MOVEMENT.Location_Code IN ('" + clsCommon.myCstr(txtLocation.Value) + "') AND TSPL_ITEM_MASTER.Structure_Code IN ('RM')and tspl_item_master.Item_Code in ('RM0001','RM0003','RM0004','RM0006','RM0007','RM0008','RM0010','RM0012','RM0014','RM0015','RM0016','RM0018','RM0019')) RM_STOCK
	                                  GROUP BY  RM_STOCK.Item_Code 	UNION ALL
	                                  SELECT max(TSPL_LOCATION_MASTER.Location_Code)LocationCode,max(TSPL_LOCATION_MASTER.Location_Desc)LocDes,max(TSPL_LOCATION_MASTER.Add1)Add1,max(TSPL_LOCATION_MASTER.Add2)Add2,max(TSPL_LOCATION_MASTER.Add3)Add3,max(TSPL_LOCATION_MASTER.Add4)Add4,
                                      TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE,max(TSPL_ITEM_MASTER.Short_Description) as Item_Desc,max(TSPL_ITEM_MASTER.Unit_Code) as 'UOM',
	                                  0 AS 'STOCK_QTY',	AVG(CASE WHEN TSPL_MF_BOM_DETAIL.Percentage>0 THEN (TSPL_MF_BOM_DETAIL.Percentage*TSPL_LOCATION_MASTER.Silo_Capacity*1000)/100 ELSE 
                                      CASE WHEN TSPL_MF_BOM_DETAIL.CONSM_QUANTITY>0 THEN ((TSPL_MF_BOM_DETAIL.CONSM_QUANTITY*TSPL_LOCATION_MASTER.Silo_Capacity*1000)/TSPL_MF_BOM_HEAD.PROD_QUANTITY) ELSE 0 END
                                      END)  AS 'REQ_STOCK',0 AS 'MIN_LEVEL' FROM TSPL_MF_BOM_HEAD 
	                                  LEFT OUTER JOIN TSPL_MF_BOM_DETAIL ON TSPL_MF_BOM_DETAIL.BOM_CODE=TSPL_MF_BOM_HEAD.BOM_CODE
	                                  LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE
	                                  left outer join TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code=TSPL_MF_BOM_HEAD.LOCATION_CODE
	                                  INNER join (select PROD_ITEM_CODE,MAX(BOM_CODE) AS 'BOM_CODE',MAX(REVISION_NO) AS 'REVISION_NO' from TSPL_MF_BOM_HEAD WHERE 2=2 
                                      and TSPL_MF_BOM_HEAD.LOCATION_CODE IN ('" + clsCommon.myCstr(txtLocation.Value) + "') GROUP BY PROD_ITEM_CODE	) BOM_LATEST ON BOM_LATEST.BOM_CODE=TSPL_MF_BOM_HEAD.BOM_CODE AND BOM_LATEST.REVISION_NO=TSPL_MF_BOM_HEAD.REVISION_NO
	                                  WHERE  TSPL_ITEM_MASTER.Structure_Code IN ('RM') and tspl_item_master.Item_Code in('RM0001','RM0003','RM0004','RM0006','RM0007','RM0008','RM0010','RM0012','RM0014','RM0015','RM0016','RM0018','RM0019')   and 	TSPL_MF_BOM_HEAD.LOCATION_CODE IN ('" + clsCommon.myCstr(txtLocation.Value) + "')  GROUP BY TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE
	                                  UNION ALL
	                                  select TSPL_LOCATION_MASTER.Location_Code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_LOCATION_MASTER.Add1,TSPL_LOCATION_MASTER.Add2,TSPL_LOCATION_MASTER.Add3,TSPL_LOCATION_MASTER.Add4,
                                      TSPL_ITEM_REORDER_LEVEL_NEW.Item_Code ,TSPL_ITEM_MASTER.Short_Description AS 'ITEM_DESC',TSPL_ITEM_MASTER.Unit_Code AS 'UOM',
	                                  0 AS 'STOCK_QTY', 0 AS 'REQ_STOCK',TSPL_ITEM_REORDER_LEVEL_NEW.Min_Level AS 'MIN_LEVEL' from TSPL_ITEM_REORDER_LEVEL_NEW 
	                                  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_REORDER_LEVEL_NEW.Item_Code
                                      left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_ITEM_REORDER_LEVEL_NEW.Location_Code
	                                  where TSPL_ITEM_MASTER.Structure_Code IN ('RM') and tspl_item_master.Item_Code in 
                                      ('RM0001','RM0003','RM0004','RM0006','RM0007','RM0008','RM0010','RM0012','RM0014','RM0015','RM0016','RM0018','RM0019') 
                                      and Apply='Y'  AND TSPL_ITEM_REORDER_LEVEL_NEW.Location_Code IN ('" + clsCommon.myCstr(txtLocation.Value) + "') ) RM_STOCK_DAYS
                                      GROUP BY  RM_STOCK_DAYS.ITEM_CODE )xx 
                                      left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=xx.ITEM_CODE and UPPER(TSPL_ITEM_UOM_DETAIL.UOM_Code)='QTL'
                                      left outer join TSPL_ITEM_UOM_DETAIL KG_UOM_DETAIL on KG_UOM_DETAIL.Item_Code=xx.ITEM_CODE and UPPER(KG_UOM_DETAIL.UOM_Code)='KG'
                                      WHERE XX.ITEM_CODE NOT IN ('PM0001','PM0002') and xx.STOCK_QTY>0 ) FINAL WHERE FINAL.QTY_FOR_DAYS<=3 "

            dt = clsDBFuncationality.GetDataTable(Shortitemqry)

            Dim Breakdown As String = "select DATEDIFF(HOUR,Start_Time,End_Time)  as Hrs,Break_Down_Code ,Name as Reason  from TSPL_BREAK_DOWN_ENTRY 
                                      left outer join TSPL_BREAK_DOWN_MASTER on TSPL_BREAK_DOWN_MASTER.Code	=TSPL_BREAK_DOWN_ENTRY.Break_Down_Code
                                      where Doc_Date= '" + clsCommon.GetPrintDate(FromDate.Value) + "'and Location_Code IN ('" + clsCommon.myCstr(txtLocation.Value) + "')"

            Dim dtbreakdown As DataTable = clsDBFuncationality.GetDataTable(Breakdown)
            If dt IsNot Nothing And dt.Rows.Count <= 0 Then
                Dim qry As String = "  SELECT '" + clsCommon.GetPrintDate(FromDate.Value) + "' as Date,'" + txtLocation.Value + "' as Location_Code,'" + lblLocation.Text + "' as Location_Desc "
                dt = clsDBFuncationality.GetDataTable(qry)
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funsubreportWithdt(MyBase.Form_ID, Nothing, CrystalReportFolder.SalesReport, dt, dtshift, "rptMSIProductionSaleReport", "", Nothing, "SubShift.rpt", "SubPrdDaily.rpt", dtproductiondaily, "SubPrdPeriodically.rpt", dtproductionperiodically, "SubSaleDaily.rpt", dtsalesdaily, "SubSalePeriodically.rpt", dtsalesperiodically, "SubInventory.rpt", dtinventory, "Subbreakdown.rpt", dtbreakdown)

                'PDFPath = frmCRV.funsubreportWithdt(isPDFPath, CrystalReportFolder.MilkProcurement, dt, dtAdditionFinance, "crptMilkPurchaseBillPaymentProcessNewJPR", "", Nothing, "subAddition.rpt", "subDeduction.rpt", dtDeductionFinance, "subReduceDeduction.rpt", dtReduceDeduction, "subSaving.rpt", dtSaving, "SubAdditionOther.rpt", dtAdditionOther, "SubDeductionOther.rpt", dtDeductionOther)
                'frmCRV.funreport(CrystalReportFolder.SalesReport, dtsalesdaily, "rptMSIProductionSaleReport", "", Nothing)
                frmCRV = Nothing
            Else
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funsubreportWithdt(MyBase.Form_ID, Nothing, CrystalReportFolder.SalesReport, dt, dtshift, "rptMSIProductionSaleReport", "", Nothing, "SubShift.rpt", "SubPrdDaily.rpt", dtproductiondaily, "SubPrdPeriodically.rpt", dtproductionperiodically, "SubSaleDaily.rpt", dtsalesdaily, "SubSalePeriodically.rpt", dtsalesperiodically, "SubInventory.rpt", dtinventory, "Subbreakdown.rpt", dtbreakdown)

            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try


    End Sub

    Private Sub txtLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocation._MYValidating

        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        Dim WhrCls As String = " Location_Type='Physical'  "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If

        txtLocation.Value = clsCommon.ShowSelectForm("VendorMafnd", qry, "Code", WhrCls, txtLocation.Value, "Code", isButtonClicked)
        lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
            End Sub

    Private Sub fromDate_ValueChanged(sender As Object, e As EventArgs) Handles FromDate.ValueChanged
        Dim selectedMonth As Integer = FromDate.Value.Month
        Dim selectedYear As Integer = FromDate.Value.Year

        Dim currentDate As New DateTime(selectedYear, selectedMonth, 1)
        Slot1FD = clsCommon.GetPrintDate(currentDate, "dd/MMM/yyyy")
        Slot1TD = clsCommon.GetPrintDate(currentDate.AddDays(9), "dd/MMM/yyyy")
        Slot2FD = clsCommon.GetPrintDate(currentDate.AddDays(10), "dd/MMM/yyyy")
        Slot2TD = clsCommon.GetPrintDate(currentDate.AddDays(19), "dd/MMM/yyyy")
        Slot3FD = clsCommon.GetPrintDate(currentDate.AddDays(20), "dd/MMM/yyyy")
        Slot3TD = clsCommon.GetPrintDate(currentDate.AddMonths(1).AddDays(-1), "dd/MMM/yyyy")
    End Sub

    Private Sub MSIProductionSaleReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FromDate.Value = clsCommon.GETSERVERDATE()
        LOCATIONRIGTHS()
    End Sub

    Private Sub LOCATIONRIGTHS()
        Dim obj As New clsMCCCodes()
        Try
            obj = clsMCCCodes.GetData()
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                    arrLoc = obj.arrLocCodes
                End If
            End If
            Dim Loc_Desc As String = " Select Loc_Short_Name from TSPL_LOCATION_MASTER WHERE LOCATION_CODE In (" & arrLoc & ")  "
            Dim dt As DataTable = (clsDBFuncationality.GetDataTable(Loc_Desc))
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    If i = 0 Then
                        Loc_Desc_Name.Append("'" & clsCommon.myCstr(dt.Rows(i)("Loc_Short_Name")) & "' ")
                    Else
                        Loc_Desc_Name.Append(", '" & clsCommon.myCstr(dt.Rows(i)("Loc_Short_Name")) & "' ")
                    End If
                Next
                'Loc_Desc_Code = 
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message)
        Finally
            obj = Nothing
        End Try
    End Sub
    Private Sub rdbSaleReturn_CheckStateChanged(sender As Object, e As EventArgs) Handles rdbSaleReturn.CheckStateChanged
        If rdbSaleReturn.IsChecked = True Then
            rdbDispatch.IsChecked = False
            rdbInvoice.IsChecked = False
        Else
            rdbDispatch.IsChecked = True
        End If
    End Sub
End Class