Imports common
Imports System.Data.SqlClient

Public Class clsMilkProcurementUploaderHead
#Region "Variables"
    Public Document_No As String
    Public Document_Date As DateTime
    Public MCC_Code As String
    Public MCC_Name As String
    Public Dock_Code As String
    Public Dock_Name As String
    Public Description As String
    Public Arr As List(Of clsMilkProcurementUploaderDetail) = Nothing
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Posting_Date As DateTime? = Nothing
    Public Reject As Boolean = False
    Public Approve_Date As DateTime? = Nothing
#End Region

    Public Function SaveData(ByVal obj As clsMilkProcurementUploaderHead, ByVal isNewEntry As Boolean) As Boolean
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

    Public Function SaveData(ByVal obj As clsMilkProcurementUploaderHead, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.frmMilkReceipt, obj.MCC_Code, obj.Document_Date, trans)
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.frmMilkSample, obj.MCC_Code, obj.Document_Date, trans)

            Dim qry As String = "delete from TSPL_MILK_PROCUREMENT_UPLOADER_QC_PARAMETER_DETAIL where Document_No='" & obj.Document_No & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL where Document_No='" + obj.Document_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If clsCommon.myLen(obj.MCC_Code) <= 0 Then
                Throw New Exception("Please first select MCC")
            Else
                qry = "select Code from TSPL_DOCK_MASTER where MCC_Code='" + obj.MCC_Code + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    If clsCommon.myLen(obj.Dock_Code) <= 0 Then
                        Throw New Exception("Please first select Dock")
                    End If
                    Dim isfound As Boolean = False
                    For Each dr As DataRow In dt.Rows
                        If clsCommon.CompairString(clsCommon.myCstr(dr("Code")), obj.Dock_Code) = CompairStringResult.Equal Then
                            isfound = True
                            Exit For
                        End If
                    Next
                    If Not isfound Then
                        Throw New Exception("Dock [" + obj.Dock_Code + "] is not for MCC [" + obj.MCC_Code + "]")
                    End If
                Else
                    If clsCommon.myLen(obj.Dock_Code) > 0 Then
                        Throw New Exception("Dock [" + obj.Dock_Code + "] is not For MCC [" + obj.MCC_Code + "]")
                    End If
                End If
            End If

            If isNewEntry Then
                obj.Document_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select max(Document_No) from TSPL_MILK_PROCUREMENT_UPLOADER_HEAD where Document_No like '" + obj.MCC_Code + "%' ", trans))
                If clsCommon.myLen(obj.Document_No) <= 0 Then
                    obj.Document_No = obj.MCC_Code + "0000000000"
                End If
                obj.Document_No = clsCommon.incval(obj.Document_No)
            End If
            If (clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "MCC_Code", obj.MCC_Code)
            clsCommon.AddColumnsForChange(coll, "Dock_Code", obj.Dock_Code, True)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Reject", IIf(obj.Reject, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            '' update Sync Satatus
            clsCommon.AddColumnsForChange(coll, "SYNC_STATUS", 0)
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_PROCUREMENT_UPLOADER_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_PROCUREMENT_UPLOADER_HEAD", OMInsertOrUpdate.Update, "TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_No='" + obj.Document_No + "'", trans)
            End If
            clsMilkProcurementUploaderDetail.SaveData(obj.Document_No, obj.MCC_Code, obj.Arr, trans)

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_No, "TSPL_MILK_PROCUREMENT_UPLOADER_HEAD", "Document_No", "TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL", "Document_No", trans)

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsMilkProcurementUploaderHead
        Dim obj As clsMilkProcurementUploaderHead = Nothing
        Dim qry As String = "SELECT TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.*,TSPL_MCC_MASTER.MCC_NAME,TSPL_DOCK_MASTER.Description as Dock_Name " & _
        " FROM TSPL_MILK_PROCUREMENT_UPLOADER_HEAD left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.MCC_Code left outer join TSPL_DOCK_MASTER on TSPL_DOCK_MASTER.Code=TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Dock_Code  where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_No = (select MIN(Document_No) from TSPL_MILK_PROCUREMENT_UPLOADER_HEAD where 1=1  )"
            Case NavigatorType.Last
                qry += " and TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_No = (select Max(Document_No) from TSPL_MILK_PROCUREMENT_UPLOADER_HEAD where 1=1  )"
            Case NavigatorType.Next
                qry += " and TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_No = (select Min(Document_No) from TSPL_MILK_PROCUREMENT_UPLOADER_HEAD where Document_No>'" + strPONo + "'  )"
            Case NavigatorType.Previous
                qry += " and TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_No = (select Max(Document_No) from TSPL_MILK_PROCUREMENT_UPLOADER_HEAD where Document_No<'" + strPONo + "'  )"
            Case NavigatorType.Current
                qry += " and TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_No = '" + strPONo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsMilkProcurementUploaderHead()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.MCC_Code = clsCommon.myCstr(dt.Rows(0)("MCC_Code"))
            obj.MCC_Name = clsCommon.myCstr(dt.Rows(0)("MCC_NAME"))
            obj.Dock_Code = clsCommon.myCstr(dt.Rows(0)("Dock_Code"))
            obj.Dock_Name = clsCommon.myCstr(dt.Rows(0)("Dock_Name"))
            obj.Reject = IIf(clsCommon.myCdbl(dt.Rows(0)("Reject")) = 1, True, False)
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 2, ERPTransactionStatus.Posted, ERPTransactionStatus.Pending))
            obj.Arr = clsMilkProcurementUploaderDetail.GetData(obj.Document_No, "", trans)
        End If
        Return obj
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim obj As clsMilkProcurementUploaderHead = clsMilkProcurementUploaderHead.GetData(strCode, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Document No: " + strCode + " not found to Delete")
            End If

            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Posted on :" + obj.Posting_Date)
            End If
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_MILK_PROCUREMENT_UPLOADER_QC_PARAMETER_DETAIL where Document_No='" + strCode + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL where Document_No='" + strCode + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_MILK_PROCUREMENT_UPLOADER_HEAD where Document_No='" + strCode + "'", trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    'Public Shared Function DeleteAndCleanData(ByVal strCode As String) As Boolean
    '    If (clsCommon.myLen(strCode) <= 0) Then
    '        Throw New Exception("Document No not found to Delete")
    '    End If

    '    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
    '    Try
    '        Dim obj As clsMilkProcurementUploaderHead = clsMilkProcurementUploaderHead.GetData(strCode, NavigatorType.Current, trans)
    '        If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
    '            Throw New Exception("Document No: " + strCode + " not found to Delete")
    '        End If


    '        clsDBFuncationality.ExecuteNonQuery("update TSPL_MILK_RECEIPT_DETAIL set Against_Uploader_TR_No=null where Against_Uploader_TR_No in (select TR_No from TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL where Document_No='" + strCode + "')", trans)
    '        clsDBFuncationality.ExecuteNonQuery("delete from TSPL_MILK_PROCUREMENT_UPLOADER_QC_PARAMETER_DETAIL where Document_No='" + strCode + "'", trans)
    '        clsDBFuncationality.ExecuteNonQuery("delete from TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL where Document_No='" + strCode + "'", trans)
    '        clsDBFuncationality.ExecuteNonQuery("delete from TSPL_MILK_PROCUREMENT_UPLOADER_HEAD where Document_No='" + strCode + "'", trans)
    '        trans.Commit()
    '    Catch ex As Exception
    '        trans.Rollback()
    '        Throw New Exception(ex.Message)
    '    End Try
    '    Return True
    'End Function

    Public Shared Function PostDataForBatch(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If

            Dim obj As clsMilkProcurementUploaderHead = clsMilkProcurementUploaderHead.GetData(strCode, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Document No: " + strCode + " not found to Post")
            End If
            If (obj.Status = ERPTransactionStatus.Approved OrElse obj.Status = ERPTransactionStatus.Posted) Then
                Throw New Exception("Already Posted on :" + obj.Posting_Date)
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Status", 2)
            clsCommon.AddColumnsForChange(coll, "Approve_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Approve_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_PROCUREMENT_UPLOADER_HEAD", OMInsertOrUpdate.Update, "Document_No='" + obj.Document_No + "'", trans)
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

            Dim obj As clsMilkProcurementUploaderHead = clsMilkProcurementUploaderHead.GetData(strCode, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Document No: " + strCode + " not found to Post")
            End If
            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Posted on :" + obj.Posting_Date)
            End If
            If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                Dim MinDate As Date = obj.Arr(0).Shift_Date
                For Each objtr As clsMilkProcurementUploaderDetail In obj.Arr
                    If MinDate > objtr.Shift_Date Then
                        MinDate = objtr.Shift_Date
                    End If
                Next
                clsMCCPaymentCycleLockForScheduler.CheckForSchedulerLock(obj.MCC_Code, MinDate, trans)
            End If
            'If obj.Reject Then
            '    MilkRejectUploader(obj, trans)
            'Else
            '    MilkProcurementUploader(obj, trans)
            'End If

            MilkProcurementUploader(obj, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Status", 1)
            clsCommon.AddColumnsForChange(coll, "Posted_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Posted_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_PROCUREMENT_UPLOADER_HEAD", OMInsertOrUpdate.Update, "Document_No='" + obj.Document_No + "'", trans)
            'Throw New Exception("Balwinder Singh Premi")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function


    Public Shared Function RevereAndUnpost(ByVal strCode As String) As Boolean
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim obj As clsMilkProcurementUploaderHead = clsMilkProcurementUploaderHead.GetData(strCode, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Document No: " + strCode + " not found to Reverse and unpost")
            End If
            If obj.Status = 0 Then
                Throw New Exception("Document No: " + strCode + " should be posted for Reverse and unpost")
            End If
            'If obj.Reject Then
            '    Throw New Exception("Reverse and Unpost is not Reject")
            'End If
            Dim strSRN As String = "select TSPL_MILK_SRN_HEAD.DOC_CODE from TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL
inner join TSPL_MILK_SRN_HEAD  on TSPL_MILK_SRN_HEAD.Against_Uploader_TR_No=TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No  
where TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No='" + strCode + "'"

            Dim qry As String = "select TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  
from TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL
inner join TSPL_MILK_SRN_HEAD  on TSPL_MILK_SRN_HEAD.Against_Uploader_TR_No=TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No
inner join TSPL_MILK_PURCHASE_INVOICE_DETAIL on TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE=TSPL_MILK_SRN_HEAD.DOC_CODE
where  TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No='" + strCode + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Throw New Exception("Can't delete used in Milk Purchase invoice [" + clsCommon.myCstr(dt.Rows(0)("DOC_CODE")) + "]")
            End If

            qry = "delete from TSPL_INVENTORY_MOVEMENT_new where Source_Doc_No in ( " + strSRN + ")"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_JOURNAL_DETAILS  where Voucher_No in ( select Voucher_No from TSPL_JOURNAL_MASTER  where Source_Doc_No in (" + strSRN + " ))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_JOURNAL_MASTER  where Source_Doc_No in ( " + strSRN + ")"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_MILK_SRN_DETAIL where DOC_CODE in ( " + strSRN + ")"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_MILK_SRN_HEAD where DOC_CODE in (" + strSRN + ")"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Status", 0)
            clsCommon.AddColumnsForChange(coll, "Posted_By", Nothing, True)
            clsCommon.AddColumnsForChange(coll, "Posted_Date", Nothing, True)
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_PROCUREMENT_UPLOADER_HEAD", OMInsertOrUpdate.Update, "Document_No='" + obj.Document_No + "'", trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function MilkRejectUploader(ByVal obj As clsMilkProcurementUploaderHead, ByVal trans As SqlTransaction) As Boolean
        Dim dblRetrunPenaltyPerUnit As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RejectionReturnPenaltyPerUnit, clsFixedParameterCode.RejectionReturnPaneltyPerUnit, trans))
        Dim dblDrainPenaltyPerUnit As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RejectionDrainPenaltyPerUnit, clsFixedParameterCode.RejectionDrainPenaltyPerUnit, trans))
        Dim dblCOBPenaltyPerUnit As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RejectionCOBPenaltyPerUnit, clsFixedParameterCode.RejectionCOBPenaltyPerUnit, trans))

        Dim dblSNFDeductionPer As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SNFDeductionPercent, clsFixedParameterCode.SNFDeductionPercent, trans))
        Dim dblFATDeductionPer As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.FATDeductionPercent, clsFixedParameterCode.FATDeductionPercent, trans))
        Dim settMilkCollectionPickBulkRoute As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkCollectionPickBulkRoute, clsFixedParameterCode.MilkCollectionPickBulkRoute, trans)) = 1)
        Dim objMilkRejectHeadMor As clsMilkRejectHead = New clsMilkRejectHead
        objMilkRejectHeadMor.DOC_DATE = obj.Document_Date
        objMilkRejectHeadMor.SHIFT = "M"
        objMilkRejectHeadMor.MCC_CODE = obj.MCC_Code
        objMilkRejectHeadMor.TOTAL_WEIGHT = 0


        Dim objMilkRejectHeadEven As clsMilkRejectHead = New clsMilkRejectHead
        objMilkRejectHeadEven.DOC_DATE = obj.Document_Date
        objMilkRejectHeadEven.SHIFT = "E"
        objMilkRejectHeadEven.MCC_CODE = obj.MCC_Code
        objMilkRejectHeadEven.TOTAL_WEIGHT = 0

        objMilkRejectHeadMor.Arr = New List(Of clsMilkRejectDetail)
        objMilkRejectHeadEven.Arr = New List(Of clsMilkRejectDetail)
        Dim qry As String
        Dim dt As DataTable
        For Each objTr As clsMilkProcurementUploaderDetail In obj.Arr
            qry = " select TSPL_Primary_Vehicle_Master.Vehicle_Weight,TSPL_VLC_MASTER_HEAD.Route_Code,TSPL_VLC_MASTER_HEAD.VSP_Code,TSPL_MCC_ROUTE_MASTER.Vehicle_Code,TSPL_VENDOR_MASTER.EMP_Type,TSPL_VENDOR_MASTER.EMP_Fixed_Amount ,TSPL_VENDOR_MASTER.Actual_charges_Slab,TSPL_VENDOR_MASTER.Actual_charges,TSPL_VENDOR_MASTER.Actual_charges_Slab2,TSPL_VENDOR_MASTER.Actual_charges2,TSPL_VENDOR_MASTER.Actual_charges_Slab3,TSPL_VENDOR_MASTER.Actual_charges3,TSPL_VENDOR_MASTER.Actual_charges_Slab4,TSPL_VENDOR_MASTER.Actual_charges4 ,TSPL_VENDOR_MASTER.Actual_charges_Slab5,TSPL_VENDOR_MASTER.Actual_charges5,TSPL_VENDOR_MASTER.Service_Charge_Per_Unit,coalesce(TSPL_VENDOR_MASTER.Rate_Head_Load,0) as Rate_Head_Load,coalesce(TSPL_VENDOR_MASTER.Rate_Own_Asset,0) as Rate_Own_Asset,TSPL_VENDOR_MASTER.Service_Basis_Head_Load,TSPL_VENDOR_MASTER.Service_Basis_Own_Asset,TSPL_Primary_Vehicle_Master.Vendor_Code as TransporterCode,TSPL_VENDOR_MASTER.Service_Charge_Type,TSPL_VENDOR_MASTER.TIP_Buffalo,TSPL_VENDOR_MASTER.TIP_Cow,TSPL_VENDOR_MASTER.TIP_Mix " + Environment.NewLine + _
                                " from TSPL_VLC_MASTER_HEAD " + Environment.NewLine + _
                                " left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_VLC_MASTER_HEAD.Route_Code " + Environment.NewLine + _
                                " left join TSPL_VENDOR_MASTER on   TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_CODE " + Environment.NewLine + _
                                " left outer join TSPL_Primary_Vehicle_Master on TSPL_Primary_Vehicle_Master.Vehicle_Code=TSPL_MCC_ROUTE_MASTER.Vehicle_Code " + Environment.NewLine + _
                                " where TSPL_VLC_MASTER_HEAD.VLC_Code='" + objTr.VLC_Code + "'"
            Dim dtVLC As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dtVLC Is Nothing OrElse dtVLC.Rows.Count <= 0 Then
                Throw New Exception("VLC Code not found :" + objTr.VLC_Code)
            End If
            If objTr.Against_Milk_Collection_DCS_Detail > 0 OrElse settMilkCollectionPickBulkRoute Then
                Dim dttemp As DataTable = clsMilkCollectionDCS.GetRouteDetails(objTr.Against_Milk_Collection_DCS_Detail, trans, settMilkCollectionPickBulkRoute)
                If dttemp IsNot Nothing AndAlso dttemp.Rows.Count > 0 Then
                    If clsCommon.myLen(dtVLC.Rows(0)("Route_Code")) <= 0 Then
                        dtVLC.Rows(0)("Route_Code") = dttemp.Rows(0)("Route_Code")
                    End If
                    If clsCommon.myLen(dtVLC.Rows(0)("TransporterCode")) <= 0 Then
                        dtVLC.Rows(0)("TransporterCode") = dttemp.Rows(0)("Tanker_Transporter_Code")
                    End If
                    If clsCommon.myLen(dtVLC.Rows(0)("Vehicle_Code")) <= 0 Then
                        dtVLC.Rows(0)("Vehicle_Code") = dttemp.Rows(0)("Vehicle_No")
                    End If
                    dtVLC.AcceptChanges()
                End If
            End If
            ''Check no purhcase invoice found for that day if found means bills are generated
            qry = "select distinct TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE from  TSPL_MILK_PURCHASE_INVOICE_DETAIL left outer join TSPL_MILK_PURCHASE_INVOICE_HEAD on TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE=TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE where TSPL_MILK_SRN_HEAD.MCC_CODE='" + obj.MCC_Code + "' and TSPL_MILK_SRN_HEAD.SHIFT='" + objTr.Shift + "' and convert(date, TSPL_MILK_SRN_HEAD.DOC_DATE,103)=convert(date, '" + clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") + "',103) and TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE='" + clsCommon.myCstr(dtVLC.Rows(0)("VSP_Code")) + "'"
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Throw New Exception("Milk Purchase invoice :" + clsCommon.myCstr(dt.Rows(0)("DOC_CODE")) + " created for VSP:" + clsCommon.myCstr(dtVLC.Rows(0)("VSP_Code")))
            End If
            qry = "select UOM_Code from TSPL_Mcc_UOM_DETAIL where stocking_unit='Y' and MCC_CODE='" & obj.MCC_Code & "' "
            Dim Unit_Code As String = clsDBFuncationality.getSingleValue(qry, trans)
            If Unit_Code = "" Then
                Throw New Exception("Fill UOM of Mcc" + obj.MCC_Code)
            End If
            Dim conv_fac As Double = clsWeightConversionInfo.GetConversion_factor(Unit_Code, IIf(clsCommon.CompairString(Unit_Code, "KG") = CompairStringResult.Equal, "LTR", "KG"), trans)

            Dim objMilkRejectDetail As clsMilkRejectDetail = New clsMilkRejectDetail
            objMilkRejectDetail.Item_CODE = "" ''Will come on based of rejection.
            objMilkRejectDetail.VLC_CODE = objTr.VLC_Code
            objMilkRejectDetail.ROUTE_CODE = clsCommon.myCstr(dtVLC.Rows(0)("Route_Code"))
            objMilkRejectDetail.VSP_CODE = clsCommon.myCstr(dtVLC.Rows(0)("VSP_Code"))
            objMilkRejectDetail.VEHICLE_CODE = clsCommon.myCstr(dtVLC.Rows(0)("Vehicle_Code"))
            objMilkRejectDetail.Other_VEHICLE = False
            objMilkRejectDetail.Other_VLC = False
            objMilkRejectDetail.NO_OF_CANS = objTr.No_Of_Cans
            objMilkRejectDetail.MILK_WEIGHT = objTr.Milk_Weight
            If clsCommon.CompairString(Unit_Code, "KG") = CompairStringResult.Equal Then
                objMilkRejectDetail.ACC_WEIGHT_KG = clsCommon.myCdbl(objTr.Milk_Weight)
                objMilkRejectDetail.ACC_WEIGHT_LTR = clsCommon.myCdbl(objTr.Milk_Weight * conv_fac)
            Else
                objMilkRejectDetail.ACC_WEIGHT_LTR = clsCommon.myCdbl(objTr.Milk_Weight)
                objMilkRejectDetail.ACC_WEIGHT_KG = clsCommon.myCdbl(objTr.Milk_Weight * conv_fac)
            End If
            objMilkRejectDetail.Conversion_Factor = conv_fac
            objMilkRejectDetail.UOM_Code = Unit_Code
            objMilkRejectDetail.FAT = objTr.FAT
            objMilkRejectDetail.SNF = objTr.SNF
            objMilkRejectDetail.SNF_Deduction_Per = dblSNFDeductionPer
            objMilkRejectDetail.FAT_Deduction_Per = dblFATDeductionPer

            If clsCommon.CompairString(objTr.Reject_Type, "#Return#") = CompairStringResult.Equal Then
                objMilkRejectDetail.Is_Return = 1
                objMilkRejectDetail.dblPenaltyPerUnit = dblRetrunPenaltyPerUnit
            ElseIf clsCommon.CompairString(objTr.Reject_Type, "#Drain#") = CompairStringResult.Equal Then
                objMilkRejectDetail.Is_Return = 2
                objMilkRejectDetail.dblPenaltyPerUnit = dblDrainPenaltyPerUnit
            ElseIf clsCommon.CompairString(objTr.Reject_Type, "#COB#") = CompairStringResult.Equal Then
                objMilkRejectDetail.Is_Return = 3
                objMilkRejectDetail.dblPenaltyPerUnit = dblCOBPenaltyPerUnit
            Else
                objMilkRejectDetail.Reject_Type = objTr.Reject_Type
                objMilkRejectDetail.Is_Return = 0
            End If
            objMilkRejectDetail.Defaulter = objTr.Reject_Defaulter
            If clsCommon.CompairString(objTr.Shift, "M") = CompairStringResult.Equal Then
                objMilkRejectDetail.SAMPLE_NO = objMilkRejectHeadMor.Arr.Count + 1
                objMilkRejectHeadMor.TOTAL_WEIGHT += objMilkRejectDetail.MILK_WEIGHT
                objMilkRejectHeadMor.Arr.Add(objMilkRejectDetail)
            Else
                objMilkRejectDetail.SAMPLE_NO = objMilkRejectHeadEven.Arr.Count + 1
                objMilkRejectHeadEven.TOTAL_WEIGHT += objMilkRejectDetail.MILK_WEIGHT
                objMilkRejectHeadEven.Arr.Add(objMilkRejectDetail)
            End If
        Next
        If objMilkRejectHeadMor.Arr IsNot Nothing AndAlso objMilkRejectHeadMor.Arr.Count > 0 Then
            qry = "select DOC_CODE from TSPL_MILK_REJECT_HEAD where convert(date, DOC_DATE,103)='" + clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") + "' and SHIFT='M' and MCC_CODE='" + obj.MCC_Code + "'"
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Throw New Exception("Milk Reject No [" + clsCommon.myCstr(dt.Rows(0)("DOC_CODE") + "] already Created"))
            End If

            clsMilkRejectHead.SaveData(objMilkRejectHeadMor, True, trans)
            clsMilkRejectHead.PostData(objMilkRejectHeadMor.DOC_CODE, trans)
        End If
        If objMilkRejectHeadEven.Arr IsNot Nothing AndAlso objMilkRejectHeadEven.Arr.Count > 0 Then
            qry = "select DOC_CODE from TSPL_MILK_REJECT_HEAD where convert(date, DOC_DATE,103)='" + clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") + "' and SHIFT='E' and MCC_CODE='" + obj.MCC_Code + "'"
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Throw New Exception("Milk Reject No [" + clsCommon.myCstr(dt.Rows(0)("DOC_CODE") + "] already Created"))
            End If
            clsMilkRejectHead.SaveData(objMilkRejectHeadEven, True, trans)
            clsMilkRejectHead.PostData(objMilkRejectHeadEven.DOC_CODE, trans)
        End If
        Return True
    End Function

    Public Shared Function MilkProcurementUploaderOLD(ByVal obj As clsMilkProcurementUploaderHead, ByVal trans As SqlTransaction) As Boolean
        Dim DtVSPChargeDetail As DataTable = clsDBFuncationality.GetDataTable("SELECT * FROM  TSPL_MCC_VSP_ChargeCategory_MAPPING ", trans)
        Dim DtPriceChargeDetail As DataTable = clsDBFuncationality.GetDataTable("SELECT * FROM  TSPL_FAT_SNF_UPLOADER_Chart_Detail ", trans)
        Dim dblEmptyCanWeight As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.EmptyCanWeight, clsFixedParameterCode.EmptyCanWeight, trans))
        Dim dblMinuteInLastVehicleForGateEntry As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MinuteInLastVehicleForGateEntry, clsFixedParameterCode.MinuteInLastVehicleForGateEntry, trans))
        Dim dblMinuteGateEntryToGrossWeight As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MinuteGateEntryToGrossWeight, clsFixedParameterCode.MinuteGateEntryToGrossWeight, trans))
        Dim dblMinuteGrossWeightToTareWeight As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MinuteGrossWeightToTareWeight, clsFixedParameterCode.MinuteGrossWeightToTareWeight, trans))
        Dim CreateNewDocumentOnUploader As Boolean = clsFixedParameter.GetData(clsFixedParameterType.CreateNewDocumentOnUploader, clsFixedParameterCode.CreateNewDocumentOnUploader, trans) = 1
        Dim WeighmentNotMandatoryInMCC As Boolean = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.WeighmentNotMandatoryInMCC, clsFixedParameterCode.WeighmentNotMandatoryInMCC, trans)) = 1
        Dim IsRoundOffPaiseAmount As Boolean = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RoundOffPaiseAmount, clsFixedParameterCode.RoundOffPaiseAmount, trans)) = 1
        Dim isPickCLRInsteadOfSNF As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProcuremntPickCLRInsteadOfSNF, clsFixedParameterCode.MilkProcuremntPickCLRInsteadOfSNF, trans)) > 0)
        Dim PickPriceFromFATAndSNF As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProcuremntPickCLRInsteadOfSNF, clsFixedParameterCode.PickPriceFromFATAndSNF, trans)) > 0)
        Dim settMilkCollectionPickBulkRoute As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkCollectionPickBulkRoute, clsFixedParameterCode.MilkCollectionPickBulkRoute, trans)) = 1)
        Dim arrShiftEndCode As New ArrayList
        Try
            Dim qry As String
            Dim dt As DataTable

            Dim corrFactor As Double = clsFixedParameter.GetData(clsFixedParameterType.defaultCorrectionFactor, clsFixedParameterCode.MilkSetting, trans)

            qry = "select is_Reuired_Gate_Entry,Shift_Default_Time_Morning,Shift_Default_Time_Evening  from TSPL_MCC_MASTER where MCC_Code='" + obj.MCC_Code + "' "
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Invalid MCC Code :" + obj.MCC_Code)
            End If
            Dim isRequiredGateEntry As Boolean = (clsCommon.myCdbl(dt.Rows(0)("is_Reuired_Gate_Entry")) = 1)
            If dt.Rows(0)("Shift_Default_Time_Morning") Is DBNull.Value Then
                Throw New Exception("Please set morning default open MCC Shift time of MCC :" + obj.MCC_Code)
            End If
            If dt.Rows(0)("Shift_Default_Time_Evening") Is DBNull.Value Then
                Throw New Exception("Please set Evening default open MCC Shift time of MCC :" + obj.MCC_Code)
            End If

            Dim dtMCCDefaultOpenTimeMoring As DateTime = clsCommon.myCDate(dt.Rows(0)("Shift_Default_Time_Morning"))
            Dim dtMCCDefaultOpenTimeEvening As DateTime = clsCommon.myCDate(dt.Rows(0)("Shift_Default_Time_Evening"))

            qry = "select Shift_Date,Shift,"
            If objCommonVar.DisplayTypeInMilkReceipt Then
                qry += "'M' as Dock_Collection_Milk_Type"
            Else
                qry += "Dock_Collection_Milk_Type "
            End If
            qry += " from TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL where Document_No='" + obj.Document_No + "' group by Shift_Date,Shift"
            If Not objCommonVar.DisplayTypeInMilkReceipt Then
                qry += ",Dock_Collection_Milk_Type "
            End If
            qry += " order by Shift_Date,Shift desc,Dock_Collection_Milk_Type"
            Dim dtDateShift As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dtDateShift IsNot Nothing AndAlso dtDateShift.Rows.Count > 0 Then
                Dim strICode = clsFixedParameter.GetData(clsFixedParameterType.MCCDefaultMilkItem, clsFixedParameterCode.MilkSetting, trans)
                For Each drDateShift As DataRow In dtDateShift.Rows
                    Dim strShift As String = clsCommon.myCstr(drDateShift("Shift"))
                    Dim strShiftDate As String = clsCommon.GetPrintDate(clsCommon.myCDate(drDateShift("Shift_Date")), "dd/MMM/yyyy")
                    Dim dtShiftDate As DateTime = clsCommon.myCDate(drDateShift("Shift_Date"))
                    Dim strDockCollectionMilkType As String = clsCommon.myCstr(drDateShift("Dock_Collection_Milk_Type"))
                    ''Check shift should be open 
                    qry = "select MCC_SHIFT_CODE  from TSPL_OPEN_MCC_SHIFT where MCC_CODE='" + obj.MCC_Code + "' and SHIFT='" + strShift + "' and convert(date, MCC_SHIFT_DATE,103)=convert(date, '" + strShiftDate + "',103)"
                    dt = clsDBFuncationality.GetDataTable(qry, trans)
                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        If CreateNewDocumentOnUploader Then
                            Dim objOpenShift As New ClsOpenMCCShift()
                            objOpenShift.MCC_CODE = obj.MCC_Code
                            objOpenShift.MCC_NAME = obj.MCC_Name
                            objOpenShift.MCC_SHIFT_DATE = clsCommon.myCDate(strShiftDate + " " + clsCommon.GetPrintDate(dtMCCDefaultOpenTimeMoring, "hh:mm:ss tt"))
                            objOpenShift.SHIFT = strShift
                            objOpenShift.Actual_Stock = 0
                            objOpenShift.Actual_FAT = 0
                            objOpenShift.Actual_SNF = 0
                            objOpenShift.Actual_FAT_Per = 0
                            objOpenShift.Actual_SNF_Per = 0
                            objOpenShift.System_Stock = 0
                            objOpenShift.System_FAT_Per = 0
                            objOpenShift.System_SNF_Per = 0
                            objOpenShift.Manual_Stock = 0
                            objOpenShift.Manual_FAT = 0
                            objOpenShift.Manual_SNF = 0
                            objOpenShift.Manual_FAT_Per = 0
                            objOpenShift.Manual_SNF_Per = 0
                            objOpenShift.CLR = 0
                            ''Generate Shift Code
                            objOpenShift.MCC_SHIFT_CODE = obj.MCC_Code + "/" + strShift
                            qry = "select max(mcc_shift_code) as code from TSPL_OPEN_MCC_SHIFT where mcc_shift_code like '" + objOpenShift.MCC_SHIFT_CODE + "%'"
                            Dim xcode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                            If xcode IsNot Nothing AndAlso clsCommon.CompairString(xcode, """") <> CompairStringResult.Equal AndAlso clsCommon.myLen(xcode) > 0 Then
                                objOpenShift.MCC_SHIFT_CODE = clsCommon.incval(xcode)
                            Else
                                objOpenShift.MCC_SHIFT_CODE = obj.MCC_Code + "/" + strShift + "00001"
                            End If
                            ''End of Generate Shift Code
                            ClsOpenMCCShift.SaveData(objOpenShift, True, trans)
                        End If

                        ''Again check Mcc Shift opend or Not
                        qry = "select MCC_SHIFT_CODE  from TSPL_OPEN_MCC_SHIFT where MCC_CODE='" + obj.MCC_Code + "' and SHIFT='" + strShift + "' and convert(date, MCC_SHIFT_DATE,103)=convert(date, '" + strShiftDate + "',103)"
                        dt = clsDBFuncationality.GetDataTable(qry, trans)
                        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                            Throw New Exception("Shift Not Opened Date :" + strShiftDate + " Shift :" + strShift)
                        End If
                    End If
                    ''Check shift should be closed
                    qry = "select DOC_CODE  from TSPL_MILK_Shift_End_HEAD where MCC_CODE='" + obj.MCC_Code + "' and SHIFT='" + strShift + "' and convert(date, MCC_DATE,103)=convert(date, '" + strShiftDate + "',103)"
                    dt = clsDBFuncationality.GetDataTable(qry, trans)
                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        If CreateNewDocumentOnUploader Then
                            Dim objShiftEnd As New clsMilkShiftEndMCC
                            'objShiftEnd.DOC_CODE = clsCommon.myCstr(txtCode.Value) Auto generated
                            objShiftEnd.DOC_DATE = clsCommon.myCDate(strShiftDate + " " + clsCommon.GetPrintDate(dtMCCDefaultOpenTimeEvening, "hh:mm:ss tt"))
                            objShiftEnd.MCC_DATE = objShiftEnd.DOC_DATE
                            objShiftEnd.SHIFT = strShift
                            objShiftEnd.MCC_CODE = obj.MCC_Code
                            objShiftEnd.Actual_Stock = 0
                            objShiftEnd.Manual_Stock = 0
                            objShiftEnd.Manual_FAT = 0
                            objShiftEnd.Manual_SNF = 0
                            objShiftEnd.Actual_FAT = 0
                            objShiftEnd.Actual_SNF = 0
                            objShiftEnd.Manual_FAT_Per = 0
                            objShiftEnd.Manual_SNF_Per = 0
                            objShiftEnd.Actual_FAT_Per = 0
                            objShiftEnd.Actual_SNF_Per = 0
                            objShiftEnd.CLR = 0
                            clsMilkShiftEndMCC.SaveData(objShiftEnd, Nothing, Nothing, trans)
                            arrShiftEndCode.Add(objShiftEnd.DOC_CODE)
                        End If
                        qry = "select DOC_CODE  from TSPL_MILK_Shift_End_HEAD where MCC_CODE='" + obj.MCC_Code + "' and SHIFT='" + strShift + "' and convert(date, MCC_DATE,103)=convert(date, '" + strShiftDate + "',103)"
                        dt = clsDBFuncationality.GetDataTable(qry, trans)
                        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                            Throw New Exception("Shift Not closed Date :" + strShiftDate + " Shift :" + strShift)
                        End If
                    End If
                    Dim isCreateNewDocOFMilkReceiptAndSample As Boolean = False
                    qry = "select DOC_CODE,(select max(TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO) as SAMPLE_NO from TSPL_MILK_RECEIPT_DETAIL where TSPL_MILK_RECEIPT_DETAIL.DOC_CODE=TSPL_MILK_RECEIPT_HEAD.DOC_CODE) as SAMPLE_NO ,(select DOC_CODE as Sample_Doc_No from TSPL_MILK_SAMPLE_HEAD where TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE=TSPL_MILK_RECEIPT_HEAD.DOC_CODE) as Sample_Doc_No  from TSPL_MILK_RECEIPT_HEAD where convert(date, DOC_DATE ,103)='" + strShiftDate + "' and SHIFT='" + strShift + "' and MCC_CODE='" + obj.MCC_Code + "' and Dock_Collection_Milk_Type ='" + strDockCollectionMilkType + "'"
                    If clsCommon.myLen(obj.Dock_Code) > 0 Then
                        qry += " and Dock_Code='" + obj.Dock_Code + "'"
                    End If

                    dt = clsDBFuncationality.GetDataTable(qry, trans)
                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        If CreateNewDocumentOnUploader Then
                            Dim objMilkReceiptHead As New clsMilkReceiptMCC
                            '' objMilkReceiptHead.DOC_CODE = clsCommon.myCstr(txtCode.Value)
                            objMilkReceiptHead.DOC_DATE = clsCommon.myCDate(drDateShift("Shift_Date"))
                            objMilkReceiptHead.SHIFT = strShift
                            objMilkReceiptHead.COMM_PORT = ""
                            objMilkReceiptHead.MCC_CODE = obj.MCC_Code
                            objMilkReceiptHead.Irregular_MCC_CODE = Nothing
                            objMilkReceiptHead.TOTAL_WEIGHT = 0
                            objMilkReceiptHead.Dock_Collection_Milk_Type = strDockCollectionMilkType
                            objMilkReceiptHead.DOCK_Code = obj.Dock_Code
                            clsMilkReceiptMCC.SaveData(objMilkReceiptHead, Nothing, trans)

                            Dim objMilkSampleHead As New clsMilkSampleMCC
                            'objMilkSampleHead.DOC_CODE = clsCommon.myCstr(txtCode.Value)
                            objMilkSampleHead.DOC_DATE = clsCommon.myCDate(drDateShift("Shift_Date"))
                            objMilkSampleHead.SHIFT = strShift
                            objMilkSampleHead.MCC_CODE = obj.MCC_Code
                            objMilkSampleHead.DOCK_Code = obj.Dock_Code
                            objMilkSampleHead.MILK_RECEIPT_CODE = objMilkReceiptHead.DOC_CODE
                            objMilkSampleHead.TOTAL_QTY = 0
                            objMilkSampleHead.TOTAL_FAT = 0
                            objMilkSampleHead.TOTAL_SNF = 0
                            objMilkSampleHead.Dock_Collection_Milk_Type = strDockCollectionMilkType
                            clsMilkSampleMCC.SaveData(objMilkSampleHead, Nothing, Nothing, trans)

                            isCreateNewDocOFMilkReceiptAndSample = True
                            dt = clsDBFuncationality.GetDataTable(qry, trans)
                            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                                Throw New Exception("Milk Receipt no not found. Date :" + strShiftDate + " Shift :" + strShift)
                            End If
                        Else
                            Throw New Exception("Milk Receipt no not found. Date :" + strShiftDate + " Shift :" + strShift)
                        End If
                    End If
                    Dim MilkReceiptNo As String = clsCommon.myCstr(dt.Rows(0)("DOC_CODE"))
                    Dim SampleNo As Integer = clsCommon.myCdbl(dt.Rows(0)("SAMPLE_NO"))
                    Dim MilkSampleNo As String = clsCommon.myCstr(dt.Rows(0)("Sample_Doc_No"))

                    Dim arr As List(Of clsMilkProcurementUploaderDetail) = clsMilkProcurementUploaderDetail.GetData(obj.Document_No, "  Shift_Date='" + strShiftDate + "' and Shift='" + strShift + "' " + IIf(objCommonVar.DisplayTypeInMilkReceipt, "", " and Dock_Collection_Milk_Type='" + strDockCollectionMilkType + "'"), trans)
                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
                        For ii As Integer = 0 To arr.Count - 1
                            SampleNo += 1
                            Dim objtr As clsMilkProcurementUploaderDetail = arr(ii)
                            qry = " select TSPL_Primary_Vehicle_Master.Vehicle_Weight,TSPL_VLC_MASTER_HEAD.Route_Code,TSPL_VLC_MASTER_HEAD.VSP_Code,TSPL_MCC_ROUTE_MASTER.Vehicle_Code,TSPL_VENDOR_MASTER.EMP_Type,TSPL_VENDOR_MASTER.EMP_Fixed_Amount ,TSPL_VENDOR_MASTER.Actual_charges_Slab,TSPL_VENDOR_MASTER.Actual_charges,TSPL_VENDOR_MASTER.Actual_charges_Slab2,TSPL_VENDOR_MASTER.Actual_charges2,TSPL_VENDOR_MASTER.Actual_charges_Slab3,TSPL_VENDOR_MASTER.Actual_charges3,TSPL_VENDOR_MASTER.Actual_charges_Slab4,TSPL_VENDOR_MASTER.Actual_charges4 ,TSPL_VENDOR_MASTER.Actual_charges_Slab5,TSPL_VENDOR_MASTER.Actual_charges5,TSPL_VENDOR_MASTER.Service_Charge_Per_Unit,coalesce(TSPL_VENDOR_MASTER.Rate_Head_Load,0) as Rate_Head_Load,coalesce(TSPL_VENDOR_MASTER.Rate_Own_Asset,0) as Rate_Own_Asset,TSPL_VENDOR_MASTER.Service_Basis_Head_Load,TSPL_VENDOR_MASTER.Service_Basis_Own_Asset,TSPL_Primary_Vehicle_Master.Vendor_Code as TransporterCode,TSPL_VENDOR_MASTER.Service_Charge_Type,TSPL_VENDOR_MASTER.TIP_Buffalo,TSPL_VENDOR_MASTER.TIP_Cow,TSPL_VENDOR_MASTER.TIP_Mix,TSPL_VLC_MASTER_HEAD.Milk_Receive_UOM,TSPL_VENDOR_MASTER.DistanceKM_Head_Load " + Environment.NewLine +
                                " from TSPL_VLC_MASTER_HEAD " + Environment.NewLine +
                                " left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_VLC_MASTER_HEAD.Route_Code " + Environment.NewLine +
                                " left join TSPL_VENDOR_MASTER on   TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_CODE " + Environment.NewLine +
                                " left outer join TSPL_Primary_Vehicle_Master on TSPL_Primary_Vehicle_Master.Vehicle_Code=TSPL_MCC_ROUTE_MASTER.Vehicle_Code " + Environment.NewLine +
                                " where TSPL_VLC_MASTER_HEAD.VLC_Code='" + objtr.VLC_Code + "'"
                            Dim dtVLC As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                            If dtVLC Is Nothing OrElse dtVLC.Rows.Count <= 0 Then
                                Throw New Exception("VLC Code not found :" + objtr.VLC_Code)
                            End If

                            If objtr.Against_Milk_Collection_DCS_Detail > 0 OrElse settMilkCollectionPickBulkRoute Then
                                Dim dttemp As DataTable = clsMilkCollectionDCS.GetRouteDetails(objtr.Against_Milk_Collection_DCS_Detail, trans, settMilkCollectionPickBulkRoute, objtr.Bulk_Route_Code)
                                If dttemp IsNot Nothing AndAlso dttemp.Rows.Count > 0 Then
                                    If clsCommon.myLen(dtVLC.Rows(0)("Route_Code")) <= 0 Then
                                        dtVLC.Rows(0)("Route_Code") = dttemp.Rows(0)("Route_Code")
                                    End If
                                    If clsCommon.myLen(dtVLC.Rows(0)("TransporterCode")) <= 0 Then
                                        dtVLC.Rows(0)("TransporterCode") = dttemp.Rows(0)("Tanker_Transporter_Code")
                                    End If
                                    If clsCommon.myLen(dtVLC.Rows(0)("Vehicle_Code")) <= 0 Then
                                        dtVLC.Rows(0)("Vehicle_Code") = dttemp.Rows(0)("Vehicle_No")
                                    End If
                                    dtVLC.AcceptChanges()
                                End If
                            End If

                            If clsCommon.myLen(dtVLC.Rows(0)("Route_Code")) <= 0 Then
                                Throw New Exception("Route not defined for VLC Code [" + objtr.VLC_Code + "]")
                            End If
                            If clsCommon.myLen(dtVLC.Rows(0)("TransporterCode")) <= 0 Then
                                Throw New Exception("Transporter not defined for VLC Code [" + objtr.VLC_Code + "]")
                            End If

                            ''Check no purhcase invoice found for that day if found means bills are generated
                            qry = "select distinct TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE from  TSPL_MILK_PURCHASE_INVOICE_DETAIL left outer join TSPL_MILK_PURCHASE_INVOICE_HEAD on TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE=TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE where TSPL_MILK_SRN_HEAD.MCC_CODE='" + obj.MCC_Code + "' and TSPL_MILK_SRN_HEAD.SHIFT='" + strShift + "' and convert(date, TSPL_MILK_SRN_HEAD.DOC_DATE,103)=convert(date, '" + strShiftDate + "',103) and TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE='" + clsCommon.myCstr(dtVLC.Rows(0)("VSP_Code")) + "'"
                            dt = clsDBFuncationality.GetDataTable(qry, trans)
                            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                Throw New Exception("Milk Purchase invoice :" + clsCommon.myCstr(dt.Rows(0)("DOC_CODE")) + " created for VSP:" + clsCommon.myCstr(dtVLC.Rows(0)("VSP_Code")) + " .For Payment Cycle Date :" + strShiftDate + " Shift :" + strShift)
                            End If

                            qry = "select UOM_Code from TSPL_Mcc_UOM_DETAIL where stocking_unit='Y' and MCC_CODE='" & obj.MCC_Code & "' "
                            Dim Unit_Code As String = clsDBFuncationality.getSingleValue(qry, trans)
                            If Unit_Code = "" Then
                                Throw New Exception("Fill UOM of Mcc" + obj.MCC_Code)
                            End If
                            qry = "select UOM_Code from TSPL_Item_UOM_DETAIL where Item_CODE='" & strICode & "' and UOM_code='" & Unit_Code & "' "
                            Dim Item_Unit_Code As String = clsDBFuncationality.getSingleValue(qry, trans)
                            If Item_Unit_Code = "" Then
                                Throw New Exception("Fill " & Unit_Code & " UOM of Item " + strICode)
                            End If
                            Dim conv_fac As Double = clsWeightConversionInfo.GetConversion_factor(Unit_Code, IIf(clsCommon.CompairString(Unit_Code, "KG") = CompairStringResult.Equal, "LTR", "KG"), trans)


                            If isRequiredGateEntry Then
                                qry = "select Entry_Code from TSPL_MILK_GATE_ENTRY_IN where Shift_Date='" + strShiftDate + "' and Entry_Shift='" + strShift + "' and Route_Code='" + clsCommon.myCstr(dtVLC.Rows(0)("Route_Code")) + "'"
                                Dim strGateEntryNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                                If clsCommon.myLen(strGateEntryNo) <= 0 Then
                                    ''Gate Entry
                                    Dim objMGEI As New clsMilkGateEntryIn()
                                    ''objMGEI.Entry_Code = "" To be Generated
                                    qry = "select max(Entry_Date) as Entry_Date from TSPL_MILK_GATE_ENTRY_IN where Shift_Date='" + strShiftDate + "' and Entry_Shift='" + strShift + "'"
                                    dt = clsDBFuncationality.GetDataTable(qry, trans)
                                    Dim tempDate As DateTime
                                    If dt.Rows(0)(0) Is DBNull.Value Then
                                        If clsCommon.CompairString(strShift, "M") = CompairStringResult.Equal Then
                                            tempDate = New DateTime(dtShiftDate.Year, dtShiftDate.Month, dtShiftDate.Day, 9, 12, 0)
                                        Else
                                            tempDate = New DateTime(dtShiftDate.Year, dtShiftDate.Month, dtShiftDate.Day, 21, 11, 0)
                                        End If
                                    Else
                                        tempDate = clsCommon.myCDate(dt.Rows(0)(0))
                                        tempDate = tempDate.AddMinutes(dblMinuteInLastVehicleForGateEntry)
                                    End If
                                    objMGEI.Entry_Date = tempDate
                                    objMGEI.Shift_Date = dtShiftDate
                                    objMGEI.Entry_Shift = strShift
                                    objMGEI.MCC_Code = obj.MCC_Code
                                    objMGEI.Route_Code = clsCommon.myCstr(dtVLC.Rows(0)("Route_Code"))
                                    objMGEI.Vehicle_No = clsCommon.myCstr(dtVLC.Rows(0)("Vehicle_Code"))
                                    objMGEI.Transporter_Code = clsCommon.myCstr(dtVLC.Rows(0)("TransporterCode"))
                                    objMGEI.Cans_Filled = 0
                                    objMGEI.Cans_Empty = 0
                                    objMGEI.Remarks = ""
                                    objMGEI.SaveData(objMGEI, True, trans)
                                    clsMilkGateEntryIn.PostData(objMGEI.Entry_Code, trans)
                                    strGateEntryNo = objMGEI.Entry_Code
                                    ''end For Gate Entry
                                    Dim GateOutDateTime As DateTime? = Nothing
                                    If Not WeighmentNotMandatoryInMCC Then
                                        ''For Weighment Entry
                                        Dim objMGEW As New clsMilkGateEntryWeighment()
                                        ''objMGEW.Weighment_Code = txtCode.Value ''to be genrated
                                        objMGEW.GW_Weighment_Date = objMGEI.Entry_Date.AddMinutes(dblMinuteGateEntryToGrossWeight)
                                        objMGEW.TW_Weighment_Date = objMGEW.GW_Weighment_Date.AddMinutes(dblMinuteGrossWeightToTareWeight)
                                        objMGEW.Against_Gate_Entry_No = objMGEI.Entry_Code
                                        objMGEW.Tare_Weight = clsCommon.myCdbl(dtVLC.Rows(0)("Vehicle_Weight")) ''to be picked from  TSPL_Primary_Vehicle_Master
                                        If objMGEW.Tare_Weight <= 0 Then
                                            Throw New Exception("Please provide Vehicel weight for primary transporter vehicle master for vehicle no " + objMGEI.Vehicle_No)
                                        End If

                                        objMGEW.Gross_Weight = objMGEW.Tare_Weight
                                        objMGEW.Net_Weight = 0

                                        objMGEW.SaveDataGW(objMGEW, True, trans) ''Save Gross Weight
                                        clsMilkGateEntryWeighment.PostDataGW(objMGEW.Weighment_Code, trans)

                                        objMGEW.SaveDataTW(objMGEW, False, trans) ''Save Tare Weight
                                        clsMilkGateEntryWeighment.PostDataTW(objMGEW.Weighment_Code, trans)
                                        ''End For Weighment Entry
                                        GateOutDateTime = objMGEW.TW_Weighment_Date.Value.AddMinutes(dblMinuteGateEntryToGrossWeight)
                                    End If

                                    ''Gate Entry Out
                                    Dim objMGEO As New clsMilkGateEntryOut()
                                    ''objMGEO.Gate_Out_Code = txtCode.Value ''To be generated
                                    If GateOutDateTime IsNot Nothing Then
                                        objMGEO.Gate_Out_Date = GateOutDateTime
                                    Else
                                        objMGEO.Gate_Out_Date = tempDate.AddMinutes(30)
                                    End If
                                    objMGEO.Against_Gate_Entry_No = objMGEI.Entry_Code
                                    objMGEO.Cans_Empty = 0
                                    objMGEO.Cans_Filled = 0
                                    objMGEO.Remarks = ""

                                    objMGEO.SaveData(objMGEO, True, trans)
                                    clsMilkGateEntryOut.PostData(objMGEO.Gate_Out_Code, trans)
                                    ''End of Gate Entry Out
                                End If

                                qry = "update TSPL_MILK_GATE_ENTRY_IN set Cans_Filled=Cans_Filled+'" + clsCommon.myCstr(objtr.No_Of_Cans) + "'   where Entry_Code='" + strGateEntryNo + "'"
                                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                                Dim dblWeight As Double = Math.Round((objtr.No_Of_Cans * dblEmptyCanWeight) + objtr.Milk_Weight, 3, MidpointRounding.ToEven)
                                qry = "update TSPL_MILK_GATE_ENTRY_WEIGHTMENT set Gross_Weight=Gross_Weight+'" + clsCommon.myCstr(dblWeight) + "' ,Net_Weight=Net_Weight+'" + clsCommon.myCstr(dblWeight) + "' where Against_Gate_Entry_No='" + strGateEntryNo + "'"
                                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                                qry = "update TSPL_MILK_GATE_ENTRY_OUT set Cans_Empty=Cans_Empty+'" + clsCommon.myCstr(objtr.No_Of_Cans) + "' where Against_Gate_Entry_No='" + strGateEntryNo + "'"
                                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                            End If


                            ''Milk Receipt Save
                            Dim objMilkReceiptDetail As New clsMilkReceiptMCCDetail()
                            objMilkReceiptDetail.DOC_CODE = MilkReceiptNo
                            objMilkReceiptDetail.SAMPLE_NO = SampleNo
                            objMilkReceiptDetail.VLC_CODE = objtr.VLC_Code
                            objMilkReceiptDetail.ROUTE_CODE = clsCommon.myCstr(dtVLC.Rows(0)("Route_Code"))
                            objMilkReceiptDetail.VSP_CODE = clsCommon.myCstr(dtVLC.Rows(0)("VSP_Code"))
                            objMilkReceiptDetail.Item_CODE = clsCommon.myCstr(strICode)
                            objMilkReceiptDetail.VEHICLE_CODE = clsCommon.myCstr(dtVLC.Rows(0)("Vehicle_Code"))
                            objMilkReceiptDetail.Other_VEHICLE = "F"
                            objMilkReceiptDetail.Other_VLC = 0
                            objMilkReceiptDetail.NO_OF_CANS = objtr.No_Of_Cans
                            objMilkReceiptDetail.MILK_WEIGHT = objtr.Milk_Weight
                            Dim Unit_CodeApply As String = Unit_Code
                            Dim conv_facApply As String = conv_fac
                            If clsCommon.myLen(clsCommon.myCstr(dtVLC.Rows(0)("Milk_Receive_UOM"))) > 0 Then ''BHO/11/06/21-000004 By balwinder on 17/06/2021
                                Unit_CodeApply = clsCommon.myCstr(dtVLC.Rows(0)("Milk_Receive_UOM"))
                                conv_facApply = clsWeightConversionInfo.GetConversion_factor(Unit_CodeApply, IIf(clsCommon.CompairString(Unit_CodeApply, "KG") = CompairStringResult.Equal, "LTR", "KG"), trans)
                            End If

                            If clsCommon.CompairString(Unit_CodeApply, "KG") = CompairStringResult.Equal Then
                                objMilkReceiptDetail.ACC_WEIGHT = clsCommon.myCdbl(objtr.Milk_Weight)
                                objMilkReceiptDetail.LTR_WEIGHT = clsCommon.myCdbl(objtr.Milk_Weight * conv_facApply)
                            Else
                                objMilkReceiptDetail.LTR_WEIGHT = clsCommon.myCdbl(objtr.Milk_Weight)
                                objMilkReceiptDetail.ACC_WEIGHT = clsCommon.myCdbl(objtr.Milk_Weight * conv_facApply)
                            End If
                            objMilkReceiptDetail.TYPE = objtr.Dock_Collection_Milk_Type
                            objMilkReceiptDetail.MILK_TYPE = "Good"
                            objMilkReceiptDetail.DOC_DATE = dtShiftDate
                            objMilkReceiptDetail.SHIFT = objtr.Shift
                            objMilkReceiptDetail.COMM_PORT = ""
                            objMilkReceiptDetail.MCC_CODE = obj.MCC_Code

                            If objtr.Manual_Weight = 1 Then
                                objMilkReceiptDetail.IS_ENTERED_MANUAL = "Y"
                            Else
                                objMilkReceiptDetail.IS_ENTERED_MANUAL = "N"
                            End If

                            objMilkReceiptDetail.UOM_Code = Unit_CodeApply
                            objMilkReceiptDetail.Conversion_Factor = conv_facApply
                            objMilkReceiptDetail.Against_Uploader_TR_No = objtr.TR_No

                            Dim arrLIST As New List(Of clsMilkReceiptMCCDetail)
                            arrLIST.Add(objMilkReceiptDetail)
                            clsMilkReceiptMCCDetail.SaveData(MilkReceiptNo, arrLIST, "M", obj.Dock_Code, trans)
                            ''Milk Receipt Save End

                            ''Milk Sample Save
                            Dim objMilkSampleDetail As clsMilkSampleMCCDetail = New clsMilkSampleMCCDetail()
                            objMilkSampleDetail.DOC_CODE = MilkSampleNo
                            objMilkSampleDetail.SAMPLE_NO = SampleNo
                            objMilkSampleDetail.VLC_DOC_CODE = objMilkReceiptDetail.VLC_DOC_CODE
                            objMilkSampleDetail.MILK_TYPE = objMilkReceiptDetail.MILK_TYPE
                            objMilkSampleDetail.TYPE = objMilkReceiptDetail.TYPE
                            objMilkSampleDetail.VSP_CODE = objMilkReceiptDetail.VSP_CODE
                            objMilkSampleDetail.ITEm_CODE = objMilkReceiptDetail.Item_CODE
                            objMilkSampleDetail.MILK_Qty = objMilkReceiptDetail.MILK_WEIGHT
                            objMilkSampleDetail.ACC_Qty = objMilkReceiptDetail.ACC_WEIGHT
                            objMilkSampleDetail.FAT = objtr.FAT
                            Dim strMilkType As String = strDockCollectionMilkType
                            If isPickCLRInsteadOfSNF Then
                                objMilkSampleDetail.CLR = Math.Truncate(objtr.SNF * 10) / 10
                                objMilkSampleDetail.SNF = clsEkoPro.getSnfOnCalculation(objMilkSampleDetail.FAT, objMilkSampleDetail.CLR, corrFactor)
                                If PickPriceFromFATAndSNF Then
                                    objMilkSampleDetail.SNF = clsCommon.myRoundOFF(objMilkSampleDetail.SNF, 1, 4)
                                    objMilkSampleDetail.RATE = clsEkoPro.getRateAndPriceCodeFromUploaderShiftWise(objMilkReceiptDetail.MILK_WEIGHT, objMilkSampleDetail.Price_Code, objMilkSampleDetail.FAT, objMilkSampleDetail.SNF, obj.MCC_Code, objtr.VLC_Code, objtr.Shift, dtShiftDate, trans, strMilkType)
                                Else
                                    objMilkSampleDetail.SNF = clsCommon.myRoundOFF(objMilkSampleDetail.SNF, 2, 9)
                                    objMilkSampleDetail.RATE = clsEkoPro.getRateFromUploaderShiftWiseCLR(objMilkSampleDetail.FAT, objMilkSampleDetail.CLR, obj.MCC_Code, objtr.VLC_Code, objtr.Shift, dtShiftDate, trans, strDockCollectionMilkType, objMilkSampleDetail.Price_Code)
                                End If
                            Else
                                If objCommonVar.DisplayTypeInMilkReceipt Then
                                    strMilkType = objMilkSampleDetail.TYPE
                                End If

                                objMilkSampleDetail.SNF = objtr.SNF
                                objMilkSampleDetail.CLR = clsEkoPro.getClrOnCalculation(objMilkSampleDetail.FAT, objMilkSampleDetail.SNF, corrFactor)
                                objMilkSampleDetail.RATE = clsEkoPro.getRateAndPriceCodeFromUploaderShiftWise(objMilkReceiptDetail.MILK_WEIGHT, objMilkSampleDetail.Price_Code, objMilkSampleDetail.FAT, objMilkSampleDetail.SNF, obj.MCC_Code, objtr.VLC_Code, objtr.Shift, dtShiftDate, trans, strMilkType)
                            End If
                            objMilkSampleDetail.Correction_Factor = corrFactor
                            If clsCommon.myLen(objMilkSampleDetail.Price_Code) <= 0 Then
                                If objMilkReceiptDetail.MILK_WEIGHT > 0 Then
                                    If Not clsVendorMaster.IsVLCDripSaver(objtr.VLC_Code, trans) Then
                                        Throw New Exception("No Rate Found At Row No " + clsCommon.myCstr(objtr.SNo) + Environment.NewLine + "VLC: " + objtr.VLC_Code + "(" + objtr.VLC_Name + ")  FAT: " + clsCommon.myCstr(objtr.FAT) + " SNF: " + clsCommon.myCstr(objtr.SNF))
                                    End If
                                End If
                            End If

                            objMilkSampleDetail.UOM_Code = objMilkReceiptDetail.UOM_Code
                            objMilkSampleDetail.AMOUNT = Math.Round(clsCommon.myCdbl(objMilkSampleDetail.RATE * objMilkSampleDetail.MILK_Qty), 2, MidpointRounding.AwayFromZero)
                            If objtr.Manual_Sample = 1 Then
                                objMilkSampleDetail.Is_Entered_Manualy = "Manual"
                            Else
                                objMilkSampleDetail.Is_Entered_Manualy = "Auto"
                            End If

                            Dim arrSampleLIST As New List(Of clsMilkSampleMCCDetail)
                            arrSampleLIST.Add(objMilkSampleDetail)
                            clsMilkSampleMCCDetail.SaveData(MilkSampleNo, arrSampleLIST, trans, True, MilkReceiptNo)
                            ''Milk Sample QC Details
                            Dim ArrParamDetailMilkProcuremnt As List(Of clsMilkProcurementUploaderQCParameterDetail) = clsMilkProcurementUploaderQCParameterDetail.getData(obj.Document_No, trans, objtr.SNo)
                            If ArrParamDetailMilkProcuremnt IsNot Nothing AndAlso ArrParamDetailMilkProcuremnt.Count > 0 Then
                                Dim ArrParamDetail = New List(Of clsMilkSampleQCParameterDetail)
                                Dim objParam As clsMilkSampleQCParameterDetail = Nothing
                                For i As Integer = 0 To ArrParamDetailMilkProcuremnt.Count - 1
                                    objParam = New clsMilkSampleQCParameterDetail
                                    objParam.Param_Field_Code = ArrParamDetailMilkProcuremnt(i).Param_Field_Code
                                    objParam.Param_Field_Desc = ArrParamDetailMilkProcuremnt(i).Param_Field_Desc
                                    objParam.Param_Field_Value = ArrParamDetailMilkProcuremnt(i).Param_Field_Value
                                    objParam.Param_Type = ArrParamDetailMilkProcuremnt(i).Param_Type
                                    objParam.Sample_No = SampleNo
                                    ArrParamDetail.Add(objParam)
                                Next
                                clsMilkSampleQCParameterDetail.saveData(False, MilkSampleNo, ArrParamDetail, trans)
                            End If
                            ''Milk Sample Save End

                            ''Milk SRN Save
                            Dim objMilkSRNHead As clsMilkSRNMCC = New clsMilkSRNMCC
                            'objMilkSRNHead.MILK_SAMPLE_CODE = MilkSampleNo
                            objMilkSRNHead.DOC_DATE = dtShiftDate
                            objMilkSRNHead.SHIFT = strShift
                            objMilkSRNHead.COMM_PORT = ""
                            objMilkSRNHead.MCC_CODE = obj.MCC_Code
                            objMilkSRNHead.SAMPLE_NO = SampleNo
                            objMilkSRNHead.VLC_DOC_CODE = objMilkReceiptDetail.VLC_DOC_CODE
                            objMilkSRNHead.VEHICLE_CODE = objMilkReceiptDetail.VEHICLE_CODE
                            objMilkSRNHead.VLC_CODE = objMilkReceiptDetail.VLC_CODE
                            objMilkSRNHead.ROUTE_CODE = objMilkReceiptDetail.ROUTE_CODE
                            objMilkSRNHead.VSP_CODE = objMilkReceiptDetail.VSP_CODE
                            objMilkSRNHead.TransPorter = clsCommon.myCstr(dtVLC.Rows(0)("TransporterCode"))
                            objMilkSRNHead.Dock_Collection_Milk_Type = strMilkType

                            Dim objMilkSRNDetail As clsMilkSRNMCCDetail = New clsMilkSRNMCCDetail()
                            objMilkSRNDetail.Item_CODE = objMilkReceiptDetail.Item_CODE
                            objMilkSRNDetail.MILK_Qty = objMilkReceiptDetail.MILK_WEIGHT
                            objMilkSRNDetail.ACC_Qty = objMilkReceiptDetail.ACC_WEIGHT
                            objMilkSRNDetail.FAT = objMilkSampleDetail.FAT
                            objMilkSRNDetail.SNF = objMilkSampleDetail.SNF
                            objMilkSRNDetail.CLR = objMilkSampleDetail.CLR
                            objMilkSRNDetail.MCC_CODE = obj.MCC_Code
                            objMilkSRNDetail.Correction_Factor = objMilkSampleDetail.Correction_Factor
                            objMilkSRNDetail.RATE = objMilkSampleDetail.RATE
                            objMilkSRNDetail.UOM = objMilkSampleDetail.UOM_Code
                            objMilkSRNDetail.Price_Code = objMilkSampleDetail.Price_Code
                            objMilkSRNDetail.AMOUNT = objMilkSampleDetail.AMOUNT
                            objMilkSRNDetail.Own_Asset_Rate = clsCommon.myCdbl(dtVLC.Rows(0)("Rate_Own_Asset"))
                            objMilkSRNDetail.Commission = 0 ' because nature is always E and it is never C 'clsCommon.myCdbl(dr(0)("Actual_charges"))
                            objMilkSRNDetail.Commission_Amount = Math.Round(objMilkSRNDetail.AMOUNT * objMilkSRNDetail.Commission / 100, 2)
                            objMilkSRNDetail.Std_Qty = clsInventoryMovementNew.GetStdQty(trans, Math.Round(objMilkSRNDetail.ACC_Qty * objMilkSRNDetail.FAT / 100, 2), Math.Round(objMilkSRNDetail.ACC_Qty * objMilkSRNDetail.SNF / 100, 2), objMilkSRNHead.DOC_DATE)

                            If clsCommon.CompairString(clsCommon.myCstr(dtVLC.Rows(0)("EMP_Type")), "FP") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(dtVLC.Rows(0)("EMP_Type")), "FAFP") = CompairStringResult.Equal Then
                                objMilkSRNDetail.Payment_Commission = clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges"))
                                If clsCommon.CompairString(clsCommon.myCstr(dtVLC.Rows(0)("Service_Charge_Type")), "%(Percentage)") = CompairStringResult.Equal Then
                                    objMilkSRNDetail.Emp_Amount = Math.Round(objMilkSRNDetail.AMOUNT * objMilkSRNDetail.Payment_Commission / 100, 2)
                                ElseIf clsCommon.CompairString(clsCommon.myCstr(dtVLC.Rows(0)("Service_Charge_Type")), "Rate/Kg") = CompairStringResult.Equal Then
                                    objMilkSRNDetail.Emp_Amount = Math.Round(objMilkSRNDetail.ACC_Qty * objMilkSRNDetail.Payment_Commission, 2)
                                ElseIf clsCommon.CompairString(clsCommon.myCstr(dtVLC.Rows(0)("Service_Charge_Type")), "Rate/Ltr") = CompairStringResult.Equal Then
                                    objMilkSRNDetail.Emp_Amount = Math.Round(objMilkSRNDetail.MILK_Qty * objMilkSRNDetail.Payment_Commission, 2)
                                End If
                                If clsCommon.CompairString(clsCommon.myCstr(dtVLC.Rows(0)("EMP_Type")), "FAFP") = CompairStringResult.Equal Then
                                    objMilkSRNDetail.Emp_Amount += clsCommon.myCdbl(dtVLC.Rows(0)("EMP_Fixed_Amount"))
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(dtVLC.Rows(0)("EMP_Type")), "SWP") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(dtVLC.Rows(0)("EMP_Type")), "FASWP") = CompairStringResult.Equal Then
                                If clsCommon.CompairString(clsCommon.myCstr(dtVLC.Rows(0)("Service_Charge_Type")), "%(Percentage)") = CompairStringResult.Equal Then
                                    If clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab5")) > 0 AndAlso objMilkSRNDetail.AMOUNT >= clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab5")) Then
                                        objMilkSRNDetail.Payment_Commission = clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges5"))
                                    ElseIf clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab4")) > 0 AndAlso objMilkSRNDetail.AMOUNT >= clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab4")) Then
                                        objMilkSRNDetail.Payment_Commission = clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges4"))
                                    ElseIf clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab3")) > 0 AndAlso objMilkSRNDetail.AMOUNT >= clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab3")) Then
                                        objMilkSRNDetail.Payment_Commission = clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges3"))
                                    ElseIf clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab2")) > 0 AndAlso objMilkSRNDetail.AMOUNT >= clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab2")) Then
                                        objMilkSRNDetail.Payment_Commission = clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges2"))
                                    Else
                                        objMilkSRNDetail.Payment_Commission = clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges"))
                                    End If
                                    objMilkSRNDetail.Emp_Amount = Math.Round(objMilkSRNDetail.AMOUNT * objMilkSRNDetail.Payment_Commission / 100, 2)
                                ElseIf clsCommon.CompairString(clsCommon.myCstr(dtVLC.Rows(0)("Service_Charge_Type")), "Rate/Kg") = CompairStringResult.Equal Then
                                    If clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab5")) > 0 AndAlso objMilkSRNDetail.ACC_Qty >= clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab5")) Then
                                        objMilkSRNDetail.Payment_Commission = clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges5"))
                                    ElseIf clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab4")) > 0 AndAlso objMilkSRNDetail.ACC_Qty >= clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab4")) Then
                                        objMilkSRNDetail.Payment_Commission = clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges4"))
                                    ElseIf clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab3")) > 0 AndAlso objMilkSRNDetail.ACC_Qty >= clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab3")) Then
                                        objMilkSRNDetail.Payment_Commission = clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges3"))
                                    ElseIf clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab2")) > 0 AndAlso objMilkSRNDetail.ACC_Qty >= clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab2")) Then
                                        objMilkSRNDetail.Payment_Commission = clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges2"))
                                    Else
                                        objMilkSRNDetail.Payment_Commission = clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges"))
                                    End If
                                    objMilkSRNDetail.Emp_Amount = Math.Round(objMilkSRNDetail.ACC_Qty * objMilkSRNDetail.Payment_Commission, 2)
                                ElseIf clsCommon.CompairString(clsCommon.myCstr(dtVLC.Rows(0)("Service_Charge_Type")), "Rate/Ltr") = CompairStringResult.Equal Then
                                    If clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab5")) > 0 AndAlso objMilkSRNDetail.MILK_Qty >= clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab5")) Then
                                        objMilkSRNDetail.Payment_Commission = clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges5"))
                                    ElseIf clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab4")) > 0 AndAlso objMilkSRNDetail.MILK_Qty >= clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab4")) Then
                                        objMilkSRNDetail.Payment_Commission = clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges4"))
                                    ElseIf clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab3")) > 0 AndAlso objMilkSRNDetail.MILK_Qty >= clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab3")) Then
                                        objMilkSRNDetail.Payment_Commission = clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges3"))
                                    ElseIf clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab2")) > 0 AndAlso objMilkSRNDetail.MILK_Qty >= clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab2")) Then
                                        objMilkSRNDetail.Payment_Commission = clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges2"))
                                    Else
                                        objMilkSRNDetail.Payment_Commission = clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges"))
                                    End If
                                    objMilkSRNDetail.Emp_Amount = Math.Round(objMilkSRNDetail.MILK_Qty * objMilkSRNDetail.Payment_Commission, 2)
                                End If
                                If clsCommon.CompairString(clsCommon.myCstr(dtVLC.Rows(0)("EMP_Type")), "FASWP") = CompairStringResult.Equal Then
                                    objMilkSRNDetail.Emp_Amount += clsCommon.myCdbl(dtVLC.Rows(0)("EMP_Fixed_Amount"))
                                End If

                            ElseIf clsCommon.CompairString(clsCommon.myCstr(dtVLC(0)("EMP_Type")), "FPSP") = CompairStringResult.Equal Then
                                objMilkSRNDetail.Payment_Commission = clsCommon.myCdbl(dtVLC(0)("Actual_charges"))
                                Dim objSPR As clsStandardPrice = clsStandardPrice.GetStandartPrice(objMilkSRNDetail.Price_Code, trans)
                                If objSPR IsNot Nothing Then
                                    If (objSPR.Std_Percent_FAT <> 0 AndAlso objSPR.Std_Percent_SNF <> 0) Then
                                        If clsCommon.CompairString(clsCommon.myCstr(dtVLC(0)("Service_Charge_Type")), "Rate/Kg") = CompairStringResult.Equal Then
                                            objMilkSRNDetail.Emp_Amount = Math.Round((Math.Round(objMilkSRNDetail.ACC_Qty * objMilkSRNDetail.FAT / 100, 3) * objMilkSRNDetail.Payment_Commission * objSPR.Weightage_FAT / objSPR.Std_Percent_FAT) + (Math.Round(objMilkSRNDetail.ACC_Qty * objMilkSRNDetail.SNF / 100, 3) * objMilkSRNDetail.Payment_Commission * objSPR.Weightage_SNF / objSPR.Std_Percent_SNF), 2)
                                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dtVLC(0)("Service_Charge_Type")), "Rate/Ltr") = CompairStringResult.Equal Then
                                            Dim qty As Decimal = objMilkSRNDetail.ACC_Qty
                                            If conv_facApply <> 0 Then
                                                qty = objMilkSRNDetail.ACC_Qty / conv_facApply
                                            End If
                                            objMilkSRNDetail.Emp_Amount = Math.Round((Math.Round(qty * objMilkSRNDetail.FAT / 100, 3) * objMilkSRNDetail.Payment_Commission * objSPR.Weightage_FAT / objSPR.Std_Percent_FAT) + (Math.Round(qty * objMilkSRNDetail.SNF / 100, 3) * objMilkSRNDetail.Payment_Commission * objSPR.Weightage_SNF / objSPR.Std_Percent_SNF), 2)
                                        Else
                                            objMilkSRNDetail.Emp_Amount = 0
                                            'Throw New Exception("EMP Service Basis is Not valid of VSP " + objMilkSRNDetail.VlC_Code)
                                        End If
                                    End If
                                End If
                            Else
                                Throw New Exception("EMP Type is Not a valid")
                            End If

                            If clsCommon.CompairString(strMilkType, "C") = CompairStringResult.Equal Then
                                objMilkSRNDetail.TIP_Amount = Math.Round(clsCommon.myCdbl(dtVLC(0)("TIP_Cow")) * (objMilkSRNDetail.FAT + objMilkSRNDetail.SNF) * objMilkSRNDetail.ACC_Qty / 100, 2, MidpointRounding.AwayFromZero)
                            ElseIf clsCommon.CompairString(strMilkType, "B") = CompairStringResult.Equal Then
                                objMilkSRNDetail.TIP_Amount = Math.Round(clsCommon.myCdbl(dtVLC(0)("TIP_Buffalo")) * objMilkSRNDetail.FAT * objMilkSRNDetail.ACC_Qty / 100, 2, MidpointRounding.AwayFromZero)
                            Else
                                objMilkSRNDetail.TIP_Amount = Math.Round(clsCommon.myCdbl(dtVLC(0)("TIP_Mix")) * objMilkSRNDetail.FAT * objMilkSRNDetail.ACC_Qty / 100, 2, MidpointRounding.AwayFromZero)
                            End If

                            objMilkSRNDetail.Service_Charge_Type = clsCommon.myCstr(dtVLC.Rows(0)("Service_Charge_Type"))
                            '==================Head Load==========================
                            Dim MinimumQtyForHeadLoad As Decimal = clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.MinimumQtyForHeadLoad, clsFixedParameterCode.MinimumQtyForHeadLoad, trans))
                            Dim dclDistanceKM As Decimal = clsCommon.myCdbl(dtVLC.Rows(0)("DistanceKM_Head_Load"))
                            If dclDistanceKM = 0 Then
                                dclDistanceKM = 1
                            End If

                            Dim objHeadLoad As New clsHeadLoadDCS()
                            objHeadLoad = clsHeadLoadDCS.GetDcsData(objMilkReceiptDetail.VLC_CODE, dtShiftDate, trans)
                            objMilkSRNDetail.Head_Load_Rate = objHeadLoad.Head_Load_Rate
                            If clsCommon.CompairString(clsCommon.myCstr(objHeadLoad.Head_Load_Basis), "K") = CompairStringResult.Equal Then
                                If objMilkSRNDetail.ACC_Qty >= MinimumQtyForHeadLoad Then
                                    objMilkSRNDetail.Head_Load_Amount = Math.Round(objMilkSRNDetail.ACC_Qty * objHeadLoad.Head_Load_Rate * dclDistanceKM, 2)
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(objHeadLoad.Head_Load_Basis), "L") = CompairStringResult.Equal Then
                                If objMilkReceiptDetail.LTR_WEIGHT >= MinimumQtyForHeadLoad Then
                                    objMilkSRNDetail.Head_Load_Amount = Math.Round(objMilkReceiptDetail.LTR_WEIGHT * objHeadLoad.Head_Load_Rate * dclDistanceKM, 2)
                                End If
                                'ElseIf clsCommon.CompairString(clsCommon.myCstr(objHeadLoad.Head_Load_Basis), "W") = CompairStringResult.Equal Then
                                '    qry = "select Ratio,SNF_Ratio,FAT_Pers,SNF_Pers from TSPL_MILK_PRICE_MASTER where Price_Code=(select top 1 Price_Code from TSPL_FAT_SNF_UPLOADER_MASTER where Code='" + objMilkSRNDetail.Price_Code + "')"
                                '    Dim dtTemp As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                '    If dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 0 Then
                                '        objMilkSRNDetail.FAT_KG = Math.Round(objMilkSRNDetail.ACC_Qty * objMilkSRNDetail.FAT / 100, 2)
                                '        objMilkSRNDetail.SNF_KG = Math.Round(objMilkSRNDetail.ACC_Qty * objMilkSRNDetail.SNF / 100, 2)
                                '        Dim dblFATRate As Decimal = objHeadLoad.Head_Load_Rate * clsCommon.myCdbl(dtTemp.Rows(0)("Ratio")) / clsCommon.myCdbl(dtTemp.Rows(0)("FAT_Pers"))
                                '        Dim dblSNFRate As Decimal = objHeadLoad.Head_Load_Rate * clsCommon.myCdbl(dtTemp.Rows(0)("SNF_Ratio")) / clsCommon.myCdbl(dtTemp.Rows(0)("SNF_Pers"))
                                '        objMilkSRNDetail.Head_Load_Amount = Math.Round(((objMilkSRNDetail.FAT_KG * dblFATRate) + (objMilkSRNDetail.SNF_KG * dblSNFRate)) * dclDistanceKM, 2)
                                '    End If
                            End If
                            objMilkSRNDetail.Head_Load_Type = clsCommon.myCstr(objHeadLoad.Head_Load_Basis)
                            '============================================
                            '==================Own Asset==========================
                            If clsCommon.CompairString(clsCommon.myCstr(dtVLC.Rows(0)("Service_Basis_Own_Asset")), "K") = CompairStringResult.Equal Then
                                objMilkSRNDetail.Own_Asset_Amount = Math.Round(objMilkSRNDetail.ACC_Qty * objMilkSRNDetail.Own_Asset_Rate, 2)
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(dtVLC.Rows(0)("Service_Basis_Own_Asset")), "L") = CompairStringResult.Equal Then
                                objMilkSRNDetail.Own_Asset_Amount = Math.Round(objMilkSRNDetail.MILK_Qty * objMilkSRNDetail.Own_Asset_Rate, 2)
                            End If
                            objMilkSRNDetail.Own_Asset_Type = clsCommon.myCstr(dtVLC.Rows(0)("Service_Basis_Own_Asset"))
                            '============================================
                            objMilkSRNDetail.Service_Charge_Amount = Math.Round(objMilkSRNDetail.MILK_Qty * clsCommon.myCdbl(dtVLC.Rows(0)("Service_Charge_Per_Unit")), 2)
                            objMilkSRNDetail.NET_AMOUNT = Math.Round(objMilkSRNDetail.AMOUNT + objMilkSRNDetail.Emp_Amount + objMilkSRNDetail.TIP_Amount - objMilkSRNDetail.Service_Charge_Amount, 2)
                            If IsRoundOffPaiseAmount Then
                                objMilkSRNDetail.Round_Off = (objMilkSRNDetail.NET_AMOUNT Mod 1)
                                objMilkSRNDetail.NET_AMOUNT = objMilkSRNDetail.NET_AMOUNT - (objMilkSRNDetail.NET_AMOUNT Mod 1)
                            End If
                            objMilkSRNDetail.COMM_PORT = ""
                            Dim arrMilkSRNDetail As New List(Of clsMilkSRNMCCDetail)
                            arrMilkSRNDetail.Add(objMilkSRNDetail)

                            Dim objVSPChargeList As New List(Of clsMilkSRNVSpChargeDetail)
                            '============VSp Charge Detail=====================
                            For Each row_VSP_Charge As DataRow In DtVSPChargeDetail.Select("Vsp_Code='" & objMilkSRNHead.VSP_CODE & "'")
                                Dim objVSP_Charge1 As New clsMilkSRNVSpChargeDetail()
                                objVSP_Charge1.Vsp_Code = clsCommon.myCstr(objMilkSRNHead.VSP_CODE)
                                objVSP_Charge1.Vlc_Doc_Code = objMilkReceiptDetail.VLC_DOC_CODE
                                objVSP_Charge1.Charge_Code = clsCommon.myCstr(row_VSP_Charge("Charge_Code"))
                                objVSP_Charge1.Charge_Rate = clsCommon.myCstr(row_VSP_Charge("Rate"))
                                objVSP_Charge1.Service_Type = clsCommon.myCstr(dtVLC.Rows(0)("Service_Charge_Type"))
                                If clsCommon.CompairString(objVSP_Charge1.Service_Type, "%(Percentage)") = CompairStringResult.Equal Then
                                    objVSP_Charge1.AMOUNT = Math.Round(objMilkSRNDetail.AMOUNT * objVSP_Charge1.Charge_Rate / 100, 2)
                                ElseIf clsCommon.CompairString(objVSP_Charge1.Service_Type, "Rate/Kg") = CompairStringResult.Equal Then
                                    objVSP_Charge1.AMOUNT = Math.Round(objMilkSRNDetail.ACC_Qty * objVSP_Charge1.Charge_Rate, 2)
                                ElseIf clsCommon.CompairString(objVSP_Charge1.Service_Type, "Rate/Ltr") = CompairStringResult.Equal And clsCommon.CompairString(objMilkReceiptDetail.UOM_Code, "LTR") = CompairStringResult.Equal Then
                                    objVSP_Charge1.AMOUNT = Math.Round(objMilkSRNDetail.MILK_Qty * objVSP_Charge1.Charge_Rate, 2)
                                End If
                                objVSPChargeList.Add(objVSP_Charge1)
                            Next
                            '===========================================


                            '============Price Charge Detail=====================
                            Dim objPriceChargeList As New List(Of clsMilkSRNPriceChargeDetail)
                            For Each row_Price_Charge As DataRow In DtPriceChargeDetail.Select("Price_Code='" & objMilkSRNDetail.Price_Code & "'")
                                Dim objPrice_Charge1 As New clsMilkSRNPriceChargeDetail()
                                objPrice_Charge1.Price_Code = objMilkSampleDetail.Price_Code
                                objPrice_Charge1.Vlc_Doc_Code = objMilkReceiptDetail.VLC_DOC_CODE
                                objPrice_Charge1.Charge_Code = clsCommon.myCstr(row_Price_Charge("Charge_Code"))
                                objPrice_Charge1.Charge_Rate = clsCommon.myCstr(row_Price_Charge("Rate"))
                                objPrice_Charge1.Service_type = clsCommon.myCstr(dtVLC.Rows(0)("Service_Charge_Type"))
                                If clsCommon.CompairString(objPrice_Charge1.Service_type, "%(Percentage)") = CompairStringResult.Equal Then
                                    objPrice_Charge1.AMOUNT = Math.Round(objMilkSRNDetail.AMOUNT * objPrice_Charge1.Charge_Rate / 100, 2)
                                ElseIf clsCommon.CompairString(objPrice_Charge1.Service_type, "Rate/Kg") = CompairStringResult.Equal Then
                                    objPrice_Charge1.AMOUNT = Math.Round(objMilkSRNDetail.ACC_Qty * objPrice_Charge1.Charge_Rate, 2)
                                ElseIf clsCommon.CompairString(objPrice_Charge1.Service_type, "Rate/Ltr") = CompairStringResult.Equal And clsCommon.CompairString(objMilkReceiptDetail.UOM_Code, "LTR") = CompairStringResult.Equal Then
                                    objPrice_Charge1.AMOUNT = Math.Round(objMilkSRNDetail.MILK_Qty * objPrice_Charge1.Charge_Rate, 2)
                                End If
                                objPriceChargeList.Add(objPrice_Charge1)
                            Next
                            '===========================================
                            clsMilkSRNMCC.SaveData(objMilkSRNHead, arrMilkSRNDetail, trans)
                            ''Milk SRN Save End
                            objtr = Nothing
                        Next
                    End If
                    If isCreateNewDocOFMilkReceiptAndSample Then
                        qry = "update TSPL_MILK_RECEIPT_HEAD set Posted=1 where DOC_CODE='" + MilkReceiptNo + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        qry = "update TSPL_MILK_SAMPLE_HEAD set Posted=1,Posting_Date='" + strShiftDate + "' where DOC_CODE='" + MilkSampleNo + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    End If
                Next
            End If
            ''BHA/27/12/18-000764 by balwinder on 02/01/2019
            If arrShiftEndCode IsNot Nothing AndAlso arrShiftEndCode.Count > 0 Then
                Dim settSeprateDistanceMorningEveningShift As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SeprateDistanceMorningEveningShift, clsFixedParameterCode.SeprateDistanceMorningEveningShift, trans)) > 0, True, False)
                For ii As Integer = 0 To arrShiftEndCode.Count - 1
                    clsMilkShiftEndMCC.CreateMilkShiftEntrRouteDetailsWithProvision(arrShiftEndCode(ii), settSeprateDistanceMorningEveningShift, False, trans)
                Next
                clsMilkShiftEndMCC.RecreateConsumptionEntry(arrShiftEndCode, trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        arrShiftEndCode = Nothing
        Return True
    End Function

    Public Shared Function MilkProcurementUploader(ByVal obj As clsMilkProcurementUploaderHead, ByVal trans As SqlTransaction) As Boolean
        Dim DtVSPChargeDetail As DataTable = clsDBFuncationality.GetDataTable("SELECT * FROM  TSPL_MCC_VSP_ChargeCategory_MAPPING ", trans)
        Dim DtPriceChargeDetail As DataTable = clsDBFuncationality.GetDataTable("SELECT * FROM  TSPL_FAT_SNF_UPLOADER_Chart_Detail ", trans)
        Dim dblEmptyCanWeight As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.EmptyCanWeight, clsFixedParameterCode.EmptyCanWeight, trans))
        Dim dblMinuteInLastVehicleForGateEntry As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MinuteInLastVehicleForGateEntry, clsFixedParameterCode.MinuteInLastVehicleForGateEntry, trans))
        Dim dblMinuteGateEntryToGrossWeight As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MinuteGateEntryToGrossWeight, clsFixedParameterCode.MinuteGateEntryToGrossWeight, trans))
        Dim dblMinuteGrossWeightToTareWeight As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MinuteGrossWeightToTareWeight, clsFixedParameterCode.MinuteGrossWeightToTareWeight, trans))
        Dim CreateNewDocumentOnUploader As Boolean = clsFixedParameter.GetData(clsFixedParameterType.CreateNewDocumentOnUploader, clsFixedParameterCode.CreateNewDocumentOnUploader, trans) = 1
        Dim WeighmentNotMandatoryInMCC As Boolean = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.WeighmentNotMandatoryInMCC, clsFixedParameterCode.WeighmentNotMandatoryInMCC, trans)) = 1
        Dim IsRoundOffPaiseAmount As Boolean = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RoundOffPaiseAmount, clsFixedParameterCode.RoundOffPaiseAmount, trans)) = 1
        Dim isPickCLRInsteadOfSNF As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProcuremntPickCLRInsteadOfSNF, clsFixedParameterCode.MilkProcuremntPickCLRInsteadOfSNF, trans)) > 0)
        Dim PickPriceFromFATAndSNF As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProcuremntPickCLRInsteadOfSNF, clsFixedParameterCode.PickPriceFromFATAndSNF, trans)) > 0)
        Dim settMilkCollectionPickBulkRoute As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkCollectionPickBulkRoute, clsFixedParameterCode.MilkCollectionPickBulkRoute, trans)) = 1)
        Dim arrShiftEndCode As New ArrayList
        Try
            Dim qry As String
            Dim dt As DataTable

            Dim corrFactor As Double = clsFixedParameter.GetData(clsFixedParameterType.defaultCorrectionFactor, clsFixedParameterCode.MilkSetting, trans)
            qry = "select Shift_Date,Shift,"
            If objCommonVar.DisplayTypeInMilkReceipt Then
                qry += "'M' as Dock_Collection_Milk_Type"
            Else
                qry += "Dock_Collection_Milk_Type "
            End If
            qry += " from TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL where Document_No='" + obj.Document_No + "' group by Shift_Date,Shift"
            If Not objCommonVar.DisplayTypeInMilkReceipt Then
                qry += ",Dock_Collection_Milk_Type "
            End If
            qry += " order by Shift_Date,Shift desc,Dock_Collection_Milk_Type"
            Dim dtDateShift As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dtDateShift IsNot Nothing AndAlso dtDateShift.Rows.Count > 0 Then
                Dim strICode = clsFixedParameter.GetData(clsFixedParameterType.MCCDefaultMilkItem, clsFixedParameterCode.MilkSetting, trans)
                For Each drDateShift As DataRow In dtDateShift.Rows
                    Dim strShift As String = clsCommon.myCstr(drDateShift("Shift"))
                    Dim strShiftDate As String = clsCommon.GetPrintDate(clsCommon.myCDate(drDateShift("Shift_Date")), "dd/MMM/yyyy")
                    Dim dtShiftDate As DateTime = clsCommon.myCDate(drDateShift("Shift_Date"))
                    Dim strDockCollectionMilkType As String = clsCommon.myCstr(drDateShift("Dock_Collection_Milk_Type"))

                    Dim arr As List(Of clsMilkProcurementUploaderDetail) = clsMilkProcurementUploaderDetail.GetData(obj.Document_No, "  Shift_Date='" + strShiftDate + "' and Shift='" + strShift + "' " + IIf(objCommonVar.DisplayTypeInMilkReceipt, "", " and Dock_Collection_Milk_Type='" + strDockCollectionMilkType + "'"), trans)
                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
                        For ii As Integer = 0 To arr.Count - 1
                            Dim objtr As clsMilkProcurementUploaderDetail = arr(ii)
                            qry = " select TSPL_Primary_Vehicle_Master.Vehicle_Weight,TSPL_VLC_MASTER_HEAD.Route_Code,TSPL_VLC_MASTER_HEAD.VSP_Code,TSPL_MCC_ROUTE_MASTER.Vehicle_Code,TSPL_VENDOR_MASTER.EMP_Type,TSPL_VENDOR_MASTER.EMP_Fixed_Amount ,TSPL_VENDOR_MASTER.Actual_charges_Slab,TSPL_VENDOR_MASTER.Actual_charges,TSPL_VENDOR_MASTER.Actual_charges_Slab2,TSPL_VENDOR_MASTER.Actual_charges2,TSPL_VENDOR_MASTER.Actual_charges_Slab3,TSPL_VENDOR_MASTER.Actual_charges3,TSPL_VENDOR_MASTER.Actual_charges_Slab4,TSPL_VENDOR_MASTER.Actual_charges4 ,TSPL_VENDOR_MASTER.Actual_charges_Slab5,TSPL_VENDOR_MASTER.Actual_charges5,TSPL_VENDOR_MASTER.Service_Charge_Per_Unit,coalesce(TSPL_VENDOR_MASTER.Rate_Head_Load,0) as Rate_Head_Load,coalesce(TSPL_VENDOR_MASTER.Rate_Own_Asset,0) as Rate_Own_Asset,TSPL_VENDOR_MASTER.Service_Basis_Head_Load,TSPL_VENDOR_MASTER.Service_Basis_Own_Asset,TSPL_Primary_Vehicle_Master.Vendor_Code as TransporterCode,TSPL_VENDOR_MASTER.Service_Charge_Type,TSPL_VENDOR_MASTER.TIP_Buffalo,TSPL_VENDOR_MASTER.TIP_Cow,TSPL_VENDOR_MASTER.TIP_Mix,TSPL_VLC_MASTER_HEAD.Milk_Receive_UOM,TSPL_VENDOR_MASTER.DistanceKM_Head_Load " + Environment.NewLine +
                                " from TSPL_VLC_MASTER_HEAD " + Environment.NewLine +
                                " left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_VLC_MASTER_HEAD.Route_Code " + Environment.NewLine +
                                " left join TSPL_VENDOR_MASTER on   TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_CODE " + Environment.NewLine +
                                " left outer join TSPL_Primary_Vehicle_Master on TSPL_Primary_Vehicle_Master.Vehicle_Code=TSPL_MCC_ROUTE_MASTER.Vehicle_Code " + Environment.NewLine +
                                " where TSPL_VLC_MASTER_HEAD.VLC_Code='" + objtr.VLC_Code + "'"
                            Dim dtVLC As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                            If dtVLC Is Nothing OrElse dtVLC.Rows.Count <= 0 Then
                                Throw New Exception("VLC Code not found :" + objtr.VLC_Code)
                            End If

                            If objtr.Against_Milk_Collection_DCS_Detail > 0 OrElse settMilkCollectionPickBulkRoute Then
                                Dim dttemp As DataTable = clsMilkCollectionDCS.GetRouteDetails(objtr.Against_Milk_Collection_DCS_Detail, trans, settMilkCollectionPickBulkRoute, objtr.Bulk_Route_Code)
                                If dttemp IsNot Nothing AndAlso dttemp.Rows.Count > 0 Then
                                    If clsCommon.myLen(dtVLC.Rows(0)("Route_Code")) <= 0 Then
                                        dtVLC.Rows(0)("Route_Code") = dttemp.Rows(0)("Route_Code")
                                    End If
                                    If clsCommon.myLen(dtVLC.Rows(0)("TransporterCode")) <= 0 Then
                                        dtVLC.Rows(0)("TransporterCode") = dttemp.Rows(0)("Tanker_Transporter_Code")
                                    End If
                                    If clsCommon.myLen(dtVLC.Rows(0)("Vehicle_Code")) <= 0 Then
                                        dtVLC.Rows(0)("Vehicle_Code") = dttemp.Rows(0)("Vehicle_No")
                                    End If
                                    dtVLC.AcceptChanges()
                                End If
                            End If

                            If clsCommon.myLen(dtVLC.Rows(0)("Route_Code")) <= 0 Then
                                Throw New Exception("Route not defined for VLC Code [" + objtr.VLC_Code + "]")
                            End If
                            If clsCommon.myLen(dtVLC.Rows(0)("TransporterCode")) <= 0 Then
                                Throw New Exception("Transporter not defined for VLC Code [" + objtr.VLC_Code + "]")
                            End If

                            ''Check no purhcase invoice found for that day if found means bills are generated
                            qry = "select distinct TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE 
from  TSPL_MILK_PURCHASE_INVOICE_DETAIL 
left outer join TSPL_MILK_PURCHASE_INVOICE_HEAD on TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE=TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE 
left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE 
where TSPL_MILK_SRN_HEAD.MCC_CODE='" + obj.MCC_Code + "' and TSPL_MILK_SRN_HEAD.SHIFT='" + strShift + "' and convert(date, TSPL_MILK_SRN_HEAD.DOC_DATE,103)=convert(date, '" + strShiftDate + "',103) and TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE='" + clsCommon.myCstr(dtVLC.Rows(0)("VSP_Code")) + "'"
                            dt = clsDBFuncationality.GetDataTable(qry, trans)
                            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                Throw New Exception("Milk Purchase invoice :" + clsCommon.myCstr(dt.Rows(0)("DOC_CODE")) + " created for VSP:" + clsCommon.myCstr(dtVLC.Rows(0)("VSP_Code")) + " .For Payment Cycle Date :" + strShiftDate + " Shift :" + strShift)
                            End If

                            qry = "select UOM_Code from TSPL_MCC_UOM_DETAIL where stocking_unit='Y' and MCC_CODE='" & obj.MCC_Code & "' "
                            Dim Unit_Code As String = clsDBFuncationality.getSingleValue(qry, trans)
                            If Unit_Code = "" Then
                                Throw New Exception("Fill UOM of Mcc" + obj.MCC_Code)
                            End If
                            qry = "select UOM_Code from TSPL_Item_UOM_DETAIL where Item_CODE='" & strICode & "' and UOM_code='" & Unit_Code & "' "
                            Dim Item_Unit_Code As String = clsDBFuncationality.getSingleValue(qry, trans)
                            If Item_Unit_Code = "" Then
                                Throw New Exception("Fill " & Unit_Code & " UOM of Item " + strICode)
                            End If
                            Dim conv_fac As Double = clsWeightConversionInfo.GetConversion_factor(Unit_Code, IIf(clsCommon.CompairString(Unit_Code, "KG") = CompairStringResult.Equal, "LTR", "KG"), trans)

                            ''Milk SRN Save
                            Dim objMilkSRNHead As clsMilkSRNMCC = New clsMilkSRNMCC
                            'objMilkSRNHead.MILK_SAMPLE_CODE = objtr.SNo
                            objMilkSRNHead.DOC_DATE = dtShiftDate
                            objMilkSRNHead.SHIFT = strShift
                            objMilkSRNHead.COMM_PORT = ""
                            objMilkSRNHead.MCC_CODE = obj.MCC_Code
                            objMilkSRNHead.SAMPLE_NO = objtr.SNo
                            objMilkSRNHead.VLC_DOC_CODE = objtr.VLC_Code
                            objMilkSRNHead.VEHICLE_CODE = clsCommon.myCstr(dtVLC.Rows(0)("Vehicle_Code"))
                            objMilkSRNHead.VLC_CODE = objtr.VLC_Code
                            objMilkSRNHead.ROUTE_CODE = clsCommon.myCstr(dtVLC.Rows(0)("Route_Code"))
                            objMilkSRNHead.VSP_CODE = clsCommon.myCstr(dtVLC.Rows(0)("VSP_Code"))
                            objMilkSRNHead.TransPorter = clsCommon.myCstr(dtVLC.Rows(0)("TransporterCode"))
                            objMilkSRNHead.Dock_Collection_Milk_Type = strDockCollectionMilkType
                            objMilkSRNHead.Against_Uploader_TR_No = objtr.TR_No

                            Dim objMilkSRNDetail As clsMilkSRNMCCDetail = New clsMilkSRNMCCDetail()
                            objMilkSRNDetail.Item_CODE = strICode
                            objMilkSRNDetail.MILK_Qty = objtr.Milk_Weight

                            Dim Unit_CodeApply As String = Unit_Code
                            Dim conv_facApply As String = conv_fac
                            If clsCommon.myLen(clsCommon.myCstr(dtVLC.Rows(0)("Milk_Receive_UOM"))) > 0 Then ''BHO/11/06/21-000004 By balwinder on 17/06/2021
                                Unit_CodeApply = clsCommon.myCstr(dtVLC.Rows(0)("Milk_Receive_UOM"))
                                conv_facApply = clsWeightConversionInfo.GetConversion_factor(Unit_CodeApply, IIf(clsCommon.CompairString(Unit_CodeApply, "KG") = CompairStringResult.Equal, "LTR", "KG"), trans)
                            End If

                            If clsCommon.CompairString(Unit_CodeApply, "KG") = CompairStringResult.Equal Then
                                objMilkSRNDetail.ACC_Qty = clsCommon.myCdbl(objtr.Milk_Weight)
                                objMilkSRNDetail.ACC_Qty_LTR = clsCommon.myCdbl(objtr.Milk_Weight * conv_facApply)
                            Else
                                objMilkSRNDetail.ACC_Qty_LTR = clsCommon.myCdbl(objtr.Milk_Weight)
                                objMilkSRNDetail.ACC_Qty = clsCommon.myCdbl(objtr.Milk_Weight * conv_facApply)
                            End If
                            objMilkSRNDetail.FAT = objtr.FAT
                            If isPickCLRInsteadOfSNF Then
                                objMilkSRNDetail.CLR = Math.Truncate(objtr.SNF * 10) / 10
                                objMilkSRNDetail.SNF = clsEkoPro.getSnfOnCalculation(objMilkSRNDetail.FAT, objMilkSRNDetail.CLR, corrFactor)
                                If PickPriceFromFATAndSNF Then
                                    objMilkSRNDetail.SNF = clsCommon.myRoundOFF(objMilkSRNDetail.SNF, 1, 4)
                                    objMilkSRNDetail.RATE = clsEkoPro.getRateAndPriceCodeFromUploaderShiftWise(objMilkSRNDetail.MILK_Qty, objMilkSRNDetail.Price_Code, objMilkSRNDetail.FAT, objMilkSRNDetail.SNF, obj.MCC_Code, objtr.VLC_Code, objtr.Shift, dtShiftDate, trans, strDockCollectionMilkType)
                                Else
                                    objMilkSRNDetail.SNF = clsCommon.myRoundOFF(objMilkSRNDetail.SNF, 2, 9)
                                    objMilkSRNDetail.RATE = clsEkoPro.getRateFromUploaderShiftWiseCLR(objMilkSRNDetail.FAT, objMilkSRNDetail.CLR, obj.MCC_Code, objtr.VLC_Code, objtr.Shift, dtShiftDate, trans, strDockCollectionMilkType, objMilkSRNDetail.Price_Code)
                                End If
                            Else
                                objMilkSRNDetail.SNF = objtr.SNF
                                objMilkSRNDetail.CLR = clsEkoPro.getClrOnCalculation(objMilkSRNDetail.FAT, objMilkSRNDetail.SNF, corrFactor)
                                objMilkSRNDetail.RATE = clsEkoPro.getRateAndPriceCodeFromUploaderShiftWise(objMilkSRNDetail.MILK_Qty, objMilkSRNDetail.Price_Code, objMilkSRNDetail.FAT, objMilkSRNDetail.SNF, obj.MCC_Code, objtr.VLC_Code, objtr.Shift, dtShiftDate, trans, strDockCollectionMilkType)
                            End If
                            objMilkSRNDetail.MCC_CODE = obj.MCC_Code
                            objMilkSRNDetail.Correction_Factor = corrFactor
                            objMilkSRNDetail.UOM = Unit_CodeApply
                            objMilkSRNDetail.AMOUNT = Math.Round(clsCommon.myCdbl(objMilkSRNDetail.RATE * objMilkSRNDetail.MILK_Qty), 2, MidpointRounding.AwayFromZero)
                            objMilkSRNDetail.Own_Asset_Rate = clsCommon.myCdbl(dtVLC.Rows(0)("Rate_Own_Asset"))
                            objMilkSRNDetail.Commission = 0 ' because nature is always E and it is never C 'clsCommon.myCdbl(dr(0)("Actual_charges"))
                            objMilkSRNDetail.Commission_Amount = Math.Round(objMilkSRNDetail.AMOUNT * objMilkSRNDetail.Commission / 100, 2)
                            objMilkSRNDetail.Std_Qty = clsInventoryMovementNew.GetStdQty(trans, Math.Round(objMilkSRNDetail.ACC_Qty * objMilkSRNDetail.FAT / 100, 2), Math.Round(objMilkSRNDetail.ACC_Qty * objMilkSRNDetail.SNF / 100, 2), objMilkSRNHead.DOC_DATE)
                            If clsCommon.myLen(objtr.Reject_Type) > 0 Then
                                Dim objMRT As clsMilkRejectType = clsMilkRejectType.GetData(objtr.Reject_Type, NavigatorType.Current, trans)
                                If objMRT Is Nothing Then
                                    Throw New Exception("Invalid rejection type [" + objtr.Reject_Type + "]")
                                End If
                                objMilkSRNDetail.Item_CODE = objMRT.Item_Code
                                Dim dclRate As Decimal = objMilkSRNDetail.RATE
                                Dim dclAmt As Decimal = 0
                                Dim CalKG As Decimal = 0
                                If objMRT.Applicable_On = 1 Then  ''RAte
                                    dclRate = objMRT.Applicable_Per
                                    dclAmt = Math.Round((dclRate * objtr.Milk_Weight), 2, MidpointRounding.ToEven)
                                ElseIf objMRT.Applicable_On = 2 Then  ''FAT KG RAte
                                    CalKG = (objtr.Milk_Weight * objtr.FAT) / 100
                                    dclAmt = Math.Round((objMRT.Applicable_Per * CalKG), 2, MidpointRounding.ToEven)
                                    dclRate = clsCommon.myCDivide(dclAmt, objtr.Milk_Weight)
                                ElseIf objMRT.Applicable_On = 3 Then  ''SNF KG RAte
                                    CalKG = (objtr.Milk_Weight * objtr.SNF) / 100
                                    dclAmt = Math.Round((objMRT.Applicable_Per * CalKG), 2, MidpointRounding.ToEven)
                                    dclRate = clsCommon.myCDivide(dclAmt, objtr.Milk_Weight)
                                Else ''%Age
                                    dclRate = Math.Round(dclRate * objMRT.Applicable_Per / 100, 2, MidpointRounding.ToEven)
                                    dclAmt = Math.Round((dclRate * objtr.Milk_Weight), 2, MidpointRounding.ToEven)
                                End If
                                objMilkSRNDetail.RATE = dclRate
                                objMilkSRNDetail.AMOUNT = dclAmt
                            End If
                            If clsCommon.CompairString(clsCommon.myCstr(dtVLC.Rows(0)("EMP_Type")), "FP") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(dtVLC.Rows(0)("EMP_Type")), "FAFP") = CompairStringResult.Equal Then
                                objMilkSRNDetail.Payment_Commission = clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges"))
                                If clsCommon.CompairString(clsCommon.myCstr(dtVLC.Rows(0)("Service_Charge_Type")), "%(Percentage)") = CompairStringResult.Equal Then
                                    objMilkSRNDetail.Emp_Amount = Math.Round(objMilkSRNDetail.AMOUNT * objMilkSRNDetail.Payment_Commission / 100, 2)
                                ElseIf clsCommon.CompairString(clsCommon.myCstr(dtVLC.Rows(0)("Service_Charge_Type")), "Rate/Kg") = CompairStringResult.Equal Then
                                    objMilkSRNDetail.Emp_Amount = Math.Round(objMilkSRNDetail.ACC_Qty * objMilkSRNDetail.Payment_Commission, 2)
                                ElseIf clsCommon.CompairString(clsCommon.myCstr(dtVLC.Rows(0)("Service_Charge_Type")), "Rate/Ltr") = CompairStringResult.Equal Then
                                    objMilkSRNDetail.Emp_Amount = Math.Round(objMilkSRNDetail.MILK_Qty * objMilkSRNDetail.Payment_Commission, 2)
                                End If
                                If clsCommon.CompairString(clsCommon.myCstr(dtVLC.Rows(0)("EMP_Type")), "FAFP") = CompairStringResult.Equal Then
                                    objMilkSRNDetail.Emp_Amount += clsCommon.myCdbl(dtVLC.Rows(0)("EMP_Fixed_Amount"))
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(dtVLC.Rows(0)("EMP_Type")), "SWP") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(dtVLC.Rows(0)("EMP_Type")), "FASWP") = CompairStringResult.Equal Then
                                If clsCommon.CompairString(clsCommon.myCstr(dtVLC.Rows(0)("Service_Charge_Type")), "%(Percentage)") = CompairStringResult.Equal Then
                                    If clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab5")) > 0 AndAlso objMilkSRNDetail.AMOUNT >= clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab5")) Then
                                        objMilkSRNDetail.Payment_Commission = clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges5"))
                                    ElseIf clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab4")) > 0 AndAlso objMilkSRNDetail.AMOUNT >= clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab4")) Then
                                        objMilkSRNDetail.Payment_Commission = clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges4"))
                                    ElseIf clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab3")) > 0 AndAlso objMilkSRNDetail.AMOUNT >= clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab3")) Then
                                        objMilkSRNDetail.Payment_Commission = clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges3"))
                                    ElseIf clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab2")) > 0 AndAlso objMilkSRNDetail.AMOUNT >= clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab2")) Then
                                        objMilkSRNDetail.Payment_Commission = clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges2"))
                                    Else
                                        objMilkSRNDetail.Payment_Commission = clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges"))
                                    End If
                                    objMilkSRNDetail.Emp_Amount = Math.Round(objMilkSRNDetail.AMOUNT * objMilkSRNDetail.Payment_Commission / 100, 2)
                                ElseIf clsCommon.CompairString(clsCommon.myCstr(dtVLC.Rows(0)("Service_Charge_Type")), "Rate/Kg") = CompairStringResult.Equal Then
                                    If clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab5")) > 0 AndAlso objMilkSRNDetail.ACC_Qty >= clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab5")) Then
                                        objMilkSRNDetail.Payment_Commission = clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges5"))
                                    ElseIf clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab4")) > 0 AndAlso objMilkSRNDetail.ACC_Qty >= clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab4")) Then
                                        objMilkSRNDetail.Payment_Commission = clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges4"))
                                    ElseIf clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab3")) > 0 AndAlso objMilkSRNDetail.ACC_Qty >= clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab3")) Then
                                        objMilkSRNDetail.Payment_Commission = clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges3"))
                                    ElseIf clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab2")) > 0 AndAlso objMilkSRNDetail.ACC_Qty >= clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab2")) Then
                                        objMilkSRNDetail.Payment_Commission = clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges2"))
                                    Else
                                        objMilkSRNDetail.Payment_Commission = clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges"))
                                    End If
                                    objMilkSRNDetail.Emp_Amount = Math.Round(objMilkSRNDetail.ACC_Qty * objMilkSRNDetail.Payment_Commission, 2)
                                ElseIf clsCommon.CompairString(clsCommon.myCstr(dtVLC.Rows(0)("Service_Charge_Type")), "Rate/Ltr") = CompairStringResult.Equal Then
                                    If clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab5")) > 0 AndAlso objMilkSRNDetail.MILK_Qty >= clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab5")) Then
                                        objMilkSRNDetail.Payment_Commission = clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges5"))
                                    ElseIf clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab4")) > 0 AndAlso objMilkSRNDetail.MILK_Qty >= clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab4")) Then
                                        objMilkSRNDetail.Payment_Commission = clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges4"))
                                    ElseIf clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab3")) > 0 AndAlso objMilkSRNDetail.MILK_Qty >= clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab3")) Then
                                        objMilkSRNDetail.Payment_Commission = clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges3"))
                                    ElseIf clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab2")) > 0 AndAlso objMilkSRNDetail.MILK_Qty >= clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab2")) Then
                                        objMilkSRNDetail.Payment_Commission = clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges2"))
                                    Else
                                        objMilkSRNDetail.Payment_Commission = clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges"))
                                    End If
                                    objMilkSRNDetail.Emp_Amount = Math.Round(objMilkSRNDetail.MILK_Qty * objMilkSRNDetail.Payment_Commission, 2)
                                End If
                                If clsCommon.CompairString(clsCommon.myCstr(dtVLC.Rows(0)("EMP_Type")), "FASWP") = CompairStringResult.Equal Then
                                    objMilkSRNDetail.Emp_Amount += clsCommon.myCdbl(dtVLC.Rows(0)("EMP_Fixed_Amount"))
                                End If

                            ElseIf clsCommon.CompairString(clsCommon.myCstr(dtVLC(0)("EMP_Type")), "FPSP") = CompairStringResult.Equal Then
                                objMilkSRNDetail.Payment_Commission = clsCommon.myCdbl(dtVLC(0)("Actual_charges"))
                                Dim objSPR As clsStandardPrice = clsStandardPrice.GetStandartPrice(objMilkSRNDetail.Price_Code, trans)
                                If objSPR IsNot Nothing Then
                                    If (objSPR.Std_Percent_FAT <> 0 AndAlso objSPR.Std_Percent_SNF <> 0) Then
                                        If clsCommon.CompairString(clsCommon.myCstr(dtVLC(0)("Service_Charge_Type")), "Rate/Kg") = CompairStringResult.Equal Then
                                            objMilkSRNDetail.Emp_Amount = Math.Round((Math.Round(objMilkSRNDetail.ACC_Qty * objMilkSRNDetail.FAT / 100, 3) * objMilkSRNDetail.Payment_Commission * objSPR.Weightage_FAT / objSPR.Std_Percent_FAT) + (Math.Round(objMilkSRNDetail.ACC_Qty * objMilkSRNDetail.SNF / 100, 3) * objMilkSRNDetail.Payment_Commission * objSPR.Weightage_SNF / objSPR.Std_Percent_SNF), 2)
                                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dtVLC(0)("Service_Charge_Type")), "Rate/Ltr") = CompairStringResult.Equal Then
                                            Dim qty As Decimal = objMilkSRNDetail.ACC_Qty
                                            If conv_facApply <> 0 Then
                                                qty = objMilkSRNDetail.ACC_Qty / conv_facApply
                                            End If
                                            objMilkSRNDetail.Emp_Amount = Math.Round((Math.Round(qty * objMilkSRNDetail.FAT / 100, 3) * objMilkSRNDetail.Payment_Commission * objSPR.Weightage_FAT / objSPR.Std_Percent_FAT) + (Math.Round(qty * objMilkSRNDetail.SNF / 100, 3) * objMilkSRNDetail.Payment_Commission * objSPR.Weightage_SNF / objSPR.Std_Percent_SNF), 2)
                                        Else
                                            objMilkSRNDetail.Emp_Amount = 0
                                            'Throw New Exception("EMP Service Basis is Not valid of VSP " + objMilkSRNDetail.VlC_Code)
                                        End If
                                    End If
                                End If
                            Else
                                Throw New Exception("EMP Type is Not a valid")
                            End If

                            If clsCommon.CompairString(strDockCollectionMilkType, "C") = CompairStringResult.Equal Then
                                objMilkSRNDetail.TIP_Amount = Math.Round(clsCommon.myCdbl(dtVLC(0)("TIP_Cow")) * (objMilkSRNDetail.FAT + objMilkSRNDetail.SNF) * objMilkSRNDetail.ACC_Qty / 100, 2, MidpointRounding.AwayFromZero)
                            ElseIf clsCommon.CompairString(strDockCollectionMilkType, "B") = CompairStringResult.Equal Then
                                objMilkSRNDetail.TIP_Amount = Math.Round(clsCommon.myCdbl(dtVLC(0)("TIP_Buffalo")) * objMilkSRNDetail.FAT * objMilkSRNDetail.ACC_Qty / 100, 2, MidpointRounding.AwayFromZero)
                            Else
                                objMilkSRNDetail.TIP_Amount = Math.Round(clsCommon.myCdbl(dtVLC(0)("TIP_Mix")) * objMilkSRNDetail.FAT * objMilkSRNDetail.ACC_Qty / 100, 2, MidpointRounding.AwayFromZero)
                            End If

                            objMilkSRNDetail.Service_Charge_Type = clsCommon.myCstr(dtVLC.Rows(0)("Service_Charge_Type"))
                            '==================Head Load==========================
                            Dim MinimumQtyForHeadLoad As Decimal = clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.MinimumQtyForHeadLoad, clsFixedParameterCode.MinimumQtyForHeadLoad, trans))
                            Dim dclDistanceKM As Decimal = clsCommon.myCdbl(dtVLC.Rows(0)("DistanceKM_Head_Load"))
                            If dclDistanceKM = 0 Then
                                dclDistanceKM = 1
                            End If
                            Dim ExcluetHeadLoad As Boolean = False
                            If clsCommon.myLen(objtr.Reject_Type) > 0 Then
                                qry = "select ISNULL(Exclude_Head,0) as Exclude_Head from TSPL_MILK_REJECT_TYPE where Code='" + objtr.Reject_Type + "' "
                                ExcluetHeadLoad = (clsCommon.myCDecimal(clsDBFuncationality.getSingleValue(qry, trans)) = 1)
                            End If
                            If ExcluetHeadLoad Then
                                objMilkSRNDetail.Head_Load_Rate = 0
                                objMilkSRNDetail.Head_Load_Amount = 0
                                objMilkSRNDetail.Head_Load_Type = ""
                            Else
                                Dim objHeadLoad As New clsHeadLoadDCS()
                                objHeadLoad = clsHeadLoadDCS.GetDcsData(objMilkSRNHead.VLC_CODE, dtShiftDate, trans)
                                objMilkSRNDetail.Head_Load_Rate = objHeadLoad.Head_Load_Rate
                                If clsCommon.CompairString(clsCommon.myCstr(objHeadLoad.Head_Load_Basis), "K") = CompairStringResult.Equal Then
                                    If objMilkSRNDetail.ACC_Qty >= MinimumQtyForHeadLoad Then
                                        objMilkSRNDetail.Head_Load_Amount = Math.Round(objMilkSRNDetail.ACC_Qty * objHeadLoad.Head_Load_Rate * dclDistanceKM, 2)
                                    End If
                                ElseIf clsCommon.CompairString(clsCommon.myCstr(objHeadLoad.Head_Load_Basis), "L") = CompairStringResult.Equal Then
                                    If objMilkSRNDetail.ACC_Qty_LTR >= MinimumQtyForHeadLoad Then
                                        objMilkSRNDetail.Head_Load_Amount = Math.Round(objMilkSRNDetail.ACC_Qty_LTR * objHeadLoad.Head_Load_Rate * dclDistanceKM, 2)
                                    End If
                                End If
                                objMilkSRNDetail.Head_Load_Type = clsCommon.myCstr(objHeadLoad.Head_Load_Basis)
                            End If

                            '============================================
                            '==================Own Asset==========================
                            If clsCommon.CompairString(clsCommon.myCstr(dtVLC.Rows(0)("Service_Basis_Own_Asset")), "K") = CompairStringResult.Equal Then
                                objMilkSRNDetail.Own_Asset_Amount = Math.Round(objMilkSRNDetail.ACC_Qty * objMilkSRNDetail.Own_Asset_Rate, 2)
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(dtVLC.Rows(0)("Service_Basis_Own_Asset")), "L") = CompairStringResult.Equal Then
                                objMilkSRNDetail.Own_Asset_Amount = Math.Round(objMilkSRNDetail.MILK_Qty * objMilkSRNDetail.Own_Asset_Rate, 2)
                            End If
                            objMilkSRNDetail.Own_Asset_Type = clsCommon.myCstr(dtVLC.Rows(0)("Service_Basis_Own_Asset"))
                            '============================================
                            objMilkSRNDetail.Service_Charge_Amount = Math.Round(objMilkSRNDetail.MILK_Qty * clsCommon.myCdbl(dtVLC.Rows(0)("Service_Charge_Per_Unit")), 2)
                            objMilkSRNDetail.NET_AMOUNT = Math.Round(objMilkSRNDetail.AMOUNT + objMilkSRNDetail.Emp_Amount + objMilkSRNDetail.TIP_Amount - objMilkSRNDetail.Service_Charge_Amount, 2)
                            If IsRoundOffPaiseAmount Then
                                objMilkSRNDetail.Round_Off = (objMilkSRNDetail.NET_AMOUNT Mod 1)
                                objMilkSRNDetail.NET_AMOUNT = objMilkSRNDetail.NET_AMOUNT - (objMilkSRNDetail.NET_AMOUNT Mod 1)
                            End If
                            objMilkSRNDetail.COMM_PORT = ""
                            Dim arrMilkSRNDetail As New List(Of clsMilkSRNMCCDetail)
                            arrMilkSRNDetail.Add(objMilkSRNDetail)
                            clsMilkSRNMCC.SaveData(objMilkSRNHead, arrMilkSRNDetail, trans)
                            objtr = Nothing
                        Next
                    End If
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        arrShiftEndCode = Nothing
        Return True
    End Function
End Class

Public Class clsMilkProcurementUploaderDetail
#Region "Variables"
    Public Uploader_Code As String
    Public TR_No As String
    Public Document_No As String
    Public SNo As Integer
    Public Shift_Date As Date
    Public Shift As String
    Public VLC_Code As String
    Public VLC_Name As String
    Public Bulk_Route_Code As String
    Public No_Of_Cans As Integer
    Public Milk_Weight As Decimal
    Public FAT As Decimal
    Public SNF As Decimal
    Public Dock_Collection_Milk_Type As String
    Public arrQCParameter As List(Of clsMilkProcurementUploaderQCParameterDetail) = Nothing
    Public Reject_Type As String
    Public Reject_Defaulter As String
    Public Against_Milk_Collection_DCS_Detail As Integer
    Public Dock_Collection_Milk_Type_Auto As Boolean = True
    Public Manual_Weight As Integer
    Public Manual_Sample As Integer
    Public Empty_Sample As Integer
    Public Page_No As Integer
    'Public Retesting_FAT As Decimal
    'Public Retesting_SNF As Decimal
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal strMCCCode As String, ByVal Arr As List(Of clsMilkProcurementUploaderDetail), ByVal trans As SqlTransaction) As Boolean
        Return SaveData(strDocNo, strMCCCode, Arr, trans, "")
    End Function
    Public Shared Function SaveData(ByVal strDocNo As String, ByVal strMCCCode As String, ByVal Arr As List(Of clsMilkProcurementUploaderDetail), ByVal trans As SqlTransaction, ByVal strTR_No As String) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim qry As String = "select TSPL_MCC_MASTER.Is_Seprate_Dock_Cow_Buffalo from TSPL_MILK_PROCUREMENT_UPLOADER_HEAD left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.MCC_Code where TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_No='" + strDocNo + "'"
            Dim Is_Seprate_Dock_Cow_Buffalo As Boolean = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) = 1
            Dim MinDate As Date = Arr(0).Shift_Date
            For Each obj As clsMilkProcurementUploaderDetail In Arr
                If MinDate > obj.Shift_Date Then
                    MinDate = obj.Shift_Date
                End If
                Dim coll As New Hashtable()
                If clsCommon.myLen(strTR_No) > 0 Then
                    obj.TR_No = strTR_No
                Else
                    obj.TR_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select max(TR_No) from TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL where TR_No like '" + strMCCCode + "%'", trans))
                    If clsCommon.myLen(obj.TR_No) <= 0 Then
                        obj.TR_No = strMCCCode + "TR0000000000000000"
                    End If
                    obj.TR_No = clsCommon.incval(obj.TR_No)
                End If

                clsCommon.AddColumnsForChange(coll, "TR_No", obj.TR_No)
                clsCommon.AddColumnsForChange(coll, "Document_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "SNo", obj.SNo)
                clsCommon.AddColumnsForChange(coll, "Shift_Date", clsCommon.GetPrintDate(obj.Shift_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Shift", obj.Shift)
                clsCommon.AddColumnsForChange(coll, "VLC_Code", obj.VLC_Code)
                clsCommon.AddColumnsForChange(coll, "No_Of_Cans", obj.No_Of_Cans)
                clsCommon.AddColumnsForChange(coll, "Milk_Weight", obj.Milk_Weight)
                clsCommon.AddColumnsForChange(coll, "FAT", obj.FAT)
                clsCommon.AddColumnsForChange(coll, "SNF", obj.SNF)
                clsCommon.AddColumnsForChange(coll, "Reject_Type", obj.Reject_Type, True)
                clsCommon.AddColumnsForChange(coll, "Reject_Defaulter", obj.Reject_Defaulter)
                clsCommon.AddColumnsForChange(coll, "Against_Milk_Collection_DCS_Detail", obj.Against_Milk_Collection_DCS_Detail, True)
                clsCommon.AddColumnsForChange(coll, "Bulk_Route_Code", obj.Bulk_Route_Code, True)
                If obj.Dock_Collection_Milk_Type_Auto Then
                    If Not objCommonVar.DisplayTypeInMilkReceipt Then
                        If Is_Seprate_Dock_Cow_Buffalo Then
                            If Not (clsCommon.CompairString(obj.Dock_Collection_Milk_Type, "C") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Dock_Collection_Milk_Type, "B") = CompairStringResult.Equal) Then
                                Throw New Exception("Milk Type can be B or C")
                            End If
                        Else
                            obj.Dock_Collection_Milk_Type = "M"
                        End If
                    End If
                End If
                clsCommon.AddColumnsForChange(coll, "Dock_Collection_Milk_Type", obj.Dock_Collection_Milk_Type)

                clsCommon.AddColumnsForChange(coll, "Manual_Weight", obj.Manual_Weight)
                clsCommon.AddColumnsForChange(coll, "Manual_Sample", obj.Manual_Sample)
                clsCommon.AddColumnsForChange(coll, "Empty_Sample", obj.Empty_Sample, True)
                clsCommon.AddColumnsForChange(coll, "Page_No", obj.Page_No, True)

                If clsCommon.myLen(strTR_No) > 0 Then
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL", OMInsertOrUpdate.Update, "TR_No='" + strTR_No + "'", trans)
                Else
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                End If

                clsMilkProcurementUploaderQCParameterDetail.saveData(strDocNo, obj.TR_No, obj.arrQCParameter, trans)
            Next
            clsMCCPaymentCycleLockForScheduler.CheckForSchedulerLock(strMCCCode, MinDate, trans)
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strPONo As String, ByVal strExtraWhrclas As String, ByVal trans As SqlTransaction) As List(Of clsMilkProcurementUploaderDetail)
        Dim arr As List(Of clsMilkProcurementUploaderDetail) = Nothing
        Dim qry As String = "SELECT TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.*,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [Uploader_Code] 
FROM TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL 
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.VLC_Code  
where  TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No='" + strPONo + "' "
        If clsCommon.myLen(strExtraWhrclas) > 0 Then
            qry += " and " + strExtraWhrclas
        End If
        qry += " ORDER BY TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.SNO"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            arr = New List(Of clsMilkProcurementUploaderDetail)
            Dim objTr As clsMilkProcurementUploaderDetail
            For Each dr As DataRow In dt.Rows
                objTr = New clsMilkProcurementUploaderDetail
                objTr.TR_No = clsCommon.myCstr(dr("TR_No"))
                objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                objTr.SNo = clsCommon.myCdbl(dr("SNo"))
                objTr.Shift_Date = clsCommon.myCDate(dr("Shift_Date"))
                objTr.Shift = clsCommon.myCstr(dr("Shift"))
                objTr.VLC_Code = clsCommon.myCstr(dr("VLC_Code"))
                objTr.VLC_Name = clsCommon.myCstr(dr("VLC_Name"))
                objTr.No_Of_Cans = clsCommon.myCdbl(dr("No_Of_Cans"))
                objTr.Milk_Weight = clsCommon.myCdbl(dr("Milk_Weight"))
                objTr.FAT = clsCommon.myCdbl(dr("FAT"))
                objTr.SNF = clsCommon.myCdbl(dr("SNF"))
                objTr.Dock_Collection_Milk_Type = clsCommon.myCstr(dr("Dock_Collection_Milk_Type"))
                objTr.Against_Milk_Collection_DCS_Detail = clsCommon.myCdbl(dr("Against_Milk_Collection_DCS_Detail"))
                objTr.Reject_Type = clsCommon.myCstr(dr("Reject_Type"))
                objTr.Reject_Defaulter = clsCommon.myCstr(dr("Reject_Defaulter"))
                objTr.Uploader_Code = clsCommon.myCstr(dr("Uploader_Code"))
                objTr.Bulk_Route_Code = clsCommon.myCstr(dr("Bulk_Route_Code"))

                objTr.Manual_Weight = clsCommon.myCDecimal(dr("Manual_Weight"))
                objTr.Manual_Sample = clsCommon.myCDecimal(dr("Manual_Sample"))
                objTr.Empty_Sample = clsCommon.myCDecimal(dr("Empty_Sample"))
                objTr.Page_No = clsCommon.myCDecimal(dr("Page_No"))

                arr.Add(objTr)
            Next
        End If
        Return arr
    End Function
End Class

Public Class clsBulkProcurementDispatchUploader
#Region "Variables"
    Public Document_No As String
    Public Document_Date As DateTime
    Public MCC_Code As String
    Public MCC_Name As String
    Public Description As String
    Public Arr As List(Of clsBulkProcurementDispatchUploader) = Nothing
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Posting_Date As DateTime? = Nothing

    '' Added Field 
    Public isNewEntry As Boolean = False
    Public Dispatch_Date As Date = Nothing
    Public Chalan_NO As String = String.Empty
    Public Tanker_Dispatch_To As String = String.Empty
    Public Mcc_Or_Plant_Code As String = String.Empty
    Public Tanker_No As String = String.Empty
    Public Tanker_KM_Reading As Double = 0
    Public Drip_Marking As String = String.Empty
    Public Tanker_Full As String = String.Empty
    Public Control_Sample As String = String.Empty
    Public Name_Of_Custodian As String = String.Empty
    Public Seal_No1 As String = String.Empty
    Public Seal_No2 As String = String.Empty
    Public Seal_No3 As String = String.Empty
    Public Seal_No4 As String = String.Empty
    Public Seal_No5 As String = String.Empty
    Public Seal_No6 As String = String.Empty
    Public Seal_No7 As String = String.Empty
    Public Seal_No8 As String = String.Empty
    Public Seal_No9 As String = String.Empty
    Public Seal_No10 As String = String.Empty
    Public isPosted As Integer = 0
    Public Tare_Weight As Double = 0
    Public Gross_Weight As Double = 0
    Public control_sample_fat As Double = 0
    Public control_sample_snf As Double = 0
    Public Net_Qty As Double = 0
    Public Transfer_Price As Double = 0
    Public Comp_Code As String = String.Empty
    Public Created_By As String = String.Empty
    Public Created_Date As String = String.Empty
    Public Modified_By As String = String.Empty
    Public Modified_Date As String = String.Empty
    Public Item_Code As String = String.Empty
    Public Item_Desc As String = String.Empty
    Public arrParmValue As List(Of Mcc_Dispatch_Chalan_Parameter) = Nothing
    Public ArrImport As List(Of clsBulkProcurementDispatchUploader) = Nothing
    Public RefBulkDispatchUploader As String = String.Empty

    Public FatPer As Double = 0
    Public SNFPer As Double = 0
    Public Param_Field_Code As String = String.Empty
    Public Param_Field_Desc As String = String.Empty
    Public Param_Field_Value As String = String.Empty
    Public Param_Type As String = String.Empty

    Public Tanker_Transporter_Name As String = String.Empty
    Public Payment_Type As String = String.Empty
    Public Payment_Rate As String = String.Empty
    Public Charge_For As String = String.Empty
    Public Payment_Amount As Double = 0
    Public Chemist_Code As String = String.Empty
    Public Chemist_Name As String = String.Empty
    Public UOM_Code As String = String.Empty
    Public UOM_desc As String = String.Empty
    Public Remarks As String = String.Empty
    Public isReversed As Double = 0
    Public PriceCode As String = ""
    Public FAT_W As Double = 0
    Public SNF_W As Double = 0
    Public FAT_R As Double = 0
    Public SNF_R As Double = 0
    Public FAT_KG As Double = 0
    Public SNF_KG As Double = 0
    Public FAT_RATE As Double = 0
    Public SNF_RATE As Double = 0
    Public Amount As Double = 0
    Public isIntermittent As Double = 0
    Public CurrentLevel As Integer = 0
    Public FinalLoc As String = ""
    Public Level1ChallanNo As String = ""
    Public ReachedAtFinal As Integer = 0
    Public Form_ID As String = ""
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing
    Public isBulkSaleData As Boolean = False
    Public RefDocType As String = String.Empty
    Public RefDocNo As String = String.Empty
    Public MCC_Weighment_No As String = String.Empty

    Public EWayBillNo As String
    Public EWayBillDate As DateTime?
    Public Electronic_Ref_No As String
    Public IsAgainstJobWork As Integer = 0
    Public Sublocation_Code As String

    Public Avg_FAT_Rate As Decimal = 0
    Public Avg_SNF_Rate As Decimal = 0
    Public Avg_FAT_Amount As Decimal = 0
    Public Avg_SNF_Amount As Decimal = 0
    Public Avg_Amount As Decimal = 0
    ''End
#End Region

    Public Function SaveData(ByVal obj As clsMccDispatch, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            ' Dim qry As String = "delete from TSPL_MCC_Dispatch_Challan where RefBulkDispatchUploader='" + obj.Document_No + "'"
            ' clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If isNewEntry Then
                obj.Document_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select max(Document_No) from TSPL_BULK_PROCUREMENT_Dispatch_UPLOADER", trans))
                If clsCommon.myLen(obj.Document_No) <= 0 Then
                    obj.Document_No = "BPU00000000000"
                End If
                obj.Document_No = clsCommon.incval(obj.Document_No)
            End If
            If (clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "MCC_Code", obj.MCC_Code)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULK_PROCUREMENT_Dispatch_UPLOADER", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULK_PROCUREMENT_Dispatch_UPLOADER", OMInsertOrUpdate.Update, "TSPL_BULK_PROCUREMENT_Dispatch_UPLOADER.Document_No='" + obj.Document_No + "'", trans)
            End If

            clsMccDispatch.SaveData(obj, trans, 0, False, obj.Document_No)

            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsBulkProcurementDispatchUploader
        Dim obj As clsBulkProcurementDispatchUploader = Nothing

        Dim qry As String = "SELECT TSPL_BULK_PROCUREMENT_DISPATCH_UPLOADER.* ,TSPL_MCC_MASTER.MCC_NAME  " & _
        " FROM TSPL_BULK_PROCUREMENT_DISPATCH_UPLOADER left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_BULK_PROCUREMENT_DISPATCH_UPLOADER.MCC_Code  where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_BULK_PROCUREMENT_DISPATCH_UPLOADER.Document_No = (select MIN(Document_No) from TSPL_BULK_PROCUREMENT_DISPATCH_UPLOADER where 1=1  )"
            Case NavigatorType.Last
                qry += " and TSPL_BULK_PROCUREMENT_DISPATCH_UPLOADER.Document_No = (select Max(Document_No) from TSPL_BULK_PROCUREMENT_DISPATCH_UPLOADER where 1=1  )"
            Case NavigatorType.Next
                qry += " and TSPL_BULK_PROCUREMENT_DISPATCH_UPLOADER.Document_No = (select Min(Document_No) from TSPL_BULK_PROCUREMENT_DISPATCH_UPLOADER where Document_No>'" + strPONo + "'  )"
            Case NavigatorType.Previous
                qry += " and TSPL_BULK_PROCUREMENT_DISPATCH_UPLOADER.Document_No = (select Max(Document_No) from TSPL_BULK_PROCUREMENT_DISPATCH_UPLOADER where Document_No<'" + strPONo + "'  )"
            Case NavigatorType.Current
                qry += " and TSPL_BULK_PROCUREMENT_DISPATCH_UPLOADER.Document_No = '" + strPONo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsBulkProcurementDispatchUploader()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.MCC_Code = clsCommon.myCstr(dt.Rows(0)("MCC_Code"))
            obj.MCC_Name = clsCommon.myCstr(dt.Rows(0)("MCC_NAME"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)

            qry = "SELECT TSPL_MCC_Dispatch_Challan.* FROM TSPL_MCC_Dispatch_Challan "
            qry += " left outer join TSPL_BULK_PROCUREMENT_DISPATCH_UPLOADER on TSPL_BULK_PROCUREMENT_DISPATCH_UPLOADER.Document_No=TSPL_MCC_Dispatch_Challan.RefBulkDispatchUploader where TSPL_MCC_Dispatch_Challan.RefBulkDispatchUploader='" + obj.Document_No + "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.ArrImport = New List(Of clsBulkProcurementDispatchUploader)
                Dim objTr1 As clsBulkProcurementDispatchUploader
                For Each dr As DataRow In dt.Rows
                    objTr1 = New clsBulkProcurementDispatchUploader
                    objTr1.Dispatch_Date = clsCommon.myCstr(dr("Dispatch_Date"))
                    objTr1.Chalan_NO = clsCommon.myCstr(dr("Chalan_No"))
                    objTr1.Tanker_Dispatch_To = clsCommon.myCstr(dr("Tanker_Dispatch_To"))
                    objTr1.MCC_Code = clsCommon.myCstr(dr("Mcc_Code"))
                    objTr1.MCC_Name = clsCommon.myCstr(dr("Mcc_Name"))
                    objTr1.Mcc_Or_Plant_Code = clsCommon.myCstr(dr("Mcc_Or_Plant_Code"))
                    objTr1.Tanker_No = clsCommon.myCstr(dr("Tanker_No"))
                    objTr1.Tanker_KM_Reading = clsCommon.myCstr(dr("Tanker_KM_Reading"))
                    objTr1.Drip_Marking = clsCommon.myCstr(dr("Drip_Marking"))
                    objTr1.Tanker_Full = clsCommon.myCstr(dr("Tanker_Full"))
                    objTr1.Control_Sample = clsCommon.myCstr(dr("Control_Sample"))
                    objTr1.Name_Of_Custodian = clsCommon.myCstr(dr("Name_Of_Custodian"))
                    objTr1.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr1.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    objTr1.Tare_Weight = clsCommon.myCdbl(dr("Tare_Weight"))
                    objTr1.Gross_Weight = clsCommon.myCdbl(dr("Gross_Weight"))
                    objTr1.Net_Qty = clsCommon.myCstr(dr("Net_Qty"))
                    objTr1.Transfer_Price = clsCommon.myCstr(dr("Transfer_Price"))
                    objTr1.control_sample_fat = clsCommon.myCstr(dr("control_sample_fat"))
                    objTr1.control_sample_snf = clsCommon.myCstr(dr("control_sample_snf"))
                    objTr1.UOM_Code = clsCommon.myCstr(dr("UOM_Code"))
                    objTr1.UOM_desc = clsCommon.myCstr(dr("UOM_desc"))
                    objTr1.Remarks = clsCommon.myCstr(dr("Remarks"))
                    objTr1.PriceCode = clsCommon.myCstr(dr("PriceCode"))
                    objTr1.FAT_W = clsCommon.myCdbl(dr("FAT_W"))
                    objTr1.SNF_W = clsCommon.myCdbl(dr("SNF_W"))
                    objTr1.FAT_R = clsCommon.myCdbl(dr("FAT_R"))
                    objTr1.SNF_R = clsCommon.myCdbl(dr("SNF_R"))
                    objTr1.FAT_KG = clsCommon.myCdbl(dr("FAT_KG"))
                    objTr1.SNF_KG = clsCommon.myCdbl(dr("SNF_KG"))
                    objTr1.FAT_RATE = clsCommon.myCdbl(dr("FAT_RATE"))
                    objTr1.SNF_RATE = clsCommon.myCdbl(dr("SNF_RATE"))
                    objTr1.Amount = clsCommon.myCdbl(dr("Amount"))
                    objTr1.RefBulkDispatchUploader = clsCommon.myCstr(dr("RefBulkDispatchUploader"))
                    objTr1.arrParmValue = ParamtergetData(objTr1.Chalan_NO, trans, objTr1.RefBulkDispatchUploader)
                    obj.ArrImport.Add(objTr1)
                Next
            End If

        End If
        Return obj
    End Function
    Public Shared Function ParamtergetData(ByVal chalanNo As String, ByVal trans As SqlTransaction, ByVal RefBulkDispatchUploader As String) As List(Of Mcc_Dispatch_Chalan_Parameter)
        Dim arr As New List(Of Mcc_Dispatch_Chalan_Parameter)
        Try
            Dim obj As New Mcc_Dispatch_Chalan_Parameter
            Dim q As String = "select TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.* from TSPL_Mcc_Dispatch_Chalan_Parameter_Detail "
            q += " left outer join TSPL_MCC_Dispatch_Challan on TSPL_MCC_Dispatch_Challan.Chalan_NO=TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Chalan_No"
            q += " where TSPL_MCC_Dispatch_Challan.Chalan_NO='" & chalanNo & "' and TSPL_MCC_Dispatch_Challan.RefBulkDispatchUploader='" & RefBulkDispatchUploader & "'"
            Dim dtbl As DataTable = clsDBFuncationality.GetDataTable(q, trans)
            If dtbl IsNot Nothing AndAlso dtbl.Rows.Count > 0 Then
                For i As Integer = 0 To dtbl.Rows.Count - 1
                    obj = New Mcc_Dispatch_Chalan_Parameter
                    obj.SNO = clsCommon.myCstr(dtbl.Rows(i)("SNO"))
                    obj.Chalan_No = clsCommon.myCstr(dtbl.Rows(i)("Chalan_No"))
                    obj.Param_Field_Code = clsCommon.myCstr(dtbl.Rows(i)("Param_Field_Code"))
                    obj.Param_Field_Desc = clsCommon.myCstr(dtbl.Rows(i)("Param_Field_Desc"))
                    obj.Param_Field_Value = clsCommon.myCstr(dtbl.Rows(i)("Param_Field_Value"))
                    obj.Param_Type = clsCommon.myCstr(dtbl.Rows(i)("Param_Type"))
                    arr.Add(obj)
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return arr
    End Function
End Class


Public Class clsImportTemp
    Public Code As String = Nothing
    Public Description As String = Nothing
    Public Type As String = Nothing
    Public Nature As String = Nothing
    Public IsMandatory As Integer = 0
    Public Param_For As String = Nothing
End Class

Public Class clsMilkProcurementUploaderQCParameterDetail
    Public Document_No As String = String.Empty
    Public TR_No As String = String.Empty
    Public Sample_No As Integer = 0
    Public Param_Field_Code As String = String.Empty
    Public Param_Field_Desc As String = String.Empty
    Public Param_Field_Value As String = String.Empty
    Public Param_Type As String = String.Empty

    Public Shared Function saveData(ByVal strQCNo As String, ByVal strTRCode As String, ByVal arrObj As List(Of clsMilkProcurementUploaderQCParameterDetail)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            saveData(strQCNo, strTRCode, arrObj, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function saveData(ByVal strQCNo As String, ByVal strTRCode As String, ByVal arrObj As List(Of clsMilkProcurementUploaderQCParameterDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim coll As Hashtable
            If arrObj IsNot Nothing Then
                For Each obj As clsMilkProcurementUploaderQCParameterDetail In arrObj
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Document_No", strQCNo)
                    clsCommon.AddColumnsForChange(coll, "TR_No", strTRCode)
                    clsCommon.AddColumnsForChange(coll, "Param_Field_Code", obj.Param_Field_Code)
                    clsCommon.AddColumnsForChange(coll, "Param_Field_Desc", obj.Param_Field_Desc)
                    clsCommon.AddColumnsForChange(coll, "Param_Field_Value", obj.Param_Field_Value)
                    clsCommon.AddColumnsForChange(coll, "Param_Type", obj.Param_Type)
                    clsCommon.AddColumnsForChange(coll, "Sample_No", obj.Sample_No)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_PROCUREMENT_UPLOADER_QC_PARAMETER_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function getData(ByVal strQCNo As String, ByVal trans As SqlTransaction, ByVal intMilkProcSRNo As Integer) As List(Of clsMilkProcurementUploaderQCParameterDetail)
        Dim arrObj As List(Of clsMilkProcurementUploaderQCParameterDetail) = Nothing
        Try
            Dim obj As clsMilkProcurementUploaderQCParameterDetail = Nothing
            Dim qry As String = "select * from TSPL_MILK_PROCUREMENT_UPLOADER_QC_PARAMETER_DETAIL where Document_No='" & strQCNo & "'"
            If intMilkProcSRNo > 0 Then
                qry += " and Sample_No='" + clsCommon.myCstr(intMilkProcSRNo) + "'"
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arrObj = New List(Of clsMilkProcurementUploaderQCParameterDetail)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New clsMilkProcurementUploaderQCParameterDetail()
                    obj.Document_No = clsCommon.myCstr(dt.Rows(i)("Document_No"))
                    obj.TR_No = clsCommon.myCstr(dt.Rows(i)("TR_No"))
                    obj.Param_Field_Code = clsCommon.myCstr(dt.Rows(i)("Param_Field_Code"))
                    obj.Param_Field_Desc = clsCommon.myCstr(dt.Rows(i)("Param_Field_Desc"))
                    obj.Param_Field_Value = clsCommon.myCstr(dt.Rows(i)("Param_Field_Value"))
                    obj.Param_Type = clsCommon.myCstr(dt.Rows(i)("Param_Type"))
                    obj.Sample_No = clsCommon.myCdbl(dt.Rows(i)("Sample_No"))
                    arrObj.Add(obj)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return arrObj
    End Function
End Class