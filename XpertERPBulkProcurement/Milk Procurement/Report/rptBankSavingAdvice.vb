Imports common

Public Class rptBankSavingAdvice
    Inherits FrmMainTranScreen
    Private Sub rptBankSavingAdvice_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub txtFiscalYear__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtFiscalYear._MYValidating
        Try
            Dim qry As String = "select Fiscal_Code,Fiscal_Name,Start_Date,End_Date from TSPL_FISCAL_YEAR_MASTER"
            txtFiscalYear.Value = clsCommon.ShowSelectForm("LRFY", qry, "Fiscal_Code", "", txtFiscalYear.Value, "", isButtonClicked)
            'fromDate.Value = clsCommon.GETSERVERDATE()
            'ToDate.Value = clsCommon.GETSERVERDATE()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub fromDate_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles fromDate.Validating

        SetToDate()
    End Sub
    Sub SetToDate()
        'If MultipleFinderFillAuto Then
        Dim PaymentCycleType As String = ""
        Dim PaymentCycleValue As Integer = 0

        'If clsCommon.myLen(fndLoc.Value) <= 0 AndAlso MultipleFinderFillAuto = False Then
        '    clsCommon.MyMessageBoxShow("Please select the Location first")
        '    Exit Sub
        'End If
        'If MultipleFinderFillAuto = True Then
        '    If mfndMcc.arrValueMember Is Nothing OrElse mfndMcc.arrValueMember.Count <= 0 Then
        '        clsCommon.MyMessageBoxShow("Please select the Location first")
        '        Exit Sub
        '    End If
        'End If
        Dim strMCCcode = ""



        Dim dt As DataTable = clsDBFuncationality.GetDataTable(" select TSPL_MCC_MASTER.Payment_Cycle,TSPL_PAYMENT_CYCLE_MASTER.PC_TYPE,TSPL_PAYMENT_CYCLE_MASTER.PC_VALUE  from TSPL_MCC_MASTER left outer join TSPL_PAYMENT_CYCLE_MASTER on TSPL_PAYMENT_CYCLE_MASTER.PC_CODE=TSPL_MCC_MASTER.Payment_Cycle   where TSPL_MCC_MASTER.MCC_Code  in (select Location_Code  from TSPL_LOCATION_MASTER where " + strMCCcode + "  Location_Category='MCC' and Rejected_Type='N') ")
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "No Payment Cycle found on current MCC/Location", Me.Text)
            Exit Sub
        End If
        PaymentCycleType = clsCommon.myCstr(dt.Rows(0)("PC_TYPE"))
        PaymentCycleValue = clsCommon.myCdbl(dt.Rows(0)("PC_VALUE"))
        Dim dtCurr As DateTime = clsCommon.GETSERVERDATE()
        'If clsCommon.CompairString(PaymentCycleType, "Day") = CompairStringResult.Equal Then
        '    If fromDate.Value.Day Mod PaymentCycleValue <> 1 And (Not PaymentCycleValue = 1) Then
        '        clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month or at interval of " & PaymentCycleValue & " Day, Because MCC has payment Cycle of " & PaymentCycleValue & " Day ")
        '        fromDate.Value = New Date(dtCurr.Year, dtCurr.Month, 1)
        '        ToDate.Value = fromDate.Value
        '        Exit Sub
        '    End If
        '    ToDate.Value = fromDate.Value.AddDays(PaymentCycleValue - 1)

        '    If fromDate.Value.Month <> ToDate.Value.Month Then
        '        ToDate.Value = New Date(fromDate.Value.Year, fromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
        '    End If
        '    Dim dtNxtPay As DateTime = ToDate.Value.AddDays(Math.Ceiling(PaymentCycleValue / 2.0))
        '    If fromDate.Value.Month <> dtNxtPay.Month Then
        '        ToDate.Value = New Date(fromDate.Value.Year, fromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
        '    End If
        'Else
        If clsCommon.CompairString(PaymentCycleType, "Month") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsCommon.GetPrintDate(fromDate.Value, "dd")) <> 1 Then
                    clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month, Because MCC has payment Cycle of Month Type")
                    fromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    ToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    Exit Sub
                End If
                ToDate.Value = DateAdd(DateInterval.Month, PaymentCycleValue, fromDate.Value)
            ElseIf clsCommon.CompairString(PaymentCycleType, "Year") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsCommon.GetPrintDate(fromDate.Value, "dd")) <> 1 Then
                    clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month, Because MCC has payment Cycle of Year Type")
                    fromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    ToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    Exit Sub
                End If
                ToDate.Value = DateAdd(DateInterval.Year, PaymentCycleValue, fromDate.Value)
            ElseIf clsCommon.CompairString(PaymentCycleType, "Week") = CompairStringResult.Equal Then
                Dim today As Date = fromDate.Value
            Dim dayDiff As Integer = today.DayOfWeek - IIf(PaymentCycleValue = 1, DayOfWeek.Sunday, IIf(PaymentCycleValue = 2, DayOfWeek.Monday, IIf(PaymentCycleValue = 3, DayOfWeek.Tuesday, IIf(PaymentCycleValue = 4, DayOfWeek.Wednesday, IIf(PaymentCycleValue = 5, DayOfWeek.Thursday, IIf(PaymentCycleValue = 6, DayOfWeek.Friday, DayOfWeek.Saturday))))))
            fromDate.Value = today.AddDays(-dayDiff)
            ToDate.Value = fromDate.Value.AddDays(6)
        End If
        ' End If
        'If clsCommon.myLen(txtMCC.Text) > 0 Then
        '    txtPaymentCycleNo.Text = clsGenratePaymentCycles.GetPaymentCycleNo(txtMCC.Text, dtpToDate.Value)
        '    txtFiscalYear.Text = clsGenratePaymentCycles.GetPaymentFiscalCode(txtMCC.Text, dtpToDate.Value)
        'Else
        '    txtPaymentCycleNo.Text = clsGenratePaymentCycles.GetPaymentCycleNo(fndLoc.Value, dtpToDate.Value)
        '    txtFiscalYear.Text = clsGenratePaymentCycles.GetPaymentFiscalCode(fndLoc.Value, dtpToDate.Value)
        'End If
        'End If
    End Sub
End Class