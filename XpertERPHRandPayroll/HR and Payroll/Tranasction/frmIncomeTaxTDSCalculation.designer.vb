
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmIncomeTaxTDSCalculation
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer6 = New System.Windows.Forms.SplitContainer()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.txtEmployee = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.txtFiscalYear = New common.UserControls.txtFinder()
        Me.lblFiscalYear = New common.Controls.MyTextBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.lblPending = New common.usLock()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.txtDesc = New common.Controls.MyTextBox()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvEmp = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvTax = New common.UserControls.MyRadGridView()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer6.Panel1.SuspendLayout()
        Me.SplitContainer6.Panel2.SuspendLayout()
        Me.SplitContainer6.SuspendLayout()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFiscalYear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.gvEmp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvEmp.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage3.SuspendLayout()
        CType(Me.gvTax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvTax.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer6)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(658, 384)
        Me.SplitContainer1.SplitterDistance = 354
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer6
        '
        Me.SplitContainer6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer6.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer6.IsSplitterFixed = True
        Me.SplitContainer6.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer6.Name = "SplitContainer6"
        Me.SplitContainer6.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer6.Panel1
        '
        Me.SplitContainer6.Panel1.Controls.Add(Me.btnGo)
        Me.SplitContainer6.Panel1.Controls.Add(Me.txtEmployee)
        Me.SplitContainer6.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer6.Panel1.Controls.Add(Me.RadLabel4)
        Me.SplitContainer6.Panel1.Controls.Add(Me.txtDate)
        Me.SplitContainer6.Panel1.Controls.Add(Me.txtFiscalYear)
        Me.SplitContainer6.Panel1.Controls.Add(Me.lblFiscalYear)
        Me.SplitContainer6.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer6.Panel1.Controls.Add(Me.lblPending)
        Me.SplitContainer6.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer6.Panel1.Controls.Add(Me.RadLabel1)
        Me.SplitContainer6.Panel1.Controls.Add(Me.btnReset)
        Me.SplitContainer6.Panel1.Controls.Add(Me.RadLabel2)
        Me.SplitContainer6.Panel1.Controls.Add(Me.txtDesc)
        '
        'SplitContainer6.Panel2
        '
        Me.SplitContainer6.Panel2.Controls.Add(Me.RadPageView1)
        Me.SplitContainer6.Size = New System.Drawing.Size(658, 354)
        Me.SplitContainer6.SplitterDistance = 97
        Me.SplitContainer6.TabIndex = 0
        '
        'btnGo
        '
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(590, 50)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(68, 42)
        Me.btnGo.TabIndex = 5
        Me.btnGo.Text = ">>"
        '
        'txtEmployee
        '
        Me.txtEmployee.arrDispalyMember = Nothing
        Me.txtEmployee.arrValueMember = Nothing
        Me.txtEmployee.Location = New System.Drawing.Point(75, 72)
        Me.txtEmployee.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmployee.MyLinkLable1 = Me.MyLabel4
        Me.txtEmployee.MyLinkLable2 = Nothing
        Me.txtEmployee.MyNullText = "Please Select..."
        Me.txtEmployee.Name = "txtEmployee"
        Me.txtEmployee.Size = New System.Drawing.Size(513, 20)
        Me.txtEmployee.TabIndex = 4
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(5, 73)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(55, 18)
        Me.MyLabel4.TabIndex = 10
        Me.MyLabel4.Text = "Employee"
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(338, 8)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 56
        Me.RadLabel4.Text = "Date"
        '
        'txtDate
        '
        Me.txtDate.CalculationExpression = Nothing
        Me.txtDate.CustomFormat = "dd/MM/yyyy"
        Me.txtDate.FieldCode = Nothing
        Me.txtDate.FieldDesc = Nothing
        Me.txtDate.FieldMaxLength = 0
        Me.txtDate.FieldName = Nothing
        Me.txtDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDate.isCalculatedField = False
        Me.txtDate.IsSourceFromTable = False
        Me.txtDate.IsSourceFromValueList = False
        Me.txtDate.IsUnique = False
        Me.txtDate.Location = New System.Drawing.Point(372, 7)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.RadLabel4
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(83, 18)
        Me.txtDate.TabIndex = 1
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'txtFiscalYear
        '
        Me.txtFiscalYear.CalculationExpression = Nothing
        Me.txtFiscalYear.FieldCode = Nothing
        Me.txtFiscalYear.FieldDesc = Nothing
        Me.txtFiscalYear.FieldMaxLength = 0
        Me.txtFiscalYear.FieldName = Nothing
        Me.txtFiscalYear.isCalculatedField = False
        Me.txtFiscalYear.IsSourceFromTable = False
        Me.txtFiscalYear.IsSourceFromValueList = False
        Me.txtFiscalYear.IsUnique = False
        Me.txtFiscalYear.Location = New System.Drawing.Point(75, 49)
        Me.txtFiscalYear.MendatroryField = True
        Me.txtFiscalYear.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFiscalYear.MyLinkLable1 = Nothing
        Me.txtFiscalYear.MyLinkLable2 = Nothing
        Me.txtFiscalYear.MyReadOnly = False
        Me.txtFiscalYear.MyShowMasterFormButton = False
        Me.txtFiscalYear.Name = "txtFiscalYear"
        Me.txtFiscalYear.ReferenceFieldDesc = Nothing
        Me.txtFiscalYear.ReferenceFieldName = Nothing
        Me.txtFiscalYear.ReferenceTableName = Nothing
        Me.txtFiscalYear.Size = New System.Drawing.Size(141, 20)
        Me.txtFiscalYear.TabIndex = 3
        Me.txtFiscalYear.Value = ""
        '
        'lblFiscalYear
        '
        Me.lblFiscalYear.CalculationExpression = Nothing
        Me.lblFiscalYear.FieldCode = Nothing
        Me.lblFiscalYear.FieldDesc = Nothing
        Me.lblFiscalYear.FieldMaxLength = 0
        Me.lblFiscalYear.FieldName = Nothing
        Me.lblFiscalYear.isCalculatedField = False
        Me.lblFiscalYear.IsSourceFromTable = False
        Me.lblFiscalYear.IsSourceFromValueList = False
        Me.lblFiscalYear.IsUnique = False
        Me.lblFiscalYear.Location = New System.Drawing.Point(218, 49)
        Me.lblFiscalYear.MendatroryField = False
        Me.lblFiscalYear.MyLinkLable1 = Nothing
        Me.lblFiscalYear.MyLinkLable2 = Nothing
        Me.lblFiscalYear.Name = "lblFiscalYear"
        Me.lblFiscalYear.ReadOnly = True
        Me.lblFiscalYear.ReferenceFieldDesc = Nothing
        Me.lblFiscalYear.ReferenceFieldName = Nothing
        Me.lblFiscalYear.ReferenceTableName = Nothing
        Me.lblFiscalYear.Size = New System.Drawing.Size(370, 20)
        Me.lblFiscalYear.TabIndex = 6
        Me.lblFiscalYear.TabStop = False
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Location = New System.Drawing.Point(5, 50)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(58, 18)
        Me.MyLabel3.TabIndex = 7
        Me.MyLabel3.Text = "Fiscal Year"
        '
        'lblPending
        '
        Me.lblPending.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lblPending.Location = New System.Drawing.Point(465, 5)
        Me.lblPending.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPending.Name = "lblPending"
        Me.lblPending.Size = New System.Drawing.Size(182, 20)
        Me.lblPending.Status = common.ERPTransactionStatus.Pending
        Me.lblPending.TabIndex = 44
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(75, 5)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.RadLabel1
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 32767
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(241, 20)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.RadLabel1.Location = New System.Drawing.Point(5, 7)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(33, 16)
        Me.RadLabel1.TabIndex = 9
        Me.RadLabel1.Text = "Code"
        '
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Image = Global.XpertERPHRandPayroll.My.Resources.Resources._new
        Me.btnReset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReset.Location = New System.Drawing.Point(316, 5)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(19, 20)
        Me.btnReset.TabIndex = 1
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(5, 29)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel2.TabIndex = 8
        Me.RadLabel2.Text = "Description"
        '
        'txtDesc
        '
        Me.txtDesc.CalculationExpression = Nothing
        Me.txtDesc.FieldCode = Nothing
        Me.txtDesc.FieldDesc = Nothing
        Me.txtDesc.FieldMaxLength = 0
        Me.txtDesc.FieldName = Nothing
        Me.txtDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc.isCalculatedField = False
        Me.txtDesc.IsSourceFromTable = False
        Me.txtDesc.IsSourceFromValueList = False
        Me.txtDesc.IsUnique = False
        Me.txtDesc.Location = New System.Drawing.Point(75, 28)
        Me.txtDesc.MendatroryField = True
        Me.txtDesc.MyLinkLable1 = Me.RadLabel2
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ReferenceFieldDesc = Nothing
        Me.txtDesc.ReferenceFieldName = Nothing
        Me.txtDesc.ReferenceTableName = Nothing
        Me.txtDesc.Size = New System.Drawing.Size(580, 18)
        Me.txtDesc.TabIndex = 2
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(658, 253)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.gvEmp)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(92.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(637, 205)
        Me.RadPageViewPage1.Text = "Employee Wise"
        '
        'gvEmp
        '
        Me.gvEmp.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvEmp.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvEmp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvEmp.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvEmp.ForeColor = System.Drawing.Color.Black
        Me.gvEmp.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvEmp.Location = New System.Drawing.Point(0, 0)
        '
        'gvEmp
        '
        Me.gvEmp.MasterTemplate.AllowDeleteRow = False
        Me.gvEmp.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvEmp.Name = "gvEmp"
        Me.gvEmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvEmp.ShowGroupPanel = False
        Me.gvEmp.ShowHeaderCellButtons = True
        Me.gvEmp.Size = New System.Drawing.Size(637, 205)
        Me.gvEmp.TabIndex = 1
        Me.gvEmp.TabStop = False
        Me.gvEmp.Text = "RadGridView1"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(124.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(637, 205)
        Me.RadPageViewPage2.Text = "Allowance & Sections"
        '
        'gv
        '
        Me.gv.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv.ForeColor = System.Drawing.Color.Black
        Me.gv.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv.Location = New System.Drawing.Point(0, 0)
        '
        'gv
        '
        Me.gv.MasterTemplate.AllowDeleteRow = False
        Me.gv.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv.Name = "gv"
        Me.gv.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv.ShowGroupPanel = False
        Me.gv.ShowHeaderCellButtons = True
        Me.gv.Size = New System.Drawing.Size(637, 205)
        Me.gv.TabIndex = 1
        Me.gv.TabStop = False
        Me.gv.Text = "RadGridView1"
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.gvTax)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(125.0!, 28.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(637, 205)
        Me.RadPageViewPage3.Text = "Slab Wise Income Tax"
        '
        'gvTax
        '
        Me.gvTax.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvTax.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvTax.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvTax.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvTax.ForeColor = System.Drawing.Color.Black
        Me.gvTax.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvTax.Location = New System.Drawing.Point(0, 0)
        '
        'gvTax
        '
        Me.gvTax.MasterTemplate.AllowDeleteRow = False
        Me.gvTax.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvTax.Name = "gvTax"
        Me.gvTax.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvTax.ShowGroupPanel = False
        Me.gvTax.ShowHeaderCellButtons = True
        Me.gvTax.Size = New System.Drawing.Size(637, 205)
        Me.gvTax.TabIndex = 1
        Me.gvTax.TabStop = False
        Me.gvTax.Text = "RadGridView1"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(163, 4)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(81, 18)
        Me.btnPost.TabIndex = 2
        Me.btnPost.Text = "Post"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(575, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(80, 18)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Location = New System.Drawing.Point(83, 4)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(80, 18)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(3, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(80, 18)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'frmIncomeTaxTDSCalculation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(658, 384)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmIncomeTaxTDSCalculation"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Income Tax TDS Calculation"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer6.Panel1.ResumeLayout(False)
        Me.SplitContainer6.Panel1.PerformLayout()
        Me.SplitContainer6.Panel2.ResumeLayout(False)
        Me.SplitContainer6.ResumeLayout(False)
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFiscalYear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.gvEmp.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvEmp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage3.ResumeLayout(False)
        CType(Me.gvTax.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvTax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    ' Friend WithEvents UcCustomFields1 As EnumCustomFieldType
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDesc As common.Controls.MyTextBox
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents grdScheme As common.UserControls.MyRadGridView
    Friend WithEvents SplitContainer6 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents lblPending As common.usLock
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtFiscalYear As common.UserControls.txtFinder
    Friend WithEvents lblFiscalYear As common.Controls.MyTextBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage3 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtEmployee As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents gvEmp As common.UserControls.MyRadGridView
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents gvTax As common.UserControls.MyRadGridView
End Class

