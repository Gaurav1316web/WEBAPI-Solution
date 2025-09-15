<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class BMC_Transporter_Bill
    Inherits FrmMainTranScreen
    'Inherits System.Windows.Forms.Form

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
        Dim TableViewDefinition6 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UsLock1 = New common.usLock()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtFatShortage = New common.Controls.MyTextBox()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.lblFatShortage = New common.Controls.MyLabel()
        Me.TxtSnfShortage = New common.Controls.MyTextBox()
        Me.TxtTankerprorata = New common.Controls.MyTextBox()
        Me.TxtIceCharge = New common.Controls.MyTextBox()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.TxtKMRate = New common.Controls.MyTextBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.TxtBarelCap = New common.Controls.MyTextBox()
        Me.TxtTollTax = New common.Controls.MyTextBox()
        Me.TxtFatRate = New common.Controls.MyTextBox()
        Me.TxtDieselMinus = New common.Controls.MyTextBox()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.TxtSnfRate = New common.Controls.MyTextBox()
        Me.txtDieselplus = New common.Controls.MyTextBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.TxtRateprorata = New common.Controls.MyTextBox()
        Me.TxtTDR = New common.Controls.MyTextBox()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.chkDCS = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.txtTransporter = New common.Controls.MyTextBox()
        Me.lblTransporter = New common.Controls.MyLabel()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.lblTankerDesc = New common.Controls.MyLabel()
        Me.lblAbandonmentNo = New common.Controls.MyLabel()
        Me.lblTankerNo = New common.Controls.MyLabel()
        Me.txtTankerNo = New common.UserControls.txtFinder()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.RadPageViewPage4 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.MyLabel47 = New common.Controls.MyLabel()
        Me.MyLabel46 = New common.Controls.MyLabel()
        Me.MyLabel45 = New common.Controls.MyLabel()
        Me.MyLabel44 = New common.Controls.MyLabel()
        Me.MyLabel43 = New common.Controls.MyLabel()
        Me.MyLabel42 = New common.Controls.MyLabel()
        Me.MyLabel41 = New common.Controls.MyLabel()
        Me.MyLabel40 = New common.Controls.MyLabel()
        Me.TxtTotalIceCharge = New common.Controls.MyLabel()
        Me.TxtTotalAmount = New common.Controls.MyLabel()
        Me.TxtTotalFatSnfShortage = New common.Controls.MyLabel()
        Me.TxtGrossAmount = New common.Controls.MyLabel()
        Me.TxtTotalTollTax = New common.Controls.MyLabel()
        Me.MyLabel34 = New common.Controls.MyLabel()
        Me.MyLabel33 = New common.Controls.MyLabel()
        Me.MyLabel32 = New common.Controls.MyLabel()
        Me.MyLabel31 = New common.Controls.MyLabel()
        Me.TxtBMCTotal = New common.Controls.MyLabel()
        Me.TxtBMCDiesel = New common.Controls.MyLabel()
        Me.TxtBMCProrataamt = New common.Controls.MyLabel()
        Me.MyLabel27 = New common.Controls.MyLabel()
        Me.MyLabel26 = New common.Controls.MyLabel()
        Me.MyLabel25 = New common.Controls.MyLabel()
        Me.MyLabel24 = New common.Controls.MyLabel()
        Me.MyLabel23 = New common.Controls.MyLabel()
        Me.MyLabel22 = New common.Controls.MyLabel()
        Me.MyLabel21 = New common.Controls.MyLabel()
        Me.MyLabel20 = New common.Controls.MyLabel()
        Me.MyLabel19 = New common.Controls.MyLabel()
        Me.MyLabel18 = New common.Controls.MyLabel()
        Me.MyLabel17 = New common.Controls.MyLabel()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.MyLabel15 = New common.Controls.MyLabel()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.lblInvoiceDiscAmt = New common.Controls.MyLabel()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.RadLabel32 = New common.Controls.MyLabel()
        Me.lblAddCharges1 = New common.Controls.MyLabel()
        Me.RadLabel9 = New common.Controls.MyLabel()
        Me.RadLabel27 = New common.Controls.MyLabel()
        Me.lblTotRAmt = New common.Controls.MyLabel()
        Me.RadLabel25 = New common.Controls.MyLabel()
        Me.lblTaxAmt = New common.Controls.MyLabel()
        Me.lblAmtAfterDiscount = New common.Controls.MyLabel()
        Me.lblDiscountAmt = New common.Controls.MyLabel()
        Me.lblAmtWithDiscount = New common.Controls.MyLabel()
        Me.RadLabel22 = New common.Controls.MyLabel()
        Me.RadLabel19 = New common.Controls.MyLabel()
        Me.btnReverse = New Telerik.WinControls.UI.RadButton()
        Me.btnLayout = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmiImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnCancel = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.txtFatShortage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFatShortage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtSnfShortage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtTankerprorata, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtIceCharge, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtKMRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtBarelCap, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtTollTax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtFatRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtDieselMinus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtSnfRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDieselplus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtRateprorata, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtTDR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDCS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTransporter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTransporter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTankerDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAbandonmentNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTankerNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage4.SuspendLayout()
        CType(Me.MyLabel47, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel46, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel45, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel44, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel43, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel42, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel41, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel40, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtTotalIceCharge, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtTotalAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtTotalFatSnfShortage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtGrossAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtTotalTollTax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel34, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel33, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel32, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel31, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtBMCTotal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtBMCDiesel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtBMCProrataamt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel27, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel26, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblInvoiceDiscAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel32, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAddCharges1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel27, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotRAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTaxAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAmtAfterDiscount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDiscountAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAmtWithDiscount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnLayout, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReverse)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnLayout)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnCancel)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(800, 450)
        Me.SplitContainer1.SplitterDistance = 413
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage4)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(800, 413)
        Me.RadPageView1.TabIndex = 1
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.UsLock1)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.chkDCS)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.btnAddNew)
        Me.RadPageViewPage1.Controls.Add(Me.txtTransporter)
        Me.RadPageViewPage1.Controls.Add(Me.lblTransporter)
        Me.RadPageViewPage1.Controls.Add(Me.txtDate)
        Me.RadPageViewPage1.Controls.Add(Me.lblTankerDesc)
        Me.RadPageViewPage1.Controls.Add(Me.lblAbandonmentNo)
        Me.RadPageViewPage1.Controls.Add(Me.lblTankerNo)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.txtTankerNo)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.txtDocNo)
        Me.RadPageViewPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(93.0!, 26.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(779, 367)
        Me.RadPageViewPage1.Text = "Transporter Bill"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(506, -2)
        Me.UsLock1.Margin = New System.Windows.Forms.Padding(4)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(118, 22)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 1555
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.txtFatShortage)
        Me.RadGroupBox3.Controls.Add(Me.btnGo)
        Me.RadGroupBox3.Controls.Add(Me.lblFatShortage)
        Me.RadGroupBox3.Controls.Add(Me.TxtSnfShortage)
        Me.RadGroupBox3.Controls.Add(Me.TxtTankerprorata)
        Me.RadGroupBox3.Controls.Add(Me.TxtIceCharge)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel11)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel13)
        Me.RadGroupBox3.Controls.Add(Me.TxtKMRate)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel8)
        Me.RadGroupBox3.Controls.Add(Me.TxtBarelCap)
        Me.RadGroupBox3.Controls.Add(Me.TxtTollTax)
        Me.RadGroupBox3.Controls.Add(Me.TxtFatRate)
        Me.RadGroupBox3.Controls.Add(Me.TxtDieselMinus)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel12)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel7)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel5)
        Me.RadGroupBox3.Controls.Add(Me.TxtSnfRate)
        Me.RadGroupBox3.Controls.Add(Me.txtDieselplus)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel4)
        Me.RadGroupBox3.Controls.Add(Me.TxtRateprorata)
        Me.RadGroupBox3.Controls.Add(Me.TxtTDR)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel9)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel10)
        Me.RadGroupBox3.HeaderText = ""
        Me.RadGroupBox3.Location = New System.Drawing.Point(3, 71)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Size = New System.Drawing.Size(763, 126)
        Me.RadGroupBox3.TabIndex = 1554
        '
        'txtFatShortage
        '
        Me.txtFatShortage.CalculationExpression = Nothing
        Me.txtFatShortage.FieldCode = Nothing
        Me.txtFatShortage.FieldDesc = Nothing
        Me.txtFatShortage.FieldMaxLength = 0
        Me.txtFatShortage.FieldName = Nothing
        Me.txtFatShortage.isCalculatedField = False
        Me.txtFatShortage.IsSourceFromTable = False
        Me.txtFatShortage.IsSourceFromValueList = False
        Me.txtFatShortage.IsUnique = False
        Me.txtFatShortage.Location = New System.Drawing.Point(676, 5)
        Me.txtFatShortage.MendatroryField = False
        Me.txtFatShortage.MyLinkLable1 = Nothing
        Me.txtFatShortage.MyLinkLable2 = Nothing
        Me.txtFatShortage.Name = "txtFatShortage"
        Me.txtFatShortage.ReadOnly = True
        Me.txtFatShortage.ReferenceFieldDesc = Nothing
        Me.txtFatShortage.ReferenceFieldName = Nothing
        Me.txtFatShortage.ReferenceTableName = Nothing
        Me.txtFatShortage.Size = New System.Drawing.Size(82, 20)
        Me.txtFatShortage.TabIndex = 1534
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(503, 96)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(57, 17)
        Me.btnGo.TabIndex = 1553
        Me.btnGo.Text = ">>"
        '
        'lblFatShortage
        '
        Me.lblFatShortage.FieldName = Nothing
        Me.lblFatShortage.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFatShortage.Location = New System.Drawing.Point(596, 8)
        Me.lblFatShortage.Name = "lblFatShortage"
        Me.lblFatShortage.Size = New System.Drawing.Size(77, 16)
        Me.lblFatShortage.TabIndex = 1533
        Me.lblFatShortage.Text = "FAT Shortage"
        '
        'TxtSnfShortage
        '
        Me.TxtSnfShortage.CalculationExpression = Nothing
        Me.TxtSnfShortage.FieldCode = Nothing
        Me.TxtSnfShortage.FieldDesc = Nothing
        Me.TxtSnfShortage.FieldMaxLength = 0
        Me.TxtSnfShortage.FieldName = Nothing
        Me.TxtSnfShortage.isCalculatedField = False
        Me.TxtSnfShortage.IsSourceFromTable = False
        Me.TxtSnfShortage.IsSourceFromValueList = False
        Me.TxtSnfShortage.IsUnique = False
        Me.TxtSnfShortage.Location = New System.Drawing.Point(676, 31)
        Me.TxtSnfShortage.MendatroryField = False
        Me.TxtSnfShortage.MyLinkLable1 = Nothing
        Me.TxtSnfShortage.MyLinkLable2 = Nothing
        Me.TxtSnfShortage.Name = "TxtSnfShortage"
        Me.TxtSnfShortage.ReadOnly = True
        Me.TxtSnfShortage.ReferenceFieldDesc = Nothing
        Me.TxtSnfShortage.ReferenceFieldName = Nothing
        Me.TxtSnfShortage.ReferenceTableName = Nothing
        Me.TxtSnfShortage.Size = New System.Drawing.Size(82, 20)
        Me.TxtSnfShortage.TabIndex = 1535
        '
        'TxtTankerprorata
        '
        Me.TxtTankerprorata.CalculationExpression = Nothing
        Me.TxtTankerprorata.FieldCode = Nothing
        Me.TxtTankerprorata.FieldDesc = Nothing
        Me.TxtTankerprorata.FieldMaxLength = 0
        Me.TxtTankerprorata.FieldName = Nothing
        Me.TxtTankerprorata.isCalculatedField = False
        Me.TxtTankerprorata.IsSourceFromTable = False
        Me.TxtTankerprorata.IsSourceFromValueList = False
        Me.TxtTankerprorata.IsUnique = False
        Me.TxtTankerprorata.Location = New System.Drawing.Point(87, 92)
        Me.TxtTankerprorata.MendatroryField = False
        Me.TxtTankerprorata.MyLinkLable1 = Nothing
        Me.TxtTankerprorata.MyLinkLable2 = Nothing
        Me.TxtTankerprorata.Name = "TxtTankerprorata"
        Me.TxtTankerprorata.ReferenceFieldDesc = Nothing
        Me.TxtTankerprorata.ReferenceFieldName = Nothing
        Me.TxtTankerprorata.ReferenceTableName = Nothing
        Me.TxtTankerprorata.Size = New System.Drawing.Size(82, 20)
        Me.TxtTankerprorata.TabIndex = 1547
        '
        'TxtIceCharge
        '
        Me.TxtIceCharge.CalculationExpression = Nothing
        Me.TxtIceCharge.FieldCode = Nothing
        Me.TxtIceCharge.FieldDesc = Nothing
        Me.TxtIceCharge.FieldMaxLength = 0
        Me.TxtIceCharge.FieldName = Nothing
        Me.TxtIceCharge.isCalculatedField = False
        Me.TxtIceCharge.IsSourceFromTable = False
        Me.TxtIceCharge.IsSourceFromValueList = False
        Me.TxtIceCharge.IsUnique = False
        Me.TxtIceCharge.Location = New System.Drawing.Point(379, 42)
        Me.TxtIceCharge.MendatroryField = False
        Me.TxtIceCharge.MyLinkLable1 = Nothing
        Me.TxtIceCharge.MyLinkLable2 = Nothing
        Me.TxtIceCharge.Name = "TxtIceCharge"
        Me.TxtIceCharge.ReferenceFieldDesc = Nothing
        Me.TxtIceCharge.ReferenceFieldName = Nothing
        Me.TxtIceCharge.ReferenceTableName = Nothing
        Me.TxtIceCharge.Size = New System.Drawing.Size(82, 20)
        Me.TxtIceCharge.TabIndex = 1551
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel11.Location = New System.Drawing.Point(9, 96)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(61, 16)
        Me.MyLabel11.TabIndex = 1546
        Me.MyLabel11.Text = "PRORATA"
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel13.Location = New System.Drawing.Point(301, 42)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(62, 16)
        Me.MyLabel13.TabIndex = 1550
        Me.MyLabel13.Text = "Ice Charge"
        '
        'TxtKMRate
        '
        Me.TxtKMRate.CalculationExpression = Nothing
        Me.TxtKMRate.FieldCode = Nothing
        Me.TxtKMRate.FieldDesc = Nothing
        Me.TxtKMRate.FieldMaxLength = 0
        Me.TxtKMRate.FieldName = Nothing
        Me.TxtKMRate.isCalculatedField = False
        Me.TxtKMRate.IsSourceFromTable = False
        Me.TxtKMRate.IsSourceFromValueList = False
        Me.TxtKMRate.IsUnique = False
        Me.TxtKMRate.Location = New System.Drawing.Point(87, 70)
        Me.TxtKMRate.MendatroryField = False
        Me.TxtKMRate.MyLinkLable1 = Nothing
        Me.TxtKMRate.MyLinkLable2 = Nothing
        Me.TxtKMRate.Name = "TxtKMRate"
        Me.TxtKMRate.ReferenceFieldDesc = Nothing
        Me.TxtKMRate.ReferenceFieldName = Nothing
        Me.TxtKMRate.ReferenceTableName = Nothing
        Me.TxtKMRate.Size = New System.Drawing.Size(82, 20)
        Me.TxtKMRate.TabIndex = 1541
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(596, 31)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(78, 16)
        Me.MyLabel2.TabIndex = 1532
        Me.MyLabel2.Text = "SNF Shortage"
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(9, 74)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(50, 16)
        Me.MyLabel8.TabIndex = 1540
        Me.MyLabel8.Text = "KM Rate"
        '
        'TxtBarelCap
        '
        Me.TxtBarelCap.CalculationExpression = Nothing
        Me.TxtBarelCap.FieldCode = Nothing
        Me.TxtBarelCap.FieldDesc = Nothing
        Me.TxtBarelCap.FieldMaxLength = 0
        Me.TxtBarelCap.FieldName = Nothing
        Me.TxtBarelCap.isCalculatedField = False
        Me.TxtBarelCap.IsSourceFromTable = False
        Me.TxtBarelCap.IsSourceFromValueList = False
        Me.TxtBarelCap.IsUnique = False
        Me.TxtBarelCap.Location = New System.Drawing.Point(379, 88)
        Me.TxtBarelCap.MendatroryField = False
        Me.TxtBarelCap.MyLinkLable1 = Nothing
        Me.TxtBarelCap.MyLinkLable2 = Nothing
        Me.TxtBarelCap.Name = "TxtBarelCap"
        Me.TxtBarelCap.ReferenceFieldDesc = Nothing
        Me.TxtBarelCap.ReferenceFieldName = Nothing
        Me.TxtBarelCap.ReferenceTableName = Nothing
        Me.TxtBarelCap.Size = New System.Drawing.Size(82, 20)
        Me.TxtBarelCap.TabIndex = 1549
        '
        'TxtTollTax
        '
        Me.TxtTollTax.CalculationExpression = Nothing
        Me.TxtTollTax.FieldCode = Nothing
        Me.TxtTollTax.FieldDesc = Nothing
        Me.TxtTollTax.FieldMaxLength = 0
        Me.TxtTollTax.FieldName = Nothing
        Me.TxtTollTax.isCalculatedField = False
        Me.TxtTollTax.IsSourceFromTable = False
        Me.TxtTollTax.IsSourceFromValueList = False
        Me.TxtTollTax.IsUnique = False
        Me.TxtTollTax.Location = New System.Drawing.Point(87, 49)
        Me.TxtTollTax.MendatroryField = False
        Me.TxtTollTax.MyLinkLable1 = Nothing
        Me.TxtTollTax.MyLinkLable2 = Nothing
        Me.TxtTollTax.Name = "TxtTollTax"
        Me.TxtTollTax.ReferenceFieldDesc = Nothing
        Me.TxtTollTax.ReferenceFieldName = Nothing
        Me.TxtTollTax.ReferenceTableName = Nothing
        Me.TxtTollTax.Size = New System.Drawing.Size(82, 20)
        Me.TxtTollTax.TabIndex = 1536
        '
        'TxtFatRate
        '
        Me.TxtFatRate.CalculationExpression = Nothing
        Me.TxtFatRate.FieldCode = Nothing
        Me.TxtFatRate.FieldDesc = Nothing
        Me.TxtFatRate.FieldMaxLength = 0
        Me.TxtFatRate.FieldName = Nothing
        Me.TxtFatRate.isCalculatedField = False
        Me.TxtFatRate.IsSourceFromTable = False
        Me.TxtFatRate.IsSourceFromValueList = False
        Me.TxtFatRate.IsUnique = False
        Me.TxtFatRate.Location = New System.Drawing.Point(676, 57)
        Me.TxtFatRate.MendatroryField = False
        Me.TxtFatRate.MyLinkLable1 = Nothing
        Me.TxtFatRate.MyLinkLable2 = Nothing
        Me.TxtFatRate.Name = "TxtFatRate"
        Me.TxtFatRate.ReadOnly = True
        Me.TxtFatRate.ReferenceFieldDesc = Nothing
        Me.TxtFatRate.ReferenceFieldName = Nothing
        Me.TxtFatRate.ReferenceTableName = Nothing
        Me.TxtFatRate.Size = New System.Drawing.Size(82, 20)
        Me.TxtFatRate.TabIndex = 1535
        '
        'TxtDieselMinus
        '
        Me.TxtDieselMinus.CalculationExpression = Nothing
        Me.TxtDieselMinus.FieldCode = Nothing
        Me.TxtDieselMinus.FieldDesc = Nothing
        Me.TxtDieselMinus.FieldMaxLength = 0
        Me.TxtDieselMinus.FieldName = Nothing
        Me.TxtDieselMinus.isCalculatedField = False
        Me.TxtDieselMinus.IsSourceFromTable = False
        Me.TxtDieselMinus.IsSourceFromValueList = False
        Me.TxtDieselMinus.IsUnique = False
        Me.TxtDieselMinus.Location = New System.Drawing.Point(87, 27)
        Me.TxtDieselMinus.MendatroryField = False
        Me.TxtDieselMinus.MyLinkLable1 = Nothing
        Me.TxtDieselMinus.MyLinkLable2 = Nothing
        Me.TxtDieselMinus.Name = "TxtDieselMinus"
        Me.TxtDieselMinus.ReferenceFieldDesc = Nothing
        Me.TxtDieselMinus.ReferenceFieldName = Nothing
        Me.TxtDieselMinus.ReferenceTableName = Nothing
        Me.TxtDieselMinus.Size = New System.Drawing.Size(82, 20)
        Me.TxtDieselMinus.TabIndex = 1539
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel12.Location = New System.Drawing.Point(301, 92)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(80, 16)
        Me.MyLabel12.TabIndex = 1548
        Me.MyLabel12.Text = "Barel Capacity"
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(9, 52)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(46, 16)
        Me.MyLabel7.TabIndex = 1535
        Me.MyLabel7.Text = "Toll Tax"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(596, 57)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(55, 16)
        Me.MyLabel3.TabIndex = 1532
        Me.MyLabel3.Text = "FAT Rate"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(9, 30)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(72, 16)
        Me.MyLabel5.TabIndex = 1538
        Me.MyLabel5.Text = "Diesel Rate -"
        '
        'TxtSnfRate
        '
        Me.TxtSnfRate.CalculationExpression = Nothing
        Me.TxtSnfRate.FieldCode = Nothing
        Me.TxtSnfRate.FieldDesc = Nothing
        Me.TxtSnfRate.FieldMaxLength = 0
        Me.TxtSnfRate.FieldName = Nothing
        Me.TxtSnfRate.isCalculatedField = False
        Me.TxtSnfRate.IsSourceFromTable = False
        Me.TxtSnfRate.IsSourceFromValueList = False
        Me.TxtSnfRate.IsUnique = False
        Me.TxtSnfRate.Location = New System.Drawing.Point(676, 83)
        Me.TxtSnfRate.MendatroryField = False
        Me.TxtSnfRate.MyLinkLable1 = Nothing
        Me.TxtSnfRate.MyLinkLable2 = Nothing
        Me.TxtSnfRate.Name = "TxtSnfRate"
        Me.TxtSnfRate.ReadOnly = True
        Me.TxtSnfRate.ReferenceFieldDesc = Nothing
        Me.TxtSnfRate.ReferenceFieldName = Nothing
        Me.TxtSnfRate.ReferenceTableName = Nothing
        Me.TxtSnfRate.Size = New System.Drawing.Size(82, 20)
        Me.TxtSnfRate.TabIndex = 1535
        '
        'txtDieselplus
        '
        Me.txtDieselplus.CalculationExpression = Nothing
        Me.txtDieselplus.FieldCode = Nothing
        Me.txtDieselplus.FieldDesc = Nothing
        Me.txtDieselplus.FieldMaxLength = 0
        Me.txtDieselplus.FieldName = Nothing
        Me.txtDieselplus.isCalculatedField = False
        Me.txtDieselplus.IsSourceFromTable = False
        Me.txtDieselplus.IsSourceFromValueList = False
        Me.txtDieselplus.IsUnique = False
        Me.txtDieselplus.Location = New System.Drawing.Point(87, 4)
        Me.txtDieselplus.MendatroryField = False
        Me.txtDieselplus.MyLinkLable1 = Nothing
        Me.txtDieselplus.MyLinkLable2 = Nothing
        Me.txtDieselplus.Name = "txtDieselplus"
        Me.txtDieselplus.ReferenceFieldDesc = Nothing
        Me.txtDieselplus.ReferenceFieldName = Nothing
        Me.txtDieselplus.ReferenceTableName = Nothing
        Me.txtDieselplus.Size = New System.Drawing.Size(82, 20)
        Me.txtDieselplus.TabIndex = 1537
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(9, 8)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(74, 16)
        Me.MyLabel1.TabIndex = 1536
        Me.MyLabel1.Text = "Diesel Rate +"
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(596, 83)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(56, 16)
        Me.MyLabel4.TabIndex = 1532
        Me.MyLabel4.Text = "SNF Rate"
        '
        'TxtRateprorata
        '
        Me.TxtRateprorata.CalculationExpression = Nothing
        Me.TxtRateprorata.FieldCode = Nothing
        Me.TxtRateprorata.FieldDesc = Nothing
        Me.TxtRateprorata.FieldMaxLength = 0
        Me.TxtRateprorata.FieldName = Nothing
        Me.TxtRateprorata.isCalculatedField = False
        Me.TxtRateprorata.IsSourceFromTable = False
        Me.TxtRateprorata.IsSourceFromValueList = False
        Me.TxtRateprorata.IsUnique = False
        Me.TxtRateprorata.Location = New System.Drawing.Point(379, 65)
        Me.TxtRateprorata.MendatroryField = False
        Me.TxtRateprorata.MyLinkLable1 = Nothing
        Me.TxtRateprorata.MyLinkLable2 = Nothing
        Me.TxtRateprorata.Name = "TxtRateprorata"
        Me.TxtRateprorata.ReferenceFieldDesc = Nothing
        Me.TxtRateprorata.ReferenceFieldName = Nothing
        Me.TxtRateprorata.ReferenceTableName = Nothing
        Me.TxtRateprorata.Size = New System.Drawing.Size(82, 20)
        Me.TxtRateprorata.TabIndex = 1543
        '
        'TxtTDR
        '
        Me.TxtTDR.CalculationExpression = Nothing
        Me.TxtTDR.FieldCode = Nothing
        Me.TxtTDR.FieldDesc = Nothing
        Me.TxtTDR.FieldMaxLength = 0
        Me.TxtTDR.FieldName = Nothing
        Me.TxtTDR.isCalculatedField = False
        Me.TxtTDR.IsSourceFromTable = False
        Me.TxtTDR.IsSourceFromValueList = False
        Me.TxtTDR.IsUnique = False
        Me.TxtTDR.Location = New System.Drawing.Point(503, 59)
        Me.TxtTDR.MendatroryField = False
        Me.TxtTDR.MyLinkLable1 = Nothing
        Me.TxtTDR.MyLinkLable2 = Nothing
        Me.TxtTDR.Name = "TxtTDR"
        Me.TxtTDR.ReferenceFieldDesc = Nothing
        Me.TxtTDR.ReferenceFieldName = Nothing
        Me.TxtTDR.ReferenceTableName = Nothing
        Me.TxtTDR.Size = New System.Drawing.Size(82, 20)
        Me.TxtTDR.TabIndex = 1545
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(301, 69)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(61, 16)
        Me.MyLabel9.TabIndex = 1542
        Me.MyLabel9.Text = "PRORATA"
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(468, 62)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(29, 16)
        Me.MyLabel10.TabIndex = 1544
        Me.MyLabel10.Text = "TDR"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.txtToDate)
        Me.RadGroupBox1.Controls.Add(Me.txtFromDate)
        Me.RadGroupBox1.Controls.Add(Me.Label2)
        Me.RadGroupBox1.Controls.Add(Me.Label1)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(448, 22)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(304, 43)
        Me.RadGroupBox1.TabIndex = 1552
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
        Me.txtToDate.Location = New System.Drawing.Point(215, 9)
        Me.txtToDate.MendatroryField = False
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Nothing
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.ReferenceFieldDesc = Nothing
        Me.txtToDate.ReferenceFieldName = Nothing
        Me.txtToDate.ReferenceTableName = Nothing
        Me.txtToDate.Size = New System.Drawing.Size(82, 20)
        Me.txtToDate.TabIndex = 3
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "13-07-2023"
        Me.txtToDate.Value = New Date(2023, 7, 13, 0, 0, 0, 0)
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
        Me.txtFromDate.Location = New System.Drawing.Point(70, 9)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Nothing
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.ReferenceFieldDesc = Nothing
        Me.txtFromDate.ReferenceFieldName = Nothing
        Me.txtFromDate.ReferenceTableName = Nothing
        Me.txtFromDate.Size = New System.Drawing.Size(82, 20)
        Me.txtFromDate.TabIndex = 2
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "13-07-2023"
        Me.txtFromDate.Value = New Date(2023, 7, 13, 0, 0, 0, 0)
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(164, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(46, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "To Date"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(5, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "From Date"
        '
        'chkDCS
        '
        Me.chkDCS.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDCS.Location = New System.Drawing.Point(802, 1)
        Me.chkDCS.Name = "chkDCS"
        Me.chkDCS.Size = New System.Drawing.Size(44, 16)
        Me.chkDCS.TabIndex = 1484
        Me.chkDCS.Text = "DCS"
        Me.chkDCS.Visible = False
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox2.Controls.Add(Me.gv1)
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "BMC Details"
        Me.RadGroupBox2.Location = New System.Drawing.Point(2, 203)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(774, 156)
        Me.RadGroupBox2.TabIndex = 28
        Me.RadGroupBox2.Text = "BMC Details"
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
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition6
        Me.gv1.MyExportFilePath = ""
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(754, 126)
        Me.gv1.TabIndex = 17
        Me.gv1.TabStop = False
        Me.gv1.VarID = ""
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.XpertERPBulkProcurement.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(314, 1)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 21)
        Me.btnAddNew.TabIndex = 1
        '
        'txtTransporter
        '
        Me.txtTransporter.CalculationExpression = Nothing
        Me.txtTransporter.FieldCode = Nothing
        Me.txtTransporter.FieldDesc = Nothing
        Me.txtTransporter.FieldMaxLength = 0
        Me.txtTransporter.FieldName = Nothing
        Me.txtTransporter.isCalculatedField = False
        Me.txtTransporter.IsSourceFromTable = False
        Me.txtTransporter.IsSourceFromValueList = False
        Me.txtTransporter.IsUnique = False
        Me.txtTransporter.Location = New System.Drawing.Point(78, 49)
        Me.txtTransporter.MendatroryField = False
        Me.txtTransporter.MyLinkLable1 = Nothing
        Me.txtTransporter.MyLinkLable2 = Nothing
        Me.txtTransporter.Name = "txtTransporter"
        Me.txtTransporter.ReferenceFieldDesc = Nothing
        Me.txtTransporter.ReferenceFieldName = Nothing
        Me.txtTransporter.ReferenceTableName = Nothing
        Me.txtTransporter.Size = New System.Drawing.Size(257, 20)
        Me.txtTransporter.TabIndex = 1532
        '
        'lblTransporter
        '
        Me.lblTransporter.FieldName = Nothing
        Me.lblTransporter.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTransporter.Location = New System.Drawing.Point(0, 49)
        Me.lblTransporter.Name = "lblTransporter"
        Me.lblTransporter.Size = New System.Drawing.Size(65, 16)
        Me.lblTransporter.TabIndex = 1531
        Me.lblTransporter.Text = "Transporter"
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
        Me.txtDate.Location = New System.Drawing.Point(373, 2)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.RadLabel4
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(127, 18)
        Me.txtDate.TabIndex = 1524
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13-06-2011 11:29 AM"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(341, 3)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 1
        Me.RadLabel4.Text = "Date"
        '
        'lblTankerDesc
        '
        Me.lblTankerDesc.AutoSize = False
        Me.lblTankerDesc.BorderVisible = True
        Me.lblTankerDesc.FieldName = Nothing
        Me.lblTankerDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTankerDesc.Location = New System.Drawing.Point(199, 27)
        Me.lblTankerDesc.Name = "lblTankerDesc"
        Me.lblTankerDesc.Size = New System.Drawing.Size(227, 19)
        Me.lblTankerDesc.TabIndex = 144
        Me.lblTankerDesc.TextWrap = False
        '
        'lblAbandonmentNo
        '
        Me.lblAbandonmentNo.FieldName = Nothing
        Me.lblAbandonmentNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAbandonmentNo.Location = New System.Drawing.Point(764, 11)
        Me.lblAbandonmentNo.Name = "lblAbandonmentNo"
        Me.lblAbandonmentNo.Size = New System.Drawing.Size(2, 2)
        Me.lblAbandonmentNo.TabIndex = 27
        '
        'lblTankerNo
        '
        Me.lblTankerNo.FieldName = Nothing
        Me.lblTankerNo.Location = New System.Drawing.Point(-1, 25)
        Me.lblTankerNo.Name = "lblTankerNo"
        Me.lblTankerNo.Size = New System.Drawing.Size(60, 18)
        Me.lblTankerNo.TabIndex = 119
        Me.lblTankerNo.Text = "Tanker No."
        '
        'txtTankerNo
        '
        Me.txtTankerNo.CalculationExpression = Nothing
        Me.txtTankerNo.FieldCode = Nothing
        Me.txtTankerNo.FieldDesc = Nothing
        Me.txtTankerNo.FieldMaxLength = 0
        Me.txtTankerNo.FieldName = Nothing
        Me.txtTankerNo.isCalculatedField = False
        Me.txtTankerNo.IsSourceFromTable = False
        Me.txtTankerNo.IsSourceFromValueList = False
        Me.txtTankerNo.IsUnique = False
        Me.txtTankerNo.Location = New System.Drawing.Point(78, 27)
        Me.txtTankerNo.MendatroryField = False
        Me.txtTankerNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTankerNo.MyLinkLable1 = Me.lblTankerNo
        Me.txtTankerNo.MyLinkLable2 = Nothing
        Me.txtTankerNo.MyReadOnly = False
        Me.txtTankerNo.MyShowMasterFormButton = False
        Me.txtTankerNo.Name = "txtTankerNo"
        Me.txtTankerNo.ReferenceFieldDesc = Nothing
        Me.txtTankerNo.ReferenceFieldName = Nothing
        Me.txtTankerNo.ReferenceTableName = Nothing
        Me.txtTankerNo.Size = New System.Drawing.Size(115, 19)
        Me.txtTankerNo.TabIndex = 22
        Me.txtTankerNo.Value = ""
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(-1, 3)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(33, 16)
        Me.RadLabel1.TabIndex = 22
        Me.RadLabel1.Text = "Code"
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(46, 1)
        Me.txtDocNo.MendatroryField = False
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Me.RadLabel1
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 32767
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(269, 22)
        Me.txtDocNo.TabIndex = 0
        Me.txtDocNo.TabStop = False
        Me.txtDocNo.Value = ""
        '
        'RadPageViewPage4
        '
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel47)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel46)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel45)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel44)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel43)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel42)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel41)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel40)
        Me.RadPageViewPage4.Controls.Add(Me.TxtTotalIceCharge)
        Me.RadPageViewPage4.Controls.Add(Me.TxtTotalAmount)
        Me.RadPageViewPage4.Controls.Add(Me.TxtTotalFatSnfShortage)
        Me.RadPageViewPage4.Controls.Add(Me.TxtGrossAmount)
        Me.RadPageViewPage4.Controls.Add(Me.TxtTotalTollTax)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel34)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel33)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel32)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel31)
        Me.RadPageViewPage4.Controls.Add(Me.TxtBMCTotal)
        Me.RadPageViewPage4.Controls.Add(Me.TxtBMCDiesel)
        Me.RadPageViewPage4.Controls.Add(Me.TxtBMCProrataamt)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel27)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel26)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel25)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel24)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel23)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel22)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel21)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel20)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel19)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel18)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel17)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel16)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel15)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel14)
        Me.RadPageViewPage4.Controls.Add(Me.lblInvoiceDiscAmt)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel6)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel32)
        Me.RadPageViewPage4.Controls.Add(Me.lblAddCharges1)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel9)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel27)
        Me.RadPageViewPage4.Controls.Add(Me.lblTotRAmt)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel25)
        Me.RadPageViewPage4.Controls.Add(Me.lblTaxAmt)
        Me.RadPageViewPage4.Controls.Add(Me.lblAmtAfterDiscount)
        Me.RadPageViewPage4.Controls.Add(Me.lblDiscountAmt)
        Me.RadPageViewPage4.Controls.Add(Me.lblAmtWithDiscount)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel22)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel19)
        Me.RadPageViewPage4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage4.ItemSize = New System.Drawing.SizeF(41.0!, 26.0!)
        Me.RadPageViewPage4.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage4.Name = "RadPageViewPage4"
        Me.RadPageViewPage4.Size = New System.Drawing.Size(779, 367)
        Me.RadPageViewPage4.Text = "Total"
        '
        'MyLabel47
        '
        Me.MyLabel47.AutoSize = False
        Me.MyLabel47.BorderVisible = True
        Me.MyLabel47.FieldName = Nothing
        Me.MyLabel47.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel47.Location = New System.Drawing.Point(326, 150)
        Me.MyLabel47.Name = "MyLabel47"
        Me.MyLabel47.Size = New System.Drawing.Size(77, 18)
        Me.MyLabel47.TabIndex = 181
        Me.MyLabel47.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.MyLabel47.Visible = False
        '
        'MyLabel46
        '
        Me.MyLabel46.AutoSize = False
        Me.MyLabel46.BorderVisible = True
        Me.MyLabel46.FieldName = Nothing
        Me.MyLabel46.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel46.Location = New System.Drawing.Point(243, 150)
        Me.MyLabel46.Name = "MyLabel46"
        Me.MyLabel46.Size = New System.Drawing.Size(77, 18)
        Me.MyLabel46.TabIndex = 180
        Me.MyLabel46.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.MyLabel46.Visible = False
        '
        'MyLabel45
        '
        Me.MyLabel45.AutoSize = False
        Me.MyLabel45.BorderVisible = True
        Me.MyLabel45.FieldName = Nothing
        Me.MyLabel45.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel45.Location = New System.Drawing.Point(326, 126)
        Me.MyLabel45.Name = "MyLabel45"
        Me.MyLabel45.Size = New System.Drawing.Size(77, 18)
        Me.MyLabel45.TabIndex = 179
        Me.MyLabel45.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.MyLabel45.Visible = False
        '
        'MyLabel44
        '
        Me.MyLabel44.AutoSize = False
        Me.MyLabel44.BorderVisible = True
        Me.MyLabel44.FieldName = Nothing
        Me.MyLabel44.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel44.Location = New System.Drawing.Point(243, 126)
        Me.MyLabel44.Name = "MyLabel44"
        Me.MyLabel44.Size = New System.Drawing.Size(77, 18)
        Me.MyLabel44.TabIndex = 178
        Me.MyLabel44.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.MyLabel44.Visible = False
        '
        'MyLabel43
        '
        Me.MyLabel43.AutoSize = False
        Me.MyLabel43.BorderVisible = True
        Me.MyLabel43.FieldName = Nothing
        Me.MyLabel43.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel43.Location = New System.Drawing.Point(326, 102)
        Me.MyLabel43.Name = "MyLabel43"
        Me.MyLabel43.Size = New System.Drawing.Size(77, 18)
        Me.MyLabel43.TabIndex = 177
        Me.MyLabel43.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.MyLabel43.Visible = False
        '
        'MyLabel42
        '
        Me.MyLabel42.AutoSize = False
        Me.MyLabel42.BorderVisible = True
        Me.MyLabel42.FieldName = Nothing
        Me.MyLabel42.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel42.Location = New System.Drawing.Point(243, 101)
        Me.MyLabel42.Name = "MyLabel42"
        Me.MyLabel42.Size = New System.Drawing.Size(77, 18)
        Me.MyLabel42.TabIndex = 176
        Me.MyLabel42.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.MyLabel42.Visible = False
        '
        'MyLabel41
        '
        Me.MyLabel41.AutoSize = False
        Me.MyLabel41.BorderVisible = True
        Me.MyLabel41.FieldName = Nothing
        Me.MyLabel41.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel41.Location = New System.Drawing.Point(326, 76)
        Me.MyLabel41.Name = "MyLabel41"
        Me.MyLabel41.Size = New System.Drawing.Size(77, 18)
        Me.MyLabel41.TabIndex = 175
        Me.MyLabel41.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.MyLabel41.Visible = False
        '
        'MyLabel40
        '
        Me.MyLabel40.AutoSize = False
        Me.MyLabel40.BorderVisible = True
        Me.MyLabel40.FieldName = Nothing
        Me.MyLabel40.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel40.Location = New System.Drawing.Point(243, 76)
        Me.MyLabel40.Name = "MyLabel40"
        Me.MyLabel40.Size = New System.Drawing.Size(77, 18)
        Me.MyLabel40.TabIndex = 174
        Me.MyLabel40.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.MyLabel40.Visible = False
        '
        'TxtTotalIceCharge
        '
        Me.TxtTotalIceCharge.AutoSize = False
        Me.TxtTotalIceCharge.BorderVisible = True
        Me.TxtTotalIceCharge.FieldName = Nothing
        Me.TxtTotalIceCharge.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTotalIceCharge.Location = New System.Drawing.Point(326, 204)
        Me.TxtTotalIceCharge.Name = "TxtTotalIceCharge"
        Me.TxtTotalIceCharge.Size = New System.Drawing.Size(77, 18)
        Me.TxtTotalIceCharge.TabIndex = 173
        Me.TxtTotalIceCharge.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'TxtTotalAmount
        '
        Me.TxtTotalAmount.AutoSize = False
        Me.TxtTotalAmount.BorderVisible = True
        Me.TxtTotalAmount.FieldName = Nothing
        Me.TxtTotalAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTotalAmount.Location = New System.Drawing.Point(326, 227)
        Me.TxtTotalAmount.Name = "TxtTotalAmount"
        Me.TxtTotalAmount.Size = New System.Drawing.Size(77, 18)
        Me.TxtTotalAmount.TabIndex = 172
        Me.TxtTotalAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'TxtTotalFatSnfShortage
        '
        Me.TxtTotalFatSnfShortage.AutoSize = False
        Me.TxtTotalFatSnfShortage.BorderVisible = True
        Me.TxtTotalFatSnfShortage.FieldName = Nothing
        Me.TxtTotalFatSnfShortage.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTotalFatSnfShortage.Location = New System.Drawing.Point(326, 249)
        Me.TxtTotalFatSnfShortage.Name = "TxtTotalFatSnfShortage"
        Me.TxtTotalFatSnfShortage.Size = New System.Drawing.Size(77, 18)
        Me.TxtTotalFatSnfShortage.TabIndex = 165
        Me.TxtTotalFatSnfShortage.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'TxtGrossAmount
        '
        Me.TxtGrossAmount.AutoSize = False
        Me.TxtGrossAmount.BorderVisible = True
        Me.TxtGrossAmount.FieldName = Nothing
        Me.TxtGrossAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtGrossAmount.Location = New System.Drawing.Point(326, 271)
        Me.TxtGrossAmount.Name = "TxtGrossAmount"
        Me.TxtGrossAmount.Size = New System.Drawing.Size(77, 18)
        Me.TxtGrossAmount.TabIndex = 165
        Me.TxtGrossAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'TxtTotalTollTax
        '
        Me.TxtTotalTollTax.AutoSize = False
        Me.TxtTotalTollTax.BorderVisible = True
        Me.TxtTotalTollTax.FieldName = Nothing
        Me.TxtTotalTollTax.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTotalTollTax.Location = New System.Drawing.Point(326, 183)
        Me.TxtTotalTollTax.Name = "TxtTotalTollTax"
        Me.TxtTotalTollTax.Size = New System.Drawing.Size(77, 18)
        Me.TxtTotalTollTax.TabIndex = 171
        Me.TxtTotalTollTax.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel34
        '
        Me.MyLabel34.AutoSize = False
        Me.MyLabel34.BorderVisible = True
        Me.MyLabel34.FieldName = Nothing
        Me.MyLabel34.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel34.Location = New System.Drawing.Point(149, 148)
        Me.MyLabel34.Name = "MyLabel34"
        Me.MyLabel34.Size = New System.Drawing.Size(77, 18)
        Me.MyLabel34.TabIndex = 170
        Me.MyLabel34.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.MyLabel34.Visible = False
        '
        'MyLabel33
        '
        Me.MyLabel33.AutoSize = False
        Me.MyLabel33.BorderVisible = True
        Me.MyLabel33.FieldName = Nothing
        Me.MyLabel33.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel33.Location = New System.Drawing.Point(149, 124)
        Me.MyLabel33.Name = "MyLabel33"
        Me.MyLabel33.Size = New System.Drawing.Size(77, 18)
        Me.MyLabel33.TabIndex = 169
        Me.MyLabel33.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.MyLabel33.Visible = False
        '
        'MyLabel32
        '
        Me.MyLabel32.AutoSize = False
        Me.MyLabel32.BorderVisible = True
        Me.MyLabel32.FieldName = Nothing
        Me.MyLabel32.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel32.Location = New System.Drawing.Point(149, 100)
        Me.MyLabel32.Name = "MyLabel32"
        Me.MyLabel32.Size = New System.Drawing.Size(77, 18)
        Me.MyLabel32.TabIndex = 168
        Me.MyLabel32.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.MyLabel32.Visible = False
        '
        'MyLabel31
        '
        Me.MyLabel31.AutoSize = False
        Me.MyLabel31.BorderVisible = True
        Me.MyLabel31.FieldName = Nothing
        Me.MyLabel31.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel31.Location = New System.Drawing.Point(149, 76)
        Me.MyLabel31.Name = "MyLabel31"
        Me.MyLabel31.Size = New System.Drawing.Size(77, 18)
        Me.MyLabel31.TabIndex = 167
        Me.MyLabel31.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.MyLabel31.Visible = False
        '
        'TxtBMCTotal
        '
        Me.TxtBMCTotal.AutoSize = False
        Me.TxtBMCTotal.BorderVisible = True
        Me.TxtBMCTotal.FieldName = Nothing
        Me.TxtBMCTotal.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBMCTotal.Location = New System.Drawing.Point(326, 50)
        Me.TxtBMCTotal.Name = "TxtBMCTotal"
        Me.TxtBMCTotal.Size = New System.Drawing.Size(77, 18)
        Me.TxtBMCTotal.TabIndex = 166
        Me.TxtBMCTotal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'TxtBMCDiesel
        '
        Me.TxtBMCDiesel.AutoSize = False
        Me.TxtBMCDiesel.BorderVisible = True
        Me.TxtBMCDiesel.FieldName = Nothing
        Me.TxtBMCDiesel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBMCDiesel.Location = New System.Drawing.Point(243, 50)
        Me.TxtBMCDiesel.Name = "TxtBMCDiesel"
        Me.TxtBMCDiesel.Size = New System.Drawing.Size(77, 18)
        Me.TxtBMCDiesel.TabIndex = 165
        Me.TxtBMCDiesel.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'TxtBMCProrataamt
        '
        Me.TxtBMCProrataamt.AutoSize = False
        Me.TxtBMCProrataamt.BorderVisible = True
        Me.TxtBMCProrataamt.FieldName = Nothing
        Me.TxtBMCProrataamt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBMCProrataamt.Location = New System.Drawing.Point(149, 50)
        Me.TxtBMCProrataamt.Name = "TxtBMCProrataamt"
        Me.TxtBMCProrataamt.Size = New System.Drawing.Size(77, 18)
        Me.TxtBMCProrataamt.TabIndex = 164
        Me.TxtBMCProrataamt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel27
        '
        Me.MyLabel27.FieldName = Nothing
        Me.MyLabel27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel27.Location = New System.Drawing.Point(35, 273)
        Me.MyLabel27.Name = "MyLabel27"
        Me.MyLabel27.Size = New System.Drawing.Size(99, 16)
        Me.MyLabel27.TabIndex = 163
        Me.MyLabel27.Text = "GROSS AMOUNT"
        '
        'MyLabel26
        '
        Me.MyLabel26.FieldName = Nothing
        Me.MyLabel26.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel26.Location = New System.Drawing.Point(35, 251)
        Me.MyLabel26.Name = "MyLabel26"
        Me.MyLabel26.Size = New System.Drawing.Size(120, 16)
        Me.MyLabel26.TabIndex = 162
        Me.MyLabel26.Text = "FAT SNF SHORTAGE"
        '
        'MyLabel25
        '
        Me.MyLabel25.FieldName = Nothing
        Me.MyLabel25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel25.Location = New System.Drawing.Point(35, 229)
        Me.MyLabel25.Name = "MyLabel25"
        Me.MyLabel25.Size = New System.Drawing.Size(95, 16)
        Me.MyLabel25.TabIndex = 161
        Me.MyLabel25.Text = "TOTAL AMOUNT"
        '
        'MyLabel24
        '
        Me.MyLabel24.FieldName = Nothing
        Me.MyLabel24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel24.Location = New System.Drawing.Point(35, 205)
        Me.MyLabel24.Name = "MyLabel24"
        Me.MyLabel24.Size = New System.Drawing.Size(77, 16)
        Me.MyLabel24.TabIndex = 160
        Me.MyLabel24.Text = "ICE CHARGE"
        '
        'MyLabel23
        '
        Me.MyLabel23.FieldName = Nothing
        Me.MyLabel23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel23.Location = New System.Drawing.Point(35, 183)
        Me.MyLabel23.Name = "MyLabel23"
        Me.MyLabel23.Size = New System.Drawing.Size(60, 16)
        Me.MyLabel23.TabIndex = 159
        Me.MyLabel23.Text = "TOLL TAX"
        '
        'MyLabel22
        '
        Me.MyLabel22.FieldName = Nothing
        Me.MyLabel22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel22.Location = New System.Drawing.Point(35, 150)
        Me.MyLabel22.Name = "MyLabel22"
        Me.MyLabel22.Size = New System.Drawing.Size(56, 16)
        Me.MyLabel22.TabIndex = 158
        Me.MyLabel22.Text = "TOTAL=>"
        Me.MyLabel22.Visible = False
        '
        'MyLabel21
        '
        Me.MyLabel21.FieldName = Nothing
        Me.MyLabel21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel21.Location = New System.Drawing.Point(35, 126)
        Me.MyLabel21.Name = "MyLabel21"
        Me.MyLabel21.Size = New System.Drawing.Size(36, 16)
        Me.MyLabel21.TabIndex = 157
        Me.MyLabel21.Text = "NMG:"
        Me.MyLabel21.Visible = False
        '
        'MyLabel20
        '
        Me.MyLabel20.FieldName = Nothing
        Me.MyLabel20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel20.Location = New System.Drawing.Point(34, 102)
        Me.MyLabel20.Name = "MyLabel20"
        Me.MyLabel20.Size = New System.Drawing.Size(35, 16)
        Me.MyLabel20.TabIndex = 156
        Me.MyLabel20.Text = "MCC:"
        Me.MyLabel20.Visible = False
        '
        'MyLabel19
        '
        Me.MyLabel19.FieldName = Nothing
        Me.MyLabel19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel19.Location = New System.Drawing.Point(34, 80)
        Me.MyLabel19.Name = "MyLabel19"
        Me.MyLabel19.Size = New System.Drawing.Size(36, 16)
        Me.MyLabel19.TabIndex = 156
        Me.MyLabel19.Text = "RMG:"
        Me.MyLabel19.Visible = False
        '
        'MyLabel18
        '
        Me.MyLabel18.FieldName = Nothing
        Me.MyLabel18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel18.Location = New System.Drawing.Point(35, 52)
        Me.MyLabel18.Name = "MyLabel18"
        Me.MyLabel18.Size = New System.Drawing.Size(34, 16)
        Me.MyLabel18.TabIndex = 155
        Me.MyLabel18.Text = "BMC:"
        '
        'MyLabel17
        '
        Me.MyLabel17.FieldName = Nothing
        Me.MyLabel17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel17.Location = New System.Drawing.Point(341, 20)
        Me.MyLabel17.Name = "MyLabel17"
        Me.MyLabel17.Size = New System.Drawing.Size(43, 16)
        Me.MyLabel17.TabIndex = 154
        Me.MyLabel17.Text = "TOTAL"
        '
        'MyLabel16
        '
        Me.MyLabel16.FieldName = Nothing
        Me.MyLabel16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel16.Location = New System.Drawing.Point(246, 20)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(70, 16)
        Me.MyLabel16.TabIndex = 153
        Me.MyLabel16.Text = "DIESEL(RD)"
        '
        'MyLabel15
        '
        Me.MyLabel15.FieldName = Nothing
        Me.MyLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel15.Location = New System.Drawing.Point(149, 20)
        Me.MyLabel15.Name = "MyLabel15"
        Me.MyLabel15.Size = New System.Drawing.Size(88, 16)
        Me.MyLabel15.TabIndex = 152
        Me.MyLabel15.Text = "PRORATA AMT"
        '
        'MyLabel14
        '
        Me.MyLabel14.FieldName = Nothing
        Me.MyLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel14.Location = New System.Drawing.Point(31, 20)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(112, 16)
        Me.MyLabel14.TabIndex = 151
        Me.MyLabel14.Text = "CATEGORY HEADS"
        '
        'lblInvoiceDiscAmt
        '
        Me.lblInvoiceDiscAmt.AutoSize = False
        Me.lblInvoiceDiscAmt.BorderVisible = True
        Me.lblInvoiceDiscAmt.FieldName = Nothing
        Me.lblInvoiceDiscAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInvoiceDiscAmt.Location = New System.Drawing.Point(628, 155)
        Me.lblInvoiceDiscAmt.Name = "lblInvoiceDiscAmt"
        Me.lblInvoiceDiscAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblInvoiceDiscAmt.TabIndex = 7
        Me.lblInvoiceDiscAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.lblInvoiceDiscAmt.Visible = False
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(483, 155)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(138, 16)
        Me.MyLabel6.TabIndex = 150
        Me.MyLabel6.Text = "- Invoice Discount Amount"
        Me.MyLabel6.Visible = False
        '
        'RadLabel32
        '
        Me.RadLabel32.FieldName = Nothing
        Me.RadLabel32.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel32.Location = New System.Drawing.Point(485, 268)
        Me.RadLabel32.Name = "RadLabel32"
        Me.RadLabel32.Size = New System.Drawing.Size(140, 16)
        Me.RadLabel32.TabIndex = 135
        Me.RadLabel32.Text = "+ Total Additional Charges"
        Me.RadLabel32.Visible = False
        '
        'lblAddCharges1
        '
        Me.lblAddCharges1.AutoSize = False
        Me.lblAddCharges1.BorderVisible = True
        Me.lblAddCharges1.FieldName = Nothing
        Me.lblAddCharges1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddCharges1.Location = New System.Drawing.Point(628, 268)
        Me.lblAddCharges1.Name = "lblAddCharges1"
        Me.lblAddCharges1.Size = New System.Drawing.Size(110, 18)
        Me.lblAddCharges1.TabIndex = 11
        Me.lblAddCharges1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.lblAddCharges1.Visible = False
        '
        'RadLabel9
        '
        Me.RadLabel9.FieldName = Nothing
        Me.RadLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel9.Location = New System.Drawing.Point(501, 211)
        Me.RadLabel9.Name = "RadLabel9"
        Me.RadLabel9.Size = New System.Drawing.Size(120, 16)
        Me.RadLabel9.TabIndex = 120
        Me.RadLabel9.Text = "Amount After Discount"
        Me.RadLabel9.Visible = False
        '
        'RadLabel27
        '
        Me.RadLabel27.FieldName = Nothing
        Me.RadLabel27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel27.Location = New System.Drawing.Point(522, 295)
        Me.RadLabel27.Name = "RadLabel27"
        Me.RadLabel27.Size = New System.Drawing.Size(100, 16)
        Me.RadLabel27.TabIndex = 123
        Me.RadLabel27.Text = "Document Amount"
        Me.RadLabel27.Visible = False
        '
        'lblTotRAmt
        '
        Me.lblTotRAmt.AutoSize = False
        Me.lblTotRAmt.BorderVisible = True
        Me.lblTotRAmt.FieldName = Nothing
        Me.lblTotRAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotRAmt.Location = New System.Drawing.Point(628, 293)
        Me.lblTotRAmt.Name = "lblTotRAmt"
        Me.lblTotRAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblTotRAmt.TabIndex = 12
        Me.lblTotRAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTotRAmt.Visible = False
        '
        'RadLabel25
        '
        Me.RadLabel25.FieldName = Nothing
        Me.RadLabel25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel25.Location = New System.Drawing.Point(545, 241)
        Me.RadLabel25.Name = "RadLabel25"
        Me.RadLabel25.Size = New System.Drawing.Size(77, 16)
        Me.RadLabel25.TabIndex = 122
        Me.RadLabel25.Text = "+ Tax Amount"
        Me.RadLabel25.Visible = False
        '
        'lblTaxAmt
        '
        Me.lblTaxAmt.AutoSize = False
        Me.lblTaxAmt.BorderVisible = True
        Me.lblTaxAmt.FieldName = Nothing
        Me.lblTaxAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTaxAmt.Location = New System.Drawing.Point(628, 241)
        Me.lblTaxAmt.Name = "lblTaxAmt"
        Me.lblTaxAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblTaxAmt.TabIndex = 10
        Me.lblTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTaxAmt.Visible = False
        '
        'lblAmtAfterDiscount
        '
        Me.lblAmtAfterDiscount.AutoSize = False
        Me.lblAmtAfterDiscount.BorderVisible = True
        Me.lblAmtAfterDiscount.FieldName = Nothing
        Me.lblAmtAfterDiscount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmtAfterDiscount.Location = New System.Drawing.Point(628, 211)
        Me.lblAmtAfterDiscount.Name = "lblAmtAfterDiscount"
        Me.lblAmtAfterDiscount.Size = New System.Drawing.Size(110, 18)
        Me.lblAmtAfterDiscount.TabIndex = 9
        Me.lblAmtAfterDiscount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.lblAmtAfterDiscount.Visible = False
        '
        'lblDiscountAmt
        '
        Me.lblDiscountAmt.AutoSize = False
        Me.lblDiscountAmt.BorderVisible = True
        Me.lblDiscountAmt.FieldName = Nothing
        Me.lblDiscountAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDiscountAmt.Location = New System.Drawing.Point(628, 181)
        Me.lblDiscountAmt.Name = "lblDiscountAmt"
        Me.lblDiscountAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblDiscountAmt.TabIndex = 8
        Me.lblDiscountAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.lblDiscountAmt.Visible = False
        '
        'lblAmtWithDiscount
        '
        Me.lblAmtWithDiscount.AutoSize = False
        Me.lblAmtWithDiscount.BorderVisible = True
        Me.lblAmtWithDiscount.FieldName = Nothing
        Me.lblAmtWithDiscount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmtWithDiscount.Location = New System.Drawing.Point(640, 94)
        Me.lblAmtWithDiscount.Name = "lblAmtWithDiscount"
        Me.lblAmtWithDiscount.Size = New System.Drawing.Size(110, 18)
        Me.lblAmtWithDiscount.TabIndex = 1
        Me.lblAmtWithDiscount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.lblAmtWithDiscount.Visible = False
        '
        'RadLabel22
        '
        Me.RadLabel22.FieldName = Nothing
        Me.RadLabel22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel22.Location = New System.Drawing.Point(521, 181)
        Me.RadLabel22.Name = "RadLabel22"
        Me.RadLabel22.Size = New System.Drawing.Size(99, 16)
        Me.RadLabel22.TabIndex = 121
        Me.RadLabel22.Text = "- Discount Amount"
        Me.RadLabel22.Visible = False
        '
        'RadLabel19
        '
        Me.RadLabel19.FieldName = Nothing
        Me.RadLabel19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel19.Location = New System.Drawing.Point(436, 94)
        Me.RadLabel19.Name = "RadLabel19"
        Me.RadLabel19.Size = New System.Drawing.Size(186, 16)
        Me.RadLabel19.TabIndex = 119
        Me.RadLabel19.Text = "Document Amount without Discount"
        Me.RadLabel19.Visible = False
        '
        'btnReverse
        '
        Me.btnReverse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReverse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverse.Location = New System.Drawing.Point(315, 23)
        Me.btnReverse.Name = "btnReverse"
        Me.btnReverse.Size = New System.Drawing.Size(177, 22)
        Me.btnReverse.TabIndex = 161
        Me.btnReverse.Text = "Revese and Unpost"
        Me.btnReverse.Visible = False
        '
        'btnLayout
        '
        Me.btnLayout.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiImport, Me.rmiExport})
        Me.btnLayout.Location = New System.Drawing.Point(294, 23)
        Me.btnLayout.Name = "btnLayout"
        Me.btnLayout.Size = New System.Drawing.Size(110, 22)
        Me.btnLayout.TabIndex = 160
        Me.btnLayout.Text = "Layout"
        '
        'rmiImport
        '
        Me.rmiImport.Name = "rmiImport"
        Me.rmiImport.Text = "Save Layout"
        Me.rmiImport.UseCompatibleTextRendering = False
        '
        'rmiExport
        '
        Me.rmiExport.Name = "rmiExport"
        Me.rmiExport.Text = "Delete Layout"
        Me.rmiExport.UseCompatibleTextRendering = False
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(75, 6)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(60, 22)
        Me.btnDelete.TabIndex = 22
        Me.btnDelete.Text = "Delete"
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(990, 6)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(44, 22)
        Me.btnCancel.TabIndex = 21
        Me.btnCancel.Text = "Close"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(143, 6)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(60, 22)
        Me.btnPost.TabIndex = 3
        Me.btnPost.Text = "Post"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(12, 6)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(57, 22)
        Me.btnSave.TabIndex = 2
        Me.btnSave.Text = "Save"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(208, 22)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(78, 22)
        Me.btnPrint.TabIndex = 162
        Me.btnPrint.Text = "Print"
        Me.btnPrint.Visible = False
        '
        'BMC_Transporter_Bill
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "BMC_Transporter_Bill"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "BMC_Transporter_Bill"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.txtFatShortage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFatShortage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtSnfShortage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtTankerprorata, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtIceCharge, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtKMRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtBarelCap, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtTollTax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtFatRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtDieselMinus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtSnfRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDieselplus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtRateprorata, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtTDR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkDCS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTransporter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTransporter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTankerDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAbandonmentNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTankerNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage4.ResumeLayout(False)
        Me.RadPageViewPage4.PerformLayout()
        CType(Me.MyLabel47, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel46, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel45, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel44, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel43, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel42, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel41, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel40, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtTotalIceCharge, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtTotalAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtTotalFatSnfShortage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtGrossAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtTotalTollTax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel34, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel33, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel32, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel31, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtBMCTotal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtBMCDiesel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtBMCProrataamt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel27, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel26, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblInvoiceDiscAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel32, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAddCharges1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel27, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotRAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTaxAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAmtAfterDiscount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDiscountAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAmtWithDiscount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnLayout, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents chkDCS As RadCheckBox
    Friend WithEvents lblTankerDesc As common.Controls.MyLabel
    Friend WithEvents lblAbandonmentNo As common.Controls.MyLabel
    Friend WithEvents lblTankerNo As common.Controls.MyLabel
    Friend WithEvents RadGroupBox2 As RadGroupBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents txtTankerNo As common.UserControls.txtFinder
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents btnAddNew As RadButton
    Friend WithEvents RadPageViewPage4 As RadPageViewPage
    Friend WithEvents lblInvoiceDiscAmt As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents RadLabel32 As common.Controls.MyLabel
    Friend WithEvents lblAddCharges1 As common.Controls.MyLabel
    Friend WithEvents RadLabel9 As common.Controls.MyLabel
    Friend WithEvents RadLabel27 As common.Controls.MyLabel
    Friend WithEvents lblTotRAmt As common.Controls.MyLabel
    Friend WithEvents RadLabel25 As common.Controls.MyLabel
    Friend WithEvents lblTaxAmt As common.Controls.MyLabel
    Friend WithEvents lblAmtAfterDiscount As common.Controls.MyLabel
    Friend WithEvents lblDiscountAmt As common.Controls.MyLabel
    Friend WithEvents lblAmtWithDiscount As common.Controls.MyLabel
    Friend WithEvents RadLabel22 As common.Controls.MyLabel
    Friend WithEvents RadLabel19 As common.Controls.MyLabel
    Friend WithEvents txtTransporter As common.Controls.MyTextBox
    Friend WithEvents lblTransporter As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents lblFatShortage As common.Controls.MyLabel
    Friend WithEvents TxtKMRate As common.Controls.MyTextBox
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents TxtTollTax As common.Controls.MyTextBox
    Friend WithEvents TxtDieselMinus As common.Controls.MyTextBox
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtDieselplus As common.Controls.MyTextBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents TxtSnfRate As common.Controls.MyTextBox
    Friend WithEvents TxtFatRate As common.Controls.MyTextBox
    Friend WithEvents TxtSnfShortage As common.Controls.MyTextBox
    Friend WithEvents txtFatShortage As common.Controls.MyTextBox
    Friend WithEvents TxtTDR As common.Controls.MyTextBox
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents TxtRateprorata As common.Controls.MyTextBox
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents TxtTankerprorata As common.Controls.MyTextBox
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents TxtBarelCap As common.Controls.MyTextBox
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents TxtIceCharge As common.Controls.MyTextBox
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents MyLabel17 As common.Controls.MyLabel
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
    Friend WithEvents MyLabel21 As common.Controls.MyLabel
    Friend WithEvents MyLabel20 As common.Controls.MyLabel
    Friend WithEvents MyLabel19 As common.Controls.MyLabel
    Friend WithEvents MyLabel18 As common.Controls.MyLabel
    Friend WithEvents MyLabel23 As common.Controls.MyLabel
    Friend WithEvents MyLabel22 As common.Controls.MyLabel
    Friend WithEvents MyLabel24 As common.Controls.MyLabel
    Friend WithEvents MyLabel25 As common.Controls.MyLabel
    Friend WithEvents MyLabel26 As common.Controls.MyLabel
    Friend WithEvents MyLabel27 As common.Controls.MyLabel
    Friend WithEvents MyLabel47 As common.Controls.MyLabel
    Friend WithEvents MyLabel46 As common.Controls.MyLabel
    Friend WithEvents MyLabel45 As common.Controls.MyLabel
    Friend WithEvents MyLabel44 As common.Controls.MyLabel
    Friend WithEvents MyLabel43 As common.Controls.MyLabel
    Friend WithEvents MyLabel42 As common.Controls.MyLabel
    Friend WithEvents MyLabel41 As common.Controls.MyLabel
    Friend WithEvents MyLabel40 As common.Controls.MyLabel
    Friend WithEvents TxtTotalIceCharge As common.Controls.MyLabel
    Friend WithEvents TxtTotalAmount As common.Controls.MyLabel
    Friend WithEvents TxtTotalFatSnfShortage As common.Controls.MyLabel
    Friend WithEvents TxtGrossAmount As common.Controls.MyLabel
    Friend WithEvents TxtTotalTollTax As common.Controls.MyLabel
    Friend WithEvents MyLabel34 As common.Controls.MyLabel
    Friend WithEvents MyLabel33 As common.Controls.MyLabel
    Friend WithEvents MyLabel32 As common.Controls.MyLabel
    Friend WithEvents MyLabel31 As common.Controls.MyLabel
    Friend WithEvents TxtBMCTotal As common.Controls.MyLabel
    Friend WithEvents TxtBMCDiesel As common.Controls.MyLabel
    Friend WithEvents TxtBMCProrataamt As common.Controls.MyLabel
    Friend WithEvents RadGroupBox1 As RadGroupBox
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents btnPost As RadButton
    Friend WithEvents btnSave As RadButton
    Friend WithEvents btnCancel As RadButton
    Friend WithEvents btnGo As RadButton
    Friend WithEvents btnDelete As RadButton
    Friend WithEvents btnLayout As RadSplitButton
    Friend WithEvents rmiImport As RadMenuItem
    Friend WithEvents rmiExport As RadMenuItem
    Friend WithEvents RadGroupBox3 As RadGroupBox
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents btnReverse As RadButton
    Friend WithEvents btnPrint As RadButton
End Class
