<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmItemListRpt
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.chkUOMWise = New Telerik.WinControls.UI.RadCheckBox()
        Me.grpItemType = New System.Windows.Forms.GroupBox()
        Me.chkAll = New common.Controls.MyRadioButton()
        Me.chkActive = New common.Controls.MyRadioButton()
        Me.chkInactive = New common.Controls.MyRadioButton()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgItem = New common.MyCheckBoxGrid()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.chkSelect = New common.Controls.MyRadioButton()
        Me.chkitemAll = New common.Controls.MyRadioButton()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv = New common.UserControls.MyRadGridView()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnImport = New Telerik.WinControls.UI.RadButton()
        Me.btnUpdate = New Telerik.WinControls.UI.RadButton()
        Me.btnexport = New Telerik.WinControls.UI.RadSplitButton()
        Me.btnexcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnpdf = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnreset = New Telerik.WinControls.UI.RadButton()
        Me.btnprint = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.chkUOMWise, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpItemType.SuspendLayout()
        CType(Me.chkAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkInactive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        Me.Panel6.SuspendLayout()
        CType(Me.chkSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkitemAll, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnImport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUpdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnexport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu1)
        Me.SplitContainer1.Panel1.Padding = New System.Windows.Forms.Padding(3)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnImport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnUpdate)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnexport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnreset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnprint)
        Me.SplitContainer1.Size = New System.Drawing.Size(460, 401)
        Me.SplitContainer1.SplitterDistance = 363
        Me.SplitContainer1.TabIndex = 1
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(3, 23)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(454, 337)
        Me.RadPageView1.TabIndex = 0
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.chkUOMWise)
        Me.RadPageViewPage1.Controls.Add(Me.grpItemType)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(62.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(433, 289)
        Me.RadPageViewPage1.Text = "Selection"
        '
        'chkUOMWise
        '
        Me.chkUOMWise.Location = New System.Drawing.Point(234, 14)
        Me.chkUOMWise.Name = "chkUOMWise"
        Me.chkUOMWise.Size = New System.Drawing.Size(74, 18)
        Me.chkUOMWise.TabIndex = 391
        Me.chkUOMWise.Text = "UOM Wise"
        '
        'grpItemType
        '
        Me.grpItemType.Controls.Add(Me.chkAll)
        Me.grpItemType.Controls.Add(Me.chkActive)
        Me.grpItemType.Controls.Add(Me.chkInactive)
        Me.grpItemType.Location = New System.Drawing.Point(3, 3)
        Me.grpItemType.Name = "grpItemType"
        Me.grpItemType.Size = New System.Drawing.Size(225, 33)
        Me.grpItemType.TabIndex = 318
        Me.grpItemType.TabStop = False
        '
        'chkAll
        '
        Me.chkAll.Location = New System.Drawing.Point(179, 11)
        Me.chkAll.MyLinkLable1 = Nothing
        Me.chkAll.MyLinkLable2 = Nothing
        Me.chkAll.Name = "chkAll"
        Me.chkAll.Size = New System.Drawing.Size(33, 18)
        Me.chkAll.TabIndex = 74
        Me.chkAll.Text = "All"
        '
        'chkActive
        '
        Me.chkActive.Location = New System.Drawing.Point(11, 11)
        Me.chkActive.MyLinkLable1 = Nothing
        Me.chkActive.MyLinkLable2 = Nothing
        Me.chkActive.Name = "chkActive"
        Me.chkActive.Size = New System.Drawing.Size(51, 18)
        Me.chkActive.TabIndex = 72
        Me.chkActive.Text = "Active"
        '
        'chkInactive
        '
        Me.chkInactive.Location = New System.Drawing.Point(83, 11)
        Me.chkInactive.MyLinkLable1 = Nothing
        Me.chkInactive.MyLinkLable2 = Nothing
        Me.chkInactive.Name = "chkInactive"
        Me.chkInactive.Size = New System.Drawing.Size(63, 18)
        Me.chkInactive.TabIndex = 73
        Me.chkInactive.Text = "In Active"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox2.Controls.Add(Me.cbgItem)
        Me.RadGroupBox2.Controls.Add(Me.Panel6)
        Me.RadGroupBox2.HeaderText = "Item"
        Me.RadGroupBox2.Location = New System.Drawing.Point(3, 38)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(411, 248)
        Me.RadGroupBox2.TabIndex = 72
        Me.RadGroupBox2.Text = "Item"
        '
        'cbgItem
        '
        Me.cbgItem.AccessibleName = ""
        Me.cbgItem.CheckedValue = Nothing
        Me.cbgItem.DataSource = Nothing
        Me.cbgItem.DisplayMember = "Name"
        Me.cbgItem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgItem.Location = New System.Drawing.Point(10, 40)
        Me.cbgItem.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgItem.MyShowHeadrText = False
        Me.cbgItem.Name = "cbgItem"
        Me.cbgItem.Size = New System.Drawing.Size(391, 198)
        Me.cbgItem.TabIndex = 1
        Me.cbgItem.ValueMember = "Code"
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.chkSelect)
        Me.Panel6.Controls.Add(Me.chkitemAll)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(10, 20)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(391, 20)
        Me.Panel6.TabIndex = 0
        '
        'chkSelect
        '
        Me.chkSelect.Location = New System.Drawing.Point(208, 1)
        Me.chkSelect.MyLinkLable1 = Nothing
        Me.chkSelect.MyLinkLable2 = Nothing
        Me.chkSelect.Name = "chkSelect"
        Me.chkSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkSelect.TabIndex = 1
        Me.chkSelect.Text = "Select"
        '
        'chkitemAll
        '
        Me.chkitemAll.Location = New System.Drawing.Point(52, 1)
        Me.chkitemAll.MyLinkLable1 = Nothing
        Me.chkitemAll.MyLinkLable2 = Nothing
        Me.chkitemAll.Name = "chkitemAll"
        Me.chkitemAll.Size = New System.Drawing.Size(33, 18)
        Me.chkitemAll.TabIndex = 0
        Me.chkitemAll.Text = "All"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(433, 289)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gv
        '
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv.MasterTemplate.AllowAddNewRow = False
        Me.gv.MasterTemplate.AllowDeleteRow = False
        Me.gv.MasterTemplate.AllowDragToGroup = False
        Me.gv.MasterTemplate.EnableFiltering = True
        Me.gv.MasterTemplate.EnableGrouping = False
        Me.gv.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv.MyStopExport = False
        Me.gv.Name = "gv"
        Me.gv.ShowGroupPanel = False
        Me.gv.ShowHeaderCellButtons = True
        Me.gv.Size = New System.Drawing.Size(433, 289)
        Me.gv.TabIndex = 0
        Me.gv.VarID = ""
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(3, 3)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(454, 20)
        Me.RadMenu1.TabIndex = 2
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmSaveLayout, Me.rmDeleteLayout})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Setting"
        '
        'rmSaveLayout
        '
        Me.rmSaveLayout.Name = "rmSaveLayout"
        Me.rmSaveLayout.Text = "Save Layout"
        '
        'rmDeleteLayout
        '
        Me.rmDeleteLayout.Name = "rmDeleteLayout"
        Me.rmDeleteLayout.Text = "Delete Layout"
        '
        'btnImport
        '
        Me.btnImport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnImport.Location = New System.Drawing.Point(305, 8)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(66, 20)
        Me.btnImport.TabIndex = 5
        Me.btnImport.Text = "Import"
        Me.btnImport.Visible = False
        '
        'btnUpdate
        '
        Me.btnUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUpdate.Location = New System.Drawing.Point(233, 8)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(66, 20)
        Me.btnUpdate.TabIndex = 4
        Me.btnUpdate.Text = "Update"
        Me.btnUpdate.Visible = False
        '
        'btnexport
        '
        Me.btnexport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnexport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnexcel, Me.btnpdf})
        Me.btnexport.Location = New System.Drawing.Point(152, 8)
        Me.btnexport.Name = "btnexport"
        Me.btnexport.Size = New System.Drawing.Size(75, 20)
        Me.btnexport.TabIndex = 3
        Me.btnexport.Text = "Export"
        '
        'btnexcel
        '
        Me.btnexcel.Image = Global.ERP.My.Resources.Resources.MSE
        Me.btnexcel.Name = "btnexcel"
        Me.btnexcel.Text = "Export To Excel"
        '
        'btnpdf
        '
        Me.btnpdf.Image = Global.ERP.My.Resources.Resources.pdf
        Me.btnpdf.Name = "btnpdf"
        Me.btnpdf.Text = "Export To PDF"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(382, 8)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 20)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'btnreset
        '
        Me.btnreset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreset.Location = New System.Drawing.Point(80, 8)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(66, 20)
        Me.btnreset.TabIndex = 1
        Me.btnreset.Text = "Reset"
        '
        'btnprint
        '
        Me.btnprint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnprint.Location = New System.Drawing.Point(8, 8)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(66, 20)
        Me.btnprint.TabIndex = 0
        Me.btnprint.Text = ">>>"
        '
        'FrmItemListRpt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(460, 401)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmItemListRpt"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmItemListRpt"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.chkUOMWise, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpItemType.ResumeLayout(False)
        Me.grpItemType.PerformLayout()
        CType(Me.chkAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkInactive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        CType(Me.chkSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkitemAll, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnImport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUpdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnexport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnexport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnprint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnexcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnpdf As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgItem As common.MyCheckBoxGrid
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents chkSelect As common.Controls.MyRadioButton
    Friend WithEvents chkitemAll As common.Controls.MyRadioButton
    Friend WithEvents grpItemType As System.Windows.Forms.GroupBox
    Friend WithEvents chkAll As common.Controls.MyRadioButton
    Friend WithEvents chkActive As common.Controls.MyRadioButton
    Friend WithEvents chkInactive As common.Controls.MyRadioButton
    Friend WithEvents chkUOMWise As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnUpdate As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnImport As Telerik.WinControls.UI.RadButton
End Class

