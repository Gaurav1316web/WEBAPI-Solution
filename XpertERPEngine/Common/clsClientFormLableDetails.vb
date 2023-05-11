Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsClientFormLableDetails

#Region "Variables"

    Public REIMBURSEMENT_CODE As String
    Public PAY_PERIOD_CODE As String
    'Public ADJUSTMENT_BY_Code As String
    'Public ADJUSTMENT_BY_Name As String
    Public EMP_CODE As String
    Public EMP_NAME As String
    Public REIMBURSEMENT_REMARK As String
    Public POSTED As Boolean
    Public PAY_PERIOD_NAME As String
    Public REIMBURSEMENT_DATE As Date
    Public Posting_Date As DateTime
    'Public PP_TOTAL_DAYS As Integer

    '' grid columns

    Public FORM_CODE As String
    Public LABEL_ID As String
    Public CURRENT_LABEL_NAME As String

    Public NEW_LABEL_NAME As String
    Public NEW_LABEL_Font As String
    Public NEW_LABEL_Font_Size As String
    Public NEW_LABEL_Fore_Color As String
    Public NEW_LABEL_Back_Color As String


    Public Shared ObjList As List(Of clsClientFormLableDetails)
    Public Arr As New List(Of clsClientFormLableModuleDetails)
    Public DT As New DataTable

#End Region

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsClientFormLableDetails
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function DeleteData(ByVal strCode As String, Optional ByVal form_code As String = "") As Boolean
        Dim isSaved As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = "delete from TSPL_CLIENT_FORM_LABEL_SETTING where LABEL_ID ='" + strCode + "' AND FORM_NAME='" & FORM_CODE & "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)


            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsClientFormLableDetails
        Dim obj As New clsClientFormLableDetails()
        Dim objtr As New clsClientFormLableDetails()

        ObjList = New List(Of clsClientFormLableDetails)

        Dim qry As String = "SELECT TAV.REIMBURSEMENT_CODE,TAV.REIMBURSEMENT_DATE,TAV.PAY_PERIOD_CODE,TPM.PAY_PERIOD_NAME,TAV.POSTED,TPM.Posting_Date, " _
                            & " TAV.REIMBURSEMENT_REMARK,TAV.EMP_CODE,EMP.Emp_Name  FROM TSPL_EMP_REIMBURSEMENT TAV " _
                            & " INNER JOIN TSPL_PAYPERIOD_MASTER TPM ON TAV.PAY_PERIOD_CODE=TPM.PAY_PERIOD_CODE " _
                            & " LEFT JOIN TSPL_EMPLOYEE_MASTER EMP ON TAV.EMP_CODE=EMP.EMP_CODE  where 2=2 "

        Select Case NavType
            Case NavigatorType.First
                qry += " and REIMBURSEMENT_CODE = (select MIN(REIMBURSEMENT_CODE) from TSPL_EMP_REIMBURSEMENT)"
            Case NavigatorType.Last
                qry += " and REIMBURSEMENT_CODE = (select Max(REIMBURSEMENT_CODE) from TSPL_EMP_REIMBURSEMENT)"
            Case NavigatorType.Next
                qry += " and REIMBURSEMENT_CODE = (select Min(REIMBURSEMENT_CODE) from TSPL_EMP_REIMBURSEMENT where  REIMBURSEMENT_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and REIMBURSEMENT_CODE = (select Max(REIMBURSEMENT_CODE) from TSPL_EMP_REIMBURSEMENT where REIMBURSEMENT_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and REIMBURSEMENT_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            'obj.SALARY_STRUCTURE_NAME = dt.Rows(0)("SALARY_STRUCTURE_NAME")
            obj.REIMBURSEMENT_CODE = dt.Rows(0)("REIMBURSEMENT_CODE")
            obj.REIMBURSEMENT_DATE = dt.Rows(0)("REIMBURSEMENT_DATE")
            strCode = dt.Rows(0)("REIMBURSEMENT_CODE")
            obj.PAY_PERIOD_CODE = dt.Rows(0)("PAY_PERIOD_CODE")
            'obj.ADJUSTMENT_BY_Code = clsCommon.myCstr(dt.Rows(0)("ADJUSTMENT_BY"))
            'obj.ADJUSTMENT_BY_Name = clsCommon.myCstr(dt.Rows(0)("ADJUSTMENT_BY_NAME"))

            obj.REIMBURSEMENT_REMARK = clsCommon.myCstr(dt.Rows(0)("REIMBURSEMENT_REMARK"))
            obj.PAY_PERIOD_NAME = clsCommon.myCstr(dt.Rows(0)("PAY_PERIOD_NAME"))
            obj.EMP_CODE = clsCommon.myCstr(dt.Rows(0)("EMP_CODE"))
            obj.EMP_NAME = clsCommon.myCstr(dt.Rows(0)("EMP_NAME"))
            obj.POSTED = clsCommon.myCBool(dt.Rows(0)("POSTED"))
            obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))

        End If

        qry = "select TAV.REIMBURSEMENT_CODE,TAVD.EMP_CODE,EMP.EMP_NAME,TAVD.PAY_HEAD_CODE,TPH.PAY_HEAD_NAME," _
             & " TAVD.NEW_LABEL_NAME FROM TSPL_EMPREIMBURSEMENT_DETAIL TAVD " _
             & " INNER JOIN  TSPL_EMP_REIMBURSEMENT TAV ON TAVD.REIMBURSEMENT_CODE=TAV.REIMBURSEMENT_CODE " _
             & " INNER JOIN TSPL_EMPLOYEE_MASTER EMP ON TAVD.EMP_CODE=EMP.EMP_CODE " _
             & " LEFT JOIN TSPL_PAYHEAD_MASTER TPH ON TAVD.PAY_HEAD_CODE=TPH.PAY_HEAD_CODE where 2=2"

        qry += " and TAV.REIMBURSEMENT_CODE = '" + strCode + "'"

        dt = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsClientFormLableDetails()
                objtr.REIMBURSEMENT_CODE = clsCommon.myCstr(dr("REIMBURSEMENT_CODE"))
                objtr.FORM_CODE = clsCommon.myCstr(dr("EMP_CODE"))
                objtr.LABEL_ID = clsCommon.myCstr(dr("PAY_HEAD_CODE"))
                objtr.CURRENT_LABEL_NAME = clsCommon.myCstr(dr("PAY_HEAD_NAME"))
                objtr.NEW_LABEL_NAME = clsCommon.myCstr(dr("NEW_LABEL_NAME"))
                ObjList.Add(objtr)
            Next
        End If

        clsClientFormLableDetails.ObjList = ObjList
        Return obj
    End Function

    Public Function SaveData(ByVal obj As clsClientFormLableDetails, ByVal objList As List(Of clsClientFormLableDetails), ByVal isNewEntry As Boolean, Optional ByVal strCode As String = "") As Boolean
        Dim isSaved As Boolean = True

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Try
            'If isNewEntry Then
            '    If strCode = "" Then
            '        obj.REIMBURSEMENT_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(clsCommon.GETSERVERDATE(trans)), clsDocType.ReimbursementDetails, "", "")
            '    Else
            '        obj.REIMBURSEMENT_CODE = strCode
            '    End If
            'End If

            'Dim qry As String = "delete from TSPL_EMPREIMBURSEMENT_DETAIL where REIMBURSEMENT_CODE='" + obj.REIMBURSEMENT_CODE + "'"
            'isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            'Dim strDocNo As String = ""

            'If (clsCommon.myLen(obj.REIMBURSEMENT_CODE) <= 0) Then
            '    Throw New Exception("Error in Document Code Generation")
            'End If

            'Dim coll As New Hashtable()
            'clsCommon.AddColumnsForChange(coll, "REIMBURSEMENT_CODE", obj.REIMBURSEMENT_CODE)
            'clsCommon.AddColumnsForChange(coll, "PAY_PERIOD_CODE", obj.PAY_PERIOD_CODE)
            ''clsCommon.AddColumnsForChange(coll, "ADJUSTMENT_BY", IIf(obj.ADJUSTMENT_BY_Code Is Nothing, DBNull.Value, obj.ADJUSTMENT_BY_Code))
            'clsCommon.AddColumnsForChange(coll, "REIMBURSEMENT_REMARK", obj.REIMBURSEMENT_REMARK)
            'clsCommon.AddColumnsForChange(coll, "EMP_CODE", obj.EMP_CODE)
            'clsCommon.AddColumnsForChange(coll, "REIMBURSEMENT_DATE", clsCommon.GetPrintDate(obj.REIMBURSEMENT_DATE, "dd/MMM/yyyy"))
            'clsCommon.AddColumnsForChange(coll, "NEW_LABEL_NAME", obj.NEW_LABEL_NAME)

            'clsCommon.AddColumnsForChange(coll, "POSTED", "0")
            ''clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            ''clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
            'clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            'clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

            'If isNewEntry Then

            '    'clsCommon.AddColumnsForChange(coll, "REIMBURSEMENT_CODE", obj.REIMBURSEMENT_CODE)
            '    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            '    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            '    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMP_REIMBURSEMENT", OMInsertOrUpdate.Insert, "", trans)
            'Else

            '    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMP_REIMBURSEMENT", OMInsertOrUpdate.Update, "TSPL_EMP_REIMBURSEMENT.REIMBURSEMENT_CODE='" + obj.REIMBURSEMENT_CODE + "'", trans)
            'End If


            isSaved = isSaved AndAlso clsClientFormLableModuleDetails.SaveData(obj.REIMBURSEMENT_CODE, objList, trans)
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
            Dim obj As clsClientFormLableDetails = clsClientFormLableDetails.GetData(strDocNo, NavigatorType.Current)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.REIMBURSEMENT_CODE) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (isCheckForPosted AndAlso obj.POSTED = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If

            Dim qry As String = "Update TSPL_EMP_REIMBURSEMENT set POSTED=1, Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where REIMBURSEMENT_CODE ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

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

Public Class clsClientFormLableModuleDetails
#Region "Variables"
    Public FORM_CODE As String
    Public LABEL_ID As String
    Public CURRENT_LABEL_NAME As String


    Public NEW_LABEL_NAME As String
    Public CREATED_BY As String
    Public MODIFED_BY As String


    Public Shared ObjList As List(Of clsAdjustmentVoucher)
    'Public Const AttendanceCode As String = "MT"
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsClientFormLableDetails), ByVal trans As SqlTransaction) As Boolean


        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsClientFormLableDetails In Arr
                Dim coll As New Hashtable()
                'clsCommon.AddColumnsForChange(coll, "DOCUMENT_ID", strDocNo)
                clsCommon.AddColumnsForChange(coll, "FORM_NAME", obj.FORM_CODE)
                clsCommon.AddColumnsForChange(coll, "LABEL_ID", obj.LABEL_ID)
                clsCommon.AddColumnsForChange(coll, "CURRENT_LABEL_NAME", obj.CURRENT_LABEL_NAME)
                clsCommon.AddColumnsForChange(coll, "NEW_LABEL_NAME", obj.NEW_LABEL_NAME)
                clsCommon.AddColumnsForChange(coll, "LABEL_Font", obj.NEW_LABEL_Font)
                clsCommon.AddColumnsForChange(coll, "LABEL_Font_Size", obj.NEW_LABEL_Font_Size)
                ' clsCommon.AddColumnsForChange(coll, "LABEL_Fore_Color", obj.NEW_LABEL_Fore_Color)
                'clsCommon.AddColumnsForChange(coll, "LABEL_Back_Color", obj.NEW_LABEL_Back_Color)


                'clsCommon.AddColumnsForChange(coll, "CREATED_BY", objCommonVar.CurrentUser)
                'clsCommon.AddColumnsForChange(coll, "MODIFIED_BY", objCommonVar.CurrentUser)
                'clsCommon.AddColumnsForChange(coll, "CREATED_DATE", Today.Date.ToString)
                'clsCommon.AddColumnsForChange(coll, "MODIFIED_DATE", Today.Date.ToString)
                'Dim qry As String = "select * from TSPL_CLIENT_FORM_LABEL_SETTING where LABEL_ID='" + obj.LABEL_ID + "' and FORM_NAME='" + obj.FORM_CODE + "'"
                'Dim dr As DataTable = clsDBFuncationality.GetDataTable(qry)

                Dim exists As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue(" select Count (*) from TSPL_CLIENT_FORM_LABEL_SETTING where FORM_NAME = '" + obj.FORM_CODE + "' and Label_id = '" + obj.LABEL_ID + "' ", trans)) 'obj.DT.[Select]().ToList().Exists(Function(row) row("LABEL_ID").ToString().ToUpper() = obj.LABEL_ID.ToUpper())
                If exists = True Then  ' obj.DT.Select("LABEL_ID='" & obj.LABEL_ID & "'").Length > 0
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CLIENT_FORM_LABEL_SETTING", OMInsertOrUpdate.Update, "TSPL_CLIENT_FORM_LABEL_SETTING.LABEL_ID='" + obj.LABEL_ID + "' and TSPL_CLIENT_FORM_LABEL_SETTING.FORM_NAME='" + obj.FORM_CODE + "'", trans)
                Else
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CLIENT_FORM_LABEL_SETTING", OMInsertOrUpdate.Insert, "TSPL_CLIENT_FORM_LABEL_SETTING.LABEL_ID='" + obj.LABEL_ID + "' and TSPL_CLIENT_FORM_LABEL_SETTING.FORM_NAME='" + obj.FORM_CODE + "'", trans)
                End If

            Next

        End If

        Return True
    End Function
End Class
