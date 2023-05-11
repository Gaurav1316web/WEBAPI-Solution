Imports common
Imports System.Data.SqlClient
Imports System.IO

Public Class FrmBranchAccountMapping
    Inherits FrmMainTranScreen
#Region "Variables"
    Const ColFromLocation As String = "From Location"
    Const ColFromLocationName As String = "From Location Name"
    Const ColToLocation As String = "To Location"
    Const ColToLocationName As String = "To Location Name"
    Const ColBranchAccount As String = "Branch Account"
    Const ColBranchAccountName As String = "Branch Account Name"

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim userCode, companyCode As String
    Dim IsLoadData As Boolean = False
#End Region

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmBranchAccountMapping)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnSave.Visible = MyBase.isModifyFlag

        'If btnSave.Visible = True Then
        '    RmExport.Enabled = True
        '    RmImport.Enabled = True
        'Else
        '    RmExport.Enabled = False
        '    RmImport.Enabled = False
        'End If

        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Function AllowToSave() As Boolean

        ''Handle in save data class function done by balwinder on 03/04/2017 for optimize.

        'Dim GridRowQual As Integer = 0
        'Dim HighQual As Integer = 0
        'Dim FromLoc As String = ""
        'Dim ToLoc As String = ""
        'Dim BranchAcc As String = ""
        'Dim BranchSeg As String = ""
        'Dim lineno As Integer = 0
        'For Each grow As GridViewRowInfo In gv.Rows
        '    'If clsCommon.myLen(grow.Cells(ColUnvClg).Value) <= 0 Then
        '    '    Continue For
        '    'End If
        '    lineno += 1
        '    FromLoc = clsCommon.myCstr(grow.Cells(ColFromLocation).Value)
        '    ToLoc = clsCommon.myCstr(grow.Cells(ColToLocation).Value)
        '    BranchAcc = clsCommon.myCstr(grow.Cells(ColBranchAccount).Value)
        '    If clsCommon.myLen(FromLoc) > 0 Then
        '        Dim qry As String = "select Count(Segment_code) as Code from TSPL_GL_SEGMENT_CODE left outer join TSPL_LOCATION_MASTER on TSPL_GL_SEGMENT_CODE.Segment_code =TSPL_LOCATION_MASTER.Loc_Segment_Code WHERE Segment_code ='" + FromLoc + "' AND Seg_No = '7' AND GIT='N'"
        '        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
        '        If check <= 0 Then
        '            clsCommon.MyMessageBoxShow("From location code '" & FromLoc & "' does not exists at line no. " + clsCommon.myCstr(lineno) + ".")
        '            Return False
        '        End If
        '    End If
        '    If clsCommon.myLen(FromLoc) > 0 Then
        '        If clsCommon.myLen(ToLoc) > 0 Then
        '            Dim qry As String = "select Count(Segment_code) as Code from TSPL_GL_SEGMENT_CODE left outer join TSPL_LOCATION_MASTER on TSPL_GL_SEGMENT_CODE.Segment_code =TSPL_LOCATION_MASTER.Loc_Segment_Code WHERE Segment_code ='" + ToLoc + "' AND Seg_No = '7' AND GIT='N'"
        '            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
        '            If check <= 0 Then
        '                clsCommon.MyMessageBoxShow("To location code '" & ToLoc & "' does not exists at line no. " + clsCommon.myCstr(lineno) + ".")
        '                Return False
        '            End If
        '        Else
        '            clsCommon.MyMessageBoxShow("To location code can not be left blank at line no. " + clsCommon.myCstr(lineno) + ".")
        '            Return False
        '        End If
        '        If clsCommon.CompairString(FromLoc, ToLoc) = CompairStringResult.Equal Then
        '            clsCommon.MyMessageBoxShow("From location and To location can not be same at line no. " + clsCommon.myCstr(lineno) + ".")
        '            Return False
        '        End If
        '    End If
        '    If clsCommon.myLen(FromLoc) > 0 AndAlso clsCommon.myLen(ToLoc) > 0 Then
        '        If clsCommon.myLen(BranchAcc) <= 0 Then
        '            clsCommon.MyMessageBoxShow("Branch account can not be left blank at line no. " + clsCommon.myCstr(lineno) + ".")
        '            Return False
        '        End If
        '    End If
        '    If clsCommon.myLen(FromLoc) > 0 AndAlso clsCommon.myLen(BranchAcc) > 0 Then
        '        BranchSeg = clsDBFuncationality.getSingleValue("select ISNULL(Account_Seg_Code7,'') As Account_Seg_Code7 from TSPL_GL_ACCOUNTS Where Account_Code='" & clsCommon.myCstr(BranchAcc) & "'")
        '        If clsCommon.CompairString(BranchSeg, FromLoc) <> CompairStringResult.Equal Then
        '            clsCommon.MyMessageBoxShow("Please check ! branch account (" & BranchAcc & ") does not exists with from loction (" & FromLoc & ") at line no. " + clsCommon.myCstr(lineno) + ".")
        '            Return False
        '        End If
        '    End If

        'Next
        Return True
    End Function
    'Private Sub DeleteData()
    '    Try
    '        If (deleteConfirm()) Then
    '            If (ClsBranchAccountMapping.DeleteData(fndCode.Value)) Then
    '                common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
    '                funReset()
    '            End If
    '        End If
    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub
    Sub funReset()
        isNewEntry = True

        btnSave.Text = "Save"
        btnSave.Enabled = True
        'btnDelete.Enabled = False
    End Sub

    'Public Function SaveData()
    '    If AllowToSave() Then
    '        Dim obj As New ClsBranchAccountMapping()
    '        obj.Branch_Account_Map_Code = clsCommon.myCstr(fndCode.Value)
    '        obj.Branch_Account = clsCommon.myCstr(txtBranchAcc.Value)
    '        obj.From_Location = clsCommon.myCstr(txtFromLocation.Value)
    '        obj.To_Location = clsCommon.myCstr(txtToLocation.Value)
    '        If (obj.SaveData(obj, isNewEntry)) Then
    '            common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
    '            LoadData(obj.Branch_Account_Map_Code, NavigatorType.Current)
    '        End If
    '    End If
    'End Function
    Sub funClose()
        Me.Close()
        GC.Collect()
    End Sub
    Sub OpenFromLoc(ByVal isButtonClick As Boolean)
        If gv.CurrentRow.Index >= 0 Then
            Dim qry As String = "select distinct(Segment_code) as Code ,Description  from TSPL_GL_SEGMENT_CODE left outer join TSPL_LOCATION_MASTER on TSPL_GL_SEGMENT_CODE.Segment_code =TSPL_LOCATION_MASTER.Loc_Segment_Code "
            Dim WhrCls As String = "Seg_No = '7' AND GIT='N'"
            'If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            '    WhrCls += "  and  TSPL_LOCATION_MASTER.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            'End If
            gv.CurrentRow.Cells(ColFromLocation).Value = clsCommon.ShowSelectForm("BAccFromLoc", qry, "Code", WhrCls, gv.CurrentRow.Cells(ColFromLocation).Value, "Code", isButtonClick)
            If clsCommon.myLen(clsCommon.myCstr(gv.CurrentRow.Cells(ColFromLocation).Value)) > 0 Then
                gv.CurrentRow.Cells(ColFromLocationName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Description,'') As Description FROM TSPL_GL_SEGMENT_CODE WHERE Segment_code ='" & clsCommon.myCstr(gv.CurrentRow.Cells(ColFromLocation).Value) & "'"))
            Else
                gv.CurrentRow.Cells(ColFromLocationName).Value = ""
            End If
        End If
        
    End Sub
    Sub OpenToLoc(ByVal isButtonClick As Boolean)
        
        'If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
        '    WhrCls += "  and  TSPL_LOCATION_MASTER.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        'End If
        If gv.CurrentRow.Index >= 0 Then
            Dim qry As String = "select distinct(Segment_code) as Code ,Description  from TSPL_GL_SEGMENT_CODE left outer join TSPL_LOCATION_MASTER on TSPL_GL_SEGMENT_CODE.Segment_code =TSPL_LOCATION_MASTER.Loc_Segment_Code "
            Dim WhrCls As String = "Seg_No = '7' AND GIT='N'"
            gv.CurrentRow.Cells(ColToLocation).Value = clsCommon.ShowSelectForm("BAccToLoc", qry, "Code", WhrCls, gv.CurrentRow.Cells(ColToLocation).Value, "Code", isButtonClick)
            If clsCommon.myLen(clsCommon.myCstr(gv.CurrentRow.Cells(ColToLocation).Value)) > 0 Then
                gv.CurrentRow.Cells(ColToLocationName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Description,'') As Description FROM TSPL_GL_SEGMENT_CODE WHERE Segment_code ='" & clsCommon.myCstr(gv.CurrentRow.Cells(ColToLocation).Value) & "'"))
            Else
                gv.CurrentRow.Cells(ColToLocationName).Value = ""
            End If
        End If
        
    End Sub
    Sub OpenBranchAcc(ByVal isButtonClick As Boolean)
        Dim Whr As String = ""
        If gv.CurrentRow.Index >= 0 Then
            If clsCommon.myLen(gv.CurrentRow.Cells(ColFromLocation).Value) <= 0 Then
                gv.CurrentRow.Cells(ColBranchAccount).Value = ""
                clsCommon.MyMessageBoxShow("Please select from location first", Me.Text)
                Exit Sub
            End If

            Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
            If clsCommon.myLen(clsCommon.myCstr(gv.CurrentRow.Cells(ColFromLocation).Value)) > 0 Then
                Whr = " Account_Seg_Code7 ='" & clsCommon.myCstr(gv.CurrentRow.Cells(ColFromLocation).Value) & "' "
            End If

            gv.CurrentRow.Cells(ColBranchAccount).Value = clsCommon.ShowSelectForm("GLBranchAcc", Qry, "Account_Code", Whr, gv.CurrentRow.Cells(ColBranchAccount).Value, "Account_Code", isButtonClick)
            If clsCommon.myLen(clsCommon.myCstr(gv.CurrentRow.Cells(ColBranchAccount).Value)) > 0 Then
                gv.CurrentRow.Cells(ColBranchAccountName).Value = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + clsCommon.myCstr(gv.CurrentRow.Cells(ColBranchAccount).Value) + "' ")
            Else
                gv.CurrentRow.Cells(ColBranchAccountName).Value = ""
            End If
        End If
        
    End Sub
    'Private Sub txtFromLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean)
    '    Try
    '        Dim qry As String = "select distinct(Segment_code) as Code ,Description  from TSPL_GL_SEGMENT_CODE left outer join TSPL_LOCATION_MASTER on TSPL_GL_SEGMENT_CODE.Segment_code =TSPL_LOCATION_MASTER.Loc_Segment_Code "
    '        Dim WhrCls As String = "Seg_No = '7' AND GIT='N'"
    '        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
    '            WhrCls += "  and  TSPL_LOCATION_MASTER.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
    '        End If
    '        txtFromLocation.Value = clsCommon.ShowSelectForm("BAccFromLoc", qry, "Code", WhrCls, txtFromLocation.Value, "Code", isButtonClicked)
    '        If clsCommon.myLen(txtFromLocation.Value) > 0 Then
    '            lblFromLocName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Location_Desc,'') As Location_Desc FROM TSPL_LOCATION_MASTER WHERE Location_Code ='" & clsCommon.myCstr(txtFromLocation.Value) & "'"))
    '        Else
    '            lblFromLocName.Text = ""
    '        End If
    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub

    Public Sub SaveData()
        Try
            If AllowToSave() Then
                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.FrmBranchAccountMapping, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If
                Dim arr As New List(Of ClsBranchAccountMapping)
                Dim obj As ClsBranchAccountMapping = Nothing
                For Each grow As GridViewRowInfo In gv.Rows
                    If clsCommon.myLen(grow.Cells(ColFromLocation).Value) > 0 Then
                        obj = New ClsBranchAccountMapping()
                        obj.From_Location = clsCommon.myCstr(grow.Cells(ColFromLocation).Value)
                        obj.To_Location = clsCommon.myCstr(grow.Cells(ColToLocation).Value)
                        obj.Branch_Account = clsCommon.myCstr(grow.Cells(ColBranchAccount).Value)
                        If clsCommon.myLen(obj.From_Location) > 0 AndAlso clsCommon.myLen(obj.To_Location) > 0 AndAlso clsCommon.myLen(obj.Branch_Account) > 0 Then
                            arr.Add(obj)
                        End If
                    End If
                Next
                If arr Is Nothing OrElse arr.Count <= 0 Then
                    clsCommon.MyMessageBoxShow("No data found.")
                Else
                    If ClsBranchAccountMapping.SaveData(arr) Then
                        clsCommon.MyMessageBoxShow("Data saved successfully.")
                    End If
                End If
                LoadData()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub FrmBranchAccountMapping_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Private Sub FrmBranchAccountMapping_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        gv.AllowAddNewRow = False
        LoadData()
    End Sub
    Sub LoadBlankGrid()
        gv.Rows.Clear()
        gv.Columns.Clear()

        Dim repoFromLoc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoFromLoc.FormatString = ""
        repoFromLoc.HeaderText = "From Location"
        repoFromLoc.Name = ColFromLocation
        repoFromLoc.HeaderImage = Global.XpertERPCommanServices.My.Resources.Resources.search4
        repoFromLoc.TextImageRelation = TextImageRelation.TextBeforeImage
        repoFromLoc.Width = 130
        gv.MasterTemplate.Columns.Add(repoFromLoc)

        Dim repoFromLocName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoFromLocName.FormatString = ""
        repoFromLocName.HeaderText = "From Location Name"
        repoFromLocName.Name = ColFromLocationName
        repoFromLocName.Width = 200
        repoFromLocName.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoFromLocName)

        Dim repoToLocation As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoToLocation.FormatString = ""
        repoToLocation.HeaderText = "To Location"
        repoToLocation.Name = ColToLocation
        repoToLocation.HeaderImage = Global.XpertERPCommanServices.My.Resources.Resources.search4
        repoToLocation.TextImageRelation = TextImageRelation.TextBeforeImage
        repoToLocation.Width = 130
        'repoToLocation.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoToLocation)

        Dim repoToLocName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoToLocName.FormatString = ""
        repoToLocName.HeaderText = "To Location Name"
        repoToLocName.Name = ColToLocationName
        repoToLocName.Width = 200
        repoToLocName.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoToLocName)

        Dim repoBAccount As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBAccount.FormatString = ""
        repoBAccount.HeaderText = "Branch Account"
        repoBAccount.Name = ColBranchAccount
        repoBAccount.HeaderImage = Global.XpertERPCommanServices.My.Resources.Resources.search4
        repoBAccount.TextImageRelation = TextImageRelation.TextBeforeImage
        repoBAccount.Width = 130
        gv.MasterTemplate.Columns.Add(repoBAccount)

        Dim repoBAccName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBAccName.FormatString = ""
        repoBAccName.HeaderText = "Branch Account Name"
        repoBAccName.Name = ColBranchAccountName
        repoBAccName.Width = 240
        repoBAccName.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoBAccName)

        gv.AllowDeleteRow = True
        gv.AllowAddNewRow = False
        gv.ShowGroupPanel = False
        gv.AllowColumnReorder = False
        gv.AllowRowReorder = False
        gv.EnableSorting = False
        gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv.MasterTemplate.ShowRowHeaderColumn = False
        gv.TableElement.TableHeaderHeight = 40
        ReStoreGridLayout()
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv.Columns.Count - 1 Step ii + 1
                        gv.Columns(ii).IsVisible = False
                        gv.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Public Sub LoadData()
        Try
            IsLoadData = True
            gv.DataSource = ClsBranchAccountMapping.GetDataTable(Nothing)

            gv.Columns("From Location").Width = 130
            gv.Columns("From Location Name").Width = 200
            gv.Columns("To Location").Width = 130
            gv.Columns("To Location Name").Width = 200
            gv.Columns("Branch Account").Width = 130
            gv.Columns("Branch Account Name").Width = 240
            gv.AllowDeleteRow = True
            gv.AllowAddNewRow = False
            gv.ShowGroupPanel = False
            gv.AllowColumnReorder = False
            gv.AllowRowReorder = False
            gv.EnableSorting = False
            gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
            gv.MasterTemplate.ShowRowHeaderColumn = False
            gv.TableElement.TableHeaderHeight = 40
            ReStoreGridLayout()
            gv.Rows.AddNew()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            IsLoadData = False
        End Try
    End Sub

    Public Sub LoadDataOLD()
        Try
            Dim dtStart As DateTime = DateTime.Now
            LoadBlankGrid()
            IsLoadData = True
            Dim arr As New List(Of ClsBranchAccountMapping)
            arr = ClsBranchAccountMapping.GetData(Nothing)
            gv.Rows.AddNew()
            For Each obj As ClsBranchAccountMapping In arr
                gv.CurrentRow.Cells(ColFromLocation).Value = clsCommon.myCstr(obj.From_Location)
                If clsCommon.myLen(clsCommon.myCstr(gv.CurrentRow.Cells(ColFromLocation).Value)) > 0 Then
                    gv.CurrentRow.Cells(ColFromLocationName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Description,'') As Description FROM TSPL_GL_SEGMENT_CODE WHERE Segment_code ='" & clsCommon.myCstr(gv.CurrentRow.Cells(ColFromLocation).Value) & "'"))
                Else
                    gv.CurrentRow.Cells(ColFromLocationName).Value = ""
                End If
                gv.CurrentRow.Cells(ColToLocation).Value = clsCommon.myCstr(obj.To_Location)
                If clsCommon.myLen(clsCommon.myCstr(gv.CurrentRow.Cells(ColToLocation).Value)) > 0 Then
                    gv.CurrentRow.Cells(ColToLocationName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Description,'') As Description FROM TSPL_GL_SEGMENT_CODE WHERE Segment_code ='" & clsCommon.myCstr(gv.CurrentRow.Cells(ColToLocation).Value) & "'"))
                Else
                    gv.CurrentRow.Cells(ColToLocationName).Value = ""
                End If
                gv.CurrentRow.Cells(ColBranchAccount).Value = clsCommon.myCstr(obj.Branch_Account)
                If clsCommon.myLen(clsCommon.myCstr(gv.CurrentRow.Cells(ColBranchAccount).Value)) > 0 Then
                    gv.CurrentRow.Cells(ColBranchAccountName).Value = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + clsCommon.myCstr(gv.CurrentRow.Cells(ColBranchAccount).Value) + "' ")
                Else
                    gv.CurrentRow.Cells(ColBranchAccountName).Value = ""
                End If
                gv.Rows.AddNew()
            Next
            Dim dtEnd As DateTime = DateTime.Now
            Dim dtSpan As TimeSpan = dtEnd.Subtract(dtStart)
            clsCommon.MyMessageBoxShow("Minutes: " + clsCommon.myCstr(dtSpan.Minutes) + " and Seconds: " + clsCommon.myCstr(dtSpan.Seconds))
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            IsLoadData = False
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        funClose()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'DeleteData()
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If gv.Rows.Count > 0 Then
            SaveData()
        Else
            clsCommon.MyMessageBoxShow("No data found")
        End If

    End Sub

    Private Sub rdbtnreset_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        funReset()
    End Sub
    'Sub LoadToLocation(ByVal isButtonClick As Boolean)
    '    Try
    '        Dim qry As String = "select distinct(Segment_code) as Code ,Description  from TSPL_GL_SEGMENT_CODE left outer join TSPL_LOCATION_MASTER on TSPL_GL_SEGMENT_CODE.Segment_code =TSPL_LOCATION_MASTER.Loc_Segment_Code "
    '        Dim WhrCls As String = "Seg_No = '7' AND GIT='N'"
    '        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
    '            WhrCls += "  and  TSPL_LOCATION_MASTER.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
    '        End If
    '        gv.CurrentRow.Cells(ColToLocation).Value = clsCommon.ShowSelectForm("BAccToLoc", qry, "Code", WhrCls, gv.CurrentRow.Cells(ColToLocation).Value, "Code", isButtonClick)
    '        If clsCommon.myLen(gv.CurrentRow.Cells(ColToLocation).Value) > 0 Then
    '            lblToLocName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Location_Desc,'') AS Location_Desc FROM TSPL_LOCATION_MASTER WHERE Location_Code ='" & clsCommon.myCstr(txtToLocation.Value) & "'"))
    '        Else
    '            lblToLocName.Text = ""
    '        End If
    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub
    Private Sub txtToLocation__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean)

    End Sub

    Private Sub gv_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellValueChanged
        If Not IsLoadData Then
            If e.Column Is gv.Columns(ColFromLocation) Then
                OpenFromLoc(False)
            ElseIf e.Column Is gv.Columns(ColToLocation) Then
                OpenToLoc(False)
            ElseIf e.Column Is gv.Columns(ColBranchAccount) Then
                OpenBranchAcc(False)
            End If
        End If
    End Sub

    'Private Sub txtBranchAcc__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean)
    '    Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
    '    txtBranchAcc.Value = clsCommon.ShowSelectForm("GLBranchAcc", Qry, "Account_Code", "", txtBranchAcc.Value, "Account_Code", isButtonClicked)
    '    If clsCommon.myLen(txtBranchAcc.Value) > 0 Then
    '        LblBranchAcc.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + txtBranchAcc.Value + "' ")
    '    Else
    '        LblBranchAcc.Text = ""
    '    End If

    'End Sub

    Private Sub gv_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv.CurrentColumnChanged
        If gv.RowCount > 0 Then
            Dim intCurrRow As Integer = gv.CurrentRow.Index
            'gv.CurrentRow.Cells(colContainerQty).Value = 1
            'gv.CurrentRow.Cells(coltemp).Value = "="
            If intCurrRow = gv.Rows.Count - 1 Then
                gv.Rows.AddNew()
                gv.CurrentRow = gv.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gv_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub rmExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmExport.Click
        Dim str As String
        str = " select From_Location AS [From Location],FLoc.Location_Desc AS [From Location Name],To_Location AS [To Location],TLoc.Location_Desc AS [To Location Name],Branch_Account As [Branch Account],TSPL_GL_ACCOUNTS.Description AS [Branch Account Name] from TSPL_BRANCH_ACCOUNT_MAPPING " & _
              " LEFT OUTER JOIN TSPL_LOCATION_MASTER FLoc on FLoc.Location_Code =tspl_branch_account_Mapping.From_Location " & _
              " LEFT OUTER JOIN TSPL_LOCATION_MASTER TLoc on TLoc.Location_Code =tspl_branch_account_Mapping.To_Location  " & _
              " LEFT OUTER JOIN TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_BRANCH_ACCOUNT_MAPPING.Branch_Account  "
        transportSql.ExporttoExcel(str, Me)
    End Sub

    Private Sub rmImport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rmImport.Click
        Dim CustMapEntry As Double = 0
        Dim VenMapEntry As Double = 0
        Dim DuplicateEntry As String = ""
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "From Location", "From Location Name", "To Location", "To Location Name", "Branch Account", "Branch Account Name") Then
            Dim linno As Integer = 0
            Dim trans As SqlTransaction = Nothing
            Try
                connectSql.OpenConnection()
                clsCommon.ProgressBarShow()

                trans = clsDBFuncationality.GetTransactin()
                For i As Integer = 0 To gv.Rows.Count - 1
                    clsDBFuncationality.ExecuteNonQuery("DELETE FROM tspl_branch_account_Mapping where From_Location = '" & clsCommon.myCstr(gv.Rows(i).Cells("From Location").Value) & "' AND To_Location = '" & clsCommon.myCstr(gv.Rows(i).Cells("To Location").Value) & "'", trans)
                Next
                For Each grow As GridViewRowInfo In gv.Rows

                    linno += 1

                    Dim From_Loc As String = ""
                    From_Loc = clsCommon.myCstr(grow.Cells("From Location").Value)

                    If clsCommon.myLen(From_Loc) > 0 Then
                        Dim FLocQry As String = "select Count(Segment_code) as Code from TSPL_GL_SEGMENT_CODE left outer join TSPL_LOCATION_MASTER on TSPL_GL_SEGMENT_CODE.Segment_code =TSPL_LOCATION_MASTER.Loc_Segment_Code WHERE Segment_code ='" + From_Loc + "' AND Seg_No = '7' AND GIT='N'"
                        'Dim WhrCls As String = " "

                        'Dim FLocQry As String = "select Count(*) As Row from TSPL_LOCATION_MASTER where Location_Code ='" + From_Loc + "'"
                        Dim checkFLoc As Integer = clsDBFuncationality.getSingleValue(FLocQry, trans)
                        If checkFLoc <= 0 Then
                            Throw New Exception("Please check ! From location code (" & clsCommon.myCstr(From_Loc) & ") does not exists in location master at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    Else
                        Throw New Exception("From location code can not be left blank at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    From_Loc = From_Loc

                    Dim To_Loc As String = ""
                    To_Loc = clsCommon.myCstr(grow.Cells("To Location").Value)

                    If clsCommon.myLen(To_Loc) > 0 Then
                        Dim TLocQry As String = "select Count(Segment_code) as Code  from TSPL_GL_SEGMENT_CODE left outer join TSPL_LOCATION_MASTER on TSPL_GL_SEGMENT_CODE.Segment_code =TSPL_LOCATION_MASTER.Loc_Segment_Code WHERE Segment_code ='" + To_Loc + "' AND Seg_No = '7' AND GIT='N'"
                        ' Dim WhrCls As String = ""
                        'Dim TLocQry As String = "select Count(*) As Row from TSPL_LOCATION_MASTER where Location_Code ='" + To_Loc + "'"
                        Dim checkTLoc As Integer = clsDBFuncationality.getSingleValue(TLocQry, trans)
                        If checkTLoc <= 0 Then
                            Throw New Exception("Please check ! To location code (" & clsCommon.myCstr(To_Loc) & ") does not exists in location master at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    Else
                        Throw New Exception("To location code can not be left blank at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    To_Loc = To_Loc

                    If clsCommon.myLen(From_Loc) > 0 AndAlso clsCommon.myLen(To_Loc) > 0 Then
                        If clsCommon.CompairString(From_Loc, To_Loc) = CompairStringResult.Equal Then
                            Throw New Exception("From location and To location can not be same at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    Dim Branch_Acc As String = ""
                    Branch_Acc = clsCommon.myCstr(grow.Cells("Branch Account").Value)
                    If clsCommon.myLen(Branch_Acc) > 0 Then
                        Dim BAccQry As String = "select Count(*) As Row from TSPL_GL_ACCOUNTS where Account_Code ='" + Branch_Acc + "'"
                        Dim checkBAcc As Integer = clsDBFuncationality.getSingleValue(BAccQry, trans)
                        If checkBAcc <= 0 Then
                            Throw New Exception("Please check ! Branch account code (" & clsCommon.myCstr(Branch_Acc) & ") does not exists in its master at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim BAccWithLocQry As String = "select Count(*) As Row from TSPL_GL_ACCOUNTS where Account_Code ='" + Branch_Acc + "' AND Account_Seg_Code7 ='" & From_Loc & "' "
                        Dim checkBAccWithLoc As Integer = clsDBFuncationality.getSingleValue(BAccWithLocQry, trans)
                        If checkBAccWithLoc <= 0 Then
                            Throw New Exception("Please check ! Branch account code (" & clsCommon.myCstr(Branch_Acc) & ") is not mapped with from from location (" & From_Loc & ") at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    Else
                        Throw New Exception("Branch account code can not be left blank at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    Branch_Acc = Branch_Acc
                    ''
                    'Dim NewEntry As Boolean
                    Dim NewCheck As Double = 0
                    Dim qry As String = ""
                    'If clsCommon.myLen(From_Loc) > 0 AndAlso clsCommon.myLen(To_Loc) > 0 Then
                    '    NewCheck = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT COUNT(*) AS Row FROM tspl_branch_account_Mapping where From_Location='" & From_Loc & "'", trans)) ' AND To_Location='" & To_Loc & "'", trans))
                    '    If NewCheck > 0 Then
                    '        NewEntry = False
                    '    Else
                    '        NewEntry = True
                    '    End If
                    'End If
                    'If NewEntry = True Then
                    qry = "insert into TSPL_BRANCH_ACCOUNT_MAPPING values('" + From_Loc + "','" + To_Loc + "','" + Branch_Acc + "','" + objCommonVar.CurrentCompanyCode + "','" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") + "','" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") + "')"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    'ElseIf NewEntry = False Then
                    'qry = "update TSPL_BRANCH_ACCOUNT_MAPPING set  To_Location='" + To_Loc + "', Branch_Account='" + Branch_Acc + "',Comp_Code='" + objCommonVar.CurrentCompanyCode + "',Modify_By='" + objCommonVar.CurrentUserCode + "',Modified_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") + "' where From_Location='" + From_Loc + "'" ' AND To_Location='" + To_Loc + "'"
                    'clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    'End If

                Next
                DuplicateEntry = "select From_Location ,To_Location, SUM(1) as Repeated from tspl_branch_account_Mapping group by From_Location,To_Location  having SUM(1) > 1"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(DuplicateEntry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Throw New Exception("Please check ! from location (" & clsCommon.myCstr(dt.Rows(0)("From_Location")) & ") with to location (" & clsCommon.myCstr(dt.Rows(0)("To_Location")) & ") repeated " & clsCommon.myCstr(dt.Rows(0)("Repeated")) & " times.")
                End If

                trans.Commit()
                clsCommon.ProgressBarHide()

                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                LoadData()
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub rmSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)

        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub
End Class
