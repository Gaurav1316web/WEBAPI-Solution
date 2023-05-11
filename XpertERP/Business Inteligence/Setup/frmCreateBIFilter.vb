Imports common
Imports System.Data.SqlClient
Imports ActiveDatabaseSoftware.ActiveQueryBuilder
Imports System.IO
Imports Telerik.Charting
Imports Telerik.WinControls.UI

Imports System.Text
Imports Telerik.Pivot.Core

Public Class frmCreateBIFilter
    Inherits FrmMainTranScreen
#Region "Variables"
    Const colSelect As String = "colSelect"
    Const colFilter As String = "colFilter"

    Dim isNewEntry As Boolean = False
    Dim ConnString As String = ""
    Dim currLayout As Stream
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isShowAllReport As Boolean = False
#End Region

    Private Sub RadForm1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+A Add New")
        ResetQueryBuilder()
        LoadType()
        LoadLevel()
        AddNew()
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.BICreateFilter)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub LoadLevel()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("code", GetType(Integer))

        For index As Integer = 0 To 10
            Dim dr As DataRow = dt.NewRow()
            dr("Code") = index
            dt.Rows.Add(dr)
        Next

        cboLevel.DataSource = dt
        cboLevel.ValueMember = "Code"
        'cboLevel.DisplayMember = "Name"
    End Sub

    Sub LoadType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "G"
        dr("Name") = "Check box Grid"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "T"
        dr("Name") = "Check box Tree"
        dt.Rows.Add(dr)

        cboType.DataSource = dt
        cboType.ValueMember = "Code"
        cboType.DisplayMember = "Name"
    End Sub

    Private Sub SetConnection()
        Try
            Dim line As String
            Dim objReader As New System.IO.StreamReader("config.Txp")
            Do While objReader.Peek() <> -1
                line = objReader.ReadLine()
                clsDBFuncationality.SetConnectionEncryptFormat(line)
                ConnString = clsDBFuncationality.connectionString
            Loop
            ''stuti regarding memory leakage
            objReader.Close()
            objReader.Dispose()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try

        MssqlMetadataProvider1.Connection = New SqlConnection(ConnString)

        ' setup the query builder with metadata and syntax providers
        queryBuilder.MetadataProvider = MssqlMetadataProvider1
        queryBuilder.SyntaxProvider = MssqlSyntaxProvider1

        ' kick the query builder to fill metadata tree
        queryBuilder.InitializeDatabaseSchemaTree()

    End Sub

    Public Sub ResetQueryBuilder()
        queryBuilder.MetadataContainer.Items.Clear()
        queryBuilder.MetadataProvider = Nothing
        queryBuilder.SyntaxProvider = Nothing
        queryBuilder.OfflineMode = False
        SetConnection()
    End Sub

    Sub SetControlByQuery()
        Try
            ' Update the query builder with manually edited query text
            If clsCommon.myLen(sqlQueryText.Text) <= 0 Then
                ResetQueryBuilder()
            Else
                queryBuilder.SQL = sqlQueryText.Text
            End If
        Catch ex As SQLParsingException
            ' Set caret to error position
            sqlQueryText.SelectionStart = ex.ErrorPos.pos
            ' Report error
            MessageBox.Show(ex.Message, "Parsing error")
        Catch ex1 As Exception
            clsCommon.MyMessageBoxShow(ex1.Message)
        End Try
    End Sub

    Private Sub MyTextBox1_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)
        SetControlByQuery()
    End Sub

    Private Sub plainTextSQLBuilder1_SQLUpdated(ByVal sender As Object, ByVal e As EventArgs) Handles PlainTextSQLBuilder1.SQLUpdated
        ' Handle the event raised by SQL Builder object that the text of SQL query is changed

        ' update the text box
        sqlQueryText.Text = PlainTextSQLBuilder1.SQL
    End Sub

    Private Sub RadPageView1_SelectedPageChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadPageView1.SelectedPageChanged

        Try
            If RadPageView1.SelectedPage Is RadPageView1.Pages("RadPageViewPage2") AndAlso clsCommon.myLen(sqlQueryText.Text) > 0 Then
                gv1.DataSource = Nothing
                gv1.Columns.Clear()
                gv1.Rows.Clear()
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(sqlQueryText.Text)
                gv1.DataSource = dt
                gv1.TableElement.TableHeaderHeight = 30
                gv1.MasterTemplate.ShowRowHeaderColumn = False
                For ii As Integer = 0 To gv1.Columns.Count - 1
                    gv1.Columns(ii).ReadOnly = True
                Next
                gv1.AllowAddNewRow = False
                gv1.ShowGroupPanel = True
                gv1.BestFitColumns()
            ElseIf RadPageView1.SelectedPage Is RadPageView1.Pages("RadPageViewPage6") AndAlso clsCommon.myLen(sqlQueryText.Text) > 0 Then
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(sqlQueryText.Text)
                If dt IsNot Nothing AndAlso dt.Columns.Count > 0 Then
                    Dim arrAdded As New List(Of String)
                    For Each dc As DataColumn In dt.Columns
                        Dim isFound As Boolean = False
                        For jj As Integer = 0 To gvFilter.Rows.Count - 1
                            If clsCommon.CompairString(clsCommon.myCstr(gvFilter.Rows(jj).Cells(colFilter).Value), dc.ColumnName) = CompairStringResult.Equal Then
                                isFound = True
                                arrAdded.Add(dc.ColumnName.ToUpper())
                                Exit For
                            End If
                        Next
                        If Not isFound Then
                            gvFilter.Rows.AddNew()
                            gvFilter.Rows(gvFilter.RowCount - 1).Cells(colFilter).Value = dc.ColumnName
                            arrAdded.Add(dc.ColumnName.ToUpper())
                        End If
                    Next
                    For jj As Integer = gvFilter.Rows.Count - 1 To 0 Step -1
                        If Not arrAdded.Contains(clsCommon.myCstr(gvFilter.Rows(jj).Cells(colFilter).Value).ToUpper()) Then
                            gvFilter.Rows.RemoveAt(jj)
                        End If
                    Next
                End If
            ElseIf RadPageView1.SelectedPage Is RadPageView1.Pages("RadPageViewPage3") AndAlso clsCommon.myLen(sqlQueryText.Text) > 0 Then
                cbt.DataSource = Nothing
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(sqlQueryText.Text)
                If dt IsNot Nothing AndAlso dt.Columns.Count > 0 Then
                    Dim arrAdded As New List(Of String)
                    For jj As Integer = 0 To gvFilter.Rows.Count - 1
                        If clsCommon.myCdbl(gvFilter.Rows(jj).Cells(colSelect).Value) = 1 Then
                            cbt.ValueMember = clsCommon.myCstr(gvFilter.Rows(jj).Cells(colFilter).Value)
                        ElseIf clsCommon.myCdbl(gvFilter.Rows(jj).Cells(colSelect).Value) = 2 Then
                            cbt.DisplayMember = clsCommon.myCstr(gvFilter.Rows(jj).Cells(colFilter).Value)
                        ElseIf clsCommon.myCdbl(gvFilter.Rows(jj).Cells(colSelect).Value) = 3 Then
                            cbt.ParentValue = clsCommon.myCstr(gvFilter.Rows(jj).Cells(colFilter).Value)
                        End If
                    Next
                    cbt.DataSource = dt
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub

    Sub AddNew()
        isNewEntry = True
        btnSave.Text = "Save"
        txtCode.Value = ""
        txtDesc.Text = ""
        sqlQueryText.Text = ""
        queryBuilder.SQL = sqlQueryText.Text
        ResetQueryBuilder()
        LoadBlankGrid()
        RadPageView1.SelectedPage = RadPageView1.Pages("RadPageViewPage1")
        cboType.SelectedValue = "G"
    End Sub

    Sub LoadBlankGrid()
        gvFilter.Rows.Clear()
        gvFilter.Columns.Clear()

        Dim repoFilterColumn As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoFilterColumn.FormatString = ""
        repoFilterColumn.HeaderText = "Column"
        repoFilterColumn.Name = colFilter
        repoFilterColumn.ReadOnly = True
        repoFilterColumn.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoFilterColumn.TextImageRelation = TextImageRelation.TextBeforeImage
        repoFilterColumn.Width = 200
        gvFilter.MasterTemplate.Columns.Add(repoFilterColumn)

        Dim repoRowType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoRowType.FormatString = ""
        repoRowType.HeaderText = "Type"
        repoRowType.Name = colSelect
        repoRowType.Width = 150
        repoRowType.ReadOnly = False
        repoRowType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoRowType.DataSource = GetRowType()
        repoRowType.ValueMember = "Code"
        repoRowType.DisplayMember = "Name"
        gvFilter.MasterTemplate.Columns.Add(repoRowType)

        gvFilter.AllowDeleteRow = True
        gvFilter.AllowAddNewRow = False
        gvFilter.ShowGroupPanel = False
        gvFilter.AllowColumnReorder = False
        gvFilter.AllowRowReorder = False
        gvFilter.EnableSorting = False
        gvFilter.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvFilter.MasterTemplate.ShowRowHeaderColumn = False
        gvFilter.TableElement.TableHeaderHeight = 40
    End Sub

    Private Function GetRowType() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(Integer))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = 0
        dr("Name") = " "
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = 1
        dr("Name") = "Value Member"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = 2
        dr("Name") = "Display Member"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = 3
        dr("Name") = "Parent Member"
        dt.Rows.Add(dr)

        Return dt
    End Function

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            AddNew()
            Dim obj As New clsCreateBIFilter()
            obj = clsCreateBIFilter.GetData(strCode, isShowAllReport, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
                isNewEntry = False
                txtCode.Value = obj.Code
                chkSecurityLocation.Checked = obj.Is_Security_Location
                txtDesc.Text = obj.Description
                sqlQueryText.Text = obj.Qry
                cboType.SelectedValue = obj.Filter_Type
                If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "T") = CompairStringResult.Equal Then
                    cboLevel.SelectedValue = obj.Tree_Level
                End If
                SetControlByQuery()
                If obj.arr IsNot Nothing AndAlso obj.arr.Count > 0 Then
                    For Each objtr As clsCreateBIFilterDetails In obj.arr
                        gvFilter.Rows.AddNew()
                        gvFilter.Rows(gvFilter.RowCount - 1).Cells(colSelect).Value = objtr.Is_Select
                        gvFilter.Rows(gvFilter.RowCount - 1).Cells(colFilter).Value = objtr.Filter_Column
                    Next
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Sub reset()
        txtCode.Value = ""
        txtDesc.Text = ""
        chkSecurityLocation.Checked = False
        sqlQueryText.Text = ""
        gvFilter.Rows.Clear()
        gvFilter.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        cboLevel.SelectedValue = 0
    End Sub

    Sub SaveData()
        Try
            If AllowToSave() Then
                Dim obj As New clsCreateBIFilter()
                obj.Code = txtCode.Value
                obj.Is_Security_Location = chkSecurityLocation.Checked
                obj.Description = txtDesc.Text
                obj.Qry = sqlQueryText.Text
                obj.Filter_Type = clsCommon.myCstr(cboType.SelectedValue)
                If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "T") = CompairStringResult.Equal Then
                    obj.Tree_Level = clsCommon.myCdbl(cboLevel.SelectedValue)
                End If


                obj.arr = New List(Of clsCreateBIFilterDetails)
                For ii As Integer = 0 To gvFilter.RowCount - 1
                    Dim objtr As New clsCreateBIFilterDetails
                    objtr.Is_Select = clsCommon.myCdbl(gvFilter.Rows(ii).Cells(colSelect).Value)
                    objtr.Filter_Column = clsCommon.myCstr(gvFilter.Rows(ii).Cells(colFilter).Value)
                    obj.arr.Add(objtr)
                Next
                obj.SaveData(obj, isNewEntry)
                clsCommon.MyMessageBoxShow("Data saved successfully", Me.Text)
                LoadData(obj.Code, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Function AllowToSave() As Boolean
        If clsCommon.myLen(txtDesc.Text) <= 0 Then
            txtDesc.Focus()
            Throw New Exception("Please enter description")
        End If
        If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "T") = CompairStringResult.Equal Then
            If clsCommon.myCdbl(cboLevel.SelectedValue) = 0 Then
                Throw New Exception("Please enter levels of tree")
            End If
        End If
        Return True
    End Function

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Dim qry As String = "select Code,Description from TSPL_CREATE_BI_FILTER"
        Dim whrclas As String = ""
        If Not isShowAllReport Then
            whrclas += "  TSPL_CREATE_BI_FILTER.Is_Create_By_Developer = 0"
        End If
        LoadData(clsCommon.ShowSelectForm("FCreateBIRpt", qry, "Code", whrclas, txtCode.Value, "", isButtonClicked), NavigatorType.Current)
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub

    Sub CloseForm()
        Me.Close()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        Try
            If clsCommon.myLen(txtCode.Value) > 0 Then
                If clsCommon.MyMessageBoxShow("Delete the current data" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                    clsCreateBIFilter.DeleteData(txtCode.Value)
                    AddNew()
                    MDI.LoadMenu()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FRM_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.A Then
            AddNew()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            CloseForm()
        ElseIf e.Alt And e.KeyCode = Keys.X Then
            'Dim frm As New FrmBIReport
            'frm.obj = clsCreateBIFilter.GetData(txtCode.Value, True, NavigatorType.Current)
            'frm.Show()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = "SIRC"
            frm.strCode = "SIReversAndCreate"
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                isShowAllReport = Not isShowAllReport
                RadButton2.Visible = isShowAllReport
            End If
        End If
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton2.Click
        If clsCommon.myLen(txtCode.Value) > 0 Then

            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select * from TSPL_CREATE_BI_FILTER where code='" + txtCode.Value + "'")
            Dim builder As New StringBuilder
            builder.AppendLine("Private Shared Function FunctionName( )" + Environment.NewLine + "Dim obj As New clsCreateBIFilter()")

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For ii As Integer = 0 To dt.Columns.Count - 1
                    If clsCommon.CompairString(dt.Columns(ii).ColumnName, "Layout") = CompairStringResult.Equal Then
                        builder.AppendLine("Dim strGridLayout As String =""" + clsCommon.myCstr(dt.Rows(0)(ii)).Replace("""", """""") + """")
                        builder.AppendLine("Dim byteArray As Byte() = Encoding.ASCII.GetBytes(strGridLayout)")
                        builder.AppendLine("obj.Layout = New MemoryStream(byteArray)")
                    ElseIf clsCommon.CompairString(dt.Columns(ii).ColumnName, "Is_Create_By_Developer") = CompairStringResult.Equal Then
                        builder.AppendLine("obj." + clsCommon.myCstr(dt.Columns(ii).ColumnName) + "=1")
                    ElseIf clsCommon.CompairString(dt.Columns(ii).ColumnName, "Created_By") = CompairStringResult.Equal OrElse clsCommon.CompairString(dt.Columns(ii).ColumnName, "Created_Date") = CompairStringResult.Equal OrElse clsCommon.CompairString(dt.Columns(ii).ColumnName, "Modify_By") = CompairStringResult.Equal OrElse clsCommon.CompairString(dt.Columns(ii).ColumnName, "Modify_Date") = CompairStringResult.Equal OrElse clsCommon.CompairString(dt.Columns(ii).ColumnName, "Comp_Code") = CompairStringResult.Equal Then

                    Else
                        builder.AppendLine("obj." + clsCommon.myCstr(dt.Columns(ii).ColumnName) + "=""" + clsCommon.myCstr(dt.Rows(0)(ii)).Replace(vbCr, "").Replace(vbLf, "") + """")
                    End If
                Next

                dt = clsDBFuncationality.GetDataTable("select * from TSPL_CREATE_BI_FILTER_DETAIL where code='" + txtCode.Value + "' ")
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    builder.AppendLine("obj.arr = New List(Of clsCreateBIFilterDetails)")
                    builder.AppendLine(" Dim objtr As clsCreateBIFilterDetails")
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        builder.AppendLine("objtr = New clsCreateBIFilterDetails")
                        For jj As Integer = 0 To dt.Columns.Count - 1
                            builder.Append("objtr." + clsCommon.myCstr(dt.Columns(jj).ColumnName) + "=""" + clsCommon.myCstr(dt.Rows(ii)(jj)) + """" + Environment.NewLine)
                        Next
                        builder.AppendLine("obj.arr.Add(objtr)")
                    Next
                End If
                builder.AppendLine("obj.SaveData(obj, False, True)")
                builder.AppendLine("End Function")
            End If
            Dim frm As New FrmFreeTxtBox1()
            frm.WindowState = FormWindowState.Maximized
            frm.strRmks = builder.ToString()

            frm.ShowDialog()
            frm.Dispose()
        End If
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset()
    End Sub

    
    Private Sub cboType_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboType.SelectedValueChanged
        Try
            If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "T") = CompairStringResult.Equal Then
                RadPageView1.Pages("RadPageViewPage2").Item.Visibility = ElementVisibility.Collapsed
                RadPageView1.Pages("RadPageViewPage3").Item.Visibility = ElementVisibility.Visible
                cboLevel.Visible = True
                MyLabel2.Visible = True
            Else
                RadPageView1.Pages("RadPageViewPage2").Item.Visibility = ElementVisibility.Visible
                RadPageView1.Pages("RadPageViewPage3").Item.Visibility = ElementVisibility.Collapsed
                cboLevel.Visible = False
                MyLabel2.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub sqlQueryText_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles sqlQueryText.Validating
        SetControlByQuery()
    End Sub
End Class
