Imports common
Imports System.Data.SqlClient
Public Class clsCreditLimitApprovalHead

#Region "Variables"
    Public ModuleName As String = Nothing
    Public Shared trans As SqlTransaction = Nothing
    Public arr As List(Of clsCreditLimitApprovalDeatils) = Nothing
    Public arrUser As New ArrayList
#End Region
    Public Shared Function SaveData(ByVal Modu As clsCreditLimitApprovalHead, ByVal isnewentry As Boolean, ByVal Arra As List(Of clsCreditLimitApprovalDeatils))
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            'For Each Usercode As String In Usr
            Dim qry As String = "delete from TSPL_CREDIT_LIMIT_APPROVAL_Detail where Module_Name='" + Modu.ModuleName + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            'Next
            'For Each obj As clsBankPermission In Arr
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Module_Name", Modu.ModuleName)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

            If isnewentry Then
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CREDIT_LIMIT_APPROVAL_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CREDIT_LIMIT_APPROVAL_HEAD", OMInsertOrUpdate.Update, " Module_Name='" + Modu.ModuleName + "'", trans)
            End If
            clsCreditLimitApprovalDeatils.savedata(Modu.ModuleName, Arra, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function getdata(ByVal code As String) As clsCreditLimitApprovalHead
        Try
            Dim obj As clsCreditLimitApprovalHead = Nothing
            Dim qst As String = (" select TSPL_CREDIT_LIMIT_APPROVAL_HEAD.Module_Name ,TSPL_CREDIT_LIMIT_APPROVAL_Detail.User_Code  from TSPL_CREDIT_LIMIT_APPROVAL_Detail left join  TSPL_CREDIT_LIMIT_APPROVAL_HEAD  on TSPL_CREDIT_LIMIT_APPROVAL_HEAD.Module_Name =TSPL_CREDIT_LIMIT_APPROVAL_Detail.Module_Name   where 2=2 ")
            qst += " and TSPL_CREDIT_LIMIT_APPROVAL_HEAD. Module_Name ='" + code + "'"
            'Select Case navigatortype
            '    Case navigatortype.Current
            '        qst += "and TSPL_CREDIT_LIMIT_APPROVAL_HEAD.Module_Name in ('" + code + "')"
            '    Case navigatortype.Next
            '        qst += "and TSPL_CREDIT_LIMIT_APPROVAL_HEAD.Module_Name in (select  min(Module_Name)from TSPL_CREDIT_LIMIT_APPROVAL_HEAD  where TSPL_CREDIT_LIMIT_APPROVAL_HEAD.Module_Name  >'" + code + "')"
            '    Case navigatortype.First
            '        qst += "and TSPL_CREDIT_LIMIT_APPROVAL_HEAD.Module_Name in (select MIN(Module_Name)from TSPL_CREDIT_LIMIT_APPROVAL_HEAD )"

            '    Case navigatortype.Last
            '        qst += "and TSPL_CREDIT_LIMIT_APPROVAL_HEAD.Module_Name in (select Max(Module_Name)from TSPL_CREDIT_LIMIT_APPROVAL_HEAD )"
            '    Case navigatortype.Previous
            '        qst += "and TSPL_CREDIT_LIMIT_APPROVAL_HEAD.Module_Name in (select  max(Module_Name)from TSPL_CREDIT_LIMIT_APPROVAL_HEAD  where TSPL_CREDIT_LIMIT_APPROVAL_HEAD.Module_Name <'" + code + "')"
            'End Select
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qst)
            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                obj = New clsCreditLimitApprovalHead
                obj.ModuleName = clsCommon.myCstr(dt1.Rows(0)("Module_Name"))


            End If
            If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                obj.arr = New List(Of clsCreditLimitApprovalDeatils)
                For Each dr As DataRow In dt1.Rows
                    Dim objTr As clsCreditLimitApprovalDeatils = New clsCreditLimitApprovalDeatils()
                    objTr.UserCode = clsCommon.myCstr(dr("User_Code"))
                    obj.arr.Add(objTr)
                    obj.arrUser.Add(objTr.UserCode)
                Next
            End If

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
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
            qry = "DELETE FROM TSPL_CREDIT_LIMIT_APPROVAL_Detail WHERE Module_Name='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "DELETE FROM TSPL_CREDIT_LIMIT_APPROVAL_HEAD WHERE Module_Name ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If isSaved Then
                trans.Commit()
            Else
                trans.Rollback()
                isSaved = False
            End If
        Catch ex As Exception
            trans.Rollback()
            isSaved = False
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

End Class


Public Class clsCreditLimitApprovalDeatils
#Region "Variables"
    Public ModuleName As String
    Public UserCode As String

#End Region
    Public Shared Function savedata(ByVal ModuleName As String, ByVal arr As List(Of clsCreditLimitApprovalDeatils), ByVal trans As SqlTransaction) As Boolean
        If (arr IsNot Nothing AndAlso arr.Count > 0) Then
            For Each Credit As clsCreditLimitApprovalDeatils In arr
                Try
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Module_Name", ModuleName)
                    clsCommon.AddColumnsForChange(coll, "User_Code", Credit.UserCode)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CREDIT_LIMIT_APPROVAL_Detail", OMInsertOrUpdate.Insert, "", trans)
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                End Try
            Next Credit
        End If
        Return True
    End Function


End Class

