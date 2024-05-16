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
Public Class RptSaleRegisterReportWithCSASalePattiProductPackWise
    Inherits FrmMainTranScreen

    Dim atchqry As String = ""
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim dt As DataTable
    Dim btnReferesh As Boolean = False
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
    Dim strPivotForFinalOuterQuery As String
    Dim strPivotLoationForOuterQryForm As String
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
        'MyBase.SetUserMgmt(clsUserMgtCode.MISSaleRegisterWithCSASalePattiProductPackWise)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
        'btnQuickExport.Visible = MyBase.isExport

    End Sub
#End Region

    Sub LoadTypes()
        dt = New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Rows.Add("Location Wise")
        dt.Rows.Add("Item Wise")
        dt.Rows.Add("Item Group Wise")
        dt.Rows.Add("Customer Group Wise")

        dt.Rows.Add("Customer Wise")
        dt.Rows.Add("Document Wise")
        dt.Rows.Add("Document Detail")
        dt.Rows.Add("Total Sale")
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowParticluarColumnInSalesRegisterForGopalJee, clsFixedParameterCode.ShowParticluarColumnInSalesRegisterForGopalJee, Nothing)) = 0 Then
            dt.Rows.Add("Document Info Level")
        End If

        ddlReportType.DataSource = dt
        ddlReportType.ValueMember = "Code"
        ddlReportType.DisplayMember = "Code"
    End Sub

    Private Sub txtUOM__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtUOM._MYValidating
        Dim qry As String = "select Unit_Code as Code,Unit_Desc as Description from TSPL_UNIT_MASTER"
        txtUOM.Value = clsCommon.ShowSelectForm("fndUOMMaster", qry, "Code", "", txtUOM.Value, "Code", isButtonClicked)
    End Sub


   Sub Print(ByVal IsPrint As Exporter)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""

            Dim obj As clsSaleRegisterParameterType = ReturnFilterData()
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " ")

            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
                arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If
            If IsPrint = Exporter.Excel Then
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid(" Sale Register:" + ddlReportType.SelectedValue, Gv1, arrHeader, Me.Text)
                Exit Sub
            ElseIf IsPrint = Exporter.PDF Then
                clsCommon.MyExportToPDF("Sale Register" + ddlReportType.SelectedValue, Gv1, arrHeader, "Sale Register", True)
                Exit Sub
            End If
            '======================Added by preeti gupta Against ticket no[ERO/23/04/19-000569]===
            Dim strCodeColumn As String = ""
            Dim strMaxDescColumn As String = ""
            Dim strCodeDescColumn As String = ""
            Dim dtCategory As DataTable

            dtCategory = clsDBFuncationality.GetDataTable("select ITEM_CATEGORY_CODE AS CodeColumn,ITEM_CATEGORY_CODE+' Description' as CodeDescColumn,DESCRIPTION as DescColumn  from TSPL_ITEM_CATEGORY_LEVEL order by CATEGORY_LEVEL")
            If dtCategory IsNot Nothing AndAlso dtCategory.Rows.Count > 0 Then
                For ii As Integer = 0 To dtCategory.Rows.Count - 1
                    If ii <> 0 Then
                        strCodeColumn += ","
                        strCodeDescColumn += ","
                        strMaxDescColumn += ","
                    End If
                    strCodeColumn += "[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
                    strCodeDescColumn += "[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")) + "]"
                    strMaxDescColumn += " max([" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")) + "]) as [" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")) + "]"

                Next
            End If

            '====================================================================
            clsCommon.ProgressBarShow()
            ddlReportType.Enabled = False
            txtState.Enabled = False
            txtLocation.Enabled = False
            txtTransaction.Enabled = False
            txtItemGroup.Enabled = False
            txtItem.Enabled = False
            txtCustomer.Enabled = False
            txtCustGroup.Enabled = False

            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()


            Unit_Code = txtUOM.Value
            clsCommon.ProgressBarUpdate("Loading Data.Please Wait...")
            Dim str As String = ""
            Dim dt As DataTable = Nothing
            Dim strRunQuery As String = ""
            Dim strMain As String = ClsUDLSalesQuery.ReturnQueryWithCSASalePatti(obj, FORMTYPE)(0)
            If ddlReportType.SelectedValue = "Total Sale" Then
                strRunQuery = "select sum(COALESCE([FAT KG],0)) as [Total FAT KG],sum(COALESCE([SNF KG],0)) as [Total SNF KG],sum([Sale Amount]) as [Total Sale Amount],sum([Additional Amount]) as [Total Additional Amount],sum([Total Tax Amount]) as [Total Tax Amount],sum([Total Amount] ) as [Total Amount] from (" & strMain & ") as Final"
            ElseIf ddlReportType.SelectedValue = "Location Wise" Then
                strRunQuery = " select ' " & clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") & " ' as FromDate,' " & clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") & " ' as ToDate," + strMaxDescColumn + ", " & _
                             " sum([Sale Amount]/100000 )  as [Sale Amount (Rs./Lac)] " & _
                              " from (" & strMain & ") as Final  group by " + strCodeColumn + ""
            ElseIf ddlReportType.SelectedValue = "Item Group Wise" Then
                strRunQuery = "select [Location Code],[Location Name],[Item Group Code],[Item Group Description],sum(COALESCE([FAT KG],0)) as [Total FAT KG],sum(COALESCE([SNF KG],0)) as [Total SNF KG],sum([Sale Amount]) as [Total Sale Amount],sum([Additional Amount]) as [Total Additional Amount],sum([Total Tax Amount]) as [Total Tax Amount],sum([Total Amount] ) as [Total Amount] from (" & strMain & ") as Final group by [Location Code],[Location Name],[Item Group Code],[Item Group Description]"
            ElseIf ddlReportType.SelectedValue = "Customer Group Wise" Then
                strRunQuery = "select [Location Code],[Location Name],[Item Group Code],[Item Group Description],[Customer Group Code],[Customer Group Description],sum(COALESCE([FAT KG],0)) as [Total FAT KG],sum(COALESCE([SNF KG],0)) as [Total SNF KG],sum([Sale Amount]) as [Total Sale Amount],sum([Additional Amount]) as [Total Additional Amount],sum([Total Tax Amount]) as [Total Tax Amount],sum([Total Amount] ) as [Total Amount] from (" & strMain & ") as Final group by [Location Code],[Location Name],[Item Group Code],[Item Group Description],[Customer Group Code],[Customer Group Description]"
            ElseIf ddlReportType.SelectedValue = "Item Wise" Then
                strRunQuery = "select [Location Code],[Location Name],[Item Group Code],[Item Group Description],[Customer Group Code],[Customer Group Description],[Item Code],[Item Name],sum(COALESCE([FAT KG],0)) as [Total FAT KG],sum(COALESCE([SNF KG],0)) as [Total SNF KG],sum([Sale Amount]) as [Total Sale Amount],sum([Additional Amount]) as [Total Additional Amount],sum([Total Tax Amount]) as [Total Tax Amount],sum([Total Amount] ) as [Total Amount] from (" & strMain & ") as Final group by [Location Code],[Location Name],[Item Group Code],[Item Group Description],[Customer Group Code],[Customer Group Description],[Item Code],[Item Name]"
            ElseIf ddlReportType.SelectedValue = "Customer Wise" Then
                strRunQuery = "select [Location Code],[Location Name],[Item Group Code],[Item Group Description],[Customer Group Code],[Customer Group Description],[Customer Code],[Customer Name],[Item Code],[Item Name],sum(COALESCE([FAT KG],0)) as [Total FAT KG],sum(COALESCE([SNF KG],0)) as [Total SNF KG],sum([Sale Amount]) as [Total Sale Amount],sum([Additional Amount]) as [Total Additional Amount],sum([Total Tax Amount]) as [Total Tax Amount],sum([Total Amount] ) as [Total Amount] from (" & strMain & ") as Final group by [Location Code],[Location Name],[Item Group Code],[Item Group Description],[Customer Group Code],[Customer Group Description],[Item Code],[Item Name],[Customer Code],[Customer Name]"
            ElseIf ddlReportType.SelectedValue = "Document Wise" Then
                strRunQuery = "select [Document No],[Document_date],[Trans Type],[Location Code],[Location Name],[Customer Group Code],[Customer Group Description],[Customer Code],[Customer Name],max([TIN No]) as [TIN No],max([GR No]) as [GR No],max([WayBill No]) as [WayBill No],max([Transporter Name]) as [Transporter Name],sum(COALESCE([FAT KG],0)) as [Total FAT KG],sum(COALESCE([SNF KG],0)) as [Total SNF KG],sum([Sale Amount]) as [Total Sale Amount],sum([Discount Amount]) as [Discount Amount],sum([Additional Amount]) as [Total Additional Amount],sum([Total Tax Amount]) as [Total Tax Amount],sum([Total Amount] ) as [Total Amount],max([AR Document No]) as [AR Document No], max([AR Document Amt]) as [AR Document Amt],max([AR Document Discount Amt]) as [AR Document Discount Amt] , case when max(coalesce([AR Amount Before Tax],0))>0 then  max([AR Amount Before Tax]) else  min([AR Amount Before Tax]) end as [AR Amount Before Tax],max([AR Total Tax]) as [AR Total Tax],max([AR Total Add Charge]) as [AR Total Add Charge] from (" & strMain & ") as Final group by [Document No],[Location Code],[Location Name],[Customer Group Code],[Customer Group Description],[Customer Code],[Customer Name],[Document_date],[Trans Type] order by convert(date,[Document_Date],103),[Document No]"
            ElseIf ddlReportType.SelectedValue = "Document Detail" Then
                strRunQuery = strMain & "order by convert(date,[Document_Date],103),[Document No]"
            End If
            RadPageViewPage2.Text = "Pack Wise"

            dt = clsDBFuncationality.GetDataTable(strRunQuery)
            Gv1.DataSource = Nothing
            Gv1.Columns.Clear()
            Gv1.Rows.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.EnableFiltering = True

            Gv1.Tag = "Pack Wise"


            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            Else
                Gv1.DataSource = dt
                SetGridFormationOFGV1()
            End If
            'FindAndRestoreGridLayout(Me)
            ReStoreGridLayout()
            Gv1.MasterTemplate.AllowAddNewRow = False
            RadPageView1.SelectedPage = RadPageViewPage2
            If btnReferesh = False Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinHeader(), "rptProductNPackWiseSalesReport", "MCC Milk Bill Details (Route Wise)", "Address.rpt")
                frmCRV = Nothing
            End If

        Catch ex As Exception
            clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)

        Finally
            clsCommon.ProgressBarHide()
        End Try


    End Sub
    Function ReturnDataReader() As SqlClient.SqlDataReader
        Dim obj As clsSaleRegisterParameterType = ReturnFilterData()
        Dim rd As SqlClient.SqlDataReader = clsPSInvoiceHead.GetReportDataReaderWithCSASalePatti(obj, clsUserMgtCode.MISSaleRegisterWithCSASalePattiProductPackWise)
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

        Dim obj As New clsSaleRegisterParameterType
        strPivotForFinalOuterQuery = ClsUDLSalesQuery.GetPivotForFinalOuterQry()
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
        If clsCommon.myLen(Document_No) > 0 Then
            obj.Document_Code = Document_No
            'strMCCMaterial += " and xx.[Document No] = '" & Document_No & "' "
        End If
        Dim Other_Cond As String = ""
        Dim strWhrCatg As String = ""
        strWhrCatg = ""
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
                        strWhrCatg += " [" + clsCommon.myCstr(gvCategory.Rows(ii).Cells("CODE").Value) + "] in ("
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
        End If
        If btnPosted.IsChecked Then
            Other_Cond += " and xx.Status=1  "
        ElseIf btnUnposted.IsChecked Then
            Other_Cond += " and xx.Status=0  "
        End If
        obj.Other_Cond = Other_Cond
        obj.Unit_Code = txtUOM.Value
        obj.ReportType = ddlReportType.SelectedValue
        obj.From_Date = fromDate.Value
        obj.To_Date = ToDate.Value
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
            Gv1.Columns(ii).FormatString = "{0:n2}"
        Next
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
                Gv1.Columns("Net Weight").IsVisible = False
                Gv1.Columns("Net Weight").Width = 70
                Gv1.Columns("Net Weight").HeaderText = "Net Weight"

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
                Gv1.Columns("Net Weight").IsVisible = False
                Gv1.Columns("Net Weight").Width = 70
                Gv1.Columns("Net Weight").HeaderText = "Net Weight"

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

                 For i As Integer = 0 To Gv1.Columns.Count - 1
                Gv1.Columns(i).BestFit()
                Gv1.Columns(i).IsVisible = True

                If Gv1.Columns(i).Name.Contains("Code") = True And Not (Gv1.Columns(i).Name.Contains("Item Code") = False Or Gv1.Columns(i).Name.Contains("Customer Code") = False Or Gv1.Columns(i).Name.Contains("FromDate") = False Or Gv1.Columns(i).Name.Contains("ToDate") = False) Then
                    Gv1.Columns(i).IsVisible = False
                End If
               
                Gv1.Columns("fromDate").IsVisible = False
                Gv1.Columns("ToDate").IsVisible = False
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



                For i As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(i).BestFit()
                    Gv1.Columns(i).IsVisible = True

                    If Gv1.Columns(i).Name.Contains("Code") = True And Not (Gv1.Columns(i).Name.Contains("Item Code") = False Or Gv1.Columns(i).Name.Contains("Customer Code") = False Or Gv1.Columns(i).Name.Contains("Location Code") = False) Then
                        Gv1.Columns(i).IsVisible = False
                    End If
                Next


                Gv1.Columns("Item Code").IsVisible = False
                Gv1.Columns("Item Name").IsVisible = True
                Gv1.Columns("Item_Category_Code").HeaderText = " Category Code"
                Gv1.Columns("Item_Category_CodeDesc").HeaderText = "Category Description"
                Gv1.Columns("Item_Cagetory_Values").HeaderText = " Category Values"
                Gv1.Columns("Category_Value_Desc").HeaderText = "Values Desc."


            ElseIf ddlReportType.SelectedValue = "Customer Wise" Then

                Gv1.Columns("Total FAT KG").IsVisible = True
                Gv1.Columns("Total SNF KG").IsVisible = True
                Gv1.Columns("Total Amount").IsVisible = True
                Gv1.Columns("Location Code").IsVisible = True
                Gv1.Columns("Location Name").IsVisible = True

                Gv1.Columns("Customer Group Code").IsVisible = True
                Gv1.Columns("Customer Group Description").IsVisible = True


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

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim intCount As Integer = 0
            For Each col As GridViewColumn In Gv1.Columns
                If clsCommon.CompairString(col.Name, "Total FAT KG") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "Total SNF KG") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "FAT KG") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "SNF KG") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "Quantity") = CompairStringResult.Equal Then
                    Dim item1 As New GridViewSummaryItem(col.Name, "{0:F3}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)

                ElseIf col.Name.Contains("Amount") = True Or col.Name.Contains("Amt") = True Or col.Name.Contains("Total") = True Or strPivotForFinalOuterQuery.Contains(col.Name) = True Then
                    Dim item As New GridViewSummaryItem(col.Name, "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item)
                ElseIf col.Name.Contains("Rate") = True Or col.Name.Contains("%") = True Then
                    Dim item As New GridViewSummaryItem(col.Name, "{0:F2}", GridAggregateFunction.Avg)
                    summaryRowItem.Add(item)
                End If
            Next
        'Gv1.GroupDescriptors.Add(New GridGroupByExpression("Product as Item format ""{0}: {1}"" Group By Product"))
            
            Gv1.ShowGroupPanel = False
            Gv1.MasterTemplate.AutoExpandGroups = True
            Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom

        RadPageView1.SelectedPage = RadPageViewPage2
        Gv1.AllowAddNewRow = False
        Gv1.ShowGroupPanel = False
        Gv1.MasterTemplate.ShowTotals = True
        Gv1.BestFitColumns()
    End Sub
    Sub Reset()
        'txtUOM.Enabled = False
        Document_No = ""
        Document_No_Old = ""
      
        txtUOM.Value = ""
        LoadTypes()

        LoadCategory()
        txtState.arrValueMember = Nothing
        txtLocation.arrValueMember = Nothing
        txtItemGroup.arrValueMember = Nothing
        txtItem.arrValueMember = Nothing
        txtCustomer.arrValueMember = Nothing
        txtCustGroup.arrValueMember = Nothing
        rbtnCategoryAll.IsChecked = True

        ddlReportType.Enabled = True
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

        ddlReportType.SelectedIndex = 0
        btnPosted.IsChecked = True
        Gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        RadPageViewPage2.Text = "Pack Wise"
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
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            Dim obj As New clsGridLayout()
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            obj = New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
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
        btnReferesh = True
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = Gv1
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
        '    clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
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
            Reset()
        End If
    End Sub

    Private Sub RptSaleRegisterReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Visible = False
        arrBack = New List(Of String)
        SetUserMgmtNew()
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New ")
        GetMIS_ITem_GroupColumn()
        'If clsCommon.myLen(MIS_Item_Group) <= 0 Then
        '    clsCommon.MyMessageBoxShow("MIS Item Group Custom field is not create in Item Structure.")
        'End If
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
        If clsCommon.CompairString(clsUserMgtCode.RptFreshSaleRegister1, FORMTYPE) = CompairStringResult.Equal Then
            Dim arr As New ArrayList
            arr.Add("Fresh Sale")
            arr.Add("Fresh Sale Return")
            txtTransaction.arrValueMember = arr
            txtTransaction.Enabled = False
        End If
        RadPageViewPage2.Text = "Pack Wise"
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
        'DrillDown()
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
                        Case "Misc Sale"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ScrapSale, strTransCode)
                        Case "MCC Transfer"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCDispatch, strTransCode)
                        Case "Merchant Sale"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSalesInvoiceMT, strTransCode)
                    End Select

                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        Dim qry As String = " select cust_code as [Code], Customer_Name as [Name] from tspl_customer_master "
        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("CustMulSel", qry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
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
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCustGroup__My_Click(sender As Object, e As EventArgs) Handles txtCustGroup._My_Click
        Dim qry As String = " select Cust_Group_Code as Code,Cust_Group_Desc as Name from TSPL_CUSTOMER_GROUP_MASTER"
        txtCustGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("CustGroupMulSel", qry, "Code", "Name", txtCustGroup.arrValueMember, txtCustGroup.arrDispalyMember)
    End Sub

    Private Sub btnQuickExport_Click(sender As Object, e As EventArgs)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.MISSaleRegisterWithCSASalePattiProductPackWise & "'"))
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
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
            Dim qry As String = clsPSInvoiceHead.GetReportDataQueryWithCSASalePatti(obj, clsUserMgtCode.MISSaleRegisterWithCSASalePattiProductPackWise)

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
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            clsCommon.ProgressBarPercentHide()
        End Try
    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs)
        BulkExport("csv")
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        If (Gv1.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow("No Data To Export")
            Exit Sub
        End If
        Print(Exporter.Excel)
    End Sub

    Private Sub RadMenuItem3_Click(sender As Object, e As EventArgs)
        BulkExport("xls")
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        btnReferesh = False
        Print(Exporter.Refresh)
    End Sub

    Private Sub PDF_Click(sender As Object, e As EventArgs) Handles PDF.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""

            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " ")

            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)

            If clsCommon.myLen(txtUOM.Value) > 0 Then
                arrHeader.Add("UOM : " + txtUOM.Value)
            End If
            If txtTransaction.arrDispalyMember IsNot Nothing AndAlso txtTransaction.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Transaction : " + clsCommon.GetMulcallStringWithComma(txtTransaction.arrDispalyMember))
            End If
            If txtState.arrDispalyMember IsNot Nothing AndAlso txtState.arrDispalyMember.Count > 0 Then
                arrHeader.Add("State : " + clsCommon.GetMulcallStringWithComma(txtState.arrDispalyMember))
            End If

            If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If

            If txtItemGroup.arrDispalyMember IsNot Nothing AndAlso txtItemGroup.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Item Group : " + clsCommon.GetMulcallStringWithComma(txtItemGroup.arrDispalyMember))
            End If
            If txtItem.arrDispalyMember IsNot Nothing AndAlso txtItem.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(txtItem.arrDispalyMember))
            End If
            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            clsCommon.MyExportToPDF("Sale Register", Gv1, arrHeader, "Sale Register", PageSetupReport_ID, objCommonVar.CurrentUserCode)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class
