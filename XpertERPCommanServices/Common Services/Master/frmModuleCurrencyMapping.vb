Imports common
Public Class frmModuleCurrencyMapping
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Const colModuleCode As String = "ModuleCode"
    Const colModuleName As String = "ModuleName"
    Const colIsApply As String = "IsApply"
    
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
#End Region

    Private Sub FrmCustomFieldMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        LoadData()
        'gv1.MasterTemplate.AllowAddNewRow = False
        'gv1.AllowAddNewRow = False
    End Sub

    Private Sub SetUserMgmtNew()
        '' Anubhooti 31-July-2014 BM00000003131
        'MyBase.SetUserMgmt(clsUserMgtCode.frmModuleCurrencyMapping)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function
        End If
        btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub AddNew()
        LoadData()
    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim ModuleCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ModuleCode.FormatString = ""
        ModuleCode.HeaderText = "Module Code"
        ModuleCode.Name = colModuleCode
        ModuleCode.Width = 150
        ModuleCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(ModuleCode)

        Dim ModuleName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ModuleName.FormatString = ""
        ModuleName.HeaderText = "Module Name"
        ModuleName.Name = colModuleName
        ModuleName.Width = 150
        ModuleName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(ModuleName)

        Dim Apply As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        Apply.FormatString = ""
        Apply.HeaderText = "Apply"
        Apply.Name = colIsApply
        Apply.Width = 150
        Apply.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(Apply)


    End Sub

    Private Sub chkValidate_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        LoadBlankGrid()
        gv1.Rows.AddNew()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Sub SaveData()
        Try
            If (AllowToSave()) Then
                Dim Arr As New List(Of clsModuleCurrencyMapping)
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New clsModuleCurrencyMapping()
                    objTr.Module_Code = clsCommon.myCstr(grow.Cells(colModuleCode).Value)
                    objTr.Module_Name = clsCommon.myCstr(grow.Cells(colModuleName).Value)
                    objTr.Apply = clsCommon.myCBool(grow.Cells(colIsApply).Value)

                    If clsCommon.myLen(objTr.Module_Code) > 0 Then
                        Arr.Add(objTr)
                    End If
                Next
                If (clsModuleCurrencyMapping.SaveData(Arr)) Then
                    clsCommon.MyMessageBoxShow(Me, "Data saved successfully", Me.Text)
                    LoadData()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData()
        Try
            
            isInsideLoadData = True
            LoadBlankGrid()
            Dim Arr As List(Of clsModuleCurrencyMapping) = clsModuleCurrencyMapping.GetData()
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                    For Each objTr As clsModuleCurrencyMapping In Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colModuleCode).Value = objTr.Module_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colModuleName).Value = objTr.Module_Name
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIsApply).Value = objTr.Apply

                    Next
                End If
            End If


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Function AllowToSave() As Boolean
      
        Return True
    End Function

    Private Sub FrmCustomFieldMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub

    Sub CloseForm()
        Me.Close()
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        LoadData()
    End Sub

   
   
End Class
