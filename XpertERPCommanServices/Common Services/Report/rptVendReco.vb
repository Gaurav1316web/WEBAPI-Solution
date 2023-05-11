Imports common
Imports System.IO
Imports System.Net
Imports System.Net.Configuration
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Xml
Imports Outlook = Microsoft.Office.Interop.Outlook
Imports System.Text.RegularExpressions
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared

Public Class rptVendReco

#Region "Varibales"
    Dim atchqry As String = ""
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim dt As DataTable
    '' new varables 
    Public isDataLoad As Boolean = False
    Public dtFrom As Date
    Public dtTo As Date
    Public strType As String
    Public arrItem As ArrayList
    Public arrTransaction As ArrayList
    Public arrCat As Dictionary(Of String, Object) = Nothing
    Public Unit_Code As String = Nothing
    Public arrLocation As ArrayList
    Public arrCustomer As ArrayList
    Public arrCustGroup As ArrayList
    Public arrItemGroup As ArrayList

    Public arrGLAccount As ArrayList
    Public Stocking_Uom As Boolean = False
    '' new filters
    Dim dtCategory As DataTable
    Dim strPivotForFinalOuterQuery As String
    Dim strPivotForAddChargeFinalOutersumQuery As String
    Dim MIS_Item_Group As String = ""
    Dim arrBack As List(Of String)
    Dim Document_No As String = ""
    Dim Document_No_Old As String = ""
#End Region

    Private Sub rptVendReco_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MyLabel9.Visible = True
        cboType.Visible = True
        btnBack.Visible = True
        Me.Visible = False
        arrBack = New List(Of String)
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New ")
        GetMIS_ITem_GroupColumn()
        If clsCommon.myLen(MIS_Item_Group) <= 0 Then
            clsCommon.MyMessageBoxShow("MIS Item Group Custom field is not create in Item Structure.")
        End If
        LoadReportTypes()
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
        LoadTypes()
        Document_No = ""
        ddlReportType.SelectedValue = "Total Purchase"
        txtState.arrValueMember = Nothing
        txtLocation.arrValueMember = Nothing
        txtTransaction.arrValueMember = Nothing
        txtCustomer.arrValueMember = Nothing
        txtCustGroup.arrValueMember = Nothing
        ddlReportType.Enabled = True
        txtLocation.Enabled = True
        txtState.Enabled = True
        txtTransaction.Enabled = True
        txtCustomer.Enabled = True
        txtCustGroup.Enabled = True
        ddlReportType.SelectedIndex = 0
        btnPosted.IsChecked = True
        Gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        RadPageViewPage2.Text = ddlReportType.SelectedValue
        cboType.SelectedValue = "Detail"
        RadPageView1.SelectedPage = RadPageViewPage1
        Gv1.EnableGrouping = True
        Gv1.ShowGroupPanel = True
        If isDataLoad Then
            fromDate.Value = dtFrom
            ToDate.Value = dtTo
            txtLocation.arrValueMember = arrLocation
            txtCustomer.arrValueMember = arrCustomer
            txtTransaction.arrValueMember = arrTransaction
            ddlReportType.SelectedValue = strType
            Print(True)
            Me.Visible = True
        End If
    End Sub

    Sub LoadReportTypes()
        dt = New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Rows.Add("Detail")
        dt.Rows.Add("Account Wise")

        cboType.DataSource = dt
        cboType.ValueMember = "Code"
        cboType.DisplayMember = "Code"
    End Sub

    Private Sub SetUserMgmtNew()
        MyBase.SetUserMgmt(clsUserMgtCode.rptPurReco)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
    End Sub

    Sub LoadTypes()
        dt = New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Rows.Add("Total Purchase")
        dt.Rows.Add("Location Wise")
        dt.Rows.Add("Vendor Group Wise")
        dt.Rows.Add("Item Wise")
        dt.Rows.Add("Vendor Wise")
        dt.Rows.Add("Document Wise")
        dt.Rows.Add("Document Detail")
        dt.Rows.Add("Document Type Info")

        ddlReportType.DataSource = dt
        ddlReportType.ValueMember = "Code"
        ddlReportType.DisplayMember = "Code"
    End Sub

    Sub Print(ByVal IsPrint As Exporter, Optional ByVal BulkExport As Integer = 0)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
             
            If clsCommon.myLen(ddlReportType.Text) > 0 Then
                arrHeader.Add("Report Type : " + ddlReportType.Text)
            End If
             
            If Not IsNothing(txtLocation.arrValueMember) Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If
             
            If Not IsNothing(txtCustomer.arrValueMember) Then
                arrHeader.Add("Vendor : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember))
            End If
            If btnAll.CheckState = True Or btnPosted.CheckState = True Or btnUnposted.CheckState = True Then
                arrHeader.Add("Status : " + IIf(btnAll.IsChecked, "ALL", IIf(btnPosted.IsChecked, "Posted", "Unposted")))
            End If
            If IsPrint = Exporter.Excel Then
                clsCommon.MyExportToExcelGrid(" Purchase Register:" + ddlReportType.SelectedValue, Gv1, arrHeader, Me.Text)
                Exit Sub
            ElseIf IsPrint = Exporter.PDF Then
                clsCommon.MyExportToPDF("Purchase Register" + ddlReportType.SelectedValue, Gv1, arrHeader, "Purchase Register", True)
                Exit Sub
            End If
            clsCommon.ProgressBarShow()
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()

            clsCommon.ProgressBarUpdate("Loading Data.Please Wait...")
            Dim str As String = ""
            Dim dt As DataTable = Nothing
            Dim strRunQuery As String = ""
            Dim strMain As String = Nothing
            Dim obj As New clsPurchaseReco
            If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
                obj.Transaction = txtTransaction.arrValueMember
            End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                obj.Location = txtLocation.arrValueMember
            End If
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                obj.Vendor_Code = txtCustomer.arrValueMember
            End If
            If txtMultAccountNo.arrValueMember IsNot Nothing AndAlso txtMultAccountNo.arrValueMember.Count > 0 Then
                obj.Acc_Code = txtMultAccountNo.arrValueMember
            End If
            obj.IncludeAllDoc = True
            obj.From_Date = fromDate.Value
            obj.To_Date = ToDate.Value
            obj.Account_Set = fndMultiAccSet.arrValueMember
            obj.Vendor_Group = fndMultiVendorGroup.arrValueMember
            obj.ShowMismatchDoc = chkMismatchDoc.Checked
            strRunQuery = clsPurchaseInvoiceHead.GetVendorRecoQry(obj)

             
            If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Account Wise") = CompairStringResult.Equal Then
                strRunQuery = "select AP_Account_Code,max(AP_Account_Desc) as AP_Account_Desc,sum(AP_Account_Amount) as AP_Account_Amount, gl_account_no,max(GL_Account_Desc) as GL_Account_Desc,sum(GL_Account_Amount) as GL_Account_Amount,sum([Diff Amount]) as [Diff Amount] from (" + strRunQuery + ")xxx group by AP_Account_Code, gl_account_no"
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Detail") = CompairStringResult.Equal Then
            End If

            RadPageViewPage2.Text = "Purchase Reco"
            dt = clsDBFuncationality.GetDataTable(strRunQuery)
            Gv1.DataSource = Nothing
            Gv1.Columns.Clear()
            Gv1.Rows.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.EnableFiltering = True
            Gv1.Tag = ddlReportType.SelectedValue
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            Else
                EnableDisableAllControl(False)
                Gv1.DataSource = dt
                SetGridFormationOFGV1()
            End If
            FindAndRestoreGridLayout(Me)
            Gv1.MasterTemplate.AllowAddNewRow = False
            RadPageView1.SelectedPage = RadPageViewPage2
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        Finally
            clsCommon.ProgressBarHide()
        End Try
    End Sub

    Sub EnableDisableAllControl(ByVal val As Boolean)
        ddlReportType.Enabled = val
        txtTransaction.Enabled = val
        txtState.Enabled = val
        txtLocation.Enabled = val
        txtCustGroup.Enabled = val
        txtCustomer.Enabled = val
        txtMultAccountNo.Enabled = val
        RadGroupBox6.Enabled = val
        chkSerializeInv.Enabled = val
        RadGroupBox3.Enabled = val
        fndMultiAccSet.Enabled = val
        fndMultiVendorGroup.Enabled = val
        cboType.Enabled = val
    End Sub

    Sub SetGridFormationOFGV1()
        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = False
            Gv1.Columns(ii).BestFit()
        Next

        If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Account Wise") = CompairStringResult.Equal Then
            Gv1.Columns("AP_Account_Code").IsVisible = True
            Gv1.Columns("AP_Account_Code").Width = 120
            Gv1.Columns("AP_Account_Code").HeaderText = "AP Account No"

            Gv1.Columns("AP_Account_Desc").IsVisible = True
            Gv1.Columns("AP_Account_Desc").Width = 120
            Gv1.Columns("AP_Account_Desc").HeaderText = "AP Account Desc"

            Gv1.Columns("AP_Account_Amount").IsVisible = True
            Gv1.Columns("AP_Account_Amount").Width = 120
            Gv1.Columns("AP_Account_Amount").HeaderText = "AP Account Amount"

            Gv1.Columns("GL_Account_No").IsVisible = True
            Gv1.Columns("GL_Account_No").Width = 120
            Gv1.Columns("GL_Account_No").HeaderText = "GL Account No"

            Gv1.Columns("GL_Account_Desc").IsVisible = True
            Gv1.Columns("GL_Account_Desc").Width = 120
            Gv1.Columns("GL_Account_Desc").HeaderText = "GL Account Desc"

            Gv1.Columns("GL_Account_Amount").IsVisible = True
            Gv1.Columns("GL_Account_Amount").Width = 120
            Gv1.Columns("GL_Account_Amount").HeaderText = "GL Account Amount"

            Gv1.Columns("Diff Amount").IsVisible = True
            Gv1.Columns("Diff Amount").Width = 120

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim intCount As Integer = 0
            For Each col As GridViewColumn In Gv1.Columns
                If col.Name.Contains("AP_Account_Amount") = True Or col.Name.Contains("GL_Account_Amount") = True Or col.Name.Contains("Diff Amount") = True Then
                    Dim item As New GridViewSummaryItem(col.Name, "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item)
                End If
            Next
            Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Detail") = CompairStringResult.Equal Then
            Gv1.Columns("Document Code").IsVisible = True
            Gv1.Columns("Document Code").Width = 120
            Gv1.Columns("Document Code").HeaderText = "Document No"

            Gv1.Columns("Document Date").IsVisible = True
            Gv1.Columns("Document Date").Width = 120
            Gv1.Columns("Document Date").HeaderText = "Document Date"

            Gv1.Columns("Trans_Type").IsVisible = True
            Gv1.Columns("Trans_Type").Width = 120
            Gv1.Columns("Trans_Type").HeaderText = "Transaction Type"

            Gv1.Columns("Document Type").IsVisible = True
            Gv1.Columns("Document Type").Width = 100
            Gv1.Columns("Document Type").HeaderText = "Document Type"

            Gv1.Columns("Vendor_Code").IsVisible = True
            Gv1.Columns("Vendor_Code").Width = 120
            Gv1.Columns("Vendor_Code").HeaderText = "Vendor Code"

            Gv1.Columns("Vendor_Name").IsVisible = True
            Gv1.Columns("Vendor_Name").Width = 120
            Gv1.Columns("Vendor_Name").HeaderText = "Vendor Name"

            Gv1.Columns("Diff Amount").IsVisible = True
            Gv1.Columns("Diff Amount").Width = 120
            Gv1.Columns("Diff Amount").HeaderText = "Diff Amount"

            'Diff Amount
            Gv1.Columns("AP_Doc_No").IsVisible = True
            Gv1.Columns("AP_Doc_No").Width = 120
            Gv1.Columns("AP_Doc_No").HeaderText = "AP Doc No"

            Gv1.Columns("AP_Account_Code").IsVisible = True
            Gv1.Columns("AP_Account_Code").Width = 120
            Gv1.Columns("AP_Account_Code").HeaderText = "AP Account No"

            Gv1.Columns("AP_Account_Desc").IsVisible = True
            Gv1.Columns("AP_Account_Desc").Width = 120
            Gv1.Columns("AP_Account_Desc").HeaderText = "AP Account Desc"

            Gv1.Columns("AP_Account_Amount").IsVisible = True
            Gv1.Columns("AP_Account_Amount").Width = 120
            Gv1.Columns("AP_Account_Amount").HeaderText = "AP Account Amount"

            Gv1.Columns("GL_VoucherNo").IsVisible = True
            Gv1.Columns("GL_VoucherNo").Width = 120
            Gv1.Columns("GL_VoucherNo").HeaderText = "Voucher No"

            Gv1.Columns("Source_Doc_No").IsVisible = True
            Gv1.Columns("Source_Doc_No").Width = 120
            Gv1.Columns("Source_Doc_No").HeaderText = "GL Source Doc No"

            Gv1.Columns("GL_Account_No").IsVisible = True
            Gv1.Columns("GL_Account_No").Width = 120
            Gv1.Columns("GL_Account_No").HeaderText = "GL Account No"

            Gv1.Columns("GL_Account_Desc").IsVisible = True
            Gv1.Columns("GL_Account_Desc").Width = 120
            Gv1.Columns("GL_Account_Desc").HeaderText = "GL Account Desc"

            Gv1.Columns("GL_Account_Amount").IsVisible = True
            Gv1.Columns("GL_Account_Amount").Width = 120
            Gv1.Columns("GL_Account_Amount").HeaderText = "GL Account Amount"


            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim intCount As Integer = 0
            For Each col As GridViewColumn In Gv1.Columns
                If clsCommon.CompairString(col.Name, "Total FAT KG") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "Total SNF KG") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "FAT KG") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "SNF KG") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "Quantity") = CompairStringResult.Equal Then
                    Dim item1 As New GridViewSummaryItem(col.Name, "{0:F3}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)

                ElseIf col.Name.Contains("Amount") = True Or col.Name.Contains("Amt") = True Or col.Name.Contains("Total") = True Then
                    Dim item As New GridViewSummaryItem(col.Name, "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item)
                ElseIf col.Name.Contains("Rate") = True Or col.Name.Contains("%") = True Then
                    Dim item As New GridViewSummaryItem(col.Name, "{0:F2}", GridAggregateFunction.Avg)
                    summaryRowItem.Add(item)
                End If
            Next
            Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        End If


        





        RadPageView1.SelectedPage = RadPageViewPage2
        Gv1.AllowAddNewRow = False
        Gv1.ShowGroupPanel = True
        Gv1.BestFitColumns()
    End Sub

    Sub Reset()
        EnableDisableAllControl(True)
    End Sub

    Enum Exporter
        Excel = 0
        PDF = 1
        Print = 2
        Refresh = 3
    End Enum

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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

    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click

    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Print(Exporter.Refresh)
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub rmExport_Click(sender As Object, e As EventArgs) Handles rmExport.Click
        If (Gv1.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow("No Data To Export")
            Exit Sub
        End If
        Print(Exporter.Excel)
    End Sub

    Private Sub rmSetting_Click(sender As Object, e As EventArgs) Handles rmSetting.Click
        Dim frm As New FrmMailSMSSettingNew2()
        frm.FormId = clsUserMgtCode.RptFreshSaleRegister1
        frm.ShowDialog()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub rptVendReco_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.R Then
            Print(Exporter.Refresh)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub

    Private Sub Gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles Gv1.CellDoubleClick
        DrillDown()
    End Sub

    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        Dim qry As String = " select Vendor_Code as Code,Vendor_name as Name from TSPL_VENDOR_master  WHERE  Status='N'  "
        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("VenMulSel", qry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
        Dim FrmR As New FrmPendingRequisitionQty
        FrmR.SetDiplayMember(txtCustomer, "Vendor_name", "TSPL_VENDOR_master", "Vendor_Code")
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim stateCond As String = ""
        If txtState.arrValueMember IsNot Nothing AndAlso txtState.arrValueMember.Count > 0 Then
            stateCond = " and state in  (" + clsCommon.GetMulcallString(txtState.arrValueMember) + ") "
        End If
        Dim qry As String = " select Location_Code as Code,Location_Desc as [Name] from TSPL_LOCATION_MASTER  where location_type IN  ('Physical','Virtual') " & stateCond & "  "
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        Dim FrmR As New FrmPendingRequisitionQty
        FrmR.SetDiplayMember(txtLocation, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    End Sub

    Private Sub txtTransaction__My_Click(sender As Object, e As EventArgs) Handles txtTransaction._My_Click
        Dim qry As String = " Select xxx.Code,  xxx.Name From (" & _
                                 " Select distinct 'PI' As Code,    'Purchase Invoice' As Name from TSPL_PI_HEAD " & _
                                 " Union  Select distinct 'MCC' As Code,    'Milk Receipt' As Name from TSPL_MILK_RECEIPT_HEAD " & _
                                 " Union  Select distinct 'Bulk' As Code,    'Bulk Purchase' As Name from tspl_Bulk_milk_purchase_Invoice_head " & _
                                  " Union  Select distinct 'Bulk Purchase Return' As Code,    'Bulk Purchase Return' As Name from TSPL_BULK_MILK_PURCHASE_RETURN_HEAD " & _
                                 " Union  Select distinct 'MCC Transfer' As Code,    'MCC Transfer' As Name from TSPL_MILK_TRANSFER_IN " & _
                                 " Union  Select distinct 'Transfer' As Code,    'Transfer' As Name from TSPL_TRANSFER_ORDER_HEAD " & _
                                  " Union  Select distinct 'Transfer Return' As Code,    'Transfer Return' As Name from TSPL_TRANSFER_RETURN " & _
                                 " Union  Select distinct 'Return' As Code,    'Purchase Return' As Name from TSPL_PR_HEAD " & _
                                 " union Select distinct 'MT' As Code,    'Merchant Trade' As Name from TSPL_PI_HEAD " & _
                                 " ) xxx"
        txtTransaction.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulPur", qry, "Name", "Name", txtTransaction.arrValueMember, txtTransaction.arrDispalyMember)
    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub

    Function GetMIS_ITem_GroupColumn() As String
        Dim qry As String = ""
        qry = " select MAP.Custom_Field_Code from TSPL_CUSTOM_FIELD_MAPPING MAP " & _
            " left join TSPL_CUSTOM_FIELD_HEAD CF on MAP.Custom_Field_Code=CF.Code " & _
            " where CF.Name='MIS Item Group' and MAP.PROGRAM_CODE='" & clsUserMgtCode.itemStructure & "'"
        MIS_Item_Group = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
        Return MIS_Item_Group
    End Function

    Private Sub Gv1_KeyDown(sender As Object, e As KeyEventArgs) Handles Gv1.KeyDown
        If e.Control And e.KeyCode = Keys.D Then
            DrillDown()
        End If
    End Sub

    Private Sub txtCustGroup__My_Click(sender As Object, e As EventArgs) Handles txtCustGroup._My_Click
        Dim qry As String = " select Ven_Group_Code as Code,Group_desc as Name from TSPL_VENDOR_group"
        txtCustGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("VenGroupMulSel", qry, "Code", "Name", txtCustGroup.arrValueMember, txtCustGroup.arrDispalyMember)
        Dim FrmR As New FrmPendingRequisitionQty
        FrmR.SetDiplayMember(txtCustGroup, "Group_desc", "TSPL_VENDOR_group", "Ven_Group_Code")
    End Sub

    Private Sub btnQuickExport_Click(sender As Object, e As EventArgs) Handles btnQuickExport.Click
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : Purchase Reco")
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " ")
            If clsCommon.myLen(ddlReportType.Text) > 0 Then
                arrHeader.Add("Report Type : " + ddlReportType.Text)
            End If
            If Not IsNothing(txtState.arrValueMember) Then
                arrHeader.Add("State : " + clsCommon.GetMulcallStringWithComma(txtState.arrDispalyMember))
            End If
            If Not IsNothing(txtLocation.arrValueMember) Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If
            If Not IsNothing(txtCustGroup.arrValueMember) Then
                arrHeader.Add("Vendor Group : " + clsCommon.GetMulcallStringWithComma(txtCustGroup.arrDispalyMember))
            End If
            If Not IsNothing(txtCustomer.arrValueMember) Then
                arrHeader.Add("Vendor : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember))
            End If
            If btnAll.CheckState = True Or btnPosted.CheckState = True Or btnUnposted.CheckState = True Then
                arrHeader.Add("Status : " + IIf(btnAll.IsChecked, "ALL", IIf(btnPosted.IsChecked, "Posted", "Unposted")))
            End If
            Dim sfd As SaveFileDialog = New SaveFileDialog()
            Dim filePath As String
            sfd.FileName = Me.Text
            sfd.Filter = "Excel 2007 (*.xlsx) |*.xlsx;|Excel 97-2003 (*.xls)|*.xlsx;|CSV Files (*.csv) |*.csv"
            If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                filePath = sfd.FileName
            Else
                Exit Sub
            End If
            transportSql.exportdataChilRows(Gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader) 'frm.Text)
            common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            Process.Start(filePath)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Public Shared Function GetTaxQuery(ByVal lstTables As List(Of String)) As String
        Dim qry As String = String.Empty
        If Not lstTables Is Nothing AndAlso lstTables.Count > 0 Then
            For Each TableName As String In lstTables
                For intloop As Integer = 1 To 10
                    If clsCommon.myLen(qry) <= 0 Then
                        qry = "select TAX" & intloop & " from " & TableName & ""
                    Else
                        qry = qry & " Union  " & "select TAX" & intloop & " from " & TableName & ""
                    End If
                Next
            Next
        Else
            Return qry
        End If
        Return qry
    End Function

    Public Shared Function GetAddChargeQuery(ByVal lstTables As List(Of String)) As String
        Dim qry As String = String.Empty
        If Not lstTables Is Nothing AndAlso lstTables.Count > 0 Then
            For Each TableName As String In lstTables
                For intloop As Integer = 1 To 10
                    If clsCommon.myLen(qry) <= 0 Then
                        qry = "select Add_Charge_Code" & intloop & "  from " & TableName & ""
                    Else
                        qry = qry & " Union  " & "select Add_Charge_Code" & intloop & " from " & TableName & ""
                    End If
                Next
            Next
        Else
            Return qry
        End If
        Return qry
    End Function

    Public Shared Function GetAddChargeZeroQuery(ByVal lstTables As List(Of String)) As String
        Dim qry As String = String.Empty
        If Not lstTables Is Nothing AndAlso lstTables.Count > 0 Then
            For Each TableName As String In lstTables
                For intloop As Integer = 1 To 10
                    If clsCommon.myLen(qry) <= 0 Then
                        qry = "select 'AC_'+Add_Charge_Code" & intloop & " as Add_Charge_Code" & intloop & "  from " & TableName & ""
                    Else
                        qry = qry & " Union  " & "select 'AC_'+Add_Charge_Code" & intloop & " as Add_Charge_Code" & intloop & " from " & TableName & ""
                    End If
                Next
            Next
        Else
            Return qry
        End If
        If clsCommon.myLen(qry) > 0 Then
            qry = "select * from( " & qry & ") as t1 where Add_Charge_Code1 not in ('AC_')"
        End If

        Return qry
    End Function

    Private Sub txtState__My_Click(sender As Object, e As EventArgs) Handles txtState._My_Click
        Dim qry As String = " select STATE_CODE as Code,STATE_NAME as Name from TSPL_STATE_MASTER"
        txtState.arrValueMember = clsCommon.ShowMultipleSelectForm("StateMulSel", qry, "Code", "Name", txtState.arrValueMember, txtState.arrDispalyMember)
        Dim FrmR As New FrmPendingRequisitionQty
        FrmR.SetDiplayMember(txtState, "STATE_NAME", "TSPL_sTATE_MASTER", "STATE_CODE")
    End Sub

    Private Sub BulkExportCSV_Click(sender As Object, e As EventArgs) Handles BulkExportCSV.Click
        Print(Exporter.Refresh, 1)
    End Sub

    Private Sub BulkExportXls_Click(sender As Object, e As EventArgs) Handles BulkExportXls.Click
        Print(Exporter.Refresh, 2)
    End Sub

    Private Sub txtMultAccountNo__My_Click(sender As Object, e As EventArgs) Handles txtMultAccountNo._My_Click
        Dim qry As String = " select  Account_Code AS Code,Description as [Name] from TSPL_GL_ACCOUNTS "
        txtMultAccountNo.arrValueMember = clsCommon.ShowMultipleSelectForm("GLMulSel", qry, "Code", "Name", txtMultAccountNo.arrValueMember, txtMultAccountNo.arrDispalyMember)
    End Sub

    Private Sub fndMultiAccSet__My_Click(sender As Object, e As EventArgs) Handles fndMultiAccSet._My_Click
        Dim qry As String = " select Acct_Set_Code as [Code],Acct_Set_Desc as Name,Payable_Account as [Payable Account],Discount_Account as [Discount Account],Advance_Account as [Advance Account],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_Code as [Company Code],CURRENCY_CODE as [Currency Code],EXCHANGE_LOSS_ACCOUNT as [Exchange Loss Account],EXCHANGE_GAIN_ACCOUNT as [Exchange Gain Account] from TSPL_VENDOR_ACCOUNT_SET "
        fndMultiAccSet.arrValueMember = clsCommon.ShowMultipleSelectForm("VenAccMulSel", qry, "Code", "Name", fndMultiAccSet.arrValueMember, fndMultiAccSet.arrDispalyMember)
    End Sub

    Private Sub fndMultiVendorGroup__My_Click(sender As Object, e As EventArgs) Handles fndMultiVendorGroup._My_Click
        Dim qry As String = " select Ven_Group_Code as Code,Group_Desc as Name from TSPL_VENDOR_GROUP  "
        fndMultiVendorGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("VenGrpMulSel", qry, "Code", "Name", fndMultiVendorGroup.arrValueMember, fndMultiVendorGroup.arrDispalyMember)
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Account Wise") = CompairStringResult.Equal Then
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Detail") = CompairStringResult.Equal AndAlso arrBack.Contains("Account Wise") Then
                arrBack.Remove("Account Wise")
                cboType.SelectedValue = "Account Wise"
                txtMultAccountNo.arrValueMember = arrGLAccount
                Print(Exporter.Refresh)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Sub DrillDown()
        Try
            If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Account Wise") = CompairStringResult.Equal Then
                If Not arrBack.Contains("Account Wise") Then
                    arrBack.Add("Account Wise")
                End If
                cboType.SelectedValue = "Detail"
                arrGLAccount = New ArrayList()
                arrGLAccount = txtMultAccountNo.arrValueMember
                Dim tmp As New ArrayList()
                tmp.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("AP_Account_Code").Value))
                txtMultAccountNo.arrValueMember = tmp
                Print(Exporter.Refresh)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
End Class


