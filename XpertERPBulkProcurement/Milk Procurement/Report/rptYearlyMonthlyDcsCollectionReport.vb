Imports common
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports common.UserControls
Public Class rptYearlyMonthlyDcsCollectionReport
    Inherits FrmMainTranScreen
    Dim Slot1 As DateTime = Nothing
    Dim Slot2 As DateTime = Nothing
    Dim Month1 As String = Nothing
    Dim Month2 As String = Nothing
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadData
    End Sub
    'Sub Month()
    '    If clsCommon.myLen(txtFromDate.Value) > 0 Then
    '        Dim SM As Integer = txtFromDate.Value.Month
    '        Dim SY As Integer = txtFromDate.Value.Year

    '        Dim CD As New DateTime(SY, SM, 1)
    '        Slot2 = clsCommon.GetPrintDate(CD.AddDays(-1), "dd/MMM/yyyy")
    '        txtToDate.Value = txtFromDate.Value.AddMonths(2)
    '        Month1 = clsCommon.GetPrintDate(txtFromDate.Value, "MM-yyyy")
    '        Month2 = clsCommon.GetPrintDate(txtFromDate.Value.AddMonths(1), "MM-yyyy")

    '    End If
    'End Sub
    Public Sub LoadData()
        Try

            Dim dt As New DataTable
            Dim strQry As String = "SELECT XXX.*,xxxMPCount.[Count of Farmers],xxxMPCount.[Farmers Milk Qty
] FROM (Select 
TSPL_MILK_SRN_HEAD.VSP_Code as [Secretary Code],MAX(TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader) as [BMC Uploader code],MAX(TSPL_VLC_MASTER_HEAD.VLC_Name) as [BMC Name],max(TSPL_VLC_MASTER_HEAD.MCC) as [DCS],MAX(TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader) As [DCS Uploader Code],MAX(TSPL_MILK_SRN_HEAD.Route_code) AS [Route Code],
Sum(TSPL_MILK_SRN_DETAIL.Qty)AS [MILK WEIGHT],sum(TSPL_MILK_SRN_DETAIL.ACC_Qty) As [Milk Weight(KG)], 
sum(TSPL_MILK_SRN_DETAIL.ACC_Qty_LTR) As [Milk Weight(LTR)],sum(TSPL_MILK_SRN_DETAIL.FAT_PER) As [FAT(%)], sum(TSPL_MILK_SRN_DETAIL.SNF_PER) As [SNF(%)],
sum(TSPL_MILK_SRN_DETAIL.FAT_kg) As [FAT(KG)], sum(TSPL_MILK_SRN_DETAIL.SNF_kg) As [SNF(KG)]

from TSPL_MILK_SRN_DETAIL
Left Outer Join TSPL_MILK_SRN_HEAD On TSPL_MILK_SRN_HEAD.Doc_Code=TSPL_MILK_SRN_DETAIL.Doc_Code
Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_SRN_HEAD.VLC_CODE
Left Outer Join(Select TSPL_MP_INCENTIVE_ENTRY_HEAD.MCC_Code,Sum(TSPL_MP_INCENTIVE_ENTRY_DETAIL.Qty)Qty from TSPL_MP_INCENTIVE_ENTRY_DETAIL
Left Outer Join TSPL_MP_INCENTIVE_ENTRY_HEAD On TSPL_MP_INCENTIVE_ENTRY_HEAD.Document_Code=TSPL_MP_INCENTIVE_ENTRY_DETAIL.Document_Code
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.MCC=TSPL_MP_INCENTIVE_ENTRY_HEAD.MCC_Code
where 2=2 and  Convert(Date,TSPL_MP_INCENTIVE_ENTRY_HEAD.Document_Date,103)>=Convert(Date,'" + Slot1 + "',103)
And            Convert(Date,TSPL_MP_INCENTIVE_ENTRY_HEAD.Document_Date,103)<=Convert(Date,'" + Slot2 + "',103)
Group By TSPL_MP_INCENTIVE_ENTRY_HEAD.MCC_Code) xxxQty On xxxQty.MCC_Code=TSPL_MILK_SRN_HEAD.MCC_CODE

where 2=2 and  Convert(Date,TSPL_MILK_SRN_HEAD.DOC_DATE,103)>=Convert(Date,'" + Slot1 + "',103) And Convert(Date,TSPL_MILK_SRN_HEAD.DOC_DATE,103)<=Convert(Date,'" + Slot2 + "',103)"
            If txtMultRoute.arrValueMember IsNot Nothing AndAlso txtMultRoute.arrValueMember.Count > 0 Then
                strQry += "  and TSPL_MILK_SRN_HEAD.ROUTE_CODE in (" + clsCommon.GetMulcallString(txtMultRoute.arrValueMember) + ")"
            End If
            If txtMultBmc.arrValueMember IsNot Nothing AndAlso txtMultBmc.arrValueMember.Count > 0 Then
                strQry += "and MCC in (" + clsCommon.GetMulcallString(txtMultBmc.arrValueMember) + ")"
            End If

            If txtMultDCS.arrValueMember IsNot Nothing AndAlso txtMultDCS.arrValueMember.Count > 0 Then
                strQry += " and  TSPL_MILK_SRN_HEAD.VSP_Code in (" + clsCommon.GetMulcallString(txtMultDCS.arrValueMember) + ")"
            End If
            strQry += " Group By TSPL_MILK_SRN_HEAD.MCC_CODE,TSPL_MILK_SRN_HEAD.VSP_Code)XXX
lEFT oUTER JOIN TSPL_VLC_MASTER_HEAD oN TSPL_VLC_MASTER_HEAD.VSP_Code=xxx.[Secretary Code]"
            strQry += " Left Outer Join(select TSPL_MP_INCENTIVE_ENTRY_DETAIL.VLC_Code,sum(TSPL_MP_INCENTIVE_ENTRY_DETAIL.Qty)AS [Farmers Milk Qty
],COUNT(Distinct TSPL_MP_MASTER.MP_Code_VLC_Uploader)AS [Count of Farmers],TSPL_MP_INCENTIVE_ENTRY_HEAD.MCC_Code from TSPL_MP_MASTER
LEFT OUTER JOIN TSPL_MP_INCENTIVE_ENTRY_DETAIL ON TSPL_MP_INCENTIVE_ENTRY_DETAIL.MP_Code=TSPL_MP_MASTER.MP_Code
LEFT OUTER JOIN TSPL_MP_INCENTIVE_ENTRY_HEAD  ON TSPL_MP_INCENTIVE_ENTRY_HEAD.Document_Code=TSPL_MP_INCENTIVE_ENTRY_DETAIL.Document_Code
where 2=2 and  Convert(Date,TSPL_MP_INCENTIVE_ENTRY_HEAD.Document_Date,103)>=Convert(Date,'" + Slot1 + "',103) 
And            Convert(Date,TSPL_MP_INCENTIVE_ENTRY_HEAD.Document_Date,103)<=Convert(Date,'" + Slot2 + "',103)
group by TSPL_MP_INCENTIVE_ENTRY_HEAD.MCC_Code,TSPL_MP_INCENTIVE_ENTRY_DETAIL.VLC_Code
)xxxMPCount On xxxMPCount.VLC_Code=TSPL_VLC_MASTER_HEAD.VLC_Code"
            'strQry += " Group by TSPL_MILK_RECEIPT_DETAIL.MILK_WEIGHT,TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT,TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT_LTR, TSPL_MILK_SAMPLE_DETAIL.FAT , TSPL_MILK_SAMPLE_DETAIL.SNF ,   TSPL_MILK_SRN_DETAIL.FAT_kg , TSPL_MILK_SRN_DETAIL.SNF_kg,(MP_Code_VLC_Uploader),TSPL_MP_INCENTIVE_ENTRY_DETAIL.Qty "
            dt = clsDBFuncationality.GetDataTable(strQry)
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.MasterView.Refresh()

            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                Gv1.DataSource = dt

                RadPageView1.SelectedPage = RadPageViewPage2

                Gv1.EnableFiltering = True
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            End If
            FormatGrid()
            Gv1.BestFitColumns()

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub FormatGrid()
        Gv1.AutoExpandGroups = False
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
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item1 As New GridViewSummaryItem("MILK WEIGHT", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("Milk Weight(KG)", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("Milk Weight(LTR)", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Dim item4 As New GridViewSummaryItem("FAT(KG)", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem("SNF(KG)", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Dim item6 As New GridViewSummaryItem("Farmers Milk Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)

        Gv1.ShowGroupPanel = True
        Gv1.MasterTemplate.AutoExpandGroups = True
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub

    Private Sub txtMultRoute__My_Click(sender As Object, e As EventArgs) Handles txtMultRoute._My_Click
        Dim qry As String = " select DISTINCT TSPL_ROUTE_MASTER.Route_No from TSPL_ROUTE_MASTER"
'        Left OUTER JOIN TSPL_MILK_SRN_HEAD ON TSPL_MILK_SRN_HEAD.ROUTE_CODE=TSPL_ROUTE_MASTER.Route_No
'where 2=2  AND TSPL_MILK_SRN_HEAD.MCC_CODE IN   (" + clsCommon.GetMulcallString(txtMultBmc.arrValueMember) + ") "
        txtMultRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("VSPMulSelect", qry, "Route_no", "", txtMultRoute.arrValueMember, txtMultRoute.arrDispalyMember)
    End Sub

    Private Sub txtMultBmc__My_Click(sender As Object, e As EventArgs) Handles txtMultBmc._My_Click
        Try
            Dim qry As String = "select DISTINCT MCC_CODE as [MCC Code] from TSPL_MILK_SRN_HEAD"
            txtMultBmc.arrValueMember = clsCommon.ShowMultipleSelectForm("@TSDSR1", qry, "MCC Code", "", txtMultBmc.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtMultDCS__My_Click(sender As Object, e As EventArgs) Handles txtMultDCS._My_Click
        Dim qry As String = "select DISTINCT TSPL_MILK_SRN_HEAD.VSP_Code as Code,TSPL_VENDOR_MASTER.Zone_Code  from TSPL_MILK_SRN_HEAD 
left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_SRN_HEAD.VSP_CODE"
        'where MCC_CODE IN  (" + clsCommon.GetMulcallString(txtMultBmc.arrValueMember) + ")"
        txtMultDCS.arrValueMember = clsCommon.ShowMultipleSelectForm("PCUVLC1", qry, "Code", "", txtMultDCS.arrValueMember, Nothing)
    End Sub

    Private Sub rptYearlyMonthlyDcsCollectionReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()

    End Sub
    'Sub Month()
    '    If clsCommon.myLen(txtFromDate.Value) > 0 Then
    '        Dim SM As Integer = txtFromDate.Value.Month
    '        Dim SY As Integer = txtFromDate.Value.Year

    '        Dim CD As New DateTime(SY, SM, 1)
    '        Slot2 = clsCommon.GetPrintDate(CD.AddDays(-1), "dd/MMM/yyyy")
    '        txtToDate.Value = txtFromDate.Value.AddMonths(2)
    '        Month1 = clsCommon.GetPrintDate(txtFromDate.Value, "MM-yyyy")
    '        Month2 = clsCommon.GetPrintDate(txtFromDate.Value.AddMonths(1), "MM-yyyy")

    '    End If
    'End Sub


    Private Sub txtToDate_ValueChanged(sender As Object, e As EventArgs) Handles txtToDate.ValueChanged
        Try
            Dim SM As Integer = txtToDate.Value.Month
            Dim SY As Integer = txtToDate.Value.Year
            Dim dates As Integer = txtToDate.Value.Day
            Dim lastDayOfMonth As Integer = DateTime.DaysInMonth(SY, SM)

            Dim CD As New DateTime(SY, SM, lastDayOfMonth)
            'Slot2 = clsCommon.GetPrintDate(CD.AddDays(-1), "dd/MMM/yyyy")
            Slot2 = clsCommon.GetPrintDate(CD, "dd/MMM/yyyy")

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtFromDate_ValueChanged(sender As Object, e As EventArgs) Handles txtFromDate.ValueChanged
        Try
            Dim SM As Integer = txtFromDate.Value.Month
            Dim SY As Integer = txtFromDate.Value.Year

            Dim CD As New DateTime(SY, SM, 1)
            Slot1 = clsCommon.GetPrintDate(CD, "dd/MMM/yyyy")
            ' Month()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub Reset()
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        txtMultBmc.arrValueMember = Nothing
        txtMultDCS.arrValueMember = Nothing
        txtMultRoute.arrValueMember = Nothing
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        Gv1.Refresh()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        Try
            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptYearlyMonthlyDcsCollectionReport & "'"))
                'If rbtnSummary.IsChecked = True Then
                '    arrHeader.Add("Report Type : " & "Summary")
                'End If
                'If rbtnDetail.IsChecked = True Then
                '    arrHeader.Add("Report Type : " & "Details")
                'End If
                transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnPDF_Click(sender As Object, e As EventArgs) Handles btnPDF.Click
        Try
            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Date Range : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy"))

                'arrHeader.Add("Month :" & MonthNo)
                clsCommon.MyExportToPDF(Me.Text, Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)

            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class