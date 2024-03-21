Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAssetWork
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
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.lbltaxAmount = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.lblWOBalAmount = New common.Controls.MyLabel()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.lblWOAmount = New common.Controls.MyLabel()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.txtRefDocNo = New common.UserControls.txtFinder()
        Me.RadLabel15 = New common.Controls.MyLabel()
        Me.lbl_BalAmountWithTolerenace = New common.Controls.MyLabel()
        Me.lbl_AmountWithTol = New common.Controls.MyLabel()
        Me.RadLabel14 = New common.Controls.MyLabel()
        Me.cmbRefType = New common.Controls.MyComboBox()
        Me.lblbalAmountWithTol = New common.Controls.MyLabel()
        Me.lblamountwithtol = New common.Controls.MyLabel()
        Me.lbl_BalAmount = New common.Controls.MyLabel()
        Me.lbl_Amount = New common.Controls.MyLabel()
        Me.lblBalancAmount = New common.Controls.MyLabel()
        Me.lblAmount = New common.Controls.MyLabel()
        Me.lbl_capexcode = New common.Controls.MyLabel()
        Me.lbl_capexsubcode = New common.Controls.MyLabel()
        Me.fndcapexcode = New common.UserControls.txtFinder()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.fndcapexsubcode = New common.UserControls.txtFinder()
        Me.lblcapexCode = New common.Controls.MyLabel()
        Me.lblSubCapex = New common.Controls.MyLabel()
        Me.lblAccountDesc = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtAccount = New common.UserControls.txtFinder()
        Me.ddlype = New common.Controls.MyComboBox()
        Me.lblMoveType = New common.Controls.MyLabel()
        Me.lblTotRAmt = New common.Controls.MyLabel()
        Me.lblAsset = New common.Controls.MyLabel()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtAssetCode = New common.UserControls.txtFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtFinder()
        Me.RadLabel12 = New common.Controls.MyLabel()
        Me.RadLabel6 = New common.Controls.MyLabel()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.chkOnHold = New Telerik.WinControls.UI.RadCheckBox()
        Me.lblVendorName = New common.Controls.MyLabel()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.txtRemarks = New common.Controls.MyTextBox()
        Me.txtVendorNo = New common.UserControls.txtFinder()
        Me.UsLock1 = New common.usLock()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.txtDesc = New common.Controls.MyTextBox()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbtnTaxCalManual = New common.Controls.MyRadioButton()
        Me.rbtnTaxCalAutomatic = New common.Controls.MyRadioButton()
        Me.txtTaxGroup = New common.UserControls.txtFinder()
        Me.RadLabel11 = New common.Controls.MyLabel()
        Me.lblTaxGrpName = New common.Controls.MyLabel()
        Me.RadLabel10 = New common.Controls.MyLabel()
        Me.gv2 = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage4 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.lblTDSAmount = New common.Controls.MyLabel()
        Me.RadLabel9 = New common.Controls.MyLabel()
        Me.RadLabel27 = New common.Controls.MyLabel()
        Me.lblNetAmt = New common.Controls.MyLabel()
        Me.RadLabel25 = New common.Controls.MyLabel()
        Me.lblTaxAmt = New common.Controls.MyLabel()
        Me.lblTotalAmt = New common.Controls.MyLabel()
        Me.butCostCenterAndHirerachy_Update_AfterPost = New Telerik.WinControls.UI.RadButton()
        Me.btnReverse = New Telerik.WinControls.UI.RadButton()
        Me.btnPrintJV = New Telerik.WinControls.UI.RadButton()
        Me.btnViewTDSDetails = New Telerik.WinControls.UI.RadButton()
        Me.BtnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.lbltaxAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblWOBalAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblWOAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_BalAmountWithTolerenace, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_AmountWithTol, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbRefType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblbalAmountWithTol, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblamountwithtol, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_BalAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_Amount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBalancAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_capexcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_capexsubcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblcapexCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSubCapex, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAccountDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlype, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMoveType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotRAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAsset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkOnHold, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendorName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.rbtnTaxCalManual, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnTaxCalAutomatic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTaxGrpName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage4.SuspendLayout()
        CType(Me.lblTDSAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel27, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNetAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTaxAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotalAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.butCostCenterAndHirerachy_Update_AfterPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrintJV, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnViewTDSDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SplitContainer1.IsSplitterFixed = True
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.butCostCenterAndHirerachy_Update_AfterPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReverse)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrintJV)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnViewTDSDetails)
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(1106, 461)
        Me.SplitContainer1.SplitterDistance = 431
        Me.SplitContainer1.TabIndex = 1
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage4)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1106, 431)
        Me.RadPageView1.TabIndex = 0
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.lbltaxAmount)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel7)
        Me.RadPageViewPage1.Controls.Add(Me.lblWOBalAmount)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel8)
        Me.RadPageViewPage1.Controls.Add(Me.lblWOAmount)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel6)
        Me.RadPageViewPage1.Controls.Add(Me.txtRefDocNo)
        Me.RadPageViewPage1.Controls.Add(Me.lbl_BalAmountWithTolerenace)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel15)
        Me.RadPageViewPage1.Controls.Add(Me.lbl_AmountWithTol)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel14)
        Me.RadPageViewPage1.Controls.Add(Me.cmbRefType)
        Me.RadPageViewPage1.Controls.Add(Me.lblbalAmountWithTol)
        Me.RadPageViewPage1.Controls.Add(Me.lblamountwithtol)
        Me.RadPageViewPage1.Controls.Add(Me.lbl_BalAmount)
        Me.RadPageViewPage1.Controls.Add(Me.lbl_Amount)
        Me.RadPageViewPage1.Controls.Add(Me.lblBalancAmount)
        Me.RadPageViewPage1.Controls.Add(Me.lblAmount)
        Me.RadPageViewPage1.Controls.Add(Me.lbl_capexcode)
        Me.RadPageViewPage1.Controls.Add(Me.lbl_capexsubcode)
        Me.RadPageViewPage1.Controls.Add(Me.fndcapexcode)
        Me.RadPageViewPage1.Controls.Add(Me.fndcapexsubcode)
        Me.RadPageViewPage1.Controls.Add(Me.lblcapexCode)
        Me.RadPageViewPage1.Controls.Add(Me.lblSubCapex)
        Me.RadPageViewPage1.Controls.Add(Me.lblAccountDesc)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.txtAccount)
        Me.RadPageViewPage1.Controls.Add(Me.ddlype)
        Me.RadPageViewPage1.Controls.Add(Me.lblMoveType)
        Me.RadPageViewPage1.Controls.Add(Me.lblTotRAmt)
        Me.RadPageViewPage1.Controls.Add(Me.lblAsset)
        Me.RadPageViewPage1.Controls.Add(Me.lblLocation)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.txtAssetCode)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.txtLocation)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel12)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel6)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.chkOnHold)
        Me.RadPageViewPage1.Controls.Add(Me.lblVendorName)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.btnAddNew)
        Me.RadPageViewPage1.Controls.Add(Me.txtRemarks)
        Me.RadPageViewPage1.Controls.Add(Me.txtVendorNo)
        Me.RadPageViewPage1.Controls.Add(Me.UsLock1)
        Me.RadPageViewPage1.Controls.Add(Me.txtDocNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtDate)
        Me.RadPageViewPage1.Controls.Add(Me.txtDesc)
        Me.RadPageViewPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(74.0!, 26.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1085, 385)
        Me.RadPageViewPage1.Text = "Asset Work"
        '
        'lbltaxAmount
        '
        Me.lbltaxAmount.AutoSize = False
        Me.lbltaxAmount.BorderVisible = True
        Me.lbltaxAmount.FieldName = Nothing
        Me.lbltaxAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltaxAmount.Location = New System.Drawing.Point(870, 152)
        Me.lbltaxAmount.Name = "lbltaxAmount"
        Me.lbltaxAmount.Size = New System.Drawing.Size(78, 18)
        Me.lbltaxAmount.TabIndex = 642
        Me.lbltaxAmount.TextWrap = False
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(771, 154)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(67, 16)
        Me.MyLabel7.TabIndex = 641
        Me.MyLabel7.Text = "Tax Amount"
        '
        'lblWOBalAmount
        '
        Me.lblWOBalAmount.AutoSize = False
        Me.lblWOBalAmount.BorderVisible = True
        Me.lblWOBalAmount.FieldName = Nothing
        Me.lblWOBalAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWOBalAmount.Location = New System.Drawing.Point(585, 179)
        Me.lblWOBalAmount.Name = "lblWOBalAmount"
        Me.lblWOBalAmount.Size = New System.Drawing.Size(119, 18)
        Me.lblWOBalAmount.TabIndex = 640
        Me.lblWOBalAmount.TextWrap = False
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(490, 179)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(89, 16)
        Me.MyLabel8.TabIndex = 639
        Me.MyLabel8.Text = "Balance Amount"
        '
        'lblWOAmount
        '
        Me.lblWOAmount.AutoSize = False
        Me.lblWOAmount.BorderVisible = True
        Me.lblWOAmount.FieldName = Nothing
        Me.lblWOAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWOAmount.Location = New System.Drawing.Point(584, 151)
        Me.lblWOAmount.Name = "lblWOAmount"
        Me.lblWOAmount.Size = New System.Drawing.Size(120, 18)
        Me.lblWOAmount.TabIndex = 638
        Me.lblWOAmount.TextWrap = False
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(490, 154)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(45, 16)
        Me.MyLabel6.TabIndex = 637
        Me.MyLabel6.Text = "Amount"
        '
        'txtRefDocNo
        '
        Me.txtRefDocNo.CalculationExpression = Nothing
        Me.txtRefDocNo.FieldCode = Nothing
        Me.txtRefDocNo.FieldDesc = Nothing
        Me.txtRefDocNo.FieldMaxLength = 0
        Me.txtRefDocNo.FieldName = Nothing
        Me.txtRefDocNo.isCalculatedField = False
        Me.txtRefDocNo.IsSourceFromTable = False
        Me.txtRefDocNo.IsSourceFromValueList = False
        Me.txtRefDocNo.IsUnique = False
        Me.txtRefDocNo.Location = New System.Drawing.Point(302, 153)
        Me.txtRefDocNo.MendatroryField = False
        Me.txtRefDocNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefDocNo.MyLinkLable1 = Me.RadLabel15
        Me.txtRefDocNo.MyLinkLable2 = Nothing
        Me.txtRefDocNo.MyReadOnly = False
        Me.txtRefDocNo.MyShowMasterFormButton = False
        Me.txtRefDocNo.Name = "txtRefDocNo"
        Me.txtRefDocNo.ReferenceFieldDesc = Nothing
        Me.txtRefDocNo.ReferenceFieldName = Nothing
        Me.txtRefDocNo.ReferenceTableName = Nothing
        Me.txtRefDocNo.Size = New System.Drawing.Size(177, 19)
        Me.txtRefDocNo.TabIndex = 24
        Me.txtRefDocNo.Value = ""
        '
        'RadLabel15
        '
        Me.RadLabel15.FieldName = Nothing
        Me.RadLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel15.Location = New System.Drawing.Point(230, 153)
        Me.RadLabel15.Name = "RadLabel15"
        Me.RadLabel15.Size = New System.Drawing.Size(65, 16)
        Me.RadLabel15.TabIndex = 25
        Me.RadLabel15.Text = "Ref Doc No"
        '
        'lbl_BalAmountWithTolerenace
        '
        Me.lbl_BalAmountWithTolerenace.AutoSize = False
        Me.lbl_BalAmountWithTolerenace.BorderVisible = True
        Me.lbl_BalAmountWithTolerenace.FieldName = Nothing
        Me.lbl_BalAmountWithTolerenace.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_BalAmountWithTolerenace.Location = New System.Drawing.Point(870, 123)
        Me.lbl_BalAmountWithTolerenace.Name = "lbl_BalAmountWithTolerenace"
        Me.lbl_BalAmountWithTolerenace.Size = New System.Drawing.Size(78, 18)
        Me.lbl_BalAmountWithTolerenace.TabIndex = 636
        Me.lbl_BalAmountWithTolerenace.TextWrap = False
        '
        'lbl_AmountWithTol
        '
        Me.lbl_AmountWithTol.AutoSize = False
        Me.lbl_AmountWithTol.BorderVisible = True
        Me.lbl_AmountWithTol.FieldName = Nothing
        Me.lbl_AmountWithTol.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_AmountWithTol.Location = New System.Drawing.Point(870, 99)
        Me.lbl_AmountWithTol.Name = "lbl_AmountWithTol"
        Me.lbl_AmountWithTol.Size = New System.Drawing.Size(78, 18)
        Me.lbl_AmountWithTol.TabIndex = 635
        Me.lbl_AmountWithTol.TextWrap = False
        '
        'RadLabel14
        '
        Me.RadLabel14.FieldName = Nothing
        Me.RadLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel14.Location = New System.Drawing.Point(3, 153)
        Me.RadLabel14.Name = "RadLabel14"
        Me.RadLabel14.Size = New System.Drawing.Size(72, 16)
        Me.RadLabel14.TabIndex = 26
        Me.RadLabel14.Text = "Ref DocType"
        '
        'cmbRefType
        '
        Me.cmbRefType.AllowDrop = True
        Me.cmbRefType.AutoCompleteDisplayMember = Nothing
        Me.cmbRefType.AutoCompleteValueMember = Nothing
        Me.cmbRefType.CalculationExpression = Nothing
        Me.cmbRefType.DropDownAnimationEnabled = True
        Me.cmbRefType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbRefType.FieldCode = Nothing
        Me.cmbRefType.FieldDesc = Nothing
        Me.cmbRefType.FieldMaxLength = 0
        Me.cmbRefType.FieldName = Nothing
        Me.cmbRefType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbRefType.isCalculatedField = False
        Me.cmbRefType.IsSourceFromTable = False
        Me.cmbRefType.IsSourceFromValueList = False
        Me.cmbRefType.IsUnique = False
        Me.cmbRefType.Location = New System.Drawing.Point(83, 151)
        Me.cmbRefType.MendatroryField = False
        Me.cmbRefType.MyLinkLable1 = Me.RadLabel14
        Me.cmbRefType.MyLinkLable2 = Nothing
        Me.cmbRefType.Name = "cmbRefType"
        Me.cmbRefType.ReferenceFieldDesc = Nothing
        Me.cmbRefType.ReferenceFieldName = Nothing
        Me.cmbRefType.ReferenceTableName = Nothing
        Me.cmbRefType.Size = New System.Drawing.Size(142, 18)
        Me.cmbRefType.TabIndex = 23
        Me.cmbRefType.Text = "RadDropDownList1"
        '
        'lblbalAmountWithTol
        '
        Me.lblbalAmountWithTol.FieldName = Nothing
        Me.lblbalAmountWithTol.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblbalAmountWithTol.Location = New System.Drawing.Point(714, 123)
        Me.lblbalAmountWithTol.Name = "lblbalAmountWithTol"
        Me.lblbalAmountWithTol.Size = New System.Drawing.Size(147, 16)
        Me.lblbalAmountWithTol.TabIndex = 634
        Me.lblbalAmountWithTol.Text = "Bal. Amount With Tolerence"
        '
        'lblamountwithtol
        '
        Me.lblamountwithtol.FieldName = Nothing
        Me.lblamountwithtol.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblamountwithtol.Location = New System.Drawing.Point(714, 97)
        Me.lblamountwithtol.Name = "lblamountwithtol"
        Me.lblamountwithtol.Size = New System.Drawing.Size(125, 16)
        Me.lblamountwithtol.TabIndex = 633
        Me.lblamountwithtol.Text = "Amount With Tolerence"
        '
        'lbl_BalAmount
        '
        Me.lbl_BalAmount.AutoSize = False
        Me.lbl_BalAmount.BorderVisible = True
        Me.lbl_BalAmount.FieldName = Nothing
        Me.lbl_BalAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_BalAmount.Location = New System.Drawing.Point(584, 123)
        Me.lbl_BalAmount.Name = "lbl_BalAmount"
        Me.lbl_BalAmount.Size = New System.Drawing.Size(120, 18)
        Me.lbl_BalAmount.TabIndex = 632
        Me.lbl_BalAmount.TextWrap = False
        '
        'lbl_Amount
        '
        Me.lbl_Amount.AutoSize = False
        Me.lbl_Amount.BorderVisible = True
        Me.lbl_Amount.FieldName = Nothing
        Me.lbl_Amount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Amount.Location = New System.Drawing.Point(584, 99)
        Me.lbl_Amount.Name = "lbl_Amount"
        Me.lbl_Amount.Size = New System.Drawing.Size(120, 18)
        Me.lbl_Amount.TabIndex = 631
        Me.lbl_Amount.TextWrap = False
        '
        'lblBalancAmount
        '
        Me.lblBalancAmount.FieldName = Nothing
        Me.lblBalancAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBalancAmount.Location = New System.Drawing.Point(490, 125)
        Me.lblBalancAmount.Name = "lblBalancAmount"
        Me.lblBalancAmount.Size = New System.Drawing.Size(68, 16)
        Me.lblBalancAmount.TabIndex = 630
        Me.lblBalancAmount.Text = "Bal. Amount"
        '
        'lblAmount
        '
        Me.lblAmount.FieldName = Nothing
        Me.lblAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmount.Location = New System.Drawing.Point(490, 99)
        Me.lblAmount.Name = "lblAmount"
        Me.lblAmount.Size = New System.Drawing.Size(45, 16)
        Me.lblAmount.TabIndex = 629
        Me.lblAmount.Text = "Amount"
        '
        'lbl_capexcode
        '
        Me.lbl_capexcode.AutoSize = False
        Me.lbl_capexcode.BorderVisible = True
        Me.lbl_capexcode.FieldName = Nothing
        Me.lbl_capexcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_capexcode.Location = New System.Drawing.Point(227, 125)
        Me.lbl_capexcode.Name = "lbl_capexcode"
        Me.lbl_capexcode.Size = New System.Drawing.Size(253, 18)
        Me.lbl_capexcode.TabIndex = 628
        Me.lbl_capexcode.TextWrap = False
        '
        'lbl_capexsubcode
        '
        Me.lbl_capexsubcode.AutoSize = False
        Me.lbl_capexsubcode.BorderVisible = True
        Me.lbl_capexsubcode.FieldName = Nothing
        Me.lbl_capexsubcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_capexsubcode.Location = New System.Drawing.Point(227, 97)
        Me.lbl_capexsubcode.Name = "lbl_capexsubcode"
        Me.lbl_capexsubcode.Size = New System.Drawing.Size(253, 18)
        Me.lbl_capexsubcode.TabIndex = 627
        Me.lbl_capexsubcode.TextWrap = False
        '
        'fndcapexcode
        '
        Me.fndcapexcode.CalculationExpression = Nothing
        Me.fndcapexcode.Enabled = False
        Me.fndcapexcode.FieldCode = Nothing
        Me.fndcapexcode.FieldDesc = Nothing
        Me.fndcapexcode.FieldMaxLength = 0
        Me.fndcapexcode.FieldName = Nothing
        Me.fndcapexcode.isCalculatedField = False
        Me.fndcapexcode.IsSourceFromTable = False
        Me.fndcapexcode.IsSourceFromValueList = False
        Me.fndcapexcode.IsUnique = False
        Me.fndcapexcode.Location = New System.Drawing.Point(83, 126)
        Me.fndcapexcode.MendatroryField = True
        Me.fndcapexcode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndcapexcode.MyLinkLable1 = Me.MyLabel4
        Me.fndcapexcode.MyLinkLable2 = Nothing
        Me.fndcapexcode.MyReadOnly = False
        Me.fndcapexcode.MyShowMasterFormButton = False
        Me.fndcapexcode.Name = "fndcapexcode"
        Me.fndcapexcode.ReferenceFieldDesc = Nothing
        Me.fndcapexcode.ReferenceFieldName = Nothing
        Me.fndcapexcode.ReferenceTableName = Nothing
        Me.fndcapexcode.Size = New System.Drawing.Size(143, 19)
        Me.fndcapexcode.TabIndex = 626
        Me.fndcapexcode.Value = ""
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(143, 72)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(71, 16)
        Me.MyLabel4.TabIndex = 128
        Me.MyLabel4.Text = "TDS Amount"
        '
        'fndcapexsubcode
        '
        Me.fndcapexsubcode.CalculationExpression = Nothing
        Me.fndcapexsubcode.Enabled = False
        Me.fndcapexsubcode.FieldCode = Nothing
        Me.fndcapexsubcode.FieldDesc = Nothing
        Me.fndcapexsubcode.FieldMaxLength = 0
        Me.fndcapexsubcode.FieldName = Nothing
        Me.fndcapexsubcode.isCalculatedField = False
        Me.fndcapexsubcode.IsSourceFromTable = False
        Me.fndcapexsubcode.IsSourceFromValueList = False
        Me.fndcapexsubcode.IsUnique = False
        Me.fndcapexsubcode.Location = New System.Drawing.Point(83, 98)
        Me.fndcapexsubcode.MendatroryField = True
        Me.fndcapexsubcode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndcapexsubcode.MyLinkLable1 = Me.MyLabel4
        Me.fndcapexsubcode.MyLinkLable2 = Nothing
        Me.fndcapexsubcode.MyReadOnly = False
        Me.fndcapexsubcode.MyShowMasterFormButton = False
        Me.fndcapexsubcode.Name = "fndcapexsubcode"
        Me.fndcapexsubcode.ReferenceFieldDesc = Nothing
        Me.fndcapexsubcode.ReferenceFieldName = Nothing
        Me.fndcapexsubcode.ReferenceTableName = Nothing
        Me.fndcapexsubcode.Size = New System.Drawing.Size(143, 19)
        Me.fndcapexsubcode.TabIndex = 625
        Me.fndcapexsubcode.Value = ""
        '
        'lblcapexCode
        '
        Me.lblcapexCode.FieldName = Nothing
        Me.lblcapexCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcapexCode.Location = New System.Drawing.Point(3, 127)
        Me.lblcapexCode.Name = "lblcapexCode"
        Me.lblcapexCode.Size = New System.Drawing.Size(69, 16)
        Me.lblcapexCode.TabIndex = 624
        Me.lblcapexCode.Text = "Capex Code"
        '
        'lblSubCapex
        '
        Me.lblSubCapex.FieldName = Nothing
        Me.lblSubCapex.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubCapex.Location = New System.Drawing.Point(3, 100)
        Me.lblSubCapex.Name = "lblSubCapex"
        Me.lblSubCapex.Size = New System.Drawing.Size(62, 16)
        Me.lblSubCapex.TabIndex = 623
        Me.lblSubCapex.Text = "Sub Capex"
        '
        'lblAccountDesc
        '
        Me.lblAccountDesc.AutoSize = False
        Me.lblAccountDesc.BorderVisible = True
        Me.lblAccountDesc.FieldName = Nothing
        Me.lblAccountDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAccountDesc.Location = New System.Drawing.Point(808, 65)
        Me.lblAccountDesc.Name = "lblAccountDesc"
        Me.lblAccountDesc.Size = New System.Drawing.Size(137, 20)
        Me.lblAccountDesc.TabIndex = 621
        Me.lblAccountDesc.TextWrap = False
        Me.lblAccountDesc.Visible = False
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(607, 69)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(47, 16)
        Me.MyLabel5.TabIndex = 622
        Me.MyLabel5.Text = "Account"
        Me.MyLabel5.Visible = False
        '
        'txtAccount
        '
        Me.txtAccount.CalculationExpression = Nothing
        Me.txtAccount.FieldCode = Nothing
        Me.txtAccount.FieldDesc = Nothing
        Me.txtAccount.FieldMaxLength = 0
        Me.txtAccount.FieldName = Nothing
        Me.txtAccount.isCalculatedField = False
        Me.txtAccount.IsSourceFromTable = False
        Me.txtAccount.IsSourceFromValueList = False
        Me.txtAccount.IsUnique = False
        Me.txtAccount.Location = New System.Drawing.Point(664, 66)
        Me.txtAccount.MendatroryField = False
        Me.txtAccount.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAccount.MyLinkLable1 = Me.MyLabel5
        Me.txtAccount.MyLinkLable2 = Me.lblAccountDesc
        Me.txtAccount.MyReadOnly = False
        Me.txtAccount.MyShowMasterFormButton = False
        Me.txtAccount.Name = "txtAccount"
        Me.txtAccount.ReferenceFieldDesc = Nothing
        Me.txtAccount.ReferenceFieldName = Nothing
        Me.txtAccount.ReferenceTableName = Nothing
        Me.txtAccount.Size = New System.Drawing.Size(139, 20)
        Me.txtAccount.TabIndex = 620
        Me.txtAccount.Value = ""
        Me.txtAccount.Visible = False
        '
        'ddlype
        '
        Me.ddlype.CalculationExpression = Nothing
        Me.ddlype.DropDownAnimationEnabled = True
        Me.ddlype.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlype.FieldCode = Nothing
        Me.ddlype.FieldDesc = Nothing
        Me.ddlype.FieldMaxLength = 0
        Me.ddlype.FieldName = Nothing
        Me.ddlype.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddlype.isCalculatedField = False
        Me.ddlype.IsSourceFromTable = False
        Me.ddlype.IsSourceFromValueList = False
        Me.ddlype.IsUnique = False
        RadListDataItem1.Text = "Vendor"
        RadListDataItem2.Text = "Other"
        Me.ddlype.Items.Add(RadListDataItem1)
        Me.ddlype.Items.Add(RadListDataItem2)
        Me.ddlype.Location = New System.Drawing.Point(606, 2)
        Me.ddlype.MendatroryField = False
        Me.ddlype.MyLinkLable1 = Nothing
        Me.ddlype.MyLinkLable2 = Nothing
        Me.ddlype.Name = "ddlype"
        Me.ddlype.ReferenceFieldDesc = Nothing
        Me.ddlype.ReferenceFieldName = Nothing
        Me.ddlype.ReferenceTableName = Nothing
        Me.ddlype.Size = New System.Drawing.Size(143, 18)
        Me.ddlype.TabIndex = 618
        '
        'lblMoveType
        '
        Me.lblMoveType.FieldName = Nothing
        Me.lblMoveType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMoveType.Location = New System.Drawing.Point(564, 3)
        Me.lblMoveType.Name = "lblMoveType"
        Me.lblMoveType.Size = New System.Drawing.Size(31, 16)
        Me.lblMoveType.TabIndex = 619
        Me.lblMoveType.Text = "Type"
        '
        'lblTotRAmt
        '
        Me.lblTotRAmt.AutoSize = False
        Me.lblTotRAmt.BorderVisible = True
        Me.lblTotRAmt.FieldName = Nothing
        Me.lblTotRAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotRAmt.Location = New System.Drawing.Point(485, 69)
        Me.lblTotRAmt.Name = "lblTotRAmt"
        Me.lblTotRAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblTotRAmt.TabIndex = 126
        Me.lblTotRAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTotRAmt.Visible = False
        '
        'lblAsset
        '
        Me.lblAsset.AutoSize = False
        Me.lblAsset.BorderVisible = True
        Me.lblAsset.FieldName = Nothing
        Me.lblAsset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAsset.Location = New System.Drawing.Point(227, 22)
        Me.lblAsset.Name = "lblAsset"
        Me.lblAsset.Size = New System.Drawing.Size(253, 20)
        Me.lblAsset.TabIndex = 30
        Me.lblAsset.TextWrap = False
        '
        'lblLocation
        '
        Me.lblLocation.AutoSize = False
        Me.lblLocation.BorderVisible = True
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(227, 67)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(253, 20)
        Me.lblLocation.TabIndex = 27
        Me.lblLocation.TextWrap = False
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(0, 24)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(35, 16)
        Me.MyLabel3.TabIndex = 31
        Me.MyLabel3.Text = "Asset"
        '
        'txtAssetCode
        '
        Me.txtAssetCode.CalculationExpression = Nothing
        Me.txtAssetCode.FieldCode = Nothing
        Me.txtAssetCode.FieldDesc = Nothing
        Me.txtAssetCode.FieldMaxLength = 0
        Me.txtAssetCode.FieldName = Nothing
        Me.txtAssetCode.isCalculatedField = False
        Me.txtAssetCode.IsSourceFromTable = False
        Me.txtAssetCode.IsSourceFromValueList = False
        Me.txtAssetCode.IsUnique = False
        Me.txtAssetCode.Location = New System.Drawing.Point(83, 22)
        Me.txtAssetCode.MendatroryField = True
        Me.txtAssetCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAssetCode.MyLinkLable1 = Me.MyLabel3
        Me.txtAssetCode.MyLinkLable2 = Me.lblAsset
        Me.txtAssetCode.MyReadOnly = False
        Me.txtAssetCode.MyShowMasterFormButton = False
        Me.txtAssetCode.Name = "txtAssetCode"
        Me.txtAssetCode.ReferenceFieldDesc = Nothing
        Me.txtAssetCode.ReferenceFieldName = Nothing
        Me.txtAssetCode.ReferenceTableName = Nothing
        Me.txtAssetCode.Size = New System.Drawing.Size(143, 20)
        Me.txtAssetCode.TabIndex = 29
        Me.txtAssetCode.Value = ""
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(1, 69)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(49, 16)
        Me.MyLabel2.TabIndex = 28
        Me.MyLabel2.Text = "Location"
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
        Me.txtLocation.Location = New System.Drawing.Point(83, 68)
        Me.txtLocation.MendatroryField = True
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.MyLabel2
        Me.txtLocation.MyLinkLable2 = Me.lblLocation
        Me.txtLocation.MyReadOnly = False
        Me.txtLocation.MyShowMasterFormButton = False
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.ReferenceFieldDesc = Nothing
        Me.txtLocation.ReferenceFieldName = Nothing
        Me.txtLocation.ReferenceTableName = Nothing
        Me.txtLocation.Size = New System.Drawing.Size(143, 19)
        Me.txtLocation.TabIndex = 26
        Me.txtLocation.Value = ""
        '
        'RadLabel12
        '
        Me.RadLabel12.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadLabel12.FieldName = Nothing
        Me.RadLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel12.ForeColor = System.Drawing.SystemColors.Highlight
        Me.RadLabel12.Location = New System.Drawing.Point(695, 369)
        Me.RadLabel12.Name = "RadLabel12"
        Me.RadLabel12.Size = New System.Drawing.Size(388, 16)
        Me.RadLabel12.TabIndex = 25
        Me.RadLabel12.Text = "Double click on Tax Amount Column To Set Addition charges wise Tax"
        '
        'RadLabel6
        '
        Me.RadLabel6.FieldName = Nothing
        Me.RadLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel6.Location = New System.Drawing.Point(490, 24)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(51, 16)
        Me.RadLabel6.TabIndex = 13
        Me.RadLabel6.Text = "Remarks"
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(490, 46)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel3.TabIndex = 14
        Me.RadLabel3.Text = "Description"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox2.Controls.Add(Me.gv1)
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "Additional Charges"
        Me.RadGroupBox2.Location = New System.Drawing.Point(2, 204)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(1080, 156)
        Me.RadGroupBox2.TabIndex = 15
        Me.RadGroupBox2.Text = "Additional Charges"
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
        Me.gv1.MasterTemplate.ShowFilteringRow = False
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(1060, 126)
        Me.gv1.TabIndex = 0
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(367, 2)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 18
        Me.RadLabel4.Text = "Date"
        '
        'chkOnHold
        '
        Me.chkOnHold.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOnHold.Location = New System.Drawing.Point(486, 2)
        Me.chkOnHold.Name = "chkOnHold"
        '
        '
        '
        Me.chkOnHold.RootElement.StretchHorizontally = True
        Me.chkOnHold.RootElement.StretchVertically = True
        Me.chkOnHold.Size = New System.Drawing.Size(62, 16)
        Me.chkOnHold.TabIndex = 3
        Me.chkOnHold.Text = "On Hold"
        '
        'lblVendorName
        '
        Me.lblVendorName.AutoSize = False
        Me.lblVendorName.BorderVisible = True
        Me.lblVendorName.FieldName = Nothing
        Me.lblVendorName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVendorName.Location = New System.Drawing.Point(226, 44)
        Me.lblVendorName.Name = "lblVendorName"
        Me.lblVendorName.Size = New System.Drawing.Size(253, 20)
        Me.lblVendorName.TabIndex = 18
        Me.lblVendorName.TextWrap = False
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(0, 46)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(60, 16)
        Me.RadLabel2.TabIndex = 21
        Me.RadLabel2.Text = "Vendor No"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(0, 2)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(75, 16)
        Me.RadLabel1.TabIndex = 22
        Me.RadLabel1.Text = "Document No"
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.XpertERPFixedAssets.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(346, 1)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 18)
        Me.btnAddNew.TabIndex = 0
        '
        'txtRemarks
        '
        Me.txtRemarks.CalculationExpression = Nothing
        Me.txtRemarks.FieldCode = Nothing
        Me.txtRemarks.FieldDesc = Nothing
        Me.txtRemarks.FieldMaxLength = 0
        Me.txtRemarks.FieldName = Nothing
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.isCalculatedField = False
        Me.txtRemarks.IsSourceFromTable = False
        Me.txtRemarks.IsSourceFromValueList = False
        Me.txtRemarks.IsUnique = False
        Me.txtRemarks.Location = New System.Drawing.Point(561, 23)
        Me.txtRemarks.MaxLength = 200
        Me.txtRemarks.MendatroryField = False
        Me.txtRemarks.MyLinkLable1 = Me.RadLabel6
        Me.txtRemarks.MyLinkLable2 = Nothing
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.ReferenceFieldDesc = Nothing
        Me.txtRemarks.ReferenceFieldName = Nothing
        Me.txtRemarks.ReferenceTableName = Nothing
        Me.txtRemarks.Size = New System.Drawing.Size(387, 18)
        Me.txtRemarks.TabIndex = 7
        '
        'txtVendorNo
        '
        Me.txtVendorNo.CalculationExpression = Nothing
        Me.txtVendorNo.FieldCode = Nothing
        Me.txtVendorNo.FieldDesc = Nothing
        Me.txtVendorNo.FieldMaxLength = 0
        Me.txtVendorNo.FieldName = Nothing
        Me.txtVendorNo.isCalculatedField = False
        Me.txtVendorNo.IsSourceFromTable = False
        Me.txtVendorNo.IsSourceFromValueList = False
        Me.txtVendorNo.IsUnique = False
        Me.txtVendorNo.Location = New System.Drawing.Point(82, 45)
        Me.txtVendorNo.MendatroryField = False
        Me.txtVendorNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVendorNo.MyLinkLable1 = Me.RadLabel2
        Me.txtVendorNo.MyLinkLable2 = Me.lblVendorName
        Me.txtVendorNo.MyReadOnly = False
        Me.txtVendorNo.MyShowMasterFormButton = False
        Me.txtVendorNo.Name = "txtVendorNo"
        Me.txtVendorNo.ReferenceFieldDesc = Nothing
        Me.txtVendorNo.ReferenceFieldName = Nothing
        Me.txtVendorNo.ReferenceTableName = Nothing
        Me.txtVendorNo.Size = New System.Drawing.Size(143, 19)
        Me.txtVendorNo.TabIndex = 4
        Me.txtVendorNo.Value = ""
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(857, -3)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(88, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 11
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(82, 1)
        Me.txtDocNo.MendatroryField = False
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Me.RadLabel1
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 30
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(263, 18)
        Me.txtDocNo.TabIndex = 0
        Me.txtDocNo.Value = ""
        '
        'txtDate
        '
        Me.txtDate.CalculationExpression = Nothing
        Me.txtDate.CustomFormat = "dd/MM/yyyy"
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
        Me.txtDate.Location = New System.Drawing.Point(402, 1)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.RadLabel4
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(77, 18)
        Me.txtDate.TabIndex = 1
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'txtDesc
        '
        Me.txtDesc.CalculationExpression = Nothing
        Me.txtDesc.FieldCode = Nothing
        Me.txtDesc.FieldDesc = Nothing
        Me.txtDesc.FieldMaxLength = 0
        Me.txtDesc.FieldName = Nothing
        Me.txtDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc.isCalculatedField = False
        Me.txtDesc.IsSourceFromTable = False
        Me.txtDesc.IsSourceFromValueList = False
        Me.txtDesc.IsUnique = False
        Me.txtDesc.Location = New System.Drawing.Point(561, 45)
        Me.txtDesc.MaxLength = 200
        Me.txtDesc.MendatroryField = False
        Me.txtDesc.MyLinkLable1 = Me.RadLabel3
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ReferenceFieldDesc = Nothing
        Me.txtDesc.ReferenceFieldName = Nothing
        Me.txtDesc.ReferenceTableName = Nothing
        Me.txtDesc.Size = New System.Drawing.Size(387, 18)
        Me.txtDesc.TabIndex = 5
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.GroupBox1)
        Me.RadPageViewPage2.Controls.Add(Me.txtTaxGroup)
        Me.RadPageViewPage2.Controls.Add(Me.lblTaxGrpName)
        Me.RadPageViewPage2.Controls.Add(Me.RadLabel10)
        Me.RadPageViewPage2.Controls.Add(Me.gv2)
        Me.RadPageViewPage2.Controls.Add(Me.RadLabel11)
        Me.RadPageViewPage2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(47.0!, 26.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(948, 385)
        Me.RadPageViewPage2.Text = "Taxes"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbtnTaxCalManual)
        Me.GroupBox1.Controls.Add(Me.rbtnTaxCalAutomatic)
        Me.GroupBox1.Location = New System.Drawing.Point(547, -2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(163, 36)
        Me.GroupBox1.TabIndex = 15
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Tax Calculation Type"
        Me.GroupBox1.Visible = False
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
        Me.txtTaxGroup.Location = New System.Drawing.Point(69, 3)
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
        Me.txtTaxGroup.TabIndex = 5
        Me.txtTaxGroup.Value = ""
        '
        'RadLabel11
        '
        Me.RadLabel11.FieldName = Nothing
        Me.RadLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel11.Location = New System.Drawing.Point(3, 4)
        Me.RadLabel11.Name = "RadLabel11"
        Me.RadLabel11.Size = New System.Drawing.Size(60, 16)
        Me.RadLabel11.TabIndex = 2
        Me.RadLabel11.Text = "Tax Group"
        '
        'lblTaxGrpName
        '
        Me.lblTaxGrpName.AutoSize = False
        Me.lblTaxGrpName.BorderVisible = True
        Me.lblTaxGrpName.FieldName = Nothing
        Me.lblTaxGrpName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTaxGrpName.Location = New System.Drawing.Point(220, 3)
        Me.lblTaxGrpName.Name = "lblTaxGrpName"
        Me.lblTaxGrpName.Size = New System.Drawing.Size(321, 19)
        Me.lblTaxGrpName.TabIndex = 3
        Me.lblTaxGrpName.TextWrap = False
        '
        'RadLabel10
        '
        Me.RadLabel10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadLabel10.FieldName = Nothing
        Me.RadLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel10.ForeColor = System.Drawing.SystemColors.Highlight
        Me.RadLabel10.Location = New System.Drawing.Point(790, 281)
        Me.RadLabel10.Name = "RadLabel10"
        Me.RadLabel10.Size = New System.Drawing.Size(155, 16)
        Me.RadLabel10.TabIndex = 4
        Me.RadLabel10.Text = "Double click To Chage Rate"
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
        Me.gv2.Location = New System.Drawing.Point(2, 34)
        '
        '
        '
        Me.gv2.MasterTemplate.AllowAddNewRow = False
        Me.gv2.MasterTemplate.AllowDeleteRow = False
        Me.gv2.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv2.MasterTemplate.ShowFilteringRow = False
        Me.gv2.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv2.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gv2.MyStopExport = False
        Me.gv2.Name = "gv2"
        Me.gv2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv2.ShowHeaderCellButtons = True
        Me.gv2.Size = New System.Drawing.Size(943, 243)
        Me.gv2.TabIndex = 1
        '
        'RadPageViewPage4
        '
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel4)
        Me.RadPageViewPage4.Controls.Add(Me.lblTDSAmount)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel9)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel27)
        Me.RadPageViewPage4.Controls.Add(Me.lblNetAmt)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel25)
        Me.RadPageViewPage4.Controls.Add(Me.lblTaxAmt)
        Me.RadPageViewPage4.Controls.Add(Me.lblTotalAmt)
        Me.RadPageViewPage4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage4.ItemSize = New System.Drawing.SizeF(41.0!, 26.0!)
        Me.RadPageViewPage4.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage4.Name = "RadPageViewPage4"
        Me.RadPageViewPage4.Size = New System.Drawing.Size(948, 385)
        Me.RadPageViewPage4.Text = "Total"
        '
        'lblTDSAmount
        '
        Me.lblTDSAmount.AutoSize = False
        Me.lblTDSAmount.BorderVisible = True
        Me.lblTDSAmount.FieldName = Nothing
        Me.lblTDSAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTDSAmount.Location = New System.Drawing.Point(224, 72)
        Me.lblTDSAmount.Name = "lblTDSAmount"
        Me.lblTDSAmount.Size = New System.Drawing.Size(110, 18)
        Me.lblTDSAmount.TabIndex = 127
        Me.lblTDSAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadLabel9
        '
        Me.RadLabel9.FieldName = Nothing
        Me.RadLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel9.Location = New System.Drawing.Point(169, 24)
        Me.RadLabel9.Name = "RadLabel9"
        Me.RadLabel9.Size = New System.Drawing.Size(45, 16)
        Me.RadLabel9.TabIndex = 120
        Me.RadLabel9.Text = "Amount"
        '
        'RadLabel27
        '
        Me.RadLabel27.FieldName = Nothing
        Me.RadLabel27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel27.Location = New System.Drawing.Point(114, 98)
        Me.RadLabel27.Name = "RadLabel27"
        Me.RadLabel27.Size = New System.Drawing.Size(100, 16)
        Me.RadLabel27.TabIndex = 123
        Me.RadLabel27.Text = "Document Amount"
        '
        'lblNetAmt
        '
        Me.lblNetAmt.AutoSize = False
        Me.lblNetAmt.BorderVisible = True
        Me.lblNetAmt.FieldName = Nothing
        Me.lblNetAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNetAmt.Location = New System.Drawing.Point(224, 96)
        Me.lblNetAmt.Name = "lblNetAmt"
        Me.lblNetAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblNetAmt.TabIndex = 125
        Me.lblNetAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadLabel25
        '
        Me.RadLabel25.FieldName = Nothing
        Me.RadLabel25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel25.Location = New System.Drawing.Point(137, 48)
        Me.RadLabel25.Name = "RadLabel25"
        Me.RadLabel25.Size = New System.Drawing.Size(77, 16)
        Me.RadLabel25.TabIndex = 122
        Me.RadLabel25.Text = "+ Tax Amount"
        '
        'lblTaxAmt
        '
        Me.lblTaxAmt.AutoSize = False
        Me.lblTaxAmt.BorderVisible = True
        Me.lblTaxAmt.FieldName = Nothing
        Me.lblTaxAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTaxAmt.Location = New System.Drawing.Point(224, 48)
        Me.lblTaxAmt.Name = "lblTaxAmt"
        Me.lblTaxAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblTaxAmt.TabIndex = 124
        Me.lblTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotalAmt
        '
        Me.lblTotalAmt.AutoSize = False
        Me.lblTotalAmt.BorderVisible = True
        Me.lblTotalAmt.FieldName = Nothing
        Me.lblTotalAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalAmt.Location = New System.Drawing.Point(224, 24)
        Me.lblTotalAmt.Name = "lblTotalAmt"
        Me.lblTotalAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblTotalAmt.TabIndex = 123
        Me.lblTotalAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'butCostCenterAndHirerachy_Update_AfterPost
        '
        Me.butCostCenterAndHirerachy_Update_AfterPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butCostCenterAndHirerachy_Update_AfterPost.Location = New System.Drawing.Point(562, 2)
        Me.butCostCenterAndHirerachy_Update_AfterPost.Name = "butCostCenterAndHirerachy_Update_AfterPost"
        Me.butCostCenterAndHirerachy_Update_AfterPost.Size = New System.Drawing.Size(186, 22)
        Me.butCostCenterAndHirerachy_Update_AfterPost.TabIndex = 142
        Me.butCostCenterAndHirerachy_Update_AfterPost.Text = "Update Cost Center And Hirerachy"
        '
        'btnReverse
        '
        Me.btnReverse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverse.Location = New System.Drawing.Point(437, 2)
        Me.btnReverse.Name = "btnReverse"
        Me.btnReverse.Size = New System.Drawing.Size(119, 22)
        Me.btnReverse.TabIndex = 138
        Me.btnReverse.Text = "Revese and Unpost"
        Me.btnReverse.Visible = False
        '
        'btnPrintJV
        '
        Me.btnPrintJV.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrintJV.Location = New System.Drawing.Point(365, 2)
        Me.btnPrintJV.Name = "btnPrintJV"
        Me.btnPrintJV.Size = New System.Drawing.Size(69, 22)
        Me.btnPrintJV.TabIndex = 8
        Me.btnPrintJV.Text = "Print JV"
        '
        'btnViewTDSDetails
        '
        Me.btnViewTDSDetails.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnViewTDSDetails.Location = New System.Drawing.Point(293, 2)
        Me.btnViewTDSDetails.Name = "btnViewTDSDetails"
        Me.btnViewTDSDetails.Size = New System.Drawing.Size(69, 22)
        Me.btnViewTDSDetails.TabIndex = 7
        Me.btnViewTDSDetails.Text = "View TDS"
        '
        'BtnPrint
        '
        Me.BtnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPrint.Location = New System.Drawing.Point(221, 2)
        Me.BtnPrint.Name = "BtnPrint"
        Me.BtnPrint.Size = New System.Drawing.Size(69, 22)
        Me.BtnPrint.TabIndex = 6
        Me.BtnPrint.Text = "Print"
        Me.BtnPrint.Visible = False
        '
        'btnDelete
        '
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(149, 2)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 22)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "Delete"
        '
        'btnPost
        '
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(77, 2)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(69, 22)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(1031, 2)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(5, 2)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'frmAssetWork
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1106, 461)
        Me.Controls.Add(Me.SplitContainer1)
        Me.MinimumSize = New System.Drawing.Size(890, 467)
        Me.Name = "frmAssetWork"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Asset Work"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.lbltaxAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblWOBalAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblWOAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_BalAmountWithTolerenace, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_AmountWithTol, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbRefType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblbalAmountWithTol, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblamountwithtol, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_BalAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_Amount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBalancAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_capexcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_capexsubcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblcapexCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSubCapex, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAccountDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlype, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMoveType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotRAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAsset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkOnHold, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendorName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.RadPageViewPage2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.rbtnTaxCalManual, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnTaxCalAutomatic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTaxGrpName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage4.ResumeLayout(False)
        Me.RadPageViewPage4.PerformLayout()
        CType(Me.lblTDSAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel27, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNetAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTaxAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotalAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.butCostCenterAndHirerachy_Update_AfterPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrintJV, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnViewTDSDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadLabel12 As common.Controls.MyLabel
    Friend WithEvents RadLabel6 As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents chkOnHold As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents lblVendorName As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtRemarks As common.Controls.MyTextBox
    Friend WithEvents txtVendorNo As common.UserControls.txtFinder
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtDesc As common.Controls.MyTextBox
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rbtnTaxCalManual As common.Controls.MyRadioButton
    Friend WithEvents rbtnTaxCalAutomatic As common.Controls.MyRadioButton
    Friend WithEvents txtTaxGroup As common.UserControls.txtFinder
    Friend WithEvents RadLabel11 As common.Controls.MyLabel
    Friend WithEvents lblTaxGrpName As common.Controls.MyLabel
    Friend WithEvents RadLabel10 As common.Controls.MyLabel
    Friend WithEvents gv2 As common.UserControls.MyRadGridView
    Friend WithEvents RadPageViewPage4 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadLabel9 As common.Controls.MyLabel
    Friend WithEvents RadLabel27 As common.Controls.MyLabel
    Friend WithEvents lblNetAmt As common.Controls.MyLabel
    Friend WithEvents RadLabel25 As common.Controls.MyLabel
    Friend WithEvents lblTaxAmt As common.Controls.MyLabel
    Friend WithEvents lblTotalAmt As common.Controls.MyLabel
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtFinder
    Friend WithEvents lblAsset As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtAssetCode As common.UserControls.txtFinder
    Friend WithEvents BtnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnViewTDSDetails As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblTotRAmt As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents lblTDSAmount As common.Controls.MyLabel
    Friend WithEvents ddlype As common.Controls.MyComboBox
    Friend WithEvents lblMoveType As common.Controls.MyLabel
    Friend WithEvents lblAccountDesc As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtAccount As common.UserControls.txtFinder
    Friend WithEvents lbl_BalAmountWithTolerenace As common.Controls.MyLabel
    Friend WithEvents lbl_AmountWithTol As common.Controls.MyLabel
    Friend WithEvents lblbalAmountWithTol As common.Controls.MyLabel
    Friend WithEvents lblamountwithtol As common.Controls.MyLabel
    Friend WithEvents lbl_BalAmount As common.Controls.MyLabel
    Friend WithEvents lbl_Amount As common.Controls.MyLabel
    Friend WithEvents lblBalancAmount As common.Controls.MyLabel
    Friend WithEvents lblAmount As common.Controls.MyLabel
    Friend WithEvents lbl_capexcode As common.Controls.MyLabel
    Friend WithEvents lbl_capexsubcode As common.Controls.MyLabel
    Friend WithEvents fndcapexcode As common.UserControls.txtFinder
    Friend WithEvents fndcapexsubcode As common.UserControls.txtFinder
    Friend WithEvents lblcapexCode As common.Controls.MyLabel
    Friend WithEvents lblSubCapex As common.Controls.MyLabel
    Friend WithEvents txtRefDocNo As common.UserControls.txtFinder
    Friend WithEvents RadLabel15 As common.Controls.MyLabel
    Friend WithEvents RadLabel14 As common.Controls.MyLabel
    Friend WithEvents cmbRefType As common.Controls.MyComboBox
    Friend WithEvents lblWOBalAmount As common.Controls.MyLabel
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents lblWOAmount As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents btnPrintJV As Telerik.WinControls.UI.RadButton
    Friend WithEvents lbltaxAmount As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents btnReverse As Telerik.WinControls.UI.RadButton
    Friend WithEvents butCostCenterAndHirerachy_Update_AfterPost As Telerik.WinControls.UI.RadButton
End Class

