Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDistributor_VS_SecondaryCustomer_Sale
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgPacks = New common.MyCheckBoxGrid
        Me.ddlMonth = New common.Controls.MyComboBox
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.ddlYear = New common.Controls.MyComboBox
        Me.lblMoveType = New common.Controls.MyLabel
        Me.gbCustomer = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgCustomer = New common.MyCheckBoxGrid
        Me.Panel8 = New System.Windows.Forms.Panel
        Me.chkCustomerSelect = New common.Controls.MyRadioButton
        Me.chkCustomerAll = New common.Controls.MyRadioButton
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage
        Me.GV1 = New common.UserControls.MyRadGridView
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        Me.btnRefresh = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.ddlMonth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlYear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMoveType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gbCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbCustomer.SuspendLayout()
        Me.Panel8.SuspendLayout()
        CType(Me.chkCustomerSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCustomerAll, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.GV1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GV1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnRefresh)
        Me.SplitContainer1.Size = New System.Drawing.Size(702, 467)
        Me.SplitContainer1.SplitterDistance = 437
        Me.SplitContainer1.TabIndex = 1
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(702, 437)
        Me.RadPageView1.TabIndex = 117
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.RadPageViewPage1.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.ddlMonth)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.ddlYear)
        Me.RadPageViewPage1.Controls.Add(Me.lblMoveType)
        Me.RadPageViewPage1.Controls.Add(Me.gbCustomer)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(681, 389)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.cbgPacks)
        Me.RadGroupBox1.HeaderText = "Packs"
        Me.RadGroupBox1.Location = New System.Drawing.Point(5, 211)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(463, 174)
        Me.RadGroupBox1.TabIndex = 622
        Me.RadGroupBox1.Text = "Packs"
        '
        'cbgPacks
        '
        Me.cbgPacks.CheckedValue = Nothing
        Me.cbgPacks.DataSource = Nothing
        Me.cbgPacks.DisplayMember = "Name"
        Me.cbgPacks.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgPacks.Location = New System.Drawing.Point(10, 20)
        Me.cbgPacks.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgPacks.MyShowHeadrText = False
        Me.cbgPacks.Name = "cbgPacks"
        Me.cbgPacks.Size = New System.Drawing.Size(443, 144)
        Me.cbgPacks.TabIndex = 1
        Me.cbgPacks.ValueMember = "Code"
        '
        'ddlMonth
        '
        Me.ddlMonth.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlMonth.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddlMonth.Location = New System.Drawing.Point(266, 12)
        Me.ddlMonth.MendatroryField = False
        Me.ddlMonth.MyLinkLable1 = Nothing
        Me.ddlMonth.MyLinkLable2 = Nothing
        Me.ddlMonth.Name = "ddlMonth"
        Me.ddlMonth.Size = New System.Drawing.Size(127, 18)
        Me.ddlMonth.TabIndex = 620
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(211, 13)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(38, 16)
        Me.MyLabel1.TabIndex = 621
        Me.MyLabel1.Text = "Month"
        '
        'ddlYear
        '
        Me.ddlYear.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlYear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddlYear.Location = New System.Drawing.Point(56, 12)
        Me.ddlYear.MendatroryField = False
        Me.ddlYear.MyLinkLable1 = Nothing
        Me.ddlYear.MyLinkLable2 = Nothing
        Me.ddlYear.Name = "ddlYear"
        Me.ddlYear.Size = New System.Drawing.Size(127, 18)
        Me.ddlYear.TabIndex = 618
        '
        'lblMoveType
        '
        Me.lblMoveType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMoveType.Location = New System.Drawing.Point(13, 13)
        Me.lblMoveType.Name = "lblMoveType"
        Me.lblMoveType.Size = New System.Drawing.Size(30, 16)
        Me.lblMoveType.TabIndex = 619
        Me.lblMoveType.Text = "Year"
        '
        'gbCustomer
        '
        Me.gbCustomer.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbCustomer.Controls.Add(Me.cbgCustomer)
        Me.gbCustomer.Controls.Add(Me.Panel8)
        Me.gbCustomer.HeaderText = "Distributor"
        Me.gbCustomer.Location = New System.Drawing.Point(5, 36)
        Me.gbCustomer.Name = "gbCustomer"
        Me.gbCustomer.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.gbCustomer.Size = New System.Drawing.Size(463, 174)
        Me.gbCustomer.TabIndex = 115
        Me.gbCustomer.Text = "Distributor"
        '
        'cbgCustomer
        '
        Me.cbgCustomer.CheckedValue = Nothing
        Me.cbgCustomer.DataSource = Nothing
        Me.cbgCustomer.DisplayMember = "Name"
        Me.cbgCustomer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgCustomer.Location = New System.Drawing.Point(10, 40)
        Me.cbgCustomer.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgCustomer.MyShowHeadrText = False
        Me.cbgCustomer.Name = "cbgCustomer"
        Me.cbgCustomer.Size = New System.Drawing.Size(443, 124)
        Me.cbgCustomer.TabIndex = 1
        Me.cbgCustomer.ValueMember = "Code"
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.chkCustomerSelect)
        Me.Panel8.Controls.Add(Me.chkCustomerAll)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel8.Location = New System.Drawing.Point(10, 20)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(443, 20)
        Me.Panel8.TabIndex = 0
        '
        'chkCustomerSelect
        '
        Me.chkCustomerSelect.Location = New System.Drawing.Point(218, 1)
        Me.chkCustomerSelect.MyLinkLable1 = Nothing
        Me.chkCustomerSelect.MyLinkLable2 = Nothing
        Me.chkCustomerSelect.Name = "chkCustomerSelect"
        Me.chkCustomerSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkCustomerSelect.TabIndex = 1
        Me.chkCustomerSelect.Text = "Select"
        '
        'chkCustomerAll
        '
        Me.chkCustomerAll.Location = New System.Drawing.Point(167, 1)
        Me.chkCustomerAll.MyLinkLable1 = Nothing
        Me.chkCustomerAll.MyLinkLable2 = Nothing
        Me.chkCustomerAll.Name = "chkCustomerAll"
        Me.chkCustomerAll.Size = New System.Drawing.Size(33, 18)
        Me.chkCustomerAll.TabIndex = 0
        Me.chkCustomerAll.Text = "All"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.GV1)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(819, 412)
        Me.RadPageViewPage2.Text = "Report"
        '
        'GV1
        '
        Me.GV1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GV1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.GV1.MasterTemplate.AllowAddNewRow = False
        Me.GV1.MasterTemplate.AllowEditRow = False
        Me.GV1.Name = "GV1"
        Me.GV1.ShowGroupPanel = False
        Me.GV1.Size = New System.Drawing.Size(819, 412)
        Me.GV1.TabIndex = 0
        Me.GV1.Text = "RadGridView1"
        '
        'btnClose
        '
        Me.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnClose.Location = New System.Drawing.Point(625, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(74, 18)
        Me.btnClose.TabIndex = 130
        Me.btnClose.Text = "Close"
        '
        'btnExport
        '
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItem2})
        Me.btnExport.Location = New System.Drawing.Point(154, 4)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(80, 18)
        Me.btnExport.TabIndex = 129
        Me.btnExport.Text = "Export"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Excel"
        Me.RadMenuItem1.AccessibleName = "Excel"
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Excel"
        Me.RadMenuItem1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "PDF"
        Me.RadMenuItem2.AccessibleName = "PDF"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "PDF"
        Me.RadMenuItem2.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnReset
        '
        Me.btnReset.Location = New System.Drawing.Point(78, 4)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(68, 18)
        Me.btnReset.TabIndex = 127
        Me.btnReset.Text = "Reset"
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(4, 4)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(68, 18)
        Me.btnRefresh.TabIndex = 128
        Me.btnRefresh.Text = ">>>"
        '
        'FrmDistributor_VS_SecondaryCustomer_Sale
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(702, 467)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmDistributor_VS_SecondaryCustomer_Sale"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Distributor/Secondary Sale"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.ddlMonth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlYear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMoveType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gbCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbCustomer.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        CType(Me.chkCustomerSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCustomerAll, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.GV1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GV1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gbCustomer As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgCustomer As common.MyCheckBoxGrid
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents chkCustomerSelect As common.Controls.MyRadioButton
    Friend WithEvents chkCustomerAll As common.Controls.MyRadioButton
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents GV1 As common.UserControls.MyRadGridView
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnRefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents ddlMonth As common.Controls.MyComboBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents ddlYear As common.Controls.MyComboBox
    Friend WithEvents lblMoveType As common.Controls.MyLabel
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgPacks As common.MyCheckBoxGrid
End Class

