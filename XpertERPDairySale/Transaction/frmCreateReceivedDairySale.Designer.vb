<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCreateReceivedDairySale
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCreateReceivedDairySale))
        Dim WindowsSettings1 As Telerik.WinControls.WindowsSettings = New Telerik.WinControls.WindowsSettings()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.comSalesMan = New common.Controls.MyComboBox()
        Me.lblSalesMan = New common.Controls.MyLabel()
        Me.comDriver = New common.Controls.MyComboBox()
        Me.lblDriver = New common.Controls.MyLabel()
        Me.chkCustomerWise = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkMCCAndScrap = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtRouteName = New common.Controls.MyLabel()
        Me.fndRouteNo = New common.UserControls.txtFinder()
        Me.lblRouteNo = New common.Controls.MyLabel()
        Me.txtCanQty = New common.MyNumBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtCrateQty = New common.MyNumBox()
        Me.MyLabel50 = New common.Controls.MyLabel()
        Me.ddlType = New common.Controls.MyComboBox()
        Me.lbltype = New common.Controls.MyLabel()
        Me.txtInvoiceDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.lblVehicle = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.fndVehicle = New common.UserControls.txtFinder()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.RadLabel14 = New common.Controls.MyLabel()
        Me.txtComment = New common.Controls.MyTextBox()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lblLocationName = New common.Controls.MyLabel()
        Me.RadLabel15 = New common.Controls.MyLabel()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.lblCustomerName = New common.Controls.MyLabel()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.fndCustomerNo = New common.UserControls.txtFinder()
        Me.fndLocation = New common.UserControls.txtFinder()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.btnDeleteInvoiceafterPost = New Telerik.WinControls.UI.RadButton()
        Me.btnShowInventory = New Telerik.WinControls.UI.RadButton()
        Me.btnExportToExcel = New Telerik.WinControls.UI.RadButton()
        Me.btnsetting = New Telerik.WinControls.UI.RadSplitButton()
        Me.BtnPreview = New Telerik.WinControls.UI.RadMenuItem()
        Me.BtnSend = New Telerik.WinControls.UI.RadMenuItem()
        Me.BtnSendForApproval = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.EmailSmsSetting = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadDropDownMenu()
        Me.RadThemeManager1 = New Telerik.WinControls.RadThemeManager()
        Me.RadGroupBox7 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtnMrng = New System.Windows.Forms.RadioButton()
        Me.rbtnEvng = New System.Windows.Forms.RadioButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.comSalesMan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSalesMan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.comDriver, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDriver, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCustomerWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMCCAndScrap, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRouteName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRouteNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCanQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCrateQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel50, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbltype, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtInvoiceDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVehicle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtComment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomerName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDeleteInvoiceafterPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnShowInventory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExportToExcel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsetting, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenuItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox7.SuspendLayout()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDeleteInvoiceafterPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnShowInventory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExportToExcel)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsetting)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(1243, 501)
        Me.SplitContainer1.SplitterDistance = 469
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1243, 469)
        Me.RadPageView1.TabIndex = 0
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox7)
        Me.RadPageViewPage1.Controls.Add(Me.comSalesMan)
        Me.RadPageViewPage1.Controls.Add(Me.lblSalesMan)
        Me.RadPageViewPage1.Controls.Add(Me.comDriver)
        Me.RadPageViewPage1.Controls.Add(Me.lblDriver)
        Me.RadPageViewPage1.Controls.Add(Me.chkCustomerWise)
        Me.RadPageViewPage1.Controls.Add(Me.chkMCCAndScrap)
        Me.RadPageViewPage1.Controls.Add(Me.txtRouteName)
        Me.RadPageViewPage1.Controls.Add(Me.fndRouteNo)
        Me.RadPageViewPage1.Controls.Add(Me.lblRouteNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtCanQty)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.txtCrateQty)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel50)
        Me.RadPageViewPage1.Controls.Add(Me.ddlType)
        Me.RadPageViewPage1.Controls.Add(Me.lbltype)
        Me.RadPageViewPage1.Controls.Add(Me.txtInvoiceDate)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.lblVehicle)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.fndVehicle)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.txtDocNo)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel14)
        Me.RadPageViewPage1.Controls.Add(Me.txtComment)
        Me.RadPageViewPage1.Controls.Add(Me.btnGo)
        Me.RadPageViewPage1.Controls.Add(Me.txtDate)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.lblLocationName)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel15)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.lblCustomerName)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.fndCustomerNo)
        Me.RadPageViewPage1.Controls.Add(Me.fndLocation)
        Me.RadPageViewPage1.Controls.Add(Me.btnAddNew)
        Me.RadPageViewPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(115.0!, 26.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1222, 423)
        Me.RadPageViewPage1.Text = "Crate Qty Received"
        '
        'comSalesMan
        '
        Me.comSalesMan.AutoCompleteDisplayMember = Nothing
        Me.comSalesMan.AutoCompleteValueMember = Nothing
        Me.comSalesMan.CalculationExpression = Nothing
        Me.comSalesMan.DropDownAnimationEnabled = True
        Me.comSalesMan.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.comSalesMan.FieldCode = Nothing
        Me.comSalesMan.FieldDesc = Nothing
        Me.comSalesMan.FieldMaxLength = 0
        Me.comSalesMan.FieldName = Nothing
        Me.comSalesMan.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.comSalesMan.isCalculatedField = False
        Me.comSalesMan.IsSourceFromTable = False
        Me.comSalesMan.IsSourceFromValueList = False
        Me.comSalesMan.IsUnique = False
        Me.comSalesMan.Location = New System.Drawing.Point(877, 71)
        Me.comSalesMan.MendatroryField = False
        Me.comSalesMan.MyLinkLable1 = Nothing
        Me.comSalesMan.MyLinkLable2 = Nothing
        Me.comSalesMan.Name = "comSalesMan"
        Me.comSalesMan.ReferenceFieldDesc = Nothing
        Me.comSalesMan.ReferenceFieldName = Nothing
        Me.comSalesMan.ReferenceTableName = Nothing
        Me.comSalesMan.Size = New System.Drawing.Size(93, 18)
        Me.comSalesMan.TabIndex = 1461
        '
        'lblSalesMan
        '
        Me.lblSalesMan.FieldName = Nothing
        Me.lblSalesMan.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSalesMan.Location = New System.Drawing.Point(808, 70)
        Me.lblSalesMan.Name = "lblSalesMan"
        Me.lblSalesMan.Size = New System.Drawing.Size(57, 16)
        Me.lblSalesMan.TabIndex = 1460
        Me.lblSalesMan.Text = "SalesMan"
        '
        'comDriver
        '
        Me.comDriver.AutoCompleteDisplayMember = Nothing
        Me.comDriver.AutoCompleteValueMember = Nothing
        Me.comDriver.CalculationExpression = Nothing
        Me.comDriver.DropDownAnimationEnabled = True
        Me.comDriver.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.comDriver.FieldCode = Nothing
        Me.comDriver.FieldDesc = Nothing
        Me.comDriver.FieldMaxLength = 0
        Me.comDriver.FieldName = Nothing
        Me.comDriver.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.comDriver.isCalculatedField = False
        Me.comDriver.IsSourceFromTable = False
        Me.comDriver.IsSourceFromValueList = False
        Me.comDriver.IsUnique = False
        Me.comDriver.Location = New System.Drawing.Point(877, 53)
        Me.comDriver.MendatroryField = False
        Me.comDriver.MyLinkLable1 = Nothing
        Me.comDriver.MyLinkLable2 = Nothing
        Me.comDriver.Name = "comDriver"
        Me.comDriver.ReferenceFieldDesc = Nothing
        Me.comDriver.ReferenceFieldName = Nothing
        Me.comDriver.ReferenceTableName = Nothing
        Me.comDriver.Size = New System.Drawing.Size(93, 18)
        Me.comDriver.TabIndex = 1459
        '
        'lblDriver
        '
        Me.lblDriver.FieldName = Nothing
        Me.lblDriver.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDriver.Location = New System.Drawing.Point(808, 52)
        Me.lblDriver.Name = "lblDriver"
        Me.lblDriver.Size = New System.Drawing.Size(36, 16)
        Me.lblDriver.TabIndex = 1458
        Me.lblDriver.Text = "Driver"
        '
        'chkCustomerWise
        '
        Me.chkCustomerWise.Enabled = False
        Me.chkCustomerWise.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCustomerWise.Location = New System.Drawing.Point(767, 28)
        Me.chkCustomerWise.Name = "chkCustomerWise"
        Me.chkCustomerWise.Size = New System.Drawing.Size(72, 16)
        Me.chkCustomerWise.TabIndex = 1457
        Me.chkCustomerWise.Text = "Customer "
        '
        'chkMCCAndScrap
        '
        Me.chkMCCAndScrap.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMCCAndScrap.Location = New System.Drawing.Point(767, 4)
        Me.chkMCCAndScrap.Name = "chkMCCAndScrap"
        Me.chkMCCAndScrap.Size = New System.Drawing.Size(79, 16)
        Me.chkMCCAndScrap.TabIndex = 70
        Me.chkMCCAndScrap.Text = "MCC/Scrap"
        '
        'txtRouteName
        '
        Me.txtRouteName.AutoSize = False
        Me.txtRouteName.BorderVisible = True
        Me.txtRouteName.FieldName = Nothing
        Me.txtRouteName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRouteName.Location = New System.Drawing.Point(243, 48)
        Me.txtRouteName.Name = "txtRouteName"
        Me.txtRouteName.Size = New System.Drawing.Size(287, 18)
        Me.txtRouteName.TabIndex = 1456
        Me.txtRouteName.TextWrap = False
        '
        'fndRouteNo
        '
        Me.fndRouteNo.CalculationExpression = Nothing
        Me.fndRouteNo.FieldCode = Nothing
        Me.fndRouteNo.FieldDesc = Nothing
        Me.fndRouteNo.FieldMaxLength = 0
        Me.fndRouteNo.FieldName = Nothing
        Me.fndRouteNo.isCalculatedField = False
        Me.fndRouteNo.IsSourceFromTable = False
        Me.fndRouteNo.IsSourceFromValueList = False
        Me.fndRouteNo.IsUnique = False
        Me.fndRouteNo.Location = New System.Drawing.Point(99, 48)
        Me.fndRouteNo.MendatroryField = False
        Me.fndRouteNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndRouteNo.MyLinkLable1 = Me.lblRouteNo
        Me.fndRouteNo.MyLinkLable2 = Me.txtRouteName
        Me.fndRouteNo.MyReadOnly = False
        Me.fndRouteNo.MyShowMasterFormButton = False
        Me.fndRouteNo.Name = "fndRouteNo"
        Me.fndRouteNo.ReferenceFieldDesc = Nothing
        Me.fndRouteNo.ReferenceFieldName = Nothing
        Me.fndRouteNo.ReferenceTableName = Nothing
        Me.fndRouteNo.Size = New System.Drawing.Size(143, 19)
        Me.fndRouteNo.TabIndex = 1455
        Me.fndRouteNo.Value = ""
        '
        'lblRouteNo
        '
        Me.lblRouteNo.FieldName = Nothing
        Me.lblRouteNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRouteNo.Location = New System.Drawing.Point(0, 50)
        Me.lblRouteNo.Name = "lblRouteNo"
        Me.lblRouteNo.Size = New System.Drawing.Size(54, 16)
        Me.lblRouteNo.TabIndex = 1454
        Me.lblRouteNo.Text = "Route No"
        Me.lblRouteNo.Visible = False
        '
        'txtCanQty
        '
        Me.txtCanQty.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtCanQty.CalculationExpression = Nothing
        Me.txtCanQty.DecimalPlaces = 5
        Me.txtCanQty.Enabled = False
        Me.txtCanQty.FieldCode = Nothing
        Me.txtCanQty.FieldDesc = Nothing
        Me.txtCanQty.FieldMaxLength = 0
        Me.txtCanQty.FieldName = Nothing
        Me.txtCanQty.isCalculatedField = False
        Me.txtCanQty.IsSourceFromTable = False
        Me.txtCanQty.IsSourceFromValueList = False
        Me.txtCanQty.IsUnique = False
        Me.txtCanQty.Location = New System.Drawing.Point(722, 70)
        Me.txtCanQty.MendatroryField = False
        Me.txtCanQty.MyLinkLable1 = Nothing
        Me.txtCanQty.MyLinkLable2 = Nothing
        Me.txtCanQty.Name = "txtCanQty"
        Me.txtCanQty.ReadOnly = True
        Me.txtCanQty.ReferenceFieldDesc = Nothing
        Me.txtCanQty.ReferenceFieldName = Nothing
        Me.txtCanQty.ReferenceTableName = Nothing
        Me.txtCanQty.Size = New System.Drawing.Size(77, 20)
        Me.txtCanQty.TabIndex = 1453
        Me.txtCanQty.Text = "0"
        Me.txtCanQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtCanQty.Value = 0R
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(626, 72)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(76, 16)
        Me.MyLabel4.TabIndex = 1452
        Me.MyLabel4.Text = "Total Can Qty"
        '
        'txtCrateQty
        '
        Me.txtCrateQty.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtCrateQty.CalculationExpression = Nothing
        Me.txtCrateQty.DecimalPlaces = 5
        Me.txtCrateQty.Enabled = False
        Me.txtCrateQty.FieldCode = Nothing
        Me.txtCrateQty.FieldDesc = Nothing
        Me.txtCrateQty.FieldMaxLength = 0
        Me.txtCrateQty.FieldName = Nothing
        Me.txtCrateQty.isCalculatedField = False
        Me.txtCrateQty.IsSourceFromTable = False
        Me.txtCrateQty.IsSourceFromValueList = False
        Me.txtCrateQty.IsUnique = False
        Me.txtCrateQty.Location = New System.Drawing.Point(722, 48)
        Me.txtCrateQty.MendatroryField = False
        Me.txtCrateQty.MyLinkLable1 = Nothing
        Me.txtCrateQty.MyLinkLable2 = Nothing
        Me.txtCrateQty.Name = "txtCrateQty"
        Me.txtCrateQty.ReadOnly = True
        Me.txtCrateQty.ReferenceFieldDesc = Nothing
        Me.txtCrateQty.ReferenceFieldName = Nothing
        Me.txtCrateQty.ReferenceTableName = Nothing
        Me.txtCrateQty.Size = New System.Drawing.Size(77, 20)
        Me.txtCrateQty.TabIndex = 1451
        Me.txtCrateQty.Text = "0"
        Me.txtCrateQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtCrateQty.Value = 0R
        '
        'MyLabel50
        '
        Me.MyLabel50.FieldName = Nothing
        Me.MyLabel50.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel50.Location = New System.Drawing.Point(626, 50)
        Me.MyLabel50.Name = "MyLabel50"
        Me.MyLabel50.Size = New System.Drawing.Size(83, 16)
        Me.MyLabel50.TabIndex = 1450
        Me.MyLabel50.Text = "Total Crate Qty"
        '
        'ddlType
        '
        Me.ddlType.AutoCompleteDisplayMember = Nothing
        Me.ddlType.AutoCompleteValueMember = Nothing
        Me.ddlType.CalculationExpression = Nothing
        Me.ddlType.DropDownAnimationEnabled = True
        Me.ddlType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlType.FieldCode = Nothing
        Me.ddlType.FieldDesc = Nothing
        Me.ddlType.FieldMaxLength = 0
        Me.ddlType.FieldName = Nothing
        Me.ddlType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddlType.isCalculatedField = False
        Me.ddlType.IsSourceFromTable = False
        Me.ddlType.IsSourceFromValueList = False
        Me.ddlType.IsUnique = False
        Me.ddlType.Location = New System.Drawing.Point(651, 26)
        Me.ddlType.MendatroryField = False
        Me.ddlType.MyLinkLable1 = Nothing
        Me.ddlType.MyLinkLable2 = Nothing
        Me.ddlType.Name = "ddlType"
        Me.ddlType.ReferenceFieldDesc = Nothing
        Me.ddlType.ReferenceFieldName = Nothing
        Me.ddlType.ReferenceTableName = Nothing
        Me.ddlType.Size = New System.Drawing.Size(93, 18)
        Me.ddlType.TabIndex = 70
        '
        'lbltype
        '
        Me.lbltype.FieldName = Nothing
        Me.lbltype.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltype.Location = New System.Drawing.Point(582, 25)
        Me.lbltype.Name = "lbltype"
        Me.lbltype.Size = New System.Drawing.Size(31, 16)
        Me.lbltype.TabIndex = 69
        Me.lbltype.Text = "Type"
        '
        'txtInvoiceDate
        '
        Me.txtInvoiceDate.CalculationExpression = Nothing
        Me.txtInvoiceDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtInvoiceDate.FieldCode = Nothing
        Me.txtInvoiceDate.FieldDesc = Nothing
        Me.txtInvoiceDate.FieldMaxLength = 0
        Me.txtInvoiceDate.FieldName = Nothing
        Me.txtInvoiceDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvoiceDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.txtInvoiceDate.isCalculatedField = False
        Me.txtInvoiceDate.IsSourceFromTable = False
        Me.txtInvoiceDate.IsSourceFromValueList = False
        Me.txtInvoiceDate.IsUnique = False
        Me.txtInvoiceDate.Location = New System.Drawing.Point(651, 4)
        Me.txtInvoiceDate.MendatroryField = False
        Me.txtInvoiceDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtInvoiceDate.MyLinkLable1 = Me.MyLabel2
        Me.txtInvoiceDate.MyLinkLable2 = Nothing
        Me.txtInvoiceDate.Name = "txtInvoiceDate"
        Me.txtInvoiceDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtInvoiceDate.ReferenceFieldDesc = Nothing
        Me.txtInvoiceDate.ReferenceFieldName = Nothing
        Me.txtInvoiceDate.ReferenceTableName = Nothing
        Me.txtInvoiceDate.Size = New System.Drawing.Size(93, 18)
        Me.txtInvoiceDate.TabIndex = 67
        Me.txtInvoiceDate.TabStop = False
        Me.txtInvoiceDate.Text = "13/06/2011"
        Me.txtInvoiceDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(579, 3)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(50, 16)
        Me.MyLabel2.TabIndex = 68
        Me.MyLabel2.Text = "DO Date"
        '
        'lblVehicle
        '
        Me.lblVehicle.AutoSize = False
        Me.lblVehicle.BorderVisible = True
        Me.lblVehicle.FieldName = Nothing
        Me.lblVehicle.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVehicle.Location = New System.Drawing.Point(243, 47)
        Me.lblVehicle.Name = "lblVehicle"
        Me.lblVehicle.Size = New System.Drawing.Size(287, 18)
        Me.lblVehicle.TabIndex = 66
        Me.lblVehicle.TextWrap = False
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(0, 48)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(46, 16)
        Me.MyLabel3.TabIndex = 65
        Me.MyLabel3.Text = "Vehicle "
        '
        'fndVehicle
        '
        Me.fndVehicle.CalculationExpression = Nothing
        Me.fndVehicle.FieldCode = Nothing
        Me.fndVehicle.FieldDesc = Nothing
        Me.fndVehicle.FieldMaxLength = 0
        Me.fndVehicle.FieldName = Nothing
        Me.fndVehicle.isCalculatedField = False
        Me.fndVehicle.IsSourceFromTable = False
        Me.fndVehicle.IsSourceFromValueList = False
        Me.fndVehicle.IsUnique = False
        Me.fndVehicle.Location = New System.Drawing.Point(99, 47)
        Me.fndVehicle.MendatroryField = True
        Me.fndVehicle.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndVehicle.MyLinkLable1 = Me.MyLabel3
        Me.fndVehicle.MyLinkLable2 = Me.lblVehicle
        Me.fndVehicle.MyReadOnly = False
        Me.fndVehicle.MyShowMasterFormButton = False
        Me.fndVehicle.Name = "fndVehicle"
        Me.fndVehicle.ReferenceFieldDesc = Nothing
        Me.fndVehicle.ReferenceFieldName = Nothing
        Me.fndVehicle.ReferenceTableName = Nothing
        Me.fndVehicle.Size = New System.Drawing.Size(143, 19)
        Me.fndVehicle.TabIndex = 64
        Me.fndVehicle.Value = ""
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(0, 2)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(75, 16)
        Me.RadLabel1.TabIndex = 63
        Me.RadLabel1.Text = "Document No"
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(99, 0)
        Me.txtDocNo.MendatroryField = False
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Me.RadLabel1
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 32767
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(252, 22)
        Me.txtDocNo.TabIndex = 61
        Me.txtDocNo.Value = ""
        '
        'RadLabel14
        '
        Me.RadLabel14.FieldName = Nothing
        Me.RadLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel14.Location = New System.Drawing.Point(0, 71)
        Me.RadLabel14.Name = "RadLabel14"
        Me.RadLabel14.Size = New System.Drawing.Size(55, 16)
        Me.RadLabel14.TabIndex = 60
        Me.RadLabel14.Text = "Comment"
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
        Me.txtComment.Location = New System.Drawing.Point(98, 70)
        Me.txtComment.MaxLength = 200
        Me.txtComment.MendatroryField = False
        Me.txtComment.MyLinkLable1 = Me.RadLabel14
        Me.txtComment.MyLinkLable2 = Nothing
        Me.txtComment.Name = "txtComment"
        Me.txtComment.ReferenceFieldDesc = Nothing
        Me.txtComment.ReferenceFieldName = Nothing
        Me.txtComment.ReferenceTableName = Nothing
        Me.txtComment.Size = New System.Drawing.Size(431, 18)
        Me.txtComment.TabIndex = 59
        '
        'btnGo
        '
        Me.btnGo.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnGo.Location = New System.Drawing.Point(535, 71)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(79, 17)
        Me.btnGo.TabIndex = 39
        Me.btnGo.Text = ">>"
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
        Me.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.txtDate.isCalculatedField = False
        Me.txtDate.IsSourceFromTable = False
        Me.txtDate.IsSourceFromValueList = False
        Me.txtDate.IsUnique = False
        Me.txtDate.Location = New System.Drawing.Point(478, 3)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.MyLabel1
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(91, 18)
        Me.txtDate.TabIndex = 57
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(385, 3)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(85, 16)
        Me.MyLabel1.TabIndex = 58
        Me.MyLabel1.Text = "Document Date"
        '
        'lblLocationName
        '
        Me.lblLocationName.AutoSize = False
        Me.lblLocationName.BorderVisible = True
        Me.lblLocationName.FieldName = Nothing
        Me.lblLocationName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocationName.Location = New System.Drawing.Point(242, 25)
        Me.lblLocationName.Name = "lblLocationName"
        Me.lblLocationName.Size = New System.Drawing.Size(287, 18)
        Me.lblLocationName.TabIndex = 55
        Me.lblLocationName.TextWrap = False
        '
        'RadLabel15
        '
        Me.RadLabel15.FieldName = Nothing
        Me.RadLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel15.Location = New System.Drawing.Point(0, 26)
        Me.RadLabel15.Name = "RadLabel15"
        Me.RadLabel15.Size = New System.Drawing.Size(49, 16)
        Me.RadLabel15.TabIndex = 37
        Me.RadLabel15.Text = "Location"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox2.Controls.Add(Me.gv1)
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "Invoice Details"
        Me.RadGroupBox2.Location = New System.Drawing.Point(2, 96)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(1217, 325)
        Me.RadGroupBox2.TabIndex = 27
        Me.RadGroupBox2.Text = "Invoice Details"
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
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(1197, 295)
        Me.gv1.TabIndex = 1
        Me.gv1.TabStop = False
        '
        'lblCustomerName
        '
        Me.lblCustomerName.AutoSize = False
        Me.lblCustomerName.BorderVisible = True
        Me.lblCustomerName.FieldName = Nothing
        Me.lblCustomerName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerName.Location = New System.Drawing.Point(1141, 62)
        Me.lblCustomerName.Name = "lblCustomerName"
        Me.lblCustomerName.Size = New System.Drawing.Size(287, 18)
        Me.lblCustomerName.TabIndex = 56
        Me.lblCustomerName.TextWrap = False
        Me.lblCustomerName.Visible = False
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(898, 63)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(73, 16)
        Me.RadLabel2.TabIndex = 38
        Me.RadLabel2.Text = "Customer No"
        Me.RadLabel2.Visible = False
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
        Me.fndCustomerNo.Location = New System.Drawing.Point(997, 62)
        Me.fndCustomerNo.MendatroryField = True
        Me.fndCustomerNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndCustomerNo.MyLinkLable1 = Me.RadLabel2
        Me.fndCustomerNo.MyLinkLable2 = Me.lblCustomerName
        Me.fndCustomerNo.MyReadOnly = False
        Me.fndCustomerNo.MyShowMasterFormButton = False
        Me.fndCustomerNo.Name = "fndCustomerNo"
        Me.fndCustomerNo.ReferenceFieldDesc = Nothing
        Me.fndCustomerNo.ReferenceFieldName = Nothing
        Me.fndCustomerNo.ReferenceTableName = Nothing
        Me.fndCustomerNo.Size = New System.Drawing.Size(143, 18)
        Me.fndCustomerNo.TabIndex = 4
        Me.fndCustomerNo.Value = ""
        Me.fndCustomerNo.Visible = False
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
        Me.fndLocation.Location = New System.Drawing.Point(98, 25)
        Me.fndLocation.MendatroryField = True
        Me.fndLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLocation.MyLinkLable1 = Me.RadLabel15
        Me.fndLocation.MyLinkLable2 = Me.lblLocationName
        Me.fndLocation.MyReadOnly = False
        Me.fndLocation.MyShowMasterFormButton = False
        Me.fndLocation.Name = "fndLocation"
        Me.fndLocation.ReferenceFieldDesc = Nothing
        Me.fndLocation.ReferenceFieldName = Nothing
        Me.fndLocation.ReferenceTableName = Nothing
        Me.fndLocation.Size = New System.Drawing.Size(143, 18)
        Me.fndLocation.TabIndex = 6
        Me.fndLocation.Value = ""
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = CType(resources.GetObject("btnAddNew.Image"), System.Drawing.Image)
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(352, 1)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 21)
        Me.btnAddNew.TabIndex = 1
        '
        'btnDeleteInvoiceafterPost
        '
        Me.btnDeleteInvoiceafterPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDeleteInvoiceafterPost.Location = New System.Drawing.Point(607, 3)
        Me.btnDeleteInvoiceafterPost.Name = "btnDeleteInvoiceafterPost"
        Me.btnDeleteInvoiceafterPost.Size = New System.Drawing.Size(122, 24)
        Me.btnDeleteInvoiceafterPost.TabIndex = 330
        Me.btnDeleteInvoiceafterPost.Text = "Reverse And Unpost"
        Me.btnDeleteInvoiceafterPost.Visible = False
        '
        'btnShowInventory
        '
        Me.btnShowInventory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnShowInventory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShowInventory.Location = New System.Drawing.Point(500, 4)
        Me.btnShowInventory.Name = "btnShowInventory"
        Me.btnShowInventory.Size = New System.Drawing.Size(101, 22)
        Me.btnShowInventory.TabIndex = 329
        Me.btnShowInventory.Text = "Show Inventory"
        '
        'btnExportToExcel
        '
        Me.btnExportToExcel.Location = New System.Drawing.Point(229, 4)
        Me.btnExportToExcel.Name = "btnExportToExcel"
        Me.btnExportToExcel.Size = New System.Drawing.Size(86, 22)
        Me.btnExportToExcel.TabIndex = 328
        Me.btnExportToExcel.Text = "Export To Excel"
        '
        'btnsetting
        '
        Me.btnsetting.Items.AddRange(New Telerik.WinControls.RadItem() {Me.BtnPreview, Me.BtnSend, Me.BtnSendForApproval})
        Me.btnsetting.Location = New System.Drawing.Point(407, 4)
        Me.btnsetting.Name = "btnsetting"
        Me.btnsetting.Size = New System.Drawing.Size(87, 22)
        Me.btnsetting.TabIndex = 4
        Me.btnsetting.Text = "E-Mail/SMS"
        Me.btnsetting.Visible = False
        '
        'BtnPreview
        '
        Me.BtnPreview.Name = "BtnPreview"
        Me.BtnPreview.Text = "Preview"
        '
        'BtnSend
        '
        Me.BtnSend.Name = "BtnSend"
        Me.BtnSend.Text = "Send Mail/Sms"
        '
        'BtnSendForApproval
        '
        Me.BtnSendForApproval.Name = "BtnSendForApproval"
        Me.BtnSendForApproval.Text = "Send Mail For Approval"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(332, 4)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(69, 22)
        Me.btnPrint.TabIndex = 3
        Me.btnPrint.Text = "Print"
        Me.btnPrint.Visible = False
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(155, 4)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 22)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "Delete"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(80, 4)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(69, 22)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(1170, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(5, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'EmailSmsSetting
        '
        Me.EmailSmsSetting.Name = "EmailSmsSetting"
        Me.EmailSmsSetting.Text = "Email/SMS Setting"
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Delete Layout"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.SplitContainer1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1243, 501)
        Me.Panel1.TabIndex = 7
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1243, 20)
        Me.RadMenu1.TabIndex = 6
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItem4, Me.EmailSmsSetting, Me.rmExport, Me.rmImport})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Save Layout"
        '
        'rmExport
        '
        Me.rmExport.Name = "rmExport"
        Me.rmExport.Text = "Export(Opening for OUT)"
        '
        'rmImport
        '
        Me.rmImport.Name = "rmImport"
        Me.rmImport.Text = "Import(Opening For Out)"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AnimationEnabled = False
        Me.RadMenuItem2.AnimationFrames = 1
        Me.RadMenuItem2.AnimationType = Telerik.WinControls.UI.PopupAnimationTypes.None
        Me.RadMenuItem2.AutoSize = True
        Me.RadMenuItem2.DropDownAnimationDirection = Telerik.WinControls.UI.RadDirection.Down
        Me.RadMenuItem2.DropShadow = True
        Me.RadMenuItem2.EasingType = Telerik.WinControls.RadEasingType.InQuad
        Me.RadMenuItem2.EnableAeroEffects = False
        Me.RadMenuItem2.FadeAnimationFrames = 10
        Me.RadMenuItem2.FadeAnimationSpeed = 10
        Me.RadMenuItem2.FadeAnimationType = Telerik.WinControls.UI.FadeAnimationType.FadeIn
        Me.RadMenuItem2.FitToScreenMode = CType((Telerik.WinControls.UI.FitToScreenModes.FitWidth Or Telerik.WinControls.UI.FitToScreenModes.FitHeight), Telerik.WinControls.UI.FitToScreenModes)
        Me.RadMenuItem2.HorizontalAlignmentCorrectionMode = Telerik.WinControls.UI.AlignmentCorrectionMode.SnapToOuterEdges
        Me.RadMenuItem2.LastShowDpiScaleFactor = New System.Drawing.SizeF(1.0!, 1.0!)
        Me.RadMenuItem2.Location = New System.Drawing.Point(50, 191)
        Me.RadMenuItem2.Maximum = New System.Drawing.Size(0, 0)
        Me.RadMenuItem2.Minimum = New System.Drawing.Size(0, 0)
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Opacity = 1.0!
        Me.RadMenuItem2.ProcessKeyboard = False
        Me.RadMenuItem2.RollOverItemSelection = True
        Me.RadMenuItem2.Size = New System.Drawing.Size(27, 2)
        Me.RadMenuItem2.TabIndex = 5
        Me.RadMenuItem2.VerticalAlignmentCorrectionMode = Telerik.WinControls.UI.AlignmentCorrectionMode.SnapToOuterEdges
        Me.RadMenuItem2.Visible = False
        WindowsSettings1.EnableRoundedCorners = Nothing
        WindowsSettings1.RoundedCornersStyle = Telerik.WinControls.RoundedCornersStyle.Round
        Me.RadMenuItem2.WindowsSettings = WindowsSettings1
        '
        'RadGroupBox7
        '
        Me.RadGroupBox7.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox7.Controls.Add(Me.rbtnMrng)
        Me.RadGroupBox7.Controls.Add(Me.rbtnEvng)
        Me.RadGroupBox7.HeaderText = ""
        Me.RadGroupBox7.Location = New System.Drawing.Point(862, 2)
        Me.RadGroupBox7.Name = "RadGroupBox7"
        Me.RadGroupBox7.Size = New System.Drawing.Size(167, 26)
        Me.RadGroupBox7.TabIndex = 1462
        '
        'rbtnMrng
        '
        Me.rbtnMrng.AutoSize = True
        Me.rbtnMrng.Checked = True
        Me.rbtnMrng.Location = New System.Drawing.Point(5, 4)
        Me.rbtnMrng.Name = "rbtnMrng"
        Me.rbtnMrng.Size = New System.Drawing.Size(70, 17)
        Me.rbtnMrng.TabIndex = 440
        Me.rbtnMrng.TabStop = True
        Me.rbtnMrng.Text = "Morning"
        Me.rbtnMrng.UseVisualStyleBackColor = True
        '
        'rbtnEvng
        '
        Me.rbtnEvng.AutoSize = True
        Me.rbtnEvng.Location = New System.Drawing.Point(98, 4)
        Me.rbtnEvng.Name = "rbtnEvng"
        Me.rbtnEvng.Size = New System.Drawing.Size(66, 17)
        Me.rbtnEvng.TabIndex = 441
        Me.rbtnEvng.Text = "Evening"
        Me.rbtnEvng.UseVisualStyleBackColor = True
        '
        'frmCreateReceivedDairySale
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1243, 521)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Controls.Add(Me.RadMenuItem2)
        Me.Name = "frmCreateReceivedDairySale"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Crate Received"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.comSalesMan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSalesMan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.comDriver, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDriver, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCustomerWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMCCAndScrap, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRouteName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRouteNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCanQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCrateQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel50, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbltype, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtInvoiceDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVehicle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtComment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomerName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDeleteInvoiceafterPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnShowInventory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExportToExcel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsetting, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenuItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox7.ResumeLayout(False)
        Me.RadGroupBox7.PerformLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents lblLocationName As common.Controls.MyLabel
    Friend WithEvents RadLabel15 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents lblCustomerName As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents fndCustomerNo As common.UserControls.txtFinder
    Friend WithEvents fndLocation As common.UserControls.txtFinder
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsetting As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents BtnPreview As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents BtnSend As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents BtnSendForApproval As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents EmailSmsSetting As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem4 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadDropDownMenu
    Friend WithEvents RadThemeManager1 As Telerik.WinControls.RadThemeManager
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnExportToExcel As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel14 As common.Controls.MyLabel
    Friend WithEvents txtComment As common.Controls.MyTextBox
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents lblVehicle As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents fndVehicle As common.UserControls.txtFinder
    Friend WithEvents txtInvoiceDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents lbltype As common.Controls.MyLabel
    Friend WithEvents ddlType As common.Controls.MyComboBox
    Friend WithEvents txtCanQty As common.MyNumBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtCrateQty As common.MyNumBox
    Friend WithEvents MyLabel50 As common.Controls.MyLabel
    Friend WithEvents txtRouteName As common.Controls.MyLabel
    Friend WithEvents fndRouteNo As common.UserControls.txtFinder
    Friend WithEvents lblRouteNo As common.Controls.MyLabel
    Friend WithEvents chkMCCAndScrap As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnShowInventory As Telerik.WinControls.UI.RadButton
    Friend WithEvents chkCustomerWise As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents comSalesMan As common.Controls.MyComboBox
    Friend WithEvents lblSalesMan As common.Controls.MyLabel
    Friend WithEvents comDriver As common.Controls.MyComboBox
    Friend WithEvents lblDriver As common.Controls.MyLabel
    Friend WithEvents btnDeleteInvoiceafterPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents rmExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadGroupBox7 As RadGroupBox
    Friend WithEvents rbtnMrng As RadioButton
    Friend WithEvents rbtnEvng As RadioButton
End Class

