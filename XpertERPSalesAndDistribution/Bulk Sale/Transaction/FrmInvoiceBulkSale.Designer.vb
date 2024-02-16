<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmInvoiceBulkSale
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
        Dim TableViewDefinition3 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition4 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.txtTCSTaxRate = New common.MyNumBox()
        Me.MyLabel57 = New common.Controls.MyLabel()
        Me.lblActualTCSTaxBaseAmt = New common.Controls.MyLabel()
        Me.MyLabel58 = New common.Controls.MyLabel()
        Me.txttcstaxbaseamount = New common.MyNumBox()
        Me.MyLabel22 = New common.Controls.MyLabel()
        Me.RadLabel25 = New common.Controls.MyLabel()
        Me.lblTaxAmt = New common.Controls.MyLabel()
        Me.lblDocumentAmount = New common.Controls.MyLabel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtElectronicRefNo = New common.Controls.MyTextBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtewaybilldate = New common.Controls.MyDateTimePicker()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.btnEwaybillupdate = New Telerik.WinControls.UI.RadButton()
        Me.TxtEWayBillNo = New common.Controls.MyTextBox()
        Me.MyLabel20 = New common.Controls.MyLabel()
        Me.txtComment = New common.Controls.MyTextBox()
        Me.lblComments = New common.Controls.MyLabel()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rdbAgainstDispatchTrade = New Telerik.WinControls.UI.RadRadioButton()
        Me.rdbAgainstDispatch = New Telerik.WinControls.UI.RadRadioButton()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.lblToDate = New common.Controls.MyLabel()
        Me.lblfromDate = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.TxtRoundoff = New common.Controls.MyLabel()
        Me.lblInvoiceStatus = New common.Controls.MyLabel()
        Me.lblCustomerName = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.lblTotRAmt1 = New common.Controls.MyLabel()
        Me.txtDispatchNo = New common.UserControls.txtFinder()
        Me.FndLocationCode = New common.UserControls.txtFinder()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.LblLocationName = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.fndCustomerNo = New common.UserControls.txtFinder()
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
        Me.RMSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.RMDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.EmailSMSSetting = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnInvoiceJE = New Telerik.WinControls.UI.RadButton()
        Me.btnimportdocument = New Telerik.WinControls.UI.RadButton()
        Me.btnDeleteInvoiceafterPost = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnPrintJV = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.txtTCSTaxRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel57, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblActualTCSTaxBaseAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel58, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txttcstaxbaseamount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTaxAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocumentAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.txtElectronicRefNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtewaybilldate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnEwaybillupdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtEWayBillNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtComment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblComments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.rdbAgainstDispatchTrade, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbAgainstDispatch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblfromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtRoundoff, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblInvoiceStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomerName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotRAmt1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
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
        CType(Me.btnInvoiceJE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnimportdocument, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDeleteInvoiceafterPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrintJV, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrintJV)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnInvoiceJE)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnimportdocument)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDeleteInvoiceafterPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(1046, 483)
        Me.SplitContainer1.SplitterDistance = 445
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
        Me.RadPageView1.Size = New System.Drawing.Size(1046, 425)
        Me.RadPageView1.TabIndex = 0
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.txtTCSTaxRate)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel57)
        Me.RadPageViewPage1.Controls.Add(Me.lblActualTCSTaxBaseAmt)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel58)
        Me.RadPageViewPage1.Controls.Add(Me.txttcstaxbaseamount)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel22)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel25)
        Me.RadPageViewPage1.Controls.Add(Me.lblTaxAmt)
        Me.RadPageViewPage1.Controls.Add(Me.lblDocumentAmount)
        Me.RadPageViewPage1.Controls.Add(Me.GroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.txtComment)
        Me.RadPageViewPage1.Controls.Add(Me.lblComments)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.TxtRoundoff)
        Me.RadPageViewPage1.Controls.Add(Me.lblInvoiceStatus)
        Me.RadPageViewPage1.Controls.Add(Me.lblCustomerName)
        Me.RadPageViewPage1.Controls.Add(Me.UsLock1)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel11)
        Me.RadPageViewPage1.Controls.Add(Me.lblTotRAmt1)
        Me.RadPageViewPage1.Controls.Add(Me.txtDispatchNo)
        Me.RadPageViewPage1.Controls.Add(Me.FndLocationCode)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.LblLocationName)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.txtDate)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.fndCustomerNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtDocNo)
        Me.RadPageViewPage1.Controls.Add(Me.btnAddNew)
        Me.RadPageViewPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(79.0!, 26.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1025, 379)
        Me.RadPageViewPage1.Text = "Invoice Note"
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
        Me.txtTCSTaxRate.Location = New System.Drawing.Point(897, 128)
        Me.txtTCSTaxRate.MendatroryField = False
        Me.txtTCSTaxRate.MyLinkLable1 = Nothing
        Me.txtTCSTaxRate.MyLinkLable2 = Nothing
        Me.txtTCSTaxRate.Name = "txtTCSTaxRate"
        Me.txtTCSTaxRate.ReferenceFieldDesc = Nothing
        Me.txtTCSTaxRate.ReferenceFieldName = Nothing
        Me.txtTCSTaxRate.ReferenceTableName = Nothing
        Me.txtTCSTaxRate.Size = New System.Drawing.Size(115, 20)
        Me.txtTCSTaxRate.TabIndex = 1409
        Me.txtTCSTaxRate.Text = "0"
        Me.txtTCSTaxRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTCSTaxRate.Value = 0R
        Me.txtTCSTaxRate.Visible = False
        '
        'MyLabel57
        '
        Me.MyLabel57.FieldName = Nothing
        Me.MyLabel57.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel57.Location = New System.Drawing.Point(722, 156)
        Me.MyLabel57.Name = "MyLabel57"
        Me.MyLabel57.Size = New System.Drawing.Size(138, 16)
        Me.MyLabel57.TabIndex = 1408
        Me.MyLabel57.Text = "Actual TCS Tax Base Amt"
        '
        'lblActualTCSTaxBaseAmt
        '
        Me.lblActualTCSTaxBaseAmt.AutoSize = False
        Me.lblActualTCSTaxBaseAmt.BorderVisible = True
        Me.lblActualTCSTaxBaseAmt.FieldName = Nothing
        Me.lblActualTCSTaxBaseAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblActualTCSTaxBaseAmt.Location = New System.Drawing.Point(859, 154)
        Me.lblActualTCSTaxBaseAmt.Name = "lblActualTCSTaxBaseAmt"
        Me.lblActualTCSTaxBaseAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblActualTCSTaxBaseAmt.TabIndex = 1407
        Me.lblActualTCSTaxBaseAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel58
        '
        Me.MyLabel58.FieldName = Nothing
        Me.MyLabel58.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel58.Location = New System.Drawing.Point(722, 180)
        Me.MyLabel58.Name = "MyLabel58"
        Me.MyLabel58.Size = New System.Drawing.Size(122, 16)
        Me.MyLabel58.TabIndex = 1406
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
        Me.txttcstaxbaseamount.Location = New System.Drawing.Point(859, 176)
        Me.txttcstaxbaseamount.MendatroryField = False
        Me.txttcstaxbaseamount.MyLinkLable1 = Nothing
        Me.txttcstaxbaseamount.MyLinkLable2 = Nothing
        Me.txttcstaxbaseamount.Name = "txttcstaxbaseamount"
        Me.txttcstaxbaseamount.ReferenceFieldDesc = Nothing
        Me.txttcstaxbaseamount.ReferenceFieldName = Nothing
        Me.txttcstaxbaseamount.ReferenceTableName = Nothing
        Me.txttcstaxbaseamount.Size = New System.Drawing.Size(115, 20)
        Me.txttcstaxbaseamount.TabIndex = 1405
        Me.txttcstaxbaseamount.Text = "0"
        Me.txttcstaxbaseamount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txttcstaxbaseamount.Value = 0R
        '
        'MyLabel22
        '
        Me.MyLabel22.AutoSize = False
        Me.MyLabel22.FieldName = Nothing
        Me.MyLabel22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel22.Location = New System.Drawing.Point(536, 179)
        Me.MyLabel22.Name = "MyLabel22"
        Me.MyLabel22.Size = New System.Drawing.Size(72, 19)
        Me.MyLabel22.TabIndex = 350
        Me.MyLabel22.Text = "Total Amount"
        '
        'RadLabel25
        '
        Me.RadLabel25.FieldName = Nothing
        Me.RadLabel25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel25.Location = New System.Drawing.Point(536, 156)
        Me.RadLabel25.Name = "RadLabel25"
        Me.RadLabel25.Size = New System.Drawing.Size(67, 16)
        Me.RadLabel25.TabIndex = 349
        Me.RadLabel25.Text = "Tax Amount"
        '
        'lblTaxAmt
        '
        Me.lblTaxAmt.AutoSize = False
        Me.lblTaxAmt.BorderVisible = True
        Me.lblTaxAmt.FieldName = Nothing
        Me.lblTaxAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTaxAmt.Location = New System.Drawing.Point(614, 155)
        Me.lblTaxAmt.Name = "lblTaxAmt"
        Me.lblTaxAmt.Size = New System.Drawing.Size(104, 18)
        Me.lblTaxAmt.TabIndex = 348
        Me.lblTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDocumentAmount
        '
        Me.lblDocumentAmount.AutoSize = False
        Me.lblDocumentAmount.BorderVisible = True
        Me.lblDocumentAmount.FieldName = Nothing
        Me.lblDocumentAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocumentAmount.Location = New System.Drawing.Point(361, 156)
        Me.lblDocumentAmount.Name = "lblDocumentAmount"
        Me.lblDocumentAmount.Size = New System.Drawing.Size(171, 20)
        Me.lblDocumentAmount.TabIndex = 347
        Me.lblDocumentAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtElectronicRefNo)
        Me.GroupBox2.Controls.Add(Me.MyLabel4)
        Me.GroupBox2.Controls.Add(Me.txtewaybilldate)
        Me.GroupBox2.Controls.Add(Me.MyLabel3)
        Me.GroupBox2.Controls.Add(Me.btnEwaybillupdate)
        Me.GroupBox2.Controls.Add(Me.TxtEWayBillNo)
        Me.GroupBox2.Controls.Add(Me.MyLabel20)
        Me.GroupBox2.Location = New System.Drawing.Point(535, 56)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(346, 92)
        Me.GroupBox2.TabIndex = 343
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "GST"
        '
        'txtElectronicRefNo
        '
        Me.txtElectronicRefNo.CalculationExpression = Nothing
        Me.txtElectronicRefNo.FieldCode = Nothing
        Me.txtElectronicRefNo.FieldDesc = Nothing
        Me.txtElectronicRefNo.FieldMaxLength = 0
        Me.txtElectronicRefNo.FieldName = Nothing
        Me.txtElectronicRefNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtElectronicRefNo.isCalculatedField = False
        Me.txtElectronicRefNo.IsSourceFromTable = False
        Me.txtElectronicRefNo.IsSourceFromValueList = False
        Me.txtElectronicRefNo.IsUnique = False
        Me.txtElectronicRefNo.Location = New System.Drawing.Point(108, 65)
        Me.txtElectronicRefNo.MaxLength = 10
        Me.txtElectronicRefNo.MendatroryField = False
        Me.txtElectronicRefNo.MyLinkLable1 = Nothing
        Me.txtElectronicRefNo.MyLinkLable2 = Nothing
        Me.txtElectronicRefNo.Name = "txtElectronicRefNo"
        Me.txtElectronicRefNo.ReferenceFieldDesc = Nothing
        Me.txtElectronicRefNo.ReferenceFieldName = Nothing
        Me.txtElectronicRefNo.ReferenceTableName = Nothing
        Me.txtElectronicRefNo.Size = New System.Drawing.Size(146, 18)
        Me.txtElectronicRefNo.TabIndex = 336
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel4.Location = New System.Drawing.Point(7, 66)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(97, 16)
        Me.MyLabel4.TabIndex = 337
        Me.MyLabel4.Text = "Electronic Ref. No"
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
        Me.txtewaybilldate.Location = New System.Drawing.Point(108, 40)
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
        Me.txtewaybilldate.TabIndex = 335
        Me.txtewaybilldate.TabStop = False
        Me.txtewaybilldate.Text = "13/06/2011"
        Me.txtewaybilldate.Value = New Date(2011, 6, 13, 0, 0, 0, 0)
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(375, 31)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 4
        Me.RadLabel4.Text = "Date"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel3.Location = New System.Drawing.Point(7, 43)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(82, 16)
        Me.MyLabel3.TabIndex = 334
        Me.MyLabel3.Text = "E-WayBill Date"
        '
        'btnEwaybillupdate
        '
        Me.btnEwaybillupdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnEwaybillupdate.Location = New System.Drawing.Point(261, 62)
        Me.btnEwaybillupdate.Name = "btnEwaybillupdate"
        Me.btnEwaybillupdate.Size = New System.Drawing.Size(73, 20)
        Me.btnEwaybillupdate.TabIndex = 333
        Me.btnEwaybillupdate.Text = "Update"
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
        Me.TxtEWayBillNo.Location = New System.Drawing.Point(108, 16)
        Me.TxtEWayBillNo.MaxLength = 30
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
        Me.MyLabel20.Location = New System.Drawing.Point(7, 17)
        Me.MyLabel20.Name = "MyLabel20"
        Me.MyLabel20.Size = New System.Drawing.Size(73, 16)
        Me.MyLabel20.TabIndex = 332
        Me.MyLabel20.Text = "E-WayBill No"
        '
        'txtComment
        '
        Me.txtComment.CalculationExpression = Nothing
        Me.txtComment.FieldCode = Nothing
        Me.txtComment.FieldDesc = Nothing
        Me.txtComment.FieldMaxLength = 0
        Me.txtComment.FieldName = Nothing
        Me.txtComment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtComment.isCalculatedField = False
        Me.txtComment.IsSourceFromTable = False
        Me.txtComment.IsSourceFromValueList = False
        Me.txtComment.IsUnique = False
        Me.txtComment.Location = New System.Drawing.Point(90, 180)
        Me.txtComment.MaxLength = 200
        Me.txtComment.MendatroryField = False
        Me.txtComment.MyLinkLable1 = Nothing
        Me.txtComment.MyLinkLable2 = Nothing
        Me.txtComment.Name = "txtComment"
        Me.txtComment.ReferenceFieldDesc = Nothing
        Me.txtComment.ReferenceFieldName = Nothing
        Me.txtComment.ReferenceTableName = Nothing
        Me.txtComment.Size = New System.Drawing.Size(442, 18)
        Me.txtComment.TabIndex = 23
        '
        'lblComments
        '
        Me.lblComments.FieldName = Nothing
        Me.lblComments.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblComments.Location = New System.Drawing.Point(12, 180)
        Me.lblComments.Name = "lblComments"
        Me.lblComments.Size = New System.Drawing.Size(61, 16)
        Me.lblComments.TabIndex = 22
        Me.lblComments.Text = "Comments"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.rdbAgainstDispatchTrade)
        Me.RadGroupBox3.Controls.Add(Me.rdbAgainstDispatch)
        Me.RadGroupBox3.HeaderText = ""
        Me.RadGroupBox3.Location = New System.Drawing.Point(90, 3)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(273, 23)
        Me.RadGroupBox3.TabIndex = 0
        '
        'rdbAgainstDispatchTrade
        '
        Me.rdbAgainstDispatchTrade.Location = New System.Drawing.Point(126, 2)
        Me.rdbAgainstDispatchTrade.Name = "rdbAgainstDispatchTrade"
        Me.rdbAgainstDispatchTrade.Size = New System.Drawing.Size(136, 18)
        Me.rdbAgainstDispatchTrade.TabIndex = 1
        Me.rdbAgainstDispatchTrade.Text = "Against Dispatch Trade"
        '
        'rdbAgainstDispatch
        '
        Me.rdbAgainstDispatch.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rdbAgainstDispatch.Location = New System.Drawing.Point(6, 2)
        Me.rdbAgainstDispatch.Name = "rdbAgainstDispatch"
        Me.rdbAgainstDispatch.Size = New System.Drawing.Size(104, 18)
        Me.rdbAgainstDispatch.TabIndex = 0
        Me.rdbAgainstDispatch.Text = "Against Dispatch"
        Me.rdbAgainstDispatch.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.txtToDate)
        Me.RadGroupBox1.Controls.Add(Me.txtFromDate)
        Me.RadGroupBox1.Controls.Add(Me.lblToDate)
        Me.RadGroupBox1.Controls.Add(Me.lblfromDate)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(90, 102)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(362, 26)
        Me.RadGroupBox1.TabIndex = 14
        '
        'txtToDate
        '
        Me.txtToDate.CalculationExpression = Nothing
        Me.txtToDate.CustomFormat = "dd-MM-yyyy"
        Me.txtToDate.FieldCode = Nothing
        Me.txtToDate.FieldDesc = Nothing
        Me.txtToDate.FieldMaxLength = 0
        Me.txtToDate.FieldName = Nothing
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.isCalculatedField = False
        Me.txtToDate.IsSourceFromTable = False
        Me.txtToDate.IsSourceFromValueList = False
        Me.txtToDate.IsUnique = False
        Me.txtToDate.Location = New System.Drawing.Point(227, 4)
        Me.txtToDate.MendatroryField = False
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Nothing
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.ReferenceFieldDesc = Nothing
        Me.txtToDate.ReferenceFieldName = Nothing
        Me.txtToDate.ReferenceTableName = Nothing
        Me.txtToDate.Size = New System.Drawing.Size(126, 20)
        Me.txtToDate.TabIndex = 3
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "17-12-2011"
        Me.txtToDate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'txtFromDate
        '
        Me.txtFromDate.CalculationExpression = Nothing
        Me.txtFromDate.CustomFormat = "dd-MM-yyyy"
        Me.txtFromDate.FieldCode = Nothing
        Me.txtFromDate.FieldDesc = Nothing
        Me.txtFromDate.FieldMaxLength = 0
        Me.txtFromDate.FieldName = Nothing
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.isCalculatedField = False
        Me.txtFromDate.IsSourceFromTable = False
        Me.txtFromDate.IsSourceFromValueList = False
        Me.txtFromDate.IsUnique = False
        Me.txtFromDate.Location = New System.Drawing.Point(77, 3)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Nothing
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.ReferenceFieldDesc = Nothing
        Me.txtFromDate.ReferenceFieldName = Nothing
        Me.txtFromDate.ReferenceTableName = Nothing
        Me.txtFromDate.Size = New System.Drawing.Size(91, 20)
        Me.txtFromDate.TabIndex = 1
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "17-12-2011"
        Me.txtFromDate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'lblToDate
        '
        Me.lblToDate.FieldName = Nothing
        Me.lblToDate.Location = New System.Drawing.Point(177, 4)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(45, 18)
        Me.lblToDate.TabIndex = 2
        Me.lblToDate.Text = "To Date"
        '
        'lblfromDate
        '
        Me.lblfromDate.FieldName = Nothing
        Me.lblfromDate.Location = New System.Drawing.Point(4, 4)
        Me.lblfromDate.Name = "lblfromDate"
        Me.lblfromDate.Size = New System.Drawing.Size(59, 18)
        Me.lblfromDate.TabIndex = 0
        Me.lblfromDate.Text = "From Date"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(12, 156)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(58, 16)
        Me.MyLabel2.TabIndex = 17
        Me.MyLabel2.Text = "Round Off"
        '
        'TxtRoundoff
        '
        Me.TxtRoundoff.AutoSize = False
        Me.TxtRoundoff.BorderVisible = True
        Me.TxtRoundoff.FieldName = Nothing
        Me.TxtRoundoff.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRoundoff.Location = New System.Drawing.Point(90, 155)
        Me.TxtRoundoff.Name = "TxtRoundoff"
        Me.TxtRoundoff.Size = New System.Drawing.Size(147, 19)
        Me.TxtRoundoff.TabIndex = 18
        Me.TxtRoundoff.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblInvoiceStatus
        '
        Me.lblInvoiceStatus.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lblInvoiceStatus.FieldName = Nothing
        Me.lblInvoiceStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblInvoiceStatus.Location = New System.Drawing.Point(643, 30)
        Me.lblInvoiceStatus.Name = "lblInvoiceStatus"
        Me.lblInvoiceStatus.Size = New System.Drawing.Size(77, 16)
        Me.lblInvoiceStatus.TabIndex = 21
        Me.lblInvoiceStatus.Text = "Approved By"
        Me.lblInvoiceStatus.Visible = False
        '
        'lblCustomerName
        '
        Me.lblCustomerName.AutoSize = False
        Me.lblCustomerName.BorderVisible = True
        Me.lblCustomerName.FieldName = Nothing
        Me.lblCustomerName.Location = New System.Drawing.Point(241, 54)
        Me.lblCustomerName.Name = "lblCustomerName"
        Me.lblCustomerName.Size = New System.Drawing.Size(291, 19)
        Me.lblCustomerName.TabIndex = 9
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(538, 29)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(92, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 6
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel11.Location = New System.Drawing.Point(244, 156)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(100, 16)
        Me.MyLabel11.TabIndex = 19
        Me.MyLabel11.Text = "Document Amount"
        '
        'lblTotRAmt1
        '
        Me.lblTotRAmt1.AutoSize = False
        Me.lblTotRAmt1.BorderVisible = True
        Me.lblTotRAmt1.FieldName = Nothing
        Me.lblTotRAmt1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotRAmt1.Location = New System.Drawing.Point(614, 177)
        Me.lblTotRAmt1.Name = "lblTotRAmt1"
        Me.lblTotRAmt1.Size = New System.Drawing.Size(104, 19)
        Me.lblTotRAmt1.TabIndex = 20
        Me.lblTotRAmt1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDispatchNo
        '
        Me.txtDispatchNo.CalculationExpression = Nothing
        Me.txtDispatchNo.FieldCode = Nothing
        Me.txtDispatchNo.FieldDesc = Nothing
        Me.txtDispatchNo.FieldMaxLength = 0
        Me.txtDispatchNo.FieldName = Nothing
        Me.txtDispatchNo.isCalculatedField = False
        Me.txtDispatchNo.IsSourceFromTable = False
        Me.txtDispatchNo.IsSourceFromValueList = False
        Me.txtDispatchNo.IsUnique = False
        Me.txtDispatchNo.Location = New System.Drawing.Point(90, 131)
        Me.txtDispatchNo.MendatroryField = False
        Me.txtDispatchNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDispatchNo.MyLinkLable1 = Nothing
        Me.txtDispatchNo.MyLinkLable2 = Nothing
        Me.txtDispatchNo.MyReadOnly = True
        Me.txtDispatchNo.MyShowMasterFormButton = False
        Me.txtDispatchNo.Name = "txtDispatchNo"
        Me.txtDispatchNo.ReferenceFieldDesc = Nothing
        Me.txtDispatchNo.ReferenceFieldName = Nothing
        Me.txtDispatchNo.ReferenceTableName = Nothing
        Me.txtDispatchNo.Size = New System.Drawing.Size(146, 19)
        Me.txtDispatchNo.TabIndex = 16
        Me.txtDispatchNo.Value = ""
        '
        'FndLocationCode
        '
        Me.FndLocationCode.CalculationExpression = Nothing
        Me.FndLocationCode.FieldCode = Nothing
        Me.FndLocationCode.FieldDesc = Nothing
        Me.FndLocationCode.FieldMaxLength = 0
        Me.FndLocationCode.FieldName = Nothing
        Me.FndLocationCode.isCalculatedField = False
        Me.FndLocationCode.IsSourceFromTable = False
        Me.FndLocationCode.IsSourceFromValueList = False
        Me.FndLocationCode.IsUnique = False
        Me.FndLocationCode.Location = New System.Drawing.Point(90, 78)
        Me.FndLocationCode.MendatroryField = True
        Me.FndLocationCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndLocationCode.MyLinkLable1 = Me.RadLabel2
        Me.FndLocationCode.MyLinkLable2 = Nothing
        Me.FndLocationCode.MyReadOnly = False
        Me.FndLocationCode.MyShowMasterFormButton = False
        Me.FndLocationCode.Name = "FndLocationCode"
        Me.FndLocationCode.ReferenceFieldDesc = Nothing
        Me.FndLocationCode.ReferenceFieldName = Nothing
        Me.FndLocationCode.ReferenceTableName = Nothing
        Me.FndLocationCode.Size = New System.Drawing.Size(146, 19)
        Me.FndLocationCode.TabIndex = 11
        Me.FndLocationCode.Value = ""
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(11, 55)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(73, 16)
        Me.RadLabel2.TabIndex = 7
        Me.RadLabel2.Text = "Customer No"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(11, 132)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(68, 16)
        Me.MyLabel1.TabIndex = 15
        Me.MyLabel1.Text = "Dispatch No"
        '
        'LblLocationName
        '
        Me.LblLocationName.AutoSize = False
        Me.LblLocationName.BorderVisible = True
        Me.LblLocationName.FieldName = Nothing
        Me.LblLocationName.Location = New System.Drawing.Point(241, 78)
        Me.LblLocationName.Name = "LblLocationName"
        Me.LblLocationName.Size = New System.Drawing.Size(291, 19)
        Me.LblLocationName.TabIndex = 12
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel5.Location = New System.Drawing.Point(12, 79)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(49, 16)
        Me.MyLabel5.TabIndex = 10
        Me.MyLabel5.Text = "Location"
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
        Me.txtDate.Location = New System.Drawing.Point(406, 30)
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
        Me.txtDate.TabIndex = 5
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
        Me.RadGroupBox2.Location = New System.Drawing.Point(2, 202)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(1020, 179)
        Me.RadGroupBox2.TabIndex = 0
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
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition3
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(1000, 149)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(11, 31)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(60, 16)
        Me.RadLabel1.TabIndex = 1
        Me.RadLabel1.Text = "Invoice No"
        '
        'fndCustomerNo
        '
        Me.fndCustomerNo.CalculationExpression = Nothing
        Me.fndCustomerNo.FieldCode = Nothing
        Me.fndCustomerNo.FieldDesc = Nothing
        Me.fndCustomerNo.FieldMaxLength = 0
        Me.fndCustomerNo.FieldName = Nothing
        Me.fndCustomerNo.isCalculatedField = False
        Me.fndCustomerNo.IsSourceFromTable = False
        Me.fndCustomerNo.IsSourceFromValueList = False
        Me.fndCustomerNo.IsUnique = False
        Me.fndCustomerNo.Location = New System.Drawing.Point(90, 54)
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
        Me.fndCustomerNo.Size = New System.Drawing.Size(146, 19)
        Me.fndCustomerNo.TabIndex = 8
        Me.fndCustomerNo.Value = ""
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(90, 30)
        Me.txtDocNo.MendatroryField = False
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Me.RadLabel1
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 32767
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(252, 19)
        Me.txtDocNo.TabIndex = 2
        Me.txtDocNo.Value = ""
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.XpertERPSalesAndDistribution.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(344, 29)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 21)
        Me.btnAddNew.TabIndex = 3
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
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1025, 379)
        Me.RadPageViewPage2.Text = "Taxes"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.rbtnTaxCalManual)
        Me.GroupBox3.Controls.Add(Me.rbtnTaxCalAutomatic)
        Me.GroupBox3.Location = New System.Drawing.Point(551, -2)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(163, 36)
        Me.GroupBox3.TabIndex = 13
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
        Me.txtTaxGroup.Location = New System.Drawing.Point(73, 3)
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
        Me.txtTaxGroup.TabIndex = 11
        Me.txtTaxGroup.Value = ""
        '
        'RadLabel11
        '
        Me.RadLabel11.FieldName = Nothing
        Me.RadLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel11.Location = New System.Drawing.Point(7, 6)
        Me.RadLabel11.Name = "RadLabel11"
        Me.RadLabel11.Size = New System.Drawing.Size(60, 16)
        Me.RadLabel11.TabIndex = 15
        Me.RadLabel11.Text = "Tax Group"
        '
        'lblTaxGrpName
        '
        Me.lblTaxGrpName.AutoSize = False
        Me.lblTaxGrpName.BorderVisible = True
        Me.lblTaxGrpName.FieldName = Nothing
        Me.lblTaxGrpName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTaxGrpName.Location = New System.Drawing.Point(224, 3)
        Me.lblTaxGrpName.Name = "lblTaxGrpName"
        Me.lblTaxGrpName.Size = New System.Drawing.Size(321, 20)
        Me.lblTaxGrpName.TabIndex = 12
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
        Me.gv2.Location = New System.Drawing.Point(6, 34)
        '
        '
        '
        Me.gv2.MasterTemplate.AllowAddNewRow = False
        Me.gv2.MasterTemplate.AllowDeleteRow = False
        Me.gv2.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv2.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv2.MasterTemplate.ViewDefinition = TableViewDefinition4
        Me.gv2.Name = "gv2"
        Me.gv2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv2.ShowHeaderCellButtons = True
        Me.gv2.Size = New System.Drawing.Size(981, 343)
        Me.gv2.TabIndex = 14
        Me.gv2.TabStop = False
        '
        'Attachments
        '
        Me.Attachments.Controls.Add(Me.UcAttachment1)
        Me.Attachments.ItemSize = New System.Drawing.SizeF(79.0!, 26.0!)
        Me.Attachments.Location = New System.Drawing.Point(10, 35)
        Me.Attachments.Name = "Attachments"
        Me.Attachments.Size = New System.Drawing.Size(1025, 379)
        Me.Attachments.Text = "Attachments"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(1025, 379)
        Me.UcAttachment1.TabIndex = 1
        Me.UcAttachment1.TabStop = False
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1046, 20)
        Me.RadMenu1.TabIndex = 11
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RMSaveLayout, Me.RMDeleteLayout, Me.EmailSMSSetting})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        '
        'RMSaveLayout
        '
        Me.RMSaveLayout.Name = "RMSaveLayout"
        Me.RMSaveLayout.Text = "Save Layout"
        '
        'RMDeleteLayout
        '
        Me.RMDeleteLayout.Name = "RMDeleteLayout"
        Me.RMDeleteLayout.Text = "Delete Layout"
        '
        'EmailSMSSetting
        '
        Me.EmailSMSSetting.Name = "EmailSMSSetting"
        Me.EmailSMSSetting.Text = "Email/SMS Setting"
        '
        'btnInvoiceJE
        '
        Me.btnInvoiceJE.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnInvoiceJE.Location = New System.Drawing.Point(326, 8)
        Me.btnInvoiceJE.Name = "btnInvoiceJE"
        Me.btnInvoiceJE.Size = New System.Drawing.Size(73, 20)
        Me.btnInvoiceJE.TabIndex = 9
        Me.btnInvoiceJE.Text = "Show JE"
        Me.btnInvoiceJE.Visible = False
        '
        'btnimportdocument
        '
        Me.btnimportdocument.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnimportdocument.Location = New System.Drawing.Point(684, 8)
        Me.btnimportdocument.Name = "btnimportdocument"
        Me.btnimportdocument.Size = New System.Drawing.Size(194, 20)
        Me.btnimportdocument.TabIndex = 8
        Me.btnimportdocument.Text = "Import data from excel sheet"
        '
        'btnDeleteInvoiceafterPost
        '
        Me.btnDeleteInvoiceafterPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDeleteInvoiceafterPost.Location = New System.Drawing.Point(484, 8)
        Me.btnDeleteInvoiceafterPost.Name = "btnDeleteInvoiceafterPost"
        Me.btnDeleteInvoiceafterPost.Size = New System.Drawing.Size(194, 20)
        Me.btnDeleteInvoiceafterPost.TabIndex = 7
        Me.btnDeleteInvoiceafterPost.Text = "Reverse And Unpost"
        '
        'btnPrint
        '
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(247, 8)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(73, 20)
        Me.btnPrint.TabIndex = 3
        Me.btnPrint.Text = "Print"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(965, 8)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(73, 20)
        Me.btnclose.TabIndex = 4
        Me.btnclose.Text = "Close"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(89, 8)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(73, 20)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(168, 8)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(73, 20)
        Me.btndelete.TabIndex = 2
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(10, 8)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(73, 20)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btnPrintJV
        '
        Me.btnPrintJV.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrintJV.Location = New System.Drawing.Point(405, 8)
        Me.btnPrintJV.Name = "btnPrintJV"
        Me.btnPrintJV.Size = New System.Drawing.Size(73, 20)
        Me.btnPrintJV.TabIndex = 10
        Me.btnPrintJV.Text = "Print JV"
        '
        'FrmInvoiceBulkSale
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1046, 483)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmInvoiceBulkSale"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmInvoiceBulkSale"
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
        CType(Me.lblActualTCSTaxBaseAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel58, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txttcstaxbaseamount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTaxAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocumentAmount, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.txtElectronicRefNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtewaybilldate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnEwaybillupdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtEWayBillNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtComment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblComments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.rdbAgainstDispatchTrade, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbAgainstDispatch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblfromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtRoundoff, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblInvoiceStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomerName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotRAmt1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me.btnInvoiceJE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnimportdocument, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDeleteInvoiceafterPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrintJV, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents FndLocationCode As common.UserControls.txtFinder
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents LblLocationName As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents fndCustomerNo As common.UserControls.txtFinder
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents Attachments As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RMSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RMDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents EmailSMSSetting As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents txtDispatchNo As common.UserControls.txtFinder
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents lblTotRAmt1 As common.Controls.MyLabel
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblCustomerName As common.Controls.MyLabel
    Friend WithEvents lblInvoiceStatus As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents TxtRoundoff As common.Controls.MyLabel
    Friend WithEvents rdbAgainstDispatchTrade As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbAgainstDispatch As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblToDate As common.Controls.MyLabel
    Friend WithEvents lblfromDate As common.Controls.MyLabel
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents btnDeleteInvoiceafterPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnimportdocument As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblComments As common.Controls.MyLabel
    Friend WithEvents txtComment As common.Controls.MyTextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnEwaybillupdate As Telerik.WinControls.UI.RadButton
    Friend WithEvents TxtEWayBillNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel20 As common.Controls.MyLabel
    Friend WithEvents txtewaybilldate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtElectronicRefNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents btnInvoiceJE As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageViewPage2 As RadPageViewPage
    Friend WithEvents MyLabel22 As common.Controls.MyLabel
    Friend WithEvents RadLabel25 As common.Controls.MyLabel
    Friend WithEvents lblTaxAmt As common.Controls.MyLabel
    Friend WithEvents lblDocumentAmount As common.Controls.MyLabel
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents rbtnTaxCalManual As common.Controls.MyRadioButton
    Friend WithEvents rbtnTaxCalAutomatic As common.Controls.MyRadioButton
    Friend WithEvents txtTaxGroup As common.UserControls.txtFinder
    Friend WithEvents RadLabel11 As common.Controls.MyLabel
    Friend WithEvents lblTaxGrpName As common.Controls.MyLabel
    Friend WithEvents gv2 As common.UserControls.MyRadGridView
    Friend WithEvents MyLabel57 As common.Controls.MyLabel
    Friend WithEvents lblActualTCSTaxBaseAmt As common.Controls.MyLabel
    Friend WithEvents MyLabel58 As common.Controls.MyLabel
    Friend WithEvents txttcstaxbaseamount As common.MyNumBox
    Friend WithEvents txtTCSTaxRate As common.MyNumBox
    Friend WithEvents btnPrintJV As RadButton
End Class

