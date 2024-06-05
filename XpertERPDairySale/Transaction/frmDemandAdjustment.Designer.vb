<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmDemandAdjustment
    Inherits XpertERPEngine.FrmMainTranScreen

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
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.txtRouteCode = New common.UserControls.txtMultiSelectFinder()
        Me.lblRouteNo = New common.Controls.MyLabel()
        Me.txtZoneCode = New common.UserControls.txtMultiSelectFinder()
        Me.lblZoneCode = New common.Controls.MyLabel()
        Me.rgbChangeItem = New Telerik.WinControls.UI.RadGroupBox()
        Me.btnChange = New Telerik.WinControls.UI.RadButton()
        Me.txtAddQty = New Telerik.WinControls.UI.RadTextBox()
        Me.lblAddQty = New common.Controls.MyLabel()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.TxtFinder6 = New common.UserControls.txtFinder()
        Me.lblChangeitemDesc = New common.Controls.MyLabel()
        Me.lblChangeitem = New common.Controls.MyLabel()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.TxtFinder5 = New common.UserControls.txtFinder()
        Me.txtChangeItemCode = New common.UserControls.txtFinder()
        Me.txtDeductQty = New Telerik.WinControls.UI.RadTextBox()
        Me.lblDeductQty = New common.Controls.MyLabel()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.TxtFinder4 = New common.UserControls.txtFinder()
        Me.rgbDecreaseOrder = New Telerik.WinControls.UI.RadGroupBox()
        Me.btnDecrease1 = New Telerik.WinControls.UI.RadButton()
        Me.rgbIncreaseOrder = New Telerik.WinControls.UI.RadGroupBox()
        Me.btnIncrease1 = New Telerik.WinControls.UI.RadButton()
        Me.rgbIDOrder = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.MyLabel17 = New common.Controls.MyLabel()
        Me.MyLabel18 = New common.Controls.MyLabel()
        Me.TxtFinder8 = New common.UserControls.txtFinder()
        Me.lblPer = New common.Controls.MyLabel()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.MyLabel15 = New common.Controls.MyLabel()
        Me.TxtFinder7 = New common.UserControls.txtFinder()
        Me.txtPer = New Telerik.WinControls.UI.RadTextBox()
        Me.txtQty = New Telerik.WinControls.UI.RadTextBox()
        Me.rgbMode = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtnFix = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtnAutomatic = New Telerik.WinControls.UI.RadRadioButton()
        Me.txtMinCrate = New Telerik.WinControls.UI.RadTextBox()
        Me.lblUOMDesc = New common.Controls.MyLabel()
        Me.lblMinCrate = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.TxtFinder2 = New common.UserControls.txtFinder()
        Me.lblUOM = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.TxtFinder1 = New common.UserControls.txtFinder()
        Me.txtUOM = New common.UserControls.txtFinder()
        Me.chkChangeItem = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtDemandDate = New common.Controls.MyDateTimePicker()
        Me.lblDemandDate = New common.Controls.MyLabel()
        Me.lblItemDesc = New common.Controls.MyLabel()
        Me.lblItemCode = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.TxtFinder3 = New common.UserControls.txtFinder()
        Me.txtItemCode = New common.UserControls.txtFinder()
        Me.rgbShiftType = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtnEvening = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtnMorning = New Telerik.WinControls.UI.RadRadioButton()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.lblDate = New common.Controls.MyLabel()
        Me.lblDocNo = New common.Controls.MyLabel()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv1 = New Telerik.WinControls.UI.RadGridView()
        Me.btnProceed = New Telerik.WinControls.UI.RadButton()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.lblRouteNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblZoneCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rgbChangeItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.rgbChangeItem.SuspendLayout()
        CType(Me.btnChange, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAddQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAddQty, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.lblAddQty.SuspendLayout()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblChangeitemDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblChangeitem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.lblChangeitem.SuspendLayout()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDeductQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDeductQty, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.lblDeductQty.SuspendLayout()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rgbDecreaseOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.rgbDecreaseOrder.SuspendLayout()
        CType(Me.btnDecrease1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rgbIncreaseOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.rgbIncreaseOrder.SuspendLayout()
        CType(Me.btnIncrease1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rgbIDOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.rgbIDOrder.SuspendLayout()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MyLabel16.SuspendLayout()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.lblPer.SuspendLayout()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rgbMode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.rgbMode.SuspendLayout()
        CType(Me.rbtnFix, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnAutomatic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMinCrate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblUOMDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMinCrate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.lblMinCrate.SuspendLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblUOM, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.lblUOM.SuspendLayout()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkChangeItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDemandDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDemandDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblItemDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblItemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.lblItemCode.SuspendLayout()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rgbShiftType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.rgbShiftType.SuspendLayout()
        CType(Me.rbtnEvening, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnMorning, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnProceed, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer3)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnProceed)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(826, 450)
        Me.SplitContainer1.SplitterDistance = 407
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.RadMenu1)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.RadPageView1)
        Me.SplitContainer3.Size = New System.Drawing.Size(826, 407)
        Me.SplitContainer3.SplitterDistance = 25
        Me.SplitContainer3.TabIndex = 1
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(826, 20)
        Me.RadMenu1.TabIndex = 1
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiImport, Me.rmiExport})
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Setting"
        Me.RadMenuItem2.UseCompatibleTextRendering = False
        '
        'rmiImport
        '
        Me.rmiImport.Name = "rmiImport"
        Me.rmiImport.Text = "Import"
        '
        'rmiExport
        '
        Me.rmiExport.Name = "rmiExport"
        Me.rmiExport.Text = "Export"
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(826, 378)
        Me.RadPageView1.TabIndex = 0
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.txtRouteCode)
        Me.RadPageViewPage1.Controls.Add(Me.txtZoneCode)
        Me.RadPageViewPage1.Controls.Add(Me.rgbChangeItem)
        Me.RadPageViewPage1.Controls.Add(Me.rgbDecreaseOrder)
        Me.RadPageViewPage1.Controls.Add(Me.rgbIncreaseOrder)
        Me.RadPageViewPage1.Controls.Add(Me.rgbIDOrder)
        Me.RadPageViewPage1.Controls.Add(Me.rgbMode)
        Me.RadPageViewPage1.Controls.Add(Me.txtMinCrate)
        Me.RadPageViewPage1.Controls.Add(Me.lblUOMDesc)
        Me.RadPageViewPage1.Controls.Add(Me.lblMinCrate)
        Me.RadPageViewPage1.Controls.Add(Me.lblUOM)
        Me.RadPageViewPage1.Controls.Add(Me.txtUOM)
        Me.RadPageViewPage1.Controls.Add(Me.chkChangeItem)
        Me.RadPageViewPage1.Controls.Add(Me.txtDemandDate)
        Me.RadPageViewPage1.Controls.Add(Me.lblDemandDate)
        Me.RadPageViewPage1.Controls.Add(Me.lblItemDesc)
        Me.RadPageViewPage1.Controls.Add(Me.lblItemCode)
        Me.RadPageViewPage1.Controls.Add(Me.txtItemCode)
        Me.RadPageViewPage1.Controls.Add(Me.rgbShiftType)
        Me.RadPageViewPage1.Controls.Add(Me.lblRouteNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtDate)
        Me.RadPageViewPage1.Controls.Add(Me.lblZoneCode)
        Me.RadPageViewPage1.Controls.Add(Me.lblDate)
        Me.RadPageViewPage1.Controls.Add(Me.lblDocNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtDocNo)
        Me.RadPageViewPage1.Controls.Add(Me.btnAddNew)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(119.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(805, 330)
        Me.RadPageViewPage1.Text = "Demand Adjustment"
        '
        'txtRouteCode
        '
        Me.txtRouteCode.arrDispalyMember = Nothing
        Me.txtRouteCode.arrValueMember = Nothing
        Me.txtRouteCode.Location = New System.Drawing.Point(91, 47)
        Me.txtRouteCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRouteCode.MyLinkLable1 = Me.lblRouteNo
        Me.txtRouteCode.MyLinkLable2 = Nothing
        Me.txtRouteCode.MyNullText = "All"
        Me.txtRouteCode.Name = "txtRouteCode"
        Me.txtRouteCode.Size = New System.Drawing.Size(272, 21)
        Me.txtRouteCode.TabIndex = 1599
        '
        'lblRouteNo
        '
        Me.lblRouteNo.FieldName = Nothing
        Me.lblRouteNo.Location = New System.Drawing.Point(2, 49)
        Me.lblRouteNo.Name = "lblRouteNo"
        Me.lblRouteNo.Size = New System.Drawing.Size(54, 18)
        Me.lblRouteNo.TabIndex = 1581
        Me.lblRouteNo.Text = "Route No"
        '
        'txtZoneCode
        '
        Me.txtZoneCode.arrDispalyMember = Nothing
        Me.txtZoneCode.arrValueMember = Nothing
        Me.txtZoneCode.Location = New System.Drawing.Point(91, 25)
        Me.txtZoneCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtZoneCode.MyLinkLable1 = Me.lblZoneCode
        Me.txtZoneCode.MyLinkLable2 = Nothing
        Me.txtZoneCode.MyNullText = "All"
        Me.txtZoneCode.Name = "txtZoneCode"
        Me.txtZoneCode.Size = New System.Drawing.Size(272, 21)
        Me.txtZoneCode.TabIndex = 1598
        '
        'lblZoneCode
        '
        Me.lblZoneCode.FieldName = Nothing
        Me.lblZoneCode.Location = New System.Drawing.Point(2, 25)
        Me.lblZoneCode.Name = "lblZoneCode"
        Me.lblZoneCode.Size = New System.Drawing.Size(61, 18)
        Me.lblZoneCode.TabIndex = 1577
        Me.lblZoneCode.Text = "Zone Code"
        '
        'rgbChangeItem
        '
        Me.rgbChangeItem.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.rgbChangeItem.Controls.Add(Me.btnChange)
        Me.rgbChangeItem.Controls.Add(Me.txtAddQty)
        Me.rgbChangeItem.Controls.Add(Me.lblAddQty)
        Me.rgbChangeItem.Controls.Add(Me.lblChangeitemDesc)
        Me.rgbChangeItem.Controls.Add(Me.lblChangeitem)
        Me.rgbChangeItem.Controls.Add(Me.txtChangeItemCode)
        Me.rgbChangeItem.Controls.Add(Me.txtDeductQty)
        Me.rgbChangeItem.Controls.Add(Me.lblDeductQty)
        Me.rgbChangeItem.HeaderText = "Change Product"
        Me.rgbChangeItem.Location = New System.Drawing.Point(360, 122)
        Me.rgbChangeItem.Name = "rgbChangeItem"
        Me.rgbChangeItem.Size = New System.Drawing.Size(384, 103)
        Me.rgbChangeItem.TabIndex = 1597
        Me.rgbChangeItem.Text = "Change Product"
        '
        'btnChange
        '
        Me.btnChange.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnChange.Location = New System.Drawing.Point(221, 72)
        Me.btnChange.Name = "btnChange"
        Me.btnChange.Size = New System.Drawing.Size(152, 19)
        Me.btnChange.TabIndex = 1554
        Me.btnChange.Text = "Change Product"
        '
        'txtAddQty
        '
        Me.txtAddQty.Location = New System.Drawing.Point(93, 71)
        Me.txtAddQty.Name = "txtAddQty"
        Me.txtAddQty.Size = New System.Drawing.Size(120, 20)
        Me.txtAddQty.TabIndex = 1553
        '
        'lblAddQty
        '
        Me.lblAddQty.Controls.Add(Me.MyLabel12)
        Me.lblAddQty.Controls.Add(Me.MyLabel13)
        Me.lblAddQty.Controls.Add(Me.TxtFinder6)
        Me.lblAddQty.FieldName = Nothing
        Me.lblAddQty.Location = New System.Drawing.Point(4, 69)
        Me.lblAddQty.Name = "lblAddQty"
        Me.lblAddQty.Size = New System.Drawing.Size(48, 18)
        Me.lblAddQty.TabIndex = 1552
        Me.lblAddQty.Text = "Add Qty"
        '
        'MyLabel12
        '
        Me.MyLabel12.AutoSize = False
        Me.MyLabel12.BorderVisible = True
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel12.Location = New System.Drawing.Point(211, 26)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(152, 19)
        Me.MyLabel12.TabIndex = 1542
        Me.MyLabel12.TextWrap = False
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Location = New System.Drawing.Point(0, 24)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(54, 18)
        Me.MyLabel13.TabIndex = 1541
        Me.MyLabel13.Text = "Route No"
        '
        'TxtFinder6
        '
        Me.TxtFinder6.CalculationExpression = Nothing
        Me.TxtFinder6.FieldCode = Nothing
        Me.TxtFinder6.FieldDesc = Nothing
        Me.TxtFinder6.FieldMaxLength = 0
        Me.TxtFinder6.FieldName = Nothing
        Me.TxtFinder6.isCalculatedField = False
        Me.TxtFinder6.IsSourceFromTable = False
        Me.TxtFinder6.IsSourceFromValueList = False
        Me.TxtFinder6.IsUnique = False
        Me.TxtFinder6.Location = New System.Drawing.Point(93, 26)
        Me.TxtFinder6.MendatroryField = False
        Me.TxtFinder6.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFinder6.MyLinkLable1 = Me.MyLabel13
        Me.TxtFinder6.MyLinkLable2 = Nothing
        Me.TxtFinder6.MyReadOnly = False
        Me.TxtFinder6.MyShowMasterFormButton = False
        Me.TxtFinder6.Name = "TxtFinder6"
        Me.TxtFinder6.ReferenceFieldDesc = Nothing
        Me.TxtFinder6.ReferenceFieldName = Nothing
        Me.TxtFinder6.ReferenceTableName = Nothing
        Me.TxtFinder6.Size = New System.Drawing.Size(115, 19)
        Me.TxtFinder6.TabIndex = 1540
        Me.TxtFinder6.Value = ""
        '
        'lblChangeitemDesc
        '
        Me.lblChangeitemDesc.AutoSize = False
        Me.lblChangeitemDesc.BorderVisible = True
        Me.lblChangeitemDesc.FieldName = Nothing
        Me.lblChangeitemDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChangeitemDesc.Location = New System.Drawing.Point(219, 47)
        Me.lblChangeitemDesc.Name = "lblChangeitemDesc"
        Me.lblChangeitemDesc.Size = New System.Drawing.Size(152, 19)
        Me.lblChangeitemDesc.TabIndex = 1551
        Me.lblChangeitemDesc.TextWrap = False
        '
        'lblChangeitem
        '
        Me.lblChangeitem.Controls.Add(Me.MyLabel10)
        Me.lblChangeitem.Controls.Add(Me.MyLabel11)
        Me.lblChangeitem.Controls.Add(Me.TxtFinder5)
        Me.lblChangeitem.FieldName = Nothing
        Me.lblChangeitem.Location = New System.Drawing.Point(5, 43)
        Me.lblChangeitem.Name = "lblChangeitem"
        Me.lblChangeitem.Size = New System.Drawing.Size(58, 18)
        Me.lblChangeitem.TabIndex = 1550
        Me.lblChangeitem.Text = "Item Code"
        '
        'MyLabel10
        '
        Me.MyLabel10.AutoSize = False
        Me.MyLabel10.BorderVisible = True
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(211, 26)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(152, 19)
        Me.MyLabel10.TabIndex = 1542
        Me.MyLabel10.TextWrap = False
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Location = New System.Drawing.Point(0, 24)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(54, 18)
        Me.MyLabel11.TabIndex = 1541
        Me.MyLabel11.Text = "Route No"
        '
        'TxtFinder5
        '
        Me.TxtFinder5.CalculationExpression = Nothing
        Me.TxtFinder5.FieldCode = Nothing
        Me.TxtFinder5.FieldDesc = Nothing
        Me.TxtFinder5.FieldMaxLength = 0
        Me.TxtFinder5.FieldName = Nothing
        Me.TxtFinder5.isCalculatedField = False
        Me.TxtFinder5.IsSourceFromTable = False
        Me.TxtFinder5.IsSourceFromValueList = False
        Me.TxtFinder5.IsUnique = False
        Me.TxtFinder5.Location = New System.Drawing.Point(93, 26)
        Me.TxtFinder5.MendatroryField = False
        Me.TxtFinder5.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFinder5.MyLinkLable1 = Me.MyLabel11
        Me.TxtFinder5.MyLinkLable2 = Nothing
        Me.TxtFinder5.MyReadOnly = False
        Me.TxtFinder5.MyShowMasterFormButton = False
        Me.TxtFinder5.Name = "TxtFinder5"
        Me.TxtFinder5.ReferenceFieldDesc = Nothing
        Me.TxtFinder5.ReferenceFieldName = Nothing
        Me.TxtFinder5.ReferenceTableName = Nothing
        Me.TxtFinder5.Size = New System.Drawing.Size(115, 19)
        Me.TxtFinder5.TabIndex = 1540
        Me.TxtFinder5.Value = ""
        '
        'txtChangeItemCode
        '
        Me.txtChangeItemCode.CalculationExpression = Nothing
        Me.txtChangeItemCode.FieldCode = Nothing
        Me.txtChangeItemCode.FieldDesc = Nothing
        Me.txtChangeItemCode.FieldMaxLength = 0
        Me.txtChangeItemCode.FieldName = Nothing
        Me.txtChangeItemCode.isCalculatedField = False
        Me.txtChangeItemCode.IsSourceFromTable = False
        Me.txtChangeItemCode.IsSourceFromValueList = False
        Me.txtChangeItemCode.IsUnique = False
        Me.txtChangeItemCode.Location = New System.Drawing.Point(93, 46)
        Me.txtChangeItemCode.MendatroryField = False
        Me.txtChangeItemCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChangeItemCode.MyLinkLable1 = Me.lblChangeitem
        Me.txtChangeItemCode.MyLinkLable2 = Nothing
        Me.txtChangeItemCode.MyReadOnly = False
        Me.txtChangeItemCode.MyShowMasterFormButton = False
        Me.txtChangeItemCode.Name = "txtChangeItemCode"
        Me.txtChangeItemCode.ReferenceFieldDesc = Nothing
        Me.txtChangeItemCode.ReferenceFieldName = Nothing
        Me.txtChangeItemCode.ReferenceTableName = Nothing
        Me.txtChangeItemCode.Size = New System.Drawing.Size(120, 19)
        Me.txtChangeItemCode.TabIndex = 1549
        Me.txtChangeItemCode.Value = ""
        '
        'txtDeductQty
        '
        Me.txtDeductQty.Location = New System.Drawing.Point(94, 23)
        Me.txtDeductQty.Name = "txtDeductQty"
        Me.txtDeductQty.Size = New System.Drawing.Size(120, 20)
        Me.txtDeductQty.TabIndex = 1548
        '
        'lblDeductQty
        '
        Me.lblDeductQty.Controls.Add(Me.MyLabel8)
        Me.lblDeductQty.Controls.Add(Me.MyLabel9)
        Me.lblDeductQty.Controls.Add(Me.TxtFinder4)
        Me.lblDeductQty.FieldName = Nothing
        Me.lblDeductQty.Location = New System.Drawing.Point(5, 21)
        Me.lblDeductQty.Name = "lblDeductQty"
        Me.lblDeductQty.Size = New System.Drawing.Size(63, 18)
        Me.lblDeductQty.TabIndex = 1547
        Me.lblDeductQty.Text = "Deduct Qty"
        '
        'MyLabel8
        '
        Me.MyLabel8.AutoSize = False
        Me.MyLabel8.BorderVisible = True
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(211, 26)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(152, 19)
        Me.MyLabel8.TabIndex = 1542
        Me.MyLabel8.TextWrap = False
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Location = New System.Drawing.Point(0, 24)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(54, 18)
        Me.MyLabel9.TabIndex = 1541
        Me.MyLabel9.Text = "Route No"
        '
        'TxtFinder4
        '
        Me.TxtFinder4.CalculationExpression = Nothing
        Me.TxtFinder4.FieldCode = Nothing
        Me.TxtFinder4.FieldDesc = Nothing
        Me.TxtFinder4.FieldMaxLength = 0
        Me.TxtFinder4.FieldName = Nothing
        Me.TxtFinder4.isCalculatedField = False
        Me.TxtFinder4.IsSourceFromTable = False
        Me.TxtFinder4.IsSourceFromValueList = False
        Me.TxtFinder4.IsUnique = False
        Me.TxtFinder4.Location = New System.Drawing.Point(93, 26)
        Me.TxtFinder4.MendatroryField = False
        Me.TxtFinder4.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFinder4.MyLinkLable1 = Me.MyLabel9
        Me.TxtFinder4.MyLinkLable2 = Nothing
        Me.TxtFinder4.MyReadOnly = False
        Me.TxtFinder4.MyShowMasterFormButton = False
        Me.TxtFinder4.Name = "TxtFinder4"
        Me.TxtFinder4.ReferenceFieldDesc = Nothing
        Me.TxtFinder4.ReferenceFieldName = Nothing
        Me.TxtFinder4.ReferenceTableName = Nothing
        Me.TxtFinder4.Size = New System.Drawing.Size(115, 19)
        Me.TxtFinder4.TabIndex = 1540
        Me.TxtFinder4.Value = ""
        '
        'rgbDecreaseOrder
        '
        Me.rgbDecreaseOrder.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.rgbDecreaseOrder.Controls.Add(Me.btnDecrease1)
        Me.rgbDecreaseOrder.HeaderText = "Decrease Order"
        Me.rgbDecreaseOrder.Location = New System.Drawing.Point(151, 240)
        Me.rgbDecreaseOrder.Name = "rgbDecreaseOrder"
        Me.rgbDecreaseOrder.Size = New System.Drawing.Size(113, 52)
        Me.rgbDecreaseOrder.TabIndex = 1596
        Me.rgbDecreaseOrder.Text = "Decrease Order"
        '
        'btnDecrease1
        '
        Me.btnDecrease1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDecrease1.Location = New System.Drawing.Point(16, 21)
        Me.btnDecrease1.Name = "btnDecrease1"
        Me.btnDecrease1.Size = New System.Drawing.Size(87, 24)
        Me.btnDecrease1.TabIndex = 1543
        Me.btnDecrease1.Text = "Decrease"
        '
        'rgbIncreaseOrder
        '
        Me.rgbIncreaseOrder.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.rgbIncreaseOrder.Controls.Add(Me.btnIncrease1)
        Me.rgbIncreaseOrder.HeaderText = "Increase Order"
        Me.rgbIncreaseOrder.Location = New System.Drawing.Point(35, 240)
        Me.rgbIncreaseOrder.Name = "rgbIncreaseOrder"
        Me.rgbIncreaseOrder.Size = New System.Drawing.Size(113, 52)
        Me.rgbIncreaseOrder.TabIndex = 1595
        Me.rgbIncreaseOrder.Text = "Increase Order"
        '
        'btnIncrease1
        '
        Me.btnIncrease1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnIncrease1.Location = New System.Drawing.Point(13, 20)
        Me.btnIncrease1.Name = "btnIncrease1"
        Me.btnIncrease1.Size = New System.Drawing.Size(87, 24)
        Me.btnIncrease1.TabIndex = 1542
        Me.btnIncrease1.Text = "Increase"
        '
        'rgbIDOrder
        '
        Me.rgbIDOrder.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.rgbIDOrder.Controls.Add(Me.MyLabel16)
        Me.rgbIDOrder.Controls.Add(Me.lblPer)
        Me.rgbIDOrder.Controls.Add(Me.txtPer)
        Me.rgbIDOrder.Controls.Add(Me.txtQty)
        Me.rgbIDOrder.HeaderText = "Increase/Decrease Order"
        Me.rgbIDOrder.Location = New System.Drawing.Point(2, 182)
        Me.rgbIDOrder.Name = "rgbIDOrder"
        Me.rgbIDOrder.Size = New System.Drawing.Size(288, 52)
        Me.rgbIDOrder.TabIndex = 1594
        Me.rgbIDOrder.Text = "Increase/Decrease Order"
        '
        'MyLabel16
        '
        Me.MyLabel16.Controls.Add(Me.MyLabel17)
        Me.MyLabel16.Controls.Add(Me.MyLabel18)
        Me.MyLabel16.Controls.Add(Me.TxtFinder8)
        Me.MyLabel16.FieldName = Nothing
        Me.MyLabel16.Location = New System.Drawing.Point(6, 23)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(49, 18)
        Me.MyLabel16.TabIndex = 1593
        Me.MyLabel16.Text = "Quantity"
        '
        'MyLabel17
        '
        Me.MyLabel17.AutoSize = False
        Me.MyLabel17.BorderVisible = True
        Me.MyLabel17.FieldName = Nothing
        Me.MyLabel17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel17.Location = New System.Drawing.Point(211, 26)
        Me.MyLabel17.Name = "MyLabel17"
        Me.MyLabel17.Size = New System.Drawing.Size(152, 19)
        Me.MyLabel17.TabIndex = 1542
        Me.MyLabel17.TextWrap = False
        '
        'MyLabel18
        '
        Me.MyLabel18.FieldName = Nothing
        Me.MyLabel18.Location = New System.Drawing.Point(0, 24)
        Me.MyLabel18.Name = "MyLabel18"
        Me.MyLabel18.Size = New System.Drawing.Size(54, 18)
        Me.MyLabel18.TabIndex = 1541
        Me.MyLabel18.Text = "Route No"
        '
        'TxtFinder8
        '
        Me.TxtFinder8.CalculationExpression = Nothing
        Me.TxtFinder8.FieldCode = Nothing
        Me.TxtFinder8.FieldDesc = Nothing
        Me.TxtFinder8.FieldMaxLength = 0
        Me.TxtFinder8.FieldName = Nothing
        Me.TxtFinder8.isCalculatedField = False
        Me.TxtFinder8.IsSourceFromTable = False
        Me.TxtFinder8.IsSourceFromValueList = False
        Me.TxtFinder8.IsUnique = False
        Me.TxtFinder8.Location = New System.Drawing.Point(93, 26)
        Me.TxtFinder8.MendatroryField = False
        Me.TxtFinder8.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFinder8.MyLinkLable1 = Me.MyLabel18
        Me.TxtFinder8.MyLinkLable2 = Nothing
        Me.TxtFinder8.MyReadOnly = False
        Me.TxtFinder8.MyShowMasterFormButton = False
        Me.TxtFinder8.Name = "TxtFinder8"
        Me.TxtFinder8.ReferenceFieldDesc = Nothing
        Me.TxtFinder8.ReferenceFieldName = Nothing
        Me.TxtFinder8.ReferenceTableName = Nothing
        Me.TxtFinder8.Size = New System.Drawing.Size(115, 19)
        Me.TxtFinder8.TabIndex = 1540
        Me.TxtFinder8.Value = ""
        '
        'lblPer
        '
        Me.lblPer.Controls.Add(Me.MyLabel14)
        Me.lblPer.Controls.Add(Me.MyLabel15)
        Me.lblPer.Controls.Add(Me.TxtFinder7)
        Me.lblPer.FieldName = Nothing
        Me.lblPer.Location = New System.Drawing.Point(146, 23)
        Me.lblPer.Name = "lblPer"
        Me.lblPer.Size = New System.Drawing.Size(62, 18)
        Me.lblPer.TabIndex = 1592
        Me.lblPer.Text = "Percentage"
        '
        'MyLabel14
        '
        Me.MyLabel14.AutoSize = False
        Me.MyLabel14.BorderVisible = True
        Me.MyLabel14.FieldName = Nothing
        Me.MyLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel14.Location = New System.Drawing.Point(211, 26)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(152, 19)
        Me.MyLabel14.TabIndex = 1542
        Me.MyLabel14.TextWrap = False
        '
        'MyLabel15
        '
        Me.MyLabel15.FieldName = Nothing
        Me.MyLabel15.Location = New System.Drawing.Point(0, 24)
        Me.MyLabel15.Name = "MyLabel15"
        Me.MyLabel15.Size = New System.Drawing.Size(54, 18)
        Me.MyLabel15.TabIndex = 1541
        Me.MyLabel15.Text = "Route No"
        '
        'TxtFinder7
        '
        Me.TxtFinder7.CalculationExpression = Nothing
        Me.TxtFinder7.FieldCode = Nothing
        Me.TxtFinder7.FieldDesc = Nothing
        Me.TxtFinder7.FieldMaxLength = 0
        Me.TxtFinder7.FieldName = Nothing
        Me.TxtFinder7.isCalculatedField = False
        Me.TxtFinder7.IsSourceFromTable = False
        Me.TxtFinder7.IsSourceFromValueList = False
        Me.TxtFinder7.IsUnique = False
        Me.TxtFinder7.Location = New System.Drawing.Point(93, 26)
        Me.TxtFinder7.MendatroryField = False
        Me.TxtFinder7.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFinder7.MyLinkLable1 = Me.MyLabel15
        Me.TxtFinder7.MyLinkLable2 = Nothing
        Me.TxtFinder7.MyReadOnly = False
        Me.TxtFinder7.MyShowMasterFormButton = False
        Me.TxtFinder7.Name = "TxtFinder7"
        Me.TxtFinder7.ReferenceFieldDesc = Nothing
        Me.TxtFinder7.ReferenceFieldName = Nothing
        Me.TxtFinder7.ReferenceTableName = Nothing
        Me.TxtFinder7.Size = New System.Drawing.Size(115, 19)
        Me.TxtFinder7.TabIndex = 1540
        Me.TxtFinder7.Value = ""
        '
        'txtPer
        '
        Me.txtPer.Location = New System.Drawing.Point(212, 22)
        Me.txtPer.Name = "txtPer"
        Me.txtPer.Size = New System.Drawing.Size(67, 20)
        Me.txtPer.TabIndex = 1
        '
        'txtQty
        '
        Me.txtQty.Location = New System.Drawing.Point(59, 23)
        Me.txtQty.Name = "txtQty"
        Me.txtQty.Size = New System.Drawing.Size(72, 20)
        Me.txtQty.TabIndex = 0
        '
        'rgbMode
        '
        Me.rgbMode.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.rgbMode.Controls.Add(Me.rbtnFix)
        Me.rgbMode.Controls.Add(Me.rbtnAutomatic)
        Me.rgbMode.HeaderText = "Mode"
        Me.rgbMode.Location = New System.Drawing.Point(2, 121)
        Me.rgbMode.Name = "rgbMode"
        Me.rgbMode.Size = New System.Drawing.Size(167, 52)
        Me.rgbMode.TabIndex = 1593
        Me.rgbMode.Text = "Mode"
        '
        'rbtnFix
        '
        Me.rbtnFix.Location = New System.Drawing.Point(83, 21)
        Me.rbtnFix.Name = "rbtnFix"
        Me.rbtnFix.Size = New System.Drawing.Size(55, 18)
        Me.rbtnFix.TabIndex = 1
        Me.rbtnFix.Text = "Fix Qty"
        '
        'rbtnAutomatic
        '
        Me.rbtnAutomatic.Location = New System.Drawing.Point(6, 22)
        Me.rbtnAutomatic.Name = "rbtnAutomatic"
        Me.rbtnAutomatic.Size = New System.Drawing.Size(72, 18)
        Me.rbtnAutomatic.TabIndex = 0
        Me.rbtnAutomatic.Text = "Automatic"
        '
        'txtMinCrate
        '
        Me.txtMinCrate.Location = New System.Drawing.Point(473, 93)
        Me.txtMinCrate.Name = "txtMinCrate"
        Me.txtMinCrate.Size = New System.Drawing.Size(120, 20)
        Me.txtMinCrate.TabIndex = 1546
        '
        'lblUOMDesc
        '
        Me.lblUOMDesc.AutoSize = False
        Me.lblUOMDesc.BorderVisible = True
        Me.lblUOMDesc.FieldName = Nothing
        Me.lblUOMDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUOMDesc.Location = New System.Drawing.Point(211, 93)
        Me.lblUOMDesc.Name = "lblUOMDesc"
        Me.lblUOMDesc.Size = New System.Drawing.Size(152, 19)
        Me.lblUOMDesc.TabIndex = 1592
        Me.lblUOMDesc.TextWrap = False
        '
        'lblMinCrate
        '
        Me.lblMinCrate.Controls.Add(Me.MyLabel2)
        Me.lblMinCrate.Controls.Add(Me.MyLabel7)
        Me.lblMinCrate.Controls.Add(Me.TxtFinder2)
        Me.lblMinCrate.FieldName = Nothing
        Me.lblMinCrate.Location = New System.Drawing.Point(384, 93)
        Me.lblMinCrate.Name = "lblMinCrate"
        Me.lblMinCrate.Size = New System.Drawing.Size(75, 18)
        Me.lblMinCrate.TabIndex = 1545
        Me.lblMinCrate.Text = "Minimum Qty"
        '
        'MyLabel2
        '
        Me.MyLabel2.AutoSize = False
        Me.MyLabel2.BorderVisible = True
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(211, 26)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(152, 19)
        Me.MyLabel2.TabIndex = 1542
        Me.MyLabel2.TextWrap = False
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Location = New System.Drawing.Point(0, 24)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(54, 18)
        Me.MyLabel7.TabIndex = 1541
        Me.MyLabel7.Text = "Route No"
        '
        'TxtFinder2
        '
        Me.TxtFinder2.CalculationExpression = Nothing
        Me.TxtFinder2.FieldCode = Nothing
        Me.TxtFinder2.FieldDesc = Nothing
        Me.TxtFinder2.FieldMaxLength = 0
        Me.TxtFinder2.FieldName = Nothing
        Me.TxtFinder2.isCalculatedField = False
        Me.TxtFinder2.IsSourceFromTable = False
        Me.TxtFinder2.IsSourceFromValueList = False
        Me.TxtFinder2.IsUnique = False
        Me.TxtFinder2.Location = New System.Drawing.Point(93, 26)
        Me.TxtFinder2.MendatroryField = False
        Me.TxtFinder2.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFinder2.MyLinkLable1 = Me.MyLabel7
        Me.TxtFinder2.MyLinkLable2 = Nothing
        Me.TxtFinder2.MyReadOnly = False
        Me.TxtFinder2.MyShowMasterFormButton = False
        Me.TxtFinder2.Name = "TxtFinder2"
        Me.TxtFinder2.ReferenceFieldDesc = Nothing
        Me.TxtFinder2.ReferenceFieldName = Nothing
        Me.TxtFinder2.ReferenceTableName = Nothing
        Me.TxtFinder2.Size = New System.Drawing.Size(115, 19)
        Me.TxtFinder2.TabIndex = 1540
        Me.TxtFinder2.Value = ""
        '
        'lblUOM
        '
        Me.lblUOM.Controls.Add(Me.MyLabel3)
        Me.lblUOM.Controls.Add(Me.MyLabel4)
        Me.lblUOM.Controls.Add(Me.TxtFinder1)
        Me.lblUOM.FieldName = Nothing
        Me.lblUOM.Location = New System.Drawing.Point(2, 94)
        Me.lblUOM.Name = "lblUOM"
        Me.lblUOM.Size = New System.Drawing.Size(33, 18)
        Me.lblUOM.TabIndex = 1591
        Me.lblUOM.Text = "UOM"
        '
        'MyLabel3
        '
        Me.MyLabel3.AutoSize = False
        Me.MyLabel3.BorderVisible = True
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(211, 26)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(152, 19)
        Me.MyLabel3.TabIndex = 1542
        Me.MyLabel3.TextWrap = False
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Location = New System.Drawing.Point(0, 24)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(54, 18)
        Me.MyLabel4.TabIndex = 1541
        Me.MyLabel4.Text = "Route No"
        '
        'TxtFinder1
        '
        Me.TxtFinder1.CalculationExpression = Nothing
        Me.TxtFinder1.FieldCode = Nothing
        Me.TxtFinder1.FieldDesc = Nothing
        Me.TxtFinder1.FieldMaxLength = 0
        Me.TxtFinder1.FieldName = Nothing
        Me.TxtFinder1.isCalculatedField = False
        Me.TxtFinder1.IsSourceFromTable = False
        Me.TxtFinder1.IsSourceFromValueList = False
        Me.TxtFinder1.IsUnique = False
        Me.TxtFinder1.Location = New System.Drawing.Point(93, 26)
        Me.TxtFinder1.MendatroryField = False
        Me.TxtFinder1.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFinder1.MyLinkLable1 = Me.MyLabel4
        Me.TxtFinder1.MyLinkLable2 = Nothing
        Me.TxtFinder1.MyReadOnly = False
        Me.TxtFinder1.MyShowMasterFormButton = False
        Me.TxtFinder1.Name = "TxtFinder1"
        Me.TxtFinder1.ReferenceFieldDesc = Nothing
        Me.TxtFinder1.ReferenceFieldName = Nothing
        Me.TxtFinder1.ReferenceTableName = Nothing
        Me.TxtFinder1.Size = New System.Drawing.Size(115, 19)
        Me.TxtFinder1.TabIndex = 1540
        Me.TxtFinder1.Value = ""
        '
        'txtUOM
        '
        Me.txtUOM.CalculationExpression = Nothing
        Me.txtUOM.FieldCode = Nothing
        Me.txtUOM.FieldDesc = Nothing
        Me.txtUOM.FieldMaxLength = 0
        Me.txtUOM.FieldName = Nothing
        Me.txtUOM.isCalculatedField = False
        Me.txtUOM.IsSourceFromTable = False
        Me.txtUOM.IsSourceFromValueList = False
        Me.txtUOM.IsUnique = False
        Me.txtUOM.Location = New System.Drawing.Point(91, 93)
        Me.txtUOM.MendatroryField = False
        Me.txtUOM.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUOM.MyLinkLable1 = Me.lblUOM
        Me.txtUOM.MyLinkLable2 = Nothing
        Me.txtUOM.MyReadOnly = False
        Me.txtUOM.MyShowMasterFormButton = False
        Me.txtUOM.Name = "txtUOM"
        Me.txtUOM.ReferenceFieldDesc = Nothing
        Me.txtUOM.ReferenceFieldName = Nothing
        Me.txtUOM.ReferenceTableName = Nothing
        Me.txtUOM.Size = New System.Drawing.Size(117, 19)
        Me.txtUOM.TabIndex = 1590
        Me.txtUOM.Value = ""
        '
        'chkChangeItem
        '
        Me.chkChangeItem.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkChangeItem.Location = New System.Drawing.Point(566, 37)
        Me.chkChangeItem.Name = "chkChangeItem"
        Me.chkChangeItem.Size = New System.Drawing.Size(102, 16)
        Me.chkChangeItem.TabIndex = 1589
        Me.chkChangeItem.Text = "Change Product"
        '
        'txtDemandDate
        '
        Me.txtDemandDate.CalculationExpression = Nothing
        Me.txtDemandDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtDemandDate.FieldCode = Nothing
        Me.txtDemandDate.FieldDesc = Nothing
        Me.txtDemandDate.FieldMaxLength = 0
        Me.txtDemandDate.FieldName = Nothing
        Me.txtDemandDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDemandDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDemandDate.isCalculatedField = False
        Me.txtDemandDate.IsSourceFromTable = False
        Me.txtDemandDate.IsSourceFromValueList = False
        Me.txtDemandDate.IsUnique = False
        Me.txtDemandDate.Location = New System.Drawing.Point(639, 5)
        Me.txtDemandDate.MendatroryField = False
        Me.txtDemandDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDemandDate.MyLinkLable1 = Me.lblDemandDate
        Me.txtDemandDate.MyLinkLable2 = Nothing
        Me.txtDemandDate.Name = "txtDemandDate"
        Me.txtDemandDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDemandDate.ReferenceFieldDesc = Nothing
        Me.txtDemandDate.ReferenceFieldName = Nothing
        Me.txtDemandDate.ReferenceTableName = Nothing
        Me.txtDemandDate.Size = New System.Drawing.Size(134, 18)
        Me.txtDemandDate.TabIndex = 1588
        Me.txtDemandDate.TabStop = False
        Me.txtDemandDate.Text = "13/06/2011 11:29 AM"
        Me.txtDemandDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'lblDemandDate
        '
        Me.lblDemandDate.FieldName = Nothing
        Me.lblDemandDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDemandDate.Location = New System.Drawing.Point(557, 7)
        Me.lblDemandDate.Name = "lblDemandDate"
        Me.lblDemandDate.Size = New System.Drawing.Size(76, 16)
        Me.lblDemandDate.TabIndex = 1587
        Me.lblDemandDate.Text = "Demand Date"
        '
        'lblItemDesc
        '
        Me.lblItemDesc.AutoSize = False
        Me.lblItemDesc.BorderVisible = True
        Me.lblItemDesc.FieldName = Nothing
        Me.lblItemDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItemDesc.Location = New System.Drawing.Point(211, 72)
        Me.lblItemDesc.Name = "lblItemDesc"
        Me.lblItemDesc.Size = New System.Drawing.Size(152, 19)
        Me.lblItemDesc.TabIndex = 1586
        Me.lblItemDesc.TextWrap = False
        '
        'lblItemCode
        '
        Me.lblItemCode.Controls.Add(Me.MyLabel5)
        Me.lblItemCode.Controls.Add(Me.MyLabel6)
        Me.lblItemCode.Controls.Add(Me.TxtFinder3)
        Me.lblItemCode.FieldName = Nothing
        Me.lblItemCode.Location = New System.Drawing.Point(2, 73)
        Me.lblItemCode.Name = "lblItemCode"
        Me.lblItemCode.Size = New System.Drawing.Size(58, 18)
        Me.lblItemCode.TabIndex = 1585
        Me.lblItemCode.Text = "Item Code"
        '
        'MyLabel5
        '
        Me.MyLabel5.AutoSize = False
        Me.MyLabel5.BorderVisible = True
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(211, 26)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(152, 19)
        Me.MyLabel5.TabIndex = 1542
        Me.MyLabel5.TextWrap = False
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Location = New System.Drawing.Point(0, 24)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(54, 18)
        Me.MyLabel6.TabIndex = 1541
        Me.MyLabel6.Text = "Route No"
        '
        'TxtFinder3
        '
        Me.TxtFinder3.CalculationExpression = Nothing
        Me.TxtFinder3.FieldCode = Nothing
        Me.TxtFinder3.FieldDesc = Nothing
        Me.TxtFinder3.FieldMaxLength = 0
        Me.TxtFinder3.FieldName = Nothing
        Me.TxtFinder3.isCalculatedField = False
        Me.TxtFinder3.IsSourceFromTable = False
        Me.TxtFinder3.IsSourceFromValueList = False
        Me.TxtFinder3.IsUnique = False
        Me.TxtFinder3.Location = New System.Drawing.Point(93, 26)
        Me.TxtFinder3.MendatroryField = False
        Me.TxtFinder3.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFinder3.MyLinkLable1 = Me.MyLabel6
        Me.TxtFinder3.MyLinkLable2 = Nothing
        Me.TxtFinder3.MyReadOnly = False
        Me.TxtFinder3.MyShowMasterFormButton = False
        Me.TxtFinder3.Name = "TxtFinder3"
        Me.TxtFinder3.ReferenceFieldDesc = Nothing
        Me.TxtFinder3.ReferenceFieldName = Nothing
        Me.TxtFinder3.ReferenceTableName = Nothing
        Me.TxtFinder3.Size = New System.Drawing.Size(115, 19)
        Me.TxtFinder3.TabIndex = 1540
        Me.TxtFinder3.Value = ""
        '
        'txtItemCode
        '
        Me.txtItemCode.CalculationExpression = Nothing
        Me.txtItemCode.FieldCode = Nothing
        Me.txtItemCode.FieldDesc = Nothing
        Me.txtItemCode.FieldMaxLength = 0
        Me.txtItemCode.FieldName = Nothing
        Me.txtItemCode.isCalculatedField = False
        Me.txtItemCode.IsSourceFromTable = False
        Me.txtItemCode.IsSourceFromValueList = False
        Me.txtItemCode.IsUnique = False
        Me.txtItemCode.Location = New System.Drawing.Point(91, 72)
        Me.txtItemCode.MendatroryField = False
        Me.txtItemCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemCode.MyLinkLable1 = Me.lblItemCode
        Me.txtItemCode.MyLinkLable2 = Nothing
        Me.txtItemCode.MyReadOnly = False
        Me.txtItemCode.MyShowMasterFormButton = False
        Me.txtItemCode.Name = "txtItemCode"
        Me.txtItemCode.ReferenceFieldDesc = Nothing
        Me.txtItemCode.ReferenceFieldName = Nothing
        Me.txtItemCode.ReferenceTableName = Nothing
        Me.txtItemCode.Size = New System.Drawing.Size(117, 19)
        Me.txtItemCode.TabIndex = 1584
        Me.txtItemCode.Value = ""
        '
        'rgbShiftType
        '
        Me.rgbShiftType.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.rgbShiftType.Controls.Add(Me.rbtnEvening)
        Me.rgbShiftType.Controls.Add(Me.rbtnMorning)
        Me.rgbShiftType.HeaderText = "Shift Type"
        Me.rgbShiftType.Location = New System.Drawing.Point(384, 28)
        Me.rgbShiftType.Name = "rgbShiftType"
        Me.rgbShiftType.Size = New System.Drawing.Size(167, 48)
        Me.rgbShiftType.TabIndex = 1583
        Me.rgbShiftType.Text = "Shift Type"
        '
        'rbtnEvening
        '
        Me.rbtnEvening.Location = New System.Drawing.Point(83, 21)
        Me.rbtnEvening.Name = "rbtnEvening"
        Me.rbtnEvening.Size = New System.Drawing.Size(59, 18)
        Me.rbtnEvening.TabIndex = 1
        Me.rbtnEvening.Text = "Evening"
        '
        'rbtnMorning
        '
        Me.rbtnMorning.Location = New System.Drawing.Point(6, 22)
        Me.rbtnMorning.Name = "rbtnMorning"
        Me.rbtnMorning.Size = New System.Drawing.Size(63, 18)
        Me.rbtnMorning.TabIndex = 0
        Me.rbtnMorning.Text = "Morning"
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
        Me.txtDate.Location = New System.Drawing.Point(417, 4)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.lblDate
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(134, 18)
        Me.txtDate.TabIndex = 1579
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011 11:29 AM"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'lblDate
        '
        Me.lblDate.FieldName = Nothing
        Me.lblDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate.Location = New System.Drawing.Point(384, 5)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(30, 16)
        Me.lblDate.TabIndex = 1573
        Me.lblDate.Text = "Date"
        '
        'lblDocNo
        '
        Me.lblDocNo.FieldName = Nothing
        Me.lblDocNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocNo.Location = New System.Drawing.Point(2, 3)
        Me.lblDocNo.Name = "lblDocNo"
        Me.lblDocNo.Size = New System.Drawing.Size(88, 16)
        Me.lblDocNo.TabIndex = 1576
        Me.lblDocNo.Text = "Document Code"
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(93, 2)
        Me.txtDocNo.MendatroryField = False
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Me.lblDocNo
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 32767
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(249, 19)
        Me.txtDocNo.TabIndex = 1572
        Me.txtDocNo.TabStop = False
        Me.txtDocNo.Value = ""
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.XpertERPDairySale.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(343, 2)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 21)
        Me.btnAddNew.TabIndex = 1574
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(91.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(805, 330)
        Me.RadPageViewPage2.Text = "Demand Detail"
        '
        'gv1
        '
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1.Name = "gv1"
        Me.gv1.Size = New System.Drawing.Size(805, 330)
        Me.gv1.TabIndex = 0
        '
        'btnProceed
        '
        Me.btnProceed.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnProceed.Location = New System.Drawing.Point(246, 7)
        Me.btnProceed.Name = "btnProceed"
        Me.btnProceed.Size = New System.Drawing.Size(74, 24)
        Me.btnProceed.TabIndex = 3
        Me.btnProceed.Text = "Proceed >>>"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Location = New System.Drawing.Point(6, 7)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(74, 24)
        Me.btnGo.TabIndex = 2
        Me.btnGo.Text = "GO >>"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(732, 7)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(86, 24)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Location = New System.Drawing.Point(166, 7)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(74, 24)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(86, 7)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(74, 24)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'frmDemandAdjustment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(826, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmDemandAdjustment"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Demand Adjustment"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.PerformLayout()
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.lblRouteNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblZoneCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rgbChangeItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.rgbChangeItem.ResumeLayout(False)
        Me.rgbChangeItem.PerformLayout()
        CType(Me.btnChange, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAddQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAddQty, System.ComponentModel.ISupportInitialize).EndInit()
        Me.lblAddQty.ResumeLayout(False)
        Me.lblAddQty.PerformLayout()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblChangeitemDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblChangeitem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.lblChangeitem.ResumeLayout(False)
        Me.lblChangeitem.PerformLayout()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDeductQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDeductQty, System.ComponentModel.ISupportInitialize).EndInit()
        Me.lblDeductQty.ResumeLayout(False)
        Me.lblDeductQty.PerformLayout()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rgbDecreaseOrder, System.ComponentModel.ISupportInitialize).EndInit()
        Me.rgbDecreaseOrder.ResumeLayout(False)
        CType(Me.btnDecrease1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rgbIncreaseOrder, System.ComponentModel.ISupportInitialize).EndInit()
        Me.rgbIncreaseOrder.ResumeLayout(False)
        CType(Me.btnIncrease1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rgbIDOrder, System.ComponentModel.ISupportInitialize).EndInit()
        Me.rgbIDOrder.ResumeLayout(False)
        Me.rgbIDOrder.PerformLayout()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MyLabel16.ResumeLayout(False)
        Me.MyLabel16.PerformLayout()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.lblPer.ResumeLayout(False)
        Me.lblPer.PerformLayout()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rgbMode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.rgbMode.ResumeLayout(False)
        Me.rgbMode.PerformLayout()
        CType(Me.rbtnFix, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnAutomatic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMinCrate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblUOMDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMinCrate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.lblMinCrate.ResumeLayout(False)
        Me.lblMinCrate.PerformLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblUOM, System.ComponentModel.ISupportInitialize).EndInit()
        Me.lblUOM.ResumeLayout(False)
        Me.lblUOM.PerformLayout()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkChangeItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDemandDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDemandDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblItemDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblItemCode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.lblItemCode.ResumeLayout(False)
        Me.lblItemCode.PerformLayout()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rgbShiftType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.rgbShiftType.ResumeLayout(False)
        Me.rgbShiftType.PerformLayout()
        CType(Me.rbtnEvening, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnMorning, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnProceed, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents btnClose As RadButton
    Friend WithEvents btnDelete As RadButton
    Friend WithEvents btnSave As RadButton
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents RadPageViewPage2 As RadPageViewPage
    Friend WithEvents gv1 As RadGridView
    Friend WithEvents SplitContainer3 As SplitContainer
    Friend WithEvents RadMenuItem2 As RadMenuItem
    Friend WithEvents RadMenu1 As RadMenu
    Friend WithEvents rmiImport As RadMenuItem
    Friend WithEvents rmiExport As RadMenuItem
    Friend WithEvents rgbChangeItem As RadGroupBox
    Friend WithEvents txtAddQty As RadTextBox
    Friend WithEvents lblAddQty As common.Controls.MyLabel
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents TxtFinder6 As common.UserControls.txtFinder
    Friend WithEvents lblChangeitemDesc As common.Controls.MyLabel
    Friend WithEvents lblChangeitem As common.Controls.MyLabel
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents TxtFinder5 As common.UserControls.txtFinder
    Friend WithEvents txtChangeItemCode As common.UserControls.txtFinder
    Friend WithEvents txtDeductQty As RadTextBox
    Friend WithEvents lblDeductQty As common.Controls.MyLabel
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents TxtFinder4 As common.UserControls.txtFinder
    Friend WithEvents txtMinCrate As RadTextBox
    Friend WithEvents lblMinCrate As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents TxtFinder2 As common.UserControls.txtFinder
    Friend WithEvents rgbDecreaseOrder As RadGroupBox
    Friend WithEvents btnDecrease1 As RadButton
    Friend WithEvents rgbIncreaseOrder As RadGroupBox
    Friend WithEvents btnIncrease1 As RadButton
    Friend WithEvents rgbIDOrder As RadGroupBox
    Friend WithEvents txtQty As RadTextBox
    Friend WithEvents rgbMode As RadGroupBox
    Friend WithEvents rbtnFix As RadRadioButton
    Friend WithEvents rbtnAutomatic As RadRadioButton
    Friend WithEvents lblUOMDesc As common.Controls.MyLabel
    Friend WithEvents lblUOM As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents TxtFinder1 As common.UserControls.txtFinder
    Friend WithEvents txtUOM As common.UserControls.txtFinder
    Friend WithEvents chkChangeItem As RadCheckBox
    Friend WithEvents txtDemandDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblDemandDate As common.Controls.MyLabel
    Friend WithEvents lblItemDesc As common.Controls.MyLabel
    Friend WithEvents lblItemCode As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents TxtFinder3 As common.UserControls.txtFinder
    Friend WithEvents txtItemCode As common.UserControls.txtFinder
    Friend WithEvents rgbShiftType As RadGroupBox
    Friend WithEvents rbtnEvening As RadRadioButton
    Friend WithEvents rbtnMorning As RadRadioButton
    Friend WithEvents lblRouteNo As common.Controls.MyLabel
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblDate As common.Controls.MyLabel
    Friend WithEvents lblZoneCode As common.Controls.MyLabel
    Friend WithEvents lblDocNo As common.Controls.MyLabel
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents btnAddNew As RadButton
    Friend WithEvents txtRouteCode As common.UserControls.txtMultiSelectFinder
    Friend WithEvents txtZoneCode As common.UserControls.txtMultiSelectFinder
    Friend WithEvents txtPer As RadTextBox
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents MyLabel17 As common.Controls.MyLabel
    Friend WithEvents MyLabel18 As common.Controls.MyLabel
    Friend WithEvents TxtFinder8 As common.UserControls.txtFinder
    Friend WithEvents lblPer As common.Controls.MyLabel
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
    Friend WithEvents TxtFinder7 As common.UserControls.txtFinder
    Friend WithEvents btnGo As RadButton
    Friend WithEvents btnChange As RadButton
    Friend WithEvents btnProceed As RadButton
End Class
