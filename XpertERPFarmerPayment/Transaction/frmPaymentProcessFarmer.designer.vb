<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPaymentProcessFarmer
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
        Dim TableViewDefinition3 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition4 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition5 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition6 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition7 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition8 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition9 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition10 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition11 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition12 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition13 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition14 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.chkSelected = New common.Controls.MyCheckBox()
        Me.txtVSP = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtNEFTUploaderREFNo = New common.Controls.MyTextBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.chkPrevMPAdj = New common.Controls.MyCheckBox()
        Me.chkSkipPreviousDocumentOfAdvancePayment = New common.Controls.MyCheckBox()
        Me.chkSkipPrevItemIssueReturn = New common.Controls.MyCheckBox()
        Me.ChkSkipMccSaleReturn = New common.Controls.MyCheckBox()
        Me.chkSkipPrevCreditNote = New common.Controls.MyCheckBox()
        Me.chkSkipPrevDeduction = New common.Controls.MyCheckBox()
        Me.chkSkipPrevItemIssue = New common.Controls.MyCheckBox()
        Me.chkSkipPrevMccSale = New common.Controls.MyCheckBox()
        Me.btnUnselectAll = New Telerik.WinControls.UI.RadButton()
        Me.btnSelectAll = New Telerik.WinControls.UI.RadButton()
        Me.txtLocName = New common.Controls.MyTextBox()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.fndLoc = New common.UserControls.txtFinder()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.dtpDate = New common.Controls.MyDateTimePicker()
        Me.lblDocDate = New common.Controls.MyLabel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.dtpToDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.dtpFromDate = New common.Controls.MyDateTimePicker()
        Me.lblPending = New common.usLock()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.lblDocNo = New common.Controls.MyLabel()
        Me.fndDocNo = New common.UserControls.txtNavigator()
        Me.gv = New common.UserControls.MyRadGridView()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage8 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvPaymentToFarmer = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvInvoice = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvMccSale = New common.UserControls.MyRadGridView()
        Me.RDPMccSaleReturn = New Telerik.WinControls.UI.RadPageViewPage()
        Me.GvMccSaleReturn = New common.UserControls.MyRadGridView()
        Me.RDPMccSaleFarmer = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvMccSaleFarmer = New common.UserControls.MyRadGridView()
        Me.RDPMccSaleReturnFarmer = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvMccSaleReturnFarmer = New common.UserControls.MyRadGridView()
        Me.RDPFarmerdjustment = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvMPAdj = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage4 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvItemIssue = New common.UserControls.MyRadGridView()
        Me.PageItemIssueReturn = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvItemIssueReturn = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage5 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvDeduction = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage6 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvCreditNote = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage7 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvAdvancePayment = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage9 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvFA = New common.UserControls.MyRadGridView()
        Me.btnPreFormat2Print = New Telerik.WinControls.UI.RadButton()
        Me.btnExportToExcelPaymentToFarmer = New Telerik.WinControls.UI.RadButton()
        Me.btnPreFormatePrint = New Telerik.WinControls.UI.RadButton()
        Me.btnReverse = New Telerik.WinControls.UI.RadButton()
        Me.btnDocPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnExport = New Telerik.WinControls.UI.RadButton()
        Me.btnProcess = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadPageViewPage1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.chkSelected, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNEFTUploaderREFNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.chkPrevMPAdj, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSkipPreviousDocumentOfAdvancePayment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSkipPrevItemIssueReturn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkSkipMccSaleReturn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSkipPrevCreditNote, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSkipPrevDeduction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSkipPrevItemIssue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSkipPrevMccSale, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUnselectAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSelectAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLocName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage8.SuspendLayout()
        CType(Me.gvPaymentToFarmer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvPaymentToFarmer.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gvInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvInvoice.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage3.SuspendLayout()
        CType(Me.gvMccSale, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvMccSale.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RDPMccSaleReturn.SuspendLayout()
        CType(Me.GvMccSaleReturn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GvMccSaleReturn.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RDPMccSaleFarmer.SuspendLayout()
        CType(Me.gvMccSaleFarmer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvMccSaleFarmer.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RDPMccSaleReturnFarmer.SuspendLayout()
        CType(Me.gvMccSaleReturnFarmer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvMccSaleReturnFarmer.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RDPFarmerdjustment.SuspendLayout()
        CType(Me.gvMPAdj, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvMPAdj.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage4.SuspendLayout()
        CType(Me.gvItemIssue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvItemIssue.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PageItemIssueReturn.SuspendLayout()
        CType(Me.gvItemIssueReturn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvItemIssueReturn.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage5.SuspendLayout()
        CType(Me.gvDeduction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDeduction.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage6.SuspendLayout()
        CType(Me.gvCreditNote, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCreditNote.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage7.SuspendLayout()
        CType(Me.gvAdvancePayment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvAdvancePayment.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage9.SuspendLayout()
        CType(Me.gvFA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvFA.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPreFormat2Print, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExportToExcelPaymentToFarmer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPreFormatePrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDocPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnProcess, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer2)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(98.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1172, 423)
        Me.RadPageViewPage1.Text = "Payment To VSP"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkSelected)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtVSP)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtNEFTUploaderREFNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer2.Panel1.Controls.Add(Me.GroupBox2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnUnselectAll)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnSelectAll)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtLocName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblLocation)
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndLoc)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnGo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.dtpDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.GroupBox1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblPending)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnReset)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblDocNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblDocDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndDocNo)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gv)
        Me.SplitContainer2.Size = New System.Drawing.Size(1172, 423)
        Me.SplitContainer2.SplitterDistance = 133
        Me.SplitContainer2.TabIndex = 265
        '
        'chkSelected
        '
        Me.chkSelected.Location = New System.Drawing.Point(843, 34)
        Me.chkSelected.MyLinkLable1 = Nothing
        Me.chkSelected.MyLinkLable2 = Nothing
        Me.chkSelected.Name = "chkSelected"
        Me.chkSelected.Size = New System.Drawing.Size(62, 18)
        Me.chkSelected.TabIndex = 285
        Me.chkSelected.Tag1 = Nothing
        Me.chkSelected.Text = "Selected"
        Me.chkSelected.Visible = False
        '
        'txtVSP
        '
        Me.txtVSP.arrDispalyMember = Nothing
        Me.txtVSP.arrValueMember = Nothing
        Me.txtVSP.Location = New System.Drawing.Point(51, 88)
        Me.txtVSP.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVSP.MyLinkLable1 = Me.MyLabel4
        Me.txtVSP.MyLinkLable2 = Nothing
        Me.txtVSP.MyNullText = "Please Select..."
        Me.txtVSP.Name = "txtVSP"
        Me.txtVSP.Size = New System.Drawing.Size(225, 19)
        Me.txtVSP.TabIndex = 283
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(9, 90)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(26, 18)
        Me.MyLabel4.TabIndex = 284
        Me.MyLabel4.Text = "VSP"
        '
        'txtNEFTUploaderREFNo
        '
        Me.txtNEFTUploaderREFNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtNEFTUploaderREFNo.CalculationExpression = Nothing
        Me.txtNEFTUploaderREFNo.FieldCode = Nothing
        Me.txtNEFTUploaderREFNo.FieldDesc = Nothing
        Me.txtNEFTUploaderREFNo.FieldMaxLength = 0
        Me.txtNEFTUploaderREFNo.FieldName = Nothing
        Me.txtNEFTUploaderREFNo.isCalculatedField = False
        Me.txtNEFTUploaderREFNo.IsSourceFromTable = False
        Me.txtNEFTUploaderREFNo.IsSourceFromValueList = False
        Me.txtNEFTUploaderREFNo.IsUnique = False
        Me.txtNEFTUploaderREFNo.Location = New System.Drawing.Point(134, 110)
        Me.txtNEFTUploaderREFNo.MendatroryField = False
        Me.txtNEFTUploaderREFNo.MyLinkLable1 = Nothing
        Me.txtNEFTUploaderREFNo.MyLinkLable2 = Nothing
        Me.txtNEFTUploaderREFNo.Name = "txtNEFTUploaderREFNo"
        Me.txtNEFTUploaderREFNo.ReferenceFieldDesc = Nothing
        Me.txtNEFTUploaderREFNo.ReferenceFieldName = Nothing
        Me.txtNEFTUploaderREFNo.ReferenceTableName = Nothing
        Me.txtNEFTUploaderREFNo.Size = New System.Drawing.Size(358, 20)
        Me.txtNEFTUploaderREFNo.TabIndex = 281
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(9, 112)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(125, 16)
        Me.MyLabel3.TabIndex = 282
        Me.MyLabel3.Text = "NEFT Uploder REF. No"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.chkPrevMPAdj)
        Me.GroupBox2.Controls.Add(Me.chkSkipPreviousDocumentOfAdvancePayment)
        Me.GroupBox2.Controls.Add(Me.chkSkipPrevItemIssueReturn)
        Me.GroupBox2.Controls.Add(Me.ChkSkipMccSaleReturn)
        Me.GroupBox2.Controls.Add(Me.chkSkipPrevCreditNote)
        Me.GroupBox2.Controls.Add(Me.chkSkipPrevDeduction)
        Me.GroupBox2.Controls.Add(Me.chkSkipPrevItemIssue)
        Me.GroupBox2.Controls.Add(Me.chkSkipPrevMccSale)
        Me.GroupBox2.Location = New System.Drawing.Point(353, 25)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(484, 84)
        Me.GroupBox2.TabIndex = 280
        Me.GroupBox2.TabStop = False
        '
        'chkPrevMPAdj
        '
        Me.chkPrevMPAdj.Location = New System.Drawing.Point(6, 63)
        Me.chkPrevMPAdj.MyLinkLable1 = Nothing
        Me.chkPrevMPAdj.MyLinkLable2 = Nothing
        Me.chkPrevMPAdj.Name = "chkPrevMPAdj"
        Me.chkPrevMPAdj.Size = New System.Drawing.Size(243, 18)
        Me.chkPrevMPAdj.TabIndex = 67
        Me.chkPrevMPAdj.Tag1 = Nothing
        Me.chkPrevMPAdj.Text = "Skip Prev Documents of Farmer Adjustments"
        '
        'chkSkipPreviousDocumentOfAdvancePayment
        '
        Me.chkSkipPreviousDocumentOfAdvancePayment.Location = New System.Drawing.Point(253, 64)
        Me.chkSkipPreviousDocumentOfAdvancePayment.MyLinkLable1 = Nothing
        Me.chkSkipPreviousDocumentOfAdvancePayment.MyLinkLable2 = Nothing
        Me.chkSkipPreviousDocumentOfAdvancePayment.Name = "chkSkipPreviousDocumentOfAdvancePayment"
        Me.chkSkipPreviousDocumentOfAdvancePayment.Size = New System.Drawing.Size(227, 18)
        Me.chkSkipPreviousDocumentOfAdvancePayment.TabIndex = 66
        Me.chkSkipPreviousDocumentOfAdvancePayment.Tag1 = Nothing
        Me.chkSkipPreviousDocumentOfAdvancePayment.Text = "Skip Prev Documents of Advane Payment"
        '
        'chkSkipPrevItemIssueReturn
        '
        Me.chkSkipPrevItemIssueReturn.Location = New System.Drawing.Point(6, 9)
        Me.chkSkipPrevItemIssueReturn.MyLinkLable1 = Nothing
        Me.chkSkipPrevItemIssueReturn.MyLinkLable2 = Nothing
        Me.chkSkipPrevItemIssueReturn.Name = "chkSkipPrevItemIssueReturn"
        Me.chkSkipPrevItemIssueReturn.Size = New System.Drawing.Size(229, 18)
        Me.chkSkipPrevItemIssueReturn.TabIndex = 65
        Me.chkSkipPrevItemIssueReturn.Tag1 = Nothing
        Me.chkSkipPrevItemIssueReturn.Text = "Skip Prev Documents of Item Issue Return"
        '
        'ChkSkipMccSaleReturn
        '
        Me.ChkSkipMccSaleReturn.Location = New System.Drawing.Point(6, 27)
        Me.ChkSkipMccSaleReturn.MyLinkLable1 = Nothing
        Me.ChkSkipMccSaleReturn.MyLinkLable2 = Nothing
        Me.ChkSkipMccSaleReturn.Name = "ChkSkipMccSaleReturn"
        Me.ChkSkipMccSaleReturn.Size = New System.Drawing.Size(227, 18)
        Me.ChkSkipMccSaleReturn.TabIndex = 62
        Me.ChkSkipMccSaleReturn.Tag1 = Nothing
        Me.ChkSkipMccSaleReturn.Text = "Skip Prev Documents of MCC Sale Return"
        '
        'chkSkipPrevCreditNote
        '
        Me.chkSkipPrevCreditNote.Location = New System.Drawing.Point(253, 45)
        Me.chkSkipPrevCreditNote.MyLinkLable1 = Nothing
        Me.chkSkipPrevCreditNote.MyLinkLable2 = Nothing
        Me.chkSkipPrevCreditNote.Name = "chkSkipPrevCreditNote"
        Me.chkSkipPrevCreditNote.Size = New System.Drawing.Size(201, 18)
        Me.chkSkipPrevCreditNote.TabIndex = 64
        Me.chkSkipPrevCreditNote.Tag1 = Nothing
        Me.chkSkipPrevCreditNote.Text = "Skip Prev Documents of Credit Note"
        '
        'chkSkipPrevDeduction
        '
        Me.chkSkipPrevDeduction.Location = New System.Drawing.Point(253, 27)
        Me.chkSkipPrevDeduction.MyLinkLable1 = Nothing
        Me.chkSkipPrevDeduction.MyLinkLable2 = Nothing
        Me.chkSkipPrevDeduction.Name = "chkSkipPrevDeduction"
        Me.chkSkipPrevDeduction.Size = New System.Drawing.Size(199, 18)
        Me.chkSkipPrevDeduction.TabIndex = 63
        Me.chkSkipPrevDeduction.Tag1 = Nothing
        Me.chkSkipPrevDeduction.Text = "Skip Prev Documents of Deductions"
        '
        'chkSkipPrevItemIssue
        '
        Me.chkSkipPrevItemIssue.Location = New System.Drawing.Point(6, 45)
        Me.chkSkipPrevItemIssue.MyLinkLable1 = Nothing
        Me.chkSkipPrevItemIssue.MyLinkLable2 = Nothing
        Me.chkSkipPrevItemIssue.Name = "chkSkipPrevItemIssue"
        Me.chkSkipPrevItemIssue.Size = New System.Drawing.Size(193, 18)
        Me.chkSkipPrevItemIssue.TabIndex = 62
        Me.chkSkipPrevItemIssue.Tag1 = Nothing
        Me.chkSkipPrevItemIssue.Text = "Skip Prev Documents of Item Issue"
        '
        'chkSkipPrevMccSale
        '
        Me.chkSkipPrevMccSale.Location = New System.Drawing.Point(253, 9)
        Me.chkSkipPrevMccSale.MyLinkLable1 = Nothing
        Me.chkSkipPrevMccSale.MyLinkLable2 = Nothing
        Me.chkSkipPrevMccSale.Name = "chkSkipPrevMccSale"
        Me.chkSkipPrevMccSale.Size = New System.Drawing.Size(190, 18)
        Me.chkSkipPrevMccSale.TabIndex = 61
        Me.chkSkipPrevMccSale.Tag1 = Nothing
        Me.chkSkipPrevMccSale.Text = "Skip Prev Documents of MCC Sale"
        '
        'btnUnselectAll
        '
        Me.btnUnselectAll.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUnselectAll.Location = New System.Drawing.Point(654, 111)
        Me.btnUnselectAll.Name = "btnUnselectAll"
        Me.btnUnselectAll.Size = New System.Drawing.Size(161, 21)
        Me.btnUnselectAll.TabIndex = 279
        Me.btnUnselectAll.Text = "Unselect All"
        '
        'btnSelectAll
        '
        Me.btnSelectAll.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSelectAll.Location = New System.Drawing.Point(498, 110)
        Me.btnSelectAll.Name = "btnSelectAll"
        Me.btnSelectAll.Size = New System.Drawing.Size(150, 21)
        Me.btnSelectAll.TabIndex = 278
        Me.btnSelectAll.Text = "Select All"
        '
        'txtLocName
        '
        Me.txtLocName.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtLocName.CalculationExpression = Nothing
        Me.txtLocName.Enabled = False
        Me.txtLocName.FieldCode = Nothing
        Me.txtLocName.FieldDesc = Nothing
        Me.txtLocName.FieldMaxLength = 0
        Me.txtLocName.FieldName = Nothing
        Me.txtLocName.isCalculatedField = False
        Me.txtLocName.IsSourceFromTable = False
        Me.txtLocName.IsSourceFromValueList = False
        Me.txtLocName.IsUnique = False
        Me.txtLocName.Location = New System.Drawing.Point(207, 26)
        Me.txtLocName.MendatroryField = False
        Me.txtLocName.MyLinkLable1 = Nothing
        Me.txtLocName.MyLinkLable2 = Nothing
        Me.txtLocName.Name = "txtLocName"
        Me.txtLocName.ReferenceFieldDesc = Nothing
        Me.txtLocName.ReferenceFieldName = Nothing
        Me.txtLocName.ReferenceTableName = Nothing
        Me.txtLocName.Size = New System.Drawing.Size(143, 20)
        Me.txtLocName.TabIndex = 276
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(5, 28)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 16)
        Me.lblLocation.TabIndex = 277
        Me.lblLocation.Text = "Location"
        '
        'fndLoc
        '
        Me.fndLoc.CalculationExpression = Nothing
        Me.fndLoc.FieldCode = Nothing
        Me.fndLoc.FieldDesc = Nothing
        Me.fndLoc.FieldMaxLength = 0
        Me.fndLoc.FieldName = Nothing
        Me.fndLoc.isCalculatedField = False
        Me.fndLoc.IsSourceFromTable = False
        Me.fndLoc.IsSourceFromValueList = False
        Me.fndLoc.IsUnique = False
        Me.fndLoc.Location = New System.Drawing.Point(68, 27)
        Me.fndLoc.MendatroryField = True
        Me.fndLoc.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLoc.MyLinkLable1 = Me.lblLocation
        Me.fndLoc.MyLinkLable2 = Nothing
        Me.fndLoc.MyReadOnly = False
        Me.fndLoc.MyShowMasterFormButton = False
        Me.fndLoc.Name = "fndLoc"
        Me.fndLoc.ReferenceFieldDesc = Nothing
        Me.fndLoc.ReferenceFieldName = Nothing
        Me.fndLoc.ReferenceTableName = Nothing
        Me.fndLoc.Size = New System.Drawing.Size(136, 19)
        Me.fndLoc.TabIndex = 275
        Me.fndLoc.Value = ""
        '
        'btnGo
        '
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(282, 54)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(68, 55)
        Me.btnGo.TabIndex = 266
        Me.btnGo.Text = ">>"
        '
        'dtpDate
        '
        Me.dtpDate.CalculationExpression = Nothing
        Me.dtpDate.CustomFormat = "dd/MM/yyyy hh:mm:ss tt"
        Me.dtpDate.FieldCode = Nothing
        Me.dtpDate.FieldDesc = Nothing
        Me.dtpDate.FieldMaxLength = 0
        Me.dtpDate.FieldName = Nothing
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDate.isCalculatedField = False
        Me.dtpDate.IsSourceFromTable = False
        Me.dtpDate.IsSourceFromValueList = False
        Me.dtpDate.IsUnique = False
        Me.dtpDate.Location = New System.Drawing.Point(487, 4)
        Me.dtpDate.MendatroryField = False
        Me.dtpDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDate.MyLinkLable1 = Me.lblDocDate
        Me.dtpDate.MyLinkLable2 = Nothing
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDate.ReferenceFieldDesc = Nothing
        Me.dtpDate.ReferenceFieldName = Nothing
        Me.dtpDate.ReferenceTableName = Nothing
        Me.dtpDate.Size = New System.Drawing.Size(147, 20)
        Me.dtpDate.TabIndex = 259
        Me.dtpDate.TabStop = False
        Me.dtpDate.Text = "10/06/2011 11:51:56 AM"
        Me.dtpDate.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'lblDocDate
        '
        Me.lblDocDate.FieldName = Nothing
        Me.lblDocDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocDate.Location = New System.Drawing.Point(356, 6)
        Me.lblDocDate.Name = "lblDocDate"
        Me.lblDocDate.Size = New System.Drawing.Size(122, 16)
        Me.lblDocDate.TabIndex = 262
        Me.lblDocDate.Text = "Payment Process Date"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.MyLabel2)
        Me.GroupBox1.Controls.Add(Me.dtpToDate)
        Me.GroupBox1.Controls.Add(Me.MyLabel1)
        Me.GroupBox1.Controls.Add(Me.dtpFromDate)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 48)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(272, 38)
        Me.GroupBox1.TabIndex = 263
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Date Range"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(142, 16)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(19, 16)
        Me.MyLabel2.TabIndex = 266
        Me.MyLabel2.Text = "To"
        '
        'dtpToDate
        '
        Me.dtpToDate.CalculationExpression = Nothing
        Me.dtpToDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpToDate.FieldCode = Nothing
        Me.dtpToDate.FieldDesc = Nothing
        Me.dtpToDate.FieldMaxLength = 0
        Me.dtpToDate.FieldName = Nothing
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.isCalculatedField = False
        Me.dtpToDate.IsSourceFromTable = False
        Me.dtpToDate.IsSourceFromValueList = False
        Me.dtpToDate.IsUnique = False
        Me.dtpToDate.Location = New System.Drawing.Point(181, 14)
        Me.dtpToDate.MendatroryField = False
        Me.dtpToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.MyLinkLable1 = Me.MyLabel2
        Me.dtpToDate.MyLinkLable2 = Nothing
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.ReadOnly = True
        Me.dtpToDate.ReferenceFieldDesc = Nothing
        Me.dtpToDate.ReferenceFieldName = Nothing
        Me.dtpToDate.ReferenceTableName = Nothing
        Me.dtpToDate.Size = New System.Drawing.Size(84, 20)
        Me.dtpToDate.TabIndex = 265
        Me.dtpToDate.TabStop = False
        Me.dtpToDate.Text = "10/06/2011"
        Me.dtpToDate.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(6, 16)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(33, 16)
        Me.MyLabel1.TabIndex = 264
        Me.MyLabel1.Text = "From"
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CalculationExpression = Nothing
        Me.dtpFromDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpFromDate.FieldCode = Nothing
        Me.dtpFromDate.FieldDesc = Nothing
        Me.dtpFromDate.FieldMaxLength = 0
        Me.dtpFromDate.FieldName = Nothing
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.isCalculatedField = False
        Me.dtpFromDate.IsSourceFromTable = False
        Me.dtpFromDate.IsSourceFromValueList = False
        Me.dtpFromDate.IsUnique = False
        Me.dtpFromDate.Location = New System.Drawing.Point(45, 14)
        Me.dtpFromDate.MendatroryField = False
        Me.dtpFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromDate.MyLinkLable1 = Me.MyLabel1
        Me.dtpFromDate.MyLinkLable2 = Nothing
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromDate.ReferenceFieldDesc = Nothing
        Me.dtpFromDate.ReferenceFieldName = Nothing
        Me.dtpFromDate.ReferenceTableName = Nothing
        Me.dtpFromDate.Size = New System.Drawing.Size(84, 20)
        Me.dtpFromDate.TabIndex = 263
        Me.dtpFromDate.TabStop = False
        Me.dtpFromDate.Text = "10/06/2011"
        Me.dtpFromDate.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'lblPending
        '
        Me.lblPending.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lblPending.Location = New System.Drawing.Point(637, 3)
        Me.lblPending.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPending.Name = "lblPending"
        Me.lblPending.Size = New System.Drawing.Size(174, 20)
        Me.lblPending.Status = common.ERPTransactionStatus.Pending
        Me.lblPending.TabIndex = 256
        '
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Image = Global.XpertERPFarmerPayment.My.Resources.Resources._new
        Me.btnReset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReset.Location = New System.Drawing.Point(331, 4)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(19, 21)
        Me.btnReset.TabIndex = 260
        '
        'lblDocNo
        '
        Me.lblDocNo.FieldName = Nothing
        Me.lblDocNo.Location = New System.Drawing.Point(4, 5)
        Me.lblDocNo.Name = "lblDocNo"
        Me.lblDocNo.Size = New System.Drawing.Size(58, 18)
        Me.lblDocNo.TabIndex = 261
        Me.lblDocNo.Text = "Document"
        '
        'fndDocNo
        '
        Me.fndDocNo.FieldName = Nothing
        Me.fndDocNo.Location = New System.Drawing.Point(66, 4)
        Me.fndDocNo.MendatroryField = False
        Me.fndDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.fndDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndDocNo.MyLinkLable1 = Me.lblDocNo
        Me.fndDocNo.MyLinkLable2 = Nothing
        Me.fndDocNo.MyMaxLength = 30
        Me.fndDocNo.MyReadOnly = False
        Me.fndDocNo.Name = "fndDocNo"
        Me.fndDocNo.Size = New System.Drawing.Size(265, 21)
        Me.fndDocNo.TabIndex = 258
        Me.fndDocNo.Value = ""
        '
        'gv
        '
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv.MyStopExport = False
        Me.gv.Name = "gv"
        Me.gv.ShowHeaderCellButtons = True
        Me.gv.Size = New System.Drawing.Size(1172, 286)
        Me.gv.TabIndex = 0
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPreFormat2Print)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExportToExcelPaymentToFarmer)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPreFormatePrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReverse)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDocPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnProcess)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(1193, 501)
        Me.SplitContainer1.SplitterDistance = 471
        Me.SplitContainer1.TabIndex = 2
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage8)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.Controls.Add(Me.RDPMccSaleReturn)
        Me.RadPageView1.Controls.Add(Me.RDPMccSaleFarmer)
        Me.RadPageView1.Controls.Add(Me.RDPMccSaleReturnFarmer)
        Me.RadPageView1.Controls.Add(Me.RDPFarmerdjustment)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage4)
        Me.RadPageView1.Controls.Add(Me.PageItemIssueReturn)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage5)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage6)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage7)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage9)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1193, 471)
        Me.RadPageView1.TabIndex = 2
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage8
        '
        Me.RadPageViewPage8.Controls.Add(Me.gvPaymentToFarmer)
        Me.RadPageViewPage8.ItemSize = New System.Drawing.SizeF(113.0!, 28.0!)
        Me.RadPageViewPage8.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage8.Name = "RadPageViewPage8"
        Me.RadPageViewPage8.Size = New System.Drawing.Size(1172, 423)
        Me.RadPageViewPage8.Text = "Payment To Farmer"
        '
        'gvPaymentToFarmer
        '
        Me.gvPaymentToFarmer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvPaymentToFarmer.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvPaymentToFarmer.MasterTemplate.EnableAlternatingRowColor = True
        Me.gvPaymentToFarmer.MasterTemplate.EnableFiltering = True
        Me.gvPaymentToFarmer.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvPaymentToFarmer.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvPaymentToFarmer.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gvPaymentToFarmer.MyStopExport = False
        Me.gvPaymentToFarmer.Name = "gvPaymentToFarmer"
        Me.gvPaymentToFarmer.ShowHeaderCellButtons = True
        Me.gvPaymentToFarmer.Size = New System.Drawing.Size(1172, 423)
        Me.gvPaymentToFarmer.TabIndex = 1
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gvInvoice)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(56.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1172, 423)
        Me.RadPageViewPage2.Text = "Invoices"
        '
        'gvInvoice
        '
        Me.gvInvoice.Cursor = System.Windows.Forms.Cursors.AppStarting
        Me.gvInvoice.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvInvoice.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvInvoice.MasterTemplate.EnableFiltering = True
        Me.gvInvoice.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvInvoice.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvInvoice.MasterTemplate.ViewDefinition = TableViewDefinition3
        Me.gvInvoice.MyStopExport = False
        Me.gvInvoice.Name = "gvInvoice"
        Me.gvInvoice.ShowHeaderCellButtons = True
        Me.gvInvoice.Size = New System.Drawing.Size(1172, 423)
        Me.gvInvoice.TabIndex = 266
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.gvMccSale)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(64.0!, 28.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(1172, 423)
        Me.RadPageViewPage3.Text = "MCC Sale"
        '
        'gvMccSale
        '
        Me.gvMccSale.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvMccSale.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvMccSale.MasterTemplate.EnableFiltering = True
        Me.gvMccSale.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvMccSale.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvMccSale.MasterTemplate.ViewDefinition = TableViewDefinition4
        Me.gvMccSale.MyStopExport = False
        Me.gvMccSale.Name = "gvMccSale"
        Me.gvMccSale.ShowHeaderCellButtons = True
        Me.gvMccSale.Size = New System.Drawing.Size(1172, 423)
        Me.gvMccSale.TabIndex = 266
        '
        'RDPMccSaleReturn
        '
        Me.RDPMccSaleReturn.Controls.Add(Me.GvMccSaleReturn)
        Me.RDPMccSaleReturn.ItemSize = New System.Drawing.SizeF(97.0!, 28.0!)
        Me.RDPMccSaleReturn.Location = New System.Drawing.Point(10, 37)
        Me.RDPMccSaleReturn.Name = "RDPMccSaleReturn"
        Me.RDPMccSaleReturn.Size = New System.Drawing.Size(1172, 423)
        Me.RDPMccSaleReturn.Text = "Mcc Sale Return"
        '
        'GvMccSaleReturn
        '
        Me.GvMccSaleReturn.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GvMccSaleReturn.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.GvMccSaleReturn.MasterTemplate.EnableFiltering = True
        Me.GvMccSaleReturn.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.GvMccSaleReturn.MasterTemplate.ShowHeaderCellButtons = True
        Me.GvMccSaleReturn.MasterTemplate.ViewDefinition = TableViewDefinition5
        Me.GvMccSaleReturn.MyStopExport = False
        Me.GvMccSaleReturn.Name = "GvMccSaleReturn"
        Me.GvMccSaleReturn.ShowHeaderCellButtons = True
        Me.GvMccSaleReturn.Size = New System.Drawing.Size(1172, 423)
        Me.GvMccSaleReturn.TabIndex = 267
        '
        'RDPMccSaleFarmer
        '
        Me.RDPMccSaleFarmer.Controls.Add(Me.gvMccSaleFarmer)
        Me.RDPMccSaleFarmer.ItemSize = New System.Drawing.SizeF(90.0!, 28.0!)
        Me.RDPMccSaleFarmer.Location = New System.Drawing.Point(10, 37)
        Me.RDPMccSaleFarmer.Name = "RDPMccSaleFarmer"
        Me.RDPMccSaleFarmer.Size = New System.Drawing.Size(1172, 423)
        Me.RDPMccSaleFarmer.Text = "Sale To Farmer"
        '
        'gvMccSaleFarmer
        '
        Me.gvMccSaleFarmer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvMccSaleFarmer.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvMccSaleFarmer.MasterTemplate.EnableFiltering = True
        Me.gvMccSaleFarmer.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvMccSaleFarmer.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvMccSaleFarmer.MasterTemplate.ViewDefinition = TableViewDefinition6
        Me.gvMccSaleFarmer.MyStopExport = False
        Me.gvMccSaleFarmer.Name = "gvMccSaleFarmer"
        Me.gvMccSaleFarmer.ShowHeaderCellButtons = True
        Me.gvMccSaleFarmer.Size = New System.Drawing.Size(1172, 423)
        Me.gvMccSaleFarmer.TabIndex = 267
        '
        'RDPMccSaleReturnFarmer
        '
        Me.RDPMccSaleReturnFarmer.Controls.Add(Me.gvMccSaleReturnFarmer)
        Me.RDPMccSaleReturnFarmer.ItemSize = New System.Drawing.SizeF(111.0!, 28.0!)
        Me.RDPMccSaleReturnFarmer.Location = New System.Drawing.Point(10, 37)
        Me.RDPMccSaleReturnFarmer.Name = "RDPMccSaleReturnFarmer"
        Me.RDPMccSaleReturnFarmer.Size = New System.Drawing.Size(1172, 423)
        Me.RDPMccSaleReturnFarmer.Text = "Sale Return Farmer"
        '
        'gvMccSaleReturnFarmer
        '
        Me.gvMccSaleReturnFarmer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvMccSaleReturnFarmer.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvMccSaleReturnFarmer.MasterTemplate.EnableFiltering = True
        Me.gvMccSaleReturnFarmer.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvMccSaleReturnFarmer.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvMccSaleReturnFarmer.MasterTemplate.ViewDefinition = TableViewDefinition7
        Me.gvMccSaleReturnFarmer.MyStopExport = False
        Me.gvMccSaleReturnFarmer.Name = "gvMccSaleReturnFarmer"
        Me.gvMccSaleReturnFarmer.ShowHeaderCellButtons = True
        Me.gvMccSaleReturnFarmer.Size = New System.Drawing.Size(1172, 423)
        Me.gvMccSaleReturnFarmer.TabIndex = 267
        '
        'RDPFarmerdjustment
        '
        Me.RDPFarmerdjustment.Controls.Add(Me.gvMPAdj)
        Me.RDPFarmerdjustment.ItemSize = New System.Drawing.SizeF(112.0!, 28.0!)
        Me.RDPFarmerdjustment.Location = New System.Drawing.Point(10, 37)
        Me.RDPFarmerdjustment.Name = "RDPFarmerdjustment"
        Me.RDPFarmerdjustment.Size = New System.Drawing.Size(1172, 423)
        Me.RDPFarmerdjustment.Text = "Farmer Adjustment"
        '
        'gvMPAdj
        '
        Me.gvMPAdj.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvMPAdj.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvMPAdj.MasterTemplate.EnableFiltering = True
        Me.gvMPAdj.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvMPAdj.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvMPAdj.MasterTemplate.ViewDefinition = TableViewDefinition8
        Me.gvMPAdj.MyStopExport = False
        Me.gvMPAdj.Name = "gvMPAdj"
        Me.gvMPAdj.ShowHeaderCellButtons = True
        Me.gvMPAdj.Size = New System.Drawing.Size(1172, 423)
        Me.gvMPAdj.TabIndex = 268
        '
        'RadPageViewPage4
        '
        Me.RadPageViewPage4.Controls.Add(Me.gvItemIssue)
        Me.RadPageViewPage4.ItemSize = New System.Drawing.SizeF(67.0!, 28.0!)
        Me.RadPageViewPage4.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage4.Name = "RadPageViewPage4"
        Me.RadPageViewPage4.Size = New System.Drawing.Size(897, 423)
        Me.RadPageViewPage4.Text = "Item Issue"
        '
        'gvItemIssue
        '
        Me.gvItemIssue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvItemIssue.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvItemIssue.MasterTemplate.EnableFiltering = True
        Me.gvItemIssue.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvItemIssue.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvItemIssue.MasterTemplate.ViewDefinition = TableViewDefinition9
        Me.gvItemIssue.MyStopExport = False
        Me.gvItemIssue.Name = "gvItemIssue"
        Me.gvItemIssue.ShowHeaderCellButtons = True
        Me.gvItemIssue.Size = New System.Drawing.Size(897, 423)
        Me.gvItemIssue.TabIndex = 266
        '
        'PageItemIssueReturn
        '
        Me.PageItemIssueReturn.Controls.Add(Me.gvItemIssueReturn)
        Me.PageItemIssueReturn.ItemSize = New System.Drawing.SizeF(103.0!, 28.0!)
        Me.PageItemIssueReturn.Location = New System.Drawing.Point(10, 37)
        Me.PageItemIssueReturn.Name = "PageItemIssueReturn"
        Me.PageItemIssueReturn.Size = New System.Drawing.Size(897, 423)
        Me.PageItemIssueReturn.Text = "Item Issue Return"
        '
        'gvItemIssueReturn
        '
        Me.gvItemIssueReturn.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvItemIssueReturn.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvItemIssueReturn.MasterTemplate.EnableFiltering = True
        Me.gvItemIssueReturn.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvItemIssueReturn.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvItemIssueReturn.MasterTemplate.ViewDefinition = TableViewDefinition10
        Me.gvItemIssueReturn.MyStopExport = False
        Me.gvItemIssueReturn.Name = "gvItemIssueReturn"
        Me.gvItemIssueReturn.ShowHeaderCellButtons = True
        Me.gvItemIssueReturn.Size = New System.Drawing.Size(897, 423)
        Me.gvItemIssueReturn.TabIndex = 267
        '
        'RadPageViewPage5
        '
        Me.RadPageViewPage5.Controls.Add(Me.gvDeduction)
        Me.RadPageViewPage5.ItemSize = New System.Drawing.SizeF(73.0!, 28.0!)
        Me.RadPageViewPage5.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage5.Name = "RadPageViewPage5"
        Me.RadPageViewPage5.Size = New System.Drawing.Size(897, 423)
        Me.RadPageViewPage5.Text = "Deductions"
        '
        'gvDeduction
        '
        Me.gvDeduction.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvDeduction.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvDeduction.MasterTemplate.EnableFiltering = True
        Me.gvDeduction.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvDeduction.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvDeduction.MasterTemplate.ViewDefinition = TableViewDefinition11
        Me.gvDeduction.MyStopExport = False
        Me.gvDeduction.Name = "gvDeduction"
        Me.gvDeduction.ShowHeaderCellButtons = True
        Me.gvDeduction.Size = New System.Drawing.Size(897, 423)
        Me.gvDeduction.TabIndex = 267
        '
        'RadPageViewPage6
        '
        Me.RadPageViewPage6.Controls.Add(Me.gvCreditNote)
        Me.RadPageViewPage6.ItemSize = New System.Drawing.SizeF(74.0!, 28.0!)
        Me.RadPageViewPage6.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage6.Name = "RadPageViewPage6"
        Me.RadPageViewPage6.Size = New System.Drawing.Size(1172, 423)
        Me.RadPageViewPage6.Text = "Credit Note"
        '
        'gvCreditNote
        '
        Me.gvCreditNote.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvCreditNote.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvCreditNote.MasterTemplate.EnableFiltering = True
        Me.gvCreditNote.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvCreditNote.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvCreditNote.MasterTemplate.ViewDefinition = TableViewDefinition12
        Me.gvCreditNote.MyStopExport = False
        Me.gvCreditNote.Name = "gvCreditNote"
        Me.gvCreditNote.ShowHeaderCellButtons = True
        Me.gvCreditNote.Size = New System.Drawing.Size(1172, 423)
        Me.gvCreditNote.TabIndex = 268
        '
        'RadPageViewPage7
        '
        Me.RadPageViewPage7.Controls.Add(Me.gvAdvancePayment)
        Me.RadPageViewPage7.ItemSize = New System.Drawing.SizeF(106.0!, 28.0!)
        Me.RadPageViewPage7.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage7.Name = "RadPageViewPage7"
        Me.RadPageViewPage7.Size = New System.Drawing.Size(1172, 423)
        Me.RadPageViewPage7.Text = "Advance Payment"
        '
        'gvAdvancePayment
        '
        Me.gvAdvancePayment.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvAdvancePayment.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvAdvancePayment.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvAdvancePayment.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvAdvancePayment.MasterTemplate.ViewDefinition = TableViewDefinition13
        Me.gvAdvancePayment.MyStopExport = False
        Me.gvAdvancePayment.Name = "gvAdvancePayment"
        Me.gvAdvancePayment.ShowHeaderCellButtons = True
        Me.gvAdvancePayment.Size = New System.Drawing.Size(1172, 423)
        Me.gvAdvancePayment.TabIndex = 269
        '
        'RadPageViewPage9
        '
        Me.RadPageViewPage9.Controls.Add(Me.gvFA)
        Me.RadPageViewPage9.ItemSize = New System.Drawing.SizeF(97.0!, 28.0!)
        Me.RadPageViewPage9.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage9.Name = "RadPageViewPage9"
        Me.RadPageViewPage9.Size = New System.Drawing.Size(1172, 423)
        Me.RadPageViewPage9.Text = "Farmer Advance"
        '
        'gvFA
        '
        Me.gvFA.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvFA.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvFA.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvFA.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvFA.MasterTemplate.ViewDefinition = TableViewDefinition14
        Me.gvFA.MyStopExport = False
        Me.gvFA.Name = "gvFA"
        Me.gvFA.ShowHeaderCellButtons = True
        Me.gvFA.Size = New System.Drawing.Size(1172, 423)
        Me.gvFA.TabIndex = 270
        '
        'btnPreFormat2Print
        '
        Me.btnPreFormat2Print.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPreFormat2Print.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPreFormat2Print.Location = New System.Drawing.Point(713, 3)
        Me.btnPreFormat2Print.Name = "btnPreFormat2Print"
        Me.btnPreFormat2Print.Size = New System.Drawing.Size(106, 18)
        Me.btnPreFormat2Print.TabIndex = 285
        Me.btnPreFormat2Print.Text = "Pre Formate2 Print"
        '
        'btnExportToExcelPaymentToFarmer
        '
        Me.btnExportToExcelPaymentToFarmer.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExportToExcelPaymentToFarmer.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExportToExcelPaymentToFarmer.Location = New System.Drawing.Point(880, 4)
        Me.btnExportToExcelPaymentToFarmer.Name = "btnExportToExcelPaymentToFarmer"
        Me.btnExportToExcelPaymentToFarmer.Size = New System.Drawing.Size(191, 18)
        Me.btnExportToExcelPaymentToFarmer.TabIndex = 284
        Me.btnExportToExcelPaymentToFarmer.Text = "Export To Excel Payment To Farmer"
        '
        'btnPreFormatePrint
        '
        Me.btnPreFormatePrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPreFormatePrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPreFormatePrint.Location = New System.Drawing.Point(601, 4)
        Me.btnPreFormatePrint.Name = "btnPreFormatePrint"
        Me.btnPreFormatePrint.Size = New System.Drawing.Size(106, 18)
        Me.btnPreFormatePrint.TabIndex = 283
        Me.btnPreFormatePrint.Text = "Pre Formate Print"
        '
        'btnReverse
        '
        Me.btnReverse.Location = New System.Drawing.Point(469, 4)
        Me.btnReverse.Name = "btnReverse"
        Me.btnReverse.Size = New System.Drawing.Size(126, 18)
        Me.btnReverse.TabIndex = 282
        Me.btnReverse.Text = "Revese And Unpost"
        Me.btnReverse.Visible = False
        '
        'btnDocPrint
        '
        Me.btnDocPrint.Location = New System.Drawing.Point(387, 4)
        Me.btnDocPrint.Name = "btnDocPrint"
        Me.btnDocPrint.Size = New System.Drawing.Size(79, 18)
        Me.btnDocPrint.TabIndex = 280
        Me.btnDocPrint.Text = "Doc Print"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(316, 4)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 18)
        Me.btnPrint.TabIndex = 21
        Me.btnPrint.Text = "Print"
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExport.Location = New System.Drawing.Point(216, 4)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(97, 18)
        Me.btnExport.TabIndex = 279
        Me.btnExport.Text = "Export To Excel"
        '
        'btnProcess
        '
        Me.btnProcess.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnProcess.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnProcess.Location = New System.Drawing.Point(145, 4)
        Me.btnProcess.Name = "btnProcess"
        Me.btnProcess.Size = New System.Drawing.Size(68, 18)
        Me.btnProcess.TabIndex = 7
        Me.btnProcess.Text = "Process"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(74, 4)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 18)
        Me.btnDelete.TabIndex = 6
        Me.btnDelete.Text = "Delete"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(1122, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 8
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(3, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 18)
        Me.btnSave.TabIndex = 5
        Me.btnSave.Text = "Save"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1193, 20)
        Me.RadMenu1.TabIndex = 5
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItem2})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Save Layout"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Delete Layout"
        '
        'frmPaymentProcessFarmer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1193, 521)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmPaymentProcessFarmer"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Farmer Payment Process"
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.chkSelected, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNEFTUploaderREFNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.chkPrevMPAdj, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSkipPreviousDocumentOfAdvancePayment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSkipPrevItemIssueReturn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkSkipMccSaleReturn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSkipPrevCreditNote, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSkipPrevDeduction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSkipPrevItemIssue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSkipPrevMccSale, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUnselectAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSelectAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLocName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage8.ResumeLayout(False)
        CType(Me.gvPaymentToFarmer.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvPaymentToFarmer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gvInvoice.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage3.ResumeLayout(False)
        CType(Me.gvMccSale.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvMccSale, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RDPMccSaleReturn.ResumeLayout(False)
        CType(Me.GvMccSaleReturn.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GvMccSaleReturn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RDPMccSaleFarmer.ResumeLayout(False)
        CType(Me.gvMccSaleFarmer.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvMccSaleFarmer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RDPMccSaleReturnFarmer.ResumeLayout(False)
        CType(Me.gvMccSaleReturnFarmer.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvMccSaleReturnFarmer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RDPFarmerdjustment.ResumeLayout(False)
        CType(Me.gvMPAdj.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvMPAdj, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage4.ResumeLayout(False)
        CType(Me.gvItemIssue.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvItemIssue, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PageItemIssueReturn.ResumeLayout(False)
        CType(Me.gvItemIssueReturn.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvItemIssueReturn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage5.ResumeLayout(False)
        CType(Me.gvDeduction.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDeduction, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage6.ResumeLayout(False)
        CType(Me.gvCreditNote.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCreditNote, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage7.ResumeLayout(False)
        CType(Me.gvAdvancePayment.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvAdvancePayment, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage9.ResumeLayout(False)
        CType(Me.gvFA.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvFA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPreFormat2Print, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExportToExcelPaymentToFarmer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPreFormatePrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDocPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnProcess, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents lblPending As common.usLock
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents btnProcess As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblDocDate As common.Controls.MyLabel
    Friend WithEvents fndDocNo As common.UserControls.txtNavigator
    Friend WithEvents lblDocNo As common.Controls.MyLabel
    Friend WithEvents dtpDate As common.Controls.MyDateTimePicker
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents dtpFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents dtpToDate As common.Controls.MyDateTimePicker
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtLocName As common.Controls.MyTextBox
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents fndLoc As common.UserControls.txtFinder
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvInvoice As common.UserControls.MyRadGridView
    Friend WithEvents RadPageViewPage3 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvMccSale As common.UserControls.MyRadGridView
    Friend WithEvents RadPageViewPage4 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvItemIssue As common.UserControls.MyRadGridView
    Friend WithEvents RadPageViewPage5 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvDeduction As common.UserControls.MyRadGridView
    Public WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents btnUnselectAll As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSelectAll As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents chkSkipPrevDeduction As common.Controls.MyCheckBox
    Friend WithEvents chkSkipPrevItemIssue As common.Controls.MyCheckBox
    Friend WithEvents chkSkipPrevMccSale As common.Controls.MyCheckBox
    Friend WithEvents txtNEFTUploaderREFNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents RadPageViewPage6 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvCreditNote As common.UserControls.MyRadGridView
    Friend WithEvents chkSkipPrevCreditNote As common.Controls.MyCheckBox
    Friend WithEvents RDPMccSaleReturn As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents GvMccSaleReturn As common.UserControls.MyRadGridView
    Friend WithEvents ChkSkipMccSaleReturn As common.Controls.MyCheckBox
    Friend WithEvents PageItemIssueReturn As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvItemIssueReturn As common.UserControls.MyRadGridView
    Friend WithEvents chkSkipPrevItemIssueReturn As common.Controls.MyCheckBox
    Friend WithEvents txtVSP As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageViewPage7 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvAdvancePayment As common.UserControls.MyRadGridView
    Friend WithEvents btnDocPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents chkSkipPreviousDocumentOfAdvancePayment As common.Controls.MyCheckBox
    Friend WithEvents RadPageViewPage8 As Telerik.WinControls.UI.RadPageViewPage
    Public WithEvents gvPaymentToFarmer As common.UserControls.MyRadGridView
    Friend WithEvents RDPMccSaleFarmer As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RDPMccSaleReturnFarmer As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvMccSaleFarmer As common.UserControls.MyRadGridView
    Friend WithEvents gvMccSaleReturnFarmer As common.UserControls.MyRadGridView
    Friend WithEvents RDPFarmerdjustment As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvMPAdj As common.UserControls.MyRadGridView
    Friend WithEvents chkPrevMPAdj As common.Controls.MyCheckBox
    Friend WithEvents btnReverse As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPreFormatePrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageViewPage9 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvFA As common.UserControls.MyRadGridView
    Friend WithEvents btnExportToExcelPaymentToFarmer As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPreFormat2Print As RadButton
    Friend WithEvents chkSelected As common.Controls.MyCheckBox
End Class

