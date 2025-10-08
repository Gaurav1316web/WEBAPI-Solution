Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Public Class rptDealerSalesReport
    Inherits FrmMainTranScreen
    Private Sub rptDealerSalesReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtToDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        txtFromDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Load_Dealer_Sales_Report(False)

    End Sub
    Private Sub Load_Dealer_Sales_Report(ByVal Print As Boolean)
        Dim qry As String = ""
        Dim dt As New DataTable()
        txtFromDate.Enabled = False
        txtToDate.Enabled = False
        Dim location As String = Nothing
        Dim location1 As String = Nothing
        Dim customer As String = Nothing
        Dim FromDate As String = clsCommon.myCstr(txtFromDate.Text)
        Dim TODate As String = clsCommon.myCstr(txtToDate.Text)
        If clsCommon.myLen(txtBillToLocation.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Plz Select Location.", Me.Text)
            txtBillToLocation.Focus()
            Exit Sub
        End If

        Try

            qry = "(SELECT   '" + objCommonVar.CurrentUser + "' as username ,'" + FromDate + "' AS FromDate, '" + TODate + " ' as ToDate,   ROW_NUMBER() OVER (ORDER BY Customer_Code) AS serial_number,  Customer_Code,MAX(CUSTOMER_NAME)CUSTOMER_NAME,max(XX.Location)Location,MAX(XX.Location_Desc)Location_Desc,max(xx.Add1)Add1,max(xx.Add2)Add2,
                                     max(xx.add1+xx.add2)COMPANYLocation,
                                     max(xx.Add4)Add4,max(xx.Document_Date)Document_Date,max(xx.CustAdd2+xx.CustAdd1+xx.CustAdd3)place,
                                     cast(sum(xx.Quantity)as decimal(10,2))Quantity,max(price_CodeNon)price_CodeNon,max(xx.HeadName)HeadName FROM 
									
                                   (SELECT TSPL_SD_SHIPMENT_HEAD.Customer_Code,(TSPL_CUSTOMER_MASTER.CUSTOMER_NAME)CUSTOMER_NAME, 
                                    (TSPL_SD_SHIPMENT_DETAIL.Location)Location,(TSPL_LOCATION_MASTER.Location_Desc)Location_Desc,(TSPL_LOCATION_MASTER.Add1)Add1,(TSPL_LOCATION_MASTER.Add2)Add2,(TSPL_LOCATION_MASTER.Add4)Add4,
                                    (convert (date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103)) as Document_Date,
                                     (TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SD_SHIPMENT_DETAIL.Qty/TSPL_ITEM_UOM_MT.Conversion_Factor) as Quantity,
                                     (price_CodeNon )price_CodeNon,'RAJASTHAN CO-OPERATIVE DAIRY FEDERATION LIMITED' as HeadName
      	                            ,(TSPL_CUSTOMER_MASTER.Add1) as custAdd1 ,(TSPL_CUSTOMER_MASTER.add2) as custadd2 ,(TSPL_CUSTOMER_MASTER.add3 ) as custadd3                              
                                     FROM TSPL_SD_SHIPMENT_DETAIL
								     left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code =TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE
                                     left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code
                                     LEFT OUTER JOIN TSPL_PRICE_COMPONENT_MAPPING ON TSPL_PRICE_COMPONENT_MAPPING.Price_Code=TSPL_CUSTOMER_MASTER.price_CodeNon
                                     left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_SD_SHIPMENT_DETAIL.Unit_code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_MT ON  TSPL_ITEM_UOM_MT.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_MT.UOM_Code= 'MT' 
						      		 left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SHIPMENT_DETAIL.Location
	                                 WHERE  convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) >= convert(date,'" + txtFromDate.Value + "',103)   
                                     and  convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <= convert(date,'" + txtToDate.Value + "',103) "
            If clsCommon.myLen(txtBillToLocation.Value) > 0 Then
                qry += "  and TSPL_SD_SHIPMENT_DETAIL.Location In ('" + clsCommon.myCstr(txtBillToLocation.Value) + "') "
            End If
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                qry += "  and TSPL_CUSTOMER_MASTER.cust_code In  (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")  "

            End If


            qry += "  AND TSPL_SD_SHIPMENT_HEAD.Status=1   and TSPL_Item_Master.FG_for_CF_RPT=1  And TSPL_SD_SHIPMENT_HEAD.Inter_unit_sale=0 
									  
						              union all
                         			  SELECT TSPL_SCRAPSALE_HEAD.cust_Code,(TSPL_CUSTOMER_MASTER.CUSTOMER_NAME)CUSTOMER_NAME,(TSPL_SCRAPSALE_HEAD.Loc_Code) as Location,(TSPL_LOCATION_MASTER.Location_Desc)Location_Desc,(TSPL_LOCATION_MASTER.Add1)Add1,(TSPL_LOCATION_MASTER.Add2)Add2,(TSPL_LOCATION_MASTER.Add4)Add4,(convert (date,TSPL_SCRAPSALE_HEAD.shipment_Date,103)) as Document_Date,
                                      (TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SCRAPSALE_DETAIL.shipped_Qty/TSPL_ITEM_UOM_MT.Conversion_Factor) as Quantity,
                                      (price_CodeNon)price_CodeNon,'RAJASTHAN CO-OPERATIVE DAIRY FEDERATION LIMITED' as HeadName								   
									 ,(TSPL_CUSTOMER_MASTER.Add1) as custAdd1 ,(TSPL_CUSTOMER_MASTER.add2) as custadd2 ,(TSPL_CUSTOMER_MASTER.add3 ) as custadd3   
									 FROM TSPL_SCRAPSALE_DETAIL
								     left outer join TSPL_SCRAPSALE_HEAD on TSPL_SCRAPSALE_HEAD.shipment_No =TSPL_SCRAPSALE_DETAIL.shipment_No
                                     left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SCRAPSALE_HEAD.cust_Code
                                     LEFT OUTER JOIN TSPL_PRICE_COMPONENT_MAPPING ON TSPL_PRICE_COMPONENT_MAPPING.Price_Code=TSPL_CUSTOMER_MASTER.price_CodeNon
                                     left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SCRAPSALE_DETAIL.Item_Code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SCRAPSALE_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_SCRAPSALE_DETAIL.Unit_code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_MT ON  TSPL_ITEM_UOM_MT.Item_Code=TSPL_SCRAPSALE_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_MT.UOM_Code= 'MT' 
						      		left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SCRAPSALE_HEAD.Loc_Code
	                                 WHERE convert(date,TSPL_SCRAPSALE_HEAD.shipment_Date,103) >= convert(date,'" + txtFromDate.Value + "',103)   
                                     and  convert(date,TSPL_SCRAPSALE_HEAD.shipment_Date,103) <= convert(date,'" + txtToDate.Value + "',103) "

            If clsCommon.myLen(txtBillToLocation.Value) > 0 Then
                qry += "  and TSPL_SCRAPSALE_HEAD.Loc_Code In ('" + clsCommon.myCstr(txtBillToLocation.Value) + "') "
            End If

            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                qry += "  and TSPL_CUSTOMER_MASTER.cust_code In   (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")  "

            End If

            qry += "AND TSPL_SCRAPSALE_HEAD.ispost=1   and TSPL_Item_Master.FG_for_CF_RPT=1   
     					and TSPL_SCRAPSALE_HEAD.Inter_unit_sale =0  
														 )XX GROUP BY xx.Customer_Code)
								 "

            If clsCommon.myLen(qry) > 0 Then
                dt = clsDBFuncationality.GetDataTable(qry)
            End If

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If Print = False Then
                    Gv1.DataSource = Nothing
                    Gv1.Rows.Clear()
                    Gv1.Columns.Clear()
                    Gv1.GroupDescriptors.Clear()
                    Gv1.SummaryRowsBottom.Clear()
                    Gv1.MasterView.Refresh()
                    Gv1.DataSource = dt
                    Gv1.MasterTemplate.AllowAddNewRow = False

                    RadPageView1.SelectedPage = RadPageViewPage2
                    Gv1.EnableFiltering = True
                    Gv1.BestFitColumns()
                    FormatGrid()
                    'EnableDisableCntrl(False)
                    'ReStoreGridLayout()
                Else
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.SalesReport, dt, "crptDealerSalesReport", "")
                    frmCRV = Nothing
                End If
            Else
                clsCommon.MyMessageBoxShow("No data found to display.", "Sales Report")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Sub FormatGrid()
        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        Dim summaryRowItem As New GridViewSummaryRowItem()
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = True
            'gv1.Columns("Document_No").HeaderText = "Document No."
            Gv1.Columns("serial_number").IsVisible = False
            Gv1.Columns("serial_number").HeaderText = "SNO"
            Gv1.Columns("FROMDATE").IsVisible = False
            Gv1.Columns("TODATE").IsVisible = False
            Gv1.Columns("Document_Date").IsVisible = False
            Gv1.Columns("Document_Date").HeaderText = "Document Date"
            Gv1.Columns("Customer_Code").IsVisible = False
            Gv1.Columns("Customer_Code").HeaderText = "Customer Code"
            Gv1.Columns("CUSTOMER_NAME").IsVisible = True
            Gv1.Columns("CUSTOMER_NAME").HeaderText = "CUSTOMER NAME"
            Gv1.Columns("Add1").IsVisible = False
            Gv1.Columns("Add2").IsVisible = False
            Gv1.Columns("Add4").IsVisible = False
            Gv1.Columns("username").IsVisible = False
            Gv1.Columns("HeadName").IsVisible = False
            Gv1.Columns("place").IsVisible = True
            Gv1.Columns("place").HeaderText = "Place"
            Gv1.Columns("Quantity").IsVisible = True
            Gv1.Columns("Quantity").HeaderText = "Quantity"


            Gv1.Columns("COMPANYLocation").IsVisible = False
            Gv1.Columns("Location_Desc").IsVisible = False
            Gv1.Columns("Add2").IsVisible = False
            'Gv1.Columns("QuantityBag").IsVisible = False
            Gv1.Columns("price_CodeNon").IsVisible = False
            Gv1.Columns("LOcation").IsVisible = False

        Next
        Dim summaryRowItemB As New GridViewSummaryRowItem()

        'Dim MilkTypeB As New GridViewSummaryItem("Payable_Amount", "{0:n0}", GridAggregateFunction.Sum)

        Dim Quantity As New GridViewSummaryItem("Quantity", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(Quantity)
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItemB)
            Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom

        Gv1.AutoSizeRows = True
        Gv1.BestFitColumns()
        Gv1.MasterTemplate.AutoExpandGroups = True
    End Sub
    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Gv1.DataSource = Nothing
        txtFromDate.Enabled = True
        txtToDate.Enabled = True
        RadPageView1.SelectedPage = RadPageViewPage1
        txtToDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        txtFromDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        txtBillToLocation.Value = ""
        lblBillToLocation.Text = ""
        txtCustomer.arrValueMember = Nothing
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        Dim qry As String = " select cust_code as [Code], Customer_Name as [Name] from tspl_customer_master "
        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("CustMulSel", qry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
    End Sub

    'Private Sub txtLocation__My_Click(sender As Object, e As EventArgs)
    '    Dim qry As String = " select Location_Code as Code,Location_Desc as [Name] from TSPL_LOCATION_MASTER where location_type IN ('Physical','Virtual')"
    '    txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    '    Dim FrmPendingRequisitionQty As New FrmPendingRequisitionQty()
    '    FrmPendingRequisitionQty.SetDiplayMember(txtLocation, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    'End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Load_Dealer_Sales_Report(True)
    End Sub

    Private Sub txtBillToLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtBillToLocation._MYValidating
        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        Dim WhrCls As String = " Location_Type='Physical'  "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If


        txtBillToLocation.Value = clsCommon.ShowSelectForm("VendorMafnd", qry, "Code", WhrCls, txtBillToLocation.Value, "Code", isButtonClicked)
        lblBillToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtBillToLocation.Value + "'"))

    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Export(EnumExportTo.Excel)
    End Sub
    Private Sub Export(ByVal exporter As EnumExportTo)
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
                clsCommon.MyExportToExcelGrid("Dealer Sales Report", Gv1, arrHeader, Me.Text)
                ' transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                common.clsCommon.MyMessageBoxShow("Exported Successfully.", Me.Text)
                'Process.Start(filePath)

            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub radPdf_Click(sender As Object, e As EventArgs) Handles radPdf.Click
        Try

            If Gv1.Rows.Count > 0 Then

                Dim arrHeader As List(Of String) = New List(Of String)()
                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "Admin") = CompairStringResult.Equal Then
                    arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

                Else
                    'Dim strLocDesc As String = clsDBFuncationality.getSingleValue("select Location_Desc from tspl_location_master where Location_Code in (" + objCommonVar.strCurrUserLocations + ")")
                    'arrHeader.Add("Location : " + strLocDesc)
                End If
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

                If txtBillToLocation.Value IsNot Nothing AndAlso txtBillToLocation.Value.Count > 0 Then
                    arrHeader.Add("Location : " + clsCommon.myCstr(lblBillToLocation.Text))
                End If
                'If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                '    arrHeader.Add("Customer : " + clsCommon.myCstr(txtCustomer.arrDispalyMember))
                'End If '

                'If txtBillToLocation.Value IsNot Nothing AndAlso txtBillToLocation.Value.Count > 0 Then
                '    arrHeader.Add("Location : " + clsCommon.myCstr(lblBillToLocation.Text))
                'End If

                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Dealer Sales Report", Gv1, arrHeader, "Dealer Sales Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
End Class