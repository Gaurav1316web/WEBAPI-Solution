Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmTDSPayment
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
        Me.lblPayment_No = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.pnlCheque = New System.Windows.Forms.Panel()
        Me.chkCheckPrint = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkPDC = New Telerik.WinControls.UI.RadCheckBox()
        Me.lblchequeno = New common.Controls.MyLabel()
        Me.txtChequeNo = New common.Controls.MyTextBox()
        Me.lblchequedate = New common.Controls.MyLabel()
        Me.dtpChequeDate = New common.Controls.MyDateTimePicker()
        Me.TxtTotalAmount = New common.MyNumBox()
        Me.lblpaymentcode = New common.Controls.MyLabel()
        Me.txtPaymentMode = New common.UserControls.txtFinder()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.fndLocation = New common.UserControls.txtFinder()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.lblLocationName = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.txtBSRCode = New common.Controls.MyTextBox()
        Me.lblChallanDate = New common.Controls.MyLabel()
        Me.lblChallanNo = New common.Controls.MyLabel()
        Me.dtpChallanDate = New common.Controls.MyDateTimePicker()
        Me.txtChallanNo = New common.Controls.MyTextBox()
        Me.txtNatureofDeduction = New common.UserControls.txtMultiSelectFinder()
        Me.lblBankName = New common.Controls.MyLabel()
        Me.FndBankCode = New common.UserControls.txtFinder()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.lblSectionName = New common.Controls.MyLabel()
        Me.fndSectionCode = New common.UserControls.txtFinder()
        Me.Section = New common.Controls.MyLabel()
        Me.lblNatureofDuduction = New common.Controls.MyLabel()
        Me.lblPending = New common.usLock()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.dtptodate = New common.Controls.MyDateTimePicker()
        Me.dtpFromdate = New common.Controls.MyDateTimePicker()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.Attachments = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RdbSavelayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.RdDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnupdate = New Telerik.WinControls.UI.RadButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.lblPayment_No, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlCheque.SuspendLayout()
        CType(Me.chkCheckPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPDC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblchequeno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtChequeNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblchequedate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpChequeDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtTotalAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblpaymentcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBSRCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblChallanDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblChallanNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpChallanDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtChallanNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBankName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSectionName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Section, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNatureofDuduction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtptodate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFromdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Attachments.SuspendLayout()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnupdate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(1141, 508)
        Me.SplitContainer1.SplitterDistance = 476
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.Attachments)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1141, 476)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.GroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.lblPayment_No)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.pnlCheque)
        Me.RadPageViewPage1.Controls.Add(Me.TxtTotalAmount)
        Me.RadPageViewPage1.Controls.Add(Me.lblpaymentcode)
        Me.RadPageViewPage1.Controls.Add(Me.txtPaymentMode)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.fndLocation)
        Me.RadPageViewPage1.Controls.Add(Me.lblLocation)
        Me.RadPageViewPage1.Controls.Add(Me.lblLocationName)
        Me.RadPageViewPage1.Controls.Add(Me.txtNatureofDeduction)
        Me.RadPageViewPage1.Controls.Add(Me.lblBankName)
        Me.RadPageViewPage1.Controls.Add(Me.FndBankCode)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel11)
        Me.RadPageViewPage1.Controls.Add(Me.lblSectionName)
        Me.RadPageViewPage1.Controls.Add(Me.fndSectionCode)
        Me.RadPageViewPage1.Controls.Add(Me.Section)
        Me.RadPageViewPage1.Controls.Add(Me.lblNatureofDuduction)
        Me.RadPageViewPage1.Controls.Add(Me.lblPending)
        Me.RadPageViewPage1.Controls.Add(Me.txtDate)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.txtDocNo)
        Me.RadPageViewPage1.Controls.Add(Me.btnAddNew)
        Me.RadPageViewPage1.Controls.Add(Me.dtptodate)
        Me.RadPageViewPage1.Controls.Add(Me.dtpFromdate)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.btnGo)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(87.0!, 26.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1120, 430)
        Me.RadPageViewPage1.Text = "TDS Payment"
        '
        'lblPayment_No
        '
        Me.lblPayment_No.AutoSize = False
        Me.lblPayment_No.BorderVisible = True
        Me.lblPayment_No.FieldName = Nothing
        Me.lblPayment_No.Location = New System.Drawing.Point(554, 140)
        Me.lblPayment_No.Name = "lblPayment_No"
        Me.lblPayment_No.Size = New System.Drawing.Size(240, 19)
        Me.lblPayment_No.TabIndex = 384
        Me.lblPayment_No.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(480, 141)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(69, 16)
        Me.MyLabel4.TabIndex = 390
        Me.MyLabel4.Text = "Payment No"
        '
        'pnlCheque
        '
        Me.pnlCheque.Controls.Add(Me.chkCheckPrint)
        Me.pnlCheque.Controls.Add(Me.chkPDC)
        Me.pnlCheque.Controls.Add(Me.lblchequeno)
        Me.pnlCheque.Controls.Add(Me.txtChequeNo)
        Me.pnlCheque.Controls.Add(Me.lblchequedate)
        Me.pnlCheque.Controls.Add(Me.dtpChequeDate)
        Me.pnlCheque.Location = New System.Drawing.Point(285, 45)
        Me.pnlCheque.Name = "pnlCheque"
        Me.pnlCheque.Size = New System.Drawing.Size(577, 22)
        Me.pnlCheque.TabIndex = 389
        '
        'chkCheckPrint
        '
        Me.chkCheckPrint.CheckAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.chkCheckPrint.Location = New System.Drawing.Point(189, 2)
        Me.chkCheckPrint.Name = "chkCheckPrint"
        Me.chkCheckPrint.Size = New System.Drawing.Size(77, 18)
        Me.chkCheckPrint.TabIndex = 2
        Me.chkCheckPrint.Text = "Check Print"
        '
        'chkPDC
        '
        Me.chkPDC.Location = New System.Drawing.Point(477, 2)
        Me.chkPDC.Name = "chkPDC"
        Me.chkPDC.Size = New System.Drawing.Size(83, 18)
        Me.chkPDC.TabIndex = 5
        Me.chkPDC.Text = "PDC Cheque"
        '
        'lblchequeno
        '
        Me.lblchequeno.FieldName = Nothing
        Me.lblchequeno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblchequeno.Location = New System.Drawing.Point(1, 3)
        Me.lblchequeno.Name = "lblchequeno"
        Me.lblchequeno.Size = New System.Drawing.Size(83, 16)
        Me.lblchequeno.TabIndex = 0
        Me.lblchequeno.Text = "Cheque/DD No"
        '
        'txtChequeNo
        '
        Me.txtChequeNo.CalculationExpression = Nothing
        Me.txtChequeNo.FieldCode = Nothing
        Me.txtChequeNo.FieldDesc = Nothing
        Me.txtChequeNo.FieldMaxLength = 0
        Me.txtChequeNo.FieldName = Nothing
        Me.txtChequeNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChequeNo.isCalculatedField = False
        Me.txtChequeNo.IsSourceFromTable = False
        Me.txtChequeNo.IsSourceFromValueList = False
        Me.txtChequeNo.IsUnique = False
        Me.txtChequeNo.Location = New System.Drawing.Point(86, 2)
        Me.txtChequeNo.MaxLength = 6
        Me.txtChequeNo.MendatroryField = False
        Me.txtChequeNo.MyLinkLable1 = Nothing
        Me.txtChequeNo.MyLinkLable2 = Nothing
        Me.txtChequeNo.Name = "txtChequeNo"
        Me.txtChequeNo.ReferenceFieldDesc = Nothing
        Me.txtChequeNo.ReferenceFieldName = Nothing
        Me.txtChequeNo.ReferenceTableName = Nothing
        Me.txtChequeNo.Size = New System.Drawing.Size(102, 18)
        Me.txtChequeNo.TabIndex = 1
        '
        'lblchequedate
        '
        Me.lblchequedate.FieldName = Nothing
        Me.lblchequedate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblchequedate.Location = New System.Drawing.Point(272, 3)
        Me.lblchequedate.Name = "lblchequedate"
        Me.lblchequedate.Size = New System.Drawing.Size(92, 16)
        Me.lblchequedate.TabIndex = 3
        Me.lblchequedate.Text = "Cheque/DD Date"
        '
        'dtpChequeDate
        '
        Me.dtpChequeDate.CalculationExpression = Nothing
        Me.dtpChequeDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtpChequeDate.FieldCode = Nothing
        Me.dtpChequeDate.FieldDesc = Nothing
        Me.dtpChequeDate.FieldMaxLength = 0
        Me.dtpChequeDate.FieldName = Nothing
        Me.dtpChequeDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpChequeDate.isCalculatedField = False
        Me.dtpChequeDate.IsSourceFromTable = False
        Me.dtpChequeDate.IsSourceFromValueList = False
        Me.dtpChequeDate.IsUnique = False
        Me.dtpChequeDate.Location = New System.Drawing.Point(368, 1)
        Me.dtpChequeDate.MendatroryField = False
        Me.dtpChequeDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpChequeDate.MyLinkLable1 = Nothing
        Me.dtpChequeDate.MyLinkLable2 = Nothing
        Me.dtpChequeDate.Name = "dtpChequeDate"
        Me.dtpChequeDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpChequeDate.ReferenceFieldDesc = Nothing
        Me.dtpChequeDate.ReferenceFieldName = Nothing
        Me.dtpChequeDate.ReferenceTableName = Nothing
        Me.dtpChequeDate.Size = New System.Drawing.Size(104, 20)
        Me.dtpChequeDate.TabIndex = 4
        Me.dtpChequeDate.TabStop = False
        Me.dtpChequeDate.Text = "10/06/2011 12:34 PM"
        Me.dtpChequeDate.Value = New Date(2011, 6, 10, 12, 34, 10, 140)
        '
        'TxtTotalAmount
        '
        Me.TxtTotalAmount.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtTotalAmount.CalculationExpression = Nothing
        Me.TxtTotalAmount.DecimalPlaces = 2
        Me.TxtTotalAmount.Enabled = False
        Me.TxtTotalAmount.FieldCode = Nothing
        Me.TxtTotalAmount.FieldDesc = Nothing
        Me.TxtTotalAmount.FieldMaxLength = 0
        Me.TxtTotalAmount.FieldName = Nothing
        Me.TxtTotalAmount.isCalculatedField = False
        Me.TxtTotalAmount.IsSourceFromTable = False
        Me.TxtTotalAmount.IsSourceFromValueList = False
        Me.TxtTotalAmount.IsUnique = False
        Me.TxtTotalAmount.Location = New System.Drawing.Point(558, 115)
        Me.TxtTotalAmount.MendatroryField = False
        Me.TxtTotalAmount.MyLinkLable1 = Nothing
        Me.TxtTotalAmount.MyLinkLable2 = Nothing
        Me.TxtTotalAmount.Name = "TxtTotalAmount"
        Me.TxtTotalAmount.ReadOnly = True
        Me.TxtTotalAmount.ReferenceFieldDesc = Nothing
        Me.TxtTotalAmount.ReferenceFieldName = Nothing
        Me.TxtTotalAmount.ReferenceTableName = Nothing
        Me.TxtTotalAmount.Size = New System.Drawing.Size(136, 20)
        Me.TxtTotalAmount.TabIndex = 385
        Me.TxtTotalAmount.Text = "0"
        Me.TxtTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtTotalAmount.Value = 0.0R
        '
        'lblpaymentcode
        '
        Me.lblpaymentcode.FieldName = Nothing
        Me.lblpaymentcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpaymentcode.Location = New System.Drawing.Point(12, 48)
        Me.lblpaymentcode.Name = "lblpaymentcode"
        Me.lblpaymentcode.Size = New System.Drawing.Size(82, 16)
        Me.lblpaymentcode.TabIndex = 387
        Me.lblpaymentcode.Text = "Payment Mode"
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
        Me.txtPaymentMode.Location = New System.Drawing.Point(132, 47)
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
        Me.txtPaymentMode.Size = New System.Drawing.Size(143, 19)
        Me.txtPaymentMode.TabIndex = 388
        Me.txtPaymentMode.Value = ""
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel3.Location = New System.Drawing.Point(482, 117)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(74, 16)
        Me.MyLabel3.TabIndex = 386
        Me.MyLabel3.Text = "Total Amount"
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
        Me.fndLocation.Location = New System.Drawing.Point(132, 93)
        Me.fndLocation.MendatroryField = False
        Me.fndLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLocation.MyLinkLable1 = Nothing
        Me.fndLocation.MyLinkLable2 = Nothing
        Me.fndLocation.MyReadOnly = False
        Me.fndLocation.MyShowMasterFormButton = False
        Me.fndLocation.Name = "fndLocation"
        Me.fndLocation.ReferenceFieldDesc = Nothing
        Me.fndLocation.ReferenceFieldName = Nothing
        Me.fndLocation.ReferenceTableName = Nothing
        Me.fndLocation.Size = New System.Drawing.Size(143, 19)
        Me.fndLocation.TabIndex = 384
        Me.fndLocation.Value = ""
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(12, 94)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 16)
        Me.lblLocation.TabIndex = 382
        Me.lblLocation.Text = "Location"
        '
        'lblLocationName
        '
        Me.lblLocationName.AutoSize = False
        Me.lblLocationName.BorderVisible = True
        Me.lblLocationName.FieldName = Nothing
        Me.lblLocationName.Location = New System.Drawing.Point(281, 93)
        Me.lblLocationName.Name = "lblLocationName"
        Me.lblLocationName.Size = New System.Drawing.Size(316, 19)
        Me.lblLocationName.TabIndex = 383
        Me.lblLocationName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(6, 41)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(60, 16)
        Me.MyLabel7.TabIndex = 380
        Me.MyLabel7.Text = "BSR Code"
        '
        'txtBSRCode
        '
        Me.txtBSRCode.CalculationExpression = Nothing
        Me.txtBSRCode.FieldCode = Nothing
        Me.txtBSRCode.FieldDesc = Nothing
        Me.txtBSRCode.FieldMaxLength = 0
        Me.txtBSRCode.FieldName = Nothing
        Me.txtBSRCode.isCalculatedField = False
        Me.txtBSRCode.IsSourceFromTable = False
        Me.txtBSRCode.IsSourceFromValueList = False
        Me.txtBSRCode.IsUnique = False
        Me.txtBSRCode.Location = New System.Drawing.Point(114, 40)
        Me.txtBSRCode.MaxLength = 50
        Me.txtBSRCode.MendatroryField = False
        Me.txtBSRCode.MyLinkLable1 = Nothing
        Me.txtBSRCode.MyLinkLable2 = Nothing
        Me.txtBSRCode.Name = "txtBSRCode"
        Me.txtBSRCode.ReferenceFieldDesc = Nothing
        Me.txtBSRCode.ReferenceFieldName = Nothing
        Me.txtBSRCode.ReferenceTableName = Nothing
        Me.txtBSRCode.Size = New System.Drawing.Size(271, 20)
        Me.txtBSRCode.TabIndex = 381
        '
        'lblChallanDate
        '
        Me.lblChallanDate.FieldName = Nothing
        Me.lblChallanDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChallanDate.Location = New System.Drawing.Point(399, 19)
        Me.lblChallanDate.Name = "lblChallanDate"
        Me.lblChallanDate.Size = New System.Drawing.Size(72, 16)
        Me.lblChallanDate.TabIndex = 377
        Me.lblChallanDate.Text = "Challan Date"
        '
        'lblChallanNo
        '
        Me.lblChallanNo.FieldName = Nothing
        Me.lblChallanNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChallanNo.Location = New System.Drawing.Point(6, 19)
        Me.lblChallanNo.Name = "lblChallanNo"
        Me.lblChallanNo.Size = New System.Drawing.Size(62, 16)
        Me.lblChallanNo.TabIndex = 376
        Me.lblChallanNo.Text = "Challan No"
        '
        'dtpChallanDate
        '
        Me.dtpChallanDate.CalculationExpression = Nothing
        Me.dtpChallanDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpChallanDate.FieldCode = Nothing
        Me.dtpChallanDate.FieldDesc = Nothing
        Me.dtpChallanDate.FieldMaxLength = 0
        Me.dtpChallanDate.FieldName = Nothing
        Me.dtpChallanDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpChallanDate.isCalculatedField = False
        Me.dtpChallanDate.IsSourceFromTable = False
        Me.dtpChallanDate.IsSourceFromValueList = False
        Me.dtpChallanDate.IsUnique = False
        Me.dtpChallanDate.Location = New System.Drawing.Point(474, 16)
        Me.dtpChallanDate.MendatroryField = False
        Me.dtpChallanDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpChallanDate.MyLinkLable1 = Nothing
        Me.dtpChallanDate.MyLinkLable2 = Nothing
        Me.dtpChallanDate.Name = "dtpChallanDate"
        Me.dtpChallanDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpChallanDate.ReferenceFieldDesc = Nothing
        Me.dtpChallanDate.ReferenceFieldName = Nothing
        Me.dtpChallanDate.ReferenceTableName = Nothing
        Me.dtpChallanDate.Size = New System.Drawing.Size(117, 20)
        Me.dtpChallanDate.TabIndex = 379
        Me.dtpChallanDate.TabStop = False
        Me.dtpChallanDate.Text = "10/06/2011"
        Me.dtpChallanDate.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'txtChallanNo
        '
        Me.txtChallanNo.CalculationExpression = Nothing
        Me.txtChallanNo.FieldCode = Nothing
        Me.txtChallanNo.FieldDesc = Nothing
        Me.txtChallanNo.FieldMaxLength = 0
        Me.txtChallanNo.FieldName = Nothing
        Me.txtChallanNo.isCalculatedField = False
        Me.txtChallanNo.IsSourceFromTable = False
        Me.txtChallanNo.IsSourceFromValueList = False
        Me.txtChallanNo.IsUnique = False
        Me.txtChallanNo.Location = New System.Drawing.Point(114, 16)
        Me.txtChallanNo.MaxLength = 50
        Me.txtChallanNo.MendatroryField = False
        Me.txtChallanNo.MyLinkLable1 = Nothing
        Me.txtChallanNo.MyLinkLable2 = Nothing
        Me.txtChallanNo.Name = "txtChallanNo"
        Me.txtChallanNo.ReferenceFieldDesc = Nothing
        Me.txtChallanNo.ReferenceFieldName = Nothing
        Me.txtChallanNo.ReferenceTableName = Nothing
        Me.txtChallanNo.Size = New System.Drawing.Size(271, 20)
        Me.txtChallanNo.TabIndex = 378
        '
        'txtNatureofDeduction
        '
        Me.txtNatureofDeduction.arrDispalyMember = Nothing
        Me.txtNatureofDeduction.arrValueMember = Nothing
        Me.txtNatureofDeduction.Location = New System.Drawing.Point(132, 116)
        Me.txtNatureofDeduction.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNatureofDeduction.MyLinkLable1 = Nothing
        Me.txtNatureofDeduction.MyLinkLable2 = Nothing
        Me.txtNatureofDeduction.MyNullText = "All"
        Me.txtNatureofDeduction.Name = "txtNatureofDeduction"
        Me.txtNatureofDeduction.Size = New System.Drawing.Size(344, 19)
        Me.txtNatureofDeduction.TabIndex = 375
        '
        'lblBankName
        '
        Me.lblBankName.AutoSize = False
        Me.lblBankName.BorderVisible = True
        Me.lblBankName.FieldName = Nothing
        Me.lblBankName.Location = New System.Drawing.Point(281, 24)
        Me.lblBankName.Name = "lblBankName"
        Me.lblBankName.Size = New System.Drawing.Size(315, 19)
        Me.lblBankName.TabIndex = 366
        '
        'FndBankCode
        '
        Me.FndBankCode.CalculationExpression = Nothing
        Me.FndBankCode.FieldCode = Nothing
        Me.FndBankCode.FieldDesc = Nothing
        Me.FndBankCode.FieldMaxLength = 0
        Me.FndBankCode.FieldName = Nothing
        Me.FndBankCode.isCalculatedField = False
        Me.FndBankCode.IsSourceFromTable = False
        Me.FndBankCode.IsSourceFromValueList = False
        Me.FndBankCode.IsUnique = False
        Me.FndBankCode.Location = New System.Drawing.Point(132, 24)
        Me.FndBankCode.MendatroryField = True
        Me.FndBankCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndBankCode.MyLinkLable1 = Me.MyLabel11
        Me.FndBankCode.MyLinkLable2 = Me.lblBankName
        Me.FndBankCode.MyReadOnly = False
        Me.FndBankCode.MyShowMasterFormButton = False
        Me.FndBankCode.Name = "FndBankCode"
        Me.FndBankCode.ReferenceFieldDesc = Nothing
        Me.FndBankCode.ReferenceFieldName = Nothing
        Me.FndBankCode.ReferenceTableName = Nothing
        Me.FndBankCode.Size = New System.Drawing.Size(143, 19)
        Me.FndBankCode.TabIndex = 365
        Me.FndBankCode.Value = ""
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Location = New System.Drawing.Point(12, 24)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(30, 18)
        Me.MyLabel11.TabIndex = 364
        Me.MyLabel11.Text = "Bank"
        '
        'lblSectionName
        '
        Me.lblSectionName.AutoSize = False
        Me.lblSectionName.BorderVisible = True
        Me.lblSectionName.FieldName = Nothing
        Me.lblSectionName.Location = New System.Drawing.Point(281, 70)
        Me.lblSectionName.Name = "lblSectionName"
        Me.lblSectionName.Size = New System.Drawing.Size(315, 19)
        Me.lblSectionName.TabIndex = 363
        '
        'fndSectionCode
        '
        Me.fndSectionCode.CalculationExpression = Nothing
        Me.fndSectionCode.FieldCode = Nothing
        Me.fndSectionCode.FieldDesc = Nothing
        Me.fndSectionCode.FieldMaxLength = 0
        Me.fndSectionCode.FieldName = Nothing
        Me.fndSectionCode.isCalculatedField = False
        Me.fndSectionCode.IsSourceFromTable = False
        Me.fndSectionCode.IsSourceFromValueList = False
        Me.fndSectionCode.IsUnique = False
        Me.fndSectionCode.Location = New System.Drawing.Point(132, 70)
        Me.fndSectionCode.MendatroryField = True
        Me.fndSectionCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndSectionCode.MyLinkLable1 = Me.Section
        Me.fndSectionCode.MyLinkLable2 = Me.lblSectionName
        Me.fndSectionCode.MyReadOnly = False
        Me.fndSectionCode.MyShowMasterFormButton = False
        Me.fndSectionCode.Name = "fndSectionCode"
        Me.fndSectionCode.ReferenceFieldDesc = Nothing
        Me.fndSectionCode.ReferenceFieldName = Nothing
        Me.fndSectionCode.ReferenceTableName = Nothing
        Me.fndSectionCode.Size = New System.Drawing.Size(143, 19)
        Me.fndSectionCode.TabIndex = 362
        Me.fndSectionCode.Value = ""
        '
        'Section
        '
        Me.Section.FieldName = Nothing
        Me.Section.Location = New System.Drawing.Point(12, 70)
        Me.Section.Name = "Section"
        Me.Section.Size = New System.Drawing.Size(43, 18)
        Me.Section.TabIndex = 361
        Me.Section.Text = "Section"
        '
        'lblNatureofDuduction
        '
        Me.lblNatureofDuduction.FieldName = Nothing
        Me.lblNatureofDuduction.Location = New System.Drawing.Point(12, 116)
        Me.lblNatureofDuduction.Name = "lblNatureofDuduction"
        Me.lblNatureofDuduction.Size = New System.Drawing.Size(80, 18)
        Me.lblNatureofDuduction.TabIndex = 358
        Me.lblNatureofDuduction.Text = "Nature of Ded."
        '
        'lblPending
        '
        Me.lblPending.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lblPending.Location = New System.Drawing.Point(590, -2)
        Me.lblPending.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPending.Name = "lblPending"
        Me.lblPending.Size = New System.Drawing.Size(92, 24)
        Me.lblPending.Status = common.ERPTransactionStatus.Pending
        Me.lblPending.TabIndex = 355
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
        Me.txtDate.Location = New System.Drawing.Point(445, 1)
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
        Me.txtDate.TabIndex = 351
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011 11:29 AM"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(414, 2)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 354
        Me.RadLabel4.Text = "Date"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(12, 2)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(75, 16)
        Me.MyLabel1.TabIndex = 353
        Me.MyLabel1.Text = "Document No"
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(132, 1)
        Me.txtDocNo.MendatroryField = False
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Me.MyLabel1
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 32767
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(252, 19)
        Me.txtDocNo.TabIndex = 350
        Me.txtDocNo.Value = ""
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(386, 0)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 21)
        Me.btnAddNew.TabIndex = 352
        '
        'dtptodate
        '
        Me.dtptodate.CalculationExpression = Nothing
        Me.dtptodate.CustomFormat = "dd/MM/yyyy"
        Me.dtptodate.FieldCode = Nothing
        Me.dtptodate.FieldDesc = Nothing
        Me.dtptodate.FieldMaxLength = 0
        Me.dtptodate.FieldName = Nothing
        Me.dtptodate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtptodate.isCalculatedField = False
        Me.dtptodate.IsSourceFromTable = False
        Me.dtptodate.IsSourceFromValueList = False
        Me.dtptodate.IsUnique = False
        Me.dtptodate.Location = New System.Drawing.Point(336, 139)
        Me.dtptodate.MendatroryField = False
        Me.dtptodate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtptodate.MyLinkLable1 = Nothing
        Me.dtptodate.MyLinkLable2 = Nothing
        Me.dtptodate.Name = "dtptodate"
        Me.dtptodate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtptodate.ReferenceFieldDesc = Nothing
        Me.dtptodate.ReferenceFieldName = Nothing
        Me.dtptodate.ReferenceTableName = Nothing
        Me.dtptodate.Size = New System.Drawing.Size(129, 20)
        Me.dtptodate.TabIndex = 11
        Me.dtptodate.TabStop = False
        Me.dtptodate.Text = "05/08/2011"
        Me.dtptodate.Value = New Date(2011, 8, 5, 17, 38, 42, 656)
        '
        'dtpFromdate
        '
        Me.dtpFromdate.CalculationExpression = Nothing
        Me.dtpFromdate.CustomFormat = "dd/MM/yyyy"
        Me.dtpFromdate.FieldCode = Nothing
        Me.dtpFromdate.FieldDesc = Nothing
        Me.dtpFromdate.FieldMaxLength = 0
        Me.dtpFromdate.FieldName = Nothing
        Me.dtpFromdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromdate.isCalculatedField = False
        Me.dtpFromdate.IsSourceFromTable = False
        Me.dtpFromdate.IsSourceFromValueList = False
        Me.dtpFromdate.IsUnique = False
        Me.dtpFromdate.Location = New System.Drawing.Point(132, 139)
        Me.dtpFromdate.MendatroryField = False
        Me.dtpFromdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromdate.MyLinkLable1 = Nothing
        Me.dtpFromdate.MyLinkLable2 = Nothing
        Me.dtpFromdate.Name = "dtpFromdate"
        Me.dtpFromdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromdate.ReferenceFieldDesc = Nothing
        Me.dtpFromdate.ReferenceFieldName = Nothing
        Me.dtpFromdate.ReferenceTableName = Nothing
        Me.dtpFromdate.Size = New System.Drawing.Size(129, 20)
        Me.dtpFromdate.TabIndex = 10
        Me.dtpFromdate.TabStop = False
        Me.dtpFromdate.Text = "05/08/2011"
        Me.dtpFromdate.Value = New Date(2011, 8, 5, 17, 38, 42, 656)
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(281, 140)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(45, 18)
        Me.MyLabel2.TabIndex = 12
        Me.MyLabel2.Text = "To Date"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(12, 140)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel1.TabIndex = 13
        Me.RadLabel1.Text = "From Date"
        '
        'btnGo
        '
        Me.btnGo.Location = New System.Drawing.Point(625, 185)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(73, 20)
        Me.btnGo.TabIndex = 6
        Me.btnGo.Text = ">>>>"
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
        Me.RadGroupBox2.Location = New System.Drawing.Point(2, 230)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(1115, 196)
        Me.RadGroupBox2.TabIndex = 7
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
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(1095, 166)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        Me.gv1.Text = "RadGridView1"
        '
        'Attachments
        '
        Me.Attachments.Controls.Add(Me.UcAttachment1)
        Me.Attachments.ItemSize = New System.Drawing.SizeF(79.0!, 26.0!)
        Me.Attachments.Location = New System.Drawing.Point(10, 35)
        Me.Attachments.Name = "Attachments"
        Me.Attachments.Size = New System.Drawing.Size(1120, 399)
        Me.Attachments.Text = "Attachments"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(1120, 399)
        Me.UcAttachment1.TabIndex = 0
        Me.UcAttachment1.TabStop = False
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(175, 5)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(73, 20)
        Me.btnPost.TabIndex = 12
        Me.btnPost.Text = "Post"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(96, 5)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(73, 20)
        Me.btndelete.TabIndex = 11
        Me.btndelete.Text = "Delete"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(1062, 4)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(73, 20)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(8, 5)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(84, 20)
        Me.btnsave.TabIndex = 1
        Me.btnsave.Text = "Save"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1141, 20)
        Me.RadMenu1.TabIndex = 12
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Setting"
        Me.RadMenuItem3.AccessibleName = "Setting"
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RdbSavelayout, Me.RdDeleteLayout})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        '
        'RdbSavelayout
        '
        Me.RdbSavelayout.AccessibleDescription = "Save Layout"
        Me.RdbSavelayout.AccessibleName = "Save Layout"
        Me.RdbSavelayout.Name = "RdbSavelayout"
        Me.RdbSavelayout.Text = "Save Layout"
        '
        'RdDeleteLayout
        '
        Me.RdDeleteLayout.AccessibleDescription = "Delete Layout"
        Me.RdDeleteLayout.AccessibleName = "Delete Layout"
        Me.RdDeleteLayout.Name = "RdDeleteLayout"
        Me.RdDeleteLayout.Text = "Delete Layout"
        '
        'btnupdate
        '
        Me.btnupdate.Enabled = False
        Me.btnupdate.Location = New System.Drawing.Point(420, 41)
        Me.btnupdate.Name = "btnupdate"
        Me.btnupdate.Size = New System.Drawing.Size(133, 20)
        Me.btnupdate.TabIndex = 391
        Me.btnupdate.Text = "Update After Posting"
        Me.btnupdate.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblChallanNo)
        Me.GroupBox1.Controls.Add(Me.btnupdate)
        Me.GroupBox1.Controls.Add(Me.MyLabel7)
        Me.GroupBox1.Controls.Add(Me.txtChallanNo)
        Me.GroupBox1.Controls.Add(Me.txtBSRCode)
        Me.GroupBox1.Controls.Add(Me.dtpChallanDate)
        Me.GroupBox1.Controls.Add(Me.lblChallanDate)
        Me.GroupBox1.Location = New System.Drawing.Point(15, 161)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(604, 66)
        Me.GroupBox1.TabIndex = 393
        Me.GroupBox1.TabStop = False
        '
        'FrmTDSPayment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1141, 528)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmTDSPayment"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "TDS Payment"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.lblPayment_No, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlCheque.ResumeLayout(False)
        Me.pnlCheque.PerformLayout()
        CType(Me.chkCheckPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPDC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblchequeno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtChequeNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblchequedate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpChequeDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtTotalAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblpaymentcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBSRCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblChallanDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblChallanNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpChallanDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtChallanNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBankName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSectionName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Section, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNatureofDuduction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtptodate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFromdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Attachments.ResumeLayout(False)
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnupdate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents Attachments As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents RdbSavelayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RdDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents dtptodate As common.Controls.MyDateTimePicker
    Friend WithEvents dtpFromdate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents lblPending As common.usLock
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblBankName As common.Controls.MyLabel
    Friend WithEvents FndBankCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents lblSectionName As common.Controls.MyLabel
    Friend WithEvents fndSectionCode As common.UserControls.txtFinder
    Friend WithEvents Section As common.Controls.MyLabel
    Friend WithEvents lblNatureofDuduction As common.Controls.MyLabel
    Friend WithEvents txtNatureofDeduction As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents txtBSRCode As common.Controls.MyTextBox
    Friend WithEvents lblChallanDate As common.Controls.MyLabel
    Friend WithEvents lblChallanNo As common.Controls.MyLabel
    Friend WithEvents dtpChallanDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtChallanNo As common.Controls.MyTextBox
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndLocation As common.UserControls.txtFinder
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents lblLocationName As common.Controls.MyLabel
    Friend WithEvents TxtTotalAmount As common.MyNumBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents pnlCheque As System.Windows.Forms.Panel
    Friend WithEvents chkCheckPrint As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkPDC As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents lblchequeno As common.Controls.MyLabel
    Friend WithEvents txtChequeNo As common.Controls.MyTextBox
    Friend WithEvents lblchequedate As common.Controls.MyLabel
    Friend WithEvents dtpChequeDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblpaymentcode As common.Controls.MyLabel
    Friend WithEvents txtPaymentMode As common.UserControls.txtFinder
    Friend WithEvents lblPayment_No As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents btnupdate As Telerik.WinControls.UI.RadButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
End Class

