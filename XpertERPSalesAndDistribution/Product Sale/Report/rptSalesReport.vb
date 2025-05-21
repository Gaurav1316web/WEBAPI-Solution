Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class rptSalesReport
    Inherits FrmMainTranScreen

#Region "Variables"

    Dim arrBack As New List(Of String)
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public arrBatchNo As ArrayList
    Dim arrLoc As String = Nothing
    Dim FORMTYPE As String = Nothing

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptItemConsumptionReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        'rmExportToExcel.Visible = MyBase.isExport
        btnSplitExport.Visible = MyBase.isExport
    End Sub
#End Region
    Sub Reset()
        'txtToDate.Value = clsCommon.GETSERVERDATE()
        'txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        'txtBillToLocation.Value = Nothing
        'lblBillToLocation.Text = ""
        'TxtMultiLocation.arrValueMember = Nothing
        Gv1.DataSource = Nothing
        rbnCustgroup.Checked = False
        rbnPricegroup.Checked = True
        EnableDisableCntrl(True)
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub
    Sub GetReportGridID()
        Dim VarID As String = ""
        If rbnPricegroup.Checked = True Then
            VarID += "_PU"
        ElseIf rbnCustgroup.Checked = True Then
            VarID += "_CG"
        End If
        Gv1.VarID = VarID
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        GetReportGridID()
        Load_Sales_Report(False)
    End Sub

    Private Sub Load_Sales_Report(ByVal PCGroup As Boolean)

        Dim qry As String = ""
        Dim dt As New DataTable()
        Try
            If clsCommon.myLen(txtBillToLocation.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Plz Select Location.", Me.Text)
                txtBillToLocation.Focus()
                Exit Sub
            End If
            Dim whr As String = ""
            Dim Status As String = ""
            Dim Status1 As String = ""
            Dim FG As String = ""
            Dim SFG As String = ""
            Dim FGSFG As String = ""
            Dim StatusInvoice As String = ""
            Dim StatusReturn As String = ""
            Dim StatusReturn1 As String = ""
            Dim Stocktransferdispatch As String = ""
            Dim stocktransferinvoice As String = ""
            Dim statusScrap As String = ""
            Dim statusScrapInvoice As String = ""
            If rdbPosted.IsChecked = True Then
                Status = " AND TSPL_SD_SHIPMENT_HEAD.Status=1 "
                Status1 = " AND TSPL_SPP_PRODUCTION_ENTRY.posted=1 "
                StatusInvoice = " AND TSPL_SD_SALE_INVOICE_HEAD.Status=1 "
                StatusReturn = " AND TSPL_SD_SALE_RETURN_HEAD.Status=1 "
                StatusReturn1 = " And TSPL_SCRAPSALE_HEAD_RETURN.ispost=1"
                statusScrap = " AND TSPL_SCRAPSALE_HEAD.ispost=1 "
                statusScrapInvoice = " AND TSPL_SCRAPINVOICE_HEAD.ispost=1 "
            ElseIf rdbUnposted.IsChecked = True Then
                Status = " AND TSPL_SD_SHIPMENT_HEAD.Status=0 "
                Status1 = " AND TSPL_SPP_PRODUCTION_ENTRY.posted=0 "
                StatusInvoice = " AND TSPL_SD_SALE_INVOICE_HEAD.Status=0 "
                StatusReturn = " AND TSPL_SD_SALE_RETURN_HEAD.Status=0 "
                StatusReturn1 = " AND TSPL_SCRAPSALE_HEAD_RETURN.ispost=0 "
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

            Dim Itemqry As String = ""
            Dim itemNames1 As String = Nothing
            Dim itemNames2 As String = Nothing
            Dim itemNames3 As String = Nothing
            Dim itemNames4 As String = Nothing
            Itemqry = " Select price_code from TSPL_PRICE_COMPONENT_MAPPING "
            Dim dtitemName As DataTable = clsDBFuncationality.GetDataTable(Itemqry)
            If dtitemName.Rows.Count > 0 Then
                For i As Integer = 0 To dtitemName.Rows.Count - 1
                    If i = 0 Then
                        itemNames1 += "[" + clsCommon.myCstr(dtitemName.Rows(i)("price_code")) + "] "
                        itemNames2 += " IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("price_code")) + "],0) As [" + clsCommon.myCstr(dtitemName.Rows(i)("price_code")) + "]"
                        itemNames3 += " ISNULL([" + clsCommon.myCstr(dtitemName.Rows(i)("price_code")) + "],0)"
                        itemNames4 += " Sum(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("price_code")) + "],0)) As [" + clsCommon.myCstr(dtitemName.Rows(i)("price_code")) + "]"

                    Else
                        itemNames1 += ", [" + clsCommon.myCstr(dtitemName.Rows(i)("price_code")) + "] "
                        itemNames2 += ", IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("price_code")) + "],0) As [" + clsCommon.myCstr(dtitemName.Rows(i)("price_code")) + "]"
                        itemNames3 += "+" + "ISNULL([" + clsCommon.myCstr(dtitemName.Rows(i)("price_code")) + "],0)"
                        itemNames4 += ", Sum(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("price_code")) + "],0)) As [" + clsCommon.myCstr(dtitemName.Rows(i)("price_code")) + "]"

                    End If
                Next
            End If

            If rdbFG.IsChecked = True Then
                FG = " and TSPL_Item_Master.FG_for_CF_RPT=1 "
            ElseIf rdbSFG.IsChecked = True Then
                SFG = " and TSPL_Item_Master.SFG_for_CF=1 "
            ElseIf rdbfgsfg.IsChecked = True Then
                FGSFG = " and TSPL_Item_Master.FG_for_CF=1 "
            End If

            If txtBillToLocation.Value IsNot Nothing AndAlso txtBillToLocation.Value.Count > 0 AndAlso rdbDispatch.IsChecked = True Then
                whr += " and TSPL_SD_SHIPMENT_DETAIL.Location In  ('" + clsCommon.myCstr(txtBillToLocation.Value) + "') "
            ElseIf txtBillToLocation.Value IsNot Nothing AndAlso txtBillToLocation.Value.Count > 0 AndAlso rdbInvoice.IsChecked = True Then
                whr += " and TSPL_SD_SALE_INVOICE_DETAIL.Location In  ('" + clsCommon.myCstr(txtBillToLocation.Value) + "') "
            ElseIf txtBillToLocation.Value IsNot Nothing AndAlso txtBillToLocation.Value.Count > 0 AndAlso rdbSaleTransfer.IsChecked = True Then
                whr += " and TSPL_SD_SALE_RETURN_DETAIL.Location In  ('" + clsCommon.myCstr(txtBillToLocation.Value) + "') "
            Else
                'whr += " and TSPL_LOCATION_MASTER.Location_Type='Physical'  "
                'If clsCommon.myLen(arrLoc) > 0 Then
                'whr += "  and  LOCATION_CODE IN (" + clsCommon.GetMulcallStringWithComma(TxtMultiLocation.arrValueMember) + ") "
                'End If
            End If
            If rbnPricegroup.Checked AndAlso rdbDispatch.IsChecked = True AndAlso rdbSaleTransfer.IsChecked = True Then
                qry = " Select * from (SELECT 'RAJASTHAN CO-OPERATIVE DAIRY FEDERATION LIMITED' as HeadName, Location,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "' As FromDate, '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + "'  As ToDate,Location_Desc as [Location Description],Add1,Add4,FORMAT(Document_Date, 'dd/MM/yyyy')as Document_Date,ISNULL ([MILKUNION], 0) as [MILKUNION] ,ISNULL ([GOSHALA], 0) as [GOSHALA] ,ISNULL ([DCS], 0) as [DCS],ISNULL ([GOVT], 0) as [GOVT],ISNULL ([KVSS], 0) as [KVSS], ISNULL ([OTHER], 0) as [OTHER],
                      (ISNULL ([MILKUNION], 0) + ISNULL ([GOSHALA], 0) + ISNULL ([DCS], 0) + ISNULL ([GOVT], 0) + ISNULL ([KVSS], 0) + ISNULL ([OTHER], 0)) as [Total Sale],
                    QuantityBag as [Total BagSale]
                      FROM (Select Location,max(Location_Desc)Location_Desc,max(Add1)Add1,max(Add4)Add4,Document_Date,Sum(SaleQty-ReturnQty)Quantity,Sum(SaleQtyBag-ReturnQtyBag)QuantityBag,price_CodeNon
                      from (Select Location,max(xx.Location_Desc)Location_Desc,max(xx.Add1)Add1,max(xx.Add4)Add4,xx.Document_Date,Sum(xx.SaleQty)SaleQty,sum(xx.SaleQtyBag)SaleQtyBag,
                      sum(xx.ReturnQty)ReturnQty,sum(xx.ReturnQtyBag)ReturnQtyBag,xx.price_CodeNon from
                      (Select TSPL_SD_SHIPMENT_DETAIL.Location,max(TSPL_LOCATION_MASTER.Location_Desc)Location_Desc,max(TSPL_LOCATION_MASTER.Add1)Add1,max(TSPL_LOCATION_MASTER.Add4)Add4,convert (date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as Document_Date,sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SD_SHIPMENT_DETAIL.Qty/TSPL_ITEM_UOM_QTL.Conversion_Factor) as SaleQty,
					  sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SD_SHIPMENT_DETAIL.Qty/TSPL_ITEM_UOM_BAG.Conversion_Factor) as SaleQtyBag,
					  0 as ReturnQty,0 as ReturnQtyBag,price_CodeNon								   
								   FROM TSPL_SD_SHIPMENT_DETAIL
								     left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code =TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE
                                     left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code
                                     LEFT OUTER JOIN TSPL_PRICE_COMPONENT_MAPPING ON TSPL_PRICE_COMPONENT_MAPPING.Price_Code=TSPL_CUSTOMER_MASTER.price_CodeNon
                                     left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_SD_SHIPMENT_DETAIL.Unit_code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_QTL ON  TSPL_ITEM_UOM_QTL.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_QTL.UOM_Code= 'QTL'  
									  LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_BAG ON  TSPL_ITEM_UOM_BAG.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_BAG.UOM_Code= 'BAG' 
						      		 left outer join TSPL_SD_SHIPMENT_HEAD as LO_SD_SALE_INVOICE_HEAD on LO_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE                            
									 left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SHIPMENT_DETAIL.Location
	                                 WHERE  convert (date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) >= Convert (date,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "',103)  
                                     and  convert (date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <= convert (date, '" + clsCommon.GetPrintDate(txtToDate.Value) + "',103) and TSPL_SD_SHIPMENT_DETAIL.Location In  ('" + clsCommon.myCstr(txtBillToLocation.Value) + "') "
                qry += " " + Status + " " + FG + " " + SFG + " " + FGSFG + " "

                qry += " group by convert (date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103),price_CodeNon,Location

                         union all
									
									SELECT TSPL_SCRAPSALE_HEAD.Loc_Code as Location,max(TSPL_LOCATION_MASTER.Location_Desc)Location_Desc,max(TSPL_LOCATION_MASTER.Add1)Add1,max(TSPL_LOCATION_MASTER.Add4)Add4,convert (date,TSPL_SCRAPSALE_HEAD.shipment_Date,103) as Document_Date,sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SCRAPSALE_DETAIL.shipped_Qty/TSPL_ITEM_UOM_QTL.Conversion_Factor) as SaleQty,
									 sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SCRAPSALE_DETAIL.shipped_Qty/TSPL_ITEM_UOM_BAG.Conversion_Factor) as SaleQtyBag,
									 0 as ReturnQty,0 as ReturnQtyBag,price_CodeNon								   
								   FROM TSPL_SCRAPSALE_DETAIL
								     left outer join TSPL_SCRAPSALE_HEAD on TSPL_SCRAPSALE_HEAD.shipment_No =TSPL_SCRAPSALE_DETAIL.shipment_No
                                     left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SCRAPSALE_HEAD.cust_Code
                                     LEFT OUTER JOIN TSPL_PRICE_COMPONENT_MAPPING ON TSPL_PRICE_COMPONENT_MAPPING.Price_Code=TSPL_CUSTOMER_MASTER.price_CodeNon
                                     left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SCRAPSALE_DETAIL.Item_Code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SCRAPSALE_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_SCRAPSALE_DETAIL.Unit_code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_QTL ON  TSPL_ITEM_UOM_QTL.Item_Code=TSPL_SCRAPSALE_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_QTL.UOM_Code= 'QTL' 
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_BAG ON  TSPL_ITEM_UOM_BAG.Item_Code=TSPL_SCRAPSALE_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_BAG.UOM_Code= 'BAG'
						      		left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SCRAPSALE_HEAD.Loc_Code
	                                 WHERE Convert(date,TSPL_SCRAPSALE_HEAD.shipment_Date,103) >= convert(date,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "',103)  
                                     and  convert(date,TSPL_SCRAPSALE_HEAD.shipment_Date,103) <= convert(date,'" + clsCommon.GetPrintDate(txtToDate.Value) + "',103) and TSPL_SCRAPSALE_HEAD.Loc_Code In ('" + clsCommon.myCstr(txtBillToLocation.Value) + "') "
                qry += " " + statusScrap + " " + FG + " " + SFG + " " + FGSFG + " "
                qry += " group by convert (date,TSPL_SCRAPSALE_HEAD.shipment_Date,103),price_CodeNon,Loc_Code)XX Group by XX.Location,xx.Document_Date,xx.price_CodeNon "

                qry += "  UNION ALL
									SELECT TSPL_SD_SALE_RETURN_DETAIL.Location,max(TSPL_LOCATION_MASTER.Location_Desc)Location_Desc,max(TSPL_LOCATION_MASTER.Add1)Add1,
                                    max(TSPL_LOCATION_MASTER.Add4)Add4,convert (date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) as Document_Date,0 as SaleQty,0 as SaleQtyBag,
                                    sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SD_SALE_RETURN_DETAIL.Qty/TSPL_ITEM_UOM_QTL.Conversion_Factor) as ReturnQty,
									 sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SD_SALE_RETURN_DETAIL.Qty/TSPL_ITEM_UOM_BAG.Conversion_Factor) as ReturnQtyBag,
                                    price_CodeNon FROM TSPL_SD_SALE_RETURN_DETAIL
								     left outer join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Document_Code =TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE
                                     left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_RETURN_HEAD.Customer_Code
                                     LEFT OUTER JOIN TSPL_PRICE_COMPONENT_MAPPING ON TSPL_PRICE_COMPONENT_MAPPING.Price_Code=TSPL_CUSTOMER_MASTER.price_CodeNon
                                     left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_SD_SALE_RETURN_DETAIL.Unit_code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_QTL ON  TSPL_ITEM_UOM_QTL.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_QTL.UOM_Code= 'QTL'   
									 LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_BAG ON  TSPL_ITEM_UOM_BAG.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_BAG.UOM_Code= 'BAG' 
						      		 left outer join TSPL_SD_SALE_INVOICE_HEAD as LO_SD_SALE_INVOICE_HEAD on LO_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE                            
									 left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SALE_RETURN_DETAIL.Location
	                                 WHERE  convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) >= convert(date,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "',103)   
                                     and  convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) <= convert(date,'" + clsCommon.GetPrintDate(txtToDate.Value) + "',103) and TSPL_SD_SALE_RETURN_DETAIL.Location In  ('" + clsCommon.myCstr(txtBillToLocation.Value) + "')"
                qry += " " + StatusReturn + " " + FG + " " + SFG + " " + FGSFG + " "
                qry += "	 group by convert (date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103),price_CodeNon,Location)XX group by xx.Location,xx.Document_Date,
                             xx.price_CodeNon )Tab1
                                    PIVOT (SUM(Quantity) FOR price_CodeNon IN ([MILKUNION],[GOSHALA],[DCS],[GOVT],[KVSS],[OTHER]))AS Tab2)tmp  "

            ElseIf rbnPricegroup.Checked AndAlso rdbInvoice.IsChecked = True AndAlso rdbSaleTransfer.IsChecked = True Then
                qry = " Select * from (SELECT 'RAJASTHAN CO-OPERATIVE DAIRY FEDERATION LIMITED' as HeadName, Location,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "' As FromDate, '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + "'  As ToDate,Location_Desc as [Location Description],Add1,Add4,FORMAT(Document_Date, 'dd/MM/yyyy')as Document_Date,ISNULL ([MILKUNION], 0) as [MILKUNION] ,ISNULL ([GOSHALA], 0) as [GOSHALA] ,ISNULL ([DCS], 0) as [DCS],ISNULL ([GOVT], 0) as [GOVT],ISNULL ([KVSS], 0) as [KVSS], ISNULL ([OTHER], 0) as [OTHER],
                      (ISNULL ([MILKUNION], 0) + ISNULL ([GOSHALA], 0) + ISNULL ([DCS], 0) + ISNULL ([GOVT], 0) + ISNULL ([KVSS], 0) + ISNULL ([OTHER], 0)) as [Total Sale],
                    QuantityBag as [Total BagSale]
                      FROM (Select Location,max(Location_Desc)Location_Desc,max(Add1)Add1,max(Add4)Add4,Document_Date,Sum(SaleQty-ReturnQty)Quantity,Sum(SaleQtyBag-ReturnQtyBag)QuantityBag,price_CodeNon 
					  from (Select Location,max(xx.Location_Desc)Location_Desc,max(xx.Add1)Add1,max(xx.Add4)Add4,xx.Document_Date,Sum(xx.SaleQty)SaleQty,sum(xx.SaleQtyBag)SaleQtyBag,
                      sum(xx.ReturnQty)ReturnQty,sum(xx.ReturnQtyBag)ReturnQtyBag,xx.price_CodeNon from
					  (SELECT TSPL_SD_SALE_INVOICE_DETAIL.Location,max(TSPL_LOCATION_MASTER.Location_Desc)Location_Desc,max(TSPL_LOCATION_MASTER.Add1)Add1,
                      max(TSPL_LOCATION_MASTER.Add4)Add4,convert (date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as Document_Date,
                      sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SD_SALE_INVOICE_DETAIL.Qty/TSPL_ITEM_UOM_QTL.Conversion_Factor) as SaleQty,
					  sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SD_SALE_INVOICE_DETAIL.Qty/TSPL_ITEM_UOM_BAG.Conversion_Factor) as SaleQtyBag
					  ,0 as ReturnQty,0 as ReturnQtyBag,price_CodeNon		   
                      FROM TSPL_SD_SALE_INVOICE_DETAIL
								     left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE
                                     left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
                                     LEFT OUTER JOIN TSPL_PRICE_COMPONENT_MAPPING ON TSPL_PRICE_COMPONENT_MAPPING.Price_Code=TSPL_CUSTOMER_MASTER.price_CodeNon
                                     left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_SD_SALE_INVOICE_DETAIL.Unit_code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_QTL ON  TSPL_ITEM_UOM_QTL.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_QTL.UOM_Code= 'QTL' 
									 LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_BAG ON  TSPL_ITEM_UOM_BAG.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_BAG.UOM_Code= 'BAG' 
						      		 left outer join TSPL_SD_SALE_INVOICE_HEAD as LO_SD_SALE_INVOICE_HEAD on LO_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE                            
									 left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SALE_INVOICE_DETAIL.Location
	                                 WHERE  convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= convert(date,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "',103)  
                                     and  convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= convert(date,'" + clsCommon.GetPrintDate(txtToDate.Value) + "',103) and TSPL_SD_SALE_INVOICE_DETAIL.Location In  ('" + clsCommon.myCstr(txtBillToLocation.Value) + "') "
                qry += " " + StatusInvoice + " " + FG + " " + SFG + " " + FGSFG + " "

                qry += " group by convert (date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103),price_CodeNon,Location

                          union all
									
									  SELECT TSPL_SCRAPINVOICE_HEAD.Loc_Code as Location,max(TSPL_LOCATION_MASTER.Location_Desc)Location_Desc,max(TSPL_LOCATION_MASTER.Add1)Add1,max(TSPL_LOCATION_MASTER.Add4)Add4,convert (date,TSPL_SCRAPINVOICE_HEAD.shipment_Date,103) as Document_Date,sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SCRAPINVOICE_DETAIL.shipped_Qty/TSPL_ITEM_UOM_QTL.Conversion_Factor) as SaleQty,
									 sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SCRAPINVOICE_DETAIL.shipped_Qty/TSPL_ITEM_UOM_BAG.Conversion_Factor) as SaleQtyBag
									 ,0 as ReturnQty,0 as ReturnQtyBag,price_CodeNon								   
								   FROM TSPL_SCRAPINVOICE_DETAIL
								     left outer join TSPL_SCRAPINVOICE_HEAD on TSPL_SCRAPINVOICE_HEAD.invoice_No =TSPL_SCRAPINVOICE_DETAIL.invoice_No
                                     left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SCRAPINVOICE_HEAD.cust_Code
                                     LEFT OUTER JOIN TSPL_PRICE_COMPONENT_MAPPING ON TSPL_PRICE_COMPONENT_MAPPING.Price_Code=TSPL_CUSTOMER_MASTER.price_CodeNon
                                     left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SCRAPINVOICE_DETAIL.Item_Code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SCRAPINVOICE_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_SCRAPINVOICE_DETAIL.Unit_code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_QTL ON  TSPL_ITEM_UOM_QTL.Item_Code=TSPL_SCRAPINVOICE_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_QTL.UOM_Code= 'QTL'
									 	 LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_BAG ON  TSPL_ITEM_UOM_BAG.Item_Code=TSPL_SCRAPINVOICE_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_BAG.UOM_Code= 'BAG' 
						      		left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SCRAPINVOICE_HEAD.Loc_Code
	                                 WHERE convert(date,TSPL_SCRAPINVOICE_HEAD.shipment_Date,103) >= convert(date,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "',103)
                                     and  convert(date,TSPL_SCRAPINVOICE_HEAD.shipment_Date,103) <= convert(date,'" + clsCommon.GetPrintDate(txtToDate.Value) + "',103) and TSPL_SCRAPINVOICE_HEAD.Loc_Code In ('" + clsCommon.myCstr(txtBillToLocation.Value) + "') "
                qry += " " + statusScrapInvoice + " " + FG + " " + SFG + " " + FGSFG + " "
                qry += " group by convert (date,TSPL_SCRAPINVOICE_HEAD.shipment_Date,103),price_CodeNon,Loc_Code)XX Group by XX.Location,xx.Document_Date,xx.price_CodeNon "

                qry += "  UNION ALL
									SELECT TSPL_SD_SALE_RETURN_DETAIL.Location,max(TSPL_LOCATION_MASTER.Location_Desc)Location_Desc,max(TSPL_LOCATION_MASTER.Add1)Add1,
                                    max(TSPL_LOCATION_MASTER.Add4)Add4,convert (date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) as Document_Date,0 as SaleQty,0 as SaleQtyBag,
                                    sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SD_SALE_RETURN_DETAIL.Qty/TSPL_ITEM_UOM_QTL.Conversion_Factor) as ReturnQty,
									 sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SD_SALE_RETURN_DETAIL.Qty/TSPL_ITEM_UOM_BAG.Conversion_Factor) as ReturnQtyBag,
                                    price_CodeNon FROM TSPL_SD_SALE_RETURN_DETAIL
								     left outer join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Document_Code =TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE
                                     left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_RETURN_HEAD.Customer_Code
                                     LEFT OUTER JOIN TSPL_PRICE_COMPONENT_MAPPING ON TSPL_PRICE_COMPONENT_MAPPING.Price_Code=TSPL_CUSTOMER_MASTER.price_CodeNon
                                     left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_SD_SALE_RETURN_DETAIL.Unit_code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_QTL ON  TSPL_ITEM_UOM_QTL.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_QTL.UOM_Code= 'QTL'   
									 LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_BAG ON  TSPL_ITEM_UOM_BAG.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_BAG.UOM_Code= 'BAG' 
						      		 left outer join TSPL_SD_SALE_INVOICE_HEAD as LO_SD_SALE_INVOICE_HEAD on LO_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE                            
									 left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SALE_RETURN_DETAIL.Location
	                                 WHERE  convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) >= convert(date,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "',103)   
                                     and  convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) <= convert(date,'" + clsCommon.GetPrintDate(txtToDate.Value) + "',103) and TSPL_SD_SALE_RETURN_DETAIL.Location In  ('" + clsCommon.myCstr(txtBillToLocation.Value) + "')"
                qry += " " + StatusReturn + " " + FG + " " + SFG + " " + FGSFG + " "
                qry += "	 group by convert (date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103),price_CodeNon,Location
									 
									 )XX group by xx.Location,xx.Document_Date,xx.price_CodeNon )Tab1
                                    PIVOT (SUM(Quantity) FOR price_CodeNon IN ([MILKUNION],[GOSHALA],[DCS],[GOVT],[KVSS],[OTHER]))AS Tab2)tmp  "

            ElseIf rbnPricegroup.Checked AndAlso rdbDispatch.IsChecked = True Then
                qry = "   Select 'RAJASTHAN CO-OPERATIVE DAIRY FEDERATION LIMITED' as HeadName,Location,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "' As FromDate,
                          '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + "'  As ToDate,max([Location Description])[Location Description],max(Add1)Add1,max(Add4)Add4,
                           max(Document_Date)Document_Date," & itemNames4 & ",Sum([Total Sale])[Total Sale],sum([Total BagSale])[Total BagSale]
                          from (SELECT  Location,Location_Desc as [Location Description],Add1,Add4,FORMAT(Document_Date, 'dd/MM/yyyy')as Document_Date,
                          " & itemNames2 & ", " & itemNames3 & " as [Total Sale],
                            QuantityBag as [Total BagSale]
  FROM
                                   (SELECT XX.Location,MAX(XX.Location_Desc)Location_Desc,max(xx.Add1)Add1,max(xx.Add4)Add4,(xx.Document_Date)Document_Date,
                                   sum(xx.Quantity)Quantity,sum(xx.QuantityBag)QuantityBag,
                                    price_CodeNon FROM (SELECT TSPL_SD_SHIPMENT_DETAIL.Location,max(TSPL_LOCATION_MASTER.Location_Desc)Location_Desc,max(TSPL_LOCATION_MASTER.Add1)Add1,max(TSPL_LOCATION_MASTER.Add4)Add4,
                                    convert (date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as Document_Date,sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SD_SHIPMENT_DETAIL.Qty/TSPL_ITEM_UOM_QTL.Conversion_Factor) as Quantity,
                                    Sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SD_SHIPMENT_DETAIL.Qty/TSPL_ITEM_UOM_BAG.Conversion_Factor) as QuantityBag, price_CodeNon 
                                    FROM TSPL_SD_SHIPMENT_DETAIL
								     left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code =TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE
                                     left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code
                                     LEFT OUTER JOIN TSPL_PRICE_COMPONENT_MAPPING ON TSPL_PRICE_COMPONENT_MAPPING.Price_Code=TSPL_CUSTOMER_MASTER.price_CodeNon
                                     left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_SD_SHIPMENT_DETAIL.Unit_code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_QTL ON  TSPL_ITEM_UOM_QTL.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_QTL.UOM_Code= 'QTL'
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_BAG ON  TSPL_ITEM_UOM_BAG.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_BAG.UOM_Code= 'BAG'
						      		 left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SHIPMENT_DETAIL.Location
	                                 WHERE  convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) >= convert(date,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "',103)  
                                     and  convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <= convert(date,'" + clsCommon.GetPrintDate(txtToDate.Value) + "',103)" + whr + " "
                qry += " " + Status + " " + FG + " " + SFG + " " + FGSFG + "  "
                If rdbStockTransfer.IsChecked = True Then
                    qry += "" + Stocktransferdispatch + ""
                Else
                    qry += " And TSPL_SD_SHIPMENT_HEAD.Inter_unit_sale=0 "
                End If

                qry += " group by convert (date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103),price_CodeNon,Location
                         union all
                         
									 SELECT TSPL_SCRAPSALE_HEAD.Loc_Code as Location,max(TSPL_LOCATION_MASTER.Location_Desc)Location_Desc,max(TSPL_LOCATION_MASTER.Add1)Add1,max(TSPL_LOCATION_MASTER.Add4)Add4,convert (date,TSPL_SCRAPSALE_HEAD.shipment_Date,103) as Document_Date,
                                     sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SCRAPSALE_DETAIL.shipped_Qty/TSPL_ITEM_UOM_QTL.Conversion_Factor) as Quantity,
                                     Sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SCRAPSALE_DETAIL.shipped_Qty/TSPL_ITEM_UOM_BAG.Conversion_Factor) as QuantityBag,price_CodeNon								   
								   FROM TSPL_SCRAPSALE_DETAIL
								     left outer join TSPL_SCRAPSALE_HEAD on TSPL_SCRAPSALE_HEAD.shipment_No =TSPL_SCRAPSALE_DETAIL.shipment_No
                                     left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SCRAPSALE_HEAD.cust_Code
                                     LEFT OUTER JOIN TSPL_PRICE_COMPONENT_MAPPING ON TSPL_PRICE_COMPONENT_MAPPING.Price_Code=TSPL_CUSTOMER_MASTER.price_CodeNon
                                     left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SCRAPSALE_DETAIL.Item_Code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SCRAPSALE_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_SCRAPSALE_DETAIL.Unit_code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_QTL ON  TSPL_ITEM_UOM_QTL.Item_Code=TSPL_SCRAPSALE_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_QTL.UOM_Code= 'QTL' 
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_BAG ON  TSPL_ITEM_UOM_BAG.Item_Code=TSPL_SCRAPSALE_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_BAG.UOM_Code= 'BAG' 
						      		left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SCRAPSALE_HEAD.Loc_Code
	                                 WHERE convert(date,TSPL_SCRAPSALE_HEAD.shipment_Date,103) >= convert(date,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "',103)  
                                     and  convert(date,TSPL_SCRAPSALE_HEAD.shipment_Date,103) <= convert(date,'" + clsCommon.GetPrintDate(txtToDate.Value) + "',103)
                                     and TSPL_SCRAPSALE_HEAD.Loc_Code In ('" + clsCommon.myCstr(txtBillToLocation.Value) + "') "
                qry += " " + statusScrap + " " + FG + " " + SFG + " " + FGSFG + ""
                If rdbStockTransfer.IsChecked = True Then
                    qry += " and TSPL_SCRAPSALE_HEAD.Inter_unit_sale =1 "
                Else
                    qry += " and TSPL_SCRAPSALE_HEAD.Inter_unit_sale =0 "
                End If

                qry += " group by convert (date,TSPL_SCRAPSALE_HEAD.shipment_Date,103),price_CodeNon,Loc_Code )XX GROUP BY xx.Document_Date,XX.price_CodeNon,XX.Location )Tab1
                                    PIVOT (SUM(Quantity) FOR price_CodeNon IN (" & itemNames1 & "))AS Tab2)tmp group by Location  "

            ElseIf rbnPricegroup.Checked AndAlso rdbInvoice.IsChecked = True Then
                qry = "   Select 'RAJASTHAN CO-OPERATIVE DAIRY FEDERATION LIMITED' as HeadName,Location,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "' As FromDate,
                          '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + "'  As ToDate,max([Location Description])[Location Description],max(Add1)Add1,max(Add4)Add4,
                           max(Document_Date)Document_Date," & itemNames4 & ",Sum([Total Sale])[Total Sale],sum([Total BagSale])[Total BagSale]
                          from (SELECT  Location,Location_Desc as [Location Description],Add1,Add4,FORMAT(Document_Date, 'dd/MM/yyyy')as Document_Date,
                          " & itemNames2 & ", " & itemNames3 & " as [Total Sale],
                            QuantityBag as [Total BagSale]
  FROM
                                   (SELECT XX.Location,MAX(XX.Location_Desc)Location_Desc,max(xx.Add1)Add1,max(xx.Add4)Add4,(xx.Document_Date)Document_Date,
                                   sum(xx.Quantity)Quantity,Sum(xx.QuantityBag)QuantityBag,
                                    price_CodeNon FROM (SELECT TSPL_SD_SALE_INVOICE_DETAIL.Location,max(TSPL_LOCATION_MASTER.Location_Desc)Location_Desc,max(TSPL_LOCATION_MASTER.Add1)Add1,max(TSPL_LOCATION_MASTER.Add4)Add4,
                                    convert (date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as Document_Date,
                                   sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SD_SALE_INVOICE_DETAIL.Qty/TSPL_ITEM_UOM_QTL.Conversion_Factor) as Quantity,
                                    Sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SD_SALE_INVOICE_DETAIL.Qty/TSPL_ITEM_UOM_BAG.Conversion_Factor) as QuantityBag,price_CodeNon 
                                   FROM TSPL_SD_SALE_INVOICE_DETAIL
								     left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE
                                     left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
                                     LEFT OUTER JOIN TSPL_PRICE_COMPONENT_MAPPING ON TSPL_PRICE_COMPONENT_MAPPING.Price_Code=TSPL_CUSTOMER_MASTER.price_CodeNon
                                     left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_SD_SALE_INVOICE_DETAIL.Unit_code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_QTL ON  TSPL_ITEM_UOM_QTL.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_QTL.UOM_Code= 'QTL' 
                                    LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_BAG ON  TSPL_ITEM_UOM_BAG.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_BAG.UOM_Code= 'BAG'
						      		 left outer join TSPL_SD_SALE_INVOICE_HEAD as LO_SD_SALE_INVOICE_HEAD on LO_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE                            
									 left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SALE_INVOICE_DETAIL.Location
	                                 WHERE  convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= convert(date,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "',103)  
                                     and  convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= convert(date,'" + clsCommon.GetPrintDate(txtToDate.Value) + "',103)" + whr + " "
                qry += " " + StatusInvoice + " " + FG + " " + SFG + " " + FGSFG + "  "
                If rdbStockTransfer.IsChecked = True Then
                    qry += "" + stocktransferinvoice + ""
                Else
                    qry += " and TSPL_SD_SALE_INVOICE_HEAD.Inter_unit_sale=0 "
                End If

                qry += " group by convert (date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103),price_CodeNon,Location
                          union all
                         
									 SELECT TSPL_SCRAPINVOICE_HEAD.Loc_Code as Location,max(TSPL_LOCATION_MASTER.Location_Desc)Location_Desc,max(TSPL_LOCATION_MASTER.Add1)Add1,max(TSPL_LOCATION_MASTER.Add4)Add4,convert (date,TSPL_SCRAPINVOICE_HEAD.shipment_Date,103) as Document_Date,
                                   sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SCRAPINVOICE_DETAIL.shipped_Qty/TSPL_ITEM_UOM_QTL.Conversion_Factor) as Quantity,
                                   Sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SCRAPINVOICE_DETAIL.shipped_Qty/TSPL_ITEM_UOM_BAG.Conversion_Factor) as QuantityBag,price_CodeNon								   
								   FROM TSPL_SCRAPINVOICE_DETAIL
								     left outer join TSPL_SCRAPINVOICE_HEAD on TSPL_SCRAPINVOICE_HEAD.invoice_No =TSPL_SCRAPINVOICE_DETAIL.invoice_No
                                     left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SCRAPINVOICE_HEAD.cust_Code
                                     LEFT OUTER JOIN TSPL_PRICE_COMPONENT_MAPPING ON TSPL_PRICE_COMPONENT_MAPPING.Price_Code=TSPL_CUSTOMER_MASTER.price_CodeNon
                                     left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SCRAPINVOICE_DETAIL.Item_Code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SCRAPINVOICE_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_SCRAPINVOICE_DETAIL.Unit_code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_QTL ON  TSPL_ITEM_UOM_QTL.Item_Code=TSPL_SCRAPINVOICE_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_QTL.UOM_Code= 'QTL'
                                    LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_BAG ON  TSPL_ITEM_UOM_BAG.Item_Code=TSPL_SCRAPINVOICE_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_BAG.UOM_Code= 'BAG'
						      		left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SCRAPINVOICE_HEAD.Loc_Code
	                                 WHERE convert(date,TSPL_SCRAPINVOICE_HEAD.shipment_Date,103) >= convert(date,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "',103) 
                                     and  convert(date,TSPL_SCRAPINVOICE_HEAD.shipment_Date,103) <= convert(date,'" + clsCommon.GetPrintDate(txtToDate.Value) + "',103)
                                     and TSPL_SCRAPINVOICE_HEAD.Loc_Code In ('" + clsCommon.myCstr(txtBillToLocation.Value) + "') "
                qry += " " + statusScrapInvoice + " " + FG + " " + SFG + " " + FGSFG + " "

                If rdbStockTransfer.IsChecked = True Then
                    qry += " and TSPL_SCRAPINVOICE_HEAD.Inter_unit_sale=1 "
                Else
                    qry += " and TSPL_SCRAPINVOICE_HEAD.Inter_unit_sale=0 "
                End If

                qry += " group by convert (date,TSPL_SCRAPINVOICE_HEAD.shipment_Date,103),price_CodeNon,Loc_Code )XX GROUP BY xx.Document_Date,XX.price_CodeNon,XX.Location )Tab1
                                    PIVOT (SUM(Quantity) FOR price_CodeNon IN (" & itemNames1 & "))AS Tab2)tmp group by Location  "

            ElseIf rbnPricegroup.Checked AndAlso rdbSaleReturn.IsChecked = True Then
                qry = "   Select 'RAJASTHAN CO-OPERATIVE DAIRY FEDERATION LIMITED' as HeadName,Location,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "' As FromDate,
                          '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + "'  As ToDate,max([Location Description])[Location Description],max(Add1)Add1,max(Add4)Add4,
                           max(Document_Date)Document_Date," & itemNames4 & ",Sum([Total Sale])[Total Sale],sum([Total BagSale])[Total BagSale]
                          from (SELECT  Location,Location_Desc as [Location Description],Add1,Add4,FORMAT(Document_Date, 'dd/MM/yyyy')as Document_Date,
                          " & itemNames2 & ", " & itemNames3 & " as [Total Sale],
                            QuantityBag as [Total BagSale]
                              FROM 
                            (SELECT XX.Location,MAX(XX.Location_Desc)Location_Desc,max(xx.Add1)Add1,max(xx.Add4)Add4,(xx.Document_Date)Document_Date,
                                   sum(xx.Quantity)Quantity,sum(xx.QuantityBag)QuantityBag,
                                    price_CodeNon FROM 
                                   (SELECT TSPL_SD_SALE_RETURN_DETAIL.Location,MAX(TSPL_LOCATION_MASTER.Location_Desc)Location_Desc,max(TSPL_LOCATION_MASTER.Add1)Add1,
                                    max(TSPL_LOCATION_MASTER.Add4)Add4,convert (date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) as Document_Date,
                                    sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SD_SALE_RETURN_DETAIL.Qty/TSPL_ITEM_UOM_QTL.Conversion_Factor) as Quantity,
									Sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SD_SALE_RETURN_DETAIL.Qty/TSPL_ITEM_UOM_BAG.Conversion_Factor) as QuantityBag,
									price_CodeNon								   
								   FROM TSPL_SD_SALE_RETURN_DETAIL
								     left outer join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Document_Code =TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE
                                     left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_RETURN_HEAD.Customer_Code
                                     LEFT OUTER JOIN TSPL_PRICE_COMPONENT_MAPPING ON TSPL_PRICE_COMPONENT_MAPPING.Price_Code=TSPL_CUSTOMER_MASTER.price_CodeNon
                                     left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_SD_SALE_RETURN_DETAIL.Unit_code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_QTL ON  TSPL_ITEM_UOM_QTL.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_QTL.UOM_Code= 'QTL' 
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_BAG ON  TSPL_ITEM_UOM_BAG.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_BAG.UOM_Code= 'BAG'
						      		 left outer join TSPL_SD_SALE_RETURN_HEAD as LO_SD_SALE_INVOICE_HEAD on LO_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE                            
									 left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SALE_RETURN_DETAIL.Location
	                                 WHERE  convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) >= convert(date,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "',103)  
                                     and  convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) <= convert(date,'" + clsCommon.GetPrintDate(txtToDate.Value) + "',103) and TSPL_SD_SALE_RETURN_DETAIL.Location In  ('" + clsCommon.myCstr(txtBillToLocation.Value) + "') "
                qry += " " + StatusReturn + " " + FG + " " + SFG + " " + FGSFG + " "
                qry += " group by convert (date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103),price_CodeNon,Location

UNION ALL

SELECT TSPL_SCRAPSALE_HEAD_RETURN.Loc_Code,max(TSPL_LOCATION_MASTER.Location_Desc)Location_Desc,max(TSPL_LOCATION_MASTER.Add1)Add1,
                                    max(TSPL_LOCATION_MASTER.Add4)Add4,convert (date,TSPL_SCRAPSALE_HEAD_RETURN.Return_ship_Date,103) as Document_Date,
                                    sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SCRAPSALE_DETAIL_RETURN.shipped_Qty/TSPL_ITEM_UOM_QTL.Conversion_Factor) as Quantity,
									Sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SCRAPSALE_DETAIL_RETURN.shipped_Qty/TSPL_ITEM_UOM_BAG.Conversion_Factor) as QuantityBag,
									price_CodeNon								   
								   FROM TSPL_SCRAPSALE_DETAIL_RETURN
								     left outer join TSPL_SCRAPSALE_HEAD_RETURN on TSPL_SCRAPSALE_HEAD_RETURN.Document_No =TSPL_SCRAPSALE_DETAIL_RETURN.Document_No
                                     left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SCRAPSALE_HEAD_RETURN.cust_Code
                                     LEFT OUTER JOIN TSPL_PRICE_COMPONENT_MAPPING ON TSPL_PRICE_COMPONENT_MAPPING.Price_Code=TSPL_CUSTOMER_MASTER.price_CodeNon
                                     left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SCRAPSALE_DETAIL_RETURN.Item_Code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SCRAPSALE_DETAIL_RETURN.Item_Code 
                                     AND TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_SCRAPSALE_DETAIL_RETURN.Unit_code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_QTL ON  TSPL_ITEM_UOM_QTL.Item_Code=TSPL_SCRAPSALE_DETAIL_RETURN.Item_Code 
                                     AND TSPL_ITEM_UOM_QTL.UOM_Code= 'QTL'  
									  LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_BAG ON  TSPL_ITEM_UOM_BAG.Item_Code=TSPL_SCRAPSALE_DETAIL_RETURN.Item_Code 
                                     AND TSPL_ITEM_UOM_BAG.UOM_Code= 'BAG'
						      		 left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SCRAPSALE_HEAD_RETURN.Loc_Code
	                                 WHERE  convert(date,TSPL_SCRAPSALE_HEAD_RETURN.Return_ship_Date,103) >= convert(date,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "',103)  
                                     and  convert(date,TSPL_SCRAPSALE_HEAD_RETURN.Return_ship_Date,103) <= convert(date,'" + clsCommon.GetPrintDate(txtToDate.Value) + "',103) and TSPL_SCRAPSALE_HEAD_RETURN.Loc_Code In  ('" + clsCommon.myCstr(txtBillToLocation.Value) + "') "
                qry += " " + StatusReturn1 + " " + FG + " " + SFG + " " + FGSFG + " "
                qry += " group by convert (date,TSPL_SCRAPSALE_HEAD_RETURN.Return_ship_Date,103),price_CodeNon,Loc_Code)XX  GROUP BY xx.Document_Date,XX.price_CodeNon,XX.Location
)Tab1
                                    PIVOT (SUM(Quantity) FOR price_CodeNon IN (" & itemNames1 & "))AS Tab2)tmp group by location   "

            Else
                If rbnCustgroup.Checked AndAlso rdbDispatch.IsChecked = True AndAlso rdbSaleTransfer.IsChecked = True Then
                    qry = " Select * from (SELECT 'RAJASTHAN CO-OPERATIVE DAIRY FEDERATION LIMITED' as HeadName, Location,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "' As FromDate, '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + "'as ToDate,Location_Desc as [Location Description],Add1,Add4,FORMAT(Document_Date, 'dd/MM/yyyy')as Document_Date,ISNULL ([UNION], 0) as [MILKUNION] ,ISNULL ([DEALER], 0) as [DEALER],ISNULL ([GOSHAL], 0) as [GOSHALA] ,ISNULL ([DCS], 0) as [DCS],ISNULL ([GOV], 0) as [GOVT],ISNULL ([KVSS], 0) as [KVSS], ISNULL ([AGENCY],0) AS [AGENCY],ISNULL ([RETAIL], 0) AS [RETAIL],ISNULL ([CFP], 0) AS [CFP],ISNULL ([MISC], 0) as [OTHER],
  (ISNULL ([UNION], 0) +ISNULL ([DEALER], 0)+ ISNULL ([GOSHAL], 0) + ISNULL ([DCS], 0) + ISNULL ([GOV], 0) + ISNULL ([KVSS], 0) + ISNULL ([RETAIL], 0)+ ISNULL ([CFP], 0)+ ISNULL ([AGENCY], 0)+ ISNULL ([MISC], 0)) as [Total Sale],QuantityBag as [Total BagSale]
  FROM (Select Location,max(Location_Desc)Location_Desc,max(Add1)Add1,max(Add4)Add4,Document_Date,Sum(SaleQty-ReturnQty)Quantity,Sum(SaleQtyBag-ReturnQtyBag)QuantityBag,Cust_Group_Code
                      from (Select Location,max(xx.Location_Desc)Location_Desc,max(xx.Add1)Add1,max(xx.Add4)Add4,xx.Document_Date,Sum(xx.SaleQty)SaleQty,sum(xx.SaleQtyBag)SaleQtyBag,
                      sum(xx.ReturnQty)ReturnQty,sum(xx.ReturnQtyBag)ReturnQtyBag,xx.Cust_Group_Code   from
                                   ( Select TSPL_SD_SHIPMENT_DETAIL.Location,max(TSPL_LOCATION_MASTER.Location_Desc)Location_Desc,max(TSPL_LOCATION_MASTER.Add1)Add1,max(TSPL_LOCATION_MASTER.Add4)Add4,convert (date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as Document_Date,sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SD_SHIPMENT_DETAIL.Qty/TSPL_ITEM_UOM_QTL.Conversion_Factor) as SaleQty,
					  sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SD_SHIPMENT_DETAIL.Qty/TSPL_ITEM_UOM_BAG.Conversion_Factor) as SaleQtyBag,
					  0 as ReturnQty,0 as ReturnQtyBag,Cust_Group_Code								   
								   FROM TSPL_SD_SHIPMENT_DETAIL
								     left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code =TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE
                                     left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code
                                     LEFT OUTER JOIN TSPL_PRICE_COMPONENT_MAPPING ON TSPL_PRICE_COMPONENT_MAPPING.Price_Code=TSPL_CUSTOMER_MASTER.price_CodeNon
                                     left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_SD_SHIPMENT_DETAIL.Unit_code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_QTL ON  TSPL_ITEM_UOM_QTL.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_QTL.UOM_Code= 'QTL'  
									  LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_BAG ON  TSPL_ITEM_UOM_BAG.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_BAG.UOM_Code= 'BAG' 
						      		 left outer join TSPL_SD_SHIPMENT_HEAD as LO_SD_SALE_INVOICE_HEAD on LO_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE                            
									 left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SHIPMENT_DETAIL.Location
	                                 WHERE convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) >= convert(date,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "',103) 
                                     and  convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <= convert(date,'" + clsCommon.GetPrintDate(txtToDate.Value) + "',103) and TSPL_SD_SHIPMENT_DETAIL.Location In  ('" + clsCommon.myCstr(txtBillToLocation.Value) + "') "
                    qry += " " + Status + " " + FG + " " + SFG + " " + FGSFG + " "
                    qry += " group by convert (date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103),Cust_Group_Code,Location

                                    union all
									
									SELECT TSPL_SCRAPSALE_HEAD.Loc_Code as Location,max(TSPL_LOCATION_MASTER.Location_Desc)Location_Desc,max(TSPL_LOCATION_MASTER.Add1)Add1,max(TSPL_LOCATION_MASTER.Add4)Add4,convert (date,TSPL_SCRAPSALE_HEAD.shipment_Date,103) as Document_Date,sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SCRAPSALE_DETAIL.shipped_Qty/TSPL_ITEM_UOM_QTL.Conversion_Factor) as SaleQty,
									 sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SCRAPSALE_DETAIL.shipped_Qty/TSPL_ITEM_UOM_BAG.Conversion_Factor) as SaleQtyBag,
									 0 as ReturnQty,0 as ReturnQtyBag,Cust_Group_Code								   
								   FROM TSPL_SCRAPSALE_DETAIL
								     left outer join TSPL_SCRAPSALE_HEAD on TSPL_SCRAPSALE_HEAD.shipment_No =TSPL_SCRAPSALE_DETAIL.shipment_No
                                     left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SCRAPSALE_HEAD.cust_Code
                                     LEFT OUTER JOIN TSPL_PRICE_COMPONENT_MAPPING ON TSPL_PRICE_COMPONENT_MAPPING.Price_Code=TSPL_CUSTOMER_MASTER.price_CodeNon
                                     left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SCRAPSALE_DETAIL.Item_Code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SCRAPSALE_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_SCRAPSALE_DETAIL.Unit_code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_QTL ON  TSPL_ITEM_UOM_QTL.Item_Code=TSPL_SCRAPSALE_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_QTL.UOM_Code= 'QTL' 
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_BAG ON  TSPL_ITEM_UOM_BAG.Item_Code=TSPL_SCRAPSALE_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_BAG.UOM_Code= 'BAG'
						      		left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SCRAPSALE_HEAD.Loc_Code
                                    WHERE convert(date,TSPL_SCRAPSALE_HEAD.shipment_Date,103) >= convert(date,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "',103)
                                     and  convert(date,TSPL_SCRAPSALE_HEAD.shipment_Date,103) <= convert(date,'" + clsCommon.GetPrintDate(txtToDate.Value) + "',103) and TSPL_SCRAPSALE_HEAD.Loc_Code In ('" + clsCommon.myCstr(txtBillToLocation.Value) + "') "
                    qry += " " + statusScrap + " " + FG + " " + SFG + " " + FGSFG + " "

                    qry += " group by convert (date,TSPL_SCRAPSALE_HEAD.shipment_Date,103),Cust_Group_Code,Loc_Code)XX Group by XX.Location,xx.Document_Date,xx.Cust_Group_Code
                             UNION ALL
									SELECT TSPL_SD_SALE_RETURN_DETAIL.Location,max(TSPL_LOCATION_MASTER.Location_Desc)Location_Desc,max(TSPL_LOCATION_MASTER.Add1)Add1,
                                    max(TSPL_LOCATION_MASTER.Add4)Add4,convert (date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) as Document_Date,0 as SaleQty,0 as SaleQtyBag,
                                    sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SD_SALE_RETURN_DETAIL.Qty/TSPL_ITEM_UOM_QTL.Conversion_Factor) as ReturnQty,
									 sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SD_SALE_RETURN_DETAIL.Qty/TSPL_ITEM_UOM_BAG.Conversion_Factor) as ReturnQtyBag,
                                    Cust_Group_Code FROM TSPL_SD_SALE_RETURN_DETAIL
								     left outer join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Document_Code =TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE
                                     left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_RETURN_HEAD.Customer_Code
                                     LEFT OUTER JOIN TSPL_PRICE_COMPONENT_MAPPING ON TSPL_PRICE_COMPONENT_MAPPING.Price_Code=TSPL_CUSTOMER_MASTER.price_CodeNon
                                     left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_SD_SALE_RETURN_DETAIL.Unit_code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_QTL ON  TSPL_ITEM_UOM_QTL.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_QTL.UOM_Code= 'QTL'   
									 LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_BAG ON  TSPL_ITEM_UOM_BAG.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_BAG.UOM_Code= 'BAG' 
						      		 left outer join TSPL_SD_SALE_INVOICE_HEAD as LO_SD_SALE_INVOICE_HEAD on LO_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE                            
									 left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SALE_RETURN_DETAIL.Location
	                                 WHERE convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) >= convert(date,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "',103) 
                                     and convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) >= convert(date,'" + clsCommon.GetPrintDate(txtToDate.Value) + "',103)  and TSPL_SD_SALE_RETURN_DETAIL.Location In  ('" + clsCommon.myCstr(txtBillToLocation.Value) + "') "
                    qry += " " + StatusReturn + " " + FG + " " + SFG + " " + FGSFG + " "
                    qry += " group by convert (date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103),Cust_Group_Code,Location 
                             )XX group by xx.Location,xx.Document_Date,
                             xx.Cust_Group_Code)Tab1 PIVOT (SUM(Quantity) FOR Cust_Group_Code IN ([UNION],[DEALER],[GOSHAL],[DCS],[GOV],[KVSS],[AGENCY],[RETAIL],[CFP],[MISC]))AS Tab2)tmp      "

                ElseIf rbnCustgroup.Checked AndAlso rdbInvoice.IsChecked = True AndAlso rdbSaleTransfer.IsChecked = True Then
                    qry = " Select * from (SELECT 'RAJASTHAN CO-OPERATIVE DAIRY FEDERATION LIMITED' as HeadName, Location,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "' As FromDate, '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + "'as ToDate,Location_Desc as [Location Description],Add1,Add4,FORMAT(Document_Date, 'dd/MM/yyyy')as Document_Date,ISNULL ([UNION], 0) as [MILKUNION] ,ISNULL ([DEALER], 0) as [DEALER],ISNULL ([GOSHAL], 0) as [GOSHALA] ,ISNULL ([DCS], 0) as [DCS],ISNULL ([GOV], 0) as [GOVT],ISNULL ([KVSS], 0) as [KVSS], ISNULL ([AGENCY],0) AS [AGENCY],ISNULL ([RETAIL], 0) AS [RETAIL],ISNULL ([CFP], 0) AS [CFP],ISNULL ([MISC], 0) as [OTHER],
  (ISNULL ([UNION], 0) +ISNULL ([DEALER], 0)+ ISNULL ([GOSHAL], 0) + ISNULL ([DCS], 0) + ISNULL ([GOV], 0) + ISNULL ([KVSS], 0) + ISNULL ([RETAIL], 0)+ ISNULL ([CFP], 0)+ ISNULL ([AGENCY], 0)+ ISNULL ([MISC], 0)) as [Total Sale],
 QuantityBag as [Total BagSale]
  FROM (Select Location,max(Location_Desc)Location_Desc,max(Add1)Add1,max(Add4)Add4,Document_Date,Sum(SaleQty-ReturnQty)Quantity,Sum(SaleQtyBag-ReturnQtyBag)QuantityBag,Cust_Group_Code 
					  from (Select Location,max(xx.Location_Desc)Location_Desc,max(xx.Add1)Add1,max(xx.Add4)Add4,xx.Document_Date,Sum(xx.SaleQty)SaleQty,sum(xx.SaleQtyBag)SaleQtyBag,
                      sum(xx.ReturnQty)ReturnQty,sum(xx.ReturnQtyBag)ReturnQtyBag,xx.Cust_Group_Code
                                  from ( SELECT TSPL_SD_SALE_INVOICE_DETAIL.Location,max(TSPL_LOCATION_MASTER.Location_Desc)Location_Desc,max(TSPL_LOCATION_MASTER.Add1)Add1,
                      max(TSPL_LOCATION_MASTER.Add4)Add4,convert (date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as Document_Date,
                      sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SD_SALE_INVOICE_DETAIL.Qty/TSPL_ITEM_UOM_QTL.Conversion_Factor) as SaleQty,
					  sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SD_SALE_INVOICE_DETAIL.Qty/TSPL_ITEM_UOM_BAG.Conversion_Factor) as SaleQtyBag
					  ,0 as ReturnQty,0 as ReturnQtyBag,Cust_Group_Code		   
                      FROM TSPL_SD_SALE_INVOICE_DETAIL
								     left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE
                                     left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
                                     LEFT OUTER JOIN TSPL_PRICE_COMPONENT_MAPPING ON TSPL_PRICE_COMPONENT_MAPPING.Price_Code=TSPL_CUSTOMER_MASTER.price_CodeNon
                                     left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_SD_SALE_INVOICE_DETAIL.Unit_code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_QTL ON  TSPL_ITEM_UOM_QTL.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_QTL.UOM_Code= 'QTL' 
									 LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_BAG ON  TSPL_ITEM_UOM_BAG.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_BAG.UOM_Code= 'BAG' 
						      		 left outer join TSPL_SD_SALE_INVOICE_HEAD as LO_SD_SALE_INVOICE_HEAD on LO_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE                            
									 left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SALE_INVOICE_DETAIL.Location	                                 WHERE convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= convert(date,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "',103) 
                                     and  convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= convert(date,'" + clsCommon.GetPrintDate(txtToDate.Value) + "',103) and TSPL_SD_SALE_INVOICE_DETAIL.Location In  ('" + clsCommon.myCstr(txtBillToLocation.Value) + "') "
                    qry += " " + StatusInvoice + " " + FG + " " + SFG + " " + FGSFG + " "
                    qry += " group by convert (date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103),Cust_Group_Code,Location 
                             union all
									
									  SELECT TSPL_SCRAPINVOICE_HEAD.Loc_Code as Location,max(TSPL_LOCATION_MASTER.Location_Desc)Location_Desc,max(TSPL_LOCATION_MASTER.Add1)Add1,max(TSPL_LOCATION_MASTER.Add4)Add4,convert (date,TSPL_SCRAPINVOICE_HEAD.shipment_Date,103) as Document_Date,sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SCRAPINVOICE_DETAIL.shipped_Qty/TSPL_ITEM_UOM_QTL.Conversion_Factor) as SaleQty,
									 sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SCRAPINVOICE_DETAIL.shipped_Qty/TSPL_ITEM_UOM_BAG.Conversion_Factor) as SaleQtyBag
									 ,0 as ReturnQty,0 as ReturnQtyBag,Cust_Group_Code								   
								   FROM TSPL_SCRAPINVOICE_DETAIL
								     left outer join TSPL_SCRAPINVOICE_HEAD on TSPL_SCRAPINVOICE_HEAD.invoice_No =TSPL_SCRAPINVOICE_DETAIL.invoice_No
                                     left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SCRAPINVOICE_HEAD.cust_Code
                                     LEFT OUTER JOIN TSPL_PRICE_COMPONENT_MAPPING ON TSPL_PRICE_COMPONENT_MAPPING.Price_Code=TSPL_CUSTOMER_MASTER.price_CodeNon
                                     left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SCRAPINVOICE_DETAIL.Item_Code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SCRAPINVOICE_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_SCRAPINVOICE_DETAIL.Unit_code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_QTL ON  TSPL_ITEM_UOM_QTL.Item_Code=TSPL_SCRAPINVOICE_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_QTL.UOM_Code= 'QTL'
									 	 LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_BAG ON  TSPL_ITEM_UOM_BAG.Item_Code=TSPL_SCRAPINVOICE_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_BAG.UOM_Code= 'BAG' 
						      		left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SCRAPINVOICE_HEAD.Loc_Code
                                    WHERE convert(date,TSPL_SCRAPINVOICE_HEAD.shipment_Date,103) >= convert(date,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "',103)
                                     and  convert(date,TSPL_SCRAPINVOICE_HEAD.shipment_Date,103) <= convert(date,'" + clsCommon.GetPrintDate(txtToDate.Value) + "',103) and TSPL_SCRAPINVOICE_HEAD.Loc_Code In ('" + clsCommon.myCstr(txtBillToLocation.Value) + "') "
                    qry += " " + statusScrapInvoice + " " + FG + " " + SFG + " " + FGSFG + " "


                    qry += " group by convert (date,TSPL_SCRAPINVOICE_HEAD.shipment_Date,103),Cust_Group_Code,Loc_Code)XX Group by XX.Location,xx.Document_Date,xx.Cust_Group_Code 
                            UNION ALL
									SELECT TSPL_SD_SALE_RETURN_DETAIL.Location,max(TSPL_LOCATION_MASTER.Location_Desc)Location_Desc,max(TSPL_LOCATION_MASTER.Add1)Add1,
                                    max(TSPL_LOCATION_MASTER.Add4)Add4,convert (date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) as Document_Date,0 as SaleQty,0 as SaleQtyBag,
                                    sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SD_SALE_RETURN_DETAIL.Qty/TSPL_ITEM_UOM_QTL.Conversion_Factor) as ReturnQty,
									 sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SD_SALE_RETURN_DETAIL.Qty/TSPL_ITEM_UOM_BAG.Conversion_Factor) as ReturnQtyBag,
                                    Cust_Group_Code FROM TSPL_SD_SALE_RETURN_DETAIL
								     left outer join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Document_Code =TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE
                                     left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_RETURN_HEAD.Customer_Code
                                     LEFT OUTER JOIN TSPL_PRICE_COMPONENT_MAPPING ON TSPL_PRICE_COMPONENT_MAPPING.Price_Code=TSPL_CUSTOMER_MASTER.price_CodeNon
                                     left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_SD_SALE_RETURN_DETAIL.Unit_code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_QTL ON  TSPL_ITEM_UOM_QTL.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_QTL.UOM_Code= 'QTL'   
									 LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_BAG ON  TSPL_ITEM_UOM_BAG.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_BAG.UOM_Code= 'BAG' 
						      		 left outer join TSPL_SD_SALE_INVOICE_HEAD as LO_SD_SALE_INVOICE_HEAD on LO_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE                            
									 left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SALE_RETURN_DETAIL.Location
	                                 WHERE convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) >= convert(date,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "',103) 
                                     and convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) >= convert(date,'" + clsCommon.GetPrintDate(txtToDate.Value) + "',103)  and TSPL_SD_SALE_RETURN_DETAIL.Location In  ('" + clsCommon.myCstr(txtBillToLocation.Value) + "')  "
                    qry += " " + StatusReturn + " " + FG + " " + SFG + " " + FGSFG + " "
                    qry += " group by convert (date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103),Cust_Group_Code,Location 
                             )XX group by xx.Location,xx.Document_Date,xx.Cust_Group_Code )Tab1 PIVOT (SUM(Quantity) FOR Cust_Group_Code IN ([UNION],[DEALER],[GOSHAL],[DCS],[GOV],[KVSS],[AGENCY],[RETAIL],[CFP],[MISC]))AS Tab2)tmp"

                ElseIf rbnCustgroup.Checked AndAlso rdbDispatch.IsChecked = True Then
                    qry = "Select * from (SELECT 'RAJASTHAN CO-OPERATIVE DAIRY FEDERATION LIMITED' as HeadName, Location,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "' As FromDate, '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + "'as ToDate,Location_Desc as [Location Description],Add1,Add4,FORMAT(Document_Date, 'dd/MM/yyyy')as Document_Date,ISNULL ([UNION], 0) as [MILKUNION] ,ISNULL ([DEALER], 0) as [DEALER],ISNULL ([GOSHAL], 0) as [GOSHALA] ,ISNULL ([DCS], 0) as [DCS],ISNULL ([GOV], 0) as [GOVT],ISNULL ([KVSS], 0) as [KVSS], ISNULL ([AGENCY],0) AS [AGENCY],ISNULL ([RETAIL], 0) AS [RETAIL],ISNULL ([CFP], 0) AS [CFP],ISNULL ([MISC], 0) as [OTHER],
                          (ISNULL ([UNION], 0) +ISNULL ([DEALER], 0)+ ISNULL ([GOSHAL], 0) + ISNULL ([DCS], 0) + ISNULL ([GOV], 0) + ISNULL ([KVSS], 0) + ISNULL ([RETAIL], 0)+ ISNULL ([CFP], 0)+ ISNULL ([AGENCY], 0)+ ISNULL ([MISC], 0)) as [Total Sale],
                           QuantityBag as [Total BagSale]
                           FROM (SELECT XX.Location,MAX(XX.Location_Desc)Location_Desc,max(xx.Add1)Add1,max(xx.Add4)Add4,(xx.Document_Date)Document_Date,
                                   sum(xx.Quantity)Quantity,sum(xx.QuantityBag)QuantityBag,
                                    Cust_Group_Code from    
                                     ( SELECT TSPL_SD_SHIPMENT_DETAIL.Location,max(TSPL_LOCATION_MASTER.Location_Desc)Location_Desc,max(TSPL_LOCATION_MASTER.Add1)Add1,max(TSPL_LOCATION_MASTER.Add4)Add4,convert (date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as Document_Date,
                                         sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SD_SHIPMENT_DETAIL.Qty/TSPL_ITEM_UOM_QTL.Conversion_Factor) as Quantity,
										 sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SD_SHIPMENT_DETAIL.Qty/TSPL_ITEM_UOM_BAG.Conversion_Factor) as QuantityBag,
										 Cust_Group_Code								   
								   FROM TSPL_SD_SHIPMENT_DETAIL
								     left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code =TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE
                                     left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code
                                     LEFT OUTER JOIN TSPL_PRICE_COMPONENT_MAPPING ON TSPL_PRICE_COMPONENT_MAPPING.Price_Code=TSPL_CUSTOMER_MASTER.price_CodeNon
                                     left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_SD_SHIPMENT_DETAIL.Unit_code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_QTL ON  TSPL_ITEM_UOM_QTL.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_QTL.UOM_Code= 'QTL' 
									 LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_BAG ON  TSPL_ITEM_UOM_BAG.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_BAG.UOM_Code= 'BAG' 
						      		 left outer join TSPL_SD_SHIPMENT_HEAD as LO_SD_SALE_INVOICE_HEAD on LO_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE                            
									 left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SHIPMENT_DETAIL.Location
	                                 WHERE convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) >= convert(date,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "',103) 
                                     and  convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <= convert(date,'" + clsCommon.GetPrintDate(txtToDate.Value) + "',103)" + whr + " "
                    qry += " " + Status + " " + FG + " " + SFG + " " + FGSFG + " " + Stocktransferdispatch + " "
                    If rdbStockTransfer.IsChecked = True Then
                        qry += " and TSPL_SD_SHIPMENT_HEAD.Inter_unit_sale =1 "
                    Else
                        qry += " and TSPL_SD_SHIPMENT_HEAD.Inter_unit_sale =0 "
                    End If
                    qry += " group by convert (date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103),Cust_Group_Code,Location 
                            union all
                         
									 SELECT TSPL_SCRAPSALE_HEAD.Loc_Code as Location,max(TSPL_LOCATION_MASTER.Location_Desc)Location_Desc,max(TSPL_LOCATION_MASTER.Add1)Add1,max(TSPL_LOCATION_MASTER.Add4)Add4,convert (date,TSPL_SCRAPSALE_HEAD.shipment_Date,103) as Document_Date,
                                     sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SCRAPSALE_DETAIL.shipped_Qty/TSPL_ITEM_UOM_QTL.Conversion_Factor) as Quantity,
                                     Sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SCRAPSALE_DETAIL.shipped_Qty/TSPL_ITEM_UOM_BAG.Conversion_Factor) as QuantityBag,Cust_Group_Code								   
								   FROM TSPL_SCRAPSALE_DETAIL
								     left outer join TSPL_SCRAPSALE_HEAD on TSPL_SCRAPSALE_HEAD.shipment_No =TSPL_SCRAPSALE_DETAIL.shipment_No
                                     left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SCRAPSALE_HEAD.cust_Code
                                     LEFT OUTER JOIN TSPL_PRICE_COMPONENT_MAPPING ON TSPL_PRICE_COMPONENT_MAPPING.Price_Code=TSPL_CUSTOMER_MASTER.price_CodeNon
                                     left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SCRAPSALE_DETAIL.Item_Code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SCRAPSALE_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_SCRAPSALE_DETAIL.Unit_code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_QTL ON  TSPL_ITEM_UOM_QTL.Item_Code=TSPL_SCRAPSALE_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_QTL.UOM_Code= 'QTL' 
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_BAG ON  TSPL_ITEM_UOM_BAG.Item_Code=TSPL_SCRAPSALE_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_BAG.UOM_Code= 'BAG' 
						      		left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SCRAPSALE_HEAD.Loc_Code
	                                 WHERE convert(date,TSPL_SCRAPSALE_HEAD.shipment_Date,103) >= convert(date,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "',103)  
                                     and  convert(date,TSPL_SCRAPSALE_HEAD.shipment_Date,103) <= convert(date,'" + clsCommon.GetPrintDate(txtToDate.Value) + "',103)
                                     and TSPL_SCRAPSALE_HEAD.Loc_Code In ('" + clsCommon.myCstr(txtBillToLocation.Value) + "') "
                    qry += " " + statusScrap + " " + FG + " " + SFG + " " + FGSFG + ""
                    If rdbStockTransfer.IsChecked = True Then
                        qry += " and TSPL_SCRAPSALE_HEAD.Inter_unit_sale =1 "
                    Else
                        qry += " and TSPL_SCRAPSALE_HEAD.Inter_unit_sale =0 "
                    End If

                    qry += " group by convert (date,TSPL_SCRAPSALE_HEAD.shipment_Date,103),Cust_Group_Code,Loc_Code )XX GROUP BY xx.Document_Date,XX.Cust_Group_Code,XX.Location )Tab1
                             PIVOT (SUM(Quantity) FOR Cust_Group_Code IN ([UNION],[DEALER],[GOSHAL],[DCS],[GOV],[KVSS],[AGENCY],[RETAIL],[CFP],[MISC]))AS Tab2)tmp  "
                ElseIf rbnCustgroup.Checked AndAlso rdbInvoice.IsChecked = True Then

                    qry = " Select * from (SELECT 'RAJASTHAN CO-OPERATIVE DAIRY FEDERATION LIMITED' as HeadName, Location,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "' As FromDate, '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + "'as ToDate,Location_Desc as [Location Description],Add1,Add4,FORMAT(Document_Date, 'dd/MM/yyyy')as Document_Date,ISNULL ([UNION], 0) as [MILKUNION] ,ISNULL ([DEALER], 0) as [DEALER],ISNULL ([GOSHAL], 0) as [GOSHALA] ,ISNULL ([DCS], 0) as [DCS],ISNULL ([GOV], 0) as [GOVT],ISNULL ([KVSS], 0) as [KVSS], ISNULL ([AGENCY],0) AS [AGENCY],ISNULL ([RETAIL], 0) AS [RETAIL],ISNULL ([CFP], 0) AS [CFP],ISNULL ([MISC], 0) as [OTHER],
  (ISNULL ([UNION], 0) +ISNULL ([DEALER], 0)+ ISNULL ([GOSHAL], 0) + ISNULL ([DCS], 0) + ISNULL ([GOV], 0) + ISNULL ([KVSS], 0) + ISNULL ([RETAIL], 0)+ ISNULL ([CFP], 0)+ ISNULL ([AGENCY], 0)+ ISNULL ([MISC], 0)) as [Total Sale],
QuantityBag as [Total BagSale]
  FROM                     (SELECT XX.Location,MAX(XX.Location_Desc)Location_Desc,max(xx.Add1)Add1,max(xx.Add4)Add4,(xx.Document_Date)Document_Date,
                                   sum(xx.Quantity)Quantity,Sum(xx.QuantityBag)QuantityBag,
                                    Cust_Group_Code FROM               ( SELECT TSPL_SD_SALE_INVOICE_DETAIL.Location,max(TSPL_LOCATION_MASTER.Location_Desc)Location_Desc,max(TSPL_LOCATION_MASTER.Add1)Add1,max(TSPL_LOCATION_MASTER.Add4)Add4,
                                    convert (date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as Document_Date,
                                   sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SD_SALE_INVOICE_DETAIL.Qty/TSPL_ITEM_UOM_QTL.Conversion_Factor) as Quantity,
                                    Sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SD_SALE_INVOICE_DETAIL.Qty/TSPL_ITEM_UOM_BAG.Conversion_Factor) as QuantityBag,Cust_Group_Code									   
								   FROM TSPL_SD_SALE_INVOICE_DETAIL
								     left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE
                                     left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
                                     LEFT OUTER JOIN TSPL_PRICE_COMPONENT_MAPPING ON TSPL_PRICE_COMPONENT_MAPPING.Price_Code=TSPL_CUSTOMER_MASTER.price_CodeNon
                                     left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_SD_SALE_INVOICE_DETAIL.Unit_code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_QTL ON  TSPL_ITEM_UOM_QTL.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_QTL.UOM_Code= 'QTL'  
									  LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_BAG ON  TSPL_ITEM_UOM_BAG.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_BAG.UOM_Code= 'BAG'
						      		 left outer join TSPL_SD_SALE_INVOICE_HEAD as LO_SD_SALE_INVOICE_HEAD on LO_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE                            
									 left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SALE_INVOICE_DETAIL.Location
	                                 WHERE convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= convert(date,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "',103) 
                                     and  convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= convert(date,'" + clsCommon.GetPrintDate(txtToDate.Value) + "',103)" + whr + " "
                    qry += " " + StatusInvoice + " " + FG + " " + SFG + " " + FGSFG + " "
                    If rdbStockTransfer.IsChecked = True Then
                        qry += " and TSPL_SD_SALE_INVOICE_HEAD.Inter_unit_sale=1 "
                    Else
                        qry += " and TSPL_SD_SALE_INVOICE_HEAD.Inter_unit_sale=0 "
                    End If
                    qry += " group by convert (date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103),Cust_Group_Code,Location

 union all
							 SELECT TSPL_SCRAPINVOICE_HEAD.Loc_Code as Location,max(TSPL_LOCATION_MASTER.Location_Desc)Location_Desc,max(TSPL_LOCATION_MASTER.Add1)Add1,max(TSPL_LOCATION_MASTER.Add4)Add4,convert (date,TSPL_SCRAPINVOICE_HEAD.shipment_Date,103) as Document_Date,
                                   sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SCRAPINVOICE_DETAIL.shipped_Qty/TSPL_ITEM_UOM_QTL.Conversion_Factor) as Quantity,
                                   Sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SCRAPINVOICE_DETAIL.shipped_Qty/TSPL_ITEM_UOM_BAG.Conversion_Factor) as QuantityBag,Cust_Group_Code								   
								   FROM TSPL_SCRAPINVOICE_DETAIL
								     left outer join TSPL_SCRAPINVOICE_HEAD on TSPL_SCRAPINVOICE_HEAD.invoice_No =TSPL_SCRAPINVOICE_DETAIL.invoice_No
                                     left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SCRAPINVOICE_HEAD.cust_Code
                                     LEFT OUTER JOIN TSPL_PRICE_COMPONENT_MAPPING ON TSPL_PRICE_COMPONENT_MAPPING.Price_Code=TSPL_CUSTOMER_MASTER.price_CodeNon
                                     left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SCRAPINVOICE_DETAIL.Item_Code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SCRAPINVOICE_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_SCRAPINVOICE_DETAIL.Unit_code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_QTL ON  TSPL_ITEM_UOM_QTL.Item_Code=TSPL_SCRAPINVOICE_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_QTL.UOM_Code= 'QTL'
                                    LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_BAG ON  TSPL_ITEM_UOM_BAG.Item_Code=TSPL_SCRAPINVOICE_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_BAG.UOM_Code= 'BAG'
						      		left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SCRAPINVOICE_HEAD.Loc_Code
                                    WHERE convert(date,TSPL_SCRAPINVOICE_HEAD.shipment_Date,103) >= convert(date,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "',103) 
                                     and  convert(date,TSPL_SCRAPINVOICE_HEAD.shipment_Date,103) <= convert(date,'" + clsCommon.GetPrintDate(txtToDate.Value) + "',103)
                                     and TSPL_SCRAPINVOICE_HEAD.Loc_Code In ('" + clsCommon.myCstr(txtBillToLocation.Value) + "') "
                    qry += " " + statusScrapInvoice + " " + FG + " " + SFG + " " + FGSFG + " "
                    If rdbStockTransfer.IsChecked = True Then
                        qry += " and TSPL_SCRAPINVOICE_HEAD.Inter_unit_sale=1 "
                    Else
                        qry += " and TSPL_SCRAPINVOICE_HEAD.Inter_unit_sale=0 "
                    End If
                    qry += " group by convert (date,TSPL_SCRAPINVOICE_HEAD.shipment_Date,103),Cust_Group_Code,Loc_Code )XX GROUP BY xx.Document_Date,XX.Cust_Group_Code,XX.Location "

                    qry += " )Tab1
                             PIVOT (SUM(Quantity) FOR Cust_Group_Code IN ([UNION],[DEALER],[GOSHAL],[DCS],[GOV],[KVSS],[AGENCY],[RETAIL],[CFP],[MISC]))AS Tab2)tmp"

                ElseIf rbnCustgroup.Checked AndAlso rdbSaleReturn.IsChecked = True Then
                    qry = "Select 'RAJASTHAN CO-OPERATIVE DAIRY FEDERATION LIMITED' as HeadName,Location,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "' As FromDate,
                          '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + "'  As ToDate,max([Location Description])[Location Description],max(Add1)Add1,max(Add4)Add4,
                           max(Document_Date)Document_Date," & itemNames4 & ",Sum([Total Sale])[Total Sale],sum([Total BagSale])[Total BagSale] 
                           from (SELECT  Location,Location_Desc as [Location Description],Add1,Add4,FORMAT(Document_Date, 'dd/MM/yyyy')as Document_Date,
                           " & itemNames2 & "," & itemNames3 & " as [Total Sale],
                        QuantityBag as [Total BagSale]
                          FROM       (SELECT XX.Location,MAX(XX.Location_Desc)Location_Desc,max(xx.Add1)Add1,max(xx.Add4)Add4,(xx.Document_Date)Document_Date,
                                   sum(xx.Quantity)Quantity,sum(xx.QuantityBag)QuantityBag,Cust_Group_Code FROM 
             ( SELECT TSPL_SD_SALE_RETURN_DETAIL.Location,max(TSPL_LOCATION_MASTER.Location_Desc)Location_Desc,max(TSPL_LOCATION_MASTER.Add1)Add1,
                                    max(TSPL_LOCATION_MASTER.Add4)Add4,convert (date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) as Document_Date,
                                         sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SD_SALE_RETURN_DETAIL.Qty/TSPL_ITEM_UOM_QTL.Conversion_Factor) as Quantity,
                                         Sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SD_SALE_RETURN_DETAIL.Qty/TSPL_ITEM_UOM_BAG.Conversion_Factor) as QuantityBag,
                                         Cust_Group_Code								   
								   FROM TSPL_SD_SALE_RETURN_DETAIL
								     left outer join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Document_Code =TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE
                                     left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_RETURN_HEAD.Customer_Code
                                     LEFT OUTER JOIN TSPL_PRICE_COMPONENT_MAPPING ON TSPL_PRICE_COMPONENT_MAPPING.Price_Code=TSPL_CUSTOMER_MASTER.price_CodeNon
                                     left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_SD_SALE_RETURN_DETAIL.Unit_code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_QTL ON  TSPL_ITEM_UOM_QTL.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_QTL.UOM_Code= 'QTL' 
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_BAG ON  TSPL_ITEM_UOM_BAG.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_BAG.UOM_Code= 'BAG'
						      		 left outer join TSPL_SD_SALE_RETURN_HEAD as LO_SD_SALE_INVOICE_HEAD on LO_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE                            
									 left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SALE_RETURN_DETAIL.Location
	                                 WHERE convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) >= convert(date,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "',103) 
                                     and  convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) <= convert(date,'" + clsCommon.GetPrintDate(txtToDate.Value) + "',103)" + whr + " "
                    qry += " " + StatusReturn + " " + FG + " " + SFG + " " + FGSFG + " "

                    qry += " group by convert (date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103),Cust_Group_Code,Location 

union all

                                    SELECT TSPL_SCRAPSALE_HEAD_RETURN.Loc_Code,max(TSPL_LOCATION_MASTER.Location_Desc)Location_Desc,max(TSPL_LOCATION_MASTER.Add1)Add1,
                                    max(TSPL_LOCATION_MASTER.Add4)Add4,convert (date,TSPL_SCRAPSALE_HEAD_RETURN.Return_ship_Date,103) as Document_Date,
                                    sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SCRAPSALE_DETAIL_RETURN.shipped_Qty/TSPL_ITEM_UOM_QTL.Conversion_Factor) as Quantity,
									Sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SCRAPSALE_DETAIL_RETURN.shipped_Qty/TSPL_ITEM_UOM_BAG.Conversion_Factor) as QuantityBag,
									Cust_Group_Code								   
								   FROM TSPL_SCRAPSALE_DETAIL_RETURN
								     left outer join TSPL_SCRAPSALE_HEAD_RETURN on TSPL_SCRAPSALE_HEAD_RETURN.Document_No =TSPL_SCRAPSALE_DETAIL_RETURN.Document_No
                                     left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SCRAPSALE_HEAD_RETURN.cust_Code
                                     LEFT OUTER JOIN TSPL_PRICE_COMPONENT_MAPPING ON TSPL_PRICE_COMPONENT_MAPPING.Price_Code=TSPL_CUSTOMER_MASTER.price_CodeNon
                                     left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SCRAPSALE_DETAIL_RETURN.Item_Code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SCRAPSALE_DETAIL_RETURN.Item_Code 
                                     AND TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_SCRAPSALE_DETAIL_RETURN.Unit_code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_QTL ON  TSPL_ITEM_UOM_QTL.Item_Code=TSPL_SCRAPSALE_DETAIL_RETURN.Item_Code 
                                     AND TSPL_ITEM_UOM_QTL.UOM_Code= 'QTL'  
									  LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_BAG ON  TSPL_ITEM_UOM_BAG.Item_Code=TSPL_SCRAPSALE_DETAIL_RETURN.Item_Code 
                                     AND TSPL_ITEM_UOM_BAG.UOM_Code= 'BAG'
						      		 left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SCRAPSALE_HEAD_RETURN.Loc_Code
                                     WHERE convert(date,TSPL_SCRAPSALE_HEAD_RETURN.Return_ship_Date,103) >= convert(date,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "',103) 
                                     and  convert(date,TSPL_SCRAPSALE_HEAD_RETURN.Return_ship_Date,103) <= convert(date,'" + clsCommon.GetPrintDate(txtToDate.Value) + "',103)  And TSPL_SCRAPSALE_HEAD_RETURN.Loc_Code In ('" + clsCommon.myCstr(txtBillToLocation.Value) + "') "

                    qry += " " + StatusReturn1 + " " + FG + " " + SFG + " " + FGSFG + " "
                    qry += " group by convert (date,TSPL_SCRAPSALE_HEAD_RETURN.Return_ship_Date,103),Cust_Group_Code,Loc_Code)XX GROUP BY xx.Document_Date,XX.Cust_Group_Code,XX.Location  "

                    qry += " )Tab1
                             PIVOT (SUM(Quantity) FOR Cust_Group_Code IN (" & itemNames1 & "))AS Tab2)tmp group by location "
                End If


                '      qry = " (SELECT TSPL_SD_SHIPMENT_DETAIL.Location,TSPL_LOCATION_MASTER.Location_Desc,TSPL_LOCATION_MASTER.Add1,TSPL_LOCATION_MASTER.Add4,convert (date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as Document_Date,sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SD_SHIPMENT_DETAIL.Qty/TSPL_ITEM_UOM_QTL.Conversion_Factor) as Quantity,Cust_Group_Code                                 
                ' FROM TSPL_SD_SHIPMENT_DETAIL
                '   left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code =TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE
                '                           left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code
                '                           LEFT OUTER JOIN TSPL_PRICE_COMPONENT_MAPPING ON TSPL_PRICE_COMPONENT_MAPPING.Price_Code=TSPL_CUSTOMER_MASTER.price_CodeNon
                '                           left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code
                '                           LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code 
                '                               AND TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_SD_SHIPMENT_DETAIL.Unit_code
                '                           LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_QTL ON  TSPL_ITEM_UOM_QTL.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code 
                '                           AND TSPL_ITEM_UOM_QTL.UOM_Code= 'QTL'    
                '  		 left outer join TSPL_SD_SHIPMENT_HEAD as LO_SD_SALE_INVOICE_HEAD on LO_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE                            
                'left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SHIPMENT_DETAIL.Location
                '                        WHERE  TSPL_SD_SHIPMENT_HEAD.Document_Date >= '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' 
                '                           and  TSPL_SD_SHIPMENT_HEAD.Document_Date <= '" + clsCommon.GetPrintDate(txtToDate.Value) + "'" + whr + " "
                '      qry += " " + Status + " " + FG + " " + SFG + " " + FGSFG + " "

                '      qry += " group by convert (date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103),Cust_Group_Code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_LOCATION_MASTER.Add1,TSPL_LOCATION_MASTER.Add4,Location)Tab1
                '                          PIVOT (SUM(Quantity) FOR Cust_Group_Code IN ([UNION],[DEALER],[GOSHAL],[DCS],[GOV],[KVSS],[AGENCY],[RETAIL],[CFP],[MISC]))AS Tab2)tmp " 


            End If
            If clsCommon.myLen(qry) > 0 Then
                dt = clsDBFuncationality.GetDataTable(qry)
            End If

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If PCGroup = False Then
                    Gv1.DataSource = Nothing
                    Gv1.GroupDescriptors.Clear()
                    Gv1.SummaryRowsBottom.Clear()
                    Gv1.DataSource = dt
                    'gv1.Columns("TransType").IsVisible = False
                    'gv1.Columns("PROD_ENTRY_CODE").IsVisible = False
                    RadPageView1.SelectedPage = RadPageViewPage2
                    Gv1.BestFitColumns()
                    FormatGrid()
                    EnableDisableCntrl(False)
                    'ReStoreGridLayout()
                Else
                    If rbnPricegroup.Checked Then
                        Dim frmCRV As New frmCrystalReportViewer()
                        frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.SalesReport, dt, "rptSalesReport", "")
                        frmCRV = Nothing
                    Else
                        Dim frmCRV As New frmCrystalReportViewer()
                        frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.SalesReport, dt, "rptSalesReportCustGroup", "")
                        frmCRV = Nothing
                    End If

                End If
            Else
                clsCommon.MyMessageBoxShow("No data found to display.", "Sales Report")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try


    End Sub
    Sub EnableDisableCntrl(ByVal val As Boolean)
        RadGroupBox3.Enabled = val
        RadGroupBox4.Enabled = val
        RadGroupBox5.Enabled = val
        RadGroupBox6.Enabled = val
        RadGroupBox1.Enabled = val
        RadGroupBox2.Enabled = val
        txtToDate.Enabled = val
        txtFromDate.Enabled = val
        txtBillToLocation.Enabled = val
        lblBillToLocation.Enabled = val

    End Sub
    Sub FormatGrid()

        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = True
        Next

        Gv1.Columns("Location Description").Width = 100
        Gv1.Columns("Location Description").IsVisible = False
        Gv1.Columns("Location Description").HeaderText = "Location Description"

        Gv1.Columns("FromDate").Width = 100
        Gv1.Columns("FromDate").IsVisible = False
        Gv1.Columns("FromDate").HeaderText = "FromDate"

        Gv1.Columns("ToDate").Width = 100
        Gv1.Columns("ToDate").IsVisible = False
        Gv1.Columns("ToDate").HeaderText = "ToDate"

        Gv1.Columns("Add1").Width = 100
        Gv1.Columns("Add1").IsVisible = False
        Gv1.Columns("Add1").HeaderText = "Add1"

        Gv1.Columns("Add4").Width = 100
        Gv1.Columns("Add4").IsVisible = False
        Gv1.Columns("Add4").HeaderText = "Add4"

        Gv1.Columns("Document_Date").HeaderText = "Invoice Date"

        'Gv1.Columns("Location").Width = 100
        'Gv1.Columns("Location").IsVisible = True
        'Gv1.Columns("Location").HeaderText = "Location"

        'Gv1.Columns("Document_Date").Width = 100
        'Gv1.Columns("Document_Date").IsVisible = True
        'Gv1.Columns("Document_Date").HeaderText = "Invoice Date"

        'Gv1.Columns("MILKUNION").Width = 150
        'Gv1.Columns("MILKUNION").IsVisible = True
        'Gv1.Columns("MILKUNION").HeaderText = "MILK UNION(Qtl)"

        'If rbnCustgroup.Checked Then
        '    Gv1.Columns("DEALER").Width = 150
        '    Gv1.Columns("DEALER").IsVisible = True
        '    Gv1.Columns("DEALER").HeaderText = "DEALER(Qtl)"

        '    Gv1.Columns("AGENCY").Width = 150
        '    Gv1.Columns("AGENCY").IsVisible = True
        '    Gv1.Columns("AGENCY").HeaderText = "AGENCY(Qtl)"

        '    Gv1.Columns("RETAIL").Width = 150
        '    Gv1.Columns("RETAIL").IsVisible = True
        '    Gv1.Columns("RETAIL").HeaderText = "RETAIL(Qtl)"

        '    Gv1.Columns("CFP").Width = 150
        '    Gv1.Columns("CFP").IsVisible = True
        '    Gv1.Columns("CFP").HeaderText = "CFP(Qtl)"
        'End If

        'Gv1.Columns("GOSHALA").Width = 150
        'Gv1.Columns("GOSHALA").IsVisible = True
        'Gv1.Columns("GOSHALA").HeaderText = "GOSHALA(Qtl)"

        'Gv1.Columns("DCS").Width = 150
        'Gv1.Columns("DCS").IsVisible = True
        'Gv1.Columns("DCS").HeaderText = "DCS(Qtl)"

        'Gv1.Columns("GOVT").Width = 150
        'Gv1.Columns("GOVT").IsVisible = True
        'Gv1.Columns("GOVT").HeaderText = "GOVT(Qtl)"

        'Gv1.Columns("KVSS").Width = 150
        'Gv1.Columns("KVSS").IsVisible = True
        'Gv1.Columns("KVSS").HeaderText = "KVSS(Qtl)"

        'Gv1.Columns("OTHER").Width = 150
        'Gv1.Columns("OTHER").IsVisible = True
        'Gv1.Columns("OTHER").HeaderText = "OTHER(Qtl)"

        'Gv1.Columns("Total Sale").Width = 150
        'Gv1.Columns("Total Sale").IsVisible = True
        'Gv1.Columns("Total Sale").HeaderText = "Total Sale(Qtl)"

        'Gv1.Columns("Total BagSale").Width = 150
        'Gv1.Columns("Total BagSale").IsVisible = True
        'Gv1.Columns("Total BagSale").HeaderText = "Total Sale(Bag)"

        Dim summaryRowItem As New GridViewSummaryRowItem()

        'Dim item1 As New GridViewSummaryItem("Total Sale", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item1)

        'Dim item2 As New GridViewSummaryItem("OTHER", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item2)

        'Dim item3 As New GridViewSummaryItem("KVSS", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item3)

        'Dim item4 As New GridViewSummaryItem("GOVT", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item4)

        'Dim item5 As New GridViewSummaryItem("DCS", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item5)

        'Dim item6 As New GridViewSummaryItem("GOSHALA", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item6)

        'Dim item7 As New GridViewSummaryItem("MILKUNION", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item7)

        'Dim item8 As New GridViewSummaryItem("Total BagSale", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item8)
        'If rbnCustgroup.Checked = True Then
        '    Dim item11 As New GridViewSummaryItem("DEALER", "{0:F2}", GridAggregateFunction.Sum)
        '    summaryRowItem.Add(item11)

        '    Dim item12 As New GridViewSummaryItem("AGENCY", "{0:F2}", GridAggregateFunction.Sum)
        '    summaryRowItem.Add(item12)

        '    Dim item13 As New GridViewSummaryItem("RETAIL", "{0:F2}", GridAggregateFunction.Sum)
        '    summaryRowItem.Add(item13)

        '    Dim item14 As New GridViewSummaryItem("CFP", "{0:F2}", GridAggregateFunction.Sum)
        '    summaryRowItem.Add(item14)
        'End If
        Dim index As Integer = 5
        For ii As Integer = index To Gv1.Columns.Count - 1
            summaryRowItem.Add(New GridViewSummaryItem(Gv1.Columns(ii).Name, "{0:F2}", GridAggregateFunction.Sum))
        Next
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub


    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "Admin") = CompairStringResult.Equal Then
                    arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

                Else
                    'Dim strLocDesc As String = clsDBFuncationality.getSingleValue("select Location_Desc from tspl_location_master where Location_Code in (" + objCommonVar.strCurrUserLocations + ")")
                    'arrHeader.Add("Location : " + strLocDesc)
                End If

                If txtBillToLocation.Value IsNot Nothing AndAlso txtBillToLocation.Value.Count > 0 Then
                    arrHeader.Add(" Location : " + clsCommon.myCstr(lblBillToLocation.Text))
                End If
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("Sales Report", Gv1, arrHeader, Me.Text)
                ' transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                common.clsCommon.MyMessageBoxShow("Exported Successfully.", Me.Text)
                'Process.Start(filePath)

            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try

            If Gv1.Rows.Count > 0 Then

                Dim arrHeader As List(Of String) = New List(Of String)()
                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "Admin") = CompairStringResult.Equal Then
                    arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                Else
                    'Dim strLocDesc As String = clsDBFuncationality.getSingleValue("select Location_Desc from tspl_location_master where Location_Code in (" + objCommonVar.strCurrUserLocations + ")")
                    'arrHeader.Add("Location : " + strLocDesc)
                End If
                '
                If txtBillToLocation.Value IsNot Nothing AndAlso txtBillToLocation.Value.Count > 0 Then
                    arrHeader.Add("Location : " + clsCommon.myCstr(lblBillToLocation.Text))
                End If
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Sales Report", Gv1, arrHeader, "Sales Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Load_Sales_Report(True)
        '        Try
        '            Dim whr As String = ""
        '            If txtBillToLocation.Value IsNot Nothing AndAlso txtBillToLocation.Value.Count > 0 Then
        '                whr += " and TSPL_SD_SALE_INVOICE_DETAIL.Location In   ('" + clsCommon.myCstr(txtBillToLocation.Value) + "') "
        '            Else
        '                'whr += " and TSPL_LOCATION_MASTER.Location_Type='Physical'  "
        '                'If clsCommon.myLen(arrLoc) > 0 Then
        '                'whr += "  and  LOCATION_CODE IN (" + clsCommon.GetMulcallStringWithComma(TxtMultiLocation.arrValueMember) + ") "
        '                'End If
        '            End If

        '            Dim qry As String = "  Select * from (SELECT 'RAJASTHAN CO-OPERATIVE DAIRY FEDERATION LIMITED' as HeadName, Location,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "' As FromDate, '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + "'  As ToDate,Location_Desc as [Location Description],Add1,Add4,FORMAT(Document_Date, 'dd/MM/yyyy')as Document_Date,ISNULL ([MILKUNION], 0) as [MILKUNION] ,ISNULL ([GOSHALA], 0) as [GOSHALA] ,ISNULL ([DCS], 0) as [DCS],ISNULL ([GOVT], 0) as [GOVT],ISNULL ([KVSS], 0) as [KVSS], ISNULL ([OTHER], 0) as [OTHER],
        '  (ISNULL ([MILKUNION], 0) + ISNULL ([GOSHALA], 0) + ISNULL ([DCS], 0) + ISNULL ([GOVT], 0) + ISNULL ([KVSS], 0) + ISNULL ([OTHER], 0)) as [Total Sale],
        '((ISNULL ([MILKUNION], 0) + ISNULL ([GOSHALA], 0) + ISNULL ([DCS], 0) + ISNULL ([GOVT], 0) + ISNULL ([KVSS], 0) + ISNULL ([OTHER], 0))/50)*100 as [Total BagSale]
        '  FROM
        '                                   (SELECT TSPL_SD_SALE_INVOICE_DETAIL.Location,TSPL_LOCATION_MASTER.Location_Desc,TSPL_LOCATION_MASTER.Add1,TSPL_LOCATION_MASTER.Add4,convert (date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as Document_Date,sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SD_SALE_INVOICE_DETAIL.Qty)/100 as Quantity,price_CodeNon								   
        '								   FROM TSPL_SD_SALE_INVOICE_DETAIL
        '								     left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE
        '                                     left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
        '                                     LEFT OUTER JOIN TSPL_PRICE_COMPONENT_MAPPING ON TSPL_PRICE_COMPONENT_MAPPING.Price_Code=TSPL_CUSTOMER_MASTER.price_CodeNon
        '                                     left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
        '                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code 
        '                                     AND TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_SD_SALE_INVOICE_DETAIL.Unit_code
        '						      		 left outer join TSPL_SD_SALE_INVOICE_HEAD as LO_SD_SALE_INVOICE_HEAD on LO_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE                            
        '									 left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SALE_INVOICE_DETAIL.Location
        '	                                 WHERE  TSPL_SD_SALE_INVOICE_HEAD.Document_Date >= '" + clsCommon.GetPrintDate(txtFromDate.Value) + "'  and  TSPL_SD_SALE_INVOICE_HEAD.Document_Date <= '" + clsCommon.GetPrintDate(txtToDate.Value) + "'" + whr + "

        '									 group by convert (date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103),price_CodeNon,TSPL_LOCATION_MASTER.Location_Desc,TSPL_LOCATION_MASTER.Add1,TSPL_LOCATION_MASTER.Add4,Location)Tab1
        '                                    PIVOT (SUM(Quantity) FOR price_CodeNon IN ([MILKUNION],[GOSHALA],[DCS],[GOVT],[KVSS],[OTHER]))AS Tab2)tmp  "

        '            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        '            If dt IsNot Nothing And dt.Rows.Count > 0 Then
        '                Dim frmCRV As New frmCrystalReportViewer()
        '                frmCRV.funreport(CrystalReportFolder.SalesReport, dt, "rptSalesReport", "")
        '                frmCRV = Nothing
        '            Else
        '                clsCommon.MyMessageBoxShow("No Data Found")
        '            End If
        '        Catch ex As Exception
        '            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        '        End Try
    End Sub

    Private Sub txtBillToLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtBillToLocation._MYValidating

        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        Dim WhrCls As String = " Location_Type='Physical'  "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If


        txtBillToLocation.Value = clsCommon.ShowSelectForm("VendorMafnd", qry, "Code", WhrCls, txtBillToLocation.Value, "Code", isButtonClicked)
        lblBillToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtBillToLocation.Value + "'"))

    End Sub

    Private Sub rptSalesReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtBillToLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
        If clsCommon.myLen(txtBillToLocation.Value) > 0 Then
            lblBillToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_Location_Master where Location_Code='" + txtBillToLocation.Value + "' "))
        End If
        rbnPricegroup.Checked = True
        rbnCustgroup.Checked = False
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