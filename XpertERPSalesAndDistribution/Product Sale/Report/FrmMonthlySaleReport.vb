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
'================Create by Sanjeet (21/02/2017)========

Public Class FrmMonthlySaleReport
    Inherits FrmMainTranScreen
    Dim item As Integer = 0
    Dim atchqry As String = ""
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim dt As DataTable

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
    Dim MIS_Item_Group As String = ""
    Dim arrBack As List(Of String)
    Dim Document_No As String = ""
    Dim Document_No_Old As String = ""
    Dim FORMTYPE As String = Nothing
    Dim ReportID As String = String.Empty
    Dim StrAddLastMonth As String

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
        ''MyBase.SetUserMgmt(FORMTYPE)
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmMonthlySaleReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If

        'RadSplitButton1.Visible = MyBase.isExport
        'btnQuickExport.Visible = MyBase.isExport
        btnExport.Visible = MyBase.isExport
        'radbtnBulkExp.Visible = MyBase.isQuickExportFlag
    End Sub
#End Region

    Sub LoadTypes()
        dt = New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Rows.Add("Product Wise")
        dt.Rows.Add("Town Wise")
        dt.Rows.Add("Customer Wise")
       
        ddlReportType.DataSource = dt
        ddlReportType.ValueMember = "Code"
        ddlReportType.DisplayMember = "Code"
    End Sub

    Private Sub txtUOM__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtUOM._MYValidating
        Dim qry As String = "select Unit_Code as Code,Unit_Desc as Description from TSPL_UNIT_MASTER"
        txtUOM.Value = clsCommon.ShowSelectForm("fndUOMMaster", qry, "Code", "", txtUOM.Value, "Code", isButtonClicked)
    End Sub


    'Sub Print(ByVal IsPrint As Exporter)
    '    Try
    '        Dim arrHeader As List(Of String) = New List(Of String)()
    '        Dim strTemp As String = ""
    '        arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " ")

    '        arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
    '        If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
    '            arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
    '        End If
    '        If IsPrint = Exporter.Excel Then
    '            clsCommon.MyExportToExcelGrid(" Sale Register:" + ddlReportType.SelectedValue, Gv1, arrHeader, Me.Text)
    '            Exit Sub
    '        ElseIf IsPrint = Exporter.PDF Then
    '            clsCommon.MyExportToPDF("Sale Register" + ddlReportType.SelectedValue, Gv1, arrHeader, "Sale Register", True)
    '            Exit Sub
    '        End If

    '        clsCommon.ProgressBarShow()
    '        ddlReportType.Enabled = False
    '        txtState.Enabled = False
    '        txtLocation.Enabled = False
    '        txtTransaction.Enabled = False
    '        txtItemGroup.Enabled = False
    '        txtItem.Enabled = False
    '        txtCustomer.Enabled = False
    '        txtCustGroup.Enabled = False

    '        Gv1.DataSource = Nothing
    '        Gv1.Rows.Clear()


    '        Unit_Code = txtUOM.Value
    '        clsCommon.ProgressBarUpdate("Loading Data.Please Wait...")
    '        Dim str As String = ""
    '        Dim dt As DataTable = Nothing

    '        RadPageViewPage2.Text = ddlReportType.SelectedValue

    '        Gv1.DataSource = Nothing
    '        Gv1.Columns.Clear()
    '        Gv1.Rows.Clear()
    '        Gv1.GroupDescriptors.Clear()
    '        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
    '        Gv1.EnableFiltering = True

    '        Gv1.Tag = ddlReportType.SelectedValue

    '        'Dim rd As SqlClient.SqlDataReader = ReturnDataReader()
    '        'Me.Gv1.MasterTemplate.LoadFrom(rd)
    '        'rd.Close()
    '        SetGridFormationOFGV1()

    '        FindAndRestoreGridLayout(Me)
    '        Gv1.MasterTemplate.AllowAddNewRow = False
    '        RadPageView1.SelectedPage = RadPageViewPage2





    '    Catch ex As Exception
    '        clsCommon.ProgressBarHide()
    '        clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
    '    Finally
    '        clsCommon.ProgressBarHide()
    '    End Try


    'End Sub
    '===============Added by preeti gupta Against ticket no[BHA/05/09/19-000926]
    Sub Print1(ByVal IsPrint As Exporter)
        Try

        
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
            Dim strCode As String = ""
            Dim strCodeColumn As String = ""
            Dim strMaxDescColumn As String = ""
            Dim strCodeDescColumn As String = ""
            Dim dtCategory As DataTable

            dtCategory = clsDBFuncationality.GetDataTable("select ITEM_CATEGORY_CODE AS CodeColumn,ITEM_CATEGORY_CODE+' Desc' as CodeDescColumn,DESCRIPTION as DescColumn  from TSPL_ITEM_CATEGORY_LEVEL order by CATEGORY_LEVEL")
            item = dtCategory.Rows.Count - 1
            If dtCategory IsNot Nothing AndAlso dtCategory.Rows.Count > 0 Then
                For ii As Integer = 0 To dtCategory.Rows.Count - 1
                    If ii <> 0 Then
                        strCode += ","
                        strCodeColumn += ","
                        strCodeDescColumn += ","
                        strMaxDescColumn += ","
                    End If
                    strCode += "[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
                    strCodeColumn += "[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
                    strCodeDescColumn += "[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")) + "]"
                    strMaxDescColumn += " max([" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")) + "]) as [" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")) + "]"
                Next
            End If
        clsCommon.ProgressBarUpdate("Loading Data.Please Wait...")
            Dim str As String = ""
            Dim STR1 As String = "SELECT '['+ A.DT +'],' FROM(select DISTINCT DATENAME(MONTH,Document_Date) as DT,DATEPART(MONTH,Document_Date) as DTD from TSPL_SD_SALE_INVOICE_HEAD WHERE Document_Date>=DateAdd(mm,-1, '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "') AND Document_Date<='" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "' ) AS A ORDER BY A.DTD FOR XML PATH('')"
            Dim StrMonth As String = Nothing
            StrMonth = clsDBFuncationality.getSingleValue(STR1)

            If StrMonth.Length > 0 Then
                StrMonth = StrMonth.Substring(0, StrMonth.Length - 1)
            End If
            Dim STR2 As String = "SELECT '['+ A.DT +'],' FROM(select DISTINCT DATENAME(MONTH,Document_Date) as DT,DATEPART(MONTH,Document_Date) as DTD from TSPL_SD_SALE_INVOICE_HEAD WHERE Document_Date>= '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' AND Document_Date<='" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "' ) AS A ORDER BY A.DTD FOR XML PATH('')"
            Dim StrMonthOnlyforCustomer As String = Nothing
            StrMonthOnlyforCustomer = clsDBFuncationality.getSingleValue(STR2)

            If StrMonthOnlyforCustomer.Length > 0 Then
                StrMonthOnlyforCustomer = StrMonthOnlyforCustomer.Substring(0, StrMonthOnlyforCustomer.Length - 1)
            End If
            ''======================Sum of Column Header==================================================
            Dim STRmid As String = "with datename1 as (select DISTINCT DATENAME(MONTH,Document_Date) as DT,DATEPART(MONTH,Document_Date) as DTD from TSPL_SD_SALE_INVOICE_HEAD WHERE Document_Date>='" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' AND Document_Date<='" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "') " & _
                   " SELECT case when (dtd=(select min(DTD) from datename1) or dtd=(select max(DTD) from datename1)) then '-['+ A.DT +']' else '' end FROM datename1 AS A ORDER BY A.DTD  FOR XML PATH('') "
            Dim StrMidClm As String = Nothing
            StrMidClm = clsDBFuncationality.getSingleValue(STRmid)
            If StrMidClm.Length > 0 Then
                StrMidClm = StrMidClm.Substring(1, StrMidClm.Length - 1)
            End If

            Dim STRlast As String = "with datename1 as (select DISTINCT DATENAME(MONTH,Document_Date) as DT,DATEPART(MONTH,Document_Date) as DTD from TSPL_SD_SALE_INVOICE_HEAD WHERE Document_Date>=dateadd(mm,-1,'" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "') AND Document_Date<='" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "') " & _
                  " SELECT case when (dtd=(select max(DTD) from datename1) or dtd=(select min(DTD) from datename1)) then '-['+ A.DT +']' else '' end FROM datename1 AS A ORDER BY A.DTD  FOR XML PATH('') "
            Dim StrLastClm As String = Nothing
            StrLastClm = clsDBFuncationality.getSingleValue(STRlast)
            If StrLastClm.Length > 0 Then
                StrLastClm = StrLastClm.Substring(1, StrLastClm.Length - 1)
            End If
            

            '=================sum of pivot 1st and last column==============================
            Dim StrAdd_Last As String = "select substring(a.xvalue,0,len(a.xvalue)-0) from (select distinct(select distinct('isnull(['+DATENAME(MONTH,Document_Date)+'],0)+') from TSPL_SD_SALE_INVOICE_HEAD WHERE Document_Date>=DateAdd(mm,-1, '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "') AND Document_Date<='" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'  FOR XML PATH(''))as xvalue)a"
            Dim StrAddLastMonth As String = Nothing
            StrAddLastMonth = clsDBFuncationality.getSingleValue(StrAdd_Last)

            Dim StrAdd As String = "select substring(a.xvalue,0,len(a.xvalue)-0) from (select distinct(select distinct('isnull(['+DATENAME(MONTH,Document_Date)+'],0)+') from TSPL_SD_SALE_INVOICE_HEAD WHERE Document_Date>='" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' AND Document_Date<='" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'  FOR XML PATH(''))as xvalue)a"
            Dim StrAddMonth As String = Nothing
            StrAddMonth = clsDBFuncationality.getSingleValue(StrAdd)


        Dim dt As DataTable = Nothing
        Dim strRunQuery As String = ""
        Dim obj As clsSaleRegisterParameterType = ReturnFilterData()
            Dim strMain As String = clsPSInvoiceHead.ReturnQuery(obj)(0)
            strRunQuery = strMain
            strRunQuery = "select final.*, DATENAME(MONTH,convert(date,([Document_date]),103)) as [MONTH]," & _
          " DATEPART(YEAR,convert(date,([Document_date]),103)) as [YEAR] from " & _
            "(" & strRunQuery & ")" & _
            "as final"

            If clsCommon.CompairString(ddlReportType.SelectedValue, "Customer Wise") = CompairStringResult.Equal Then
                strRunQuery = "Select *," + StrAddMonth + " as [Sale Amount]  from( select [Customer Group Code],max([Customer Group Description]) as [Customer Group Description],[Customer Code],max([Customer Name]) as [Customer Name], max([MONTH]) as [MONTH], max([YEAR]) as [YEAR], sum ([Sale Amount]) as [Sale Amount] from " & _
                "(" & strRunQuery & ")" & _
                "as CustomerWise group by [Customer Group Code],[Customer Code], DATEPART(MONTH,convert(date,CustomerWise.[Document_date],103)),DATEPART(YEAR,convert(date,CustomerWise.[Document_date],103))  " & _
                " ) as Mian_Query Pivot(sum([Sale Amount]) FOR [MONTH] IN(" + StrMonthOnlyforCustomer + ") )  AS PVT1 order by [Customer Code] "
            ElseIf clsCommon.CompairString(ddlReportType.SelectedValue, "Product Wise") = CompairStringResult.Equal Then
                strRunQuery = "Select *," + StrAddMonth + " as '" + StrMidClm + "'," + StrAddLastMonth + " as  '" + StrLastClm + "' from (select " + strMaxDescColumn + ", max([MONTH]) as [MONTH], max([YEAR]) as [YEAR], isnull(sum ([Sale Amount]),0) as [Sale Amount] from" & _
                 "(" & strRunQuery & ")" & _
                 " as ProductWise group by " + strCodeColumn + ", DATEPART(MONTH,convert(date,ProductWise.[Document_date],103)),DATEPART(YEAR,convert(date,ProductWise.[Document_date],103))" & _
                " ) as Mian_Query Pivot(sum([Sale Amount]) FOR [MONTH] IN(" + StrMonth + ") )  AS PVT1 order by " + strCodeDescColumn + " "
            ElseIf clsCommon.CompairString(ddlReportType.SelectedValue, "Town Wise") = CompairStringResult.Equal Then
                strRunQuery = "Select *," + StrAddMonth + " as '" + StrMidClm + "'," + StrAddLastMonth + " as  '" + StrLastClm + "' from (select max([Place of Supply]) as [Town], " + strMaxDescColumn + ", max([MONTH]) as [MONTH], max([YEAR]) as [YEAR], isnull(sum ([Sale Amount]),0) as [Sale Amount] from" & _
                "(" & strRunQuery & ")" & _
                " as ProductWise group by [City Code], " + strCodeColumn + ", DATEPART(MONTH,convert(date,ProductWise.[Document_date],103)),DATEPART(YEAR,convert(date,ProductWise.[Document_date],103))" & _
               " ) as Mian_Query Pivot(sum([Sale Amount]) FOR [MONTH] IN(" + StrMonth + ") )  AS PVT1 order by " + strCodeDescColumn + " "
            End If
            dt = clsDBFuncationality.GetDataTable(strRunQuery)

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            Else
                Gv1.DataSource = Nothing
                Gv1.Rows.Clear()
                Gv1.Columns.Clear()
                Gv1.DataSource = dt
                Gv1.GroupDescriptors.Clear()
                Gv1.MasterTemplate.SummaryRowsBottom.Clear()
                If clsCommon.CompairString(ddlReportType.SelectedValue, "Town Wise") = CompairStringResult.Equal Then
                    item = item + 3
                ElseIf clsCommon.CompairString(ddlReportType.SelectedValue, "Customer Wise") = CompairStringResult.Equal Then
                    item = 5
                Else
                    item = item + 1
                End If
                SetGridFormationOFGV1(item)
            End If
            RadPageView1.SelectedPage = RadPageViewPage2
            RadPageViewPage2.Text = ddlReportType.SelectedValue
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        Finally
            clsCommon.ProgressBarHide()
        End Try



    End Sub
    Sub Print(ByVal IsPrint As Exporter, Optional ByVal TownWise As String = Nothing)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""


            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " ")

            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
                arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If
            'If IsPrint = Exporter.Excel Then
            '    clsCommon.MyExportToExcelGrid(" Sale Register:" + ddlReportType.SelectedValue, Gv1, arrHeader, Me.Text)
            '    Exit Sub
            'ElseIf IsPrint = Exporter.PDF Then
            '    clsCommon.MyExportToPDF("Sale Register" + ddlReportType.SelectedValue, Gv1, arrHeader, "Sale Register", True)
            '    Exit Sub
            'End If

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
            'added by preeti against ticket no[]=========
            Dim strCode As String = ""
            Dim strCodeColumn As String = ""
            Dim strMaxDescColumn As String = ""
            Dim strCodeDescColumn As String = ""
            Dim dtCategory As DataTable

            dtCategory = clsDBFuncationality.GetDataTable("select ITEM_CATEGORY_CODE AS CodeColumn,ITEM_CATEGORY_CODE+' Description' as CodeDescColumn,DESCRIPTION as DescColumn  from TSPL_ITEM_CATEGORY_LEVEL order by CATEGORY_LEVEL")
            If dtCategory IsNot Nothing AndAlso dtCategory.Rows.Count > 0 Then
                For ii As Integer = 0 To dtCategory.Rows.Count - 1
                    If ii <> 0 Then
                        strCode += ","
                        strCodeColumn += ","
                        strCodeDescColumn += ","
                        strMaxDescColumn += ","
                    End If
                    strCode += "[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
                    strCodeColumn += "final.[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
                    strCodeDescColumn += "[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")) + "]"
                    strMaxDescColumn += " max([" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")) + "]) as [" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")) + "]"
                Next
            End If
            '===================================

            'Dim STR1 As String = "select substring(a.xvalue,0,len(a.xvalue)-0) from (select distinct(select distinct('['+DATENAME(MONTH,Document_Date)+'],') from TSPL_SD_SALE_INVOICE_HEAD WHERE Document_Date>='" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' AND Document_Date<='" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'  FOR XML PATH(''))as xvalue)a"
            Dim STR1 As String = "SELECT '['+ A.DT +'],' FROM(select DISTINCT DATENAME(MONTH,Document_Date) as DT,DATEPART(MONTH,Document_Date) as DTD from TSPL_SD_SALE_INVOICE_HEAD WHERE Document_Date>=DateAdd(mm,-1, '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "') AND Document_Date<='" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "' ) AS A ORDER BY A.DTD FOR XML PATH('')"
            Dim StrMonth As String = Nothing
            StrMonth = clsDBFuncationality.getSingleValue(STR1)

            If StrMonth.Length > 0 Then
                StrMonth = StrMonth.Substring(0, StrMonth.Length - 1)
            End If
            Dim STR2 As String = "SELECT '['+ A.DT +'],' FROM(select DISTINCT DATENAME(MONTH,Document_Date) as DT,DATEPART(MONTH,Document_Date) as DTD from TSPL_SD_SALE_INVOICE_HEAD WHERE Document_Date>= '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' AND Document_Date<='" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "' ) AS A ORDER BY A.DTD FOR XML PATH('')"
            Dim StrMonthOnlyforCustomer As String = Nothing
            StrMonthOnlyforCustomer = clsDBFuncationality.getSingleValue(STR2)

            If StrMonthOnlyforCustomer.Length > 0 Then
                StrMonthOnlyforCustomer = StrMonthOnlyforCustomer.Substring(0, StrMonthOnlyforCustomer.Length - 1)
            End If
            ''======================Sum of Column Header==================================================
            Dim STRmid As String = "with datename1 as (select DISTINCT DATENAME(MONTH,Document_Date) as DT,DATEPART(MONTH,Document_Date) as DTD from TSPL_SD_SALE_INVOICE_HEAD WHERE Document_Date>='" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' AND Document_Date<='" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "') " & _
                   " SELECT case when (dtd=(select min(DTD) from datename1) or dtd=(select max(DTD) from datename1)) then '-['+ A.DT +']' else '' end FROM datename1 AS A ORDER BY A.DTD  FOR XML PATH('') "
            Dim StrMidClm As String = Nothing
            StrMidClm = clsDBFuncationality.getSingleValue(STRmid)
            If StrMidClm.Length > 0 Then
                StrMidClm = StrMidClm.Substring(1, StrMidClm.Length - 1)
            End If

            Dim STRlast As String = "with datename1 as (select DISTINCT DATENAME(MONTH,Document_Date) as DT,DATEPART(MONTH,Document_Date) as DTD from TSPL_SD_SALE_INVOICE_HEAD WHERE Document_Date>=dateadd(mm,-1,'" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "') AND Document_Date<='" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "') " & _
                  " SELECT case when (dtd=(select max(DTD) from datename1) or dtd=(select min(DTD) from datename1)) then '-['+ A.DT +']' else '' end FROM datename1 AS A ORDER BY A.DTD  FOR XML PATH('') "
            Dim StrLastClm As String = Nothing
            StrLastClm = clsDBFuncationality.getSingleValue(STRlast)
            If StrLastClm.Length > 0 Then
                StrLastClm = StrLastClm.Substring(1, StrLastClm.Length - 1)
            End If
            'Dim StrMidClm As String = clsCommon.myCstr(MonthName(clsCommon.myCstr(fromDate.Value.Month))) + "-" + clsCommon.myCstr(MonthName(clsCommon.myCstr(ToDate.Value.Month)))
            'Dim StrLastClm As String = clsCommon.myCstr(MonthName(clsCommon.myCstr(ToDate.Value.Month))) + "-" + clsCommon.myCstr(MonthName(clsCommon.myCstr(clsCommon.myCDate(fromDate.Value.AddMonths(-1)).Month)))
            ''======================================================================
            '=================sum of pivot 1st and last column==============================
            Dim StrAdd_Last As String = "select substring(a.xvalue,0,len(a.xvalue)-0) from (select distinct(select distinct('isnull(['+DATENAME(MONTH,Document_Date)+'],0)+') from TSPL_SD_SALE_INVOICE_HEAD WHERE Document_Date>=DateAdd(mm,-1, '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "') AND Document_Date<='" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'  FOR XML PATH(''))as xvalue)a"
            Dim StrAddLastMonth As String = Nothing
            StrAddLastMonth = clsDBFuncationality.getSingleValue(StrAdd_Last)

            Dim StrAdd As String = "select substring(a.xvalue,0,len(a.xvalue)-0) from (select distinct(select distinct('isnull(['+DATENAME(MONTH,Document_Date)+'],0)+') from TSPL_SD_SALE_INVOICE_HEAD WHERE Document_Date>='" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' AND Document_Date<='" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'  FOR XML PATH(''))as xvalue)a"
            Dim StrAddMonth As String = Nothing
            StrAddMonth = clsDBFuncationality.getSingleValue(StrAdd)
            '======================================End========================
            '=====================Preeti Gupta==============

            '===================================
            Dim obj As clsSaleRegisterParameterType = ReturnFilterData()
            Dim strMain As String = clsPSInvoiceHead.ReturnQuery(obj)(0)

            Dim obj1 As clsSaleRegisterParameterType = ReturnFilterData("Y")

            Dim strMain2 As String = ClsUDLSalesQuery.ReturnQueryWithCSASalePatti(obj1, FORMTYPE)(0)

            Dim obj2 As clsSaleRegisterParameterType = ReturnFilterData("2")

            Dim strMain3 As String = ClsUDLSalesQuery.ReturnQueryWithCSASalePatti(obj2, FORMTYPE)(0)

            If clsCommon.CompairString(ddlReportType.SelectedValue, "Town Wise") = CompairStringResult.Equal Then
                strRunQuery = " Select *," + StrAddMonth + " as '" + StrMidClm + "'," + StrAddLastMonth + " as  '" + StrLastClm + "'  from ( select TSPL_CITY_MASTER.City_Name AS Town," + strMaxDescColumn + ","
            ElseIf clsCommon.CompairString(ddlReportType.SelectedValue, "Customer Wise") = CompairStringResult.Equal Then
                strRunQuery = " Select *," + StrAddMonth + " as [Total Amount]  from ( select [Customer Group Code],max([Customer Group Description]) as [Customer Group Description],[Customer Code],max([Customer Name]) as [Customer Name],"
            Else
                strRunQuery = " Select *," + StrAddMonth + " as '" + StrMidClm + "'," + StrAddLastMonth + " as  '" + StrLastClm + "'  from ( select " + strMaxDescColumn + ","
            End If
            strRunQuery += " DATENAME(MONTH,convert(date,max([Document_date]),103)) as [MONTH]," & _
            " DATEPART(YEAR,convert(date,max([Document_date]),103)) as [YEAR], isnull(sum([Sale Amount]),0.00)  as [Total Amount] " & _
            " from (" & strMain & ") as Final "
            If clsCommon.CompairString(ddlReportType.SelectedValue, "Town Wise") = CompairStringResult.Equal Then
                strRunQuery += " LEFT OUTER JOIN  TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=Final.[Customer Code] LEFT OUTER JOIN TSPL_CITY_MASTER ON TSPL_CITY_MASTER.CITY_CODE=TSPL_CUSTOMER_MASTER.CITY_CODE " & _
                     " group by TSPL_CITY_MASTER.City_Name, " + strCodeColumn + ", DATEPART(MONTH,convert(date,Final.[Document_date],103)),DATEPART(YEAR,convert(date,Final.[Document_date],103)) "
            ElseIf clsCommon.CompairString(ddlReportType.SelectedValue, "Customer Wise") = CompairStringResult.Equal Then
                strRunQuery += " group by final.[Customer Group Code] ,final.[Customer Code], DATEPART(MONTH,convert(date,Final.[Document_date],103)),DATEPART(YEAR,convert(date,Final.[Document_date],103)) "
            Else
                strRunQuery += " group by " + strCodeColumn + ", DATEPART(MONTH,convert(date,Final.[Document_date],103)),DATEPART(YEAR,convert(date,Final.[Document_date],103)) "
            End If
            If clsCommon.CompairString(ddlReportType.SelectedValue, "Town Wise") = CompairStringResult.Equal Then
                strRunQuery += " UNION ALL select TSPL_CITY_MASTER.City_Name AS Town," + strMaxDescColumn + ","
            ElseIf clsCommon.CompairString(ddlReportType.SelectedValue, "Customer Wise") = CompairStringResult.Equal Then
                strRunQuery += " UNION ALL select  [Customer Group Code],max([Customer Group Description]) as [Customer Group Description],[Customer Code],max([Customer Name]) as [Customer Name],"
            Else
                strRunQuery += " UNION ALL select " + strMaxDescColumn + ","
            End If
            strRunQuery += " DATENAME(MONTH,convert(date,max([Document_date]),103)) as [MONTH]," & _
          " DATEPART(YEAR,convert(date,max([Document_date]),103)) as [YEAR], sum([Total Amount])  as [Total Amount] " & _
          " from (" & strMain2 & ") as Final "
            If clsCommon.CompairString(ddlReportType.SelectedValue, "Town Wise") = CompairStringResult.Equal Then
                strRunQuery += " LEFT OUTER JOIN  TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=Final.[Customer Code] LEFT OUTER JOIN TSPL_CITY_MASTER ON TSPL_CITY_MASTER.CITY_CODE=TSPL_CUSTOMER_MASTER.CITY_CODE " & _
                     " group by TSPL_CITY_MASTER.City_Name, " + strCodeColumn + ", DATEPART(MONTH,convert(date,Final.[Document_date],103)),DATEPART(YEAR,convert(date,Final.[Document_date],103)) "
            ElseIf clsCommon.CompairString(ddlReportType.SelectedValue, "Customer Wise") = CompairStringResult.Equal Then
                strRunQuery += " group by final.[Customer Group Code] ,final.[Customer Code], DATEPART(MONTH,convert(date,Final.[Document_date],103)),DATEPART(YEAR,convert(date,Final.[Document_date],103)) "
            Else
                strRunQuery += " group by " + strCodeColumn + ", DATEPART(MONTH,convert(date,Final.[Document_date],103)),DATEPART(YEAR,convert(date,Final.[Document_date],103)) "
            End If

            If clsCommon.CompairString(ddlReportType.SelectedValue, "Town Wise") = CompairStringResult.Equal Then
                strRunQuery += " UNION ALL select TSPL_CITY_MASTER.City_Name AS Town," + strMaxDescColumn + ","
            ElseIf clsCommon.CompairString(ddlReportType.SelectedValue, "Customer Wise") = CompairStringResult.Equal Then
                strRunQuery += " UNION ALL select  [Customer Group Code],max([Customer Group Description]) as [Customer Group Description],[Customer Code],max([Customer Name]) as [Customer Name],"
            Else
                strRunQuery += " UNION ALL select " + strMaxDescColumn + ","
            End If
            strRunQuery += " DATENAME(MONTH,convert(date,max([Document_date]),103)) as [MONTH]," & _
        " DATEPART(YEAR,convert(date,max([Document_date]),103)) as [YEAR], sum([Total Amount])  as [Total Amount] " & _
        " from (" & strMain3 & ") as Final "
            If clsCommon.CompairString(ddlReportType.SelectedValue, "Town Wise") = CompairStringResult.Equal Then
                strRunQuery += " LEFT OUTER JOIN  TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=Final.[Customer Code] LEFT OUTER JOIN TSPL_CITY_MASTER ON TSPL_CITY_MASTER.CITY_CODE=TSPL_CUSTOMER_MASTER.CITY_CODE " & _
                     " group by TSPL_CITY_MASTER.City_Name, " + strCodeColumn + ", DATEPART(MONTH,convert(date,Final.[Document_date],103)),DATEPART(YEAR,convert(date,Final.[Document_date],103)) " & _
                      " ) as Mian_Query Pivot(sum([Total Amount]) FOR [MONTH] IN(" + StrMonth + ") )  AS PVT1 order by " + strCodeDescColumn + ",TOWN "

            ElseIf clsCommon.CompairString(ddlReportType.SelectedValue, "Customer Wise") = CompairStringResult.Equal Then
                strRunQuery += " group by final.[Customer Group Code] ,final.[Customer Code], DATEPART(MONTH,convert(date,Final.[Document_date],103)),DATEPART(YEAR,convert(date,Final.[Document_date],103)) " & _
                     " ) as Mian_Query Pivot(sum([Total Amount]) FOR [MONTH] IN(" + StrMonthOnlyforCustomer + ") )  AS PVT1 order by [Customer Code] "
            Else
                strRunQuery += " group by " + strCodeColumn + ", DATEPART(MONTH,convert(date,Final.[Document_date],103)),DATEPART(YEAR,convert(date,Final.[Document_date],103)) " & _
                     " ) as Mian_Query Pivot(sum([Total Amount]) FOR [MONTH] IN(" + StrMonth + ") )  AS PVT1 order by " + strCodeDescColumn + " "
            End If

            dt = clsDBFuncationality.GetDataTable(strRunQuery)

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)

                Exit Sub
            Else
                Gv1.DataSource = Nothing
                Gv1.Rows.Clear()
                Gv1.Columns.Clear()
                Gv1.DataSource = dt
                Gv1.GroupDescriptors.Clear()
                Gv1.MasterTemplate.SummaryRowsBottom.Clear()
                SetGridFormationOFGV1(item)


                If clsCommon.CompairString(ddlReportType.SelectedValue, "Town Wise") = CompairStringResult.Equal Then
                    item = 7
                ElseIf clsCommon.CompairString(ddlReportType.SelectedValue, "Customer Wise") = CompairStringResult.Equal Then
                    item = 5
                Else
                    item = 6
                End If

            End If

            'Me.PvtGrid.PivotGridElement.AggregateDescriptorsArea.Visibility = ElementVisibility.Collapsed
            'Me.PvtGrid.PivotGridElement.ColumnDescriptorsArea.Visibility = ElementVisibility.Collapsed
            'Me.PvtGrid.PivotGridElement.Margin = New Padding(0, -30, 0, 0)
            'Me.PvtGrid.PivotGridElement.VScrollBar.Margin = New Padding(0, 30, 0, 0)
            'End If
            ' FindAndRestoreGridLayout(Me)
            '' Gv1.MasterTemplate.AllowAddNewRow = False
            RadPageView1.SelectedPage = RadPageViewPage2
            RadPageViewPage2.Text = ddlReportType.SelectedValue

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


    End Function
    Function ReturnFilterData(Optional ByVal IsPrevious As String = Nothing) As clsSaleRegisterParameterType
        'strPivotForFinalOuterQuery = clsPSInvoiceHead.GetPivotForFinalOuterQry()
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
        If clsCommon.CompairString(IsPrevious, "Y") = CompairStringResult.Equal Then
            Dim strd As String = "01/" + clsCommon.myCstr(fromDate.Value.Month) + "/" + clsCommon.myCstr(fromDate.Value.Year)
            obj.From_Date = clsCommon.myCDate(strd).AddMonths(-1).AddYears(-1) 'fromDate.Value.AddYears(-1)
            obj.To_Date = ToDate.Value.AddYears(-1)
        ElseIf clsCommon.CompairString(IsPrevious, "2") = CompairStringResult.Equal Then
            Dim strd As String = "01/" + clsCommon.myCstr(fromDate.Value.Month) + "/" + clsCommon.myCstr(fromDate.Value.Year)
            obj.From_Date = clsCommon.myCDate(strd).AddMonths(-1).AddYears(-2)
            obj.To_Date = ToDate.Value.AddYears(-2)
        Else
            Dim strd As String = "01/" + clsCommon.myCstr(fromDate.Value.Month) + "/" + clsCommon.myCstr(fromDate.Value.Year)
            obj.From_Date = clsCommon.myCDate(strd).AddMonths(-1)
            obj.To_Date = ToDate.Value
        End If


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
    '====added by preeti gupta Against ticket no[BHA/03/09/19-000925]
    Sub SetGridFormationOFGV1(ByVal STRPivotSum As Integer)
        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            'Gv1.Columns(ii).IsVisible = False
            'Gv1.Columns(ii).FormatString = "{0:n2}"
        Next
        If clsCommon.CompairString(ddlReportType.SelectedValue, "Town Wise") = CompairStringResult.Equal Then
            Gv1.GroupDescriptors.Add(New GridGroupByExpression("Town AS TOWN format ""{0}: {1}"" Group By Town"))
        End If
        Dim summaryRowItem As New GridViewSummaryRowItem()
        If Gv1.Rows.Count > 0 Then

            For i As Integer = STRPivotSum To Gv1.Columns.Count - 1
                Dim aa = Gv1.Columns(i).HeaderText()
                Dim item1 As New GridViewSummaryItem(aa, "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)


            Next

            Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)


        End If




        RadPageView1.SelectedPage = RadPageViewPage2
        Gv1.AllowAddNewRow = False
        Gv1.ShowGroupPanel = True
        Gv1.BestFitColumns()
    End Sub
    Sub Reset()
        'txtUOM.Enabled = False
        Document_No = ""
        Document_No_Old = ""
        ToDate.Value = clsCommon.GETSERVERDATE()
        'KUNAL > TICKET : BM00000009568 > DATE : 19-OCT-2016
        fromDate.Value = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
        txtUOM.Value = ""
        LoadTypes()
        ddlReportType.SelectedValue = "Product Wise"
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
        RadPageViewPage2.Text = ddlReportType.SelectedValue
    End Sub
    Enum Exporter
        Excel = 0
        PDF = 1
        Print = 2
        Refresh = 3
    End Enum
    'Private Sub ReStoreGridLayout()
    '    Try
    '        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
    '            Dim obj As clsGridLayout = New clsGridLayout()
    '            obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
    '            If Not obj Is Nothing AndAlso obj.GridColumns >= Gv1.ColumnCount Then
    '                Dim ii As Integer
    '                For ii = 0 To Gv1.Columns.Count - 1 Step ii + 1
    '                    Gv1.Columns(ii).IsVisible = False
    '                    Gv1.Columns(ii).VisibleInColumnChooser = True
    '                Next
    '                Gv1.LoadLayout(obj.GridLayout)
    '                obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
    '            End If
    '        End If
    '    Catch err As Exception
    '        MessageBox.Show(err.Message)
    '    End Try
    'End Sub


    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
       
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
       
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
        ReportID = clsERPFuncationality.GetReportID(MyBase.Form_ID, ddlReportType.Text)
        PageSetupReport_ID = ReportID
        Print1(Exporter.Refresh)

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

    'Private Sub rmSetting_Click(sender As Object, e As EventArgs) Handles rmSetting.Click
    '    Dim frm As New FrmMailSMSSettingNew2()
    '    frm.FormId = clsUserMgtCode.RptFreshSaleRegister1
    '    frm.ShowDialog()
    'End Sub

    'Private Sub rmSend_Click(sender As Object, e As EventArgs) Handles rmSend.Click
    '    'Try
    '    '    Dim repotype As String = ""
    '    '    Dim invtype As String = ""
    '    '    If Gv1.Rows.Count <= 0 Then
    '    '        clsCommon.MyMessageBoxShow("No Data Found To Send Mail", Me.Text)
    '    '        Return
    '    '    End If

    '    '    Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(clsUserMgtCode.RptFreshSaleRegister1)

    '    '    If obj Is Nothing Then
    '    '        clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
    '    '        Return
    '    '    End If
    '    '    If clsCommon.myLen(obj.mailsubjct) <= 0 Then
    '    '        clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
    '    '        Return
    '    '    End If

    '    '    Try

    '    '        Dim strEmail As String = ""


    '    '        If Process.GetProcessesByName("OutLook").Length < 1 Then
    '    '            'restarts the Process
    '    '            Process.Start("OutLook.exe")
    '    '        End If
    '    '        Dim oApp As New Outlook.Application()
    '    '        Dim oMsg As Outlook.MailItem

    '    '        'If chkAll.IsChecked Then
    '    '        '    invtype = ""
    '    '        'ElseIf chkTax.IsChecked Then
    '    '        '    invtype = "Tax Invoice"
    '    '        'ElseIf chkRetail.IsChecked Then
    '    '        '    invtype = "Retail Invoice"
    '    '        'End If

    '    '        If rdbDetail.IsChecked Then
    '    '            repotype = "Detail Report"
    '    '        Else
    '    '            repotype = "Summary Report"
    '    '        End If

    '    '        oMsg = DirectCast(oApp.CreateItem(Outlook.OlItemType.olMailItem), Outlook.MailItem)
    '    '        strEmail = clsDBFuncationality.getSingleValue("select distinct (select ','+Email_id from tspl_employee_master where emp_code in ('" & obj.usercode & "') for xml path('')) ")

    '    '        Try
    '    '            If strEmail.Substring(0, 1) = "," Then
    '    '                strEmail = strEmail.Substring(1, strEmail.Length - 1)
    '    '            End If
    '    '        Catch ex As Exception
    '    '        End Try

    '    '        If clsCommon.myLen(strEmail) <= 0 Then
    '    '            clsCommon.MyMessageBoxShow("No Mail ID Found for Sending Mail,Please Fill E-Mail Id In Employee Master", Me.Text)
    '    '            Return
    '    '        End If

    '    '        oMsg.Body = obj.mailbody

    '    '        oMsg.Body = oMsg.Body.Replace("'", " ").Replace("`", "/")

    '    '        If oMsg.Body.Contains(clsEmailSMSConstants.FromDate) Then
    '    '            oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.FromDate, clsCommon.GetPrintDate(fromDate.Text, "dd/MMM/yyyy"))
    '    '        End If
    '    '        If oMsg.Body.Contains(clsEmailSMSConstants.ToDate) Then
    '    '            oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.ToDate, clsCommon.GetPrintDate(ToDate.Text, "dd/MMM/yyyy"))
    '    '        End If
    '    '        If oMsg.Body.Contains(clsEmailSMSConstants.ReportType) Then
    '    '            oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.ReportType, repotype)
    '    '        End If
    '    '        If oMsg.Body.Contains(clsEmailSMSConstants.InvoiceType) Then
    '    '            oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.InvoiceType, invtype)
    '    '        End If


    '    '        oMsg.Subject = obj.mailsubjct

    '    '        oMsg.Subject = oMsg.Subject.Replace("'", " ").Replace("`", "/")

    '    '        If oMsg.Subject.Contains(clsEmailSMSConstants.FromDate) Then
    '    '            oMsg.Subject = oMsg.Subject.Replace(clsEmailSMSConstants.FromDate, clsCommon.GetPrintDate(fromDate.Text, "dd/MMM/yyyy"))
    '    '        End If
    '    '        If oMsg.Subject.Contains(clsEmailSMSConstants.ToDate) Then
    '    '            oMsg.Subject = oMsg.Subject.Replace(clsEmailSMSConstants.ToDate, clsCommon.GetPrintDate(ToDate.Text, "dd/MMM/yyyy"))
    '    '        End If
    '    '        If oMsg.Subject.Contains(clsEmailSMSConstants.ReportType) Then
    '    '            oMsg.Subject = oMsg.Subject.Replace(clsEmailSMSConstants.ReportType, repotype)
    '    '        End If
    '    '        If oMsg.Subject.Contains(clsEmailSMSConstants.InvoiceType) Then
    '    '            oMsg.Subject = oMsg.Subject.Replace(clsEmailSMSConstants.InvoiceType, invtype)
    '    '        End If

    '    '        '------------------------code for attchament-------------------------------------
    '    '        If obj.atchmnt = "Y" Then
    '    '            Dim sDisplayname As [String] = "MyAttachment"
    '    '            If oMsg.Body Is Nothing Then
    '    '                oMsg.Body = " "
    '    '            End If
    '    '            Dim iPosition As Integer = CInt(oMsg.Body.Length) + 1
    '    '            Dim iAtchmentType As Integer = CInt(Outlook.OlAttachmentType.olByValue)

    '    '            Dim strRptPath As String = ""

    '    '            Dim oAttachment As Outlook.Attachment = Nothing
    '    '            Dim dt As DataTable = clsDBFuncationality.GetDataTable(atchqry)

    '    '            If dt.Rows.Count > 0 Then
    '    '                Dim subPath As String = Application.StartupPath + "\Mail Reports"

    '    '                Dim IsExists As Boolean = System.IO.Directory.Exists(subPath)

    '    '                If (IsExists = False) Then

    '    '                    System.IO.Directory.CreateDirectory(subPath)
    '    '                End If
    '    '                strRptPath = Application.StartupPath + "\Mail Reports\Sale Register.xls"
    '    '                transportSql.exportdata(Gv1, strRptPath, "Sheet1")
    '    '                oAttachment = oMsg.Attachments.Add(strRptPath, iAtchmentType, iPosition, sDisplayname)
    '    '            End If
    '    '        End If
    '    '        '---------------------------------------------------------------------------


    '    '        oMsg.Recipients.Add(strEmail)
    '    '        oMsg.CC = "ranjana.sinha@tecxpert.in;rakesh.sharma@tecxpert.in"
    '    '        oMsg.Send()
    '    '        oMsg = Nothing
    '    '        oApp = Nothing

    '    '        clsCommon.MyMessageBoxShow("E-Mail Send Successfully", Me.Text)
    '    '    Catch ex As Exception
    '    '        Throw New Exception(ex.Message)
    '    '    End Try

    '    '    Try
    '    '        Dim client As New System.Net.WebClient()

    '    '        If clsCommon.myLen(obj.smsbody) <= 0 Then
    '    '            Throw New Exception("Please Set First SMS Body In SMS/Email Setting")
    '    '        End If

    '    '        Dim strMes As String = ""

    '    '        strMes = obj.smsbody
    '    '        strMes = strMes.Replace("'", " ").Replace("`", "/")

    '    '        If strMes.Contains(clsEmailSMSConstants.FromDate) Then
    '    '            strMes = strMes.Replace(clsEmailSMSConstants.FromDate, clsCommon.GetPrintDate(fromDate.Text, "dd/MMM/yyyy"))
    '    '        End If
    '    '        If strMes.Contains(clsEmailSMSConstants.ToDate) Then
    '    '            strMes = strMes.Replace(clsEmailSMSConstants.ToDate, clsCommon.GetPrintDate(ToDate.Text, "dd/MMM/yyyy"))
    '    '        End If
    '    '        If strMes.Contains(clsEmailSMSConstants.ReportType) Then
    '    '            strMes = strMes.Replace(clsEmailSMSConstants.ReportType, repotype)
    '    '        End If
    '    '        If strMes.Contains(clsEmailSMSConstants.InvoiceType) Then
    '    '            strMes = strMes.Replace(clsEmailSMSConstants.InvoiceType, invtype)
    '    '        End If


    '    '        Dim strphone As String = clsDBFuncationality.getSingleValue("select distinct (select ','+Phone from tspl_employee_master where emp_code in ('" & obj.usercode & "') for xml path(''))  ")

    '    '        Try
    '    '            If strphone.Substring(0, 1) = "," Then
    '    '                strphone = strphone.Substring(1, strphone.Length - 1)
    '    '            End If
    '    '        Catch ex As Exception
    '    '        End Try

    '    '        'Dim baseurl As String = "http://bulksms.mysmsmantra.com:8080/WebSMS/SMSAPI.jsp?username=tecxpert&password=1818948263&sendername=vipin&mobileno=91" + strphone + "&message=" + strMes
    '    '        'Dim data As Stream = client.OpenRead(baseurl)
    '    '        'Dim reader As StreamReader = New StreamReader(data)
    '    '        'Dim s As String = reader.ReadToEnd()
    '    '        'data.Close()
    '    '        'reader.Close()


    '    '        Dim UserId As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.SMS_User_Name, clsFixedParameterCode.MilkSetting, Nothing))
    '    '        Dim Paswd As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.SMS_User_PWD, clsFixedParameterCode.MilkSetting, Nothing))
    '    '        Dim SenderId As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.SMS_Sendor_ID, clsFixedParameterCode.MilkSetting, Nothing))
    '    '        Dim SMS_Provider As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.SMS_Provider, clsFixedParameterCode.MilkSetting, Nothing))

    '    '        If clsCommon.CompairString(SMS_Provider, "Bulk SMS") = CompairStringResult.Equal Then
    '    '            '================send sms through PerfectBulkSMS====================
    '    '            Dim encode As System.Text.Encoding = System.Text.Encoding.GetEncoding("utf-8")
    '    '            Dim str As String = "http://www.perfectbulksms.in/Sendsmsapi.aspx?USERID=" + UserId + "&PASSWORD=" + Paswd + "&SENDERID=" + SenderId + "&TO=" & strphone & "&MESSAGE=" & strMes & ""
    '    '            Dim wrquest As HttpWebRequest = WebRequest.Create(str)
    '    '            Dim getresponse As HttpWebResponse = Nothing
    '    '            getresponse = wrquest.GetResponse()

    '    '            Dim objStream As Stream = getresponse.GetResponseStream()
    '    '            Dim objSR As StreamReader = New StreamReader(objStream, encode, True)
    '    '            Dim strResponse As String = objSR.ReadToEnd()
    '    '            'clsCommon.MyMessageBoxShow(getresponse.StatusDescription)

    '    '            objSR.Close()
    '    '            objStream.Close()
    '    '            getresponse.Close()
    '    '            '===========================================================
    '    '        ElseIf clsCommon.CompairString(SMS_Provider, "BSWS") = CompairStringResult.Equal Then
    '    '            Dim consumeWebService As BSWS.BSWS
    '    '            consumeWebService = New BSWS.BSWS
    '    '            Dim xmlResult As XmlElement
    '    '            xmlResult = consumeWebService.SubmitSMS("prashant@tecxpert.in", "tecxpert", strphone, strMes, "", 0, "TSPLSW", "")
    '    '        End If

    '    '        clsCommon.MyMessageBoxShow("SMS Send Successfully", Me.Text)
    '    '    Catch ex As Exception
    '    '        Throw New Exception(ex.Message)
    '    '    End Try
    '    'Catch ex As Exception
    '    '    clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
    '    'End Try

    'End Sub

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
        'Ticket No-ERO/01/07/19-000667 sanjay
        Try
            'DrillDown()
            If clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentColumn.HeaderText), "january") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentColumn.HeaderText), "february") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentColumn.HeaderText), "march") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentColumn.HeaderText), "april") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentColumn.HeaderText), "may") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentColumn.HeaderText), "june") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentColumn.HeaderText), "july") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentColumn.HeaderText), "august") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentColumn.HeaderText), "september") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentColumn.HeaderText), "october") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentColumn.HeaderText), "november") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentColumn.HeaderText), "december") = CompairStringResult.Equal Then
                Dim StrMonth As String = clsCommon.myCstr(returnMonthNumber(clsCommon.myCstr(Gv1.CurrentColumn.HeaderText)))
                Dim StrYear As String = clsCommon.myCstr(Gv1.CurrentRow.Cells("YEAR").Value)
                MISSaleRegisterDrillDown(StrMonth, StrYear)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Sub MISSaleRegisterDrillDown(ByVal StrMonth As String, ByVal StrYear As String)
        Try
            Dim StartDate As Date = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select DATEADD(m," + StrMonth + " - 1, DATEADD(yyyy," + StrYear + " - 1900, 0))"))
            Dim EndDate As Date = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select DATEADD(d, -1, DATEADD(m," + StrMonth + ", DATEADD(yyyy," + StrYear + "- 1900, 0)))"))
            If StartDate < fromDate.Value Then
                StartDate = fromDate.Value
            End If
            If EndDate > ToDate.Value Then
                EndDate = ToDate.Value
            End If
            Dim frm As RptSaleRegisterReport
            frm = New RptSaleRegisterReport(clsUserMgtCode.MISSaleRegister)
            frm.isReadFlag = True
            frm.isDataLoad = True
            frm.dtFrom = StartDate
            frm.dtTo = EndDate
            frm.Unit_Code = txtUOM.Value
            frm.arrTransaction = txtTransaction.arrValueMember
            frm.arrItem = txtItem.arrValueMember
            frm.arrItemGroup = txtItemGroup.arrValueMember
            frm.arrLocation = txtLocation.arrValueMember
            frm.arrCustomer = txtCustomer.arrValueMember
            frm.arrCustGroup = txtCustGroup.arrValueMember
            frm.arrState = txtState.arrValueMember
            frm.arrCat = New Dictionary(Of String, Object)
            Dim arrSel As Dictionary(Of String, Object) = Nothing
            Dim TempCode As String = ""
            Dim dtCategory As DataTable

            dtCategory = clsDBFuncationality.GetDataTable("select ITEM_CATEGORY_CODE AS CodeColumn,ITEM_CATEGORY_CODE+' Description' as CodeDescColumn,DESCRIPTION as DescColumn  from TSPL_ITEM_CATEGORY_LEVEL order by CATEGORY_LEVEL")
            If dtCategory IsNot Nothing AndAlso dtCategory.Rows.Count > 0 Then
                For ii As Integer = 0 To dtCategory.Rows.Count - 1
                    If clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells(clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")).Trim()).Value), "") = CompairStringResult.Equal Then
                        Exit For
                    End If
                    arrSel = New Dictionary(Of String, Object)
                    TempCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE from TSPL_ITEM_CATEGORY_LEVEL_VALUES where TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE='" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "' AND TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION='" + clsCommon.myCstr(Gv1.CurrentRow.Cells(clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")).Trim()).Value) + "'"))
                    arrSel.Add(TempCode, Nothing)
                    frm.arrCat.Add(clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim(), arrSel)
                Next
            End If


            frm.strType = "Document Detail"
            frm.WindowState = FormWindowState.Maximized
            frm.Focus()
            frm.Visible = False
            frm.Show()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub


    Public Function returnMonthNumber(ByVal monthName As String) As Integer
        Select Case monthName.ToLower
            Case Is = "january"
                Return 1
            Case Is = "february"
                Return 2
            Case Is = "march"
                Return 3
            Case Is = "april"
                Return 4
            Case Is = "may"
                Return 5
            Case Is = "june"
                Return 6
            Case Is = "july"
                Return 7
            Case Is = "august"
                Return 8
            Case Is = "september"
                Return 9
            Case Is = "october"
                Return 10
            Case Is = "november"
                Return 11
            Case Is = "december"
                Return 12
            Case Else
                Return 0
        End Select
    End Function

    Sub DrillDown()
        Try
            Print(Exporter.Refresh, "Y")
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
        Try
            If e.Control And e.KeyCode = Keys.D Then
                'DrillDown()
                If clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentColumn.HeaderText), "january") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentColumn.HeaderText), "february") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentColumn.HeaderText), "march") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentColumn.HeaderText), "april") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentColumn.HeaderText), "may") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentColumn.HeaderText), "june") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentColumn.HeaderText), "july") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentColumn.HeaderText), "august") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentColumn.HeaderText), "september") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentColumn.HeaderText), "october") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentColumn.HeaderText), "november") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentColumn.HeaderText), "december") = CompairStringResult.Equal Then
                    Dim StrMonth As String = clsCommon.myCstr(returnMonthNumber(clsCommon.myCstr(Gv1.CurrentColumn.HeaderText)))
                    Dim StrYear As String = clsCommon.myCstr(Gv1.CurrentRow.Cells("YEAR").Value)
                    MISSaleRegisterDrillDown(StrMonth, StrYear)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    'Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
    '    Try
    '        If clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Total Sale") = CompairStringResult.Equal Then

    '        ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Location Wise") = CompairStringResult.Equal AndAlso arrBack.Contains("Total Sale") Then
    '            arrBack.Remove("Total Sale")
    '            ddlReportType.SelectedValue = "Total Sale"
    '            'txtLocation.arrValueMember = arrLocation
    '            Print(Exporter.Refresh)
    '        ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Item Group Wise") = CompairStringResult.Equal AndAlso arrBack.Contains("Location Wise") Then
    '            arrBack.Remove("Location Wise")
    '            ddlReportType.SelectedValue = "Location Wise"
    '            txtLocation.arrValueMember = arrLocation
    '            Print(Exporter.Refresh)
    '        ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Customer Group Wise") = CompairStringResult.Equal AndAlso arrBack.Contains("Item Group Wise") Then
    '            arrBack.Remove("Item Group Wise")
    '            ddlReportType.SelectedValue = "Item Group Wise"
    '            txtItemGroup.arrValueMember = arrItemGroup
    '            Print(Exporter.Refresh)
    '        ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Item Wise") = CompairStringResult.Equal AndAlso arrBack.Contains("Customer Group Wise") Then
    '            arrBack.Remove("Customer Group Wise")
    '            ddlReportType.SelectedValue = "Customer Group Wise"
    '            txtCustGroup.arrValueMember = arrCustGroup
    '            Print(Exporter.Refresh)
    '        ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Customer Wise") = CompairStringResult.Equal AndAlso arrBack.Contains("Item Wise") Then
    '            arrBack.Remove("Item Wise")
    '            ddlReportType.SelectedValue = "Item Wise"
    '            txtItem.arrValueMember = arrItem
    '            Print(Exporter.Refresh)
    '        ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Document Wise") = CompairStringResult.Equal AndAlso arrBack.Contains("Customer Wise") Then
    '            arrBack.Remove("Customer Wise")
    '            ddlReportType.SelectedValue = "Customer Wise"
    '            txtCustomer.arrValueMember = arrCustomer
    '            Print(Exporter.Refresh)
    '        ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Document Detail") = CompairStringResult.Equal AndAlso arrBack.Contains("Document Wise") Then
    '            arrBack.Remove("Document Wise")
    '            ddlReportType.SelectedValue = "Document Wise"
    '            Document_No = Document_No_Old
    '            'txtCustomer.arrValueMember = arrCustomer
    '            Print(Exporter.Refresh)
    '        End If
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
    '    End Try
    'End Sub

    Private Sub txtCustGroup__My_Click(sender As Object, e As EventArgs) Handles txtCustGroup._My_Click
        Dim qry As String = " select Cust_Group_Code as Code,Cust_Group_Desc as Name from TSPL_CUSTOMER_GROUP_MASTER"
        txtCustGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("CustGroupMulSel", qry, "Code", "Name", txtCustGroup.arrValueMember, txtCustGroup.arrDispalyMember)
    End Sub

    Private Sub txtState__My_Click(sender As Object, e As EventArgs) Handles txtState._My_Click
        Dim qry As String = " select STATE_CODE as Code,STATE_NAME as Name from TSPL_STATE_MASTER"
        txtState.arrValueMember = clsCommon.ShowMultipleSelectForm("StateMulSel", qry, "Code", "Name", txtState.arrValueMember, txtState.arrDispalyMember)
    End Sub

    'Private Sub btnBulkExport_Click(sender As Object, e As EventArgs)
    '    'BulkExport()
    'End Sub
    'Sub BulkExport(ByVal FormatType As String)
    '    Try
    '        clsCommon.ProgressBarPercentShow()
    '        clsCommon.ProgressBarPercentUpdate(0, "Generating query for the report..")
    '        Dim obj As clsSaleRegisterParameterType = ReturnFilterData()
    '        Dim qry As String = clsPSInvoiceHead.GetReportDataQuery(obj)

    '        clsCommon.ProgressBarPercentUpdate(10, "Query generated..starting bulk export..")
    '        If ddlReportType.SelectedValue = "Total Sale" Then
    '            qry = "select * from (" & qry & ") PP order by [Total FAT KG]"
    '            transportSql.BulkExport("Sale_Register", qry, "order by [Total FAT KG]", FormatType)
    '            Exit Sub
    '        ElseIf ddlReportType.SelectedValue = "Location Wise" Then
    '            qry = "select * from (" & qry & ") PP order by [Location Code],[Location Name]"
    '            transportSql.BulkExport("Sale_Register", qry, "order by [Location Code],[Location Name]", FormatType)
    '            Exit Sub
    '        ElseIf ddlReportType.SelectedValue = "Item Group Wise" Then
    '            qry = "select * from (" & qry & ") PP order by [Location Code],[Location Name],[Item Group Code],[Item Group Description]"
    '            transportSql.BulkExport("Sale_Register", qry, "order by [Location Code],[Location Name],[Item Group Code],[Item Group Description]", FormatType)
    '            Exit Sub
    '        ElseIf ddlReportType.SelectedValue = "Customer Group Wise" Then
    '            qry = "select * from (" & qry & ") PP order by [Location Code],[Location Name],[Item Group Code],[Item Group Description],[Customer Group Code],[Customer Group Description]"
    '            transportSql.BulkExport("Sale_Register", qry, "order by [Location Code],[Location Name],[Item Group Code],[Item Group Description],[Customer Group Code],[Customer Group Description]", FormatType)
    '            Exit Sub
    '        ElseIf ddlReportType.SelectedValue = "Item Wise" Then
    '            qry = "select * from (" & qry & ") PP order by [Location Code],[Location Name],[Item Group Code],[Item Group Description],[Customer Group Code],[Customer Group Description],[Item Code],[Item Name]"
    '            transportSql.BulkExport("Sale_Register", qry, " order by [Location Code],[Location Name],[Item Group Code],[Item Group Description],[Customer Group Code],[Customer Group Description],[Item Code],[Item Name]", FormatType)
    '            Exit Sub
    '        ElseIf ddlReportType.SelectedValue = "Customer Wise" Then
    '            qry = "select * from (" & qry & ") PP order by [Location Code],[Location Name],[Item Group Code],[Item Group Description],[Customer Group Code],[Customer Group Description],[Item Code],[Item Name],[Customer Code],[Customer Name]"
    '            transportSql.BulkExport("Sale_Register", qry, "order by [Location Code],[Location Name],[Item Group Code],[Item Group Description],[Customer Group Code],[Customer Group Description],[Item Code],[Item Name],[Customer Code],[Customer Name]", FormatType)
    '            Exit Sub
    '        ElseIf ddlReportType.SelectedValue = "Document Wise" Then

    '            transportSql.BulkExport("Sale_Register", qry, "order by convert(date,[Document_Date],103),[Document No]", FormatType)
    '            Exit Sub
    '        ElseIf ddlReportType.SelectedValue = "Document Detail" Then
    '            transportSql.BulkExport("Sale_Register", qry, "order by convert(date,[Document_Date],103),[Document No]", FormatType)
    '            Exit Sub
    '        ElseIf obj.ReportType = "Document Info Level" Then
    '            transportSql.BulkExport("Sale_Register", qry, "order by convert(date,[Document_Date],103),[Document No]", FormatType)
    '            Exit Sub
    '        End If


    '        clsCommon.ProgressBarPercentHide()
    '        clsCommon.MyMessageBoxShow("Data exported successfully")
    '    Catch ex As Exception
    '        clsCommon.ProgressBarPercentHide()
    '        clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
    '    Finally
    '        clsCommon.ProgressBarPercentHide()
    '    End Try
    'End Sub

    'Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles BulkExportCsv.Click
    '    BulkExport("csv")
    'End Sub

    'Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
    '    If (Gv1.Rows.Count <= 0) Then
    '        common.clsCommon.MyMessageBoxShow("No Data To Export")
    '        Exit Sub
    '    End If
    '    Print(Exporter.Excel)
    '    'RadPivotGrid1.E()
    'End Sub

    'Private Sub RadMenuItem3_Click(sender As Object, e As EventArgs) Handles BulkExportXls.Click
    '    BulkExport("xls")
    'End Sub


    Private Sub FrmMonthlySaleReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
    End Sub

    Private Sub FrmMonthlySaleReport_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.R Then
            Print(Exporter.Refresh)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub

    Private Sub btnQuickExport_DragDrop(sender As Object, e As DragEventArgs)

    End Sub

 
  
    Private Sub ExpExcel_Click(sender As Object, e As EventArgs) Handles ExpExcel.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmMonthlySaleReport & "'"))
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
         
            Dim Export As New ExportToExcelML(Gv1)
            Export.RunExport(filePath)
            common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            Process.Start(filePath)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub PDF_Click(sender As Object, e As EventArgs) Handles PDF.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmMonthlySaleReport & "'"))

            If clsCommon.myLen(ddlReportType.Text) > 0 Then
                arrHeader.Add("Report Type : " + ddlReportType.Text)
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

            If txtCustGroup.arrDispalyMember IsNot Nothing AndAlso txtCustGroup.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Customer Group : " + clsCommon.GetMulcallStringWithComma(txtCustGroup.arrDispalyMember))
            End If
            If txtCustomer.arrDispalyMember IsNot Nothing AndAlso txtCustomer.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Customer : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember))
            End If

            clsCommon.MyExportToPDF("Sale Report", Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class
