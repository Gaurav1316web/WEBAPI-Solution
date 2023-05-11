'' Developed by Panch Raj on 07-07-17
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
Public Class rptSaleReco
    Inherits FrmMainTranScreen

    Dim atchqry As String = ""
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim dt As DataTable
    Dim AmountinLacs As Boolean = False
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
    '' new filters

    Dim dtCategory As DataTable
    'Dim strPivotForFinalOuterQuery As String
    Dim MIS_Item_Group As String = ""
    Dim arrBack As List(Of String)
    Dim Document_No As String = ""
    Dim Document_No_Old As String = ""
    Dim FORMTYPE As String = Nothing
#Region "User Defined Functions and Subroutines"
    Public Sub New(ByVal formid As String)
        InitializeComponent()
        FORMTYPE = formid
    End Sub
    Public Sub New()
        InitializeComponent()
    End Sub
#End Region
#Region "Functions"
    Private Sub SetUserMgmtNew()
        '=========Update By Preeti Gupta===============
        MyBase.SetUserMgmt(clsUserMgtCode.rptSaleReco)

        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If

        RadSplitButton1.Visible = MyBase.isExport
        btnQuickExport.Visible = MyBase.isExport
        radbtnBulkExp.Visible = False ' MyBase.isQuickExportFlag
    End Sub
#End Region
    'Sub LoadTypes()
    '    dt = New DataTable
    '    dt.Columns.Add("Code", GetType(String))
    '    dt.Rows.Add("Total Sale")
    '    dt.Rows.Add("Location Wise")
    '    dt.Rows.Add("Item Group Wise")
    '    dt.Rows.Add("Customer Group Wise")
    '    dt.Rows.Add("Item Wise")
    '    dt.Rows.Add("Customer Wise")
    '    dt.Rows.Add("Document Wise")
    '    dt.Rows.Add("Document Detail")
    '    If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowParticluarColumnInSalesRegisterForGopalJee, clsFixedParameterCode.ShowParticluarColumnInSalesRegisterForGopalJee, Nothing)) = 0 Then
    '        dt.Rows.Add("Document Info Level")
    '    End If

    '    ddlReportType.DataSource = dt
    '    ddlReportType.ValueMember = "Code"
    '    ddlReportType.DisplayMember = "Code"
    'End Sub

    'Private Sub txtUOM__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)
    '    Dim qry As String = "select Unit_Code as Code,Unit_Desc as Description from TSPL_UNIT_MASTER"
    '    txtUOM.Value = clsCommon.ShowSelectForm("fndUOMMaster", qry, "Code", "", txtUOM.Value, "Code", isButtonClicked)
    'End Sub
    Sub Print(ByVal IsPrint As Exporter)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " ")

            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
                arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If
            If IsPrint = Exporter.Excel Then
                clsCommon.MyExportToExcelGrid(" Sale Reco:", Gv1, arrHeader, Me.Text)
                Exit Sub
            ElseIf IsPrint = Exporter.PDF Then
                clsCommon.MyExportToPDF("Sale Reco", Gv1, arrHeader, "Sale Reco", True)
                Exit Sub
            End If

            clsCommon.ProgressBarShow()
            'ddlReportType.Enabled = False
            txtState.Enabled = False
            txtLocation.Enabled = False
            txtTransaction.Enabled = False
            txtItemGroup.Enabled = False
            txtItem.Enabled = False
            txtCustomer.Enabled = False
            txtCustGroup.Enabled = False
            'chk_stockingunit.Enabled = False
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()


            'Unit_Code = txtUOM.Value
            clsCommon.ProgressBarUpdate("Loading Data.Please Wait...")
            Dim str As String = ""
            Dim dt As DataTable = Nothing
          
            Gv1.DataSource = Nothing
            Gv1.Columns.Clear()
            Gv1.Rows.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.EnableFiltering = True

            Dim rd As SqlClient.SqlDataReader = ReturnDataReader()
            Me.Gv1.MasterTemplate.LoadFrom(rd)
            rd.Close()
            SetGridFormationOFGV1()
           
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
    Function ReturnDataReader() As SqlClient.SqlDataReader
        Dim obj As clsSaleRegisterParameterType = ReturnFilterData()
        Dim rd As SqlClient.SqlDataReader = clsDBFuncationality.GetDataReader(clsPSInvoiceHead.GetReportDataQuerySaleReco(obj))
        Return rd
    End Function
    Function ReturnData() As DataTable
        Dim obj As clsSaleRegisterParameterType = ReturnFilterData()
        Dim dt As DataTable = clsPSInvoiceHead.GetReportData(obj)
        Return dt

    End Function
    Function ReturnFilterData() As clsSaleRegisterParameterType
        'strPivotForFinalOuterQuery = clsPSInvoiceHead.GetPivotForFinalOuterQry() ''for tax pivoting
        Dim obj As New clsSaleRegisterParameterType
        If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
            obj.Item_Code_List = txtItem.arrValueMember
            'strMCCMaterial += " and xx.[Item Code] in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ") "
        End If
        If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
            obj.Trans_Type_List = txtTransaction.arrValueMember
            'strMCCMaterial += " and xx.[Trans Type] in (" + clsCommon.GetMulcallString(txtTransaction.arrValueMember) + ") "
        Else
            Dim qry As String
            qry = clsPSInvoiceHead.GetAllSaleTransactionTypeQuery()
            Dim dtTrans As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim arrTrans As New ArrayList
            For Each dr As DataRow In dtTrans.Rows
                arrTrans.Add(clsCommon.myCstr(dr.Item("Name")))
            Next
            obj.Trans_Type_List = arrTrans
        End If
        If txtState.arrValueMember IsNot Nothing AndAlso txtState.arrValueMember.Count > 0 Then
            obj.State_List = txtState.arrValueMember
            'strMCCMaterial += " and Loc.State in (" + clsCommon.GetMulcallString(txtState.arrValueMember) + ") "
        End If
        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            obj.Location_Code_List = txtLocation.arrValueMember
            'strMCCMaterial += " and xx.[Location Code] in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") "
        End If

        If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
            obj.Customer_Code_List = txtCustomer.arrValueMember
            'strMCCMaterial += " and xx.[Customer Code] in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") "
        End If
        If txtItemGroup.arrValueMember IsNot Nothing AndAlso txtItemGroup.arrValueMember.Count > 0 Then
            obj.Item_Group_List = txtItemGroup.arrValueMember
            'strMCCMaterial += " and coalesce(Item_Group.Item_Group,'') in (" + clsCommon.GetMulcallString(txtItemGroup.arrValueMember) + ") "
        End If
        '' Done by Panch raj against Ticket No:BM00000007277
        If txtCustGroup.arrValueMember IsNot Nothing AndAlso txtCustGroup.arrValueMember.Count > 0 Then
            obj.Cust_Group_Code_List = txtCustGroup.arrValueMember
            'strMCCMaterial += " and coalesce(Cust.Cust_Group_Code,'') in (" + clsCommon.GetMulcallString(txtCustGroup.arrValueMember) + ") "
        End If
        If txtMultDoc.arrValueMember IsNot Nothing AndAlso txtMultDoc.arrValueMember.Count > 0 Then
            obj.Doc_Code_List = txtMultDoc.arrValueMember
        End If
        If txtMultAccountNo.arrValueMember IsNot Nothing AndAlso txtMultAccountNo.arrValueMember.Count > 0 Then
            obj.Acc_Code_List = txtMultAccountNo.arrValueMember
        End If
        If clsCommon.myLen(Document_No) > 0 Then
            obj.Document_Code = Document_No
            'strMCCMaterial += " and xx.[Document No] = '" & Document_No & "' "
        End If
        Dim Other_Cond As String = ""
        Dim strWhrCatg As String = ""
        strWhrCatg = ""

        obj.rbtnCategorySected = False
        
        If btnPosted.IsChecked Then
            Other_Cond += " and xx.Status=1  "
        ElseIf btnUnposted.IsChecked Then
            Other_Cond += " and xx.Status=0  "
        End If
        obj.Other_Cond = Other_Cond
       
        obj.From_Date = fromDate.Value
        obj.To_Date = ToDate.Value
       
        Return obj
    End Function
    Function GetTaxQuery(ByVal lstTables As List(Of String)) As String
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
    Sub SetGridFormationOFGV1()
        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = False
            'Gv1.Columns(ii).FormatString = "{0:n2}"
        Next
        Gv1.Columns("Document Code").IsVisible = True
        Gv1.Columns("Document Code").Width = 120
        Gv1.Columns("Document Code").HeaderText = "Document No"

        Gv1.Columns("Document Date").IsVisible = True
        Gv1.Columns("Document Date").Width = 120
        Gv1.Columns("Document Date").HeaderText = "Document Date"

        Gv1.Columns("Trans_Type").IsVisible = True
        Gv1.Columns("Trans_Type").Width = 120
        Gv1.Columns("Trans_Type").HeaderText = "Transaction Type"
        
        Gv1.Columns("AR_Doc_No").IsVisible = True
        Gv1.Columns("AR_Doc_No").Width = 120
        Gv1.Columns("AR_Doc_No").HeaderText = "AR Doc No"

        Gv1.Columns("AR_Account_Code").IsVisible = True
        Gv1.Columns("AR_Account_Code").Width = 120
        Gv1.Columns("AR_Account_Code").HeaderText = "AR Account No"

        Gv1.Columns("AR_Account_Desc").IsVisible = True
        Gv1.Columns("AR_Account_Desc").Width = 120
        Gv1.Columns("AR_Account_Desc").HeaderText = "AR Account Desc"

        Gv1.Columns("AR_Account_Amount").IsVisible = True
        Gv1.Columns("AR_Account_Amount").Width = 120
        Gv1.Columns("AR_Account_Amount").HeaderText = "AR Account Amount"

        Gv1.Columns("GL_VoucherNo").IsVisible = True
        Gv1.Columns("GL_VoucherNo").Width = 120
        Gv1.Columns("GL_VoucherNo").HeaderText = "Voucher No"

        Gv1.Columns("GL_Source_Doc_No").IsVisible = True
        Gv1.Columns("GL_Source_Doc_No").Width = 120
        Gv1.Columns("GL_Source_Doc_No").HeaderText = "GL Source Doc No"

        Gv1.Columns("GL_Account_No").IsVisible = True
        Gv1.Columns("GL_Account_No").Width = 120
        Gv1.Columns("GL_Account_No").HeaderText = "GL Account No"

        Gv1.Columns("GL_Account_Desc").IsVisible = True
        Gv1.Columns("GL_Account_Desc").Width = 120
        Gv1.Columns("GL_Account_Desc").HeaderText = "GL Account Desc"

        Gv1.Columns("GL_Account_Amount").IsVisible = True
        Gv1.Columns("GL_Account_Amount").Width = 120
        Gv1.Columns("GL_Account_Amount").HeaderText = "GL Account Amount"


        For i As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(i).BestFit()            
        Next

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
        'End If




        RadPageView1.SelectedPage = RadPageViewPage2
        Gv1.AllowAddNewRow = False
        Gv1.ShowGroupPanel = True
        Gv1.BestFitColumns()
    End Sub
    Sub Reset()
        'txtUOM.Enabled = True
        'chk_stockingunit.Enabled = True
        'chk_stockingunit.Checked = False
        'chk_amtinlacs.Checked = False
        Document_No = ""
        Document_No_Old = ""
        ToDate.Value = clsCommon.GETSERVERDATE()
        'KUNAL > TICKET : BM00000009568 > DATE : 19-OCT-2016
        fromDate.Value = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
        'txtUOM.Value = ""
        'LoadTypes()
        'ddlReportType.SelectedValue = "Total Sale"
        LoadCategory()
        txtState.arrValueMember = Nothing
        txtLocation.arrValueMember = Nothing
        txtItemGroup.arrValueMember = Nothing
        txtItem.arrValueMember = Nothing
        txtCustomer.arrValueMember = Nothing
        txtCustGroup.arrValueMember = Nothing
        'rbtnCategoryAll.IsChecked = True

        'ddlReportType.Enabled = True
        txtState.Enabled = True
        txtLocation.Enabled = True
        txtItemGroup.Enabled = True
        txtItem.Enabled = True
        txtCustomer.Enabled = True
        txtCustGroup.Enabled = True

        If clsCommon.CompairString(clsUserMgtCode.RptFreshSaleRegister1, FORMTYPE) = CompairStringResult.Equal Then
            txtTransaction.Enabled = False
        Else
            txtTransaction.arrValueMember = Nothing
            txtTransaction.Enabled = True
        End If

        'ddlReportType.SelectedIndex = 0
        btnPosted.IsChecked = True
        Gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        'RadPageViewPage2.Text = ddlReportType.SelectedValue
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
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Print(Exporter.Refresh)
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Private Sub rmExport_Click(sender As Object, e As EventArgs)
        'If (Gv1.Rows.Count <= 0) Then
        '    common.clsCommon.MyMessageBoxShow("No Data To Export")
        '    Exit Sub
        'End If
        'Print(Exporter.Excel)
    End Sub
    Private Sub rmSetting_Click(sender As Object, e As EventArgs) Handles rmSetting.Click
        Dim frm As New FrmMailSMSSettingNew2()
        frm.FormId = clsUserMgtCode.RptFreshSaleRegister1
        frm.ShowDialog()
    End Sub
    Private Sub rmSend_Click(sender As Object, e As EventArgs) Handles rmSend.Click
    
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub rptSaleReco_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.R Then
            Print(Exporter.Refresh)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub
    Private Sub rptSaleReco_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        AmountinLacs = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AmountInLacsOnMisSaleRegister, clsFixedParameterCode.AmountInLacsOnMisSaleRegister, Nothing)) = "1", True, False))
        'If AmountinLacs Then
        '    chk_amtinlacs.Visible = True
        'Else
        '    chk_amtinlacs.Visible = False
        'End If

        Reset()
        RadPageView1.SelectedPage = RadPageViewPage1
        Gv1.EnableGrouping = True
        Gv1.ShowGroupPanel = True
        If isDataLoad Then
            fromDate.Value = dtFrom
            ToDate.Value = dtTo
            'txtUOM.Value = Unit_Code
            
            txtLocation.arrValueMember = arrLocation
            txtItem.arrValueMember = arrItem
            txtCustomer.arrValueMember = arrCustomer
            txtTransaction.arrValueMember = arrTransaction
            txtItemGroup.arrValueMember = arrItemGroup

            'If arrCat IsNot Nothing AndAlso arrCat.Count > 0 Then
            '    'rbtnCategorySelect.IsChecked = True
            '    For Each str As String In arrCat.Keys
            '        'For ii As Integer = 0 To gvCategory.RowCount - 1
            '        '    If clsCommon.CompairString(clsCommon.myCstr(gvCategory.Rows(ii).Cells("CODE").Value), str) = CompairStringResult.Equal Then
            '        '        gvCategory.Rows(ii).Cells("SEL").Value = True
            '        '        gvCategory.Rows(ii).Tag = arrCat(str)
            '        '    End If
            '        'Next
            '    Next
            'End If
            'ddlReportType.SelectedValue = strType
            Print(True)
            Me.Visible = True
        End If
        If clsCommon.CompairString(clsUserMgtCode.RptFreshSaleRegister1, FORMTYPE) = CompairStringResult.Equal Then
            Dim arr As New ArrayList
            arr.Add("Fresh Sale")
            arr.Add("Fresh Sale Return")
            txtTransaction.arrValueMember = arr
            txtTransaction.Enabled = False
        End If
    End Sub
    Private Sub Gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles Gv1.CellDoubleClick
        DrillDown()
    End Sub
    Sub DrillDown()
        'Try
        '    If clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Total Sale") = CompairStringResult.Equal Then
        '        If Not arrBack.Contains("Total Sale") Then
        '            arrBack.Add("Total Sale")
        '        End If
        '        ddlReportType.SelectedValue = "Location Wise"

        '        Print(Exporter.Refresh)

        '    ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Location Wise") = CompairStringResult.Equal Then
        '        If Not arrBack.Contains("Location Wise") Then
        '            arrBack.Add("Location Wise")
        '        End If
        '        ddlReportType.SelectedValue = "Item Group Wise"
        '        arrLocation = New ArrayList()
        '        arrLocation = txtLocation.arrValueMember
        '        Dim tmp As New ArrayList()
        '        tmp.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Location Code").Value))
        '        txtLocation.arrValueMember = tmp
        '        Print(Exporter.Refresh)

        '    ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Item Group Wise") = CompairStringResult.Equal Then
        '        If Not arrBack.Contains("Item Group Wise") Then
        '            arrBack.Add("Item Group Wise")
        '        End If
        '        ddlReportType.SelectedValue = "Customer Group Wise"
        '        arrItemGroup = New ArrayList()
        '        arrItemGroup = txtItemGroup.arrValueMember
        '        Dim tmp As New ArrayList()
        '        tmp.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Item Group Code").Value))
        '        txtItemGroup.arrValueMember = tmp
        '        Print(Exporter.Refresh)

        '    ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Customer Group Wise") = CompairStringResult.Equal Then
        '        If Not arrBack.Contains("Customer Group Wise") Then
        '            arrBack.Add("Customer Group Wise")
        '        End If
        '        ddlReportType.SelectedValue = "Item Wise"
        '        arrCustGroup = New ArrayList()
        '        arrCustGroup = txtCustGroup.arrValueMember
        '        Dim tmp As New ArrayList()
        '        tmp.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Customer Group Code").Value))
        '        txtCustGroup.arrValueMember = tmp
        '        Print(Exporter.Refresh)

        '    ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Item Wise") = CompairStringResult.Equal Then
        '        If Not arrBack.Contains("Item Wise") Then
        '            arrBack.Add("Item Wise")
        '        End If
        '        ddlReportType.SelectedValue = "Customer Wise"
        '        arrItem = New ArrayList()
        '        arrItem = txtItem.arrValueMember
        '        Dim tmp As New ArrayList()
        '        tmp.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Item Code").Value))
        '        txtItem.arrValueMember = tmp
        '        Print(Exporter.Refresh)


        '    ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Customer Wise") = CompairStringResult.Equal Then
        '        If Not arrBack.Contains("Customer Wise") Then
        '            arrBack.Add("Customer Wise")
        '        End If
        '        ddlReportType.SelectedValue = "Document Wise"
        '        arrCustomer = New ArrayList()
        '        arrCustomer = txtCustomer.arrValueMember
        '        Dim tmp As New ArrayList()
        '        tmp.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Customer Code").Value))
        '        txtCustomer.arrValueMember = tmp
        '        Print(Exporter.Refresh)

        '    ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Document Wise") = CompairStringResult.Equal Then
        '        If Not arrBack.Contains("Document Wise") Then
        '            arrBack.Add("Document Wise")
        '        End If
        '        ddlReportType.SelectedValue = "Document Detail"
        '        Document_No_Old = Document_No

        '        Document_No = clsCommon.myCstr(Gv1.CurrentRow.Cells("Document No").Value)
        '        Print(Exporter.Refresh)
        '    ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Document Detail") = CompairStringResult.Equal Then
        '        Dim strTransType As String = clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value)
        '        Dim strTransCode As String = clsCommon.myCstr(Gv1.CurrentRow.Cells("Document No").Value)
        '        If clsCommon.myLen(strTransType) > 0 AndAlso clsCommon.myLen(strTransCode) > 0 Then
        '            Select Case strTransType
        '                Case "Fresh Sale"
        '                    clsOpenTransactionForm.OpenTransacionForm(EnumTransType.InvoiceFreshSale, strTransCode)
        '                Case "Product Sale"
        '                    clsOpenTransactionForm.OpenTransacionForm(EnumTransType.Product_Invoice, strTransCode)
        '                Case "Export Sale"
        '                    clsOpenTransactionForm.OpenTransacionForm(EnumTransType.EXPORT_Invoice, strTransCode)
        '                Case "MCC Sale"
        '                    clsOpenTransactionForm.OpenTransacionForm(EnumTransType.MCC_Material, strTransCode)
        '                Case "CSA Sale"
        '                    clsOpenTransactionForm.OpenTransacionForm(EnumTransType.SDCSATrans, strTransCode)
        '                Case "Fresh Sale Return"
        '                    clsOpenTransactionForm.OpenTransacionForm(EnumTransType.Sale_Return, strTransCode)
        '                Case "Product Sale Return"
        '                    clsOpenTransactionForm.OpenTransacionForm(EnumTransType.Product_Return_Sale, strTransCode)
        '                Case "Export Sale Return"
        '                    clsOpenTransactionForm.OpenTransacionForm(EnumTransType.Export_Sale_Return, strTransCode)
        '                Case "CSA Sale Return"
        '                    clsOpenTransactionForm.OpenTransacionForm(EnumTransType.SD_CSATRANS_RETURN, strTransCode)
        '                Case "MCC Sale Return"
        '                    clsOpenTransactionForm.OpenTransacionForm(EnumTransType.MCCMaterialSaleReturn, strTransCode)
        '                Case "Bulk Sale"
        '                    clsOpenTransactionForm.OpenTransacionForm(EnumTransType.Bulk_Invoice, strTransCode)
        '                Case "Bulk Sale Trade"
        '                    clsOpenTransactionForm.OpenTransacionForm(EnumTransType.Bulk_Invoice, strTransCode)
        '                Case "Bulk Sale Return"
        '                    clsOpenTransactionForm.OpenTransacionForm(EnumTransType.Bulk_Return, strTransCode)
        '                    'Case "Bulk Sale Return Trade"
        '                    '    clsOpenTransactionForm.OpenTransacionForm(EnumTransType.Bulk_Return, strTransCode)
        '                Case "Transfer"
        '                    clsOpenTransactionForm.OpenTransacionForm(EnumTransType.transfer, strTransCode)
        '                Case "Misc Sale"
        '                    clsOpenTransactionForm.OpenTransacionForm(EnumTransType.ScrapInvoice, strTransCode)
        '                Case "MCC Transfer"
        '                    clsOpenTransactionForm.OpenTransacionForm(EnumTransType.DispChallan, strTransCode)
        '                Case "Merchant Sale"
        '                    clsOpenTransactionForm.OpenTransacionForm(EnumTransType.MT_Sale_Inv, strTransCode)
        '            End Select

        '        End If
        '    End If
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        'End Try
    End Sub
    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        Dim qry As String = " select cust_code as [Code], Customer_Name as [Name] from tspl_customer_master "
        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("CustMulSel", qry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
    End Sub
    Private Sub txtMultAccountNo__My_Click(sender As Object, e As EventArgs) Handles txtMultAccountNo._My_Click
        Dim qry As String = " select  Account_Code AS Code,Description as [Name] from TSPL_GL_ACCOUNTS "
        txtMultAccountNo.arrValueMember = clsCommon.ShowMultipleSelectForm("GLMulSel", qry, "Code", "Name", txtMultAccountNo.arrValueMember, txtMultAccountNo.arrDispalyMember)
    End Sub
    Private Sub txtMultDoc__My_Click(sender As Object, e As EventArgs) Handles txtMultDoc._My_Click
        Dim qry As String = clsPSInvoiceHead.GetAllSaleTransactionTypeQuery(True)
        txtMultDoc.arrValueMember = clsCommon.ShowMultipleSelectForm("DocMulSel", qry, "Document Code", "Document Date", txtMultDoc.arrValueMember, txtMultDoc.arrDispalyMember)
    End Sub
    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Dim qry As String = " select Item_Code,Item_Desc from TSPL_ITEM_MASTER order by Item_Code "
        txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Item_Code", "Item_Code", txtItem.arrValueMember, txtItem.arrDispalyMember)
    End Sub
    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim stateCond As String = ""
        If txtState.arrValueMember IsNot Nothing AndAlso txtState.arrValueMember.Count > 0 Then
            stateCond = " and state in  (" + clsCommon.GetMulcallString(txtState.arrValueMember) + ") "
        End If
        Dim qry As String = " select Location_Code as Code,Location_Desc as [Name] from TSPL_LOCATION_MASTER where location_type IN ('Physical','Virtual') " & stateCond & " "
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        Dim FrmR As New FrmPendingRequisitionQty
        FrmR.SetDiplayMember(txtLocation, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    End Sub
    Private Sub txtTransaction__My_Click(sender As Object, e As EventArgs) Handles txtTransaction._My_Click
        ' Dim Dt As DataTable = clsDBFuncationality.GetDataTable("select * from TSPL_MODULE_PERMISSION")
        Dim Str As String = String.Empty

        Dim qry As String = clsPSInvoiceHead.GetAllSaleTransactionTypeQuery()

        If clsCommon.CompairString(clsUserMgtCode.RptFreshSaleRegister1, FORMTYPE) <> CompairStringResult.Equal Then
            txtTransaction.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Name", "Name", txtTransaction.arrValueMember, txtTransaction.arrDispalyMember)
        End If
    End Sub
    Sub LoadCategory()
        'dtCategory = clsDBFuncationality.GetDataTable("select ITEM_CATEGORY_CODE AS CodeColumn,ITEM_CATEGORY_CODE+' Description' as CodeDescColumn,DESCRIPTION as DescColumn  from TSPL_ITEM_CATEGORY_LEVEL order by CATEGORY_LEVEL")

        'gvCategory.DataSource = Nothing
        'Dim qry As String = "select cast( 0 as bit) as SEL,ITEM_CATEGORY_CODE AS Code,DESCRIPTION as NAME from TSPL_ITEM_CATEGORY_LEVEL ORDER BY CATEGORY_LEVEL"
        'gvCategory.DataSource = clsDBFuncationality.GetDataTable(qry)

        'gvCategory.Columns("SEL").ReadOnly = False
        'gvCategory.Columns("SEL").Width = 30
        'gvCategory.Columns("SEL").HeaderText = " "

        'gvCategory.Columns("CODE").ReadOnly = True
        'gvCategory.Columns("CODE").Width = 100
        'gvCategory.Columns("CODE").HeaderText = "Code"

        'gvCategory.Columns("NAME").ReadOnly = True
        'gvCategory.Columns("NAME").Width = 200
        'gvCategory.Columns("NAME").HeaderText = "Description"

        'gvCategory.ShowGroupPanel = False
        'gvCategory.AllowAddNewRow = False
        'gvCategory.AllowColumnReorder = False
        'gvCategory.AllowRowReorder = False
        'gvCategory.EnableSorting = False
        'gvCategory.ShowFilteringRow = True
        'gvCategory.EnableFiltering = True
        'gvCategory.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        'gvCategory.MasterTemplate.ShowRowHeaderColumn = True

    End Sub
    Function GetMIS_ITem_GroupColumn() As String
        Dim qry As String = ""
        qry = " select MAP.Custom_Field_Code from TSPL_CUSTOM_FIELD_MAPPING MAP " & _
            " left join TSPL_CUSTOM_FIELD_HEAD CF on MAP.Custom_Field_Code=CF.Code " & _
            " where CF.Name='MIS Item Group' and MAP.PROGRAM_CODE='" & clsUserMgtCode.itemStructure & "'"
        MIS_Item_Group = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
        Return MIS_Item_Group
    End Function
    Private Sub txtItemGroup__My_Click(sender As Object, e As EventArgs) Handles txtItemGroup._My_Click
        Dim qry As String = " select Value as [Code],Description as Name from TSPL_CUSTOM_FIELD_DETAIL where Custom_Field_Code='" & MIS_Item_Group & "' "
        txtItemGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemGroupMulSel", qry, "Code", "Name", txtItemGroup.arrValueMember, txtItemGroup.arrDispalyMember)
    End Sub
    Private Sub Gv1_KeyDown(sender As Object, e As KeyEventArgs) Handles Gv1.KeyDown
        If e.Control And e.KeyCode = Keys.D Then
            DrillDown()
        End If
    End Sub
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        'Try
        '    If clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Total Sale") = CompairStringResult.Equal Then

        '    ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Location Wise") = CompairStringResult.Equal AndAlso arrBack.Contains("Total Sale") Then
        '        arrBack.Remove("Total Sale")
        '        ddlReportType.SelectedValue = "Total Sale"
        '        'txtLocation.arrValueMember = arrLocation
        '        Print(Exporter.Refresh)
        '    ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Item Group Wise") = CompairStringResult.Equal AndAlso arrBack.Contains("Location Wise") Then
        '        arrBack.Remove("Location Wise")
        '        ddlReportType.SelectedValue = "Location Wise"
        '        txtLocation.arrValueMember = arrLocation
        '        Print(Exporter.Refresh)
        '    ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Customer Group Wise") = CompairStringResult.Equal AndAlso arrBack.Contains("Item Group Wise") Then
        '        arrBack.Remove("Item Group Wise")
        '        ddlReportType.SelectedValue = "Item Group Wise"
        '        txtItemGroup.arrValueMember = arrItemGroup
        '        Print(Exporter.Refresh)
        '    ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Item Wise") = CompairStringResult.Equal AndAlso arrBack.Contains("Customer Group Wise") Then
        '        arrBack.Remove("Customer Group Wise")
        '        ddlReportType.SelectedValue = "Customer Group Wise"
        '        txtCustGroup.arrValueMember = arrCustGroup
        '        Print(Exporter.Refresh)
        '    ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Customer Wise") = CompairStringResult.Equal AndAlso arrBack.Contains("Item Wise") Then
        '        arrBack.Remove("Item Wise")
        '        ddlReportType.SelectedValue = "Item Wise"
        '        txtItem.arrValueMember = arrItem
        '        Print(Exporter.Refresh)
        '    ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Document Wise") = CompairStringResult.Equal AndAlso arrBack.Contains("Customer Wise") Then
        '        arrBack.Remove("Customer Wise")
        '        ddlReportType.SelectedValue = "Customer Wise"
        '        txtCustomer.arrValueMember = arrCustomer
        '        Print(Exporter.Refresh)
        '    ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Document Detail") = CompairStringResult.Equal AndAlso arrBack.Contains("Document Wise") Then
        '        arrBack.Remove("Document Wise")
        '        ddlReportType.SelectedValue = "Document Wise"
        '        Document_No = Document_No_Old
        '        'txtCustomer.arrValueMember = arrCustomer
        '        Print(Exporter.Refresh)
        '    End If
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        'End Try
    End Sub
    Private Sub txtCustGroup__My_Click(sender As Object, e As EventArgs) Handles txtCustGroup._My_Click
        Dim qry As String = " select Cust_Group_Code as Code,Cust_Group_Desc as Name from TSPL_CUSTOMER_GROUP_MASTER"
        txtCustGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("CustGroupMulSel", qry, "Code", "Name", txtCustGroup.arrValueMember, txtCustGroup.arrDispalyMember)
    End Sub
    Private Sub btnQuickExport_Click(sender As Object, e As EventArgs) Handles btnQuickExport.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : Sale Reco")
            If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
                arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If

            Dim sfd As SaveFileDialog = New SaveFileDialog()
            Dim filePath As String
            sfd.FileName = Me.Text
            sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
            If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                filePath = sfd.FileName
            Else
                Exit Sub
            End If
            transportSql.exportdataChilRows(Gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            Process.Start(filePath)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub txtState__My_Click(sender As Object, e As EventArgs) Handles txtState._My_Click
        Dim qry As String = " select STATE_CODE as Code,STATE_NAME as Name from TSPL_STATE_MASTER"
        txtState.arrValueMember = clsCommon.ShowMultipleSelectForm("StateMulSel", qry, "Code", "Name", txtState.arrValueMember, txtState.arrDispalyMember)
    End Sub
    Private Sub btnBulkExport_Click(sender As Object, e As EventArgs)
        'BulkExport()
    End Sub
    Sub BulkExport(ByVal FormatType As String)
        Try
            clsCommon.ProgressBarPercentShow()
            clsCommon.ProgressBarPercentUpdate(0, "Generating query for the report..")
            Dim obj As clsSaleRegisterParameterType = ReturnFilterData()
            Dim qry As String = clsPSInvoiceHead.GetReportDataQuery(obj)

            clsCommon.ProgressBarPercentUpdate(10, "Query generated..starting bulk export..")
            'If ddlReportType.SelectedValue = "Total Sale" Then
            '    qry = "select * from (" & qry & ") PP order by [Total FAT KG]"
            '    transportSql.BulkExport("Sale_Register", qry, "order by [Total FAT KG]", FormatType)
            '    Exit Sub
            'ElseIf ddlReportType.SelectedValue = "Location Wise" Then
            '    qry = "select * from (" & qry & ") PP order by [Location Code],[Location Name]"
            '    transportSql.BulkExport("Sale_Register", qry, "order by [Location Code],[Location Name]", FormatType)
            '    Exit Sub
            'ElseIf ddlReportType.SelectedValue = "Item Group Wise" Then
            '    qry = "select * from (" & qry & ") PP order by [Location Code],[Location Name],[Item Group Code],[Item Group Description]"
            '    transportSql.BulkExport("Sale_Register", qry, "order by [Location Code],[Location Name],[Item Group Code],[Item Group Description]", FormatType)
            '    Exit Sub
            'ElseIf ddlReportType.SelectedValue = "Customer Group Wise" Then
            '    qry = "select * from (" & qry & ") PP order by [Location Code],[Location Name],[Item Group Code],[Item Group Description],[Customer Group Code],[Customer Group Description]"
            '    transportSql.BulkExport("Sale_Register", qry, "order by [Location Code],[Location Name],[Item Group Code],[Item Group Description],[Customer Group Code],[Customer Group Description]", FormatType)
            '    Exit Sub
            'ElseIf ddlReportType.SelectedValue = "Item Wise" Then
            '    qry = "select * from (" & qry & ") PP order by [Location Code],[Location Name],[Item Group Code],[Item Group Description],[Customer Group Code],[Customer Group Description],[Item Code],[Item Name]"
            '    transportSql.BulkExport("Sale_Register", qry, " order by [Location Code],[Location Name],[Item Group Code],[Item Group Description],[Customer Group Code],[Customer Group Description],[Item Code],[Item Name]", FormatType)
            '    Exit Sub
            'ElseIf ddlReportType.SelectedValue = "Customer Wise" Then
            '    qry = "select * from (" & qry & ") PP order by [Location Code],[Location Name],[Item Group Code],[Item Group Description],[Customer Group Code],[Customer Group Description],[Item Code],[Item Name],[Customer Code],[Customer Name]"
            '    transportSql.BulkExport("Sale_Register", qry, "order by [Location Code],[Location Name],[Item Group Code],[Item Group Description],[Customer Group Code],[Customer Group Description],[Item Code],[Item Name],[Customer Code],[Customer Name]", FormatType)
            '    Exit Sub
            'ElseIf ddlReportType.SelectedValue = "Document Wise" Then

            '    transportSql.BulkExport("Sale_Register", qry, "order by convert(date,[Document_Date],103),[Document No]", FormatType)
            '    Exit Sub
            'ElseIf ddlReportType.SelectedValue = "Document Detail" Then
            '    transportSql.BulkExport("Sale_Register", qry, "order by convert(date,[Document_Date],103),[Document No]", FormatType)
            '    Exit Sub
            'ElseIf obj.ReportType = "Document Info Level" Then
            '    transportSql.BulkExport("Sale_Register", qry, "order by convert(date,[Document_Date],103),[Document No]", FormatType)
            '    Exit Sub
            'End If


            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow("Data exported successfully")
        Catch ex As Exception
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            clsCommon.ProgressBarPercentHide()
        End Try
    End Sub
    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles BulkExportCsv.Click
        BulkExport("csv")
    End Sub
    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        If (Gv1.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow("No Data To Export")
            Exit Sub
        End If
        Print(Exporter.Excel)
    End Sub
    Private Sub RadMenuItem3_Click(sender As Object, e As EventArgs) Handles BulkExportXls.Click
        BulkExport("xls")
    End Sub
End Class
