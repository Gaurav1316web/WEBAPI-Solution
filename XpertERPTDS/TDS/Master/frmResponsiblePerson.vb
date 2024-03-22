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

Public Class frmResponsiblePerson
    Inherits FrmMainTranScreen
    Dim userCode, companyCode As String
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.ResponsiblePerson)
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
    Private Sub frmResponsiblePerson_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
    Private Sub frmResponsiblePerson_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' globalFunc.mandatoryText(fndCode.txtValue, txtName)
        'globalFunc.mandatoryDropdown()
        'AddHandler fndBranch.txtValue.TextChanged, AddressOf fndBranch_text_changed1
        'AddHandler fndBranch.txtValue.Leave, AddressOf fndBranch_leave
        ' AddHandler fndBranch.txtValue.KeyPress, AddressOf fndBranch_KeyPress
        SetUserMgmtNew()
        fndCodeNew.MyReadOnly = False
        ' AddHandler fndStateCode.txtValue.TextChanged, AddressOf fndStateCode_text_changed
        'AddHandler fndStateCode.txtValue.Leave, AddressOf fndStateCode_leave
        ' AddHandler fndStateCode.txtValue.KeyPress, AddressOf fndStateCode_KeyPress

        ' fndCode.txtValue.TextChanged, AddressOf fndCode_textchanged
        'AddHandler fndCode.txtValue.KeyPress, AddressOf fndCode_KeyPress
        'fndCode.txtValue.CharacterCasing = CharacterCasing.Upper
        rbtnDelete.Enabled = False
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        fndCodeNew.MyCharacterCasing = CharacterCasing.Upper
        SetLength()
    End Sub

    Public Sub SetLength()
        fndCodeNew.MyMaxLength = 12
        txtName.MaxLength = 100
        txtFathersName.MaxLength = 100
        txtDesignation.MaxLength = 100
        txtAddress1.MaxLength = 100
        txtAddress2.MaxLength = 50
        txtCity.MaxLength = 12
        txtCountry.MaxLength = 50
        txtPinCode.MaxLength = 50
        txtTelephoneNo.MaxLength = 50
        txtFaxNo.MaxLength = 50
        txtEMail.MaxLength = 50
        txtSignaturePath.MaxLength = 50

    End Sub

    'Private Sub fndBranch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fndBranch.Load
    '    fndBranch.ConnectionString = connectSql.SqlCon()
    '    fndBranch.Query = "select Branch_Code as [Branch Code],Branch_Name as [Branch Name] from TSPL_TDS_BRANCH_MASTER"
    '    fndBranch.ValueToSelect = "Branch Code"
    '    fndBranch.Caption = "Branch Master"
    '    fndBranch.txtValue.MaxLength = 12
    '    fndBranch.ValueToSelect1 = "Branch Name"
    'End Sub
    ''  --------------------------------------------
    '' Added by abhishek as on 4 june 2012
    Private Sub fndbranchCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndbranchCode._MYValidating
        Dim query As String = "select Branch_Code as [Code],Branch_Name as [Branch Name] from TSPL_TDS_BRANCH_MASTER"
        fndbranchCode.Value = clsCommon.ShowSelectForm("BranchCodevaldt", query, "Code", "", fndbranchCode.Value, "Code", isButtonClicked)
        Dim desc As String = "select  Branch_Name from  TSPL_TDS_BRANCH_MASTER where Branch_Code='" & fndbranchCode.Value & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(desc)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            txtBranch.Text = clsCommon.myCstr(dt.Rows(0)("Branch_Name"))
        Else
            txtBranch.Text = ""
        End If
    End Sub

    Private Sub fndbranchCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles fndbranchCode.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub


    'Private Sub fndBranch_text_changed1(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        Dim strBranch_code As String = "select Branch_Code, Branch_Name from TSPL_TDS_BRANCH_MASTER where Branch_Code='" + fndBranch.txtValue.Text + "'"
    '        Dim dr As SqlDataReader
    '        Dim strvalue As String
    '        dr = connectSql.RunSqlReturnDR(strBranch_code)
    '        While dr.Read()
    '            strvalue = dr(0).ToString()
    '        End While
    '        If strvalue <> "" Then
    '            funfill1_BankName()
    '        Else
    '            txtBranch.Text = ""
    '        End If
    '    Catch ex As Exception
    '        myMessages.myExceptions(ex)
    '    End Try
    'End Sub
    'Public Sub funfill1_BankName()
    '    Try
    '        Dim strBranch_name As String = "select Branch_Code, Branch_Name from TSPL_TDS_BRANCH_MASTER where Branch_Code='" + fndBranch.txtValue.Text + "'"
    '        Dim dr As SqlDataReader
    '        dr = connectSql.RunSqlReturnDR(strBranch_name)
    '        While dr.Read()
    '            txtBranch.Text = dr(1).ToString()
    '        End While
    '    Catch ex As Exception
    '        myMessages.myExceptions(ex)
    '    End Try
    'End Sub
    'Private Sub fndBranch_leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If fndBranch.txtValue.Text = "" Then
    '    Else
    '        Try
    '            Dim strBranch_code As String = "select Branch_Code, Branch_Name from TSPL_TDS_BRANCH_MASTER where Branch_Code='" + fndBranch.txtValue.Text + "'"
    '            Dim dr As SqlDataReader
    '            Dim strvalue As String
    '            dr = connectSql.RunSqlReturnDR(strBranch_code)
    '            While dr.Read()
    '                strvalue = dr(0).ToString()
    '            End While
    '            If strvalue <> "" Then
    '            Else : strBranch_code = ""
    '                txtBranch.Text = ""
    '                common.clsCommon.MyMessageBoxShow("Branch Code does not exist in the Master Table")
    '                fndBranch.txtValue.Text = ""
    '                fndBranch.Focus()
    '            End If
    '        Catch ex As Exception
    '            myMessages.myExceptions(ex)
    '        End Try
    '    End If
    'End Sub
    'Private Sub fndBranch_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    '    If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
    '        e.Handled = True
    '    End If
    'End Sub



    'Private Sub fndStateCode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fndStateCode.Load
    '    fndStateCode.ConnectionString = connectSql.SqlCon()
    '    fndStateCode.Query = "select State_Code as [State Code],State_Name as [State Name] from TSPL_TDS_STATE_MASTER"
    '    fndStateCode.ValueToSelect = "State Code"
    '    fndStateCode.Caption = "State Master"
    '    fndStateCode.txtValue.MaxLength = 12
    '    fndStateCode.ValueToSelect1 = "State Name"
    'End Sub
    '------------------------------------------
    '' Added By abhishek as on 4 june 2012
    Private Sub fndStateCodeNew__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndStateCodeNew._MYValidating
        Dim query As String = "select State_Code as [Code],State_Name as [State Name] from TSPL_TDS_STATE_MASTER"
        fndStateCodeNew.Value = clsCommon.ShowSelectForm("Codefndd", query, "Code", "", fndStateCodeNew.Value, "Code", isButtonClicked)
        Dim desc As String = "select  State_Name from TSPL_TDS_STATE_MASTER where State_Code='" & fndStateCodeNew.Value & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(desc)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            txtStateCode.Text = clsCommon.myCstr(dt.Rows(0)("State_Name"))
        Else
            txtStateCode.Text = ""
        End If



    End Sub

    'Private Sub fndStateCode_text_changed(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        Dim strBranch_code As String = "select State_Code, State_Name from TSPL_TDS_STATE_MASTER where State_Code='" + fndStateCode.txtValue.Text + "'"
    '        Dim dr As SqlDataReader
    '        Dim strvalue As String
    '        dr = connectSql.RunSqlReturnDR(strBranch_code)
    '        While dr.Read()
    '            strvalue = dr(0).ToString()
    '        End While
    '        If strvalue <> "" Then
    '            funfill1_StateName()
    '        Else
    '            txtStateCode.Text = ""
    '        End If
    '    Catch ex As Exception
    '        myMessages.myExceptions(ex)
    '    End Try
    'End Sub
    'Public Sub funfill1_StateName()
    '    Try
    '        Dim strstate_code As String = "select State_Code, State_Name from TSPL_TDS_STATE_MASTER where State_Code='" + fndStateCode.txtValue.Text + "'"
    '        Dim dr As SqlDataReader
    '        dr = connectSql.RunSqlReturnDR(strstate_code)
    '        While dr.Read()
    '            txtStateCode.Text = dr(1).ToString()
    '        End While
    '    Catch ex As Exception
    '        myMessages.myExceptions(ex)
    '    End Try
    'End Sub
    'Private Sub fndStateCode_leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If fndStateCode.txtValue.Text = "" Then
    '    Else
    '        Try
    '            Dim strstate_code As String = "select State_Code, State_Name from TSPL_TDS_STATE_MASTER where State_Code='" + fndStateCode.txtValue.Text + "'"
    '            Dim dr As SqlDataReader
    '            Dim strvalue As String
    '            dr = connectSql.RunSqlReturnDR(strstate_code)
    '            While dr.Read()
    '                strvalue = dr(0).ToString()
    '            End While
    '            If strvalue <> "" Then
    '            Else : strstate_code = ""
    '                txtStateCode.Text = ""
    '                common.clsCommon.MyMessageBoxShow("State Code does not exist in the Master Table")
    '                fndStateCode.txtValue.Text = ""
    '                fndStateCode.Focus()
    '            End If
    '        Catch ex As Exception
    '            myMessages.myExceptions(ex)
    '        End Try
    '    End If
    'End Sub
    'Private Sub fndStateCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    '    If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
    '        e.Handled = True
    '    End If
    'End Sub
    Private Sub fndStateCodeNew_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles fndStateCodeNew.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub
    '---------------------------------------------
    '' Added By Abhishek as on 4 june 2012 

    ''Private Sub fndCode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fndCode.Load
    ''    fndCode.ConnectionString = connectSql.SqlCon()
    ''    fndCode.Query = "select Person_Code as [Person Code],Person_Name as [Person Name],Designation,City,Branch_Code as [Branch Code] from TSPL_TDS_RESP_PERSON"
    ''    fndCode.ValueToSelect = "Person Code"
    ''    fndCode.Caption = "Responsible Person"
    ''    fndCode.txtValue.MaxLength = 12
    ''    fndCode.ValueToSelect1 = "Person Name"
    ''End Sub

    Private Sub fndCodeNew_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles fndCodeNew.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub
    Private Sub fndCodeNew__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndCodeNew._MYNavigator
        Dim qry As String = "select Person_Code from TSPL_TDS_RESP_PERSON where 2=2"
        Select Case NavType
            Case NavigatorType.Current
                qry += " and TSPL_TDS_RESP_PERSON  .Person_Code in ('" + fndCodeNew.Value + "')"
            Case NavigatorType.Next
                qry += " and TSPL_TDS_RESP_PERSON  .Person_Code in (select min(Person_Code) from TSPL_TDS_RESP_PERSON where Person_Code >'" + fndCodeNew.Value + "')"
            Case NavigatorType.First
                qry += " and TSPL_TDS_RESP_PERSON  .Person_Code in (select MIN(Person_Code) from TSPL_TDS_RESP_PERSON)"

            Case NavigatorType.Last
                qry += " and TSPL_TDS_RESP_PERSON  .Person_Code in (select Max(Person_Code) from TSPL_TDS_RESP_PERSON)"
            Case NavigatorType.Previous
                qry += " and TSPL_TDS_RESP_PERSON  .Person_Code in (select Max(Person_Code ) from TSPL_TDS_RESP_PERSON where Person_Code <'" + fndCodeNew.Value + "')"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            fndCodeNew.Value = clsCommon.myCstr(dt.Rows(0)("Person_Code"))
        End If

        Loaddata()
    End Sub

    Private Sub fndCodeNew__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndCodeNew._MYValidating
        Dim Qry As String = " Select Count(*) From TSPL_TDS_RESP_PERSON where Person_Code='" & fndCodeNew.Value & "'"
        Dim Count As String = clsDBFuncationality.getSingleValue(Qry)
        If Count = 0 Then
            fndCodeNew.MyReadOnly = False
        Else
            fndCodeNew.MyReadOnly = True
        End If
        If fndCodeNew.MyReadOnly Or isButtonClicked Then

            'Dim qry1 As String = "select Person_Code as [Code],Person_Name as [Person Name],Designation,City,Branch_Code as [Branch Code] from TSPL_TDS_RESP_PERSON"
            'fndCodeNew.Value = clsCommon.ShowSelectForm("PersonCodeid", qry1, "Code", "", fndCodeNew.Value, "Code", isButtonClicked)
            fndCodeNew.Value = clsResponsiblePerson.getFinder("", fndCodeNew.Value, isButtonClicked)
            fndCodeNew.MyMaxLength = 12
            Loaddata()
        End If
    End Sub
    'Public Sub fndCode_textchanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Loaddata()
    'End Sub
    Public Sub Loaddata()
        Try
            Dim strPerson_Code As String = "select Person_Code from TSPL_TDS_RESP_PERSON where Person_Code='" + fndCodeNew.Value + "'"
            Dim strvalue As String = clsDBFuncationality.getSingleValue(strPerson_Code)
            If (strvalue <> "") Then
                funfill()
            Else
                'fndCode.txtValue.Text = ""
                txtName.Text = ""
                fndCodeNew.MyReadOnly = False
                txtFathersName.Text = ""
                txtDesignation.Text = ""
                txtAddress1.Text = ""
                txtAddress2.Text = ""
                txtCity.Text = ""
                fndbranchCode.Value = ""
                txtBranch.Text = ""
                fndStateCodeNew.Value = ""
                txtStateCode.Text = ""
                txtCountry.Text = ""
                txtPinCode.Text = ""
                txtTelephoneNo.Text = ""
                txtFaxNo.Text = ""
                txtEMail.Text = ""
                txtSignaturePath.Text = ""
                chkActive.Checked = False
                rbtnSave.Text = "Save"
                rbtnDelete.Enabled = False
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Private Sub funfill()
        Try
            Dim strQuery As String = "select Person_Name,Father_Name,Designation,Address1,Address2,City,Branch_Code,State_Code,Country,Pincode,Telephone,Fax,Email,Signature,Active from TSPL_TDS_RESP_PERSON where Person_Code='" + fndCodeNew.Value + "'"
            Dim dt As DataTable
            dt = clsDBFuncationality.GetDataTable(strQuery)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    txtName.Text = dt.Rows(i)("Person_Name")
                    txtFathersName.Text = dt.Rows(i)("Father_Name")
                    txtDesignation.Text = dt.Rows(i)("Designation")
                    txtAddress1.Text = dt.Rows(i)("Address1")
                    txtAddress2.Text = dt.Rows(i)("Address2")
                    txtCity.Text = dt.Rows(i)("City")
                    fndbranchCode.Value = dt.Rows(i)("Branch_Code")
                    fndStateCodeNew.Value = dt.Rows(i)("State_Code")
                    txtCountry.Text = dt.Rows(i)("Country")
                    txtPinCode.Text = dt.Rows(i)("Pincode")
                    txtTelephoneNo.Text = dt.Rows(i)("Telephone")
                    txtFaxNo.Text = dt.Rows(i)("Fax")
                    txtEMail.Text = dt.Rows(i)("Email")
                    txtSignaturePath.Text = dt.Rows(i)("Signature")
                    Dim chk As String = dt.Rows(i)("Active")
                    If chk = "T" Then
                        chkActive.Checked = True
                    Else
                        chkActive.Checked = False
                    End If
                    txtBranch.Text = clsDBFuncationality.getSingleValue("select  Branch_Name from  TSPL_TDS_BRANCH_MASTER where Branch_Code='" & fndbranchCode.Value & "'")
                    txtStateCode.Text = clsDBFuncationality.getSingleValue("select  State_Name from TSPL_TDS_STATE_MASTER where State_Code='" & fndStateCodeNew.Value & "'")

                    rbtnDelete.Enabled = True
                    rbtnSave.Text = "Update"

                Next
            End If



            ''Dim dr As SqlDataReader
            ''dr = connectSql.RunSqlReturnDR(strQuery)
            ''If dr.Read() Then
            ''    txtName.Text = dr(0).ToString()
            ''    txtFathersName.Text = dr(1).ToString()
            ''    txtDesignation.Text = dr(2).ToString()
            ''    txtAddress1.Text = dr(3).ToString()
            ''    txtAddress2.Text = dr(4).ToString()
            ''    txtCity.Text = dr(5).ToString()
            ''    fndbranchCode.Value = dr(6).ToString()
            ''    fndStateCodeNew.Value = dr(7).ToString()
            ''    txtCountry.Text = dr(8).ToString()
            ''    txtPinCode.Text = dr(9).ToString()
            ''    txtTelephoneNo.Text = dr(10).ToString()
            ''    txtFaxNo.Text = dr(11).ToString()
            ''    txtEMail.Text = dr(12).ToString()
            ''    txtSignaturePath.Text = dr(13).ToString()
            ''    Dim chk As String = dr(14).ToString()
            ''    If chk = "T" Then
            ''        chkActive.Checked = True                        
            ''    Else
            ''        chkActive.Checked = False
            ''    End If
            ''    txtBranch.Text = clsDBFuncationality.getSingleValue("select  Branch_Name from  TSPL_TDS_BRANCH_MASTER where Branch_Code='" & fndbranchCode.Value & "'")
            ''    txtStateCode.Text = clsDBFuncationality.getSingleValue("select  State_Name from TSPL_TDS_STATE_MASTER where State_Code='" & fndStateCodeNew.Value & "'")

            ''    rbtnDelete.Enabled = True
            ''    rbtnSave.Text = "Update"
            ''    'If userCode <> "ADMIN" Then
            ''    '    If funSetUserAccess() = False Then Exit Sub
            ''    'End If
            ''End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub rbtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnSave.Click
        SaveData()
    End Sub
    Sub SaveData()
        Dim check As Match = Regex.Match(txtEMail.Text, "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")
        If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False) = False) AndAlso fndCodeNew.Value = "" Then
            myMessages.blankValue(Me, "Person Code", Me.Text)
            fndCodeNew.Focus()
        ElseIf txtName.Text = "" Then
            myMessages.blankValue(Me, "Person Name", Me.Text)
            txtName.Focus()
        ElseIf check.Success = False And txtEMail.Text <> "" Then
            common.clsCommon.MyMessageBoxShow(Me, "Please insert the proper format of e-mail address", Me.Text)
            'txtEMail.Text = ""
            txtEMail.Focus()
        ElseIf rbtnSave.Text = "Save" Then
            funInsert()
        Else
            funUpdate()
        End If
    End Sub
    Private Sub funInsert()
        Try
            Dim chk As String
            If chkActive.Checked = True Then
                chk = "T"
            Else
                chk = "F"
            End If
            If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False)) Then
                Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_TDS_RESP_PERSON where Person_Code='" & fndCodeNew.Value & "'")
                If ChkNewEntry = 0 Then
                    fndCodeNew.Value = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"), clsDocType.ResponsiblePerson, "", "")
                    If clsCommon.myLen(fndCodeNew.Value) <= 0 Then
                        Throw New Exception("Error in Code Generation")
                    End If
                End If
            End If
            connectSql.RunSp("SP_TSPL_TDS_RESP_PERSON_INSERT", New SqlParameter("@Person_Code", fndCodeNew.Value), New SqlParameter("@Person_Name", txtName.Text), New SqlParameter("@Father_Name", txtFathersName.Text), New SqlParameter("@Designation", txtDesignation.Text), New SqlParameter("@Address1", txtAddress1.Text), New SqlParameter("@Address2", txtAddress2.Text), New SqlParameter("@City ", txtCity.Text), New SqlParameter("@Branch_Code", fndbranchCode.Value), New SqlParameter("@State_Code", fndStateCodeNew.Value), New SqlParameter("@Country", txtCountry.Text), New SqlParameter("@Pincode", txtPinCode.Text), New SqlParameter("@Telephone", txtTelephoneNo.Text), New SqlParameter("@Fax", txtFaxNo.Text), New SqlParameter("@Email", txtEMail.Text), New SqlParameter("@Signature", txtSignaturePath.Text), New SqlParameter("@Active", chk), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate()), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate()), New SqlParameter("@Comp_Code", companyCode))
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
            If chkActive.Checked = True Then
                chk = "T"
            Else
                chk = "F"
            End If
            connectSql.RunSp("SP_TSPL_TDS_RESP_PERSON_UPDATE", New SqlParameter("@Person_Code", fndCodeNew.Value), New SqlParameter("@Person_Name", txtName.Text), New SqlParameter("@Father_Name", txtFathersName.Text), New SqlParameter("@Designation", txtDesignation.Text), New SqlParameter("@Address1", txtAddress1.Text), New SqlParameter("@Address2", txtAddress2.Text), New SqlParameter("@City ", txtCity.Text), New SqlParameter("@Branch_Code", fndbranchCode.Value), New SqlParameter("@State_Code", fndStateCodeNew.Value), New SqlParameter("@Country", txtCountry.Text), New SqlParameter("@Pincode", txtPinCode.Text), New SqlParameter("@Telephone", txtTelephoneNo.Text), New SqlParameter("@Fax", txtFaxNo.Text), New SqlParameter("@Email", txtEMail.Text), New SqlParameter("@Signature", txtSignaturePath.Text), New SqlParameter("@Active", chk), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate()), New SqlParameter("@Comp_Code", companyCode))
            myMessages.update()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub rbtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnDelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If fndCodeNew.Value = "" Then
            myMessages.blankValue(Me, "Person Code", Me.Text)
        ElseIf myMessages.deleteConfirm() Then
            funDelete()
            rbtnSave.Text = "Save"
            rbtnDelete.Enabled = False
        End If
    End Sub
    Private Sub funDelete()
        Try
            connectSql.RunSp("SP_TSPL_TDS_RESP_PERSON_DELETE", New SqlParameter("@Person_Code", fndCodeNew.Value))
            myMessages.delete()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub rdbtnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnRefresh.Click
        funRefresh()
    End Sub
    Private Sub funRefresh()
        Try
            fndCodeNew.Value = ""

            fndCodeNew.MyReadOnly = False
            txtName.Text = ""
            txtFathersName.Text = ""
            txtDesignation.Text = ""
            txtAddress1.Text = ""
            txtAddress2.Text = ""
            txtCity.Text = ""
            fndbranchCode.Value = ""
            txtBranch.Text = ""
            fndStateCodeNew.Value = ""
            txtStateCode.Text = ""
            txtCountry.Text = ""
            txtPinCode.Text = ""
            txtTelephoneNo.Text = ""
            txtFaxNo.Text = ""
            txtEMail.Text = ""
            txtSignaturePath.Text = ""
            chkActive.Checked = False
            rbtnSave.Text = "Save"
            rbtnDelete.Enabled = False
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub rbtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnClose.Click
        Me.Close()
    End Sub

    Private Sub RadMenuItemExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemExport.Click
        Dim strSql As String = "Select Person_Code as [Person Code],Person_Name as [Person Name], Father_Name as [Father Name],Designation,Address1,Address2,City,Branch_Code as [Branch Code],State_Code as [State Code],Country,Pincode,Telephone,Fax,Email,Signature ,Active from TSPL_TDS_RESP_PERSON"
        transportSql.ExporttoExcel(strSql, Me)
    End Sub

    Private Sub RadMenuItemImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "Person Code", "Person Name", "Father Name", "Designation", "Address1", "Address2", "City", "Branch Code", "State Code", "Country", "Pincode", "Telephone", "Fax", "Email", "Signature", "Active") Then
            Dim trans As SqlTransaction = Nothing
            Try
                connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim strP_Code As String = clsCommon.myCstr(grow.Cells(0).Value)
                    If String.IsNullOrEmpty(strP_Code) Or clsCommon.myLen(strP_Code) > 12 Then
                        Throw New Exception("Person Code Can not be left Blank or size can not be grater than 12")
                    End If

                    Dim strP_Name As String = clsCommon.myCstr(grow.Cells(1).Value)
                    If String.IsNullOrEmpty(strP_Name) Or clsCommon.myLen(strP_Name) > 50 Then
                        Throw New Exception("Person Name Can not be left Blank or size can not be grater than 50")
                    End If

                    Dim strF_Name As String = clsCommon.myCstr(grow.Cells(2).Value)
                    If clsCommon.myLen(strF_Name) > 100 Then
                        Throw New Exception("Father Name can not be grater than 100")
                    End If

                    Dim strDesignation As String = clsCommon.myCstr(grow.Cells(3).Value)
                    If clsCommon.myLen(strDesignation) > 100 Then
                        Throw New Exception("Designation can not be grater than 100")
                    End If

                    Dim strAddress1 As String = clsCommon.myCstr(grow.Cells(4).Value)
                    If clsCommon.myLen(strAddress1) > 100 Then
                        Throw New Exception("Address1 can not be grater than 100")
                    End If

                    Dim strAddress2 As String = clsCommon.myCstr(grow.Cells(5).Value)
                    If clsCommon.myLen(strAddress2) > 50 Then
                        Throw New Exception("Address2 can not be greater than 50")
                    End If

                    Dim strCity As String = clsCommon.myCstr(grow.Cells(6).Value)
                    If clsCommon.myLen(strCity) > 12 Then
                        Throw New Exception("City can not be greater than 12")
                    End If

                    Dim strB_Code As String = clsCommon.myCstr(grow.Cells(7).Value)
                    If clsCommon.myLen(strB_Code) > 12 Then
                        Throw New Exception("Branch Code can not be grater than 12")
                    End If

                    Dim strstate_Code As String = clsCommon.myCstr(grow.Cells(8).Value)
                    If clsCommon.myLen(strstate_Code) > 12 Then
                        Throw New Exception("State Code can not be greater than 12")
                    End If

                    Dim strCountry As String = clsCommon.myCstr(grow.Cells(9).Value)
                    If clsCommon.myLen(strCountry) > 50 Then
                        Throw New Exception("Countrycan not be greater than 50")
                    End If

                    Dim strPin_code As String = clsCommon.myCstr(grow.Cells(10).Value)
                    If clsCommon.myLen(strPin_code) > 50 Then
                        Throw New Exception("Pin Code can not be grater than 50")
                    End If

                    Dim strTelephone As String = grow.Cells(11).Value.ToString()
                    If clsCommon.myLen(strTelephone) > 50 Then
                        Throw New Exception("Telephone not be grater than 50")
                    End If

                    Dim strFax As String = grow.Cells(12).Value.ToString()
                    If clsCommon.myLen(strFax) > 50 Then
                        Throw New Exception("Fax can not be greater than 50")
                    End If

                    Dim strEmail As String = grow.Cells(13).Value.ToString()
                    If clsCommon.myLen(strEmail) > 50 Then
                        Throw New Exception("Email can not be greater than 50")
                    End If

                    Dim strSignature As String = grow.Cells(14).Value.ToString()
                    If clsCommon.myLen(strSignature) > 50 Then
                        Throw New Exception("Signature can not be grater than 50")
                    End If

                    Dim strActive As String = grow.Cells(15).Value.ToString()
                    If strActive <> "" Then
                        If strActive = "T" Then
                            strActive = "T"
                        ElseIf strActive = "F" Then
                            strActive = "F"
                        Else
                            Throw New Exception("Active can not be grater than 1.Please Entre Either T or F")
                        End If

                    End If
                    Dim strquery As String = "select count(*) from TSPL_TDS_RESP_PERSON where Person_Code='" + strP_Code + "'"
                    Dim i As Integer = CInt(connectSql.RunScalar(trans, strquery))
                    If (i = 0) Then
                        connectSql.RunSpTransaction(trans, "SP_TSPL_TDS_RESP_PERSON_INSERT", New SqlParameter("@Person_Code", strP_Code), New SqlParameter("@Person_Name", strP_Name), New SqlParameter("@Father_Name", strF_Name), New SqlParameter("@Designation", strDesignation), New SqlParameter("@Address1", strAddress1), New SqlParameter("@Address2", strAddress2), New SqlParameter("@City ", strCity), New SqlParameter("@Branch_Code", strB_Code), New SqlParameter("@State_Code", strstate_Code), New SqlParameter("@Country", strCountry), New SqlParameter("@Pincode", strPin_code), New SqlParameter("@Telephone", strTelephone), New SqlParameter("@Fax", strFax), New SqlParameter("@Email", strEmail), New SqlParameter("@Signature", strSignature), New SqlParameter("@Active", strActive), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate(trans)), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", companyCode))
                    Else
                        connectSql.RunSpTransaction(trans, "SP_TSPL_TDS_RESP_PERSON_UPDATE", New SqlParameter("@Person_Code", strP_Code), New SqlParameter("@Person_Name", strP_Name), New SqlParameter("@Father_Name", strF_Name), New SqlParameter("@Designation", strDesignation), New SqlParameter("@Address1", strAddress1), New SqlParameter("@Address2", strAddress2), New SqlParameter("@City ", strCity), New SqlParameter("@Branch_Code", strB_Code), New SqlParameter("@State_Code", strstate_Code), New SqlParameter("@Country", strCountry), New SqlParameter("@Pincode", strPin_code), New SqlParameter("@Telephone", strTelephone), New SqlParameter("@Fax", strFax), New SqlParameter("@Email", strEmail), New SqlParameter("@Signature", strSignature), New SqlParameter("@Active", strActive), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", companyCode))
                    End If
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
                'connectSql.CloseConnection(connectSql.Connection())
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub RadMenuItemClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemClose.Click
        Me.Close()
    End Sub
    'It Is Used To Give The Authority To User,To Access This Form.(It Is Bassed On Mapping)
    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "RESP-PERSON"
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
    '            rbtnSave.Enabled = False
    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access
    '            rbtnDelete.Enabled = False
    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception

    '    End Try
    'End Function



    Private Sub txtTelephoneNo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTelephoneNo.KeyPress
        If ((e.KeyChar >= Chr(48) And e.KeyChar <= Chr(57)) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(48) Or e.KeyChar = Chr(45)) Then
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtPinCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPinCode.KeyPress
        If ((e.KeyChar >= Chr(48) And e.KeyChar <= Chr(57)) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(48) Or e.KeyChar = Chr(45)) Then
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtFaxNo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFaxNo.KeyPress
        If ((e.KeyChar >= Chr(48) And e.KeyChar <= Chr(57)) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(48) Or e.KeyChar = Chr(45)) Then
        Else
            e.Handled = True
        End If
    End Sub
End Class
