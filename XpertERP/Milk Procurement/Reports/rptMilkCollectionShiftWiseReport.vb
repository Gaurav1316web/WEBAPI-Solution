Imports common
Imports System.ComponentModel
Imports System.IO

'by Sanjay - Create new report 
Public Class rptMilkCollectionShiftWiseReport
    Inherits FrmMainTranScreen

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExp.Visible = MyBase.isExport
    End Sub

    Private Sub RptInventoryMovement_Load(sender As Object, e As EventArgs) Handles Me.Load
        Reset()
    End Sub
    Sub Reset()
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        btnGo.Enabled = True
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        txtMCC.arrValueMember = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = clsCommon.myCstr(MyBase.Form_ID)
        Print(False)
    End Sub
    Sub Print(ByVal isPrint As Boolean, Optional ByVal isPrerint As Boolean = False)
        Try
            Dim dt1 As New DataTable
            Dim qry As String = ""
            'qry = "with dates_cte(Date) as ( select convert(date,'" + fromDate.Value + "',103) union all select dateadd(day,1,date) from dates_cte where convert(date,date,103)<convert(date,'" + ToDate.Value + "',103))" &
            '    " select isnull(tspl_location_master.location_desc,'') as [Plant Name],isnull(TSPL_MCC_MASTER.mcc_name,'') as [Mcc Name],convert(varchar,TDate.DOC_DATE,103) as [Date],Tshift.SHIFT as [Shift],isnull(tsrn.Quantity,0) as Quantity " &
            '    " FROM (select 'M' AS SHIFT UNION ALL select 'E') Tshift cross JOIN ( select date as doc_date from dates_cte) TDate cross join (select distinct TSPL_MILK_SRN_HEAD.MCC_CODE from TSPL_MILK_SRN_HEAD where 1=1 "

            'If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
            '    qry += " and TSPL_MILK_SRN_HEAD.MCC_CODE  IN (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ") "
            'End If

            'qry += " ) Tmcc left outer Join ( " &
            '    " SELECT TSPL_MILK_SRN_HEAD.MCC_CODE,TSPL_MILK_SRN_HEAD.doc_date,TSPL_MILK_SRN_HEAD.shift,SUM(TSPL_MILK_SRN_DETAIL.ACC_Qty) AS Quantity from TSPL_MILK_SRN_DETAIL left outer join " &
            '    " TSPL_MILK_SRN_HEAD On  TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE " &
            '    " where CONVERT(date,TSPL_MILK_SRN_HEAD.Doc_Date,103) >= convert(date,'" + fromDate.Value + "',103) AND  CONVERT(date,TSPL_MILK_SRN_HEAD.Doc_Date,103) <= convert(date,'" + ToDate.Value + "',103) "

            'If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
            '    qry += " and TSPL_MILK_SRN_HEAD.MCC_CODE  IN (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ") "
            'End If

            'qry += " group by TSPL_MILK_SRN_HEAD.MCC_CODE,TSPL_MILK_SRN_HEAD.doc_date,TSPL_MILK_SRN_HEAD.shift " &
            '    " ) tsrn on " &
            '    "  convert(date,tsrn.doc_date,103) =  convert(date,TDate.doc_date,103) And tsrn.shift = Tshift.shift And tsrn.MCC_CODE = Tmcc.MCC_CODE " &
            '    " left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.mcc_code=Tmcc.MCC_CODE " &
            '    " left outer join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.Plant_code " &
            '    " order by Location_Desc,MCC_NAME,TDate.DOC_DATE,SHIFT "

            Dim strDate As String = clsDBFuncationality.getSingleValue(" Declare @colsScheme As NVARCHAR(MAX),@query  As NVARCHAR(MAX) with dates_cte(Date) as (select convert(date,'" + fromDate.Value + "',103) union all select dateadd(day,1,date) from dates_cte where convert(date,date,103)<convert(date,'" + ToDate.Value + "',103)) select  STUFF((Select distinct ',' + QUOTENAME(convert(varchar,dates_cte.Date,103) ) as Alies_Name FROM dates_cte order by Alies_Name FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') option (maxrecursion 0)")
            Dim strDateSum As String = clsDBFuncationality.getSingleValue(" Declare @colsScheme As NVARCHAR(MAX),@query  As NVARCHAR(MAX)  with dates_cte(Date) as (select convert(date,'" + fromDate.Value + "',103) union all select dateadd(day,1,date) from dates_cte where convert(date,date,103)<convert(date,'" + ToDate.Value + "',103)) select  STUFF((SELECT distinct ',' +'Sum(isnull(' + QUOTENAME(convert(varchar,dates_cte.Date,103)) +',0))' +' as ' + QUOTENAME(convert(varchar,dates_cte.Date,103)) as Alies_Name FROM dates_cte order by Alies_Name FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') option (maxrecursion 0)")

            qry = "select [Plant],[Mcc],[Shift]," + strDateSum + " from (SELECT isnull(tspl_location_master.location_desc,'') as [Plant],isnull(TSPL_MCC_MASTER.mcc_name,'') as [Mcc],tspl_location_master.Location_Code,TSPL_MILK_SRN_HEAD.MCC_CODE,convert(varchar,TSPL_MILK_SRN_HEAD.doc_date,103) as doc_date,TSPL_MILK_SRN_HEAD.shift,(case when TSPL_MILK_SRN_HEAD.shift='M' THEN '1'+TSPL_MILK_SRN_HEAD.shift ELSE '2'+TSPL_MILK_SRN_HEAD.shift END) as shift1,cast(TSPL_MILK_SRN_DETAIL.ACC_Qty as decimal(18,2)) AS Quantity from TSPL_MILK_SRN_DETAIL left outer join  TSPL_MILK_SRN_HEAD On  TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.mcc_code=TSPL_MILK_SRN_HEAD.MCC_CODE left outer join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.Plant_code " &
                " where CONVERT(date,TSPL_MILK_SRN_HEAD.Doc_Date,103) >= convert(date,'" + fromDate.Value + "',103) AND  CONVERT(date,TSPL_MILK_SRN_HEAD.Doc_Date,103) <= convert(date,'" + ToDate.Value + "',103) "
            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                qry += " and TSPL_MILK_SRN_HEAD.MCC_CODE  IN (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ") "
            End If
            qry += " ) as s pivot (  sum(Quantity) for doc_date in (" + strDate + " ) ) as zpivot group by zpivot.[Plant],zpivot.[Mcc],zpivot.[Shift],zpivot.[Shift1] order by [Plant],[Mcc],[Shift1]"

            dt1 = Nothing
            dt1 = clsDBFuncationality.GetDataTable(qry)
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.MasterView.Refresh()

            If dt1 Is Nothing OrElse dt1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            Else
                Gv1.DataSource = dt1
                RadPageView1.SelectedPage = RadPageViewPage2
                btnGo.Enabled = False
                SetGridFormat()
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub SetGridFormat()
        Gv1.GroupDescriptors.Add(New GridGroupByExpression("Plant as Plant format ""{0}: {1}"" Group By Plant"))
        Gv1.GroupDescriptors.Add(New GridGroupByExpression("Mcc as Mcc format ""{0}: {1}"" Group By Mcc"))
        Gv1.AutoExpandGroups = True
        Gv1.ShowGroupPanel = False
        Gv1.ShowRowHeaderColumn = False
        Gv1.AllowAddNewRow = False
        Gv1.AllowDeleteRow = False
        Gv1.EnableFiltering = True
        Gv1.ShowFilteringRow = True

        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).BestFit()
        Next

        If Gv1.Rows.Count > 0 Then
            Dim summaryRowItem As New GridViewSummaryRowItem()
            For i As Integer = 3 To Gv1.Columns.Count - 1
                Dim aa = Gv1.Columns(i).HeaderText()
                Dim item1 As New GridViewSummaryItem(aa, "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
            Next
            Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        End If

        Gv1.AutoSizeRows = True
        Gv1.BestFitColumns()
        Gv1.MasterTemplate.AutoExpandGroups = True
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub


    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : " & clsCommon.myCstr("Milk Collection Shift Wise Report"))
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            If txtMCC.arrDispalyMember IsNot Nothing AndAlso txtMCC.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Mcc : " + clsCommon.GetMulcallStringWithComma(txtMCC.arrDispalyMember))
            End If
            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Milk Collection Shift Wise Report", Gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Milk Collection Shift Wise Report", Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        ExportGrid(EnumExportTo.PDF)
    End Sub


    Private Sub TxtMCC__My_Click(sender As Object, e As EventArgs) Handles txtMCC._My_Click
        Dim qry As String = "select MCC_Code as [MCC Code],MCC_NAME as [MCC Name],TSPL_MCC_MASTER.plant_code as [Plant Code],tspl_location_master.location_desc as [Plant Name] from TSPL_MCC_MASTER left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.plant_code"
        txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("@MCC1", qry, "MCC Code", "MCC Name", txtMCC.arrValueMember, txtMCC.arrDispalyMember)
    End Sub
End Class
