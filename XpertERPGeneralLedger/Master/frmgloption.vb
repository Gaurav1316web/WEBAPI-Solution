Imports System.Data.SqlClient
Imports System.Data
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports common
'' CREATED BY : SURAJ
''Start Date: 10-05-2011
'' End Date:10-05-2011
Public Class frmgloption
    Inherits FrmMainTranScreen
    Dim userCode, companyCode As String
    Dim dr As SqlDataReader
    Dim dt As DataTable
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.glOptions)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub frmgloption_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Control AndAlso e.Alt AndAlso e.KeyCode = Keys.D Then
            Dim pwd As New FrmPWD(Nothing)
            pwd.strCode = clsFixedParameterCode.AllowToSaveAndUpdatePasswordBased
            pwd.strType = clsFixedParameterType.AllowToSaveAndUpdatePasswordBased
            pwd.ShowDialog()
            If pwd.isPasswordCorrect Then
                GBUpdateCA.Visible = True
            Else
                GBUpdateCA.Visible = False
            End If
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
            'ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            '    PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            ' DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub
    Private Sub frmgloption_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        Dim dt As GridViewComboBoxColumn = TryCast(dgvsegment.Columns(3), GridViewComboBoxColumn)
        dt.DataSource = New String() {"Yes", "No"}
        'fnddefaultratetype.txtValue.MaxLength = 2
        'fndstructurecode.txtValue.MaxLength = 10
        'fnddefaultratetype.MaxLength = 2
        '' Anubhooti 20-Oct-2014 
        Dim dt1 As GridViewComboBoxColumn = TryCast(dgvsegment.Columns(4), GridViewComboBoxColumn)
        dt1.DataSource = New String() {"Yes", "No"}
        ''
        fndclosingaccount1.MyReadOnly = True
        fndsourcecode.MyReadOnly = True
        fndstructurecode.MyReadOnly = True
        binddata()
        LoadSourceCode()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        RadPageView2.SelectedPage = pvposting
        ddlaccountsegment.Text = "1"
        ddlaccountsegment.Enabled = False
    End Sub

    Private Sub LoadSourceCode()
        Dim qry As String = "select SourceCode,SourceDescription from TSPL_GL_SOURCECODE where SourceCode not in ('GL-JE')"
        cbgMergeGLAccount.ValueMember = "SourceCode"
        cbgMergeGLAccount.DisplayMember = "SourceDescription"
        cbgMergeGLAccount.DataSource = clsDBFuncationality.GetDataTable(qry)

        cbgMergeGLAccount.CheckedValue = clsGLSourceCode.GetMergeSourceCode()
    End Sub
    ''To Authorised the user 
    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        'If funCheckLoginStatus() = False Then Exit Function
    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "GL-OPTION"
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
    '            'rdbtndelete.Enabled = False
    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception

    '    End Try
    'End Function
    ''To close the form
    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    ''to call the insert function and bind the data
    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub
    Sub SaveData()
        If MyBase.isModifyonPasswordFlag Then
            If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.glOptions, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
            Else
                Return
            End If
        End If
        clsFixedParameter.UpdateData(clsFixedParameterType.DoubleClickOnVC, clsFixedParameterCode.DoubleClickOnVC, IIf(chkDoubleClickSystem.Checked, "1", "0"), Nothing)
        If btnsave.Text = "Save" Then
            funinsert()
            dgvsegment.DataSource = Nothing
            dgvsegment.Rows.Clear()
            binddata()
        Else : btnsave.Text = "Update"

            funupdate()
            dgvsegment.DataSource = Nothing
            dgvsegment.Rows.Clear()
            binddata()
        End If
    End Sub
    ''To insert the data into the table
    ''insert function 
    Private Sub funinsert()
        Dim strmulticurrency As String
        Dim strpostingprevious As String
        Dim strprovisionalposting As String
        Dim straccountgroup As String
        Dim strGLAccBySubAcc As Integer
        btnsave.Focus()
        If chkmulticurrency.Checked = True Then
            strmulticurrency = "Y"
        Else
            strmulticurrency = "N"
        End If
        If chkprovisionalposting.Checked = True Then
            strprovisionalposting = "Y"
        Else
            strprovisionalposting = "N"
        End If
        If chkpostingprevious.Checked = True Then
            strpostingprevious = "Y"
        Else
            strpostingprevious = "N"
        End If
        If chkaccountgroup.Checked = True Then
            straccountgroup = "Y"
        Else
            straccountgroup = "N"
        End If

        '' Anubhooti 12-Dec-2014 (Settings Of Auto Generated Code Of GL Main Account From Sub Account)
        If ChkGLAccBySubAcc.Checked = True Then
            strGLAccBySubAcc = 1
        Else
            strGLAccBySubAcc = 0
        End If
        ''
        Dim trans As SqlTransaction = Nothing
        Try
            Dim Qry As String
            Dim Is_Visible As Double = 0
            'connectSql.OpenConnection()
            trans = clsDBFuncationality.GetTransactin() '' added by abhishek as on 12/10/2012
            connectSql.RunSpTransaction(trans, "sp_TSPL_GLSETTING_insert", New SqlParameter("@funcurrency", txtfuncurrency.Text), New SqlParameter("@multicurrency", strmulticurrency), New SqlParameter("@ratetype", fnddefaultratetype.Value), New SqlParameter("@accountgroup", straccountgroup), New SqlParameter("@closing_account", fndclosingaccount1.Value), New SqlParameter("@postprevious", strpostingprevious), New SqlParameter("@provisional_posting", strprovisionalposting), New SqlParameter("@sourcecode", fndsourcecode.Value), New SqlParameter("@accountsegment", ddlaccountsegment.Text), New SqlParameter("@structurecode", fndstructurecode.Value), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate(trans)), New SqlParameter("@modifyby", userCode), New SqlParameter("@modifydate", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode))
            For i As Integer = 0 To dgvsegment.Rows.Count - 1
                If String.IsNullOrEmpty(dgvsegment.Rows(i).Cells(1).Value) Or String.IsNullOrEmpty(dgvsegment.Rows(i).Cells(2).Value) Or String.IsNullOrEmpty(dgvsegment.Rows(i).Cells(3).Value) Then
                Else
                    connectSql.RunSpTransaction(trans, "sp_TSPL_GL_SEGMENT_INSERT", New SqlParameter("@segno", dgvsegment.Rows(i).Cells(0).Value), New SqlParameter("@segname", dgvsegment.Rows(i).Cells(1).Value), New SqlParameter("@seglength", dgvsegment.Rows(i).Cells(2).Value), New SqlParameter("@seg_useinclosing", dgvsegment.Rows(i).Cells(3).Value), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate(trans)), New SqlParameter("@modifyby", userCode), New SqlParameter("@modifydate", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode))
                    '' Anubhooti 20-Oct-2014
                    If clsCommon.CompairString(clsCommon.myCstr(dgvsegment.Rows(i).Cells(4).Value), "Yes") = CompairStringResult.Equal Then
                        Is_Visible = 1
                    Else
                        Is_Visible = 0
                    End If
                    Qry = "Update TSPL_GL_SEGMENT set Report_Filters=" & clsCommon.myCstr(Is_Visible) & " where Seg_No='" & clsCommon.myCstr(dgvsegment.Rows(i).Cells(0).Value) & "'"
                    connectSql.RunSqlTransaction(trans, Qry)
                End If
            Next
            Dim ClrAcc As String = String.Empty
            If clsCommon.myLen(TxtClrAcc.Value) > 0 Then
                ClrAcc = "'" & TxtClrAcc.Value & "'"
            Else
                ClrAcc = "NULL"
            End If
            '' Anubhooti 12-Dec-2014 (GL Main Account Code Generation From Sub Account) (17-Mar-2015 Clearing_Account save which is used for VCGL)
            Qry = "UPDATE TSPL_GLSETTING SET AutoGenerated_GLCode_From_SubAccount=" & clsCommon.myCdbl(strGLAccBySubAcc) & ",Clearing_Account =" & ClrAcc & ""
            connectSql.RunSqlTransaction(trans, Qry)
            ''
            clsGLSourceCode.SaveMergeSourceCode(cbgMergeGLAccount.CheckedValue, trans)
            trans.Commit()
            myMessages.insert()
        Catch ex As Exception
            trans.Rollback()
            myMessages.myExceptions(ex)

        End Try
    End Sub
    ''To update the data of gl option 
    Private Sub funupdate()
        Dim strmulticurrency As String
        Dim strpostingprevious As String
        Dim strprovisionalposting As String
        Dim straccountgroup As String
        Dim strGLAccBySubAcc As Integer
        btnsave.Focus()
        If chkmulticurrency.Checked = True Then
            strmulticurrency = "Y"
        Else
            strmulticurrency = "N"
        End If
        If chkprovisionalposting.Checked = True Then
            strprovisionalposting = "Y"
        Else
            strprovisionalposting = "N"
        End If
        If chkpostingprevious.Checked = True Then
            strpostingprevious = "Y"
        Else
            strpostingprevious = "N"
        End If
        If chkaccountgroup.Checked = True Then
            straccountgroup = "Y"
        Else
            straccountgroup = "N"
        End If
        '' Anubhooti 12-Dec-2014 (Settings Of Auto Generated Code Of GL Main Account From Sub Account)
        If ChkGLAccBySubAcc.Checked = True Then
            strGLAccBySubAcc = 1
        Else
            strGLAccBySubAcc = 0
        End If
        ''
        Dim trans As SqlTransaction = Nothing
        Try
            Dim Qry As String
            Dim Is_Visible As Double = 0
            'connectSql.OpenConnection()
            trans = clsDBFuncationality.GetTransactin() '' Added by abhishek as on 12/10/2012
            connectSql.RunSpTransaction(trans, "sp_TSPL_GLSETTING_update", New SqlParameter("@funcurrency", txtfuncurrency.Text), New SqlParameter("@multicurrency", strmulticurrency), New SqlParameter("@ratetype", fnddefaultratetype.Value), New SqlParameter("@accountgroup", straccountgroup), New SqlParameter("@closing_account", fndclosingaccount1.Value), New SqlParameter("@postprevious", strpostingprevious), New SqlParameter("@provisional_posting", strprovisionalposting), New SqlParameter("@sourcecode", fndsourcecode.Value), New SqlParameter("@accountsegment", ddlaccountsegment.Text), New SqlParameter("@structurecode", fndstructurecode.Value), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate(trans)), New SqlParameter("@modifyby", userCode), New SqlParameter("@modifydate", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode))
            connectSql.RunSqlTransaction(trans, "delete from tspl_gl_segment")
            For i As Integer = 0 To dgvsegment.Rows.Count - 1
                If String.IsNullOrEmpty(dgvsegment.Rows(i).Cells(1).Value) Or String.IsNullOrEmpty(dgvsegment.Rows(i).Cells(2).Value) Or String.IsNullOrEmpty(dgvsegment.Rows(i).Cells(3).Value) Then
                Else
                    connectSql.RunSpTransaction(trans, "sp_TSPL_GL_SEGMENT_INSERT", New SqlParameter("@segno", dgvsegment.Rows(i).Cells(0).Value), New SqlParameter("@segname", dgvsegment.Rows(i).Cells(1).Value), New SqlParameter("@seglength", dgvsegment.Rows(i).Cells(2).Value), New SqlParameter("@seg_useinclosing", dgvsegment.Rows(i).Cells(3).Value), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate(trans)), New SqlParameter("@modifyby", userCode), New SqlParameter("@modifydate", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode))
                    If clsCommon.CompairString(clsCommon.myCstr(dgvsegment.Rows(i).Cells(4).Value), "Yes") = CompairStringResult.Equal Then
                        Is_Visible = 1
                    Else
                        Is_Visible = 0
                    End If
                    Qry = "Update TSPL_GL_SEGMENT set Report_Filters=" & clsCommon.myCstr(Is_Visible) & " where Seg_No='" & clsCommon.myCstr(dgvsegment.Rows(i).Cells(0).Value) & "'"
                    connectSql.RunSqlTransaction(trans, Qry)
                End If
            Next
            Dim ClrAcc As String = String.Empty
            If clsCommon.myLen(TxtClrAcc.Value) > 0 Then
                ClrAcc = "'" & TxtClrAcc.Value & "'"
            Else
                ClrAcc = "NULL"
            End If
            '' Anubhooti 12-Dec-2014 (GL Main Account Code Generation From Sub Account) (17-Mar-2015 Clearing_Account save which is used for VCGL)
            Qry = "UPDATE TSPL_GLSETTING SET AutoGenerated_GLCode_From_SubAccount=" & clsCommon.myCdbl(strGLAccBySubAcc) & " ,Clearing_Account =" & ClrAcc & ""
            connectSql.RunSqlTransaction(trans, Qry)

            clsGLSourceCode.SaveMergeSourceCode(cbgMergeGLAccount.CheckedValue, trans)
            ''
            trans.Commit()
            myMessages.update()
        Catch ex As Exception
            trans.Rollback()
            myMessages.myExceptions(ex)

        End Try
    End Sub

    Private Sub dgvsegment_UserAddedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles dgvsegment.UserAddedRow
        If dgvsegment.RowCount = 10 Then
            dgvsegment.AllowAddNewRow = False
        End If
        ' Dim j As Integer
        For i As Integer = 0 To dgvsegment.Rows.Count - 1
            dgvsegment.Rows(0).Cells(0).Value = 1
            If i <> 0 Then
                dgvsegment.Rows(i).Cells(0).Value = i + 1
            End If
        Next
        If ddlaccountsegment.Text = "" Then
            ddlaccountsegment.Items.Clear()
            For k As Integer = 0 To dgvsegment.Rows.Count - 1
                If Not String.IsNullOrEmpty(dgvsegment.Rows(k).Cells(0).Value.ToString()) Then
                    ddlaccountsegment.Items.Add(dgvsegment.Rows(k).Cells(0).Value.ToString())
                End If
            Next
        End If
        For l As Integer = 0 To dgvsegment.Rows.Count - 1
            If String.IsNullOrEmpty(dgvsegment.Rows(l).Cells(3).Value) Then
                dgvsegment.Rows(l).Cells(3).Value = "No"
            End If
        Next
    End Sub
    Private Sub Finder4_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndclosingaccount1.ConnectionString = connectSql.SqlCon()
        'fndclosingaccount1.Query = "select Account_Code as [Account Code], Description  from tspl_gl_accounts where Account_Code = 'Retained Earnings'"
        'fndclosingaccount1.ValueToSelect = "Account Code"
        'fndclosingaccount1.Caption = "Account Detail"
        'fndclosingaccount1.ValueToSelect1 = "Description"
    End Sub
    Private Sub fndsourcecode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndsourcecode.ConnectionString = connectSql.SqlCon()
        'fndsourcecode.Query = "select sourcecode as [Source Code], sourceledger as [Source Ledger] from TSPL_GL_SOURCECODE "
        'fndsourcecode.ValueToSelect = "Source Code"
        'fndsourcecode.ValueToSelect1 = "Source Ledger"
        'fndsourcecode.Caption = "Source Code Detail"
    End Sub
    Private Sub fndclosingaccount_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndclosingaccount1.ConnectionString = connectSql.SqlCon()
        'fndclosingaccount1.Query = "select Account_Code as [Account Code], Description  from tspl_gl_accounts"
        'fndclosingaccount1.ValueToSelect = "Account Code"
        'fndclosingaccount1.Caption = "Account Detail"
        'fndclosingaccount1.ValueToSelect1 = "Description"
    End Sub
    Private Sub fndstructurecode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndstructurecode.ConnectionString = connectSql.SqlCon()
        'fndstructurecode.Query = "select str_code as [Structure Code], str_description as [Description] from TSPL_GL_STRUCTURE"
        'fndstructurecode.ValueToSelect = "Structure Code"
        'fndstructurecode.ValueToSelect1 = "Description"
        'fndstructurecode.Caption = "Structure Details"
    End Sub
    ''bind data to the window form 
    Private Sub binddata()
        Dim strsegname1 As String = ""
        Dim struseinclosing As String = ""
        Dim Report_Filter As String = ""
        Dim isNewEntry As Boolean = True
        Dim da As New SqlDataAdapter("select seg_no, seg_name, seg_length, seg_useinclosing,Report_Filters  from tspl_gl_segment", connectSql.SqlCon)
        Dim dt As New DataTable()
        da.Fill(dt)
        If dt.Rows.Count > 0 Then
            isNewEntry = False
            Dim row As DataRow = dt.Rows(0)
            For Each r As Object In row.Table.Rows
                Dim k As GridViewRowInfo = dgvsegment.Rows.AddNew()
                k.Cells(0).Value = r(0).ToString()

                k.Cells(1).Value = r(1).ToString()
                strsegname1 = connectSql.RunScalar("select top 1 Segment_code  from TSPL_GL_SEGMENT_CODE where Segment_name ='" + k.Cells(1).Value.ToString() + "'")
                If strsegname1 <> "" Then
                    k.Cells(1).ReadOnly = True
                    k.Cells(2).ReadOnly = True
                End If
                k.Cells(2).Value = r(2).ToString()
                struseinclosing = r(3).ToString()
                If struseinclosing = "N" Then
                    struseinclosing = "No"
                Else
                    struseinclosing = "Yes"
                End If
                k.Cells(3).Value = struseinclosing
                '' Anubhooti 20-Oct-2014 BM00000003783
                Report_Filter = r(4).ToString()
                If clsCommon.CompairString(Report_Filter, "0") = CompairStringResult.Equal Then
                    Report_Filter = "No"
                Else
                    Report_Filter = "Yes"
                End If
                k.Cells(4).Value = Report_Filter
            Next
        End If
        Dim strmulticurrency As String = ""
        Dim strpostingprevious As String = ""
        Dim strprovisionalposting As String = ""
        Dim straccountgroup As String = ""
        Dim strGLAccBySubAcc As Integer

        Dim stroption As String = "select Functional_currency , Multicurrency, Default_Ratetype, Account_Group, Closing_Account, Post_Previousyear, Provisional_Posting, Source_Code, Account_Segment, Structure_Code,AutoGenerated_GLCode_From_SubAccount,Clearing_Account  from TSPL_GLSETTING"
        '' Added by abhishek as on 12/10/2012
        dt = clsDBFuncationality.GetDataTable(stroption)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                txtfuncurrency.Text = dr(0).ToString()
                txtfuncurrency.Enabled = False
                strmulticurrency = dr(1).ToString()
                If strmulticurrency = "Y" Then
                    chkmulticurrency.Checked = True
                    chkmulticurrency.Enabled = False
                Else
                    chkmulticurrency.Checked = False
                End If
                fnddefaultratetype.Value = dr(2).ToString()
                straccountgroup = dr(3).ToString()
                If straccountgroup = "Y" Then
                    chkaccountgroup.Checked = True
                Else
                    chkaccountgroup.Checked = False
                End If
                fndclosingaccount1.Value = dr(4).ToString()
                strpostingprevious = dr(5).ToString()
                If strpostingprevious = "Y" Then
                    chkpostingprevious.Checked = True
                Else
                    chkpostingprevious.Checked = False
                End If
                strprovisionalposting = dr(6).ToString()
                If strprovisionalposting = "Y" Then
                    chkprovisionalposting.Checked = True
                Else
                    chkprovisionalposting.Checked = False
                End If
                fndsourcecode.Value = dr(7).ToString()
                ddlaccountsegment.Text = dr(8).ToString()
                ddlaccountsegment.Enabled = False
                fndstructurecode.Value = dr(9).ToString()
                '' Anubhooti 12-Dec-2014 (AutoGLCode)
                strGLAccBySubAcc = clsCommon.myCstr(dr("AutoGenerated_GLCode_From_SubAccount"))
                If clsCommon.CompairString(strGLAccBySubAcc, "1") = CompairStringResult.Equal Then
                    ChkGLAccBySubAcc.Checked = True
                Else
                    ChkGLAccBySubAcc.Checked = False
                End If
                TxtClrAcc.Value = clsCommon.myCstr(dr("Clearing_Account"))
                If clsCommon.myLen(TxtClrAcc.Value) > 0 Then
                    lblClrAcc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ISNULL(Description,'') AS Desp  from tspl_gl_accounts Where Account_Code ='" & clsCommon.myCstr(TxtClrAcc.Value) & "'"))
                Else
                    lblClrAcc.Text = ""
                End If
                ''
            Next
        End If

        If isNewEntry Then
            If dgvsegment.Rows.Count > 0 Then
                dgvsegment.Rows(0).Cells(1).ReadOnly = False
                dgvsegment.Rows(0).Cells(2).ReadOnly = False
            End If
        Else
            btnsave.Text = "Update"
            dgvsegment.Rows(0).Cells(1).ReadOnly = True
            dgvsegment.Rows(0).Cells(2).ReadOnly = True
        End If
        chkDoubleClickSystem.Checked = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DoubleClickOnVC, clsFixedParameterCode.DoubleClickOnVC, Nothing)) = 1, True, False)
    End Sub

    Private Sub mnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnclose.Click
        Me.Close()
    End Sub

    Private Sub txtfuncurrency_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtfuncurrency.KeyPress
        txtfuncurrency.CharacterCasing = CharacterCasing.Upper
    End Sub

    Private Sub fndclosingaccount1__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndclosingaccount1._MYValidating
        Dim qry As String = "select Account_Code as Code, Description  from tspl_gl_accounts"
        fndclosingaccount1.Value = clsCommon.ShowSelectForm("AC_CODE", qry, "Code", "", fndclosingaccount1.Value, "", isButtonClicked)
    End Sub

    Private Sub fndsourcecode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndsourcecode._MYValidating
        Dim qry As String = "select sourcecode as [SourceCode], sourceledger as [Source Ledger] from TSPL_GL_SOURCECODE "
        fndsourcecode.Value = clsCommon.ShowSelectForm("SC_CODE", qry, "SourceCode", "", fndsourcecode.Value, "", isButtonClicked)
    End Sub

    Private Sub fnddefaultratetype__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fnddefaultratetype._MYValidating

    End Sub

    Private Sub fndstructurecode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndstructurecode._MYValidating
        Dim qry As String = "select str_code as [StructureCode], str_description as [Description] from TSPL_GL_STRUCTURE"
        fndstructurecode.Value = clsCommon.ShowSelectForm("SC_CODE1", qry, "StructureCode", "", fndsourcecode.Value, "", isButtonClicked)
    End Sub

    Private Sub dgvsegment_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles dgvsegment.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Do you want to delete current row?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub RadButton300_Click(sender As Object, e As EventArgs) Handles RadButton300.Click
        Dim coll As Dictionary(Of String, String)
        coll = New Dictionary(Of String, String)()
        coll.Add("Account_Code", "varchar(30) null")
        clsCommonFunctionality.CreateOrAlterTable("TEMP_CREATED_GL_Account", coll)

        coll = New Dictionary(Of String, String)()
        coll.Add("Account_Code", "varchar(30) null")
        clsCommonFunctionality.CreateOrAlterTable("TEMP_DELETED_GL_Account", coll)

        clsDBFuncationality.ExecuteNonQuery("delete from TEMP_CREATED_GL_Account")
        clsDBFuncationality.ExecuteNonQuery("delete from TEMP_DELETED_GL_Account")
    End Sub

    Private Sub RadButton299_Click(sender As Object, e As EventArgs) Handles RadButton299.Click
        Try


            Dim qry As String = " select * from TSPL_GL_ACCOUNTS  "
            Dim QryInsert As String = ""
            Dim arr As ArrayList = clsCommon.ShowMultipleSelectForm(False, "GLAccount", qry, "Account_code", "", Nothing, Nothing)
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                clsDBFuncationality.ExecuteNonQuery("delete from TEMP_DELETED_GL_Account")
                QryInsert = "insert into TEMP_DELETED_GL_Account "
                QryInsert += "select Account_Code from TSPL_GL_ACCOUNTS where Account_code in(" + clsCommon.GetMulcallString(arr) & ")"
                clsDBFuncationality.ExecuteNonQuery(QryInsert)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton297_Click(sender As Object, e As EventArgs) Handles RadButton297.Click
        Dim qry As String = ""
        Dim dt As New DataTable()
        Try
            qry = "select * from TEMP_DELETED_GL_Account where Account_code not in (select Account_code from TEMP_CREATED_GL_Account)"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry)
            Dim strErro As String = ""
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If common.clsCommon.MyMessageBoxShow("Update Control Account 'N' for GL Accounts  " + clsCommon.myCstr(dt.Rows.Count) + " ", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                    clsCommon.ProgressBarPercentShow()
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        Dim strDocNo As String = clsCommon.myCstr(dt.Rows(ii)("Account_code"))
                        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                        Try

                            clsDBFuncationality.ExecuteNonQuery("update TSPL_GL_ACCOUNTS  set  ControlAccount='N' where Account_code='" & strDocNo & "'", trans)

                            clsDBFuncationality.ExecuteNonQuery("Insert into TEMP_CREATED_GL_Account values('" & strDocNo & "')", trans)
                            trans.Commit()
                        Catch ex As Exception
                            trans.Rollback()
                            strErro += "GL Account- " + strDocNo + " Exception -" + ex.Message + Environment.NewLine
                        End Try
                        clsCommon.ProgressBarPercentUpdate((ii + 1) * 100 / dt.Rows.Count, "Update Control Account 'N' " + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                    Next
                    clsCommon.ProgressBarPercentHide()
                    If clsCommon.myLen(strErro) > 0 Then
                        common.clsCommon.MyMessageBoxShow(strErro, Me.Text)
                    Else
                        common.clsCommon.MyMessageBoxShow("Task completed", Me.Text)
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub RadButton298_Click(sender As Object, e As EventArgs) Handles RadButton298.Click
        Dim qry As String = ""
        Dim dt As New DataTable()
        Try
            qry = "select * from TEMP_DELETED_GL_Account where Account_code not in (select Account_code from TEMP_CREATED_GL_Account)"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry)
            Dim strErro As String = ""
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If common.clsCommon.MyMessageBoxShow("Update Control Account 'Y' for Gl Accounts  " + clsCommon.myCstr(dt.Rows.Count) + " ", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                    clsCommon.ProgressBarPercentShow()
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        Dim strDocNo As String = clsCommon.myCstr(dt.Rows(ii)("Account_code"))
                        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                        Try

                            clsDBFuncationality.ExecuteNonQuery("update TSPL_GL_ACCOUNTS  set  ControlAccount='Y' where Account_code='" & strDocNo & "'", trans)

                            clsDBFuncationality.ExecuteNonQuery("Insert into TEMP_CREATED_GL_Account values('" & strDocNo & "')", trans)
                            trans.Commit()
                        Catch ex As Exception
                            trans.Rollback()
                            strErro += "GL Account- " + strDocNo + " Exception -" + ex.Message + Environment.NewLine
                        End Try
                        clsCommon.ProgressBarPercentUpdate((ii + 1) * 100 / dt.Rows.Count, "Update Control Account 'Y' " + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                    Next
                    clsCommon.ProgressBarPercentHide()
                    If clsCommon.myLen(strErro) > 0 Then
                        common.clsCommon.MyMessageBoxShow(strErro, Me.Text)
                    Else
                        common.clsCommon.MyMessageBoxShow("Task completed", Me.Text)
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    '' Anubhooti 17-Mar-2015 (GL Account For VCGL)
    Private Sub TxtClrAcc__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtClrAcc._MYValidating
        Dim qry As String = "select Account_Code as Code, Description  from tspl_gl_accounts"
        TxtClrAcc.Value = clsCommon.ShowSelectForm("GL_Clr", qry, "Code", " CONTROLACCOUNT ='Y' ", TxtClrAcc.Value, "", isButtonClicked)
        If clsCommon.myLen(TxtClrAcc.Value) > 0 Then
            lblClrAcc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ISNULL(Description,'') AS Desp  from tspl_gl_accounts Where Account_Code ='" & clsCommon.myCstr(TxtClrAcc.Value) & "'"))
        Else
            lblClrAcc.Text = ""
        End If
    End Sub
End Class
