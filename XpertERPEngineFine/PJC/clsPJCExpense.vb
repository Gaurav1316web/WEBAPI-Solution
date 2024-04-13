Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsPJCExpense
#Region "Variables"
    Public Document_No As String = Nothing
    Public Document_Date As DateTime = Nothing
    Public EMP_CODE As String = Nothing
    Public PROJECT_CODE As String = Nothing
    Public Job_Code As String = Nothing
    Public Task_Code As String = Nothing
    Public Reference As String = Nothing
    Public Description As String = Nothing
    Public Posted As String = Nothing
    Public Posting_Date As DateTime = Nothing
    Public TotalCost As Double = 0
    Public TotPaymentCost As Double = 0
    Public Form_ID As String = ""
    'Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing

    Public Arr As List(Of ClsPJCExpenseDetail) = Nothing
#End Region
    Public Function SaveData(ByVal obj As clsPJCExpense, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Function SaveData(ByVal obj As clsPJCExpense, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim cntr As Integer = 0
        Dim isSaved As Boolean = True
        Try
            Dim qry As String = "delete from TSPL_PJC_EXPENSE_DETAIL where DOCUMENT_No='" + obj.Document_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strDocNo As String = ""

            If isNewEntry Then
                Dim strCode As String = clsDBFuncationality.getSingleValue("select isnull(max(DOCUMENT_No),'') from TSPL_PJC_EXPENSE_Header", trans)
                If clsCommon.myLen(strCode) <= 0 Then
                    obj.Document_No = "PJEX000000001"
                Else
                    obj.Document_No = clsCommon.incval(strCode)
                End If

            End If


            If (clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Posting_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "EMP_CODE", obj.EMP_CODE, True)
            clsCommon.AddColumnsForChange(coll, "PROJECT_CODE", obj.PROJECT_CODE, True)
            clsCommon.AddColumnsForChange(coll, "Job_Code", obj.Job_Code, True)
            clsCommon.AddColumnsForChange(coll, "Task_Code", obj.Task_Code, True)
            clsCommon.AddColumnsForChange(coll, "Reference", obj.Reference)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "TotalCost", obj.TotalCost)
            clsCommon.AddColumnsForChange(coll, "TotPaymentCost", obj.TotPaymentCost)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PJC_EXPENSE_Header", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PJC_EXPENSE_Header", OMInsertOrUpdate.Update, "Document_No='" + obj.Document_No + "'", trans)
            End If
            isSaved = isSaved AndAlso ClsPJCExpenseDetail.SaveData(obj.Document_No, Arr, trans)

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim obj As New clsPJCExpense()
        obj = clsPJCExpense.GetData(strCode, NavigatorType.Current, Nothing)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
            Try
                If (clsCommon.CompairString(obj.Posted, "Y") = CompairStringResult.Equal) Then
                    Throw New Exception("Already Posted on :" + clsCommon.GetPrintDate(obj.Posting_Date, "dd/MM/yyyy hh:mm tt"))
                End If
                Dim qry As String = "delete from TSPL_PJC_EXPENSE_DETAIL where Document_No='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_PJC_EXPENSE_HEADER where Document_No='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

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


    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsPJCExpense

        Dim obj As clsPJCExpense = Nothing
        Dim qry As String = "SELECT * from TSPL_PJC_EXPENSE_Header where 2=2"

        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_PJC_EXPENSE_Header.Document_No = (select MIN(Document_No) from TSPL_PJC_EXPENSE_Header where 1=1)"
            Case NavigatorType.Last
                qry += " and TSPL_PJC_EXPENSE_Header.Document_No = (select Max(Document_No) from TSPL_PJC_EXPENSE_Header where 1=1 )"
            Case NavigatorType.Next
                qry += " and TSPL_PJC_EXPENSE_Header.Document_No = (select Min(Document_No) from TSPL_PJC_EXPENSE_Header where Document_No>'" + strDocNo + "' )"
            Case NavigatorType.Previous
                qry += " and TSPL_PJC_EXPENSE_Header.Document_No = (select Max(Document_No) from TSPL_PJC_EXPENSE_Header where Document_No<'" + strDocNo + "' )"
            Case NavigatorType.Current
                qry += " and TSPL_PJC_EXPENSE_Header.Document_No = '" + strDocNo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsPJCExpense()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            obj.Reference = clsCommon.myCstr(dt.Rows(0)("Reference"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Posted = clsCommon.myCstr(dt.Rows(0)("Posted"))
            obj.EMP_CODE = clsCommon.myCstr(dt.Rows(0)("EMP_CODE"))
            obj.PROJECT_CODE = clsCommon.myCstr(dt.Rows(0)("PROJECT_CODE"))
            obj.Job_Code = clsCommon.myCstr(dt.Rows(0)("Job_Code"))
            obj.Task_Code = clsCommon.myCstr(dt.Rows(0)("Task_Code"))
            obj.TotalCost = clsCommon.myCdbl(dt.Rows(0)("TotalCost"))
            obj.TotPaymentCost = clsCommon.myCdbl(dt.Rows(0)("TotPaymentCost"))

            qry = "SELECT  * from TSPL_PJC_EXPENSE_Detail where  Document_No='" + obj.Document_No + "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of ClsPJCExpenseDetail)
                Dim objTr As ClsPJCExpenseDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New ClsPJCExpenseDetail()
                    objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                    objTr.Document_Line_No = clsCommon.myCdbl(dr("Document_Line_No"))
                    objTr.EXPENSE_CODE = clsCommon.myCstr(dr("EXPENSE_CODE"))
                    objTr.DESCRIPTION = clsCommon.myCstr(dr("DESCRIPTION"))
                    objTr.EXPENSE_TYPE = clsCommon.myCstr(dr("EXPENSE_TYPE"))
                    objTr.INTEGRATE_AP = clsCommon.myCstr(dr("INTEGRATE_AP"))
                    objTr.GLACCOUNT = clsCommon.myCstr(dr("GLACCOUNT"))
                    objTr.Cost = clsCommon.myCdbl(dr("Cost"))
                    objTr.Billing = clsCommon.myCstr(dr("Billing"))
                    objTr.Comments = clsCommon.myCstr(dr("Comments"))

                    obj.Arr.Add(objTr)
                Next
            End If
        End If
        Return obj
    End Function
End Class
Public Class ClsPJCExpenseDetail
#Region "Variables"
    Public Document_No As String = Nothing
    Public Document_Line_No As Integer = 0
    Public EXPENSE_CODE As String = Nothing
    Public DESCRIPTION As String = Nothing
    Public EXPENSE_TYPE As String = Nothing
    Public INTEGRATE_AP As String = Nothing
    Public GLACCOUNT As String = Nothing
    Public Cost As Double = 0
    Public Billing As String = Nothing
    Public Comments As String = Nothing
#End Region
    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of ClsPJCExpenseDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim counter As Integer = 1
            For Each objtr As ClsPJCExpenseDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Document_Line_No", counter)
                counter += 1
                clsCommon.AddColumnsForChange(coll, "EXPENSE_CODE", objtr.EXPENSE_CODE)
                clsCommon.AddColumnsForChange(coll, "DESCRIPTION", objtr.DESCRIPTION)
                clsCommon.AddColumnsForChange(coll, "EXPENSE_TYPE", objtr.EXPENSE_TYPE)
                clsCommon.AddColumnsForChange(coll, "INTEGRATE_AP", objtr.INTEGRATE_AP)
                clsCommon.AddColumnsForChange(coll, "Cost", objtr.Cost)
                clsCommon.AddColumnsForChange(coll, "Billing", objtr.Billing)
                clsCommon.AddColumnsForChange(coll, "GLACCOUNT", objtr.GLACCOUNT)
                clsCommon.AddColumnsForChange(coll, "Comments", objtr.Comments)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PJC_EXPENSE_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function


End Class
