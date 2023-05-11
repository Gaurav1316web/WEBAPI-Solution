Imports common
Imports System.Data.SqlClient
'' changes done by richa agarwal against ticket no. BM00000008466 on 02/12/2015
Public Class FrmMilkPurchaseInvoice
    Inherits FrmMainTranScreen
    Dim BulkProcPriceChartStandardRateWithZero As Integer = 0
    Dim AllowJobWorkonGateEntryBulkProc As Integer = 0
    Dim IsItemMilkType As Integer = 0
    Dim IsPriceChartGradeWise As Integer = 0
    Dim TankerFromMaster As Integer = 0
    Dim isSRNselected As Boolean = False
    Dim AllowTruncateAmount As Boolean = False
    Public isLoadData As Boolean = False
    Public Const colSelect As String = "colSelect"
    Public Const colSlNo As String = "SLNO"
    Public Const colSRNNo As String = "SRNNO"
    Public Const colSRNDATe As String = "SRNDate"
    Public Const colItemCode As String = "ItemCode"
    Public Const colHSN As String = "HSNCode"
    Public Const colItemDesc As String = "ItemDesc"
    Public Const colChamberDesc As String = "colChamberDesc"
    Public Const colQty As String = "Qty"
    Public Const colUOM As String = "UOM"
    Public Const colFat As String = "colFAT"
    Dim AllowDateChanged As Boolean = False
    Public Const colSNF As String = "colSNF"
    Public Const colFatKG As String = "colFATKG"
    Public Const colPendingQty As String = "colPendingQty"
    Public Const colSNFKG As String = "colSNFKG"
    Public Const colGrossWeight As String = "colGrossWeight"
    Public Const colTareWeight As String = "colTareWeight"
    Public Const colNetWeight As String = "colNetWeight"
    Public Const colFatRate As String = "colFATRate"
    Public Const colSNFRate As String = "colSNFRate"
    Public Const colAmt As String = "colAmt"
    Public Const colDeduc As String = "colDeduc"
    Public Const colIncen As String = "colIncen"
    Public Const colSpecialDeduc As String = "colSpecialDeduc"
    Public Const colActAmt As String = "colActAmt"
    Public Const colNetRate As String = "colNetRate"
    Public Const colPriceCode As String = "colPriceCode"
    Public Const colTnkrNo As String = "colTnkrNo"
    Public isCellValueChangedOpen As Boolean
    Public errorControl As clsErrorControl = New clsErrorControl()
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim obj As clsMilkPurchaseInvoiceHead = Nothing
    Dim RunBulkProcOnAdjustFATCLR As Integer = 0
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmBulkMilkPurchaseInvoice)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPrint.Visible = MyBase.isPrintFlag
        btnBillOfSupply.Visible = MyBase.isPrintFlag

    End Sub
    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
        GC.Collect()
    End Sub

    Private Sub btnReverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReverse.Click
        Try
            Dim qry As String = "select tspl_Bulk_milk_purchase_Invoice_head.DOC_NO,InvGL.Voucher_No as InvGLVoucherNo,TSPL_ADJUSTMENT_HEADER.Adjustment_No,TSPL_VENDOR_INVOICE_HEAD.Document_No as VendorInvoiceNo ,TSPL_JOURNAL_MASTER.Voucher_No as APINVVoucherNO from tspl_Bulk_milk_purchase_Invoice_head  left outer join TSPL_JOURNAL_MASTER InvGL on InvGL.Source_Doc_No=tspl_Bulk_milk_purchase_Invoice_head.DOC_NO left outer join  TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.Against_Bulk_Srn_PI_adjustment=tspl_Bulk_milk_purchase_Invoice_head.DOC_NO  left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No =tspl_Bulk_milk_purchase_Invoice_head.DOC_NO left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_VENDOR_INVOICE_HEAD.Document_No where tspl_Bulk_milk_purchase_Invoice_head.DOC_NO='" & fndDocNo.Value & "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If TankerFromMaster = 0 Then
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    If clsCommon.MyMessageBoxShow("Do you want to Reverse and unpost the current Document" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                        '' reason for reverse
                        Dim Reason As String = ""
                        Dim frm As New FrmFreeTxtBox1
                        frm.Text = "Remarks for Reverse"
                        frm.ShowDialog()
                        If clsCommon.myLen(frm.strRmks) <= 0 Then
                            Exit Sub
                        Else
                            Reason = frm.strRmks
                        End If
                        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                        Try
                            For Each dr As DataRow In dt.Rows
                                ''Delete Purchase Invoice Journal Entry 
                                Dim docNo As String = clsCommon.myCstr(dr("InvGLVoucherNo"))
                                If clsCommon.myLen(docNo) > 0 Then
                                    qry = "delete from TSPL_JOURNAL_DETAILS where TSPL_JOURNAL_DETAILS.Voucher_No  in ('" + docNo + "')"
                                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                                    qry = "delete from TSPL_JOURNAL_MASTER where TSPL_JOURNAL_MASTER.Voucher_No in ('" + docNo + "')"
                                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                                End If


                                ''Delete AP Invoice Journal Entry 
                                docNo = clsCommon.myCstr(dr("APINVVoucherNO"))
                                If clsCommon.myLen(docNo) > 0 Then
                                    qry = "delete from TSPL_JOURNAL_DETAILS where TSPL_JOURNAL_DETAILS.Voucher_No  in ('" + docNo + "')"
                                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                                    qry = "delete from TSPL_JOURNAL_MASTER where TSPL_JOURNAL_MASTER.Voucher_No in ('" + docNo + "')"
                                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                                End If


                                Dim dtAP As DataTable
                                '' Get Payment Entry Against AP Invoice
                                docNo = clsCommon.myCstr(dr("VendorInvoiceNo"))
                                If clsCommon.myLen(docNo) > 0 Then
                                    qry = "select Payment_No from TSPL_PAYMENT_DETAIL  where Document_No in ('" + docNo + "')"
                                    dtAP = clsDBFuncationality.GetDataTable(qry, trans)
                                    If dtAP IsNot Nothing AndAlso dtAP.Rows.Count > 0 Then
                                        qry = "AP-Invoice " + docNo + " is used in following Payment -"
                                        For Each drAP As DataRow In dtAP.Rows
                                            qry += Environment.NewLine + clsCommon.myCstr(drAP("Payment_No"))
                                        Next
                                        Throw New Exception(qry)
                                    End If
                                End If

                                '' Get Payment Entry Against Purchase  Invoice
                                docNo = clsCommon.myCstr(dr("DOC_NO"))
                                If clsCommon.myLen(docNo) > 0 Then
                                    qry = "select Payment_No from TSPL_PAYMENT_DETAIL  where Document_No in ('" + docNo + "')"
                                    dtAP = clsDBFuncationality.GetDataTable(qry, trans)
                                    If dtAP IsNot Nothing AndAlso dtAP.Rows.Count > 0 Then
                                        qry = "Purchase-Invoice " + docNo + " is used in following Payment -"
                                        For Each drAP As DataRow In dtAP.Rows
                                            qry += Environment.NewLine + clsCommon.myCstr(drAP("Payment_No"))
                                        Next
                                        Throw New Exception(qry)
                                    End If
                                End If


                                ''Delete AP Invoice

                                docNo = clsCommon.myCstr(dr("VendorInvoiceNo"))
                                If clsCommon.myLen(docNo) > 0 Then
                                    qry = "delete from TSPL_VENDOR_INVOICE_DETAIL where Document_No in ('" & docNo & "')"
                                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                                    qry = "delete from TSPL_VENDOR_INVOICE_HEAD where Document_No in ('" & docNo & "')"
                                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                                End If



                                docNo = clsCommon.myCstr(dr("Adjustment_No"))
                                If clsCommon.myLen(docNo) > 0 Then
                                    qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No in (select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='IC-AD' and Source_Doc_No in ('" + docNo + "'))"
                                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                                    qry = "delete from TSPL_JOURNAL_MASTER where Source_Code='IC-AD' and Source_Doc_No in ('" + docNo + "')"
                                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                                    qry = "delete from TSPL_ADJUSTMENT_DETAIL where  TSPL_ADJUSTMENT_DETAIL.Adjustment_No in ('" + docNo + "')"
                                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                                    qry = "delete from TSPL_ADJUSTMENT_HEADER where Adjustment_No in ('" + docNo + "')"
                                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                                End If

                                ''''''''''''''''''


                                ''Change status to unpost
                                docNo = clsCommon.myCstr(dr("DOC_NO"))
                                qry = "update tspl_Bulk_milk_purchase_Invoice_head set isPosted=0 where DOC_NO in ('" + docNo + "')"
                                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                            Next
                            saveCancelLog(Reason, "Reverse and Recreate", trans)
                            trans.Commit()
                            clsCommon.MyMessageBoxShow("Task done Successfully", Me.Text)
                            loadData(fndDocNo.Value, NavigatorType.Current)
                        Catch ex As Exception
                            trans.Rollback()
                            Throw New Exception(ex.Message)
                        End Try
                    End If
                End If
            Else
                If common.clsCommon.MyMessageBoxShow("Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                    If clsMilkPurchaseInvoiceHead.ReverseAndUnpost(fndDocNo.Value) Then
                        common.clsCommon.MyMessageBoxShow("Successfully Reversed and Recreated", Me.Text)
                        loadData(fndDocNo.Value, NavigatorType.Current)
                    End If
                End If

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = MyBase.Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.fndDocNo.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function
    Sub reset()
        txtSubLocation.Value = ""
        lblSubLocation.Text = ""
        chkJobWork.Checked = False
        chkSRNTrade.Enabled = True
        TxtVendorUpdate.Visible = False
        btnUpdateVendor.Visible = False
        TxtVendorUpdate.Value = ""
        fndLocation.Value = ""
        lblLocationName.Text = ""
        fndDocNo.Value = ""
        fndDocNo.MyReadOnly = False
        Dim dt As Date = clsCommon.GETSERVERDATE()
        dtpDocDate.Value = dt
        fndVendor.Value = ""
        fndVendor.Enabled = True
        fndVendor.MyReadOnly = False
        lblVendorName.Text = ""

        If TankerFromMaster = 0 Then
            dtpFromDate.Value = DateAdd(DateInterval.Day, -10, dt)
            dtpToDate.Value = dt
        Else
            txtMonth.Value = clsCommon.GETSERVERDATE
            dtpFromDate.Value = clsCommon.GetDateWithStartTime(clsCommon.GETSERVERDATE())
            dtpToDate.Value = clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE)
        End If

        dtpFromDate.Enabled = True
        dtpToDate.Enabled = True
        fndSRNNo.Value = ""
        txtTotalFatKg.Text = ""
        txtTotalSNFKg.Text = ""
        txtTotalQty.Text = ""
        txtTotalAmt.Text = ""
        loadBlankGrid()
        lblPending.Status = ERPTransactionStatus.Pending
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = False
        btnPost.Enabled = False
        btnPrint.Enabled = False
        btnBillOfSupply.Enabled = False
        btnReverse.Visible = False
        fndLocation.Enabled = True
        txtVendorInvoiceNo.Text = ""
        txtRoundOffAmt.Text = ""
        FindAndRestoreGridLayout(Me)
        FindAndSetTabStopFalse(Me)

        txtewaybilldate.Value = clsCommon.GETSERVERDATE()
        txtewaybilldate.Checked = False
        TxtEWayBillNo.Text = ""
        txtElectronicRefNo.Text = ""

        '=========Added by preeti gupta==================
        Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, Nothing)

        If DateTime = "1" Then
            dtpFromDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
            dtpFromDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Else
            dtpFromDate.CustomFormat = "dd/MM/yyyy"
            dtpToDate.CustomFormat = "dd/MM/yyyy"

        End If
        '==========================================================
    End Sub
    Public Function isVendorInvoiceNo(ByVal strVendor As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = " select isvendorInvoiceNo from tspl_vendor_master where Vendor_Code='" & strVendor & "'"
        Dim rValue As Double = 0
        rValue = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
        If rValue = 1 Then
            Return True
        Else
            Return False
        End If
    End Function
    Function allowToSave() As Boolean
        Try
            ' = KUNAL > TICKET : BM00000009575 ====
            If AllowFutureDateTransaction(dtpDocDate.Value, Nothing) = False Then
                dtpDocDate.Focus()
                Return False
            End If

            If clsCommon.myLen(fndVendor.Value) <= 0 Then
                Throw New Exception("Vendor Name Can't left blank")
            End If

            If clsCommon.myLen(txtVendorInvoiceNo.Text) <= 0 AndAlso isVendorInvoiceNo(fndVendor.Value, Nothing) Then
                Throw New Exception("Please Enter vendor invoice no")
            End If
            If gvItem.Rows.Count <= 0 Then
                Throw New Exception("Please select atleast one SRN ")
            End If
            '' done by Parteek for KDIL 15/01/2018
            If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                Dim dt As New DataTable
                Dim count As String = ""
                Dim qry1 As String = "select concat('''',replace('" & fndSRNNo.Value & "',', ',''', '''),'''') as FinalSRN "
                count = clsDBFuncationality.getSingleValue(qry1)
                Dim qry As String = "select SRN_No from TSPL_BULK_MILK_PURCHASE_INVOICE_detail where srn_no in (" & count & ")"
                dt = clsDBFuncationality.GetDataTable(qry)
                For Each dr As DataRow In dt.Rows
                    Dim docNo As String = clsCommon.myCstr(dr("SRN_No"))
                    If clsCommon.myLen(docNo) > 0 Then
                        Throw New Exception(" Already Used SRN No: " & docNo & "")
                    End If
                Next
            End If
            '' End 
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Function
    Sub deleteData()
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(fndDocNo.Value) > 0 Then

                If clsMilkPurchaseInvoiceHead.deleteData(fndDocNo.Value, trans) Then
                    myMessages.delete()
                    trans.Commit()
                    reset()
                Else
                    clsCommon.MyMessageBoxShow("Can't delete the record")
                    trans.Rollback()
                End If
            Else

                clsCommon.MyMessageBoxShow("Please Select a document to delete")
                trans.Rollback()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            trans.Rollback()
        End Try
    End Sub
    Sub postData()
        Try
            Dim qry As String = ""
            Dim msg As String = ""
            Dim dt As DataTable = Nothing

            If (myMessages.postConfirm()) Then
                If Not allowToSave() Then
                    Exit Sub
                End If
                SaveData(True)
                '              Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                If (clsMilkPurchaseInvoiceHead.PostData(fndDocNo.Value, MyBase.Form_ID)) Then
                    '                   trans.Commit()
                    msg = "Successfully Posted"
                Else
                    'trans.Rollback()
                    qry = "select No_Of_Level, LEVEL from TSPL_APPROVAL_LEVEL_SCREEN where User_Code='" + objCommonVar.CurrentUserCode + "' and Trans_Code='" + MyBase.Form_ID + "' "
                    dt = clsDBFuncationality.GetDataTable(qry)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        Dim level As String = dt.Rows(0)("LEVEL").ToString()
                        Dim NoOflevel As Integer = clsCommon.myCdbl(dt.Rows(0)("No_Of_Level"))
                        If clsCommon.CompairString(level, "Level1") = CompairStringResult.Equal Then
                            msg = "Level 1 Approval done. "
                            If NoOflevel = 1 Then
                                msg += "Successfully Posted. "
                            Else
                                msg += "Level 2 Approval Required."
                            End If
                        ElseIf clsCommon.CompairString(level, "Level2") = CompairStringResult.Equal Then
                            msg = "Level 2 Approval done. "
                            If NoOflevel = 2 Then
                                msg += "Successfully Posted "
                            Else
                                msg += "Level 3 Approval Required."
                            End If
                        Else
                            msg = "Level 3 Approval done. Successfully Posted. "
                        End If
                    End If
                End If
                common.clsCommon.MyMessageBoxShow(msg)
                LoadData(fndDocNo.Value, NavigatorType.Current)
                'If (common.clsCommon.MyMessageBoxShow("Do you want to print", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes) Then
                '    PrintDataNew()
                'End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    'created by preeti gupta ticket no[BM00000004523]
    '=================changes by shivani
    Sub printData()
        If clsCommon.myLen(fndDocNo.Value) > 0 Then
            'Dim strQuery As String = "select Tspl_Gate_Entry_Details.Gate_Entry_No,convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,103) as Date_And_Time,tspl_Bulk_milk_purchase_Invoice_head.Total_AMT ,tspl_Bulk_milk_purchase_Invoice_head.Total_QTY,tspl_Bulk_milk_purchase_Invoice_head.DOC_NO ,convert(varchar,tspl_Bulk_milk_purchase_Invoice_head.DOC_DATE,103) as DOC_DATE ,tspl_Bulk_milk_purchase_Invoice_head.Vendor_Invoice_No,tspl_Bulk_milk_purchase_Invoice_Detail.Item_Code ,TSPL_ITEM_MASTER.Item_Desc  ,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2  ,TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add4,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add4,'') else ' ' end  as Loc_Add,TSPL_LOCATION_MASTER.TIN_No as Loc_TinNo,TSPL_LOCATION_MASTER.Pin_Code  as Loc_PINCode,TSPL_LOCATION_MASTER.Email as Loc_Email,case when ISNULL(TSPL_LOCATION_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_LOCATION_MASTER.Phone1 end +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_LOCATION_MASTER.Phone2 Else'' End as  Loc_Phn,TSPL_VENDOR_MASTER.Vendor_Name ,TSPL_VENDOR_MASTER.add1 +case when len(TSPL_VENDOR_MASTER.add2)>0 then ', '+TSPL_VENDOR_MASTER.add2 else '' end +case when LEN(isnull(TSPL_VENDOR_MASTER.Add3,''))>0 then ', '+isnull(TSPL_VENDOR_MASTER.Add3,'') else ' ' end   as Ven_Add,TSPL_VENDOR_MASTER.Tin_No as Ven_TINNo ,TSPL_VENDOR_MASTER.Email as Ven_Email ,case when ISNULL(TSPL_VENDOR_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_VENDOR_MASTER.Phone1 end +  Case When   ISNULL(TSPL_VENDOR_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_VENDOR_MASTER.Phone2 Else'' End as Ven_Phn,TSPL_Bulk_MILK_SRN.SRN_NO ,convert(varchar,TSPL_Bulk_MILK_SRN.SRN_Date,103) as SRN_Date ,TSPL_Bulk_MILK_SRN.Tanker_No ,t_FAT.Param_Field_Value As FAT,t_SNF .Param_Field_Value As SNF,TSPL_Weighment_Detail.Net_Weight  as Milk_qty,'For  FAT' +convert(varchar,TSPL_Bulk_Price_MASTER.Fat_Percentage) + ' % &  SNF' +  convert(varchar,TSPL_Bulk_Price_MASTER.Snf_Percentage)+ ' %' as 'MilkRate' ,"
            'strQuery += " 'For ' +convert(varchar,TSPL_Bulk_Price_MASTER.Fat_Weightage ) + ' & ' +  convert(varchar,TSPL_Bulk_Price_MASTER.Snf_Weightage ) as 'Weightage',tspl_Bulk_milk_purchase_Invoice_Detail.NetRate,tspl_Bulk_milk_purchase_Invoice_Detail.Actual_Amount,tspl_Bulk_milk_purchase_Invoice_head.RoundOffAmount ,(TSPL_Bulk_MILK_SRN.Incentive+TSPL_Bulk_MILK_SRN.Deduction-TSPL_Bulk_MILK_SRN.SpecialDeduction)as Ded_Inc,TSPL_Bulk_MILK_SRN.BasicRate,TSPL_Bulk_MILK_SRN.Fat_KG,TSPL_Bulk_MILK_SRN.SNF_KG"
            'strQuery += " from tspl_Bulk_milk_purchase_Invoice_Detail"
            'strQuery += " left outer join tspl_Bulk_milk_purchase_Invoice_head on tspl_Bulk_milk_purchase_Invoice_head.DOC_NO =tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO "
            'strQuery += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code =tspl_Bulk_milk_purchase_Invoice_Detail.Item_Code "
            'strQuery += " left outer join TSPL_COMPANY_MASTER  on TSPL_COMPANY_MASTER.Comp_Code =tspl_Bulk_milk_purchase_Invoice_head.Comp_Code "
            'strQuery += " left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER .Location_Code =tspl_Bulk_milk_purchase_Invoice_head.loc_code "
            'strQuery += " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER .Vendor_Code =tspl_Bulk_milk_purchase_Invoice_head.vendor_code "
            'strQuery += " left outer join TSPL_Bulk_MILK_SRN  on TSPL_Bulk_MILK_SRN.SRN_NO =tspl_Bulk_milk_purchase_Invoice_Detail.SRN_NO "
            'strQuery += " left outer join Tspl_Gate_Entry_Details on Tspl_Gate_Entry_Details.Gate_Entry_No =TSPL_Bulk_MILK_SRN.Gate_Entry_No "

            'strQuery += " Left Outer Join (Select TSPL_QC_Parameter_Detail.* From TSPL_Bulk_MILK_SRN Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No  =        TSPL_Bulk_MILK_SRN.QC_No  And        TSPL_QC_Parameter_Detail.Param_Type = 'FAT') t_FAT      On t_FAT.QC_No   = TSPL_Bulk_MILK_SRN.QC_No "
            'strQuery += " Left Outer Join (Select TSPL_QC_Parameter_Detail.* From TSPL_Bulk_MILK_SRN Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No  =        TSPL_Bulk_MILK_SRN.QC_No  And        TSPL_QC_Parameter_Detail.Param_Type = 'SNF') t_SNF      On t_SNF .QC_No   = TSPL_Bulk_MILK_SRN.QC_No "
            'strQuery += " left outer join TSPL_Bulk_Price_MASTER on TSPL_Bulk_Price_MASTER.Price_Code=tspl_Bulk_milk_purchase_Invoice_Detail.Price_code "
            'strQuery += " left outer join TSPL_Weighment_Detail on TSPL_Weighment_Detail.Weighment_No =TSPL_Bulk_MILK_SRN.Weighment_No  "
            'strQuery += " where  tspl_bulk_milk_purchase_invoice_head.doc_no='" & fndDocNo.Value & "' order by Date_And_Time"
            '======================changes by shivani [BM00000008444]
            Dim TankerFromMaster As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GateEntryTankerFromTankerMaster, clsFixedParameterCode.GateEntryTankerFromTankerMaster, Nothing))
            Dim strQuery As String = ""
            If TankerFromMaster = 0 Then
                strQuery = " ;with BulkMilkSRN as (select * from tspl_bulk_milk_srn where isnull(TSPL_Bulk_MILK_SRN.srn_return_no,'')=''  ) select isnull(TSPL_BULK_MILK_PURCHASE_INVOICE_head.Purchase_Tax_Invoice,'') as Purchase_Tax_Invoice,isnull(TSPL_BULK_MILK_PURCHASE_INVOICE_head.Purchase_Tax_Invoice_Type,'') as Purchase_Tax_Invoice_Type ,  TSPL_ITEM_MASTER.HSN_Code,tspl_state_master_for_location_state.GST_STATE_Code as LOC_GST_State_Code,TSPL_LOCATION_MASTER.GSTNO as Loc_GstInNo,TSPL_VENDOR_MASTER.GSTFinalNo AS Vendor_GSTIN_NO,TSPL_STATE_MASTER.GST_STATE_Code AS Vendor_GST_StateCode ,case when isnull(Tspl_Gate_Entry_Details.Gate_Entry_No,'')='' then 'SRN No' else 'GRN No' end as Gate_SRN_No ,case when isnull(convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,103),'')='' then 'SRN Date' else 'GRN Date' end as Gate_SRN_Date,tspl_Bulk_milk_purchase_Invoice_head.Total_AMT as Sum_of_ActualAmt ,tspl_Bulk_milk_purchase_Invoice_head.Total_QTY,tspl_Bulk_milk_purchase_Invoice_head.DOC_NO ,convert(varchar,tspl_Bulk_milk_purchase_Invoice_head.DOC_DATE,103) as DOC_DATE ,tspl_bulk_milk_purchase_invoice_head.EWayBillNo,convert(varchar,tspl_bulk_milk_purchase_invoice_head.EWayBillDate,103) as EWayBillDate,tspl_bulk_milk_purchase_invoice_head.Electronic_Ref_No,tspl_Bulk_milk_purchase_Invoice_head.Vendor_Invoice_No,tspl_Bulk_milk_purchase_Invoice_Detail.Item_Code ,TSPL_ITEM_MASTER.Item_Desc  ,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2  ,TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add4,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add4,'') else ' ' end  as Loc_Add,TSPL_LOCATION_MASTER.TIN_No as Loc_TinNo,TSPL_LOCATION_MASTER.Pin_Code  as Loc_PINCode,TSPL_LOCATION_MASTER.Email as Loc_Email,case when ISNULL(TSPL_LOCATION_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_LOCATION_MASTER.Phone1 end +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_LOCATION_MASTER.Phone2 Else'' End as  Loc_Phn,TSPL_VENDOR_MASTER.Vendor_Name ,TSPL_VENDOR_MASTER.add1 +case when len(TSPL_VENDOR_MASTER.add2)>0 then ', '+TSPL_VENDOR_MASTER.add2 else '' end +case when LEN(isnull(TSPL_VENDOR_MASTER.Add3,''))>0 then ', '+isnull(TSPL_VENDOR_MASTER.Add3,'') else ' ' end   as Ven_Add,TSPL_VENDOR_MASTER.Tin_No as Ven_TINNo ,TSPL_VENDOR_MASTER.Email as Ven_Email ,case when ISNULL(TSPL_VENDOR_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_VENDOR_MASTER.Phone1 end +  Case When   ISNULL(TSPL_VENDOR_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_VENDOR_MASTER.Phone2 Else'' End as Ven_Phn,BulkMilkSrn.SRN_NO ,convert(varchar,BulkMilkSrn.SRN_Date,103) as SRN_Date ,BulkMilkSrn.Tanker_No  , case when isnull(BulkMilkSrn.SRN_NO,'')='' then Tspl_Gate_Entry_Details.Gate_Entry_No else BulkMilkSrn.SRN_NO end as Gate_Entry_No,case when isnull(convert(varchar,BulkMilkSrn.SRN_Date,103),'')='' then convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,103) else convert(varchar,BulkMilkSrn.SRN_Date,103) end as Date_And_Time,"
                If RunBulkProcOnAdjustFATCLR = 0 Then
                    strQuery += " case when isnull(t_FAT.Param_Field_Value,'')='' then tspl_Bulk_milk_purchase_Invoice_Detail.fat_per else t_FAT.Param_Field_Value end As FAT ,case when isnull(t_SNF .Param_Field_Value,'')='' then tspl_Bulk_milk_purchase_Invoice_Detail.snf_Per else t_SNF .Param_Field_Value end As SNF ,"
                Else
                    strQuery += "  tspl_Bulk_milk_purchase_Invoice_Detail.fat_per As FAT ,tspl_Bulk_milk_purchase_Invoice_Detail.snf_Per AS SNF,"
                End If
                strQuery += " case when isnull(TSPL_Weighment_Detail.Net_Weight,'')='' then tspl_Bulk_milk_purchase_Invoice_Detail.Invoice_Qty else TSPL_Weighment_Detail.Net_Weight end as Milk_qty,'For  FAT' +convert(varchar,TSPL_Bulk_Price_MASTER.Fat_Percentage) + ' % &  SNF' " & _
                                     " +  convert(varchar,TSPL_Bulk_Price_MASTER.Snf_Percentage)+ ' %' as 'MilkRate' , 'For ' +convert(varchar,TSPL_Bulk_Price_MASTER.Fat_Weightage ) + ' & ' +  convert(varchar,TSPL_Bulk_Price_MASTER.Snf_Weightage ) as 'Weightage',tspl_Bulk_milk_purchase_Invoice_Detail.NetRate,tspl_Bulk_milk_purchase_Invoice_Detail.Actual_Amount,tspl_Bulk_milk_purchase_Invoice_head.RoundOffAmount ,(BulkMilkSrn.Incentive+BulkMilkSrn.Deduction-BulkMilkSrn.SpecialDeduction)as Ded_Inc,case when isnull(BulkMilkSrn.BasicRate,'')='' then tspl_Bulk_milk_purchase_Invoice_Detail.NetRate else BulkMilkSrn.BasicRate end as BasicRate,BulkMilkSrn.Fat_KG,BulkMilkSrn.SNF_KG " & _
                                     " from tspl_Bulk_milk_purchase_Invoice_Detail  left outer join tspl_Bulk_milk_purchase_Invoice_head on tspl_Bulk_milk_purchase_Invoice_head.DOC_NO =tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code =tspl_Bulk_milk_purchase_Invoice_Detail.Item_Code   left outer join TSPL_COMPANY_MASTER  on TSPL_COMPANY_MASTER.Comp_Code =tspl_Bulk_milk_purchase_Invoice_head.Comp_Code   left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER .Location_Code =tspl_Bulk_milk_purchase_Invoice_head.loc_code  left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER .Vendor_Code =tspl_Bulk_milk_purchase_Invoice_head.vendor_code  left join TSPL_VENDOR_BANK_MASTER on TSPL_VENDOR_MASTER.BANK_CODE=TSPL_VENDOR_BANK_MASTER.BANK_CODE left outer join BulkMilkSrn  on BulkMilkSrn.SRN_NO =tspl_Bulk_milk_purchase_Invoice_Detail.SRN_NO   left outer join Tspl_Gate_Entry_Details on Tspl_Gate_Entry_Details.Gate_Entry_No =BulkMilkSrn.Gate_Entry_No  Left Outer Join (Select TSPL_QC_Parameter_Detail.* From BulkMilkSrn Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No  =        BulkMilkSrn.QC_No  And        TSPL_QC_Parameter_Detail.Param_Type = 'FAT') t_FAT      On t_FAT.QC_No   = BulkMilkSrn.QC_No  Left Outer Join (Select TSPL_QC_Parameter_Detail.* From BulkMilkSrn Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No  =        BulkMilkSrn.QC_No  And        TSPL_QC_Parameter_Detail.Param_Type = 'SNF') t_SNF      On t_SNF .QC_No   = BulkMilkSrn.QC_No  left outer join TSPL_Bulk_Price_MASTER on TSPL_Bulk_Price_MASTER.Price_Code=tspl_Bulk_milk_purchase_Invoice_Detail.Price_code  left outer join TSPL_Weighment_Detail on TSPL_Weighment_Detail.Weighment_No =BulkMilkSrn.Weighment_No  left outer join TSPL_STATE_MASTER on TSPL_VENDOR_MASTER.State_Code = TSPL_STATE_MASTER.STATE_CODE left outer join tspl_state_master as tspl_state_master_for_location_state on  tspl_state_master_for_location_state.state_code=tspl_location_master.state  where tspl_bulk_milk_purchase_invoice_head.DOC_NO in ('" & fndDocNo.Value & "') order by Vendor_name,Date_And_Time"
                ''" where  tspl_bulk_milk_purchase_invoice_head.doc_no='" & fndDocNo.Value & "' order by Date_And_Time"



            Else
                Dim frm As New RptBulkMilkMultiplePurchaseInvoice
                strQuery = frm.GetQuery("", "", fndDocNo.Value)
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQuery)

            frmCrystalReportViewer.funreport(CrystalReportFolder.MilkProcurement, dt, "rptBulkMilkPurchaseInvoice", "Purchase Invoice", clsCommon.myCDate(dtpDocDate.Value))


        Else
            clsCommon.MyMessageBoxShow("Please select an invoice to print")
        End If
    End Sub
    Sub SaveData(ByVal isPost As Boolean)
        Try
            Dim trans As SqlTransaction = Nothing
            obj = New clsMilkPurchaseInvoiceHead
            If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                obj.isNewEntry = True
            Else
                obj.isNewEntry = False
            End If
            trans = clsDBFuncationality.GetTransactin()
            Dim dt As Date = clsCommon.GETSERVERDATE(trans, "dd/MMM/yyyy hh:mm:ss tt")
            Dim PIDate As Date = dtpDocDate.Value
            If obj.isNewEntry Then
                Dim strSRN As String = ""
                For ii As Integer = 0 To gvItem.Rows.Count - 1
                    strSRN = clsCommon.myCstr(gvItem.Rows(ii).Cells(colSRNNo).Value)
                    If clsCommon.myLen(strSRN) > 0 Then
                        Exit For
                    End If
                Next
                'Dim Isjobwork As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select isnull(IsAgainstJobWork,0) from tspl_bulk_milk_srn left outer join tspl_gate_entry_details on tspl_bulk_milk_srn.gate_entry_no=tspl_gate_entry_details.gate_entry_no  where srn_no='" & strSRN & "'", trans))

                If chkSRNTrade.Checked Then
                    If isVendorInvoiceNo(fndVendor.Value, trans) Then
                        obj.DOC_NO = clsERPFuncationality.GetNextCode(trans, PIDate, clsDocType.BulkMilkPurchaseInvoiceTrade, clsDocTransactionType.WithVendorInvoiceNo, fndLocation.Value)
                    Else
                        obj.DOC_NO = clsERPFuncationality.GetNextCode(trans, PIDate, clsDocType.BulkMilkPurchaseInvoiceTrade, clsDocTransactionType.WithoutVendorInvoiceNo, fndLocation.Value)
                    End If
                Else
                    If chkJobWork.Checked Then
                        obj.DOC_NO = clsERPFuncationality.GetNextCode(trans, PIDate, clsDocType.BulkMilkPurchaseInvoice, clsDocTransactionType.BulkProcJobWorkOutward, txtSubLocation.Value)
                    Else
                        If isVendorInvoiceNo(fndVendor.Value, trans) Then
                            obj.DOC_NO = clsERPFuncationality.GetNextCode(trans, PIDate, clsDocType.BulkMilkPurchaseInvoice, clsDocTransactionType.WithVendorInvoiceNo, fndLocation.Value)
                        Else
                            obj.DOC_NO = clsERPFuncationality.GetNextCode(trans, PIDate, clsDocType.BulkMilkPurchaseInvoice, clsDocTransactionType.WithoutVendorInvoiceNo, fndLocation.Value)
                        End If
                    End If
                   
                End If
                'If isVendorInvoiceNo(fndVendor.Value, trans) Then
                '    obj.DOC_NO = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.BulkMilkPurchaseInvoice, clsDocTransactionType.WithVendorInvoiceNo, fndLocation.Value)
                'Else
                '    obj.DOC_NO = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.BulkMilkPurchaseInvoice, clsDocTransactionType.WithoutVendorInvoiceNo, fndLocation.Value)
                'End If
                If clsCommon.myLen(obj.DOC_NO) <= 0 Then
                    clsCommon.MyMessageBoxShow("Error In Document No Genertion")
                    Exit Sub
                End If
            Else
                obj.DOC_NO = clsCommon.myCstr(fndDocNo.Value)
            End If
            obj.IsAgainstJobWork = IIf(chkJobWork.Checked, 1, 0)
            obj.Joblocation_Code = txtSubLocation.Value
            fndDocNo.Value = obj.DOC_NO
            obj.DOC_DATE = clsCommon.GetPrintDate(dtpDocDate.Value, "dd/MMM/yyyy")
            obj.vendor_code = clsCommon.myCstr(fndVendor.Value)
            obj.Loc_Code = clsCommon.myCstr(fndLocation.Value)
            obj.Vendor_Invoice_No = clsCommon.myCstr(txtVendorInvoiceNo.Text)
            obj.SRN_From_Date = clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy hh:mm:ss tt")
            obj.SRN_TO_Date = clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy hh:mm:ss tt")
            obj.Total_FAT_KG = clsCommon.myCdbl(txtTotalFatKg.Text)
            obj.Total_SNF_KG = clsCommon.myCdbl(txtTotalSNFKg.Text)
            obj.Total_QTY = clsCommon.myCdbl(txtTotalQty.Text)
            obj.Total_AMT = clsCommon.myCdbl(txtTotalAmt.Text)
            obj.RoundOffAmount = clsCommon.myCdbl(txtRoundOffAmt.Text)
            obj.isSRNTradeInvoice = IIf(chkSRNTrade.Checked, 1, 0)
            If Not isPost Then
                obj.isPosted = 0
                ' Else
                'obj.isPosted = 1
                'obj.Posting_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
            End If
            obj.Modified_By = objCommonVar.CurrentUserCode
            obj.Modified_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
            obj.Comp_Code = objCommonVar.CurrentCompanyCode
            If obj.isNewEntry Then
                obj.Created_By = objCommonVar.CurrentUserCode
                obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
            End If
            Dim i As Integer = 0
            Dim objDetail As New clsMilkPurchaseInvoiceDetail
            obj.arrDetail = New List(Of clsMilkPurchaseInvoiceDetail)
            For i = 0 To gvItem.Rows.Count - 1
                objDetail = New clsMilkPurchaseInvoiceDetail
                objDetail.DOC_NO = clsCommon.myCstr(obj.DOC_NO)
                objDetail.SL_NO = clsCommon.myCstr(gvItem.Rows(i).Cells(colSlNo).Value)
                objDetail.SRN_NO = clsCommon.myCstr(gvItem.Rows(i).Cells(colSRNNo).Value)
                objDetail.SRN_Date = clsCommon.GetPrintDate(gvItem.Rows(i).Cells(colSRNDATe).Value, "dd/MMM/yyyy hh:mm:ss tt")
                objDetail.Item_Code = clsCommon.myCstr(gvItem.Rows(i).Cells(colItemCode).Value)
                objDetail.Item_Desc = clsCommon.myCstr(gvItem.Rows(i).Cells(colItemDesc).Value)
                objDetail.UOM = clsCommon.myCstr(gvItem.Rows(i).Cells(colUOM).Value)
                objDetail.Gross_Weight = clsCommon.myCdbl(gvItem.Rows(i).Cells(colGrossWeight).Value)
                objDetail.Tare_Weight = clsCommon.myCdbl(gvItem.Rows(i).Cells(colTareWeight).Value)
                objDetail.Net_Weight = clsCommon.myCdbl(gvItem.Rows(i).Cells(colNetWeight).Value)
                objDetail.Invoice_Qty = clsCommon.myCdbl(gvItem.Rows(i).Cells(colQty).Value)
                objDetail.fat_per = clsCommon.myCdbl(gvItem.Rows(i).Cells(colFat).Value)
                objDetail.fat_KG = clsCommon.myCdbl(gvItem.Rows(i).Cells(colFatKG).Value)
                objDetail.fat_Rate = clsCommon.myCdbl(gvItem.Rows(i).Cells(colFatRate).Value)
                objDetail.snf_Per = clsCommon.myCdbl(gvItem.Rows(i).Cells(colSNF).Value)
                objDetail.SNF_KG = clsCommon.myCdbl(gvItem.Rows(i).Cells(colSNFKG).Value)
                objDetail.SNF_Rate = clsCommon.myCdbl(gvItem.Rows(i).Cells(colSNFRate).Value)
                objDetail.Amount = clsCommon.myCdbl(gvItem.Rows(i).Cells(colAmt).Value)
                objDetail.Deduction = clsCommon.myCdbl(gvItem.Rows(i).Cells(colDeduc).Value)
                objDetail.Incentive = clsCommon.myCdbl(gvItem.Rows(i).Cells(colIncen).Value)
                objDetail.Special_Deduction = clsCommon.myCdbl(gvItem.Rows(i).Cells(colSpecialDeduc).Value)
                objDetail.Actual_Amount = clsCommon.myCdbl(gvItem.Rows(i).Cells(colActAmt).Value)
                objDetail.price_code = clsCommon.myCstr(gvItem.Rows(i).Cells(colPriceCode).Value)
                objDetail.NetRate = clsCommon.myCdbl(gvItem.Rows(i).Cells(colNetRate).Value)
                objDetail.CHAMBER_DESC = clsCommon.myCstr(gvItem.Rows(i).Cells(colChamberDesc).Value)
                obj.arrDetail.Add(objDetail)
            Next
            If clsMilkPurchaseInvoiceHead.saveData(obj, trans) Then
                trans.Commit()
                If Not isPost Then
                    If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                        clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    Else
                        clsCommon.MyMessageBoxShow("Data Updated Successfully")
                    End If
                End If
                loadData(obj.DOC_NO, NavigatorType.Current)
                btnSave.Text = "Update"
                fndDocNo.MyReadOnly = True
                btnDelete.Enabled = True
                btnPost.Enabled = True
                btnPrint.Enabled = True
                'btnBillOfSupply.Enabled = True
                Exit Sub
            End If
            clsCommon.MyMessageBoxShow("Data Not Saved ")
            btnSave.Text = "Save"
            btnDelete.Enabled = False
            btnPost.Enabled = False
            btnPrint.Enabled = False
            btnBillOfSupply.Enabled = False
            fndDocNo.MyReadOnly = False
            trans.Rollback()
        Catch ex As Exception
            If isPost Then
                Throw New Exception(ex.Message)
            Else
                clsCommon.MyMessageBoxShow(ex.Message)
            End If

        End Try
    End Sub

    Sub loadData(ByVal str As String, ByVal navtype As NavigatorType)
        obj = clsMilkPurchaseInvoiceHead.getData(str, navtype)
        If obj IsNot Nothing Then
            reset()
            If obj.isPosted = 0 Then
                isSRNselected = True
            Else
                isSRNselected = False
            End If
            isLoadData = True
            chkJobWork.Checked = IIf(obj.IsAgainstJobWork = 1, True, False)
            txtSubLocation.Value = obj.Joblocation_Code
            If clsCommon.myLen(txtSubLocation.Value) > 0 Then
                lblSubLocation.Text = clsLocation.GetName(txtSubLocation.Value, Nothing)
            End If
            fndLocation.Enabled = False
            fndDocNo.Value = obj.DOC_NO
            dtpDocDate.Value = obj.DOC_DATE
            fndVendor.Value = obj.vendor_code
            lblVendorName.Text = clsVendorMaster.GetName(obj.vendor_code, Nothing)
            fndLocation.Value = obj.Loc_Code
            lblLocationName.Text = clsLocation.GetName(obj.Loc_Code, Nothing)
            dtpFromDate.Value = obj.SRN_From_Date
            dtpToDate.Value = obj.SRN_TO_Date
            txtTotalFatKg.Text = clsCommon.myFormat(MyMath.RoundDown(obj.Total_FAT_KG, 3), False, True, False, 3, True)
            txtTotalSNFKg.Text = clsCommon.myFormat(MyMath.RoundDown(obj.Total_SNF_KG, 3), False, True, False, 3, True)
            txtTotalQty.Text = clsCommon.myFormat(obj.Total_QTY)
            txtRoundOffAmt.Text = clsCommon.myFormat(obj.RoundOffAmount)
            txtTotalAmt.Text = clsCommon.myFormat(obj.Total_AMT)
            txtVendorInvoiceNo.Text = obj.Vendor_Invoice_No
            loadBlankGrid()
            If obj.isSRNTradeInvoice = 1 Then
                chkSRNTrade.Checked = True
            Else
                chkSRNTrade.Checked = False
            End If
            chkSRNTrade.Enabled = False
            Dim SRNs As String = ""

            If obj.arrDetail IsNot Nothing Then
                Dim arr As New List(Of String)
                For i As Integer = 0 To obj.arrDetail.Count - 1
                    gvItem.Rows.AddNew()
                    gvItem.Rows(i).Cells(colSlNo).Value = obj.arrDetail(i).SL_NO
                    gvItem.Rows(i).Cells(colSRNNo).Value = obj.arrDetail(i).SRN_NO
                    If Not arr.Contains(obj.arrDetail(i).SRN_NO) Then
                        arr.Add(obj.arrDetail(i).SRN_NO)
                        SRNs = SRNs & obj.arrDetail(i).SRN_NO
                    End If
                    If i <> obj.arrDetail.Count - 1 Then
                        SRNs = SRNs & ", "
                    End If
                    gvItem.Rows(i).Cells(colSRNDATe).Value = obj.arrDetail(i).SRN_Date
                    gvItem.Rows(i).Cells(colItemCode).Value = obj.arrDetail(i).Item_Code
                    gvItem.Rows(i).Cells(colItemDesc).Value = obj.arrDetail(i).Item_Desc
                    gvItem.Rows(i).Cells(colHSN).Value = clsItemMaster.GetItemHSNCode(obj.arrDetail(i).Item_Code, Nothing)
                    gvItem.Rows(i).Cells(colUOM).Value = obj.arrDetail(i).UOM
                    gvItem.Rows(i).Cells(colGrossWeight).Value = clsCommon.myFormat(obj.arrDetail(i).Gross_Weight)
                    gvItem.Rows(i).Cells(colTareWeight).Value = clsCommon.myFormat(obj.arrDetail(i).Tare_Weight)
                    gvItem.Rows(i).Cells(colNetWeight).Value = clsCommon.myFormat(obj.arrDetail(i).Net_Weight)
                    gvItem.Rows(i).Cells(colQty).Value = clsCommon.myFormat(obj.arrDetail(i).Invoice_Qty)
                    gvItem.Rows(i).Cells(colFat).Value = clsCommon.myFormat(obj.arrDetail(i).fat_per)
                    gvItem.Rows(i).Cells(colFatKG).Value = clsCommon.myFormat(obj.arrDetail(i).fat_KG, False, True, False, 3, True)
                    gvItem.Rows(i).Cells(colFatRate).Value = clsCommon.myFormat(obj.arrDetail(i).fat_Rate)
                    gvItem.Rows(i).Cells(colSNF).Value = clsCommon.myFormat(obj.arrDetail(i).snf_Per)
                    gvItem.Rows(i).Cells(colSNFKG).Value = clsCommon.myFormat(obj.arrDetail(i).SNF_KG, False, True, False, 3, True)
                    gvItem.Rows(i).Cells(colSNFRate).Value = clsCommon.myFormat(obj.arrDetail(i).SNF_Rate)
                    gvItem.Rows(i).Cells(colAmt).Value = clsCommon.myFormat(obj.arrDetail(i).Amount)
                    gvItem.Rows(i).Cells(colDeduc).Value = clsCommon.myFormat(obj.arrDetail(i).Deduction)
                    gvItem.Rows(i).Cells(colIncen).Value = clsCommon.myFormat(obj.arrDetail(i).Incentive)
                    gvItem.Rows(i).Cells(colSpecialDeduc).Value = clsCommon.myFormat(obj.arrDetail(i).Special_Deduction)
                    gvItem.Rows(i).Cells(colActAmt).Value = clsCommon.myFormat(obj.arrDetail(i).Actual_Amount)
                    gvItem.Rows(i).Cells(colPriceCode).Value = obj.arrDetail(i).price_code
                    gvItem.Rows(i).Cells(colNetRate).Value = clsCommon.myFormat(obj.arrDetail(i).NetRate)
                    gvItem.Rows(i).Cells(colTnkrNo).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select tanker_no from TSPL_Bulk_MILK_SRN where SRN_NO='" & obj.arrDetail.Item(i).SRN_NO & "' "))    '' RICHA AGARWAL IIf(chkSRNTrade.Checked, "NA", clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select tanker_no from TSPL_Bulk_MILK_SRN where SRN_NO='" & obj.arrDetail.Item(i).SRN_NO & "' ")))
                    gvItem.Rows(i).Cells(colChamberDesc).Value = obj.arrDetail(i).CHAMBER_DESC
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable("select TSPL_Bulk_MILK_SRN.SRN_NO, (TSPL_Bulk_MILK_SRN.Net_Weight-coalesce(SUM(tdl.invoice_qty),0)) as pending_qty from TSPL_Bulk_MILK_SRN left join ( select invoice_qty,SRN_NO  from Tspl_bulk_milk_purchase_invoice_detail left outer join Tspl_bulk_milk_purchase_invoice_head on Tspl_bulk_milk_purchase_invoice_head.DOC_NO=tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO  where Tspl_bulk_milk_purchase_invoice_head.isPosted=1) as  tdl  on TSPL_Bulk_MILK_SRN.SRN_NO=tdl.srn_no  where TSPL_Bulk_MILK_SRN.isPosted=1 and TSPL_Bulk_MILK_SRN.SRN_NO='" & obj.arrDetail(i).SRN_NO & "'  group by TSPL_Bulk_MILK_SRN.SRN_NO ,TSPL_Bulk_MILK_SRN.Net_Weight	")
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        gvItem.Rows(i).Cells(colPendingQty).Value = clsCommon.myFormat(clsCommon.myCdbl(dt.Rows(0)("pending_qty")))
                    End If

                Next
                fndSRNNo.Value = SRNs
            End If
            btnSave.Text = "Update"
            btnDelete.Enabled = True
            btnPrint.Enabled = True

            If obj.isPosted = 1 Then
                btnSave.Enabled = False
                btnPost.Enabled = False
                btnDelete.Enabled = False
                lblPending.Status = ERPTransactionStatus.Approved
                Dim strPurchaseTaxInvoiceNo As String = clsDBFuncationality.getSingleValue(" select isnull(TSPL_BULK_MILK_PURCHASE_INVOICE_head.Purchase_Tax_Invoice,'') as Purchase_Tax_Invoice from TSPL_BULK_MILK_PURCHASE_INVOICE_head where TSPL_BULK_MILK_PURCHASE_INVOICE_head.DOC_NO = '" + fndDocNo.Value + "'  ")
                If clsCommon.myLen(strPurchaseTaxInvoiceNo) > 0 Then
                    btnBillOfSupply.Enabled = True
                Else
                    btnBillOfSupply.Enabled = False
                End If
            Else
                btnSave.Enabled = True
                btnPost.Enabled = True
                btnDelete.Enabled = True
                lblPending.Status = ERPTransactionStatus.Pending
                btnBillOfSupply.Enabled = False
            End If
        End If
        isLoadData = False
    End Sub

    Sub loadBlankGrid()
        gvItem.Rows.Clear()
        gvItem.Columns.Clear()


        Dim repoSLNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSLNo.FormatString = ""
        repoSLNo.HeaderText = "SL.No"
        repoSLNo.Name = colSlNo
        repoSLNo.Width = 60
        repoSLNo.ReadOnly = True
        repoSLNo.BestFit()
        gvItem.MasterTemplate.Columns.Add(repoSLNo)

        Dim repoTnkrNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTnkrNo.FormatString = ""
        repoTnkrNo.HeaderText = "Tanker No"
        repoTnkrNo.Name = colTnkrNo
        repoTnkrNo.Width = 100
        repoTnkrNo.ReadOnly = True
        gvItem.MasterTemplate.Columns.Add(repoTnkrNo)

        Dim repoSRNNO As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSRNNO.FormatString = ""
        repoSRNNO.HeaderText = "SRN No"
        repoSRNNO.Name = colSRNNo
        repoSRNNO.Width = 200
        repoSRNNO.ReadOnly = True
        gvItem.MasterTemplate.Columns.Add(repoSRNNO)

        Dim repoSRNDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoSRNDate.FormatString = "{0:d}"
        repoSRNDate.CustomFormat = "dd/MM/yyyy hh:mm:ss tt"
        repoSRNDate.HeaderText = "SRN Date"
        repoSRNDate.Name = colSRNDATe
        repoSRNDate.Width = 100
        repoSRNDate.ReadOnly = True
        gvItem.MasterTemplate.Columns.Add(repoSRNDate)

        Dim repoChName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoChName.FormatString = ""
        repoChName.HeaderText = "Chamber Desc"
        repoChName.Name = colChamberDesc
        repoChName.Width = 180
        repoChName.ReadOnly = True
        If TankerFromMaster = 0 Then repoChName.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoChName)

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colItemCode
        repoICode.Width = 100
        repoICode.ReadOnly = True
        gvItem.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item Desc"
        repoIName.Name = colItemDesc
        repoIName.Width = 180
        repoIName.ReadOnly = True
        gvItem.MasterTemplate.Columns.Add(repoIName)

        Dim repoHSNCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoHSNCode.FormatString = ""
        repoHSNCode.HeaderText = "HSN Code"
        repoHSNCode.Name = colHSN
        repoHSNCode.Width = 100
        repoHSNCode.ReadOnly = True
        gvItem.MasterTemplate.Columns.Add(repoHSNCode)


        Dim repoUOM As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUOM.FormatString = ""
        repoUOM.HeaderText = "UOM"
        repoUOM.Name = colUOM
        repoUOM.ReadOnly = True
        repoUOM.Width = 80
        repoUOM.WrapText = True
        repoUOM.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoUOM)

        Dim repoGrossWeight As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoGrossWeight.FormatString = ""
        repoGrossWeight.HeaderText = "Gross Weight"
        repoGrossWeight.Name = colGrossWeight
        repoGrossWeight.Width = 100
        repoGrossWeight.ReadOnly = True
        repoGrossWeight.IsVisible = False
        repoGrossWeight.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoGrossWeight)

        Dim repoTareWeight As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTareWeight.FormatString = ""
        repoTareWeight.HeaderText = "Tare Weight"
        repoTareWeight.Name = colTareWeight
        repoTareWeight.ReadOnly = True
        repoTareWeight.Width = 100
        repoTareWeight.WrapText = True
        repoTareWeight.IsVisible = False
        repoTareWeight.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoTareWeight)

        Dim repoNetWeight As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoNetWeight.FormatString = ""
        repoNetWeight.HeaderText = "Net Weight"
        repoNetWeight.Name = colNetWeight
        repoNetWeight.ReadOnly = True
        repoNetWeight.Width = 100
        repoNetWeight.WrapText = True
        repoNetWeight.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoNetWeight)

        Dim repoPendingQty As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPendingQty.FormatString = ""
        repoPendingQty.HeaderText = "Pending Qty"
        repoPendingQty.Name = colPendingQty
        repoPendingQty.ReadOnly = True
        repoPendingQty.WrapText = True
        repoPendingQty.Width = 100
        If TankerFromMaster = 1 Then
            repoPendingQty.IsVisible = False
        End If
        repoPendingQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoPendingQty)

        Dim repoInvoiceQty As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoInvoiceQty.FormatString = ""
        repoInvoiceQty.HeaderText = "Invoice Qty"
        repoInvoiceQty.Name = colQty
        repoInvoiceQty.ReadOnly = True
        repoInvoiceQty.WrapText = True
        repoInvoiceQty.Width = 100
        repoInvoiceQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoInvoiceQty)


        Dim repoPriceCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPriceCode.FormatString = ""
        repoPriceCode.HeaderText = "Price Code"
        repoPriceCode.Name = colPriceCode
        repoPriceCode.ReadOnly = True
        repoPriceCode.Width = 80
        repoPriceCode.WrapText = True
        repoPriceCode.IsVisible = False
        repoPriceCode.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoPriceCode)

        Dim repoFatPer As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFatPer.FormatString = ""
        repoFatPer.HeaderText = "FAT(%)"
        repoFatPer.Name = colFat
        repoFatPer.ReadOnly = True
        repoFatPer.Width = 80
        repoFatPer.WrapText = True
        'repoFatPer.IsVisible = False
        repoFatPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoFatPer)

        Dim repoSNFPer As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSNFPer.FormatString = ""
        repoSNFPer.HeaderText = "SNF(%)"
        repoSNFPer.Name = colSNF
        repoSNFPer.ReadOnly = True
        repoSNFPer.Width = 80
        repoSNFPer.WrapText = True
        'repoSNFPer.IsVisible = False
        repoSNFPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoSNFPer)


        Dim repoFATKG As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoFATKG.FormatString = ""
        repoFATKG.HeaderText = "FAT(KG)"
        repoFATKG.Name = colFatKG
        repoFATKG.ReadOnly = True
        repoFATKG.Width = 80
        repoFATKG.WrapText = True
        repoFATKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoFATKG)

        Dim repoSNFKG As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSNFKG.FormatString = ""
        repoSNFKG.HeaderText = "SNF(KG)"
        repoSNFKG.Name = colSNFKG
        repoSNFKG.ReadOnly = True
        repoSNFKG.Width = 80
        repoSNFKG.WrapText = True
        repoSNFKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoSNFKG)






        Dim repoAmount As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAmount.FormatString = ""
        repoAmount.HeaderText = "Amount"
        repoAmount.Name = colAmt
        repoAmount.ReadOnly = True
        repoAmount.Width = 100
        repoAmount.WrapText = True
        repoAmount.IsVisible = False
        repoAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoAmount)

        Dim repoDeduc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDeduc.FormatString = ""
        repoDeduc.HeaderText = "Deduction"
        repoDeduc.Name = colDeduc
        repoDeduc.ReadOnly = True
        repoDeduc.Width = 80
        repoDeduc.WrapText = True
        repoDeduc.IsVisible = False
        repoDeduc.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoDeduc)

        Dim repoIncen As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIncen.FormatString = ""
        repoIncen.HeaderText = "Incentive"
        repoIncen.Name = colIncen
        repoIncen.ReadOnly = True
        repoIncen.Width = 80
        repoIncen.WrapText = True
        repoIncen.IsVisible = False
        repoIncen.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoIncen)

        Dim repoSpclDeduc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSpclDeduc.FormatString = ""
        repoSpclDeduc.HeaderText = "Special Deduction"
        repoSpclDeduc.Name = colSpecialDeduc
        repoSpclDeduc.ReadOnly = True
        repoSpclDeduc.Width = 80
        repoSpclDeduc.WrapText = True
        repoSpclDeduc.IsVisible = False
        repoSpclDeduc.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoSpclDeduc)


        Dim repoActAmt As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoActAmt.FormatString = ""
        repoActAmt.HeaderText = "Net Rate"
        repoActAmt.Name = colNetRate
        repoActAmt.ReadOnly = True
        repoActAmt.Width = 80
        repoActAmt.WrapText = True
        repoActAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoActAmt)

        Dim repoFATRate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoFATRate.FormatString = ""
        repoFATRate.HeaderText = "FAT Rate"
        repoFATRate.Name = colFatRate
        repoFATRate.ReadOnly = True
        repoFATRate.Width = 80
        repoFATRate.WrapText = True
        repoFATRate.IsVisible = True
        repoFATRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoFATRate)


        Dim repoSNFRate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSNFRate.FormatString = ""
        repoSNFRate.HeaderText = "SNF Rate"
        repoSNFRate.Name = colSNFRate
        repoSNFRate.ReadOnly = True
        repoSNFRate.Width = 80
        repoSNFRate.WrapText = True
        repoSNFRate.IsVisible = True
        repoSNFRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoSNFRate)

        repoActAmt = New GridViewTextBoxColumn()
        repoActAmt.FormatString = ""
        repoActAmt.HeaderText = "Amount"
        repoActAmt.Name = colActAmt
        repoActAmt.ReadOnly = True
        repoActAmt.Width = 80
        repoActAmt.WrapText = True
        repoActAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoActAmt)



        gvItem.AllowAddNewRow = False
        ' gvItem.AllowDeleteRow = False
        gvItem.AllowColumnChooser = True
        gvItem.ShowGroupPanel = False
        gvItem.AllowColumnReorder = True
        gvItem.AllowRowReorder = True
        gvItem.EnableSorting = False
        gvItem.MasterTemplate.ShowRowHeaderColumn = False
        gvItem.MasterTemplate.ShowColumnHeaders = True
        gvItem.EnableAlternatingRowColor = True
        '   gvItem.EnableFiltering = True
        '    gvItem.ShowFilteringRow = True
        gvItem.TableElement.TableHeaderHeight = 40
    End Sub

    Private Sub fndSRNNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndSRNNo._MYValidating

        isLoadData = True
        isSRNselected = True
        If isButtonClicked Then
            Dim objB As clsBulkMilkSRN = Nothing
            'If clsCommon.myLen(fndVendor.Value) <= 0 Then
            '    clsCommon.MyMessageBoxShow("Please Select Vendor ")
            '    fndVendor.Focus()
            '    Exit Sub
            'End If
            If dtpFromDate.Value > dtpToDate.Value Then
                clsCommon.MyMessageBoxShow(" 'From Date' can't be greator than 'To Date'")
                dtpFromDate.Focus()
                Exit Sub
            End If

            If clsCommon.myCDate(dtpDocDate.Value, "dd/MMM/yyyy") < clsCommon.myCDate(dtpToDate.Value, "dd/MMM/yyyy") Then
                clsCommon.MyMessageBoxShow(" 'To Date' can't be greator than 'Doc Date'")
                dtpToDate.Focus()
                Exit Sub
            End If
            Dim frm As FrmPendingBulkMilkSrn = New FrmPendingBulkMilkSrn()
            Dim strZeroSRN As String = ""
            If BulkProcPriceChartStandardRateWithZero = 1 Then
                strZeroSRN = " and TSPL_Bulk_MILK_SRN.Actual_Amount > 0 "            
            End If
            ''richa agarwal against ticket no BM00000008799
            ' frm.qry = " SELECT xxx.*,TSPL_Bulk_MILK_SRN.Vendor_Code,TSPL_Bulk_MILK_SRN.Loc_Code,TSPL_Bulk_MILK_SRN.SRN_Date ,TSPL_Bulk_MILK_SRN.UOM   FROM (select TSPL_Bulk_MILK_SRN.SRN_NO,TSPL_Bulk_MILK_SRN.Item_Code,(TSPL_Bulk_MILK_SRN.Net_Weight),coalesce(SUM(tdl.invoice_qty),0)  as invoice_qty,(TSPL_Bulk_MILK_SRN.Net_Weight-coalesce(SUM(tdl.invoice_qty),0)) as remaining_qty from TSPL_Bulk_MILK_SRN left join ( select invoice_qty,SRN_NO  from Tspl_bulk_milk_purchase_invoice_detail left outer join Tspl_bulk_milk_purchase_invoice_head on Tspl_bulk_milk_purchase_invoice_head.DOC_NO=tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO  where Tspl_bulk_milk_purchase_invoice_head.isPosted=1) as  tdl  on TSPL_Bulk_MILK_SRN.SRN_NO=tdl.srn_no  where TSPL_Bulk_MILK_SRN.isPosted=1 group by TSPL_Bulk_MILK_SRN.SRN_NO,TSPL_Bulk_MILK_SRN.Item_Code,(TSPL_Bulk_MILK_SRN.Net_Weight) )  xxx   left outer join TSPL_Bulk_MILK_SRN on TSPL_Bulk_MILK_SRN.SRN_NO=xxx.SRN_NO  where (xxx.Net_Weight-xxx.invoice_qty) >0 " & IIf(clsCommon.myLen(fndVendor.Value) > 0, " and Vendor_Code ='" & fndVendor.Value & "'", "") & " and SRN_Date between CONVERT(datetime,'" & clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy hh:mm:ss tt") & "',103) and CONVERT(datetime,'" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy hh:mm:ss tt") & "' ,103) " & IIf(chkSRNTrade.Checked, "", "and ISNULL(isApproved,0)=1 ")
            If TankerFromMaster = 0 Then
                frm.qry = " SELECT xxx.*,TSPL_Bulk_MILK_SRN.Vendor_Code,TSPL_Bulk_MILK_SRN.Loc_Code,TSPL_Bulk_MILK_SRN.SRN_Date ,TSPL_Bulk_MILK_SRN.UOM   FROM (select TSPL_Bulk_MILK_SRN.SRN_NO,TSPL_Bulk_MILK_SRN.Item_Code,(TSPL_Bulk_MILK_SRN.Net_Weight),coalesce(SUM(tdl.invoice_qty),0)  as invoice_qty,(TSPL_Bulk_MILK_SRN.Net_Weight-coalesce(SUM(tdl.invoice_qty),0)) as remaining_qty from TSPL_Bulk_MILK_SRN left join ( select invoice_qty,SRN_NO  from Tspl_bulk_milk_purchase_invoice_detail left outer join Tspl_bulk_milk_purchase_invoice_head on Tspl_bulk_milk_purchase_invoice_head.DOC_NO=tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO  ) as  tdl  on TSPL_Bulk_MILK_SRN.SRN_NO=tdl.srn_no  where TSPL_Bulk_MILK_SRN.isPosted=1 " & strZeroSRN & " group by TSPL_Bulk_MILK_SRN.SRN_NO,TSPL_Bulk_MILK_SRN.Item_Code,(TSPL_Bulk_MILK_SRN.Net_Weight) )  xxx   left outer join TSPL_Bulk_MILK_SRN on TSPL_Bulk_MILK_SRN.SRN_NO=xxx.SRN_NO  where (xxx.Net_Weight-xxx.invoice_qty) >0 " & IIf(clsCommon.myLen(fndVendor.Value) > 0, " and Vendor_Code ='" & fndVendor.Value & "'", "") & " and SRN_Date between CONVERT(datetime,'" & clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy hh:mm:ss tt") & "',103) and CONVERT(datetime,'" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy hh:mm:ss tt") & "' ,103) " & IIf(chkSRNTrade.Checked, "", "and ISNULL(isApproved,0)=1 ")
            Else
                frm.qry = " SELECT xxx.*,TSPL_Bulk_MILK_SRN.Vendor_Code,TSPL_Bulk_MILK_SRN.Loc_Code,TSPL_Bulk_MILK_SRN.SRN_Date ,TSPL_Bulk_MILK_SRN.UOM   FROM (select TSPL_Bulk_MILK_SRN.SRN_NO,TSPL_Bulk_MILK_SRN.Item_Code,(TSPL_Bulk_MILK_SRN.Net_Weight),coalesce(SUM(tdl.invoice_qty),0)  as invoice_qty,(TSPL_Bulk_MILK_SRN.Net_Weight-coalesce(SUM(tdl.invoice_qty),0)) as remaining_qty from TSPL_Bulk_MILK_SRN left join ( select invoice_qty,SRN_NO  from Tspl_bulk_milk_purchase_invoice_detail left outer join Tspl_bulk_milk_purchase_invoice_head on Tspl_bulk_milk_purchase_invoice_head.DOC_NO=tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO  ) as  tdl  on TSPL_Bulk_MILK_SRN.SRN_NO=tdl.srn_no  where TSPL_Bulk_MILK_SRN.isPosted=1 " & strZeroSRN & " group by TSPL_Bulk_MILK_SRN.SRN_NO,TSPL_Bulk_MILK_SRN.Item_Code,(TSPL_Bulk_MILK_SRN.Net_Weight) )  xxx   left outer join TSPL_Bulk_MILK_SRN on TSPL_Bulk_MILK_SRN.SRN_NO=xxx.SRN_NO  where (xxx.Net_Weight-xxx.invoice_qty) >0 " & IIf(clsCommon.myLen(fndVendor.Value) > 0, " and Vendor_Code ='" & fndVendor.Value & "'", "") & " and SRN_Date >= CONVERT(datetime,'" & clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy hh:mm:ss tt") & "',103) and SRN_Date <= CONVERT(datetime,'" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy hh:mm:ss tt") & "' ,103) " & IIf(chkSRNTrade.Checked, "", "")
            End If

            Dim whrCls As String = String.Empty
            'If Not clsMccMaster.isCurrentUserHO() Then
            '    If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            '        whrCls = " and TSPL_Bulk_MILK_SRN.Loc_Code in (" & objCommonVar.strCurrUserLocations & ")"
            '    End If
            'End If
            If clsCommon.myLen(fndLocation.Value) > 0 Then
                whrCls = " and TSPL_Bulk_MILK_SRN.Loc_Code='" & fndLocation.Value & "'"
            End If
            whrCls = whrCls & IIf(chkSRNTrade.Checked, " and TSPL_Bulk_MILK_SRN.formType='Bulk Milk SRN Trade'", " and TSPL_Bulk_MILK_SRN.formType='BulkMilkSrn'") & " and isnull(srn_return_no,'')=''"
            frm.qry = frm.qry & whrCls
            frm.ShowDialog()
            Dim SRNs As String = String.Empty
            If frm.btnOkClicked Then
                fndVendor.Enabled = False
                fndLocation.Enabled = False
                dtpFromDate.Enabled = False
                dtpToDate.Enabled = False
                Dim newNetRate As Double = 0

                loadBlankGrid()
                Dim dt As DataTable
                For i As Integer = 0 To frm.arrSrnNo.Count - 1
                    objB = New clsBulkMilkSRN()
                    If chkSRNTrade.Checked Then
                        objB = clsBulkMilkSRN.getData(frm.arrSrnNo(i).ToString, NavigatorType.Current, True)
                    Else
                        objB = clsBulkMilkSRN.getData(frm.arrSrnNo(i).ToString, NavigatorType.Current)
                        txtSubLocation.Value = objB.Joblocation_Code
                        lblSubLocation.Text = clsLocation.GetName(txtSubLocation.Value, Nothing)
                        chkJobWork.Checked = IIf(objB.IsAgainstJobWork = 1, True, False)
                    End If

                    SRNs = SRNs & frm.arrSrnNo(i).ToString
                    If i <> frm.arrSrnNo.Count - 1 Then
                        SRNs = SRNs & ", "
                    End If

                    fndLocation.Value = objB.Loc_Code
                    lblLocationName.Text = clsLocation.GetName(objB.Loc_Code, Nothing)
                    fndVendor.Value = objB.Vendor_Code
                    lblVendorName.Text = clsVendorMaster.GetName(objB.Vendor_Code, Nothing)
                    txtVendorInvoiceNo.Text = ""
                    '' done by Priti from PanchRaj account
                    Dim intCount As Integer = 0
                    If TankerFromMaster = 0 Then
                        gvItem.Rows.AddNew()
                        gvItem.Rows(i).Cells(colSlNo).Value = (i + 1)
                        gvItem.Rows(i).Cells(colTnkrNo).Value = objB.Tanker_No
                        gvItem.Rows(i).Cells(colSRNNo).Value = objB.SRN_NO
                        newNetRate = IIf(chkSRNTrade.Checked, objB.Standardrate, clsCommon.myCdbl(clsDBFuncationality.getSingleValue("  select ISNULL ( TSPL_Bulk_MILK_SRN.Approved_Rate,0) + isnull(TSPL_Bulk_MILK_SRN.Incentive,0)+isnull(TSPL_Bulk_MILK_SRN.Deduction,0)-isnull(TSPL_QUALITY_CHECK.DeductionAmount,0) as AppRate   from TSPL_Bulk_MILK_SRN  left outer join TSPL_QUALITY_CHECK on TSPL_QUALITY_CHECK.QC_No=TSPL_Bulk_MILK_SRN.QC_No  where SRN_NO='" & objB.SRN_NO & "'")))
                        gvItem.Rows(i).Cells(colSRNDATe).Value = clsCommon.GetPrintDate(objB.SRN_Date, "dd/MM/yyyy hh:mm:ss tt")
                        gvItem.Rows(i).Cells(colItemCode).Value = objB.Item_Code
                        gvItem.Rows(i).Cells(colHSN).Value = clsItemMaster.GetItemHSNCode(objB.Item_Code, Nothing)
                        gvItem.Rows(i).Cells(colItemDesc).Value = objB.Item_Desc
                        gvItem.Rows(i).Cells(colUOM).Value = objB.UOM
                        gvItem.Rows(i).Cells(colGrossWeight).Value = clsCommon.myFormat(objB.Gross_Weight)
                        gvItem.Rows(i).Cells(colTareWeight).Value = clsCommon.myFormat(objB.Tare_Weight)
                        gvItem.Rows(i).Cells(colNetWeight).Value = clsCommon.myFormat(objB.Net_Weight)
                        gvItem.Rows(i).Cells(colNetRate).Value = clsCommon.myFormat(newNetRate)
                        dt = clsDBFuncationality.GetDataTable("select TSPL_Bulk_MILK_SRN.SRN_NO, (TSPL_Bulk_MILK_SRN.Net_Weight-coalesce(SUM(tdl.invoice_qty),0)) as pending_qty from TSPL_Bulk_MILK_SRN left join ( select invoice_qty,SRN_NO  from Tspl_bulk_milk_purchase_invoice_detail left outer join Tspl_bulk_milk_purchase_invoice_head on Tspl_bulk_milk_purchase_invoice_head.DOC_NO=tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO  where Tspl_bulk_milk_purchase_invoice_head.isPosted=1) as  tdl  on TSPL_Bulk_MILK_SRN.SRN_NO=tdl.srn_no  where TSPL_Bulk_MILK_SRN.isPosted=1 and TSPL_Bulk_MILK_SRN.SRN_NO='" & objB.SRN_NO & "'  group by TSPL_Bulk_MILK_SRN.SRN_NO ,TSPL_Bulk_MILK_SRN.Net_Weight ")
                        gvItem.Rows(i).Cells(colPendingQty).Value = clsCommon.myFormat(clsCommon.myCdbl(dt.Rows(0)("pending_qty")))
                        gvItem.Rows(i).Cells(colQty).Value = clsCommon.myFormat(clsCommon.myCdbl(dt.Rows(0)("pending_qty")))
                        gvItem.Rows(i).Cells(colFat).Value = clsCommon.myFormat(objB.fat_per)
                        gvItem.Rows(i).Cells(colFatKG).Value = clsCommon.myFormat(objB.fat_KG, False, True, False, 3, True)
                        gvItem.Rows(i).Cells(colPriceCode).Value = objB.Price_Code
                        gvItem.Rows(i).Cells(colSNF).Value = clsCommon.myFormat(objB.snf_Per)
                        gvItem.Rows(i).Cells(colSNFKG).Value = clsCommon.myFormat(objB.SNF_KG, False, True, False, 3, True)
                        Dim strQry As String = " select  * from TSPL_Bulk_Price_MASTER where Price_Code='" & objB.Price_Code & "' "
                        dt = clsDBFuncationality.GetDataTable(strQry)
                        Dim FatW As Double = 0
                        Dim SNfW As Double = 0
                        Dim FATRate As Double = 0
                        Dim SNFRate As Double = 0
                        Dim FATValue As Double = 0
                        Dim SNfValue As Double = 0
                        Dim FATRatio As Double = 0
                        Dim SNFRatio As Double = 0
                        Dim StdRate As Double = 0
                        Dim fatKG As Double = 0
                        Dim snfKG As Double = 0
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            FatW = clsCommon.myCdbl(dt.Rows(0)("Fat_Weightage"))
                            SNfW = clsCommon.myCdbl(dt.Rows(0)("Snf_Weightage"))
                            FATRatio = clsCommon.myCdbl(dt.Rows(0)("Fat_Percentage"))
                            SNFRatio = clsCommon.myCdbl(dt.Rows(0)("Snf_Percentage"))
                            StdRate = newNetRate
                            fatKG = clsCommon.myCdbl(gvItem.Rows(i).Cells(colFatKG).Value)
                            snfKG = clsCommon.myCdbl(gvItem.Rows(i).Cells(colSNFKG).Value)
                            If chkSRNTrade.Checked Then
                                FATRate = objB.fat_Rate
                                SNFRate = objB.SNF_Rate
                                FATValue = fatKG * FATRate
                                SNfValue = snfKG * SNFRate
                            Else
                                FATRate = MyMath.RoundDown(StdRate * FatW / FATRatio, 2)
                                SNFRate = MyMath.RoundDown(StdRate * SNfW / SNFRatio, 2)
                                FATValue = MyMath.RoundDown(fatKG * FATRate, 2)
                                SNfValue = MyMath.RoundDown(snfKG * SNFRate, 2)
                            End If

                            gvItem.Rows(i).Cells(colFatRate).Value = clsCommon.myFormat(FATRate)
                            gvItem.Rows(i).Cells(colSNFRate).Value = clsCommon.myFormat(SNFRate)
                            If chkSRNTrade.Checked Then
                                gvItem.Rows(i).Cells(colActAmt).Value = objB.Actual_Amount

                            Else
                                gvItem.Rows(i).Cells(colActAmt).Value = clsCommon.myFormat(Math.Round(FATValue + SNfValue, 0))

                            End If

                            If AllowTruncateAmount Then ''BM00000010118
                                Dim xNewAmt As Double = clsCommon.myCdbl(FATValue + SNfValue)
                                If chkSRNTrade.Checked Then
                                    xNewAmt = objB.Actual_Amount
                                End If
                                If clsCommon.myLen(xNewAmt) > 0 AndAlso clsCommon.myCstr(xNewAmt).Contains(".") Then
                                    xNewAmt = clsCommon.myCstr(xNewAmt).Substring(0, clsCommon.myCstr(xNewAmt).IndexOf("."))
                                End If
                                gvItem.Rows(i).Cells(colActAmt).Value = CInt(xNewAmt)
                            End If

                        End If
                    Else
                        For Each objTr As clsBulkMilkSRNChemberNoDetails In objB.Arr
                            gvItem.Rows.AddNew()
                            intCount = intCount + 1
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSlNo).Value = intCount
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colTnkrNo).Value = objB.Tanker_No
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSRNNo).Value = objB.SRN_NO
                            'newNetRate = IIf(chkSRNTrade.Checked, objTr.Standardrate, objTr.Standardrate + objTr.Incentive + objTr.Deduction)
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSRNDATe).Value = clsCommon.GetPrintDate(objB.SRN_Date, "dd/MM/yyyy hh:mm:ss tt")
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colChamberDesc).Value = objTr.Chamber_Desc
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colItemCode).Value = objTr.Item_Code
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colItemDesc).Value = clsIntimation.getItemName(objTr.Item_Code, Nothing)
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colHSN).Value = clsItemMaster.GetItemHSNCode(objTr.Item_Code, Nothing)
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colUOM).Value = objTr.UOM
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colGrossWeight).Value = clsCommon.myFormat(objTr.Gross_Weight)
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colTareWeight).Value = clsCommon.myFormat(objTr.Tare_Weight)
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colNetWeight).Value = clsCommon.myFormat(objTr.Net_Weight)
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colNetRate).Value = clsCommon.myFormat(objTr.NetRate)
                            'gvItem.Rows(gvItem.Rows.Count - 1).Cells(colPendingQty).Value = clsCommon.myFormat(clsCommon.myCdbl(dt.Rows(0)("pending_qty")))
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colQty).Value = clsCommon.myFormat(objTr.Net_Weight)
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colFat).Value = clsCommon.myFormat(objTr.fat_per)
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colFatKG).Value = clsCommon.myFormat(objTr.fat_KG, False, True, False, 3, True)
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colPriceCode).Value = objTr.Price_Code
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSNF).Value = clsCommon.myFormat(objTr.snf_Per)
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSNFKG).Value = clsCommon.myFormat(objTr.SNF_KG, False, True, False, 3, True)
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colFatRate).Value = clsCommon.myFormat(objTr.fat_Rate)
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSNFRate).Value = clsCommon.myFormat(objTr.SNF_Rate)
                            If chkSRNTrade.Checked Then
                                gvItem.Rows(gvItem.Rows.Count - 1).Cells(colActAmt).Value = objTr.Actual_Amount

                            Else
                                gvItem.Rows(gvItem.Rows.Count - 1).Cells(colActAmt).Value = clsCommon.myFormat(Math.Round(objTr.Actual_Amount, 0))

                            End If

                            If AllowTruncateAmount Then ''BM00000010118
                                Dim xNewAmt As Double = objTr.Actual_Amount
                                If clsCommon.myLen(xNewAmt) > 0 AndAlso clsCommon.myCstr(xNewAmt).Contains(".") Then
                                    xNewAmt = clsCommon.myCstr(xNewAmt).Substring(0, clsCommon.myCstr(xNewAmt).IndexOf("."))
                                End If
                                gvItem.Rows(gvItem.Rows.Count - 1).Cells(colActAmt).Value = CInt(xNewAmt)
                            End If

                            'Dim strQry As String = ""
                            'Dim intPriceType As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IsPrice_GradeWise from TSPL_Bulk_Price_MASTER where Price_Code='" & objTr.Price_Code & "'"))
                            'If intPriceType = 1 Then
                            '    strQry = " select  * from tspl_bulk_price_detail where Price_Code='" & objTr.Price_Code & "' and milk_grade_code='" & objTr.MILK_GRADE_CODE & "' "
                            'Else
                            '    strQry = " select  * from TSPL_Bulk_Price_MASTER where Price_Code='" & objTr.Price_Code & "' "
                            'End If
                            'dt = clsDBFuncationality.GetDataTable(strQry)
                            'Dim FatW As Double = 0
                            'Dim SNfW As Double = 0
                            'Dim FATRate As Double = 0
                            'Dim SNFRate As Double = 0
                            'Dim FATValue As Double = 0
                            'Dim SNfValue As Double = 0
                            'Dim FATRatio As Double = 0
                            'Dim SNFRatio As Double = 0
                            'Dim StdRate As Double = 0
                            'Dim fatKG As Double = 0
                            'Dim snfKG As Double = 0
                            'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            '    FatW = clsCommon.myCdbl(dt.Rows(0)("Fat_Weightage"))
                            '    SNfW = clsCommon.myCdbl(dt.Rows(0)("Snf_Weightage"))
                            '    FATRatio = clsCommon.myCdbl(dt.Rows(0)("Fat_Percentage"))
                            '    SNFRatio = clsCommon.myCdbl(dt.Rows(0)("Snf_Percentage"))
                            '    StdRate = newNetRate
                            '    fatKG = clsCommon.myCdbl(gvItem.Rows(gvItem.Rows.Count - 1).Cells(colFatKG).Value)
                            '    snfKG = clsCommon.myCdbl(gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSNFKG).Value)
                            '    If chkSRNTrade.Checked Then
                            '        FATRate = objB.fat_Rate
                            '        SNFRate = objB.SNF_Rate
                            '        FATValue = fatKG * FATRate
                            '        SNfValue = snfKG * SNFRate
                            '    Else
                            '        FATRate = MyMath.RoundDown(StdRate * FatW / FATRatio, 2)
                            '        SNFRate = MyMath.RoundDown(StdRate * SNfW / SNFRatio, 2)
                            '        FATValue = MyMath.RoundDown(fatKG * FATRate, 2)
                            '        SNfValue = MyMath.RoundDown(snfKG * SNFRate, 2)
                            '    End If

                            'gvItem.Rows(gvItem.Rows.Count - 1).Cells(colFatRate).Value = clsCommon.myFormat(objTr.fat_Rate)
                            'gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSNFRate).Value = clsCommon.myFormat(objTr.SNF_Rate)
                            'If chkSRNTrade.Checked Then
                            '    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colActAmt).Value = objB.Actual_Amount

                            'Else
                            '    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colActAmt).Value = clsCommon.myFormat(Math.Round(objB.Actual_Amount, 0))

                            'End If
                            'End If
                        Next
                    End If

                    'gvItem.Rows(i).Cells(colActAmt).Value = clsCommon.myCdbl(gvItem.Rows(i).Cells(colQty).Value) * clsCommon.myCdbl(gvItem.Rows(i).Cells(colNetRate).Value)

                Next
                fndSRNNo.Value = SRNs
                UpdateTotals()
            End If
        End If
        isLoadData = False
    End Sub
    Function getDeduction(ByVal paramCode As String, ByVal value As Double, ByVal docDate As Date) As Double
        Dim strQry As String = "select top 1  isnull(Value,0) as value   from TSPL_PARAMETER_RANGE_MASTER where Code='" & paramCode & "' and " & value & ">=Lower_range  and " & value & "<=Upper_range  and effective_date<='" & clsCommon.GetPrintDate(docDate, "dd/MMM/yyyy") & "' and isnull(Value,0)<0  and loc_code='" & fndLocation.Value & "'   order by Effective_Date desc  "
        Dim deducValue As Double = 0
        deducValue = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(strQry))
        Return deducValue
    End Function

    Function getDeduction(ByVal QCNo As String, ByVal srnDate As Date) As Double
        Dim totDeducValue As Double = 0
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(" select * from TSPL_QC_Parameter_Detail where QC_no='" & QCNo & "'")
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                totDeducValue = totDeducValue + getDeduction(dt.Rows(i)("Param_Field_Code"), clsCommon.myCdbl(dt.Rows(i)("Param_Field_Value")), srnDate)
            Next
        End If
        Return totDeducValue
    End Function


    Function getIncentive(ByVal paramCode As String, ByVal value As Double, ByVal docDate As Date) As Double
        Dim strQry As String = "select top 1  isnull(Value,0) as value   from TSPL_PARAMETER_RANGE_MASTER where Code='" & paramCode & "' and " & value & ">=Lower_range  and " & value & "<=Upper_range  and effective_date<='" & clsCommon.GetPrintDate(docDate, "dd/MMM/yyyy") & "' and isnull(Value,0)>0   and loc_code='" & fndLocation.Value & "'   order by Effective_Date desc  "
        Dim IncenValue As Double = 0
        IncenValue = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(strQry))
        Return IncenValue
    End Function

    Function getIncentive(ByVal QCNo As String, ByVal srnDate As Date) As Double
        Dim totIncenValue As Double = 0
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(" select * from TSPL_QC_Parameter_Detail where QC_no='" & QCNo & "'")
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                totIncenValue = totIncenValue + getIncentive(dt.Rows(i)("Param_Field_Code"), clsCommon.myCdbl(dt.Rows(i)("Param_Field_Value")), srnDate)
            Next
        End If
        Return totIncenValue
    End Function

    Private Sub fndVendor__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndVendor._MYValidating
        Dim qry As String = " SELECT distinct xxx .Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name    FROM (select TSPL_Bulk_MILK_SRN.SRN_NO,max(TSPL_Bulk_MILK_SRN.Vendor_Code) as Vendor_code ,TSPL_Bulk_MILK_SRN.Item_Code,(TSPL_Bulk_MILK_SRN.Net_Weight),coalesce(SUM(tdl.invoice_qty),0)  as invoice_qty,(TSPL_Bulk_MILK_SRN.Net_Weight-coalesce(SUM(tdl.invoice_qty),0)) as remaining_qty from TSPL_Bulk_MILK_SRN left join ( select invoice_qty,SRN_NO  from Tspl_bulk_milk_purchase_invoice_detail left outer join Tspl_bulk_milk_purchase_invoice_head on Tspl_bulk_milk_purchase_invoice_head.DOC_NO=tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO  where Tspl_bulk_milk_purchase_invoice_head.isPosted=1) as  tdl  on TSPL_Bulk_MILK_SRN.SRN_NO=tdl.srn_no  where TSPL_Bulk_MILK_SRN.isPosted=1 group by TSPL_Bulk_MILK_SRN.SRN_NO,TSPL_Bulk_MILK_SRN.Item_Code,(TSPL_Bulk_MILK_SRN.Net_Weight) )  xxx   left outer join TSPL_Bulk_MILK_SRN on TSPL_Bulk_MILK_SRN.SRN_NO=xxx.SRN_NO  left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=xxx.Vendor_Code    "
        Dim strWhrCl As String = " TSPL_VENDOR_MASTER.Status='N' and "
        If TankerFromMaster = 0 Then
            strWhrCl = "(xxx.Net_Weight-xxx.invoice_qty) >0  and SRN_Date between CONVERT(date,'" & clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MM/yyyy") & "',103) and CONVERT(date,'" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy") & "' ,103) "
        Else
            strWhrCl = "(xxx.Net_Weight-xxx.invoice_qty) >0  and  convert(date,SRN_Date,103) >= CONVERT(date,'" & clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MM/yyyy") & "',103) and convert(date,SRN_Date,103) <=CONVERT(date,'" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy") & "' ,103) "
        End If
        fndVendor.Value = clsCommon.myCstr(clsCommon.ShowSelectForm("SRNVENDFND", qry, "Vendor_Code", strWhrCl, fndVendor.Value, "Vendor_Code", isButtonClicked))
        lblVendorName.Text = clsCommon.myCstr(clsVendorMaster.GetName(fndVendor.Value, Nothing))
        If TankerFromMaster = 1 AndAlso clsCommon.myLen(fndVendor.Value) > 0 Then
            SetToDate()
        End If
    End Sub
    Sub SetToDate()
        Try
            If AllowDateChanged Then
                If isLoadData = False Then
                    If clsCommon.myLen(fndVendor.Value) <= 0 Then
                        dtpFromDate.Focus()
                        dtpFromDate.Value = clsCommon.GetDateWithStartTime(clsCommon.GETSERVERDATE())
                        Throw New Exception("Please select Vendor First.")
                    End If
                End If
                Dim PaymentCycleValue As Integer = GetpaymentCycle(fndVendor.Value)
                If PaymentCycleValue = 0 Then
                    fndSRNNo.Value = ""
                    Throw New Exception("Please map payment cycle with selected vendor.")
                End If
                If dtpFromDate.Value.Day Mod PaymentCycleValue <> 1 And (Not PaymentCycleValue = 1) Then
                    AllowDateChanged = False
                    clsCommon.MyMessageBoxShow("Invalid date.Date should be multiple of " & clsCommon.myCstr(PaymentCycleValue) & " + 1 ")
                    dtpFromDate.Value = clsCommon.GetDateWithStartTime(dtpFromDate.MinDate)
                    dtpFromDate.Text = clsCommon.GetDateWithStartTime(dtpFromDate.MinDate)
                    AllowDateChanged = True
                End If
                dtpToDate.Value = clsCommon.GetDateWithEndTime(dtpFromDate.Value.AddDays(PaymentCycleValue - 1))

                If dtpFromDate.Value.Month <> dtpToDate.Value.Month Then
                    dtpToDate.Value = clsCommon.GetDateWithEndTime(New Date(dtpFromDate.Value.Year, dtpFromDate.Value.Month, 1).AddMonths(1).AddDays(-1))
                End If
                Dim dtNxtPay As DateTime = dtpToDate.Value.AddDays(Math.Ceiling(PaymentCycleValue / 2.0))
                If dtpFromDate.Value.Month <> dtNxtPay.Month Then
                    dtpToDate.Value = clsCommon.GetDateWithEndTime(New Date(dtpFromDate.Value.Year, dtpFromDate.Value.Month, 1).AddMonths(1).AddDays(-1))
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Public Function GetpaymentCycle(ByVal Vendor As String)
        Dim sQuery As String = "select case when Pc_Type='Day' then PC_VALUE when PC_Type='Month' then PC_Value * " & Date.DaysInMonth(txtMonth.Value.Year, txtMonth.Value.Month) & " end " _
               & " as Pc_Value from TSPL_VENDOR_MASTER inner join TSPL_PAYMENT_CYCLE_MASTER  on TSPL_VENDOR_MASTER.PC_CODE=TSPL_PAYMENT_CYCLE_MASTER.PC_CODE where vendor_code='" & Vendor & "'"
        Dim py_code As String = clsDBFuncationality.getSingleValue(sQuery)
        Return py_code
    End Function
    Private Sub FrmMilkPurchaseInvoice_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnReset.Enabled Then
            btnReset_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            btnSave_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled AndAlso MyBase.isPostFlag Then
            btnPost_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled AndAlso MyBase.isDeleteFlag Then
            btnDelete_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            btnClose_Click(sender, e)
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = "SIRC"
            frm.strCode = "SIReversAndCreate"
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnReverse.Visible = True
            End If
        ElseIf e.Alt AndAlso e.Control AndAlso e.Shift AndAlso e.KeyCode = Keys.F6 Then
            If clsCommon.myLen(fndDocNo.Value) > 0 Then
                If btnPost.Enabled = False Then
                    TxtVendorUpdate.Visible = True
                    TxtVendorUpdate.Enabled = True
                    btnUpdateVendor.Visible = True
                    btnUpdateVendor.Enabled = True
                Else
                    clsCommon.MyMessageBoxShow("Please post the Invoice first")
                End If
            Else
                clsCommon.MyMessageBoxShow("Please select Invoice No first")
            End If
        End If
    End Sub

    Private Sub FrmMilkPurchaseInvoice_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Panel3.Enabled = False
        BulkProcPriceChartStandardRateWithZero = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.BulkProcPriceChartStandardRateWithZero, clsFixedParameterCode.BulkProcPriceChartStandardRateWithZero, Nothing))
        AllowJobWorkonGateEntryBulkProc = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowJobWorkonGateEntryBulkProc, clsFixedParameterCode.AllowJobWorkonGateEntryBulkProc, Nothing))
        SetUserMgmtNew()
        TankerFromMaster = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GateEntryTankerFromTankerMaster, clsFixedParameterCode.GateEntryTankerFromTankerMaster, Nothing))
        ''=======BM00000010118===============
        AllowTruncateAmount = IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.Allow_AmountTruncate_BulkMilkSRN, clsFixedParameterCode.Allow_AmountTruncate_BulkMilkSRN, Nothing)) = "1", True, False)
        ''=====================
        ''====Adjusted Fat & SNF======
        RunBulkProcOnAdjustFATCLR = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RunBulkProcOnAdjustedFATCLR, clsFixedParameterCode.RunBulkProcOnAdjustedFATCLR, Nothing))
        ''=========================
        reset()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D To Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C To Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N For New")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P to Post the Transaction")

        If clsCommon.myLen(Me.Tag) > 0 Then
            loadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
        btnUpdateVendor.Visible = False
        If TankerFromMaster = 1 Then
            lblMonth.Visible = True
            txtMonth.Visible = True
        End If
        AllowDateChanged = True

        Dim coll As Dictionary(Of String, String)
        coll = New Dictionary(Of String, String)()
        coll.Add("EWayBillNo", "Varchar(30) null")
        coll.Add("EWayBillDate", "Datetime NULL")
        coll.Add("Electronic_Ref_No", "varchar(10) NULL")

        clsCommonFunctionality.CreateOrAlterTable("TSPL_BULK_MILK_PURCHASE_INVOICE_HEAD", coll)
        If AllowJobWorkonGateEntryBulkProc = 1 Then
            Panel3.Visible = True
        Else
            Panel3.Visible = False
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If myMessages.deleteConfirm() Then
            deleteData()
        End If

    End Sub

    Private Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPost.Click
        postData()
        'loadData(fndDocNo.Value, NavigatorType.Current)
    End Sub

    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        printData()
    End Sub

    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset()
    End Sub



    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If allowToSave() Then SaveData(False)
    End Sub

    Sub UpdateTotals()
        Dim totQty As Double = 0
        Dim totFatKg As Double = 0
        Dim totSnfKg As Double = 0
        Dim totAmt As Double = 0
        For i As Integer = 0 To gvItem.Rows.Count - 1
            totQty = totQty + clsCommon.myCdbl(gvItem.Rows(i).Cells(colQty).Value)
            totFatKg = totFatKg + clsCommon.myCdbl(gvItem.Rows(i).Cells(colFatKG).Value)
            totSnfKg = totSnfKg + clsCommon.myCdbl(gvItem.Rows(i).Cells(colSNFKG).Value)
            totAmt = totAmt + clsCommon.myCdbl(gvItem.Rows(i).Cells(colActAmt).Value)
        Next
        Dim intPart As Double = Math.Round(totAmt, 0)
        Dim roundOffAmt As Double = 0
        'If totAmt > intPart Then
        '    roundOffAmt = (totAmt - intPart)
        'ElseIf totAmt < intPart Then
        '    roundOffAmt = intPart - totAmt
        'Else
        '    roundOffAmt = 0
        'End If

        roundOffAmt = -(totAmt - intPart)
        txtTotalAmt.Text = clsCommon.myFormat(clsCommon.myFormat(intPart))
        txtRoundOffAmt.Text = clsCommon.myFormat(Math.Round(roundOffAmt, 2))



        txtTotalQty.Text = clsCommon.myFormat(clsCommon.myCstr(Math.Round(totQty, 2)))
        txtTotalFatKg.Text = clsCommon.myFormat(clsCommon.myCstr(MyMath.RoundDown(totFatKg, 3)), False, True, False, 3, True)
        txtTotalSNFKg.Text = clsCommon.myFormat(clsCommon.myCstr(MyMath.RoundDown(totSnfKg, 3)), False, True, False, 3, True)
        '        gvItem.CurrentRow.Cells(colActAmt).Value = clsCommon.myCdbl(gvItem.CurrentRow.Cells(colQty).Value) * clsCommon.myCdbl(gvItem.CurrentRow.Cells(colNetRate).Value)
    End Sub

    Private Sub gvItem_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvItem.CellValueChanged
        If isSRNselected Then
            If Not isLoadData Then
                If gvItem.CurrentRow.Cells(colPendingQty).Value < gvItem.CurrentRow.Cells(colQty).Value Then
                    clsCommon.MyMessageBoxShow("Invoice Qty can't be more than pending Qty")
                    gvItem.CurrentRow.Cells(colQty).Value = clsCommon.myFormat(gvItem.CurrentRow.Cells(colPendingQty).Value)
                    Exit Sub
                End If

                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gvItem.Columns(colQty) Then
                        gvItem.CurrentRow.Cells(colFatKG).Value = clsCommon.myFormat(MyMath.RoundDown(gvItem.CurrentRow.Cells(colFat).Value * clsCommon.myCdbl(gvItem.CurrentRow.Cells(colQty).Value), 3) / 100, False, True, False, 3, True)
                        gvItem.CurrentRow.Cells(colSNFKG).Value = clsCommon.myFormat(MyMath.RoundDown(gvItem.CurrentRow.Cells(colSNF).Value * clsCommon.myCdbl(gvItem.CurrentRow.Cells(colQty).Value), 3) / 100, False, True, False, 3, True)
                        Dim objb As clsBulkMilkSRN = clsBulkMilkSRN.getData(gvItem.CurrentRow.Cells(colSRNNo).Value, NavigatorType.Current)
                        'gvItem.CurrentRow.Cells(colDeduc).Value = getDeduction(objb.QC_No, objb.SRN_Date) * clsCommon.myCdbl(gvItem.CurrentRow.Cells(colQty).Value)
                        Dim strQry As String = " select  * from TSPL_Bulk_Price_MASTER where Price_Code='" & objb.Price_Code & "' "
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry)
                        Dim FatW As Double = 0
                        Dim SNfW As Double = 0
                        Dim FATRate As Double = 0
                        Dim SNFRate As Double = 0
                        Dim FATValue As Double = 0
                        Dim SNfValue As Double = 0
                        Dim FATRatio As Double = 0
                        Dim SNFRatio As Double = 0
                        Dim StdRate As Double = 0
                        Dim fatKG As Double = 0
                        Dim snfKG As Double = 0
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            FatW = clsCommon.myCdbl(dt.Rows(0)("Fat_Weightage"))
                            SNfW = clsCommon.myCdbl(dt.Rows(0)("Snf_Weightage"))
                            FATRatio = clsCommon.myCdbl(dt.Rows(0)("Fat_Percentage"))
                            SNFRatio = clsCommon.myCdbl(dt.Rows(0)("Snf_Percentage"))
                            StdRate = clsCommon.myCdbl(gvItem.CurrentRow.Cells(colNetRate).Value)
                            fatKG = clsCommon.myCdbl(gvItem.Rows(0).Cells(colFatKG).Value)
                            snfKG = clsCommon.myCdbl(gvItem.Rows(0).Cells(colSNFKG).Value)
                            FATRate = MyMath.RoundDown(StdRate * FatW / FATRatio, 2)
                            SNFRate = MyMath.RoundDown(StdRate * SNfW / SNFRatio, 2)
                            FATValue = MyMath.RoundDown(fatKG * FATRate, 2)
                            SNfValue = MyMath.RoundDown(snfKG * SNFRate, 2)
                            gvItem.CurrentRow.Cells(colFatRate).Value = clsCommon.myFormat(MyMath.RoundDown(FATRate, 2))
                            gvItem.CurrentRow.Cells(colSNFRate).Value = clsCommon.myFormat(MyMath.RoundDown(SNFRate, 2))
                            gvItem.CurrentRow.Cells(colActAmt).Value = clsCommon.myFormat(Math.Round(FATValue + SNfValue, 0))

                            If AllowTruncateAmount Then ''BM00000010118
                                Dim xNewAmt As Double = clsCommon.myCdbl(FATValue + SNfValue)
                                If clsCommon.myLen(xNewAmt) > 0 AndAlso clsCommon.myCstr(xNewAmt).Contains(".") Then
                                    xNewAmt = clsCommon.myCstr(xNewAmt).Substring(0, clsCommon.myCstr(xNewAmt).IndexOf("."))
                                End If
                                gvItem.CurrentRow.Cells(colActAmt).Value = CInt(xNewAmt)
                            End If

                            'gvItem.CurrentRow.Cells(colActAmt).Value = clsCommon.myCdbl(gvItem.CurrentRow.Cells(colQty).Value) * clsCommon.myCdbl(gvItem.CurrentRow.Cells(colNetRate).Value)
                        End If
                    End If

                    'If e.Column Is gvItem.Columns(colQty) OrElse e.Column Is gvItem.Columns(colFatKG) OrElse e.Column Is gvItem.Columns(colSNFKG) OrElse e.Column Is gvItem.Columns(colActAmt) Then
                    '    UpdateTotals()
                    'End If
                End If
                isCellValueChangedOpen = False
            End If
        End If
    End Sub





    Private Sub gvItem_UserDeletedRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gvItem.UserDeletedRow
        UpdateTotals()
    End Sub


    Private Sub fndLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndLocation._MYValidating
        Dim whrcls As String = String.Empty
        If Not clsMccMaster.isCurrentUserHO() Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrcls = "  location_code in (" & objCommonVar.strCurrUserLocations & ")"
            End If
        End If
        fndLocation.Value = clsLocation.getFinder(whrcls, fndLocation.Value, isButtonClicked)
        lblLocationName.Text = clsLocation.GetName(fndLocation.Value, Nothing)
    End Sub

    Private Sub fndDocNo__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndDocNo._MYNavigator
        loadData(fndDocNo.Value, NavType)
    End Sub

    Private Sub fndDocNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndDocNo._MYValidating
        Dim whrCls As String = String.Empty
        If Not clsMccMaster.isCurrentUserHO() Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrCls = "  loc_code in(" & objCommonVar.strCurrUserLocations & ")"
            End If
        End If
        fndDocNo.Value = clsMilkPurchaseInvoiceHead.getFinder(whrCls, fndDocNo.Value, isButtonClicked)
        loadData(fndDocNo.Value, NavigatorType.Current)
    End Sub


    Private Sub btnUpdateVendor_Click(sender As Object, e As EventArgs) Handles btnUpdateVendor.Click
        Try
            If clsCommon.myLen(fndDocNo.Value) > 0 And btnPost.Enabled = False And clsCommon.myLen(TxtVendorUpdate.Value) > 0 Then
                Dim ISVendorInvoice As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" Select IsVendorInvoiceNo from TSPL_VENDOR_MASTER where Vendor_Code =(Select vendor_code  from tspl_Bulk_milk_purchase_Invoice_head where DOC_NO ='" & fndDocNo.Value & "') "))
                If ISVendorInvoice = 1 And clsCommon.myLen(txtVendorInvoiceNo.Text) <= 0 Then
                    clsCommon.MyMessageBoxShow("Please enter Vendor Invoice No.")
                Else
                    fndVendor.Value = ""
                    If clsCommon.myLen(txtVendorInvoiceNo.Text) > 0 Then
                        If clsDBFuncationality.getSingleValue("Select count(*) from tspl_Bulk_milk_purchase_Invoice_head where DOC_NO<>'" & fndDocNo.Value & "' and  Vendor_Invoice_No ='" & txtVendorInvoiceNo.Text & "'") > 1 Then
                            clsCommon.MyMessageBoxShow("Duplicate Vendor Invoice No.,Please enter different vendor invoice no")
                        Else
                            If UpdateVendorAfterPosting() Then
                                clsCommon.MyMessageBoxShow("Vendor updated successfully.")
                                TxtVendorUpdate.Value = ""
                                TxtVendorUpdate.Visible = False
                                btnUpdateVendor.Enabled = False
                                btnUpdateVendor.Visible = False
                                loadData(fndDocNo.Value, NavigatorType.Current)
                            End If
                        End If
                    Else
                        If UpdateVendorAfterPosting() Then
                            clsCommon.MyMessageBoxShow("Vendor updated successfully.")
                            TxtVendorUpdate.Value = ""
                            TxtVendorUpdate.Visible = False
                            btnUpdateVendor.Enabled = False
                            btnUpdateVendor.Visible = False
                            loadData(fndDocNo.Value, NavigatorType.Current)
                        End If
                    End If



                End If
            Else
                clsCommon.MyMessageBoxShow("Please Select Vendor first.")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Function UpdateVendorAfterPosting() As Boolean
        Dim strAPInvoiceNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Document_No  from TSPL_VENDOR_INVOICE_HEAD where Against_BulkMillkPurchaseInvoice_No ='" & fndDocNo.Value & "' "))

        'Dim strPaymentNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Payment_No from TSPL_PAYMENT_DETAIL where Document_No='" & strAPInvoiceNo & "'"))
        Dim strPaymentNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_PAYMENT_DETAIL.Payment_No from TSPL_PAYMENT_DETAIL left outer join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No =TSPL_PAYMENT_DETAIL.Payment_No where TSPL_PAYMENT_DETAIL.Document_No='" & strAPInvoiceNo & "' and TSPL_PAYMENT_HEADER .IsChkReverse='N'"))

        Dim strInvoiceNo As String = clsCommon.myCstr(fndDocNo.Value)

        'Dim strSaleReturnNoAgainstdispatch = clsDBFuncationality.getSingleValue("Select Document_No  from TSPL_SALE_RETURN_MASTER_BULKSALE where DispatchNo ='" & txtDocNo.Value & "'")
        ''Dim strSaleReturnNoAgainstinvoice = clsDBFuncationality.getSingleValue("Select Document_No  from TSPL_SALE_RETURN_MASTER_BULKSALE where InvoiceNo ='" & strInvoiceNo & "'")


        Dim strVendorCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Vendor_Code  from TSPL_VENDOR_INVOICE_HEAD  where Document_No='" & strAPInvoiceNo & "'"))
        Dim strVendorName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Vendor_Name  from TSPL_VENDOR_INVOICE_HEAD  where Document_No='" & strAPInvoiceNo & "'"))
        Dim strDescription As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select description from TSPL_VENDOR_INVOICE_HEAD  where Document_No='" & strAPInvoiceNo & "'"))
        strDescription = strDescription.Replace(strVendorCode, TxtVendorUpdate.Value)
        strDescription = strDescription.Replace(strVendorName, lblVendorName.Text)

        If clsCommon.myLen(strPaymentNo) > 0 Then
            clsCommon.MyMessageBoxShow("Vendor cannot be updated because Payment has been created for this invoice " & strInvoiceNo)
            Exit Function
        End If

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(fndDocNo.Value) > 0 Then
                Dim issrntradeInvoice As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select isnull(isSRNTradeInvoice,0)  from tspl_Bulk_milk_purchase_Invoice_head where DOC_NO ='" & fndDocNo.Value & "'", trans))

                ' If issrntradeInvoice = 0 Then
                '        '' to update journal master ap invoice and bulk purchase invoice table againt invoice
                Dim qry = "update TSPL_JOURNAL_MASTER  set CustVend_Code= '" & TxtVendorUpdate.Value & "',CustVend_Name='" & lblVendorName.Text & "'  where Source_Doc_No='" + clsCommon.myCstr(strAPInvoiceNo) + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "update TSPL_VENDOR_INVOICE_HEAD  set Vendor_Code= '" & TxtVendorUpdate.Value & "',Vendor_Name='" & lblVendorName.Text & "',Description='" & strDescription & "',Vendor_Invoice_No='" & txtVendorInvoiceNo.Text & "'  where Document_No='" + clsCommon.myCstr(strAPInvoiceNo) + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "update tspl_Bulk_milk_purchase_Invoice_head  set vendor_code= '" & TxtVendorUpdate.Value & "',Is_Update_Vendor_AfterPost=1,Vendor_Invoice_No='" & txtVendorInvoiceNo.Text & "' where DOC_NO='" + clsCommon.myCstr(strInvoiceNo) + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                '' to update journal master ,inventory and bulk milk srn table againt srn( multiple for one invoice)
                qry = "update TSPL_JOURNAL_MASTER  set CustVend_Code= '" & TxtVendorUpdate.Value & "',CustVend_Name='" & lblVendorName.Text & "'  where Source_Doc_No in ( Select distinct SRN_NO  from tspl_Bulk_milk_purchase_Invoice_Detail where DOC_NO=  '" & fndDocNo.Value & "')"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "update TSPL_INVENTORY_MOVEMENT_NEW  set Vendor_Code = '" & TxtVendorUpdate.Value & "',Vendor_Name ='" & lblVendorName.Text & "'  where Source_Doc_No in ( Select distinct SRN_NO  from tspl_Bulk_milk_purchase_Invoice_Detail where DOC_NO=  '" & fndDocNo.Value & "')"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "update TSPL_Bulk_MILK_SRN  set Vendor_Code= '" & TxtVendorUpdate.Value & "'  where SRN_NO in ( Select distinct SRN_NO  from tspl_Bulk_milk_purchase_Invoice_Detail where DOC_NO=  '" & fndDocNo.Value & "')"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                '        '' to update gate entry, qc,weighment  table againt srn( multiple for one invoice)
                If issrntradeInvoice = 0 Then
                    qry = "update tspl_quality_check set Vendor_Code ='" & TxtVendorUpdate.Value & "',Vendor_Desc='" & lblVendorName.Text & "' where Gate_Entry_No in (Select Gate_Entry_No  from TSPL_Bulk_MILK_SRN where SRN_NO in  (Select distinct SRN_NO  from tspl_Bulk_milk_purchase_Invoice_Detail where DOC_NO=  '" & fndDocNo.Value & "'))"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    qry = "update tspl_weighment_detail set Vendor_Code ='" & TxtVendorUpdate.Value & "',Vendor_Desc='" & lblVendorName.Text & "' where Gate_Entry_No in (Select Gate_Entry_No  from TSPL_Bulk_MILK_SRN where SRN_NO in  (Select distinct SRN_NO  from tspl_Bulk_milk_purchase_Invoice_Detail where DOC_NO=  '" & fndDocNo.Value & "'))"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    qry = "update tspl_gate_entry_details set  Vendor_Code ='" & TxtVendorUpdate.Value & "',Vendor_Desc='" & lblVendorName.Text & "' where Gate_Entry_No in (Select Gate_Entry_No  from TSPL_Bulk_MILK_SRN where SRN_NO in  (Select distinct SRN_NO  from tspl_Bulk_milk_purchase_Invoice_Detail where DOC_NO=  '" & fndDocNo.Value & "'))"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                End If
                'Else

                'End If
                trans.Commit()
            End If

            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Private Sub TxtVendorUpdate__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtVendorUpdate._MYValidating
        Dim ISVendorInvoice As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" Select IsVendorInvoiceNo from TSPL_VENDOR_MASTER where Vendor_Code =(Select vendor_code  from tspl_Bulk_milk_purchase_Invoice_head where DOC_NO ='" & fndDocNo.Value & "') "))
        Dim StrVendorType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select Vendor_Type   from TSPL_VENDOR_MASTER where Vendor_Code =(Select vendor_code  from tspl_Bulk_milk_purchase_Invoice_head where DOC_NO ='" & fndDocNo.Value & "') "))
        Dim strVendor_Account As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select Vendor_Account   from TSPL_VENDOR_MASTER where Vendor_Code =(Select vendor_code  from tspl_Bulk_milk_purchase_Invoice_head where DOC_NO ='" & fndDocNo.Value & "') "))

        Dim qry As String = "SELECT distinct Vendor_Code as Code,TSPL_VENDOR_MASTER.Vendor_Name  as Name from TSPL_VENDOR_MASTER  "
        TxtVendorUpdate.Value = clsCommon.myCstr(clsCommon.ShowSelectForm("INVVendorUpdate", qry, "Code", " Status='N' and  Vendor_Type='" & StrVendorType & "' and IsVendorInvoiceNo=" & ISVendorInvoice & " and Vendor_Account='" & strVendor_Account & "'", TxtVendorUpdate.Value, "Code", isButtonClicked))
        lblVendorName.Text = clsCommon.myCstr(clsVendorMaster.GetName(TxtVendorUpdate.Value, Nothing))
    End Sub

    Private Sub dtpFromDate_Leave(sender As Object, e As EventArgs) Handles dtpFromDate.Leave
        If TankerFromMaster = 1 And clsCommon.myLen(fndVendor.Value) > 0 Then
            SetToDate()
        End If
    End Sub

    Private Sub dtpFromDate_TextChanged(sender As Object, e As EventArgs) Handles dtpFromDate.TextChanged

    End Sub

    Private Sub dtpFromDate_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles dtpFromDate.Validating
        If TankerFromMaster = 1 Then
            'SetToDate()
        End If
    End Sub

    Private Sub txtMonth_ValueChanged(sender As Object, e As EventArgs) Handles txtMonth.ValueChanged
        Try
            If TankerFromMaster = 1 Then
                AllowDateChanged = False
                dtpFromDate.MinDate = "01-Jan-0001"
                dtpFromDate.MaxDate = Date.DaysInMonth(txtMonth.Value.Year, txtMonth.Value.Month) & "-" & txtMonth.Value.Month & "-" & txtMonth.Value.Year
                dtpFromDate.MinDate = "01-" & txtMonth.Value.Month & "-" & txtMonth.Value.Year
                dtpToDate.Value = Date.DaysInMonth(txtMonth.Value.Year, txtMonth.Value.Month) & "-" & txtMonth.Value.Month & "-" & txtMonth.Value.Year
                AllowDateChanged = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub dtpFromDate_ValueChanged(sender As Object, e As EventArgs) Handles dtpFromDate.ValueChanged
        'If TankerFromMaster = 1 And clsCommon.myLen(fndVendor.Value) > 0 Then
        '    SetToDate()
        'End If
    End Sub

    Private Sub dtpFromDate_ValueChanging(sender As Object, e As ValueChangingEventArgs) Handles dtpFromDate.ValueChanging

    End Sub

    Private Sub btnEwaybillupdate_Click(sender As Object, e As EventArgs) Handles btnEwaybillupdate.Click
        Try
            If clsCommon.myLen(fndDocNo.Value) > 0 Then
                Dim obj As New clsMilkPurchaseInvoiceHead
                obj.DOC_NO = clsCommon.myCstr(fndDocNo.Value)
                obj.EWayBillNo = TxtEWayBillNo.Text

                If txtewaybilldate.Checked Then
                    obj.EWayBillDate = clsCommon.GetPrintDate(txtewaybilldate.Value, "dd/MMM/yyyy")
                Else
                    obj.EWayBillDate = Nothing
                End If
                obj.Electronic_Ref_No = txtElectronicRefNo.Text


                If clsMilkPurchaseInvoiceHead.UpdateAfterPosting(obj, Nothing) Then
                    clsCommon.MyMessageBoxShow("Information updated successfully.")
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Private Sub btnBillOfSupply_Click(sender As Object, e As EventArgs) Handles btnBillOfSupply.Click
        If clsCommon.myLen(fndDocNo.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("No data found to print")
        Else
            ' Dim strQuery As String = "select  GSTRegistered from TSPL_VENDOR_MASTER where vendor_code='" + fndVendor.Value + "' "
            Dim isVendorRegister As Boolean = clsDBFuncationality.getSingleValue("select  GSTRegistered from TSPL_VENDOR_MASTER where vendor_code='" + fndVendor.Value + "' ")
            If isVendorRegister = False Then
                Dim strPurchaseTaxInvoiceNo As String = clsDBFuncationality.getSingleValue(" select isnull(TSPL_BULK_MILK_PURCHASE_INVOICE_head.Purchase_Tax_Invoice,'') as Purchase_Tax_Invoice from TSPL_BULK_MILK_PURCHASE_INVOICE_head where TSPL_BULK_MILK_PURCHASE_INVOICE_head.DOC_NO = '" + fndDocNo.Value + "'  ")
                If clsCommon.myLen(strPurchaseTaxInvoiceNo) > 0 Then
                    printDataForBillOfSupply()
                Else
                    clsCommon.MyMessageBoxShow("No data found to print")
                End If
            Else
                clsCommon.MyMessageBoxShow("No data found to print")
            End If

        End If

    End Sub

    Sub printDataForBillOfSupply()
        If clsCommon.myLen(fndDocNo.Value) > 0 Then
            Dim TankerFromMaster As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GateEntryTankerFromTankerMaster, clsFixedParameterCode.GateEntryTankerFromTankerMaster, Nothing))
            Dim strQuery As String = ""
            If TankerFromMaster = 0 Then

                strQuery = " ;with BulkMilkSRN as (select * from tspl_bulk_milk_srn where isnull(TSPL_Bulk_MILK_SRN.srn_return_no,'')=''  ) select TSPL_BULK_MILK_PURCHASE_INVOICE_detail.SRN_NO,convert (varchar,TSPL_BULK_MILK_PURCHASE_INVOICE_detail.SRN_Date,103) as SRN_Date, isnull(TSPL_BULK_MILK_PURCHASE_INVOICE_head.Purchase_Tax_Invoice,'') as Purchase_Tax_Invoice,isnull(TSPL_BULK_MILK_PURCHASE_INVOICE_head.Purchase_Tax_Invoice_Type,'') as Purchase_Tax_Invoice_Type ,  TSPL_ITEM_MASTER.HSN_Code,tspl_state_master_for_location_state.GST_STATE_Code as LOC_GST_State_Code,TSPL_LOCATION_MASTER.GSTNO as Loc_GstInNo,TSPL_VENDOR_MASTER.GSTFinalNo AS Vendor_GSTIN_NO,TSPL_STATE_MASTER.GST_STATE_Code AS Vendor_GST_StateCode, Tspl_Gate_Entry_Details.Gate_Entry_No,convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,103) as Date_And_Time,tspl_Bulk_milk_purchase_Invoice_head.Total_AMT as Sum_of_ActualAmt ,tspl_Bulk_milk_purchase_Invoice_head.Total_QTY,tspl_Bulk_milk_purchase_Invoice_head.DOC_NO ,convert(varchar,tspl_Bulk_milk_purchase_Invoice_head.DOC_DATE,103) as DOC_DATE ,tspl_bulk_milk_purchase_invoice_head.EWayBillNo,convert(varchar,tspl_bulk_milk_purchase_invoice_head.EWayBillDate,103) as EWayBillDate,tspl_bulk_milk_purchase_invoice_head.Electronic_Ref_No,tspl_Bulk_milk_purchase_Invoice_head.Vendor_Invoice_No,tspl_Bulk_milk_purchase_Invoice_Detail.Item_Code ,TSPL_ITEM_MASTER.Item_Desc  ,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2  ,TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add4,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add4,'') else ' ' end  as Loc_Add,TSPL_LOCATION_MASTER.TIN_No as Loc_TinNo,TSPL_LOCATION_MASTER.Pin_Code  as Loc_PINCode,TSPL_LOCATION_MASTER.Email as Loc_Email,case when ISNULL(TSPL_LOCATION_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_LOCATION_MASTER.Phone1 end +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_LOCATION_MASTER.Phone2 Else'' End as  Loc_Phn,TSPL_VENDOR_MASTER.Vendor_Name ,TSPL_VENDOR_MASTER.add1 +case when len(TSPL_VENDOR_MASTER.add2)>0 then ', '+TSPL_VENDOR_MASTER.add2 else '' end +case when LEN(isnull(TSPL_VENDOR_MASTER.Add3,''))>0 then ', '+isnull(TSPL_VENDOR_MASTER.Add3,'') else ' ' end   as Ven_Add,TSPL_VENDOR_MASTER.Tin_No as Ven_TINNo ,TSPL_VENDOR_MASTER.Email as Ven_Email ,case when ISNULL(TSPL_VENDOR_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_VENDOR_MASTER.Phone1 end +  Case When   ISNULL(TSPL_VENDOR_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_VENDOR_MASTER.Phone2 Else'' End as Ven_Phn,BulkMilkSrn.SRN_NO ,convert(varchar,BulkMilkSrn.SRN_Date,103) as SRN_Date ,BulkMilkSrn.Tanker_No ,"
                If RunBulkProcOnAdjustFATCLR = 0 Then
                    strQuery += " case when len(isnull(t_FAT.Param_Field_Value,''))>0 then t_FAT.Param_Field_Value else TSPL_BULK_MILK_PURCHASE_INVOICE_detail.fat_per end As FAT, case when len(isnull(t_SNF .Param_Field_Value,''))>0 then t_SNF .Param_Field_Value else TSPL_BULK_MILK_PURCHASE_INVOICE_detail.snf_Per end as SNF, "
                Else
                    strQuery += "  tspl_Bulk_milk_purchase_Invoice_Detail.fat_per As FAT ,tspl_Bulk_milk_purchase_Invoice_Detail.snf_Per AS SNF,"
                End If
                strQuery += " +  case when isnull(TSPL_Weighment_Detail.Net_Weight,'')>0 then TSPL_Weighment_Detail.Net_Weight else TSPL_BULK_MILK_PURCHASE_INVOICE_detail.Invoice_Qty  end as Milk_qty , 'For  FAT' +convert(varchar,TSPL_Bulk_Price_MASTER.Fat_Percentage) + ' % &  SNF' + convert(varchar,TSPL_Bulk_Price_MASTER.Snf_Percentage)+ ' %' as 'MilkRate' , 'For ' +convert(varchar,TSPL_Bulk_Price_MASTER.Fat_Weightage ) + ' & ' +  convert(varchar,TSPL_Bulk_Price_MASTER.Snf_Weightage ) as 'Weightage',tspl_Bulk_milk_purchase_Invoice_Detail.NetRate,tspl_Bulk_milk_purchase_Invoice_Detail.Actual_Amount,tspl_Bulk_milk_purchase_Invoice_head.RoundOffAmount ,(BulkMilkSrn.Incentive+BulkMilkSrn.Deduction-BulkMilkSrn.SpecialDeduction)as Ded_Inc,case when isnull(BulkMilkSrn.BasicRate,0)>0 then BulkMilkSrn.BasicRate else TSPL_BULK_MILK_PURCHASE_INVOICE_detail.NetRate end as BasicRate ,BulkMilkSrn.Fat_KG,BulkMilkSrn.SNF_KG " & _
                                   " from tspl_Bulk_milk_purchase_Invoice_Detail  left outer join tspl_Bulk_milk_purchase_Invoice_head on tspl_Bulk_milk_purchase_Invoice_head.DOC_NO =tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code =tspl_Bulk_milk_purchase_Invoice_Detail.Item_Code   left outer join TSPL_COMPANY_MASTER  on TSPL_COMPANY_MASTER.Comp_Code =tspl_Bulk_milk_purchase_Invoice_head.Comp_Code   left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER .Location_Code =tspl_Bulk_milk_purchase_Invoice_head.loc_code  left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER .Vendor_Code =tspl_Bulk_milk_purchase_Invoice_head.vendor_code  left join TSPL_VENDOR_BANK_MASTER on TSPL_VENDOR_MASTER.BANK_CODE=TSPL_VENDOR_BANK_MASTER.BANK_CODE left outer join BulkMilkSrn  on BulkMilkSrn.SRN_NO =tspl_Bulk_milk_purchase_Invoice_Detail.SRN_NO   left outer join Tspl_Gate_Entry_Details on Tspl_Gate_Entry_Details.Gate_Entry_No =BulkMilkSrn.Gate_Entry_No  Left Outer Join (Select TSPL_QC_Parameter_Detail.* From BulkMilkSrn Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No  =        BulkMilkSrn.QC_No  And        TSPL_QC_Parameter_Detail.Param_Type = 'FAT') t_FAT      On t_FAT.QC_No   = BulkMilkSrn.QC_No  Left Outer Join (Select TSPL_QC_Parameter_Detail.* From BulkMilkSrn Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No  =        BulkMilkSrn.QC_No  And        TSPL_QC_Parameter_Detail.Param_Type = 'SNF') t_SNF      On t_SNF .QC_No   = BulkMilkSrn.QC_No  left outer join TSPL_Bulk_Price_MASTER on TSPL_Bulk_Price_MASTER.Price_Code=tspl_Bulk_milk_purchase_Invoice_Detail.Price_code  left outer join TSPL_Weighment_Detail on TSPL_Weighment_Detail.Weighment_No =BulkMilkSrn.Weighment_No  left outer join TSPL_STATE_MASTER on TSPL_VENDOR_MASTER.State_Code = TSPL_STATE_MASTER.STATE_CODE left outer join tspl_state_master as tspl_state_master_for_location_state on  tspl_state_master_for_location_state.state_code=tspl_location_master.state  where tspl_bulk_milk_purchase_invoice_head.DOC_NO in ('" & fndDocNo.Value & "') order by Vendor_name,Date_And_Time"
            Else
                Dim frm As New RptBulkMilkMultiplePurchaseInvoice
                strQuery = GetQueryMuliPI("", "", fndDocNo.Value)
            End If

                Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQuery)
                frmCrystalReportViewer.funreport(CrystalReportFolder.MilkProcurement, dt, "rptBulkMilkPurchaseInvoice_Bill_of_Supply", "Purchase Invoice", clsCommon.myCDate(dtpDocDate.Value))
            Else
                clsCommon.MyMessageBoxShow("Please select an invoice to print")
            End If
    End Sub
    Function GetQueryMuliPI(ByVal frmDate As String, ByVal ToDate As String, ByVal InvoiceNo As String) As String
        Dim qry = " ;with BulkMilkSRN as (select * from tspl_bulk_milk_srn where isnull(TSPL_Bulk_MILK_SRN.srn_return_no,'')=''  ) " & _
               "select tspl_bulk_milk_purchase_invoice_head.EWayBillNo,convert(varchar,tspl_bulk_milk_purchase_invoice_head.EWayBillDate,103) as EWayBillDate,tspl_bulk_milk_purchase_invoice_head.Electronic_Ref_No, isnull(TSPL_BULK_MILK_PURCHASE_INVOICE_head.Purchase_Tax_Invoice,'') as Purchase_Tax_Invoice,isnull(TSPL_BULK_MILK_PURCHASE_INVOICE_head.Purchase_Tax_Invoice_Type,'') as Purchase_Tax_Invoice_Type ,  TSPL_ITEM_MASTER.HSN_Code,tspl_state_master_for_location_state.GST_STATE_Code as LOC_GST_State_Code,TSPL_LOCATION_MASTER.GSTNO as Loc_GstInNo ,TSPL_VENDOR_MASTER.GSTFinalNo AS Vendor_GSTIN_NO,TSPL_STATE_MASTER.GST_STATE_Code AS Vendor_GST_StateCode,   Tspl_Gate_Entry_Details.Gate_Entry_No,convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,103) as Date_And_Time, " & _
               "'" & frmDate & "' as frmDate,'" & ToDate & "' as ToDate, CONVERT(VARCHAR(15),tspl_Bulk_milk_purchase_Invoice_head.SRN_From_Date,103) AS From_Date,convert(varchar(15),tspl_Bulk_milk_purchase_Invoice_head.SRN_TO_Date,103) as To_Date, TSPL_LOCATION_MASTER.add1 as Loc_Add1,tspl_Bulk_milk_purchase_Invoice_head.Loc_Code , " & _
               "TSPL_LOCATION_MASTER.Location_Desc ,TSPL_VENDOR_MASTER.Vendor_Code as Ven_code,tspl_Bulk_milk_purchase_Invoice_Detail.Actual_Amount as Total_AMT ,tspl_Bulk_milk_purchase_Invoice_head.Total_AMT as Sum_of_ActualAmt, " & _
               "tspl_Bulk_milk_purchase_Invoice_Detail.Invoice_Qty as Total_QTY,tspl_Bulk_milk_purchase_Invoice_head.DOC_NO , " & _
               "convert(varchar,tspl_Bulk_milk_purchase_Invoice_head.DOC_DATE,103) as DOC_DATE ,tspl_Bulk_milk_purchase_Invoice_head.Vendor_Invoice_No, " & _
               "tspl_Bulk_milk_purchase_Invoice_Detail.Item_Code ,TSPL_ITEM_MASTER.Item_Desc  ,TSPL_COMPANY_MASTER.Comp_Name , " & _
               "TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2  ,TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add4,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add4,'') else ' ' end  as Loc_Add, " & _
               "TSPL_LOCATION_MASTER.TIN_No as Loc_TinNo,TSPL_LOCATION_MASTER.Pin_Code  as Loc_PINCode,TSPL_LOCATION_MASTER.Email as Loc_Email,case when ISNULL(TSPL_LOCATION_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_LOCATION_MASTER.Phone1 end +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_LOCATION_MASTER.Phone2 Else'' End as  Loc_Phn, " & _
               "TSPL_VENDOR_MASTER.Vendor_Name ,TSPL_VENDOR_MASTER.add1 +case when len(TSPL_VENDOR_MASTER.add2)>0 then ', '+TSPL_VENDOR_MASTER.add2 else '' end +case when LEN(isnull(TSPL_VENDOR_MASTER.Add3,''))>0 then ', '+isnull(TSPL_VENDOR_MASTER.Add3,'') else ' ' end   as Ven_Add, " & _
               "TSPL_VENDOR_MASTER.Tin_No as Ven_TINNo ,TSPL_VENDOR_MASTER.Email as Ven_Email , " & _
               "case when ISNULL(TSPL_VENDOR_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_VENDOR_MASTER.Phone1 end +  Case When   ISNULL(TSPL_VENDOR_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_VENDOR_MASTER.Phone2 Else'' End as Ven_Phn, " & _
               "BulkMilkSrn.SRN_NO ,convert(varchar,BulkMilkSrn.SRN_Date,103) as SRN_Date ,BulkMilkSrn.Tanker_No , "
        If RunBulkProcOnAdjustFATCLR = 0 Then
            qry += "case when len(isnull(t_FAT.Param_Field_Value,''))>0 then t_FAT.Param_Field_Value else TSPL_BULK_MILK_PURCHASE_INVOICE_detail.fat_per end As FAT, case when len(isnull(t_SNF .Param_Field_Value,''))>0 then t_SNF .Param_Field_Value else TSPL_BULK_MILK_PURCHASE_INVOICE_detail.snf_Per end as SNF,  "
        Else
            qry += "  tspl_Bulk_milk_purchase_Invoice_Detail.fat_per As FAT ,tspl_Bulk_milk_purchase_Invoice_Detail.snf_Per AS SNF,"
        End If

        qry += " case when isnull(TSPL_WEIGHMENT_CHEMBER_DETAILS.Net_Weight,'')>0 then TSPL_WEIGHMENT_CHEMBER_DETAILS.Net_Weight else TSPL_BULK_MILK_PURCHASE_INVOICE_detail.Invoice_Qty  end as Milk_qty , " & _
               "'For  FAT' +convert(varchar,TSPL_BULK_PRICE_DETAIL.Fat_Percentage) + ' % &  SNF' +  convert(varchar,TSPL_BULK_PRICE_DETAIL.Snf_Percentage)+ ' %' as 'MilkRate' , " & _
               "'For ' +convert(varchar,TSPL_BULK_PRICE_DETAIL.Fat_Weightage ) + ' & ' +  convert(varchar,TSPL_BULK_PRICE_DETAIL.Snf_Weightage ) as 'Weightage', " & _
               "tspl_Bulk_milk_purchase_Invoice_Detail.NetRate,tspl_Bulk_milk_purchase_Invoice_Detail.Actual_Amount,tspl_Bulk_milk_purchase_Invoice_head.RoundOffAmount , " & _
               "(TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Incentive+TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Deduction-TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SpecialDeduction)as Ded_Inc, " & _
               "TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.BasicRate,TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Fat_KG,TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SNF_KG, " & _
               "TSPL_VENDOR_MASTER.Bank_Code,TSPL_VENDOR_BANK_MASTER.Bank_Name,(TSPL_VENDOR_BANK_MASTER.Add1+TSPL_VENDOR_BANK_MASTER.Add2+TSPL_VENDOR_BANK_MASTER.Add3)as Bank_Address, " & _
               "Account_No from " & _
               "tspl_Bulk_milk_purchase_Invoice_Detail " & _
               "left outer join tspl_Bulk_milk_purchase_Invoice_head on tspl_Bulk_milk_purchase_Invoice_head.DOC_NO =tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO " & _
               "left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code =tspl_Bulk_milk_purchase_Invoice_Detail.Item_Code  " & _
               "left outer join TSPL_COMPANY_MASTER  on TSPL_COMPANY_MASTER.Comp_Code =tspl_Bulk_milk_purchase_Invoice_head.Comp_Code  " & _
               "left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER .Location_Code =tspl_Bulk_milk_purchase_Invoice_head.loc_code " & _
               "left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER .Vendor_Code =tspl_Bulk_milk_purchase_Invoice_head.vendor_code " & _
               " left join TSPL_VENDOR_BANK_MASTER on TSPL_VENDOR_MASTER.BANK_CODE=TSPL_VENDOR_BANK_MASTER.BANK_CODE " & _
               "left outer join BulkMilkSrn  on BulkMilkSrn.SRN_NO =tspl_Bulk_milk_purchase_Invoice_Detail.SRN_NO " & _
               "left outer join TSPL_BULK_MILK_SRN_CHEMBER_DETAILS  on BulkMilkSrn.SRN_NO =TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SRN_NO and " & _
               "tspl_Bulk_milk_purchase_Invoice_Detail.CHAMBER_DESC=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Chamber_Desc " & _
               "left outer join Tspl_Gate_Entry_Details on Tspl_Gate_Entry_Details.Gate_Entry_No =BulkMilkSrn.Gate_Entry_No  " & _
               "Left Outer Join (Select TSPL_QC_Parameter_Detail.* From BulkMilkSrn Left Outer Join TSPL_QC_Parameter_Detail On " & _
               "TSPL_QC_Parameter_Detail.QC_No  =  BulkMilkSrn.QC_No  And  TSPL_QC_Parameter_Detail.Param_Type = 'FAT') t_FAT " & _
               "On t_FAT.QC_No   = BulkMilkSrn.QC_No and t_FAT.LINE_NO=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Line_No " & _
               "Left Outer Join (Select TSPL_QC_Parameter_Detail.* From BulkMilkSrn Left Outer Join TSPL_QC_Parameter_Detail On " & _
               "TSPL_QC_Parameter_Detail.QC_No  = BulkMilkSrn.QC_No  And TSPL_QC_Parameter_Detail.Param_Type = 'SNF') t_SNF On " & _
               "t_SNF .QC_No   = BulkMilkSrn.QC_No   and t_SNF.LINE_NO=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Line_No " & _
               "left outer join TSPL_Bulk_Price_MASTER on TSPL_Bulk_Price_MASTER.Price_Code=tspl_Bulk_milk_purchase_Invoice_Detail.Price_code  " & _
               "left outer join TSPL_BULK_PRICE_DETAIL on TSPL_Bulk_Price_MASTER.Price_Code=TSPL_BULK_PRICE_DETAIL.Price_code  and " & _
               "TSPL_BULK_PRICE_DETAIL.Milk_Grade_code=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.MILK_GRADE_CODE " & _
               "left outer join TSPL_Weighment_Detail on TSPL_Weighment_Detail.Weighment_No =BulkMilkSrn.Weighment_No " & _
               "left outer join  TSPL_WEIGHMENT_CHEMBER_DETAILS on TSPL_Weighment_Detail.Weighment_No=TSPL_WEIGHMENT_CHEMBER_DETAILS.Weighment_No and " & _
               "TSPL_WEIGHMENT_CHEMBER_DETAILS.Line_No=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Line_No " & _
               " left outer join TSPL_STATE_MASTER on TSPL_VENDOR_MASTER.State_Code = TSPL_STATE_MASTER.STATE_CODE " & _
               " left outer join tspl_state_master as tspl_state_master_for_location_state on  " & _
               " tspl_state_master_for_location_state.state_code=tspl_location_master.state  " & _
               " where tspl_bulk_milk_purchase_invoice_head.DOC_NO in ('" + InvoiceNo + "') order by Vendor_name,Date_And_Time"
        Return qry
    End Function
End Class
