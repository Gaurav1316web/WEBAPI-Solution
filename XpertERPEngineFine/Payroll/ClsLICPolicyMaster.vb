Imports common
Imports System.Data
Imports System.Data.SqlClient

Public Class ClsLICPolicyMaster

#Region "Variables"
    Public Code As String
    Public Description As String
    Public Name As String
    Public SALARY_DEPENDENT_ON_ATTEN As String
    Public OT_CODE As String
    Public OT_Name As String
    Public CALC_SAL_ON As String
    Public ATTN_REGISTER_TYPE As String

#End Region

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select TSPL_LIC_POLICY_MASTER.LIC_CODE as [Code] ,TSPL_LIC_POLICY_MASTER.LIC_NAME as [LIC Name]  From TSPL_LIC_POLICY_MASTER   "
        str = clsCommon.ShowSelectForm("ATTMSTFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function

    Public Function SaveData(ByVal obj As ClsLICPolicyMaster, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim coll As New Hashtable()
            'clsCommon.AddColumnsForChange(coll, "LIC_CODE", obj.Name)
            clsCommon.AddColumnsForChange(coll, "LIC_NAME", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "LIC_CODE", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LIC_POLICY_MASTER", OMInsertOrUpdate.Insert, "")

            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LIC_POLICY_MASTER", OMInsertOrUpdate.Update, "")
            End If

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Code, "TSPL_ATTENDANCE_MASTER", "ATTENDANCE_CODE", Nothing)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True

    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            clsCommonFunctionality.SaveDeletedData(objCommonVar.CurrentUserCode, strCode, "TSPL_LIC_POLICY_MASTER", "LIC_CODE", Nothing)

            Dim qry As String
            qry = "delete from TSPL_LIC_POLICY_MASTER where LIC_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsLICPolicyMaster
        Return GetData(strCode, NavType, Nothing)
    End Function


    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsLICPolicyMaster
        Dim obj As ClsLICPolicyMaster = Nothing

    End Function


End Class
