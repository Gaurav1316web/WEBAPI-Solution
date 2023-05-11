Imports common
Imports System.Data.SqlClient

Public Class clsDailyElectricalEntryHead
#Region "Variables"
    Public Document_No As String = String.Empty
    Public Document_Date As Date = Nothing
    Public Consumption_Date As Date = Nothing
    Public Remarks As String = String.Empty
    Public Posted As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Posted_Date As DateTime? = Nothing
    Public Arr As List(Of clsDailyElectricalEntrySlotDetails) = Nothing
    Public ArrDG As List(Of clsDailyElectricalEntryDGDetails) = Nothing

#End Region

    Public Shared Function SaveData(ByVal obj As clsDailyElectricalEntryHead, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            'clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "JobWork Outward", "JobWork SRN", obj.Loc_Code, obj.Document_Date, trans)

            If Not isNewEntry Then
                clsERPFuncationality.IsDocumentAlreadyPosted("TSPL_DAILY_ELECTRICAL_ENTRY_HEAD", "Document_No", obj.Document_No, "Posted=1", trans)
            End If
            Dim qry As String = "Delete from TSPL_DAILY_ELECTRICAL_ENTRY_EB_UNIT_DETAILS where Document_No='" + obj.Document_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from TSPL_DAILY_ELECTRICAL_ENTRY_DG_DETAILS where Document_No='" + obj.Document_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Consumption_Date", clsCommon.GetPrintDate(obj.Consumption_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.DailyElectricalEntry, "", "")
                If clsCommon.myLen(obj.Document_No) <= 0 Then
                    Throw New Exception("Error in document generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DAILY_ELECTRICAL_ENTRY_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DAILY_ELECTRICAL_ENTRY_HEAD", OMInsertOrUpdate.Update, "TSPL_DAILY_ELECTRICAL_ENTRY_HEAD.Document_No='" + obj.Document_No + "'", trans)
            End If
            clsDailyElectricalEntrySlotDetails.saveData(obj.Arr, obj.Document_No, trans)
            clsDailyElectricalEntryDGDetails.saveData(obj.ArrDG, obj.Document_No, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocumentCode As String, ByVal navtype As NavigatorType, Optional ByVal trans As SqlTransaction = Nothing) As clsDailyElectricalEntryHead
        Dim obj As clsDailyElectricalEntryHead = Nothing
        Try
            Dim qry As String = " select TSPL_DAILY_ELECTRICAL_ENTRY_HEAD.* from TSPL_DAILY_ELECTRICAL_ENTRY_HEAD where 2=2"
            Dim whrCls As String = "  "
            'If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            '    whrCls = " and Loc_code in (" & objCommonVar.strCurrUserLocations & ")"
            'End If
            qry = qry & whrCls
            Select Case navtype
                Case NavigatorType.Current
                    qry += " and TSPL_DAILY_ELECTRICAL_ENTRY_HEAD.Document_No in ('" + strDocumentCode + "') "
                Case NavigatorType.Next
                    qry += " and TSPL_DAILY_ELECTRICAL_ENTRY_HEAD.Document_No in (select min(Document_No ) from TSPL_DAILY_ELECTRICAL_ENTRY_HEAD where Document_No  >'" + strDocumentCode + "' " & whrCls & " )"
                Case NavigatorType.First
                    qry += " and TSPL_DAILY_ELECTRICAL_ENTRY_HEAD.Document_No in (select MIN(Document_No ) from TSPL_DAILY_ELECTRICAL_ENTRY_HEAD  where 1=1 " & whrCls & " )"
                Case NavigatorType.Last
                    qry += " and TSPL_DAILY_ELECTRICAL_ENTRY_HEAD.Document_No in (select Max(Document_No ) from TSPL_DAILY_ELECTRICAL_ENTRY_HEAD  where 1=1 " & whrCls & " )"
                Case NavigatorType.Previous
                    qry += " and TSPL_DAILY_ELECTRICAL_ENTRY_HEAD.Document_No in (select Max(Document_No ) from TSPL_DAILY_ELECTRICAL_ENTRY_HEAD where Document_No  <'" + strDocumentCode + "'  " & whrCls & " )"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj = New clsDailyElectricalEntryHead()
                obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
                obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
                obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
                obj.Consumption_Date = clsCommon.myCDate(dt.Rows(0)("Consumption_Date"))

                obj.Posted = IIf(clsCommon.myCdbl(dt.Rows(0)("Posted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
                If obj.Posted = ERPTransactionStatus.Approved Then
                    obj.Posted_Date = clsCommon.myCDate(dt.Rows(0)("Posted_Date"))
                End If
                obj.Arr = clsDailyElectricalEntrySlotDetails.GetData(obj.Document_No, trans)
                obj.ArrDG = clsDailyElectricalEntryDGDetails.GetData(obj.Document_No, trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function

    Public Shared Function deleteData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            clsERPFuncationality.IsDocumentAlreadyPosted("TSPL_DAILY_ELECTRICAL_ENTRY_HEAD", "Document_No", strDocNo, "Posted=1", trans)
            Dim qry As String = "delete from TSPL_DAILY_ELECTRICAL_ENTRY_DG_DETAILS where Document_No='" & strDocNo & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_DAILY_ELECTRICAL_ENTRY_EB_UNIT_DETAILS where Document_No='" & strDocNo & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_DAILY_ELECTRICAL_ENTRY_HEAD where Document_No='" & strDocNo & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try
            Dim qry As String = " select TSPL_DAILY_ELECTRICAL_ENTRY_HEAD.Document_No as [DocumentNo] , convert (varchar,TSPL_DAILY_ELECTRICAL_ENTRY_HEAD.Document_Date,103) as [Document Date],TSPL_DAILY_ELECTRICAL_ENTRY_HEAD.Remarks ,convert (varchar,TSPL_DAILY_ELECTRICAL_ENTRY_HEAD.Consumption_Date,103) as [Consumption Date], case when  TSPL_DAILY_ELECTRICAL_ENTRY_HEAD.Posted = 1 then 'Yes' else 'No' end as Posted  From TSPL_DAILY_ELECTRICAL_ENTRY_HEAD"
            str = clsCommon.ShowSelectForm("DAilyElect@EntryFinder", qry, "DocumentNo", whrcls, curcode, "DocumentNo", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return str
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

    Public Shared Function postData(ByVal StrDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim isPosted As Boolean = True
            If (clsCommon.myLen(StrDocNo) <= 0) Then
                Throw New Exception(" Doc No not found to Post")
            End If
            clsERPFuncationality.IsDocumentAlreadyPosted("TSPL_DAILY_ELECTRICAL_ENTRY_HEAD", "Document_No", StrDocNo, "Posted=1", trans)
            Dim obj As clsDailyElectricalEntryHead = clsDailyElectricalEntryHead.GetData(StrDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

            If (obj.Posted = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Post on :" + clsCommon.GetPrintDate(obj.Posted_Date, "dd/MM/yyyy"))
            End If
            Dim strQry As String = " update TSPL_DAILY_ELECTRICAL_ENTRY_HEAD set Posted='1', Posted_Date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") & "' where Document_No='" & StrDocNo & "' "
            isPosted = isPosted AndAlso clsDBFuncationality.ExecuteNonQuery(strQry, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function ReverseAndUnpostData(ByVal StrDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            ReverseAndUnpostData(StrDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function ReverseAndUnpostData(ByVal StrDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim isPosted As Boolean = True
            If (clsCommon.myLen(StrDocNo) <= 0) Then
                Throw New Exception("Doc No not found to Post")
            End If
            Dim obj As clsDailyElectricalEntryHead = clsDailyElectricalEntryHead.GetData(StrDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Posted = ERPTransactionStatus.Pending) Then
                Throw New Exception("Transacation should be posted for reverse and unposted")
            End If
            Dim qry As String = ""
            qry = "update TSPL_DAILY_ELECTRICAL_ENTRY_HEAD set Posted=0,Posted_Date=null where Document_No='" + StrDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsDailyElectricalEntrySlotDetails
#Region "Variables"
    Public Document_No As String = String.Empty
    Public SNo As Integer
    Public Slot_Code As String = String.Empty
    Public Slot_Unit As Double = 0
#End Region

    Public Shared Function saveData(ByVal arrObj As List(Of clsDailyElectricalEntrySlotDetails), ByVal strDocumentNo As String, ByVal trans As SqlTransaction) As Boolean

        If arrObj IsNot Nothing Then
            For Each obj As clsDailyElectricalEntrySlotDetails In arrObj
                Dim coll As Hashtable = New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", strDocumentNo)
                clsCommon.AddColumnsForChange(coll, "SNo", obj.SNo)
                clsCommon.AddColumnsForChange(coll, "Slot_Code", obj.Slot_Code)
                clsCommon.AddColumnsForChange(coll, "Slot_Unit", obj.Slot_Unit)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DAILY_ELECTRICAL_ENTRY_EB_UNIT_DETAILS", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function


    Public Shared Function GetData(ByVal strDocumentNo As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsDailyElectricalEntrySlotDetails)
        Dim arr As List(Of clsDailyElectricalEntrySlotDetails) = Nothing
        Try
            Dim obj As clsDailyElectricalEntrySlotDetails = Nothing
            Dim qry As String = "select TSPL_DAILY_ELECTRICAL_ENTRY_EB_UNIT_DETAILS.* from TSPL_DAILY_ELECTRICAL_ENTRY_EB_UNIT_DETAILS  where Document_No='" & strDocumentNo & "' order by Document_No , SNo asc "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arr = New List(Of clsDailyElectricalEntrySlotDetails)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New clsDailyElectricalEntrySlotDetails()
                    obj.Document_No = clsCommon.myCstr(dt.Rows(i)("Document_No"))
                    obj.SNo = clsCommon.myCdbl(dt.Rows(i)("SNo"))
                    obj.Slot_Code = clsCommon.myCstr(dt.Rows(i)("Slot_Code"))
                    obj.Slot_Unit = clsCommon.myCdbl(dt.Rows(i)("Slot_Unit"))
                    arr.Add(obj)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return arr
    End Function
   
End Class

Public Class clsDailyElectricalEntryDGDetails
    Public Document_No As String = String.Empty
    Public SNo As Integer
    Public DG_Code As String = String.Empty
    Public DG_Unit As Double = 0
    Public DG_Consumption As Double = 0
    Public DG_Runing_Hours As Double = 0

    Public Shared Function saveData(ByVal arrObj As List(Of clsDailyElectricalEntryDGDetails), ByVal strDocumentNo As String, ByVal trans As SqlTransaction) As Boolean
        If arrObj IsNot Nothing Then
            For Each obj As clsDailyElectricalEntryDGDetails In arrObj
                Dim coll As Hashtable = New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", strDocumentNo)
                clsCommon.AddColumnsForChange(coll, "SNo", obj.SNo)
                clsCommon.AddColumnsForChange(coll, "DG_Code", obj.DG_Code)
                clsCommon.AddColumnsForChange(coll, "DG_Unit", obj.DG_Unit)
                clsCommon.AddColumnsForChange(coll, "DG_Consumption", obj.DG_Consumption)
                clsCommon.AddColumnsForChange(coll, "DG_Runing_Hours", obj.DG_Runing_Hours)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DAILY_ELECTRICAL_ENTRY_DG_DETAILS", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsDailyElectricalEntryDGDetails)
        Dim arr As List(Of clsDailyElectricalEntryDGDetails) = Nothing
        Try
            Dim obj As clsDailyElectricalEntryDGDetails = Nothing
            Dim qry As String = "select TSPL_DAILY_ELECTRICAL_ENTRY_DG_DETAILS.* from TSPL_DAILY_ELECTRICAL_ENTRY_DG_DETAILS  where Document_No='" & strDocumentNo & "' order by Document_No , SNo asc "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arr = New List(Of clsDailyElectricalEntryDGDetails)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New clsDailyElectricalEntryDGDetails()
                    obj.Document_No = clsCommon.myCstr(dt.Rows(i)("Document_No"))
                    obj.SNo = clsCommon.myCdbl(dt.Rows(i)("SNo"))
                    obj.DG_Code = clsCommon.myCstr(dt.Rows(i)("DG_Code"))
                    obj.DG_Unit = clsCommon.myCdbl(dt.Rows(i)("DG_Unit"))
                    obj.DG_Consumption = clsCommon.myCdbl(dt.Rows(i)("DG_Consumption"))
                    obj.DG_Runing_Hours = clsCommon.myCdbl(dt.Rows(i)("DG_Runing_Hours"))
                    arr.Add(obj)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return arr
    End Function

End Class



 