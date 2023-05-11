
'--------Created By Richa 16/07/2016 Against Ticket No 
Imports System.Data.SqlClient
Imports common
Public Class ClsGatePassTransfer
#Region "Variable"
    Public Document_No As String = Nothing
    Public Document_Date As Date
    Public Booking_Code As String = Nothing
    Public Vehicle_Code As String = Nothing
    Public Branch_Code As String = Nothing
    Public Location_Code As String = Nothing
    Public Vehicle_Name As String = Nothing
    Public Branch_Name As String = Nothing
    Public Location_Name As String = Nothing
    Public TotalQty As Double = 0
    Public Posted As Integer = 0
    Public Posting_Date As Date?
    Public Is_Create_Auto_Invoice As Integer = 0
    Public Created_Date As Date?
    Public Modified_Date As Date?
    Public arrGatePassTransferDetail As List(Of ClsGatePassTransferDetail) = Nothing

    Public No_of_Crate As Decimal = Nothing
    Public No_of_Jaali As Decimal = Nothing
    Public No_of_Box As Decimal = Nothing
#End Region
    Public Shared Function SaveData(ByVal obj As ClsGatePassTransfer, ByVal isNewEntry As Boolean) As Boolean
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
    Public Shared Function SaveData(ByVal obj As ClsGatePassTransfer, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = String.Empty
        Try
            'clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Bulk Sale", "Bulk Dispatch", obj.Location_Code, obj.Document_Date, trans)
            qry = "delete from TSPL_GATEPASS_TRANSFER_DETAIL where Document_No='" & obj.Document_No & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.GatePasstransfer, "", obj.Branch_Code)
            End If
            Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, trans)
            Dim coll As New Hashtable()
            If DateTime = "1" Then
                clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
            End If

            clsCommon.AddColumnsForChange(coll, "Booking_Code", obj.Booking_Code)
            clsCommon.AddColumnsForChange(coll, "Branch_Code", obj.Branch_Code)
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "Vehicle_Code", obj.Vehicle_Code)
            clsCommon.AddColumnsForChange(coll, "TotalQty", obj.TotalQty)

            clsCommon.AddColumnsForChange(coll, "No_of_Crate", obj.No_of_Crate)
            clsCommon.AddColumnsForChange(coll, "No_of_Jaali", obj.No_of_Jaali)
            clsCommon.AddColumnsForChange(coll, "No_of_Box", obj.No_of_Box)

            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If isNewEntry Then
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GATEPASS_TRANSFER_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GATEPASS_TRANSFER_HEAD", OMInsertOrUpdate.Update, "TSPL_GATEPASS_TRANSFER_HEAD.Document_No='" + obj.Document_No + "'", trans)
            End If
            ClsGatePassTransferDetail.saveData(obj.arrGatePassTransferDetail, obj.Document_No, trans)
           
        Catch err As Exception
            Throw New Exception(err.Message)
        Finally
            obj = Nothing
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal arrLoc As String, ByVal NavType As NavigatorType) As ClsGatePassTransfer
        Return GetData(strCode, arrLoc, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal arrLoc As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsGatePassTransfer
        Dim obj As ClsGatePassTransfer = Nothing
        Dim Arr As List(Of ClsGatePassTransfer) = Nothing
        Dim qry As String = "SELECT TSPL_GATEPASS_TRANSFER_HEAD.no_of_crate,TSPL_GATEPASS_TRANSFER_HEAD.no_of_jaali,TSPL_GATEPASS_TRANSFER_HEAD.no_of_box,TSPL_GATEPASS_TRANSFER_HEAD.TotalQty,TSPL_GATEPASS_TRANSFER_HEAD.Document_No,TSPL_GATEPASS_TRANSFER_HEAD.Document_Date,TSPL_GATEPASS_TRANSFER_HEAD.Booking_Code,TSPL_GATEPASS_TRANSFER_HEAD.Vehicle_Code,TSPL_VEHICLE_MASTER.Description as vehiclename,TSPL_GATEPASS_TRANSFER_HEAD.Location_Code,TSPL_LOCATION_MASTER.Location_Desc ,TSPL_GATEPASS_TRANSFER_HEAD.Branch_Code,BRANCHcODE.Location_Desc AS BRANCHNAME,TSPL_GATEPASS_TRANSFER_HEAD.Posted,TSPL_GATEPASS_TRANSFER_HEAD.Posting_Date,TSPL_GATEPASS_TRANSFER_HEAD.Modified_Date,TSPL_GATEPASS_TRANSFER_HEAD.Created_Date FROM TSPL_GATEPASS_TRANSFER_HEAD LEFT OUTER JOIN TSPL_VEHICLE_MASTER on TSPL_GATEPASS_TRANSFER_HEAD.Vehicle_Code=TSPL_VEHICLE_MASTER.Vehicle_Id lEFT oUTER JOIN TSPL_LOCATION_MASTER ON TSPL_GATEPASS_TRANSFER_HEAD.Location_Code=TSPL_LOCATION_MASTER.Location_Code lEFT oUTER JOIN TSPL_LOCATION_MASTER AS BRANCHcODE ON TSPL_GATEPASS_TRANSFER_HEAD.Branch_Code=BRANCHcODE.Location_Code WHERE 2=2"

        If clsCommon.myLen(arrLoc) > 0 Then
            qry += "  and TSPL_GATEPASS_TRANSFER_HEAD.Location_Code in (" + arrLoc + ") "
        End If
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_GATEPASS_TRANSFER_HEAD.Document_No = (select MIN(Document_No) from TSPL_GATEPASS_TRANSFER_HEAD WHERE 1=1 " + whrclas + " and TSPL_GATEPASS_TRANSFER_HEAD.Location_Code in (" + arrLoc + ") )"
            Case NavigatorType.Last
                qry += " and TSPL_GATEPASS_TRANSFER_HEAD.Document_No = (select Max(Document_No) from TSPL_GATEPASS_TRANSFER_HEAD WHERE 1=1 " + whrclas + " and TSPL_GATEPASS_TRANSFER_HEAD.Location_Code in (" + arrLoc + ") )"
            Case NavigatorType.Current
                qry += " and TSPL_GATEPASS_TRANSFER_HEAD.Document_No='" + strCode + "' "
            Case NavigatorType.Next
                qry += " and TSPL_GATEPASS_TRANSFER_HEAD.Document_No = (select Min(Document_No) from TSPL_GATEPASS_TRANSFER_HEAD where Document_No>'" + strCode + "' " + whrclas + " and TSPL_GATEPASS_TRANSFER_HEAD.Location_Code in (" + arrLoc + ") )"
            Case NavigatorType.Previous
                qry += " and TSPL_GATEPASS_TRANSFER_HEAD.Document_No = (select Max(Document_No) from TSPL_GATEPASS_TRANSFER_HEAD where Document_No<'" + strCode + "' " + whrclas + " and TSPL_GATEPASS_TRANSFER_HEAD.Location_Code in (" + arrLoc + ") )"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsGatePassTransfer()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Branch_Code = clsCommon.myCstr(dt.Rows(0)("Branch_Code"))
            obj.TotalQty = clsCommon.myCdbl(dt.Rows(0)("TotalQty"))
            obj.Booking_Code = clsCommon.myCstr(dt.Rows(0)("Booking_Code"))
            obj.Vehicle_Code = clsCommon.myCstr(dt.Rows(0)("Vehicle_Code"))
            obj.Location_Name = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
            obj.Branch_Name = clsCommon.myCstr(dt.Rows(0)("BRANCHNAME"))
            obj.Vehicle_Name = clsCommon.myCstr(dt.Rows(0)("vehiclename"))
            obj.Posted = clsCommon.myCdbl(dt.Rows(0)("Posted"))

            obj.No_of_Box = clsCommon.myCdbl(dt.Rows(0)("No_of_Box"))
            obj.No_of_Crate = clsCommon.myCdbl(dt.Rows(0)("No_of_Crate"))
            obj.No_of_Jaali = clsCommon.myCdbl(dt.Rows(0)("No_of_Jaali"))

           If dt.Rows(0)("Posting_Date") IsNot DBNull.Value Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            End If
            If dt.Rows(0)("Modified_Date") IsNot DBNull.Value Then
                obj.Modified_Date = clsCommon.myCDate(dt.Rows(0)("Modified_Date"))
            End If
            If dt.Rows(0)("Created_Date") IsNot DBNull.Value Then
                obj.Created_Date = clsCommon.myCDate(dt.Rows(0)("Created_Date"))
            End If

            obj.arrGatePassTransferDetail = ClsGatePassTransferDetail.getData(obj.Document_No, trans)
        End If
        Return obj
    End Function
    Public Shared Function DeleteData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Try
            Dim qry As String = ""
            qry = "delete from TSPL_GATEPASS_TRANSFER_DETAIL where Document_No='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_GATEPASS_TRANSFER_HEAD where Document_No='" + strDocNo + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try

        Return isSaved
    End Function

    Public Shared Function PostData(ByVal FormId As String, ByVal arrLoc As String, ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(FormId, arrLoc, strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal arrLoc As String, ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean

        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Gate Pass No not found to Post")
            End If
            Dim obj As ClsGatePassTransfer = ClsGatePassTransfer.GetData(strDocNo, arrLoc, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

            Dim qry = "Update TSPL_GATEPASS_TRANSFER_HEAD set Posted=1, " & _
            "Posting_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "' " & _
            " where Document_No='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function


End Class
Public Class ClsGatePassTransferDetail
    Public Document_No As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = String.Empty
    Public Unit_code As String = Nothing
    Public Qty As Double = 0
    Public Remarks As String = String.Empty
    Public Shared Function saveData(ByVal arrObj As List(Of ClsGatePassTransferDetail), ByVal strQCNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim issaved As Boolean = True
            Dim coll As Hashtable

            If arrObj IsNot Nothing Then
                For Each obj As ClsGatePassTransferDetail In arrObj
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Document_No", strQCNo)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Unit_code", obj.Unit_code)
                    clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                    clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GATEPASS_TRANSFER_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            arrObj = Nothing
        End Try
    End Function
    Public Shared Function getData(ByVal strQCNo As String, ByVal trans As SqlTransaction) As List(Of ClsGatePassTransferDetail)
        Try
            Dim arrObj As List(Of ClsGatePassTransferDetail) = Nothing
            Dim obj As ClsGatePassTransferDetail = Nothing
            Dim qry As String = "Select TSPL_GATEPASS_TRANSFER_DETAIL.Document_No,TSPL_GATEPASS_TRANSFER_DETAIL.Unit_code,TSPL_GATEPASS_TRANSFER_DETAIL.Item_Code,TSPL_GATEPASS_TRANSFER_DETAIL.Qty,TSPL_GATEPASS_TRANSFER_DETAIL.Remarks,TSPL_ITEM_MASTER.Item_Desc from TSPL_GATEPASS_TRANSFER_DETAIL Left Outer Join TSPL_ITEM_MASTER on TSPL_GATEPASS_TRANSFER_DETAIL.Item_Code =TSPL_ITEM_MASTER .Item_Code  where Document_No='" & strQCNo & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arrObj = New List(Of ClsGatePassTransferDetail)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New ClsGatePassTransferDetail()
                    obj.Document_No = clsCommon.myCstr(dt.Rows(i)("Document_No"))
                    obj.Item_Code = clsCommon.myCstr(dt.Rows(i)("Item_Code"))
                    obj.Item_Desc = clsCommon.myCstr(dt.Rows(i)("Item_Desc"))
                    obj.Unit_code = clsCommon.myCstr(dt.Rows(i)("Unit_code"))
                    obj.Qty = clsCommon.myCdbl(dt.Rows(i)("Qty"))
                    obj.Remarks = clsCommon.myCstr(dt.Rows(i)("Remarks"))
                    arrObj.Add(obj)
                Next
            End If
            Return arrObj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    
End Class
