'--25/01/2013-09:00AM--Created By-Pankaj Kumar
'---------------------------------------------Table Used--[TSPL_ASSET_GROUP]
'---------------------------------------------Class Used--[clsAssetGroup]
'===against[BM00000008099,BM00000008097]
'Sanjay Ticket No- TEC/05/11/18-000365 Add image on all the new buttons on all FA screens
Imports common
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports XpertERPEngine
Imports Telerik.WinControls

Public Class FrmGroups
    Inherits FrmMainTranScreen
    Dim Qry As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim ReadOnlyTemplateFieldsOnAcqusition As Boolean = False

    Private Sub FrmGroups_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Reset()
        SetMaxLength()
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
    End Sub
    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        txtGroupCode.Value = ""
        Reset()
        txtGroupCode.Focus()
    End Sub

    Private Sub SetUserMgmtNew()
        '' Anubhooti 31-July-2014 BM00000003131
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmAssetGroups)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function
        End If
        btnSave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnSave.Visible = True Then
            rmiExport.Enabled = True
            rmiImport.Enabled = True
        Else
            rmiExport.Enabled = False
            rmiImport.Enabled = False
        End If
        '--------------------------------------------------
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub Reset()
        '' if below setting is on then 
        '' 1. Rename FA Template Master to Asset Category
        '' 2. Rename Asset Category Master to Asset Group
        '' 3. Rename Asset Group Master to Sub Group Master
        ReadOnlyTemplateFieldsOnAcqusition = If(clsFixedParameter.GetData(clsFixedParameterCode.ReadOnlyTemplateFieldsOnAcqusition, clsFixedParameterType.ReadOnlyTemplateFieldsOnAcqusition, Nothing) = 1, True, False)
        If ReadOnlyTemplateFieldsOnAcqusition Then
            Me.Text = "Asset Sub Group Master"
            Me.lblCategory.Text = "Asset Group"
        End If
        txtCategory.Value = ""
        lblCategoryDesc.Text = ""
        txtcounter.Text = ""
        txtGroupCode.MyReadOnly = False
        chkDefaultCategory.Checked = False
        chkInactive.Checked = False
        txtDescription.Text = ""
        txtNotes.Text = ""
        dtpLastMaintained.Value = clsCommon.GETSERVERDATE()
        btnSave.Text = "Save"
        btnDelete.Enabled = False
        txtcounter.Enabled = True
    End Sub
    Private Sub SetMaxLength()
        txtGroupCode.MyMaxLength = 20
        txtDescription.MaxLength = 100
        txtNotes.MaxLength = 200
    End Sub

    Private Sub txtCategoryCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtGroupCode._MYValidating
        Try
            Qry = "select count(*) from TSPL_ASSET_GROUP where Group_Code='" + txtGroupCode.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry))
            If count = 0 Then
                txtGroupCode.MyReadOnly = False
            Else
                txtGroupCode.MyReadOnly = True
            End If

            If txtGroupCode.MyReadOnly OrElse isButtonClicked Then
                'Qry = "Select Group_Code as [Code], Description, Is_Default as [Default], Convert(VARCHAR,Last_Maintained_Date, 103) as [Last Maintained], Inactive from TSPL_ASSET_GROUP "
                'txtGroupCode.Value = clsCommon.ShowSelectForm("AssetCategorySelector", Qry, "Code", "", txtGroupCode.Value, "", isButtonClicked)
                txtGroupCode.Value = clsAssetGroups.getFinder("", txtGroupCode.Value, isButtonClicked)
                LoadData(txtGroupCode.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub txtCategoryCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtGroupCode._MYNavigator
        Try
            Qry = "select count(*) from TSPL_ASSET_GROUP where Group_Code='" + txtGroupCode.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry))
            If count = 0 Then
                txtGroupCode.MyReadOnly = False
            Else
                txtGroupCode.MyReadOnly = True
            End If
            LoadData(txtGroupCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub
    Private Sub SaveData()
        Try
            If AllowToSave() Then

                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmAssetGroups, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If
                Dim Arr As New List(Of clsAssetGroups)
                Dim obj As New clsAssetGroups()
                obj.Group_Code = clsCommon.myCstr(txtGroupCode.Value)
                If chkDefaultCategory.Checked = True Then
                    obj.Is_Default = "1"
                End If
                obj.Description = clsCommon.myCstr(txtDescription.Text)
                obj.Category_Code = clsCommon.myCstr(txtCategory.Value)
                obj.Last_Maintained_Date = clsCommon.GetPrintDate(dtpLastMaintained.Value, "dd/MMM/yyyy")
                If chkInactive.Checked = True Then
                    obj.Inactive = "1"
                End If
                obj.Notes = clsCommon.myCstr(txtNotes.Text)
                obj.precounter = clsCommon.myCstr(txtcounter.Text)
                Arr.Add(obj)
                If (clsAssetGroups.SaveData(Arr)) Then
                    myMessages.insert()
                    LoadData(txtGroupCode.Value, NavigatorType.Current)
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Function AllowToSave() As Boolean
        If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False) = False) Then
            If clsCommon.myLen(txtGroupCode.Value) <= 0 Then
                RadMessageBox.Show("Please enter Group Code")
                txtGroupCode.Focus()
                Return False
            End If
        End If
        If clsCommon.myLen(txtDescription.Text) <= 0 Then
            RadMessageBox.Show("Please enter Group Description")
            txtDescription.Focus()
            Return False
        End If
        If clsCommon.myLen(txtCategory.Value) <= 0 Then
            RadMessageBox.Show("Please select category")
            txtCategory.Focus()
            Return False
        End If
        If clsCommon.myLen(txtcounter.Text) <= 0 Then
            RadMessageBox.Show("Please Fill Prefix Counter")
            txtcounter.Focus()
            txtcounter.Select()
            Return False
        End If
        If clsCommon.myLen(txtcounter.Text) > 20 Then
            RadMessageBox.Show("Length Of Prefix Counter Should Not Exceed 20 Characters")
            txtcounter.Focus()
            txtcounter.Select()
            Return False
        End If

        Return True
    End Function

    Private Sub LoadData(ByVal strCategoryCode As String, ByVal navType As common.NavigatorType)
        Try
            Dim obj As New clsAssetGroups()
            obj = clsAssetGroups.GetData(strCategoryCode, navType)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Group_Code) > 0) Then
                txtGroupCode.MyReadOnly = True
                txtGroupCode.Value = obj.Group_Code
                If obj.Is_Default <> "0" Then
                    chkDefaultCategory.Checked = True
                Else
                    chkDefaultCategory.Checked = False
                End If
                txtDescription.Text = obj.Description
                txtcounter.Text = clsCommon.myCstr(obj.precounter)
                txtCategory.Value = obj.Category_Code
                lblCategoryDesc.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_ASSET_CATEGORY where Category_Code='" & txtCategory.Value & "' ")
                dtpLastMaintained.Value = obj.Last_Maintained_Date
                If obj.Inactive <> "0" Then
                    chkInactive.Checked = True
                Else
                    chkInactive.Checked = False
                End If
                txtNotes.Text = obj.Notes
                btnSave.Text = "Update"
                btnDelete.Enabled = True
                CheckCounter()
            Else
                txtGroupCode.Value = ""
                Reset()
                btnSave.Text = "Save"
                btnDelete.Enabled = False
            End If
        Catch ex As Exception
            RadMessageBox.Show(ex.Message)
        End Try

    End Sub
    Sub CheckCounter()
        Dim qry As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(Group_Code ) from TSPL_ACQUISITION_DETAIL left join TSPL_ACQUISITION_HEAD on TSPL_ACQUISITION_HEAD.Acquisition_Code =TSPL_ACQUISITION_DETAIL.Acquisition_Code where Status ='1' and Group_Code ='" & txtGroupCode.Value & "' "))
        If qry > 0 Then
            txtcounter.Enabled = False
        Else
            txtcounter.Enabled = True
        End If
    End Sub

    Private Sub rmiExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiExport.Click
        Export()
    End Sub

    Private Sub Export()
        Try
            Qry = "Select Group_Code as [Group], Description as [Description],Category_Code as [Category Code],prefix_counter as [Prefix Counter] from TSPL_ASSET_GROUP"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt.Rows.Count <= 0 Then
                Qry = "Select '' as [Group], '' as [Description], 0 as [Is Default], 0 as Inactive, '' as Notes "
            End If
            transportSql.ExporttoExcel(Qry, Me)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Customer")
        End Try
    End Sub

    Private Sub rmiImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiImport.Click
        Import()
    End Sub
    Private Sub Import()
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "Group", "Description", "Category Code", "Prefix Counter") Then
            Try
                clsCommon.ProgressBarShow()
                Dim Arr As New List(Of clsAssetGroups)
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim LineNo As String = clsCommon.myCstr(grow.Index) + 2
                    Dim obj As New clsAssetGroups()

                    '-------Category Code---------
                    obj.Group_Code = clsCommon.myCstr(grow.Cells("Group").Value)
                    If clsCommon.myLen(obj.Group_Code) <= 0 Then
                        Throw New Exception("Please Insert Group Code on Line No '" + LineNo + "'")
                        Exit Sub
                    ElseIf clsCommon.myLen(obj.Group_Code) > 30 Then
                        Throw New Exception("The Maximum Length of Group Code on Line No '" + LineNo + "' Is Greater Than 30")
                        Exit Sub
                    End If

                    ''-------Is Default-------
                    'If clsCommon.myCstr(grow.Cells("Is Default").Value) = "1" Then
                    '    obj.Is_Default = "1"
                    'Else
                    obj.Is_Default = "0"
                    'End If

                    '-----Description------
                    obj.Description = clsCommon.myCstr(grow.Cells("Description").Value)
                    If clsCommon.myLen(obj.Description) > 100 Then
                        Throw New Exception("The Maximum Length of Description on Line No '" + LineNo + "' Is Greater Than 100")
                    End If

                    obj.Category_Code = clsCommon.myCstr(grow.Cells("Category Code").Value)
                    If clsCommon.myLen(obj.Category_Code) <= 0 Then
                        Throw New Exception("Category Code on Line No '" + LineNo + "' Is blank")
                    End If
                    If clsCommon.myLen(obj.Category_Code) > 12 Then
                        Throw New Exception("The Maximum Length of Category Code on Line No '" + LineNo + "' Is Greater Than 12")
                    End If

                    obj.Last_Maintained_Date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")   '----Last Maintained date

                    '--------Inactive------------
                    'If clsCommon.myCstr(grow.Cells("Inactive").Value) = "1" Then
                    '    obj.Inactive = "1"
                    'Else
                    obj.Inactive = "0"
                    'End If

                    obj.Notes = "" 'clsCommon.myCstr(grow.Cells("Notes").Value)     '---Notes
                    'If clsCommon.myLen(obj.Notes) > 200 Then
                    '    Throw New Exception("The Maximum Length of Notes on Line No '" + LineNo + "' Is Greater Than 100")
                    'End If
                    obj.precounter = clsCommon.myCstr(grow.Cells("Prefix Counter").Value)

                    If clsCommon.myLen(obj.precounter) <= 0 Then
                        Throw New Exception("Invalid Value Of Prefix Counter,Please Fill Value In Prefix Counter(Character Limits 20)")
                        Exit Sub
                    End If
                    Arr.Add(obj)
                Next

                If (clsAssetGroups.SaveData(Arr)) Then
                    clsCommon.ProgressBarHide()
                    common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                End If

            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub rmiExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiExit.Click
        Me.Close()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub FrmDepreciationField_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            Me.Close()
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsAssetGroups.DeleteData(txtGroupCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully", Me.Text)
                    txtGroupCode.Value = ""
                    Reset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub txtCategory__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCategory._MYValidating
        Dim qry As String = "Select Category_Code as [Code], Description from TSPL_ASSET_CATEGORY "
        txtCategory.Value = clsCommon.ShowSelectForm("AssetCategorySelector", qry, "Code", "", txtCategory.Value, "", isButtonClicked)
        lblCategoryDesc.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_ASSET_CATEGORY where Category_Code='" & txtCategory.Value & "' ")
    End Sub
End Class
