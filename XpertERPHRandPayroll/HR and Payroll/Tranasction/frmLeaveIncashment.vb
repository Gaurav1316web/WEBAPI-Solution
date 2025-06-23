Imports common
Imports XpertERPEngine
Imports XpertERPEngineFine
Public Class frmLeaveIncashment
#Region "Variables"
    Dim isNewEntry As Boolean = False
#End Region
    Private Sub SetUserMgmtNew()
        Me.Form_ID = clsUserMgtCode.frmLeaveIncashment
        MyBase.SetUserMgmt(clsUserMgtCode.frmLeaveIncashment)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPost.Visible = MyBase.isPostFlag
        btnPrint.Visible = MyBase.isPrintFlag
        If MyBase.isReverse Then
            btnReverse.Enabled = True
        End If
    End Sub

    Private Sub frmLeaveIncashment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        AddNew()
    End Sub
    Private Sub AddNew()
        LoadBlankGrid()
        LoadDocType()
    End Sub
    Private Sub LoadBlankGrid()
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        'Dim TargetDetail As New GridViewCheckBoxColumn
        'TargetDetail.FormatString = ""
        'TargetDetail.HeaderText = "Is Detail"
        'TargetDetail.Name = colTargetDetail
        'TargetDetail.Width = 80
        'TargetDetail.ReadOnly = False
        'TargetDetail.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        'gv1.Columns.Add(TargetDetail)
        'gvTargetTables.EnableCustomFiltering = False
        'gvTargetTables.EnableFiltering = True
        'Dim lineNo As New GridViewTextBoxColumn()
        'lineNo.FormatString = ""
        'lineNo.HeaderText = "Line No"
        'lineNo.Name = "LineNo"
        'lineNo.Width = 30
        'lineNo.ReadOnly = True
        'lineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gv1.Columns.Add(lineNo)

    End Sub
    Private Sub LoadDocType()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = Nothing

        dr = dt.NewRow()
        dr("Code") = "Leave Incashment"
        dr("Name") = "Leave Incashment"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "Surrender leave"
        dr("Name") = "Surrender leave"
        dt.Rows.Add(dr)
        cmbDocType.DataSource = dt
        cmbDocType.ValueMember = "Code"
        cmbDocType.DisplayMember = "Name"
    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub
End Class