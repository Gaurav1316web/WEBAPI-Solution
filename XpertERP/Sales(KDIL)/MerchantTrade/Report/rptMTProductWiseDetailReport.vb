Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
'========================Preeti Gupta===============================
Public Class RptMTProductWiseDetailReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.RptEXProductWiseDetail)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnGo.Visible = MyBase.isModifyFlag
        RadSplitButton1.Visible = MyBase.isExport
    End Sub
    Public Sub Load_Report()
        Try

            Dim sQuery As String

            If txtFromDate.Value > txtToDate.Value Then
                txtFromDate.Focus()
                Throw New Exception("From date can not be greater then to Date")
            End If
            'KUNAL > TICKET : BM00000009581 > DATE 26-SEP-2016 > ADDED LOCATION FILTER IN QUERY < VERIFIED BY RANJANA MADAM ( FOR ADD BILL-TO-LOCATION INSTEAD OF SHIP-TO-LOCATION) >

            sQuery = " select Document_Type,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code as [Code Of Product],TSPL_ITEM_MASTER.Item_Desc as [Name of Product]," & _
                     " TSPL_SD_SALE_INVOICE_HEAD.Customer_Code as [Buyer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Buyer Name],TSPL_SD_SALE_INVOICE_HEAD.Ship_To_Location as [Code of Consignee]," & _
                     "  Consignee.Ship_To_Desc as [Name Of Consignee],TSPL_SD_SALE_INVOICE_HEAD.Document_Code+'  ' + convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)  as [Invoice No. & Date ] ," & _
                     " convert(varchar,TSPL_SD_SALE_INVOICE_DETAIL.Qty)+' '+ Document_Type as [Quantity],TSPL_SD_SALE_INVOICE_HEAD.CURRENCY_CODE +' '+convert(varchar,TSPL_SD_SALE_INVOICE_DETAIL.Item_Cost) as [Rate In USD]," & _
                     " case when EX_Term_Code='' then (isnull(TSPL_SD_SALE_INVOICE_DETAIL.Qty,0)*isnull(TSPL_SD_SALE_INVOICE_DETAIL.Item_Cost,0)) end as [None]," & _
                     " case when EX_Term_Code='CIF' or EX_Term_Code='C&F' then (isnull(TSPL_SD_SALE_INVOICE_DETAIL.Qty,0)*isnull(TSPL_SD_SALE_INVOICE_DETAIL.Item_Cost,0)) end as [CIF Amount]," & _
                     " case when EX_Term_Code='CFR' then (isnull(TSPL_SD_SALE_INVOICE_DETAIL.Qty,0)*isnull(TSPL_SD_SALE_INVOICE_DETAIL.Item_Cost,0)) end as [CFR Amount]," & _
                     " case when EX_Term_Code='FOB' then (isnull(TSPL_SD_SALE_INVOICE_DETAIL.Qty,0)*isnull(TSPL_SD_SALE_INVOICE_DETAIL.Item_Cost,0)) end as [FOB Amount] ," & _
                     " convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Cust_PO_No,103) as [PO Number] ,TSPL_SD_SALE_INVOICE_HEAD.Cust_PODate as [PO Date] ," & _
                     " TSPL_SD_SALE_INVOICE_HEAD.Payment_Terms as [Payment Terms] ,dateName(M,DATEADD(Day, TSPL_TERMS_MASTER.No_Days,Document_Date))+' '+ convert(varchar,DATEPART(YEAR ,DATEADD(Day, TSPL_TERMS_MASTER.No_Days,Document_Date)))+' - '+convert(varchar,TSPL_SD_SALE_INVOICE_DETAIL.Qty)+' '+ Document_Type as [Last Date Of Shipment]," & _
                      " TSPL_SD_SALE_INVOICE_HEAD.CHA_Code as [CHA Code],CHA.Vendor_Name as [CHA NAme] , TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location [Location], TSPL_LOCATION_MASTER.Location_Desc [Location Desc]  " & _
                     "  from TSPL_SD_SALE_INVOICE_HEAD " & _
                     " left join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE =TSPL_SD_SALE_INVOICE_HEAD.Document_Code " & _
                     " left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " & _
                     " left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_SD_SALE_INVOICE_HEAD.Customer_Code" & _
                     " left join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code =TSPL_SD_SALE_INVOICE_HEAD.Terms_Code " & _
                     "left join TSPL_SHIP_TO_LOCATION as Consignee  on Consignee.Ship_To_Code  =TSPL_SD_SALE_INVOICE_HEAD.Ship_To_Location" & _
                     " left join TSPL_VENDOR_MASTER as CHA on CHA.Vendor_Code =TSPL_SD_SALE_INVOICE_HEAD.CHA_Code  LEFT JOIN  TSPL_LOCATION_MASTER ON TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location = TSPL_LOCATION_MASTER.Location_Code " & _
                     " where  TSPL_SD_SALE_INVOICE_HEAD. Trans_Type ='EXP'" & _
                    "  and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)>=convert(date,'" & txtFromDate.Value & "',103) and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <=convert(date,'" & txtToDate.Value & "' ,103)  "
            If ddlType.Text = "Export" Then
                sQuery += "and Document_Type='EX' "
            ElseIf ddlType.Text = "Merchent" Then
                sQuery += "and Document_Type='MT' "
            End If
            If TxtMultiBuyer.arrValueMember IsNot Nothing AndAlso TxtMultiBuyer.arrValueMember.Count > 0 Then
                sQuery += " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in  (" + clsCommon.GetMulcallString(TxtMultiBuyer.arrValueMember) + ") "
            End If
            If TxtMultiItem.arrValueMember IsNot Nothing AndAlso TxtMultiItem.arrValueMember.Count > 0 Then
                sQuery += " and TSPL_SD_SALE_INVOICE_DETAIL.Item_Code  in  (" + clsCommon.GetMulcallString(TxtMultiItem.arrValueMember) + ") "
            End If
            If TxtMultiConsignee.arrValueMember IsNot Nothing AndAlso TxtMultiConsignee.arrValueMember.Count > 0 Then
                sQuery += " and TSPL_SD_SALE_INVOICE_HEAD.Ship_To_Location  in  (" + clsCommon.GetMulcallString(TxtMultiConsignee.arrValueMember) + ") "
            End If
            'KUNAL > TICKET : BM00000009581 > DATE 26-SEP-2016 > ADDED LOCATION FILTER IN QUERY
            If TxtMultiLocationFinder4.arrValueMember IsNot Nothing AndAlso TxtMultiLocationFinder4.arrValueMember.Count > 0 Then
                sQuery += "  AND TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location IN (" + clsCommon.GetMulcallString(TxtMultiLocationFinder4.arrValueMember) + ")"
            End If

            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(sQuery)
            If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
                gv1.DataSource = Nothing
                gv1.Rows.Clear()
                gv1.Columns.Clear()
                gv1.DataSource = dtgv
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                'FormatGrid()
                'gv1.Columns("Shipment Date").IsVisible = False
                gv1.BestFitColumns()
                RadPageView1.SelectedPage = RadPageViewPage2
                'FindAndRestoreGridLayout(Me)
                ReStoreGridLayout()
            Else

                clsCommon.MyMessageBoxShow("No Data Found")
            End If


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try

    End Sub
    Sub Reset()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        ddlType.Text = "All"
        RadPageView1.SelectedPage = RadPageViewPage1
        'gv1.Rows.Clear()
        gv1.DataSource = Nothing
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

            If TxtMultiBuyer.arrValueMember IsNot Nothing AndAlso TxtMultiBuyer.arrValueMember.Count > 0 Then
                arrHeader.Add(" Buyer : " + clsCommon.GetMulcallStringWithComma(TxtMultiBuyer.arrValueMember))
            Else
                arrHeader.Add((" Buyer : All"))
            End If
            If TxtMultiConsignee.arrValueMember IsNot Nothing AndAlso TxtMultiConsignee.arrValueMember.Count > 0 Then
                arrHeader.Add(" Consignee : " + clsCommon.GetMulcallStringWithComma(TxtMultiConsignee.arrValueMember))
            Else
                arrHeader.Add((" Consignee: All"))
            End If
            If TxtMultiItem.arrValueMember IsNot Nothing AndAlso TxtMultiItem.arrValueMember.Count > 0 Then
                arrHeader.Add(" Item : " + clsCommon.GetMulcallStringWithComma(TxtMultiItem.arrValueMember))
            Else
                arrHeader.Add((" Item: All"))
            End If

            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("ProductWise Detail Report", gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("ProductWise Detail Report", gv1, arrHeader, Me.Text, True)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub RptMTProductWiseDetailReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        'ButtonToolTip.SetToolTip(BtnPrint, "Press Alt+P For Print")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
        Reset()
    End Sub

    Private Sub RptMTProductWiseDetailReport_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.R Then
            Load_Report()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv1
        Load_Report()
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Private Sub rmExportToExcel_Click(sender As Object, e As EventArgs) Handles rmExportToExcel.Click
        'print(EnumExportTo.Excel)
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
        'print(EnumExportTo.PDF)
        ExportGrid(EnumExportTo.PDF)
    End Sub

    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()

                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptEXProductWiseDetail & "'"))
                If TxtMultiBuyer.arrValueMember IsNot Nothing AndAlso TxtMultiBuyer.arrValueMember.Count > 0 Then
                    arrHeader.Add("Buyer : " + clsCommon.GetMulcallStringWithComma(TxtMultiBuyer.arrValueMember))
                Else
                    arrHeader.Add(("Buyer : All"))
                End If
                If TxtMultiConsignee.arrValueMember IsNot Nothing AndAlso TxtMultiConsignee.arrValueMember.Count > 0 Then
                    arrHeader.Add("Consignee : " + clsCommon.GetMulcallStringWithComma(TxtMultiConsignee.arrValueMember))
                Else
                    arrHeader.Add(("Consignee: All"))
                End If

                If TxtMultiItem.arrValueMember IsNot Nothing AndAlso TxtMultiItem.arrValueMember.Count > 0 Then
                    arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(TxtMultiItem.arrValueMember))
                Else
                    arrHeader.Add(("Item: All"))
                End If
                If TxtMultiLocationFinder4.arrValueMember IsNot Nothing AndAlso TxtMultiLocationFinder4.arrValueMember.Count > 0 Then
                    arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(TxtMultiLocationFinder4.arrValueMember))
                Else
                    arrHeader.Add(("Location : All"))
                End If
                If exporter = EnumExportTo.Excel Then
                    'Dim sfd As SaveFileDialog = New SaveFileDialog()
                    'Dim filePath As String
                    'sfd.FileName = Me.Text
                    'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
                    'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    '    filePath = sfd.FileName
                    'Else
                    '    Exit Sub
                    'End If
                    transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
                    'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                    'Process.Start(filePath)
                Else
                    transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("ProductWise Detail Report", gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub TxtMultiBuyer__My_Click(sender As Object, e As EventArgs) Handles TxtMultiBuyer._My_Click
        Dim qry As String = "select vendor_code as [Code],vendor_Name as [Name] from TSPL_VENDOR_MASTER "
        TxtMultiBuyer.arrValueMember = clsCommon.ShowMultipleSelectForm("Vendor", qry, "Code", "Name", TxtMultiBuyer.arrValueMember, TxtMultiBuyer.arrDispalyMember)
    End Sub

    Private Sub TxtMultiItem__My_Click(sender As Object, e As EventArgs) Handles TxtMultiItem._My_Click
        Dim qry As String = "select Item_Code as [Code],Item_Desc as [Name] from TSPL_ITEM_MASTER "
        TxtMultiItem.arrValueMember = clsCommon.ShowMultipleSelectForm("Item", qry, "Code", "Name", TxtMultiItem.arrValueMember, TxtMultiItem.arrDispalyMember)
    End Sub
    Private Sub TxtMultiConsignee__My_Click(sender As Object, e As EventArgs) Handles TxtMultiConsignee._My_Click
        Dim qry As String = "select Ship_To_Code as Code,Ship_To_Desc as Name from TSPL_SHIP_TO_LOCATION"
        TxtMultiConsignee.arrValueMember = clsCommon.ShowMultipleSelectForm("Ship", qry, "Code", "Name", TxtMultiConsignee.arrValueMember, TxtMultiConsignee.arrDispalyMember)
    End Sub
    'KUNAL > TICKET : BM00000009581 > DATE 26-SEP-2016 > ADDED LOCATION FILTER IN FORM
    Private Sub TxtMultiLocationFinder4__My_Click(sender As Object, e As EventArgs) Handles TxtMultiLocationFinder4._My_Click
        Dim qry As String = "select Location_Code Code , Location_Desc Name from TSPL_LOCATION_MASTER"
        Try
            TxtMultiLocationFinder4.arrValueMember = clsCommon.ShowMultipleSelectForm("rptMTProductWiseDetailReportLocFndr", qry, "Code", "Name", TxtMultiLocationFinder4.arrValueMember, TxtMultiLocationFinder4.arrDispalyMember)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= Gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To Gv1.Columns.Count - 1 Step ii + 1
                        Gv1.Columns(ii).IsVisible = False
                        Gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    Gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub
End Class
