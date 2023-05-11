<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmPrimaryTransporterProvisionCorrection
    Inherits FrmMainTranScreen

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem6 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem5 = New Telerik.WinControls.UI.RadMenuItem()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.GroupBox76 = New System.Windows.Forms.GroupBox()
        Me.TxtMultiSelectFinder8 = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel40 = New common.Controls.MyLabel()
        Me.txtMCCToDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel39 = New common.Controls.MyLabel()
        Me.txtMCCFromDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel41 = New common.Controls.MyLabel()
        Me.BulkDelete = New Telerik.WinControls.UI.RadButton()
        Me.gv1 = New Telerik.WinControls.UI.RadGridView()
        Me.txtFromShift = New Telerik.WinControls.UI.RadDropDownList()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.Panel2.SuspendLayout()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.GroupBox76.SuspendLayout()
        CType(Me.MyLabel40, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMCCToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel39, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMCCFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel41, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BulkDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromShift, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.btnClose)
        Me.Panel2.Controls.Add(Me.btnSave)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 359)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(672, 26)
        Me.Panel2.TabIndex = 1
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(572, 2)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(97, 22)
        Me.btnClose.TabIndex = 6
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(4, 2)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(97, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(672, 20)
        Me.RadMenu1.TabIndex = 3
        Me.RadMenu1.Text = "RadMenu1"
        Me.RadMenu1.Visible = False
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Setting"
        Me.RadMenuItem3.AccessibleName = "Setting"
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItem2, Me.RadMenuItem4, Me.RadMenuItem6, Me.RadMenuItem5})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Save Layout"
        Me.RadMenuItem1.AccessibleName = "Save Layout"
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Save Layout"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Delete Layout"
        Me.RadMenuItem2.AccessibleName = "Delete Layout"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Delete Layout"
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.AccessibleDescription = "Export"
        Me.RadMenuItem4.AccessibleName = "Export"
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Export Blank"
        '
        'RadMenuItem6
        '
        Me.RadMenuItem6.AccessibleDescription = "Export Grid"
        Me.RadMenuItem6.AccessibleName = "Export Grid"
        Me.RadMenuItem6.Name = "RadMenuItem6"
        Me.RadMenuItem6.Text = "Export Grid"
        '
        'RadMenuItem5
        '
        Me.RadMenuItem5.AccessibleDescription = "Import"
        Me.RadMenuItem5.AccessibleName = "Import"
        Me.RadMenuItem5.Name = "RadMenuItem5"
        Me.RadMenuItem5.Text = "Import"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.GroupBox76)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(672, 75)
        Me.Panel1.TabIndex = 0
        '
        'GroupBox76
        '
        Me.GroupBox76.Controls.Add(Me.MyLabel1)
        Me.GroupBox76.Controls.Add(Me.txtFromShift)
        Me.GroupBox76.Controls.Add(Me.TxtMultiSelectFinder8)
        Me.GroupBox76.Controls.Add(Me.MyLabel40)
        Me.GroupBox76.Controls.Add(Me.txtMCCToDate)
        Me.GroupBox76.Controls.Add(Me.MyLabel39)
        Me.GroupBox76.Controls.Add(Me.txtMCCFromDate)
        Me.GroupBox76.Controls.Add(Me.MyLabel41)
        Me.GroupBox76.Controls.Add(Me.BulkDelete)
        Me.GroupBox76.Location = New System.Drawing.Point(4, 3)
        Me.GroupBox76.Name = "GroupBox76"
        Me.GroupBox76.Size = New System.Drawing.Size(400, 67)
        Me.GroupBox76.TabIndex = 0
        Me.GroupBox76.TabStop = False
        '
        'TxtMultiSelectFinder8
        '
        Me.TxtMultiSelectFinder8.arrDispalyMember = Nothing
        Me.TxtMultiSelectFinder8.arrValueMember = Nothing
        Me.TxtMultiSelectFinder8.Location = New System.Drawing.Point(40, 40)
        Me.TxtMultiSelectFinder8.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMultiSelectFinder8.MyLinkLable1 = Nothing
        Me.TxtMultiSelectFinder8.MyLinkLable2 = Nothing
        Me.TxtMultiSelectFinder8.MyNullText = "Please Select..."
        Me.TxtMultiSelectFinder8.Name = "TxtMultiSelectFinder8"
        Me.TxtMultiSelectFinder8.Size = New System.Drawing.Size(312, 19)
        Me.TxtMultiSelectFinder8.TabIndex = 3
        '
        'MyLabel40
        '
        Me.MyLabel40.FieldName = Nothing
        Me.MyLabel40.Location = New System.Drawing.Point(129, 19)
        Me.MyLabel40.Name = "MyLabel40"
        Me.MyLabel40.Size = New System.Drawing.Size(17, 18)
        Me.MyLabel40.TabIndex = 6
        Me.MyLabel40.Text = "to"
        '
        'txtMCCToDate
        '
        Me.txtMCCToDate.CalculationExpression = Nothing
        Me.txtMCCToDate.CustomFormat = "dd/MMM/yyyy"
        Me.txtMCCToDate.FieldCode = Nothing
        Me.txtMCCToDate.FieldDesc = Nothing
        Me.txtMCCToDate.FieldMaxLength = 0
        Me.txtMCCToDate.FieldName = Nothing
        Me.txtMCCToDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMCCToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtMCCToDate.isCalculatedField = False
        Me.txtMCCToDate.IsSourceFromTable = False
        Me.txtMCCToDate.IsSourceFromValueList = False
        Me.txtMCCToDate.IsUnique = False
        Me.txtMCCToDate.Location = New System.Drawing.Point(149, 19)
        Me.txtMCCToDate.MendatroryField = False
        Me.txtMCCToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtMCCToDate.MyLinkLable1 = Nothing
        Me.txtMCCToDate.MyLinkLable2 = Nothing
        Me.txtMCCToDate.Name = "txtMCCToDate"
        Me.txtMCCToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtMCCToDate.ReferenceFieldDesc = Nothing
        Me.txtMCCToDate.ReferenceFieldName = Nothing
        Me.txtMCCToDate.ReferenceTableName = Nothing
        Me.txtMCCToDate.Size = New System.Drawing.Size(87, 18)
        Me.txtMCCToDate.TabIndex = 1
        Me.txtMCCToDate.TabStop = False
        Me.txtMCCToDate.Text = "13/Jun/2011"
        Me.txtMCCToDate.Value = New Date(2011, 6, 13, 0, 0, 0, 0)
        '
        'MyLabel39
        '
        Me.MyLabel39.FieldName = Nothing
        Me.MyLabel39.Location = New System.Drawing.Point(4, 19)
        Me.MyLabel39.Name = "MyLabel39"
        Me.MyLabel39.Size = New System.Drawing.Size(30, 18)
        Me.MyLabel39.TabIndex = 7
        Me.MyLabel39.Text = "Date"
        '
        'txtMCCFromDate
        '
        Me.txtMCCFromDate.CalculationExpression = Nothing
        Me.txtMCCFromDate.CustomFormat = "dd/MMM/yyyy"
        Me.txtMCCFromDate.FieldCode = Nothing
        Me.txtMCCFromDate.FieldDesc = Nothing
        Me.txtMCCFromDate.FieldMaxLength = 0
        Me.txtMCCFromDate.FieldName = Nothing
        Me.txtMCCFromDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMCCFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtMCCFromDate.isCalculatedField = False
        Me.txtMCCFromDate.IsSourceFromTable = False
        Me.txtMCCFromDate.IsSourceFromValueList = False
        Me.txtMCCFromDate.IsUnique = False
        Me.txtMCCFromDate.Location = New System.Drawing.Point(40, 19)
        Me.txtMCCFromDate.MendatroryField = False
        Me.txtMCCFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtMCCFromDate.MyLinkLable1 = Nothing
        Me.txtMCCFromDate.MyLinkLable2 = Nothing
        Me.txtMCCFromDate.Name = "txtMCCFromDate"
        Me.txtMCCFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtMCCFromDate.ReferenceFieldDesc = Nothing
        Me.txtMCCFromDate.ReferenceFieldName = Nothing
        Me.txtMCCFromDate.ReferenceTableName = Nothing
        Me.txtMCCFromDate.Size = New System.Drawing.Size(87, 18)
        Me.txtMCCFromDate.TabIndex = 0
        Me.txtMCCFromDate.TabStop = False
        Me.txtMCCFromDate.Text = "13/Jun/2011"
        Me.txtMCCFromDate.Value = New Date(2011, 6, 13, 0, 0, 0, 0)
        '
        'MyLabel41
        '
        Me.MyLabel41.FieldName = Nothing
        Me.MyLabel41.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel41.Location = New System.Drawing.Point(5, 41)
        Me.MyLabel41.Name = "MyLabel41"
        Me.MyLabel41.Size = New System.Drawing.Size(32, 16)
        Me.MyLabel41.TabIndex = 8
        Me.MyLabel41.Text = "MCC"
        '
        'BulkDelete
        '
        Me.BulkDelete.Location = New System.Drawing.Point(354, 19)
        Me.BulkDelete.Name = "BulkDelete"
        Me.BulkDelete.Size = New System.Drawing.Size(40, 38)
        Me.BulkDelete.TabIndex = 4
        Me.BulkDelete.Text = ">>"
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(0, 95)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.EnableAlternatingRowColor = True
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.Size = New System.Drawing.Size(672, 264)
        Me.gv1.TabIndex = 2
        Me.gv1.Text = "RadGridView1"
        '
        'txtFromShift
        '
        Me.txtFromShift.AutoCompleteDisplayMember = Nothing
        Me.txtFromShift.AutoCompleteValueMember = Nothing
        Me.txtFromShift.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.txtFromShift.Location = New System.Drawing.Point(272, 18)
        Me.txtFromShift.Name = "txtFromShift"
        Me.txtFromShift.Size = New System.Drawing.Size(80, 20)
        Me.txtFromShift.TabIndex = 2
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(240, 19)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(29, 18)
        Me.MyLabel1.TabIndex = 5
        Me.MyLabel1.Text = "Shift"
        '
        'frmPrimaryTransporterProvisionCorrection
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(672, 385)
        Me.ControlBox = False
        Me.Controls.Add(Me.gv1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Controls.Add(Me.Panel2)
        Me.Name = "frmPrimaryTransporterProvisionCorrection"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Milk Shift Uploader"
        Me.Panel2.ResumeLayout(False)
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox76.ResumeLayout(False)
        Me.GroupBox76.PerformLayout()
        CType(Me.MyLabel40, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMCCToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel39, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMCCFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel41, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BulkDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromShift, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents gv1 As Telerik.WinControls.UI.RadGridView
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenuItem4 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem5 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem6 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents GroupBox76 As GroupBox
    Friend WithEvents TxtMultiSelectFinder8 As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel40 As common.Controls.MyLabel
    Friend WithEvents txtMCCToDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel39 As common.Controls.MyLabel
    Friend WithEvents txtMCCFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel41 As common.Controls.MyLabel
    Friend WithEvents BulkDelete As RadButton
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtFromShift As RadDropDownList
End Class

