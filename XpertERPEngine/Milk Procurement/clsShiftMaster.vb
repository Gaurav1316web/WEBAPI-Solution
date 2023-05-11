Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsShiftMaster

#Region "Variables"
    Public Code As String
    Public FromTime As String
    Public ToTime As String
    Public Name As String
    Public INTERVAL_Time As String
    Public FSTHALF_ADJUST_MIN As Int16
    Public SECHALF_ADJUST_MIN As Int16

#End Region

    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "  select TSPL_SHIFT_MASTER.SHIFT_CODE as [Code] ,TSPL_SHIFT_MASTER.SHIFT_NAME as [Shift Name]  ,TSPL_SHIFT_MASTER.Created_By as [Created By] ,TSPL_SHIFT_MASTER.Created_Date as [Created Date] ,TSPL_SHIFT_MASTER.Modified_By as [Modified By] ,TSPL_SHIFT_MASTER.Modified_Date as [Modified Date] ,TSPL_SHIFT_MASTER.FROM_Time as [From Time] ,TSPL_SHIFT_MASTER.TO_Time as [To Time] ,TSPL_SHIFT_MASTER.INTERVAL_Time as [Interval Time] ,TSPL_SHIFT_MASTER.FSTHALF_ADJUST_MIN as [Fst Half Adjust Min] ,TSPL_SHIFT_MASTER.SECHALF_ADJUST_MIN as [Second Half Adjust Min]  From TSPL_SHIFT_MASTER  "

        str = clsCommon.ShowSelectForm("SHFTMST", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsShiftMaster
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
            qry = "delete from TSPL_SHIFT_MASTER where SHIFT_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)


        Catch ex As Exception

            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsShiftMaster
        Dim obj As clsShiftMaster = Nothing
        Dim qry As String = "select * from TSPL_SHIFT_MASTER where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and SHIFT_CODE = (select MIN(SHIFT_CODE) from TSPL_SHIFT_MASTER)"
            Case NavigatorType.Last
                qry += " and SHIFT_CODE = (select Max(SHIFT_CODE) from TSPL_SHIFT_MASTER)"
            Case NavigatorType.Next
                qry += " and SHIFT_CODE = (select Min(SHIFT_CODE) from TSPL_SHIFT_MASTER where  SHIFT_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and SHIFT_CODE = (select Max(SHIFT_CODE) from TSPL_SHIFT_MASTER where SHIFT_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and SHIFT_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsShiftMaster()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("SHIFT_CODE"))
            obj.ToTime = clsCommon.myCstr(dt.Rows(0)("TO_Time"))
            obj.FromTime = clsCommon.myCstr(dt.Rows(0)("FROM_Time"))
            obj.Name = clsCommon.myCstr(dt.Rows(0)("SHIFT_NAME"))
            obj.INTERVAL_Time = clsCommon.myCstr(dt.Rows(0)("INTERVAL_Time"))
            obj.FSTHALF_ADJUST_MIN = clsCommon.myCstr(dt.Rows(0)("FSTHALF_ADJUST_MIN"))
            obj.SECHALF_ADJUST_MIN = clsCommon.myCstr(dt.Rows(0)("SECHALF_ADJUST_MIN"))
        End If
        Return obj
    End Function

    Public Function SaveData(ByVal obj As clsShiftMaster, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "SHIFT_NAME", obj.Name)
            clsCommon.AddColumnsForChange(coll, "TO_Time", obj.ToTime)
            clsCommon.AddColumnsForChange(coll, "From_Time", obj.FromTime)
            clsCommon.AddColumnsForChange(coll, "INTERVAL_Time", obj.INTERVAL_Time)
            clsCommon.AddColumnsForChange(coll, "FSTHALF_ADJUST_MIN", obj.FSTHALF_ADJUST_MIN)
            clsCommon.AddColumnsForChange(coll, "SECHALF_ADJUST_MIN", obj.SECHALF_ADJUST_MIN)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
            If isNewEntry Then
                If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False)) Then
                    Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_SHIFT_MASTER where SHIFT_CODE='" & obj.Code & "'")
                    If ChkNewEntry = 0 Then
                        obj.Code = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"), clsDocType.ShiftMaster, "", "")
                        If clsCommon.myLen(obj.Code) <= 0 Then
                            Throw New Exception("Error in Code Generation")
                        End If
                    End If
                End If
                clsCommon.AddColumnsForChange(coll, "SHIFT_CODE", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
                Dim qry As String = "SELECT Count(*) FROM TSPL_SHIFT_MASTER where SHIFT_CODE= '" & obj.Code & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHIFT_MASTER", OMInsertOrUpdate.Insert, "")
                Else
                    Throw New Exception("This Code Is Already Exist")

                End If

            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHIFT_MASTER", OMInsertOrUpdate.Update, "SHIFT_CODE='" + obj.Code + "'")
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function CheckNewEntry(ByVal Code As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = "select SHIFT_CODE from TSPL_SHIFT_MASTER where SHIFT_CODE ='" + Code + "'   "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If

    End Function


End Class
