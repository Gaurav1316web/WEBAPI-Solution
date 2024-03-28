<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmPOWeighment
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
        Dim TableViewDefinition4 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition6 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition5 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.IsAutoWeighment = New System.Windows.Forms.CheckBox()
        Me.lblCode = New common.Controls.MyLabel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RGBUpdate = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtNetWeight = New common.MyNumBox()
        Me.txtExtraWeight = New common.MyNumBox()
        Me.lblExtraWeight = New common.Controls.MyLabel()
        Me.lblNetWeight = New common.Controls.MyLabel()
        Me.btnUpdateToSRN = New Telerik.WinControls.UI.RadButton()
        Me.btnUpdateWeighment = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.lbRALTender = New common.Controls.MyLabel()
        Me.txtGRNDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.lblGDShipToLocationName = New common.Controls.MyLabel()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.lblGDShipToLocation = New common.Controls.MyLabel()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.lblGDBillToLocationName = New common.Controls.MyLabel()
        Me.lblGDVendorName = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.lblGDCarrier = New common.Controls.MyLabel()
        Me.lblGDBillToLocation = New common.Controls.MyLabel()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.lblGDVendorCode = New common.Controls.MyLabel()
        Me.lblGDVehicleNo = New common.Controls.MyLabel()
        Me.lblDocDate = New common.Controls.MyLabel()
        Me.RadLabel6 = New common.Controls.MyLabel()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.UsGrossWeight = New common.usLock()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.txtGateEntryNo = New common.UserControls.txtFinder()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtGrossWeight = New common.MyNumBox()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.Items = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.GunnyBag = New Telerik.WinControls.UI.RadPageViewPage()
        Me.MyRadGridView1 = New common.UserControls.MyRadGridView()
        Me.gv2 = New common.UserControls.MyRadGridView()
        Me.btnPrintWithGunnyBags = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.BtnPost = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.UcWeighing1 = New XpertERPEngine.ucWeighing()
        Me.btnhistory = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.RGBUpdate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RGBUpdate.SuspendLayout()
        CType(Me.txtNetWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtExtraWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblExtraWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNetWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUpdateToSRN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUpdateWeighment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbRALTender, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGRNDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGDShipToLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGDShipToLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGDBillToLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGDVendorName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGDCarrier, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGDBillToLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGDVendorCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGDVehicleNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGrossWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.Items.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GunnyBag.SuspendLayout()
        CType(Me.MyRadGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyRadGridView1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MyRadGridView1.SuspendLayout()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrintWithGunnyBags, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnhistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 64)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnhistory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrintWithGunnyBags)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(997, 471)
        Me.SplitContainer1.SplitterDistance = 432
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.IsAutoWeighment)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadGroupBox1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnnew)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel8)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.UsGrossWeight)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel7)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtGateEntryNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtGrossWeight)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel3)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadPageView1)
        Me.SplitContainer2.Size = New System.Drawing.Size(997, 432)
        Me.SplitContainer2.SplitterDistance = 145
        Me.SplitContainer2.TabIndex = 1040
        '
        'IsAutoWeighment
        '
        Me.IsAutoWeighment.AutoSize = True
        Me.IsAutoWeighment.Enabled = False
        Me.IsAutoWeighment.Location = New System.Drawing.Point(96, 100)
        Me.IsAutoWeighment.Name = "IsAutoWeighment"
        Me.IsAutoWeighment.Size = New System.Drawing.Size(114, 17)
        Me.IsAutoWeighment.TabIndex = 1040
        Me.IsAutoWeighment.Text = "Auto Weighment"
        Me.IsAutoWeighment.UseVisualStyleBackColor = True
        '
        'lblCode
        '
        Me.lblCode.FieldName = Nothing
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblCode.Location = New System.Drawing.Point(5, 5)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(81, 16)
        Me.lblCode.TabIndex = 6
        Me.lblCode.Text = "Weighment No"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.RGBUpdate)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.lbRALTender)
        Me.RadGroupBox1.Controls.Add(Me.txtGRNDate)
        Me.RadGroupBox1.Controls.Add(Me.lblGDShipToLocationName)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel12)
        Me.RadGroupBox1.Controls.Add(Me.lblGDShipToLocation)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel9)
        Me.RadGroupBox1.Controls.Add(Me.lblGDBillToLocationName)
        Me.RadGroupBox1.Controls.Add(Me.lblGDVendorName)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel5)
        Me.RadGroupBox1.Controls.Add(Me.lblGDCarrier)
        Me.RadGroupBox1.Controls.Add(Me.lblGDBillToLocation)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox1.Controls.Add(Me.lblGDVendorCode)
        Me.RadGroupBox1.Controls.Add(Me.lblGDVehicleNo)
        Me.RadGroupBox1.Controls.Add(Me.lblDocDate)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel6)
        Me.RadGroupBox1.HeaderText = "GRN Details"
        Me.RadGroupBox1.Location = New System.Drawing.Point(355, 3)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(544, 140)
        Me.RadGroupBox1.TabIndex = 5
        Me.RadGroupBox1.Text = "GRN Details"
        '
        'RGBUpdate
        '
        Me.RGBUpdate.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RGBUpdate.Controls.Add(Me.txtNetWeight)
        Me.RGBUpdate.Controls.Add(Me.txtExtraWeight)
        Me.RGBUpdate.Controls.Add(Me.lblExtraWeight)
        Me.RGBUpdate.Controls.Add(Me.lblNetWeight)
        Me.RGBUpdate.Controls.Add(Me.btnUpdateToSRN)
        Me.RGBUpdate.Controls.Add(Me.btnUpdateWeighment)
        Me.RGBUpdate.HeaderText = "Update"
        Me.RGBUpdate.Location = New System.Drawing.Point(307, 4)
        Me.RGBUpdate.Name = "RGBUpdate"
        Me.RGBUpdate.Size = New System.Drawing.Size(252, 131)
        Me.RGBUpdate.TabIndex = 1041
        Me.RGBUpdate.Text = "Update"
        Me.RGBUpdate.Visible = False
        '
        'txtNetWeight
        '
        Me.txtNetWeight.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtNetWeight.CalculationExpression = Nothing
        Me.txtNetWeight.DecimalPlaces = 3
        Me.txtNetWeight.FieldCode = Nothing
        Me.txtNetWeight.FieldDesc = Nothing
        Me.txtNetWeight.FieldMaxLength = 0
        Me.txtNetWeight.FieldName = Nothing
        Me.txtNetWeight.isCalculatedField = False
        Me.txtNetWeight.IsSourceFromTable = False
        Me.txtNetWeight.IsSourceFromValueList = False
        Me.txtNetWeight.IsUnique = False
        Me.txtNetWeight.Location = New System.Drawing.Point(90, 58)
        Me.txtNetWeight.MendatroryField = True
        Me.txtNetWeight.MyLinkLable1 = Nothing
        Me.txtNetWeight.MyLinkLable2 = Nothing
        Me.txtNetWeight.Name = "txtNetWeight"
        Me.txtNetWeight.ReferenceFieldDesc = Nothing
        Me.txtNetWeight.ReferenceFieldName = Nothing
        Me.txtNetWeight.ReferenceTableName = Nothing
        Me.txtNetWeight.Size = New System.Drawing.Size(146, 20)
        Me.txtNetWeight.TabIndex = 13
        Me.txtNetWeight.Text = "0"
        Me.txtNetWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtNetWeight.Value = 0R
        '
        'txtExtraWeight
        '
        Me.txtExtraWeight.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtExtraWeight.CalculationExpression = Nothing
        Me.txtExtraWeight.DecimalPlaces = 3
        Me.txtExtraWeight.FieldCode = Nothing
        Me.txtExtraWeight.FieldDesc = Nothing
        Me.txtExtraWeight.FieldMaxLength = 0
        Me.txtExtraWeight.FieldName = Nothing
        Me.txtExtraWeight.isCalculatedField = False
        Me.txtExtraWeight.IsSourceFromTable = False
        Me.txtExtraWeight.IsSourceFromValueList = False
        Me.txtExtraWeight.IsUnique = False
        Me.txtExtraWeight.Location = New System.Drawing.Point(90, 35)
        Me.txtExtraWeight.MendatroryField = True
        Me.txtExtraWeight.MyLinkLable1 = Nothing
        Me.txtExtraWeight.MyLinkLable2 = Nothing
        Me.txtExtraWeight.Name = "txtExtraWeight"
        Me.txtExtraWeight.ReferenceFieldDesc = Nothing
        Me.txtExtraWeight.ReferenceFieldName = Nothing
        Me.txtExtraWeight.ReferenceTableName = Nothing
        Me.txtExtraWeight.Size = New System.Drawing.Size(146, 20)
        Me.txtExtraWeight.TabIndex = 12
        Me.txtExtraWeight.Text = "0"
        Me.txtExtraWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtExtraWeight.Value = 0R
        '
        'lblExtraWeight
        '
        Me.lblExtraWeight.FieldName = Nothing
        Me.lblExtraWeight.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblExtraWeight.Location = New System.Drawing.Point(18, 35)
        Me.lblExtraWeight.Name = "lblExtraWeight"
        Me.lblExtraWeight.Size = New System.Drawing.Size(71, 16)
        Me.lblExtraWeight.TabIndex = 11
        Me.lblExtraWeight.Text = "Extra Weight"
        '
        'lblNetWeight
        '
        Me.lblNetWeight.FieldName = Nothing
        Me.lblNetWeight.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblNetWeight.Location = New System.Drawing.Point(18, 61)
        Me.lblNetWeight.Name = "lblNetWeight"
        Me.lblNetWeight.Size = New System.Drawing.Size(62, 16)
        Me.lblNetWeight.TabIndex = 10
        Me.lblNetWeight.Text = "Net Weight"
        '
        'btnUpdateToSRN
        '
        Me.btnUpdateToSRN.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnUpdateToSRN.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUpdateToSRN.Location = New System.Drawing.Point(136, 94)
        Me.btnUpdateToSRN.Name = "btnUpdateToSRN"
        Me.btnUpdateToSRN.Size = New System.Drawing.Size(100, 21)
        Me.btnUpdateToSRN.TabIndex = 2
        Me.btnUpdateToSRN.Text = "Update To SRN"
        '
        'btnUpdateWeighment
        '
        Me.btnUpdateWeighment.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnUpdateWeighment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUpdateWeighment.Location = New System.Drawing.Point(18, 94)
        Me.btnUpdateWeighment.Name = "btnUpdateWeighment"
        Me.btnUpdateWeighment.Size = New System.Drawing.Size(100, 21)
        Me.btnUpdateWeighment.TabIndex = 1
        Me.btnUpdateWeighment.Text = "Update"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(5, 118)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(85, 16)
        Me.MyLabel2.TabIndex = 1054
        Me.MyLabel2.Text = "RAL Tender No"
        '
        'lbRALTender
        '
        Me.lbRALTender.AutoSize = False
        Me.lbRALTender.BorderVisible = True
        Me.lbRALTender.FieldName = Nothing
        Me.lbRALTender.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbRALTender.Location = New System.Drawing.Point(100, 118)
        Me.lbRALTender.Name = "lbRALTender"
        Me.lbRALTender.Size = New System.Drawing.Size(143, 18)
        Me.lbRALTender.TabIndex = 1053
        Me.lbRALTender.TextWrap = False
        '
        'txtGRNDate
        '
        Me.txtGRNDate.CalculationExpression = Nothing
        Me.txtGRNDate.CustomFormat = "dd/MM/yyyy hh:mm:ss tt"
        Me.txtGRNDate.Enabled = False
        Me.txtGRNDate.FieldCode = Nothing
        Me.txtGRNDate.FieldDesc = Nothing
        Me.txtGRNDate.FieldMaxLength = 0
        Me.txtGRNDate.FieldName = Nothing
        Me.txtGRNDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGRNDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtGRNDate.isCalculatedField = False
        Me.txtGRNDate.IsSourceFromTable = False
        Me.txtGRNDate.IsSourceFromValueList = False
        Me.txtGRNDate.IsUnique = False
        Me.txtGRNDate.Location = New System.Drawing.Point(100, 11)
        Me.txtGRNDate.MendatroryField = True
        Me.txtGRNDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtGRNDate.MyLinkLable1 = Me.MyLabel8
        Me.txtGRNDate.MyLinkLable2 = Nothing
        Me.txtGRNDate.Name = "txtGRNDate"
        Me.txtGRNDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtGRNDate.ReferenceFieldDesc = Nothing
        Me.txtGRNDate.ReferenceFieldName = Nothing
        Me.txtGRNDate.ReferenceTableName = Nothing
        Me.txtGRNDate.Size = New System.Drawing.Size(143, 18)
        Me.txtGRNDate.TabIndex = 1052
        Me.txtGRNDate.TabStop = False
        Me.txtGRNDate.Text = "03/05/2011 12:00:00 AM"
        Me.txtGRNDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(5, 29)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(91, 16)
        Me.MyLabel8.TabIndex = 7
        Me.MyLabel8.Text = "Weighment Date"
        '
        'lblGDShipToLocationName
        '
        Me.lblGDShipToLocationName.AutoSize = False
        Me.lblGDShipToLocationName.BorderVisible = True
        Me.lblGDShipToLocationName.FieldName = Nothing
        Me.lblGDShipToLocationName.Location = New System.Drawing.Point(248, 106)
        Me.lblGDShipToLocationName.Name = "lblGDShipToLocationName"
        Me.lblGDShipToLocationName.Size = New System.Drawing.Size(296, 18)
        Me.lblGDShipToLocationName.TabIndex = 1049
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel12.Location = New System.Drawing.Point(5, 97)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(91, 16)
        Me.MyLabel12.TabIndex = 1051
        Me.MyLabel12.Text = "Ship To Location"
        '
        'lblGDShipToLocation
        '
        Me.lblGDShipToLocation.AutoSize = False
        Me.lblGDShipToLocation.BorderVisible = True
        Me.lblGDShipToLocation.FieldName = Nothing
        Me.lblGDShipToLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGDShipToLocation.Location = New System.Drawing.Point(100, 97)
        Me.lblGDShipToLocation.Name = "lblGDShipToLocation"
        Me.lblGDShipToLocation.Size = New System.Drawing.Size(143, 18)
        Me.lblGDShipToLocation.TabIndex = 1050
        Me.lblGDShipToLocation.TextWrap = False
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(248, 41)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(41, 16)
        Me.MyLabel9.TabIndex = 1047
        Me.MyLabel9.Text = "Carrier"
        '
        'lblGDBillToLocationName
        '
        Me.lblGDBillToLocationName.AutoSize = False
        Me.lblGDBillToLocationName.BorderVisible = True
        Me.lblGDBillToLocationName.FieldName = Nothing
        Me.lblGDBillToLocationName.Location = New System.Drawing.Point(248, 84)
        Me.lblGDBillToLocationName.Name = "lblGDBillToLocationName"
        Me.lblGDBillToLocationName.Size = New System.Drawing.Size(296, 18)
        Me.lblGDBillToLocationName.TabIndex = 1044
        '
        'lblGDVendorName
        '
        Me.lblGDVendorName.AutoSize = False
        Me.lblGDVendorName.BorderVisible = True
        Me.lblGDVendorName.FieldName = Nothing
        Me.lblGDVendorName.Location = New System.Drawing.Point(248, 62)
        Me.lblGDVendorName.Name = "lblGDVendorName"
        Me.lblGDVendorName.Size = New System.Drawing.Size(296, 18)
        Me.lblGDVendorName.TabIndex = 35
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(5, 76)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(84, 16)
        Me.MyLabel5.TabIndex = 1046
        Me.MyLabel5.Text = "Bill To Location"
        '
        'lblGDCarrier
        '
        Me.lblGDCarrier.AutoSize = False
        Me.lblGDCarrier.BorderVisible = True
        Me.lblGDCarrier.FieldName = Nothing
        Me.lblGDCarrier.Location = New System.Drawing.Point(298, 40)
        Me.lblGDCarrier.Name = "lblGDCarrier"
        Me.lblGDCarrier.Size = New System.Drawing.Size(246, 18)
        Me.lblGDCarrier.TabIndex = 1044
        '
        'lblGDBillToLocation
        '
        Me.lblGDBillToLocation.AutoSize = False
        Me.lblGDBillToLocation.BorderVisible = True
        Me.lblGDBillToLocation.FieldName = Nothing
        Me.lblGDBillToLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGDBillToLocation.Location = New System.Drawing.Point(100, 75)
        Me.lblGDBillToLocation.Name = "lblGDBillToLocation"
        Me.lblGDBillToLocation.Size = New System.Drawing.Size(143, 18)
        Me.lblGDBillToLocation.TabIndex = 1045
        Me.lblGDBillToLocation.TextWrap = False
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(5, 54)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(60, 16)
        Me.RadLabel2.TabIndex = 1043
        Me.RadLabel2.Text = "Vendor No"
        '
        'lblGDVendorCode
        '
        Me.lblGDVendorCode.AutoSize = False
        Me.lblGDVendorCode.BorderVisible = True
        Me.lblGDVendorCode.FieldName = Nothing
        Me.lblGDVendorCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGDVendorCode.Location = New System.Drawing.Point(100, 53)
        Me.lblGDVendorCode.Name = "lblGDVendorCode"
        Me.lblGDVendorCode.Size = New System.Drawing.Size(143, 18)
        Me.lblGDVendorCode.TabIndex = 1042
        Me.lblGDVendorCode.TextWrap = False
        '
        'lblGDVehicleNo
        '
        Me.lblGDVehicleNo.AutoSize = False
        Me.lblGDVehicleNo.BorderVisible = True
        Me.lblGDVehicleNo.FieldName = Nothing
        Me.lblGDVehicleNo.Location = New System.Drawing.Point(100, 31)
        Me.lblGDVehicleNo.Name = "lblGDVehicleNo"
        Me.lblGDVehicleNo.Size = New System.Drawing.Size(143, 18)
        Me.lblGDVehicleNo.TabIndex = 34
        '
        'lblDocDate
        '
        Me.lblDocDate.FieldName = Nothing
        Me.lblDocDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocDate.Location = New System.Drawing.Point(5, 13)
        Me.lblDocDate.Name = "lblDocDate"
        Me.lblDocDate.Size = New System.Drawing.Size(58, 16)
        Me.lblDocDate.TabIndex = 1028
        Me.lblDocDate.Text = "GRN Date"
        '
        'RadLabel6
        '
        Me.RadLabel6.FieldName = Nothing
        Me.RadLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel6.Location = New System.Drawing.Point(5, 32)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(61, 16)
        Me.RadLabel6.TabIndex = 1041
        Me.RadLabel6.Text = "Vehicle No"
        '
        'btnnew
        '
        Me.btnnew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnnew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnnew.Location = New System.Drawing.Point(330, 3)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(19, 21)
        Me.btnnew.TabIndex = 0
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(96, 3)
        Me.txtCode.MendatroryField = False
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 30
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(234, 21)
        Me.txtCode.TabIndex = 4
        Me.txtCode.Value = ""
        '
        'txtDate
        '
        Me.txtDate.CalculationExpression = Nothing
        Me.txtDate.CustomFormat = "dd/MM/yyyy hh:mm:ss tt"
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
        Me.txtDate.Location = New System.Drawing.Point(96, 28)
        Me.txtDate.MendatroryField = True
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.MyLabel8
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(146, 18)
        Me.txtDate.TabIndex = 1
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "03/05/2011 12:00:00 AM"
        Me.txtDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'UsGrossWeight
        '
        Me.UsGrossWeight.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsGrossWeight.Location = New System.Drawing.Point(249, 27)
        Me.UsGrossWeight.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsGrossWeight.Name = "UsGrossWeight"
        Me.UsGrossWeight.Size = New System.Drawing.Size(98, 21)
        Me.UsGrossWeight.Status = common.ERPTransactionStatus.Pending
        Me.UsGrossWeight.TabIndex = 1039
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel7.Location = New System.Drawing.Point(5, 76)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(75, 16)
        Me.MyLabel7.TabIndex = 9
        Me.MyLabel7.Text = "Gross Weight"
        '
        'txtGateEntryNo
        '
        Me.txtGateEntryNo.CalculationExpression = Nothing
        Me.txtGateEntryNo.FieldCode = Nothing
        Me.txtGateEntryNo.FieldDesc = Nothing
        Me.txtGateEntryNo.FieldMaxLength = 0
        Me.txtGateEntryNo.FieldName = Nothing
        Me.txtGateEntryNo.isCalculatedField = False
        Me.txtGateEntryNo.IsSourceFromTable = False
        Me.txtGateEntryNo.IsSourceFromValueList = False
        Me.txtGateEntryNo.IsUnique = False
        Me.txtGateEntryNo.Location = New System.Drawing.Point(96, 50)
        Me.txtGateEntryNo.MendatroryField = True
        Me.txtGateEntryNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGateEntryNo.MyLinkLable1 = Me.MyLabel3
        Me.txtGateEntryNo.MyLinkLable2 = Nothing
        Me.txtGateEntryNo.MyReadOnly = False
        Me.txtGateEntryNo.MyShowMasterFormButton = False
        Me.txtGateEntryNo.Name = "txtGateEntryNo"
        Me.txtGateEntryNo.ReferenceFieldDesc = Nothing
        Me.txtGateEntryNo.ReferenceFieldName = Nothing
        Me.txtGateEntryNo.ReferenceTableName = Nothing
        Me.txtGateEntryNo.Size = New System.Drawing.Size(146, 20)
        Me.txtGateEntryNo.TabIndex = 2
        Me.txtGateEntryNo.Value = ""
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel3.Location = New System.Drawing.Point(5, 51)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(47, 18)
        Me.MyLabel3.TabIndex = 8
        Me.MyLabel3.Text = "GRN No"
        '
        'txtGrossWeight
        '
        Me.txtGrossWeight.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtGrossWeight.CalculationExpression = Nothing
        Me.txtGrossWeight.DecimalPlaces = 3
        Me.txtGrossWeight.FieldCode = Nothing
        Me.txtGrossWeight.FieldDesc = Nothing
        Me.txtGrossWeight.FieldMaxLength = 0
        Me.txtGrossWeight.FieldName = Nothing
        Me.txtGrossWeight.isCalculatedField = False
        Me.txtGrossWeight.IsSourceFromTable = False
        Me.txtGrossWeight.IsSourceFromValueList = False
        Me.txtGrossWeight.IsUnique = False
        Me.txtGrossWeight.Location = New System.Drawing.Point(96, 74)
        Me.txtGrossWeight.MendatroryField = True
        Me.txtGrossWeight.MyLinkLable1 = Nothing
        Me.txtGrossWeight.MyLinkLable2 = Nothing
        Me.txtGrossWeight.Name = "txtGrossWeight"
        Me.txtGrossWeight.ReadOnly = True
        Me.txtGrossWeight.ReferenceFieldDesc = Nothing
        Me.txtGrossWeight.ReferenceFieldName = Nothing
        Me.txtGrossWeight.ReferenceTableName = Nothing
        Me.txtGrossWeight.Size = New System.Drawing.Size(146, 20)
        Me.txtGrossWeight.TabIndex = 3
        Me.txtGrossWeight.Text = "0"
        Me.txtGrossWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtGrossWeight.Value = 0R
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.Items)
        Me.RadPageView1.Controls.Add(Me.GunnyBag)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.Items
        Me.RadPageView1.Size = New System.Drawing.Size(997, 283)
        Me.RadPageView1.TabIndex = 0
        '
        'Items
        '
        Me.Items.Controls.Add(Me.gv1)
        Me.Items.ItemSize = New System.Drawing.SizeF(44.0!, 28.0!)
        Me.Items.Location = New System.Drawing.Point(10, 37)
        Me.Items.Name = "Items"
        Me.Items.Size = New System.Drawing.Size(976, 235)
        Me.Items.Text = "Items"
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
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition4
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(976, 235)
        Me.gv1.TabIndex = 1521
        Me.gv1.TabStop = False
        '
        'GunnyBag
        '
        Me.GunnyBag.Controls.Add(Me.MyRadGridView1)
        Me.GunnyBag.ItemSize = New System.Drawing.SizeF(68.0!, 28.0!)
        Me.GunnyBag.Location = New System.Drawing.Point(10, 37)
        Me.GunnyBag.Name = "GunnyBag"
        Me.GunnyBag.Size = New System.Drawing.Size(976, 235)
        Me.GunnyBag.Text = "GunnyBag"
        '
        'MyRadGridView1
        '
        Me.MyRadGridView1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.MyRadGridView1.Controls.Add(Me.gv2)
        Me.MyRadGridView1.Cursor = System.Windows.Forms.Cursors.Default
        Me.MyRadGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MyRadGridView1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyRadGridView1.ForeColor = System.Drawing.Color.Black
        Me.MyRadGridView1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.MyRadGridView1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.MyRadGridView1.MasterTemplate.AllowDeleteRow = False
        Me.MyRadGridView1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.MyRadGridView1.MasterTemplate.ShowHeaderCellButtons = True
        Me.MyRadGridView1.MasterTemplate.ViewDefinition = TableViewDefinition6
        Me.MyRadGridView1.MyStopExport = False
        Me.MyRadGridView1.Name = "MyRadGridView1"
        Me.MyRadGridView1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.MyRadGridView1.ShowGroupPanel = False
        Me.MyRadGridView1.ShowHeaderCellButtons = True
        Me.MyRadGridView1.Size = New System.Drawing.Size(976, 235)
        Me.MyRadGridView1.TabIndex = 1521
        Me.MyRadGridView1.TabStop = False
        '
        'gv2
        '
        Me.gv2.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv2.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv2.ForeColor = System.Drawing.Color.Black
        Me.gv2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv2.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv2.MasterTemplate.AllowDeleteRow = False
        Me.gv2.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv2.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv2.MasterTemplate.ViewDefinition = TableViewDefinition5
        Me.gv2.MyStopExport = False
        Me.gv2.Name = "gv2"
        Me.gv2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv2.ShowGroupPanel = False
        Me.gv2.ShowHeaderCellButtons = True
        Me.gv2.Size = New System.Drawing.Size(976, 235)
        Me.gv2.TabIndex = 1522
        Me.gv2.TabStop = False
        '
        'btnPrintWithGunnyBags
        '
        Me.btnPrintWithGunnyBags.Location = New System.Drawing.Point(285, 7)
        Me.btnPrintWithGunnyBags.Name = "btnPrintWithGunnyBags"
        Me.btnPrintWithGunnyBags.Size = New System.Drawing.Size(136, 21)
        Me.btnPrintWithGunnyBags.TabIndex = 5
        Me.btnPrintWithGunnyBags.Text = "Print With Gunny Bags"
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(213, 7)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(66, 21)
        Me.btnPrint.TabIndex = 4
        Me.btnPrint.Text = "Print"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(926, 7)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 21)
        Me.btnclose.TabIndex = 3
        Me.btnclose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(5, 7)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(66, 21)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        Me.btnSave.Visible = False
        '
        'BtnPost
        '
        Me.BtnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPost.Location = New System.Drawing.Point(143, 7)
        Me.BtnPost.Name = "BtnPost"
        Me.BtnPost.Size = New System.Drawing.Size(66, 21)
        Me.BtnPost.TabIndex = 2
        Me.BtnPost.Text = "Post"
        Me.BtnPost.Visible = False
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(74, 7)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 21)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
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
        Me.UcWeighing1.Size = New System.Drawing.Size(997, 64)
        Me.UcWeighing1.TabIndex = 1
        '
        'btnhistory
        '
        Me.btnhistory.Location = New System.Drawing.Point(427, 7)
        Me.btnhistory.Name = "btnhistory"
        Me.btnhistory.Size = New System.Drawing.Size(66, 21)
        Me.btnhistory.TabIndex = 6
        Me.btnhistory.Text = "History"
        '
        'frmPOWeighment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(997, 535)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.UcWeighing1)
        Me.Name = "frmPOWeighment"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "PO Weighment"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.RGBUpdate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RGBUpdate.ResumeLayout(False)
        Me.RGBUpdate.PerformLayout()
        CType(Me.txtNetWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtExtraWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblExtraWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNetWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUpdateToSRN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUpdateWeighment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbRALTender, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGRNDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGDShipToLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGDShipToLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGDBillToLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGDVendorName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGDCarrier, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGDBillToLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGDVendorCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGDVehicleNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGrossWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.Items.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GunnyBag.ResumeLayout(False)
        CType(Me.MyRadGridView1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyRadGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MyRadGridView1.ResumeLayout(False)
        Me.MyRadGridView1.PerformLayout()
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrintWithGunnyBags, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnhistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents BtnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblDocDate As common.Controls.MyLabel
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents UsGrossWeight As common.usLock
    Friend WithEvents RadLabel6 As common.Controls.MyLabel
    Friend WithEvents lblGDVehicleNo As common.Controls.MyLabel
    Friend WithEvents UcWeighing1 As XpertERPEngine.ucWeighing
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtGateEntryNo As common.UserControls.txtFinder
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents txtGrossWeight As common.MyNumBox
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents lblGDVendorCode As common.Controls.MyLabel
    Friend WithEvents lblGDVendorName As common.Controls.MyLabel
    Friend WithEvents lblGDCarrier As common.Controls.MyLabel
    Friend WithEvents lblGDBillToLocationName As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents lblGDBillToLocation As common.Controls.MyLabel
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents lblGDShipToLocationName As common.Controls.MyLabel
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents lblGDShipToLocation As common.Controls.MyLabel
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents IsAutoWeighment As System.Windows.Forms.CheckBox
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtGRNDate As common.Controls.MyDateTimePicker
    Friend WithEvents RGBUpdate As RadGroupBox
    Friend WithEvents btnUpdateToSRN As RadButton
    Friend WithEvents btnUpdateWeighment As RadButton
    Friend WithEvents lblExtraWeight As common.Controls.MyLabel
    Friend WithEvents lblNetWeight As common.Controls.MyLabel
    Friend WithEvents txtNetWeight As common.MyNumBox
    Friend WithEvents txtExtraWeight As common.MyNumBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents lbRALTender As common.Controls.MyLabel
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents Items As RadPageViewPage
    Friend WithEvents GunnyBag As RadPageViewPage
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents MyRadGridView1 As common.UserControls.MyRadGridView
    Friend WithEvents gv2 As common.UserControls.MyRadGridView
    Friend WithEvents btnPrintWithGunnyBags As RadButton
    Friend WithEvents btnhistory As RadButton
End Class
