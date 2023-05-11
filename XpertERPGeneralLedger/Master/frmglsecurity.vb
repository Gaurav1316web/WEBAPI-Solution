''Created By : Suraj Kumar
''Created Date :15/07/2011
'' Finder and GridView DropDownList Changed By Abhishek As on 26/3/2012
''-25/08/2012--Updation By--[pankaj Kumar]--Addded Import/Export    Fwrd By--Amit Sir
''changes by richa agarwal againt ticket no (BM00000005628)
'================update by preeti gupta against ticket no [BM00000008971]
Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports common

Public Class Frmglsecurity
    Inherits FrmMainTranScreen
    Dim userCode, companyCode As String
    Const GlSeg As String = "GlSeg"
    Const SegCode As String = "SegCode"
    Const DefaultSegment As String = "DefaultSegment"
    Const AcctCode As String = "AcctCode"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isCellValueChangedOpen As Boolean = False

    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub
    Dim increment As Integer
    Dim ds As New DataSet()
    Dim ds1 As New DataSet()
    Dim dt As New DataTable()
    Dim dr As SqlDataReader
    Private isInsideLoadData As Boolean = False
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.glsecurity)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 02/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnsave.Visible = True Then
            RMIImportAccount.Enabled = True
            RMIImportSegment.Enabled = True
            ExportFormatAccount.Enabled = True
            ExportSegmentFormat.Enabled = True
        Else
            RMIImportAccount.Enabled = False
            RMIImportSegment.Enabled = False
            ExportFormatAccount.Enabled = False
            ExportSegmentFormat.Enabled = False
        End If
        '--------------------------------------------------
        'btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub Frmglsecurity_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadBlankGrid()
        SetUserMgmtNew()
        RadPageView1.SelectedPage = RadPageViewPage1
        'Dim strsegment As String = "select Seg_No,Seg_Name  from TSPL_GL_SEGMENT "
        'Dim segmentcmb As GridViewTextBoxColumn = TryCast(dgvsegment.Columns(0), GridViewTextBoxColumn)
        'ds = connectSql.RunSQLReturnDS(strsegment)
        ''segmentcmb.DataSource = ds.Tables(0)
        ''segmentcmb.DisplayMember = "Seg_Name"
        ''segmentcmb.ValueMember = "Seg_No"
        'Dim straccount As String = "select Account_Code as [Account Code], Description  from TSPL_GL_ACCOUNTS "
        'Dim acctcmb As GridViewTextBoxColumn = TryCast(dgvaccount.Columns(0), GridViewTextBoxColumn)
        'ds = connectSql.RunSQLReturnDS(straccount)
        'acctcmb.DataSource = ds.Tables(0)
        ' '' Dim descriptor As New FilterDescriptor("Description", FilterOperator.StartsWith, String.Empty)
        'Dim chk As Boolean = acctcmb.FilterDescriptor.Value
        'If Not String.IsNullOrEmpty(acctcmb.FilterDescriptor.Value) Then

        '    acctcmb.FilterDescriptor.Value = "Description"

        'End If

        'acctcmb.DropDownStyle = RadDropDownStyle.DropDown

        'AddHandler fndusercode.txtValue.TextChanged, AddressOf userCodechanged
        btndelete.Enabled = False
        dgvsegment.AllowAddNewRow = True
        dgvsegment.AllowDeleteRow = False
        dgvsegment.AllowEditRow = True
        dgvaccount.AllowAddNewRow = True
        fndUserCode.MyReadOnly = False
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnreset, "Press Alt+N Adding New Trasnaction")
        dgvsegment.Columns("DefaultSegment").ReadOnly = True
    End Sub
    ''To Retrieve the user name according to the user code
    'Private Sub userCodechanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    dgvsegment.Rows.Clear()
    '    If Not String.IsNullOrEmpty(connectSql.RunScalar("select User_Name as [User Name] from TSPL_USER_MASTER where User_Code = '" + fndusercode.txtValue.Text + "' ")) Then
    '        txtname.Text = connectSql.RunScalar("select User_Name as [User Name] from TSPL_USER_MASTER where User_Code = '" + fndusercode.txtValue.Text + "' ")
    '    End If
    '    Dim checkacct As String
    '    If Not String.IsNullOrEmpty(connectSql.RunScalar("select Account_Code  from tspl_gl_account_permission where User_Code ='" + fndusercode.txtValue.Text + "'")) Then
    '        checkacct = connectSql.RunScalar("select Account_Code  from tspl_gl_account_permission where User_Code ='" + fndusercode.txtValue.Text + "'")
    '    End If
    '    If String.IsNullOrEmpty(checkacct) Then
    '        btnsave.Text = "Save"
    '        btndelete.Enabled = False
    '    Else
    '        funfill()
    '        btnsave.Text = "Update"
    '        btndelete.Enabled = True
    '    End If
    '    Dim checksegment As String
    '    If Not String.IsNullOrEmpty(connectSql.RunScalar("select GL_Segment  from TSPL_GL_SEGMENT_PERMISSION where User_Code ='" + fndusercode.txtValue.Text + "'")) Then
    '        checksegment = connectSql.RunScalar("select GL_Segment  from TSPL_GL_SEGMENT_PERMISSION where User_Code ='" + fndusercode.txtValue.Text + "'")
    '    End If
    '    If String.IsNullOrEmpty(checksegment) Then
    '        btnsave.Text = "Save"
    '        btndelete.Enabled = False
    '    Else
    '        funfill()
    '        btnsave.Text = "Update"
    '        btndelete.Enabled = True
    '    End If

    'End Sub
    ''To Close The gl security form
    Private Sub RadButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        increment = 0
        Me.Close()
    End Sub
    ''To insert the data on TSPL_GL_SEGMENT_PERMISSION & TSPL_GL_ACCOUNT_PERMISSION
    Private Sub funinsert()
        Dim trans As SqlTransaction = Nothing
        Try
            'connectSql.OpenConnection()
            trans = clsDBFuncationality.GetTransactin() '' added by abhishek as on 12/10/2012
            If fndUserCode.Value <> "" Then
                Dim rowcount As Integer = dgvsegment.Rows.Count
                Dim rowcount1 As Integer = dgvaccount.Rows.Count
                Dim strread As Char
                If rowcount > 0 Then
                    For i As Integer = 0 To dgvsegment.Rows.Count - 1

                        If CBool(dgvsegment.Rows(i).Cells(2).Value) = True Then
                            strread = "Y"
                            ''richa agarwal 10/07/2015
                            ' Dim strDefaultlocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Default_Location,'')  from TSPL_USER_MASTER where User_Code ='" & fndUserCode.Value & "'", trans))
                            Dim strDefaultlocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select isnull(Loc_Segment_Code,'') from TSPL_LOCATION_MASTER where Location_Code =( Select isnull(Default_Location,'')  from TSPL_USER_MASTER where User_Code ='" & fndUserCode.Value & "' )", trans))
                            If clsCommon.CompairString(clsCommon.myCstr(dgvsegment.Rows(i).Cells(1).Value), strDefaultlocation) <> CompairStringResult.Equal Then
                                Throw New Exception("Default location of user cannot be changed, if you want to change default location then change it from user master")
                            End If
                            ''--------------------
                        Else
                            strread = "N"

                        End If
                        '>> Added By abhishek as on 26/3/2012
                        Dim SegCodeQry As String = "select Seg_No   from TSPL_GL_SEGMENT_CODE where Segment_name ='" + dgvsegment.Rows(i).Cells(0).Value + "' "
                        Dim Gl_Segment As String = clsDBFuncationality.getSingleValue(SegCodeQry, trans)
                        If String.IsNullOrEmpty(Gl_Segment) Then
                            Throw New Exception("Please Fill the Gl Segment")
                            ' Exit Sub
                        ElseIf String.IsNullOrEmpty(dgvsegment.Rows(i).Cells(1).Value) Then
                            Throw New Exception("Please Fill the Gl Segment Code")
                            ' Exit Sub
                        Else
                            connectSql.RunSpTransaction(trans, "SP_TSPL_GL_SEGMENT_PERMISSION_INSERT", New SqlParameter("@usercode", fndUserCode.Value), New SqlParameter("@glsegment", Gl_Segment), New SqlParameter("@segmentcode", dgvsegment.Rows(i).Cells(1).Value), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate(trans)), New SqlParameter("@modifydate", connectSql.serverDate(trans)), New SqlParameter("@modifyby", userCode), New SqlParameter("@compcode", companyCode), New SqlParameter("@Default_Segment", strread))
                        End If
                    Next
                Else
                End If
                If rowcount1 > 0 Then
                    For i As Integer = 0 To dgvaccount.Rows.Count - 1
                        connectSql.RunSpTransaction(trans, "SP_TSPL_GL_ACCOUNT_PERMISSION_INSERT", New SqlParameter("usercode", fndUserCode.Value), New SqlParameter("@acctcode", dgvaccount.Rows(i).Cells(0).Value), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate(trans)), New SqlParameter("@modifydate", connectSql.serverDate(trans)), New SqlParameter("@modifyby", userCode), New SqlParameter("@compcode", companyCode))
                    Next
                Else
                End If
                trans.Commit()
                myMessages.insert()
                btnsave.Text = "Update"
                btndelete.Enabled = True
            Else
                myMessages.blankValue("User Code")
                'common.clsCommon.MyMessageBoxShow(" User Code can not left blank ")
                fndUserCode.Focus()
            End If
        Catch ex As Exception
            trans.Rollback()
            myMessages.myExceptions(ex)
        End Try
    End Sub
    ''To update the data on TSPL_GL_SEGMENT_PERMISSION & TSPL_GL_ACCOUNT_PERMISSION
    Private Sub funupdate()
        Dim trans As SqlTransaction = Nothing
        Try
            'connectSql.OpenConnection()
            trans = clsDBFuncationality.GetTransactin()
            Dim rowcount As Integer = dgvsegment.Rows.Count
            Dim rowcount1 As Integer = dgvaccount.Rows.Count
            Dim strread As Char
            connectSql.RunSpTransaction(trans, "SP_TSPL_GL_SEGMENT_PERMISSION_DELETE", New SqlParameter("@usercode", fndUserCode.Value))
            connectSql.RunSpTransaction(trans, "SP_TSPL_GL_ACCOUNT_PERMISSION_DELETE", New SqlParameter("@usercode", fndUserCode.Value))
            If rowcount > 0 Then
                For i As Integer = 0 To dgvsegment.Rows.Count - 1
                    If CBool(dgvsegment.Rows(i).Cells(2).Value) = True Then
                        strread = "Y"
                        ''richa agarwal 10/07/2015
                        'Dim strDefaultlocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Default_Location,'')  from TSPL_USER_MASTER where User_Code ='" & fndUserCode.Value & "'", trans))
                        Dim strDefaultlocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select isnull(Loc_Segment_Code,'') from TSPL_LOCATION_MASTER where Location_Code =( Select isnull(Default_Location,'')  from TSPL_USER_MASTER where User_Code ='" & fndUserCode.Value & "' )", trans))
                        If clsCommon.CompairString(clsCommon.myCstr(dgvsegment.Rows(i).Cells(1).Value), strDefaultlocation) <> CompairStringResult.Equal Then
                            Throw New Exception("Default location of user cannot be changed, if you want to change default location then change it from user master")
                        End If
                        ''--------------------

                    Else
                        strread = "N"

                    End If
                    If clsCommon.myLen(dgvsegment.Rows(i).Cells(0).Value) > 0 AndAlso clsCommon.myLen(dgvsegment.Rows(i).Cells(1).Value) > 0 Then
                        Dim SegCodeQry As String = "select Seg_No   from TSPL_GL_SEGMENT_CODE where Segment_name ='" + dgvsegment.Rows(i).Cells(0).Value + "' "
                        Dim Gl_Segment As String = clsDBFuncationality.getSingleValue(SegCodeQry, trans)
                        If String.IsNullOrEmpty(Gl_Segment) Then
                            Throw New Exception("Please Fill the Gl Segment")
                            '  Exit Sub
                        ElseIf String.IsNullOrEmpty(dgvsegment.Rows(i).Cells(1).Value) Then
                            Throw New Exception("Please Fill the Gl Segment Code")
                            'Exit Sub

                        Else
                            connectSql.RunSpTransaction(trans, "SP_TSPL_GL_SEGMENT_PERMISSION_INSERT", New SqlParameter("@usercode", fndUserCode.Value), New SqlParameter("@glsegment", Gl_Segment), New SqlParameter("@segmentcode", dgvsegment.Rows(i).Cells(1).Value), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate(trans)), New SqlParameter("@modifydate", connectSql.serverDate(trans)), New SqlParameter("@modifyby", userCode), New SqlParameter("@compcode", companyCode), New SqlParameter("@Default_Segment", strread))
                        End If
                    End If
                Next
            Else
            End If
            If rowcount1 > 0 Then
                For i As Integer = 0 To dgvaccount.Rows.Count - 1
                    connectSql.RunSpTransaction(trans, "SP_TSPL_GL_ACCOUNT_PERMISSION_INSERT", New SqlParameter("usercode", fndUserCode.Value), New SqlParameter("@acctcode", dgvaccount.Rows(i).Cells(0).Value), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate(trans)), New SqlParameter("@modifydate", connectSql.serverDate(trans)), New SqlParameter("@modifyby", userCode), New SqlParameter("@compcode", companyCode))
                Next
            Else
            End If
            trans.Commit()
            myMessages.update()
        Catch ex As Exception
            trans.Rollback()
            myMessages.myExceptions(ex)
        End Try
    End Sub
    ''To delete the data of TSPL_GL_SEGMENT_PERMISSION & TSPL_GL_ACCOUNT_PERMISSION
    Private Sub fundelete()
        Dim trans As SqlTransaction = Nothing
        Try
            'connectSql.OpenConnection()
            trans = clsDBFuncationality.GetTransactin()
            connectSql.RunSpTransaction(trans, "SP_TSPL_GL_SEGMENT_PERMISSION_DELETE", New SqlParameter("@usercode", fndUserCode.Value))
            connectSql.RunSpTransaction(trans, "SP_TSPL_GL_ACCOUNT_PERMISSION_DELETE", New SqlParameter("@usercode", fndUserCode.Value))
            trans.Commit()
            myMessages.delete()
        Catch ex As Exception

            myMessages.myExceptions(ex)
            trans.Rollback()

        End Try
    End Sub
    ''To reset all the data on the screen
    Private Sub funreset()
        fndUserCode.Value = ""
        dgvaccount.DataSource = Nothing
        dgvaccount.Rows.Clear()
        dgvsegment.DataSource = Nothing
        dgvsegment.Rows.Clear()
        txtname.Text = ""
        btnsave.Text = "Save"
        btndelete.Enabled = False
        fndUserCode.MyReadOnly = False
        isInsideLoadData = True
        fndUserCode.MyReadOnly = False
    End Sub
    ''To bind the segment code according to the segment name 
    ''Private Sub dgvsegment_EditorRequired(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.EditorRequiredEventArgs) Handles dgvsegment.EditorRequired
    ''    Dim strsegmentcode As String = "select Segment_code  from TSPL_GL_SEGMENT_CODE  where Seg_No ='" + common.clsCommon.myCstr(dgvsegment.CurrentRow.Cells(0).Value) + "'"
    ''    ds = connectSql.RunSQLReturnDS(strsegmentcode)
    ''    Dim dtsegmentcode As GridViewComboBoxColumn = TryCast(dgvsegment.Columns(1), GridViewComboBoxColumn)
    ''    dtsegmentcode.DataSource = ds.Tables(0)
    ''    dtsegmentcode.ValueMember = "Segment_code"
    ''    '    dtsegmentcode.DisplayMember = "Segment_code"
    ''End Sub
    'Private Sub fndusercode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fndusercode.ConnectionString = connectSql.SqlCon()
    '    fndusercode.Query = "select User_Code as [User Code], User_Name as [User Name] from TSPL_USER_MASTER "
    '    fndusercode.ValueToSelect = "User Code"
    '    fndusercode.Caption = "User Details"
    '    fndusercode.ValueToSelect1 = "User Name"
    'End Sub
    ''To call the funreset function 
    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        funreset()
    End Sub
    ''To call the insert and update function
    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub
    Sub SaveData()
        Dim countsegment As Integer = dgvsegment.Rows.Count
        Dim countaccount As Integer = dgvaccount.Rows.Count
        Dim no As Integer = 0
        For i As Integer = 0 To dgvsegment.Rows.Count - 1
            If dgvsegment.Rows(i).Cells(2).Value = True Then
                no = no + 1

            End If
        Next

        If countaccount = 0 And countsegment = 0 Then
            common.clsCommon.MyMessageBoxShow("Please Select at least one account or location")
        ElseIf no > 1 Then
            common.clsCommon.MyMessageBoxShow("You can not provide two default segment in one time")
        Else
            If MyBase.isModifyonPasswordFlag Then
                If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.glsecurity, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
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

        If clsCommon.CompairString(objCommonVar.CurrentUserCode, fndUserCode.Value) = CompairStringResult.Equal Then
            'frmlogIn.RefeshUserApplicableLocationsAndGLAccount()
            RefeshLocationsAndGLAccount.RefeshUserApplicableLocationsAndGLAccount()
        End If

    End Sub
    ''To call the delete function 
    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If fndUserCode.Value <> "" Then
            If myMessages.deleteConfirm() Then
                fundelete()
                funreset()
            End If
        Else
            common.clsCommon.MyMessageBoxShow("Please select the User Code")
            fndUserCode.Focus()
        End If
    End Sub
    ''To load data on the table 
    Private Sub funloaddata()
        Try
            Dim da As New SqlDataAdapter("select User_Code  from TSPL_USER_MASTER ", connectSql.SqlCon())
            da.Fill(ds1, "TSPL_USER_MASTER")
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
    End Sub
    ''to show data on the finderaccountcode
    Private Sub funshowdata()
        Try
            funloaddata()
            fndUserCode.Value = ds1.Tables(0).Rows(increment)(0).ToString()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
    End Sub
    '>> Added By abhishek as on 26/3/2012
        Private Sub fndUserCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndUserCode._MYValidating
        Dim str As String = "select count(*) from TSPL_GL_SEGMENT_PERMISSION   where  User_Code ='" + fndUserCode.Value + "' "
        '"select User_Code, GL_Segment , Segment_Code,Default_Segment  from TSPL_GL_SEGMENT_PERMISSION 
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))

        If no = 0 AndAlso fndUserCode.Value <> "" Then
            fndUserCode.MyReadOnly = False
            common.clsCommon.MyMessageBoxShow(" This User Id does not exist")
            fndUserCode.Focus()
            Exit Sub
        Else
            fndUserCode.MyReadOnly = True
        End If
        '  fndUserCode.MyReadOnly = True
        isInsideLoadData = True
        If fndUserCode.MyReadOnly OrElse isButtonClicked Then
            'Dim qry As String = "select User_Code as Code, User_Name  from TSPL_USER_MASTER  "
            'fndUserCode.Value = clsCommon.ShowSelectForm("FmUserCode", qry, "Code", "", fndUserCode.Value, "Code", isButtonClicked)
            fndUserCode.Value = clsUserMaster.getFinder("", fndUserCode.Value, isButtonClicked)
            ''richa agarwal 13/02/2015
            If clsCommon.myLen(fndUserCode.Value) > 0 Then
                txtname.Text = connectSql.RunScalar("select User_Name as [User Name] from TSPL_USER_MASTER where User_Code = '" + fndUserCode.Value + "' ")
            End If
            ''----------------------
            dgvsegment.Rows.Clear()
            Dim checkacct As String = ""
            If Not String.IsNullOrEmpty(connectSql.RunScalar("select Account_Code  from tspl_gl_account_permission where User_Code ='" + fndUserCode.Value + "'")) Then
                checkacct = connectSql.RunScalar("select Account_Code  from tspl_gl_account_permission where User_Code ='" + fndUserCode.Value + "'")
            End If
            If String.IsNullOrEmpty(checkacct) Then
                isInsideLoadData = False

                btnsave.Text = "Save"
                btndelete.Enabled = False
            Else
                If Not String.IsNullOrEmpty(connectSql.RunScalar("select User_Name as [User Name] from TSPL_USER_MASTER where User_Code = '" + fndUserCode.Value + "' ")) Then
                    txtname.Text = connectSql.RunScalar("select User_Name as [User Name] from TSPL_USER_MASTER where User_Code = '" + fndUserCode.Value + "' ")
                End If
                isInsideLoadData = True
                LoadData(fndUserCode.Value, NavigatorType.Current)
                btnsave.Text = "Update"
                btndelete.Enabled = True
            End If
            Dim checksegment As String = ""
            If Not String.IsNullOrEmpty(connectSql.RunScalar("select GL_Segment  from TSPL_GL_SEGMENT_PERMISSION where User_Code ='" + fndUserCode.Value + "'")) Then
                checksegment = connectSql.RunScalar("select GL_Segment  from TSPL_GL_SEGMENT_PERMISSION where User_Code ='" + fndUserCode.Value + "'")
            End If
            If String.IsNullOrEmpty(checksegment) Then
                isInsideLoadData = False
                btnsave.Text = "Save"
                btndelete.Enabled = False
            Else
                If Not String.IsNullOrEmpty(connectSql.RunScalar("select User_Name as [User Name] from TSPL_USER_MASTER where User_Code = '" + fndUserCode.Value + "' ")) Then
                    txtname.Text = connectSql.RunScalar("select User_Name as [User Name] from TSPL_USER_MASTER where User_Code = '" + fndUserCode.Value + "' ")
                End If
                isInsideLoadData = True
                LoadData(fndUserCode.Value, NavigatorType.Current)
                btnsave.Text = "Update"
                btndelete.Enabled = True
            End If

        End If
    End Sub

    Private Sub fndUserCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndUserCode._MYNavigator
       
        fndUserCode.MyReadOnly = True


        isInsideLoadData = True
        Try
            LoadData(fndUserCode.Value, NavType)
            btnsave.Text = "Update"
            btndelete.Enabled = True

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    'Private Sub fndUserCode_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fndUserCode.Leave
    '    'Dim qry As String = "Select * from TSPL_GL_SEGMENT_PERMISSION  Where User_Code='" + fndUserCode.Value + "'"
    '    'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '    'If dt.Rows.Count <= 0 Then
    '    '    fndUserCode.Value = ""
    '    '    dgvaccount.DataSource = Nothing
    '    '    dgvaccount.Rows.Clear()
    '    '    dgvsegment.DataSource = Nothing
    '    '    dgvsegment.Rows.Clear()
    '    '    txtname.Text = ""
    '    '    btndelete.Enabled = False
    '    '    btnsave.Text = "Save"
    '    'Else
    '    '    LoadData(fndUserCode.Value, NavigatorType.Current)
    '    'End If
    'End Sub
    ''To Fill segment and account according to the user code
    Public Sub LoadData(ByVal strUserCode As String, ByVal navType As common.NavigatorType)
        Try

            If isInsideLoadData = True Then
                Dim Qry As String = "select User_Code, GL_Segment , Segment_Code,Default_Segment  from TSPL_GL_SEGMENT_PERMISSION  where 1=1 "
                Select Case navType
                    Case NavigatorType.First
                        Qry += " and TSPL_GL_SEGMENT_PERMISSION .User_Code=(select MIN(User_Code) from TSPL_GL_SEGMENT_PERMISSION)"
                    Case NavigatorType.Last
                        Qry += " and TSPL_GL_SEGMENT_PERMISSION .User_Code=(select Max(User_Code) from TSPL_GL_SEGMENT_PERMISSION)"
                    Case NavigatorType.Next
                        Qry += " and TSPL_GL_SEGMENT_PERMISSION .User_Code=(select Min(User_Code) from TSPL_GL_SEGMENT_PERMISSION where User_Code > '" + strUserCode + "')"
                    Case NavigatorType.Previous
                        Qry += " and TSPL_GL_SEGMENT_PERMISSION .User_Code=(select Max(User_Code) from TSPL_GL_SEGMENT_PERMISSION where User_Code < '" + strUserCode + "')"
                    Case NavigatorType.Current
                        Qry += " and TSPL_GL_SEGMENT_PERMISSION .User_Code='" + strUserCode + "'"
                End Select
                dgvsegment.AutoGenerateColumns = False
                dgvsegment.Rows.Clear()
                Dim dt As DataTable
                dt = clsDBFuncationality.GetDataTable(Qry)

                If dt.Rows.Count <= 0 Then
                    Return
                End If
                fndUserCode.Value = clsCommon.myCstr(dt.Rows(0)("User_Code"))
                If Not String.IsNullOrEmpty(connectSql.RunScalar("select User_Name as [User Name] from TSPL_USER_MASTER where User_Code = '" + fndUserCode.Value + "' ")) Then
                    txtname.Text = connectSql.RunScalar("select User_Name as [User Name] from TSPL_USER_MASTER where User_Code = '" + fndUserCode.Value + "' ")
                Else
                    txtname.Text = ""
                End If
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    dgvsegment.Columns(2).ReadOnly = True
                    For Each dr As DataRow In dt.Rows
                        dgvsegment.Rows.AddNew()
                        '>> Added By abhishek as on 26/3/2012
                        Dim segCode As Integer = dr("GL_Segment")
                        Dim SegNameQry As String = "select Segment_name  from TSPL_GL_SEGMENT_CODE where Seg_No =" + segCode.ToString() + "  "
                        Dim SegName As String = clsDBFuncationality.getSingleValue(SegNameQry)
                        dgvsegment.Rows(dgvsegment.Rows.Count - 1).Cells(0).Value = SegName
                        dgvsegment.Rows(dgvsegment.Rows.Count - 1).Cells(1).Value = clsCommon.myCstr(dr("Segment_Code"))
                        dgvsegment.Rows(dgvsegment.Rows.Count - 1).Cells(2).Value = IIf(clsCommon.CompairString(clsCommon.myCstr(dr("Default_Segment")), "Y") = CompairStringResult.Equal, True, False)

                    Next
                End If


                ''transportSql.FillGridView(strsegment, dgvsegment)
                ''dgvsegment.Columns(0).FieldName = "GL_Segment"
                ''dgvsegment.Columns(1).FieldName = "Segment_Code"

                Dim stracct As String = "select Account_Code  from TSPL_GL_ACCOUNT_PERMISSION  where User_Code = '" + fndUserCode.Value + "'"
                dgvaccount.AutoGenerateColumns = False
                transportSql.FillGridView(stracct, dgvaccount)
                dgvaccount.Columns(0).FieldName = "Account_Code"
                isInsideLoadData = False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Private Sub dgvaccount_CellEditorInitialized(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles dgvaccount.CellEditorInitialized
        ''If TypeOf Me.dgvaccount.CurrentColumn Is GridViewMultiComboBoxColumn Then
        ''    Dim editor As RadMultiColumnComboBoxElement = DirectCast(Me.dgvaccount.ActiveEditor, RadMultiColumnComboBoxElement)
        ''    editor.AutoSizeDropDownToBestFit = True
        ''    editor.EditorControl.MasterTemplate.BestFitColumns()
        ''    editor.DropDownStyle = RadDropDownStyle.DropDown
        ''    editor.AutoFilter = True
        ''    If editor.EditorControl.MasterTemplate.FilterDescriptors.Count = 0 Then
        ''        Dim autoFilter As FilterDescriptor = New FilterDescriptor("Description", FilterOperator.StartsWith, "")
        ''        autoFilter.IsFilterEditor = True
        ''        editor.EditorControl.FilterDescriptors.Add(autoFilter)
        ''        'Dim autoFilter1 As FilterDescriptor = New FilterDescriptor(editor.EditorControl.MasterTemplate.Columns.Item(3).FieldName, FilterOperator.StartsWith, "")
        ''        'autoFilter1.IsFilterEditor = True
        ''        'editor.EditorControl.FilterDescriptors.Add(autoFilter1)


        ''    End If
        ''End If
    End Sub

    ''To Authorised the user 
    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        'If funCheckLoginStatus() = False Then Exit Function
    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "GL-SECURITY"
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

    '    End Try
    'End Function
    '>> Added By abhishek as on 26/3/2012
    Sub LoadBlankGrid()

        Dim Gl_Seg As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Gl_Seg = New GridViewTextBoxColumn()
        Gl_Seg.FormatString = ""
        Gl_Seg.HeaderText = "GL Segment"
        Gl_Seg.Name = GlSeg
        Gl_Seg.Width = 300
        Gl_Seg.HeaderImage = My.Resources.search4
        Gl_Seg.TextImageRelation = TextImageRelation.TextBeforeImage
        dgvsegment.MasterTemplate.Columns.Add(Gl_Seg)

        Dim Seg_Code As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Seg_Code = New GridViewTextBoxColumn()
        Seg_Code.FormatString = ""
        Seg_Code.HeaderText = "GL Segment Code"
        Seg_Code.Name = SegCode
        Seg_Code.Width = 300
        Seg_Code.ReadOnly = True

        Seg_Code.HeaderImage = My.Resources.search4
        Seg_Code.TextImageRelation = TextImageRelation.TextBeforeImage
        dgvsegment.MasterTemplate.Columns.Add(Seg_Code)

        Dim chkdefaultseg As GridViewCheckBoxColumn = New GridViewCheckBoxColumn
        chkdefaultseg = New GridViewCheckBoxColumn()
        chkdefaultseg.FormatString = ""
        chkdefaultseg.HeaderText = "defaultseg "
        chkdefaultseg.Name = DefaultSegment
        chkdefaultseg.Width = 75
        chkdefaultseg.ReadOnly = True
        'chkdefaultseg.HeaderImage = Global.ERP.My.Resources.Resources.search4
        'chkdefaultseg.TextImageRelation = TextImageRelation.TextBeforeImage
        dgvsegment.MasterTemplate.Columns.Add(chkdefaultseg)

        Dim ACCT As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ACCT = New GridViewTextBoxColumn()
        ACCT.FormatString = ""
        ACCT.HeaderText = "Account Code"
        ACCT.Name = AcctCode
        ACCT.Width = 300
        ACCT.HeaderImage = My.Resources.search4
        ACCT.TextImageRelation = TextImageRelation.TextBeforeImage
        dgvaccount.MasterTemplate.Columns.Add(ACCT)

        dgvsegment.EnableFiltering = True
        dgvsegment.EnableSorting = True
        ''''end
    End Sub
    '>> Added By abhishek as on 26/3/2012
    Private Sub frmCreateAccount_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F2 AndAlso dgvsegment.CurrentCell IsNot Nothing Then
            isCellValueChangedOpen = True
            If dgvsegment.CurrentColumn Is dgvsegment.Columns(GlSeg) Then
                'dgvcreateaccount.CurrentColumn = dgvcreateaccount.Columns(colTo)
                OpenFromList(True)
                dgvsegment.CurrentColumn = dgvsegment.Columns(SegCode)
            ElseIf dgvsegment.CurrentColumn Is dgvsegment.Columns(SegCode) Then
                dgvsegment.CurrentColumn = dgvsegment.Columns(GlSeg)
                OpenToList(True)
                dgvsegment.CurrentColumn = dgvsegment.Columns(SegCode)
            ElseIf dgvaccount.CurrentColumn Is dgvaccount.Columns(AcctCode) Then

            End If

        End If
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
            'ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            '    PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funreset()
        End If
    End Sub

  
    '>> Added By abhishek as on 26/3/2012
    Private Sub dgvsegment_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles dgvsegment.CellValueChanged

        Dim grow As GridViewRowInfo = TryCast(e.Row, GridViewRowInfo)
        Dim column As GridViewDataColumn = TryCast(e.Column, GridViewDataColumn)
        If isInsideLoadData = False Then
            If e.Column Is dgvsegment.Columns(GlSeg) Then
                OpenFromList(False)
            End If
            'If e.Column Is dgvsegment.Columns(SegCode) Then
            '    OpenToList(False)
            'End If
           
        End If
    End Sub
    ''added by richa agarwal
    Private Sub dgvsegment_CellDoubleClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles dgvsegment.CellDoubleClick
        If isInsideLoadData = False Then
            If e.Column Is dgvsegment.Columns(SegCode) Then
                OpenToListNew(False)
            End If
        End If
    End Sub
    Sub OpenToListNew(ByVal isButtonClick As Boolean)
        Dim strsegmentCode As String = String.Empty
        If dgvsegment.Rows.Count > 0 Then
            Dim strcurrentSegment As String = clsCommon.myCstr(dgvsegment.CurrentRow.Cells(0).Value)
            For i As Integer = 0 To dgvsegment.Rows.Count - 1
                If clsCommon.CompairString(clsCommon.myCstr(dgvsegment.Rows(i).Cells(0).Value), strcurrentSegment) = CompairStringResult.Equal Then
                    If clsCommon.myCstr(dgvsegment.Rows(i).Cells(1).Value) <> "" Then
                        strsegmentCode = strsegmentCode + "'" + clsCommon.myCstr(dgvsegment.Rows(i).Cells(1).Value) + "',"
                    End If
                End If
            Next
            If clsCommon.myLen(strsegmentCode) > 0 Then
                strsegmentCode = strsegmentCode.Substring(0, strsegmentCode.Length - 1)
            End If
        End If
        Dim frm As FrmSelectSegment = New FrmSelectSegment()
        frm.strSegment = clsCommon.myCstr(dgvsegment.CurrentRow.Cells(0).Value)
        frm.strSegmentCode = strsegmentCode
        frm.arr = TryCast(dgvsegment.CurrentRow.Tag, ArrayList)
        frm.ShowDialog()
        If Not frm.isCencelButtonClicked AndAlso frm.arr IsNot Nothing Then
            dgvsegment.CurrentRow.Tag = frm.arr
            Dim strTemp As String = ""
            For Each Str As String In frm.arr

                dgvsegment.CurrentRow.Cells(0).Value = frm.strSegment
                dgvsegment.CurrentRow.Cells(SegCode).Value = Str
                dgvsegment.Rows.AddNew()
            Next
            dgvsegment.Rows.RemoveAt(dgvsegment.Rows.Count - 1)
            'dgvsegment.CurrentRow.Cells(SegCode).Value = strTemp
        End If


        ''''end
    End Sub
    ''---------------------------------------
    '>> Added By abhishek as on 26/3/2012
    Private Sub dgvaccount_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles dgvaccount.CellValueChanged
        Dim grow As GridViewRowInfo = TryCast(e.Row, GridViewRowInfo)
        Dim column As GridViewDataColumn = TryCast(e.Column, GridViewDataColumn)
        If isInsideLoadData = False Then
            If e.Column Is dgvaccount.Columns(AcctCode) Then
                OpenAcctList(False)
            End If
        End If
    End Sub
    '>> Added By abhishek as on 26/3/2012
    Sub OpenFromList(ByVal isButtonClick As Boolean)
        Dim qry As String = "select Seg_Name as Code  from TSPL_GL_SEGMENT "
        dgvsegment.CurrentRow.Cells(0).Value = clsCommon.ShowSelectForm("CAGLOPEN", qry, "Code", "", clsCommon.myCstr(dgvsegment.CurrentRow.Cells(GlSeg).Value), "Code", isButtonClick)
        dgvsegment.CurrentRow.Cells(1).Value = ""
    End Sub
    '>> Added By abhishek as on 26/3/2012
    Sub OpenToList(ByVal isButtonClick As Boolean)
        Dim qry As String = "select Segment_code as Code ,Description  from TSPL_GL_SEGMENT_CODE  "
        dgvsegment.CurrentRow.Cells(SegCode).Value = clsCommon.ShowSelectForm("CAGLOPEN", qry, "Code", "Segment_name ='" + clsCommon.myCstr(dgvsegment.CurrentRow.Cells(0).Value) + "'", clsCommon.myCstr(dgvsegment.CurrentRow.Cells(SegCode).Value), "Code", isButtonClick)

       
        ''''end
    End Sub
    '>> Added By abhishek as on 26/3/2012
    Sub OpenAcctList(ByVal isButtonClick As Boolean)
        Dim qry As String = "select Account_Code as Code, Description  from TSPL_GL_ACCOUNTS "
        dgvaccount.CurrentRow.Cells(AcctCode).Value = clsCommon.ShowSelectForm("CAGLOPEN", qry, "Code", "", clsCommon.myCstr(dgvaccount.CurrentRow.Cells(AcctCode).Value), "Code", isButtonClick)
    End Sub



    'Private Sub dgvsegment_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles dgvsegment.CellValueChanged
    '    If e.Column.Name = "defaultseg" Then
    '        'For Each grow As GridViewRowInfo In dgvsegment.Rows
    '        '    RemoveHandler dgvsegment.CellValueChanged, AddressOf dgvsegment_CellValueChanged
    '        '    If grow.Index <> dgvsegment.CurrentRow.Index Then

    '        '        grow.Cells("defaultseg").Value = False
    '        '    End If
    '        'Next
    '        'AddHandler dgvsegment.CellValueChanged, AddressOf dgvsegment_CellValueChanged


    '        If dgvsegment.CurrentRow.Cells("defaultseg").Value = True Then
    '            For i As Integer = 0 To dgvsegment.Rows.Count - 1

    '                dgvsegment.Rows(i).Cells(2).Value = False


    '            Next
    '        End If


    '    End If
    'End Sub



    Private Sub ExportSegmentFormat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportSegmentFormat.Click
        Try
            'Dim strCmd As String = '" select '' as 'User Code', '' as 'GL Segment', '' as 'Segment Code' "
            Dim strCmd As String = "select TSPL_GL_SEGMENT_PERMISSION.User_Code as 'User Code' ,TSPL_GL_SEGMENT_PERMISSION.GL_Segment as 'GL Segment' ,Segment_Code as 'Segment Code' from TSPL_GL_SEGMENT_PERMISSION"
            ListImpExpColumnsMandatory = New List(Of String)({"User Code", "GL Segment", "Segment Code"})
            ListImpExpColumnsSuperMandatory = New List(Of String)({"User Code", "Segment Code"})
            transportSql.ExporttoExcel(strCmd, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID + "Segment")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "GL Security")
        End Try
    End Sub

    Private Sub ExportFormatAccount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportFormatAccount.Click
        Try
            'Dim strCmd As String = " select '' as 'User Code', '' as 'Account Code' "
            Dim strCmd As String = "select TSPL_GL_Account_PERMISSION.User_Code as 'User Code' ,TSPL_GL_Account_PERMISSION.Account_Code as 'Account Code' from TSPL_GL_Account_PERMISSION"
            ListImpExpColumnsMandatory = New List(Of String)({"User Code", "Account Code"})
            ListImpExpColumnsSuperMandatory = New List(Of String)({"User Code", "Account Code"})
            transportSql.ExporttoExcel(strCmd, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID + "Account")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "GL Security")
        End Try
    End Sub

    Private Sub RMIImportSegment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RMIImportSegment.Click
        Dim gv As New RadGridView()

        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today

        If transportSql.importExcel(gv, "User Code", "GL Segment", "Segment Code") Then
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try
                clsCommon.ProgressBarShow()

                For Each grow As GridViewRowInfo In gv.Rows
                    Dim LineNo As String = clsCommon.myCstr(grow.Index) + 2
                    Dim strUserCode As String = clsCommon.myCstr(grow.Cells("User Code").Value)
                    Dim coll As New Hashtable()
                    If clsCommon.myLen(strUserCode) > 0 Then
                        Dim User_Code As String = clsDBFuncationality.getSingleValue("Select User_Code  from TSPL_USER_MASTER where User_Code ='" + strUserCode + "'", trans)
                        If clsCommon.CompairString(User_Code, strUserCode) = CompairStringResult.Equal Then
                            clsCommon.AddColumnsForChange(coll, "User_Code", User_Code)
                        Else
                            Throw New Exception("The User '" + strUserCode + "' at line " + LineNo + " Does Not Exist In Employee Master")
                        End If

                        Dim strGlSegment As String = clsCommon.myCstr(clsCommon.myCstr(grow.Cells("GL Segment").Value))
                        If clsCommon.myLen(strGlSegment) > 0 Then
                            Dim Gl_Segmenet As String = clsDBFuncationality.getSingleValue("Select Seg_No  from TSPL_GL_SEGMENT_CODE Where Seg_No='" + strGlSegment + "'", trans)
                            If clsCommon.CompairString(Gl_Segmenet, strGlSegment) = CompairStringResult.Equal Then
                                clsCommon.AddColumnsForChange(coll, "GL_Segment", Gl_Segmenet)
                            Else
                                Throw New Exception("The GL Segment '" + strGlSegment + "' at line " + LineNo + " Does Not Exist")
                            End If
                        Else
                            Throw New Exception("Please Insert GL Segement Against User '" + User_Code + "' at line " + LineNo + " ")
                        End If

                        Dim strSegmentCode As String = clsCommon.myCstr(clsCommon.myCstr(grow.Cells("Segment Code").Value))
                        If clsCommon.myLen(strSegmentCode) > 0 Then
                            Dim Segment_Code As String = clsDBFuncationality.getSingleValue("Select Segment_code from TSPL_GL_SEGMENT_CODE Where Seg_No='" + strGlSegment + "' and Segment_code='" + strSegmentCode + "'", trans)
                            If clsCommon.CompairString(Segment_Code, strSegmentCode) = CompairStringResult.Equal Then
                                clsCommon.AddColumnsForChange(coll, "Segment_Code", Segment_Code)
                            Else
                                Throw New Exception("The Segement Code '" + strSegmentCode + "' Aganist GL Segment '" + strGlSegment + "' at line " + LineNo + " Does Not Exist")
                            End If
                        Else
                            Throw New Exception("Please Insert Segment Code Against GL Segment '" + strGlSegment + "' at line " + LineNo + " ")
                        End If

                        clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                        clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                        clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)

                        Dim i As Integer = CInt(clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_GL_SEGMENT_PERMISSION Where User_Code='" + User_Code + "' AND GL_Segment='" + strGlSegment + "' AND Segment_Code='" + strSegmentCode + "'", trans))

                        If (i = 0) Then
                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GL_SEGMENT_PERMISSION", OMInsertOrUpdate.Insert, "", trans)
                        End If
                    End If
                Next

                trans.Commit()
                clsCommon.ProgressBarHide()

                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)

            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()

                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub RMIImportAccount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RMIImportAccount.Click
        Dim gv As New RadGridView()

        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today

        If transportSql.importExcel(gv, "User Code", "Account Code") Then
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try
                clsCommon.ProgressBarShow()

                For Each grow As GridViewRowInfo In gv.Rows
                    Dim LineNo As String = clsCommon.myCstr(grow.Index) + 2
                    Dim strUserCode As String = clsCommon.myCstr(grow.Cells("User Code").Value)
                    Dim coll As New Hashtable()
                    If clsCommon.myLen(strUserCode) > 0 Then
                        Dim User_Code As String = clsDBFuncationality.getSingleValue("Select User_Code  from TSPL_USER_MASTER where User_Code ='" + strUserCode + "'", trans)
                        If clsCommon.CompairString(User_Code, strUserCode) = CompairStringResult.Equal Then
                            clsCommon.AddColumnsForChange(coll, "User_Code", User_Code)
                        Else
                            Throw New Exception("The User '" + strUserCode + "' at line " + LineNo + " Does Not Exist In Employee Master")
                        End If

                        Dim strGlAccount As String = clsCommon.myCstr(clsCommon.myCstr(grow.Cells("Account Code").Value))
                        If clsCommon.myLen(strGlAccount) > 0 Then
                            Dim Account_Code As String = clsDBFuncationality.getSingleValue("Select Account_Code  from TSPL_GL_ACCOUNTS Where Account_Code ='" + strGlAccount + "'", trans)
                            If clsCommon.CompairString(Account_Code, strGlAccount) = CompairStringResult.Equal Then
                                clsCommon.AddColumnsForChange(coll, "Account_Code", Account_Code)
                            Else
                                Throw New Exception("The Account Code '" + strGlAccount + "' at line " + LineNo + " Does Not Exist")
                            End If
                        Else
                            Throw New Exception("Please Account Code Against User '" + User_Code + "' at line " + LineNo + " ")
                        End If

                        clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                        clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                        clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)

                        Dim i As Integer = CInt(clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_GL_ACCOUNT_PERMISSION Where User_Code='" + User_Code + "' AND Account_Code ='" + strGlAccount + "' ", trans))

                        If (i = 0) Then
                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GL_Account_PERMISSION", OMInsertOrUpdate.Insert, "", trans)
                        End If
                    End If
                Next

                trans.Commit()
                clsCommon.ProgressBarHide()

                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)

            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()

                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub dgvaccount_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles dgvaccount.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Do you want to delete current row?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub dgvsegment_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles dgvsegment.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Do you want to delete current row?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub
End Class
