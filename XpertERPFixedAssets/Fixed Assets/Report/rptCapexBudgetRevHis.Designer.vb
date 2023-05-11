Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RptCapexBudgetRevHis
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
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.dtToDate = New common.Controls.MyDateTimePicker()
        Me.dtFromDate = New common.Controls.MyDateTimePicker()
        Me.lblLocationCode = New common.Controls.MyLabel()
        Me.txtmulticapex = New common.UserControls.txtMultiSelectFinder()
        Me.lblToDate = New common.Controls.MyLabel()
        Me.lblfromDate = New common.Controls.MyLabel()
        Me.txtmultiSubCapex = New common.UserControls.txtMultiSelectFinder()
        Me.lblEmployee = New common.Controls.MyLabel()
        Me.chksubcapexBudget = New common.Controls.MyRadioButton()
        Me.chkCapexBudget = New common.Controls.MyRadioButton()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv3 = New common.UserControls.MyRadGridView()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        'Me.btnQuickExport = New Telerik.WinControls.UI.RadButton()
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton()
        Me.RmiExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnGenrate = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblfromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmployee, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chksubcapexBudget, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCapexBudget, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv3.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        'CType(Me.btnQuickExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGenrate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        'Me.SplitContainer1.Panel2.Controls.Add(Me.btnQuickExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGenrate)
        Me.SplitContainer1.Size = New System.Drawing.Size(573, 272)
        Me.SplitContainer1.SplitterDistance = 223
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(573, 223)
        Me.RadPageView1.TabIndex = 216
        Me.RadPageView1.Text = "RadPageView1"
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.GroupBox1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(552, 175)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.dtToDate)
        Me.GroupBox1.Controls.Add(Me.dtFromDate)
        Me.GroupBox1.Controls.Add(Me.lblLocationCode)
        Me.GroupBox1.Controls.Add(Me.txtmulticapex)
        Me.GroupBox1.Controls.Add(Me.lblToDate)
        Me.GroupBox1.Controls.Add(Me.lblfromDate)
        Me.GroupBox1.Controls.Add(Me.txtmultiSubCapex)
        Me.GroupBox1.Controls.Add(Me.lblEmployee)
        Me.GroupBox1.Controls.Add(Me.chksubcapexBudget)
        Me.GroupBox1.Controls.Add(Me.chkCapexBudget)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(549, 175)
        Me.GroupBox1.TabIndex = 218
        Me.GroupBox1.TabStop = False
        '
        'dtToDate
        '
        Me.dtToDate.CalculationExpression = Nothing
        Me.dtToDate.CustomFormat = "dd-MM-yyyy"
        Me.dtToDate.FieldCode = Nothing
        Me.dtToDate.FieldDesc = Nothing
        Me.dtToDate.FieldMaxLength = 0
        Me.dtToDate.FieldName = Nothing
        Me.dtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtToDate.isCalculatedField = False
        Me.dtToDate.IsSourceFromTable = False
        Me.dtToDate.IsSourceFromValueList = False
        Me.dtToDate.IsUnique = False
        Me.dtToDate.Location = New System.Drawing.Point(226, 16)
        Me.dtToDate.MendatroryField = False
        Me.dtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtToDate.MyLinkLable1 = Nothing
        Me.dtToDate.MyLinkLable2 = Nothing
        Me.dtToDate.Name = "dtToDate"
        Me.dtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtToDate.ReferenceFieldDesc = Nothing
        Me.dtToDate.ReferenceFieldName = Nothing
        Me.dtToDate.ReferenceTableName = Nothing
        Me.dtToDate.Size = New System.Drawing.Size(82, 20)
        Me.dtToDate.TabIndex = 356
        Me.dtToDate.TabStop = False
        Me.dtToDate.Text = "17-12-2011"
        Me.dtToDate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'dtFromDate
        '
        Me.dtFromDate.CalculationExpression = Nothing
        Me.dtFromDate.CustomFormat = "dd-MM-yyyy"
        Me.dtFromDate.FieldCode = Nothing
        Me.dtFromDate.FieldDesc = Nothing
        Me.dtFromDate.FieldMaxLength = 0
        Me.dtFromDate.FieldName = Nothing
        Me.dtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtFromDate.isCalculatedField = False
        Me.dtFromDate.IsSourceFromTable = False
        Me.dtFromDate.IsSourceFromValueList = False
        Me.dtFromDate.IsUnique = False
        Me.dtFromDate.Location = New System.Drawing.Point(87, 16)
        Me.dtFromDate.MendatroryField = False
        Me.dtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtFromDate.MyLinkLable1 = Nothing
        Me.dtFromDate.MyLinkLable2 = Nothing
        Me.dtFromDate.Name = "dtFromDate"
        Me.dtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtFromDate.ReferenceFieldDesc = Nothing
        Me.dtFromDate.ReferenceFieldName = Nothing
        Me.dtFromDate.ReferenceTableName = Nothing
        Me.dtFromDate.Size = New System.Drawing.Size(82, 20)
        Me.dtFromDate.TabIndex = 355
        Me.dtFromDate.TabStop = False
        Me.dtFromDate.Text = "17-12-2011"
        Me.dtFromDate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'lblLocationCode
        '
        Me.lblLocationCode.FieldName = Nothing
        Me.lblLocationCode.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocationCode.Location = New System.Drawing.Point(18, 45)
        Me.lblLocationCode.Name = "lblLocationCode"
        Me.lblLocationCode.Size = New System.Drawing.Size(76, 18)
        Me.lblLocationCode.TabIndex = 354
        Me.lblLocationCode.Text = "Capex Budget"
        '
        'txtmulticapex
        '
        Me.txtmulticapex.arrDispalyMember = Nothing
        Me.txtmulticapex.arrValueMember = Nothing
        Me.txtmulticapex.Location = New System.Drawing.Point(122, 44)
        Me.txtmulticapex.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtmulticapex.MyLinkLable1 = Me.lblLocationCode
        Me.txtmulticapex.MyLinkLable2 = Nothing
        Me.txtmulticapex.MyNullText = "All"
        Me.txtmulticapex.Name = "txtmulticapex"
        Me.txtmulticapex.Size = New System.Drawing.Size(412, 19)
        Me.txtmulticapex.TabIndex = 353
        '
        'lblToDate
        '
        Me.lblToDate.FieldName = Nothing
        Me.lblToDate.Location = New System.Drawing.Point(175, 17)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(45, 18)
        Me.lblToDate.TabIndex = 221
        Me.lblToDate.Text = "To Date"
        '
        'lblfromDate
        '
        Me.lblfromDate.FieldName = Nothing
        Me.lblfromDate.Location = New System.Drawing.Point(18, 19)
        Me.lblfromDate.Name = "lblfromDate"
        Me.lblfromDate.Size = New System.Drawing.Size(59, 18)
        Me.lblfromDate.TabIndex = 220
        Me.lblfromDate.Text = "From Date"
        '
        'txtmultiSubCapex
        '
        Me.txtmultiSubCapex.arrDispalyMember = Nothing
        Me.txtmultiSubCapex.arrValueMember = Nothing
        Me.txtmultiSubCapex.Location = New System.Drawing.Point(122, 68)
        Me.txtmultiSubCapex.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtmultiSubCapex.MyLinkLable1 = Nothing
        Me.txtmultiSubCapex.MyLinkLable2 = Nothing
        Me.txtmultiSubCapex.MyNullText = "All"
        Me.txtmultiSubCapex.Name = "txtmultiSubCapex"
        Me.txtmultiSubCapex.Size = New System.Drawing.Size(412, 19)
        Me.txtmultiSubCapex.TabIndex = 217
        '
        'lblEmployee
        '
        Me.lblEmployee.FieldName = Nothing
        Me.lblEmployee.Location = New System.Drawing.Point(18, 69)
        Me.lblEmployee.Name = "lblEmployee"
        Me.lblEmployee.Size = New System.Drawing.Size(98, 18)
        Me.lblEmployee.TabIndex = 216
        Me.lblEmployee.Text = "Sub Capex Budget"
        '
        'chksubcapexBudget
        '
        Me.chksubcapexBudget.Location = New System.Drawing.Point(422, 17)
        Me.chksubcapexBudget.MyLinkLable1 = Nothing
        Me.chksubcapexBudget.MyLinkLable2 = Nothing
        Me.chksubcapexBudget.Name = "chksubcapexBudget"
        Me.chksubcapexBudget.Size = New System.Drawing.Size(112, 18)
        Me.chksubcapexBudget.TabIndex = 215
        Me.chksubcapexBudget.TabStop = False
        Me.chksubcapexBudget.Text = "Sub Capex Budget"
        '
        'chkCapexBudget
        '
        Me.chkCapexBudget.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCapexBudget.Location = New System.Drawing.Point(328, 17)
        Me.chkCapexBudget.MyLinkLable1 = Nothing
        Me.chkCapexBudget.MyLinkLable2 = Nothing
        Me.chkCapexBudget.Name = "chkCapexBudget"
        Me.chkCapexBudget.Size = New System.Drawing.Size(90, 18)
        Me.chkCapexBudget.TabIndex = 1
        Me.chkCapexBudget.Text = "Capex Budget"
        Me.chkCapexBudget.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv3)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(552, 175)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gv3
        '
        Me.gv3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv3.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv3.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv3.Name = "gv3"
        Me.gv3.ShowHeaderCellButtons = True
        Me.gv3.Size = New System.Drawing.Size(552, 175)
        Me.gv3.TabIndex = 1
        Me.gv3.Text = "RadGridView1"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnReset.Location = New System.Drawing.Point(83, 12)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(78, 21)
        Me.btnReset.TabIndex = 343
        Me.btnReset.Text = "Reset"
        '
        'btnQuickExport
        '
        'Me.btnQuickExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        'Me.btnQuickExport.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Me.btnQuickExport.ImageScalingSize = New System.Drawing.Size(68, 14)
        'Me.btnQuickExport.Location = New System.Drawing.Point(254, 12)
        'Me.btnQuickExport.Name = "btnQuickExport"
        'Me.btnQuickExport.Size = New System.Drawing.Size(81, 21)
        'Me.btnQuickExport.TabIndex = 342
        'Me.btnQuickExport.Text = "Quick Export"
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RmiExcel, Me.rmiPDF})
        Me.btnExport.Location = New System.Drawing.Point(168, 12)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(81, 21)
        Me.btnExport.TabIndex = 341
        Me.btnExport.Text = "Export"
        '
        'RmiExcel
        '
        Me.RmiExcel.AccessibleDescription = "Excel"
        Me.RmiExcel.AccessibleName = "Excel"
        Me.RmiExcel.Name = "RmiExcel"
        Me.RmiExcel.Text = "Excel"
        '
        'rmiPDF
        '
        Me.rmiPDF.AccessibleDescription = "PDF"
        Me.rmiPDF.AccessibleName = "PDF"
        Me.rmiPDF.Name = "rmiPDF"
        Me.rmiPDF.Text = "PDF"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnClose.Location = New System.Drawing.Point(473, 12)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(97, 21)
        Me.btnClose.TabIndex = 340
        Me.btnClose.Text = "Close"
        '
        'btnGenrate
        '
        Me.btnGenrate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGenrate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGenrate.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnGenrate.Location = New System.Drawing.Point(12, 12)
        Me.btnGenrate.Name = "btnGenrate"
        Me.btnGenrate.Size = New System.Drawing.Size(65, 21)
        Me.btnGenrate.TabIndex = 339
        Me.btnGenrate.Text = ">>"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(573, 20)
        Me.RadMenu1.TabIndex = 215
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Setting"
        Me.RadMenuItem3.AccessibleName = "Setting"
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmSaveLayout, Me.rmDeleteLayout})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        '
        'rmSaveLayout
        '
        Me.rmSaveLayout.AccessibleDescription = "Save Layout"
        Me.rmSaveLayout.AccessibleName = "Save Layout"
        Me.rmSaveLayout.Name = "rmSaveLayout"
        Me.rmSaveLayout.Text = "Save Layout"
        '
        'rmDeleteLayout
        '
        Me.rmDeleteLayout.AccessibleDescription = "RadMenuItem2"
        Me.rmDeleteLayout.AccessibleName = "RadMenuItem2"
        Me.rmDeleteLayout.Name = "rmDeleteLayout"
        Me.rmDeleteLayout.Text = "Delete Layout"
        '
        'txtFromDate
        '
        Me.txtFromDate.CalculationExpression = Nothing
        Me.txtFromDate.CustomFormat = "dd-MM-yyyy"
        Me.txtFromDate.FieldCode = Nothing
        Me.txtFromDate.FieldDesc = Nothing
        Me.txtFromDate.FieldMaxLength = 0
        Me.txtFromDate.FieldName = Nothing
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.isCalculatedField = False
        Me.txtFromDate.IsSourceFromTable = False
        Me.txtFromDate.IsSourceFromValueList = False
        Me.txtFromDate.IsUnique = False
        Me.txtFromDate.Location = New System.Drawing.Point(99, 18)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Nothing
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.ReferenceFieldDesc = Nothing
        Me.txtFromDate.ReferenceFieldName = Nothing
        Me.txtFromDate.ReferenceTableName = Nothing
        Me.txtFromDate.Size = New System.Drawing.Size(147, 20)
        Me.txtFromDate.TabIndex = 218
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "17-12-2011"
        Me.txtFromDate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'txtToDate
        '
        Me.txtToDate.CalculationExpression = Nothing
        Me.txtToDate.CustomFormat = "dd-MM-yyyy"
        Me.txtToDate.FieldCode = Nothing
        Me.txtToDate.FieldDesc = Nothing
        Me.txtToDate.FieldMaxLength = 0
        Me.txtToDate.FieldName = Nothing
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.isCalculatedField = False
        Me.txtToDate.IsSourceFromTable = False
        Me.txtToDate.IsSourceFromValueList = False
        Me.txtToDate.IsUnique = False
        Me.txtToDate.Location = New System.Drawing.Point(306, 17)
        Me.txtToDate.MendatroryField = False
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Nothing
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.ReferenceFieldDesc = Nothing
        Me.txtToDate.ReferenceFieldName = Nothing
        Me.txtToDate.ReferenceTableName = Nothing
        Me.txtToDate.Size = New System.Drawing.Size(100, 20)
        Me.txtToDate.TabIndex = 219
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "17-12-2011"
        Me.txtToDate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'RptCapexBudgetRevHis
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(573, 292)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "RptCapexBudgetRevHis"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "RptCapexBudgetRevHis"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblfromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmployee, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chksubcapexBudget, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCapexBudget, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv3.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        'CType(Me.btnQuickExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGenrate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv3 As common.UserControls.MyRadGridView
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    'Friend WithEvents btnQuickExport As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnGenrate As Telerik.WinControls.UI.RadButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblLocationCode As common.Controls.MyLabel
    Friend WithEvents txtmulticapex As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblToDate As common.Controls.MyLabel
    Friend WithEvents lblfromDate As common.Controls.MyLabel
    Friend WithEvents txtmultiSubCapex As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblEmployee As common.Controls.MyLabel
    Friend WithEvents chksubcapexBudget As common.Controls.MyRadioButton
    Friend WithEvents chkCapexBudget As common.Controls.MyRadioButton
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents dtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents dtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents RmiExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiPDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmDeleteLayout As Telerik.WinControls.UI.RadMenuItem
End Class

