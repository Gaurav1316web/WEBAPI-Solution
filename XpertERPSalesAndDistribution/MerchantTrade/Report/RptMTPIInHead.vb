Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
'========================Preeti Gupta===============================
Public Class RptMTPIInHead
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.RptMTPIInHead)
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
            'KUNAL > TICKET : BM00000009581 > DATE 26-SEP-2016 > ADDED LOCATION FILTER IN QUERY
            sQuery = " select final.Document_Type as [Doc Type],final.Item_Code as [Code of Product],final.Item_Desc as [Name of Product]," & _
                    " final.Customer_Code as [Buyer Code],final.Customer_Name as [Buyer Name],final.Cust_PO_No  as [P.O Number],convert(varchar,final.Cust_PODate,103) as [PO Date]," & _
                        " convert(varchar,final.Qty)+' ' +final.Document_Type as [Quantity],final.CURRENCY_CODE +'.'+convert(varchar,final.Item_Cost) as [Rate In USD] ," & _
                        " final.Payment_Terms as [Payment Term],final.Month +' '+final.Year +'-'+convert(varchar,final.qty)+' '+final.Document_Type as [Last Date Of Shipment]," & _
                        " final.Remarks,final.Document_Date as [Shipment Date] ,  final.[Location Code], FINAL.[Location Desc]  from (select Document_Type,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code ,max(TSPL_ITEM_MASTER.Item_Desc) as Item_Desc ," & _
                        " TSPL_SD_SALE_INVOICE_HEAD.Customer_Code as Customer_Code,max(TSPL_CUSTOMER_MASTER.Customer_Name) as Customer_Name,TSPL_SD_SALE_INVOICE_HEAD.Cust_PO_No as Cust_PO_No ," & _
                        "  max(TSPL_SD_SALE_INVOICE_HEAD.Cust_PODate) as Cust_PODate ,sum(TSPL_SD_SALE_INVOICE_DETAIL.Qty) as Qty,max(TSPL_SD_SALE_INVOICE_HEAD.CURRENCY_CODE) as CURRENCY_CODE ," & _
                        " sum(TSPL_SD_SALE_INVOICE_DETAIL.Item_Cost)/count(*) as Item_Cost ,max(TSPL_SD_SALE_INVOICE_HEAD.Payment_Terms) as Payment_Terms ," & _
                        "  dateName(M,DATEADD(Day, max(TSPL_TERMS_MASTER.No_Days),max(Document_Date))) as Month ," & _
                        " convert(varchar,DATEPART(YEAR ,DATEADD(Day, max(TSPL_TERMS_MASTER.No_Days),max(Document_Date)))) as Year," & _
                        " max(TSPL_SD_SALE_INVOICE_HEAD.Remarks) as Remarks ,max(Document_Date) as Document_Date , TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location as [Location Code],  MAX(TSPL_LOCATION_MASTER.Location_Desc) As [Location Desc] from TSPL_SD_SALE_INVOICE_HEAD " & _
                        " left join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE =TSPL_SD_SALE_INVOICE_HEAD.Document_Code " & _
                        " left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " & _
                        " left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_SD_SALE_INVOICE_HEAD.Customer_Code" & _
                        " left join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code =TSPL_SD_SALE_INVOICE_HEAD.Terms_Code  LEFT JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code = TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location " & _
                       " where   Trans_Type ='EXP' group by Document_Type ,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_SD_SALE_INVOICE_HEAD.Cust_PO_No , TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location)final" & _
                " where 2=2 and convert(date,final.Document_Date,103)>=convert(date,'" & txtFromDate.Value & "',103) and convert(date,final.Document_Date,103) <=convert(date,'" & txtToDate.Value & "' ,103)  "
            If ddlType.Text = "Export" Then
                sQuery += "and final.Document_Type='EX' "
            ElseIf ddlType.Text = "Merchent" Then
                sQuery += "and final.Document_Type='MT' "
            End If
            If TxtMultiBuyer.arrValueMember IsNot Nothing AndAlso TxtMultiBuyer.arrValueMember.Count > 0 Then
                sQuery += " and final.Customer_Code in  (" + clsCommon.GetMulcallString(TxtMultiBuyer.arrValueMember) + ") "
            End If
            If TxtMultiItem.arrValueMember IsNot Nothing AndAlso TxtMultiItem.arrValueMember.Count > 0 Then
                sQuery += " and final.Item_Code  in  (" + clsCommon.GetMulcallString(TxtMultiItem.arrValueMember) + ") "
            End If
            'KUNAL > TICKET : BM00000009581 > DATE 26-SEP-2016 > ADDED LOCATION FILTER IN QUERY
            If TxtMultiLocationFinder4.arrValueMember IsNot Nothing AndAlso TxtMultiLocationFinder4.arrValueMember.Count > 0 Then
                sQuery += "  AND final.[Location Code] IN (" + clsCommon.GetMulcallString(TxtMultiLocationFinder4.arrValueMember) + ")"
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
                gv1.Columns("Shipment Date").IsVisible = False
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
        Try
            txtToDate.Value = clsCommon.GETSERVERDATE()
            txtFromDate.Value = txtToDate.Value.AddMonths(-1)
            ddlType.Text = "All"
            RadPageView1.SelectedPage = RadPageViewPage1
            'gv1.Rows.Clear()
            gv1.DataSource = Nothing
            TxtMultiBuyer.arrValueMember = Nothing
            TxtMultiItem.arrValueMember = Nothing
            TxtMultiLocationFinder4.arrValueMember = Nothing
        Catch ex As Exception
        End Try

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
            If TxtMultiItem.arrValueMember IsNot Nothing AndAlso TxtMultiItem.arrValueMember.Count > 0 Then
                arrHeader.Add(" Item : " + clsCommon.GetMulcallStringWithComma(TxtMultiItem.arrValueMember))
            Else
                arrHeader.Add((" Item: All"))
            End If
           
            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("PO In Hand", gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("PO In Hand", gv1, arrHeader, Me.Text, True)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
   

    Private Sub TxtMultiBuyer__My_Click(sender As Object, e As EventArgs) Handles TxtMultiBuyer._My_Click
        Dim qry As String = "select Cust_Code as Code,Customer_Name as Name from TSPL_CUSTOMER_MASTER "
        TxtMultiBuyer.arrValueMember = clsCommon.ShowMultipleSelectForm("Vendor", qry, "Code", "Name", TxtMultiBuyer.arrValueMember, TxtMultiBuyer.arrDispalyMember)
    End Sub

    Private Sub TxtMultiItem__My_Click(sender As Object, e As EventArgs) Handles TxtMultiItem._My_Click
        Dim qry As String = "select Item_Code as [Code],Item_Desc as [Name] from TSPL_ITEM_MASTER "
        TxtMultiItem.arrValueMember = clsCommon.ShowMultipleSelectForm("Item", qry, "Code", "Name", TxtMultiItem.arrValueMember, TxtMultiItem.arrDispalyMember)
    End Sub

    Private Sub RptMTPIInHead_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        'ButtonToolTip.SetToolTip(BtnPrint, "Press Alt+P For Print")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
        Reset()
    End Sub

    Private Sub RptMTPIInHead_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.R Then
            Load_Report()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            PageSetupReport_ID = MyBase.Form_ID
            TemplateGridview = gv1
            Load_Report()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub


    Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
        ' print(EnumExportTo.PDF)
        ExportGrid(EnumExportTo.PDF)
    End Sub

    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()

                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptMTPIInHead & "'"))
                If TxtMultiBuyer.arrValueMember IsNot Nothing AndAlso TxtMultiBuyer.arrValueMember.Count > 0 Then
                    arrHeader.Add("Buyer : " + clsCommon.GetMulcallStringWithComma(TxtMultiBuyer.arrValueMember))
                Else
                    arrHeader.Add(("Buyer : All"))
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
                    clsCommon.MyExportToPDF("PO In Hand", gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub rmExport_Click(sender As Object, e As EventArgs) Handles rmExport.Click
        'print(EnumExportTo.Excel)
        ExportGrid(EnumExportTo.Excel)
    End Sub
    'KUNAL > TICKET : BM00000009581 > DATE 26-SEP-2016 > ADDED LOCATION FILTER IN FORM
    Private Sub TxtMultiLocationFinder4__My_Click(sender As Object, e As EventArgs) Handles TxtMultiLocationFinder4._My_Click
        Dim qry As String = "select Location_Code Code , Location_Desc Name from TSPL_LOCATION_MASTER"
        Try
            TxtMultiLocationFinder4.arrValueMember = clsCommon.ShowMultipleSelectForm("RptMTPIInHeadMultiLocFinder", qry, "Code", "Name", TxtMultiLocationFinder4.arrValueMember, TxtMultiLocationFinder4.arrDispalyMember)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
        Dim ReportID As String = PageSetupReport_ID
        If clsCommon.myLen(ReportID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
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
