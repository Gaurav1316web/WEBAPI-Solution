Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsLeaveSetting
#Region "Variables"
    Public Code As String
    Public Name As String
    Public LEAVE_ALLOT_TYPE As Int16
    Public ALLOT_AFTER_MONTHS As Int16
    Public ALLOT_AFTER_DAYS As Int16
    Public LEAVE_AVAIL_TYPE As Int16
    Public AVAIL_AFTER_MONTHS As Int16
    Public AVAIL_AFTER_DAYS As Int16
    Public BAL_ROUND_OFF_TYPE As String
    Public LEAVE_ENCASHED As Int16
    Public MIN_BAL As Double
    Public CARRY_OVER As Int16
    Public CARRY_LOWER_LIM As Double
    Public CARRY_UPPER_LIM As Double
    Public LAPSE_UNAVAILED As Int16
    Public LAPSE_MONTH As String
    Public LAPSE_NEGATIVE As Int16
    Public LAPSE_EXCEEDING As Double
    Public LAPSE_AFTER_DAYS As Double
    Public Location_Code As String
    Public Allot_Periodicity As String
    Public Allot_Type As String
    Public Alloted_Days As Decimal
    Public PerPresentDays As Decimal
    Public AutoAllotDuringSalaryGen As Integer
    Public objList As New List(Of clsLeaveSettingSalSlabLeaveAlloted)
#End Region

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsLeaveSetting
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
            qry = "delete from TSPL_LEAVE_SETTING where LEAVE_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsLeaveSetting
        Dim obj As clsLeaveSetting = Nothing
        Dim qry As String = ""
        Dim whrQry As String = Nothing
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrQry = " And TSPL_LEAVE_SETTING.Location_Code=" + objCommonVar.strCurrUserLocations + ""
        End If
        qry += " select TSPL_LEAVE_SETTING.AutoAllotDuringSalaryGen,TSPL_LEAVE_SETTING.Allot_Periodicity,TSPL_LEAVE_SETTING.Allot_Type,TSPL_LEAVE_SETTING.Alloted_Days,TSPL_LEAVE_SETTING.PerPresentDays,TSPL_LEAVE_SETTING.LEAVE_ALLOT_TYPE,TSPL_LEAVE_SETTING.ALLOT_AFTER_MONTHS,TSPL_LEAVE_SETTING.ALLOT_AFTER_DAYS,TSPL_LEAVE_SETTING.LEAVE_AVAIL_TYPE,TSPL_LEAVE_SETTING.AVAIL_AFTER_MONTHS,TSPL_LEAVE_SETTING.AVAIL_AFTER_DAYS,TSPL_LEAVE_SETTING.BAL_ROUND_OFF_TYPE,TSPL_LEAVE_SETTING.LEAVE_ENCASHED,TSPL_LEAVE_SETTING.MIN_BAL,TSPL_LEAVE_SETTING.CARRY_OVER,TSPL_LEAVE_SETTING.CARRY_LOWER_LIM,TSPL_LEAVE_SETTING.CARRY_UPPER_LIM,TSPL_LEAVE_SETTING.LAPSE_UNAVAILED,TSPL_LEAVE_SETTING.LAPSE_MONTH,TSPL_LEAVE_SETTING.LAPSE_NEGATIVE,TSPL_LEAVE_SETTING.LAPSE_EXCEEDING,TSPL_LEAVE_SETTING.LAPSE_AFTER_DAYS, "
        qry += " TSPL_LEAVE_MASTER.LEAVE_NAME, TSPL_LEAVE_MASTER.LEAVE_CODE,TSPL_LEAVE_SETTING.Location_Code "
        qry += " from TSPL_LEAVE_MASTER "
        qry += " LEFT OUTER JOIN TSPL_LEAVE_SETTING ON TSPL_LEAVE_MASTER.LEAVE_CODE = TSPL_LEAVE_SETTING.LEAVE_CODE "
        qry += " where(2 = 2) " + whrQry
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_LEAVE_MASTER.LEAVE_CODE = (select MIN(LEAVE_CODE) from TSPL_LEAVE_MASTER)"
            Case NavigatorType.Last
                qry += " and TSPL_LEAVE_MASTER.LEAVE_CODE = (select Max(LEAVE_CODE) from TSPL_LEAVE_MASTER)"
            Case NavigatorType.Next
                qry += " and TSPL_LEAVE_MASTER.LEAVE_CODE = (select Min(LEAVE_CODE) from TSPL_LEAVE_MASTER where  LEAVE_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_LEAVE_MASTER.LEAVE_CODE = (select Max(LEAVE_CODE) from TSPL_LEAVE_MASTER where LEAVE_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_LEAVE_MASTER.LEAVE_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsLeaveSetting()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("LEAVE_CODE"))
            obj.Name = clsCommon.myCstr(dt.Rows(0)("LEAVE_NAME"))
            obj.LEAVE_ALLOT_TYPE = Convert.ToInt16(clsCommon.myCdbl(dt.Rows(0)("LEAVE_ALLOT_TYPE")))
            obj.ALLOT_AFTER_MONTHS = Convert.ToInt16(clsCommon.myCdbl(dt.Rows(0)("ALLOT_AFTER_MONTHS")))
            obj.ALLOT_AFTER_DAYS = Convert.ToInt16(clsCommon.myCdbl(dt.Rows(0)("ALLOT_AFTER_DAYS")))
            obj.LEAVE_AVAIL_TYPE = Convert.ToInt16(clsCommon.myCdbl(dt.Rows(0)("LEAVE_AVAIL_TYPE")))
            obj.AVAIL_AFTER_MONTHS = Convert.ToInt16(clsCommon.myCdbl(dt.Rows(0)("AVAIL_AFTER_MONTHS")))
            obj.AVAIL_AFTER_DAYS = Convert.ToInt16(clsCommon.myCdbl(dt.Rows(0)("AVAIL_AFTER_DAYS")))
            obj.LEAVE_ENCASHED = Convert.ToInt16(clsCommon.myCdbl(dt.Rows(0)("LEAVE_ENCASHED")))
            obj.LAPSE_NEGATIVE = Convert.ToInt16(clsCommon.myCdbl(dt.Rows(0)("LAPSE_NEGATIVE")))
            obj.LAPSE_UNAVAILED = Convert.ToInt16(clsCommon.myCdbl(dt.Rows(0)("LAPSE_UNAVAILED")))
            obj.CARRY_OVER = Convert.ToInt16(clsCommon.myCdbl(dt.Rows(0)("CARRY_OVER")))
            obj.BAL_ROUND_OFF_TYPE = clsCommon.myCstr(dt.Rows(0)("BAL_ROUND_OFF_TYPE"))
            obj.LAPSE_MONTH = clsCommon.myCstr(dt.Rows(0)("LAPSE_MONTH"))
            obj.MIN_BAL = clsCommon.myCdbl(dt.Rows(0)("MIN_BAL"))
            obj.CARRY_LOWER_LIM = clsCommon.myCdbl(dt.Rows(0)("CARRY_LOWER_LIM"))
            obj.CARRY_UPPER_LIM = clsCommon.myCdbl(dt.Rows(0)("CARRY_UPPER_LIM"))
            obj.LAPSE_EXCEEDING = clsCommon.myCdbl(dt.Rows(0)("LAPSE_EXCEEDING"))
            obj.LAPSE_AFTER_DAYS = clsCommon.myCdbl(dt.Rows(0)("LAPSE_AFTER_DAYS"))
            '' new columns 
            obj.Allot_Periodicity = clsCommon.myCstr(dt.Rows(0)("Allot_Periodicity"))
            obj.Allot_Type = clsCommon.myCstr(dt.Rows(0)("Allot_Type"))
            obj.Alloted_Days = clsCommon.myCdbl(dt.Rows(0)("Alloted_Days"))
            obj.PerPresentDays = clsCommon.myCdbl(dt.Rows(0)("PerPresentDays"))
            obj.AutoAllotDuringSalaryGen = clsCommon.myCdbl(dt.Rows(0)("AutoAllotDuringSalaryGen"))
            If clsCommon.myLen(dt.Rows(0)("Location_Code")) > 0 Then
                obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            End If
            obj.objList = clsLeaveSettingSalSlabLeaveAlloted.GetData(obj.Code, trans)
        End If
        Return obj


    End Function

    Public Function SaveData(ByVal obj As clsLeaveSetting, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            '' delete TSPL_PAYROLL_SETTING_CTC_DETAIL
            Dim qry As String
            qry = "delete from TSPL_LEAVE_SETTING_SALARY_SLAB_ALLOTED_LEAVE where LEAVE_CODE='" & obj.Code & "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "LEAVE_ALLOT_TYPE", obj.LEAVE_ALLOT_TYPE)
            clsCommon.AddColumnsForChange(coll, "ALLOT_AFTER_MONTHS", obj.ALLOT_AFTER_MONTHS)
            clsCommon.AddColumnsForChange(coll, "ALLOT_AFTER_DAYS", obj.ALLOT_AFTER_DAYS)
            clsCommon.AddColumnsForChange(coll, "LEAVE_AVAIL_TYPE", obj.LEAVE_AVAIL_TYPE)
            clsCommon.AddColumnsForChange(coll, "AVAIL_AFTER_MONTHS ", obj.AVAIL_AFTER_MONTHS)
            clsCommon.AddColumnsForChange(coll, "AVAIL_AFTER_DAYS", obj.AVAIL_AFTER_DAYS)
            clsCommon.AddColumnsForChange(coll, "BAL_ROUND_OFF_TYPE", obj.BAL_ROUND_OFF_TYPE)
            clsCommon.AddColumnsForChange(coll, "LEAVE_ENCASHED", obj.LEAVE_ENCASHED)
            clsCommon.AddColumnsForChange(coll, "CARRY_OVER", obj.CARRY_OVER)
            clsCommon.AddColumnsForChange(coll, "CARRY_LOWER_LIM", obj.CARRY_LOWER_LIM)
            clsCommon.AddColumnsForChange(coll, "CARRY_UPPER_LIM", obj.CARRY_UPPER_LIM)
            clsCommon.AddColumnsForChange(coll, "LAPSE_UNAVAILED", obj.LAPSE_UNAVAILED)
            clsCommon.AddColumnsForChange(coll, "MIN_BAL", obj.MIN_BAL)
            clsCommon.AddColumnsForChange(coll, "LAPSE_MONTH", obj.LAPSE_MONTH)
            clsCommon.AddColumnsForChange(coll, "LAPSE_NEGATIVE", obj.LAPSE_NEGATIVE)
            clsCommon.AddColumnsForChange(coll, "LAPSE_EXCEEDING", obj.LAPSE_EXCEEDING)
            clsCommon.AddColumnsForChange(coll, "LAPSE_AFTER_DAYS", obj.LAPSE_AFTER_DAYS)

            '' new columns 
            clsCommon.AddColumnsForChange(coll, "Allot_Periodicity", obj.Allot_Periodicity, True)
            clsCommon.AddColumnsForChange(coll, "Allot_Type", obj.Allot_Type, True)
            clsCommon.AddColumnsForChange(coll, "Alloted_Days", obj.Alloted_Days, True)
            clsCommon.AddColumnsForChange(coll, "PerPresentDays", obj.PerPresentDays, True)
            clsCommon.AddColumnsForChange(coll, "AutoAllotDuringSalaryGen", obj.AutoAllotDuringSalaryGen, True)

            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "LEAVE_CODE", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                qry = "SELECT Count(*) FROM TSPL_LEAVE_SETTING where LEAVE_CODE= '" & obj.Code & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LEAVE_SETTING", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("This Code Is Already Exist")
                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LEAVE_SETTING", OMInsertOrUpdate.Update, "LEAVE_CODE='" + obj.Code + "'", trans)
            End If
            isSaved = isSaved AndAlso clsLeaveSettingSalSlabLeaveAlloted.SaveData(obj.Code, obj.objList, trans)
            trans.Commit()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function GetYearlyAllotedLeave(ByVal Leave_Code As String, ByVal BasicSal As Decimal, Optional ByVal trans As SqlTransaction = Nothing) As Decimal
        Dim qry As String = "select ALLOTED_LEAVE from TSPL_LEAVE_SETTING_SALARY_SLAB_ALLOTED_LEAVE where LEAVE_CODE='" & Leave_Code & "' and " & BasicSal & " between SALARY_FROM and SALARY_TO"
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
End Class
Public Class clsLeaveSettingSalSlabLeaveAlloted
#Region "Variables"
    Public Leave_Code As String
    Public LINE_NO As Integer
    Public SALARY_FROM As Decimal
    Public SALARY_TO As Decimal
    Public ALLOTED_LEAVE As Decimal

#End Region
    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsLeaveSettingSalSlabLeaveAlloted)
        Dim objList As New List(Of clsLeaveSettingSalSlabLeaveAlloted)
        Dim objTr As New clsLeaveSettingSalSlabLeaveAlloted
        Dim qry As String = ""
        qry += " select * FROM TSPL_LEAVE_SETTING_SALARY_SLAB_ALLOTED_LEAVE WHERE LEAVE_CODE='" & strCode & "' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objTr = New clsLeaveSettingSalSlabLeaveAlloted()
                objTr.Leave_Code = clsCommon.myCstr(dr("Leave_Code"))
                objTr.LINE_NO = clsCommon.myCdbl(dr("LINE_NO"))
                objTr.SALARY_FROM = clsCommon.myCdbl(dr("SALARY_FROM"))
                objTr.SALARY_TO = clsCommon.myCdbl(dr("SALARY_TO"))
                objTr.ALLOTED_LEAVE = clsCommon.myCdbl(dr("ALLOTED_LEAVE"))
                objList.Add(objTr)
            Next

        End If
        Return objList
    End Function

    Public Shared Function SaveData(ByVal strCode As String, ByVal Arr As List(Of clsLeaveSettingSalSlabLeaveAlloted), ByVal trans As SqlTransaction) As Boolean
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsLeaveSettingSalSlabLeaveAlloted In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "LEAVE_CODE", strCode)
                    clsCommon.AddColumnsForChange(coll, "LINE_NO", obj.LINE_NO)
                    clsCommon.AddColumnsForChange(coll, "SALARY_FROM", obj.SALARY_FROM)
                    clsCommon.AddColumnsForChange(coll, "SALARY_TO", obj.SALARY_TO)
                    clsCommon.AddColumnsForChange(coll, "ALLOTED_LEAVE", obj.ALLOTED_LEAVE)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LEAVE_SETTING_SALARY_SLAB_ALLOTED_LEAVE", OMInsertOrUpdate.Insert, "TSPL_LEAVE_SETTING_SALARY_SLAB_ALLOTED_LEAVE.LEAVE_CODE='" + strCode + "'", trans)
                Next
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class