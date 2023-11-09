Imports common
Public Class FrmCreateDashBoard
    Inherits FrmMainTranScreen
#Region "Variables"
    Const colCode As String = "COLCODE"
    Const colName As String = "COLNAME"
    Public isInsideLoadData As Boolean = False
    Public isCellValueChangedOpen As Boolean = False
    Private isNewEntry As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()
#End Region

   Private Sub FrmCreateDashBoard_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
    End Sub

    Public Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.BICreateDashBoard)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoTextBox As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Code"
        repoTextBox.Name = colCode
        repoTextBox.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoTextBox.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTextBox.Width = 100
        repoTextBox.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Description"
        repoTextBox.Name = colName
        repoTextBox.Width = 150
        repoTextBox.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = True
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.AutoSizeRows = False

    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub

    Sub AddNew()
        BlankAllControls()
        LoadBlankGrid()
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = True
        gv1.Rows.AddNew()
    End Sub

    Sub BlankAllControls()
        txtDocNo.Value = ""
        txtDesc.Text = ""
        txtReportModule.Value = ""
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(colCode) Then
                        OpenReport(False)
                        isCellValueChangedOpen = False
                    End If
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub OpenReport(ByVal isButtonClick As Boolean)
        Dim qry As String = "select Code,Description from TSPL_CREATE_BI_REPORT"
        Dim whrCls As String = "is_For_Dashboard='1'"
        gv1.CurrentRow.Cells(colCode).Value = clsCommon.ShowSelectForm("CRBIRPT", qry, "Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colCode).Value), "Code", isButtonClick)
        gv1.CurrentRow.Cells(colName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_CREATE_BI_REPORT where Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colCode).Value) + "'"))
    End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub

    Function AllowToSave() As Boolean
        If clsCommon.myLen(txtDesc.Text) <= 0 Then
            txtDesc.Focus()
            Throw New Exception("Please enter description")
        End If
        If clsCommon.myLen(txtReportModule.Value) <= 0 Then
            txtReportModule.Focus()
            Throw New Exception("Please enter Module")
        End If
        Return True
    End Function

    Public Function SaveData() As Boolean
        Dim obj As clsCreateDashboard = Nothing
        Dim objTr As clsCreateDashboardDetails = Nothing
        Try
            If (AllowToSave()) Then
                obj = New clsCreateDashboard()
                obj.Code = txtDocNo.Value
                obj.Description = txtDesc.Text
                obj.Report_Module = txtReportModule.Value
                obj.arr = New List(Of clsCreateDashboardDetails)
                For Each grow As GridViewRowInfo In gv1.Rows
                    objTr = New clsCreateDashboardDetails()
                    objTr.Report_Code = clsCommon.myCstr(grow.Cells(colCode).Value)
                    If (clsCommon.myLen(objTr.Report_Code) > 0) Then
                        obj.arr.Add(objTr)
                    End If
                Next
                If (obj.arr Is Nothing OrElse obj.arr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please Fill at list one report", Me.Text)
                    Return False
                End If

                If obj.SaveData(obj, isNewEntry) Then
                    clsCommon.MyMessageBoxShow(Me, "Data saved Successfully", Me.Text)
                    LoadData(obj.Code, NavigatorType.Current)
                    MDI.LoadImageList()
                    MDI.LoadMenu()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing
            objTr = Nothing
        End Try
    End Function

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As clsCreateDashboard = Nothing
        isInsideLoadData = True
        Try
            isNewEntry = False
            btnSave.Text = "Update"
            BlankAllControls()
            LoadBlankGrid()
            obj = clsCreateDashboard.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
                txtDocNo.Value = obj.Code
                txtDesc.Text = obj.Description
                txtReportModule.Value = obj.Report_Module
                If obj.arr IsNot Nothing AndAlso obj.arr.Count > 0 Then
                    For Each objTr As clsCreateDashboardDetails In obj.arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCode).Value = objTr.Report_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colName).Value = objTr.Report_Description
                    Next
                End If
            End If
            gv1.Rows.AddNew()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub FrmCreateDashBoard_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F2 AndAlso gv1.CurrentCell IsNot Nothing Then
            isCellValueChangedOpen = True
            If gv1.CurrentColumn Is gv1.Columns(colCode) Then
                gv1.CurrentColumn = gv1.Columns(colName)
                OpenReport(True)
                gv1.CurrentColumn = gv1.Columns(colCode)
            End If
            isCellValueChangedOpen = False
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        End If
    End Sub

    Sub DeleteData()
        Try
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                If (myMessages.deleteConfirm()) Then
                    If (clsCreateDashboard.DeleteData(txtDocNo.Value)) Then
                        common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                        AddNew()
                        MDI.LoadImageList()
                        MDI.LoadMenu()
                    End If
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Sub CloseForm()
        clsERPFuncationality.closeForm(Me)
    End Sub

    Private Sub txtReportModule__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtReportModule._MYValidating
        Try
            Dim qry As String = "select TSPL_PROGRAM_MASTER.Program_Code as ReportModule,TSPL_PROGRAM_MASTER.Program_Name as ReportModuleDescription, TSPL_PROGRAM_MASTER.Parent_Code as ModuleCode,OutTabale.Program_Name as ModuleName from TSPL_PROGRAM_MASTER "
            qry += " inner join TSPL_PROGRAM_MASTER as OutTabale on OutTabale.Program_Code=TSPL_PROGRAM_MASTER.Parent_Code"
            qry += " inner join tspl_Module_Permission on tspl_Module_Permission.Module_Name= TSPL_PROGRAM_MASTER.Parent_Code"
            Dim WhrCls As String = " tspl_Module_Permission.IsAvailable=1 and TSPL_PROGRAM_MASTER.Type='SM' and TSPL_PROGRAM_MASTER.PROGRAM_NAME like '%Report%'  "
            txtReportModule.Value = clsCommon.ShowSelectForm("ReportModule1", qry, "ReportModule", WhrCls, txtReportModule.Value, "", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub

    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Try
            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDocNo._MYValidating
        Dim qry As String = "select Code,Description  from TSPL_CREATE_DASHBOARD"
        Dim whrclas As String = ""
        LoadData(clsCommon.ShowSelectForm("CreateMyDB", qry, "Code", whrclas, txtDocNo.Value, "", isButtonClicked), NavigatorType.Current)
    End Sub
End Class
