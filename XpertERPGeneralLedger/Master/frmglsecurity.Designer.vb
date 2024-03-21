<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frmglsecurity
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
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.lbluserid = New common.Controls.MyLabel()
        Me.btnreset = New Telerik.WinControls.UI.RadButton()
        Me.txtname = New common.Controls.MyTextBox()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.dgvsegment = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.dgvaccount = New common.UserControls.MyRadGridView()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RMIImportSegment = New Telerik.WinControls.UI.RadMenuItem()
        Me.RMIImportAccount = New Telerik.WinControls.UI.RadMenuItem()
        Me.ExportSegmentFormat = New Telerik.WinControls.UI.RadMenuItem()
        Me.ExportFormatAccount = New Telerik.WinControls.UI.RadMenuItem()
        Me.RMIExit = New Telerik.WinControls.UI.RadMenuItem()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.fndUserCode = New common.UserControls.txtNavigator()
        CType(Me.lbluserid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.dgvsegment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvsegment.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.dgvaccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvaccount.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbluserid
        '
        Me.lbluserid.FieldName = Nothing
        Me.lbluserid.Location = New System.Drawing.Point(3, 3)
        Me.lbluserid.Name = "lbluserid"
        Me.lbluserid.Size = New System.Drawing.Size(41, 18)
        Me.lbluserid.TabIndex = 0
        Me.lbluserid.Text = "User Id"
        '
        'btnreset
        '
        Me.btnreset.Image = Global.XpertERPGeneralLedger.My.Resources.Resources._new
        Me.btnreset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnreset.Location = New System.Drawing.Point(348, 3)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(25, 20)
        Me.btnreset.TabIndex = 0
        '
        'txtname
        '
        Me.txtname.CalculationExpression = Nothing
        Me.txtname.FieldCode = Nothing
        Me.txtname.FieldDesc = Nothing
        Me.txtname.FieldMaxLength = 0
        Me.txtname.FieldName = Nothing
        Me.txtname.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtname.isCalculatedField = False
        Me.txtname.IsSourceFromTable = False
        Me.txtname.IsSourceFromValueList = False
        Me.txtname.IsUnique = False
        Me.txtname.Location = New System.Drawing.Point(393, 3)
        Me.txtname.MendatroryField = False
        Me.txtname.MyLinkLable1 = Me.lbluserid
        Me.txtname.MyLinkLable2 = Nothing
        Me.txtname.Name = "txtname"
        Me.txtname.ReadOnly = True
        Me.txtname.ReferenceFieldDesc = Nothing
        Me.txtname.ReferenceFieldName = Nothing
        Me.txtname.ReferenceTableName = Nothing
        Me.txtname.Size = New System.Drawing.Size(384, 18)
        Me.txtname.TabIndex = 1
        Me.txtname.TabStop = False
        '
        'RadPageView1
        '
        Me.RadPageView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Location = New System.Drawing.Point(1, 59)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(817, 351)
        Me.RadPageView1.TabIndex = 0
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.dgvsegment)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(61.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(796, 303)
        Me.RadPageViewPage1.Text = "Segment"
        '
        'dgvsegment
        '
        Me.dgvsegment.BackColor = System.Drawing.Color.White
        Me.dgvsegment.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgvsegment.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvsegment.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.dgvsegment.ForeColor = System.Drawing.SystemColors.ControlText
        Me.dgvsegment.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dgvsegment.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.dgvsegment.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.dgvsegment.MasterTemplate.AllowAddNewRow = False
        Me.dgvsegment.MasterTemplate.EnableGrouping = False
        Me.dgvsegment.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.dgvsegment.MasterTemplate.ShowHeaderCellButtons = True
        Me.dgvsegment.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.dgvsegment.MyStopExport = False
        Me.dgvsegment.Name = "dgvsegment"
        Me.dgvsegment.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.dgvsegment.ShowHeaderCellButtons = True
        Me.dgvsegment.Size = New System.Drawing.Size(796, 303)
        Me.dgvsegment.TabIndex = 0
        Me.dgvsegment.TabStop = False
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.dgvaccount)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(57.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(796, 331)
        Me.RadPageViewPage2.Text = "Account"
        '
        'dgvaccount
        '
        Me.dgvaccount.BackColor = System.Drawing.Color.White
        Me.dgvaccount.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgvaccount.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvaccount.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.dgvaccount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.dgvaccount.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dgvaccount.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.dgvaccount.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.dgvaccount.MasterTemplate.AllowAddNewRow = False
        Me.dgvaccount.MasterTemplate.EnableGrouping = False
        Me.dgvaccount.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.dgvaccount.MasterTemplate.ShowHeaderCellButtons = True
        Me.dgvaccount.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.dgvaccount.MyStopExport = False
        Me.dgvaccount.Name = "dgvaccount"
        Me.dgvaccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.dgvaccount.ShowHeaderCellButtons = True
        Me.dgvaccount.Size = New System.Drawing.Size(796, 331)
        Me.dgvaccount.TabIndex = 0
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(748, 5)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(77, 3)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(7, 3)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(819, 445)
        Me.SplitContainer1.SplitterDistance = 415
        Me.SplitContainer1.TabIndex = 10
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(819, 20)
        Me.RadMenu1.TabIndex = 75
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RMIImportSegment, Me.RMIImportAccount, Me.ExportSegmentFormat, Me.ExportFormatAccount, Me.RMIExit})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'RMIImportSegment
        '
        Me.RMIImportSegment.Name = "RMIImportSegment"
        Me.RMIImportSegment.Text = "Import GL Segment"
        '
        'RMIImportAccount
        '
        Me.RMIImportAccount.Name = "RMIImportAccount"
        Me.RMIImportAccount.Text = "Import GL Account"
        '
        'ExportSegmentFormat
        '
        Me.ExportSegmentFormat.Name = "ExportSegmentFormat"
        Me.ExportSegmentFormat.Text = "Export Format (GL Segment)"
        '
        'ExportFormatAccount
        '
        Me.ExportFormatAccount.Name = "ExportFormatAccount"
        Me.ExportFormatAccount.Text = "Export Format (Account)"
        '
        'RMIExit
        '
        Me.RMIExit.Name = "RMIExit"
        Me.RMIExit.Text = "Exit"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.fndUserCode)
        Me.Panel1.Controls.Add(Me.lbluserid)
        Me.Panel1.Controls.Add(Me.btnreset)
        Me.Panel1.Controls.Add(Me.txtname)
        Me.Panel1.Location = New System.Drawing.Point(0, 29)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(819, 24)
        Me.Panel1.TabIndex = 0
        '
        'fndUserCode
        '
        Me.fndUserCode.FieldName = Nothing
        Me.fndUserCode.Location = New System.Drawing.Point(82, 2)
        Me.fndUserCode.MendatroryField = True
        Me.fndUserCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndUserCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndUserCode.MyLinkLable1 = Me.lbluserid
        Me.fndUserCode.MyLinkLable2 = Nothing
        Me.fndUserCode.MyMaxLength = 30
        Me.fndUserCode.MyReadOnly = False
        Me.fndUserCode.Name = "fndUserCode"
        Me.fndUserCode.Size = New System.Drawing.Size(264, 21)
        Me.fndUserCode.TabIndex = 0
        Me.fndUserCode.Value = ""
        '
        'Frmglsecurity
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(819, 445)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "Frmglsecurity"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "GL Security"
        CType(Me.lbluserid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.dgvsegment.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvsegment, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.dgvaccount.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvaccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtname As common.Controls.MyTextBox
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents dgvsegment As common.UserControls.MyRadGridView
    Friend WithEvents dgvaccount As common.UserControls.MyRadGridView
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents fndUserCode As common.UserControls.txtNavigator
    Friend WithEvents MasterTemplate As common.UserControls.MyRadGridView
    Friend WithEvents lbluserid As common.Controls.MyLabel
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RMIImportSegment As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RMIImportAccount As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents ExportSegmentFormat As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents ExportFormatAccount As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem6 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RMIExit As Telerik.WinControls.UI.RadMenuItem
End Class

