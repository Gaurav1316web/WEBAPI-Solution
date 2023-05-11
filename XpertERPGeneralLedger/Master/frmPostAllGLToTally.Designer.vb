<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPostAllGLToTally
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
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel
        Me.btnUNSELECT = New Telerik.WinControls.UI.RadButton
        Me.btnSelect = New Telerik.WinControls.UI.RadButton
        Me.btnsave = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.BtnShow = New Telerik.WinControls.UI.RadButton
        Me.lblRecordNo = New common.Controls.MyLabel
        Me.dtfrom = New common.Controls.MyDateTimePicker
        Me.RadLabel3 = New common.Controls.MyLabel
        Me.dtTo = New common.Controls.MyDateTimePicker
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.dgvAccountMap = New common.UserControls.MyRadGridView
        Me.chkShowAll = New Telerik.WinControls.UI.RadCheckBox
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.btnUNSELECT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnShow, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRecordNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtfrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvAccountMap, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvAccountMap.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkShowAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPanel1
        '
        Me.RadPanel1.Controls.Add(Me.btnUNSELECT)
        Me.RadPanel1.Controls.Add(Me.btnSelect)
        Me.RadPanel1.Controls.Add(Me.btnsave)
        Me.RadPanel1.Controls.Add(Me.btnClose)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.RadPanel1.Location = New System.Drawing.Point(0, 385)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(509, 25)
        Me.RadPanel1.TabIndex = 1
        '
        'btnUNSELECT
        '
        Me.btnUNSELECT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnUNSELECT.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUNSELECT.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnUNSELECT.Location = New System.Drawing.Point(75, 3)
        Me.btnUNSELECT.Name = "btnUNSELECT"
        Me.btnUNSELECT.Size = New System.Drawing.Size(66, 18)
        Me.btnUNSELECT.TabIndex = 3
        Me.btnUNSELECT.Text = "Unselect All"
        '
        'btnSelect
        '
        Me.btnSelect.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSelect.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSelect.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnSelect.Location = New System.Drawing.Point(7, 3)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(66, 18)
        Me.btnSelect.TabIndex = 2
        Me.btnSelect.Text = "Select All"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnsave.Location = New System.Drawing.Point(143, 3)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Post"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClose.Location = New System.Drawing.Point(431, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(66, 18)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "Close"
        '
        'BtnShow
        '
        Me.BtnShow.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnShow.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.BtnShow.Location = New System.Drawing.Point(354, 8)
        Me.BtnShow.Name = "BtnShow"
        Me.BtnShow.Size = New System.Drawing.Size(66, 18)
        Me.BtnShow.TabIndex = 2
        Me.BtnShow.Text = ">>"
        '
        'lblRecordNo
        '
        Me.lblRecordNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRecordNo.Location = New System.Drawing.Point(571, 14)
        Me.lblRecordNo.Name = "lblRecordNo"
        Me.lblRecordNo.Size = New System.Drawing.Size(113, 16)
        Me.lblRecordNo.TabIndex = 46
        Me.lblRecordNo.Text = "Total Record Found :"
        '
        'dtfrom
        '
        Me.dtfrom.CustomFormat = "dd/MM/yyyy"
        Me.dtfrom.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtfrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtfrom.Location = New System.Drawing.Point(64, 7)
        Me.dtfrom.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.dtfrom.MendatroryField = False
        Me.dtfrom.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtfrom.MyLinkLable1 = Nothing
        Me.dtfrom.MyLinkLable2 = Nothing
        Me.dtfrom.Name = "dtfrom"
        Me.dtfrom.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtfrom.Size = New System.Drawing.Size(77, 18)
        Me.dtfrom.TabIndex = 47
        Me.dtfrom.Text = "RadDateTimePicker1"
        Me.dtfrom.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel3
        '
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(4, 8)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(60, 16)
        Me.RadLabel3.TabIndex = 48
        Me.RadLabel3.Text = "From Date"
        '
        'dtTo
        '
        Me.dtTo.CustomFormat = "dd/MM/yyyy"
        Me.dtTo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtTo.Location = New System.Drawing.Point(200, 7)
        Me.dtTo.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.dtTo.MendatroryField = False
        Me.dtTo.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtTo.MyLinkLable1 = Nothing
        Me.dtTo.MyLinkLable2 = Nothing
        Me.dtTo.Name = "dtTo"
        Me.dtTo.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtTo.Size = New System.Drawing.Size(77, 18)
        Me.dtTo.TabIndex = 49
        Me.dtTo.Text = "RadDateTimePicker1"
        Me.dtTo.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(153, 8)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(46, 16)
        Me.MyLabel1.TabIndex = 50
        Me.MyLabel1.Text = "To Date"
        '
        'dgvAccountMap
        '
        Me.dgvAccountMap.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvAccountMap.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvAccountMap.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgvAccountMap.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.dgvAccountMap.ForeColor = System.Drawing.Color.Black
        Me.dgvAccountMap.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dgvAccountMap.Location = New System.Drawing.Point(0, 32)
        '
        'dgvAccountMap
        '
        Me.dgvAccountMap.MasterTemplate.EnableFiltering = True
        Me.dgvAccountMap.MasterTemplate.EnableGrouping = False
        Me.dgvAccountMap.Name = "dgvAccountMap"
        Me.dgvAccountMap.RightToLeft = System.Windows.Forms.RightToLeft.No
        '
        '
        '
        Me.dgvAccountMap.RootElement.ForeColor = System.Drawing.Color.Black
        Me.dgvAccountMap.Size = New System.Drawing.Size(507, 348)
        Me.dgvAccountMap.TabIndex = 51
        '
        'chkShowAll
        '
        Me.chkShowAll.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkShowAll.Location = New System.Drawing.Point(283, 8)
        Me.chkShowAll.Name = "chkShowAll"
        '
        '
        '
        Me.chkShowAll.RootElement.StretchHorizontally = True
        Me.chkShowAll.RootElement.StretchVertically = True
        Me.chkShowAll.Size = New System.Drawing.Size(89, 16)
        Me.chkShowAll.TabIndex = 52
        Me.chkShowAll.Text = "Show All"
        Me.chkShowAll.Visible = False
        '
        'frmPostAllGLToTally
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(509, 410)
        Me.ControlBox = False
        Me.Controls.Add(Me.BtnShow)
        Me.Controls.Add(Me.chkShowAll)
        Me.Controls.Add(Me.dgvAccountMap)
        Me.Controls.Add(Me.dtTo)
        Me.Controls.Add(Me.MyLabel1)
        Me.Controls.Add(Me.dtfrom)
        Me.Controls.Add(Me.RadLabel3)
        Me.Controls.Add(Me.lblRecordNo)
        Me.Controls.Add(Me.RadPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmPostAllGLToTally"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Post All GL To Tally"
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        CType(Me.btnUNSELECT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnShow, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRecordNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtfrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvAccountMap.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvAccountMap, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkShowAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblRecordNo As common.Controls.MyLabel
    Friend WithEvents dtfrom As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents dtTo As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents BtnShow As Telerik.WinControls.UI.RadButton
    Friend WithEvents dgvAccountMap As common.UserControls.MyRadGridView
    Friend WithEvents chkShowAll As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents btnUNSELECT As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSelect As Telerik.WinControls.UI.RadButton
End Class

