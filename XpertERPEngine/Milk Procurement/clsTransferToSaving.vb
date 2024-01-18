Imports common
Imports System.Data.SqlClient
Public Class clsTransferToSaving
#Region "Variables"
    Public Document_No As String = Nothing
    Public Document_Date As DateTime
    Public Loc_Seg_Code As String = Nothing
    Public Loc_Seg_Name As String = Nothing ''Not a Column
    Public MCC_Code As String = String.Empty
    Public MCC_Name As String = String.Empty
    Public Remarks As String = Nothing
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Posted_Date As DateTime?

    Public Arr As List(Of clsTransferToSavingDetail) = Nothing
    Public ArrDT As DataTable = Nothing
#End Region

    Public Function SaveData(ByVal obj As clsTransferToSaving, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = False
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = obj.SaveData(obj, isNewEntry, trans)
            If (isSaved) Then
                trans.Commit()
            Else
                trans.Rollback()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Function SaveData(ByVal obj As clsTransferToSaving, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean

        Dim isSaved As Boolean = True
        If clsCommon.myLen(obj.Loc_Seg_Code) <= 0 Then
            Throw New Exception("Please first select Location")
        End If
        clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.frmTransferToSaving, obj.Loc_Seg_Code, obj.Document_Date, trans)

        Dim qry As String = "delete from TSPL_TRANSFER_TO_SAVING_DETAIL where Document_No='" + obj.Document_No + "'"
        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

        If obj.Arr.Count <= 0 Then
            Throw New Exception("No detail found to save")
        End If

        If (isNewEntry) Then
            obj.Document_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Document_Date), clsDocType.TransferToSaving, "", obj.Loc_Seg_Code, True)
        End If
        If (clsCommon.myLen(obj.Document_No) <= 0) Then
            Throw New Exception("Error in Document Code Generation")
        End If

        Dim coll As New Hashtable()
        clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
        clsCommon.AddColumnsForChange(coll, "Loc_Seg_Code", obj.Loc_Seg_Code)
        clsCommon.AddColumnsForChange(coll, "MCC_Code", obj.MCC_Code, True)
        clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
        clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
        clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
        If isNewEntry Then
            clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TRANSFER_TO_SAVING", OMInsertOrUpdate.Insert, "", trans)
        Else
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TRANSFER_TO_SAVING", OMInsertOrUpdate.Update, "Document_No='" + obj.Document_No + "'", trans)
        End If
        isSaved = isSaved AndAlso clsTransferToSavingDetail.SaveData(obj.Document_No, Arr, trans)
        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_No, "TSPL_TRANSFER_TO_SAVING", "Document_No", "TSPL_TRANSFER_TO_SAVING_DETAIL", "Document_No", trans)
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType, ByVal isGetDT As Boolean, ByVal trans As SqlTransaction) As clsTransferToSaving
        Dim obj As clsTransferToSaving = Nothing
        Dim qry As String = "Select TSPL_TRANSFER_TO_SAVING.*,TSPL_GL_SEGMENT_CODE.Description as Loc_Seg_Name,TSPL_MCC_MASTER.MCC_NAME
from TSPL_TRANSFER_TO_SAVING 
left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code=TSPL_TRANSFER_TO_SAVING.Loc_Seg_Code and TSPL_GL_SEGMENT_CODE.Seg_No='7'
left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_TRANSFER_TO_SAVING.MCC_CODE
where 2=2 "
        Dim whrClas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_TRANSFER_TO_SAVING.Document_No = (select MIN(Document_No) from TSPL_TRANSFER_TO_SAVING where 1=1 " + whrClas + ")"
            Case NavigatorType.Last
                qry += " and TSPL_TRANSFER_TO_SAVING.Document_No = (select Max(Document_No) from TSPL_TRANSFER_TO_SAVING where 1=1 " + whrClas + ")"
            Case NavigatorType.Next
                qry += " and TSPL_TRANSFER_TO_SAVING.Document_No = (select Min(Document_No) from TSPL_TRANSFER_TO_SAVING where Document_No>'" + strDocumentNo + "' " + whrClas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_TRANSFER_TO_SAVING.Document_No = (select Max(Document_No) from TSPL_TRANSFER_TO_SAVING where Document_No<'" + strDocumentNo + "' " + whrClas + ")"
            Case NavigatorType.Current
                qry += " and TSPL_TRANSFER_TO_SAVING.Document_No = '" + strDocumentNo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsTransferToSaving()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Loc_Seg_Code = clsCommon.myCstr(dt.Rows(0)("Loc_Seg_Code"))
            obj.Loc_Seg_Name = clsCommon.myCstr(dt.Rows(0)("Loc_Seg_Name"))
            obj.MCC_Code = clsCommon.myCstr(dt.Rows(0)("MCC_Code"))
            obj.MCC_Name = clsCommon.myCstr(dt.Rows(0)("MCC_NAME"))
            If dt.Rows(0)("Posted_Date") IsNot DBNull.Value Then
                obj.Posted_Date = clsCommon.myCDate(dt.Rows(0)("Posted_Date"))
            Else
                obj.Posted_Date = Nothing
            End If
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 0, ERPTransactionStatus.Pending, ERPTransactionStatus.Approved)

            qry = "Select ROW_NUMBER() over(order by TSPL_TRANSFER_TO_SAVING_DETAIL.PK_ID) as SNo,TSPL_TRANSFER_TO_SAVING_DETAIL.* , TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VLC_MASTER_HEAD.VLC_Name 
from TSPL_TRANSFER_TO_SAVING_DETAIL
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_TRANSFER_TO_SAVING_DETAIL.Vendor_Code
where TSPL_TRANSFER_TO_SAVING_DETAIL.Document_No='" + obj.Document_No + "' ORDER BY TSPL_TRANSFER_TO_SAVING_DETAIL.PK_ID"
            obj.ArrDT = clsDBFuncationality.GetDataTable(qry, trans)
            If Not isGetDT Then
                If (obj.ArrDT IsNot Nothing AndAlso obj.ArrDT.Rows.Count > 0) Then
                    obj.Arr = New List(Of clsTransferToSavingDetail)
                    For Each dr As DataRow In obj.ArrDT.Rows
                        Dim objTr As New clsTransferToSavingDetail
                        objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                        objTr.PK_ID = clsCommon.myCstr(dr("PK_ID"))
                        objTr.Vendor_Code = clsCommon.myCstr(dr("Vendor_Code"))
                        objTr.Vendor_Name = clsCommon.myCstr(dr("VLC_Name"))
                        objTr.VLCUploderCode = clsCommon.myCstr(dr("VLC_Code_VLC_Uploader"))
                        objTr.Amount = clsCommon.myCdbl(dr("Amount"))
                        obj.Arr.Add(objTr)
                    Next
                End If
            End If
        End If
        Return obj
    End Function

    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim isSaved As Boolean = False
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = clsTransferToSaving.PostData(strDocNo, trans)
            If isSaved Then
                trans.Commit()
            Else
                trans.Rollback()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Private Shared Function CreateAPInvoiceHeader(ByVal obj As clsTransferToSaving, ByVal trans As SqlTransaction) As Boolean
        Dim VendAccSet As String = String.Empty
        Dim dtDed As DataTable = clsDBFuncationality.GetDataTable("select Code,Description,GL_Account_Code from TSPL_DEDUCTION_MASTER  where Is_Transfer_To_Saving=1", trans)
        If dtDed Is Nothing OrElse dtDed.Rows.Count <= 0 Then
            Throw New Exception("Please set default Transfer to saving deduction")
        End If

        For Each objTr As clsTransferToSavingDetail In obj.Arr
            ''Debit Note
            Dim objVendInv As New clsVedorInvoiceHead()
            objVendInv.Invoice_Entry_Date = obj.Document_Date
            objVendInv.Document_Type = "D"
            objVendInv.Invoice_Type = "AP"
            objVendInv.loc_code = obj.Loc_Seg_Code
            objVendInv.Document_Total = objTr.Amount
            objVendInv.Posting_Date = obj.Document_Date
            objVendInv.Vendor_Invoice_Date = obj.Document_Date
            objVendInv.On_Hold = 0
            objVendInv.Remarks = obj.Remarks
            objVendInv.Description = "Transfer To Saving Debit Note"
            objVendInv.Balance_Amt = objTr.Amount
            objVendInv.ISProcurementDeduction = 1
            objVendInv.Amount_Less_Discount = objVendInv.Document_Total
            objVendInv.Discount_Base = objVendInv.Document_Total
            objVendInv.Against_TransferToSavingPKID = objTr.PK_ID
            objVendInv.isDeduction = 1
            Dim qry As String = "Select TSPL_VENDOR_ACCOUNT_SET.Payable_Account,TSPL_VENDOR_MASTER.GSTRegistered,TSPL_VENDOR_MASTER.Vendor_Account,TSPL_VENDOR_MASTER.Terms_Code ,TSPL_TERMS_MASTER.Terms_Desc,TSPL_TERMS_MASTER.No_Days   
        from TSPL_VENDOR_MASTER 
        left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code =TSPL_VENDOR_MASTER.Terms_Code 
        left outer join TSPL_VENDOR_ACCOUNT_SET on TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code = TSPL_VENDOR_MASTER.Vendor_Account
        where TSPL_VENDOR_MASTER.Vendor_Code ='" + objTr.Vendor_Code + "'"

            Dim dtVendor As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dtVendor IsNot Nothing AndAlso dtVendor.Rows.Count > 0 Then
                objVendInv.Terms_Code = clsCommon.myCstr(dtVendor.Rows(0)("Terms_Code"))
                objVendInv.Terms_Description = clsCommon.myCstr(dtVendor.Rows(0)("Terms_Desc"))
                objVendInv.Due_Date = obj.Document_Date.AddDays(clsCommon.myCdbl(dtVendor.Rows(0)("No_Days")))
                objVendInv.Account_Set = clsCommon.myCstr(dtVendor.Rows(0)("Vendor_Account"))
                objVendInv.GSTRegistered = clsCommon.myCDecimal(dtVendor.Rows(0)("GSTRegistered"))
                objVendInv.Vendor_Control_AC = clsCommon.myCstr(dtVendor.Rows(0)("Payable_Account"))
            Else
                Throw New Exception("Please define vendor account set for vendor [" + objTr.Vendor_Code + "] ")
            End If
            objVendInv.Vendor_Code = objTr.Vendor_Code
            objVendInv.Vendor_Name = objTr.Vendor_Name
            objVendInv.MCC_Code = obj.MCC_Code
            objVendInv.MCC_Name = obj.MCC_Name
            objVendInv.Arr = New List(Of clsVedorInvoiceDetail)

            '' Detail Level Saving
            Dim objVendInvTR As clsVedorInvoiceDetail = New clsVedorInvoiceDetail()
            VendAccSet = objVendInv.Account_Set
            objVendInvTR.Detail_Line_No = 1
            If clsCommon.myLen(VendAccSet) <= 0 Then
                Throw New Exception("Please set vendor account set for vendor -" + objTr.Vendor_Code)
            End If
            objVendInvTR.Amount = objTr.Amount
            objVendInvTR.Amount_less_Discount = objTr.Amount
            objVendInvTR.Total_Amount = objTr.Amount
            objVendInvTR.DeductionCode = clsCommon.myCstr(dtDed.Rows(0)("code"))
            objVendInvTR.DeductionDesc = clsCommon.myCstr(dtDed.Rows(0)("Description"))
            objVendInvTR.GL_Account_Code = clsCommon.myCstr(dtDed.Rows(0)("GL_Account_Code"))
            If clsCommon.myLen(objVendInvTR.GL_Account_Code) <= 0 Then
                Throw New Exception("Please set GL Account Code for deduction code [" + clsCommon.myCstr(dtDed.Rows(0)("code")) + "] ")
            End If
            objVendInvTR.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendInvTR.GL_Account_Code, obj.Loc_Seg_Code, True, trans)
            If clsCommon.myLen(objVendInv.Vendor_Control_AC) <= 0 Then
                Throw New Exception("Please set the vendor payable Account")
            End If
            objVendInvTR.GL_Account_Desc = clsGLAccount.GetName(objVendInvTR.GL_Account_Code, trans)
            objVendInv.Arr.Add(objVendInvTR)

            objVendInv.SaveData(objVendInv, True, trans)
            clsVedorInvoiceHead.PostData("", objVendInv.Document_No, "", trans)

            'Credit Note 
            objVendInv.Document_No = ""
            objVendInv.Document_Type = "C"
            objVendInv.Saving = 2 ''Compulsory
            objVendInv.Description = "Transfer To Saving Credit Note"
            objVendInv.SaveData(objVendInv, True, trans)
            clsVedorInvoiceHead.PostData("", objVendInv.Document_No, "", trans)
        Next

        Return True
    End Function
    Public Shared Function PostData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = ""
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Document No not found to Post")
        End If


        Dim obj As clsTransferToSaving = clsTransferToSaving.GetData(strDocNo, NavigatorType.Current, False, trans)
        clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.frmMultipleProcDeduction, obj.Loc_Seg_Code, obj.Document_Date, trans)

        If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
            Throw New Exception("No Data found to Post")
        End If

        If (clsCommon.myLen(obj.Posted_Date) > 0) Then
            Throw New Exception("Already Post on :" + obj.Posted_Date)
        End If
        clsMCCPaymentCycleLockForScheduler.CheckForSchedulerLock(obj.MCC_Code, obj.Document_Date, trans)

        CreateAPInvoiceHeader(obj, trans)
        qry = "Update TSPL_TRANSFER_TO_SAVING set Posted_Date='" + clsCommon.GetPrintDate(clsCommon.myCDate(clsCommon.GETSERVERDATE(trans)), "dd/MMM/yyyy hh:mm:ss tt") + "',Status=1 ,Posted_By='" + objCommonVar.CurrentUserCode + "' where Document_No='" + strDocNo + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            DeleteData(strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function DeleteData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean

        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim obj As clsTransferToSaving = clsTransferToSaving.GetData(strDocNo, NavigatorType.Current, False, trans)
        clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.frmMultipleProcDeduction, obj.Loc_Seg_Code, obj.Document_Date, trans)

        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
            Try
                If obj.Status = ERPTransactionStatus.Approved Then
                    Throw New Exception("Already Post on :" + obj.Posted_Date)
                End If
                Dim qry As String = "delete from TSPL_TRANSFER_TO_SAVING_DETAIL where Document_No='" + strDocNo + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_TRANSFER_TO_SAVING where Document_No='" + strDocNo + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End If
        Return isSaved
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            ReverseAndUnpost(strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim Qry As String = "Select Posted_Date from TSPL_TRANSFER_TO_SAVING WHERE Document_No='" + strDocNo + "'"
            If clsCommon.myLen(clsDBFuncationality.getSingleValue(Qry, trans)) <= 0 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If

            Dim obj As clsTransferToSaving = clsTransferToSaving.GetData(strDocNo, NavigatorType.Current, False, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("No Data found to Reverse And UnPost")
            End If

            For Each objtr As clsTransferToSavingDetail In obj.Arr
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Document_No from tspl_vendor_invoice_head where Against_TransferToSavingPKID= " + clsCommon.myCstr(objtr.PK_ID) + "", trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        Dim strAPDocCode As String = clsCommon.myCstr(dr("Document_No"))
                        If clsCommon.myLen(strAPDocCode) > 0 Then
                            Dim dtCheck As DataTable = clsDBFuncationality.GetDataTable("select Doc_No from TSPL_PAYMENT_PROCESS_DEDUCTION where AP_Invoice_No='" + strAPDocCode + "'", trans)
                            If dtCheck IsNot Nothing AndAlso dtCheck.Rows.Count > 0 Then
                                Throw New Exception("Used In Payment Process No [" + clsCommon.myCstr(dtCheck.Rows(0)("Doc_No")) + "] in Deduction ")
                            End If
                            dtCheck = clsDBFuncationality.GetDataTable("select Doc_No from TSPL_PAYMENT_PROCESS_CREDIT_NOTE where AP_Invoice_No='" + strAPDocCode + "'", trans)
                            If dtCheck IsNot Nothing AndAlso dtCheck.Rows.Count > 0 Then
                                Throw New Exception("Used In Payment Process No [" + clsCommon.myCstr(dtCheck.Rows(0)("Doc_No")) + "] in Addition")
                            End If
                            clsVedorInvoiceHead.ReverseAndUnpost(strAPDocCode, trans)
                            clsVedorInvoiceHead.DeleteData(strAPDocCode, trans)
                        End If
                    Next
                End If
            Next

            Qry = "Update TSPL_TRANSFER_TO_SAVING set Posted_By=null,Posted_Date=NULL, Modify_By='" + objCommonVar.CurrentUserCode + "',Status=0 where Document_No='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class

Public Class clsTransferToSavingDetail
#Region "Variables"
    Public PK_ID As Integer = 0
    Public Document_No As String = Nothing
    Public Vendor_Code As String = Nothing
    Public Amount As Decimal = 0
    Public VLCUploderCode As String ''Not a table column
    Public Vendor_Name As String = Nothing ''Not a table column
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsTransferToSavingDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsTransferToSavingDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code)
                clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TRANSFER_TO_SAVING_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
End Class

