<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmDocumentCancelledReport
    Inherits FrmMainTranScreen

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgUser = New common.MyCheckBoxGrid()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.chkUserSelect = New common.Controls.MyRadioButton()
        Me.ChkUserAll = New common.Controls.MyRadioButton()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgLocation = New common.MyCheckBoxGrid()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.chkLocSelect = New common.Controls.MyRadioButton()
        Me.chkLocAll = New common.Controls.MyRadioButton()
        Me.btnShow = New Telerik.WinControls.UI.RadButton()
        Me.dtpToDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.dtpFromDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.cboTransaction = New common.Controls.MyComboBox()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.cboModule = New common.Controls.MyComboBox()
        Me.RadLabel10 = New common.Controls.MyLabel()
        Me.RadSplitButton1 = New Telerik.WinControls.UI.RadSplitButton()
        Me.btn_Excel = New Telerik.WinControls.UI.RadMenuItem()
        Me.btn_PDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.lblNoOfRecords = New common.Controls.MyLabel()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.chkUserSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkUserAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnShow, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboTransaction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboModule, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNoOfRecords, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(992, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "Close"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnShow)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpToDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpFromDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboTransaction)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboModule)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel10)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadSplitButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lblNoOfRecords)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(1064, 459)
        Me.SplitContainer1.SplitterDistance = 422
        Me.SplitContainer1.TabIndex = 0
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.cbgUser)
        Me.RadGroupBox4.Controls.Add(Me.Panel2)
        Me.RadGroupBox4.HeaderText = "User"
        Me.RadGroupBox4.Location = New System.Drawing.Point(774, 3)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(282, 72)
        Me.RadGroupBox4.TabIndex = 15
        Me.RadGroupBox4.Text = "User"
        Me.RadGroupBox4.Visible = False
        '
        'cbgUser
        '
        Me.cbgUser.CheckedValue = Nothing
        Me.cbgUser.DataSource = Nothing
        Me.cbgUser.DisplayMember = "Name"
        Me.cbgUser.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgUser.Location = New System.Drawing.Point(10, 40)
        Me.cbgUser.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgUser.MyShowHeadrText = False
        Me.cbgUser.Name = "cbgUser"
        Me.cbgUser.Size = New System.Drawing.Size(262, 22)
        Me.cbgUser.TabIndex = 1
        Me.cbgUser.TabStop = False
        Me.cbgUser.ValueMember = "Code"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chkUserSelect)
        Me.Panel2.Controls.Add(Me.ChkUserAll)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(262, 20)
        Me.Panel2.TabIndex = 0
        '
        'chkUserSelect
        '
        Me.chkUserSelect.Location = New System.Drawing.Point(129, 1)
        Me.chkUserSelect.MyLinkLable1 = Nothing
        Me.chkUserSelect.MyLinkLable2 = Nothing
        Me.chkUserSelect.Name = "chkUserSelect"
        Me.chkUserSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkUserSelect.TabIndex = 1
        Me.chkUserSelect.Text = "Select"
        '
        'ChkUserAll
        '
        Me.ChkUserAll.Location = New System.Drawing.Point(92, 1)
        Me.ChkUserAll.MyLinkLable1 = Nothing
        Me.ChkUserAll.MyLinkLable2 = Nothing
        Me.ChkUserAll.Name = "ChkUserAll"
        Me.ChkUserAll.Size = New System.Drawing.Size(33, 18)
        Me.ChkUserAll.TabIndex = 0
        Me.ChkUserAll.Text = "All"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.cbgLocation)
        Me.RadGroupBox3.Controls.Add(Me.Panel1)
        Me.RadGroupBox3.HeaderText = "Location"
        Me.RadGroupBox3.Location = New System.Drawing.Point(489, 3)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(279, 72)
        Me.RadGroupBox3.TabIndex = 14
        Me.RadGroupBox3.Text = "Location"
        Me.RadGroupBox3.Visible = False
        '
        'cbgLocation
        '
        Me.cbgLocation.CheckedValue = Nothing
        Me.cbgLocation.DataSource = Nothing
        Me.cbgLocation.DisplayMember = "Name"
        Me.cbgLocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgLocation.Location = New System.Drawing.Point(10, 40)
        Me.cbgLocation.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgLocation.MyShowHeadrText = False
        Me.cbgLocation.Name = "cbgLocation"
        Me.cbgLocation.Size = New System.Drawing.Size(259, 22)
        Me.cbgLocation.TabIndex = 1
        Me.cbgLocation.TabStop = False
        Me.cbgLocation.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkLocSelect)
        Me.Panel1.Controls.Add(Me.chkLocAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(259, 20)
        Me.Panel1.TabIndex = 0
        '
        'chkLocSelect
        '
        Me.chkLocSelect.Location = New System.Drawing.Point(133, 1)
        Me.chkLocSelect.MyLinkLable1 = Nothing
        Me.chkLocSelect.MyLinkLable2 = Nothing
        Me.chkLocSelect.Name = "chkLocSelect"
        Me.chkLocSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkLocSelect.TabIndex = 1
        Me.chkLocSelect.Text = "Select"
        '
        'chkLocAll
        '
        Me.chkLocAll.Location = New System.Drawing.Point(81, 1)
        Me.chkLocAll.MyLinkLable1 = Nothing
        Me.chkLocAll.MyLinkLable2 = Nothing
        Me.chkLocAll.Name = "chkLocAll"
        Me.chkLocAll.Size = New System.Drawing.Size(33, 18)
        Me.chkLocAll.TabIndex = 0
        Me.chkLocAll.Text = "All"
        '
        'btnShow
        '
        Me.btnShow.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShow.Location = New System.Drawing.Point(323, 36)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(81, 39)
        Me.btnShow.TabIndex = 7
        Me.btnShow.Text = ">>"
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpToDate.Location = New System.Drawing.Point(231, 10)
        Me.dtpToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.Size = New System.Drawing.Size(86, 20)
        Me.dtpToDate.TabIndex = 5
        Me.dtpToDate.TabStop = False
        Me.dtpToDate.Text = "22/07/2011"
        Me.dtpToDate.Value = New Date(2011, 7, 22, 13, 55, 15, 703)
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFromDate.Location = New System.Drawing.Point(83, 10)
        Me.dtpFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromDate.Size = New System.Drawing.Size(90, 20)
        Me.dtpFromDate.TabIndex = 4
        Me.dtpFromDate.TabStop = False
        Me.dtpFromDate.Text = "22/07/2011"
        Me.dtpFromDate.Value = New Date(2011, 7, 22, 13, 55, 15, 703)
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(179, 12)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(46, 16)
        Me.RadLabel3.TabIndex = 12
        Me.RadLabel3.Text = "To Date"
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(8, 10)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(60, 16)
        Me.RadLabel2.TabIndex = 9
        Me.RadLabel2.Text = "From Date"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox2.Controls.Add(Me.gv1)
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "Transaction Details"
        Me.RadGroupBox2.Location = New System.Drawing.Point(5, 85)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(1056, 335)
        Me.RadGroupBox2.TabIndex = 8
        Me.RadGroupBox2.Text = "Transaction Details"
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(10, 20)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.EnableFiltering = True
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(1036, 305)
        Me.gv1.TabIndex = 0
        Me.gv1.Text = "RadGridView1"
        '
        'cboTransaction
        '
        Me.cboTransaction.CalculationExpression = Nothing
        Me.cboTransaction.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboTransaction.FieldCode = Nothing
        Me.cboTransaction.FieldDesc = Nothing
        Me.cboTransaction.FieldMaxLength = 0
        Me.cboTransaction.FieldName = Nothing
        Me.cboTransaction.isCalculatedField = False
        Me.cboTransaction.IsSourceFromTable = False
        Me.cboTransaction.IsSourceFromValueList = False
        Me.cboTransaction.IsUnique = False
        Me.cboTransaction.Location = New System.Drawing.Point(83, 36)
        Me.cboTransaction.MendatroryField = True
        Me.cboTransaction.MyLinkLable1 = Nothing
        Me.cboTransaction.MyLinkLable2 = Nothing
        Me.cboTransaction.Name = "cboTransaction"
        Me.cboTransaction.ReferenceFieldDesc = Nothing
        Me.cboTransaction.ReferenceFieldName = Nothing
        Me.cboTransaction.ReferenceTableName = Nothing
        Me.cboTransaction.Size = New System.Drawing.Size(147, 20)
        Me.cboTransaction.TabIndex = 3
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(8, 36)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(65, 16)
        Me.RadLabel1.TabIndex = 10
        Me.RadLabel1.Text = "Transaction"
        '
        'cboModule
        '
        Me.cboModule.CalculationExpression = Nothing
        Me.cboModule.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboModule.FieldCode = Nothing
        Me.cboModule.FieldDesc = Nothing
        Me.cboModule.FieldMaxLength = 0
        Me.cboModule.FieldName = Nothing
        Me.cboModule.isCalculatedField = False
        Me.cboModule.IsSourceFromTable = False
        Me.cboModule.IsSourceFromValueList = False
        Me.cboModule.IsUnique = False
        Me.cboModule.Location = New System.Drawing.Point(59, 59)
        Me.cboModule.MendatroryField = True
        Me.cboModule.MyLinkLable1 = Nothing
        Me.cboModule.MyLinkLable2 = Nothing
        Me.cboModule.Name = "cboModule"
        Me.cboModule.ReferenceFieldDesc = Nothing
        Me.cboModule.ReferenceFieldName = Nothing
        Me.cboModule.ReferenceTableName = Nothing
        Me.cboModule.Size = New System.Drawing.Size(147, 20)
        Me.cboModule.TabIndex = 0
        Me.cboModule.Visible = False
        '
        'RadLabel10
        '
        Me.RadLabel10.FieldName = Nothing
        Me.RadLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel10.Location = New System.Drawing.Point(10, 61)
        Me.RadLabel10.Name = "RadLabel10"
        Me.RadLabel10.Size = New System.Drawing.Size(43, 16)
        Me.RadLabel10.TabIndex = 11
        Me.RadLabel10.Text = "Module"
        Me.RadLabel10.Visible = False
        '
        'RadSplitButton1
        '
        Me.RadSplitButton1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btn_Excel, Me.btn_PDF})
        Me.RadSplitButton1.Location = New System.Drawing.Point(85, 5)
        Me.RadSplitButton1.Name = "RadSplitButton1"
        Me.RadSplitButton1.Size = New System.Drawing.Size(95, 22)
        Me.RadSplitButton1.TabIndex = 151
        Me.RadSplitButton1.Text = "Export"
        '
        'btn_Excel
        '
        Me.btn_Excel.AccessibleDescription = "btn_Excel"
        Me.btn_Excel.AccessibleName = "btn_Excel"
        Me.btn_Excel.Name = "btn_Excel"
        Me.btn_Excel.Text = "Excel"
        '
        'btn_PDF
        '
        Me.btn_PDF.AccessibleDescription = "btn_PDF"
        Me.btn_PDF.AccessibleName = "btn_PDF"
        Me.btn_PDF.Name = "btn_PDF"
        Me.btn_PDF.Text = "PDF"
        '
        'btnReset
        '
        Me.btnReset.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(10, 5)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(69, 22)
        Me.btnReset.TabIndex = 6
        Me.btnReset.Text = "Reset"
        '
        'lblNoOfRecords
        '
        Me.lblNoOfRecords.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblNoOfRecords.FieldName = Nothing
        Me.lblNoOfRecords.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNoOfRecords.Location = New System.Drawing.Point(860, 8)
        Me.lblNoOfRecords.Name = "lblNoOfRecords"
        Me.lblNoOfRecords.Size = New System.Drawing.Size(87, 16)
        Me.lblNoOfRecords.TabIndex = 2
        Me.lblNoOfRecords.Text = "0 Record Found"
        '
        'frmDocumentCancelledReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1064, 459)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmDocumentCancelledReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Document Cancelled Report"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.chkUserSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkUserAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnShow, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboTransaction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboModule, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNoOfRecords, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents cboTransaction As common.Controls.MyComboBox
    Friend WithEvents cboModule As common.Controls.MyComboBox
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents dtpFromDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents btnShow As Telerik.WinControls.UI.RadButton
    Friend WithEvents dtpToDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadLabel10 As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents lblNoOfRecords As common.Controls.MyLabel
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgUser As common.MyCheckBoxGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chkUserSelect As common.Controls.MyRadioButton
    Friend WithEvents ChkUserAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkLocSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocAll As common.Controls.MyRadioButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadSplitButton1 As RadSplitButton
    Friend WithEvents btn_Excel As RadMenuItem
    Friend WithEvents btn_PDF As RadMenuItem
End Class

