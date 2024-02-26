'Works on keyDown Event that all shortcut keys are working while buttons are diabled --- by Dipti Waila (26/10/2012)
' by vipin for posting status on update on 04/02/2013
'--Preeti Gupta--Ticket No.-BM00000003015-1/7/2014-- ,BM00000003244,BHA/28/02/19-000826
'Ticket No- BHA/29/09/18-000580 Show only Active Bank Account in finder
Imports Telerik.WinControls.UI
Imports Telerik.Collections
Imports System.Data.SqlClient
Imports System.Data
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Text.RegularExpressions
Imports System.Globalization
Imports System.Threading
Imports common
Imports System


Public Class FrmBankTransfer

    Inherits FrmMainTranScreen
    Public strbankTrans As String
    Dim userCode, companyCode As String
    Dim dr As DataTable
    Dim ds As DataSet
    Dim btntooltip As ToolTip = New ToolTip()
    Public clicked As Boolean = False
    Public IsLoadTransType As Boolean = False
    Dim InTransitSettings As String
    Dim isFlag As Boolean = False
    Dim isLoad As Boolean = True
    Public arrMCC As ArrayList
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.bankTransfer)
        '--Preeti Gupta--Ticket No-[BM00000003166]
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btn_save.Visible = MyBase.isModifyFlag
        btn_post.Visible = MyBase.isPostFlag
        btn_delete.Visible = MyBase.isDeleteFlag
        btnPrint.Visible = MyBase.isPrintFlag
        If MyBase.isReverse Then
            btnReverseAndRecreate.Enabled = True
        Else
            btnReverseAndRecreate.Enabled = False

        End If
        If btn_save.Visible = True Then
            rmiIMport.Enabled = True
            rmiExport.Enabled = True
        Else
            rmiIMport.Enabled = False
            rmiExport.Enabled = False
        End If
    End Sub
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub
    Public Sub FrmBankTransfer_vb1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        SetLength()
        txtchkno.Enabled = False
        txtchkdate.Enabled = False
        Lblchkno.Enabled = False
        IsLoadTransType = False
        globalFunc.mandatoryText(txt_frombankname, txt_transferamount, txt_tobankname, txt_depositamount)
        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-GB")
        Dim cd As GridViewMultiComboBoxColumn = TryCast(MasterTemplate.Columns(1), GridViewMultiComboBoxColumn)
        ds = connectSql.RunSQLReturnDS("select Account_Code as [Account Code],Description from TSPL_GL_ACCOUNTS ")
        cd.DataSource = ds.Tables(0)
        cd.ValueMember = "Account Code"
        cd.DisplayMember = "Account Code"
        'AddHandler Txt_frombankCode.TextChanged, AddressOf TextChanged1
        'AddHandler Txt_toBankCode.TextChanged, AddressOf TextChanged2
        'AddHandler fnd_transfernumber.txtValue.TextChanged, AddressOf TextChanged3
        'AddHandler fnd_transfernumber.txtValue.KeyPress, AddressOf KeyPress1
        'fnd_transfernumber.txtValue.MaxLength = 30
        'Txt_frombankCode.My_Validating()
        'fnd_tobankcode.txtValue.MaxLength = 12
        txt_frombankname.Enabled = False
        txt_frombankaccount.Enabled = False
        txt_tobankname.Enabled = False
        txt_tobankaccount.Enabled = False
        txt_transferamount.Text = "0"
        txt_depositamount.Text = "0"
        txt_depositamount.Enabled = False
        btn_delete.Enabled = False
        txtbnkaccnumber.Enabled = False
        dtp_transferpostingdate.Value = clsCommon.GETSERVERDATE()
        txtchkdate.Value = clsCommon.GETSERVERDATE()
        txt_depositamount.Text = "0.00"
        txt_transferamount.Text = ""
        txtBankChargesAmt.Value = 0
        'grd_serviceCharge.Columns(0).r()
        'For i As Integer = 0 To 1
        'grd_serviceCharge.Rows(0).Cells(3).Value = 0
        'Next
        '' Anubhooti 17-Dec-2014 BM00000004959
        LoadTransactionType()

        InTransitSettings = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.InTransitFeatureIsRequired, clsFixedParameterCode.InTransitFeatureIsRequired, Nothing))
        If clsCommon.CompairString(InTransitSettings, "1") = CompairStringResult.Equal Then
            CmbTransType.SelectedValue = "W"
            lbltranstype.Visible = True
            CmbTransType.Visible = True
            LblWithdrawal.Visible = False
            txtwithdrawal.Visible = False
        Else
            CmbTransType.SelectedValue = ""
            lbltranstype.Visible = False
            CmbTransType.Visible = False
            LblWithdrawal.Visible = False
            txtwithdrawal.Visible = False
        End If
        ''

        For i As Integer = 0 To 1
            MasterTemplate.Rows.AddNew()
            MasterTemplate.AllowAddNewRow = False
            MasterTemplate.Rows(i).Cells(3).Value = 0.0
        Next
        ' Edited by Abhishek
        btntooltip.SetToolTip(btn_save, "Press Alt+S for Save/Update Trasnaction")
        btntooltip.SetToolTip(btn_post, "Press Alt+P Post Trasnaction")
        btntooltip.SetToolTip(btn_delete, "Press Alt+D Delete Trasnaction")
        btntooltip.SetToolTip(btn_close, "Press Esc Close the Window")
        btntooltip.SetToolTip(btn_reset, "Press Alt+N Adding New Transaction")
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        btn_post.Enabled = False
        gbAgainstMilkBill.Visible = IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowBankTransferAgainstMilkBill, clsFixedParameterCode.AllowBankTransferAgainstMilkBill, Nothing)) = "1", True, False)
        If clsCommon.myLen(strbankTrans) > 0 Then
            Fnd_Transfernumber.Value = strbankTrans
            funfill()
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            resetdata()
            Fnd_Transfernumber.Value = clsCommon.myCstr(Me.Tag)
            funfill()
        Else
            resetdata()
        End If
    End Sub

    'Private Sub fnd_frombankcode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fnd_frombankcode.Load
    '    fnd_frombankcode.ConnectionString = connectSql.SqlCon()
    '    fnd_frombankcode.Query = "select [Bank_Code] as [Bank Code],Description from TSPL_BANK_MASTER"
    '    fnd_frombankcode.ValueToSelect = "Bank Code"
    '    fnd_frombankcode.Caption = "Bank Code"
    '    fnd_frombankcode.ValueToSelect1 = "Bank Code"
    'End Sub

    'Private Sub fnd_tobankcode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fnd_tobankcode.ConnectionString = connectSql.SqlCon()
    '    fnd_tobankcode.Query = "select [Bank_Code] as [Bank Code],[description] as [Description] from TSPL_BANK_MASTER"
    '    fnd_tobankcode.ValueToSelect = "Bank Code"
    '    fnd_tobankcode.Caption = "Bank Code"
    '    fnd_tobankcode.ValueToSelect1 = "Bank Code"
    'End Sub
    'Private Sub TextChanged1(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        dr = connectSql.RunSqlReturnDR("select Bank_Code from TSPL_BANK_MASTER where [Bank_Code] ='" + Txt_frombankCode.Value + "'")
    '        Dim s As String
    '        While dr.Read()
    '            s = dr(0).ToString()
    '        End While
    '        If s <> "" Then
    '            funFill1()
    '        Else
    '            txt_frombankname.Text = ""
    '            txt_frombankaccount.Text = ""
    '            txt_transferamount.Text = ""
    '            btn_save.Enabled = True
    '            btn_save.Text = "Save"
    '        End If
    '    Catch ex As Exception
    '        myMessages.myExceptions(ex)
    '    End Try

    'End Sub
    'Private Sub TextChanged2(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        dr = connectSql.RunSqlReturnDR("select Bank_Code from TSPL_BANK_MASTER where [Bank_Code] ='" + Txt_toBankCode.Value + "'")
    '        Dim s As String
    '        While dr.Read()
    '            s = dr(0).ToString()
    '        End While
    '        If s <> "" Then
    '            funFill2()
    '        Else
    '            txt_tobankname.Text = ""
    '            txt_tobankaccount.Text = ""
    '            txt_depositamount.Text = ""
    '            btn_save.Enabled = True
    '            btn_save.Text = "Save"
    '        End If
    '    Catch ex As Exception
    '        myMessages.myExceptions(ex)
    '    End Try

    'End Sub
    Public Sub SetLength()
        Fnd_Transfernumber.MyMaxLength = 30
        txt_description.MaxLength = 100
        txt_references.MaxLength = 100
        txt_frombankaccount.MaxLength = 50
        txt_frombankname.MaxLength = 60
        txt_tobankaccount.MaxLength = 50
        txt_tobankname.MaxLength = 60
        txt_transferamount.MaxLength = 18
        txtchkno.MaxLength = 12

    End Sub
    Public Sub funFill1()
        Try
            If Txt_frombankCode.Value <> "" Then
                Dim strSql As String = "select Bank_code,description,bankacc,BANKACCNUMBER from TSPL_BANK_MASTER where Bank_Code='" + Txt_frombankCode.Value + "'"
                'dr = connectSql.RunSqlReturnDR("select Bank_code,description,bankacc from TSPL_BANK_MASTER where Bank_Code='" + Txt_frombankCode.Value + "'")
                'While dr.Read()

                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(strSql)
                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    For Each dr As DataRow In dt1.Rows
                        txt_frombankname.Text = dr(1).ToString()
                        txt_frombankaccount.Text = dr(2).ToString()
                        txtbnkaccnumber.Text = dr(3).ToString()
                    Next
                End If
                MasterTemplate.Rows(0).Cells(0).Value = Txt_frombankCode.Value
                MasterTemplate.AllowAddNewRow = False
                btn_save.Enabled = True
            Else
                btn_save.Enabled = True
                btn_save.Text = "Save"
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Public Sub funFill2()
        Try
            If Txt_toBankCode.Value <> "" Then
                'dr = connectSql.RunSqlReturnDR("select Bank_code,description,bankacc from TSPL_BANK_MASTER where Bank_Code='" + Txt_toBankCode.Value + "'")
                'While dr.Read()
                Dim strSql As String = "select Bank_code,description,bankacc,BANKACCNUMBER from TSPL_BANK_MASTER where Bank_Code='" + Txt_toBankCode.Value + "'"
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(strSql)
                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    For Each dr As DataRow In dt1.Rows
                        txt_tobankname.Text = dr(1).ToString()
                        txt_tobankaccount.Text = dr(2).ToString()
                        txttranbnkaccno.Text = dr(3).ToString()
                        'If grd_serviceCharge.Rows.Count <= 0 Then
                        '    Dim dgv As GridViewRowInfo = grd_serviceCharge.Rows.AddNew()
                        '    grd_serviceCharge.Rows(1).Cells(0).Value = fnd_tobankcode.txtValue.Text
                        'Else
                        '    grd_serviceCharge.Rows(1).Cells(0).Value = fnd_tobankcode.txtValue.Text
                        'End If
                    Next
                End If
                MasterTemplate.Rows(1).Cells(0).Value = Txt_toBankCode.Value
                MasterTemplate.AllowAddNewRow = False

                btn_save.Enabled = True
            Else
                btn_save.Enabled = True
                btn_save.Text = "Save"
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    'Private Sub grd_serviceCharge_EditorRequired(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.EditorRequiredEventArgs)
    '    grd_serviceCharge.CurrentRow.Cells(2).Value = connectSql.RunScalar("select Description  from TSPL_GL_ACCOUNTS  where Account_Code ='" + grd_serviceCharge.CurrentRow.Cells(1).Value + "'")
    'End Sub

    Private Sub MasterTemplate_EditorRequired(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.EditorRequiredEventArgs) Handles MasterTemplate.EditorRequired
        MasterTemplate.CurrentRow.Cells(2).Value = connectSql.RunScalar("select Description  from TSPL_GL_ACCOUNTS  where Account_Code ='" + MasterTemplate.CurrentRow.Cells(1).Value + "'")
    End Sub

    Private Sub btn_close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_close.Click
        closeform()
    End Sub
    Public Sub closeform()
        Me.Close()
    End Sub

    Private Sub btn_save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_save.Click
        savedata(clicked)
    End Sub
    Public Function SaveAndReturnDocNo(ByVal clicked As Boolean, Optional ByVal trans As SqlTransaction = Nothing) As String
        savedata(clicked, trans)
        Return clsCommon.myCstr(Fnd_Transfernumber.Value)
    End Function
    Public Sub savedata(ByVal clicked As Boolean, Optional ByVal trans As SqlTransaction = Nothing)
        '--30/11/2012--Added By--Pankaj Kumar----For Validate Location And Doc Date by Transaction Lock---------------
        Try
            'Dim InTransitSettings As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.InTransitFeatureIsRequired, clsFixedParameterCode.InTransitFeatureIsRequired, trans))

            If btn_save.Text = "Update" Then
                Dim strchk As String = "select Post from TSPL_BANK_TRANSFER where Transfer_No='" + Fnd_Transfernumber.Value + "'"
                Dim chkpost As String = clsDBFuncationality.getSingleValue(strchk, trans)
                If chkpost = "p" Then
                    clsCommon.MyMessageBoxShow(Me, "Transaction already posted", Me.Text)
                    Exit Sub
                End If
            End If


            If clsCommon.myLen(Txt_frombankCode.Value) > 0 Then
                Dim LocSegmentCode As String = clsDBFuncationality.getSingleValue("Select RIGHT(BANKACC, 3) from TSPL_BANK_MASTER  Where BANK_CODE='" + Txt_frombankCode.Value + "'", trans)
                clsERPFuncationality.ValidateLocationSegment(objCommonVar.CurrentCompanyCode, "Common Services", "Bank Transfer", LocSegmentCode, dtp_transferpostingdate.Value, trans)
            End If
            '--------------------------------------------------------------------------------------------------------------
            '' Anubhooti 18-Dec-2014 BM00000004959
            If clsCommon.CompairString(InTransitSettings, "1") = CompairStringResult.Equal AndAlso CmbTransType.Visible = True Then
                If clsCommon.CompairString(CmbTransType.SelectedValue, "R") = CompairStringResult.Equal AndAlso txtwithdrawal.Visible = True Then
                    If clsCommon.myLen(txtwithdrawal.Value) <= 0 Then
                        common.clsCommon.MyMessageBoxShow(Me, "Please check withdrawal no.can not be left blank.", Me.Text)
                        txtwithdrawal.Focus()
                        Exit Sub
                    End If
                End If
            End If
            ''

            If Txt_frombankCode.Value = "" Then
                myMessages.blankValue("Bank Code")
                Txt_frombankCode.Focus()
                Exit Sub
            ElseIf txt_frombankname.Text = "" Then
                myMessages.blankValue("Bank Name")
                txt_frombankname.Focus()
                Exit Sub
            ElseIf txt_transferamount.Text = "" Then
                myMessages.blankValue("Transfer Amount")
                txt_transferamount.Focus()
                Exit Sub
            ElseIf Txt_toBankCode.Value = "" Then
                myMessages.blankValue("Bank Code")
                Txt_toBankCode.Focus()
                Exit Sub
            ElseIf txt_tobankname.Text = "" Then
                myMessages.blankValue("Bank Name")
                txt_tobankname.Focus()
                Exit Sub
            ElseIf txt_depositamount.Text = "" Then
                myMessages.blankValue("Deposit Amount")
                txt_depositamount.Focus()
                Exit Sub
            ElseIf txtchkno.Text = "" And chkCheckPrint.Checked = False Then
                '--10/12/2012--Added  By__Pankaj Kumar-----While addding Finder(Payment Mode)---By-Amit Sir-----
                Dim strcheckcode As String = connectSql.RunScalar(trans, "select Payment_Type  from TSPL_PAYMENT_CODE  where Payment_Code ='" + fndPayType.Value + "'")
                If Not String.IsNullOrEmpty(strcheckcode) Then
                    If strcheckcode.Trim() = "Cheque" Then
                        common.clsCommon.MyMessageBoxShow(Me, "Please enter a valid cheque number", Me.Text)
                        txtchkno.Focus()
                        Exit Sub
                    End If
                End If

            ElseIf Not clsCommon.CompairString(CmbTransType.SelectedValue, "R") = CompairStringResult.Equal AndAlso txtchkno.Text <> "" Then
                Dim var As String = "Select Transfer_No  from TSPL_BANK_TRANSFER where Cheque_No='" + txtchkno.Text + "' and Transfer_No not in ('" + Fnd_Transfernumber.Value + "') "
                Dim trans_no As String = clsDBFuncationality.getSingleValue(var, trans)
                If clsCommon.myLen(trans_no) > 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "The cheque Number '" + txtchkno.Text + "' is already exist against '" + trans_no + "',please enter a valid cheque no.")
                    txtchkno.Focus()
                    Exit Sub
                End If

            End If
            '' UDL/08/03/19-000276 RICHA 
            If AllowFutureDateTransaction(dtp_transferpostingdate.Value, Nothing) = False Then
                dtp_transferpostingdate.Focus()
                Exit Sub
            End If

            '' Anubhooti 17-Dec-2014 BM00000004959 (From bank loc and To bank loc can not be same for W/R)

            'If clsCommon.CompairString(InTransitSettings, "1") = CompairStringResult.Equal AndAlso CmbTransType.Visible = True Then
            '    If clsCommon.myLen(Txt_frombankCode.Value) > 0 AndAlso clsCommon.myLen(Txt_toBankCode.Value) > 0 Then
            '        Dim FromBankLocSeg As String = clsDBFuncationality.getSingleValue("Select RIGHT(BANKACC, 3) from TSPL_BANK_MASTER  Where BANK_CODE='" + Txt_frombankCode.Value + "'", trans)
            '        Dim ToBankLocSeg As String = clsDBFuncationality.getSingleValue("Select RIGHT(BANKACC, 3) from TSPL_BANK_MASTER  Where BANK_CODE='" + Txt_toBankCode.Value + "'", trans)
            '        If clsCommon.CompairString(CmbTransType.SelectedValue, "W") = CompairStringResult.Equal OrElse clsCommon.CompairString(CmbTransType.SelectedValue, "R") = CompairStringResult.Equal Then
            '            If clsCommon.CompairString(FromBankLocSeg, ToBankLocSeg) = CompairStringResult.Equal Then
            '                common.clsCommon.MyMessageBoxShow("From bank location (" & FromBankLocSeg & ") and To bank location (" & ToBankLocSeg & ") can not be same in case of - " & CmbTransType.Text & "")
            '                Txt_frombankCode.Focus()
            '                Exit Sub
            '            End If
            '            'ElseIf clsCommon.CompairString(CmbTransType.SelectedValue, "B") = CompairStringResult.Equal Then
            '            '    ''RICHA AGARWAL SWA/03/12/18-000061
            '            '    Dim isApplyBrachAccounting As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyBrachAccounting, clsFixedParameterCode.ApplyBrachAccounting, trans)) = 1, True, False)
            '            '    If isApplyBrachAccounting = False AndAlso clsCommon.CompairString(FromBankLocSeg, ToBankLocSeg) <> CompairStringResult.Equal Then
            '            '        common.clsCommon.MyMessageBoxShow("From bank location (" & FromBankLocSeg & ") and To bank location (" & ToBankLocSeg & ") should be same in case of - " & CmbTransType.Text & "")
            '            '        Txt_frombankCode.Focus()
            '            '        Exit Sub
            '            '    End If
            '        End If
            '    End If
            'End If
            ''richa agarwal 3 Aug,2016 to check balance amount of bank
            If CheckNegativeBankBalance() Then
                If btn_save.Text = "Save" Then
                    savebutton(trans)
                ElseIf btn_save.Text = "Update" Then
                    updatebutton(trans)
                End If
            End If

            ''
           
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Function CheckNegativeBankBalanceonDelete() As Boolean
        If Not isFlag Then
            Dim Bank_Code As String = ""
            Dim Bank_GL_Location As String = ""
            If clsCommon.CompairString(CmbTransType.SelectedValue, "R") = CompairStringResult.Equal Or clsCommon.CompairString(CmbTransType.SelectedValue, "B") = CompairStringResult.Equal Then
                Bank_Code = clsCommon.myCstr(Txt_toBankCode.Value)
                Bank_GL_Location = clsGLAccount.Get_Location_Segment(txt_tobankaccount.Text, Nothing)
                'ElseIf clsCommon.CompairString(CmbTransType.SelectedValue, "R") = CompairStringResult.Equal Then
                '    'Bank_Code = obj.To_Bank_Code
                '    'Bank_GL_Location = clsGLAccount.Get_Location_Segment(obj.To_Bank_Acc_No, trans)
                '    Return True
            End If

            Dim Bank_Type_Check As String = "0"
            Bank_Type_Check = clsFixedParameter.GetData(clsFixedParameterType.StopNegativeBankBalance, clsFixedParameterCode.StopNegativeBankBalance, Nothing)

            Dim Bank_Type As String = clsBankMaster.GetBankType(Bank_Code, Nothing)
            Dim Bank_Balance As Double = 0
            If clsCommon.CompairString(Bank_Type_Check, "0") = CompairStringResult.Equal Then
                '' allow for all
            ElseIf clsCommon.CompairString(Bank_Type_Check, "1") = CompairStringResult.Equal Then
                If clsCommon.CompairString(Bank_Type, "C") = CompairStringResult.Equal OrElse clsCommon.CompairString(Bank_Type, "P") = CompairStringResult.Equal Then
                    Bank_Balance = clsPaymentHeader.GetBankBalance(Fnd_Transfernumber.Value, dtp_transferpostingdate.Value, Bank_Code, Bank_GL_Location, Nothing, True)
                    If Bank_Balance < clsCommon.myCdbl(txt_transferamount.Text) Then
                        Throw New Exception("Payment Amount : " & clsCommon.myCdbl(txt_transferamount.Text) & " Available Bank Balance(" & Bank_GL_Location & ") : " & Bank_Balance & "")
                    End If
                End If
            ElseIf clsCommon.CompairString(Bank_Type_Check, "2") = CompairStringResult.Equal Then
                If clsCommon.CompairString(Bank_Type, "B") = CompairStringResult.Equal Then
                    Bank_Balance = clsPaymentHeader.GetBankBalance(Fnd_Transfernumber.Value, dtp_transferpostingdate.Value, Bank_Code, Bank_GL_Location, Nothing, True)
                    If Bank_Balance < clsCommon.myCdbl(txt_transferamount.Text) Then
                        Throw New Exception("Payment Amount : " & clsCommon.myCdbl(txt_transferamount.Text) & " Available Bank Balance(" & Bank_GL_Location & ") : " & Bank_Balance & "")
                    End If
                End If
            ElseIf clsCommon.CompairString(Bank_Type_Check, "3") = CompairStringResult.Equal Then
                Bank_Balance = clsPaymentHeader.GetBankBalance(Fnd_Transfernumber.Value, dtp_transferpostingdate.Value, Bank_Code, Bank_GL_Location, Nothing, True)
                If Bank_Balance < clsCommon.myCdbl(txt_transferamount.Text) Then
                    Throw New Exception("Payment Amount : " & clsCommon.myCdbl(txt_transferamount.Text) & " Available Bank Balance(" & Bank_GL_Location & ") : " & Bank_Balance & "")
                End If
            End If
        End If
        Return True
    End Function

    Public Function CheckNegativeBankBalance() As Boolean
        If Not isFlag Then
            Dim Bank_Code As String = ""
            Dim Bank_GL_Location As String = ""
            If clsCommon.CompairString(CmbTransType.SelectedValue, "W") = CompairStringResult.Equal Or clsCommon.CompairString(CmbTransType.SelectedValue, "B") = CompairStringResult.Equal Then
                Bank_Code = clsCommon.myCstr(Txt_frombankCode.Value)
                Bank_GL_Location = clsGLAccount.Get_Location_Segment(txt_frombankaccount.Text, Nothing)
            ElseIf clsCommon.CompairString(CmbTransType.SelectedValue, "R") = CompairStringResult.Equal Then
                'Bank_Code = obj.To_Bank_Code
                'Bank_GL_Location = clsGLAccount.Get_Location_Segment(obj.To_Bank_Acc_No, trans)
                Return True
            End If

            Dim Bank_Type_Check As String = "0"
            Bank_Type_Check = clsFixedParameter.GetData(clsFixedParameterType.StopNegativeBankBalance, clsFixedParameterCode.StopNegativeBankBalance, Nothing)

            Dim Bank_Type As String = clsBankMaster.GetBankType(Bank_Code, Nothing)
            Dim Bank_Balance As Double = 0
            If clsCommon.CompairString(Bank_Type_Check, "0") = CompairStringResult.Equal Then
                '' allow for all
            ElseIf clsCommon.CompairString(Bank_Type_Check, "1") = CompairStringResult.Equal Then
                If clsCommon.CompairString(Bank_Type, "C") = CompairStringResult.Equal OrElse clsCommon.CompairString(Bank_Type, "P") = CompairStringResult.Equal Then
                    Bank_Balance = clsPaymentHeader.GetBankBalance(Fnd_Transfernumber.Value, dtp_transferpostingdate.Value, Bank_Code, Bank_GL_Location, Nothing)
                    If Bank_Balance < clsCommon.myCdbl(txt_transferamount.Text) Then
                        Throw New Exception("Payment Amount : " & clsCommon.myCdbl(txt_transferamount.Text) & " Available Bank Balance(" & Bank_GL_Location & ") : " & Bank_Balance & "")
                    End If
                End If
            ElseIf clsCommon.CompairString(Bank_Type_Check, "2") = CompairStringResult.Equal Then
                If clsCommon.CompairString(Bank_Type, "B") = CompairStringResult.Equal Then
                    Bank_Balance = clsPaymentHeader.GetBankBalance(Fnd_Transfernumber.Value, dtp_transferpostingdate.Value, Bank_Code, Bank_GL_Location, Nothing)
                    If Bank_Balance < clsCommon.myCdbl(txt_transferamount.Text) Then
                        Throw New Exception("Payment Amount : " & clsCommon.myCdbl(txt_transferamount.Text) & " Available Bank Balance(" & Bank_GL_Location & ") : " & Bank_Balance & "")
                    End If
                End If
            ElseIf clsCommon.CompairString(Bank_Type_Check, "3") = CompairStringResult.Equal Then
                Bank_Balance = clsPaymentHeader.GetBankBalance(Fnd_Transfernumber.Value, dtp_transferpostingdate.Value, Bank_Code, Bank_GL_Location, Nothing)
                If Bank_Balance < clsCommon.myCdbl(txt_transferamount.Text) Then
                    Throw New Exception("Payment Amount : " & clsCommon.myCdbl(txt_transferamount.Text) & " Available Bank Balance(" & Bank_GL_Location & ") : " & Bank_Balance & "")
                End If
            End If
        End If
        Return True
    End Function
    Private Sub savebutton(Optional ByVal trans As SqlTransaction = Nothing)
        If Txt_frombankCode.Value = Txt_toBankCode.Value Then
            common.clsCommon.MyMessageBoxShow(Me, "From bank code and To bank code should not be same", Me.Text)
            Exit Sub
        End If
        Dim OuterTrans As Boolean = False
        If trans Is Nothing Then
            trans = clsDBFuncationality.GetTransactin()
            OuterTrans = False
        Else
            OuterTrans = True
        End If

        Try
            Dim STR As String = ""
            Dim strBank As String = Txt_frombankCode.Value
            If clsCommon.CompairString(clsCommon.myCstr(CmbTransType.SelectedValue), "R") = CompairStringResult.Equal Then
                strBank = Txt_toBankCode.Value
            End If

            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.BankTransferRunPaymentCounter, clsFixedParameterCode.BankTransferRunPaymentCounter, trans)) = 1 Then
                Dim qry As String = "select Bank_type,BANKACC from TSPL_BANK_MASTER where BANK_CODE='" + strBank + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                Dim BankType As String = clsCommon.myCstr(dt.Rows(0)("Bank_type"))
                Dim BankAcc As String = clsCommon.myCstr(dt.Rows(0)("BANKACC"))
                If (BankAcc.Length >= 3) Then
                    BankAcc = BankAcc.Substring(BankAcc.Length - 3, 3)
                    If (IsNumeric(BankAcc)) Then
                        Throw New Exception("Bank Master's Bank Account should be have location segment Type")
                    End If
                Else
                    Throw New Exception("Bank Master's Bank Account should be have location segment Type")
                End If

                If clsCommon.CompairString(BankType, "B") = CompairStringResult.Equal Then
                    STR = clsERPFuncationality.GetNextCode(trans, dtp_transferpostingdate.Value, clsDocType.Payment, clsDocTransactionType.Bank, BankAcc, True)
                ElseIf clsCommon.CompairString(BankType, "C") = CompairStringResult.Equal Then
                    STR = clsERPFuncationality.GetNextCode(trans, dtp_transferpostingdate.Value, clsDocType.Payment, clsDocTransactionType.Cash, BankAcc, True)
                ElseIf clsCommon.CompairString(BankType, "P") = CompairStringResult.Equal Then
                    STR = clsERPFuncationality.GetNextCode(trans, dtp_transferpostingdate.Value, clsDocType.Payment, clsDocTransactionType.PettyCash, BankAcc, True)
                ElseIf clsCommon.CompairString(BankType, "O") = CompairStringResult.Equal Then
                    STR = clsERPFuncationality.GetNextCode(trans, dtp_transferpostingdate.Value, clsDocType.Payment, clsDocTransactionType.Others, BankAcc, True)
                ElseIf clsCommon.CompairString(BankType, "S") = CompairStringResult.Equal Then
                    STR = clsERPFuncationality.GetNextCode(trans, dtp_transferpostingdate.Value, clsDocType.Payment, clsDocTransactionType.Others, BankAcc, True)
                Else
                    Throw New Exception("Plase set the Bank Type for Bank SETTLEMENT")
                End If
            Else
                Dim LocSegmentCode As String = clsDBFuncationality.getSingleValue("Select RIGHT(BANKACC, 3) from TSPL_BANK_MASTER  Where BANK_CODE='" + strBank + "'", trans)
                STR = clsERPFuncationality.GetNextCode(trans, dtp_transferpostingdate.Value, clsDocType.ContraVoucher, "", LocSegmentCode, True)
                ' STR = funautogenerateno(trans)
            End If
            If clsCommon.myLen(STR) <= 0 Then
                Throw New Exception("Error in code generation")
            End If

            'Dim tpd As String = Format(dtp_transferpostingdate.Value.ToString(), "dd/MM/yyyy")
            'Dim STR As String = funautogenerateno()
            Dim PostingDate As String = clsCommon.GetPrintDate(dtp_transferpostingdate.Value, "dd/MMM/yyyy")
            connectSql.RunSpTransaction(trans, "sp_tspl_banktransfer_insert", New SqlParameter("@Transfer_No", STR), New SqlParameter("@Transfer_Date", PostingDate), New SqlParameter("@Transfer_Posting_Date", PostingDate), New SqlParameter("@Description", txt_description.Text), New SqlParameter("@Reference", txt_references.Text), New SqlParameter("@TransType", CmbTransType.SelectedValue), New SqlParameter("@From_Bank_Code", Txt_frombankCode.Value), New SqlParameter("@From_Bank_Name", txt_frombankname.Text), New SqlParameter("@From_Bank_Acc_No", txt_frombankaccount.Text), New SqlParameter("@Transfer_Amount", txt_transferamount.Text), New SqlParameter("@From_Bank_GL_Acc", clsCommon.myCstr(MasterTemplate.Rows(0).Cells(1).Value)), New SqlParameter("@From_Bank_GLAcc_Desc", clsCommon.myCstr(MasterTemplate.Rows(0).Cells(2).Value)), New SqlParameter("@From_Bank_GL_Amount", MasterTemplate.Rows(0).Cells(3).Value), New SqlParameter("@To_Bank_Code", Txt_toBankCode.Value), New SqlParameter("@To_Bank_Name", txt_tobankname.Text), New SqlParameter("@To_Bank_Acc_No", txt_tobankaccount.Text), New SqlParameter("@Deposit_Amount", txt_depositamount.Text), New SqlParameter("@To_Bank_GL_Acc", MasterTemplate.Rows(1).Cells(1).Value), New SqlParameter("@To_Bank_GLAcc_Desc", MasterTemplate.Rows(1).Cells(2).Value), New SqlParameter("@To_Bank_GL_Amount", MasterTemplate.Rows(1).Cells(3).Value), New SqlParameter("@Post", "n"), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate(trans)), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@comp_code", companyCode), New SqlParameter("@Cheque_No", txtchkno.Text), New SqlParameter("@Cheque_Date", clsCommon.GetPrintDate(txtchkdate.Value, "dd/MMM/yyyy")), New SqlParameter("@Payment_Mode", clsCommon.myCstr(fndPayType.Value)), New SqlParameter("@frmbnkaccno", clsCommon.myCstr(txtbnkaccnumber.Text)), New SqlParameter("@tobnkaccno", clsCommon.myCstr(txttranbnkaccno.Text)))
            '' Anubhooti 17-Dec-2014 BM00000004959
            If clsCommon.CompairString(InTransitSettings, "1") = CompairStringResult.Equal AndAlso CmbTransType.Visible = True Then
                clsDBFuncationality.ExecuteNonQuery("UPDATE TSPL_BANK_TRANSFER SET Transaction_Type='" & clsCommon.myCstr(CmbTransType.SelectedValue) & "',Against_Withdrawal_No=NULL Where Transfer_No='" & clsCommon.myCstr(STR) & "'", trans)
                If clsCommon.CompairString(CmbTransType.SelectedValue, "R") = CompairStringResult.Equal AndAlso clsCommon.myLen(txtwithdrawal.Value) > 0 Then
                    clsDBFuncationality.ExecuteNonQuery("UPDATE TSPL_BANK_TRANSFER SET Against_Withdrawal_No='" & clsCommon.myCstr(txtwithdrawal.Value) & "' Where Transfer_No='" & clsCommon.myCstr(STR) & "'", trans)
                End If
            Else
                ''  '' Balwinder on  11-Jan-2015 change transaction_type null to 'B' because trigger not fired for bank book is null exists
                clsDBFuncationality.ExecuteNonQuery("UPDATE TSPL_BANK_TRANSFER SET Transaction_Type='B', Against_Withdrawal_No=NULL Where Transfer_No='" & clsCommon.myCstr(STR) & "'", trans)
            End If
           
            '' update check printing
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Check_Print", IIf(chkCheckPrint.Checked, 1, 0))
            '' Anubhooti 02-Apr-2015 (Added "Remitt To" Column)
            clsCommon.AddColumnsForChange(coll, "Remitt_To", clsCommon.myCstr(TxtRemittTo.Text))
            ''richa BHA/13/09/18-000545
            clsCommon.AddColumnsForChange(coll, "BankCharges", clsCommon.myCdbl(txtBankChargesAmt.Value))
            Dim strBankChargesAcc As String = Nothing
            If clsCommon.myCdbl(txtBankChargesAmt.Value) <> 0 Then
                If clsCommon.CompairString(CmbTransType.SelectedValue, "B") = CompairStringResult.Equal Or clsCommon.CompairString(CmbTransType.SelectedValue, "W") = CompairStringResult.Equal Then
                    strBankChargesAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select CREDITACC from TSPL_BANK_MASTER Where BANK_CODE='" + clsCommon.myCstr(Txt_frombankCode.Value) + "'", trans))
                End If
            End If
            clsCommon.AddColumnsForChange(coll, "Bank_Charges_Ac", strBankChargesAcc, True)
            ''-----------------
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BANK_TRANSFER", OMInsertOrUpdate.Update, "TSPL_BANK_TRANSFER.Transfer_No='" + STR + "'", trans)
            '=====================================
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_BANK_TRANSFER_MCC  where Transfer_No ='" + STR + "' ", trans)
            arrMCC = txtMCC.arrValueMember
            If (arrMCC IsNot Nothing AndAlso arrMCC.Count > 0) Then
                For Each strMCCCode As String In arrMCC
                    Dim collMCC As New Hashtable()
                    Dim strTRCode As String = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"), clsDocType.Detail, clsDocTransactionType.Detail, "")
                    clsCommon.AddColumnsForChange(collMCC, "TR_Code", strTRCode)
                    clsCommon.AddColumnsForChange(collMCC, "Transfer_No", STR)
                    clsCommon.AddColumnsForChange(collMCC, "MCC_Code", strMCCCode)
                    clsCommonFunctionality.UpdateDataTable(collMCC, "TSPL_BANK_TRANSFER_MCC", OMInsertOrUpdate.Insert, "", trans)
                Next
                clsDBFuncationality.ExecuteNonQuery("update TSPL_BANK_TRANSFER set From_Date = '" + clsCommon.myCstr(clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy")) + "' , To_Date = '" + clsCommon.myCstr(clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy")) + "' where Transfer_No = '" + STR + "'", trans)
            End If
            '=====================================

            If OuterTrans = False Then
                trans.Commit()
            End If

            If clicked = False Then
                myMessages.insert()
            End If

            Fnd_Transfernumber.Value = STR
            funfill()
            btn_save.Text = "Update"

            Fnd_Transfernumber.MyReadOnly = True
            btn_delete.Enabled = True
            CmbTransType.Enabled = False
        Catch ex As Exception
            If OuterTrans = False Then
                trans.Rollback()
            End If
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub updatebutton(Optional ByVal trans As SqlTransaction = Nothing)
        Try
            'Dim tpd As String = Format(dtp_transferpostingdate.Value, "dd/MM/yyyy")

            Dim strCreatedBy As String = userCode
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
            strCreatedBy = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Created_By  from TSPL_BANK_TRANSFER  where Transfer_No ='" + Fnd_Transfernumber.Value + "'"))
            If clsCommon.myLen(strCreatedBy) <= 0 Then
                    strCreatedBy = userCode
                End If
            End If

            'Dim strCratedDate As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select convert(varchar, Created_Date,103)  from TSPL_BANK_TRANSFER  where Transfer_No ='" + Fnd_Transfernumber.Value + "'"))
            'strCratedDate = clsCommon.myCstr(clsCommon.GetPrintDate(strCratedDate, "dd/MMM/yyyy"))
            'If clsCommon.myLen(strCratedDate) <= 0 Or IsDBNull(strCratedDate) = True Then

            'End If
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, Fnd_Transfernumber.Value, "TSPL_BANK_TRANSFER", "Transfer_No", Nothing)
            Dim PostingDate As String = clsCommon.GetPrintDate(dtp_transferpostingdate.Value, "dd/MMM/yyyy")
            connectSql.RunSpTransaction(trans, "sp_tspl_banktransfer_update", New SqlParameter("@Transfer_No", Fnd_Transfernumber.Value), New SqlParameter("@Transfer_Date", PostingDate), New SqlParameter("@Transfer_Posting_Date", PostingDate), New SqlParameter("@Description", txt_description.Text), New SqlParameter("@Reference", txt_references.Text), New SqlParameter("@TransType", CmbTransType.SelectedValue), New SqlParameter("@From_Bank_Code", Txt_frombankCode.Value), New SqlParameter("@From_Bank_Name", txt_frombankname.Text), New SqlParameter("@From_Bank_Acc_No", txt_frombankaccount.Text), New SqlParameter("@Transfer_Amount", txt_transferamount.Text), New SqlParameter("@From_Bank_GL_Acc", clsCommon.myCstr(MasterTemplate.Rows(0).Cells(1).Value)), New SqlParameter("@From_Bank_GLAcc_Desc", clsCommon.myCstr(MasterTemplate.Rows(0).Cells(2).Value)), New SqlParameter("@From_Bank_GL_Amount", MasterTemplate.Rows(0).Cells(3).Value), New SqlParameter("@To_Bank_Code", Txt_toBankCode.Value), New SqlParameter("@To_Bank_Name", txt_tobankname.Text), New SqlParameter("@To_Bank_Acc_No", txt_tobankaccount.Text), New SqlParameter("@Deposit_Amount", txt_depositamount.Text), New SqlParameter("@To_Bank_GL_Acc", MasterTemplate.Rows(1).Cells(1).Value), New SqlParameter("@To_Bank_GLAcc_Desc", MasterTemplate.Rows(1).Cells(2).Value), New SqlParameter("@To_Bank_GL_Amount", MasterTemplate.Rows(1).Cells(3).Value), New SqlParameter("@Post", "n"), New SqlParameter("@Created_By", strCreatedBy), New SqlParameter("@Created_Date", clsCommon.GETSERVERDATE(trans)), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", clsCommon.GETSERVERDATE(trans)), New SqlParameter("@comp_code", companyCode), New SqlParameter("@Cheque_No", txtchkno.Text), New SqlParameter("@Cheque_Date", clsCommon.GetPrintDate(txtchkdate.Value, "dd/MMM/yyyy")), New SqlParameter("@Payment_Mode", clsCommon.myCstr(fndPayType.Value)), New SqlParameter("@frmbnkaccno", clsCommon.myCstr(txtbnkaccnumber.Text)), New SqlParameter("@tobnkaccno", clsCommon.myCstr(txttranbnkaccno.Text)))
            '' Anubhooti 17-Dec-2014 BM00000004959
            If clsCommon.CompairString(InTransitSettings, "1") = CompairStringResult.Equal AndAlso CmbTransType.Visible = True Then
                clsDBFuncationality.ExecuteNonQuery("UPDATE TSPL_BANK_TRANSFER SET Transaction_Type='" & clsCommon.myCstr(CmbTransType.SelectedValue) & "',Against_Withdrawal_No=NULL Where Transfer_No='" & clsCommon.myCstr(Fnd_Transfernumber.Value) & "'", trans)
                If clsCommon.CompairString(CmbTransType.SelectedValue, "R") = CompairStringResult.Equal AndAlso clsCommon.myLen(txtwithdrawal.Value) > 0 Then
                    clsDBFuncationality.ExecuteNonQuery("UPDATE TSPL_BANK_TRANSFER SET Against_Withdrawal_No='" & clsCommon.myCstr(txtwithdrawal.Value) & "' Where Transfer_No='" & clsCommon.myCstr(Fnd_Transfernumber.Value) & "'", trans)
                End If
            Else
                ''  '' Balwinder on  11-Jan-2015 change transaction_type null to 'B' because trigger not fired for bank book is null exists
                clsDBFuncationality.ExecuteNonQuery("UPDATE TSPL_BANK_TRANSFER SET Transaction_Type='B', Against_Withdrawal_No=NULL Where Transfer_No='" & clsCommon.myCstr(Fnd_Transfernumber.Value) & "'", trans)
            End If
            '' update check printing
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Check_Print", IIf(chkCheckPrint.Checked, 1, 0))
            '' Anubhooti 02-Apr-2015 (Added "Remitt To" Column)
            clsCommon.AddColumnsForChange(coll, "Remitt_To", clsCommon.myCstr(TxtRemittTo.Text))
            ''richa BHA/13/09/18-000545
            clsCommon.AddColumnsForChange(coll, "BankCharges", clsCommon.myCdbl(txtBankChargesAmt.Value))
            Dim strBankChargesAcc As String = Nothing
            If clsCommon.myCdbl(txtBankChargesAmt.Value) <> 0 Then
                If clsCommon.CompairString(CmbTransType.SelectedValue, "B") = CompairStringResult.Equal Or clsCommon.CompairString(CmbTransType.SelectedValue, "W") = CompairStringResult.Equal Then
                    strBankChargesAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select CREDITACC from TSPL_BANK_MASTER Where BANK_CODE='" + clsCommon.myCstr(Txt_frombankCode.Value) + "'", trans))
                End If
            End If
            clsCommon.AddColumnsForChange(coll, "Bank_Charges_Ac", strBankChargesAcc, True)
            ''-----------------
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BANK_TRANSFER", OMInsertOrUpdate.Update, "TSPL_BANK_TRANSFER.Transfer_No='" + clsCommon.myCstr(Fnd_Transfernumber.Value) + "'", trans)

            '=====================================
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_BANK_TRANSFER_MCC  where Transfer_No ='" + clsCommon.myCstr(Fnd_Transfernumber.Value) + "' ", trans)
            arrMCC = txtMCC.arrValueMember
            If (arrMCC IsNot Nothing AndAlso arrMCC.Count > 0) Then
                For Each strMCCCode As String In arrMCC
                    Dim collMCC As New Hashtable()
                    Dim strTRCode As String = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"), clsDocType.Detail, clsDocTransactionType.Detail, "")
                    clsCommon.AddColumnsForChange(collMCC, "TR_Code", strTRCode)
                    clsCommon.AddColumnsForChange(collMCC, "Transfer_No", clsCommon.myCstr(Fnd_Transfernumber.Value))
                    clsCommon.AddColumnsForChange(collMCC, "MCC_Code", strMCCCode)
                    clsCommonFunctionality.UpdateDataTable(collMCC, "TSPL_BANK_TRANSFER_MCC", OMInsertOrUpdate.Insert, "", trans)
                Next
                clsDBFuncationality.ExecuteNonQuery("update TSPL_BANK_TRANSFER set From_Date = '" + clsCommon.myCstr(clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy")) + "' , To_Date = '" + clsCommon.myCstr(clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy")) + "' where Transfer_No = '" + clsCommon.myCstr(Fnd_Transfernumber.Value) + "'", trans)
            End If
            '=====================================

            If clicked = False Then
                myMessages.update()
            End If

            btn_save.Text = "Update"
            btn_delete.Enabled = True
            CmbTransType.Enabled = False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub txt_transferamount_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_transferamount.Leave
        txt_depositamount.Text = txt_transferamount.Text
    End Sub

    Private Sub btn_delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_delete.Click
        deletedata()
    End Sub
    Public Sub deletedata(Optional ByVal trans As SqlTransaction = Nothing)
        Try
            If clsCommon.myLen(Txt_frombankCode.Value) > 0 Then
                Dim LocSegmentCode As String = clsDBFuncationality.getSingleValue("Select RIGHT(BANKACC, 3) from TSPL_BANK_MASTER  Where BANK_CODE='" + Txt_frombankCode.Value + "'", trans)
                clsERPFuncationality.ValidateLocationSegment(objCommonVar.CurrentCompanyCode, "Common Services", "Bank Transfer", LocSegmentCode, dtp_transferpostingdate.Value, trans)
            End If
            ''richa ERO/15/05/19-000602
            If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(Post,'') from TSPL_BANK_TRANSFER  where Transfer_No= '" & clsCommon.myCstr(Fnd_Transfernumber.Value) & "' ")), "p") = CompairStringResult.Equal Then
                Throw New Exception("Transaction status is posted so it can't be deleted.")
            End If

            If CheckNegativeBankBalanceonDelete() Then
                If trans Is Nothing Then
                    If myMessages.deleteConfirm() Then
                        fundelete(trans)
                    End If
                Else
                    fundelete(trans)
                End If
            End If
           

        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub fundelete(Optional ByVal trans As SqlTransaction = Nothing)
        Try
            'Ticket No  TEC/10/09/19-001007 Sanjay
            clsCommonFunctionality.SaveDeletedData(objCommonVar.CurrentUserCode, Fnd_Transfernumber.Value, "TSPL_BANK_TRANSFER", "Transfer_No", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, Fnd_Transfernumber.Value, "TSPL_BANK_TRANSFER", "Transfer_No", Nothing)
            connectSql.RunSpTransaction(trans, "sp_tspl_banktransfer_delete", New SqlParameter("@Transfer_No", Fnd_Transfernumber.Value))
            If trans Is Nothing Then
                myMessages.delete()
            End If

            btn_delete.Enabled = False
            btn_save.Text = "Save"
            Fnd_Transfernumber.Focus()
            CmbTransType.Enabled = True
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub btn_reset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_reset.Click
        resetdata()
    End Sub
    '' Anubhooti 16-Dec-2014
    Sub LoadTransactionType()
        Dim dt As New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "W"
        dr("Name") = "Withdrawal"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "R"
        dr("Name") = "Receipt"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "B"
        dr("Name") = "Both"
        dt.Rows.Add(dr)

        CmbTransType.DataSource = dt
        CmbTransType.ValueMember = "Code"
        CmbTransType.DisplayMember = "Name"
        IsLoadTransType = True
    End Sub
    ''
    Public Sub resetdata()
        isLoad = True
        Fnd_Transfernumber.Enabled = True
        dtp_transferpostingdate.Enabled = True
        txt_description.Enabled = True
        txt_references.Enabled = True
        txttranbnkaccno.Text = ""
        Txt_frombankCode.Enabled = True
        txt_transferamount.Enabled = True
        Txt_toBankCode.Enabled = True
        MasterTemplate.ReadOnly = False
        btn_save.Enabled = True
        btn_delete.Enabled = True
        btn_post.Enabled = False

        Fnd_Transfernumber.Value = ""
        dtp_transferpostingdate.Value = clsCommon.GETSERVERDATE()
        txt_description.Text = ""
        txt_references.Text = ""
        Txt_frombankCode.Value = ""
        txt_frombankname.Text = ""
        txt_frombankaccount.Text = ""
        txt_transferamount.Text = ""
        Txt_toBankCode.Value = ""
        txt_tobankname.Text = ""
        txt_tobankaccount.Text = ""
        txtchkno.Text = ""
        txtbnkaccnumber.Text = ""
        txt_depositamount.Text = ""
        txt_depositamount.Text = ""
        txt_transferamount.Text = ""
        txtBankChargesAmt.Value = 0
        TxtRemittTo.Text = ""
        txt_depositamount.Enabled = False
        Fnd_Transfernumber.MyReadOnly = False
        btn_save.Text = "Save"
        UsLock1.Status = ERPTransactionStatus.Pending
        btn_delete.Enabled = False
        CmbTransType.Enabled = True
        CmbTransType.SelectedValue = "W"
        txtwithdrawal.Visible = False
        LblWithdrawal.Visible = False
        Fnd_Transfernumber.Focus()
        MasterTemplate.DataSource = Nothing
        MasterTemplate.Rows.Clear()
        fndPayType.Value = ""
        Lblchkno.Enabled = False
        txtchkno.Enabled = False
        txtchkdate.Enabled = False

        btnPrintCheck.Enabled = False
        btnVoidCheck.Enabled = False
        dtpFromDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
        dtpToDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
        txtMCC.arrValueMember = Nothing
        dtpToDate.Enabled = True
        dtpToDate.ReadOnly = True
        dtpFromDate.Enabled = True
        For i As Integer = 0 To 1
            MasterTemplate.Rows.AddNew()
            MasterTemplate.AllowAddNewRow = False
            MasterTemplate.Rows(i).Cells(3).Value = 0.0
        Next

        RadGroupBox2.Enabled = True
        RadGroupBox3.Enabled = True
        isLoad = False
    End Sub

    'Private Sub TextChanged3(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    dr = connectSql.RunSqlReturnDR("select Transfer_No from TSPL_BANK_TRANSFER where Transfer_No = '" + Fnd_Transfernumber.Value.Trim() + "'")
    '    Dim s As String
    '    While dr.Read()
    '        s = dr(0).ToString()
    '    End While
    '    If s <> "" Then
    '        funfill()
    '    Else
    '        dtp_transferpostingdate.Text = connectSql.serverDate()
    '        txt_description.Text = ""
    '        txt_references.Text = ""
    '        Txt_frombankCode.Value = ""
    '        txt_frombankname.Text = ""
    '        txt_frombankaccount.Text = ""
    '        txt_transferamount.Text = ""
    '        Txt_toBankCode.Value = ""
    '        txt_tobankname.Text = ""
    '        txt_tobankaccount.Text = ""
    '        txt_depositamount.Text = ""
    '        btn_save.Text = "Save"
    '        btn_delete.Enabled = False
    '        'fnd_transfernumber.txtValue.Focus()
    '        MasterTemplate.DataSource = Nothing
    '        MasterTemplate.Rows.Clear()
    '        For i As Integer = 0 To 1
    '            MasterTemplate.Rows.AddNew()
    '            MasterTemplate.AllowAddNewRow = False
    '            MasterTemplate.Rows(i).Cells(3).Value = 0.0

    '        Next
    '    End If
    '    If userCode <> "ADMIN" Then
    '        If funSetUserAccess() = False Then Exit Sub
    '    End If
    'End Sub
    Public Sub funfill(Optional ByVal trans As SqlTransaction = Nothing)
        Try
            If Fnd_Transfernumber.Value <> "" Then
                isLoad = True
                ds = connectSql.RunSQLReturnDS(trans, "select Transfer_Posting_Date,Description,Reference,From_Bank_Code,From_Bank_Name,From_Bank_Acc_No,Transfer_Amount,From_Bank_GL_Acc,From_Bank_GLAcc_Desc,From_Bank_GL_Amount,To_Bank_Code,To_Bank_Name,To_Bank_Acc_No,Deposit_Amount,To_Bank_GL_Acc,To_Bank_GLAcc_Desc,To_Bank_GL_Amount,Cheque_No, Payment_Mode,from_bankaccnumber,to_bankaccnumber,Transaction_Type,Against_Withdrawal_No,Check_Print,ISNULL(Remitt_To,'') AS Remitt_To,BankCharges from TSPL_BANK_TRANSFER where Transfer_No = '" + Fnd_Transfernumber.Value + "'")
                Dim dr As DataRow = ds.Tables(0).Rows(0)
                CmbTransType.SelectedValue = clsCommon.myCstr(dr("Transaction_Type"))
                dtp_transferpostingdate.Value = dr(0).ToString()
                txt_description.Text = dr(1).ToString()
                txt_references.Text = dr(2).ToString()
                Txt_frombankCode.Value = dr(3).ToString()
                txt_frombankname.Text = dr(4).ToString()
                txt_frombankaccount.Text = dr(5).ToString()
                txt_transferamount.Text = dr(6).ToString()
                MasterTemplate.Rows(0).Cells(0).Value = dr(3).ToString()
                MasterTemplate.Rows(0).Cells(1).Value = dr(7).ToString()
                MasterTemplate.Rows(0).Cells(2).Value = dr(8).ToString()
                MasterTemplate.Rows(0).Cells(3).Value = dr(9).ToString()
                Txt_toBankCode.Value = dr(10).ToString()
                txt_tobankname.Text = dr(11).ToString()
                txt_tobankaccount.Text = dr(12).ToString()
                txt_depositamount.Text = dr(13).ToString()
                MasterTemplate.Rows(1).Cells(0).Value = dr(10).ToString()
                MasterTemplate.Rows(1).Cells(1).Value = dr(14).ToString()
                MasterTemplate.Rows(1).Cells(2).Value = dr(15).ToString()
                MasterTemplate.Rows(1).Cells(3).Value = dr(16).ToString()
                txtchkno.Text = clsCommon.myCstr(dr("Cheque_No"))

                txtBankChargesAmt.Value = clsCommon.myCdbl(dr("BankCharges"))
                '' check printing
                chkCheckPrint.Checked = IIf(dr("Check_Print").ToString() = 1, True, False)
                If (dr("Check_Print").ToString() = 1 Or clsCommon.myLen(dr("Cheque_No").ToString()) > 0) Then
                    Me.btnPrintCheck.Enabled = True
                    btnVoidCheck.Enabled = True
                Else
                    Me.btnPrintCheck.Enabled = False
                    btnVoidCheck.Enabled = False
                End If
                '' end check printing


                fndPayType.Value = clsCommon.myCstr(dr("Payment_Mode"))
                If clsCommon.CompairString("Cheque", fndPayType.Value) = CompairStringResult.Equal Then
                    chkCheckPrint.Visible = True
                Else
                    chkCheckPrint.Visible = False
                End If
                txtbnkaccnumber.Text = clsCommon.myCstr(dr("from_bankaccnumber"))
                txttranbnkaccno.Text = clsCommon.myCstr(dr("to_bankaccnumber"))
                '' Anubhooti 18-Dec-2014 BM00000004959 (Fetched Transaction_Type,Against_Withdrawal_No)

                If clsCommon.CompairString(CmbTransType.SelectedValue, "R") = CompairStringResult.Equal Then
                    LblWithdrawal.Visible = True
                    txtwithdrawal.Visible = True
                    RadGroupBox2.Enabled = False
                    RadGroupBox3.Enabled = False
                Else
                    LblWithdrawal.Visible = False
                    txtwithdrawal.Visible = False
                    RadGroupBox2.Enabled = True
                    RadGroupBox3.Enabled = True
                End If
                txtwithdrawal.Value = clsCommon.myCstr(dr("Against_Withdrawal_No"))
                ''
                '' Anubhooti 02-Apr-2015 (Fetch Remitt_To Column)
                TxtRemittTo.Text = clsCommon.myCstr(dr("Remitt_To"))
                ''

                Fnd_Transfernumber.MyReadOnly = True
                CmbTransType.Enabled = False

                Dim post As String = connectSql.RunScalar(trans, "select post from TSPL_BANK_TRANSFER where Transfer_No = '" + Fnd_Transfernumber.Value + "'")
                If Not String.IsNullOrEmpty(post) Then
                    If post.Trim() = "p" Then
                        funposteddata()
                        UsLock1.Status = ERPTransactionStatus.Approved
                        btn_save.Text = "Update"
                    Else
                        btn_save.Text = "Update"
                        Fnd_Transfernumber.MyReadOnly = True
                        CmbTransType.Enabled = False
                        UsLock1.Status = ERPTransactionStatus.Pending

                        txtbnkaccnumber.Text = ""
                        txttranbnkaccno.Text = ""
                        Fnd_Transfernumber.Enabled = True
                        dtp_transferpostingdate.Enabled = True
                        txt_description.Enabled = True
                        txt_references.Enabled = True
                        Txt_frombankCode.Enabled = True
                        txt_transferamount.Enabled = True
                        Txt_toBankCode.Enabled = True
                        txt_depositamount.Enabled = False
                        MasterTemplate.ReadOnly = False
                        btn_save.Enabled = True
                        btn_delete.Enabled = True
                        btn_post.Enabled = True
                        If clsCommon.myLen(txtchkno.Text) <= 0 Then
                            txtchangedPaymentMode(trans)
                        End If

                    End If
                End If

                'Dim post As String = connectSql.RunScalar(trans, "select post from TSPL_BANK_TRANSFER where Transfer_No = '" + Fnd_Transfernumber.Value + "'")
                'If Not String.IsNullOrEmpty(post) Then
                '    If post.Trim() = "p" Then
                '        ds = connectSql.RunSQLReturnDS(trans, "select Transfer_Posting_Date,Description,Reference,From_Bank_Code,From_Bank_Name,From_Bank_Acc_No,Transfer_Amount,From_Bank_GL_Acc,From_Bank_GLAcc_Desc,From_Bank_GL_Amount,To_Bank_Code,To_Bank_Name,To_Bank_Acc_No,Deposit_Amount,To_Bank_GL_Acc,To_Bank_GLAcc_Desc,To_Bank_GL_Amount,Cheque_No, Payment_Mode,from_bankaccnumber,to_bankaccnumber,Transaction_Type,Against_Withdrawal_No,Check_Print,ISNULL(Remitt_To,'') AS Remitt_To from TSPL_BANK_TRANSFER where Transfer_No = '" + Fnd_Transfernumber.Value + "'")
                '        Dim dr As DataRow = ds.Tables(0).Rows(0)
                '        CmbTransType.SelectedValue = clsCommon.myCstr(dr("Transaction_Type"))
                '        dtp_transferpostingdate.Value = dr(0).ToString()
                '        txt_description.Text = dr(1).ToString()
                '        txt_references.Text = dr(2).ToString()
                '        Txt_frombankCode.Value = dr(3).ToString()
                '        txt_frombankname.Text = dr(4).ToString()
                '        txt_frombankaccount.Text = dr(5).ToString()
                '        txt_transferamount.Text = dr(6).ToString()
                '        MasterTemplate.Rows(0).Cells(0).Value = dr(3).ToString()
                '        MasterTemplate.Rows(0).Cells(1).Value = dr(7).ToString()
                '        MasterTemplate.Rows(0).Cells(2).Value = dr(8).ToString()
                '        MasterTemplate.Rows(0).Cells(3).Value = dr(9).ToString()
                '        Txt_toBankCode.Value = dr(10).ToString()
                '        txt_tobankname.Text = dr(11).ToString()
                '        txt_tobankaccount.Text = dr(12).ToString()
                '        txt_depositamount.Text = dr(13).ToString()
                '        MasterTemplate.Rows(1).Cells(0).Value = dr(10).ToString()
                '        MasterTemplate.Rows(1).Cells(1).Value = dr(14).ToString()
                '        MasterTemplate.Rows(1).Cells(2).Value = dr(15).ToString()
                '        MasterTemplate.Rows(1).Cells(3).Value = dr(16).ToString()
                '        txtchkno.Text = clsCommon.myCstr(dr("Cheque_No"))


                '        '' check printing
                '        chkCheckPrint.Checked = IIf(dr("Check_Print").ToString() = 1, True, False)
                '        If (dr("Check_Print").ToString() = 1 Or clsCommon.myLen(dr("Cheque_No").ToString()) > 0) Then
                '            Me.btnPrintCheck.Enabled = True
                '            btnVoidCheck.Enabled = True
                '        Else
                '            Me.btnPrintCheck.Enabled = False
                '            btnVoidCheck.Enabled = False
                '        End If
                '        '' end check printing


                '        fndPayType.Value = clsCommon.myCstr(dr("Payment_Mode"))
                '        If clsCommon.CompairString("Cheque", fndPayType.Value) = CompairStringResult.Equal Then
                '            chkCheckPrint.Visible = True
                '        Else
                '            chkCheckPrint.Visible = False
                '        End If
                '        txtbnkaccnumber.Text = clsCommon.myCstr(dr("from_bankaccnumber"))
                '        txttranbnkaccno.Text = clsCommon.myCstr(dr("to_bankaccnumber"))
                '        '' Anubhooti 18-Dec-2014 BM00000004959 (Fetched Transaction_Type,Against_Withdrawal_No)

                '        If clsCommon.CompairString(CmbTransType.SelectedValue, "R") = CompairStringResult.Equal Then
                '            LblWithdrawal.Visible = True
                '            txtwithdrawal.Visible = True
                '            RadGroupBox2.Enabled = False
                '            RadGroupBox3.Enabled = False
                '        Else
                '            LblWithdrawal.Visible = False
                '            txtwithdrawal.Visible = False
                '            RadGroupBox2.Enabled = True
                '            RadGroupBox3.Enabled = True
                '        End If
                '        txtwithdrawal.Value = clsCommon.myCstr(dr("Against_Withdrawal_No"))
                '        ''
                '        '' Anubhooti 02-Apr-2015 (Fetch Remitt_To Column)
                '        TxtRemittTo.Text = clsCommon.myCstr(dr("Remitt_To"))
                '        ''
                '        btn_save.Text = "Update"
                '        Fnd_Transfernumber.MyReadOnly = True
                '        CmbTransType.Enabled = False
                '        funposteddata()
                '        UsLock1.Status = ERPTransactionStatus.Approved
                '    Else

                '        ds = connectSql.RunSQLReturnDS(trans, "select Transfer_Posting_Date,Description,Reference,From_Bank_Code,From_Bank_Name,From_Bank_Acc_No,Transfer_Amount,From_Bank_GL_Acc,From_Bank_GLAcc_Desc,From_Bank_GL_Amount,To_Bank_Code,To_Bank_Name,To_Bank_Acc_No,Deposit_Amount,To_Bank_GL_Acc,To_Bank_GLAcc_Desc,To_Bank_GL_Amount,Cheque_No, Payment_Mode,from_bankaccnumber,to_bankaccnumber,Transaction_Type,Against_Withdrawal_No,Check_Print,ISNULL(Remitt_To,'') AS Remitt_To from TSPL_BANK_TRANSFER where Transfer_No = '" + Fnd_Transfernumber.Value + "'")
                '        Dim dr As DataRow = ds.Tables(0).Rows(0)
                '        dtp_transferpostingdate.Value = dr(0).ToString()
                '        txt_description.Text = dr(1).ToString()
                '        txt_references.Text = dr(2).ToString()
                '        Txt_frombankCode.Value = dr(3).ToString()
                '        txt_frombankname.Text = dr(4).ToString()
                '        txt_frombankaccount.Text = dr(5).ToString()
                '        txt_transferamount.Text = dr(6).ToString()
                '        txtbnkaccnumber.Text = clsCommon.myCstr(dr("from_bankaccnumber"))
                '        txttranbnkaccno.Text = clsCommon.myCstr(dr("to_bankaccnumber"))
                '        MasterTemplate.Rows(0).Cells(0).Value = dr(3).ToString()
                '        MasterTemplate.Rows(0).Cells(1).Value = dr(7).ToString()
                '        MasterTemplate.Rows(0).Cells(2).Value = dr(8).ToString()
                '        MasterTemplate.Rows(0).Cells(3).Value = dr(9).ToString()
                '        Txt_toBankCode.Value = dr(10).ToString()
                '        txt_tobankname.Text = dr(11).ToString()
                '        txt_tobankaccount.Text = dr(12).ToString()
                '        txt_depositamount.Text = dr(13).ToString()
                '        MasterTemplate.Rows(1).Cells(0).Value = dr(10).ToString()
                '        MasterTemplate.Rows(1).Cells(1).Value = dr(14).ToString()
                '        MasterTemplate.Rows(1).Cells(2).Value = dr(15).ToString()
                '        MasterTemplate.Rows(1).Cells(3).Value = dr(16).ToString()
                '        txtchkno.Text = clsCommon.myCstr(dr("Cheque_No"))
                '        fndPayType.Value = clsCommon.myCstr(dr("Payment_Mode"))

                '        If clsCommon.CompairString("Cheque", fndPayType.Value) = CompairStringResult.Equal Then
                '            chkCheckPrint.Visible = True
                '        Else
                '            chkCheckPrint.Visible = False
                '        End If

                '        '' Anubhooti 18-Dec-2014 BM00000004959 (Fetched Transaction_Type,Against_Withdrawal_No)
                '        CmbTransType.SelectedValue = clsCommon.myCstr(dr("Transaction_Type"))
                '        txtwithdrawal.Value = clsCommon.myCstr(dr("Against_Withdrawal_No"))
                '        ''
                '        '' check printing
                '        chkCheckPrint.Checked = IIf(dr("Check_Print").ToString() = 1, True, False)
                '        If (dr("Check_Print").ToString() = 1 Or clsCommon.myLen(dr("Cheque_No").ToString()) > 0) Then
                '            Me.btnPrintCheck.Enabled = True
                '            btnVoidCheck.Enabled = True
                '        Else
                '            Me.btnPrintCheck.Enabled = False
                '            btnVoidCheck.Enabled = False
                '        End If
                '        '' end check printing
                '        '' Anubhooti 02-Apr-2015 (Fetch Remitt_To Column)
                '        TxtRemittTo.Text = clsCommon.myCstr(dr("Remitt_To"))
                '        ''
                '        btn_save.Text = "Update"
                '        Fnd_Transfernumber.MyReadOnly = True
                '        CmbTransType.Enabled = False
                '        UsLock1.Status = ERPTransactionStatus.Pending

                '        txtbnkaccnumber.Text = ""
                '        txttranbnkaccno.Text = ""
                '        Fnd_Transfernumber.Enabled = True
                '        dtp_transferpostingdate.Enabled = True
                '        txt_description.Enabled = True
                '        txt_references.Enabled = True
                '        Txt_frombankCode.Enabled = True
                '        txt_transferamount.Enabled = True
                '        Txt_toBankCode.Enabled = True
                '        txt_depositamount.Enabled = False
                '        MasterTemplate.ReadOnly = False
                '        btn_save.Enabled = True
                '        btn_delete.Enabled = True
                '        btn_post.Enabled = True
                '    End If
                '    If clsCommon.myLen(txtchkno.Text) <= 0 Then
                '        txtchangedPaymentMode(trans)
                '    End If
                'Else
                '    btn_save.Text = "Save"
                '    Fnd_Transfernumber.MyReadOnly = False
                'End If

                '===========================
                Dim arrMCC1 As ArrayList = Nothing
                Dim qry As String = "select TSPL_BANK_TRANSFER_MCC.MCC_Code from TSPL_BANK_TRANSFER_MCC where TSPL_BANK_TRANSFER_MCC.Transfer_No='" + Fnd_Transfernumber.Value + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    arrMCC1 = New ArrayList()
                    For Each drMCC As DataRow In dt.Rows
                        arrMCC1.Add(clsCommon.myCstr(drMCC("MCC_Code")))
                    Next
                End If
                txtMCC.arrValueMember = arrMCC1
                If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                    dtpFromDate.Value = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select From_Date from TSPL_BANK_TRANSFER where Transfer_No = '" + Fnd_Transfernumber.Value + "'"))
                    dtpToDate.Value = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select To_Date from TSPL_BANK_TRANSFER where Transfer_No = '" + Fnd_Transfernumber.Value + "'"))
                End If
                '===========================

                isLoad = False
            End If
        Catch ex As Exception
            isLoad = False
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Private Sub funposteddata()
        txt_depositamount.Enabled = False
        UsLock1.Status = ERPTransactionStatus.Approved
        MasterTemplate.ReadOnly = True
        btn_save.Enabled = False
        btn_delete.Enabled = False
        btn_post.Enabled = False
    End Sub
    'Private Sub KeyPress1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    '    Fnd_Transfernumber.CharacterCasing = CharacterCasing.Upper
    'End Sub

    'Private Sub fnd_transfernumber_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fnd_transfernumber.ConnectionString = connectSql.SqlCon()
    '    fnd_transfernumber.Query = "select [Transfer_No] as [Transfer No],[description] as [Description] from TSPL_BANK_TRANSFER"
    '    fnd_transfernumber.ValueToSelect = "Transfer No"
    '    fnd_transfernumber.Caption = "Transfer No"
    '    fnd_transfernumber.ValueToSelect1 = "Transfer No"
    'End Sub

    Private Sub txt_transferamount_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_transferamount.KeyPress
        If (Microsoft.VisualBasic.Asc(e.KeyChar) < 48) _
                Or (Microsoft.VisualBasic.Asc(e.KeyChar) > 57) Then
            e.Handled = True
            If (Microsoft.VisualBasic.Asc(e.KeyChar) = 8) Then
                e.Handled = False
            End If
            If (Microsoft.VisualBasic.Asc(e.KeyChar) = 46) Then
                e.Handled = False
            End If
            If (Microsoft.VisualBasic.Asc(e.KeyChar) = 39) Then
                e.Handled = False
            End If
        End If
    End Sub

    Private Sub txt_depositamount_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_depositamount.KeyPress
        If (Microsoft.VisualBasic.Asc(e.KeyChar) < 48) _
                Or (Microsoft.VisualBasic.Asc(e.KeyChar) > 57) Then
            e.Handled = True
            If (Microsoft.VisualBasic.Asc(e.KeyChar) = 8) Then
                e.Handled = False
            End If
            If (Microsoft.VisualBasic.Asc(e.KeyChar) = 46) Then
                e.Handled = False
            End If
            If (Microsoft.VisualBasic.Asc(e.KeyChar) = 39) Then
                e.Handled = False
            End If
        End If
    End Sub

    Private Sub MasterTemplate_CellBeginEdit(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellCancelEventArgs) Handles MasterTemplate.CellBeginEdit
        If TypeOf Me.MasterTemplate.CurrentColumn Is GridViewMultiComboBoxColumn Then
            Dim editor As RadMultiColumnComboBoxElement = DirectCast(Me.MasterTemplate.ActiveEditor, RadMultiColumnComboBoxElement)
            editor.AutoSizeDropDownToBestFit = True
        End If
    End Sub

    'Private Sub MasterTemplate_UserAddedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles MasterTemplate.UserAddedRow
    '    If IsDBNull(MasterTemplate.CurrentRow.Cells(3).Value) Then
    '        MasterTemplate.CurrentRow.Cells(3).Value = 0
    '    End If
    'End Sub

    Private Function funautogenerateno(Optional ByVal Tran As SqlTransaction = Nothing) As String
        Dim strgeneratecode As String = ""
        Dim strgenerate As String
        Dim total As String = ""
        Dim cutgenerate As String
        Try
            strgenerate = strgeneratecode + "%"
            Dim str1 As String = ""
            Dim check As Integer
            If Tran IsNot Nothing Then
                check = connectSql.RunScalar(Tran, "select count(*) from TSPL_BANK_TRANSFER")
            Else
                check = connectSql.RunScalar("select count(*) from TSPL_BANK_TRANSFER")
            End If
            If check <> 0 Then
                If Tran IsNot Nothing Then
                    str1 = connectSql.RunScalar(Tran, "select MAX(Transfer_No)  from TSPL_BANK_TRANSFER  where Transfer_No like 'TN%'")
                Else
                    str1 = connectSql.RunScalar("select MAX(Transfer_No)  from TSPL_BANK_TRANSFER  where Transfer_No like 'TN%'")
                End If
            Else
            End If
            If String.IsNullOrEmpty(str1) Then
                total = "TN" + "000001"
            Else
                cutgenerate = str1.Substring(2, 6)
                Dim i As Integer = Integer.Parse(cutgenerate)
                i = i + 1
                Dim stri As String = CStr(i)
                If stri.Length = 1 Then
                    Dim t As String = Convert.ToString(i)
                    total = Convert.ToString("TN" + "00000" + t)
                ElseIf stri.Length = 2 Then
                    Dim t As String = Convert.ToString(i)
                    total = "TN" + "0000" + t
                ElseIf stri.Length = 3 Then
                    Dim t As String = Convert.ToString(i)
                    total = "TN" + "000" + t
                ElseIf stri.Length = 4 Then
                    Dim t As String = Convert.ToString(i)
                    total = "TN" + "00" + t
                ElseIf stri.Length = 5 Then
                    Dim t As String = Convert.ToString(i)
                    total = "TN" + "0" + t
                ElseIf stri.Length = 6 Then
                    Dim t As String = Convert.ToString(i)
                    total = "TN" + t
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
        Return total

    End Function

    Private Sub btn_post_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_post.Click
        clicked = True
        postdata()
    End Sub
    Public Sub postdata(Optional ByVal trans As SqlTransaction = Nothing)
        Try
            If clsCommon.myLen(Txt_frombankCode.Value) > 0 Then
                Dim LocSegmentCode As String = clsDBFuncationality.getSingleValue("Select RIGHT(BANKACC, 3) from TSPL_BANK_MASTER  Where BANK_CODE='" + Txt_frombankCode.Value + "'", trans)
                clsERPFuncationality.ValidateLocationSegment(objCommonVar.CurrentCompanyCode, "Common Services", "Bank Transfer", LocSegmentCode, dtp_transferpostingdate.Value, trans)
            End If
            If trans Is Nothing Then
                If myMessages.postConfirm() Then
                    isFlag = True
                    savedata(clicked)
                    If clsBankTrasnferNew.PostData(Fnd_Transfernumber.Value) Then
                        myMessages.post()
                        funposteddata()
                    End If
                Else
                End If
            Else
                isFlag = True
                savedata(clicked, trans)
                If clsBankTrasnferNew.PostData(Fnd_Transfernumber.Value, trans) Then
                    funposteddata()
                End If
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isFlag = False
        End Try
        clicked = False
    End Sub

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click

    End Sub
    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "BANK-TRANSF"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete & "," & enuUserRights.enuAuthorised
    '        strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
    '        strTemp = Split(strRights, ",")
    '        If strTemp(0) = "0" Then
    '            MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
    '            funSetUserAccess = False
    '            blnRead = False
    '            Me.Close()
    '            Exit Function
    '        Else
    '            blnRead = True
    '        End If
    '        If strTemp(1) = "0" Then 'Grant modify access
    '            btn_save.Enabled = False
    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access
    '            btn_delete.Enabled = False
    '        End If
    '        If strTemp(3) = "0" Then 'Grant Authorize access
    '            btn_post.Enabled = False
    '        End If
    '        funSetUserAccess = True
    '    Catch er As Exception

    '    End Try
    'End Function

    Private Sub FrmBankTransfer_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btn_reset.Enabled Then
            resetdata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btn_save.Enabled Then
            savedata(clicked)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btn_post.Enabled Then
            postdata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btn_delete.Enabled Then
            deletedata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Control AndAlso e.Alt AndAlso e.Shift AndAlso e.KeyCode = Keys.F12 Then
            If MyBase.isReverse Then

                Dim frm As New FrmPWD(Nothing)
                frm.strType = "SIRC"
                frm.strCode = "SIReversAndCreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnReverseAndRecreate.Visible = True
                    'btnBlankForReCreateJE.Visible = True
                    'btnReCreateJE.Visible = True
                End If
            Else
                MessageBox.Show("You are not authorized to perform this action.", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub

    Private Sub Txt_frombankCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles Txt_frombankCode._MYValidating
        Dim strWhrclas As String = ""
        Dim qry As String = clsERPFuncationality.glbankqueryNew(strWhrclas)
        strWhrclas = "INACTIVE='Active'"
        Txt_frombankCode.Value = clsCommon.ShowSelectForm("FrmBankCode", qry, "Code", strWhrclas, Txt_frombankCode.Value, "Code", isButtonClicked)
        txt_frombankname.Text = clsDBFuncationality.getSingleValue("select Description from tspl_bank_master where bank_Code='" & Txt_frombankCode.Value & "'")
        fndPayType.Value = connectSql.RunScalar("select TSPL_PAYMENT_CODE.Payment_Code   from TSPL_PAYMENT_CODE Where TSPL_PAYMENT_CODE.Payment_Code=  (select DISTINCT (case when Bank_type = 'C' THEN 'CASH' WHEN BANK_TYPE = 'B' THEN 'CHEQUE' WHEN BANK_TYPE = 'O' THEN 'OTHER' WHEN Bank_type = 'P' THEN 'PETTYCASH' else 'CASH' END ) AS [Paymet Type] from TSPL_BANK_MASTER Where BANK_CODE='" + Txt_frombankCode.Value + "' )") '--10/12/2012-Added By-Pankaj Kumar
        Try
            'dr = connectSql.RunSqlReturnDR("select Bank_Code from TSPL_BANK_MASTER where [Bank_Code] ='" + Txt_frombankCode.Value + "'")
            Dim s As String = ""
            'While dr.Read()
            Dim strSql As String = "select Bank_Code from TSPL_BANK_MASTER where [Bank_Code] ='" + Txt_frombankCode.Value + "'"

            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(strSql)
            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                For Each dr As DataRow In dt1.Rows
                    s = dr(0).ToString()
                Next
            End If
            If s <> "" Then
                funFill1()
            Else
                txt_frombankname.Text = ""
                txt_frombankaccount.Text = ""
                txt_transferamount.Text = ""
                txtbnkaccnumber.Text = ""
                btn_save.Enabled = True
                btn_save.Text = "Save"
            End If
            txtchangedPaymentMode()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub Txt_frombankCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Txt_frombankCode.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub Txt_toBankCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles Txt_toBankCode._MYValidating
        Dim strWhrclas As String = ""
        Dim qry As String = clsERPFuncationality.glbankqueryNew(strWhrclas)
        strWhrclas = "INACTIVE='Active'"
        Txt_toBankCode.Value = clsCommon.ShowSelectForm("fmToBankCode", qry, "Code", strWhrclas, Txt_toBankCode.Value, "Code", isButtonClicked)
        txt_tobankname.Text = clsDBFuncationality.getSingleValue("select Description from tspl_bank_master where bank_Code='" & Txt_toBankCode.Value & "'")

        Try
            'dr = connectSql.RunSqlReturnDR("select Bank_Code from TSPL_BANK_MASTER where [Bank_Code] ='" + Txt_toBankCode.Value + "'")
            Dim s As String = ""
            'While dr.Read()
            Dim strSql As String = "select Bank_Code from TSPL_BANK_MASTER where [Bank_Code] ='" + Txt_toBankCode.Value + "'"

            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(strSql)
            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                For Each dr As DataRow In dt1.Rows
                    s = dr(0).ToString()
                Next
            End If
            If s <> "" Then
                funFill2()
            Else
                txt_tobankname.Text = ""
                txt_tobankaccount.Text = ""
                txt_depositamount.Text = ""
                txttranbnkaccno.Text = ""
                btn_save.Enabled = True
                btn_save.Text = "Save"
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub Txt_toBankCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Txt_toBankCode.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub
    Private Sub Fnd_Transfernumber__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles Fnd_Transfernumber._MYNavigator
        Dim qry As String = "select Transfer_No  from TSPL_BANK_TRANSFER LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.Bank_Code=TSPL_BANK_TRANSFER.From_Bank_Code Where 2=2 "
        Dim Bank_Code As String = FrmMainTranScreen.bankPermission(Nothing)
        Dim strWhrclas As String = ""
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PermissionSettingForTransactionWithBank, clsFixedParameterType.PermissionSettingForTransactionWithBank, Nothing)) = 1 Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                strWhrclas += " AND RIGHT(TSPL_BANK_MASTER.BANKACC,3) in (" + objCommonVar.strCurrUserLocationsSegment + ")"
            End If
        ElseIf clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PermissionSettingForTransactionWithBank, clsFixedParameterType.PermissionSettingForTransactionWithBank, Nothing)) = 1 Then
            If clsCommon.myLen(Bank_Code) > 0 Then
                strWhrclas += " AND TSPL_BANK_TRANSFER.Bank_Code in ( " + Bank_Code + " )"
            End If
        End If
        qry += " " + strWhrclas + ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_BANK_TRANSFER.Transfer_No=(select MIN(Transfer_No) from TSPL_BANK_TRANSFER LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.Bank_Code=TSPL_BANK_TRANSFER.From_Bank_Code Where 1=1 " + strWhrclas + ")"
            Case NavigatorType.Last
                qry += " and TSPL_BANK_TRANSFER.Transfer_No=(select MAX(Transfer_No) from TSPL_BANK_TRANSFER LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.Bank_Code=TSPL_BANK_TRANSFER.From_Bank_Code Where 1=1 " + strWhrclas + ")"
            Case NavigatorType.Next
                qry += " and TSPL_BANK_TRANSFER.Transfer_No=(select Min(Transfer_No) from TSPL_BANK_TRANSFER LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.Bank_Code=TSPL_BANK_TRANSFER.From_Bank_Code where Transfer_No > '" + Fnd_Transfernumber.Value + "' " + strWhrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_BANK_TRANSFER.Transfer_No=(select Max(Transfer_No) from TSPL_BANK_TRANSFER LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.Bank_Code=TSPL_BANK_TRANSFER.From_Bank_Code where Transfer_No < '" + Fnd_Transfernumber.Value + "' " + strWhrclas + ")"
            Case NavigatorType.Current
                qry += " and TSPL_BANK_TRANSFER.Transfer_No='" + Fnd_Transfernumber.Value + "'"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            Fnd_Transfernumber.Value = clsCommon.myCstr(dt.Rows(0)("Transfer_No"))
            funfill()
        End If
    End Sub

    Private Sub Fnd_Transfernumber__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles Fnd_Transfernumber._MYValidating
        Dim qry As String = "select [Transfer_No] as [Code],Transfer_Date AS [Transfer Date], TSPL_BANK_TRANSFER.Description ,Payment_Mode AS [Payment Mode], Transaction_Type AS [TransactionType],ISNULL(Against_Withdrawal_No,'') AS Against_Withdrawal_No, From_Bank_Code  AS [From Bank Code],From_Bank_Name AS [From Bank Name] ,From_Bank_Acc_No AS [From Bank Acc No],Transfer_Amount AS [Transfer Amount],To_Bank_Code AS [To Bank Code],To_Bank_Name AS [To Bank Name],To_Bank_Acc_No AS  [To Bank Acc No],Deposit_Amount AS [Deposit Amount],Post,TSPL_BANK_TRANSFER.CHECK_PRINT as [Check Print] ,TSPL_BANK_TRANSFER.CHECK_CODE as [Check Code],ISNULL(Remitt_To,'') AS [Remitt To],isnull(BankCharges,0) as [Bank Charges],isnull(Bank_Charges_Ac,'') as [Bank Charges A/c]  from TSPL_BANK_TRANSFER" & _
        " LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.Bank_Code=TSPL_BANK_TRANSFER.From_Bank_Code"
        Dim Bank_Code As String = FrmMainTranScreen.bankPermission(Nothing)
        Dim strWhrclas As String = "1=1"
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PermissionSettingForTransactionWithBank, clsFixedParameterType.PermissionSettingForTransactionWithBank, Nothing)) = 1 Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                strWhrclas += " AND RIGHT(TSPL_BANK_MASTER.BANKACC,3) in (" + objCommonVar.strCurrUserLocationsSegment + ")"
            End If
        ElseIf clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PermissionSettingForTransactionWithBank, clsFixedParameterType.PermissionSettingForTransactionWithBank, Nothing)) = 1 Then
            If clsCommon.myLen(Bank_Code) > 0 Then
                strWhrclas += " AND TSPL_BANK_TRANSFER.From_Bank_Code in ( " + Bank_Code + " )"
            End If
        End If
        Fnd_Transfernumber.Value = clsCommon.ShowSelectForm("BankTranNo", qry, "Code", strWhrclas, Fnd_Transfernumber.Value, "Code", isButtonClicked)
        txt_description.Text = clsDBFuncationality.getSingleValue("select Description from TSPL_BANK_TRANSFER where Transfer_No='" & Fnd_Transfernumber.Value & "'")
        'dr = connectSql.RunSqlReturnDR("select Transfer_No from TSPL_BANK_TRANSFER where Transfer_No = '" + Fnd_Transfernumber.Value.Trim() + "'")
        Dim s As String = ""
        'While dr.Read()
        Dim strSql As String = "select Transfer_No from TSPL_BANK_TRANSFER where Transfer_No = '" + Fnd_Transfernumber.Value.Trim() + "'"

        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(strSql)
        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            For Each dr As DataRow In dt1.Rows
                s = dr(0).ToString()
            Next
        End If
        If s <> "" Then
            funfill()
        Else
            dtp_transferpostingdate.Text = connectSql.serverDate()
            txt_description.Text = ""
            txt_references.Text = ""
            Txt_frombankCode.Value = ""
            txt_frombankname.Text = ""
            txt_frombankaccount.Text = ""
            txt_transferamount.Text = ""
            txtBankChargesAmt.Text =
            Txt_toBankCode.Value = ""
            txt_tobankname.Text = ""
            txt_tobankaccount.Text = ""
            txt_depositamount.Text = ""
            txtbnkaccnumber.Text = ""
            txttranbnkaccno.Text = ""
            txtwithdrawal.Value = ""
            CmbTransType.Enabled = True
            CmbTransType.SelectedValue = "W"
            TxtRemittTo.Text = ""
            txtMCC.arrValueMember = Nothing
            btn_save.Text = "Save"
            btn_delete.Enabled = False
            'fnd_transfernumber.txtValue.Focus()
            MasterTemplate.DataSource = Nothing
            MasterTemplate.Rows.Clear()
            For i As Integer = 0 To 1
                MasterTemplate.Rows.AddNew()
                MasterTemplate.AllowAddNewRow = False
                MasterTemplate.Rows(i).Cells(3).Value = 0.0
            Next
        End If
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If

    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        FunPrintData(Fnd_Transfernumber.Value)
    End Sub

    ' Following codes re-arrangment was made according shruti madam on 06-03-2017
    Public Sub FunPrintData(ByVal StrCode As String)
        Dim query As String = Nothing
        Dim stLoc As String = Nothing
        Dim strCmpr As String = Nothing
        Dim strMcc As String = Nothing
        ''richa BHA/13/09/18-000545
        Try
            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                strMcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select  STUFF((SELECT ','+'['+ +aa.MCC_NAME +']' from (select distinct  TSPL_MCC_MASTER.MCC_NAME from TSPL_BANK_TRANSFER_MCC left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_BANK_TRANSFER_MCC.MCC_Code where TSPL_MCC_MASTER.MCC_NAME is not null and TSPL_BANK_TRANSFER_MCC.mcc_code in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ") )aa order by aa.MCC_NAME  FOR XML PATH ('')), 1, 1, '') "))  ' clsCommon.GetMulcallString(txtMCC.arrValueMember)
            End If
            stLoc = "loc_segment_code"
            strCmpr = "TSPL_GL_ACCOUNTS"
            query = "select '" + strMcc + "' as MCC, '" + clsCommon.myCstr(clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy")) + "' as FromDate, '" + clsCommon.myCstr(clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy")) + "' as ToDate , tspl_bank_transfer.Transfer_Amount+ISNULL (BankCharges ,0) as CrAmt,tspl_bank_transfer.BankCharges,tspl_bank_transfer.Bank_Charges_Ac,BM_GL.Account_Seg_Desc1 AS BankChargesDesc, Transfer_No as TransferNum ,From_Bank_Acc_No as FromBankCode,From_Bank_Name as FrombankName,To_Bank_Acc_No  as ToAccCode ,To_Bank_Name as ToBankName, tspl_bank_transfer.Cheque_No,tspl_bank_transfer.Description + CASE WHEN LEN(TSPL_BANK_TRANSFER .Cheque_No)>0 THEN '/ CHEQUE NO :'+TSPL_BANK_TRANSFER .Cheque_No  ELSE '' END as Description," &
                  "  Deposit_Amount as Amount,TSPL_GL_ACCOUNTS .Account_Seg_Code7 as Location,GlAcc .Account_Seg_Code7 as FromLocation,Transfer_Posting_Date as VoucherDate ," &
                  "  TSPL_BANK_TRANSFER .Comp_Code as CompCode ,TSPL_COMPANY_MASTER.Comp_Name as CompName, TSPL_COMPANY_MASTER.Logo_Img as Image1" &
                  "   , TSPL_COMPANY_MASTER.Logo_Img2 as Image2,(select max(ADD1 + case when len(add2)> 0 then ',' else '' end + ADD2 +case when len(add3)> 0 then ','else '' end +ADD3+case when len(add4)> 0 then ',' else '' end +ADD4+case when len(City_Code)> 0 then ',' else '' end +City_Code +case when len(STATE)> 0 then ',' else '' end  +STATE) from tspl_location_master where " + stLoc + "=" + strCmpr + " .Account_Seg_Code7)  as address " &
                  "   , (case TSPL_BANK_MASTER .Bank_type when 'B'then 'Bank Transfer'when 'C'then 'Cash Transfer' when 'P'then 'Petty Cash' when 'O' then 'Other' else'' end ) as  BankType,(case TSPL_BANK_TRANSFER.Transaction_Type when 'B' then 'Bank Transfer'when 'W' then 'Bank Debit Voucher' when 'R' then 'Bank Credit Voucher' else'' end ) as  TransType,  TSPL_USER_MASTER_Created.User_Name as Created_By, TSPL_USER_MASTER_Modified.User_Name as Modify_By    from TSPL_BANK_TRANSFER  left outer join TSPL_GL_ACCOUNTS on TSPL_BANK_TRANSFER .To_Bank_Acc_No  =TSPL_GL_ACCOUNTS .Account_Code left outer join TSPL_GL_ACCOUNTS as GlAcc  on TSPL_BANK_TRANSFER .From_Bank_Acc_No =GlAcc .Account_Code   " &
                  "   left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER .Comp_Code = TSPL_BANK_TRANSFER .Comp_Code left outer join TSPL_BANK_MASTER on TSPL_BANK_TRANSFER .From_Bank_Code =TSPL_BANK_MASTER .BANK_CODE  left outer join TSPL_GL_ACCOUNTS BM_GL on TSPL_BANK_TRANSFER .Bank_Charges_Ac  =BM_GL.Account_Code  left outer join TSPL_USER_MASTER as TSPL_USER_MASTER_Created on  TSPL_USER_MASTER_Created.User_Code = TSPL_BANK_TRANSFER.Created_By
                      left outer join TSPL_USER_MASTER as TSPL_USER_MASTER_Modified on  TSPL_USER_MASTER_Modified.User_Code = TSPL_BANK_TRANSFER.Modify_By where Transfer_No ='" + StrCode + "'"

            If (clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "KL") = CompairStringResult.Equal OrElse clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "001") = CompairStringResult.Equal) Then
                stLoc = "loc_segment_code"
                strCmpr = " RIGHT(TSPL_BANK_TRANSFER .From_Bank_Acc_No  ,3) and tspl_location_master.Location_Code =tspl_location_master.Loc_Segment_Code "
                query = "  select Transfer_No as TransferNum ,From_Bank_Acc_No as FromBankCode,From_Bank_Name as FrombankName,To_Bank_Acc_No  as ToAccCode ,To_Bank_Name as ToBankName, tspl_bank_transfer.Cheque_No,tspl_bank_transfer.Description + CASE WHEN LEN(TSPL_BANK_TRANSFER .Cheque_No)>0 THEN '/ CHEQUE NO :'+TSPL_BANK_TRANSFER .Cheque_No  ELSE '' END as Description," &
                        "  Deposit_Amount as Amount,TSPL_GL_ACCOUNTS .Account_Seg_Code7 as Location,GlAcc .Account_Seg_Code7 as FromLocation,Transfer_Posting_Date as VoucherDate ," &
                        "  TSPL_BANK_TRANSFER .Comp_Code as CompCode ,TSPL_COMPANY_MASTER.Comp_Name as CompName, TSPL_COMPANY_MASTER.Logo_Img as Image1" &
                        "  , TSPL_COMPANY_MASTER.Logo_Img2 as Image2,(select (ADD1 + case when len(add2)> 0 then ',' else '' end + ADD2 +case when len(add3)> 0 then ','else '' end +ADD3+case when len(add4)> 0 then ',' else '' end +ADD4+case when len(City_Code)> 0 then ',' else '' end +City_Code +case when len(STATE)> 0 then ',' else '' end  +STATE) from tspl_location_master where " + stLoc + "=" + strCmpr + " )  as address " &
                        "  , (case TSPL_BANK_MASTER .Bank_type when 'B'then 'Bank Transfer'when 'C'then 'Cash Transfer' when 'P'then 'Petty Cash' when 'O' then 'Other' else'' end ) as  BankType   from TSPL_BANK_TRANSFER  left outer join TSPL_GL_ACCOUNTS on TSPL_BANK_TRANSFER .To_Bank_Acc_No  =TSPL_GL_ACCOUNTS .Account_Code left outer join TSPL_GL_ACCOUNTS as GlAcc  on TSPL_BANK_TRANSFER .From_Bank_Acc_No =GlAcc .Account_Code   " &
                        "  left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER .Comp_Code = TSPL_BANK_TRANSFER .Comp_Code left outer join TSPL_BANK_MASTER on TSPL_BANK_TRANSFER .From_Bank_Code =TSPL_BANK_MASTER .BANK_CODE   where Transfer_No ='" + StrCode + "'"
            End If

            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                stLoc = " location_code "
                strCmpr = "GlAcc"
                query = "   select Transfer_No as TransferNum ,From_Bank_Acc_No as FromBankCode,From_Bank_Name as FrombankName,To_Bank_Acc_No  as ToAccCode ,To_Bank_Name as ToBankName, tspl_bank_transfer.Cheque_No,tspl_bank_transfer.Description + CASE WHEN LEN(TSPL_BANK_TRANSFER .Cheque_No)>0 THEN '/ CHEQUE NO :'+TSPL_BANK_TRANSFER .Cheque_No  ELSE '' END as Description," &
                         "  Deposit_Amount as Amount,TSPL_GL_ACCOUNTS .Account_Seg_Code7 as Location,GlAcc .Account_Seg_Code7 as FromLocation,Transfer_Posting_Date as VoucherDate ," &
                         "  TSPL_BANK_TRANSFER .Comp_Code as CompCode ,TSPL_COMPANY_MASTER.Comp_Name as CompName, TSPL_COMPANY_MASTER.Logo_Img as Image1" &
                         "  , TSPL_COMPANY_MASTER.Logo_Img2 as Image2,(select max(ADD1 + case when len(add2)> 0 then ',' else '' end + ADD2 +case when len(add3)> 0 then ','else '' end +ADD3+case when len(add4)> 0 then ',' else '' end +ADD4+case when len(City_Code)> 0 then ',' else '' end +City_Code +case when len(STATE)> 0 then ',' else '' end  +tspl_state_master.STATE_NAME) from tspl_location_master   left outer join TSPL_STATE_MASTER on tspl_state_master.STATE_CODE=TSPL_LOCATION_MASTER.State where " + stLoc + "=" + strCmpr + ".Account_Seg_Code7)  as address " &
                         "  , (case TSPL_BANK_MASTER .Bank_type when 'B'then 'Bank Transfer'when 'C'then 'Cash Transfer' when 'P'then 'Petty Cash' when 'O' then 'Other' else'' end ) as  BankType , TSPL_USER_MASTER_for_CreatedName.User_Name as  Created_By,case when TSPL_BANK_TRANSFER.Post ='P' then  TSPL_USER_MASTER_for_ModifyName.User_Name else '' end  as Modify_By   from TSPL_BANK_TRANSFER  left outer join TSPL_GL_ACCOUNTS on TSPL_BANK_TRANSFER .To_Bank_Acc_No  =TSPL_GL_ACCOUNTS .Account_Code left outer join TSPL_GL_ACCOUNTS as GlAcc  on TSPL_BANK_TRANSFER .From_Bank_Acc_No =GlAcc .Account_Code   " &
                         "  left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER .Comp_Code = TSPL_BANK_TRANSFER .Comp_Code left outer join TSPL_BANK_MASTER on TSPL_BANK_TRANSFER .From_Bank_Code =TSPL_BANK_MASTER .BANK_CODE left outer join TSPL_USER_MASTER as TSPL_USER_MASTER_for_CreatedName on TSPL_USER_MASTER_for_CreatedName.User_Code =TSPL_BANK_TRANSFER.Created_By left outer join TSPL_USER_MASTER as TSPL_USER_MASTER_for_ModifyName on TSPL_USER_MASTER_for_ModifyName.User_Code =TSPL_BANK_TRANSFER.Modify_By   where Transfer_No ='" + StrCode + "'"
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(query)
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.CommonServices, dt, "CashVoucher", "Cash Voucher Report")
            frmCRV = Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub RadGroupBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadGroupBox2.Click

    End Sub

    Private Sub btnBlankForReCreateJE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBlankForReCreateJE.Click
        Dim qry As String = "insert into TEMP_Delete_BankTransfer select Transfer_No from TSPL_BANK_TRANSFER where  Post='P' and Transfer_No not in(select TransferNo from TEMP_Delete_BankTransfer union all select TransferNo from TEMP_Created_BankTransfer)"
        clsDBFuncationality.ExecuteNonQuery(qry)
        common.clsCommon.MyMessageBoxShow(Me, "Task Completed", Me.Text)
    End Sub

    Private Sub btnReCreateJE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReCreateJE.Click
        Dim qry As String = "select TransferNo from TEMP_Delete_BankTransfer where TransferNo not in ( select TransferNo from TEMP_Created_BankTransfer)"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            If common.clsCommon.MyMessageBoxShow("Recreate " + clsCommon.myCstr(dt.Rows.Count) + " Bank Transfer" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                For Each dr As DataRow In dt.Rows
                    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                    Try
                        Dim strDocNo As String = clsCommon.myCstr(dr("TransferNo"))

                        qry = "delete from TSPL_JOURNAL_DETAILS where TSPL_JOURNAL_DETAILS.Voucher_No in (select Voucher_No from TSPL_JOURNAL_MASTER where TSPL_JOURNAL_MASTER.Source_Code='BK-TF' and TSPL_JOURNAL_MASTER.Source_Doc_No='" + strDocNo + "')"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)

                        qry = "delete from TSPL_JOURNAL_MASTER where TSPL_JOURNAL_MASTER.Source_Code='BK-TF' and TSPL_JOURNAL_MASTER.Source_Doc_No='" + strDocNo + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)

                        qry = "select From_Bank_Acc_No,Transfer_Amount,To_Bank_Acc_No,Transfer_No,Deposit_Amount,Transfer_Posting_Date,Description,Transfer_No,Post from TSPL_BANK_TRANSFER where Transfer_No='" + strDocNo + "'"
                        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        If (clsBankTrasnferNew.CreateJournalEntry(strDocNo, dt1, trans)) Then
                            qry = "insert into TEMP_Created_BankTransfer values('" + strDocNo + "')"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)

                            trans.Commit()


                        Else
                            Throw New Exception("Not Posted Transfer No " + clsCommon.myCstr(dr("TransferNo")))
                        End If


                    Catch ex As Exception
                        trans.Rollback()
                        common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                    End Try
                Next
                common.clsCommon.MyMessageBoxShow(Me, "Task Completed", Me.Text)
            End If
        End If
    End Sub

    Private Sub fndPayType__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndPayType._MYValidating
        Dim strbankcode As String
        If Not String.IsNullOrEmpty(connectSql.RunScalar("select bank_type from tspl_bank_master where bank_code = '" + Txt_frombankCode.Value + "'")) Then
            strbankcode = connectSql.RunScalar("select bank_type from tspl_bank_master where bank_code = '" + Txt_frombankCode.Value + "'")
            If strbankcode.Trim() = "C" Then
                Dim Qry1 As String = "select Payment_Code as [PaymentMode], Payment_Desc as [Description], Payment_Type  as [PaymentType]  from TSPL_PAYMENT_CODE "
                fndPayType.Value = clsCommon.ShowSelectForm("PaymentCode_Selector1", Qry1, "PaymentMode", "PAYMENT_TYPE = 'CASH'", fndPayType.Value, "PaymentMode", isButtonClicked)
                chkCheckPrint.Visible = False
            ElseIf strbankcode.Trim() = "P" Then
                Dim Qry1 As String = "select Payment_Code as [PaymentMode], Payment_Desc as [Description], Payment_Type  as [PaymentType]  from TSPL_PAYMENT_CODE "
                fndPayType.Value = clsCommon.ShowSelectForm("PaymentCode_Selector2", Qry1, "PaymentMode", "PAYMENT_TYPE = 'Petty Cash'", fndPayType.Value, "PaymentMode", isButtonClicked)
                chkCheckPrint.Visible = True
            ElseIf strbankcode = "B" Then
                Dim Qry1 As String = "select Payment_Code as [PaymentMode], Payment_Desc as [Description], Payment_Type  as [PaymentType]  from TSPL_PAYMENT_CODE "
                fndPayType.Value = clsCommon.ShowSelectForm("PaymentCode_Selector3", Qry1, "PaymentMode", "PAYMENT_TYPE IN ('Cheque', 'Other')", fndPayType.Value, "PaymentMode", isButtonClicked)
                If clsCommon.CompairString("Cheque", fndPayType.Value) = CompairStringResult.Equal Then
                    chkCheckPrint.Visible = True
                Else
                    chkCheckPrint.Visible = False
                End If

            ElseIf strbankcode = "S" Then
                Dim Qry1 As String = "select Payment_Code as [PaymentMode], Payment_Desc as [Description], Payment_Type  as [PaymentType]  from TSPL_PAYMENT_CODE "
                fndPayType.Value = clsCommon.ShowSelectForm("PaymentCode_Selector4", Qry1, "PaymentMode", "PAYMENT_TYPE = 'Cheque' or PAYMENT_TYPE = 'Cash'", fndPayType.Value, "PaymentMode", isButtonClicked)
                If clsCommon.CompairString("Cheque", fndPayType.Value) = CompairStringResult.Equal Then
                    chkCheckPrint.Visible = True
                Else
                    chkCheckPrint.Visible = False
                End If
            Else
                Dim Qry1 As String = "select Payment_Code as [PaymentMode], Payment_Desc as [Description], Payment_Type  as [PaymentType]  from TSPL_PAYMENT_CODE "
                fndPayType.Value = clsCommon.ShowSelectForm("PaymentCode_Selector5", Qry1, "PaymentMode", "PAYMENT_TYPE = 'Other'", fndPayType.Value, "PaymentMode", isButtonClicked)
                chkCheckPrint.Visible = False
            End If
        End If
        txtchangedPaymentMode()
    End Sub

    Private Sub txtchangedPaymentMode(Optional ByVal trans As SqlTransaction = Nothing)
        Dim strcheckcode As String = connectSql.RunScalar(trans, "select Payment_Type  from TSPL_PAYMENT_CODE  where Payment_Code ='" + fndPayType.Value + "'")
        If Not String.IsNullOrEmpty(strcheckcode) Then
            If strcheckcode.Trim() = "Cheque" Then
                txtchkno.Text = ""
                txtchkno.Enabled = True
                txtchkdate.Enabled = True
                Lblchkno.Enabled = True
            Else
                txtchkno.Text = ""
                txtchkno.Enabled = False
                txtchkdate.Enabled = False
                Lblchkno.Enabled = False
            End If
        End If
    End Sub

    Private Sub rmiExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiExport.Click
        Export()
    End Sub

    Public Sub Export()
        Try
            Dim qry As String = "Select Transfer_No as [Transfer No], convert(varchar,Transfer_Date,103) as [Date], Description, Reference,case when Transaction_Type='B' then 'Both' when Transaction_Type='W' then 'Withdrawal' when Transaction_Type='R' then 'Receipt' end as [Transaction Type], From_Bank_Code as [From Bank], "
            qry += " From_Bank_Name as [From Bank Name], To_Bank_Code as [To Bank], To_Bank_Name as [To Bank Name], "
            qry += " Transfer_Amount as [Amount], Payment_Mode as [Payment Mode], Cheque_No as [Cheque No], convert(varchar,Cheque_Date,103) as [Cheque Date], "
            qry += " CASE WHEN Post='P' Then 'Posted' Else 'Pending' End as [Status] from TSPL_BANK_TRANSFER"
            transportSql.ExporttoExcel(qry, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub rmiIMport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiIMport.Click
        Import()
    End Sub

    Public Sub Import()
        Dim gv As New RadGridView()
        Dim LineNo As String = ""
        'Dim Qry As String
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Transfer No", "Date", "Description", "Reference", "Transaction Type", "From Bank", "From Bank Name", "To Bank", "To Bank Name", "Amount", "Payment Mode", "Cheque No", "Cheque Date", "Status") Then
            Dim tran As SqlTransaction = Nothing
            Try
                tran = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    LineNo = clsCommon.myCstr(grow.Index + 2)

                    '--------------Transfer Date----------------------------
                    Dim TransferDate As Date
                    Try
                        TransferDate = clsCommon.myCDate(grow.Cells("Date").Value)
                    Catch ex As Exception
                        Throw New Exception("Date is Not in correct format ")
                    End Try

                    '-----------------------------------------------

                    '---------------Description---------------------
                    Dim Desc As String = clsCommon.myCstr(grow.Cells("Description").Value)
                    If clsCommon.myLen(Desc) > 100 Then
                        Throw New Exception("Length of Description is greater than 100. ")
                    End If
                    '-----------------------------------------------

                    '---------------Reference---------------------
                    Dim Reference As String = clsCommon.myCstr(grow.Cells("Reference").Value)
                    If clsCommon.myLen(Reference) > 100 Then
                        Throw New Exception("Length of Reference is greater than 100. ")
                    End If
                    '-----------------------------------------------

                    '------------From Bank Code---------------------
                    Dim FromBank As String = clsCommon.myCstr(grow.Cells("From Bank").Value)
                    If clsCommon.myLen(FromBank) > 0 Then
                        FromBank = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select BANK_CODE from TSPL_BANK_MASTER Where BANK_CODE='" + FromBank + "'", tran))
                        If FromBank = "" Then
                            Throw New Exception("'From Bank' does not exist. ")
                        End If
                    Else
                        Throw New Exception("Insert 'From Bank'.")
                    End If
                    Dim fromBankName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select DESCRIPTION from  TSPL_BANK_Master Where BANK_CODE='" + FromBank + "'", tran))
                    Dim FromBankAccNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select BANKACC from  TSPL_BANK_MASTER Where BANK_CODE='" + FromBank + "'", tran))
                    '-----------------------------------------------

                    '------------To Bank Code---------------------
                    Dim ToBank As String = clsCommon.myCstr(grow.Cells("To Bank").Value)
                    If clsCommon.myLen(ToBank) > 0 Then
                        ToBank = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select BANK_CODE from TSPL_BANK_MASTER Where BANK_CODE='" + ToBank + "'", tran))
                        If ToBank = "" Then
                            Throw New Exception(" 'To Bank' does not exist. ")
                        End If
                    Else
                        Throw New Exception("Insert 'To Bank'.")
                    End If
                    Dim ToBankName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select DESCRIPTION from  TSPL_BANK_Master Where BANK_CODE='" + ToBank + "'", tran))
                    Dim ToBankAccNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select BANKACC from  TSPL_BANK_MASTER Where BANK_CODE='" + ToBank + "'", tran))
                    '-----------------------------------------------

                    '----------Amount-------------------------------
                    Dim trnsAmount As Double = clsCommon.myCdbl(grow.Cells("Amount").Value)
                    If trnsAmount <= 0 Then
                        Throw New Exception("Insert Transfer Amount")
                    End If
                    '-----------------------------------------------

                    '================Transfer No===============================
                    Dim TransferNo = clsCommon.myCstr(grow.Cells("Transfer No").Value)
                    Dim IsNewEntry As Boolean = False
                    If clsCommon.myLen(TransferNo) > 0 Then
                        Dim Post As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Post From TSPL_BANK_TRANSFER WHERE Transfer_No='" + TransferNo + "'", tran))
                        If clsCommon.CompairString(Post, "P") = CompairStringResult.Equal Then
                            Continue For
                        End If
                        Dim Counter As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Count(*) From TSPL_BANK_TRANSFER Where Transfer_No='" + TransferNo + "'", tran))
                        If Counter <= 0 Then
                            If common.clsCommon.MyMessageBoxShow("Transfer does not exist a line " + LineNo + "" + Environment.NewLine + "Do you want to create new.?", "", MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes Then
                                IsNewEntry = True
                                TransferNo = funautogenerateno(tran)
                            Else
                                tran.Rollback()
                                Exit Sub
                            End If
                        Else
                            IsNewEntry = False
                        End If
                    Else
                        IsNewEntry = True
                        Dim LocSegmentCode As String = clsDBFuncationality.getSingleValue("Select RIGHT(BANKACC, 3) from TSPL_BANK_MASTER  Where BANK_CODE='" + FromBank + "'", tran)
                        TransferNo = clsERPFuncationality.GetNextCode(tran, TransferDate, clsDocType.ContraVoucher, "", LocSegmentCode)
                        'TransferNo = funautogenerateno(tran)
                    End If
                    '==========================================================

                    

                    Dim ChequeNo As String
                    Dim ChequeDate As String
                    Dim PaymentCode As String
                    '----------------Cheque No/ CHeque Date=------------------
                    Dim BankType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Bank_type from TSPL_BANK_MASTER Where BANK_CODE='" + FromBank + "'", tran))
                    If clsCommon.CompairString(BankType, "B") = CompairStringResult.Equal Then
                        PaymentCode = "Cheque"
                        ChequeNo = clsCommon.myCstr(grow.Cells("Cheque No").Value)
                        '-------------------------
                        If clsCommon.myLen(ChequeNo) <= 0 Then
                            ' Throw New Exception("Insert Cheque No.")
                        ElseIf clsCommon.myLen(ChequeNo) > 6 Then
                            ' Throw New Exception("The length of Cheque No is greater than 6 .")
                        Else
                            Dim ChkNo As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Count(*) From TSPL_BANK_TRANSFER Where Cheque_No='" + ChequeNo + "' AND Transfer_No <> '" + TransferNo + "' ", tran))
                            If ChkNo > 0 Then
                                Throw New Exception("The Cheque No is already in use .")
                            End If
                        End If
                        '--------------------------
                        ChequeDate = clsCommon.myCstr(grow.Cells("Cheque Date").Value)
                        If clsCommon.myLen(ChequeDate) <= 0 Then
                            ' Throw New Exception("Insert Cheque Date.")
                        ElseIf clsCommon.myLen(ChequeDate) < 10 Then
                            ' Throw New Exception("Cheque Date has some incorrect values .")
                        End If
                        ChequeDate = clsCommon.GetPrintDate(ChequeDate, "dd/MMM/yyyy")
                        '--------------------------
                    Else
                        PaymentCode = "Cash"
                        ChequeNo = ""
                        ChequeDate = Nothing
                    End If
                    '--------------------------Transaction Type-------------------------------
                    Dim TransactionType As String = clsCommon.myCstr(grow.Cells("Transaction Type").Value)
                    If clsCommon.CompairString(TransactionType, "Both") = CompairStringResult.Equal Then
                        TransactionType = "B"
                    ElseIf clsCommon.CompairString(TransactionType, "Receipt") = CompairStringResult.Equal Then
                        TransactionType = "R"
                    ElseIf clsCommon.CompairString(TransactionType, "Withdrawal") = CompairStringResult.Equal Then
                        TransactionType = "W"
                    End If

                    '--------------------------------------------------------
                    If IsNewEntry Then
                        clsDBFuncationality.SaveAStorePorcedure(tran, "sp_tspl_banktransfer_insert", New SqlParameter("@Transfer_No", TransferNo), New SqlParameter("@Transfer_Date", clsCommon.GetPrintDate(TransferDate, "dd/MMM/yyyy")), New SqlParameter("@Transfer_Posting_Date", clsCommon.GetPrintDate(TransferDate, "dd/MMM/yyyy")), New SqlParameter("@Description", Desc), New SqlParameter("@Reference", Reference), New SqlParameter("@TransType", TransactionType), New SqlParameter("@From_Bank_Code", FromBank), New SqlParameter("@From_Bank_Name", fromBankName), New SqlParameter("@From_Bank_Acc_No", FromBankAccNo), New SqlParameter("@Transfer_Amount", clsCommon.myCstr(trnsAmount)), New SqlParameter("@From_Bank_GL_Acc", ""), New SqlParameter("@From_Bank_GLAcc_Desc", ""), New SqlParameter("@From_Bank_GL_Amount", 0), New SqlParameter("@To_Bank_Code", ToBank), New SqlParameter("@To_Bank_Name", ToBankName), New SqlParameter("@To_Bank_Acc_No", ToBankAccNo), New SqlParameter("@Deposit_Amount", clsCommon.myCstr(trnsAmount)), New SqlParameter("@To_Bank_GL_Acc", ""), New SqlParameter("@To_Bank_GLAcc_Desc", ""), New SqlParameter("@To_Bank_GL_Amount", 0), New SqlParameter("@Post", "N"), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(tran))), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(tran))), New SqlParameter("@comp_code", companyCode), New SqlParameter("@Cheque_No", ChequeNo), New SqlParameter("@Cheque_Date", ChequeDate), New SqlParameter("@Payment_Mode", PaymentCode))
                    Else
                        clsDBFuncationality.SaveAStorePorcedure(tran, "sp_tspl_banktransfer_update", New SqlParameter("@Transfer_No", TransferNo), New SqlParameter("@Transfer_Date", clsCommon.GetPrintDate(TransferDate, "dd/MMM/yyyy")), New SqlParameter("@Transfer_Posting_Date", clsCommon.GetPrintDate(TransferDate, "dd/MMM/yyyy")), New SqlParameter("@Description", Desc), New SqlParameter("@Reference", Reference), New SqlParameter("@TransType", TransactionType), New SqlParameter("@From_Bank_Code", FromBank), New SqlParameter("@From_Bank_Name", fromBankName), New SqlParameter("@From_Bank_Acc_No", FromBankAccNo), New SqlParameter("@Transfer_Amount", clsCommon.myCstr(trnsAmount)), New SqlParameter("@From_Bank_GL_Acc", ""), New SqlParameter("@From_Bank_GLAcc_Desc", ""), New SqlParameter("@From_Bank_GL_Amount", 0), New SqlParameter("@To_Bank_Code", ToBank), New SqlParameter("@To_Bank_Name", ToBankName), New SqlParameter("@To_Bank_Acc_No", ToBankAccNo), New SqlParameter("@Deposit_Amount", clsCommon.myCstr(trnsAmount)), New SqlParameter("@To_Bank_GL_Acc", ""), New SqlParameter("@To_Bank_GLAcc_Desc", ""), New SqlParameter("@To_Bank_GL_Amount", 0), New SqlParameter("@Post", "N"), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(tran))), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(tran))), New SqlParameter("@comp_code", companyCode), New SqlParameter("@Cheque_No", ChequeNo), New SqlParameter("@Cheque_Date", ChequeDate), New SqlParameter("@Payment_Mode", PaymentCode))
                    End If
                Next

                tran.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                tran.Rollback()
                clsCommon.ProgressBarHide()
                Dim Msg As String = "Error at line " + LineNo + "." + Environment.NewLine
                Msg += "" + ex.Message + "."
                clsCommon.MyMessageBoxShow(Msg)
            End Try
        End If
        Me.Controls.Remove(gv)

    End Sub

    Private Sub btnReverseAndRecreate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReverseAndRecreate.Click
        If clsCommon.myLen(Fnd_Transfernumber.Value) > 0 Then
            If common.clsCommon.MyMessageBoxShow(Me, "Reverse and Recreate Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Try
                    Dim qry As String = "select Post  from TSPL_BANK_TRANSFER where Transfer_No='" + Fnd_Transfernumber.Value + "'"
                    If Not clsCommon.CompairString(clsDBFuncationality.getSingleValue(qry, trans), "p") = CompairStringResult.Equal Then
                        Throw New Exception("Transaction should be posted for Reverse and unpost")
                    End If
                    Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='BK-TF' and Source_Doc_No='" + Fnd_Transfernumber.Value + "'", trans)

                    If clsCommon.myLen(VoucherNo) > 0 Then
                        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, VoucherNo, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
                        qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    End If


                    qry = "Update TSPL_BANK_TRANSFER set Post = 'n' where Transfer_No='" + Fnd_Transfernumber.Value + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, Fnd_Transfernumber.Value, "TSPL_BANK_TRANSFER", "Transfer_No", trans)
                    trans.Commit()

                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    funfill()
                Catch ex As Exception
                    trans.Rollback()
                    common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                End Try
            End If
        End If
    End Sub


    Private Sub CmbTransType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles CmbTransType.SelectedIndexChanged
        If IsLoadTransType = True Then
            If clsCommon.CompairString(CmbTransType.SelectedValue, "R") = CompairStringResult.Equal Then
                txtwithdrawal.Value = ""
                LblWithdrawal.Visible = True
                txtwithdrawal.Visible = True
                RadGroupBox2.Enabled = False
                RadGroupBox3.Enabled = False
            Else
                LblWithdrawal.Visible = False
                txtwithdrawal.Visible = False
                RadGroupBox2.Enabled = True
                RadGroupBox3.Enabled = True
                Txt_frombankCode.Value = ""
                txt_frombankname.Text = ""
                txt_frombankaccount.Text = ""
                txt_transferamount.Text = ""
                txtbnkaccnumber.Text = ""
                Txt_toBankCode.Value = ""
                txt_tobankname.Text = ""
                txt_depositamount.Text = ""
                fndPayType.Value = ""
                txtchkno.Text = ""
                txt_tobankaccount.Text = ""
            End If
        End If

    End Sub


    Private Sub txtwithdrawal__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtwithdrawal._MYValidating
        'Dim bankcode As String = bankPermission()
        Dim strWhrclas As String = ""
        strWhrclas = " TSPL_BANK_TRANSFER.Transaction_Type ='W' AND Transfer_No NOT IN (SELECT ISNULL(Against_Withdrawal_No,'') FROM TSPL_BANK_TRANSFER)"
        Dim qry As String = "SELECT Transfer_No AS [Code],Transfer_Date AS [Transfer Date],Description ,Payment_Mode AS [Payment Mode], Transaction_Type AS [TransactionType], From_Bank_Code  AS [From Bank Code],From_Bank_Name AS [From Bank Name] ,From_Bank_Acc_No AS [From Bank Acc No],Transfer_Amount AS [Transfer Amount],To_Bank_Code AS [To Bank Code],To_Bank_Name AS [To Bank Name],To_Bank_Acc_No AS  [To Bank Acc No],Deposit_Amount AS [Deposit Amount],Post FROM TSPL_BANK_TRANSFER"
        txtwithdrawal.Value = clsCommon.ShowSelectForm("FrmBankW", qry, "Code", strWhrclas, txtwithdrawal.Value, "Code", isButtonClicked)
        Try
            Dim WithdrawalData As String = ""
            Dim strSql As String = "SELECT Transfer_No FROM TSPL_BANK_TRANSFER where [Transfer_No] ='" + txtwithdrawal.Value + "'"

            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(strSql)
            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                For Each dr As DataRow In dt1.Rows
                    WithdrawalData = dr(0).ToString()
                Next
            End If
            If WithdrawalData <> "" Then
                FillWithdrawalDetails()
            Else
                Txt_frombankCode.Value = ""
                txt_frombankname.Text = ""
                txt_frombankaccount.Text = ""
                txt_transferamount.Text = ""
                txtbnkaccnumber.Text = ""
                Txt_toBankCode.Value = ""
                txt_tobankname.Text = ""
                txt_depositamount.Text = ""
                fndPayType.Value = ""
                txtchkno.Text = ""
                txt_tobankaccount.Text = ""
                btn_save.Enabled = True
                btn_save.Text = "Save"
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Public Sub FillWithdrawalDetails()
        Try
            If clsCommon.myLen(txtwithdrawal.Value) > 0 Then
                Dim strSql As String = "select * from TSPL_BANK_TRANSFER where Transfer_No='" + txtwithdrawal.Value + "'"

                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(strSql)
                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    For Each dr As DataRow In dt1.Rows
                        txt_frombankname.Text = clsCommon.myCstr(dr("From_Bank_Name"))
                        txt_frombankaccount.Text = clsCommon.myCstr(dr("From_Bank_Acc_No"))
                        txtbnkaccnumber.Text = clsCommon.myCstr(dr("TO_BANKACCNUMBER"))
                        Txt_frombankCode.Value = clsCommon.myCstr(dr("From_Bank_Code"))
                        fndPayType.Value = clsCommon.myCstr(dr("Payment_Mode"))
                        txt_transferamount.Text = clsCommon.myCstr(dr("Transfer_Amount"))
                        txtchkno.Text = clsCommon.myCstr(dr("Cheque_No"))
                        Txt_toBankCode.Value = clsCommon.myCstr(dr("To_Bank_Code"))
                        txt_tobankname.Text = clsCommon.myCstr(dr("To_Bank_Name"))
                        txt_tobankaccount.Text = clsCommon.myCstr(dr("To_Bank_Acc_No"))
                        txttranbnkaccno.Text = clsCommon.myCstr(dr("From_BANKACCNUMBER"))
                        txt_depositamount.Text = clsCommon.myCstr(dr("Deposit_Amount"))
                        txt_description.Text = clsCommon.myCstr(dr("Description"))
                        txtBankChargesAmt.Value = 0
                    Next
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub btnVoidCheck_Click(sender As Object, e As EventArgs) Handles btnVoidCheck.Click
        If clsCommon.myLen(Fnd_Transfernumber.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Select document no to void check.", Me.Text)
            Exit Sub
        End If
        Dim obj As New clsPaymentHeader()
        obj = clsPaymentHeader.GetData(Me.Fnd_Transfernumber.Value, NavigatorType.Current)
        If clsCommon.myLen(obj.Bank_Code) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Bank Code not found for selected document.", Me.Text)
            Exit Sub
            'ElseIf clsCommon.myLen(obj.CHECK_CODE) <= 0 Then
            '    clsCommon.MyMessageBoxShow("Check Code not found for selected document.")
            '    Exit Sub
        End If
        If clsPrintCheck.VoidCheck(obj.Bank_Code, obj.CHECK_CODE, "Bank Transfer", obj.Payment_No) Then
            clsCommon.MyMessageBoxShow(Me, "done successfully", Me.Text)
        End If
        ''or  clscommon.myLen(obj.Bank_Code )<=0
    End Sub

    Private Sub btnPrintCheck_Click(sender As Object, e As EventArgs) Handles btnPrintCheck.Click
        If chkCheckPrint.Checked Then
            '' get data of payment entry
            Dim obj As New clsBankTransfer
            obj = clsBankTransfer.GetData(Me.Fnd_Transfernumber.Value, NavigatorType.Current)
            Dim frm As New frmPrintCheck
            frmPrintCheck.Manual_Print = 0
            frmPrintCheck.Manual_Print = 0
            frmPrintCheck.BankCode = obj.From_Bank_Code
            frmPrintCheck.CheckCode = obj.Check_Code
            frm.lblCheckDesc.Text = connectSql.RunScalar("select description from TSPL_BANK_CHECK_PRINTING where CHECK_CODE = '" + obj.Check_Code + "'")
            frmPrintCheck.DocumentType = "Bank Transfer"
            frmPrintCheck.DocumentCode = obj.Transfer_No
            '' Anubhooti 22-July-2014 BM00000003161
            'frm.chkAccPayee.Checked = IIf(obj.Account_Payee = 1, True, False)
            If clsCommon.myLen(obj.Check_Code) > 0 Then
                frm.btnPrint.Text = "RePrint"
            End If
            frm.Show()
        Else
            '' get data of payment entry

            Dim obj As New clsBankTransfer()
            obj = clsBankTransfer.GetData(Me.Fnd_Transfernumber.Value, NavigatorType.Current)
            If clsPrintCheck.CheckforVoidCheck(obj.From_Bank_Code, obj.Cheque_No) Then
                clsCommon.MyMessageBoxShow("Please enter valid Cheque No, Entered Cheque No -" & obj.Cheque_No & " is Void.")
                Exit Sub
            End If
            Dim frm As New frmPrintCheck
            frmPrintCheck.BankCode = obj.From_Bank_Code
            frmPrintCheck.CheckCode = obj.Check_Code
            frmPrintCheck.fndCheckCode.Enabled = False
            'frm.lblCheckDesc.Text = connectSql.RunScalar("select description from TSPL_BANK_CHECK_PRINTING where CHECK_CODE = '" + obj.CHECK_CODE + "'")
            frmPrintCheck.DocumentType = "Bank Transfer"
            frmPrintCheck.DocumentCode = obj.Transfer_No
            '' Anubhooti 22-July-2014 BM00000003161
            'frm.chkAccPayee.Checked = IIf(obj.Account_Payee = 1, True, False)
            frmPrintCheck.Manual_Print = 1
            frmPrintCheck.Manual_Print = 1
            frmPrintCheck.Manual_Check_No = txtchkno.Text
            frmPrintCheck.Manual_Check_No = txtchkno.Text
            'If clsCommon.myLen(obj.CHECK_CODE) > 0 Then
            '    frm.btnPrint.Text = "RePrint"
            'End If
            frm.Show()
        End If

    End Sub

    Private Sub chkCheckPrint_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkCheckPrint.ToggleStateChanged
        If Me.chkCheckPrint.Checked Then
            Me.txtchkno.Enabled = False
            'Me.txtChequeNo.Text = ""
        Else
            Me.txtchkno.Enabled = True
        End If
    End Sub

    Private Sub dtpFromDate_Validating(sender As Object, e As ComponentModel.CancelEventArgs) Handles dtpFromDate.Validating
        SetToDate()
    End Sub

    Private Sub dtpFromDate_Leave(sender As Object, e As EventArgs) Handles dtpFromDate.Leave
        SetToDate()
    End Sub

    Sub SetToDate()
        If Not isLoad Then
            Dim PaymentCycleType As String = ""
            Dim PaymentCycleValue As Integer = 0
            ' If Not isLoad Then
            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
            Else
                clsCommon.MyMessageBoxShow(Me, "Please select the MCC first", Me.Text)
                Exit Sub
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(" select top 1 TSPL_MCC_MASTER.Payment_Cycle,TSPL_PAYMENT_CYCLE_MASTER.PC_TYPE,TSPL_PAYMENT_CYCLE_MASTER.PC_VALUE  from TSPL_MCC_MASTER left outer join TSPL_PAYMENT_CYCLE_MASTER on TSPL_PAYMENT_CYCLE_MASTER.PC_CODE=TSPL_MCC_MASTER.Payment_Cycle   where TSPL_MCC_MASTER.MCC_Code  in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ") ")
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Payment Cycle found on current MCC/Location", Me.Text)
                Exit Sub
            End If
            PaymentCycleType = clsCommon.myCstr(dt.Rows(0)("PC_TYPE"))
            PaymentCycleValue = clsCommon.myCdbl(dt.Rows(0)("PC_VALUE"))
            '=======================
            Dim totalCountMCC As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Count(*) from TSPL_MCC_MASTER where TSPL_MCC_MASTER.MCC_Code  in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ") "))
            Dim NoOfMCCExistSamePaymentCycle As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Count(*) from TSPL_MCC_MASTER where TSPL_MCC_MASTER.MCC_Code  in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ") and TSPL_MCC_MASTER.Payment_Cycle = '" + clsCommon.myCstr(dt.Rows(0)("Payment_Cycle")) + "'  "))
            If totalCountMCC = NoOfMCCExistSamePaymentCycle Then
            Else
                txtMCC.arrValueMember = Nothing
                clsCommon.MyMessageBoxShow(Me, "Payment Cycle of selected MCC should be same", Me.Text)
                Exit Sub
            End If
            '=======================
            Dim dtCurr As DateTime = clsCommon.GETSERVERDATE()
            If clsCommon.CompairString(PaymentCycleType, "Day") = CompairStringResult.Equal Then
                If dtpFromDate.Value.Day Mod PaymentCycleValue <> 1 And (Not PaymentCycleValue = 1) Then
                    clsCommon.MyMessageBoxShow("Date can only be first day of month or at interval of " & PaymentCycleValue & " Day, Because MCC has payment Cycle of " & PaymentCycleValue & " Day ")
                    dtpFromDate.Value = New Date(dtCurr.Year, dtCurr.Month, 1)
                    dtpToDate.Value = dtpFromDate.Value
                    Exit Sub
                End If
                dtpToDate.Value = dtpFromDate.Value.AddDays(PaymentCycleValue - 1)

                If dtpFromDate.Value.Month <> dtpToDate.Value.Month Then
                    dtpToDate.Value = New Date(dtpFromDate.Value.Year, dtpFromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
                End If
                Dim dtNxtPay As DateTime = dtpToDate.Value.AddDays(Math.Ceiling(PaymentCycleValue / 2.0))
                If dtpFromDate.Value.Month <> dtNxtPay.Month Then
                    dtpToDate.Value = New Date(dtpFromDate.Value.Year, dtpFromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
                End If
            ElseIf clsCommon.CompairString(PaymentCycleType, "Month") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsCommon.GetPrintDate(dtpFromDate.Value, "dd")) <> 1 Then
                    clsCommon.MyMessageBoxShow("Date can only be first day of month, Because MCC has payment Cycle of Month Type")
                    dtpFromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    dtpToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    Exit Sub
                End If
                dtpToDate.Value = DateAdd(DateInterval.Month, PaymentCycleValue, dtpFromDate.Value)
            ElseIf clsCommon.CompairString(PaymentCycleType, "Year") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsCommon.GetPrintDate(dtpFromDate.Value, "dd")) <> 1 Then
                    clsCommon.MyMessageBoxShow("Date can only be first day of month, Because MCC has payment Cycle of Year Type")
                    dtpFromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    dtpToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    Exit Sub
                End If
                dtpToDate.Value = DateAdd(DateInterval.Year, PaymentCycleValue, dtpFromDate.Value)
            ElseIf clsCommon.CompairString(PaymentCycleType, "Week") = CompairStringResult.Equal Then
                Dim today As Date = dtpFromDate.Value
                Dim dayDiff As Integer = today.DayOfWeek - IIf(PaymentCycleValue = 1, DayOfWeek.Sunday, IIf(PaymentCycleValue = 2, DayOfWeek.Monday, IIf(PaymentCycleValue = 3, DayOfWeek.Tuesday, IIf(PaymentCycleValue = 4, DayOfWeek.Wednesday, IIf(PaymentCycleValue = 5, DayOfWeek.Thursday, IIf(PaymentCycleValue = 6, DayOfWeek.Friday, DayOfWeek.Saturday))))))
                dtpFromDate.Value = today.AddDays(-dayDiff)
                dtpToDate.Value = dtpFromDate.Value.AddDays(6)
            End If
            ' End If
        End If
    End Sub

    Private Sub txtMCC__My_Click(sender As Object, e As EventArgs) Handles txtMCC._My_Click
        Try
            Dim qry As String = "select MCC_Code,MCC_NAME,TSPL_MCC_MASTER.plant_code as [Plant Code],tspl_location_master.location_desc as [Plant Name],tspl_mcc_master.Payment_Cycle as [Payment Cycle]  from TSPL_MCC_MASTER left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.plant_code " ' where tspl_mcc_master.mcc_Code in (" & StrPermission & ")
            txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("PCUMCC", qry, "MCC_Code", "MCC_NAME", txtMCC.arrValueMember, txtMCC.arrDispalyMember)
            SetToDate()
        Catch ex As Exception

        End Try

    End Sub

    ' Ticket No : TEC/07/05/19-000477 By Prabhakar
    Private Sub btnOpenBankCashBook_Click(sender As Object, e As EventArgs) Handles btnOpenBankCashBook.Click
        clsOpenBankCashBook.ShowBankCashBookDatails(Fnd_Transfernumber.Value)
    End Sub
End Class
