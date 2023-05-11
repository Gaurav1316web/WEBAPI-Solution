<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmBudgetMaintenance
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
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem
        Me.btnSave = New Telerik.WinControls.UI.RadButton
        Me.btnDelete = New Telerik.WinControls.UI.RadButton
        Me.fndProject = New common.UserControls.txtFinder
        Me.WIP = New common.Controls.MyLabel
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.rdgpbxcustomeraccountset = New Telerik.WinControls.UI.RadGroupBox
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.txtComments = New Telerik.WinControls.UI.RadTextBox
        Me.txtBudget = New common.MyNumBox
        Me.lblUnitCost = New common.Controls.MyLabel
        Me.txtFromDate = New common.Controls.MyDateTimePicker
        Me.RadLabel4 = New common.Controls.MyLabel
        Me.txtToDate = New common.Controls.MyDateTimePicker
        Me.rdbtnnew = New Telerik.WinControls.UI.RadButton
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.txtProjectdesc = New common.Controls.MyTextBox
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.WIP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.rdgpbxcustomeraccountset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.rdgpbxcustomeraccountset.SuspendLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtComments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBudget, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblUnitCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtProjectdesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenufile})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(638, 20)
        Me.RadMenu1.TabIndex = 4
        Me.RadMenu1.Text = "RadMenu1"
        '
        'rdmenufile
        '
        Me.rdmenufile.AccessibleDescription = "RadMenuItem1"
        Me.rdmenufile.AccessibleName = "RadMenuItem1"
        Me.rdmenufile.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItem2})
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Text = "File"
        Me.rdmenufile.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Export"
        Me.RadMenuItem1.AccessibleName = "Export"
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Export"
        Me.RadMenuItem1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Import"
        Me.RadMenuItem2.AccessibleName = "Import"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Import"
        Me.RadMenuItem2.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(9, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(82, 18)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(102, 4)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(80, 18)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'fndProject
        '
        Me.fndProject.Location = New System.Drawing.Point(114, 36)
        Me.fndProject.MendatroryField = False
        Me.fndProject.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndProject.MyLinkLable1 = Me.WIP
        Me.fndProject.MyLinkLable2 = Nothing
        Me.fndProject.MyReadOnly = False
        Me.fndProject.Name = "fndProject"
        Me.fndProject.Size = New System.Drawing.Size(143, 19)
        Me.fndProject.TabIndex = 2
        Me.fndProject.Value = ""
        '
        'WIP
        '
        Me.WIP.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.WIP.Location = New System.Drawing.Point(13, 37)
        Me.WIP.Name = "WIP"
        Me.WIP.Size = New System.Drawing.Size(41, 16)
        Me.WIP.TabIndex = 11
        Me.WIP.Text = "Project"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdgpbxcustomeraccountset)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(638, 221)
        Me.SplitContainer1.SplitterDistance = 192
        Me.SplitContainer1.TabIndex = 5
        '
        'rdgpbxcustomeraccountset
        '
        Me.rdgpbxcustomeraccountset.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.MyLabel2)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.txtComments)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.txtBudget)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.lblUnitCost)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.txtFromDate)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.txtToDate)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.rdbtnnew)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.MyLabel1)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.RadLabel4)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.WIP)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.txtProjectdesc)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.fndProject)
        Me.rdgpbxcustomeraccountset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdgpbxcustomeraccountset.HeaderText = ""
        Me.rdgpbxcustomeraccountset.Location = New System.Drawing.Point(4, 25)
        Me.rdgpbxcustomeraccountset.Name = "rdgpbxcustomeraccountset"
        Me.rdgpbxcustomeraccountset.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.rdgpbxcustomeraccountset.Size = New System.Drawing.Size(634, 163)
        Me.rdgpbxcustomeraccountset.TabIndex = 0
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(13, 93)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(61, 16)
        Me.MyLabel2.TabIndex = 241
        Me.MyLabel2.Text = "Comments"
        '
        'txtComments
        '
        Me.txtComments.AutoSize = False
        Me.txtComments.Location = New System.Drawing.Point(114, 87)
        Me.txtComments.Multiline = True
        Me.txtComments.Name = "txtComments"
        Me.txtComments.Size = New System.Drawing.Size(487, 61)
        Me.txtComments.TabIndex = 6
        '
        'txtBudget
        '
        Me.txtBudget.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtBudget.DecimalPlaces = 6
        Me.txtBudget.Location = New System.Drawing.Point(114, 61)
        Me.txtBudget.MaxLength = 18
        Me.txtBudget.MendatroryField = True
        Me.txtBudget.MyLinkLable1 = Me.lblUnitCost
        Me.txtBudget.MyLinkLable2 = Nothing
        Me.txtBudget.Name = "txtBudget"
        Me.txtBudget.Size = New System.Drawing.Size(143, 20)
        Me.txtBudget.TabIndex = 5
        Me.txtBudget.Text = "0"
        Me.txtBudget.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtBudget.Value = 0
        '
        'lblUnitCost
        '
        Me.lblUnitCost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUnitCost.Location = New System.Drawing.Point(13, 65)
        Me.lblUnitCost.Name = "lblUnitCost"
        Me.lblUnitCost.Size = New System.Drawing.Size(42, 16)
        Me.lblUnitCost.TabIndex = 240
        Me.lblUnitCost.Text = "Budget"
        '
        'txtFromDate
        '
        Me.txtFromDate.CustomFormat = "dd/MM/yyyy"
        Me.txtFromDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.txtFromDate.Location = New System.Drawing.Point(115, 14)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Me.RadLabel4
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.Size = New System.Drawing.Size(78, 18)
        Me.txtFromDate.TabIndex = 0
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "13/06/2011"
        Me.txtFromDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel4
        '
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.RadLabel4.Location = New System.Drawing.Point(13, 15)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(60, 16)
        Me.RadLabel4.TabIndex = 28
        Me.RadLabel4.Text = "From Date"
        '
        'txtToDate
        '
        Me.txtToDate.CustomFormat = "dd/MM/yyyy"
        Me.txtToDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.txtToDate.Location = New System.Drawing.Point(275, 14)
        Me.txtToDate.MendatroryField = False
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Me.RadLabel4
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.Size = New System.Drawing.Size(78, 18)
        Me.txtToDate.TabIndex = 1
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "13/06/2011"
        Me.txtToDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'rdbtnnew
        '
        Me.rdbtnnew.Image = My.Resources._new
        Me.rdbtnnew.Location = New System.Drawing.Point(607, 37)
        Me.rdbtnnew.Name = "rdbtnnew"
        Me.rdbtnnew.Size = New System.Drawing.Size(18, 18)
        Me.rdbtnnew.TabIndex = 4
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(211, 16)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(46, 16)
        Me.MyLabel1.TabIndex = 29
        Me.MyLabel1.Text = "To Date"
        '
        'txtProjectdesc
        '
        Me.txtProjectdesc.Location = New System.Drawing.Point(275, 37)
        Me.txtProjectdesc.MendatroryField = False
        Me.txtProjectdesc.MyLinkLable1 = Me.WIP
        Me.txtProjectdesc.MyLinkLable2 = Nothing
        Me.txtProjectdesc.Name = "txtProjectdesc"
        Me.txtProjectdesc.ReadOnly = True
        Me.txtProjectdesc.Size = New System.Drawing.Size(326, 20)
        Me.txtProjectdesc.TabIndex = 3
        Me.txtProjectdesc.TabStop = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(544, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(83, 18)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'FrmBudgetMaintenance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(638, 221)
        Me.Controls.Add(Me.RadMenu1)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmBudgetMaintenance"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Budget Maintenance"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.WIP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.rdgpbxcustomeraccountset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.rdgpbxcustomeraccountset.ResumeLayout(False)
        Me.rdgpbxcustomeraccountset.PerformLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtComments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBudget, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblUnitCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtProjectdesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndProject As common.UserControls.txtFinder
    Friend WithEvents WIP As common.Controls.MyLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents rdgpbxcustomeraccountset As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rdbtnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtProjectdesc As common.Controls.MyTextBox
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtComments As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents txtBudget As common.MyNumBox
    Friend WithEvents lblUnitCost As common.Controls.MyLabel
End Class

