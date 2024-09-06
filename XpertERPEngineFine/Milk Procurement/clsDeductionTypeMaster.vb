Imports System.Data.SqlClient
Imports common

Public Class clsDeductionTypeMaster

#Region "Variables"

    Public Document_No As String = Nothing
    Public Start_Date As Date? = Nothing
    Public Document_date As Date? = Nothing
    Public Description As String = Nothing
    Public Status As Integer = 0
    Public Arr As List(Of clsHeadLoadDCS) = Nothing
#End Region

    Public Function SaveData(ByVal obj As clsDeductionTypeMaster, ByVal isNewEntry As Boolean, ByVal strTransType As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, Nothing, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Function SaveData(ByVal obj As clsDeductionTypeMaster, ByVal isNewEntry As Boolean, ByVal strTransType As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim qry As String = "delete from TSPL_HEAD_LOAD_DCS where Document_No='" + obj.Document_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DEDUCTION_TYPE_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DEDUCTION_TYPE_MASTER", OMInsertOrUpdate.Update, "TSPL_DEDUCTION_TYPE_MASTER.Document_No='" + obj.Document_No + "'", trans)
            End If

        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    'Public Shared Function GetData(ByVal strRetCode As String, ByVal NavType As NavigatorType, ByVal trans As String) As clsDeductionTypeMaster
    Public Shared Function GetData(ByVal strRetCode As String, ByVal NavType As NavigatorType) As clsDeductionTypeMaster
        Return GetData(strRetCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsDeductionTypeMaster
        Dim obj As clsDeductionTypeMaster = Nothing
        Dim qry As String = "select Document_No ,Description from TSPL_DEDUCTION_TYPE_MASTER where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_DEDUCTION_TYPE_MASTER.Document_No = (select MIN(Document_No) from TSPL_DEDUCTION_TYPE_MASTER)"
            Case NavigatorType.Last
                qry += " and TSPL_DEDUCTION_TYPE_MASTER.Document_No = (select Max(Document_No) from TSPL_DEDUCTION_TYPE_MASTER)"
            Case NavigatorType.Next
                qry += " and TSPL_DEDUCTION_TYPE_MASTER.Document_No = (select Min(Document_No) from TSPL_DEDUCTION_TYPE_MASTER where Document_No >'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_DEDUCTION_TYPE_MASTER.Document_No = (select Max(Document_No) from TSPL_DEDUCTION_TYPE_MASTER where Document_No <'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_DEDUCTION_TYPE_MASTER.Document_No = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsDeductionTypeMaster()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
        End If
        Return obj
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim qry As String = ""
        Try

            qry = "Delete from TSPL_DEDUCTION_TYPE_MASTER  where Document_No='" + strCode + "' "
            clsDBFuncationality.ExecuteNonQuery(qry, tran)

            tran.Commit()
        Catch err As Exception
            tran.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function getFinder(ByVal whrcls As String, ByVal strCode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim sql As String = "select Document_No as DocumentNo ,Description from TSPL_DEDUCTION_TYPE_MASTER"
        str = clsCommon.ShowSelectForm("DeductionTypeMaster", sql, "DocumentNo", "", strCode, "DocumentNo", isButtonClicked)
        Return str
    End Function

End Class
