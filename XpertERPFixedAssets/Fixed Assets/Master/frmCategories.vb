'-24/01/2013-9:30AM --Created By Pankaj Kumar
'----Table Used-[TSPL_ASSET_CACTEGORY]
'----Class Used--[clsCategories]
Imports common
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports Telerik.WinControls

Public Class FrmCategories
    Inherits FrmMainTranScreen
    Dim Qry As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim ReadOnlyTemplateFieldsOnAcqusition As Boolean = False
    Private Sub FrmCategories_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Reset()
        SetMaxLength()
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        txtCategoryCode.Value = ""
        Reset()
        txtCategoryCode.Focus()
    End Sub

    Public Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.Categories)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub Reset()
        '' if below setting is on then 
        '' 1. Rename FA Template Master to Asset Category
        '' 2. Rename Asset Category Master to Asset Group
        '' 3. Rename Asset Group Master to Sub Group Master
        ReadOnlyTemplateFieldsOnAcqusition = If(clsFixedParameter.GetData(clsFixedParameterCode.ReadOnlyTemplateFieldsOnAcqusition, clsFixedParameterType.ReadOnlyTemplateFieldsOnAcqusition, Nothing) = 1, True, False)
        If ReadOnlyTemplateFieldsOnAcqusition Then
            Me.Text = "Asset Group Master"
        End If
        txtcounter.Text = ""
        txtCategoryCode.MyReadOnly = False
        chkDefaultCategory.Checked = False
        chkInactive.Checked = False
        txtDescription.Text = ""
        txtNotes.Text = ""
        dtpLastMaintained.Value = clsCommon.GETSERVERDATE()
        txtDefaultAccSet.Value = ""
        txtSegmentCode.Text = ""
        txtNextAutoNo.Text = 0
        lblDefaultSetDesc.Text = ""
        btnSave.Text = "Save"
        btnDelete.Enabled = False
        txtSeries1.Text = 0
        txtcounter.Enabled = True
        txtSeries1.Enabled = True
    End Sub
    Private Sub SetMaxLength()
        txtCategoryCode.MyMaxLength = 12
        txtDescription.MaxLength = 100
        txtNotes.MaxLength = 200
        txtSegmentCode.MaxLength = 12
        txtNextAutoNo.MaxLength = 8
    End Sub

    Private Sub txtCategoryCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCategoryCode._MYValidating
        Try
            Qry = "select count(*) from TSPL_ASSET_CATEGORY where Category_Code='" + txtCategoryCode.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry))
            If count = 0 Then
                txtCategoryCode.MyReadOnly = False
            Else
                txtCategoryCode.MyReadOnly = True
            End If
            If txtCategoryCode.MyReadOnly OrElse isButtonClicked Then
                Qry = "Select Category_Code as [Code], Description, Convert(VARCHAR,Last_Maintained_Date, 103) as [Last Maintained] from TSPL_ASSET_CATEGORY "
                txtCategoryCode.Value = clsCommon.ShowSelectForm("AssetCategorySelector", Qry, "Code", "", txtCategoryCode.Value, "", isButtonClicked)
                LoadData(txtCategoryCode.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub txtCategoryCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCategoryCode._MYNavigator
        Try
            Qry = "select count(*) from TSPL_ASSET_CATEGORY where Category_Code='" + txtCategoryCode.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry))
            If count = 0 Then
                txtCategoryCode.MyReadOnly = False
            Else
                txtCategoryCode.MyReadOnly = True
            End If
            LoadData(txtCategoryCode.Value, NavType)
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
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.Categories, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If

                Dim Arr As New List(Of clsCategories)
                Dim obj As New clsCategories()
                obj.arrGroup = New List(Of clsAssetCategoryGroups)
                obj.Category_Code = clsCommon.myCstr(txtCategoryCode.Value)
                If chkDefaultCategory.Checked = True Then
                    obj.Is_Default = "1"
                End If
                obj.Description = clsCommon.myCstr(txtDescription.Text)
                obj.Last_Maintained_Date = clsCommon.GetPrintDate(dtpLastMaintained.Value, "dd/MMM/yyyy")
                If chkInactive.Checked = True Then
                    obj.Inactive = "1"
                End If
                obj.Notes = clsCommon.myCstr(txtNotes.Text)
                obj.Default_Account_Set = clsCommon.myCstr(txtDefaultAccSet.Value)
                obj.Segment_Code = clsCommon.myCstr(txtSegmentCode.Text)
                obj.Next_Auto_No = clsCommon.myCdbl(txtNextAutoNo.Text)
                obj.precounter = clsCommon.myCstr(txtcounter.Text)
                obj.Series = clsCommon.myCdbl(txtSeries1.Text)
                Arr.Add(obj)
                'Dim arrGroup As New ArrayList
                'arrGroup = txtGroup.arrValueMember
                'Dim objGroup As New List(Of clsAssetCategoryGroups)
                'Dim sno As Integer = 0
                'For Each Group_Code As String In arrGroup
                '    sno = sno + 1
                '    Dim objTr As New clsAssetCategoryGroups
                '    objTr.Category_Code = obj.Category_Code
                '    objTr.Group_Code = Group_Code
                '    objTr.SNO = sno
                '    obj.arrGroup.Add(objTr)
                'Next
                If (clsCategories.SaveData(Arr)) Then
                    myMessages.insert()

                    LoadData(obj.Category_Code, NavigatorType.Current)
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Function AllowToSave() As Boolean
        '' Ticket No:BM00000007692 by Panch raj
        If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False) = False) Then
            If clsCommon.myLen(txtCategoryCode.Value) <= 0 Then
                RadMessageBox.Show("Please Enter Category Code")
                txtCategoryCode.Focus()
                Return False
            End If
        End If

        If clsCommon.myLen(txtCategoryCode.Value) > 0 Then
            If clsCommon.myLen(txtDescription.Text) <= 0 Then
                RadMessageBox.Show("Please Insert Category Description")
                txtCategoryCode.Focus()
                Return False
            ElseIf clsCommon.myLen(txtDefaultAccSet.Value) <= 0 Then
                RadMessageBox.Show("Please Select Default Account Set")
                txtDefaultAccSet.Focus()
                Return False
                'ElseIf clsCommon.myLen(txtSegmentCode.Text) <= 0 Then
                '    RadMessageBox.Show("Please Insert Segment Code")
                '    txtSegmentCode.Focus()
                '    Return False
            End If
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
            Dim obj As New clsCategories()
            obj = clsCategories.GetData(strCategoryCode, navType)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Category_Code) > 0) Then
                txtCategoryCode.MyReadOnly = True
                txtCategoryCode.Value = obj.Category_Code
                If obj.Is_Default <> "0" Then
                    chkDefaultCategory.Checked = True
                Else
                    chkDefaultCategory.Checked = False
                End If
                txtDescription.Text = obj.Description
                dtpLastMaintained.Value = obj.Last_Maintained_Date
                If obj.Inactive <> "0" Then
                    chkInactive.Checked = True
                Else
                    chkInactive.Checked = False
                End If
                txtSeries1.Text = obj.Series
                txtNotes.Text = obj.Notes
                txtDefaultAccSet.Value = obj.Default_Account_Set
                lblDefaultSetDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select AcSet_Desc from TSPL_Dep_AccountSet WHERE AcSet_Code ='" + txtDefaultAccSet.Value + "'"))
                txtSegmentCode.Text = obj.Segment_Code
                txtNextAutoNo.Text = clsCommon.myCstr(obj.Next_Auto_No)

                txtcounter.Text = clsCommon.myCstr(obj.precounter)

                Dim arrGroup As New ArrayList
                For Each objTr As clsAssetCategoryGroups In obj.arrGroup
                    arrGroup.Add(objTr.Group_Code)
                Next
                txtGroup.arrValueMember = arrGroup
                CheckCounter()
                btnSave.Text = "Update"
                btnDelete.Enabled = True
            Else
                txtCategoryCode.Value = ""
                Reset()
                btnSave.Text = "Save"
                btnDelete.Enabled = False
            End If
        Catch ex As Exception
            RadMessageBox.Show(ex.Message)
        End Try

    End Sub
    Sub CheckCounter()
        Dim qry As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(Category_Code ) from TSPL_ACQUISITION_DETAIL left join TSPL_ACQUISITION_HEAD on TSPL_ACQUISITION_HEAD.Acquisition_Code =TSPL_ACQUISITION_DETAIL.Acquisition_Code where Status ='1' and Category_Code ='" & txtCategoryCode.Value & "' "))
        If qry > 0 Then
            txtcounter.Enabled = False
            txtSeries1.Enabled = False
        Else
            txtcounter.Enabled = True
            txtSeries1.Enabled = True
        End If
    End Sub
    Private Sub fndDefaultAccSet__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDefaultAccSet._MYValidating
        Qry = "Select AcSet_Code as [Code], AcSet_Desc as [Description] from TSPL_Dep_AccountSet"
        txtDefaultAccSet.Value = clsCommon.ShowSelectForm("AccSetFinder", Qry, "Code", "", txtDefaultAccSet.Value, "Code", isButtonClicked)
        lblDefaultSetDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select AcSet_Desc from TSPL_Dep_AccountSet WHERE AcSet_Code ='" + txtDefaultAccSet.Value + "'"))
    End Sub

    Private Sub rmiExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiExport.Click
        Export()
    End Sub

    Private Sub Export()
        Try
            Qry = "Select Category_Code as [Category], Description as [Description],  Notes, Default_Account_Set as [Default A/c Set],prefix_counter as [Prefix Counter] from TSPL_ASSET_CATEGORY"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt.Rows.Count <= 0 Then
                Qry = "Select '' as [Category], '' as [Description], '' as Notes, '' as [Default A/c Set],'' as  [Prefix Counter]"
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
        If transportSql.importExcel(gv, "Category", "Description", "Notes", "Default A/c Set", "Prefix Counter") Then
            Try
                clsCommon.ProgressBarShow()
                Dim Arr As New List(Of clsCategories)
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim LineNo As String = clsCommon.myCstr(grow.Index) + 2
                    Dim obj As New clsCategories()

                    '-------Category Code---------
                    obj.Category_Code = clsCommon.myCstr(grow.Cells("Category").Value)
                    If clsCommon.myLen(obj.Category_Code) <= 0 Then
                        Throw New Exception("Please Insert Category Code on Line No '" + LineNo + "'")
                        Exit Sub
                    ElseIf clsCommon.myLen(obj.Category_Code) > 12 Then
                        Throw New Exception("The Maximum Length of Category Code on Line No '" + LineNo + "' Is Greater Than 12")
                        Exit Sub
                    End If

                    '-------Is Default-------
                    'If clsCommon.myCstr(grow.Cells("Is Default").Value) = "1" Then
                    '    obj.Is_Default = "1"
                    'Else
                    '    obj.Is_Default = "0"
                    'End If

                    '-----Description------
                    obj.Description = clsCommon.myCstr(grow.Cells("Description").Value)
                    If clsCommon.myLen(obj.Description) > 100 Then
                        Throw New Exception("The Maximum Length of Description on Line No '" + LineNo + "' Is Greater Than 100")
                    End If

                    obj.Last_Maintained_Date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")   '----Last Maintained date

                    '--------Inactive------------
                    'If clsCommon.myCstr(grow.Cells("Inactive").Value) = "1" Then
                    '    obj.Inactive = "1"
                    'Else
                    '    obj.Inactive = "0"
                    'End If

                    obj.Notes = clsCommon.myCstr(grow.Cells("Notes").Value)     '---Notes
                    If clsCommon.myLen(obj.Notes) > 200 Then
                        Throw New Exception("The Maximum Length of Notes on Line No '" + LineNo + "' Is Greater Than 100")
                    End If

                    '------Default Acc SEt-------
                    obj.Default_Account_Set = clsCommon.myCstr(grow.Cells("Default A/c Set").Value)
                    Dim DAccSet As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select AcSet_Code from TSPL_Dep_AccountSet WHERE AcSet_Code='" + obj.Default_Account_Set + "'"))
                    If clsCommon.myLen(DAccSet) <= 0 Then
                        Throw New Exception("The Default Acc Set '" + obj.Default_Account_Set + "' on Line No '" + LineNo + "' Does Not Exist ")
                        Exit Sub
                    Else
                        obj.Default_Account_Set = DAccSet
                    End If

                    'obj.Segment_Code = clsCommon.myCstr(grow.Cells("Segment").Value)
                    'If clsCommon.myLen(obj.Segment_Code) > 12 Then
                    '    Throw New Exception("The Maximum Length of Notes on Line No '" + LineNo + "' Is Greater Than 12")
                    '    Exit Sub
                    'End If

                    'obj.Next_Auto_No = clsCommon.myCdbl(grow.Cells("Segment").Value)

                    obj.precounter = clsCommon.myCstr(grow.Cells("Prefix Counter").Value)

                    If clsCommon.myLen(obj.precounter) <= 0 Then
                        Throw New Exception("Invalid Value Of Prefix Counter,Please Fill Value In Prefix Counter(Character Limits 20)")
                        Exit Sub
                    End If
                    Arr.Add(obj)
                Next

                If (clsCategories.SaveData(Arr)) Then
                    clsCommon.ProgressBarHide()
                    common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
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
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
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
                If (clsCategories.DeleteData(txtCategoryCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully", Me.Text)
                    txtCategoryCode.Value = ""
                    Reset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub txtGroup__My_Click(sender As Object, e As EventArgs) Handles txtGroup._My_Click
        Dim qry As String = " select Group_Code as Code,Description as Name from TSPL_ASSET_GROUP "
        txtGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("GroupMulSel", qry, "Code", "Name", txtGroup.arrValueMember, txtGroup.arrDispalyMember)
    End Sub
End Class
