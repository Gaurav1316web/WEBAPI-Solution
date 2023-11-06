<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDispatchBulkSale
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
        Me.txtTCSTaxRate = New common.MyNumBox()
        Me.MyLabel57 = New common.Controls.MyLabel()
        Me.RadLabel25 = New common.Controls.MyLabel()
        Me.lblTaxAmt = New common.Controls.MyLabel()
        Me.lblActualTCSTaxBaseAmt = New common.Controls.MyLabel()
        Me.MyLabel58 = New common.Controls.MyLabel()
        Me.txttcstaxbaseamount = New common.MyNumBox()
        Me.MyLabel22 = New common.Controls.MyLabel()
        Me.lblDocumentAmount = New common.Controls.MyLabel()
        Me.lblTotRAmt1 = New common.Controls.MyLabel()
        Me.txtsealno = New common.Controls.MyTextBox()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtewaybilldate = New common.Controls.MyDateTimePicker()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.MyLabel21 = New common.Controls.MyLabel()
        Me.TxtEWayBillNo = New common.Controls.MyTextBox()
        Me.MyLabel20 = New common.Controls.MyLabel()
        Me.MyLabel19 = New common.Controls.MyLabel()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.txtinsuranceno = New common.Controls.MyTextBox()
        Me.MyLabel18 = New common.Controls.MyLabel()
        Me.LblQCCode = New common.Controls.MyLabel()
        Me.lblCustomerCode = New common.Controls.MyLabel()
        Me.fndCustomerNo = New common.UserControls.txtFinder()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TxtToleranceinplus = New common.MyNumBox()
        Me.MyLabel17 = New common.Controls.MyLabel()
        Me.txtToleranceinminus = New common.MyNumBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.txtSNFRatio = New common.MyNumBox()
        Me.TxtSNFWeightage = New common.MyNumBox()
        Me.TxtFatWeightage = New common.MyNumBox()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.FndPriceCode = New common.UserControls.txtFinder()
        Me.txtStanadardrate = New common.MyNumBox()
        Me.MyLabel15 = New common.Controls.MyLabel()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.txtfatRatio = New common.MyNumBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.chkCreateAoutoInvoice = New Telerik.WinControls.UI.RadCheckBox()
        Me.lblCustomerName = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.txtCreditLimit = New common.MyNumBox()
        Me.lblCreditLimit = New common.Controls.MyLabel()
        Me.TxtChallanNo = New common.Controls.MyTextBox()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.TxtDipMarking = New common.Controls.MyTextBox()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.FndTankerCode = New common.UserControls.txtFinder()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.TxtTareWeight = New common.MyNumBox()
        Me.txtNetWeight = New common.MyNumBox()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtGrossWeight = New common.MyNumBox()
        Me.LblLocationName = New common.Controls.MyLabel()
        Me.LblTankerName = New common.Controls.MyLabel()
        Me.lblLocationCode = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.fndQcNo = New common.UserControls.txtFinder()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.rbtnTaxCalManual = New common.Controls.MyRadioButton()
        Me.rbtnTaxCalAutomatic = New common.Controls.MyRadioButton()
        Me.txtTaxGroup = New common.UserControls.txtFinder()
        Me.RadLabel11 = New common.Controls.MyLabel()
        Me.lblTaxGrpName = New common.Controls.MyLabel()
        Me.gv2 = New common.UserControls.MyRadGridView()
        Me.Attachments = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RDSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.RDDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.EmailSmsSetting = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnReverseAndUnpost = New Telerik.WinControls.UI.RadButton()
        Me.btnShowInventory = New Telerik.WinControls.UI.RadButton()
        Me.btn_printproforma = New Telerik.WinControls.UI.RadButton()
        Me.btnUpdateCustomer = New Telerik.WinControls.UI.RadButton()
        Me.btnsetting = New Telerik.WinControls.UI.RadSplitButton()
        Me.btnPreview = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnSend = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnSendforApproval = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.txtTCSTaxRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel57, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTaxAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblActualTCSTaxBaseAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel58, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txttcstaxbaseamount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocumentAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotRAmt1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtsealno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.txtewaybilldate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtEWayBillNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.txtinsuranceno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblQCCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomerCode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.lblCustomerCode.SuspendLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.TxtToleranceinplus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToleranceinminus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSNFRatio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtSNFWeightage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtFatWeightage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtStanadardrate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtfatRatio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCreateAoutoInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomerName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCreditLimit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCreditLimit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtChallanNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtDipMarking, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtTareWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNetWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGrossWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblTankerName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.rbtnTaxCalManual, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnTaxCalAutomatic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTaxGrpName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Attachments.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReverseAndUnpost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnShowInventory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_printproforma, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUpdateCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsetting, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReverseAndUnpost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnShowInventory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btn_printproforma)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnUpdateCustomer)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsetting)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(1009, 546)
        Me.SplitContainer1.SplitterDistance = 511
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.Attachments)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageView1.Location = New System.Drawing.Point(0, 20)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1009, 491)
        Me.RadPageView1.TabIndex = 0
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.txtTCSTaxRate)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel57)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel25)
        Me.RadPageViewPage1.Controls.Add(Me.lblTaxAmt)
        Me.RadPageViewPage1.Controls.Add(Me.lblActualTCSTaxBaseAmt)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel58)
        Me.RadPageViewPage1.Controls.Add(Me.txttcstaxbaseamount)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel22)
        Me.RadPageViewPage1.Controls.Add(Me.lblDocumentAmount)
        Me.RadPageViewPage1.Controls.Add(Me.lblTotRAmt1)
        Me.RadPageViewPage1.Controls.Add(Me.txtsealno)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel11)
        Me.RadPageViewPage1.Controls.Add(Me.GroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel19)
        Me.RadPageViewPage1.Controls.Add(Me.RadPanel1)
        Me.RadPageViewPage1.Controls.Add(Me.LblQCCode)
        Me.RadPageViewPage1.Controls.Add(Me.lblCustomerCode)
        Me.RadPageViewPage1.Controls.Add(Me.GroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.chkCreateAoutoInvoice)
        Me.RadPageViewPage1.Controls.Add(Me.lblCustomerName)
        Me.RadPageViewPage1.Controls.Add(Me.UsLock1)
        Me.RadPageViewPage1.Controls.Add(Me.txtCreditLimit)
        Me.RadPageViewPage1.Controls.Add(Me.lblCreditLimit)
        Me.RadPageViewPage1.Controls.Add(Me.TxtChallanNo)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel10)
        Me.RadPageViewPage1.Controls.Add(Me.TxtDipMarking)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel9)
        Me.RadPageViewPage1.Controls.Add(Me.FndTankerCode)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel7)
        Me.RadPageViewPage1.Controls.Add(Me.TxtTareWeight)
        Me.RadPageViewPage1.Controls.Add(Me.txtNetWeight)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel6)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.txtGrossWeight)
        Me.RadPageViewPage1.Controls.Add(Me.LblLocationName)
        Me.RadPageViewPage1.Controls.Add(Me.LblTankerName)
        Me.RadPageViewPage1.Controls.Add(Me.lblLocationCode)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel8)
        Me.RadPageViewPage1.Controls.Add(Me.txtDate)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.fndQcNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtDocNo)
        Me.RadPageViewPage1.Controls.Add(Me.btnAddNew)
        Me.RadPageViewPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(87.0!, 26.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(988, 445)
        Me.RadPageViewPage1.Text = "Dispatch Note"
        '
        'txtTCSTaxRate
        '
        Me.txtTCSTaxRate.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtTCSTaxRate.CalculationExpression = Nothing
        Me.txtTCSTaxRate.DecimalPlaces = 0
        Me.txtTCSTaxRate.FieldCode = Nothing
        Me.txtTCSTaxRate.FieldDesc = Nothing
        Me.txtTCSTaxRate.FieldMaxLength = 0
        Me.txtTCSTaxRate.FieldName = Nothing
        Me.txtTCSTaxRate.isCalculatedField = False
        Me.txtTCSTaxRate.IsSourceFromTable = False
        Me.txtTCSTaxRate.IsSourceFromValueList = False
        Me.txtTCSTaxRate.IsUnique = False
        Me.txtTCSTaxRate.Location = New System.Drawing.Point(260, 198)
        Me.txtTCSTaxRate.MendatroryField = False
        Me.txtTCSTaxRate.MyLinkLable1 = Nothing
        Me.txtTCSTaxRate.MyLinkLable2 = Nothing
        Me.txtTCSTaxRate.Name = "txtTCSTaxRate"
        Me.txtTCSTaxRate.ReferenceFieldDesc = Nothing
        Me.txtTCSTaxRate.ReferenceFieldName = Nothing
        Me.txtTCSTaxRate.ReferenceTableName = Nothing
        Me.txtTCSTaxRate.Size = New System.Drawing.Size(115, 20)
        Me.txtTCSTaxRate.TabIndex = 1405
        Me.txtTCSTaxRate.Text = "0"
        Me.txtTCSTaxRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTCSTaxRate.Value = 0R
        Me.txtTCSTaxRate.Visible = False
        '
        'MyLabel57
        '
        Me.MyLabel57.FieldName = Nothing
        Me.MyLabel57.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel57.Location = New System.Drawing.Point(720, 161)
        Me.MyLabel57.Name = "MyLabel57"
        Me.MyLabel57.Size = New System.Drawing.Size(138, 16)
        Me.MyLabel57.TabIndex = 1404
        Me.MyLabel57.Text = "Actual TCS Tax Base Amt"
        '
        'RadLabel25
        '
        Me.RadLabel25.FieldName = Nothing
        Me.RadLabel25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel25.Location = New System.Drawing.Point(511, 181)
        Me.RadLabel25.Name = "RadLabel25"
        Me.RadLabel25.Size = New System.Drawing.Size(67, 16)
        Me.RadLabel25.TabIndex = 344
        Me.RadLabel25.Text = "Tax Amount"
        '
        'lblTaxAmt
        '
        Me.lblTaxAmt.AutoSize = False
        Me.lblTaxAmt.BorderVisible = True
        Me.lblTaxAmt.FieldName = Nothing
        Me.lblTaxAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTaxAmt.Location = New System.Drawing.Point(594, 181)
        Me.lblTaxAmt.Name = "lblTaxAmt"
        Me.lblTaxAmt.Size = New System.Drawing.Size(122, 18)
        Me.lblTaxAmt.TabIndex = 343
        Me.lblTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblActualTCSTaxBaseAmt
        '
        Me.lblActualTCSTaxBaseAmt.AutoSize = False
        Me.lblActualTCSTaxBaseAmt.BorderVisible = True
        Me.lblActualTCSTaxBaseAmt.FieldName = Nothing
        Me.lblActualTCSTaxBaseAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblActualTCSTaxBaseAmt.Location = New System.Drawing.Point(864, 159)
        Me.lblActualTCSTaxBaseAmt.Name = "lblActualTCSTaxBaseAmt"
        Me.lblActualTCSTaxBaseAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblActualTCSTaxBaseAmt.TabIndex = 1403
        Me.lblActualTCSTaxBaseAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel58
        '
        Me.MyLabel58.FieldName = Nothing
        Me.MyLabel58.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel58.Location = New System.Drawing.Point(731, 185)
        Me.MyLabel58.Name = "MyLabel58"
        Me.MyLabel58.Size = New System.Drawing.Size(122, 16)
        Me.MyLabel58.TabIndex = 1402
        Me.MyLabel58.Text = "TCS Tax Base Amount"
        '
        'txttcstaxbaseamount
        '
        Me.txttcstaxbaseamount.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txttcstaxbaseamount.CalculationExpression = Nothing
        Me.txttcstaxbaseamount.DecimalPlaces = 0
        Me.txttcstaxbaseamount.FieldCode = Nothing
        Me.txttcstaxbaseamount.FieldDesc = Nothing
        Me.txttcstaxbaseamount.FieldMaxLength = 0
        Me.txttcstaxbaseamount.FieldName = Nothing
        Me.txttcstaxbaseamount.isCalculatedField = False
        Me.txttcstaxbaseamount.IsSourceFromTable = False
        Me.txttcstaxbaseamount.IsSourceFromValueList = False
        Me.txttcstaxbaseamount.IsUnique = False
        Me.txttcstaxbaseamount.Location = New System.Drawing.Point(864, 181)
        Me.txttcstaxbaseamount.MendatroryField = False
        Me.txttcstaxbaseamount.MyLinkLable1 = Nothing
        Me.txttcstaxbaseamount.MyLinkLable2 = Nothing
        Me.txttcstaxbaseamount.Name = "txttcstaxbaseamount"
        Me.txttcstaxbaseamount.ReferenceFieldDesc = Nothing
        Me.txttcstaxbaseamount.ReferenceFieldName = Nothing
        Me.txttcstaxbaseamount.ReferenceTableName = Nothing
        Me.txttcstaxbaseamount.Size = New System.Drawing.Size(115, 20)
        Me.txttcstaxbaseamount.TabIndex = 1401
        Me.txttcstaxbaseamount.Text = "0"
        Me.txttcstaxbaseamount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txttcstaxbaseamount.Value = 0R
        '
        'MyLabel22
        '
        Me.MyLabel22.AutoSize = False
        Me.MyLabel22.FieldName = Nothing
        Me.MyLabel22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel22.Location = New System.Drawing.Point(511, 198)
        Me.MyLabel22.Name = "MyLabel22"
        Me.MyLabel22.Size = New System.Drawing.Size(84, 17)
        Me.MyLabel22.TabIndex = 346
        Me.MyLabel22.Text = "Total Amount"
        '
        'lblDocumentAmount
        '
        Me.lblDocumentAmount.AutoSize = False
        Me.lblDocumentAmount.BorderVisible = True
        Me.lblDocumentAmount.FieldName = Nothing
        Me.lblDocumentAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocumentAmount.Location = New System.Drawing.Point(594, 159)
        Me.lblDocumentAmount.Name = "lblDocumentAmount"
        Me.lblDocumentAmount.Size = New System.Drawing.Size(123, 20)
        Me.lblDocumentAmount.TabIndex = 345
        Me.lblDocumentAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotRAmt1
        '
        Me.lblTotRAmt1.AutoSize = False
        Me.lblTotRAmt1.BorderVisible = True
        Me.lblTotRAmt1.FieldName = Nothing
        Me.lblTotRAmt1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotRAmt1.Location = New System.Drawing.Point(594, 199)
        Me.lblTotRAmt1.Name = "lblTotRAmt1"
        Me.lblTotRAmt1.Size = New System.Drawing.Size(124, 20)
        Me.lblTotRAmt1.TabIndex = 331
        Me.lblTotRAmt1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtsealno
        '
        Me.txtsealno.CalculationExpression = Nothing
        Me.txtsealno.FieldCode = Nothing
        Me.txtsealno.FieldDesc = Nothing
        Me.txtsealno.FieldMaxLength = 0
        Me.txtsealno.FieldName = Nothing
        Me.txtsealno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsealno.isCalculatedField = False
        Me.txtsealno.IsSourceFromTable = False
        Me.txtsealno.IsSourceFromValueList = False
        Me.txtsealno.IsUnique = False
        Me.txtsealno.Location = New System.Drawing.Point(89, 161)
        Me.txtsealno.MaxLength = 200
        Me.txtsealno.MendatroryField = True
        Me.txtsealno.MyLinkLable1 = Nothing
        Me.txtsealno.MyLinkLable2 = Nothing
        Me.txtsealno.Name = "txtsealno"
        Me.txtsealno.ReferenceFieldDesc = Nothing
        Me.txtsealno.ReferenceFieldName = Nothing
        Me.txtsealno.ReferenceTableName = Nothing
        Me.txtsealno.Size = New System.Drawing.Size(126, 18)
        Me.txtsealno.TabIndex = 331
        '
        'MyLabel11
        '
        Me.MyLabel11.AutoSize = False
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel11.Location = New System.Drawing.Point(511, 154)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(67, 25)
        Me.MyLabel11.TabIndex = 332
        Me.MyLabel11.Text = "Document Amount"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtewaybilldate)
        Me.GroupBox2.Controls.Add(Me.MyLabel21)
        Me.GroupBox2.Controls.Add(Me.TxtEWayBillNo)
        Me.GroupBox2.Controls.Add(Me.MyLabel20)
        Me.GroupBox2.Location = New System.Drawing.Point(262, 136)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(245, 56)
        Me.GroupBox2.TabIndex = 342
        Me.GroupBox2.TabStop = False
        '
        'txtewaybilldate
        '
        Me.txtewaybilldate.CalculationExpression = Nothing
        Me.txtewaybilldate.CustomFormat = "dd/MM/yyyy"
        Me.txtewaybilldate.FieldCode = Nothing
        Me.txtewaybilldate.FieldDesc = Nothing
        Me.txtewaybilldate.FieldMaxLength = 0
        Me.txtewaybilldate.FieldName = Nothing
        Me.txtewaybilldate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtewaybilldate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtewaybilldate.isCalculatedField = False
        Me.txtewaybilldate.IsSourceFromTable = False
        Me.txtewaybilldate.IsSourceFromValueList = False
        Me.txtewaybilldate.IsUnique = False
        Me.txtewaybilldate.Location = New System.Drawing.Point(97, 32)
        Me.txtewaybilldate.MendatroryField = False
        Me.txtewaybilldate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtewaybilldate.MyLinkLable1 = Me.RadLabel4
        Me.txtewaybilldate.MyLinkLable2 = Nothing
        Me.txtewaybilldate.Name = "txtewaybilldate"
        Me.txtewaybilldate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtewaybilldate.ReferenceFieldDesc = Nothing
        Me.txtewaybilldate.ReferenceFieldName = Nothing
        Me.txtewaybilldate.ReferenceTableName = Nothing
        Me.txtewaybilldate.ShowCheckBox = True
        Me.txtewaybilldate.Size = New System.Drawing.Size(146, 18)
        Me.txtewaybilldate.TabIndex = 337
        Me.txtewaybilldate.TabStop = False
        Me.txtewaybilldate.Text = "13/06/2011"
        Me.txtewaybilldate.Value = New Date(2011, 6, 13, 0, 0, 0, 0)
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(375, 2)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 40
        Me.RadLabel4.Text = "Date"
        '
        'MyLabel21
        '
        Me.MyLabel21.FieldName = Nothing
        Me.MyLabel21.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel21.Location = New System.Drawing.Point(8, 35)
        Me.MyLabel21.Name = "MyLabel21"
        Me.MyLabel21.Size = New System.Drawing.Size(82, 16)
        Me.MyLabel21.TabIndex = 336
        Me.MyLabel21.Text = "E-WayBill Date"
        '
        'TxtEWayBillNo
        '
        Me.TxtEWayBillNo.CalculationExpression = Nothing
        Me.TxtEWayBillNo.FieldCode = Nothing
        Me.TxtEWayBillNo.FieldDesc = Nothing
        Me.TxtEWayBillNo.FieldMaxLength = 0
        Me.TxtEWayBillNo.FieldName = Nothing
        Me.TxtEWayBillNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtEWayBillNo.isCalculatedField = False
        Me.TxtEWayBillNo.IsSourceFromTable = False
        Me.TxtEWayBillNo.IsSourceFromValueList = False
        Me.TxtEWayBillNo.IsUnique = False
        Me.TxtEWayBillNo.Location = New System.Drawing.Point(96, 10)
        Me.TxtEWayBillNo.MaxLength = 50
        Me.TxtEWayBillNo.MendatroryField = False
        Me.TxtEWayBillNo.MyLinkLable1 = Nothing
        Me.TxtEWayBillNo.MyLinkLable2 = Nothing
        Me.TxtEWayBillNo.Name = "TxtEWayBillNo"
        Me.TxtEWayBillNo.ReferenceFieldDesc = Nothing
        Me.TxtEWayBillNo.ReferenceFieldName = Nothing
        Me.TxtEWayBillNo.ReferenceTableName = Nothing
        Me.TxtEWayBillNo.Size = New System.Drawing.Size(146, 18)
        Me.TxtEWayBillNo.TabIndex = 331
        '
        'MyLabel20
        '
        Me.MyLabel20.FieldName = Nothing
        Me.MyLabel20.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel20.Location = New System.Drawing.Point(7, 11)
        Me.MyLabel20.Name = "MyLabel20"
        Me.MyLabel20.Size = New System.Drawing.Size(76, 16)
        Me.MyLabel20.TabIndex = 332
        Me.MyLabel20.Text = "E-Way Bill No"
        '
        'MyLabel19
        '
        Me.MyLabel19.FieldName = Nothing
        Me.MyLabel19.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel19.Location = New System.Drawing.Point(12, 161)
        Me.MyLabel19.Name = "MyLabel19"
        Me.MyLabel19.Size = New System.Drawing.Size(46, 16)
        Me.MyLabel19.TabIndex = 332
        Me.MyLabel19.Text = "Seal No"
        '
        'RadPanel1
        '
        Me.RadPanel1.Controls.Add(Me.txtinsuranceno)
        Me.RadPanel1.Controls.Add(Me.MyLabel18)
        Me.RadPanel1.Location = New System.Drawing.Point(7, 136)
        Me.RadPanel1.Name = "RadPanel1"
        '
        '
        '
        Me.RadPanel1.RootElement.ClipDrawing = True
        Me.RadPanel1.Size = New System.Drawing.Size(240, 22)
        Me.RadPanel1.TabIndex = 341
        '
        'txtinsuranceno
        '
        Me.txtinsuranceno.CalculationExpression = Nothing
        Me.txtinsuranceno.FieldCode = Nothing
        Me.txtinsuranceno.FieldDesc = Nothing
        Me.txtinsuranceno.FieldMaxLength = 0
        Me.txtinsuranceno.FieldName = Nothing
        Me.txtinsuranceno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtinsuranceno.isCalculatedField = False
        Me.txtinsuranceno.IsSourceFromTable = False
        Me.txtinsuranceno.IsSourceFromValueList = False
        Me.txtinsuranceno.IsUnique = False
        Me.txtinsuranceno.Location = New System.Drawing.Point(83, 2)
        Me.txtinsuranceno.MaxLength = 200
        Me.txtinsuranceno.MendatroryField = True
        Me.txtinsuranceno.MyLinkLable1 = Nothing
        Me.txtinsuranceno.MyLinkLable2 = Nothing
        Me.txtinsuranceno.Name = "txtinsuranceno"
        Me.txtinsuranceno.ReferenceFieldDesc = Nothing
        Me.txtinsuranceno.ReferenceFieldName = Nothing
        Me.txtinsuranceno.ReferenceTableName = Nothing
        Me.txtinsuranceno.Size = New System.Drawing.Size(125, 18)
        Me.txtinsuranceno.TabIndex = 329
        '
        'MyLabel18
        '
        Me.MyLabel18.FieldName = Nothing
        Me.MyLabel18.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel18.Location = New System.Drawing.Point(3, 3)
        Me.MyLabel18.Name = "MyLabel18"
        Me.MyLabel18.Size = New System.Drawing.Size(74, 16)
        Me.MyLabel18.TabIndex = 330
        Me.MyLabel18.Text = "Insurance No"
        '
        'LblQCCode
        '
        Me.LblQCCode.AutoSize = False
        Me.LblQCCode.BorderVisible = True
        Me.LblQCCode.FieldName = Nothing
        Me.LblQCCode.Location = New System.Drawing.Point(90, 70)
        Me.LblQCCode.Name = "LblQCCode"
        Me.LblQCCode.Size = New System.Drawing.Size(146, 20)
        Me.LblQCCode.TabIndex = 340
        '
        'lblCustomerCode
        '
        Me.lblCustomerCode.AutoSize = False
        Me.lblCustomerCode.BorderVisible = True
        Me.lblCustomerCode.Controls.Add(Me.fndCustomerNo)
        Me.lblCustomerCode.FieldName = Nothing
        Me.lblCustomerCode.Location = New System.Drawing.Point(90, 23)
        Me.lblCustomerCode.Name = "lblCustomerCode"
        Me.lblCustomerCode.Size = New System.Drawing.Size(146, 20)
        Me.lblCustomerCode.TabIndex = 339
        '
        'fndCustomerNo
        '
        Me.fndCustomerNo.CalculationExpression = Nothing
        Me.fndCustomerNo.Enabled = False
        Me.fndCustomerNo.FieldCode = Nothing
        Me.fndCustomerNo.FieldDesc = Nothing
        Me.fndCustomerNo.FieldMaxLength = 0
        Me.fndCustomerNo.FieldName = Nothing
        Me.fndCustomerNo.isCalculatedField = False
        Me.fndCustomerNo.IsSourceFromTable = False
        Me.fndCustomerNo.IsSourceFromValueList = False
        Me.fndCustomerNo.IsUnique = False
        Me.fndCustomerNo.Location = New System.Drawing.Point(0, 0)
        Me.fndCustomerNo.MendatroryField = True
        Me.fndCustomerNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndCustomerNo.MyLinkLable1 = Me.RadLabel2
        Me.fndCustomerNo.MyLinkLable2 = Nothing
        Me.fndCustomerNo.MyReadOnly = False
        Me.fndCustomerNo.MyShowMasterFormButton = False
        Me.fndCustomerNo.Name = "fndCustomerNo"
        Me.fndCustomerNo.ReferenceFieldDesc = Nothing
        Me.fndCustomerNo.ReferenceFieldName = Nothing
        Me.fndCustomerNo.ReferenceTableName = Nothing
        Me.fndCustomerNo.Size = New System.Drawing.Size(146, 20)
        Me.fndCustomerNo.TabIndex = 2
        Me.fndCustomerNo.Value = ""
        Me.fndCustomerNo.Visible = False
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(10, 24)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(73, 16)
        Me.RadLabel2.TabIndex = 38
        Me.RadLabel2.Text = "Customer No"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TxtToleranceinplus)
        Me.GroupBox1.Controls.Add(Me.MyLabel17)
        Me.GroupBox1.Controls.Add(Me.txtToleranceinminus)
        Me.GroupBox1.Controls.Add(Me.MyLabel4)
        Me.GroupBox1.Controls.Add(Me.MyLabel12)
        Me.GroupBox1.Controls.Add(Me.txtSNFRatio)
        Me.GroupBox1.Controls.Add(Me.TxtSNFWeightage)
        Me.GroupBox1.Controls.Add(Me.TxtFatWeightage)
        Me.GroupBox1.Controls.Add(Me.MyLabel13)
        Me.GroupBox1.Controls.Add(Me.MyLabel14)
        Me.GroupBox1.Controls.Add(Me.FndPriceCode)
        Me.GroupBox1.Controls.Add(Me.txtStanadardrate)
        Me.GroupBox1.Controls.Add(Me.MyLabel15)
        Me.GroupBox1.Controls.Add(Me.MyLabel16)
        Me.GroupBox1.Controls.Add(Me.txtfatRatio)
        Me.GroupBox1.Controls.Add(Me.MyLabel1)
        Me.GroupBox1.Location = New System.Drawing.Point(538, 25)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(346, 130)
        Me.GroupBox1.TabIndex = 338
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Price Detail"
        '
        'TxtToleranceinplus
        '
        Me.TxtToleranceinplus.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtToleranceinplus.CalculationExpression = Nothing
        Me.TxtToleranceinplus.DecimalPlaces = 2
        Me.TxtToleranceinplus.Enabled = False
        Me.TxtToleranceinplus.FieldCode = Nothing
        Me.TxtToleranceinplus.FieldDesc = Nothing
        Me.TxtToleranceinplus.FieldMaxLength = 0
        Me.TxtToleranceinplus.FieldName = Nothing
        Me.TxtToleranceinplus.isCalculatedField = False
        Me.TxtToleranceinplus.IsSourceFromTable = False
        Me.TxtToleranceinplus.IsSourceFromValueList = False
        Me.TxtToleranceinplus.IsUnique = False
        Me.TxtToleranceinplus.Location = New System.Drawing.Point(97, 81)
        Me.TxtToleranceinplus.MendatroryField = True
        Me.TxtToleranceinplus.MyLinkLable1 = Nothing
        Me.TxtToleranceinplus.MyLinkLable2 = Nothing
        Me.TxtToleranceinplus.Name = "TxtToleranceinplus"
        Me.TxtToleranceinplus.ReferenceFieldDesc = Nothing
        Me.TxtToleranceinplus.ReferenceFieldName = Nothing
        Me.TxtToleranceinplus.ReferenceTableName = Nothing
        Me.TxtToleranceinplus.Size = New System.Drawing.Size(69, 20)
        Me.TxtToleranceinplus.TabIndex = 347
        Me.TxtToleranceinplus.Text = "0"
        Me.TxtToleranceinplus.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtToleranceinplus.Value = 0R
        '
        'MyLabel17
        '
        Me.MyLabel17.FieldName = Nothing
        Me.MyLabel17.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel17.Location = New System.Drawing.Point(7, 82)
        Me.MyLabel17.Name = "MyLabel17"
        Me.MyLabel17.Size = New System.Drawing.Size(84, 16)
        Me.MyLabel17.TabIndex = 348
        Me.MyLabel17.Text = "Tolerance %(+)"
        '
        'txtToleranceinminus
        '
        Me.txtToleranceinminus.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtToleranceinminus.CalculationExpression = Nothing
        Me.txtToleranceinminus.DecimalPlaces = 2
        Me.txtToleranceinminus.Enabled = False
        Me.txtToleranceinminus.FieldCode = Nothing
        Me.txtToleranceinminus.FieldDesc = Nothing
        Me.txtToleranceinminus.FieldMaxLength = 0
        Me.txtToleranceinminus.FieldName = Nothing
        Me.txtToleranceinminus.isCalculatedField = False
        Me.txtToleranceinminus.IsSourceFromTable = False
        Me.txtToleranceinminus.IsSourceFromValueList = False
        Me.txtToleranceinminus.IsUnique = False
        Me.txtToleranceinminus.Location = New System.Drawing.Point(263, 81)
        Me.txtToleranceinminus.MendatroryField = True
        Me.txtToleranceinminus.MyLinkLable1 = Nothing
        Me.txtToleranceinminus.MyLinkLable2 = Nothing
        Me.txtToleranceinminus.Name = "txtToleranceinminus"
        Me.txtToleranceinminus.ReferenceFieldDesc = Nothing
        Me.txtToleranceinminus.ReferenceFieldName = Nothing
        Me.txtToleranceinminus.ReferenceTableName = Nothing
        Me.txtToleranceinminus.Size = New System.Drawing.Size(66, 20)
        Me.txtToleranceinminus.TabIndex = 345
        Me.txtToleranceinminus.Text = "0"
        Me.txtToleranceinminus.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtToleranceinminus.Value = 0R
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel4.Location = New System.Drawing.Point(171, 82)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(81, 16)
        Me.MyLabel4.TabIndex = 346
        Me.MyLabel4.Text = "Tolerance %(-)"
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel12.Location = New System.Drawing.Point(170, 60)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(58, 16)
        Me.MyLabel12.TabIndex = 344
        Me.MyLabel12.Text = "SNF Ratio"
        '
        'txtSNFRatio
        '
        Me.txtSNFRatio.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtSNFRatio.CalculationExpression = Nothing
        Me.txtSNFRatio.DecimalPlaces = 2
        Me.txtSNFRatio.Enabled = False
        Me.txtSNFRatio.FieldCode = Nothing
        Me.txtSNFRatio.FieldDesc = Nothing
        Me.txtSNFRatio.FieldMaxLength = 0
        Me.txtSNFRatio.FieldName = Nothing
        Me.txtSNFRatio.isCalculatedField = False
        Me.txtSNFRatio.IsSourceFromTable = False
        Me.txtSNFRatio.IsSourceFromValueList = False
        Me.txtSNFRatio.IsUnique = False
        Me.txtSNFRatio.Location = New System.Drawing.Point(263, 58)
        Me.txtSNFRatio.MendatroryField = True
        Me.txtSNFRatio.MyLinkLable1 = Nothing
        Me.txtSNFRatio.MyLinkLable2 = Nothing
        Me.txtSNFRatio.Name = "txtSNFRatio"
        Me.txtSNFRatio.ReferenceFieldDesc = Nothing
        Me.txtSNFRatio.ReferenceFieldName = Nothing
        Me.txtSNFRatio.ReferenceTableName = Nothing
        Me.txtSNFRatio.Size = New System.Drawing.Size(66, 20)
        Me.txtSNFRatio.TabIndex = 343
        Me.txtSNFRatio.Text = "0"
        Me.txtSNFRatio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSNFRatio.Value = 0R
        '
        'TxtSNFWeightage
        '
        Me.TxtSNFWeightage.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtSNFWeightage.CalculationExpression = Nothing
        Me.TxtSNFWeightage.DecimalPlaces = 2
        Me.TxtSNFWeightage.Enabled = False
        Me.TxtSNFWeightage.FieldCode = Nothing
        Me.TxtSNFWeightage.FieldDesc = Nothing
        Me.TxtSNFWeightage.FieldMaxLength = 0
        Me.TxtSNFWeightage.FieldName = Nothing
        Me.TxtSNFWeightage.isCalculatedField = False
        Me.TxtSNFWeightage.IsSourceFromTable = False
        Me.TxtSNFWeightage.IsSourceFromValueList = False
        Me.TxtSNFWeightage.IsUnique = False
        Me.TxtSNFWeightage.Location = New System.Drawing.Point(263, 35)
        Me.TxtSNFWeightage.MendatroryField = True
        Me.TxtSNFWeightage.MyLinkLable1 = Nothing
        Me.TxtSNFWeightage.MyLinkLable2 = Nothing
        Me.TxtSNFWeightage.Name = "TxtSNFWeightage"
        Me.TxtSNFWeightage.ReferenceFieldDesc = Nothing
        Me.TxtSNFWeightage.ReferenceFieldName = Nothing
        Me.TxtSNFWeightage.ReferenceTableName = Nothing
        Me.TxtSNFWeightage.Size = New System.Drawing.Size(66, 20)
        Me.TxtSNFWeightage.TabIndex = 336
        Me.TxtSNFWeightage.Text = "0"
        Me.TxtSNFWeightage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtSNFWeightage.Value = 0R
        '
        'TxtFatWeightage
        '
        Me.TxtFatWeightage.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtFatWeightage.CalculationExpression = Nothing
        Me.TxtFatWeightage.DecimalPlaces = 2
        Me.TxtFatWeightage.Enabled = False
        Me.TxtFatWeightage.FieldCode = Nothing
        Me.TxtFatWeightage.FieldDesc = Nothing
        Me.TxtFatWeightage.FieldMaxLength = 0
        Me.TxtFatWeightage.FieldName = Nothing
        Me.TxtFatWeightage.isCalculatedField = False
        Me.TxtFatWeightage.IsSourceFromTable = False
        Me.TxtFatWeightage.IsSourceFromValueList = False
        Me.TxtFatWeightage.IsUnique = False
        Me.TxtFatWeightage.Location = New System.Drawing.Point(97, 35)
        Me.TxtFatWeightage.MendatroryField = True
        Me.TxtFatWeightage.MyLinkLable1 = Nothing
        Me.TxtFatWeightage.MyLinkLable2 = Nothing
        Me.TxtFatWeightage.Name = "TxtFatWeightage"
        Me.TxtFatWeightage.ReferenceFieldDesc = Nothing
        Me.TxtFatWeightage.ReferenceFieldName = Nothing
        Me.TxtFatWeightage.ReferenceTableName = Nothing
        Me.TxtFatWeightage.Size = New System.Drawing.Size(68, 20)
        Me.TxtFatWeightage.TabIndex = 335
        Me.TxtFatWeightage.Text = "0"
        Me.TxtFatWeightage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtFatWeightage.Value = 0R
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel13.Location = New System.Drawing.Point(168, 37)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(86, 16)
        Me.MyLabel13.TabIndex = 342
        Me.MyLabel13.Text = "SNF Weightage"
        '
        'MyLabel14
        '
        Me.MyLabel14.FieldName = Nothing
        Me.MyLabel14.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel14.Location = New System.Drawing.Point(7, 60)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(57, 16)
        Me.MyLabel14.TabIndex = 340
        Me.MyLabel14.Text = "FAT Ratio"
        '
        'FndPriceCode
        '
        Me.FndPriceCode.CalculationExpression = Nothing
        Me.FndPriceCode.FieldCode = Nothing
        Me.FndPriceCode.FieldDesc = Nothing
        Me.FndPriceCode.FieldMaxLength = 0
        Me.FndPriceCode.FieldName = Nothing
        Me.FndPriceCode.isCalculatedField = False
        Me.FndPriceCode.IsSourceFromTable = False
        Me.FndPriceCode.IsSourceFromValueList = False
        Me.FndPriceCode.IsUnique = False
        Me.FndPriceCode.Location = New System.Drawing.Point(96, 12)
        Me.FndPriceCode.MendatroryField = True
        Me.FndPriceCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndPriceCode.MyLinkLable1 = Nothing
        Me.FndPriceCode.MyLinkLable2 = Nothing
        Me.FndPriceCode.MyReadOnly = False
        Me.FndPriceCode.MyShowMasterFormButton = False
        Me.FndPriceCode.Name = "FndPriceCode"
        Me.FndPriceCode.ReferenceFieldDesc = Nothing
        Me.FndPriceCode.ReferenceFieldName = Nothing
        Me.FndPriceCode.ReferenceTableName = Nothing
        Me.FndPriceCode.Size = New System.Drawing.Size(233, 20)
        Me.FndPriceCode.TabIndex = 12
        Me.FndPriceCode.Value = ""
        '
        'txtStanadardrate
        '
        Me.txtStanadardrate.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtStanadardrate.CalculationExpression = Nothing
        Me.txtStanadardrate.DecimalPlaces = 2
        Me.txtStanadardrate.FieldCode = Nothing
        Me.txtStanadardrate.FieldDesc = Nothing
        Me.txtStanadardrate.FieldMaxLength = 0
        Me.txtStanadardrate.FieldName = Nothing
        Me.txtStanadardrate.isCalculatedField = False
        Me.txtStanadardrate.IsSourceFromTable = False
        Me.txtStanadardrate.IsSourceFromValueList = False
        Me.txtStanadardrate.IsUnique = False
        Me.txtStanadardrate.Location = New System.Drawing.Point(97, 104)
        Me.txtStanadardrate.MendatroryField = True
        Me.txtStanadardrate.MyLinkLable1 = Nothing
        Me.txtStanadardrate.MyLinkLable2 = Nothing
        Me.txtStanadardrate.Name = "txtStanadardrate"
        Me.txtStanadardrate.ReferenceFieldDesc = Nothing
        Me.txtStanadardrate.ReferenceFieldName = Nothing
        Me.txtStanadardrate.ReferenceTableName = Nothing
        Me.txtStanadardrate.Size = New System.Drawing.Size(69, 20)
        Me.txtStanadardrate.TabIndex = 338
        Me.txtStanadardrate.Text = "0"
        Me.txtStanadardrate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtStanadardrate.Value = 0R
        '
        'MyLabel15
        '
        Me.MyLabel15.FieldName = Nothing
        Me.MyLabel15.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel15.Location = New System.Drawing.Point(7, 105)
        Me.MyLabel15.Name = "MyLabel15"
        Me.MyLabel15.Size = New System.Drawing.Size(79, 16)
        Me.MyLabel15.TabIndex = 341
        Me.MyLabel15.Text = "Standard Rate"
        '
        'MyLabel16
        '
        Me.MyLabel16.FieldName = Nothing
        Me.MyLabel16.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel16.Location = New System.Drawing.Point(7, 37)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(85, 16)
        Me.MyLabel16.TabIndex = 339
        Me.MyLabel16.Text = "FAT Weightage"
        '
        'txtfatRatio
        '
        Me.txtfatRatio.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtfatRatio.CalculationExpression = Nothing
        Me.txtfatRatio.DecimalPlaces = 2
        Me.txtfatRatio.Enabled = False
        Me.txtfatRatio.FieldCode = Nothing
        Me.txtfatRatio.FieldDesc = Nothing
        Me.txtfatRatio.FieldMaxLength = 0
        Me.txtfatRatio.FieldName = Nothing
        Me.txtfatRatio.isCalculatedField = False
        Me.txtfatRatio.IsSourceFromTable = False
        Me.txtfatRatio.IsSourceFromValueList = False
        Me.txtfatRatio.IsUnique = False
        Me.txtfatRatio.Location = New System.Drawing.Point(97, 58)
        Me.txtfatRatio.MendatroryField = True
        Me.txtfatRatio.MyLinkLable1 = Nothing
        Me.txtfatRatio.MyLinkLable2 = Nothing
        Me.txtfatRatio.Name = "txtfatRatio"
        Me.txtfatRatio.ReferenceFieldDesc = Nothing
        Me.txtfatRatio.ReferenceFieldName = Nothing
        Me.txtfatRatio.ReferenceTableName = Nothing
        Me.txtfatRatio.Size = New System.Drawing.Size(69, 20)
        Me.txtfatRatio.TabIndex = 337
        Me.txtfatRatio.Text = "0"
        Me.txtfatRatio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtfatRatio.Value = 0R
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(7, 14)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel1.TabIndex = 321
        Me.MyLabel1.Text = "Price Chart"
        '
        'chkCreateAoutoInvoice
        '
        Me.chkCreateAoutoInvoice.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCreateAoutoInvoice.Location = New System.Drawing.Point(649, 5)
        Me.chkCreateAoutoInvoice.Name = "chkCreateAoutoInvoice"
        Me.chkCreateAoutoInvoice.Size = New System.Drawing.Size(120, 16)
        Me.chkCreateAoutoInvoice.TabIndex = 336
        Me.chkCreateAoutoInvoice.Text = "Create Auto Invoice"
        '
        'lblCustomerName
        '
        Me.lblCustomerName.AutoSize = False
        Me.lblCustomerName.BorderVisible = True
        Me.lblCustomerName.FieldName = Nothing
        Me.lblCustomerName.Location = New System.Drawing.Point(241, 23)
        Me.lblCustomerName.Name = "lblCustomerName"
        Me.lblCustomerName.Size = New System.Drawing.Size(291, 20)
        Me.lblCustomerName.TabIndex = 335
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(551, 0)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(92, 22)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 334
        '
        'txtCreditLimit
        '
        Me.txtCreditLimit.BackColor = System.Drawing.Color.White
        Me.txtCreditLimit.CalculationExpression = Nothing
        Me.txtCreditLimit.DecimalPlaces = 5
        Me.txtCreditLimit.Enabled = False
        Me.txtCreditLimit.FieldCode = Nothing
        Me.txtCreditLimit.FieldDesc = Nothing
        Me.txtCreditLimit.FieldMaxLength = 0
        Me.txtCreditLimit.FieldName = Nothing
        Me.txtCreditLimit.isCalculatedField = False
        Me.txtCreditLimit.IsSourceFromTable = False
        Me.txtCreditLimit.IsSourceFromValueList = False
        Me.txtCreditLimit.IsUnique = False
        Me.txtCreditLimit.Location = New System.Drawing.Point(89, 205)
        Me.txtCreditLimit.MendatroryField = False
        Me.txtCreditLimit.MyLinkLable1 = Nothing
        Me.txtCreditLimit.MyLinkLable2 = Nothing
        Me.txtCreditLimit.Name = "txtCreditLimit"
        Me.txtCreditLimit.ReferenceFieldDesc = Nothing
        Me.txtCreditLimit.ReferenceFieldName = Nothing
        Me.txtCreditLimit.ReferenceTableName = Nothing
        Me.txtCreditLimit.Size = New System.Drawing.Size(90, 20)
        Me.txtCreditLimit.TabIndex = 8
        Me.txtCreditLimit.Text = "0"
        Me.txtCreditLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtCreditLimit.Value = 0R
        '
        'lblCreditLimit
        '
        Me.lblCreditLimit.FieldName = Nothing
        Me.lblCreditLimit.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCreditLimit.Location = New System.Drawing.Point(12, 207)
        Me.lblCreditLimit.Name = "lblCreditLimit"
        Me.lblCreditLimit.Size = New System.Drawing.Size(63, 16)
        Me.lblCreditLimit.TabIndex = 333
        Me.lblCreditLimit.Text = "Credit Limit"
        '
        'TxtChallanNo
        '
        Me.TxtChallanNo.CalculationExpression = Nothing
        Me.TxtChallanNo.FieldCode = Nothing
        Me.TxtChallanNo.FieldDesc = Nothing
        Me.TxtChallanNo.FieldMaxLength = 0
        Me.TxtChallanNo.FieldName = Nothing
        Me.TxtChallanNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtChallanNo.isCalculatedField = False
        Me.TxtChallanNo.IsSourceFromTable = False
        Me.TxtChallanNo.IsSourceFromValueList = False
        Me.TxtChallanNo.IsUnique = False
        Me.TxtChallanNo.Location = New System.Drawing.Point(90, 183)
        Me.TxtChallanNo.MaxLength = 200
        Me.TxtChallanNo.MendatroryField = False
        Me.TxtChallanNo.MyLinkLable1 = Nothing
        Me.TxtChallanNo.MyLinkLable2 = Nothing
        Me.TxtChallanNo.Name = "TxtChallanNo"
        Me.TxtChallanNo.ReferenceFieldDesc = Nothing
        Me.TxtChallanNo.ReferenceFieldName = Nothing
        Me.TxtChallanNo.ReferenceTableName = Nothing
        Me.TxtChallanNo.Size = New System.Drawing.Size(90, 18)
        Me.TxtChallanNo.TabIndex = 5
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel10.Location = New System.Drawing.Point(11, 184)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(62, 16)
        Me.MyLabel10.TabIndex = 328
        Me.MyLabel10.Text = "Challan No"
        '
        'TxtDipMarking
        '
        Me.TxtDipMarking.CalculationExpression = Nothing
        Me.TxtDipMarking.FieldCode = Nothing
        Me.TxtDipMarking.FieldDesc = Nothing
        Me.TxtDipMarking.FieldMaxLength = 0
        Me.TxtDipMarking.FieldName = Nothing
        Me.TxtDipMarking.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDipMarking.isCalculatedField = False
        Me.TxtDipMarking.IsSourceFromTable = False
        Me.TxtDipMarking.IsSourceFromValueList = False
        Me.TxtDipMarking.IsUnique = False
        Me.TxtDipMarking.Location = New System.Drawing.Point(349, 70)
        Me.TxtDipMarking.MaxLength = 200
        Me.TxtDipMarking.MendatroryField = False
        Me.TxtDipMarking.MyLinkLable1 = Nothing
        Me.TxtDipMarking.MyLinkLable2 = Nothing
        Me.TxtDipMarking.Name = "TxtDipMarking"
        Me.TxtDipMarking.ReferenceFieldDesc = Nothing
        Me.TxtDipMarking.ReferenceFieldName = Nothing
        Me.TxtDipMarking.ReferenceTableName = Nothing
        Me.TxtDipMarking.Size = New System.Drawing.Size(183, 18)
        Me.TxtDipMarking.TabIndex = 7
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel9.Location = New System.Drawing.Point(241, 71)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(67, 16)
        Me.MyLabel9.TabIndex = 326
        Me.MyLabel9.Text = "Dip Marking"
        '
        'FndTankerCode
        '
        Me.FndTankerCode.CalculationExpression = Nothing
        Me.FndTankerCode.FieldCode = Nothing
        Me.FndTankerCode.FieldDesc = Nothing
        Me.FndTankerCode.FieldMaxLength = 0
        Me.FndTankerCode.FieldName = Nothing
        Me.FndTankerCode.isCalculatedField = False
        Me.FndTankerCode.IsSourceFromTable = False
        Me.FndTankerCode.IsSourceFromValueList = False
        Me.FndTankerCode.IsUnique = False
        Me.FndTankerCode.Location = New System.Drawing.Point(90, 46)
        Me.FndTankerCode.MendatroryField = True
        Me.FndTankerCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndTankerCode.MyLinkLable1 = Me.RadLabel2
        Me.FndTankerCode.MyLinkLable2 = Nothing
        Me.FndTankerCode.MyReadOnly = False
        Me.FndTankerCode.MyShowMasterFormButton = False
        Me.FndTankerCode.Name = "FndTankerCode"
        Me.FndTankerCode.ReferenceFieldDesc = Nothing
        Me.FndTankerCode.ReferenceFieldName = Nothing
        Me.FndTankerCode.ReferenceTableName = Nothing
        Me.FndTankerCode.Size = New System.Drawing.Size(146, 20)
        Me.FndTankerCode.TabIndex = 4
        Me.FndTankerCode.Value = ""
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel7.Location = New System.Drawing.Point(182, 117)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(75, 16)
        Me.MyLabel7.TabIndex = 320
        Me.MyLabel7.Text = "Gross Weight"
        '
        'TxtTareWeight
        '
        Me.TxtTareWeight.BackColor = System.Drawing.Color.White
        Me.TxtTareWeight.CalculationExpression = Nothing
        Me.TxtTareWeight.DecimalPlaces = 2
        Me.TxtTareWeight.Enabled = False
        Me.TxtTareWeight.FieldCode = Nothing
        Me.TxtTareWeight.FieldDesc = Nothing
        Me.TxtTareWeight.FieldMaxLength = 0
        Me.TxtTareWeight.FieldName = Nothing
        Me.TxtTareWeight.isCalculatedField = False
        Me.TxtTareWeight.IsSourceFromTable = False
        Me.TxtTareWeight.IsSourceFromValueList = False
        Me.TxtTareWeight.IsUnique = False
        Me.TxtTareWeight.Location = New System.Drawing.Point(90, 114)
        Me.TxtTareWeight.MendatroryField = False
        Me.TxtTareWeight.MyLinkLable1 = Nothing
        Me.TxtTareWeight.MyLinkLable2 = Nothing
        Me.TxtTareWeight.Name = "TxtTareWeight"
        Me.TxtTareWeight.ReadOnly = True
        Me.TxtTareWeight.ReferenceFieldDesc = Nothing
        Me.TxtTareWeight.ReferenceFieldName = Nothing
        Me.TxtTareWeight.ReferenceTableName = Nothing
        Me.TxtTareWeight.Size = New System.Drawing.Size(90, 20)
        Me.TxtTareWeight.TabIndex = 9
        Me.TxtTareWeight.Text = "0"
        Me.TxtTareWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtTareWeight.Value = 0R
        '
        'txtNetWeight
        '
        Me.txtNetWeight.BackColor = System.Drawing.Color.White
        Me.txtNetWeight.CalculationExpression = Nothing
        Me.txtNetWeight.DecimalPlaces = 2
        Me.txtNetWeight.Enabled = False
        Me.txtNetWeight.FieldCode = Nothing
        Me.txtNetWeight.FieldDesc = Nothing
        Me.txtNetWeight.FieldMaxLength = 0
        Me.txtNetWeight.FieldName = Nothing
        Me.txtNetWeight.isCalculatedField = False
        Me.txtNetWeight.IsSourceFromTable = False
        Me.txtNetWeight.IsSourceFromValueList = False
        Me.txtNetWeight.IsUnique = False
        Me.txtNetWeight.Location = New System.Drawing.Point(417, 115)
        Me.txtNetWeight.MendatroryField = False
        Me.txtNetWeight.MyLinkLable1 = Nothing
        Me.txtNetWeight.MyLinkLable2 = Nothing
        Me.txtNetWeight.Name = "txtNetWeight"
        Me.txtNetWeight.ReadOnly = True
        Me.txtNetWeight.ReferenceFieldDesc = Nothing
        Me.txtNetWeight.ReferenceFieldName = Nothing
        Me.txtNetWeight.ReferenceTableName = Nothing
        Me.txtNetWeight.Size = New System.Drawing.Size(90, 20)
        Me.txtNetWeight.TabIndex = 11
        Me.txtNetWeight.Text = "0"
        Me.txtNetWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtNetWeight.Value = 0R
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel6.Location = New System.Drawing.Point(354, 117)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(62, 16)
        Me.MyLabel6.TabIndex = 319
        Me.MyLabel6.Text = "Net Weight"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel3.Location = New System.Drawing.Point(10, 115)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(68, 16)
        Me.MyLabel3.TabIndex = 318
        Me.MyLabel3.Text = "Tare Weight"
        '
        'txtGrossWeight
        '
        Me.txtGrossWeight.BackColor = System.Drawing.Color.White
        Me.txtGrossWeight.CalculationExpression = Nothing
        Me.txtGrossWeight.DecimalPlaces = 2
        Me.txtGrossWeight.Enabled = False
        Me.txtGrossWeight.FieldCode = Nothing
        Me.txtGrossWeight.FieldDesc = Nothing
        Me.txtGrossWeight.FieldMaxLength = 0
        Me.txtGrossWeight.FieldName = Nothing
        Me.txtGrossWeight.isCalculatedField = False
        Me.txtGrossWeight.IsSourceFromTable = False
        Me.txtGrossWeight.IsSourceFromValueList = False
        Me.txtGrossWeight.IsUnique = False
        Me.txtGrossWeight.Location = New System.Drawing.Point(260, 115)
        Me.txtGrossWeight.MendatroryField = False
        Me.txtGrossWeight.MyLinkLable1 = Nothing
        Me.txtGrossWeight.MyLinkLable2 = Nothing
        Me.txtGrossWeight.Name = "txtGrossWeight"
        Me.txtGrossWeight.ReadOnly = True
        Me.txtGrossWeight.ReferenceFieldDesc = Nothing
        Me.txtGrossWeight.ReferenceFieldName = Nothing
        Me.txtGrossWeight.ReferenceTableName = Nothing
        Me.txtGrossWeight.Size = New System.Drawing.Size(90, 20)
        Me.txtGrossWeight.TabIndex = 10
        Me.txtGrossWeight.Text = "0"
        Me.txtGrossWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtGrossWeight.Value = 0R
        '
        'LblLocationName
        '
        Me.LblLocationName.AutoSize = False
        Me.LblLocationName.BorderVisible = True
        Me.LblLocationName.FieldName = Nothing
        Me.LblLocationName.Location = New System.Drawing.Point(241, 92)
        Me.LblLocationName.Name = "LblLocationName"
        Me.LblLocationName.Size = New System.Drawing.Size(291, 20)
        Me.LblLocationName.TabIndex = 312
        '
        'LblTankerName
        '
        Me.LblTankerName.AutoSize = False
        Me.LblTankerName.BorderVisible = True
        Me.LblTankerName.FieldName = Nothing
        Me.LblTankerName.Location = New System.Drawing.Point(241, 46)
        Me.LblTankerName.Name = "LblTankerName"
        Me.LblTankerName.Size = New System.Drawing.Size(291, 20)
        Me.LblTankerName.TabIndex = 310
        Me.LblTankerName.Visible = False
        '
        'lblLocationCode
        '
        Me.lblLocationCode.AutoSize = False
        Me.lblLocationCode.BorderVisible = True
        Me.lblLocationCode.FieldName = Nothing
        Me.lblLocationCode.Location = New System.Drawing.Point(90, 92)
        Me.lblLocationCode.Name = "lblLocationCode"
        Me.lblLocationCode.Size = New System.Drawing.Size(146, 20)
        Me.lblLocationCode.TabIndex = 311
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel5.Location = New System.Drawing.Point(10, 93)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(49, 16)
        Me.MyLabel5.TabIndex = 313
        Me.MyLabel5.Text = "Location"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel2.Location = New System.Drawing.Point(10, 47)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(59, 16)
        Me.MyLabel2.TabIndex = 314
        Me.MyLabel2.Text = "Tanker No"
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(10, 70)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(41, 16)
        Me.MyLabel8.TabIndex = 134
        Me.MyLabel8.Text = "QC No"
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
        Me.txtDate.Location = New System.Drawing.Point(406, 1)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.RadLabel4
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(126, 18)
        Me.txtDate.TabIndex = 1
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011 11:29 AM"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox2.Controls.Add(Me.gv1)
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "Item Details"
        Me.RadGroupBox2.Location = New System.Drawing.Point(2, 228)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(983, 217)
        Me.RadGroupBox2.TabIndex = 14
        Me.RadGroupBox2.Text = "Item Details"
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
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(963, 187)
        Me.gv1.TabIndex = 13
        Me.gv1.TabStop = False
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(10, 1)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(68, 16)
        Me.RadLabel1.TabIndex = 39
        Me.RadLabel1.Text = "Dispatch No"
        '
        'fndQcNo
        '
        Me.fndQcNo.CalculationExpression = Nothing
        Me.fndQcNo.Enabled = False
        Me.fndQcNo.FieldCode = Nothing
        Me.fndQcNo.FieldDesc = Nothing
        Me.fndQcNo.FieldMaxLength = 0
        Me.fndQcNo.FieldName = Nothing
        Me.fndQcNo.isCalculatedField = False
        Me.fndQcNo.IsSourceFromTable = False
        Me.fndQcNo.IsSourceFromValueList = False
        Me.fndQcNo.IsUnique = False
        Me.fndQcNo.Location = New System.Drawing.Point(927, 5)
        Me.fndQcNo.MendatroryField = True
        Me.fndQcNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndQcNo.MyLinkLable1 = Nothing
        Me.fndQcNo.MyLinkLable2 = Nothing
        Me.fndQcNo.MyReadOnly = False
        Me.fndQcNo.MyShowMasterFormButton = False
        Me.fndQcNo.Name = "fndQcNo"
        Me.fndQcNo.ReferenceFieldDesc = Nothing
        Me.fndQcNo.ReferenceFieldName = Nothing
        Me.fndQcNo.ReferenceTableName = Nothing
        Me.fndQcNo.Size = New System.Drawing.Size(52, 20)
        Me.fndQcNo.TabIndex = 6
        Me.fndQcNo.Value = ""
        Me.fndQcNo.Visible = False
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(90, 0)
        Me.txtDocNo.MendatroryField = False
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Me.RadLabel1
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 32767
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(252, 20)
        Me.txtDocNo.TabIndex = 0
        Me.txtDocNo.Value = ""
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.XpertERPSalesAndDistribution.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(346, 0)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 21)
        Me.btnAddNew.TabIndex = 1
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.GroupBox3)
        Me.RadPageViewPage2.Controls.Add(Me.txtTaxGroup)
        Me.RadPageViewPage2.Controls.Add(Me.lblTaxGrpName)
        Me.RadPageViewPage2.Controls.Add(Me.gv2)
        Me.RadPageViewPage2.Controls.Add(Me.RadLabel11)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(47.0!, 26.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(988, 445)
        Me.RadPageViewPage2.Text = "Taxes"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.rbtnTaxCalManual)
        Me.GroupBox3.Controls.Add(Me.rbtnTaxCalAutomatic)
        Me.GroupBox3.Location = New System.Drawing.Point(549, 6)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(163, 36)
        Me.GroupBox3.TabIndex = 8
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Tax Calculation Type"
        '
        'rbtnTaxCalManual
        '
        Me.rbtnTaxCalManual.Location = New System.Drawing.Point(89, 13)
        Me.rbtnTaxCalManual.MyLinkLable1 = Nothing
        Me.rbtnTaxCalManual.MyLinkLable2 = Nothing
        Me.rbtnTaxCalManual.Name = "rbtnTaxCalManual"
        Me.rbtnTaxCalManual.Size = New System.Drawing.Size(57, 18)
        Me.rbtnTaxCalManual.TabIndex = 1
        Me.rbtnTaxCalManual.Text = "Manual"
        '
        'rbtnTaxCalAutomatic
        '
        Me.rbtnTaxCalAutomatic.Location = New System.Drawing.Point(7, 13)
        Me.rbtnTaxCalAutomatic.MyLinkLable1 = Nothing
        Me.rbtnTaxCalAutomatic.MyLinkLable2 = Nothing
        Me.rbtnTaxCalAutomatic.Name = "rbtnTaxCalAutomatic"
        Me.rbtnTaxCalAutomatic.Size = New System.Drawing.Size(72, 18)
        Me.rbtnTaxCalAutomatic.TabIndex = 0
        Me.rbtnTaxCalAutomatic.Text = "Automatic"
        '
        'txtTaxGroup
        '
        Me.txtTaxGroup.CalculationExpression = Nothing
        Me.txtTaxGroup.FieldCode = Nothing
        Me.txtTaxGroup.FieldDesc = Nothing
        Me.txtTaxGroup.FieldMaxLength = 0
        Me.txtTaxGroup.FieldName = Nothing
        Me.txtTaxGroup.isCalculatedField = False
        Me.txtTaxGroup.IsSourceFromTable = False
        Me.txtTaxGroup.IsSourceFromValueList = False
        Me.txtTaxGroup.IsUnique = False
        Me.txtTaxGroup.Location = New System.Drawing.Point(71, 11)
        Me.txtTaxGroup.MendatroryField = True
        Me.txtTaxGroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTaxGroup.MyLinkLable1 = Me.RadLabel11
        Me.txtTaxGroup.MyLinkLable2 = Me.lblTaxGrpName
        Me.txtTaxGroup.MyReadOnly = False
        Me.txtTaxGroup.MyShowMasterFormButton = False
        Me.txtTaxGroup.Name = "txtTaxGroup"
        Me.txtTaxGroup.ReferenceFieldDesc = Nothing
        Me.txtTaxGroup.ReferenceFieldName = Nothing
        Me.txtTaxGroup.ReferenceTableName = Nothing
        Me.txtTaxGroup.Size = New System.Drawing.Size(143, 18)
        Me.txtTaxGroup.TabIndex = 6
        Me.txtTaxGroup.Value = ""
        '
        'RadLabel11
        '
        Me.RadLabel11.FieldName = Nothing
        Me.RadLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel11.Location = New System.Drawing.Point(5, 14)
        Me.RadLabel11.Name = "RadLabel11"
        Me.RadLabel11.Size = New System.Drawing.Size(60, 16)
        Me.RadLabel11.TabIndex = 10
        Me.RadLabel11.Text = "Tax Group"
        '
        'lblTaxGrpName
        '
        Me.lblTaxGrpName.AutoSize = False
        Me.lblTaxGrpName.BorderVisible = True
        Me.lblTaxGrpName.FieldName = Nothing
        Me.lblTaxGrpName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTaxGrpName.Location = New System.Drawing.Point(222, 11)
        Me.lblTaxGrpName.Name = "lblTaxGrpName"
        Me.lblTaxGrpName.Size = New System.Drawing.Size(321, 20)
        Me.lblTaxGrpName.TabIndex = 7
        Me.lblTaxGrpName.TextWrap = False
        '
        'gv2
        '
        Me.gv2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gv2.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv2.BeginEditMode = Telerik.WinControls.RadGridViewBeginEditMode.BeginEditOnEnter
        Me.gv2.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv2.ForeColor = System.Drawing.Color.Black
        Me.gv2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv2.Location = New System.Drawing.Point(4, 42)
        '
        '
        '
        Me.gv2.MasterTemplate.AllowAddNewRow = False
        Me.gv2.MasterTemplate.AllowDeleteRow = False
        Me.gv2.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv2.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv2.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gv2.Name = "gv2"
        Me.gv2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv2.ShowHeaderCellButtons = True
        Me.gv2.Size = New System.Drawing.Size(981, 395)
        Me.gv2.TabIndex = 9
        Me.gv2.TabStop = False
        '
        'Attachments
        '
        Me.Attachments.Controls.Add(Me.UcAttachment1)
        Me.Attachments.ItemSize = New System.Drawing.SizeF(79.0!, 26.0!)
        Me.Attachments.Location = New System.Drawing.Point(10, 35)
        Me.Attachments.Name = "Attachments"
        Me.Attachments.Size = New System.Drawing.Size(988, 445)
        Me.Attachments.Text = "Attachments"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(988, 445)
        Me.UcAttachment1.TabIndex = 1
        Me.UcAttachment1.TabStop = False
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1009, 20)
        Me.RadMenu1.TabIndex = 10
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RDSaveLayout, Me.RDDeleteLayout, Me.EmailSmsSetting})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        '
        'RDSaveLayout
        '
        Me.RDSaveLayout.Name = "RDSaveLayout"
        Me.RDSaveLayout.Text = "Save Layout"
        '
        'RDDeleteLayout
        '
        Me.RDDeleteLayout.Name = "RDDeleteLayout"
        Me.RDDeleteLayout.Text = "Delete Layout"
        '
        'EmailSmsSetting
        '
        Me.EmailSmsSetting.Name = "EmailSmsSetting"
        Me.EmailSmsSetting.Text = "Email/SMS Setting"
        '
        'btnReverseAndUnpost
        '
        Me.btnReverseAndUnpost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReverseAndUnpost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverseAndUnpost.Location = New System.Drawing.Point(792, 4)
        Me.btnReverseAndUnpost.Name = "btnReverseAndUnpost"
        Me.btnReverseAndUnpost.Size = New System.Drawing.Size(122, 20)
        Me.btnReverseAndUnpost.TabIndex = 10
        Me.btnReverseAndUnpost.Text = "Reverse and Unpost"
        Me.btnReverseAndUnpost.Visible = False
        '
        'btnShowInventory
        '
        Me.btnShowInventory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnShowInventory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShowInventory.Location = New System.Drawing.Point(681, 4)
        Me.btnShowInventory.Name = "btnShowInventory"
        Me.btnShowInventory.Size = New System.Drawing.Size(105, 20)
        Me.btnShowInventory.TabIndex = 8
        Me.btnShowInventory.Text = "Show Inventory"
        '
        'btn_printproforma
        '
        Me.btn_printproforma.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btn_printproforma.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_printproforma.Location = New System.Drawing.Point(551, 4)
        Me.btn_printproforma.Name = "btn_printproforma"
        Me.btn_printproforma.Size = New System.Drawing.Size(128, 20)
        Me.btn_printproforma.TabIndex = 7
        Me.btn_printproforma.Text = "Print Proforma Invoice"
        Me.btn_printproforma.Visible = False
        '
        'btnUpdateCustomer
        '
        Me.btnUpdateCustomer.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnUpdateCustomer.Location = New System.Drawing.Point(420, 4)
        Me.btnUpdateCustomer.Name = "btnUpdateCustomer"
        Me.btnUpdateCustomer.Size = New System.Drawing.Size(129, 20)
        Me.btnUpdateCustomer.TabIndex = 6
        Me.btnUpdateCustomer.Text = "Update Customer"
        '
        'btnsetting
        '
        Me.btnsetting.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsetting.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnPreview, Me.btnSend, Me.btnSendforApproval})
        Me.btnsetting.Location = New System.Drawing.Point(332, 4)
        Me.btnsetting.Name = "btnsetting"
        Me.btnsetting.Size = New System.Drawing.Size(83, 20)
        Me.btnsetting.TabIndex = 4
        Me.btnsetting.Text = "E-Mail/SMS"
        Me.btnsetting.Visible = False
        '
        'btnPreview
        '
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Text = "Preview"
        '
        'btnSend
        '
        Me.btnSend.Name = "btnSend"
        Me.btnSend.Text = "Send Mail/Sms"
        '
        'btnSendforApproval
        '
        Me.btnSendforApproval.Name = "btnSendforApproval"
        Me.btnSendforApproval.Text = "Send Mail For Approval"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(253, 4)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(73, 20)
        Me.btnPrint.TabIndex = 3
        Me.btnPrint.Text = "Print"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(94, 4)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(73, 20)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(922, 4)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(73, 20)
        Me.btnclose.TabIndex = 5
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(173, 4)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(73, 20)
        Me.btndelete.TabIndex = 2
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(15, 4)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(73, 20)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'FrmDispatchBulkSale
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1009, 546)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmDispatchBulkSale"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Dispatch Bulk Sale"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.txtTCSTaxRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel57, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTaxAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblActualTCSTaxBaseAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel58, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txttcstaxbaseamount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocumentAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotRAmt1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtsealno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.txtewaybilldate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtEWayBillNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.RadPanel1.PerformLayout()
        CType(Me.txtinsuranceno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblQCCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomerCode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.lblCustomerCode.ResumeLayout(False)
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.TxtToleranceinplus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToleranceinminus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSNFRatio, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtSNFWeightage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtFatWeightage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtStanadardrate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtfatRatio, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCreateAoutoInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomerName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCreditLimit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCreditLimit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtChallanNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtDipMarking, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtTareWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNetWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGrossWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblTankerName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.RadPageViewPage2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.rbtnTaxCalManual, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnTaxCalAutomatic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTaxGrpName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Attachments.ResumeLayout(False)
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReverseAndUnpost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnShowInventory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_printproforma, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUpdateCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsetting, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents fndCustomerNo As common.UserControls.txtFinder
    Friend WithEvents fndQcNo As common.UserControls.txtFinder
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents Attachments As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents LblLocationName As common.Controls.MyLabel
    Friend WithEvents LblTankerName As common.Controls.MyLabel
    Friend WithEvents lblLocationCode As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents TxtTareWeight As common.MyNumBox
    Friend WithEvents txtNetWeight As common.MyNumBox
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtGrossWeight As common.MyNumBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents TxtChallanNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents TxtDipMarking As common.Controls.MyTextBox
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents FndTankerCode As common.UserControls.txtFinder
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents FndPriceCode As common.UserControls.txtFinder
    Friend WithEvents RDSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RDDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents EmailSmsSetting As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnsetting As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnPreview As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnSend As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnSendforApproval As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents lblTotRAmt1 As common.Controls.MyLabel
    Friend WithEvents txtCreditLimit As common.MyNumBox
    Friend WithEvents lblCreditLimit As common.Controls.MyLabel
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents lblCustomerName As common.Controls.MyLabel
    Friend WithEvents chkCreateAoutoInvoice As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtToleranceinminus As common.MyNumBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents txtSNFRatio As common.MyNumBox
    Friend WithEvents TxtSNFWeightage As common.MyNumBox
    Friend WithEvents TxtFatWeightage As common.MyNumBox
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents txtStanadardrate As common.MyNumBox
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents txtfatRatio As common.MyNumBox
    Friend WithEvents TxtToleranceinplus As common.MyNumBox
    Friend WithEvents MyLabel17 As common.Controls.MyLabel
    Friend WithEvents lblCustomerCode As common.Controls.MyLabel
    Friend WithEvents LblQCCode As common.Controls.MyLabel
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents btnUpdateCustomer As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents txtsealno As common.Controls.MyTextBox
    Friend WithEvents MyLabel19 As common.Controls.MyLabel
    Friend WithEvents txtinsuranceno As common.Controls.MyTextBox
    Friend WithEvents MyLabel18 As common.Controls.MyLabel
    Friend WithEvents btn_printproforma As Telerik.WinControls.UI.RadButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents TxtEWayBillNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel20 As common.Controls.MyLabel
    Friend WithEvents txtewaybilldate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel21 As common.Controls.MyLabel
    Friend WithEvents btnShowInventory As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReverseAndUnpost As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageViewPage2 As RadPageViewPage
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents rbtnTaxCalManual As common.Controls.MyRadioButton
    Friend WithEvents rbtnTaxCalAutomatic As common.Controls.MyRadioButton
    Friend WithEvents txtTaxGroup As common.UserControls.txtFinder
    Friend WithEvents RadLabel11 As common.Controls.MyLabel
    Friend WithEvents lblTaxGrpName As common.Controls.MyLabel
    Friend WithEvents gv2 As common.UserControls.MyRadGridView
    Friend WithEvents RadLabel25 As common.Controls.MyLabel
    Friend WithEvents lblTaxAmt As common.Controls.MyLabel
    Friend WithEvents MyLabel22 As common.Controls.MyLabel
    Friend WithEvents lblDocumentAmount As common.Controls.MyLabel
    Friend WithEvents txtTCSTaxRate As common.MyNumBox
    Friend WithEvents MyLabel57 As common.Controls.MyLabel
    Friend WithEvents lblActualTCSTaxBaseAmt As common.Controls.MyLabel
    Friend WithEvents MyLabel58 As common.Controls.MyLabel
    Friend WithEvents txttcstaxbaseamount As common.MyNumBox
End Class

