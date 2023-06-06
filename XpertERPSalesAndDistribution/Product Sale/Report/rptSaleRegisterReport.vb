Imports common
Imports System.IO
Imports System.Net
Imports System.Net.Configuration
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Xml
Imports System.Text.RegularExpressions
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
'================Create by Preeti Gupta=========
'' changes by richa agarwal BM00000007178,BM00000007508,BM00000007921
'Sanjay TEC/03/07/19-000923,Filter ->Category-> TYPE Column ambigious error
Public Class RptSaleRegisterReport
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
    Public arrState As ArrayList
    '' new filters

    Dim dtCategory As DataTable
    Dim strPivotForFinalOuterQuery As String
    Dim MIS_Item_Group As String = ""
    Dim arrBack As List(Of String)
    Dim Document_No As String = ""
    Dim Document_No_Old As String = ""
    Dim FORMTYPE As String = Nothing
    Dim IsFormLoad As Boolean = False
    Dim arrLoc As String = Nothing

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
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptsaleRegisterReport)
        'MyBase.SetUserMgmt(FORMTYPE)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If

        'RadSplitButton1.Visible = MyBase.isExport
        btnQuickExport.Visible = MyBase.isExport
        'radbtnBulkExp.Visible = MyBase.isQuickExportFlag
        'btnCreateExportTemplate.Visible = MyBase.isQuickExportFlag
    End Sub
#End Region
    Private Sub LOCATIONRIGTHS()
        Try
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData()

            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub LoadTypes()
        dt = New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Rows.Add("Total Sale")
        dt.Rows.Add("Location Wise")
        dt.Rows.Add("Item Group Wise")
        dt.Rows.Add("Customer Group Wise")
        dt.Rows.Add("Item Wise")
        dt.Rows.Add("Customer Wise")
        dt.Rows.Add("Document Wise")
        dt.Rows.Add("Document Detail")
        'If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowParticluarColumnInSalesRegisterForGopalJee, clsFixedParameterCode.ShowParticluarColumnInSalesRegisterForGopalJee, Nothing)) = 0 Then
        '    dt.Rows.Add("Document Info Level")
        'End If
        ''richa 11 Sep,2018
        dt.Rows.Add("Net Sale")
        ddlReportType.DataSource = dt
        ddlReportType.ValueMember = "Code"
        ddlReportType.DisplayMember = "Code"
    End Sub


    Sub LoadSubCategory()
        dt = New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Rows.Add("All")
        dt.Rows.Add("Cash Sale")
        dt.Rows.Add("Scrap Sale")
        dt.Rows.Add("Job Work")

        ddlSubCategory.DataSource = dt
        ddlSubCategory.ValueMember = "Code"
        ddlSubCategory.DisplayMember = "Code"
    End Sub

    Private Sub txtUOM__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtUOM._MYValidating
        Dim qry As String = "select Unit_Code as Code,Unit_Desc as Description from TSPL_UNIT_MASTER"
        txtUOM.Value = clsCommon.ShowSelectForm("fndUOMMaster", qry, "Code", "", txtUOM.Value, "Code", isButtonClicked)
    End Sub


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
                clsCommon.MyExportToExcelGrid(" Sale Register:" + ddlReportType.SelectedValue, Gv1, arrHeader, Me.Text)
                Exit Sub
            ElseIf IsPrint = Exporter.PDF Then
                clsCommon.MyExportToPDF("Sale Register" + ddlReportType.SelectedValue, Gv1, arrHeader, "Sale Register", True)
                Exit Sub
            End If
            clsCommon.ProgressBarShow()
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Unit_Code = txtUOM.Value
            clsCommon.ProgressBarUpdate("Loading Data.Please Wait...")
            Dim str As String = ""
            Dim dt As DataTable = Nothing
            RadPageViewPage2.Text = ddlReportType.SelectedValue
            Gv1.DataSource = Nothing
            Gv1.Columns.Clear()
            Gv1.Rows.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.EnableFiltering = True
            Gv1.Tag = ddlReportType.SelectedValue
            Dim rd As SqlClient.SqlDataReader = ReturnDataReader()
            Me.Gv1.MasterTemplate.LoadFrom(rd)
            rd.Close()
            SetGridFormationOFGV1()
            'FindAndRestoreGridLayout(Me)
            PageSetupReport_ID = clsERPFuncationality.GetReportID(MyBase.Form_ID, ddlReportType.Text)
            ReStoreGridLayout()
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
        Dim rd As SqlClient.SqlDataReader = clsPSInvoiceHead.GetReportDataReader(obj)
        strPivotForFinalOuterQuery = obj.strPivotForFinalOuterQuery
        Return rd
    End Function

    Function ReturnData() As DataTable
        Dim obj As clsSaleRegisterParameterType = ReturnFilterData()
        Dim dt As DataTable = clsPSInvoiceHead.GetReportData(obj)
        Return dt

        ''Dim qryList As ArrayList
        ''qryList = XpertERPEngine.clsPSInvoiceHead.ReturnQuery(fromDate.Value, ToDate.Value, Unit_Code)
        ''Dim strMCCMaterial As String = qryList(0)
        'strPivotForFinalOuterQuery = clsPSInvoiceHead.GetPivotForFinalOuterQry()
        'Dim obj As New clsSaleRegisterParameterType
        'If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
        '    obj.Item_Code_List = txtItem.arrValueMember
        '    'strMCCMaterial += " and xx.[Item Code] in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ") "
        'End If
        'If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
        '    obj.Trans_Type_List = txtTransaction.arrValueMember
        '    'strMCCMaterial += " and xx.[Trans Type] in (" + clsCommon.GetMulcallString(txtTransaction.arrValueMember) + ") "
        'Else
        '    Dim qry As String
        '    Qry = clsPSInvoiceHead.GetAllSaleTransactionTypeQuery()
        '    Dim dtTrans As DataTable = clsDBFuncationality.GetDataTable(Qry)
        '    Dim arrTrans As New ArrayList
        '    For Each dr As DataRow In dtTrans.Rows
        '        arrTrans.Add(clsCommon.myCstr(dr.Item("Name")))
        '    Next
        '    obj.Trans_Type_List = arrTrans
        'End If
        'If txtState.arrValueMember IsNot Nothing AndAlso txtState.arrValueMember.Count > 0 Then
        '    obj.State_List = txtState.arrValueMember
        '    'strMCCMaterial += " and Loc.State in (" + clsCommon.GetMulcallString(txtState.arrValueMember) + ") "
        'End If
        'If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
        '    obj.Location_Code_List = txtLocation.arrValueMember
        '    'strMCCMaterial += " and xx.[Location Code] in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") "
        'End If

        'If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
        '    obj.Customer_Code_List = txtCustomer.arrValueMember
        '    'strMCCMaterial += " and xx.[Customer Code] in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") "
        'End If
        'If txtItemGroup.arrValueMember IsNot Nothing AndAlso txtItemGroup.arrValueMember.Count > 0 Then
        '    obj.Item_Group_List = txtItemGroup.arrValueMember
        '    'strMCCMaterial += " and coalesce(Item_Group.Item_Group,'') in (" + clsCommon.GetMulcallString(txtItemGroup.arrValueMember) + ") "
        'End If
        ' '' Done by Panch raj against Ticket No:BM00000007277
        'If txtCustGroup.arrValueMember IsNot Nothing AndAlso txtCustGroup.arrValueMember.Count > 0 Then
        '    obj.Cust_Group_Code_List = txtCustGroup.arrValueMember
        '    'strMCCMaterial += " and coalesce(Cust.Cust_Group_Code,'') in (" + clsCommon.GetMulcallString(txtCustGroup.arrValueMember) + ") "
        'End If
        'If clsCommon.myLen(Document_No) > 0 Then
        '    obj.Document_Code = Document_No
        '    'strMCCMaterial += " and xx.[Document No] = '" & Document_No & "' "
        'End If
        'Dim Other_Cond As String = ""
        'Dim strWhrCatg As String = ""
        'strWhrCatg = ""
        'If rbtnCategorySelect.IsChecked Then
        '    Dim IsApplicable As Boolean = False
        '    For ii As Integer = 0 To gvCategory.RowCount - 1
        '        If clsCommon.myCBool(gvCategory.Rows(ii).Cells("SEL").Value) Then
        '            If IsApplicable Then
        '                strWhrCatg += " and "
        '            End If
        '            IsApplicable = True
        '            strWhrCatg += "("
        '            Dim arr As Dictionary(Of String, Object) = gvCategory.Rows(ii).Tag
        '            If arr IsNot Nothing AndAlso arr.Count > 0 Then
        '                strWhrCatg += " [" + clsCommon.myCstr(gvCategory.Rows(ii).Cells("CODE").Value) + "] in ("
        '                Dim isFirstTime As Boolean = True
        '                For Each strInn As String In arr.Keys
        '                    If Not isFirstTime Then
        '                        strWhrCatg += ","
        '                    End If
        '                    strWhrCatg += "'" + strInn + "'"
        '                    isFirstTime = False
        '                Next
        '                strWhrCatg += ")"
        '            Else
        '                strWhrCatg += " 2=2  "
        '            End If
        '            strWhrCatg += ")"
        '        End If
        '    Next
        '    If Not IsApplicable Then
        '        Throw New Exception("Please select at least one category")
        '    End If
        '    Other_Cond += " and (" + strWhrCatg + ")"
        'End If
        'If btnPosted.IsChecked Then
        '    Other_Cond += " and xx.Status=1  "
        'ElseIf btnUnposted.IsChecked Then
        '    Other_Cond += " and xx.Status=0  "
        'End If
        'obj.Other_Cond = Other_Cond
        'obj.Unit_Code = txtUOM.Value
        'obj.ReportType = ddlReportType.SelectedValue
        'obj.From_Date = fromDate.Value
        'obj.To_Date = ToDate.Value

    End Function
    Function ReturnFilterData() As clsSaleRegisterParameterType
        'strPivotForFinalOuterQuery = clsPSInvoiceHead.GetPivotForFinalOuterQry() ''for tax pivoting
        Dim obj As New clsSaleRegisterParameterType
        If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
            obj.Item_Code_List = txtItem.arrValueMember
            'strMCCMaterial += " and xx.[Item Code] in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ") "
        End If

        If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then

            If chkFarmerSale.Checked = True Then
                If txtTransaction.arrValueMember.Contains("MCC Sale Farmer") = False Then
                    txtTransaction.arrValueMember.Add("MCC Sale Farmer")
                End If
                If txtTransaction.arrValueMember.Contains("MCC Sale Return Farmer") = False Then
                    txtTransaction.arrValueMember.Add("MCC Sale Return Farmer")
                End If
            Else
                If txtTransaction.arrValueMember.Contains("MCC Sale Farmer") = True Then
                    txtTransaction.arrValueMember.Remove("MCC Sale Farmer")
                End If
                If txtTransaction.arrValueMember.Contains("MCC Sale Return Farmer") = True Then
                    txtTransaction.arrValueMember.Remove("MCC Sale Return Farmer")
                End If
            End If

        End If

        If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
            obj.Trans_Type_List = txtTransaction.arrValueMember
            'strMCCMaterial += " and xx.[Trans Type] in (" + clsCommon.GetMulcallString(txtTransaction.arrValueMember) + ") "
            If obj.Trans_Type_List.Contains("MCC Sale Farmer") = True OrElse obj.Trans_Type_List.Contains("MCC Sale Return Farmer") = True Then
                chkFarmerSale.Checked = True
            End If
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
        Else
            If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                Dim strLoc As String = " select Location_Code from TSPL_LOCATION_MASTER where 2=2 "
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    strLoc += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
                End If
                Dim dtLoc As DataTable = clsDBFuncationality.GetDataTable(strLoc)
                Dim arrGLLocCode As New ArrayList
                If dtLoc IsNot Nothing AndAlso dtLoc.Rows.Count > 0 Then
                    For Each dr As DataRow In dtLoc.Rows
                        arrGLLocCode.Add(clsCommon.myCstr(dr("Location_Code")))
                    Next
                End If
                obj.Location_Code_List = arrGLLocCode
            End If

            'clsCommon.GetMulcallString(objCommonVar.strCurrUserLocations) 'clsCommon.GetMulcallString(objCommonVar.strCurrUserLocations)
            'strMCCMaterial += " and xx.[Location Code] in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") "
        End If

        If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
            obj.Customer_Code_List = txtCustomer.arrValueMember
            'strMCCMaterial += " and xx.[Customer Code] in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") "
            ''Else
            ''    If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserCustomers) > 0 Then
            ''        Dim strLoc As String = " select TSPL_CUSTOMER_MASTER.Cust_Code as Code from TSPL_CUSTOMER_MASTER  
            ''                                     where TSPL_CUSTOMER_MASTER.Cust_Code in (" + objCommonVar.strCurrUserCustomers + ")"

            ''        Dim dtLoc As DataTable = clsDBFuncationality.GetDataTable(strLoc)
            ''        Dim arrGLLocCode As New ArrayList
            ''        If dtLoc IsNot Nothing AndAlso dtLoc.Rows.Count > 0 Then
            ''            For Each dr As DataRow In dtLoc.Rows
            ''                arrGLLocCode.Add(clsCommon.myCstr(dr("Code")))
            ''            Next
            ''        End If
            ''        obj.Customer_Code_List = arrGLLocCode
            ''    End If
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
        If txtZone.arrValueMember IsNot Nothing AndAlso txtZone.arrValueMember.Count > 0 Then
            obj.Zone_List = txtZone.arrValueMember
        End If
        If TxtRoute.arrValueMember IsNot Nothing AndAlso TxtRoute.arrValueMember.Count > 0 Then
            obj.Route_List = TxtRoute.arrValueMember
        End If

        If clsCommon.myLen(Document_No) > 0 Then
            obj.Document_Code = Document_No
            'strMCCMaterial += " and xx.[Document No] = '" & Document_No & "' "
        End If

        Dim Other_Cond As String = ""
        Dim strWhrCatg As String = ""
        strWhrCatg = ""

        obj.rbtnCategorySected = False
        If rbtnCategorySelect.IsChecked Then
            Dim IsApplicable As Boolean = False
            For ii As Integer = 0 To gvCategory.RowCount - 1
                If clsCommon.myCBool(gvCategory.Rows(ii).Cells("SEL").Value) Then
                    If IsApplicable Then
                        strWhrCatg += " and "
                    End If
                    IsApplicable = True
                    strWhrCatg += "("
                    Dim arr As Dictionary(Of String, Object) = gvCategory.Rows(ii).Tag
                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
                        strWhrCatg += " VirtualCategoryTabel.[" + clsCommon.myCstr(gvCategory.Rows(ii).Cells("CODE").Value) + "] in ("
                        Dim isFirstTime As Boolean = True
                        For Each strInn As String In arr.Keys
                            If Not isFirstTime Then
                                strWhrCatg += ","
                            End If
                            strWhrCatg += "'" + strInn + "'"
                            isFirstTime = False
                        Next
                        strWhrCatg += ")"
                    Else
                        strWhrCatg += " 2=2  "
                    End If
                    strWhrCatg += ")"
                End If
            Next
            If Not IsApplicable Then
                Throw New Exception("Please select at least one category")
            End If
            Other_Cond += " and (" + strWhrCatg + ")"

            ''==================Monika 11/04/2017==========
            obj.rbtnCategorySected = True
            ''===================================================
        End If
        If btnPosted.IsChecked Then
            Other_Cond += " and xx.Status=1  "
        ElseIf btnUnposted.IsChecked Then
            Other_Cond += " and xx.Status=0  "
        End If

        'If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal AndAlso ddlSubCategory.SelectedValue = "Job Work" Then
        '    Other_Cond += " AND VirtualCategoryTabel.[JW] IS NOT NULL "
        'ElseIf clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal AndAlso ddlSubCategory.SelectedValue = "Scrap Sale" Then
        '    Other_Cond += " AND VirtualCategoryTabel.[SCRAP] IS NOT NULL "
        'End If

        obj.Other_Cond = Other_Cond
        obj.Unit_Code = txtUOM.Value
        obj.ReportType = ddlReportType.SelectedValue
        obj.From_Date = fromDate.Value
        obj.To_Date = ToDate.Value
        obj.stockinguom = chk_stockingunit.Checked
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
            '' richa UDL/09/08/18-000213
            If clsCommon.CompairString(ddlSubCategory.SelectedValue, "Cash Sale") = CompairStringResult.Equal Then
                obj.MiscSaleSubCategory = "C"
            ElseIf clsCommon.CompairString(ddlSubCategory.SelectedValue, "Scrap Sale") = CompairStringResult.Equal Then
                obj.MiscSaleSubCategory = "S"
            ElseIf clsCommon.CompairString(ddlSubCategory.SelectedValue, "Job Work") = CompairStringResult.Equal Then
                obj.MiscSaleSubCategory = "J"
            Else
                obj.MiscSaleSubCategory = ""
            End If
            'obj.MiscSaleSubCategory = IIf(ddlSubCategory.SelectedValue = "Cash Sale", "C", IIf(ddlSubCategory.SelectedValue = "Scrap Sale", "S", ""))
        End If
        obj.Include_Debit_Credit = chkIncludeDebitCredit.Checked
        obj.Include_MCCFarmerSale = chkFarmerSale.Checked
        obj.QuickLoad = chkQuickLoad.Checked
        obj.PickCSASaleFromSalePatti = chkShowCSASaleFromSalePatti.Checked
        obj.Program_Code = Me.Form_ID
        'Dim dt As DataTable = clsPSInvoiceHead.GetReportData(obj)
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
    '=====================Added by preeti gupta against ticket no [BM00000009916,BM00000009858]
    Sub SetGridFormationOFGV1()
        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = False
            If ddlReportType.SelectedValue = "Document Detail" AndAlso Gv1.Columns(ii).Name.Contains("TCS%") = True Then
                Gv1.Columns(ii).FormatString = "{0:n3}"
            Else
                Gv1.Columns(ii).FormatString = "{0:n2}"
            End If

        Next

        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowParticluarColumnInSalesRegisterForGopalJee, clsFixedParameterCode.ShowParticluarColumnInSalesRegisterForGopalJee, Nothing)) = 1 Then
            If ddlReportType.SelectedValue = "Document Detail" Then

                Gv1.Columns("Trans Type").IsVisible = True
                Gv1.Columns("Trans Type").Width = 70
                Gv1.Columns("Trans Type").HeaderText = "Trans Type"

                For i As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(i).BestFit()
                    Gv1.Columns(i).IsVisible = True

                    If Gv1.Columns(i).Name.Contains("Code") = True And Not (Gv1.Columns(i).Name.Contains("Item Code") = False Or Gv1.Columns(i).Name.Contains("Customer Code") = False Or Gv1.Columns(i).Name.Contains("Location Code") = False) Then
                        Gv1.Columns(i).IsVisible = False
                    Else
                        Gv1.Columns(i).IsVisible = True
                    End If
                    If Gv1.Columns(i).Name.Contains("AR") = True Then
                        Gv1.Columns(i).IsVisible = False
                    End If
                Next
            ElseIf ddlReportType.SelectedValue = "Document Wise" Then

                For i As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(i).BestFit()
                    Gv1.Columns(i).IsVisible = True

                    If Gv1.Columns(i).Name.Contains("Code") = True And Not (Gv1.Columns(i).Name.Contains("Item Code") = False Or Gv1.Columns(i).Name.Contains("Customer Code") = False Or Gv1.Columns(i).Name.Contains("Location Code") = False) Then
                        Gv1.Columns(i).IsVisible = False
                    Else
                        Gv1.Columns(i).IsVisible = True
                    End If
                    'If Gv1.Columns(i).Name.Contains("AR") = True Then
                    '    Gv1.Columns(i).IsVisible = False
                    'End If
                Next
            ElseIf ddlReportType.SelectedValue = "Total Sale" Then

                'Gv1.Columns("Total FAT KG").IsVisible = True
                'Gv1.Columns("Total SNF KG").IsVisible = True
                Gv1.Columns("Total Amount").IsVisible = True

                For i As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(i).BestFit()
                    Gv1.Columns(i).IsVisible = True

                    If Gv1.Columns(i).Name.Contains("Code") = True And Not (Gv1.Columns(i).Name.Contains("Item Code") = False Or Gv1.Columns(i).Name.Contains("Customer Code") = False Or Gv1.Columns(i).Name.Contains("Warehouse Code") = False) Then
                        Gv1.Columns(i).IsVisible = False
                    End If
                Next
            ElseIf ddlReportType.SelectedValue = "Location Wise" Then

                'Gv1.Columns("Total FAT KG").IsVisible = True
                'Gv1.Columns("Total SNF KG").IsVisible = True
                Gv1.Columns("Total Amount").IsVisible = True
                Gv1.Columns("Warehouse Code").IsVisible = True
                Gv1.Columns("warehouse Name").IsVisible = True

                For i As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(i).BestFit()
                    Gv1.Columns(i).IsVisible = True

                    If Gv1.Columns(i).Name.Contains("Code") = True And Not (Gv1.Columns(i).Name.Contains("Item Code") = False Or Gv1.Columns(i).Name.Contains("Customer Code") = False Or Gv1.Columns(i).Name.Contains("Warehouse Code") = False) Then
                        Gv1.Columns(i).IsVisible = False
                    End If
                Next
            ElseIf ddlReportType.SelectedValue = "Item Group Wise" Then

                'Gv1.Columns("Total FAT KG").IsVisible = True
                'Gv1.Columns("Total SNF KG").IsVisible = True
                Gv1.Columns("Total Amount").IsVisible = True
                Gv1.Columns("Warehouse Code").IsVisible = True
                Gv1.Columns("warehouse Name").IsVisible = True

                Gv1.Columns("Product Group Code").IsVisible = True
                Gv1.Columns("Product Group Description").IsVisible = True

                For i As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(i).BestFit()
                    Gv1.Columns(i).IsVisible = True

                    If Gv1.Columns(i).Name.Contains("Code") = True And Not (Gv1.Columns(i).Name.Contains("Product Code") = False Or Gv1.Columns(i).Name.Contains("Customer Code") = False Or Gv1.Columns(i).Name.Contains("Warehouse Code") = False) Then
                        Gv1.Columns(i).IsVisible = False
                    End If
                Next
            ElseIf ddlReportType.SelectedValue = "Customer Group Wise" Then

                'Gv1.Columns("Total FAT KG").IsVisible = True
                'Gv1.Columns("Total SNF KG").IsVisible = True
                Gv1.Columns("Total Amount").IsVisible = True
                Gv1.Columns("Warehouse Code").IsVisible = True
                Gv1.Columns("warehouse Name").IsVisible = True

                Gv1.Columns("Product Group Code").IsVisible = True
                Gv1.Columns("Product Group Description").IsVisible = True

                Gv1.Columns("Customer Group Code").IsVisible = True
                Gv1.Columns("Customer Group Description").IsVisible = True

                For i As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(i).BestFit()
                    Gv1.Columns(i).IsVisible = True

                    If Gv1.Columns(i).Name.Contains("Code") = True And Not (Gv1.Columns(i).Name.Contains("Product Code") = False Or Gv1.Columns(i).Name.Contains("Customer Code") = False Or Gv1.Columns(i).Name.Contains("Warehouse Code") = False) Then
                        Gv1.Columns(i).IsVisible = False
                    End If
                Next
            ElseIf ddlReportType.SelectedValue = "Item Wise" Then

                'Gv1.Columns("Total FAT KG").IsVisible = True
                'Gv1.Columns("Total SNF KG").IsVisible = True
                Gv1.Columns("Total Amount").IsVisible = True
                Gv1.Columns("Warehouse Code").IsVisible = True
                Gv1.Columns("warehouse Name").IsVisible = True

                Gv1.Columns("Product Group Code").IsVisible = True
                Gv1.Columns("Product Group Description").IsVisible = True

                Gv1.Columns("Customer Group Code").IsVisible = True
                Gv1.Columns("Customer Group Description").IsVisible = True

                Gv1.Columns("Product Code").IsVisible = True
                Gv1.Columns("Product Name").IsVisible = True

                '' KUNAL > WHOLLY JOY > DATE : 3 -JAN -2016 > TICKET : NONE , ONLY VERBAL DISCUSSION WITH ANAND
                Gv1.Columns("Quantity").IsVisible = True
                Gv1.Columns("UOM").IsVisible = True

                For i As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(i).BestFit()
                    Gv1.Columns(i).IsVisible = True

                    If Gv1.Columns(i).Name.Contains("Code") = True And Not (Gv1.Columns(i).Name.Contains("Product Code") = False Or Gv1.Columns(i).Name.Contains("Customer Code") = False Or Gv1.Columns(i).Name.Contains("Warehouse Code") = False) Then
                        Gv1.Columns(i).IsVisible = False
                    End If
                Next
            ElseIf ddlReportType.SelectedValue = "Customer Wise" Then

                'Gv1.Columns("Total FAT KG").IsVisible = True
                'Gv1.Columns("Total SNF KG").IsVisible = True
                Gv1.Columns("Total Amount").IsVisible = True
                Gv1.Columns("Warehouse Code").IsVisible = True
                Gv1.Columns("warehouse Name").IsVisible = True

                Gv1.Columns("Customer Group Code").IsVisible = True
                Gv1.Columns("Customer Group Description").IsVisible = True


                Gv1.Columns("Product Code").IsVisible = True
                Gv1.Columns("Product Name").IsVisible = True

                Gv1.Columns("Customer Code").IsVisible = True
                Gv1.Columns("Customer Name").IsVisible = True

                For i As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(i).BestFit()
                    Gv1.Columns(i).IsVisible = True

                    If Gv1.Columns(i).Name.Contains("Code") = True And Not (Gv1.Columns(i).Name.Contains("Product Code") = False Or Gv1.Columns(i).Name.Contains("Customer Code") = False Or Gv1.Columns(i).Name.Contains("Warehouse Code") = False) Then
                        Gv1.Columns(i).IsVisible = False
                    End If
                Next
            End If

            If chk_amtinlacs.Checked Then
                If ddlReportType.SelectedValue = "Total Sale" OrElse ddlReportType.SelectedValue = "Document Wise" OrElse ddlReportType.SelectedValue = "Location Wise" OrElse ddlReportType.SelectedValue = "Item Group Wise" OrElse ddlReportType.SelectedValue = "Customer Group Wise" OrElse ddlReportType.SelectedValue = "Item Wise" OrElse ddlReportType.SelectedValue = "Customer Wise" Then
                    Gv1.Columns("Total Amount").HeaderText = "Total Amount (in lacs)"
                    For i As Integer = 0 To Gv1.Rows.Count - 1
                        Gv1.Rows(i).Cells("Total Amount").Value = (clsCommon.myCdbl(Gv1.Rows(i).Cells("Total Amount").Value) / 100000)
                    Next
                End If
            End If

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim intCount As Integer = 0
            For Each col As GridViewColumn In Gv1.Columns
                'If clsCommon.CompairString(col.Name, "Total FAT KG") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "Total SNF KG") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "FAT KG") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "SNF KG") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "Quantity") = CompairStringResult.Equal Then
                '    Dim item1 As New GridViewSummaryItem(col.Name, "{0:F3}", GridAggregateFunction.Sum)
                '    summaryRowItem.Add(item1)

                If col.Name.Contains("Amount") = True Or col.Name.Contains("Amt") = True Or col.Name.Contains("Total") = True Or strPivotForFinalOuterQuery.Contains(col.Name) = True Then
                    Dim item As New GridViewSummaryItem(col.Name, "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item)
                ElseIf col.Name.Contains("Rate") = True Or col.Name.Contains("%") = True Then
                    Dim item As New GridViewSummaryItem(col.Name, "{0:F2}", GridAggregateFunction.Avg)
                    summaryRowItem.Add(item)
                End If
            Next
            Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Else
            If ddlReportType.SelectedValue = "Document Info Level" Then

                Gv1.Columns("Trans Type").IsVisible = True
                Gv1.Columns("Trans Type").Width = 70
                Gv1.Columns("Trans Type").HeaderText = "Trans Type"

                For i As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(i).BestFit()
                    Gv1.Columns(i).IsVisible = True

                    If Gv1.Columns(i).Name.Contains("Code") = True And Not (Gv1.Columns(i).Name.Contains("Item Code") = False Or Gv1.Columns(i).Name.Contains("Customer Code") = False Or Gv1.Columns(i).Name.Contains("Location Code") = False) Then
                        Gv1.Columns(i).IsVisible = False
                    Else
                        Gv1.Columns(i).IsVisible = True
                    End If
                    If Gv1.Columns(i).Name.Contains("AR") = True Then
                        Gv1.Columns(i).IsVisible = False
                    End If
                Next
            ElseIf ddlReportType.SelectedValue = "Document Detail" Then

                Gv1.Columns("Trans Type").IsVisible = True
                Gv1.Columns("Trans Type").Width = 70
                Gv1.Columns("Trans Type").HeaderText = "Trans Type"

                For i As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(i).BestFit()
                    Gv1.Columns(i).IsVisible = True

                    If Gv1.Columns(i).Name.Contains("Code") = True And Not (Gv1.Columns(i).Name.Contains("Item Code") = False Or Gv1.Columns(i).Name.Contains("Customer Code") = False Or Gv1.Columns(i).Name.Contains("Location Code") = False) Then
                        Gv1.Columns(i).IsVisible = False
                    Else
                        Gv1.Columns(i).IsVisible = True
                    End If
                    If Gv1.Columns(i).Name.Contains("AR") = True Then
                        Gv1.Columns(i).IsVisible = False
                    End If
                Next

                Gv1.Columns("Customer Zone Description").IsVisible = False

                Gv1.Columns("Form Type").IsVisible = False
                Gv1.Columns("Form Type").Width = 70
                Gv1.Columns("Form Type").HeaderText = "Form Type"
                Gv1.Columns("Form Type").VisibleInColumnChooser = True
            ElseIf ddlReportType.SelectedValue = "Document Wise" Then

                For i As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(i).BestFit()
                    Gv1.Columns(i).IsVisible = True

                    If Gv1.Columns(i).Name.Contains("Code") = True And Not (Gv1.Columns(i).Name.Contains("Item Code") = False Or Gv1.Columns(i).Name.Contains("Customer Code") = False Or Gv1.Columns(i).Name.Contains("Location Code") = False) Then
                        Gv1.Columns(i).IsVisible = False
                    Else
                        Gv1.Columns(i).IsVisible = True
                    End If
                    If Gv1.Columns(i).Name.Contains("AR") = True Then
                        Gv1.Columns(i).IsVisible = False
                    End If
                Next
                ''richa 11 Sep,2018
            ElseIf ddlReportType.SelectedValue = "Net Sale" Then


                Gv1.Columns("Trans Type").IsVisible = True
                Gv1.Columns("Trans Type").Width = 70
                Gv1.Columns("Trans Type").HeaderText = "Trans Type"

                For i As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(i).BestFit()
                    Gv1.Columns(i).IsVisible = True
                Next

                Gv1.Columns("Scheme Type").IsVisible = False
            ElseIf ddlReportType.SelectedValue = "Total Sale" Then

                Gv1.Columns("Total FAT KG").IsVisible = True
                Gv1.Columns("Total SNF KG").IsVisible = True
                Gv1.Columns("Total Amount").IsVisible = True

                For i As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(i).BestFit()
                    Gv1.Columns(i).IsVisible = True

                    If Gv1.Columns(i).Name.Contains("Code") = True And Not (Gv1.Columns(i).Name.Contains("Item Code") = False Or Gv1.Columns(i).Name.Contains("Customer Code") = False Or Gv1.Columns(i).Name.Contains("Location Code") = False) Then
                        Gv1.Columns(i).IsVisible = False
                    End If
                Next
            ElseIf ddlReportType.SelectedValue = "Location Wise" Then

                Gv1.Columns("Total FAT KG").IsVisible = True
                Gv1.Columns("Total SNF KG").IsVisible = True
                Gv1.Columns("Total Amount").IsVisible = True
                Gv1.Columns("Location Code").IsVisible = True
                Gv1.Columns("Location Name").IsVisible = True

                For i As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(i).BestFit()
                    Gv1.Columns(i).IsVisible = True

                    If Gv1.Columns(i).Name.Contains("Code") = True And Not (Gv1.Columns(i).Name.Contains("Item Code") = False Or Gv1.Columns(i).Name.Contains("Customer Code") = False Or Gv1.Columns(i).Name.Contains("Location Code") = False) Then
                        Gv1.Columns(i).IsVisible = False
                    End If
                Next
            ElseIf ddlReportType.SelectedValue = "Item Group Wise" Then

                Gv1.Columns("Total FAT KG").IsVisible = True
                Gv1.Columns("Total SNF KG").IsVisible = True
                Gv1.Columns("Total Amount").IsVisible = True
                Gv1.Columns("Location Code").IsVisible = True
                Gv1.Columns("Location Name").IsVisible = True

                Gv1.Columns("Item Group Code").IsVisible = True
                Gv1.Columns("Item Group Description").IsVisible = True

                For i As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(i).BestFit()
                    Gv1.Columns(i).IsVisible = True

                    If Gv1.Columns(i).Name.Contains("Code") = True And Not (Gv1.Columns(i).Name.Contains("Item Code") = False Or Gv1.Columns(i).Name.Contains("Customer Code") = False Or Gv1.Columns(i).Name.Contains("Location Code") = False) Then
                        Gv1.Columns(i).IsVisible = False
                    End If
                Next
            ElseIf ddlReportType.SelectedValue = "Customer Group Wise" Then

                Gv1.Columns("Total FAT KG").IsVisible = True
                Gv1.Columns("Total SNF KG").IsVisible = True
                Gv1.Columns("Total Amount").IsVisible = True
                Gv1.Columns("Location Code").IsVisible = True
                Gv1.Columns("Location Name").IsVisible = True

                Gv1.Columns("Item Group Code").IsVisible = True
                Gv1.Columns("Item Group Description").IsVisible = True

                Gv1.Columns("Customer Group Code").IsVisible = True
                Gv1.Columns("Customer Group Description").IsVisible = True

                For i As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(i).BestFit()
                    Gv1.Columns(i).IsVisible = True

                    If Gv1.Columns(i).Name.Contains("Code") = True And Not (Gv1.Columns(i).Name.Contains("Item Code") = False Or Gv1.Columns(i).Name.Contains("Customer Code") = False Or Gv1.Columns(i).Name.Contains("Location Code") = False) Then
                        Gv1.Columns(i).IsVisible = False
                    End If
                Next
            ElseIf ddlReportType.SelectedValue = "Item Wise" Then

                Gv1.Columns("Total FAT KG").IsVisible = True
                Gv1.Columns("Total SNF KG").IsVisible = True
                Gv1.Columns("Total Amount").IsVisible = True
                Gv1.Columns("Location Code").IsVisible = True
                Gv1.Columns("Location Name").IsVisible = True

                Gv1.Columns("Item Group Code").IsVisible = True
                Gv1.Columns("Item Group Description").IsVisible = True

                Gv1.Columns("Customer Group Code").IsVisible = True
                Gv1.Columns("Customer Group Description").IsVisible = True

                Gv1.Columns("Item Code").IsVisible = True
                Gv1.Columns("Item Name").IsVisible = True

                ''KUNAL
                Gv1.Columns("Quantity").IsVisible = True
                Gv1.Columns("UOM").IsVisible = True

                For i As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(i).BestFit()
                    Gv1.Columns(i).IsVisible = True

                    If Gv1.Columns(i).Name.Contains("Code") = True And Not (Gv1.Columns(i).Name.Contains("Item Code") = False Or Gv1.Columns(i).Name.Contains("Customer Code") = False Or Gv1.Columns(i).Name.Contains("Location Code") = False) Then
                        Gv1.Columns(i).IsVisible = False
                    End If
                Next
            ElseIf ddlReportType.SelectedValue = "Customer Wise" Then

                Gv1.Columns("Total FAT KG").IsVisible = True
                Gv1.Columns("Total SNF KG").IsVisible = True
                Gv1.Columns("Total Amount").IsVisible = True
                Gv1.Columns("Location Code").IsVisible = True
                Gv1.Columns("Location Name").IsVisible = True

                Gv1.Columns("Customer Group Code").IsVisible = True
                Gv1.Columns("Customer Group Description").IsVisible = True

                'richa ERO/22/05/18-000322
                Gv1.Columns("Customer Zone Code").IsVisible = True

                Gv1.Columns("Item Code").IsVisible = True
                Gv1.Columns("Item Name").IsVisible = True

                Gv1.Columns("Customer Code").IsVisible = True
                Gv1.Columns("Customer Name").IsVisible = True

                For i As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(i).BestFit()
                    Gv1.Columns(i).IsVisible = True

                    If Gv1.Columns(i).Name.Contains("Code") = True And Not (Gv1.Columns(i).Name.Contains("Item Code") = False Or Gv1.Columns(i).Name.Contains("Customer Code") = False Or Gv1.Columns(i).Name.Contains("Location Code") = False) Then
                        Gv1.Columns(i).IsVisible = False
                    End If
                Next
            End If

            If chk_amtinlacs.Checked Then
                If ddlReportType.SelectedValue = "Total Sale" OrElse ddlReportType.SelectedValue = "Document Wise" OrElse ddlReportType.SelectedValue = "Location Wise" OrElse ddlReportType.SelectedValue = "Item Group Wise" OrElse ddlReportType.SelectedValue = "Customer Group Wise" OrElse ddlReportType.SelectedValue = "Item Wise" OrElse ddlReportType.SelectedValue = "Customer Wise" Then
                    Gv1.Columns("Total Amount").HeaderText = "Total Amount (in lacs)"
                    For i As Integer = 0 To Gv1.Rows.Count - 1
                        Gv1.Rows(i).Cells("Total Amount").Value = (clsCommon.myCdbl(Gv1.Rows(i).Cells("Total Amount").Value) / 100000)
                    Next
                End If
            End If
            ''RICHA AGARWAL ADD TSKG COLUMN AGAINST TICKET NO ERO/05/06/19-000635 10 jUNE,2019
            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim intCount As Integer = 0
            For Each col As GridViewColumn In Gv1.Columns
                If clsCommon.CompairString(col.Name, "Total FAT KG") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "Total SNF KG") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "FAT KG") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "SNF KG") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "Quantity") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "TSKG") = CompairStringResult.Equal Then
                    Dim item1 As New GridViewSummaryItem(col.Name, "{0:F3}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)

                ElseIf col.Name.Contains("Amount") = True Or col.Name.Contains("Amt") = True Or col.Name.Contains("Total") = True Or strPivotForFinalOuterQuery.Contains(col.Name) = True Or col.Name.Contains("COGS") = True Then
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
        EnableDisableControl(False)
        Gv1.AllowAddNewRow = False
        Gv1.ShowGroupPanel = True
        Gv1.BestFitColumns()
    End Sub
    Sub Reset()
        LOCATIONRIGTHS()
        chk_stockingunit.Checked = False
        chk_amtinlacs.Checked = False
        Document_No = ""
        Document_No_Old = ""
        ToDate.Value = clsCommon.GETSERVERDATE()
        'KUNAL > TICKET : BM00000009568 > DATE : 19-OCT-2016
        fromDate.Value = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
        txtUOM.Value = ""
        LoadTypes()
        LoadSubCategory()
        ddlReportType.SelectedValue = "Total Sale"
        lblSubCategory.Visible = False
        ddlSubCategory.Visible = False
        ddlSubCategory.SelectedValue = "All"
        LoadCategory()
        txtState.arrValueMember = Nothing
        txtLocation.arrValueMember = Nothing
        txtItemGroup.arrValueMember = Nothing
        txtItem.arrValueMember = Nothing
        txtCustomer.arrValueMember = Nothing
        txtCustGroup.arrValueMember = Nothing
        rbtnCategoryAll.IsChecked = True
        If clsCommon.CompairString(clsUserMgtCode.RptFreshSaleRegister1, FORMTYPE) = CompairStringResult.Equal Then
            txtTransaction.Enabled = False
        Else
            txtTransaction.arrValueMember = Nothing
            txtTransaction.Enabled = True
        End If

        ddlReportType.SelectedIndex = 0
        btnPosted.IsChecked = True
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        RadPageViewPage2.Text = ddlReportType.SelectedValue
        EnableDisableControl(True)
    End Sub

    Sub EnableDisableControl(ByVal val As Boolean)
        Panel1.Enabled = val
        RadGroupBox3.Enabled = val
        RadGroupBox7.Enabled = val
    End Sub
    Enum Exporter
        Excel = 0
        PDF = 1
        Print = 2
        Refresh = 3
    End Enum
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
            Dim obj As New clsGridLayout()
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            obj = New clsGridLayout()
            obj.ReportID = PageSetupReport_ID 'clsERPFuncationality.GetReportID(MyBase.Form_ID, ddlReportType)
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = Gv1.ColumnCount
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
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        End If
    End Sub

    'Private Sub rbtnCustomerAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs)
    '    cbgCustomer.Enabled = rbtnCustomerSelect.IsChecked
    'End Sub

    'Private Sub rbtnItemAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs)
    '    cbgItem.Enabled = rbtnItemSelect.IsChecked
    'End Sub

    'Private Sub rbtnLocationAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs)
    '    cbgLocation.Enabled = rbtnLocationSelect.IsChecked
    'End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = clsERPFuncationality.GetReportID(MyBase.Form_ID, ddlReportType.Text)
        TemplateGridview = Gv1
        Print(Exporter.Refresh)
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        EnableDisableControl(True)
        RadPageView1.SelectedPage = RadPageViewPage1
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
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
        'Try
        '    Dim repotype As String = ""
        '    Dim invtype As String = ""
        '    If Gv1.Rows.Count <= 0 Then
        '        clsCommon.MyMessageBoxShow("No Data Found To Send Mail", Me.Text)
        '        Return
        '    End If

        '    Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(clsUserMgtCode.RptFreshSaleRegister1)

        '    If obj Is Nothing Then
        '        clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
        '        Return
        '    End If
        '    If clsCommon.myLen(obj.mailsubjct) <= 0 Then
        '        clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
        '        Return
        '    End If

        '    Try

        '        Dim strEmail As String = ""


        '        If Process.GetProcessesByName("OutLook").Length < 1 Then
        '            'restarts the Process
        '            Process.Start("OutLook.exe")
        '        End If
        '        Dim oApp As New Outlook.Application()
        '        Dim oMsg As Outlook.MailItem

        '        'If chkAll.IsChecked Then
        '        '    invtype = ""
        '        'ElseIf chkTax.IsChecked Then
        '        '    invtype = "Tax Invoice"
        '        'ElseIf chkRetail.IsChecked Then
        '        '    invtype = "Retail Invoice"
        '        'End If

        '        If rdbDetail.IsChecked Then
        '            repotype = "Detail Report"
        '        Else
        '            repotype = "Summary Report"
        '        End If

        '        oMsg = DirectCast(oApp.CreateItem(Outlook.OlItemType.olMailItem), Outlook.MailItem)
        '        strEmail = clsDBFuncationality.getSingleValue("select distinct (select ','+Email_id from tspl_employee_master where emp_code in ('" & obj.usercode & "') for xml path('')) ")

        '        Try
        '            If strEmail.Substring(0, 1) = "," Then
        '                strEmail = strEmail.Substring(1, strEmail.Length - 1)
        '            End If
        '        Catch ex As Exception
        '        End Try

        '        If clsCommon.myLen(strEmail) <= 0 Then
        '            clsCommon.MyMessageBoxShow("No Mail ID Found for Sending Mail,Please Fill E-Mail Id In Employee Master", Me.Text)
        '            Return
        '        End If

        '        oMsg.Body = obj.mailbody

        '        oMsg.Body = oMsg.Body.Replace("'", " ").Replace("`", "/")

        '        If oMsg.Body.Contains(clsEmailSMSConstants.FromDate) Then
        '            oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.FromDate, clsCommon.GetPrintDate(fromDate.Text, "dd/MMM/yyyy"))
        '        End If
        '        If oMsg.Body.Contains(clsEmailSMSConstants.ToDate) Then
        '            oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.ToDate, clsCommon.GetPrintDate(ToDate.Text, "dd/MMM/yyyy"))
        '        End If
        '        If oMsg.Body.Contains(clsEmailSMSConstants.ReportType) Then
        '            oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.ReportType, repotype)
        '        End If
        '        If oMsg.Body.Contains(clsEmailSMSConstants.InvoiceType) Then
        '            oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.InvoiceType, invtype)
        '        End If


        '        oMsg.Subject = obj.mailsubjct

        '        oMsg.Subject = oMsg.Subject.Replace("'", " ").Replace("`", "/")

        '        If oMsg.Subject.Contains(clsEmailSMSConstants.FromDate) Then
        '            oMsg.Subject = oMsg.Subject.Replace(clsEmailSMSConstants.FromDate, clsCommon.GetPrintDate(fromDate.Text, "dd/MMM/yyyy"))
        '        End If
        '        If oMsg.Subject.Contains(clsEmailSMSConstants.ToDate) Then
        '            oMsg.Subject = oMsg.Subject.Replace(clsEmailSMSConstants.ToDate, clsCommon.GetPrintDate(ToDate.Text, "dd/MMM/yyyy"))
        '        End If
        '        If oMsg.Subject.Contains(clsEmailSMSConstants.ReportType) Then
        '            oMsg.Subject = oMsg.Subject.Replace(clsEmailSMSConstants.ReportType, repotype)
        '        End If
        '        If oMsg.Subject.Contains(clsEmailSMSConstants.InvoiceType) Then
        '            oMsg.Subject = oMsg.Subject.Replace(clsEmailSMSConstants.InvoiceType, invtype)
        '        End If

        '        '------------------------code for attchament-------------------------------------
        '        If obj.atchmnt = "Y" Then
        '            Dim sDisplayname As [String] = "MyAttachment"
        '            If oMsg.Body Is Nothing Then
        '                oMsg.Body = " "
        '            End If
        '            Dim iPosition As Integer = CInt(oMsg.Body.Length) + 1
        '            Dim iAtchmentType As Integer = CInt(Outlook.OlAttachmentType.olByValue)

        '            Dim strRptPath As String = ""

        '            Dim oAttachment As Outlook.Attachment = Nothing
        '            Dim dt As DataTable = clsDBFuncationality.GetDataTable(atchqry)

        '            If dt.Rows.Count > 0 Then
        '                Dim subPath As String = Application.StartupPath + "\Mail Reports"

        '                Dim IsExists As Boolean = System.IO.Directory.Exists(subPath)

        '                If (IsExists = False) Then

        '                    System.IO.Directory.CreateDirectory(subPath)
        '                End If
        '                strRptPath = Application.StartupPath + "\Mail Reports\Sale Register.xls"
        '                transportSql.exportdata(Gv1, strRptPath, "Sheet1")
        '                oAttachment = oMsg.Attachments.Add(strRptPath, iAtchmentType, iPosition, sDisplayname)
        '            End If
        '        End If
        '        '---------------------------------------------------------------------------


        '        oMsg.Recipients.Add(strEmail)
        '        oMsg.CC = "ranjana.sinha@tecxpert.in;rakesh.sharma@tecxpert.in"
        '        oMsg.Send()
        '        oMsg = Nothing
        '        oApp = Nothing

        '        clsCommon.MyMessageBoxShow("E-Mail Send Successfully", Me.Text)
        '    Catch ex As Exception
        '        Throw New Exception(ex.Message)
        '    End Try

        '    Try
        '        Dim client As New System.Net.WebClient()

        '        If clsCommon.myLen(obj.smsbody) <= 0 Then
        '            Throw New Exception("Please Set First SMS Body In SMS/Email Setting")
        '        End If

        '        Dim strMes As String = ""

        '        strMes = obj.smsbody
        '        strMes = strMes.Replace("'", " ").Replace("`", "/")

        '        If strMes.Contains(clsEmailSMSConstants.FromDate) Then
        '            strMes = strMes.Replace(clsEmailSMSConstants.FromDate, clsCommon.GetPrintDate(fromDate.Text, "dd/MMM/yyyy"))
        '        End If
        '        If strMes.Contains(clsEmailSMSConstants.ToDate) Then
        '            strMes = strMes.Replace(clsEmailSMSConstants.ToDate, clsCommon.GetPrintDate(ToDate.Text, "dd/MMM/yyyy"))
        '        End If
        '        If strMes.Contains(clsEmailSMSConstants.ReportType) Then
        '            strMes = strMes.Replace(clsEmailSMSConstants.ReportType, repotype)
        '        End If
        '        If strMes.Contains(clsEmailSMSConstants.InvoiceType) Then
        '            strMes = strMes.Replace(clsEmailSMSConstants.InvoiceType, invtype)
        '        End If


        '        Dim strphone As String = clsDBFuncationality.getSingleValue("select distinct (select ','+Phone from tspl_employee_master where emp_code in ('" & obj.usercode & "') for xml path(''))  ")

        '        Try
        '            If strphone.Substring(0, 1) = "," Then
        '                strphone = strphone.Substring(1, strphone.Length - 1)
        '            End If
        '        Catch ex As Exception
        '        End Try

        '        'Dim baseurl As String = "http://bulksms.mysmsmantra.com:8080/WebSMS/SMSAPI.jsp?username=tecxpert&password=1818948263&sendername=vipin&mobileno=91" + strphone + "&message=" + strMes
        '        'Dim data As Stream = client.OpenRead(baseurl)
        '        'Dim reader As StreamReader = New StreamReader(data)
        '        'Dim s As String = reader.ReadToEnd()
        '        'data.Close()
        '        'reader.Close()


        '        Dim UserId As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.SMS_User_Name, clsFixedParameterCode.MilkSetting, Nothing))
        '        Dim Paswd As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.SMS_User_PWD, clsFixedParameterCode.MilkSetting, Nothing))
        '        Dim SenderId As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.SMS_Sendor_ID, clsFixedParameterCode.MilkSetting, Nothing))
        '        Dim SMS_Provider As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.SMS_Provider, clsFixedParameterCode.MilkSetting, Nothing))

        '        If clsCommon.CompairString(SMS_Provider, "Bulk SMS") = CompairStringResult.Equal Then
        '            '================send sms through PerfectBulkSMS====================
        '            Dim encode As System.Text.Encoding = System.Text.Encoding.GetEncoding("utf-8")
        '            Dim str As String = "http://www.perfectbulksms.in/Sendsmsapi.aspx?USERID=" + UserId + "&PASSWORD=" + Paswd + "&SENDERID=" + SenderId + "&TO=" & strphone & "&MESSAGE=" & strMes & ""
        '            Dim wrquest As HttpWebRequest = WebRequest.Create(str)
        '            Dim getresponse As HttpWebResponse = Nothing
        '            getresponse = wrquest.GetResponse()

        '            Dim objStream As Stream = getresponse.GetResponseStream()
        '            Dim objSR As StreamReader = New StreamReader(objStream, encode, True)
        '            Dim strResponse As String = objSR.ReadToEnd()
        '            'clsCommon.MyMessageBoxShow(getresponse.StatusDescription)

        '            objSR.Close()
        '            objStream.Close()
        '            getresponse.Close()
        '            '===========================================================
        '        ElseIf clsCommon.CompairString(SMS_Provider, "BSWS") = CompairStringResult.Equal Then
        '            Dim consumeWebService As BSWS.BSWS
        '            consumeWebService = New BSWS.BSWS
        '            Dim xmlResult As XmlElement
        '            xmlResult = consumeWebService.SubmitSMS("prashant@tecxpert.in", "tecxpert", strphone, strMes, "", 0, "TSPLSW", "")
        '        End If

        '        clsCommon.MyMessageBoxShow("SMS Send Successfully", Me.Text)
        '    Catch ex As Exception
        '        Throw New Exception(ex.Message)
        '    End Try
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(ex.Message)
        'End Try

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    'Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
    '    If (Gv1.Rows.Count <= 0) Then
    '        common.clsCommon.MyMessageBoxShow("No Data To Export")
    '        Exit Sub
    '    End If
    '    Print(Exporter.PDF)
    'End Sub

    Private Sub RptSaleRegisterReport_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.R Then
            Print(Exporter.Refresh)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            EnableDisableControl(True)
        End If
    End Sub

    Private Sub RptSaleRegisterReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Visible = False
        arrBack = New List(Of String)
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New ")
        GetMIS_ITem_GroupColumn()
        'If clsCommon.myLen(MIS_Item_Group) <= 0 Then
        '    clsCommon.MyMessageBoxShow("MIS Item Group Custom field is not create in Item Structure.")
        'End If
        AmountinLacs = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AmountInLacsOnMisSaleRegister, clsFixedParameterCode.AmountInLacsOnMisSaleRegister, Nothing)) = "1", True, False))
        If AmountinLacs Then
            chk_amtinlacs.Visible = True
        Else
            chk_amtinlacs.Visible = False
        End If

        Reset()
        RadPageView1.SelectedPage = RadPageViewPage1
        Gv1.EnableGrouping = True
        Gv1.ShowGroupPanel = True
        If isDataLoad Then
            fromDate.Value = dtFrom
            ToDate.Value = dtTo
            txtUOM.Value = Unit_Code
            'cbgItem.CheckedValue = arrItem
            'cbgType.CheckedValue = arrTransaction
            'cbgLocation.CheckedValue = arrLocation
            'cbgCustomer.CheckedValue = arrCustomer

            'If arrLoc IsNot Nothing AndAlso arrLoc.Count > 0 Then
            '    rbtnLocationSelect.IsChecked = True
            '    For Each str As String In arrLoc.Keys
            '        For ii As Integer = 0 To gvLocation.RowCount - 1
            '            If clsCommon.CompairString(clsCommon.myCstr(gvLocation.Rows(ii).Cells("CODE").Value), str) = CompairStringResult.Equal Then
            '                gvLocation.Rows(ii).Cells("SEL").Value = True
            '                gvLocation.Rows(ii).Tag = arrLoc(str)
            '            End If
            '        Next
            '    Next
            'End If
            txtLocation.arrValueMember = arrLocation
            txtItem.arrValueMember = arrItem
            txtCustomer.arrValueMember = arrCustomer
            txtTransaction.arrValueMember = arrTransaction
            txtItemGroup.arrValueMember = arrItemGroup
            txtState.arrValueMember = arrState
            txtCustGroup.arrValueMember = arrCustGroup

            If arrCat IsNot Nothing AndAlso arrCat.Count > 0 Then
                rbtnCategorySelect.IsChecked = True
                For Each str As String In arrCat.Keys
                    For ii As Integer = 0 To gvCategory.RowCount - 1
                        If clsCommon.CompairString(clsCommon.myCstr(gvCategory.Rows(ii).Cells("CODE").Value), str) = CompairStringResult.Equal Then
                            gvCategory.Rows(ii).Cells("SEL").Value = True
                            gvCategory.Rows(ii).Tag = arrCat(str)
                        End If
                    Next
                Next
            End If
            ddlReportType.SelectedValue = strType
            Print(True)
            Me.Visible = True
        End If
        ddlReportType.DropDownStyle = RadDropDownStyle.DropDownList
        If clsCommon.CompairString(clsUserMgtCode.RptFreshSaleRegister1, FORMTYPE) = CompairStringResult.Equal Then
            Dim arr As New ArrayList
            arr.Add("Fresh Sale")
            arr.Add("Fresh Sale Return")
            txtTransaction.arrValueMember = arr
            txtTransaction.Enabled = False
        ElseIf clsCommon.CompairString(clsUserMgtCode.MISSaleRegisterWithCSASalePatti, FORMTYPE) = CompairStringResult.Equal Then
            '' changed by panch raj on 02-05-18 against ticket No: UDL/27/04/18-000143
            Me.chkShowCSASaleFromSalePatti.Checked = True
            Me.chkShowCSASaleFromSalePatti.Enabled = False
            Me.chkIncludeDebitCredit.Visible = False
            Me.chkFarmerSale.Visible = False
            Me.chkSerializeInv.Visible = False
        ElseIf clsCommon.CompairString(clsUserMgtCode.RptProductSaleRegister1, FORMTYPE) = CompairStringResult.Equal Then
            '' changed by panch raj on 08-05-18 against ticket No: KDI/04/05/18-000295
            Dim arr As New ArrayList
            arr.Add("Product Sale")
            arr.Add("Product Sale Return")
            txtTransaction.arrValueMember = arr
            txtTransaction.Enabled = False
        ElseIf clsCommon.CompairString(clsUserMgtCode.RptBulkSaleRegister, FORMTYPE) = CompairStringResult.Equal Then
            '' changed by panch raj on 08-05-18 against ticket No: KDI/04/05/18-000295
            Dim arr As New ArrayList
            arr.Add("Bulk Sale")
            arr.Add("Bulk Sale Trade")
            arr.Add("Bulk Sale Return")
            txtTransaction.arrValueMember = arr
            txtTransaction.Enabled = False
        ElseIf clsCommon.CompairString(clsUserMgtCode.RptMccSaleRegister, FORMTYPE) = CompairStringResult.Equal Then
            '' changed by panch raj on 08-05-18 against ticket No: KDI/04/05/18-000295
            Dim arr As New ArrayList
            arr.Add("MCC Sale")
            arr.Add("MCC Sale Return")
            txtTransaction.arrValueMember = arr
            txtTransaction.Enabled = False
        ElseIf clsCommon.CompairString(clsUserMgtCode.RptCSASaleRegister, FORMTYPE) = CompairStringResult.Equal Then
            '' changed by panch raj on 08-05-18 against ticket No: KDI/04/05/18-000295
            Dim arr As New ArrayList
            arr.Add("CSA Sale")
            arr.Add("CSA Sale Return")
            txtTransaction.arrValueMember = arr
            txtTransaction.Enabled = False
        ElseIf clsCommon.CompairString(clsUserMgtCode.RptExportSaleRegister, FORMTYPE) = CompairStringResult.Equal Then
            '' changed by panch raj on 08-05-18 against ticket No: KDI/04/05/18-000295
            Dim arr As New ArrayList
            arr.Add("Export Sale")
            arr.Add("Export Sale Return")
            txtTransaction.arrValueMember = arr
            txtTransaction.Enabled = False
        ElseIf clsCommon.CompairString(clsUserMgtCode.frmItemWiseDispatchLedger3, FORMTYPE) = CompairStringResult.Equal Then
            '' changed by panch raj on 08-05-18 against ticket No: KDI/04/05/18-000295
            Dim arr As New ArrayList
            arr.Add("Misc Sale")
            arr.Add("Misc Sale Return")
            txtTransaction.arrValueMember = arr
            txtTransaction.Enabled = False
        End If
        IsFormLoad = True
    End Sub

    Private Sub chkAllType_ToggleStateChanged(sender As Object, args As StateChangedEventArgs)
        'cbgType.Enabled = chkselecttype.IsChecked
    End Sub

    Private Sub rbtnCategoryAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnCategoryAll.ToggleStateChanged
        gvCategory.Enabled = rbtnCategorySelect.IsChecked
    End Sub

    Private Sub chkAllProductType_ToggleStateChanged(sender As Object, args As StateChangedEventArgs)
        'cbgProductType.Enabled = chkSelectProductType.IsChecked
    End Sub

    Private Sub Gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles Gv1.CellDoubleClick
        DrillDown()
    End Sub
    Sub DrillDown()
        Try
            If clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Total Sale") = CompairStringResult.Equal Then
                If Not arrBack.Contains("Total Sale") Then
                    arrBack.Add("Total Sale")
                End If
                ddlReportType.SelectedValue = "Location Wise"
                'arrLocation = New ArrayList()
                'arrLocation = txtLocation.arrValueMember
                'Dim tmp As New ArrayList()
                'tmp.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Item_Group").Value))
                'txtItemGroup.arrValueMember = tmp
                Print(Exporter.Refresh)

                'Dim frm As New RptSaleRegisterReport
                'frm.Text = Me.Text
                'frm.isDataLoad = True
                'frm.dtFrom = fromDate.Value
                'frm.dtTo = ToDate.Value
                'frm.Unit_Code = txtUOM.Value

                'frm.arrTransaction = txtTransaction.arrValueMember
                'frm.arrItem = txtItem.arrValueMember
                'frm.arrLocation = txtLocation.arrValueMember
                'frm.arrCustomer = txtCustomer.arrValueMember
                'frm.arrItemGroup = txtItemGroup.arrValueMember

                'frm.strType = "Location Wise"
                'frm.WindowState = FormWindowState.Maximized
                'frm.Focus()
                ''frm.Visible = False

                'frm.Show()
            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Location Wise") = CompairStringResult.Equal Then
                If Not arrBack.Contains("Location Wise") Then
                    arrBack.Add("Location Wise")
                End If
                ddlReportType.SelectedValue = "Item Group Wise"
                arrLocation = New ArrayList()
                arrLocation = txtLocation.arrValueMember
                Dim tmp As New ArrayList()
                tmp.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Location Code").Value))
                txtLocation.arrValueMember = tmp
                Print(Exporter.Refresh)


                'Dim frm As New RptSaleRegisterReport
                'frm.Text = Me.Text
                'frm.isDataLoad = True
                'frm.dtFrom = fromDate.Value
                'frm.dtTo = ToDate.Value
                'frm.Unit_Code = txtUOM.Value

                'frm.arrTransaction = txtTransaction.arrValueMember
                'frm.arrLocation = New ArrayList
                'frm.arrLocation.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Location Code").Value))
                'frm.arrItem = txtItem.arrValueMember
                'frm.arrItemGroup = txtItemGroup.arrValueMember

                '' '' new filter
                ''arrTrans(2) = txtTransaction.arrValueMember
                ''arrItm(2) = txtItem.arrValueMember
                ''arrLoc(2) = arrLoc(1).Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Location Code").Value))
                ''arrCust(2) = txtCustomer.arrValueMember
                ''arrCustGrp(2) = Nothing
                ''arrItemGrp(2) = txtItemGroup.arrValueMember
                '' '' end new filter 
                'frm.strType = "Item Group Wise"
                'frm.WindowState = FormWindowState.Maximized
                'frm.Focus()
                ''frm.Visible = False
                'frm.Show()
            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Item Group Wise") = CompairStringResult.Equal Then
                If Not arrBack.Contains("Item Group Wise") Then
                    arrBack.Add("Item Group Wise")
                End If
                ddlReportType.SelectedValue = "Customer Group Wise"
                arrItemGroup = New ArrayList()
                arrItemGroup = txtItemGroup.arrValueMember
                Dim tmp As New ArrayList()
                tmp.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Item Group Code").Value))
                txtItemGroup.arrValueMember = tmp
                Print(Exporter.Refresh)

                'Dim frm As New RptSaleRegisterReport
                'frm.Text = Me.Text
                'frm.isDataLoad = True
                'frm.dtFrom = fromDate.Value
                'frm.dtTo = ToDate.Value
                'frm.Unit_Code = txtUOM.Value
                ''frm.MRP_Wise = ChkMRPWise.Checked
                ''frm.ShowFatAndSNF = chkFATAndSNF.Checked
                'frm.arrTransaction = txtTransaction.arrValueMember
                'frm.arrLocation = New ArrayList
                'frm.arrLocation.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Location Code").Value))

                'frm.arrItemGroup = New ArrayList
                'frm.arrItemGroup.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Item Group Code").Value))

                ''frm.arrCustomer = New ArrayList
                ''frm.arrCustomer.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Customer Code").Value))

                'frm.strType = "Customer Group Wise"
                'frm.WindowState = FormWindowState.Maximized
                'frm.Focus()
                ''frm.Visible = False
                'frm.Show()
            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Customer Group Wise") = CompairStringResult.Equal Then
                If Not arrBack.Contains("Customer Group Wise") Then
                    arrBack.Add("Customer Group Wise")
                End If
                ddlReportType.SelectedValue = "Item Wise"
                arrCustGroup = New ArrayList()
                arrCustGroup = txtCustGroup.arrValueMember
                Dim tmp As New ArrayList()
                tmp.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Customer Group Code").Value))
                txtCustGroup.arrValueMember = tmp
                Print(Exporter.Refresh)

                'Dim frm As New RptSaleRegisterReport
                'frm.Text = Me.Text
                'frm.isDataLoad = True
                'frm.dtFrom = fromDate.Value
                'frm.dtTo = ToDate.Value
                'frm.Unit_Code = txtUOM.Value
                ''frm.MRP_Wise = ChkMRPWise.Checked
                ''frm.ShowFatAndSNF = chkFATAndSNF.Checked
                'frm.arrTransaction = txtTransaction.arrValueMember
                'frm.arrLocation = New ArrayList
                'frm.arrLocation.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Location Code").Value))

                'frm.arrItemGroup = New ArrayList
                'frm.arrItemGroup.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Item Group Code").Value))

                'frm.arrCustGroup = New ArrayList
                'frm.arrCustGroup.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Customer Group Code").Value))

                ''frm.arrCustomer = New ArrayList
                ''frm.arrCustomer.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Customer Code").Value))

                'frm.strType = "Item Wise"
                'frm.WindowState = FormWindowState.Maximized
                'frm.Focus()
                ''frm.Visible = False
                'frm.Show()
            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Item Wise") = CompairStringResult.Equal Then
                If Not arrBack.Contains("Item Wise") Then
                    arrBack.Add("Item Wise")
                End If
                ddlReportType.SelectedValue = "Customer Wise"
                arrItem = New ArrayList()
                arrItem = txtItem.arrValueMember
                Dim tmp As New ArrayList()
                tmp.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Item Code").Value))
                txtItem.arrValueMember = tmp
                Print(Exporter.Refresh)

                'Dim frm As New RptSaleRegisterReport
                'frm.Text = Me.Text
                'frm.isDataLoad = True
                'frm.dtFrom = fromDate.Value
                'frm.dtTo = ToDate.Value
                'frm.Unit_Code = txtUOM.Value
                ''frm.MRP_Wise = ChkMRPWise.Checked
                ''frm.ShowFatAndSNF = chkFATAndSNF.Checked
                'frm.arrTransaction = txtTransaction.arrValueMember
                'frm.arrLocation = New ArrayList
                'frm.arrLocation.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Location Code").Value))

                'frm.arrItemGroup = New ArrayList
                'frm.arrItemGroup.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Item Group Code").Value))

                'frm.arrCustGroup = New ArrayList
                'frm.arrCustGroup.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Customer Group Code").Value))

                'frm.arrItem = New ArrayList
                'frm.arrItem.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Item Code").Value))

                ''frm.arrItem = New ArrayList
                ''frm.arrItem.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Item Code").Value))

                'frm.strType = "Customer Wise"
                'frm.WindowState = FormWindowState.Maximized
                'frm.Focus()
                ''frm.Visible = False
                'frm.Show()
            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Customer Wise") = CompairStringResult.Equal Then
                If Not arrBack.Contains("Customer Wise") Then
                    arrBack.Add("Customer Wise")
                End If
                ddlReportType.SelectedValue = "Document Wise"
                arrCustomer = New ArrayList()
                arrCustomer = txtCustomer.arrValueMember
                Dim tmp As New ArrayList()
                tmp.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Customer Code").Value))
                txtCustomer.arrValueMember = tmp
                Print(Exporter.Refresh)

                'Dim frm As New RptSaleRegisterReport
                'frm.Text = Me.Text
                'frm.isDataLoad = True
                'frm.dtFrom = fromDate.Value
                'frm.dtTo = ToDate.Value
                'frm.Unit_Code = txtUOM.Value
                ''frm.MRP_Wise = ChkMRPWise.Checked
                ''frm.ShowFatAndSNF = chkFATAndSNF.Checked
                'frm.arrTransaction = txtTransaction.arrValueMember
                'frm.arrLocation = New ArrayList
                'frm.arrLocation.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Location Code").Value))

                'frm.arrCustGroup = New ArrayList
                'frm.arrCustGroup.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Customer Group Code").Value))

                'frm.arrItem = New ArrayList
                'frm.arrItem.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Item Code").Value))

                'frm.arrCustomer = New ArrayList
                'frm.arrCustomer.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Customer Code").Value))

                'frm.strType = "Document Detail"
                'frm.WindowState = FormWindowState.Maximized
                'frm.Focus()
                ''frm.Visible = False
                'frm.Show()
            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Document Wise") = CompairStringResult.Equal Then
                If Not arrBack.Contains("Document Wise") Then
                    arrBack.Add("Document Wise")
                End If
                ddlReportType.SelectedValue = "Document Detail"
                Document_No_Old = Document_No
                'arrCustomer = New ArrayList()
                'arrCustomer = txtCustomer.arrValueMember
                'Dim tmp As New ArrayList()
                'tmp.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Document No").Value))
                'txtCustomer.arrValueMember = tmp
                Document_No = clsCommon.myCstr(Gv1.CurrentRow.Cells("Document No").Value)
                Print(Exporter.Refresh)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Document Detail") = CompairStringResult.Equal Then
                Dim strTransType As String = clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value)
                Dim strTransCode As String = clsCommon.myCstr(Gv1.CurrentRow.Cells("Document No").Value)
                If clsCommon.myLen(strTransType) > 0 AndAlso clsCommon.myLen(strTransCode) > 0 Then
                    Select Case strTransType
                        Case "Fresh Sale"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmInvoiceFreshSale, strTransCode)
                        Case "Product Sale"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSaleInvoiceProductSale, strTransCode)
                        Case "Export Sale"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmEXSalesInvoice, strTransCode)
                        Case "MCC Sale Farmer"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCMaterialFarmer, strTransCode)
                        Case "MCC Sale Return Farmer"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCMaterialSaleReturnFarmer, strTransCode)
                        Case "MCC Sale"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCMaterial, strTransCode)
                        Case "CSA Sale"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSATransfer, strTransCode)
                        Case "Fresh Sale Return"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.saleReturn, strTransCode)
                        Case "Product Sale Return"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSaleReturnProductSale, strTransCode)
                        Case "Export Sale Return"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmEXSalesReturn, strTransCode)
                        Case "CSA Sale Return"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSATransferReturn, strTransCode)
                        Case "MCC Sale Return"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCMaterialSaleReturn, strTransCode)
                        'Case "Tanker Dispatch Return"
                        '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.MCCDispatchReturn, strTransCode)
                        Case "Bulk Sale"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmInvoiceBulkSale, strTransCode)
                        Case "Bulk Sale Trade"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmInvoiceBulkSale, strTransCode)
                        Case "Bulk Sale Return"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmBulkSaleReturn, strTransCode)
                            'Case "Bulk Sale Return Trade"
                            '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmBulkSaleReturn, strTransCode)
                        Case "Transfer"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.Transfer, strTransCode)
                        Case "Transfer Return"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.TransferReturn, strTransCode)
                        Case "Misc Sale"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ScrapSale, strTransCode)
                        Case "MCC Transfer"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCDispatch, strTransCode)
                        Case "Merchant Sale"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSalesInvoiceMT, strTransCode)
                        Case "Misc Sale Return"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ScrapSaleRetrun, strTransCode)
                            '=========================Added by preeti Gupta against Ticket No[KDI/08/05/18-000305]====================
                        Case "General Sale"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSNServiceInvoice, strTransCode)
                        Case "General Sale Return"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSNSaleReturn, strTransCode)
                            '=======================================================================================
                    End Select

                End If
            End If
            PageSetupReport_ID = clsERPFuncationality.GetReportID(MyBase.Form_ID, ddlReportType.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        Dim qry As String = " select tspl_customer_master.cust_code as [Code], tspl_customer_master.Customer_Name as [Name] from tspl_customer_master 
                              where 1=1 "
        Dim dtCustGroup As DataTable = clsDBFuncationality.GetDataTable("select distinct Cust_Group_Code from TSPL_CUSTOMER_GROUP_MAPPING where User_Code ='" & clsCommon.myCstr(objCommonVar.CurrentUserCode) & "'")

        If dtCustGroup IsNot Nothing AndAlso dtCustGroup.Rows.Count > 0 Then
            qry += " AND tspl_customer_master.cust_code in (select DISTINCT Cust_Code from TSPL_CUSTOMER_GROUP_MAPPING_DETAIL WHERE User_Code ='" & clsCommon.myCstr(objCommonVar.CurrentUserCode) & "' "
            If txtCustGroup.arrValueMember IsNot Nothing AndAlso txtCustGroup.arrValueMember.Count > 0 Then
                qry += " and TSPL_CUSTOMER_GROUP_MAPPING_DETAIL.Cust_Group_Code in (" + clsCommon.GetMulcallString(txtCustGroup.arrValueMember) + ") ) "
            Else
                qry += " ) "
            End If
        End If
        ''richa agarwal 24 June,2019 ERO/24/06/19-000653
        If chkShowOnlyVSP.Checked = True Then
            qry += " AND tspl_customer_master.cust_code in (select Cust_Code  from TSPL_CUSTOMER_VENDOR_MAPPING where Vendor_Code in (select Vendor_Code from TSPL_VENDOR_MASTER where Form_Type ='VSP')) "
        End If
        If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserCustomers) > 0 Then
            qry += " AND tspl_customer_master.cust_code in (" + objCommonVar.strCurrUserCustomers + ")"
        End If
        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("CustMulSel", qry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
        ''richa agarwal 26 June,2019 ERO/24/06/19-000653
        If chkShowOnlyVSP.Checked = True Then
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                Dim arr As ArrayList = Nothing
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select TSPL_VLC_MASTER_HEAD.VLC_Code as Code from TSPL_VLC_MASTER_HEAD where 1=1 and TSPL_VLC_MASTER_HEAD.Active=1 and TSPL_VLC_MASTER_HEAD.VSP_Code in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") ")
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    arr = New ArrayList
                    For Each dr As DataRow In dt.Rows
                        arr.Add(dr("Code"))
                    Next
                End If
                TxtVLC.arrValueMember = arr
            Else
                TxtVLC.arrValueMember = Nothing
            End If
        End If
    End Sub

    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Dim qry As String = " select Item_Code,Item_Desc from TSPL_ITEM_MASTER order by Item_Code "
        txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Item_Code", "Item_Code", txtItem.arrValueMember, txtItem.arrDispalyMember)
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim stateCond As String = ""
        Dim qry As String = ""

        If txtState.arrValueMember IsNot Nothing AndAlso txtState.arrValueMember.Count > 0 Then
            stateCond = " and state in  (" + clsCommon.GetMulcallString(txtState.arrValueMember) + ") "
        End If
        '' change by panch raj against ticket  KDI/12/06/18-000358 on 12-06-2018  , KDI/06/06/18-000341
        '' for  location query : customization of mcc sae register location filter 
        If clsCommon.CompairString(clsUserMgtCode.RptMccSaleRegister, FORMTYPE) = CompairStringResult.Equal Then
            qry = "select TSPL_LOCATION_MASTER.Location_Code as [Code] ,TSPL_LOCATION_MASTER.Location_Desc as [Name] from TSPL_LOCATION_MASTER where isnull(CSA_Type,'N') ='N' and (isnull(GIT_Type,'N')='N' or isnull(GIT_Type,'N')='') and isnull(Is_Consumption_Location,0) =0  and isnull(Rejected_type,'N') ='N'  and Location_Code in (" + arrLoc + ") " & stateCond & " "
        Else
            qry = " select Location_Code as Code,Location_Desc as [Name] from TSPL_LOCATION_MASTER where location_type IN ('Physical','Virtual') " & stateCond & " "
        End If
        If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            qry += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        Dim FrmPendingRequisitionQty As New FrmPendingRequisitionQty()
        FrmPendingRequisitionQty.SetDiplayMember(txtLocation, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    End Sub

    Private Sub txtTransaction__My_Click(sender As Object, e As EventArgs) Handles txtTransaction._My_Click
        ' Dim Dt As DataTable = clsDBFuncationality.GetDataTable("select * from TSPL_MODULE_PERMISSION")
        Dim Str As String = String.Empty

        Dim qry As String = clsPSInvoiceHead.GetAllSaleTransactionTypeQuery()

        If clsCommon.CompairString(clsUserMgtCode.RptFreshSaleRegister1, FORMTYPE) <> CompairStringResult.Equal Then
            txtTransaction.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Name", "Name", txtTransaction.arrValueMember, txtTransaction.arrDispalyMember)
        End If

        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
            '' richa UDL/09/08/18-000213
            'If txtTransaction.arrValueMember Is Nothing OrElse txtTransaction.arrValueMember.Contains("Misc Sale") Then
            If txtTransaction.arrValueMember.Contains("Misc Sale") Then
                lblSubCategory.Visible = True
                ddlSubCategory.Visible = True
                ddlSubCategory.SelectedValue = "All"
            Else
                lblSubCategory.Visible = False
                ddlSubCategory.Visible = False
                ddlSubCategory.SelectedValue = "All"
            End If
        End If

    End Sub
    Sub LoadCategory()
        dtCategory = clsDBFuncationality.GetDataTable("select ITEM_CATEGORY_CODE AS CodeColumn,ITEM_CATEGORY_CODE+' Description' as CodeDescColumn,DESCRIPTION as DescColumn  from TSPL_ITEM_CATEGORY_LEVEL order by CATEGORY_LEVEL")

        gvCategory.DataSource = Nothing
        Dim qry As String = "select cast( 0 as bit) as SEL,ITEM_CATEGORY_CODE AS Code,DESCRIPTION as NAME from TSPL_ITEM_CATEGORY_LEVEL ORDER BY CATEGORY_LEVEL"
        gvCategory.DataSource = clsDBFuncationality.GetDataTable(qry)

        gvCategory.Columns("SEL").ReadOnly = False
        gvCategory.Columns("SEL").Width = 30
        gvCategory.Columns("SEL").HeaderText = " "

        gvCategory.Columns("CODE").ReadOnly = True
        gvCategory.Columns("CODE").Width = 100
        gvCategory.Columns("CODE").HeaderText = "Code"

        gvCategory.Columns("NAME").ReadOnly = True
        gvCategory.Columns("NAME").Width = 200
        gvCategory.Columns("NAME").HeaderText = "Description"

        gvCategory.ShowGroupPanel = False
        gvCategory.AllowAddNewRow = False
        gvCategory.AllowColumnReorder = False
        gvCategory.AllowRowReorder = False
        gvCategory.EnableSorting = False
        gvCategory.ShowFilteringRow = True
        gvCategory.EnableFiltering = True
        gvCategory.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvCategory.MasterTemplate.ShowRowHeaderColumn = True

    End Sub


    'Sub LoadCustomer()
    '    Dim strquery As String = "select cust_code as [Code], Customer_Name as [Name] from tspl_customer_master"
    '    cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(strquery)
    '    cbgCustomer.ValueMember = "Code"
    '    cbgCustomer.DisplayMember = "Name"
    'End Sub

    'Sub LoadType()
    '    'Dim strquery As String = "Select xxx.Code,  xxx.Name From (Select 'FS' As Code,    'Fresh Sale' As Name  Union  Select 'PS' As Code,    'Product sale' As Name Union  Select 'MCC' As Code,    'MCC sale' As Name Union  Select 'Exp' As Code,    'Export sale' As Name Union  Select 'BS' As Code,    'Bulk sale' As Name Union  Select 'SS' As Code,    'Misc Sale' As Name Union  Select 'BS' As Code,    'Bulk sale' As Name Union  Select 'CSA' As Code,    'CSA sale' As Name) xxx"
    '    Dim strquery As String = " Select xxx.Code,  xxx.Name From (" & _
    '                             " Select 'FS' As Code,    'Fresh Sale' As Name Union Select 'FSR' As Code,    'Fresh Sale Return' As Name  " & _
    '                             " Union  Select 'PS' As Code,    'Product sale' As Name Union  Select 'PSR' As Code,    'Product Sale Return' As Name " & _
    '                             " Union  Select 'MCC' As Code,    'MCC Sale' As Name Union  Select 'MCCR' As Code,    'MCC Sale Return' As Name " & _
    '                             " Union  Select 'Exp' As Code,    'Export Sale' As Name Union  Select 'ExpR' As Code,    'Export Sale Return' As Name " & _
    '                             " Union  Select 'BS' As Code,    'Bulk Sale' As Name Union  Select 'BSR' As Code,    'Bulk Sale Return' As Name " & _
    '                             " Union  Select 'SS' As Code,    'Misc Sale' As Name " & _
    '                             " Union  Select 'Transfer' As Code,    'Transfer' As Name " & _
    '                             " Union  Select 'BS' As Code,    'Bulk Sale' As Name Union  Select 'BSR' As Code,    'Bulk Sale Return' As Name " & _
    '                             " Union  Select 'CSA' As Code,    'CSA Sale' As Name Union  Select 'CSAR' As Code,    'CSA Sale Return' As Name " & _
    '                             " ) xxx"
    '    cbgType.DataSource = Nothing
    '    cbgType.DataSource = clsDBFuncationality.GetDataTable(strquery)
    '    cbgType.ValueMember = "Name"
    '    cbgType.DisplayMember = "Code"
    'End Sub
    'Sub LoadLocation()
    '    Dim qry As String = "select Location_Code as Code,Location_Desc as [Name] from TSPL_LOCATION_MASTER  "
    '    cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
    '    cbgLocation.ValueMember = "Code"
    '    cbgLocation.DisplayMember = "Name"
    'End Sub
    'Sub LoadItem()
    '    Dim qry As String = " select item_code as Code ,item_Desc as Name  from TSPL_ITEM_MASTER order by Item_Code "
    '    cbgItem.DataSource = clsDBFuncationality.GetDataTable(qry)
    '    cbgItem.ValueMember = "Code"
    '    cbgItem.DisplayMember = "Name"
    'End Sub

    Private Sub gvCategory_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gvCategory.CellDoubleClick
        If clsCommon.myCBool(gvCategory.CurrentRow.Cells("SEL").Value) Then
            Dim frm As New FrmCategorySelect()
            frm.lvl = 2
            frm.strCode = clsCommon.myCstr(gvCategory.CurrentRow.Cells("CODE").Value)
            frm.arrIn = gvCategory.CurrentRow.Tag
            frm.ShowDialog()
            If Not frm.isCancel Then
                gvCategory.CurrentRow.Tag = frm.arrOut
            End If
        End If
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
        Try
            If clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Total Sale") = CompairStringResult.Equal Then

            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Location Wise") = CompairStringResult.Equal AndAlso arrBack.Contains("Total Sale") Then
                arrBack.Remove("Total Sale")
                ddlReportType.SelectedValue = "Total Sale"
                'txtLocation.arrValueMember = arrLocation
                Print(Exporter.Refresh)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Item Group Wise") = CompairStringResult.Equal AndAlso arrBack.Contains("Location Wise") Then
                arrBack.Remove("Location Wise")
                ddlReportType.SelectedValue = "Location Wise"
                txtLocation.arrValueMember = arrLocation
                Print(Exporter.Refresh)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Customer Group Wise") = CompairStringResult.Equal AndAlso arrBack.Contains("Item Group Wise") Then
                arrBack.Remove("Item Group Wise")
                ddlReportType.SelectedValue = "Item Group Wise"
                txtItemGroup.arrValueMember = arrItemGroup
                Print(Exporter.Refresh)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Item Wise") = CompairStringResult.Equal AndAlso arrBack.Contains("Customer Group Wise") Then
                arrBack.Remove("Customer Group Wise")
                ddlReportType.SelectedValue = "Customer Group Wise"
                txtCustGroup.arrValueMember = arrCustGroup
                Print(Exporter.Refresh)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Customer Wise") = CompairStringResult.Equal AndAlso arrBack.Contains("Item Wise") Then
                arrBack.Remove("Item Wise")
                ddlReportType.SelectedValue = "Item Wise"
                txtItem.arrValueMember = arrItem
                Print(Exporter.Refresh)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Document Wise") = CompairStringResult.Equal AndAlso arrBack.Contains("Customer Wise") Then
                arrBack.Remove("Customer Wise")
                ddlReportType.SelectedValue = "Customer Wise"
                txtCustomer.arrValueMember = arrCustomer
                Print(Exporter.Refresh)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Document Detail") = CompairStringResult.Equal AndAlso arrBack.Contains("Document Wise") Then
                arrBack.Remove("Document Wise")
                ddlReportType.SelectedValue = "Document Wise"
                Document_No = Document_No_Old
                'txtCustomer.arrValueMember = arrCustomer
                Print(Exporter.Refresh)
            End If
            PageSetupReport_ID = clsERPFuncationality.GetReportID(MyBase.Form_ID, ddlReportType.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCustGroup__My_Click(sender As Object, e As EventArgs) Handles txtCustGroup._My_Click
        Dim qry As String = " select Cust_Group_Code as Code,Cust_Group_Desc as Name from TSPL_CUSTOMER_GROUP_MASTER where 1=1 "
        Dim dtCustGroup As DataTable = clsDBFuncationality.GetDataTable("select distinct Cust_Group_Code from TSPL_CUSTOMER_GROUP_MAPPING where User_Code ='" & clsCommon.myCstr(objCommonVar.CurrentUserCode) & "'")

        If dtCustGroup IsNot Nothing AndAlso dtCustGroup.Rows.Count > 0 Then
            qry += " and TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code in (select distinct Cust_Group_Code from TSPL_CUSTOMER_GROUP_MAPPING where User_Code ='" & clsCommon.myCstr(objCommonVar.CurrentUserCode) & "')"
        End If
        txtCustGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("CustGroupMulSel", qry, "Code", "Name", txtCustGroup.arrValueMember, txtCustGroup.arrDispalyMember)
    End Sub

    Private Sub btnQuickExport_Click(sender As Object, e As EventArgs)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptsaleRegisterReport & "'"))
            If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
                arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If

            'Dim sfd As SaveFileDialog = New SaveFileDialog()
            'Dim filePath As String
            'sfd.FileName = Me.Text
            'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
            'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            '    filePath = sfd.FileName
            'Else
            '    Exit Sub
            'End If
            'transportSql.exportdataChilRows(Gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
            transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
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
            If ddlReportType.SelectedValue = "Total Sale" Then
                qry = "select * from (" & qry & ") PP order by [Total FAT KG]"
                transportSql.BulkExport("Sale_Register", qry, "order by [Total FAT KG]", FormatType)
                Exit Sub
            ElseIf ddlReportType.SelectedValue = "Location Wise" Then
                qry = "select * from (" & qry & ") PP order by [Location Code],[Location Name]"
                transportSql.BulkExport("Sale_Register", qry, "order by [Location Code],[Location Name]", FormatType)
                Exit Sub
            ElseIf ddlReportType.SelectedValue = "Item Group Wise" Then
                qry = "select * from (" & qry & ") PP order by [Location Code],[Location Name],[Item Group Code],[Item Group Description]"
                transportSql.BulkExport("Sale_Register", qry, "order by [Location Code],[Location Name],[Item Group Code],[Item Group Description]", FormatType)
                Exit Sub
            ElseIf ddlReportType.SelectedValue = "Customer Group Wise" Then
                qry = "select * from (" & qry & ") PP order by [Location Code],[Location Name],[Item Group Code],[Item Group Description],[Customer Group Code],[Customer Group Description]"
                transportSql.BulkExport("Sale_Register", qry, "order by [Location Code],[Location Name],[Item Group Code],[Item Group Description],[Customer Group Code],[Customer Group Description]", FormatType)
                Exit Sub
            ElseIf ddlReportType.SelectedValue = "Item Wise" Then
                qry = "select * from (" & qry & ") PP order by [Location Code],[Location Name],[Item Group Code],[Item Group Description],[Customer Group Code],[Customer Group Description],[Item Code],[Item Name]"
                transportSql.BulkExport("Sale_Register", qry, " order by [Location Code],[Location Name],[Item Group Code],[Item Group Description],[Customer Group Code],[Customer Group Description],[Item Code],[Item Name]", FormatType)
                Exit Sub
            ElseIf ddlReportType.SelectedValue = "Customer Wise" Then
                qry = "select * from (" & qry & ") PP order by [Location Code],[Location Name],[Item Group Code],[Item Group Description],[Customer Group Code],[Customer Group Description],[Item Code],[Item Name],[Customer Code],[Customer Name]"
                transportSql.BulkExport("Sale_Register", qry, "order by [Location Code],[Location Name],[Item Group Code],[Item Group Description],[Customer Group Code],[Customer Group Description],[Item Code],[Item Name],[Customer Code],[Customer Name]", FormatType)
                Exit Sub
            ElseIf ddlReportType.SelectedValue = "Document Wise" Then

                transportSql.BulkExport("Sale_Register", qry, "order by convert(date,[Document_Date],103),[Document No]", FormatType)
                Exit Sub
            ElseIf ddlReportType.SelectedValue = "Document Detail" Then
                transportSql.BulkExport("Sale_Register", qry, "order by convert(date,[Document_Date],103),[Document No]", FormatType)
                Exit Sub
            ElseIf obj.ReportType = "Document Info Level" Then
                transportSql.BulkExport("Sale_Register", qry, "order by convert(date,[Document_Date],103),[Document No]", FormatType)
                Exit Sub
            End If


            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow("Data exported successfully")
        Catch ex As Exception
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            clsCommon.ProgressBarPercentHide()
        End Try
    End Sub


    'Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs)
    '    If (Gv1.Rows.Count <= 0) Then
    '        common.clsCommon.MyMessageBoxShow("No Data To Export")
    '        Exit Sub
    '    End If
    '    Print(Exporter.Excel)
    'End Sub


    Private Sub chk_stockingunit_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chk_stockingunit.ToggleStateChanged
        If chk_stockingunit.Checked Then
            txtUOM.Enabled = False
            txtUOM.Value = ""
        Else
            txtUOM.Enabled = True
        End If
    End Sub

    Private Sub ddlReportType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles ddlReportType.SelectedIndexChanged
        If IsFormLoad = False Then
            Exit Sub
        End If
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
            '' richa UDL/09/08/18-000213
            If ddlReportType.SelectedValue = "Document Detail" AndAlso (txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Contains("Misc Sale")) Then
                lblSubCategory.Visible = True
                ddlSubCategory.Visible = True
                ddlSubCategory.SelectedValue = "All"
            Else
                lblSubCategory.Visible = False
                ddlSubCategory.Visible = False
                ddlSubCategory.SelectedValue = "All"
            End If
        End If
    End Sub

    Private Sub QExpExcel_Click(sender As Object, e As EventArgs) Handles QExpExcel.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptsaleRegisterReport & "'"))
            If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
                arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If

            'Dim sfd As SaveFileDialog = New SaveFileDialog()
            'Dim filePath As String
            'sfd.FileName = Me.Text
            'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
            'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            '    filePath = sfd.FileName
            'Else
            '    Exit Sub
            'End If
            '' applyExportTemplate
            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            'Dim fileCount As Integer = transportSql.exportdataChilRows(Gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
            'Dim fileCount As Integer = transportSql.exportdataChilRows(Gv1, Me.Text, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            ' Dim fileCount As Integer = transportSql.QuickExportToExcel(Gv1, Me.Text, , , arrHeader)
            'If fileCount > 1 Then
            '    common.clsCommon.MyMessageBoxShow("Data Exported in directory -" & System.IO.Path.GetDirectoryName(filePath) & "\" & System.IO.Path.GetFileName(filePath) & " in " & fileCount & " files.")
            'Else
            '    common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            '    Process.Start(filePath)
            'End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub QExpCSV_Click(sender As Object, e As EventArgs) Handles QExpCSV.Click
        Try
            If Gv1 Is Nothing OrElse Gv1.RowCount <= 0 Then
                MsgBox("Grid is empty!!!")
                Exit Sub
            End If
            ExportCSV(Gv1, True)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub
    Public Sub ExportCSV(ByVal sender As RadGridView, Optional ByVal AddHeader As Boolean = False)
        Try
            '', ByVal FileName As String, 

            Dim sfd As SaveFileDialog = New SaveFileDialog()
            Dim filePath As String
            sfd.FileName = Me.Text
            sfd.Filter = "CSV Files (*.csv) |*.csv"
            If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                filePath = sfd.FileName
            Else
                Exit Sub
            End If
            'transportSql.applyExportTemplate(Gv1, FORMTYPE, ddlReportType.SelectedValue)
            Dim filecount As Integer = ExportCSVMultipleFile(Gv1, filePath, True)
            If filecount <= 1 Then
                clsCommon.MyMessageBoxShow("Data Exported successfully")
                Process.Start(filePath)
            Else
                clsCommon.MyMessageBoxShow("Data Exported in directory -" & System.IO.Path.GetDirectoryName(filePath) & "\" & System.IO.Path.GetFileName(filePath) & " in " & filecount & " files")
            End If

            'Dim OpenInExcel As Boolean = True
            'If Gv1.Rows.Count * Gv1.Columns.Count > 22000000 Then
            '    OpenInExcel = False
            'Else
            '    OpenInExcel = True
            'End If
            'clsCommon.ProgressBarShow()
            'IO.File.WriteAllLines(filePath, transportSql.ExportCSV(sender, AddHeader))
            'clsCommon.ProgressBarHide()

            'If OpenInExcel Then
            '    clsCommon.MyMessageBoxShow("Data Exported successfully")
            '    Process.Start(filePath)
            'Else
            '    clsCommon.MyMessageBoxShow("Data Exported successfully but can not open through excel, use other utility to open the file.")
            'End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub


    'added by priti ERO/22/05/18-000322 for ERODe to add zone filter
    Private Sub txtZone__My_Click(sender As Object, e As EventArgs) Handles txtZone._My_Click
        Dim qry As String = " select distinct TSPL_customer_master.Zone_Code as Code,TSPL_ZONE_MASTER.Description as Name,TSPL_ZONE_MASTER.City_Code from TSPL_customer_master left join TSPL_ZONE_MASTER on TSPL_customer_master.Zone_Code=TSPL_ZONE_MASTER.Zone_Code where 1=1 "
        txtZone.arrValueMember = clsCommon.ShowMultipleSelectForm("ZoneMulSel", qry, "Code", "Name", txtZone.arrValueMember, txtZone.arrDispalyMember)
    End Sub
    'added by richa  BHA/17/08/18-000441 
    Private Sub TxtRoute__My_Click(sender As Object, e As EventArgs) Handles TxtRoute._My_Click
        Dim qry As String = "Select TSPL_ROUTE_MASTER.Route_No AS Code,TSPL_ROUTE_MASTER.Route_Desc as Name from TSPL_ROUTE_MASTER  where 1=1 "
        TxtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("RouteMulSel", qry, "Code", "Name", TxtRoute.arrValueMember, TxtRoute.arrDispalyMember)
    End Sub


    Private Sub PDF_Click(sender As Object, e As EventArgs) Handles Pdf.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptsaleRegisterReport & "'"))
            If clsCommon.myLen(txtUOM.Value) > 0 Then
                arrHeader.Add(" UOM : " + txtUOM.Value)
            End If
            arrHeader.Add(" Report Type : " + ddlReportType.Text)
            If txtTransaction.arrDispalyMember IsNot Nothing AndAlso txtTransaction.arrDispalyMember.Count > 0 Then
                arrHeader.Add(" Transaction : " + clsCommon.GetMulcallStringWithComma(txtTransaction.arrDispalyMember))
            End If
            If txtState.arrDispalyMember IsNot Nothing AndAlso txtState.arrDispalyMember.Count > 0 Then
                arrHeader.Add(" State : " + clsCommon.GetMulcallStringWithComma(txtState.arrDispalyMember))
            End If
            If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
                arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If

            If txtItemGroup.arrDispalyMember IsNot Nothing AndAlso txtItemGroup.arrDispalyMember.Count > 0 Then
                arrHeader.Add(" Item Group : " + clsCommon.GetMulcallStringWithComma(txtItemGroup.arrDispalyMember))
            End If
            If txtItem.arrDispalyMember IsNot Nothing AndAlso txtItem.arrDispalyMember.Count > 0 Then
                arrHeader.Add(" Item : " + clsCommon.GetMulcallStringWithComma(txtItem.arrDispalyMember))
            End If
            If txtCustGroup.arrDispalyMember IsNot Nothing AndAlso txtCustGroup.arrDispalyMember.Count > 0 Then
                arrHeader.Add(" Customer Group : " + clsCommon.GetMulcallStringWithComma(txtCustGroup.arrDispalyMember))
            End If
            If txtCustomer.arrDispalyMember IsNot Nothing AndAlso txtCustomer.arrDispalyMember.Count > 0 Then
                arrHeader.Add(" Customer : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember))
            End If
            If txtZone.arrDispalyMember IsNot Nothing AndAlso txtZone.arrDispalyMember.Count > 0 Then
                arrHeader.Add(" Zone : " + clsCommon.GetMulcallStringWithComma(txtZone.arrDispalyMember))
            End If
            If TxtRoute.arrDispalyMember IsNot Nothing AndAlso TxtRoute.arrDispalyMember.Count > 0 Then
                arrHeader.Add(" Route : " + clsCommon.GetMulcallStringWithComma(TxtRoute.arrDispalyMember))
            End If

            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)

            clsCommon.MyExportToPDF(Me.Text, Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub BulkCSV_Click(sender As Object, e As EventArgs) Handles BulkCSV.Click
        BulkExport("csv")
    End Sub

    Private Sub BulkExcel_Click(sender As Object, e As EventArgs) Handles BulkExcel.Click
        BulkExport("xls")
    End Sub
    ''richa ERO/24/06/19-000653
    Private Sub chkShowOnlyVSP_CheckStateChanged(sender As Object, e As EventArgs) Handles chkShowOnlyVSP.CheckStateChanged
        If chkShowOnlyVSP.Checked Then
            LblVLC.Visible = True
            TxtVLC.Visible = True
        Else
            LblVLC.Visible = False
            TxtVLC.Visible = False
            TxtVLC.arrValueMember = Nothing
        End If
    End Sub

    Private Sub TxtVLC__My_Click(sender As Object, e As EventArgs) Handles TxtVLC._My_Click
        Dim qry As String = "select TSPL_VLC_MASTER_HEAD.VLC_Code as Code,TSPL_VLC_MASTER_HEAD.VLC_Name as Name from TSPL_VLC_MASTER_HEAD where 1=1 and TSPL_VLC_MASTER_HEAD.Active=1"
        If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
            qry += " and TSPL_VLC_MASTER_HEAD.VSP_Code in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") "
        End If
        TxtVLC.arrValueMember = clsCommon.ShowMultipleSelectForm("VLCMulSel", qry, "Code", "Name", TxtVLC.arrValueMember, TxtVLC.arrDispalyMember)
        ''richa agarwal 26 June,2019 ERO/24/06/19-000653
        If chkShowOnlyVSP.Checked = True Then
            If TxtVLC.arrValueMember IsNot Nothing AndAlso TxtVLC.arrValueMember.Count > 0 Then
                Dim arr As ArrayList = Nothing
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Cust_Code as Code  from TSPL_CUSTOMER_VENDOR_MAPPING where Vendor_Code in (select TSPL_VLC_MASTER_HEAD.VSP_Code from TSPL_VLC_MASTER_HEAD where 1=1 and TSPL_VLC_MASTER_HEAD.Active=1 and TSPL_VLC_MASTER_HEAD.VLC_Code in (" + clsCommon.GetMulcallString(TxtVLC.arrValueMember) + ")) ")
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    arr = New ArrayList
                    For Each dr As DataRow In dt.Rows
                        arr.Add(dr("Code"))
                    Next
                End If
                txtCustomer.arrValueMember = arr
            Else
                txtCustomer.arrValueMember = Nothing
            End If
        End If
    End Sub
End Class
