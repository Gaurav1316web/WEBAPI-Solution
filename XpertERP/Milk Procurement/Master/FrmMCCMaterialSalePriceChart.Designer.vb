<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMCCMaterialSalePriceChart
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
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyDateTimePicker1 = New common.Controls.MyDateTimePicker()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgMCC = New common.MyCheckBoxGrid()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.chkMCCSelect = New common.Controls.MyRadioButton()
        Me.chkMCCAll = New common.Controls.MyRadioButton()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.fndno = New common.UserControls.txtFinder()
        Me.lblvandorno = New common.Controls.MyLabel()
        Me.txtdate = New common.Controls.MyDateTimePicker()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.btnHistory = New Telerik.WinControls.UI.RadButton()
        Me.BtnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnexcel = New Telerik.WinControls.UI.RadSplitButton()
        Me.btnexport = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnimport = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnrefresh = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnshow = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyDateTimePicker1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkMCCSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMCCAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblvandorno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnexcel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnrefresh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnshow, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnHistory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnexcel)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnrefresh)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnshow)
        Me.SplitContainer1.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.SplitContainer1.Size = New System.Drawing.Size(728, 482)
        Me.SplitContainer1.SplitterDistance = 446
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyDateTimePicker1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadGroupBox3)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndno)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblvandorno)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtdate)
        Me.SplitContainer2.Panel1.Padding = New System.Windows.Forms.Padding(3)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadPageView1)
        Me.SplitContainer2.Panel2.Padding = New System.Windows.Forms.Padding(1)
        Me.SplitContainer2.Size = New System.Drawing.Size(728, 446)
        Me.SplitContainer2.SplitterDistance = 225
        Me.SplitContainer2.TabIndex = 9
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel2.Location = New System.Drawing.Point(412, 14)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(94, 16)
        Me.MyLabel2.TabIndex = 16
        Me.MyLabel2.Text = "Effective Till Date"
        '
        'MyDateTimePicker1
        '
        Me.MyDateTimePicker1.CalculationExpression = Nothing
        Me.MyDateTimePicker1.CustomFormat = "dd/MM/yyyy"
        Me.MyDateTimePicker1.FieldCode = Nothing
        Me.MyDateTimePicker1.FieldDesc = Nothing
        Me.MyDateTimePicker1.FieldMaxLength = 0
        Me.MyDateTimePicker1.FieldName = Nothing
        Me.MyDateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.MyDateTimePicker1.isCalculatedField = False
        Me.MyDateTimePicker1.IsSourceFromTable = False
        Me.MyDateTimePicker1.IsSourceFromValueList = False
        Me.MyDateTimePicker1.IsUnique = False
        Me.MyDateTimePicker1.Location = New System.Drawing.Point(512, 12)
        Me.MyDateTimePicker1.MendatroryField = True
        Me.MyDateTimePicker1.MinDate = New Date(1973, 1, 1, 0, 0, 0, 0)
        Me.MyDateTimePicker1.MyLinkLable1 = Me.MyLabel2
        Me.MyDateTimePicker1.MyLinkLable2 = Nothing
        Me.MyDateTimePicker1.Name = "MyDateTimePicker1"
        Me.MyDateTimePicker1.ReferenceFieldDesc = Nothing
        Me.MyDateTimePicker1.ReferenceFieldName = Nothing
        Me.MyDateTimePicker1.ReferenceTableName = Nothing
        Me.MyDateTimePicker1.Size = New System.Drawing.Size(95, 20)
        Me.MyDateTimePicker1.TabIndex = 15
        Me.MyDateTimePicker1.TabStop = False
        Me.MyDateTimePicker1.Text = "28/05/2014"
        Me.MyDateTimePicker1.Value = New Date(2014, 5, 28, 15, 5, 19, 923)
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.cbgMCC)
        Me.RadGroupBox3.Controls.Add(Me.Panel1)
        Me.RadGroupBox3.HeaderText = "MCC"
        Me.RadGroupBox3.Location = New System.Drawing.Point(6, 38)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(360, 144)
        Me.RadGroupBox3.TabIndex = 14
        Me.RadGroupBox3.Text = "MCC"
        '
        'cbgMCC
        '
        Me.cbgMCC.CheckedValue = Nothing
        Me.cbgMCC.DataSource = Nothing
        Me.cbgMCC.DisplayMember = "Name"
        Me.cbgMCC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgMCC.Location = New System.Drawing.Point(10, 40)
        Me.cbgMCC.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgMCC.MyShowHeadrText = False
        Me.cbgMCC.Name = "cbgMCC"
        Me.cbgMCC.Size = New System.Drawing.Size(340, 94)
        Me.cbgMCC.TabIndex = 2
        Me.cbgMCC.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkMCCSelect)
        Me.Panel1.Controls.Add(Me.chkMCCAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(340, 20)
        Me.Panel1.TabIndex = 1
        '
        'chkMCCSelect
        '
        Me.chkMCCSelect.Location = New System.Drawing.Point(176, 1)
        Me.chkMCCSelect.MyLinkLable1 = Nothing
        Me.chkMCCSelect.MyLinkLable2 = Nothing
        Me.chkMCCSelect.Name = "chkMCCSelect"
        Me.chkMCCSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkMCCSelect.TabIndex = 2
        Me.chkMCCSelect.Text = "Select"
        '
        'chkMCCAll
        '
        Me.chkMCCAll.Location = New System.Drawing.Point(118, 1)
        Me.chkMCCAll.MyLinkLable1 = Nothing
        Me.chkMCCAll.MyLinkLable2 = Nothing
        Me.chkMCCAll.Name = "chkMCCAll"
        Me.chkMCCAll.Size = New System.Drawing.Size(33, 18)
        Me.chkMCCAll.TabIndex = 1
        Me.chkMCCAll.Text = "All"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(6, 14)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(33, 16)
        Me.MyLabel1.TabIndex = 10
        Me.MyLabel1.Text = "Code"
        '
        'fndno
        '
        Me.fndno.CalculationExpression = Nothing
        Me.fndno.FieldCode = Nothing
        Me.fndno.FieldDesc = Nothing
        Me.fndno.FieldMaxLength = 0
        Me.fndno.FieldName = Nothing
        Me.fndno.isCalculatedField = False
        Me.fndno.IsSourceFromTable = False
        Me.fndno.IsSourceFromValueList = False
        Me.fndno.IsUnique = False
        Me.fndno.Location = New System.Drawing.Point(45, 13)
        Me.fndno.MendatroryField = True
        Me.fndno.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndno.MyLinkLable1 = Me.MyLabel1
        Me.fndno.MyLinkLable2 = Nothing
        Me.fndno.MyReadOnly = False
        Me.fndno.MyShowMasterFormButton = False
        Me.fndno.Name = "fndno"
        Me.fndno.ReferenceFieldDesc = Nothing
        Me.fndno.ReferenceFieldName = Nothing
        Me.fndno.ReferenceTableName = Nothing
        Me.fndno.Size = New System.Drawing.Size(187, 19)
        Me.fndno.TabIndex = 1
        Me.fndno.Value = ""
        '
        'lblvandorno
        '
        Me.lblvandorno.FieldName = Nothing
        Me.lblvandorno.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblvandorno.Location = New System.Drawing.Point(237, 14)
        Me.lblvandorno.Name = "lblvandorno"
        Me.lblvandorno.Size = New System.Drawing.Size(57, 16)
        Me.lblvandorno.TabIndex = 7
        Me.lblvandorno.Text = "Start Date"
        '
        'txtdate
        '
        Me.txtdate.CalculationExpression = Nothing
        Me.txtdate.CustomFormat = "dd/MM/yyyy"
        Me.txtdate.FieldCode = Nothing
        Me.txtdate.FieldDesc = Nothing
        Me.txtdate.FieldMaxLength = 0
        Me.txtdate.FieldName = Nothing
        Me.txtdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtdate.isCalculatedField = False
        Me.txtdate.IsSourceFromTable = False
        Me.txtdate.IsSourceFromValueList = False
        Me.txtdate.IsUnique = False
        Me.txtdate.Location = New System.Drawing.Point(300, 12)
        Me.txtdate.MendatroryField = True
        Me.txtdate.MinDate = New Date(1973, 1, 1, 0, 0, 0, 0)
        Me.txtdate.MyLinkLable1 = Me.lblvandorno
        Me.txtdate.MyLinkLable2 = Nothing
        Me.txtdate.Name = "txtdate"
        Me.txtdate.ReferenceFieldDesc = Nothing
        Me.txtdate.ReferenceFieldName = Nothing
        Me.txtdate.ReferenceTableName = Nothing
        Me.txtdate.Size = New System.Drawing.Size(95, 20)
        Me.txtdate.TabIndex = 0
        Me.txtdate.TabStop = False
        Me.txtdate.Text = "28/05/2014"
        Me.txtdate.Value = New Date(2014, 5, 28, 15, 5, 19, 923)
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(1, 1)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(726, 215)
        Me.RadPageView1.TabIndex = 1
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.gv)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(90.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(705, 167)
        Me.RadPageViewPage1.Text = "Price Chart List"
        '
        'gv
        '
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv.MasterTemplate.AllowAddNewRow = False
        Me.gv.MasterTemplate.AllowColumnReorder = False
        Me.gv.MasterTemplate.AllowDeleteRow = False
        Me.gv.MasterTemplate.AllowDragToGroup = False
        Me.gv.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv.MyStopExport = False
        Me.gv.Name = "gv"
        Me.gv.ShowHeaderCellButtons = True
        Me.gv.Size = New System.Drawing.Size(705, 167)
        Me.gv.TabIndex = 0
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.UcAttachment1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(75.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(705, 167)
        Me.RadPageViewPage2.Text = "Attachment"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(705, 167)
        Me.UcAttachment1.TabIndex = 2
        '
        'btnHistory
        '
        Me.btnHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnHistory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHistory.Location = New System.Drawing.Point(331, 6)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Size = New System.Drawing.Size(87, 20)
        Me.btnHistory.TabIndex = 40
        Me.btnHistory.Text = "History"
        '
        'BtnSave
        '
        Me.BtnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnSave.Location = New System.Drawing.Point(6, 6)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(73, 20)
        Me.BtnSave.TabIndex = 4
        Me.BtnSave.Text = "&Save"
        '
        'btnexcel
        '
        Me.btnexcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnexcel.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnexport, Me.btnimport})
        Me.btnexcel.Location = New System.Drawing.Point(166, 6)
        Me.btnexcel.Name = "btnexcel"
        Me.btnexcel.Size = New System.Drawing.Size(80, 20)
        Me.btnexcel.TabIndex = 0
        Me.btnexcel.Text = "Excel"
        '
        'btnexport
        '
        Me.btnexport.Image = Global.ERP.My.Resources.Resources.MSE
        Me.btnexport.Name = "btnexport"
        Me.btnexport.Text = "Export"
        '
        'btnimport
        '
        Me.btnimport.Image = Global.ERP.My.Resources.Resources.MSE
        Me.btnimport.Name = "btnimport"
        Me.btnimport.Text = "Import"
        '
        'btnrefresh
        '
        Me.btnrefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnrefresh.Location = New System.Drawing.Point(87, 6)
        Me.btnrefresh.Name = "btnrefresh"
        Me.btnrefresh.Size = New System.Drawing.Size(73, 20)
        Me.btnrefresh.TabIndex = 2
        Me.btnrefresh.Text = "&Refresh"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(649, 6)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(73, 20)
        Me.btnclose.TabIndex = 3
        Me.btnclose.Text = "&Close"
        '
        'btnshow
        '
        Me.btnshow.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnshow.Location = New System.Drawing.Point(252, 6)
        Me.btnshow.Name = "btnshow"
        Me.btnshow.Size = New System.Drawing.Size(73, 20)
        Me.btnshow.TabIndex = 1
        Me.btnshow.Text = "&Show"
        Me.btnshow.Visible = False
        '
        'FrmMCCMaterialSalePriceChart
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(728, 482)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmMCCMaterialSalePriceChart"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmPriceChartUploader"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyDateTimePicker1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkMCCSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMCCAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblvandorno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnexcel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnrefresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnshow, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnshow As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblvandorno As common.Controls.MyLabel
    Friend WithEvents txtdate As common.Controls.MyDateTimePicker
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents btnrefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnexcel As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnexport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnimport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents fndno As common.UserControls.txtFinder
    Friend WithEvents MyComboBox2 As common.Controls.MyComboBox
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgMCC As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkMCCSelect As common.Controls.MyRadioButton
    Friend WithEvents chkMCCAll As common.Controls.MyRadioButton
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyDateTimePicker1 As common.Controls.MyDateTimePicker
    Friend WithEvents BtnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnHistory As RadButton
End Class

