Imports common
Imports System.Data.SqlClient
Public Class clsFormIssuehead

#Region "Variables"
    Public DemandNo As String = Nothing
    Public FormIssue_no As String = Nothing
    Public Demand_Date As Date? = Nothing
    Public FormCode As String = Nothing
    Public FormName As String = Nothing
    Public FormSeries As String = Nothing
    Public FromNo As Integer = 0
    Public ToNo As Integer = 0
    Public TotalForms As Integer = 0
    Public Remarks As String = Nothing
    Public Comments As String = Nothing
    Public Arr As List(Of clsFormIssueDetail) = Nothing


#End Region

    Public Function SaveData(ByVal obj As clsFormIssuehead, ByVal isNewEntry As Boolean) As Boolean

        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Try
            Dim qry As String = "delete from TSPL_FORMISSUE_Detail where FormIssue_no='" + obj.FormIssue_no + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim strDocNo As String = ""

            If obj.FormSeries <> "" And isNewEntry = True Then
                obj.FormIssue_no = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Demand_Date), clsDocType.FormIssue, Nothing, Nothing)
                If (clsCommon.myLen(obj.FormIssue_no) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If

            End If


            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "DemandNo", obj.DemandNo)
            clsCommon.AddColumnsForChange(coll, "Demand_Date", clsCommon.GetPrintDate(obj.Demand_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "FormCode", obj.FormCode)
            clsCommon.AddColumnsForChange(coll, "FormName", obj.FormName)
            clsCommon.AddColumnsForChange(coll, "FormSeries", obj.FormSeries)
            clsCommon.AddColumnsForChange(coll, "FromNo", obj.FromNo)
            clsCommon.AddColumnsForChange(coll, "ToNo", obj.ToNo)
            clsCommon.AddColumnsForChange(coll, "TotalForms", obj.TotalForms)
            clsCommon.AddColumnsForChange(coll, "Comments", obj.Comments)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)

            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))


            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "FormIssue_no", obj.FormIssue_no)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_FORMISSUE_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_FORMISSUE_HEAD", OMInsertOrUpdate.Update, "TSPL_FORMISSUE_HEAD.FormIssue_no='" + obj.FormIssue_no + "'", trans)
            End If


            isSaved = isSaved AndAlso clsFormIssueDetail.SaveData(obj.FormIssue_no, Arr, trans)
            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsFormIssuehead
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Demand No not found to Delete")
        End If
        Dim obj As clsFormIssuehead = clsFormIssuehead.GetData(strCode, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.DemandNo) > 0) Then
            Try

                Dim qry As String = "delete from tspl_formissue_detail where FormIssue_no='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from tspl_formissue_Head where FormIssue_no='" + strCode + "'"
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
    Public Shared Function GetData(ByVal strDemandNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsFormIssuehead
        Dim obj As clsFormIssuehead = Nothing
        Dim qry As String = "select TSPL_formissue_head.FormIssue_no,TSPL_formissue_head.demandNo,TSPL_formissue_head.demand_date,TSPL_formissue_head.formcode,TSPL_formissue_head.formname,TSPL_formissue_head.formseries,TSPL_formissue_head.fromno,TSPL_formissue_head.Tono,TSPL_formissue_head.totalforms,TSPL_formissue_head.remarks,TSPL_formissue_head.comments  from TSPL_formissue_head where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_formissue_head.FormIssue_no = (select MIN(FormIssue_no) from TSPL_SRN_HEAD)"
            Case NavigatorType.Last
                qry += " and TSPL_formissue_head.FormIssue_no = (select Max(FormIssue_no) from TSPL_SRN_HEAD)"
            Case NavigatorType.Current
                qry += " and TSPL_formissue_head.FormIssue_no = '" + strDemandNo + "'"
            Case NavigatorType.Next
                qry += " and TSPL_formissue_head.FormIssue_no = (select Min(FormIssue_no) from TSPL_formissue_head where FormIssue_no>'" + strDemandNo + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_formissue_head.FormIssue_no = (select Max(FormIssue_no) from TSPL_formissue_head where FormIssue_no<'" + strDemandNo + "')"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsFormIssuehead()
            obj.FormIssue_no = clsCommon.myCstr(dt.Rows(0)("FormIssue_no"))
            obj.DemandNo = clsCommon.myCstr(dt.Rows(0)("DemandNo"))
            obj.Demand_Date = clsCommon.myCDate(dt.Rows(0)("Demand_Date"))
            obj.FormCode = clsCommon.myCstr(dt.Rows(0)("FormCode"))
            obj.FormName = clsCommon.myCstr(dt.Rows(0)("FormName"))
            obj.FormSeries = clsCommon.myCstr(dt.Rows(0)("FormSeries"))
            obj.FromNo = clsCommon.myCdbl(dt.Rows(0)("FromNo"))
            obj.ToNo = clsCommon.myCdbl(dt.Rows(0)("ToNo"))
            obj.TotalForms = clsCommon.myCdbl(dt.Rows(0)("TotalForms"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Comments = clsCommon.myCstr(dt.Rows(0)("Comments"))


            qry = "select tspl_formissue_detail.FormIssue_no,tspl_formissue_detail.Formno,tspl_formissue_detail.FormDate,tspl_formissue_detail.Expirydate,tspl_formissue_detail.Remarks,tspl_formissue_detail.line_no from tspl_formissue_detail where tspl_formissue_detail.FormIssue_no='" + obj.FormIssue_no + "' ORDER BY tspl_formissue_detail.Line_No"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsFormIssueDetail)
                Dim objTr As clsFormIssueDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsFormIssueDetail
                    objTr.Line_no = clsCommon.myCstr(dr("Line_no"))
                    objTr.FormIssue_no = clsCommon.myCstr(dr("FormIssue_no"))
                    objTr.FormNo = clsCommon.myCstr(dr("FormNo"))
                    objTr.FormDate = clsCommon.myCDate(dr("FormDate"))
                    objTr.Expirydate = clsCommon.myCDate(dr("Expirydate"))
                    objTr.Remarks = clsCommon.myCstr(dr("Remarks"))

                    obj.Arr.Add(objTr)
                Next
            End If

        End If

        Return obj
    End Function

End Class
Public Class clsFormIssueDetail
#Region "Variables"
    Public Line_no As Integer = 0
    Public DemandNo As String = Nothing
    Public FormNo As String = Nothing
    Public FormDate As Date? = Nothing
    Public Expirydate As Date? = Nothing
    Public Remarks As String = Nothing
    Public FormIssue_no As String = Nothing


#End Region


    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsFormIssueDetail), ByVal trans As SqlTransaction) As Boolean

        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsFormIssueDetail In Arr
                Dim coll As New Hashtable()

                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_no)
                clsCommon.AddColumnsForChange(coll, "FormIssue_no", strDocNo)
                clsCommon.AddColumnsForChange(coll, "FormNo", obj.FormNo)
                clsCommon.AddColumnsForChange(coll, "FormDate", clsCommon.GetPrintDate(obj.FormDate, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Expirydate", clsCommon.GetPrintDate(obj.Expirydate, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_FORMISSUE_Detail", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
End Class