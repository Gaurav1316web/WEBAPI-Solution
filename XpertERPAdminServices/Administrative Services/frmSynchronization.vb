'--18/12/2014--form Add By- Panch Raj ---------
Imports common
Imports System.Data
Imports System.Data.SqlClient

Public Class frmSynchronization
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"
    'Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String

    Const colSourceSeqNo As String = "colSourceSeqNo"
    Const colSourceTableName As String = "colSourceTableName"

    Const colTargetSeqNo As String = "colTargetSeqNo"
    Const colTargetTableName As String = "colTargetTableName"
    Const colTargetPK As String = "colTargetPK"
    Const colTargetPK2 As String = "colTargetPK2"
    Const colTargetDetail As String = "colTargetDetail"
    Const colType As String = "colType"
    Const col_COND As String = "col_COND"

   
#End Region

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Save()
    End Sub
    Sub LoadGridColumns(ByVal gv As RadGridView)
        'gvSourceTables.DataSource = Nothing
        gv.Rows.Clear()
        gv.Columns.Clear()
        If clsCommon.CompairString(gv.Name, gvSourceTables.Name) = CompairStringResult.Equal Then
            'Dim SourceSeq As New GridViewTextBoxColumn
            'SourceSeq.FormatString = ""
            'SourceSeq.HeaderText = "Sequence No"
            'SourceSeq.Name = colSourceSeqNo
            'SourceSeq.Width = 100
            'SourceSeq.ReadOnly = True
            'SourceSeq.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            'gv.Columns.Add(SourceSeq)

            Dim SourceTableName As New GridViewTextBoxColumn
            SourceTableName.FormatString = ""
            SourceTableName.HeaderText = "Table Name"
            SourceTableName.Name = colSourceTableName
            SourceTableName.Width = 200
            SourceTableName.ReadOnly = True
            SourceTableName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            gv.Columns.Add(SourceTableName)
            gvSourceTables.EnableCustomFiltering = False
            gvSourceTables.EnableFiltering = True
        Else
            Dim TargetSeq As New GridViewTextBoxColumn
            TargetSeq.FormatString = ""
            TargetSeq.HeaderText = "Sequence No"
            TargetSeq.Name = colTargetSeqNo
            TargetSeq.Width = 100
            TargetSeq.ReadOnly = False
            TargetSeq.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            gv.Columns.Add(TargetSeq)

            Dim TargetTableName As New GridViewTextBoxColumn
            TargetTableName.FormatString = ""
            TargetTableName.HeaderText = "Table Name"
            TargetTableName.Name = colTargetTableName
            TargetTableName.Width = 200
            TargetTableName.ReadOnly = True
            TargetTableName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            gv.Columns.Add(TargetTableName)

            Dim TargetPK As New GridViewTextBoxColumn
            TargetPK.FormatString = ""
            TargetPK.HeaderText = "Primary Key"
            TargetPK.Name = colTargetPK
            TargetPK.Width = 100
            TargetPK.ReadOnly = True
            TargetPK.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            gv.Columns.Add(TargetPK)

            Dim TargetPK2 As New GridViewTextBoxColumn
            TargetPK2.FormatString = ""
            TargetPK2.HeaderText = "Primary Key"
            TargetPK2.Name = colTargetPK2
            TargetPK2.Width = 100
            TargetPK2.ReadOnly = True
            TargetPK2.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            gv.Columns.Add(TargetPK2)
            

            Dim TargetDetail As New GridViewCheckBoxColumn
            TargetDetail.FormatString = ""
            TargetDetail.HeaderText = "Is Detail"
            TargetDetail.Name = colTargetDetail
            TargetDetail.Width = 80
            TargetDetail.ReadOnly = False
            TargetDetail.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            gv.Columns.Add(TargetDetail)
            gvTargetTables.EnableCustomFiltering = False
            gvTargetTables.EnableFiltering = True

            Dim _Type As New GridViewTextBoxColumn
            _Type.FormatString = ""
            _Type.HeaderText = "Type"
            _Type.Name = colType
            _Type.Width = 80
            _Type.ReadOnly = False
            _Type.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            gv.Columns.Add(_Type)

            Dim _COND As New GridViewTextBoxColumn
            _COND.FormatString = ""
            _COND.HeaderText = "Condition"
            _COND.Name = col_COND
            _COND.Width = 80
            _COND.ReadOnly = False
            _COND.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            gv.Columns.Add(_COND)

            gvTargetTables.EnableCustomFiltering = False
            gvTargetTables.EnableFiltering = True
        End If
        

    End Sub
    Public Sub Save()
        If AllowToSave() Then

            If MyBase.isModifyonPasswordFlag Then
                If clsPasswordCheckForMasters.CheckMasterPwd("", clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                Else
                    Return
                End If
            End If

            Dim obj As New clsHostSettings()
            obj.HOST_ID = txtClientNameIP.Tag
            obj.SERVER_TYPE = "Client"
            obj.SERVER_NAME_IP = txtClientNameIP.Text
            obj.DATABASE_NAME = txtClientDBName.Text
            obj.SCHEMA_NAME = txtClientSchema.Text
            obj.DB_USER_ID = txtClientUserId.Text
            obj.DB_PWD = txtClientPassword.Text
            obj.REMARKS = txtClientRemarks.Text

            Dim obj1 As New clsHostSettings()
            obj1.HOST_ID = txtServerNameIP.Tag
            obj1.SERVER_TYPE = "Server"
            obj1.SERVER_NAME_IP = txtServerNameIP.Text
            obj1.DATABASE_NAME = txtServerDBName.Text
            obj1.SCHEMA_NAME = txtServerSchema.Text
            obj1.DB_USER_ID = txtServerUserId.Text
            obj1.DB_PWD = txtServerPassword.Text
            obj1.REMARKS = txtServerRemarks.Text

            '' save tables 
            Dim objList As New List(Of clsSyncTables)
            Dim objSync As New clsSyncTables
            For Each grow As GridViewRowInfo In gvTargetTables.Rows
                If grow.IsVisible = False Then
                    Continue For
                End If
                objSync = New clsSyncTables
                objSync.SEQ_NO = grow.Cells(colTargetSeqNo).Value
                objSync.TABLE_NAME = grow.Cells(colTargetTableName).Value
                objSync.PRIMARY_KEY = grow.Cells(colTargetPK).Value
                objSync.PRIMARY_KEY2 = grow.Cells(colTargetPK2).Value
                objSync.Is_Detail = IIf(grow.Cells(colTargetDetail).Value = True, 1, 0)
                objSync._COND = grow.Cells(col_COND).Value
                objSync._Type = grow.Cells(colType).Value
                objList.Add(objSync)

            Next
            If (obj.SaveData(obj)) AndAlso (obj1.SaveData(obj1)) AndAlso objSync.SaveData(objList) Then
                common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                LoadData()
            End If

        End If
    End Sub

    Sub LoadData()
        btnSave.Enabled = True
        btnDelete.Enabled = True
        funReset()
        '' load client settings
        Dim obj As New clsHostSettings()
        obj = clsHostSettings.GetData("Client")
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.HOST_ID) > 0) Then
            btnSave.Text = "Save"
            txtClientNameIP.Tag = obj.HOST_ID
            txtClientNameIP.Text = obj.SERVER_NAME_IP
            txtClientDBName.Text = obj.DATABASE_NAME
            txtClientSchema.Text = obj.SCHEMA_NAME
            txtClientUserId.Text = obj.DB_USER_ID
            txtClientPassword.Text = obj.DB_PWD
            txtClientRetypePWD.Text = obj.DB_PWD
            txtClientRemarks.Text = obj.REMARKS

            '' load server settings
            Dim obj1 As New clsHostSettings()
            obj1 = clsHostSettings.GetData("Server")
            If (obj1 IsNot Nothing AndAlso clsCommon.myLen(obj1.HOST_ID) > 0) Then
                btnSave.Text = "Save"
                txtServerNameIP.Tag = obj1.HOST_ID
                txtServerNameIP.Text = obj1.SERVER_NAME_IP
                txtServerDBName.Text = obj1.DATABASE_NAME
                txtServerSchema.Text = obj1.SCHEMA_NAME
                txtServerUserId.Text = obj1.DB_USER_ID
                txtServerPassword.Text = obj1.DB_PWD
                txtServerRetypePassword.Text = obj1.DB_PWD
                txtServerRemarks.Text = obj1.REMARKS
            End If
            '' load sync tables
            gvTargetTables.Rows.Clear()
            For Each objSync As clsSyncTables In obj.objSyncList
                gvTargetTables.Rows.AddNew()
                gvTargetTables.Rows(gvTargetTables.Rows.Count - 1).Cells(colTargetSeqNo).Value = objSync.SEQ_NO
                gvTargetTables.Rows(gvTargetTables.Rows.Count - 1).Cells(colTargetSeqNo).ReadOnly = False
                gvTargetTables.Rows(gvTargetTables.Rows.Count - 1).Cells(colTargetTableName).Value = objSync.TABLE_NAME
                gvTargetTables.Rows(gvTargetTables.Rows.Count - 1).Cells(colTargetPK).Value = objSync.PRIMARY_KEY
                gvTargetTables.Rows(gvTargetTables.Rows.Count - 1).Cells(colTargetPK2).Value = objSync.PRIMARY_KEY2
                gvTargetTables.Rows(gvTargetTables.Rows.Count - 1).Cells(colTargetDetail).Value = objSync.Is_Detail
                gvTargetTables.Rows(gvTargetTables.Rows.Count - 1).Cells(colTargetDetail).ReadOnly = False

                gvTargetTables.Rows(gvTargetTables.Rows.Count - 1).Cells(colType).Value = objSync._Type
                gvTargetTables.Rows(gvTargetTables.Rows.Count - 1).Cells(col_COND).Value = objSync._COND
                gvTargetTables.Rows(gvTargetTables.Rows.Count - 1).Cells(colType).ReadOnly = False
            Next
        End If

       
        gvTargetTables.EnableCustomFiltering = False
        gvTargetTables.EnableFiltering = True
    End Sub

    Function AllowToSave() As Boolean
        If clsCommon.myLen(txtClientNameIP.Text) <= 0 Then
            myMessages.blankValue(Me, "Client Name/IP", Me.Text)
            txtClientNameIP.Focus()
            Return False
        ElseIf clsCommon.myLen(txtClientDBName.Text) <= 0 Then
            myMessages.blankValue(Me, "Client Database Name", Me.Text)
            txtClientDBName.Focus()
            Return False
        ElseIf clsCommon.myLen(txtClientSchema.Text) <= 0 Then
            myMessages.blankValue(Me, "Client Schema Name", Me.Text)
            txtClientSchema.Focus()
            Return False
        ElseIf clsCommon.myLen(txtClientUserId.Text) <= 0 Then
            myMessages.blankValue(Me, "Client Database User Id", Me.Text)
            txtClientUserId.Focus()
            Return False
        ElseIf clsCommon.myLen(txtClientPassword.Text) <= 0 Then
            myMessages.blankValue(Me, "Client Database Password", Me.Text)
            txtClientPassword.Focus()
            Return False
        ElseIf clsCommon.CompairString(txtClientPassword.Text, txtClientRetypePWD.Text, True) <> CompairStringResult.Equal Then
            clsCommon.MyMessageBoxShow(Me, "Client Password and Client Retype Password must be same", Me.Text)
            txtClientPassword.Focus()
            Return False
        ElseIf clsCommon.myLen(txtServerNameIP.Text) <= 0 Then
            myMessages.blankValue(Me, "Server Name/IP", Me.Text)
            txtServerNameIP.Focus()
            Return False
        ElseIf clsCommon.myLen(txtServerDBName.Text) <= 0 Then
            myMessages.blankValue(Me, "Server Database Name", Me.Text)
            txtServerDBName.Focus()
            Return False
        ElseIf clsCommon.myLen(txtServerSchema.Text) <= 0 Then
            myMessages.blankValue(Me, "Server Schema Name", Me.Text)
            txtServerSchema.Focus()
            Return False
        ElseIf clsCommon.myLen(txtServerUserId.Text) <= 0 Then
            myMessages.blankValue(Me, "Server Database User Id", Me.Text)
            txtServerUserId.Focus()
            Return False
        ElseIf clsCommon.myLen(txtServerPassword.Text) <= 0 Then
            myMessages.blankValue(Me, "Server Database Password", Me.Text)
            txtServerPassword.Focus()
            Return False
        ElseIf clsCommon.CompairString(txtServerPassword.Text, txtServerRetypePassword.Text, True) <> CompairStringResult.Equal Then
            clsCommon.MyMessageBoxShow(Me, "Server Password and Server Retype Password must be same", Me.Text)
            txtClientPassword.Focus()
            Return False
        End If
        Return True
    End Function


    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        funDelete()
    End Sub
    
    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsHostSettings.DeleteData("Client")) AndAlso (clsHostSettings.DeleteData("Server")) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Private Sub frmSynchronization_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        gvTargetTables.MasterTemplate.ReadOnly = False
        LoadGridColumns(gvSourceTables)
        LoadGridColumns(gvTargetTables)
        btnSyncNew.Visible = True
        LoadSourceTables()

        LoadData()
        If gvTargetTables.Rows.Count <= 0 Then
            LoadTargetTables()
        End If
    End Sub
    Sub LoadSourceTables()
        gvSourceTables.Rows.Clear()
        Dim qry As String = "select name as Table_Name from sys.tables where is_replicated=0 and type='U' and name not in (select TABLE_NAME  from TSPL_SYNC_TABLES) and name like 'TSPL%' order by name"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        For Each dr As DataRow In dt.Rows
            gvSourceTables.Rows.AddNew()
            gvSourceTables.Rows(gvSourceTables.Rows.Count - 1).Cells(colSourceTableName).Value = dr.Item("Table_Name")
        Next
        gvSourceTables.EnableCustomFiltering = False
        gvSourceTables.EnableFiltering = True
    End Sub
    Sub LoadTargetTables()
        gvTargetTables.Rows.Clear()
        Dim qry As String = "select SEQ_NO,TABLE_NAME,PRIMARY_KEY,PRIMARY_KEY2,Is_Detail,_Type,_COND  from TSPL_SYNC_TABLES order by SEQ_NO"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        For Each dr As DataRow In dt.Rows
            gvTargetTables.Rows.AddNew()
            gvTargetTables.Rows(gvTargetTables.Rows.Count - 1).Cells(colTargetSeqNo).Value = dr.Item("SEQ_NO")
            gvTargetTables.Rows(gvTargetTables.Rows.Count - 1).Cells(colTargetTableName).Value = dr.Item("Table_Name")
            gvTargetTables.Rows(gvTargetTables.Rows.Count - 1).Cells(colTargetPK).Value = clsCommon.myCstr(dr.Item("PRIMARY_KEY"))
            gvTargetTables.Rows(gvTargetTables.Rows.Count - 1).Cells(colTargetDetail).Value = dr.Item("Is_Detail")

            gvTargetTables.Rows(gvTargetTables.Rows.Count - 1).Cells(colTargetSeqNo).Value = dr.Item("SEQ_NO")
            gvTargetTables.Rows(gvTargetTables.Rows.Count - 1).Cells(colTargetSeqNo).ReadOnly = False
            gvTargetTables.Rows(gvTargetTables.Rows.Count - 1).Cells(colTargetTableName).Value = dr.Item("Table_Name")
            gvTargetTables.Rows(gvTargetTables.Rows.Count - 1).Cells(colTargetPK).Value = clsCommon.myCstr(dr.Item("PRIMARY_KEY"))
            gvTargetTables.Rows(gvTargetTables.Rows.Count - 1).Cells(colTargetPK2).Value = clsCommon.myCstr(dr.Item("PRIMARY_KEY2"))
            gvTargetTables.Rows(gvTargetTables.Rows.Count - 1).Cells(colTargetDetail).Value = dr.Item("Is_Detail")
            gvTargetTables.Rows(gvTargetTables.Rows.Count - 1).Cells(colTargetDetail).ReadOnly = False

            gvTargetTables.Rows(gvTargetTables.Rows.Count - 1).Cells(colType).Value = clsCommon.myCstr(dr.Item("_Type"))
            gvTargetTables.Rows(gvTargetTables.Rows.Count - 1).Cells(col_COND).Value = clsCommon.myCstr(dr.Item("_COND"))
            gvTargetTables.Rows(gvTargetTables.Rows.Count - 1).Cells(colType).ReadOnly = False
        Next
        gvTargetTables.EnableCustomFiltering = False
        gvTargetTables.EnableFiltering = True
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmSynchronization)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")           
        End If
        btnSave.Visible = MyBase.isModifyFlag

        '--------------------------------------------------
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        funReset()
    End Sub

    Sub funReset()       
        ''clear client settings
        btnSave.Text = "Save"
        txtClientNameIP.Tag = ""
        txtClientNameIP.Text = ""
        txtClientDBName.Text = ""
        txtClientSchema.Text = ""
        txtClientUserId.Text = ""
        txtClientPassword.Text = ""
        txtClientRemarks.Text = ""

        '' clear server settings      
        txtServerNameIP.Tag = ""
        txtServerNameIP.Text = ""
        txtServerDBName.Text = ""
        txtServerSchema.Text = ""
        txtServerUserId.Text = ""
        txtServerPassword.Text = ""
        txtServerRemarks.Text = ""

        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = True
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        funClose()
    End Sub

    Sub funClose()
        Me.Close()
    End Sub

    Private Sub frmSynchronization_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            funDelete()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub


    Private Sub rdbNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbNext.Click
        If gvSourceTables.SelectedRows.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Select any source table", Me.Text)
            Exit Sub
        Else
            For Each grow As GridViewRowInfo In gvSourceTables.SelectedRows
                gvTargetTables.Rows.AddNew()
                gvTargetTables.Rows(gvTargetTables.Rows.Count - 1).Cells(colTargetSeqNo).Value = gvTargetTables.Rows.Count
                gvTargetTables.Rows(gvTargetTables.Rows.Count - 1).Cells(colTargetSeqNo).ReadOnly = False
                gvTargetTables.Rows(gvTargetTables.Rows.Count - 1).Cells(colTargetTableName).Value = grow.Cells(colSourceTableName).Value
                Dim dt As DataTable = clsHostSettings.getPrimaryKeyDT(gvTargetTables.Rows(gvTargetTables.Rows.Count - 1).Cells(colTargetTableName).Value)
                If dt.Rows.Count > 0 Then
                    gvTargetTables.Rows(gvTargetTables.Rows.Count - 1).Cells(colTargetPK).Value = clsCommon.myCstr(dt.Rows(0).Item("column_name")) ''clsHostSettings.getPrimaryKey(gvTargetTables.Rows(gvTargetTables.Rows.Count - 1).Cells(colTargetTableName).Value)
                    If dt.Rows.Count > 1 Then
                        gvTargetTables.Rows(gvTargetTables.Rows.Count - 1).Cells(colTargetPK2).Value = clsCommon.myCstr(dt.Rows(1).Item("column_name"))
                    End If
                End If
                'gvTargetTables.Rows(gvTargetTables.Rows.Count - 1).Cells(colTargetPK).Value = clsHostSettings.getPrimaryKey(gvTargetTables.Rows(gvTargetTables.Rows.Count - 1).Cells(colTargetTableName).Value)
                gvTargetTables.Rows(gvTargetTables.Rows.Count - 1).Cells(colTargetDetail).Value = False
                gvTargetTables.Rows(gvTargetTables.Rows.Count - 1).Cells(colTargetDetail).ReadOnly = False
                grow.IsVisible = False
            Next

        End If
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        If gvTargetTables.SelectedRows.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Select any target table", Me.Text)
            Exit Sub
        Else
            For Each grow As GridViewRowInfo In gvTargetTables.SelectedRows
                gvSourceTables.Rows.AddNew()
                gvSourceTables.Rows(gvSourceTables.Rows.Count - 1).Cells(colSourceTableName).Value = gvSourceTables.Rows.Count
                grow.IsVisible = False
            Next

        End If
    End Sub

    Private Sub btnTestClient_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTestClient.Click
        Dim connStr As String = "Server=" & txtClientNameIP.Text & ";" & "Database=" & txtClientDBName.Text & ";" & "User Id=" & txtClientUserId.Text & ";" & "Password=" & txtClientPassword.Text
        TestConnection(connStr)
    End Sub
    Public Function TestConnection(ByVal connStr As String) As Boolean
        Dim cm As New SqlCommand
        Dim conn As New SqlConnection
        conn.ConnectionString = connStr
        cm.CommandText = "select Comp_Code from TSPL_COMPANY_MASTER "
        cm.Connection = conn
        Dim status As Boolean = False
        Try
            conn.Open()
            conn.Close()
            'status = cm.ExecuteNonQuery()
            clsCommon.MyMessageBoxShow(Me, "Test connection succeed", Me.Text)
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return False
    End Function


    Private Sub btnTestServer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTestServer.Click
        Dim connStr As String = "Server=" & txtServerNameIP.Text & ";" & "Database=" & txtServerDBName.Text & ";" & "User Id=" & txtServerUserId.Text & ";" & "Password=" & txtServerPassword.Text
        If TestConnection(connStr) Then
            'clsHostSettings.AddLinkedServer(txtServerNameIP.Text, txtServerUserId.Text, txtServerPassword.Text)
        End If

    End Sub

    Private Sub btnSyncClient_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSyncClient.Click
        Sync()
    End Sub
    Sub Sync(Optional ByVal _Type As String = "All")
        If AllowToSave() = False Then
            'Exit Function
            Exit Sub
        End If
        '' logging
        Dim strLog As String = ""
        Dim logFile As String = "MasterSyncLog.txt"
        If System.IO.File.Exists(logFile) Then
            Dim stream As New IO.StreamWriter(logFile, True)
            stream.WriteLine("Synchronization of master tables on date " & Now & " by " & objCommonVar.CurrentUserCode & " Type : " & _Type & ":")
            'stream.WriteLine("""""""""""""""""""""""""""""""")
            stream.Close()
        Else
            Dim fs As IO.FileStream = System.IO.File.Create(logFile)
            fs.Close()
        End If
        Dim tran As SqlTransaction = Nothing
        If chkTransaction.Checked = True Then
            tran = clsDBFuncationality.GetTransactin()
        End If
        Try
            Dim Strserver As String = "[" & txtServerNameIP.Text & "]" & "." & txtServerDBName.Text & "." & txtServerSchema.Text
            Dim strClient As String = "[" & txtClientNameIP.Text & "]" & "." & txtClientDBName.Text & "." & txtClientSchema.Text
            Dim connStr As String = "Server=" & txtServerNameIP.Text & ";" & "Database=" & txtServerDBName.Text & ";" & "User Id=" & txtServerUserId.Text & ";" & "Password=" & txtServerPassword.Text
            Dim serverconn As New SqlConnection
            serverconn.ConnectionString = connStr
            clsCommon.ProgressBarShow()
            For Each grow As GridViewRowInfo In gvTargetTables.Rows
                clsCommon.ProgressBarUpdate((grow.Index + 1) & "/" & gvTargetTables.Rows.Count)
                If clsCommon.CompairString(_Type, "All") = CompairStringResult.Equal Then
                    If grow.Cells(colTargetDetail).Value = True Then
                        clsHostSettings.SynchronizeClientDetailTable(grow.Cells(colTargetTableName).Value, grow.Cells(colTargetPK).Value, grow.Cells(colTargetPK2).Value, grow.Cells(col_COND).Value, strClient, Strserver, serverconn, tran)
                    Else
                        clsHostSettings.SynchronizeClientTable(grow.Cells(colTargetTableName).Value, grow.Cells(colTargetPK).Value, grow.Cells(colTargetPK2).Value, grow.Cells(col_COND).Value, strClient, Strserver, serverconn, tran)
                    End If

                    Dim objWriter As New System.IO.StreamWriter(logFile, True)
                    objWriter.WriteLine("" & (grow.Index + 1) & ". " & grow.Cells(colTargetTableName).Value & " done at " & Now & "")
                    objWriter.Close()
                Else
                    If clsCommon.CompairString(grow.Cells(colType).Value, _Type) = CompairStringResult.Equal Then
                        If grow.Cells(colTargetDetail).Value = True Then
                            clsHostSettings.SynchronizeClientDetailTable(grow.Cells(colTargetTableName).Value, grow.Cells(colTargetPK).Value, grow.Cells(colTargetPK2).Value, grow.Cells(col_COND).Value, strClient, Strserver, serverconn, tran)
                        Else
                            clsHostSettings.SynchronizeClientTable(grow.Cells(colTargetTableName).Value, grow.Cells(colTargetPK).Value, grow.Cells(colTargetPK2).Value, grow.Cells(col_COND).Value, strClient, Strserver, serverconn, tran)
                        End If
                        Dim objWriter As New System.IO.StreamWriter(logFile, True)
                        objWriter.WriteLine("" & (grow.Index + 1) & ". " & grow.Cells(colTargetTableName).Value & " done at " & Now & "")
                        objWriter.Close()
                    End If
                End If
            Next
            If chkTransaction.Checked = True Then
                tran.Commit()
            End If

            clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(Me, "Data Syncronized successfully", Me.Text)
            ' Ticket No : UDL/24/09/18-000222 By Prabhakar Anand
            If clsCommon.CompairString(_Type, "Price") = CompairStringResult.Equal Then
                SendSMS(Nothing, "Price synced successfully")
            End If

        Catch ex As Exception
            If chkTransaction.Checked = True Then
                tran.Rollback()
            End If

            clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Dim objWriter As New System.IO.StreamWriter(logFile, True)
            objWriter.WriteLine("Error in synchronization : " & ex.Message & " at " & Now & "")
            If clsCommon.CompairString(_Type, "Price") = CompairStringResult.Equal Then
                SendSMS(Nothing, "Price not synced")
            End If
            objWriter.Close()
        End Try

    End Sub

    Private Sub RadMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCommon.Click
        Sync("Common")
    End Sub

    Private Sub RadMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuGL.Click
        Sync("GL")
    End Sub

    Private Sub RadMenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEmployee.Click
        Sync("Employee")
    End Sub

    Private Sub RadMenuItem6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuVendor.Click
        Sync("Vendor")
    End Sub

    Private Sub RadMenuItem7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuVLC.Click
        Sync("VLC")
    End Sub

    Private Sub RadMenuItem8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMCC.Click
        Sync("MCC")
    End Sub

    Private Sub RadMenuItem9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPrice.Click
        Sync("Price")
    End Sub

    'Private Sub btnSyncServer_Click(sender As Object, e As EventArgs) Handles btnSyncServer.Click
    '    SynchronizeServer()
    'End Sub
    'Sub SynchronizeServer()
    '    '' logging
    '    Dim strLog As String = ""
    '    Dim logFile As String = "ServerSyncLog.txt"
    '    If System.IO.File.Exists(logFile) Then
    '        Dim stream As New IO.StreamWriter(logFile, True)
    '        stream.Flush()
    '        stream.WriteLine("Synchronization of Transaction tables on date " & Now & " by " & objCommonVar.CurrentUserCode & ":")
    '        'stream.WriteLine("""""""""""""""""""""""""""""""")
    '        stream.Close()
    '    Else
    '        Dim fs As IO.FileStream = System.IO.File.Create(logFile)
    '        fs.Close()
    '    End If
    '    Try
    '        Dim Strserver As String = "[" & txtServerNameIP.Text & "]" & "." & txtServerDBName.Text & "." & txtServerSchema.Text
    '        Dim strClient As String = "[" & txtClientNameIP.Text & "]" & "." & txtClientDBName.Text & "." & txtClientSchema.Text
    '        Dim connStr As String = "Server=" & txtServerNameIP.Text & ";" & "Database=" & txtServerDBName.Text & ";" & "User Id=" & txtServerUserId.Text & ";" & "Password=" & txtServerPassword.Text
    '        Dim serverconn As New SqlConnection
    '        serverconn.ConnectionString = connStr
    '        clsCommon.ProgressBarShow()
    '        clsHostSettings.SynchronizeServer(strClient, Strserver, serverconn, Nothing)

    '        clsCommon.ProgressBarHide()
    '        clsCommon.MyMessageBoxShow("Data Syncronized successfully")
    '    Catch ex As Exception
    '        clsCommon.ProgressBarHide()
    '        clsCommon.MyMessageBoxShow(ex.Message)
    '        Dim objWriter As New System.IO.StreamWriter(logFile, True)
    '        objWriter.WriteLine("Error in synchronization : " & ex.Message & " at " & Now & "")
    '        objWriter.Close()
    '    End Try

    'End Sub

    Private Sub BtnSetTables_Click(sender As Object, e As EventArgs) Handles BtnSetTables.Click
        If AllowToSave() = False Then
            Exit Sub
        End If
        Dim _Type As String = "All"
        '' logging
        Dim strLog As String = ""
        Dim logFile As String = "MasterSyncLog.txt"
        If System.IO.File.Exists(logFile) Then
            Dim stream As New IO.StreamWriter(logFile, True)
            stream.WriteLine("Synchronization of master tables on date " & Now & " by " & objCommonVar.CurrentUserCode & " Type : " & _Type & ":")
            'stream.WriteLine("""""""""""""""""""""""""""""""")
            stream.Close()
        Else
            Dim fs As IO.FileStream = System.IO.File.Create(logFile)
            fs.Close()
        End If
        Try
            Dim Strserver As String = "[" & txtServerNameIP.Text & "]" & "." & txtServerDBName.Text '& "." & txtServerSchema.Text
            Dim strClient As String = "[" & txtClientNameIP.Text & "]" & "." & txtClientDBName.Text '& "." & txtClientSchema.Text
            Dim connStr As String = "Server=" & txtServerNameIP.Text & ";" & "Database=" & txtServerDBName.Text & ";" & "User Id=" & txtServerUserId.Text & ";" & "Password=" & txtServerPassword.Text
            Dim serverconn As New SqlConnection
            serverconn.ConnectionString = connStr
            clsCommon.ProgressBarShow()

            For Each grow As GridViewRowInfo In gvTargetTables.Rows
                clsCommon.ProgressBarUpdate((grow.Index + 1) & "/" & gvTargetTables.Rows.Count)
                If clsCommon.CompairString(_Type, "All") = CompairStringResult.Equal Then
                    ' If grow.Cells(colTargetDetail).Value = True Then
                    clsHostSettings.SynchronizeClientTableScheme(grow.Cells(colTargetTableName).Value, grow.Cells(colTargetPK).Value, grow.Cells(colTargetPK2).Value, grow.Cells(col_COND).Value, strClient, Strserver, serverconn)
                    'Else
                    '    clsHostSettings.SynchronizeClientTable(grow.Cells(colTargetTableName).Value, grow.Cells(colTargetPK).Value, grow.Cells(colTargetPK2).Value, grow.Cells(col_COND).Value, strClient, Strserver, serverconn)
                    'End If

                    Dim objWriter As New System.IO.StreamWriter(logFile, True)
                    objWriter.WriteLine("" & (grow.Index + 1) & ". " & grow.Cells(colTargetTableName).Value & " done at " & Now & "")
                    objWriter.Close()
                Else
                    If clsCommon.CompairString(grow.Cells(colType).Value, _Type) = CompairStringResult.Equal Then
                        'If grow.Cells(colTargetDetail).Value = True Then
                        clsHostSettings.SynchronizeClientTableScheme(grow.Cells(colTargetTableName).Value, grow.Cells(colTargetPK).Value, grow.Cells(colTargetPK2).Value, grow.Cells(col_COND).Value, strClient, Strserver, serverconn)
                        'Else
                        '    clsHostSettings.SynchronizeClientTable(grow.Cells(colTargetTableName).Value, grow.Cells(colTargetPK).Value, grow.Cells(colTargetPK2).Value, grow.Cells(col_COND).Value, strClient, Strserver, serverconn)
                        'End If
                        Dim objWriter As New System.IO.StreamWriter(logFile, True)
                        objWriter.WriteLine("" & (grow.Index + 1) & ". " & grow.Cells(colTargetTableName).Value & " done at " & Now & "")
                        objWriter.Close()
                    End If


                End If

            Next
            clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(Me, "Data Syncronized successfully", Me.Text)
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Dim objWriter As New System.IO.StreamWriter(logFile, True)
            objWriter.WriteLine("Error in synchronization : " & ex.Message & " at " & Now & "")
            objWriter.Close()
        End Try

    End Sub

    Private Sub btnNewTables_Click(sender As Object, e As EventArgs) Handles btnNewTables.Click
        Dim obj As New clsSyncTables
        Try
            Dim intCount As Integer = obj.UpdateNewTables(Nothing)
            LoadTargetTables()
            clsCommon.MyMessageBoxShow(Me, "" & intCount & " tables updated successfully", Me.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Public Sub SendSMS(ByVal trans As SqlTransaction, ByVal msg As String)
        Dim dtSMSEmail As DataTable = clsDBFuncationality.GetDataTable("SELECT SMS_Text,EMail_Text from TSPL_ES_Content where Form_ID='" + "'", trans)
        Dim strSMSContent As String = ""
        If dtSMSEmail.Rows.Count > 0 Then
            strSMSContent = clsCommon.myCstr(dtSMSEmail.Rows(0).Item("SMS_Text"))
        End If
        'SMSCode Start
        If clsCommon.myLen(strSMSContent) > 0 Then
            Dim objSMSH As New clsSMSHead()
            objSMSH.SMS_Text = strSMSContent
            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Synchroniz_By_User, clsCommon.myCstr(objCommonVar.CurrentUser))
            Dim strLocation As String = clsCommon.myCstr(clsLocation.GetName(clsGateEntry.getUsersDefaultLocation(), Nothing))
            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.MCCName, strLocation)
            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Synchroniz_Date, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            'objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Synchronization_Msg, clsCommon.myCstr(msg))
            objSMSH.SMS_Text = msg + Environment.NewLine + objSMSH.SMS_Text
            CreateSMSContent(objSMSH.SMS_Text, trans)
        End If

    End Sub
    Public Shared Sub CreateSMSContent(ByVal strSMSContent As String, ByVal trans As SqlTransaction)
        If clsCommon.myLen(strSMSContent) > 0 Then
            Dim objSMSH As New clsSMSHead()
            objSMSH.SMS_Text = strSMSContent
            objSMSH.arrMobilNo = New List(Of String)()
            objSMSH.SaveData("", objSMSH, trans)
            objSMSH = Nothing
        End If
    End Sub

End Class
