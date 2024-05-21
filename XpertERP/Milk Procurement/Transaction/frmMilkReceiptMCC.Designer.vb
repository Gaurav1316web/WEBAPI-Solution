<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMilkReceiptMCC
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.TxtToRange = New common.MyNumBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.TxtFrmRange = New common.MyNumBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.UcWeighing1 = New XpertERPEngine.ucWeighing()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtVehicle = New common.Controls.MyTextBox()
        Me.lblVehicle = New common.Controls.MyLabel()
        Me.lblVillageCode = New common.Controls.MyLabel()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.lblVillageName = New common.Controls.MyLabel()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.txtCan = New common.UserControls.txtFinder()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtMilkWeight = New common.MyNumBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.cboDockCollectionMilkType = New common.Controls.MyComboBox()
        Me.lblType = New common.Controls.MyLabel()
        Me.Lbl_Type = New common.Controls.MyLabel()
        Me.ChkALLVLC = New common.Controls.MyCheckBox()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.TotalCansAllRoute = New common.MyNumBox()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.TxtTotalWeightallRoute = New common.MyNumBox()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.lblTotalWeight = New common.Controls.MyLabel()
        Me.txtTotalWeight = New common.MyNumBox()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.TxtTotalcans = New common.MyNumBox()
        Me.fndItem_Code = New common.Controls.MyLabel()
        Me.fndVspCode = New common.Controls.MyLabel()
        Me.chkOther = New common.Controls.MyCheckBox()
        Me.LblMccName = New common.Controls.MyLabel()
        Me.LblUom = New common.Controls.MyLabel()
        Me.cboMilkType = New common.Controls.MyComboBox()
        Me.lblMilkType = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.lblItemDesc = New common.Controls.MyLabel()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.cboType = New common.Controls.MyComboBox()
        Me.txtNoOfCans = New common.MyNumBox()
        Me.lblNoOfCans = New common.Controls.MyLabel()
        Me.lblVehicleCode = New common.Controls.MyLabel()
        Me.fndVehicleCode = New common.Controls.MyLabel()
        Me.lblRouteCode = New common.Controls.MyLabel()
        Me.fndRouteCode = New common.UserControls.txtFinder()
        Me.lblRouteDesc = New common.Controls.MyLabel()
        Me.lblVSPCode = New common.Controls.MyLabel()
        Me.lblVSPDesc = New common.Controls.MyLabel()
        Me.lblVLCCode = New common.Controls.MyLabel()
        Me.fndVLCCode = New common.UserControls.txtFinder()
        Me.lblVLCDesc = New common.Controls.MyLabel()
        Me.lblBOMStatus = New common.Controls.MyLabel()
        Me.cboShift = New common.Controls.MyComboBox()
        Me.lblDocDate = New common.Controls.MyLabel()
        Me.dtpDocDate = New common.Controls.MyDateTimePicker()
        Me.lblMCCCode = New common.Controls.MyLabel()
        Me.fndMCCCode = New common.UserControls.txtFinder()
        Me.lblCode = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.lbErrors = New Telerik.WinControls.UI.RadListControl()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.pvpCustomFields = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcCustomFields1 = New ERP.ucCustomFields()
        Me.Attachments = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.BtnExportImport = New Telerik.WinControls.UI.RadSplitButton()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.BtnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenu()
        Me.pnlDock = New System.Windows.Forms.Panel()
        Me.lblDockName = New common.Controls.MyLabel()
        Me.lblDockCode = New common.Controls.MyLabel()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.rdmenufile1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.BtnSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.BtnDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.TxtToRange, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtFrmRange, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.txtVehicle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVehicle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVillageCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVillageName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMilkWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboDockCollectionMilkType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Lbl_Type, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkALLVLC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TotalCansAllRoute, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtTotalWeightallRoute, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotalWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtTotalcans, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fndItem_Code, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fndVspCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkOther, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblMccName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblUom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboMilkType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMilkType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblItemDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoOfCans, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNoOfCans, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVehicleCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fndVehicleCode, System.ComponentModel.ISupportInitialize).BeginInit()
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
        CType(Me.lbErrors, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pvpCustomFields.SuspendLayout()
        Me.Attachments.SuspendLayout()
        CType(Me.BtnExportImport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.rdmenufile.SuspendLayout()
        Me.pnlDock.SuspendLayout()
        CType(Me.lblDockName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDockCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnExportImport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(988, 467)
        Me.SplitContainer1.SplitterDistance = 431
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
        Me.RadPageView1.Size = New System.Drawing.Size(988, 431)
        Me.RadPageView1.TabIndex = 0
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.TxtToRange)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.TxtFrmRange)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.UcWeighing1)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(78.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(967, 383)
        Me.RadPageViewPage1.Text = "Milk Receipt"
        '
        'TxtToRange
        '
        Me.TxtToRange.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtToRange.CalculationExpression = Nothing
        Me.TxtToRange.DecimalPlaces = 0
        Me.TxtToRange.FieldCode = Nothing
        Me.TxtToRange.FieldDesc = Nothing
        Me.TxtToRange.FieldMaxLength = 0
        Me.TxtToRange.FieldName = Nothing
        Me.TxtToRange.isCalculatedField = False
        Me.TxtToRange.IsSourceFromTable = False
        Me.TxtToRange.IsSourceFromValueList = False
        Me.TxtToRange.IsUnique = False
        Me.TxtToRange.Location = New System.Drawing.Point(184, 37)
        Me.TxtToRange.MaxLength = 5
        Me.TxtToRange.MendatroryField = True
        Me.TxtToRange.MyLinkLable1 = Me.MyLabel4
        Me.TxtToRange.MyLinkLable2 = Nothing
        Me.TxtToRange.Name = "TxtToRange"
        Me.TxtToRange.ReferenceFieldDesc = Nothing
        Me.TxtToRange.ReferenceFieldName = Nothing
        Me.TxtToRange.ReferenceTableName = Nothing
        Me.TxtToRange.Size = New System.Drawing.Size(67, 20)
        Me.TxtToRange.TabIndex = 46
        Me.TxtToRange.Text = "0"
        Me.TxtToRange.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtToRange.Value = 0R
        '
        'MyLabel4
        '
        Me.MyLabel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel4.ForeColor = System.Drawing.Color.Lime
        Me.MyLabel4.Location = New System.Drawing.Point(158, 39)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(19, 16)
        Me.MyLabel4.TabIndex = 47
        Me.MyLabel4.Text = "To"
        '
        'TxtFrmRange
        '
        Me.TxtFrmRange.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtFrmRange.CalculationExpression = Nothing
        Me.TxtFrmRange.DecimalPlaces = 0
        Me.TxtFrmRange.FieldCode = Nothing
        Me.TxtFrmRange.FieldDesc = Nothing
        Me.TxtFrmRange.FieldMaxLength = 0
        Me.TxtFrmRange.FieldName = Nothing
        Me.TxtFrmRange.isCalculatedField = False
        Me.TxtFrmRange.IsSourceFromTable = False
        Me.TxtFrmRange.IsSourceFromValueList = False
        Me.TxtFrmRange.IsUnique = False
        Me.TxtFrmRange.Location = New System.Drawing.Point(87, 37)
        Me.TxtFrmRange.MaxLength = 5
        Me.TxtFrmRange.MendatroryField = True
        Me.TxtFrmRange.MyLinkLable1 = Me.MyLabel3
        Me.TxtFrmRange.MyLinkLable2 = Nothing
        Me.TxtFrmRange.Name = "TxtFrmRange"
        Me.TxtFrmRange.ReferenceFieldDesc = Nothing
        Me.TxtFrmRange.ReferenceFieldName = Nothing
        Me.TxtFrmRange.ReferenceTableName = Nothing
        Me.TxtFrmRange.Size = New System.Drawing.Size(67, 20)
        Me.TxtFrmRange.TabIndex = 44
        Me.TxtFrmRange.Text = "0"
        Me.TxtFrmRange.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtFrmRange.Value = 0R
        '
        'MyLabel3
        '
        Me.MyLabel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel3.ForeColor = System.Drawing.Color.Lime
        Me.MyLabel3.Location = New System.Drawing.Point(12, 39)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(69, 16)
        Me.MyLabel3.TabIndex = 45
        Me.MyLabel3.Text = "From Range"
        '
        'UcWeighing1
        '
        Me.UcWeighing1.Dock = System.Windows.Forms.DockStyle.Top
        Me.UcWeighing1.form_ID = Nothing
        Me.UcWeighing1.LiveReading = 0R
        Me.UcWeighing1.Location = New System.Drawing.Point(0, 0)
        Me.UcWeighing1.Machine = ""
        Me.UcWeighing1.Name = "UcWeighing1"
        Me.UcWeighing1.Port = ""
        Me.UcWeighing1.Size = New System.Drawing.Size(967, 64)
        Me.UcWeighing1.TabIndex = 70
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.txtVehicle)
        Me.RadGroupBox1.Controls.Add(Me.lblVehicle)
        Me.RadGroupBox1.Controls.Add(Me.lblVillageCode)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel11)
        Me.RadGroupBox1.Controls.Add(Me.lblVillageName)
        Me.RadGroupBox1.Controls.Add(Me.SplitContainer2)
        Me.RadGroupBox1.Controls.Add(Me.txtCode)
        Me.RadGroupBox1.Controls.Add(Me.cboDockCollectionMilkType)
        Me.RadGroupBox1.Controls.Add(Me.Lbl_Type)
        Me.RadGroupBox1.Controls.Add(Me.ChkALLVLC)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox4)
        Me.RadGroupBox1.Controls.Add(Me.fndItem_Code)
        Me.RadGroupBox1.Controls.Add(Me.fndVspCode)
        Me.RadGroupBox1.Controls.Add(Me.chkOther)
        Me.RadGroupBox1.Controls.Add(Me.LblMccName)
        Me.RadGroupBox1.Controls.Add(Me.LblUom)
        Me.RadGroupBox1.Controls.Add(Me.cboMilkType)
        Me.RadGroupBox1.Controls.Add(Me.lblMilkType)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.btnsave)
        Me.RadGroupBox1.Controls.Add(Me.lblItemDesc)
        Me.RadGroupBox1.Controls.Add(Me.btnnew)
        Me.RadGroupBox1.Controls.Add(Me.lblType)
        Me.RadGroupBox1.Controls.Add(Me.cboType)
        Me.RadGroupBox1.Controls.Add(Me.txtNoOfCans)
        Me.RadGroupBox1.Controls.Add(Me.lblNoOfCans)
        Me.RadGroupBox1.Controls.Add(Me.lblVehicleCode)
        Me.RadGroupBox1.Controls.Add(Me.fndVehicleCode)
        Me.RadGroupBox1.Controls.Add(Me.lblRouteCode)
        Me.RadGroupBox1.Controls.Add(Me.fndRouteCode)
        Me.RadGroupBox1.Controls.Add(Me.lblRouteDesc)
        Me.RadGroupBox1.Controls.Add(Me.lblVSPCode)
        Me.RadGroupBox1.Controls.Add(Me.lblVSPDesc)
        Me.RadGroupBox1.Controls.Add(Me.lblVLCCode)
        Me.RadGroupBox1.Controls.Add(Me.fndVLCCode)
        Me.RadGroupBox1.Controls.Add(Me.lblVLCDesc)
        Me.RadGroupBox1.Controls.Add(Me.lblBOMStatus)
        Me.RadGroupBox1.Controls.Add(Me.cboShift)
        Me.RadGroupBox1.Controls.Add(Me.lblDocDate)
        Me.RadGroupBox1.Controls.Add(Me.dtpDocDate)
        Me.RadGroupBox1.Controls.Add(Me.lblMCCCode)
        Me.RadGroupBox1.Controls.Add(Me.fndMCCCode)
        Me.RadGroupBox1.Controls.Add(Me.lblCode)
        Me.RadGroupBox1.Controls.Add(Me.UsLock1)
        Me.RadGroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(3, 67)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(962, 167)
        Me.RadGroupBox1.TabIndex = 2
        '
        'txtVehicle
        '
        Me.txtVehicle.CalculationExpression = Nothing
        Me.txtVehicle.FieldCode = Nothing
        Me.txtVehicle.FieldDesc = Nothing
        Me.txtVehicle.FieldMaxLength = 0
        Me.txtVehicle.FieldName = Nothing
        Me.txtVehicle.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVehicle.isCalculatedField = False
        Me.txtVehicle.IsSourceFromTable = False
        Me.txtVehicle.IsSourceFromValueList = False
        Me.txtVehicle.IsUnique = False
        Me.txtVehicle.Location = New System.Drawing.Point(654, 118)
        Me.txtVehicle.MaxLength = 200
        Me.txtVehicle.MendatroryField = False
        Me.txtVehicle.MyLinkLable1 = Nothing
        Me.txtVehicle.MyLinkLable2 = Nothing
        Me.txtVehicle.Name = "txtVehicle"
        Me.txtVehicle.ReadOnly = True
        Me.txtVehicle.ReferenceFieldDesc = Nothing
        Me.txtVehicle.ReferenceFieldName = Nothing
        Me.txtVehicle.ReferenceTableName = Nothing
        Me.txtVehicle.Size = New System.Drawing.Size(128, 18)
        Me.txtVehicle.TabIndex = 1447
        '
        'lblVehicle
        '
        Me.lblVehicle.FieldName = Nothing
        Me.lblVehicle.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblVehicle.Location = New System.Drawing.Point(609, 119)
        Me.lblVehicle.Name = "lblVehicle"
        Me.lblVehicle.Size = New System.Drawing.Size(43, 16)
        Me.lblVehicle.TabIndex = 1031
        Me.lblVehicle.Text = "Vehicle"
        '
        'lblVillageCode
        '
        Me.lblVillageCode.AutoSize = False
        Me.lblVillageCode.BorderVisible = True
        Me.lblVillageCode.FieldName = Nothing
        Me.lblVillageCode.Location = New System.Drawing.Point(505, 95)
        Me.lblVillageCode.Name = "lblVillageCode"
        Me.lblVillageCode.Size = New System.Drawing.Size(148, 21)
        Me.lblVillageCode.TabIndex = 1029
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel11.Location = New System.Drawing.Point(435, 96)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(40, 18)
        Me.MyLabel11.TabIndex = 1027
        Me.MyLabel11.Text = "Village"
        '
        'lblVillageName
        '
        Me.lblVillageName.AutoSize = False
        Me.lblVillageName.BorderVisible = True
        Me.lblVillageName.FieldName = Nothing
        Me.lblVillageName.Location = New System.Drawing.Point(654, 95)
        Me.lblVillageName.Name = "lblVillageName"
        Me.lblVillageName.Size = New System.Drawing.Size(298, 21)
        Me.lblVillageName.TabIndex = 1028
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Location = New System.Drawing.Point(179, 95)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtCan)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel5)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.txtMilkWeight)
        Me.SplitContainer2.Panel2.Controls.Add(Me.MyLabel1)
        Me.SplitContainer2.Size = New System.Drawing.Size(251, 21)
        Me.SplitContainer2.SplitterDistance = 131
        Me.SplitContainer2.TabIndex = 1026
        '
        'txtCan
        '
        Me.txtCan.CalculationExpression = Nothing
        Me.txtCan.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtCan.FieldCode = Nothing
        Me.txtCan.FieldDesc = Nothing
        Me.txtCan.FieldMaxLength = 0
        Me.txtCan.FieldName = Nothing
        Me.txtCan.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCan.isCalculatedField = False
        Me.txtCan.IsSourceFromTable = False
        Me.txtCan.IsSourceFromValueList = False
        Me.txtCan.IsUnique = False
        Me.txtCan.Location = New System.Drawing.Point(25, 0)
        Me.txtCan.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtCan.MendatroryField = True
        Me.txtCan.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCan.MyLinkLable1 = Me.MyLabel5
        Me.txtCan.MyLinkLable2 = Nothing
        Me.txtCan.MyReadOnly = False
        Me.txtCan.MyShowMasterFormButton = False
        Me.txtCan.Name = "txtCan"
        Me.txtCan.ReferenceFieldDesc = Nothing
        Me.txtCan.ReferenceFieldName = Nothing
        Me.txtCan.ReferenceTableName = Nothing
        Me.txtCan.Size = New System.Drawing.Size(106, 21)
        Me.txtCan.TabIndex = 0
        Me.txtCan.Value = ""
        '
        'MyLabel5
        '
        Me.MyLabel5.Dock = System.Windows.Forms.DockStyle.Left
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel5.Location = New System.Drawing.Point(0, 0)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(25, 18)
        Me.MyLabel5.TabIndex = 1
        Me.MyLabel5.Text = "Can"
        '
        'txtMilkWeight
        '
        Me.txtMilkWeight.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtMilkWeight.CalculationExpression = Nothing
        Me.txtMilkWeight.DecimalPlaces = 2
        Me.txtMilkWeight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtMilkWeight.FieldCode = Nothing
        Me.txtMilkWeight.FieldDesc = Nothing
        Me.txtMilkWeight.FieldMaxLength = 0
        Me.txtMilkWeight.FieldName = Nothing
        Me.txtMilkWeight.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.txtMilkWeight.isCalculatedField = False
        Me.txtMilkWeight.IsSourceFromTable = False
        Me.txtMilkWeight.IsSourceFromValueList = False
        Me.txtMilkWeight.IsUnique = False
        Me.txtMilkWeight.Location = New System.Drawing.Point(41, 0)
        Me.txtMilkWeight.MaxLength = 7
        Me.txtMilkWeight.MendatroryField = True
        Me.txtMilkWeight.MyLinkLable1 = Me.MyLabel1
        Me.txtMilkWeight.MyLinkLable2 = Nothing
        Me.txtMilkWeight.Name = "txtMilkWeight"
        Me.txtMilkWeight.ReferenceFieldDesc = Nothing
        Me.txtMilkWeight.ReferenceFieldName = Nothing
        Me.txtMilkWeight.ReferenceTableName = Nothing
        Me.txtMilkWeight.Size = New System.Drawing.Size(75, 21)
        Me.txtMilkWeight.TabIndex = 0
        Me.txtMilkWeight.Text = "0"
        Me.txtMilkWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMilkWeight.Value = 0R
        '
        'MyLabel1
        '
        Me.MyLabel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(0, 0)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(41, 21)
        Me.MyLabel1.TabIndex = 1
        Me.MyLabel1.Text = "Weight"
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(111, 1)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Nothing
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 32767
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(301, 20)
        Me.txtCode.TabIndex = 1025
        Me.txtCode.Value = ""
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
        Me.cboDockCollectionMilkType.Location = New System.Drawing.Point(791, 2)
        Me.cboDockCollectionMilkType.MendatroryField = True
        Me.cboDockCollectionMilkType.MyLinkLable1 = Me.lblType
        Me.cboDockCollectionMilkType.MyLinkLable2 = Nothing
        Me.cboDockCollectionMilkType.Name = "cboDockCollectionMilkType"
        Me.cboDockCollectionMilkType.ReferenceFieldDesc = Nothing
        Me.cboDockCollectionMilkType.ReferenceFieldName = Nothing
        Me.cboDockCollectionMilkType.ReferenceTableName = Nothing
        Me.cboDockCollectionMilkType.Size = New System.Drawing.Size(94, 18)
        Me.cboDockCollectionMilkType.TabIndex = 1025
        '
        'lblType
        '
        Me.lblType.FieldName = Nothing
        Me.lblType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblType.Location = New System.Drawing.Point(680, 181)
        Me.lblType.Name = "lblType"
        Me.lblType.Size = New System.Drawing.Size(31, 16)
        Me.lblType.TabIndex = 39
        Me.lblType.Text = "Type"
        Me.lblType.Visible = False
        '
        'Lbl_Type
        '
        Me.Lbl_Type.FieldName = Nothing
        Me.Lbl_Type.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Type.Location = New System.Drawing.Point(316, 119)
        Me.Lbl_Type.Name = "Lbl_Type"
        Me.Lbl_Type.Size = New System.Drawing.Size(31, 16)
        Me.Lbl_Type.TabIndex = 1024
        Me.Lbl_Type.Text = "Type"
        Me.Lbl_Type.Visible = False
        '
        'ChkALLVLC
        '
        Me.ChkALLVLC.Location = New System.Drawing.Point(386, 73)
        Me.ChkALLVLC.MyLinkLable1 = Nothing
        Me.ChkALLVLC.MyLinkLable2 = Nothing
        Me.ChkALLVLC.Name = "ChkALLVLC"
        Me.ChkALLVLC.Size = New System.Drawing.Size(49, 18)
        Me.ChkALLVLC.TabIndex = 2
        Me.ChkALLVLC.Tag1 = Nothing
        Me.ChkALLVLC.Text = "Other"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox4.Controls.Add(Me.RadButton1)
        Me.RadGroupBox4.Controls.Add(Me.TotalCansAllRoute)
        Me.RadGroupBox4.Controls.Add(Me.TxtTotalWeightallRoute)
        Me.RadGroupBox4.Controls.Add(Me.MyLabel7)
        Me.RadGroupBox4.Controls.Add(Me.lblTotalWeight)
        Me.RadGroupBox4.Controls.Add(Me.txtTotalWeight)
        Me.RadGroupBox4.Controls.Add(Me.MyLabel9)
        Me.RadGroupBox4.Controls.Add(Me.MyLabel6)
        Me.RadGroupBox4.Controls.Add(Me.TxtTotalcans)
        Me.RadGroupBox4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox4.HeaderText = ""
        Me.RadGroupBox4.Location = New System.Drawing.Point(3, 139)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(957, 23)
        Me.RadGroupBox4.TabIndex = 2
        '
        'RadButton1
        '
        Me.RadButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadButton1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton1.Location = New System.Drawing.Point(898, 3)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(45, 18)
        Me.RadButton1.TabIndex = 71
        Me.RadButton1.Text = "Clear"
        Me.RadButton1.Visible = False
        '
        'TotalCansAllRoute
        '
        Me.TotalCansAllRoute.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TotalCansAllRoute.CalculationExpression = Nothing
        Me.TotalCansAllRoute.DecimalPlaces = 0
        Me.TotalCansAllRoute.Enabled = False
        Me.TotalCansAllRoute.FieldCode = Nothing
        Me.TotalCansAllRoute.FieldDesc = Nothing
        Me.TotalCansAllRoute.FieldMaxLength = 0
        Me.TotalCansAllRoute.FieldName = Nothing
        Me.TotalCansAllRoute.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.TotalCansAllRoute.isCalculatedField = False
        Me.TotalCansAllRoute.IsSourceFromTable = False
        Me.TotalCansAllRoute.IsSourceFromValueList = False
        Me.TotalCansAllRoute.IsUnique = False
        Me.TotalCansAllRoute.Location = New System.Drawing.Point(268, 2)
        Me.TotalCansAllRoute.MendatroryField = True
        Me.TotalCansAllRoute.MyLinkLable1 = Me.MyLabel7
        Me.TotalCansAllRoute.MyLinkLable2 = Nothing
        Me.TotalCansAllRoute.Name = "TotalCansAllRoute"
        Me.TotalCansAllRoute.ReadOnly = True
        Me.TotalCansAllRoute.ReferenceFieldDesc = Nothing
        Me.TotalCansAllRoute.ReferenceFieldName = Nothing
        Me.TotalCansAllRoute.ReferenceTableName = Nothing
        Me.TotalCansAllRoute.Size = New System.Drawing.Size(109, 21)
        Me.TotalCansAllRoute.TabIndex = 1027
        Me.TotalCansAllRoute.TabStop = False
        Me.TotalCansAllRoute.Text = "0"
        Me.TotalCansAllRoute.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TotalCansAllRoute.Value = 0R
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel7.Location = New System.Drawing.Point(199, 4)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(61, 16)
        Me.MyLabel7.TabIndex = 1026
        Me.MyLabel7.Text = "Total Cans"
        '
        'TxtTotalWeightallRoute
        '
        Me.TxtTotalWeightallRoute.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtTotalWeightallRoute.CalculationExpression = Nothing
        Me.TxtTotalWeightallRoute.DecimalPlaces = 0
        Me.TxtTotalWeightallRoute.Enabled = False
        Me.TxtTotalWeightallRoute.FieldCode = Nothing
        Me.TxtTotalWeightallRoute.FieldDesc = Nothing
        Me.TxtTotalWeightallRoute.FieldMaxLength = 0
        Me.TxtTotalWeightallRoute.FieldName = Nothing
        Me.TxtTotalWeightallRoute.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.TxtTotalWeightallRoute.isCalculatedField = False
        Me.TxtTotalWeightallRoute.IsSourceFromTable = False
        Me.TxtTotalWeightallRoute.IsSourceFromValueList = False
        Me.TxtTotalWeightallRoute.IsUnique = False
        Me.TxtTotalWeightallRoute.Location = New System.Drawing.Point(82, 2)
        Me.TxtTotalWeightallRoute.MendatroryField = True
        Me.TxtTotalWeightallRoute.MyLinkLable1 = Me.MyLabel9
        Me.TxtTotalWeightallRoute.MyLinkLable2 = Nothing
        Me.TxtTotalWeightallRoute.Name = "TxtTotalWeightallRoute"
        Me.TxtTotalWeightallRoute.ReadOnly = True
        Me.TxtTotalWeightallRoute.ReferenceFieldDesc = Nothing
        Me.TxtTotalWeightallRoute.ReferenceFieldName = Nothing
        Me.TxtTotalWeightallRoute.ReferenceTableName = Nothing
        Me.TxtTotalWeightallRoute.Size = New System.Drawing.Size(109, 21)
        Me.TxtTotalWeightallRoute.TabIndex = 1025
        Me.TxtTotalWeightallRoute.TabStop = False
        Me.TxtTotalWeightallRoute.Text = "0"
        Me.TxtTotalWeightallRoute.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtTotalWeightallRoute.Value = 0R
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel9.Location = New System.Drawing.Point(4, 4)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(70, 16)
        Me.MyLabel9.TabIndex = 1024
        Me.MyLabel9.Text = "Total Weight"
        '
        'lblTotalWeight
        '
        Me.lblTotalWeight.FieldName = Nothing
        Me.lblTotalWeight.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblTotalWeight.Location = New System.Drawing.Point(385, 4)
        Me.lblTotalWeight.Name = "lblTotalWeight"
        Me.lblTotalWeight.Size = New System.Drawing.Size(136, 16)
        Me.lblTotalWeight.TabIndex = 43
        Me.lblTotalWeight.Text = "Total Weight(Route Wise)"
        '
        'txtTotalWeight
        '
        Me.txtTotalWeight.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtTotalWeight.CalculationExpression = Nothing
        Me.txtTotalWeight.DecimalPlaces = 0
        Me.txtTotalWeight.Enabled = False
        Me.txtTotalWeight.FieldCode = Nothing
        Me.txtTotalWeight.FieldDesc = Nothing
        Me.txtTotalWeight.FieldMaxLength = 0
        Me.txtTotalWeight.FieldName = Nothing
        Me.txtTotalWeight.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.txtTotalWeight.isCalculatedField = False
        Me.txtTotalWeight.IsSourceFromTable = False
        Me.txtTotalWeight.IsSourceFromValueList = False
        Me.txtTotalWeight.IsUnique = False
        Me.txtTotalWeight.Location = New System.Drawing.Point(529, 2)
        Me.txtTotalWeight.MendatroryField = True
        Me.txtTotalWeight.MyLinkLable1 = Me.lblTotalWeight
        Me.txtTotalWeight.MyLinkLable2 = Nothing
        Me.txtTotalWeight.Name = "txtTotalWeight"
        Me.txtTotalWeight.ReadOnly = True
        Me.txtTotalWeight.ReferenceFieldDesc = Nothing
        Me.txtTotalWeight.ReferenceFieldName = Nothing
        Me.txtTotalWeight.ReferenceTableName = Nothing
        Me.txtTotalWeight.Size = New System.Drawing.Size(109, 21)
        Me.txtTotalWeight.TabIndex = 100
        Me.txtTotalWeight.TabStop = False
        Me.txtTotalWeight.Text = "0"
        Me.txtTotalWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTotalWeight.Value = 0R
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel6.Location = New System.Drawing.Point(646, 4)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(127, 16)
        Me.MyLabel6.TabIndex = 101
        Me.MyLabel6.Text = "Total Cans(Route Wise)"
        '
        'TxtTotalcans
        '
        Me.TxtTotalcans.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtTotalcans.CalculationExpression = Nothing
        Me.TxtTotalcans.DecimalPlaces = 0
        Me.TxtTotalcans.Enabled = False
        Me.TxtTotalcans.FieldCode = Nothing
        Me.TxtTotalcans.FieldDesc = Nothing
        Me.TxtTotalcans.FieldMaxLength = 0
        Me.TxtTotalcans.FieldName = Nothing
        Me.TxtTotalcans.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.TxtTotalcans.isCalculatedField = False
        Me.TxtTotalcans.IsSourceFromTable = False
        Me.TxtTotalcans.IsSourceFromValueList = False
        Me.TxtTotalcans.IsUnique = False
        Me.TxtTotalcans.Location = New System.Drawing.Point(781, 2)
        Me.TxtTotalcans.MendatroryField = True
        Me.TxtTotalcans.MyLinkLable1 = Me.MyLabel6
        Me.TxtTotalcans.MyLinkLable2 = Nothing
        Me.TxtTotalcans.Name = "TxtTotalcans"
        Me.TxtTotalcans.ReadOnly = True
        Me.TxtTotalcans.ReferenceFieldDesc = Nothing
        Me.TxtTotalcans.ReferenceFieldName = Nothing
        Me.TxtTotalcans.ReferenceTableName = Nothing
        Me.TxtTotalcans.Size = New System.Drawing.Size(109, 21)
        Me.TxtTotalcans.TabIndex = 102
        Me.TxtTotalcans.TabStop = False
        Me.TxtTotalcans.Text = "0"
        Me.TxtTotalcans.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtTotalcans.Value = 0R
        '
        'fndItem_Code
        '
        Me.fndItem_Code.AutoSize = False
        Me.fndItem_Code.BorderVisible = True
        Me.fndItem_Code.FieldName = Nothing
        Me.fndItem_Code.Location = New System.Drawing.Point(505, 48)
        Me.fndItem_Code.Name = "fndItem_Code"
        Me.fndItem_Code.Size = New System.Drawing.Size(148, 21)
        Me.fndItem_Code.TabIndex = 37
        '
        'fndVspCode
        '
        Me.fndVspCode.AutoSize = False
        Me.fndVspCode.BorderVisible = True
        Me.fndVspCode.FieldName = Nothing
        Me.fndVspCode.Location = New System.Drawing.Point(505, 72)
        Me.fndVspCode.Name = "fndVspCode"
        Me.fndVspCode.Size = New System.Drawing.Size(148, 21)
        Me.fndVspCode.TabIndex = 1023
        '
        'chkOther
        '
        Me.chkOther.Location = New System.Drawing.Point(345, 49)
        Me.chkOther.MyLinkLable1 = Nothing
        Me.chkOther.MyLinkLable2 = Nothing
        Me.chkOther.Name = "chkOther"
        Me.chkOther.Size = New System.Drawing.Size(88, 18)
        Me.chkOther.TabIndex = 1
        Me.chkOther.Tag1 = Nothing
        Me.chkOther.Text = "Other Vehicle"
        '
        'LblMccName
        '
        Me.LblMccName.AutoSize = False
        Me.LblMccName.BorderVisible = True
        Me.LblMccName.FieldName = Nothing
        Me.LblMccName.Location = New System.Drawing.Point(236, 24)
        Me.LblMccName.Name = "LblMccName"
        Me.LblMccName.Size = New System.Drawing.Size(197, 21)
        Me.LblMccName.TabIndex = 1022
        '
        'LblUom
        '
        Me.LblUom.FieldName = Nothing
        Me.LblUom.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.LblUom.Location = New System.Drawing.Point(435, 119)
        Me.LblUom.Name = "LblUom"
        Me.LblUom.Size = New System.Drawing.Size(28, 16)
        Me.LblUom.TabIndex = 44
        Me.LblUom.Text = "LTR"
        '
        'cboMilkType
        '
        Me.cboMilkType.AutoCompleteDisplayMember = Nothing
        Me.cboMilkType.AutoCompleteValueMember = Nothing
        Me.cboMilkType.CalculationExpression = Nothing
        Me.cboMilkType.DropDownAnimationEnabled = True
        Me.cboMilkType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboMilkType.FieldCode = Nothing
        Me.cboMilkType.FieldDesc = Nothing
        Me.cboMilkType.FieldMaxLength = 0
        Me.cboMilkType.FieldName = Nothing
        Me.cboMilkType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboMilkType.isCalculatedField = False
        Me.cboMilkType.IsSourceFromTable = False
        Me.cboMilkType.IsSourceFromValueList = False
        Me.cboMilkType.IsUnique = False
        Me.cboMilkType.Location = New System.Drawing.Point(111, 118)
        Me.cboMilkType.MendatroryField = True
        Me.cboMilkType.MyLinkLable1 = Me.lblMilkType
        Me.cboMilkType.MyLinkLable2 = Nothing
        Me.cboMilkType.Name = "cboMilkType"
        Me.cboMilkType.ReferenceFieldDesc = Nothing
        Me.cboMilkType.ReferenceFieldName = Nothing
        Me.cboMilkType.ReferenceTableName = Nothing
        Me.cboMilkType.Size = New System.Drawing.Size(199, 18)
        Me.cboMilkType.TabIndex = 66
        '
        'lblMilkType
        '
        Me.lblMilkType.FieldName = Nothing
        Me.lblMilkType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMilkType.Location = New System.Drawing.Point(9, 119)
        Me.lblMilkType.Name = "lblMilkType"
        Me.lblMilkType.Size = New System.Drawing.Size(55, 16)
        Me.lblMilkType.TabIndex = 41
        Me.lblMilkType.Text = "Milk Type"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel2.Location = New System.Drawing.Point(435, 49)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(58, 18)
        Me.MyLabel2.TabIndex = 35
        Me.MyLabel2.Text = "Item Code"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(505, 118)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(65, 18)
        Me.btnsave.TabIndex = 4
        Me.btnsave.Text = ">>"
        '
        'lblItemDesc
        '
        Me.lblItemDesc.AutoSize = False
        Me.lblItemDesc.BorderVisible = True
        Me.lblItemDesc.FieldName = Nothing
        Me.lblItemDesc.Location = New System.Drawing.Point(654, 48)
        Me.lblItemDesc.Name = "lblItemDesc"
        Me.lblItemDesc.Size = New System.Drawing.Size(298, 21)
        Me.lblItemDesc.TabIndex = 36
        '
        'btnnew
        '
        Me.btnnew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnnew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnnew.Location = New System.Drawing.Point(412, 1)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(21, 20)
        Me.btnnew.TabIndex = 44
        '
        'cboType
        '
        Me.cboType.AutoCompleteDisplayMember = Nothing
        Me.cboType.AutoCompleteValueMember = Nothing
        Me.cboType.CalculationExpression = Nothing
        Me.cboType.DropDownAnimationEnabled = True
        Me.cboType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboType.FieldCode = Nothing
        Me.cboType.FieldDesc = Nothing
        Me.cboType.FieldMaxLength = 0
        Me.cboType.FieldName = Nothing
        Me.cboType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboType.isCalculatedField = False
        Me.cboType.IsSourceFromTable = False
        Me.cboType.IsSourceFromValueList = False
        Me.cboType.IsUnique = False
        Me.cboType.Location = New System.Drawing.Point(355, 118)
        Me.cboType.MendatroryField = True
        Me.cboType.MyLinkLable1 = Me.lblType
        Me.cboType.MyLinkLable2 = Nothing
        Me.cboType.Name = "cboType"
        Me.cboType.ReferenceFieldDesc = Nothing
        Me.cboType.ReferenceFieldName = Nothing
        Me.cboType.ReferenceTableName = Nothing
        Me.cboType.Size = New System.Drawing.Size(75, 18)
        Me.cboType.TabIndex = 38
        Me.cboType.Visible = False
        '
        'txtNoOfCans
        '
        Me.txtNoOfCans.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtNoOfCans.CalculationExpression = Nothing
        Me.txtNoOfCans.DecimalPlaces = 0
        Me.txtNoOfCans.FieldCode = Nothing
        Me.txtNoOfCans.FieldDesc = Nothing
        Me.txtNoOfCans.FieldMaxLength = 0
        Me.txtNoOfCans.FieldName = Nothing
        Me.txtNoOfCans.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.txtNoOfCans.isCalculatedField = False
        Me.txtNoOfCans.IsSourceFromTable = False
        Me.txtNoOfCans.IsSourceFromValueList = False
        Me.txtNoOfCans.IsUnique = False
        Me.txtNoOfCans.Location = New System.Drawing.Point(111, 95)
        Me.txtNoOfCans.MaxLength = 5
        Me.txtNoOfCans.MendatroryField = True
        Me.txtNoOfCans.MyLinkLable1 = Me.lblNoOfCans
        Me.txtNoOfCans.MyLinkLable2 = Nothing
        Me.txtNoOfCans.Name = "txtNoOfCans"
        Me.txtNoOfCans.ReferenceFieldDesc = Nothing
        Me.txtNoOfCans.ReferenceFieldName = Nothing
        Me.txtNoOfCans.ReferenceTableName = Nothing
        Me.txtNoOfCans.Size = New System.Drawing.Size(63, 21)
        Me.txtNoOfCans.TabIndex = 36
        Me.txtNoOfCans.Text = "0"
        Me.txtNoOfCans.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtNoOfCans.Value = 0R
        '
        'lblNoOfCans
        '
        Me.lblNoOfCans.FieldName = Nothing
        Me.lblNoOfCans.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblNoOfCans.Location = New System.Drawing.Point(9, 97)
        Me.lblNoOfCans.Name = "lblNoOfCans"
        Me.lblNoOfCans.Size = New System.Drawing.Size(63, 16)
        Me.lblNoOfCans.TabIndex = 35
        Me.lblNoOfCans.Text = "No of Cans"
        '
        'lblVehicleCode
        '
        Me.lblVehicleCode.FieldName = Nothing
        Me.lblVehicleCode.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblVehicleCode.Location = New System.Drawing.Point(9, 49)
        Me.lblVehicleCode.Name = "lblVehicleCode"
        Me.lblVehicleCode.Size = New System.Drawing.Size(60, 18)
        Me.lblVehicleCode.TabIndex = 32
        Me.lblVehicleCode.Text = "Vehicle No"
        '
        'fndVehicleCode
        '
        Me.fndVehicleCode.AutoSize = False
        Me.fndVehicleCode.BorderVisible = True
        Me.fndVehicleCode.FieldName = Nothing
        Me.fndVehicleCode.Location = New System.Drawing.Point(111, 48)
        Me.fndVehicleCode.Name = "fndVehicleCode"
        Me.fndVehicleCode.Size = New System.Drawing.Size(228, 21)
        Me.fndVehicleCode.TabIndex = 33
        '
        'lblRouteCode
        '
        Me.lblRouteCode.FieldName = Nothing
        Me.lblRouteCode.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblRouteCode.Location = New System.Drawing.Point(435, 25)
        Me.lblRouteCode.Name = "lblRouteCode"
        Me.lblRouteCode.Size = New System.Drawing.Size(65, 18)
        Me.lblRouteCode.TabIndex = 29
        Me.lblRouteCode.Text = "Route Code"
        '
        'fndRouteCode
        '
        Me.fndRouteCode.CalculationExpression = Nothing
        Me.fndRouteCode.FieldCode = Nothing
        Me.fndRouteCode.FieldDesc = Nothing
        Me.fndRouteCode.FieldMaxLength = 0
        Me.fndRouteCode.FieldName = Nothing
        Me.fndRouteCode.isCalculatedField = False
        Me.fndRouteCode.IsSourceFromTable = False
        Me.fndRouteCode.IsSourceFromValueList = False
        Me.fndRouteCode.IsUnique = False
        Me.fndRouteCode.Location = New System.Drawing.Point(505, 24)
        Me.fndRouteCode.MendatroryField = True
        Me.fndRouteCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndRouteCode.MyLinkLable1 = Me.lblRouteCode
        Me.fndRouteCode.MyLinkLable2 = Nothing
        Me.fndRouteCode.MyReadOnly = False
        Me.fndRouteCode.MyShowMasterFormButton = False
        Me.fndRouteCode.Name = "fndRouteCode"
        Me.fndRouteCode.ReferenceFieldDesc = Nothing
        Me.fndRouteCode.ReferenceFieldName = Nothing
        Me.fndRouteCode.ReferenceTableName = Nothing
        Me.fndRouteCode.Size = New System.Drawing.Size(147, 21)
        Me.fndRouteCode.TabIndex = 0
        Me.fndRouteCode.Value = ""
        '
        'lblRouteDesc
        '
        Me.lblRouteDesc.AutoSize = False
        Me.lblRouteDesc.BorderVisible = True
        Me.lblRouteDesc.FieldName = Nothing
        Me.lblRouteDesc.Location = New System.Drawing.Point(654, 24)
        Me.lblRouteDesc.Name = "lblRouteDesc"
        Me.lblRouteDesc.Size = New System.Drawing.Size(298, 21)
        Me.lblRouteDesc.TabIndex = 30
        '
        'lblVSPCode
        '
        Me.lblVSPCode.FieldName = Nothing
        Me.lblVSPCode.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblVSPCode.Location = New System.Drawing.Point(435, 73)
        Me.lblVSPCode.Name = "lblVSPCode"
        Me.lblVSPCode.Size = New System.Drawing.Size(82, 18)
        Me.lblVSPCode.TabIndex = 26
        Me.lblVSPCode.Text = "Secretary Code"
        '
        'lblVSPDesc
        '
        Me.lblVSPDesc.AutoSize = False
        Me.lblVSPDesc.BorderVisible = True
        Me.lblVSPDesc.FieldName = Nothing
        Me.lblVSPDesc.Location = New System.Drawing.Point(654, 72)
        Me.lblVSPDesc.Name = "lblVSPDesc"
        Me.lblVSPDesc.Size = New System.Drawing.Size(298, 21)
        Me.lblVSPDesc.TabIndex = 27
        '
        'lblVLCCode
        '
        Me.lblVLCCode.FieldName = Nothing
        Me.lblVLCCode.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblVLCCode.Location = New System.Drawing.Point(9, 73)
        Me.lblVLCCode.Name = "lblVLCCode"
        Me.lblVLCCode.Size = New System.Drawing.Size(56, 18)
        Me.lblVLCCode.TabIndex = 23
        Me.lblVLCCode.Text = "DCS Code"
        '
        'fndVLCCode
        '
        Me.fndVLCCode.CalculationExpression = Nothing
        Me.fndVLCCode.FieldCode = Nothing
        Me.fndVLCCode.FieldDesc = Nothing
        Me.fndVLCCode.FieldMaxLength = 0
        Me.fndVLCCode.FieldName = Nothing
        Me.fndVLCCode.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndVLCCode.isCalculatedField = False
        Me.fndVLCCode.IsSourceFromTable = False
        Me.fndVLCCode.IsSourceFromValueList = False
        Me.fndVLCCode.IsUnique = False
        Me.fndVLCCode.Location = New System.Drawing.Point(111, 72)
        Me.fndVLCCode.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.fndVLCCode.MendatroryField = True
        Me.fndVLCCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndVLCCode.MyLinkLable1 = Me.lblVLCCode
        Me.fndVLCCode.MyLinkLable2 = Nothing
        Me.fndVLCCode.MyReadOnly = False
        Me.fndVLCCode.MyShowMasterFormButton = False
        Me.fndVLCCode.Name = "fndVLCCode"
        Me.fndVLCCode.ReferenceFieldDesc = Nothing
        Me.fndVLCCode.ReferenceFieldName = Nothing
        Me.fndVLCCode.ReferenceTableName = Nothing
        Me.fndVLCCode.Size = New System.Drawing.Size(119, 21)
        Me.fndVLCCode.TabIndex = 1
        Me.fndVLCCode.Value = ""
        '
        'lblVLCDesc
        '
        Me.lblVLCDesc.AutoSize = False
        Me.lblVLCDesc.BorderVisible = True
        Me.lblVLCDesc.FieldName = Nothing
        Me.lblVLCDesc.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblVLCDesc.Location = New System.Drawing.Point(236, 72)
        Me.lblVLCDesc.Name = "lblVLCDesc"
        Me.lblVLCDesc.Size = New System.Drawing.Size(146, 21)
        Me.lblVLCDesc.TabIndex = 24
        Me.lblVLCDesc.TextWrap = False
        '
        'lblBOMStatus
        '
        Me.lblBOMStatus.FieldName = Nothing
        Me.lblBOMStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBOMStatus.Location = New System.Drawing.Point(654, 3)
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
        Me.cboShift.Location = New System.Drawing.Point(694, 2)
        Me.cboShift.MendatroryField = True
        Me.cboShift.MyLinkLable1 = Me.lblBOMStatus
        Me.cboShift.MyLinkLable2 = Nothing
        Me.cboShift.Name = "cboShift"
        Me.cboShift.ReferenceFieldDesc = Nothing
        Me.cboShift.ReferenceFieldName = Nothing
        Me.cboShift.ReferenceTableName = Nothing
        Me.cboShift.Size = New System.Drawing.Size(93, 18)
        Me.cboShift.TabIndex = 20
        '
        'lblDocDate
        '
        Me.lblDocDate.FieldName = Nothing
        Me.lblDocDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocDate.Location = New System.Drawing.Point(435, 3)
        Me.lblDocDate.Name = "lblDocDate"
        Me.lblDocDate.Size = New System.Drawing.Size(53, 16)
        Me.lblDocDate.TabIndex = 19
        Me.lblDocDate.Text = "Doc Date"
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
        Me.dtpDocDate.Location = New System.Drawing.Point(505, 2)
        Me.dtpDocDate.MendatroryField = True
        Me.dtpDocDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDocDate.MyLinkLable1 = Me.lblDocDate
        Me.dtpDocDate.MyLinkLable2 = Nothing
        Me.dtpDocDate.Name = "dtpDocDate"
        Me.dtpDocDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDocDate.ReferenceFieldDesc = Nothing
        Me.dtpDocDate.ReferenceFieldName = Nothing
        Me.dtpDocDate.ReferenceTableName = Nothing
        Me.dtpDocDate.Size = New System.Drawing.Size(148, 18)
        Me.dtpDocDate.TabIndex = 18
        Me.dtpDocDate.TabStop = False
        Me.dtpDocDate.Text = "03/05/2011 12:00:00 AM"
        Me.dtpDocDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblMCCCode
        '
        Me.lblMCCCode.FieldName = Nothing
        Me.lblMCCCode.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblMCCCode.Location = New System.Drawing.Point(9, 25)
        Me.lblMCCCode.Name = "lblMCCCode"
        Me.lblMCCCode.Size = New System.Drawing.Size(60, 18)
        Me.lblMCCCode.TabIndex = 13
        Me.lblMCCCode.Text = "MCC Code"
        '
        'fndMCCCode
        '
        Me.fndMCCCode.CalculationExpression = Nothing
        Me.fndMCCCode.FieldCode = Nothing
        Me.fndMCCCode.FieldDesc = Nothing
        Me.fndMCCCode.FieldMaxLength = 0
        Me.fndMCCCode.FieldName = Nothing
        Me.fndMCCCode.isCalculatedField = False
        Me.fndMCCCode.IsSourceFromTable = False
        Me.fndMCCCode.IsSourceFromValueList = False
        Me.fndMCCCode.IsUnique = False
        Me.fndMCCCode.Location = New System.Drawing.Point(111, 24)
        Me.fndMCCCode.MendatroryField = True
        Me.fndMCCCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndMCCCode.MyLinkLable1 = Me.lblMCCCode
        Me.fndMCCCode.MyLinkLable2 = Nothing
        Me.fndMCCCode.MyReadOnly = False
        Me.fndMCCCode.MyShowMasterFormButton = False
        Me.fndMCCCode.Name = "fndMCCCode"
        Me.fndMCCCode.ReferenceFieldDesc = Nothing
        Me.fndMCCCode.ReferenceFieldName = Nothing
        Me.fndMCCCode.ReferenceTableName = Nothing
        Me.fndMCCCode.Size = New System.Drawing.Size(119, 21)
        Me.fndMCCCode.TabIndex = 12
        Me.fndMCCCode.Value = ""
        '
        'lblCode
        '
        Me.lblCode.FieldName = Nothing
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblCode.Location = New System.Drawing.Point(9, 3)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(88, 16)
        Me.lblCode.TabIndex = 10
        Me.lblCode.Text = "Document Code"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(888, 1)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(64, 21)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 11
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox2.Controls.Add(Me.lbErrors)
        Me.RadGroupBox2.Controls.Add(Me.gv1)
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "Milk Receipt Details"
        Me.RadGroupBox2.Location = New System.Drawing.Point(3, 243)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(960, 142)
        Me.RadGroupBox2.TabIndex = 1
        Me.RadGroupBox2.Text = "Milk Receipt Details"
        '
        'lbErrors
        '
        Me.lbErrors.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbErrors.Location = New System.Drawing.Point(853, 0)
        Me.lbErrors.Name = "lbErrors"
        Me.lbErrors.Size = New System.Drawing.Size(104, 142)
        Me.lbErrors.TabIndex = 1026
        Me.lbErrors.Visible = False
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
        Me.gv1.MasterTemplate.AllowColumnHeaderContextMenu = False
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(940, 112)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        '
        'pvpCustomFields
        '
        Me.pvpCustomFields.Controls.Add(Me.UcCustomFields1)
        Me.pvpCustomFields.ItemSize = New System.Drawing.SizeF(86.0!, 28.0!)
        Me.pvpCustomFields.Location = New System.Drawing.Point(10, 37)
        Me.pvpCustomFields.Name = "pvpCustomFields"
        Me.pvpCustomFields.Size = New System.Drawing.Size(967, 383)
        Me.pvpCustomFields.Text = "Custom Fields"
        '
        'UcCustomFields1
        '
        Me.UcCustomFields1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcCustomFields1.Location = New System.Drawing.Point(0, 0)
        Me.UcCustomFields1.Name = "UcCustomFields1"
        Me.UcCustomFields1.Size = New System.Drawing.Size(967, 383)
        Me.UcCustomFields1.TabIndex = 1
        '
        'Attachments
        '
        Me.Attachments.Controls.Add(Me.UcAttachment1)
        Me.Attachments.ItemSize = New System.Drawing.SizeF(79.0!, 28.0!)
        Me.Attachments.Location = New System.Drawing.Point(10, 37)
        Me.Attachments.Name = "Attachments"
        Me.Attachments.Size = New System.Drawing.Size(967, 383)
        Me.Attachments.Text = "Attachments"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(967, 383)
        Me.UcAttachment1.TabIndex = 0
        '
        'BtnExportImport
        '
        Me.BtnExportImport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnExportImport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItem2})
        Me.BtnExportImport.Location = New System.Drawing.Point(13, 7)
        Me.BtnExportImport.Name = "BtnExportImport"
        Me.BtnExportImport.Size = New System.Drawing.Size(95, 21)
        Me.BtnExportImport.TabIndex = 4
        Me.BtnExportImport.Text = "Export"
        Me.BtnExportImport.Visible = False
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Export"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Import"
        '
        'BtnPost
        '
        Me.BtnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPost.Location = New System.Drawing.Point(111, 7)
        Me.BtnPost.Name = "BtnPost"
        Me.BtnPost.Size = New System.Drawing.Size(66, 21)
        Me.BtnPost.TabIndex = 2
        Me.BtnPost.Text = "Post"
        Me.BtnPost.Visible = False
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(914, 7)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 21)
        Me.btnclose.TabIndex = 1
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(180, 7)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 21)
        Me.btndelete.TabIndex = 0
        Me.btndelete.Text = "Delete"
        '
        'rdmenufile
        '
        Me.rdmenufile.BackColor = System.Drawing.Color.Transparent
        Me.rdmenufile.Controls.Add(Me.pnlDock)
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenufile1})
        Me.rdmenufile.Location = New System.Drawing.Point(0, 0)
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Size = New System.Drawing.Size(988, 20)
        Me.rdmenufile.TabIndex = 4
        '
        'pnlDock
        '
        Me.pnlDock.Controls.Add(Me.lblDockName)
        Me.pnlDock.Controls.Add(Me.lblDockCode)
        Me.pnlDock.Controls.Add(Me.MyLabel8)
        Me.pnlDock.Location = New System.Drawing.Point(621, 3)
        Me.pnlDock.Name = "pnlDock"
        Me.pnlDock.Size = New System.Drawing.Size(364, 17)
        Me.pnlDock.TabIndex = 5
        Me.pnlDock.Visible = False
        '
        'lblDockName
        '
        Me.lblDockName.AutoSize = False
        Me.lblDockName.BorderVisible = True
        Me.lblDockName.FieldName = Nothing
        Me.lblDockName.Location = New System.Drawing.Point(192, 0)
        Me.lblDockName.Name = "lblDockName"
        Me.lblDockName.Size = New System.Drawing.Size(168, 16)
        Me.lblDockName.TabIndex = 1030
        '
        'lblDockCode
        '
        Me.lblDockCode.AutoSize = False
        Me.lblDockCode.BorderVisible = True
        Me.lblDockCode.FieldName = Nothing
        Me.lblDockCode.Location = New System.Drawing.Point(42, 0)
        Me.lblDockCode.Name = "lblDockCode"
        Me.lblDockCode.Size = New System.Drawing.Size(148, 16)
        Me.lblDockCode.TabIndex = 1029
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel8.Location = New System.Drawing.Point(5, 1)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(32, 16)
        Me.MyLabel8.TabIndex = 1028
        Me.MyLabel8.Text = "Dock"
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
        'frmMilkReceiptMCC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(988, 487)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.rdmenufile)
        Me.Name = "frmMilkReceiptMCC"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Milk Receipt"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.TxtToRange, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtFrmRange, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.txtVehicle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVehicle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVillageCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVillageName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.Panel2.PerformLayout()
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMilkWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboDockCollectionMilkType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Lbl_Type, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkALLVLC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.RadGroupBox4.PerformLayout()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TotalCansAllRoute, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtTotalWeightallRoute, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotalWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtTotalcans, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fndItem_Code, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fndVspCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkOther, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblMccName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblUom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboMilkType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMilkType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblItemDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoOfCans, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNoOfCans, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVehicleCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fndVehicleCode, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me.lbErrors, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pvpCustomFields.ResumeLayout(False)
        Me.Attachments.ResumeLayout(False)
        CType(Me.BtnExportImport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.rdmenufile.ResumeLayout(False)
        Me.pnlDock.ResumeLayout(False)
        Me.pnlDock.PerformLayout()
        CType(Me.lblDockName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDockCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
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
    Friend WithEvents fndMCCCode As common.UserControls.txtFinder
    Friend WithEvents lblDocDate As common.Controls.MyLabel
    Friend WithEvents dtpDocDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblBOMStatus As common.Controls.MyLabel
    Friend WithEvents cboShift As common.Controls.MyComboBox
    Friend WithEvents lblVLCCode As common.Controls.MyLabel
    Friend WithEvents fndVLCCode As common.UserControls.txtFinder
    Friend WithEvents lblVLCDesc As common.Controls.MyLabel
    Friend WithEvents lblVSPCode As common.Controls.MyLabel
    Friend WithEvents lblVSPDesc As common.Controls.MyLabel
    Friend WithEvents lblRouteCode As common.Controls.MyLabel
    Friend WithEvents fndRouteCode As common.UserControls.txtFinder
    Friend WithEvents lblRouteDesc As common.Controls.MyLabel
    Friend WithEvents lblVehicleCode As common.Controls.MyLabel
    Friend WithEvents fndVehicleCode As common.Controls.MyLabel
    Friend WithEvents txtNoOfCans As common.MyNumBox
    Friend WithEvents lblNoOfCans As common.Controls.MyLabel
    Friend WithEvents txtMilkWeight As common.MyNumBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents lblMilkType As common.Controls.MyLabel
    Friend WithEvents cboMilkType As common.Controls.MyComboBox
    Friend WithEvents lblType As common.Controls.MyLabel
    Friend WithEvents cboType As common.Controls.MyComboBox
    Friend WithEvents txtTotalWeight As common.MyNumBox
    Friend WithEvents lblTotalWeight As common.Controls.MyLabel
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents BtnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents lblItemDesc As common.Controls.MyLabel
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenu
    Friend WithEvents rdmenufile1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents BtnSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents BtnDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Attachments As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents TxtFrmRange As common.MyNumBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents TxtToRange As common.MyNumBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents LblUom As common.Controls.MyLabel
    Friend WithEvents LblMccName As common.Controls.MyLabel
    Friend WithEvents chkOther As common.Controls.MyCheckBox
    Friend WithEvents TxtTotalcans As common.MyNumBox
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents fndVspCode As common.Controls.MyLabel
    Friend WithEvents fndItem_Code As common.Controls.MyLabel
    Friend WithEvents TotalCansAllRoute As common.MyNumBox
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents TxtTotalWeightallRoute As common.MyNumBox
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents ChkALLVLC As common.Controls.MyCheckBox
    Friend WithEvents BtnExportImport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Lbl_Type As common.Controls.MyLabel
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents cboDockCollectionMilkType As common.Controls.MyComboBox
    Private WithEvents lbErrors As Telerik.WinControls.UI.RadListControl
    Friend WithEvents UcWeighing1 As XpertERPEngine.ucWeighing
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtCan As common.UserControls.txtFinder
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents pnlDock As System.Windows.Forms.Panel
    Friend WithEvents lblDockName As common.Controls.MyLabel
    Friend WithEvents lblDockCode As common.Controls.MyLabel
    Friend WithEvents lblVillageCode As common.Controls.MyLabel
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents lblVillageName As common.Controls.MyLabel
    Friend WithEvents lblVehicle As common.Controls.MyLabel
    Friend WithEvents txtVehicle As common.Controls.MyTextBox
End Class

