Imports XpertERPEngine
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmArrear
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
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.fndFromPeriod = New common.UserControls.txtFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtApplicabbleDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.txtmulLocation = New common.UserControls.txtMultiSelectFinder()
        Me.txtmultPayperiod = New common.UserControls.txtMultiSelectFinder()
        Me.BtnGo = New Telerik.WinControls.UI.RadButton()
        Me.txtDAArrear = New common.Controls.MyTextBox()
        Me.MyLabel60 = New common.Controls.MyLabel()
        Me.lblShiftType = New Telerik.WinControls.UI.RadLabel()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel15 = New common.Controls.MyLabel()
        Me.lblPayperiod = New common.Controls.MyLabel()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv2 = New common.UserControls.MyRadGridView()
        Me.btnHistory = New Telerik.WinControls.UI.RadButton()
        Me.RadSplitButton1 = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnReverse = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnExcelExport = New Telerik.WinControls.UI.RadSplitButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtApplicabbleDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDAArrear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel60, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblShiftType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPayperiod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExcelExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExcelExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnHistory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadSplitButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReverse)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Size = New System.Drawing.Size(913, 450)
        Me.SplitContainer1.SplitterDistance = 414
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(913, 414)
        Me.RadPageView1.TabIndex = 1
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.fndFromPeriod)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.txtApplicabbleDate)
        Me.RadPageViewPage1.Controls.Add(Me.UsLock1)
        Me.RadPageViewPage1.Controls.Add(Me.txtmulLocation)
        Me.RadPageViewPage1.Controls.Add(Me.txtmultPayperiod)
        Me.RadPageViewPage1.Controls.Add(Me.BtnGo)
        Me.RadPageViewPage1.Controls.Add(Me.txtDAArrear)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel60)
        Me.RadPageViewPage1.Controls.Add(Me.lblShiftType)
        Me.RadPageViewPage1.Controls.Add(Me.txtDate)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel15)
        Me.RadPageViewPage1.Controls.Add(Me.lblPayperiod)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.txtDocNo)
        Me.RadPageViewPage1.Controls.Add(Me.btnAddNew)
        Me.RadPageViewPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 26.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(892, 368)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(359, 31)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(117, 16)
        Me.MyLabel2.TabIndex = 153
        Me.MyLabel2.Text = "Applicable Pay Period"
        '
        'fndFromPeriod
        '
        Me.fndFromPeriod.CalculationExpression = Nothing
        Me.fndFromPeriod.FieldCode = Nothing
        Me.fndFromPeriod.FieldDesc = Nothing
        Me.fndFromPeriod.FieldMaxLength = 0
        Me.fndFromPeriod.FieldName = Nothing
        Me.fndFromPeriod.isCalculatedField = False
        Me.fndFromPeriod.IsSourceFromTable = False
        Me.fndFromPeriod.IsSourceFromValueList = False
        Me.fndFromPeriod.IsUnique = False
        Me.fndFromPeriod.Location = New System.Drawing.Point(481, 30)
        Me.fndFromPeriod.MendatroryField = True
        Me.fndFromPeriod.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndFromPeriod.MyLinkLable1 = Nothing
        Me.fndFromPeriod.MyLinkLable2 = Nothing
        Me.fndFromPeriod.MyReadOnly = False
        Me.fndFromPeriod.MyShowMasterFormButton = False
        Me.fndFromPeriod.Name = "fndFromPeriod"
        Me.fndFromPeriod.ReferenceFieldDesc = Nothing
        Me.fndFromPeriod.ReferenceFieldName = Nothing
        Me.fndFromPeriod.ReferenceTableName = Nothing
        Me.fndFromPeriod.Size = New System.Drawing.Size(180, 19)
        Me.fndFromPeriod.TabIndex = 1582
        Me.fndFromPeriod.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(3, 72)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(86, 16)
        Me.MyLabel1.TabIndex = 1581
        Me.MyLabel1.Text = "Applicable Date"
        '
        'txtApplicabbleDate
        '
        Me.txtApplicabbleDate.CalculationExpression = Nothing
        Me.txtApplicabbleDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtApplicabbleDate.FieldCode = Nothing
        Me.txtApplicabbleDate.FieldDesc = Nothing
        Me.txtApplicabbleDate.FieldMaxLength = 0
        Me.txtApplicabbleDate.FieldName = Nothing
        Me.txtApplicabbleDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtApplicabbleDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtApplicabbleDate.isCalculatedField = False
        Me.txtApplicabbleDate.IsSourceFromTable = False
        Me.txtApplicabbleDate.IsSourceFromValueList = False
        Me.txtApplicabbleDate.IsUnique = False
        Me.txtApplicabbleDate.Location = New System.Drawing.Point(93, 72)
        Me.txtApplicabbleDate.MendatroryField = False
        Me.txtApplicabbleDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtApplicabbleDate.MyLinkLable1 = Me.RadLabel4
        Me.txtApplicabbleDate.MyLinkLable2 = Nothing
        Me.txtApplicabbleDate.Name = "txtApplicabbleDate"
        Me.txtApplicabbleDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtApplicabbleDate.ReferenceFieldDesc = Nothing
        Me.txtApplicabbleDate.ReferenceFieldName = Nothing
        Me.txtApplicabbleDate.ReferenceTableName = Nothing
        Me.txtApplicabbleDate.Size = New System.Drawing.Size(77, 18)
        Me.txtApplicabbleDate.TabIndex = 1580
        Me.txtApplicabbleDate.TabStop = False
        Me.txtApplicabbleDate.Text = "13/06/2011 11:29 AM"
        Me.txtApplicabbleDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(362, 3)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 1
        Me.RadLabel4.Text = "Date"
        '
        'UsLock1
        '
        Me.UsLock1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(529, 2)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(107, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 1579
        '
        'txtmulLocation
        '
        Me.txtmulLocation.arrDispalyMember = Nothing
        Me.txtmulLocation.arrValueMember = Nothing
        Me.txtmulLocation.Location = New System.Drawing.Point(63, 52)
        Me.txtmulLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtmulLocation.MyLinkLable1 = Nothing
        Me.txtmulLocation.MyLinkLable2 = Nothing
        Me.txtmulLocation.MyNullText = "All"
        Me.txtmulLocation.Name = "txtmulLocation"
        Me.txtmulLocation.Size = New System.Drawing.Size(291, 17)
        Me.txtmulLocation.TabIndex = 1578
        '
        'txtmultPayperiod
        '
        Me.txtmultPayperiod.arrDispalyMember = Nothing
        Me.txtmultPayperiod.arrValueMember = Nothing
        Me.txtmultPayperiod.Location = New System.Drawing.Point(63, 29)
        Me.txtmultPayperiod.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtmultPayperiod.MyLinkLable1 = Nothing
        Me.txtmultPayperiod.MyLinkLable2 = Nothing
        Me.txtmultPayperiod.MyNullText = "All"
        Me.txtmultPayperiod.Name = "txtmultPayperiod"
        Me.txtmultPayperiod.Size = New System.Drawing.Size(291, 20)
        Me.txtmultPayperiod.TabIndex = 1577
        '
        'BtnGo
        '
        Me.BtnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnGo.Location = New System.Drawing.Point(359, 72)
        Me.BtnGo.Name = "BtnGo"
        Me.BtnGo.Size = New System.Drawing.Size(80, 20)
        Me.BtnGo.TabIndex = 1575
        Me.BtnGo.Text = ">>>"
        '
        'txtDAArrear
        '
        Me.txtDAArrear.CalculationExpression = Nothing
        Me.txtDAArrear.FieldCode = Nothing
        Me.txtDAArrear.FieldDesc = Nothing
        Me.txtDAArrear.FieldMaxLength = 0
        Me.txtDAArrear.FieldName = Nothing
        Me.txtDAArrear.isCalculatedField = False
        Me.txtDAArrear.IsSourceFromTable = False
        Me.txtDAArrear.IsSourceFromValueList = False
        Me.txtDAArrear.IsUnique = False
        Me.txtDAArrear.Location = New System.Drawing.Point(245, 72)
        Me.txtDAArrear.MendatroryField = False
        Me.txtDAArrear.MyLinkLable1 = Nothing
        Me.txtDAArrear.MyLinkLable2 = Nothing
        Me.txtDAArrear.Name = "txtDAArrear"
        Me.txtDAArrear.ReferenceFieldDesc = Nothing
        Me.txtDAArrear.ReferenceFieldName = Nothing
        Me.txtDAArrear.ReferenceTableName = Nothing
        Me.txtDAArrear.Size = New System.Drawing.Size(111, 20)
        Me.txtDAArrear.TabIndex = 1532
        Me.txtDAArrear.Text = "0"
        Me.txtDAArrear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'MyLabel60
        '
        Me.MyLabel60.FieldName = Nothing
        Me.MyLabel60.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel60.Location = New System.Drawing.Point(172, 73)
        Me.MyLabel60.Name = "MyLabel60"
        Me.MyLabel60.Size = New System.Drawing.Size(70, 16)
        Me.MyLabel60.TabIndex = 1545
        Me.MyLabel60.Text = "DA Arrear %"
        '
        'lblShiftType
        '
        Me.lblShiftType.Location = New System.Drawing.Point(686, -3)
        Me.lblShiftType.Name = "lblShiftType"
        Me.lblShiftType.Size = New System.Drawing.Size(2, 2)
        Me.lblShiftType.TabIndex = 1536
        '
        'txtDate
        '
        Me.txtDate.CalculationExpression = Nothing
        Me.txtDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
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
        Me.txtDate.Location = New System.Drawing.Point(400, 2)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.RadLabel4
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(123, 18)
        Me.txtDate.TabIndex = 1524
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011 11:29 AM"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel15
        '
        Me.RadLabel15.FieldName = Nothing
        Me.RadLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel15.Location = New System.Drawing.Point(3, 52)
        Me.RadLabel15.Name = "RadLabel15"
        Me.RadLabel15.Size = New System.Drawing.Size(49, 16)
        Me.RadLabel15.TabIndex = 152
        Me.RadLabel15.Text = "Location"
        '
        'lblPayperiod
        '
        Me.lblPayperiod.FieldName = Nothing
        Me.lblPayperiod.Location = New System.Drawing.Point(2, 29)
        Me.lblPayperiod.Name = "lblPayperiod"
        Me.lblPayperiod.Size = New System.Drawing.Size(59, 18)
        Me.lblPayperiod.TabIndex = 119
        Me.lblPayperiod.Text = "Pay Period"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox2.Controls.Add(Me.gv1)
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "DA Arrear"
        Me.RadGroupBox2.Location = New System.Drawing.Point(0, 108)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(892, 268)
        Me.RadGroupBox2.TabIndex = 28
        Me.RadGroupBox2.Text = "DA Arrear"
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(10, 20)
        '
        '
        '
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1.MyExportAPI = False
        Me.gv1.MyExportFilePath = ""
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(872, 238)
        Me.gv1.TabIndex = 17
        Me.gv1.TabStop = False
        Me.gv1.VarID = ""
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(3, 5)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(33, 16)
        Me.RadLabel1.TabIndex = 22
        Me.RadLabel1.Text = "Code"
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(66, 1)
        Me.txtDocNo.MendatroryField = False
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Me.RadLabel1
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 32767
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(269, 21)
        Me.txtDocNo.TabIndex = 0
        Me.txtDocNo.TabStop = False
        Me.txtDocNo.Value = ""
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.XpertERPHRandPayroll.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(336, 1)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 21)
        Me.btnAddNew.TabIndex = 1
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv2)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(45.0!, 26.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(892, 368)
        Me.RadPageViewPage2.Text = "Detail"
        '
        'gv2
        '
        Me.gv2.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv2.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv2.ForeColor = System.Drawing.Color.Black
        Me.gv2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv2.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv2.MasterTemplate.AllowDeleteRow = False
        Me.gv2.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv2.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv2.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gv2.MyExportAPI = False
        Me.gv2.MyExportFilePath = ""
        Me.gv2.MyStopExport = False
        Me.gv2.Name = "gv2"
        Me.gv2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv2.ShowGroupPanel = False
        Me.gv2.ShowHeaderCellButtons = True
        Me.gv2.Size = New System.Drawing.Size(892, 368)
        Me.gv2.TabIndex = 4
        Me.gv2.TabStop = False
        Me.gv2.VarID = ""
        '
        'btnHistory
        '
        Me.btnHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnHistory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHistory.Location = New System.Drawing.Point(504, 3)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Size = New System.Drawing.Size(70, 22)
        Me.btnHistory.TabIndex = 1582
        Me.btnHistory.Text = "History"
        Me.btnHistory.Visible = False
        '
        'RadSplitButton1
        '
        Me.RadSplitButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadSplitButton1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmExcel, Me.rmPDF})
        Me.RadSplitButton1.Location = New System.Drawing.Point(211, 3)
        Me.RadSplitButton1.Name = "RadSplitButton1"
        Me.RadSplitButton1.Size = New System.Drawing.Size(77, 22)
        Me.RadSplitButton1.TabIndex = 1581
        Me.RadSplitButton1.Text = "Export"
        '
        'rmExcel
        '
        Me.rmExcel.Name = "rmExcel"
        Me.rmExcel.Text = "Arrear Summary"
        Me.rmExcel.UseCompatibleTextRendering = False
        '
        'rmPDF
        '
        Me.rmPDF.Name = "rmPDF"
        Me.rmPDF.Text = "Arrear Detail"
        Me.rmPDF.UseCompatibleTextRendering = False
        '
        'btnReverse
        '
        Me.btnReverse.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReverse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverse.Location = New System.Drawing.Point(430, 3)
        Me.btnReverse.Name = "btnReverse"
        Me.btnReverse.Size = New System.Drawing.Size(70, 22)
        Me.btnReverse.TabIndex = 1580
        Me.btnReverse.Text = "Reverse"
        Me.btnReverse.Visible = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(837, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(67, 22)
        Me.btnClose.TabIndex = 21
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(141, 3)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(67, 22)
        Me.btnDelete.TabIndex = 20
        Me.btnDelete.Text = "Delete"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(12, 3)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(61, 22)
        Me.btnSave.TabIndex = 18
        Me.btnSave.Text = "Save"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(75, 3)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(63, 22)
        Me.btnPost.TabIndex = 19
        Me.btnPost.Text = "Post"
        '
        'btnExcelExport
        '
        Me.btnExcelExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExcelExport.Location = New System.Drawing.Point(294, 3)
        Me.btnExcelExport.Name = "btnExcelExport"
        Me.btnExcelExport.Size = New System.Drawing.Size(91, 22)
        Me.btnExcelExport.TabIndex = 1583
        Me.btnExcelExport.Text = "Export Excel"
        '
        'frmArrear
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(913, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmArrear"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "frmArrear"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtApplicabbleDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDAArrear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel60, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblShiftType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPayperiod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExcelExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv2 As common.UserControls.MyRadGridView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents txtmulLocation As common.UserControls.txtMultiSelectFinder
    Friend WithEvents txtmultPayperiod As common.UserControls.txtMultiSelectFinder
    Friend WithEvents BtnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDAArrear As common.Controls.MyTextBox
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel60 As common.Controls.MyLabel
    Friend WithEvents lblShiftType As Telerik.WinControls.UI.RadLabel
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel15 As common.Controls.MyLabel
    Friend WithEvents lblPayperiod As common.Controls.MyLabel
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReverse As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadSplitButton1 As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents rmExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmPDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtApplicabbleDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents fndFromPeriod As common.UserControls.txtFinder
    Friend WithEvents btnHistory As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnExcelExport As Telerik.WinControls.UI.RadSplitButton
End Class
