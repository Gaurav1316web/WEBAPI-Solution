<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFarmerPaymentEntry
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.dtp_chequedate = New common.Controls.MyDateTimePicker()
        Me.txtchequeno = New common.Controls.MyTextBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.txtBankCode = New common.Controls.MyTextBox()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.txtdescription = New common.Controls.MyTextBox()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtPaymentCode = New common.Controls.MyTextBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtPayment_Amount = New common.Controls.MyTextBox()
        Me.MyLabel15 = New common.Controls.MyLabel()
        Me.dtpfarmerinvoice = New common.Controls.MyDateTimePicker()
        Me.txtFarmerCode = New common.Controls.MyTextBox()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.lblFarmerName = New common.Controls.MyLabel()
        Me.txtfarmerinvoice = New common.Controls.MyTextBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.lblVLCDesc = New common.Controls.MyLabel()
        Me.txtVSPCode = New common.Controls.MyTextBox()
        Me.txtMccCode = New common.Controls.MyTextBox()
        Me.lblMccName = New common.Controls.MyLabel()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.lblMCCCode = New common.Controls.MyLabel()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.txtVLCCode = New common.Controls.MyTextBox()
        Me.lblVSPCode = New common.Controls.MyLabel()
        Me.lblVSPDesc = New common.Controls.MyLabel()
        Me.dtpPayment = New common.Controls.MyDateTimePicker()
        Me.lblpaymentno = New common.Controls.MyLabel()
        Me.lblpaymentdate = New common.Controls.MyLabel()
        Me.txtPaymentNo = New common.UserControls.txtNavigator()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.btnreset = New Telerik.WinControls.UI.RadButton()
        Me.btnprint = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dtp_chequedate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtchequeno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBankCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPaymentCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPayment_Amount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpfarmerinvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFarmerCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFarmerName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtfarmerinvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVLCDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVSPCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMccCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMccName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMCCCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVLCCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVSPCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVSPDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpPayment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblpaymentno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblpaymentdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnreset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnprint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(1136, 538)
        Me.SplitContainer1.SplitterDistance = 509
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 20)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1136, 489)
        Me.RadPageView1.TabIndex = 0
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.GroupBox1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(60.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1115, 441)
        Me.RadPageViewPage1.Text = "Payment"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.dtp_chequedate)
        Me.GroupBox1.Controls.Add(Me.txtchequeno)
        Me.GroupBox1.Controls.Add(Me.MyLabel9)
        Me.GroupBox1.Controls.Add(Me.MyLabel13)
        Me.GroupBox1.Controls.Add(Me.txtBankCode)
        Me.GroupBox1.Controls.Add(Me.MyLabel6)
        Me.GroupBox1.Controls.Add(Me.txtdescription)
        Me.GroupBox1.Controls.Add(Me.MyLabel5)
        Me.GroupBox1.Controls.Add(Me.txtPaymentCode)
        Me.GroupBox1.Controls.Add(Me.MyLabel1)
        Me.GroupBox1.Controls.Add(Me.txtPayment_Amount)
        Me.GroupBox1.Controls.Add(Me.MyLabel15)
        Me.GroupBox1.Controls.Add(Me.dtpfarmerinvoice)
        Me.GroupBox1.Controls.Add(Me.txtFarmerCode)
        Me.GroupBox1.Controls.Add(Me.MyLabel11)
        Me.GroupBox1.Controls.Add(Me.MyLabel12)
        Me.GroupBox1.Controls.Add(Me.lblFarmerName)
        Me.GroupBox1.Controls.Add(Me.txtfarmerinvoice)
        Me.GroupBox1.Controls.Add(Me.MyLabel2)
        Me.GroupBox1.Controls.Add(Me.MyLabel4)
        Me.GroupBox1.Controls.Add(Me.lblVLCDesc)
        Me.GroupBox1.Controls.Add(Me.txtVSPCode)
        Me.GroupBox1.Controls.Add(Me.txtMccCode)
        Me.GroupBox1.Controls.Add(Me.lblMccName)
        Me.GroupBox1.Controls.Add(Me.MyLabel10)
        Me.GroupBox1.Controls.Add(Me.MyLabel7)
        Me.GroupBox1.Controls.Add(Me.lblMCCCode)
        Me.GroupBox1.Controls.Add(Me.MyLabel3)
        Me.GroupBox1.Controls.Add(Me.RadLabel3)
        Me.GroupBox1.Controls.Add(Me.txtVLCCode)
        Me.GroupBox1.Controls.Add(Me.lblVSPCode)
        Me.GroupBox1.Controls.Add(Me.lblVSPDesc)
        Me.GroupBox1.Controls.Add(Me.dtpPayment)
        Me.GroupBox1.Controls.Add(Me.lblpaymentno)
        Me.GroupBox1.Controls.Add(Me.lblpaymentdate)
        Me.GroupBox1.Controls.Add(Me.txtPaymentNo)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1115, 441)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'dtp_chequedate
        '
        Me.dtp_chequedate.CalculationExpression = Nothing
        Me.dtp_chequedate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtp_chequedate.FieldCode = Nothing
        Me.dtp_chequedate.FieldDesc = Nothing
        Me.dtp_chequedate.FieldMaxLength = 0
        Me.dtp_chequedate.FieldName = Nothing
        Me.dtp_chequedate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_chequedate.isCalculatedField = False
        Me.dtp_chequedate.IsSourceFromTable = False
        Me.dtp_chequedate.IsSourceFromValueList = False
        Me.dtp_chequedate.IsUnique = False
        Me.dtp_chequedate.Location = New System.Drawing.Point(556, 155)
        Me.dtp_chequedate.MendatroryField = False
        Me.dtp_chequedate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtp_chequedate.MyLinkLable1 = Nothing
        Me.dtp_chequedate.MyLinkLable2 = Nothing
        Me.dtp_chequedate.Name = "dtp_chequedate"
        Me.dtp_chequedate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtp_chequedate.ReferenceFieldDesc = Nothing
        Me.dtp_chequedate.ReferenceFieldName = Nothing
        Me.dtp_chequedate.ReferenceTableName = Nothing
        Me.dtp_chequedate.Size = New System.Drawing.Size(80, 20)
        Me.dtp_chequedate.TabIndex = 114
        Me.dtp_chequedate.TabStop = False
        Me.dtp_chequedate.Text = "10/06/2011 11:51 AM"
        Me.dtp_chequedate.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'txtchequeno
        '
        Me.txtchequeno.CalculationExpression = Nothing
        Me.txtchequeno.FieldCode = Nothing
        Me.txtchequeno.FieldDesc = Nothing
        Me.txtchequeno.FieldMaxLength = 0
        Me.txtchequeno.FieldName = Nothing
        Me.txtchequeno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtchequeno.isCalculatedField = False
        Me.txtchequeno.IsSourceFromTable = False
        Me.txtchequeno.IsSourceFromValueList = False
        Me.txtchequeno.IsUnique = False
        Me.txtchequeno.Location = New System.Drawing.Point(110, 157)
        Me.txtchequeno.MaxLength = 200
        Me.txtchequeno.MendatroryField = False
        Me.txtchequeno.MyLinkLable1 = Me.MyLabel3
        Me.txtchequeno.MyLinkLable2 = Nothing
        Me.txtchequeno.Name = "txtchequeno"
        Me.txtchequeno.ReadOnly = True
        Me.txtchequeno.ReferenceFieldDesc = Nothing
        Me.txtchequeno.ReferenceFieldName = Nothing
        Me.txtchequeno.ReferenceTableName = Nothing
        Me.txtchequeno.Size = New System.Drawing.Size(321, 18)
        Me.txtchequeno.TabIndex = 113
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(6, 111)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(58, 16)
        Me.MyLabel3.TabIndex = 88
        Me.MyLabel3.Text = "VLC Code"
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel9.Location = New System.Drawing.Point(438, 156)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(71, 18)
        Me.MyLabel9.TabIndex = 112
        Me.MyLabel9.Text = "Cheque Date"
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel13.Location = New System.Drawing.Point(6, 157)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(63, 18)
        Me.MyLabel13.TabIndex = 111
        Me.MyLabel13.Text = "Cheque No"
        '
        'txtBankCode
        '
        Me.txtBankCode.CalculationExpression = Nothing
        Me.txtBankCode.FieldCode = Nothing
        Me.txtBankCode.FieldDesc = Nothing
        Me.txtBankCode.FieldMaxLength = 0
        Me.txtBankCode.FieldName = Nothing
        Me.txtBankCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankCode.isCalculatedField = False
        Me.txtBankCode.IsSourceFromTable = False
        Me.txtBankCode.IsSourceFromValueList = False
        Me.txtBankCode.IsUnique = False
        Me.txtBankCode.Location = New System.Drawing.Point(111, 180)
        Me.txtBankCode.MaxLength = 200
        Me.txtBankCode.MendatroryField = False
        Me.txtBankCode.MyLinkLable1 = Me.MyLabel3
        Me.txtBankCode.MyLinkLable2 = Nothing
        Me.txtBankCode.Name = "txtBankCode"
        Me.txtBankCode.ReadOnly = True
        Me.txtBankCode.ReferenceFieldDesc = Nothing
        Me.txtBankCode.ReferenceFieldName = Nothing
        Me.txtBankCode.ReferenceTableName = Nothing
        Me.txtBankCode.Size = New System.Drawing.Size(321, 18)
        Me.txtBankCode.TabIndex = 110
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel6.Location = New System.Drawing.Point(6, 180)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(60, 18)
        Me.MyLabel6.TabIndex = 109
        Me.MyLabel6.Text = "Bank Code"
        '
        'txtdescription
        '
        Me.txtdescription.CalculationExpression = Nothing
        Me.txtdescription.FieldCode = Nothing
        Me.txtdescription.FieldDesc = Nothing
        Me.txtdescription.FieldMaxLength = 0
        Me.txtdescription.FieldName = Nothing
        Me.txtdescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdescription.isCalculatedField = False
        Me.txtdescription.IsSourceFromTable = False
        Me.txtdescription.IsSourceFromValueList = False
        Me.txtdescription.IsUnique = False
        Me.txtdescription.Location = New System.Drawing.Point(111, 229)
        Me.txtdescription.MaxLength = 200
        Me.txtdescription.MendatroryField = False
        Me.txtdescription.MyLinkLable1 = Me.MyLabel3
        Me.txtdescription.MyLinkLable2 = Nothing
        Me.txtdescription.Name = "txtdescription"
        Me.txtdescription.ReadOnly = True
        Me.txtdescription.ReferenceFieldDesc = Nothing
        Me.txtdescription.ReferenceFieldName = Nothing
        Me.txtdescription.ReferenceTableName = Nothing
        Me.txtdescription.Size = New System.Drawing.Size(321, 18)
        Me.txtdescription.TabIndex = 108
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel5.Location = New System.Drawing.Point(6, 229)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(63, 18)
        Me.MyLabel5.TabIndex = 107
        Me.MyLabel5.Text = "Description"
        '
        'txtPaymentCode
        '
        Me.txtPaymentCode.CalculationExpression = Nothing
        Me.txtPaymentCode.FieldCode = Nothing
        Me.txtPaymentCode.FieldDesc = Nothing
        Me.txtPaymentCode.FieldMaxLength = 0
        Me.txtPaymentCode.FieldName = Nothing
        Me.txtPaymentCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPaymentCode.isCalculatedField = False
        Me.txtPaymentCode.IsSourceFromTable = False
        Me.txtPaymentCode.IsSourceFromValueList = False
        Me.txtPaymentCode.IsUnique = False
        Me.txtPaymentCode.Location = New System.Drawing.Point(111, 205)
        Me.txtPaymentCode.MaxLength = 200
        Me.txtPaymentCode.MendatroryField = False
        Me.txtPaymentCode.MyLinkLable1 = Me.MyLabel3
        Me.txtPaymentCode.MyLinkLable2 = Nothing
        Me.txtPaymentCode.Name = "txtPaymentCode"
        Me.txtPaymentCode.ReadOnly = True
        Me.txtPaymentCode.ReferenceFieldDesc = Nothing
        Me.txtPaymentCode.ReferenceFieldName = Nothing
        Me.txtPaymentCode.ReferenceTableName = Nothing
        Me.txtPaymentCode.Size = New System.Drawing.Size(321, 18)
        Me.txtPaymentCode.TabIndex = 106
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(6, 205)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(79, 18)
        Me.MyLabel1.TabIndex = 105
        Me.MyLabel1.Text = "Payment Code"
        '
        'txtPayment_Amount
        '
        Me.txtPayment_Amount.CalculationExpression = Nothing
        Me.txtPayment_Amount.FieldCode = Nothing
        Me.txtPayment_Amount.FieldDesc = Nothing
        Me.txtPayment_Amount.FieldMaxLength = 0
        Me.txtPayment_Amount.FieldName = Nothing
        Me.txtPayment_Amount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPayment_Amount.isCalculatedField = False
        Me.txtPayment_Amount.IsSourceFromTable = False
        Me.txtPayment_Amount.IsSourceFromValueList = False
        Me.txtPayment_Amount.IsUnique = False
        Me.txtPayment_Amount.Location = New System.Drawing.Point(111, 256)
        Me.txtPayment_Amount.MaxLength = 200
        Me.txtPayment_Amount.MendatroryField = False
        Me.txtPayment_Amount.MyLinkLable1 = Nothing
        Me.txtPayment_Amount.MyLinkLable2 = Nothing
        Me.txtPayment_Amount.Name = "txtPayment_Amount"
        Me.txtPayment_Amount.ReadOnly = True
        Me.txtPayment_Amount.ReferenceFieldDesc = Nothing
        Me.txtPayment_Amount.ReferenceFieldName = Nothing
        Me.txtPayment_Amount.ReferenceTableName = Nothing
        Me.txtPayment_Amount.Size = New System.Drawing.Size(82, 18)
        Me.txtPayment_Amount.TabIndex = 104
        Me.txtPayment_Amount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'MyLabel15
        '
        Me.MyLabel15.FieldName = Nothing
        Me.MyLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel15.Location = New System.Drawing.Point(6, 256)
        Me.MyLabel15.Name = "MyLabel15"
        Me.MyLabel15.Size = New System.Drawing.Size(93, 16)
        Me.MyLabel15.TabIndex = 103
        Me.MyLabel15.Text = "Payment Amount"
        '
        'dtpfarmerinvoice
        '
        Me.dtpfarmerinvoice.CalculationExpression = Nothing
        Me.dtpfarmerinvoice.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtpfarmerinvoice.FieldCode = Nothing
        Me.dtpfarmerinvoice.FieldDesc = Nothing
        Me.dtpfarmerinvoice.FieldMaxLength = 0
        Me.dtpfarmerinvoice.FieldName = Nothing
        Me.dtpfarmerinvoice.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpfarmerinvoice.isCalculatedField = False
        Me.dtpfarmerinvoice.IsSourceFromTable = False
        Me.dtpfarmerinvoice.IsSourceFromValueList = False
        Me.dtpfarmerinvoice.IsUnique = False
        Me.dtpfarmerinvoice.Location = New System.Drawing.Point(556, 41)
        Me.dtpfarmerinvoice.MendatroryField = False
        Me.dtpfarmerinvoice.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpfarmerinvoice.MyLinkLable1 = Nothing
        Me.dtpfarmerinvoice.MyLinkLable2 = Nothing
        Me.dtpfarmerinvoice.Name = "dtpfarmerinvoice"
        Me.dtpfarmerinvoice.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpfarmerinvoice.ReferenceFieldDesc = Nothing
        Me.dtpfarmerinvoice.ReferenceFieldName = Nothing
        Me.dtpfarmerinvoice.ReferenceTableName = Nothing
        Me.dtpfarmerinvoice.Size = New System.Drawing.Size(80, 20)
        Me.dtpfarmerinvoice.TabIndex = 102
        Me.dtpfarmerinvoice.TabStop = False
        Me.dtpfarmerinvoice.Text = "10/06/2011 11:51 AM"
        Me.dtpfarmerinvoice.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'txtFarmerCode
        '
        Me.txtFarmerCode.CalculationExpression = Nothing
        Me.txtFarmerCode.FieldCode = Nothing
        Me.txtFarmerCode.FieldDesc = Nothing
        Me.txtFarmerCode.FieldMaxLength = 0
        Me.txtFarmerCode.FieldName = Nothing
        Me.txtFarmerCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFarmerCode.isCalculatedField = False
        Me.txtFarmerCode.IsSourceFromTable = False
        Me.txtFarmerCode.IsSourceFromValueList = False
        Me.txtFarmerCode.IsUnique = False
        Me.txtFarmerCode.Location = New System.Drawing.Point(111, 133)
        Me.txtFarmerCode.MaxLength = 200
        Me.txtFarmerCode.MendatroryField = False
        Me.txtFarmerCode.MyLinkLable1 = Me.MyLabel3
        Me.txtFarmerCode.MyLinkLable2 = Nothing
        Me.txtFarmerCode.Name = "txtFarmerCode"
        Me.txtFarmerCode.ReadOnly = True
        Me.txtFarmerCode.ReferenceFieldDesc = Nothing
        Me.txtFarmerCode.ReferenceFieldName = Nothing
        Me.txtFarmerCode.ReferenceTableName = Nothing
        Me.txtFarmerCode.Size = New System.Drawing.Size(321, 18)
        Me.txtFarmerCode.TabIndex = 101
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel11.Location = New System.Drawing.Point(438, 133)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(74, 18)
        Me.MyLabel11.TabIndex = 99
        Me.MyLabel11.Text = "Farmer Name"
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel12.Location = New System.Drawing.Point(6, 133)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(70, 18)
        Me.MyLabel12.TabIndex = 98
        Me.MyLabel12.Text = "Farmer Code"
        '
        'lblFarmerName
        '
        Me.lblFarmerName.AutoSize = False
        Me.lblFarmerName.BackColor = System.Drawing.Color.White
        Me.lblFarmerName.BorderVisible = True
        Me.lblFarmerName.FieldName = Nothing
        Me.lblFarmerName.Location = New System.Drawing.Point(556, 133)
        Me.lblFarmerName.Name = "lblFarmerName"
        Me.lblFarmerName.Size = New System.Drawing.Size(381, 19)
        Me.lblFarmerName.TabIndex = 100
        '
        'txtfarmerinvoice
        '
        Me.txtfarmerinvoice.CalculationExpression = Nothing
        Me.txtfarmerinvoice.FieldCode = Nothing
        Me.txtfarmerinvoice.FieldDesc = Nothing
        Me.txtfarmerinvoice.FieldMaxLength = 0
        Me.txtfarmerinvoice.FieldName = Nothing
        Me.txtfarmerinvoice.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtfarmerinvoice.isCalculatedField = False
        Me.txtfarmerinvoice.IsSourceFromTable = False
        Me.txtfarmerinvoice.IsSourceFromValueList = False
        Me.txtfarmerinvoice.IsUnique = False
        Me.txtfarmerinvoice.Location = New System.Drawing.Point(110, 43)
        Me.txtfarmerinvoice.MaxLength = 200
        Me.txtfarmerinvoice.MendatroryField = False
        Me.txtfarmerinvoice.MyLinkLable1 = Me.MyLabel3
        Me.txtfarmerinvoice.MyLinkLable2 = Nothing
        Me.txtfarmerinvoice.Name = "txtfarmerinvoice"
        Me.txtfarmerinvoice.ReadOnly = True
        Me.txtfarmerinvoice.ReferenceFieldDesc = Nothing
        Me.txtfarmerinvoice.ReferenceFieldName = Nothing
        Me.txtfarmerinvoice.ReferenceTableName = Nothing
        Me.txtfarmerinvoice.Size = New System.Drawing.Size(321, 18)
        Me.txtfarmerinvoice.TabIndex = 97
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel2.Location = New System.Drawing.Point(438, 42)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(106, 18)
        Me.MyLabel2.TabIndex = 96
        Me.MyLabel2.Text = "Farmer Invoice Date"
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel4.Location = New System.Drawing.Point(6, 43)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(98, 18)
        Me.MyLabel4.TabIndex = 95
        Me.MyLabel4.Text = "Farmer Invoice No"
        '
        'lblVLCDesc
        '
        Me.lblVLCDesc.AutoSize = False
        Me.lblVLCDesc.BackColor = System.Drawing.Color.White
        Me.lblVLCDesc.BorderVisible = True
        Me.lblVLCDesc.FieldName = Nothing
        Me.lblVLCDesc.Location = New System.Drawing.Point(556, 109)
        Me.lblVLCDesc.Name = "lblVLCDesc"
        Me.lblVLCDesc.Size = New System.Drawing.Size(381, 19)
        Me.lblVLCDesc.TabIndex = 93
        '
        'txtVSPCode
        '
        Me.txtVSPCode.CalculationExpression = Nothing
        Me.txtVSPCode.FieldCode = Nothing
        Me.txtVSPCode.FieldDesc = Nothing
        Me.txtVSPCode.FieldMaxLength = 0
        Me.txtVSPCode.FieldName = Nothing
        Me.txtVSPCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVSPCode.isCalculatedField = False
        Me.txtVSPCode.IsSourceFromTable = False
        Me.txtVSPCode.IsSourceFromValueList = False
        Me.txtVSPCode.IsUnique = False
        Me.txtVSPCode.Location = New System.Drawing.Point(111, 87)
        Me.txtVSPCode.MaxLength = 200
        Me.txtVSPCode.MendatroryField = False
        Me.txtVSPCode.MyLinkLable1 = Me.MyLabel3
        Me.txtVSPCode.MyLinkLable2 = Nothing
        Me.txtVSPCode.Name = "txtVSPCode"
        Me.txtVSPCode.ReadOnly = True
        Me.txtVSPCode.ReferenceFieldDesc = Nothing
        Me.txtVSPCode.ReferenceFieldName = Nothing
        Me.txtVSPCode.ReferenceTableName = Nothing
        Me.txtVSPCode.Size = New System.Drawing.Size(321, 18)
        Me.txtVSPCode.TabIndex = 92
        '
        'txtMccCode
        '
        Me.txtMccCode.CalculationExpression = Nothing
        Me.txtMccCode.FieldCode = Nothing
        Me.txtMccCode.FieldDesc = Nothing
        Me.txtMccCode.FieldMaxLength = 0
        Me.txtMccCode.FieldName = Nothing
        Me.txtMccCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMccCode.isCalculatedField = False
        Me.txtMccCode.IsSourceFromTable = False
        Me.txtMccCode.IsSourceFromValueList = False
        Me.txtMccCode.IsUnique = False
        Me.txtMccCode.Location = New System.Drawing.Point(110, 65)
        Me.txtMccCode.MaxLength = 200
        Me.txtMccCode.MendatroryField = False
        Me.txtMccCode.MyLinkLable1 = Me.MyLabel3
        Me.txtMccCode.MyLinkLable2 = Nothing
        Me.txtMccCode.Name = "txtMccCode"
        Me.txtMccCode.ReadOnly = True
        Me.txtMccCode.ReferenceFieldDesc = Nothing
        Me.txtMccCode.ReferenceFieldName = Nothing
        Me.txtMccCode.ReferenceTableName = Nothing
        Me.txtMccCode.Size = New System.Drawing.Size(321, 18)
        Me.txtMccCode.TabIndex = 91
        '
        'lblMccName
        '
        Me.lblMccName.AutoSize = False
        Me.lblMccName.BackColor = System.Drawing.Color.White
        Me.lblMccName.BorderVisible = True
        Me.lblMccName.FieldName = Nothing
        Me.lblMccName.Location = New System.Drawing.Point(556, 64)
        Me.lblMccName.Name = "lblMccName"
        Me.lblMccName.Size = New System.Drawing.Size(381, 19)
        Me.lblMccName.TabIndex = 81
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel10.Location = New System.Drawing.Point(438, 64)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(63, 18)
        Me.MyLabel10.TabIndex = 90
        Me.MyLabel10.Text = "MCC Name"
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel7.Location = New System.Drawing.Point(438, 87)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(59, 18)
        Me.MyLabel7.TabIndex = 79
        Me.MyLabel7.Text = "Secretary Name"
        '
        'lblMCCCode
        '
        Me.lblMCCCode.FieldName = Nothing
        Me.lblMCCCode.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblMCCCode.Location = New System.Drawing.Point(6, 65)
        Me.lblMCCCode.Name = "lblMCCCode"
        Me.lblMCCCode.Size = New System.Drawing.Size(60, 18)
        Me.lblMCCCode.TabIndex = 89
        Me.lblMCCCode.Text = "MCC Code"
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(438, 111)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(62, 16)
        Me.RadLabel3.TabIndex = 85
        Me.RadLabel3.Text = "VLC Name"
        '
        'txtVLCCode
        '
        Me.txtVLCCode.CalculationExpression = Nothing
        Me.txtVLCCode.FieldCode = Nothing
        Me.txtVLCCode.FieldDesc = Nothing
        Me.txtVLCCode.FieldMaxLength = 0
        Me.txtVLCCode.FieldName = Nothing
        Me.txtVLCCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVLCCode.isCalculatedField = False
        Me.txtVLCCode.IsSourceFromTable = False
        Me.txtVLCCode.IsSourceFromValueList = False
        Me.txtVLCCode.IsUnique = False
        Me.txtVLCCode.Location = New System.Drawing.Point(111, 110)
        Me.txtVLCCode.MaxLength = 200
        Me.txtVLCCode.MendatroryField = False
        Me.txtVLCCode.MyLinkLable1 = Me.MyLabel3
        Me.txtVLCCode.MyLinkLable2 = Nothing
        Me.txtVLCCode.Name = "txtVLCCode"
        Me.txtVLCCode.ReadOnly = True
        Me.txtVLCCode.ReferenceFieldDesc = Nothing
        Me.txtVLCCode.ReferenceFieldName = Nothing
        Me.txtVLCCode.ReferenceTableName = Nothing
        Me.txtVLCCode.Size = New System.Drawing.Size(321, 18)
        Me.txtVLCCode.TabIndex = 86
        '
        'lblVSPCode
        '
        Me.lblVSPCode.FieldName = Nothing
        Me.lblVSPCode.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblVSPCode.Location = New System.Drawing.Point(6, 87)
        Me.lblVSPCode.Name = "lblVSPCode"
        Me.lblVSPCode.Size = New System.Drawing.Size(55, 18)
        Me.lblVSPCode.TabIndex = 78
        Me.lblVSPCode.Text = "Secretary Code"
        '
        'lblVSPDesc
        '
        Me.lblVSPDesc.AutoSize = False
        Me.lblVSPDesc.BackColor = System.Drawing.Color.White
        Me.lblVSPDesc.BorderVisible = True
        Me.lblVSPDesc.FieldName = Nothing
        Me.lblVSPDesc.Location = New System.Drawing.Point(556, 87)
        Me.lblVSPDesc.Name = "lblVSPDesc"
        Me.lblVSPDesc.Size = New System.Drawing.Size(381, 19)
        Me.lblVSPDesc.TabIndex = 80
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
        Me.dtpPayment.Location = New System.Drawing.Point(543, 19)
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
        Me.dtpPayment.TabIndex = 4
        Me.dtpPayment.TabStop = False
        Me.dtpPayment.Text = "10/06/2011 11:51 AM"
        Me.dtpPayment.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'lblpaymentno
        '
        Me.lblpaymentno.FieldName = Nothing
        Me.lblpaymentno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpaymentno.Location = New System.Drawing.Point(6, 21)
        Me.lblpaymentno.Name = "lblpaymentno"
        Me.lblpaymentno.Size = New System.Drawing.Size(69, 16)
        Me.lblpaymentno.TabIndex = 0
        Me.lblpaymentno.Text = "Payment No"
        '
        'lblpaymentdate
        '
        Me.lblpaymentdate.FieldName = Nothing
        Me.lblpaymentdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpaymentdate.Location = New System.Drawing.Point(459, 21)
        Me.lblpaymentdate.Name = "lblpaymentdate"
        Me.lblpaymentdate.Size = New System.Drawing.Size(78, 16)
        Me.lblpaymentdate.TabIndex = 3
        Me.lblpaymentdate.Text = "Payment Date"
        '
        'txtPaymentNo
        '
        Me.txtPaymentNo.FieldName = Nothing
        Me.txtPaymentNo.Location = New System.Drawing.Point(94, 19)
        Me.txtPaymentNo.MendatroryField = False
        Me.txtPaymentNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtPaymentNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtPaymentNo.MyLinkLable1 = Nothing
        Me.txtPaymentNo.MyLinkLable2 = Nothing
        Me.txtPaymentNo.MyMaxLength = 30
        Me.txtPaymentNo.MyReadOnly = False
        Me.txtPaymentNo.Name = "txtPaymentNo"
        Me.txtPaymentNo.Size = New System.Drawing.Size(343, 20)
        Me.txtPaymentNo.TabIndex = 1
        Me.txtPaymentNo.Value = ""
        '
        'RadMenu1
        '
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1136, 20)
        Me.RadMenu1.TabIndex = 1
        '
        'btnreset
        '
        Me.btnreset.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnreset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnreset.Location = New System.Drawing.Point(0, 0)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(68, 25)
        Me.btnreset.TabIndex = 9
        Me.btnreset.Text = "Reset"
        '
        'btnprint
        '
        Me.btnprint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnprint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnprint.Location = New System.Drawing.Point(289, 4)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(68, 18)
        Me.btnprint.TabIndex = 4
        Me.btnprint.Text = "Print"
        '
        'btnclose
        '
        Me.btnclose.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(1056, 3)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(68, 18)
        Me.btnclose.TabIndex = 8
        Me.btnclose.Text = "Close"
        '
        'frmFarmerPaymentEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1136, 538)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmFarmerPaymentEntry"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Farmer Payment Entry"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dtp_chequedate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtchequeno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBankCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPaymentCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPayment_Amount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpfarmerinvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFarmerCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFarmerName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtfarmerinvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVLCDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVSPCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMccCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMccName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMCCCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVLCCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVSPCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVSPDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpPayment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblpaymentno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblpaymentdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtPaymentNo As common.UserControls.txtNavigator
    Friend WithEvents lblpaymentno As common.Controls.MyLabel
    Friend WithEvents lblpaymentdate As common.Controls.MyLabel
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents dtpPayment As common.Controls.MyDateTimePicker
    Friend WithEvents btnprint As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblVLCDesc As common.Controls.MyLabel
    Friend WithEvents txtVSPCode As common.Controls.MyTextBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtMccCode As common.Controls.MyTextBox
    Friend WithEvents lblMccName As common.Controls.MyLabel
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents lblMCCCode As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents txtVLCCode As common.Controls.MyTextBox
    Friend WithEvents lblVSPCode As common.Controls.MyLabel
    Friend WithEvents lblVSPDesc As common.Controls.MyLabel
    Friend WithEvents txtfarmerinvoice As common.Controls.MyTextBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents dtpfarmerinvoice As common.Controls.MyDateTimePicker
    Friend WithEvents txtFarmerCode As common.Controls.MyTextBox
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents lblFarmerName As common.Controls.MyLabel
    Friend WithEvents txtBankCode As common.Controls.MyTextBox
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents txtdescription As common.Controls.MyTextBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtPaymentCode As common.Controls.MyTextBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtPayment_Amount As common.Controls.MyTextBox
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
    Friend WithEvents dtp_chequedate As common.Controls.MyDateTimePicker
    Friend WithEvents txtchequeno As common.Controls.MyTextBox
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
End Class

