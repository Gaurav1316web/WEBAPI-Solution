
Imports common
Imports System.Data.SqlClient
Public Class clsApproverCreation


#Region "Variables"
    Public Emp_Code As String = Nothing
    Public Doc_Date As DateTime = Nothing
    Public Emp_Name As String = Nothing
    Public Remarks As String = Nothing

    Public Arr As List(Of clsApproverCreationDetail) = Nothing
    Public ArrExp As List(Of clsApproverCreationExpenseDetail) = Nothing

    Public Form_ID As String = ""
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing

#End Region

    Public Function SaveData(ByVal obj As clsApproverCreation, ByVal isNewEntry As Boolean) As Boolean

        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Try
            'clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Purchase Order", "Issue/Return/Transfer", obj.From_Location, obj.Doc_Date, trans)
            Dim qry As String = "delete from Tspl_HR_Requision_Approver_Detail where Emp_code='" + obj.Emp_Code + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from Tspl_HR_Expense_Approver_Detail where Emp_code='" + obj.Emp_Code + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim strDocNo As String = ""

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(obj.Doc_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Emp_Code", obj.Emp_Code)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks, True)

            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If isNewEntry Then
                ' clsCommon.AddColumnsForChange(coll, "Emp_Code", obj.Emp_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "Tspl_HR_Approver_Creation_Master", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "Tspl_HR_Approver_Creation_Master", OMInsertOrUpdate.Update, "Tspl_HR_Approver_Creation_Master.Emp_Code='" + obj.Emp_Code + "'", trans)
            End If
            isSaved = isSaved AndAlso clsApproverCreationDetail.SaveData(obj.Emp_Code, Arr, trans)
            isSaved = isSaved AndAlso clsApproverCreationExpenseDetail.SaveData(obj.Emp_Code, ArrExp, trans)
            isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.Emp_Code, obj.arrCustomFields, trans)

            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsApproverCreation
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsApproverCreation
        Dim obj As clsApproverCreation = Nothing
        Dim qry As String = "SELECT Tspl_HR_Approver_Creation_Master.Emp_Code,emP_name,Tspl_HR_Approver_Creation_Master.Doc_Date,Tspl_HR_Approver_Creation_Master.Remarks  FROM Tspl_HR_Approver_Creation_Master left outer join TSPL_Employee_MASTER as FLocation on FLocation.Emp_Code=Tspl_HR_Approver_Creation_Master.Emp_COde  where 2=2"
        Dim whrCls As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and Tspl_HR_Approver_Creation_Master.Emp_Code = (select MIN(Emp_Code) from Tspl_HR_Approver_Creation_Master WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Last
                qry += " and Tspl_HR_Approver_Creation_Master.Emp_Code = (select Max(Emp_Code) from Tspl_HR_Approver_Creation_Master WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and Tspl_HR_Approver_Creation_Master.Emp_Code = '" + strDocNo + "'"
            Case NavigatorType.Next
                qry += " and Tspl_HR_Approver_Creation_Master.Emp_Code = (select Min(Emp_Code) from Tspl_HR_Approver_Creation_Master where Emp_Code>'" + strDocNo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and Tspl_HR_Approver_Creation_Master.Emp_Code = (select Max(Emp_Code) from Tspl_HR_Approver_Creation_Master where Emp_Code<'" + strDocNo + "' " + whrCls + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsApproverCreation()
            obj.Emp_Code = clsCommon.myCstr(dt.Rows(0)("Emp_Code"))
            obj.Doc_Date = clsCommon.myCstr(dt.Rows(0)("Doc_Date"))
            obj.Emp_Name = clsCommon.myCstr(dt.Rows(0)("Emp_Name"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))






            qry = "SELECT Tspl_HR_Requision_Approver_Detail.Emp_Code,Tspl_HR_Requision_Approver_Detail.Req_Emp_Code,Emp_Name FROM Tspl_HR_Requision_Approver_Detail left join " _
                & " tspl_EMployee_master on tspl_EMployee_master.emp_Code=Tspl_HR_Requision_Approver_Detail.Req_emp_Code  where " _
                & " Tspl_HR_Requision_Approver_Detail.Emp_Code='" + obj.Emp_Code + "' "
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsApproverCreationDetail)
                Dim objTr As clsApproverCreationDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsApproverCreationDetail
                    objTr.Emp_Code = clsCommon.myCstr(dr("Emp_Code"))
                    objTr.Req_Emp_Code = clsCommon.myCstr(dr("Req_Emp_Code"))
                    objTr.Req_Emp_Name = clsCommon.myCstr(dr("Emp_Name"))
                    obj.Arr.Add(objTr)
                Next
            End If


            qry = "SELECT Tspl_HR_Expense_Approver_Detail.Emp_Code,Tspl_HR_Expense_Approver_Detail.EXP_Emp_Code,Emp_Name FROM Tspl_HR_Expense_Approver_Detail left join " _
                & " tspl_EMployee_master on tspl_EMployee_master.emp_Code=Tspl_HR_Expense_Approver_Detail.EXP_Emp_Code  where " _
                & " Tspl_HR_Expense_Approver_Detail.Emp_Code='" + obj.Emp_Code + "' "
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.ArrExp = New List(Of clsApproverCreationExpenseDetail)
                Dim objTr As clsApproverCreationExpenseDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsApproverCreationExpenseDetail
                    objTr.Emp_Code = clsCommon.myCstr(dr("Emp_Code"))
                    objTr.exp_Emp_Code = clsCommon.myCstr(dr("Exp_Emp_Code"))
                    objTr.exp_Emp_Name = clsCommon.myCstr(dr("Emp_Name"))
                    obj.ArrExp.Add(objTr)
                Next
            End If
        End If

        Return obj
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsApproverCreation = clsApproverCreation.GetData(strDocNo, NavigatorType.Current)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Emp_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

            Dim qry As String = "Update Tspl_HR_Approver_Creation_Master set POSTED=1, Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where Emp_Code ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
            'trans.Commit()
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try

        Return True
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Employee not found to Delete")
        End If
        Dim obj As clsApproverCreation = clsApproverCreation.GetData(strCode, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Emp_Code) > 0) Then
            Try
                'clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Purchase Order", "Issue/Return/Transfer", obj.From_Location, obj.Doc_Date, trans)

                Dim qry As String = "delete from Tspl_HR_Requision_Approver_Detail where emp_code='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from Tspl_HR_Expense_Approver_Detail where emp_code='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from Tspl_HR_Approver_Creation_Master where Emp_COde='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                isSaved = isSaved AndAlso clsCustomFieldValues.DeleteData(obj.Form_ID, strCode, trans)
                If (isSaved) Then
                    trans.Commit()
                Else
                    trans.Rollback()
                End If
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        End If
        Return isSaved
    End Function

    Private Function Doc_No() As Object
        Throw New NotImplementedException
    End Function


End Class

Public Class clsApproverCreationDetail
#Region "Variables"
    Public Emp_Code As String = Nothing

    Public Req_Emp_Code As String = Nothing
    Public Req_Emp_Name As String = Nothing


#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsApproverCreationDetail), ByVal trans As SqlTransaction) As Boolean
        Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)

        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsApproverCreationDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Emp_Code", obj.Emp_Code)
                clsCommon.AddColumnsForChange(coll, "Req_Emp_Code", obj.Req_Emp_Code)
                clsCommonFunctionality.UpdateDataTable(coll, "Tspl_HR_Requision_Approver_Detail", OMInsertOrUpdate.Insert, "", trans)



            Next
        End If
        Return True
    End Function
End Class

Public Class clsApproverCreationExpenseDetail
#Region "Variables"
    Public Emp_Code As String = Nothing

    Public exp_Emp_Code As String = Nothing
    Public exp_Emp_Name As String = Nothing


#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsApproverCreationExpenseDetail), ByVal trans As SqlTransaction) As Boolean
        Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)

        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsApproverCreationExpenseDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Emp_Code", obj.Emp_Code)
                clsCommon.AddColumnsForChange(coll, "EXP_Emp_Code", obj.exp_Emp_Code)
                clsCommonFunctionality.UpdateDataTable(coll, "Tspl_HR_Expense_Approver_Detail", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
End Class
