<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmVPFActiveScreens
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.LblModuleName = New common.Controls.MyLabel()
        Me.TxtModuleCode = New common.UserControls.txtFinder()
        Me.MyLabel22 = New common.Controls.MyLabel()
        Me.btnPhotoBrowse = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgTrans = New common.MyCheckBoxGrid()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.ChkTransSelect = New common.Controls.MyRadioButton()
        Me.ChkTransAll = New common.Controls.MyRadioButton()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgMasters = New common.MyCheckBoxGrid()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ChkMasSelect = New common.Controls.MyRadioButton()
        Me.ChkMasAll = New common.Controls.MyRadioButton()
        Me.Locationgb = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgRpt = New common.MyCheckBoxGrid()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.chkRptSelect = New common.Controls.MyRadioButton()
        Me.chkRptAll = New common.Controls.MyRadioButton()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.GrpMReports = New System.Windows.Forms.GroupBox()
        Me.LblTrans = New common.Controls.MyLabel()
        Me.LblMaster = New common.Controls.MyLabel()
        Me.LblRptParent = New common.Controls.MyLabel()
        Me.BtnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.LblModuleName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPhotoBrowse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.ChkTransSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkTransAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.ChkMasSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkMasAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Locationgb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Locationgb.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.chkRptSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRptAll, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.GrpMReports.SuspendLayout()
        CType(Me.LblTrans, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblRptParent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 22)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.LblModuleName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TxtModuleCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel22)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPhotoBrowse)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadGroupBox2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadGroupBox1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Locationgb)
        Me.SplitContainer1.Size = New System.Drawing.Size(1061, 577)
        Me.SplitContainer1.SplitterDistance = 43
        Me.SplitContainer1.TabIndex = 0
        '
        'LblModuleName
        '
        Me.LblModuleName.AutoSize = False
        Me.LblModuleName.BorderVisible = True
        Me.LblModuleName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblModuleName.Location = New System.Drawing.Point(218, 15)
        Me.LblModuleName.Name = "LblModuleName"
        Me.LblModuleName.Size = New System.Drawing.Size(312, 19)
        Me.LblModuleName.TabIndex = 42
        Me.LblModuleName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblModuleName.TextWrap = False
        '
        'TxtModuleCode
        '
        Me.TxtModuleCode.Location = New System.Drawing.Point(90, 15)
        Me.TxtModuleCode.MendatroryField = True
        Me.TxtModuleCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtModuleCode.MyLinkLable1 = Nothing
        Me.TxtModuleCode.MyLinkLable2 = Nothing
        Me.TxtModuleCode.MyReadOnly = False
        Me.TxtModuleCode.MyShowMasterFormButton = False
        Me.TxtModuleCode.Name = "TxtModuleCode"
        Me.TxtModuleCode.Size = New System.Drawing.Size(125, 19)
        Me.TxtModuleCode.TabIndex = 30
        Me.TxtModuleCode.Value = ""
        '
        'MyLabel22
        '
        Me.MyLabel22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel22.Location = New System.Drawing.Point(9, 15)
        Me.MyLabel22.Name = "MyLabel22"
        Me.MyLabel22.Size = New System.Drawing.Size(77, 16)
        Me.MyLabel22.TabIndex = 31
        Me.MyLabel22.Text = "Module Name"
        '
        'btnPhotoBrowse
        '
        Me.btnPhotoBrowse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPhotoBrowse.Location = New System.Drawing.Point(588, 316)
        Me.btnPhotoBrowse.Name = "btnPhotoBrowse"
        Me.btnPhotoBrowse.Size = New System.Drawing.Size(66, 18)
        Me.btnPhotoBrowse.TabIndex = 114
        Me.btnPhotoBrowse.Text = "Browse"
        Me.btnPhotoBrowse.Visible = False
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.cbgTrans)
        Me.RadGroupBox2.Controls.Add(Me.Panel2)
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold)
        Me.RadGroupBox2.HeaderText = "Transactions"
        Me.RadGroupBox2.Location = New System.Drawing.Point(532, 5)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(524, 260)
        Me.RadGroupBox2.TabIndex = 113
        Me.RadGroupBox2.Text = "Transactions"
        '
        'cbgTrans
        '
        Me.cbgTrans.CheckedValue = Nothing
        Me.cbgTrans.DataSource = Nothing
        Me.cbgTrans.DisplayMember = "Name"
        Me.cbgTrans.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgTrans.Location = New System.Drawing.Point(10, 40)
        Me.cbgTrans.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgTrans.MyShowHeadrText = False
        Me.cbgTrans.Name = "cbgTrans"
        Me.cbgTrans.Size = New System.Drawing.Size(504, 210)
        Me.cbgTrans.TabIndex = 1
        Me.cbgTrans.ValueMember = "Code"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.ChkTransSelect)
        Me.Panel2.Controls.Add(Me.ChkTransAll)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(504, 20)
        Me.Panel2.TabIndex = 0
        '
        'ChkTransSelect
        '
        Me.ChkTransSelect.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ChkTransSelect.Location = New System.Drawing.Point(333, 1)
        Me.ChkTransSelect.MyLinkLable1 = Nothing
        Me.ChkTransSelect.MyLinkLable2 = Nothing
        Me.ChkTransSelect.Name = "ChkTransSelect"
        Me.ChkTransSelect.Size = New System.Drawing.Size(50, 18)
        Me.ChkTransSelect.TabIndex = 1
        Me.ChkTransSelect.Text = "Select"
        '
        'ChkTransAll
        '
        Me.ChkTransAll.Location = New System.Drawing.Point(111, 1)
        Me.ChkTransAll.MyLinkLable1 = Nothing
        Me.ChkTransAll.MyLinkLable2 = Nothing
        Me.ChkTransAll.Name = "ChkTransAll"
        Me.ChkTransAll.Size = New System.Drawing.Size(33, 18)
        Me.ChkTransAll.TabIndex = 0
        Me.ChkTransAll.Text = "All"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.cbgMasters)
        Me.RadGroupBox1.Controls.Add(Me.Panel1)
        Me.RadGroupBox1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold)
        Me.RadGroupBox1.HeaderText = "Masters"
        Me.RadGroupBox1.Location = New System.Drawing.Point(5, 4)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(522, 260)
        Me.RadGroupBox1.TabIndex = 112
        Me.RadGroupBox1.Text = "Masters"
        '
        'cbgMasters
        '
        Me.cbgMasters.CheckedValue = Nothing
        Me.cbgMasters.DataSource = Nothing
        Me.cbgMasters.DisplayMember = "Name"
        Me.cbgMasters.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgMasters.Location = New System.Drawing.Point(10, 40)
        Me.cbgMasters.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgMasters.MyShowHeadrText = False
        Me.cbgMasters.Name = "cbgMasters"
        Me.cbgMasters.Size = New System.Drawing.Size(502, 210)
        Me.cbgMasters.TabIndex = 1
        Me.cbgMasters.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.ChkMasSelect)
        Me.Panel1.Controls.Add(Me.ChkMasAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(502, 20)
        Me.Panel1.TabIndex = 0
        '
        'ChkMasSelect
        '
        Me.ChkMasSelect.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ChkMasSelect.Location = New System.Drawing.Point(341, 1)
        Me.ChkMasSelect.MyLinkLable1 = Nothing
        Me.ChkMasSelect.MyLinkLable2 = Nothing
        Me.ChkMasSelect.Name = "ChkMasSelect"
        Me.ChkMasSelect.Size = New System.Drawing.Size(50, 18)
        Me.ChkMasSelect.TabIndex = 1
        Me.ChkMasSelect.Text = "Select"
        '
        'ChkMasAll
        '
        Me.ChkMasAll.Location = New System.Drawing.Point(111, 1)
        Me.ChkMasAll.MyLinkLable1 = Nothing
        Me.ChkMasAll.MyLinkLable2 = Nothing
        Me.ChkMasAll.Name = "ChkMasAll"
        Me.ChkMasAll.Size = New System.Drawing.Size(33, 18)
        Me.ChkMasAll.TabIndex = 0
        Me.ChkMasAll.Text = "All"
        '
        'Locationgb
        '
        Me.Locationgb.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.Locationgb.Controls.Add(Me.cbgRpt)
        Me.Locationgb.Controls.Add(Me.Panel3)
        Me.Locationgb.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Locationgb.HeaderText = "Reports"
        Me.Locationgb.Location = New System.Drawing.Point(6, 267)
        Me.Locationgb.Name = "Locationgb"
        Me.Locationgb.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.Locationgb.Size = New System.Drawing.Size(524, 260)
        Me.Locationgb.TabIndex = 111
        Me.Locationgb.Text = "Reports"
        '
        'cbgRpt
        '
        Me.cbgRpt.CheckedValue = Nothing
        Me.cbgRpt.DataSource = Nothing
        Me.cbgRpt.DisplayMember = "Name"
        Me.cbgRpt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgRpt.Location = New System.Drawing.Point(10, 40)
        Me.cbgRpt.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgRpt.MyShowHeadrText = False
        Me.cbgRpt.Name = "cbgRpt"
        Me.cbgRpt.Size = New System.Drawing.Size(504, 210)
        Me.cbgRpt.TabIndex = 1
        Me.cbgRpt.ValueMember = "Code"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.chkRptSelect)
        Me.Panel3.Controls.Add(Me.chkRptAll)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(10, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(504, 20)
        Me.Panel3.TabIndex = 0
        '
        'chkRptSelect
        '
        Me.chkRptSelect.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkRptSelect.Location = New System.Drawing.Point(333, 1)
        Me.chkRptSelect.MyLinkLable1 = Nothing
        Me.chkRptSelect.MyLinkLable2 = Nothing
        Me.chkRptSelect.Name = "chkRptSelect"
        Me.chkRptSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkRptSelect.TabIndex = 1
        Me.chkRptSelect.Text = "Select"
        '
        'chkRptAll
        '
        Me.chkRptAll.Location = New System.Drawing.Point(111, 1)
        Me.chkRptAll.MyLinkLable1 = Nothing
        Me.chkRptAll.MyLinkLable2 = Nothing
        Me.chkRptAll.Name = "chkRptAll"
        Me.chkRptAll.Size = New System.Drawing.Size(33, 18)
        Me.chkRptAll.TabIndex = 0
        Me.chkRptAll.Text = "All"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.GrpMReports)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.LblTrans)
        Me.SplitContainer2.Panel2.Controls.Add(Me.LblMaster)
        Me.SplitContainer2.Panel2.Controls.Add(Me.LblRptParent)
        Me.SplitContainer2.Panel2.Controls.Add(Me.BtnClose)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer2.Size = New System.Drawing.Size(1067, 648)
        Me.SplitContainer2.SplitterDistance = 604
        Me.SplitContainer2.TabIndex = 1
        '
        'GrpMReports
        '
        Me.GrpMReports.Controls.Add(Me.SplitContainer1)
        Me.GrpMReports.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GrpMReports.Location = New System.Drawing.Point(0, 0)
        Me.GrpMReports.Name = "GrpMReports"
        Me.GrpMReports.Size = New System.Drawing.Size(1067, 602)
        Me.GrpMReports.TabIndex = 1
        Me.GrpMReports.TabStop = False
        Me.GrpMReports.Text = "Active Section"
        '
        'LblTrans
        '
        Me.LblTrans.AutoSize = False
        Me.LblTrans.BorderVisible = True
        Me.LblTrans.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTrans.Location = New System.Drawing.Point(204, 13)
        Me.LblTrans.Name = "LblTrans"
        Me.LblTrans.Size = New System.Drawing.Size(57, 19)
        Me.LblTrans.TabIndex = 45
        Me.LblTrans.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblTrans.TextWrap = False
        Me.LblTrans.Visible = False
        '
        'LblMaster
        '
        Me.LblMaster.AutoSize = False
        Me.LblMaster.BorderVisible = True
        Me.LblMaster.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMaster.Location = New System.Drawing.Point(141, 13)
        Me.LblMaster.Name = "LblMaster"
        Me.LblMaster.Size = New System.Drawing.Size(57, 19)
        Me.LblMaster.TabIndex = 44
        Me.LblMaster.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblMaster.TextWrap = False
        Me.LblMaster.Visible = False
        '
        'LblRptParent
        '
        Me.LblRptParent.AutoSize = False
        Me.LblRptParent.BorderVisible = True
        Me.LblRptParent.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblRptParent.Location = New System.Drawing.Point(78, 13)
        Me.LblRptParent.Name = "LblRptParent"
        Me.LblRptParent.Size = New System.Drawing.Size(57, 19)
        Me.LblRptParent.TabIndex = 43
        Me.LblRptParent.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblRptParent.TextWrap = False
        Me.LblRptParent.Visible = False
        '
        'BtnClose
        '
        Me.BtnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClose.Location = New System.Drawing.Point(997, 13)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(66, 18)
        Me.BtnClose.TabIndex = 2
        Me.BtnClose.Text = "Close"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(6, 13)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 1
        Me.btnsave.Text = "Save"
        '
        'FrmVPFActiveScreens
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1067, 648)
        Me.Controls.Add(Me.SplitContainer2)
        Me.Name = "FrmVPFActiveScreens"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Active Filters"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.LblModuleName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPhotoBrowse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.ChkTransSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkTransAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.ChkMasSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkMasAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Locationgb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Locationgb.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.chkRptSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRptAll, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        Me.GrpMReports.ResumeLayout(False)
        CType(Me.LblTrans, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblMaster, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblRptParent, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Locationgb As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgRpt As common.MyCheckBoxGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents chkRptSelect As common.Controls.MyRadioButton
    Friend WithEvents chkRptAll As common.Controls.MyRadioButton
    Friend WithEvents TxtModuleCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel22 As common.Controls.MyLabel
    Friend WithEvents LblModuleName As common.Controls.MyLabel
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents BtnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents LblRptParent As common.Controls.MyLabel
    Friend WithEvents GrpMReports As System.Windows.Forms.GroupBox
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgMasters As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ChkMasSelect As common.Controls.MyRadioButton
    Friend WithEvents ChkMasAll As common.Controls.MyRadioButton
    Friend WithEvents LblMaster As common.Controls.MyLabel
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgTrans As common.MyCheckBoxGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents ChkTransSelect As common.Controls.MyRadioButton
    Friend WithEvents ChkTransAll As common.Controls.MyRadioButton
    Friend WithEvents LblTrans As common.Controls.MyLabel
    Friend WithEvents btnPhotoBrowse As Telerik.WinControls.UI.RadButton
    Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog
End Class
