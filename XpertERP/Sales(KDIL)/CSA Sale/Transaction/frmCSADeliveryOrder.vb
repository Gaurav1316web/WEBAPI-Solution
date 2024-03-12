'=================BM00000003598===========created by Monika
'===============BM00000003979=================BM00000004883=======
'=============Ticket no BM00000009888 Testing ticket
Imports common
Imports System.Data.SqlClient

Imports System.IO

Public Class FrmCSADeliveryOrder
    Inherits FrmMainTranScreen

#Region "variables"
    Public AllowModifcationByApprovalUser As Boolean = False
    Dim AllowNLevel As Boolean = False
    Dim ForUDLOnly As Boolean = False
    Dim AllowRate_Readonly As Boolean = False
    Dim strShowCSARequest As Boolean = False
    Dim AllowSchemeFlow As Boolean = False
    Dim Apply_PriceChat_On_OtherItems As Boolean = False
    Dim CheckCreditLimit As Boolean = False

    Public StrDocNo As String = ""
    Dim strFromDrillDown As Boolean = False

    Dim ReportID As String = "CSADOENTRY"
    Dim Errorcontrol As New clsErrorControl()
    Dim arrLoc As String = ""
    Dim isNewentry As Boolean = True
    Dim ButtonToolTip As New ToolTip()
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChanged As Boolean = False
    'Dim qry As String = ""
    'Dim check As Integer = 0
    'Dim dt As New DataTable

    Const colLineno As String = "LineNo"
    Const colItemCode As String = "ItemCode"
    Const colitemname As String = "ItemName"
    Const colIHSN As String = "colIHSN"
    Const colUOM As String = "UOM"
    Const colCSAType As String = "CSATYPE"
    Const colQty As String = "Qty"
    Const coluomPrice As String = "Rate"
    Const colMRP As String = "MRP"
    Const colTax As String = "Tax"
    Const colTotalPrice As String = "TotalRate"
    Const colRemarks As String = "remarks"
    Const colBal_Qty As String = "BalQty"
    Const colCSARequest_No As String = "Req_No"

    ''===========scheme columns==============================
    Const colSchmCode As String = "SchmCode"
    Const colSchmCodeType As String = "SchmCodeType"
    Const colMainIcode As String = "MainIcode"
    Const colMainIQty As String = "MainIQty"
    Const colMainIUOM As String = "MainIUOM"
    Const colFOC As String = "FOC"
    Const colIsSchmItem As String = "SchmItem"
    Const colCashSchemeCode As String = "CashSchemeCode"
    Const colCashSchemeType As String = "CashSchType"
    Const colCash_Pers As String = "CashScPers"
    Const colCash_Amt As String = "CashSc_Amt"
    ''============end here=======================================

    Dim CSAPricePostedData As Boolean
#End Region

    Private Sub LOCATIONRIGTHS()
        Try
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData()

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                Dim check As Integer = 0
                check = clsDBFuncationality.getSingleValue("select count(*) from tspl_location_master where type='Plant' and location_code='" + obj.Default_LocCode + "'")

                If check > 0 Then
                    txtfrmloc_code.Value = obj.Default_LocCode
                    txtfrmloc_name.Text = obj.Default_LocName
                End If
            End If
            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmCSADeliveryOrder)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
        btnpost.Visible = MyBase.isPostFlag
        btnPrint.Visible = MyBase.isPrintFlag
        btnAmendment.Visible = False
    End Sub

    Private Sub FunReset()
        If AllowNLevel Then
            btnpost.Visible = MyBase.isPostFlag
        End If
        fndRequestNo.Value = ""
        txtfrmloc_code.Enabled = True
        txtcustcode.Enabled = True
        txttotal_amt.Text = ""
        txtCode.Value = ""
        txtRt_UOM.Text = ""
        dtpdate.Text = clsCommon.GETSERVERDATE(Nothing)
        txtcustcode.Value = ""
        txtcustName.Text = ""
        txtfrmloc_code.Value = ""
        txtfrmloc_name.Text = ""
        txttoloc_code.Value = ""
        txttoloc_name.Text = ""
        txtstate_code.Value = ""
        txtstate_name.Text = ""

        isInsideLoadData = True
        txtrate.Text = ""
        isInsideLoadData = False

        cmbTax.SelectedValue = ""
        cmbCSAType.SelectedValue = "CPD-DESI GHEE"
        cmbCSAType.Enabled = False

        If ForUDLOnly Then
            cmbTax.SelectedValue = "No"
        End If

        gv.Rows.Clear()
        gv.Rows.AddNew()
        cmbTax.Enabled = True

        isNewentry = True
        LOCATIONRIGTHS()
        UcAttachment1.Form_ID = Me.Form_ID
        UcAttachment1.BlankAllControls()

        txtCode.MyReadOnly = False
        btnsave.Enabled = True
        btndelete.Enabled = False
        btnpost.Enabled = False
        btnsave.Text = "Save"
        UsLock1.Status = ERPTransactionStatus.Pending
        btnAmendment.Visible = False
        fndRequestNo.Enabled = True

        RadPageView1.SelectedPage = RadPageViewPage1

        txtcustcode.Focus()
        txtcustcode.Select()
        SetMailRight()

    End Sub

    Sub setBalance()
        UcItemBalance1.ItemCode = clsCommon.myCstr(gv.CurrentRow.Cells(colItemCode).Value)
        UcItemBalance1.ItemName = clsCommon.myCstr(gv.CurrentRow.Cells(colitemname).Value)
        UcItemBalance1.ItemMRP = 0
        UcItemBalance1.LocationCode = txtfrmloc_code.Value
        UcItemBalance1.LocationName = txtfrmloc_name.Text
        UcItemBalance1.UOM = clsCommon.myCstr(gv.CurrentRow.Cells(colUOM).Value)
        UcItemBalance1.TransNo = txtCode.Value
        UcItemBalance1.TransDate = dtpdate.Text
        UcItemBalance1.ShowCSADOQty = True
        UcItemBalance1.RefreshData()
    End Sub

    Private Sub gv_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv.Click
        If gv.CurrentRow IsNot Nothing Then
            setBalance()
        End If
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                txtCode.Focus()
                txtCode.Select()
                Errorcontrol.SetError(txtCode, "Select document for deletion first.")
                Throw New Exception("Select document for deletion first.")
            Else
                Errorcontrol.ResetError(txtCode)
            End If

            If Not (myMessages.deleteConfirm()) Then
                Return
            End If

            'done by stuti on 1/12/2016 for N-LevelApproval
            If allownlevel Then
                clsApply_Approval.CheckUpdate_Doc_Valid(clsUserMgtCode.frmCSADeliveryOrder, clsCommon.myCstr(txtCode.Value))
            End If
            '===========================================================

            'clsCommon.ProgressBarShow()
            If clsCSADeliveryOrder.DeleteData(txtCode.Value) Then
                'clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully", Me.Text)

                FunReset()
            End If
            'clsCommon.ProgressBarHide()
        Catch ex As Exception
            'clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Function AllowToSave() As Boolean
        Dim qry As String = ""
        Try
            'KUNAL > TICKET : BM00000009580 > DATE :  18 - OCTOBER - 2016
            If AllowFutureDateTransaction(dtpdate.Value, Nothing) = False Then
                dtpdate.Select()
                Return False
            End If

            If strShowCSARequest AndAlso clsCommon.myLen(fndRequestNo.Value) <= 0 Then
                fndRequestNo.Focus()
                fndRequestNo.Select()
                Errorcontrol.SetError(fndRequestNo, "Select Request No.")
                Throw New Exception("Select Request No.")
            Else
                Errorcontrol.ResetError(fndRequestNo)
            End If

            If clsCommon.myLen(txtcustcode.Value) <= 0 Then
                txtcustcode.Focus()
                txtcustcode.Select()
                Errorcontrol.SetError(txtcustName, "Select CSA detail.")
                Throw New Exception("Select CSA detail.")
            Else
                Errorcontrol.ResetError(txtcustName)
            End If

            If clsCommon.myLen(txtfrmloc_code.Value) <= 0 Then
                txtfrmloc_code.Focus()
                txtfrmloc_code.Select()
                Errorcontrol.SetError(txtfrmloc_name, "Select From location detail.")
                Throw New Exception("Select From location detail.")
            Else
                Errorcontrol.ResetError(txtfrmloc_name)
            End If

            If clsCommon.myLen(txttoloc_code.Value) <= 0 Then
                txttoloc_code.Focus()
                txttoloc_code.Select()
                Errorcontrol.SetError(txttoloc_name, "Mapped CSA with location at loaction master first.")
                Throw New Exception("Mapped CSA with location at loaction master first.")
            Else
                Errorcontrol.ResetError(txttoloc_name)
            End If

            'If clsCommon.myCdbl(txtrate.Value) <= 0 Then
            'txtrate.Focus()
            'txtrate.Select()
            'Errorcontrol.SetError(txtrate, "Fill transfer rate for RT.")
            'Throw New Exception("Fill transfer rate for RT.")
            'Else
            'Errorcontrol.ResetError(txtrate)
            'End If

            ''===================================================
            If ForUDLOnly AndAlso (clsCommon.CompairString(cmbCSAType.SelectedValue, "") = CompairStringResult.Equal OrElse clsCommon.CompairString(cmbCSAType.SelectedValue, "None") = CompairStringResult.Equal) Then
                cmbCSAType.SelectedValue = "CPD-DESI GHEE"
            End If
            If ForUDLOnly AndAlso (clsCommon.CompairString(cmbTax.SelectedValue, "") = CompairStringResult.Equal OrElse clsCommon.CompairString(cmbTax.SelectedValue, "None") = CompairStringResult.Equal) Then
                cmbTax.SelectedValue = "No"
            End If
            ''==================================================================

            If Not ForUDLOnly AndAlso (clsCommon.CompairString(cmbCSAType.SelectedValue, "") = CompairStringResult.Equal OrElse clsCommon.CompairString(cmbCSAType.SelectedValue, "None") = CompairStringResult.Equal) Then
                cmbCSAType.Select()
                Errorcontrol.SetError(cmbCSAType, "Select CSA Item Type.")
                Throw New Exception("Select CSA Item Type.")
            Else
                Errorcontrol.ResetError(cmbCSAType)
            End If

            If Not ForUDLOnly AndAlso clsCommon.CompairString(cmbTax.SelectedValue, "") = CompairStringResult.Equal Then
                cmbTax.Select()
                cmbTax.Focus()
                Errorcontrol.SetError(cmbTax, "Select Tax Status.")
                Throw New Exception("Select Tax Status.")
            Else
                Errorcontrol.ResetError(cmbTax)
            End If

            For ii As Integer = 0 To gv.Rows.Count - 1
                Dim icode As String = ""
                Dim oldicode As String = ""
                Dim uom As String = ""
                Dim qty As String = ""
                Dim rate As String = ""
                Dim taxtype As String = ""
                Dim FOC As Integer = 0
                Dim requestno As String = ""
                Dim requestdate As DateTime? = Nothing

                Dim balqty As Decimal = clsCommon.myCdbl(clsCSADeliveryOrderDetail.GetBalanceRequestQty(clsCommon.myCstr(txtCode.Value), clsCommon.myCDate(dtpdate.Text), clsCommon.myCstr(gv.Rows(ii).Cells(colCSARequest_No).Value), clsCommon.myCstr(gv.Rows(ii).Cells(colItemCode).Value), Nothing))

                gv.Rows(ii).Cells(colBal_Qty).Value = balqty
                icode = clsCommon.myCstr(gv.Rows(ii).Cells(colItemCode).Value)
                uom = clsCommon.myCstr(gv.Rows(ii).Cells(colUOM).Value)
                qty = clsCommon.myCdbl(gv.Rows(ii).Cells(colQty).Value)
                rate = clsCommon.myCdbl(gv.Rows(ii).Cells(coluomPrice).Value)
                taxtype = clsCommon.myCstr(gv.Rows(ii).Cells(colTax).Value)
                requestno = clsCommon.myCstr(gv.Rows(ii).Cells(colCSARequest_No).Value)

                If clsCommon.myLen(icode) <= 0 AndAlso ii = 0 Then
                    Throw New Exception("Fill atleast one row in grid.")
                End If

                If clsCommon.myLen(icode) > 0 Then
                    If strShowCSARequest AndAlso clsCommon.myLen(gv.Rows(ii).Cells(colCSARequest_No).Value) > 0 AndAlso (Not clsCSADeliveryOrderDetail.IsValidCustomerFor_CSARequestItem(clsCommon.myCstr(gv.Rows(ii).Cells(colCSARequest_No).Value), icode, clsCommon.myCstr(txtcustcode.Value))) Then
                        common.clsCommon.MyMessageBoxShow("Customer :" + clsCommon.myCstr(txtcustName.Text) + " is not valid for Request No:" + clsCommon.myCstr(gv.Rows(ii).Cells(colCSARequest_No).Value) + " and Item : " + clsCommon.myCstr(gv.Rows(ii).Cells(colitemname).Value) + " At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                        gv.CurrentRow = gv.Rows(ii)
                        gv.CurrentColumn = gv.Columns(colItemCode)
                        Return False
                    End If

                    If clsCommon.myLen(uom) <= 0 Then
                        gv.CurrentRow = gv.Rows(ii)
                        gv.CurrentColumn = gv.Columns(colUOM)
                        Throw New Exception("Fill unit at row no. " + clsCommon.myCstr(ii + 1) + "")
                    End If
                    If qty <= 0 Then
                        gv.CurrentRow = gv.Rows(ii)
                        gv.CurrentColumn = gv.Columns(colQty)
                        Throw New Exception("Fill quantity at row no. " + clsCommon.myCstr(ii + 1) + "")
                    End If

                    If strShowCSARequest AndAlso clsCommon.myLen(gv.Rows(ii).Cells(colCSARequest_No).Value) > 0 AndAlso qty > balqty Then
                        gv.CurrentRow = gv.Rows(ii)
                        gv.CurrentColumn = gv.Columns(colQty)
                        gv.Rows(ii).Cells(colQty).Value = 0
                        Throw New Exception("Filled quantity can not be more than balance qty i.e " + clsCommon.myCstr(balqty) + " at row no. " + clsCommon.myCstr(ii + 1) + ".")
                    End If

                    If ForUDLOnly AndAlso (clsCommon.myLen(taxtype) <= 0 OrElse clsCommon.CompairString(taxtype, "None") = CompairStringResult.Equal) Then
                        gv.Rows(ii).Cells(colTax).Value = "No"
                    End If

                    If AllowSchemeFlow Then
                        If ForUDLOnly AndAlso clsCommon.myCBool(gv.Rows(ii).Cells(colFOC).Value) = False AndAlso rate <= 0 Then
                            gv.CurrentRow = gv.Rows(ii)
                            gv.CurrentColumn = gv.Columns(coluomPrice)
                            Throw New Exception("Price rate not define at row no. " + clsCommon.myCstr(ii + 1) + "")
                        End If
                        If Not ForUDLOnly AndAlso clsCommon.myCBool(gv.Rows(ii).Cells(colFOC).Value) = False AndAlso rate <= 0 Then
                            gv.CurrentRow = gv.Rows(ii)
                            gv.CurrentColumn = gv.Columns(coluomPrice)
                            Throw New Exception("Fill unit rate at row no. " + clsCommon.myCstr(ii + 1) + "")
                        End If
                    Else
                        If Not ForUDLOnly AndAlso rate <= 0 Then
                            gv.CurrentRow = gv.Rows(ii)
                            gv.CurrentColumn = gv.Columns(coluomPrice)
                            Throw New Exception("Fill unit rate at row no. " + clsCommon.myCstr(ii + 1) + "")
                        End If
                        If ForUDLOnly AndAlso rate <= 0 Then
                            gv.CurrentRow = gv.Rows(ii)
                            gv.CurrentColumn = gv.Columns(coluomPrice)
                            Throw New Exception("Price rate not define at row no. " + clsCommon.myCstr(ii + 1) + "")
                        End If

                        If Not ForUDLOnly AndAlso (clsCommon.myLen(taxtype) <= 0 OrElse clsCommon.CompairString(taxtype, "None") = CompairStringResult.Equal) Then
                            Throw New Exception("Fill including tax status at row no. " + clsCommon.myCstr(ii + 1) + "")
                        End If
                    End If



                    If strShowCSARequest AndAlso clsCommon.myLen(requestno) > 0 Then
                        qry = "select booking_date from TSPL_CSA_BOOKING_HEAD where booking_no='" + requestno + "' and trans_type='Request'"
                        requestdate = clsCommon.myCDate(clsDBFuncationality.getSingleValue(qry))

                        If clsCommon.myCDate(requestdate) > clsCommon.myCDate(dtpdate.Text) Then
                            gv.CurrentRow = gv.Rows(ii)
                            gv.CurrentColumn = gv.Columns(colCSARequest_No)
                            Throw New Exception("Selected CSA Request should be less than or equal to document date,see at row no. " + clsCommon.myCstr(ii + 1) + "")
                        End If
                    End If


                    For jj As Integer = ii + 1 To gv.Rows.Count - 1
                        oldicode = clsCommon.myCstr(gv.Rows(jj).Cells(colItemCode).Value)

                        If AllowSchemeFlow Then
                            If clsCommon.myCBool(gv.Rows(jj).Cells(colFOC).Value) = False Then
                                If clsCommon.CompairString(icode, oldicode) = CompairStringResult.Equal AndAlso clsCommon.myCBool(gv.Rows(ii).Cells(colFOC).Value) = False Then
                                    Throw New Exception("Main Item should not be duplicate,see at row no. " + clsCommon.myCstr(jj + 1) + "")
                                End If
                            End If
                        Else
                            If clsCommon.CompairString(icode, oldicode) = CompairStringResult.Equal Then
                                Throw New Exception("Duplicate item at row no. " + clsCommon.myCstr(jj + 1) + "")
                            End If
                        End If
                    Next
                End If
            Next

            RefreshSerialNo()
            CalDocumentAmt()

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        If AllowToSave() Then SaveData(False, False)
    End Sub

    Private Function SaveData(ByVal isPost As Boolean, ByVal isDOAmended As Boolean) As Boolean
        Try
            'clsCommon.ProgressBarShow()
            'done by stuti on 06/12/2016 for approval work
            Dim totalqty As Decimal = 0
            Dim objApproval As New clsApply_Approval()
            If AllowNLevel Then
                If Not AllowModifcationByApprovalUser Then
                    clsApply_Approval.CheckUpdate_Doc_Valid(clsUserMgtCode.frmCSADeliveryOrder, clsCommon.myCstr(txtCode.Value))
                End If
                objApproval.ToLoc = clsCommon.myCstr(txttoloc_code.Value)
                objApproval.CustCode = clsCommon.myCstr(txtcustcode.Value)
                objApproval.TotAmt = clsCommon.myCdbl(txttotal_amt.Text)
                objApproval.DocCode = clsCommon.myCstr(txtCode.Value)
                objApproval.DocDate = clsCommon.myCDate(dtpdate.Text)
            Else
                ''========================================================================
                If Not isPost AndAlso Not isDOAmended Then
                    ''=============checkApproval Status===================
                    Dim check As Integer = 0
                    If CheckCreditLimit AndAlso Not AllowNLevel Then
                        ''check record exist or not
                        check = clsDBFuncationality.getSingleValue("select count(*) from TSPL_TRANSACTION_APPROVAL where screen_name='CSA Delivery Order' and program_code='" + clsUserMgtCode.frmCSADeliveryOrder + "' and document_no='" + clsCommon.myCstr(txtCode.Value) + "' ")
                        If check > 0 Then ''if record exist then
                            Dim NewLimt As Double = clsCSADeliveryOrder.CustomerOutstandingAmount(clsCommon.myCstr(txttoloc_code.Value), clsCommon.myCstr(txtcustcode.Value), clsCommon.myCdbl(txttotal_amt.Text), Nothing, clsCommon.myCstr(txtCode.Value), clsCommon.myCDate(dtpdate.Text))
                            If NewLimt <= 0 Then
                                If clsCommon.MyMessageBoxShow(Me, "Any updation in document need approval by " + clsCommon.myCstr(NewLimt) + " credit limit," + Environment.NewLine + "Are you sure want to send document for approval?", "Attention", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
                                    Return False
                                End If
                            End If
                        End If ''end document cond.
                    End If
                End If
                ''=====================================================================
            End If
            ''end here===================

            Dim obj As New clsCSADeliveryOrder()
            obj.Arr = New List(Of clsCSADeliveryOrderDetail)

            obj.docno = clsCommon.myCstr(txtCode.Value)
            obj.docdate = clsCommon.myCDate(dtpdate.Text)
            obj.cust_code = clsCommon.myCstr(txtcustcode.Value)
            obj.frm_loc_code = clsCommon.myCstr(txtfrmloc_code.Value)
            obj.to_loc_code = clsCommon.myCstr(txttoloc_code.Value)
            obj.state_code = clsCommon.myCstr(txtstate_code.Value)
            obj.trans_rate = clsCommon.myCdbl(txtrate.Text)
            obj.tax = clsCommon.myCstr(cmbTax.SelectedValue)
            obj.doc_amt = clsCommon.myCdbl(txttotal_amt.Text)
            obj.csa_header_type = clsCommon.myCstr(cmbCSAType.SelectedValue)
            obj.RT_UOM = clsCommon.myCstr(txtRt_UOM.Text)
            obj.CSA_Request_No = clsCommon.myCstr(fndRequestNo.Value)
            obj.isDOAmended = isDOAmended

            For Each grow As GridViewRowInfo In gv.Rows
                Dim objtr As New clsCSADeliveryOrderDetail()

                objtr.lineno = CInt(grow.Cells(colLineno).Value)
                objtr.icode = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                objtr.uom = clsCommon.myCstr(grow.Cells(colUOM).Value)
                objtr.qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                objtr.unit_rate = clsCommon.myCdbl(grow.Cells(coluomPrice).Value)
                objtr.tax = clsCommon.myCstr(grow.Cells(colTax).Value)
                objtr.toltalamt = clsCommon.myCdbl(grow.Cells(colTotalPrice).Value)
                objtr.remarks = clsCommon.myCstr(grow.Cells(colRemarks).Value).Replace("'", "`")
                objtr.CSA_Request_No = clsCommon.myCstr(grow.Cells(colCSARequest_No).Value)
                objtr.Pending_Qty = clsCommon.myCdbl(grow.Cells(colBal_Qty).Value)

                If AllowSchemeFlow Then
                    ''================scheme item=========================
                    objtr.Scheme_Applicable = IIf(clsCommon.myCstr(grow.Cells(colIsSchmItem).Value) = "Y", True, False)
                    objtr.FOC = clsCommon.myCBool(grow.Cells(colFOC).Value)
                    objtr.Scheme_Code = clsCommon.myCstr(grow.Cells(colSchmCode).Value)
                    objtr.Scheme_Type = clsCommon.myCstr(grow.Cells(colSchmCodeType).Value)
                    objtr.Scheme_Item_Code = clsCommon.myCstr(grow.Cells(colMainIcode).Value)
                    objtr.Scheme_Item_UOM = clsCommon.myCstr(grow.Cells(colMainIUOM).Value)
                    objtr.Scheme_Qty = clsCommon.myCdbl(grow.Cells(colMainIQty).Value)
                    objtr.Cash_Scheme_Code = clsCommon.myCstr(grow.Cells(colCashSchemeCode).Value)
                    objtr.Cash_Scheme_Type = clsCommon.myCstr(grow.Cells(colCashSchemeType).Value)
                    objtr.Cash_Scheme_Pers = clsCommon.myCdbl(grow.Cells(colCash_Pers).Value)
                    objtr.Cash_Scheme_Amount = clsCommon.myCdbl(grow.Cells(colCash_Amt).Value)
                    ''====================end here======================
                End If

                If clsCommon.myLen(objtr.icode) > 0 Then
                    obj.Arr.Add(objtr)
                    totalqty += clsCommon.myCdbl(grow.Cells(colQty).Value)
                End If
            Next

            Dim xvalue As Boolean = False
            If isPost OrElse isDOAmended Then
                xvalue = True
            End If
            If clsCSADeliveryOrder.SaveData(obj, xvalue, isNewentry) Then
                'clsCommon.ProgressBarHide()
                If Not isPost AndAlso Not isDOAmended Then
                    clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                End If

                txtCode.Value = obj.docno
                UcAttachment1.SaveData(txtCode.Value)

                ''done by stuti approval work 01/12/2016
                If AllowNLevel Then
                    clsApply_Approval.CheckApprovalRequired(clsUserMgtCode.frmCSADeliveryOrder, clsCommon.myCstr(obj.docno), dtpdate.Text, "", "", clsCommon.myCdbl(txttotal_amt.Text), clsCommon.myCdbl(totalqty), "", objApproval)
                End If

                '================================================================

                If Not isDOAmended Then
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If

                Return True
            Else
                Return False
            End If
            'clsCommon.ProgressBarHide()
        Catch ex As Exception
            'clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnpost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpost.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                txtCode.Focus()
                txtCode.Select()
                Errorcontrol.SetError(txtCode, "Select document for posting first.")
                Throw New Exception("Select document for posting first.")
            Else
                Errorcontrol.ResetError(txtCode)
            End If

            If Not (myMessages.postConfirm()) Then
                Return
            End If

            ''=============checkApproval Status===================
            Dim check As Integer = 0
            Dim approved As Boolean = True
            If CheckCreditLimit AndAlso Not AllowNlevel Then
                ''check record exist or not
                check = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_TRANSACTION_APPROVAL where screen_name='CSA Delivery Order' and program_code='" + clsUserMgtCode.frmCSADeliveryOrder + "' and document_no='" + clsCommon.myCstr(txtCode.Value) + "' "))
                If check > 0 Then ''if record exist then
                    check = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_TRANSACTION_APPROVAL where screen_name='CSA Delivery Order' and program_code='" + clsUserMgtCode.frmCSADeliveryOrder + "' and document_no='" + clsCommon.myCstr(txtCode.Value) + "' and isnull(Approve,0)<>1"))
                    If check > 0 Then
                        approved = False
                        Dim NewLimt As Double = clsCSADeliveryOrder.CustomerOutstandingAmount(clsCommon.myCstr(txttoloc_code.Value), clsCommon.myCstr(txtcustcode.Value), clsCommon.myCdbl(txttotal_amt.Text), Nothing, clsCommon.myCstr(txtCode.Value), clsCommon.myCDate(dtpdate.Text))
                        clsCommon.MyMessageBoxShow(Me, "Document required Approval for Credit Limit,before post." + Environment.NewLine + "(for this goto --> Transaction Approval)" + Environment.NewLine + "increase credit limit " + clsCommon.myCstr(NewLimt) + " for customer " + clsCommon.myCstr(txtcustName.Text) + ".")
                        Exit Sub
                    Else
                        approved = True
                    End If
                Else
                    approved = False
                End If ''end document cond.
            End If
            ''====================================================

            If AllowToSave() Then
                If SaveData(True, False) Then

                    ''=============check credit limit===========================
                    If CheckCreditLimit AndAlso Not AllowNLevel Then
                        Dim NewLimt As Double = clsCSADeliveryOrder.CustomerOutstandingAmount(clsCommon.myCstr(txttoloc_code.Value), clsCommon.myCstr(txtcustcode.Value), clsCommon.myCdbl(txttotal_amt.Text), Nothing, clsCommon.myCstr(txtCode.Value), clsCommon.myCDate(dtpdate.Text))
                        If Not approved AndAlso clsCommon.myCdbl(NewLimt) <= 0 Then
                            clsCommon.MyMessageBoxShow(Me, "Document required Approval for Credit Limit,before post." + Environment.NewLine + "(for this goto --> Transaction Approval)" + Environment.NewLine + "increase credit limit " + clsCommon.myCstr(NewLimt) + " for customer " + clsCommon.myCstr(txtcustName.Text) + ".")
                            Exit Sub
                        End If
                    End If
                    ''==============================================================


                    If clsCSADeliveryOrder.PostData(txtCode.Value) Then
                        clsCommon.MyMessageBoxShow(Me, "Data Posted Successfully", Me.Text)

                        LoadData(txtCode.Value, NavigatorType.Current)
                        'If clsSMSAtPost_Sales.SMSATPOST_SALE() Then
                        '    SMSSENDONLY(True)
                        'End If
                    End If
                End If
            End If


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FrmCSADeliveryOrder_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.Alt AndAlso e.KeyCode = Keys.N Then
                FunReset()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
                btnsave.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
                btndelete.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
                btnpost.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
                Me.Close()
            ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F11 Then
                Dim frm As New FrmPWD(Nothing)
                frm.strType = "POAmendmentType"
                frm.strCode = "POAmendment"
                frm.ShowDialog()

                btnAmendment.Visible = False
                If frm.isPasswordCorrect AndAlso MyBase.isPostFlag AndAlso UsLock1.Status = ERPTransactionStatus.Approved Then
                    btnAmendment.Visible = True
                End If
            End If

            If e.KeyCode = Keys.F2 AndAlso gv.CurrentColumn IsNot Nothing AndAlso gv.CurrentColumn Is gv.Columns(colItemCode) Then
                isCellValueChanged = True
                If clsCommon.myLen(gv.CurrentRow.Cells(colCSARequest_No).Value) <= 0 Then
                    OpenIcode(True)
                End If
                isCellValueChanged = False
            End If
            If e.KeyCode = Keys.F2 AndAlso gv.CurrentColumn IsNot Nothing AndAlso gv.CurrentColumn Is gv.Columns(colUOM) Then
                isCellValueChanged = True
                If clsCommon.myLen(gv.CurrentRow.Cells(colCSARequest_No).Value) <= 0 Then
                    OpenUOM(True)
                End If
                isCellValueChanged = False
            End If
            If e.KeyCode = Keys.F2 AndAlso gv.CurrentColumn IsNot Nothing AndAlso ((gv.CurrentColumn Is gv.Columns(colUOM)) OrElse (gv.CurrentColumn Is gv.Columns(colQty)) OrElse (gv.CurrentColumn Is gv.Columns(colTax))) Then
                isCellValueChanged = True
                CalDiffrate(CInt(gv.CurrentRow.Index), True)
                isCellValueChanged = False
            End If
            If e.KeyCode = Keys.F2 AndAlso gv.CurrentColumn IsNot Nothing AndAlso gv.CurrentColumn Is gv.Columns(coluomPrice) Then
                isCellValueChanged = True
                gv.CurrentRow.Cells(colTotalPrice).Value = clsCommon.myCdbl(gv.CurrentRow.Cells(coluomPrice).Value) * clsCommon.myCdbl(gv.CurrentRow.Cells(colQty).Value)
                isCellValueChanged = False
            End If
        Catch ex As Exception
            isCellValueChanged = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Function LoadComboBox() As DataTable
        Dim qry As String = "select '' as Code,'None' as Name union all select 'Yes' as Code,'Yes' as Name union all select 'No' as Code,'No' as Name"
        Dim dt As New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry)

        Return dt
    End Function

    Private Sub LoadComboCSABox()
        Dim qry As String = "select 'CPD-DESI GHEE' as Code,'CPD-DESI GHEE' as Name " ' select 'None' as Code,'None' as Name union all  union all select 'BULK-DESI GHEE' as Code,'BULK-DESI GHEE' as Name union all select 'CPD-OTHER' as Code,'CPD-OTHER' as Name union all select 'BULK-OTHER' as Code,'BULK-OTHER' as Name
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        cmbCSAType.DataSource = Nothing

        cmbCSAType.DataSource = dt
        cmbCSAType.DisplayMember = "Name"
        cmbCSAType.ValueMember = "Code"
    End Sub

    Private Sub FrmCSADeliveryOrder_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AllowNLevel = clsApply_Approval.AllowNlevelonScreen(clsUserMgtCode.frmCSADeliveryOrder)
        SetUserMgmtNew()

        CSAPricePostedData = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowCSAPriceMasterPostedData, clsFixedParameterCode.AllowCSAPriceMasterPostedData, Nothing)) = 1, True, False)
        strShowCSARequest = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowCSARequestScreen, clsFixedParameterCode.ShowCSARequestScreen, Nothing)) = 1, True, False)
        AllowSchemeFlow = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowSchemeOnCSADeliveryOrder, clsFixedParameterCode.AllowSchemeOnCSADeliveryOrder, Nothing)) = 1, True, False)
        Apply_PriceChat_On_OtherItems = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowOtherItemOnCSAPriceMaster, clsFixedParameterCode.AllowOtherItemOnCSAPriceMaster, Nothing)) = 1, True, False)
        AllowRate_Readonly = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DoReadonly_UnitRate_AtCSASale, clsFixedParameterCode.DoReadonly_UnitRate_AtCSASale, Nothing)) = 1, True, False)
        ForUDLOnly = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ForUDLOnly, clsFixedParameterCode.ForUDLOnly, Nothing)) = 1, True, False)
        CheckCreditLimit = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CheckCreditLimitonCSADO, clsFixedParameterCode.CheckCreditLimitonCSADO, Nothing)) = 1, True, False)

        If Not strShowCSARequest Then
            MyLabel5.Visible = False
            fndRequestNo.Visible = False
        Else
            MyLabel5.Visible = True
            fndRequestNo.Visible = True
        End If

        ''=========================FOR UDL==================================================================
        Panel1.Visible = Not ForUDLOnly
        MyLabel1.Visible = Not ForUDLOnly
        txttotal_amt.Visible = Not ForUDLOnly
        MyLabel8.Visible = Not ForUDLOnly
        txtrate.Visible = Not ForUDLOnly
        txtRt_UOM.Visible = Not ForUDLOnly
        ''=======================end here====================================================================

        cmbTax.DataSource = Nothing
        cmbTax.DataSource = LoadComboBox()
        cmbTax.DisplayMember = "Name"
        cmbTax.ValueMember = "Code"

        LoadComboCSABox()
        LoadBlankGrid()
        FunReset()

        txtstate_code.MendatroryField = False
        txtstate_code.Enabled = False
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for save record.")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D for delete record.")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C for close window.")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+P for post record.")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N for reset window.")


        strFromDrillDown = False
        If clsCommon.myLen(StrDocNo) > 0 Then
            strFromDrillDown = True
            LoadData(StrDocNo, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            strFromDrillDown = True
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Private Sub LoadBlankGrid()
        gv.Rows.Clear()
        gv.Columns.Clear()

        Dim repolineno As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repolineno.Name = colLineno
        repolineno.FormatString = ""
        repolineno.Width = 80
        'repolineno.DecimalPlaces = 0
        repolineno.HeaderText = "S.No."
        repolineno.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repolineno)

        Dim repoitemcode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoitemcode.Name = colItemCode
        repoitemcode.FormatString = ""
        repoitemcode.Width = 110
        repoitemcode.HeaderText = "Item Code"
        repoitemcode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoitemcode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv.MasterTemplate.Columns.Add(repoitemcode)

        Dim repoitemname As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoitemname.Name = colitemname
        repoitemname.FormatString = ""
        repoitemname.Width = 300
        repoitemname.HeaderText = "Description"
        repoitemname.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoitemname)

        Dim repoIHSN As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIHSN.FormatString = ""
        repoIHSN.HeaderText = "HSN Code"
        repoIHSN.Name = colIHSN
        repoIHSN.Width = 150
        repoIHSN.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoIHSN)

        Dim repouom As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repouom.Name = colUOM
        repouom.FormatString = ""
        repouom.Width = 80
        repouom.HeaderText = "UOM"
        repouom.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repouom.TextImageRelation = TextImageRelation.TextBeforeImage
        repouom.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repouom)

        Dim repodiff As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repodiff.Name = colCSAType
        repodiff.FormatString = ""
        repodiff.Width = 80
        repodiff.HeaderText = "CSA Item Type"
        repodiff.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repodiff)

        Dim repoltrpercase As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoltrpercase.Name = colQty
        repoltrpercase.FormatString = ""
        repoltrpercase.Width = 80
        repoltrpercase.HeaderText = "Quantity"
        repoltrpercase.DecimalPlaces = 2
        gv.MasterTemplate.Columns.Add(repoltrpercase)

        repoltrpercase = New GridViewDecimalColumn()
        repoltrpercase.Name = colBal_Qty
        repoltrpercase.FormatString = ""
        repoltrpercase.Width = 80
        repoltrpercase.HeaderText = "Pending Qty"
        repoltrpercase.DecimalPlaces = 2
        repoltrpercase.ReadOnly = True
        repoltrpercase.WrapText = True
        repoltrpercase.IsVisible = strShowCSARequest
        repoltrpercase.VisibleInColumnChooser = strShowCSARequest
        gv.MasterTemplate.Columns.Add(repoltrpercase)

        repodiff = New GridViewTextBoxColumn()
        repodiff.Name = colCSARequest_No
        repodiff.FormatString = ""
        repodiff.Width = 110
        repodiff.HeaderText = "CSA Request No"
        repodiff.ReadOnly = True
        repodiff.WrapText = True
        repodiff.VisibleInColumnChooser = strShowCSARequest
        repodiff.IsVisible = strShowCSARequest
        gv.MasterTemplate.Columns.Add(repodiff)

        Dim repopcspercase As GridViewDecimalColumn = New GridViewDecimalColumn()
        repopcspercase.Name = coluomPrice
        repopcspercase.FormatString = ""
        repopcspercase.Width = 80
        repopcspercase.HeaderText = "Unit Price"
        repopcspercase.DecimalPlaces = 2
        repopcspercase.ReadOnly = AllowRate_Readonly
        repopcspercase.VisibleInColumnChooser = True
        repopcspercase.IsVisible = True
        gv.MasterTemplate.Columns.Add(repopcspercase)

        'If AllowSchemeFlow Then
        '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>Scheme Columns>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

        Dim repoIsSchmItem As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoIsSchmItem.FormatString = ""
        repoIsSchmItem.HeaderText = "Scheme Applicable(Y/N)"
        repoIsSchmItem.Name = colIsSchmItem
        repoIsSchmItem.Width = 50
        repoIsSchmItem.DataSource = clsDBFuncationality.GetDataTable("select 'Y' as Code,'Y' as Name union all select 'N' as Code,'N' as Name")
        repoIsSchmItem.DisplayMember = "Name"
        repoIsSchmItem.ValueMember = "Code"
        repoIsSchmItem.VisibleInColumnChooser = AllowSchemeFlow
        repoIsSchmItem.IsVisible = AllowSchemeFlow
        gv.MasterTemplate.Columns.Add(repoIsSchmItem)

        Dim repoIsSchmItem13 As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoIsSchmItem13.FormatString = ""
        repoIsSchmItem13.HeaderText = "Scheme Type"
        repoIsSchmItem13.Name = colSchmCodeType
        repoIsSchmItem13.Width = 50
        repoIsSchmItem13.ReadOnly = False
        repoIsSchmItem13.DataSource = clsSchemeApplyOnDairy.GetSchemeTypes()
        repoIsSchmItem13.DisplayMember = "Code"
        repoIsSchmItem13.ValueMember = "Name"
        repoIsSchmItem13.VisibleInColumnChooser = AllowSchemeFlow
        repoIsSchmItem13.IsVisible = AllowSchemeFlow
        gv.MasterTemplate.Columns.Add(repoIsSchmItem13)

        Dim repoIsSchmItem1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIsSchmItem1.FormatString = ""
        repoIsSchmItem1.HeaderText = "Scheme Code"
        repoIsSchmItem1.Name = colSchmCode
        repoIsSchmItem1.Width = 50
        repoIsSchmItem1.ReadOnly = True
        repoIsSchmItem1.VisibleInColumnChooser = AllowSchemeFlow
        repoIsSchmItem1.IsVisible = AllowSchemeFlow
        gv.MasterTemplate.Columns.Add(repoIsSchmItem1)

        Dim repoIsSchmItem2 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSchmItem2.FormatString = ""
        repoIsSchmItem2.HeaderText = "Is FOC"
        repoIsSchmItem2.Name = colFOC
        repoIsSchmItem2.Width = 50
        repoIsSchmItem2.ReadOnly = True
        repoIsSchmItem2.ThreeState = False
        repoIsSchmItem2.VisibleInColumnChooser = AllowSchemeFlow
        repoIsSchmItem2.IsVisible = AllowSchemeFlow
        gv.MasterTemplate.Columns.Add(repoIsSchmItem2)

        Dim repoIsSchmItem3 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIsSchmItem3.FormatString = ""
        repoIsSchmItem3.HeaderText = "Main Item Code"
        repoIsSchmItem3.Name = colMainIcode
        repoIsSchmItem3.Width = 50
        repoIsSchmItem3.IsVisible = False
        repoIsSchmItem3.VisibleInColumnChooser = False
        gv.MasterTemplate.Columns.Add(repoIsSchmItem3)

        Dim repoIsSchmItem4 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIsSchmItem4.FormatString = ""
        repoIsSchmItem4.HeaderText = "Main Item UOM"
        repoIsSchmItem4.Name = colMainIUOM
        repoIsSchmItem4.Width = 50
        repoIsSchmItem4.IsVisible = False
        repoIsSchmItem4.VisibleInColumnChooser = False
        gv.MasterTemplate.Columns.Add(repoIsSchmItem4)

        Dim repoIsSchmItem5 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIsSchmItem5.FormatString = ""
        repoIsSchmItem5.HeaderText = "Main Item Qty"
        repoIsSchmItem5.Name = colMainIQty
        repoIsSchmItem5.Width = 50
        repoIsSchmItem5.IsVisible = False
        repoIsSchmItem5.VisibleInColumnChooser = False
        gv.MasterTemplate.Columns.Add(repoIsSchmItem5)

        Dim repoIsSchmItem1c As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIsSchmItem1c.FormatString = ""
        repoIsSchmItem1c.HeaderText = "Cash Scheme Code"
        repoIsSchmItem1c.Name = colCashSchemeCode
        repoIsSchmItem1c.Width = 50
        repoIsSchmItem1c.ReadOnly = True
        repoIsSchmItem1c.IsVisible = AllowSchemeFlow
        repoIsSchmItem1c.VisibleInColumnChooser = AllowSchemeFlow
        gv.MasterTemplate.Columns.Add(repoIsSchmItem1c)

        Dim repoIsSchmItem1c1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIsSchmItem1c1.FormatString = ""
        repoIsSchmItem1c1.HeaderText = "Cash Scheme Type"
        repoIsSchmItem1c1.Name = colCashSchemeType
        repoIsSchmItem1c1.Width = 50
        repoIsSchmItem1c1.ReadOnly = True
        repoIsSchmItem1c1.IsVisible = False
        repoIsSchmItem1c1.VisibleInColumnChooser = False
        gv.MasterTemplate.Columns.Add(repoIsSchmItem1c1)

        Dim repoIsSchmItem1c2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoIsSchmItem1c2.FormatString = ""
        repoIsSchmItem1c2.HeaderText = "Cash %"
        repoIsSchmItem1c2.Name = colCash_Pers
        repoIsSchmItem1c2.Width = 50
        repoIsSchmItem1c2.ReadOnly = True
        repoIsSchmItem1c2.VisibleInColumnChooser = AllowSchemeFlow
        repoIsSchmItem1c2.IsVisible = AllowSchemeFlow
        gv.MasterTemplate.Columns.Add(repoIsSchmItem1c2)

        Dim repoIsSchmItem1c3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoIsSchmItem1c3.FormatString = ""
        repoIsSchmItem1c3.HeaderText = "Cash Amount"
        repoIsSchmItem1c3.Name = colCash_Amt
        repoIsSchmItem1c3.Width = 50
        repoIsSchmItem1c3.ReadOnly = True
        repoIsSchmItem1c3.VisibleInColumnChooser = AllowSchemeFlow
        repoIsSchmItem1c3.IsVisible = AllowSchemeFlow
        gv.MasterTemplate.Columns.Add(repoIsSchmItem1c3)
        '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        'End If

        'Dim repopcsrate As GridViewDecimalColumn = New GridViewDecimalColumn()
        'repopcsrate.Name = colMRP
        'repopcsrate.FormatString = ""
        'repopcsrate.Width = 80
        'repopcsrate.HeaderText = "MRP"
        'repopcsrate.ReadOnly = True
        'gv.MasterTemplate.Columns.Add(repopcsrate)

        Dim repoltrrate As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoltrrate.Name = colTax
        repoltrrate.FormatString = ""
        repoltrrate.Width = 80
        repoltrrate.HeaderText = "Including Tax"
        repoltrrate.DataSource = LoadComboBox()
        repoltrrate.DisplayMember = "Name"
        repoltrrate.ValueMember = "Code"
        repoltrrate.IsVisible = Not ForUDLOnly
        repoltrrate.VisibleInColumnChooser = Not ForUDLOnly
        gv.MasterTemplate.Columns.Add(repoltrrate)

        Dim repocaserate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repocaserate.Name = colTotalPrice
        repocaserate.FormatString = ""
        repocaserate.Width = 80
        repocaserate.HeaderText = "Total Amount"
        repocaserate.ReadOnly = True
        repocaserate.DecimalPlaces = 2
        repocaserate.IsVisible = Not ForUDLOnly
        repocaserate.VisibleInColumnChooser = Not ForUDLOnly
        gv.MasterTemplate.Columns.Add(repocaserate)

        Dim repocaserate1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repocaserate1.Name = colRemarks
        repocaserate1.FormatString = ""
        repocaserate1.Width = 150
        repocaserate1.HeaderText = "Remarks"
        repocaserate1.MaxLength = 200
        gv.MasterTemplate.Columns.Add(repocaserate1)

        gv.AllowDeleteRow = True
        gv.AllowAddNewRow = False
        gv.ShowGroupPanel = False
        gv.AllowColumnReorder = True
        gv.AllowRowReorder = False
        gv.EnableSorting = False
        'gv.ColumnChooser = True
        gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv.MasterTemplate.ShowRowHeaderColumn = False

        ReStoreGridLayout()
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        FunReset()
    End Sub

    Private Sub txtcustcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtcustcode._MYValidating
        txtcustcode.Value = clsCustomerMaster.getFinder(" isnull(tspl_customer_master.csa_type,'N')='Y'", txtcustcode.Value, isButtonClicked)

        If clsCommon.myLen(txtcustcode.Value) > 0 Then
            txtcustName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select customer_name from tspl_customer_master where cust_code='" + txtcustcode.Value + "'"))
            txttoloc_code.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select location_code from tspl_location_master where cust_code='" + txtcustcode.Value + "' and isnull(tspl_location_master.csa_type,'N')='Y'")) ' and tspl_location_master.location_code in (" + arrLoc + ")
            txttoloc_name.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select location_desc from tspl_location_master where location_code='" + txttoloc_code.Value + "'"))
            txtstate_code.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select state from tspl_location_master where location_code='" + txttoloc_code.Value + "'"))
            txtstate_name.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select state_name from tspl_state_master where state_code='" + txtstate_code.Value + "'"))
            txtRt_UOM.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select unit_code from tspl_unit_master where isnull(RT_Rate,'N')='Y'"))
        Else
            txtcustName.Text = ""
            txttoloc_code.Value = ""
            txttoloc_name.Text = ""
            txtRt_UOM.Text = ""
            txtstate_code.Value = ""
            txtstate_name.Text = ""
        End If

        CalDiffrate(Nothing, False)
    End Sub

    Private Sub txtfrmloc_code__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtfrmloc_code._MYValidating
        Dim WhrCls As String = " coalesce(ltrim(Rejected_Type),'') in ('N','') and coalesce(ltrim(GIT_Type),'') in ('N','') and Is_Section='N' and Is_Sub_Location='N'  "
        If clsCommon.myLen(arrLoc) > 0 Then
            WhrCls += "  and  Location_Code in (" + arrLoc + ")"
        End If
        'txtfrmloc_code.Value = clsLocation.getFinder(" tspl_location_master.type='Plant' and tspl_location_master.location_code in (" + arrLoc + ")", txtfrmloc_code.Value, isButtonClicked)
        txtfrmloc_code.Value = clsLocation.getFinder(WhrCls, txtfrmloc_code.Value, isButtonClicked)

        If clsCommon.myLen(txtfrmloc_code.Value) > 0 Then
            If clsCommon.CompairString(txtfrmloc_code.Value, txttoloc_code.Value) = CompairStringResult.Equal Then
                clsCommon.MyMessageBoxShow(Me, "From Location and To Location can not be same.", Me.Text)
                txtfrmloc_code.Value = ""
                txtfrmloc_name.Text = ""
                Return
            End If
            txtfrmloc_name.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select location_desc from tspl_location_master where location_code='" + txtfrmloc_code.Value + "'"))
            CalDiffrate(0, False)
        Else
            txtfrmloc_code.Value = ""
            txtfrmloc_name.Text = ""
        End If

        CalDiffrate(Nothing, False)
    End Sub

    Private Sub txttoloc_code__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txttoloc_code._MYValidating
        txttoloc_code.Value = clsLocation.getFinder(" isnull(tspl_location_master.csa_type,'N')='Y' and tspl_location_master.location_code in (" + arrLoc + ") ", txttoloc_code.Value, isButtonClicked)

        If clsCommon.myLen(txttoloc_code.Value) > 0 Then
            If clsCommon.CompairString(txtfrmloc_code.Value, txttoloc_code.Value) = CompairStringResult.Equal Then
                clsCommon.MyMessageBoxShow(Me, "From Location and To Location can not be same.", Me.Text)
                txttoloc_code.Value = ""
                txttoloc_name.Text = ""
                Return
            End If
            txttoloc_name.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select location_desc from tspl_location_master where location_code='" + txttoloc_code.Value + "'"))
            txtstate_code.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select state from tspl_location_master where location_code='" + txttoloc_code.Value + "'"))
            txtstate_name.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select state_name from tspl_state_master where state_code='" + txtstate_code.Value + "'"))
        Else
            txttoloc_code.Value = ""
            txttoloc_name.Text = ""
            txtstate_code.Value = ""
            txtstate_name.Text = ""
        End If
    End Sub

    Private Sub txtstate_code__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtstate_code._MYValidating
        'txtstate_code.Value = ClsStageMaster.GetFinder("", txtstate_code.Value, isButtonClicked)
        'txtstate_name.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select state_name from tspl_state_master where state_code='" + txtstate_code.Value + "'"))
    End Sub

    Private Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Try
            'clsCommon.ProgressBarShow()
            Dim objApproval As New clsApply_Approval()
            Dim obj As clsCSADeliveryOrder = clsCSADeliveryOrder.GetData(strCode, arrLoc, NavType)
            isNewentry = True
            isInsideLoadData = True
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.docno) > 0 Then
                isNewentry = False
                txtfrmloc_code.Enabled = False
                txtcustcode.Enabled = False

                txtCode.Value = obj.docno
                dtpdate.Text = obj.docdate
                txtcustcode.Value = obj.cust_code
                txtcustName.Text = obj.cust_name
                txtfrmloc_code.Value = obj.frm_loc_code
                txtfrmloc_name.Text = obj.frm_loc_name
                txttoloc_code.Value = obj.to_loc_code
                txttoloc_name.Text = obj.to_loc_name
                txtstate_code.Value = obj.state_code
                txtstate_name.Text = obj.state_name
                txtrate.Text = obj.trans_rate
                cmbTax.SelectedValue = obj.tax
                txttotal_amt.Text = obj.doc_amt
                cmbCSAType.SelectedValue = obj.csa_header_type
                txtRt_UOM.Text = obj.RT_UOM
                fndRequestNo.Value = obj.CSA_Request_No

                gv.Rows.Clear()
                gv.Rows.AddNew()

                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objtr As clsCSADeliveryOrderDetail In obj.Arr
                        gv.Rows(gv.Rows.Count - 1).Cells(colLineno).Value = clsCommon.myCstr(objtr.lineno)
                        gv.Rows(gv.Rows.Count - 1).Cells(colItemCode).Value = objtr.icode
                        gv.Rows(gv.Rows.Count - 1).Cells(colitemname).Value = objtr.iname
                        gv.Rows(gv.Rows.Count - 1).Cells(colIHSN).Value = clsItemMaster.GetItemHSNCode(objtr.icode, Nothing)
                        gv.Rows(gv.Rows.Count - 1).Cells(colUOM).Value = objtr.uom
                        gv.Rows(gv.Rows.Count - 1).Cells(colCSAType).Value = objtr.csa_type
                        gv.Rows(gv.Rows.Count - 1).Cells(colQty).Value = objtr.qty
                        gv.Rows(gv.Rows.Count - 1).Cells(coluomPrice).Value = objtr.unit_rate
                        gv.Rows(gv.Rows.Count - 1).Cells(colTax).Value = objtr.tax
                        gv.Rows(gv.Rows.Count - 1).Cells(colTotalPrice).Value = objtr.toltalamt
                        gv.Rows(gv.Rows.Count - 1).Cells(colRemarks).Value = objtr.remarks

                        gv.Rows(gv.Rows.Count - 1).Cells(colCSARequest_No).Value = objtr.CSA_Request_No
                        gv.Rows(gv.Rows.Count - 1).Cells(colBal_Qty).Value = clsCSADeliveryOrderDetail.GetBalanceRequestQty(clsCommon.myCstr(txtCode.Value), clsCommon.myCDate(dtpdate.Text), objtr.CSA_Request_No, objtr.icode, Nothing)

                        If strShowCSARequest AndAlso clsCommon.myLen(objtr.CSA_Request_No) > 0 Then
                            gv.Rows(gv.Rows.Count - 1).Cells(colItemCode).ReadOnly = True
                            gv.Rows(gv.Rows.Count - 1).Cells(colUOM).ReadOnly = True
                        Else
                            gv.Rows(gv.Rows.Count - 1).Cells(colItemCode).ReadOnly = False
                            gv.Rows(gv.Rows.Count - 1).Cells(colUOM).ReadOnly = False
                        End If

                        If AllowSchemeFlow Then
                            gv.Rows(gv.Rows.Count - 1).Cells(colFOC).Value = objtr.FOC
                            gv.Rows(gv.Rows.Count - 1).Cells(colSchmCode).Value = objtr.Scheme_Code
                            gv.Rows(gv.Rows.Count - 1).Cells(colSchmCodeType).Value = objtr.Scheme_Type
                            gv.Rows(gv.Rows.Count - 1).Cells(colMainIcode).Value = objtr.Scheme_Item_Code
                            gv.Rows(gv.Rows.Count - 1).Cells(colMainIQty).Value = objtr.Scheme_Qty
                            gv.Rows(gv.Rows.Count - 1).Cells(colMainIUOM).Value = objtr.Scheme_Item_UOM
                            gv.Rows(gv.Rows.Count - 1).Cells(colIsSchmItem).Value = IIf(objtr.Scheme_Applicable = True, "Y", "N")
                            gv.Rows(gv.Rows.Count - 1).Cells(colCash_Amt).Value = objtr.Cash_Scheme_Amount
                            gv.Rows(gv.Rows.Count - 1).Cells(colCash_Pers).Value = objtr.Cash_Scheme_Pers
                            gv.Rows(gv.Rows.Count - 1).Cells(colCashSchemeCode).Value = objtr.Cash_Scheme_Code
                            gv.Rows(gv.Rows.Count - 1).Cells(colCashSchemeType).Value = objtr.Cash_Scheme_Type
                            If objtr.FOC Then
                                ''==========make readonly========================
                                For iCol As Integer = 0 To gv.Columns.Count - 1
                                    gv.Rows(gv.Rows.Count - 1).Cells(iCol).ReadOnly = True
                                Next
                                ''===========================================
                            Else
                                gv.Rows(gv.Rows.Count - 1).Cells(colQty).ReadOnly = False
                                gv.Rows(gv.Rows.Count - 1).Cells(coluomPrice).ReadOnly = AllowRate_Readonly
                            End If
                        End If

                        gv.Rows.AddNew()
                    Next
                End If

                txtCode.MyReadOnly = True
                btnsave.Text = "Update"
                btnsave.Enabled = True
                btndelete.Enabled = True
                btnpost.Enabled = True
                btnAmendment.Visible = False
                UsLock1.Status = ERPTransactionStatus.Pending
                cmbTax.Enabled = False
                UcAttachment1.LoadData(txtCode.Value)
                fndRequestNo.Enabled = False

                If clsCommon.CompairString(obj.is_post, "1") = CompairStringResult.Equal Then
                    btndelete.Enabled = False
                    btnpost.Enabled = False
                    btnsave.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Approved
                End If

                RadPageView1.SelectedPage = RadPageViewPage1
            Else
                FunReset()
            End If
            '=====================if document go for approval then no post button visible or if document contain related setting
            If AllowNLevel Then
                btnpost.Visible = MyBase.isPostFlag

                objApproval = New clsApply_Approval()
                objApproval.ToLoc = clsCommon.myCstr(txttoloc_code.Value)
                objApproval.CustCode = clsCommon.myCstr(txtcustcode.Value)
                objApproval.TotAmt = clsCommon.myCdbl(txttotal_amt.Text)
                objApproval.DocCode = clsCommon.myCstr(txtCode.Value)
                objApproval.DocDate = clsCommon.myCDate(dtpdate.Text)

                If Not clsApply_Approval.Visibility_PostButtonForApproval(clsUserMgtCode.frmCSADeliveryOrder, clsCommon.myCstr(txtCode.Value), clsCommon.myCdbl(txttotal_amt.Text), 0, "", objApproval) Then
                    btnpost.Visible = False
                    If UsLock1.Status = ERPTransactionStatus.Pending Then
                        UsLock1.Status = clsApply_Approval.ApprovalCondCheck_Doc(clsUserMgtCode.frmCSADeliveryOrder, clsCommon.myCstr(txtCode.Value), Nothing)
                    End If
                End If
            End If
            '============================================
            'clsCommon.ProgressBarHide()
        Catch ex As Exception
            isNewentry = True
            'clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        isInsideLoadData = False
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        LoadData(clsCommon.myCstr(txtCode.Value), NavType)
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Dim qry As String = "select count(*) from TSPL_CSA_DO_HEAD where doc_no='" + txtCode.Value + "'"
        Dim check As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))

        If check > 0 Then
            txtCode.MyReadOnly = True
        Else
            txtCode.MyReadOnly = False
        End If

        If txtCode.MyReadOnly OrElse isButtonClicked Then
            txtCode.Value = clsCSADeliveryOrder.GetFinder(" TSPL_CSA_DO_HEAD.from_location_code in (" + arrLoc + ")", txtCode.Value, isButtonClicked)

            If clsCommon.myLen(txtCode.Value) > 0 Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                FunReset()
            End If
        End If
    End Sub

#Region "Grid Event"

    Private Sub OpenCSAType(ByVal isButtonClicked As Boolean)
        Try
            Dim qry As String = "select 'None' as Code,'None' as Name union all select 'CPD-DESI GHEE' as Code,'CPD-DESI GHEE' as Name union all select 'BULK-DESI GHEE' as Code,'BULK-DESI GHEE' as Name union all select 'CPD-OTHER' as Code,'CPD-OTHER' as Name union all select 'BULK-OTHER' as Code,'BULK-OTHER' as Name"
            gv.CurrentRow.Cells(colCSAType).Value = clsCommon.myCstr(clsCommon.ShowSelectForm("CSAFND", qry, "Code", "", clsCommon.myCstr(gv.CurrentRow.Cells(colCSAType).Value), "Code", isButtonClicked))

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub gv_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellValueChanged
        Try
            If Not isInsideLoadData Then
                If Not isCellValueChanged Then
                    If clsCommon.myLen(txtcustcode.Value) <= 0 Then
                        txtcustcode.Focus()
                        txtcustcode.Select()
                        Throw New Exception("Select CSA Detail First.")
                    End If
                    If clsCommon.myLen(txtfrmloc_code.Value) <= 0 Then
                        txtfrmloc_code.Focus()
                        txtfrmloc_code.Select()
                        Throw New Exception("Select From Location Detail First.")
                    End If
                    If clsCommon.myLen(txttoloc_code.Value) <= 0 Then
                        txttoloc_code.Focus()
                        txttoloc_code.Select()
                        Throw New Exception("Select To Location Detail First.")
                    End If

                    If ForUDLOnly AndAlso (clsCommon.CompairString(cmbCSAType.SelectedValue, "") = CompairStringResult.Equal OrElse clsCommon.CompairString(cmbCSAType.SelectedValue, "None") = CompairStringResult.Equal) Then
                        cmbCSAType.SelectedValue = "CPD-DESI GHEE"
                    End If
                    If ForUDLOnly AndAlso (clsCommon.CompairString(cmbTax.SelectedValue, "") = CompairStringResult.Equal OrElse clsCommon.CompairString(cmbTax.SelectedValue, "None") = CompairStringResult.Equal) Then
                        cmbTax.SelectedValue = "No"
                    End If

                    If Not ForUDLOnly AndAlso (clsCommon.CompairString(cmbCSAType.SelectedValue, "") = CompairStringResult.Equal OrElse clsCommon.CompairString(cmbCSAType.SelectedValue, "None") = CompairStringResult.Equal) Then
                        cmbCSAType.Select()
                        Throw New Exception("Select CSA Item Type.")
                    End If

                    'If e.Column Is gv.Columns(colCSAType) Then
                    '    isCellValueChanged = True
                    '    OpenCSAType(False)
                    '    isCellValueChanged = False
                    'End If
                    If Not strShowCSARequest AndAlso e.Column Is gv.Columns(colItemCode) Then
                        isCellValueChanged = True
                        If AllowSchemeFlow Then
                            For ii As Integer = gv.Rows.Count - 1 To 0 Step -1
                                If clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(colIsSchmItem).Value), "N") = CompairStringResult.Equal AndAlso clsCommon.myCBool(gv.Rows(ii).Cells(colFOC).Value) = True AndAlso clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(colMainIcode).Value), clsCommon.myCstr(gv.CurrentRow.Cells(colItemCode).Value)) = CompairStringResult.Equal Then
                                    gv.Rows.RemoveAt(ii)
                                End If
                            Next
                        End If
                        OpenIcode(False)
                        isCellValueChanged = False
                    End If
                    If e.Column Is gv.Columns(colUOM) Then
                        isCellValueChanged = True
                        If AllowSchemeFlow Then
                            For ii As Integer = gv.Rows.Count - 1 To 0 Step -1
                                If clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(colIsSchmItem).Value), "N") = CompairStringResult.Equal AndAlso clsCommon.myCBool(gv.Rows(ii).Cells(colFOC).Value) = True AndAlso clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(colMainIcode).Value), clsCommon.myCstr(gv.CurrentRow.Cells(colItemCode).Value)) = CompairStringResult.Equal Then
                                    gv.Rows.RemoveAt(ii)
                                End If
                            Next
                        End If
                        OpenUOM(False)
                        isCellValueChanged = False
                    End If

                    If strShowCSARequest AndAlso e.Column Is gv.Columns(colQty) Then
                        Dim balqty As Decimal = clsCommon.myCdbl(clsCSADeliveryOrderDetail.GetBalanceRequestQty(clsCommon.myCstr(txtCode.Value), clsCommon.myCDate(dtpdate.Text), clsCommon.myCstr(gv.CurrentRow.Cells(colCSARequest_No).Value), clsCommon.myCstr(gv.CurrentRow.Cells(colItemCode).Value), Nothing))
                        If clsCommon.myLen(gv.CurrentRow.Cells(colCSARequest_No).Value) > 0 AndAlso clsCommon.myCdbl(gv.CurrentRow.Cells(colQty).Value) > balqty Then
                            gv.CurrentRow = gv.Rows(gv.CurrentRow.Index)
                            gv.CurrentColumn = gv.Columns(colQty)
                            gv.CurrentRow.Cells(colQty).Value = 0
                            Throw New Exception("Filled quantity can not be more than balance qty i.e " + clsCommon.myCstr(balqty) + " at row no. " + clsCommon.myCstr(gv.CurrentRow.Index + 1) + ".")
                        End If
                    End If

                    If (e.Column Is gv.Columns(colUOM)) OrElse (e.Column Is gv.Columns(colQty)) OrElse (e.Column Is gv.Columns(colTax)) Then
                        isCellValueChanged = True
                        CalDiffrate(CInt(gv.CurrentRow.Index), True)
                        isCellValueChanged = False
                    End If
                    If e.Column Is gv.Columns(coluomPrice) Then
                        isCellValueChanged = True
                        gv.CurrentRow.Cells(colTotalPrice).Value = clsCommon.myCdbl(gv.CurrentRow.Cells(coluomPrice).Value) * clsCommon.myCdbl(gv.CurrentRow.Cells(colQty).Value)
                        isCellValueChanged = False
                    End If

                    If AllowSchemeFlow AndAlso (e.Column Is gv.Columns(colIsSchmItem) OrElse e.Column Is gv.Columns(colSchmCodeType) OrElse e.Column Is gv.Columns(colQty) OrElse e.Column Is gv.Columns(colUOM) OrElse e.Column Is gv.Columns(coluomPrice)) Then
                        isCellValueChanged = True
                        Dim index As Integer = gv.CurrentRow.Index

                        FillFreeItemsInGrid()
                        isValid_CashScheme()
                        gv.Rows(index).Cells(colTotalPrice).Value = Math.Round((clsCommon.myCdbl(gv.Rows(index).Cells(coluomPrice).Value) * clsCommon.myCdbl(gv.Rows(index).Cells(colQty).Value)) - clsCommon.myCdbl(gv.Rows(index).Cells(colCash_Amt).Value), 2)
                        CalDocumentAmt()
                        isCellValueChanged = False
                    End If

                    If AllowSchemeFlow AndAlso e.Column Is gv.Columns(colCash_Pers) Then
                        isCellValueChanged = True
                        gv.CurrentRow.Cells(colCash_Amt).Value = System.Math.Round((clsCommon.myCdbl(gv.CurrentRow.Cells(colQty).Value) * clsCommon.myCdbl(gv.CurrentRow.Cells(coluomPrice).Value) * clsCommon.myCdbl(gv.CurrentRow.Cells(colCash_Pers).Value)) / 100, 2)
                        isValid_CashScheme()
                        gv.CurrentRow.Cells(colTotalPrice).Value = Math.Round((clsCommon.myCdbl(gv.CurrentRow.Cells(coluomPrice).Value) * clsCommon.myCdbl(gv.CurrentRow.Cells(colQty).Value)) - clsCommon.myCdbl(gv.CurrentRow.Cells(colCash_Amt).Value), 2)

                        CalDocumentAmt()
                        isCellValueChanged = False
                    End If
                End If
            End If
        Catch ex As Exception
            isCellValueChanged = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

#Region "variable"
    Private Sub isValid_CashScheme()
        Dim scheme_Code As String = ""
        Dim isSchemeApply As String = ""
        Dim cash_scheme_code As String = ""
        Dim cash_amt As Decimal = 0
        Dim amount As Decimal = 0

        For Each grow As GridViewRowInfo In gv.Rows
            isSchemeApply = clsCommon.myCstr(grow.Cells(colIsSchmItem).Value)
            scheme_Code = clsCommon.myCstr(grow.Cells(colSchmCodeType).Value)
            cash_scheme_code = clsCommon.myCstr(grow.Cells(colCashSchemeCode).Value)
            cash_amt = clsCommon.myCdbl(grow.Cells(colCash_Amt).Value)
            amount = clsCommon.myCdbl(grow.Cells(coluomPrice).Value) * clsCommon.myCdbl(grow.Cells(colQty).Value)

            '================if cash scheme amount exceed total basic amount than scheme not applicable.
            If cash_amt > amount Then
                grow.Cells(colCash_Amt).Value = 0
                grow.Cells(colCash_Pers).Value = 0
                grow.Cells(colCashSchemeCode).Value = Nothing
                grow.Cells(colCashSchemeType).Value = Nothing

                If clsCommon.myLen(scheme_Code) <= 0 Then
                    grow.Cells(colIsSchmItem).Value = Nothing
                    grow.Cells(colSchmCodeType).Value = Nothing
                End If
            End If
        Next
    End Sub

    Private Sub FillFreeItemsInGrid()
        Dim Index As Integer = gv.CurrentRow.Index
        Try
            'If isbtnDOClick Then
            '    Exit Sub
            'End If


            If clsCommon.CompairString(clsCommon.myCstr(gv.Rows(Index).Cells(colIsSchmItem).Value), "N") = CompairStringResult.Equal AndAlso clsCommon.myCBool(gv.Rows(Index).Cells(colFOC).Value) = False Then

                For ii As Integer = gv.Rows.Count - 1 To 0 Step -1
                    If clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(colIsSchmItem).Value), "N") = CompairStringResult.Equal AndAlso clsCommon.myCBool(gv.Rows(ii).Cells(colFOC).Value) = True AndAlso clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(colMainIcode).Value), clsCommon.myCstr(gv.Rows(Index).Cells(colItemCode).Value)) = CompairStringResult.Equal Then
                        gv.Rows.RemoveAt(ii)
                    End If
                Next


                gv.Rows(Index).Cells(colSchmCode).Value = Nothing
                gv.Rows(Index).Cells(colSchmCodeType).Value = Nothing
                gv.Rows(Index).Cells(colCash_Amt).Value = Nothing
                gv.Rows(Index).Cells(colCash_Pers).Value = Nothing
                gv.Rows(Index).Cells(colCashSchemeCode).Value = Nothing
                gv.Rows(Index).Cells(colCashSchemeType).Value = Nothing
                gv.Rows(Index).Cells(colMainIcode).Value = Nothing
                gv.Rows(Index).Cells(colMainIQty).Value = Nothing
                gv.Rows(Index).Cells(colMainIUOM).Value = Nothing
                gv.Rows(Index).Cells(colFOC).Value = False
                gv.Rows(Index).Cells(colIsSchmItem).Value = "N"

                RefreshSerialNo()
            End If

            If clsCommon.CompairString(clsCommon.myCstr(gv.Rows(Index).Cells(colSchmCodeType).Value), "None") <> CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(gv.Rows(Index).Cells(colSchmCodeType).Value), "") <> CompairStringResult.Equal Then
                If clsCommon.CompairString(clsCommon.myCstr(gv.Rows(Index).Cells(colIsSchmItem).Value), "Y") = CompairStringResult.Equal AndAlso clsCommon.myCBool(gv.Rows(Index).Cells(colFOC).Value) = False Then
                    For ii As Integer = gv.Rows.Count - 1 To 0 Step -1
                        If clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(colIsSchmItem).Value), "N") = CompairStringResult.Equal AndAlso clsCommon.myCBool(gv.Rows(ii).Cells(colFOC).Value) = True AndAlso clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(colMainIcode).Value), clsCommon.myCstr(gv.Rows(Index).Cells(colItemCode).Value)) = CompairStringResult.Equal Then
                            gv.Rows.RemoveAt(ii)
                        End If
                    Next

                    '-------------fill cash scheme---------------------------------------
                    Dim obj_Cash As clsSchemeApplyOnDairy = clsSchemeApplyOnDairy.GetPriceSchemeData(clsCommon.myCstr(gv.Rows(Index).Cells(colItemCode).Value), clsCommon.myCstr(gv.Rows(Index).Cells(colUOM).Value), clsCommon.myCdbl(gv.Rows(Index).Cells(colQty).Value), clsCommon.myCstr(txtcustcode.Value), Nothing, clsCommon.myCDate(dtpdate.Text))
                    If obj_Cash IsNot Nothing AndAlso clsCommon.myLen(obj_Cash.Schm_Code) > 0 Then

                        gv.Rows(Index).Cells(colCash_Pers).Value = obj_Cash.Cash_Pers
                        gv.Rows(Index).Cells(colCash_Amt).Value = obj_Cash.Cash_Amt
                        gv.Rows(Index).Cells(colCashSchemeCode).Value = obj_Cash.Schm_Code
                        If clsCommon.myCdbl(obj_Cash.Cash_Pers) > 0 Then
                            gv.Rows(Index).Cells(colCashSchemeType).Value = "P"
                            gv.CurrentRow.Cells(colCash_Amt).Value = System.Math.Round((clsCommon.myCdbl(gv.CurrentRow.Cells(colQty).Value) * clsCommon.myCdbl(gv.CurrentRow.Cells(coluomPrice).Value) * clsCommon.myCdbl(gv.CurrentRow.Cells(colCash_Pers).Value)) / 100, 2)
                        ElseIf clsCommon.myCdbl(obj_Cash.Cash_Amt) > 0 AndAlso clsCommon.myCdbl(obj_Cash.Cash_Pers) <= 0 Then
                            gv.Rows(Index).Cells(colCashSchemeType).Value = "A"
                            gv.Rows(Index).Cells(colCash_Amt).Value = obj_Cash.Cash_Amt
                            If clsCommon.myCdbl(gv.Rows(Index).Cells(colQty).Value) > 0 AndAlso clsCommon.myCdbl(gv.Rows(Index).Cells(coluomPrice).Value) > 0 Then
                                gv.Rows(Index).Cells(colCash_Pers).Value = (clsCommon.myCdbl(obj_Cash.Cash_Amt) * 100) / (clsCommon.myCdbl(gv.Rows(Index).Cells(colQty).Value) * clsCommon.myCdbl(gv.Rows(Index).Cells(coluomPrice).Value))
                            End If
                        End If
                        gv.Rows(Index).Cells(colIsSchmItem).Value = "Y"
                    Else
                        gv.Rows(Index).Cells(colCash_Amt).Value = Nothing
                        gv.Rows(Index).Cells(colCash_Pers).Value = Nothing
                        gv.Rows(Index).Cells(colCashSchemeCode).Value = Nothing
                        gv.Rows(Index).Cells(colCashSchemeType).Value = Nothing
                        gv.Rows(Index).Cells(colIsSchmItem).Value = "N"

                        '==========if cash scheme is there but no quantitive or volumn then also scheme item set to Y
                        If clsCommon.myLen(gv.Rows(Index).Cells(colMainIcode).Value) > 0 Then
                            gv.Rows(Index).Cells(colIsSchmItem).Value = "Y"
                        End If
                    End If
                    '------------------------------------------------------------------

                    Dim objD As clsSchemeApplyOnDairy = clsSchemeApplyOnDairy.GetSchemeData(clsCommon.myCstr(gv.Rows(Index).Cells(colItemCode).Value), clsCommon.myCstr(gv.Rows(Index).Cells(colUOM).Value), clsCommon.myCdbl(gv.Rows(Index).Cells(colQty).Value), txtcustcode.Value, clsCommon.myCstr(gv.Rows(Index).Cells(colSchmCodeType).Value), Nothing, clsCommon.myCDate(dtpdate.Text))
                    If objD IsNot Nothing AndAlso objD.Arr.Count > 0 Then
                        For Each objtr As clsSchemeApplyOnDairy In objD.Arr
                            '--------------update free itemcode in main item row------------------
                            gv.Rows(Index).Cells(colSchmCode).Value = Nothing
                            gv.Rows(Index).Cells(colSchmCodeType).Value = objtr.schm_Type
                            gv.Rows(Index).Cells(colMainIcode).Value = objtr.Schm_Icode
                            gv.Rows(Index).Cells(colMainIQty).Value = objtr.Schm_Qty
                            gv.Rows(Index).Cells(colMainIUOM).Value = objtr.Schm_Item_Uom
                            gv.Rows(Index).Cells(colFOC).Value = False
                            gv.Rows(Index).Cells(colIsSchmItem).Value = "Y"
                            '-------------------------------------------------------------

                            gv.Rows.AddNew()
                            gv.Rows(gv.Rows.Count - 1).Cells(colLineno).Value = clsCommon.myCstr(gv.Rows.Count)
                            gv.Rows(gv.Rows.Count - 1).Cells(colItemCode).Value = objtr.Schm_Icode
                            gv.Rows(gv.Rows.Count - 1).Cells(colitemname).Value = objtr.Schm_Iname
                            gv.Rows(gv.Rows.Count - 1).Cells(colCSAType).Value = objtr.Schm_Item_CSA_Type
                            gv.Rows(gv.Rows.Count - 1).Cells(colUOM).Value = objtr.Schm_Item_Uom
                            'gv.Rows(gv.Rows.Count - 1).Cells(colAltUnitCOde).Value = objtr.Schm_Item_Uom
                            gv.Rows(gv.Rows.Count - 1).Cells(coluomPrice).Value = objtr.Schm_IUnit_Rate
                            gv.Rows(gv.Rows.Count - 1).Cells(colQty).Value = objtr.Schm_Qty
                            'gv.Rows(gv.Rows.Count - 1).Cells(colTotalBasicAmount).Value = 0
                            'gv.Rows(gv.Rows.Count - 1).Cells(colTaxBasis).Value = Nothing

                            'gv.Rows(gv.Rows.Count - 1).Cells(ColActualBalQty).Value = Math.Round(clsItemLocationDetails.getBalance(clsCommon.myCstr(gv.Rows(gv.Rows.Count - 1).Cells(colitemcode).Value), txtFromLocation.Value, txtDocNo.Value, txtDate.Value, Nothing, clsCommon.myCstr(gv.CurrentRow.Cells(coluom).Value), 0), 2)

                            gv.Rows(gv.Rows.Count - 1).Cells(colSchmCode).Value = objtr.Schm_Code
                            gv.Rows(gv.Rows.Count - 1).Cells(colSchmCodeType).Value = objtr.schm_Type
                            gv.Rows(gv.Rows.Count - 1).Cells(colMainIcode).Value = clsCommon.myCstr(gv.Rows(Index).Cells(colItemCode).Value)
                            gv.Rows(gv.Rows.Count - 1).Cells(colMainIQty).Value = clsCommon.myCdbl(gv.Rows(Index).Cells(colQty).Value)
                            gv.Rows(gv.Rows.Count - 1).Cells(colMainIUOM).Value = clsCommon.myCstr(gv.Rows(Index).Cells(colUOM).Value)
                            gv.Rows(gv.Rows.Count - 1).Cells(colFOC).Value = True
                            gv.Rows(gv.Rows.Count - 1).Cells(colIsSchmItem).Value = "N"
                            gv.Rows(gv.Rows.Count - 1).Cells(colIsSchmItem).ReadOnly = True
                            gv.Rows(gv.Rows.Count - 1).Cells(colQty).ReadOnly = True
                            gv.Rows(gv.Rows.Count - 1).Cells(colSchmCodeType).ReadOnly = True
                            gv.Rows(gv.Rows.Count - 1).Cells(coluomPrice).ReadOnly = True

                            ''==========make readonly========================
                            For iCol As Integer = 0 To gv.Columns.Count - 1
                                gv.Rows(gv.Rows.Count - 1).Cells(iCol).ReadOnly = True
                            Next
                            ''===========================================

                            gv.Rows.Move(gv.Rows.Count - 1, Index + 1)
                        Next
                    Else
                        gv.Rows(Index).Cells(colSchmCode).Value = Nothing
                        gv.Rows(Index).Cells(colSchmCodeType).Value = Nothing
                        gv.Rows(Index).Cells(colMainIcode).Value = Nothing
                        gv.Rows(Index).Cells(colMainIQty).Value = Nothing
                        gv.Rows(Index).Cells(colMainIUOM).Value = Nothing
                        gv.Rows(Index).Cells(colFOC).Value = False
                        gv.Rows(Index).Cells(colIsSchmItem).Value = "N"
                        '==========if cash scheme is there but no quantitive or volumn then also scheme item set to Y
                        If clsCommon.myLen(gv.Rows(Index).Cells(colCashSchemeCode).Value) > 0 Then
                            gv.Rows(Index).Cells(colIsSchmItem).Value = "Y"
                        End If
                    End If
                End If
            End If


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        gv.CurrentRow = gv.Rows(Index)
        RefreshSerialNo()
    End Sub

    Sub RefreshSerialNo()
        Dim intSerialNo As Integer
        For intCount As Integer = 0 To gv.Rows.Count - 1
            intSerialNo += 1
            gv.Rows(intCount).Cells(colLineno).Value = clsCommon.myCstr(intSerialNo)
        Next
    End Sub
#End Region

    Private Sub CalDocumentAmt()
        Try
            txttotal_amt.Text = 0
            For Each grow As GridViewRowInfo In gv.Rows
                grow.Cells(colTotalPrice).Value = clsCommon.myCdbl(grow.Cells(coluomPrice).Value) * clsCommon.myCdbl(grow.Cells(colQty).Value)

                txttotal_amt.Text = clsCommon.myCdbl(txttotal_amt.Text) + clsCommon.myCdbl(grow.Cells(colTotalPrice).Value)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub CalDiffrate(ByVal XR As Integer, ByVal CellChanged As Boolean)
        Try
            Dim qry As String = ""
            Dim diffrate As Decimal = 0
            Dim orgrate As Decimal = 0
            Dim ltr_per_case As Decimal = 0
            Dim case_rate As Decimal = 0
            Dim pcs_per_case As Decimal = 0
            Dim uom As String = ""
            Dim cnvrsn As Decimal = 1
            Dim csauom As String = ""

            If ForUDLOnly AndAlso (clsCommon.CompairString(cmbCSAType.SelectedValue, "") = CompairStringResult.Equal OrElse clsCommon.CompairString(cmbCSAType.SelectedValue, "None") = CompairStringResult.Equal) Then
                cmbCSAType.SelectedValue = "CPD-DESI GHEE"
            End If
            If ForUDLOnly AndAlso (clsCommon.CompairString(cmbTax.SelectedValue, "") = CompairStringResult.Equal OrElse clsCommon.CompairString(cmbTax.SelectedValue, "None") = CompairStringResult.Equal) Then
                cmbTax.SelectedValue = "No"
            End If


            Dim CurrntCPDType As String = Nothing
            Dim CSA_State As String = clsCSAPriceMaster.GetCSAState(clsCommon.myCstr(txtcustcode.Value))

            If CellChanged Then
                CurrntCPDType = clsCommon.myCstr(gv.Rows(XR).Cells(colCSAType).Value) 'CPD-DESI GHEE
                uom = clsCommon.myCstr(gv.Rows(XR).Cells(colUOM).Value)
                If ForUDLOnly AndAlso (clsCommon.myLen(gv.Rows(XR).Cells(colTax).Value) <= 0) Then
                    gv.Rows(XR).Cells(colTax).Value = "No"
                End If

                If clsCommon.myCBool(gv.Rows(XR).Cells(colFOC).Value) = True Then
                    gv.Rows(XR).Cells(coluomPrice).Value = 0
                    gv.Rows(XR).Cells(colTotalPrice).Value = 0
                    Exit Sub
                End If
                qry = "select conversion_factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(gv.Rows(XR).Cells(colItemCode).Value) + "' and uom_code='" + uom + "'"
                cnvrsn = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
                If cnvrsn <= 0 Then
                    cnvrsn = 1
                End If

                If Not Apply_PriceChat_On_OtherItems AndAlso clsCommon.CompairString(clsCommon.myCstr(cmbCSAType.SelectedValue), clsCommon.myCstr(gv.Rows(XR).Cells(colCSAType).Value)) <> CompairStringResult.Equal Then
                    Exit Sub
                End If

                qry = "select top 1 TSPL_CSA_PRICE_DETAIL.*,TSPL_CSA_PRICE_HEAD.csa_uom,TSPL_CSA_PRICE_HEAD.CSA_Rate from TSPL_CSA_PRICE_DETAIL left outer join TSPL_CSA_PRICE_HEAD on TSPL_CSA_PRICE_HEAD.doc_no=TSPL_CSA_PRICE_DETAIL.doc_no "
                qry += " left outer join tspl_csa_price_state_detail on tspl_csa_price_state_detail.doc_no=tspl_csa_price_detail.doc_no left outer join TSPL_CSA_PRICE_CSA_DETAIL on TSPL_CSA_PRICE_CSA_DETAIL.doc_no=TSPL_CSA_PRICE_HEAD.doc_no "
                qry += "where TSPL_CSA_PRICE_DETAIL.item_code='" + clsCommon.myCstr(gv.Rows(XR).Cells(colItemCode).Value) + "' and TSPL_CSA_PRICE_HEAD.tax='" + clsCommon.myCstr(gv.Rows(XR).Cells(colTax).Value) + "'"
                qry += " and TSPL_CSA_PRICE_HEAD.doc_no in (select distinct doc_no from TSPL_CSA_LOCATION_DETAIL where location_code='" + txtfrmloc_code.Value + "') "

                If CSAPricePostedData = True Then
                    qry += " and Tspl_CSA_Price_Head.Posted='1' "
                End If

                ''============when setting ON and item is not CPD then other price chat apply
                If Apply_PriceChat_On_OtherItems AndAlso clsCommon.CompairString(CurrntCPDType, "CPD-DESI GHEE") <> CompairStringResult.Equal Then
                    qry += " and TSPL_CSA_PRICE_CSA_DETAIL.cust_code='" + clsCommon.myCstr(txtcustcode.Value) + "'  "
                Else
                    qry += " and tspl_csa_price_state_detail.state_code='" + CSA_State + "' and TSPL_CSA_PRICE_HEAD.csa_type='" + clsCommon.myCstr(gv.Rows(XR).Cells(colCSAType).Value) + "' "
                End If

                If Apply_PriceChat_On_OtherItems Then ''if setting is ON then expiry check in all cases
                    qry += " and convert(date,coalesce(TSPL_CSA_PRICE_HEAD.Expiry_Date,SYSDATETIME()),103)>='" + clsCommon.GetPrintDate(clsCommon.myCDate(dtpdate.Text), "dd/MMM/yyyy") + "' "
                End If
                ''===============end here============================================
                'done by stuti on 08/12/2016
                qry += " order by TSPL_CSA_PRICE_HEAD.doc_date desc "
                '==============end here=========================
                Dim dt As New DataTable()
                dt = clsDBFuncationality.GetDataTable(qry)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    csauom = clsCommon.myCstr(dt.Rows(0)("uom"))
                    diffrate = clsCommon.myCdbl(dt.Rows(0)("Diff_Rate"))
                    Dim csaconversion As Decimal = 0
                    If Apply_PriceChat_On_OtherItems AndAlso clsCommon.CompairString(CurrntCPDType, "CPD-DESI GHEE") <> CompairStringResult.Equal Then
                        csaconversion = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select conversion_factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(gv.Rows(XR).Cells(colItemCode).Value) + "' and uom_code='" + clsCommon.myCstr(dt.Rows(0)("case_uom")) + "'"))
                        orgrate = clsCommon.myCdbl(diffrate)
                    Else
                        csaconversion = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select conversion_factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(gv.Rows(XR).Cells(colItemCode).Value) + "' and uom_code='" + csauom + "'"))

                        If ForUDLOnly Then
                            txtrate.Text = clsCommon.myCdbl(dt.Rows(0)("CSA_Rate"))
                        End If
                        orgrate = (clsCommon.myCdbl((txtrate.Text)) + clsCommon.myCdbl(diffrate)) '+ clsCommon.myCdbl(dt.Rows(0)("CSA_Rate")))
                    End If

                    If csaconversion <= 0 Then
                        csaconversion = 1
                    End If


                    orgrate = System.Math.Round((orgrate * cnvrsn) / csaconversion, 2)

                    gv.Rows(XR).Cells(coluomPrice).Value = orgrate
                Else
                    If clsCommon.myCdbl(gv.Rows(XR).Cells(coluomPrice).Value) <= 0 Then
                        gv.Rows(XR).Cells(coluomPrice).Value = 0
                    End If
                End If

                gv.Rows(XR).Cells(colTotalPrice).Value = clsCommon.myCdbl(gv.Rows(XR).Cells(coluomPrice).Value) * clsCommon.myCdbl(gv.Rows(XR).Cells(colQty).Value)
            Else

                For Each grow As GridViewRowInfo In gv.Rows
                    uom = clsCommon.myCstr(grow.Cells(colUOM).Value)
                    CurrntCPDType = clsCommon.myCstr(grow.Cells(colCSAType).Value) 'CPD-DESI GHEE

                    If ForUDLOnly AndAlso (clsCommon.myLen(grow.Cells(colTax).Value) <= 0) Then
                        grow.Cells(colTax).Value = "No"
                    End If

                    If clsCommon.myCBool(grow.Cells(colFOC).Value) = True Then
                        grow.Cells(coluomPrice).Value = 0
                        grow.Cells(colTotalPrice).Value = 0
                        Continue For
                    End If

                    qry = "select conversion_factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(grow.Cells(colItemCode).Value) + "' and uom_code='" + uom + "'"
                    cnvrsn = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
                    If cnvrsn <= 0 Then
                        cnvrsn = 1
                    End If

                    If clsCommon.myLen(uom) <= 0 Then
                        Continue For
                    End If
                    If Not Apply_PriceChat_On_OtherItems AndAlso clsCommon.CompairString(clsCommon.myCstr(cmbCSAType.SelectedValue), clsCommon.myCstr(grow.Cells(colCSAType).Value)) <> CompairStringResult.Equal Then
                        Continue For
                    End If

                    qry = "select top 1 TSPL_CSA_PRICE_DETAIL.*,TSPL_CSA_PRICE_HEAD.csa_uom,TSPL_CSA_PRICE_HEAD.CSA_Rate  from TSPL_CSA_PRICE_DETAIL left outer join TSPL_CSA_PRICE_HEAD on TSPL_CSA_PRICE_HEAD.doc_no=TSPL_CSA_PRICE_DETAIL.doc_no "
                    qry += " left outer join tspl_csa_price_state_detail on tspl_csa_price_state_detail.doc_no=tspl_csa_price_detail.doc_no left outer join TSPL_CSA_PRICE_CSA_DETAIL on TSPL_CSA_PRICE_CSA_DETAIL.doc_no=TSPL_CSA_PRICE_HEAD.doc_no "
                    qry += "where TSPL_CSA_PRICE_DETAIL.item_code='" + clsCommon.myCstr(grow.Cells(colItemCode).Value) + "' and TSPL_CSA_PRICE_HEAD.tax='" + clsCommon.myCstr(grow.Cells(colTax).Value) + "'"
                    qry += " and TSPL_CSA_PRICE_HEAD.doc_no in (select distinct doc_no from TSPL_CSA_LOCATION_DETAIL where location_code='" + txtfrmloc_code.Value + "')"

                    If CSAPricePostedData = True Then
                        qry += " and Tspl_CSA_Price_Head.Posted='1' "
                    End If

                    ''============when setting ON and item is not CPD then other price chat apply
                    If Apply_PriceChat_On_OtherItems AndAlso clsCommon.CompairString(CurrntCPDType, "CPD-DESI GHEE") <> CompairStringResult.Equal Then
                        qry += " and TSPL_CSA_PRICE_CSA_DETAIL.cust_code='" + clsCommon.myCstr(txtcustcode.Value) + "' "
                    Else
                        qry += " and tspl_csa_price_state_detail.state_code='" + CSA_State + "' and TSPL_CSA_PRICE_HEAD.csa_type='" + clsCommon.myCstr(gv.Rows(XR).Cells(colCSAType).Value) + "' "
                    End If

                    If Apply_PriceChat_On_OtherItems Then ''if setting is ON then expiry check in all cases
                        qry += " and convert(date,coalesce(TSPL_CSA_PRICE_HEAD.Expiry_Date,SYSDATETIME()),103)>='" + clsCommon.GetPrintDate(clsCommon.myCDate(dtpdate.Text), "dd/MMM/yyyy") + "' "
                    End If
                    ''===============end here============================================
                    'done by stuti on 08/12/2016
                    qry += " order by TSPL_CSA_PRICE_HEAD.doc_date desc "
                    '==============end here=========================
                    Dim dt As New DataTable()
                    dt = clsDBFuncationality.GetDataTable(qry)

                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        csauom = clsCommon.myCstr(dt.Rows(0)("uom"))
                        diffrate = clsCommon.myCdbl(dt.Rows(0)("Diff_Rate"))
                        Dim csaconversion As Decimal = 0
                        If Apply_PriceChat_On_OtherItems AndAlso clsCommon.CompairString(CurrntCPDType, "CPD-DESI GHEE") <> CompairStringResult.Equal Then
                            csaconversion = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select conversion_factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(grow.Cells(colItemCode).Value) + "' and uom_code='" + clsCommon.myCstr(dt.Rows(0)("case_uom")) + "'"))
                            orgrate = clsCommon.myCdbl(diffrate)
                        Else
                            csaconversion = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select conversion_factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(grow.Cells(colItemCode).Value) + "' and uom_code='" + csauom + "'"))

                            If ForUDLOnly Then
                                txtrate.Text = clsCommon.myCdbl(dt.Rows(0)("CSA_Rate"))
                            End If
                            orgrate = (clsCommon.myCdbl(txtrate.Text) + clsCommon.myCdbl(diffrate)) '+ clsCommon.myCdbl(dt.Rows(0)("CSA_Rate")))
                        End If

                        If csaconversion <= 0 Then
                            csaconversion = 1
                        End If

                        orgrate = System.Math.Round((orgrate * cnvrsn) / csaconversion, 2)

                        grow.Cells(coluomPrice).Value = orgrate
                    Else
                        If clsCommon.myCdbl(grow.Cells(coluomPrice).Value) <= 0 Then
                            grow.Cells(coluomPrice).Value = 0
                        End If
                    End If
                    grow.Cells(colTotalPrice).Value = clsCommon.myCdbl(grow.Cells(coluomPrice).Value) * clsCommon.myCdbl(grow.Cells(colQty).Value)
                Next
            End If

            CalDocumentAmt()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub OpenIcode(ByVal isButtonClicked As Boolean)
        Dim icode As String = ""
        Dim csatype As String = ""

        icode = clsCommon.myCstr(clsItemMaster.getFinder(" tspl_item_master.Active=1 and tspl_item_master.item_type in ('F','T') and isnull(Is_FreshItem,0)<>1 ", clsCommon.myCstr(gv.CurrentRow.Cells(colItemCode).Value), isButtonClicked))

        If clsCommon.myLen(icode) > 0 Then
            csatype = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select csa_type from tspl_item_master where item_code='" + icode + "'"))
            If Not ForUDLOnly AndAlso clsCommon.myCdbl(txtrate.Text) <= 0 AndAlso clsCommon.CompairString(csatype, "CPD-DESI GHEE") = CompairStringResult.Equal Then
                txtrate.Focus()
                txtrate.Select()
                clsCommon.MyMessageBoxShow(Me, "Fill Rate For RT", Me.Text)
                ClearCurrentRow()
                Exit Sub
            End If

            gv.CurrentRow.Cells(colItemCode).Value = icode
            gv.CurrentRow.Cells(colitemname).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_desc from tspl_item_master where item_code='" + icode + "'"))
            gv.CurrentRow.Cells(colIHSN).Value = clsItemMaster.GetItemHSNCode(icode, Nothing)
            gv.CurrentRow.Cells(colUOM).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select unit_code from tspl_item_master where item_code='" + icode + "'"))
            gv.CurrentRow.Cells(colTax).Value = clsCommon.myCstr(cmbTax.SelectedValue)
            gv.CurrentRow.Cells(colCSAType).Value = csatype
        Else
            ClearCurrentRow()
        End If
    End Sub

    Private Sub ClearCurrentRow()
        gv.CurrentRow.Cells(colItemCode).Value = ""
        gv.CurrentRow.Cells(colitemname).Value = ""
        gv.CurrentRow.Cells(colUOM).Value = ""
        gv.CurrentRow.Cells(colQty).Value = Nothing
        gv.CurrentRow.Cells(coluomPrice).Value = Nothing
        gv.CurrentRow.Cells(colTax).Value = ""
        gv.CurrentRow.Cells(colTotalPrice).Value = Nothing
        gv.CurrentRow.Cells(colRemarks).Value = Nothing
        gv.CurrentRow.Cells(colCSAType).Value = ""
    End Sub
    Private Sub OpenUOM(ByVal isButtonClicked As Boolean)
        Dim icode As String = ""
        Dim qry As String = "select TSPL_ITEM_UOM_DETAIL.uom_code as Code,TSPL_ITEM_UOM_DETAIL.uom_description as Unit from TSPL_ITEM_UOM_DETAIL "
        icode = clsCommon.myCstr(clsCommon.ShowSelectForm("UOMFND", qry, "Code", " item_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colItemCode).Value) + "'", clsCommon.myCstr(gv.CurrentRow.Cells(colUOM).Value), "Code", isButtonClicked))
        gv.CurrentRow.Cells(colUOM).Value = icode
    End Sub

    Private Sub gv_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv.CurrentColumnChanged
        If gv.RowCount > 0 Then
            Dim intCurrRow As Integer = gv.CurrentRow.Index
            gv.CurrentRow.Cells(colLineno).Value = clsCommon.myCstr(clsCommon.myCdbl(intCurrRow + 1))
            If intCurrRow = gv.Rows.Count - 1 Then
                gv.Rows.AddNew()
                gv.CurrentRow = gv.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gv_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv.UserDeletingRow
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If common.clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                e.Cancel = True
            Else
                If Not btnpost.Enabled AndAlso Not btnsave.Enabled AndAlso Not btndelete.Enabled Then
                    e.Cancel = True
                    Throw New Exception("No row deleted because data is posted")
                End If
                e.Cancel = False


                If AllowSchemeFlow Then
                    For ii As Integer = gv.Rows.Count - 1 To 0 Step -1
                        If clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(colIsSchmItem).Value), "N") = CompairStringResult.Equal AndAlso clsCommon.myCBool(gv.Rows(ii).Cells(colFOC).Value) = True AndAlso clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(colMainIcode).Value), clsCommon.myCstr(gv.CurrentRow.Cells(colItemCode).Value)) = CompairStringResult.Equal Then
                            gv.Rows.RemoveAt(ii)
                        End If
                    Next
                Else
                    Dim qry As String = "delete from TSPL_CSA_DO_DETAIL where doc_no='" + txtCode.Value + "' and item_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colItemCode).Value) + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                End If
            End If
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
#End Region

    Private Sub txtrate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtrate.TextChanged
        Try
            If isInsideLoadData OrElse ForUDLOnly Then
                Return
            End If


            CalDiffrate(0, False)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnsavelayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsavelayout.Click
        If clsCommon.myLen(ReportID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv.Columns.Count - 1 Step ii + 1
                        gv.Columns(ii).IsVisible = False
                        gv.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub btndeletelayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndeletelayout.Click
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Delete layout successfully", "Information", Me.Text)
    End Sub

    '' ====updated by Preeti Gupta Ticket No[]
    Public Function funPrint(ByVal StrCode As String) As DataTable
        Dim dt As DataTable = Nothing
        Try
            Dim qry As String = "select "
            qry += "  case when coalesce(p_cust.P_cust_code,'')='' then Cust_State  .GST_STATE_Code    when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_GST_STATE_Code  end as P_Cust_GST_State_code,Cust_State  .GST_STATE_Code as Cust_GST_State_code "
            qry += " ,case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .GSTNO    when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Cust_GST_IN   end as P_Cust_GST_IN_no"
            qry += " ,TSPL_CUSTOMER_MASTER.GSTNO as Cust_GST_In_No"
            qry += " ,TSPL_LOCATION_MASTER.GSTNO as Comp_GSTNO,Loc_State_Master.GST_STATE_Code as Comp__GST_State_code"
            qry += " ,isnull(TSPL_ITEM_MASTER.HSN_Code,'') as HSN_Code,"
            qry += " case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Add1  when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_add1 end as P_Add1,"
            qry += " case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Add2  when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_add2 end as P_Add2,"
            qry += " case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Add3  when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_add3 end as P_Add3,"
            qry += " case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .PIN_Code   when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Pin_No  end as P_PinNo,"
            qry += " case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .CST    when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_CST_No   end as P_CstNo,case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Tin_No     when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .p_Tin_No   end as P_TinNo,"

            qry += " case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Email    when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Email  end as P_Email,case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Fax     when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Fax   end as P_Fax,"
            qry += " case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .CST      when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_CST_No    end as P_CSTNo,case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Lst_No     when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_LST_No    end as P_LstNo,"
            qry += " case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Cust_Code      when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_code   end as P_CustCode,"
            qry += " case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CUSTOMER_MASTER  .Customer_Name       when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_cust_name    end as P_Cust_Name,case when coalesce(p_cust.P_cust_code,'')='' then TSPL_CITY_MASTER   .City_Name       when coalesce(p_cust.P_city_name,'')<>'' then p_cust .P_City_Name    end as P_City_Name,"
            qry += " case when coalesce(p_cust.P_cust_code,'')='' then    "

            qry += " case when ISNULL(TSPL_CUSTOMER_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_CUSTOMER_MASTER.Phone1 end +  Case When   ISNULL(TSPL_CUSTOMER_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_CUSTOMER_MASTER.Phone2 Else'' End "


            qry += "  when coalesce(p_cust.P_cust_code,'')<>'' then p_cust .P_Phn    end as P_Cust_Phn,TSPL_CUSTOMER_MASTER.Cust_Code ,"

            qry += "  TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_CUSTOMER_MASTER.Add1 as Cust_Add1,TSPL_CUSTOMER_MASTER.Add2 as Cust_add2,TSPL_CUSTOMER_MASTER.Add3 as cust_add3,"

            qry += "  case when ISNULL(TSPL_CUSTOMER_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_CUSTOMER_MASTER.Phone1 end +  Case When   ISNULL(TSPL_CUSTOMER_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_CUSTOMER_MASTER.Phone2 Else'' End "
            qry += " as Cust_Phn,TSPL_CUSTOMER_MASTER.Tin_No  as Cust_TinNo ,TSPL_CUSTOMER_MASTER.CST as Cust_CSTNo,TSPL_CUSTOMER_MASTER.Lst_No as Cust_LSTNo,TSPL_CUSTOMER_MASTER.Email as Cust_Email ,TSPL_CUSTOMER_MASTER.PIN_Code as Cust_PinCode,TSPL_CITY_MASTER.City_Name as Cust_City_Name,TSPL_CUSTOMER_MASTER.Fax as Cust_Fax,"

            qry += " TSPL_COMPANY_MASTER.Comp_Name as comp_Name ,TSPL_COMPANY_MASTER.Add1 as comp_Add1 ,TSPL_COMPANY_MASTER.Add2 as comp_Add2 ,TSPL_COMPANY_MASTER.Add3  as comp_add3,TSPL_COMPANY_MASTER.Fax  as comp_fax ,TSPL_COMPANY_MASTER.Email as comp_email ,TSPL_COMPANY_MASTER.Pincode as copm_pincode ,TSPL_COMPANY_MASTER.State as comp_state ,TSPL_COMPANY_MASTER.Tin_No  as copm_TinNo, ( case when TSPL_COMPANY_MASTER.Phone2  <> '' then TSPL_COMPANY_MASTER.Phone1 +','+TSPL_COMPANY_MASTER.Phone2 else TSPL_COMPANY_MASTER.Phone1 end) as Comp_Phn ,TSPL_CSA_DO_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_CSA_DO_DETAIL.UOM ,TSPL_CSA_DO_DETAIL.Qty ,TSPL_CSA_DO_DETAIL.Unit_Rate ,TSPL_CSA_DO_DETAIL.Total_Amt ,TSPL_CSA_DO_HEAD.Document_Amount ,TSPL_CSA_DO_HEAD.Cust_Code,convert(varchar,TSPL_CSA_DO_HEAD.Doc_Date,103) as Doc_Date ,TSPL_CSA_DO_HEAD.Doc_No as Doc_Code ,"
            qry += " TSPL_LOCATION_MASTER.Location_Desc  as From_Loc_Desc"
            qry += " ,TSPL_LOCATION_MASTER.Add1 as From_Location_Add1 ,TSPL_LOCATION_MASTER.Add2 as From_Location_Add2 ,TSPL_LOCATION_MASTER.Add3 as From_Location_Add3 ,TSPL_LOCATION_MASTER.Add4 as From_Location_Add4,case when ISNULL(TSPL_LOCATION_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_LOCATION_MASTER.Phone1 end +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_LOCATION_MASTER.Phone2 Else'' End "
            qry += "  as From_Location_Phn,TSPL_LOCATION_MASTER .Pin_Code  as From_Loc_Pin_Code,TSPL_LOCATION_MASTER.Email as From_Loc_Email,TSPL_LOCATION_MASTER_1.Location_Desc  as To_Location_desc,TSPL_LOCATION_MASTER_1.Add1  as To_Loc_Add1,TSPL_LOCATION_MASTER_1.Add2 as To_Loc_Add2,TSPL_LOCATION_MASTER_1.Add3  as To_Loc_Add3,TSPL_LOCATION_MASTER_1.Add4 as To_Loc_Add4,case when ISNULL(TSPL_LOCATION_MASTER_1.Phone1,'')='(+__)__________' then '' else TSPL_LOCATION_MASTER_1.Phone1 end +  Case When   ISNULL(TSPL_LOCATION_MASTER_1.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_LOCATION_MASTER_1.Phone2 Else'' End "
            qry += "  as To_Loc_Phn,TSPL_LOCATION_MASTER_1.Pin_Code  as To_Loc_Pin_Code,TSPL_LOCATION_MASTER_1.Email as To_Loc_Email"

            qry += "  from TSPL_CSA_DO_DETAIL"
            qry += " left outer join  TSPL_CSA_DO_HEAD  on TSPL_CSA_DO_HEAD.Doc_No = TSPL_CSA_DO_DETAIL.Doc_No "
            qry += " left outer join TSPL_COMPANY_MASTER  on TSPL_COMPANY_MASTER .Comp_Code =TSPL_CSA_DO_HEAD.Comp_Code  "
            qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =TSPL_CSA_DO_DETAIL.Item_Code "
            qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_CSA_DO_HEAD.From_Location_Code "
            qry += " left outer join TSPL_LOCATION_MASTER AS TSPL_LOCATION_MASTER_1 on TSPL_LOCATION_MASTER_1.Location_Code =  TSPL_CSA_DO_HEAD.To_Location_Code"
            qry += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_CSA_DO_HEAD.Cust_Code  "
            qry += " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code =TSPL_CUSTOMER_MASTER.City_Code"
            qry += " left join TSPL_STATE_MASTER as Loc_State_Master on Loc_State_Master.STATE_CODE =TSPL_LOCATION_MASTER.State"
            qry += " left outer join TSPL_STATE_MASTER as Cust_State on Cust_State.STATE_CODE  =TSPL_CUSTOMER_MASTER.State"
            qry += "  LEFT join (select "
            qry += "  P_Cust_State.GST_STATE_Code as P_GST_STATE_Code,TSPL_CUSTOMER_MASTER.GSTNO as P_Cust_GST_IN,"
            qry += " Cust_Code as P_cust_code,Customer_Name as P_cust_name,Add1 as P_cust_add1 ,Add2  as P_cust_add2,add3  as P_cust_add3,PIN_Code as P_Pin_No,Tin_No as p_Tin_No ,State as P_state,Email as P_Email,fax as P_Fax,TSPL_CITY_MASTER.City_Name as  P_City_Name, case when ISNULL(Phone1,'')='(+__)__________' then '' else Phone1 end +  Case When   ISNULL(Phone2,'')<>'(+__)__________' Then ', '+ Phone2 Else'' End as  P_Phn,CST as P_CST_No,Terms_Code as P_Terms,Lst_No as P_LST_No  from TSPL_CUSTOMER_MASTER  "
            qry += " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code =TSPL_CUSTOMER_MASTER.City_Code "
            qry += " left join TSPL_STATE_MASTER as P_Cust_State on P_Cust_State.STATE_CODE =TSPL_CUSTOMER_MASTER.State"
            qry += " ) p_cust on p_cust.P_cust_code=TSPL_CUSTOMER_MASTER.Parent_Customer_No and TSPL_CUSTOMER_MASTER.Parent_Customer_YN='N'   "
            qry += " where 1=1 and TSPL_CSA_DO_HEAD.Doc_No='" + StrCode + "'"

            dt = clsDBFuncationality.GetDataTable(qry)


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return dt
    End Function
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        'clsCommon.ProgressBarShow()
        Dim dt As New DataTable()
        dt = funPrint(txtCode.Value)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "rptCSADeliverySale", "CSA Delivery Sale", clsCommon.myCstr(dt.Rows(0)("Doc_Date")))
            frmCRV = Nothing
        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found.", Me.Text)
        End If
        'clsCommon.ProgressBarHide()
    End Sub

    Private Sub gv_CurrentRowChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentRowChangedEventArgs) Handles gv.CurrentRowChanged
        If gv.CurrentRow IsNot Nothing AndAlso Not e.CurrentRow.Index < 0 Then
            setBalance()
        End If
    End Sub

#Region "Mail Event"
    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        Dim frm As New FrmMailSMSSettingNew2()
        frm.FormId = clsUserMgtCode.frmCSADeliveryOrder
        frm.ShowDialog()
    End Sub

    Public Sub SetMailRight()
        If objCommonVar.IsMailSend Then
            btnsetting.Enabled = True
        Else
            btnsetting.Enabled = False
        End If
    End Sub

    Private Sub btnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select Document No. First", Me.Text)
                txtCode.Focus()
                txtCode.Select()
                Return
            End If

            If Not (common.clsCommon.MyMessageBoxShow("Send E-Mail/SMS Of Respective Delivery Order No. " + txtCode.Value + "" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes) Then
                Return
            End If
            'LoadData(txtCode.Value, NavigatorType.Current)
            Dim lstUsers As New List(Of String)
            lstUsers.Add(txtcustcode.Value)
            'SendSMSandEmail(lstUsers, False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    'Private Sub SendSMSandEmail(ByVal lstUsers As List(Of String), ByVal isSendForApproval As Boolean)
    '    Try
    '        Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(clsUserMgtCode.frmCSADeliveryOrder)

    '        If obj Is Nothing Then
    '            clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
    '            Return
    '        End If
    '        If clsCommon.myLen(obj.mailsubjct) <= 0 Then
    '            clsCommon.MyMessageBoxShow("First do email setting", Me.Text)
    '            Return
    '        End If

    '        Dim strContactPerson As String = ""
    '        Dim strSubject As String = obj.mailsubjct.Replace(clsEmailSMSConstants.DOC_NO, txtCode.Value)
    '        strSubject = strSubject.Replace(clsEmailSMSConstants.DOC_Date, clsCommon.GetPrintDate(dtpdate.Text, "dd/MMM/yyyy"))

    '        Dim strbody As String = obj.mailbody.Replace(clsEmailSMSConstants.DOC_NO, txtCode.Value)
    '        strbody = strbody.Replace(clsEmailSMSConstants.DOC_Date, clsCommon.GetPrintDate(dtpdate.Text, "dd/MMM/yyyy"))
    '        strbody = strbody.Replace(clsEmailSMSConstants.Cust_Name, txtcustName.Text)
    '        strbody = strbody.Replace(clsEmailSMSConstants.From_Location, txtfrmloc_code.Value)
    '        strbody = strbody.Replace(clsEmailSMSConstants.RT_Detail, ("RT Rate: " + clsCommon.myCstr(txtrate.Text) + " And UOM: " + txtRt_UOM.Text))
    '        strbody = strbody.Replace(clsEmailSMSConstants.CSA_Item_Type, cmbCSAType.SelectedValue)
    '        strbody = strbody.Replace(clsEmailSMSConstants.Doc_Amount, txttotal_amt.Text)

    '        '------------------------code for attchament-------------------------------------
    '        Dim strRptPath As String = ""
    '        If obj.atchmnt = "Y" Then
    '            Dim dt As New DataTable()
    '            dt = funPrint(txtCode.Value)
    '            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '                Dim frmCRV As New frmCrystalReportViewer()
    '                strRptPath = frmCRV.EmailAttachment(CrystalReportFolder.KwalitySalesReport, dt, "rptCSADeliverySale", "CSA Delivery Sale")
    '                frmCRV = Nothing
    '            End If
    '        End If
    '        '---------------------------------------------------------------------------


    '        For Each strUser As String In lstUsers

    '            Dim lstReceiptents As New List(Of String)
    '            Dim qry As String = ""
    '            Dim emailId As String = ""
    '            If isSendForApproval Then
    '                strContactPerson = strUser
    '                qry = "select E_Mail from TSPL_USER_MASTER where User_Code in ('" + strUser + "') "
    '                emailId = clsDBFuncationality.getSingleValue(qry)
    '            Else
    '                strContactPerson = clsDBFuncationality.getSingleValue("select upper(substring(Contact_Person_Name,1,1)) + lower(substring(Contact_Person_Name,2,49)) from TSPL_customer_MASTER where cust_code ='" & strUser & "' ")
    '                emailId = clsDBFuncationality.getSingleValue("select Email from TSPL_customer_MASTER where cust_code ='" & strUser & "' ")
    '            End If

    '            lstReceiptents.Add(emailId)

    '            Dim body As String = strbody.Replace(clsEmailSMSConstants.UserCode, strUser)

    '            clsMailViaOutlook.SendEmail(strSubject, body, lstReceiptents, Nothing, strRptPath)
    '        Next

    '        clsCommon.MyMessageBoxShow("E-Mail Send Successfully", Me.Text)



    '        If Not clsSMSAtPost_Sales.SMSATPOST_SALE() Then
    '            SMSSENDONLY(False)
    '        End If
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Sub

    'Private Sub SMSSENDONLY(ByVal isPost As Boolean)
    '    Try
    '        Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(clsUserMgtCode.frmCSASaleInvoice)

    '        If obj Is Nothing Then
    '            clsCommon.MyMessageBoxShow("First do sms setting", Me.Text)
    '            Return
    '        End If


    '        If clsCommon.myLen(obj.smsbody) <= 0 Then
    '            Return
    '        End If

    '        Dim strMes As String = obj.smsbody
    '        strMes = strMes.Replace(clsEmailSMSConstants.DOC_NO, txtCode.Value)
    '        strMes = strMes.Replace(clsEmailSMSConstants.DOC_Date, clsCommon.GetPrintDate(dtpdate.Text, "dd/MMM/yyyy"))
    '        strMes = strMes.Replace(clsEmailSMSConstants.Cust_Name, txtcustName.Text)
    '        strMes = strMes.Replace(clsEmailSMSConstants.From_Location, txtfrmloc_code.Value)
    '        strMes = strMes.Replace(clsEmailSMSConstants.RT_Detail, ("RT Rate: " + clsCommon.myCstr(txtrate.Text) + " And UOM: " + txtRt_UOM.Text))
    '        strMes = strMes.Replace(clsEmailSMSConstants.CSA_Item_Type, cmbCSAType.SelectedValue)
    '        strMes = strMes.Replace(clsEmailSMSConstants.Doc_Amount, txttotal_amt.Text)

    '        Dim strphone As String = clsDBFuncationality.getSingleValue("select Phone1 from tspl_customer_master where cust_code ='" & txtcustcode.Value & "' ")

    '        If clsSMSSend.SendSMS(clsUserMgtCode.frmCSADeliveryOrder, strMes, strphone) Then
    '            If Not isPost Then
    '                clsCommon.MyMessageBoxShow("SMS Send Successfully", Me.Text)
    '            End If
    '        End If

    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Sub

    Private Sub btnSend_Approval_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend_Approval.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select Document No. First", Me.Text)
                txtCode.Focus()
                txtCode.Select()
                Return
            End If

            If Not (common.clsCommon.MyMessageBoxShow(Me, "Send for Approval Of Respective Delivery Order No. " + txtCode.Value + "" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes) Then
                Return
            End If
            Dim qry As String = "Select * from TSPL_APPROVAL_LEVEL_SCREEN where 1=1 and Trans_Code='" + MyBase.Form_ID + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            Dim lstUsers As New List(Of String)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    lstUsers.Add(dr("User_Code").ToString())
                Next
            End If

            If lstUsers.Count = 0 Then
                Throw New Exception("No Receiptent Found")
            End If
            'SendSMSandEmail(lstUsers, True)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
#End Region

    Private Sub fndRequestNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndRequestNo._MYValidating
        Dim obj As New clsCSABooking()
        Try

            If strFromDrillDown Then
                Exit Sub
            End If

            Dim Auto_Scheme_Check As Integer = 0
            Auto_Scheme_Check = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AutoSchemeOn, clsFixedParameterCode.AutoSchemeOn, Nothing))

            isInsideLoadData = True
            Dim frm As New frmCSAPendingDO()
            frm.VendorCode = clsCommon.myCstr(txtcustcode.Value)
            frm.VendorName = clsCommon.myCstr(txtcustName.Text)
            frm.strCurrCode = clsCommon.myCstr(txtCode.Value)
            frm.strCurrDate = clsCommon.myCDate(dtpdate.Text)
            frm.MainFormID = MyBase.Form_ID
            frm.ShowDialog()

            If frm.ArrCSARequest IsNot Nothing AndAlso frm.ArrCSARequest.Count > 0 Then
                obj = clsCSABooking.GetData(clsCommon.myCstr(frm.ArrCSARequest(0).BOOKING_NO), arrLoc, NavigatorType.Current, "Request")
                If obj IsNot Nothing AndAlso clsCommon.myLen(obj.BOOKING_NO) > 0 Then
                    txtCode.Value = Nothing

                    fndRequestNo.Value = obj.BOOKING_NO
                    txtcustcode.Value = obj.CSA_CODE
                    txtcustName.Text = clsCustomerMaster.GetName(obj.CSA_CODE, Nothing)
                    txtfrmloc_code.Value = obj.Location_Code
                    txtfrmloc_name.Text = clsLocation.GetName(obj.Location_Code, Nothing)
                    txttoloc_code.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select location_code from tspl_location_master where cust_code='" + txtcustcode.Value + "' and isnull(tspl_location_master.csa_type,'N')='Y'"))
                    txttoloc_name.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select location_desc from tspl_location_master where location_code='" + txttoloc_code.Value + "'"))
                    txtstate_code.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select state from tspl_location_master where location_code='" + txttoloc_code.Value + "'"))
                    txtstate_name.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select state_name from tspl_state_master where state_code='" + txtstate_code.Value + "'"))
                    txtRt_UOM.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select unit_code from tspl_unit_master where isnull(RT_Rate,'N')='Y'"))
                    cmbTax.SelectedValue = "No"
                    txttotal_amt.Text = Nothing
                    cmbCSAType.SelectedValue = "CPD-DESI GHEE"

                    gv.Rows.Clear()
                    gv.Rows.AddNew()

                    Dim LineNo As Integer = 1
                    Dim qry1 As String = ""
                    For Each objtr As clsCSABookingDetail In frm.ArrCSARequest
                        gv.Rows(gv.Rows.Count - 1).Cells(colLineno).Value = LineNo
                        gv.Rows(gv.Rows.Count - 1).Cells(colItemCode).Value = objtr.Itemcode
                        qry1 = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select HSN_Code from tspl_item_master where item_code='" & objtr.Itemcode & "' "))
                        If clsCommon.myLen(qry1) > 0 Then
                            objtr.HSNCode = qry1
                        End If
                        gv.Rows(gv.Rows.Count - 1).Cells(colIHSN).Value = objtr.HSNCode
                     
                        gv.Rows(gv.Rows.Count - 1).Cells(colitemname).Value = objtr.Itemname
                        gv.Rows(gv.Rows.Count - 1).Cells(colUOM).Value = objtr.BOOK_QTY_UOM
                        gv.Rows(gv.Rows.Count - 1).Cells(colCSAType).Value = objtr.CSA_ITEM_TYPE
                        gv.Rows(gv.Rows.Count - 1).Cells(colQty).Value = objtr.Bal_Qty
                        gv.Rows(gv.Rows.Count - 1).Cells(coluomPrice).Value = objtr.BOOK_Rate
                        CalDiffrate(gv.Rows.Count - 1, True)
                        gv.Rows(gv.Rows.Count - 1).Cells(colTax).Value = objtr.TAX_PAID
                        gv.Rows(gv.Rows.Count - 1).Cells(colTotalPrice).Value = clsCommon.myCdbl(objtr.BOOK_QTY) * clsCommon.myCdbl(objtr.BOOK_Rate)
                        gv.Rows(gv.Rows.Count - 1).Cells(colRemarks).Value = Nothing

                        gv.Rows(gv.Rows.Count - 1).Cells(colCSARequest_No).Value = objtr.BOOKING_NO
                        gv.Rows(gv.Rows.Count - 1).Cells(colBal_Qty).Value = objtr.Bal_Qty

                        gv.Rows(gv.Rows.Count - 1).Cells(colItemCode).ReadOnly = True
                        gv.Rows(gv.Rows.Count - 1).Cells(colUOM).ReadOnly = True
                        gv.Rows(gv.Rows.Count - 1).Cells(colTotalPrice).Value = clsCommon.myCdbl(gv.Rows(gv.Rows.Count - 1).Cells(colQty).Value) * clsCommon.myCdbl(gv.Rows(gv.Rows.Count - 1).Cells(coluomPrice).Value)

                        If AllowSchemeFlow Then
                            '--------------update free itemcode in main item row------------------
                            gv.Rows(gv.Rows.Count - 1).Cells(colSchmCode).Value = Nothing
                            gv.Rows(gv.Rows.Count - 1).Cells(colSchmCodeType).Value = Nothing
                            gv.Rows(gv.Rows.Count - 1).Cells(colMainIcode).Value = Nothing
                            gv.Rows(gv.Rows.Count - 1).Cells(colMainIQty).Value = Nothing
                            gv.Rows(gv.Rows.Count - 1).Cells(colMainIUOM).Value = Nothing
                            gv.Rows(gv.Rows.Count - 1).Cells(colFOC).Value = False
                            gv.Rows(gv.Rows.Count - 1).Cells(colIsSchmItem).Value = "N"
                            '-------------------------------------------------------------

                            If Auto_Scheme_Check >= 1 Then
                                '=CASH SCHEME=====================
                                Dim obj_Cash As clsSchemeApplyOnDairy = clsSchemeApplyOnDairy.GetPriceSchemeData(objtr.Itemcode, objtr.BOOK_QTY_UOM, objtr.Bal_Qty, obj.CSA_CODE, Nothing, clsCommon.myCDate(dtpdate.Text))

                                If obj_Cash IsNot Nothing AndAlso clsCommon.myLen(obj_Cash.Schm_Code) > 0 Then
                                    gv.Rows(gv.Rows.Count - 1).Cells(colCash_Pers).Value = obj_Cash.Cash_Pers
                                    gv.Rows(gv.Rows.Count - 1).Cells(colCash_Amt).Value = obj_Cash.Cash_Amt
                                    gv.Rows(gv.Rows.Count - 1).Cells(colCashSchemeCode).Value = obj_Cash.Schm_Code
                                    If obj_Cash.Cash_Pers > 0 Then
                                        gv.Rows(gv.Rows.Count - 1).Cells(colCashSchemeType).Value = "P"
                                        gv.Rows(gv.Rows.Count - 1).Cells(colCash_Amt).Value = System.Math.Round((clsCommon.myCdbl(gv.Rows(gv.Rows.Count - 1).Cells(colQty).Value) * clsCommon.myCdbl(gv.Rows(gv.Rows.Count - 1).Cells(coluomPrice).Value) * clsCommon.myCdbl(gv.Rows(gv.Rows.Count - 1).Cells(colCash_Pers).Value)) / 100, 2)
                                    ElseIf obj_Cash.Cash_Amt > 0 AndAlso obj_Cash.Cash_Pers <= 0 Then
                                        gv.Rows(gv.Rows.Count - 1).Cells(colCashSchemeType).Value = "A"
                                        gv.Rows(gv.Rows.Count - 1).Cells(colCash_Pers).Value = (clsCommon.myCdbl(obj_Cash.Cash_Amt) * 100) / (clsCommon.myCdbl(gv.Rows(gv.Rows.Count - 1).Cells(colQty).Value) * clsCommon.myCdbl(gv.Rows(gv.Rows.Count - 1).Cells(coluomPrice).Value))
                                        gv.Rows(gv.Rows.Count - 1).Cells(colCash_Amt).Value = obj_Cash.Cash_Amt
                                    End If

                                    gv.Rows(gv.Rows.Count - 1).Cells(colIsSchmItem).Value = "Y"
                                    gv.Rows(gv.Rows.Count - 1).Cells(colSchmCodeType).Value = Nothing
                                End If
                                '>>>>>>>>>>>>>>do code for scheme item------------------------------
                                Dim objD As clsSchemeApplyOnDairy = clsSchemeApplyOnDairy.GetSchemeData(objtr.Itemcode, objtr.BOOK_QTY_UOM, objtr.Bal_Qty, obj.CSA_CODE, Nothing, Nothing, clsCommon.myCDate(dtpdate.Text))

                                Dim index As Integer = gv.Rows.Count - 1
                                If objD IsNot Nothing AndAlso objD.Arr.Count > 0 Then
                                    For Each objtr1 As clsSchemeApplyOnDairy In objD.Arr
                                        '--------------update free itemcode in main item row------------------
                                        gv.Rows(index).Cells(colSchmCode).Value = Nothing
                                        gv.Rows(index).Cells(colSchmCodeType).Value = objtr1.schm_Type
                                        gv.Rows(index).Cells(colMainIcode).Value = objtr1.Schm_Icode
                                        gv.Rows(index).Cells(colMainIQty).Value = objtr1.Schm_Qty
                                        gv.Rows(index).Cells(colMainIUOM).Value = objtr1.Schm_Item_Uom
                                        gv.Rows(index).Cells(colFOC).Value = False
                                        gv.Rows(index).Cells(colIsSchmItem).Value = "Y"
                                        '-------------------------------------------------------------

                                        gv.Rows.AddNew()
                                        gv.Rows(gv.Rows.Count - 1).Cells(colLineno).Value = clsCommon.myCstr(gv.Rows.Count)
                                        gv.Rows(gv.Rows.Count - 1).Cells(colItemCode).Value = objtr1.Schm_Icode
                                        gv.Rows(gv.Rows.Count - 1).Cells(colitemname).Value = objtr1.Schm_Iname
                                        gv.Rows(gv.Rows.Count - 1).Cells(colCSAType).Value = objtr1.Schm_Item_CSA_Type
                                        gv.Rows(gv.Rows.Count - 1).Cells(colUOM).Value = objtr1.Schm_Item_Uom
                                        'gv.Rows(gv.Rows.Count - 1).Cells(colAltUnitCOde).Value = objtr.Schm_Item_Uom
                                        gv.Rows(gv.Rows.Count - 1).Cells(coluomPrice).Value = objtr1.Schm_IUnit_Rate
                                        gv.Rows(gv.Rows.Count - 1).Cells(colQty).Value = objtr1.Schm_Qty
                                        gv.Rows(gv.Rows.Count - 1).Cells(colTotalPrice).Value = 0

                                        gv.Rows(gv.Rows.Count - 1).Cells(colBal_Qty).Value = 0

                                        gv.Rows(gv.Rows.Count - 1).Cells(colSchmCode).Value = objtr1.Schm_Code
                                        gv.Rows(gv.Rows.Count - 1).Cells(colSchmCodeType).Value = objtr1.schm_Type
                                        gv.Rows(gv.Rows.Count - 1).Cells(colMainIcode).Value = objtr.Itemcode
                                        gv.Rows(gv.Rows.Count - 1).Cells(colMainIQty).Value = objtr.Bal_Qty
                                        gv.Rows(gv.Rows.Count - 1).Cells(colMainIUOM).Value = objtr.BOOK_QTY_UOM
                                        gv.Rows(gv.Rows.Count - 1).Cells(colFOC).Value = True
                                        gv.Rows(gv.Rows.Count - 1).Cells(colIsSchmItem).Value = "N"
                                        gv.Rows(gv.Rows.Count - 1).Cells(colIsSchmItem).ReadOnly = True
                                        gv.Rows(gv.Rows.Count - 1).Cells(colQty).ReadOnly = True
                                        gv.Rows(gv.Rows.Count - 1).Cells(colSchmCodeType).ReadOnly = True
                                        gv.Rows(gv.Rows.Count - 1).Cells(coluomPrice).ReadOnly = True
                                    Next
                                End If '-------------------scheme if cond.
                            End If '-----------------end scheme check cond.
                        End If


                        gv.Rows.AddNew()
                    Next

                    RadPageView1.SelectedPage = RadPageViewPage1
                End If ''end if obj cond

                RefreshSerialNo()
                CalDocumentAmt()
                txtcustcode.Enabled = False
                txtfrmloc_code.Enabled = False
            Else
                FunReset()
            End If ''end frm if cond
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub btnAmendment_Click(sender As Object, e As EventArgs) Handles btnAmendment.Click
        Try
            Dim isAmendmentNo As Boolean = False
            Dim Reason As String = ""
            If UsLock1.Status = ERPTransactionStatus.Approved Then
                If common.clsCommon.MyMessageBoxShow(Me, "Do you want to make Amendment", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                    isAmendmentNo = True
                Else
                    Exit Sub
                End If

                For ii As Integer = 0 To gv.Rows.Count - 1
                    Dim strICode As String = clsCommon.myCstr(gv.Rows(ii).Cells(colItemCode).Value)
                    Dim strIName As String = clsCommon.myCstr(gv.Rows(ii).Cells(colitemname).Value)


                    Dim dbltransferqty As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum(qty) from TSPL_CSA_TRANSFER_DETAIL left outer join TSPL_CSA_TRANSFER_Head on TSPL_CSA_TRANSFER_DETAIL.doc_code=TSPL_CSA_TRANSFER_Head.doc_code where isnull(TSPL_CSA_TRANSFER_DETAIL.delevery_order_no,'')='" & clsCommon.myCstr(txtCode.Value) & "' and item_code='" & strICode & "'"))
                    Dim dblQty As Double = clsCommon.myCdbl(gv.Rows(ii).Cells(colQty).Value)
                    If dblQty < dbltransferqty Then
                        common.clsCommon.MyMessageBoxShow("" & dbltransferqty & " Qty of [" + strIName + "] has been used in CSA Transfer," + Environment.NewLine + "Quantity cannot be less than used CSA Transfer Qty at row no. " + clsCommon.myCstr(ii + 1) + "")
                        Exit Sub
                    End If
                Next

                If AllowToSave() AndAlso SaveData(False, isAmendmentNo) Then
                    clsCSADeliveryOrder.PostData(clsCommon.myCstr(txtCode.Value))
                    common.clsCommon.MyMessageBoxShow(Me, "Record Successfully Amendmented.", Me.Text)

                    LoadData(clsCommon.myCstr(txtCode.Value), NavigatorType.Current)
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "Post record first.", Me.Text)
            End If ''approved cond. check

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


End Class
