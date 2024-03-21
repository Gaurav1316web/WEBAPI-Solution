Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAcquisionEntry
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
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition3 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition4 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.lblSRNDate = New common.Controls.MyLabel()
        Me.lblPIDate = New common.Controls.MyLabel()
        Me.chkOpeningDirect = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkSameDesSpecStartDate = New Telerik.WinControls.UI.RadCheckBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtAssembleOpeningAmt = New common.Controls.MyTextBox()
        Me.chkOpening = New Telerik.WinControls.UI.RadCheckBox()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.dtpPostingDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtAssembleCode = New common.Controls.MyTextBox()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.MyLabel37 = New common.Controls.MyLabel()
        Me.MyLabel38 = New common.Controls.MyLabel()
        Me.MyLabel40 = New common.Controls.MyLabel()
        Me.lbl_rebudgetamtwithtolerence = New common.Controls.MyLabel()
        Me.lbl_rebudgetamt = New common.Controls.MyLabel()
        Me.MyLabel35 = New common.Controls.MyLabel()
        Me.lbl_budgetamtwithtolerence = New common.Controls.MyLabel()
        Me.lbl_budgetamt = New common.Controls.MyLabel()
        Me.lbl_capexcode = New common.Controls.MyLabel()
        Me.lblcaption1 = New common.Controls.MyLabel()
        Me.fndcapexcode = New common.UserControls.txtFinder()
        Me.lbl_capexsubcode = New common.Controls.MyLabel()
        Me.lblcaption2 = New common.Controls.MyLabel()
        Me.fndcapexsubcode = New common.UserControls.txtFinder()
        Me.lblPINo = New common.Controls.MyLabel()
        Me.fndPINo = New common.UserControls.txtFinder()
        Me.ddlAcqType = New common.Controls.MyComboBox()
        Me.lblMoveType = New common.Controls.MyLabel()
        Me.ChkISAssemble = New Telerik.WinControls.UI.RadCheckBox()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.lblTemplateDesc = New common.Controls.MyLabel()
        Me.lblTemplate = New common.Controls.MyLabel()
        Me.fndTemplateCode = New common.UserControls.txtFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtVendorInvoiceNo = New common.Controls.MyTextBox()
        Me.rbtnnew = New common.Controls.MyRadioButton()
        Me.rbtnold = New common.Controls.MyRadioButton()
        Me.chkVisiType = New Telerik.WinControls.UI.RadCheckBox()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtFinder()
        Me.RadLabel23 = New common.Controls.MyLabel()
        Me.RadLabel12 = New common.Controls.MyLabel()
        Me.RadLabel6 = New common.Controls.MyLabel()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.chkOnHold = New Telerik.WinControls.UI.RadCheckBox()
        Me.lblVendorName = New common.Controls.MyLabel()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.txtRemarks = New common.Controls.MyTextBox()
        Me.txtSRNNo = New common.UserControls.txtFinder()
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
        Me.RadPageViewPage5 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvAC = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage4 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.lblAssetAmount = New common.Controls.MyLabel()
        Me.txtAssetAmount = New common.Controls.MyLabel()
        Me.lblTaxRecAmt = New common.Controls.MyLabel()
        Me.lblTaxNonRecAmt = New common.Controls.MyLabel()
        Me.txtNonRecAmt = New common.Controls.MyLabel()
        Me.txtRecAmt = New common.Controls.MyLabel()
        Me.RadLabel31 = New common.Controls.MyLabel()
        Me.lblAddCharges = New common.Controls.MyLabel()
        Me.RadLabel9 = New common.Controls.MyLabel()
        Me.RadLabel27 = New common.Controls.MyLabel()
        Me.lblNetAmt = New common.Controls.MyLabel()
        Me.RadLabel25 = New common.Controls.MyLabel()
        Me.lblTaxAmt = New common.Controls.MyLabel()
        Me.lblTotalAmt = New common.Controls.MyLabel()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvAssemble = New common.UserControls.MyRadGridView()
        Me.butCostCenterAndHirerachy_Update_AfterPost = New Telerik.WinControls.UI.RadButton()
        Me.btnShowInventory = New Telerik.WinControls.UI.RadButton()
        Me.btnReverse = New Telerik.WinControls.UI.RadButton()
        Me.btnUnSelect = New Telerik.WinControls.UI.RadButton()
        Me.btnTDSDetail = New Telerik.WinControls.UI.RadButton()
        Me.btnChangeDepDetail = New Telerik.WinControls.UI.RadButton()
        Me.BtnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.lblSRNDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPIDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkOpeningDirect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSameDesSpecStartDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAssembleOpeningAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkOpening, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpPostingDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAssembleCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel37, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel38, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel40, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_rebudgetamtwithtolerence, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_rebudgetamt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel35, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_budgetamtwithtolerence, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_budgetamt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_capexcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblcaption1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_capexsubcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblcaption2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPINo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlAcqType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMoveType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkISAssemble, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTemplateDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVendorInvoiceNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnold, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkVisiType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.RadPageViewPage5.SuspendLayout()
        CType(Me.gvAC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvAC.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage4.SuspendLayout()
        CType(Me.lblAssetAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAssetAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTaxRecAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTaxNonRecAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNonRecAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRecAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel31, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAddCharges, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel27, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNetAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTaxAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotalAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage3.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.gvAssemble, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvAssemble.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.butCostCenterAndHirerachy_Update_AfterPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnShowInventory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUnSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnTDSDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnChangeDepDetail, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.butCostCenterAndHirerachy_Update_AfterPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnShowInventory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReverse)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnUnSelect)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnTDSDetail)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnChangeDepDetail)
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(1050, 509)
        Me.SplitContainer1.SplitterDistance = 479
        Me.SplitContainer1.TabIndex = 1
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1050, 20)
        Me.RadMenu1.TabIndex = 2
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2, Me.RadMenuItem3, Me.rmSaveLayout, Me.rmDeleteLayout})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Export"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Import"
        '
        'rmSaveLayout
        '
        Me.rmSaveLayout.Name = "rmSaveLayout"
        Me.rmSaveLayout.Text = "Save Layout"
        '
        'rmDeleteLayout
        '
        Me.rmDeleteLayout.Name = "rmDeleteLayout"
        Me.rmDeleteLayout.Text = "Delete Layout"
        '
        'RadPageView1
        '
        Me.RadPageView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage5)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage4)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageView1.Location = New System.Drawing.Point(5, 26)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1050, 454)
        Me.RadPageView1.TabIndex = 0
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.lblSRNDate)
        Me.RadPageViewPage1.Controls.Add(Me.lblPIDate)
        Me.RadPageViewPage1.Controls.Add(Me.chkOpeningDirect)
        Me.RadPageViewPage1.Controls.Add(Me.chkSameDesSpecStartDate)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.txtAssembleOpeningAmt)
        Me.RadPageViewPage1.Controls.Add(Me.chkOpening)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.dtpPostingDate)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.txtAssembleCode)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel37)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel38)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel40)
        Me.RadPageViewPage1.Controls.Add(Me.lbl_rebudgetamtwithtolerence)
        Me.RadPageViewPage1.Controls.Add(Me.lbl_rebudgetamt)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel35)
        Me.RadPageViewPage1.Controls.Add(Me.lbl_budgetamtwithtolerence)
        Me.RadPageViewPage1.Controls.Add(Me.lbl_budgetamt)
        Me.RadPageViewPage1.Controls.Add(Me.lbl_capexcode)
        Me.RadPageViewPage1.Controls.Add(Me.lblcaption1)
        Me.RadPageViewPage1.Controls.Add(Me.fndcapexcode)
        Me.RadPageViewPage1.Controls.Add(Me.lbl_capexsubcode)
        Me.RadPageViewPage1.Controls.Add(Me.lblcaption2)
        Me.RadPageViewPage1.Controls.Add(Me.fndcapexsubcode)
        Me.RadPageViewPage1.Controls.Add(Me.lblPINo)
        Me.RadPageViewPage1.Controls.Add(Me.fndPINo)
        Me.RadPageViewPage1.Controls.Add(Me.ddlAcqType)
        Me.RadPageViewPage1.Controls.Add(Me.lblMoveType)
        Me.RadPageViewPage1.Controls.Add(Me.ChkISAssemble)
        Me.RadPageViewPage1.Controls.Add(Me.btnGo)
        Me.RadPageViewPage1.Controls.Add(Me.lblTemplateDesc)
        Me.RadPageViewPage1.Controls.Add(Me.lblTemplate)
        Me.RadPageViewPage1.Controls.Add(Me.fndTemplateCode)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.txtVendorInvoiceNo)
        Me.RadPageViewPage1.Controls.Add(Me.rbtnnew)
        Me.RadPageViewPage1.Controls.Add(Me.rbtnold)
        Me.RadPageViewPage1.Controls.Add(Me.chkVisiType)
        Me.RadPageViewPage1.Controls.Add(Me.lblLocation)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.txtLocation)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel23)
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
        Me.RadPageViewPage1.Controls.Add(Me.txtSRNNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtVendorNo)
        Me.RadPageViewPage1.Controls.Add(Me.UsLock1)
        Me.RadPageViewPage1.Controls.Add(Me.txtDocNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtDate)
        Me.RadPageViewPage1.Controls.Add(Me.txtDesc)
        Me.RadPageViewPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(101.0!, 26.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1029, 408)
        Me.RadPageViewPage1.Text = "Acquisition Entry"
        '
        'lblSRNDate
        '
        Me.lblSRNDate.AutoSize = False
        Me.lblSRNDate.BorderVisible = True
        Me.lblSRNDate.FieldName = Nothing
        Me.lblSRNDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSRNDate.Location = New System.Drawing.Point(734, 22)
        Me.lblSRNDate.Name = "lblSRNDate"
        Me.lblSRNDate.Size = New System.Drawing.Size(70, 19)
        Me.lblSRNDate.TabIndex = 673
        Me.lblSRNDate.TextWrap = False
        Me.lblSRNDate.Visible = False
        '
        'lblPIDate
        '
        Me.lblPIDate.AutoSize = False
        Me.lblPIDate.BorderVisible = True
        Me.lblPIDate.FieldName = Nothing
        Me.lblPIDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPIDate.Location = New System.Drawing.Point(734, 1)
        Me.lblPIDate.Name = "lblPIDate"
        Me.lblPIDate.Size = New System.Drawing.Size(70, 19)
        Me.lblPIDate.TabIndex = 672
        Me.lblPIDate.TextWrap = False
        Me.lblPIDate.Visible = False
        '
        'chkOpeningDirect
        '
        Me.chkOpeningDirect.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOpeningDirect.Location = New System.Drawing.Point(175, 110)
        Me.chkOpeningDirect.Name = "chkOpeningDirect"
        '
        '
        '
        Me.chkOpeningDirect.RootElement.StretchHorizontally = True
        Me.chkOpeningDirect.RootElement.StretchVertically = True
        Me.chkOpeningDirect.Size = New System.Drawing.Size(107, 16)
        Me.chkOpeningDirect.TabIndex = 671
        Me.chkOpeningDirect.Text = "Opening Direct"
        '
        'chkSameDesSpecStartDate
        '
        Me.chkSameDesSpecStartDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSameDesSpecStartDate.Location = New System.Drawing.Point(501, 110)
        Me.chkSameDesSpecStartDate.Name = "chkSameDesSpecStartDate"
        '
        '
        '
        Me.chkSameDesSpecStartDate.RootElement.StretchHorizontally = True
        Me.chkSameDesSpecStartDate.RootElement.StretchVertically = True
        Me.chkSameDesSpecStartDate.Size = New System.Drawing.Size(461, 16)
        Me.chkSameDesSpecStartDate.TabIndex = 670
        Me.chkSameDesSpecStartDate.Text = "Fill Same Description,Specification and Start Date for same item"
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(290, 111)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(45, 16)
        Me.MyLabel4.TabIndex = 669
        Me.MyLabel4.Text = "Amount"
        '
        'txtAssembleOpeningAmt
        '
        Me.txtAssembleOpeningAmt.CalculationExpression = Nothing
        Me.txtAssembleOpeningAmt.FieldCode = Nothing
        Me.txtAssembleOpeningAmt.FieldDesc = Nothing
        Me.txtAssembleOpeningAmt.FieldMaxLength = 0
        Me.txtAssembleOpeningAmt.FieldName = Nothing
        Me.txtAssembleOpeningAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAssembleOpeningAmt.isCalculatedField = False
        Me.txtAssembleOpeningAmt.IsSourceFromTable = False
        Me.txtAssembleOpeningAmt.IsSourceFromValueList = False
        Me.txtAssembleOpeningAmt.IsUnique = False
        Me.txtAssembleOpeningAmt.Location = New System.Drawing.Point(347, 109)
        Me.txtAssembleOpeningAmt.MaxLength = 200
        Me.txtAssembleOpeningAmt.MendatroryField = False
        Me.txtAssembleOpeningAmt.MyLinkLable1 = Me.MyLabel4
        Me.txtAssembleOpeningAmt.MyLinkLable2 = Nothing
        Me.txtAssembleOpeningAmt.Name = "txtAssembleOpeningAmt"
        Me.txtAssembleOpeningAmt.ReferenceFieldDesc = Nothing
        Me.txtAssembleOpeningAmt.ReferenceFieldName = Nothing
        Me.txtAssembleOpeningAmt.ReferenceTableName = Nothing
        Me.txtAssembleOpeningAmt.Size = New System.Drawing.Size(151, 18)
        Me.txtAssembleOpeningAmt.TabIndex = 668
        '
        'chkOpening
        '
        Me.chkOpening.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOpening.Location = New System.Drawing.Point(102, 110)
        Me.chkOpening.Name = "chkOpening"
        '
        '
        '
        Me.chkOpening.RootElement.StretchHorizontally = True
        Me.chkOpening.RootElement.StretchVertically = True
        Me.chkOpening.Size = New System.Drawing.Size(67, 16)
        Me.chkOpening.TabIndex = 667
        Me.chkOpening.Text = "Opening"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(501, 90)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(71, 16)
        Me.MyLabel5.TabIndex = 666
        Me.MyLabel5.Text = "Posting Date"
        '
        'dtpPostingDate
        '
        Me.dtpPostingDate.CalculationExpression = Nothing
        Me.dtpPostingDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpPostingDate.FieldCode = Nothing
        Me.dtpPostingDate.FieldDesc = Nothing
        Me.dtpPostingDate.FieldMaxLength = 0
        Me.dtpPostingDate.FieldName = Nothing
        Me.dtpPostingDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpPostingDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpPostingDate.isCalculatedField = False
        Me.dtpPostingDate.IsSourceFromTable = False
        Me.dtpPostingDate.IsSourceFromValueList = False
        Me.dtpPostingDate.IsUnique = False
        Me.dtpPostingDate.Location = New System.Drawing.Point(573, 89)
        Me.dtpPostingDate.MendatroryField = False
        Me.dtpPostingDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpPostingDate.MyLinkLable1 = Me.MyLabel5
        Me.dtpPostingDate.MyLinkLable2 = Nothing
        Me.dtpPostingDate.Name = "dtpPostingDate"
        Me.dtpPostingDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpPostingDate.ReadOnly = True
        Me.dtpPostingDate.ReferenceFieldDesc = Nothing
        Me.dtpPostingDate.ReferenceFieldName = Nothing
        Me.dtpPostingDate.ReferenceTableName = Nothing
        Me.dtpPostingDate.Size = New System.Drawing.Size(92, 18)
        Me.dtpPostingDate.TabIndex = 665
        Me.dtpPostingDate.TabStop = False
        Me.dtpPostingDate.Text = "13/06/2011"
        Me.dtpPostingDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(249, 90)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(86, 16)
        Me.MyLabel3.TabIndex = 664
        Me.MyLabel3.Text = "Assemble Code"
        '
        'txtAssembleCode
        '
        Me.txtAssembleCode.CalculationExpression = Nothing
        Me.txtAssembleCode.FieldCode = Nothing
        Me.txtAssembleCode.FieldDesc = Nothing
        Me.txtAssembleCode.FieldMaxLength = 0
        Me.txtAssembleCode.FieldName = Nothing
        Me.txtAssembleCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAssembleCode.isCalculatedField = False
        Me.txtAssembleCode.IsSourceFromTable = False
        Me.txtAssembleCode.IsSourceFromValueList = False
        Me.txtAssembleCode.IsUnique = False
        Me.txtAssembleCode.Location = New System.Drawing.Point(347, 89)
        Me.txtAssembleCode.MaxLength = 200
        Me.txtAssembleCode.MendatroryField = False
        Me.txtAssembleCode.MyLinkLable1 = Me.RadLabel3
        Me.txtAssembleCode.MyLinkLable2 = Nothing
        Me.txtAssembleCode.Name = "txtAssembleCode"
        Me.txtAssembleCode.ReadOnly = True
        Me.txtAssembleCode.ReferenceFieldDesc = Nothing
        Me.txtAssembleCode.ReferenceFieldName = Nothing
        Me.txtAssembleCode.ReferenceTableName = Nothing
        Me.txtAssembleCode.Size = New System.Drawing.Size(151, 18)
        Me.txtAssembleCode.TabIndex = 663
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(501, 68)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel3.TabIndex = 14
        Me.RadLabel3.Text = "Description"
        '
        'MyLabel37
        '
        Me.MyLabel37.FieldName = Nothing
        Me.MyLabel37.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel37.Location = New System.Drawing.Point(706, 154)
        Me.MyLabel37.Name = "MyLabel37"
        Me.MyLabel37.Size = New System.Drawing.Size(147, 16)
        Me.MyLabel37.TabIndex = 661
        Me.MyLabel37.Text = "Bal. Amount With Tolerence"
        Me.MyLabel37.Visible = False
        '
        'MyLabel38
        '
        Me.MyLabel38.FieldName = Nothing
        Me.MyLabel38.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel38.Location = New System.Drawing.Point(706, 131)
        Me.MyLabel38.Name = "MyLabel38"
        Me.MyLabel38.Size = New System.Drawing.Size(125, 16)
        Me.MyLabel38.TabIndex = 657
        Me.MyLabel38.Text = "Amount With Tolerence"
        Me.MyLabel38.Visible = False
        '
        'MyLabel40
        '
        Me.MyLabel40.FieldName = Nothing
        Me.MyLabel40.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel40.Location = New System.Drawing.Point(501, 154)
        Me.MyLabel40.Name = "MyLabel40"
        Me.MyLabel40.Size = New System.Drawing.Size(68, 16)
        Me.MyLabel40.TabIndex = 659
        Me.MyLabel40.Text = "Bal. Amount"
        Me.MyLabel40.Visible = False
        '
        'lbl_rebudgetamtwithtolerence
        '
        Me.lbl_rebudgetamtwithtolerence.AutoSize = False
        Me.lbl_rebudgetamtwithtolerence.BorderVisible = True
        Me.lbl_rebudgetamtwithtolerence.FieldName = Nothing
        Me.lbl_rebudgetamtwithtolerence.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_rebudgetamtwithtolerence.Location = New System.Drawing.Point(858, 153)
        Me.lbl_rebudgetamtwithtolerence.Name = "lbl_rebudgetamtwithtolerence"
        Me.lbl_rebudgetamtwithtolerence.Size = New System.Drawing.Size(140, 19)
        Me.lbl_rebudgetamtwithtolerence.TabIndex = 662
        Me.lbl_rebudgetamtwithtolerence.TextWrap = False
        Me.lbl_rebudgetamtwithtolerence.Visible = False
        '
        'lbl_rebudgetamt
        '
        Me.lbl_rebudgetamt.AutoSize = False
        Me.lbl_rebudgetamt.BorderVisible = True
        Me.lbl_rebudgetamt.FieldName = Nothing
        Me.lbl_rebudgetamt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_rebudgetamt.Location = New System.Drawing.Point(573, 153)
        Me.lbl_rebudgetamt.Name = "lbl_rebudgetamt"
        Me.lbl_rebudgetamt.Size = New System.Drawing.Size(127, 19)
        Me.lbl_rebudgetamt.TabIndex = 660
        Me.lbl_rebudgetamt.TextWrap = False
        Me.lbl_rebudgetamt.Visible = False
        '
        'MyLabel35
        '
        Me.MyLabel35.FieldName = Nothing
        Me.MyLabel35.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel35.Location = New System.Drawing.Point(501, 131)
        Me.MyLabel35.Name = "MyLabel35"
        Me.MyLabel35.Size = New System.Drawing.Size(45, 16)
        Me.MyLabel35.TabIndex = 655
        Me.MyLabel35.Text = "Amount"
        Me.MyLabel35.Visible = False
        '
        'lbl_budgetamtwithtolerence
        '
        Me.lbl_budgetamtwithtolerence.AutoSize = False
        Me.lbl_budgetamtwithtolerence.BorderVisible = True
        Me.lbl_budgetamtwithtolerence.FieldName = Nothing
        Me.lbl_budgetamtwithtolerence.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_budgetamtwithtolerence.Location = New System.Drawing.Point(858, 130)
        Me.lbl_budgetamtwithtolerence.Name = "lbl_budgetamtwithtolerence"
        Me.lbl_budgetamtwithtolerence.Size = New System.Drawing.Size(141, 19)
        Me.lbl_budgetamtwithtolerence.TabIndex = 658
        Me.lbl_budgetamtwithtolerence.TextWrap = False
        Me.lbl_budgetamtwithtolerence.Visible = False
        '
        'lbl_budgetamt
        '
        Me.lbl_budgetamt.AutoSize = False
        Me.lbl_budgetamt.BorderVisible = True
        Me.lbl_budgetamt.FieldName = Nothing
        Me.lbl_budgetamt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_budgetamt.Location = New System.Drawing.Point(573, 130)
        Me.lbl_budgetamt.Name = "lbl_budgetamt"
        Me.lbl_budgetamt.Size = New System.Drawing.Size(128, 19)
        Me.lbl_budgetamt.TabIndex = 656
        Me.lbl_budgetamt.TextWrap = False
        Me.lbl_budgetamt.Visible = False
        '
        'lbl_capexcode
        '
        Me.lbl_capexcode.AutoSize = False
        Me.lbl_capexcode.BorderVisible = True
        Me.lbl_capexcode.FieldName = Nothing
        Me.lbl_capexcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_capexcode.Location = New System.Drawing.Point(251, 151)
        Me.lbl_capexcode.Name = "lbl_capexcode"
        Me.lbl_capexcode.Size = New System.Drawing.Size(247, 22)
        Me.lbl_capexcode.TabIndex = 653
        Me.lbl_capexcode.TextWrap = False
        Me.lbl_capexcode.Visible = False
        '
        'lblcaption1
        '
        Me.lblcaption1.FieldName = Nothing
        Me.lblcaption1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcaption1.Location = New System.Drawing.Point(4, 154)
        Me.lblcaption1.Name = "lblcaption1"
        Me.lblcaption1.Size = New System.Drawing.Size(69, 16)
        Me.lblcaption1.TabIndex = 654
        Me.lblcaption1.Text = "Capex Code"
        Me.lblcaption1.Visible = False
        '
        'fndcapexcode
        '
        Me.fndcapexcode.CalculationExpression = Nothing
        Me.fndcapexcode.FieldCode = Nothing
        Me.fndcapexcode.FieldDesc = Nothing
        Me.fndcapexcode.FieldMaxLength = 0
        Me.fndcapexcode.FieldName = Nothing
        Me.fndcapexcode.isCalculatedField = False
        Me.fndcapexcode.IsSourceFromTable = False
        Me.fndcapexcode.IsSourceFromValueList = False
        Me.fndcapexcode.IsUnique = False
        Me.fndcapexcode.Location = New System.Drawing.Point(102, 152)
        Me.fndcapexcode.MendatroryField = True
        Me.fndcapexcode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndcapexcode.MyLinkLable1 = Me.lblcaption1
        Me.fndcapexcode.MyLinkLable2 = Me.lbl_capexcode
        Me.fndcapexcode.MyReadOnly = False
        Me.fndcapexcode.MyShowMasterFormButton = False
        Me.fndcapexcode.Name = "fndcapexcode"
        Me.fndcapexcode.ReferenceFieldDesc = Nothing
        Me.fndcapexcode.ReferenceFieldName = Nothing
        Me.fndcapexcode.ReferenceTableName = Nothing
        Me.fndcapexcode.Size = New System.Drawing.Size(143, 20)
        Me.fndcapexcode.TabIndex = 652
        Me.fndcapexcode.Value = ""
        Me.fndcapexcode.Visible = False
        '
        'lbl_capexsubcode
        '
        Me.lbl_capexsubcode.AutoSize = False
        Me.lbl_capexsubcode.BorderVisible = True
        Me.lbl_capexsubcode.FieldName = Nothing
        Me.lbl_capexsubcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_capexsubcode.Location = New System.Drawing.Point(251, 128)
        Me.lbl_capexsubcode.Name = "lbl_capexsubcode"
        Me.lbl_capexsubcode.Size = New System.Drawing.Size(247, 22)
        Me.lbl_capexsubcode.TabIndex = 650
        Me.lbl_capexsubcode.TextWrap = False
        Me.lbl_capexsubcode.Visible = False
        '
        'lblcaption2
        '
        Me.lblcaption2.FieldName = Nothing
        Me.lblcaption2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcaption2.Location = New System.Drawing.Point(4, 131)
        Me.lblcaption2.Name = "lblcaption2"
        Me.lblcaption2.Size = New System.Drawing.Size(62, 16)
        Me.lblcaption2.TabIndex = 651
        Me.lblcaption2.Text = "Sub Capex"
        Me.lblcaption2.Visible = False
        '
        'fndcapexsubcode
        '
        Me.fndcapexsubcode.CalculationExpression = Nothing
        Me.fndcapexsubcode.FieldCode = Nothing
        Me.fndcapexsubcode.FieldDesc = Nothing
        Me.fndcapexsubcode.FieldMaxLength = 0
        Me.fndcapexsubcode.FieldName = Nothing
        Me.fndcapexsubcode.isCalculatedField = False
        Me.fndcapexsubcode.IsSourceFromTable = False
        Me.fndcapexsubcode.IsSourceFromValueList = False
        Me.fndcapexsubcode.IsUnique = False
        Me.fndcapexsubcode.Location = New System.Drawing.Point(102, 129)
        Me.fndcapexsubcode.MendatroryField = True
        Me.fndcapexsubcode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndcapexsubcode.MyLinkLable1 = Me.lblcaption2
        Me.fndcapexsubcode.MyLinkLable2 = Me.lbl_capexsubcode
        Me.fndcapexsubcode.MyReadOnly = False
        Me.fndcapexsubcode.MyShowMasterFormButton = False
        Me.fndcapexsubcode.Name = "fndcapexsubcode"
        Me.fndcapexsubcode.ReferenceFieldDesc = Nothing
        Me.fndcapexsubcode.ReferenceFieldName = Nothing
        Me.fndcapexsubcode.ReferenceTableName = Nothing
        Me.fndcapexsubcode.Size = New System.Drawing.Size(143, 20)
        Me.fndcapexsubcode.TabIndex = 649
        Me.fndcapexsubcode.Value = ""
        Me.fndcapexsubcode.Visible = False
        '
        'lblPINo
        '
        Me.lblPINo.FieldName = Nothing
        Me.lblPINo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPINo.Location = New System.Drawing.Point(501, 2)
        Me.lblPINo.Name = "lblPINo"
        Me.lblPINo.Size = New System.Drawing.Size(34, 16)
        Me.lblPINo.TabIndex = 619
        Me.lblPINo.Text = "PI No"
        '
        'fndPINo
        '
        Me.fndPINo.CalculationExpression = Nothing
        Me.fndPINo.FieldCode = Nothing
        Me.fndPINo.FieldDesc = Nothing
        Me.fndPINo.FieldMaxLength = 0
        Me.fndPINo.FieldName = Nothing
        Me.fndPINo.isCalculatedField = False
        Me.fndPINo.IsSourceFromTable = False
        Me.fndPINo.IsSourceFromValueList = False
        Me.fndPINo.IsUnique = False
        Me.fndPINo.Location = New System.Drawing.Point(573, 0)
        Me.fndPINo.MendatroryField = False
        Me.fndPINo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndPINo.MyLinkLable1 = Me.lblPINo
        Me.fndPINo.MyLinkLable2 = Nothing
        Me.fndPINo.MyReadOnly = True
        Me.fndPINo.MyShowMasterFormButton = False
        Me.fndPINo.Name = "fndPINo"
        Me.fndPINo.ReferenceFieldDesc = Nothing
        Me.fndPINo.ReferenceFieldName = Nothing
        Me.fndPINo.ReferenceTableName = Nothing
        Me.fndPINo.Size = New System.Drawing.Size(160, 20)
        Me.fndPINo.TabIndex = 618
        Me.fndPINo.Value = ""
        '
        'ddlAcqType
        '
        Me.ddlAcqType.CalculationExpression = Nothing
        Me.ddlAcqType.DropDownAnimationEnabled = True
        Me.ddlAcqType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlAcqType.FieldCode = Nothing
        Me.ddlAcqType.FieldDesc = Nothing
        Me.ddlAcqType.FieldMaxLength = 0
        Me.ddlAcqType.FieldName = Nothing
        Me.ddlAcqType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddlAcqType.isCalculatedField = False
        Me.ddlAcqType.IsSourceFromTable = False
        Me.ddlAcqType.IsSourceFromValueList = False
        Me.ddlAcqType.IsUnique = False
        RadListDataItem1.Text = "Direct"
        RadListDataItem2.Text = "Asset"
        RadListDataItem3.Text = "Assembled"
        Me.ddlAcqType.Items.Add(RadListDataItem1)
        Me.ddlAcqType.Items.Add(RadListDataItem2)
        Me.ddlAcqType.Items.Add(RadListDataItem3)
        Me.ddlAcqType.Location = New System.Drawing.Point(102, 89)
        Me.ddlAcqType.MendatroryField = False
        Me.ddlAcqType.MyLinkLable1 = Nothing
        Me.ddlAcqType.MyLinkLable2 = Nothing
        Me.ddlAcqType.Name = "ddlAcqType"
        Me.ddlAcqType.ReferenceFieldDesc = Nothing
        Me.ddlAcqType.ReferenceFieldName = Nothing
        Me.ddlAcqType.ReferenceTableName = Nothing
        Me.ddlAcqType.Size = New System.Drawing.Size(143, 18)
        Me.ddlAcqType.TabIndex = 616
        '
        'lblMoveType
        '
        Me.lblMoveType.FieldName = Nothing
        Me.lblMoveType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMoveType.Location = New System.Drawing.Point(4, 90)
        Me.lblMoveType.Name = "lblMoveType"
        Me.lblMoveType.Size = New System.Drawing.Size(57, 16)
        Me.lblMoveType.TabIndex = 617
        Me.lblMoveType.Text = "Acq. Type"
        '
        'ChkISAssemble
        '
        Me.ChkISAssemble.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkISAssemble.Location = New System.Drawing.Point(697, 90)
        Me.ChkISAssemble.Name = "ChkISAssemble"
        '
        '
        '
        Me.ChkISAssemble.RootElement.StretchHorizontally = True
        Me.ChkISAssemble.RootElement.StretchVertically = True
        Me.ChkISAssemble.Size = New System.Drawing.Size(90, 16)
        Me.ChkISAssemble.TabIndex = 38
        Me.ChkISAssemble.Text = "Is Assemble"
        Me.ChkISAssemble.Visible = False
        '
        'btnGo
        '
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(955, 87)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(74, 22)
        Me.btnGo.TabIndex = 37
        Me.btnGo.Text = ">>"
        '
        'lblTemplateDesc
        '
        Me.lblTemplateDesc.AutoSize = False
        Me.lblTemplateDesc.BorderVisible = True
        Me.lblTemplateDesc.FieldName = Nothing
        Me.lblTemplateDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTemplateDesc.Location = New System.Drawing.Point(251, 66)
        Me.lblTemplateDesc.Name = "lblTemplateDesc"
        Me.lblTemplateDesc.Size = New System.Drawing.Size(247, 20)
        Me.lblTemplateDesc.TabIndex = 35
        Me.lblTemplateDesc.TextWrap = False
        '
        'lblTemplate
        '
        Me.lblTemplate.FieldName = Nothing
        Me.lblTemplate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTemplate.Location = New System.Drawing.Point(4, 68)
        Me.lblTemplate.Name = "lblTemplate"
        Me.lblTemplate.Size = New System.Drawing.Size(53, 16)
        Me.lblTemplate.TabIndex = 36
        Me.lblTemplate.Text = "Template"
        '
        'fndTemplateCode
        '
        Me.fndTemplateCode.CalculationExpression = Nothing
        Me.fndTemplateCode.FieldCode = Nothing
        Me.fndTemplateCode.FieldDesc = Nothing
        Me.fndTemplateCode.FieldMaxLength = 0
        Me.fndTemplateCode.FieldName = Nothing
        Me.fndTemplateCode.isCalculatedField = False
        Me.fndTemplateCode.IsSourceFromTable = False
        Me.fndTemplateCode.IsSourceFromValueList = False
        Me.fndTemplateCode.IsUnique = False
        Me.fndTemplateCode.Location = New System.Drawing.Point(102, 66)
        Me.fndTemplateCode.MendatroryField = True
        Me.fndTemplateCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndTemplateCode.MyLinkLable1 = Me.lblTemplate
        Me.fndTemplateCode.MyLinkLable2 = Me.lblTemplateDesc
        Me.fndTemplateCode.MyReadOnly = False
        Me.fndTemplateCode.MyShowMasterFormButton = False
        Me.fndTemplateCode.Name = "fndTemplateCode"
        Me.fndTemplateCode.ReferenceFieldDesc = Nothing
        Me.fndTemplateCode.ReferenceFieldName = Nothing
        Me.fndTemplateCode.ReferenceTableName = Nothing
        Me.fndTemplateCode.Size = New System.Drawing.Size(143, 20)
        Me.fndTemplateCode.TabIndex = 34
        Me.fndTemplateCode.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(807, 23)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(99, 16)
        Me.MyLabel1.TabIndex = 33
        Me.MyLabel1.Text = "Vendor Invoice No"
        '
        'txtVendorInvoiceNo
        '
        Me.txtVendorInvoiceNo.CalculationExpression = Nothing
        Me.txtVendorInvoiceNo.FieldCode = Nothing
        Me.txtVendorInvoiceNo.FieldDesc = Nothing
        Me.txtVendorInvoiceNo.FieldMaxLength = 0
        Me.txtVendorInvoiceNo.FieldName = Nothing
        Me.txtVendorInvoiceNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVendorInvoiceNo.isCalculatedField = False
        Me.txtVendorInvoiceNo.IsSourceFromTable = False
        Me.txtVendorInvoiceNo.IsSourceFromValueList = False
        Me.txtVendorInvoiceNo.IsUnique = False
        Me.txtVendorInvoiceNo.Location = New System.Drawing.Point(911, 22)
        Me.txtVendorInvoiceNo.MaxLength = 30
        Me.txtVendorInvoiceNo.MendatroryField = False
        Me.txtVendorInvoiceNo.MyLinkLable1 = Me.MyLabel1
        Me.txtVendorInvoiceNo.MyLinkLable2 = Nothing
        Me.txtVendorInvoiceNo.Name = "txtVendorInvoiceNo"
        Me.txtVendorInvoiceNo.ReferenceFieldDesc = Nothing
        Me.txtVendorInvoiceNo.ReferenceFieldName = Nothing
        Me.txtVendorInvoiceNo.ReferenceTableName = Nothing
        Me.txtVendorInvoiceNo.Size = New System.Drawing.Size(121, 18)
        Me.txtVendorInvoiceNo.TabIndex = 10
        '
        'rbtnnew
        '
        Me.rbtnnew.Location = New System.Drawing.Point(838, 89)
        Me.rbtnnew.MyLinkLable1 = Nothing
        Me.rbtnnew.MyLinkLable2 = Nothing
        Me.rbtnnew.Name = "rbtnnew"
        Me.rbtnnew.Size = New System.Drawing.Size(43, 18)
        Me.rbtnnew.TabIndex = 12
        Me.rbtnnew.Text = "New"
        Me.rbtnnew.Visible = False
        '
        'rbtnold
        '
        Me.rbtnold.Location = New System.Drawing.Point(793, 89)
        Me.rbtnold.MyLinkLable1 = Nothing
        Me.rbtnold.MyLinkLable2 = Nothing
        Me.rbtnold.Name = "rbtnold"
        Me.rbtnold.Size = New System.Drawing.Size(38, 18)
        Me.rbtnold.TabIndex = 11
        Me.rbtnold.Text = "Old"
        Me.rbtnold.Visible = False
        '
        'chkVisiType
        '
        Me.chkVisiType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkVisiType.Location = New System.Drawing.Point(870, 2)
        Me.chkVisiType.Name = "chkVisiType"
        '
        '
        '
        Me.chkVisiType.RootElement.StretchHorizontally = True
        Me.chkVisiType.RootElement.StretchVertically = True
        Me.chkVisiType.Size = New System.Drawing.Size(68, 16)
        Me.chkVisiType.TabIndex = 5
        Me.chkVisiType.Text = "Visi Type"
        Me.chkVisiType.Visible = False
        '
        'lblLocation
        '
        Me.lblLocation.AutoSize = False
        Me.lblLocation.BorderVisible = True
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(251, 44)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(247, 19)
        Me.lblLocation.TabIndex = 27
        Me.lblLocation.TextWrap = False
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(4, 45)
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
        Me.txtLocation.Location = New System.Drawing.Point(102, 44)
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
        Me.txtLocation.TabIndex = 8
        Me.txtLocation.Value = ""
        '
        'RadLabel23
        '
        Me.RadLabel23.FieldName = Nothing
        Me.RadLabel23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel23.Location = New System.Drawing.Point(501, 23)
        Me.RadLabel23.Name = "RadLabel23"
        Me.RadLabel23.Size = New System.Drawing.Size(48, 16)
        Me.RadLabel23.TabIndex = 15
        Me.RadLabel23.Text = "SRN No"
        '
        'RadLabel12
        '
        Me.RadLabel12.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadLabel12.FieldName = Nothing
        Me.RadLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel12.ForeColor = System.Drawing.SystemColors.Highlight
        Me.RadLabel12.Location = New System.Drawing.Point(533, 396)
        Me.RadLabel12.Name = "RadLabel12"
        Me.RadLabel12.Size = New System.Drawing.Size(498, 16)
        Me.RadLabel12.TabIndex = 25
        Me.RadLabel12.Text = "Double click on Tax Amount Column To Set Item wise Tax on Other open for Assets D" &
    "etails"
        '
        'RadLabel6
        '
        Me.RadLabel6.FieldName = Nothing
        Me.RadLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel6.Location = New System.Drawing.Point(501, 45)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(51, 16)
        Me.RadLabel6.TabIndex = 13
        Me.RadLabel6.Text = "Remarks"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox2.Controls.Add(Me.gv1)
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "Asset Details"
        Me.RadGroupBox2.Location = New System.Drawing.Point(2, 171)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(1024, 225)
        Me.RadGroupBox2.TabIndex = 15
        Me.RadGroupBox2.Text = "Asset Details"
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
        Me.gv1.Size = New System.Drawing.Size(1004, 195)
        Me.gv1.TabIndex = 0
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(386, 2)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 18
        Me.RadLabel4.Text = "Date"
        '
        'chkOnHold
        '
        Me.chkOnHold.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOnHold.Location = New System.Drawing.Point(806, 2)
        Me.chkOnHold.Name = "chkOnHold"
        '
        '
        '
        Me.chkOnHold.RootElement.StretchHorizontally = True
        Me.chkOnHold.RootElement.StretchVertically = True
        Me.chkOnHold.Size = New System.Drawing.Size(62, 16)
        Me.chkOnHold.TabIndex = 4
        Me.chkOnHold.Text = "On Hold"
        '
        'lblVendorName
        '
        Me.lblVendorName.AutoSize = False
        Me.lblVendorName.BorderVisible = True
        Me.lblVendorName.FieldName = Nothing
        Me.lblVendorName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVendorName.Location = New System.Drawing.Point(251, 21)
        Me.lblVendorName.Name = "lblVendorName"
        Me.lblVendorName.Size = New System.Drawing.Size(247, 21)
        Me.lblVendorName.TabIndex = 18
        Me.lblVendorName.TextWrap = False
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(4, 23)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(60, 16)
        Me.RadLabel2.TabIndex = 21
        Me.RadLabel2.Text = "Vendor No"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(4, 2)
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
        Me.btnAddNew.Location = New System.Drawing.Point(360, 1)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 18)
        Me.btnAddNew.TabIndex = 1
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
        Me.txtRemarks.Location = New System.Drawing.Point(573, 44)
        Me.txtRemarks.MaxLength = 200
        Me.txtRemarks.MendatroryField = False
        Me.txtRemarks.MyLinkLable1 = Me.RadLabel6
        Me.txtRemarks.MyLinkLable2 = Nothing
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.ReferenceFieldDesc = Nothing
        Me.txtRemarks.ReferenceFieldName = Nothing
        Me.txtRemarks.ReferenceTableName = Nothing
        Me.txtRemarks.Size = New System.Drawing.Size(456, 18)
        Me.txtRemarks.TabIndex = 7
        '
        'txtSRNNo
        '
        Me.txtSRNNo.CalculationExpression = Nothing
        Me.txtSRNNo.FieldCode = Nothing
        Me.txtSRNNo.FieldDesc = Nothing
        Me.txtSRNNo.FieldMaxLength = 0
        Me.txtSRNNo.FieldName = Nothing
        Me.txtSRNNo.isCalculatedField = False
        Me.txtSRNNo.IsSourceFromTable = False
        Me.txtSRNNo.IsSourceFromValueList = False
        Me.txtSRNNo.IsUnique = False
        Me.txtSRNNo.Location = New System.Drawing.Point(573, 22)
        Me.txtSRNNo.MendatroryField = False
        Me.txtSRNNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSRNNo.MyLinkLable1 = Me.RadLabel23
        Me.txtSRNNo.MyLinkLable2 = Nothing
        Me.txtSRNNo.MyReadOnly = True
        Me.txtSRNNo.MyShowMasterFormButton = False
        Me.txtSRNNo.Name = "txtSRNNo"
        Me.txtSRNNo.ReferenceFieldDesc = Nothing
        Me.txtSRNNo.ReferenceFieldName = Nothing
        Me.txtSRNNo.ReferenceTableName = Nothing
        Me.txtSRNNo.Size = New System.Drawing.Size(160, 19)
        Me.txtSRNNo.TabIndex = 3
        Me.txtSRNNo.Value = ""
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
        Me.txtVendorNo.Location = New System.Drawing.Point(102, 22)
        Me.txtVendorNo.MendatroryField = True
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
        Me.txtVendorNo.TabIndex = 6
        Me.txtVendorNo.Value = ""
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(943, 0)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(88, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 11
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(102, 0)
        Me.txtDocNo.MendatroryField = False
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Me.RadLabel1
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 30
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(258, 20)
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
        Me.txtDate.Location = New System.Drawing.Point(421, 1)
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
        Me.txtDate.TabIndex = 2
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
        Me.txtDesc.Location = New System.Drawing.Point(573, 67)
        Me.txtDesc.MaxLength = 200
        Me.txtDesc.MendatroryField = False
        Me.txtDesc.MyLinkLable1 = Me.RadLabel3
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ReferenceFieldDesc = Nothing
        Me.txtDesc.ReferenceFieldName = Nothing
        Me.txtDesc.ReferenceTableName = Nothing
        Me.txtDesc.Size = New System.Drawing.Size(456, 18)
        Me.txtDesc.TabIndex = 9
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
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1029, 408)
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
        Me.RadLabel10.Location = New System.Drawing.Point(871, 304)
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
        Me.gv2.Size = New System.Drawing.Size(1024, 266)
        Me.gv2.TabIndex = 1
        '
        'RadPageViewPage5
        '
        Me.RadPageViewPage5.Controls.Add(Me.gvAC)
        Me.RadPageViewPage5.ItemSize = New System.Drawing.SizeF(106.0!, 26.0!)
        Me.RadPageViewPage5.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage5.Name = "RadPageViewPage5"
        Me.RadPageViewPage5.Size = New System.Drawing.Size(1029, 408)
        Me.RadPageViewPage5.Text = "Additional Charge"
        '
        'gvAC
        '
        Me.gvAC.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvAC.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvAC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvAC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvAC.ForeColor = System.Drawing.Color.Black
        Me.gvAC.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvAC.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvAC.MasterTemplate.AllowDeleteRow = False
        Me.gvAC.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvAC.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvAC.MasterTemplate.ViewDefinition = TableViewDefinition3
        Me.gvAC.MyStopExport = False
        Me.gvAC.Name = "gvAC"
        Me.gvAC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvAC.ShowGroupPanel = False
        Me.gvAC.ShowHeaderCellButtons = True
        Me.gvAC.Size = New System.Drawing.Size(1029, 408)
        Me.gvAC.TabIndex = 1
        Me.gvAC.TabStop = False
        '
        'RadPageViewPage4
        '
        Me.RadPageViewPage4.Controls.Add(Me.lblAssetAmount)
        Me.RadPageViewPage4.Controls.Add(Me.txtAssetAmount)
        Me.RadPageViewPage4.Controls.Add(Me.lblTaxRecAmt)
        Me.RadPageViewPage4.Controls.Add(Me.lblTaxNonRecAmt)
        Me.RadPageViewPage4.Controls.Add(Me.txtNonRecAmt)
        Me.RadPageViewPage4.Controls.Add(Me.txtRecAmt)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel31)
        Me.RadPageViewPage4.Controls.Add(Me.lblAddCharges)
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
        Me.RadPageViewPage4.Size = New System.Drawing.Size(1029, 408)
        Me.RadPageViewPage4.Text = "Total"
        '
        'lblAssetAmount
        '
        Me.lblAssetAmount.FieldName = Nothing
        Me.lblAssetAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAssetAmount.Location = New System.Drawing.Point(137, 68)
        Me.lblAssetAmount.Name = "lblAssetAmount"
        Me.lblAssetAmount.Size = New System.Drawing.Size(77, 16)
        Me.lblAssetAmount.TabIndex = 132
        Me.lblAssetAmount.Text = "Asset Amount"
        '
        'txtAssetAmount
        '
        Me.txtAssetAmount.AutoSize = False
        Me.txtAssetAmount.BorderVisible = True
        Me.txtAssetAmount.FieldName = Nothing
        Me.txtAssetAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAssetAmount.Location = New System.Drawing.Point(224, 68)
        Me.txtAssetAmount.Name = "txtAssetAmount"
        Me.txtAssetAmount.Size = New System.Drawing.Size(110, 18)
        Me.txtAssetAmount.TabIndex = 133
        Me.txtAssetAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTaxRecAmt
        '
        Me.lblTaxRecAmt.FieldName = Nothing
        Me.lblTaxRecAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTaxRecAmt.Location = New System.Drawing.Point(87, 92)
        Me.lblTaxRecAmt.Name = "lblTaxRecAmt"
        Me.lblTaxRecAmt.Size = New System.Drawing.Size(134, 16)
        Me.lblTaxRecAmt.TabIndex = 128
        Me.lblTaxRecAmt.Text = "Tax Recoverable Amount"
        '
        'lblTaxNonRecAmt
        '
        Me.lblTaxNonRecAmt.FieldName = Nothing
        Me.lblTaxNonRecAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTaxNonRecAmt.Location = New System.Drawing.Point(56, 46)
        Me.lblTaxNonRecAmt.Name = "lblTaxNonRecAmt"
        Me.lblTaxNonRecAmt.Size = New System.Drawing.Size(168, 16)
        Me.lblTaxNonRecAmt.TabIndex = 129
        Me.lblTaxNonRecAmt.Text = "+ Tax Non Recoverable Amount"
        '
        'txtNonRecAmt
        '
        Me.txtNonRecAmt.AutoSize = False
        Me.txtNonRecAmt.BorderVisible = True
        Me.txtNonRecAmt.FieldName = Nothing
        Me.txtNonRecAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNonRecAmt.Location = New System.Drawing.Point(225, 46)
        Me.txtNonRecAmt.Name = "txtNonRecAmt"
        Me.txtNonRecAmt.Size = New System.Drawing.Size(110, 18)
        Me.txtNonRecAmt.TabIndex = 131
        Me.txtNonRecAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtRecAmt
        '
        Me.txtRecAmt.AutoSize = False
        Me.txtRecAmt.BorderVisible = True
        Me.txtRecAmt.FieldName = Nothing
        Me.txtRecAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRecAmt.Location = New System.Drawing.Point(224, 91)
        Me.txtRecAmt.Name = "txtRecAmt"
        Me.txtRecAmt.Size = New System.Drawing.Size(110, 18)
        Me.txtRecAmt.TabIndex = 130
        Me.txtRecAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadLabel31
        '
        Me.RadLabel31.FieldName = Nothing
        Me.RadLabel31.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel31.Location = New System.Drawing.Point(350, 48)
        Me.RadLabel31.Name = "RadLabel31"
        Me.RadLabel31.Size = New System.Drawing.Size(130, 16)
        Me.RadLabel31.TabIndex = 127
        Me.RadLabel31.Text = "Total Additional Charges"
        Me.RadLabel31.Visible = False
        '
        'lblAddCharges
        '
        Me.lblAddCharges.AutoSize = False
        Me.lblAddCharges.BorderVisible = True
        Me.lblAddCharges.FieldName = Nothing
        Me.lblAddCharges.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddCharges.Location = New System.Drawing.Point(486, 48)
        Me.lblAddCharges.Name = "lblAddCharges"
        Me.lblAddCharges.Size = New System.Drawing.Size(110, 18)
        Me.lblAddCharges.TabIndex = 126
        Me.lblAddCharges.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.lblAddCharges.Visible = False
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
        Me.RadLabel27.Location = New System.Drawing.Point(114, 115)
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
        Me.lblNetAmt.Location = New System.Drawing.Point(224, 113)
        Me.lblNetAmt.Name = "lblNetAmt"
        Me.lblNetAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblNetAmt.TabIndex = 125
        Me.lblNetAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadLabel25
        '
        Me.RadLabel25.FieldName = Nothing
        Me.RadLabel25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel25.Location = New System.Drawing.Point(361, 24)
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
        Me.lblTaxAmt.Location = New System.Drawing.Point(448, 24)
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
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(65.0!, 26.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(1029, 408)
        Me.RadPageViewPage3.Text = "Assembly"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox1.Controls.Add(Me.gvAssemble)
        Me.RadGroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = "Assembly Details"
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(1024, 405)
        Me.RadGroupBox1.TabIndex = 16
        Me.RadGroupBox1.Text = "Assembly Details"
        '
        'gvAssemble
        '
        Me.gvAssemble.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvAssemble.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvAssemble.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvAssemble.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvAssemble.ForeColor = System.Drawing.Color.Black
        Me.gvAssemble.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvAssemble.Location = New System.Drawing.Point(10, 20)
        '
        '
        '
        Me.gvAssemble.MasterTemplate.AllowDeleteRow = False
        Me.gvAssemble.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvAssemble.MasterTemplate.ShowFilteringRow = False
        Me.gvAssemble.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvAssemble.MasterTemplate.ViewDefinition = TableViewDefinition4
        Me.gvAssemble.MyStopExport = False
        Me.gvAssemble.Name = "gvAssemble"
        Me.gvAssemble.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvAssemble.ShowGroupPanel = False
        Me.gvAssemble.ShowHeaderCellButtons = True
        Me.gvAssemble.Size = New System.Drawing.Size(1004, 375)
        Me.gvAssemble.TabIndex = 0
        '
        'butCostCenterAndHirerachy_Update_AfterPost
        '
        Me.butCostCenterAndHirerachy_Update_AfterPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butCostCenterAndHirerachy_Update_AfterPost.Location = New System.Drawing.Point(539, 2)
        Me.butCostCenterAndHirerachy_Update_AfterPost.Name = "butCostCenterAndHirerachy_Update_AfterPost"
        Me.butCostCenterAndHirerachy_Update_AfterPost.Size = New System.Drawing.Size(186, 22)
        Me.butCostCenterAndHirerachy_Update_AfterPost.TabIndex = 49
        Me.butCostCenterAndHirerachy_Update_AfterPost.Text = "Update Cost Center And Hirerachy"
        '
        'btnShowInventory
        '
        Me.btnShowInventory.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnShowInventory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShowInventory.Location = New System.Drawing.Point(869, 3)
        Me.btnShowInventory.Name = "btnShowInventory"
        Me.btnShowInventory.Size = New System.Drawing.Size(105, 20)
        Me.btnShowInventory.TabIndex = 48
        Me.btnShowInventory.Text = "Show Inventory"
        '
        'btnReverse
        '
        Me.btnReverse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReverse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverse.Location = New System.Drawing.Point(745, 2)
        Me.btnReverse.Name = "btnReverse"
        Me.btnReverse.Size = New System.Drawing.Size(121, 22)
        Me.btnReverse.TabIndex = 25
        Me.btnReverse.Text = "Reverse and Unpost"
        Me.btnReverse.Visible = False
        '
        'btnUnSelect
        '
        Me.btnUnSelect.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnUnSelect.Location = New System.Drawing.Point(473, 2)
        Me.btnUnSelect.Name = "btnUnSelect"
        Me.btnUnSelect.Size = New System.Drawing.Size(62, 22)
        Me.btnUnSelect.TabIndex = 24
        Me.btnUnSelect.Text = "Select All"
        '
        'btnTDSDetail
        '
        Me.btnTDSDetail.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTDSDetail.Location = New System.Drawing.Point(233, 2)
        Me.btnTDSDetail.Name = "btnTDSDetail"
        Me.btnTDSDetail.Size = New System.Drawing.Size(69, 22)
        Me.btnTDSDetail.TabIndex = 6
        Me.btnTDSDetail.Text = "TDS Detail"
        '
        'btnChangeDepDetail
        '
        Me.btnChangeDepDetail.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnChangeDepDetail.Location = New System.Drawing.Point(305, 2)
        Me.btnChangeDepDetail.Name = "btnChangeDepDetail"
        Me.btnChangeDepDetail.Size = New System.Drawing.Size(164, 22)
        Me.btnChangeDepDetail.TabIndex = 4
        Me.btnChangeDepDetail.Text = "Change Depreciation Details"
        '
        'BtnPrint
        '
        Me.BtnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPrint.Location = New System.Drawing.Point(175, 2)
        Me.BtnPrint.Name = "BtnPrint"
        Me.BtnPrint.Size = New System.Drawing.Size(54, 22)
        Me.BtnPrint.TabIndex = 3
        Me.BtnPrint.Text = "Print"
        '
        'btnDelete
        '
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(118, 2)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(54, 22)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "Delete"
        '
        'btnPost
        '
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(60, 2)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(55, 22)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(977, 2)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(2, 2)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(56, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'frmAcquisionEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1050, 509)
        Me.Controls.Add(Me.SplitContainer1)
        Me.MinimumSize = New System.Drawing.Size(890, 467)
        Me.Name = "frmAcquisionEntry"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Acquisition Entry"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.lblSRNDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPIDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkOpeningDirect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSameDesSpecStartDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAssembleOpeningAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkOpening, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpPostingDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAssembleCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel37, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel38, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel40, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_rebudgetamtwithtolerence, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_rebudgetamt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel35, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_budgetamtwithtolerence, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_budgetamt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_capexcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblcaption1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_capexsubcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblcaption2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPINo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlAcqType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMoveType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkISAssemble, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTemplateDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVendorInvoiceNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnold, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkVisiType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
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
        Me.RadPageViewPage5.ResumeLayout(False)
        CType(Me.gvAC.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvAC, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage4.ResumeLayout(False)
        Me.RadPageViewPage4.PerformLayout()
        CType(Me.lblAssetAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAssetAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTaxRecAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTaxNonRecAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNonRecAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRecAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel31, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAddCharges, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel27, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNetAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTaxAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotalAmt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage3.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.gvAssemble.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvAssemble, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.butCostCenterAndHirerachy_Update_AfterPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnShowInventory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUnSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnTDSDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnChangeDepDetail, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents RadLabel23 As common.Controls.MyLabel
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
    Friend WithEvents txtSRNNo As common.UserControls.txtFinder
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
    Friend WithEvents BtnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents chkVisiType As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents rbtnnew As common.Controls.MyRadioButton
    Friend WithEvents rbtnold As common.Controls.MyRadioButton
    Friend WithEvents btnChangeDepDetail As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtVendorInvoiceNo As common.Controls.MyTextBox
    Friend WithEvents lblTemplateDesc As common.Controls.MyLabel
    Friend WithEvents lblTemplate As common.Controls.MyLabel
    Friend WithEvents fndTemplateCode As common.UserControls.txtFinder
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents ChkISAssemble As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents ddlAcqType As common.Controls.MyComboBox
    Friend WithEvents lblMoveType As common.Controls.MyLabel
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadPageViewPage3 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvAssemble As common.UserControls.MyRadGridView
    Friend WithEvents btnTDSDetail As Telerik.WinControls.UI.RadButton
    Friend WithEvents rmSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents lblPINo As common.Controls.MyLabel
    Friend WithEvents fndPINo As common.UserControls.txtFinder
    Friend WithEvents btnUnSelect As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel37 As common.Controls.MyLabel
    Friend WithEvents MyLabel38 As common.Controls.MyLabel
    Friend WithEvents MyLabel40 As common.Controls.MyLabel
    Friend WithEvents lbl_rebudgetamtwithtolerence As common.Controls.MyLabel
    Friend WithEvents lbl_rebudgetamt As common.Controls.MyLabel
    Friend WithEvents MyLabel35 As common.Controls.MyLabel
    Friend WithEvents lbl_budgetamtwithtolerence As common.Controls.MyLabel
    Friend WithEvents lbl_budgetamt As common.Controls.MyLabel
    Friend WithEvents lbl_capexcode As common.Controls.MyLabel
    Friend WithEvents lblcaption1 As common.Controls.MyLabel
    Friend WithEvents fndcapexcode As common.UserControls.txtFinder
    Friend WithEvents lbl_capexsubcode As common.Controls.MyLabel
    Friend WithEvents lblcaption2 As common.Controls.MyLabel
    Friend WithEvents fndcapexsubcode As common.UserControls.txtFinder
    Friend WithEvents RadPageViewPage5 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvAC As common.UserControls.MyRadGridView
    Friend WithEvents RadLabel31 As common.Controls.MyLabel
    Friend WithEvents lblAddCharges As common.Controls.MyLabel
    Friend WithEvents lblTaxRecAmt As common.Controls.MyLabel
    Friend WithEvents lblTaxNonRecAmt As common.Controls.MyLabel
    Friend WithEvents txtNonRecAmt As common.Controls.MyLabel
    Friend WithEvents txtRecAmt As common.Controls.MyLabel
    Friend WithEvents lblAssetAmount As common.Controls.MyLabel
    Friend WithEvents txtAssetAmount As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtAssembleCode As common.Controls.MyTextBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents dtpPostingDate As common.Controls.MyDateTimePicker
    Friend WithEvents chkOpening As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtAssembleOpeningAmt As common.Controls.MyTextBox
    Friend WithEvents btnReverse As Telerik.WinControls.UI.RadButton
    Friend WithEvents chkSameDesSpecStartDate As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkOpeningDirect As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents btnShowInventory As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblSRNDate As common.Controls.MyLabel
    Friend WithEvents lblPIDate As common.Controls.MyLabel
    Friend WithEvents butCostCenterAndHirerachy_Update_AfterPost As Telerik.WinControls.UI.RadButton
End Class

