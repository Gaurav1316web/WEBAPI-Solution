<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmBarCodeGenerator1
    Inherits FrmMainTranScreen

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
        Me.chkShowBarCode = New Telerik.WinControls.UI.RadCheckBox
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.lblIName = New common.Controls.MyLabel
        Me.txtItemCode = New common.UserControls.txtFinder
        Me.lblItemType = New common.Controls.MyLabel
        Me.dgvItem = New common.UserControls.MyRadGridView
        Me.btnSelect = New Telerik.WinControls.UI.RadButton
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnPrint = New Telerik.WinControls.UI.RadButton
        CType(Me.chkShowBarCode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.lblIName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblItemType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvItem.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'chkShowBarCode
        '
        Me.chkShowBarCode.Location = New System.Drawing.Point(675, 5)
        Me.chkShowBarCode.Name = "chkShowBarCode"
        Me.chkShowBarCode.Size = New System.Drawing.Size(96, 18)
        Me.chkShowBarCode.TabIndex = 1
        Me.chkShowBarCode.Text = "Show Bar Code"
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSelect)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Size = New System.Drawing.Size(869, 371)
        Me.SplitContainer1.SplitterDistance = 342
        Me.SplitContainer1.TabIndex = 3
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblIName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtItemCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkShowBarCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblItemType)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.dgvItem)
        Me.SplitContainer2.Size = New System.Drawing.Size(869, 342)
        Me.SplitContainer2.SplitterDistance = 30
        Me.SplitContainer2.TabIndex = 1
        '
        'lblIName
        '
        Me.lblIName.AutoSize = False
        Me.lblIName.BorderVisible = True
        Me.lblIName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIName.Location = New System.Drawing.Point(203, 6)
        Me.lblIName.Name = "lblIName"
        Me.lblIName.Size = New System.Drawing.Size(446, 18)
        Me.lblIName.TabIndex = 38
        Me.lblIName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblIName.TextWrap = False
        '
        'txtItemCode
        '
        Me.txtItemCode.Location = New System.Drawing.Point(77, 6)
        Me.txtItemCode.MendatroryField = True
        Me.txtItemCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemCode.MyLinkLable1 = Nothing
        Me.txtItemCode.MyLinkLable2 = Me.lblIName
        Me.txtItemCode.MyReadOnly = False
        Me.txtItemCode.Name = "txtItemCode"
        Me.txtItemCode.Size = New System.Drawing.Size(123, 18)
        Me.txtItemCode.TabIndex = 37
        Me.txtItemCode.Value = ""
        '
        'lblItemType
        '
        Me.lblItemType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItemType.Location = New System.Drawing.Point(6, 6)
        Me.lblItemType.Name = "lblItemType"
        Me.lblItemType.Size = New System.Drawing.Size(58, 16)
        Me.lblItemType.TabIndex = 36
        Me.lblItemType.Text = "Item Code"
        '
        'dgvItem
        '
        Me.dgvItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.dgvItem.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgvItem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvItem.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.dgvItem.ForeColor = System.Drawing.Color.Black
        Me.dgvItem.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dgvItem.Location = New System.Drawing.Point(0, 0)
        '
        'dgvItem
        '
        Me.dgvItem.MasterTemplate.AllowDeleteRow = False
        Me.dgvItem.MasterTemplate.EnableFiltering = True
        Me.dgvItem.Name = "dgvItem"
        Me.dgvItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.dgvItem.ShowGroupPanel = False
        Me.dgvItem.Size = New System.Drawing.Size(869, 308)
        Me.dgvItem.TabIndex = 1
        Me.dgvItem.Text = "RadGridView1"
        '
        'btnSelect
        '
        Me.btnSelect.Location = New System.Drawing.Point(217, 4)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(76, 18)
        Me.btnSelect.TabIndex = 3
        Me.btnSelect.Text = "Select All"
        '
        'btnReset
        '
        Me.btnReset.Location = New System.Drawing.Point(6, 4)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(76, 18)
        Me.btnReset.TabIndex = 2
        Me.btnReset.Text = "Reset"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(798, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "Close"
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(88, 4)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(123, 18)
        Me.btnPrint.TabIndex = 0
        Me.btnPrint.Text = "Generate  Bar  Code"
        '
        'FrmBarCodeGenerator1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(869, 371)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmBarCodeGenerator1"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmBarCodeGenerator1"
        CType(Me.chkShowBarCode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.lblIName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblItemType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvItem.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents chkShowBarCode As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblItemType As common.Controls.MyLabel
    Friend WithEvents dgvItem As common.UserControls.MyRadGridView
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblIName As common.Controls.MyLabel
    Friend WithEvents txtItemCode As common.UserControls.txtFinder
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSelect As Telerik.WinControls.UI.RadButton
End Class

