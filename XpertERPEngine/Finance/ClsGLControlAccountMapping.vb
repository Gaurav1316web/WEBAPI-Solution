Imports common
Imports System.Data.SqlClient
Public Class ClsGLControlAccountMapping
#Region "Variables"
    Public AccountCode As String = Nothing
    Public Account_Description As String = Nothing
    Public IsForReceipt As Integer = 0
    Public IsForPayment As Integer = 0
    Public IsForAP As Integer = 0
    Public IsForAR As Integer = 0
    Public IsForJE As Integer = 0
    'Public SourceCode As String = Nothing
    'Public Type As String = Nothing
#End Region
    Public Function SaveData(ByVal Arr As List(Of ClsGLControlAccountMapping), ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Try
            Dim qry As String = "delete from TSPL_CONTROL_ACC_MAPPING"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If isNewEntry Then
                For Each obj As ClsGLControlAccountMapping In Arr
                    Dim coll As New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "Account_code", obj.AccountCode)
                    clsCommon.AddColumnsForChange(coll, "Account_Description", obj.Account_Description)
                    clsCommon.AddColumnsForChange(coll, "IsForReceipt", obj.IsForReceipt)
                    clsCommon.AddColumnsForChange(coll, "IsForPayment", obj.IsForPayment)
                    clsCommon.AddColumnsForChange(coll, "IsForAR", obj.IsForAR)
                    clsCommon.AddColumnsForChange(coll, "IsForAP", obj.IsForAP)
                    clsCommon.AddColumnsForChange(coll, "IsForJE", obj.IsForJE)
                    'clsCommon.AddColumnsForChange(coll, "Type", obj.Type)


                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CONTROL_ACC_MAPPING", OMInsertOrUpdate.Insert, "", trans)

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
        Dim obj As ClsGLControlAccountMapping = Nothing
        Dim qry As String = "select TSPL_CONTROL_ACC_MAPPING.account_code  ,TSPL_CONTROL_ACC_MAPPING.Account_Description,TSPL_CONTROL_ACC_MAPPING.IsForReceipt,TSPL_CONTROL_ACC_MAPPING.IsForPayment,TSPL_CONTROL_ACC_MAPPING.IsForAP,TSPL_CONTROL_ACC_MAPPING.IsForJE,TSPL_CONTROL_ACC_MAPPING.IsForAR from TSPL_CONTROL_ACC_MAPPING " & _
            " left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS .Account_Code =TSPL_CONTROL_ACC_MAPPING .Account_Code  order by TSPL_CONTROL_ACC_MAPPING.account_code "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        dt = clsDBFuncationality.GetDataTable(qry)
        Dim Arr As New List(Of ClsGLControlAccountMapping)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            Dim objTr As ClsGLControlAccountMapping
            For Each dr As DataRow In dt.Rows
                objTr = New ClsGLControlAccountMapping()
                objTr.AccountCode = clsCommon.myCstr(dr("account_code"))
                objTr.Account_Description = clsCommon.myCstr(dr("Account_Description"))
                objTr.IsForReceipt = clsCommon.myCdbl(dr("IsForReceipt"))
                objTr.IsForPayment = clsCommon.myCdbl(dr("IsForPayment"))
                objTr.IsForAP = clsCommon.myCdbl(dr("IsForAP"))
                objTr.IsForAR = clsCommon.myCdbl(dr("IsForAR"))
                objTr.IsForJE = clsCommon.myCdbl(dr("IsForJE"))
                'objTr.SourceCode = clsCommon.myCstr(dr("Source Code"))
                'objTr.Type = clsCommon.myCstr(dr("Type"))

                Arr.Add(objTr)
            Next
        End If
        Return Arr
    End Function
End Class
