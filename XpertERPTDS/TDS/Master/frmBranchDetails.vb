'Created By---> Mayank
'Created Date--->15/june/2011
'Modified By--> Mayank
'Last Modify Date-->20/june/2011
'Tables Used-->TSPL_GL_ACCOUNTS,TSPL_BANK_MASTER,TSPL_BANK_MASTER,TSPL_TDS_RESP_PERSON,
'--preeti gupta..ticket no.[BM00000003134]
Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.WinControls.Enumerations
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Text.RegularExpressions
Imports common

Imports System.Data
Imports XpertERPEngine

Public Class frmBranchDetails
    Inherits FrmMainTranScreen
    Dim userCode, companyCode As String
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.BranchDetails)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            Me.Close()
        End If
        rbtnSave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 02/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If rbtnSave.Visible = True Then
            RadMenuItemImport.Enabled = True
            RadMenuItemExport.Enabled = True
        Else
            RadMenuItemImport.Enabled = False
            RadMenuItemExport.Enabled = False
        End If
        '--------------------------------------------------
        'btnPost.Visible = MyBase.isPostFlag
        rbtnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub frmBranchDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso rbtnSave.Enabled Then
            SaveData()
            'ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            '    PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso rbtnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub
    Private Sub FrmBranchDetails_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        'globalFunc.mandatoryText(fndBranchCode.txtValue, txtBranchName) 
        ''AddHandler fndBranchCode.txtValue.TextChanged, AddressOf text_changed
        ''AddHandler fndBranchCode.txtValue.KeyPress, AddressOf fndBranchCode_keyPress

        ''AddHandler fndTaxAccount.txtValue.TextChanged, AddressOf text_changed1
        ''AddHandler fndTaxAccount.txtValue.Leave, AddressOf fndTaxAccount_Leave
        ''AddHandler fndTaxAccount.txtValue.KeyPress, AddressOf fndTaxAccount_KeyPress

        ''AddHandler fndInterestAccount.txtValue.TextChanged, AddressOf text_changed2
        ''AddHandler fndInterestAccount.txtValue.Leave, AddressOf fndInterestAccount_Leave
        ''AddHandler fndInterestAccount.txtValue.KeyPress, AddressOf fndInterestAccount_KeyPress


        ''AddHandler fndOtherAccount.txtValue.TextChanged, AddressOf text_changed3
        ''AddHandler fndOtherAccount.txtValue.Leave, AddressOf fndOtherAccount_Leave
        ''AddHandler fndOtherAccount.txtValue.KeyPress, AddressOf fndOtherAccount_KeyPress

        ''AddHandler fndPenaltyAccount.txtValue.TextChanged, AddressOf text_changed4
        ''AddHandler fndPenaltyAccount.txtValue.Leave, AddressOf fndPenaltyAccount_Leave
        ''AddHandler fndPenaltyAccount.txtValue.KeyPress, AddressOf fndPenaltyAccount_KeyPress

        ''AddHandler fndBank.txtValue.TextChanged, AddressOf text_changed_Bank
        ''AddHandler fndBank.txtValue.Leave, AddressOf fndBank_Leave
        ''AddHandler fndBank.txtValue.KeyPress, AddressOf fndBank_KeyPress

        ''AddHandler fndStateCode.txtValue.TextChanged, AddressOf text_changed_State
        ''AddHandler fndStateCode.txtValue.Leave, AddressOf fndStateCode_Leave
        ''AddHandler fndStateCode.txtValue.KeyPress, AddressOf fndStateCode_KeyPress

        ''AddHandler fndResponsiblePerson.txtValue.TextChanged, AddressOf text_changed_Resp_Person
        ''AddHandler fndResponsiblePerson.txtValue.Leave, AddressOf fndResponsiblePerson_Leave
        ''AddHandler fndResponsiblePerson.txtValue.KeyPress, AddressOf fndResponsiblePerson_KeyPress
        FndBranchCodeNew.MyCharacterCasing = CharacterCasing.Upper
        rbtnDelete.Enabled = False
        rtxtdate.Visible = False
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If

        SetLength()

    End Sub

    Public Sub SetLength()
        FndBranchCodeNew.MyMaxLength = 12
        txtBranchName.MaxLength = 50
        rtxtCircleCode.MaxLength = 50
        rtxtRemitTo.MaxLength = 50
    End Sub




    'Private Sub fndBranchCode_keyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

    '    If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
    '        e.Handled = True
    '    End If
    'End Sub



    'Private Sub fndResponsiblePerson_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    '    If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
    '        e.Handled = True
    '    End If
    'End Sub

    Private Sub rbtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnSave.Click
        SaveData()
    End Sub
    Sub SaveData()
        If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False) = False) AndAlso FndBranchCodeNew.Value = "" Then
            myMessages.blankValue("Branch Code")
            FndBranchCodeNew.Focus()
        ElseIf txtBranchName.Text = "" Then
            myMessages.blankValue("Branch Name")
            txtBranchName.Focus()
        ElseIf rbtnSave.Text = "Save" Then
            funInsert()
        Else
            funUpdate()
        End If
    End Sub
    Private Sub funInsert()
        Try
            Dim chk As String
            If rchkInactive.Checked = True Then
                chk = "T"
            Else
                chk = "F"
            End If
            If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False)) Then
                Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_TDS_BRANCH_MASTER where Branch_Code='" & FndBranchCodeNew.Value & "'")
                If ChkNewEntry = 0 Then
                    FndBranchCodeNew.Value = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"), clsDocType.BranchDetails, "", "")
                    If clsCommon.myLen(FndBranchCodeNew.Value) <= 0 Then
                        Throw New Exception("Error in Code Generation")
                    End If
                End If
            End If
            connectSql.RunSp("SP_TSPL_TDS_BRANCH_MASTER_INSERT", New SqlParameter("@Branch_Code", FndBranchCodeNew.Value), New SqlParameter("@Branch_Name", txtBranchName.Text), New SqlParameter("@Tax_Account", fndTaxAccountNew.Value), New SqlParameter("@TaxAcct_Description", rtxtTaxAccount.Text), New SqlParameter("@Interest_Account", FndInterestAccountNew.Value), New SqlParameter("@Interest_Acc_Desc", rtxtInterestAccount.Text), New SqlParameter("@Others_Account", fndOtherAccountNew.Value), New SqlParameter("@Other_Acct_Desc", rtxtOtherAccount.Text), New SqlParameter("@Penalty_Account", fndPenaltyAccountNew.Value), New SqlParameter("@Penalty_Acct_Desc", rtxtPenaltyAccount.Text), New SqlParameter("@Circle_Code", rtxtCircleCode.Text), New SqlParameter("@Bank_Code", fndBankNew.Value), New SqlParameter("@Bank_Name", rtxtBank.Text), New SqlParameter("@Remit_To", rtxtRemitTo.Text), New SqlParameter("@Resp_Person", fndResponsiblePersonNew.Value), New SqlParameter("@Person_Name", rtxtResponsiblePerson.Text), New SqlParameter("@State_Code", fndStateCodeNew.Value), New SqlParameter("@State_Name", rtxtStateCode.Text), New SqlParameter("@Inactive", chk), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate()), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate()), New SqlParameter("@Comp_Code", companyCode))
            myMessages.insert()
            rbtnSave.Text = "Update"
            rbtnDelete.Enabled = True
            'If userCode <> "ADMIN" Then
            '    If funSetUserAccess() = False Then Exit Sub
            'End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Private Sub funUpdate()
        Try
            Dim chk As String
            If rchkInactive.Checked = True Then
                chk = "T"
            Else
                chk = "F"
            End If
            connectSql.RunSp("SP_TSPL_TDS_BRANCH_MASTER_UPDATE", New SqlParameter("@Branch_Code", FndBranchCodeNew.Value), New SqlParameter("@Branch_Name", txtBranchName.Text), New SqlParameter("@Tax_Account", fndTaxAccountNew.Value), New SqlParameter("@TaxAcct_Description", rtxtTaxAccount.Text), New SqlParameter("@Interest_Account", FndInterestAccountNew.Value), New SqlParameter("@Interest_Acc_Desc", rtxtInterestAccount.Text), New SqlParameter("@Others_Account", fndOtherAccountNew.Value), New SqlParameter("@Other_Acct_Desc", rtxtOtherAccount.Text), New SqlParameter("@Penalty_Account", fndPenaltyAccountNew.Value), New SqlParameter("@Penalty_Acct_Desc", rtxtPenaltyAccount.Text), New SqlParameter("@Circle_Code", rtxtCircleCode.Text), New SqlParameter("@Bank_Code", fndBankNew.Value), New SqlParameter("@Bank_Name", rtxtBank.Text), New SqlParameter("@Remit_To", rtxtRemitTo.Text), New SqlParameter("@Resp_Person", fndResponsiblePersonNew.Value), New SqlParameter("@Person_Name", rtxtResponsiblePerson.Text), New SqlParameter("@State_Code", fndStateCodeNew.Value), New SqlParameter("@State_Name", rtxtStateCode.Text), New SqlParameter("@Inactive", chk), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate()), New SqlParameter("@Comp_Code", companyCode))
            myMessages.update()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    ''Private Sub fndBranchCode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fndBranchCode.Load
    ''    fndBranchCode.ConnectionString = connectSql.SqlCon()
    ''    fndBranchCode.Query = "select Branch_Code as [Branch Code],Branch_Name as [Branch Name], State_Code as [State Code],State_Name as [State Name] from TSPL_TDS_BRANCH_MASTER"
    ''    fndBranchCode.ValueToSelect = "Branch Code"
    ''    fndBranchCode.Caption = "Branch Master"
    ''    fndBranchCode.txtValue.MaxLength = 12
    ''    fndBranchCode.ValueToSelect1 = "Branch Name"
    ''End Sub
    'Public Sub text_changed(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    LoadData()
    'End Sub
    Public Sub LoadData()
        Try
            Dim strB_Code As String = "select Branch_Code from TSPL_TDS_BRANCH_MASTER where Branch_Code='" + FndBranchCodeNew.Value + "'"

            Dim strvalue As String
            strvalue = clsDBFuncationality.getSingleValue(strB_Code)
            If (strvalue <> "") Then
                funfill()
            Else
                txtBranchName.Text = ""
                fndTaxAccountNew.Value = ""
                rtxtTaxAccount.Text = ""
                FndInterestAccountNew.Value = ""
                rtxtInterestAccount.Text = ""
                fndOtherAccountNew.Value = ""
                rtxtOtherAccount.Text = ""
                fndPenaltyAccountNew.Value = ""
                rtxtPenaltyAccount.Text = ""
                rtxtCircleCode.Text = ""
                fndBankNew.Value = ""
                rtxtBank.Text = ""
                rtxtRemitTo.Text = ""
                fndResponsiblePersonNew.Value = ""
                rtxtResponsiblePerson.Text = ""
                fndStateCodeNew.Value = ""
                rtxtStateCode.Text = ""
                rchkInactive.Checked = False
                rtxtdate.Visible = False
                rbtnSave.Text = "Save"
                rbtnDelete.Enabled = False
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Private Sub funfill()
        Try
            Dim strQuery As String = "select Branch_Name,Tax_Account,TaxAcct_Description,Interest_Account,Interest_Acc_Desc,Others_Account,Other_Acct_Desc,Penalty_Account,Penalty_Acct_Desc,Circle_Code,Bank_Code,Bank_Name,Remit_To,Resp_Person,Person_Name,State_Code,State_Name,Inactive from TSPL_TDS_BRANCH_MASTER where Branch_Code='" + FndBranchCodeNew.Value + "'"
            Dim dt As DataTable
            dt = clsDBFuncationality.GetDataTable(strQuery)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1

                    txtBranchName.Text = dt.Rows(i)("Branch_Name").ToString()
                    fndTaxAccountNew.Value = dt.Rows(i)("Tax_Account")
                    rtxtTaxAccount.Text = dt.Rows(i)("TaxAcct_Description")
                    FndInterestAccountNew.Value = dt.Rows(i)("Interest_Account")
                    rtxtInterestAccount.Text = dt.Rows(i)("Interest_Acc_Desc")
                    fndOtherAccountNew.Value = dt.Rows(i)("Others_Account")
                    rtxtOtherAccount.Text = dt.Rows(i)("Other_Acct_Desc")
                    fndPenaltyAccountNew.Value = dt.Rows(i)("Penalty_Account")
                    rtxtPenaltyAccount.Text = dt.Rows(i)("Penalty_Acct_Desc")
                    rtxtCircleCode.Text = dt.Rows(i)("Circle_Code")
                    fndBankNew.Value = dt.Rows(i)("Bank_Code")
                    rtxtBank.Text = dt.Rows(i)("Bank_Name")
                    rtxtRemitTo.Text = dt.Rows(i)("Remit_To")
                    fndResponsiblePersonNew.Value = dt.Rows(i)("Resp_Person")
                    rtxtResponsiblePerson.Text = dt.Rows(i)("Person_Name")
                    fndStateCodeNew.Value = dt.Rows(i)("State_Code")
                    rtxtStateCode.Text = dt.Rows(i)("State_Name")
                    Dim sts As String = dt.Rows(i)("Inactive")

                    If sts = "T" Then
                        rchkInactive.Checked = True
                        rtxtdate.Visible = True
                        rtxtdate.Text = Date.Today()
                    Else
                        rchkInactive.Checked = False
                    End If
                    rbtnDelete.Enabled = True
                    rbtnSave.Text = "Update"
                Next
            End If




        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub rdbtnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnRefresh.Click
        Try
            funreset()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Private Sub funreset()
        FndBranchCodeNew.Value = ""
        FndBranchCodeNew.MyReadOnly = False
        txtBranchName.Text = ""
        fndTaxAccountNew.Value = ""
        rtxtTaxAccount.Text = ""
        FndInterestAccountNew.Value = ""
        rtxtInterestAccount.Text = ""
        fndOtherAccountNew.Value = ""
        rtxtOtherAccount.Text = ""
        fndPenaltyAccountNew.Value = ""
        rtxtPenaltyAccount.Text = ""
        rtxtCircleCode.Text = ""
        fndBankNew.Value = ""
        rtxtBank.Text = ""
        rtxtRemitTo.Text = ""
        fndResponsiblePersonNew.Value = ""
        rtxtResponsiblePerson.Text = ""
        fndStateCodeNew.Value = ""
        rtxtStateCode.Text = ""
        rtxtdate.Visible = False
        rchkInactive.Checked = False
        rbtnSave.Text = "Save"
        rbtnDelete.Enabled = False
    End Sub

    Private Sub rbtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnDelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If FndBranchCodeNew.Value = "" Then
            myMessages.blankValue("Branch Code")
        ElseIf myMessages.deleteConfirm() Then
            funDelete()
            FndBranchCodeNew.Enabled = True
            rbtnSave.Text = "Save"
            rbtnDelete.Enabled = False
        End If
    End Sub
    Private Sub funDelete()
        Try
            connectSql.RunSp("SP_TSPL_TDS_BRANCH_MASTER_DELETE", New SqlParameter("@Branch_Code", FndBranchCodeNew.Value))
            myMessages.delete()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Private Sub RadMenuItemExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemExport.Click
        Dim strSql As String = "Select Branch_Code as [Branch Code],Branch_Name as [Branch Name], Tax_Account as [Tax Account],TaxAcct_Description as [TaxAcct Description],Interest_Account as [Interest Account],Interest_Acc_Desc as [Interest Acc Desc],Others_Account as [Others Account] ,Other_Acct_Desc as [Other Acct Desc],Penalty_Account as [Penalty Account],Penalty_Acct_Desc as [Penalty Acct Desc],Circle_Code as [Circle Code],Bank_Code as[Bank Code],Bank_Name as [Bank Name],Remit_To as [Remit To],Resp_Person as [Resp Person],Person_Name as [Person Name],State_Code as [State Code],State_Name as [State Name],Inactive from TSPL_TDS_BRANCH_MASTER"
        transportSql.ExporttoExcel(strSql, Me)
    End Sub
    Private Sub RadMenuItemImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "Branch Code", "Branch Name", "Tax Account", "TaxAcct Description", "Interest Account", "Interest Acc Desc", "Others Account", "Other Acct Desc", "Penalty Account", "Penalty Acct Desc", "Circle Code", "Bank Code", "Bank Name", "Remit To", "Resp Person", "Person Name", "State Code", "State Name", "Inactive") Then
            Dim trans As SqlTransaction = Nothing
            Try
                connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim strB_Code As String = clsCommon.myCstr(grow.Cells(0).Value)
                    If String.IsNullOrEmpty(strB_Code) Or clsCommon.myLen(strB_Code) > 12 Then
                        Throw New Exception("Branch Code Can not be left Blank or size can not be grater than 12")
                    End If

                    Dim strB_Name As String = clsCommon.myCstr(grow.Cells(1).Value)
                    If String.IsNullOrEmpty(strB_Name) Or clsCommon.myLen(strB_Name) > 50 Then
                        Throw New Exception("Branch Name Can not be left Blank or size can not be grater than 50")
                    End If

                    Dim strT_Account As String = clsCommon.myCstr(grow.Cells(2).Value)
                    If clsCommon.myLen(strT_Account) > 50 Then
                        Throw New Exception("Tax Account can not be grater than 50")
                    End If

                    Dim strT_desc As String = clsCommon.myCstr(grow.Cells(3).Value)
                    If clsCommon.myLen(strT_desc) > 100 Then
                        Throw New Exception("Tax Account Description can not be grater than 100")
                    End If

                    Dim strintr_account As String = clsCommon.myCstr(grow.Cells(4).Value)
                    If clsCommon.myLen(strintr_account) > 50 Then
                        Throw New Exception("Interest Account can not be grater than 50")
                    End If

                    Dim strintr_account_desc As String = clsCommon.myCstr(grow.Cells(5).Value)
                    If clsCommon.myLen(strintr_account_desc) > 100 Then
                        Throw New Exception("Interest Account Description can not be greater than 100")
                    End If

                    Dim strOther_Account As String = clsCommon.myCstr(grow.Cells(6).Value)
                    If clsCommon.myLen(strOther_Account) > 50 Then
                        Throw New Exception("Other Account can not be greater than 50")
                    End If

                    Dim strOther_Account_desc As String = clsCommon.myCstr(grow.Cells(7).Value)
                    If clsCommon.myLen(strOther_Account_desc) > 100 Then
                        Throw New Exception("Other Account Description can not be grater than 100")
                    End If

                    Dim strPanalty_Acct As String = clsCommon.myCstr(grow.Cells(8).Value)
                    If clsCommon.myLen(strPanalty_Acct) > 50 Then
                        Throw New Exception("Panalty Account can not be greater than 50")
                    End If

                    Dim strPanalty_Acct_desc As String = clsCommon.myCstr(grow.Cells(9).Value)
                    If clsCommon.myLen(strPanalty_Acct_desc) > 100 Then
                        Throw New Exception("Panalty Account description can not be greater than 100")
                    End If

                    Dim strcircle_code As String = clsCommon.myCstr(grow.Cells(10).Value)
                    If clsCommon.myLen(strcircle_code) > 50 Then
                        Throw New Exception("Circle Code can not be grater than 50")
                    End If

                    Dim strBank_Code As String = clsCommon.myCstr(grow.Cells(11).Value)
                    If clsCommon.myLen(strBank_Code) > 12 Then
                        Throw New Exception("Bank Code can not be grater than 12")
                    End If

                    Dim strBank_Name As String = clsCommon.myCstr(grow.Cells(12).Value)
                    If clsCommon.myLen(strBank_Name) > 50 Then
                        Throw New Exception("Bank Name can not be greater than 50")
                    End If

                    Dim strRemit_to As String = clsCommon.myCstr(grow.Cells(13).Value)
                    If clsCommon.myLen(strRemit_to) > 50 Then
                        Throw New Exception("Remit To can not be greater than 100")
                    End If

                    Dim strResp_Person As String = clsCommon.myCstr(grow.Cells(14).Value)
                    If clsCommon.myLen(strResp_Person) > 12 Then
                        Throw New Exception("Responsible person can not be grater than 12")
                    End If

                    Dim strPerson_name As String = clsCommon.myCstr(grow.Cells(15).Value)
                    If clsCommon.myLen(strPerson_name) > 50 Then
                        Throw New Exception("Person Name can not be grater than 50")
                    End If

                    Dim strState_Code As String = clsCommon.myCstr(grow.Cells(16).Value)
                    If clsCommon.myLen(strState_Code) > 12 Then
                        Throw New Exception("State Code can not be greater than 12")
                    End If

                    Dim strState_Name As String = clsCommon.myCstr(grow.Cells(17).Value)
                    If clsCommon.myLen(strState_Name) > 50 Then
                        Throw New Exception("State Name can not be grater than 50")
                    End If

                    Dim strInactive As String = clsCommon.myCstr(grow.Cells(18).Value)
                    If strInactive <> "" Then
                        If strInactive = "T" Then
                            strInactive = "T"
                        ElseIf strInactive = "F" Then
                            strInactive = "F"
                        Else
                            Throw New Exception("Please Insert T or F for Inactive,Size Can Not be greater than 1 character")
                        End If
                    End If

                    Dim strquery As String = "select count(*) from TSPL_TDS_BRANCH_MASTER where Branch_Code='" + strB_Code + "'"
                    Dim i As Integer = CInt(connectSql.RunScalar(trans, strquery))
                    If (i = 0) Then
                        connectSql.RunSpTransaction(trans, "SP_TSPL_TDS_BRANCH_MASTER_INSERT", New SqlParameter("@Branch_Code", strB_Code), New SqlParameter("@Branch_Name", strB_Name), New SqlParameter("@Tax_Account", strT_Account), New SqlParameter("@TaxAcct_Description", strT_desc), New SqlParameter("@Interest_Account", strintr_account), New SqlParameter("@Interest_Acc_Desc", strintr_account_desc), New SqlParameter("@Others_Account", strOther_Account), New SqlParameter("@Other_Acct_Desc", strOther_Account_desc), New SqlParameter("@Penalty_Account", strPanalty_Acct), New SqlParameter("@Penalty_Acct_Desc", strPanalty_Acct_desc), New SqlParameter("@Circle_Code", strcircle_code), New SqlParameter("@Bank_Code", strBank_Code), New SqlParameter("@Bank_Name", strBank_Name), New SqlParameter("@Remit_To", strRemit_to), New SqlParameter("@Resp_Person", strResp_Person), New SqlParameter("@Person_Name", strPerson_name), New SqlParameter("@State_Code", strState_Code), New SqlParameter("@State_Name", strState_Name), New SqlParameter("@Inactive", strInactive), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate(trans)), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", companyCode))
                    Else
                        connectSql.RunSpTransaction(trans, "SP_TSPL_TDS_BRANCH_MASTER_UPDATE", New SqlParameter("@Branch_Code", strB_Code), New SqlParameter("@Branch_Name", strB_Name), New SqlParameter("@Tax_Account", strT_Account), New SqlParameter("@TaxAcct_Description", strT_desc), New SqlParameter("@Interest_Account", strintr_account), New SqlParameter("@Interest_Acc_Desc", strintr_account_desc), New SqlParameter("@Others_Account", strOther_Account), New SqlParameter("@Other_Acct_Desc", strOther_Account_desc), New SqlParameter("@Penalty_Account", strPanalty_Acct), New SqlParameter("@Penalty_Acct_Desc", strPanalty_Acct_desc), New SqlParameter("@Circle_Code", strcircle_code), New SqlParameter("@Bank_Code", strBank_Code), New SqlParameter("@Bank_Name", strBank_Name), New SqlParameter("@Remit_To", strRemit_to), New SqlParameter("@Resp_Person", strResp_Person), New SqlParameter("@Person_Name", strPerson_name), New SqlParameter("@State_Code", strState_Code), New SqlParameter("@State_Name", strState_Name), New SqlParameter("@Inactive", strInactive), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", companyCode))
                    End If
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)

            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub
    Private Sub rchkInactive_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles rchkInactive.MouseClick

    End Sub

    Private Sub rchkInactive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rchkInactive.Click
        If rchkInactive.Checked = False Then
            rtxtdate.Visible = True
            rtxtdate.Text = Date.Today()
        Else
            rtxtdate.Visible = False
        End If
    End Sub

    Private Sub rbtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnClose.Click
        Me.Close()
    End Sub

    Private Sub RadMenuItemClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemClose.Click
        Me.Close()
    End Sub
    'It Is Used To Give The Authority To User,To Access This Form.(It Is Bassed On Mapping)
    '    Private Function funSetUserAccess() As Boolean
    '        Try
    '            Dim strRights As String
    '            Dim strTemp() As String
    '            Dim strProgCode = "BRANCH-DET"
    '            strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete
    '            strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
    '            strTemp = Split(strRights, ",")
    '            If strTemp(0) = "0" Then
    '                MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
    '                funSetUserAccess = False
    '                blnRead = False
    '                Me.Close()
    '                Exit Function
    '            Else
    '                blnRead = True
    '            End If
    '            If strTemp(1) = "0" Then 'Grant modify access
    '                rbtnSave.Enabled = False
    '            End If
    '            If strTemp(2) = "0" Then 'Grant modify access
    '                rbtnDelete.Enabled = False
    '            End If

    '            funSetUserAccess = True
    '        Catch er As Exception

    '        End Try
    '    End Function

    ''------------- Replace  By Abhishek Old Finder to New Finder as On 6/june/2012 -------------

    Private Sub fndTaxAccountNew__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndTaxAccountNew._MYValidating
        'Dim qry As String = clsERPFuncationality.glaccountquery
        'fndTaxAccountNew.Value = clsCommon.ShowSelectForm("GLAccountTax", qry, "Account_Code", "", fndTaxAccountNew.Value, "Account_Code", isButtonClicked)

        Dim qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS"
        Dim whr As String = " ControlAccount ='N'"
        fndTaxAccountNew.Value = clsCommon.ShowSelectForm("GLAccountTax", qry, "Account_Code", whr, fndTaxAccountNew.Value, "Account_Code", isButtonClicked)



        Dim qry1 As String = "select Account_Code, Description from TSPL_GL_ACCOUNTS where Account_Code='" + fndTaxAccountNew.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry1)
        If (dt.Rows.Count > 0) Then
            rtxtTaxAccount.Text = dt.Rows(0)("Description")
        Else
            rtxtTaxAccount.Text = ""
        End If

    End Sub

    Private Sub fndTaxAccountNew_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles fndTaxAccountNew.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub FndInterestAccountNew__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles FndInterestAccountNew._MYValidating
        'Dim qry As String = clsERPFuncationality.glaccountquery
        'FndInterestAccountNew.Value = clsCommon.ShowSelectForm("GLAccountINt", qry, "Account_Code", "", FndInterestAccountNew.Value, "Account_Code", isButtonClicked)


        Dim qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS"
        Dim whr As String = " ControlAccount ='N'"
        FndInterestAccountNew.Value = clsCommon.ShowSelectForm("GLAccountINt", qry, "Account_Code", whr, FndInterestAccountNew.Value, "Account_Code", isButtonClicked)
        Dim qry1 As String = "select Description from TSPL_GL_ACCOUNTS where Account_Code='" + FndInterestAccountNew.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry1)
        If (dt.Rows.Count > 0) Then
            rtxtInterestAccount.Text = dt.Rows(0)("Description")
        Else
            rtxtInterestAccount.Text = ""
        End If
    End Sub

    Private Sub FndInterestAccountNew_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles FndInterestAccountNew.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub fndOtherAccountNew__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndOtherAccountNew._MYValidating
        'Dim qry As String = clsERPFuncationality.glaccountquery
        'fndOtherAccountNew.Value = clsCommon.ShowSelectForm("GLAccountoth", qry, "Account_Code", "", fndOtherAccountNew.Value, "Account_Code", isButtonClicked)




        Dim qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS"
        Dim whr As String = " ControlAccount ='N'"
        fndOtherAccountNew.Value = clsCommon.ShowSelectForm("GLAccountoth", qry, "Account_Code", whr, fndOtherAccountNew.Value, "Account_Code", isButtonClicked)
        Dim qry1 As String = "select Description from TSPL_GL_ACCOUNTS where Account_Code='" + fndOtherAccountNew.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry1)
        If (dt.Rows.Count > 0) Then
            rtxtOtherAccount.Text = dt.Rows(0)("Description")
        Else
            rtxtOtherAccount.Text = ""
        End If
    End Sub

    Private Sub fndOtherAccountNew_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles fndOtherAccountNew.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub fndPenaltyAccountNew__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndPenaltyAccountNew._MYValidating
        'Dim qry As String = clsERPFuncationality.glaccountquery
        'fndPenaltyAccountNew.Value = clsCommon.ShowSelectForm("GLAccountPen", qry, "Account_Code", "", fndPenaltyAccountNew.Value, "Account_Code", isButtonClicked)


        Dim qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS"
        Dim whr As String = " ControlAccount ='N'"

        fndPenaltyAccountNew.Value = clsCommon.ShowSelectForm("GLAccountPen", qry, "Account_Code", whr, fndPenaltyAccountNew.Value, "Account_Code", isButtonClicked)

        Dim qry1 As String = "select Description from TSPL_GL_ACCOUNTS where Account_Code='" + fndPenaltyAccountNew.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry1)
        If (dt.Rows.Count > 0) Then
            rtxtPenaltyAccount.Text = dt.Rows(0)("Description")
        Else
            rtxtPenaltyAccount.Text = ""
        End If
    End Sub

    Private Sub fndBankNew__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndBankNew._MYValidating
        Dim qry As String = "select BANK_CODE as [CODE],DESCRIPTION,BANKACCNUMBER as [Bank Account No],BANKACC as [Bank Account] from TSPL_BANK_MASTER"
        fndBankNew.Value = clsCommon.ShowSelectForm("BankCodeval", qry, "CODE", "", fndBankNew.Value, "CODE", isButtonClicked)
        Dim qry1 As String = "select Description from TSPL_BANK_MASTER where BANK_CODE ='" + fndBankNew.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry1)
        If (dt.Rows.Count > 0) Then
            rtxtBank.Text = dt.Rows(0)("Description")
        Else
            rtxtBank.Text = ""
        End If
    End Sub

    Private Sub fndResponsiblePersonNew__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndResponsiblePersonNew._MYValidating
        Dim qry As String = "select Person_Code as [Code],Person_Name as [Person Name] from TSPL_TDS_RESP_PERSON"
        fndResponsiblePersonNew.Value = clsCommon.ShowSelectForm("Resp_PerSonCodefnd", qry, "Code", "", fndResponsiblePersonNew.Value, "Code", isButtonClicked)
        Dim qry1 As String = "select Person_Name from TSPL_TDS_RESP_PERSON where Person_Code ='" + fndResponsiblePersonNew.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry1)
        If (dt.Rows.Count > 0) Then
            rtxtResponsiblePerson.Text = dt.Rows(0)("Person_Name")
        Else
            rtxtResponsiblePerson.Text = ""
        End If
    End Sub

    Private Sub fndPenaltyAccountNew_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles fndPenaltyAccountNew.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub fndBankNew_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles fndBankNew.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub fndResponsiblePersonNew_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles fndResponsiblePersonNew.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub fndStateCodeNew__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndStateCodeNew._MYValidating
        Dim qry As String = "select State_Code as [Code],State_Name as [State Name] from TSPL_TDS_STATE_MASTER"
        fndStateCodeNew.Value = clsCommon.ShowSelectForm("StateCodeNew", qry, "Code", "", fndStateCodeNew.Value, "Code", isButtonClicked)
        Dim qry1 As String = "select State_Name  from TSPL_TDS_STATE_MASTER where State_Code ='" + fndStateCodeNew.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry1)
        If (dt.Rows.Count > 0) Then
            rtxtStateCode.Text = dt.Rows(0)("State_Name")
        Else

            rtxtStateCode.Text = ""
        End If
    End Sub

    Private Sub fndStateCodeNew_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles fndStateCodeNew.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub FndBranchCodeNew__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles FndBranchCodeNew._MYNavigator
        Dim qry As String = "select Branch_Code From TSPL_TDS_BRANCH_MASTER where  2=2"
        Select Case NavType
            Case NavigatorType.Current
                qry += " and TSPL_TDS_BRANCH_MASTER  .Branch_Code  in ('" + FndBranchCodeNew.Value + "')"
            Case NavigatorType.Next
                qry += " and TSPL_TDS_BRANCH_MASTER  .Branch_Code  in (select min(Branch_Code ) from TSPL_TDS_BRANCH_MASTER where Branch_Code  >'" + FndBranchCodeNew.Value + "')"
            Case NavigatorType.First
                qry += " and TSPL_TDS_BRANCH_MASTER  .Branch_Code  in (select MIN(Branch_Code ) from TSPL_TDS_BRANCH_MASTER)"

            Case NavigatorType.Last
                qry += " and TSPL_TDS_BRANCH_MASTER  .Branch_Code  in (select Max(Branch_Code ) from TSPL_TDS_BRANCH_MASTER)"
            Case NavigatorType.Previous
                qry += " and TSPL_TDS_BRANCH_MASTER  .Branch_Code  in (select Max(Branch_Code  ) from TSPL_TDS_BRANCH_MASTER where Branch_Code  <'" + FndBranchCodeNew.Value + "')"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            FndBranchCodeNew.Value = clsCommon.myCstr(dt.Rows(0)("Branch_Code"))
        End If

        LoadData()
    End Sub

    Private Sub FndBranchCodeNew__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles FndBranchCodeNew._MYValidating
        Dim Qry As String = " Select Count(*) From TSPL_TDS_BRANCH_MASTER where Branch_Code='" & FndBranchCodeNew.Value & "'"
        Dim Count As String = clsDBFuncationality.getSingleValue(Qry)
        If Count = 0 Then
            FndBranchCodeNew.MyReadOnly = False
        Else
            FndBranchCodeNew.MyReadOnly = True
        End If
        If FndBranchCodeNew.MyReadOnly Or isButtonClicked Then

            'Dim qry1 As String = "select Branch_Code as [Code],Branch_Name as [Branch Name], State_Code as [State Code],State_Name as [State Name] from TSPL_TDS_BRANCH_MASTER"
            'FndBranchCodeNew.Value = clsCommon.ShowSelectForm("BranchCodefnd", qry1, "Code", "", FndBranchCodeNew.Value, "Code", isButtonClicked)
            FndBranchCodeNew.Value = clsBranchDetails.getFinder("", FndBranchCodeNew.Value, isButtonClicked)
            FndBranchCodeNew.MyMaxLength = 12
            LoadData()
        End If
    End Sub

    Private Sub FndBranchCodeNew_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles FndBranchCodeNew.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub




End Class
