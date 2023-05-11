<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDocumentAcceptance
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.LblPurchaseOrder = New common.Controls.MyLabel()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.lblRequest = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.ChkAcceptanceLetter = New Telerik.WinControls.UI.RadCheckBox()
        Me.UsLock1 = New common.usLock()
        Me.ChkA2 = New Telerik.WinControls.UI.RadCheckBox()
        Me.TxtLCAmount = New common.MyNumBox()
        Me.chkTrustReceipt = New Telerik.WinControls.UI.RadCheckBox()
        Me.lblvendor = New common.Controls.MyLabel()
        Me.TxtDueDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.FndVendor = New common.UserControls.txtFinder()
        Me.TXTReferenceNo = New common.Controls.MyTextBox()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.TxtLocationCode = New common.Controls.MyLabel()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.fndLCCreationNo = New common.UserControls.txtFinder()
        Me.lblLocationDesc = New common.Controls.MyLabel()
        Me.cboShipment = New common.Controls.MyComboBox()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.txtdocumentAcceptancedate = New common.Controls.MyDateTimePicker()
        Me.fndDocumentAcceptance = New common.UserControls.txtNavigator()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.TxtAcceptanceReferenceNo = New common.Controls.MyTextBox()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.Attachments = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment
        Me.RDTotal = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadLabel27 = New common.Controls.MyLabel()
        Me.lblTotRAmt = New common.Controls.MyLabel()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RMSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.RMDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnprint = New Telerik.WinControls.UI.RadSplitButton()
        Me.RDA2Letter = New Telerik.WinControls.UI.RadMenuItem()
        Me.RDTrustReceipt = New Telerik.WinControls.UI.RadMenuItem()
        Me.RDAcceptanceLetter = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.TxtPurchaseInvoiceNo = New common.Controls.MyLabel()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.LblPurchaseOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRequest, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkAcceptanceLetter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkA2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtLCAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkTrustReceipt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblvendor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtDueDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TXTReferenceNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtLocationCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboShipment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdocumentAcceptancedate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage3.SuspendLayout()
        CType(Me.TxtAcceptanceReferenceNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Attachments.SuspendLayout()
        Me.RDTotal.SuspendLayout()
        CType(Me.RadLabel27, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotRAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtPurchaseInvoiceNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnprint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(791, 535)
        Me.SplitContainer1.SplitterDistance = 484
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.Controls.Add(Me.Attachments)
        Me.RadPageView1.Controls.Add(Me.RDTotal)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageView1.Location = New System.Drawing.Point(0, 20)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(791, 464)
        Me.RadPageView1.TabIndex = 398
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.TxtPurchaseInvoiceNo)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel16)
        Me.RadPageViewPage1.Controls.Add(Me.LblPurchaseOrder)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel12)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.lblRequest)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.ChkAcceptanceLetter)
        Me.RadPageViewPage1.Controls.Add(Me.UsLock1)
        Me.RadPageViewPage1.Controls.Add(Me.ChkA2)
        Me.RadPageViewPage1.Controls.Add(Me.TxtLCAmount)
        Me.RadPageViewPage1.Controls.Add(Me.chkTrustReceipt)
        Me.RadPageViewPage1.Controls.Add(Me.lblvendor)
        Me.RadPageViewPage1.Controls.Add(Me.TxtDueDate)
        Me.RadPageViewPage1.Controls.Add(Me.FndVendor)
        Me.RadPageViewPage1.Controls.Add(Me.TXTReferenceNo)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel9)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel6)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.TxtLocationCode)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel8)
        Me.RadPageViewPage1.Controls.Add(Me.fndLCCreationNo)
        Me.RadPageViewPage1.Controls.Add(Me.lblLocationDesc)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.cboShipment)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel11)
        Me.RadPageViewPage1.Controls.Add(Me.txtdocumentAcceptancedate)
        Me.RadPageViewPage1.Controls.Add(Me.fndDocumentAcceptance)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.btnnew)
        Me.RadPageViewPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(107.0!, 26.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(770, 418)
        Me.RadPageViewPage1.Text = "Acceptance Letter"
        '
        'LblPurchaseOrder
        '
        Me.LblPurchaseOrder.AutoSize = False
        Me.LblPurchaseOrder.BorderVisible = True
        Me.LblPurchaseOrder.Location = New System.Drawing.Point(162, 122)
        Me.LblPurchaseOrder.Name = "LblPurchaseOrder"
        Me.LblPurchaseOrder.Size = New System.Drawing.Size(184, 18)
        Me.LblPurchaseOrder.TabIndex = 403
        Me.LblPurchaseOrder.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel12
        '
        Me.MyLabel12.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel12.Location = New System.Drawing.Point(12, 123)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(104, 16)
        Me.MyLabel12.TabIndex = 402
        Me.MyLabel12.Text = "Purchase Order No"
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
        Me.RadGroupBox2.Location = New System.Drawing.Point(6, 253)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(761, 162)
        Me.RadGroupBox2.TabIndex = 398
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
        'gv1
        '
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.Size = New System.Drawing.Size(741, 132)
        Me.gv1.TabIndex = 13
        Me.gv1.TabStop = False
        Me.gv1.Text = "RadGridView1"
        '
        'lblRequest
        '
        Me.lblRequest.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblRequest.Location = New System.Drawing.Point(12, 14)
        Me.lblRequest.Name = "lblRequest"
        Me.lblRequest.Size = New System.Drawing.Size(138, 16)
        Me.lblRequest.TabIndex = 360
        Me.lblRequest.Text = "Document Acceptance No"
        '
        'MyLabel3
        '
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel3.Location = New System.Drawing.Point(12, 233)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel3.TabIndex = 394
        Me.MyLabel3.Text = "Documents"
        '
        'MyLabel5
        '
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel5.Location = New System.Drawing.Point(12, 145)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel5.TabIndex = 373
        Me.MyLabel5.Text = "LC Amount"
        '
        'ChkAcceptanceLetter
        '
        Me.ChkAcceptanceLetter.AccessibleDescription = ""
        Me.ChkAcceptanceLetter.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkAcceptanceLetter.Location = New System.Drawing.Point(295, 233)
        Me.ChkAcceptanceLetter.Name = "ChkAcceptanceLetter"
        Me.ChkAcceptanceLetter.Size = New System.Drawing.Size(111, 16)
        Me.ChkAcceptanceLetter.TabIndex = 397
        Me.ChkAcceptanceLetter.Text = "Acceptance Letter"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(504, 12)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(92, 22)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 377
        '
        'ChkA2
        '
        Me.ChkA2.AccessibleDescription = ""
        Me.ChkA2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkA2.Location = New System.Drawing.Point(256, 233)
        Me.ChkA2.Name = "ChkA2"
        Me.ChkA2.Size = New System.Drawing.Size(34, 16)
        Me.ChkA2.TabIndex = 396
        Me.ChkA2.Text = "A2"
        '
        'TxtLCAmount
        '
        Me.TxtLCAmount.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtLCAmount.DecimalPlaces = 2
        Me.TxtLCAmount.Location = New System.Drawing.Point(162, 143)
        Me.TxtLCAmount.MendatroryField = False
        Me.TxtLCAmount.MyLinkLable1 = Nothing
        Me.TxtLCAmount.MyLinkLable2 = Nothing
        Me.TxtLCAmount.Name = "TxtLCAmount"
        Me.TxtLCAmount.Size = New System.Drawing.Size(184, 20)
        Me.TxtLCAmount.TabIndex = 374
        Me.TxtLCAmount.Text = "0"
        Me.TxtLCAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtLCAmount.Value = 0.0R
        '
        'chkTrustReceipt
        '
        Me.chkTrustReceipt.AccessibleDescription = ""
        Me.chkTrustReceipt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTrustReceipt.Location = New System.Drawing.Point(162, 233)
        Me.chkTrustReceipt.Name = "chkTrustReceipt"
        Me.chkTrustReceipt.Size = New System.Drawing.Size(88, 16)
        Me.chkTrustReceipt.TabIndex = 395
        Me.chkTrustReceipt.Text = "Trust Receipt"
        '
        'lblvendor
        '
        Me.lblvendor.AutoSize = False
        Me.lblvendor.BorderVisible = True
        Me.lblvendor.Location = New System.Drawing.Point(352, 79)
        Me.lblvendor.Name = "lblvendor"
        Me.lblvendor.Size = New System.Drawing.Size(331, 19)
        Me.lblvendor.TabIndex = 372
        Me.lblvendor.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtDueDate
        '
        Me.TxtDueDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.TxtDueDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.TxtDueDate.Location = New System.Drawing.Point(162, 189)
        Me.TxtDueDate.MendatroryField = True
        Me.TxtDueDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.TxtDueDate.MyLinkLable1 = Me.MyLabel2
        Me.TxtDueDate.MyLinkLable2 = Nothing
        Me.TxtDueDate.Name = "TxtDueDate"
        Me.TxtDueDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.TxtDueDate.Size = New System.Drawing.Size(184, 18)
        Me.TxtDueDate.TabIndex = 394
        Me.TxtDueDate.TabStop = False
        Me.TxtDueDate.Text = "13/06/2011 11:29 AM"
        Me.TxtDueDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel2.Location = New System.Drawing.Point(12, 36)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(147, 16)
        Me.MyLabel2.TabIndex = 363
        Me.MyLabel2.Text = "Document Acceptance Date"
        '
        'FndVendor
        '
        Me.FndVendor.Enabled = False
        Me.FndVendor.Location = New System.Drawing.Point(162, 79)
        Me.FndVendor.MendatroryField = True
        Me.FndVendor.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndVendor.MyLinkLable1 = Nothing
        Me.FndVendor.MyLinkLable2 = Nothing
        Me.FndVendor.MyReadOnly = False
        Me.FndVendor.MyShowMasterFormButton = False
        Me.FndVendor.Name = "FndVendor"
        Me.FndVendor.Size = New System.Drawing.Size(184, 19)
        Me.FndVendor.TabIndex = 371
        Me.FndVendor.Value = ""
        '
        'TXTReferenceNo
        '
        Me.TXTReferenceNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTReferenceNo.Location = New System.Drawing.Point(162, 210)
        Me.TXTReferenceNo.MaxLength = 200
        Me.TXTReferenceNo.MendatroryField = False
        Me.TXTReferenceNo.MyLinkLable1 = Nothing
        Me.TXTReferenceNo.MyLinkLable2 = Nothing
        Me.TXTReferenceNo.Name = "TXTReferenceNo"
        Me.TXTReferenceNo.Size = New System.Drawing.Size(184, 18)
        Me.TXTReferenceNo.TabIndex = 392
        '
        'MyLabel9
        '
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel9.Location = New System.Drawing.Point(12, 80)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(43, 16)
        Me.MyLabel9.TabIndex = 370
        Me.MyLabel9.Text = "Vendor"
        '
        'MyLabel6
        '
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel6.Location = New System.Drawing.Point(12, 211)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(76, 16)
        Me.MyLabel6.TabIndex = 393
        Me.MyLabel6.Text = "Reference No"
        '
        'MyLabel4
        '
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel4.Location = New System.Drawing.Point(12, 167)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(54, 16)
        Me.MyLabel4.TabIndex = 380
        Me.MyLabel4.Text = "Shipment"
        '
        'TxtLocationCode
        '
        Me.TxtLocationCode.AutoSize = False
        Me.TxtLocationCode.BorderVisible = True
        Me.TxtLocationCode.Location = New System.Drawing.Point(162, 101)
        Me.TxtLocationCode.Name = "TxtLocationCode"
        Me.TxtLocationCode.Size = New System.Drawing.Size(184, 18)
        Me.TxtLocationCode.TabIndex = 391
        Me.TxtLocationCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel8
        '
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel8.Location = New System.Drawing.Point(12, 102)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(49, 16)
        Me.MyLabel8.TabIndex = 389
        Me.MyLabel8.Text = "Location"
        '
        'fndLCCreationNo
        '
        Me.fndLCCreationNo.Location = New System.Drawing.Point(162, 56)
        Me.fndLCCreationNo.MendatroryField = True
        Me.fndLCCreationNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLCCreationNo.MyLinkLable1 = Nothing
        Me.fndLCCreationNo.MyLinkLable2 = Nothing
        Me.fndLCCreationNo.MyReadOnly = False
        Me.fndLCCreationNo.MyShowMasterFormButton = False
        Me.fndLCCreationNo.Name = "fndLCCreationNo"
        Me.fndLCCreationNo.Size = New System.Drawing.Size(184, 19)
        Me.fndLCCreationNo.TabIndex = 366
        Me.fndLCCreationNo.Value = ""
        '
        'lblLocationDesc
        '
        Me.lblLocationDesc.AutoSize = False
        Me.lblLocationDesc.BorderVisible = True
        Me.lblLocationDesc.Location = New System.Drawing.Point(352, 101)
        Me.lblLocationDesc.Name = "lblLocationDesc"
        Me.lblLocationDesc.Size = New System.Drawing.Size(331, 19)
        Me.lblLocationDesc.TabIndex = 390
        Me.lblLocationDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboShipment
        '
        Me.cboShipment.AutoCompleteDisplayMember = Nothing
        Me.cboShipment.AutoCompleteValueMember = Nothing
        Me.cboShipment.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboShipment.Location = New System.Drawing.Point(162, 165)
        Me.cboShipment.MendatroryField = False
        Me.cboShipment.MyLinkLable1 = Nothing
        Me.cboShipment.MyLinkLable2 = Nothing
        Me.cboShipment.Name = "cboShipment"
        Me.cboShipment.Size = New System.Drawing.Size(184, 20)
        Me.cboShipment.TabIndex = 388
        '
        'MyLabel11
        '
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel11.Location = New System.Drawing.Point(12, 189)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(54, 16)
        Me.MyLabel11.TabIndex = 382
        Me.MyLabel11.Text = "Due Date"
        '
        'txtdocumentAcceptancedate
        '
        Me.txtdocumentAcceptancedate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtdocumentAcceptancedate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdocumentAcceptancedate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtdocumentAcceptancedate.Location = New System.Drawing.Point(162, 35)
        Me.txtdocumentAcceptancedate.MendatroryField = True
        Me.txtdocumentAcceptancedate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtdocumentAcceptancedate.MyLinkLable1 = Me.MyLabel2
        Me.txtdocumentAcceptancedate.MyLinkLable2 = Nothing
        Me.txtdocumentAcceptancedate.Name = "txtdocumentAcceptancedate"
        Me.txtdocumentAcceptancedate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtdocumentAcceptancedate.Size = New System.Drawing.Size(184, 18)
        Me.txtdocumentAcceptancedate.TabIndex = 364
        Me.txtdocumentAcceptancedate.TabStop = False
        Me.txtdocumentAcceptancedate.Text = "13/06/2011 11:29 AM"
        Me.txtdocumentAcceptancedate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'fndDocumentAcceptance
        '
        Me.fndDocumentAcceptance.Location = New System.Drawing.Point(162, 13)
        Me.fndDocumentAcceptance.MendatroryField = True
        Me.fndDocumentAcceptance.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndDocumentAcceptance.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndDocumentAcceptance.MyLinkLable1 = Me.lblRequest
        Me.fndDocumentAcceptance.MyLinkLable2 = Nothing
        Me.fndDocumentAcceptance.MyMaxLength = 32767
        Me.fndDocumentAcceptance.MyReadOnly = False
        Me.fndDocumentAcceptance.Name = "fndDocumentAcceptance"
        Me.fndDocumentAcceptance.Size = New System.Drawing.Size(318, 20)
        Me.fndDocumentAcceptance.TabIndex = 361
        Me.fndDocumentAcceptance.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(12, 57)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(84, 16)
        Me.MyLabel1.TabIndex = 365
        Me.MyLabel1.Text = "LC Creation No"
        '
        'btnnew
        '
        Me.btnnew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(483, 13)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(15, 19)
        Me.btnnew.TabIndex = 362
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(84.0!, 26.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(770, 418)
        Me.RadPageViewPage2.Text = "Trust Receipt"
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.TxtAcceptanceReferenceNo)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel7)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(107.0!, 26.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(770, 418)
        Me.RadPageViewPage3.Text = "Acceptance Letter"
        '
        'TxtAcceptanceReferenceNo
        '
        Me.TxtAcceptanceReferenceNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAcceptanceReferenceNo.Location = New System.Drawing.Point(153, 2)
        Me.TxtAcceptanceReferenceNo.MaxLength = 200
        Me.TxtAcceptanceReferenceNo.MendatroryField = False
        Me.TxtAcceptanceReferenceNo.MyLinkLable1 = Nothing
        Me.TxtAcceptanceReferenceNo.MyLinkLable2 = Nothing
        Me.TxtAcceptanceReferenceNo.Name = "TxtAcceptanceReferenceNo"
        Me.TxtAcceptanceReferenceNo.Size = New System.Drawing.Size(184, 18)
        Me.TxtAcceptanceReferenceNo.TabIndex = 394
        '
        'MyLabel7
        '
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel7.Location = New System.Drawing.Point(3, 3)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(107, 16)
        Me.MyLabel7.TabIndex = 395
        Me.MyLabel7.Text = "Acceptance Ref. No"
        '
        'Attachments
        '
        Me.Attachments.Controls.Add(Me.UcAttachment1)
        Me.Attachments.ItemSize = New System.Drawing.SizeF(79.0!, 26.0!)
        Me.Attachments.Location = New System.Drawing.Point(10, 35)
        Me.Attachments.Name = "Attachments"
        Me.Attachments.Size = New System.Drawing.Size(770, 418)
        Me.Attachments.Text = "Attachments"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(770, 418)
        Me.UcAttachment1.TabIndex = 0
        Me.UcAttachment1.TabStop = False
        '
        'RDTotal
        '
        Me.RDTotal.Controls.Add(Me.RadLabel27)
        Me.RDTotal.Controls.Add(Me.lblTotRAmt)
        Me.RDTotal.ItemSize = New System.Drawing.SizeF(41.0!, 26.0!)
        Me.RDTotal.Location = New System.Drawing.Point(10, 35)
        Me.RDTotal.Name = "RDTotal"
        Me.RDTotal.Size = New System.Drawing.Size(770, 418)
        Me.RDTotal.Text = "Total"
        '
        'RadLabel27
        '
        Me.RadLabel27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel27.Location = New System.Drawing.Point(18, 16)
        Me.RadLabel27.Name = "RadLabel27"
        Me.RadLabel27.Size = New System.Drawing.Size(100, 16)
        Me.RadLabel27.TabIndex = 8
        Me.RadLabel27.Text = "Document Amount"
        '
        'lblTotRAmt
        '
        Me.lblTotRAmt.AutoSize = False
        Me.lblTotRAmt.BorderVisible = True
        Me.lblTotRAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotRAmt.Location = New System.Drawing.Point(124, 14)
        Me.lblTotRAmt.Name = "lblTotRAmt"
        Me.lblTotRAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblTotRAmt.TabIndex = 7
        Me.lblTotRAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(791, 20)
        Me.RadMenu1.TabIndex = 399
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Setting"
        Me.RadMenuItem3.AccessibleName = "Setting"
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RMSaveLayout, Me.RMDeleteLayout})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        '
        'RMSaveLayout
        '
        Me.RMSaveLayout.AccessibleDescription = "Save Layout"
        Me.RMSaveLayout.AccessibleName = "Save Layout"
        Me.RMSaveLayout.Name = "RMSaveLayout"
        Me.RMSaveLayout.Text = "Save Layout"
        '
        'RMDeleteLayout
        '
        Me.RMDeleteLayout.AccessibleDescription = "Delete Layout"
        Me.RMDeleteLayout.AccessibleName = "Delete Layout"
        Me.RMDeleteLayout.Name = "RMDeleteLayout"
        Me.RMDeleteLayout.Text = "Delete Layout"
        '
        'btnprint
        '
        Me.btnprint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnprint.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RDA2Letter, Me.RDTrustReceipt, Me.RDAcceptanceLetter})
        Me.btnprint.Location = New System.Drawing.Point(253, 15)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(83, 19)
        Me.btnprint.TabIndex = 8
        Me.btnprint.Text = "Print"
        '
        'RDA2Letter
        '
        Me.RDA2Letter.AccessibleDescription = "A2"
        Me.RDA2Letter.AccessibleName = "A2"
        Me.RDA2Letter.Name = "RDA2Letter"
        Me.RDA2Letter.Text = "A2"
        '
        'RDTrustReceipt
        '
        Me.RDTrustReceipt.AccessibleDescription = "Trust Receipt"
        Me.RDTrustReceipt.AccessibleName = "Trust Receipt"
        Me.RDTrustReceipt.Name = "RDTrustReceipt"
        Me.RDTrustReceipt.Text = "Trust Receipt"
        '
        'RDAcceptanceLetter
        '
        Me.RDAcceptanceLetter.AccessibleDescription = "Acceptance Letter"
        Me.RDAcceptanceLetter.AccessibleName = "Acceptance Letter"
        Me.RDAcceptanceLetter.Name = "RDAcceptanceLetter"
        Me.RDAcceptanceLetter.Text = "Acceptance Letter"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(174, 15)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(73, 20)
        Me.btnPost.TabIndex = 6
        Me.btnPost.Text = "Post"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(12, 15)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(73, 20)
        Me.btnsave.TabIndex = 4
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(715, 15)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(73, 20)
        Me.btnclose.TabIndex = 7
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(93, 15)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(73, 20)
        Me.btndelete.TabIndex = 5
        Me.btndelete.Text = "Delete"
        '
        'TxtPurchaseInvoiceNo
        '
        Me.TxtPurchaseInvoiceNo.AutoSize = False
        Me.TxtPurchaseInvoiceNo.BorderVisible = True
        Me.TxtPurchaseInvoiceNo.Location = New System.Drawing.Point(466, 122)
        Me.TxtPurchaseInvoiceNo.Name = "TxtPurchaseInvoiceNo"
        Me.TxtPurchaseInvoiceNo.Size = New System.Drawing.Size(217, 18)
        Me.TxtPurchaseInvoiceNo.TabIndex = 405
        Me.TxtPurchaseInvoiceNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel16
        '
        Me.MyLabel16.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel16.Location = New System.Drawing.Point(352, 123)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(111, 16)
        Me.MyLabel16.TabIndex = 404
        Me.MyLabel16.Text = "Purchase Invoice No"
        '
        'FrmDocumentAcceptance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(791, 535)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmDocumentAcceptance"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Document Acceptance"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.LblPurchaseOrder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRequest, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkAcceptanceLetter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkA2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtLCAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkTrustReceipt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblvendor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtDueDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TXTReferenceNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtLocationCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboShipment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdocumentAcceptancedate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage3.ResumeLayout(False)
        Me.RadPageViewPage3.PerformLayout()
        CType(Me.TxtAcceptanceReferenceNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Attachments.ResumeLayout(False)
        Me.RDTotal.ResumeLayout(False)
        Me.RDTotal.PerformLayout()
        CType(Me.RadLabel27, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotRAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtPurchaseInvoiceNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents TxtLocationCode As common.Controls.MyLabel
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents lblLocationDesc As common.Controls.MyLabel
    Friend WithEvents cboShipment As common.Controls.MyComboBox
    Friend WithEvents lblRequest As common.Controls.MyLabel
    Friend WithEvents fndDocumentAcceptance As common.UserControls.txtNavigator
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtdocumentAcceptancedate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents fndLCCreationNo As common.UserControls.txtFinder
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents FndVendor As common.UserControls.txtFinder
    Friend WithEvents lblvendor As common.Controls.MyLabel
    Friend WithEvents TxtLCAmount As common.MyNumBox
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents TxtDueDate As common.Controls.MyDateTimePicker
    Friend WithEvents TXTReferenceNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents ChkAcceptanceLetter As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents ChkA2 As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkTrustReceipt As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents Attachments As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents RDTotal As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnprint As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents RDA2Letter As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RDTrustReceipt As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RDAcceptanceLetter As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage3 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents TxtAcceptanceReferenceNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents RadLabel27 As common.Controls.MyLabel
    Friend WithEvents lblTotRAmt As common.Controls.MyLabel
    Friend WithEvents LblPurchaseOrder As common.Controls.MyLabel
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RMSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RMDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents TxtPurchaseInvoiceNo As common.Controls.MyLabel
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
End Class

