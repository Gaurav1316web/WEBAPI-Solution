Imports common
Imports System.Data.SqlClient
Public Class ClsGlTrialAccountExcluded
#Region "Variables"
    Public AccountCode As String = Nothing
    Public desc As String = Nothing
    Public SourceCode As String = Nothing
    Public Type As String = Nothing
#End Region
    Public Function SaveData(ByVal Arr As List(Of ClsGlTrialAccountExcluded), ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Try
            Dim qry As String = "delete from TSPL_TRIAL_GLACCOUNTS_EXCLUDED"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If isNewEntry Then
                For Each obj As ClsGlTrialAccountExcluded In Arr
                    Dim coll As New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "Account_code", obj.AccountCode)
                    clsCommon.AddColumnsForChange(coll, "Source_code", obj.SourceCode)
                    clsCommon.AddColumnsForChange(coll, "Type", obj.Type)


                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TRIAL_GLACCOUNTS_EXCLUDED", OMInsertOrUpdate.Insert, "", trans)

                Next
                If isSaved Then
                    trans.Commit()

                Else
                    trans.Rollback()

                End If
            End If

        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return isSaved

    End Function
    Public Shared Function GetData()
        Dim obj As ClsGlTrialAccountExcluded = Nothing
        Dim qry As String = "select TSPL_TRIAL_GLACCOUNTS_EXCLUDED. account_code as 'Account Code' ,TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Main_GL_Account_Desc ,Source_code as 'Source Code',Type from TSPL_TRIAL_GLACCOUNTS_EXCLUDED left outer join TSPL_ACCOUNT_MAIN_GL_ACCOUNT on TSPL_ACCOUNT_MAIN_GL_ACCOUNT .Main_GL_Account =TSPL_TRIAL_GLACCOUNTS_EXCLUDED .Account_Code  order by [Account Code] ,[Source Code] ,Type  "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        dt = clsDBFuncationality.GetDataTable(qry)
        Dim Arr As New List(Of ClsGlTrialAccountExcluded)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            Dim objTr As ClsGlTrialAccountExcluded
            For Each dr As DataRow In dt.Rows
                objTr = New ClsGlTrialAccountExcluded()
                objTr.AccountCode = clsCommon.myCstr(dr("Account Code"))
                objTr.desc = clsCommon.myCstr(dr("Main_GL_Account_Desc"))
                objTr.SourceCode = clsCommon.myCstr(dr("Source Code"))
                objTr.Type = clsCommon.myCstr(dr("Type"))

                Arr.Add(objTr)
            Next
        End If
        Return Arr
    End Function
End Class
