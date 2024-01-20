'Created By---> Mayank
'Created Date--->25/may/2011
'Modified By--> mayank
'Last Modify Date-->03/june/2011
'Tables Used-->TSPL_USER_GROUP_MAPPING ,TSPL_USER_GROUP_MASTER
Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.WinControls.Enumerations
Imports common
Public Class frmDeptHeadCustomerMapping
    Inherits FrmMainTranScreen
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim ds As New DataSet
    Dim IsInsideLoadData As Boolean = False
    Private Sub SetUserMgmtNew()
        '' Anubhooti 30-July-2014 BM00000003130
        'MyBase.SetUserMgmt(clsUserMgtCode.userGroupMapping)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function
        End If
        rbtnSave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 03/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If MyBase.isModifyFlag = True Then
            RadMenuItem_Import.Enabled = True
            RadMenuItem_Export.Enabled = True
        Else
            RadMenuItem_Import.Enabled = False
            RadMenuItem_Export.Enabled = False
        End If
        '--------------------------------------------------
        'rbtnPost.Visible = MyBase.isPostFlag
        rbtnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    'Public Sub New(ByVal user As String, ByVal company As String)
    '    InitializeComponent()
    '    userCode = user
    '    companyCode = company
    'End Sub

    Private Sub frmDeptHeadCustomerMapping_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso rbtnSave.Enabled Then
            SaveData()
            'ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            '    PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso rbtnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()

        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()

        End If
    End Sub
    Private Sub frmDeptHeadCustomerMapping_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        '      globalFunc.mandatoryText(fndUser_Name.Value)

        userCode = objCommonVar.CurrentUserCode
        companyCode = objCommonVar.CurrentCompanyCode

        fun_gridfill()
        rbtnDelete.Enabled = False
        'AddHandler fndUser_Name.ValueChanged, AddressOf text_changed
        'AddHandler fndUser_Name.Value.Leave, AddressOf fndUser_Name_Leave
        'AddHandler fndUser_Name.Value.KeyPress, AddressOf fndUser_Name_KeyPress

        ButtonToolTip.SetToolTip(rbtnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(rbtnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(rbtnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(rbtnReset, "Press Alt+N Adding New Trasnaction")
        textchangedsub()
        fndUser_NameLeave()

        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub
    'This is FunGrid Function Used To Fill Records From TSPL_USER_GROUP_MASTER In Grid
    Private Sub fun_gridfill()
        IsInsideLoadData = True
        dgv_Groupmapping.AutoGenerateColumns = False
        Try
            'dgv_Groupmapping.Cursor
            'dgv_Groupmapping.Property("IRowSetIdentity") = True
            'dgv_Groupmapping.CursorType = adKeyset
            'dgv_Groupmapping.LockType = adLockOptimistic
            Dim strQuery As String = "select Cust_Group_Code as Group_Code, Cust_Group_Desc as Group_Desc from TSPL_CUSTOMER_GROUP_MASTER"
            transportSql.FillGridView(strQuery, dgv_Groupmapping)
            dgv_Groupmapping.Columns(0).FieldName = "Group_Code"
            dgv_Groupmapping.Columns(1).FieldName = "Group_Desc"
            dgv_Groupmapping.Select()
            textbox_lostfocus()

            'End With

            'dgv_Groupmapping.CurrentCell = New DataGridCell(0, 0)
            IsInsideLoadData = False
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message.ToString(), Me.Text)
        End Try
    End Sub
    Sub textbox_lostfocus()
        dgv_Groupmapping.Select()
        ''dgv_Groupmapping.CurrentCell = New DataGridCell(0, 0)
    End Sub

    'This is Insert Function Used To Insert Values In TSPL_USER_GROUP_MAPPING
    Private Sub fun_insert()
        Dim trans As SqlTransaction = Nothing
        Try
            trans = clsDBFuncationality.GetTransactin()
            For i As Integer = 0 To dgv_Groupmapping.Rows.Count - 1
                If CBool(dgv_Groupmapping.Rows(i).Cells(2).Value = True) Then
                    connectSql.RunSpTransaction(trans, "SP_TSPL_CUSTOMER_GROUP_MAPPING", New SqlParameter("@User_Code", fndUser_Name.Value), New SqlParameter("@Cust_Group_Code", dgv_Groupmapping.Rows(i).Cells("GroupCode").Value), New SqlParameter("@created_by", userCode), New SqlParameter("@created_date", connectSql.serverDate(trans)), New SqlParameter("Modify_By", userCode), New SqlParameter("Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", companyCode))


                    Dim arr As Dictionary(Of String, Object) = dgv_Groupmapping.Rows(i).Tag
                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
                        'strWhrCatg += " and Location_Code in ("
                        'Dim isFirstTime As Boolean = True
                        For Each strInn As String In arr.Keys
                            'If Not isFirstTime Then
                            '    strWhrCatg += ","
                            'End If
                            'strWhrCatg += "'" + strInn + "'"
                            connectSql.RunSpTransaction(trans, "SP_TSPL_CUSTOMER_GROUP_MAPPING_DETAIL", New SqlParameter("@User_Code", fndUser_Name.Value), New SqlParameter("@Cust_Group_Code", dgv_Groupmapping.Rows(i).Cells("GroupCode").Value), New SqlParameter("@Cust_Code", strInn))
                            'isFirstTime = False
                        Next
                        'strWhrCatg += ")"
                    End If

                End If
            Next
            Dim strUser_Code As String = "select User_Code from TSPL_CUSTOMER_GROUP_MAPPING where User_Code='" + fndUser_Name.Value + "'"
            Dim dr As DataTable
            dr = clsDBFuncationality.GetDataTable(strUser_Code, trans)
            If dr.Rows.Count > 0 Then
                rbtnSave.Text = "Update"
                rbtnDelete.Enabled = True
                myMessages.insert()
                'If userCode <> "ADMIN" Then
                '    If funSetUserAccess() = False Then Exit Sub
                'End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "Please Check Status", Me.Text)
            End If
            trans.Commit()
        Catch ex As Exception
            myMessages.myExceptions(ex)
            trans.Rollback()
        End Try
    End Sub
    'It Is Used To Save And Update All Record To TSPL_USER_GROUP_MAPPING
    Private Sub rbtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnSave.Click
        SaveData()
    End Sub
    Sub SaveData()

        If MyBase.isModifyonPasswordFlag Then
            If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.userGroupMapping, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
            Else
                Return
            End If
        End If

        If fndUser_Name.Value = "" Then
            myMessages.blankValue("User Code")
            fndUser_Name.Focus()
        ElseIf rbtnSave.Text = "Save" Then
            fun_insert()
        Else
            fun_Update()
        End If
    End Sub
    'It Is Used To Fill The User Code and User Name in fndUser_Name and TxtUserName Respectively from TSPL_USER_MASTER
    Private Sub fndUser_Name_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '   fndUser_Name.ConnectionString = connectSql.SqlCon()
        '    fndUser_Name.Query = "select User_Code as [User Code],User_Name as [User Name] from TSPL_USER_MASTER"
        'fndUser_Name.ValueToSelect = "User Code"
        'fndUser_Name.Caption = "User Master"
        'fndUser_Name.Value.MaxLength = 12
        'fndUser_Name.ValueToSelect1 = "User Name"
    End Sub
    'It Is Used To Fill Or Clear All Fields of Current Windows Form Bassed On User Code(fndUser_Name) From TSPL_USER_GROUP_MAPPING
    Public Sub text_changed(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim strname As String = "select User_Name from TSPL_USER_MASTER where User_Code='" + fndUser_Name.Value + "'"
        'Dim dr1 As SqlDataReader
        'dr1 = connectSql.RunSqlReturnDR(strname)
        'Dim strvalue1 As String
        'If dr1.Read() Then
        '    strvalue1 = dr1(0).ToString()
        'End If
        'Dim strUser_Code As String = "select User_Code from TSPL_USER_GROUP_MAPPING where User_Code='" + fndUser_Name.Value + "'"
        'Dim dr As SqlDataReader
        'dr = connectSql.RunSqlReturnDR(strUser_Code)
        'Dim strvalue As String
        'If dr.Read() Then
        '    strvalue = dr(0).ToString()
        'End If
        'If (strvalue <> "") Then
        '    funfill()
        '    rbtnSave.Text = "Update"
        '    rbtnDelete.Enabled = True
        'Else
        '    fun_gridfill()
        '    rbtnSave.Text = "Save"
        '    rbtnDelete.Enabled = False
        '    TxtUserName.Text = " "
        'End If
        'TxtUserName.Text = strvalue1
    End Sub
    Sub textchangedsub()
        Dim strname As String = "select User_Name from TSPL_USER_MASTER where User_Code='" + fndUser_Name.Value + "' and isnull(Department_Head,0)=1"
        Dim strvalue1 As String
        strvalue1 = clsDBFuncationality.getSingleValue(strname)

        'If dr1.Read() Then
        '    strvalue1 = dr1(0).ToString()
        'End If
        Dim strUser_Code As String = "select User_Code from TSPL_CUSTOMER_GROUP_MAPPING where User_Code='" + fndUser_Name.Value + "'"
        Dim strvalue As String
        strvalue = clsDBFuncationality.getSingleValue(strUser_Code)

        'If dr.Read() Then
        '    strvalue = dr(0).ToString()
        'End If
        If (strvalue <> "") Then
            funfill()
            rbtnSave.Text = "Update"
            rbtnDelete.Enabled = True
        Else
            fun_gridfill()
            rbtnSave.Text = "Save"
            rbtnDelete.Enabled = False
            TxtUserName.Text = " "
        End If
        TxtUserName.Text = strvalue1
    End Sub
    'This is Funfill Function Used To Fill All Fields of Current Windows Form.
    Private Sub funfill()
        IsInsideLoadData = True
        Dim strQuery As String = "select Cust_Group_Code as [Group Code] from TSPL_CUSTOMER_GROUP_MAPPING WHERE User_Code='" + fndUser_Name.Value + "'"
        Dim da As New SqlDataAdapter(strQuery, connectSql.SqlCon())
        Dim dt As New DataTable()
        da.Fill(dt)
        If dt.Rows.Count > 0 Then
            Dim row As DataRow = dt.Rows(0)
            For j As Integer = 0 To dt.Rows.Count - 1
                Dim strcode As String = dt.Rows(j)(0).ToString()
                For i As Integer = 0 To dgv_Groupmapping.Rows.Count - 1
                    If dgv_Groupmapping.Rows(i).Cells(0).Value = strcode Then
                        dgv_Groupmapping.Rows(i).Cells(2).Value = True

                        'skg
                        Dim arr As Dictionary(Of String, Object) = Nothing
                        arr = New Dictionary(Of String, Object)
                        Dim dtcustomer As DataTable
                        dtcustomer = clsDBFuncationality.GetDataTable("select Cust_Code from TSPL_CUSTOMER_GROUP_MAPPING_DETAIL  where User_Code='" + fndUser_Name.Value + "' and Cust_Group_Code='" + clsCommon.myCstr(dgv_Groupmapping.Rows(i).Cells("GroupCode").Value) + "'")
                        For k As Integer = 0 To dtcustomer.Rows.Count - 1
                            arr.Add(clsCommon.myCstr(dtcustomer.Rows(k).Item(0)), Nothing)
                        Next

                        dgv_Groupmapping.Rows(i).Tag = arr
                        'skg


                    End If
                Next
            Next
        End If
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        IsInsideLoadData = False
    End Sub
    'It Is Used To Close The Current Windows Form
    Private Sub rbtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnClose.Click
        Me.Close()
    End Sub
    'It Is Used To Delete The Record From TSPL_USER_GROUP_MAPPING
    Private Sub rbtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnDelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If fndUser_Name.Value = "" Then
            myMessages.blankValue("User Code")
            fndUser_Name.Focus()
        ElseIf myMessages.deleteConfirm() Then
            Dim str As String = ""
            str = "delete from TSPL_CUSTOMER_GROUP_MAPPING_DETAIL where User_Code='" + fndUser_Name.Value + "'"
            connectSql.RunSql(str)
            str = "delete from TSPL_CUSTOMER_GROUP_MAPPING where User_Code='" + fndUser_Name.Value + "'"
            connectSql.RunSql(str)

            myMessages.delete()
            rbtnSave.Text = "Save"
            rbtnDelete.Enabled = False
            funReset()
        End If
    End Sub
    'It Is Used To Export The Records From TSPL_USER_GROUP_MAPPING
    Private Sub RadMenuItem_Export_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem_Export.Click
        Dim strQuery As String = "select TSPL_CUSTOMER_GROUP_MAPPING_DETAIL.User_Code as [User Code],TSPL_CUSTOMER_GROUP_MAPPING_DETAIL.Cust_Group_Code as [Customer Group Code]" & _
        " ,TSPL_CUSTOMER_GROUP_MAPPING_DETAIL.Cust_Code as [Customer Code] from TSPL_CUSTOMER_GROUP_MAPPING_DETAIL  " & _
        " join TSPL_CUSTOMER_GROUP_MAPPING on TSPL_CUSTOMER_GROUP_MAPPING.Cust_Group_Code = TSPL_CUSTOMER_GROUP_MAPPING_DETAIL.Cust_Group_Code " & _
        " and TSPL_CUSTOMER_GROUP_MAPPING.User_Code = TSPL_CUSTOMER_GROUP_MAPPING_DETAIL.User_Code "
        transportSql.ExporttoExcel(strQuery, Me)
    End Sub
    'It Is Used To Import The Records From TSPL_USER_GROUP_MAPPING
    Private Sub RadMenuItem_Import_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem_Import.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "User Code", "Customer Group Code", "Customer Code") Then
            Dim trans As SqlTransaction = Nothing
            Try
                trans = clsDBFuncationality.GetTransactin()
                Dim UserCode As String = ""
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim LineNo As String = clsCommon.myCstr(grow.Index + 2)
                    Dim stru_code As String = grow.Cells(0).Value.ToString()
                    Dim strg_code As String = grow.Cells(1).Value.ToString()
                    Dim strcustomer_code As String = grow.Cells(2).Value.ToString()
                    '--------------------------------------------------------------------------------------------------
                    'If Not clsCommon.CompairString(UserCode, stru_code) = CompairStringResult.Equal Then
                    '    clsDBFuncationality.ExecuteNonQuery("delete From TSPL_USER_GROUP_MAPPING Where User_Code='" + stru_code + "'", trans)
                    'End If

                    If Not clsCommon.CompairString(UserCode, stru_code) = CompairStringResult.Equal Then
                        clsDBFuncationality.ExecuteNonQuery("delete From TSPL_CUSTOMER_GROUP_MAPPING_DETAIL Where User_Code='" + stru_code + "'", trans)
                        clsDBFuncationality.ExecuteNonQuery("delete From TSPL_CUSTOMER_GROUP_MAPPING Where User_Code='" + stru_code + "'", trans)
                    End If

                    '--------------------------------------------------------------------------------------------------
                    UserCode = stru_code
                    If clsCommon.myLen(stru_code) > 0 Then
                        UserCode = clsDBFuncationality.getSingleValue("Select User_Code from TSPL_USER_MASTER Where User_Code='" + stru_code + "'", trans)
                        If clsCommon.myLen(UserCode) <= 0 Then
                            Throw New Exception("User Code '" + stru_code + "' at line " + LineNo + " does not exist .")
                        End If
                    Else
                        Throw New Exception("User Code can not be blank at line " + LineNo + " .")
                    End If

                    Dim GroupCode As String
                    If clsCommon.myLen(strg_code) > 0 Then
                        GroupCode = clsDBFuncationality.getSingleValue("Select Cust_Group_Code from TSPL_CUSTOMER_GROUP_MASTER Where Cust_Group_Code='" + strg_code + "'", trans)
                        If clsCommon.myLen(GroupCode) <= 0 Then
                            Throw New Exception("Customer Group Code '" + strg_code + "' at line " + LineNo + " does not exist .")
                        End If
                    Else
                        Throw New Exception("Customer Group Code can not be blank at line " + LineNo + " .")
                    End If

                    Dim CustomerCode As String
                    If clsCommon.myLen(strcustomer_code) > 0 Then
                        CustomerCode = clsDBFuncationality.getSingleValue("Select Cust_Code from TSPL_CUSTOMER_MASTER Where Cust_Code='" + strcustomer_code + "'", trans)
                        If clsCommon.myLen(CustomerCode) <= 0 Then
                            Throw New Exception("Customer Code '" + strg_code + "' at line " + LineNo + " does not exist .")
                        End If
                    Else
                        Throw New Exception("Customer Code can not be blank at line " + LineNo + " .")
                    End If

                    'skg
                    Dim sql As String = "select COUNT(*) from TSPL_CUSTOMER_MASTER  where Cust_Code='" + CustomerCode + "' and Cust_Group_Code='" + GroupCode + "'"
                    Dim j As Integer = CInt(connectSql.RunScalar(trans, sql))
                    If (j = 0) Then
                        Throw New Exception("Customer does not map with Customer Group at line " + LineNo + " .")
                    End If
                    'skg


                    Dim sql1 As String = "select COUNT(*) from TSPL_CUSTOMER_GROUP_MAPPING  where User_Code='" + UserCode + "' and Cust_Group_Code='" + GroupCode + "'"
                    Dim i As Integer = CInt(connectSql.RunScalar(trans, sql1))
                    If (i = 0) Then
                        connectSql.RunSpTransaction(trans, "SP_TSPL_CUSTOMER_GROUP_MAPPING", New SqlParameter("@User_Code", UserCode), New SqlParameter("@Cust_Group_Code", GroupCode), New SqlParameter("@Created_By", UserCode), New SqlParameter("@Created_Date", connectSql.serverDate(trans)), New SqlParameter("@Modify_By", UserCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", companyCode))
                    End If

                    connectSql.RunSpTransaction(trans, "SP_TSPL_CUSTOMER_GROUP_MAPPING_DETAIL", New SqlParameter("@User_Code", UserCode), New SqlParameter("@Cust_Group_Code", GroupCode), New SqlParameter("@Cust_Code", CustomerCode))

                Next
                trans.Commit()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                myMessages.myExceptions(ex)
                trans.Rollback()

            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub
    'It Is Used To Close The Current Windows Form
    Private Sub RadMenuItem_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem_Close.Click
        Me.Close()
    End Sub
    'It is Used To Clear All Fields Of Current Windows Form
    Private Sub rbtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnReset.Click
        funReset()
    End Sub
    'This is Reset Function Used To Clear All Fields Of Current Windows Form
    Private Sub funReset()
        Try
            fndUser_Name.MyReadOnly = False
            fndUser_Name.Value = ""
            fun_gridfill()
            rbtnDelete.Enabled = False
            TxtUserName.Text = ""
            rbtnSave.Text = "Save"
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'This is Update Function Used To Update Records In TSPL_USER_GROUP_MAPPING
    Private Sub fun_Update()
        
        Dim trans As SqlTransaction = Nothing
        Try
            trans = clsDBFuncationality.GetTransactin()

            Dim strUser_Code As String = ""
            strUser_Code = "delete from TSPL_CUSTOMER_GROUP_MAPPING_DETAIL where User_Code='" + fndUser_Name.Value + "'"
            connectSql.RunSqlTransaction(trans, strUser_Code)
            strUser_Code = "delete from TSPL_CUSTOMER_GROUP_MAPPING where User_Code='" + fndUser_Name.Value + "'"
            connectSql.RunSqlTransaction(trans, strUser_Code)


            For i As Integer = 0 To dgv_Groupmapping.Rows.Count - 1
                If CBool(dgv_Groupmapping.Rows(i).Cells(2).Value = True) Then
                    connectSql.RunSpTransaction(trans, "SP_TSPL_CUSTOMER_GROUP_MAPPING", New SqlParameter("@User_Code", fndUser_Name.Value), New SqlParameter("@Cust_Group_Code", dgv_Groupmapping.Rows(i).Cells("GroupCode").Value), New SqlParameter("@created_by", userCode), New SqlParameter("@created_date", connectSql.serverDate(trans)), New SqlParameter("Modify_By", userCode), New SqlParameter("Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", companyCode))

                    Dim arr As Dictionary(Of String, Object) = dgv_Groupmapping.Rows(i).Tag
                    If arr IsNot Nothing AndAlso arr.Count > 0 Then

                        For Each strInn As String In arr.Keys
                            connectSql.RunSpTransaction(trans, "SP_TSPL_CUSTOMER_GROUP_MAPPING_DETAIL", New SqlParameter("@User_Code", fndUser_Name.Value), New SqlParameter("@Cust_Group_Code", dgv_Groupmapping.Rows(i).Cells("GroupCode").Value), New SqlParameter("@Cust_Code", strInn))
                        Next

                    End If

                End If
            Next



            myMessages.update()
            trans.Commit()
        Catch ex As Exception
            myMessages.myExceptions(ex)
            trans.Rollback()
        End Try
    End Sub
    'It Validate User To Press The Keys 
    Private Sub fndUser_Name_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        'If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
        '    e.Handled = True
        '    fndUser_Name.Value.CharacterCasing = CharacterCasing.Upper
        'End If
    End Sub
    'It Is Used To Check The Value Of Finder(fndUser_Name),Is Present In Database Or Not
    Private Sub fndUser_Name_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If fndUser_Name.Value = "" Then
        'Else
        '    Dim strUser_Code As String = "select User_Code,User_Name from TSPL_USER_MASTER where User_Code='" + fndUser_Name.Value + "'"
        '    Dim dr As SqlDataReader
        '    dr = connectSql.RunSqlReturnDR(strUser_Code)
        '    Dim strvalue As String
        '    If dr.Read() Then
        '        strvalue = dr(0).ToString()
        '    End If
        '    If strvalue <> "" Then
        '    Else : strUser_Code = ""
        '        common.clsCommon.MyMessageBoxShow("User Code does not exist Master Table")
        '        fndUser_Name.Value = ""
        '        TxtUserName.Text = ""
        '        fndUser_Name.Focus()
        '    End If
        'End If
    End Sub
    Sub fndUser_NameLeave()
        If fndUser_Name.Value = "" Then
        Else
            Dim strUser_Code As String = "select User_Code,User_Name from TSPL_USER_MASTER where User_Code='" + fndUser_Name.Value + "' and isnull(Department_Head,0)=1"
            Dim strvalue As String
            strvalue = clsDBFuncationality.getSingleValue(strUser_Code)

            'If dr.Read() Then
            '    strvalue = dr(0).ToString()
            'End If
            If strvalue <> "" Then
            Else : strUser_Code = ""
                common.clsCommon.MyMessageBoxShow(Me, "User Code does not exist/Mark as Dept Head in Master", Me.Text)
                fndUser_Name.Value = ""
                TxtUserName.Text = ""
                fndUser_Name.Focus()
            End If
        End If
    End Sub
    'It Is Used To Give The Authority To User,To Access This Form (User Group Mapping) Or Not.(It Is Bassed On Mapping)
    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        'If funCheckLoginStatus() = False Then Exit Function
    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "USER-GRP-MAP"
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


    Private Sub fndUser_Name__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndUser_Name._MYValidating
        Dim str As String = "select count(*) from TSPL_USER_MASTER where User_Code ='" + fndUser_Name.Value + "' and isnull(Department_Head,0)=1"
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 Then
            fndUser_Name.MyReadOnly = False
        Else
            fndUser_Name.MyReadOnly = True
        End If

        If fndUser_Name.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = " select User_Code as [UserCode],User_Name as [User Name] from TSPL_USER_MASTER "
            fndUser_Name.Value = clsCommon.ShowSelectForm("fmUser_Name", qry, "UserCode", "isnull(Department_Head,0)=1", fndUser_Name.Value, "", isButtonClicked)
            TxtUserName.Text = clsDBFuncationality.getSingleValue("select User_Name from TSPL_USER_MASTER where User_Code= '" + fndUser_Name.Value + "' and isnull(Department_Head,0)=1")
            fun_gridfill()
            textchangedsub()
            fndUser_NameLeave()

        End If
    End Sub

    Private Sub fndUser_Name__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavigatorType As common.NavigatorType) Handles fndUser_Name._MYNavigator
        Dim qst As String = "select User_Code as [User Code],User_Name as [User Name] from TSPL_USER_MASTER   where  2=2 and isnull(Department_Head,0)=1 "
        Select Case NavigatorType
            Case NavigatorType.Current
                '  qst += "and assign_to='" + txtassign.Value + "' "
                ' qst += "and job_code in ('" + txtcode1.Value + "')"
            Case NavigatorType.Next
                qst += "and User_Code in (select min(User_Code) from TSPL_USER_MASTER where User_Code>'" + fndUser_Name.Value + "' and isnull(Department_Head,0)=1 ) "

                'qst += "and job_code in (select min(job_code) from job_master where job_code>'" + txtcode1.Value + "' and assign_to='" + txtassign.Value + "') "
            Case NavigatorType.First
                qst += "and User_Code in (select MIN(User_Code) from TSPL_USER_MASTER where isnull(Department_Head,0)=1)"

            Case NavigatorType.Last
                qst += "and User_Code in (select Max(User_Code) from TSPL_USER_MASTER where isnull(Department_Head,0)=1 )"
            Case NavigatorType.Previous
                qst += "and User_Code in (select max(User_Code) from TSPL_USER_MASTER where User_Code<'" + fndUser_Name.Value + "'  )"
        End Select
        ' fun_gridfill()
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            fndUser_Name.Value = clsCommon.myCstr(dt.Rows(0)("User Code"))
            TxtUserName.Text = clsCommon.myCstr(dt.Rows(0)("User Name"))
        End If
        fun_gridfill()
        textchangedsub()
        fndUser_NameLeave()

    End Sub

    Private Sub dgv_Groupmapping_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles dgv_Groupmapping.CellDoubleClick
        If clsCommon.myCBool(dgv_Groupmapping.CurrentRow.Cells("Status").Value) Then
            Dim frm As New frmCustomerSelect()
            'frm.lvl = If(Form_ID = clsUserMgtCode.stockRecoNewJR, 4, 3)

            Dim sql1 As String = "select COUNT(*) from TSPL_CUSTOMER_GROUP_MAPPING_DETAIL  where User_Code='" + fndUser_Name.Value + "' and Cust_Group_Code='" + clsCommon.myCstr(dgv_Groupmapping.CurrentRow.Cells("GroupCode").Value) + "'"
            Dim i As Integer = CInt(connectSql.RunScalar(sql1))
            If (i > 0) And rbtnSave.Text = "Update" Then
                frm.strusercode = fndUser_Name.Value
            Else
                frm.strusercode = ""
            End If

            'If rbtnSave.Text = "Update" Then
            '    frm.strusercode = fndUser_Name.Value
            'Else
            '    frm.strusercode = ""
            'End If
            frm.strCode = clsCommon.myCstr(dgv_Groupmapping.CurrentRow.Cells("GroupCode").Value)
            frm.arrIn = dgv_Groupmapping.CurrentRow.Tag
            frm.ShowDialog()
            If Not frm.isCancel Then
                dgv_Groupmapping.CurrentRow.Tag = frm.arrIn
            End If
        End If
    End Sub

    Private Sub dgv_Groupmapping_CellMouseMove(sender As Object, e As MouseEventArgs) Handles dgv_Groupmapping.CellMouseMove

    End Sub

    Private Sub dgv_Groupmapping_ValueChanged(sender As Object, e As EventArgs) Handles dgv_Groupmapping.ValueChanged
        If Not IsInsideLoadData Then
            If dgv_Groupmapping.CurrentColumn Is dgv_Groupmapping.Columns(2) Then
                'Dim strVendorCode As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHVendorCode).Value)
                'Dim strVendorName As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHVendorName).Value)
                'If clsCommon.myLen(CustomerCode) <= 0 Then
                '    CustomerCode = strVendorCode
                '    CustomerName = strVendorName
                'End If

                'If CBool(dgv_Groupmapping.CurrentRow.Cells(2).Value = True) Then
                '    If dgv_Groupmapping.CurrentRow.Tag Is Nothing Then

                Dim arr As Dictionary(Of String, Object) = Nothing
                arr = New Dictionary(Of String, Object)
                Dim dtcustomer As DataTable

                Dim sql1 As String = "select COUNT(*) from TSPL_CUSTOMER_GROUP_MAPPING_DETAIL  where User_Code='" + fndUser_Name.Value + "' and Cust_Group_Code='" + clsCommon.myCstr(dgv_Groupmapping.CurrentRow.Cells("GroupCode").Value) + "'"
                Dim i As Integer = CInt(connectSql.RunScalar(sql1))
                If (i > 0) Then
                    dtcustomer = clsDBFuncationality.GetDataTable("select Cust_Code from TSPL_CUSTOMER_GROUP_MAPPING_DETAIL  where User_Code='" + fndUser_Name.Value + "' and Cust_Group_Code='" + clsCommon.myCstr(dgv_Groupmapping.CurrentRow.Cells("GroupCode").Value) + "'")
                Else
                    dtcustomer = clsDBFuncationality.GetDataTable("select Cust_Code from TSPL_CUSTOMER_MASTER  where Cust_Group_Code='" + clsCommon.myCstr(dgv_Groupmapping.CurrentRow.Cells("GroupCode").Value) + "'")
                End If



                For k As Integer = 0 To dtcustomer.Rows.Count - 1
                    arr.Add(clsCommon.myCstr(dtcustomer.Rows(k).Item(0)), Nothing)
                Next

                dgv_Groupmapping.CurrentRow.Tag = arr

                '    End If
                'End If

                'If clsCommon.CompairString(strVendorCode, CustomerCode) = CompairStringResult.Equal Then
                '    Dim strCode As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHCode).Value)
                '    If clsCommon.myLen(strCode) > 0 Then
                '        LoadDetailData(e.NewValue, strCode)
                '    End If
                'Else
                '    common.clsCommon.MyMessageBoxShow("Document Vendor should be `" + CustomerName)
                '    e.Cancel = True
                'End If
            End If
        End If
    End Sub
End Class
