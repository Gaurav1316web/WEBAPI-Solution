Imports common
Imports System.Data.SqlClient

Public Class clsRequestMaster

#Region "Variables"
    Public REQUEST_CODE As String = Nothing
    Public REQUEST_DATE As Date? = Nothing
    Public REQUEST_BY As String = Nothing
    Public REQUEST_FOR As String = Nothing
    Public SCREEN_CODE As String = Nothing
    Public REASON As String = Nothing
    Public REMARKS As String = Nothing
    Public POSTED As Integer = 0
    Public APPROVED_STATUS As Integer = 0
#End Region

    Public Shared Function getUserName(ByVal UserCode As String, ByVal trans As SqlTransaction) As String
        Dim UserName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select USER_NAME from TSPL_USER_MASTER where User_Code = '" + UserCode + "' ", trans))
        Return UserName
    End Function
    Public Shared Function getReportingUserCode(ByVal UserCode As String, ByVal trans As SqlTransaction) As String
        Dim User As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select Level4_Code from TSPL_USER_MASTER where User_Code = '" + UserCode + "' ", trans))
        Return User
    End Function
    'Select Level4_Code,* from TSPL_USER_MASTER where User_Code = 'Admin'

    Public Function SaveData(ByVal obj As clsRequestMaster, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Function SaveData(ByVal obj As clsRequestMaster, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "REQUEST_DATE", clsCommon.GetPrintDate(obj.REQUEST_DATE, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "REQUEST_BY", obj.REQUEST_BY)
            clsCommon.AddColumnsForChange(coll, "REQUEST_FOR", obj.REQUEST_FOR)
            clsCommon.AddColumnsForChange(coll, "SCREEN_CODE", obj.SCREEN_CODE)
            clsCommon.AddColumnsForChange(coll, "REASON", obj.REASON)
            clsCommon.AddColumnsForChange(coll, "REMARKS", obj.REMARKS)
            clsCommon.AddColumnsForChange(coll, "POSTED", obj.POSTED)
            clsCommon.AddColumnsForChange(coll, "APPROVED_STATUS", obj.APPROVED_STATUS)
            clsCommon.AddColumnsForChange(coll, "COMP_CODE", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                obj.REQUEST_CODE = clsERPFuncationality.GetNextCode(trans, obj.REQUEST_DATE, clsDocType.UserRequestMaster, "", "")
                If (clsCommon.myLen(obj.REQUEST_CODE) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "REQUEST_CODE", obj.REQUEST_CODE)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_USER_REQUEST_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_USER_REQUEST_MASTER", OMInsertOrUpdate.Update, "TSPL_USER_REQUEST_MASTER.REQUEST_CODE ='" + obj.REQUEST_CODE + "'", trans)
            End If

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsRequestMaster
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsRequestMaster
        Dim obj As clsRequestMaster = Nothing
        Dim qry As String = ""
        qry = " select * from TSPL_USER_REQUEST_MASTER where 2=2 "
        Dim whrClas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_USER_REQUEST_MASTER.REQUEST_CODE = (select MIN(REQUEST_CODE) from TSPL_USER_REQUEST_MASTER where 1=1 )"
            Case NavigatorType.Last
                qry += " and TSPL_USER_REQUEST_MASTER.REQUEST_CODE = (select Max(REQUEST_CODE) from TSPL_USER_REQUEST_MASTER where 1=1 )"
            Case NavigatorType.Next
                qry += " and TSPL_USER_REQUEST_MASTER.REQUEST_CODE = (select Min(REQUEST_CODE) from TSPL_USER_REQUEST_MASTER where REQUEST_CODE>'" + strDocumentNo + "'" + whrClas + " )"
            Case NavigatorType.Previous
                qry += " and TSPL_USER_REQUEST_MASTER.REQUEST_CODE = (select Max(REQUEST_CODE) from TSPL_USER_REQUEST_MASTER where REQUEST_CODE<'" + strDocumentNo + "'" + whrClas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_USER_REQUEST_MASTER.REQUEST_CODE = '" + strDocumentNo + "'"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsRequestMaster()
            obj.REQUEST_CODE = clsCommon.myCstr(dt.Rows(0)("REQUEST_CODE"))
            obj.REQUEST_DATE = clsCommon.myCDate(dt.Rows(0)("REQUEST_DATE"))
            obj.REQUEST_BY = clsCommon.myCstr(dt.Rows(0)("REQUEST_BY"))
            obj.REQUEST_FOR = clsCommon.myCstr(dt.Rows(0)("REQUEST_FOR"))
            obj.SCREEN_CODE = clsCommon.myCstr(dt.Rows(0)("SCREEN_CODE"))
            obj.REASON = clsCommon.myCstr(dt.Rows(0)("REASON"))
            obj.REMARKS = clsCommon.myCstr(dt.Rows(0)("REMARKS"))
            obj.POSTED = IIf(clsCommon.myCdbl(dt.Rows(0)("POSTED")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            If clsCommon.myCdbl(dt.Rows(0)("APPROVED_STATUS")) = 0 Then
                obj.APPROVED_STATUS = ERPTransactionStatus.Pending
            Else
                obj.APPROVED_STATUS = IIf(clsCommon.myCdbl(dt.Rows(0)("APPROVED_STATUS")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Reject)
            End If
        End If
        Return obj
    End Function

    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(strDocNo, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim obj As clsRequestMaster = clsRequestMaster.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.REQUEST_CODE) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Posted = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Post Date")
            End If

            Dim qry As String = "Update TSPL_USER_REQUEST_MASTER set POSTED=1,Posted_By='" + objCommonVar.CurrentUserCode + "',Posted_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt") + "' where REQUEST_CODE='" + obj.REQUEST_CODE + "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim qry As String = ""
        Try
            qry = "Delete from TSPL_USER_REQUEST_MASTER where REQUEST_CODE='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)
            tran.Commit()
        Catch err As Exception
            tran.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "select TSPL_USER_REQUEST_MASTER.REQUEST_CODE as Code,convert (varchar,TSPL_USER_REQUEST_MASTER.REQUEST_DATE,103) as [Request Date] , TSPL_USER_REQUEST_MASTER.REQUEST_BY as [Request By], TSPL_USER_REQUEST_MASTER.REQUEST_FOR as [Request To], case when POSTED = 1 then 'Posted' else 'Pending' end [Posting Status], case when APPROVED_STATUS = 1 then 'Approved' when APPROVED_STATUS = 2 then 'Rejected' else 'Pending' end [Approval Status]    from TSPL_USER_REQUEST_MASTER "
        str = clsCommon.ShowSelectForm("RequestCode@UserRequest", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function

End Class
