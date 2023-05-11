'--09/01/2013-01:05PM--Updation By--Pankaj Kumar---Quantity was Not Correct
'For pdf work by vipib 07/02/2013
'' for bug no BM00000000644
Imports common
Imports XpertERPEngine
Imports System.Threading
Imports Telerik.WinControls.UI.Export
Imports Telerik.WinControls.UI.Export.ExcelML
Imports System.Data.SqlClient
Imports Telerik.WinControls
Imports Telerik.WinControls.UI

Public Class EmptyReportDetail
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()


    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        'printdata()
    End Sub
    Sub printdata(ByVal exporter As EnumExportTo)
        Try


            loaddata()
            RadPageView1.SelectedPage = RadPageViewPage2

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(objCommonVar.CurrentCompanyName)
            arrHeader.Add("Date Range : " + clsCommon.GetPrintDate(dtpFdate.Value, "dd/MM/yyyy") + "   To  " + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy"))

            Dim type As String = ""
            If clsCommon.CompairString(ddltype.Text, "LI") = CompairStringResult.Equal Then
                type += "Inward"
            Else
                type += "Outward"
            End If
            arrHeader.Add("Report Type : " + type)

            If rbtnSaleInvoice.IsChecked Then
                arrHeader.Add("Against Sale Invoice")
            Else
                arrHeader.Add("Against Loadout / Transfer")
            End If

            Dim LocFiltr As String = ""
            If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0 Then
                LocFiltr = clsCommon.GetMulcallString(cbgLocation.CheckedValue)
                LocFiltr = LocFiltr.Replace("'", "")
            End If
            arrHeader.Add("Loc Code : " + LocFiltr)
            If gv.Rows.Count > 0 Then

                'clsCommon.MyExportToExcel(IIf(clsCommon.CompairString(ddltype.Text, "In") = CompairStringResult.Equal, "Empty Inward Report (Detail)", "Empty Outward Report (Detail)"), gv, arrHeader, Me.Text)
                If exporter = EnumExportTo.Excel Then
                    clsCommon.MyExportToExcel(IIf(clsCommon.CompairString(ddltype.Text, "In") = CompairStringResult.Equal, "Empty Inward Report (Detail)", "Empty Outward Report (Detail)"), gv, arrHeader, Me.Text)
                Else
                    clsCommon.MyExportToPDF(IIf(clsCommon.CompairString(ddltype.Text, "In") = CompairStringResult.Equal, "Empty Inward Report (Detail)", "Empty Outward Report (Detail)"), gv, arrHeader, "Vendor Ledger Report", True)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Public Sub gridformat(ByVal arritem As ArrayList)
        Try
            'gv.DataSource = Nothing
            'gv.Rows.Clear()
            'gv.Columns.Clear()

            gv.MasterTemplate.SummaryRowsBottom.Clear()

            Dim strItemCode As String
            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim intCount As Integer = 0

            gv.AllowAddNewRow = False





            'DocumentNo ,DocumentDate ,Route ,RouteDescription ,Salesmancode ,SalesmanDesc,VehicleNo
            gv.Columns("DocumentNo").IsVisible = True
            gv.Columns("DocumentNo").Width = 100
            gv.Columns("DocumentNo").HeaderText = "Document No"


            gv.Columns("DocumentDate").IsVisible = True
            gv.Columns("DocumentDate").Width = 100
            gv.Columns("DocumentDate").HeaderText = "Document Date"

            gv.Columns("Route").IsVisible = True
            gv.Columns("Route").Width = 50
            gv.Columns("Route").HeaderText = "Route"

            gv.Columns("RouteDescription").IsVisible = True
            gv.Columns("RouteDescription").Width = 200
            gv.Columns("RouteDescription").HeaderText = "Route Description"

            gv.Columns("Salesmancode").IsVisible = True
            gv.Columns("Salesmancode").Width = 70
            gv.Columns("Salesmancode").HeaderText = "Salesman Code"


            gv.Columns("SalesmanDesc").IsVisible = True
            gv.Columns("SalesmanDesc").Width = 200
            gv.Columns("SalesmanDesc").HeaderText = "Salesman Desc"

            gv.Columns("VehicleNo").IsVisible = True
            gv.Columns("VehicleNo").Width = 100
            gv.Columns("VehicleNo").HeaderText = "Vehicle No"

            If rbtnSaleInvoice.IsChecked Then
                gv.Columns("Shipment_Type").IsVisible = True
                gv.Columns("Shipment_Type").Width = 90
                gv.Columns("Shipment_Type").HeaderText = "Shipment Type"

                gv.Columns("Customer_CODE").IsVisible = True
                gv.Columns("Customer_CODE").Width = 90
                gv.Columns("Customer_CODE").HeaderText = "Customer Code"

                gv.Columns("Customer_NAME").IsVisible = True
                gv.Columns("Customer_NAME").Width = 250
                gv.Columns("Customer_NAME").HeaderText = "Customer Name"
            Else
                gv.Columns("Shipment_Type").IsVisible = False

                gv.Columns("Customer_CODE").IsVisible = False

                gv.Columns("Customer_NAME").IsVisible = False

            End If

            For ii As Integer = 10 To gv.Columns.Count - 1
                intCount = intCount + 1
                strItemCode = gv.Columns(ii).FieldName
                Dim item16 As New GridViewSummaryItem("" & strItemCode & "", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item16)
            Next

            gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            'For j As Integer = 0 To arritem.Count - 1
            '    gv.Columns(arritem.Item(j)).IsVisible = True
            '    gv.Columns(arritem.Item(j)).Width = 100
            '    gv.Columns(arritem.Item(j)).HeaderText = arritem.Item(j)

            'Next






            For j As Integer = 0 To arritem.Count - 1
                gv.Columns(arritem.Item(j)).IsVisible = True
                gv.Columns(arritem.Item(j)).Width = 100
                gv.Columns(arritem.Item(j)).HeaderText = arritem.Item(j)

            Next
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)

        End Try
    End Sub
    Sub LoadLocation()
        ' Dim qry As String = "select distinct Location,Item_Code as [Item Code] from TSPL_SALE_INVOICE_DETAIL"
        '  Dim qry As String = "select Location_Code as [Location],Location_Desc as [Location Description] from TSPL_LOCATION_MASTER where Location_Type <> 'Logical'  order by Location_Code"
        Dim qry As String = " select Location_Code as Code,Location_Desc as Description  from TSPL_LOCATION_MASTER where Location_Type='physical' "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Description"
    End Sub
    Sub loaddata()
        Try
            If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Location or select ALL")
                Exit Sub
            End If
            Dim strItemCodestring, strItemCode, strMainItemCode, strmainItemCodeString, strsum As String
            strItemCodestring = ""
            strItemCode = ""
            strMainItemCode = ""
            strmainItemCodeString = ""
            strsum = ""
            If dtpFdate.Value > dtpToDate.Value Then
                common.clsCommon.MyMessageBoxShow("FromDate is greater than ToDate")
                Exit Sub
            End If

            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()

            Dim isLIType As Boolean = IIf(clsCommon.CompairString(ddltype.Text, "In") = CompairStringResult.Equal, True, False)
            Dim startdate As String = clsCommon.GetPrintDate(dtpFdate.Value, "dd/MM/yyyy")
            Dim enddate As String = clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy")

            Dim BaseQry As String = "select TSPL_TRANSFER_HEAD .transfer_no as DocumentNo ,convert (date,tspl_transfer_head.Transfer_Date ,103) as DocumentDate, Load_Out_No as [Against Document No], Load_Out_Date as [Against Document Date] " + Environment.NewLine
            If isLIType Then
                BaseQry += " ,TSPL_TRANSFER_HEAD .to_Location  as Location," + Environment.NewLine

            Else
                BaseQry += "  ,TSPL_TRANSFER_HEAD .From_Location  as Location," + Environment.NewLine

            End If
            BaseQry += " TSPL_TRANSFER_HEAD .route_no as Route, TSPL_TRANSFER_HEAD .route_desc as RouteDescription,TSPL_TRANSFER_HEAD .salesmancode as SalesmanCode,TSPL_TRANSFER_HEAD .description as SalesmanDesc,TSPL_TRANSFER_HEAD .Vehicle_No  as VehicleNo,TSPL_TRANSFER_DETAIL.Item_Code, CONVERT(decimal(18,2), isnull(TSPL_TRANSFER_DETAIL.Item_Qty,0) /TSPL_ITEM_UOM_DETAIL.Conversion_Factor,2) as Qty,'Transfer' as Shipment_Type ,'' as Customer_CODE,'' as Customer_NAME " + Environment.NewLine
            BaseQry += "   from TSPL_TRANSFER_DETAIL " + Environment.NewLine
            BaseQry += "left outer join TSPL_TRANSFER_HEAD  on TSPL_TRANSFER_DETAIL.Transfer_No  =TSPL_TRANSFER_HEAD.Transfer_No " + Environment.NewLine
            BaseQry += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code= TSPL_TRANSFER_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_TRANSFER_DETAIL.Uom" + Environment.NewLine
            BaseQry += " where 2=2"
            BaseQry += "  and (tspl_transfer_head.Transfer_Date >=  convert(date,'" + startdate + "',103)) AND (tspl_transfer_head.Transfer_Date <= convert(date,'" + enddate + "',103)) and TSPL_TRANSFER_HEAD .item_type='Empty'  and TSPL_TRANSFER_HEAD.post='Y'  "

            If isLIType Then
                BaseQry += " and TSPL_TRANSFER_HEAD.Transfer_Type='LI'" + Environment.NewLine
            Else
                BaseQry += "  and TSPL_TRANSFER_HEAD.Transfer_Type='LO'" + Environment.NewLine
            End If

            If ddltype.Text = "Out" Then
                If chkLocationSelect.IsChecked = True Then
                    BaseQry += " and TSPL_TRANSFER_HEAD.From_Location in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                End If
            ElseIf ddltype.Text = "In" Then
                If chkLocationSelect.IsChecked = True Then
                    BaseQry += " and TSPL_TRANSFER_HEAD. To_Location in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                End If
            End If

            If rbtnSaleInvoice.IsChecked Then
                BaseQry += " and 1=2"
            End If

            BaseQry += " union all" + Environment.NewLine
            BaseQry += "   select TSPL_ADJUSTMENT_HEADER .Adjustment_No  as DocumentNo ,TSPL_ADJUSTMENT_HEADER.Adjustment_Date  as DocumentDate,TSPL_ADJUSTMENT_HEADER.Document_No  as [Against Document No], TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date   as [Against Document Date],TSPL_ADJUSTMENT_HEADER .Loc_Code  as Location,'' as Route, '' as RouteDescription,TSPL_ADJUSTMENT_HEADER.EMP_CODE  as SalesmanCode,TSPL_ADJUSTMENT_HEADER .EMP_NAME as SalesmanDesc,TSPL_ADJUSTMENT_HEADER .Vehicle_No  as VehicleNo,TSPL_ADJUSTMENT_detail.Item_Code , CONVERT(decimal(18,2), (isnull( TSPL_ADJUSTMENT_DETAIL .Item_Quantity,0)+isnull(TSPL_ADJUSTMENT_DETAIL.LeakageQty,0)) /TSPL_ITEM_UOM_DETAIL.Conversion_Factor,2) as Qty,TSPL_SALE_INVOICE_HEAD.Shipment_Type ,TSPL_ADJUSTMENT_HEADER.Customer_CODE,TSPL_ADJUSTMENT_HEADER.Customer_NAME  from TSPL_ADJUSTMENT_HEADER left outer join TSPL_ADJUSTMENT_detail on TSPL_ADJUSTMENT_detail.Adjustment_No =TSPL_ADJUSTMENT_HEADER.Adjustment_No  " + Environment.NewLine
            BaseQry += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code= TSPL_ADJUSTMENT_detail.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_ADJUSTMENT_detail.Unit_Code " + Environment.NewLine
            BaseQry += "  left outer join TSPL_SALE_INVOICE_HEAD on TSPL_ADJUSTMENT_HEADER.Document_No=TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No  " + Environment.NewLine
            BaseQry += "  where  (convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date ,103)  >=  convert(date,'" + startdate + "',103)) AND (convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date ,103)  <= convert(date,'" + enddate + "',103)) and TSPL_ADJUSTMENT_HEADER .itemtype='E' and  TSPL_ADJUSTMENT_HEADER.posted='Y' "

            If chkLocationSelect.IsChecked = True Then
                BaseQry += " and TSPL_ADJUSTMENT_HEADER.Loc_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If
            If isLIType Then
                BaseQry += " and TSPL_ADJUSTMENT_HEADER .Trans_Type ='In' " + Environment.NewLine
            Else
                BaseQry += "  and TSPL_ADJUSTMENT_HEADER .Trans_Type='Out'" + Environment.NewLine
            End If
            If rbtnSaleInvoice.IsChecked Then
                BaseQry += " and  Reference_Document='Sale Invoice'"
            Else
                BaseQry += " and  Reference_Document='Load out/Transfer'"
            End If
            ' TSPL_ADJUSTMENT_HEADER .Trans_Type ='ln' 

            Dim qryForCoumns As String = "select   xxx.Item_Code   from (" + BaseQry + ") xxx"
            qryForCoumns += "  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =xxx.Item_Code"
            qryForCoumns += " group by xxx.Item_Code"
            qryForCoumns += " order by MAX( TSPL_ITEM_MASTER.Sku_Seq  )"
            'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qryForCoumns)
            'If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            '    gv.DataSource = Nothing
            '    Throw New Exception("No Data found")

            'End If
            Dim isFirstTime As Boolean = True
            Dim ItemColumnsForPivot As String = ""
            Dim ItemColumnsForPivot1 As String = ""

            ' Dim dr As SqlDataReader = connectSql.RunSqlReturnDR(qryForCoumns)
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qryForCoumns)

            Dim arritem As New ArrayList
            ' While dr.Read
            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                For Each dr As DataRow In dt1.Rows
                    strItemCode = CStr(dr(0).ToString())
                    strItemCodestring = strItemCodestring & "[" & strItemCode & "]" & ","
                    arritem.Add(strItemCode)
                    strMainItemCode = CStr(dr(0).ToString())
                    strmainItemCodeString = strmainItemCodeString & "  isnull(" & "[" & strItemCode & "]" & ",0)  " & "as  " & "[" & strItemCode & "]" & ","
                    'strsum = strItemCode + "+"

                    strsum = strsum & "  isnull(" & "[" & strItemCode & "]" & ",0)" & "+"
                    ' End While
                Next
            End If
            If strItemCode <> "" Then
                strItemCodestring = strItemCodestring.Substring(0, strItemCodestring.Length - 1)
                strmainItemCodeString = strmainItemCodeString.Substring(0, strmainItemCodeString.Length - 1)
                strsum = strsum.Substring(0, strsum.Length - 1)
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                gv.DataSource = Nothing
                Exit Sub
            End If

            'For Each dr As DataRow In dt.Rows
            '    If Not isFirstTime Then
            '        ItemColumnsForPivot += ","
            '    End If

            '    ItemColumnsForPivot = CStr(dr(0).ToString())
            '    ItemColumnsForPivot1 = strmainItemCodeString & "  isnull(" & "[" & strItemCode & "]" & ",0)  " & "as  " & "[" & strItemCode & "]" & ","
            '    'ItemColumnsForPivot += "[" + clsCommon.myCstr(dr("Item_Code")) + "]"
            '    'ItemColumnsForPivot1 = ItemColumnsForPivot & "  isnull(" & "[" & ItemColumnsForPivot & "]" & ",0)  " & "as  " & "[" & ItemColumnsForPivot & "]" & ","
            '    isFirstTime = False
            'Next
            ' Dim strmain As String
            'strmain = " select   Transfer_No as [Transfer No],Transfer_Date as [Transfer Date],Route_No as Route,Route_Desc as [Route Description],Salesmancode as [Salesman Code],Emp_Name as [Salesman Desc],Vehicle_No as [Vehicle No] ," & strmainItemCodeString & " ,(" + strsum + ")as Total from(select Transfer_No,Transfer_Date ,Route_No,Route_Desc,Salesmancode,Emp_Name,Vehicle_No,convert(decimal(18,2),round(sum(Sale),2)) as Sale,Group1 from (" + strQuery + ")aaa group by aaa.Group1,aaa.Comp_Name,aaa.HierCode,aaa.HierDesc,aaa.Transfer_No,aaa.Transfer_Date,aaa.Route_No,aaa.Route_Desc,aaa.Salesmancode,aaa.Emp_Name,aaa.Vehicle_No) down pivot (SUM(Sale) FOR Group1 IN ( " & strItemCodestring & ")) AS pvt1 "
            Dim qryForPivot As String = "select DocumentNo ,DocumentDate,[Against Document No] ,[Against Document Date] ,Route ,RouteDescription,Shipment_Type ,Salesmancode ,SalesmanDesc,Customer_CODE ,Customer_NAME,VehicleNo," & strmainItemCodeString & " ,(" + strsum + ")as Total  from (" + BaseQry + "  ) xxx pivot (sum(Qty) for item_code in (" & strItemCodestring & ")) as PQ"

            gv.DataSource = clsDBFuncationality.GetDataTable(qryForPivot)
            gridformat(arritem)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub exporter_ExcelCellFormatting(ByVal sender As Object, ByVal e As ExcelCellFormattingEventArgs)
        If e.GridRowInfoType Is GetType(GridViewDataRowInfo) Then
            'update progress bar                
            ''Dim position As Integer = CInt(Fix(100 * CDbl(e.GridRowIndex) / CDbl(gv1.RowCount - 1)))
            ''Me.UpdateProgressBar(position)                'do some formatting                
            ''If e.GridColumnIndex = 3 AndAlso CInt(Fix(e.ExcelCellElement.Data.DataItem)) < 200 Then
            ''    e.ExcelStyleElement.InteriorStyle.Color = Color.Yellow
            ''End If
        End If
    End Sub
    Private Sub exporter_ExcelTableCreated(ByVal sender As Object, ByVal e As ExcelTableCreatedEventArgs)

        If e.SheetIndex = 0 Then 'add header row only for the first excel sheet                
            Dim style As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 30, "KPI Report")
            style.AlignmentElement.HorizontalAlignment = HorizontalAlignmentType.Center
            style.AlignmentElement.VerticalAlignment = VerticalAlignmentType.Center
            style.InteriorStyle.Pattern = InteriorPatternType.Solid
            'style.InteriorStyle.Color = Color.Red
            style.FontStyle.Color = Color.Black
            style.FontStyle.Bold = True
            style.FontStyle.Size = 26

            Dim style1 As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 20, "Date : " + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy"))
            'If chkLocationSelect.IsChecked Then
            '    Dim strLoca As String = ""
            '    For Each Str As String In cbgLocation.CheckedDisplayMember
            '        If clsCommon.myLen(strLoca) > 0 Then
            '            strLoca += ", "
            '        End If
            '        strLoca += Str
            '    Next
            '    Dim style2 As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 20, "Location Segment : " + strLoca)

            'End If

        End If
    End Sub
    Private Sub ExportToExcel()
        Dim saveDialog1 As New SaveFileDialog()
        saveDialog1.Filter = "Excel (*.xls)|*.xls"
        If saveDialog1.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
            ''Me.RadProgressBar1.Text = "Exporting to ExcelML..."
            ''Me.RadProgressBar1.Value1 = 0
            ''Me.RadProgressBar1.Visible = True

            Dim thread2 As New Thread(New ParameterizedThreadStart(AddressOf RunExportToExcelML))
            thread2.Start(saveDialog1.FileName)
        End If
    End Sub

    Private Sub EmptyReportDetail_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        If e.Control AndAlso e.KeyCode = Keys.P Then
            printdata(EnumExportTo.Excel)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            dtpFdate.Value = clsCommon.GETSERVERDATE
            dtpToDate.Value = clsCommon.GETSERVERDATE
            ddltype.Text = "Out"
            chkLocationAll.IsChecked = True
            ' LoadLocation()
            ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
            ButtonToolTip.SetToolTip(btnReset, "Press Alt+N for reset")
            ButtonToolTip.SetToolTip(btnprint, "Press Alt+P for Print ")
            gv.DataSource = Nothing
        End If
    End Sub

    Private Sub EmptyReportDetail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadLocation()
        chkLocationAll.IsChecked = True
        dtpFdate.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        ddltype.Text = "Out"
        'LoadLocation()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(btnprint, "Press Alt+P for Print ")
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub RunExportToExcelML(ByVal fileName As Object)
        Try
            Dim exporter As New ExportToExcelML(gv)
            exporter.ExportVisualSettings = True
            'If Me.radRadioButton1.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On Then
            exporter.SheetMaxRows = ExcelMaxRows._1048576
            'End If
            AddHandler exporter.ExcelCellFormatting, AddressOf exporter_ExcelCellFormatting
            AddHandler exporter.ExcelTableCreated, AddressOf exporter_ExcelTableCreated
            exporter.RunExport(fileName.ToString())
            Dim text As String = "Export finished successfully!"
            ''Dim ev As CustomDelegate = AddressOf Me.MessageShow
            ''If Me.InvokeRequired Then
            ''    ev.Invoke(Me, text)
            ''Else
            ''    common.clsCommon.MyMessageBoxShow(Me, text)
            ''End If
            common.clsCommon.MyMessageBoxShow(text)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
            ''If Me.InvokeRequired Then
            ''    Dim ev As CustomDelegate = AddressOf Me.MessageShowError
            ''    ev.Invoke(Me, ex.Message)
            ''Else
            ''    RadMessageBox.SetThemeName("Breeze")
            ''    common.clsCommon.MyMessageBoxShow(Me, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error)
            ''End If

        End Try
    End Sub

    Private Sub btnrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        loaddata()
        gv.EnableFiltering = True
        RadPageView1.SelectedPage = RadPageViewPage2

    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        dtpFdate.Value = clsCommon.GETSERVERDATE
        dtpToDate.Value = clsCommon.GETSERVERDATE
        ddltype.Text = "Out"
        chkLocationAll.IsChecked = True
        ' LoadLocation()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(btnprint, "Press Alt+P for Print ")
        gv.DataSource = Nothing
    End Sub

    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocationAll.IsChecked
    End Sub

    Private Sub gv_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv.ViewCellFormatting
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.EmptyReportDetail)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        printdata(EnumExportTo.Excel)
    End Sub

    Private Sub btnPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        printdata(EnumExportTo.PDF)
    End Sub
End Class
