<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmQualityCheckForSRN
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
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmQualityCheckForSRN))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv_MRN = New common.UserControls.MyRadGridView()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv = New common.UserControls.MyRadGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtRALNo = New common.Controls.MyTextBox()
        Me.RadLabel21 = New common.Controls.MyLabel()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.fnd_PendingMRN = New common.UserControls.txtFinder()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.TxtVendor_desc = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtAccept = New System.Windows.Forms.Label()
        Me.UsLock1 = New common.usLock()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.RadLabel20 = New common.Controls.MyLabel()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.txtGENo = New common.Controls.MyTextBox()
        Me.txtDesc = New common.Controls.MyTextBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtGEDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.cboSRNType = New common.Controls.MyComboBox()
        Me.dtpDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel29 = New common.Controls.MyLabel()
        Me.fndVendor_code = New common.UserControls.txtFinder()
        Me.cboItemType = New common.Controls.MyComboBox()
        Me.lblBillToLocation = New common.Controls.MyLabel()
        Me.RadLabel15 = New common.Controls.MyLabel()
        Me.txtBillToLocation = New common.UserControls.txtFinder()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.lblRalPrint = New common.Controls.MyLabel()
        Me.TxtFinderRalPrint = New common.UserControls.txtFinder()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.lblVendorPrint = New common.Controls.MyLabel()
        Me.lblItemPrint = New common.Controls.MyLabel()
        Me.TxtFinderItemPrint = New common.UserControls.txtFinder()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.TxtFinderVendorPrint = New common.UserControls.txtFinder()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.ToDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.fromDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.btnRALWiseAnaysisPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnRejected = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmiEnglish = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiHindi = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnAnalysisPrintVertical = New Telerik.WinControls.UI.RadButton()
        Me.btnAnalysisPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnSendEmail = New Telerik.WinControls.UI.RadButton()
        Me.btnreverse = New Telerik.WinControls.UI.RadButton()
        Me.btnTemplates = New Telerik.WinControls.UI.RadButton()
        Me.btnpost = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv_MRN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_MRN.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.txtRALNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtVendor_desc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGENo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGEDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboSRNType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel29, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboItemType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBillToLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        Me.RadPageViewPage3.SuspendLayout()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRalPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendorPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblItemPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRALWiseAnaysisPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRejected, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAnalysisPrintVertical, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAnalysisPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSendEmail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnTemplates, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnpost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Padding = New System.Windows.Forms.Padding(1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnRALWiseAnaysisPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnRejected)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnAnalysisPrintVertical)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnAnalysisPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSendEmail)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnreverse)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnTemplates)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnpost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(1061, 493)
        Me.SplitContainer1.SplitterDistance = 460
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(1, 1)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage3
        Me.RadPageView1.Size = New System.Drawing.Size(1059, 458)
        Me.RadPageView1.TabIndex = 0
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer2)
        Me.RadPageViewPage1.Controls.Add(Me.Panel1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(114.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1038, 410)
        Me.RadPageViewPage1.Text = "Quality Check Entry"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 171)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadGroupBox2)
        Me.SplitContainer2.Panel1.Padding = New System.Windows.Forms.Padding(1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadGroupBox1)
        Me.SplitContainer2.Panel2.Padding = New System.Windows.Forms.Padding(1)
        Me.SplitContainer2.Size = New System.Drawing.Size(1038, 239)
        Me.SplitContainer2.SplitterDistance = 153
        Me.SplitContainer2.TabIndex = 72
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.gv_MRN)
        Me.RadGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "MRN Details"
        Me.RadGroupBox2.Location = New System.Drawing.Point(1, 1)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(1036, 151)
        Me.RadGroupBox2.TabIndex = 0
        Me.RadGroupBox2.Text = "MRN Details"
        '
        'gv_MRN
        '
        Me.gv_MRN.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv_MRN.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv_MRN.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv_MRN.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv_MRN.ForeColor = System.Drawing.Color.Black
        Me.gv_MRN.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv_MRN.Location = New System.Drawing.Point(10, 20)
        '
        '
        '
        Me.gv_MRN.MasterTemplate.AllowDeleteRow = False
        Me.gv_MRN.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv_MRN.MasterTemplate.ShowHeaderCellButtons = True
        'Me.gv_MRN.MasterTemplate.ViewDefinition = TableViewDefinition5
        Me.gv_MRN.Name = "gv_MRN"
        Me.gv_MRN.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv_MRN.ShowGroupPanel = False
        Me.gv_MRN.ShowHeaderCellButtons = True
        Me.gv_MRN.Size = New System.Drawing.Size(1016, 121)
        Me.gv_MRN.TabIndex = 0
        Me.gv_MRN.TabStop = False
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.gv)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = "Item Details"
        Me.RadGroupBox1.Location = New System.Drawing.Point(1, 1)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(1036, 80)
        Me.RadGroupBox1.TabIndex = 0
        Me.RadGroupBox1.Text = "Item Details"
        '
        'gv
        '
        Me.gv.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv.ForeColor = System.Drawing.Color.Black
        Me.gv.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv.Location = New System.Drawing.Point(10, 20)
        '
        '
        '
        Me.gv.MasterTemplate.AllowDeleteRow = False
        Me.gv.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv.MasterTemplate.ShowHeaderCellButtons = True
        'Me.gv.MasterTemplate.ViewDefinition = TableViewDefinition6
        Me.gv.Name = "gv"
        Me.gv.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv.ShowGroupPanel = False
        Me.gv.ShowHeaderCellButtons = True
        Me.gv.Size = New System.Drawing.Size(1016, 50)
        Me.gv.TabIndex = 0
        Me.gv.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.txtRALNo)
        Me.Panel1.Controls.Add(Me.MyLabel9)
        Me.Panel1.Controls.Add(Me.MyLabel5)
        Me.Panel1.Controls.Add(Me.fnd_PendingMRN)
        Me.Panel1.Controls.Add(Me.MyLabel4)
        Me.Panel1.Controls.Add(Me.txtAccept)
        Me.Panel1.Controls.Add(Me.UsLock1)
        Me.Panel1.Controls.Add(Me.lblLocation)
        Me.Panel1.Controls.Add(Me.RadLabel21)
        Me.Panel1.Controls.Add(Me.btnNew)
        Me.Panel1.Controls.Add(Me.RadLabel20)
        Me.Panel1.Controls.Add(Me.txtDocNo)
        Me.Panel1.Controls.Add(Me.txtGENo)
        Me.Panel1.Controls.Add(Me.txtDesc)
        Me.Panel1.Controls.Add(Me.txtGEDate)
        Me.Panel1.Controls.Add(Me.MyLabel1)
        Me.Panel1.Controls.Add(Me.MyLabel7)
        Me.Panel1.Controls.Add(Me.MyLabel2)
        Me.Panel1.Controls.Add(Me.cboSRNType)
        Me.Panel1.Controls.Add(Me.dtpDate)
        Me.Panel1.Controls.Add(Me.RadLabel29)
        Me.Panel1.Controls.Add(Me.fndVendor_code)
        Me.Panel1.Controls.Add(Me.cboItemType)
        Me.Panel1.Controls.Add(Me.MyLabel3)
        Me.Panel1.Controls.Add(Me.lblBillToLocation)
        Me.Panel1.Controls.Add(Me.TxtVendor_desc)
        Me.Panel1.Controls.Add(Me.RadLabel15)
        Me.Panel1.Controls.Add(Me.txtBillToLocation)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1038, 171)
        Me.Panel1.TabIndex = 71
        '
        'txtRALNo
        '
        Me.txtRALNo.CalculationExpression = Nothing
        Me.txtRALNo.FieldCode = Nothing
        Me.txtRALNo.FieldDesc = Nothing
        Me.txtRALNo.FieldMaxLength = 0
        Me.txtRALNo.FieldName = Nothing
        Me.txtRALNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRALNo.isCalculatedField = False
        Me.txtRALNo.IsSourceFromTable = False
        Me.txtRALNo.IsSourceFromValueList = False
        Me.txtRALNo.IsUnique = False
        Me.txtRALNo.Location = New System.Drawing.Point(561, 77)
        Me.txtRALNo.MaxLength = 50
        Me.txtRALNo.MendatroryField = False
        Me.txtRALNo.MyLinkLable1 = Me.RadLabel21
        Me.txtRALNo.MyLinkLable2 = Nothing
        Me.txtRALNo.Name = "txtRALNo"
        Me.txtRALNo.ReferenceFieldDesc = Nothing
        Me.txtRALNo.ReferenceFieldName = Nothing
        Me.txtRALNo.ReferenceTableName = Nothing
        Me.txtRALNo.Size = New System.Drawing.Size(181, 18)
        Me.txtRALNo.TabIndex = 78
        '
        'RadLabel21
        '
        Me.RadLabel21.FieldName = Nothing
        Me.RadLabel21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel21.Location = New System.Drawing.Point(509, 97)
        Me.RadLabel21.Name = "RadLabel21"
        Me.RadLabel21.Size = New System.Drawing.Size(78, 16)
        Me.RadLabel21.TabIndex = 69
        Me.RadLabel21.Text = "Gate Entry No"
        Me.RadLabel21.Visible = False
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(509, 76)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(46, 16)
        Me.MyLabel9.TabIndex = 77
        Me.MyLabel9.Text = "RAL No"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(12, 54)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(84, 16)
        Me.MyLabel5.TabIndex = 76
        Me.MyLabel5.Text = "Pending for QC"
        '
        'fnd_PendingMRN
        '
        Me.fnd_PendingMRN.CalculationExpression = Nothing
        Me.fnd_PendingMRN.FieldCode = Nothing
        Me.fnd_PendingMRN.FieldDesc = Nothing
        Me.fnd_PendingMRN.FieldMaxLength = 0
        Me.fnd_PendingMRN.FieldName = Nothing
        Me.fnd_PendingMRN.isCalculatedField = False
        Me.fnd_PendingMRN.IsSourceFromTable = False
        Me.fnd_PendingMRN.IsSourceFromValueList = False
        Me.fnd_PendingMRN.IsUnique = False
        Me.fnd_PendingMRN.Location = New System.Drawing.Point(101, 51)
        Me.fnd_PendingMRN.MendatroryField = True
        Me.fnd_PendingMRN.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fnd_PendingMRN.MyLinkLable1 = Me.MyLabel3
        Me.fnd_PendingMRN.MyLinkLable2 = Me.TxtVendor_desc
        Me.fnd_PendingMRN.MyReadOnly = False
        Me.fnd_PendingMRN.MyShowMasterFormButton = False
        Me.fnd_PendingMRN.Name = "fnd_PendingMRN"
        Me.fnd_PendingMRN.ReferenceFieldDesc = Nothing
        Me.fnd_PendingMRN.ReferenceFieldName = Nothing
        Me.fnd_PendingMRN.ReferenceTableName = Nothing
        Me.fnd_PendingMRN.Size = New System.Drawing.Size(272, 19)
        Me.fnd_PendingMRN.TabIndex = 75
        Me.fnd_PendingMRN.Value = ""
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(12, 99)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(43, 16)
        Me.MyLabel3.TabIndex = 50
        Me.MyLabel3.Text = "Vendor"
        '
        'TxtVendor_desc
        '
        Me.TxtVendor_desc.AutoSize = False
        Me.TxtVendor_desc.BorderVisible = True
        Me.TxtVendor_desc.FieldName = Nothing
        Me.TxtVendor_desc.Location = New System.Drawing.Point(245, 98)
        Me.TxtVendor_desc.Name = "TxtVendor_desc"
        Me.TxtVendor_desc.Size = New System.Drawing.Size(243, 19)
        Me.TxtVendor_desc.TabIndex = 49
        '
        'MyLabel4
        '
        Me.MyLabel4.AutoSize = False
        Me.MyLabel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.MyLabel4.Location = New System.Drawing.Point(0, 0)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(1038, 19)
        Me.MyLabel4.TabIndex = 74
        Me.MyLabel4.Text = "QUALITY CHECK"
        Me.MyLabel4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtAccept
        '
        Me.txtAccept.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtAccept.AutoSize = True
        Me.txtAccept.BackColor = System.Drawing.Color.LightGreen
        Me.txtAccept.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAccept.Location = New System.Drawing.Point(925, 44)
        Me.txtAccept.MaximumSize = New System.Drawing.Size(107, 20)
        Me.txtAccept.MinimumSize = New System.Drawing.Size(107, 20)
        Me.txtAccept.Name = "txtAccept"
        Me.txtAccept.Size = New System.Drawing.Size(107, 20)
        Me.txtAccept.TabIndex = 73
        Me.txtAccept.Text = "Accepted"
        Me.txtAccept.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'UsLock1
        '
        Me.UsLock1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(925, 21)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(107, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 71
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(12, 30)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(79, 16)
        Me.lblLocation.TabIndex = 11
        Me.lblLocation.Text = "Document No."
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnNew.Location = New System.Drawing.Point(354, 27)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(20, 21)
        Me.btnNew.TabIndex = 0
        Me.btnNew.Text = " "
        '
        'RadLabel20
        '
        Me.RadLabel20.FieldName = Nothing
        Me.RadLabel20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel20.Location = New System.Drawing.Point(509, 119)
        Me.RadLabel20.Name = "RadLabel20"
        Me.RadLabel20.Size = New System.Drawing.Size(91, 16)
        Me.RadLabel20.TabIndex = 70
        Me.RadLabel20.Text = "Gate Entry  Date"
        Me.RadLabel20.Visible = False
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(101, 27)
        Me.txtDocNo.MendatroryField = True
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Me.lblLocation
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 100
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(253, 21)
        Me.txtDocNo.TabIndex = 12
        Me.txtDocNo.Value = ""
        '
        'txtGENo
        '
        Me.txtGENo.CalculationExpression = Nothing
        Me.txtGENo.FieldCode = Nothing
        Me.txtGENo.FieldDesc = Nothing
        Me.txtGENo.FieldMaxLength = 0
        Me.txtGENo.FieldName = Nothing
        Me.txtGENo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGENo.isCalculatedField = False
        Me.txtGENo.IsSourceFromTable = False
        Me.txtGENo.IsSourceFromValueList = False
        Me.txtGENo.IsUnique = False
        Me.txtGENo.Location = New System.Drawing.Point(607, 95)
        Me.txtGENo.MaxLength = 50
        Me.txtGENo.MendatroryField = False
        Me.txtGENo.MyLinkLable1 = Me.RadLabel21
        Me.txtGENo.MyLinkLable2 = Nothing
        Me.txtGENo.Name = "txtGENo"
        Me.txtGENo.ReferenceFieldDesc = Nothing
        Me.txtGENo.ReferenceFieldName = Nothing
        Me.txtGENo.ReferenceTableName = Nothing
        Me.txtGENo.Size = New System.Drawing.Size(181, 18)
        Me.txtGENo.TabIndex = 5
        Me.txtGENo.Visible = False
        '
        'txtDesc
        '
        Me.txtDesc.CalculationExpression = Nothing
        Me.txtDesc.FieldCode = Nothing
        Me.txtDesc.FieldDesc = Nothing
        Me.txtDesc.FieldMaxLength = 0
        Me.txtDesc.FieldName = Nothing
        Me.txtDesc.isCalculatedField = False
        Me.txtDesc.IsSourceFromTable = False
        Me.txtDesc.IsSourceFromValueList = False
        Me.txtDesc.IsUnique = False
        Me.txtDesc.Location = New System.Drawing.Point(101, 75)
        Me.txtDesc.MaxLength = 200
        Me.txtDesc.MendatroryField = False
        Me.txtDesc.MyLinkLable1 = Me.MyLabel1
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ReferenceFieldDesc = Nothing
        Me.txtDesc.ReferenceFieldName = Nothing
        Me.txtDesc.ReferenceTableName = Nothing
        Me.txtDesc.Size = New System.Drawing.Size(387, 20)
        Me.txtDesc.TabIndex = 2
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(12, 76)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel1.TabIndex = 13
        Me.MyLabel1.Text = "Description"
        '
        'txtGEDate
        '
        Me.txtGEDate.CalculationExpression = Nothing
        Me.txtGEDate.CustomFormat = "dd/MM/yyyy"
        Me.txtGEDate.FieldCode = Nothing
        Me.txtGEDate.FieldDesc = Nothing
        Me.txtGEDate.FieldMaxLength = 0
        Me.txtGEDate.FieldName = Nothing
        Me.txtGEDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGEDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtGEDate.isCalculatedField = False
        Me.txtGEDate.IsSourceFromTable = False
        Me.txtGEDate.IsSourceFromValueList = False
        Me.txtGEDate.IsUnique = False
        Me.txtGEDate.Location = New System.Drawing.Point(607, 118)
        Me.txtGEDate.MendatroryField = False
        Me.txtGEDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtGEDate.MyLinkLable1 = Me.RadLabel20
        Me.txtGEDate.MyLinkLable2 = Nothing
        Me.txtGEDate.Name = "txtGEDate"
        Me.txtGEDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtGEDate.ReferenceFieldDesc = Nothing
        Me.txtGEDate.ReferenceFieldName = Nothing
        Me.txtGEDate.ReferenceTableName = Nothing
        Me.txtGEDate.ShowCheckBox = True
        Me.txtGEDate.Size = New System.Drawing.Size(181, 18)
        Me.txtGEDate.TabIndex = 6
        Me.txtGEDate.TabStop = False
        Me.txtGEDate.Text = "13/06/2011"
        Me.txtGEDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        Me.txtGEDate.Visible = False
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(12, 143)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(58, 16)
        Me.MyLabel7.TabIndex = 66
        Me.MyLabel7.Text = "SRN Type"
        Me.MyLabel7.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(373, 30)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(30, 16)
        Me.MyLabel2.TabIndex = 14
        Me.MyLabel2.Text = "Date"
        '
        'cboSRNType
        '
        Me.cboSRNType.AutoCompleteDisplayMember = Nothing
        Me.cboSRNType.AutoCompleteValueMember = Nothing
        Me.cboSRNType.CalculationExpression = Nothing
        Me.cboSRNType.DropDownAnimationEnabled = True
        Me.cboSRNType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboSRNType.FieldCode = Nothing
        Me.cboSRNType.FieldDesc = Nothing
        Me.cboSRNType.FieldMaxLength = 0
        Me.cboSRNType.FieldName = Nothing
        Me.cboSRNType.isCalculatedField = False
        Me.cboSRNType.IsSourceFromTable = False
        Me.cboSRNType.IsSourceFromValueList = False
        Me.cboSRNType.IsUnique = False
        Me.cboSRNType.Location = New System.Drawing.Point(101, 141)
        Me.cboSRNType.MendatroryField = True
        Me.cboSRNType.MyLinkLable1 = Me.MyLabel7
        Me.cboSRNType.MyLinkLable2 = Nothing
        Me.cboSRNType.Name = "cboSRNType"
        Me.cboSRNType.ReferenceFieldDesc = Nothing
        Me.cboSRNType.ReferenceFieldName = Nothing
        Me.cboSRNType.ReferenceTableName = Nothing
        Me.cboSRNType.Size = New System.Drawing.Size(143, 20)
        Me.cboSRNType.TabIndex = 7
        '
        'dtpDate
        '
        Me.dtpDate.CalculationExpression = Nothing
        Me.dtpDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpDate.FieldCode = Nothing
        Me.dtpDate.FieldDesc = Nothing
        Me.dtpDate.FieldMaxLength = 0
        Me.dtpDate.FieldName = Nothing
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDate.isCalculatedField = False
        Me.dtpDate.IsSourceFromTable = False
        Me.dtpDate.IsSourceFromValueList = False
        Me.dtpDate.IsUnique = False
        Me.dtpDate.Location = New System.Drawing.Point(406, 28)
        Me.dtpDate.MendatroryField = False
        Me.dtpDate.MyLinkLable1 = Me.MyLabel2
        Me.dtpDate.MyLinkLable2 = Nothing
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.ReferenceFieldDesc = Nothing
        Me.dtpDate.ReferenceFieldName = Nothing
        Me.dtpDate.ReferenceTableName = Nothing
        Me.dtpDate.Size = New System.Drawing.Size(82, 20)
        Me.dtpDate.TabIndex = 1
        Me.dtpDate.TabStop = False
        Me.dtpDate.Text = "24/02/2015"
        Me.dtpDate.Value = New Date(2015, 2, 24, 17, 7, 15, 425)
        '
        'RadLabel29
        '
        Me.RadLabel29.FieldName = Nothing
        Me.RadLabel29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel29.Location = New System.Drawing.Point(245, 142)
        Me.RadLabel29.Name = "RadLabel29"
        Me.RadLabel29.Size = New System.Drawing.Size(57, 16)
        Me.RadLabel29.TabIndex = 65
        Me.RadLabel29.Text = "Item Type"
        '
        'fndVendor_code
        '
        Me.fndVendor_code.CalculationExpression = Nothing
        Me.fndVendor_code.FieldCode = Nothing
        Me.fndVendor_code.FieldDesc = Nothing
        Me.fndVendor_code.FieldMaxLength = 0
        Me.fndVendor_code.FieldName = Nothing
        Me.fndVendor_code.isCalculatedField = False
        Me.fndVendor_code.IsSourceFromTable = False
        Me.fndVendor_code.IsSourceFromValueList = False
        Me.fndVendor_code.IsUnique = False
        Me.fndVendor_code.Location = New System.Drawing.Point(101, 98)
        Me.fndVendor_code.MendatroryField = True
        Me.fndVendor_code.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndVendor_code.MyLinkLable1 = Me.MyLabel3
        Me.fndVendor_code.MyLinkLable2 = Me.TxtVendor_desc
        Me.fndVendor_code.MyReadOnly = False
        Me.fndVendor_code.MyShowMasterFormButton = False
        Me.fndVendor_code.Name = "fndVendor_code"
        Me.fndVendor_code.ReferenceFieldDesc = Nothing
        Me.fndVendor_code.ReferenceFieldName = Nothing
        Me.fndVendor_code.ReferenceTableName = Nothing
        Me.fndVendor_code.Size = New System.Drawing.Size(142, 19)
        Me.fndVendor_code.TabIndex = 3
        Me.fndVendor_code.Value = ""
        '
        'cboItemType
        '
        Me.cboItemType.AutoCompleteDisplayMember = Nothing
        Me.cboItemType.AutoCompleteValueMember = Nothing
        Me.cboItemType.CalculationExpression = Nothing
        Me.cboItemType.DropDownAnimationEnabled = True
        Me.cboItemType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboItemType.FieldCode = Nothing
        Me.cboItemType.FieldDesc = Nothing
        Me.cboItemType.FieldMaxLength = 0
        Me.cboItemType.FieldName = Nothing
        Me.cboItemType.isCalculatedField = False
        Me.cboItemType.IsSourceFromTable = False
        Me.cboItemType.IsSourceFromValueList = False
        Me.cboItemType.IsUnique = False
        Me.cboItemType.Location = New System.Drawing.Point(342, 141)
        Me.cboItemType.MendatroryField = True
        Me.cboItemType.MyLinkLable1 = Me.RadLabel29
        Me.cboItemType.MyLinkLable2 = Nothing
        Me.cboItemType.Name = "cboItemType"
        Me.cboItemType.ReferenceFieldDesc = Nothing
        Me.cboItemType.ReferenceFieldName = Nothing
        Me.cboItemType.ReferenceTableName = Nothing
        Me.cboItemType.Size = New System.Drawing.Size(146, 20)
        Me.cboItemType.TabIndex = 8
        '
        'lblBillToLocation
        '
        Me.lblBillToLocation.AutoSize = False
        Me.lblBillToLocation.BorderVisible = True
        Me.lblBillToLocation.FieldName = Nothing
        Me.lblBillToLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBillToLocation.Location = New System.Drawing.Point(245, 120)
        Me.lblBillToLocation.Name = "lblBillToLocation"
        Me.lblBillToLocation.Size = New System.Drawing.Size(243, 18)
        Me.lblBillToLocation.TabIndex = 58
        Me.lblBillToLocation.TextWrap = False
        '
        'RadLabel15
        '
        Me.RadLabel15.FieldName = Nothing
        Me.RadLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel15.Location = New System.Drawing.Point(12, 121)
        Me.RadLabel15.Name = "RadLabel15"
        Me.RadLabel15.Size = New System.Drawing.Size(79, 16)
        Me.RadLabel15.TabIndex = 57
        Me.RadLabel15.Text = "From Location"
        '
        'txtBillToLocation
        '
        Me.txtBillToLocation.CalculationExpression = Nothing
        Me.txtBillToLocation.FieldCode = Nothing
        Me.txtBillToLocation.FieldDesc = Nothing
        Me.txtBillToLocation.FieldMaxLength = 0
        Me.txtBillToLocation.FieldName = Nothing
        Me.txtBillToLocation.isCalculatedField = False
        Me.txtBillToLocation.IsSourceFromTable = False
        Me.txtBillToLocation.IsSourceFromValueList = False
        Me.txtBillToLocation.IsUnique = False
        Me.txtBillToLocation.Location = New System.Drawing.Point(101, 120)
        Me.txtBillToLocation.MendatroryField = True
        Me.txtBillToLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillToLocation.MyLinkLable1 = Me.RadLabel15
        Me.txtBillToLocation.MyLinkLable2 = Me.lblBillToLocation
        Me.txtBillToLocation.MyReadOnly = False
        Me.txtBillToLocation.MyShowMasterFormButton = False
        Me.txtBillToLocation.Name = "txtBillToLocation"
        Me.txtBillToLocation.ReferenceFieldDesc = Nothing
        Me.txtBillToLocation.ReferenceFieldName = Nothing
        Me.txtBillToLocation.ReferenceTableName = Nothing
        Me.txtBillToLocation.Size = New System.Drawing.Size(142, 18)
        Me.txtBillToLocation.TabIndex = 4
        Me.txtBillToLocation.Value = ""
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.UcAttachment1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(75.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1038, 410)
        Me.RadPageViewPage2.Text = "Attachment"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(1038, 410)
        Me.UcAttachment1.TabIndex = 1
        Me.UcAttachment1.TabStop = False
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel11)
        Me.RadPageViewPage3.Controls.Add(Me.lblRalPrint)
        Me.RadPageViewPage3.Controls.Add(Me.TxtFinderRalPrint)
        Me.RadPageViewPage3.Controls.Add(Me.lblItemPrint)
        Me.RadPageViewPage3.Controls.Add(Me.TxtFinderItemPrint)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel8)
        Me.RadPageViewPage3.Controls.Add(Me.TxtFinderVendorPrint)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel6)
        Me.RadPageViewPage3.Controls.Add(Me.lblVendorPrint)
        Me.RadPageViewPage3.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(107.0!, 28.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(1038, 410)
        Me.RadPageViewPage3.Text = "Multiple Doc Print"
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel11.Location = New System.Drawing.Point(8, 112)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(49, 16)
        Me.MyLabel11.TabIndex = 63
        Me.MyLabel11.Text = "RAL No."
        '
        'lblRalPrint
        '
        Me.lblRalPrint.AutoSize = False
        Me.lblRalPrint.BorderVisible = True
        Me.lblRalPrint.Enabled = False
        Me.lblRalPrint.FieldName = Nothing
        Me.lblRalPrint.Location = New System.Drawing.Point(213, 109)
        Me.lblRalPrint.Name = "lblRalPrint"
        Me.lblRalPrint.Size = New System.Drawing.Size(243, 19)
        Me.lblRalPrint.TabIndex = 62
        '
        'TxtFinderRalPrint
        '
        Me.TxtFinderRalPrint.CalculationExpression = Nothing
        Me.TxtFinderRalPrint.FieldCode = Nothing
        Me.TxtFinderRalPrint.FieldDesc = Nothing
        Me.TxtFinderRalPrint.FieldMaxLength = 0
        Me.TxtFinderRalPrint.FieldName = Nothing
        Me.TxtFinderRalPrint.isCalculatedField = False
        Me.TxtFinderRalPrint.IsSourceFromTable = False
        Me.TxtFinderRalPrint.IsSourceFromValueList = False
        Me.TxtFinderRalPrint.IsUnique = False
        Me.TxtFinderRalPrint.Location = New System.Drawing.Point(65, 109)
        Me.TxtFinderRalPrint.MendatroryField = True
        Me.TxtFinderRalPrint.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFinderRalPrint.MyLinkLable1 = Me.MyLabel6
        Me.TxtFinderRalPrint.MyLinkLable2 = Me.lblVendorPrint
        Me.TxtFinderRalPrint.MyReadOnly = False
        Me.TxtFinderRalPrint.MyShowMasterFormButton = False
        Me.TxtFinderRalPrint.Name = "TxtFinderRalPrint"
        Me.TxtFinderRalPrint.ReferenceFieldDesc = Nothing
        Me.TxtFinderRalPrint.ReferenceFieldName = Nothing
        Me.TxtFinderRalPrint.ReferenceTableName = Nothing
        Me.TxtFinderRalPrint.Size = New System.Drawing.Size(142, 19)
        Me.TxtFinderRalPrint.TabIndex = 61
        Me.TxtFinderRalPrint.Value = ""
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(8, 60)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(43, 16)
        Me.MyLabel6.TabIndex = 57
        Me.MyLabel6.Text = "Vendor"
        '
        'lblVendorPrint
        '
        Me.lblVendorPrint.AutoSize = False
        Me.lblVendorPrint.BorderVisible = True
        Me.lblVendorPrint.Enabled = False
        Me.lblVendorPrint.FieldName = Nothing
        Me.lblVendorPrint.Location = New System.Drawing.Point(213, 59)
        Me.lblVendorPrint.Name = "lblVendorPrint"
        Me.lblVendorPrint.Size = New System.Drawing.Size(243, 19)
        Me.lblVendorPrint.TabIndex = 56
        '
        'lblItemPrint
        '
        Me.lblItemPrint.AutoSize = False
        Me.lblItemPrint.BorderVisible = True
        Me.lblItemPrint.Enabled = False
        Me.lblItemPrint.FieldName = Nothing
        Me.lblItemPrint.Location = New System.Drawing.Point(213, 84)
        Me.lblItemPrint.Name = "lblItemPrint"
        Me.lblItemPrint.Size = New System.Drawing.Size(243, 19)
        Me.lblItemPrint.TabIndex = 60
        '
        'TxtFinderItemPrint
        '
        Me.TxtFinderItemPrint.CalculationExpression = Nothing
        Me.TxtFinderItemPrint.FieldCode = Nothing
        Me.TxtFinderItemPrint.FieldDesc = Nothing
        Me.TxtFinderItemPrint.FieldMaxLength = 0
        Me.TxtFinderItemPrint.FieldName = Nothing
        Me.TxtFinderItemPrint.isCalculatedField = False
        Me.TxtFinderItemPrint.IsSourceFromTable = False
        Me.TxtFinderItemPrint.IsSourceFromValueList = False
        Me.TxtFinderItemPrint.IsUnique = False
        Me.TxtFinderItemPrint.Location = New System.Drawing.Point(65, 84)
        Me.TxtFinderItemPrint.MendatroryField = True
        Me.TxtFinderItemPrint.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFinderItemPrint.MyLinkLable1 = Me.MyLabel6
        Me.TxtFinderItemPrint.MyLinkLable2 = Me.lblVendorPrint
        Me.TxtFinderItemPrint.MyReadOnly = False
        Me.TxtFinderItemPrint.MyShowMasterFormButton = False
        Me.TxtFinderItemPrint.Name = "TxtFinderItemPrint"
        Me.TxtFinderItemPrint.ReferenceFieldDesc = Nothing
        Me.TxtFinderItemPrint.ReferenceFieldName = Nothing
        Me.TxtFinderItemPrint.ReferenceTableName = Nothing
        Me.TxtFinderItemPrint.Size = New System.Drawing.Size(142, 19)
        Me.TxtFinderItemPrint.TabIndex = 59
        Me.TxtFinderItemPrint.Value = ""
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(8, 84)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(28, 16)
        Me.MyLabel8.TabIndex = 58
        Me.MyLabel8.Text = "Item"
        '
        'TxtFinderVendorPrint
        '
        Me.TxtFinderVendorPrint.CalculationExpression = Nothing
        Me.TxtFinderVendorPrint.FieldCode = Nothing
        Me.TxtFinderVendorPrint.FieldDesc = Nothing
        Me.TxtFinderVendorPrint.FieldMaxLength = 0
        Me.TxtFinderVendorPrint.FieldName = Nothing
        Me.TxtFinderVendorPrint.isCalculatedField = False
        Me.TxtFinderVendorPrint.IsSourceFromTable = False
        Me.TxtFinderVendorPrint.IsSourceFromValueList = False
        Me.TxtFinderVendorPrint.IsUnique = False
        Me.TxtFinderVendorPrint.Location = New System.Drawing.Point(65, 59)
        Me.TxtFinderVendorPrint.MendatroryField = True
        Me.TxtFinderVendorPrint.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFinderVendorPrint.MyLinkLable1 = Me.MyLabel6
        Me.TxtFinderVendorPrint.MyLinkLable2 = Me.lblVendorPrint
        Me.TxtFinderVendorPrint.MyReadOnly = False
        Me.TxtFinderVendorPrint.MyShowMasterFormButton = False
        Me.TxtFinderVendorPrint.Name = "TxtFinderVendorPrint"
        Me.TxtFinderVendorPrint.ReferenceFieldDesc = Nothing
        Me.TxtFinderVendorPrint.ReferenceFieldName = Nothing
        Me.TxtFinderVendorPrint.ReferenceTableName = Nothing
        Me.TxtFinderVendorPrint.Size = New System.Drawing.Size(142, 19)
        Me.TxtFinderVendorPrint.TabIndex = 55
        Me.TxtFinderVendorPrint.Value = ""
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox3.Controls.Add(Me.ToDate)
        Me.RadGroupBox3.Controls.Add(Me.fromDate)
        Me.RadGroupBox3.HeaderText = "Date Range"
        Me.RadGroupBox3.Location = New System.Drawing.Point(8, 11)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(310, 42)
        Me.RadGroupBox3.TabIndex = 54
        Me.RadGroupBox3.Text = "Date Range"
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Location = New System.Drawing.Point(130, 16)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(19, 18)
        Me.RadLabel2.TabIndex = 3
        Me.RadLabel2.Text = "To"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(5, 16)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(32, 18)
        Me.RadLabel1.TabIndex = 2
        Me.RadLabel1.Text = "From"
        '
        'ToDate
        '
        Me.ToDate.CustomFormat = "dd/MM/yyyy"
        Me.ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.ToDate.Location = New System.Drawing.Point(157, 15)
        Me.ToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.Name = "ToDate"
        Me.ToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.Size = New System.Drawing.Size(78, 20)
        Me.ToDate.TabIndex = 1
        Me.ToDate.TabStop = False
        Me.ToDate.Text = "24/10/2011"
        Me.ToDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'fromDate
        '
        Me.fromDate.CustomFormat = "dd/MM/yyyy"
        Me.fromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.fromDate.Location = New System.Drawing.Point(44, 15)
        Me.fromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Name = "fromDate"
        Me.fromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Size = New System.Drawing.Size(78, 20)
        Me.fromDate.TabIndex = 0
        Me.fromDate.TabStop = False
        Me.fromDate.Text = "24/10/2011"
        Me.fromDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'btnRALWiseAnaysisPrint
        '
        Me.btnRALWiseAnaysisPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRALWiseAnaysisPrint.Location = New System.Drawing.Point(642, 5)
        Me.btnRALWiseAnaysisPrint.Name = "btnRALWiseAnaysisPrint"
        Me.btnRALWiseAnaysisPrint.Size = New System.Drawing.Size(120, 22)
        Me.btnRALWiseAnaysisPrint.TabIndex = 64
        Me.btnRALWiseAnaysisPrint.Text = "RAL Wise Anaysis Print"
        '
        'btnRejected
        '
        Me.btnRejected.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRejected.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiEnglish, Me.rmiHindi})
        Me.btnRejected.Location = New System.Drawing.Point(764, 5)
        Me.btnRejected.Name = "btnRejected"
        Me.btnRejected.Size = New System.Drawing.Size(83, 22)
        Me.btnRejected.TabIndex = 158
        Me.btnRejected.Text = "Rejected Analysis Print"
        '
        'rmiEnglish
        '
        Me.rmiEnglish.Name = "rmiEnglish"
        Me.rmiEnglish.Text = "English"
        Me.rmiEnglish.UseCompatibleTextRendering = False
        '
        'rmiHindi
        '
        Me.rmiHindi.Name = "rmiHindi"
        Me.rmiHindi.Text = "Hindi"
        Me.rmiHindi.UseCompatibleTextRendering = False
        '
        'btnAnalysisPrintVertical
        '
        Me.btnAnalysisPrintVertical.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAnalysisPrintVertical.Location = New System.Drawing.Point(524, 5)
        Me.btnAnalysisPrintVertical.Name = "btnAnalysisPrintVertical"
        Me.btnAnalysisPrintVertical.Size = New System.Drawing.Size(116, 22)
        Me.btnAnalysisPrintVertical.TabIndex = 9
        Me.btnAnalysisPrintVertical.Text = "Analysis Print Vertical"
        '
        'btnAnalysisPrint
        '
        Me.btnAnalysisPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAnalysisPrint.Location = New System.Drawing.Point(439, 5)
        Me.btnAnalysisPrint.Name = "btnAnalysisPrint"
        Me.btnAnalysisPrint.Size = New System.Drawing.Size(83, 22)
        Me.btnAnalysisPrint.TabIndex = 8
        Me.btnAnalysisPrint.Text = "Analysis Print"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(398, 5)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(39, 22)
        Me.btnPrint.TabIndex = 7
        Me.btnPrint.Text = "Print"
        '
        'btnSendEmail
        '
        Me.btnSendEmail.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSendEmail.Location = New System.Drawing.Point(316, 5)
        Me.btnSendEmail.Name = "btnSendEmail"
        Me.btnSendEmail.Size = New System.Drawing.Size(80, 22)
        Me.btnSendEmail.TabIndex = 6
        Me.btnSendEmail.Text = "Send E-Mail"
        '
        'btnreverse
        '
        Me.btnreverse.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreverse.Location = New System.Drawing.Point(849, 5)
        Me.btnreverse.Name = "btnreverse"
        Me.btnreverse.Size = New System.Drawing.Size(53, 22)
        Me.btnreverse.TabIndex = 5
        Me.btnreverse.Text = "Reverse"
        Me.btnreverse.Visible = False
        '
        'btnTemplates
        '
        Me.btnTemplates.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnTemplates.Location = New System.Drawing.Point(234, 4)
        Me.btnTemplates.Name = "btnTemplates"
        Me.btnTemplates.Size = New System.Drawing.Size(80, 22)
        Me.btnTemplates.TabIndex = 4
        Me.btnTemplates.Text = "Fill Templates"
        '
        'btnpost
        '
        Me.btnpost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnpost.Location = New System.Drawing.Point(157, 4)
        Me.btnpost.Name = "btnpost"
        Me.btnpost.Size = New System.Drawing.Size(75, 22)
        Me.btnpost.TabIndex = 2
        Me.btnpost.Text = "Post"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(1012, 4)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(46, 22)
        Me.btnclose.TabIndex = 3
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(80, 4)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(75, 22)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(3, 4)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(75, 22)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'FrmQualityCheckForSRN
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1061, 493)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmQualityCheckForSRN"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmQualityCheckForSRN"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv_MRN.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_MRN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.txtRALNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtVendor_desc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGENo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGEDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboSRNType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel29, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboItemType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBillToLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.RadPageViewPage3.ResumeLayout(False)
        Me.RadPageViewPage3.PerformLayout()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRalPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendorPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblItemPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRALWiseAnaysisPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRejected, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAnalysisPrintVertical, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAnalysisPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSendEmail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnTemplates, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnpost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnpost As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtDesc As common.Controls.MyTextBox
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents dtpDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents TxtVendor_desc As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents fndVendor_code As common.UserControls.txtFinder
    Friend WithEvents lblBillToLocation As common.Controls.MyLabel
    Friend WithEvents RadLabel15 As common.Controls.MyLabel
    Friend WithEvents txtBillToLocation As common.UserControls.txtFinder
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents cboSRNType As common.Controls.MyComboBox
    Friend WithEvents RadLabel29 As common.Controls.MyLabel
    Friend WithEvents cboItemType As common.Controls.MyComboBox
    Friend WithEvents RadLabel21 As common.Controls.MyLabel
    Friend WithEvents RadLabel20 As common.Controls.MyLabel
    Friend WithEvents txtGENo As common.Controls.MyTextBox
    Friend WithEvents txtGEDate As common.Controls.MyDateTimePicker
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv_MRN As common.UserControls.MyRadGridView
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents txtAccept As System.Windows.Forms.Label
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents btnTemplates As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnreverse As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSendEmail As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents fnd_PendingMRN As common.UserControls.txtFinder
    Friend WithEvents btnAnalysisPrint As RadButton
    Friend WithEvents RadPageViewPage3 As RadPageViewPage
    Friend WithEvents RadGroupBox3 As RadGroupBox
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents ToDate As RadDateTimePicker
    Friend WithEvents fromDate As RadDateTimePicker
    Friend WithEvents TxtFinderVendorPrint As common.UserControls.txtFinder
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents lblVendorPrint As common.Controls.MyLabel
    Friend WithEvents TxtFinderItemPrint As common.UserControls.txtFinder
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents lblItemPrint As common.Controls.MyLabel
    Friend WithEvents btnAnalysisPrintVertical As RadButton
    Friend WithEvents txtRALNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents btnRejected As RadSplitButton
    Friend WithEvents rmiEnglish As RadMenuItem
    Friend WithEvents rmiHindi As RadMenuItem
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents lblRalPrint As common.Controls.MyLabel
    Friend WithEvents TxtFinderRalPrint As common.UserControls.txtFinder
    Friend WithEvents btnRALWiseAnaysisPrint As RadButton
End Class

