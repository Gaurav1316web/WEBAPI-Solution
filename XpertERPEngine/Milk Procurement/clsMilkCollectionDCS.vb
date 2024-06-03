Imports common
Imports System.Data.SqlClient

Public Class clsMilkCollectionDCS
#Region "Variables"
    Public Document_No As String
    Public Document_Date As DateTime
    Public Description As String
    Public Slip_No As String
    'Public Against_Milk_Collection_MCC_Detail As Integer
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Posting_Date As DateTime? = Nothing
    Public Arr As List(Of clsMilkCollectionDCSDetail) = Nothing
    Public ArrMCC As List(Of clsMilkCollectionDCSMCCDetail) = Nothing
#End Region

    Public Function SaveData(ByVal obj As clsMilkCollectionDCS, ByVal isNewEntry As Boolean) As Boolean
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
    Public Function SaveData(ByVal obj As clsMilkCollectionDCS, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            'clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.MilkShiftUploader, obj.VLC_Code, obj.Document_Date, trans)
            'clsMCCPaymentCycleLockForScheduler.CheckForSchedulerLock(obj.VLC_Code, obj.Document_Date, trans)
            If isNewEntry = False Then
                HistoryUpdate(obj.Document_No, trans)
            End If
            Dim qry As String = "delete from TSPL_MILK_COLLECTION_DCS_DETAIL where Document_No='" + obj.Document_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_MILK_COLLECTION_DCS_MCC_DETAIL where Document_No='" + obj.Document_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Slip_No", obj.Slip_No)

            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.MilkCollectionDCS, "", "")
                If (clsCommon.myLen(obj.Document_No) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_COLLECTION_DCS", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_COLLECTION_DCS", OMInsertOrUpdate.Update, "TSPL_MILK_COLLECTION_DCS.Document_No='" + obj.Document_No + "'", trans)
            End If
            clsMilkCollectionDCSMCCDetail.SaveData(obj.Document_No, obj.ArrMCC, trans)
            clsMilkCollectionDCSDetail.SaveData(obj.Document_No, obj.Arr, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    Public Shared Function HistoryUpdate(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_MILK_COLLECTION_DCS", "Document_No", "TSPL_MILK_COLLECTION_DCS_DETAIL", "Document_No", "TSPL_MILK_COLLECTION_DCS_MCC_DETAIL", "Document_No", trans)
        Return True
    End Function

    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsMilkCollectionDCS
        Dim obj As clsMilkCollectionDCS = Nothing
        Dim qry As String = "SELECT TSPL_MILK_COLLECTION_DCS.* FROM TSPL_MILK_COLLECTION_DCS where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_MILK_COLLECTION_DCS.Document_No = (select MIN(Document_No) from TSPL_MILK_COLLECTION_DCS where 1=1  )"
            Case NavigatorType.Last
                qry += " and TSPL_MILK_COLLECTION_DCS.Document_No = (select Max(Document_No) from TSPL_MILK_COLLECTION_DCS where 1=1  )"
            Case NavigatorType.Next
                qry += " and TSPL_MILK_COLLECTION_DCS.Document_No = (select Min(Document_No) from TSPL_MILK_COLLECTION_DCS where Document_No>'" + strPONo + "'  )"
            Case NavigatorType.Previous
                qry += " and TSPL_MILK_COLLECTION_DCS.Document_No = (select Max(Document_No) from TSPL_MILK_COLLECTION_DCS where Document_No<'" + strPONo + "'  )"
            Case NavigatorType.Current
                qry += " and TSPL_MILK_COLLECTION_DCS.Document_No = '" + strPONo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsMilkCollectionDCS()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Slip_No = clsCommon.myCstr(dt.Rows(0)("Slip_No"))

            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            If dt.Rows(0)("Posted_Date") IsNot DBNull.Value Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posted_Date"))
            End If

            obj.Arr = clsMilkCollectionDCSDetail.GetData(obj.Document_No, "", trans)
            obj.ArrMCC = clsMilkCollectionDCSMCCDetail.GetData(obj.Document_No, "", trans)
        End If
        Return obj
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            'Dim dt As DataTable = clsDBFuncationality.GetDataTable("select TSPL_MILK_COLLECTION_DCS.Document_Date,TSPL_MILK_COLLECTION_DCS.VLC_Code from TSPL_MILK_COLLECTION_DCS where Document_No='" + strCode + "'", trans)
            'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            '    clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.MilkShiftUploader, clsCommon.myCstr(dt.Rows(0)("VLC_Code")), clsCommon.myCDate(dt.Rows(0)("Document_Date")), trans)
            'End If

            Dim obj As clsMilkCollectionDCS = clsMilkCollectionDCS.GetData(strCode, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Document No: " + strCode + " not found to Delete")
            End If

            If (obj.Status = ERPTransactionStatus.Approved OrElse obj.Status = ERPTransactionStatus.Posted) Then
                Throw New Exception("Already Posted on :" + obj.Posting_Date)
            End If
            HistoryUpdate(strCode, trans)

            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_MILK_COLLECTION_DCS_MCC_DETAIL where Document_No='" + obj.Document_No + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_MILK_COLLECTION_DCS_DETAIL where Document_No='" + strCode + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_MILK_COLLECTION_DCS where Document_No='" + strCode + "'", trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function PostData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean

        Try
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If
            Dim obj As clsMilkCollectionDCS = clsMilkCollectionDCS.GetData(strCode, NavigatorType.Current, trans)
            'clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.MilkShiftUploader, obj.VLC_Code, obj.Document_Date, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Document No: " + strCode + " not found to Post")
            End If
            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Posted on :" + obj.Posting_Date)
            End If
            'HistoryUpdate(strCode, trans)
            GenerateMilkShiftUploader(obj, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Status", 1)
            clsCommon.AddColumnsForChange(coll, "Posted_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Posted_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_COLLECTION_DCS", OMInsertOrUpdate.Update, "Document_No='" + obj.Document_No + "'", trans)
            'Throw New Exception("Balwinder Singh Premi")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Private Shared Function GenerateMilkShiftUploader(ByVal objDCS As clsMilkCollectionDCS, ByVal trans As SqlTransaction) As Boolean
        Dim objM As clsMilkShiftUploaderHead = Nothing
        Dim objE As clsMilkShiftUploaderHead = Nothing
        Dim objMilkPro As clsMilkProcurementUploaderHead = Nothing
        Dim objMilkProRej As clsMilkProcurementUploaderHead = Nothing
        For Each objDCSTr As clsMilkCollectionDCSDetail In objDCS.Arr
            Dim qry As String = "select max(MCC_Code) as MCC_Code,max(Route_Code) as Route_Code,max(Tanker_No) as Tanker_No,max(Late) as Late,sum(Qty) as Qty,sum(FATKG) as FATKG,sum(SNFKG) as SNFKG,max(Against_DCS_Multiple_Days_Merge) as Against_DCS_Multiple_Days_Merge,max(Against_DCS_Multiple_Days) as Against_DCS_Multiple_Days from (select TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code,TSPL_MILK_COLLECTION_MCC.Route_Code,TSPL_MILK_COLLECTION_MCC.Tanker_No,TSPL_MILK_COLLECTION_MCC.Late,TSPL_MILK_COLLECTION_MCC_DETAIL.Qty,TSPL_MILK_COLLECTION_MCC_DETAIL.FATKG,TSPL_MILK_COLLECTION_MCC_DETAIL.SNFKG,TSPL_MILK_COLLECTION_MCC.Against_DCS_Multiple_Days_Merge,TSPL_MILK_COLLECTION_MCC.Against_DCS_Multiple_Days from TSPL_MILK_COLLECTION_MCC_DETAIL 
left outer join TSPL_MILK_COLLECTION_MCC on TSPL_MILK_COLLECTION_MCC.Document_No=TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No 
            where 2=2   "
            If objDCS.ArrMCC IsNot Nothing AndAlso objDCS.ArrMCC.Count > 0 Then
                qry += " and TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id in ("
                For ii As Integer = 0 To objDCS.ArrMCC.Count - 1
                    If ii > 0 Then
                        qry += ","
                    End If
                    qry += clsCommon.myCstr(objDCS.ArrMCC(ii).Against_Milk_Collection_MCC_Detail)
                Next
                qry += ")"
            Else
                Throw New Exception("BCC Document no not found")
            End If
            qry += ")x"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim isAgainstMultipleDays As Boolean = False
                If clsCommon.myLen(dt.Rows(0)("Against_DCS_Multiple_Days")) > 0 OrElse clsCommon.myLen(dt.Rows(0)("Against_DCS_Multiple_Days_Merge")) > 0 Then
                    isAgainstMultipleDays = True
                End If
                qry = "select Document_No from TSPL_MILK_SHIFT_UPLOADER_HEAD where MCC_CODE='" + clsCommon.myCstr(dt.Rows(0)("MCC_Code")) + "' 
and SHIFT='" + objDCSTr.Shift + "' 
and convert(date, Shift_Date,103)=convert(date, '" + clsCommon.GetPrintDate(IIf((clsCommon.CompairString(objDCSTr.Shift, "M") = CompairStringResult.Equal OrElse isAgainstMultipleDays), objDCS.Document_Date, objDCS.Document_Date.AddDays(-1)), "dd/MMM/yyyy") + "',103)"
                Dim dtShit As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dtShit Is Nothing OrElse dtShit.Rows.Count <= 0 Then
                    Dim objtr As New clsMilkShiftUploaderDetail
                    objtr.VLC_Code = objDCSTr.VLC_Code
                    objtr.No_Of_Cans = 1
                    objtr.Milk_Weight = objDCSTr.Qty
                    objtr.FAT = objDCSTr.FAT
                    objtr.SNF = objDCSTr.SNF
                    objtr.Dock_Collection_Milk_Type = objDCSTr.Dock_Collection_Milk_Type
                    objtr.Dock_Collection_Milk_Type_Auto = False
                    objtr.Against_Milk_Collection_DCS_Detail = objDCSTr.PK_Id
                    If Not clsCommon.CompairString(objDCSTr.Milk_Type, "Good") = CompairStringResult.Equal Then
                        objtr.Reject_Type = objDCSTr.Milk_Type
                        objtr.Reject_Defaulter = "VSP"
                    End If
                    If clsCommon.CompairString(objDCSTr.Shift, "M") = CompairStringResult.Equal Then
                        If objM Is Nothing Then
                            objM = New clsMilkShiftUploaderHead()
                            objM.Shift_Date = objDCS.Document_Date
                            objM.Shift = objDCSTr.Shift
                            objM.MCC_Code = clsCommon.myCstr(dt.Rows(0)("MCC_Code"))
                            objM.Raj_Bulk_Route_Code = clsCommon.myCstr(dt.Rows(0)("Route_Code"))
                            objM.Raj_Truck_no = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
                            objM.Raj_Late = clsCommon.myCDecimal(dt.Rows(0)("Late"))
                            objM.Raj_Entered_Qty = clsCommon.myCDecimal(dt.Rows(0)("Qty"))
                            objM.Raj_Entered_FATKg = clsCommon.myCDecimal(dt.Rows(0)("FATKG"))
                            objM.Raj_Entered_SNFKg = clsCommon.myCDecimal(dt.Rows(0)("SNFKG"))
                            objM.Arr = New List(Of clsMilkShiftUploaderDetail)
                        End If
                        objtr.SNo = objM.Arr.Count + 1
                        objM.Arr.Add(objtr)
                    ElseIf clsCommon.CompairString(objDCSTr.Shift, "E") = CompairStringResult.Equal Then
                        If objE Is Nothing Then
                            objE = New clsMilkShiftUploaderHead()
                            If isAgainstMultipleDays Then
                                objE.Shift_Date = objDCS.Document_Date
                            Else
                                objE.Shift_Date = objDCS.Document_Date.AddDays(-1)
                            End If
                            objE.Shift = objDCSTr.Shift
                            objE.MCC_Code = clsCommon.myCstr(dt.Rows(0)("MCC_Code"))
                            objE.Raj_Bulk_Route_Code = clsCommon.myCstr(dt.Rows(0)("Route_Code"))
                            objE.Raj_Truck_no = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
                            objE.Raj_Late = clsCommon.myCDecimal(dt.Rows(0)("Late"))
                            objE.Raj_Entered_Qty = clsCommon.myCDecimal(dt.Rows(0)("Qty"))
                            objE.Raj_Entered_FATKg = clsCommon.myCDecimal(dt.Rows(0)("FATKG"))
                            objE.Raj_Entered_SNFKg = clsCommon.myCDecimal(dt.Rows(0)("SNFKG"))
                            objE.Arr = New List(Of clsMilkShiftUploaderDetail)
                        End If
                        objtr.SNo = objE.Arr.Count + 1
                        objE.Arr.Add(objtr)
                    End If
                Else
                    Dim objTrMilkPro As New clsMilkProcurementUploaderDetail()
                    If clsCommon.CompairString(objDCSTr.Shift, "M") = CompairStringResult.Equal Then
                        objTrMilkPro.Shift_Date = objDCS.Document_Date
                    Else
                        If isAgainstMultipleDays Then
                            objTrMilkPro.Shift_Date = objDCS.Document_Date
                        Else
                            objTrMilkPro.Shift_Date = objDCS.Document_Date.AddDays(-1)
                        End If
                    End If
                    objTrMilkPro.Shift = objDCSTr.Shift
                    objTrMilkPro.VLC_Code = objDCSTr.VLC_Code
                    objTrMilkPro.No_Of_Cans = 1
                    objTrMilkPro.Milk_Weight = objDCSTr.Qty
                    objTrMilkPro.FAT = objDCSTr.FAT
                    objTrMilkPro.SNF = objDCSTr.SNF
                    objTrMilkPro.Dock_Collection_Milk_Type = objDCSTr.Dock_Collection_Milk_Type
                    objTrMilkPro.Dock_Collection_Milk_Type_Auto = False
                    objTrMilkPro.Against_Milk_Collection_DCS_Detail = objDCSTr.PK_Id
                    If clsCommon.CompairString(objDCSTr.Milk_Type, "Good") = CompairStringResult.Equal Then
                        If objMilkPro Is Nothing Then
                            objMilkPro = New clsMilkProcurementUploaderHead()
                            objMilkPro.Document_Date = objDCS.Document_Date
                            objMilkPro.Description = objDCS.Description
                            objMilkPro.MCC_Code = clsCommon.myCstr(dt.Rows(0)("MCC_Code"))
                            objMilkPro.Dock_Code = Nothing
                            objMilkPro.Reject = False
                            objMilkPro.Arr = New List(Of clsMilkProcurementUploaderDetail)
                        End If
                        objTrMilkPro.SNo = objMilkPro.Arr.Count + 1
                        objMilkPro.Arr.Add(objTrMilkPro)
                    Else
                        If objMilkProRej Is Nothing Then
                            objMilkProRej = New clsMilkProcurementUploaderHead()
                            objMilkProRej.Document_Date = objDCS.Document_Date
                            objMilkProRej.Description = objDCS.Description
                            objMilkProRej.MCC_Code = clsCommon.myCstr(dt.Rows(0)("MCC_Code"))
                            objMilkProRej.Dock_Code = Nothing
                            objMilkProRej.Reject = True
                            objMilkProRej.Arr = New List(Of clsMilkProcurementUploaderDetail)
                        End If
                        objTrMilkPro.Reject_Defaulter = "VSP"
                        objTrMilkPro.Reject_Type = objDCSTr.Milk_Type
                        objTrMilkPro.SNo = objMilkProRej.Arr.Count + 1
                        objMilkProRej.Arr.Add(objTrMilkPro)
                    End If
                End If
            End If
        Next
        If objM IsNot Nothing AndAlso objM.Arr.Count > 0 Then
            objM.SaveData(objM, True, True, trans)
            clsMilkShiftUploaderHead.PostData(objM.Document_No, trans)
        End If
        If objE IsNot Nothing AndAlso objE.Arr.Count > 0 Then
            objE.SaveData(objE, True, True, trans)
            clsMilkShiftUploaderHead.PostData(objE.Document_No, trans)
        End If
        If objMilkPro IsNot Nothing AndAlso objMilkPro.Arr.Count > 0 Then
            objMilkPro.SaveData(objMilkPro, True, trans)
            clsMilkProcurementUploaderHead.PostData(objMilkPro.Document_No, trans)
        End If
        If objMilkProRej IsNot Nothing AndAlso objMilkProRej.Arr.Count > 0 Then
            objMilkProRej.SaveData(objMilkProRej, True, trans)
            clsMilkProcurementUploaderHead.PostData(objMilkProRej.Document_No, trans)
        End If
        Return True
    End Function

    Public Shared Function GetRouteDetails(ByVal Against_Milk_Collection_DCS_Detail As Integer, ByVal trans As SqlTransaction, ByVal settMilkCollectionPickBulkRoute As Boolean) As DataTable
        Return GetRouteDetails(Against_Milk_Collection_DCS_Detail, trans, settMilkCollectionPickBulkRoute, "")
    End Function
    Public Shared Function GetRouteDetails(ByVal Against_Milk_Collection_DCS_Detail As Integer, ByVal trans As SqlTransaction, ByVal settMilkCollectionPickBulkRoute As Boolean, ByVal strBulkRouteCode As String) As DataTable
        Dim qry As String = ""
        Dim dt As DataTable = Nothing
        If Against_Milk_Collection_DCS_Detail > 0 Then
            qry = "select TSPL_MILK_COLLECTION_MCC.Tanker_No,TSPL_TANKER_MASTER.Tanker_Transporter_Code,TSPL_MILK_COLLECTION_MCC.Route_Code,TSPL_MILK_COLLECTION_MCC.Vehicle_No from TSPL_MILK_COLLECTION_DCS_DETAIL
left outer join TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
left outer join TSPL_MILK_COLLECTION_DCS_MCC_DETAIL on TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No=TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
left outer join TSPL_MILK_COLLECTION_MCC_DETAIL on TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id=TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail
left outer join TSPL_MILK_COLLECTION_MCC on TSPL_MILK_COLLECTION_MCC.Document_No=TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No
left outer join TSPL_TANKER_MASTER on TSPL_TANKER_MASTER.Tanker_No=TSPL_MILK_COLLECTION_MCC.Tanker_No
where TSPL_MILK_COLLECTION_DCS_DETAIL.PK_Id='" + clsCommon.myCstr(Against_Milk_Collection_DCS_Detail) + "'"
            dt = clsDBFuncationality.GetDataTable(qry, trans)
        ElseIf settMilkCollectionPickBulkRoute Then
            qry = "SELECT TSPL_BULK_ROUTE_MASTER.Tanker_No as Tanker_No,TSPL_TANKER_MASTER.Tanker_Transporter_Code,TSPL_BULK_ROUTE_MASTER.ROUTE_NO AS Route_Code,TSPL_TANKER_MASTER.Tanker_No AS Vehicle_No FROM TSPL_BULK_ROUTE_MASTER 
left outer join TSPL_TANKER_MASTER on TSPL_TANKER_MASTER.Tanker_No=TSPL_BULK_ROUTE_MASTER.Tanker_No
where 2=2"
            If clsCommon.myLen(strBulkRouteCode) > 0 Then
                qry += " and TSPL_BULK_ROUTE_MASTER.ROUTE_NO='" + strBulkRouteCode + "'"
            Else
                qry += " and TSPL_BULK_ROUTE_MASTER.IsDefault=1"
            End If

            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Please set Default In Bulk Route Master")
            End If
        End If
        Return dt
    End Function

    Public Shared Function AddMissing(ByVal strDocNo As String, ByVal Arr As List(Of clsMilkCollectionDCSDetail)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            clsMilkCollectionDCSDetail.SaveData(strDocNo, Arr, trans, -1, True)

            Dim obj As clsMilkCollectionDCS = clsMilkCollectionDCS.GetData(strDocNo, NavigatorType.Current, trans)
            obj.Arr = Nothing
            Dim strWhr As String = "not exists (select 1 from TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL where TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Against_Milk_Collection_DCS_Detail=TSPL_MILK_COLLECTION_DCS_DETAIL.PK_Id)  
and not exists (select 1 from TSPL_MILK_SHIFT_UPLOADER_DETAIL where TSPL_MILK_SHIFT_UPLOADER_DETAIL.Against_Milk_Collection_DCS_Detail=TSPL_MILK_COLLECTION_DCS_DETAIL.PK_Id)"
            obj.Arr = clsMilkCollectionDCSDetail.GetData(obj.Document_No, strWhr, trans)
            GenerateMilkShiftUploader(obj, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetBaseQueryFATSNFGainLoss(ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal arrMCC As ArrayList) As String
        Dim BaseQry As String = " select xxx.MCC_Code,xxx.MCC_NAME,xxx.Document_Date,xxx.MCCQty,xxx.MCCFATKG,xxx.MCCSNFKG,xxx.DCSQty,xxx.DCSFATKG,xxx.DCSSNFKG,xxx.DiffFATKG,xxx.DiffSNFKG
,cast((case when xxx.DiffFATKG<0 then TSPL_OWN_BMC_GAIN_LOSS_RATE.Loss_FAT_Rate else TSPL_OWN_BMC_GAIN_LOSS_RATE.Gain_FAT_Rate end)*xxx.DiffFATKG as decimal(18,2)) as FatAmt 
,cast((case when xxx.DiffSNFKG<0 then TSPL_OWN_BMC_GAIN_LOSS_RATE.Loss_SNF_Rate else TSPL_OWN_BMC_GAIN_LOSS_RATE.Gain_SNF_Rate end)*xxx.DiffSNFKG as decimal(18,2)) as SNFAmt 
,cast((((case when xxx.DiffFATKG<0 then TSPL_OWN_BMC_GAIN_LOSS_RATE.Loss_FAT_Rate else TSPL_OWN_BMC_GAIN_LOSS_RATE.Gain_FAT_Rate end)*xxx.DiffFATKG) +((case when xxx.DiffSNFKG<0 then TSPL_OWN_BMC_GAIN_LOSS_RATE.Loss_SNF_Rate else TSPL_OWN_BMC_GAIN_LOSS_RATE.Gain_SNF_Rate end)*xxx.DiffSNFKG))as decimal(18,2)) as Amt ,(FindCode) as FindCode
from (
select  PK_Id,max(MCC_Code) as MCC_Code,max(MCC_NAME) as MCC_NAME,max(Document_Date) as Document_Date,sum(MCCQty) as MCCQty,sum(MCCFATKG) as MCCFATKG,sum(MCCSNFKG) as MCCSNFKG,sum(DCSQty) as DCSQty,sum(DCSFATKG) as DCSFATKG,sum(DCSSNFKG) as DCSSNFKG,-1*(sum(DCSFATKG) - max(MCCFATKG)) as DiffFATKG,-1*(sum(DCSSNFKG) - max(MCCSNFKG)) as DiffSNFKG,(select top 1 case when TSPL_OWN_BMC_GAIN_LOSS_RATE.Inactive=0 then TSPL_OWN_BMC_GAIN_LOSS_RATE.Code else '' end as  FindCode 
from TSPL_OWN_BMC_GAIN_LOSS_RATE where max(Document_Date)>=TSPL_OWN_BMC_GAIN_LOSS_RATE.Start_Date  and (2= case when TSPL_OWN_BMC_GAIN_LOSS_RATE.End_Date is null then 2 else case when max(Document_Date)<= TSPL_OWN_BMC_GAIN_LOSS_RATE.End_Date then 2 else 3 end end)  and TSPL_OWN_BMC_GAIN_LOSS_RATE.Posted=1 order by TSPL_OWN_BMC_GAIN_LOSS_RATE.Start_Date desc,TSPL_OWN_BMC_GAIN_LOSS_RATE.Code desc) as  FindCode
from (

select TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id,TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code,TSPL_MCC_MASTER.MCC_NAME,convert(date,TSPL_MILK_COLLECTION_MCC.Document_Date,103) as Document_Date,TSPL_MILK_COLLECTION_MCC_DETAIL.Qty as MCCQty,TSPL_MILK_COLLECTION_MCC_DETAIL.FATKG as MCCFATKG,TSPL_MILK_COLLECTION_MCC_DETAIL.FAT as MCCFAT,TSPL_MILK_COLLECTION_MCC_DETAIL.SNF as MCCSNF,TSPL_MILK_COLLECTION_MCC_DETAIL.SNFKG as MCCSNFKG
,0 as DCSQty ,0 as DCSFATKG ,0 as DCSSNFKG
from   TSPL_MILK_COLLECTION_MCC_DETAIL 
left outer join TSPL_MILK_COLLECTION_MCC on TSPL_MILK_COLLECTION_MCC.Document_No=TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No
left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code
where convert(date, TSPL_MILK_COLLECTION_MCC.Document_Date,103)>='" + clsCommon.GetPrintDate(FromDate, "dd/MMM/yyyy") + "' and convert(date, TSPL_MILK_COLLECTION_MCC.Document_Date,103)<='" + clsCommon.GetPrintDate(ToDate, "dd/MMM/yyyy") + "' "
        If arrMCC IsNot Nothing AndAlso arrMCC.Count > 0 Then
            BaseQry += " and TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code in (" + clsCommon.GetMulcallString(arrMCC) + ") "
        End If
        BaseQry += "  union all
select Tab.PK_Id,null as MCC_Code,null as MCC_NAME,null as Document_Date,0 as MCCQty,0 as MCCFATKG,0 as MCCFAT,0 as MCCSNF,0 as MCCSNFKG
,TSPL_MILK_COLLECTION_DCS_DETAIL.Qty as DCSQty ,TSPL_MILK_COLLECTION_DCS_DETAIL.FATKG as DCSFATKG ,TSPL_MILK_COLLECTION_DCS_DETAIL.SNFKG as DCSSNFKG
from   TSPL_MILK_COLLECTION_DCS_DETAIL 
left outer join TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No 
inner join (
select Document_No,min(PK_Id) as PK_Id from (
select TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id,TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No 
from TSPL_MILK_COLLECTION_MCC_DETAIL
left outer join TSPL_MILK_COLLECTION_MCC on TSPL_MILK_COLLECTION_MCC.Document_No=TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No
left outer join  TSPL_MILK_COLLECTION_DCS_MCC_DETAIL on TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail=TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id 
where convert(date, TSPL_MILK_COLLECTION_MCC.Document_Date,103)>='" + clsCommon.GetPrintDate(FromDate, "dd/MMM/yyyy") + "' and convert(date, TSPL_MILK_COLLECTION_MCC.Document_Date,103)<='" + clsCommon.GetPrintDate(ToDate, "dd/MMM/yyyy") + "' "
        If arrMCC IsNot Nothing AndAlso arrMCC.Count > 0 Then
            BaseQry += " and TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code in (" + clsCommon.GetMulcallString(arrMCC) + ") "
        End If
        BaseQry += " )xx group by xx.Document_No
)Tab on Tab.Document_No= TSPL_MILK_COLLECTION_DCS.Document_No
)X group by PK_Id  
)xxx 
left outer join TSPL_OWN_BMC_GAIN_LOSS_RATE on TSPL_OWN_BMC_GAIN_LOSS_RATE.Code=xxx.FindCode"
        Return BaseQry
    End Function
End Class

Public Class clsMilkCollectionDCSDetail
#Region "Variables"
    Public PK_Id As Integer
    Public Document_No As String
    Public SNo As Integer
    Public VLC_Uploader_Code As String ''Not a Table Column
    Public VLC_Code As String
    Public VLC_Name As String ''Not a Table Column
    Public Shift As String
    Public Milk_Type As String
    Public Dock_Collection_Milk_Type As String
    Public Qty As Decimal
    Public FAT As Decimal
    Public SNF As Decimal
    Public FATKG As Decimal
    Public SNFKG As Decimal
#End Region
    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsMilkCollectionDCSDetail), ByVal trans As SqlTransaction) As Boolean
        Return SaveData(strDocNo, Arr, trans, -1, False)
    End Function
    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsMilkCollectionDCSDetail), ByVal trans As SqlTransaction, ByVal intPKID As Integer, ByVal isMissingOnly As Boolean) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsMilkCollectionDCSDetail In Arr
                If obj.Qty > 0 Then
                    If obj.FAT <= 0 OrElse obj.SNF <= 0 Then
                        If clsMilkRejectType.GetApplicableOn(obj.Milk_Type, trans) <> 1 Then
                            Continue For
                        End If
                    End If
                    SaveDataTRData(strDocNo, obj, intPKID, trans)
                End If
            Next
            Dim SettAdjQty As Boolean = (clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.AdjustFATSNFINOwnVSP, clsFixedParameterCode.AdjustQtyINOwnVSP, trans)) = 1)
            If SettAdjQty Then
                Dim qry As String = "select * from (
select case when isnull(TSPL_VLC_MASTER_HEAD.isOwnBMC,0)=1 and TSPL_VLC_MASTER_HEAD.MCC=Tab.MCC_Code then 1 else 0 end as isOwnBMC 
from TSPL_MILK_COLLECTION_DCS_DETAIL
left outer join TSPL_MILK_COLLECTION_DCS on  TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_COLLECTION_DCS_DETAIL.VLC_Code
left outer join (select Document_No,max(MCC_Code) as MCC_Code from (select TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No,TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code from TSPL_MILK_COLLECTION_DCS_MCC_DETAIL inner join TSPL_MILK_COLLECTION_MCC_DETAIL on TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id=TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail )xx group by Document_No )Tab on Tab.Document_No= TSPL_MILK_COLLECTION_DCS.Document_No
where TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No='" + strDocNo + "' 
)x where isOwnBMC=1"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    qry = "(select Document_No,max(MCC_Code) as MCC_Code from (
select TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No,TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code from TSPL_MILK_COLLECTION_DCS_MCC_DETAIL inner join TSPL_MILK_COLLECTION_MCC_DETAIL on TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id=TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail 
where TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No='" + strDocNo + "' 
)xx group by Document_No )"
                    dt = clsDBFuncationality.GetDataTable(qry, trans)
                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        Throw New Exception("No mcc detail found")
                    End If
                    Dim obj As New clsMilkCollectionDCSDetail
                    obj.VLC_Code = clsfrmVLCMaster.OwnBMCCodeByMCC(clsCommon.myCstr(dt.Rows(0)("MCC_Code")), trans)
                    If clsCommon.myLen(obj.VLC_Code) <= 0 Then
                        Throw New Exception("Please defince own BMC for BMC [" + clsCommon.myCstr(dt.Rows(0)("MCC_Code")) + "]")
                    End If
                    obj.SNo = (Arr(Arr.Count - 1).SNo + 1)
                    obj.Shift = Arr(Arr.Count - 1).Shift
                    obj.Milk_Type = Arr(Arr.Count - 1).Milk_Type
                    obj.Dock_Collection_Milk_Type = Arr(Arr.Count - 1).Dock_Collection_Milk_Type
                    obj.Qty = 0
                    obj.FAT = 0
                    obj.SNF = 0
                    obj.FATKG = 0
                    obj.SNFKG = 0
                    SaveDataTRData(strDocNo, obj, 0, trans)
                End If
            End If

            If Not isMissingOnly Then
                If SettAdjQty OrElse (clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.AdjustFATSNFINOwnVSP, clsFixedParameterCode.AdjustFATSNFINOwnVSP, trans)) = 1) Then
                    Dim settSNFDecimalPlace As Integer = clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.SNFDecimalPlaces, clsFixedParameterCode.SNFDecimalPlaces, trans))
                    Dim qry As String = "select max(isOwnBMC) as isOwnBMC, 
sum(MCCQty) as MCCQty,sum(Qty) as TotQty,sum(Qty)-sum(MCCQty) as DiffQty,
sum(MCCFATKG) as MCCFATKG,sum(FATKG) as TotFATKG,sum(FATKG)-sum(MCCFATKG) as DiffFATKG,
sum(MCCSNFKG) as MCCSNFKG,sum(SNFKG) as TotSNFKG,sum(SNFKG)-sum(MCCSNFKG) as DiffSNFKG,
max(case when isOwnBMC=1 then VLC_Code else '' end ) as VLC_Code
from (
select case when isnull(TSPL_VLC_MASTER_HEAD.isOwnBMC,0)=1 and TSPL_VLC_MASTER_HEAD.MCC=Tab.MCC_Code then 1 else 0 end as isOwnBMC,0.00 as MCCQty,0.00 as MCCFATKG,0.00 as MCCSNFKG,TSPL_MILK_COLLECTION_DCS_DETAIL.Qty, TSPL_MILK_COLLECTION_DCS_DETAIL.FATKG, TSPL_MILK_COLLECTION_DCS_DETAIL.SNFKG,TSPL_MILK_COLLECTION_DCS_DETAIL.VLC_Code
from TSPL_MILK_COLLECTION_DCS_DETAIL
left outer join TSPL_MILK_COLLECTION_DCS on  TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_COLLECTION_DCS_DETAIL.VLC_Code
left outer join (select Document_No,max(MCC_Code) as MCC_Code from (select TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No,TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code from TSPL_MILK_COLLECTION_DCS_MCC_DETAIL inner join TSPL_MILK_COLLECTION_MCC_DETAIL on TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id=TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail )xx group by Document_No )Tab on Tab.Document_No= TSPL_MILK_COLLECTION_DCS.Document_No
where TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No='" + strDocNo + "' 
union all
select 0 as isOwnBMC,TSPL_MILK_COLLECTION_MCC_DETAIL.Qty as MCCQty, TSPL_MILK_COLLECTION_MCC_DETAIL.FATKG as MCCFATKG,TSPL_MILK_COLLECTION_MCC_DETAIL.SNFKG as MCCSNFKG,0.00 as Qty,0.00 as FATKG,0.00 as SNFKG,'' as VLC_Code 
from TSPL_MILK_COLLECTION_DCS_MCC_DETAIL  
left outer join TSPL_MILK_COLLECTION_MCC_DETAIL on TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id=TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail
where TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No='" + strDocNo + "' 
)x"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        If clsCommon.myCdbl(dt.Rows(0)("isOwnBMC")) = 1 Then
                            If (SettAdjQty AndAlso Math.Abs(clsCommon.myCdbl(dt.Rows(0)("DiffQty"))) > 0) OrElse Math.Abs(clsCommon.myCdbl(dt.Rows(0)("DiffFATKG"))) > 0 OrElse Math.Abs(clsCommon.myCdbl(dt.Rows(0)("DiffSNFKG"))) > 0 Then
                                qry = "select PK_Id,Qty,FATKG,SNFKG from TSPL_MILK_COLLECTION_DCS_DETAIL where Document_No='" + strDocNo + "'  and VLC_Code='" + clsCommon.myCstr(dt.Rows(0)("VLC_Code")) + "' order by Shift desc,FATKG,SNFKG"
                                Dim dtDetail As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                If dtDetail IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                    Dim coll As New Hashtable()
                                    Dim ii As Integer
                                    Dim Qty As Decimal = (clsCommon.myCDecimal(dtDetail.Rows(ii)("Qty")) - clsCommon.myCDecimal(dt.Rows(0)("DiffQty")))
                                    Dim FATKG As Decimal = 0
                                    Dim SNFKG As Decimal = 0
                                    Dim FAT As Decimal = 0
                                    Dim SNF As Decimal = 0
                                    If SettAdjQty AndAlso Qty > 0 Then
                                        clsCommon.AddColumnsForChange(coll, "Qty", Qty)
                                        FATKG = Math.Abs(clsCommon.myCDecimal(dtDetail.Rows(ii)("FATKG")) - clsCommon.myCDecimal(dt.Rows(0)("DiffFATKG")))
                                        SNFKG = Math.Abs(clsCommon.myCDecimal(dtDetail.Rows(ii)("SNFKG")) - clsCommon.myCDecimal(dt.Rows(0)("DiffSNFKG")))
                                        FAT = clsCommon.myRoundOFF((100 * FATKG) / Qty, 1, 6)
                                        SNF = clsCommon.myRoundOFF((100 * SNFKG) / Qty, settSNFDecimalPlace, 6)
                                        FATKG = ((Qty * FAT) / 100)
                                        SNFKG = ((Qty * SNF) / 100)
                                    Else
                                        Qty = clsCommon.myCDecimal(dtDetail.Rows(ii)("Qty"))
                                        If Qty > 0 Then
                                            FATKG = Math.Abs(clsCommon.myCDecimal(dtDetail.Rows(ii)("FATKG")) - clsCommon.myCDecimal(dt.Rows(0)("DiffFATKG")))
                                            SNFKG = Math.Abs(clsCommon.myCDecimal(dtDetail.Rows(ii)("SNFKG")) - clsCommon.myCDecimal(dt.Rows(0)("DiffSNFKG")))
                                            FAT = Math.Round((100 * FATKG) / Qty, 1, MidpointRounding.ToEven)
                                            SNF = Math.Round((100 * SNFKG) / Qty, settSNFDecimalPlace, MidpointRounding.ToEven)
                                        End If
                                    End If
                                    clsCommon.AddColumnsForChange(coll, "FAT", FAT)
                                    clsCommon.AddColumnsForChange(coll, "SNF", SNF)
                                    clsCommon.AddColumnsForChange(coll, "FATKG", FATKG)
                                    clsCommon.AddColumnsForChange(coll, "SNFKG", SNFKG)
                                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_COLLECTION_DCS_DETAIL", OMInsertOrUpdate.Update, "PK_Id='" + clsCommon.myCstr(dtDetail.Rows(ii)("PK_Id")) + "'", trans)
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        End If
        Return True
    End Function

    Private Shared Sub SaveDataTRData(strDocNo As String, obj As clsMilkCollectionDCSDetail, ByVal intPKID As Integer, trans As SqlTransaction)
        Dim coll As New Hashtable()
        clsCommon.AddColumnsForChange(coll, "Document_No", strDocNo)
        clsCommon.AddColumnsForChange(coll, "SNo", obj.SNo)
        clsCommon.AddColumnsForChange(coll, "VLC_Code", obj.VLC_Code)
        clsCommon.AddColumnsForChange(coll, "Shift", obj.Shift)
        If clsCommon.CompairString(obj.Milk_Type, "Good") = CompairStringResult.Equal OrElse clsCommon.myLen(obj.Milk_Type) <= 0 Then
            obj.Milk_Type = "Good" ''To Handle Case sensitivity
        End If
        clsCommon.AddColumnsForChange(coll, "Milk_Type", obj.Milk_Type)
        clsCommon.AddColumnsForChange(coll, "Dock_Collection_Milk_Type", obj.Dock_Collection_Milk_Type)
        clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
        clsCommon.AddColumnsForChange(coll, "FAT", obj.FAT)
        clsCommon.AddColumnsForChange(coll, "SNF", obj.SNF)
        clsCommon.AddColumnsForChange(coll, "FATKG", obj.FATKG)
        clsCommon.AddColumnsForChange(coll, "SNFKG", obj.SNFKG)
        If intPKID > 0 Then
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_COLLECTION_DCS_DETAIL", OMInsertOrUpdate.Update, "PK_Id=" + clsCommon.myCstr(intPKID) + "", trans)
        Else
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_COLLECTION_DCS_DETAIL", OMInsertOrUpdate.Insert, "", trans)
        End If
    End Sub

    Public Shared Function GetData(ByVal strPONo As String, ByVal strExtraWhrclas As String, ByVal trans As SqlTransaction) As List(Of clsMilkCollectionDCSDetail)
        Dim arr As List(Of clsMilkCollectionDCSDetail) = Nothing
        Dim qry As String = "SELECT TSPL_MILK_COLLECTION_DCS_DETAIL.*,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader  
FROM TSPL_MILK_COLLECTION_DCS_DETAIL 
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_COLLECTION_DCS_DETAIL.VLC_Code 
where  TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No='" + strPONo + "' "
        If clsCommon.myLen(strExtraWhrclas) > 0 Then
            qry += " and " + strExtraWhrclas
        End If
        qry += " ORDER BY TSPL_MILK_COLLECTION_DCS_DETAIL.SNO"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            arr = New List(Of clsMilkCollectionDCSDetail)
            Dim objTr As clsMilkCollectionDCSDetail
            For Each dr As DataRow In dt.Rows
                objTr = New clsMilkCollectionDCSDetail
                objTr.PK_Id = clsCommon.myCdbl(dr("PK_Id"))
                objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                objTr.SNo = clsCommon.myCdbl(dr("SNo"))
                objTr.Shift = clsCommon.myCstr(dr("Shift"))
                objTr.VLC_Uploader_Code = clsCommon.myCstr(dr("VLC_Code_VLC_Uploader"))
                objTr.VLC_Code = clsCommon.myCstr(dr("VLC_Code"))
                objTr.VLC_Name = clsCommon.myCstr(dr("VLC_Name"))
                objTr.Qty = clsCommon.myCdbl(dr("Qty"))
                objTr.FAT = clsCommon.myCdbl(dr("FAT"))
                objTr.SNF = clsCommon.myCdbl(dr("SNF"))
                objTr.FATKG = clsCommon.myCdbl(dr("FATKG"))
                objTr.SNFKG = clsCommon.myCdbl(dr("SNFKG"))
                objTr.Milk_Type = clsCommon.myCstr(dr("Milk_Type"))
                objTr.Dock_Collection_Milk_Type = clsCommon.myCstr(dr("Dock_Collection_Milk_Type"))
                arr.Add(objTr)
            Next
        End If
        Return arr
    End Function


End Class

Public Class clsMilkCollectionDCSMCCDetail
#Region "Variables"
    Public PK_Id As Integer
    Public Document_No As String
    Public Against_Milk_Collection_MCC_Detail As Integer

#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsMilkCollectionDCSMCCDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim settSNFDecimalPlace As Integer = clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.SNFDecimalPlaces, clsFixedParameterCode.SNFDecimalPlaces, trans))
            For Each obj As clsMilkCollectionDCSMCCDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Against_Milk_Collection_MCC_Detail", obj.Against_Milk_Collection_MCC_Detail)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_COLLECTION_DCS_MCC_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strPONo As String, ByVal strExtraWhrclas As String, ByVal trans As SqlTransaction) As List(Of clsMilkCollectionDCSMCCDetail)
        Dim arr As List(Of clsMilkCollectionDCSMCCDetail) = Nothing
        Dim qry As String = "SELECT TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.* 
FROM TSPL_MILK_COLLECTION_DCS_MCC_DETAIL 
where  TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No='" + strPONo + "' "
        If clsCommon.myLen(strExtraWhrclas) > 0 Then
            qry += " and " + strExtraWhrclas
        End If
        qry += " ORDER BY TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.PK_Id"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            arr = New List(Of clsMilkCollectionDCSMCCDetail)
            Dim objTr As clsMilkCollectionDCSMCCDetail
            For Each dr As DataRow In dt.Rows
                objTr = New clsMilkCollectionDCSMCCDetail
                objTr.PK_Id = clsCommon.myCdbl(dr("PK_Id"))
                objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                objTr.Against_Milk_Collection_MCC_Detail = clsCommon.myCDecimal(dr("Against_Milk_Collection_MCC_Detail"))
                arr.Add(objTr)
            Next
        End If
        Return arr
    End Function
End Class

