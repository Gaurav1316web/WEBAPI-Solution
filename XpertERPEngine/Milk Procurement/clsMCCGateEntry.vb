Imports System.Data.SqlClient

Public Class clsMCCGateEntry
#Region "Variables"
    Public Document_No As String = Nothing
    Public Document_Date As Date
    Public Location_Code As String = Nothing
    Public Location_Name As String = Nothing ''Not a table column
    Public Tanker_No As String = Nothing
    Public Transporter_Code As String = Nothing
    Public Transporter_name As String = Nothing ''Not a table column
    Public Remarks As String = Nothing
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
#End Region

    Public Shared Function SaveData(ByVal obj As clsMCCGateEntry, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim coll As New Hashtable()
            ''tanker Work
            Dim qry As String = "select Tanker_No from TSPL_MCC_GATE_ENTRY where document_No='" + obj.Document_No + "'"
            Dim strTankerNoOld As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            If clsCommon.myLen(strTankerNoOld) > 0 Then
                coll = New Hashtable()
                clsCommon.AddColumnsForChange(coll, "isGateOut", 1)
                clsCommonFunctionality.UpdateDataTable(coll, "tspl_tanker_master", OMInsertOrUpdate.Update, "Tanker_No='" + strTankerNoOld + "'", trans)
            End If

            qry = "select 1 from tspl_tanker_master where isGateOut=0 and Tanker_No='" + obj.Tanker_No + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Throw New Exception("Tanker's status is alredy Entered")
            End If
            ''Gate in tanker no
            coll = New Hashtable()
            clsCommon.AddColumnsForChange(coll, "isGateOut", 0)
            clsCommonFunctionality.UpdateDataTable(coll, "tspl_tanker_master", OMInsertOrUpdate.Update, "Tanker_No='" + obj.Tanker_No + "'", trans)
            ''End of Gate in tanker no
            ''End of tanker Work

            Dim dtCurrent As DateTime = clsCommon.GETSERVERDATE(trans)
            coll = New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "Tanker_No", obj.Tanker_No)
            clsCommon.AddColumnsForChange(coll, "Transporter_Code", obj.Transporter_Code)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(dtCurrent, "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.MCCGateEntry, "", obj.Location_Code)
                If clsCommon.myLen(obj.Document_No) <= 0 Then
                    Throw New Exception("Error in document genereation")
                End If
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(dtCurrent, "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_GATE_ENTRY", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_GATE_ENTRY", OMInsertOrUpdate.Update, "Document_No='" + obj.Document_No + "'", trans)
            End If


            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

  
    Public Shared Function GetData(ByVal strCode As String, ByVal strPermitLocation As String, ByVal NavType As NavigatorType) As clsMCCGateEntry
        Dim obj As clsMCCGateEntry = Nothing
        Dim Arr As List(Of clsMCCGateEntry) = Nothing
        Dim qry As String = "select TSPL_MCC_GATE_ENTRY.*,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_LOCATION_MASTER.Location_Desc from TSPL_MCC_GATE_ENTRY left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_MCC_GATE_ENTRY.Location_Code left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.vendor_code=TSPL_MCC_GATE_ENTRY.Transporter_Code where 2=2  "
        If clsCommon.myLen(strPermitLocation) > 0 Then
            qry += " and TSPL_MCC_GATE_ENTRY.Location_Code in (" + strPermitLocation + ")"
        End If
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_MCC_GATE_ENTRY.Document_No = (select MIN(Document_No) from TSPL_MCC_GATE_ENTRY WHERE 1=1 " + whrclas + " and TSPL_MCC_GATE_ENTRY.Location_Code in (" + strPermitLocation + ") )"
            Case NavigatorType.Last
                qry += " and TSPL_MCC_GATE_ENTRY.Document_No = (select Max(Document_No) from TSPL_MCC_GATE_ENTRY WHERE 1=1 " + whrclas + " and TSPL_MCC_GATE_ENTRY.Location_Code in (" + strPermitLocation + ") )"
            Case NavigatorType.Current
                qry += " and TSPL_MCC_GATE_ENTRY.Document_No = '" + strCode + "' "
            Case NavigatorType.Next
                qry += " and TSPL_MCC_GATE_ENTRY.Document_No = (select Min(Document_No) from TSPL_MCC_GATE_ENTRY where Document_No > '" + strCode + "' " + whrclas + " and TSPL_MCC_GATE_ENTRY.Location_Code in (" + strPermitLocation + ") )"
            Case NavigatorType.Previous
                qry += " and TSPL_MCC_GATE_ENTRY.Document_No = (select Max(Document_No) from TSPL_MCC_GATE_ENTRY where Document_No < '" + strCode + "' " + whrclas + " and TSPL_MCC_GATE_ENTRY.Location_Code in (" + strPermitLocation + ") )"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsMCCGateEntry()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Location_Name = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
            obj.Tanker_No = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
            obj.Transporter_Code = clsCommon.myCstr(dt.Rows(0)("Transporter_Code"))
            obj.Transporter_name = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
        End If
        Return obj
    End Function

    

    Public Shared Function DeleteData(ByVal strDocNo As String, ByVal strPermitLocation As String) As Boolean

        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Document No not found to Delete")
            End If
            Dim obj As clsMCCGateEntry = clsMCCGateEntry.GetData(strDocNo, strPermitLocation, NavigatorType.Current)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Document No not found to Delete")
            End If
            If obj.Status = ERPTransactionStatus.Approved Then
                Throw New Exception("Already posted transaction")
            End If
            Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "isGateOut", 1)
                clsCommonFunctionality.UpdateDataTable(coll, "tspl_tanker_master", OMInsertOrUpdate.Update, "Tanker_No='" + obj.Tanker_No + "'", tran)

                Dim qry As String = "delete from TSPL_MCC_GATE_ENTRY where Document_No='" + strDocNo + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, tran)
                tran.Commit()
            Catch ex As Exception
                tran.Rollback()
                Throw New Exception(ex.Message)
            End Try
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostData(ByVal FormId As String, ByVal strPermitLocation As String, ByVal strDocNo As String) As Boolean
        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("No Data foundt to post")
            End If
            Dim obj As clsMCCGateEntry = clsMCCGateEntry.GetData(strDocNo, strPermitLocation, NavigatorType.Current)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If obj.Status = ERPTransactionStatus.Approved Then
                Throw New Exception("Already posted transaction")
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Post_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Post_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Status", 1)
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_GATE_ENTRY", OMInsertOrUpdate.Update, "Document_No='" + obj.Document_No + "'")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
     
End Class

 
