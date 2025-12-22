<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSalesOrderDispatch
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
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer5 = New System.Windows.Forms.SplitContainer()
        Me.lblInvnoForReplacement = New common.Controls.MyLabel()
        Me.txtInvoice_for_replacement = New common.UserControls.txtFinder()
        Me.lblorderNo = New common.Controls.MyLabel()
        Me.lblorderdesc = New common.Controls.MyLabel()
        Me.txtRemark = New common.Controls.MyTextBox()
        Me.lblRemark = New common.Controls.MyLabel()
        Me.lblTransporterName = New common.Controls.MyTextBox()
        Me.MyLabel31 = New common.Controls.MyLabel()
        Me.txtTransporterCode = New common.UserControls.txtFinder()
        Me.lblVehicleNo = New common.Controls.MyTextBox()
        Me.txtVehicleCode = New common.UserControls.txtFinder()
        Me.lblVehicleCode = New common.Controls.MyLabel()
        Me.txtInvoiceno = New common.Controls.MyLabel()
        Me.lblInvoiceNo = New common.Controls.MyLabel()
        Me.chkReplacement = New System.Windows.Forms.CheckBox()
        Me.gbQtySummary = New System.Windows.Forms.GroupBox()
        Me.txtBalQty = New common.Controls.MyLabel()
        Me.txtOrderQty = New common.Controls.MyLabel()
        Me.lblbalQty = New common.Controls.MyLabel()
        Me.lblOrderQty = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.txtCustomerCode = New common.UserControls.txtFinder()
        Me.lblCustomer = New common.Controls.MyLabel()
        Me.lblCustomerName = New common.Controls.MyLabel()
        Me.lblDocCode = New common.Controls.MyLabel()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.txtDocCode = New common.UserControls.txtNavigator()
        Me.lblSubLocationDesc = New common.Controls.MyLabel()
        Me.lblSubLocaiton = New common.Controls.MyLabel()
        Me.txtOrderNo = New common.UserControls.txtFinder()
        Me.txtSubLocation = New common.UserControls.txtFinder()
        Me.lblDate = New common.Controls.MyLabel()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.lblLocationDesc = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtFinder()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.lblShiftType = New Telerik.WinControls.UI.RadLabel()
        Me.lblAbandonmentNo = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbtnTaxCalManual = New common.Controls.MyRadioButton()
        Me.rbtnTaxCalAutomatic = New common.Controls.MyRadioButton()
        Me.txtTaxGroup = New common.UserControls.txtFinder()
        Me.RadLabel11 = New common.Controls.MyLabel()
        Me.lblTaxGrpName = New common.Controls.MyLabel()
        Me.gv2 = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage5 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.btnReverseUnpost = New Telerik.WinControls.UI.RadButton()
        Me.btnCancel = New Telerik.WinControls.UI.RadButton()
        Me.lblTaxAmt = New common.Controls.MyLabel()
        Me.txtTaxAmt = New common.Controls.MyLabel()
        Me.lblDocAmtWithoutTax = New common.Controls.MyLabel()
        Me.txtDocAmtWithoutTax = New common.Controls.MyLabel()
        Me.lblDocamt = New common.Controls.MyLabel()
        Me.txtDocAmt = New common.Controls.MyLabel()
        Me.SplitContainer4 = New System.Windows.Forms.SplitContainer()
        Me.btnHistory = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnprinte_wayBill = New Telerik.WinControls.UI.RadButton()
        Me.btnShowInventory = New Telerik.WinControls.UI.RadButton()
        Me.btnInvoiceJE = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.chkewaybill = New System.Windows.Forms.CheckBox()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.SplitContainer5.Panel1.SuspendLayout()
        Me.SplitContainer5.Panel2.SuspendLayout()
        Me.SplitContainer5.SuspendLayout()
        CType(Me.lblInvnoForReplacement, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblorderNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblorderdesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemark, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRemark, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTransporterName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel31, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVehicleNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVehicleCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtInvoiceno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblInvoiceNo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbQtySummary.SuspendLayout()
        CType(Me.txtBalQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOrderQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblbalQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblOrderQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomerName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSubLocationDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSubLocaiton, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblShiftType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAbandonmentNo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.rbtnTaxCalManual, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnTaxCalAutomatic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTaxGrpName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage5.SuspendLayout()
        Me.RadPageViewPage3.SuspendLayout()
        CType(Me.btnReverseUnpost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTaxAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTaxAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocAmtWithoutTax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDocAmtWithoutTax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocamt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDocAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer4.Panel1.SuspendLayout()
        Me.SplitContainer4.Panel2.SuspendLayout()
        Me.SplitContainer4.SuspendLayout()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnprinte_wayBill, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnShowInventory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnInvoiceJE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage5)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1000, 413)
        Me.RadPageView1.TabIndex = 2
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer5)
        Me.RadPageViewPage1.Controls.Add(Me.lblShiftType)
        Me.RadPageViewPage1.Controls.Add(Me.lblAbandonmentNo)
        Me.RadPageViewPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(92.0!, 26.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(979, 367)
        Me.RadPageViewPage1.Text = "Dispatch Order"
        '
        'SplitContainer5
        '
        Me.SplitContainer5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer5.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer5.IsSplitterFixed = True
        Me.SplitContainer5.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer5.Name = "SplitContainer5"
        Me.SplitContainer5.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer5.Panel1
        '
        Me.SplitContainer5.Panel1.Controls.Add(Me.chkewaybill)
        Me.SplitContainer5.Panel1.Controls.Add(Me.lblInvnoForReplacement)
        Me.SplitContainer5.Panel1.Controls.Add(Me.txtInvoice_for_replacement)
        Me.SplitContainer5.Panel1.Controls.Add(Me.txtRemark)
        Me.SplitContainer5.Panel1.Controls.Add(Me.lblRemark)
        Me.SplitContainer5.Panel1.Controls.Add(Me.lblTransporterName)
        Me.SplitContainer5.Panel1.Controls.Add(Me.MyLabel31)
        Me.SplitContainer5.Panel1.Controls.Add(Me.txtTransporterCode)
        Me.SplitContainer5.Panel1.Controls.Add(Me.lblVehicleNo)
        Me.SplitContainer5.Panel1.Controls.Add(Me.txtVehicleCode)
        Me.SplitContainer5.Panel1.Controls.Add(Me.lblVehicleCode)
        Me.SplitContainer5.Panel1.Controls.Add(Me.txtInvoiceno)
        Me.SplitContainer5.Panel1.Controls.Add(Me.lblInvoiceNo)
        Me.SplitContainer5.Panel1.Controls.Add(Me.chkReplacement)
        Me.SplitContainer5.Panel1.Controls.Add(Me.gbQtySummary)
        Me.SplitContainer5.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer5.Panel1.Controls.Add(Me.txtCustomerCode)
        Me.SplitContainer5.Panel1.Controls.Add(Me.lblDocCode)
        Me.SplitContainer5.Panel1.Controls.Add(Me.btnAddNew)
        Me.SplitContainer5.Panel1.Controls.Add(Me.txtDocCode)
        Me.SplitContainer5.Panel1.Controls.Add(Me.lblSubLocationDesc)
        Me.SplitContainer5.Panel1.Controls.Add(Me.lblCustomer)
        Me.SplitContainer5.Panel1.Controls.Add(Me.lblSubLocaiton)
        Me.SplitContainer5.Panel1.Controls.Add(Me.txtOrderNo)
        Me.SplitContainer5.Panel1.Controls.Add(Me.txtSubLocation)
        Me.SplitContainer5.Panel1.Controls.Add(Me.lblCustomerName)
        Me.SplitContainer5.Panel1.Controls.Add(Me.lblDate)
        Me.SplitContainer5.Panel1.Controls.Add(Me.txtDate)
        Me.SplitContainer5.Panel1.Controls.Add(Me.lblorderNo)
        Me.SplitContainer5.Panel1.Controls.Add(Me.lblLocationDesc)
        Me.SplitContainer5.Panel1.Controls.Add(Me.lblorderdesc)
        Me.SplitContainer5.Panel1.Controls.Add(Me.txtLocation)
        Me.SplitContainer5.Panel1.Controls.Add(Me.lblLocation)
        '
        'SplitContainer5.Panel2
        '
        Me.SplitContainer5.Panel2.Controls.Add(Me.gv1)
        Me.SplitContainer5.Size = New System.Drawing.Size(979, 367)
        Me.SplitContainer5.SplitterDistance = 177
        Me.SplitContainer5.TabIndex = 1578
        '
        'lblInvnoForReplacement
        '
        Me.lblInvnoForReplacement.FieldName = Nothing
        Me.lblInvnoForReplacement.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInvnoForReplacement.Location = New System.Drawing.Point(638, 50)
        Me.lblInvnoForReplacement.Name = "lblInvnoForReplacement"
        Me.lblInvnoForReplacement.Size = New System.Drawing.Size(150, 16)
        Me.lblInvnoForReplacement.TabIndex = 1629
        Me.lblInvnoForReplacement.Text = "Invoice No For Replacement"
        '
        'txtInvoice_for_replacement
        '
        Me.txtInvoice_for_replacement.CalculationExpression = Nothing
        Me.txtInvoice_for_replacement.FieldCode = Nothing
        Me.txtInvoice_for_replacement.FieldDesc = Nothing
        Me.txtInvoice_for_replacement.FieldMaxLength = 0
        Me.txtInvoice_for_replacement.FieldName = Nothing
        Me.txtInvoice_for_replacement.isCalculatedField = False
        Me.txtInvoice_for_replacement.IsSourceFromTable = False
        Me.txtInvoice_for_replacement.IsSourceFromValueList = False
        Me.txtInvoice_for_replacement.IsUnique = False
        Me.txtInvoice_for_replacement.Location = New System.Drawing.Point(794, 48)
        Me.txtInvoice_for_replacement.MendatroryField = False
        Me.txtInvoice_for_replacement.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvoice_for_replacement.MyLinkLable1 = Me.lblorderNo
        Me.txtInvoice_for_replacement.MyLinkLable2 = Me.lblorderdesc
        Me.txtInvoice_for_replacement.MyReadOnly = False
        Me.txtInvoice_for_replacement.MyShowMasterFormButton = False
        Me.txtInvoice_for_replacement.Name = "txtInvoice_for_replacement"
        Me.txtInvoice_for_replacement.ReferenceFieldDesc = Nothing
        Me.txtInvoice_for_replacement.ReferenceFieldName = Nothing
        Me.txtInvoice_for_replacement.ReferenceTableName = Nothing
        Me.txtInvoice_for_replacement.Size = New System.Drawing.Size(140, 20)
        Me.txtInvoice_for_replacement.TabIndex = 1628
        Me.txtInvoice_for_replacement.Value = ""
        '
        'lblorderNo
        '
        Me.lblorderNo.FieldName = Nothing
        Me.lblorderNo.Location = New System.Drawing.Point(12, 27)
        Me.lblorderNo.Name = "lblorderNo"
        Me.lblorderNo.Size = New System.Drawing.Size(77, 18)
        Me.lblorderNo.TabIndex = 1605
        Me.lblorderNo.Text = "Sale Order No"
        '
        'lblorderdesc
        '
        Me.lblorderdesc.AutoSize = False
        Me.lblorderdesc.BorderVisible = True
        Me.lblorderdesc.FieldName = Nothing
        Me.lblorderdesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblorderdesc.Location = New System.Drawing.Point(279, 26)
        Me.lblorderdesc.Name = "lblorderdesc"
        Me.lblorderdesc.Size = New System.Drawing.Size(162, 20)
        Me.lblorderdesc.TabIndex = 1606
        Me.lblorderdesc.TextWrap = False
        '
        'txtRemark
        '
        Me.txtRemark.CalculationExpression = Nothing
        Me.txtRemark.FieldCode = Nothing
        Me.txtRemark.FieldDesc = Nothing
        Me.txtRemark.FieldMaxLength = 0
        Me.txtRemark.FieldName = Nothing
        Me.txtRemark.isCalculatedField = False
        Me.txtRemark.IsSourceFromTable = False
        Me.txtRemark.IsSourceFromValueList = False
        Me.txtRemark.IsUnique = False
        Me.txtRemark.Location = New System.Drawing.Point(501, 151)
        Me.txtRemark.MendatroryField = False
        Me.txtRemark.MyLinkLable1 = Me.lblRemark
        Me.txtRemark.MyLinkLable2 = Nothing
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.ReferenceFieldDesc = Nothing
        Me.txtRemark.ReferenceFieldName = Nothing
        Me.txtRemark.ReferenceTableName = Nothing
        Me.txtRemark.Size = New System.Drawing.Size(281, 20)
        Me.txtRemark.TabIndex = 1627
        '
        'lblRemark
        '
        Me.lblRemark.FieldName = Nothing
        Me.lblRemark.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRemark.Location = New System.Drawing.Point(449, 153)
        Me.lblRemark.Name = "lblRemark"
        Me.lblRemark.Size = New System.Drawing.Size(46, 16)
        Me.lblRemark.TabIndex = 1626
        Me.lblRemark.Text = "Remark"
        '
        'lblTransporterName
        '
        Me.lblTransporterName.CalculationExpression = Nothing
        Me.lblTransporterName.Enabled = False
        Me.lblTransporterName.FieldCode = Nothing
        Me.lblTransporterName.FieldDesc = Nothing
        Me.lblTransporterName.FieldMaxLength = 0
        Me.lblTransporterName.FieldName = Nothing
        Me.lblTransporterName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTransporterName.isCalculatedField = False
        Me.lblTransporterName.IsSourceFromTable = False
        Me.lblTransporterName.IsSourceFromValueList = False
        Me.lblTransporterName.IsUnique = False
        Me.lblTransporterName.Location = New System.Drawing.Point(279, 128)
        Me.lblTransporterName.MaxLength = 200
        Me.lblTransporterName.MendatroryField = False
        Me.lblTransporterName.MyLinkLable1 = Nothing
        Me.lblTransporterName.MyLinkLable2 = Nothing
        Me.lblTransporterName.Name = "lblTransporterName"
        Me.lblTransporterName.ReferenceFieldDesc = Nothing
        Me.lblTransporterName.ReferenceFieldName = Nothing
        Me.lblTransporterName.ReferenceTableName = Nothing
        Me.lblTransporterName.Size = New System.Drawing.Size(162, 18)
        Me.lblTransporterName.TabIndex = 1625
        '
        'MyLabel31
        '
        Me.MyLabel31.FieldName = Nothing
        Me.MyLabel31.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel31.Location = New System.Drawing.Point(12, 129)
        Me.MyLabel31.Name = "MyLabel31"
        Me.MyLabel31.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel31.TabIndex = 1624
        Me.MyLabel31.Text = "Transporter"
        '
        'txtTransporterCode
        '
        Me.txtTransporterCode.CalculationExpression = Nothing
        Me.txtTransporterCode.FieldCode = Nothing
        Me.txtTransporterCode.FieldDesc = Nothing
        Me.txtTransporterCode.FieldMaxLength = 0
        Me.txtTransporterCode.FieldName = Nothing
        Me.txtTransporterCode.isCalculatedField = False
        Me.txtTransporterCode.IsSourceFromTable = False
        Me.txtTransporterCode.IsSourceFromValueList = False
        Me.txtTransporterCode.IsUnique = False
        Me.txtTransporterCode.Location = New System.Drawing.Point(93, 125)
        Me.txtTransporterCode.MendatroryField = False
        Me.txtTransporterCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransporterCode.MyLinkLable1 = Me.MyLabel31
        Me.txtTransporterCode.MyLinkLable2 = Nothing
        Me.txtTransporterCode.MyReadOnly = False
        Me.txtTransporterCode.MyShowMasterFormButton = False
        Me.txtTransporterCode.Name = "txtTransporterCode"
        Me.txtTransporterCode.ReferenceFieldDesc = Nothing
        Me.txtTransporterCode.ReferenceFieldName = Nothing
        Me.txtTransporterCode.ReferenceTableName = Nothing
        Me.txtTransporterCode.Size = New System.Drawing.Size(180, 24)
        Me.txtTransporterCode.TabIndex = 1623
        Me.txtTransporterCode.Value = ""
        '
        'lblVehicleNo
        '
        Me.lblVehicleNo.CalculationExpression = Nothing
        Me.lblVehicleNo.FieldCode = Nothing
        Me.lblVehicleNo.FieldDesc = Nothing
        Me.lblVehicleNo.FieldMaxLength = 0
        Me.lblVehicleNo.FieldName = Nothing
        Me.lblVehicleNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVehicleNo.isCalculatedField = False
        Me.lblVehicleNo.IsSourceFromTable = False
        Me.lblVehicleNo.IsSourceFromValueList = False
        Me.lblVehicleNo.IsUnique = False
        Me.lblVehicleNo.Location = New System.Drawing.Point(279, 152)
        Me.lblVehicleNo.MaxLength = 200
        Me.lblVehicleNo.MendatroryField = False
        Me.lblVehicleNo.MyLinkLable1 = Nothing
        Me.lblVehicleNo.MyLinkLable2 = Nothing
        Me.lblVehicleNo.Name = "lblVehicleNo"
        Me.lblVehicleNo.ReferenceFieldDesc = Nothing
        Me.lblVehicleNo.ReferenceFieldName = Nothing
        Me.lblVehicleNo.ReferenceTableName = Nothing
        Me.lblVehicleNo.Size = New System.Drawing.Size(162, 18)
        Me.lblVehicleNo.TabIndex = 1622
        '
        'txtVehicleCode
        '
        Me.txtVehicleCode.CalculationExpression = Nothing
        Me.txtVehicleCode.FieldCode = Nothing
        Me.txtVehicleCode.FieldDesc = Nothing
        Me.txtVehicleCode.FieldMaxLength = 0
        Me.txtVehicleCode.FieldName = Nothing
        Me.txtVehicleCode.isCalculatedField = False
        Me.txtVehicleCode.IsSourceFromTable = False
        Me.txtVehicleCode.IsSourceFromValueList = False
        Me.txtVehicleCode.IsUnique = False
        Me.txtVehicleCode.Location = New System.Drawing.Point(93, 150)
        Me.txtVehicleCode.MendatroryField = False
        Me.txtVehicleCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVehicleCode.MyLinkLable1 = Me.lblVehicleCode
        Me.txtVehicleCode.MyLinkLable2 = Nothing
        Me.txtVehicleCode.MyReadOnly = False
        Me.txtVehicleCode.MyShowMasterFormButton = False
        Me.txtVehicleCode.Name = "txtVehicleCode"
        Me.txtVehicleCode.ReferenceFieldDesc = Nothing
        Me.txtVehicleCode.ReferenceFieldName = Nothing
        Me.txtVehicleCode.ReferenceTableName = Nothing
        Me.txtVehicleCode.Size = New System.Drawing.Size(180, 22)
        Me.txtVehicleCode.TabIndex = 1620
        Me.txtVehicleCode.Value = ""
        '
        'lblVehicleCode
        '
        Me.lblVehicleCode.FieldName = Nothing
        Me.lblVehicleCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVehicleCode.Location = New System.Drawing.Point(13, 153)
        Me.lblVehicleCode.Name = "lblVehicleCode"
        Me.lblVehicleCode.Size = New System.Drawing.Size(74, 16)
        Me.lblVehicleCode.TabIndex = 1621
        Me.lblVehicleCode.Text = "Vehicle Code"
        '
        'txtInvoiceno
        '
        Me.txtInvoiceno.AutoSize = False
        Me.txtInvoiceno.BorderVisible = True
        Me.txtInvoiceno.FieldName = Nothing
        Me.txtInvoiceno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvoiceno.Location = New System.Drawing.Point(515, 26)
        Me.txtInvoiceno.Name = "txtInvoiceno"
        Me.txtInvoiceno.Size = New System.Drawing.Size(117, 20)
        Me.txtInvoiceno.TabIndex = 1619
        Me.txtInvoiceno.TextWrap = False
        '
        'lblInvoiceNo
        '
        Me.lblInvoiceNo.FieldName = Nothing
        Me.lblInvoiceNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInvoiceNo.Location = New System.Drawing.Point(449, 28)
        Me.lblInvoiceNo.Name = "lblInvoiceNo"
        Me.lblInvoiceNo.Size = New System.Drawing.Size(60, 16)
        Me.lblInvoiceNo.TabIndex = 1618
        Me.lblInvoiceNo.Text = "Invoice No"
        '
        'chkReplacement
        '
        Me.chkReplacement.AutoSize = True
        Me.chkReplacement.Location = New System.Drawing.Point(638, 27)
        Me.chkReplacement.Name = "chkReplacement"
        Me.chkReplacement.Size = New System.Drawing.Size(88, 18)
        Me.chkReplacement.TabIndex = 1617
        Me.chkReplacement.Text = "Replacement"
        Me.chkReplacement.UseVisualStyleBackColor = True
        '
        'gbQtySummary
        '
        Me.gbQtySummary.Controls.Add(Me.txtBalQty)
        Me.gbQtySummary.Controls.Add(Me.txtOrderQty)
        Me.gbQtySummary.Controls.Add(Me.lblbalQty)
        Me.gbQtySummary.Controls.Add(Me.lblOrderQty)
        Me.gbQtySummary.Location = New System.Drawing.Point(447, 47)
        Me.gbQtySummary.Name = "gbQtySummary"
        Me.gbQtySummary.Size = New System.Drawing.Size(178, 82)
        Me.gbQtySummary.TabIndex = 1616
        Me.gbQtySummary.TabStop = False
        Me.gbQtySummary.Text = "Qty Summary"
        '
        'txtBalQty
        '
        Me.txtBalQty.AutoSize = False
        Me.txtBalQty.BorderVisible = True
        Me.txtBalQty.FieldName = Nothing
        Me.txtBalQty.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBalQty.Location = New System.Drawing.Point(90, 42)
        Me.txtBalQty.Name = "txtBalQty"
        Me.txtBalQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBalQty.Size = New System.Drawing.Size(79, 20)
        Me.txtBalQty.TabIndex = 1588
        Me.txtBalQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.txtBalQty.TextWrap = False
        '
        'txtOrderQty
        '
        Me.txtOrderQty.AutoSize = False
        Me.txtOrderQty.BorderVisible = True
        Me.txtOrderQty.FieldName = Nothing
        Me.txtOrderQty.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOrderQty.Location = New System.Drawing.Point(90, 20)
        Me.txtOrderQty.Name = "txtOrderQty"
        Me.txtOrderQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOrderQty.Size = New System.Drawing.Size(79, 20)
        Me.txtOrderQty.TabIndex = 1587
        Me.txtOrderQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.txtOrderQty.TextWrap = False
        '
        'lblbalQty
        '
        Me.lblbalQty.FieldName = Nothing
        Me.lblbalQty.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblbalQty.Location = New System.Drawing.Point(6, 44)
        Me.lblbalQty.Name = "lblbalQty"
        Me.lblbalQty.Size = New System.Drawing.Size(68, 16)
        Me.lblbalQty.TabIndex = 1580
        Me.lblbalQty.Text = "Balance Qty"
        '
        'lblOrderQty
        '
        Me.lblOrderQty.FieldName = Nothing
        Me.lblOrderQty.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOrderQty.Location = New System.Drawing.Point(6, 22)
        Me.lblOrderQty.Name = "lblOrderQty"
        Me.lblOrderQty.Size = New System.Drawing.Size(56, 16)
        Me.lblOrderQty.TabIndex = 1579
        Me.lblOrderQty.Text = "Order Qty"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(638, 1)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(97, 24)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 1615
        '
        'txtCustomerCode
        '
        Me.txtCustomerCode.CalculationExpression = Nothing
        Me.txtCustomerCode.FieldCode = Nothing
        Me.txtCustomerCode.FieldDesc = Nothing
        Me.txtCustomerCode.FieldMaxLength = 0
        Me.txtCustomerCode.FieldName = Nothing
        Me.txtCustomerCode.isCalculatedField = False
        Me.txtCustomerCode.IsSourceFromTable = False
        Me.txtCustomerCode.IsSourceFromValueList = False
        Me.txtCustomerCode.IsUnique = False
        Me.txtCustomerCode.Location = New System.Drawing.Point(93, 98)
        Me.txtCustomerCode.MendatroryField = True
        Me.txtCustomerCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerCode.MyLinkLable1 = Me.lblCustomer
        Me.txtCustomerCode.MyLinkLable2 = Me.lblCustomerName
        Me.txtCustomerCode.MyReadOnly = False
        Me.txtCustomerCode.MyShowMasterFormButton = False
        Me.txtCustomerCode.Name = "txtCustomerCode"
        Me.txtCustomerCode.ReferenceFieldDesc = Nothing
        Me.txtCustomerCode.ReferenceFieldName = Nothing
        Me.txtCustomerCode.ReferenceTableName = Nothing
        Me.txtCustomerCode.Size = New System.Drawing.Size(180, 22)
        Me.txtCustomerCode.TabIndex = 1614
        Me.txtCustomerCode.Value = ""
        '
        'lblCustomer
        '
        Me.lblCustomer.FieldName = Nothing
        Me.lblCustomer.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomer.Location = New System.Drawing.Point(12, 101)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.Size = New System.Drawing.Size(55, 16)
        Me.lblCustomer.TabIndex = 1602
        Me.lblCustomer.Text = "Customer"
        '
        'lblCustomerName
        '
        Me.lblCustomerName.AutoSize = False
        Me.lblCustomerName.BorderVisible = True
        Me.lblCustomerName.FieldName = Nothing
        Me.lblCustomerName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerName.Location = New System.Drawing.Point(279, 99)
        Me.lblCustomerName.Name = "lblCustomerName"
        Me.lblCustomerName.Size = New System.Drawing.Size(162, 20)
        Me.lblCustomerName.TabIndex = 1601
        Me.lblCustomerName.TextWrap = False
        '
        'lblDocCode
        '
        Me.lblDocCode.FieldName = Nothing
        Me.lblDocCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocCode.Location = New System.Drawing.Point(12, 5)
        Me.lblDocCode.Name = "lblDocCode"
        Me.lblDocCode.Size = New System.Drawing.Size(33, 16)
        Me.lblDocCode.TabIndex = 1603
        Me.lblDocCode.Text = "Code"
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.XpertERPDairySale.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(420, 3)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(21, 20)
        Me.btnAddNew.TabIndex = 1600
        '
        'txtDocCode
        '
        Me.txtDocCode.FieldName = Nothing
        Me.txtDocCode.Location = New System.Drawing.Point(93, 2)
        Me.txtDocCode.MendatroryField = False
        Me.txtDocCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocCode.MyLinkLable1 = Me.lblDocCode
        Me.txtDocCode.MyLinkLable2 = Nothing
        Me.txtDocCode.MyMaxLength = 32767
        Me.txtDocCode.MyReadOnly = False
        Me.txtDocCode.Name = "txtDocCode"
        Me.txtDocCode.Size = New System.Drawing.Size(327, 22)
        Me.txtDocCode.TabIndex = 1598
        Me.txtDocCode.TabStop = False
        Me.txtDocCode.Value = ""
        '
        'lblSubLocationDesc
        '
        Me.lblSubLocationDesc.AutoSize = False
        Me.lblSubLocationDesc.BorderVisible = True
        Me.lblSubLocationDesc.FieldName = Nothing
        Me.lblSubLocationDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubLocationDesc.Location = New System.Drawing.Point(279, 75)
        Me.lblSubLocationDesc.Name = "lblSubLocationDesc"
        Me.lblSubLocationDesc.Size = New System.Drawing.Size(162, 20)
        Me.lblSubLocationDesc.TabIndex = 1612
        Me.lblSubLocationDesc.TextWrap = False
        '
        'lblSubLocaiton
        '
        Me.lblSubLocaiton.FieldName = Nothing
        Me.lblSubLocaiton.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubLocaiton.Location = New System.Drawing.Point(12, 77)
        Me.lblSubLocaiton.Name = "lblSubLocaiton"
        Me.lblSubLocaiton.Size = New System.Drawing.Size(72, 16)
        Me.lblSubLocaiton.TabIndex = 1613
        Me.lblSubLocaiton.Text = "Sub Location"
        '
        'txtOrderNo
        '
        Me.txtOrderNo.CalculationExpression = Nothing
        Me.txtOrderNo.FieldCode = Nothing
        Me.txtOrderNo.FieldDesc = Nothing
        Me.txtOrderNo.FieldMaxLength = 0
        Me.txtOrderNo.FieldName = Nothing
        Me.txtOrderNo.isCalculatedField = False
        Me.txtOrderNo.IsSourceFromTable = False
        Me.txtOrderNo.IsSourceFromValueList = False
        Me.txtOrderNo.IsUnique = False
        Me.txtOrderNo.Location = New System.Drawing.Point(93, 25)
        Me.txtOrderNo.MendatroryField = False
        Me.txtOrderNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOrderNo.MyLinkLable1 = Me.lblorderNo
        Me.txtOrderNo.MyLinkLable2 = Me.lblorderdesc
        Me.txtOrderNo.MyReadOnly = False
        Me.txtOrderNo.MyShowMasterFormButton = False
        Me.txtOrderNo.Name = "txtOrderNo"
        Me.txtOrderNo.ReferenceFieldDesc = Nothing
        Me.txtOrderNo.ReferenceFieldName = Nothing
        Me.txtOrderNo.ReferenceTableName = Nothing
        Me.txtOrderNo.Size = New System.Drawing.Size(180, 22)
        Me.txtOrderNo.TabIndex = 1604
        Me.txtOrderNo.Value = ""
        '
        'txtSubLocation
        '
        Me.txtSubLocation.CalculationExpression = Nothing
        Me.txtSubLocation.FieldCode = Nothing
        Me.txtSubLocation.FieldDesc = Nothing
        Me.txtSubLocation.FieldMaxLength = 0
        Me.txtSubLocation.FieldName = Nothing
        Me.txtSubLocation.isCalculatedField = False
        Me.txtSubLocation.IsSourceFromTable = False
        Me.txtSubLocation.IsSourceFromValueList = False
        Me.txtSubLocation.IsUnique = False
        Me.txtSubLocation.Location = New System.Drawing.Point(93, 74)
        Me.txtSubLocation.MendatroryField = False
        Me.txtSubLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubLocation.MyLinkLable1 = Me.lblSubLocaiton
        Me.txtSubLocation.MyLinkLable2 = Me.lblSubLocationDesc
        Me.txtSubLocation.MyReadOnly = False
        Me.txtSubLocation.MyShowMasterFormButton = False
        Me.txtSubLocation.Name = "txtSubLocation"
        Me.txtSubLocation.ReferenceFieldDesc = Nothing
        Me.txtSubLocation.ReferenceFieldName = Nothing
        Me.txtSubLocation.ReferenceTableName = Nothing
        Me.txtSubLocation.Size = New System.Drawing.Size(180, 22)
        Me.txtSubLocation.TabIndex = 1611
        Me.txtSubLocation.Value = ""
        '
        'lblDate
        '
        Me.lblDate.FieldName = Nothing
        Me.lblDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate.Location = New System.Drawing.Point(454, 5)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(30, 16)
        Me.lblDate.TabIndex = 1599
        Me.lblDate.Text = "Date"
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
        Me.txtDate.Location = New System.Drawing.Point(505, 4)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.lblDate
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(127, 18)
        Me.txtDate.TabIndex = 1610
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011 11:29 AM"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'lblLocationDesc
        '
        Me.lblLocationDesc.AutoSize = False
        Me.lblLocationDesc.BorderVisible = True
        Me.lblLocationDesc.FieldName = Nothing
        Me.lblLocationDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocationDesc.Location = New System.Drawing.Point(279, 51)
        Me.lblLocationDesc.Name = "lblLocationDesc"
        Me.lblLocationDesc.Size = New System.Drawing.Size(162, 20)
        Me.lblLocationDesc.TabIndex = 1609
        Me.lblLocationDesc.TextWrap = False
        '
        'txtLocation
        '
        Me.txtLocation.CalculationExpression = Nothing
        Me.txtLocation.FieldCode = Nothing
        Me.txtLocation.FieldDesc = Nothing
        Me.txtLocation.FieldMaxLength = 0
        Me.txtLocation.FieldName = Nothing
        Me.txtLocation.isCalculatedField = False
        Me.txtLocation.IsSourceFromTable = False
        Me.txtLocation.IsSourceFromValueList = False
        Me.txtLocation.IsUnique = False
        Me.txtLocation.Location = New System.Drawing.Point(93, 50)
        Me.txtLocation.MendatroryField = True
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.lblLocation
        Me.txtLocation.MyLinkLable2 = Me.lblLocationDesc
        Me.txtLocation.MyReadOnly = False
        Me.txtLocation.MyShowMasterFormButton = False
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.ReferenceFieldDesc = Nothing
        Me.txtLocation.ReferenceFieldName = Nothing
        Me.txtLocation.ReferenceTableName = Nothing
        Me.txtLocation.Size = New System.Drawing.Size(180, 22)
        Me.txtLocation.TabIndex = 1608
        Me.txtLocation.Value = ""
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(12, 53)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 16)
        Me.lblLocation.TabIndex = 1607
        Me.lblLocation.Text = "Location"
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1.MyExportFilePath = ""
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(979, 186)
        Me.gv1.TabIndex = 19
        Me.gv1.TabStop = False
        Me.gv1.VarID = ""
        '
        'lblShiftType
        '
        Me.lblShiftType.Location = New System.Drawing.Point(686, -3)
        Me.lblShiftType.Name = "lblShiftType"
        Me.lblShiftType.Size = New System.Drawing.Size(2, 2)
        Me.lblShiftType.TabIndex = 1536
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
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.SplitContainer3)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(47.0!, 26.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(906, 404)
        Me.RadPageViewPage2.Text = "Taxes"
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.GroupBox1)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtTaxGroup)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblTaxGrpName)
        Me.SplitContainer3.Panel1.Controls.Add(Me.RadLabel11)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.gv2)
        Me.SplitContainer3.Size = New System.Drawing.Size(906, 404)
        Me.SplitContainer3.SplitterDistance = 47
        Me.SplitContainer3.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbtnTaxCalManual)
        Me.GroupBox1.Controls.Add(Me.rbtnTaxCalAutomatic)
        Me.GroupBox1.Location = New System.Drawing.Point(559, 7)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(163, 36)
        Me.GroupBox1.TabIndex = 23
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Tax Calculation Type"
        '
        'rbtnTaxCalManual
        '
        Me.rbtnTaxCalManual.Location = New System.Drawing.Point(89, 13)
        Me.rbtnTaxCalManual.MyLinkLable1 = Nothing
        Me.rbtnTaxCalManual.MyLinkLable2 = Nothing
        Me.rbtnTaxCalManual.Name = "rbtnTaxCalManual"
        Me.rbtnTaxCalManual.Size = New System.Drawing.Size(57, 18)
        Me.rbtnTaxCalManual.TabIndex = 1
        Me.rbtnTaxCalManual.TabStop = False
        Me.rbtnTaxCalManual.Text = "Manual"
        '
        'rbtnTaxCalAutomatic
        '
        Me.rbtnTaxCalAutomatic.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rbtnTaxCalAutomatic.Location = New System.Drawing.Point(7, 13)
        Me.rbtnTaxCalAutomatic.MyLinkLable1 = Nothing
        Me.rbtnTaxCalAutomatic.MyLinkLable2 = Nothing
        Me.rbtnTaxCalAutomatic.Name = "rbtnTaxCalAutomatic"
        Me.rbtnTaxCalAutomatic.Size = New System.Drawing.Size(72, 18)
        Me.rbtnTaxCalAutomatic.TabIndex = 0
        Me.rbtnTaxCalAutomatic.Text = "Automatic"
        Me.rbtnTaxCalAutomatic.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
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
        Me.txtTaxGroup.Location = New System.Drawing.Point(81, 12)
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
        Me.txtTaxGroup.Size = New System.Drawing.Size(143, 19)
        Me.txtTaxGroup.TabIndex = 21
        Me.txtTaxGroup.Value = ""
        '
        'RadLabel11
        '
        Me.RadLabel11.FieldName = Nothing
        Me.RadLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel11.Location = New System.Drawing.Point(15, 15)
        Me.RadLabel11.Name = "RadLabel11"
        Me.RadLabel11.Size = New System.Drawing.Size(60, 16)
        Me.RadLabel11.TabIndex = 24
        Me.RadLabel11.Text = "Tax Group"
        '
        'lblTaxGrpName
        '
        Me.lblTaxGrpName.AutoSize = False
        Me.lblTaxGrpName.BorderVisible = True
        Me.lblTaxGrpName.FieldName = Nothing
        Me.lblTaxGrpName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTaxGrpName.Location = New System.Drawing.Point(232, 12)
        Me.lblTaxGrpName.Name = "lblTaxGrpName"
        Me.lblTaxGrpName.Size = New System.Drawing.Size(321, 20)
        Me.lblTaxGrpName.TabIndex = 22
        Me.lblTaxGrpName.TextWrap = False
        '
        'gv2
        '
        Me.gv2.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv2.BeginEditMode = Telerik.WinControls.RadGridViewBeginEditMode.BeginEditOnEnter
        Me.gv2.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv2.ForeColor = System.Drawing.Color.Black
        Me.gv2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv2.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv2.MasterTemplate.AllowAddNewRow = False
        Me.gv2.MasterTemplate.AllowDeleteRow = False
        Me.gv2.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv2.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv2.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gv2.MyExportFilePath = ""
        Me.gv2.MyStopExport = False
        Me.gv2.Name = "gv2"
        Me.gv2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv2.ShowHeaderCellButtons = True
        Me.gv2.Size = New System.Drawing.Size(906, 353)
        Me.gv2.TabIndex = 21
        Me.gv2.TabStop = False
        Me.gv2.VarID = ""
        '
        'RadPageViewPage5
        '
        Me.RadPageViewPage5.Controls.Add(Me.UcAttachment1)
        Me.RadPageViewPage5.ItemSize = New System.Drawing.SizeF(73.0!, 26.0!)
        Me.RadPageViewPage5.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage5.Name = "RadPageViewPage5"
        Me.RadPageViewPage5.Size = New System.Drawing.Size(906, 404)
        Me.RadPageViewPage5.Text = "Attachment"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(906, 404)
        Me.UcAttachment1.TabIndex = 1
        Me.UcAttachment1.TabStop = False
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.btnReverseUnpost)
        Me.RadPageViewPage3.Controls.Add(Me.btnCancel)
        Me.RadPageViewPage3.Controls.Add(Me.lblTaxAmt)
        Me.RadPageViewPage3.Controls.Add(Me.txtTaxAmt)
        Me.RadPageViewPage3.Controls.Add(Me.lblDocAmtWithoutTax)
        Me.RadPageViewPage3.Controls.Add(Me.txtDocAmtWithoutTax)
        Me.RadPageViewPage3.Controls.Add(Me.lblDocamt)
        Me.RadPageViewPage3.Controls.Add(Me.txtDocAmt)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(41.0!, 26.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(979, 367)
        Me.RadPageViewPage3.Text = "Total"
        '
        'btnReverseUnpost
        '
        Me.btnReverseUnpost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReverseUnpost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverseUnpost.Location = New System.Drawing.Point(577, 152)
        Me.btnReverseUnpost.Name = "btnReverseUnpost"
        Me.btnReverseUnpost.Size = New System.Drawing.Size(101, 22)
        Me.btnReverseUnpost.TabIndex = 1594
        Me.btnReverseUnpost.Text = "Reverse/Unpost"
        Me.btnReverseUnpost.Visible = False
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(577, 180)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(101, 22)
        Me.btnCancel.TabIndex = 1593
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.Visible = False
        '
        'lblTaxAmt
        '
        Me.lblTaxAmt.FieldName = Nothing
        Me.lblTaxAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTaxAmt.Location = New System.Drawing.Point(20, 46)
        Me.lblTaxAmt.Name = "lblTaxAmt"
        Me.lblTaxAmt.Size = New System.Drawing.Size(61, 16)
        Me.lblTaxAmt.TabIndex = 1591
        Me.lblTaxAmt.Text = "+ Tax Amt"
        '
        'txtTaxAmt
        '
        Me.txtTaxAmt.AutoSize = False
        Me.txtTaxAmt.BorderVisible = True
        Me.txtTaxAmt.FieldName = Nothing
        Me.txtTaxAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTaxAmt.Location = New System.Drawing.Point(198, 44)
        Me.txtTaxAmt.Name = "txtTaxAmt"
        Me.txtTaxAmt.Size = New System.Drawing.Size(104, 20)
        Me.txtTaxAmt.TabIndex = 1592
        Me.txtTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDocAmtWithoutTax
        '
        Me.lblDocAmtWithoutTax.FieldName = Nothing
        Me.lblDocAmtWithoutTax.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocAmtWithoutTax.Location = New System.Drawing.Point(20, 22)
        Me.lblDocAmtWithoutTax.Name = "lblDocAmtWithoutTax"
        Me.lblDocAmtWithoutTax.Size = New System.Drawing.Size(175, 16)
        Me.lblDocAmtWithoutTax.TabIndex = 1589
        Me.lblDocAmtWithoutTax.Text = "Document Amount Without Tax"
        '
        'txtDocAmtWithoutTax
        '
        Me.txtDocAmtWithoutTax.AutoSize = False
        Me.txtDocAmtWithoutTax.BorderVisible = True
        Me.txtDocAmtWithoutTax.FieldName = Nothing
        Me.txtDocAmtWithoutTax.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDocAmtWithoutTax.Location = New System.Drawing.Point(198, 20)
        Me.txtDocAmtWithoutTax.Name = "txtDocAmtWithoutTax"
        Me.txtDocAmtWithoutTax.Size = New System.Drawing.Size(104, 20)
        Me.txtDocAmtWithoutTax.TabIndex = 1590
        Me.txtDocAmtWithoutTax.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDocamt
        '
        Me.lblDocamt.FieldName = Nothing
        Me.lblDocamt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocamt.Location = New System.Drawing.Point(20, 70)
        Me.lblDocamt.Name = "lblDocamt"
        Me.lblDocamt.Size = New System.Drawing.Size(108, 16)
        Me.lblDocamt.TabIndex = 1587
        Me.lblDocamt.Text = "Document Amount"
        '
        'txtDocAmt
        '
        Me.txtDocAmt.AutoSize = False
        Me.txtDocAmt.BorderVisible = True
        Me.txtDocAmt.FieldName = Nothing
        Me.txtDocAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDocAmt.Location = New System.Drawing.Point(198, 68)
        Me.txtDocAmt.Name = "txtDocAmt"
        Me.txtDocAmt.Size = New System.Drawing.Size(104, 20)
        Me.txtDocAmt.TabIndex = 1588
        Me.txtDocAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'SplitContainer4
        '
        Me.SplitContainer4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer4.IsSplitterFixed = True
        Me.SplitContainer4.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer4.Name = "SplitContainer4"
        Me.SplitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer4.Panel1
        '
        Me.SplitContainer4.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer4.Panel2
        '
        Me.SplitContainer4.Panel2.Controls.Add(Me.btnHistory)
        Me.SplitContainer4.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer4.Panel2.Controls.Add(Me.btnprinte_wayBill)
        Me.SplitContainer4.Panel2.Controls.Add(Me.btnShowInventory)
        Me.SplitContainer4.Panel2.Controls.Add(Me.btnInvoiceJE)
        Me.SplitContainer4.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer4.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer4.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer4.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer4.Size = New System.Drawing.Size(1000, 450)
        Me.SplitContainer4.SplitterDistance = 413
        Me.SplitContainer4.TabIndex = 3
        '
        'btnHistory
        '
        Me.btnHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnHistory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHistory.Location = New System.Drawing.Point(271, 6)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Size = New System.Drawing.Size(61, 22)
        Me.btnHistory.TabIndex = 1630
        Me.btnHistory.Text = "History"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(204, 6)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(61, 22)
        Me.btnPrint.TabIndex = 1530
        Me.btnPrint.Text = "Print"
        '
        'btnprinte_wayBill
        '
        Me.btnprinte_wayBill.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnprinte_wayBill.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnprinte_wayBill.Location = New System.Drawing.Point(637, 5)
        Me.btnprinte_wayBill.Name = "btnprinte_wayBill"
        Me.btnprinte_wayBill.Size = New System.Drawing.Size(97, 22)
        Me.btnprinte_wayBill.TabIndex = 1529
        Me.btnprinte_wayBill.Text = "Print E-Way Bill"
        '
        'btnShowInventory
        '
        Me.btnShowInventory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnShowInventory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShowInventory.Location = New System.Drawing.Point(740, 5)
        Me.btnShowInventory.Name = "btnShowInventory"
        Me.btnShowInventory.Size = New System.Drawing.Size(101, 22)
        Me.btnShowInventory.TabIndex = 23
        Me.btnShowInventory.Text = "Show Inventory"
        '
        'btnInvoiceJE
        '
        Me.btnInvoiceJE.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnInvoiceJE.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnInvoiceJE.Location = New System.Drawing.Point(847, 5)
        Me.btnInvoiceJE.Name = "btnInvoiceJE"
        Me.btnInvoiceJE.Size = New System.Drawing.Size(61, 22)
        Me.btnInvoiceJE.TabIndex = 22
        Me.btnInvoiceJE.Text = "Invoice JE"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(914, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(71, 22)
        Me.btnClose.TabIndex = 21
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(137, 5)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(61, 22)
        Me.btnDelete.TabIndex = 20
        Me.btnDelete.Text = "Delete"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(73, 5)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(60, 22)
        Me.btnPost.TabIndex = 19
        Me.btnPost.Text = "Post"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(14, 5)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(57, 22)
        Me.btnSave.TabIndex = 18
        Me.btnSave.Text = "Save"
        '
        'chkewaybill
        '
        Me.chkewaybill.AutoSize = True
        Me.chkewaybill.Location = New System.Drawing.Point(638, 69)
        Me.chkewaybill.Name = "chkewaybill"
        Me.chkewaybill.Size = New System.Drawing.Size(109, 18)
        Me.chkewaybill.TabIndex = 1630
        Me.chkewaybill.Text = "Create E-way Bill"
        Me.chkewaybill.UseVisualStyleBackColor = True
        '
        'FrmSalesOrderDispatch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1000, 450)
        Me.Controls.Add(Me.SplitContainer4)
        Me.Name = "FrmSalesOrderDispatch"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Sales Order Dispatch"
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        Me.SplitContainer5.Panel1.ResumeLayout(False)
        Me.SplitContainer5.Panel1.PerformLayout()
        Me.SplitContainer5.Panel2.ResumeLayout(False)
        Me.SplitContainer5.ResumeLayout(False)
        CType(Me.lblInvnoForReplacement, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblorderNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblorderdesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemark, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRemark, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTransporterName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel31, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVehicleNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVehicleCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtInvoiceno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblInvoiceNo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbQtySummary.ResumeLayout(False)
        Me.gbQtySummary.PerformLayout()
        CType(Me.txtBalQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOrderQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblbalQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblOrderQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomerName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSubLocationDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSubLocaiton, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblShiftType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAbandonmentNo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.PerformLayout()
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.rbtnTaxCalManual, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnTaxCalAutomatic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTaxGrpName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage5.ResumeLayout(False)
        Me.RadPageViewPage3.ResumeLayout(False)
        Me.RadPageViewPage3.PerformLayout()
        CType(Me.btnReverseUnpost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTaxAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTaxAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocAmtWithoutTax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDocAmtWithoutTax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocamt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDocAmt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer4.Panel1.ResumeLayout(False)
        Me.SplitContainer4.Panel2.ResumeLayout(False)
        Me.SplitContainer4.ResumeLayout(False)
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnprinte_wayBill, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnShowInventory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnInvoiceJE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents lblShiftType As RadLabel
    Friend WithEvents lblAbandonmentNo As common.Controls.MyLabel
    Friend WithEvents RadPageViewPage2 As RadPageViewPage
    Friend WithEvents SplitContainer3 As SplitContainer
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents rbtnTaxCalManual As common.Controls.MyRadioButton
    Friend WithEvents rbtnTaxCalAutomatic As common.Controls.MyRadioButton
    Friend WithEvents txtTaxGroup As common.UserControls.txtFinder
    Friend WithEvents RadLabel11 As common.Controls.MyLabel
    Friend WithEvents lblTaxGrpName As common.Controls.MyLabel
    Friend WithEvents gv2 As common.UserControls.MyRadGridView
    Friend WithEvents RadPageViewPage5 As RadPageViewPage
    Friend WithEvents UcAttachment1 As ucAttachment
    Friend WithEvents RadPageViewPage3 As RadPageViewPage
    Friend WithEvents lblTaxAmt As common.Controls.MyLabel
    Friend WithEvents txtTaxAmt As common.Controls.MyLabel
    Friend WithEvents lblDocAmtWithoutTax As common.Controls.MyLabel
    Friend WithEvents txtDocAmtWithoutTax As common.Controls.MyLabel
    Friend WithEvents lblDocamt As common.Controls.MyLabel
    Friend WithEvents txtDocAmt As common.Controls.MyLabel
    Friend WithEvents SplitContainer5 As SplitContainer
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents SplitContainer4 As SplitContainer
    Friend WithEvents btnShowInventory As RadButton
    Friend WithEvents btnInvoiceJE As RadButton
    Friend WithEvents btnClose As RadButton
    Friend WithEvents btnDelete As RadButton
    Friend WithEvents btnPost As RadButton
    Friend WithEvents btnSave As RadButton
    Friend WithEvents txtRemark As common.Controls.MyTextBox
    Friend WithEvents lblRemark As common.Controls.MyLabel
    Friend WithEvents lblTransporterName As common.Controls.MyTextBox
    Friend WithEvents MyLabel31 As common.Controls.MyLabel
    Friend WithEvents txtTransporterCode As common.UserControls.txtFinder
    Friend WithEvents lblVehicleNo As common.Controls.MyTextBox
    Friend WithEvents txtVehicleCode As common.UserControls.txtFinder
    Friend WithEvents lblVehicleCode As common.Controls.MyLabel
    Friend WithEvents lblInvoiceNo As common.Controls.MyLabel
    Friend WithEvents chkReplacement As CheckBox
    Friend WithEvents gbQtySummary As GroupBox
    Friend WithEvents txtBalQty As common.Controls.MyLabel
    Friend WithEvents txtOrderQty As common.Controls.MyLabel
    Friend WithEvents lblbalQty As common.Controls.MyLabel
    Friend WithEvents lblOrderQty As common.Controls.MyLabel
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents txtCustomerCode As common.UserControls.txtFinder
    Friend WithEvents lblCustomer As common.Controls.MyLabel
    Friend WithEvents lblCustomerName As common.Controls.MyLabel
    Friend WithEvents lblDocCode As common.Controls.MyLabel
    Friend WithEvents btnAddNew As RadButton
    Friend WithEvents txtDocCode As common.UserControls.txtNavigator
    Friend WithEvents lblSubLocationDesc As common.Controls.MyLabel
    Friend WithEvents lblSubLocaiton As common.Controls.MyLabel
    Friend WithEvents txtOrderNo As common.UserControls.txtFinder
    Friend WithEvents lblorderNo As common.Controls.MyLabel
    Friend WithEvents lblorderdesc As common.Controls.MyLabel
    Friend WithEvents txtSubLocation As common.UserControls.txtFinder
    Friend WithEvents lblDate As common.Controls.MyLabel
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblLocationDesc As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtFinder
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents btnReverseUnpost As RadButton
    Friend WithEvents btnCancel As RadButton
    Friend WithEvents btnprinte_wayBill As RadButton
    Friend WithEvents txtInvoice_for_replacement As common.UserControls.txtFinder
    Friend WithEvents txtInvoiceno As common.Controls.MyLabel
    Friend WithEvents lblInvnoForReplacement As common.Controls.MyLabel
    Friend WithEvents btnPrint As RadButton
    Friend WithEvents btnHistory As RadButton
    Friend WithEvents chkewaybill As CheckBox
End Class
