Imports System.Data.SqlClient
Public Class clsMCCWeighment
#Region "Variables"
    Public Document_No As String = Nothing
    Public Document_Date As Date
    Public Gate_Entry_No As String = Nothing
    Public Location_Code As String = Nothing

    Public Location_Name As String = Nothing ''Not a table column
    Public Tanker_No As String = Nothing ''Not a table column
    Public Transporter_Code As String = Nothing ''Not a table column
    Public Transporter_name As String = Nothing ''Not a table column

    Public Tare_Weight As Double = 0
    Public Gross_Weight As Double = 0
    Public Net_Weight As Double = 0
    Public Status_Tare_Weight As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Status_Gross_Weight As ERPTransactionStatus = ERPTransactionStatus.Pending
#End Region

    Public Shared Function SaveData(ByVal obj As clsMCCWeighment, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim coll As New Hashtable()
            'Dim qry As String = "select 1 from tspl_tanker_master where isGateOut=0 and Tanker_No='" + obj.Tanker_No + "'"
            'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            '    Throw New Exception("Tanker's status is alredy Entered")
            'End If

            Dim dtCurrent As DateTime = clsCommon.GETSERVERDATE(trans)

            coll = New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Gate_Entry_No", obj.Gate_Entry_No)
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "Tare_Weight", obj.Tare_Weight)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(dtCurrent, "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.MCCWeighment, "", obj.Location_Code)
                If clsCommon.myLen(obj.Document_No) <= 0 Then
                    Throw New Exception("Error in document genereation")
                End If
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(dtCurrent, "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_WEIGHMENT", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_WEIGHMENT", OMInsertOrUpdate.Update, "Document_No='" + obj.Document_No + "'", trans)
            End If
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveGrossWeightData(ByVal DocNo As String, ByVal GrossWeight As Double, ByVal NetWeight As Double) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim dtCurrent As DateTime = clsCommon.GETSERVERDATE(trans)
            Dim coll As New Hashtable()
            coll = New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Gross_Weight", GrossWeight)
            clsCommon.AddColumnsForChange(coll, "Net_Weight", NetWeight)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(dtCurrent, "dd/MMM/yyyy hh:mm tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_WEIGHMENT", OMInsertOrUpdate.Update, "Document_No='" + DocNo + "'", trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal strPermitLocation As String, ByVal NavType As NavigatorType) As clsMCCWeighment
        Dim obj As clsMCCWeighment = Nothing
        Dim Arr As List(Of clsMCCWeighment) = Nothing
        Dim qry As String = "select TSPL_MCC_WEIGHMENT.* ,TSPL_MCC_GATE_ENTRY.Transporter_Code,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_LOCATION_MASTER.Location_Desc,TSPL_MCC_GATE_ENTRY.Tanker_No " + Environment.NewLine +
        " from TSPL_MCC_WEIGHMENT left outer join TSPL_MCC_GATE_ENTRY on TSPL_MCC_GATE_ENTRY.Document_No=TSPL_MCC_WEIGHMENT.Gate_Entry_No  left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_MCC_WEIGHMENT.Location_Code left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.vendor_code=TSPL_MCC_GATE_ENTRY.Transporter_Code where 2=2  "
        If clsCommon.myLen(strPermitLocation) > 0 Then
            qry += " and TSPL_MCC_WEIGHMENT.Location_Code in (" + strPermitLocation + ")"
        End If
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_MCC_WEIGHMENT.Document_No = (select MIN(Document_No) from TSPL_MCC_WEIGHMENT WHERE 1=1 " + whrclas + " and TSPL_MCC_WEIGHMENT.Location_Code in (" + strPermitLocation + ") )"
            Case NavigatorType.Last
                qry += " and TSPL_MCC_WEIGHMENT.Document_No = (select Max(Document_No) from TSPL_MCC_WEIGHMENT WHERE 1=1 " + whrclas + " and TSPL_MCC_WEIGHMENT.Location_Code in (" + strPermitLocation + ") )"
            Case NavigatorType.Current
                qry += " and TSPL_MCC_WEIGHMENT.Document_No = '" + strCode + "' "
            Case NavigatorType.Next
                qry += " and TSPL_MCC_WEIGHMENT.Document_No = (select Min(Document_No) from TSPL_MCC_WEIGHMENT where Document_No > '" + strCode + "' " + whrclas + " and TSPL_MCC_WEIGHMENT.Location_Code in (" + strPermitLocation + ") )"
            Case NavigatorType.Previous
                qry += " and TSPL_MCC_WEIGHMENT.Document_No = (select Max(Document_No) from TSPL_MCC_WEIGHMENT where Document_No < '" + strCode + "' " + whrclas + " and TSPL_MCC_WEIGHMENT.Location_Code in (" + strPermitLocation + ") )"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsMCCWeighment()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Location_Name = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
            obj.Gate_Entry_No = clsCommon.myCstr(dt.Rows(0)("Gate_Entry_No"))
            obj.Tare_Weight = clsCommon.myCdbl(dt.Rows(0)("Tare_Weight"))
            obj.Gross_Weight = clsCommon.myCdbl(dt.Rows(0)("Gross_Weight"))
            obj.Net_Weight = clsCommon.myCdbl(dt.Rows(0)("Net_Weight"))
            obj.Status_Tare_Weight = IIf(clsCommon.myCdbl(dt.Rows(0)("Status_Tare_Weight")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            obj.Status_Gross_Weight = IIf(clsCommon.myCdbl(dt.Rows(0)("Status_Gross_Weight")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            obj.Tanker_No = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
            obj.Transporter_Code = clsCommon.myCstr(dt.Rows(0)("Transporter_Code"))
            obj.Transporter_name = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
        End If
        Return obj
    End Function

    Public Shared Function DeleteData(ByVal strDocNo As String, ByVal strPermitLocation As String) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Document No not found to Delete")
            End If
            Dim obj As clsMCCWeighment = clsMCCWeighment.GetData(strDocNo, strPermitLocation, NavigatorType.Current)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Document No not found to Delete")
            End If
            If obj.Status_Tare_Weight = ERPTransactionStatus.Approved Then
                Throw New Exception("Tare weight is posted")
            End If
            If obj.Status_Gross_Weight = ERPTransactionStatus.Approved Then
                Throw New Exception("Gross weight is posted")
            End If
            Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try
                Dim qry As String = "delete from TSPL_MCC_WEIGHMENT where Document_No='" + strDocNo + "'"
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

    Public Shared Function PostData(ByVal strDocNo As String, ByVal strPermitLocation As String) As Boolean
        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("No Data foundt to post")
            End If
            Dim obj As clsMCCWeighment = clsMCCWeighment.GetData(strDocNo, strPermitLocation, NavigatorType.Current)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            

            If obj.Status_Gross_Weight = ERPTransactionStatus.Approved Then
                Throw New Exception("Already posted transaction")
            ElseIf obj.Status_Tare_Weight = ERPTransactionStatus.Approved Then
                If obj.Net_Weight <= 0 Then
                    Throw New Exception("Net Weight cant be zero")
                End If
                ''Approve Gross Weigght
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Post_Date_Gross_Weight", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Post_By_Gross_Weight", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Status_Gross_Weight", 1)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_WEIGHMENT", OMInsertOrUpdate.Update, "Document_No='" + strDocNo + "'")
                
            Else
                ''Approve Tare Weight
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Post_Date_Tare_Weight", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Post_By_Tare_Weight", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Status_Tare_Weight", 1)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_WEIGHMENT", OMInsertOrUpdate.Update, "Document_No='" + strDocNo + "'")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class
