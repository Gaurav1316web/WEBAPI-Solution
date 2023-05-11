<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class RptMccSaleAdjustment
    Inherits XpertERPEngine.FrmMainTranScreen

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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.rbtnVLCWise = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtnMCCWise = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtnAdhoc = New Telerik.WinControls.UI.RadRadioButton()
        Me.txtPer = New common.MyNumBox()
        Me.chkSkippedDocument = New Telerik.WinControls.UI.RadCheckBox()
        Me.cbxPaymentCycle = New Telerik.WinControls.UI.RadCheckBox()
        Me.lblTankerNo = New common.Controls.MyLabel()
        Me.txtMCC = New common.UserControls.txtFinder()
        Me.lblMCC = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.txtVSP = New common.UserControls.txtMultiSelectFinder()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.RadSplitButton1 = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmiExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnreset = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnok = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnCreatePayment = New Telerik.WinControls.UI.RadButton()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.GrpPaymentDetails = New System.Windows.Forms.GroupBox()
        Me.btnCancel = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.pnlEMI = New System.Windows.Forms.Panel()
        Me.dtpPayment = New common.Controls.MyDateTimePicker()
        Me.lblpaymentdate = New common.Controls.MyLabel()
        Me.lblbankcode = New common.Controls.MyLabel()
        Me.txtPaymentMode = New common.UserControls.txtFinder()
        Me.txtBankCode = New common.UserControls.txtFinder()
        Me.lblpaymentcode = New common.Controls.MyLabel()
        Me.lblBankDesc = New common.Controls.MyLabel()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.rbtnVLCWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnMCCWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnAdhoc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSkippedDocument, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbxPaymentCycle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTankerNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMCC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnok, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCreatePayment, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpPaymentDetails.SuspendLayout()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlEMI.SuspendLayout()
        CType(Me.dtpPayment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblpaymentdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblbankcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblpaymentcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBankDesc, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnCreatePayment)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadSplitButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnreset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnok)
        Me.SplitContainer1.Size = New System.Drawing.Size(817, 401)
        Me.SplitContainer1.SplitterDistance = 372
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
        Me.RadPageView1.Size = New System.Drawing.Size(817, 372)
        Me.RadPageView1.TabIndex = 1
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.Panel1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(796, 324)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.GrpPaymentDetails)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.chkSkippedDocument)
        Me.Panel1.Controls.Add(Me.cbxPaymentCycle)
        Me.Panel1.Controls.Add(Me.lblTankerNo)
        Me.Panel1.Controls.Add(Me.txtMCC)
        Me.Panel1.Controls.Add(Me.MyLabel3)
        Me.Panel1.Controls.Add(Me.lblMCC)
        Me.Panel1.Controls.Add(Me.MyLabel2)
        Me.Panel1.Controls.Add(Me.MyLabel4)
        Me.Panel1.Controls.Add(Me.txtToDate)
        Me.Panel1.Controls.Add(Me.txtFromDate)
        Me.Panel1.Controls.Add(Me.txtVSP)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(796, 324)
        Me.Panel1.TabIndex = 190
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.rbtnVLCWise)
        Me.Panel2.Controls.Add(Me.rbtnMCCWise)
        Me.Panel2.Controls.Add(Me.rbtnAdhoc)
        Me.Panel2.Controls.Add(Me.txtPer)
        Me.Panel2.Location = New System.Drawing.Point(197, 6)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(294, 42)
        Me.Panel2.TabIndex = 191
        '
        'rbtnVLCWise
        '
        Me.rbtnVLCWise.Location = New System.Drawing.Point(89, 3)
        Me.rbtnVLCWise.Name = "rbtnVLCWise"
        Me.rbtnVLCWise.Size = New System.Drawing.Size(67, 18)
        Me.rbtnVLCWise.TabIndex = 22
        Me.rbtnVLCWise.Text = "VLC Wise"
        '
        'rbtnMCCWise
        '
        Me.rbtnMCCWise.Location = New System.Drawing.Point(3, 3)
        Me.rbtnMCCWise.Name = "rbtnMCCWise"
        Me.rbtnMCCWise.Size = New System.Drawing.Size(72, 18)
        Me.rbtnMCCWise.TabIndex = 23
        Me.rbtnMCCWise.Text = "MCC Wise"
        '
        'rbtnAdhoc
        '
        Me.rbtnAdhoc.Location = New System.Drawing.Point(3, 22)
        Me.rbtnAdhoc.Name = "rbtnAdhoc"
        Me.rbtnAdhoc.Size = New System.Drawing.Size(118, 18)
        Me.rbtnAdhoc.TabIndex = 26
        Me.rbtnAdhoc.Text = "Adhoc Payment [%]"
        '
        'txtPer
        '
        Me.txtPer.BackColor = System.Drawing.Color.White
        Me.txtPer.CalculationExpression = Nothing
        Me.txtPer.DecimalPlaces = 0
        Me.txtPer.FieldCode = Nothing
        Me.txtPer.FieldDesc = Nothing
        Me.txtPer.FieldMaxLength = 5
        Me.txtPer.FieldName = Nothing
        Me.txtPer.isCalculatedField = False
        Me.txtPer.IsSourceFromTable = False
        Me.txtPer.IsSourceFromValueList = False
        Me.txtPer.IsUnique = False
        Me.txtPer.Location = New System.Drawing.Point(122, 21)
        Me.txtPer.MendatroryField = False
        Me.txtPer.MyLinkLable1 = Nothing
        Me.txtPer.MyLinkLable2 = Nothing
        Me.txtPer.Name = "txtPer"
        Me.txtPer.ReferenceFieldDesc = Nothing
        Me.txtPer.ReferenceFieldName = Nothing
        Me.txtPer.ReferenceTableName = Nothing
        Me.txtPer.Size = New System.Drawing.Size(37, 20)
        Me.txtPer.TabIndex = 189
        Me.txtPer.Text = "0"
        Me.txtPer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtPer.Value = 0R
        '
        'chkSkippedDocument
        '
        Me.chkSkippedDocument.Location = New System.Drawing.Point(73, 7)
        Me.chkSkippedDocument.Name = "chkSkippedDocument"
        Me.chkSkippedDocument.Size = New System.Drawing.Size(116, 18)
        Me.chkSkippedDocument.TabIndex = 190
        Me.chkSkippedDocument.Text = "Skipped Document"
        '
        'cbxPaymentCycle
        '
        Me.cbxPaymentCycle.Location = New System.Drawing.Point(73, 28)
        Me.cbxPaymentCycle.Name = "cbxPaymentCycle"
        Me.cbxPaymentCycle.Size = New System.Drawing.Size(111, 18)
        Me.cbxPaymentCycle.TabIndex = 24
        Me.cbxPaymentCycle.Text = "No Payment Cycle"
        '
        'lblTankerNo
        '
        Me.lblTankerNo.FieldName = Nothing
        Me.lblTankerNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTankerNo.Location = New System.Drawing.Point(9, 52)
        Me.lblTankerNo.Name = "lblTankerNo"
        Me.lblTankerNo.Size = New System.Drawing.Size(32, 16)
        Me.lblTankerNo.TabIndex = 19
        Me.lblTankerNo.Text = "MCC"
        '
        'txtMCC
        '
        Me.txtMCC.CalculationExpression = Nothing
        Me.txtMCC.FieldCode = Nothing
        Me.txtMCC.FieldDesc = Nothing
        Me.txtMCC.FieldMaxLength = 0
        Me.txtMCC.FieldName = Nothing
        Me.txtMCC.isCalculatedField = False
        Me.txtMCC.IsSourceFromTable = False
        Me.txtMCC.IsSourceFromValueList = False
        Me.txtMCC.IsUnique = False
        Me.txtMCC.Location = New System.Drawing.Point(73, 51)
        Me.txtMCC.MendatroryField = True
        Me.txtMCC.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMCC.MyLinkLable1 = Me.lblMCC
        Me.txtMCC.MyLinkLable2 = Me.lblTankerNo
        Me.txtMCC.MyReadOnly = False
        Me.txtMCC.MyShowMasterFormButton = False
        Me.txtMCC.Name = "txtMCC"
        Me.txtMCC.ReferenceFieldDesc = Nothing
        Me.txtMCC.ReferenceFieldName = Nothing
        Me.txtMCC.ReferenceTableName = Nothing
        Me.txtMCC.Size = New System.Drawing.Size(115, 18)
        Me.txtMCC.TabIndex = 12
        Me.txtMCC.Value = ""
        '
        'lblMCC
        '
        Me.lblMCC.AutoSize = False
        Me.lblMCC.BorderVisible = True
        Me.lblMCC.FieldName = Nothing
        Me.lblMCC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMCC.Location = New System.Drawing.Point(194, 51)
        Me.lblMCC.Name = "lblMCC"
        Me.lblMCC.Size = New System.Drawing.Size(297, 19)
        Me.lblMCC.TabIndex = 16
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(330, 74)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(46, 16)
        Me.MyLabel3.TabIndex = 17
        Me.MyLabel3.Text = "To Date"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(9, 74)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(60, 16)
        Me.MyLabel2.TabIndex = 20
        Me.MyLabel2.Text = "From Date"
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(9, 95)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(26, 18)
        Me.MyLabel4.TabIndex = 21
        Me.MyLabel4.Text = "VLC"
        '
        'txtToDate
        '
        Me.txtToDate.CalculationExpression = Nothing
        Me.txtToDate.CustomFormat = "dd/MMM/yyyy"
        Me.txtToDate.FieldCode = Nothing
        Me.txtToDate.FieldDesc = Nothing
        Me.txtToDate.FieldMaxLength = 0
        Me.txtToDate.FieldName = Nothing
        Me.txtToDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.isCalculatedField = False
        Me.txtToDate.IsSourceFromTable = False
        Me.txtToDate.IsSourceFromValueList = False
        Me.txtToDate.IsUnique = False
        Me.txtToDate.Location = New System.Drawing.Point(383, 73)
        Me.txtToDate.MendatroryField = True
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Me.MyLabel3
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.ReferenceFieldDesc = Nothing
        Me.txtToDate.ReferenceFieldName = Nothing
        Me.txtToDate.ReferenceTableName = Nothing
        Me.txtToDate.Size = New System.Drawing.Size(108, 18)
        Me.txtToDate.TabIndex = 14
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "09/Feb/2017"
        Me.txtToDate.Value = New Date(2017, 2, 9, 0, 0, 0, 0)
        '
        'txtFromDate
        '
        Me.txtFromDate.CalculationExpression = Nothing
        Me.txtFromDate.CustomFormat = "dd/MMM/yyyy"
        Me.txtFromDate.FieldCode = Nothing
        Me.txtFromDate.FieldDesc = Nothing
        Me.txtFromDate.FieldMaxLength = 0
        Me.txtFromDate.FieldName = Nothing
        Me.txtFromDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.isCalculatedField = False
        Me.txtFromDate.IsSourceFromTable = False
        Me.txtFromDate.IsSourceFromValueList = False
        Me.txtFromDate.IsUnique = False
        Me.txtFromDate.Location = New System.Drawing.Point(73, 73)
        Me.txtFromDate.MendatroryField = True
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Me.MyLabel2
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.ReferenceFieldDesc = Nothing
        Me.txtFromDate.ReferenceFieldName = Nothing
        Me.txtFromDate.ReferenceTableName = Nothing
        Me.txtFromDate.Size = New System.Drawing.Size(115, 18)
        Me.txtFromDate.TabIndex = 13
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "09/Feb/2017"
        Me.txtFromDate.Value = New Date(2017, 2, 9, 0, 0, 0, 0)
        '
        'txtVSP
        '
        Me.txtVSP.arrDispalyMember = Nothing
        Me.txtVSP.arrValueMember = Nothing
        Me.txtVSP.Location = New System.Drawing.Point(73, 95)
        Me.txtVSP.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVSP.MyLinkLable1 = Me.MyLabel4
        Me.txtVSP.MyLinkLable2 = Nothing
        Me.txtVSP.MyNullText = "All"
        Me.txtVSP.Name = "txtVSP"
        Me.txtVSP.Size = New System.Drawing.Size(418, 19)
        Me.txtVSP.TabIndex = 15
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(796, 324)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gv1
        '
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.AllowEditRow = False
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.Name = "gv1"
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(796, 324)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        '
        'RadSplitButton1
        '
        Me.RadSplitButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadSplitButton1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExcel, Me.rmiPDF, Me.RadMenuItem3})
        Me.RadSplitButton1.Location = New System.Drawing.Point(155, 3)
        Me.RadSplitButton1.Name = "RadSplitButton1"
        Me.RadSplitButton1.Size = New System.Drawing.Size(94, 18)
        Me.RadSplitButton1.TabIndex = 146
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
        'btnreset
        '
        Me.btnreset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnreset.Location = New System.Drawing.Point(80, 3)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(72, 18)
        Me.btnreset.TabIndex = 42
        Me.btnreset.Text = "Reset"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(748, 3)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 41
        Me.btnclose.Text = "Close"
        '
        'btnok
        '
        Me.btnok.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnok.Location = New System.Drawing.Point(6, 3)
        Me.btnok.Name = "btnok"
        Me.btnok.Size = New System.Drawing.Size(72, 18)
        Me.btnok.TabIndex = 39
        Me.btnok.Text = ">>>>"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenufile})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(817, 20)
        Me.RadMenu1.TabIndex = 26
        Me.RadMenu1.Text = "RadMenu1"
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
        'btnCreatePayment
        '
        Me.btnCreatePayment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCreatePayment.Location = New System.Drawing.Point(250, 3)
        Me.btnCreatePayment.Name = "btnCreatePayment"
        Me.btnCreatePayment.Size = New System.Drawing.Size(114, 18)
        Me.btnCreatePayment.TabIndex = 12121
        Me.btnCreatePayment.Text = "Create Payment"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Export Sheet for Advance Payment "
        Me.RadMenuItem3.AccessibleName = "Export Sheet for Advance Payment "
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Export Sheet for Advance Payment "
        '
        'GrpPaymentDetails
        '
        Me.GrpPaymentDetails.Controls.Add(Me.btnCancel)
        Me.GrpPaymentDetails.Controls.Add(Me.btnSave)
        Me.GrpPaymentDetails.Controls.Add(Me.pnlEMI)
        Me.GrpPaymentDetails.Location = New System.Drawing.Point(159, 120)
        Me.GrpPaymentDetails.Name = "GrpPaymentDetails"
        Me.GrpPaymentDetails.Size = New System.Drawing.Size(500, 126)
        Me.GrpPaymentDetails.TabIndex = 192
        Me.GrpPaymentDetails.TabStop = False
        Me.GrpPaymentDetails.Text = "Payment Details"
        '
        'btnCancel
        '
        Me.btnCancel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(298, 99)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(72, 18)
        Me.btnCancel.TabIndex = 12121
        Me.btnCancel.Text = "Cancel"
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(135, 99)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(153, 18)
        Me.btnSave.TabIndex = 12120
        Me.btnSave.Text = "Import Sheet and Save"
        '
        'pnlEMI
        '
        Me.pnlEMI.Controls.Add(Me.dtpPayment)
        Me.pnlEMI.Controls.Add(Me.lblpaymentdate)
        Me.pnlEMI.Controls.Add(Me.lblbankcode)
        Me.pnlEMI.Controls.Add(Me.txtPaymentMode)
        Me.pnlEMI.Controls.Add(Me.txtBankCode)
        Me.pnlEMI.Controls.Add(Me.lblpaymentcode)
        Me.pnlEMI.Controls.Add(Me.lblBankDesc)
        Me.pnlEMI.Location = New System.Drawing.Point(3, 18)
        Me.pnlEMI.Name = "pnlEMI"
        Me.pnlEMI.Size = New System.Drawing.Size(497, 75)
        Me.pnlEMI.TabIndex = 12119
        '
        'dtpPayment
        '
        Me.dtpPayment.CalculationExpression = Nothing
        Me.dtpPayment.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtpPayment.FieldCode = Nothing
        Me.dtpPayment.FieldDesc = Nothing
        Me.dtpPayment.FieldMaxLength = 0
        Me.dtpPayment.FieldName = Nothing
        Me.dtpPayment.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpPayment.isCalculatedField = False
        Me.dtpPayment.IsSourceFromTable = False
        Me.dtpPayment.IsSourceFromValueList = False
        Me.dtpPayment.IsUnique = False
        Me.dtpPayment.Location = New System.Drawing.Point(91, 48)
        Me.dtpPayment.MendatroryField = False
        Me.dtpPayment.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpPayment.MyLinkLable1 = Nothing
        Me.dtpPayment.MyLinkLable2 = Nothing
        Me.dtpPayment.Name = "dtpPayment"
        Me.dtpPayment.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpPayment.ReferenceFieldDesc = Nothing
        Me.dtpPayment.ReferenceFieldName = Nothing
        Me.dtpPayment.ReferenceTableName = Nothing
        Me.dtpPayment.Size = New System.Drawing.Size(83, 20)
        Me.dtpPayment.TabIndex = 25
        Me.dtpPayment.TabStop = False
        Me.dtpPayment.Text = "10/06/2011 11:51 AM"
        Me.dtpPayment.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'lblpaymentdate
        '
        Me.lblpaymentdate.FieldName = Nothing
        Me.lblpaymentdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpaymentdate.Location = New System.Drawing.Point(3, 47)
        Me.lblpaymentdate.Name = "lblpaymentdate"
        Me.lblpaymentdate.Size = New System.Drawing.Size(78, 16)
        Me.lblpaymentdate.TabIndex = 24
        Me.lblpaymentdate.Text = "Payment Date"
        '
        'lblbankcode
        '
        Me.lblbankcode.FieldName = Nothing
        Me.lblbankcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblbankcode.Location = New System.Drawing.Point(3, 3)
        Me.lblbankcode.Name = "lblbankcode"
        Me.lblbankcode.Size = New System.Drawing.Size(62, 16)
        Me.lblbankcode.TabIndex = 13
        Me.lblbankcode.Text = "Bank Code"
        '
        'txtPaymentMode
        '
        Me.txtPaymentMode.CalculationExpression = Nothing
        Me.txtPaymentMode.FieldCode = Nothing
        Me.txtPaymentMode.FieldDesc = Nothing
        Me.txtPaymentMode.FieldMaxLength = 0
        Me.txtPaymentMode.FieldName = Nothing
        Me.txtPaymentMode.isCalculatedField = False
        Me.txtPaymentMode.IsSourceFromTable = False
        Me.txtPaymentMode.IsSourceFromValueList = False
        Me.txtPaymentMode.IsUnique = False
        Me.txtPaymentMode.Location = New System.Drawing.Point(91, 27)
        Me.txtPaymentMode.MendatroryField = True
        Me.txtPaymentMode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPaymentMode.MyLinkLable1 = Nothing
        Me.txtPaymentMode.MyLinkLable2 = Nothing
        Me.txtPaymentMode.MyReadOnly = False
        Me.txtPaymentMode.MyShowMasterFormButton = False
        Me.txtPaymentMode.Name = "txtPaymentMode"
        Me.txtPaymentMode.ReferenceFieldDesc = Nothing
        Me.txtPaymentMode.ReferenceFieldName = Nothing
        Me.txtPaymentMode.ReferenceTableName = Nothing
        Me.txtPaymentMode.Size = New System.Drawing.Size(139, 18)
        Me.txtPaymentMode.TabIndex = 23
        Me.txtPaymentMode.Value = ""
        '
        'txtBankCode
        '
        Me.txtBankCode.CalculationExpression = Nothing
        Me.txtBankCode.FieldCode = Nothing
        Me.txtBankCode.FieldDesc = Nothing
        Me.txtBankCode.FieldMaxLength = 0
        Me.txtBankCode.FieldName = Nothing
        Me.txtBankCode.isCalculatedField = False
        Me.txtBankCode.IsSourceFromTable = False
        Me.txtBankCode.IsSourceFromValueList = False
        Me.txtBankCode.IsUnique = False
        Me.txtBankCode.Location = New System.Drawing.Point(91, 3)
        Me.txtBankCode.MendatroryField = True
        Me.txtBankCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankCode.MyLinkLable1 = Nothing
        Me.txtBankCode.MyLinkLable2 = Nothing
        Me.txtBankCode.MyReadOnly = False
        Me.txtBankCode.MyShowMasterFormButton = False
        Me.txtBankCode.Name = "txtBankCode"
        Me.txtBankCode.ReferenceFieldDesc = Nothing
        Me.txtBankCode.ReferenceFieldName = Nothing
        Me.txtBankCode.ReferenceTableName = Nothing
        Me.txtBankCode.Size = New System.Drawing.Size(139, 19)
        Me.txtBankCode.TabIndex = 14
        Me.txtBankCode.Value = ""
        '
        'lblpaymentcode
        '
        Me.lblpaymentcode.FieldName = Nothing
        Me.lblpaymentcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpaymentcode.Location = New System.Drawing.Point(3, 25)
        Me.lblpaymentcode.Name = "lblpaymentcode"
        Me.lblpaymentcode.Size = New System.Drawing.Size(82, 16)
        Me.lblpaymentcode.TabIndex = 22
        Me.lblpaymentcode.Text = "Payment Mode"
        '
        'lblBankDesc
        '
        Me.lblBankDesc.AutoSize = False
        Me.lblBankDesc.BorderVisible = True
        Me.lblBankDesc.FieldName = Nothing
        Me.lblBankDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBankDesc.Location = New System.Drawing.Point(236, 4)
        Me.lblBankDesc.Name = "lblBankDesc"
        Me.lblBankDesc.Size = New System.Drawing.Size(258, 18)
        Me.lblBankDesc.TabIndex = 15
        Me.lblBankDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'RptMccSaleAdjustment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(817, 421)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "RptMccSaleAdjustment"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Mcc Sale Adjusment Report"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.rbtnVLCWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnMCCWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnAdhoc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSkippedDocument, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbxPaymentCycle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTankerNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMCC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnok, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCreatePayment, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpPaymentDetails.ResumeLayout(False)
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlEMI.ResumeLayout(False)
        Me.pnlEMI.PerformLayout()
        CType(Me.dtpPayment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblpaymentdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblbankcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblpaymentcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBankDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnok As Telerik.WinControls.UI.RadButton
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents txtVSP As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents lblMCC As common.Controls.MyLabel
    Friend WithEvents txtMCC As common.UserControls.txtFinder
    Friend WithEvents lblTankerNo As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents rbtnVLCWise As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnMCCWise As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents cbxPaymentCycle As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents rbtnAdhoc As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents txtPer As common.MyNumBox
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chkSkippedDocument As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadSplitButton1 As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents rmiExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiPDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnCreatePayment As RadButton
    Friend WithEvents RadMenuItem3 As RadMenuItem
    Friend WithEvents GrpPaymentDetails As GroupBox
    Friend WithEvents btnCancel As RadButton
    Friend WithEvents btnSave As RadButton
    Friend WithEvents pnlEMI As Panel
    Friend WithEvents dtpPayment As common.Controls.MyDateTimePicker
    Friend WithEvents lblpaymentdate As common.Controls.MyLabel
    Friend WithEvents lblbankcode As common.Controls.MyLabel
    Friend WithEvents txtPaymentMode As common.UserControls.txtFinder
    Friend WithEvents txtBankCode As common.UserControls.txtFinder
    Friend WithEvents lblpaymentcode As common.Controls.MyLabel
    Friend WithEvents lblBankDesc As common.Controls.MyLabel
End Class

