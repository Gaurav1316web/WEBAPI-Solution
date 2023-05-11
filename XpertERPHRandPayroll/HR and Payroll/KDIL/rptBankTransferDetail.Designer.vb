Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RptBankTransferDetail
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
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.cbgLocation = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbglocationn = New common.MyCheckBoxGrid()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgEmployee = New common.MyCheckBoxGrid()
        Me.txtDivisionMult = New common.UserControls.txtMultiSelectFinder()
        Me.txtpayPeriod = New common.UserControls.txtFinder()
        Me.lblFrompp = New common.Controls.MyLabel()
        Me.txtBankMult = New common.UserControls.txtMultiSelectFinder()
        Me.lblBank = New common.Controls.MyLabel()
        Me.txtLocationMult = New common.UserControls.txtMultiSelectFinder()
        Me.lblDevision = New common.Controls.MyLabel()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadSplitButton1 = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmiExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnRefresh = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbgLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cbgLocation.SuspendLayout()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.lblFrompp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBank, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDevision, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadSplitButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnRefresh)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(1179, 435)
        Me.SplitContainer1.SplitterDistance = 402
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
        Me.RadPageView1.Size = New System.Drawing.Size(1179, 402)
        Me.RadPageView1.TabIndex = 343
        Me.RadPageView1.Text = "RadPageView1"
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.cbgLocation)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage1.Controls.Add(Me.txtDivisionMult)
        Me.RadPageViewPage1.Controls.Add(Me.txtpayPeriod)
        Me.RadPageViewPage1.Controls.Add(Me.txtBankMult)
        Me.RadPageViewPage1.Controls.Add(Me.lblFrompp)
        Me.RadPageViewPage1.Controls.Add(Me.lblBank)
        Me.RadPageViewPage1.Controls.Add(Me.txtLocationMult)
        Me.RadPageViewPage1.Controls.Add(Me.lblDevision)
        Me.RadPageViewPage1.Controls.Add(Me.lblLocation)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1158, 354)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(3, 22)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(92, 18)
        Me.RadLabel1.TabIndex = 224
        Me.RadLabel1.Text = "Select Pay Period"
        '
        'cbgLocation
        '
        Me.cbgLocation.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.cbgLocation.Controls.Add(Me.cbglocationn)
        Me.cbgLocation.HeaderText = "Select Division"
        Me.cbgLocation.Location = New System.Drawing.Point(105, 200)
        Me.cbgLocation.Name = "cbgLocation"
        Me.cbgLocation.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.cbgLocation.Size = New System.Drawing.Size(150, 97)
        Me.cbgLocation.TabIndex = 231
        Me.cbgLocation.Text = "Select Division"
        Me.cbgLocation.Visible = False
        '
        'cbglocationn
        '
        Me.cbglocationn.CheckedValue = Nothing
        Me.cbglocationn.DataSource = Nothing
        Me.cbglocationn.DisplayMember = "Name"
        Me.cbglocationn.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbglocationn.Location = New System.Drawing.Point(10, 20)
        Me.cbglocationn.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbglocationn.MyShowHeadrText = False
        Me.cbglocationn.Name = "cbglocationn"
        Me.cbglocationn.Size = New System.Drawing.Size(130, 67)
        Me.cbglocationn.TabIndex = 1
        Me.cbglocationn.ValueMember = "Code"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.cbgEmployee)
        Me.RadGroupBox3.HeaderText = "Select Employee"
        Me.RadGroupBox3.Location = New System.Drawing.Point(17, 130)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(92, 64)
        Me.RadGroupBox3.TabIndex = 227
        Me.RadGroupBox3.Text = "Select Employee"
        Me.RadGroupBox3.Visible = False
        '
        'cbgEmployee
        '
        Me.cbgEmployee.CheckedValue = Nothing
        Me.cbgEmployee.DataSource = Nothing
        Me.cbgEmployee.DisplayMember = "Name"
        Me.cbgEmployee.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgEmployee.Location = New System.Drawing.Point(10, 20)
        Me.cbgEmployee.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgEmployee.MyShowHeadrText = False
        Me.cbgEmployee.Name = "cbgEmployee"
        Me.cbgEmployee.Size = New System.Drawing.Size(72, 34)
        Me.cbgEmployee.TabIndex = 1
        Me.cbgEmployee.ValueMember = "Code"
        '
        'txtDivisionMult
        '
        Me.txtDivisionMult.arrDispalyMember = Nothing
        Me.txtDivisionMult.arrValueMember = Nothing
        Me.txtDivisionMult.Location = New System.Drawing.Point(105, 89)
        Me.txtDivisionMult.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDivisionMult.MyLinkLable1 = Nothing
        Me.txtDivisionMult.MyLinkLable2 = Nothing
        Me.txtDivisionMult.MyNullText = "All"
        Me.txtDivisionMult.Name = "txtDivisionMult"
        Me.txtDivisionMult.Size = New System.Drawing.Size(404, 19)
        Me.txtDivisionMult.TabIndex = 342
        '
        'txtpayPeriod
        '
        Me.txtpayPeriod.CalculationExpression = Nothing
        Me.txtpayPeriod.FieldCode = Nothing
        Me.txtpayPeriod.FieldDesc = Nothing
        Me.txtpayPeriod.FieldMaxLength = 0
        Me.txtpayPeriod.FieldName = Nothing
        Me.txtpayPeriod.isCalculatedField = False
        Me.txtpayPeriod.IsSourceFromTable = False
        Me.txtpayPeriod.IsSourceFromValueList = False
        Me.txtpayPeriod.IsUnique = False
        Me.txtpayPeriod.Location = New System.Drawing.Point(105, 20)
        Me.txtpayPeriod.MendatroryField = True
        Me.txtpayPeriod.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpayPeriod.MyLinkLable1 = Me.RadLabel1
        Me.txtpayPeriod.MyLinkLable2 = Me.lblFrompp
        Me.txtpayPeriod.MyReadOnly = False
        Me.txtpayPeriod.MyShowMasterFormButton = False
        Me.txtpayPeriod.Name = "txtpayPeriod"
        Me.txtpayPeriod.ReferenceFieldDesc = Nothing
        Me.txtpayPeriod.ReferenceFieldName = Nothing
        Me.txtpayPeriod.ReferenceTableName = Nothing
        Me.txtpayPeriod.Size = New System.Drawing.Size(208, 18)
        Me.txtpayPeriod.TabIndex = 225
        Me.txtpayPeriod.Value = ""
        '
        'lblFrompp
        '
        Me.lblFrompp.AutoSize = False
        Me.lblFrompp.BorderVisible = True
        Me.lblFrompp.FieldName = Nothing
        Me.lblFrompp.Location = New System.Drawing.Point(318, 20)
        Me.lblFrompp.Name = "lblFrompp"
        Me.lblFrompp.Size = New System.Drawing.Size(311, 19)
        Me.lblFrompp.TabIndex = 226
        Me.lblFrompp.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtBankMult
        '
        Me.txtBankMult.arrDispalyMember = Nothing
        Me.txtBankMult.arrValueMember = Nothing
        Me.txtBankMult.Location = New System.Drawing.Point(105, 64)
        Me.txtBankMult.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankMult.MyLinkLable1 = Nothing
        Me.txtBankMult.MyLinkLable2 = Nothing
        Me.txtBankMult.MyNullText = "All"
        Me.txtBankMult.Name = "txtBankMult"
        Me.txtBankMult.Size = New System.Drawing.Size(404, 19)
        Me.txtBankMult.TabIndex = 341
        '
        'lblBank
        '
        Me.lblBank.FieldName = Nothing
        Me.lblBank.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblBank.Location = New System.Drawing.Point(3, 66)
        Me.lblBank.Name = "lblBank"
        Me.lblBank.Size = New System.Drawing.Size(32, 16)
        Me.lblBank.TabIndex = 237
        Me.lblBank.Text = "Bank"
        '
        'txtLocationMult
        '
        Me.txtLocationMult.arrDispalyMember = Nothing
        Me.txtLocationMult.arrValueMember = Nothing
        Me.txtLocationMult.Location = New System.Drawing.Point(105, 40)
        Me.txtLocationMult.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocationMult.MyLinkLable1 = Nothing
        Me.txtLocationMult.MyLinkLable2 = Nothing
        Me.txtLocationMult.MyNullText = "All"
        Me.txtLocationMult.Name = "txtLocationMult"
        Me.txtLocationMult.Size = New System.Drawing.Size(404, 19)
        Me.txtLocationMult.TabIndex = 340
        '
        'lblDevision
        '
        Me.lblDevision.FieldName = Nothing
        Me.lblDevision.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblDevision.Location = New System.Drawing.Point(3, 88)
        Me.lblDevision.Name = "lblDevision"
        Me.lblDevision.Size = New System.Drawing.Size(46, 16)
        Me.lblDevision.TabIndex = 233
        Me.lblDevision.Text = "Division"
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Location = New System.Drawing.Point(3, 42)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 18)
        Me.lblLocation.TabIndex = 228
        Me.lblLocation.Text = "Location"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1158, 354)
        Me.RadPageViewPage2.Text = "Report"
        '
        'RadSplitButton1
        '
        Me.RadSplitButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadSplitButton1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExcel, Me.rmiPDF})
        Me.RadSplitButton1.Location = New System.Drawing.Point(161, 4)
        Me.RadSplitButton1.Name = "RadSplitButton1"
        Me.RadSplitButton1.Size = New System.Drawing.Size(87, 18)
        Me.RadSplitButton1.TabIndex = 333
        Me.RadSplitButton1.Text = "Export"
        '
        'rmiExcel
        '
        Me.rmiExcel.AccessibleDescription = "Excel"
        Me.rmiExcel.AccessibleName = "Excel"
        Me.rmiExcel.Name = "rmiExcel"
        Me.rmiExcel.Text = "Excel"
        '
        'rmiPDF
        '
        Me.rmiPDF.AccessibleDescription = "PDF"
        Me.rmiPDF.AccessibleName = "PDF"
        Me.rmiPDF.Name = "rmiPDF"
        Me.rmiPDF.Text = "PDF"
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRefresh.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnRefresh.Location = New System.Drawing.Point(16, 4)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(67, 18)
        Me.btnRefresh.TabIndex = 8
        Me.btnRefresh.Text = ">> "
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnSave.Location = New System.Drawing.Point(88, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(67, 18)
        Me.btnSave.TabIndex = 5
        Me.btnSave.Text = "Print "
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnClose.Location = New System.Drawing.Point(1098, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 6
        Me.btnClose.Text = "Close"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1179, 20)
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
        Me.rmDeleteLayout.AccessibleDescription = "Delete Layout"
        Me.rmDeleteLayout.AccessibleName = "Delete Layout"
        Me.rmDeleteLayout.Name = "rmDeleteLayout"
        Me.rmDeleteLayout.Text = "Delete Layout"
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.AllowEditRow = False
        Me.gv1.MasterTemplate.EnableFiltering = True
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(1158, 354)
        Me.gv1.TabIndex = 147
        Me.gv1.Text = "RadGridView4"
        '
        'RptBankTransferDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1179, 455)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "RptBankTransferDetail"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "RptBankTransferDetail"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbgLocation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cbgLocation.ResumeLayout(False)
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        CType(Me.lblFrompp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBank, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDevision, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgEmployee As common.MyCheckBoxGrid
    Friend WithEvents lblFrompp As common.Controls.MyLabel
    Friend WithEvents txtpayPeriod As common.UserControls.txtFinder
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents cbgLocation As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbglocationn As common.MyCheckBoxGrid
    Friend WithEvents lblDevision As common.Controls.MyLabel
    Friend WithEvents lblBank As common.Controls.MyLabel
    Friend WithEvents txtDivisionMult As common.UserControls.txtMultiSelectFinder
    Friend WithEvents txtBankMult As common.UserControls.txtMultiSelectFinder
    Friend WithEvents txtLocationMult As common.UserControls.txtMultiSelectFinder
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents btnRefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadSplitButton1 As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents rmiExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiPDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
End Class

