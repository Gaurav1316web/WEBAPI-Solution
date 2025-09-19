<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSalesOrder
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
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.gbQtySummary = New System.Windows.Forms.GroupBox()
        Me.txtBalQty = New common.Controls.MyLabel()
        Me.txtTenderQty = New common.Controls.MyLabel()
        Me.lblbalQty = New common.Controls.MyLabel()
        Me.lblTenderQty = New common.Controls.MyLabel()
        Me.txtRemark = New common.Controls.MyTextBox()
        Me.lblRemark = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.txtRefDate = New common.Controls.MyDateTimePicker()
        Me.lblRefdate = New common.Controls.MyLabel()
        Me.txtRefNo = New common.Controls.MyTextBox()
        Me.lblRefNo = New common.Controls.MyLabel()
        Me.txtCustomerCode = New common.UserControls.txtFinder()
        Me.lblCustomer = New common.Controls.MyLabel()
        Me.lblCustomerName = New common.Controls.MyLabel()
        Me.lblDocCode = New common.Controls.MyLabel()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.txtDocCode = New common.UserControls.txtNavigator()
        Me.lblSubLocationDesc = New common.Controls.MyLabel()
        Me.lblSubLocaiton = New common.Controls.MyLabel()
        Me.txtRALNo = New common.UserControls.txtFinder()
        Me.lblRALNo = New common.Controls.MyLabel()
        Me.lblRalNoDesc = New common.Controls.MyLabel()
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
        Me.lblTaxAmt = New common.Controls.MyLabel()
        Me.txtTaxAmt = New common.Controls.MyLabel()
        Me.lblDocAmtWithoutTax = New common.Controls.MyLabel()
        Me.txtDocAmtWithoutTax = New common.Controls.MyLabel()
        Me.lblDocamt = New common.Controls.MyLabel()
        Me.txtDocAmt = New common.Controls.MyLabel()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnReverseAndUnPost = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.gbQtySummary.SuspendLayout()
        CType(Me.txtBalQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTenderQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblbalQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTenderQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemark, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRemark, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRefDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRefdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRefNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRefNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomerName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSubLocationDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSubLocaiton, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRALNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRalNoDesc, System.ComponentModel.ISupportInitialize).BeginInit()
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
        CType(Me.lblTaxAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTaxAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocAmtWithoutTax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDocAmtWithoutTax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocamt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDocAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReverseAndUnPost, System.ComponentModel.ISupportInitialize).BeginInit()
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
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReverseAndUnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(867, 500)
        Me.SplitContainer1.SplitterDistance = 465
        Me.SplitContainer1.TabIndex = 0
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
        Me.RadPageView1.Size = New System.Drawing.Size(867, 465)
        Me.RadPageView1.TabIndex = 1
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer2)
        Me.RadPageViewPage1.Controls.Add(Me.lblShiftType)
        Me.RadPageViewPage1.Controls.Add(Me.lblAbandonmentNo)
        Me.RadPageViewPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(89.0!, 26.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(846, 419)
        Me.RadPageViewPage1.Text = "Booking Order"
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.gbQtySummary)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtRemark)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblRemark)
        Me.SplitContainer2.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtRefDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtRefNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblRefdate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblRefNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtCustomerCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblDocCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnAddNew)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDocCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblSubLocationDesc)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblCustomer)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblSubLocaiton)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtRALNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtSubLocation)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblCustomerName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblRALNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblLocationDesc)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblRalNoDesc)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtLocation)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblLocation)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gv1)
        Me.SplitContainer2.Size = New System.Drawing.Size(846, 419)
        Me.SplitContainer2.SplitterDistance = 141
        Me.SplitContainer2.TabIndex = 1576
        '
        'gbQtySummary
        '
        Me.gbQtySummary.Controls.Add(Me.txtBalQty)
        Me.gbQtySummary.Controls.Add(Me.txtTenderQty)
        Me.gbQtySummary.Controls.Add(Me.lblbalQty)
        Me.gbQtySummary.Controls.Add(Me.lblTenderQty)
        Me.gbQtySummary.Location = New System.Drawing.Point(639, 30)
        Me.gbQtySummary.Name = "gbQtySummary"
        Me.gbQtySummary.Size = New System.Drawing.Size(162, 82)
        Me.gbQtySummary.TabIndex = 1586
        Me.gbQtySummary.TabStop = False
        Me.gbQtySummary.Text = "Qty Summary"
        '
        'txtBalQty
        '
        Me.txtBalQty.AutoSize = False
        Me.txtBalQty.BorderVisible = True
        Me.txtBalQty.FieldName = Nothing
        Me.txtBalQty.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBalQty.Location = New System.Drawing.Point(76, 44)
        Me.txtBalQty.Name = "txtBalQty"
        Me.txtBalQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBalQty.Size = New System.Drawing.Size(79, 20)
        Me.txtBalQty.TabIndex = 1588
        Me.txtBalQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.txtBalQty.TextWrap = False
        '
        'txtTenderQty
        '
        Me.txtTenderQty.AutoSize = False
        Me.txtTenderQty.BorderVisible = True
        Me.txtTenderQty.FieldName = Nothing
        Me.txtTenderQty.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTenderQty.Location = New System.Drawing.Point(76, 22)
        Me.txtTenderQty.Name = "txtTenderQty"
        Me.txtTenderQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTenderQty.Size = New System.Drawing.Size(79, 20)
        Me.txtTenderQty.TabIndex = 1587
        Me.txtTenderQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.txtTenderQty.TextWrap = False
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
        'lblTenderQty
        '
        Me.lblTenderQty.FieldName = Nothing
        Me.lblTenderQty.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTenderQty.Location = New System.Drawing.Point(6, 22)
        Me.lblTenderQty.Name = "lblTenderQty"
        Me.lblTenderQty.Size = New System.Drawing.Size(63, 16)
        Me.lblTenderQty.TabIndex = 1579
        Me.lblTenderQty.Text = "Tender Qty"
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
        Me.txtRemark.Location = New System.Drawing.Point(94, 114)
        Me.txtRemark.MendatroryField = False
        Me.txtRemark.MyLinkLable1 = Me.lblRemark
        Me.txtRemark.MyLinkLable2 = Nothing
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.ReferenceFieldDesc = Nothing
        Me.txtRemark.ReferenceFieldName = Nothing
        Me.txtRemark.ReferenceTableName = Nothing
        Me.txtRemark.Size = New System.Drawing.Size(348, 20)
        Me.txtRemark.TabIndex = 1584
        '
        'lblRemark
        '
        Me.lblRemark.FieldName = Nothing
        Me.lblRemark.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRemark.Location = New System.Drawing.Point(13, 116)
        Me.lblRemark.Name = "lblRemark"
        Me.lblRemark.Size = New System.Drawing.Size(46, 16)
        Me.lblRemark.TabIndex = 1583
        Me.lblRemark.Text = "Remark"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(639, 2)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(97, 22)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 1582
        '
        'txtRefDate
        '
        Me.txtRefDate.CalculationExpression = Nothing
        Me.txtRefDate.CustomFormat = "dd/MM/yyyy "
        Me.txtRefDate.FieldCode = Nothing
        Me.txtRefDate.FieldDesc = Nothing
        Me.txtRefDate.FieldMaxLength = 0
        Me.txtRefDate.FieldName = Nothing
        Me.txtRefDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtRefDate.isCalculatedField = False
        Me.txtRefDate.IsSourceFromTable = False
        Me.txtRefDate.IsSourceFromValueList = False
        Me.txtRefDate.IsUnique = False
        Me.txtRefDate.Location = New System.Drawing.Point(506, 49)
        Me.txtRefDate.MendatroryField = False
        Me.txtRefDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtRefDate.MyLinkLable1 = Me.lblRefdate
        Me.txtRefDate.MyLinkLable2 = Nothing
        Me.txtRefDate.Name = "txtRefDate"
        Me.txtRefDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtRefDate.ReferenceFieldDesc = Nothing
        Me.txtRefDate.ReferenceFieldName = Nothing
        Me.txtRefDate.ReferenceTableName = Nothing
        Me.txtRefDate.Size = New System.Drawing.Size(127, 18)
        Me.txtRefDate.TabIndex = 1581
        Me.txtRefDate.TabStop = False
        Me.txtRefDate.Text = "13/06/2011 "
        Me.txtRefDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'lblRefdate
        '
        Me.lblRefdate.FieldName = Nothing
        Me.lblRefdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRefdate.Location = New System.Drawing.Point(455, 50)
        Me.lblRefdate.Name = "lblRefdate"
        Me.lblRefdate.Size = New System.Drawing.Size(51, 16)
        Me.lblRefdate.TabIndex = 1577
        Me.lblRefdate.Text = "Ref Date"
        '
        'txtRefNo
        '
        Me.txtRefNo.CalculationExpression = Nothing
        Me.txtRefNo.FieldCode = Nothing
        Me.txtRefNo.FieldDesc = Nothing
        Me.txtRefNo.FieldMaxLength = 0
        Me.txtRefNo.FieldName = Nothing
        Me.txtRefNo.isCalculatedField = False
        Me.txtRefNo.IsSourceFromTable = False
        Me.txtRefNo.IsSourceFromValueList = False
        Me.txtRefNo.IsUnique = False
        Me.txtRefNo.Location = New System.Drawing.Point(506, 25)
        Me.txtRefNo.MendatroryField = False
        Me.txtRefNo.MyLinkLable1 = Me.lblRefNo
        Me.txtRefNo.MyLinkLable2 = Nothing
        Me.txtRefNo.Name = "txtRefNo"
        Me.txtRefNo.ReferenceFieldDesc = Nothing
        Me.txtRefNo.ReferenceFieldName = Nothing
        Me.txtRefNo.ReferenceTableName = Nothing
        Me.txtRefNo.Size = New System.Drawing.Size(128, 20)
        Me.txtRefNo.TabIndex = 1580
        '
        'lblRefNo
        '
        Me.lblRefNo.FieldName = Nothing
        Me.lblRefNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRefNo.Location = New System.Drawing.Point(455, 27)
        Me.lblRefNo.Name = "lblRefNo"
        Me.lblRefNo.Size = New System.Drawing.Size(41, 16)
        Me.lblRefNo.TabIndex = 1578
        Me.lblRefNo.Text = "Ref No"
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
        Me.txtCustomerCode.Location = New System.Drawing.Point(94, 92)
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
        Me.txtCustomerCode.Size = New System.Drawing.Size(180, 20)
        Me.txtCustomerCode.TabIndex = 1576
        Me.txtCustomerCode.Value = ""
        '
        'lblCustomer
        '
        Me.lblCustomer.FieldName = Nothing
        Me.lblCustomer.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomer.Location = New System.Drawing.Point(13, 94)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.Size = New System.Drawing.Size(55, 16)
        Me.lblCustomer.TabIndex = 21
        Me.lblCustomer.Text = "Customer"
        '
        'lblCustomerName
        '
        Me.lblCustomerName.AutoSize = False
        Me.lblCustomerName.BorderVisible = True
        Me.lblCustomerName.FieldName = Nothing
        Me.lblCustomerName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerName.Location = New System.Drawing.Point(280, 92)
        Me.lblCustomerName.Name = "lblCustomerName"
        Me.lblCustomerName.Size = New System.Drawing.Size(162, 20)
        Me.lblCustomerName.TabIndex = 10
        Me.lblCustomerName.TextWrap = False
        '
        'lblDocCode
        '
        Me.lblDocCode.FieldName = Nothing
        Me.lblDocCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocCode.Location = New System.Drawing.Point(13, 5)
        Me.lblDocCode.Name = "lblDocCode"
        Me.lblDocCode.Size = New System.Drawing.Size(33, 16)
        Me.lblDocCode.TabIndex = 22
        Me.lblDocCode.Text = "Code"
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.XpertERPDairySale.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(421, 3)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(21, 20)
        Me.btnAddNew.TabIndex = 1
        '
        'txtDocCode
        '
        Me.txtDocCode.FieldName = Nothing
        Me.txtDocCode.Location = New System.Drawing.Point(94, 3)
        Me.txtDocCode.MendatroryField = False
        Me.txtDocCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocCode.MyLinkLable1 = Me.lblDocCode
        Me.txtDocCode.MyLinkLable2 = Nothing
        Me.txtDocCode.MyMaxLength = 32767
        Me.txtDocCode.MyReadOnly = False
        Me.txtDocCode.Name = "txtDocCode"
        Me.txtDocCode.Size = New System.Drawing.Size(327, 20)
        Me.txtDocCode.TabIndex = 0
        Me.txtDocCode.TabStop = False
        Me.txtDocCode.Value = ""
        '
        'lblSubLocationDesc
        '
        Me.lblSubLocationDesc.AutoSize = False
        Me.lblSubLocationDesc.BorderVisible = True
        Me.lblSubLocationDesc.FieldName = Nothing
        Me.lblSubLocationDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubLocationDesc.Location = New System.Drawing.Point(280, 70)
        Me.lblSubLocationDesc.Name = "lblSubLocationDesc"
        Me.lblSubLocationDesc.Size = New System.Drawing.Size(162, 20)
        Me.lblSubLocationDesc.TabIndex = 1544
        Me.lblSubLocationDesc.TextWrap = False
        '
        'lblSubLocaiton
        '
        Me.lblSubLocaiton.FieldName = Nothing
        Me.lblSubLocaiton.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubLocaiton.Location = New System.Drawing.Point(13, 72)
        Me.lblSubLocaiton.Name = "lblSubLocaiton"
        Me.lblSubLocaiton.Size = New System.Drawing.Size(72, 16)
        Me.lblSubLocaiton.TabIndex = 1545
        Me.lblSubLocaiton.Text = "Sub Location"
        '
        'txtRALNo
        '
        Me.txtRALNo.CalculationExpression = Nothing
        Me.txtRALNo.FieldCode = Nothing
        Me.txtRALNo.FieldDesc = Nothing
        Me.txtRALNo.FieldMaxLength = 0
        Me.txtRALNo.FieldName = Nothing
        Me.txtRALNo.isCalculatedField = False
        Me.txtRALNo.IsSourceFromTable = False
        Me.txtRALNo.IsSourceFromValueList = False
        Me.txtRALNo.IsUnique = False
        Me.txtRALNo.Location = New System.Drawing.Point(94, 25)
        Me.txtRALNo.MendatroryField = False
        Me.txtRALNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRALNo.MyLinkLable1 = Me.lblRALNo
        Me.txtRALNo.MyLinkLable2 = Me.lblRalNoDesc
        Me.txtRALNo.MyReadOnly = False
        Me.txtRALNo.MyShowMasterFormButton = False
        Me.txtRALNo.Name = "txtRALNo"
        Me.txtRALNo.ReferenceFieldDesc = Nothing
        Me.txtRALNo.ReferenceFieldName = Nothing
        Me.txtRALNo.ReferenceTableName = Nothing
        Me.txtRALNo.Size = New System.Drawing.Size(180, 20)
        Me.txtRALNo.TabIndex = 22
        Me.txtRALNo.Value = ""
        '
        'lblRALNo
        '
        Me.lblRALNo.FieldName = Nothing
        Me.lblRALNo.Location = New System.Drawing.Point(13, 26)
        Me.lblRALNo.Name = "lblRALNo"
        Me.lblRALNo.Size = New System.Drawing.Size(44, 18)
        Me.lblRALNo.TabIndex = 119
        Me.lblRALNo.Text = "RAL No"
        '
        'lblRalNoDesc
        '
        Me.lblRalNoDesc.AutoSize = False
        Me.lblRalNoDesc.BorderVisible = True
        Me.lblRalNoDesc.FieldName = Nothing
        Me.lblRalNoDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRalNoDesc.Location = New System.Drawing.Point(280, 25)
        Me.lblRalNoDesc.Name = "lblRalNoDesc"
        Me.lblRalNoDesc.Size = New System.Drawing.Size(162, 20)
        Me.lblRalNoDesc.TabIndex = 144
        Me.lblRalNoDesc.TextWrap = False
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
        Me.txtSubLocation.Location = New System.Drawing.Point(94, 70)
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
        Me.txtSubLocation.Size = New System.Drawing.Size(180, 20)
        Me.txtSubLocation.TabIndex = 1543
        Me.txtSubLocation.Value = ""
        '
        'lblDate
        '
        Me.lblDate.FieldName = Nothing
        Me.lblDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate.Location = New System.Drawing.Point(455, 5)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(30, 16)
        Me.lblDate.TabIndex = 1
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
        Me.txtDate.Location = New System.Drawing.Point(506, 4)
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
        Me.txtDate.TabIndex = 1524
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
        Me.lblLocationDesc.Location = New System.Drawing.Point(280, 48)
        Me.lblLocationDesc.Name = "lblLocationDesc"
        Me.lblLocationDesc.Size = New System.Drawing.Size(162, 20)
        Me.lblLocationDesc.TabIndex = 154
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
        Me.txtLocation.Location = New System.Drawing.Point(94, 48)
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
        Me.txtLocation.Size = New System.Drawing.Size(180, 20)
        Me.txtLocation.TabIndex = 153
        Me.txtLocation.Value = ""
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(13, 50)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 16)
        Me.lblLocation.TabIndex = 152
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
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition3
        Me.gv1.MyExportFilePath = ""
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(846, 274)
        Me.gv1.TabIndex = 18
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
        Me.RadPageViewPage2.Size = New System.Drawing.Size(765, 419)
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
        Me.SplitContainer3.Size = New System.Drawing.Size(765, 419)
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
        Me.gv2.MasterTemplate.ViewDefinition = TableViewDefinition4
        Me.gv2.MyExportFilePath = ""
        Me.gv2.MyStopExport = False
        Me.gv2.Name = "gv2"
        Me.gv2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv2.ShowHeaderCellButtons = True
        Me.gv2.Size = New System.Drawing.Size(765, 368)
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
        Me.RadPageViewPage5.Size = New System.Drawing.Size(775, 369)
        Me.RadPageViewPage5.Text = "Attachment"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(775, 369)
        Me.UcAttachment1.TabIndex = 1
        Me.UcAttachment1.TabStop = False
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.lblTaxAmt)
        Me.RadPageViewPage3.Controls.Add(Me.txtTaxAmt)
        Me.RadPageViewPage3.Controls.Add(Me.lblDocAmtWithoutTax)
        Me.RadPageViewPage3.Controls.Add(Me.txtDocAmtWithoutTax)
        Me.RadPageViewPage3.Controls.Add(Me.lblDocamt)
        Me.RadPageViewPage3.Controls.Add(Me.txtDocAmt)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(41.0!, 26.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(765, 419)
        Me.RadPageViewPage3.Text = "Total"
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
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(784, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(71, 22)
        Me.btnClose.TabIndex = 11
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(133, 5)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(61, 22)
        Me.btnDelete.TabIndex = 5
        Me.btnDelete.Text = "Delete"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(69, 5)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(60, 22)
        Me.btnPost.TabIndex = 4
        Me.btnPost.Text = "Post"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(10, 5)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(57, 22)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "Save"
        '
        'btnReverseAndUnPost
        '
        Me.btnReverseAndUnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReverseAndUnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverseAndUnPost.Location = New System.Drawing.Point(679, 4)
        Me.btnReverseAndUnPost.Name = "btnReverseAndUnPost"
        Me.btnReverseAndUnPost.Size = New System.Drawing.Size(99, 22)
        Me.btnReverseAndUnPost.TabIndex = 12
        Me.btnReverseAndUnPost.Text = "Reverse/UnPost"
        Me.btnReverseAndUnPost.Visible = False
        '
        'FrmSalesOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(867, 500)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmSalesOrder"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Sales Order"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        Me.gbQtySummary.ResumeLayout(False)
        Me.gbQtySummary.PerformLayout()
        CType(Me.txtBalQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTenderQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblbalQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTenderQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemark, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRemark, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRefDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRefdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRefNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRefNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomerName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSubLocationDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSubLocaiton, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRALNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRalNoDesc, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me.lblTaxAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTaxAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocAmtWithoutTax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDocAmtWithoutTax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocamt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDocAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReverseAndUnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents lblSubLocationDesc As common.Controls.MyLabel
    Friend WithEvents lblSubLocaiton As common.Controls.MyLabel
    Friend WithEvents txtSubLocation As common.UserControls.txtFinder
    Friend WithEvents lblShiftType As RadLabel
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblDate As common.Controls.MyLabel
    Friend WithEvents lblLocationDesc As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtFinder
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents lblRalNoDesc As common.Controls.MyLabel
    Friend WithEvents lblAbandonmentNo As common.Controls.MyLabel
    Friend WithEvents lblRALNo As common.Controls.MyLabel
    Friend WithEvents lblCustomerName As common.Controls.MyLabel
    Friend WithEvents txtRALNo As common.UserControls.txtFinder
    Friend WithEvents lblCustomer As common.Controls.MyLabel
    Friend WithEvents lblDocCode As common.Controls.MyLabel
    Friend WithEvents txtDocCode As common.UserControls.txtNavigator
    Friend WithEvents btnAddNew As RadButton
    Friend WithEvents RadPageViewPage5 As RadPageViewPage
    Friend WithEvents UcAttachment1 As ucAttachment
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents txtCustomerCode As common.UserControls.txtFinder
    Friend WithEvents txtRefDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtRefNo As common.Controls.MyTextBox
    Friend WithEvents lblRefdate As common.Controls.MyLabel
    Friend WithEvents lblRefNo As common.Controls.MyLabel
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnDelete As RadButton
    Friend WithEvents btnPost As RadButton
    Friend WithEvents btnSave As RadButton
    Friend WithEvents btnClose As RadButton
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents txtRemark As common.Controls.MyTextBox
    Friend WithEvents lblRemark As common.Controls.MyLabel
    Friend WithEvents RadPageViewPage2 As RadPageViewPage
    Friend WithEvents RadPageViewPage3 As RadPageViewPage
    Friend WithEvents SplitContainer3 As SplitContainer
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents rbtnTaxCalManual As common.Controls.MyRadioButton
    Friend WithEvents rbtnTaxCalAutomatic As common.Controls.MyRadioButton
    Friend WithEvents txtTaxGroup As common.UserControls.txtFinder
    Friend WithEvents RadLabel11 As common.Controls.MyLabel
    Friend WithEvents lblTaxGrpName As common.Controls.MyLabel
    Friend WithEvents gv2 As common.UserControls.MyRadGridView
    Friend WithEvents lblTaxAmt As common.Controls.MyLabel
    Friend WithEvents txtTaxAmt As common.Controls.MyLabel
    Friend WithEvents lblDocAmtWithoutTax As common.Controls.MyLabel
    Friend WithEvents txtDocAmtWithoutTax As common.Controls.MyLabel
    Friend WithEvents lblDocamt As common.Controls.MyLabel
    Friend WithEvents txtDocAmt As common.Controls.MyLabel
    Friend WithEvents gbQtySummary As GroupBox
    Friend WithEvents txtBalQty As common.Controls.MyLabel
    Friend WithEvents txtTenderQty As common.Controls.MyLabel
    Friend WithEvents lblbalQty As common.Controls.MyLabel
    Friend WithEvents lblTenderQty As common.Controls.MyLabel
    Friend WithEvents btnReverseAndUnPost As RadButton
End Class
