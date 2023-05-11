<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmApprovalAlertSumm
    Inherits XpertERPEngine.FrmMainTranScreen

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
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem5 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem6 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv_Alert = New common.UserControls.MyRadGridView()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnRefresh = New Telerik.WinControls.UI.RadButton()
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton()
        Me.btnExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnCloseAlert = New Telerik.WinControls.UI.RadButton()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv_Doc = New common.UserControls.MyRadGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cboType = New common.Controls.MyComboBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.btnExport_Doc = New Telerik.WinControls.UI.RadSplitButton()
        Me.btnExcel_Doc = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnPDF_Doc = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnBack = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv_Alert, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_Alert.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCloseAlert, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.gv_Doc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_Doc.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.cboType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport_Doc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPageView1
        '
        Me.RadPageView1.BackColor = System.Drawing.Color.Transparent
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 20)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(817, 487)
        Me.RadPageView1.TabIndex = 26
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.Panel2)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(147.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(796, 439)
        Me.RadPageViewPage1.Text = "Messages/Alerts Overview"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.RadGroupBox2.Controls.Add(Me.gv_Alert)
        Me.RadGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "Messages/Alerts"
        Me.RadGroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(796, 415)
        Me.RadGroupBox2.TabIndex = 1
        Me.RadGroupBox2.Text = "Messages/Alerts"
        '
        'gv_Alert
        '
        Me.gv_Alert.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv_Alert.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv_Alert.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv_Alert.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv_Alert.ForeColor = System.Drawing.Color.Black
        Me.gv_Alert.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv_Alert.Location = New System.Drawing.Point(10, 20)
        '
        'gv_Alert
        '
        Me.gv_Alert.MasterTemplate.AllowAddNewRow = False
        Me.gv_Alert.MasterTemplate.AllowDeleteRow = False
        Me.gv_Alert.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv_Alert.Name = "gv_Alert"
        Me.gv_Alert.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv_Alert.ShowGroupPanel = False
        Me.gv_Alert.ShowHeaderCellButtons = True
        Me.gv_Alert.Size = New System.Drawing.Size(776, 385)
        Me.gv_Alert.TabIndex = 0
        Me.gv_Alert.TabStop = False
        Me.gv_Alert.Text = "RadGridView1"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel2.Controls.Add(Me.btnRefresh)
        Me.Panel2.Controls.Add(Me.btnExport)
        Me.Panel2.Controls.Add(Me.btnCloseAlert)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 415)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(796, 24)
        Me.Panel2.TabIndex = 2
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(78, 1)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(68, 22)
        Me.btnRefresh.TabIndex = 3
        Me.btnRefresh.Text = "Refersh"
        '
        'btnExport
        '
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnExcel, Me.btnPDF})
        Me.btnExport.Location = New System.Drawing.Point(3, 0)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(71, 23)
        Me.btnExport.TabIndex = 2
        Me.btnExport.Text = "Export"
        '
        'btnExcel
        '
        Me.btnExcel.AccessibleDescription = "Export to Excel"
        Me.btnExcel.AccessibleName = "Export to Excel"
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Text = "Export to Excel"
        '
        'btnPDF
        '
        Me.btnPDF.AccessibleDescription = "RadMenuItem4"
        Me.btnPDF.AccessibleName = "RadMenuItem4"
        Me.btnPDF.Name = "btnPDF"
        Me.btnPDF.Text = "Export to PDF"
        '
        'btnCloseAlert
        '
        Me.btnCloseAlert.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCloseAlert.Location = New System.Drawing.Point(725, 1)
        Me.btnCloseAlert.Name = "btnCloseAlert"
        Me.btnCloseAlert.Size = New System.Drawing.Size(68, 22)
        Me.btnCloseAlert.TabIndex = 1
        Me.btnCloseAlert.Text = "Close"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage2.Controls.Add(Me.Panel1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(134.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(796, 439)
        Me.RadPageViewPage2.Text = "Document for Approval"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadGroupBox1.Controls.Add(Me.gv_Doc)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = "Document List"
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 24)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(796, 415)
        Me.RadGroupBox1.TabIndex = 2
        Me.RadGroupBox1.Text = "Document List"
        '
        'gv_Doc
        '
        Me.gv_Doc.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv_Doc.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv_Doc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv_Doc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv_Doc.ForeColor = System.Drawing.Color.Black
        Me.gv_Doc.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv_Doc.Location = New System.Drawing.Point(10, 20)
        '
        'gv_Doc
        '
        Me.gv_Doc.MasterTemplate.AllowAddNewRow = False
        Me.gv_Doc.MasterTemplate.AllowDeleteRow = False
        Me.gv_Doc.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv_Doc.Name = "gv_Doc"
        Me.gv_Doc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv_Doc.ShowGroupPanel = False
        Me.gv_Doc.ShowHeaderCellButtons = True
        Me.gv_Doc.Size = New System.Drawing.Size(776, 385)
        Me.gv_Doc.TabIndex = 0
        Me.gv_Doc.TabStop = False
        Me.gv_Doc.Text = "RadGridView1"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel1.Controls.Add(Me.cboType)
        Me.Panel1.Controls.Add(Me.MyLabel1)
        Me.Panel1.Controls.Add(Me.btnExport_Doc)
        Me.Panel1.Controls.Add(Me.btnBack)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(796, 24)
        Me.Panel1.TabIndex = 1
        '
        'cboType
        '
        Me.cboType.AutoCompleteDisplayMember = Nothing
        Me.cboType.AutoCompleteValueMember = Nothing
        Me.cboType.CalculationExpression = Nothing
        Me.cboType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboType.FieldCode = Nothing
        Me.cboType.FieldDesc = Nothing
        Me.cboType.FieldMaxLength = 0
        Me.cboType.FieldName = Nothing
        Me.cboType.isCalculatedField = False
        Me.cboType.IsSourceFromTable = False
        Me.cboType.IsSourceFromValueList = False
        Me.cboType.IsUnique = False
        RadListDataItem4.Text = "Level1"
        RadListDataItem5.Text = "Level2"
        RadListDataItem6.Text = "Level3"
        Me.cboType.Items.Add(RadListDataItem4)
        Me.cboType.Items.Add(RadListDataItem5)
        Me.cboType.Items.Add(RadListDataItem6)
        Me.cboType.Location = New System.Drawing.Point(243, 2)
        Me.cboType.MendatroryField = True
        Me.cboType.MyLinkLable1 = Me.MyLabel1
        Me.cboType.MyLinkLable2 = Nothing
        Me.cboType.Name = "cboType"
        Me.cboType.ReferenceFieldDesc = Nothing
        Me.cboType.ReferenceFieldName = Nothing
        Me.cboType.ReferenceTableName = Nothing
        Me.cboType.Size = New System.Drawing.Size(132, 20)
        Me.cboType.TabIndex = 26
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(208, 4)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(31, 16)
        Me.MyLabel1.TabIndex = 27
        Me.MyLabel1.Text = "Type"
        '
        'btnExport_Doc
        '
        Me.btnExport_Doc.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnExcel_Doc, Me.btnPDF_Doc})
        Me.btnExport_Doc.Location = New System.Drawing.Point(77, 0)
        Me.btnExport_Doc.Name = "btnExport_Doc"
        Me.btnExport_Doc.Size = New System.Drawing.Size(76, 23)
        Me.btnExport_Doc.TabIndex = 3
        Me.btnExport_Doc.Text = "Export"
        '
        'btnExcel_Doc
        '
        Me.btnExcel_Doc.AccessibleDescription = "Export to Excel"
        Me.btnExcel_Doc.AccessibleName = "Export to Excel"
        Me.btnExcel_Doc.Name = "btnExcel_Doc"
        Me.btnExcel_Doc.Text = "Export to Excel"
        '
        'btnPDF_Doc
        '
        Me.btnPDF_Doc.AccessibleDescription = "Export to PDF"
        Me.btnPDF_Doc.AccessibleName = "Export to PDF"
        Me.btnPDF_Doc.Name = "btnPDF_Doc"
        Me.btnPDF_Doc.Text = "Export to PDF"
        '
        'btnBack
        '
        Me.btnBack.Location = New System.Drawing.Point(3, 1)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(68, 22)
        Me.btnBack.TabIndex = 2
        Me.btnBack.Text = "<<Back"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(725, 1)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 22)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "Close"
        '
        'rdmenufile
        '
        Me.rdmenufile.AccessibleDescription = "RadMenuItem1"
        Me.rdmenufile.AccessibleName = "RadMenuItem1"
        Me.rdmenufile.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItem2})
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Text = "File"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Save Layout"
        Me.RadMenuItem1.AccessibleName = "Save Layout"
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Save Layout"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Delete Layout"
        Me.RadMenuItem2.AccessibleName = "Delete Layout"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Delete Layout"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenufile})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(817, 20)
        Me.RadMenu1.TabIndex = 25
        Me.RadMenu1.Text = "RadMenu1"
        '
        'FrmApprovalAlertSumm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(817, 507)
        Me.Controls.Add(Me.RadPageView1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmApprovalAlertSumm"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmApprovalAlertSumm"
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv_Alert.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_Alert, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCloseAlert, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.gv_Doc.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_Doc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.cboType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport_Doc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv_Alert As common.UserControls.MyRadGridView
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv_Doc As common.UserControls.MyRadGridView
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnBack As Telerik.WinControls.UI.RadButton
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnCloseAlert As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnExcel_Doc As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnPDF_Doc As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnExport_Doc As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnPDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnRefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents cboType As common.Controls.MyComboBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
End Class

