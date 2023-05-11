<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMilkSRNMCC
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMilkSRNMCC))
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.RadButton2 = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.txtShiftEnd = New common.UserControls.txtMultiSelectFinder()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cboDockCollectionMilkType = New common.Controls.MyComboBox()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.btnDrillDown = New Telerik.WinControls.UI.RadButton()
        Me.fndVehicleCode = New common.Controls.MyTextBox()
        Me.fndVSPCode = New common.Controls.MyTextBox()
        Me.fndRouteCOde = New common.Controls.MyTextBox()
        Me.fndVlcCode = New common.Controls.MyTextBox()
        Me.fndMccCode = New common.Controls.MyTextBox()
        Me.lblMccName = New common.Controls.MyLabel()
        Me.lblTransporter = New common.Controls.MyLabel()
        Me.txtCustomerName = New common.Controls.MyTextBox()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.lblTotalWeight = New common.Controls.MyLabel()
        Me.txtsampleNo = New common.MyNumBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lblVehicleCode = New common.Controls.MyLabel()
        Me.lblVehicleDesc = New common.Controls.MyLabel()
        Me.lblRouteCode = New common.Controls.MyLabel()
        Me.lblRouteDesc = New common.Controls.MyLabel()
        Me.lblVSPCode = New common.Controls.MyLabel()
        Me.lblVSPDesc = New common.Controls.MyLabel()
        Me.lblVLCCode = New common.Controls.MyLabel()
        Me.lblVLCDesc = New common.Controls.MyLabel()
        Me.lblBOMStatus = New common.Controls.MyLabel()
        Me.cboShift = New common.Controls.MyComboBox()
        Me.lblDocDate = New common.Controls.MyLabel()
        Me.dtpDocDate = New common.Controls.MyDateTimePicker()
        Me.lblMCCCode = New common.Controls.MyLabel()
        Me.lblCode = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.pvpCustomFields = New Telerik.WinControls.UI.RadPageViewPage()
        Me.Attachments = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.btnJE = New Telerik.WinControls.UI.RadButton()
        Me.btnHistory = New Telerik.WinControls.UI.RadButton()
        Me.btnShowInventory = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenu()
        Me.rdmenufile1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.BtnSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.BtnDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.cboDockCollectionMilkType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDrillDown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fndVehicleCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fndVSPCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fndRouteCOde, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fndVlcCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fndMccCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMccName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTransporter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustomerName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotalWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtsampleNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVehicleCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVehicleDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRouteCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRouteDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVSPCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVSPDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVLCCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVLCDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBOMStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboShift, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDocDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMCCCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Attachments.SuspendLayout()
        CType(Me.btnJE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnShowInventory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnJE)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnHistory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnShowInventory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Size = New System.Drawing.Size(988, 506)
        Me.SplitContainer1.SplitterDistance = 470
        Me.SplitContainer1.TabIndex = 42
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.pvpCustomFields)
        Me.RadPageView1.Controls.Add(Me.Attachments)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(988, 470)
        Me.RadPageView1.TabIndex = 0
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.Panel1)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(62.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(967, 422)
        Me.RadPageViewPage1.Text = "Milk SRN"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.RadButton2)
        Me.Panel1.Controls.Add(Me.MyLabel4)
        Me.Panel1.Controls.Add(Me.RadButton1)
        Me.Panel1.Controls.Add(Me.txtShiftEnd)
        Me.Panel1.Location = New System.Drawing.Point(3, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(962, 27)
        Me.Panel1.TabIndex = 32
        Me.Panel1.Visible = False
        '
        'RadButton2
        '
        Me.RadButton2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton2.Location = New System.Drawing.Point(937, 4)
        Me.RadButton2.Name = "RadButton2"
        Me.RadButton2.Size = New System.Drawing.Size(19, 19)
        Me.RadButton2.TabIndex = 32
        Me.RadButton2.Text = "X"
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(3, 4)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(157, 18)
        Me.MyLabel4.TabIndex = 30
        Me.MyLabel4.Text = "Shift End For consuption Entry"
        '
        'RadButton1
        '
        Me.RadButton1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton1.Location = New System.Drawing.Point(914, 4)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(19, 19)
        Me.RadButton1.TabIndex = 31
        Me.RadButton1.Text = ">>"
        '
        'txtShiftEnd
        '
        Me.txtShiftEnd.arrDispalyMember = Nothing
        Me.txtShiftEnd.arrValueMember = Nothing
        Me.txtShiftEnd.Location = New System.Drawing.Point(166, 4)
        Me.txtShiftEnd.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShiftEnd.MyLinkLable1 = Me.MyLabel4
        Me.txtShiftEnd.MyLinkLable2 = Nothing
        Me.txtShiftEnd.MyNullText = "All"
        Me.txtShiftEnd.Name = "txtShiftEnd"
        Me.txtShiftEnd.Size = New System.Drawing.Size(746, 19)
        Me.txtShiftEnd.TabIndex = 29
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.cboDockCollectionMilkType)
        Me.RadGroupBox1.Controls.Add(Me.txtCode)
        Me.RadGroupBox1.Controls.Add(Me.btnDrillDown)
        Me.RadGroupBox1.Controls.Add(Me.fndVehicleCode)
        Me.RadGroupBox1.Controls.Add(Me.fndVSPCode)
        Me.RadGroupBox1.Controls.Add(Me.fndRouteCOde)
        Me.RadGroupBox1.Controls.Add(Me.fndVlcCode)
        Me.RadGroupBox1.Controls.Add(Me.fndMccCode)
        Me.RadGroupBox1.Controls.Add(Me.lblMccName)
        Me.RadGroupBox1.Controls.Add(Me.lblTransporter)
        Me.RadGroupBox1.Controls.Add(Me.txtCustomerName)
        Me.RadGroupBox1.Controls.Add(Me.btnnew)
        Me.RadGroupBox1.Controls.Add(Me.lblTotalWeight)
        Me.RadGroupBox1.Controls.Add(Me.txtsampleNo)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.lblVehicleCode)
        Me.RadGroupBox1.Controls.Add(Me.lblVehicleDesc)
        Me.RadGroupBox1.Controls.Add(Me.lblRouteCode)
        Me.RadGroupBox1.Controls.Add(Me.lblRouteDesc)
        Me.RadGroupBox1.Controls.Add(Me.lblVSPCode)
        Me.RadGroupBox1.Controls.Add(Me.lblVSPDesc)
        Me.RadGroupBox1.Controls.Add(Me.lblVLCCode)
        Me.RadGroupBox1.Controls.Add(Me.lblVLCDesc)
        Me.RadGroupBox1.Controls.Add(Me.lblBOMStatus)
        Me.RadGroupBox1.Controls.Add(Me.cboShift)
        Me.RadGroupBox1.Controls.Add(Me.lblDocDate)
        Me.RadGroupBox1.Controls.Add(Me.dtpDocDate)
        Me.RadGroupBox1.Controls.Add(Me.lblMCCCode)
        Me.RadGroupBox1.Controls.Add(Me.lblCode)
        Me.RadGroupBox1.Controls.Add(Me.UsLock1)
        Me.RadGroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = "SRN Head"
        Me.RadGroupBox1.Location = New System.Drawing.Point(2, 25)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(967, 180)
        Me.RadGroupBox1.TabIndex = 2
        Me.RadGroupBox1.Text = "SRN Head"
        '
        'cboDockCollectionMilkType
        '
        Me.cboDockCollectionMilkType.AutoCompleteDisplayMember = Nothing
        Me.cboDockCollectionMilkType.AutoCompleteValueMember = Nothing
        Me.cboDockCollectionMilkType.CalculationExpression = Nothing
        Me.cboDockCollectionMilkType.DropDownAnimationEnabled = True
        Me.cboDockCollectionMilkType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboDockCollectionMilkType.Enabled = False
        Me.cboDockCollectionMilkType.FieldCode = Nothing
        Me.cboDockCollectionMilkType.FieldDesc = Nothing
        Me.cboDockCollectionMilkType.FieldMaxLength = 0
        Me.cboDockCollectionMilkType.FieldName = Nothing
        Me.cboDockCollectionMilkType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDockCollectionMilkType.isCalculatedField = False
        Me.cboDockCollectionMilkType.IsSourceFromTable = False
        Me.cboDockCollectionMilkType.IsSourceFromValueList = False
        Me.cboDockCollectionMilkType.IsUnique = False
        Me.cboDockCollectionMilkType.Location = New System.Drawing.Point(701, 16)
        Me.cboDockCollectionMilkType.MendatroryField = True
        Me.cboDockCollectionMilkType.MyLinkLable1 = Nothing
        Me.cboDockCollectionMilkType.MyLinkLable2 = Nothing
        Me.cboDockCollectionMilkType.Name = "cboDockCollectionMilkType"
        Me.cboDockCollectionMilkType.ReferenceFieldDesc = Nothing
        Me.cboDockCollectionMilkType.ReferenceFieldName = Nothing
        Me.cboDockCollectionMilkType.ReferenceTableName = Nothing
        Me.cboDockCollectionMilkType.Size = New System.Drawing.Size(85, 18)
        Me.cboDockCollectionMilkType.TabIndex = 1028
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(111, 15)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Nothing
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 32767
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(280, 21)
        Me.txtCode.TabIndex = 65
        Me.txtCode.Value = ""
        '
        'btnDrillDown
        '
        Me.btnDrillDown.Image = CType(resources.GetObject("btnDrillDown.Image"), System.Drawing.Image)
        Me.btnDrillDown.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnDrillDown.Location = New System.Drawing.Point(412, 16)
        Me.btnDrillDown.Name = "btnDrillDown"
        Me.btnDrillDown.Size = New System.Drawing.Size(20, 20)
        Me.btnDrillDown.TabIndex = 64
        '
        'fndVehicleCode
        '
        Me.fndVehicleCode.CalculationExpression = Nothing
        Me.fndVehicleCode.FieldCode = Nothing
        Me.fndVehicleCode.FieldDesc = Nothing
        Me.fndVehicleCode.FieldMaxLength = 0
        Me.fndVehicleCode.FieldName = Nothing
        Me.fndVehicleCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndVehicleCode.isCalculatedField = False
        Me.fndVehicleCode.IsSourceFromTable = False
        Me.fndVehicleCode.IsSourceFromValueList = False
        Me.fndVehicleCode.IsUnique = False
        Me.fndVehicleCode.Location = New System.Drawing.Point(111, 131)
        Me.fndVehicleCode.MaxLength = 200
        Me.fndVehicleCode.MendatroryField = False
        Me.fndVehicleCode.MyLinkLable1 = Nothing
        Me.fndVehicleCode.MyLinkLable2 = Nothing
        Me.fndVehicleCode.Name = "fndVehicleCode"
        Me.fndVehicleCode.ReadOnly = True
        Me.fndVehicleCode.ReferenceFieldDesc = Nothing
        Me.fndVehicleCode.ReferenceFieldName = Nothing
        Me.fndVehicleCode.ReferenceTableName = Nothing
        Me.fndVehicleCode.Size = New System.Drawing.Size(321, 18)
        Me.fndVehicleCode.TabIndex = 62
        '
        'fndVSPCode
        '
        Me.fndVSPCode.CalculationExpression = Nothing
        Me.fndVSPCode.FieldCode = Nothing
        Me.fndVSPCode.FieldDesc = Nothing
        Me.fndVSPCode.FieldMaxLength = 0
        Me.fndVSPCode.FieldName = Nothing
        Me.fndVSPCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndVSPCode.isCalculatedField = False
        Me.fndVSPCode.IsSourceFromTable = False
        Me.fndVSPCode.IsSourceFromValueList = False
        Me.fndVSPCode.IsUnique = False
        Me.fndVSPCode.Location = New System.Drawing.Point(111, 86)
        Me.fndVSPCode.MaxLength = 200
        Me.fndVSPCode.MendatroryField = False
        Me.fndVSPCode.MyLinkLable1 = Nothing
        Me.fndVSPCode.MyLinkLable2 = Nothing
        Me.fndVSPCode.Name = "fndVSPCode"
        Me.fndVSPCode.ReadOnly = True
        Me.fndVSPCode.ReferenceFieldDesc = Nothing
        Me.fndVSPCode.ReferenceFieldName = Nothing
        Me.fndVSPCode.ReferenceTableName = Nothing
        Me.fndVSPCode.Size = New System.Drawing.Size(321, 18)
        Me.fndVSPCode.TabIndex = 62
        '
        'fndRouteCOde
        '
        Me.fndRouteCOde.CalculationExpression = Nothing
        Me.fndRouteCOde.FieldCode = Nothing
        Me.fndRouteCOde.FieldDesc = Nothing
        Me.fndRouteCOde.FieldMaxLength = 0
        Me.fndRouteCOde.FieldName = Nothing
        Me.fndRouteCOde.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndRouteCOde.isCalculatedField = False
        Me.fndRouteCOde.IsSourceFromTable = False
        Me.fndRouteCOde.IsSourceFromValueList = False
        Me.fndRouteCOde.IsUnique = False
        Me.fndRouteCOde.Location = New System.Drawing.Point(111, 109)
        Me.fndRouteCOde.MaxLength = 200
        Me.fndRouteCOde.MendatroryField = False
        Me.fndRouteCOde.MyLinkLable1 = Nothing
        Me.fndRouteCOde.MyLinkLable2 = Nothing
        Me.fndRouteCOde.Name = "fndRouteCOde"
        Me.fndRouteCOde.ReadOnly = True
        Me.fndRouteCOde.ReferenceFieldDesc = Nothing
        Me.fndRouteCOde.ReferenceFieldName = Nothing
        Me.fndRouteCOde.ReferenceTableName = Nothing
        Me.fndRouteCOde.Size = New System.Drawing.Size(321, 18)
        Me.fndRouteCOde.TabIndex = 61
        '
        'fndVlcCode
        '
        Me.fndVlcCode.CalculationExpression = Nothing
        Me.fndVlcCode.FieldCode = Nothing
        Me.fndVlcCode.FieldDesc = Nothing
        Me.fndVlcCode.FieldMaxLength = 0
        Me.fndVlcCode.FieldName = Nothing
        Me.fndVlcCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndVlcCode.isCalculatedField = False
        Me.fndVlcCode.IsSourceFromTable = False
        Me.fndVlcCode.IsSourceFromValueList = False
        Me.fndVlcCode.IsUnique = False
        Me.fndVlcCode.Location = New System.Drawing.Point(111, 64)
        Me.fndVlcCode.MaxLength = 200
        Me.fndVlcCode.MendatroryField = False
        Me.fndVlcCode.MyLinkLable1 = Nothing
        Me.fndVlcCode.MyLinkLable2 = Nothing
        Me.fndVlcCode.Name = "fndVlcCode"
        Me.fndVlcCode.ReadOnly = True
        Me.fndVlcCode.ReferenceFieldDesc = Nothing
        Me.fndVlcCode.ReferenceFieldName = Nothing
        Me.fndVlcCode.ReferenceTableName = Nothing
        Me.fndVlcCode.Size = New System.Drawing.Size(321, 18)
        Me.fndVlcCode.TabIndex = 55
        '
        'fndMccCode
        '
        Me.fndMccCode.CalculationExpression = Nothing
        Me.fndMccCode.FieldCode = Nothing
        Me.fndMccCode.FieldDesc = Nothing
        Me.fndMccCode.FieldMaxLength = 0
        Me.fndMccCode.FieldName = Nothing
        Me.fndMccCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndMccCode.isCalculatedField = False
        Me.fndMccCode.IsSourceFromTable = False
        Me.fndMccCode.IsSourceFromValueList = False
        Me.fndMccCode.IsUnique = False
        Me.fndMccCode.Location = New System.Drawing.Point(111, 41)
        Me.fndMccCode.MaxLength = 200
        Me.fndMccCode.MendatroryField = False
        Me.fndMccCode.MyLinkLable1 = Nothing
        Me.fndMccCode.MyLinkLable2 = Nothing
        Me.fndMccCode.Name = "fndMccCode"
        Me.fndMccCode.ReadOnly = True
        Me.fndMccCode.ReferenceFieldDesc = Nothing
        Me.fndMccCode.ReferenceFieldName = Nothing
        Me.fndMccCode.ReferenceTableName = Nothing
        Me.fndMccCode.Size = New System.Drawing.Size(321, 18)
        Me.fndMccCode.TabIndex = 55
        '
        'lblMccName
        '
        Me.lblMccName.AutoSize = False
        Me.lblMccName.BorderVisible = True
        Me.lblMccName.FieldName = Nothing
        Me.lblMccName.Location = New System.Drawing.Point(458, 41)
        Me.lblMccName.Name = "lblMccName"
        Me.lblMccName.Size = New System.Drawing.Size(478, 19)
        Me.lblMccName.TabIndex = 25
        '
        'lblTransporter
        '
        Me.lblTransporter.AutoSize = False
        Me.lblTransporter.BorderVisible = True
        Me.lblTransporter.FieldName = Nothing
        Me.lblTransporter.Location = New System.Drawing.Point(654, 154)
        Me.lblTransporter.Name = "lblTransporter"
        Me.lblTransporter.Size = New System.Drawing.Size(282, 19)
        Me.lblTransporter.TabIndex = 34
        '
        'txtCustomerName
        '
        Me.txtCustomerName.CalculationExpression = Nothing
        Me.txtCustomerName.FieldCode = Nothing
        Me.txtCustomerName.FieldDesc = Nothing
        Me.txtCustomerName.FieldMaxLength = 0
        Me.txtCustomerName.FieldName = Nothing
        Me.txtCustomerName.isCalculatedField = False
        Me.txtCustomerName.IsSourceFromTable = False
        Me.txtCustomerName.IsSourceFromValueList = False
        Me.txtCustomerName.IsUnique = False
        Me.txtCustomerName.Location = New System.Drawing.Point(528, 153)
        Me.txtCustomerName.MaxLength = 50
        Me.txtCustomerName.MendatroryField = False
        Me.txtCustomerName.MyLinkLable1 = Nothing
        Me.txtCustomerName.MyLinkLable2 = Nothing
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.ReferenceFieldDesc = Nothing
        Me.txtCustomerName.ReferenceFieldName = Nothing
        Me.txtCustomerName.ReferenceTableName = Nothing
        Me.txtCustomerName.Size = New System.Drawing.Size(120, 20)
        Me.txtCustomerName.TabIndex = 45
        '
        'btnnew
        '
        Me.btnnew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(397, 16)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(15, 20)
        Me.btnnew.TabIndex = 44
        '
        'lblTotalWeight
        '
        Me.lblTotalWeight.FieldName = Nothing
        Me.lblTotalWeight.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblTotalWeight.Location = New System.Drawing.Point(457, 155)
        Me.lblTotalWeight.Name = "lblTotalWeight"
        Me.lblTotalWeight.Size = New System.Drawing.Size(65, 16)
        Me.lblTotalWeight.TabIndex = 43
        Me.lblTotalWeight.Text = "Transporter"
        '
        'txtsampleNo
        '
        Me.txtsampleNo.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtsampleNo.CalculationExpression = Nothing
        Me.txtsampleNo.DecimalPlaces = 0
        Me.txtsampleNo.Enabled = False
        Me.txtsampleNo.FieldCode = Nothing
        Me.txtsampleNo.FieldDesc = Nothing
        Me.txtsampleNo.FieldMaxLength = 0
        Me.txtsampleNo.FieldName = Nothing
        Me.txtsampleNo.isCalculatedField = False
        Me.txtsampleNo.IsSourceFromTable = False
        Me.txtsampleNo.IsSourceFromValueList = False
        Me.txtsampleNo.IsUnique = False
        Me.txtsampleNo.Location = New System.Drawing.Point(111, 153)
        Me.txtsampleNo.MendatroryField = True
        Me.txtsampleNo.MyLinkLable1 = Me.MyLabel1
        Me.txtsampleNo.MyLinkLable2 = Nothing
        Me.txtsampleNo.Name = "txtsampleNo"
        Me.txtsampleNo.ReferenceFieldDesc = Nothing
        Me.txtsampleNo.ReferenceFieldName = Nothing
        Me.txtsampleNo.ReferenceTableName = Nothing
        Me.txtsampleNo.Size = New System.Drawing.Size(101, 20)
        Me.txtsampleNo.TabIndex = 36
        Me.txtsampleNo.Text = "0"
        Me.txtsampleNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtsampleNo.Value = 0R
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(15, 155)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(62, 16)
        Me.MyLabel1.TabIndex = 37
        Me.MyLabel1.Text = "Sample No"
        '
        'lblVehicleCode
        '
        Me.lblVehicleCode.FieldName = Nothing
        Me.lblVehicleCode.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblVehicleCode.Location = New System.Drawing.Point(15, 131)
        Me.lblVehicleCode.Name = "lblVehicleCode"
        Me.lblVehicleCode.Size = New System.Drawing.Size(72, 18)
        Me.lblVehicleCode.TabIndex = 32
        Me.lblVehicleCode.Text = "Vehicle Code"
        '
        'lblVehicleDesc
        '
        Me.lblVehicleDesc.AutoSize = False
        Me.lblVehicleDesc.BorderVisible = True
        Me.lblVehicleDesc.FieldName = Nothing
        Me.lblVehicleDesc.Location = New System.Drawing.Point(458, 131)
        Me.lblVehicleDesc.Name = "lblVehicleDesc"
        Me.lblVehicleDesc.Size = New System.Drawing.Size(479, 19)
        Me.lblVehicleDesc.TabIndex = 33
        '
        'lblRouteCode
        '
        Me.lblRouteCode.FieldName = Nothing
        Me.lblRouteCode.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblRouteCode.Location = New System.Drawing.Point(15, 109)
        Me.lblRouteCode.Name = "lblRouteCode"
        Me.lblRouteCode.Size = New System.Drawing.Size(65, 18)
        Me.lblRouteCode.TabIndex = 29
        Me.lblRouteCode.Text = "Route Code"
        '
        'lblRouteDesc
        '
        Me.lblRouteDesc.AutoSize = False
        Me.lblRouteDesc.BorderVisible = True
        Me.lblRouteDesc.FieldName = Nothing
        Me.lblRouteDesc.Location = New System.Drawing.Point(458, 109)
        Me.lblRouteDesc.Name = "lblRouteDesc"
        Me.lblRouteDesc.Size = New System.Drawing.Size(479, 19)
        Me.lblRouteDesc.TabIndex = 30
        '
        'lblVSPCode
        '
        Me.lblVSPCode.FieldName = Nothing
        Me.lblVSPCode.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblVSPCode.Location = New System.Drawing.Point(14, 86)
        Me.lblVSPCode.Name = "lblVSPCode"
        Me.lblVSPCode.Size = New System.Drawing.Size(55, 18)
        Me.lblVSPCode.TabIndex = 26
        Me.lblVSPCode.Text = "VSP Code"
        '
        'lblVSPDesc
        '
        Me.lblVSPDesc.AutoSize = False
        Me.lblVSPDesc.BorderVisible = True
        Me.lblVSPDesc.FieldName = Nothing
        Me.lblVSPDesc.Location = New System.Drawing.Point(458, 86)
        Me.lblVSPDesc.Name = "lblVSPDesc"
        Me.lblVSPDesc.Size = New System.Drawing.Size(478, 19)
        Me.lblVSPDesc.TabIndex = 27
        '
        'lblVLCCode
        '
        Me.lblVLCCode.FieldName = Nothing
        Me.lblVLCCode.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblVLCCode.Location = New System.Drawing.Point(14, 64)
        Me.lblVLCCode.Name = "lblVLCCode"
        Me.lblVLCCode.Size = New System.Drawing.Size(55, 18)
        Me.lblVLCCode.TabIndex = 23
        Me.lblVLCCode.Text = "VLC Code"
        '
        'lblVLCDesc
        '
        Me.lblVLCDesc.AutoSize = False
        Me.lblVLCDesc.BorderVisible = True
        Me.lblVLCDesc.FieldName = Nothing
        Me.lblVLCDesc.Location = New System.Drawing.Point(458, 64)
        Me.lblVLCDesc.Name = "lblVLCDesc"
        Me.lblVLCDesc.Size = New System.Drawing.Size(478, 19)
        Me.lblVLCDesc.TabIndex = 24
        '
        'lblBOMStatus
        '
        Me.lblBOMStatus.FieldName = Nothing
        Me.lblBOMStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBOMStatus.Location = New System.Drawing.Point(226, 155)
        Me.lblBOMStatus.Name = "lblBOMStatus"
        Me.lblBOMStatus.Size = New System.Drawing.Size(29, 16)
        Me.lblBOMStatus.TabIndex = 21
        Me.lblBOMStatus.Text = "Shift"
        '
        'cboShift
        '
        Me.cboShift.AutoCompleteDisplayMember = Nothing
        Me.cboShift.AutoCompleteValueMember = Nothing
        Me.cboShift.CalculationExpression = Nothing
        Me.cboShift.DropDownAnimationEnabled = True
        Me.cboShift.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboShift.Enabled = False
        Me.cboShift.FieldCode = Nothing
        Me.cboShift.FieldDesc = Nothing
        Me.cboShift.FieldMaxLength = 0
        Me.cboShift.FieldName = Nothing
        Me.cboShift.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboShift.isCalculatedField = False
        Me.cboShift.IsSourceFromTable = False
        Me.cboShift.IsSourceFromValueList = False
        Me.cboShift.IsUnique = False
        RadListDataItem1.Text = "M"
        RadListDataItem2.Text = "E"
        Me.cboShift.Items.Add(RadListDataItem1)
        Me.cboShift.Items.Add(RadListDataItem2)
        Me.cboShift.Location = New System.Drawing.Point(261, 154)
        Me.cboShift.MendatroryField = True
        Me.cboShift.MyLinkLable1 = Me.lblBOMStatus
        Me.cboShift.MyLinkLable2 = Nothing
        Me.cboShift.Name = "cboShift"
        Me.cboShift.ReferenceFieldDesc = Nothing
        Me.cboShift.ReferenceFieldName = Nothing
        Me.cboShift.ReferenceTableName = Nothing
        Me.cboShift.Size = New System.Drawing.Size(171, 18)
        Me.cboShift.TabIndex = 20
        '
        'lblDocDate
        '
        Me.lblDocDate.FieldName = Nothing
        Me.lblDocDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocDate.Location = New System.Drawing.Point(458, 17)
        Me.lblDocDate.Name = "lblDocDate"
        Me.lblDocDate.Size = New System.Drawing.Size(57, 16)
        Me.lblDocDate.TabIndex = 19
        Me.lblDocDate.Text = "SRN Date"
        '
        'dtpDocDate
        '
        Me.dtpDocDate.CalculationExpression = Nothing
        Me.dtpDocDate.CustomFormat = "dd/MM/yyyy hh:mm:ss tt"
        Me.dtpDocDate.FieldCode = Nothing
        Me.dtpDocDate.FieldDesc = Nothing
        Me.dtpDocDate.FieldMaxLength = 0
        Me.dtpDocDate.FieldName = Nothing
        Me.dtpDocDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDocDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDocDate.isCalculatedField = False
        Me.dtpDocDate.IsSourceFromTable = False
        Me.dtpDocDate.IsSourceFromValueList = False
        Me.dtpDocDate.IsUnique = False
        Me.dtpDocDate.Location = New System.Drawing.Point(528, 16)
        Me.dtpDocDate.MendatroryField = True
        Me.dtpDocDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDocDate.MyLinkLable1 = Me.lblDocDate
        Me.dtpDocDate.MyLinkLable2 = Nothing
        Me.dtpDocDate.Name = "dtpDocDate"
        Me.dtpDocDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDocDate.ReferenceFieldDesc = Nothing
        Me.dtpDocDate.ReferenceFieldName = Nothing
        Me.dtpDocDate.ReferenceTableName = Nothing
        Me.dtpDocDate.Size = New System.Drawing.Size(167, 18)
        Me.dtpDocDate.TabIndex = 18
        Me.dtpDocDate.TabStop = False
        Me.dtpDocDate.Text = "03/05/2011 12:00:00 AM"
        Me.dtpDocDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblMCCCode
        '
        Me.lblMCCCode.FieldName = Nothing
        Me.lblMCCCode.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblMCCCode.Location = New System.Drawing.Point(14, 41)
        Me.lblMCCCode.Name = "lblMCCCode"
        Me.lblMCCCode.Size = New System.Drawing.Size(60, 18)
        Me.lblMCCCode.TabIndex = 13
        Me.lblMCCCode.Text = "MCC Code"
        '
        'lblCode
        '
        Me.lblCode.FieldName = Nothing
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblCode.Location = New System.Drawing.Point(13, 17)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(48, 16)
        Me.lblCode.TabIndex = 10
        Me.lblCode.Text = "SRN No"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(838, 15)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 11
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox2.Controls.Add(Me.gv1)
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "SRN Detail"
        Me.RadGroupBox2.Location = New System.Drawing.Point(0, 210)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(967, 212)
        Me.RadGroupBox2.TabIndex = 1
        Me.RadGroupBox2.Text = "SRN Detail"
        '
        'gv1
        '
        Me.gv1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(10, 20)
        '
        '
        '
        Me.gv1.MasterTemplate.AllowColumnHeaderContextMenu = False
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(947, 182)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        '
        'pvpCustomFields
        '
        Me.pvpCustomFields.ItemSize = New System.Drawing.SizeF(86.0!, 28.0!)
        Me.pvpCustomFields.Location = New System.Drawing.Point(10, 37)
        Me.pvpCustomFields.Name = "pvpCustomFields"
        Me.pvpCustomFields.Size = New System.Drawing.Size(967, 422)
        Me.pvpCustomFields.Text = "Custom Fields"
        '
        'Attachments
        '
        Me.Attachments.Controls.Add(Me.UcAttachment1)
        Me.Attachments.ItemSize = New System.Drawing.SizeF(79.0!, 28.0!)
        Me.Attachments.Location = New System.Drawing.Point(10, 37)
        Me.Attachments.Name = "Attachments"
        Me.Attachments.Size = New System.Drawing.Size(967, 422)
        Me.Attachments.Text = "Attachments"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(967, 422)
        Me.UcAttachment1.TabIndex = 0
        '
        'btnJE
        '
        Me.btnJE.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnJE.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnJE.Location = New System.Drawing.Point(835, 7)
        Me.btnJE.Name = "btnJE"
        Me.btnJE.Size = New System.Drawing.Size(70, 18)
        Me.btnJE.TabIndex = 63
        Me.btnJE.Text = "Show JE"
        '
        'btnHistory
        '
        Me.btnHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnHistory.Location = New System.Drawing.Point(192, 7)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Size = New System.Drawing.Size(71, 18)
        Me.btnHistory.TabIndex = 6
        Me.btnHistory.Text = "&History"
        '
        'btnShowInventory
        '
        Me.btnShowInventory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnShowInventory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShowInventory.Location = New System.Drawing.Point(87, 7)
        Me.btnShowInventory.Name = "btnShowInventory"
        Me.btnShowInventory.Size = New System.Drawing.Size(99, 18)
        Me.btnShowInventory.TabIndex = 3
        Me.btnShowInventory.Text = "Show Inventory"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(911, 7)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(15, 7)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(66, 18)
        Me.btnPrint.TabIndex = 0
        Me.btnPrint.Text = "Print"
        '
        'rdmenufile
        '
        Me.rdmenufile.BackColor = System.Drawing.Color.Transparent
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenufile1})
        Me.rdmenufile.Location = New System.Drawing.Point(0, 0)
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Size = New System.Drawing.Size(988, 20)
        Me.rdmenufile.TabIndex = 6
        '
        'rdmenufile1
        '
        Me.rdmenufile1.AccessibleDescription = "File"
        Me.rdmenufile1.AccessibleName = "File"
        Me.rdmenufile1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.BtnSaveLayout, Me.BtnDeleteLayout})
        Me.rdmenufile1.Name = "rdmenufile1"
        Me.rdmenufile1.Text = "Settings"
        '
        'BtnSaveLayout
        '
        Me.BtnSaveLayout.Name = "BtnSaveLayout"
        Me.BtnSaveLayout.Text = "Save Layout"
        '
        'BtnDeleteLayout
        '
        Me.BtnDeleteLayout.Name = "BtnDeleteLayout"
        Me.BtnDeleteLayout.Text = "Delete Layout"
        '
        'frmMilkSRNMCC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(988, 526)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.rdmenufile)
        Me.Name = "frmMilkSRNMCC"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Milk SRN"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.cboDockCollectionMilkType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDrillDown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fndVehicleCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fndVSPCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fndRouteCOde, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fndVlcCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fndMccCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMccName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTransporter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustomerName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotalWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtsampleNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVehicleCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVehicleDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRouteCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRouteDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVSPCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVSPDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVLCCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVLCDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBOMStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboShift, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDocDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMCCCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Attachments.ResumeLayout(False)
        CType(Me.btnJE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnShowInventory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents pvpCustomFields As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcCustomFields1 As ERP.ucCustomFields
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents lblMCCCode As common.Controls.MyLabel
    Friend WithEvents lblDocDate As common.Controls.MyLabel
    Friend WithEvents dtpDocDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblBOMStatus As common.Controls.MyLabel
    Friend WithEvents cboShift As common.Controls.MyComboBox
    Friend WithEvents lblVLCCode As common.Controls.MyLabel
    Friend WithEvents lblVLCDesc As common.Controls.MyLabel
    Friend WithEvents lblVSPCode As common.Controls.MyLabel
    Friend WithEvents lblVSPDesc As common.Controls.MyLabel
    Friend WithEvents lblRouteCode As common.Controls.MyLabel
    Friend WithEvents lblRouteDesc As common.Controls.MyLabel
    Friend WithEvents lblVehicleCode As common.Controls.MyLabel
    Friend WithEvents lblVehicleDesc As common.Controls.MyLabel
    Friend WithEvents txtsampleNo As common.MyNumBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents lblTotalWeight As common.Controls.MyLabel
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtCustomerName As common.Controls.MyTextBox
    Friend WithEvents lblTransporter As common.Controls.MyLabel
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenu
    Friend WithEvents rdmenufile1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents BtnSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents BtnDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents lblMccName As common.Controls.MyLabel
    Friend WithEvents fndVlcCode As common.Controls.MyTextBox
    Friend WithEvents fndMccCode As common.Controls.MyTextBox
    Friend WithEvents fndVehicleCode As common.Controls.MyTextBox
    Friend WithEvents fndVSPCode As common.Controls.MyTextBox
    Friend WithEvents fndRouteCOde As common.Controls.MyTextBox
    Friend WithEvents Attachments As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents btnDrillDown As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents cboDockCollectionMilkType As common.Controls.MyComboBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtShiftEnd As common.UserControls.txtMultiSelectFinder
    Friend WithEvents btnShowInventory As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton2 As RadButton
    Friend WithEvents btnHistory As RadButton
    Friend WithEvents btnJE As RadButton
End Class

