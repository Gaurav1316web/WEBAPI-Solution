Imports common
Imports XpertERPEngine
Imports XpertERPEngineFine
Imports Telerik.WinControls.UI
Public Class frmSeedSelectionEntry
    Dim isNewEntry As Boolean = True
    Dim ErrorControl As New clsErrorControl()
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub frmSeedSelectionEntry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New ")
        Addnew()
        btnPost.Visible = True
        btnPost.Enabled = False
    End Sub

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
        RadSplitButton1.Visible = MyBase.isExport
        btnPost.Visible = MyBase.isPostFlag
        If MyBase.isExport = True Then
            rmExport.Enabled = True
            rmimport.Enabled = True
        Else
            rmExport.Enabled = False
            rmimport.Enabled = False
        End If

    End Sub

    Private Sub Addnew()

    End Sub

End Class