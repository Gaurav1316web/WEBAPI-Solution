'--------Created By Richa 23/09/2014 Against Ticket No BM00000003639,BM00000006158
Imports common
Imports System.Data.SqlClient

Public Class FrmLCCreation
    Inherits FrmMainTranScreen

#Region "Variables"
    Public AllowModifcationByApprovalUser As Boolean = False
    Dim AllowNLevel As Boolean = False
    Dim Qry As String
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim isFlag As Boolean = False
    Dim isLoadInsideData As Boolean = False
    Public DocumentNo As String = ""
#End Region


#Region "User Defined Functions and Subroutines"

    Public Sub New()
        InitializeComponent()
    End Sub
#End Region

    Private Sub FrmLCCreation_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnclose.Enabled Then
            CloseForm()
        End If
    End Sub

    Private Sub FrmLCCreation_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AllowNLevel = clsApply_Approval.AllowNlevelonScreen(clsUserMgtCode.FrmLCCreation)
        SetUserMgmtNew()
        Reset()
        UcAttachment1.Form_ID = MyBase.Form_ID
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Transaction")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N New Transaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Transaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        If clsCommon.myLen(DocumentNo) > 0 Then
            LoadData(DocumentNo, NavigatorType.Current)
        End If
    End Sub
    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        CloseForm()
    End Sub

    Private Sub btndelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub btnnew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnnew.Click
        Reset()
    End Sub

    Private Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub
    Private Sub CloseForm()
        Me.Close()
        GC.Collect()
    End Sub
    Private Sub DeleteData()
        Try
            If (deleteConfirm()) Then

                'done by stuti on 1/12/2016 for N-LevelApproval
                If AllowNLevel Then
                    clsApply_Approval.CheckUpdate_Doc_Valid(clsUserMgtCode.FrmLCCreation, clsCommon.myCstr(fndLCCreationcode.Value))
                End If
                '===========================================================
                If (ClsLCCreation.DeleteData(fndLCCreationcode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data deleted successfully ")
                    Reset()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub Reset()
        If AllowNLevel Then
            btnPost.Visible = MyBase.isPostFlag
        End If
        fndLCCreationcode.Value = ""
        FndLCRequestNo.Value = ""
        TxtBankCode.Text = ""
        LblBankName.Text = ""
        TxtLCAmount.Value = 0
        TxtPurchaseOrderNo.Text = ""
        TxtPurchaseInvoiceNo.Text = ""
        TxtBenefeciery.Text = ""
        lblBenefeciery.Text = ""
        TxtLCPeriod.Value = 0
        TxtLCCharges.Value = 0
        TxtLCNo.Text = ""
        LoadLCPeriod()
        LoadFDType()
        cboLCPeriod.SelectedValue = ""
        cmbFDType.SelectedValue = ""
        FndFDNo.Value = ""
        ddlLCType.Text = ""
        lblLCExpiryDate.Text = ""
        TxtLocationCode.Text = ""
        lblLocationDesc.Text = ""
        txtCurrencyCode.Value = ""
        txtConversionRate.Value = 0
        ddlLCType.Enabled = False
        TxtLCPeriod.Enabled = False
        cboLCPeriod.Enabled = False
        RadPageView1.SelectedPage = RadPageViewPage1
        UsLock1.Status = ERPTransactionStatus.Pending
        txtLCCreationdate.Value = clsCommon.GETSERVERDATE()
        UcAttachment1.BlankAllControls()
        fndLCCreationcode.MyReadOnly = False
        btnsave.Text = "Save"
        btndelete.Enabled = False
        BtnLcCancellation.Enabled = False
        btnsave.Enabled = True
        isNewEntry = True
        isLoadInsideData = True
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmLCCreation)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FndLCRequestNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles FndLCRequestNo._MYValidating
        Dim dt As DataTable
        FndLCRequestNo.Value = ClsLCRequest.getFinder(" TSPL_LC_REQUEST_MT.Posted=1 and TSPL_LC_REQUEST_MT.LCRequestNo not in (Select LCRequestNo from TSPL_LC_CREATION_MT where LCCreationNo <> '" & fndLCCreationcode.Value & "'  ) ", "", isButtonClicked)
        If clsCommon.myLen(FndLCRequestNo.Value) > 0 Then
            dt = clsDBFuncationality.GetDataTable("Select TSPL_LC_REQUEST_MT.LCPeriod,TSPL_LC_REQUEST_MT.PurchaseInvoice_No ,TSPL_LC_REQUEST_MT.LCPeriodType ,TSPL_LC_REQUEST_MT.LCType,TSPL_LC_REQUEST_MT.Bank_Code ,TSPL_LC_REQUEST_MT.Bank_Name,TSPL_LC_REQUEST_MT.Location_Code ,TSPL_LC_REQUEST_MT.Location_Desc ,TSPL_LC_REQUEST_MT.ConvRate,TSPL_LC_REQUEST_MT.CURRENCY_CODE ,TSPL_LC_REQUEST_MT.PurchaseOrder_No ,TSPL_LC_REQUEST_MT.VendorCode ,LCAmount,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_LC_REQUEST_MT.LCAmount from TSPL_LC_REQUEST_MT Left Outer Join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_LC_REQUEST_MT.VendorCode where TSPL_LC_REQUEST_MT.LCRequestNo ='" & FndLCRequestNo.Value & "'")
            If dt.Rows.Count > 0 Then
                TxtBankCode.Text = clsCommon.myCstr(dt.Rows(0)("Bank_Code"))
                LblBankName.Text = clsCommon.myCstr(dt.Rows(0)("Bank_Name"))
                TxtLocationCode.Text = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
                lblLocationDesc.Text = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
                TxtPurchaseOrderNo.Text = clsCommon.myCstr(dt.Rows(0)("PurchaseOrder_No"))
                TxtPurchaseInvoiceNo.Text = clsCommon.myCstr(dt.Rows(0)("PurchaseInvoice_No"))
                TxtBenefeciery.Text = clsCommon.myCstr(dt.Rows(0)("VendorCode"))
                lblBenefeciery.Text = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
                TxtLCAmount.Value = clsCommon.myCdbl(dt.Rows(0)("LCAmount"))
                txtCurrencyCode.Value = clsCommon.myCstr(dt.Rows(0)("CURRENCY_CODE"))
                txtConversionRate.Value = clsCommon.myCdbl(dt.Rows(0)("ConvRate"))
                If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("LCType")), "U") = CompairStringResult.Equal Then
                    ddlLCType.Text = "Usance"
                Else
                    ddlLCType.Text = "Sight"
                End If
                TxtLCPeriod.Value = clsCommon.myCdbl(dt.Rows(0)("LCPeriod"))
                cboLCPeriod.SelectedValue = clsCommon.myCstr(dt.Rows(0)("LCPeriodType"))
            Else
                TxtLCPeriod.Value = 0
                cboLCPeriod.SelectedValue = ""
                ddlLCType.Text = ""
                TxtBankCode.Text = ""
                LblBankName.Text = ""
                TxtPurchaseOrderNo.Text = ""
                TxtPurchaseInvoiceNo.Text = ""
                TxtBenefeciery.Text = ""
                lblBenefeciery.Text = ""
                TxtLCAmount.Value = 0
                TxtLocationCode.Text = ""
                lblLocationDesc.Text = ""
                txtCurrencyCode.Value = ""
                txtConversionRate.Value = 0
            End If
        End If
    End Sub
    Private Function AllowToSave() As Boolean
        If AllowFutureDateTransaction(txtLCCreationdate.Value, Nothing) = False Then
            txtLCCreationdate.Select()
            Return False
        End If
        Dim LCCreditValue As Double = 0
        If clsCommon.myLen(FndLCRequestNo.Value) <= 0 Then
            FndLCRequestNo.Focus()
            Throw New Exception("LC Request No cannot be left blank")
        End If

        'If clsCommon.myLen(FndPurchaseOrderNo.Value) <= 0 Then
        '    FndPurchaseOrderNo.Focus()
        '    Throw New Exception("Purchase Order No cannot be Zero or blank")
        'End If

        If clsCommon.myCdbl(TxtLCAmount.Value) < 0 Then
            TxtLCAmount.Focus()
            Throw New Exception("LC Amount cannot be negative")
        End If

        If clsCommon.myCdbl(TxtLCAmount.Value) = 0 Then
            TxtLCAmount.Focus()
            Throw New Exception("LC Amount cannot be zero")
        End If
        'If clsCommon.myCdbl(TxtLCPeriod.Value) < 0 Then
        '    TxtLCPeriod.Focus()
        '    Throw New Exception("LC Period cannot be negative")
        'End If
        'If clsCommon.myCdbl(TxtLCPeriod.Value) = 0 Then
        '    TxtLCPeriod.Focus()
        '    Throw New Exception("LC Period cannot be zero")
        'End If

        If clsCommon.CompairString(cmbFDType.SelectedValue, "N") = CompairStringResult.Equal Or clsCommon.CompairString(cmbFDType.SelectedValue, "Ex") = CompairStringResult.Equal Then
            If clsCommon.myCstr(FndFDNo.Value) = "" Then
                FndFDNo.Focus()
                Throw New Exception("FD No cannot be left blank")
            End If
        End If
        'LCCreditValue = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select LCCreditLimit from TSPL_BANK_master where BANK_CODE ='" & fndBankCode.Value & "'"))
        'If clsCommon.myCdbl(TxtLCAmount.Value) > LCCreditValue Then
        '    TxtLCAmount.Focus()
        '    Throw New Exception("LC Amount cannot be greater than " & LCCreditValue & "")
        'End If
        'If clsCommon.myCdbl(TxtFDPer.Value) < 0 Then
        '    TxtFDPer.Focus()
        '    Throw New Exception("FD % cannot be negative")
        'End If

        'If clsCommon.myCdbl(TxtFDPer.Value) = 0 Then
        '    TxtFDPer.Focus()
        '    Throw New Exception("FD % cannot be zero")
        'End If
        If clsCommon.myLen(TxtLCNo.Text) <= 0 Then
            TxtLCNo.Focus()
            Throw New Exception("LC No cannot be left blank")
        End If
        Return True
    End Function
    Sub SaveData()
        Try
            'done by stuti against approval work n 09/12/2016
            Dim totalqty As Decimal = 0
            Dim objApproval As New clsApply_Approval()
            If AllowNLevel Then
                If Not AllowModifcationByApprovalUser Then
                    clsApply_Approval.CheckUpdate_Doc_Valid(clsUserMgtCode.FrmLCCreation, clsCommon.myCstr(fndLCCreationcode.Value))
                End If
            End If
            '======end here=============
            If AllowToSave() Then
                Dim obj As New ClsLCCreation()
                obj.LCCreationNo = fndLCCreationcode.Value
                obj.LCRequestNo = FndLCRequestNo.Value
                obj.LCCreation_Date = txtLCCreationdate.Value
                obj.Bank_Code = clsCommon.myCstr(TxtBankCode.Text)
                obj.Bank_name = clsCommon.myCstr(LblBankName.Text)
                obj.Location_Code = clsCommon.myCstr(TxtLocationCode.Text)
                obj.Location_Desc = clsCommon.myCstr(lblLocationDesc.Text)
                If clsCommon.myLen(TxtPurchaseOrderNo.Text) > 0 Then
                    obj.PurchaseOrder_No = clsCommon.myCstr(TxtPurchaseOrderNo.Text)
                    If clsCommon.myLen(TxtPurchaseInvoiceNo.Text) > 0 Then
                        obj.PurchaseInvoice_No = clsCommon.myCstr(TxtPurchaseInvoiceNo.Text)
                    End If
                Else
                    obj.PurchaseInvoice_No = clsCommon.myCstr(TxtPurchaseInvoiceNo.Text)
                End If

                obj.BenefecieryCode = clsCommon.myCstr(TxtBenefeciery.Text)
                obj.BenefecieryName = clsCommon.myCstr(lblBenefeciery.Text)
                obj.LCAmount = clsCommon.myCdbl(TxtLCAmount.Value)
                ' obj.LCExpiryDate = clsCommon.myCstr(lblLCExpiryDate.Text)
                obj.LCType = clsCommon.myCstr(ddlLCType.Text)
                obj.LCPeriod = clsCommon.myCdbl(TxtLCPeriod.Value)
                obj.LCPeriodType = clsCommon.myCstr(cboLCPeriod.SelectedValue)
                obj.CURRENCY_CODE = clsCommon.myCstr(txtCurrencyCode.Value)
                obj.ConvRate = clsCommon.myCdbl(txtConversionRate.Value)
                obj.LCCharges = clsCommon.myCdbl(TxtLCCharges.Value)
                obj.LCNo = clsCommon.myCstr(TxtLCNo.Text)
                obj.ApprovalRequired = "N"
                obj.Status = "Open"
                obj.LCCreationType = "General"
                If clsCommon.CompairString(cmbFDType.SelectedValue, "N") = CompairStringResult.Equal Or clsCommon.CompairString(cmbFDType.SelectedValue, "Ex") = CompairStringResult.Equal Then
                    obj.FDType = clsCommon.myCstr(cmbFDType.SelectedValue)
                    obj.FD_No = clsCommon.myCstr(FndFDNo.Value)
                End If

                If ApprovalRequiredforLCAmount(True) = False Then
                    obj.ApprovalRequired = "Y"
                    obj.Status = "Pending"
                    obj.LCCreationType = "Increase LC Amount"
                End If
                If clsCommon.CompairString(clsDBFuncationality.getSingleValue("Select Approved from TSPL_LC_CREATION_MT where LCCreationNo='" & fndLCCreationcode.Value & "' "), "Y") = CompairStringResult.Equal Then
                    obj.ApprovalRequired = "Y"
                    obj.Status = "Approved"
                End If
                If (ClsLCCreation.SaveData(obj, isNewEntry)) Then
                    UcAttachment1.SaveData(obj.LCCreationNo)
                    If Not isFlag Then
                        clsCommon.MyMessageBoxShow("Data saved successfully", Me.Text)
                    End If
                    ''done by stuti approval work 12/12/2016
                    If AllowNLevel Then
                        objApproval.LCRequestNo = FndLCRequestNo.Value
                        objApproval.TotAmt = clsCommon.myCdbl(TxtLCAmount.Text)
                        clsApply_Approval.CheckApprovalRequired(clsUserMgtCode.FrmLCCreation, clsCommon.myCstr(obj.LCCreationNo), txtLCCreationdate.Text, "", "", clsCommon.myCdbl(TxtLCAmount.Text), clsCommon.myCdbl(totalqty), "", objApproval)
                    End If
                    '===================================================end here===============================================

                    LoadData(obj.LCCreationNo, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Private Function ApprovalRequiredforLCAmount(ByVal ChekPostBtn As Boolean) As Boolean
        Try

            Dim LCAmount As Double = clsDBFuncationality.getSingleValue("select LCAmount from TSPL_LC_REQUEST_MT where LCRequestNo='" & FndLCRequestNo.Value & "'")
           
           
            ''richa 31/12/2014
            If clsCommon.myCdbl(TxtLCAmount.Value) > LCAmount AndAlso UsLock1.Status = ERPTransactionStatus.Open Then
                If Not AllowNLevel Then
                    common.clsCommon.MyMessageBoxShow("Please send for approval for increasing LC Amount")
                End If
                Return False
            End If
            If clsCommon.myCdbl(TxtLCAmount.Value) > LCAmount And UsLock1.Status = ERPTransactionStatus.Pending Then
                If Not AllowNLevel Then
                    common.clsCommon.MyMessageBoxShow("Please increase LC Amount ")
                End If
                Return False
            End If

            If ChekPostBtn = True Then
                Return True
            End If
            Return True



        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
    End Function
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim objApproval As New clsApply_Approval()
        Dim obj As ClsLCCreation = ClsLCCreation.GetData(strCode, NavTyep)
        If obj IsNot Nothing Then
            isNewEntry = False
            fndLCCreationcode.Value = obj.LCCreationNo
            FndLCRequestNo.Value = obj.LCRequestNo
            txtLCCreationdate.Value = obj.LCCreation_Date
            TxtBankCode.Text = obj.Bank_Code
            LblBankName.Text = obj.Bank_name
            TxtLocationCode.Text = obj.Location_Code
            lblLocationDesc.Text = obj.Location_Desc
            TxtPurchaseOrderNo.Text = obj.PurchaseOrder_No
            TxtPurchaseInvoiceNo.Text = obj.PurchaseInvoice_No
            TxtBenefeciery.Text = obj.BenefecieryCode
            lblBenefeciery.Text = obj.BenefecieryName
            TxtLCAmount.Value = obj.LCAmount
            ' lblLCExpiryDate.Text = obj.LCExpiryDate
            ddlLCType.Text = obj.LCType
            TxtLCPeriod.Value = obj.LCPeriod
            cboLCPeriod.SelectedValue = obj.LCPeriodType
            txtCurrencyCode.Value = obj.CURRENCY_CODE
            txtConversionRate.Value = obj.ConvRate
            TxtLCCharges.Value = obj.LCCharges
            TxtLCNo.Text = obj.LCNo
            If clsCommon.CompairString(obj.FDType, "N") = CompairStringResult.Equal Or clsCommon.CompairString(obj.FDType, "Ex") = CompairStringResult.Equal Then
                cmbFDType.SelectedValue = obj.FDType
                FndFDNo.Value = obj.FD_No
            End If
            fndLCCreationcode.MyReadOnly = True
            btnsave.Text = "Update"
            If clsCommon.CompairString(obj.LCCreationType, "Cancelled") = CompairStringResult.Equal Then
                btnPost.Enabled = False
                btnsave.Enabled = False
                btndelete.Enabled = False
                BtnLcCancellation.Enabled = False
                UsLock1.Status = ERPTransactionStatus.Cancel
            Else
                If clsCommon.CompairString(obj.Posted, "1") = CompairStringResult.Equal Then
                    btnPost.Enabled = False
                    btnsave.Enabled = False
                    btndelete.Enabled = False
                    BtnLcCancellation.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Approved
                Else
                    btnPost.Enabled = True
                    btnsave.Enabled = True
                    btndelete.Enabled = True
                    BtnLcCancellation.Enabled = True
                    UsLock1.Status = ERPTransactionStatus.Pending
                End If
            End If
            UcAttachment1.LoadData(obj.LCCreationNo)
        Else
            Reset()
        End If

        If AllowNLevel Then
            btnPost.Visible = MyBase.isPostFlag
            objApproval.LCRequestNo = FndLCRequestNo.Value
            objApproval.TotAmt = clsCommon.myCdbl(TxtLCAmount.Text)
            If Not clsApply_Approval.Visibility_PostButtonForApproval(clsUserMgtCode.FrmLCCreation, clsCommon.myCstr(fndLCCreationcode.Value), clsCommon.myCdbl(TxtLCAmount.Text), 0, "", objApproval) Then
                btnPost.Visible = False
                If UsLock1.Status = ERPTransactionStatus.Pending Then
                    UsLock1.Status = clsApply_Approval.ApprovalCondCheck_Doc(clsUserMgtCode.FrmLCCreation, clsCommon.myCstr(fndLCCreationcode.Value), Nothing)
                End If
            End If
        End If

    End Sub
    Sub PostData()
        Try
            If ApprovalRequiredforLCAmount(False) = False Then Exit Sub
            isFlag = True
            If (myMessages.postConfirm()) Then
                '' richa agarwal against ticket no BM00000006073 on 16/04/2015
                If clsCommon.myLen(FndFDNo.Value) <= 0 Then
                    FndFDNo.Focus()
                    Throw New Exception("FD No cannot be left blank")
                End If
                '-------------------------------------------
                SaveData()
                If (ClsLCCreation.PostData(MyBase.Form_ID, fndLCCreationcode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Successfully posted")
                    LoadData(fndLCCreationcode.Value, NavigatorType.Current)
                End If
            End If
            isFlag = False
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isFlag = False
        End Try
    End Sub

    Private Sub fndLCCreationcode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndLCCreationcode._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_LC_CREATION_MT where LCCreationNo='" + fndLCCreationcode.Value + "' and Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
            Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If check > 0 Then
                fndLCCreationcode.MyReadOnly = True
            ElseIf check <= 0 Then
                fndLCCreationcode.MyReadOnly = False
            End If
            LoadData(fndLCCreationcode.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndLCCreationcode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndLCCreationcode._MYValidating
        fndLCCreationcode.Value = ClsLCCreation.getFinder("", fndLCCreationcode.Value, isButtonClicked)
        LoadData(fndLCCreationcode.Value, NavigatorType.Current)
    End Sub
    Sub LoadLCPeriod()
        Dim dt As New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        dt.Rows.Add("", " ")
        dt.Rows.Add("D", "Days")
        dt.Rows.Add("M", "Month")
        dt.Rows.Add("Y", "Year")

        cboLCPeriod.DataSource = dt
        cboLCPeriod.ValueMember = "Code"
        cboLCPeriod.DisplayMember = "Name"
    End Sub
    Sub LoadFDType()
        Dim dt As New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        dt.Rows.Add("", "Select")
        dt.Rows.Add("N", "New")
        dt.Rows.Add("Ex", "Existing")

        cmbFDType.DataSource = dt
        cmbFDType.ValueMember = "Code"
        cmbFDType.DisplayMember = "Name"
    End Sub
    'Sub FunLCExpiryDate()
    '    If isLoadInsideData Then
    '        Dim strLCExpiryDate As String = ""
    '        If clsCommon.myLen(TxtLCPeriod.Value) <= 0 Then
    '            TxtLCPeriod.Value = 0
    '        End If
    '        If clsCommon.CompairString(cboLCPeriod.SelectedValue, "D") = CompairStringResult.Equal Then
    '            strLCExpiryDate = txtLCCreationdate.Value.AddDays(TxtLCPeriod.Value).ToShortDateString()
    '        ElseIf clsCommon.CompairString(cboLCPeriod.SelectedValue, "M") = CompairStringResult.Equal Then
    '            strLCExpiryDate = txtLCCreationdate.Value.AddMonths(TxtLCPeriod.Value).ToShortDateString()
    '        ElseIf clsCommon.CompairString(cboLCPeriod.SelectedValue, "Y") = CompairStringResult.Equal Then
    '            strLCExpiryDate = txtLCCreationdate.Value.AddYears(TxtLCPeriod.Value).ToShortDateString()
    '        End If
    '        lblLCExpiryDate.Text = strLCExpiryDate
    '    End If
    'End Sub

    Private Sub TxtLCPeriod_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtLCPeriod.TextChanged
        ' FunLCExpiryDate()
    End Sub

   
    Private Sub cboLCPeriod_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cboLCPeriod.SelectedIndexChanged
        '  FunLCExpiryDate()
    End Sub

    Private Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Private Sub BtnLcCancellation_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnLcCancellation.Click
        If clsCommon.myLen(fndLCCreationcode.Value) > 0 Then
            Dim frm As New FrmPWD(Nothing)
            frm.strCode = "LCCreationPwd"
            frm.strType = "LCCancellationPwd"
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                clsDBFuncationality.ExecuteNonQuery("update tspl_lc_creation_mt set lccreationtype='Cancelled' where lccreationno='" & fndLCCreationcode.Value & "'")
                LoadData(fndLCCreationcode.Value, NavigatorType.Current)
            Else
                clsCommon.MyMessageBoxShow("you have entered wrong password")
            End If
        Else
            clsCommon.MyMessageBoxShow("LC cannot be cancalled")
        End If
      
    End Sub

 
    Private Sub FndFDNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles FndFDNo._MYValidating
        If clsCommon.CompairString(cmbFDType.SelectedValue, "Ex") = CompairStringResult.Equal Then
            Dim qry As String = "Select Document_No as FDNo,Document_Date as [FD Date] from TSPL_FIXED_DEPOSIT_MASTER_MT "
            Dim strwhrcls As String = " isnull(Document_No,'') not in (select isnull(FD_No,'') from TSPL_LC_CREATION_MT where LCCreationNo <> '" & fndLCCreationcode.Value & "'  )"
            FndFDNo.Value = clsCommon.ShowSelectForm("FDNoForm", qry, "FDNo", strwhrcls, FndFDNo.Value, "", isButtonClicked)
        End If
    End Sub

    Private Sub FndFDNo__MYOpenMasterForm(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles FndFDNo._MYOpenMasterForm
        If clsCommon.CompairString(cmbFDType.SelectedValue, "N") = CompairStringResult.Equal Then
            Dim frmFD As New FrmFixedDeposit()
            frmFD.ShowDialog()
            FndFDNo.Value = frmFD.FDNOValue
        End If
    End Sub

  
End Class
