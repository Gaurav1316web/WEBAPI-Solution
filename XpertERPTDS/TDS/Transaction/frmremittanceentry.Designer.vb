Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frmremittanceentry
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
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Me.lblremittanceno = New common.Controls.MyLabel
        Me.lbldesc = New common.Controls.MyLabel
        Me.lblsection = New common.Controls.MyLabel
        Me.lblbranch = New common.Controls.MyLabel
        Me.lblfiscalquarter = New common.Controls.MyLabel
        Me.lblfiscalyear = New common.Controls.MyLabel
        Me.lblbankcode = New common.Controls.MyLabel
        Me.UsLock1 = New common.usLock
        Me.fndpaymentcode = New common.UserControls.txtFinder
        Me.RadLabel8 = New common.Controls.MyLabel
        Me.fndbankcode = New common.UserControls.txtFinder
        Me.fndremittance = New common.UserControls.txtFinder
        Me.btnreset = New Telerik.WinControls.UI.RadButton
        Me.txtbranchdesc = New common.Controls.MyTextBox
        Me.txtfiscalyear = New common.Controls.MyTextBox
        Me.txtbsrname = New common.Controls.MyTextBox
        Me.RadLabel13 = New common.Controls.MyLabel
        Me.txtbsrcode = New common.Controls.MyTextBox
        Me.RadLabel14 = New common.Controls.MyLabel
        Me.dtpchallan = New common.Controls.MyDateTimePicker
        Me.RadLabel11 = New common.Controls.MyLabel
        Me.txtchallanno = New common.Controls.MyTextBox
        Me.RadLabel12 = New common.Controls.MyLabel
        Me.dtpcheque = New common.Controls.MyDateTimePicker
        Me.lblchequedate = New common.Controls.MyLabel
        Me.txtchequeno = New common.Controls.MyTextBox
        Me.lblchequeno = New common.Controls.MyLabel
        Me.dtppayment = New common.Controls.MyDateTimePicker
        Me.RadLabel6 = New common.Controls.MyLabel
        Me.dtpposting = New common.Controls.MyDateTimePicker
        Me.RadLabel7 = New common.Controls.MyLabel
        Me.txtremitto = New common.Controls.MyTextBox
        Me.RadLabel5 = New common.Controls.MyLabel
        Me.txtamttoremit = New common.Controls.MyTextBox
        Me.RadLabel4 = New common.Controls.MyLabel
        Me.txttaxamt = New common.Controls.MyTextBox
        Me.RadLabel3 = New common.Controls.MyLabel
        Me.ddlfiscalquarter = New common.Controls.MyComboBox
        Me.dtpremittancedate = New common.Controls.MyDateTimePicker
        Me.lblremittancedate = New common.Controls.MyLabel
        Me.txtbankdesc = New common.Controls.MyTextBox
        Me.txtbranchcode = New common.Controls.MyTextBox
        Me.txtsectiondesc = New common.Controls.MyTextBox
        Me.txtsectioncode = New common.Controls.MyTextBox
        Me.txtdesc = New common.Controls.MyTextBox
        Me.btnvoidremittance = New Telerik.WinControls.UI.RadButton
        Me.btnpost = New Telerik.WinControls.UI.RadButton
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.btndelete = New Telerik.WinControls.UI.RadButton
        Me.btnsave = New Telerik.WinControls.UI.RadButton
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnUpdateChallanAndBSR = New Telerik.WinControls.UI.RadButton
        Me.btnTaxChallan = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.gv1 = New common.UserControls.MyRadGridView
        CType(Me.lblremittanceno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbldesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblsection, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblbranch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblfiscalquarter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblfiscalyear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblbankcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtbranchdesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtfiscalyear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtbsrname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtbsrcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpchallan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtchallanno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpcheque, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblchequedate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtchequeno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblchequeno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtppayment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpposting, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtremitto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtamttoremit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txttaxamt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlfiscalquarter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpremittancedate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblremittancedate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtbankdesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtbranchcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtsectiondesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtsectioncode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnvoidremittance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnpost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.btnUpdateChallanAndBSR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnTaxChallan, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblremittanceno
        '
        Me.lblremittanceno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblremittanceno.Location = New System.Drawing.Point(3, 3)
        Me.lblremittanceno.Name = "lblremittanceno"
        Me.lblremittanceno.Size = New System.Drawing.Size(81, 16)
        Me.lblremittanceno.TabIndex = 0
        Me.lblremittanceno.Text = "Remittance No"
        '
        'lbldesc
        '
        Me.lbldesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldesc.Location = New System.Drawing.Point(330, 3)
        Me.lbldesc.Name = "lbldesc"
        Me.lbldesc.Size = New System.Drawing.Size(63, 16)
        Me.lbldesc.TabIndex = 1
        Me.lbldesc.Text = "Description"
        '
        'lblsection
        '
        Me.lblsection.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblsection.Location = New System.Drawing.Point(3, 25)
        Me.lblsection.Name = "lblsection"
        Me.lblsection.Size = New System.Drawing.Size(44, 16)
        Me.lblsection.TabIndex = 2
        Me.lblsection.Text = "Section"
        '
        'lblbranch
        '
        Me.lblbranch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblbranch.Location = New System.Drawing.Point(411, 25)
        Me.lblbranch.Name = "lblbranch"
        Me.lblbranch.Size = New System.Drawing.Size(42, 16)
        Me.lblbranch.TabIndex = 5
        Me.lblbranch.Text = "Branch"
        '
        'lblfiscalquarter
        '
        Me.lblfiscalquarter.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblfiscalquarter.Location = New System.Drawing.Point(309, 70)
        Me.lblfiscalquarter.Name = "lblfiscalquarter"
        Me.lblfiscalquarter.Size = New System.Drawing.Size(77, 16)
        Me.lblfiscalquarter.TabIndex = 6
        Me.lblfiscalquarter.Text = "Fiscal Quarter"
        '
        'lblfiscalyear
        '
        Me.lblfiscalyear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblfiscalyear.Location = New System.Drawing.Point(3, 70)
        Me.lblfiscalyear.Name = "lblfiscalyear"
        Me.lblfiscalyear.Size = New System.Drawing.Size(63, 16)
        Me.lblfiscalyear.TabIndex = 8
        Me.lblfiscalyear.Text = "Fiscal Year"
        '
        'lblbankcode
        '
        Me.lblbankcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblbankcode.Location = New System.Drawing.Point(3, 48)
        Me.lblbankcode.Name = "lblbankcode"
        Me.lblbankcode.Size = New System.Drawing.Size(62, 16)
        Me.lblbankcode.TabIndex = 10
        Me.lblbankcode.Text = "Bank Code"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(773, 23)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(97, 21)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 8
        '
        'fndpaymentcode
        '
        Me.fndpaymentcode.Location = New System.Drawing.Point(91, 159)
        Me.fndpaymentcode.MendatroryField = True
        Me.fndpaymentcode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndpaymentcode.MyLinkLable1 = Me.RadLabel8
        Me.fndpaymentcode.MyLinkLable2 = Nothing
        Me.fndpaymentcode.MyReadOnly = False
        Me.fndpaymentcode.Name = "fndpaymentcode"
        Me.fndpaymentcode.Size = New System.Drawing.Size(187, 18)
        Me.fndpaymentcode.TabIndex = 15
        Me.fndpaymentcode.Value = ""
        '
        'RadLabel8
        '
        Me.RadLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel8.Location = New System.Drawing.Point(3, 160)
        Me.RadLabel8.Name = "RadLabel8"
        Me.RadLabel8.Size = New System.Drawing.Size(81, 16)
        Me.RadLabel8.TabIndex = 22
        Me.RadLabel8.Text = "Payment Code"
        '
        'fndbankcode
        '
        Me.fndbankcode.Location = New System.Drawing.Point(91, 47)
        Me.fndbankcode.MendatroryField = True
        Me.fndbankcode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndbankcode.MyLinkLable1 = Me.lblbankcode
        Me.fndbankcode.MyLinkLable2 = Nothing
        Me.fndbankcode.MyReadOnly = False
        Me.fndbankcode.Name = "fndbankcode"
        Me.fndbankcode.Size = New System.Drawing.Size(187, 18)
        Me.fndbankcode.TabIndex = 6
        Me.fndbankcode.Value = ""
        '
        'fndremittance
        '
        Me.fndremittance.Location = New System.Drawing.Point(91, 2)
        Me.fndremittance.MendatroryField = False
        Me.fndremittance.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndremittance.MyLinkLable1 = Me.lblremittanceno
        Me.fndremittance.MyLinkLable2 = Nothing
        Me.fndremittance.MyReadOnly = False
        Me.fndremittance.Name = "fndremittance"
        Me.fndremittance.Size = New System.Drawing.Size(187, 18)
        Me.fndremittance.TabIndex = 0
        Me.fndremittance.Value = ""
        '
        'btnreset
        '
        Me.btnreset.Image = My.Resources.Resources._new
        Me.btnreset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnreset.Location = New System.Drawing.Point(280, 1)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(19, 21)
        Me.btnreset.TabIndex = 1
        '
        'txtbranchdesc
        '
        Me.txtbranchdesc.Location = New System.Drawing.Point(539, 23)
        Me.txtbranchdesc.MendatroryField = False
        Me.txtbranchdesc.MyLinkLable1 = Nothing
        Me.txtbranchdesc.MyLinkLable2 = Nothing
        Me.txtbranchdesc.Name = "txtbranchdesc"
        Me.txtbranchdesc.ReadOnly = True
        Me.txtbranchdesc.Size = New System.Drawing.Size(230, 20)
        Me.txtbranchdesc.TabIndex = 5
        Me.txtbranchdesc.TabStop = False
        '
        'txtfiscalyear
        '
        Me.txtfiscalyear.Location = New System.Drawing.Point(91, 68)
        Me.txtfiscalyear.MendatroryField = False
        Me.txtfiscalyear.MyLinkLable1 = Me.lblfiscalyear
        Me.txtfiscalyear.MyLinkLable2 = Nothing
        Me.txtfiscalyear.Name = "txtfiscalyear"
        Me.txtfiscalyear.ReadOnly = True
        Me.txtfiscalyear.Size = New System.Drawing.Size(160, 20)
        Me.txtfiscalyear.TabIndex = 8
        Me.txtfiscalyear.TabStop = False
        '
        'txtbsrname
        '
        Me.txtbsrname.Location = New System.Drawing.Point(402, 203)
        Me.txtbsrname.MaxLength = 50
        Me.txtbsrname.MendatroryField = False
        Me.txtbsrname.MyLinkLable1 = Me.RadLabel13
        Me.txtbsrname.MyLinkLable2 = Nothing
        Me.txtbsrname.Name = "txtbsrname"
        Me.txtbsrname.Size = New System.Drawing.Size(468, 20)
        Me.txtbsrname.TabIndex = 20
        '
        'RadLabel13
        '
        Me.RadLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel13.Location = New System.Drawing.Point(309, 205)
        Me.RadLabel13.Name = "RadLabel13"
        Me.RadLabel13.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel13.TabIndex = 35
        Me.RadLabel13.Text = "BSR Name"
        '
        'txtbsrcode
        '
        Me.txtbsrcode.Location = New System.Drawing.Point(91, 203)
        Me.txtbsrcode.MaxLength = 12
        Me.txtbsrcode.MendatroryField = False
        Me.txtbsrcode.MyLinkLable1 = Me.RadLabel14
        Me.txtbsrcode.MyLinkLable2 = Nothing
        Me.txtbsrcode.Name = "txtbsrcode"
        Me.txtbsrcode.Size = New System.Drawing.Size(160, 20)
        Me.txtbsrcode.TabIndex = 19
        '
        'RadLabel14
        '
        Me.RadLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel14.Location = New System.Drawing.Point(3, 205)
        Me.RadLabel14.Name = "RadLabel14"
        Me.RadLabel14.Size = New System.Drawing.Size(60, 16)
        Me.RadLabel14.TabIndex = 33
        Me.RadLabel14.Text = "BSR Code"
        '
        'dtpchallan
        '
        Me.dtpchallan.CustomFormat = "dd/MM/yyyy"
        Me.dtpchallan.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpchallan.Location = New System.Drawing.Point(402, 180)
        Me.dtpchallan.MendatroryField = False
        Me.dtpchallan.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpchallan.MyLinkLable1 = Me.RadLabel11
        Me.dtpchallan.MyLinkLable2 = Nothing
        Me.dtpchallan.Name = "dtpchallan"
        Me.dtpchallan.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpchallan.Size = New System.Drawing.Size(160, 20)
        Me.dtpchallan.TabIndex = 18
        Me.dtpchallan.TabStop = False
        Me.dtpchallan.Text = "02/08/2011"
        Me.dtpchallan.Value = New Date(2011, 8, 2, 12, 54, 58, 312)
        '
        'RadLabel11
        '
        Me.RadLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel11.Location = New System.Drawing.Point(309, 182)
        Me.RadLabel11.Name = "RadLabel11"
        Me.RadLabel11.Size = New System.Drawing.Size(72, 16)
        Me.RadLabel11.TabIndex = 30
        Me.RadLabel11.Text = "Challan Date"
        '
        'txtchallanno
        '
        Me.txtchallanno.Location = New System.Drawing.Point(91, 180)
        Me.txtchallanno.MaxLength = 12
        Me.txtchallanno.MendatroryField = False
        Me.txtchallanno.MyLinkLable1 = Me.RadLabel12
        Me.txtchallanno.MyLinkLable2 = Nothing
        Me.txtchallanno.Name = "txtchallanno"
        Me.txtchallanno.Size = New System.Drawing.Size(160, 20)
        Me.txtchallanno.TabIndex = 17
        '
        'RadLabel12
        '
        Me.RadLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel12.Location = New System.Drawing.Point(3, 182)
        Me.RadLabel12.Name = "RadLabel12"
        Me.RadLabel12.Size = New System.Drawing.Size(62, 16)
        Me.RadLabel12.TabIndex = 31
        Me.RadLabel12.Text = "Challan No"
        '
        'dtpcheque
        '
        Me.dtpcheque.CustomFormat = "dd/MM/yyyy"
        Me.dtpcheque.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpcheque.Location = New System.Drawing.Point(732, 158)
        Me.dtpcheque.MendatroryField = False
        Me.dtpcheque.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpcheque.MyLinkLable1 = Me.lblchequedate
        Me.dtpcheque.MyLinkLable2 = Nothing
        Me.dtpcheque.Name = "dtpcheque"
        Me.dtpcheque.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpcheque.Size = New System.Drawing.Size(138, 20)
        Me.dtpcheque.TabIndex = 19
        Me.dtpcheque.TabStop = False
        Me.dtpcheque.Text = "02/08/2011"
        Me.dtpcheque.Value = New Date(2011, 8, 2, 12, 54, 58, 312)
        '
        'lblchequedate
        '
        Me.lblchequedate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblchequedate.Location = New System.Drawing.Point(643, 160)
        Me.lblchequedate.Name = "lblchequedate"
        Me.lblchequedate.Size = New System.Drawing.Size(76, 16)
        Me.lblchequedate.TabIndex = 26
        Me.lblchequedate.Text = "Cheque  Date"
        '
        'txtchequeno
        '
        Me.txtchequeno.Location = New System.Drawing.Point(402, 158)
        Me.txtchequeno.MaxLength = 6
        Me.txtchequeno.MendatroryField = False
        Me.txtchequeno.MyLinkLable1 = Me.lblchequeno
        Me.txtchequeno.MyLinkLable2 = Nothing
        Me.txtchequeno.Name = "txtchequeno"
        Me.txtchequeno.Size = New System.Drawing.Size(160, 20)
        Me.txtchequeno.TabIndex = 16
        '
        'lblchequeno
        '
        Me.lblchequeno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblchequeno.Location = New System.Drawing.Point(309, 160)
        Me.lblchequeno.Name = "lblchequeno"
        Me.lblchequeno.Size = New System.Drawing.Size(67, 16)
        Me.lblchequeno.TabIndex = 25
        Me.lblchequeno.Text = "Cheque No."
        '
        'dtppayment
        '
        Me.dtppayment.CustomFormat = "dd/MM/yyyy"
        Me.dtppayment.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtppayment.Location = New System.Drawing.Point(402, 137)
        Me.dtppayment.MendatroryField = False
        Me.dtppayment.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtppayment.MyLinkLable1 = Me.RadLabel6
        Me.dtppayment.MyLinkLable2 = Nothing
        Me.dtppayment.Name = "dtppayment"
        Me.dtppayment.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtppayment.Size = New System.Drawing.Size(160, 20)
        Me.dtppayment.TabIndex = 14
        Me.dtppayment.TabStop = False
        Me.dtppayment.Text = "02/08/2011"
        Me.dtppayment.Value = New Date(2011, 8, 2, 12, 54, 58, 312)
        '
        'RadLabel6
        '
        Me.RadLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel6.Location = New System.Drawing.Point(304, 139)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(96, 16)
        Me.RadLabel6.TabIndex = 18
        Me.RadLabel6.Text = "AP Payment Date"
        '
        'dtpposting
        '
        Me.dtpposting.CustomFormat = "dd/MM/yyyy"
        Me.dtpposting.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpposting.Location = New System.Drawing.Point(94, 137)
        Me.dtpposting.MendatroryField = False
        Me.dtpposting.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpposting.MyLinkLable1 = Me.RadLabel7
        Me.dtpposting.MyLinkLable2 = Nothing
        Me.dtpposting.Name = "dtpposting"
        Me.dtpposting.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpposting.Size = New System.Drawing.Size(160, 20)
        Me.dtpposting.TabIndex = 13
        Me.dtpposting.TabStop = False
        Me.dtpposting.Text = "02/08/2011"
        Me.dtpposting.Value = New Date(2011, 8, 2, 12, 54, 58, 312)
        '
        'RadLabel7
        '
        Me.RadLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel7.Location = New System.Drawing.Point(3, 139)
        Me.RadLabel7.Name = "RadLabel7"
        Me.RadLabel7.Size = New System.Drawing.Size(89, 16)
        Me.RadLabel7.TabIndex = 19
        Me.RadLabel7.Text = "AP Posting Date"
        '
        'txtremitto
        '
        Me.txtremitto.Location = New System.Drawing.Point(91, 114)
        Me.txtremitto.MaxLength = 100
        Me.txtremitto.MendatroryField = False
        Me.txtremitto.MyLinkLable1 = Me.RadLabel5
        Me.txtremitto.MyLinkLable2 = Nothing
        Me.txtremitto.Name = "txtremitto"
        Me.txtremitto.Size = New System.Drawing.Size(779, 20)
        Me.txtremitto.TabIndex = 12
        '
        'RadLabel5
        '
        Me.RadLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel5.Location = New System.Drawing.Point(3, 116)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(52, 16)
        Me.RadLabel5.TabIndex = 16
        Me.RadLabel5.Text = "Remit To"
        '
        'txtamttoremit
        '
        Me.txtamttoremit.Location = New System.Drawing.Point(402, 91)
        Me.txtamttoremit.MendatroryField = False
        Me.txtamttoremit.MyLinkLable1 = Me.RadLabel4
        Me.txtamttoremit.MyLinkLable2 = Nothing
        Me.txtamttoremit.Name = "txtamttoremit"
        Me.txtamttoremit.ReadOnly = True
        Me.txtamttoremit.Size = New System.Drawing.Size(160, 20)
        Me.txtamttoremit.TabIndex = 11
        Me.txtamttoremit.TabStop = False
        '
        'RadLabel4
        '
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(309, 93)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(87, 16)
        Me.RadLabel4.TabIndex = 13
        Me.RadLabel4.Text = "Amt to be Remit"
        '
        'txttaxamt
        '
        Me.txttaxamt.Location = New System.Drawing.Point(91, 91)
        Me.txttaxamt.MendatroryField = False
        Me.txttaxamt.MyLinkLable1 = Me.RadLabel3
        Me.txttaxamt.MyLinkLable2 = Nothing
        Me.txttaxamt.Name = "txttaxamt"
        Me.txttaxamt.ReadOnly = True
        Me.txttaxamt.Size = New System.Drawing.Size(160, 20)
        Me.txttaxamt.TabIndex = 10
        Me.txttaxamt.TabStop = False
        '
        'RadLabel3
        '
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(3, 93)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(67, 16)
        Me.RadLabel3.TabIndex = 14
        Me.RadLabel3.Text = "Tax Amount"
        '
        'ddlfiscalquarter
        '
        Me.ddlfiscalquarter.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlfiscalquarter.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        RadListDataItem1.Text = "First Quarter"
        RadListDataItem1.TextWrap = True
        RadListDataItem2.Text = "Second Quarter"
        RadListDataItem2.TextWrap = True
        RadListDataItem3.Text = "Third Quarter"
        RadListDataItem3.TextWrap = True
        RadListDataItem4.Text = "Fourth Quarter"
        RadListDataItem4.TextWrap = True
        Me.ddlfiscalquarter.Items.Add(RadListDataItem1)
        Me.ddlfiscalquarter.Items.Add(RadListDataItem2)
        Me.ddlfiscalquarter.Items.Add(RadListDataItem3)
        Me.ddlfiscalquarter.Items.Add(RadListDataItem4)
        Me.ddlfiscalquarter.Location = New System.Drawing.Point(400, 69)
        Me.ddlfiscalquarter.MendatroryField = False
        Me.ddlfiscalquarter.MyLinkLable1 = Me.lblfiscalquarter
        Me.ddlfiscalquarter.MyLinkLable2 = Nothing
        Me.ddlfiscalquarter.Name = "ddlfiscalquarter"
        Me.ddlfiscalquarter.Size = New System.Drawing.Size(162, 18)
        Me.ddlfiscalquarter.TabIndex = 9
        Me.ddlfiscalquarter.Text = "Select"
        '
        'dtpremittancedate
        '
        Me.dtpremittancedate.CustomFormat = "dd/MM/yyyy"
        Me.dtpremittancedate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpremittancedate.Location = New System.Drawing.Point(732, 46)
        Me.dtpremittancedate.MendatroryField = False
        Me.dtpremittancedate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpremittancedate.MyLinkLable1 = Me.lblremittancedate
        Me.dtpremittancedate.MyLinkLable2 = Nothing
        Me.dtpremittancedate.Name = "dtpremittancedate"
        Me.dtpremittancedate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpremittancedate.Size = New System.Drawing.Size(138, 20)
        Me.dtpremittancedate.TabIndex = 9
        Me.dtpremittancedate.TabStop = False
        Me.dtpremittancedate.Text = "02/08/2011"
        Me.dtpremittancedate.Value = New Date(2011, 8, 2, 12, 54, 58, 312)
        '
        'lblremittancedate
        '
        Me.lblremittancedate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblremittancedate.Location = New System.Drawing.Point(639, 48)
        Me.lblremittancedate.Name = "lblremittancedate"
        Me.lblremittancedate.Size = New System.Drawing.Size(91, 16)
        Me.lblremittancedate.TabIndex = 5
        Me.lblremittancedate.Text = "Remittance Date"
        '
        'txtbankdesc
        '
        Me.txtbankdesc.Location = New System.Drawing.Point(286, 46)
        Me.txtbankdesc.MendatroryField = False
        Me.txtbankdesc.MyLinkLable1 = Nothing
        Me.txtbankdesc.MyLinkLable2 = Nothing
        Me.txtbankdesc.Name = "txtbankdesc"
        Me.txtbankdesc.ReadOnly = True
        Me.txtbankdesc.Size = New System.Drawing.Size(339, 20)
        Me.txtbankdesc.TabIndex = 7
        Me.txtbankdesc.TabStop = False
        '
        'txtbranchcode
        '
        Me.txtbranchcode.Location = New System.Drawing.Point(463, 23)
        Me.txtbranchcode.MendatroryField = False
        Me.txtbranchcode.MyLinkLable1 = Me.lblbranch
        Me.txtbranchcode.MyLinkLable2 = Nothing
        Me.txtbranchcode.Name = "txtbranchcode"
        Me.txtbranchcode.ReadOnly = True
        Me.txtbranchcode.Size = New System.Drawing.Size(70, 20)
        Me.txtbranchcode.TabIndex = 4
        Me.txtbranchcode.TabStop = False
        '
        'txtsectiondesc
        '
        Me.txtsectiondesc.Location = New System.Drawing.Point(159, 23)
        Me.txtsectiondesc.MendatroryField = False
        Me.txtsectiondesc.MyLinkLable1 = Nothing
        Me.txtsectiondesc.MyLinkLable2 = Nothing
        Me.txtsectiondesc.Name = "txtsectiondesc"
        Me.txtsectiondesc.ReadOnly = True
        Me.txtsectiondesc.Size = New System.Drawing.Size(246, 20)
        Me.txtsectiondesc.TabIndex = 3
        Me.txtsectiondesc.TabStop = False
        '
        'txtsectioncode
        '
        Me.txtsectioncode.Location = New System.Drawing.Point(91, 23)
        Me.txtsectioncode.MendatroryField = False
        Me.txtsectioncode.MyLinkLable1 = Me.lblsection
        Me.txtsectioncode.MyLinkLable2 = Nothing
        Me.txtsectioncode.Name = "txtsectioncode"
        Me.txtsectioncode.ReadOnly = True
        Me.txtsectioncode.Size = New System.Drawing.Size(62, 20)
        Me.txtsectioncode.TabIndex = 2
        Me.txtsectioncode.TabStop = False
        '
        'txtdesc
        '
        Me.txtdesc.Location = New System.Drawing.Point(400, 1)
        Me.txtdesc.MaxLength = 49
        Me.txtdesc.MendatroryField = False
        Me.txtdesc.MyLinkLable1 = Me.lbldesc
        Me.txtdesc.MyLinkLable2 = Nothing
        Me.txtdesc.Name = "txtdesc"
        Me.txtdesc.Size = New System.Drawing.Size(470, 20)
        Me.txtdesc.TabIndex = 1
        '
        'btnvoidremittance
        '
        Me.btnvoidremittance.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnvoidremittance.Location = New System.Drawing.Point(286, 5)
        Me.btnvoidremittance.Name = "btnvoidremittance"
        Me.btnvoidremittance.Size = New System.Drawing.Size(90, 22)
        Me.btnvoidremittance.TabIndex = 3
        Me.btnvoidremittance.Text = "Void Remittance"
        '
        'btnpost
        '
        Me.btnpost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnpost.Location = New System.Drawing.Point(192, 6)
        Me.btnpost.Name = "btnpost"
        Me.btnpost.Size = New System.Drawing.Size(90, 21)
        Me.btnpost.TabIndex = 2
        Me.btnpost.Text = "Post"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(786, 6)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(90, 21)
        Me.btnclose.TabIndex = 6
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(98, 6)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(90, 21)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(5, 6)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(90, 21)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnUpdateChallanAndBSR)
        Me.Panel1.Controls.Add(Me.btnTaxChallan)
        Me.Panel1.Controls.Add(Me.btnsave)
        Me.Panel1.Controls.Add(Me.btndelete)
        Me.Panel1.Controls.Add(Me.btnvoidremittance)
        Me.Panel1.Controls.Add(Me.btnclose)
        Me.Panel1.Controls.Add(Me.btnpost)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 449)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(881, 31)
        Me.Panel1.TabIndex = 1
        '
        'btnUpdateChallanAndBSR
        '
        Me.btnUpdateChallanAndBSR.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnUpdateChallanAndBSR.Location = New System.Drawing.Point(483, 5)
        Me.btnUpdateChallanAndBSR.Name = "btnUpdateChallanAndBSR"
        Me.btnUpdateChallanAndBSR.Size = New System.Drawing.Size(131, 21)
        Me.btnUpdateChallanAndBSR.TabIndex = 5
        Me.btnUpdateChallanAndBSR.Text = "Update BSR && Challan"
        '
        'btnTaxChallan
        '
        Me.btnTaxChallan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnTaxChallan.Location = New System.Drawing.Point(381, 5)
        Me.btnTaxChallan.Name = "btnTaxChallan"
        Me.btnTaxChallan.Size = New System.Drawing.Size(99, 21)
        Me.btnTaxChallan.TabIndex = 4
        Me.btnTaxChallan.Text = "TDS Tax Challan "
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndpaymentcode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndbankcode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblfiscalyear)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblfiscalquarter)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblremittanceno)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ddlfiscalquarter)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndremittance)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblbranch)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txttaxamt)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnreset)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtfiscalyear)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblsection)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtbranchdesc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtbsrname)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lbldesc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpremittancedate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel13)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtdesc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtamttoremit)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtbankdesc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtbsrcode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtsectioncode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtremitto)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblbankcode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel14)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtsectiondesc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel7)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblremittancedate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpchallan)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtbranchcode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtchequeno)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel11)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblchequedate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpposting)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblchequeno)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtchallanno)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpcheque)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtppayment)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel8)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel12)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.gv1)
        Me.SplitContainer1.Size = New System.Drawing.Size(881, 449)
        Me.SplitContainer1.SplitterDistance = 227
        Me.SplitContainer1.TabIndex = 0
        '
        'gv1
        '
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        Me.gv1.Name = "gv1"
        Me.gv1.Size = New System.Drawing.Size(881, 218)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        Me.gv1.Text = "RadGridView1"
        '
        'Frmremittanceentry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(881, 480)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frmremittanceentry"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Remittance Entry"
        CType(Me.lblremittanceno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbldesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblsection, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblbranch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblfiscalquarter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblfiscalyear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblbankcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtbranchdesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtfiscalyear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtbsrname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtbsrcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpchallan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtchallanno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpcheque, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblchequedate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtchequeno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblchequeno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtppayment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpposting, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtremitto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtamttoremit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txttaxamt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlfiscalquarter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpremittancedate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblremittancedate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtbankdesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtbranchcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtsectiondesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtsectioncode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnvoidremittance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnpost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.btnUpdateChallanAndBSR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnTaxChallan, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtdesc As common.Controls.MyTextBox
    Friend WithEvents txtbranchcode As common.Controls.MyTextBox
    Friend WithEvents txtsectiondesc As common.Controls.MyTextBox
    Friend WithEvents txtsectioncode As common.Controls.MyTextBox
    Friend WithEvents txtbankdesc As common.Controls.MyTextBox
    Friend WithEvents ddlfiscalquarter As common.Controls.MyComboBox
    Friend WithEvents txtamttoremit As common.Controls.MyTextBox
    Friend WithEvents txttaxamt As common.Controls.MyTextBox
    Friend WithEvents txtremitto As common.Controls.MyTextBox
    Friend WithEvents dtpposting As common.Controls.MyDateTimePicker
    Friend WithEvents txtchallanno As common.Controls.MyTextBox
    Friend WithEvents dtpcheque As common.Controls.MyDateTimePicker
    Friend WithEvents txtchequeno As common.Controls.MyTextBox
    Friend WithEvents dtppayment As common.Controls.MyDateTimePicker
    Friend WithEvents txtbsrname As common.Controls.MyTextBox
    Friend WithEvents txtbsrcode As common.Controls.MyTextBox
    Friend WithEvents dtpchallan As common.Controls.MyDateTimePicker
    Friend WithEvents MasterTemplate As common.UserControls.MyRadGridView
    Friend WithEvents txtfiscalyear As common.Controls.MyTextBox
    Friend WithEvents txtbranchdesc As common.Controls.MyTextBox
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents dtpremittancedate As common.Controls.MyDateTimePicker
    Friend WithEvents btnvoidremittance As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnpost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Public WithEvents fndremittance As common.UserControls.txtFinder
    Friend WithEvents lblremittanceno As common.Controls.MyLabel
    Friend WithEvents lbldesc As common.Controls.MyLabel
    Friend WithEvents lblsection As common.Controls.MyLabel
    Friend WithEvents lblbranch As common.Controls.MyLabel
    Friend WithEvents lblfiscalquarter As common.Controls.MyLabel
    Friend WithEvents lblfiscalyear As common.Controls.MyLabel
    Friend WithEvents lblbankcode As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents RadLabel5 As common.Controls.MyLabel
    Friend WithEvents RadLabel6 As common.Controls.MyLabel
    Friend WithEvents RadLabel7 As common.Controls.MyLabel
    Friend WithEvents RadLabel8 As common.Controls.MyLabel
    Friend WithEvents lblchequeno As common.Controls.MyLabel
    Friend WithEvents lblchequedate As common.Controls.MyLabel
    Friend WithEvents RadLabel11 As common.Controls.MyLabel
    Friend WithEvents RadLabel12 As common.Controls.MyLabel
    Friend WithEvents RadLabel13 As common.Controls.MyLabel
    Friend WithEvents RadLabel14 As common.Controls.MyLabel
    Friend WithEvents lblremittancedate As common.Controls.MyLabel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents fndbankcode As common.UserControls.txtFinder
    Friend WithEvents fndpaymentcode As common.UserControls.txtFinder
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnTaxChallan As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnUpdateChallanAndBSR As Telerik.WinControls.UI.RadButton
End Class

