Imports common
Imports System.Data.SqlClient
Public Class FrmMilkPurchaseInvoice
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim intHighClassVendor As Integer = 0
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
    Public Const colXtraRate As String = "colXtraRate"
    Public Const colNetRate As String = "colNetRate"
    Public Const colPriceCode As String = "colPriceCode"
    Public Const colTnkrNo As String = "colTnkrNo"


    Const colTax1 As String = "COLTAX1"
    Const colTaxBaseAmt1 As String = "COLTAXBASEAMT1"
    Const colTaxRate1 As String = "COLTAXRATE1"
    Const colTaxAmt1 As String = "COLTAXAMT1"
    Const colIsTaxable1 As String = "ISTAXABLE1"
    Const colIsSurTax1 As String = "ISSURTAX1"
    Const colSurTaxCode1 As String = "SURTAXCODE1"
    Const colIsExcisable1 As String = "ISEXCISABLE1"
    Const colTaxOnBaseAmt1 As String = "colTaxOnBaseAmt1"

    Const colTax2 As String = "COLTAX2"
    Const colTaxBaseAmt2 As String = "COLTAXBASEAMT2"
    Const colTaxRate2 As String = "COLTAXRATE2"
    Const colTaxAmt2 As String = "COLTAXAMT2"
    Const colIsTaxable2 As String = "ISTAXABLE2"
    Const colIsSurTax2 As String = "ISSURTAX2"
    Const colSurTaxCode2 As String = "SURTAXCODE2"
    Const colIsExcisable2 As String = "ISEXCISABLE2"
    Const colTaxOnBaseAmt2 As String = "colTaxOnBaseAmt2"

    Const colTax3 As String = "COLTAX3"
    Const colTaxBaseAmt3 As String = "COLTAXBASEAMT3"
    Const colTaxRate3 As String = "COLTAXRATE3"
    Const colTaxAmt3 As String = "COLTAXAMT3"
    Const colIsTaxable3 As String = "ISTAXABLE3"
    Const colIsSurTax3 As String = "ISSURTAX3"
    Const colSurTaxCode3 As String = "SURTAXCODE3"
    Const colIsExcisable3 As String = "ISEXCISABLE3"
    Const colTaxOnBaseAmt3 As String = "colTaxOnBaseAmt3"

    Const colTax4 As String = "COLTAX4"
    Const colTaxBaseAmt4 As String = "COLTAXBASEAMT4"
    Const colTaxRate4 As String = "COLTAXRATE4"
    Const colTaxAmt4 As String = "COLTAXAMT4"
    Const colIsTaxable4 As String = "ISTAXABLE4"
    Const colIsSurTax4 As String = "ISSURTAX4"
    Const colSurTaxCode4 As String = "SURTAXCODE4"
    Const colIsExcisable4 As String = "ISEXCISABLE4"
    Const colTaxOnBaseAmt4 As String = "colTaxOnBaseAmt4"

    Const colTax5 As String = "COLTAX5"
    Const colTaxBaseAmt5 As String = "COLTAXBASEAMT5"
    Const colTaxRate5 As String = "COLTAXRATE5"
    Const colTaxAmt5 As String = "COLTAXAMT5"
    Const colIsTaxable5 As String = "ISTAXABLE5"
    Const colIsSurTax5 As String = "ISSURTAX5"
    Const colSurTaxCode5 As String = "SURTAXCODE5"
    Const colIsExcisable5 As String = "ISEXCISABLE5"
    Const colTaxOnBaseAmt5 As String = "colTaxOnBaseAmt5"

    Const colTax6 As String = "COLTAX6"
    Const colTaxBaseAmt6 As String = "COLTAXBASEAMT6"
    Const colTaxRate6 As String = "COLTAXRATE6"
    Const colTaxAmt6 As String = "COLTAXAMT6"
    Const colIsTaxable6 As String = "ISTAXABLE6"
    Const colIsSurTax6 As String = "ISSURTAX6"
    Const colSurTaxCode6 As String = "SURTAXCODE6"
    Const colIsExcisable6 As String = "ISEXCISABLE6"
    Const colTaxOnBaseAmt6 As String = "colTaxOnBaseAmt6"

    Const colTax7 As String = "COLTAX7"
    Const colTaxBaseAmt7 As String = "COLTAXBASEAMT7"
    Const colTaxRate7 As String = "COLTAXRATE7"
    Const colTaxAmt7 As String = "COLTAXAMT7"
    Const colIsTaxable7 As String = "ISTAXABLE7"
    Const colIsSurTax7 As String = "ISSURTAX7"
    Const colSurTaxCode7 As String = "SURTAXCODE7"
    Const colIsExcisable7 As String = "ISEXCISABLE7"
    Const colTaxOnBaseAmt7 As String = "colTaxOnBaseAmt7"

    Const colTax8 As String = "COLTAX8"
    Const colTaxBaseAmt8 As String = "COLTAXBASEAMT8"
    Const colTaxRate8 As String = "COLTAXRATE8"
    Const colTaxAmt8 As String = "COLTAXAMT8"
    Const colIsTaxable8 As String = "ISTAXABLE8"
    Const colIsSurTax8 As String = "ISSURTAX8"
    Const colSurTaxCode8 As String = "SURTAXCODE8"
    Const colIsExcisable8 As String = "ISEXCISABLE8"
    Const colTaxOnBaseAmt8 As String = "colTaxOnBaseAmt8"

    Const colTax9 As String = "COLTAX9"
    Const colTaxBaseAmt9 As String = "COLTAXBASEAMT9"
    Const colTaxRate9 As String = "COLTAXRATE9"
    Const colTaxAmt9 As String = "COLTAXAMT9"
    Const colIsTaxable9 As String = "ISTAXABLE9"
    Const colIsSurTax9 As String = "ISSURTAX9"
    Const colSurTaxCode9 As String = "SURTAXCODE9"
    Const colIsExcisable9 As String = "ISEXCISABLE9"
    Const colTaxOnBaseAmt9 As String = "colTaxOnBaseAmt9"

    Const colTax10 As String = "COLTAX10"
    Const colTaxBaseAmt10 As String = "COLTAXBASEAMT10"
    Const colTaxRate10 As String = "COLTAXRATE10"
    Const colTaxAmt10 As String = "COLTAXAMT10"
    Const colIsTaxable10 As String = "ISTAXABLE10"
    Const colIsSurTax10 As String = "ISSURTAX10"
    Const colSurTaxCode10 As String = "SURTAXCODE10"
    Const colIsExcisable10 As String = "ISEXCISABLE10"
    Const colTaxOnBaseAmt10 As String = "colTaxOnBaseAmt10"

    Const colTotTaxAmt As String = "TAXAMT"
    Const colItemNetAmt As String = "AMTAFTERTAX"
    Const colTranspoterCharge As String = "colTranspoterCharge"

    Const colTTaxAutCode As String = "TAXAUTCODE"
    Const colTTaxAutName As String = "TAXAUTNAME"
    Const colTTaxRate As String = "TAXRATE"
    Const colTBaseAmt As String = "TAXBASEAMT"
    Const colTTaxAmt As String = "TAXAMT"

    Public isCellValueChangedOpen As Boolean
    Public errorControl As clsErrorControl = New clsErrorControl()
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim obj As clsMilkPurchaseInvoiceHead = Nothing
    Dim RunBulkProcOnAdjustFATCLR As Integer = 0
    Dim SettCalculateLtrQtyFromKGQtyByCLR As Boolean = False
    Dim SettApplyCalculateWeightInLtr As Boolean = False
    Dim settApplyTCSTax As Boolean = False
    Dim ApplyTransportChargeAddInActualAmount As Boolean = False
    Private isCellValueChangedTaxOpen As Boolean = False

    Dim ConsiderPreviousandCurrentFYForTCSTaxCustOutstanding As Boolean = False
    Dim AmountToCheckVendorOutstandingForTCSTax As Decimal = 0
    Public AllowtoChangeTCSBaseAmountPurchase As Boolean = False

    Private objRemittance As clsRemittance
    Dim dblPreviousTDSAmt As Double = 0
    Dim strShowMessageTDS As Boolean = False
    Dim ShowMessageTDS As Boolean = False

    Dim SettBulkMilkFATSNFKGDecimalPlaces As Integer = 0
    Dim SettBulkMilkFATSNFAmtDecimalPlaces As Integer = 0

#End Region
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmBulkMilkPurchaseInvoice)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPrint.Visible = MyBase.isPrintFlag
        btnBillOfSupply.Visible = MyBase.isPrintFlag
        RadMenu1.Visible = MyBase.isExport
        btnPost.Visible = MyBase.isPostFlag

    End Sub
    Private Sub FrmMilkPurchaseInvoice_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim coll As New Dictionary(Of String, String)
        coll.Add("Xtra_Rate", "decimal(18, 2) NULL")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_BULK_MILK_PURCHASE_INVOICE_DETAIL", coll, "", False, False, "TSPL_BULK_MILK_PURCHASE_INVOICE_HEAD", "DOC_NO", "")


        SettBulkMilkFATSNFKGDecimalPlaces = clsFixedParameter.GetData(clsFixedParameterType.BulkMilkFATSNFKGDecimalPlaces, clsFixedParameterCode.BulkMilkFATSNFKGDecimalPlaces, Nothing)
        SettBulkMilkFATSNFAmtDecimalPlaces = clsFixedParameter.GetData(clsFixedParameterType.BulkMilkFATSNFAmtDecimalPlaces, clsFixedParameterCode.BulkMilkFATSNFAmtDecimalPlaces, Nothing)

        RadPageView1.SelectedPage = RadPageViewPage1
        settApplyTCSTax = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyTaxInBulkMilkPurchaseInvoice, clsFixedParameterCode.ApplyTaxInBulkMilkPurchaseInvoice, Nothing)) = 1)
        Panel3.Enabled = False
        BulkProcPriceChartStandardRateWithZero = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.BulkProcPriceChartStandardRateWithZero, clsFixedParameterCode.BulkProcPriceChartStandardRateWithZero, Nothing))
        AllowJobWorkonGateEntryBulkProc = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowJobWorkonGateEntryBulkProc, clsFixedParameterCode.AllowJobWorkonGateEntryBulkProc, Nothing))
        ShowMessageTDS = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowMessgForTDS, clsFixedParameterCode.ShowMessgForTDS, Nothing)) = "1", True, False))
        SetUserMgmtNew()
        TankerFromMaster = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GateEntryTankerFromTankerMaster, clsFixedParameterCode.GateEntryTankerFromTankerMaster, Nothing))
        ''=======BM00000010118===============
        AllowTruncateAmount = IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.Allow_AmountTruncate_BulkMilkSRN, clsFixedParameterCode.Allow_AmountTruncate_BulkMilkSRN, Nothing)) = "1", True, False)
        ''=====================
        ''====Adjusted Fat & SNF======
        RunBulkProcOnAdjustFATCLR = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RunBulkProcOnAdjustedFATCLR, clsFixedParameterCode.RunBulkProcOnAdjustedFATCLR, Nothing))
        ''=========================
        SettApplyCalculateWeightInLtr = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyCalculateWeightInLtr, clsFixedParameterCode.ApplyCalculateWeightInLtr, Nothing)) > 0)
        SettCalculateLtrQtyFromKGQtyByCLR = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CalculateLtrQtyFromKGQtyByCLR, clsFixedParameterCode.CalculateLtrQtyFromKGQtyByCLR, Nothing)) > 0)
        ApplyTransportChargeAddInActualAmount = IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ApplyTransportChargeAddInActualAmount, clsFixedParameterCode.ApplyTransportChargeAddInActualAmount, Nothing)) = "1", True, False)
        If ApplyTransportChargeAddInActualAmount = True Then
            lblTransporterCharge.Visible = True
            txtTransporterCharge.Visible = True
        Else
            lblTransporterCharge.Visible = False
            txtTransporterCharge.Visible = False
        End If
        ConsiderPreviousandCurrentFYForTCSTaxCustOutstanding = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ConsiderPreviousCurrentFYForTCSTaxVendOutstanding, clsFixedParameterCode.ConsiderPreviousCurrentFYForTCSTaxVendOutstanding, Nothing)) > 0)
        AmountToCheckVendorOutstandingForTCSTax = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AmountToCheckVendorOutstandingForTCSTax, clsFixedParameterCode.AmountToCheckVendorOutstandingForTCSTax, Nothing))
        AllowtoChangeTCSBaseAmountPurchase = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowtoChangeTCSBaseAmountPurchase, clsFixedParameterCode.AllowtoChangeTCSBaseAmountPurchase, Nothing)) > 0)

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

        If AllowJobWorkonGateEntryBulkProc = 1 Then
            Panel3.Visible = True
        Else
            Panel3.Visible = False
        End If
        If Not settApplyTCSTax Then
            RadPageView1.Pages("RadPageViewPage2").Item.Visibility = True
        End If

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

                                docNo = clsCommon.myCstr(dr("VendorInvoiceNo"))
                                clsVedorInvoiceHead.ReverseAndUnpost(docNo, trans)
                                clsVedorInvoiceHead.DeleteData(docNo, trans)

                                ''Delete AP Invoice Journal Entry 
                                'docNo = clsCommon.myCstr(dr("APINVVoucherNO"))
                                'If clsCommon.myLen(docNo) > 0 Then
                                '    qry = "delete from TSPL_JOURNAL_DETAILS where TSPL_JOURNAL_DETAILS.Voucher_No  in ('" + docNo + "')"
                                '    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                                '    qry = "delete from TSPL_JOURNAL_MASTER where TSPL_JOURNAL_MASTER.Voucher_No in ('" + docNo + "')"
                                '    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                                'End If


                                'Dim dtAP As DataTable
                                '' Get Payment Entry Against AP Invoice
                                'docNo = clsCommon.myCstr(dr("VendorInvoiceNo"))



                                ''If clsCommon.myLen(docNo) > 0 Then
                                ''    qry = "select Payment_No from TSPL_PAYMENT_DETAIL  where Document_No in ('" + docNo + "')"
                                ''    dtAP = clsDBFuncationality.GetDataTable(qry, trans)
                                ''    If dtAP IsNot Nothing AndAlso dtAP.Rows.Count > 0 Then
                                ''        qry = "AP-Invoice " + docNo + " is used in following Payment -"
                                ''        For Each drAP As DataRow In dtAP.Rows
                                ''            qry += Environment.NewLine + clsCommon.myCstr(drAP("Payment_No"))
                                ''        Next
                                ''        Throw New Exception(qry)
                                ''    End If
                                ''End If

                                '''' Get Payment Entry Against Purchase  Invoice
                                ''docNo = clsCommon.myCstr(dr("DOC_NO"))
                                ''If clsCommon.myLen(docNo) > 0 Then
                                ''    qry = "select Payment_No from TSPL_PAYMENT_DETAIL  where Document_No in ('" + docNo + "')"
                                ''    dtAP = clsDBFuncationality.GetDataTable(qry, trans)
                                ''    If dtAP IsNot Nothing AndAlso dtAP.Rows.Count > 0 Then
                                ''        qry = "Purchase-Invoice " + docNo + " is used in following Payment -"
                                ''        For Each drAP As DataRow In dtAP.Rows
                                ''            qry += Environment.NewLine + clsCommon.myCstr(drAP("Payment_No"))
                                ''        Next
                                ''        Throw New Exception(qry)
                                ''    End If
                                ''End If


                                ''''Delete AP Invoice

                                ''docNo = clsCommon.myCstr(dr("VendorInvoiceNo"))
                                ''If clsCommon.myLen(docNo) > 0 Then
                                ''    qry = "delete from TSPL_VENDOR_INVOICE_DETAIL where Document_No in ('" & docNo & "')"
                                ''    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                                ''    qry = "delete from TSPL_VENDOR_INVOICE_HEAD where Document_No in ('" & docNo & "')"
                                ''    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                                ''End If



                                docNo = clsCommon.myCstr(dr("Adjustment_No"))
                                If clsCommon.myLen(docNo) > 0 Then
                                    ClsAdjustments.ReverseAndUnpost(docNo, trans)
                                    ClsAdjustments.DeleteData(docNo, "", trans)

                                    'Change by balwinder on 02/06/2021 becuase Inventory not deleting
                                    ''qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No in (select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='IC-AD' and Source_Doc_No in ('" + docNo + "'))"
                                    ''clsDBFuncationality.ExecuteNonQuery(qry, trans)
                                    ''qry = "delete from TSPL_JOURNAL_MASTER where Source_Code='IC-AD' and Source_Doc_No in ('" + docNo + "')"
                                    ''clsDBFuncationality.ExecuteNonQuery(qry, trans)

                                    ''qry = "delete from TSPL_ADJUSTMENT_DETAIL where  TSPL_ADJUSTMENT_DETAIL.Adjustment_No in ('" + docNo + "')"
                                    ''clsDBFuncationality.ExecuteNonQuery(qry, trans)
                                    ''qry = "delete from TSPL_ADJUSTMENT_HEADER where Adjustment_No in ('" + docNo + "')"
                                    ''clsDBFuncationality.ExecuteNonQuery(qry, trans)
                                End If

                                ''''''''''''''''''


                                ''Change status to unpost
                                docNo = clsCommon.myCstr(dr("DOC_NO"))
                                qry = "update tspl_Bulk_milk_purchase_Invoice_head set isPosted=0 where DOC_NO in ('" + docNo + "')"
                                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                            Next
                            saveCancelLog(Reason, "Reverse and Recreate", trans)
                            trans.Commit()
                            clsCommon.MyMessageBoxShow(Me, "Task done Successfully", Me.Text)
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
                        common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                        loadData(fndDocNo.Value, NavigatorType.Current)
                    End If
                End If

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
        btnViewTDSDetails.Enabled = False
        chkHighClassVendor.Checked = False
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
        txtTDSAmt.Text = ""
        txtAmtAfterTDS.Text = ""
        txtTaxableAmt.Text = ""
        txtTaxAmt.Text = ""
        txtTaxGroup.Value = ""
        rbtnTaxCalAutomatic.IsChecked = True
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

        If AllowtoChangeTCSBaseAmountPurchase = True Then
            txttcstaxbaseamount.Enabled = True
        Else
            txttcstaxbaseamount.Enabled = False
        End If
        txttcstaxbaseamount.Value = 0
        lblActualTCSTaxBaseAmt.Text = "0"
        txtTCSTaxRate.Value = 0
        dblPreviousTDSAmt = 0
        loadBlankGrid()
        LoadBlankGridTax()

    End Sub
    Public Shared Function isVendorInvoiceNo(ByVal strVendor As String, ByVal trans As SqlTransaction) As Boolean
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
            If gv1.Rows.Count <= 0 Then
                Throw New Exception("Please select atleast one SRN ")
            End If
            ''richa agarwal 25 Apr,2019
            If dtpFromDate.Value > dtpToDate.Value Then
                Throw New Exception(" 'From Date' can't be greater than 'To Date'")
            End If

            If clsCommon.myCDate(dtpDocDate.Value, "dd/MMM/yyyy") < clsCommon.myCDate(dtpToDate.Value, "dd/MMM/yyyy") Then
                fndSRNNo.Value = ""
                Throw New Exception(" 'To Date' can't be greater than 'Doc Date'")
            End If


            Dim isTDSOverride As Boolean = False
            If objRemittance IsNot Nothing Then
                If objRemittance.IsTDSOverride Then
                    isTDSOverride = True
                End If
            End If
            '==================Added by preeti gupta[11/01/2017]
            strShowMessageTDS = False
            If ShowMessageTDS Then
                If (common.clsCommon.MyMessageBoxShow("Do you want to Deduct TDS", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No) Then
                    objRemittance = Nothing

                Else
                    If Not isTDSOverride AndAlso objRemittance IsNot Nothing Then
                        SetVendorTDSDetails()

                    End If
                End If
            Else
                If Not isTDSOverride AndAlso objRemittance IsNot Nothing Then
                    SetVendorTDSDetails()
                End If
            End If
            If Not objRemittance Is Nothing Then
                UpdateTDSAmount()
            End If

            If objRemittance Is Nothing AndAlso objCommonVar.TDSValidationFrom IsNot Nothing Then
                If dtpDocDate.Value >= objCommonVar.TDSValidationFrom Then
                    Dim AmountToCheckVendorOutstandingForTCSTax As Decimal = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AmountToCheckVendorOutstandingForTCSTax, clsFixedParameterCode.AmountToCheckVendorOutstandingForTCSTax, Nothing))
                    If AmountToCheckVendorOutstandingForTCSTax > 0 Then
                        Dim dblOutstandingAmount As Double = clsCommon.myCdbl(clsVendorMaster.GetVendorOutstandingForTCSTaxApplicableOnFY(fndVendor.Value, dtpDocDate.Value))
                        If dblOutstandingAmount > AmountToCheckVendorOutstandingForTCSTax Then
                            clsCommon.MyMessageBoxShow(Me, "Outstanding Amount for Vendor [" + fndVendor.Value + "] Crossed TDS Limit.Please Apply TDS on Same.", Me.Text, MessageBoxButtons.OK)
                        End If
                    End If
                End If
            End If



            ''------------------
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
                For ii As Integer = 0 To gv1.Rows.Count - 1
                    If clsCommon.myCDate(dtpDocDate.Value, "dd/MMM/yyyy") < clsCommon.myCDate(gv1.Rows(ii).Cells(colSRNDATe).Value, "dd/MMM/yyyy") Then
                        Throw New Exception("Invoice Date should not be smaller than SRN date")
                        errorControl.SetError(dtpDocDate, "Invoice Date should not be smaller than SRN date")
                    Else
                        errorControl.ResetError(dtpDocDate)
                    End If

                Next
            End If
            If AllowtoChangeTCSBaseAmountPurchase Then
                If clsCommon.myCdbl(txttcstaxbaseamount.Value) > clsCommon.myCdbl(lblActualTCSTaxBaseAmt.Text) Then
                    Throw New Exception("TCS Tax Base amount should not be greater than Actual TCS Tax Base Amount")
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return True
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
                    clsCommon.MyMessageBoxShow(Me, "Can't delete the record", Me.Text)
                    trans.Rollback()
                End If
            Else

                clsCommon.MyMessageBoxShow(Me, "Please Select a document to delete", Me.Text)
                trans.Rollback()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
                If (clsMilkPurchaseInvoiceHead.postData(fndDocNo.Value, MyBase.Form_ID)) Then
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
                loadData(fndDocNo.Value, NavigatorType.Current)
                'If (common.clsCommon.MyMessageBoxShow("Do you want to print", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes) Then
                '    PrintDataNew()
                'End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
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
                strQuery = " ;with BulkMilkSRN as (select * from tspl_bulk_milk_srn where isnull(TSPL_Bulk_MILK_SRN.srn_return_no,'')=''  ) select tspl_Bulk_milk_purchase_Invoice_Detail.Transport_Charges, " &
                " Case when dtax1.Is_TCS = 'Y' then TSPL_BULK_MILK_PURCHASE_INVOICE_head.TAX1_Rate when  dtax2.Is_TCS = 'Y' then TSPL_BULK_MILK_PURCHASE_INVOICE_head.TAX2_Rate when dtax3.Is_TCS = 'Y' then TSPL_BULK_MILK_PURCHASE_INVOICE_head.TAX3_Rate " &
                " when dtax4.Is_TCS = 'Y' then TSPL_BULK_MILK_PURCHASE_INVOICE_head.TAX4_Rate  when dtax5.Is_TCS = 'Y' then TSPL_BULK_MILK_PURCHASE_INVOICE_head.TAX5_Rate when dtax6.Is_TCS = 'Y' then TSPL_BULK_MILK_PURCHASE_INVOICE_head.TAX6_Rate  when dtax7.Is_TCS = 'Y' then TSPL_BULK_MILK_PURCHASE_INVOICE_head.TAX7_Rate when dtax8.Is_TCS = 'Y' then TSPL_BULK_MILK_PURCHASE_INVOICE_head.TAX8_Rate when dtax9.Is_TCS = 'Y' then TSPL_BULK_MILK_PURCHASE_INVOICE_head.TAX9_Rate when dtax10.Is_TCS = 'Y' then TSPL_BULK_MILK_PURCHASE_INVOICE_head.TAX10_Rate end as TCS_Rate " &
                " ,Case when dtax1.Is_TCS = 'Y' then TSPL_BULK_MILK_PURCHASE_INVOICE_head.TAX1_Amt when  dtax2.Is_TCS = 'Y' then TSPL_BULK_MILK_PURCHASE_INVOICE_head.TAX2_Amt when dtax3.Is_TCS = 'Y' then TSPL_BULK_MILK_PURCHASE_INVOICE_head.TAX3_Amt when dtax4.Is_TCS = 'Y' then TSPL_BULK_MILK_PURCHASE_INVOICE_head.TAX4_Amt  when dtax5.Is_TCS = 'Y' then TSPL_BULK_MILK_PURCHASE_INVOICE_head.TAX5_Amt when dtax6.Is_TCS = 'Y' then TSPL_BULK_MILK_PURCHASE_INVOICE_head.TAX6_Amt  when dtax7.Is_TCS = 'Y' then TSPL_BULK_MILK_PURCHASE_INVOICE_head.TAX7_Amt when dtax8.Is_TCS = 'Y' then TSPL_BULK_MILK_PURCHASE_INVOICE_head.TAX8_Amt when dtax9.Is_TCS = 'Y' then TSPL_BULK_MILK_PURCHASE_INVOICE_head.TAX9_Amt when dtax10.Is_TCS = 'Y' then TSPL_BULK_MILK_PURCHASE_INVOICE_head.TAX10_Amt end  as TCS_Amount" &
                ",isnull(TSPL_BULK_MILK_PURCHASE_INVOICE_head.Purchase_Tax_Invoice,'') as Purchase_Tax_Invoice,isnull(TSPL_BULK_MILK_PURCHASE_INVOICE_head.Purchase_Tax_Invoice_Type,'') as Purchase_Tax_Invoice_Type ,  TSPL_ITEM_MASTER.HSN_Code,tspl_state_master_for_location_state.GST_STATE_Code as LOC_GST_State_Code,TSPL_LOCATION_MASTER.GSTNO as Loc_GstInNo,TSPL_VENDOR_MASTER.GSTFinalNo AS Vendor_GSTIN_NO,TSPL_STATE_MASTER.GST_STATE_Code AS Vendor_GST_StateCode ,case when isnull(Tspl_Gate_Entry_Details.Gate_Entry_No,'')='' then 'SRN No' else 'GRN No' end as Gate_SRN_No ,case when isnull(convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,103),'')='' then 'SRN Date' else 'GRN Date' end as Gate_SRN_Date,(tspl_Bulk_milk_purchase_Invoice_head.Total_AMT)-isnull(TSPL_PI_REMITTANCE.Actual_Total_TDS,0) as Sum_of_ActualAmt  ,tspl_Bulk_milk_purchase_Invoice_head.Total_QTY,tspl_Bulk_milk_purchase_Invoice_head.DOC_NO ,convert(varchar,tspl_Bulk_milk_purchase_Invoice_head.DOC_DATE,103) as DOC_DATE ,tspl_bulk_milk_purchase_invoice_head.EWayBillNo,convert(varchar,tspl_bulk_milk_purchase_invoice_head.EWayBillDate,103) as EWayBillDate,tspl_bulk_milk_purchase_invoice_head.Electronic_Ref_No,tspl_Bulk_milk_purchase_Invoice_head.Vendor_Invoice_No,tspl_Bulk_milk_purchase_Invoice_Detail.Item_Code ,TSPL_ITEM_MASTER.Item_Desc  ,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2  ,TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add4,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add4,'') else ' ' end  as Loc_Add,TSPL_LOCATION_MASTER.TIN_No as Loc_TinNo,TSPL_LOCATION_MASTER.Pin_Code  as Loc_PINCode,TSPL_LOCATION_MASTER.Email as Loc_Email,case when ISNULL(TSPL_LOCATION_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_LOCATION_MASTER.Phone1 end +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_LOCATION_MASTER.Phone2 Else'' End as  Loc_Phn,TSPL_VENDOR_MASTER.Vendor_Name ,TSPL_VENDOR_MASTER.add1 +case when len(TSPL_VENDOR_MASTER.add2)>0 then ', '+TSPL_VENDOR_MASTER.add2 else '' end +case when LEN(isnull(TSPL_VENDOR_MASTER.Add3,''))>0 then ', '+isnull(TSPL_VENDOR_MASTER.Add3,'') else ' ' end   as Ven_Add,TSPL_VENDOR_MASTER.Tin_No as Ven_TINNo ,TSPL_VENDOR_MASTER.Email as Ven_Email ,case when ISNULL(TSPL_VENDOR_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_VENDOR_MASTER.Phone1 end +  Case When   ISNULL(TSPL_VENDOR_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_VENDOR_MASTER.Phone2 Else'' End as Ven_Phn,BulkMilkSrn.SRN_NO ,convert(varchar,BulkMilkSrn.SRN_Date,103) as SRN_Date ,BulkMilkSrn.Tanker_No  , case when isnull(BulkMilkSrn.SRN_NO,'')='' then BulkMilkSrn.SRN_NO else Tspl_Gate_Entry_Details.Gate_Entry_No end as Gate_Entry_No,isnull(BulkMilkSRN.Gate_Entry_No,'') as BUlk_Gate_Entry_No,convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,103) as Bulk_Gate_Entry_Date,case when isnull(convert(varchar,BulkMilkSrn.SRN_Date,103),'')='' then convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,103) else convert(varchar,BulkMilkSrn.SRN_Date,103) end as Date_And_Time,"
                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "SPMMD") = CompairStringResult.Equal Then
                    strQuery += "  tspl_Bulk_milk_purchase_Invoice_Detail.fat_per As FAT ,tspl_Bulk_milk_purchase_Invoice_Detail.snf_Per AS SNF,"
                Else
                    If RunBulkProcOnAdjustFATCLR = 0 Then
                        strQuery += " case when isnull(t_FAT.Param_Field_Value,'')='' then tspl_Bulk_milk_purchase_Invoice_Detail.fat_per else t_FAT.Param_Field_Value end As FAT ,case when isnull(t_SNF .Param_Field_Value,'')='' then tspl_Bulk_milk_purchase_Invoice_Detail.snf_Per else t_SNF .Param_Field_Value end As SNF ,"
                    Else
                        strQuery += "  tspl_Bulk_milk_purchase_Invoice_Detail.fat_per As FAT ,tspl_Bulk_milk_purchase_Invoice_Detail.snf_Per AS SNF,"
                    End If
                End If
                strQuery += " case when isnull(TSPL_Weighment_Detail.Net_Weight,'')='' then tspl_Bulk_milk_purchase_Invoice_Detail.Invoice_Qty else TSPL_Weighment_Detail.Net_Weight end as Milk_qty,tspl_Bulk_milk_purchase_Invoice_Detail.Invoice_Qty,'For  FAT' +convert(varchar,TSPL_Bulk_Price_MASTER.Fat_Percentage) + ' % &  SNF' " &
                                     " +  convert(varchar,TSPL_Bulk_Price_MASTER.Snf_Percentage)+ ' %' as 'MilkRate' , 'For ' +convert(varchar,TSPL_Bulk_Price_MASTER.Fat_Weightage ) + ' & ' +  convert(varchar,TSPL_Bulk_Price_MASTER.Snf_Weightage ) as 'Weightage',tspl_Bulk_milk_purchase_Invoice_Detail.NetRate,tspl_Bulk_milk_purchase_Invoice_Detail.Actual_Amount,tspl_Bulk_milk_purchase_Invoice_head.RoundOffAmount ,(BulkMilkSrn.Incentive+BulkMilkSrn.Deduction-BulkMilkSrn.SpecialDeduction)as Ded_Inc,case when isnull(BulkMilkSrn.BasicRate,'')='' then tspl_Bulk_milk_purchase_Invoice_Detail.NetRate else BulkMilkSrn.BasicRate end as BasicRate,tspl_Bulk_milk_purchase_Invoice_Detail.Fat_KG,tspl_Bulk_milk_purchase_Invoice_Detail.SNF_KG,isnull(BulkMilkSrn.Net_Weight_Calculate,0) as Net_Weight_Calculate " &
                                     ",TSPL_PI_REMITTANCE.TDS_Per,isnull(TSPL_PI_REMITTANCE.Actual_TDS,0) as Actual_TDS,TSPL_PI_REMITTANCE.Surcharge_Per,isnull(TSPL_PI_REMITTANCE.Actual_Surcharge,0) as Actual_Surcharge,TSPL_PI_REMITTANCE.Edu_Cess_Per,isnull(TSPL_PI_REMITTANCE.Actual_Edu_Cess,0) as Actual_Edu_Cess,TSPL_PI_REMITTANCE.Sec_Educess_Per,isnull(TSPL_PI_REMITTANCE.Actual_Sec_Educess,0) as Actual_Sec_Educess" &
                                     " from tspl_Bulk_milk_purchase_Invoice_Detail  left outer join tspl_Bulk_milk_purchase_Invoice_head on tspl_Bulk_milk_purchase_Invoice_head.DOC_NO =tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code =tspl_Bulk_milk_purchase_Invoice_Detail.Item_Code " &
                " left outer join TSPL_TAX_MASTER as dtax1 on dtax1.tax_code =tspl_Bulk_milk_purchase_Invoice_Detail .tax1 " &
                " left outer join tspl_tax_master as dtax2 on dtax2.tax_code = tspl_Bulk_milk_purchase_Invoice_Detail.tax2 " &
                " left outer join tspl_tax_master as dtax3 on dtax3.Tax_Code=tspl_Bulk_milk_purchase_Invoice_Detail .TAX3 " &
                " left outer join TSPL_TAX_MASTER as dtax4 on dtax4.Tax_Code= tspl_Bulk_milk_purchase_Invoice_Detail .tax4 " &
                " left outer join TSPL_TAX_MASTER as dtax5 on dtax5.Tax_Code=tspl_Bulk_milk_purchase_Invoice_Detail .tax5 " &
                " left outer join TSPL_TAX_MASTER as dtax6 on dtax6.Tax_Code =tspl_Bulk_milk_purchase_Invoice_Detail .TAX6 " &
                " left outer join TSPL_TAX_MASTER as dtax7 on dtax7.Tax_Code =tspl_Bulk_milk_purchase_Invoice_Detail .TAX7 " &
                " left outer join TSPL_TAX_MASTER as dtax8 on dtax8.Tax_Code =tspl_Bulk_milk_purchase_Invoice_Detail .TAX8 " &
                " left outer join TSPL_TAX_MASTER as dtax9 on dtax9.Tax_Code =tspl_Bulk_milk_purchase_Invoice_Detail .TAX9 " &
                " left outer join TSPL_TAX_MASTER as dtax10 on dtax10.Tax_Code =tspl_Bulk_milk_purchase_Invoice_Detail .TAX10 " &
                " left outer join TSPL_COMPANY_MASTER  on TSPL_COMPANY_MASTER.Comp_Code =tspl_Bulk_milk_purchase_Invoice_head.Comp_Code   left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER .Location_Code =tspl_Bulk_milk_purchase_Invoice_head.loc_code  left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER .Vendor_Code =tspl_Bulk_milk_purchase_Invoice_head.vendor_code  left join TSPL_VENDOR_BANK_MASTER on TSPL_VENDOR_MASTER.BANK_CODE=TSPL_VENDOR_BANK_MASTER.BANK_CODE left outer join BulkMilkSrn  on BulkMilkSrn.SRN_NO =tspl_Bulk_milk_purchase_Invoice_Detail.SRN_NO   left outer join Tspl_Gate_Entry_Details on Tspl_Gate_Entry_Details.Gate_Entry_No =BulkMilkSrn.Gate_Entry_No  Left Outer Join (Select TSPL_QC_Parameter_Detail.* From BulkMilkSrn Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No  =        BulkMilkSrn.QC_No  And        TSPL_QC_Parameter_Detail.Param_Type = 'FAT') t_FAT      On t_FAT.QC_No   = BulkMilkSrn.QC_No  Left Outer Join (Select TSPL_QC_Parameter_Detail.* From BulkMilkSrn Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No  =        BulkMilkSrn.QC_No  And        TSPL_QC_Parameter_Detail.Param_Type = 'SNF') t_SNF      On t_SNF .QC_No   = BulkMilkSrn.QC_No  left outer join TSPL_Bulk_Price_MASTER on TSPL_Bulk_Price_MASTER.Price_Code=tspl_Bulk_milk_purchase_Invoice_Detail.Price_code  left outer join TSPL_Weighment_Detail on TSPL_Weighment_Detail.Weighment_No =BulkMilkSrn.Weighment_No  left outer join TSPL_STATE_MASTER on TSPL_VENDOR_MASTER.State_Code = TSPL_STATE_MASTER.STATE_CODE left outer join tspl_state_master as tspl_state_master_for_location_state on  tspl_state_master_for_location_state.state_code=tspl_location_master.state left outer join TSPL_PI_REMITTANCE on TSPL_PI_REMITTANCE.Document_No=tspl_bulk_milk_purchase_invoice_head.DOC_NO where tspl_bulk_milk_purchase_invoice_head.DOC_NO in ('" & fndDocNo.Value & "') order by Vendor_name,Date_And_Time"
                ''" where  tspl_bulk_milk_purchase_invoice_head.doc_no='" & fndDocNo.Value & "' order by Date_And_Time"



            Else
                Dim frm As New RptBulkMilkMultiplePurchaseInvoice
                strQuery = frm.GetQuery("", "", fndDocNo.Value)
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQuery)
            Dim frmCRV As New frmCrystalReportViewer()
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "BHBA") = CompairStringResult.Equal Then
                frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "rptBulkMilkPurchaseInvoiceForBHBA", "Purchase Invoice")
            Else
                frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "rptBulkMilkPurchaseInvoice", "Purchase Invoice", clsCommon.myCDate(dtpDocDate.Value))
            End If

            frmCRV = Nothing

        Else
            clsCommon.MyMessageBoxShow(Me, "Please select an invoice to print", Me.Text)
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
                For ii As Integer = 0 To gv1.Rows.Count - 1
                    strSRN = clsCommon.myCstr(gv1.Rows(ii).Cells(colSRNNo).Value)
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
                    clsCommon.MyMessageBoxShow(Me, "Error In Document No Genertion", Me.Text)
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
            obj.Tax_Group = txtTaxGroup.Value
            If rbtnTaxCalAutomatic.IsChecked Then
                obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic
            ElseIf rbtnTaxCalManual.IsChecked Then
                obj.Tax_Calculation_Type = EnumTaxCalucationType.Mannual
            End If
            If (gv2.Rows.Count > 0) Then
                obj.TAX1 = clsCommon.myCstr(gv2.Rows(0).Cells(colTTaxAutCode).Value)
                obj.TAX1_Rate = clsCommon.myCdbl(gv2.Rows(0).Cells(colTTaxRate).Value)
                obj.TAX1_Base_Amt = clsCommon.myCdbl(gv2.Rows(0).Cells(colTBaseAmt).Value)
                obj.TAX1_Amt = clsCommon.myCdbl(gv2.Rows(0).Cells(colTTaxAmt).Value)
            End If
            If (gv2.Rows.Count > 1) Then
                obj.TAX2 = clsCommon.myCstr(gv2.Rows(1).Cells(colTTaxAutCode).Value)
                obj.TAX2_Rate = clsCommon.myCdbl(gv2.Rows(1).Cells(colTTaxRate).Value)
                obj.TAX2_Base_Amt = clsCommon.myCdbl(gv2.Rows(1).Cells(colTBaseAmt).Value)
                obj.TAX2_Amt = clsCommon.myCdbl(gv2.Rows(1).Cells(colTTaxAmt).Value)
            End If
            If (gv2.Rows.Count > 2) Then
                obj.TAX3 = clsCommon.myCstr(gv2.Rows(2).Cells(colTTaxAutCode).Value)
                obj.TAX3_Rate = clsCommon.myCdbl(gv2.Rows(2).Cells(colTTaxRate).Value)
                obj.TAX3_Base_Amt = clsCommon.myCdbl(gv2.Rows(2).Cells(colTBaseAmt).Value)
                obj.TAX3_Amt = clsCommon.myCdbl(gv2.Rows(2).Cells(colTTaxAmt).Value)
            End If
            If (gv2.Rows.Count > 3) Then
                obj.TAX4 = clsCommon.myCstr(gv2.Rows(3).Cells(colTTaxAutCode).Value)
                obj.TAX4_Rate = clsCommon.myCdbl(gv2.Rows(3).Cells(colTTaxRate).Value)
                obj.TAX4_Base_Amt = clsCommon.myCdbl(gv2.Rows(3).Cells(colTBaseAmt).Value)
                obj.TAX4_Amt = clsCommon.myCdbl(gv2.Rows(3).Cells(colTTaxAmt).Value)
            End If
            If (gv2.Rows.Count > 4) Then
                obj.TAX5 = clsCommon.myCstr(gv2.Rows(4).Cells(colTTaxAutCode).Value)
                obj.TAX5_Rate = clsCommon.myCdbl(gv2.Rows(4).Cells(colTTaxRate).Value)
                obj.TAX5_Base_Amt = clsCommon.myCdbl(gv2.Rows(4).Cells(colTBaseAmt).Value)
                obj.TAX5_Amt = clsCommon.myCdbl(gv2.Rows(4).Cells(colTTaxAmt).Value)
            End If
            If (gv2.Rows.Count > 5) Then
                obj.TAX6 = clsCommon.myCstr(gv2.Rows(5).Cells(colTTaxAutCode).Value)
                obj.TAX6_Rate = clsCommon.myCdbl(gv2.Rows(5).Cells(colTTaxRate).Value)
                obj.TAX6_Base_Amt = clsCommon.myCdbl(gv2.Rows(5).Cells(colTBaseAmt).Value)
                obj.TAX6_Amt = clsCommon.myCdbl(gv2.Rows(5).Cells(colTTaxAmt).Value)
            End If
            If (gv2.Rows.Count > 6) Then
                obj.TAX7 = clsCommon.myCstr(gv2.Rows(6).Cells(colTTaxAutCode).Value)
                obj.TAX7_Rate = clsCommon.myCdbl(gv2.Rows(6).Cells(colTTaxRate).Value)
                obj.TAX7_Base_Amt = clsCommon.myCdbl(gv2.Rows(6).Cells(colTBaseAmt).Value)
                obj.TAX7_Amt = clsCommon.myCdbl(gv2.Rows(6).Cells(colTTaxAmt).Value)
            End If
            If (gv2.Rows.Count > 7) Then
                obj.TAX8 = clsCommon.myCstr(gv2.Rows(7).Cells(colTTaxAutCode).Value)
                obj.TAX8_Rate = clsCommon.myCdbl(gv2.Rows(7).Cells(colTTaxRate).Value)
                obj.TAX8_Base_Amt = clsCommon.myCdbl(gv2.Rows(7).Cells(colTBaseAmt).Value)
                obj.TAX8_Amt = clsCommon.myCdbl(gv2.Rows(7).Cells(colTTaxAmt).Value)
            End If
            If (gv2.Rows.Count > 8) Then
                obj.TAX9 = clsCommon.myCstr(gv2.Rows(8).Cells(colTTaxAutCode).Value)
                obj.TAX9_Rate = clsCommon.myCdbl(gv2.Rows(8).Cells(colTTaxRate).Value)
                obj.TAX9_Base_Amt = clsCommon.myCdbl(gv2.Rows(8).Cells(colTBaseAmt).Value)
                obj.TAX9_Amt = clsCommon.myCdbl(gv2.Rows(8).Cells(colTTaxAmt).Value)
            End If
            If (gv2.Rows.Count > 9) Then
                obj.TAX10 = clsCommon.myCstr(gv2.Rows(9).Cells(colTTaxAutCode).Value)
                obj.TAX10_Rate = clsCommon.myCdbl(gv2.Rows(9).Cells(colTTaxRate).Value)
                obj.TAX10_Base_Amt = clsCommon.myCdbl(gv2.Rows(9).Cells(colTBaseAmt).Value)
                obj.TAX10_Amt = clsCommon.myCdbl(gv2.Rows(9).Cells(colTTaxAmt).Value)
            End If
            obj.Total_Tax_Amt = clsCommon.myCdbl(txtTaxAmt.Text)
            obj.Total_Taxable_Amount = clsCommon.myCdbl(txtTaxableAmt.Text)

            obj.ActualTCSBaseAmount = clsCommon.myCdbl(lblActualTCSTaxBaseAmt.Text)
            obj.ChangedTCSBaseAmount = clsCommon.myCdbl(txttcstaxbaseamount.Value)

            obj.objPIRemittance = clsPIRemittance.Convert(objRemittance, dblPreviousTDSAmt)
            'If objRemittance IsNot Nothing Then
            '    obj.objPIRemittance = New clsPIRemittance()
            '    obj.objPIRemittance.Vendor_Code = objRemittance.Vendor_Code
            '    obj.objPIRemittance.Vendor_Name = objRemittance.Vendor_Name
            '    obj.objPIRemittance.Document_No = objRemittance.Document_No
            '    obj.objPIRemittance.Document_Date = objRemittance.Document_Date
            '    obj.objPIRemittance.Document_Type = objRemittance.Document_Type
            '    obj.objPIRemittance.Document_Amount = objRemittance.Document_Amount
            '    obj.objPIRemittance.Service_Type = objRemittance.Service_Type
            '    obj.objPIRemittance.Actual_TDS_Base = objRemittance.Actual_TDS_Base
            '    obj.objPIRemittance.Calculated_TDS_Base = objRemittance.Calculated_TDS_Base
            '    obj.objPIRemittance.Actual_TDS = objRemittance.Actual_TDS
            '    obj.objPIRemittance.Calculated_TDS = objRemittance.Calculated_TDS
            '    obj.objPIRemittance.Actual_Surcharge = objRemittance.Actual_Surcharge
            '    obj.objPIRemittance.Calculated_Surcharge = objRemittance.Calculated_Surcharge
            '    obj.objPIRemittance.Actual_Edu_Cess = objRemittance.Actual_Edu_Cess
            '    obj.objPIRemittance.Calculated_Edu_Cess = objRemittance.Calculated_Edu_Cess
            '    obj.objPIRemittance.Actual_Sec_Educess = objRemittance.Actual_Sec_Educess
            '    obj.objPIRemittance.Calculated_Sec_Educess = objRemittance.Calculated_Sec_Educess
            '    obj.objPIRemittance.Actual_Total_TDS = objRemittance.Actual_Total_TDS
            '    obj.objPIRemittance.Calculated_Total_TDS = objRemittance.Calculated_Total_TDS
            '    obj.objPIRemittance.Fiscal_Year = objRemittance.Fiscal_Year
            '    obj.objPIRemittance.Quarter = objRemittance.Quarter
            '    obj.objPIRemittance.Section_Code = objRemittance.Section_Code
            '    obj.objPIRemittance.Section_Description = objRemittance.Section_Description
            '    obj.objPIRemittance.Branch_Code = objRemittance.Branch_Code
            '    obj.objPIRemittance.Deduction_Code = objRemittance.Deduction_Code
            '    obj.objPIRemittance.TDS_Per = objRemittance.TDS_Per
            '    obj.objPIRemittance.Surcharge_Per = objRemittance.Surcharge_Per
            '    obj.objPIRemittance.Edu_Cess_Per = objRemittance.Edu_Cess_Per
            '    obj.objPIRemittance.Sec_Educess_Per = objRemittance.Sec_Educess_Per
            '    obj.objPIRemittance.Select_By = objRemittance.Select_By
            '    obj.objPIRemittance.IsTDSOverride = objRemittance.IsTDSOverride
            '    obj.objPIRemittance.IsApplyTDS = objRemittance.IsApplyTDS
            '    obj.objPIRemittance.Previous_TDS_Amt = dblPreviousTDSAmt
            'End If

            Dim i As Integer = 0
            Dim objDetail As New clsMilkPurchaseInvoiceDetail
            obj.arrDetail = New List(Of clsMilkPurchaseInvoiceDetail)
            For i = 0 To gv1.Rows.Count - 1
                objDetail = New clsMilkPurchaseInvoiceDetail
                objDetail.DOC_NO = clsCommon.myCstr(obj.DOC_NO)
                objDetail.SL_NO = clsCommon.myCstr(gv1.Rows(i).Cells(colSlNo).Value)
                objDetail.SRN_NO = clsCommon.myCstr(gv1.Rows(i).Cells(colSRNNo).Value)
                objDetail.SRN_Date = clsCommon.GetPrintDate(gv1.Rows(i).Cells(colSRNDATe).Value, "dd/MMM/yyyy hh:mm:ss tt")
                objDetail.Item_Code = clsCommon.myCstr(gv1.Rows(i).Cells(colItemCode).Value)
                objDetail.Item_Desc = clsCommon.myCstr(gv1.Rows(i).Cells(colItemDesc).Value)
                objDetail.UOM = clsCommon.myCstr(gv1.Rows(i).Cells(colUOM).Value)
                objDetail.Gross_Weight = clsCommon.myCdbl(gv1.Rows(i).Cells(colGrossWeight).Value)
                objDetail.Tare_Weight = clsCommon.myCdbl(gv1.Rows(i).Cells(colTareWeight).Value)
                objDetail.Net_Weight = clsCommon.myCdbl(gv1.Rows(i).Cells(colNetWeight).Value)
                objDetail.Invoice_Qty = clsCommon.myCdbl(gv1.Rows(i).Cells(colQty).Value)
                objDetail.fat_per = clsCommon.myCdbl(gv1.Rows(i).Cells(colFat).Value)
                objDetail.fat_KG = clsCommon.myCdbl(gv1.Rows(i).Cells(colFatKG).Value)
                objDetail.fat_Rate = clsCommon.myCdbl(gv1.Rows(i).Cells(colFatRate).Value)
                objDetail.snf_Per = clsCommon.myCdbl(gv1.Rows(i).Cells(colSNF).Value)
                objDetail.SNF_KG = clsCommon.myCdbl(gv1.Rows(i).Cells(colSNFKG).Value)
                objDetail.SNF_Rate = clsCommon.myCdbl(gv1.Rows(i).Cells(colSNFRate).Value)
                objDetail.Amount = clsCommon.myCdbl(gv1.Rows(i).Cells(colAmt).Value)
                objDetail.Deduction = clsCommon.myCdbl(gv1.Rows(i).Cells(colDeduc).Value)
                objDetail.Incentive = clsCommon.myCdbl(gv1.Rows(i).Cells(colIncen).Value)
                objDetail.Special_Deduction = clsCommon.myCdbl(gv1.Rows(i).Cells(colSpecialDeduc).Value)
                objDetail.Actual_Amount = clsCommon.myCdbl(gv1.Rows(i).Cells(colActAmt).Value)
                objDetail.price_code = clsCommon.myCstr(gv1.Rows(i).Cells(colPriceCode).Value)
                objDetail.NetRate = clsCommon.myCdbl(gv1.Rows(i).Cells(colNetRate).Value)
                objDetail.Xtra_Rate = clsCommon.myCdbl(gv1.Rows(i).Cells(colXtraRate).Value)
                objDetail.CHAMBER_DESC = clsCommon.myCstr(gv1.Rows(i).Cells(colChamberDesc).Value)




                objDetail.TAX1 = clsCommon.myCstr(gv1.Rows(i).Cells(colTax1).Value)
                objDetail.TAX1_Base_Amt = clsCommon.myCdbl(gv1.Rows(i).Cells(colTaxBaseAmt1).Value)
                objDetail.TAX1_Rate = clsCommon.myCdbl(gv1.Rows(i).Cells(colTaxRate1).Value)
                objDetail.TAX1_Amt = clsCommon.myCdbl(gv1.Rows(i).Cells(colTaxAmt1).Value)
                objDetail.TAX2 = clsCommon.myCstr(gv1.Rows(i).Cells(colTax2).Value)
                objDetail.TAX2_Base_Amt = clsCommon.myCdbl(gv1.Rows(i).Cells(colTaxBaseAmt2).Value)
                objDetail.TAX2_Rate = clsCommon.myCdbl(gv1.Rows(i).Cells(colTaxRate2).Value)
                objDetail.TAX2_Amt = clsCommon.myCdbl(gv1.Rows(i).Cells(colTaxAmt2).Value)
                objDetail.TAX3 = clsCommon.myCstr(gv1.Rows(i).Cells(colTax3).Value)
                objDetail.TAX3_Base_Amt = clsCommon.myCdbl(gv1.Rows(i).Cells(colTaxBaseAmt3).Value)
                objDetail.TAX3_Rate = clsCommon.myCdbl(gv1.Rows(i).Cells(colTaxRate3).Value)
                objDetail.TAX3_Amt = clsCommon.myCdbl(gv1.Rows(i).Cells(colTaxAmt3).Value)
                objDetail.TAX4 = clsCommon.myCstr(gv1.Rows(i).Cells(colTax4).Value)
                objDetail.TAX4_Base_Amt = clsCommon.myCdbl(gv1.Rows(i).Cells(colTaxBaseAmt4).Value)
                objDetail.TAX4_Rate = clsCommon.myCdbl(gv1.Rows(i).Cells(colTaxRate4).Value)
                objDetail.TAX4_Amt = clsCommon.myCdbl(gv1.Rows(i).Cells(colTaxAmt4).Value)
                objDetail.TAX5 = clsCommon.myCstr(gv1.Rows(i).Cells(colTax5).Value)
                objDetail.TAX5_Base_Amt = clsCommon.myCdbl(gv1.Rows(i).Cells(colTaxBaseAmt5).Value)
                objDetail.TAX5_Rate = clsCommon.myCdbl(gv1.Rows(i).Cells(colTaxRate5).Value)
                objDetail.TAX5_Amt = clsCommon.myCdbl(gv1.Rows(i).Cells(colTaxAmt5).Value)
                objDetail.TAX6 = clsCommon.myCstr(gv1.Rows(i).Cells(colTax6).Value)
                objDetail.TAX6_Base_Amt = clsCommon.myCdbl(gv1.Rows(i).Cells(colTaxBaseAmt6).Value)
                objDetail.TAX6_Rate = clsCommon.myCdbl(gv1.Rows(i).Cells(colTaxRate6).Value)
                objDetail.TAX6_Amt = clsCommon.myCdbl(gv1.Rows(i).Cells(colTaxAmt6).Value)
                objDetail.TAX7 = clsCommon.myCstr(gv1.Rows(i).Cells(colTax7).Value)
                objDetail.TAX7_Base_Amt = clsCommon.myCdbl(gv1.Rows(i).Cells(colTaxBaseAmt7).Value)
                objDetail.TAX7_Rate = clsCommon.myCdbl(gv1.Rows(i).Cells(colTaxRate7).Value)
                objDetail.TAX7_Amt = clsCommon.myCdbl(gv1.Rows(i).Cells(colTaxAmt7).Value)
                objDetail.TAX8 = clsCommon.myCstr(gv1.Rows(i).Cells(colTax8).Value)
                objDetail.TAX8_Base_Amt = clsCommon.myCdbl(gv1.Rows(i).Cells(colTaxBaseAmt8).Value)
                objDetail.TAX8_Rate = clsCommon.myCdbl(gv1.Rows(i).Cells(colTaxRate8).Value)
                objDetail.TAX8_Amt = clsCommon.myCdbl(gv1.Rows(i).Cells(colTaxAmt8).Value)
                objDetail.TAX9 = clsCommon.myCstr(gv1.Rows(i).Cells(colTax9).Value)
                objDetail.TAX9_Base_Amt = clsCommon.myCdbl(gv1.Rows(i).Cells(colTaxBaseAmt9).Value)
                objDetail.TAX9_Rate = clsCommon.myCdbl(gv1.Rows(i).Cells(colTaxRate9).Value)
                objDetail.TAX9_Amt = clsCommon.myCdbl(gv1.Rows(i).Cells(colTaxAmt9).Value)
                objDetail.TAX10 = clsCommon.myCstr(gv1.Rows(i).Cells(colTax10).Value)
                objDetail.TAX10_Base_Amt = clsCommon.myCdbl(gv1.Rows(i).Cells(colTaxBaseAmt10).Value)
                objDetail.TAX10_Rate = clsCommon.myCdbl(gv1.Rows(i).Cells(colTaxRate10).Value)
                objDetail.TAX10_Amt = clsCommon.myCdbl(gv1.Rows(i).Cells(colTaxAmt10).Value)
                objDetail.Total_Tax_Amt = clsCommon.myCdbl(gv1.Rows(i).Cells(colTotTaxAmt).Value)
                objDetail.Transport_Charges = clsCommon.myCdbl(gv1.Rows(i).Cells(colTranspoterCharge).Value)
                objDetail.Item_Net_Amt = clsCommon.myCdbl(gv1.Rows(i).Cells(colItemNetAmt).Value)

                obj.arrDetail.Add(objDetail)
            Next
            If clsMilkPurchaseInvoiceHead.saveData(obj, trans) Then
                trans.Commit()
                If Not isPost Then
                    If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                        clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    Else
                        clsCommon.MyMessageBoxShow(Me, "Data Updated Successfully", Me.Text)
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
            clsCommon.MyMessageBoxShow(Me, "Data Not Saved ", Me.Text)
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
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
            chkHighClassVendor.Checked = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select isnull (isHighClass,0) from TSPL_VENDOR_MASTER  where Vendor_Code = '" + fndVendor.Value + "' "))
            fndLocation.Value = obj.Loc_Code
            lblLocationName.Text = clsLocation.GetName(obj.Loc_Code, Nothing)
            ''richa agarwal 25 Apr,2019
            If TankerFromMaster = 1 Then
                txtMonth.Value = obj.SRN_From_Date
            End If
            ''--------------
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

            txtTaxGroup.Value = obj.Tax_Group
            If obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic Then
                rbtnTaxCalAutomatic.IsChecked = True
                gv2.Columns(colTTaxRate).IsVisible = False
            ElseIf obj.Tax_Calculation_Type = EnumTaxCalucationType.Mannual Then
                rbtnTaxCalManual.IsChecked = True
                gv2.Columns(colTTaxRate).IsVisible = True
            End If
            Dim objTaxGrpMaster As New clsTaxGroupMaster()
            objTaxGrpMaster = objTaxGrpMaster.GetDataForPurchase(obj.Tax_Group)
            If (objTaxGrpMaster IsNot Nothing) Then
                lblTaxGrpName.Text = objTaxGrpMaster.Tax_Group_Desc
            End If
            If (clsCommon.myLen(obj.TAX1) > 0) Then
                gv2.Rows.AddNew()
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX1
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX1_Rate
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.TAX1_Base_Amt
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX1_Amt
                If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                    For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                        If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX1) = CompairStringResult.Equal) Then
                            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                            ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                            ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                            ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                            Exit For
                        End If
                    Next
                End If
                If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Is_TCS  from tspl_tax_master where tax_code ='" & clsCommon.myCstr(obj.TAX1) & "' ")), "Y") = CompairStringResult.Equal Then
                    txtTCSTaxRate.Value = clsCommon.myCdbl(gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value)
                End If
            End If
            If (clsCommon.myLen(obj.TAX2) > 0) Then
                gv2.Rows.AddNew()
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX2
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX2_Rate
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.TAX2_Base_Amt
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX2_Amt
                If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                    For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                        If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX2) = CompairStringResult.Equal) Then
                            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                            ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                            ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                            ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                            Exit For
                        End If
                    Next
                End If
                If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Is_TCS  from tspl_tax_master where tax_code ='" & clsCommon.myCstr(obj.TAX2) & "' ")), "Y") = CompairStringResult.Equal Then
                    txtTCSTaxRate.Value = clsCommon.myCdbl(gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value)
                End If
            End If
            If (clsCommon.myLen(obj.TAX3) > 0) Then
                gv2.Rows.AddNew()
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX3
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX3_Rate
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.TAX3_Base_Amt
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX3_Amt
                If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                    For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                        If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX3) = CompairStringResult.Equal) Then
                            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                            ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                            ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                            ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                            Exit For
                        End If
                    Next
                End If
                If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Is_TCS  from tspl_tax_master where tax_code ='" & clsCommon.myCstr(obj.TAX3) & "' ")), "Y") = CompairStringResult.Equal Then
                    txtTCSTaxRate.Value = clsCommon.myCdbl(gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value)
                End If
            End If
            If (clsCommon.myLen(obj.TAX4) > 0) Then
                gv2.Rows.AddNew()
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX4
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX4_Rate
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.TAX4_Base_Amt
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX4_Amt
                If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                    For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                        If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX4) = CompairStringResult.Equal) Then
                            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                            ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                            ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                            ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                            Exit For
                        End If
                    Next
                End If
                If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Is_TCS  from tspl_tax_master where tax_code ='" & clsCommon.myCstr(obj.TAX4) & "' ")), "Y") = CompairStringResult.Equal Then
                    txtTCSTaxRate.Value = clsCommon.myCdbl(gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value)
                End If
            End If
            If (clsCommon.myLen(obj.TAX5) > 0) Then
                gv2.Rows.AddNew()
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX5
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX5_Rate
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.TAX5_Base_Amt
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX5_Amt
                If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                    For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                        If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX5) = CompairStringResult.Equal) Then
                            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                            ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                            ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                            ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                            Exit For
                        End If
                    Next
                End If
            End If
            If (clsCommon.myLen(obj.TAX6) > 0) Then
                gv2.Rows.AddNew()
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX6
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX6_Rate
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.TAX6_Base_Amt
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX6_Amt
                If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                    For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                        If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX6) = CompairStringResult.Equal) Then
                            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                            ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                            ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                            ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                            Exit For
                        End If
                    Next
                End If
            End If
            If (clsCommon.myLen(obj.TAX7) > 0) Then
                gv2.Rows.AddNew()
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX7
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX7_Rate
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.TAX7_Base_Amt
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX7_Amt
                If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                    For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                        If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX7) = CompairStringResult.Equal) Then
                            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                            ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                            ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                            ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                            Exit For
                        End If
                    Next
                End If
            End If
            If (clsCommon.myLen(obj.TAX8) > 0) Then
                gv2.Rows.AddNew()
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX8
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX8_Rate
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.TAX8_Base_Amt
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX8_Amt
                If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                    For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                        If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX8) = CompairStringResult.Equal) Then
                            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                            ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                            ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                            ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                            Exit For
                        End If
                    Next
                End If
            End If
            If (clsCommon.myLen(obj.TAX9) > 0) Then
                gv2.Rows.AddNew()
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX9
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX9_Rate
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.TAX9_Base_Amt
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX9_Amt
                If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                    For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                        If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX9) = CompairStringResult.Equal) Then
                            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                            ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                            ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                            ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                            Exit For
                        End If
                    Next
                End If
            End If
            If (clsCommon.myLen(obj.TAX10) > 0) Then
                gv2.Rows.AddNew()
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX10
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX10_Rate
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.TAX10_Base_Amt
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX10_Amt
                If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                    For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                        If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX10) = CompairStringResult.Equal) Then
                            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                            ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                            ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                            ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                            Exit For
                        End If
                    Next
                End If
            End If
            txtTaxableAmt.Text = clsCommon.myFormat(obj.Total_Taxable_Amount)
            txtTaxAmt.Text = clsCommon.myFormat(obj.Total_Tax_Amt)


            txttcstaxbaseamount.Value = clsCommon.myCdbl(obj.ChangedTCSBaseAmount)
            lblActualTCSTaxBaseAmt.Text = clsCommon.myFormat(obj.ActualTCSBaseAmount)

            objRemittance = Nothing
            If obj.objPIRemittance IsNot Nothing Then
                'If objRemittance IsNot Nothing Then
                objRemittance = New clsRemittance()
                objRemittance.Vendor_Code = obj.objPIRemittance.Vendor_Code
                objRemittance.Vendor_Name = obj.objPIRemittance.Vendor_Name
                objRemittance.Document_No = obj.objPIRemittance.Document_No
                objRemittance.Document_Date = obj.objPIRemittance.Document_Date
                objRemittance.Document_Type = obj.objPIRemittance.Document_Type
                objRemittance.Document_Amount = obj.objPIRemittance.Document_Amount
                objRemittance.Service_Type = obj.objPIRemittance.Service_Type
                objRemittance.Actual_TDS_Base = obj.objPIRemittance.Actual_TDS_Base
                objRemittance.Calculated_TDS_Base = obj.objPIRemittance.Calculated_TDS_Base
                objRemittance.Actual_TDS = obj.objPIRemittance.Actual_TDS
                objRemittance.Calculated_TDS = obj.objPIRemittance.Calculated_TDS
                objRemittance.Actual_Surcharge = obj.objPIRemittance.Actual_Surcharge
                objRemittance.Calculated_Surcharge = obj.objPIRemittance.Calculated_Surcharge
                objRemittance.Actual_Edu_Cess = obj.objPIRemittance.Actual_Edu_Cess
                objRemittance.Calculated_Edu_Cess = obj.objPIRemittance.Calculated_Edu_Cess
                objRemittance.Actual_Sec_Educess = obj.objPIRemittance.Actual_Sec_Educess
                objRemittance.Calculated_Sec_Educess = obj.objPIRemittance.Calculated_Sec_Educess
                objRemittance.Actual_Total_TDS = obj.objPIRemittance.Actual_Total_TDS
                objRemittance.Calculated_Total_TDS = obj.objPIRemittance.Calculated_Total_TDS
                objRemittance.Fiscal_Year = obj.objPIRemittance.Fiscal_Year
                objRemittance.Quarter = obj.objPIRemittance.Quarter
                objRemittance.Section_Code = obj.objPIRemittance.Section_Code
                objRemittance.Section_Description = obj.objPIRemittance.Section_Description
                objRemittance.Branch_Code = obj.objPIRemittance.Branch_Code
                objRemittance.Deduction_Code = obj.objPIRemittance.Deduction_Code
                objRemittance.TDS_Per = obj.objPIRemittance.TDS_Per
                objRemittance.Surcharge_Per = obj.objPIRemittance.Surcharge_Per
                objRemittance.Edu_Cess_Per = obj.objPIRemittance.Edu_Cess_Per
                objRemittance.Sec_Educess_Per = obj.objPIRemittance.Sec_Educess_Per
                objRemittance.Select_By = obj.objPIRemittance.Select_By
                objRemittance.IsTDSOverride = obj.objPIRemittance.IsTDSOverride
                objRemittance.IsApplyTDS = obj.objPIRemittance.IsApplyTDS
                dblPreviousTDSAmt = obj.objPIRemittance.Previous_TDS_Amt
                btnViewTDSDetails.Enabled = True
            End If
            UpdateTDSAmountValue()
            If obj.arrDetail IsNot Nothing Then
                Dim arr As New List(Of String)
                For i As Integer = 0 To obj.arrDetail.Count - 1
                    gv1.Rows.AddNew()
                    gv1.Rows(i).Cells(colSlNo).Value = obj.arrDetail(i).SL_NO
                    gv1.Rows(i).Cells(colSRNNo).Value = obj.arrDetail(i).SRN_NO
                    If Not arr.Contains(obj.arrDetail(i).SRN_NO) Then
                        arr.Add(obj.arrDetail(i).SRN_NO)
                        SRNs = SRNs & obj.arrDetail(i).SRN_NO
                    End If
                    If i <> obj.arrDetail.Count - 1 Then
                        SRNs = SRNs & ", "
                    End If
                    gv1.Rows(i).Cells(colSRNDATe).Value = obj.arrDetail(i).SRN_Date
                    gv1.Rows(i).Cells(colItemCode).Value = obj.arrDetail(i).Item_Code
                    gv1.Rows(i).Cells(colItemDesc).Value = obj.arrDetail(i).Item_Desc
                    gv1.Rows(i).Cells(colHSN).Value = clsItemMaster.GetItemHSNCode(obj.arrDetail(i).Item_Code, Nothing)
                    gv1.Rows(i).Cells(colUOM).Value = obj.arrDetail(i).UOM
                    gv1.Rows(i).Cells(colGrossWeight).Value = clsCommon.myFormat(obj.arrDetail(i).Gross_Weight)
                    gv1.Rows(i).Cells(colTareWeight).Value = clsCommon.myFormat(obj.arrDetail(i).Tare_Weight)
                    gv1.Rows(i).Cells(colNetWeight).Value = clsCommon.myFormat(obj.arrDetail(i).Net_Weight)
                    gv1.Rows(i).Cells(colQty).Value = clsCommon.myFormat(obj.arrDetail(i).Invoice_Qty)
                    gv1.Rows(i).Cells(colFat).Value = clsCommon.myFormat(obj.arrDetail(i).fat_per)
                    gv1.Rows(i).Cells(colFatKG).Value = clsCommon.myFormat(obj.arrDetail(i).fat_KG, False, True, False, 3, True)
                    gv1.Rows(i).Cells(colFatRate).Value = clsCommon.myFormat(obj.arrDetail(i).fat_Rate)
                    gv1.Rows(i).Cells(colSNF).Value = clsCommon.myFormat(obj.arrDetail(i).snf_Per)
                    gv1.Rows(i).Cells(colSNFKG).Value = clsCommon.myFormat(obj.arrDetail(i).SNF_KG, False, True, False, 3, True)
                    gv1.Rows(i).Cells(colSNFRate).Value = clsCommon.myFormat(obj.arrDetail(i).SNF_Rate)
                    gv1.Rows(i).Cells(colAmt).Value = clsCommon.myFormat(obj.arrDetail(i).Amount)
                    gv1.Rows(i).Cells(colDeduc).Value = clsCommon.myFormat(obj.arrDetail(i).Deduction)
                    gv1.Rows(i).Cells(colIncen).Value = clsCommon.myFormat(obj.arrDetail(i).Incentive)
                    gv1.Rows(i).Cells(colSpecialDeduc).Value = clsCommon.myFormat(obj.arrDetail(i).Special_Deduction)
                    gv1.Rows(i).Cells(colActAmt).Value = clsCommon.myFormat(obj.arrDetail(i).Actual_Amount)
                    gv1.Rows(i).Cells(colPriceCode).Value = obj.arrDetail(i).price_code
                    gv1.Rows(i).Cells(colNetRate).Value = clsCommon.myFormat(obj.arrDetail(i).NetRate)
                    gv1.Rows(i).Cells(colXtraRate).Value = clsCommon.myFormat(obj.arrDetail(i).Xtra_Rate)
                    gv1.Rows(i).Cells(colTnkrNo).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select tanker_no from TSPL_Bulk_MILK_SRN where SRN_NO='" & obj.arrDetail.Item(i).SRN_NO & "' "))    '' RICHA AGARWAL IIf(chkSRNTrade.Checked, "NA", clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select tanker_no from TSPL_Bulk_MILK_SRN where SRN_NO='" & obj.arrDetail.Item(i).SRN_NO & "' ")))
                    gv1.Rows(i).Cells(colChamberDesc).Value = obj.arrDetail(i).CHAMBER_DESC

                    gv1.Rows(i).Cells(colTax1).Value = obj.arrDetail(i).TAX1
                    gv1.Rows(i).Cells(colTaxBaseAmt1).Value = obj.arrDetail(i).TAX1_Base_Amt
                    gv1.Rows(i).Cells(colTaxRate1).Value = obj.arrDetail(i).TAX1_Rate
                    gv1.Rows(i).Cells(colTaxAmt1).Value = obj.arrDetail(i).TAX1_Amt
                    gv1.Rows(i).Cells(colTax2).Value = obj.arrDetail(i).TAX2
                    gv1.Rows(i).Cells(colTaxBaseAmt2).Value = obj.arrDetail(i).TAX2_Base_Amt
                    gv1.Rows(i).Cells(colTaxRate2).Value = obj.arrDetail(i).TAX2_Rate
                    gv1.Rows(i).Cells(colTaxAmt2).Value = obj.arrDetail(i).TAX2_Amt
                    gv1.Rows(i).Cells(colTax3).Value = obj.arrDetail(i).TAX3
                    gv1.Rows(i).Cells(colTaxBaseAmt3).Value = obj.arrDetail(i).TAX3_Base_Amt
                    gv1.Rows(i).Cells(colTaxRate3).Value = obj.arrDetail(i).TAX3_Rate
                    gv1.Rows(i).Cells(colTaxAmt3).Value = obj.arrDetail(i).TAX3_Amt
                    gv1.Rows(i).Cells(colTax4).Value = obj.arrDetail(i).TAX4
                    gv1.Rows(i).Cells(colTaxBaseAmt4).Value = obj.arrDetail(i).TAX4_Base_Amt
                    gv1.Rows(i).Cells(colTaxRate4).Value = obj.arrDetail(i).TAX4_Rate
                    gv1.Rows(i).Cells(colTaxAmt4).Value = obj.arrDetail(i).TAX4_Amt
                    gv1.Rows(i).Cells(colTax5).Value = obj.arrDetail(i).TAX5
                    gv1.Rows(i).Cells(colTaxBaseAmt5).Value = obj.arrDetail(i).TAX5_Base_Amt
                    gv1.Rows(i).Cells(colTaxRate5).Value = obj.arrDetail(i).TAX5_Rate
                    gv1.Rows(i).Cells(colTaxAmt5).Value = obj.arrDetail(i).TAX5_Amt
                    gv1.Rows(i).Cells(colTax6).Value = obj.arrDetail(i).TAX6
                    gv1.Rows(i).Cells(colTaxBaseAmt6).Value = obj.arrDetail(i).TAX6_Base_Amt
                    gv1.Rows(i).Cells(colTaxRate6).Value = obj.arrDetail(i).TAX6_Rate
                    gv1.Rows(i).Cells(colTaxAmt6).Value = obj.arrDetail(i).TAX6_Amt
                    gv1.Rows(i).Cells(colTax7).Value = obj.arrDetail(i).TAX7
                    gv1.Rows(i).Cells(colTaxBaseAmt7).Value = obj.arrDetail(i).TAX7_Base_Amt
                    gv1.Rows(i).Cells(colTaxRate7).Value = obj.arrDetail(i).TAX7_Rate
                    gv1.Rows(i).Cells(colTaxAmt7).Value = obj.arrDetail(i).TAX7_Amt
                    gv1.Rows(i).Cells(colTax8).Value = obj.arrDetail(i).TAX8
                    gv1.Rows(i).Cells(colTaxBaseAmt8).Value = obj.arrDetail(i).TAX8_Base_Amt
                    gv1.Rows(i).Cells(colTaxRate8).Value = obj.arrDetail(i).TAX8_Rate
                    gv1.Rows(i).Cells(colTaxAmt8).Value = obj.arrDetail(i).TAX8_Amt
                    gv1.Rows(i).Cells(colTax9).Value = obj.arrDetail(i).TAX9
                    gv1.Rows(i).Cells(colTaxBaseAmt9).Value = obj.arrDetail(i).TAX9_Base_Amt
                    gv1.Rows(i).Cells(colTaxRate9).Value = obj.arrDetail(i).TAX9_Rate
                    gv1.Rows(i).Cells(colTaxAmt9).Value = obj.arrDetail(i).TAX9_Amt
                    gv1.Rows(i).Cells(colTax10).Value = obj.arrDetail(i).TAX10
                    gv1.Rows(i).Cells(colTaxBaseAmt10).Value = obj.arrDetail(i).TAX10_Base_Amt
                    gv1.Rows(i).Cells(colTaxRate10).Value = obj.arrDetail(i).TAX10_Rate
                    gv1.Rows(i).Cells(colTaxAmt10).Value = obj.arrDetail(i).TAX10_Amt
                    gv1.Rows(i).Cells(colTotTaxAmt).Value = obj.arrDetail(i).Total_Tax_Amt
                    gv1.Rows(i).Cells(colTranspoterCharge).Value = obj.arrDetail(i).Transport_Charges
                    gv1.Rows(i).Cells(colItemNetAmt).Value = obj.arrDetail(i).Item_Net_Amt

                    Dim dt As DataTable = clsDBFuncationality.GetDataTable("select TSPL_Bulk_MILK_SRN.SRN_NO,coalesce(SUM(tdl.invoice_qty),0) as InvoiceQty, TSPL_Bulk_MILK_SRN.Net_Weight, (TSPL_Bulk_MILK_SRN.Net_Weight-coalesce(SUM(tdl.invoice_qty),0)) as pending_qty from TSPL_Bulk_MILK_SRN left join ( select invoice_qty,SRN_NO  from Tspl_bulk_milk_purchase_invoice_detail left outer join Tspl_bulk_milk_purchase_invoice_head on Tspl_bulk_milk_purchase_invoice_head.DOC_NO=tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO  where Tspl_bulk_milk_purchase_invoice_head.isPosted=1) as  tdl  on TSPL_Bulk_MILK_SRN.SRN_NO=tdl.srn_no  where TSPL_Bulk_MILK_SRN.isPosted=1 and TSPL_Bulk_MILK_SRN.SRN_NO='" & obj.arrDetail(i).SRN_NO & "'  group by TSPL_Bulk_MILK_SRN.SRN_NO ,TSPL_Bulk_MILK_SRN.Net_Weight	")
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        ''richa agarwal do conversion work 2 Aug,2019
                        Dim strPriceCodeUOM As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select  isnull(Total_Solid_Unit_Code,'') from TSPL_Bulk_Price_MASTER where Price_Code='" & obj.arrDetail(i).price_code & "' "))
                        Dim dblNetWeight As Double = clsCommon.myFormat(clsCommon.myCdbl(dt.Rows(0)("Net_Weight")))
                        Dim dblinVoiceQty As Double = clsCommon.myFormat(clsCommon.myCdbl(dt.Rows(0)("InvoiceQty")))
                        Dim dblNetWeightCalculate As Double = 0
                        Dim strSRNUOM As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT UOM FROM TSPL_Bulk_MILK_SRN where SRN_NO ='" & obj.arrDetail.Item(i).SRN_NO & "'"))
                        If clsCommon.myLen(strPriceCodeUOM) > 0 Then
                            If SettCalculateLtrQtyFromKGQtyByCLR _
                           AndAlso clsCommon.CompairString("KG", clsCommon.myCstr(strSRNUOM)) = CompairStringResult.Equal _
                           AndAlso clsCommon.CompairString("LTR", clsCommon.myCstr(strPriceCodeUOM)) = CompairStringResult.Equal Then
                                dblNetWeightCalculate = clsItemMaster.GetQtyInLtrFromKgByCLR(clsCommon.myCdbl(dblNetWeight), clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT isnull(CLR_Per ,0) FROM TSPL_Bulk_MILK_SRN where SRN_NO ='" & obj.arrDetail.Item(i).SRN_NO & "'")))
                            ElseIf SettApplyCalculateWeightInLtr Then
                                dblNetWeightCalculate = clsCommon.myCdbl(dblNetWeight) * clsItemMaster.GetConvertionFactor(clsCommon.myCstr(obj.arrDetail(i).Item_Code), clsCommon.myCstr(obj.arrDetail(i).UOM), Nothing)
                                Dim convFact As Decimal = clsItemMaster.GetConvertionFactor(clsCommon.myCstr(obj.arrDetail(i).Item_Code), clsCommon.myCstr(strPriceCodeUOM), Nothing)
                                If convFact = 0 Then
                                    Throw New Exception("Unit [" + clsCommon.myCstr(strPriceCodeUOM) + "] is not for item [" + clsCommon.myCstr(obj.arrDetail(i).Item_Code) + "]")
                                End If
                                dblNetWeightCalculate = Math.Round(dblNetWeightCalculate / convFact, 2)
                            Else
                                dblNetWeightCalculate = dblNetWeightCalculate
                            End If
                        End If
                        gv1.Rows(i).Cells(colPendingQty).Value = clsCommon.myFormat(dblNetWeightCalculate - dblinVoiceQty)
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
        gv1.Rows.Clear()
        gv1.Columns.Clear()


        Dim repoSLNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSLNo.FormatString = ""
        repoSLNo.HeaderText = "SL.No"
        repoSLNo.Name = colSlNo
        repoSLNo.Width = 60
        repoSLNo.ReadOnly = True
        repoSLNo.BestFit()
        gv1.MasterTemplate.Columns.Add(repoSLNo)

        Dim repoTnkrNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTnkrNo.FormatString = ""
        repoTnkrNo.HeaderText = "Tanker No"
        repoTnkrNo.Name = colTnkrNo
        repoTnkrNo.Width = 100
        repoTnkrNo.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTnkrNo)

        Dim repoSRNNO As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSRNNO.FormatString = ""
        repoSRNNO.HeaderText = "SRN No"
        repoSRNNO.Name = colSRNNo
        repoSRNNO.Width = 200
        repoSRNNO.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoSRNNO)

        Dim repoSRNDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoSRNDate.FormatString = "{0:d}"
        repoSRNDate.CustomFormat = "dd/MM/yyyy hh:mm:ss tt"
        repoSRNDate.HeaderText = "SRN Date"
        repoSRNDate.Name = colSRNDATe
        repoSRNDate.Width = 100
        repoSRNDate.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoSRNDate)

        Dim repoChName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoChName.FormatString = ""
        repoChName.HeaderText = "Chamber Desc"
        repoChName.Name = colChamberDesc
        repoChName.Width = 180
        repoChName.ReadOnly = True
        If TankerFromMaster = 0 Then repoChName.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoChName)

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colItemCode
        repoICode.Width = 100
        repoICode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item Desc"
        repoIName.Name = colItemDesc
        repoIName.Width = 180
        repoIName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIName)

        Dim repoHSNCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoHSNCode.FormatString = ""
        repoHSNCode.HeaderText = "HSN Code"
        repoHSNCode.Name = colHSN
        repoHSNCode.Width = 100
        repoHSNCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoHSNCode)


        Dim repoUOM As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUOM.FormatString = ""
        repoUOM.HeaderText = "UOM"
        repoUOM.Name = colUOM
        repoUOM.ReadOnly = True
        repoUOM.Width = 80
        repoUOM.WrapText = True
        repoUOM.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoUOM)

        Dim repoGrossWeight As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoGrossWeight.FormatString = ""
        repoGrossWeight.HeaderText = "Gross Weight"
        repoGrossWeight.Name = colGrossWeight
        repoGrossWeight.Width = 100
        repoGrossWeight.ReadOnly = True
        repoGrossWeight.IsVisible = False
        repoGrossWeight.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoGrossWeight)

        Dim repoTareWeight As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTareWeight.FormatString = ""
        repoTareWeight.HeaderText = "Tare Weight"
        repoTareWeight.Name = colTareWeight
        repoTareWeight.ReadOnly = True
        repoTareWeight.Width = 100
        repoTareWeight.WrapText = True
        repoTareWeight.IsVisible = False
        repoTareWeight.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTareWeight)

        Dim repoNetWeight As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoNetWeight.FormatString = ""
        repoNetWeight.HeaderText = "Net Weight"
        repoNetWeight.Name = colNetWeight
        repoNetWeight.ReadOnly = True
        repoNetWeight.Width = 100
        repoNetWeight.WrapText = True
        repoNetWeight.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoNetWeight)

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
        gv1.MasterTemplate.Columns.Add(repoPendingQty)

        Dim repoInvoiceQty As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoInvoiceQty.FormatString = ""
        repoInvoiceQty.HeaderText = "Invoice Qty"
        repoInvoiceQty.Name = colQty
        repoInvoiceQty.ReadOnly = True
        repoInvoiceQty.WrapText = True
        repoInvoiceQty.Width = 100
        repoInvoiceQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoInvoiceQty)


        Dim repoPriceCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPriceCode.FormatString = ""
        repoPriceCode.HeaderText = "Price Code"
        repoPriceCode.Name = colPriceCode
        repoPriceCode.ReadOnly = True
        repoPriceCode.Width = 80
        repoPriceCode.WrapText = True
        repoPriceCode.IsVisible = False
        repoPriceCode.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoPriceCode)

        Dim repoFatPer As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFatPer.FormatString = ""
        repoFatPer.HeaderText = "FAT(%)"
        repoFatPer.Name = colFat
        repoFatPer.ReadOnly = True
        repoFatPer.Width = 80
        repoFatPer.WrapText = True
        'repoFatPer.IsVisible = False
        repoFatPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoFatPer)

        Dim repoSNFPer As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSNFPer.FormatString = ""
        repoSNFPer.HeaderText = "SNF(%)"
        repoSNFPer.Name = colSNF
        repoSNFPer.ReadOnly = True
        repoSNFPer.Width = 80
        repoSNFPer.WrapText = True
        'repoSNFPer.IsVisible = False
        repoSNFPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoSNFPer)


        Dim repoFATKG As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoFATKG.FormatString = ""
        repoFATKG.HeaderText = "FAT(KG)"
        repoFATKG.Name = colFatKG
        repoFATKG.ReadOnly = True
        repoFATKG.Width = 80
        repoFATKG.WrapText = True
        repoFATKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoFATKG)

        Dim repoSNFKG As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSNFKG.FormatString = ""
        repoSNFKG.HeaderText = "SNF(KG)"
        repoSNFKG.Name = colSNFKG
        repoSNFKG.ReadOnly = True
        repoSNFKG.Width = 80
        repoSNFKG.WrapText = True
        repoSNFKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoSNFKG)






        Dim repoAmount As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAmount.FormatString = ""
        repoAmount.HeaderText = "Amount"
        repoAmount.Name = colAmt
        repoAmount.ReadOnly = True
        repoAmount.Width = 100
        repoAmount.WrapText = True
        repoAmount.IsVisible = False
        repoAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAmount)

        Dim repoDeduc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDeduc.FormatString = ""
        repoDeduc.HeaderText = "Deduction"
        repoDeduc.Name = colDeduc
        repoDeduc.ReadOnly = True
        repoDeduc.Width = 80
        repoDeduc.WrapText = True
        repoDeduc.IsVisible = False
        repoDeduc.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoDeduc)

        Dim repoIncen As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIncen.FormatString = ""
        repoIncen.HeaderText = "Incentive"
        repoIncen.Name = colIncen
        repoIncen.ReadOnly = True
        repoIncen.Width = 80
        repoIncen.WrapText = True
        repoIncen.IsVisible = False
        repoIncen.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoIncen)

        Dim repoSpclDeduc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSpclDeduc.FormatString = ""
        repoSpclDeduc.HeaderText = "Special Deduction"
        repoSpclDeduc.Name = colSpecialDeduc
        repoSpclDeduc.ReadOnly = True
        repoSpclDeduc.Width = 80
        repoSpclDeduc.WrapText = True
        repoSpclDeduc.IsVisible = False
        repoSpclDeduc.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoSpclDeduc)

        Dim repoActAmt As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoActAmt.FormatString = ""
        repoActAmt.HeaderText = "Extra Rate"
        repoActAmt.Name = colXtraRate
        repoActAmt.ReadOnly = True
        repoActAmt.Width = 80
        repoActAmt.WrapText = True
        repoActAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoActAmt)

        repoActAmt = New GridViewTextBoxColumn()
        repoActAmt.FormatString = ""
        repoActAmt.HeaderText = "Net Rate"
        repoActAmt.Name = colNetRate
        repoActAmt.ReadOnly = True
        repoActAmt.Width = 80
        repoActAmt.WrapText = True
        repoActAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoActAmt)

        Dim repoFATRate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoFATRate.FormatString = ""
        repoFATRate.HeaderText = "FAT Rate"
        repoFATRate.Name = colFatRate
        repoFATRate.ReadOnly = True
        repoFATRate.Width = 80
        repoFATRate.WrapText = True
        repoFATRate.IsVisible = True
        repoFATRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoFATRate)


        Dim repoSNFRate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSNFRate.FormatString = ""
        repoSNFRate.HeaderText = "SNF Rate"
        repoSNFRate.Name = colSNFRate
        repoSNFRate.ReadOnly = True
        repoSNFRate.Width = 80
        repoSNFRate.WrapText = True
        repoSNFRate.IsVisible = True
        repoSNFRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoSNFRate)

        repoActAmt = New GridViewTextBoxColumn()
        repoActAmt.FormatString = ""
        repoActAmt.HeaderText = "Amount"
        repoActAmt.Name = colActAmt
        repoActAmt.ReadOnly = True
        repoActAmt.Width = 80
        repoActAmt.WrapText = True
        repoActAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoActAmt)






        Dim repoTax1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax1.FormatString = ""
        repoTax1.HeaderText = "Tax 1"
        repoTax1.Name = colTax1
        repoTax1.ReadOnly = True
        repoTax1.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax1) '26

        Dim repoTaxBaseAmt1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt1.FormatString = ""
        repoTaxBaseAmt1.HeaderText = "Tax Base Amount 1"
        repoTaxBaseAmt1.Name = colTaxBaseAmt1
        repoTaxBaseAmt1.ReadOnly = True
        repoTaxBaseAmt1.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt1) '27

        Dim repoTaxRate1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate1 = New GridViewDecimalColumn()
        repoTaxRate1.FormatString = ""
        repoTaxRate1.HeaderText = "Tax Rate 1"
        repoTaxRate1.Name = colTaxRate1
        repoTaxRate1.IsVisible = False
        repoTaxRate1.ReadOnly = True
        repoTaxRate1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate1) '28

        Dim repoTaxAmt1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt1 = New GridViewDecimalColumn()
        repoTaxAmt1.FormatString = ""
        repoTaxAmt1.HeaderText = "Tax Amt 1"
        repoTaxAmt1.Name = colTaxAmt1
        repoTaxAmt1.IsVisible = False
        repoTaxAmt1.ReadOnly = True
        repoTaxAmt1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt1) '29

        Dim repoIsSurTax1 = New GridViewCheckBoxColumn()
        repoIsSurTax1.HeaderText = "Is Surtax 1"
        repoIsSurTax1.Name = colIsSurTax1
        repoIsSurTax1.ReadOnly = True
        repoIsSurTax1.IsVisible = False
        repoIsSurTax1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax1) '30

        Dim repoSurTaxCode1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode1.FormatString = ""
        repoSurTaxCode1.HeaderText = "Surtax 1"
        repoSurTaxCode1.Name = colSurTaxCode1
        repoSurTaxCode1.ReadOnly = True
        repoSurTaxCode1.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode1) '31

        Dim repoIsTaxable1 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Is Taxable 1"
        repoIsTaxable1.Name = colIsTaxable1
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable1) '32

        Dim repoIsExcisable1 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable1.HeaderText = "Is Excisable 1"
        repoIsExcisable1.Name = colIsExcisable1
        repoIsExcisable1.ReadOnly = True
        repoIsExcisable1.IsVisible = False
        repoIsExcisable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable1) '106

        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Tax On Base Amt 1"
        repoIsTaxable1.Name = colTaxOnBaseAmt1
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable1) '32

        Dim repoTax2 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax2.FormatString = ""
        repoTax2.HeaderText = "Tax 2"
        repoTax2.Name = colTax2
        repoTax2.ReadOnly = True
        repoTax2.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax2) '34

        Dim repoTaxBaseAmt2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt2.FormatString = ""
        repoTaxBaseAmt2.HeaderText = "Tax Base Amount 2"
        repoTaxBaseAmt2.Name = colTaxBaseAmt2
        repoTaxBaseAmt2.ReadOnly = True
        repoTaxBaseAmt2.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt2) '35

        Dim repoTaxRate2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate2 = New GridViewDecimalColumn()
        repoTaxRate2.FormatString = ""
        repoTaxRate2.HeaderText = "Tax Rate 2"
        repoTaxRate2.Name = colTaxRate2
        repoTaxRate2.IsVisible = False
        repoTaxRate2.ReadOnly = True
        repoTaxRate2.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate2) '36

        Dim repoTaxAmt2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt2 = New GridViewDecimalColumn()
        repoTaxAmt2.FormatString = ""
        repoTaxAmt2.HeaderText = "Tax Amt 2"
        repoTaxAmt2.Name = colTaxAmt2
        repoTaxAmt2.IsVisible = False
        repoTaxAmt2.ReadOnly = True
        repoTaxAmt2.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt2) '37

        Dim repoIsSurTax2 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax2.HeaderText = "Is Surtax 2"
        repoIsSurTax2.Name = colIsSurTax2
        repoIsSurTax2.ReadOnly = True
        repoIsSurTax2.IsVisible = False
        repoIsSurTax2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax2) '38

        Dim repoSurTaxCode2 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode2.FormatString = ""
        repoSurTaxCode2.HeaderText = "Surtax 2"
        repoSurTaxCode2.Name = colSurTaxCode2
        repoSurTaxCode2.ReadOnly = True
        repoSurTaxCode2.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode2) '39

        Dim repoIsTaxable2 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable2.HeaderText = "Is Taxable 2"
        repoIsTaxable2.Name = colIsTaxable2
        repoIsTaxable2.ReadOnly = True
        repoIsTaxable2.IsVisible = False
        repoIsTaxable2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable2) '40

        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Tax On Base Amt 2"
        repoIsTaxable1.Name = colTaxOnBaseAmt2
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable1)

        Dim repoIsExcisable2 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable2.HeaderText = "Is Excisable 2"
        repoIsExcisable2.Name = colIsExcisable2
        repoIsExcisable2.ReadOnly = True
        repoIsExcisable2.IsVisible = False
        repoIsExcisable2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable2)

        Dim repoTax3 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax3.FormatString = ""
        repoTax3.HeaderText = "Tax 3"
        repoTax3.Name = colTax3
        repoTax3.ReadOnly = True
        repoTax3.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax3) '42

        Dim repoTaxBaseAmt3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt3.FormatString = ""
        repoTaxBaseAmt3.HeaderText = "Tax Base Amount 3"
        repoTaxBaseAmt3.Name = colTaxBaseAmt3
        repoTaxBaseAmt3.ReadOnly = True
        repoTaxBaseAmt3.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt3) '43

        Dim repoTaxRate3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate3 = New GridViewDecimalColumn()
        repoTaxRate3.FormatString = ""
        repoTaxRate3.HeaderText = "Tax Rate 3"
        repoTaxRate3.Name = colTaxRate3
        repoTaxRate3.IsVisible = False
        repoTaxRate3.ReadOnly = True
        repoTaxRate3.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate3) '44

        Dim repoTaxAmt3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt3 = New GridViewDecimalColumn()
        repoTaxAmt3.FormatString = ""
        repoTaxAmt3.HeaderText = "Tax Amt 3"
        repoTaxAmt3.Name = colTaxAmt3
        repoTaxAmt3.IsVisible = False
        repoTaxAmt3.ReadOnly = True
        repoTaxAmt3.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt3) '45

        Dim repoIsSurTax3 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax3.HeaderText = "Is Surtax 3"
        repoIsSurTax3.Name = colIsSurTax3
        repoIsSurTax3.ReadOnly = True
        repoIsSurTax3.IsVisible = False
        repoIsSurTax3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax3) '46

        Dim repoSurTaxCode3 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode3.FormatString = ""
        repoSurTaxCode3.HeaderText = "Surtax 3"
        repoSurTaxCode3.Name = colSurTaxCode3
        repoSurTaxCode3.ReadOnly = True
        repoSurTaxCode3.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode3) '47

        Dim repoIsTaxable3 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable3.HeaderText = "Is Taxable 3"
        repoIsTaxable3.Name = colIsTaxable3
        repoIsTaxable3.ReadOnly = True
        repoIsTaxable3.IsVisible = False
        repoIsTaxable3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable3) '48

        Dim repoIsExcisable3 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable3.HeaderText = "Is Excisable 3"
        repoIsExcisable3.Name = colIsExcisable3
        repoIsExcisable3.ReadOnly = True
        repoIsExcisable3.IsVisible = False
        repoIsExcisable3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable3)

        Dim repoTax4 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax4.FormatString = ""
        repoTax4.HeaderText = "Tax 4"
        repoTax4.Name = colTax4
        repoTax4.ReadOnly = True
        repoTax4.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax4) '50

        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Tax On Base Amt 3"
        repoIsTaxable1.Name = colTaxOnBaseAmt3
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable1)

        Dim repoTaxBaseAmt4 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt4.FormatString = ""
        repoTaxBaseAmt4.HeaderText = "Tax Base Amount 4"
        repoTaxBaseAmt4.Name = colTaxBaseAmt4
        repoTaxBaseAmt4.ReadOnly = True
        repoTaxBaseAmt4.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt4) '51

        Dim repoTaxRate4 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate4 = New GridViewDecimalColumn()
        repoTaxRate4.FormatString = ""
        repoTaxRate4.HeaderText = "Tax Rate 4"
        repoTaxRate4.Name = colTaxRate4
        repoTaxRate4.IsVisible = False
        repoTaxRate4.ReadOnly = True
        repoTaxRate4.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate4) '52

        Dim repoTaxAmt4 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt4 = New GridViewDecimalColumn()
        repoTaxAmt4.FormatString = ""
        repoTaxAmt4.HeaderText = "Tax Amt 4"
        repoTaxAmt4.Name = colTaxAmt4
        repoTaxAmt4.IsVisible = False
        repoTaxAmt4.ReadOnly = True
        repoTaxAmt4.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt4) '53

        Dim repoIsSurTax4 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax4.HeaderText = "Is Surtax 4"
        repoIsSurTax4.Name = colIsSurTax4
        repoIsSurTax4.ReadOnly = True
        repoIsSurTax4.IsVisible = False
        repoIsSurTax4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax4) '54

        Dim repoSurTaxCode4 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode4.FormatString = ""
        repoSurTaxCode4.HeaderText = "Surtax 4"
        repoSurTaxCode4.Name = colSurTaxCode4
        repoSurTaxCode4.ReadOnly = True
        repoSurTaxCode4.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode4) '55

        Dim repoIsTaxable4 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable4.HeaderText = "Is Taxable 4"
        repoIsTaxable4.Name = colIsTaxable4
        repoIsTaxable4.ReadOnly = True
        repoIsTaxable4.IsVisible = False
        repoIsTaxable4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable4) '56

        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Tax On Base Amt 4"
        repoIsTaxable1.Name = colTaxOnBaseAmt4
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable1)


        Dim repoIsExcisable4 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable4.HeaderText = "Is Excisable 4"
        repoIsExcisable4.Name = colIsExcisable4
        repoIsExcisable4.ReadOnly = True
        repoIsExcisable4.IsVisible = False
        repoIsExcisable4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable4)

        Dim repoTax5 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax5.FormatString = ""
        repoTax5.HeaderText = "Tax 5"
        repoTax5.Name = colTax5
        repoTax5.ReadOnly = True
        repoTax5.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax5) '58

        Dim repoTaxBaseAmt5 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt5.FormatString = ""
        repoTaxBaseAmt5.HeaderText = "Tax Base Amount 5"
        repoTaxBaseAmt5.Name = colTaxBaseAmt5
        repoTaxBaseAmt5.ReadOnly = True
        repoTaxBaseAmt5.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt5) '59

        Dim repoTaxRate5 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate5 = New GridViewDecimalColumn()
        repoTaxRate5.FormatString = ""
        repoTaxRate5.HeaderText = "Tax Rate 5"
        repoTaxRate5.Name = colTaxRate5
        repoTaxRate5.IsVisible = False
        repoTaxRate5.ReadOnly = True
        repoTaxRate5.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate5) '60

        Dim repoTaxAmt5 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt5 = New GridViewDecimalColumn()
        repoTaxAmt5.FormatString = ""
        repoTaxAmt5.HeaderText = "Tax Amt 5"
        repoTaxAmt5.Name = colTaxAmt5
        repoTaxAmt5.IsVisible = False
        repoTaxAmt3.ReadOnly = True
        repoTaxAmt5.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt5) '61

        Dim repoIsSurTax5 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax5.HeaderText = "Is Surtax 5"
        repoIsSurTax5.Name = colIsSurTax5
        repoIsSurTax5.ReadOnly = True
        repoIsSurTax5.IsVisible = False
        repoIsSurTax5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax5) '62

        Dim repoSurTaxCode5 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode5.FormatString = ""
        repoSurTaxCode5.HeaderText = "Surtax 5"
        repoSurTaxCode5.Name = colSurTaxCode5
        repoSurTaxCode5.ReadOnly = True
        repoSurTaxCode5.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode5) '63

        Dim repoIsTaxable5 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable5.HeaderText = "Is Taxable 5"
        repoIsTaxable5.Name = colIsTaxable5
        repoIsTaxable5.ReadOnly = True
        repoIsTaxable5.IsVisible = False
        repoIsTaxable5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable5) '64

        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Tax On Base Amt 5"
        repoIsTaxable1.Name = colTaxOnBaseAmt5
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable1)

        Dim repoTax6 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax6.FormatString = ""
        repoTax6.HeaderText = "Tax 6"
        repoTax6.Name = colTax6
        repoTax6.ReadOnly = True
        repoTax6.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax6) '66

        Dim repoIsExcisable5 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable5.HeaderText = "Is Excisable 5"
        repoIsExcisable5.Name = colIsExcisable5
        repoIsExcisable5.ReadOnly = True
        repoIsExcisable5.IsVisible = False
        repoIsExcisable5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable5)

        Dim repoTaxBaseAmt6 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt6.FormatString = ""
        repoTaxBaseAmt6.HeaderText = "Tax Base Amount 6"
        repoTaxBaseAmt6.Name = colTaxBaseAmt6
        repoTaxBaseAmt6.ReadOnly = True
        repoTaxBaseAmt6.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt6) '67

        Dim repoTaxRate6 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate6 = New GridViewDecimalColumn()
        repoTaxRate6.FormatString = ""
        repoTaxRate6.HeaderText = "Tax Rate 6"
        repoTaxRate6.Name = colTaxRate6
        repoTaxRate6.IsVisible = False
        repoTaxRate6.ReadOnly = True
        repoTaxRate6.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate6)

        Dim repoTaxAmt6 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt6 = New GridViewDecimalColumn()
        repoTaxAmt6.FormatString = ""
        repoTaxAmt6.HeaderText = "Tax Amt 6"
        repoTaxAmt6.Name = colTaxAmt6
        repoTaxAmt6.IsVisible = False
        repoTaxAmt6.ReadOnly = True
        repoTaxAmt6.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt6)

        Dim repoIsSurTax6 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax6.HeaderText = "Is Surtax 6"
        repoIsSurTax6.Name = colIsSurTax6
        repoIsSurTax6.ReadOnly = True
        repoIsSurTax6.IsVisible = False
        repoIsSurTax6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax6)

        Dim repoSurTaxCode6 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode6.FormatString = ""
        repoSurTaxCode6.HeaderText = "Surtax 6"
        repoSurTaxCode6.Name = colSurTaxCode6
        repoSurTaxCode6.ReadOnly = True
        repoSurTaxCode6.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode6)

        Dim repoIsTaxable6 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable6.HeaderText = "Is Taxable 6"
        repoIsTaxable6.Name = colIsTaxable6
        repoIsTaxable6.ReadOnly = True
        repoIsTaxable6.IsVisible = False
        repoIsTaxable6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable6) '72

        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Tax On Base Amt 6"
        repoIsTaxable1.Name = colTaxOnBaseAmt6
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable1)

        Dim repoIsExcisable6 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable6.HeaderText = "Is Excisable 6"
        repoIsExcisable6.Name = colIsExcisable6
        repoIsExcisable6.ReadOnly = True
        repoIsExcisable6.IsVisible = False
        repoIsExcisable6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable6)

        Dim repoTax7 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax7.FormatString = ""
        repoTax7.HeaderText = "Tax 7"
        repoTax7.Name = colTax7
        repoTax7.ReadOnly = True
        repoTax7.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax7) '74

        Dim repoTaxBaseAmt7 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt7.FormatString = ""
        repoTaxBaseAmt7.HeaderText = "Tax Base Amount 7"
        repoTaxBaseAmt7.Name = colTaxBaseAmt7
        repoTaxBaseAmt7.ReadOnly = True
        repoTaxBaseAmt7.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt7)

        Dim repoTaxRate7 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate7 = New GridViewDecimalColumn()
        repoTaxRate7.FormatString = ""
        repoTaxRate7.HeaderText = "Tax Rate 7"
        repoTaxRate7.Name = colTaxRate7
        repoTaxRate7.IsVisible = False
        repoTaxRate7.ReadOnly = True
        repoTaxRate7.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate7)

        Dim repoTaxAmt7 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt7 = New GridViewDecimalColumn()
        repoTaxAmt7.FormatString = ""
        repoTaxAmt7.HeaderText = "Tax Amt 7"
        repoTaxAmt7.Name = colTaxAmt7
        repoTaxAmt7.IsVisible = False
        repoTaxAmt7.ReadOnly = True
        repoTaxAmt7.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt7)

        Dim repoIsSurTax7 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax7.HeaderText = "Is Surtax 7"
        repoIsSurTax7.Name = colIsSurTax7
        repoIsSurTax7.ReadOnly = True
        repoIsSurTax7.IsVisible = False
        repoIsSurTax7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax7)

        Dim repoSurTaxCode7 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode7.FormatString = ""
        repoSurTaxCode7.HeaderText = "Surtax 7"
        repoSurTaxCode7.Name = colSurTaxCode7
        repoSurTaxCode7.ReadOnly = True
        repoSurTaxCode7.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode7)

        Dim repoIsTaxable7 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable7.HeaderText = "Is Taxable 7"
        repoIsTaxable7.Name = colIsTaxable7
        repoIsTaxable7.ReadOnly = True
        repoIsTaxable7.IsVisible = False
        repoIsTaxable7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable7) '80

        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Tax On Base Amt 7"
        repoIsTaxable1.Name = colTaxOnBaseAmt7
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable1)

        Dim repoIsExcisable7 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable7.HeaderText = "Is Excisable 7"
        repoIsExcisable7.Name = colIsExcisable7
        repoIsExcisable7.ReadOnly = True
        repoIsExcisable7.IsVisible = False
        repoIsExcisable7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable7)

        Dim repoTax8 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax8.FormatString = ""
        repoTax8.HeaderText = "Tax 8"
        repoTax8.Name = colTax8
        repoTax8.ReadOnly = True
        repoTax8.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax8) '82

        Dim repoTaxBaseAmt8 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt8.FormatString = ""
        repoTaxBaseAmt8.HeaderText = "Tax Base Amount 8"
        repoTaxBaseAmt8.Name = colTaxBaseAmt8
        repoTaxBaseAmt8.ReadOnly = True
        repoTaxBaseAmt8.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt8)

        Dim repoTaxRate8 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate8 = New GridViewDecimalColumn()
        repoTaxRate8.FormatString = ""
        repoTaxRate8.HeaderText = "Tax Rate 8"
        repoTaxRate8.Name = colTaxRate8
        repoTaxRate8.IsVisible = False
        repoTaxRate8.ReadOnly = True
        repoTaxRate8.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate8)

        Dim repoTaxAmt8 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt8 = New GridViewDecimalColumn()
        repoTaxAmt8.FormatString = ""
        repoTaxAmt8.HeaderText = "Tax Amt 8"
        repoTaxAmt8.Name = colTaxAmt8
        repoTaxAmt8.IsVisible = False
        repoTaxAmt8.ReadOnly = True
        repoTaxAmt8.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt8)

        Dim repoIsSurTax8 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax8.HeaderText = "Is Surtax 8"
        repoIsSurTax8.Name = colIsSurTax8
        repoIsSurTax8.ReadOnly = True
        repoIsSurTax8.IsVisible = False
        repoIsSurTax8.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax8)

        Dim repoSurTaxCode8 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode8.FormatString = ""
        repoSurTaxCode8.HeaderText = "Surtax 8"
        repoSurTaxCode8.Name = colSurTaxCode8
        repoSurTaxCode8.ReadOnly = True
        repoSurTaxCode8.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode8)

        Dim repoIsTaxable8 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable8.HeaderText = "Is Taxable 8"
        repoIsTaxable8.Name = colIsTaxable8
        repoIsTaxable8.ReadOnly = True
        repoIsTaxable8.IsVisible = False
        repoIsTaxable8.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable8) '88

        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Tax On Base Amt 8"
        repoIsTaxable1.Name = colTaxOnBaseAmt8
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable1)

        Dim repoIsExcisable8 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable8.HeaderText = "Is Excisable 8"
        repoIsExcisable8.Name = colIsExcisable8
        repoIsExcisable8.ReadOnly = True
        repoIsExcisable8.IsVisible = False
        repoIsExcisable8.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable8)

        Dim repoTax9 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax9.FormatString = ""
        repoTax9.HeaderText = "Tax 9"
        repoTax9.Name = colTax9
        repoTax9.ReadOnly = True
        repoTax9.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax9) '90

        Dim repoTaxBaseAmt9 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt9.FormatString = ""
        repoTaxBaseAmt9.HeaderText = "Tax Base Amount 9"
        repoTaxBaseAmt9.Name = colTaxBaseAmt9
        repoTaxBaseAmt9.ReadOnly = True
        repoTaxBaseAmt9.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt9)

        Dim repoTaxRate9 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate9 = New GridViewDecimalColumn()
        repoTaxRate9.FormatString = ""
        repoTaxRate9.HeaderText = "Tax Rate 9"
        repoTaxRate9.Name = colTaxRate9
        repoTaxRate9.IsVisible = False
        repoTaxRate9.ReadOnly = True
        repoTaxRate9.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate9)

        Dim repoTaxAmt9 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt9 = New GridViewDecimalColumn()
        repoTaxAmt9.FormatString = ""
        repoTaxAmt9.HeaderText = "Tax Amt 9"
        repoTaxAmt9.Name = colTaxAmt9
        repoTaxAmt9.IsVisible = False
        repoTaxAmt9.ReadOnly = True
        repoTaxAmt9.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt9)

        Dim repoIsSurTax9 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax9.HeaderText = "Is Surtax 9"
        repoIsSurTax9.Name = colIsSurTax9
        repoIsSurTax9.ReadOnly = True
        repoIsSurTax9.IsVisible = False
        repoIsSurTax9.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax9)

        Dim repoSurTaxCode9 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode9.FormatString = ""
        repoSurTaxCode9.HeaderText = "Surtax 9"
        repoSurTaxCode9.Name = colSurTaxCode9
        repoSurTaxCode9.ReadOnly = True
        repoSurTaxCode9.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode9)

        Dim repoIsTaxable9 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable9.HeaderText = "Is Taxable 9"
        repoIsTaxable9.Name = colIsTaxable9
        repoIsTaxable9.ReadOnly = True
        repoIsTaxable9.IsVisible = False
        repoIsTaxable9.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable9) '96

        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Tax On Base Amt 9"
        repoIsTaxable1.Name = colTaxOnBaseAmt9
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable1)

        Dim repoIsExcisable9 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable9.HeaderText = "Is Excisable 9"
        repoIsExcisable9.Name = colIsExcisable9
        repoIsExcisable9.ReadOnly = True
        repoIsExcisable9.IsVisible = False
        repoIsExcisable9.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable9)

        Dim repoTax10 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax10.FormatString = ""
        repoTax10.HeaderText = "Tax 10"
        repoTax10.Name = colTax10
        repoTax10.ReadOnly = True
        repoTax10.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax10) '98

        Dim repoTaxBaseAmt10 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt10.FormatString = ""
        repoTaxBaseAmt10.HeaderText = "Tax Base Amount 10"
        repoTaxBaseAmt10.Name = colTaxBaseAmt10
        repoTaxBaseAmt10.ReadOnly = True
        repoTaxBaseAmt10.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt10)

        Dim repoTaxRate10 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate10 = New GridViewDecimalColumn()
        repoTaxRate10.FormatString = ""
        repoTaxRate10.HeaderText = "Tax Rate 10"
        repoTaxRate10.Name = colTaxRate10
        repoTaxRate10.IsVisible = False
        repoTaxRate10.ReadOnly = True
        repoTaxRate10.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate10)

        Dim repoTaxAmt10 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt10 = New GridViewDecimalColumn()
        repoTaxAmt10.FormatString = ""
        repoTaxAmt10.HeaderText = "Tax Amt 10"
        repoTaxAmt10.Name = colTaxAmt10
        repoTaxAmt10.IsVisible = False
        repoTaxAmt10.ReadOnly = True
        repoTaxAmt10.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt10)

        Dim repoIsSurTax10 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax10.HeaderText = "Is Surtax 10"
        repoIsSurTax10.Name = colIsSurTax10
        repoIsSurTax10.ReadOnly = True
        repoIsSurTax10.IsVisible = False
        repoIsSurTax10.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax10)

        Dim repoSurTaxCode10 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode10.FormatString = ""
        repoSurTaxCode10.HeaderText = "Surtax 10"
        repoSurTaxCode10.Name = colSurTaxCode10
        repoSurTaxCode10.ReadOnly = True
        repoSurTaxCode10.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode10)

        Dim repoIsTaxable10 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable10.HeaderText = "Is Taxable 10"
        repoIsTaxable10.Name = colIsTaxable10
        repoIsTaxable10.ReadOnly = True
        repoIsTaxable10.IsVisible = False
        repoIsTaxable10.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable10) '104

        repoIsTaxable1 = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Tax On Base Amt 10"
        repoIsTaxable1.Name = colTaxOnBaseAmt10
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable1)



        Dim repoIsExcisable10 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable10.HeaderText = "Is Excisable 10"
        repoIsExcisable10.Name = colIsExcisable10
        repoIsExcisable10.ReadOnly = True
        repoIsExcisable10.IsVisible = False
        repoIsExcisable10.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable10) '115

        Dim repoTotTaxAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotTaxAmt = New GridViewDecimalColumn()
        repoTotTaxAmt.FormatString = ""
        repoTotTaxAmt.HeaderText = "Tax Amount"
        repoTotTaxAmt.Name = colTotTaxAmt
        repoTotTaxAmt.Width = 80
        repoTotTaxAmt.ReadOnly = True
        repoTotTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTotTaxAmt) '116

        Dim repoTranspoterCharge As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTranspoterCharge = New GridViewDecimalColumn()
        repoTranspoterCharge.FormatString = ""
        repoTranspoterCharge.HeaderText = "Transpoter Charge"
        repoTranspoterCharge.Name = colTranspoterCharge
        repoTranspoterCharge.Width = 80
        repoTranspoterCharge.ReadOnly = True
        repoTranspoterCharge.IsVisible = ApplyTransportChargeAddInActualAmount
        repoTranspoterCharge.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTranspoterCharge)

        Dim repoAmtAfterTax As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmtAfterTax = New GridViewDecimalColumn()
        repoAmtAfterTax.FormatString = ""
        repoAmtAfterTax.HeaderText = "Net Amount"
        repoAmtAfterTax.Name = colItemNetAmt
        repoAmtAfterTax.WrapText = True
        repoAmtAfterTax.Width = 80
        repoAmtAfterTax.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAmtAfterTax.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoAmtAfterTax) '117

        gv1.AllowAddNewRow = False
        gv1.AllowColumnChooser = True
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = True
        gv1.EnableSorting = False
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.EnableAlternatingRowColor = True
        gv1.TableElement.TableHeaderHeight = 40
    End Sub
    Private Sub fndSRNNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndSRNNo._MYValidating
        isLoadData = True
        isSRNselected = True
        If isButtonClicked Then
            Dim objB As clsBulkMilkSRN = Nothing
            Dim objGateEntry As clsGateEntry = Nothing
            If dtpFromDate.Value > dtpToDate.Value Then
                clsCommon.MyMessageBoxShow(Me, " 'From Date' can't be greator than 'To Date'", Me.Text)
                dtpFromDate.Focus()
                Exit Sub
            End If

            If clsCommon.myCDate(dtpDocDate.Value, "dd/MMM/yyyy") < clsCommon.myCDate(dtpToDate.Value, "dd/MMM/yyyy") Then
                clsCommon.MyMessageBoxShow(Me, " 'To Date' can't be greator than 'Doc Date'", Me.Text)
                dtpToDate.Focus()
                Exit Sub
            End If
            Dim frm As FrmPendingBulkMilkSrn = New FrmPendingBulkMilkSrn()
            Dim strZeroSRN As String = ""
            If BulkProcPriceChartStandardRateWithZero = 1 Then
                strZeroSRN = " and TSPL_Bulk_MILK_SRN.Actual_Amount > 0 "
            End If
            ''richa agarwal against ticket no BM00000008799
            If TankerFromMaster = 0 Then
                frm.qry = " SELECT xxx.*,TSPL_Bulk_MILK_SRN.Vendor_Code,TSPL_Bulk_MILK_SRN.Loc_Code,TSPL_Bulk_MILK_SRN.SRN_Date ,TSPL_Bulk_MILK_SRN.UOM   FROM (select TSPL_Bulk_MILK_SRN.SRN_NO,TSPL_Bulk_MILK_SRN.Item_Code,(TSPL_Bulk_MILK_SRN.Net_Weight),coalesce(SUM(tdl.invoice_qty),0)  as invoice_qty,(TSPL_Bulk_MILK_SRN.Net_Weight-coalesce(SUM(tdl.invoice_qty),0)) as remaining_qty from TSPL_Bulk_MILK_SRN left join ( select invoice_qty,SRN_NO  from Tspl_bulk_milk_purchase_invoice_detail left outer join Tspl_bulk_milk_purchase_invoice_head on Tspl_bulk_milk_purchase_invoice_head.DOC_NO=tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO  ) as  tdl  on TSPL_Bulk_MILK_SRN.SRN_NO=tdl.srn_no  where TSPL_Bulk_MILK_SRN.isPosted=1 " & strZeroSRN & " group by TSPL_Bulk_MILK_SRN.SRN_NO,TSPL_Bulk_MILK_SRN.Item_Code,(TSPL_Bulk_MILK_SRN.Net_Weight) )  xxx   left outer join TSPL_Bulk_MILK_SRN on TSPL_Bulk_MILK_SRN.SRN_NO=xxx.SRN_NO  where (xxx.Net_Weight-xxx.invoice_qty) >0 " & IIf(clsCommon.myLen(fndVendor.Value) > 0, " and Vendor_Code ='" & fndVendor.Value & "'", "") & " and SRN_Date between CONVERT(datetime,'" & clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy hh:mm:ss tt") & "',103) and CONVERT(datetime,'" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy hh:mm:ss tt") & "' ,103) " & IIf(chkSRNTrade.Checked, "", "and ISNULL(isApproved,0)=1 ")
            Else
                frm.qry = " SELECT xxx.*,TSPL_Bulk_MILK_SRN.Vendor_Code,TSPL_Bulk_MILK_SRN.Loc_Code,TSPL_Bulk_MILK_SRN.SRN_Date ,TSPL_Bulk_MILK_SRN.UOM   FROM (select TSPL_Bulk_MILK_SRN.SRN_NO,TSPL_Bulk_MILK_SRN.Item_Code,(TSPL_Bulk_MILK_SRN.Net_Weight),coalesce(SUM(tdl.invoice_qty),0)  as invoice_qty,(TSPL_Bulk_MILK_SRN.Net_Weight-coalesce(SUM(tdl.invoice_qty),0)) as remaining_qty from TSPL_Bulk_MILK_SRN left join ( select invoice_qty,SRN_NO  from Tspl_bulk_milk_purchase_invoice_detail left outer join Tspl_bulk_milk_purchase_invoice_head on Tspl_bulk_milk_purchase_invoice_head.DOC_NO=tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO  ) as  tdl  on TSPL_Bulk_MILK_SRN.SRN_NO=tdl.srn_no  where TSPL_Bulk_MILK_SRN.isPosted=1 " & strZeroSRN & " group by TSPL_Bulk_MILK_SRN.SRN_NO,TSPL_Bulk_MILK_SRN.Item_Code,(TSPL_Bulk_MILK_SRN.Net_Weight) )  xxx   left outer join TSPL_Bulk_MILK_SRN on TSPL_Bulk_MILK_SRN.SRN_NO=xxx.SRN_NO  where (xxx.Net_Weight-xxx.invoice_qty) >0 " & IIf(clsCommon.myLen(fndVendor.Value) > 0, " and Vendor_Code ='" & fndVendor.Value & "'", "") & " and SRN_Date >= CONVERT(datetime,'" & clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy hh:mm:ss tt") & "',103) and SRN_Date <= CONVERT(datetime,'" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy hh:mm:ss tt") & "' ,103) " & IIf(chkSRNTrade.Checked, "", "")
            End If

            Dim whrCls As String = String.Empty
            If clsCommon.myLen(fndLocation.Value) > 0 Then
                whrCls = " and TSPL_Bulk_MILK_SRN.Loc_Code='" & fndLocation.Value & "'"
            End If
            ' change by priti BHA/18/08/18-000462 to stop SRN in SRN finder after invoice is crated
            whrCls = whrCls & IIf(chkSRNTrade.Checked, " and TSPL_Bulk_MILK_SRN.formType='Bulk Milk SRN Trade'", " and TSPL_Bulk_MILK_SRN.formType='BulkMilkSrn'") & " and isnull(srn_return_no,'')='' " &
                "and TSPL_Bulk_MILK_SRN.SRN_NO not in (select SRN_NO from tspl_Bulk_milk_purchase_Invoice_Detail) "
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
                    Dim objSRN As BulkMilkSRNXtraRate = frm.arrSrnNo(i)

                    objB = New clsBulkMilkSRN()
                    objGateEntry = New clsGateEntry()
                    If chkSRNTrade.Checked Then
                        objB = clsBulkMilkSRN.getData(objSRN.SRNCode, NavigatorType.Current, True)
                    Else
                        objB = clsBulkMilkSRN.getData(objSRN.SRNCode, NavigatorType.Current)
                        txtSubLocation.Value = objB.Joblocation_Code
                        lblSubLocation.Text = clsLocation.GetName(txtSubLocation.Value, Nothing)
                        chkJobWork.Checked = IIf(objB.IsAgainstJobWork = 1, True, False)
                        objGateEntry = clsGateEntry.getData(objB.Gate_Entry_No, "BulkProc", NavigatorType.Current)
                    End If

                    SRNs = SRNs & objSRN.SRNCode
                    If i <> frm.arrSrnNo.Count - 1 Then
                        SRNs = SRNs & ", "
                    End If

                    fndLocation.Value = objB.Loc_Code
                    lblLocationName.Text = clsLocation.GetName(objB.Loc_Code, Nothing)
                    fndVendor.Value = objB.Vendor_Code
                    lblVendorName.Text = clsVendorMaster.GetName(objB.Vendor_Code, Nothing)
                    txtVendorInvoiceNo.Text = ""

                    'added by priti for check vendor type high class or not BHA/03/07/18-000122
                    intHighClassVendor = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select isHighClass from TSPL_VENDOR_MASTER where Vendor_Code='" & fndVendor.Value & "'"))
                    chkHighClassVendor.Checked = intHighClassVendor
                    '' done by Priti from PanchRaj account '' add condition high class vendor is off with TankerFromMaster richa agarwal 30 July,2019
                    Dim intCount As Integer = 0
                    If TankerFromMaster = 0 Then
                        If True Then
                            gv1.Rows.AddNew()
                            gv1.Rows(i).Cells(colSlNo).Value = (i + 1)
                            gv1.Rows(i).Cells(colTnkrNo).Value = objB.Tanker_No
                            gv1.Rows(i).Cells(colSRNNo).Value = objB.SRN_NO
                            newNetRate = IIf(chkSRNTrade.Checked, objB.Standardrate, clsCommon.myCdbl(clsDBFuncationality.getSingleValue("  select ISNULL ( TSPL_Bulk_MILK_SRN.Approved_Rate,0) + isnull(TSPL_Bulk_MILK_SRN.Incentive,0)+isnull(TSPL_Bulk_MILK_SRN.Deduction,0)-isnull(TSPL_QUALITY_CHECK.DeductionAmount,0) as AppRate   from TSPL_Bulk_MILK_SRN  left outer join TSPL_QUALITY_CHECK on TSPL_QUALITY_CHECK.QC_No=TSPL_Bulk_MILK_SRN.QC_No  where SRN_NO='" & objB.SRN_NO & "'")))

                            gv1.Rows(i).Cells(colSRNDATe).Value = clsCommon.GetPrintDate(objB.SRN_Date, "dd/MM/yyyy hh:mm:ss tt")
                            gv1.Rows(i).Cells(colItemCode).Value = objB.Item_Code
                            gv1.Rows(i).Cells(colHSN).Value = clsItemMaster.GetItemHSNCode(objB.Item_Code, Nothing)
                            gv1.Rows(i).Cells(colItemDesc).Value = objB.Item_Desc
                            gv1.Rows(i).Cells(colUOM).Value = objB.UOM
                            gv1.Rows(i).Cells(colGrossWeight).Value = clsCommon.myFormat(objB.Gross_Weight)
                            gv1.Rows(i).Cells(colTareWeight).Value = clsCommon.myFormat(objB.Tare_Weight)
                            gv1.Rows(i).Cells(colNetWeight).Value = clsCommon.myFormat(objB.Net_Weight)
                            gv1.Rows(i).Cells(colNetRate).Value = clsCommon.myFormat(newNetRate)
                            dt = clsDBFuncationality.GetDataTable("select TSPL_Bulk_MILK_SRN.SRN_NO, (TSPL_Bulk_MILK_SRN.Net_Weight-coalesce(SUM(tdl.invoice_qty),0)) as pending_qty from TSPL_Bulk_MILK_SRN left join ( select invoice_qty,SRN_NO  from Tspl_bulk_milk_purchase_invoice_detail left outer join Tspl_bulk_milk_purchase_invoice_head on Tspl_bulk_milk_purchase_invoice_head.DOC_NO=tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO  where Tspl_bulk_milk_purchase_invoice_head.isPosted=1) as  tdl  on TSPL_Bulk_MILK_SRN.SRN_NO=tdl.srn_no  where TSPL_Bulk_MILK_SRN.isPosted=1 and TSPL_Bulk_MILK_SRN.SRN_NO='" & objB.SRN_NO & "'  group by TSPL_Bulk_MILK_SRN.SRN_NO ,TSPL_Bulk_MILK_SRN.Net_Weight ")
                            gv1.Rows(i).Cells(colPendingQty).Value = clsCommon.myFormat(clsCommon.myCdbl(dt.Rows(0)("pending_qty")))
                            gv1.Rows(i).Cells(colPriceCode).Value = objB.Price_Code
                            gv1.Rows(i).Cells(colQty).Value = clsCommon.myFormat(clsCommon.myCdbl(dt.Rows(0)("pending_qty")))
                            gv1.Rows(i).Cells(colFat).Value = clsCommon.myFormat(objB.fat_per)
                            gv1.Rows(i).Cells(colFatKG).Value = clsCommon.myFormat(objB.fat_KG, False, True, False, 3, True)
                            gv1.Rows(i).Cells(colSNF).Value = clsCommon.myFormat(objB.snf_Per)
                            gv1.Rows(i).Cells(colSNFKG).Value = clsCommon.myFormat(objB.SNF_KG, False, True, False, 3, True)

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
                                fatKG = clsCommon.myCdbl(gv1.Rows(i).Cells(colFatKG).Value)
                                snfKG = clsCommon.myCdbl(gv1.Rows(i).Cells(colSNFKG).Value)
                                If chkSRNTrade.Checked OrElse objB.Net_Weight_Calculate <> 0 Then
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

                                If chkSRNTrade.Checked OrElse objB.Net_Weight_Calculate <> 0 Then
                                    gv1.Rows(i).Cells(colActAmt).Value = objB.Actual_Amount

                                Else
                                    gv1.Rows(i).Cells(colActAmt).Value = clsCommon.myFormat(Math.Round(FATValue + SNfValue, 0))

                                End If

                                ' done by richa 31 July,2019
                                If intHighClassVendor = 0 Then
                                    If objB.Net_Weight_Calculate > 0 Then ''ERO/04/01/19-000454 by balwinder on 07/12/2018
                                        gv1.Rows(i).Cells(colUOM).Value = clsCommon.myCstr(dt.Rows(0)("Total_Solid_Unit_Code"))
                                        gv1.Rows(i).Cells(colGrossWeight).Value = clsCommon.myFormat((objB.Net_Weight_Calculate / objB.Net_Weight) * objB.Gross_Weight)
                                        gv1.Rows(i).Cells(colNetWeight).Value = objB.Net_Weight_Calculate
                                        gv1.Rows(i).Cells(colTareWeight).Value = clsCommon.myFormat(gv1.Rows(i).Cells(colGrossWeight).Value - objB.Net_Weight_Calculate)
                                        gv1.Rows(i).Cells(colPendingQty).Value = objB.Net_Weight_Calculate
                                        gv1.Rows(i).Cells(colQty).Value = objB.Net_Weight_Calculate
                                        gv1.Rows(i).Cells(colNetRate).Value = objB.FinalMilkRate
                                        If ApplyTransportChargeAddInActualAmount = True Then
                                            gv1.Rows(i).Cells(colTranspoterCharge).Value = objB.Transport_Charges
                                            gv1.Rows(i).Cells(colActAmt).Value = objB.Actual_Amount - objB.Transport_Charges
                                        End If
                                    End If
                                Else
                                    ' done by richa 31 July,2019
                                    If intHighClassVendor = 1 Then
                                        If objGateEntry.Arr IsNot Nothing Then
                                            intCount = 1

                                            Dim dblNetWeightCalculate As Decimal = 0.0
                                            If SettCalculateLtrQtyFromKGQtyByCLR _
                                AndAlso clsCommon.CompairString("KG", clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(colUOM).Value)) = CompairStringResult.Equal _
                                AndAlso clsCommon.CompairString("LTR", clsCommon.myCstr(dt.Rows(0)("Total_Solid_Unit_Code"))) = CompairStringResult.Equal Then
                                                dblNetWeightCalculate = Math.Round(clsItemMaster.GetQtyInLtrFromKgByCLR(clsCommon.myCdbl(objGateEntry.Arr(intCount - 1).Chamber_Qty), clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT isnull(CLR_Per ,0) FROM TSPL_Bulk_MILK_SRN where Gate_Entry_No ='" & clsCommon.myCstr(objGateEntry.Arr(intCount - 1).GE_Code) & "'"))), 2)
                                            ElseIf SettApplyCalculateWeightInLtr Then
                                                dblNetWeightCalculate = clsCommon.myCdbl(objGateEntry.Arr(intCount - 1).Chamber_Qty) * clsItemMaster.GetConvertionFactor(clsCommon.myCstr(objGateEntry.Arr(intCount - 1).Item_Code), clsCommon.myCstr(objGateEntry.Arr(intCount - 1).UOM), Nothing)
                                                Dim convFact As Decimal = clsItemMaster.GetConvertionFactor(clsCommon.myCstr(objGateEntry.Arr(intCount - 1).Item_Code), clsCommon.myCstr(dt.Rows(0)("Total_Solid_Unit_Code")), Nothing)
                                                If convFact = 0 Then
                                                    Throw New Exception("Unit [" + clsCommon.myCstr(dt.Rows(0)("Total_Solid_Unit_Code")) + "] is not for item [" + clsCommon.myCstr(objGateEntry.Arr(intCount - 1).Item_Code) + "]")
                                                End If
                                                dblNetWeightCalculate = Math.Round(dblNetWeightCalculate / convFact, 2)
                                            Else
                                                dblNetWeightCalculate = objGateEntry.Arr(intCount - 1).Chamber_Qty
                                            End If


                                            FATRate = objB.fat_Rate
                                            SNFRate = objB.SNF_Rate
                                            FATValue = clsCommon.myFormat((objGateEntry.Arr(intCount - 1).fat_per * dblNetWeightCalculate) / 100, False, True, False, 3, True) * FATRate
                                            SNfValue = clsCommon.myFormat((objGateEntry.Arr(intCount - 1).snf_Per * dblNetWeightCalculate) / 100, False, True, False, 3, True) * SNFRate
                                            gv1.Rows(gv1.Rows.Count - 1).Cells(colUOM).Value = clsCommon.myCstr(dt.Rows(0)("Total_Solid_Unit_Code"))
                                            gv1.Rows(gv1.Rows.Count - 1).Cells(colFat).Value = clsCommon.myFormat(objGateEntry.Arr(intCount - 1).fat_per)
                                            gv1.Rows(gv1.Rows.Count - 1).Cells(colFatKG).Value = clsCommon.myFormat((objGateEntry.Arr(intCount - 1).fat_per * dblNetWeightCalculate) / 100, False, True, False, 3, True)
                                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSNF).Value = clsCommon.myFormat(objGateEntry.Arr(intCount - 1).snf_Per)
                                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFKG).Value = clsCommon.myFormat((objGateEntry.Arr(intCount - 1).snf_Per * dblNetWeightCalculate) / 100, False, True, False, 3, True)
                                            gv1.Rows(gv1.Rows.Count - 1).Cells(colNetWeight).Value = clsCommon.myFormat(dblNetWeightCalculate)
                                            gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = clsCommon.myFormat(dblNetWeightCalculate)

                                            gv1.Rows(i).Cells(colGrossWeight).Value = clsCommon.myFormat((dblNetWeightCalculate / objGateEntry.Arr(intCount - 1).Chamber_Qty) * objGateEntry.Arr(intCount - 1).Chamber_Qty)
                                            gv1.Rows(i).Cells(colNetWeight).Value = dblNetWeightCalculate
                                            gv1.Rows(i).Cells(colTareWeight).Value = clsCommon.myFormat(gv1.Rows(i).Cells(colGrossWeight).Value - dblNetWeightCalculate)
                                            gv1.Rows(i).Cells(colPendingQty).Value = dblNetWeightCalculate
                                            gv1.Rows(i).Cells(colQty).Value = dblNetWeightCalculate
                                            Dim dblFinalMilkRate As Double = 0
                                            If ApplyTransportChargeAddInActualAmount = True Then
                                                gv1.Rows(i).Cells(colTranspoterCharge).Value = objB.Transport_Charges
                                                dblFinalMilkRate = clsCommon.myFormat(Math.Round(clsCommon.myFormat(Math.Round(FATValue + SNfValue, 2)) / dblNetWeightCalculate, 2))
                                            Else
                                                dblFinalMilkRate = clsCommon.myFormat(Math.Round(clsCommon.myFormat(Math.Round(FATValue + SNfValue, 2)) / dblNetWeightCalculate, 2)) + clsCommon.myCdbl(objB.Transport_Charges)
                                            End If

                                            gv1.Rows(i).Cells(colNetRate).Value = dblFinalMilkRate
                                            gv1.Rows(i).Cells(colActAmt).Value = clsCommon.myFormat(FATValue + SNfValue)
                                        Else
                                            intCount = 1

                                            Dim dblNetWeightCalculate As Decimal = 0.0
                                            If SettCalculateLtrQtyFromKGQtyByCLR _
                                                         AndAlso clsCommon.CompairString("KG", gv1.Rows(gv1.Rows.Count - 1).Cells(colUOM).Value) = CompairStringResult.Equal _
                                                        AndAlso clsCommon.CompairString("LTR", clsCommon.myCstr(dt.Rows(0)("Total_Solid_Unit_Code"))) = CompairStringResult.Equal Then
                                                dblNetWeightCalculate = Math.Round(clsItemMaster.GetQtyInLtrFromKgByCLR(clsCommon.myCdbl(objGateEntry.Qty_In_Kg), clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT isnull(CLR_Per ,0) FROM TSPL_Bulk_MILK_SRN where Gate_Entry_No ='" & clsCommon.myCstr(objGateEntry.Gate_Entry_No) & "'"))), 2)
                                            ElseIf SettApplyCalculateWeightInLtr Then
                                                dblNetWeightCalculate = clsCommon.myCdbl(objGateEntry.Qty_In_Kg) * clsItemMaster.GetConvertionFactor(clsCommon.myCstr(objGateEntry.Item_Code), clsCommon.myCstr(objGateEntry.UOM), Nothing)
                                                Dim convFact As Decimal = clsItemMaster.GetConvertionFactor(clsCommon.myCstr(objGateEntry.Item_Code), clsCommon.myCstr(dt.Rows(0)("Total_Solid_Unit_Code")), Nothing)
                                                If convFact = 0 Then
                                                    Throw New Exception("Unit [" + clsCommon.myCstr(dt.Rows(0)("Total_Solid_Unit_Code")) + "] is not for item [" + clsCommon.myCstr(objGateEntry.Arr(intCount - 1).Item_Code) + "]")
                                                End If
                                                dblNetWeightCalculate = Math.Round(dblNetWeightCalculate / convFact, 2)
                                            Else
                                                dblNetWeightCalculate = objGateEntry.Qty_In_Kg
                                            End If

                                            FATRate = objB.fat_Rate
                                            SNFRate = objB.SNF_Rate
                                            FATValue = clsCommon.myFormat((objGateEntry.fat_per * dblNetWeightCalculate) / 100, False, True, False, 3, True) * FATRate
                                            SNfValue = clsCommon.myFormat((objGateEntry.snf_Per * dblNetWeightCalculate) / 100, False, True, False, 3, True) * SNFRate
                                            gv1.Rows(gv1.Rows.Count - 1).Cells(colUOM).Value = clsCommon.myCstr(dt.Rows(0)("Total_Solid_Unit_Code"))
                                            gv1.Rows(gv1.Rows.Count - 1).Cells(colFat).Value = clsCommon.myFormat(objGateEntry.fat_per)
                                            gv1.Rows(gv1.Rows.Count - 1).Cells(colFatKG).Value = clsCommon.myFormat((objGateEntry.fat_per * dblNetWeightCalculate) / 100, False, True, False, 3, True)
                                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSNF).Value = clsCommon.myFormat(objGateEntry.snf_Per)
                                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFKG).Value = clsCommon.myFormat((objGateEntry.snf_Per * dblNetWeightCalculate) / 100, False, True, False, 3, True)
                                            gv1.Rows(gv1.Rows.Count - 1).Cells(colNetWeight).Value = clsCommon.myFormat(dblNetWeightCalculate)
                                            gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = clsCommon.myFormat(dblNetWeightCalculate)

                                            gv1.Rows(i).Cells(colGrossWeight).Value = clsCommon.myFormat((dblNetWeightCalculate / objGateEntry.Qty_In_Kg) * objGateEntry.Qty_In_Kg)
                                            gv1.Rows(i).Cells(colNetWeight).Value = dblNetWeightCalculate
                                            gv1.Rows(i).Cells(colTareWeight).Value = clsCommon.myFormat(gv1.Rows(i).Cells(colGrossWeight).Value - dblNetWeightCalculate)
                                            gv1.Rows(i).Cells(colPendingQty).Value = dblNetWeightCalculate
                                            gv1.Rows(i).Cells(colQty).Value = dblNetWeightCalculate
                                            Dim dblFinalMilkRate As Double = 0
                                            If ApplyTransportChargeAddInActualAmount = True Then
                                                gv1.Rows(i).Cells(colTranspoterCharge).Value = objB.Transport_Charges
                                                dblFinalMilkRate = clsCommon.myFormat(Math.Round(clsCommon.myFormat(Math.Round(FATValue + SNfValue, 2)) / dblNetWeightCalculate, 2))
                                            Else
                                                dblFinalMilkRate = clsCommon.myFormat(Math.Round(clsCommon.myFormat(Math.Round(FATValue + SNfValue, 2)) / dblNetWeightCalculate, 2)) + clsCommon.myCdbl(objB.Transport_Charges)
                                            End If

                                            gv1.Rows(i).Cells(colNetRate).Value = dblFinalMilkRate
                                            gv1.Rows(i).Cells(colActAmt).Value = clsCommon.myFormat(FATValue + SNfValue)
                                        End If
                                    End If
                                End If
                                gv1.Rows(i).Cells(colFatRate).Value = clsCommon.myFormat(FATRate)
                                gv1.Rows(i).Cells(colSNFRate).Value = clsCommon.myFormat(SNFRate)
                                If AllowTruncateAmount Then ''BM00000010118
                                    Dim xNewAmt As Double = clsCommon.myCdbl(FATValue + SNfValue)
                                    If chkSRNTrade.Checked OrElse objB.Net_Weight_Calculate <> 0 Then
                                        xNewAmt = objB.Actual_Amount
                                    End If
                                    If clsCommon.myLen(xNewAmt) > 0 AndAlso clsCommon.myCstr(xNewAmt).Contains(".") Then
                                        xNewAmt = clsCommon.myCstr(xNewAmt).Substring(0, clsCommon.myCstr(xNewAmt).IndexOf("."))
                                    End If
                                    gv1.Rows(i).Cells(colActAmt).Value = CInt(xNewAmt)
                                End If
                            End If
                        End If
                    Else
                        If True Then
                            For Each objTr As clsBulkMilkSRNChemberNoDetails In objB.Arr
                                gv1.Rows.AddNew()
                                intCount = intCount + 1
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colSlNo).Value = intCount
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colTnkrNo).Value = objB.Tanker_No
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colSRNNo).Value = objB.SRN_NO
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colSRNDATe).Value = clsCommon.GetPrintDate(objB.SRN_Date, "dd/MM/yyyy hh:mm:ss tt")
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colChamberDesc).Value = objTr.Chamber_Desc
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value = objTr.Item_Code
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colItemDesc).Value = clsIntimation.getItemName(objTr.Item_Code, Nothing)
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colHSN).Value = clsItemMaster.GetItemHSNCode(objTr.Item_Code, Nothing)
                                Dim ConvRatio As Decimal = 1
                                If clsCommon.myLen(objTr.UOM_Calculate) > 0 AndAlso objTr.Net_Weight_Calculate > 0 Then
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUOM).Value = objTr.UOM_Calculate
                                    ConvRatio = objTr.Net_Weight_Calculate / objTr.Net_Weight
                                Else
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUOM).Value = objTr.UOM
                                End If
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colGrossWeight).Value = clsCommon.myFormat(objTr.Gross_Weight * ConvRatio)
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colTareWeight).Value = clsCommon.myFormat(objTr.Tare_Weight * ConvRatio)
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colNetWeight).Value = clsCommon.myFormat(objTr.Net_Weight * ConvRatio)

                                gv1.Rows(gv1.Rows.Count - 1).Cells(colNetRate).Value = clsCommon.myFormat(objTr.FinalMilkRate)
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = clsCommon.myFormat(objTr.Net_Weight * ConvRatio)
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceCode).Value = objTr.Price_Code
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colFatRate).Value = clsCommon.myFormat(objTr.fat_Rate)
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFRate).Value = clsCommon.myFormat(objTr.SNF_Rate)
                                If intHighClassVendor = 0 Then
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colFat).Value = clsCommon.myFormat(objTr.fat_per)
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colFatKG).Value = clsCommon.myFormat(objTr.fat_KG, False, True, False, 3, True)
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSNF).Value = clsCommon.myFormat(objTr.snf_Per)
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFKG).Value = clsCommon.myFormat(objTr.SNF_KG, False, True, False, 3, True)
                                    If chkSRNTrade.Checked Then
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colActAmt).Value = objTr.Actual_Amount
                                    Else
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colActAmt).Value = clsCommon.myFormat(Math.Round(objTr.Actual_Amount, 0))
                                    End If
                                Else
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colFat).Value = clsCommon.myFormat(objGateEntry.Arr(intCount - 1).fat_per)
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colFatKG).Value = clsCommon.myFormat((objGateEntry.Arr(intCount - 1).fat_per * objGateEntry.Arr(intCount - 1).Chamber_Qty) / 100, False, True, False, 3, True)
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSNF).Value = clsCommon.myFormat(objGateEntry.Arr(intCount - 1).snf_Per)
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFKG).Value = clsCommon.myFormat((objGateEntry.Arr(intCount - 1).snf_Per * objGateEntry.Arr(intCount - 1).Chamber_Qty) / 100, False, True, False, 3, True)
                                    If chkSRNTrade.Checked Then
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colActAmt).Value = objTr.Actual_Amount
                                    Else
                                        Dim fatKG As Double = gv1.Rows(gv1.Rows.Count - 1).Cells(colFatKG).Value
                                        Dim FATRate As Double = gv1.Rows(gv1.Rows.Count - 1).Cells(colFatRate).Value
                                        Dim SNFKG As Double = gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFKG).Value
                                        Dim SNFRate As Double = gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFRate).Value
                                        Dim FATValue As Double = MyMath.RoundDown(fatKG * FATRate, 2)
                                        Dim SNfValue As Double = MyMath.RoundDown(SNFKG * SNFRate, 2)
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colActAmt).Value = clsCommon.myFormat(Math.Round(FATValue + SNfValue, 0))
                                    End If
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colNetWeight).Value = clsCommon.myFormat(objGateEntry.Arr(intCount - 1).Chamber_Qty)
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = clsCommon.myFormat(objGateEntry.Arr(intCount - 1).Chamber_Qty)
                                End If
                                If objSRN.XtraRate <> 0 Then
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colXtraRate).Value = objSRN.XtraRate
                                    Dim dclRate As Decimal = objTr.NetRate + objSRN.XtraRate
                                    Dim qry As String = "select Fat_Weightage,Snf_Weightage,Fat_Percentage,Snf_Percentage from TSPL_BULK_PRICE_DETAIL_ITEM_WISE where Price_Code='" + objTr.Price_Code + "' and Item_code='" + objTr.Item_Code + "'"
                                    Dim dtPrice As DataTable = clsDBFuncationality.GetDataTable(qry)
                                    If dtPrice IsNot Nothing AndAlso dtPrice.Rows.Count > 0 Then
                                        Dim FATRate As Decimal = MyMath.RoundDown(dclRate * clsCommon.myCdbl(dtPrice.Rows(0)("Fat_Weightage")) / clsCommon.myCdbl(dtPrice.Rows(0)("Fat_Percentage")), 2)
                                        Dim SNFRate As Decimal = MyMath.RoundDown(dclRate * clsCommon.myCdbl(dtPrice.Rows(0)("Snf_Weightage")) / clsCommon.myCdbl(dtPrice.Rows(0)("Snf_Percentage")), 2)
                                        Dim FATValue As Decimal = MyMath.RoundDown(gv1.Rows(gv1.Rows.Count - 1).Cells(colFatKG).Value * FATRate, SettBulkMilkFATSNFAmtDecimalPlaces)
                                        Dim SNfValue As Decimal = MyMath.RoundDown(gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFKG).Value * SNFRate, SettBulkMilkFATSNFAmtDecimalPlaces)

                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFatRate).Value = clsCommon.myFormat(MyMath.RoundDown(FATRate, 2))
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFRate).Value = clsCommon.myFormat(MyMath.RoundDown(SNFRate, 2))
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colActAmt).Value = clsCommon.myFormat(Math.Round(FATValue + SNfValue, 0))
                                        objTr.Actual_Amount = clsCommon.myCdbl(gv1.Rows(gv1.Rows.Count - 1).Cells(colActAmt).Value)
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colNetRate).Value = clsCommon.myFormat(clsCommon.myCDivide(objTr.Actual_Amount, clsCommon.myCdbl(gv1.Rows(gv1.Rows.Count - 1).Cells(colNetWeight).Value)))
                                    End If
                                End If
                                If AllowTruncateAmount Then
                                    Dim xNewAmt As Double = objTr.Actual_Amount
                                    If clsCommon.myLen(xNewAmt) > 0 AndAlso clsCommon.myCstr(xNewAmt).Contains(".") Then
                                        xNewAmt = clsCommon.myCstr(xNewAmt).Substring(0, clsCommon.myCstr(xNewAmt).IndexOf("."))
                                    End If
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colActAmt).Value = CInt(xNewAmt)
                                End If
                            Next
                        End If
                    End If
                Next
                fndSRNNo.Value = SRNs
                If settApplyTCSTax Then
                    SetTax()
                End If
                UpdateAllTotals()
                ''richa BHA/25/04/19-000868
                SetVendorTDSDetails()

            Else
                fndSRNNo.Value = ""
                loadBlankGrid()
            End If
        End If
        isLoadData = False
        ActualTCSTaxBaseAmt()
    End Sub
    Private Sub SetTax()
        txtTaxGroup.Value = clsLocationWiseTax.GetDefaultTaxGroup(fndLocation.Value, fndVendor.Value, "P", dtpDocDate.Value)
        lblTaxGrpName.Text = clsTaxGroupMaster.GetNameOfPurchaseType(txtTaxGroup.Value, Nothing)
        SetTaxDetails()
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
        chkHighClassVendor.Checked = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select isnull (isHighClass,0) from TSPL_VENDOR_MASTER  where Vendor_Code = '" + fndVendor.Value + "' "))
        If TankerFromMaster = 1 AndAlso clsCommon.myLen(fndVendor.Value) > 0 Then
            SetToDate()
        End If
        intHighClassVendor = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select isHighClass from TSPL_VENDOR_MASTER where Vendor_Code='" & fndVendor.Value & "'"))
        SetVendorTDSDetails()
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
                    clsCommon.MyMessageBoxShow(Me, "Invalid date.Date should be multiple of " & clsCommon.myCstr(PaymentCycleValue) & " + 1 ")
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
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
            If MyBase.isReverse Then
                Dim frm As New FrmPWD(Nothing)
                frm.strType = "SIRC"
                frm.strCode = "SIReversAndCreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnReverse.Visible = True
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
                'MessageBox.Show("You are not authorized to perform this action.", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        ElseIf e.Alt AndAlso e.Control AndAlso e.Shift AndAlso e.KeyCode = Keys.F6 Then
                If clsCommon.myLen(fndDocNo.Value) > 0 Then
                If btnPost.Enabled = False Then
                    TxtVendorUpdate.Visible = True
                    TxtVendorUpdate.Enabled = True
                    btnUpdateVendor.Visible = True
                    btnUpdateVendor.Enabled = True
                Else
                    clsCommon.MyMessageBoxShow(Me, "Please post the Invoice first", Me.Text)
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "Please select Invoice No first", Me.Text)
            End If
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
    Private Sub UpdateAllTotals()
        Dim totQty As Double = 0
        Dim totFatKg As Double = 0
        Dim totSnfKg As Double = 0

        Dim dblTotAmt As Double = 0
        Dim dblAmtAfterDis As Double = 0


        Dim dblTaxBaseAmt1 As Double = 0
        Dim dblTaxBaseAmt2 As Double = 0
        Dim dblTaxBaseAmt3 As Double = 0
        Dim dblTaxBaseAmt4 As Double = 0
        Dim dblTaxBaseAmt5 As Double = 0
        Dim dblTaxBaseAmt6 As Double = 0
        Dim dblTaxBaseAmt7 As Double = 0
        Dim dblTaxBaseAmt8 As Double = 0
        Dim dblTaxBaseAmt9 As Double = 0
        Dim dblTaxBaseAmt10 As Double = 0

        Dim dblTaxAmt1 As Double = 0
        Dim dblTaxAmt2 As Double = 0
        Dim dblTaxAmt3 As Double = 0
        Dim dblTaxAmt4 As Double = 0
        Dim dblTaxAmt5 As Double = 0
        Dim dblTaxAmt6 As Double = 0
        Dim dblTaxAmt7 As Double = 0
        Dim dblTaxAmt8 As Double = 0
        Dim dblTaxAmt9 As Double = 0
        Dim dblTaxAmt10 As Double = 0


        Dim dblTaxTotAmt As Double = 0
        Dim dblNetAmt As Double = 0
        Dim dblTransporterChargeAmt As Double = 0
        For ii As Integer = 0 To gv1.Rows.Count - 1
            If (clsCommon.myLen(gv1.Rows(ii).Cells(colItemCode).Value) > 0) Then
                totQty = totQty + clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
                totFatKg = totFatKg + clsCommon.myCdbl(gv1.Rows(ii).Cells(colFatKG).Value)
                totSnfKg = totSnfKg + clsCommon.myCdbl(gv1.Rows(ii).Cells(colSNFKG).Value)
                dblTotAmt = dblTotAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colActAmt).Value)
                dblTransporterChargeAmt = dblTransporterChargeAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTranspoterCharge).Value)
                dblTaxAmt1 = dblTaxAmt1 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt1).Value)
                dblTaxAmt2 = dblTaxAmt2 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt2).Value)
                dblTaxAmt3 = dblTaxAmt3 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt3).Value)
                dblTaxAmt4 = dblTaxAmt4 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt4).Value)
                dblTaxAmt5 = dblTaxAmt5 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt5).Value)
                dblTaxAmt6 = dblTaxAmt6 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt6).Value)
                dblTaxAmt7 = dblTaxAmt7 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt7).Value)
                dblTaxAmt8 = dblTaxAmt8 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt8).Value)
                dblTaxAmt9 = dblTaxAmt9 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt9).Value)
                dblTaxAmt10 = dblTaxAmt10 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt10).Value)

                dblTaxBaseAmt1 = dblTaxBaseAmt1 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt1).Value)
                dblTaxBaseAmt2 = dblTaxBaseAmt2 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt2).Value)
                dblTaxBaseAmt3 = dblTaxBaseAmt3 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt3).Value)
                dblTaxBaseAmt4 = dblTaxBaseAmt4 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt4).Value)
                dblTaxBaseAmt5 = dblTaxBaseAmt5 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt5).Value)
                dblTaxBaseAmt6 = dblTaxBaseAmt6 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt6).Value)
                dblTaxBaseAmt7 = dblTaxBaseAmt7 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt7).Value)
                dblTaxBaseAmt8 = dblTaxBaseAmt8 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt8).Value)
                dblTaxBaseAmt9 = dblTaxBaseAmt9 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt9).Value)
                dblTaxBaseAmt10 = dblTaxBaseAmt10 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt10).Value)

                dblTaxTotAmt = dblTaxTotAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTotTaxAmt).Value)
                dblNetAmt = dblNetAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colItemNetAmt).Value)
            End If
        Next


        If rbtnTaxCalAutomatic.IsChecked Then
            For ii As Integer = 1 To gv2.Rows.Count
                Select Case (ii)
                    Case 1
                        If (clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_TAX_MASTER WHERE Tax_Code='" & clsCommon.myCstr(gv2.Rows(ii - 1).Cells(colTTaxAutCode).Value) & "' AND Is_TCS ='Y'")) > 0) AndAlso clsCommon.myCdbl(txttcstaxbaseamount.Value) > 0 AndAlso AllowtoChangeTCSBaseAmountPurchase = True Then
                            'lblActualTCSTaxBaseAmt.Text = clsCommon.myFormat(dblTaxBaseAmt1)
                            dblTaxBaseAmt1 = clsCommon.myCdbl(txttcstaxbaseamount.Value)
                            dblTaxAmt1 = (dblTaxBaseAmt1 * txtTCSTaxRate.Value) / 100
                        End If
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt1, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt1, 2)
                        If dblTaxBaseAmt1 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt1 * 100) / dblTaxBaseAmt1, 3)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                    Case 2
                        If (clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_TAX_MASTER WHERE Tax_Code='" & clsCommon.myCstr(gv2.Rows(ii - 1).Cells(colTTaxAutCode).Value) & "' AND Is_TCS ='Y'")) > 0) AndAlso clsCommon.myCdbl(txttcstaxbaseamount.Value) > 0 AndAlso AllowtoChangeTCSBaseAmountPurchase = True Then
                            'lblActualTCSTaxBaseAmt.Text = clsCommon.myFormat(dblTaxBaseAmt1 + dblTaxAmt1)
                            dblTaxBaseAmt2 = clsCommon.myCdbl(txttcstaxbaseamount.Value)
                            dblTaxAmt2 = (dblTaxBaseAmt2 * txtTCSTaxRate.Value) / 100
                        End If
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt2, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt2, 2)
                        If dblTaxBaseAmt2 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt2 * 100) / dblTaxBaseAmt2, 3)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                    Case 3
                        If (clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_TAX_MASTER WHERE Tax_Code='" & clsCommon.myCstr(gv2.Rows(ii - 1).Cells(colTTaxAutCode).Value) & "' AND Is_TCS ='Y'")) > 0) AndAlso clsCommon.myCdbl(txttcstaxbaseamount.Value) > 0 AndAlso AllowtoChangeTCSBaseAmountPurchase = True Then
                            'lblActualTCSTaxBaseAmt.Text = clsCommon.myFormat(dblTaxBaseAmt1 + dblTaxAmt1 + dblTaxAmt2)
                            dblTaxBaseAmt3 = clsCommon.myCdbl(txttcstaxbaseamount.Value)
                            dblTaxAmt3 = (dblTaxBaseAmt3 * txtTCSTaxRate.Value) / 100
                        End If
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt3, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt3, 2)
                        If dblTaxBaseAmt3 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt3 * 100) / dblTaxBaseAmt3, 3)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                    Case 4
                        If (clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_TAX_MASTER WHERE Tax_Code='" & clsCommon.myCstr(gv2.Rows(ii - 1).Cells(colTTaxAutCode).Value) & "' AND Is_TCS ='Y'")) > 0) AndAlso clsCommon.myCdbl(txttcstaxbaseamount.Value) > 0 AndAlso AllowtoChangeTCSBaseAmountPurchase = True Then
                            'lblActualTCSTaxBaseAmt.Text = clsCommon.myFormat(dblTaxBaseAmt1 + dblTaxAmt1 + dblTaxAmt2 + dblTaxAmt3)
                            dblTaxBaseAmt4 = clsCommon.myCdbl(txttcstaxbaseamount.Value)
                            dblTaxAmt4 = (dblTaxBaseAmt4 * txtTCSTaxRate.Value) / 100
                        End If
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt4, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt4, 2)
                        If dblTaxBaseAmt4 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt4 * 100) / dblTaxBaseAmt4, 3)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                    Case 5
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt5, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt5, 2)
                        If dblTaxBaseAmt5 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt5 * 100) / dblTaxBaseAmt5, 3)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                    Case 6
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt6, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt6, 2)
                        If dblTaxBaseAmt6 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt6 * 100) / dblTaxBaseAmt6, 3)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                    Case 7
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt7, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt7, 2)
                        If dblTaxBaseAmt7 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt7 * 100) / dblTaxBaseAmt7, 3)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                    Case 8
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt8, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt8, 2)
                        If dblTaxBaseAmt8 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt8 * 100) / dblTaxBaseAmt8, 3)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                    Case 9
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt9, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt9, 2)
                        If dblTaxBaseAmt9 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt9 * 100) / dblTaxBaseAmt9, 3)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                    Case 10
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt10, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt10, 2)
                        If dblTaxBaseAmt10 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt10 * 100) / dblTaxBaseAmt10, 3)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                End Select
            Next
        Else
            For ii As Integer = 1 To gv2.Rows.Count
                gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblAmtAfterDis, 2)
            Next
        End If
        dblTotAmt = Math.Round(dblTotAmt, 2)
        dblTaxTotAmt = Math.Round(dblTaxTotAmt, 2)

        txtTotalQty.Text = clsCommon.myFormat(clsCommon.myCstr(Math.Round(totQty, 2)))
        txtTotalFatKg.Text = clsCommon.myFormat(clsCommon.myCstr(MyMath.RoundDown(totFatKg, 3)), False, True, False, 3, True)
        txtTotalSNFKg.Text = clsCommon.myFormat(clsCommon.myCstr(MyMath.RoundDown(totSnfKg, 3)), False, True, False, 3, True)

        txtTaxableAmt.Text = clsCommon.myFormat(dblTotAmt)
        txtTaxAmt.Text = clsCommon.myFormat(dblTaxTotAmt)

        dblNetAmt = dblTotAmt + dblTaxTotAmt + dblTransporterChargeAmt
        Dim intPart As Double = Math.Round(dblNetAmt, 0)
        Dim roundOffAmt As Double = -(dblNetAmt - intPart)
        txtTotalAmt.Text = clsCommon.myFormat(clsCommon.myFormat(intPart))
        txtRoundOffAmt.Text = clsCommon.myFormat(Math.Round(roundOffAmt, 2))
        txtTransporterCharge.Text = clsCommon.myFormat(dblTransporterChargeAmt)

        UpdateTDSAmountValue()
    End Sub
    Private Sub gvItem_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        If isSRNselected Then
            If Not isLoadData Then
                If gv1.CurrentRow.Cells(colPendingQty).Value < gv1.CurrentRow.Cells(colQty).Value Then
                    'clsCommon.MyMessageBoxShow("Invoice Qty can't be more than pending Qty")
                    'gv1.CurrentRow.Cells(colQty).Value = clsCommon.myFormat(gv1.CurrentRow.Cells(colPendingQty).Value)
                    'Exit Sub
                End If

                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(colQty) Then
                        gv1.CurrentRow.Cells(colFatKG).Value = clsCommon.myFormat(MyMath.RoundDown(gv1.CurrentRow.Cells(colFat).Value * clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value), 3) / 100, False, True, False, 3, True)
                        gv1.CurrentRow.Cells(colSNFKG).Value = clsCommon.myFormat(MyMath.RoundDown(gv1.CurrentRow.Cells(colSNF).Value * clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value), 3) / 100, False, True, False, 3, True)
                        Dim objb As clsBulkMilkSRN = clsBulkMilkSRN.getData(gv1.CurrentRow.Cells(colSRNNo).Value, NavigatorType.Current)
                        'gv1.CurrentRow.Cells(colDeduc).Value = getDeduction(objb.QC_No, objb.SRN_Date) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
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
                            StdRate = clsCommon.myCdbl(gv1.CurrentRow.Cells(colNetRate).Value)
                            fatKG = clsCommon.myCdbl(gv1.Rows(0).Cells(colFatKG).Value)
                            snfKG = clsCommon.myCdbl(gv1.Rows(0).Cells(colSNFKG).Value)
                            FATRate = MyMath.RoundDown(StdRate * FatW / FATRatio, 2)
                            SNFRate = MyMath.RoundDown(StdRate * SNfW / SNFRatio, 2)
                            FATValue = MyMath.RoundDown(fatKG * FATRate, 2)
                            SNfValue = MyMath.RoundDown(snfKG * SNFRate, 2)
                            gv1.CurrentRow.Cells(colFatRate).Value = clsCommon.myFormat(MyMath.RoundDown(FATRate, 2))
                            gv1.CurrentRow.Cells(colSNFRate).Value = clsCommon.myFormat(MyMath.RoundDown(SNFRate, 2))
                            gv1.CurrentRow.Cells(colActAmt).Value = clsCommon.myFormat(Math.Round(FATValue + SNfValue, 0))

                            If AllowTruncateAmount Then ''BM00000010118
                                Dim xNewAmt As Double = clsCommon.myCdbl(FATValue + SNfValue)
                                If clsCommon.myLen(xNewAmt) > 0 AndAlso clsCommon.myCstr(xNewAmt).Contains(".") Then
                                    xNewAmt = clsCommon.myCstr(xNewAmt).Substring(0, clsCommon.myCstr(xNewAmt).IndexOf("."))
                                End If
                                gv1.CurrentRow.Cells(colActAmt).Value = CInt(xNewAmt)
                            End If

                            'gv1.CurrentRow.Cells(colActAmt).Value = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colNetRate).Value)
                        End If
                    End If

                    'If e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colFatKG) OrElse e.Column Is gv1.Columns(colSNFKG) OrElse e.Column Is gv1.Columns(colActAmt) Then
                    '    UpdateAllTotals()
                    'End If
                End If
                isCellValueChangedOpen = False
            End If
        End If
    End Sub
    Private Sub gvItem_UserDeletedRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserDeletedRow
        UpdateAllTotals()
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
                    clsCommon.MyMessageBoxShow(Me, "Please enter Vendor Invoice No.", Me.Text)
                Else
                    fndVendor.Value = ""
                    If clsCommon.myLen(txtVendorInvoiceNo.Text) > 0 Then
                        If clsDBFuncationality.getSingleValue("Select count(*) from tspl_Bulk_milk_purchase_Invoice_head where DOC_NO<>'" & fndDocNo.Value & "' and  Vendor_Invoice_No ='" & txtVendorInvoiceNo.Text & "'") > 1 Then
                            clsCommon.MyMessageBoxShow(Me, "Duplicate Vendor Invoice No.,Please enter different vendor invoice no")
                        Else
                            If UpdateVendorAfterPosting() Then
                                clsCommon.MyMessageBoxShow(Me, "Vendor updated successfully.", Me.Text)
                                TxtVendorUpdate.Value = ""
                                TxtVendorUpdate.Visible = False
                                btnUpdateVendor.Enabled = False
                                btnUpdateVendor.Visible = False
                                loadData(fndDocNo.Value, NavigatorType.Current)
                            End If
                        End If
                    Else
                        If UpdateVendorAfterPosting() Then
                            clsCommon.MyMessageBoxShow(Me, "Vendor updated successfully.", Me.Text)
                            TxtVendorUpdate.Value = ""
                            TxtVendorUpdate.Visible = False
                            btnUpdateVendor.Enabled = False
                            btnUpdateVendor.Visible = False
                            loadData(fndDocNo.Value, NavigatorType.Current)
                        End If
                    End If



                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "Please Select Vendor first.", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Function UpdateVendorAfterPosting() As Boolean
        Dim strAPInvoiceNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Document_No  from TSPL_VENDOR_INVOICE_HEAD where Against_BulkMillkPurchaseInvoice_No ='" & fndDocNo.Value & "' "))
        Dim strPaymentNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_PAYMENT_DETAIL.Payment_No from TSPL_PAYMENT_DETAIL left outer join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No =TSPL_PAYMENT_DETAIL.Payment_No where TSPL_PAYMENT_DETAIL.Document_No='" & strAPInvoiceNo & "' and TSPL_PAYMENT_HEADER .IsChkReverse='N'"))
        Dim strInvoiceNo As String = clsCommon.myCstr(fndDocNo.Value)
        Dim strVendorCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Vendor_Code  from TSPL_VENDOR_INVOICE_HEAD  where Document_No='" & strAPInvoiceNo & "'"))
        Dim strVendorName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Vendor_Name  from TSPL_VENDOR_INVOICE_HEAD  where Document_No='" & strAPInvoiceNo & "'"))
        Dim strDescription As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select description from TSPL_VENDOR_INVOICE_HEAD  where Document_No='" & strAPInvoiceNo & "'"))
        strDescription = strDescription.Replace(strVendorCode, TxtVendorUpdate.Value)
        strDescription = strDescription.Replace(strVendorName, lblVendorName.Text)

        If clsCommon.myLen(strPaymentNo) > 0 Then
            clsCommon.MyMessageBoxShow(Me, "Vendor cannot be updated because Payment has been created for this invoice " & strInvoiceNo)
            Return False
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

            'Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
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
                    clsCommon.MyMessageBoxShow(Me, "Information updated successfully.", Me.Text)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Private Sub btnBillOfSupply_Click(sender As Object, e As EventArgs) Handles btnBillOfSupply.Click
        If clsCommon.myLen(fndDocNo.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "No data found to print", Me.Text)
        Else
            ' Dim strQuery As String = "select  GSTRegistered from TSPL_VENDOR_MASTER where vendor_code='" + fndVendor.Value + "' "
            Dim isVendorRegister As Boolean = clsDBFuncationality.getSingleValue("select  GSTRegistered from TSPL_VENDOR_MASTER where vendor_code='" + fndVendor.Value + "' ")
            If isVendorRegister = False Then
                Dim strPurchaseTaxInvoiceNo As String = clsDBFuncationality.getSingleValue(" select isnull(TSPL_BULK_MILK_PURCHASE_INVOICE_head.Purchase_Tax_Invoice,'') as Purchase_Tax_Invoice from TSPL_BULK_MILK_PURCHASE_INVOICE_head where TSPL_BULK_MILK_PURCHASE_INVOICE_head.DOC_NO = '" + fndDocNo.Value + "'  ")
                If clsCommon.myLen(strPurchaseTaxInvoiceNo) > 0 Then
                    printDataForBillOfSupply()
                Else
                    clsCommon.MyMessageBoxShow(Me, "No data found to print", Me.Text)
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to print", Me.Text)
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
                strQuery += " +  case when isnull(TSPL_Weighment_Detail.Net_Weight,'')>0 then TSPL_Weighment_Detail.Net_Weight else TSPL_BULK_MILK_PURCHASE_INVOICE_detail.Invoice_Qty  end as Milk_qty , 'For  FAT' +convert(varchar,TSPL_Bulk_Price_MASTER.Fat_Percentage) + ' % &  SNF' + convert(varchar,TSPL_Bulk_Price_MASTER.Snf_Percentage)+ ' %' as 'MilkRate' , 'For ' +convert(varchar,TSPL_Bulk_Price_MASTER.Fat_Weightage ) + ' & ' +  convert(varchar,TSPL_Bulk_Price_MASTER.Snf_Weightage ) as 'Weightage',tspl_Bulk_milk_purchase_Invoice_Detail.NetRate,tspl_Bulk_milk_purchase_Invoice_Detail.Actual_Amount,tspl_Bulk_milk_purchase_Invoice_head.RoundOffAmount ,(BulkMilkSrn.Incentive+BulkMilkSrn.Deduction-BulkMilkSrn.SpecialDeduction)as Ded_Inc,case when isnull(BulkMilkSrn.BasicRate,0)>0 then BulkMilkSrn.BasicRate else TSPL_BULK_MILK_PURCHASE_INVOICE_detail.NetRate end as BasicRate ,BulkMilkSrn.Fat_KG,BulkMilkSrn.SNF_KG " &
                                   " from tspl_Bulk_milk_purchase_Invoice_Detail  left outer join tspl_Bulk_milk_purchase_Invoice_head on tspl_Bulk_milk_purchase_Invoice_head.DOC_NO =tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code =tspl_Bulk_milk_purchase_Invoice_Detail.Item_Code   left outer join TSPL_COMPANY_MASTER  on TSPL_COMPANY_MASTER.Comp_Code =tspl_Bulk_milk_purchase_Invoice_head.Comp_Code   left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER .Location_Code =tspl_Bulk_milk_purchase_Invoice_head.loc_code  left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER .Vendor_Code =tspl_Bulk_milk_purchase_Invoice_head.vendor_code  left join TSPL_VENDOR_BANK_MASTER on TSPL_VENDOR_MASTER.BANK_CODE=TSPL_VENDOR_BANK_MASTER.BANK_CODE left outer join BulkMilkSrn  on BulkMilkSrn.SRN_NO =tspl_Bulk_milk_purchase_Invoice_Detail.SRN_NO   left outer join Tspl_Gate_Entry_Details on Tspl_Gate_Entry_Details.Gate_Entry_No =BulkMilkSrn.Gate_Entry_No  Left Outer Join (Select TSPL_QC_Parameter_Detail.* From BulkMilkSrn Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No  =        BulkMilkSrn.QC_No  And        TSPL_QC_Parameter_Detail.Param_Type = 'FAT') t_FAT      On t_FAT.QC_No   = BulkMilkSrn.QC_No  Left Outer Join (Select TSPL_QC_Parameter_Detail.* From BulkMilkSrn Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No  =        BulkMilkSrn.QC_No  And        TSPL_QC_Parameter_Detail.Param_Type = 'SNF') t_SNF      On t_SNF .QC_No   = BulkMilkSrn.QC_No  left outer join TSPL_Bulk_Price_MASTER on TSPL_Bulk_Price_MASTER.Price_Code=tspl_Bulk_milk_purchase_Invoice_Detail.Price_code  left outer join TSPL_Weighment_Detail on TSPL_Weighment_Detail.Weighment_No =BulkMilkSrn.Weighment_No  left outer join TSPL_STATE_MASTER on TSPL_VENDOR_MASTER.State_Code = TSPL_STATE_MASTER.STATE_CODE left outer join tspl_state_master as tspl_state_master_for_location_state on  tspl_state_master_for_location_state.state_code=tspl_location_master.state  where tspl_bulk_milk_purchase_invoice_head.DOC_NO in ('" & fndDocNo.Value & "') order by Vendor_name,Date_And_Time"
            Else
                Dim frm As New RptBulkMilkMultiplePurchaseInvoice
                strQuery = GetQueryMuliPI("", "", fndDocNo.Value)
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQuery)
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "rptBulkMilkPurchaseInvoice_Bill_of_Supply", "Purchase Invoice", clsCommon.myCDate(dtpDocDate.Value))
            frmCRV = Nothing
        Else
            clsCommon.MyMessageBoxShow(Me, "Please select an invoice to print", Me.Text)
        End If
    End Sub
    Function GetQueryMuliPI(ByVal frmDate As String, ByVal ToDate As String, ByVal InvoiceNo As String) As String
        Dim qry = " ;with BulkMilkSRN as (select * from tspl_bulk_milk_srn where isnull(TSPL_Bulk_MILK_SRN.srn_return_no,'')=''  ) " &
               "select tspl_bulk_milk_purchase_invoice_head.EWayBillNo,convert(varchar,tspl_bulk_milk_purchase_invoice_head.EWayBillDate,103) as EWayBillDate,tspl_bulk_milk_purchase_invoice_head.Electronic_Ref_No, isnull(TSPL_BULK_MILK_PURCHASE_INVOICE_head.Purchase_Tax_Invoice,'') as Purchase_Tax_Invoice,isnull(TSPL_BULK_MILK_PURCHASE_INVOICE_head.Purchase_Tax_Invoice_Type,'') as Purchase_Tax_Invoice_Type ,  TSPL_ITEM_MASTER.HSN_Code,tspl_state_master_for_location_state.GST_STATE_Code as LOC_GST_State_Code,TSPL_LOCATION_MASTER.GSTNO as Loc_GstInNo ,TSPL_VENDOR_MASTER.GSTFinalNo AS Vendor_GSTIN_NO,TSPL_STATE_MASTER.GST_STATE_Code AS Vendor_GST_StateCode,   Tspl_Gate_Entry_Details.Gate_Entry_No,convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,103) as Date_And_Time, " &
               "'" & frmDate & "' as frmDate,'" & ToDate & "' as ToDate, CONVERT(VARCHAR(15),tspl_Bulk_milk_purchase_Invoice_head.SRN_From_Date,103) AS From_Date,convert(varchar(15),tspl_Bulk_milk_purchase_Invoice_head.SRN_TO_Date,103) as To_Date, TSPL_LOCATION_MASTER.add1 as Loc_Add1,tspl_Bulk_milk_purchase_Invoice_head.Loc_Code , " &
               "TSPL_LOCATION_MASTER.Location_Desc ,TSPL_VENDOR_MASTER.Vendor_Code as Ven_code,tspl_Bulk_milk_purchase_Invoice_Detail.Actual_Amount as Total_AMT ,tspl_Bulk_milk_purchase_Invoice_head.Total_AMT as Sum_of_ActualAmt, " &
               "tspl_Bulk_milk_purchase_Invoice_Detail.Invoice_Qty as Total_QTY,tspl_Bulk_milk_purchase_Invoice_head.DOC_NO , " &
               "convert(varchar,tspl_Bulk_milk_purchase_Invoice_head.DOC_DATE,103) as DOC_DATE ,tspl_Bulk_milk_purchase_Invoice_head.Vendor_Invoice_No, " &
               "tspl_Bulk_milk_purchase_Invoice_Detail.Item_Code ,TSPL_ITEM_MASTER.Item_Desc  ,TSPL_COMPANY_MASTER.Comp_Name , " &
               "TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2  ,TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add4,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add4,'') else ' ' end  as Loc_Add, " &
               "TSPL_LOCATION_MASTER.TIN_No as Loc_TinNo,TSPL_LOCATION_MASTER.Pin_Code  as Loc_PINCode,TSPL_LOCATION_MASTER.Email as Loc_Email,case when ISNULL(TSPL_LOCATION_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_LOCATION_MASTER.Phone1 end +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_LOCATION_MASTER.Phone2 Else'' End as  Loc_Phn, " &
               "TSPL_VENDOR_MASTER.Vendor_Name ,TSPL_VENDOR_MASTER.add1 +case when len(TSPL_VENDOR_MASTER.add2)>0 then ', '+TSPL_VENDOR_MASTER.add2 else '' end +case when LEN(isnull(TSPL_VENDOR_MASTER.Add3,''))>0 then ', '+isnull(TSPL_VENDOR_MASTER.Add3,'') else ' ' end   as Ven_Add, " &
               "TSPL_VENDOR_MASTER.Tin_No as Ven_TINNo ,TSPL_VENDOR_MASTER.Email as Ven_Email , " &
               "case when ISNULL(TSPL_VENDOR_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_VENDOR_MASTER.Phone1 end +  Case When   ISNULL(TSPL_VENDOR_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_VENDOR_MASTER.Phone2 Else'' End as Ven_Phn, " &
               "BulkMilkSrn.SRN_NO ,convert(varchar,BulkMilkSrn.SRN_Date,103) as SRN_Date ,BulkMilkSrn.Tanker_No , "
        If RunBulkProcOnAdjustFATCLR = 0 Then
            qry += "case when len(isnull(t_FAT.Param_Field_Value,''))>0 then t_FAT.Param_Field_Value else TSPL_BULK_MILK_PURCHASE_INVOICE_detail.fat_per end As FAT, case when len(isnull(t_SNF .Param_Field_Value,''))>0 then t_SNF .Param_Field_Value else TSPL_BULK_MILK_PURCHASE_INVOICE_detail.snf_Per end as SNF,  "
        Else
            qry += "  tspl_Bulk_milk_purchase_Invoice_Detail.fat_per As FAT ,tspl_Bulk_milk_purchase_Invoice_Detail.snf_Per AS SNF,"
        End If

        qry += " case when isnull(TSPL_WEIGHMENT_CHEMBER_DETAILS.Net_Weight,'')>0 then TSPL_WEIGHMENT_CHEMBER_DETAILS.Net_Weight else TSPL_BULK_MILK_PURCHASE_INVOICE_detail.Invoice_Qty  end as Milk_qty , " &
               "'For  FAT' +convert(varchar,TSPL_BULK_PRICE_DETAIL.Fat_Percentage) + ' % &  SNF' +  convert(varchar,TSPL_BULK_PRICE_DETAIL.Snf_Percentage)+ ' %' as 'MilkRate' , " &
               "'For ' +convert(varchar,TSPL_BULK_PRICE_DETAIL.Fat_Weightage ) + ' & ' +  convert(varchar,TSPL_BULK_PRICE_DETAIL.Snf_Weightage ) as 'Weightage', " &
               "tspl_Bulk_milk_purchase_Invoice_Detail.NetRate,tspl_Bulk_milk_purchase_Invoice_Detail.Actual_Amount,tspl_Bulk_milk_purchase_Invoice_head.RoundOffAmount , " &
               "(TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Incentive+TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Deduction-TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SpecialDeduction)as Ded_Inc, " &
               "TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.BasicRate,TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Fat_KG,TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SNF_KG, " &
               "TSPL_VENDOR_MASTER.Bank_Code,TSPL_VENDOR_BANK_MASTER.Bank_Name,(TSPL_VENDOR_BANK_MASTER.Add1+TSPL_VENDOR_BANK_MASTER.Add2+TSPL_VENDOR_BANK_MASTER.Add3)as Bank_Address, " &
               "Account_No from " &
               "tspl_Bulk_milk_purchase_Invoice_Detail " &
               "left outer join tspl_Bulk_milk_purchase_Invoice_head on tspl_Bulk_milk_purchase_Invoice_head.DOC_NO =tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO " &
               "left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code =tspl_Bulk_milk_purchase_Invoice_Detail.Item_Code  " &
               "left outer join TSPL_COMPANY_MASTER  on TSPL_COMPANY_MASTER.Comp_Code =tspl_Bulk_milk_purchase_Invoice_head.Comp_Code  " &
               "left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER .Location_Code =tspl_Bulk_milk_purchase_Invoice_head.loc_code " &
               "left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER .Vendor_Code =tspl_Bulk_milk_purchase_Invoice_head.vendor_code " &
               " left join TSPL_VENDOR_BANK_MASTER on TSPL_VENDOR_MASTER.BANK_CODE=TSPL_VENDOR_BANK_MASTER.BANK_CODE " &
               "left outer join BulkMilkSrn  on BulkMilkSrn.SRN_NO =tspl_Bulk_milk_purchase_Invoice_Detail.SRN_NO " &
               "left outer join TSPL_BULK_MILK_SRN_CHEMBER_DETAILS  on BulkMilkSrn.SRN_NO =TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SRN_NO and " &
               "tspl_Bulk_milk_purchase_Invoice_Detail.CHAMBER_DESC=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Chamber_Desc " &
               "left outer join Tspl_Gate_Entry_Details on Tspl_Gate_Entry_Details.Gate_Entry_No =BulkMilkSrn.Gate_Entry_No  " &
               "Left Outer Join (Select TSPL_QC_Parameter_Detail.* From BulkMilkSrn Left Outer Join TSPL_QC_Parameter_Detail On " &
               "TSPL_QC_Parameter_Detail.QC_No  =  BulkMilkSrn.QC_No  And  TSPL_QC_Parameter_Detail.Param_Type = 'FAT') t_FAT " &
               "On t_FAT.QC_No   = BulkMilkSrn.QC_No and t_FAT.LINE_NO=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Line_No " &
               "Left Outer Join (Select TSPL_QC_Parameter_Detail.* From BulkMilkSrn Left Outer Join TSPL_QC_Parameter_Detail On " &
               "TSPL_QC_Parameter_Detail.QC_No  = BulkMilkSrn.QC_No  And TSPL_QC_Parameter_Detail.Param_Type = 'SNF') t_SNF On " &
               "t_SNF .QC_No   = BulkMilkSrn.QC_No   and t_SNF.LINE_NO=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Line_No " &
               "left outer join TSPL_Bulk_Price_MASTER on TSPL_Bulk_Price_MASTER.Price_Code=tspl_Bulk_milk_purchase_Invoice_Detail.Price_code  " &
               "left outer join TSPL_BULK_PRICE_DETAIL on TSPL_Bulk_Price_MASTER.Price_Code=TSPL_BULK_PRICE_DETAIL.Price_code  and " &
               "TSPL_BULK_PRICE_DETAIL.Milk_Grade_code=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.MILK_GRADE_CODE " &
               "left outer join TSPL_Weighment_Detail on TSPL_Weighment_Detail.Weighment_No =BulkMilkSrn.Weighment_No " &
               "left outer join  TSPL_WEIGHMENT_CHEMBER_DETAILS on TSPL_Weighment_Detail.Weighment_No=TSPL_WEIGHMENT_CHEMBER_DETAILS.Weighment_No and " &
               "TSPL_WEIGHMENT_CHEMBER_DETAILS.Line_No=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Line_No " &
               " left outer join TSPL_STATE_MASTER on TSPL_VENDOR_MASTER.State_Code = TSPL_STATE_MASTER.STATE_CODE " &
               " left outer join tspl_state_master as tspl_state_master_for_location_state on  " &
               " tspl_state_master_for_location_state.state_code=tspl_location_master.state  " &
               " where tspl_bulk_milk_purchase_invoice_head.DOC_NO in ('" + InvoiceNo + "') order by Vendor_name,Date_And_Time"
        Return qry
    End Function
    'Ticket No-ERO/02/08/19-000982 ,Sanjay
    Private Sub btnInvoiceJE_Click(sender As Object, e As EventArgs) Handles btnInvoiceJE.Click
        Try
            Dim strCode As String = fndDocNo.Value
            If clsCommon.myLen(strCode) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Invoice No Found on Current Screen", Me.Text)
                Exit Sub
            End If
            strCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_No from TSPL_vendor_Invoice_Head where Against_BulkMillkPurchaseInvoice_No ='" + strCode + "'"))
            If clsCommon.myLen(strCode) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Invoice No Found on AP Invoice Screen", Me.Text)
                Exit Sub
            End If
            Dim qry As String = " select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No ='" & strCode & "' "
            strCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
            If clsCommon.myLen(strCode) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Journal Entry Found For Current Document", Me.Text)
                Exit Sub
            Else
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.journalEntry, strCode)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub txtTaxGroup__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtTaxGroup._MYValidating
        Try
            txtTaxGroup.Value = clsLocationWiseTax.FinderForTaxGroup(fndLocation.Value, fndVendor.Value, "P", txtTaxGroup.Value, isButtonClicked)
            SetTaxDetails()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadBlankGridTax()
        gv2.Rows.Clear()
        gv2.Columns.Clear()

        Dim repoTaxAuthCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTaxAuthCode.FormatString = ""
        repoTaxAuthCode.HeaderText = "Tax Authority Code"
        repoTaxAuthCode.Name = colTTaxAutCode
        repoTaxAuthCode.Width = 150
        repoTaxAuthCode.ReadOnly = True
        gv2.MasterTemplate.Columns.Add(repoTaxAuthCode)

        Dim repoTaxAuthName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTaxAuthName.FormatString = ""
        repoTaxAuthName.HeaderText = "Tax Authority"
        repoTaxAuthName.Name = colTTaxAutName
        repoTaxAuthName.Width = 200
        repoTaxAuthName.ReadOnly = True
        gv2.MasterTemplate.Columns.Add(repoTaxAuthName)

        Dim repoTaxBaseAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt.FormatString = ""
        repoTaxBaseAmt.HeaderText = "Base Amount"
        repoTaxBaseAmt.Name = colTBaseAmt
        repoTaxBaseAmt.Width = 100
        repoTaxBaseAmt.ReadOnly = True
        repoTaxBaseAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv2.MasterTemplate.Columns.Add(repoTaxBaseAmt)

        Dim repoTaxRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate.FormatString = ""
        repoTaxRate.HeaderText = "Tax Rate"
        repoTaxRate.Name = colTTaxRate
        repoTaxRate.Width = 100
        repoTaxRate.ReadOnly = False
        repoTaxRate.IsVisible = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowTaxRateColumnOnTransaction, clsFixedParameterCode.ShowTaxRateColumnOnTransaction, Nothing)) = 1, True, False)
        repoTaxRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv2.MasterTemplate.Columns.Add(repoTaxRate)

        Dim repoTaxAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt.FormatString = ""
        repoTaxAmt.HeaderText = "Tax Amount"
        repoTaxAmt.Name = colTTaxAmt
        repoTaxAmt.Width = 100
        repoTaxAmt.ReadOnly = False
        repoTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv2.MasterTemplate.Columns.Add(repoTaxAmt)

        gv2.AllowAddNewRow = False
        gv2.ShowGroupPanel = False
        gv2.AllowColumnReorder = False
        gv2.AllowRowReorder = False
        gv2.EnableSorting = False
        gv2.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv2.MasterTemplate.ShowRowHeaderColumn = False

    End Sub
    Sub SetTaxDetails()
        LoadBlankGridTax()
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsByLocation(txtTaxGroup.Value, "P", fndVendor.Value, fndLocation.Value)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            If (dt.Rows.Count > 10) Then
                MessageBox.Show("Can't Handle More than 10 Tax Types in a Group")
                Return
            End If
            lblTaxGrpName.Text = clsCommon.myCstr(dt.Rows(0)("Tax_Group_Desc"))
            For Each dr As DataRow In dt.Rows
                gv2.Rows.AddNew()
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = clsCommon.myCstr(dr("Tax_Code"))
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = clsCommon.myCstr(dr("Tax_Code_Desc"))
                If rbtnTaxCalAutomatic.IsChecked Then
                    If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Is_TCS  from tspl_tax_master where tax_code ='" & clsCommon.myCstr(dr("Tax_Code")) & "' ")), "Y") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsTCSnotApplicable ,0) from TSPL_VENDOR_MASTER where Vendor_Code ='" & fndVendor.Value & "'")), "0") = CompairStringResult.Equal Then
                            If AmountToCheckVendorOutstandingForTCSTax > 0 Then
                                Dim dblOutstandingAmount As Double = clsCommon.myCdbl(clsVendorMaster.GetVendorOutstandingForTCSTaxApplicableOnFY(fndVendor.Value, dtpDocDate.Value))
                                If dblOutstandingAmount < AmountToCheckVendorOutstandingForTCSTax Then
                                    dblOutstandingAmount = dblOutstandingAmount + clsCommon.myCdbl(clsCommon.myFormat(lblActualTCSTaxBaseAmt.Text))
                                    If dblOutstandingAmount > AmountToCheckVendorOutstandingForTCSTax Then
                                        If clsCommon.myCdbl(clsCommon.myFormat(lblActualTCSTaxBaseAmt.Text)) > 0 Then
                                            txttcstaxbaseamount.Value = clsCommon.myCdbl(dblOutstandingAmount - AmountToCheckVendorOutstandingForTCSTax)
                                        End If
                                    End If
                                End If
                                If dblOutstandingAmount > AmountToCheckVendorOutstandingForTCSTax Then
                                    Dim panno As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(PAN,'') as PAN from TSPL_VENDOR_MASTER where Vendor_Code='" & fndVendor.Value & "'"))
                                    If clsCommon.myLen(panno) > 0 Then
                                        gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforVendorWithPanNo, clsFixedParameterCode.TCSRateforVendorWithPanNo, Nothing))
                                    Else
                                        gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforVendorWithoutPanNo, clsFixedParameterCode.TCSRateforVendorWithoutPanNo, Nothing))
                                    End If
                                Else
                                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = 0
                                End If
                            Else
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = dr("TaxRate")
                            End If
                        Else
                            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = 0
                        End If
                        txtTCSTaxRate.Value = clsCommon.myCdbl(gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value)
                    Else
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(dr("TaxRate"))
                    End If
                End If
            Next
            SetitemWiseTaxSetting(True, False)
        Else
            lblTaxGrpName.Text = ""
            For ii As Integer = 0 To gv1.Rows.Count - 1
                BlankTaxDetails(ii)
            Next
        End If

        For ii As Integer = 0 To gv1.Rows.Count - 1
            UpdateCurrentRow(ii)
        Next
        UpdateAllTotals()
    End Sub
    Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer)
        Dim arrTaxableAuth As New List(Of String)
        Dim dblCurrentTaxableAmount As Decimal = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colActAmt).Value)
        For ii As Integer = 1 To 10
            Dim Strii As String = clsCommon.myCstr(ii)
            If rbtnTaxCalAutomatic.IsChecked Then
                Dim strTaxCode As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAX" + Strii)).Value)
                If clsCommon.myLen(strTaxCode) > 0 Then
                    Dim dblTaxRate As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXRATE" + Strii)).Value)
                    Dim IsSurTax As Boolean = clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISSURTAX" + Strii)).Value)
                    Dim strSurTaxCode As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + Strii)).Value)
                    Dim IsTaxable As Boolean = clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + Strii)).Value)
                    Dim IsTaxOnBaseAmt As Boolean = clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxOnBaseAmt" + Strii)).Value)
                    Dim dblBaseAmt As Double = 0
                    Dim dblTaxAmt As Double = 0
                    If IsTaxOnBaseAmt Then
                        dblBaseAmt = dblCurrentTaxableAmount
                    ElseIf Not IsTaxOnBaseAmt AndAlso clsCommon.myCdbl(txttcstaxbaseamount.Value) > 0 AndAlso AllowtoChangeTCSBaseAmountPurchase = True Then
                        Dim dblTotalBasicPrice As Decimal = 0
                        For n As Integer = 0 To gv1.Rows.Count - 1
                            If clsCommon.myLen(gv1.Rows(n).Cells(colItemCode).Value) > 0 Then
                                dblTotalBasicPrice = dblTotalBasicPrice + clsCommon.myCdbl(gv1.Rows(n).Cells(colActAmt).Value)
                            End If
                        Next
                        If dblTotalBasicPrice > 0 Then
                            dblBaseAmt = (clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colActAmt).Value) * clsCommon.myCdbl(txttcstaxbaseamount.Value)) / dblTotalBasicPrice
                        End If
                    ElseIf IsSurTax Then
                        Dim dblSurTaxAmt As Double = GetCurrentRowSurTaxAmt(IntRowNo, ii, strSurTaxCode)
                        dblBaseAmt = dblSurTaxAmt
                    Else
                        Dim dblOtherTaxAmt As Double = 0
                        dblOtherTaxAmt = GetCurrentRowOtherTaxAmt(IntRowNo, Strii, arrTaxableAuth)
                        dblBaseAmt = (dblCurrentTaxableAmount + dblOtherTaxAmt)
                    End If
                    gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXBASEAMT" + Strii)).Value = Math.Round(dblBaseAmt, 2)
                    dblTaxAmt = (dblBaseAmt * dblTaxRate) / 100
                    gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + Strii)).Value = Math.Round(dblTaxAmt, IIf(objCommonVar.IsRoundOffTaxToZeroDecimal, 0, 2))
                    If IsTaxable AndAlso Not arrTaxableAuth.Contains(strTaxCode.ToUpper()) Then
                        arrTaxableAuth.Add(strTaxCode.ToUpper())
                    End If
                Else
                    gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTax" + Strii)).Value = Nothing
                    gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXBASEAMT" + Strii)).Value = Nothing
                    gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxRate" + Strii)).Value = Nothing
                    gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + Strii)).Value = Nothing
                    gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISSURTAX" + Strii)).Value = Nothing
                    gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + Strii)).Value = Nothing
                    gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + Strii)).Value = Nothing
                    gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISEXCISABLE" + Strii)).Value = Nothing
                    gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxOnBaseAmt" + Strii)).Value = Nothing
                End If
            ElseIf rbtnTaxCalManual.IsChecked Then
                If gv2.Rows.Count >= ii Then
                    Dim dblTaxAmt As Double = clsCommon.myCdbl(gv2.Rows(ii - 1).Cells(colTTaxAmt).Value)
                    Dim dblTaxRate As Double = clsCommon.myCdbl(gv2.Rows(ii - 1).Cells(colTTaxRate).Value)
                    Dim dblCurrRowAmt As Double = clsCommon.myCdbl(gv1.Rows(clsCommon.myCdbl(IntRowNo)).Cells(colActAmt).Value)
                    Dim dblTotAmt As Double = 0
                    For jj As Integer = 0 To gv1.Rows.Count - 1
                        dblTotAmt += clsCommon.myCdbl(gv1.Rows(jj).Cells(colActAmt).Value)
                    Next
                    Dim dblCurrCalTax As Double = 0
                    If dblTotAmt <> 0 Then
                        dblCurrCalTax = Math.Round(clsCommon.myCdbl(dblTaxAmt * dblCurrRowAmt / dblTotAmt), 2, MidpointRounding.ToEven)
                    End If
                    gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXBASEAMT" + Strii)).Value = dblCurrRowAmt
                    gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + Strii)).Value = dblCurrCalTax
                    gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXRATE" + Strii)).Value = dblTaxRate
                End If

            End If
        Next
        Dim dblTotTaxAmt As Double = GetCurrentRowTotalTaxAmt(IntRowNo)
        Dim dblAmtAfterTax As Double = dblCurrentTaxableAmount + dblTotTaxAmt + clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colTranspoterCharge).Value)
        gv1.Rows(IntRowNo).Cells(colTotTaxAmt).Value = Math.Round(dblTotTaxAmt, 2)
        gv1.Rows(IntRowNo).Cells(colItemNetAmt).Value = Math.Round(dblAmtAfterTax, 2)
    End Sub
    Private Function GetCurrentRowTotalTaxAmt(ByVal IntRowNo As Integer) As Double
        Dim dblTotTax As Double = 0
        For ii As Integer = 1 To 10
            Dim strii As String = clsCommon.myCstr(ii)
            If IntRowNo < 0 Then
                dblTotTax = dblTotTax + clsCommon.myCdbl(gv1.CurrentRow.Cells(clsCommon.myCstr("COLTAXAMT" + strii)).Value)
            Else
                dblTotTax = dblTotTax + clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXAMT" + strii)).Value)
            End If
        Next
        Return dblTotTax
    End Function
    Private Function GetCurrentRowSurTaxAmt(ByVal IntRowNo As Integer, ByVal intEndCol As Integer, ByVal strSurTaxCode As String) As Double
        For ii As Integer = 1 To intEndCol
            Dim strii As String = clsCommon.myCstr(ii)
            If IntRowNo < 0 Then
                If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strii)).Value)) = CompairStringResult.Equal Then
                    Return clsCommon.myCdbl(gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxAmt" + strii)).Value)
                End If
            Else
                If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTax" + strii)).Value)) = CompairStringResult.Equal Then
                    Return clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + strii)).Value)
                End If
            End If
        Next
        Return 0
    End Function
    Private Function GetCurrentRowOtherTaxAmt(ByVal IntRowNo As Integer, ByVal intEndCol As Integer, ByVal arrTaxableAuth As List(Of String)) As Double
        Dim dblRet As Double = 0
        For Each strTaxAuth As String In arrTaxableAuth
            For ii As Integer = 1 To intEndCol
                Dim strii As String = clsCommon.myCstr(ii)
                If IntRowNo < 0 Then
                    If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strii)).Value)) = CompairStringResult.Equal Then
                        dblRet = dblRet + clsCommon.myCdbl(gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxAmt" + strii)).Value)
                    End If
                Else
                    If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTax" + strii)).Value)) = CompairStringResult.Equal Then
                        dblRet = dblRet + clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + strii)).Value)
                    End If
                End If
            Next
        Next
        Return dblRet
    End Function
    Private Sub BlankTaxDetails(ByVal intRowNo As Integer)
        For ii As Integer = 1 To 10
            Dim strII As String = clsCommon.myCstr(ii)
            If intRowNo < 0 Then
                gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strII)).Value = Nothing
                gv1.CurrentRow.Cells(clsCommon.myCstr("COLTAXBASEAMT" + strII)).Value = Nothing
                gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = Nothing
                gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxAmt" + strII)).Value = Nothing
                gv1.CurrentRow.Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = Nothing
                gv1.CurrentRow.Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = Nothing
                gv1.CurrentRow.Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = Nothing
                gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxOnBaseAmt" + strII)).Value = Nothing
            Else
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTax" + strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("COLTAXBASEAMT" + strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxAmt" + strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxOnBaseAmt" + strII)).Value = Nothing
            End If
        Next
    End Sub
    Sub SetitemWiseTaxSetting(ByVal isChangeRate As Boolean, ByVal isForCurrentRow As Boolean)
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsByLocation(txtTaxGroup.Value, "P", fndVendor.Value, fndLocation.Value)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            If isForCurrentRow Then
                BlankTaxDetails(gv1.CurrentRow.Index)
                If clsCommon.myLen(gv1.CurrentRow.Cells(colItemCode)) > 0 Then
                    Dim ii As Integer = 1
                    For Each dr As DataRow In dt.Rows
                        Dim strII As String = clsCommon.myCstr(ii)
                        gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strII)).Value = clsCommon.myCstr(dr("Tax_Code"))
                        If isChangeRate Then
                            gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(dr("TaxRate"))
                        End If
                        gv1.CurrentRow.Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                        gv1.CurrentRow.Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                        gv1.CurrentRow.Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                        gv1.CurrentRow.Cells(clsCommon.myCstr("ISEXCISABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                        gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxOnBaseAmt" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_On_Base_Amount")), "Y") = CompairStringResult.Equal)
                        ii = ii + 1
                    Next
                End If
            Else
                For intRowNo As Integer = 0 To gv1.Rows.Count - 1
                    BlankTaxDetails(intRowNo)
                    If clsCommon.myLen(gv1.Rows(intRowNo).Cells(colItemCode)) > 0 OrElse (clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_TAX_MASTER WHERE Tax_Code IN (select Tax_Code  from TSPL_TAX_GROUP_DETAILS WHERE TAX_GROUP_CODE='" & txtTaxGroup.Value & "') AND Is_TCS ='Y'")) > 0) Then
                        Dim ii As Integer = 1
                        For Each dr As DataRow In dt.Rows
                            Dim strII As String = clsCommon.myCstr(ii)
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTax" + strII)).Value = clsCommon.myCstr(dr("Tax_Code"))
                            If isChangeRate Then
                                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(dr("TaxRate"))
                            End If

                            ''tcs tax rate
                            If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Is_TCS  from tspl_tax_master where tax_code ='" & clsCommon.myCstr(dr("Tax_Code")) & "' ")), "Y") = CompairStringResult.Equal Then
                                If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsTCSnotApplicable ,0) from TSPL_vendor_master where Vendor_Code ='" & fndVendor.Value & "'")), "0") = CompairStringResult.Equal Then
                                    If AmountToCheckVendorOutstandingForTCSTax > 0 Then
                                        Dim dblOutstandingAmount As Double = clsCommon.myCdbl(clsVendorMaster.GetVendorOutstandingForTCSTaxApplicableOnFY(fndVendor.Value, dtpDocDate.Value))
                                        If dblOutstandingAmount < AmountToCheckVendorOutstandingForTCSTax Then
                                            dblOutstandingAmount = dblOutstandingAmount + clsCommon.myCdbl(clsCommon.myFormat(lblActualTCSTaxBaseAmt.Text))
                                            If dblOutstandingAmount > AmountToCheckVendorOutstandingForTCSTax Then
                                                If clsCommon.myCdbl(clsCommon.myFormat(lblActualTCSTaxBaseAmt.Text)) > 0 Then
                                                    txttcstaxbaseamount.Value = clsCommon.myCdbl(dblOutstandingAmount - AmountToCheckVendorOutstandingForTCSTax)
                                                End If
                                            End If
                                        End If
                                        If dblOutstandingAmount > AmountToCheckVendorOutstandingForTCSTax Then
                                            Dim panno As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(PAN,'')  as PanNoAdhar from TSPL_vendor_master where Vendor_Code='" & fndVendor.Value & "'"))
                                            If clsCommon.myLen(panno) > 0 Then
                                                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforVendorWithPanNo, clsFixedParameterCode.TCSRateforVendorWithPanNo, Nothing))
                                            Else
                                                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforVendorWithoutPanNo, clsFixedParameterCode.TCSRateforVendorWithoutPanNo, Nothing))
                                            End If
                                        Else
                                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = 0
                                        End If
                                    Else
                                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = dr("TaxRate")
                                    End If
                                Else
                                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = 0
                                End If
                            End If

                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISEXCISABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxOnBaseAmt" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_On_Base_Amount")), "Y") = CompairStringResult.Equal)
                            ii = ii + 1
                        Next
                    End If
                Next
            End If
        End If
    End Sub
    Private Sub rbtnTaxCalAutomatic_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnTaxCalAutomatic.ToggleStateChanged, rbtnTaxCalManual.ToggleStateChanged
        If Not isLoadData Then
            If rbtnTaxCalAutomatic.IsChecked Then
                SetTaxDetails()
                gv2.Columns(colTTaxRate).IsVisible = False
            ElseIf rbtnTaxCalManual.IsChecked Then
                For intRowNo As Integer = 0 To gv2.Rows.Count - 1
                    gv2.Rows(intRowNo).Cells(colTTaxRate).Value = Nothing
                    gv2.Rows(intRowNo).Cells(colTTaxAmt).Value = Nothing
                    gv2.Rows(intRowNo).Cells(colTBaseAmt).Value = Nothing
                Next
                For intRowNo As Integer = 0 To gv1.Rows.Count - 1
                    For ii As Integer = 1 To 10
                        Dim strII As String = clsCommon.myCstr(ii)
                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = Nothing
                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxAmt" + strII)).Value = Nothing
                    Next
                Next
                gv2.Columns(colTTaxRate).IsVisible = True
            End If
        End If
    End Sub
    Private Sub gv2_CellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv2.CellFormatting
        Try
            If e.Column.Index >= 0 Then
                If (e.Column Is gv2.Columns(colTTaxAmt)) Then
                    gv2.CurrentRow.Cells(colTTaxAmt).ReadOnly = rbtnTaxCalAutomatic.IsChecked
                ElseIf (e.Column Is gv2.Columns(colTTaxRate)) Then
                    gv2.CurrentRow.Cells(colTTaxRate).ReadOnly = rbtnTaxCalAutomatic.IsChecked
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub gv2_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv2.DoubleClick
        Try
            If rbtnTaxCalAutomatic.IsChecked Then
                Dim dblNewRate As Double = clsLocationWiseTax.FinderForTaxRate(fndLocation.Value, txtTaxGroup.Value, clsCommon.myCstr(gv2.CurrentRow.Cells(colTTaxAutCode).Value), fndVendor.Value, "P")
                Dim intRowNo As Integer = gv2.CurrentRow.Index
                If gv1.RowCount > 0 AndAlso intRowNo >= 0 Then
                    Dim strII As String = clsCommon.myCstr(intRowNo + 1)
                    For ii As Integer = 0 To gv1.Rows.Count - 1
                        gv1.Rows(ii).Cells("COLTAXRATE" + strII).Value = dblNewRate
                        If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Is_TCS  from tspl_tax_master where tax_code ='" & clsCommon.myCstr(gv1.Rows(ii).Cells("COLTAX" + strII).Value) & "' ")), "Y") = CompairStringResult.Equal Then
                            txtTCSTaxRate.Value = dblNewRate
                        End If
                    Next
                End If
                For ii As Integer = 0 To gv1.Rows.Count - 1
                    UpdateCurrentRow(ii)
                Next
                UpdateAllTotals()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub gv1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.DoubleClick
        Try
            If gv1.CurrentColumn Is gv1.Columns(colTotTaxAmt) AndAlso rbtnTaxCalAutomatic.IsChecked Then
                'If Not PurchaseModulePickFixTaxRate OrElse Not clsCommon.myCBool(gv1.CurrentRow.Cells(colItemTaxable).Value) Then
                Dim frm As New FrmPOItemTaxDetails()
                frm.strLineNo = clsCommon.myCstr(gv1.CurrentRow.Cells(colSlNo).Value)
                frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colItemCode).Value)
                frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colItemDesc).Value)
                frm.dblTotTax = clsCommon.myCdbl(gv1.CurrentRow.Cells(colTotTaxAmt).Value)
                frm.dblAmtAfterDis = clsCommon.myCdbl(gv1.CurrentRow.Cells(colActAmt).Value)
                ''New Column for location wise
                frm.strTaxGroup = txtTaxGroup.Value
                frm.strTransLocation = fndLocation.Value
                frm.strTaxType = "P"
                frm.strVendorCustomerCode = fndVendor.Value
                ''End of New Column for location wise

                frm.PurchaseModulePickFixTaxRate = False
                frm.IsTaxableItem = clsItemMaster.IsTaxableItem(frm.strItemCode, Nothing)

                If clsCommon.myLen(frm.strItemCode) > 0 Then
                    frm.ArrIn = New List(Of clsTempItemTaxDetails)
                    For ii As Integer = 1 To 10
                        Dim strii As String = clsCommon.myCstr(ii)
                        Dim obj As New clsTempItemTaxDetails()
                        obj.AuthorityCode = clsCommon.myCstr(gv1.CurrentRow.Cells("COLTAX" + strii).Value)
                        If clsCommon.myLen(obj.AuthorityCode) > 0 Then
                            obj.AuthorityName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Tax_Code_Desc from TSPL_TAX_MASTER WHERE Tax_Code='" + obj.AuthorityCode + "'"))
                            obj.Rate = clsCommon.myCdbl(gv1.CurrentRow.Cells("COLTAXRATE" + strii).Value)
                            obj.BaseAmt = clsCommon.myCdbl(gv1.CurrentRow.Cells("COLTAXBASEAMT" + strii).Value)
                            obj.TaxAmt = clsCommon.myCdbl(gv1.CurrentRow.Cells("COLTAXAMT" + strii).Value)
                            obj.isSurTax = clsCommon.myCBool(gv1.CurrentRow.Cells("ISSURTAX" + strii).Value)
                            obj.SurTax = clsCommon.myCstr(gv1.CurrentRow.Cells("SURTAXCODE" + strii).Value)
                            obj.IsTaxable = clsCommon.myCBool(gv1.CurrentRow.Cells("ISTAXABLE" + strii).Value)
                            frm.ArrIn.Add(obj)
                        End If
                    Next

                    frm.ShowDialog()
                    If frm.ArrOut IsNot Nothing AndAlso frm.ArrOut.Count > 0 Then
                        BlankTaxDetails(gv1.CurrentRow.Index)
                        For ii As Integer = 0 To frm.ArrOut.Count - 1
                            Dim strii As String = clsCommon.myCstr(ii + 1)
                            gv1.CurrentRow.Cells("COLTAX" + strii).Value = frm.ArrOut(ii).AuthorityCode
                            gv1.CurrentRow.Cells("COLTAXRATE" + strii).Value = frm.ArrOut(ii).Rate
                            gv1.CurrentRow.Cells("COLTAXBASEAMT" + strii).Value = frm.ArrOut(ii).BaseAmt
                            gv1.CurrentRow.Cells("COLTAXAMT" + strii).Value = frm.ArrOut(ii).TaxAmt
                            gv1.CurrentRow.Cells("ISSURTAX" + strii).Value = frm.ArrOut(ii).isSurTax
                            gv1.CurrentRow.Cells("SURTAXCODE" + strii).Value = frm.ArrOut(ii).SurTax
                            gv1.CurrentRow.Cells("ISTAXABLE" + strii).Value = frm.ArrOut(ii).IsTaxable
                        Next
                        gv1.CurrentRow.Cells(colTotTaxAmt).Value = frm.dblTotTax
                        UpdateCurrentRow(gv1.CurrentRow.Index)
                        UpdateAllTotals()
                    End If
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub gv2_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv2.CellValueChanged
        Try
            If (Not isLoadData) Then
                If Not isCellValueChangedTaxOpen Then
                    isCellValueChangedTaxOpen = True
                    If (e.Column Is (gv2.Columns(colTTaxAmt)) AndAlso rbtnTaxCalManual.IsChecked) Then
                        For ii As Integer = 0 To gv1.Rows.Count - 1
                            UpdateCurrentRow(ii)
                        Next
                        UpdateAllTotals()
                    End If
                    isCellValueChangedTaxOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub lblAmtAfterDiscount_TextChanged(sender As Object, e As EventArgs) Handles txtTaxableAmt.TextChanged
        ActualTCSTaxBaseAmt()
    End Sub

    Sub ActualTCSTaxBaseAmt()
        Try
            If Not isLoadData Then
                If ConsiderPreviousandCurrentFYForTCSTaxCustOutstanding = True Then
                    If clsCommon.myLen(txtTaxGroup.Value) > 0 Then
                        lblActualTCSTaxBaseAmt.Text = clsCommon.myFormat(txtTaxableAmt.Text)
                        SetTaxDetails()
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txttcstaxbaseamount_TextChanged(sender As Object, e As EventArgs) Handles txttcstaxbaseamount.TextChanged
        Try
            If Not isLoadData Then
                If AllowtoChangeTCSBaseAmountPurchase Then
                    For ii As Integer = 0 To gv1.Rows.Count - 1
                        UpdateCurrentRow(ii)
                    Next
                    UpdateAllTotals()
                Else
                    txttcstaxbaseamount.Value = 0
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnViewTDS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewTDSDetails.Click
        ViewTDS()
    End Sub
    Sub ViewTDS()
        Try
            Dim frm As New FrmViewTDS()
            UpdateTDSAmount()
            frm.ObjIn = objRemittance
            frm.ShowDialog()
            objRemittance = frm.ObjReturn
            UpdateTDSAmountValue()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub



    Sub SetVendorTDSDetails()
        btnViewTDSDetails.Enabled = False
        objRemittance = Nothing
        Dim objVendor As clsTDSVendorDetails = clsTDSVendorDetails.GetData(fndVendor.Value)
        If objVendor IsNot Nothing AndAlso objCommonVar.ApplyGovtRulesInTDS Then
            btnViewTDSDetails.Enabled = True
            Dim IncludeTax As String = clsDBFuncationality.getSingleValue("select ISNULL(TSPL_TDS_SECTION_MASTER.Include_Tax,'') AS Include_Tax from TSPL_TDS_VENDOR_DETAILS left outer join TSPL_TDS_DEDUCTION_HEAD on TSPL_TDS_DEDUCTION_HEAD.Deduction_Code=TSPL_TDS_VENDOR_DETAILS.Nature_Of_Deduction left outer join TSPL_TDS_SECTION_MASTER on TSPL_TDS_SECTION_MASTER.TDS_Group=TSPL_TDS_DEDUCTION_HEAD.TDS_Section where Vendor_Code='" + fndVendor.Value + "'")
            Dim appAmt As Double = 0
            If (clsCommon.CompairString(IncludeTax, "N") = CompairStringResult.Equal) Then
                appAmt = clsCommon.myCdbl(txtTaxableAmt.Text)
            Else
                appAmt = clsCommon.myCdbl(txtTotalAmt.Text)
            End If
            Dim objDedDetails As clsTDSDeductionDetails = clsTDSDeductionDetails.GetApplicableTDRate(objVendor.Nature_Of_Deduction, appAmt, Nothing, False, fndVendor.Value)
            If (objDedDetails IsNot Nothing) Then
                ''By Balwinder on 09/11/2016 against ticket no BM00000010070
                Dim isApplyTDS As Boolean = False
                Dim qry As String = "select Fiscal_Code,Start_Date,End_Date from TSPL_Fiscal_Year_Master where convert(date,'" + dtpDocDate.Value + "',103)>=  convert(date,Start_Date,103)  and convert(date,'" + dtpDocDate.Value + "',103)<=convert(date,End_Date,103) "
                Dim dtFY As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dtFY Is Nothing OrElse dtFY.Rows.Count <= 0 Then
                    Throw New Exception("Please make fiscal year where document date exists")
                End If

                ''Check if any TDS entry found in Document Fiscal Year
                qry = "select top 1 Remittance_Code from TSPL_REMITTANCE  where Vendor_Code='" + fndVendor.Value + "' and convert(date, Document_Date,103)>='" + clsCommon.GetPrintDate(clsCommon.myCDate(dtFY.Rows(0)("Start_Date")), "dd/MMM/yyyy") + "' and  convert(date, Document_Date,103)<='" + clsCommon.GetPrintDate(clsCommon.myCDate(dtFY.Rows(0)("End_Date")), "dd/MMM/yyyy") + "' and Document_No not in ('" + fndDocNo.Value + "')"
                Dim dtTemp As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 0 Then
                    isApplyTDS = True
                Else
                    qry = "select Cumm_Cutoff,Cumm_Cutoff_Document from TSPL_TDS_DEDUCTION_HEAD where Deduction_Code='" + objVendor.Nature_Of_Deduction + "'"
                    dtTemp = clsDBFuncationality.GetDataTable(qry)
                    If dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 0 Then
                        If clsCommon.myCdbl(dtTemp.Rows(0)("Cumm_Cutoff_Document")) <= 0 AndAlso clsCommon.myCdbl(dtTemp.Rows(0)("Cumm_Cutoff")) <= 0 Then
                            isApplyTDS = True
                        Else
                            qry = "select sum( " + IIf(clsCommon.CompairString(IncludeTax, "N") = CompairStringResult.Equal, "TSPL_VENDOR_INVOICE_HEAD.Amount_Less_Discount", "TSPL_VENDOR_INVOICE_HEAD.Document_Total") + ") as Document_Total from TSPL_VENDOR_INVOICE_HEAD where Vendor_Code='" + fndVendor.Value + "' and Document_Type in ('I','C') and Document_No not in ('" + fndDocNo.Value + "') and  convert(date, Invoice_Entry_Date,103)>='" + clsCommon.GetPrintDate(clsCommon.myCDate(dtFY.Rows(0)("Start_Date")), "dd/MMM/yyyy") + "' and  convert(date, Invoice_Entry_Date,103)<='" + clsCommon.GetPrintDate(clsCommon.myCDate(dtFY.Rows(0)("End_Date")), "dd/MMM/yyyy") + "' and TSPL_VENDOR_INVOICE_HEAD.Posting_Date is not null "
                            dblPreviousTDSAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
                            If appAmt >= clsCommon.myCdbl(dtTemp.Rows(0)("Cumm_Cutoff_Document")) AndAlso clsCommon.myCdbl(dtTemp.Rows(0)("Cumm_Cutoff_Document")) > 0 Then
                                isApplyTDS = True
                            ElseIf (dblPreviousTDSAmt + appAmt) >= clsCommon.myCdbl(dtTemp.Rows(0)("Cumm_Cutoff")) AndAlso clsCommon.myCdbl(dtTemp.Rows(0)("Cumm_Cutoff")) > 0 Then
                                isApplyTDS = True
                            End If
                        End If
                    End If
                End If

                If isApplyTDS Then
                    objRemittance = New clsRemittance()
                    objRemittance.Branch_Code = objVendor.Branch_Code
                    objRemittance.Deduction_Code = objVendor.Nature_Of_Deduction
                    objRemittance.TDS_Per = objDedDetails.TDS
                    objRemittance.Surcharge_Per = objDedDetails.Surcharge
                    objRemittance.Edu_Cess_Per = objDedDetails.Educess
                    objRemittance.Sec_Educess_Per = objDedDetails.Seceducess
                    objRemittance.IsTDSOverride = False

                    If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                        objRemittance.IsApplyTDS = True
                    Else
                        objRemittance.IsApplyTDS = clsPIRemittance.IsTDSApplied(fndDocNo.Value)
                    End If
                    objRemittance.Section_Code = objVendor.TDSSection
                    objRemittance.Section_Description = objVendor.TDSSectionDescription
                    objRemittance.Select_By = objVendor.VendorTypeCode
                    'objRemittance.Include_Tax = objVendor.Include_Tax

                    objRemittance.Fiscal_Year = clsCommon.myCstr(dtFY.Rows(0)("Fiscal_Code"))
                    objRemittance.Quarter = "First"
                End If
            End If
        End If
    End Sub
    Sub UpdateTDSAmount()
        If (objRemittance Is Nothing) Then
            SetVendorTDSDetails()
        End If
        If (objRemittance IsNot Nothing) Then
            Dim IncludeTax As String = clsDBFuncationality.getSingleValue("select ISNULL(TSPL_TDS_SECTION_MASTER.Include_Tax,'') AS Include_Tax from TSPL_TDS_VENDOR_DETAILS left outer join TSPL_TDS_DEDUCTION_HEAD on TSPL_TDS_DEDUCTION_HEAD.Deduction_Code=TSPL_TDS_VENDOR_DETAILS.Nature_Of_Deduction left outer join TSPL_TDS_SECTION_MASTER on TSPL_TDS_SECTION_MASTER.TDS_Group=TSPL_TDS_DEDUCTION_HEAD.TDS_Section where Vendor_Code='" + fndVendor.Value + "'")
            Dim applicableAmt As Double = 0
            If clsCommon.CompairString(IncludeTax, "N") = CompairStringResult.Equal Then
                applicableAmt = clsCommon.myCdbl(txtTaxableAmt.Text)
            Else
                applicableAmt = clsCommon.myCdbl(txtTotalAmt.Text)
            End If
            applicableAmt += dblPreviousTDSAmt


            Dim objDedDetails As clsTDSDeductionDetails = clsTDSDeductionDetails.GetApplicableTDRate(objRemittance.Deduction_Code, applicableAmt, Nothing, False, fndVendor.Value)
            If (objDedDetails IsNot Nothing AndAlso objRemittance.IsApplyTDS) Then
                objRemittance.TDS_Per = objDedDetails.TDS
                objRemittance.Surcharge_Per = objDedDetails.Surcharge
                objRemittance.Edu_Cess_Per = objDedDetails.Educess
                objRemittance.Sec_Educess_Per = objDedDetails.Seceducess
            End If

            objRemittance.Vendor_Code = fndVendor.Value
            objRemittance.Vendor_Name = lblVendorName.Text
            objRemittance.Document_Date = dtpDocDate.Value
            objRemittance.Document_Type = "I"
            objRemittance.Document_Amount = clsCommon.myCdbl(txtTotalAmt.Text)
            objRemittance.Calculated_TDS_Base = applicableAmt
            If Not objRemittance.IsTDSOverride Then
                objRemittance.Actual_TDS_Base = applicableAmt
            End If

            objRemittance.Calculated_TDS = (objRemittance.Calculated_TDS_Base * objRemittance.TDS_Per) / 100
            objRemittance.Actual_TDS = (objRemittance.Actual_TDS_Base * objRemittance.TDS_Per) / 100

            objRemittance.Calculated_Surcharge = (objRemittance.Calculated_TDS_Base * objRemittance.Surcharge_Per) / 100
            objRemittance.Actual_Surcharge = (objRemittance.Actual_TDS_Base * objRemittance.Surcharge_Per) / 100

            objRemittance.Calculated_Edu_Cess = (objRemittance.Calculated_TDS_Base * objRemittance.Edu_Cess_Per) / 100
            objRemittance.Actual_Edu_Cess = (objRemittance.Actual_TDS_Base * objRemittance.Edu_Cess_Per) / 100

            objRemittance.Calculated_Sec_Educess = (objRemittance.Calculated_TDS_Base * objRemittance.Sec_Educess_Per) / 100
            objRemittance.Actual_Sec_Educess = (objRemittance.Actual_TDS_Base * objRemittance.Sec_Educess_Per) / 100

            objRemittance.Calculated_Total_TDS = objRemittance.Calculated_TDS + objRemittance.Calculated_Surcharge + objRemittance.Calculated_Edu_Cess + objRemittance.Calculated_Sec_Educess
            objRemittance.Actual_Total_TDS = objRemittance.Actual_TDS + objRemittance.Actual_Surcharge + objRemittance.Actual_Edu_Cess + objRemittance.Actual_Sec_Educess

        End If
        UpdateTDSAmountValue()
    End Sub

    Private Sub UpdateTDSAmountValue()
        If objRemittance IsNot Nothing Then
            txtTDSAmt.Text = clsCommon.myFormat(objRemittance.Actual_Total_TDS)
        Else
            txtTDSAmt.Text = "0.00"
        End If
        txtAmtAfterTDS.Text = clsCommon.myFormat(clsCommon.myCDecimal(txtTotalAmt.Text) - clsCommon.myCDecimal(txtTDSAmt.Text))
    End Sub
End Class
