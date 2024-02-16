<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBulkPurchaseUploader
    Inherits XpertERPEngine.FrmMainTranScreen

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim TableViewDefinition5 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rdbAgainstBulkSaleTrade = New Telerik.WinControls.UI.RadRadioButton()
        Me.rdbAgainstBulkSale = New Telerik.WinControls.UI.RadRadioButton()
        Me.rdbAgainstBulkprocurement = New Telerik.WinControls.UI.RadRadioButton()
        Me.Gv1 = New common.UserControls.MyRadGridView()
        Me.btnMergeAndRecreate = New Telerik.WinControls.UI.RadButton()
        Me.btnMergeAndRecreateTrade = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSaveAndPost = New Telerik.WinControls.UI.RadButton()
        Me.btnExportInvalid = New Telerik.WinControls.UI.RadButton()
        Me.btnValidate = New Telerik.WinControls.UI.RadButton()
        Me.btnExportFormat = New Telerik.WinControls.UI.RadButton()
        Me.btnSelectSheet = New Telerik.WinControls.UI.RadButton()
        Me.btReset = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.rdbAgainstBulkSaleTrade, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbAgainstBulkSale, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbAgainstBulkprocurement, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnMergeAndRecreate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnMergeAndRecreateTrade, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSaveAndPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExportInvalid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnValidate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExportFormat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSelectSheet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnMergeAndRecreate)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnMergeAndRecreateTrade)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSaveAndPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExportInvalid)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnValidate)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExportFormat)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSelectSheet)
        Me.SplitContainer1.Size = New System.Drawing.Size(886, 472)
        Me.SplitContainer1.SplitterDistance = 435
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadGroupBox3)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.Gv1)
        Me.SplitContainer2.Size = New System.Drawing.Size(886, 435)
        Me.SplitContainer2.SplitterDistance = 42
        Me.SplitContainer2.TabIndex = 3
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.rdbAgainstBulkSaleTrade)
        Me.RadGroupBox3.Controls.Add(Me.rdbAgainstBulkSale)
        Me.RadGroupBox3.Controls.Add(Me.rdbAgainstBulkprocurement)
        Me.RadGroupBox3.HeaderText = ""
        Me.RadGroupBox3.Location = New System.Drawing.Point(12, 12)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(434, 23)
        Me.RadGroupBox3.TabIndex = 1
        '
        'rdbAgainstBulkSaleTrade
        '
        Me.rdbAgainstBulkSaleTrade.Location = New System.Drawing.Point(285, 2)
        Me.rdbAgainstBulkSaleTrade.Name = "rdbAgainstBulkSaleTrade"
        Me.rdbAgainstBulkSaleTrade.Size = New System.Drawing.Size(137, 18)
        Me.rdbAgainstBulkSaleTrade.TabIndex = 2
        Me.rdbAgainstBulkSaleTrade.Text = "Against Bulk Sale Trade"
        Me.rdbAgainstBulkSaleTrade.Visible = False
        '
        'rdbAgainstBulkSale
        '
        Me.rdbAgainstBulkSale.Location = New System.Drawing.Point(167, 2)
        Me.rdbAgainstBulkSale.Name = "rdbAgainstBulkSale"
        Me.rdbAgainstBulkSale.Size = New System.Drawing.Size(106, 18)
        Me.rdbAgainstBulkSale.TabIndex = 1
        Me.rdbAgainstBulkSale.Text = "Against Bulk Sale"
        '
        'rdbAgainstBulkprocurement
        '
        Me.rdbAgainstBulkprocurement.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rdbAgainstBulkprocurement.Location = New System.Drawing.Point(6, 2)
        Me.rdbAgainstBulkprocurement.Name = "rdbAgainstBulkprocurement"
        Me.rdbAgainstBulkprocurement.Size = New System.Drawing.Size(150, 18)
        Me.rdbAgainstBulkprocurement.TabIndex = 0
        Me.rdbAgainstBulkprocurement.Text = "Against Bulk Procurement"
        Me.rdbAgainstBulkprocurement.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'Gv1
        '
        Me.Gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.Gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.Gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.Gv1.MasterTemplate.ViewDefinition = TableViewDefinition5
        Me.Gv1.Name = "Gv1"
        Me.Gv1.ShowHeaderCellButtons = True
        Me.Gv1.Size = New System.Drawing.Size(886, 389)
        Me.Gv1.TabIndex = 2
        Me.Gv1.Visible = False
        '
        'btnMergeAndRecreate
        '
        Me.btnMergeAndRecreate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnMergeAndRecreate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMergeAndRecreate.Location = New System.Drawing.Point(536, 9)
        Me.btnMergeAndRecreate.Name = "btnMergeAndRecreate"
        Me.btnMergeAndRecreate.Size = New System.Drawing.Size(231, 18)
        Me.btnMergeAndRecreate.TabIndex = 14
        Me.btnMergeAndRecreate.Text = "Merger and Recreate Inv"
        '
        'btnMergeAndRecreateTrade
        '
        Me.btnMergeAndRecreateTrade.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnMergeAndRecreateTrade.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMergeAndRecreateTrade.Location = New System.Drawing.Point(536, 9)
        Me.btnMergeAndRecreateTrade.Name = "btnMergeAndRecreateTrade"
        Me.btnMergeAndRecreateTrade.Size = New System.Drawing.Size(231, 18)
        Me.btnMergeAndRecreateTrade.TabIndex = 14
        Me.btnMergeAndRecreateTrade.Text = "Merger and Recreate Inv Trade"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(817, 9)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(66, 18)
        Me.btnClose.TabIndex = 13
        Me.btnClose.Text = "Close"
        '
        'btnSaveAndPost
        '
        Me.btnSaveAndPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSaveAndPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveAndPost.Location = New System.Drawing.Point(339, 9)
        Me.btnSaveAndPost.Name = "btnSaveAndPost"
        Me.btnSaveAndPost.Size = New System.Drawing.Size(88, 18)
        Me.btnSaveAndPost.TabIndex = 12
        Me.btnSaveAndPost.Text = "Save && Post"
        '
        'btnExportInvalid
        '
        Me.btnExportInvalid.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExportInvalid.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExportInvalid.Location = New System.Drawing.Point(232, 9)
        Me.btnExportInvalid.Name = "btnExportInvalid"
        Me.btnExportInvalid.Size = New System.Drawing.Size(106, 18)
        Me.btnExportInvalid.TabIndex = 11
        Me.btnExportInvalid.Text = "Export Unvalidated"
        '
        'btnValidate
        '
        Me.btnValidate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnValidate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnValidate.Location = New System.Drawing.Point(164, 9)
        Me.btnValidate.Name = "btnValidate"
        Me.btnValidate.Size = New System.Drawing.Size(66, 18)
        Me.btnValidate.TabIndex = 10
        Me.btnValidate.Text = "Validate"
        '
        'btnExportFormat
        '
        Me.btnExportFormat.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExportFormat.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExportFormat.Location = New System.Drawing.Point(80, 9)
        Me.btnExportFormat.Name = "btnExportFormat"
        Me.btnExportFormat.Size = New System.Drawing.Size(83, 18)
        Me.btnExportFormat.TabIndex = 9
        Me.btnExportFormat.Text = "Export Format"
        '
        'btnSelectSheet
        '
        Me.btnSelectSheet.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSelectSheet.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSelectSheet.Location = New System.Drawing.Point(5, 9)
        Me.btnSelectSheet.Name = "btnSelectSheet"
        Me.btnSelectSheet.Size = New System.Drawing.Size(72, 18)
        Me.btnSelectSheet.TabIndex = 8
        Me.btnSelectSheet.Text = "Select Sheet"
        '
        'btReset
        '
        Me.btReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btReset.Location = New System.Drawing.Point(433, 9)
        Me.btReset.Name = "btReset"
        Me.btReset.Size = New System.Drawing.Size(83, 18)
        Me.btReset.TabIndex = 20
        Me.btReset.Text = "Reset"
        '
        'frmBulkPurchaseUploader
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(886, 472)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmBulkPurchaseUploader"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Bulk Purchase\Sale Uploader"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.rdbAgainstBulkSaleTrade, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbAgainstBulkSale, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbAgainstBulkprocurement, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnMergeAndRecreate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnMergeAndRecreateTrade, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSaveAndPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExportInvalid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnValidate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExportFormat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSelectSheet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSaveAndPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnExportInvalid As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnValidate As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnExportFormat As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSelectSheet As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rdbAgainstBulkSale As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbAgainstBulkprocurement As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbAgainstBulkSaleTrade As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents btnMergeAndRecreate As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnMergeAndRecreateTrade As Telerik.WinControls.UI.RadButton
    Friend WithEvents btReset As RadButton
End Class
