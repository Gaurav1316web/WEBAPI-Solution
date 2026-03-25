Imports System.Data.SqlClient
Imports common
Imports Telerik.WinControls.UI
Imports XpertERPEngine
Public Class rptEmployeeOTReport
    Inherits FrmMainTranScreen
    Dim fromdate As DateTime = Nothing
    Dim ToDate As DateTime = Nothing

    Private Sub rptEmployeeOTReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            txtFromDate.Value = clsCommon.GETSERVERDATE()
            txtToDate.Value = clsCommon.GETSERVERDATE()
            PageSetupReport_ID = Form_ID
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs)
        Try
            txtmultiEmpcode.arrValueMember = Nothing
            txtFinYear.Value = ""
            lblFinYear.Text = ""

            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterView.Refresh()
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            RadPageView1.SelectedPage = RadPageViewPage1
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub txtmultiEmpcode__My_Click(sender As Object, e As EventArgs) Handles txtmultiEmpcode._My_Click
        Dim qry As String = "select emp_code as Code,Emp_Name as Name,Designation,Birth_date as [Birth Date],Joining_date as [Joining Date] from TSPL_EMPLOYEE_MASTER"
        txtmultiEmpcode.arrValueMember = clsCommon.ShowMultipleSelectForm("DivMulSel", qry, "Code", "Code", txtmultiEmpcode.arrValueMember, txtmultiEmpcode.arrDispalyMember)
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs)
        Try

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text, MessageBoxButtons.OK)
        End Try
    End Sub


    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            'arrHeader.Add("Date :" + clsCommon.GETSERVERDATE() + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptEmployeeOTReport & "'"))

            'If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
            '    arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocationMult.arrDispalyMember))
            'End If
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
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
                'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                'Process.Start(filePath)
            Else
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Employee OT Records", gv1, arrHeader, "Employee OT Records", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPDF_Click(sender As Object, e As EventArgs) Handles btnPDF.Click
        ExportGrid(EnumExportTo.PDF)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub txtPayPeriod__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtPayPeriod._MYValidating
        Try
            Dim qry As String = " select TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE as [Code] ,TSPL_PAYPERIOD_MASTER.PAY_PERIOD_NAME as [Pay Period Name] ,TSPL_PAYPERIOD_MASTER.DATE_FROM as [Date From] ,TSPL_PAYPERIOD_MASTER.DATE_TO as [Date To] ,TSPL_PAYPERIOD_MASTER.DESCRIPTION as [Description] ,TSPL_PAYPERIOD_MASTER.POSTED as [Posted] ,TSPL_PAYPERIOD_MASTER.FREEZED as [Freezed] ,TSPL_PAYPERIOD_MASTER.Posting_Date as [Posting Date] ,TSPL_PAYPERIOD_MASTER.Created_By as [Created By] ,TSPL_PAYPERIOD_MASTER.Created_Date as [Created Date] ,TSPL_PAYPERIOD_MASTER.Modified_By as [Modified By] ,TSPL_PAYPERIOD_MASTER.Modified_Date as [Modified Date]  From TSPL_PAYPERIOD_MASTER "
            txtPayPeriod.Value = clsCommon.ShowSelectForm("vbaMccm", qry, "Code", "", txtPayPeriod.Value, "Code", isButtonClicked)
            lblPayPeriodDesc.Text = clsPayPeriodMaster.GetName(txtPayPeriod.Value, Nothing)
            'lblPayPeriodDesc.Name = "CODE"


            Dim qry1 As String = " select TSPL_PAYPERIOD_MASTER.DATE_FROM  From TSPL_PAYPERIOD_MASTER where TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE = '" + txtPayPeriod.Value + "'  "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry1)

            Dim fromdate As Date = clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("DATE_FROM"), "dd/MMM/yyyy"))
            'totalDays = Date.DaysInMonth(fromdate.Year, fromdate.Month)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text, MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub btnGo_Click_1(sender As Object, e As EventArgs) Handles btnGo.Click
        If rdbOTCalculation.Checked Then
            OTCalcData()
        Else
            OTHoursData
        End If

    End Sub

    Sub OTHoursData()
        Try
            Dim Slot1 As String = ""
            Dim Slot2 As String = ""
            Slot1 = clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy")
            Slot2 = clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy")
            Dim PayPeriodQry As String = " Select PAY_PERIOD_CODE from TSPL_PAYPERIOD_MASTER where CONVERT(DATE, TSPL_PAYPERIOD_MASTER.DATE_FROM, 103) >= CONVERT(DATE, '" + Slot1 + "', 103) and
                                  CONVERT(DATE, TSPL_PAYPERIOD_MASTER.DATE_TO, 103) <= CONVERT(DATE, '" + Slot2 + "', 103)"
            Dim dt12 As DataTable = clsDBFuncationality.GetDataTable(PayPeriodQry)
            Dim codes As New List(Of String)

            For Each row As DataRow In dt12.Rows
                codes.Add("'" & row("PAY_PERIOD_CODE").ToString() & "'")
            Next
            Dim finalString As String = "(" & String.Join(",", codes.ToArray()) & ")"

            Dim qry As String = ""

            qry = " SELECT CASE WHEN GROUPING(OT_DATE) = 1 THEN '' else EMP_CODE end AS [Employee Cod],CASE WHEN GROUPING(OT_DATE) = 1 THEN '' else MAX(Emp_Name) END AS [EMPLOYEE NAME],CASE WHEN GROUPING(OT_DATE) = 1 THEN '' else MAX(Designation) END AS Designation,
	                CASE WHEN GROUPING(OT_DATE) = 1 THEN 'TOTAL' ELSE CONVERT(VARCHAR, OT_DATE, 106) END AS OT_DATE,SUM(OT_HOURS) AS [TOTAL HOURS]
                    FROM ( SELECT H.Document_Code,H.Document_Date,H.PAY_PERIOD_CODE,D.EMP_CODE,D.OT_DATE,D.OT_BASIC,D.OT_DA,D.OT_HOURS,D.OT_TYPE,D.Amount,M.Emp_Name,M.Designation
                    FROM TSPL_EMPLOYEE_OT_ENTRY_DETAIL D
                    LEFT JOIN TSPL_EMPLOYEE_OT_ENTRY_HEAD H ON H.Document_Code = D.Document_Code
                    LEFT JOIN TSPL_EMPLOYEE_MASTER M ON M.EMP_CODE = D.EMP_CODE
                    ) XX WHERE XX.PAY_PERIOD_CODE In " + finalString + " "

            If txtmultiEmpcode.arrValueMember IsNot Nothing AndAlso txtmultiEmpcode.arrValueMember.Count > 0 Then
                qry += " And  XX.EMP_CODE in (" + clsCommon.GetMulcallString(txtmultiEmpcode.arrValueMember) + ")"
            End If

            qry += " GROUP BY GROUPING SETS ((EMP_CODE, OT_DATE), (EMP_CODE))
                    ORDER BY EMP_CODE,GROUPING(OT_DATE),OT_DATE "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.MasterView.Refresh()

            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                gv1.DataSource = dt

                RadPageView1.SelectedPage = RadPageViewPage2

                gv1.EnableFiltering = True
                'FormatGrid()
                'ControlEnableDisable(False)
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)

            End If
            gv1.BestFitColumns()

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text, MessageBoxButtons.OK)
        End Try
    End Sub
    Sub OTCalcData()
        Try
            Dim Slot1 As String = ""
            Dim Slot2 As String = ""
            Slot1 = clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy")
            Slot2 = clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy")
            Dim PayPeriodQry As String = " Select PAY_PERIOD_CODE from TSPL_PAYPERIOD_MASTER where CONVERT(DATE, TSPL_PAYPERIOD_MASTER.DATE_FROM, 103) >= CONVERT(DATE, '" + Slot1 + "', 103) and
                                  CONVERT(DATE, TSPL_PAYPERIOD_MASTER.DATE_TO, 103) <= CONVERT(DATE, '" + Slot2 + "', 103)"
            Dim dt12 As DataTable = clsDBFuncationality.GetDataTable(PayPeriodQry)
            Dim codes As New List(Of String)

            For Each row As DataRow In dt12.Rows
                codes.Add("'" & row("PAY_PERIOD_CODE").ToString() & "'")
            Next
            Dim finalString As String = "(" & String.Join(",", codes.ToArray()) & ")"

            Dim qry As String = ""
            qry = " Select max(Document_Code)[Document Code],max(Document_Date)[Document Date],MAX(Emp_Name)[EMPLOYEE NAME],max(Designation)Designation,SUM(OT_BASIC)[BASIC],
                    SUM(OT_HOURS)[TOTAL HOURS],Cast(sum(Amount/OT_HOURS) as Decimal(10,2))RATE,SUM(Amount)AMOUNT,0 as ESI,SUM(Amount)[Net Payable],PAY_PERIOD_CODE as [PAY PERIOD CODE],
                    max(EMP_CODE)[EMP CODE],MAX(OT_DATE)[OT DATE],SUM(OT_DA)DA,MAX(OT_TYPE)[TYPE]
                    from (Select TSPL_EMPLOYEE_OT_ENTRY_HEAD.Document_Code,TSPL_EMPLOYEE_OT_ENTRY_HEAD.Document_Date,TSPL_EMPLOYEE_OT_ENTRY_HEAD.PAY_PERIOD_CODE,
                    TSPL_EMPLOYEE_OT_ENTRY_DETAIL.EMP_CODE,TSPL_EMPLOYEE_OT_ENTRY_DETAIL.OT_DATE,TSPL_EMPLOYEE_OT_ENTRY_DETAIL.OT_BASIC,TSPL_EMPLOYEE_OT_ENTRY_DETAIL.OT_DA,
                    TSPL_EMPLOYEE_OT_ENTRY_DETAIL.OT_HOURS,TSPL_EMPLOYEE_OT_ENTRY_DETAIL.OT_TYPE,TSPL_EMPLOYEE_OT_ENTRY_DETAIL.Amount,TSPL_EMPLOYEE_MASTER.Emp_Name,TSPL_EMPLOYEE_MASTER.Designation from TSPL_EMPLOYEE_OT_ENTRY_DETAIL
                    LEFT OUTER JOIN TSPL_EMPLOYEE_OT_ENTRY_HEAD ON TSPL_EMPLOYEE_OT_ENTRY_HEAD.Document_Code=TSPL_EMPLOYEE_OT_ENTRY_DETAIL.Document_Code
                    left outer join TSPL_EMPLOYEE_MASTER ON TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_EMPLOYEE_OT_ENTRY_DETAIL.EMP_CODE 
                    ) XX where 2=2 and XX.PAY_PERIOD_CODE In " + finalString + " "

            If txtmultiEmpcode.arrValueMember IsNot Nothing AndAlso txtmultiEmpcode.arrValueMember.Count > 0 Then
                qry += " And  XX.EMP_CODE in (" + clsCommon.GetMulcallString(txtmultiEmpcode.arrValueMember) + ")"
            End If
            qry += " group by XX.PAY_PERIOD_CODE,XX.EMP_CODE "

            'where TSPL_EMPLOYEE_OT_ENTRY_HEAD.Document_Date >= '" + clsCommon.GetPrintDate((txtFromDate.Value), "dd/MMM/yyyy") + "' 
            '        And TSPL_EMPLOYEE_OT_ENTRY_HEAD.Document_Date <= '" + clsCommon.GetPrintDate((txtToDate.Value), "dd/MMM/yyyy") + "'
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.MasterView.Refresh()

            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                gv1.DataSource = dt

                RadPageView1.SelectedPage = RadPageViewPage2

                gv1.EnableFiltering = True
                FormatGrid()
                'ControlEnableDisable(False)
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)

            End If
            gv1.BestFitColumns()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text, MessageBoxButtons.OK)
        End Try
    End Sub
    Sub FormatGrid()
        gv1.AutoExpandGroups = False
        gv1.ShowGroupPanel = False
        gv1.ShowRowHeaderColumn = False
        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = False
        gv1.EnableFiltering = True
        gv1.ShowFilteringRow = True

        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).BestFit()
            'gv1.Columns(ii).IsVisible = False
        Next

        gv1.Columns("Document Code").IsVisible = False
        gv1.Columns("Document Date").IsVisible = False
        gv1.Columns("PAY PERIOD CODE").IsVisible = False
        gv1.Columns("EMP CODE").IsVisible = False
        gv1.Columns("OT DATE").IsVisible = False
        gv1.Columns("DA").IsVisible = False
        gv1.Columns("TYPE").IsVisible = False

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim Total_Amt As New GridViewSummaryItem("BASIC", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Total_Amt)

        Dim Total_Hours As New GridViewSummaryItem("TOTAL HOURS", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Total_Hours)

        Dim Amount As New GridViewSummaryItem("AMOUNT", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Amount)
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            If rdbOTCalculation.Checked Then
                OTPrintCalc()
            Else
                OTPrintHours()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text, MessageBoxButtons.OK)
        End Try
    End Sub

    Sub OTPrintCalc()
        Try
            Dim Slot1 As String = ""
            Dim Slot2 As String = ""
            Slot1 = clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy")
            Slot2 = clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy")
            Dim PayPeriodQry As String = " Select PAY_PERIOD_CODE from TSPL_PAYPERIOD_MASTER where CONVERT(DATE, TSPL_PAYPERIOD_MASTER.DATE_FROM, 103) >= CONVERT(DATE, '" + Slot1 + "', 103) and
                                  CONVERT(DATE, TSPL_PAYPERIOD_MASTER.DATE_TO, 103) <= CONVERT(DATE, '" + Slot2 + "', 103)"
            Dim dt12 As DataTable = clsDBFuncationality.GetDataTable(PayPeriodQry)
            Dim codes As New List(Of String)

            For Each row As DataRow In dt12.Rows
                codes.Add("'" & row("PAY_PERIOD_CODE").ToString() & "'")
            Next
            Dim finalString As String = "(" & String.Join(",", codes.ToArray()) & ")"

            Dim qry As String = ""
            qry = " Select max(Document_Code)[Document Code],max(Document_Date)[Document Date],MAX(Emp_Name)[EMPLOYEE NAME],max(Designation)Designation,SUM(OT_BASIC)[BASIC],
                    SUM(OT_HOURS)[TOTAL HOURS],Cast(sum(Amount/OT_HOURS) as Decimal(10,2))RATE,SUM(Amount)AMOUNT,0 as ESI,SUM(Amount)[Net Payable],PAY_PERIOD_CODE as [PAY PERIOD CODE],
                    max(EMP_CODE)[EMP CODE],MAX(OT_DATE)[OT DATE],SUM(OT_DA)DA,MAX(OT_TYPE)[TYPE]
                    from (Select TSPL_EMPLOYEE_OT_ENTRY_HEAD.Document_Code,TSPL_EMPLOYEE_OT_ENTRY_HEAD.Document_Date,TSPL_EMPLOYEE_OT_ENTRY_HEAD.PAY_PERIOD_CODE,
                    TSPL_EMPLOYEE_OT_ENTRY_DETAIL.EMP_CODE,TSPL_EMPLOYEE_OT_ENTRY_DETAIL.OT_DATE,TSPL_EMPLOYEE_OT_ENTRY_DETAIL.OT_BASIC,TSPL_EMPLOYEE_OT_ENTRY_DETAIL.OT_DA,
                    TSPL_EMPLOYEE_OT_ENTRY_DETAIL.OT_HOURS,TSPL_EMPLOYEE_OT_ENTRY_DETAIL.OT_TYPE,TSPL_EMPLOYEE_OT_ENTRY_DETAIL.Amount,TSPL_EMPLOYEE_MASTER.Emp_Name,TSPL_EMPLOYEE_MASTER.Designation from TSPL_EMPLOYEE_OT_ENTRY_DETAIL
                    LEFT OUTER JOIN TSPL_EMPLOYEE_OT_ENTRY_HEAD ON TSPL_EMPLOYEE_OT_ENTRY_HEAD.Document_Code=TSPL_EMPLOYEE_OT_ENTRY_DETAIL.Document_Code
                    left outer join TSPL_EMPLOYEE_MASTER ON TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_EMPLOYEE_OT_ENTRY_DETAIL.EMP_CODE 
                    ) XX where 2=2 and XX.PAY_PERIOD_CODE In " + finalString + " "

            If txtmultiEmpcode.arrValueMember IsNot Nothing AndAlso txtmultiEmpcode.arrValueMember.Count > 0 Then
                qry += " And  XX.EMP_CODE in (" + clsCommon.GetMulcallString(txtmultiEmpcode.arrValueMember) + ")"
            End If
            qry += " group by XX.PAY_PERIOD_CODE,XX.EMP_CODE "

            'where TSPL_EMPLOYEE_OT_ENTRY_HEAD.Document_Date >= '" + clsCommon.GetPrintDate((txtFromDate.Value), "dd/MMM/yyyy") + "' 
            '        And TSPL_EMPLOYEE_OT_ENTRY_HEAD.Document_Date <= '" + clsCommon.GetPrintDate((txtToDate.Value), "dd/MMM/yyyy") + "'
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            'gv1.DataSource = Nothing
            'gv1.Rows.Clear()
            'gv1.Columns.Clear()
            'gv1.GroupDescriptors.Clear()
            'gv1.MasterTemplate.SummaryRowsBottom.Clear()
            'gv1.MasterView.Refresh()

            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.HRPayroll, dt, "rptEmployeeOTReportCalc", "")
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text, MessageBoxButtons.OK)
        End Try
    End Sub

    Sub OTPrintHours()
        Try
            Dim Slot1 As String = ""
            Dim Slot2 As String = ""
            Slot1 = clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy")
            Slot2 = clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy")
            Dim PayPeriodQry As String = " Select PAY_PERIOD_CODE from TSPL_PAYPERIOD_MASTER where CONVERT(DATE, TSPL_PAYPERIOD_MASTER.DATE_FROM, 103) >= CONVERT(DATE, '" + Slot1 + "', 103) and
                                  CONVERT(DATE, TSPL_PAYPERIOD_MASTER.DATE_TO, 103) <= CONVERT(DATE, '" + Slot2 + "', 103)"
            Dim dt12 As DataTable = clsDBFuncationality.GetDataTable(PayPeriodQry)
            Dim codes As New List(Of String)

            For Each row As DataRow In dt12.Rows
                codes.Add("'" & row("PAY_PERIOD_CODE").ToString() & "'")
            Next
            Dim finalString As String = "(" & String.Join(",", codes.ToArray()) & ")"

            Dim qry As String = ""

            qry = " Select (EMP_CODE)[EMP CODE],MAX(Emp_Name)[EMPLOYEE NAME],max(Designation)Designation,Convert(varchar(20),OT_DATE,103) as [OT DATE],SUM(OT_HOURS)[TOTAL HOURS],
                    max(TSPL_COMPANY_MASTER.Comp_Name) AS [Comp Name],max(PAY_PERIOD_CODE)PAY_PERIOD_CODE
                    from (Select TSPL_EMPLOYEE_OT_ENTRY_HEAD.Document_Code,TSPL_EMPLOYEE_OT_ENTRY_HEAD.Document_Date,TSPL_EMPLOYEE_OT_ENTRY_HEAD.PAY_PERIOD_CODE,TSPL_EMPLOYEE_OT_ENTRY_DETAIL.EMP_CODE,TSPL_EMPLOYEE_OT_ENTRY_DETAIL.OT_DATE,TSPL_EMPLOYEE_OT_ENTRY_DETAIL.OT_BASIC,TSPL_EMPLOYEE_OT_ENTRY_DETAIL.OT_DA,TSPL_EMPLOYEE_OT_ENTRY_DETAIL.OT_HOURS,TSPL_EMPLOYEE_OT_ENTRY_DETAIL.OT_TYPE,TSPL_EMPLOYEE_OT_ENTRY_DETAIL.Amount,TSPL_EMPLOYEE_MASTER.Emp_Name,TSPL_EMPLOYEE_MASTER.Designation from TSPL_EMPLOYEE_OT_ENTRY_DETAIL
                    LEFT OUTER JOIN TSPL_EMPLOYEE_OT_ENTRY_HEAD ON TSPL_EMPLOYEE_OT_ENTRY_HEAD.Document_Code=TSPL_EMPLOYEE_OT_ENTRY_DETAIL.Document_Code
                    left outer join TSPL_EMPLOYEE_MASTER ON TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_EMPLOYEE_OT_ENTRY_DETAIL.EMP_CODE ) XX 
                    left outer join TSPL_COMPANY_MASTER ON 2=2
                    WHERE 2=2 AND  XX.PAY_PERIOD_CODE In " + finalString + " "

            If txtmultiEmpcode.arrValueMember IsNot Nothing AndAlso txtmultiEmpcode.arrValueMember.Count > 0 Then
                qry += " And  XX.EMP_CODE in (" + clsCommon.GetMulcallString(txtmultiEmpcode.arrValueMember) + ")"
            End If

            qry += " group by XX.EMP_CODE,XX.OT_DATE "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            'gv1.DataSource = Nothing
            'gv1.Rows.Clear()
            'gv1.Columns.Clear()
            'gv1.GroupDescriptors.Clear()
            'gv1.MasterTemplate.SummaryRowsBottom.Clear()
            'gv1.MasterView.Refresh()

            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.HRPayroll, dt, "rptEmployeeOTReportHours", "")
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            End If


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text, MessageBoxButtons.OK)
        End Try
    End Sub
End Class