'--21/12/2012--Updation By --Pankaj Kumar--- Applied Validations
'' updation by richa agarwal 12/03/201-----BM00000005818--'Add IBAN No,Swift code fields on form as well as in import/export functionality
'' work done agaist tcket no. BHA/29/08/18-000497
Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Text.RegularExpressions
Imports Excel = Microsoft.Office.Interop.Excel
Imports common

'created by --> Vipin
'createddate --> 10/05/2011
'modifiedby --> Vipin
'Modified date -->03/06/2011
'Tables Used --> tspl_bank_master,Tspl_gl_Accounts
Public Class frmBankMaster

    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim Qry As String
    Dim userCode, companyCode As String
    Dim strAccountFilter As String

    Const colCHECK_CODE As String = "colCHECK_CODE"
    Const colDESCRIPTION As String = "colDESCRIPTION"
    Const colNEXT_CHECK_NUMBER As String = "colNEXT_CHECK_NUMBER"
    Const colLAST_CHECK_NUMBER As String = "colLAST_CHECK_NUMBER"
    Const colLAST_PRINT_CHECK_NUMBER As String = "colLAST_PRINT_CHECK_NUMBER"
    Const colTS_FROM As String = "colTS_FROM"
    Const colTS_TO As String = "colTS_TO"
    Const colLineNo As String = "colLineNo"
    Const colCharges As String = "colCharges"


    Const ColNEFTHide As String = "Hide"
    Const ColNEFTCode As String = "Code"
    Const ColNEFTName As String = "Name"
    Const ColNEFTValue As String = "Value"



    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub

    Sub LoadBlankGridNEFT()
        gvNEFTPerforma.Rows.Clear()
        gvNEFTPerforma.Columns.Clear()
        gvNEFTPerforma.AllowAddNewRow = False

        Dim recoCheckBox As New GridViewCheckBoxColumn()
        recoCheckBox.HeaderText = ColNEFTHide
        recoCheckBox.Name = ColNEFTHide
        recoCheckBox.ReadOnly = False
        recoCheckBox.IsVisible = True
        recoCheckBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvNEFTPerforma.MasterTemplate.Columns.Add(recoCheckBox)

        Dim repoTxt As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = ColNEFTCode
        repoTxt.Width = 200
        repoTxt.Name = ColNEFTCode
        repoTxt.ReadOnly = True
        gvNEFTPerforma.MasterTemplate.Columns.Add(repoTxt)


        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = ColNEFTName
        repoTxt.Width = 200
        repoTxt.Name = ColNEFTName
        repoTxt.ReadOnly = False
        gvNEFTPerforma.MasterTemplate.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = ColNEFTValue
        repoTxt.Width = 400
        repoTxt.Name = ColNEFTValue
        repoTxt.ReadOnly = False
        gvNEFTPerforma.MasterTemplate.Columns.Add(repoTxt)

        gvNEFTPerforma.AllowDeleteRow = True
        gvNEFTPerforma.AllowAddNewRow = False
        gvNEFTPerforma.ShowGroupPanel = False
        gvNEFTPerforma.AllowColumnReorder = False
        gvNEFTPerforma.AllowRowReorder = True
        gvNEFTPerforma.EnableSorting = False
        gvNEFTPerforma.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvNEFTPerforma.MasterTemplate.ShowRowHeaderColumn = False
    End Sub

    'Main Form Load
    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim CHECK_CODE As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        CHECK_CODE.FormatString = ""
        CHECK_CODE.HeaderText = "Check Code"
        CHECK_CODE.Name = colCHECK_CODE
        CHECK_CODE.ReadOnly = False
        CHECK_CODE.IsVisible = True
        CHECK_CODE.Width = 100
        gv1.MasterTemplate.Columns.Add(CHECK_CODE)

        Dim DESCRIPTION As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        DESCRIPTION.FormatString = ""
        DESCRIPTION.HeaderText = "Description"
        DESCRIPTION.Name = colDESCRIPTION
        DESCRIPTION.ReadOnly = False
        DESCRIPTION.IsVisible = True
        DESCRIPTION.Width = 200
        gv1.MasterTemplate.Columns.Add(DESCRIPTION)

        'Dim NEXT_CHECK_NUMBER As GridViewDecimalColumn = New GridViewDecimalColumn()
        'NEXT_CHECK_NUMBER.FormatString = ""
        'NEXT_CHECK_NUMBER.HeaderText = "Next Check Number"
        'NEXT_CHECK_NUMBER.Name = colNEXT_CHECK_NUMBER
        'NEXT_CHECK_NUMBER.ReadOnly = False
        'NEXT_CHECK_NUMBER.IsVisible = True
        'NEXT_CHECK_NUMBER.Width = 120
        'NEXT_CHECK_NUMBER.Maximum = 999999
        'gv1.MasterTemplate.Columns.Add(NEXT_CHECK_NUMBER)

        Dim NEXT_CHECK_NUMBER As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        NEXT_CHECK_NUMBER = New GridViewTextBoxColumn()
        NEXT_CHECK_NUMBER.FormatString = ""
        NEXT_CHECK_NUMBER.HeaderText = "Next Check Number"
        NEXT_CHECK_NUMBER.Name = colNEXT_CHECK_NUMBER
        NEXT_CHECK_NUMBER.Width = 120
        NEXT_CHECK_NUMBER.MaxLength = 6
        NEXT_CHECK_NUMBER.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(NEXT_CHECK_NUMBER) '0

        'Dim LAST_CHECK_NUMBER As GridViewDecimalColumn = New GridViewDecimalColumn()
        'LAST_CHECK_NUMBER.FormatString = ""
        'LAST_CHECK_NUMBER.HeaderText = "Last Check Number"
        'LAST_CHECK_NUMBER.Name = colLAST_CHECK_NUMBER
        'LAST_CHECK_NUMBER.ReadOnly = False
        'LAST_CHECK_NUMBER.IsVisible = True
        'LAST_CHECK_NUMBER.Width = 120
        'LAST_CHECK_NUMBER.Maximum = 999999
        'gv1.MasterTemplate.Columns.Add(LAST_CHECK_NUMBER)

        Dim LAST_CHECK_NUMBER As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        LAST_CHECK_NUMBER = New GridViewTextBoxColumn()
        LAST_CHECK_NUMBER.FormatString = ""
        LAST_CHECK_NUMBER.HeaderText = "Last Check Number"
        LAST_CHECK_NUMBER.Name = colLAST_CHECK_NUMBER
        LAST_CHECK_NUMBER.Width = 120
        LAST_CHECK_NUMBER.MaxLength = 6
        LAST_CHECK_NUMBER.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(LAST_CHECK_NUMBER) '0
        ''
        '' Anubhooti 25-Feb-2015 (Last Printed Check Number)
        Dim LP_CHECK_NUMBER As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        LP_CHECK_NUMBER = New GridViewTextBoxColumn()
        LP_CHECK_NUMBER.FormatString = ""
        LP_CHECK_NUMBER.HeaderText = "Last Printed Check Number"
        LP_CHECK_NUMBER.Name = colLAST_PRINT_CHECK_NUMBER
        LP_CHECK_NUMBER.Width = 155
        LP_CHECK_NUMBER.MaxLength = 6
        LP_CHECK_NUMBER.ReadOnly = True
        LP_CHECK_NUMBER.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(LP_CHECK_NUMBER) '0
        gv1.Rows.AddNew()
        ''
    End Sub
    Sub LoadGridColumns()
        gvPP.Rows.Clear()
        gvPP.Columns.Clear()

        Dim LineNo As New GridViewTextBoxColumn
        Dim TS_FROM As New GridViewDecimalColumn
        Dim TS_TO As New GridViewDecimalColumn
        Dim Charges As New GridViewDecimalColumn


        LineNo.FormatString = ""
        LineNo.HeaderText = "Line No"
        LineNo.Name = colLineNo
        LineNo.Width = 70
        LineNo.ReadOnly = True
        LineNo.IsVisible = False
        LineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(LineNo)


        TS_FROM.FormatString = ""
        TS_FROM.HeaderText = "From"
        TS_FROM.Name = colTS_FROM
        TS_FROM.Width = 100
        TS_FROM.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(TS_FROM)

        TS_TO.FormatString = ""
        TS_TO.HeaderText = "To"
        TS_TO.Name = colTS_TO
        TS_TO.Width = 100
        TS_TO.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(TS_TO)


        Charges.FormatString = ""
        Charges.HeaderText = "Charges"
        Charges.Name = colCharges
        Charges.Width = 100
        Charges.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(Charges)

       

    End Sub
    Private Sub gvPP_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvPP.CurrentColumnChanged
        If gvPP.RowCount > 0 Then
            Dim intCurrRow As Integer = gvPP.CurrentRow.Index
            gvPP.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gvPP.Rows.Count - 1 Then
                gvPP.Rows.AddNew()
                gvPP.CurrentRow = gvPP.Rows(intCurrRow)
               
            End If
        End If
    End Sub
    Private Sub SetUserMgmtNew()
        '' Anubhooti 31-July-2014 BM00000003131
        'MyBase.SetUserMgmt(clsUserMgtCode.bankMaster)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function

        End If
        btnsave.Visible = MyBase.isModifyFlag

        '--------richa Ticket no. BM00000003014 03/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnsave.Visible = True Then
            bankimport.Enabled = True
            bankexport.Enabled = True
        Else
            bankimport.Enabled = False
            bankexport.Enabled = False
        End If
        '--------------------------------------------------
        '         btnclose.Visible = MyBase.isDeleteFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub BankMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadGetewayType()
        ToolTipbank.SetToolTip(btnnew, "New")
        fndbank.MyMaxLength = 12
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnclear, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New Trasnaction")
        AddHandler fndbank.TextChanged, AddressOf text_changed
        AddHandler fndbankacc.TextChanged, AddressOf fndbacc_textchange
        AddHandler fndwriteoff.TextChanged, AddressOf fndwriteoff_textchange
        AddHandler fndcredit.TextChanged, AddressOf fndcredit_textchange
        AddHandler fndbankacc.Leave, AddressOf fnd_leave1
        AddHandler fndwriteoff.Leave, AddressOf fnd_leave2
        AddHandler fndcredit.Leave, AddressOf fnd_leave3
        AddHandler fndbank.KeyPress, AddressOf key_press
        btndelete.Enabled = False
        btnsave.Enabled = True
        ValidateLength()

        LoadBlankGrid()
        LoadBlankGridNEFT()
        LoadGridColumns()
        gv1.Visible = True
        'bankstab.Pages.RemoveAt(2)

        '' Anubhooti 03-Sep-2014 BM00000003437
        Dim UseSubAccount As String = ""

        UseSubAccount = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowToUseSubAccount, clsFixedParameterCode.AllowToUseSubAccount, Nothing))
        If clsCommon.CompairString(UseSubAccount, "1") = CompairStringResult.Equal AndAlso clsCommon.CompairString(ddlbanktype.Text, "Bank") = CompairStringResult.Equal Then
            fndSubAcc.Visible = True
            MyLabel4.Visible = True
            txtSubAcc.Visible = True
            fndSubAcc.MendatroryField = True
            fndSubAcc.Value = ""
            txtSubAcc.Text = ""
        Else
            fndSubAcc.Visible = False
            MyLabel4.Visible = False
            txtSubAcc.Visible = False
            fndSubAcc.MendatroryField = False
            fndSubAcc.Value = ""
            txtSubAcc.Text = ""
        End If
        If clsCommon.myLen(objCommonVar.strCurrUserLocationsSegment) > 0 Then
            strAccountFilter = "Account_Seg_Code7 in (" + objCommonVar.strCurrUserLocationsSegment + ")"
        End If
        bankstab.SelectedPage = bankstabprofile
        ''
        gvPP.Rows.Clear()
        gvPP.Rows.AddNew()
    End Sub

    Private Sub LoadGetewayType()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "None"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "R"
        dr("Name") = "Receipt Gateway"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "P"
        dr("Name") = "Payment Gateway"
        dt.Rows.Add(dr)

        cboGetewayType.DataSource = dt
        cboGetewayType.ValueMember = "Code"
        cboGetewayType.DisplayMember = "Name"
    End Sub

    Private Sub ValidateLength()
        fndbank.MyMaxLength = 12
        txtdes.MaxLength = 60
        txtadd1.MaxLength = 60
        txtadd2.MaxLength = 60
        txtadd3.MaxLength = 60
        txtadd4.MaxLength = 60
        txtcity.MaxLength = 30
        txtstate.MaxLength = 30
        txtzip.MaxLength = 30
        txtcountry.MaxLength = 30
        txtcontact.MaxLength = 60
        txtphone.MaxLength = 30
        txtfax.MaxLength = 30
    End Sub

    'It will fill all controls in screen if find any existing data in table 
    Public Sub text_changed(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

            Dim strquery As String = "select * from TSPL_Bank_MASTER where bank_code='" + fndbank.Value + "'"
            Dim dt As DataTable
            Dim trs As String = fndbank.Tag
            Dim strvalue As String = ""
            dt = clsDBFuncationality.GetDataTable(strquery)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

                For Each row As DataRow In dt.Rows
                    strvalue = row(0).ToString()
                Next
End if
            If strvalue <> "" Then
                funfill()
            Else
                txtdes.Text = ""
                chkactive.Checked = False
                chkProvisionBank.Checked = False
                txtbankaccno.Text = ""
                fndbankacc.Value = ""
                fndwriteoff.Value = ""
                fndcredit.Value = ""
                txtbankacc.Text = ""
                txtwriteoff.Text = ""
                txtcredit.Text = ""
                txtadd1.Text = ""
                txtadd2.Text = ""
                txtadd3.Text = ""
                txtadd4.Text = ""
                txtcity.Text = ""
                txtstate.Text = ""
                txtzip.Text = ""
                txtcountry.Text = ""
                txtcontact.Text = ""
                txtphone.Text = ""
                txtfax.Text = ""
                ddlbanktype.Text = "Select"
                SetBankType()
                btnsave.Text = "Save"
                btndelete.Enabled = False
                txtChequeValidity.Text = "0"
                '' Richa Againt Ticket No. BM00000003641 on 27/08/2014
                txtLCCreditLimit.Value = 0
                txtFDPer.Value = 0
                '''''----------------------------------
                ''richa agarwal 12/03/2015
                TxtIbanno.Text = ""
                txtswiftcode.Text = ""
                ''--------------
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'It will fill  controls in screen if find any existing data in table 
    Public Sub fndbacc_textchange(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim strquery As String = "select account_code,description  from Tspl_gl_Accounts where account_code='" + fndbankacc.Value + "'"
            Dim dt As DataTable
            Dim strvalue As String = ""
            dt = clsDBFuncationality.GetDataTable(strquery)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

                For Each row As DataRow In dt.Rows
                    strvalue = row(0).ToString()
                Next
End if
            If strvalue <> "" Then
                funfill1()
            Else
                txtbankacc.Text = ""
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'It will fill  controls in screen if find any existing data in table
    Public Sub fndwriteoff_textchange(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim strquery As String = "select account_code,description  from Tspl_gl_Accounts where account_code='" + fndwriteoff.Value + "'"
            Dim dt As DataTable
            Dim strvalue As String = ""
            ' Dim strdes As String
            dt = clsDBFuncationality.GetDataTable(strquery)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

                For Each row As DataRow In dt.Rows
                    strvalue = row(0).ToString()
                Next
End if
            If strvalue <> "" Then

                funfill2()
            Else
                txtwriteoff.Text = ""
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'It will fill  controls in screen if find any existing data in table
    Public Sub fndcredit_textchange(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim strquery As String = "select account_code,description  from Tspl_gl_Accounts where account_code='" + fndcredit.Value + "'"
            Dim dt As DataTable
            Dim strvalue As String = ""
            dt = clsDBFuncationality.GetDataTable(strquery)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

                For Each row As DataRow In dt.Rows
                    strvalue = row(0).ToString()
                Next
            End if
            If strvalue <> "" Then
                funfill3()
            Else
                txtcredit.Text = ""
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'it will check the data existance in table on leave fndbankacc
    Public Sub fnd_leave1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If fndbankacc.Value = "" Then
        Else
            Try
                Dim strquery As String = "select account_code,description  from Tspl_gl_Accounts where account_code='" + fndbankacc.Value + "'"
                Dim dt As DataTable
                Dim strvalue As String = ""

                dt = clsDBFuncationality.GetDataTable(strquery)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

                    For Each row As DataRow In dt.Rows
                        strvalue = row(0).ToString()
                    Next
                End If
                If strvalue <> "" Then
                Else : strquery = ""
                    txtbankacc.Text = ""
                    common.clsCommon.MyMessageBoxShow(Me, "This Bank Account does not exist in Master Table")
                    fndbankacc.Value = ""
                End If
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
    End Sub
    'it will check the data existance in table on leave of fndwriteoff
    Public Sub fnd_leave2(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If fndwriteoff.Value = "" Then
        Else
            Try
                Dim strquery As String = "select account_code,description  from Tspl_gl_Accounts where account_code='" + fndwriteoff.Value + "'"
                Dim dt As DataTable
                Dim strvalue As String = ""
                dt = clsDBFuncationality.GetDataTable(strquery)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

                    For Each row As DataRow In dt.Rows
                        strvalue = row(0).ToString()
                    Next
End if
                If strvalue <> "" Then
                Else : strquery = ""
                    txtwriteoff.Text = ""
                    common.clsCommon.MyMessageBoxShow(Me, "This Write Off Account does not exist in Master Table")
                    fndwriteoff.Value = ""
                End If
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If

    End Sub
    'it will check the data existance in table on leave of fndcredit
    Public Sub fnd_leave3(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If fndcredit.Value = "" Then
        Else
            Try
                Dim strquery As String = "select account_code,description  from Tspl_gl_Accounts where account_code='" + fndcredit.Value + "'"
                Dim dt As DataTable
                Dim strvalue As String = ""
                dt = clsDBFuncationality.GetDataTable(strquery)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

                    For Each row As DataRow In dt.Rows
                        strvalue = row(0).ToString()
                    Next
                End if
                If strvalue <> "" Then
                Else : strquery = ""
                    txtcredit.Text = ""
                    common.clsCommon.MyMessageBoxShow(Me, "This Credit Card Charges Account does not exist in Master Table.")
                    fndcredit.Value = ""
                End If
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
    End Sub
    'Keypress validation on finder and converting lower case to upper case
    Public Sub key_press(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub
    'Validation on save button click and calling funinsert,funupdate
    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub
    Public Sub SaveData()
        Try
            '' Anubhooti 03-Sep-2014 BM00000003437
            Dim UseSubAcc As String
            UseSubAcc = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowToUseSubAccount, clsFixedParameterCode.AllowToUseSubAccount, Nothing))
            'If fndbank.Value = "" Then
            '    myMessages.blankValue("Bank Code")
            '    fndbank.Focus()
            If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False) = False) AndAlso clsCommon.myLen(fndbank.Value) <= 0 Then
                myMessages.blankValue("Bank Code")
                fndbank.Focus()
                Return
            ElseIf txtdes.Text = "" Then

                myMessages.blankValue("Description")
                txtdes.Focus()
            ElseIf fndbankacc.Value = "" Then

                myMessages.blankValue("Bank Account")
                fndbankacc.Focus()
            ElseIf fndwriteoff.Value = "" Then

                myMessages.blankValue("Write Off Account")
                fndwriteoff.Focus()
            ElseIf fndcredit.Value = "" Then
                myMessages.blankValue("Credit Cards Charges")
                fndcredit.Focus()
            ElseIf ddlbanktype.Text = "Select" Or ddlbanktype.Text = "select" Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select the bank type")
                ddlbanktype.Focus()
                '' Richa Againt Ticket No. BM00000003641 on 27/08/2014
            ElseIf clsCommon.myCdbl(txtFDPer.Value) > 100 Then
                common.clsCommon.MyMessageBoxShow(Me, "FD % cannot be more than 100")
                txtFDPer.Focus()
                ''---------------------------------
                '' Anubhooti 19-Dec-2014
            ElseIf clsCommon.myLen(fndtransferclearing.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Transfer clearing can not be left blank.")
                fndtransferclearing.Focus()
                '' Anubhooti 03-Sep-2014 BM00000003437
            ElseIf clsCommon.CompairString(UseSubAcc, "1") = CompairStringResult.Equal AndAlso clsCommon.myLen(fndSubAcc.Value) = 0 AndAlso clsCommon.CompairString(ddlbanktype.Text, "Bank") = CompairStringResult.Equal Then
                common.clsCommon.MyMessageBoxShow(Me, "Sub account can not be left blank.")
                fndSubAcc.Focus()
                '' Anubhooti 15-Dec-2014 BM00000004999
            ElseIf clsCommon.myCdbl(txtLCCreditLimit.Value) < 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "LC credit limit can not be negative.")
                txtLCCreditLimit.Focus()
            ElseIf chkClearanceBank.Checked = True AndAlso clsCommon.myLen(TxtMainBankCode.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please Select Main Bank Code.")
                TxtMainBankCode.Focus()
            ElseIf clsCommon.CompairString(clsDBFuncationality.getSingleValue("Select count(*) from TSPL_BANK_MASTER WHERE isnull(TSPL_BANK_MASTER.Main_Bank_Code,'') IN  (Select BANK_CODE  from TSPL_BANK_MASTER where BANK_CODE ='" & clsCommon.myCstr(fndbank.Value) & "')", Nothing), "1") = CompairStringResult.Equal Then
                common.clsCommon.MyMessageBoxShow(Me, "Bank Code (" & fndbank.Value & ") should not be of Clearance Bank, because it is used in other banks as a main bank.")
            Else
                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.bankMaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If

                If btnsave.Text = "Save" Then
                    funinsert()
                ElseIf btnsave.Text = "Update" Then
                    funupdate()
                End If
                UcCheckSetting1.saveSetting(False)
                UcCheckSetting1.formLoad()
            End If

        Catch ex As Exception

            myMessages.myExceptions(ex)
        End Try
    End Sub
    Function AllowToSave() As Boolean
        Dim ii As Int16 = 0
        For Each grow As GridViewRowInfo In gvPP.Rows
            If clsCommon.myLen(grow.Cells(colTS_FROM).Value) > 0 Then
             
                For Each growIn As GridViewRowInfo In gvPP.Rows
                    If grow.Index <> growIn.Index Then

                        If clsCommon.myCdbl(grow.Cells(colTS_FROM).Value) <= clsCommon.myCdbl(growIn.Cells(colTS_FROM).Value) And clsCommon.myCdbl(grow.Cells(colTS_TO).Value) >= clsCommon.myCdbl(growIn.Cells(colTS_FROM).Value) Then
                            If clsCommon.myCdbl(grow.Cells(colTS_FROM).Value) <= clsCommon.myCdbl(growIn.Cells(colTS_FROM).Value) And clsCommon.myCdbl(grow.Cells(colTS_TO).Value) > clsCommon.myCdbl(growIn.Cells(colTS_FROM).Value) Then
                                clsCommon.MyMessageBoxShow(Me, "Please Check Records in Row No [" & grow.Index + 1 & "] and [" & growIn.Index + 1 & "] . Both have Some Common Range")
                                Return False
                            End If
                            If clsCommon.myCdbl(grow.Cells(colTS_FROM).Value) <= clsCommon.myCdbl(growIn.Cells(colTS_TO).Value) And clsCommon.myCdbl(grow.Cells(colTS_TO).Value) >= clsCommon.myCdbl(growIn.Cells(colTS_TO).Value) And gvPP.Columns(colTS_TO).IsVisible Then
                                clsCommon.MyMessageBoxShow(Me, "Please Check Records in Row No [" & grow.Index + 1 & "] and [" & growIn.Index + 1 & "] . Both have Some Common Range")
                                Return False
                            End If
                        End If
                        If clsCommon.myCdbl(grow.Cells(colTS_FROM).Value) <= clsCommon.myCdbl(growIn.Cells(colTS_TO).Value) And clsCommon.myCdbl(grow.Cells(colTS_TO).Value) >= clsCommon.myCdbl(growIn.Cells(colTS_TO).Value) Then
                            If clsCommon.myCdbl(grow.Cells(colTS_FROM).Value) <= clsCommon.myCdbl(growIn.Cells(colTS_FROM).Value) And clsCommon.myCdbl(grow.Cells(colTS_TO).Value) > clsCommon.myCdbl(growIn.Cells(colTS_FROM).Value) Then
                                clsCommon.MyMessageBoxShow(Me, "Please Check Records in Row No [" & grow.Index + 1 & "] and [" & growIn.Index + 1 & "] . Both have Some Common Range")
                                Return False
                            End If
                            If clsCommon.myCdbl(grow.Cells(colTS_FROM).Value) <= clsCommon.myCdbl(growIn.Cells(colTS_TO).Value) And clsCommon.myCdbl(grow.Cells(colTS_TO).Value) >= clsCommon.myCdbl(growIn.Cells(colTS_TO).Value) Then
                                clsCommon.MyMessageBoxShow(Me, "Please Check Records in Row No [" & grow.Index + 1 & "] and [" & growIn.Index + 1 & "] . Both have Some Common Range")
                                Return False
                            End If
                        End If
                    
                    End If

                Next

                If gvPP.Columns(colTS_FROM).IsVisible Then
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colTS_FROM).Value)) <= 0 And gvPP.Columns(colTS_FROM).IsVisible Then
                        myMessages.blankValue("From Value is blank at row no " & (grow.Index + 1) & "")
                        Return False
                    End If
                End If

                If gvPP.Columns(colTS_TO).IsVisible Then
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colTS_TO).Value)) <= 0 And gvPP.Columns(colTS_TO).IsVisible Then
                        myMessages.blankValue("To Value is blank at row no " & (grow.Index + 1) & "")
                        Return False
                    End If
                End If

                If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colCharges).Value)) <= 0 Then
                    myMessages.blankValue("Charges is blank at row no " & (grow.Index + 1) & "")
                    Return False
                End If

                ii += 1

            End If

        Next

        Return True
    End Function
    'Funtion for insertion of data
    Public Sub funinsert()
        Try
            If AllowToSave() Then


                Dim val As String = ""
                If chkactive.Checked = True Then
                    val = "Inactive"
                ElseIf chkactive.Checked = False Then
                    val = "Active"
                End If
                Dim strbanktype As String
                If ddlbanktype.Text = "Bank" Then
                    strbanktype = "B"
                ElseIf ddlbanktype.Text = "Cash" Then
                    strbanktype = "C"
                ElseIf ddlbanktype.Text = "Petty Cash" Then
                    strbanktype = "P"
                ElseIf ddlbanktype.Text = "Other" Then
                    strbanktype = "O"
                ElseIf ddlbanktype.Text = "Settlement" Then
                    strbanktype = "S"
                Else

                    strbanktype = "N"
                End If
                '' Richa Againt Ticket No. BM00000003641 on 27/08/2014 add FDPercentage,LCCreditLimit in Update query
                '' Anubhooti 03-Sep-2014 BM00000003437 Update Sub_Account in update query





                Dim currentdate As Date = Date.Today

                Dim UseSubAccount As String = ""
                Dim SubAcc As String = ""
                Dim Count As Integer
                If chkDefaultBank.Checked Then
                    Dim ChkQryDefaultBank As String = "select count(*) from TSPL_BANK_MASTER where default_bank='1' and  bank_code<>'" + fndbank.Value + "'"
                    Count = clsDBFuncationality.getSingleValue(ChkQryDefaultBank)
                End If

                If Count > 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Default bank already mapped with another bank")

                Else
                    If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False)) Then
                        Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_BANK_MASTER where bank_code='" & clsCommon.myCstr(fndbank.Value) & "'")
                        If ChkNewEntry = 0 Then
                            fndbank.Value = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"), clsDocType.BankMaster, "", "")
                            If clsCommon.myLen(fndbank.Value) <= 0 Then
                                Throw New Exception("Error in Code Generation")
                            End If
                        End If
                    End If

                    connectSql.RunSp("sp_BankMaster_insert", New SqlParameter("@bcode", fndbank.Value.ToString()), New SqlParameter("@des", txtdes.Text.ToString()), New SqlParameter("@add1", txtadd1.Text.ToString()), New SqlParameter("@add2", txtadd2.Text.ToString()), New SqlParameter("@add3", txtadd3.Text.ToString()), New SqlParameter("@add4", txtadd4.Text.ToString()), New SqlParameter("@city", txtcity.Text.ToString()), New SqlParameter("@state", txtstate.Text.ToString()), New SqlParameter("@postal", txtzip.Text.ToString()), New SqlParameter("@country", txtcountry.Text.ToString()), New SqlParameter("@contact", txtcontact.Text.ToString()), New SqlParameter("@phone", txtphone.Text.ToString()), New SqlParameter("@fax", txtfax.Text.ToString()), New SqlParameter("@inactive", val), New SqlParameter("@bankaccnumber", txtbankaccno.Text.ToString()), New SqlParameter("@bankacc", fndbankacc.Value.ToString()), New SqlParameter("@writeoffacc", fndwriteoff.Value.ToString()), New SqlParameter("@creditacc", fndcredit.Value.ToString()), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate()), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate()), New SqlParameter("@compcode", companyCode), New SqlParameter("@banktype", strbanktype), New SqlParameter("@ChecKDefaultBank", chkDefaultBank.Checked), New SqlParameter("@Branch_Code", fndBranchName.Value.ToString()))

                    UpdateCommonColumns()

                    UseSubAccount = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowToUseSubAccount, clsFixedParameterCode.AllowToUseSubAccount, Nothing))
                    If clsCommon.CompairString(UseSubAccount, "1") = CompairStringResult.Equal AndAlso clsCommon.CompairString(ddlbanktype.Text, "Bank") = CompairStringResult.Equal Then
                        clsDBFuncationality.ExecuteNonQuery("Update TSPL_BANK_MASTER set Sub_Account='" & clsCommon.myCstr(fndSubAcc.Value) & "',Gateway_type='" + cboGetewayType.SelectedValue + "' where bank_code='" & clsCommon.myCstr(fndbank.Value) & "'")
                    Else
                        clsDBFuncationality.ExecuteNonQuery("Update TSPL_BANK_MASTER set Sub_Account=NULL,Gateway_type=NULL where bank_code='" & clsCommon.myCstr(fndbank.Value) & "'")
                    End If
                    If clsCommon.myLen(fndBankOpeningClearingAcount.Value) > 0 Then
                        clsDBFuncationality.ExecuteNonQuery("Update TSPL_BANK_MASTER set Bank_Opening_Clearing_Account='" + fndBankOpeningClearingAcount.Value + "' where bank_code='" & clsCommon.myCstr(fndbank.Value) & "'")
                    End If
                    clsDBFuncationality.ExecuteNonQuery("Update TSPL_BANK_MASTER set Email = '" + txtEmail.Text + "',BANK_GROUP_CODE = '" + IIf(clsCommon.myLen(txtBankGroup.Value) > 0, "'" + txtBankGroup.Value + "'", "NULL") + "' where bank_code='" & clsCommon.myCstr(fndbank.Value) & "' ")

                    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(fndbank.Value), "TSPL_BANK_MASTER", "bank_code", Nothing)
                    '' work done client Bharat on 29/08/2018
                    SaveBankSlab(Me.fndbank.Value, Nothing)

                    If SaveBankCheckData(Me.fndbank.Value.ToString) Then
                        myMessages.insert()
                    End If

                    'common.clsCommon.MyMessageBoxShow("Transport Id ' " + fndtrans.txtValue.Text + " 'saved successfully ", Me.Text)
                    btnsave.Text = "Update"
                    btndelete.Enabled = True
                    'If userCode <> "ADMIN" Then
                    '    If funSetUserAccess() = False Then Exit Sub
                    'End If
                End If

            End If
        Catch ex As Exception

            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub UpdateCommonColumns()
        If chkDefaultNEFTDBT.Checked Then
            clsDBFuncationality.ExecuteNonQuery("Update TSPL_BANK_MASTER set NEFT_DBT_Default=0")
        End If
        clsDBFuncationality.ExecuteNonQuery("Update TSPL_BANK_MASTER set NEFT_DBT_Default='" + IIf(chkDefaultNEFTDBT.Checked, "1", "0") + "' , IsSettlementBankForAD='" & IIf(chkSettlementBankForAD.Checked = True, "1", "0") & "', cheque_validity_in_days=" & clsCommon.myCdbl(txtChequeValidity.Text) & ",LCCreditLimit=" & clsCommon.myCdbl(txtLCCreditLimit.Value) & ",FDPercentage=" & clsCommon.myCdbl(txtFDPer.Value) & ",Transfer_Clearing_Account='" & fndtransferclearing.Value & "',  IBAN_No='" & TxtIbanno.Text & "',Swift_Code='" & txtswiftcode.Text & "',Is_Clearance_Bank='" & IIf(chkClearanceBank.Checked, "Y", "N") & "',Main_Bank_Code='" & clsCommon.myCstr(TxtMainBankCode.Value) & "' ,IsProvisionBank = '" + IIf(chkProvisionBank.Checked = True, "1", "0") + "' where bank_code='" & clsCommon.myCstr(fndbank.Value) & "'")
    End Sub

    Function SaveBankCheckData(ByVal strDocNo As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim qry As String
        qry = "delete from TSPL_BANK_CHECK_PRINTING where TSPL_BANK_CHECK_PRINTING.BANK_CODE='" + strDocNo + "' and TSPL_BANK_CHECK_PRINTING.CHECK_CODE not in(select ISNULL(CHECK_CODE,'') from TSPL_BANK_CHECK_PRINTING_STATUS)"
        clsDBFuncationality.ExecuteNonQuery(qry)
        For Each row As GridViewRowInfo In gv1.Rows
            If clsCommon.myLen(row.Cells(colCHECK_CODE).Value) <= 0 Then
                Continue For
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "BANK_CODE", strDocNo)
            clsCommon.AddColumnsForChange(coll, "CHECK_CODE", clsCommon.myCstr(row.Cells(colCHECK_CODE).Value))
            clsCommon.AddColumnsForChange(coll, "DESCRIPTION", clsCommon.myCstr(row.Cells(colDESCRIPTION).Value))
            clsCommon.AddColumnsForChange(coll, "NEXT_CHECK_NUMBER", clsCommon.myCdbl(row.Cells(colNEXT_CHECK_NUMBER).Value))
            clsCommon.AddColumnsForChange(coll, "LAST_CHECK_NUMBER", clsCommon.myCdbl(row.Cells(colLAST_CHECK_NUMBER).Value))
            ''qry = "select count(CHECK_CODE) from TSPL_BANK_CHECK_PRINTING where CHECK_CODE='" + clsCommon.myCstr(row.Cells(colCHECK_CODE).Value) + "'  and bank_code='" & strDocNo & "'"
            ''Dim Total As Integer
            ''Total = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
            ''If Total > 0 Then
            ''    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BANK_CHECK_PRINTING", OMInsertOrUpdate.Update, "TSPL_BANK_CHECK_PRINTING.CHECK_CODE='" + clsCommon.myCstr(row.Cells(colCHECK_CODE).Value) + "' and TSPL_BANK_CHECK_PRINTING.BANK_CODE='" + strDocNo + "' and TSPL_BANK_CHECK_PRINTING.CHECK_CODE not in(select CHECK_CODE from TSPL_BANK_CHECK_PRINTING_STATUS)", trans)
            'Else
            qry = "select CHECK_CODE,BANK_CODE from TSPL_BANK_CHECK_PRINTING where CHECK_CODE='" + clsCommon.myCstr(row.Cells(colCHECK_CODE).Value) + "' and bank_code not in ('" & strDocNo & "') "
            Dim dtcheck As DataTable
            dtcheck = clsDBFuncationality.GetDataTable(qry, trans)
            If dtcheck.Rows.Count > 0 Then
                clsCommon.MyMessageBoxShow(Me, "Check Code " & dtcheck.Rows(0).Item("CHECK_CODE") & " already exists in bank " & dtcheck.Rows(0).Item("BANK_CODE") & "")
                Return False
            End If
            '' Anubhooti 25-Feb-2015 (Printed Check Number > Last Check Number)
            If clsCommon.myCdbl(row.Cells(colLAST_CHECK_NUMBER).Value) < clsCommon.myCdbl(row.Cells(colLAST_PRINT_CHECK_NUMBER).Value) Then
                clsCommon.MyMessageBoxShow(Me, "Please check ! last check no. " & clsCommon.myCstr(clsCommon.myCdbl(row.Cells(colLAST_CHECK_NUMBER).Value)) & " should be greater than from printed check no. " & clsCommon.myCstr(clsCommon.myCdbl(row.Cells(colLAST_PRINT_CHECK_NUMBER).Value)) & "")
                Return False
            End If
            If clsDBFuncationality.getSingleValue("Select COUNT(*) from TSPL_BANK_CHECK_PRINTING WHERE CHECK_CODE ='" & clsCommon.myCstr(row.Cells(colCHECK_CODE).Value) & "' AND BANK_CODE='" & strDocNo & "'", trans) <= 0 Then
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BANK_CHECK_PRINTING", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BANK_CHECK_PRINTING", OMInsertOrUpdate.Update, " CHECK_CODE ='" & clsCommon.myCstr(row.Cells(colCHECK_CODE).Value) & "' AND BANK_CODE='" & strDocNo & "'", trans)
            End If


            ''End If
        Next

        Return True
    End Function
    Function SaveBankSlab(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = "delete from TSPL_BANK_MASTER_Slab where BANK_CODE='" + strDocNo + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        For Each row As GridViewRowInfo In gvPP.Rows
            If clsCommon.myLen(row.Cells(colTS_FROM).Value) <= 0 Then
                Continue For
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "BANK_CODE", strDocNo)
            clsCommon.AddColumnsForChange(coll, "FromSlab", clsCommon.myCdbl(row.Cells(colTS_FROM).Value))
            clsCommon.AddColumnsForChange(coll, "ToSlab", clsCommon.myCdbl(row.Cells(colTS_TO).Value))
            clsCommon.AddColumnsForChange(coll, "Charges", clsCommon.myCdbl(row.Cells(colCharges).Value))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BANK_MASTER_Slab", OMInsertOrUpdate.Insert, "", trans)
            ''End If
        Next


        qry = "delete from TSPL_BANK_MASTER_NEFT_PERFORMA where BANK_CODE='" + strDocNo + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        If gvNEFTPerforma.Rows.Count > 0 Then
            For Each row As GridViewRowInfo In gvNEFTPerforma.Rows
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "BANK_CODE", strDocNo)
                clsCommon.AddColumnsForChange(coll, "NEFT_Col_Hide", IIf(clsCommon.myCBool(row.Cells(ColNEFTHide).Value), 1, 0))
                clsCommon.AddColumnsForChange(coll, "NEFT_Col_Code", clsCommon.myCstr(row.Cells(ColNEFTCode).Value))
                clsCommon.AddColumnsForChange(coll, "NEFT_Col_Name", clsCommon.myCstr(row.Cells(ColNEFTName).Value))
                clsCommon.AddColumnsForChange(coll, "NEFT_Col_Value", clsCommon.myCstr(row.Cells(ColNEFTValue).Value))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BANK_MASTER_NEFT_PERFORMA", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
    'It will fill the all controls if value exist in database according to fndbank
    Public Sub funfill()
        Try
            Dim strquery As String = "select bank_code,description,add1,add2,add3,add4,city,state,postal,country,contact,phone,fax,inactive,bankaccnumber,bankacc,writeoffacc,creditacc, Bank_Type ,cheque_validity_in_Days,LCCreditLimit,FDPercentage,Sub_Account,Transfer_Clearing_Account,IBAN_No,Swift_Code,Gateway_type,Default_Bank,Branch_Code,Is_Clearance_Bank,Main_Bank_Code, Bank_Opening_Clearing_Account,IsProvisionBank,IsSettlementBankForAD,TSPL_Bank_MASTER.NEFT_DBT_Default, TSPL_Bank_MASTER.Email, TSPL_Bank_MASTER.BANK_GROUP_CODE   from TSPL_Bank_MASTER where bank_code='" + fndbank.Value + "'"
            Dim dt As DataTable
            dt = clsDBFuncationality.GetDataTable(strquery)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

                For Each row As DataRow In dt.Rows
                    txtChequeValidity.Text = row("cheque_validity_in_Days").ToString
                    fndbank.Value = row(0).ToString()
                    txtdes.Text = row(1).ToString()
                    txtadd1.Text = row(2).ToString()
                    txtadd2.Text = row(3).ToString()
                    txtadd3.Text = row(4).ToString()
                    txtadd4.Text = row(5).ToString()
                    txtcity.Text = row(6).ToString()
                    txtstate.Text = row(7).ToString()
                    txtzip.Text = row(8).ToString()
                    txtcountry.Text = row(9).ToString()
                    txtcontact.Text = row(10).ToString()
                    txtphone.Text = row(11).ToString()
                    txtfax.Text = row(12).ToString()

                    If row(13).ToString() = "Active" Then
                        chkactive.Checked = False
                    ElseIf row(13).ToString() = "Inactive" Then
                        chkactive.Checked = True
                    End If

                    txtbankaccno.Text = row(14).ToString()
                    fndbankacc.Value = row(15).ToString()
                    fndwriteoff.Value = row(16).ToString()
                    fndcredit.Value = row(17).ToString()
                    Dim strbanktype As String = row(18).ToString()
                    If strbanktype.Trim() = "B" Then
                        ddlbanktype.Text = "Bank"
                        cboGetewayType.SelectedValue = clsCommon.myCstr(row("Gateway_type"))
                    ElseIf strbanktype.Trim() = "C" Then
                        ddlbanktype.Text = "Cash"

                    ElseIf strbanktype.Trim() = "P" Then
                        ddlbanktype.Text = "Petty Cash"
                    ElseIf strbanktype.Trim() = "O" Then
                        ddlbanktype.Text = "Other"
                    ElseIf strbanktype.Trim() = "S" Then
                        ddlbanktype.Text = "Settlement"
                    End If
                    '' Richa Againt Ticket No. BM00000003641 on 27/08/2014
                    txtLCCreditLimit.Value = clsCommon.myCdbl(row("LCCreditLimit").ToString())
                    txtFDPer.Value = clsCommon.myCdbl(row("FDPercentage").ToString())
                    ''-----------------------------------
                    ''richa agarwal 12/03/2015
                    TxtIbanno.Text = clsCommon.myCstr(row("IBAN_No").ToString())
                    txtswiftcode.Text = clsCommon.myCstr(row("Swift_Code").ToString())
                    fndBankOpeningClearingAcount.Value = clsCommon.myCstr(row("Bank_Opening_Clearing_Account").ToString())
                    If clsCommon.myLen(fndBankOpeningClearingAcount.Value) > 0 Then
                        lblBankClearingAcount.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + fndBankOpeningClearingAcount.Value + "' ")
                    Else
                        lblBankClearingAcount.Text = ""
                    End If
                    ''--------------
                    If clsCommon.myCdbl(row("Default_Bank")) = 0 Then
                        chkDefaultBank.Checked = False
                    ElseIf clsCommon.myCdbl(row("Default_Bank")) = 1 Then
                        chkDefaultBank.Checked = True
                    End If
                    If clsCommon.myCdbl(row("IsProvisionBank")) = 1 Then
                        chkProvisionBank.Checked = True
                    Else
                        chkProvisionBank.Checked = False
                    End If
                    fndBranchName.Value = row("Branch_Code").ToString()
                    chkSettlementBankForAD.Checked = IIf(clsCommon.myCdbl(row("IsSettlementBankForAD")) = 1, True, False)
                    chkDefaultNEFTDBT.Checked = IIf(clsCommon.myCdbl(row("NEFT_DBT_Default")) = 1, True, False)
                    chkClearanceBank.Checked = IIf(clsCommon.CompairString(clsCommon.myCstr(row("Is_Clearance_Bank")), "Y") = CompairStringResult.Equal, True, False)
                    TxtMainBankCode.Value = clsCommon.myCstr(row("Main_Bank_Code"))
                    TxtMainBankName.Text = connectSql.RunScalar("select description from TSPL_BANK_MASTER where bank_code = '" + TxtMainBankCode.Value + "'")
                    txtEmail.Text = clsCommon.myCstr(row("Email"))
                    Dim UseSubAccount As String = ""

                    UseSubAccount = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowToUseSubAccount, clsFixedParameterCode.AllowToUseSubAccount, Nothing))
                    If clsCommon.CompairString(UseSubAccount, "1") = CompairStringResult.Equal AndAlso clsCommon.CompairString(ddlbanktype.Text, "Bank") = CompairStringResult.Equal Then
                        fndSubAcc.MendatroryField = True
                        fndSubAcc.Visible = True
                        MyLabel4.Visible = True
                        txtSubAcc.Visible = True
                        fndSubAcc.Value = clsCommon.myCstr(row("Sub_Account").ToString())
                        If clsCommon.myLen(fndSubAcc.Value) > 0 Then
                            txtSubAcc.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + fndSubAcc.Value + "' ")
                        Else
                            txtSubAcc.Text = ""
                        End If
                    Else
                        fndSubAcc.Value = ""
                        txtSubAcc.Text = ""
                        txtSubAcc.Visible = False
                        MyLabel4.Visible = False
                        fndSubAcc.Visible = False
                        fndSubAcc.MendatroryField = False
                    End If
                   
                    fndtransferclearing.Value = clsCommon.myCstr(row("Transfer_Clearing_Account").ToString())
                    If clsCommon.myLen(fndtransferclearing.Value) > 0 Then
                        txttransferclearing.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + fndtransferclearing.Value + "' ")
                    Else
                        txttransferclearing.Text = ""
                    End If
                    txtBankGroup.Value = row("BANK_GROUP_CODE").ToString()
                    ''
                Next
            End If
            UcCheckSetting1.strBankCode = fndbank.Value
            UcCheckSetting1.formLoad()

            btnsave.Enabled = True
            btndelete.Enabled = True
            btnsave.Text = "Update"
            'If userCode <> "ADMIN" Then
            '    If funSetUserAccess() = False Then Exit Sub
            'End If
        Catch ex As Exception

            myMessages.myExceptions(ex)
        End Try
    End Sub
    'It will fill the  controls if value exist in database according to fndbankacc
    Public Sub funfill1()
        Try

            Dim strquery As String = "select account_code,description  from Tspl_gl_Accounts where account_code='" + fndbankacc.Value + "'"
            Dim dt As DataTable
            dt = clsDBFuncationality.GetDataTable(strquery)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

                For Each row As DataRow In dt.Rows

                    txtbankacc.Text = row(1).ToString()
                Next
            End if
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'It will fill the  controls if value exist in database according to fndwriteoff
    Public Sub funfill2()
        Try

            Dim strquery As String = "select account_code,description  from Tspl_gl_Accounts where account_code='" + fndwriteoff.Value + "'"
            Dim dt As DataTable
            'Dim strvalue As String
            dt = clsDBFuncationality.GetDataTable(strquery)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

                For Each row As DataRow In dt.Rows

                    txtwriteoff.Text = row(1).ToString()
                Next
End if
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'It will fill the  controls if value exist in database according to fndcredit
    Public Sub funfill3()
        Try
            Dim strquery As String = "select account_code,description  from Tspl_gl_Accounts where account_code='" + fndcredit.Value + "'"
            Dim dt As DataTable
            'Dim strvalue As String
            dt = clsDBFuncationality.GetDataTable(strquery)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

                For Each row As DataRow In dt.Rows

                    txtcredit.Text = row(1).ToString()
                Next
            End if
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Public Sub funShowCheckData()
        Try
            Dim LastPrinted As Double = 0
            Dim strquery As String = "select CHECK_CODE,BANK_CODE,DESCRIPTION,NEXT_CHECK_NUMBER,LAST_CHECK_NUMBER,(select top 1 isnull(CHECK_CODE,0) from TSPL_BANK_CHECK_PRINTING_STATUS where CHECK_CODE=TSPL_BANK_CHECK_PRINTING.CHECK_CODE)EDIT from TSPL_BANK_CHECK_PRINTING where BANK_CODE='" & fndbank.Value.ToString & "'"
            Dim dt As DataTable
            ' Dim strvalue As String
            dt = clsDBFuncationality.GetDataTable(strquery)
            ' LoadBlankGrid()

            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

                For Each row As DataRow In dt.Rows
                    'gv1.Rows.AddNew()
                    gv1.Rows(dt.Rows.IndexOf(row)).Cells(colCHECK_CODE).Value = clsCommon.myCstr(row.Item("CHECK_CODE"))
                    gv1.Rows(dt.Rows.IndexOf(row)).Cells(colCHECK_CODE).ReadOnly = True
                    gv1.Rows(dt.Rows.IndexOf(row)).Cells(colDESCRIPTION).Value = clsCommon.myCstr(row.Item("DESCRIPTION"))
                    gv1.Rows(dt.Rows.IndexOf(row)).Cells(colNEXT_CHECK_NUMBER).Value = clsCommon.myCdbl(row.Item("NEXT_CHECK_NUMBER"))
                    gv1.Rows(dt.Rows.IndexOf(row)).Cells(colLAST_CHECK_NUMBER).Value = clsCommon.myCdbl(row.Item("LAST_CHECK_NUMBER"))
                    If Val(row.Item("Edit").ToString()) > 0 Then
                        gv1.Rows(dt.Rows.IndexOf(row)).Cells(colDESCRIPTION).ReadOnly = True
                        gv1.Rows(dt.Rows.IndexOf(row)).Cells(colNEXT_CHECK_NUMBER).ReadOnly = True
                        gv1.Rows(dt.Rows.IndexOf(row)).Cells(colLAST_CHECK_NUMBER).ReadOnly = True
                    End If
                    '' Anubhooti 25-Feb-2015 (Status of check number)
                    If clsCommon.myLen(clsCommon.myCstr(row.Item("CHECK_CODE"))) > 0 Then
                        LastPrinted = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select MAX(Check_Number) AS Check_Number From TSPL_BANK_CHECK_PRINTING_STATUS WHERE CHECK_CODE ='" & clsCommon.myCstr(row.Item("CHECK_CODE")) & "' AND BANK_CODE='" & fndbank.Value & "' GROUP BY CHECK_CODE"))
                        gv1.Rows(dt.Rows.IndexOf(row)).Cells(colLAST_PRINT_CHECK_NUMBER).Value = LastPrinted
                        If LastPrinted > 0 Then
                            gv1.Rows(dt.Rows.IndexOf(row)).Cells(colDESCRIPTION).ReadOnly = True
                            gv1.Rows(dt.Rows.IndexOf(row)).Cells(colNEXT_CHECK_NUMBER).ReadOnly = True
                            gv1.Rows(dt.Rows.IndexOf(row)).Cells(colLAST_CHECK_NUMBER).ReadOnly = False
                        End If
                    End If
                    gv1.Rows.AddNew()
                Next
            End If
            gv1.Rows.AddNew()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Public Sub funShowBankChargesSlab()
        Try
            Dim strquery As String = "select * from TSPL_BANK_MASTER_Slab where BANK_CODE='" & fndbank.Value.ToString & "'"
            Dim dt As DataTable
            dt = clsDBFuncationality.GetDataTable(strquery)
            ''richa agarwal use gvPP instead of gv1
            gvPP.Rows.Clear()
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each row As DataRow In dt.Rows
                    gvPP.Rows.AddNew()
                    gvPP.Rows(dt.Rows.IndexOf(row)).Cells(colTS_FROM).Value = clsCommon.myCdbl(row.Item("FromSlab"))
                    gvPP.Rows(dt.Rows.IndexOf(row)).Cells(colTS_TO).Value = clsCommon.myCdbl(row.Item("ToSlab"))
                    gvPP.Rows(dt.Rows.IndexOf(row)).Cells(colCharges).Value = clsCommon.myCdbl(row.Item("Charges"))
                Next
            Else
                gvPP.Rows.Clear()
            End If
            gvPP.Rows.AddNew()


            LoadBlankGridNEFT()
            strquery = "select TSPL_BANK_MASTER_NEFT_PERFORMA.* from TSPL_BANK_MASTER_NEFT_PERFORMA where BANK_CODE='" & fndbank.Value.ToString & "'"
            dt = clsDBFuncationality.GetDataTable(strquery)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each row As DataRow In dt.Rows
                    gvNEFTPerforma.Rows.AddNew()
                    gvNEFTPerforma.Rows(dt.Rows.IndexOf(row)).Cells(ColNEFTHide).Value = clsCommon.myCBool(row.Item("NEFT_Col_Hide"))
                    gvNEFTPerforma.Rows(dt.Rows.IndexOf(row)).Cells(ColNEFTCode).Value = clsCommon.myCstr(row.Item("NEFT_Col_Code"))
                    gvNEFTPerforma.Rows(dt.Rows.IndexOf(row)).Cells(ColNEFTName).Value = clsCommon.myCstr(row.Item("NEFT_Col_Name"))
                    gvNEFTPerforma.Rows(dt.Rows.IndexOf(row)).Cells(ColNEFTValue).Value = clsCommon.myCstr(row.Item("NEFT_Col_Value"))
                Next
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'Funtion for updation  of data
    Public Sub funupdate()
        Try
            If AllowToSave() Then
                Dim val As String = ""
                If chkactive.Checked = True Then
                    val = "Inactive"
                ElseIf chkactive.Checked = False Then
                    val = "Active"
                End If
                Dim strbanktype As String
                If ddlbanktype.Text = "Bank" Then
                    strbanktype = "B"
                ElseIf ddlbanktype.Text = "Cash" Then
                    strbanktype = "C"
                ElseIf ddlbanktype.Text = "Petty Cash" Then
                    strbanktype = "P"
                ElseIf ddlbanktype.Text = "Other" Then
                    strbanktype = "O"
                ElseIf ddlbanktype.Text = "Settlement" Then
                    strbanktype = "S"
                Else
                    strbanktype = "N"
                End If
                '' Richa Againt Ticket No. BM00000003641 on 27/08/2014 add FDPercentage,LCCreditLimit in Update query
                '' Anubhooti BM00000003437 03-Sep-2014 Update Sub_Account in Update Query
                Dim UseSubAccount As String = ""
                Dim SubAcc As String = ""
                Dim count As Integer
                If chkDefaultBank.Checked Then
                    Dim ChkQryDefaultBank As String = "select count(*) from TSPL_BANK_MASTER where default_bank='1' and  bank_code<>'" + fndbank.Value + "'"
                    count = clsDBFuncationality.getSingleValue(ChkQryDefaultBank)
                End If

                If count > 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Default bank already mapped with another bank")

                Else

                    Dim currentdate As Date = Date.Today
                    connectSql.RunSp("sp_BankMaster_update", New SqlParameter("@bcode", fndbank.Value.ToString()), New SqlParameter("@des", txtdes.Text.ToString()), New SqlParameter("@add1", txtadd1.Text.ToString()), New SqlParameter("@add2", txtadd2.Text.ToString()), New SqlParameter("@add3", txtadd3.Text.ToString()), New SqlParameter("@add4", txtadd4.Text.ToString()), New SqlParameter("@city", txtcity.Text.ToString()), New SqlParameter("@state", txtstate.Text.ToString()), New SqlParameter("@postal", txtzip.Text.ToString()), New SqlParameter("@country", txtcountry.Text.ToString()), New SqlParameter("@contact", txtcontact.Text.ToString()), New SqlParameter("@phone", txtphone.Text.ToString()), New SqlParameter("@fax", txtfax.Text.ToString()), New SqlParameter("@inactive", val), New SqlParameter("@bankaccnumber", txtbankaccno.Text.ToString()), New SqlParameter("@bankacc", fndbankacc.Value.ToString()), New SqlParameter("@writeoffacc", fndwriteoff.Value.ToString()), New SqlParameter("@creditacc", fndcredit.Value.ToString()), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate()), New SqlParameter("@compcode", companyCode), New SqlParameter("@banktype", strbanktype), New SqlParameter("@ChecKDefaultBank", chkDefaultBank.Checked), New SqlParameter("@Branch_Code", fndBranchName.Value.ToString()))
                    UpdateCommonColumns()

                    UseSubAccount = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowToUseSubAccount, clsFixedParameterCode.AllowToUseSubAccount, Nothing))
                    If clsCommon.CompairString(UseSubAccount, "1") = CompairStringResult.Equal AndAlso clsCommon.CompairString(ddlbanktype.Text, "Bank") = CompairStringResult.Equal Then
                        clsDBFuncationality.ExecuteNonQuery("Update TSPL_BANK_MASTER set Sub_Account='" & clsCommon.myCstr(fndSubAcc.Value) & "',Gateway_type='" + clsCommon.myCstr(cboGetewayType.SelectedValue) + "' where bank_code='" & clsCommon.myCstr(fndbank.Value) & "'")
                    Else
                        clsDBFuncationality.ExecuteNonQuery("Update TSPL_BANK_MASTER set Sub_Account=NULL,Gateway_type=NULL where bank_code='" & clsCommon.myCstr(fndbank.Value) & "'")
                    End If
                    ' Ticket No : TEC/02/11/18-000359 by Prabhakar
                    If clsCommon.myLen(fndBankOpeningClearingAcount.Value) > 0 Then
                        clsDBFuncationality.ExecuteNonQuery("Update TSPL_BANK_MASTER set Bank_Opening_Clearing_Account='" + fndBankOpeningClearingAcount.Value + "' where bank_code='" & clsCommon.myCstr(fndbank.Value) & "'")
                    End If
                    clsDBFuncationality.ExecuteNonQuery("Update TSPL_BANK_MASTER set Email = '" + txtEmail.Text + "',BANK_GROUP_CODE = " + IIf(clsCommon.myLen(txtBankGroup.Value) > 0, "'" + txtBankGroup.Value + "'", "NULL") + " where bank_code='" & clsCommon.myCstr(fndbank.Value) & "' ")

                    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(fndbank.Value), "TSPL_BANK_MASTER", "bank_code", Nothing)
                    '' work done client Bharat on 29/08/2018
                    SaveBankSlab(Me.fndbank.Value, Nothing)

                    If SaveBankCheckData(Me.fndbank.Value.ToString) Then
                        myMessages.update()
                    End If
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'Function for deletion of data
    Public Sub fundelete()
        Try
            Dim qst As String
            Dim dpt As String
            '--------- for bank transfer screen----
            qst = "select From_Bank_Code ,To_Bank_Code  from TSPL_BANK_TRANSFER where (From_Bank_Code='" + fndbank.Value + "' or To_Bank_Code='" + fndbank.Value + "')"
            dpt = clsDBFuncationality.getSingleValue(qst)
            If clsCommon.myLen(dpt) > 0 AndAlso dpt IsNot Nothing Then
                common.clsCommon.MyMessageBoxShow(Me, "This Customer Cannot be deleted." + Environment.NewLine + "This is already in Process")
                Return
            End If
            connectSql.RunSp("sp_BankMaster_delete", New SqlParameter("@bcode", fndbank.Value.ToString()))
            myMessages.delete()
            btnsave.Text = "Save"
            btndelete.Enabled = False

        Catch ex As Exception

            myMessages.myExceptions(ex)
        End Try
    End Sub
    'Delete funtion call on delete button
    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Public Sub DeleteData()
        If fndbank.Value = "" Then
            myMessages.blankValue("Bank Code")
        ElseIf myMessages.deleteConfirm() Then
            fundelete()
           
        End If
    End Sub
    'It will reset all the controls in screens
    Public Sub funreset()
        txtBankGroup.Value = ""
        chkDefaultBank.Checked = False
        fndbank.Value = ""
        fndbank.MyReadOnly = False
        txtdes.Text = ""
        chkactive.Checked = False
        txtEmail.Text = ""
        txtbankaccno.Text = ""
        fndbankacc.Value = ""
        fndwriteoff.Value = ""
        fndcredit.Value = ""
        txtbankacc.Text = ""
        txtwriteoff.Text = ""
        txtcredit.Text = ""
        txtadd1.Text = ""
        txtadd2.Text = ""
        txtadd3.Text = ""
        txtadd4.Text = ""
        txtcity.Text = ""
        txtstate.Text = ""
        txtzip.Text = ""
        txtcountry.Text = ""
        txtcontact.Text = ""
        txtphone.Text = ""
        txtfax.Text = ""
        chkDefaultBank.Checked = False
        chkSettlementBankForAD.Checked = False
        chkDefaultNEFTDBT.Checked = False
        btnsave.Text = "Save"
        btndelete.Enabled = False
        ddlbanktype.Text = "Select"
        SetBankType()
        txtChequeValidity.Text = "0"
        ''richa Ticket No. BM00000003641 on 27/08/2014
        txtLCCreditLimit.Value = 0
        txtFDPer.Value = 0
        ''-----------------------------
        ''richa agarwal 12/03/2015
        TxtIbanno.Text = ""
        txtswiftcode.Text = ""
        fndBankOpeningClearingAcount.Value = Nothing
        lblBankClearingAcount.Text = ""
        ''--------------
        cboGetewayType.SelectedValue = ""
        '' Anubhooti 03-Sep-2014 BM00000003437
        Dim UseSubAccount As String = ""

        ''sanjeet 21/12/2016
        fndBranchName.Value = ""

        UseSubAccount = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowToUseSubAccount, clsFixedParameterCode.AllowToUseSubAccount, Nothing))
        If clsCommon.CompairString(UseSubAccount, "1") = CompairStringResult.Equal AndAlso clsCommon.CompairString(ddlbanktype.Text, "Bank") = CompairStringResult.Equal Then
            fndSubAcc.Visible = True
            txtSubAcc.Visible = True
            MyLabel4.Visible = True
            fndSubAcc.MendatroryField = True
            fndSubAcc.Value = ""
            txtSubAcc.Text = ""
        Else
            fndSubAcc.Visible = False
            txtSubAcc.Visible = False
            MyLabel4.Visible = False
            fndSubAcc.MendatroryField = False
            fndSubAcc.Value = ""
            txtSubAcc.Text = ""
        End If
        fndSubAcc.MendatroryField = False
        fndtransferclearing.Value = ""
        txttransferclearing.Text = ""
        UcCheckSetting1.strBankCode = ""
        UcCheckSetting1.NewData()
        ''
        gv1.Rows.Clear()
        gv1.Rows.AddNew()

        gvPP.Rows.Clear()
        gvPP.Rows.AddNew()
        LoadBlankGridNEFT()
        chkClearanceBank.Checked = False
        TxtMainBankCode.Value = ""
        TxtMainBankName.Text = ""
    End Sub
    'It will call reset function on new button
    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        funreset()
    End Sub
    'closing of current window form
    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        Me.Close()
    End Sub
    'Property of finder
    'Private Sub fndbank_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fndbank.ConnectionString = connectSql.SqlCon()
    '    fndbank.Query = " select bank_code As [Bank Code],description  as [Description]from TSPL_Bank_MASTER "
    '    fndbank.ValueToSelect = "Bank Code"
    '    fndbank.Caption = "Bank Master"
    '    fndbank.ValueToSelect1 = "Description"
    'End Sub

    'Private Sub fndbankacc_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fndbankacc.ConnectionString = connectSql.SqlCon()
    '    ' fndbankacc.Query = "select account_code as [Account Code],description as [Description] from Tspl_gl_Accounts"
    '    fndbankacc.Query = clsERPFuncationality.glaccountquery
    '    fndbankacc.ValueToSelect = "Account_Code"
    '    fndbankacc.Caption = "Account"
    '    fndbankacc.ValueToSelect1 = "Description"

    'End Sub

    'Private Sub fndwriteoff_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fndwriteoff.ConnectionString = connectSql.SqlCon()
    '    'fndwriteoff.Query = "select account_code as [Account Code],description as [Description] from Tspl_gl_Accounts"
    '    fndwriteoff.Query = clsERPFuncationality.glaccountquery
    '    fndwriteoff.ValueToSelect = "Account_Code"
    '    fndwriteoff.Caption = "Account"
    '    fndwriteoff.ValueToSelect1 = "Description"
    'End Sub

    'Private Sub fndcredit_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fndcredit.ConnectionString = connectSql.SqlCon()
    '    'fndcredit.Query = "select account_code as [Account Code],description as [Description] from Tspl_gl_Accounts"
    '    fndcredit.Query = clsERPFuncationality.glaccountquery
    '    fndcredit.ValueToSelect = "Account_Code"
    '    fndcredit.Caption = "Account"
    '    fndcredit.ValueToSelect1 = "Description"
    'End Sub
    'For Export functionality 

    Private Sub bankexport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bankexport.Click
        '' Richa Againt Ticket No. BM00000003641 on 27/08/2014 add FDPercentage,LCCreditLimit
        Dim str As String
        str = "select bank_code as [Bank Code],description as [Description],add1 as [Address1],add2 as [Address2],add3 as [Address3],add4 as [Address4],city as [City],state as [State],postal as [Postal],country as [Country],contact as [Contact],phone as [Phone],fax as [Fax],inactive as [Inactive],bankaccnumber as [Bank Account Number],bankacc as [Bank Account],writeoffacc as [Write Off Account],creditacc as [Credit Account],Bank_Type as [Bank Type], Cheque_Validity_In_Days as [Cheque Validity In Days],LCCreditLimit as [LC Credit Limit],FDPercentage as [FD %],Sub_Account As [Sub Account],IBAN_No as [IBAN No],Swift_Code as [Swift Code],Transfer_Clearing_Account as [Transfer Clearing Account],Is_Clearance_Bank as [Is Clearance Bank],Main_Bank_Code as [Main Bank Code],Bank_Opening_Clearing_Account as [Bank Opening Clearing Account],BANK_GROUP_CODE as [Bank Group Code]  from tspl_Bank_master"
        ListImpExpColumnsMandatory = New List(Of String)({"Transfer Clearing Account", "Bank Type", "Bank Code", "Description", "Sub Account", "Main Bank Code"})
        ListImpExpColumnsSuperMandatory = New List(Of String)({"Bank Code"})
        transportSql.ExporttoExcel(str, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)
    End Sub
    'For Import functionality 
    Private Sub bankimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bankimport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        '' Richa Againt Ticket No. BM00000003641 on 27/08/2014 add FDPercentage,LCCreditLimit
        '' Anubhooti 03-Sep-2014 BM00000003437 Add Sub_Account
        If transportSql.importExcel(gv, "Bank Code", "Description", "Address1", "Address2", "Address3", "Address4", "City", "State", "Postal", "Country", "Contact", "Phone", "Fax", "Inactive", "Bank Account Number", "Bank Account", "Write Off Account", "Credit Account", "Bank Type", "Cheque Validity In Days", "LC Credit Limit", "FD %", "Sub Account", "IBAN No", "Swift Code", "Transfer Clearing Account", "Is Clearance Bank", "Main Bank Code", "Bank Opening Clearing Account", "Bank Group Code") Then
            Dim trans As SqlTransaction = Nothing
            Dim ii As Integer = 0
            Try
                'connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    ii += 1
                    Dim strcode As String = clsCommon.myCstr(grow.Cells(0).Value)
                    Dim strdes As String = clsCommon.myCstr(grow.Cells(1).Value)
                    Dim stradd1 As String = clsCommon.myCstr(grow.Cells(2).Value)
                    If stradd1.Length > 50 Then
                        Throw New Exception("Check the length of address1.")

                    End If
                    Dim stradd2 As String = clsCommon.myCstr(grow.Cells(3).Value)
                    If stradd2.Length > 50 Then
                        Throw New Exception("Check the length of address2.")

                    End If
                    Dim stradd3 As String = clsCommon.myCstr(grow.Cells(4).Value)
                    If stradd3.Length > 50 Then
                        Throw New Exception("Check the length of address3.")

                    End If
                    Dim stradd4 As String = clsCommon.myCstr(grow.Cells(5).Value)
                    If stradd4.Length > 50 Then
                        Throw New Exception("Check the length of address4.")

                    End If
                    Dim strcity As String = clsCommon.myCstr(grow.Cells(6).Value)
                    If strcity.Length > 30 Then
                        Throw New Exception("Check the length of City.")

                    End If
                    Dim strstate As String = clsCommon.myCstr(grow.Cells(7).Value)
                    If strstate.Length > 30 Then
                        Throw New Exception("Check the length of State.")

                    End If
                    Dim strpostal As String = clsCommon.myCstr(grow.Cells(8).Value)
                    If strpostal.Length > 30 Then
                        Throw New Exception("Check the length of Postal.")

                    End If
                    Dim strcountry As String = clsCommon.myCstr(grow.Cells(9).Value)
                    If strcountry.Length > 30 Then
                        Throw New Exception("Check the length of Country.")

                    End If
                    Dim strcontact As String = clsCommon.myCstr(grow.Cells(10).Value)
                    If strcontact.Length > 60 Then
                        Throw New Exception("Check the length of Contact.")

                    End If
                    Dim strphone As String = clsCommon.myCstr(grow.Cells(11).Value)
                    If strphone.Length > 30 Then
                        Throw New Exception("Check the length of Phone.")

                    End If
                    Dim strfax As String = clsCommon.myCstr(grow.Cells(12).Value)
                    If strfax.Length > 30 Then
                        Throw New Exception("Check the length of Fax.")
                    End If
                    Dim strinactive As String = clsCommon.myCstr(grow.Cells(13).Value)
                    If strinactive.Length > 10 Then
                        Throw New Exception("Check the length of Inactive.")

                    End If
                    Dim strbaccno As String = clsCommon.myCstr(grow.Cells(14).Value)
                    If strbaccno.Length > 30 Then
                        Throw New Exception("Check the length of Bank Account Number.")

                    End If
                    Dim strbaacc As String = clsCommon.myCstr(grow.Cells(15).Value)
                    Dim strwriteoff As String = clsCommon.myCstr(grow.Cells(16).Value)
                    Dim strcredit As String = clsCommon.myCstr(grow.Cells(17).Value)
                    Dim strbanktype As String = clsCommon.myCstr(grow.Cells(18).Value)
                    Dim strCheckValidityIndays As String = clsCommon.myCstr(grow.Cells(19).Value)
                    Dim strTransferClearAC As String = clsCommon.myCstr(grow.Cells("Transfer Clearing Account").Value)
                    If clsCommon.myLen(strTransferClearAC) <= 0 Then
                        Throw New Exception("Please enter Transfer Clearing Account")
                    Else
                        strTransferClearAC = clsERPFuncationality.GLGetAccountCode(strTransferClearAC, trans)
                    End If



                    If clsCommon.myLen(strbanktype) <= 0 Then
                        Throw New Exception("Bank Type can not be blank.")
                    End If

                    If (strbanktype = "B" Or strbanktype = "C" Or strbanktype = "O" Or strbanktype = "P" Or strbanktype = "S") Then
                    Else
                        Throw New Exception("Only 'B' for bank type,'C' for cash type,S for Settlement,'O' for others an 'P'are allowed in bank type")
                    End If


                    Dim strChkValidity As Integer = clsCommon.myCdbl(grow.Cells("Cheque Validity In Days").Value)

                    If (String.IsNullOrEmpty(strcode)) Or strcode.Length > 12 Then
                        Throw New Exception("Bank Code can not be blank or incorrect. ")

                    End If
                    If (String.IsNullOrEmpty(strdes)) Or strdes.Length > 50 Then
                        Throw New Exception(" Description can not be blank or incorrect.")

                    End If
                    '' Richa Againt Ticket No. BM00000003641 on 27/08/2014 

                    Dim dblLCCreditLimit As Double = clsCommon.myCdbl(grow.Cells("LC Credit Limit").Value)
                    Dim dblFDPer As Double = clsCommon.myCdbl(grow.Cells("FD %").Value)
                    If dblFDPer > 100 Then
                        Throw New Exception(" FD % cannot be more than 100")

                    End If
                    ''richa 12/03/2015
                    Dim stribanno As String = clsCommon.myCstr(grow.Cells("IBAN No").Value)
                    Dim strswiftcode As String = clsCommon.myCstr(grow.Cells("Swift Code").Value)
                    ''''-----------------------------------
                    '' Anubhooti 03-Sep-2014 BM00000003437 
                    Dim strSubAcc As String = clsCommon.myCstr(grow.Cells("Sub Account").Value)
                    Dim UseSubAccount As String = ""

                    UseSubAccount = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowToUseSubAccount, clsFixedParameterCode.AllowToUseSubAccount, trans))
                    If clsCommon.CompairString(UseSubAccount, "1") = CompairStringResult.Equal Then
                        If clsCommon.myLen(strSubAcc) > 0 Then
                            If clsCommon.myLen(strSubAcc) > 50 Then
                                Throw New Exception("Sub account length can not be more than 50")
                            End If
                            Dim GLAcc As Double = clsDBFuncationality.getSingleValue("SELECT Count(*) As Row FROM TSPL_GL_ACCOUNTS Where Account_Code='" & strSubAcc & "'", trans)
                            If GLAcc = 0 Then
                                Throw New Exception("Please check ! sub account (" & strSubAcc & ") does not exists.")
                            End If
                        Else
                            Throw New Exception("Sub account can not be left blank")
                        End If
                    Else
                        strSubAcc = ""
                    End If
                    ''------------------------Sub Account on Settings--------------------

                    Dim strClearanceBank As String = clsCommon.myCstr(grow.Cells("Is Clearance Bank").Value)
                    If strClearanceBank.Length > 1 Then
                        Throw New Exception("Is Clearance Bank length cannot be more than 1.")
                    End If

                    If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Is Clearance Bank").Value), "") = CompairStringResult.Equal Then
                        strClearanceBank = "N"
                    End If

                    'If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Is Clearance Bank").Value), "N") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Is Clearance Bank").Value), "Y") <> CompairStringResult.Equal Then
                    '    Throw New Exception("Please check ! Is Clearance Bank (" & strClearanceBank & "), it must be Y/N.")
                    'End If
                    If clsCommon.CompairString(clsCommon.myCstr(strClearanceBank), "N") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(strClearanceBank), "Y") <> CompairStringResult.Equal Then
                        Throw New Exception("Please check ! Is Clearance Bank (" & strClearanceBank & "), it must be Y/N.")
                    End If
                    Dim strMainBank As String = String.Empty
                    If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Is Clearance Bank").Value), "Y") = CompairStringResult.Equal Then
                        strMainBank = clsCommon.myCstr(grow.Cells("Main Bank Code").Value)
                        If strMainBank.Length <= 0 Then
                            Throw New Exception("Please enter Main Bank Code.")
                        End If
                        If strMainBank.Length > 12 Then
                            Throw New Exception("Main Bank Code length cannot be more than 12.")
                        End If
                        Dim bankcount As Double = clsDBFuncationality.getSingleValue("SELECT Count(*) As Row FROM TSPL_Bank_Master Where BANK_CODE='" & strMainBank & "' and isnull(TSPL_BANK_MASTER.Is_Clearance_Bank,'')='N'", trans)
                        If bankcount = 0 Then
                            Throw New Exception("Please check ! Main Bank Code (" & strMainBank & ") does not exists.")
                        End If
                    End If

                    Dim bankcount1 As Double = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_BANK_MASTER WHERE isnull(TSPL_BANK_MASTER.Main_Bank_Code,'') IN  (Select BANK_CODE  from TSPL_BANK_MASTER where BANK_CODE ='" & strcode & "')", trans)
                    If bankcount1 > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(strClearanceBank), "Y") = CompairStringResult.Equal Then
                        Throw New Exception("Please check ! Bank Code (" & strcode & ") should not be of Clearance Bank, because it is used in other banks as a main bank.")
                    End If

                    Dim strBankOpeningClearingAccount As String = clsCommon.myCstr(grow.Cells("Bank Opening Clearing Account").Value)
                    If clsCommon.myLen(strBankOpeningClearingAccount) > 0 Then
                        Dim qry As String = " select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strBankOpeningClearingAccount + "'"
                        Dim check As String = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled Bank Opening Clearing Account (" & strBankOpeningClearingAccount & ")  does not exist" + Environment.NewLine + " at Bank Code. " + clsCommon.myCstr(strcode) + ".")
                        End If
                    End If

                    Dim strBankGroup As String = clsCommon.myCstr(grow.Cells("Bank Group Code").Value)
                    If clsCommon.myLen(strBankGroup) > 0 Then
                        Dim BankGroupCount As Int16 = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_BANK_GROUP_MASTER where TSPL_BANK_GROUP_MASTER.BANK_GROUP_CODE ='" & strBankGroup & "'", trans)
                        If BankGroupCount <= 0 Then
                            Throw New Exception("Please check ! Bank Group Code (" & strBankGroup & ") does not exists.")
                        End If
                    End If
                    ''------------------------

                    Dim sql1 As String = "select count(*) from tspl_Bank_master where bank_code='" + strcode + "'"
                    Dim i As Integer = clsDBFuncationality.getSingleValue(sql1, trans)
                    If (i = 0) Then

                        connectSql.RunSpTransaction(trans, "sp_BankMaster_insert", New SqlParameter("@bcode", strcode), New SqlParameter("@des", strdes), New SqlParameter("@add1", stradd1), New SqlParameter("@add2", stradd2), New SqlParameter("@add3", stradd3), New SqlParameter("@add4", stradd4), New SqlParameter("@city", strcity), New SqlParameter("@state", strstate), New SqlParameter("@postal", strpostal), New SqlParameter("@country", strcountry), New SqlParameter("@contact", strcontact), New SqlParameter("@phone", strphone), New SqlParameter("@fax", strfax), New SqlParameter("@inactive", strinactive), New SqlParameter("@bankaccnumber", strbaccno), New SqlParameter("@bankacc", strbaacc), New SqlParameter("@writeoffacc", strwriteoff), New SqlParameter("@creditacc", strcredit), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate(trans)), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode), New SqlParameter("@banktype", strbanktype), New SqlParameter("@CheckValidityInDays", strCheckValidityIndays))

                    Else
                        connectSql.RunSpTransaction(trans, "sp_BankMaster_update", New SqlParameter("@bcode", strcode), New SqlParameter("@des", strdes), New SqlParameter("@add1", stradd1), New SqlParameter("@add2", stradd2), New SqlParameter("@add3", stradd3), New SqlParameter("@add4", stradd4), New SqlParameter("@city", strcity), New SqlParameter("@state", strstate), New SqlParameter("@postal", strpostal), New SqlParameter("@country", strcountry), New SqlParameter("@contact", strcontact), New SqlParameter("@phone", strphone), New SqlParameter("@fax", strfax), New SqlParameter("@inactive", strinactive), New SqlParameter("@bankaccnumber", strbaccno), New SqlParameter("@bankacc", strbaacc), New SqlParameter("@writeoffacc", strwriteoff), New SqlParameter("@creditacc", strcredit), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode), New SqlParameter("@banktype", strbanktype))
                    End If
                    '' Richa Againt Ticket No. BM00000003641 on 27/08/2014
                    clsDBFuncationality.ExecuteNonQuery("update tspl_bank_master set cheque_validity_in_days=" & strChkValidity & ",LCCreditLimit=" & dblLCCreditLimit & ",FDPercentage=" & dblFDPer & ",IBAN_No='" & stribanno & "',Swift_Code='" & strswiftcode & "',Transfer_Clearing_Account='" + strTransferClearAC + "',Is_Clearance_Bank='" & strClearanceBank & "',Main_Bank_Code='" & strMainBank & "',BANK_GROUP_CODE=" & IIf(clsCommon.myLen(strBankGroup) > 0, "'" + strBankGroup + "'", "NULL") & "  where bank_code='" & strcode & "'", trans)
                    '' Anubhooti 04-Sep-2014 BM00000003437
                    Dim AllowUseSubAccount As String = ""
                    Dim SubAcc As String = ""
                    AllowUseSubAccount = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowToUseSubAccount, clsFixedParameterCode.AllowToUseSubAccount, trans))
                    If clsCommon.CompairString(AllowUseSubAccount, "1") = CompairStringResult.Equal Then
                        clsDBFuncationality.ExecuteNonQuery("Update TSPL_BANK_MASTER set Sub_Account='" & strSubAcc & "' where bank_code='" & strcode & "'", trans)
                    End If
                    If clsCommon.myLen(strBankOpeningClearingAccount) > 0 Then
                        clsDBFuncationality.ExecuteNonQuery("Update TSPL_BANK_MASTER set Bank_Opening_Clearing_Account='" & strBankOpeningClearingAccount & "' where bank_code='" & strcode & "'", trans)
                    End If
                    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strcode), "TSPL_BANK_MASTER", "bank_code", trans)
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, "Error at row No " + clsCommon.myCstr(ii) + Environment.NewLine + ex.Message)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub banlclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles banlclose.Click
        closeform()
    End Sub
    Public Sub closeform()
        Me.Close()
    End Sub
    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "BANK-M"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete
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
    '            btnsave.Enabled = False
    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access
    '            btndelete.Enabled = False
    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception
    '        myMessages.myExceptions(er)
    '    End Try
    'End Function


    Private Sub frmBankMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            closeform()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funreset()
        End If
    End Sub

    Private Sub fndbank__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndbank._MYValidating
        Dim qst As String = "select count(*) from TSPL_Bank_MASTER where bank_code='" + fndbank.Value + "'"
        Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
        If count = 0 Then
            fndbank.MyReadOnly = False
        Else
            fndbank.MyReadOnly = True
        End If

        If fndbank.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String '= " select bank_code As Code,description  as [Description]from TSPL_Bank_MASTER "
            'fndbank.Value = clsCommon.ShowSelectForm("fmBankMaster", qry, "Code", "", fndbank.Value, "bank_code", isButtonClicked)
            fndbank.Value = clsBankMaster.getFinder("", fndbank.Value, isButtonClicked)
            qry = "select description  from TSPL_Bank_MASTER where bank_code ='" + fndbank.Value + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                txtdes.Text = clsCommon.myCstr(dt.Rows(0)("description"))
            Else
                txtdes.Text = ""
            End If
            LoadData()
        End If
    End Sub

    Private Sub fndbank__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndbank._MYNavigator
        Dim qst As String = " select bank_code As [Bank Code],description  as [Description]from TSPL_Bank_MASTER  where 2=2"
        Select Case NavType
            Case NavigatorType.Current
                qst += " and TSPL_Bank_MASTER .bank_code in ('" + fndbank.Value + "')"
            Case NavigatorType.Next
                qst += " and TSPL_Bank_MASTER .bank_code in (select min(bank_code ) from TSPL_Bank_MASTER where bank_code  >'" + fndbank.Value + "')"
            Case NavigatorType.First
                qst += " and TSPL_Bank_MASTER .bank_code in (select MIN(bank_code ) from TSPL_Bank_MASTER)"

            Case NavigatorType.Last
                qst += " and TSPL_Bank_MASTER .bank_code in (select Max(bank_code ) from TSPL_Bank_MASTER)"
            Case NavigatorType.Previous
                qst += " and TSPL_Bank_MASTER .bank_code in (select Max(bank_code ) from TSPL_Bank_MASTER where bank_code  <'" + fndbank.Value + "')"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            fndbank.Value = clsCommon.myCstr(dt.Rows(0)("Bank Code"))
            txtdes.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
        End If
        LoadData()
    End Sub
    Public Sub LoadData()
        Try
            ''richa ERO/01/03/19-000503
            LoadBlankGrid()
            LoadBlankGridNEFT()
            Dim strquery As String = "select * from TSPL_Bank_MASTER where bank_code='" + fndbank.Value + "'"
            Dim dt As DataTable
            Dim trs As String = fndbank.Tag
            Dim strvalue As String = ""
            dt = clsDBFuncationality.GetDataTable(strquery)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

                For Each row As DataRow In dt.Rows
                    strvalue = row(0).ToString()
                Next
            End If
            If strvalue <> "" Then
                funfill()
                funfill1()
                funfill2()
                funfill3()
                funShowCheckData()
                funShowBankChargesSlab()
            Else
                txtdes.Text = ""
                chkactive.Checked = False
                chkProvisionBank.Checked = False
                chkSettlementBankForAD.Checked = False
                txtbankaccno.Text = ""
                fndbankacc.Value = ""
                fndwriteoff.Value = ""
                fndcredit.Value = ""
                txtbankacc.Text = ""
                txtwriteoff.Text = ""
                txtcredit.Text = ""
                txtadd1.Text = ""
                txtadd2.Text = ""
                txtadd3.Text = ""
                txtadd4.Text = ""
                txtcity.Text = ""
                txtstate.Text = ""
                txtzip.Text = ""
                txtcountry.Text = ""
                txtcontact.Text = ""
                txtphone.Text = ""
                txtfax.Text = ""
                ddlbanktype.Text = "Select"
                chkProvisionBank.Checked = False
                btnsave.Text = "Save"
                btndelete.Enabled = False
                txtChequeValidity.Text = "0"
                '' Richa Againt Ticket No. BM00000003641 on 27/08/2014
                txtLCCreditLimit.Value = 0
                txtFDPer.Value = 0
                '''''----------------------------------
                ''richa agarwal 12/03/2015
                TxtIbanno.Text = ""
                txtswiftcode.Text = ""
                ''--------------

                '' Anubhooti 03-Sep-2014
                fndSubAcc.Value = ""
                txtSubAcc.Text = ""
                fndtransferclearing.Value = ""
                txttransferclearing.Text = ""
                chkClearanceBank.Checked = False
                TxtMainBankCode.Value = ""
                TxtMainBankName.Text = ""
                txtEmail.Text = ""
            End If
            SetBankType()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub fndbankacc__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndbankacc._MYValidating
        'fndbankacc.Value = clsCommon.ShowSelectForm("fmBankACC", qry, "Code", "", fndbankacc.Value, "account_code", isButtonClicked)
        fndbankacc.Value = clsGLAccount.getFinder(strAccountFilter, fndbankacc.Value, isButtonClicked)
        qry = "select description  from Tspl_gl_Accounts where account_code ='" + fndbankacc.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            txtbankacc.Text = clsCommon.myCstr(dt.Rows(0)("description"))
        Else
            txtbankacc.Text = ""
        End If

    End Sub

    Private Sub fndwriteoff__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndwriteoff._MYValidating
        'fndwriteoff.Value = clsCommon.ShowSelectForm("fmWriteOff", qry, "Code", "", fndwriteoff.Value, "account_code", isButtonClicked)
        fndwriteoff.Value = clsGLAccount.getFinder(strAccountFilter, fndwriteoff.Value, isButtonClicked)
        qry = "select description  from Tspl_gl_Accounts where account_code ='" + fndwriteoff.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            txtwriteoff.Text = clsCommon.myCstr(dt.Rows(0)("description"))
        Else
            txtwriteoff.Text = ""
        End If

    End Sub

    Private Sub fndcredit__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndcredit._MYValidating
        'fndcredit.Value = clsCommon.ShowSelectForm("fmCredit", qry, "Code", "", fndcredit.Value, "account_code", isButtonClicked)
        fndcredit.Value = clsGLAccount.getFinder(strAccountFilter, fndcredit.Value, isButtonClicked)
        qry = "select description  from Tspl_gl_Accounts where account_code ='" + fndcredit.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            txtcredit.Text = clsCommon.myCstr(dt.Rows(0)("description"))
        Else
            txtcredit.Text = ""
        End If
    End Sub

    Private Sub gv1_CellEditorInitialized(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellEditorInitialized
        If gv1.CurrentColumn Is gv1.Columns(colNEXT_CHECK_NUMBER) Then
            Dim editor As Telerik.WinControls.UI.RadTextBoxEditor = TryCast(gv1.ActiveEditor, RadTextBoxEditor)
            Dim oszlop As Telerik.WinControls.UI.GridViewTextBoxColumn = TryCast(gv1.CurrentColumn, Telerik.WinControls.UI.GridViewTextBoxColumn)
            If editor IsNot Nothing And oszlop IsNot Nothing Then
                Dim editorElement As Telerik.WinControls.UI.RadTextBoxElement = TryCast(editor.EditorElement, RadTextBoxElement)

                Try
                    RemoveHandler editorElement.KeyPress, AddressOf gv1_KeyPress
                Catch ex As Exception
                End Try
                AddHandler editorElement.KeyPress, AddressOf gv1_KeyPress
            End If
        End If
    End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        Dim intCurrRow As Integer = gv1.CurrentRow.Index
        If intCurrRow = gv1.Rows.Count - 1 Then
            gv1.Rows.AddNew()
            gv1.CurrentRow = gv1.Rows(intCurrRow)
        End If

    End Sub

    '' Anubhooti 03-Sep-2014 BM00000003437
    Private Sub fndSubAcc__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndSubAcc._MYValidating
        fndSubAcc.Value = clsGLAccount.getFinder(strAccountFilter, fndSubAcc.Value, isButtonClicked)
        If clsCommon.myLen(fndSubAcc.Value) > 0 Then
            txtSubAcc.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + fndSubAcc.Value + "' ")
        Else
            txtSubAcc.Text = ""
        End If
    End Sub

    Private Sub ddlbanktype_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles ddlbanktype.SelectedIndexChanged, ddlbanktype.SelectedValueChanged
        SetBankType()
    End Sub

    Sub SetBankType()
        If clsCommon.CompairString(ddlbanktype.Text, "Bank") = CompairStringResult.Equal Then
            fndSubAcc.MendatroryField = True
            pnlGatewayType.Visible = True
        Else
            fndSubAcc.MendatroryField = False
            pnlGatewayType.Visible = False
        End If
        Dim UseSubAccount As String
        UseSubAccount = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowToUseSubAccount, clsFixedParameterCode.AllowToUseSubAccount, Nothing))
        If clsCommon.CompairString(UseSubAccount, "1") = CompairStringResult.Equal AndAlso clsCommon.CompairString(ddlbanktype.Text, "Bank") = CompairStringResult.Equal Then
            fndSubAcc.Visible = True
            txtSubAcc.Visible = True
            MyLabel4.Visible = True
        Else
            fndSubAcc.Visible = False
            txtSubAcc.Visible = False
            MyLabel4.Visible = False
        End If
    End Sub
    Public Sub AllowOnlyNumeric(ByRef e As KeyPressEventArgs, Optional ByVal AllowedChar As String = "")
        Dim strAllowed As String() = AllowedChar.Split(",")
        Dim ienum As IEnumerator = strAllowed.GetEnumerator

        While (ienum.MoveNext)
            If e.KeyChar.ToString().ToLower = ienum.Current.ToString().ToLower Then
                Return
            End If
        End While

        If Not (IsNumeric(e.KeyChar) Or Asc(e.KeyChar) = 8 Or Asc(e.KeyChar) = 9) Then
            e.Handled = True
        End If

    End Sub
    Private Sub gv1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv1.KeyPress
        If gv1.CurrentColumn Is gv1.Columns(colNEXT_CHECK_NUMBER) Or gv1.CurrentColumn Is gv1.Columns(colLAST_CHECK_NUMBER) Then
            AllowOnlyNumeric(e, "")
        End If
    End Sub

    Private Sub fndtransferclearing__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndtransferclearing._MYValidating
        fndtransferclearing.Value = clsGLAccount.getFinder(strAccountFilter, fndtransferclearing.Value, isButtonClicked)
        If clsCommon.myLen(fndtransferclearing.Value) > 0 Then
            txttransferclearing.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + fndtransferclearing.Value + "' ")
        Else
            txttransferclearing.Text = ""
        End If
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        'For ii As Integer = 1 To gv1.Rows.Count
        '    If clsCommon.myCdbl(gv1.Rows(ii - 1).Cells(colLAST_PRINT_CHECK_NUMBER).Value) > 0 Then
        '        e.Cancel = True
        '    Else
        '        e.Cancel = False
        '    End If
        'Next
        If clsCommon.myCdbl(gv1.CurrentRow.Cells(clsCommon.myCstr(colLAST_PRINT_CHECK_NUMBER)).Value) <= 0 Then
            If common.clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
                e.Cancel = True
            End If
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub fndBranchName__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndBranchName._MYValidating
        Dim qry As String = "select BRANCH_CODE as Code,BRANCH_NAME as Name from tspl_bank_Branch_master"
        fndBranchName.Value = clsCommon.ShowSelectForm("fmBankbranchMaster", qry, "Code", "", fndbank.Value, "BRANCH_CODE", isButtonClicked)
    End Sub

    Private Sub chkClearanceBank_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkClearanceBank.ToggleStateChanged
        If chkClearanceBank.Checked Then
            TxtMainBankCode.Enabled = True
            TxtMainBankCode.Value = ""
            TxtMainBankName.Text = ""
        Else
            TxtMainBankCode.Enabled = False
            TxtMainBankCode.Value = ""
            TxtMainBankName.Text = ""
        End If
    End Sub

    Private Sub TxtMainBankCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtMainBankCode._MYValidating
        Dim strWhrclas As String = String.Empty
        Qry = clsERPFuncationality.glbankqueryNew(strWhrclas)
     
        strWhrclas += "  and isnull(TSPL_BANK_MASTER.Is_Clearance_Bank,'')='N'"
        TxtMainBankCode.Value = clsCommon.ShowSelectForm("MainBank@bankmaster", Qry, "Code", strWhrclas, TxtMainBankCode.Value, "Code", isButtonClicked)
        TxtMainBankName.Text = connectSql.RunScalar("select description from TSPL_BANK_MASTER where bank_code = '" + TxtMainBankCode.Value + "'")

    End Sub

    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(fndbank.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Select Bank")
                Exit Sub
            End If
            clsERPFuncationalityOLD.ShowHistoryData(fndbank.Value, "Bank_Code", "TSPL_BANK_MASTER")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub



    Private Sub fndBankOpeningClearingAcount__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndBankOpeningClearingAcount._MYValidating
        fndBankOpeningClearingAcount.Value = clsGLAccount.getFinder(strAccountFilter, fndBankOpeningClearingAcount.Value, isButtonClicked)
        If clsCommon.myLen(fndBankOpeningClearingAcount.Value) > 0 Then
            lblBankClearingAcount.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + fndBankOpeningClearingAcount.Value + "' ")
        Else
            lblBankClearingAcount.Text = ""
        End If
    End Sub


    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Try
            If gvNEFTPerforma.Rows.Count <= 0 Then
                RadButton2.PerformClick()
            End If
            gvNEFTPerforma.Rows.AddNew()
            gvNEFTPerforma.Rows(gvNEFTPerforma.Rows.Count - 1).Cells(ColNEFTCode).Value = "CR" + clsCommon.myCstr(gvNEFTPerforma.Rows.Count)
            gvNEFTPerforma.Rows(gvNEFTPerforma.Rows.Count - 1).Cells(ColNEFTHide).Value = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtBankGroup__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtBankGroup._MYValidating
        Try
            Dim qry As String = "select BANK_GROUP_CODE as Code,DESCRIPTION as Name from TSPL_BANK_GROUP_MASTER"
            txtBankGroup.Value = clsCommon.ShowSelectForm("fmBankGroupMaster", qry, "Code", "", txtBankGroup.Value, "BANK_GROUP_CODE", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton2_Click(sender As Object, e As EventArgs) Handles RadButton2.Click
        Try
            LoadBlankGridNEFT()
            Dim dt As DataTable = clsDBTNEFTPerforma.GetDefault()
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    gvNEFTPerforma.Rows.AddNew()
                    gvNEFTPerforma.Rows(gvNEFTPerforma.Rows.Count - 1).Cells(ColNEFTCode).Value = clsCommon.myCstr(dr("Code"))
                    gvNEFTPerforma.Rows(gvNEFTPerforma.Rows.Count - 1).Cells(ColNEFTName).Value = clsCommon.myCstr(dr("Code"))
                    gvNEFTPerforma.Rows(gvNEFTPerforma.Rows.Count - 1).Cells(ColNEFTHide).Value = False
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
