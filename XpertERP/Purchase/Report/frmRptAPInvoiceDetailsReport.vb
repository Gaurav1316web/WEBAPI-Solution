Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms

Imports System.IO
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports common
Public Class FrmRptAPInvoiceDetailsReport
    Inherits FrmMainTranScreen

    Private Sub txtVendor__My_Click(sender As Object, e As EventArgs) Handles txtVendor._My_Click
        Dim qry As String = "select Vendor_Code as Code,Vendor_Name as Name from TSPL_VENDOR_MASTER Where TSPL_VENDOR_MASTER.Status='N' "
        txtVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("FILTERVENDOR", qry, "Code", "Name", txtVendor.arrValueMember, txtVendor.arrDispalyMember)
    End Sub
    Private Sub txtVendorGroup__My_Click(sender As Object, e As EventArgs) Handles txtVendorGroup._My_Click
        Dim qry As String = "select Ven_Group_Code as Code,Group_Desc as Name from TSPL_VENDOR_GROUP order by Ven_Group_Code"
        txtVendorGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("FILTERVENDORGROUP", qry, "Code", "Name", txtVendorGroup.arrValueMember, txtVendorGroup.arrDispalyMember)
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        'Dim qry As String = " select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER"
        Dim qry As String = " select Segment_code as Code, Description as Name from  TSPL_GL_SEGMENT_CODE where seg_no= 7 "
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("FILTERLOCATION", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    End Sub
    Sub Reset()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtVendorGroup.arrValueMember = Nothing
        txtVendor.arrValueMember = Nothing
        txtLocation.arrValueMember = Nothing
        'chkSummary.Checked = True
        gv.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub FrmRptAPInvoiceDetailsReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Reset()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        Print(Exporter.Refresh)
    End Sub
    Private Sub Print(ByVal IsPrint As Exporter)
        Try
            If txtFromDate.Value > txtToDate.Value Then
                common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater then to Date", Me.Text)
                txtFromDate.Focus()
                Exit Sub
            End If
            Dim MainQuery As String = String.Empty
            Dim strWhrClause As String = String.Empty




            strWhrClause = "  and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) >= '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and  convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <= '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' and TSPL_VENDOR_INVOICE_HEAD.Posting_Date is not null"
            If txtVendorGroup.arrValueMember IsNot Nothing AndAlso txtVendorGroup.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtVendorGroup.arrValueMember)
                strWhrClause += " and TSPL_VENDOR_MASTER.Vendor_Group_Code in (" + ss + ")  "
            End If

            If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtVendor.arrValueMember)
                strWhrClause += " and TSPL_VENDOR_INVOICE_HEAD.Vendor_Code in (" + ss + ")  "

            End If

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtLocation.arrValueMember)
                strWhrClause += " and TSPL_VENDOR_INVOICE_HEAD.Loc_Code in (" + ss + ")  "
            End If

            ' change by priti for vendor name from vendor master KDI/05/07/18-000390
            MainQuery = " select TSPL_VENDOR_INVOICE_DETAIL.Document_No as [Document No],max(TSPL_VENDOR_INVOICE_HEAD.Description) as Description , " & _
                "max(TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date) as [Document Date], " & _
                "max(case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' then 'Invoice' When TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 'Debit Note' " & _
                "when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 'Credit Note' else '' end) as [Document Type] , " & _
                "max(TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No) as [Vendor Invoice No],max(TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date) as [Vendor Invoice Date], " & _
                "max(TSPL_VENDOR_MASTER.Vendor_Group_Code) as [Vendor Group Code], max(TSPL_VENDOR_MASTER.Vendor_Group_Code_Desc) as [Vendor Group Code Desc], " & _
                "max(TSPL_VENDOR_INVOICE_HEAD.Vendor_Code)  as [Vendor Code], max(TSPL_VENDOR_MASTER.Vendor_Name) as [Vendor Name] , " & _
                "max( TSPL_VENDOR_INVOICE_HEAD.Loc_Code) as [Location Code] ,max(TSPL_GL_SEGMENT_CODE.Description) as [Location Desc], " & _
                "sum(TSPL_VENDOR_INVOICE_DETAIL.Amount) as [Invoice Amount], sum( isnull (TSPL_VENDOR_INVOICE_DETAIL.Discount,0)) as [Discount amount], " & _
                "sum(TSPL_VENDOR_INVOICE_DETAIL.Amount_less_Discount) as [Amount After Discount],sum(TSPL_VENDOR_INVOICE_DETAIL.Total_Tax) as [Total Tax], " & _
                "sum(TSPL_VENDOR_INVOICE_DETAIL.Total_Amount) as [Included Tax Amount],max(TSPL_VENDOR_INVOICE_HEAD.Total_Add_Charge) as [Total Add Charge], " & _
                "max(Document_Total) as [Document Total]  from TSPL_VENDOR_INVOICE_DETAIL left outer join " & _
                "TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_DETAIL.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No left outer join " & _
                "TSPL_VENDOR_MASTER on  TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code left outer join " & _
                "TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code=TSPL_VENDOR_INVOICE_HEAD.Loc_Code " & _
                "where 2=2  " + strWhrClause + "   group by TSPL_VENDOR_INVOICE_DETAIL.Document_No   order by TSPL_VENDOR_INVOICE_DETAIL.Document_No  asc "
           

            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(MainQuery)
            gv.DataSource = Nothing

            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.DataSource = dtgv
            gv.BestFitColumns()
            ReStoreGridLayout()
            If dtgv Is Nothing OrElse dtgv.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
            RadPageView1.SelectedPage = RadPageViewPage2
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        Export(EnumExportTo.Excel)
        'Try
        '    If gv.Rows.Count > 0 Then
        '        Dim arrHeader As List(Of String) = New List(Of String)()
        '        arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
        '        arrHeader.Add("AP Invoice Details Report")
        '        clsCommon.MyExportToExcelGrid("AP Invoice Details Report", gv, arrHeader, "AP Invoice Details Report")
        '    End If
        'Catch ex As Exception
        '    common.clsCommon.MyMessageBoxShow(ex.Message)
        'End Try
    End Sub

    'Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs)
    '    Try
    '        If gv.Rows.Count > 0 Then
    '            Dim arrHeader As List(Of String) = New List(Of String)()
    '            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
    '            arrHeader.Add("AP Invoice Details Report")
    '            clsCommon.MyExportToPDF("AP Invoice Details Report", gv, arrHeader, "AP Invoice Details Report")
    '        End If
    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub

    Private Sub Export(ByVal exporter As EnumExportTo)
        Try
            If gv.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("From Date : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To Date " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmRptAPInvoiceDetailsReport & "'"))

                If txtVendorGroup.arrValueMember IsNot Nothing AndAlso txtVendorGroup.arrValueMember.Count > 0 Then
                    arrHeader.Add("Vendor Group : " + clsCommon.GetMulcallStringWithComma(txtVendorGroup.arrDispalyMember))
                End If
                If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                    arrHeader.Add("Vendor : " + clsCommon.GetMulcallStringWithComma(txtVendor.arrDispalyMember))
                End If
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
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
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
                    'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                    'Process.Start(filePath)
                Else
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("AP Invoice Details Report", gv, arrHeader, "AP Invoice Details Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv.CellDoubleClick
        Try
            Dim strTransCode As String = clsCommon.myCstr(gv.CurrentRow.Cells("Document No").Value)
            Dim strInvoiceNo As String = clsCommon.myCstr(gv.CurrentRow.Cells("Vendor Invoice No").Value)
            If e.Column.Name = "Document No" Then
                If clsCommon.myLen(strTransCode) > 0 Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmVendorService, strTransCode)
                End If
            End If

            'If e.Column.Name = "Vendor Invoice No" Then
            '    If clsCommon.myLen(strInvoiceNo) > 0 Then
            '        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseInvoice, strInvoiceNo)
            '    End If
            'End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
       

    End Sub

    Private Sub lblVendor_Click(sender As Object, e As EventArgs) Handles lblVendor.Click

    End Sub

    Private Sub RadMenuItem3_Click(sender As Object, e As EventArgs) Handles RadMenuItem3.Click
        Export(EnumExportTo.PDF)
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv.Columns.Count - 1 Step ii + 1
                        gv.Columns(ii).IsVisible = False
                        gv.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            Dim obj As New clsGridLayout()
            gv.MasterTemplate.FilterDescriptors.Clear()
            obj = New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
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
