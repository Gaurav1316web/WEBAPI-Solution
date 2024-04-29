'Developed By -BibhuPrasad Parida
'Database - TSPLERP
'Table - TSPL_GL_SOURCECODE
'Start Date -
'End Date -
#Region "NameSpace"
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports common

#End Region
Public Class FrmSourceCode
    Inherits FrmMainTranScreen
    Dim userCode, companyCode As String
#Region "Constructor"
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company

    End Sub
#End Region
#Region "Variables"
    Dim sql As String
    Dim ds As DataSet
    Dim dt1 As DataTable
    Dim dr As SqlDataReader
    Dim tableName As String = "TSPL_TAX_MASTER"
    Dim tableCode As String = "Tax_Code"
    Dim codePrefix As String = "TAX"
    Dim objstr As String = "Tecxpert Software Pvt Ltd."
    Dim dt As Date = Date.Today
    Dim ButtonToolTip As ToolTip = New ToolTip()


#End Region
#Region "Finder Load"
    Private Sub fndSourceCode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndSourceCode.ConnectionString = connectSql.SqlCon()
        'fndSourceCode.Query = "select SourceCode as [Source Code],SourceDescription as [Description] from TSPL_GL_SOURCECODE "
        'fndSourceCode.ValueToSelect = "Source Code"
        'fndSourceCode.ValueToSelect1 = "Description"
        'fndSourceCode.Caption = "Source Code Details"
    End Sub
#End Region
#Region "Page Load"
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.sourceCode)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnSave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 02/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnSave.Visible = True Then
            MenuImport.Enabled = True
            MenuExport.Enabled = True
        Else
            MenuImport.Enabled = False
            MenuExport.Enabled = False
        End If
        '--------------------------------------------------

        'btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmSourceCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
            'ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            '    PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub
    Private Sub FrmSourceCode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        'AddHandler fndSourceCode.txtValue.TextChanged, AddressOf TextChanged1
        btnSave.Enabled = True
        btnDelete.Enabled = False
        mskSourceCode.TabIndex = 0

        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New Trasnaction")
        SetLength()
        funReset()
    End Sub

    Public Sub SetLength()
        txtSourceCodeDesc.MaxLength = 60
    End Sub


#End Region
#Region "TextChanged Event"
    Private Sub TextChanged1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim s As String
        s = clsDBFuncationality.getSingleValue("select SourceCode ,SourceDescription  from TSPL_GL_SOURCECODE where SourceCode='" + fndSourceCode.Value + "'")
        If s <> fndSourceCode.Focus Then
        Else
            funFill()
        End If

    End Sub
#End Region
#Region "ButtonClick Event"
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub
    Sub SaveData()

        If MyBase.isModifyonPasswordFlag Then
            If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.sourceCode, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
            Else
                Return
            End If
        End If
        If btnSave.Text = "Save" Then
            funInsert()
        ElseIf btnSave.Text = "Update" Then
            funUpdate()
        End If
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()

    End Sub
    Sub DeleteData()
        If myMessages.deleteConfirm() Then
            funDelete()
        Else

        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub btnNew_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        funReset()
    End Sub

#End Region
#Region "Methods"
    'insert Source Code Details
    Private Sub funInsert()
        If mskSourceCode.Text = "" Then
            myMessages.blankValue(Me, "Source Code", Me.Text)
            fndSourceCode.Focus()
        ElseIf mskSourceCode.Text.Length < 5 Then
            common.clsCommon.MyMessageBoxShow("Source Code must be fill properly")
            mskSourceCode.Focus()
        Else
            Dim objStr11 As String
            Dim objStr12 As String
            Dim objStr13 As String = mskSourceCode.Text
            Dim objStr14 As String = mskSourceCode.Text
            objStr11 = objStr13.Substring(0, 2).ToString()
            objStr12 = objStr14.Substring(3, 2).ToString()

            Try
                connectSql.RunSp("sp_tspl_gl_sourcecode_insert", New SqlParameter("@SourceCode", objStr13), New SqlParameter("@SourceLedger", objStr11), New SqlParameter("@SourceType", objStr12), New SqlParameter("@SourceDescription", txtSourceCodeDesc.Text), New SqlParameter("@CreatedBy", userCode), New SqlParameter("@CreatedDate", connectSql.serverDate()), New SqlParameter("@ModifyBy", userCode), New SqlParameter("@ModifyDate", connectSql.serverDate()), New SqlParameter("@CompCode", companyCode))
                clsDBFuncationality.ExecuteNonQuery(" update TSPL_GL_SOURCECODE set TallyName = '" + CboTallyName.SelectedValue + "' where SourceCode ='" + objStr13 + "'")
                myMessages.insert()
                btnSave.Text = "Update"
                btnSave.Enabled = True
                btnDelete.Enabled = True

            Catch ex As Exception
                MessageBox.Show(ex.Message.ToString())
            End Try
        End If


    End Sub
    'update Source Code Details
    Private Sub funUpdate()
        If mskSourceCode.Text = "" Then
            myMessages.blankValue(Me, "Source Code", Me.Text)
            fndSourceCode.Focus()
        ElseIf mskSourceCode.Text.Length < 5 Then
            common.clsCommon.MyMessageBoxShow(Me, "Source Code must be fill properly", Me.Text)
            mskSourceCode.Focus()
        Else
            Dim objStr11 As String
            Dim objStr12 As String
            Dim objStr13 As String = mskSourceCode.Text
            Dim objStr14 As String = mskSourceCode.Text
            objStr11 = objStr13.Substring(0, 2).ToString()
            objStr12 = objStr14.Substring(3, 2).ToString()
            Try

                connectSql.RunSp("sp_tspl_gl_sourcecode_update", New SqlParameter("@SourceCode", objStr13), New SqlParameter("@SourceLedger", objStr11), New SqlParameter("@SourceType", objStr12), New SqlParameter("@SourceDescription", txtSourceCodeDesc.Text), New SqlParameter("@CreatedBy", userCode), New SqlParameter("@CreatedDate", connectSql.serverDate()), New SqlParameter("@ModifyBy", userCode), New SqlParameter("@ModifyDate", connectSql.serverDate()), New SqlParameter("@CompCode", companyCode))
                clsDBFuncationality.ExecuteNonQuery(" update TSPL_GL_SOURCECODE set TallyName = '" + CboTallyName.SelectedValue + "' where SourceCode ='" + objStr13 + "'")
                myMessages.update()

            Catch ex As Exception
                MessageBox.Show(ex.Message.ToString())
            End Try
        End If
    End Sub
    'fill Source Code Details
    Private Sub funFill()
        Try
            ''Added by abhishek as on 12/10/2012
            Dim qry As String = "select SourceCode ,SourceDescription,TallyName  from TSPL_GL_SOURCECODE where SourceCode='" + fndSourceCode.Value + "'"
            dt1 = clsDBFuncationality.GetDataTable(qry)
            If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then

                For Each dr As DataRow In dt1.Rows
                    mskSourceCode.Text = dr(0).ToString()
                    txtSourceCodeDesc.Text = dr(1).ToString()
                    If clsCommon.myLen(dr(2)) > 0 Then
                        CboTallyName.SelectedValue = dr(2).ToString()
                    Else
                        CboTallyName.SelectedValue = ""
                        CboTallyName.SelectedIndex = -1

                    End If
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        End Try
        btnSave.Text = "Update"
        btnDelete.Enabled = True
        btnSave.Enabled = True

        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub
    'priti added on 01-06-2011 --- To implement the access control
    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        'If funCheckLoginStatus() = False Then Exit Function
    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "SOURCE-CODE"
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
    '            btnSave.Enabled = False
    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access
    '            btnDelete.Enabled = False
    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception

    '    End Try
    'End Function
    'Code ends here

    'delete Source Code Details
    Private Sub funDelete()
        connectSql.RunSp("sp_tspl_gl_sourcecode_delete", New SqlParameter("@SourceCode", mskSourceCode.Text))
        myMessages.delete()
        btnSave.Text = "Save"
        btnDelete.Enabled = False
    End Sub
    Private Sub funReset()
        Me.mskSourceCode.Text = Nothing
        'fndSourceCode.txtValue.Text = ""
        txtSourceCodeDesc.Text = ""
        btnSave.Enabled = True
        btnSave.Text = "Save"
        btnDelete.Enabled = False
        mskSourceCode.Focus()
        CboTallyName.SelectedValue = ""
        If objCommonVar.IsSendToTally Then
            CboTallyName.Visible = True
            CboTallyName.DataSource = GetVoucherType()
            CboTallyName.ValueMember = "Code"
            CboTallyName.DisplayMember = "Code"
            CboTallyName.SelectedValue = ""
            CboTallyName.SelectedIndex = -1
            MyLabel1.Visible = True
        Else
            CboTallyName.Visible = False
            MyLabel1.Visible = False
        End If

    End Sub

    Private Function GetVoucherType() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("Code", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "Contra"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "CreditNote"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "DebitNote"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Journal"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Payment"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Purchase"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Receipt"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Sales"
        dt.Rows.Add(dr)

        dt.AcceptChanges()
        Return dt
    End Function

#End Region
#Region "MaskTextBox Leave Event"

    Private Sub MaskedTextBox1_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        btnSave.Enabled = True

    End Sub
#End Region
#Region "Finder Leave Event"
    Private Sub mskSourceCode_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        fndSourceCode.Value = mskSourceCode.Text
        btnSave.Enabled = True
        btnSave.Text = "Save"

        Dim str As String
        str = clsDBFuncationality.getSingleValue("select SourceCode ,SourceDescription  from TSPL_GL_SOURCECODE where SourceCode='" + fndSourceCode.Value + "'")
        If str <> "" Then
            btnSave.Text = "Update"
            btnDelete.Enabled = True
        Else : str = ""
            btnSave.Text = "Save"
            btnDelete.Enabled = False
        End If

    End Sub
#End Region
#Region "KeyPress Event"
    Private Sub mskSourceCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper())
    End Sub
#End Region
#Region "SourceCode Leave Event"


    Private Sub mskSourceCode_Leave_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        fndSourceCode.Value = mskSourceCode.Text
        btnSave.Enabled = True
        btnSave.Text = "Save"
        Dim str As String
        str = clsDBFuncationality.getSingleValue(" select SourceCode ,SourceDescription  from TSPL_GL_SOURCECODE where SourceCode='" + fndSourceCode.Value + "'")
        If str <> "" Then
            btnSave.Text = "Update"
            btnDelete.Enabled = True
        Else : str = ""
            btnSave.Text = "Save"
            btnDelete.Enabled = False
        End If
    End Sub
#End Region
#Region "Import/Export"

    Private Sub MenuExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuExport.Click
        sql = "select SourceCode,SourceDescription,TallyName from TSPL_GL_SOURCECODE "
        ListImpExpColumnsMandatory = New List(Of String)({"SourceCode"})
        ListImpExpColumnsSuperMandatory = New List(Of String)({"SourceCode"})
        transportSql.ExporttoExcel(sql, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)
    End Sub

    Private Sub MenuImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "SourceCode", "SourceDescription", "TallyName") Then
            Dim trans As SqlTransaction = Nothing
            Try
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows

                    Dim strSourceCode As String = clsCommon.myCstr(grow.Cells(0).Value).ToUpper()
                    Dim strSourceDesc As String = clsCommon.myCstr(grow.Cells(1).Value)
                    Dim strTallyName As String = clsCommon.myCstr(grow.Cells(2).Value)

                    Dim objstrSourceLedger As String
                    Dim objStrSourceType As String
                    Dim objStrSLedger As String = strSourceCode
                    Dim objStrSType As String = strSourceCode
                    objstrSourceLedger = clsCommon.myCstr(objStrSLedger.Substring(0, 2))
                    objStrSourceType = clsCommon.myCstr(objStrSType.Substring(3, 2))

                    If String.IsNullOrEmpty(strSourceCode) Then
                        Throw New Exception("Source Code Can Not be Blank/Empty")
                    ElseIf clsCommon.myLen(strSourceCode) < 4 Then
                        Throw New Exception("Source Code must be fill properly")
                    ElseIf clsCommon.myLen(strSourceCode) > 5 Then
                        Throw New Exception("Source Code must be fill properly")
                    End If

                    Dim sql1 As String = "select COUNT(*) from TSPL_GL_SOURCECODE  where SourceCode='" + strSourceCode + "'"
                    Dim i As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(sql1, trans))
                    If (i = 0) Then
                        sql = "insert into TSPL_GL_SOURCECODE values('" + strSourceCode + "','" + objstrSourceLedger + "','" + objStrSourceType + "','" + strSourceDesc + "','" + userCode + "','" + connectSql.serverDate(trans) + "','" + userCode + "','" + connectSql.serverDate(trans) + "','" + companyCode + "','" + strTallyName + "')"
                        clsDBFuncationality.ExecuteNonQuery(sql, trans)
                    Else
                        clsDBFuncationality.SaveAStorePorcedure(trans, "sp_tspl_gl_sourcecode_update", New SqlParameter("@SourceCode", strSourceCode), New SqlParameter("@SourceLedger", objstrSourceLedger), New SqlParameter("@SourceType", objStrSourceType), New SqlParameter("@SourceDescription", strSourceDesc), New SqlParameter("@CreatedBy", userCode), New SqlParameter("@CreatedDate", dt), New SqlParameter("@ModifyBy", userCode), New SqlParameter("@ModifyDate", dt), New SqlParameter("@CompCode", companyCode))
                        clsDBFuncationality.ExecuteNonQuery(" update TSPL_GL_SOURCECODE set TallyName = '" + strTallyName + "' where SourceCode ='" + strSourceCode + "'", trans)
                    End If
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                trans.Rollback()
                myMessages.myExceptions(ex)
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub
#End Region

    Private Sub RadMenuItem6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem6.Click
        Me.Close()
    End Sub

    Private Sub fndSourceCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndSourceCode._MYNavigator
        Dim qry As String = "select SourceCode  from TSPL_GL_SOURCECODE Where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_GL_SOURCECODE.SourceCode=(select MIN(SourceCode) from TSPL_GL_SOURCECODE)"
            Case NavigatorType.Last
                qry += " and TSPL_GL_SOURCECODE.SourceCode=(select MAX(SourceCode) from TSPL_GL_SOURCECODE)"
            Case NavigatorType.Next
                qry += " and TSPL_GL_SOURCECODE.SourceCode=(select Min(SourceCode) from TSPL_GL_SOURCECODE where SourceCode > '" + fndSourceCode.Value + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_GL_SOURCECODE.SourceCode=(select Max(SourceCode) from TSPL_GL_SOURCECODE where SourceCode < '" + fndSourceCode.Value + "')"
            Case NavigatorType.Current
                qry += " and TSPL_GL_SOURCECODE.SourceCode='" + fndSourceCode.Value + "'"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            fndSourceCode.Value = clsCommon.myCstr(dt.Rows(0)("SourceCode"))
            funFill()
        End If

    End Sub

    Private Sub fndSourceCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndSourceCode._MYValidating
        '        Dim qry As String = "select SourceCode as [SourceCode],SourceDescription as [Description] from TSPL_GL_SOURCECODE "
        '       fndSourceCode.Value = clsCommon.ShowSelectForm("fndSourceCode", qry, "SourceCode", "", fndSourceCode.Value, "SourceCode", isButtonClicked)
        fndSourceCode.Value = clsGLSourceCode.getFinder("", fndSourceCode.Value, isButtonClicked)
        funFill()
    End Sub
End Class
