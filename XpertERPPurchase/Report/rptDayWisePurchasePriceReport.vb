Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms

Imports System.IO
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports common

Public Class RptDayWisePurchasePriceReport
    Inherits FrmMainTranScreen

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnPrint.Visible = MyBase.isModifyFlag
        btnExport.Visible = MyBase.isExport
    End Sub

    Private Sub RptDayWisePurchasePriceReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        FillYear()
        LoadTypes()
        LoadMonthlyReportType()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Public Sub FillYear()
        Dim qry As String = " select convert (varchar ,min( datepart(year,SRN_Date)) -1) +' - '+ convert (varchar ,min( datepart(year,SRN_Date))-1 +1 ) as FiscalYear , convert (varchar ,min( datepart(year,SRN_Date)) -1) as Year from  TSPL_SRN_HEAD  union all  select distinct convert (varchar, datepart(year,SRN_Date) ) +' - '+  convert (varchar, datepart(year,SRN_Date) +1 ) as FiscalYear , convert (varchar, datepart(year,SRN_Date) ) as Year  from TSPL_SRN_HEAD "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            cboYear.DataSource = Nothing
            cboYear.DataSource = dt
            cboYear.ValueMember = "Year"
            cboYear.DisplayMember = "Year"
        End If
    End Sub

    Sub LoadTypes()
        Dim dt As DataTable
        dt = New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Rows.Add("January")
        dt.Rows.Add("February")
        dt.Rows.Add("March")
        dt.Rows.Add("April")
        dt.Rows.Add("May")
        dt.Rows.Add("June")
        dt.Rows.Add("July")
        dt.Rows.Add("August")
        dt.Rows.Add("September")
        dt.Rows.Add("October")
        dt.Rows.Add("November")
        dt.Rows.Add("December")
        cboMonthName.DataSource = dt
        cboMonthName.ValueMember = "Code"
        cboMonthName.DisplayMember = "Code"
    End Sub

    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Dim qry As String = "select item_Code as Code,Item_Desc as Name from  TSPL_ITEM_MASTER  "
        txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtItem.arrValueMember, txtItem.arrDispalyMember)
    End Sub
    Sub LoadMonthlyReportType()
        Dim dt As DataTable
        dt = New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Rows.Add("Qty Wise")
        dt.Rows.Add("Value Wise")
        dt.Rows.Add("Both")
        cboQtyValueWise.DataSource = dt
        cboQtyValueWise.ValueMember = "Code"
        cboQtyValueWise.DisplayMember = "Code"
    End Sub

    Sub Reset()
        
        FillYear()
        txtItem.arrValueMember = Nothing
        gv.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnreset_Click(sender As Object, e As EventArgs) Handles btnreset.Click
        Reset()
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        PageSetupReport_ID = MyBase.Form_ID
        PrintData(Nothing)
    End Sub
    Sub PrintData(ByVal exporter As EnumExportTo)
        Try
            Dim checkData As Integer = clsDBFuncationality.getSingleValue(" select count(*) from TSPL_SRN_HEAD where  datename(month,convert(date, SRN_Date,103)) ='" + cboMonthName.Text + "' and datename(year,convert(date, SRN_Date,103))  = '" + cboYear.SelectedValue + "'")
            
            If checkData <= 0 Then
                clsCommon.MyMessageBoxShow("No Data Found")
                Exit Sub
            End If

            gv.MasterTemplate.SummaryRowsBottom.Clear()
            Dim qryQty As String = ""
            Dim qryValue As String = ""
            Dim qryBoth As String = ""
            Dim qryFinal As String = ""
            Dim whrcate As String = ""
            Dim colDate As String = ""
            Dim colDateWithNullWithFinal As String = ""
            Dim colDateWithNullSumWithFinal As String = ""
            Dim colDatforSummery As String = ""
            Dim colDateForZero As String = ""
            Dim colFooterSummeryQty As String = ""
            Dim colFooterSummeryValue As String = ""
            Dim colFooterSummeryBoth As String = ""

            Dim colDateValue As String = ""
            Dim colDateWithNullWithFinalValue As String = ""
            Dim colDateWithNullSumWithFinalValue As String = ""
            Dim colDatforSummeryValue As String = ""
            Dim colDateForZeroValue As String = ""
            Dim colDateForFinalResult As String = ""

            Dim colCategoryNameDesc As String = ""
            Dim colCategoryNameDescWithFinal As String = ""
            Dim colCategoryNameDescWithNull As String = ""
            Dim colCategoryNameWithMax As String = ""
            Dim colCategoryNameWithMaxForFinalResult As String = ""
            Dim colDateForFinalResultValue As String = ""
            Dim colDateWithQtyValueOrder As String = ""

            Dim ItemArrForQty As String = ""
            Dim ItemArrForValue As String = ""
            Dim ItemArrForBoth As String = ""
            If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                ItemArrForBoth = " where  FinalResult.Item_Code in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ") " + Environment.NewLine
                ItemArrForQty = " where  Final.Item_Code in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ") " + Environment.NewLine
                ItemArrForValue = " where Final.Item_Code in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ") " + Environment.NewLine
            End If

            colDate = clsDBFuncationality.getSingleValue(" Select   STUFF((SELECT ',' +'['+aa.SRN_Date+'-Qty]' from (select distinct convert(varchar,SRN_Date,103) SRN_Date FROM TSPL_SRN_HEAD where datename(month,convert(date, SRN_Date,103)) ='" + cboMonthName.Text + "' and datename(year,convert(date, SRN_Date,103))  = '" + cboYear.SelectedValue + "')aa order by convert(date,aa.SRN_Date,103)  FOR XML PATH ('')), 1, 1, '') ")
            colDateWithNullWithFinal = clsDBFuncationality.getSingleValue(" Select  STUFF((SELECT ',' +'isnull (' +'final.['+aa.SRN_Date+'-Qty]' +',0 ) as ' +'['+aa.SRN_Date+'-Qty]'  from (select distinct convert(varchar,SRN_Date,103) SRN_Date FROM TSPL_SRN_HEAD where datename(month,convert(date, SRN_Date,103)) ='" + cboMonthName.Text + "' and datename(year,convert(date, SRN_Date,103))  = '" + cboYear.SelectedValue + "')aa order by convert(date,aa.SRN_Date,103)  FOR XML PATH ('')), 1, 1, '') ")
            colDateWithNullSumWithFinal = clsDBFuncationality.getSingleValue(" Select  STUFF((SELECT '+' +'isnull(final.' +'['+aa.SRN_Date+'-Qty],0)' from (select distinct convert(varchar,SRN_Date,103) SRN_Date FROM TSPL_SRN_HEAD where datename(month,convert(date, SRN_Date,103)) ='" + cboMonthName.Text + "' and datename(year,convert(date, SRN_Date,103))  = '" + cboYear.SelectedValue + "')aa order by convert(date,aa.SRN_Date,103)  FOR XML PATH ('')), 1, 1, '') ")
            colDatforSummery = clsDBFuncationality.getSingleValue(" Select  STUFF((SELECT ','+aa.SRN_Date+'-Qty' from (select distinct convert(varchar,SRN_Date,103) SRN_Date FROM TSPL_SRN_HEAD where datename(month,convert(date, SRN_Date,103)) ='" + cboMonthName.Text + "' and datename(year,convert(date, SRN_Date,103))  = '" + cboYear.SelectedValue + "')aa order by convert(date,aa.SRN_Date,103)  FOR XML PATH ('')), 1, 1, '') ")
            colDateForZero = clsDBFuncationality.getSingleValue(" Select   STUFF((SELECT ',' +'0 as ['+aa.SRN_Date+'-Qty]' from (select distinct convert(varchar,SRN_Date,103) SRN_Date FROM TSPL_SRN_HEAD where datename(month,convert(date, SRN_Date,103)) ='" + cboMonthName.Text + "' and datename(year,convert(date, SRN_Date,103))  ='" + cboYear.SelectedValue + "')aa order by convert(date,aa.SRN_Date,103)  FOR XML PATH ('')), 1, 1, '') ")

            colDateForFinalResult = clsDBFuncationality.getSingleValue(" Select  STUFF((SELECT ',' +'sum(isnull(FinalResult.' +'['+aa.SRN_Date+'-Qty],0)) as ' + '['+aa.SRN_Date+'-Qty]' from (select distinct convert(varchar,SRN_Date,103) SRN_Date FROM TSPL_SRN_HEAD where datename(month,convert(date, SRN_Date,103)) ='" + cboMonthName.Text + "' and datename(year,convert(date, SRN_Date,103))  = '" + cboYear.SelectedValue + "')aa order by convert(date,aa.SRN_Date,103)  FOR XML PATH ('')), 1, 1, '') ")
            colDateWithQtyValueOrder = clsDBFuncationality.getSingleValue(" Select  STUFF((SELECT ',' +'sum(isnull(FinalResult.' +'['+aa.SRN_Date+'-Qty],0)) as ' + '['+aa.SRN_Date+'-Qty]' + ',' +'sum(isnull(FinalResult.' +'['+aa.SRN_Date+'-Value],0)) as ' + '['+aa.SRN_Date+'-Value]' from (select distinct convert(varchar,SRN_Date,103) SRN_Date FROM TSPL_SRN_HEAD where datename(month,convert(date, SRN_Date,103)) ='" + cboMonthName.Text + "' and datename(year,convert(date, SRN_Date,103))  = '" + cboYear.SelectedValue + "')aa order by convert(date,aa.SRN_Date,103)  FOR XML PATH ('')), 1, 1, '') ")

            colDateValue = clsDBFuncationality.getSingleValue(" Select   STUFF((SELECT ',' +'['+aa.SRN_Date+'-Value]' from (select distinct convert(varchar,SRN_Date,103) SRN_Date FROM TSPL_SRN_HEAD where datename(month,convert(date, SRN_Date,103)) ='" + cboMonthName.Text + "' and datename(year,convert(date, SRN_Date,103))  = '" + cboYear.SelectedValue + "')aa order by convert(date,aa.SRN_Date,103)  FOR XML PATH ('')), 1, 1, '') ")
            colDateWithNullWithFinalValue = clsDBFuncationality.getSingleValue(" Select  STUFF((SELECT ',' +'isnull (' +'final.['+aa.SRN_Date+'-Value]' +',0 ) as ' +'['+aa.SRN_Date+'-Value]'  from (select distinct convert(varchar,SRN_Date,103) SRN_Date FROM TSPL_SRN_HEAD where datename(month,convert(date, SRN_Date,103)) ='" + cboMonthName.Text + "' and datename(year,convert(date, SRN_Date,103))  = '" + cboYear.SelectedValue + "')aa order by convert(date,aa.SRN_Date,103)  FOR XML PATH ('')), 1, 1, '') ")
            colDateWithNullSumWithFinalValue = clsDBFuncationality.getSingleValue(" Select  STUFF((SELECT '+' +'isnull(final.' +'['+aa.SRN_Date+'-Value],0)' from (select distinct convert(varchar,SRN_Date,103) SRN_Date FROM TSPL_SRN_HEAD where datename(month,convert(date, SRN_Date,103)) ='" + cboMonthName.Text + "' and datename(year,convert(date, SRN_Date,103))  = '" + cboYear.SelectedValue + "')aa order by convert(date,aa.SRN_Date,103)  FOR XML PATH ('')), 1, 1, '') ")
            colDatforSummeryValue = clsDBFuncationality.getSingleValue(" Select  STUFF((SELECT ','+aa.SRN_Date+'-Value' from (select distinct convert(varchar,SRN_Date,103) SRN_Date FROM TSPL_SRN_HEAD where datename(month,convert(date, SRN_Date,103)) ='" + cboMonthName.Text + "' and datename(year,convert(date, SRN_Date,103))  = '" + cboYear.SelectedValue + "')aa order by convert(date,aa.SRN_Date,103)  FOR XML PATH ('')), 1, 1, '') ")
            colDateForZeroValue = clsDBFuncationality.getSingleValue(" Select   STUFF((SELECT ',' +'0 as ['+aa.SRN_Date+'-Value]' from (select distinct convert(varchar,SRN_Date,103) SRN_Date FROM TSPL_SRN_HEAD where datename(month,convert(date, SRN_Date,103)) ='" + cboMonthName.Text + "' and datename(year,convert(date, SRN_Date,103))  ='" + cboYear.SelectedValue + "')aa order by convert(date,aa.SRN_Date,103)  FOR XML PATH ('')), 1, 1, '')  ")
            colDateForFinalResultValue = clsDBFuncationality.getSingleValue("   Select  STUFF((SELECT ',' +'sum(isnull(FinalResult.' +'['+aa.SRN_Date+'-Value],0)) as ' + '['+aa.SRN_Date+'-Value]' from (select distinct convert(varchar,SRN_Date,103) SRN_Date FROM TSPL_SRN_HEAD where datename(month,convert(date, SRN_Date,103)) ='" + cboMonthName.Text + "' and datename(year,convert(date, SRN_Date,103))  = '" + cboYear.SelectedValue + "')aa order by convert(date,aa.SRN_Date,103)  FOR XML PATH ('')), 1, 1, '') ")

            colCategoryNameDesc = clsDBFuncationality.getSingleValue(" Select  STUFF((SELECT ',' + +'['+aa.ITEM_CATEGORY_CODE+']' from (select distinct ITEM_CATEGORY_CODE,CATEGORY_LEVEL FROM TSPL_ITEM_CATEGORY_LEVEL )aa order by aa.CATEGORY_LEVEL  FOR XML PATH ('')), 1, 1, '')")
            colCategoryNameDescWithFinal = clsDBFuncationality.getSingleValue(" Select  STUFF((SELECT ',' + +'final.['+aa.ITEM_CATEGORY_CODE+']' from (select distinct ITEM_CATEGORY_CODE,CATEGORY_LEVEL FROM TSPL_ITEM_CATEGORY_LEVEL )aa order by aa.CATEGORY_LEVEL  FOR XML PATH ('')), 1, 1, '')")
            colCategoryNameDescWithNull = clsDBFuncationality.getSingleValue(" Select  STUFF((SELECT ',' + 'max(VirtualCategoryTabel.['+aa.ITEM_CATEGORY_CODE+']) as ' + '['+aa.ITEM_CATEGORY_CODE +']' from (select distinct ITEM_CATEGORY_CODE,CATEGORY_LEVEL FROM TSPL_ITEM_CATEGORY_LEVEL )aa order by aa.CATEGORY_LEVEL  FOR XML PATH ('')), 1, 1, '')")
            colCategoryNameWithMax = clsDBFuncationality.getSingleValue(" Select  STUFF((SELECT ',' + 'max(['+aa.ITEM_CATEGORY_CODE+']) as ' + '['+aa.ITEM_CATEGORY_CODE +']' from (select distinct ITEM_CATEGORY_CODE,CATEGORY_LEVEL FROM TSPL_ITEM_CATEGORY_LEVEL )aa order by aa.CATEGORY_LEVEL  FOR XML PATH ('')), 1, 1, '')")
            colCategoryNameWithMaxForFinalResult = clsDBFuncationality.getSingleValue(" Select  STUFF((SELECT ',' + 'max(FinalResult.['+aa.ITEM_CATEGORY_CODE+']) as ' + '['+aa.ITEM_CATEGORY_CODE +']' from (select distinct ITEM_CATEGORY_CODE,CATEGORY_LEVEL FROM TSPL_ITEM_CATEGORY_LEVEL )aa order by aa.CATEGORY_LEVEL  FOR XML PATH ('')), 1, 1, '')")

            colFooterSummeryQty = clsDBFuncationality.getSingleValue(" Select  STUFF((SELECT ',' +aa.SRN_Date+'-Qty' from (select distinct convert(varchar,SRN_Date,103) SRN_Date FROM TSPL_SRN_HEAD where datename(month,convert(date, SRN_Date,103)) ='" + cboMonthName.Text + "' and datename(year,convert(date, SRN_Date,103))  = '" + cboYear.SelectedValue + "')aa order by convert(date,aa.SRN_Date,103)  FOR XML PATH ('')), 1, 1, '') ")
            colFooterSummeryValue = clsDBFuncationality.getSingleValue(" Select  STUFF((SELECT ',' +aa.SRN_Date+'-Value' from (select distinct convert(varchar,SRN_Date,103) SRN_Date FROM TSPL_SRN_HEAD where datename(month,convert(date, SRN_Date,103)) ='" + cboMonthName.Text + "' and datename(year,convert(date, SRN_Date,103))  = '" + cboYear.SelectedValue + "')aa order by convert(date,aa.SRN_Date,103)  FOR XML PATH ('')), 1, 1, '') ")
            colFooterSummeryBoth = clsDBFuncationality.getSingleValue(" Select  STUFF((SELECT ',' +aa.SRN_Date+'-Qty' + ',' +aa.SRN_Date+'-Value'  from (select distinct convert(varchar,SRN_Date,103) SRN_Date FROM TSPL_SRN_HEAD where datename(month,convert(date, SRN_Date,103)) ='" + cboMonthName.Text + "' and datename(year,convert(date, SRN_Date,103))  = '" + cboYear.SelectedValue + "')aa order by convert(date,aa.SRN_Date,103)  FOR XML PATH ('')), 1, 1, '') ")


            qryQty = " select final.Item_Code,final.Item_Desc ,final.Dept_Desc, " + colCategoryNameDescWithFinal + ",final.Unit_Code, " + colDateWithNullWithFinal + " , (" + colDateWithNullSumWithFinal + " ) as Total_Qty from ( select *  from ( " & _
                                 " select  TSPL_SRN_DETAIL.Item_Code,max(TSPL_SRN_DETAIL.Item_Desc) as Item_Desc ,max(TSPL_SRN_HEAD.Dept_Desc) as Dept_Desc , " + colCategoryNameDescWithNull + ",(TSPL_SRN_DETAIL.Unit_Code) as Unit_Code, sum(SRN_Qty) as SRN_Qty ,FORMAT (convert(date, TSPL_SRN_HEAD.SRN_Date,103),'dd/MM/yyyy')+'-Qty' as SRN_Date  from TSPL_SRN_DETAIL " & _
                                 " left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No  left outer join (select Item_Code, " + colCategoryNameWithMax + " from ( select * from ( " & _
                                 " select TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values  from  TSPL_ITEM_MASTER " & _
                                 " left outer join TSPL_ITEM_MASTER_CATEGORY on  TSPL_ITEM_MASTER_CATEGORY.Item_code = TSPL_ITEM_MASTER.Item_code " & _
                                 " left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code " & _
                                 " and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values " & _
                                 " where 2=2  )xx " & _
                                 " Pivot  ( max(Item_Cagetory_Values) for Item_Category_Code   in ( " + colCategoryNameDesc + " )  ) Pivt" & _
                                 "   ) xxx group by Item_Code " & _
                                 "  ) as VirtualCategoryTabel on  VirtualCategoryTabel.Item_Code=TSPL_SRN_DETAIL.Item_Code " & _
                                 " where datename(month, TSPL_SRN_HEAD.SRN_Date) ='" + cboMonthName.Text + "' and datename(year, TSPL_SRN_HEAD.SRN_Date) ='" + cboYear.SelectedValue + "' and TSPL_SRN_HEAD.Status=1 " & _
                                 " group by  convert(date, TSPL_SRN_HEAD.SRN_Date,103) ,TSPL_SRN_DETAIL.Item_Code ,Unit_Code )xxxp " & _
                                 " pivot (sum(SRN_Qty)   for SRN_Date in ( " + colDate + " " & _
                                 " ) ) piv ) final  " + ItemArrForQty + " "

            qryValue = " select final.Item_Code,final.Item_Desc ,final.Dept_Desc, " + colCategoryNameDescWithFinal + ",final.Unit_Code,  " + colDateWithNullWithFinalValue + " , (" + colDateWithNullSumWithFinalValue + " ) as Total_Value from ( select *  from ( " & _
                  " select  TSPL_SRN_DETAIL.Item_Code,max(TSPL_SRN_DETAIL.Item_Desc) as Item_Desc ,max(TSPL_SRN_HEAD.Dept_Desc) as Dept_Desc , " + colCategoryNameDescWithNull + ",(TSPL_SRN_DETAIL.Unit_Code) as Unit_Code, sum(Amount) as Amount ,FORMAT (convert(date, TSPL_SRN_HEAD.SRN_Date,103),'dd/MM/yyyy')+'-Value' as SRN_Date  from TSPL_SRN_DETAIL " & _
                  " left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No  left outer join (select Item_Code, " + colCategoryNameWithMax + " from ( select * from ( " & _
                  " select TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values  from  TSPL_ITEM_MASTER " & _
                  " left outer join TSPL_ITEM_MASTER_CATEGORY on  TSPL_ITEM_MASTER_CATEGORY.Item_code = TSPL_ITEM_MASTER.Item_code " & _
                  " left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code " & _
                  " and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values " & _
                  " where 2=2  )xx " & _
                  " Pivot  ( max(Item_Cagetory_Values) for Item_Category_Code   in ( " + colCategoryNameDesc + " )  ) Pivt" & _
                  "   ) xxx group by Item_Code " & _
                  "  ) as VirtualCategoryTabel on  VirtualCategoryTabel.Item_Code=TSPL_SRN_DETAIL.Item_Code " & _
                  " where datename(month, TSPL_SRN_HEAD.SRN_Date) ='" + cboMonthName.Text + "' and datename(year, TSPL_SRN_HEAD.SRN_Date) ='" + cboYear.SelectedValue + "' and TSPL_SRN_HEAD.Status=1 " & _
                  " group by  convert(date, TSPL_SRN_HEAD.SRN_Date,103) ,TSPL_SRN_DETAIL.Item_Code ,Unit_Code )xxxp " & _
                  " pivot (sum(Amount)   for SRN_Date in ( " + colDateValue + " " & _
                  " ) ) piv ) final  " + ItemArrForValue + " "

            If cboQtyValueWise.Text = "Qty Wise" Then
                qryFinal = qryQty
            End If
            
            If cboQtyValueWise.Text = "Value Wise" Then
                qryFinal = qryValue
                
            End If

            If cboQtyValueWise.Text = "Both" Then
                qryFinal = " select FinalResult.Item_Code,max(FinalResult.Item_Desc) as Item_Desc ,max(FinalResult.Dept_Desc) as Dept_Desc , " + colCategoryNameWithMaxForFinalResult + "   ,max(FinalResult.Unit_Code ) as Unit_Code, " + colDateWithQtyValueOrder + " ,sum (FinalResult.Total_Qty) as Total_Qty , sum(FinalResult.Total_Value) as Total_Value from (  " & _
                                 " select final.Item_Code,final.Item_Desc ,final.Dept_Desc, " + colCategoryNameDescWithFinal + ",final.Unit_Code, " + colDateWithNullWithFinal + " , (" + colDateWithNullSumWithFinal + " ) as Total_Qty, " + colDateForZeroValue + ", 0 as Total_Value from ( select *  from ( " & _
                                 " select  TSPL_SRN_DETAIL.Item_Code,max(TSPL_SRN_DETAIL.Item_Desc) as Item_Desc ,max(TSPL_SRN_HEAD.Dept_Desc) as Dept_Desc , " + colCategoryNameDescWithNull + ",(TSPL_SRN_DETAIL.Unit_Code) as Unit_Code, sum(SRN_Qty) as SRN_Qty ,FORMAT (convert(date, TSPL_SRN_HEAD.SRN_Date,103),'dd/MM/yyyy')+'-Qty' as SRN_Date  from TSPL_SRN_DETAIL " & _
                                 " left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No  left outer join (select Item_Code, " + colCategoryNameWithMax + " from ( select * from ( " & _
                                 " select TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values  from  TSPL_ITEM_MASTER " & _
                                 " left outer join TSPL_ITEM_MASTER_CATEGORY on  TSPL_ITEM_MASTER_CATEGORY.Item_code = TSPL_ITEM_MASTER.Item_code " & _
                                 " left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code " & _
                                 " and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values " & _
                                 " where 2=2  )xx " & _
                                 " Pivot  ( max(Item_Cagetory_Values) for Item_Category_Code   in ( " + colCategoryNameDesc + " )  ) Pivt" & _
                                 "   ) xxx group by Item_Code " & _
                                 "  ) as VirtualCategoryTabel on  VirtualCategoryTabel.Item_Code=TSPL_SRN_DETAIL.Item_Code " & _
                                 " where datename(month, TSPL_SRN_HEAD.SRN_Date) ='" + cboMonthName.Text + "' and datename(year, TSPL_SRN_HEAD.SRN_Date) ='" + cboYear.SelectedValue + "' and TSPL_SRN_HEAD.Status=1 " & _
                                 " group by  convert(date, TSPL_SRN_HEAD.SRN_Date,103) ,TSPL_SRN_DETAIL.Item_Code ,Unit_Code )xxxp " & _
                                 " pivot (sum(SRN_Qty)   for SRN_Date in ( " + colDate + " " & _
                                 " ) ) piv ) final " & _
                                 " Union All" & _
                  " select final.Item_Code,final.Item_Desc ,final.Dept_Desc, " + colCategoryNameDescWithFinal + ",final.Unit_Code, " + colDateForZero + ",0 as Total_Qty, " + colDateWithNullWithFinalValue + " , (" + colDateWithNullSumWithFinalValue + " ) as Total_Value from ( select *  from ( " & _
                  " select  TSPL_SRN_DETAIL.Item_Code,max(TSPL_SRN_DETAIL.Item_Desc) as Item_Desc ,max(TSPL_SRN_HEAD.Dept_Desc) as Dept_Desc , " + colCategoryNameDescWithNull + ",(TSPL_SRN_DETAIL.Unit_Code) as Unit_Code, sum(Amount) as Amount ,FORMAT (convert(date, TSPL_SRN_HEAD.SRN_Date,103),'dd/MM/yyyy')+'-Value' as SRN_Date  from TSPL_SRN_DETAIL " & _
                  " left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No  left outer join (select Item_Code, " + colCategoryNameWithMax + " from ( select * from ( " & _
                  " select TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values  from  TSPL_ITEM_MASTER " & _
                  " left outer join TSPL_ITEM_MASTER_CATEGORY on  TSPL_ITEM_MASTER_CATEGORY.Item_code = TSPL_ITEM_MASTER.Item_code " & _
                  " left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code " & _
                  " and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values " & _
                  " where 2=2  )xx " & _
                  " Pivot  ( max(Item_Cagetory_Values) for Item_Category_Code   in ( " + colCategoryNameDesc + " )  ) Pivt" & _
                  "   ) xxx group by Item_Code " & _
                  "  ) as VirtualCategoryTabel on  VirtualCategoryTabel.Item_Code=TSPL_SRN_DETAIL.Item_Code " & _
                  " where datename(month, TSPL_SRN_HEAD.SRN_Date) ='" + cboMonthName.Text + "' and datename(year, TSPL_SRN_HEAD.SRN_Date) ='" + cboYear.SelectedValue + "' and TSPL_SRN_HEAD.Status=1 " & _
                  " group by  convert(date, TSPL_SRN_HEAD.SRN_Date,103) ,TSPL_SRN_DETAIL.Item_Code ,Unit_Code )xxxp " & _
                  " pivot (sum(Amount)   for SRN_Date in ( " + colDateValue + " " & _
                  " ) ) piv ) final  ) FinalResult " + ItemArrForBoth + " group by Item_Code "
            End If
            

            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(qryFinal)
            If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()
                gv.DataSource = dtgv
                gv.BestFitColumns()
                RadPageView1.SelectedPage = RadPageViewPage2
                gv.Columns("Item_Code").IsPinned = True
                gv.Columns("Item_Desc").IsPinned = True
                If cboQtyValueWise.Text = "Qty Wise" Then
                    FooterSummery(colFooterSummeryQty)
                    gv.Columns("Total_Qty").IsPinned = True
                    gv.Columns("Total_Qty").PinPosition = PinnedColumnPosition.Right
                ElseIf cboQtyValueWise.Text = "Value Wise" Then
                    FooterSummery(colFooterSummeryValue)
                    gv.Columns("Total_Value").IsPinned = True
                    gv.Columns("Total_Value").PinPosition = PinnedColumnPosition.Right
                ElseIf cboQtyValueWise.Text = "Both" Then
                    FooterSummery(colFooterSummeryBoth)
                    gv.Columns("Total_Qty").IsPinned = True
                    gv.Columns("Total_Value").IsPinned = True
                    gv.Columns("Total_Qty").PinPosition = PinnedColumnPosition.Right
                    gv.Columns("Total_Value").PinPosition = PinnedColumnPosition.Right
                End If

            End If
            If dtgv.Rows.Count <= 0 Then
                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()
                clsCommon.MyMessageBoxShow("No Data Found")
                Exit Sub
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Public Sub FooterSummery(ByVal strColumnName As String)
        strColumnName = strColumnName + ",Total_Qty,Total_Value"
        Dim words As String() = strColumnName.Split(New Char() {","c})
        If gv.Rows.Count > 0 Then
            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim word As String
            For Each word In words
                Dim item1 As New GridViewSummaryItem(word, "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
            Next
            gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub


    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        print(EnumExportTo.Excel)
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        print(EnumExportTo.PDF)
    End Sub

    Sub print(ByVal exporter As EnumExportTo)
        Try
            If gv.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptDayWisePurchasePriceReport & "'"))
                arrHeader.Add("Year : " + cboYear.Text + " ")
                arrHeader.Add("Month : " + cboMonthName.Text + " ")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

                If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                    arrHeader.Add(" Item : " + clsCommon.GetMulcallStringWithComma(txtItem.arrDispalyMember))
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
                    'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                    'Process.Start(filePath)
                    transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
                Else
                    clsCommon.MyExportToPDF("Day Wise Purchase Price Report", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
End Class
