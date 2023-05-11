Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsModuleScreenHead

#Region "Variables"

    '' grid columns

    Public Module_Name As String
    Public IsAvailable As Boolean
    Public Program_code As String
    Public Screen_Name As String
    Public Parent_Code As String
    Public Shared EnableScreenSelection As Boolean = False

    Public Shared ObjList As List(Of clsModuleScreenHead)
    Public Arr As New List(Of clsModuleScreenDetails)
    Public DT As New DataTable

#End Region

    Public Shared Function GetData() As ArrayList 'clsModuleScreenHead
        Return GetData(Nothing)
    End Function
    'Public Shared Function DeleteData(ByVal strCode As String, Optional ByVal form_code As String = "") As Boolean
    '    'Dim isSaved As Boolean
    '    'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
    '    'Try
    '    '    isSaved = False

    '    '    If (clsCommon.myLen(strCode) <= 0) Then
    '    '        Throw New Exception("Code not found to Delete")
    '    '    End If

    '    '    Dim qry As String
    '    '    qry = "delete from TSPL_CLIENT_FORM_LABEL_SETTING where Module_Name ='" + strCode + "' AND FORM_NAME='" & form_code & "'"
    '    '    isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)


    '    '    trans.Commit()
    '    'Catch ex As Exception
    '    '    trans.Rollback()
    '    '    Throw New Exception(ex.Message.ToString())
    '    'End Try
    '    'Return isSaved
    'End Function
    Public Shared Function GetData(ByVal trans As SqlTransaction) As ArrayList 'clsModuleScreenHead
        Dim arr As New ArrayList
        Dim qry As String = ""
        Dim EnableScreenSelection As Boolean = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.EnableScreenSelection, clsFixedParameterCode.EnableScreenSelection, Nothing)) = "1", True, False))

        '    If EnableScreenSelection = True Then
        '        qry = "select Program_Code from  TSPL_PROGRAM_MASTER tpm  " _
        '& " inner join TSPL_MODULE_SCREEN_PERMISSION on TSPL_MODULE_SCREEN_PERMISSION.Screen_Name=Program_Code where 2=2 and Program_Name<>'' and program_COde <>'Mutility' order by Program_Code"
        '    Else
        qry = "select Program_Code from  TSPL_PROGRAM_MASTER tpm Inner join TSPL_Module_Permission tmp on " _
       & " tmp.module_Name=tpm.program_Code  where Type='M' and Program_Name<>'' and program_COde <>'Mutility' order by Program_Code"
        'End If


        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        ' dt = clsDBFuncationality.GetDataTable(qry, trans)
        'Return dt
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                'objtr = New clsModuleScreenHead()

                'objtr.IsAvailable = clsCommon.myCstr(dr("isavailable"))
                'objtr.Module_Name = clsCommon.myCstr(dr("Module Name"))
                'objtr.Program_code = clsCommon.myCstr(dr("Program_Code"))

                'ObjList.Add(objtr)
                arr.Add(clsCommon.myCstr(dr("Program_Code")))
            Next
        End If

        clsModuleScreenHead.ObjList = ObjList
        Return arr 'obj
    End Function

    Public Shared Function GetDataForScreen(ByVal strProgramCode As String, ByVal trans As SqlTransaction) As ArrayList
        Dim arr As New ArrayList
        Dim qry As String = ""

        'qry = "select Program_Code from  TSPL_PROGRAM_MASTER tpm  " _
        '       & " inner join TSPL_MODULE_SCREEN_PERMISSION on TSPL_MODULE_SCREEN_PERMISSION.Screen_Name=Program_Code where 2=2 and Program_Name<>'' and program_COde <>'Mutility' order by Program_Code"
        qry = " select TSPL_PROGRAM_MASTER.Program_Code  from TSPL_PROGRAM_MASTER " & _
              " left outer join (select Program_Code, Program_Name,Parent_Code from TSPL_PROGRAM_MASTER where Type in ('SM')) as TBL_SMODULE on TBL_SMODULE.Program_Code = TSPL_PROGRAM_MASTER.Parent_Code " & _
              " left outer join (select Program_Code, Program_Name,Parent_Code from TSPL_PROGRAM_MASTER where Type in ('M')) as TBL_MODULE on TBL_MODULE.Program_Code = TBL_SMODULE.Parent_Code " & _
              " where  TBL_MODULE.Program_Code in (select  distinct Module_Name from TSPL_MODULE_PERMISSION ) and  not TSPL_PROGRAM_MASTER.Type in ('M','SM')  " & _
              "   " & _
              " and    TSPL_PROGRAM_MASTER.Program_Code   in (select Screen_Name from TSPL_MODULE_SCREEN_PERMISSION) " & _
              " and  TBL_MODULE.Program_Code  in ( '" + strProgramCode + "' )" & _
              " order by SNo   "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                arr.Add(clsCommon.myCstr(dr("Program_Code")))
            Next
        End If
        clsModuleScreenHead.ObjList = ObjList
        Return arr
    End Function

    Public Function SaveData(ByVal objList As List(Of clsModuleScreenHead), Optional ByVal strModule As String = Nothing) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            clsModuleScreenDetails.SaveData(objList, trans, strModule)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Function SaveDataForScreen(ByVal objList As List(Of clsModuleScreenHead), Optional ByVal strModule As String = Nothing) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            clsModuleScreenDetails.SaveDataScreen(objList, trans, strModule)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    'Public Shared Function PostData(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean) As Boolean
    '    'Try
    '    '    If (clsCommon.myLen(strDocNo) <= 0) Then
    '    '        Throw New Exception("Code not found to Post")
    '    '    End If
    '    '    Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt")
    '    '    Dim obj As clsModuleScreenHead = clsModuleScreenHead.GetData(strDocNo, NavigatorType.Current)
    '    '    If (obj Is Nothing OrElse clsCommon.myLen(obj.REIMBURSEMENT_CODE) <= 0) Then
    '    '        Throw New Exception("No Data found to Post")
    '    '    End If
    '    '    If (isCheckForPosted AndAlso obj.POSTED = 1) Then
    '    '        Throw New Exception("Already Post on :" + obj.Posting_Date)
    '    '    End If

    '    '    Dim qry As String = "Update TSPL_EMP_REIMBURSEMENT set POSTED=1, Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where REIMBURSEMENT_CODE ='" + strDocNo + "'"
    '    '    clsDBFuncationality.ExecuteNonQuery(qry)
    '    'Catch ex As Exception
    '    '    Throw New Exception(ex.Message)
    '    'End Try
    '    'Return True
    'End Function

    Public Shared Function GetRegisterDT(ByVal strFromPP As String, ByVal strToPP As String) As DataTable
        Dim dt As DataTable
        Try
            Dim qry As String = ""
            qry += " SELECT TT1.REIMBURSEMENT_CODE ,TT1.REIMBURSEMENT_DATE ,TT2.NEW_LABEL_NAME ,TT1.REIMBURSEMENT_REMARK ,"
            qry += " TT1.PAY_PERIOD_CODE ,TT1.PAY_PERIOD_NAME ,"
            qry += " TT1.EMP_CODE ,TT1.Emp_Name ,TT1.POSTED  FROM ("
            qry += " SELECT T1.REIMBURSEMENT_CODE,T1.REIMBURSEMENT_DATE,T1.PAY_PERIOD_CODE,T2.PAY_PERIOD_NAME,T1.EMP_CODE,T3.Emp_Name,"
            qry += " T1.REIMBURSEMENT_REMARK, T1.POSTED"
            qry += " FROM TSPL_EMP_REIMBURSEMENT T1 LEFT JOIN TSPL_PAYPERIOD_MASTER T2 ON T1.PAY_PERIOD_CODE=T2.PAY_PERIOD_CODE "
            qry += " LEFT JOIN TSPL_EMPLOYEE_MASTER T3 ON T1.EMP_CODE=T3.EMP_CODE "
            qry += " LEFT JOIN TSPL_EMPLOYEE_MASTER T4 ON T1.EMP_CODE=T4.EMP_CODE "
            If clsCommon.myLen(strFromPP) > 0 AndAlso clsCommon.myLen(strToPP) > 0 Then
                qry += " WHERE T2.DATE_FROM BETWEEN "
                qry += " (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" + strFromPP + "') AND "
                qry += " (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" + strToPP + "')"
            End If
            qry += " ) AS TT1 LEFT JOIN (SELECT REIMBURSEMENT_CODE,SUM(NEW_LABEL_NAME) AS NEW_LABEL_NAME FROM TSPL_EMPREIMBURSEMENT_DETAIL GROUP BY REIMBURSEMENT_CODE) AS TT2"
            qry += " ON TT1.REIMBURSEMENT_CODE=TT2.REIMBURSEMENT_CODE ;"

            dt = clsDBFuncationality.GetDataTable(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return dt
    End Function

    Public Shared Function GetRegisterDTDetailed(ByVal strFromPP As String, ByVal strToPP As String) As DataTable
        Dim dt As DataTable
        Try
            Dim qry As String = ""
            qry += " SELECT T1.REIMBURSEMENT_CODE ,T2.PAY_HEAD_CODE ,T5.PAY_HEAD_NAME ,"
            qry += " T2.EMP_CODE ,T4.Emp_Name ,T2.NEW_LABEL_NAME  FROM TSPL_EMP_REIMBURSEMENT T1 "
            qry += " INNER JOIN TSPL_EMPREIMBURSEMENT_DETAIL T2 ON T1.REIMBURSEMENT_CODE=T2.REIMBURSEMENT_CODE"
            qry += " LEFT JOIN TSPL_PAYPERIOD_MASTER T3 ON T1.PAY_PERIOD_CODE=T3.PAY_PERIOD_CODE "
            qry += " LEFT JOIN TSPL_EMPLOYEE_MASTER T4 ON T2.EMP_CODE=T4.EMP_CODE "
            qry += " LEFT JOIN TSPL_PAYHEAD_MASTER T5 ON T2.PAY_HEAD_CODE=T5.PAY_HEAD_CODE"
            If clsCommon.myLen(strFromPP) > 0 AndAlso clsCommon.myLen(strToPP) > 0 Then
                qry += " WHERE T3.DATE_FROM BETWEEN "
                qry += " (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" + strFromPP + "') AND "
                qry += " (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" + strToPP + "') "
            End If
            dt = clsDBFuncationality.GetDataTable(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return dt
    End Function

End Class

Public Class clsModuleScreenDetails
#Region "Variables"

    Public Module_Name As String
    Public IsAvailable As String
    Public Program_code As String


    Public Shared ObjList As List(Of clsModuleScreenDetails)
    'Public Const AttendanceCode As String = "MT"
#End Region

    Public Shared Function SaveData(ByVal Arr As List(Of clsModuleScreenHead), ByVal trans As SqlTransaction, Optional ByVal strModule As String = Nothing) As Boolean
        '' Changes by Parteek for Screen Level Rights Ticket No : TEC/16/03/18-000101 on 01/05/2018
        Dim EnableScreenSelection As Boolean = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.EnableScreenSelection, clsFixedParameterCode.EnableScreenSelection, trans)) = "1", True, False))
        Dim sQuery As String = ""
        'If EnableScreenSelection = True Then
        '    sQuery = "delete from TSPL_MODULE_PERMISSION where module_name='" & strModule & "'"
        'Else
        sQuery = "delete from TSPL_MODULE_PERMISSION "
        'End If

        clsDBFuncationality.ExecuteNonQuery(sQuery, trans)

        '' Changes by Parteek for Screen Level Rights Ticket No : TEC/16/03/18-000101 on 01/05/2018
        'If EnableScreenSelection = True Then
        '    Dim coll As New Hashtable()
        '    clsCommon.AddColumnsForChange(coll, "Module_Name", strModule)
        '    clsCommon.AddColumnsForChange(coll, "IsAvailable", 1)
        '    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MODULE_PERMISSION", OMInsertOrUpdate.Insert, "", trans)
        'Else
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsModuleScreenHead In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Module_Name", obj.Module_Name)
                clsCommon.AddColumnsForChange(coll, "IsAvailable", 1)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MODULE_PERMISSION", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        'End If
        'If EnableScreenSelection = True Then
        '    SaveScreenData(Arr, trans, strModule)
        'End If

        Return True
    End Function
    Public Shared Function SaveDataScreen(ByVal Arr As List(Of clsModuleScreenHead), ByVal trans As SqlTransaction, Optional ByVal strModule As String = Nothing) As Boolean
        Dim sQuery = "delete from TSPL_MODULE_Screen_PERMISSION where module_Name='" & strModule & "'"
        clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsModuleScreenHead In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Module_Name", obj.Module_Name)
                clsCommon.AddColumnsForChange(coll, "Screen_Name", obj.Screen_Name)
                clsCommon.AddColumnsForChange(coll, "P_Code", obj.Parent_Code)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MODULE_Screen_PERMISSION", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
End Class
