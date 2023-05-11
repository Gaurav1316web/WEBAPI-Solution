Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsPaymentCycleMaster

#Region "Variables"
    Public PC_CODE As String
    Public Description As String
    Public PC_TYPE As String
    Public PC_VALUE As Decimal = 0
    Public comp_code As String
    Public IsDefault As Integer = 0
#End Region

    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select TSPL_PAYMENT_CYCLE_MASTER.PC_CODE as [Code] ,TSPL_PAYMENT_CYCLE_MASTER.PC_TYPE as [PC_TYPE],PC_VALUE as [PC Value] ,TSPL_PAYMENT_CYCLE_MASTER.DESCRIPTION as [Description] ,TSPL_PAYMENT_CYCLE_MASTER.Created_By as [Created By] ,TSPL_PAYMENT_CYCLE_MASTER.Created_Date as [Created Date] ,TSPL_PAYMENT_CYCLE_MASTER.Modified_By as [Modified By] ,TSPL_PAYMENT_CYCLE_MASTER.Modified_Date as [Modified Date]  From TSPL_PAYMENT_CYCLE_MASTER   "
        str = clsCommon.ShowSelectForm("DEPTMSTFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of PC_CODE For Get Finder--------------------------------------------------------------'
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsPaymentCycleMaster
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean

        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = "delete from TSPL_PAYMENT_CYCLE_MASTER where PC_CODE='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)


        Catch ex As Exception

            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsPaymentCycleMaster
        Dim obj As clsPaymentCycleMaster = Nothing
        Dim qry As String = "select PC_CODE, PC_TYPE, DESCRIPTION,PC_VALUE,COMP_CODE,TSPL_PAYMENT_CYCLE_MASTER.IsDefault from TSPL_PAYMENT_CYCLE_MASTER where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and PC_CODE = (select MIN(PC_CODE) from TSPL_PAYMENT_CYCLE_MASTER)"
            Case NavigatorType.Last
                qry += " and PC_CODE = (select Max(PC_CODE) from TSPL_PAYMENT_CYCLE_MASTER)"
            Case NavigatorType.Next
                qry += " and PC_CODE = (select Min(PC_CODE) from TSPL_PAYMENT_CYCLE_MASTER where  PC_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and PC_CODE = (select Max(PC_CODE) from TSPL_PAYMENT_CYCLE_MASTER where PC_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and PC_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsPaymentCycleMaster()
            obj.PC_CODE = clsCommon.myCstr(dt.Rows(0)("PC_CODE"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))
            obj.PC_TYPE = clsCommon.myCstr(dt.Rows(0)("PC_TYPE"))
            obj.PC_VALUE = clsCommon.myCdbl(dt.Rows(0)("PC_VALUE"))
            obj.comp_code = clsCommon.myCstr(dt.Rows(0)("comp_code"))
            obj.IsDefault = clsCommon.myCdbl(dt.Rows(0)("IsDefault"))
        End If
        Return obj


    End Function

    Public Function SaveData(ByVal obj As clsPaymentCycleMaster, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try
            If obj.IsDefault = 1 Then
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_CYCLE_MASTER set IsDefault=0 where IsDefault=1")
            End If
            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "PC_TYPE", obj.PC_TYPE)
            clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.Description)
            clsCommon.AddColumnsForChange(coll, "PC_VALUE", obj.PC_VALUE)
            clsCommon.AddColumnsForChange(coll, "IsDefault", obj.IsDefault)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
            If isNewEntry Then
                If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False)) Then
                    Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_PAYMENT_CYCLE_MASTER where PC_CODE='" & obj.PC_CODE & "'")
                    If ChkNewEntry = 0 Then
                        obj.PC_CODE = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"), clsDocType.PaymentCycleMaster, "", "")
                        If clsCommon.myLen(obj.PC_CODE) <= 0 Then
                            Throw New Exception("Error in Code Generation")
                        End If
                    End If
                End If
                clsCommon.AddColumnsForChange(coll, "PC_CODE", obj.PC_CODE)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
                Dim qry As String = "SELECT Count(*) FROM TSPL_PAYMENT_CYCLE_MASTER where PC_CODE= '" & obj.PC_CODE & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PAYMENT_CYCLE_MASTER", OMInsertOrUpdate.Insert, "")
                Else
                    Throw New Exception("Code Already Exist")
                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PAYMENT_CYCLE_MASTER", OMInsertOrUpdate.Update, "PC_CODE='" + obj.PC_CODE + "'")
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetCodeByPC_TYPE(ByVal strPC_TYPE As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = " Select PC_CODE from TSPL_PAYMENT_CYCLE_MASTER where PC_TYPE = '" + strPC_TYPE + "' "
        Dim StrCode As String = clsDBFuncationality.getSingleValue(qry, trans)
        Return StrCode
    End Function
    Public Shared Function CheckNewEntry(ByVal PC_CODE As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = "select PC_CODE from TSPL_PAYMENT_CYCLE_MASTER where PC_CODE ='" + PC_CODE + "'   "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If

    End Function
    Public Shared Function GetDefault(Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = "Select PC_Code from TSPL_PAYMENT_CYCLE_MASTER where IsDefault=1"
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function

    Public Shared Function GetPaymentCycleToDate(ByVal MCCCode As String, ByVal txtFromDate As DateTime) As DateTime

        Dim txtToDate As DateTime = txtFromDate
        Dim SettMPIncentiveEntryApplyMonthly As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MPIncentiveEntryApplyMonthly, clsFixedParameterCode.MPIncentiveEntryApplyMonthly, Nothing)) > 0)
        Dim PaymentCycleType As String = ""
        Dim PaymentCycleValue As Integer = 0

        If clsCommon.myLen(MCCCode) <= 0 Then
            Throw New Exception("Please select the MCC first")
        End If

        If SettMPIncentiveEntryApplyMonthly Then
            txtFromDate = New DateTime(txtFromDate.Year, txtFromDate.Month, 1)
            txtToDate = txtFromDate.AddMonths(1).AddDays(-1)
        Else
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(" select TSPL_MCC_MASTER.Payment_Cycle,TSPL_PAYMENT_CYCLE_MASTER.PC_TYPE,TSPL_PAYMENT_CYCLE_MASTER.PC_VALUE  from TSPL_MCC_MASTER left outer join TSPL_PAYMENT_CYCLE_MASTER on TSPL_PAYMENT_CYCLE_MASTER.PC_CODE=TSPL_MCC_MASTER.Payment_Cycle   where TSPL_MCC_MASTER.MCC_Code  in ('" & MCCCode & "') ")

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Payment Cycle found on current MCC/Location")
            End If

            PaymentCycleType = clsCommon.myCstr(dt.Rows(0)("PC_TYPE"))
            PaymentCycleValue = CInt(clsCommon.myCdbl(dt.Rows(0)("PC_VALUE")))
            Dim dtCurr As DateTime = clsCommon.GETSERVERDATE()

            If clsCommon.CompairString(PaymentCycleType, "Day") = CompairStringResult.Equal Then

                If (txtFromDate.Day Mod PaymentCycleValue <> 1) AndAlso (PaymentCycleValue) <> 1 Then
                    Throw New Exception("FromDate can only be first day of month or at interval of " & PaymentCycleValue & " Day, Because MCC has payment Cycle of " & PaymentCycleValue & " Day ")
                End If

                txtToDate = txtFromDate.AddDays(PaymentCycleValue - 1)
                If txtFromDate.Month <> txtToDate.Month Then txtToDate = New DateTime(txtFromDate.Year, txtFromDate.Month, 1).AddMonths(1).AddDays(-1)
                Dim dtNxtPay As DateTime = txtToDate.AddDays(Math.Ceiling(PaymentCycleValue / 2.0))
                If txtFromDate.Month <> dtNxtPay.Month Then txtToDate = New DateTime(txtFromDate.Year, txtFromDate.Month, 1).AddMonths(1).AddDays(-1)
            ElseIf clsCommon.CompairString(PaymentCycleType, "Month") = CompairStringResult.Equal Then

                If clsCommon.myCdbl(clsCommon.GetPrintDate(txtFromDate, "dd")) <> 1 Then
                    Throw New Exception("FromDate can only be first day of month, Because MCC has payment Cycle of Month Type")
                End If
            ElseIf clsCommon.CompairString(PaymentCycleType, "Year") = CompairStringResult.Equal Then

                If clsCommon.myCdbl(clsCommon.GetPrintDate(txtFromDate, "dd")) <> 1 Then
                    Throw New Exception("FromDate can only be first day of month, Because MCC has payment Cycle of Year Type")
                End If

                txtToDate = New DateTime(txtFromDate.Year, 1, 1).AddYears(1).AddDays(-1)
            ElseIf clsCommon.CompairString(PaymentCycleType, "Week") = CompairStringResult.Equal Then
                Dim today As DateTime = txtFromDate
                Dim dayDiff As Integer = today.DayOfWeek - (If(PaymentCycleValue = 1, DayOfWeek.Sunday, (If(PaymentCycleValue = 2, DayOfWeek.Monday, (If(PaymentCycleValue = 3, DayOfWeek.Tuesday, (If(PaymentCycleValue = 4, DayOfWeek.Wednesday, (If(PaymentCycleValue = 5, DayOfWeek.Thursday, (If(PaymentCycleValue = 6, DayOfWeek.Friday, DayOfWeek.Saturday))))))))))))
                txtFromDate = today.AddDays(-dayDiff)
                txtToDate = txtFromDate.AddDays(6)
            End If
        End If

        Return txtToDate
    End Function
End Class

Public Class clsGenratePaymentCycles
    Public Shared Function SaveData(ByVal strFiscalYear As String, ByVal arrMCC As ArrayList) As Boolean
        Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim dtStartFiscalYear As Date
            Dim dtEndFiscalYear As Date
            Dim dtStart As Date
            Dim dtEnd As Date
            Dim qry As String = " select Fiscal_Code,Fiscal_Name,Start_Date,End_Date from TSPL_FISCAL_YEAR_MASTER where Fiscal_Code='" + strFiscalYear + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                dtStartFiscalYear = clsCommon.myCDate(dt.Rows(0)("Start_Date"))
                dtEndFiscalYear = clsCommon.myCDate(dt.Rows(0)("End_Date"))
                For Each strMCC As String In arrMCC
                    dtStart = dtStartFiscalYear
                    qry = "select top 1 1 from TSPL_PAYMENT_CYCLE_GENERATED WHERE Fiscal_Code='" + strFiscalYear + "' and MCC_Code='" + strMCC + "'"
                    Dim dtTemp As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
                    If dtTemp Is Nothing OrElse dtTemp.Rows.Count <= 0 Then
                        Dim ii As Integer = 1
                        While dtStart < dtEndFiscalYear
                            qry = "select Pc_Type as Type,PC_VALUE as Value, case when Pc_Type='Day' then PC_VALUE when PC_Type='Month' then PC_Value * " & Date.DaysInMonth(dtStart.Year, dtStart.Month) & " end " _
              & " as Pc_Value from tspl_Mcc_master inner join TSPL_PAYMENT_CYCLE_MASTER  on tspl_Mcc_master.payment_cycle=TSPL_PAYMENT_CYCLE_MASTER.PC_CODE where Mcc_code='" & strMCC & "'"
                            dtTemp = clsDBFuncationality.GetDataTable(qry, tran)
                            If dtTemp Is Nothing OrElse dtTemp.Rows.Count <= 0 Then
                                Throw New Exception("Please set payment cycle For Mcc [" + strMCC + "]")
                            End If
                            Dim PaymentType As String = clsCommon.myCstr(dtTemp.Rows(0)("Type"))
                            Dim PaymentValue As Decimal = clsCommon.myCdbl(dtTemp.Rows(0)("Value"))
                            If clsCommon.CompairString(clsCommon.myCstr(dtTemp.Rows(0)("Type")), "Week") = CompairStringResult.Equal Then
                                Dim today As Date = dtStart
                                Dim dayDiff As Integer = today.DayOfWeek - IIf(PaymentValue = 1, DayOfWeek.Sunday, IIf(PaymentValue = 2, DayOfWeek.Monday, IIf(PaymentValue = 3, DayOfWeek.Tuesday, IIf(PaymentValue = 4, DayOfWeek.Wednesday, IIf(PaymentValue = 5, DayOfWeek.Thursday, IIf(PaymentValue = 6, DayOfWeek.Friday, DayOfWeek.Saturday))))))
                                dtStart = today.AddDays(-dayDiff)
                                dtEnd = dtStart.AddDays(6)
                            Else
                                Dim PaymentCycleValue As Integer = dtTemp.Rows(0)("Pc_Value")
                                If dtStart.Day Mod PaymentCycleValue <> 1 And (Not PaymentCycleValue = 1) Then
                                    clsCommon.MyMessageBoxShow("Invalid date.Date should be multiple of " & clsCommon.myCstr(PaymentCycleValue) & " + 1 ")
                                End If
                                dtEnd = dtStart.AddDays(PaymentCycleValue - 1)
                                If dtStart.Month <> dtEnd.Month Then
                                    dtEnd = New Date(dtStart.Year, dtStart.Month, 1).AddMonths(1).AddDays(-1)
                                End If
                                Dim dtNxtPay As DateTime = dtEnd.AddDays(Math.Ceiling(PaymentCycleValue / 2.0))
                                If dtStart.Month <> dtNxtPay.Month Then
                                    dtEnd = New Date(dtStart.Year, dtStart.Month, 1).AddMonths(1).AddDays(-1)
                                End If
                            End If



                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "Code", clsERPFuncationality.GetNextCode(tran, dtStartFiscalYear, clsDocType.Detail, clsDocTransactionType.Detail, ""))
                            clsCommon.AddColumnsForChange(coll, "Name", ii)
                            clsCommon.AddColumnsForChange(coll, "Fiscal_Code", strFiscalYear)
                            clsCommon.AddColumnsForChange(coll, "MCC_Code", strMCC)
                            clsCommon.AddColumnsForChange(coll, "From_Date", clsCommon.GetPrintDate(dtStart, "dd/MMM/yyyy"))
                            clsCommon.AddColumnsForChange(coll, "To_Date", clsCommon.GetPrintDate(dtEnd, "dd/MMM/yyyy"))
                            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(tran), "dd/MMM/yyyy"))
                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PAYMENT_CYCLE_GENERATED", OMInsertOrUpdate.Insert, "", tran)
                            ii += 1
                            dtStart = dtEnd.AddDays(1)
                        End While
                    End If
                Next
            End If
            tran.Commit()
        Catch ex As Exception
            tran.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetPaymentCycleNo(ByVal strMccCode As String, ByVal strDate As Date) As String
        Dim qry As String = " select TSPL_PAYMENT_CYCLE_GENERATED.Name as CycleNo
            from TSPL_PAYMENT_CYCLE_GENERATED 
            left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code 
            left outer join TSPL_Location_MASTER on TSPL_Location_MASTER.Location_Code = TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code
			WHERE convert (date,'" + strDate + "',103) between From_Date and To_Date 
			and ( TSPL_Location_MASTER.Location_Code in ('" + strMccCode + "') or TSPL_Location_MASTER.Loc_Segment_Code in ('" + strMccCode + "') )"
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
    End Function

    Public Shared Function GetPaymentFiscalCode(ByVal strMccCode As String, ByVal strDate As Date) As String
        Dim qry As String = " select TSPL_PAYMENT_CYCLE_GENERATED.Fiscal_Code
            from TSPL_PAYMENT_CYCLE_GENERATED 
            left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code 
			left outer join TSPL_Location_MASTER on TSPL_Location_MASTER.Location_Code = TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code
            WHERE convert (date,'" + strDate + "',103) between From_Date and To_Date 
			and ( TSPL_Location_MASTER.Location_Code in ('" + strMccCode + "') or TSPL_Location_MASTER.Loc_Segment_Code in ('" + strMccCode + "') )"
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
    End Function

End Class
