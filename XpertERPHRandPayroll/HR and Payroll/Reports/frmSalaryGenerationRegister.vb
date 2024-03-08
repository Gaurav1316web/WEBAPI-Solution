'--16/07/2013--form Add By- Pradeep Sharma ---------
'' Anubhooti(3-July-2014) Added Export Permission Against BM00000003016 ''''''''
'' Anubhooti(11-July-2014) Added Export (Clubed)Button BM00000003137 ''''''''
'' hide releving date by shivani [BM00000008155]
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class frmSalaryGenerationRegister
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    'Const ReportID As String = "SalaryGenRegister"
    Public Program_Code As String = ""

#Region "Variable"
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
    Dim DT As DataTable
#End Region
    Public Sub New(ByVal Prog_Code As String)
        InitializeComponent()
        Program_Code = Prog_Code
    End Sub

    Sub LoadData()
        Try
            'If clsCommon.myLen(txtCode.Value) <= 0 Then
            '    clsCommon.MyMessageBoxShow("Please fill the pay period First. ")
            '    txtCode.Focus()
            '    Exit Sub
            'End If
            If txtPayPeriod.arrValueMember Is Nothing OrElse txtPayPeriod.arrValueMember.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select Pay Period. ", Me.Text)
                txtPayPeriod.Focus()
                Exit Sub
            End If
            If isInsideLoadData Then
                clsCommon.MyMessageBoxShow(Me, "Work in Progress Please Wait...", Me.Text)
                Exit Sub
            End If
            'txtCode.MyReadOnly = True
            btnGenrate.Enabled = True
            isInsideLoadData = True
            btnGenrate.Enabled = False
            gv1.DataSource = Nothing
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.Rows.Clear()
            gv1.Columns.Clear()

            Dim PPCond As String = "''"
            Dim LocCond As String = "''"
            Dim DivCond As String = "''"
            Dim PMCond As String = "''"

            If txtPayPeriod.arrValueMember IsNot Nothing AndAlso txtPayPeriod.arrValueMember.Count > 0 Then
                PPCond = "[" & clsCommon.GetMulcallString(txtPayPeriod.arrValueMember) & "]"
            End If
            If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
                LocCond = "[" & clsCommon.GetMulcallString(txtLocationMult.arrValueMember) & "]"
            End If
            If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
                DivCond = "[" & clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) & "]"
            End If
            If txtPaymentModeMulti.arrValueMember IsNot Nothing AndAlso txtPaymentModeMulti.arrValueMember.Count > 0 Then
                PMCond = "[" + " Payment_Mode in (" & clsCommon.GetMulcallString(txtPaymentModeMulti.arrValueMember) & ")" + "]"
            End If
            If clsCommon.CompairString(Program_Code, clsUserMgtCode.frmSalaryGenerationRegisterArrear) = CompairStringResult.Equal Then
                DT = clsSalaryGeneration.GetSalaryReportData(txtPayPeriod.arrValueMember, txtLocationMult.arrValueMember, txtDivisionMult.arrValueMember, PMCond, True)
            Else
                DT = clsSalaryGeneration.GetSalaryReportData(txtPayPeriod.arrValueMember, txtLocationMult.arrValueMember, txtDivisionMult.arrValueMember, PMCond, False)
            End If

            If (DT IsNot Nothing AndAlso DT.Rows.Count > 0) Then
                gv1.DataSource = DT

                Dim summaryRowItem As New GridViewSummaryRowItem()

                For Each dr As DataColumn In DT.Columns
                    If clsCommon.CompairString(dr.ColumnName, "EMP_CODE") = CompairStringResult.Equal Then
                        gv1.Columns("EMP_CODE").IsVisible = True
                        gv1.Columns("EMP_CODE").Width = 100
                        gv1.Columns("EMP_CODE").HeaderText = "empcode"
                    ElseIf clsCommon.CompairString(dr.ColumnName, "EMPLOYEE_NAME") = CompairStringResult.Equal Then
                        gv1.Columns("EMPLOYEE_NAME").IsVisible = True
                        gv1.Columns("EMPLOYEE_NAME").Width = 150
                        gv1.Columns("EMPLOYEE_NAME").HeaderText = "empname"
                    ElseIf clsCommon.CompairString(dr.ColumnName, "Father Name") = CompairStringResult.Equal Then
                        gv1.Columns("Father Name").IsVisible = True
                        gv1.Columns("Father Name").Width = 100
                        gv1.Columns("Father Name").HeaderText = "fname"
                        ' OT_HOURS_In_Days
                    ElseIf clsCommon.CompairString(dr.ColumnName, "OT_HOURS_In_Days") = CompairStringResult.Equal Then
                        gv1.Columns("OT_HOURS_In_Days").IsVisible = True
                        gv1.Columns("OT_HOURS_In_Days").Width = 100
                        gv1.Columns("OT_HOURS_In_Days").HeaderText = "OT(In Days)"

                    ElseIf clsCommon.CompairString(dr.ColumnName, "UAN No") = CompairStringResult.Equal Then
                        gv1.Columns("UAN No").IsVisible = True
                        gv1.Columns("UAN No").Width = 100
                        gv1.Columns("UAN No").HeaderText = "UAN No"
                    ElseIf clsCommon.CompairString(dr.ColumnName, "PF No") = CompairStringResult.Equal Then
                        gv1.Columns("PF No").IsVisible = True
                        gv1.Columns("PF No").Width = 100
                        gv1.Columns("PF No").HeaderText = "pfno"
                    ElseIf clsCommon.CompairString(dr.ColumnName, "ESI No") = CompairStringResult.Equal Then
                        gv1.Columns("ESI No").IsVisible = True
                        gv1.Columns("ESI No").Width = 100
                        gv1.Columns("ESI No").HeaderText = "insno"
                    ElseIf clsCommon.CompairString(dr.ColumnName, "Bank Acc No") = CompairStringResult.Equal Then
                        gv1.Columns("Bank Acc No").IsVisible = True
                        gv1.Columns("Bank Acc No").Width = 100
                        gv1.Columns("Bank Acc No").HeaderText = "acno"
                    ElseIf clsCommon.CompairString(dr.ColumnName, "Date of Birth") = CompairStringResult.Equal Then
                        gv1.Columns("Date of Birth").IsVisible = True
                        gv1.Columns("Date of Birth").Width = 100
                        gv1.Columns("Date of Birth").HeaderText = "dob1"
                    ElseIf clsCommon.CompairString(dr.ColumnName, "Joining Date") = CompairStringResult.Equal Then
                        gv1.Columns("Joining Date").IsVisible = True
                        gv1.Columns("Joining Date").Width = 100
                        gv1.Columns("Joining Date").HeaderText = "doj1"
                    ElseIf clsCommon.CompairString(dr.ColumnName, "Relieving Date") = CompairStringResult.Equal Then
                        gv1.Columns("Relieving Date").IsVisible = False
                        gv1.Columns("Relieving Date").Width = 100
                        gv1.Columns("Relieving Date").HeaderText = "dol1"
                    ElseIf clsCommon.CompairString(dr.ColumnName, "Designation") = CompairStringResult.Equal Then
                        gv1.Columns("Designation").IsVisible = True
                        gv1.Columns("Designation").Width = 100
                        gv1.Columns("Designation").HeaderText = "desiname"

                    ElseIf clsCommon.CompairString(dr.ColumnName, "Department") = CompairStringResult.Equal Then
                        gv1.Columns("Department").IsVisible = True
                        gv1.Columns("Department").Width = 100
                        gv1.Columns("Department").HeaderText = "dname"
                    ElseIf clsCommon.CompairString(dr.ColumnName, "Location") = CompairStringResult.Equal Then
                        gv1.Columns("Location").IsVisible = True
                        gv1.Columns("Location").Width = 100
                        gv1.Columns("Location").HeaderText = "brname"
                    ElseIf clsCommon.CompairString(dr.ColumnName, "Division") = CompairStringResult.Equal Then
                        gv1.Columns("Division").IsVisible = True
                        gv1.Columns("Division").Width = 100
                        gv1.Columns("Division").HeaderText = "unitname"
                    ElseIf clsCommon.CompairString(dr.ColumnName, "Bank Name") = CompairStringResult.Equal Then
                        gv1.Columns("Bank Name").IsVisible = True
                        gv1.Columns("Bank Name").Width = 100
                        gv1.Columns("Bank Name").HeaderText = "bname"
                    ElseIf clsCommon.CompairString(dr.ColumnName, "Bank Branch") = CompairStringResult.Equal Then
                        gv1.Columns("Bank Branch").IsVisible = True
                        gv1.Columns("Bank Branch").Width = 100
                        gv1.Columns("Bank Branch").HeaderText = "ifsc"
                    ElseIf clsCommon.CompairString(dr.ColumnName, "Bank Branch Name") = CompairStringResult.Equal Then
                        gv1.Columns("Bank Branch Name").IsVisible = True
                        gv1.Columns("Bank Branch Name").Width = 100
                        gv1.Columns("Bank Branch Name").HeaderText = "branchname"
                    ElseIf clsCommon.CompairString(dr.ColumnName, "Payment Mode") = CompairStringResult.Equal Then
                        gv1.Columns("Payment Mode").IsVisible = True
                        gv1.Columns("Payment Mode").Width = 100
                        gv1.Columns("Payment Mode").HeaderText = "Payment Mode"
                    ElseIf clsCommon.CompairString(dr.ColumnName, "Payable Days") = CompairStringResult.Equal Then
                        gv1.Columns("Payable Days").IsVisible = True
                        gv1.Columns("Payable Days").Width = 100
                        gv1.Columns("Payable Days").HeaderText = "pd"
                        Dim item1 As New GridViewSummaryItem(dr.ColumnName, "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(item1)
                    ElseIf clsCommon.CompairString(dr.ColumnName, "Month Days") = CompairStringResult.Equal Then
                        gv1.Columns("Month Days").IsVisible = False
                        gv1.Columns("Month Days").Width = 100
                    ElseIf clsCommon.CompairString(dr.ColumnName, "Present Days") = CompairStringResult.Equal Then
                        gv1.Columns("Present Days").IsVisible = False
                        gv1.Columns("Present Days").Width = 100

                    ElseIf clsCommon.CompairString(dr.ColumnName, "Holidays") = CompairStringResult.Equal Then
                        gv1.Columns("Holidays").IsVisible = False
                        gv1.Columns("Holidays").Width = 100
                    ElseIf clsCommon.CompairString(dr.ColumnName, "Week Off Days") = CompairStringResult.Equal Then
                        gv1.Columns("Week Off Days").IsVisible = False
                        gv1.Columns("Week Off Days").Width = 100
                    ElseIf clsCommon.CompairString(dr.ColumnName, "Leave Days") = CompairStringResult.Equal Then
                        gv1.Columns("Leave Days").IsVisible = False
                        gv1.Columns("Leave Days").Width = 100
                    ElseIf clsCommon.CompairString(dr.ColumnName, "Gross") = CompairStringResult.Equal Then
                        gv1.Columns("Gross").IsVisible = True
                        gv1.Columns("Gross").Width = 100
                        gv1.Columns("Gross").HeaderText = "gross"
                        Dim item1 As New GridViewSummaryItem(dr.ColumnName, "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(item1)
                    ElseIf clsCommon.CompairString(dr.ColumnName, "Gross Salary") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(Program_Code, clsUserMgtCode.frmSalaryGenerationRegisterArrear) = CompairStringResult.Equal Then
                            gv1.Columns("Gross Salary").IsVisible = True
                            gv1.Columns("Gross Salary").Width = 100
                            gv1.Columns("Gross Salary").HeaderText = "Total Earning"
                        Else
                            gv1.Columns("Gross Salary").IsVisible = True
                            gv1.Columns("Gross Salary").Width = 100
                            gv1.Columns("Gross Salary").HeaderText = "tearn"
                        End If


                        Dim item1 As New GridViewSummaryItem(dr.ColumnName, "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(item1)
                    ElseIf clsCommon.CompairString(dr.ColumnName, "TOTAL DEDUCTION") = CompairStringResult.Equal Then
                        gv1.Columns("TOTAL DEDUCTION").IsVisible = True
                        gv1.Columns("TOTAL DEDUCTION").Width = 100
                        gv1.Columns("TOTAL DEDUCTION").HeaderText = "tded"
                        Dim item1 As New GridViewSummaryItem(dr.ColumnName, "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(item1)
                    ElseIf clsCommon.CompairString(dr.ColumnName, "NET SALARY") = CompairStringResult.Equal Then
                        gv1.Columns("NET SALARY").IsVisible = True
                        gv1.Columns("NET SALARY").Width = 100
                        gv1.Columns("NET SALARY").HeaderText = "npay"
                        Dim item1 As New GridViewSummaryItem(dr.ColumnName, "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(item1)

                    ElseIf clsCommon.CompairString(dr.ColumnName, "SALARY_GENERATION_CODE") = CompairStringResult.Equal Then
                        gv1.Columns("SALARY_GENERATION_CODE").IsVisible = False
                        gv1.Columns("SALARY_GENERATION_CODE").Width = 100
                    ElseIf clsCommon.CompairString(dr.ColumnName, "EPF_AC_01") = CompairStringResult.Equal Then
                        gv1.Columns("EPF_AC_01").IsVisible = False
                        gv1.Columns("EPF_AC_01").Width = 100
                        gv1.Columns("EPF_AC_01").HeaderText = "EPF_AC_01"
                        Dim item1 As New GridViewSummaryItem(dr.ColumnName, "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(item1)
                    ElseIf clsCommon.CompairString(dr.ColumnName, "EPF_AC_10") = CompairStringResult.Equal Then
                        gv1.Columns("EPF_AC_10").IsVisible = False
                        gv1.Columns("EPF_AC_10").Width = 100
                        gv1.Columns("EPF_AC_10").HeaderText = "EPF_AC_10"
                        Dim item1 As New GridViewSummaryItem(dr.ColumnName, "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(item1)
                    ElseIf clsCommon.CompairString(dr.ColumnName, "EDLI_AC_21") = CompairStringResult.Equal Then
                        gv1.Columns("EDLI_AC_21").IsVisible = False
                        gv1.Columns("EDLI_AC_21").Width = 100
                        gv1.Columns("EDLI_AC_21").HeaderText = "EDLI_AC_21"
                        Dim item1 As New GridViewSummaryItem(dr.ColumnName, "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(item1)
                    ElseIf clsCommon.CompairString(dr.ColumnName, "Salary_EPF_AC_01") = CompairStringResult.Equal Then
                        gv1.Columns("Salary_EPF_AC_01").IsVisible = False
                        gv1.Columns("Salary_EPF_AC_01").Width = 100
                        gv1.Columns("Salary_EPF_AC_01").HeaderText = "Salary_EPF_AC_01"
                        Dim item1 As New GridViewSummaryItem(dr.ColumnName, "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(item1)
                    ElseIf clsCommon.CompairString(dr.ColumnName, "Salary_EPF_AC_10") = CompairStringResult.Equal Then
                        gv1.Columns("Salary_EPF_AC_10").IsVisible = False
                        gv1.Columns("Salary_EPF_AC_10").Width = 100
                        gv1.Columns("Salary_EPF_AC_10").HeaderText = "Salary_EPF_AC_10"
                        Dim item1 As New GridViewSummaryItem(dr.ColumnName, "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(item1)
                    ElseIf clsCommon.CompairString(dr.ColumnName, "Salary_EDLI_AC_21") = CompairStringResult.Equal Then
                        gv1.Columns("Salary_EDLI_AC_21").IsVisible = False
                        gv1.Columns("Salary_EDLI_AC_21").Width = 100
                        gv1.Columns("Salary_EDLI_AC_21").HeaderText = "Salary_EDLI_AC_21"
                        Dim item1 As New GridViewSummaryItem(dr.ColumnName, "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(item1)
                    ElseIf clsCommon.CompairString(dr.ColumnName, "EPF_Amount_AC_01") = CompairStringResult.Equal Then
                        gv1.Columns("EPF_Amount_AC_01").IsVisible = False
                        gv1.Columns("EPF_Amount_AC_01").Width = 100
                        gv1.Columns("EPF_Amount_AC_01").HeaderText = "EPF_Amount_AC_01"
                        Dim item1 As New GridViewSummaryItem(dr.ColumnName, "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(item1)
                    ElseIf clsCommon.CompairString(dr.ColumnName, "Pension_Amount_AC_10") = CompairStringResult.Equal Then
                        gv1.Columns("Pension_Amount_AC_10").IsVisible = False
                        gv1.Columns("Pension_Amount_AC_10").Width = 100
                        gv1.Columns("Pension_Amount_AC_10").HeaderText = "Pension_Amount_AC_10"
                        Dim item1 As New GridViewSummaryItem(dr.ColumnName, "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(item1)
                    ElseIf clsCommon.CompairString(dr.ColumnName, "Diff_Amount_AC_01") = CompairStringResult.Equal Then
                        gv1.Columns("Diff_Amount_AC_01").IsVisible = False
                        gv1.Columns("Diff_Amount_AC_01").Width = 100
                        gv1.Columns("Diff_Amount_AC_01").HeaderText = "Diff_Amount_AC_01"
                        Dim item1 As New GridViewSummaryItem(dr.ColumnName, "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(item1)
                    ElseIf clsCommon.CompairString(dr.ColumnName, "Admin_Amt_AC_02") = CompairStringResult.Equal Then
                        gv1.Columns("Admin_Amt_AC_02").IsVisible = False
                        gv1.Columns("Admin_Amt_AC_02").Width = 100
                        gv1.Columns("Admin_Amt_AC_02").HeaderText = "Admin_Amt_AC_02"
                        Dim item1 As New GridViewSummaryItem(dr.ColumnName, "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(item1)
                        'ElseIf clsCommon.CompairString(dr.ColumnName, "EDLI_Amt_AC_21") = CompairStringResult.Equal Then
                        '    gv1.Columns("EDLI_Amt_AC_21").IsVisible = False
                        '    gv1.Columns("EDLI_Amt_AC_21").Width = 100
                        '    gv1.Columns("EDLI_Amt_AC_21").HeaderText = "EDLI_Amt_AC_21"
                        '    Dim item1 As New GridViewSummaryItem(dr.ColumnName, "{0:F2}", GridAggregateFunction.Sum)
                        '    summaryRowItem.Add(item1)
                    ElseIf clsCommon.CompairString(dr.ColumnName, "CoEPF_RATE_AC01") = CompairStringResult.Equal Then
                        gv1.Columns("CoEPF_RATE_AC01").IsVisible = False
                        gv1.Columns("CoEPF_RATE_AC01").Width = 100
                        gv1.Columns("CoEPF_RATE_AC01").HeaderText = "CoEPF_RATE_AC01"
                        'Dim item1 As New GridViewSummaryItem(dr.ColumnName, "{0:F2}", GridAggregateFunction.Sum)
                        'summaryRowItem.Add(item1)
                    ElseIf clsCommon.CompairString(dr.ColumnName, "CoEPF_AMT_AC01") = CompairStringResult.Equal Then
                        gv1.Columns("CoEPF_AMT_AC01").IsVisible = False
                        gv1.Columns("CoEPF_AMT_AC01").Width = 100
                        gv1.Columns("CoEPF_AMT_AC01").HeaderText = "CoEPF_AMT_AC01"
                        Dim item1 As New GridViewSummaryItem(dr.ColumnName, "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(item1)
                    ElseIf clsCommon.CompairString(dr.ColumnName, "CoEPS_RATE_AC10") = CompairStringResult.Equal Then
                        gv1.Columns("CoEPS_RATE_AC10").IsVisible = False
                        gv1.Columns("CoEPS_RATE_AC10").Width = 100
                        gv1.Columns("CoEPS_RATE_AC10").HeaderText = "CoEPS_RATE_AC10"
                        'Dim item1 As New GridViewSummaryItem(dr.ColumnName, "{0:F2}", GridAggregateFunction.Sum)
                        'summaryRowItem.Add(item1)
                    ElseIf clsCommon.CompairString(dr.ColumnName, "CoEPS_AMT_AC10") = CompairStringResult.Equal Then
                        gv1.Columns("CoEPS_AMT_AC10").IsVisible = False
                        gv1.Columns("CoEPS_AMT_AC10").Width = 100
                        gv1.Columns("CoEPS_AMT_AC10").HeaderText = "CoEPS_AMT_AC10"
                        Dim item1 As New GridViewSummaryItem(dr.ColumnName, "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(item1)
                    ElseIf clsCommon.CompairString(dr.ColumnName, "EDLI_RATE_AC21") = CompairStringResult.Equal Then
                        gv1.Columns("EDLI_RATE_AC21").IsVisible = False
                        gv1.Columns("EDLI_RATE_AC21").Width = 100
                        gv1.Columns("EDLI_RATE_AC21").HeaderText = "EDLI_RATE_AC21"
                        'Dim item1 As New GridViewSummaryItem(dr.ColumnName, "{0:F2}", GridAggregateFunction.Sum)
                        'summaryRowItem.Add(item1)
                    ElseIf clsCommon.CompairString(dr.ColumnName, "EDLI_Amt_AC_21") = CompairStringResult.Equal Then
                        gv1.Columns("EDLI_Amt_AC_21").IsVisible = False
                        gv1.Columns("EDLI_Amt_AC_21").Width = 100
                        gv1.Columns("EDLI_Amt_AC_21").HeaderText = "EDLI_Amt_AC_21"
                        Dim item1 As New GridViewSummaryItem(dr.ColumnName, "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(item1)
                    ElseIf clsCommon.CompairString(dr.ColumnName, "ESI_HEAD_VALUE") = CompairStringResult.Equal Then
                        gv1.Columns("ESI_HEAD_VALUE").IsVisible = False
                        gv1.Columns("ESI_HEAD_VALUE").Width = 100
                        gv1.Columns("ESI_HEAD_VALUE").HeaderText = "ESI_HEAD_VALUE"
                        Dim item1 As New GridViewSummaryItem(dr.ColumnName, "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(item1)
                    ElseIf clsCommon.CompairString(dr.ColumnName, "ESI_Amount") = CompairStringResult.Equal Then
                        gv1.Columns("ESI_Amount").IsVisible = False
                        gv1.Columns("ESI_Amount").Width = 100
                        gv1.Columns("ESI_Amount").HeaderText = "ESI_Amount"
                        Dim item1 As New GridViewSummaryItem(dr.ColumnName, "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(item1)

                    ElseIf clsCommon.CompairString(dr.ColumnName, "Co_ESI_RATE") = CompairStringResult.Equal Then
                        gv1.Columns("Co_ESI_RATE").IsVisible = False
                        gv1.Columns("Co_ESI_RATE").Width = 100
                        gv1.Columns("Co_ESI_RATE").HeaderText = "Co_ESI_RATE"
                        'Dim item1 As New GridViewSummaryItem(dr.ColumnName, "{0:F2}", GridAggregateFunction.Sum)
                        'summaryRowItem.Add(item1)
                    ElseIf clsCommon.CompairString(dr.ColumnName, "Co_ESI_AMT") = CompairStringResult.Equal Then
                        gv1.Columns("Co_ESI_AMT").IsVisible = False
                        gv1.Columns("Co_ESI_AMT").Width = 100
                        gv1.Columns("Co_ESI_AMT").HeaderText = "Co_ESI_AMT"
                        Dim item1 As New GridViewSummaryItem(dr.ColumnName, "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(item1)
                    Else
                        gv1.Columns(dr.ColumnName).IsVisible = True
                        gv1.Columns(dr.ColumnName).Width = 100
                        Dim item1 As New GridViewSummaryItem(dr.ColumnName, "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(item1)
                    End If
                Next

                gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                ReStoreGridLayout()
                '' hide columns
                'gv1.Columns("SALARY_GENERATION_CODE").IsVisible = False
                'gv1.Columns("EPF_AC_01").IsVisible = False
                'gv1.Columns("EPF_AC_10").IsVisible = False
                'gv1.Columns("EDLI_AC_21").IsVisible = False
                'gv1.Columns("Salary_EPF_AC_01").IsVisible = False
                'gv1.Columns("Salary_EPF_AC_10").IsVisible = False
                'gv1.Columns("Salary_EDLI_AC_21").IsVisible = False
                'gv1.Columns("EPF_Amount_AC_01").IsVisible = False
                'gv1.Columns("Pension_Amount_AC_10").IsVisible = False
                'gv1.Columns("Diff_Amount_AC_01").IsVisible = False
                'gv1.Columns("Admin_Amt_AC_02").IsVisible = False
                'gv1.Columns("ESI_Amount").IsVisible = False
                'gv1.Columns("Co_ESI_RATE").IsVisible = False
                'gv1.Columns("Co_ESI_AMT").IsVisible = False

            Else
                clsCommon.MyMessageBoxShow(Me, "No Data to Show in Selected Pay Period.", Me.Text)
            End If
            isInsideLoadData = False
            btnGenrate.Enabled = True
            '' GRAND TOTAL
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub frmSalaryGenerationRegister_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnGenrate, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        'ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        ' Ticket No : TEC/16/04/19-000468  by prabhakar for open salary generation register report
        If clsCommon.myLen(Me.Tag) > 0 Then
            Dim strSalaryGenrationCode As String = clsCommon.myCstr(Me.Tag)
            Dim qry As String = "select TSPL_GENERATE_SALARY.PAY_PERIOD_CODE, TSPL_GENERATE_SALARY.LOCATION_CODE from TSPL_GENERATE_SALARY where SALARY_GENERATION_CODE = '" + strSalaryGenrationCode + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                Dim strPayHeadCode As String = clsCommon.myCstr(dt.Rows(0)("PAY_PERIOD_CODE"))
                Dim strLocationCode As String = clsCommon.myCstr(dt.Rows(0)("LOCATION_CODE"))
                Dim listPayHeadCode As New ArrayList
                listPayHeadCode.Add(strPayHeadCode)
                Dim listLocation As New ArrayList
                listLocation.Add(strLocationCode)
                txtPayPeriod.arrValueMember = listPayHeadCode
                txtLocationMult.arrValueMember = listLocation
            End If
            btnGenrate.PerformClick()
        End If
        '-------------------------------------------------------------------------------------------------
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmSalaryGenerationRegister)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        '' Preeti Gupta Added Export Permission  ''''''''
        'btnExpoExl.Visible = MyBase.isExport
        'btnExpoPDF.Visible = MyBase.isExport
        btnExport.Visible = MyBase.isExport
        btnGenrate.Visible = MyBase.isModifyFlag
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        funReset()
    End Sub

    Sub funReset()
        'txtCode.MyReadOnly = False
        txtPayPeriod.arrValueMember = Nothing
        txtPayPeriod.Focus()
        'lblPayPeriodName.Text = ""
        txtLocationMult.arrValueMember = Nothing
        txtDivisionMult.arrValueMember = Nothing
        txtPaymentModeMulti.arrValueMember = Nothing
        btnGenrate.Enabled = True
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        funClose()
    End Sub

    Sub funClose()
        Me.Close()
    End Sub

    'Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
    '    Dim qry As String = "SELECT PAY_PERIOD_CODE AS 'Code',(DATEDIFF(DAY,date_from,date_to)+1) as 'Total days', " _
    '        & " PAY_PERIOD_NAME as 'Pay Period Name' FROM TSPL_PAYPERIOD_MASTER  "
    '    txtCode.Value = clsCommon.ShowSelectForm("TSPL_PAYPERIOD_MASTER", qry, "Code", "POSTED=1 AND FREEZED=0", txtCode.Value, "", isButtonClicked)
    '    lblPayPeriodName.Text = clsPayPeriodMaster.GetName(txtCode.Value, Nothing)
    'End Sub

    'Private Sub txtCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    '    If (e.KeyChar = Chr(39)) Then
    '        e.Handled = True
    '    End If
    'End Sub

    Private Sub frmSalaryGenerationRegister_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Sub LoadGridColumns()


    End Sub

    Private Sub btnGenrate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenrate.Click
        PageSetupReport_ID = Program_Code
        TemplateGridview = gv1
        LoadData()
    End Sub
    Private Sub RadMenuItemSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemSave.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If

            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next

                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub RadMenuItemDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemDelete.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub

    Private Sub btnExpoExl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim arr As New List(Of String)()
        arr.Add(objCommonVar.CurrentCompanyName)
        arr.Add("Salary Sheet ")
        arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        'clsCommon.MyExportToExcel("Salary Register", gv1, arr, "Salary Sheet")
        clsCommon.MyExportToExcelGrid("Salary Sheet", gv1, arr, "Salary Sheet", False)

    End Sub

    Private Sub btnExpoPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim arr As New List(Of String)()
        arr.Add(objCommonVar.CurrentCompanyName)
        arr.Add("Salary Sheet")
        arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        clsCommon.MyExportToPDF("Salary Sheet", gv1, arr, "Salary Sheet", True)

    End Sub

    'Private Sub gv1_DataBindingComplete(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewBindingCompleteEventArgs) Handles gv1.DataBindingComplete

    '    If (e.Item Is GridDataItem) Then
    '     GridDataItem dataItem = e.Item as GridDataItem;
    '     counter += Convert.ToInt32(dataItem["SomeField"].Text);
    '    End If
    '  else if(e.Item is GridFooterItem) {
    '     GridFooterItem footerItem = e.Item as GridFooterItem;
    '     FooterItem["YourFooterColumn"].Text = counter.ToString();
    '  }
    'End Sub

    Private Sub gv1_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.ViewCellFormatting
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
            e.CellElement.Font = New Font(e.CellElement.Font, FontStyle.Bold)

        End If
    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        'Dim arr As New List(Of String)()
        'arr.Add(objCommonVar.CurrentCompanyName)
        'arr.Add("Salary Sheet ")
        'arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        'If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
        '    arr.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocationMult.arrDispalyMember))
        'End If
        ''clsCommon.MyExportToExcel("Salary Register", gv1, arr, "Salary Sheet")
        'If gv1.Rows.Count <= 0 Then
        '    gv1.Focus()
        '    clsCommon.MyMessageBoxShow("Data not found.")
        'Else
        '    clsCommon.MyExportToExcelGrid("Salary Sheet", gv1, arr, "Salary Sheet", False)
        'End If
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub btnPDF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        'Dim arr As New List(Of String)()
        'arr.Add(objCommonVar.CurrentCompanyName)
        'arr.Add("Salary Sheet")
        'arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        'clsCommon.MyExportToPDF("Salary Sheet", gv1, arr, "Salary Sheet", True)
        ExportGrid(EnumExportTo.PDF)
    End Sub
    Private Sub txtLocationMult_My_Click(sender As Object, e As EventArgs) Handles txtLocationMult._My_Click
        Dim qry As String = " select Location_Code as Code,Location_Desc as [Name] from TSPL_LOCATION_MASTER where LOCATION_CODE IN (select DISTINCT LOCATION_CODE from TSPL_GENERATE_SALARY where PAY_PERIOD_CODE in (" & clsCommon.GetMulcallString(txtPayPeriod.arrValueMember) & ")) "
        txtLocationMult.arrValueMember = clsCommon.ShowMultipleSelectForm("LocMulSel", qry, "Code", "Name", txtLocationMult.arrValueMember, txtLocationMult.arrDispalyMember)
        Dim frmpending As New FrmPendingRequisitionQty()
        frmpending.SetDiplayMember(txtLocationMult, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    End Sub

    Private Sub txtDivisionMult_My_Click(sender As Object, e As EventArgs) Handles txtDivisionMult._My_Click
        Dim qry As String = " select DEVISION_CODE as Code,DEVISION_NAME as Name from TSPL_DEVISION_MASTER"
        txtDivisionMult.arrValueMember = clsCommon.ShowMultipleSelectForm("DivMulSel", qry, "Code", "Name", txtDivisionMult.arrValueMember, txtDivisionMult.arrDispalyMember)
    End Sub
    Private Sub txtPaymentModeMulti__My_Click(sender As Object, e As EventArgs) Handles txtPaymentModeMulti._My_Click
        Dim qry As String = "select CODE as Code, NAME as Name  from TSPL_Payment_MODE"
        txtPaymentModeMulti.arrValueMember = clsCommon.ShowMultipleSelectForm("PMMulSel", qry, "Code", "Name", txtPaymentModeMulti.arrValueMember, txtPaymentModeMulti.arrDispalyMember)
    End Sub

    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()


                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmSalaryGenerationRegister & "'"))
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                'arrHeader.Add("Date :" + clsCommon.GETSERVERDATE() + " ")
                arrHeader.Add("Pay Period: " & clsCommon.GetMulcallString(txtPayPeriod.arrDispalyMember))
                If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
                    arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocationMult.arrDispalyMember))
                End If
                If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
                    arrHeader.Add("Division : " + clsCommon.GetMulcallStringWithComma(txtDivisionMult.arrDispalyMember))
                End If
                If txtPaymentModeMulti.arrValueMember IsNot Nothing AndAlso txtPaymentModeMulti.arrValueMember.Count > 0 Then
                    arrHeader.Add("Payment Mode : " + clsCommon.GetMulcallStringWithComma(txtPaymentModeMulti.arrDispalyMember))
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
                    transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
                    'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                    'Process.Start(filePath)
                Else
                    transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("Salary Sheet", gv1, arrHeader, "Salary Sheet", PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub txtPayPeriod__My_Click(sender As Object, e As EventArgs) Handles txtPayPeriod._My_Click
        Dim qry As String = "SELECT PAY_PERIOD_CODE AS Code,(DATEDIFF(DAY,date_from,date_to)+1) as 'Total days', " _
           & " PAY_PERIOD_NAME as Name FROM TSPL_PAYPERIOD_MASTER  where POSTED=1 order by Date_From"
        txtPayPeriod.arrValueMember = clsCommon.ShowMultipleSelectForm("PPMulSel", qry, "Code", "Name", txtPayPeriod.arrValueMember, txtPayPeriod.arrDispalyMember)
        'txtCode.Value = clsCommon.ShowSelectForm("TSPL_PAYPERIOD_MASTER", qry, "Code", "POSTED=1 AND FREEZED=0", txtCode.Value, "", isButtonClicked)
        'lblPayPeriodName.Text = clsPayPeriodMaster.GetName(txtCode.Value, Nothing)
    End Sub
End Class
