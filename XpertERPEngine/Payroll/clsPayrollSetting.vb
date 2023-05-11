Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsPayrollSetting

#Region "Variables"
    Public PAY_SETTING_CODE As String
    Public APPLY_COMMON_SETTINGS_ALL_LOCATIONS As Boolean
    Public LOCATION_CODE As String
    Public ATTENDANCE_AUTO_GENERATE As Boolean
    Public LEAVE_ALLOTMENT As String

    Public MIN_HOURS_HFDAY As Decimal
    Public MIN_HOURS_OT As Decimal
    Public MAX_HOURS_OT As Decimal
    Public TREAT_WKOFF_LEAVE_CONTINUOUS As Boolean
    Public MAX_MINT_LTCOMING As Decimal

    Public NO_LTCOMING_SL As Decimal
    Public NO_SHORTLEAVE_HFDAY As Decimal
    Public REMOVE_FROM_ATTENDANCE_ZERO_PRESENT_DAYS As Boolean
    Public WORKING_HOURS As Decimal
    Public MIN_PRESENT_DAYS_FOR_WEEK_OFF As Decimal
    Public INTERVAL_MINUTES As Decimal
    Public EARLY_ARRIVAL_MINUTES_OT As Decimal
    Public STATUTORY_WEEK_WORKING_HOURS As Double
    Public Gratuity_Period As Double
    Public STATUTORY_DAILY_WORKING_HOURS As Decimal

    Public fromTime As String
    Public ToTime As String

    Public objListCTC As New List(Of clsPayrollSetting_CTC_Detail)
    Public objListGROSS As New List(Of clsPayrollSetting_GROSS_Detail)
    Public objListInHand As New List(Of clsPayrollSetting_SAL_IN_HAND_Detail)
#End Region
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select PAY_SETTING_CODE as Code,LOCATION_CODE as [Loation Code] from tspl_payroll_setting  "
        str = clsCommon.ShowSelectForm("paysett", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    Public Shared Function CheckNewEntry(ByVal Code As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = "select PAY_SETTING_CODE from TSPL_PAYROLL_SETTING where PAY_SETTING_CODE ='" + Code + "'   "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If

    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsPayrollSetting
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = True
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
            Dim qry As String
            qry = "delete from TSPL_PAYROLL_SETTING_CTC_DETAIL where PAY_SETTING_CODE ='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PAYROLL_SETTING_GROSS_DETAIL where PAY_SETTING_CODE ='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PAYROLL_SETTING_INHAND_DETAIL where PAY_SETTING_CODE ='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PAYROLL_SETTING where PAY_SETTING_CODE ='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception

            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsPayrollSetting
        Dim obj As clsPayrollSetting = Nothing
        Dim qry As String = ""

        qry += " select * from TSPL_PAYROLL_SETTING "
        qry += " where(2 = 2) "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_PAYROLL_SETTING.PAY_SETTING_CODE = (select MIN(PAY_SETTING_CODE) from TSPL_PAYROLL_SETTING)"
            Case NavigatorType.Last
                qry += " and TSPL_PAYROLL_SETTING.PAY_SETTING_CODE = (select Max(PAY_SETTING_CODE) from TSPL_PAYROLL_SETTING)"
            Case NavigatorType.Next
                qry += " and TSPL_PAYROLL_SETTING.PAY_SETTING_CODE = (select Min(PAY_SETTING_CODE) from TSPL_PAYROLL_SETTING where  PAY_SETTING_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_PAYROLL_SETTING.PAY_SETTING_CODE = (select Max(PAY_SETTING_CODE) from TSPL_PAYROLL_SETTING where PAY_SETTING_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_PAYROLL_SETTING.PAY_SETTING_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsPayrollSetting()
            obj.PAY_SETTING_CODE = clsCommon.myCstr(dt.Rows(0)("PAY_SETTING_CODE"))
            obj.APPLY_COMMON_SETTINGS_ALL_LOCATIONS = clsCommon.myCBool(dt.Rows(0)("APPLY_COMMON_SETTINGS_ALL_LOCATIONS"))
            obj.LOCATION_CODE = clsCommon.myCstr(dt.Rows(0)("LOCATION_CODE"))
            obj.ATTENDANCE_AUTO_GENERATE = clsCommon.myCBool(dt.Rows(0)("ATTENDANCE_AUTO_GENERATE"))
            obj.MIN_HOURS_HFDAY = clsCommon.myCdbl(dt.Rows(0)("MIN_HOURS_HFDAY"))
            obj.MIN_HOURS_OT = clsCommon.myCdbl(dt.Rows(0)("MIN_HOURS_OT"))
            obj.MAX_HOURS_OT = clsCommon.myCdbl(dt.Rows(0)("MAX_HOURS_OT"))
            obj.TREAT_WKOFF_LEAVE_CONTINUOUS = clsCommon.myCBool(dt.Rows(0)("TREAT_WKOFF_LEAVE_CONTINUOUS"))
            obj.MAX_MINT_LTCOMING = clsCommon.myCdbl(dt.Rows(0)("MAX_MINT_LTCOMING"))
            obj.NO_LTCOMING_SL = clsCommon.myCdbl(dt.Rows(0)("NO_LTCOMING_SL"))
            obj.EARLY_ARRIVAL_MINUTES_OT = clsCommon.myCdbl(dt.Rows(0)("EARLY_ARRIVAL_MINUTES_OT"))
            obj.INTERVAL_MINUTES = clsCommon.myCdbl(dt.Rows(0)("INTERVAL_MINUTES"))
            obj.REMOVE_FROM_ATTENDANCE_ZERO_PRESENT_DAYS = clsCommon.myCBool(dt.Rows(0)("REMOVE_FROM_ATTENDANCE_ZERO_PRESENT_DAYS"))
            obj.LEAVE_ALLOTMENT = clsCommon.myCstr(dt.Rows(0)("LEAVE_ALLOTMENT"))
            obj.STATUTORY_DAILY_WORKING_HOURS = clsCommon.myCdbl(dt.Rows(0)("STATUTORY_DAILY_WORKING_HOURS"))
            '==shivani
            obj.Gratuity_Period = clsCommon.myCdbl(dt.Rows(0)("Gratuity_Period"))
            '======
            obj.NO_SHORTLEAVE_HFDAY = clsCommon.myCdbl(dt.Rows(0)("NO_SHORTLEAVE_HFDAY"))
            obj.WORKING_HOURS = clsCommon.myCdbl(dt.Rows(0)("WORKING_HOURS"))
            obj.MIN_PRESENT_DAYS_FOR_WEEK_OFF = clsCommon.myCdbl(dt.Rows(0)("MIN_PRESENT_DAYS_FOR_WEEK_OFF"))
            obj.STATUTORY_WEEK_WORKING_HOURS = clsCommon.myCdbl(dt.Rows(0)("STATUTORY_WEEK_WORKING_HOURS"))

            If dt.Rows(0)("FirstHalf_from") IsNot DBNull.Value Then
                obj.fromTime = clsCommon.myCDate(dt.Rows(0)("FirstHalf_from"))
            End If
            If dt.Rows(0)("FirstHalf_to_time") IsNot DBNull.Value Then
                obj.ToTime = clsCommon.myCDate(dt.Rows(0)("FirstHalf_to_time"))
            End If



            obj.objListCTC = clsPayrollSetting_CTC_Detail.GetData(obj.PAY_SETTING_CODE, trans)
            obj.objListGROSS = clsPayrollSetting_GROSS_Detail.GetData(obj.PAY_SETTING_CODE, trans)
            obj.objListInHand = clsPayrollSetting_SAL_IN_HAND_Detail.GetData(obj.PAY_SETTING_CODE, trans)
        End If
        Return obj
    End Function

    Public Function SaveData(ByVal strCode As String, ByVal obj As clsPayrollSetting, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = ""

            If isNewEntry Then
                If strCode = "" Then
                    obj.PAY_SETTING_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(clsCommon.GETSERVERDATE(trans)), clsDocType.PayrollSetting, "", "")
                    strCode = obj.PAY_SETTING_CODE
                Else
                    obj.PAY_SETTING_CODE = strCode
                End If
            End If


            '' delete TSPL_PAYROLL_SETTING_CTC_DETAIL
            qry = "delete from TSPL_PAYROLL_SETTING_CTC_DETAIL where PAY_SETTING_CODE='" & strCode & "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            '' delete TSPL_PAYROLL_SETTING_CTC_DETAIL
            qry = "delete from TSPL_PAYROLL_SETTING_GROSS_DETAIL where PAY_SETTING_CODE='" & strCode & "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            '' delete TSPL_PAYROLL_SETTING_CTC_DETAIL
            qry = "delete from TSPL_PAYROLL_SETTING_INHAND_DETAIL where PAY_SETTING_CODE='" & strCode & "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "ATTENDANCE_AUTO_GENERATE", obj.ATTENDANCE_AUTO_GENERATE)
            clsCommon.AddColumnsForChange(coll, "APPLY_COMMON_SETTINGS_ALL_LOCATIONS", obj.APPLY_COMMON_SETTINGS_ALL_LOCATIONS)
            clsCommon.AddColumnsForChange(coll, "MIN_HOURS_HFDAY", obj.MIN_HOURS_HFDAY)
            clsCommon.AddColumnsForChange(coll, "MIN_HOURS_OT", obj.MIN_HOURS_OT)
            clsCommon.AddColumnsForChange(coll, "MAX_HOURS_OT", obj.MAX_HOURS_OT)
            clsCommon.AddColumnsForChange(coll, "TREAT_WKOFF_LEAVE_CONTINUOUS ", obj.TREAT_WKOFF_LEAVE_CONTINUOUS)
            clsCommon.AddColumnsForChange(coll, "MAX_MINT_LTCOMING", obj.MAX_MINT_LTCOMING)
            clsCommon.AddColumnsForChange(coll, "LEAVE_ALLOTMENT", obj.LEAVE_ALLOTMENT)
            clsCommon.AddColumnsForChange(coll, "NO_LTCOMING_SL", obj.NO_LTCOMING_SL)
            clsCommon.AddColumnsForChange(coll, "REMOVE_FROM_ATTENDANCE_ZERO_PRESENT_DAYS", obj.REMOVE_FROM_ATTENDANCE_ZERO_PRESENT_DAYS)
            clsCommon.AddColumnsForChange(coll, "WORKING_HOURS", obj.WORKING_HOURS)
            clsCommon.AddColumnsForChange(coll, "LOCATION_CODE", obj.LOCATION_CODE, True)
            clsCommon.AddColumnsForChange(coll, "MIN_PRESENT_DAYS_FOR_WEEK_OFF", obj.MIN_PRESENT_DAYS_FOR_WEEK_OFF)
            clsCommon.AddColumnsForChange(coll, "INTERVAL_MINUTES", obj.INTERVAL_MINUTES)
            clsCommon.AddColumnsForChange(coll, "NO_SHORTLEAVE_HFDAY", obj.NO_SHORTLEAVE_HFDAY)
            clsCommon.AddColumnsForChange(coll, "STATUTORY_DAILY_WORKING_HOURS", obj.STATUTORY_DAILY_WORKING_HOURS)
            clsCommon.AddColumnsForChange(coll, "Gratuity_Period", obj.Gratuity_Period)
            clsCommon.AddColumnsForChange(coll, "EARLY_ARRIVAL_MINUTES_OT", obj.EARLY_ARRIVAL_MINUTES_OT)
            clsCommon.AddColumnsForChange(coll, "STATUTORY_WEEK_WORKING_HOURS", obj.STATUTORY_WEEK_WORKING_HOURS)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)

            clsCommon.AddColumnsForChange(coll, "FirstHalf_from", obj.fromTime)
            clsCommon.AddColumnsForChange(coll, "FirstHalf_to_time", obj.ToTime)

            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "PAY_SETTING_CODE", obj.PAY_SETTING_CODE)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                qry = "SELECT Count(*) FROM TSPL_PAYROLL_SETTING where PAY_SETTING_CODE= '" & obj.PAY_SETTING_CODE & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PAYROLL_SETTING", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("This Code Is Already Exist")

                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PAYROLL_SETTING", OMInsertOrUpdate.Update, "PAY_SETTING_CODE='" + obj.PAY_SETTING_CODE + "'", trans)
            End If
            isSaved = isSaved AndAlso clsPayrollSetting_CTC_Detail.SaveData(obj.PAY_SETTING_CODE, obj.objListCTC, trans)
            isSaved = isSaved AndAlso clsPayrollSetting_GROSS_Detail.SaveData(obj.PAY_SETTING_CODE, obj.objListGROSS, trans)
            isSaved = isSaved AndAlso clsPayrollSetting_SAL_IN_HAND_Detail.SaveData(obj.PAY_SETTING_CODE, obj.objListInHand, trans)
            trans.Commit()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function GetPayrollSetting(ByVal Location_Code As String, Optional ByVal trans As SqlTransaction = Nothing) As clsPayrollSetting
        Dim qry As String
        Dim obj As clsPayrollSetting = Nothing
        qry = "select PAY_SETTING_CODE from TSPL_PAYROLL_SETTING where LOCATION_CODE='" & Location_Code & "'"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            obj = clsPayrollSetting.GetData(dt.Rows(0).Item("PAY_SETTING_CODE"), NavigatorType.Current, trans)
        Else
            qry = "select PAY_SETTING_CODE from TSPL_PAYROLL_SETTING where APPLY_COMMON_SETTINGS_ALL_LOCATIONS=1"
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt.Rows.Count > 0 Then
                obj = clsPayrollSetting.GetData(dt.Rows(0).Item("PAY_SETTING_CODE"), NavigatorType.Current, trans)
            Else
                clsCommon.MyMessageBoxShow("Payroll setting not found")
            End If
        End If
        Return obj
    End Function

End Class

Public Class clsPayrollSetting_CTC_Detail
#Region "Variables"
    Public PAY_SETTING_CODE As String
    Public PAY_HEAD_CODE As String
#End Region

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsPayrollSetting_CTC_Detail)
        Dim objList As New List(Of clsPayrollSetting_CTC_Detail)
        Dim objTr As New clsPayrollSetting_CTC_Detail
        Dim qry As String = ""
        qry += " select * FROM TSPL_PAYROLL_SETTING_CTC_DETAIL WHERE PAY_SETTING_CODE='" & strCode & "' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objTr = New clsPayrollSetting_CTC_Detail()
                objTr.PAY_SETTING_CODE = clsCommon.myCstr(dr("PAY_SETTING_CODE"))
                objTr.PAY_HEAD_CODE = clsCommon.myCstr(dr("PAY_HEAD_CODE"))
                objList.Add(objTr)
            Next

        End If
        Return objList
    End Function

    Public Shared Function SaveData(ByVal strCode As String, ByVal Arr As List(Of clsPayrollSetting_CTC_Detail), ByVal trans As SqlTransaction) As Boolean

        Try

            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsPayrollSetting_CTC_Detail In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "PAY_SETTING_CODE", strCode)
                    clsCommon.AddColumnsForChange(coll, "PAY_HEAD_CODE", obj.PAY_HEAD_CODE)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PAYROLL_SETTING_CTC_DETAIL", OMInsertOrUpdate.Insert, "TSPL_PAYROLL_SETTING_CTC_DETAIL.PAY_SETTING_CODE='" + strCode + "'", trans)
                Next
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class


Public Class clsPayrollSetting_GROSS_Detail
#Region "Variables"
    Public PAY_SETTING_CODE As String
    Public PAY_HEAD_CODE As String
#End Region

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsPayrollSetting_GROSS_Detail)
        Dim objList As New List(Of clsPayrollSetting_GROSS_Detail)
        Dim objTr As New clsPayrollSetting_GROSS_Detail
        Dim qry As String = ""
        qry += " select * FROM TSPL_PAYROLL_SETTING_GROSS_DETAIL WHERE PAY_SETTING_CODE='" & strCode & "' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objTr = New clsPayrollSetting_GROSS_Detail()
                objTr.PAY_SETTING_CODE = clsCommon.myCstr(dr("PAY_SETTING_CODE"))
                objTr.PAY_HEAD_CODE = clsCommon.myCstr(dr("PAY_HEAD_CODE"))
                objList.Add(objTr)
            Next

        End If
        Return objList
    End Function

    Public Shared Function SaveData(ByVal strCode As String, ByVal Arr As List(Of clsPayrollSetting_GROSS_Detail), ByVal trans As SqlTransaction) As Boolean
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsPayrollSetting_GROSS_Detail In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "PAY_SETTING_CODE", strCode)
                    clsCommon.AddColumnsForChange(coll, "PAY_HEAD_CODE", obj.PAY_HEAD_CODE)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PAYROLL_SETTING_GROSS_DETAIL", OMInsertOrUpdate.Insert, "TSPL_PAYROLL_SETTING_GROSS_DETAIL.PAY_SETTING_CODE='" + strCode + "'", trans)
                Next
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class


Public Class clsPayrollSetting_SAL_IN_HAND_Detail
#Region "Variables"
    Public PAY_SETTING_CODE As String
    Public PAY_HEAD_CODE As String
#End Region

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsPayrollSetting_SAL_IN_HAND_Detail)
        Dim objList As New List(Of clsPayrollSetting_SAL_IN_HAND_Detail)
        Dim objTr As New clsPayrollSetting_SAL_IN_HAND_Detail
        Dim qry As String = ""
        qry += " select * FROM TSPL_PAYROLL_SETTING_INHAND_DETAIL WHERE PAY_SETTING_CODE='" & strCode & "' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objTr = New clsPayrollSetting_SAL_IN_HAND_Detail()
                objTr.PAY_SETTING_CODE = clsCommon.myCstr(dr("PAY_SETTING_CODE"))
                objTr.PAY_HEAD_CODE = clsCommon.myCstr(dr("PAY_HEAD_CODE"))
                objList.Add(objTr)
            Next

        End If
        Return objList
    End Function

    Public Shared Function SaveData(ByVal strCode As String, ByVal Arr As List(Of clsPayrollSetting_SAL_IN_HAND_Detail), ByVal trans As SqlTransaction) As Boolean
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsPayrollSetting_SAL_IN_HAND_Detail In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "PAY_SETTING_CODE", strCode)
                    clsCommon.AddColumnsForChange(coll, "PAY_HEAD_CODE", obj.PAY_HEAD_CODE)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PAYROLL_SETTING_INHAND_DETAIL", OMInsertOrUpdate.Insert, "TSPL_PAYROLL_SETTING_INHAND_DETAIL.PAY_SETTING_CODE='" + strCode + "'", trans)
                Next
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class