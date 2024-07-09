Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports System.IO
Public Class rptEmployeeDeductionMasterReport
    Private Sub rptEmployeeDeductionMasterReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub Reset()
        fndEmpMult.arrValueMember = Nothing
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Refresh()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub fndEmpMult__My_Click(sender As Object, e As EventArgs) Handles fndEmpMult._My_Click
        Try
            Dim qry As String = "Select EMP_CODE As [Code],Emp_Name As [Name], Designation,Emp_Status As Status,LOCATION_CODE As [Location] from TSPL_EMPLOYEE_MASTER"
            fndEmpMult.arrValueMember = clsCommon.ShowMultipleSelectForm("LocMulSel", qry, "Code", "Name", fndEmpMult.arrValueMember, fndEmpMult.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PrintReport(False)
    End Sub

    Private Sub PrintReport(ByVal isPrint As Boolean)
        Try
            Dim Qry As String = Nothing
            If isPrint Then
                Qry = "Select '" + objCommonVar.CurrentUser + "' As PrintBy,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,TSPL_COMPANY_MASTER.Comp_Name,'" + listDeduction.Text + "' As PayHead,"
            Else
                Qry = "Select "
            End If
            Qry += "TSPL_EMPLOYEE_MASTER.EMP_CODE As [Emp. Code],TSPL_EMPLOYEE_MASTER.Emp_Name As [Emp. Name],"
            If clsCommon.CompairString(listDeduction.Text, "LIC") = CompairStringResult.Equal Then
                Qry += " TSPL_EMPLOYEE_DEDUCTION_MASTER.LIC_POLICY_NO As [LIC Policy No],TSPL_EMPLOYEE_DEDUCTION_MASTER.LIC_PREMIUM_AMT As [LIC Premium Amt.]"
            ElseIf clsCommon.CompairString(listDeduction.Text, "Bank") = CompairStringResult.Equal Then
                Qry += " TSPL_EMPLOYEE_DEDUCTION_MASTER.BANK_ACCOUNT_NO As [Bank Account No],TSPL_EMPLOYEE_DEDUCTION_MASTER.BANK_INSTALMENT As [Bank Instalment],TSPL_EMPLOYEE_DEDUCTION_MASTER.BANK_NAME As [Bank Name]"
            ElseIf clsCommon.CompairString(listDeduction.Text, "Quarter") = CompairStringResult.Equal Then
                Qry += " TSPL_EMPLOYEE_DEDUCTION_MASTER.QUARTER_TYPE As [Quarter Type],Convert(Varchar,TSPL_EMPLOYEE_DEDUCTION_MASTER.QUARTER_ALLOTED_DATE,103) As [Quarter Alloted Date],Convert(Varchar,TSPL_EMPLOYEE_DEDUCTION_MASTER.QUARTER_LEFT_DATE,103) As [Quarter Left Date]"
            ElseIf clsCommon.CompairString(listDeduction.Text, "KKK") = CompairStringResult.Equal Then
                Qry += " TSPL_EMPLOYEE_DEDUCTION_MASTER.KKK_INSTALMENT As [KKK Instalment],TSPL_EMPLOYEE_DEDUCTION_MASTER.KKK_LOAN_TOTAL As [KKK Total Loan] "
            Else
                Qry += " TSPL_EMPLOYEE_DEDUCTION_MASTER.LIC_POLICY_NO As [LIC Policy No],TSPL_EMPLOYEE_DEDUCTION_MASTER.LIC_PREMIUM_AMT As [LIC Premium Amt.],TSPL_EMPLOYEE_DEDUCTION_MASTER.BANK_ACCOUNT_NO As [Bank Account No],TSPL_EMPLOYEE_DEDUCTION_MASTER.BANK_INSTALMENT As [Bank Instalment],TSPL_EMPLOYEE_DEDUCTION_MASTER.BANK_NAME As [Bank Name],TSPL_EMPLOYEE_DEDUCTION_MASTER.QUARTER_TYPE As [Quarter Type],Convert(Varchar,TSPL_EMPLOYEE_DEDUCTION_MASTER.QUARTER_ALLOTED_DATE,103) As [Quarter Alloted Date],Convert(Varchar,TSPL_EMPLOYEE_DEDUCTION_MASTER.QUARTER_LEFT_DATE,103) As [Quarter Left Date],TSPL_EMPLOYEE_DEDUCTION_MASTER.KKK_INSTALMENT As [KKK Instalment],TSPL_EMPLOYEE_DEDUCTION_MASTER.KKK_LOAN_TOTAL As [KKK Total Loan] "
            End If
            Qry += " from TSPL_EMPLOYEE_DEDUCTION_MASTER
                     Left Outer Join TSPL_EMPLOYEE_MASTER On TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_EMPLOYEE_DEDUCTION_MASTER.EMP_CODE
                     Left Outer Join TSPL_COMPANY_MASTER On TSPL_COMPANY_MASTER.Comp_Code1='" + objCommonVar.CurrComp_Code1 + "' Where 2=2 "
            If fndEmpMult.arrValueMember IsNot Nothing AndAlso fndEmpMult.arrValueMember.Count > 0 Then
                Qry += "  and TSPL_EMPLOYEE_MASTER.EMP_CODE IN (" + clsCommon.GetMulcallString(fndEmpMult.arrValueMember) + ") "
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If isPrint Then
                    Dim frmcrystal As New frmCrystalReportViewer()
                    frmcrystal.funreport(CrystalReportFolder.HRPayroll, dt, "crptEmployeeDeductionMaster", "Employee Deduction Report")
                Else
                    Gv1.DataSource = Nothing
                    Gv1.Rows.Clear()
                    Gv1.Refresh()
                    Gv1.DataSource = dt
                    SetSummaryRow()
                    Gv1.AllowAddNewRow = False
                    Gv1.ShowGroupPanel = True
                    Gv1.ReadOnly = True
                    Gv1.EnableFiltering = True
                    Gv1.EnableGrouping = True
                    'View()
                    Gv1.BestFitColumns()
                    RadPageView1.SelectedPage = RadPageViewPage2
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "Data Not Found", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub View()
        If Gv1 IsNot Nothing AndAlso Gv1.Rows.Count > 0 Then

        End If
    End Sub
    Sub SetSummaryRow()
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item1 As New GridViewSummaryItem("LIC Premium Amt.", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        Dim item2 As New GridViewSummaryItem("Bank Instalment", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)

        Dim item3 As New GridViewSummaryItem("KKK Instalment", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)

        Dim item4 As New GridViewSummaryItem("KKK Total Loan", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)

        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        PrintReport(True)
    End Sub

    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                Dim strTemp As String = ""
                arrHeader.Add("" & clsDBFuncationality.getSingleValue("select Comp_Name from TSPL_COMPANY_MASTER where Comp_Code1='" & objCommonVar.CurrComp_Code1 & "'"))
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmEmployeeDeductionMaster & "'"))
                If Not fndEmpMult.arrValueMember Is Nothing Then
                    arrHeader.Add("Employee : " & clsCommon.GetMulcallStringWithComma(fndEmpMult.arrValueMember))
                End If
                If exporter = EnumExportTo.Excel Then
                    transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
                Else
                    transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                    clsCommon.MyExportToPDF(Me.Text, Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class