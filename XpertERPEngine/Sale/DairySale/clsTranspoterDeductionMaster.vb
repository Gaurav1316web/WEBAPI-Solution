Imports common
Imports System.Data.SqlClient

Public Class clsTranspoterDeductionHeader
#Region "Variables"
    Public DEDUCTION_CODE As String = Nothing
    Public DEDUCTION_DATE As Date? = Nothing
    Public DESCRIPTION As String = Nothing
    Public DEDUCTION_CATEGORY As String = Nothing
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Post_Date As Date? = Nothing
    Public ArrDeductionDetails As List(Of clsTranspoterDeductionDetails) = Nothing



#End Region

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select DEDUCTION_CODE as Code,convert(varchar,DEDUCTION_DATE,103) as [Date], DESCRIPTION as [Description], DEDUCTION_CATEGORY as [Deduction Category]  from TSPL_TRANSPOTER_DEDUCTION_HEADER  "
        str = clsCommon.ShowSelectForm("Transporter@Deduction", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function

    Public Function SaveData(ByVal obj As clsTranspoterDeductionHeader, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If SaveData(obj, isNewEntry, trans) Then
                trans.Commit()
            Else
                trans.Rollback()
                Return False
            End If

        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Function SaveData(ByVal obj As clsTranspoterDeductionHeader, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            clsDBFuncationality.ExecuteNonQuery("DELETE from TSPL_TRANSPOTER_DEDUCTION_DETAIL WHERE DEDUCTION_CODE ='" + obj.DEDUCTION_CODE + "'", trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "DEDUCTION_DATE", clsCommon.GetPrintDate(obj.DEDUCTION_DATE, "dd/MMM/yyyy hh:mm:ss tt "))
            clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.DESCRIPTION)

            clsCommon.AddColumnsForChange(coll, "DEDUCTION_CATEGORY", obj.DEDUCTION_CATEGORY)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If isNewEntry Then
                obj.DEDUCTION_CODE = clsERPFuncationality.GetNextCode(trans, obj.DEDUCTION_DATE, clsDocType.TransporterDeductionMaster, "", "")
                clsCommon.AddColumnsForChange(coll, "DEDUCTION_CODE", obj.DEDUCTION_CODE)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TRANSPOTER_DEDUCTION_HEADER", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TRANSPOTER_DEDUCTION_HEADER", OMInsertOrUpdate.Update, "DEDUCTION_CODE='" + obj.DEDUCTION_CODE + "'", trans)
            End If

            clsTranspoterDeductionDetails.SaveData(obj.DEDUCTION_CODE, obj.ArrDeductionDetails, obj.DEDUCTION_DATE, trans)


        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As common.NavigatorType, Optional ByVal trans As SqlTransaction = Nothing) As clsTranspoterDeductionHeader
        Dim obj As clsTranspoterDeductionHeader = Nothing
        Dim qry As String = " SELECT TSPL_TRANSPOTER_DEDUCTION_HEADER.* FROM TSPL_TRANSPOTER_DEDUCTION_HEADER " &
                            " where  2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_TRANSPOTER_DEDUCTION_HEADER.DEDUCTION_CODE=(select MIN(DEDUCTION_CODE) from TSPL_TRANSPOTER_DEDUCTION_HEADER Where 1=1 )"
            Case NavigatorType.Last
                qry += " and TSPL_TRANSPOTER_DEDUCTION_HEADER.DEDUCTION_CODE=(select Max(DEDUCTION_CODE) from TSPL_TRANSPOTER_DEDUCTION_HEADER Where 1=1 )"
            Case NavigatorType.Next
                qry += " and TSPL_TRANSPOTER_DEDUCTION_HEADER.DEDUCTION_CODE=(select Min(DEDUCTION_CODE) from TSPL_TRANSPOTER_DEDUCTION_HEADER where DEDUCTION_CODE > '" + strCode + "' )"
            Case NavigatorType.Previous
                qry += " and TSPL_TRANSPOTER_DEDUCTION_HEADER.DEDUCTION_CODE=(select Max(DEDUCTION_CODE) from TSPL_TRANSPOTER_DEDUCTION_HEADER where DEDUCTION_CODE < '" + strCode + "' )"
            Case NavigatorType.Current
                qry += " and TSPL_TRANSPOTER_DEDUCTION_HEADER.DEDUCTION_CODE='" + strCode + "'"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsTranspoterDeductionHeader()
            obj.DEDUCTION_CODE = clsCommon.myCstr(dt.Rows(0)("DEDUCTION_CODE"))
            obj.DEDUCTION_DATE = clsCommon.myCDate(dt.Rows(0)("DEDUCTION_DATE"))
            obj.DESCRIPTION = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))
            obj.DEDUCTION_CATEGORY = clsCommon.myCstr(dt.Rows(0)("DEDUCTION_CATEGORY"))
            obj.Status = ERPTransactionStatus.Pending 'IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) > 0, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            obj.ArrDeductionDetails = clsTranspoterDeductionDetails.GetData(obj.DEDUCTION_CODE, trans)

        End If
        Return obj
    End Function

    Public Shared Function fundelete(ByVal strDeductionCode As String, ByVal trans As SqlTransaction) As Boolean
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim obj As clsTranspoterDeductionHeader
            If clsCommon.myLen(strDeductionCode) > 0 Then
                obj = clsTranspoterDeductionHeader.GetData(strDeductionCode, NavigatorType.Current, trans)
            Else
                Throw New Exception("Document not found to delete.")
            End If
            clsDBFuncationality.ExecuteNonQuery("DELETE from TSPL_TRANSPOTER_DEDUCTION_DETAIL WHERE DEDUCTION_CODE ='" + obj.DEDUCTION_CODE + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("DELETE from TSPL_TRANSPOTER_DEDUCTION_HEADER WHERE DEDUCTION_CODE ='" + obj.DEDUCTION_CODE + "'", trans)
            'clsDBFuncationality.ExecuteNonQuery("DELETE from TSPL_SALES_INCENTIVE_SLAB WHERE DEDUCTION_CODE ='" + obj.DEDUCTION_CODE + "'", trans)
            'clsDBFuncationality.ExecuteNonQuery("Delete From TSPL_SALES_INCENTIVE_HEADER Where DEDUCTION_CODE='" + obj.DEDUCTION_CODE + "'", trans)
            'clsCustomFieldValues.DeleteData(obj.Form_ID, obj.DEDUCTION_CODE, trans)
            'trans.Commit()
            'Return True
        Catch ex As Exception
            'trans.Rollback()
            'clsCommon.MyMessageBoxShow(ex.Message)
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function postData(ByVal StrDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            postData(StrDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function postData(ByVal strDeductionNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim isPosted As Boolean = True
            If (clsCommon.myLen(strDeductionNo) <= 0) Then
                Throw New Exception(" Deduction No not found to Post")
            End If
            clsERPFuncationality.IsDocumentAlreadyPosted("TSPL_TRANSPOTER_DEDUCTION_HEADER", "DEDUCTION_CODE", strDeductionNo, "Status=1", trans)
            Dim obj As clsTranspoterDeductionHeader = clsTranspoterDeductionHeader.GetData(strDeductionNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.DEDUCTION_CODE) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Post on :" + clsCommon.GetPrintDate(obj.DEDUCTION_DATE, "dd/MM/yyyy"))
            End If
            Dim strQry As String = " update TSPL_TRANSPOTER_DEDUCTION_HEADER set Status='1', Post_Date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") & "' , Post_By = '" + objCommonVar.CurrentUserCode + "' where DEDUCTION_CODE='" & strDeductionNo & "' "
            isPosted = isPosted AndAlso clsDBFuncationality.ExecuteNonQuery(strQry, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    'Public Shared Function InActiveData(ByVal strCode As String) As Boolean
    '    Dim isSaved As Boolean = True
    '    Try
    '        Dim isPosted As Boolean = True
    '        If (clsCommon.myLen(strCode) <= 0) Then
    '            Throw New Exception("Incentive No not found to Post")
    '        End If
    '        Dim obj As clsTranspoterDeductionHeader = clsTranspoterDeductionHeader.GetData(strCode, NavigatorType.Current, Nothing)
    '        If (obj Is Nothing OrElse clsCommon.myLen(obj.DEDUCTION_CODE) <= 0) Then
    '            Throw New Exception("No Data found to Post")
    '        End If
    '        If Not (obj.Status = ERPTransactionStatus.Approved) Then
    '            Throw New Exception("Document Should be posted for inactive")
    '        End If
    '        Dim strQry As String = " update TSPL_SALES_INCENTIVE_HEADER set In_Active='1',In_Active_Date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy") & "' , In_Active_By = '" + objCommonVar.CurrentUserCode + "' where DEDUCTION_CODE='" & strCode & "' "
    '        isPosted = isPosted AndAlso clsDBFuncationality.ExecuteNonQuery(strQry)
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    '    Return True
    'End Function
End Class

Public Class clsTranspoterDeductionDetails
#Region "Variables"
    Public TR_CODE As String = Nothing
    Public DEDUCTION_CODE As String = Nothing
    Public SNO As Integer = 0
    Public TYPE As String = Nothing
    Public Amount As Double = 0.0
    Public GL_CODE As String = Nothing
    Public GL_DESC As String = Nothing

#End Region


    Public Shared Function SaveData(ByVal strDeductionCode As String, ByVal Arr As List(Of clsTranspoterDeductionDetails), ByVal dtDocDate As DateTime, ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsTranspoterDeductionDetails In Arr
                Dim coll As New Hashtable()
                obj.TR_CODE = clsERPFuncationality.GetNextCode(trans, dtDocDate, clsDocType.Detail, clsDocTransactionType.Detail, "")
                clsCommon.AddColumnsForChange(coll, "TR_CODE", obj.TR_CODE)
                clsCommon.AddColumnsForChange(coll, "SNO", obj.SNO)
                clsCommon.AddColumnsForChange(coll, "DEDUCTION_CODE", strDeductionCode)
                clsCommon.AddColumnsForChange(coll, "TYPE", obj.TYPE)
                clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                clsCommon.AddColumnsForChange(coll, "GL_CODE", obj.GL_CODE)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TRANSPOTER_DEDUCTION_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsTranspoterDeductionDetails)
        Dim arr As New List(Of clsTranspoterDeductionDetails)
        Dim qry As String = "Select TSPL_TRANSPOTER_DEDUCTION_DETAIL.* from TSPL_TRANSPOTER_DEDUCTION_DETAIL Where DEDUCTION_CODE='" + strCode + "'  "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)


        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                Dim obj As New clsTranspoterDeductionDetails()
                obj.TR_CODE = clsCommon.myCstr(dr("TR_CODE"))
                obj.DEDUCTION_CODE = clsCommon.myCstr(dr("DEDUCTION_CODE"))
                obj.TYPE = clsCommon.myCstr(dr("TYPE"))
                obj.Amount = clsCommon.myCdbl(dr("Amount"))
                obj.GL_CODE = clsCommon.myCstr(dr("GL_CODE"))
                obj.GL_DESC = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + obj.GL_CODE + "' ")
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function


End Class





