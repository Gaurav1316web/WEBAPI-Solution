Imports common
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports common.UserControls
Public Class DcsMonthWiseMilkCollectionReport
    Inherits FrmMainTranScreen

    Private Sub txtFiscalYear__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtFiscalYear._MYValidating
        Dim qry As String = "select Fiscal_Code,Fiscal_Name,Start_Date,End_Date from TSPL_FISCAL_YEAR_MASTER"
        txtFiscalYear.Value = clsCommon.ShowSelectForm("rptACMGTlBal", qry, "Fiscal_Code", "", txtFiscalYear.Value, "", isButtonClicked)
        SetFiscalYear()
    End Sub

    Sub SetFiscalYear()
        txtFromDate.MinDate = New Date(2001, 4, 1)
        txtFromDate.MaxDate = New Date(3000, 12, 1)
        txtToDate.MinDate = txtFromDate.MinDate
        txtToDate.MaxDate = txtFromDate.MaxDate
        If clsCommon.myLen(txtFiscalYear.Value) > 0 Then
            Dim qry As String = " select Fiscal_Code,Fiscal_Name,Start_Date,End_Date from TSPL_FISCAL_YEAR_MASTER where Fiscal_Code='" + txtFiscalYear.Value + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                txtFromDate.MinDate = clsCommon.myCDate(dt.Rows(0)("Start_Date"))
                txtFromDate.MaxDate = clsCommon.myCDate(dt.Rows(0)("End_Date"))
                txtToDate.MinDate = txtFromDate.MinDate
                txtToDate.MaxDate = txtFromDate.MaxDate

                txtFromDate.Value = txtFromDate.MinDate
                txtToDate.Value = txtFromDate.MaxDate
            End If
        Else
            txtToDate.Value = clsCommon.GETSERVERDATE()
            If txtToDate.Value.Month >= 1 AndAlso txtToDate.Value.Month <= 3 Then
                txtFromDate.Value = New Date(txtToDate.Value.Year - 1, 4, 1)
            Else
                txtFromDate.Value = New Date(txtToDate.Value.Year, 4, 1)
            End If
        End If
    End Sub

    Private Sub DcsMonthWiseMilkCollectionReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
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
        Dim qry As String = "select DISTINCT VLC_Code_VLC_Uploader as DCSCode,VLC_Name as DCSName from  TSPL_VLC_MASTER_HEAD"
        'where MCC_CODE IN  (" + clsCommon.GetMulcallString(txtMultBmc.arrValueMember) + ")"
        txtMultDCS.arrValueMember = clsCommon.ShowMultipleSelectForm(" ", qry, "DCSCode", "DCSName", txtMultDCS.arrValueMember, Nothing)
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        EnableDisaableControls(True)
    End Sub
    Sub EnableDisaableControls(ByVal flag As Boolean)
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        txtMultBmc.arrValueMember = Nothing
        txtMultDCS.arrValueMember = Nothing
        txtFromDate.Enabled = flag
        txtToDate.Enabled = flag
        txtFiscalYear.Enabled = flag
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        Gv1.Refresh()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Close()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadData
    End Sub

    'Public Sub LoadData()
    '    Try
    '        Dim dt As New DataTable
    '        If String.IsNullOrEmpty(txtFiscalYear.Value) Then
    '            clsCommon.MyMessageBoxShow(Me, "Please select a Fiscal Year first.", Me.Text)
    '            Exit Sub
    '        End If

    '        Dim parts() As String = txtFiscalYear.Value.Split("-"c)
    '        Dim fiscalStartYear As Integer = 2000 + Val(parts(0))
    '        Dim fiscalEndYear As Integer = 2000 + Val(parts(1))
    '        Dim months As New List(Of String) From {
    '            "Apr-" & fiscalStartYear,
    '            "May-" & fiscalStartYear,
    '            "Jun-" & fiscalStartYear,
    '            "Jul-" & fiscalStartYear,
    '            "Aug-" & fiscalStartYear,
    '            "Sep-" & fiscalStartYear,
    '            "Oct-" & fiscalStartYear,
    '            "Nov-" & fiscalStartYear,
    '            "Dec-" & fiscalStartYear,
    '            "Jan-" & fiscalEndYear,
    '            "Feb-" & fiscalEndYear,
    '            "Mar-" & fiscalEndYear
    '        }
    '        Dim monthColumns As String = String.Join(",", months.Select(Function(m) "[" & m & "]").ToArray())
    '        Dim sumColumns As String = String.Join("," & vbCrLf, months.Select(Function(m) "ISNULL(SUM([" & m & "]), 0) AS [" & m & "]").ToArray())
    '        Dim totalSum As String = String.Join(" + " & vbCrLf, months.Select(Function(m) "ISNULL(SUM([" & m & "]), 0)").ToArray())
    '        Dim strQry As String = $"
    '        SELECT 
    '            TSPL_VENDOR_MASTER.Vendor_Code AS [DCS Code],
    '            TSPL_VENDOR_MASTER.Vendor_Name AS [DCS Name],
    '            {sumColumns},
    '            ({totalSum}) AS [Total]
    '        FROM 
    '        (
    '            SELECT 
    '                TSPL_MILK_SRN_HEAD.VSP_CODE AS Vendor_Code,
    '                DATENAME(MONTH, TSPL_MILK_SRN_HEAD.DOC_DATE) + '-' + CAST(YEAR(TSPL_MILK_SRN_HEAD.DOC_DATE) AS VARCHAR(4)) AS [MonthYear],
    '                TSPL_MILK_SRN_DETAIL.ACC_Qty
    '            FROM TSPL_MILK_SRN_DETAIL
    '            INNER JOIN TSPL_MILK_SRN_HEAD 
    '                ON TSPL_MILK_SRN_DETAIL.DOC_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE
    '            WHERE TSPL_MILK_SRN_HEAD.DOC_DATE 
    '                BETWEEN '{fiscalStartYear}-04-01' AND '{fiscalEndYear}-03-31'
    '        ) AS SourceTable
    '        PIVOT 
    '        (
    '            SUM(ACC_Qty)
    '            FOR [MonthYear] IN ({monthColumns})
    '        ) AS PivotTable
    '        LEFT JOIN TSPL_VENDOR_MASTER 
    '            ON TSPL_VENDOR_MASTER.Vendor_Code = PivotTable.Vendor_Code
    '        GROUP BY 
    '            TSPL_VENDOR_MASTER.Vendor_Code,
    '            TSPL_VENDOR_MASTER.Vendor_Name
    '        ORDER BY 
    '            TSPL_VENDOR_MASTER.Vendor_Code"
    '        dt = clsDBFuncationality.GetDataTable(strQry)
    '        Gv1.DataSource = Nothing
    '        Gv1.Rows.Clear()
    '        Gv1.Columns.Clear()
    '        Gv1.GroupDescriptors.Clear()
    '        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
    '        Gv1.MasterView.Refresh()
    '        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '            Gv1.DataSource = dt
    '            For ii As Integer = 0 To Gv1.Columns.Count - 1
    '                Gv1.Columns(ii).ReadOnly = True
    '                Gv1.Columns(ii).BestFit()
    '            Next
    '            RadPageView1.SelectedPage = RadPageViewPage2
    '            Gv1.EnableFiltering = True
    '        Else
    '            clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
    '        End If
    '        Gv1.BestFitColumns()

    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '    End Try
    'End Sub

    'Public Sub LoadData()
    '    Try
    '        Dim dt As New DataTable

    '        If String.IsNullOrEmpty(txtFiscalYear.Value) Then
    '            clsCommon.MyMessageBoxShow(Me, "Please select a Fiscal Year first.", Me.Text)
    '            Exit Sub
    '        End If

    '        If txtMultBmc.arrValueMember IsNot Nothing AndAlso txtMultBmc.arrValueMember.Count > 0 Then
    '            strQry += "and MCC in (" + clsCommon.GetMulcallString(txtMultBmc.arrValueMember) + ")"
    '        End If

    '        If txtMultDCS.arrValueMember IsNot Nothing AndAlso txtMultDCS.arrValueMember.Count > 0 Then
    '            strQry += " and  TSPL_MILK_SRN_HEAD.VSP_Code in (" + clsCommon.GetMulcallString(txtMultDCS.arrValueMember) + ")"
    '        End If
    '        Dim parts() As String = txtFiscalYear.Value.Split("-"c)
    '        Dim fiscalStartYear As Integer = 2000 + Val(parts(0))
    '        Dim fiscalEndYear As Integer = 2000 + Val(parts(1))
    '        Dim months As New List(Of String) From {
    '        "Apr-" & parts(0),
    '        "May-" & parts(0),
    '        "Jun-" & parts(0),
    '        "Jul-" & parts(0),
    '        "Aug-" & parts(0),
    '        "Sep-" & parts(0),
    '        "Oct-" & parts(0),
    '        "Nov-" & parts(0),
    '        "Dec-" & parts(0),
    '        "Jan-" & parts(1),
    '        "Feb-" & parts(1),
    '        "Mar-" & parts(1)
    '    }
    '        Dim monthColumns As String = String.Join(",", months.Select(Function(m) "[" & m & "]").ToArray())
    '        Dim sumColumns As String = String.Join("," & vbCrLf, months.Select(Function(m) "ISNULL(SUM([" & m & "]), 0) AS [" & m & "]").ToArray())
    '        Dim totalSum As String = String.Join(" + " & vbCrLf, months.Select(Function(m) "ISNULL(SUM([" & m & "]), 0)").ToArray())
    '        Dim strQry As String = $"
    '    SELECT 
    '        TSPL_VENDOR_MASTER.Vendor_Code AS [DCS Code],
    '        TSPL_VENDOR_MASTER.Vendor_Name AS [DCS Name],
    '        {sumColumns},
    '        ({totalSum}) AS [Total]
    '    FROM 
    '    (
    '        SELECT 
    '            TSPL_MILK_SRN_HEAD.VSP_CODE AS Vendor_Code,
    '            LEFT(DATENAME(MONTH, TSPL_MILK_SRN_HEAD.DOC_DATE), 3) 
    '                + '-' + RIGHT(CAST(YEAR(TSPL_MILK_SRN_HEAD.DOC_DATE) AS VARCHAR(4)), 2) AS [MonthYear],
    '            TSPL_MILK_SRN_DETAIL.ACC_Qty
    '        FROM TSPL_MILK_SRN_DETAIL
    '        INNER JOIN TSPL_MILK_SRN_HEAD 
    '            ON TSPL_MILK_SRN_DETAIL.DOC_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE
    '        WHERE TSPL_MILK_SRN_HEAD.DOC_DATE 
    '            BETWEEN '{fiscalStartYear}-04-01' AND '{fiscalEndYear}-03-31'
    '    ) AS SourceTable
    '    PIVOT 
    '    (
    '        SUM(ACC_Qty)
    '        FOR [MonthYear] IN ({monthColumns})
    '    ) AS PivotTable
    '    LEFT JOIN TSPL_VENDOR_MASTER 
    '        ON TSPL_VENDOR_MASTER.Vendor_Code = PivotTable.Vendor_Code
    '    GROUP BY 
    '        TSPL_VENDOR_MASTER.Vendor_Code,
    '        TSPL_VENDOR_MASTER.Vendor_Name
    '    ORDER BY 
    '        TSPL_VENDOR_MASTER.Vendor_Code"

    '        dt = clsDBFuncationality.GetDataTable(strQry)
    '        Gv1.DataSource = Nothing
    '        Gv1.Rows.Clear()
    '        Gv1.Columns.Clear()
    '        Gv1.GroupDescriptors.Clear()
    '        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
    '        Gv1.MasterView.Refresh()

    '        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '            Gv1.DataSource = dt
    '            For ii As Integer = 0 To Gv1.Columns.Count - 1
    '                Gv1.Columns(ii).ReadOnly = True
    '                Gv1.Columns(ii).BestFit()
    '            Next
    '            RadPageView1.SelectedPage = RadPageViewPage2
    '            Gv1.EnableFiltering = True
    '        Else
    '            clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
    '        End If

    '        Gv1.BestFitColumns()

    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '    End Try
    'End Sub


    Public Sub LoadData()
        Try
            Dim dt As New DataTable

            If String.IsNullOrEmpty(txtFiscalYear.Value) Then
                clsCommon.MyMessageBoxShow(Me, "Please select a Fiscal Year first.", Me.Text)
                Exit Sub
            End If

            Dim parts() As String = txtFiscalYear.Value.Split("-"c)
            Dim fiscalStartYear As Integer = 2000 + Val(parts(0))
            Dim fiscalEndYear As Integer = 2000 + Val(parts(1))
            Dim months As New List(Of String) From {
            "Apr-" & parts(0),
            "May-" & parts(0),
            "Jun-" & parts(0),
            "Jul-" & parts(0),
            "Aug-" & parts(0),
            "Sep-" & parts(0),
            "Oct-" & parts(0),
            "Nov-" & parts(0),
            "Dec-" & parts(0),
            "Jan-" & parts(1),
            "Feb-" & parts(1),
            "Mar-" & parts(1)
        }

            Dim monthColumns As String = String.Join(",", months.Select(Function(m) "[" & m & "]").ToArray())
            Dim sumColumns As String = String.Join("," & vbCrLf, months.Select(Function(m) "ISNULL(SUM([" & m & "]), 0) AS [" & m & "]").ToArray())
            Dim totalSum As String = String.Join(" + " & vbCrLf, months.Select(Function(m) "ISNULL(SUM([" & m & "]), 0)").ToArray())

            Dim strQry As String = $"
        SELECT 
            PivotTable.VLC_Code_VLC_Uploader AS [DCS Code],
            PivotTable.VLC_Name AS [DCS Name],
            {sumColumns},
            ({totalSum}) AS [Total]
        FROM 
        (
            SELECT 
                TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,
                TSPL_VLC_MASTER_HEAD.VLC_Name,
                LEFT(DATENAME(MONTH, TSPL_MILK_SRN_HEAD.DOC_DATE), 3)
                    + '-' + RIGHT(CAST(YEAR(TSPL_MILK_SRN_HEAD.DOC_DATE) AS VARCHAR(4)), 2) AS [MonthYear],
                TSPL_MILK_SRN_DETAIL.ACC_Qty
            FROM TSPL_MILK_SRN_DETAIL
            INNER JOIN TSPL_MILK_SRN_HEAD 
                ON TSPL_MILK_SRN_DETAIL.DOC_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE
            LEFT JOIN TSPL_VLC_MASTER_HEAD 
                ON TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_SRN_HEAD.VLC_CODE
            WHERE TSPL_MILK_SRN_HEAD.DOC_DATE 
                BETWEEN '{fiscalStartYear}-04-01' AND '{fiscalEndYear}-03-31' "

            If txtMultBmc.arrValueMember IsNot Nothing AndAlso txtMultBmc.arrValueMember.Count > 0 Then
                strQry += " AND TSPL_MILK_SRN_HEAD.MCC IN (" & clsCommon.GetMulcallString(txtMultBmc.arrValueMember) & ")"
            End If

            If txtMultDCS.arrValueMember IsNot Nothing AndAlso txtMultDCS.arrValueMember.Count > 0 Then
                strQry += " AND TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader IN (" & clsCommon.GetMulcallString(txtMultDCS.arrValueMember) & ")"
            End If

            strQry += $"
        ) AS SourceTable
        PIVOT 
        (
            SUM(ACC_Qty)
            FOR [MonthYear] IN ({monthColumns})
        ) AS PivotTable
        GROUP BY 
                PivotTable.VLC_Code_VLC_Uploader,
                PivotTable.VLC_Name
        ORDER BY 
                PivotTable.VLC_Code_VLC_Uploader"
            dt = clsDBFuncationality.GetDataTable(strQry)

            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.MasterView.Refresh()

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Gv1.DataSource = dt
                For ii As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(ii).ReadOnly = True
                    Gv1.Columns(ii).BestFit()
                Next
                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.EnableFiltering = True
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            End If

            Gv1.BestFitColumns()

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
