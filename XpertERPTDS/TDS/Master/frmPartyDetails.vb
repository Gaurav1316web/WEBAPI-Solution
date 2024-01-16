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
Imports System.Globalization
Imports System.Threading
Imports common
Imports XpertERPEngine
'created by --> Vipin
'createddate --> 16/06/2011
'modifiedby --> Vipin
'Modified date -->16/06/2011
'Tables Used --> TSPL_TDS_VENDOR_DETAILS
'--preeti gupta..ticket no.[BM00000003134]
Public Class frmPartyDetails
    Inherits FrmMainTranScreen
    Dim userCode, companyCode As String
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.PartyDetails)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            Me.Close()
        End If
        btnsave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 02/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnsave.Visible = True Then
            MenuImport.Enabled = True
            menuExport.Enabled = True
        Else
            MenuImport.Enabled = False
            menuExport.Enabled = False
        End If
        '--------------------------------------------------
        'btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub frmPartyDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
            'ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            '    PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    Private Sub frmPartyDetails_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        'globalFunc.mandatoryText(fndvendor.txtValue, fnddeduc.txtValue, fndstate.txtValue, fndbranch.txtValue, txtPan)
        ToolTipparty.SetToolTip(btnnew, "New")
        fndvendorNew.MyMaxLength = 12


        'AddHandler fndvendor.txtValue.TextChanged, AddressOf fndvendor_text_changed
        'AddHandler fndvendor.txtValue.KeyPress, AddressOf fndvendor_key_press
        'AddHandler fndvendor.txtValue.Leave, AddressOf fndvendor_leave

        'AddHandler fnddeduc.txtValue.TextChanged, AddressOf fnddeduc_text_changed
        'AddHandler fnddeduc.txtValue.KeyPress, AddressOf fnddeduc_key_press
        'AddHandler fnddeduc.txtValue.Leave, AddressOf fnddeduc_leave

        'AddHandler fndstate.txtValue.TextChanged, AddressOf fndstate_text_changed
        'AddHandler fndstate.txtValue.KeyPress, AddressOf fndstate_key_press
        'AddHandler fndstate.txtValue.Leave, AddressOf fndstate_leave

        'AddHandler fndbranch.txtValue.TextChanged, AddressOf fndbranch_text_changed
        'AddHandler fndbranch.txtValue.KeyPress, AddressOf fndbranch_key_press
        'AddHandler fndbranch.txtValue.Leave, AddressOf fndbranch_leave
        fndvendorNew.MyCharacterCasing = CharacterCasing.Upper
        fnddeducNew.Value.ToUpper()
        fndstatenew.Value.ToUpper()
        fndbranchnew.Value.ToUpper()


        btnsave.Enabled = True
        btndelete.Enabled = False
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            fndvendorNew.Value = clsCommon.myCstr(Me.Tag)
            Loaddata()
        End If
    End Sub

#Region "Events"


    'This will check the existence of value in databse on text change property of finder,if it exist then it will call funfill() otherwise it will blank the fields


    Public Sub Loaddata()
        Try
            Dim strquery As String = "select vendor_code from TSPL_TDS_VENDOR_DETAILS where vendor_code='" + fndvendorNew.Value + "'"

            'Dim dr As SqlDataReader
            Dim strvalue As String = clsDBFuncationality.getSingleValue(strquery)
            'dr = connectSql.RunSqlReturnDR(strquery)
            'While dr.Read()
            '    strvalue = dr(0).ToString()
            'End While
            If strvalue <> "" Then
                funfill()

            Else
                Dim strquery1 As String = "select Vendor_Code as [Vendor Code],Vendor_Name as [Vendor Name],ISNULL(PAN,'') AS PAN,TDS_Branch_Code,Deduction_Code,TDS_Vendor_Type,TDS_Status,TDS_State_Code from TSPL_VENDOR_MASTER   where Vendor_Code='" + fndvendorNew.Value + "'"
                'Dim dr1 As SqlDataReader
                Dim dr1 As DataTable = clsDBFuncationality.GetDataTable(strquery1)
                Dim chk As String = ""
                Dim des As String = ""
                Dim PAN As String = ""
                Dim TDS_Branch_Code As String = ""
                Dim Deduction_Code As String = ""
                Dim TDS_Vendor_Type As String = ""
                Dim TDS_Status As String = ""
                Dim TDS_State_Code As String = ""
                Dim Branch_Name As String = ""
                Dim State_Name As String = ""
                Dim Deduction_Name As String = ""
                ' dr1 = connectSql.RunSqlReturnDR(strquery1)
                'While dr1.Read()
                If (dr1 IsNot Nothing AndAlso dr1.Rows.Count > 0) Then
                    ' For ii As Integer = 0 To dr1.Rows.Count - 1
                    chk = dr1.Rows(0)("Vendor Code").ToString()
                    des = dr1.Rows(0)("Vendor Name").ToString()
                    PAN = clsCommon.myCstr(dr1.Rows(0)("PAN"))
                    TDS_Branch_Code = clsCommon.myCstr(dr1.Rows(0)("TDS_Branch_Code"))
                    If clsCommon.myLen(TDS_Branch_Code) > 0 Then
                        Branch_Name = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Branch_Name from  TSPL_TDS_BRANCH_MASTER where Branch_Code='" & TDS_Branch_Code & "'"))
                    End If

                    Deduction_Code = clsCommon.myCstr(dr1.Rows(0)("Deduction_Code"))
                    If clsCommon.myLen(Deduction_Code) > 0 Then
                        Deduction_Name = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  description  from TSPL_TDS_DEDUCTION_HEAD where deduction_code ='" & Deduction_Code & "'"))
                    End If
                    TDS_Vendor_Type = clsCommon.myCstr(dr1.Rows(0)("TDS_Vendor_Type"))
                    TDS_Status = clsCommon.myCstr(dr1.Rows(0)("TDS_Status"))
                    TDS_State_Code = clsCommon.myCstr(dr1.Rows(0)("TDS_State_Code"))
                    If clsCommon.myLen(TDS_State_Code) > 0 Then
                        State_Name = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  State_Name from TSPL_STATE_MASTER where State_Code='" & TDS_State_Code & "'"))
                    End If
                    ' Next
                End If
                ' End While

                If chk <> "" Then
                    txtvendes.Text = des
                    txtPan.Text = PAN
                    fnddeducNew.Value = Deduction_Code
                    txtdeduc.Text = Deduction_Name
                    fndstatenew.Value = TDS_State_Code
                    txtstate.Text = State_Name
                    fndbranchnew.Value = TDS_Branch_Code
                    txtbranch.Text = Branch_Name
                    chkinactive.Checked = False
                    If clsCommon.myLen(TDS_Vendor_Type) <= 0 Then
                        ddlventype.Text = "Individual"
                    Else
                        ddlventype.Text = TDS_Vendor_Type
                    End If
                    If clsCommon.myLen(TDS_Status) <= 0 Then
                        ddlstatus.Text = "Resident"
                    Else
                        ddlstatus.Text = TDS_Status
                    End If

                    btnsave.Text = "Save"
                    btndelete.Enabled = False


                Else
                    txtvendes.Text = ""
                    fnddeducNew.Value = ""
                    txtdeduc.Text = ""
                    fndstatenew.Value = ""
                    txtstate.Text = ""
                    fndbranchnew.Value = ""
                    txtbranch.Text = ""
                    txtPan.Text = ""
                    chkinactive.Checked = False
                    ddlventype.Text = "Individual"
                    ddlstatus.Text = "Resident"
                    btnsave.Text = "Save"
                    btndelete.Enabled = False

                End If



                End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    'Keypress validation on finder and converting lower case to upper case
    'Public Sub fndvendor_key_press(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

    '    If (e.KeyChar = Chr(39)) Then
    '        e.Handled = True
    '    End If
    'End Sub

    'This will check the existence of value in databse on leave property of finder,if it exist then it will fill, otherwise it will blank the fields
    'Public Sub fndvendor_leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If fndvendor.txtValue.Text = "" Then
    '    Else
    '        Try
    '            ' Dim strquery As String = "select ven_Group_code,group_desc  from Tspl_vendor_group where ven_group_code='" + fndvendor.txtValue.Text + "'"
    '            Dim strquery As String = "select Vendor_Code   from TSPL_VENDOR_MASTER where vendor_code='" + fndvendor.txtValue.Text + "'"
    '            Dim dr As SqlDataReader
    '            Dim strvalue As String

    '            dr = connectSql.RunSqlReturnDR(strquery)
    '            While dr.Read()
    '                strvalue = dr(0).ToString()
    '            End While
    '            If strvalue <> "" Then
    '            Else : strquery = ""
    '                txtvendes.Text = ""
    '                common.clsCommon.MyMessageBoxShow("This Vendor does not exist in Master Table")
    '                fndvendor.txtValue.Text = ""
    '            End If
    '        Catch ex As Exception
    '            myMessages.myExceptions(ex)
    '        End Try
    '    End If
    'End Sub


    'This will check the existence of value in databse on text change property of finder,if it exist then it will call funfill() otherwise it will blank the fields

    'Public Sub fnddeduc_text_changed(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        Dim strquery As String = " select deduction_code ,description from TSPL_TDS_DEDUCTION_HEAD where deduction_code='" + fnddeduc.txtValue.Text + "'"

    '        Dim dr As SqlDataReader
    '        Dim strvalue As String
    '        dr = connectSql.RunSqlReturnDR(strquery)
    '        While dr.Read()
    '            strvalue = dr(0).ToString()
    '        End While
    '        If strvalue <> "" Then
    '            funfillfnddeduc()
    '        Else
    '            txtdeduc.Text = ""
    '        End If
    '    Catch ex As Exception
    '        myMessages.myExceptions(ex)
    '    End Try
    'End Sub

    'Keypress validation on finder and converting lower case to upper case
    'Public Sub fnddeduc_key_press(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

    '    If (e.KeyChar = Chr(39)) Then
    '        e.Handled = True
    '    End If
    'End Sub

    'This will check the existence of value in databse on leave property of finder,if it exist then it will fill, otherwise it will blank the fields
    'Public Sub fnddeduc_leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If fnddeduc.txtValue.Text = "" Then
    '    Else
    '        Try
    '            Dim strquery As String = " select deduction_code ,description from TSPL_TDS_DEDUCTION_HEAD where deduction_code='" + fnddeduc.txtValue.Text + "'"
    '            Dim dr As SqlDataReader
    '            Dim strvalue As String

    '            dr = connectSql.RunSqlReturnDR(strquery)
    '            While dr.Read()
    '                strvalue = dr(0).ToString()
    '            End While
    '            If strvalue <> "" Then
    '            Else : strquery = ""
    '                txtdeduc.Text = ""
    '                common.clsCommon.MyMessageBoxShow("This Deduction does not exist in Master Table")
    '                fnddeduc.txtValue.Text = ""
    '            End If
    '        Catch ex As Exception
    '            myMessages.myExceptions(ex)
    '        End Try
    '    End If
    'End Sub


    'This will check the existence of value in databse on text change property of finder,if it exist then it will call funfill() otherwise it will blank the fields

    'Public Sub fndstate_text_changed(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        Dim strquery As String = "select State_Code,state_name from TSPL_TDS_STATE_MASTER where State_Code='" + fndstate.txtValue.Text + "'"

    '        Dim dr As SqlDataReader
    '        Dim strvalue As String
    '        dr = connectSql.RunSqlReturnDR(strquery)
    '        While dr.Read()
    '            strvalue = dr(0).ToString()
    '        End While
    '        If strvalue <> "" Then
    '            funfillfndstate()
    '        Else
    '            txtstate.Text = ""
    '        End If
    '    Catch ex As Exception
    '        myMessages.myExceptions(ex)
    '    End Try
    'End Sub

    'Keypress validation on finder and converting lower case to upper case
    'Public Sub fndstate_key_press(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

    '    If (e.KeyChar = Chr(39)) Then
    '        e.Handled = True
    '    End If
    'End Sub

    'This will check the existence of value in databse on leave property of finder,if it exist then it will fill, otherwise it will blank the fields
    'Public Sub fndstate_leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If fndstate.txtValue.Text = "" Then
    '    Else
    '        Try
    '            Dim strquery As String = "select State_Code,state_name from TSPL_TDS_STATE_MASTER where State_Code='" + fndstate.txtValue.Text + "'"
    '            Dim dr As SqlDataReader
    '            Dim strvalue As String

    '            dr = connectSql.RunSqlReturnDR(strquery)
    '            While dr.Read()
    '                strvalue = dr(0).ToString()
    '            End While
    '            If strvalue <> "" Then
    '            Else : strquery = ""
    '                txtstate.Text = ""
    '                common.clsCommon.MyMessageBoxShow("This State does not exist in Master Table")
    '                fndstate.txtValue.Text = ""
    '            End If
    '        Catch ex As Exception
    '            myMessages.myExceptions(ex)
    '        End Try
    '    End If
    'End Sub

    'This will check the existence of value in databse on text change property of finder,if it exist then it will call funfill() otherwise it will blank the fields

    'Public Sub fndbranch_text_changed(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        Dim strquery As String = "select Branch_Code as [Branch Code],Branch_Name as [Branch Name], State_Code as [State Code],State_Name as [State Name] from TSPL_TDS_BRANCH_MASTER where branch_code='" + fndbranch.txtValue.Text + "'"

    '        Dim dr As SqlDataReader
    '        Dim strvalue As String
    '        dr = connectSql.RunSqlReturnDR(strquery)
    '        While dr.Read()
    '            strvalue = dr(0).ToString()
    '        End While
    '        If strvalue <> "" Then
    '            funfillfndbranch()
    '        Else
    '            txtbranch.Text = ""
    '        End If
    '    Catch ex As Exception
    '        myMessages.myExceptions(ex)
    '    End Try
    'End Sub

    'Keypress validation on finder and converting lower case to upper case
    Public Sub fndbranch_key_press(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    ''This will check the existence of value in databse on leave property of finder,if it exist then it will fill, otherwise it will blank the fields
    'Public Sub fndbranch_leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If fndbranch.txtValue.Text = "" Then
    '    Else
    '        Try
    '            Dim strquery As String = "select Branch_Code as [Branch Code],Branch_Name as [Branch Name], State_Code as [State Code],State_Name as [State Name] from TSPL_TDS_BRANCH_MASTER where branch_code='" + fndbranch.txtValue.Text + "'"
    '            Dim dr As SqlDataReader
    '            Dim strvalue As String

    '            dr = connectSql.RunSqlReturnDR(strquery)
    '            While dr.Read()
    '                strvalue = dr(0).ToString()
    '            End While
    '            If strvalue <> "" Then
    '            Else : strquery = ""
    '                txtbranch.Text = ""
    '                common.clsCommon.MyMessageBoxShow("This Branch Code does not exist in Master Table")
    '                fndbranch.txtValue.Text = ""
    '            End If
    '        Catch ex As Exception
    '            myMessages.myExceptions(ex)
    '        End Try
    '    End If
    'End Sub


#End Region


#Region "Function"

    'It will fill the  controls if value exist in database according to fndvendor
    'Public Sub funfillvendordes()
    '    Try

    '        Dim strquery As String = "select Vendor_Code as [Vendor Code],Vendor_Name as [Vendor Name] from TSPL_VENDOR_MASTER   where Vendor_Code='" + fndvendor.txtValue.Text + "'"

    '        Dim dr As SqlDataReader
    '        Dim strvalue As String
    '        dr = connectSql.RunSqlReturnDR(strquery)
    '        While dr.Read()

    '            txtvendes.Text = dr(1).ToString()
    '        End While
    '    Catch ex As Exception
    '        myMessages.myExceptions(ex)
    '    End Try
    'End Sub



    'It will fill the  controls if value exist in database according to fnddeduc
    'Public Sub funfillfnddeduc()
    '    Try

    '        Dim strquery As String = " select deduction_code ,description from TSPL_TDS_DEDUCTION_HEAD where deduction_code='" + fnddeduc.txtValue.Text + "'"
    '        Dim dr As SqlDataReader
    '        Dim strvalue As String
    '        dr = connectSql.RunSqlReturnDR(strquery)
    '        While dr.Read()

    '            txtdeduc.Text = dr(1).ToString()
    '        End While
    '    Catch ex As Exception
    '        myMessages.myExceptions(ex)
    '    End Try
    'End Sub




    'It will fill the  controls if value exist in database according to fndstate
    'Public Sub funfillfndstate()
    '    Try

    '        Dim strquery As String = "select State_Code,state_name from TSPL_TDS_STATE_MASTER where State_Code='" + fndstate.txtValue.Text + "'"
    '        Dim dr As SqlDataReader
    '        Dim strvalue As String
    '        dr = connectSql.RunSqlReturnDR(strquery)
    '        While dr.Read()

    '            txtstate.Text = dr(1).ToString()
    '        End While
    '    Catch ex As Exception
    '        myMessages.myExceptions(ex)
    '    End Try
    'End Sub


    'It will fill the  controls if value exist in database according to fndbranch
    'Public Sub funfillfndbranch()
    '    Try

    '        Dim strquery As String = "select Branch_Code as [Branch Code],Branch_Name as [Branch Name], State_Code as [State Code],State_Name as [State Name] from TSPL_TDS_BRANCH_MASTER where branch_code='" + fndbranch.txtValue.Text + "'"
    '        Dim dr As SqlDataReader
    '        Dim strvalue As String
    '        dr = connectSql.RunSqlReturnDR(strquery)
    '        While dr.Read()

    '            txtbranch.Text = dr(1).ToString()
    '        End While
    '    Catch ex As Exception
    '        myMessages.myExceptions(ex)
    '    End Try
    'End Sub



    'It will fill all controls in screen if find any existing data in table 
    Public Sub funfill()
        Try
            'Dim str As String = "select TSPL_TDS_VENDOR_DETAILS.Nature_Of_Deduction,TSPL_TDS_VENDOR_DETAILS.State_Code,TSPL_TDS_VENDOR_DETAILS.Vendor_Type,TSPL_TDS_VENDOR_DETAILS.status,TSPL_TDS_VENDOR_DETAILS.Branch_Code,TSPL_TDS_VENDOR_DETAILS.Inactive, ISNULL(TSPL_VENDOR_MASTER.PAN,'') AS PAN from TSPL_TDS_VENDOR_DETAILS LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code =TSPL_TDS_VENDOR_DETAILS.Vendor_Code where TSPL_TDS_VENDOR_DETAILS.Vendor_Code = '" + fndvendorNew.Value + "'"
            'Dim str As String = "select Case When ISNULL( TSPL_TDS_VENDOR_DETAILS.Nature_Of_Deduction,'') ='' Then ISNULL(TSPL_VENDOR_MASTER.Deduction_Code ,'') Else ISNULL( TSPL_TDS_VENDOR_DETAILS.Nature_Of_Deduction,'') End As Nature_Of_Deduction ,Case When ISNULL(  TSPL_TDS_VENDOR_DETAILS.State_Code,'') ='' Then ISNULL(TSPL_VENDOR_MASTER.TDS_State_Code  ,'') Else ISNULL( TSPL_TDS_VENDOR_DETAILS.State_Code ,'') End As State_Code  ,Case When ISNULL( TSPL_VENDOR_MASTER.TDS_Vendor_Type ,'') ='' Then ISNULL(TSPL_TDS_VENDOR_DETAILS.Vendor_Type ,'') Else ISNULL( TSPL_VENDOR_MASTER.TDS_Vendor_Type ,'') End As Vendor_Type ,Case When ISNULL(TSPL_VENDOR_MASTER.TDS_Status,'') ='' Then ISNULL( TSPL_TDS_VENDOR_DETAILS.status ,'') Else ISNULL(TSPL_VENDOR_MASTER.TDS_Status ,'') End As status,Case When ISNULL(TSPL_TDS_VENDOR_DETAILS.Branch_Code,'') ='' Then ISNULL(TSPL_VENDOR_MASTER.TDS_Branch_Code  ,'') Else ISNULL( TSPL_TDS_VENDOR_DETAILS.Branch_Code ,'') End As Branch_Code ,TSPL_TDS_VENDOR_DETAILS.Inactive, ISNULL(TSPL_VENDOR_MASTER.PAN,'') AS PAN from TSPL_TDS_VENDOR_DETAILS LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code =TSPL_TDS_VENDOR_DETAILS.Vendor_Code where TSPL_TDS_VENDOR_DETAILS.Vendor_Code = '" + clsCommon.myCstr(fndvendorNew.Value) + "'"
            Dim str As String = "Select ISNULL( TSPL_TDS_VENDOR_DETAILS.Nature_Of_Deduction,'')  AS Nature_Of_Deduction,ISNULL( TSPL_TDS_VENDOR_DETAILS.State_Code ,'')  As State_Code, ISNULL( TSPL_TDS_VENDOR_DETAILS.Vendor_TYpe ,'')  AS Vendor_TYpe,ISNULL(TSPL_TDS_VENDOR_DETAILS.Status ,'') AS Status ,ISNULL(TSPL_TDS_VENDOR_DETAILS.Branch_Code,'') AS Branch_Code, TSPL_TDS_VENDOR_DETAILS.Inactive, ISNULL(TSPL_VENDOR_MASTER.PAN,'') AS PAN from TSPL_TDS_VENDOR_DETAILS LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code =TSPL_TDS_VENDOR_DETAILS.Vendor_Code where TSPL_TDS_VENDOR_DETAILS.Vendor_Code = '" + clsCommon.myCstr(fndvendorNew.Value) + "'"
            'Dim dr As SqlDataReader
            'dr = connectSql.RunSqlReturnDR(str)
            Dim dr As DataTable = clsDBFuncationality.GetDataTable(str)
            'While dr.Read()
            If (dr IsNot Nothing AndAlso dr.Rows.Count > 0) Then

                fnddeducNew.Value = dr.Rows(0)("Nature_Of_Deduction").ToString()
                Dim deducdesc As String = "select  description  from TSPL_TDS_DEDUCTION_HEAD where deduction_code ='" & fnddeducNew.Value & "'"
                Dim deducdesc1 As String = clsDBFuncationality.getSingleValue(deducdesc)
                txtdeduc.Text = deducdesc1
                fndstatenew.Value = dr.Rows(0)("State_Code").ToString()
                Dim statedesc As String = "select  State_Name from TSPL_STATE_MASTER where State_Code='" & fndstatenew.Value & "'"
                Dim statedesc1 As String = clsDBFuncationality.getSingleValue(statedesc)
                txtstate.Text = statedesc1
                txtPan.Text = dr.Rows(0)("Pan").ToString()
                ddlventype.Text = dr.Rows(0)("Vendor_Type").ToString()
                ddlstatus.Text = dr.Rows(0)("status").ToString()
                fndbranchnew.Value = dr.Rows(0)("Branch_Code").ToString()
                Dim branchdesc As String = "select  Branch_Name from  TSPL_TDS_BRANCH_MASTER where Branch_Code='" & fndbranchnew.Value & "'"
                Dim branchdesc1 As String = clsDBFuncationality.getSingleValue(branchdesc)
                txtbranch.Text = branchdesc1
                Dim strchk As String = dr.Rows(0)("Inactive").ToString()
                If strchk = "Y" Then
                    chkinactive.Checked = True
                ElseIf strchk = "N" Then
                    chkinactive.Checked = False
                End If

            End If
            'End While
            btnsave.Enabled = True
            btnsave.Text = "Update"
            btndelete.Enabled = True
            'If userCode <> "ADMIN" Then
            '    If funSetUserAccess() = False Then Exit Sub
            'End If
        Catch ex As Exception

            myMessages.myExceptions(ex)
        End Try
    End Sub


    'Funtion for insertion of data
    Public Sub funinsert()
        Try
            Dim strchk As String = ""
            If chkinactive.Checked = True Then
                strchk = "Y"
            ElseIf chkinactive.Checked = False Then
                strchk = "N"
            End If
            If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False)) Then
                Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_VENDOR_MASTER where Vendor_Code='" & fndvendorNew.Value & "'")
                If ChkNewEntry = 0 Then
                    fndvendorNew.Value = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"), clsDocType.PartyDetails, "", "")
                    If clsCommon.myLen(fndvendorNew.Value) <= 0 Then
                        Throw New Exception("Error in Code Generation")
                    End If
                End If
            End If
            connectSql.RunSp("SP_TDS_VENDOR_DETAILS_INSERT", New SqlParameter("@Vendor_Code", fndvendorNew.Value), New SqlParameter("@Nature_Of_Deduction", fnddeducNew.Value), New SqlParameter("@State_Code", fndstatenew.Value), New SqlParameter("@Pan", txtPan.Text.ToString()), New SqlParameter("@Vendor_Type", ddlventype.Text.ToString()), New SqlParameter("@status", ddlstatus.Text.ToString()), New SqlParameter("@Branch_Code", fndbranchnew.Value), New SqlParameter("@Inactive", strchk), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate()), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate()), New SqlParameter("@compcode", companyCode))
            Dim strTDSBranch As String
            Dim strDedCode As String
            Dim strTDSStateCode As String
            If clsCommon.myLen(clsCommon.myCstr(fndbranchnew.Value)) > 0 Then
                strTDSBranch = "'" & clsCommon.myCstr(fndbranchnew.Value) & "'"
            Else
                strTDSBranch = "Null"
            End If

            If clsCommon.myLen(clsCommon.myCstr(fnddeducNew.Value)) > 0 Then
                strDedCode = "'" & clsCommon.myCstr(fnddeducNew.Value) & "'"
            Else
                strDedCode = "Null"
            End If

            If clsCommon.myLen(clsCommon.myCstr(fndstatenew.Value)) > 0 Then
                strTDSStateCode = "'" & clsCommon.myCstr(fndstatenew.Value) & "'"
            Else
                strTDSStateCode = "Null"
            End If
            clsDBFuncationality.ExecuteNonQuery("UPDATE TSPL_VENDOR_MASTER SET Is_TDS_Applicable= 1, Deduction_Code=" & strDedCode & ",TDS_State_Code=" & strTDSStateCode & ",state_code=" & strTDSStateCode & ",TDS_Vendor_Type='" & clsCommon.myCstr(ddlventype.Text) & "',TDS_Status='" & clsCommon.myCstr(ddlstatus.Text) & "',TDS_Branch_Code=" & strTDSBranch & " where Vendor_Code='" + fndvendorNew.Value + "'")
            Dim TDSState As String = ""
            Dim TDSBranch As String = ""
            If clsCommon.myLen(fndstatenew.Value) > 0 Then
                TDSState = "'" & clsCommon.myCstr(fndstatenew.Value) & "'"
            Else
                TDSState = "Null"
            End If

            If clsCommon.myLen(fndbranchnew.Value) > 0 Then
                TDSBranch = "'" & clsCommon.myCstr(fndbranchnew.Value) & "'"
            Else
                TDSBranch = "NULL"
            End If
            'Dim TDSQry As String = "Update TSPL_VENDOR_MASTER set Is_TDS_Applicable= 1,TDS_State_Code=" & TDSState & ", TDS_Status= '" & clsCommon.myCstr(ddlstatus.Text) & "', TDS_Vendor_Type= '" & clsCommon.myCstr(ddlventype.Text) & "', Deduction_Code= '" & clsCommon.myCstr(fnddeducNew.Value) & "',TDS_Branch_Code=" & TDSBranch & " where Vendor_Code='" + clsCommon.myCstr(fndvendorNew.Value) + "'"
            'clsDBFuncationality.ExecuteNonQuery(TDSQry)
            myMessages.insert()
            btnsave.Text = "Update"
            btndelete.Enabled = True

            'If userCode <> "ADMIN" Then
            '    If funSetUserAccess() = False Then Exit Sub
            'End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    'Funtion for updation  of data
    Public Sub funupdate()
        Try
            Dim strchk As String = ""
            If chkinactive.Checked = True Then
                strchk = "Y"
            ElseIf chkinactive.Checked = False Then
                strchk = "N"
            End If

            'If clsCommon.CompairString(fndbranchnew.Value, "Null") <> CompairStringResult.Equal Then
            '    If clsCommon.myLen(fndbranchnew.Value) > 0 Then
            '        fndbranchnew.Value = "'" & clsCommon.myCstr(fndbranchnew.Value) & "'"
            '    Else
            '        fndbranchnew.Value = "''"
            '    End If
            'Else
            '    fndbranchnew.Value = "''"
            'End If
            connectSql.RunSp("SP_TDS_VENDOR_DETAILS_UPDATE", New SqlParameter("@Vendor_Code", fndvendorNew.Value), New SqlParameter("@Nature_Of_Deduction", fnddeducNew.Value), New SqlParameter("@State_Code", fndstatenew.Value), New SqlParameter("@Pan", txtPan.Text.ToString()), New SqlParameter("@Vendor_Type", ddlventype.Text.ToString()), New SqlParameter("@status", ddlstatus.Text.ToString()), New SqlParameter("@Branch_Code", fndbranchnew.Value), New SqlParameter("@Inactive", strchk), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate()), New SqlParameter("@compcode", companyCode))
            Dim strTDSBranch As String
            Dim strDedCode As String
            Dim strTDSStateCode As String
            If clsCommon.myLen(clsCommon.myCstr(fndbranchnew.Value)) > 0 Then
                strTDSBranch = "'" & clsCommon.myCstr(fndbranchnew.Value) & "'"
            Else
                strTDSBranch = "Null"
            End If

            If clsCommon.myLen(clsCommon.myCstr(fnddeducNew.Value)) > 0 Then
                strDedCode = "'" & clsCommon.myCstr(fnddeducNew.Value) & "'"
            Else
                strDedCode = "Null"
            End If

            If clsCommon.myLen(clsCommon.myCstr(fndstatenew.Value)) > 0 Then
                strTDSStateCode = "'" & clsCommon.myCstr(fndstatenew.Value) & "'"
            Else
                strTDSStateCode = "Null"
            End If
            clsDBFuncationality.ExecuteNonQuery("UPDATE TSPL_VENDOR_MASTER SET Is_TDS_Applicable= 1, Deduction_Code=" & strDedCode & ",TDS_State_Code=" & strTDSStateCode & ",state_code=" & strTDSStateCode & ",TDS_Vendor_Type='" & clsCommon.myCstr(ddlventype.Text) & "',TDS_Status='" & clsCommon.myCstr(ddlstatus.Text) & "',TDS_Branch_Code=" & strTDSBranch & " where Vendor_Code='" + fndvendorNew.Value + "'")
            Dim TDSState As String = ""
            Dim TDSBranch As String = ""
            If clsCommon.myLen(fndstatenew.Value) > 0 Then
                TDSState = "'" & clsCommon.myCstr(fndstatenew.Value) & "'"
            Else
                TDSState = "Null"
            End If


            If clsCommon.myLen(fndbranchnew.Value) > 0 Then
                TDSBranch = "'" & clsCommon.myCstr(fndbranchnew.Value) & "'"
            Else
                TDSBranch = "NULL"
            End If
            
            'Dim TDSQry As String = "Update TSPL_VENDOR_MASTER set Is_TDS_Applicable= 1,TDS_State_Code=" & TDSState & ", TDS_Status= '" & clsCommon.myCstr(ddlstatus.Text) & "', TDS_Vendor_Type= '" & clsCommon.myCstr(ddlventype.Text) & "', Deduction_Code= '" & clsCommon.myCstr(fnddeducNew.Value) & "',TDS_Branch_Code=" & TDSBranch & " where Vendor_Code='" + clsCommon.myCstr(fndvendorNew.Value) + "'"
            'clsDBFuncationality.ExecuteNonQuery(TDSQry)
            myMessages.update()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    'Function for deletion of records
    Public Sub fundelete()
        Try

            connectSql.RunSp("SP_TDS_VENDOR_DETAILS_DELETE", New SqlParameter("@Vendor_Code", fndvendorNew.Value))

        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    'It will reset all the controls in screens
    Public Sub funreset()
        fndvendorNew.Value = ""
        txtvendes.Text = ""
        fnddeducNew.Value = ""
        txtdeduc.Text = ""
        fndstatenew.Value = ""
        txtstate.Text = ""
        fndbranchnew.Value = ""
        txtbranch.Text = ""
        txtPan.Text = ""
        chkinactive.Checked = False
        ddlventype.Text = "Individual"
        ddlstatus.Text = "Resident"
        btnsave.Text = "Save"
        btndelete.Enabled = False
        fndvendorNew.MyReadOnly = False
    End Sub

    Public Sub funexport()
        Dim str As String
        str = "select vendor_code as [Vendor Code],Nature_Of_Deduction as [Deduction],State_Code as [State],Pan as [PAN],Vendor_Type as [Vendor Type],status as [Status],Branch_Code as [Branch Code],Inactive as [Inactive] from TSPL_TDS_VENDOR_DETAILS "
        transportSql.ExporttoExcel(str, Me)
    End Sub

    Public Sub funimport()
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Vendor Code", "Deduction", "State", "PAN", "Vendor Type", "Status", "Branch Code", "Inactive") Then
            Dim trans As SqlTransaction = Nothing
            Dim linno As Integer = 0
            Try
                connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    linno += 1
                    Dim strcode As String = clsCommon.myCstr(grow.Cells(0).Value)
                    If (String.IsNullOrEmpty(strcode)) Or clsCommon.myLen(strcode) > 12 Then
                        Throw New Exception("Vendor Code can not be blank or Check the length")
                    End If

                    Dim strdeduction As String = clsCommon.myCstr(grow.Cells(1).Value)
                    If (String.IsNullOrEmpty(strdeduction)) Or clsCommon.myLen(strdeduction) > 95 Then
                        Throw New Exception(" Deduction  can not be blank or Check the length")
                    End If

                    Dim strstate As String = clsCommon.myCstr(grow.Cells(2).Value)
                    If clsCommon.myLen(strstate) > 12 Then
                        Throw New Exception(" Check the length of State")
                    End If
                    If clsCommon.myLen(strstate) > 0 Then
                        Dim qryState As String = "select Count(*) As Row from TSPL_State_MASTER where State_Code='" & strstate & "'"
                        Dim checkState As Integer = clsDBFuncationality.getSingleValue(qryState, trans)
                        If checkState <= 0 Then
                            Throw New Exception("Filled state code does not exist" + Environment.NewLine + ".First make the entry for state code at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                   

                    ' Dim QryPANNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(PAN,'') As PAN  from TSPL_VENDOR_MASTER where Vendor_Code ='" & strcode & "'"))

                    Dim strpan As String = clsCommon.myCstr(grow.Cells(3).Value)
                    If clsCommon.myLen(strpan) > 0 Then
                        'Throw New Exception(" Check the length of PAN No.")
                        Dim PANNo As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Count(*) As Row from TSPL_VENDOR_MASTER where Vendor_Code  ='" & strcode & "' And isnull(PAN,'')='" & strpan & "'", trans))
                        If PANNo = 0 Then
                            Throw New Exception("Please check ! PAN No (" & strpan & ") does not exist with vendor (" + strcode + ") at line no. " + clsCommon.myCstr(linno) + ".")
                        Else
                        End If
                    Else
                        Dim DBPANNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(PAN,'') AS PAN from TSPL_VENDOR_MASTER where Vendor_Code  ='" & strcode & "'", trans))
                        If clsCommon.myLen(DBPANNo) > 0 Then
                            Throw New Exception("Please fill PAN No according to vendor (" & strcode & ") at line no. " + clsCommon.myCstr(linno) + ".")
                        Else
                        End If
                    End If

                    Dim strventype As String = clsCommon.myCstr(grow.Cells(4).Value)
                    If clsCommon.myLen(strventype) > 20 Then
                        Throw New Exception(" Check the length of Vender Type.")
                    End If

                    Dim strstatus As String = clsCommon.myCstr(grow.Cells(5).Value)
                    If clsCommon.myLen(strstatus) > 20 Then
                        Throw New Exception(" Check the length of Status")
                    End If

                    Dim strbranch As String = clsCommon.myCstr(grow.Cells(6).Value)
                    If clsCommon.myLen(strbranch) > 0 Then
                        Dim qryTDSBranch As String = "select Count(*) As Row from TSPL_TDS_BRANCH_MASTER where Branch_Code='" & strbranch & "'"
                        Dim checkBranch As Integer = clsDBFuncationality.getSingleValue(qryTDSBranch, trans)
                        If checkBranch <= 0 Then
                            Throw New Exception("Filled TDS branch code does not exist" + Environment.NewLine + ".First make the entry for TDS branch code at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    If clsCommon.myLen(strbranch) > 12 Then
                        Throw New Exception("Check the length of Branch Code")
                    End If

                    Dim strinactive As String = clsCommon.myCstr(grow.Cells(7).Value)
                    If (strinactive = "Y" Or strinactive = "N") Then
                    Else
                        Throw New Exception("Value for Inactive should be  Y or N")
                    End If

                    Dim qryNatureDed As String = ""
                    If clsCommon.myLen(strcode) > 0 Then
                        qryNatureDed = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Non_PAN_No  From TSPL_TDS_DEDUCTION_HEAD where Deduction_Code ='" & clsCommon.myCstr(strdeduction) & "'", trans))
                    End If
                    If clsCommon.CompairString(qryNatureDed.ToUpper().Trim(), "Y") = CompairStringResult.Equal AndAlso clsCommon.myLen(strpan) > 0 Then
                        txtPan.Focus()
                        Throw New Exception("You can not make this entry with Non PAN nature of deduction as PAN No exists at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    '' Anubhooti 31-Oct-2014
                    Dim VenStateCode As String = ""
                    If clsCommon.myLen(strcode) > 0 Then
                        VenStateCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(State_Code,'') AS State_Code From TSPL_VENDOR_MASTER where Vendor_Code ='" & clsCommon.myCstr(strcode) & "'", trans))
                    End If
                    Dim strTDSBranch As String
                    Dim strDedCode As String
                    Dim strTDSStateCode As String
                    If clsCommon.myLen(clsCommon.myCstr(strbranch)) > 0 Then
                        strTDSBranch = "'" & strbranch & "'"
                    Else
                        strTDSBranch = "Null"
                    End If

                    If clsCommon.myLen(clsCommon.myCstr(strdeduction)) > 0 Then
                        strDedCode = "'" & strdeduction & "'"
                    Else
                        strDedCode = "Null"
                    End If

                    If clsCommon.myLen(clsCommon.myCstr(strstate)) > 0 Then
                        strTDSStateCode = "'" & strstate & "'"
                    Else
                        strTDSStateCode = "Null"
                    End If
                    If clsCommon.myLen(VenStateCode) > 0 AndAlso clsCommon.myLen(strstate) > 0 Then
                        If clsCommon.CompairString(VenStateCode, clsCommon.myCstr(strstate)) <> CompairStringResult.Equal Then
                            fndstatenew.Focus()
                            Throw New Exception("Please check ! vendor state code (" & VenStateCode & ") must be same as party state code at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    
                    Dim sql1 As String = "select count(*) from TSPL_TDS_VENDOR_DETAILS where vendor_code='" + strcode + "'"
                    Dim i As Integer = CInt(connectSql.RunScalar(trans, sql1))
                    If (i = 0) Then

                        connectSql.RunSpTransaction(trans, "SP_TDS_VENDOR_DETAILS_INSERT", New SqlParameter("@Vendor_Code", strcode), New SqlParameter("@Nature_Of_Deduction", strdeduction), New SqlParameter("@State_Code", strstate), New SqlParameter("@Pan", strpan), New SqlParameter("@Vendor_Type", strventype), New SqlParameter("@status", strstatus), New SqlParameter("@Branch_Code", strbranch), New SqlParameter("@Inactive", strinactive), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate(trans)), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode))
                    Else
                        connectSql.RunSpTransaction(trans, "SP_TDS_VENDOR_DETAILS_UPDATE", New SqlParameter("@Vendor_Code", strcode), New SqlParameter("@Nature_Of_Deduction", strdeduction), New SqlParameter("@State_Code", strstate), New SqlParameter("@Pan", strpan), New SqlParameter("@Vendor_Type", strventype), New SqlParameter("@status", strstatus), New SqlParameter("@Branch_Code", strbranch), New SqlParameter("@Inactive", strinactive), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode))

                    End If
                    clsDBFuncationality.ExecuteNonQuery("UPDATE TSPL_VENDOR_MASTER SET Is_TDS_Applicable=1, Deduction_Code=" & strDedCode & ",TDS_State_Code=" & strTDSStateCode & ",state_code=" & strTDSStateCode & ",TDS_Vendor_Type='" & strventype & "',TDS_Status='" & strstatus & "',TDS_Branch_Code=" & strTDSBranch & " where Vendor_Code='" + strcode + "'", trans)
                    Dim TDSState As String = ""
                    If clsCommon.myLen(strstate) > 0 Then
                        TDSState = "'" & clsCommon.myCstr(strstate) & "'"
                    Else
                        TDSState = "Null"
                    End If
                    If clsCommon.myLen(strbranch) > 0 Then
                        strbranch = "'" & strbranch & "'"
                    Else
                        strbranch = "NULL"
                    End If
                    'Dim TDSQry As String = "Update TSPL_VENDOR_MASTER set Is_TDS_Applicable=1,TDS_State_Code=" & TDSState & ", TDS_Status= '" & clsCommon.myCstr(strstatus) & "', TDS_Vendor_Type= '" & clsCommon.myCstr(strventype) & "', Deduction_Code= '" & clsCommon.myCstr(strdeduction) & "',TDS_Branch_Code=" & strbranch & " where Vendor_Code='" + strcode + "'"
                    'connectSql.RunSqlTransaction(trans, TDSQry)
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


#End Region

#Region "Button Click"

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Function AllowToSave() As Boolean
        Dim qryNatureDed As String = ""
        Dim VenStateCode As String = ""
        If clsCommon.myLen(fnddeducNew.Value) > 0 Then
            qryNatureDed = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Non_PAN_No From TSPL_TDS_DEDUCTION_HEAD where Deduction_Code ='" & clsCommon.myCstr(fnddeducNew.Value) & "'"))
        End If

        If clsCommon.myLen(fndvendorNew.Value) > 0 Then
            VenStateCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(State_Code,'') AS State_Code From TSPL_VENDOR_MASTER where Vendor_Code ='" & clsCommon.myCstr(fndvendorNew.Value) & "'"))
        End If

        If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False) = False) AndAlso fndvendorNew.Value = "" Then
            fndvendorNew.Focus()
            Throw New Exception("Vendor Code can't be left blank")
        ElseIf fnddeducNew.Value = "" Then
            fnddeducNew.Focus()
            Throw New Exception("Nature of Deduction can't be left blank")
            'ElseIf fndbranchnew.Value = "" Then
            '    fndbranchnew.Focus()
            '    Throw New Exception("Branch Code can't be left blank")
            'ElseIf txtPan.Text = "" Then
            '    txtPan.Focus()
            '    Throw New Exception("PAN No. can't be left blank")
        ElseIf clsCommon.CompairString(qryNatureDed.ToUpper().Trim(), "Y") = CompairStringResult.Equal AndAlso clsCommon.myLen(txtPan.Text) > 0 Then
            txtPan.Focus()
            Throw New Exception("You can not make this entry with Non PAN nature of deduction as PAN No exists.")
       
        End If
        If clsCommon.myLen(VenStateCode) > 0 AndAlso clsCommon.myLen(fndstatenew.Value) > 0 Then
            If clsCommon.CompairString(VenStateCode, clsCommon.myCstr(fndstatenew.Value)) <> CompairStringResult.Equal Then
                fndstatenew.Focus()
                Throw New Exception("Please check ! vendor state code (" & VenStateCode & ") must be same as party state code.")
            End If
        End If
        Return True
    End Function

    Sub SaveData()
        Try
            If AllowToSave() Then

                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.PartyDetails, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If
                If btnsave.Text = "Save" Then
                    funinsert()
                ElseIf btnsave.Text = "Update" Then
                    funupdate()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()

    End Sub
    Sub DeleteData()
        If fndvendorNew.Value = "" Then
            myMessages.blankValue("Nature of Deduction")
            Exit Sub
        End If
        Dim qry As String = "select Vendor_Code from TSPL_REMITTANCE where Vendor_Code = '" + fndvendorNew.Value + "'"
        qry += " union all"
        qry += " select Vendor_Code from TSPL_PI_REMITTANCE where Vendor_Code = '" + fndvendorNew.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            clsCommon.MyMessageBoxShow(Me, "TDS Entry Exists against this vendor.Can't delete it", Me.Text)
            Exit Sub
        End If
        If myMessages.deleteConfirm() Then
            fundelete()
            myMessages.delete()
            btnsave.Text = "Save"
            btndelete.Enabled = False
        End If
    End Sub
    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        funreset()
    End Sub

    Private Sub MenuImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuImport.Click
        funimport()
    End Sub

    Private Sub menuExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuExport.Click
        funexport()
    End Sub
    Private Sub menuClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuClose.Click
        Me.Close()
    End Sub
#End Region


#Region "Finder Load"
    'Private Sub fndbranch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fndbranch.Load
    '    fndbranch.ConnectionString = connectSql.SqlCon()
    '    fndbranch.Query = "select Branch_Code as [Branch Code],Branch_Name as [Branch Name], State_Code as [State Code],State_Name as [State Name] from TSPL_TDS_BRANCH_MASTER"
    '    fndbranch.ValueToSelect = "Branch Code"
    '    fndbranch.Caption = "Branch Master"
    '    fndbranch.txtValue.MaxLength = 12
    '    fndbranch.ValueToSelect1 = "Branch Name"
    'End Sub
    '' Added By Abhishek 4 june 2012
    Private Sub fndbranchnew__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndbranchnew._MYValidating
        Dim query As String = "select Branch_Code as [Code],Branch_Name as [Branch Name], State_Code as [State Code],State_Name as [State Name] from TSPL_TDS_BRANCH_MASTER"
        fndbranchnew.Value = clsCommon.ShowSelectForm("BranchCodevald", query, "Code", "", fndbranchnew.Value, "Code", isButtonClicked)
        Dim desc As String = "select  Branch_Name from  TSPL_TDS_BRANCH_MASTER where Branch_Code='" & fndbranchnew.Value & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(desc)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            txtbranch.Text = clsCommon.myCstr(dt.Rows(0)("Branch_Name"))
        Else
            txtbranch.Text = ""
        End If
    End Sub

    Private Sub fndvendorNew__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndvendorNew._MYValidating
        'Dim Qry As String = " Select Count(*)  from TSPL_VENDOR_MASTER where  Vendor_Code ='" & fndvendorNew.Value & "'"
        'Dim Count As String = clsDBFuncationality.getSingleValue(Qry)
        'If Count = 0 Then
        '    fndvendorNew.MyReadOnly = False
        'Else
        '    fndvendorNew.MyReadOnly = True
        'End If
        fndvendorNew.MyReadOnly = True
        If fndvendorNew.MyReadOnly Or isButtonClicked Then
            Dim qry1 As String = "select Vendor_Code as [Code],Vendor_Name as [Vendor Name],Vendor_Group_Code as [Vendor Group],(select case when Status='N' then 'Active' else 'In.Active' end ) as [Status]  from TSPL_VENDOR_MASTER "
            fndvendorNew.Value = clsCommon.ShowSelectForm("VendorCodefndr", qry1, "Code", "", fndvendorNew.Value, "Code", isButtonClicked)
            Dim desc As String = "select  Vendor_Name,ISNULL(PAN,'') AS PAN from  TSPL_VENDOR_MASTER where Vendor_Code ='" & fndvendorNew.Value & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(desc)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                txtvendes.Text = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
                '' Anubhooti 16-Sep-2014 BM00000003934
                txtPan.Text = clsCommon.myCstr(dt.Rows(0)("PAN"))
            Else
                txtvendes.Text = ""
                txtPan.Text = ""
            End If
            Loaddata()
        End If
                            End Sub
    Private Sub fndvendorNew__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndvendorNew._MYNavigator
        Dim qry As String = "select Vendor_Code from TSPL_VENDOR_MASTER where 2=2"
        Select Case NavType
            Case NavigatorType.Current
                qry += " and TSPL_VENDOR_MASTER .Vendor_Code in ('" + fndvendorNew.Value + "')"
            Case NavigatorType.Next
                qry += " and TSPL_VENDOR_MASTER  .Vendor_Code in (select min(Vendor_Code) from TSPL_VENDOR_MASTER where Vendor_Code >'" + fndvendorNew.Value + "')"
            Case NavigatorType.First
                qry += " and TSPL_VENDOR_MASTER  .Vendor_Code in (select MIN(Vendor_Code) from TSPL_VENDOR_MASTER)"

            Case NavigatorType.Last
                qry += " and TSPL_VENDOR_MASTER  .Vendor_Code in (select Max(Vendor_Code) from TSPL_VENDOR_MASTER)"
            Case NavigatorType.Previous
                qry += " and TSPL_VENDOR_MASTER  .Vendor_Code in (select Max(Vendor_Code ) from TSPL_VENDOR_MASTER where Vendor_Code <'" + fndvendorNew.Value + "')"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            fndvendorNew.Value = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
        End If

        Loaddata()
    End Sub
  

    'Private Sub fndvendor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fndvendor.Load
    '    fndvendor.Query = "select Vendor_Code as [Vendor Code],Vendor_Name as [Vendor Name],Vendor_Group_Code as [Vendor Group],(select case when Status='N' then 'Active' else 'In.Active' end ) as [Status] from TSPL_VENDOR_MASTER  "
    '    fndvendor.ConnectionString = connectSql.SqlCon()
    '    fndvendor.Caption = "Vendor"
    '    fndvendor.ValueToSelect = "Vendor Code"
    '    fndvendor.ValueToSelect1 = "Vendor Name"
    'End Sub
    Private Sub fnddeducNew__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fnddeducNew._MYValidating
        Dim query As String = "select deduction_code As [Code],description  as [Description] from TSPL_TDS_DEDUCTION_HEAD"
        fnddeducNew.Value = clsCommon.ShowSelectForm("DeductionCodevald", query, "Code", "", fnddeducNew.Value, "Code", isButtonClicked)
        Dim desc As String = "select  description  from TSPL_TDS_DEDUCTION_HEAD where deduction_code ='" & fnddeducNew.Value & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(desc)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            txtdeduc.Text = clsCommon.myCstr(dt.Rows(0)("description"))
        Else
            txtdeduc.Text = ""
        End If
    End Sub
    'Private Sub fnddeduc_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fnddeduc.Load
    '    fnddeduc.ConnectionString = connectSql.SqlCon()
    '    fnddeduc.Query = " select deduction_code As [Nature Of Deduction],description  as [Description] from TSPL_TDS_DEDUCTION_HEAD "
    '    fnddeduc.ValueToSelect = "Nature Of Deduction"
    '    fnddeduc.Caption = "Nature Of Deduction"
    '    fnddeduc.ValueToSelect1 = "Description"
    'End Sub

    Private Sub fndstatenew__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndstatenew._MYValidating
        Dim query As String = "select State_Code as [Code],State_Name as [State Name] from TSPL_STATE_MASTER"
        fndstatenew.Value = clsCommon.ShowSelectForm("StateCodefrm", query, "Code", "", fndstatenew.Value, "Code", isButtonClicked)
        Dim desc As String = "select  State_Name from TSPL_STATE_MASTER where State_Code='" & fndstatenew.Value & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(desc)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            txtstate.Text = clsCommon.myCstr(dt.Rows(0)("State_Name"))
        Else
            txtstate.Text = ""
        End If

    End Sub


    'Private Sub fndstate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fndstate.Load
    '    fndstate.ConnectionString = connectSql.SqlCon()
    '    fndstate.Query = "select state_Code as [state Code],State_Name as [State Name] from TSPL_TDS_STATE_MASTER"
    '    fndstate.Caption = "State Code"
    '    fndstate.ValueToSelect = "state Code"
    '    fndstate.ValueToSelect1 = "State Name"
    'End Sub
#End Region

    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "PARTY-DET"
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


   

  
    Private Sub fndstatenew_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles fndstatenew.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub fndbranchnew_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles fndbranchnew.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    
    Private Sub fnddeducNew_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles fnddeducNew.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub



    Private Sub fndvendorNew_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles fndvendorNew.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

End Class
