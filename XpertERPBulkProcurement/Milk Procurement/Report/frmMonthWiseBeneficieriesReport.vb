Imports common
Public Class frmMonthWiseBeneficieriesReport

    Dim Slot1 As DateTime = Nothing
    Dim Slot2 As DateTime = Nothing
    Private Sub frmMonthWiseBeneficieriesReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            montrangwise.Visible = True
            If rbtnMonthRange.Checked Then
                txtToDate.Enabled = False
                txtFromDate.Value = clsCommon.GETSERVERDATE()
                rbtnMPWise.Checked = True
                reset()
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub reset()
        txtFromDate.Enabled = True
        RadGroupBox1.Enabled = True
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.Refresh()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Sub disabledFields()
        txtFromDate.Enabled = False
        RadGroupBox1.Enabled = False
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            PageSetupReport_ID = MyBase.Form_ID
            Dim Qry As String = Nothing
            If rbtnDCSWise.Checked Then
                If rbtnMonthRange.Checked Then
                    Qry = " Select Max(Month_Year)[Month],COUNT(Vendor_Code)[No Of Beneficieries] from (Select Vendor_Code,Vendor_Name,FORMAT(CONVERT(Date, Created_Date, 103), 'MMM-yyyy')Month_Year ,Month(CONVERT(DATE, Created_Date, 103))Created_Month,Year(CONVERT(DATE, Created_Date, 103))Created_Year from TSPL_Vendor_MASTER where TSPL_Vendor_MASTER.Form_Type='VSP' And CONVERT(date,Created_Date,103)>=CONVERT(date,'" + clsCommon.myCstr(fromdate.Value) + "',103) And CONVERT(date,Created_Date,103)<=CONVERT(date,'" + clsCommon.myCstr(todate.Value) + "',103))final Group By Created_Month,Created_Year Order By Convert(int,Created_Year) ASC,Convert(int,Created_Month) ASC"
                ElseIf rbtDateWise.Checked Then
                    Qry = "select coalesce(VSP_Code,Vendor_Code) as [Dcs Code],coalesce(VLC_Name,vendor_name) as [Dcs Name],VLC_Code_VLC_Uploader as[Uploader Code],
TSPL_VLC_MASTER_HEAD.mcc as [MCC],
TSPL_ROUTE_MASTER.Route_Desc as [Route Name],TSPL_VLC_MASTER_head.AccountType2 as [Bank Detail],
coalesce(TSPL_VLC_MASTER_HEAD.Created_By,TSPL_Vendor_MASTER.Created_By)[Created By],
coalesce(TSPL_VLC_MASTER_HEAD.Created_Date,TSPL_Vendor_MASTER.Created_Date)[Created Date]
from TSPL_Vendor_MASTER
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_Vendor_MASTER.Vendor_Code
 left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No=TSPL_VLC_MASTER_HEAD.Route_Code 
                where TSPL_Vendor_MASTER.Form_Type='VSP' and CONVERT(date,TSPL_Vendor_MASTER.Created_Date,103)>=CONVERT(date,'" + clsCommon.myCstr(fromdate.Value) + "',103) And CONVERT(date,TSPL_Vendor_MASTER.Created_Date,103)<=CONVERT(date,'" + clsCommon.myCstr(todate.Value) + "',103)"
                End If
            Else
                If rbtnMPWise.Checked Then
                    If rbtnMonthRange.Checked Then
                        Qry = "Select Max(Month_Year)[Month],COUNT(MP_Code)[No Of Beneficieries] from(Select MP_Code,MP_Name,FORMAT(CONVERT(DATE, Created_Date, 103), 'MMM-yyyy')Month_Year,Month(Created_Date)Created_Month,Year(Created_Date)Created_Year from TSPL_MP_MASTER where CONVERT(date,Created_Date,103)>=CONVERT(date,'" + clsCommon.myCstr(txtFromDate.Value) + "',103) And CONVERT(date,Created_Date,103)<=CONVERT(date,'" + clsCommon.myCstr(txtToDate.Value) + "',103))final Group By Created_Month,Created_Year Order By Convert(int,Created_Year) ASC,Convert(int,Created_Month) ASC"
                    ElseIf rbtDateWise.Checked Then
                        Qry = "(Select MP_Code,MP_Name,vlc_code,FORMAT(CONVERT(DATE, Created_Date, 103), 'MMM-yyyy')Month_Year,Month(Created_Date)Created_Month,Year(Created_Date)Created_Year from TSPL_MP_MASTER 
where CONVERT(date,Created_Date,103)>=CONVERT(date,'" + clsCommon.myCstr(fromdate.Value) + "',103)  And CONVERT(date,Created_Date,103)<=CONVERT(date,'" + clsCommon.myCstr(todate.Value) + "',103) )"
                        'Qry = "Select Max(Month_Year)[Month],COUNT(Vendor_Code)[No Of Beneficieries] from (Select Vendor_Code,Vendor_Name,FORMAT(CONVERT(DATE, Created_Date, 103), 'MMM-yyyy')Month_Year ,Month(CONVERT(DATE, Created_Date, 103))Created_Month,Year(CONVERT(DATE, Created_Date, 103))Created_Year from TSPL_Vendor_MASTER where TSPL_Vendor_MASTER.Form_Type='VSP' And CONVERT(date,Created_Date,103)>=CONVERT(date,'" + clsCommon.myCstr(txtFromDate.Value) + "',103) And CONVERT(date,Created_Date,103)<=CONVERT(date,'" + clsCommon.myCstr(txtToDate.Value) + "',103))final Group By Created_Month,Created_Year Order By Convert(int,Created_Year) ASC,Convert(int,Created_Month) ASC"
                    End If
                End If
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                disabledFields()
                gv1.DataSource = Nothing
                gv1.Rows.Clear()
                gv1.Columns.Clear()
                gv1.GroupDescriptors.Clear()
                gv1.AutoExpandGroups = True
                gv1.ShowGroupPanel = False
                gv1.ShowRowHeaderColumn = False
                gv1.AllowAddNewRow = False
                gv1.AllowDeleteRow = False
                gv1.EnableFiltering = True
                gv1.ShowFilteringRow = True
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                gv1.MasterView.Refresh()
                gv1.DataSource = dt
                gv1.ReadOnly = True
                For ii As Integer = 0 To gv1.Columns.Count - 1
                    gv1.Columns(ii).ReadOnly = True
                    'gv1.Rows.Add()
                Next
                RadPageView1.SelectedPage = RadPageViewPage2

                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim item As New GridViewSummaryItem("No Of Beneficieries", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item)
                gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                gv1.EnableFiltering = True
                gv1.BestFitColumns()
            Else
                Throw New Exception("Data not found !")
            End If
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
            Month()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub Month()
        If clsCommon.myLen(txtFromDate.Value) > 0 Then
            Dim SM As Integer = txtFromDate.Value.Month
            Dim SY As Integer = txtFromDate.Value.Year

            Dim CD As New DateTime(SY, SM, 1)
            Slot2 = clsCommon.GetPrintDate(CD.AddMonths(12).AddDays(-1), "dd/MMM/yyyy")
            txtToDate.Value = Slot2
        End If
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                Dim MP_DCS_Wise As String = Nothing
                If rbtnMPWise.Checked Then
                    MP_DCS_Wise = "MP Wise"
                Else
                    MP_DCS_Wise = "DCS Wise"
                End If
                arrHeader.Add("Name : Month Wise Beneficieries Report (" + MP_DCS_Wise + ")")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add(("Month Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "MMM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "MMM/yyyy")) + " ")
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                Dim MP_DCS_Wise As String = Nothing
                If rbtnMPWise.Checked Then
                    MP_DCS_Wise = "MP Wise"
                Else
                    MP_DCS_Wise = "DCS Wise"
                End If
                'arrHeader.Add(Environment.NewLine + Environment.NewLine + "Name : Month Wise Beneficieries Report (" + MP_DCS_Wise + ")" + Environment.NewLine)
                'arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName + Environment.NewLine)
                arrHeader.Add(Environment.NewLine + ("Month Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "MMM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "MMM/yyyy")) + " ")
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Month Wise Beneficieries Report (" + MP_DCS_Wise + ")", gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rbtDateWise_Click(sender As Object, e As EventArgs) Handles rbtDateWise.Click
        If rbtDateWise.Checked Then
            fromdate.Value = clsCommon.GETSERVERDATE()
            todate.Value = clsCommon.GETSERVERDATE()

            montrangwise.Visible = False

            daterangewise.Visible = True
        End If
    End Sub

    Private Sub rbtnMonthRange_Click(sender As Object, e As EventArgs) Handles rbtnMonthRange.Click
        If rbtnMonthRange.Checked Then
            daterangewise.Visible = False
            montrangwise.Visible = True
        End If
    End Sub


End Class