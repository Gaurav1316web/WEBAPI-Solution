Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsEmployeeShiftChange

#Region "Variables"
    Public EMP_SHIFT_CODE As String
    Public SHIFT_APP_DATE As Date
    Public DESCRIPTION As String
    Public POSTED As Boolean
    Public Comp_Code As String
    Public Posting_Date As DateTime
    Public ObjList As List(Of clsEmployeeShiftChangeDetail)
#End Region


    Public Shared Function GetFinder(ByVal whrCls As String, ByVal currCode As String, ByVal isButtonClicked As Boolean) As String
        Dim qry As String = "select EMP_SHIFT_CODE as Code,SHIFT_APP_DATE as [Application Date],DESCRIPTION as [Description],POSTED as [Is Posted],Posting_Date as [Posting Date] from TSPL_EMPLOYEE_SHIFT_CHANGE_HEAD "
        Dim str As String = ""
        If clsCommon.myLen(whrCls) > 0 Then
            whrCls = whrCls + " and TSPL_EMPLOYEE_SHIFT_CHANGE_HEAD.comp_code='" + objCommonVar.CurrentCompanyCode + "'"
        Else
            whrCls = " TSPL_EMPLOYEE_SHIFT_CHANGE_HEAD.comp_code='" + objCommonVar.CurrentCompanyCode + "'"
        End If
        str = clsCommon.ShowSelectForm("ShiftChange", qry, "Code", whrCls, currCode, "Code", isButtonClicked)

        Return str
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsEmployeeShiftChange
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = "delete from TSPL_EMPLOYEE_SHIFT_CHANGE_DETAIL where EMP_SHIFT_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_EMPLOYEE_SHIFT_CHANGE_HEAD where EMP_SHIFT_CODE ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsEmployeeShiftChange
        Dim obj As New clsEmployeeShiftChange()
        Dim qry As String = "select EMP_SHIFT_CODE,SHIFT_APP_DATE,DESCRIPTION,POSTED,Posting_Date from TSPL_EMPLOYEE_SHIFT_CHANGE_HEAD where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and EMP_SHIFT_CODE = (select MIN(EMP_SHIFT_CODE) from TSPL_EMPLOYEE_SHIFT_CHANGE_HEAD)"
            Case NavigatorType.Last
                qry += " and EMP_SHIFT_CODE = (select Max(EMP_SHIFT_CODE) from TSPL_EMPLOYEE_SHIFT_CHANGE_HEAD)"
            Case NavigatorType.Next
                qry += " and EMP_SHIFT_CODE = (select Min(EMP_SHIFT_CODE) from TSPL_EMPLOYEE_SHIFT_CHANGE_HEAD where  EMP_SHIFT_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and EMP_SHIFT_CODE = (select Max(EMP_SHIFT_CODE) from TSPL_EMPLOYEE_SHIFT_CHANGE_HEAD where EMP_SHIFT_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and EMP_SHIFT_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj.EMP_SHIFT_CODE = dt.Rows(0)("EMP_SHIFT_CODE")
            obj.SHIFT_APP_DATE = dt.Rows(0)("SHIFT_APP_DATE")
            strCode = dt.Rows(0)("EMP_SHIFT_CODE")
            obj.DESCRIPTION = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))
            obj.POSTED = clsCommon.myCBool(dt.Rows(0)("POSTED"))
            If clsCommon.myLen(dt.Rows(0)("Posting_Date")) Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            Else
                obj.Posting_Date = Nothing
            End If
        End If
        obj.ObjList = clsEmployeeShiftChangeDetail.GetEmpShiftDetailData(strCode, trans)
        Return obj
    End Function
    Public Function SaveData(ByVal obj As clsEmployeeShiftChange, ByVal isNewEntry As Boolean, Optional ByVal strCode As String = "") As Boolean
        Dim isSaved As Boolean = True

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Try
            If isNewEntry Then
                If strCode = "" Then
                    obj.EMP_SHIFT_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(obj.SHIFT_APP_DATE, "dd/MMM/yyyy"), clsDocType.EmployeeShiftChange, "", "")
                Else
                    obj.EMP_SHIFT_CODE = strCode
                End If
            End If
            Dim qry As String = "delete from TSPL_EMPLOYEE_SHIFT_CHANGE_DETAIL where EMP_SHIFT_CODE='" + obj.EMP_SHIFT_CODE + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strDocNo As String = ""

            If (clsCommon.myLen(obj.EMP_SHIFT_CODE) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "EMP_SHIFT_CODE", obj.EMP_SHIFT_CODE)
            clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.DESCRIPTION)
            clsCommon.AddColumnsForChange(coll, "SHIFT_APP_DATE", clsCommon.GetPrintDate(obj.SHIFT_APP_DATE, "dd/MMM/yyyy"))

            clsCommon.AddColumnsForChange(coll, "POSTED", "0")
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "COMP_CODE", objCommonVar.CurrentCompanyCode, True)

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPLOYEE_SHIFT_CHANGE_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPLOYEE_SHIFT_CHANGE_HEAD", OMInsertOrUpdate.Update, "TSPL_EMPLOYEE_SHIFT_CHANGE_HEAD.EMP_SHIFT_CODE='" + obj.EMP_SHIFT_CODE + "'", trans)
            End If
            isSaved = isSaved AndAlso clsEmployeeShiftChangeDetail.SaveData(obj.EMP_SHIFT_CODE, obj.ObjList, trans)
            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(err.Message)
            Return False
        End Try
        Return isSaved
    End Function
    Public Shared Function PostData(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsEmployeeShiftChange = clsEmployeeShiftChange.GetData(strDocNo, NavigatorType.Current)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.EMP_SHIFT_CODE) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (isCheckForPosted AndAlso obj.POSTED = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            Dim qry As String = "Update TSPL_EMPLOYEE_SHIFT_CHANGE_HEAD set POSTED=1, Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where EMP_SHIFT_CODE ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function



End Class
Public Class clsEmployeeShiftChangeDetail
#Region "Variables"
    Public EMP_SHIFT_CODE As String
    Public EMP_CODE As String
    Public EMP_NAME As String
    Public SHIFT_CODE As String
    Public SHIFT_Desc As String
    Public SHIFT_Start_Time As String
    Public SHIFT_End_Time As String

#End Region
    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsEmployeeShiftChangeDetail), ByVal trans As SqlTransaction) As Boolean

        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsEmployeeShiftChangeDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "EMP_SHIFT_CODE", strDocNo)
                clsCommon.AddColumnsForChange(coll, "EMP_CODE", obj.EMP_CODE)
                clsCommon.AddColumnsForChange(coll, "SHIFT_CODE", obj.SHIFT_CODE)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPLOYEE_SHIFT_CHANGE_DETAIL", OMInsertOrUpdate.Insert, "TSPL_EMPLOYEE_SHIFT_CHANGE_DETAIL.EMP_SHIFT_CODE='" + strDocNo + "'", trans)
            Next
        End If
        Return True
    End Function
    Public Shared Function GetEmpShiftDetailData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As List(Of clsEmployeeShiftChangeDetail)
        Dim qry As String = "select TSPL_EMPLOYEE_SHIFT_CHANGE_HEAD.EMP_SHIFT_CODE ,TSPL_EMPLOYEE_SHIFT_CHANGE_HEAD.SHIFT_APP_DATE , " & _
        " TSPL_EMPLOYEE_SHIFT_CHANGE_HEAD.DESCRIPTION ,TSPL_EMPLOYEE_SHIFT_CHANGE_HEAD.POSTED ,TSPL_EMPLOYEE_SHIFT_CHANGE_HEAD.Posting_Date, " & _
        " TSPL_EMPLOYEE_SHIFT_CHANGE_DETAIL.EMP_CODE,TSPL_EMPLOYEE_SHIFT_CHANGE_DETAIL.SHIFT_CODE ,TSPL_EMPLOYEE_MASTER.Emp_Name, " & _
        " TSPL_SHIFT_MASTER.SHIFT_NAME, TSPL_SHIFT_MASTER.FROM_Time, TSPL_SHIFT_MASTER.TO_Time " & _
        " from TSPL_EMPLOYEE_SHIFT_CHANGE_HEAD " & _
        " INNER JOIN TSPL_EMPLOYEE_SHIFT_CHANGE_DETAIL ON TSPL_EMPLOYEE_SHIFT_CHANGE_HEAD.EMP_SHIFT_CODE=TSPL_EMPLOYEE_SHIFT_CHANGE_DETAIL.EMP_SHIFT_CODE " & _
        " LEFT JOIN  TSPL_EMPLOYEE_MASTER ON TSPL_EMPLOYEE_SHIFT_CHANGE_DETAIL.EMP_CODE=TSPL_EMPLOYEE_MASTER.EMP_CODE " & _
        " LEFT JOIN TSPL_SHIFT_MASTER ON TSPL_EMPLOYEE_SHIFT_CHANGE_DETAIL.SHIFT_CODE=TSPL_SHIFT_MASTER.SHIFT_CODE where 2=2"

        qry += " and TSPL_EMPLOYEE_SHIFT_CHANGE_HEAD.EMP_SHIFT_CODE = '" + strDocNo + "'"
        Dim dt As DataTable
        dt = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        Dim objtr As clsEmployeeShiftChangeDetail
        Dim objList As New List(Of clsEmployeeShiftChangeDetail)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsEmployeeShiftChangeDetail()
                objtr.EMP_SHIFT_CODE = clsCommon.myCstr(dr("EMP_SHIFT_CODE"))
                objtr.EMP_CODE = clsCommon.myCstr(dr("EMP_CODE"))
                objtr.EMP_NAME = clsCommon.myCstr(dr("Emp_Name"))
                objtr.SHIFT_CODE = clsCommon.myCstr(dr("SHIFT_CODE"))
                objtr.SHIFT_Desc = clsCommon.myCstr(dr("SHIFT_NAME"))

                objtr.SHIFT_Start_Time = clsCommon.myCstr(dr("FROM_Time"))
                objtr.SHIFT_End_Time = clsCommon.myCstr(dr("TO_Time"))
                ObjList.Add(objtr)
            Next
        End If
        Return objList
    End Function
End Class
