<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBatchOrder
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBatchOrder))
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.lblCode = New common.Controls.MyLabel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.lblApprovedBy = New common.Controls.MyLabel()
        Me.lblApprovedByName = New common.Controls.MyLabel()
        Me.lblPPDate = New common.Controls.MyLabel()
        Me.dtpBODate = New common.Controls.MyDateTimePicker()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtManualBatchNo = New common.Controls.MyTextBox()
        Me.lblLocationDesc = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.fndLocation = New common.UserControls.txtFinder()
        Me.btnFillPP = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgPP = New common.MyCheckBoxGrid()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.gvRM = New common.UserControls.MyRadGridView()
        Me.RadButton2 = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.gvPP = New common.UserControls.MyRadGridView()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.lblCreatedByName = New common.Controls.MyLabel()
        Me.lblCreatedBy = New common.Controls.MyLabel()
        Me.lblBomDesc = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.lblApprovedBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblApprovedByName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPPDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpBODate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtManualBatchNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnFillPP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvRM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvRM.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvPP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvPP.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCreatedByName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCreatedBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBomDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(110, 14)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 30
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(322, 21)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'lblCode
        '
        Me.lblCode.FieldName = Nothing
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCode.Location = New System.Drawing.Point(24, 40)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(65, 16)
        Me.lblCode.TabIndex = 1
        Me.lblCode.Text = "Batch Code"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.lblCode)
        Me.RadGroupBox1.Controls.Add(Me.SplitContainer1)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(943, 612)
        Me.RadGroupBox1.TabIndex = 0
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(10, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblApprovedBy)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblApprovedByName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblPPDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpBODate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtManualBatchNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLocationDesc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnFillPP)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCreatedByName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCreatedBy)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblBomDesc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnNew)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(923, 582)
        Me.SplitContainer1.SplitterDistance = 541
        Me.SplitContainer1.TabIndex = 0
        '
        'lblApprovedBy
        '
        Me.lblApprovedBy.FieldName = Nothing
        Me.lblApprovedBy.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblApprovedBy.Location = New System.Drawing.Point(453, 39)
        Me.lblApprovedBy.Name = "lblApprovedBy"
        Me.lblApprovedBy.Size = New System.Drawing.Size(71, 16)
        Me.lblApprovedBy.TabIndex = 13
        Me.lblApprovedBy.Text = "Approved By"
        '
        'lblApprovedByName
        '
        Me.lblApprovedByName.AutoSize = False
        Me.lblApprovedByName.BorderVisible = True
        Me.lblApprovedByName.FieldName = Nothing
        Me.lblApprovedByName.Location = New System.Drawing.Point(536, 38)
        Me.lblApprovedByName.Name = "lblApprovedByName"
        Me.lblApprovedByName.Size = New System.Drawing.Size(178, 19)
        Me.lblApprovedByName.TabIndex = 14
        '
        'lblPPDate
        '
        Me.lblPPDate.FieldName = Nothing
        Me.lblPPDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPPDate.Location = New System.Drawing.Point(720, 39)
        Me.lblPPDate.Name = "lblPPDate"
        Me.lblPPDate.Size = New System.Drawing.Size(50, 16)
        Me.lblPPDate.TabIndex = 8
        Me.lblPPDate.Text = "BO Date"
        '
        'dtpBODate
        '
        Me.dtpBODate.CalculationExpression = Nothing
        Me.dtpBODate.CustomFormat = "dd/MM/yyyy"
        Me.dtpBODate.FieldCode = Nothing
        Me.dtpBODate.FieldDesc = Nothing
        Me.dtpBODate.FieldMaxLength = 0
        Me.dtpBODate.FieldName = Nothing
        Me.dtpBODate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpBODate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpBODate.isCalculatedField = False
        Me.dtpBODate.IsSourceFromTable = False
        Me.dtpBODate.IsSourceFromValueList = False
        Me.dtpBODate.IsUnique = False
        Me.dtpBODate.Location = New System.Drawing.Point(776, 38)
        Me.dtpBODate.MendatroryField = True
        Me.dtpBODate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpBODate.MyLinkLable1 = Me.lblPPDate
        Me.dtpBODate.MyLinkLable2 = Nothing
        Me.dtpBODate.Name = "dtpBODate"
        Me.dtpBODate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpBODate.ReferenceFieldDesc = Nothing
        Me.dtpBODate.ReferenceFieldName = Nothing
        Me.dtpBODate.ReferenceTableName = Nothing
        Me.dtpBODate.Size = New System.Drawing.Size(91, 18)
        Me.dtpBODate.TabIndex = 3
        Me.dtpBODate.TabStop = False
        Me.dtpBODate.Text = "03/05/2011"
        Me.dtpBODate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel3.Location = New System.Drawing.Point(12, 59)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(93, 18)
        Me.MyLabel3.TabIndex = 51
        Me.MyLabel3.Text = "Manual Batch No"
        '
        'txtManualBatchNo
        '
        Me.txtManualBatchNo.CalculationExpression = Nothing
        Me.txtManualBatchNo.FieldCode = Nothing
        Me.txtManualBatchNo.FieldDesc = Nothing
        Me.txtManualBatchNo.FieldMaxLength = 0
        Me.txtManualBatchNo.FieldName = Nothing
        Me.txtManualBatchNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtManualBatchNo.isCalculatedField = False
        Me.txtManualBatchNo.IsSourceFromTable = False
        Me.txtManualBatchNo.IsSourceFromValueList = False
        Me.txtManualBatchNo.IsUnique = False
        Me.txtManualBatchNo.Location = New System.Drawing.Point(110, 59)
        Me.txtManualBatchNo.MaxLength = 200
        Me.txtManualBatchNo.MendatroryField = False
        Me.txtManualBatchNo.MyLinkLable1 = Nothing
        Me.txtManualBatchNo.MyLinkLable2 = Nothing
        Me.txtManualBatchNo.Name = "txtManualBatchNo"
        Me.txtManualBatchNo.ReferenceFieldDesc = Nothing
        Me.txtManualBatchNo.ReferenceFieldName = Nothing
        Me.txtManualBatchNo.ReferenceTableName = Nothing
        Me.txtManualBatchNo.Size = New System.Drawing.Size(337, 18)
        Me.txtManualBatchNo.TabIndex = 50
        '
        'lblLocationDesc
        '
        Me.lblLocationDesc.AutoSize = False
        Me.lblLocationDesc.BorderVisible = True
        Me.lblLocationDesc.FieldName = Nothing
        Me.lblLocationDesc.Location = New System.Drawing.Point(718, 59)
        Me.lblLocationDesc.Name = "lblLocationDesc"
        Me.lblLocationDesc.Size = New System.Drawing.Size(203, 19)
        Me.lblLocationDesc.TabIndex = 49
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel2.Location = New System.Drawing.Point(453, 59)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(78, 18)
        Me.MyLabel2.TabIndex = 48
        Me.MyLabel2.Text = "Location Code"
        '
        'fndLocation
        '
        Me.fndLocation.CalculationExpression = Nothing
        Me.fndLocation.FieldCode = Nothing
        Me.fndLocation.FieldDesc = Nothing
        Me.fndLocation.FieldMaxLength = 0
        Me.fndLocation.FieldName = Nothing
        Me.fndLocation.isCalculatedField = False
        Me.fndLocation.IsSourceFromTable = False
        Me.fndLocation.IsSourceFromValueList = False
        Me.fndLocation.IsUnique = False
        Me.fndLocation.Location = New System.Drawing.Point(536, 59)
        Me.fndLocation.MendatroryField = True
        Me.fndLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLocation.MyLinkLable1 = Me.MyLabel2
        Me.fndLocation.MyLinkLable2 = Nothing
        Me.fndLocation.MyReadOnly = False
        Me.fndLocation.MyShowMasterFormButton = False
        Me.fndLocation.Name = "fndLocation"
        Me.fndLocation.ReferenceFieldDesc = Nothing
        Me.fndLocation.ReferenceFieldName = Nothing
        Me.fndLocation.ReferenceTableName = Nothing
        Me.fndLocation.Size = New System.Drawing.Size(176, 19)
        Me.fndLocation.TabIndex = 47
        Me.fndLocation.Value = ""
        '
        'btnFillPP
        '
        Me.btnFillPP.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnFillPP.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFillPP.Location = New System.Drawing.Point(870, 205)
        Me.btnFillPP.Name = "btnFillPP"
        Me.btnFillPP.Size = New System.Drawing.Size(41, 18)
        Me.btnFillPP.TabIndex = 5
        Me.btnFillPP.Text = ">>"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox4.Controls.Add(Me.cbgPP)
        Me.RadGroupBox4.HeaderText = "Select Production Plan"
        Me.RadGroupBox4.Location = New System.Drawing.Point(12, 84)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(852, 139)
        Me.RadGroupBox4.TabIndex = 4
        Me.RadGroupBox4.Text = "Select Production Plan"
        '
        'cbgPP
        '
        Me.cbgPP.CheckedValue = Nothing
        Me.cbgPP.DataSource = Nothing
        Me.cbgPP.DisplayMember = "Name"
        Me.cbgPP.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgPP.Location = New System.Drawing.Point(10, 20)
        Me.cbgPP.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgPP.MyShowHeadrText = False
        Me.cbgPP.Name = "cbgPP"
        Me.cbgPP.Size = New System.Drawing.Size(832, 109)
        Me.cbgPP.TabIndex = 0
        Me.cbgPP.ValueMember = "Code"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox3.Controls.Add(Me.gvRM)
        Me.RadGroupBox3.Controls.Add(Me.RadButton2)
        Me.RadGroupBox3.HeaderText = "Raw Material Details"
        Me.RadGroupBox3.Location = New System.Drawing.Point(10, 366)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(907, 168)
        Me.RadGroupBox3.TabIndex = 7
        Me.RadGroupBox3.Text = "Raw Material Details"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(12, 89)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(2, 2)
        Me.MyLabel1.TabIndex = 19
        '
        'gvRM
        '
        Me.gvRM.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvRM.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvRM.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvRM.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvRM.ForeColor = System.Drawing.Color.Black
        Me.gvRM.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvRM.Location = New System.Drawing.Point(10, 20)
        '
        '
        '
        Me.gvRM.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvRM.MasterTemplate.AllowAddNewRow = False
        Me.gvRM.MasterTemplate.AutoGenerateColumns = False
        Me.gvRM.MasterTemplate.EnableGrouping = False
        Me.gvRM.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvRM.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvRM.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gvRM.MyStopExport = False
        Me.gvRM.Name = "gvRM"
        Me.gvRM.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvRM.ShowHeaderCellButtons = True
        Me.gvRM.Size = New System.Drawing.Size(887, 138)
        Me.gvRM.TabIndex = 5
        Me.gvRM.TabStop = False
        '
        'RadButton2
        '
        Me.RadButton2.Image = CType(resources.GetObject("RadButton2.Image"), System.Drawing.Image)
        Me.RadButton2.Location = New System.Drawing.Point(352, -422)
        Me.RadButton2.Name = "RadButton2"
        Me.RadButton2.Size = New System.Drawing.Size(14, 20)
        Me.RadButton2.TabIndex = 17
        Me.RadButton2.Text = " "
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox2.Controls.Add(Me.RadLabel4)
        Me.RadGroupBox2.Controls.Add(Me.gvPP)
        Me.RadGroupBox2.Controls.Add(Me.RadButton1)
        Me.RadGroupBox2.HeaderText = " Production Plan Item Details"
        Me.RadGroupBox2.Location = New System.Drawing.Point(12, 226)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(907, 134)
        Me.RadGroupBox2.TabIndex = 6
        Me.RadGroupBox2.Text = " Production Plan Item Details"
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Location = New System.Drawing.Point(12, 89)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(2, 2)
        Me.RadLabel4.TabIndex = 19
        '
        'gvPP
        '
        Me.gvPP.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvPP.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvPP.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvPP.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvPP.ForeColor = System.Drawing.Color.Black
        Me.gvPP.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvPP.Location = New System.Drawing.Point(10, 20)
        '
        '
        '
        Me.gvPP.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvPP.MasterTemplate.AllowAddNewRow = False
        Me.gvPP.MasterTemplate.AutoGenerateColumns = False
        Me.gvPP.MasterTemplate.EnableGrouping = False
        Me.gvPP.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvPP.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvPP.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gvPP.MyStopExport = False
        Me.gvPP.Name = "gvPP"
        Me.gvPP.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvPP.ShowHeaderCellButtons = True
        Me.gvPP.Size = New System.Drawing.Size(887, 104)
        Me.gvPP.TabIndex = 5
        Me.gvPP.TabStop = False
        '
        'RadButton1
        '
        Me.RadButton1.Image = CType(resources.GetObject("RadButton1.Image"), System.Drawing.Image)
        Me.RadButton1.Location = New System.Drawing.Point(352, -422)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(14, 20)
        Me.RadButton1.TabIndex = 17
        Me.RadButton1.Text = " "
        '
        'lblCreatedByName
        '
        Me.lblCreatedByName.AutoSize = False
        Me.lblCreatedByName.BorderVisible = True
        Me.lblCreatedByName.FieldName = Nothing
        Me.lblCreatedByName.Location = New System.Drawing.Point(536, 15)
        Me.lblCreatedByName.Name = "lblCreatedByName"
        Me.lblCreatedByName.Size = New System.Drawing.Size(178, 19)
        Me.lblCreatedByName.TabIndex = 12
        '
        'lblCreatedBy
        '
        Me.lblCreatedBy.FieldName = Nothing
        Me.lblCreatedBy.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblCreatedBy.Location = New System.Drawing.Point(453, 16)
        Me.lblCreatedBy.Name = "lblCreatedBy"
        Me.lblCreatedBy.Size = New System.Drawing.Size(63, 16)
        Me.lblCreatedBy.TabIndex = 10
        Me.lblCreatedBy.Text = "Created By"
        '
        'lblBomDesc
        '
        Me.lblBomDesc.FieldName = Nothing
        Me.lblBomDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBomDesc.Location = New System.Drawing.Point(12, 39)
        Me.lblBomDesc.Name = "lblBomDesc"
        Me.lblBomDesc.Size = New System.Drawing.Size(63, 16)
        Me.lblBomDesc.TabIndex = 9
        Me.lblBomDesc.Text = "Description"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(769, 15)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 11
        '
        'txtDescription
        '
        Me.txtDescription.CalculationExpression = Nothing
        Me.txtDescription.FieldCode = Nothing
        Me.txtDescription.FieldDesc = Nothing
        Me.txtDescription.FieldMaxLength = 0
        Me.txtDescription.FieldName = Nothing
        Me.txtDescription.isCalculatedField = False
        Me.txtDescription.IsSourceFromTable = False
        Me.txtDescription.IsSourceFromValueList = False
        Me.txtDescription.IsUnique = False
        Me.txtDescription.Location = New System.Drawing.Point(110, 37)
        Me.txtDescription.MaxLength = 50
        Me.txtDescription.MendatroryField = True
        Me.txtDescription.MyLinkLable1 = Me.lblBomDesc
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReferenceFieldDesc = Nothing
        Me.txtDescription.ReferenceFieldName = Nothing
        Me.txtDescription.ReferenceTableName = Nothing
        Me.txtDescription.Size = New System.Drawing.Size(337, 20)
        Me.txtDescription.TabIndex = 2
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(433, 14)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 20)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Text = " "
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(228, 8)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(69, 22)
        Me.btnPrint.TabIndex = 3
        Me.btnPrint.Text = "Print"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(80, 8)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(68, 22)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(8, 8)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(68, 22)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(851, 11)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(68, 18)
        Me.btnclose.TabIndex = 4
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(154, 8)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(68, 22)
        Me.btndelete.TabIndex = 2
        Me.btndelete.Text = "Delete"
        '
        'frmBatchOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(943, 612)
        Me.Controls.Add(Me.RadGroupBox1)
        Me.Name = "frmBatchOrder"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Batch Order"
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.lblApprovedBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblApprovedByName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPPDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpBODate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtManualBatchNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnFillPP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvRM.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvRM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvPP.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvPP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCreatedByName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCreatedBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBomDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents lblPPDate As common.Controls.MyLabel
    Friend WithEvents dtpBODate As common.Controls.MyDateTimePicker
    Friend WithEvents gvPP As common.UserControls.MyRadGridView
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblBomDesc As common.Controls.MyLabel
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents lblApprovedByName As common.Controls.MyLabel
    Friend WithEvents lblApprovedBy As common.Controls.MyLabel
    Friend WithEvents lblCreatedByName As common.Controls.MyLabel
    Friend WithEvents lblCreatedBy As common.Controls.MyLabel
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents gvRM As common.UserControls.MyRadGridView
    Friend WithEvents RadButton2 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgPP As common.MyCheckBoxGrid
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnFillPP As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblLocationDesc As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents fndLocation As common.UserControls.txtFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtManualBatchNo As common.Controls.MyTextBox
End Class
