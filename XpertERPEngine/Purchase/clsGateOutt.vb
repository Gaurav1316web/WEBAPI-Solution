Imports common
Imports System.Data.SqlClient

Public Class clsGateOutt
#Region "Variables"
    Public Code As String = Nothing
    Public docDate As DateTime?
    Public Vendor_Desc As String = Nothing
    Public GRN_code As String = Nothing
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Description As String = Nothing
    Public Remarks As String = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As DateTime?
    Public Modify_By As String = Nothing
    Public Modify_Date As DateTime?
    Public Posted_By As String = Nothing
    Public Posted_Date As DateTime?
#End Region
    Public Function SaveData(ByVal obj As clsGateOutt, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            'clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Purchase Order", "Store Received Note Return", obj.Bill_To_Location, Document_Date, trans)
            Try
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Date", clsCommon.GetPrintDate(obj.docDate, "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "GRN_Code", obj.GRN_code)
                clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                If isNewEntry Then
                    obj.Code = clsERPFuncationality.GetNextCode(trans, obj.docDate, clsDocType.GTOut, "", "")
                    If (clsCommon.myLen(obj.Code) <= 0) Then
                        Throw New Exception("Error in Document Code Generation")
                    End If
                    clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "tspl_purchase_gateout", OMInsertOrUpdate.Insert, "", trans)
                Else
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "tspl_purchase_gateout", OMInsertOrUpdate.Update, "tspl_purchase_gateout.Code='" + obj.Code + "'", trans)
                End If
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Code, "tspl_purchase_gateout", "Code", trans)
                trans.Commit()
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsGateOutt
        Dim obj As clsGateOutt = Nothing
        Dim qry As String = "SELECT tspl_purchase_gateout.* from tspl_purchase_gateout  where 2=2"
        Dim whrCls As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and tspl_purchase_gateout.code = (select MIN(code) from tspl_purchase_gateout WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Last
                qry += " and tspl_purchase_gateout.code = (select Max(code) from tspl_purchase_gateout WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and tspl_purchase_gateout.code = '" + strDocNo + "'"
            Case NavigatorType.Next
                qry += " and tspl_purchase_gateout.code = (select Min(code) from tspl_purchase_gateout where code>'" + strDocNo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and tspl_purchase_gateout.code = (select Max(code) from tspl_purchase_gateout where code<'" + strDocNo + "' " + whrCls + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsGateOutt()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
            obj.docDate = clsCommon.myCDate(dt.Rows(0)("Date"))
            obj.GRN_code = clsCommon.myCstr(dt.Rows(0)("GRN_code"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            If clsCommon.myCDecimal(dt.Rows(0)("Status")) = 1 Then
                obj.Status = ERPTransactionStatus.Approved
                obj.Posted_Date = clsCommon.myCDate(dt.Rows(0)("Posted_Date"))
            End If
        End If
        Return obj
    End Function
    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsGateOutt = clsGateOutt.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Post on :" + clsCommon.GetPrintDate(obj.Posted_Date, "dd/MM/yyyy"))
            End If
            Dim qry As String = "Update tspl_purchase_gateout set Status=1, Posted_Date='" + strPostDate + "',Posted_By='" + objCommonVar.CurrentUserCode + "'  where Code='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function DeleteData(ByVal Doc_No As String) As Boolean
        Dim qry As String
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim obj As clsGateOutt = clsGateOutt.GetData(Doc_No, NavigatorType.Current, trans)
        Try
            If Not (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
                Throw New Exception("Document not found")
            End If

            If (obj.Status = 1) Then
                Throw New Exception("Already Posted on :" + obj.Posted_Date)
            End If

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Code, "tspl_purchase_gateout", "Code", trans)
            qry = "delete from tspl_purchase_gateout where Code='" + obj.Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim qry As String = "
                        select tspl_purchase_gateout.Code,tspl_purchase_gateout.Date, case when tspl_purchase_gateout.Status=1 then 'Approved' else 'Pending' end as Status,Posted_Date,TSPL_SRN_HEAD.Against_GRN from tspl_purchase_gateout
                        left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.Against_GRN=tspl_purchase_gateout.GRN_Code"
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrcls = "  TSPL_SRN_HEAD.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ") and "
        End If
        whrcls += "  TSPL_SRN_HEAD.Status=1"
        Dim Str As String = clsCommon.ShowSelectForm("NIRQCFnd", qry, "Code", whrcls, "", "Code", isButtonClicked, "tspl_purchase_gateout.Date")
        Return Str
    End Function
End Class
