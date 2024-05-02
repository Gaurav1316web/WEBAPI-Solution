'--------Created By Richa 29/08/2014 Against Ticket No BM00000003640
Imports common
Imports System.Data.SqlClient
Public Class FrmFixedDeposit
    Inherits FrmMainTranScreen

#Region "Variables"

    Dim Qry As String
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim isFlag As Boolean = False
    Dim isLoadInsideData As Boolean = False
    Public FDNOValue As String = ""
#End Region


#Region "User Defined Functions and Subroutines"

    Public Sub New()
        InitializeComponent()
    End Sub
#End Region

    Private Sub FrmFixedDeposit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnclose.Enabled Then
            CloseForm()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        End If
    End Sub

    Private Sub FrmFixedDeposit_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        Reset()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Transaction")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N New Transaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Transaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        CloseForm()
        FDNOValue = fndFixedDepositcode.Value
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

    Private Sub fndBankCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndBankCode._MYValidating
        fndBankCode.Value = clsBankMaster.getFinder("", "", isButtonClicked)
        LblBankName.Text = clsDBFuncationality.getSingleValue("Select DESCRIPTION  from TSPL_BANK_MASTER where BANK_CODE  ='" + fndBankCode.Value + "' ")
    End Sub

    Private Sub fndFixedDepositcode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndFixedDepositcode._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_FIXED_DEPOSIT_MASTER_MT where Document_No='" + fndFixedDepositcode.Value + "' and Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
            Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If check > 0 Then
                fndFixedDepositcode.MyReadOnly = True
            ElseIf check <= 0 Then
                fndFixedDepositcode.MyReadOnly = False
            End If
            LoadData(fndFixedDepositcode.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub fndFixedDepositcode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndFixedDepositcode._MYValidating
        'Dim qry As String = "Select TSPL_FIXED_DEPOSIT_MASTER_MT.Document_No as [Code],TSPL_FIXED_DEPOSIT_MASTER_MT.Document_Date as Date,TSPL_FIXED_DEPOSIT_MASTER_MT.Bank_Code as [Bank Code],TSPL_FIXED_DEPOSIT_MASTER_MT.Duration,TSPL_FIXED_DEPOSIT_MASTER_MT.Amount from TSPL_FIXED_DEPOSIT_MASTER_MT"
        'fndFixedDepositcode.Value = clsCommon.ShowSelectForm("FixedDeposit", qry, "Code", "", fndFixedDepositcode.Value, "", isButtonClicked)
        fndFixedDepositcode.Value = ClsFixedDeposit.getFinder("", fndFixedDepositcode.Value, isButtonClicked)
        LoadData(fndFixedDepositcode.Value, NavigatorType.Current)
    End Sub
    Private Function AllowToSave() As Boolean
        If AllowFutureDateTransaction(txtFddate.Value, Nothing) = False Then
            txtFddate.Select()
            Return False
        End If
        If clsCommon.myLen(fndBankCode.Value) <= 0 Then
            fndBankCode.Focus()
            Throw New Exception("Bank Code cannot be left blank")
        End If

        If clsCommon.myCdbl(TxtDuration.Value) < 0 Then
            TxtDuration.Focus()
            Throw New Exception("Duration cannot be left blank")
        End If
        If clsCommon.myCdbl(TxtDuration.Value) = 0 Then
            TxtDuration.Focus()
            Throw New Exception("Duration cannot be Zero")
        End If
        If clsCommon.myCdbl(TxtRateofInterest.Value) < 0 Then
            TxtRateofInterest.Focus()
            Throw New Exception("Rate cannot be left blank")
        End If
        If clsCommon.myCdbl(TxtRateofInterest.Value) = 0 Then
            TxtRateofInterest.Focus()
            Throw New Exception("Rate cannot be Zero")
        End If
        If clsCommon.myCdbl(TxtAmount.Value) < 0 Then
            TxtAmount.Focus()
            Throw New Exception("Amount cannot be left blank")
        End If
        If clsCommon.myCdbl(TxtAmount.Value) = 0 Then
            TxtAmount.Focus()
            Throw New Exception("Amount cannot be Zero")
        End If
        If clsCommon.myLen(fndGLAccount.Value) <= 0 Then
            fndGLAccount.Focus()
            Throw New Exception("GL Account cannot be left blank")
        End If


        'If clsCommon.CompairString(ddlDurationdescription.Text, "SELECT") = CompairStringResult.Equal Then
        '    ddlDurationdescription.Focus()
        '    Throw New Exception("Duration Description cannot be SELECT")
        'End If

        If clsCommon.myCdbl(TxtAmount.Value) <= 0 Then
            TxtDuration.Focus()
            Throw New Exception("Amount cannot be Zero or blank")
        End If

        Return True
    End Function
    Private Sub CloseForm()
        Me.Close()
        GC.Collect()
    End Sub
    Private Sub DeleteData()
        Try
            If (deleteConfirm()) Then
                If (ClsFixedDeposit.DeleteData(fndFixedDepositcode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    Reset()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub Reset()

        fndFixedDepositcode.Value = ""
        fndBankCode.Value = ""
        LblBankName.Text = ""
        TxtDuration.Value = 0
        TxtAmount.Value = 0
        txtFddate.Value = clsCommon.GETSERVERDATE()
        LoadDurationDesc()
        cboDurationdescription.SelectedValue = "D"
        TxtDueDate.Text = ""
        TxtmaturityAmount.Value = 0
        TxtRateofInterest.Value = 0
        TxtFDRNo.Text = ""
        FndLCRequestNo.Value = ""
        fndGLAccount.Value = ""
        fndFixedDepositcode.MyReadOnly = False
        UsLock1.Status = ERPTransactionStatus.Pending
        btnsave.Text = "Save"
        btndelete.Enabled = False
        btnsave.Enabled = True
        isNewEntry = True
        isLoadInsideData = True
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmFixedDeposit)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
        btnPost.Visible = MyBase.isPostFlag
    End Sub
    Sub SaveData()
        Try
            If AllowToSave() Then
                Dim obj As New ClsFixedDeposit()
                obj.Document_No = fndFixedDepositcode.Value
                obj.Document_Date = txtFddate.Value
                obj.Bank_Code = clsCommon.myCstr(fndBankCode.Value)
                obj.Amount = clsCommon.myCdbl(TxtAmount.Value)
                obj.Duration = clsCommon.myCdbl(TxtDuration.Value)
                obj.Duration_Desp = clsCommon.myCstr(cboDurationdescription.SelectedValue)
                obj.MaturityAmount = clsCommon.myCdbl(TxtmaturityAmount.Value)
                obj.Rate = clsCommon.myCdbl(TxtRateofInterest.Value)
                obj.FDRNo = clsCommon.myCstr(TxtFDRNo.Text)
                obj.Due_Date = clsCommon.myCDate(TxtDueDate.Text)
                obj.LCRequestNo = clsCommon.myCstr(FndLCRequestNo.Value)
                obj.GL_Account_Code = clsCommon.myCstr(fndGLAccount.Value)
                obj.GL_Account_Name = clsCommon.myCstr(Lblglaccount.Text)
                If (ClsFixedDeposit.SaveData(obj, isNewEntry)) Then
                    If Not isFlag Then
                        clsCommon.MyMessageBoxShow("Data saved successfully", Me.Text)
                        LoadData(obj.Document_No, NavigatorType.Current)
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadDurationDesc()
        Dim dt As New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        dt.Rows.Add("D", "Days")
        dt.Rows.Add("M", "Month")
        dt.Rows.Add("Y", "Year")

        cboDurationdescription.DataSource = dt
        cboDurationdescription.ValueMember = "Code"
        cboDurationdescription.DisplayMember = "Name"
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As ClsFixedDeposit = ClsFixedDeposit.GetData(strCode, NavTyep)
        If obj IsNot Nothing Then
            isNewEntry = False
            fndFixedDepositcode.Value = obj.Document_No
            txtFddate.Value = obj.Document_Date
            fndBankCode.Value = obj.Bank_Code
            LblBankName.Text = obj.BankName
            TxtAmount.Value = obj.Amount
            TxtDuration.Value = obj.Duration
            cboDurationdescription.SelectedValue = obj.Duration_Desp
            TxtDueDate.Text = obj.Due_Date
            TxtmaturityAmount.Value = obj.MaturityAmount
            TxtRateofInterest.Value = obj.Rate
            TxtFDRNo.Text = obj.FDRNo
            FndLCRequestNo.Value = obj.LCRequestNo
            fndGLAccount.Value = obj.GL_Account_Code
            Lblglaccount.Text = obj.GL_Account_Name
            fndFixedDepositcode.MyReadOnly = True
            btnsave.Text = "Update"
            btnsave.Enabled = True
            btndelete.Enabled = True
            If clsCommon.CompairString(obj.Posted, "1") = CompairStringResult.Equal Then
                btnPost.Enabled = False
                btnsave.Enabled = False
                btndelete.Enabled = False
                UsLock1.Status = ERPTransactionStatus.Approved
            Else
                btnPost.Enabled = True
                btnsave.Enabled = True
                btndelete.Enabled = True
                UsLock1.Status = ERPTransactionStatus.Pending
            End If
        Else
            Reset()
        End If
    End Sub

    Private Sub TxtDuration_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtDuration.TextChanged
        FunDueDate()
        CalculateMaturityAmount()
    End Sub

    Private Sub cboDurationdescription_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cboDurationdescription.SelectedIndexChanged
        FunDueDate()
        CalculateMaturityAmount()
    End Sub
    Sub FunDueDate()
        If isLoadInsideData Then
            Dim strDueDate As String = ""
            If clsCommon.myLen(TxtDuration.Value) <= 0 Then
                TxtDuration.Value = 0
            End If
            If clsCommon.CompairString(cboDurationdescription.SelectedValue, "D") = CompairStringResult.Equal Then
                strDueDate = txtFddate.Value.AddDays(TxtDuration.Value).ToShortDateString()
            ElseIf clsCommon.CompairString(cboDurationdescription.SelectedValue, "M") = CompairStringResult.Equal Then
                strDueDate = txtFddate.Value.AddMonths(TxtDuration.Value).ToShortDateString()
            ElseIf clsCommon.CompairString(cboDurationdescription.SelectedValue, "Y") = CompairStringResult.Equal Then
                strDueDate = txtFddate.Value.AddYears(TxtDuration.Value).ToShortDateString()
            End If
            TxtDueDate.Text = strDueDate
        End If
    End Sub

    Private Sub FndLCRequestNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles FndLCRequestNo._MYValidating
        ' Dim dt As DataTable
        'FndLCRequestNo.Value = ClsLCRequest.getFinder(" TSPL_LC_REQUEST_MT.Posted=1 and TSPL_LC_REQUEST_MT.LCRequestNo not in (Select LCRequestNo from TSPL_FIXED_DEPOSIT_MASTER_MT where Document_No <> '" & fndFixedDepositcode.Value & "'  ) ", "", isButtonClicked)
        FndLCRequestNo.Value = clsCommon.ShowSelectForm("LCRequestNoForm", "Select LCRequestNo,Bank_Code ,Bank_Name,PurchaseOrder_No,BenefecieryCode ,BenefecieryName,Location_Code ,Location_Desc  from TSPL_LC_CREATION_MT ", "LCRequestNo", " Posted =1 and isnull(LCCreationType,'')<> 'Cancelled' and LCRequestNo not in (Select LCRequestNo from TSPL_FIXED_DEPOSIT_MASTER_MT where Document_No <> '" & fndFixedDepositcode.Value & "'  ) ", FndLCRequestNo.Value, "", isButtonClicked)
        'If clsCommon.myLen(FndLCRequestNo.Value) > 0 Then
        '    dt = clsDBFuncationality.GetDataTable("Select TSPL_LC_REQUEST_MT.Bank_Code ,TSPL_LC_REQUEST_MT.Bank_Name,TSPL_LC_REQUEST_MT.Location_Code ,TSPL_LC_REQUEST_MT.Location_Desc ,TSPL_LC_REQUEST_MT.ConvRate,TSPL_LC_REQUEST_MT.CURRENCY_CODE ,TSPL_LC_REQUEST_MT.PurchaseOrder_No ,TSPL_LC_REQUEST_MT.VendorCode ,LCAmount,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_LC_REQUEST_MT.LCAmount from TSPL_LC_REQUEST_MT Left Outer Join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_LC_REQUEST_MT.VendorCode where TSPL_LC_REQUEST_MT.LCRequestNo ='" & FndLCRequestNo.Value & "'")
        '    If dt.Rows.Count > 0 Then
        '        TxtBankCode.Text = clsCommon.myCstr(dt.Rows(0)("Bank_Code"))
        '        LblBankName.Text = clsCommon.myCstr(dt.Rows(0)("Bank_Name"))
        '        TxtLocationCode.Text = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
        '        lblLocationDesc.Text = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
        '        TxtPurchaseOrderNo.Text = clsCommon.myCstr(dt.Rows(0)("PurchaseOrder_No"))
        '        TxtBenefeciery.Text = clsCommon.myCstr(dt.Rows(0)("VendorCode"))
        '        lblBenefeciery.Text = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
        '        TxtLCAmount.Value = clsCommon.myCdbl(dt.Rows(0)("LCAmount"))
        '        txtCurrencyCode.Value = clsCommon.myCstr(dt.Rows(0)("CURRENCY_CODE"))
        '        txtConversionRate.Value = clsCommon.myCdbl(dt.Rows(0)("ConvRate"))
        '    Else
        '        TxtBankCode.Text = ""
        '        LblBankName.Text = ""
        '        TxtPurchaseOrderNo.Text = ""
        '        TxtBenefeciery.Text = ""
        '        lblBenefeciery.Text = ""
        '        TxtLCAmount.Value = 0
        '        TxtLocationCode.Text = ""
        '        lblLocationDesc.Text = ""
        '        txtCurrencyCode.Value = ""
        '        txtConversionRate.Value = 0
        '    End If
        'End If
    End Sub

    Private Sub TxtRateofInterest_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtRateofInterest.TextChanged
        CalculateMaturityAmount()
    End Sub

    Private Sub TxtAmount_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtAmount.TextChanged
        CalculateMaturityAmount()
    End Sub
    Sub CalculateMaturityAmount()
        If isLoadInsideData Then
            Dim MaturityAmount As Double = 0
            Dim Amount As Double = 0
            Dim Rate As Double = 0
            Dim DayMonthYear As Double = 0
            Dim Interest As Double = 0
            Amount = clsCommon.myCdbl(TxtAmount.Value)
            Rate = clsCommon.myCdbl(TxtRateofInterest.Value)
            DayMonthYear = clsCommon.myCdbl(TxtDuration.Value)
            If clsCommon.CompairString(cboDurationdescription.SelectedValue, "D") = CompairStringResult.Equal Then
                Interest = (Amount * Rate * (DayMonthYear / 365)) / 100
            ElseIf clsCommon.CompairString(cboDurationdescription.SelectedValue, "M") = CompairStringResult.Equal Then
                Interest = (Amount * Rate * (DayMonthYear / 12)) / 100
            ElseIf clsCommon.CompairString(cboDurationdescription.SelectedValue, "Y") = CompairStringResult.Equal Then
                Interest = (Amount * Rate * DayMonthYear) / 100
            End If
            If Interest > 0 Then
                MaturityAmount = Amount + Math.Round(Interest, 2)
            Else
                MaturityAmount = 0
            End If
            TxtmaturityAmount.Value = MaturityAmount
        End If
    End Sub

    Private Sub fndGLAccount__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndGLAccount._MYValidating
        Try
            Dim Qry As String = ""
            Qry = "Select Account_Code as Code ,Description from TSPL_GL_ACCOUNTS  "
            fndGLAccount.Value = clsCommon.ShowSelectForm("GLAccount", Qry, "Code", " ControlAccount ='N'", fndGLAccount.Value, "", isButtonClicked)
            Lblglaccount.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS where Account_Code ='" + fndGLAccount.Value + "' ")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Sub PostData()
        Try

            isFlag = True
            If (myMessages.postConfirm()) Then
                If clsCommon.myLen(FndLCRequestNo.Value) <= 0 Then
                    Throw New Exception("FD cannot be posted without LC Request No.")
                End If
                SaveData()

                If (ClsFixedDeposit.PostData(MyBase.Form_ID, fndFixedDepositcode.Value)) Then

                    common.clsCommon.MyMessageBoxShow("Successfully posted")
                    LoadData(fndFixedDepositcode.Value, NavigatorType.Current)
                End If
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isFlag = False
        End Try
    End Sub

End Class
