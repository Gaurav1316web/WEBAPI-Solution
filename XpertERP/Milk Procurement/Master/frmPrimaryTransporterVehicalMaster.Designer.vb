<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPrimaryTransporterVehicalMaster
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
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem5 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.MenuClose = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnexport = New Telerik.WinControls.UI.RadMenuItem()
        Me.Export_Vehical_Details = New Telerik.WinControls.UI.RadMenuItem()
        Me.Export_Slab_Details = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnimport = New Telerik.WinControls.UI.RadMenuItem()
        Me.Import_Vehical_Details = New Telerik.WinControls.UI.RadMenuItem()
        Me.Import_Slab_Details = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.txtVehicle = New common.Controls.MyTextBox()
        Me.lblVehicle = New common.Controls.MyLabel()
        Me.txtEffectiveStartDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.chkTwoWay = New common.Controls.MyCheckBox()
        Me.txtVehicleWeight = New common.MyNumBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.fndcode = New common.UserControls.txtNavigator()
        Me.lblvandorno = New common.Controls.MyLabel()
        Me.FndRoute = New common.UserControls.txtFinder()
        Me.MyLabel17 = New common.Controls.MyLabel()
        Me.LblRoute = New common.Controls.MyLabel()
        Me.txtTankerNo = New common.Controls.MyTextBox()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.txtMRPDRent = New common.MyNumBox()
        Me.MyLabel21 = New common.Controls.MyLabel()
        Me.chkMonthlyRentPlusDiesel = New common.Controls.MyCheckBox()
        Me.MyLabel19 = New common.Controls.MyLabel()
        Me.txtMRPDAverage = New common.MyNumBox()
        Me.MyLabel20 = New common.Controls.MyLabel()
        Me.txtMRPDDieselRate = New common.MyNumBox()
        Me.chkIsAdditional = New common.Controls.MyCheckBox()
        Me.gv = New common.UserControls.MyRadGridView()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.rbtndiesel = New common.Controls.MyCheckBox()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.txtchrg = New common.MyNumBox()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.txtavgkm = New common.MyNumBox()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.txtdiesel = New common.MyNumBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.cmbRentalType = New common.Controls.MyComboBox()
        Me.rbtnrental = New common.Controls.MyCheckBox()
        Me.lblRentalType = New common.Controls.MyLabel()
        Me.txtRentalAmt = New common.MyNumBox()
        Me.lblRentalAmount = New common.Controls.MyLabel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txt_ltr = New common.MyNumBox()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.cmbLtrKG = New common.Controls.MyComboBox()
        Me.rbtnrateltr = New common.Controls.MyCheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txt_km = New common.MyNumBox()
        Me.rbtnratekm = New common.Controls.MyCheckBox()
        Me.rbtKmrange = New common.Controls.MyCheckBox()
        Me.txtmcccode = New common.UserControls.txtFinder()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtmccname = New common.Controls.MyLabel()
        Me.txtyear = New common.Controls.MyDateTimePicker()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtcapcity = New common.MyNumBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtprimarycode = New common.UserControls.txtFinder()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtprimaryname = New common.Controls.MyLabel()
        Me.txtdesc = New common.Controls.MyTextBox()
        Me.lblvendorname = New common.Controls.MyLabel()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.pageVehicleFitness = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gbVehFitNo = New Telerik.WinControls.UI.RadGroupBox()
        Me.lblVehInsuranceNo = New Telerik.WinControls.UI.RadLabel()
        Me.lblVehicleInsuranceDate = New Telerik.WinControls.UI.RadLabel()
        Me.dtpInsurance = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.txtVehInsuranceNo = New Telerik.WinControls.UI.RadTextBox()
        Me.gbxVehInsInfo = New Telerik.WinControls.UI.RadGroupBox()
        Me.lblVehFitNo = New Telerik.WinControls.UI.RadLabel()
        Me.lblVehFitnessDate = New Telerik.WinControls.UI.RadLabel()
        Me.txtVehFitnessNo = New Telerik.WinControls.UI.RadTextBox()
        Me.dtpVehFitness = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.btnHistory = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.txtVehicle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVehicle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEffectiveStartDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkTwoWay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVehicleWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblvandorno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblRoute, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTankerNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        CType(Me.txtMRPDRent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMonthlyRentPlusDiesel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMRPDAverage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMRPDDieselRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIsAdditional, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        CType(Me.rbtndiesel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtchrg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtavgkm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdiesel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        CType(Me.cmbRentalType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnrental, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRentalType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRentalAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRentalAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.txt_ltr, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbLtrKG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnrateltr, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.txt_km, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnratekm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtKmrange, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtmccname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtyear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcapcity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtprimaryname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblvendorname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        Me.pageVehicleFitness.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gbVehFitNo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbVehFitNo.SuspendLayout()
        CType(Me.lblVehInsuranceNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVehicleInsuranceDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpInsurance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVehInsuranceNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gbxVehInsInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbxVehInsInfo.SuspendLayout()
        CType(Me.lblVehFitNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVehFitnessDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVehFitnessNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpVehFitness, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Panel1.Padding = New System.Windows.Forms.Padding(3)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnHistory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(996, 580)
        Me.SplitContainer1.SplitterDistance = 543
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadMenu1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadPageView1)
        Me.SplitContainer2.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.SplitContainer2.Size = New System.Drawing.Size(990, 537)
        Me.SplitContainer2.SplitterDistance = 25
        Me.SplitContainer2.TabIndex = 0
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.MenuClose})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(990, 20)
        Me.RadMenu1.TabIndex = 11
        Me.RadMenu1.Text = "RadMenu1"
        '
        'MenuClose
        '
        Me.MenuClose.AccessibleDescription = "File"
        Me.MenuClose.AccessibleName = "File"
        Me.MenuClose.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnexport, Me.btnimport})
        Me.MenuClose.Name = "MenuClose"
        Me.MenuClose.Text = "File"
        '
        'btnexport
        '
        Me.btnexport.AccessibleDescription = "Export"
        Me.btnexport.AccessibleName = "Export"
        Me.btnexport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.Export_Vehical_Details, Me.Export_Slab_Details})
        Me.btnexport.Name = "btnexport"
        Me.btnexport.Text = "Export"
        '
        'Export_Vehical_Details
        '
        Me.Export_Vehical_Details.AccessibleDescription = "Vehicle Details"
        Me.Export_Vehical_Details.AccessibleName = "Vehicle Details"
        Me.Export_Vehical_Details.Name = "Export_Vehical_Details"
        Me.Export_Vehical_Details.Text = "Vehicle Details"
        '
        'Export_Slab_Details
        '
        Me.Export_Slab_Details.AccessibleDescription = "RadMenuItem1"
        Me.Export_Slab_Details.AccessibleName = "RadMenuItem1"
        Me.Export_Slab_Details.Name = "Export_Slab_Details"
        Me.Export_Slab_Details.Text = "Slab Details"
        '
        'btnimport
        '
        Me.btnimport.AccessibleDescription = "Import"
        Me.btnimport.AccessibleName = "Import"
        Me.btnimport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.Import_Vehical_Details, Me.Import_Slab_Details})
        Me.btnimport.Name = "btnimport"
        Me.btnimport.Text = "Import"
        '
        'Import_Vehical_Details
        '
        Me.Import_Vehical_Details.AccessibleDescription = "Vehical Details"
        Me.Import_Vehical_Details.AccessibleName = "Vehical Details"
        Me.Import_Vehical_Details.Name = "Import_Vehical_Details"
        Me.Import_Vehical_Details.Text = "Vehicle Details"
        '
        'Import_Slab_Details
        '
        Me.Import_Slab_Details.AccessibleDescription = "Slab Details"
        Me.Import_Slab_Details.AccessibleName = "Slab Details"
        Me.Import_Slab_Details.Name = "Import_Slab_Details"
        Me.Import_Slab_Details.Text = "Slab Details"
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.pageVehicleFitness)
        Me.RadPageView1.DefaultPage = Me.RadPageViewPage1
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(3, 3)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(984, 502)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.txtVehicle)
        Me.RadPageViewPage1.Controls.Add(Me.lblVehicle)
        Me.RadPageViewPage1.Controls.Add(Me.txtEffectiveStartDate)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel11)
        Me.RadPageViewPage1.Controls.Add(Me.chkTwoWay)
        Me.RadPageViewPage1.Controls.Add(Me.txtVehicleWeight)
        Me.RadPageViewPage1.Controls.Add(Me.fndcode)
        Me.RadPageViewPage1.Controls.Add(Me.FndRoute)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.LblRoute)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel17)
        Me.RadPageViewPage1.Controls.Add(Me.txtTankerNo)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.txtmcccode)
        Me.RadPageViewPage1.Controls.Add(Me.txtmccname)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.txtyear)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.txtcapcity)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.txtprimarycode)
        Me.RadPageViewPage1.Controls.Add(Me.txtprimaryname)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.lblvandorno)
        Me.RadPageViewPage1.Controls.Add(Me.txtdesc)
        Me.RadPageViewPage1.Controls.Add(Me.btnnew)
        Me.RadPageViewPage1.Controls.Add(Me.lblvendorname)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(45.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(963, 454)
        Me.RadPageViewPage1.Text = "Detail"
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
        Me.txtVehicle.Location = New System.Drawing.Point(518, 3)
        Me.txtVehicle.MaxLength = 200
        Me.txtVehicle.MendatroryField = False
        Me.txtVehicle.MyLinkLable1 = Nothing
        Me.txtVehicle.MyLinkLable2 = Nothing
        Me.txtVehicle.Name = "txtVehicle"
        Me.txtVehicle.ReferenceFieldDesc = Nothing
        Me.txtVehicle.ReferenceFieldName = Nothing
        Me.txtVehicle.ReferenceTableName = Nothing
        Me.txtVehicle.Size = New System.Drawing.Size(135, 18)
        Me.txtVehicle.TabIndex = 1445
        '
        'lblVehicle
        '
        Me.lblVehicle.FieldName = Nothing
        Me.lblVehicle.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblVehicle.Location = New System.Drawing.Point(469, 3)
        Me.lblVehicle.Name = "lblVehicle"
        Me.lblVehicle.Size = New System.Drawing.Size(43, 16)
        Me.lblVehicle.TabIndex = 117
        Me.lblVehicle.Text = "Vehicle"
        '
        'txtEffectiveStartDate
        '
        Me.txtEffectiveStartDate.CalculationExpression = Nothing
        Me.txtEffectiveStartDate.CustomFormat = "dd/MM/yyyy"
        Me.txtEffectiveStartDate.FieldCode = Nothing
        Me.txtEffectiveStartDate.FieldDesc = Nothing
        Me.txtEffectiveStartDate.FieldMaxLength = 0
        Me.txtEffectiveStartDate.FieldName = Nothing
        Me.txtEffectiveStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtEffectiveStartDate.isCalculatedField = False
        Me.txtEffectiveStartDate.IsSourceFromTable = False
        Me.txtEffectiveStartDate.IsSourceFromValueList = False
        Me.txtEffectiveStartDate.IsUnique = False
        Me.txtEffectiveStartDate.Location = New System.Drawing.Point(838, 1)
        Me.txtEffectiveStartDate.MendatroryField = False
        Me.txtEffectiveStartDate.MinDate = New Date(1973, 1, 1, 0, 0, 0, 0)
        Me.txtEffectiveStartDate.MyLinkLable1 = Me.MyLabel11
        Me.txtEffectiveStartDate.MyLinkLable2 = Nothing
        Me.txtEffectiveStartDate.Name = "txtEffectiveStartDate"
        Me.txtEffectiveStartDate.NullText = "01/01/1973"
        Me.txtEffectiveStartDate.ReferenceFieldDesc = Nothing
        Me.txtEffectiveStartDate.ReferenceFieldName = Nothing
        Me.txtEffectiveStartDate.ReferenceTableName = Nothing
        Me.txtEffectiveStartDate.ShowCheckBox = True
        Me.txtEffectiveStartDate.Size = New System.Drawing.Size(103, 20)
        Me.txtEffectiveStartDate.TabIndex = 115
        Me.txtEffectiveStartDate.TabStop = False
        Me.txtEffectiveStartDate.Text = "12/06/2014"
        Me.txtEffectiveStartDate.Value = New Date(2014, 6, 12, 14, 13, 0, 222)
        Me.txtEffectiveStartDate.Visible = False
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel11.Location = New System.Drawing.Point(734, 3)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(104, 16)
        Me.MyLabel11.TabIndex = 116
        Me.MyLabel11.Text = "Effective Start Date"
        Me.MyLabel11.Visible = False
        '
        'chkTwoWay
        '
        Me.chkTwoWay.Location = New System.Drawing.Point(662, 2)
        Me.chkTwoWay.MyLinkLable1 = Nothing
        Me.chkTwoWay.MyLinkLable2 = Nothing
        Me.chkTwoWay.Name = "chkTwoWay"
        Me.chkTwoWay.Size = New System.Drawing.Size(66, 18)
        Me.chkTwoWay.TabIndex = 96
        Me.chkTwoWay.Tag1 = Nothing
        Me.chkTwoWay.Text = "Two Way"
        '
        'txtVehicleWeight
        '
        Me.txtVehicleWeight.BackColor = System.Drawing.Color.White
        Me.txtVehicleWeight.CalculationExpression = Nothing
        Me.txtVehicleWeight.DecimalPlaces = 3
        Me.txtVehicleWeight.FieldCode = Nothing
        Me.txtVehicleWeight.FieldDesc = Nothing
        Me.txtVehicleWeight.FieldMaxLength = 0
        Me.txtVehicleWeight.FieldName = Nothing
        Me.txtVehicleWeight.isCalculatedField = False
        Me.txtVehicleWeight.IsSourceFromTable = False
        Me.txtVehicleWeight.IsSourceFromValueList = False
        Me.txtVehicleWeight.IsUnique = False
        Me.txtVehicleWeight.Location = New System.Drawing.Point(585, 109)
        Me.txtVehicleWeight.MendatroryField = False
        Me.txtVehicleWeight.MyLinkLable1 = Me.MyLabel1
        Me.txtVehicleWeight.MyLinkLable2 = Nothing
        Me.txtVehicleWeight.Name = "txtVehicleWeight"
        Me.txtVehicleWeight.ReferenceFieldDesc = Nothing
        Me.txtVehicleWeight.ReferenceFieldName = Nothing
        Me.txtVehicleWeight.ReferenceTableName = Nothing
        Me.txtVehicleWeight.Size = New System.Drawing.Size(138, 20)
        Me.txtVehicleWeight.TabIndex = 73
        Me.txtVehicleWeight.Text = "0"
        Me.txtVehicleWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtVehicleWeight.Value = 0R
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(470, 113)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(109, 16)
        Me.MyLabel1.TabIndex = 74
        Me.MyLabel1.Text = "Vehicle Weight (KG)"
        '
        'fndcode
        '
        Me.fndcode.FieldName = Nothing
        Me.fndcode.Location = New System.Drawing.Point(141, 1)
        Me.fndcode.MendatroryField = True
        Me.fndcode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndcode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndcode.MyLinkLable1 = Me.lblvandorno
        Me.fndcode.MyLinkLable2 = Nothing
        Me.fndcode.MyMaxLength = 32767
        Me.fndcode.MyReadOnly = False
        Me.fndcode.Name = "fndcode"
        Me.fndcode.Size = New System.Drawing.Size(302, 21)
        Me.fndcode.TabIndex = 72
        Me.fndcode.TabStop = False
        Me.fndcode.Value = ""
        '
        'lblvandorno
        '
        Me.lblvandorno.FieldName = Nothing
        Me.lblvandorno.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblvandorno.Location = New System.Drawing.Point(0, 3)
        Me.lblvandorno.Name = "lblvandorno"
        Me.lblvandorno.Size = New System.Drawing.Size(64, 16)
        Me.lblvandorno.TabIndex = 12
        Me.lblvandorno.Text = "Vehicle No."
        '
        'FndRoute
        '
        Me.FndRoute.CalculationExpression = Nothing
        Me.FndRoute.FieldCode = Nothing
        Me.FndRoute.FieldDesc = Nothing
        Me.FndRoute.FieldMaxLength = 0
        Me.FndRoute.FieldName = Nothing
        Me.FndRoute.isCalculatedField = False
        Me.FndRoute.IsSourceFromTable = False
        Me.FndRoute.IsSourceFromValueList = False
        Me.FndRoute.IsUnique = False
        Me.FndRoute.Location = New System.Drawing.Point(143, 88)
        Me.FndRoute.MendatroryField = False
        Me.FndRoute.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndRoute.MyLinkLable1 = Me.MyLabel17
        Me.FndRoute.MyLinkLable2 = Me.LblRoute
        Me.FndRoute.MyReadOnly = True
        Me.FndRoute.MyShowMasterFormButton = True
        Me.FndRoute.Name = "FndRoute"
        Me.FndRoute.ReferenceFieldDesc = Nothing
        Me.FndRoute.ReferenceFieldName = Nothing
        Me.FndRoute.ReferenceTableName = Nothing
        Me.FndRoute.Size = New System.Drawing.Size(137, 18)
        Me.FndRoute.TabIndex = 69
        Me.FndRoute.Value = ""
        '
        'MyLabel17
        '
        Me.MyLabel17.FieldName = Nothing
        Me.MyLabel17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel17.Location = New System.Drawing.Point(0, 89)
        Me.MyLabel17.Name = "MyLabel17"
        Me.MyLabel17.Size = New System.Drawing.Size(67, 16)
        Me.MyLabel17.TabIndex = 70
        Me.MyLabel17.Text = "Route Code"
        '
        'LblRoute
        '
        Me.LblRoute.AutoSize = False
        Me.LblRoute.BorderVisible = True
        Me.LblRoute.FieldName = Nothing
        Me.LblRoute.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblRoute.Location = New System.Drawing.Point(285, 88)
        Me.LblRoute.Name = "LblRoute"
        Me.LblRoute.Size = New System.Drawing.Size(476, 18)
        Me.LblRoute.TabIndex = 71
        Me.LblRoute.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblRoute.TextWrap = False
        '
        'txtTankerNo
        '
        Me.txtTankerNo.AutoSize = False
        Me.txtTankerNo.CalculationExpression = Nothing
        Me.txtTankerNo.FieldCode = Nothing
        Me.txtTankerNo.FieldDesc = Nothing
        Me.txtTankerNo.FieldMaxLength = 0
        Me.txtTankerNo.FieldName = Nothing
        Me.txtTankerNo.isCalculatedField = False
        Me.txtTankerNo.IsSourceFromTable = False
        Me.txtTankerNo.IsSourceFromValueList = False
        Me.txtTankerNo.IsUnique = False
        Me.txtTankerNo.Location = New System.Drawing.Point(181, 1)
        Me.txtTankerNo.MaxLength = 150
        Me.txtTankerNo.MendatroryField = True
        Me.txtTankerNo.Multiline = True
        Me.txtTankerNo.MyLinkLable1 = Me.MyLabel6
        Me.txtTankerNo.MyLinkLable2 = Nothing
        Me.txtTankerNo.Name = "txtTankerNo"
        Me.txtTankerNo.ReferenceFieldDesc = Nothing
        Me.txtTankerNo.ReferenceFieldName = Nothing
        Me.txtTankerNo.ReferenceTableName = Nothing
        Me.txtTankerNo.Size = New System.Drawing.Size(202, 21)
        Me.txtTankerNo.TabIndex = 68
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(6, 16)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(77, 16)
        Me.MyLabel6.TabIndex = 64
        Me.MyLabel6.Text = "Rate Per K.M."
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.GroupBox5)
        Me.RadGroupBox1.Controls.Add(Me.chkIsAdditional)
        Me.RadGroupBox1.Controls.Add(Me.gv)
        Me.RadGroupBox1.Controls.Add(Me.GroupBox4)
        Me.RadGroupBox1.Controls.Add(Me.GroupBox3)
        Me.RadGroupBox1.Controls.Add(Me.GroupBox2)
        Me.RadGroupBox1.Controls.Add(Me.GroupBox1)
        Me.RadGroupBox1.Controls.Add(Me.rbtKmrange)
        Me.RadGroupBox1.HeaderText = "Basis of Freight Payments"
        Me.RadGroupBox1.Location = New System.Drawing.Point(3, 132)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(758, 323)
        Me.RadGroupBox1.TabIndex = 67
        Me.RadGroupBox1.Text = "Basis of Freight Payments"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.txtMRPDRent)
        Me.GroupBox5.Controls.Add(Me.MyLabel21)
        Me.GroupBox5.Controls.Add(Me.chkMonthlyRentPlusDiesel)
        Me.GroupBox5.Controls.Add(Me.MyLabel19)
        Me.GroupBox5.Controls.Add(Me.txtMRPDAverage)
        Me.GroupBox5.Controls.Add(Me.MyLabel20)
        Me.GroupBox5.Controls.Add(Me.txtMRPDDieselRate)
        Me.GroupBox5.Location = New System.Drawing.Point(9, 241)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(243, 80)
        Me.GroupBox5.TabIndex = 96
        Me.GroupBox5.TabStop = False
        '
        'txtMRPDRent
        '
        Me.txtMRPDRent.BackColor = System.Drawing.Color.White
        Me.txtMRPDRent.CalculationExpression = Nothing
        Me.txtMRPDRent.DecimalPlaces = 2
        Me.txtMRPDRent.Enabled = False
        Me.txtMRPDRent.FieldCode = Nothing
        Me.txtMRPDRent.FieldDesc = Nothing
        Me.txtMRPDRent.FieldMaxLength = 0
        Me.txtMRPDRent.FieldName = Nothing
        Me.txtMRPDRent.isCalculatedField = False
        Me.txtMRPDRent.IsSourceFromTable = False
        Me.txtMRPDRent.IsSourceFromValueList = False
        Me.txtMRPDRent.IsUnique = False
        Me.txtMRPDRent.Location = New System.Drawing.Point(119, 15)
        Me.txtMRPDRent.MendatroryField = False
        Me.txtMRPDRent.MyLinkLable1 = Nothing
        Me.txtMRPDRent.MyLinkLable2 = Nothing
        Me.txtMRPDRent.Name = "txtMRPDRent"
        Me.txtMRPDRent.ReferenceFieldDesc = Nothing
        Me.txtMRPDRent.ReferenceFieldName = Nothing
        Me.txtMRPDRent.ReferenceTableName = Nothing
        Me.txtMRPDRent.Size = New System.Drawing.Size(116, 20)
        Me.txtMRPDRent.TabIndex = 92
        Me.txtMRPDRent.Text = "0"
        Me.txtMRPDRent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMRPDRent.Value = 0R
        '
        'MyLabel21
        '
        Me.MyLabel21.FieldName = Nothing
        Me.MyLabel21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel21.Location = New System.Drawing.Point(6, 17)
        Me.MyLabel21.Name = "MyLabel21"
        Me.MyLabel21.Size = New System.Drawing.Size(86, 16)
        Me.MyLabel21.TabIndex = 91
        Me.MyLabel21.Text = "Rent Per Month"
        '
        'chkMonthlyRentPlusDiesel
        '
        Me.chkMonthlyRentPlusDiesel.Location = New System.Drawing.Point(4, -1)
        Me.chkMonthlyRentPlusDiesel.MyLinkLable1 = Nothing
        Me.chkMonthlyRentPlusDiesel.MyLinkLable2 = Nothing
        Me.chkMonthlyRentPlusDiesel.Name = "chkMonthlyRentPlusDiesel"
        Me.chkMonthlyRentPlusDiesel.Size = New System.Drawing.Size(123, 18)
        Me.chkMonthlyRentPlusDiesel.TabIndex = 0
        Me.chkMonthlyRentPlusDiesel.Tag1 = Nothing
        Me.chkMonthlyRentPlusDiesel.Text = "Rental Basis + Diesel"
        '
        'MyLabel19
        '
        Me.MyLabel19.FieldName = Nothing
        Me.MyLabel19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel19.Location = New System.Drawing.Point(6, 38)
        Me.MyLabel19.Name = "MyLabel19"
        Me.MyLabel19.Size = New System.Drawing.Size(111, 16)
        Me.MyLabel19.TabIndex = 70
        Me.MyLabel19.Text = "Average K.M per Ltr."
        '
        'txtMRPDAverage
        '
        Me.txtMRPDAverage.BackColor = System.Drawing.Color.White
        Me.txtMRPDAverage.CalculationExpression = Nothing
        Me.txtMRPDAverage.DecimalPlaces = 2
        Me.txtMRPDAverage.Enabled = False
        Me.txtMRPDAverage.FieldCode = Nothing
        Me.txtMRPDAverage.FieldDesc = Nothing
        Me.txtMRPDAverage.FieldMaxLength = 0
        Me.txtMRPDAverage.FieldName = Nothing
        Me.txtMRPDAverage.isCalculatedField = False
        Me.txtMRPDAverage.IsSourceFromTable = False
        Me.txtMRPDAverage.IsSourceFromValueList = False
        Me.txtMRPDAverage.IsUnique = False
        Me.txtMRPDAverage.Location = New System.Drawing.Point(119, 36)
        Me.txtMRPDAverage.MendatroryField = False
        Me.txtMRPDAverage.MyLinkLable1 = Me.MyLabel19
        Me.txtMRPDAverage.MyLinkLable2 = Nothing
        Me.txtMRPDAverage.Name = "txtMRPDAverage"
        Me.txtMRPDAverage.ReferenceFieldDesc = Nothing
        Me.txtMRPDAverage.ReferenceFieldName = Nothing
        Me.txtMRPDAverage.ReferenceTableName = Nothing
        Me.txtMRPDAverage.Size = New System.Drawing.Size(116, 20)
        Me.txtMRPDAverage.TabIndex = 2
        Me.txtMRPDAverage.Text = "0"
        Me.txtMRPDAverage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMRPDAverage.Value = 0R
        '
        'MyLabel20
        '
        Me.MyLabel20.FieldName = Nothing
        Me.MyLabel20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel20.Location = New System.Drawing.Point(6, 59)
        Me.MyLabel20.Name = "MyLabel20"
        Me.MyLabel20.Size = New System.Drawing.Size(77, 16)
        Me.MyLabel20.TabIndex = 72
        Me.MyLabel20.Text = "Rate of Diesel"
        '
        'txtMRPDDieselRate
        '
        Me.txtMRPDDieselRate.BackColor = System.Drawing.Color.White
        Me.txtMRPDDieselRate.CalculationExpression = Nothing
        Me.txtMRPDDieselRate.DecimalPlaces = 2
        Me.txtMRPDDieselRate.Enabled = False
        Me.txtMRPDDieselRate.FieldCode = Nothing
        Me.txtMRPDDieselRate.FieldDesc = Nothing
        Me.txtMRPDDieselRate.FieldMaxLength = 0
        Me.txtMRPDDieselRate.FieldName = Nothing
        Me.txtMRPDDieselRate.isCalculatedField = False
        Me.txtMRPDDieselRate.IsSourceFromTable = False
        Me.txtMRPDDieselRate.IsSourceFromValueList = False
        Me.txtMRPDDieselRate.IsUnique = False
        Me.txtMRPDDieselRate.Location = New System.Drawing.Point(119, 57)
        Me.txtMRPDDieselRate.MendatroryField = False
        Me.txtMRPDDieselRate.MyLinkLable1 = Me.MyLabel20
        Me.txtMRPDDieselRate.MyLinkLable2 = Nothing
        Me.txtMRPDDieselRate.Name = "txtMRPDDieselRate"
        Me.txtMRPDDieselRate.ReferenceFieldDesc = Nothing
        Me.txtMRPDDieselRate.ReferenceFieldName = Nothing
        Me.txtMRPDDieselRate.ReferenceTableName = Nothing
        Me.txtMRPDDieselRate.Size = New System.Drawing.Size(116, 20)
        Me.txtMRPDDieselRate.TabIndex = 3
        Me.txtMRPDDieselRate.Text = "0"
        Me.txtMRPDDieselRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMRPDDieselRate.Value = 0R
        '
        'chkIsAdditional
        '
        Me.chkIsAdditional.Location = New System.Drawing.Point(659, 23)
        Me.chkIsAdditional.MyLinkLable1 = Nothing
        Me.chkIsAdditional.MyLinkLable2 = Nothing
        Me.chkIsAdditional.Name = "chkIsAdditional"
        Me.chkIsAdditional.Size = New System.Drawing.Size(90, 18)
        Me.chkIsAdditional.TabIndex = 95
        Me.chkIsAdditional.Tag1 = Nothing
        Me.chkIsAdditional.Text = "On Additional"
        '
        'gv
        '
        Me.gv.Enabled = False
        Me.gv.Location = New System.Drawing.Point(258, 43)
        '
        'gv
        '
        Me.gv.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv.Name = "gv"
        Me.gv.ShowHeaderCellButtons = True
        Me.gv.Size = New System.Drawing.Size(495, 273)
        Me.gv.TabIndex = 94
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.rbtndiesel)
        Me.GroupBox4.Controls.Add(Me.MyLabel8)
        Me.GroupBox4.Controls.Add(Me.txtchrg)
        Me.GroupBox4.Controls.Add(Me.MyLabel9)
        Me.GroupBox4.Controls.Add(Me.txtavgkm)
        Me.GroupBox4.Controls.Add(Me.MyLabel10)
        Me.GroupBox4.Controls.Add(Me.txtdiesel)
        Me.GroupBox4.Location = New System.Drawing.Point(11, 16)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(242, 82)
        Me.GroupBox4.TabIndex = 93
        Me.GroupBox4.TabStop = False
        '
        'rbtndiesel
        '
        Me.rbtndiesel.Location = New System.Drawing.Point(4, -2)
        Me.rbtndiesel.MyLinkLable1 = Nothing
        Me.rbtndiesel.MyLinkLable2 = Nothing
        Me.rbtndiesel.Name = "rbtndiesel"
        Me.rbtndiesel.Size = New System.Drawing.Size(132, 18)
        Me.rbtndiesel.TabIndex = 0
        Me.rbtndiesel.Tag1 = Nothing
        Me.rbtndiesel.Text = "Rate per Shift + Diesel"
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(6, 20)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(94, 16)
        Me.MyLabel8.TabIndex = 66
        Me.MyLabel8.Text = "Charges per Shift"
        '
        'txtchrg
        '
        Me.txtchrg.BackColor = System.Drawing.Color.White
        Me.txtchrg.CalculationExpression = Nothing
        Me.txtchrg.DecimalPlaces = 2
        Me.txtchrg.Enabled = False
        Me.txtchrg.FieldCode = Nothing
        Me.txtchrg.FieldDesc = Nothing
        Me.txtchrg.FieldMaxLength = 0
        Me.txtchrg.FieldName = Nothing
        Me.txtchrg.isCalculatedField = False
        Me.txtchrg.IsSourceFromTable = False
        Me.txtchrg.IsSourceFromValueList = False
        Me.txtchrg.IsUnique = False
        Me.txtchrg.Location = New System.Drawing.Point(119, 18)
        Me.txtchrg.MendatroryField = False
        Me.txtchrg.MyLinkLable1 = Me.MyLabel8
        Me.txtchrg.MyLinkLable2 = Nothing
        Me.txtchrg.Name = "txtchrg"
        Me.txtchrg.ReferenceFieldDesc = Nothing
        Me.txtchrg.ReferenceFieldName = Nothing
        Me.txtchrg.ReferenceTableName = Nothing
        Me.txtchrg.Size = New System.Drawing.Size(116, 20)
        Me.txtchrg.TabIndex = 1
        Me.txtchrg.Text = "0"
        Me.txtchrg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtchrg.Value = 0R
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(6, 41)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(111, 16)
        Me.MyLabel9.TabIndex = 70
        Me.MyLabel9.Text = "Average K.M per Ltr."
        '
        'txtavgkm
        '
        Me.txtavgkm.BackColor = System.Drawing.Color.White
        Me.txtavgkm.CalculationExpression = Nothing
        Me.txtavgkm.DecimalPlaces = 2
        Me.txtavgkm.Enabled = False
        Me.txtavgkm.FieldCode = Nothing
        Me.txtavgkm.FieldDesc = Nothing
        Me.txtavgkm.FieldMaxLength = 0
        Me.txtavgkm.FieldName = Nothing
        Me.txtavgkm.isCalculatedField = False
        Me.txtavgkm.IsSourceFromTable = False
        Me.txtavgkm.IsSourceFromValueList = False
        Me.txtavgkm.IsUnique = False
        Me.txtavgkm.Location = New System.Drawing.Point(119, 39)
        Me.txtavgkm.MendatroryField = False
        Me.txtavgkm.MyLinkLable1 = Me.MyLabel9
        Me.txtavgkm.MyLinkLable2 = Nothing
        Me.txtavgkm.Name = "txtavgkm"
        Me.txtavgkm.ReferenceFieldDesc = Nothing
        Me.txtavgkm.ReferenceFieldName = Nothing
        Me.txtavgkm.ReferenceTableName = Nothing
        Me.txtavgkm.Size = New System.Drawing.Size(116, 20)
        Me.txtavgkm.TabIndex = 2
        Me.txtavgkm.Text = "0"
        Me.txtavgkm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtavgkm.Value = 0R
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(6, 62)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(77, 16)
        Me.MyLabel10.TabIndex = 72
        Me.MyLabel10.Text = "Rate of Diesel"
        '
        'txtdiesel
        '
        Me.txtdiesel.BackColor = System.Drawing.Color.White
        Me.txtdiesel.CalculationExpression = Nothing
        Me.txtdiesel.DecimalPlaces = 2
        Me.txtdiesel.Enabled = False
        Me.txtdiesel.FieldCode = Nothing
        Me.txtdiesel.FieldDesc = Nothing
        Me.txtdiesel.FieldMaxLength = 0
        Me.txtdiesel.FieldName = Nothing
        Me.txtdiesel.isCalculatedField = False
        Me.txtdiesel.IsSourceFromTable = False
        Me.txtdiesel.IsSourceFromValueList = False
        Me.txtdiesel.IsUnique = False
        Me.txtdiesel.Location = New System.Drawing.Point(119, 60)
        Me.txtdiesel.MendatroryField = False
        Me.txtdiesel.MyLinkLable1 = Me.MyLabel10
        Me.txtdiesel.MyLinkLable2 = Nothing
        Me.txtdiesel.Name = "txtdiesel"
        Me.txtdiesel.ReferenceFieldDesc = Nothing
        Me.txtdiesel.ReferenceFieldName = Nothing
        Me.txtdiesel.ReferenceTableName = Nothing
        Me.txtdiesel.Size = New System.Drawing.Size(116, 20)
        Me.txtdiesel.TabIndex = 3
        Me.txtdiesel.Text = "0"
        Me.txtdiesel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtdiesel.Value = 0R
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.cmbRentalType)
        Me.GroupBox3.Controls.Add(Me.rbtnrental)
        Me.GroupBox3.Controls.Add(Me.lblRentalType)
        Me.GroupBox3.Controls.Add(Me.txtRentalAmt)
        Me.GroupBox3.Controls.Add(Me.lblRentalAmount)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 100)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(241, 60)
        Me.GroupBox3.TabIndex = 92
        Me.GroupBox3.TabStop = False
        '
        'cmbRentalType
        '
        Me.cmbRentalType.AutoCompleteDisplayMember = Nothing
        Me.cmbRentalType.AutoCompleteValueMember = Nothing
        Me.cmbRentalType.CalculationExpression = Nothing
        Me.cmbRentalType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbRentalType.FieldCode = Nothing
        Me.cmbRentalType.FieldDesc = Nothing
        Me.cmbRentalType.FieldMaxLength = 0
        Me.cmbRentalType.FieldName = Nothing
        Me.cmbRentalType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbRentalType.isCalculatedField = False
        Me.cmbRentalType.IsSourceFromTable = False
        Me.cmbRentalType.IsSourceFromValueList = False
        Me.cmbRentalType.IsUnique = False
        RadListDataItem1.Text = "Day"
        RadListDataItem2.Text = "Month"
        RadListDataItem3.Text = "Year"
        Me.cmbRentalType.Items.Add(RadListDataItem1)
        Me.cmbRentalType.Items.Add(RadListDataItem2)
        Me.cmbRentalType.Items.Add(RadListDataItem3)
        Me.cmbRentalType.Location = New System.Drawing.Point(119, 17)
        Me.cmbRentalType.MendatroryField = True
        Me.cmbRentalType.MyLinkLable1 = Nothing
        Me.cmbRentalType.MyLinkLable2 = Nothing
        Me.cmbRentalType.Name = "cmbRentalType"
        Me.cmbRentalType.ReferenceFieldDesc = Nothing
        Me.cmbRentalType.ReferenceFieldName = Nothing
        Me.cmbRentalType.ReferenceTableName = Nothing
        Me.cmbRentalType.Size = New System.Drawing.Size(116, 18)
        Me.cmbRentalType.TabIndex = 85
        '
        'rbtnrental
        '
        Me.rbtnrental.Location = New System.Drawing.Point(4, -1)
        Me.rbtnrental.MyLinkLable1 = Nothing
        Me.rbtnrental.MyLinkLable2 = Nothing
        Me.rbtnrental.Name = "rbtnrental"
        Me.rbtnrental.Size = New System.Drawing.Size(79, 18)
        Me.rbtnrental.TabIndex = 4
        Me.rbtnrental.Tag1 = Nothing
        Me.rbtnrental.Text = "Rental Basis"
        '
        'lblRentalType
        '
        Me.lblRentalType.FieldName = Nothing
        Me.lblRentalType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRentalType.Location = New System.Drawing.Point(6, 18)
        Me.lblRentalType.Name = "lblRentalType"
        Me.lblRentalType.Size = New System.Drawing.Size(67, 16)
        Me.lblRentalType.TabIndex = 86
        Me.lblRentalType.Text = "Rental Type"
        '
        'txtRentalAmt
        '
        Me.txtRentalAmt.BackColor = System.Drawing.Color.White
        Me.txtRentalAmt.CalculationExpression = Nothing
        Me.txtRentalAmt.DecimalPlaces = 2
        Me.txtRentalAmt.Enabled = False
        Me.txtRentalAmt.FieldCode = Nothing
        Me.txtRentalAmt.FieldDesc = Nothing
        Me.txtRentalAmt.FieldMaxLength = 0
        Me.txtRentalAmt.FieldName = Nothing
        Me.txtRentalAmt.isCalculatedField = False
        Me.txtRentalAmt.IsSourceFromTable = False
        Me.txtRentalAmt.IsSourceFromValueList = False
        Me.txtRentalAmt.IsUnique = False
        Me.txtRentalAmt.Location = New System.Drawing.Point(119, 36)
        Me.txtRentalAmt.MendatroryField = False
        Me.txtRentalAmt.MyLinkLable1 = Nothing
        Me.txtRentalAmt.MyLinkLable2 = Nothing
        Me.txtRentalAmt.Name = "txtRentalAmt"
        Me.txtRentalAmt.ReferenceFieldDesc = Nothing
        Me.txtRentalAmt.ReferenceFieldName = Nothing
        Me.txtRentalAmt.ReferenceTableName = Nothing
        Me.txtRentalAmt.Size = New System.Drawing.Size(116, 20)
        Me.txtRentalAmt.TabIndex = 88
        Me.txtRentalAmt.Text = "0"
        Me.txtRentalAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtRentalAmt.Value = 0R
        '
        'lblRentalAmount
        '
        Me.lblRentalAmount.FieldName = Nothing
        Me.lblRentalAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRentalAmount.Location = New System.Drawing.Point(6, 38)
        Me.lblRentalAmount.Name = "lblRentalAmount"
        Me.lblRentalAmount.Size = New System.Drawing.Size(81, 16)
        Me.lblRentalAmount.TabIndex = 87
        Me.lblRentalAmount.Text = "Rental Amount"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txt_ltr)
        Me.GroupBox2.Controls.Add(Me.MyLabel7)
        Me.GroupBox2.Controls.Add(Me.cmbLtrKG)
        Me.GroupBox2.Controls.Add(Me.rbtnrateltr)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 162)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(240, 40)
        Me.GroupBox2.TabIndex = 91
        Me.GroupBox2.TabStop = False
        '
        'txt_ltr
        '
        Me.txt_ltr.BackColor = System.Drawing.Color.White
        Me.txt_ltr.CalculationExpression = Nothing
        Me.txt_ltr.DecimalPlaces = 2
        Me.txt_ltr.Enabled = False
        Me.txt_ltr.FieldCode = Nothing
        Me.txt_ltr.FieldDesc = Nothing
        Me.txt_ltr.FieldMaxLength = 0
        Me.txt_ltr.FieldName = Nothing
        Me.txt_ltr.isCalculatedField = False
        Me.txt_ltr.IsSourceFromTable = False
        Me.txt_ltr.IsSourceFromValueList = False
        Me.txt_ltr.IsUnique = False
        Me.txt_ltr.Location = New System.Drawing.Point(61, 15)
        Me.txt_ltr.MendatroryField = False
        Me.txt_ltr.MyLinkLable1 = Me.MyLabel7
        Me.txt_ltr.MyLinkLable2 = Nothing
        Me.txt_ltr.Name = "txt_ltr"
        Me.txt_ltr.ReferenceFieldDesc = Nothing
        Me.txt_ltr.ReferenceFieldName = Nothing
        Me.txt_ltr.ReferenceTableName = Nothing
        Me.txt_ltr.Size = New System.Drawing.Size(96, 20)
        Me.txt_ltr.TabIndex = 10
        Me.txt_ltr.Text = "0"
        Me.txt_ltr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txt_ltr.Value = 0R
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(6, 16)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(45, 16)
        Me.MyLabel7.TabIndex = 65
        Me.MyLabel7.Text = "Amount"
        '
        'cmbLtrKG
        '
        Me.cmbLtrKG.AutoCompleteDisplayMember = Nothing
        Me.cmbLtrKG.AutoCompleteValueMember = Nothing
        Me.cmbLtrKG.CalculationExpression = Nothing
        Me.cmbLtrKG.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbLtrKG.FieldCode = Nothing
        Me.cmbLtrKG.FieldDesc = Nothing
        Me.cmbLtrKG.FieldMaxLength = 0
        Me.cmbLtrKG.FieldName = Nothing
        Me.cmbLtrKG.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbLtrKG.isCalculatedField = False
        Me.cmbLtrKG.IsSourceFromTable = False
        Me.cmbLtrKG.IsSourceFromValueList = False
        Me.cmbLtrKG.IsUnique = False
        RadListDataItem4.Text = "LTR"
        RadListDataItem5.Text = "KG"
        Me.cmbLtrKG.Items.Add(RadListDataItem4)
        Me.cmbLtrKG.Items.Add(RadListDataItem5)
        Me.cmbLtrKG.Location = New System.Drawing.Point(158, 16)
        Me.cmbLtrKG.MendatroryField = True
        Me.cmbLtrKG.MyLinkLable1 = Nothing
        Me.cmbLtrKG.MyLinkLable2 = Nothing
        Me.cmbLtrKG.Name = "cmbLtrKG"
        Me.cmbLtrKG.ReferenceFieldDesc = Nothing
        Me.cmbLtrKG.ReferenceFieldName = Nothing
        Me.cmbLtrKG.ReferenceTableName = Nothing
        Me.cmbLtrKG.Size = New System.Drawing.Size(76, 18)
        Me.cmbLtrKG.TabIndex = 89
        '
        'rbtnrateltr
        '
        Me.rbtnrateltr.Location = New System.Drawing.Point(4, -2)
        Me.rbtnrateltr.MyLinkLable1 = Nothing
        Me.rbtnrateltr.MyLinkLable2 = Nothing
        Me.rbtnrateltr.Name = "rbtnrateltr"
        Me.rbtnrateltr.Size = New System.Drawing.Size(77, 18)
        Me.rbtnrateltr.TabIndex = 8
        Me.rbtnrateltr.Tag1 = Nothing
        Me.rbtnrateltr.Text = "Rate Ltr/KG"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txt_km)
        Me.GroupBox1.Controls.Add(Me.MyLabel6)
        Me.GroupBox1.Controls.Add(Me.rbtnratekm)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 204)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(241, 37)
        Me.GroupBox1.TabIndex = 90
        Me.GroupBox1.TabStop = False
        '
        'txt_km
        '
        Me.txt_km.BackColor = System.Drawing.Color.White
        Me.txt_km.CalculationExpression = Nothing
        Me.txt_km.DecimalPlaces = 2
        Me.txt_km.Enabled = False
        Me.txt_km.FieldCode = Nothing
        Me.txt_km.FieldDesc = Nothing
        Me.txt_km.FieldMaxLength = 0
        Me.txt_km.FieldName = Nothing
        Me.txt_km.isCalculatedField = False
        Me.txt_km.IsSourceFromTable = False
        Me.txt_km.IsSourceFromValueList = False
        Me.txt_km.IsUnique = False
        Me.txt_km.Location = New System.Drawing.Point(119, 13)
        Me.txt_km.MendatroryField = False
        Me.txt_km.MyLinkLable1 = Me.MyLabel6
        Me.txt_km.MyLinkLable2 = Nothing
        Me.txt_km.Name = "txt_km"
        Me.txt_km.ReferenceFieldDesc = Nothing
        Me.txt_km.ReferenceFieldName = Nothing
        Me.txt_km.ReferenceTableName = Nothing
        Me.txt_km.Size = New System.Drawing.Size(116, 20)
        Me.txt_km.TabIndex = 12
        Me.txt_km.Text = "0"
        Me.txt_km.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txt_km.Value = 0R
        '
        'rbtnratekm
        '
        Me.rbtnratekm.Location = New System.Drawing.Point(4, -2)
        Me.rbtnratekm.MyLinkLable1 = Nothing
        Me.rbtnratekm.MyLinkLable2 = Nothing
        Me.rbtnratekm.Name = "rbtnratekm"
        Me.rbtnratekm.Size = New System.Drawing.Size(87, 18)
        Me.rbtnratekm.TabIndex = 11
        Me.rbtnratekm.Tag1 = Nothing
        Me.rbtnratekm.Text = "Rate per K.M."
        '
        'rbtKmrange
        '
        Me.rbtKmrange.Location = New System.Drawing.Point(260, 23)
        Me.rbtKmrange.MyLinkLable1 = Nothing
        Me.rbtKmrange.MyLinkLable2 = Nothing
        Me.rbtKmrange.Name = "rbtKmrange"
        Me.rbtKmrange.Size = New System.Drawing.Size(137, 18)
        Me.rbtKmrange.TabIndex = 9
        Me.rbtKmrange.Tag1 = Nothing
        Me.rbtKmrange.Text = "Amount K.M. Slab Wise"
        '
        'txtmcccode
        '
        Me.txtmcccode.CalculationExpression = Nothing
        Me.txtmcccode.FieldCode = Nothing
        Me.txtmcccode.FieldDesc = Nothing
        Me.txtmcccode.FieldMaxLength = 0
        Me.txtmcccode.FieldName = Nothing
        Me.txtmcccode.isCalculatedField = False
        Me.txtmcccode.IsSourceFromTable = False
        Me.txtmcccode.IsSourceFromValueList = False
        Me.txtmcccode.IsUnique = False
        Me.txtmcccode.Location = New System.Drawing.Point(143, 67)
        Me.txtmcccode.MendatroryField = True
        Me.txtmcccode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtmcccode.MyLinkLable1 = Me.MyLabel3
        Me.txtmcccode.MyLinkLable2 = Me.txtmccname
        Me.txtmcccode.MyReadOnly = False
        Me.txtmcccode.MyShowMasterFormButton = True
        Me.txtmcccode.Name = "txtmcccode"
        Me.txtmcccode.ReferenceFieldDesc = Nothing
        Me.txtmcccode.ReferenceFieldName = Nothing
        Me.txtmcccode.ReferenceTableName = Nothing
        Me.txtmcccode.Size = New System.Drawing.Size(137, 18)
        Me.txtmcccode.TabIndex = 3
        Me.txtmcccode.Value = ""
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(0, 68)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(62, 16)
        Me.MyLabel3.TabIndex = 62
        Me.MyLabel3.Text = "MCC Code"
        '
        'txtmccname
        '
        Me.txtmccname.AutoSize = False
        Me.txtmccname.BorderVisible = True
        Me.txtmccname.FieldName = Nothing
        Me.txtmccname.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtmccname.Location = New System.Drawing.Point(285, 67)
        Me.txtmccname.Name = "txtmccname"
        Me.txtmccname.Size = New System.Drawing.Size(476, 18)
        Me.txtmccname.TabIndex = 63
        Me.txtmccname.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtmccname.TextWrap = False
        '
        'txtyear
        '
        Me.txtyear.CalculationExpression = Nothing
        Me.txtyear.CustomFormat = "yyyy"
        Me.txtyear.FieldCode = Nothing
        Me.txtyear.FieldDesc = Nothing
        Me.txtyear.FieldMaxLength = 0
        Me.txtyear.FieldName = Nothing
        Me.txtyear.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtyear.isCalculatedField = False
        Me.txtyear.IsSourceFromTable = False
        Me.txtyear.IsSourceFromValueList = False
        Me.txtyear.IsUnique = False
        Me.txtyear.Location = New System.Drawing.Point(411, 109)
        Me.txtyear.MendatroryField = True
        Me.txtyear.MinDate = New Date(1973, 1, 1, 0, 0, 0, 0)
        Me.txtyear.MyLinkLable1 = Me.MyLabel5
        Me.txtyear.MyLinkLable2 = Nothing
        Me.txtyear.Name = "txtyear"
        Me.txtyear.NullText = "01/01/1973"
        Me.txtyear.ReferenceFieldDesc = Nothing
        Me.txtyear.ReferenceFieldName = Nothing
        Me.txtyear.ReferenceTableName = Nothing
        Me.txtyear.Size = New System.Drawing.Size(53, 20)
        Me.txtyear.TabIndex = 5
        Me.txtyear.TabStop = False
        Me.txtyear.Text = "2014"
        Me.txtyear.Value = New Date(2014, 6, 11, 14, 13, 57, 495)
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(285, 113)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(120, 16)
        Me.MyLabel5.TabIndex = 59
        Me.MyLabel5.Text = "Year Of Manufacturing"
        '
        'txtcapcity
        '
        Me.txtcapcity.BackColor = System.Drawing.Color.White
        Me.txtcapcity.CalculationExpression = Nothing
        Me.txtcapcity.DecimalPlaces = 2
        Me.txtcapcity.FieldCode = Nothing
        Me.txtcapcity.FieldDesc = Nothing
        Me.txtcapcity.FieldMaxLength = 0
        Me.txtcapcity.FieldName = Nothing
        Me.txtcapcity.isCalculatedField = False
        Me.txtcapcity.IsSourceFromTable = False
        Me.txtcapcity.IsSourceFromValueList = False
        Me.txtcapcity.IsUnique = False
        Me.txtcapcity.Location = New System.Drawing.Point(143, 109)
        Me.txtcapcity.MendatroryField = False
        Me.txtcapcity.MyLinkLable1 = Me.MyLabel2
        Me.txtcapcity.MyLinkLable2 = Nothing
        Me.txtcapcity.Name = "txtcapcity"
        Me.txtcapcity.ReferenceFieldDesc = Nothing
        Me.txtcapcity.ReferenceFieldName = Nothing
        Me.txtcapcity.ReferenceTableName = Nothing
        Me.txtcapcity.Size = New System.Drawing.Size(138, 20)
        Me.txtcapcity.TabIndex = 4
        Me.txtcapcity.Text = "0"
        Me.txtcapcity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtcapcity.Value = 0R
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(0, 111)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(113, 16)
        Me.MyLabel2.TabIndex = 57
        Me.MyLabel2.Text = "Storage Capacity(mt)"
        '
        'txtprimarycode
        '
        Me.txtprimarycode.CalculationExpression = Nothing
        Me.txtprimarycode.FieldCode = Nothing
        Me.txtprimarycode.FieldDesc = Nothing
        Me.txtprimarycode.FieldMaxLength = 0
        Me.txtprimarycode.FieldName = Nothing
        Me.txtprimarycode.isCalculatedField = False
        Me.txtprimarycode.IsSourceFromTable = False
        Me.txtprimarycode.IsSourceFromValueList = False
        Me.txtprimarycode.IsUnique = False
        Me.txtprimarycode.Location = New System.Drawing.Point(143, 46)
        Me.txtprimarycode.MendatroryField = True
        Me.txtprimarycode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtprimarycode.MyLinkLable1 = Me.MyLabel4
        Me.txtprimarycode.MyLinkLable2 = Me.txtprimaryname
        Me.txtprimarycode.MyReadOnly = False
        Me.txtprimarycode.MyShowMasterFormButton = True
        Me.txtprimarycode.Name = "txtprimarycode"
        Me.txtprimarycode.ReferenceFieldDesc = Nothing
        Me.txtprimarycode.ReferenceFieldName = Nothing
        Me.txtprimarycode.ReferenceTableName = Nothing
        Me.txtprimarycode.Size = New System.Drawing.Size(137, 18)
        Me.txtprimarycode.TabIndex = 2
        Me.txtprimarycode.Value = ""
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(0, 47)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(137, 16)
        Me.MyLabel4.TabIndex = 51
        Me.MyLabel4.Text = "Primary Transporter Code"
        '
        'txtprimaryname
        '
        Me.txtprimaryname.AutoSize = False
        Me.txtprimaryname.BorderVisible = True
        Me.txtprimaryname.FieldName = Nothing
        Me.txtprimaryname.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtprimaryname.Location = New System.Drawing.Point(285, 46)
        Me.txtprimaryname.Name = "txtprimaryname"
        Me.txtprimaryname.Size = New System.Drawing.Size(476, 18)
        Me.txtprimaryname.TabIndex = 52
        Me.txtprimaryname.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtprimaryname.TextWrap = False
        '
        'txtdesc
        '
        Me.txtdesc.CalculationExpression = Nothing
        Me.txtdesc.FieldCode = Nothing
        Me.txtdesc.FieldDesc = Nothing
        Me.txtdesc.FieldMaxLength = 0
        Me.txtdesc.FieldName = Nothing
        Me.txtdesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdesc.isCalculatedField = False
        Me.txtdesc.IsSourceFromTable = False
        Me.txtdesc.IsSourceFromValueList = False
        Me.txtdesc.IsUnique = False
        Me.txtdesc.Location = New System.Drawing.Point(143, 25)
        Me.txtdesc.MaxLength = 150
        Me.txtdesc.MendatroryField = True
        Me.txtdesc.MyLinkLable1 = Me.lblvendorname
        Me.txtdesc.MyLinkLable2 = Nothing
        Me.txtdesc.Name = "txtdesc"
        Me.txtdesc.ReferenceFieldDesc = Nothing
        Me.txtdesc.ReferenceFieldName = Nothing
        Me.txtdesc.ReferenceTableName = Nothing
        Me.txtdesc.Size = New System.Drawing.Size(618, 18)
        Me.txtdesc.TabIndex = 1
        '
        'lblvendorname
        '
        Me.lblvendorname.FieldName = Nothing
        Me.lblvendorname.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblvendorname.Location = New System.Drawing.Point(0, 26)
        Me.lblvendorname.Name = "lblvendorname"
        Me.lblvendorname.Size = New System.Drawing.Size(34, 16)
        Me.lblvendorname.TabIndex = 14
        Me.lblvendorname.Text = "Make"
        '
        'btnnew
        '
        Me.btnnew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnnew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnnew.Location = New System.Drawing.Point(443, 1)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(20, 21)
        Me.btnnew.TabIndex = 0
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.UcAttachment1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(75.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(768, 454)
        Me.RadPageViewPage2.Text = "Attachment"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(768, 454)
        Me.UcAttachment1.TabIndex = 3
        '
        'pageVehicleFitness
        '
        Me.pageVehicleFitness.Controls.Add(Me.RadGroupBox2)
        Me.pageVehicleFitness.ItemSize = New System.Drawing.SizeF(151.0!, 28.0!)
        Me.pageVehicleFitness.Location = New System.Drawing.Point(10, 37)
        Me.pageVehicleFitness.Name = "pageVehicleFitness"
        Me.pageVehicleFitness.Size = New System.Drawing.Size(768, 454)
        Me.pageVehicleFitness.Text = "Vehicle Fitness Information"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.gbVehFitNo)
        Me.RadGroupBox2.Controls.Add(Me.gbxVehInsInfo)
        Me.RadGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox2.HeaderText = "Vehicle's Other Informations"
        Me.RadGroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Size = New System.Drawing.Size(768, 454)
        Me.RadGroupBox2.TabIndex = 0
        Me.RadGroupBox2.Text = "Vehicle's Other Informations"
        '
        'gbVehFitNo
        '
        Me.gbVehFitNo.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbVehFitNo.Controls.Add(Me.lblVehInsuranceNo)
        Me.gbVehFitNo.Controls.Add(Me.lblVehicleInsuranceDate)
        Me.gbVehFitNo.Controls.Add(Me.dtpInsurance)
        Me.gbVehFitNo.Controls.Add(Me.txtVehInsuranceNo)
        Me.gbVehFitNo.HeaderText = "Vehicle Insurance Details"
        Me.gbVehFitNo.Location = New System.Drawing.Point(14, 33)
        Me.gbVehFitNo.Name = "gbVehFitNo"
        Me.gbVehFitNo.Size = New System.Drawing.Size(389, 102)
        Me.gbVehFitNo.TabIndex = 13
        Me.gbVehFitNo.Text = "Vehicle Insurance Details"
        '
        'lblVehInsuranceNo
        '
        Me.lblVehInsuranceNo.Location = New System.Drawing.Point(15, 34)
        Me.lblVehInsuranceNo.Name = "lblVehInsuranceNo"
        Me.lblVehInsuranceNo.Size = New System.Drawing.Size(114, 18)
        Me.lblVehInsuranceNo.TabIndex = 8
        Me.lblVehInsuranceNo.Text = "Vehicle Insurance No."
        '
        'lblVehicleInsuranceDate
        '
        Me.lblVehicleInsuranceDate.Location = New System.Drawing.Point(15, 58)
        Me.lblVehicleInsuranceDate.Name = "lblVehicleInsuranceDate"
        Me.lblVehicleInsuranceDate.Size = New System.Drawing.Size(114, 18)
        Me.lblVehicleInsuranceDate.TabIndex = 9
        Me.lblVehicleInsuranceDate.Text = "Insurance Expiry Date"
        '
        'dtpInsurance
        '
        Me.dtpInsurance.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpInsurance.Location = New System.Drawing.Point(171, 56)
        Me.dtpInsurance.Name = "dtpInsurance"
        Me.dtpInsurance.Size = New System.Drawing.Size(176, 20)
        Me.dtpInsurance.TabIndex = 11
        Me.dtpInsurance.TabStop = False
        Me.dtpInsurance.Text = "24/01/2017"
        Me.dtpInsurance.Value = New Date(2017, 1, 24, 16, 38, 28, 570)
        '
        'txtVehInsuranceNo
        '
        Me.txtVehInsuranceNo.Location = New System.Drawing.Point(171, 32)
        Me.txtVehInsuranceNo.MaxLength = 30
        Me.txtVehInsuranceNo.Name = "txtVehInsuranceNo"
        Me.txtVehInsuranceNo.Size = New System.Drawing.Size(176, 20)
        Me.txtVehInsuranceNo.TabIndex = 10
        '
        'gbxVehInsInfo
        '
        Me.gbxVehInsInfo.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbxVehInsInfo.Controls.Add(Me.lblVehFitNo)
        Me.gbxVehInsInfo.Controls.Add(Me.lblVehFitnessDate)
        Me.gbxVehInsInfo.Controls.Add(Me.txtVehFitnessNo)
        Me.gbxVehInsInfo.Controls.Add(Me.dtpVehFitness)
        Me.gbxVehInsInfo.HeaderText = "Vehicle Fitness Details"
        Me.gbxVehInsInfo.Location = New System.Drawing.Point(14, 143)
        Me.gbxVehInsInfo.Name = "gbxVehInsInfo"
        Me.gbxVehInsInfo.Size = New System.Drawing.Size(389, 102)
        Me.gbxVehInsInfo.TabIndex = 12
        Me.gbxVehInsInfo.Text = "Vehicle Fitness Details"
        '
        'lblVehFitNo
        '
        Me.lblVehFitNo.Location = New System.Drawing.Point(15, 32)
        Me.lblVehFitNo.Name = "lblVehFitNo"
        Me.lblVehFitNo.Size = New System.Drawing.Size(100, 18)
        Me.lblVehFitNo.TabIndex = 4
        Me.lblVehFitNo.Text = "Vehicle Fitness No."
        '
        'lblVehFitnessDate
        '
        Me.lblVehFitnessDate.Location = New System.Drawing.Point(15, 56)
        Me.lblVehFitnessDate.Name = "lblVehFitnessDate"
        Me.lblVehFitnessDate.Size = New System.Drawing.Size(106, 18)
        Me.lblVehFitnessDate.TabIndex = 5
        Me.lblVehFitnessDate.Text = "Vehicle Fitness Date"
        '
        'txtVehFitnessNo
        '
        Me.txtVehFitnessNo.Location = New System.Drawing.Point(171, 30)
        Me.txtVehFitnessNo.MaxLength = 30
        Me.txtVehFitnessNo.Name = "txtVehFitnessNo"
        Me.txtVehFitnessNo.Size = New System.Drawing.Size(176, 20)
        Me.txtVehFitnessNo.TabIndex = 6
        '
        'dtpVehFitness
        '
        Me.dtpVehFitness.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpVehFitness.Location = New System.Drawing.Point(171, 54)
        Me.dtpVehFitness.Name = "dtpVehFitness"
        Me.dtpVehFitness.Size = New System.Drawing.Size(176, 20)
        Me.dtpVehFitness.TabIndex = 7
        Me.dtpVehFitness.TabStop = False
        Me.dtpVehFitness.Text = "24/01/2017"
        Me.dtpVehFitness.Value = New Date(2017, 1, 24, 16, 38, 28, 570)
        '
        'btnHistory
        '
        Me.btnHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnHistory.Location = New System.Drawing.Point(157, 8)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Size = New System.Drawing.Size(73, 20)
        Me.btnHistory.TabIndex = 3
        Me.btnHistory.Text = "&History"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(913, 7)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(73, 20)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(78, 7)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(73, 20)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(5, 7)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(73, 20)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'FrmPrimaryTransporterVehicalMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(996, 580)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmPrimaryTransporterVehicalMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmPrimaryTransporterVehicalMaster"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.txtVehicle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVehicle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEffectiveStartDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkTwoWay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVehicleWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblvandorno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblRoute, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTankerNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        CType(Me.txtMRPDRent, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMonthlyRentPlusDiesel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMRPDAverage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMRPDDieselRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIsAdditional, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        CType(Me.rbtndiesel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtchrg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtavgkm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdiesel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.cmbRentalType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnrental, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRentalType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRentalAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRentalAmount, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.txt_ltr, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbLtrKG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnrateltr, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.txt_km, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnratekm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtKmrange, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtmccname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtyear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcapcity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtprimaryname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblvendorname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.pageVehicleFitness.ResumeLayout(False)
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gbVehFitNo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbVehFitNo.ResumeLayout(False)
        Me.gbVehFitNo.PerformLayout()
        CType(Me.lblVehInsuranceNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVehicleInsuranceDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpInsurance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVehInsuranceNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gbxVehInsInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbxVehInsInfo.ResumeLayout(False)
        Me.gbxVehInsInfo.PerformLayout()
        CType(Me.lblVehFitNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVehFitnessDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVehFitnessNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpVehFitness, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents MenuClose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents lblvandorno As common.Controls.MyLabel
    Friend WithEvents txtdesc As common.Controls.MyTextBox
    Friend WithEvents lblvendorname As common.Controls.MyLabel
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtprimarycode As common.UserControls.txtFinder
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtprimaryname As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtcapcity As common.MyNumBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtyear As common.Controls.MyDateTimePicker
    Friend WithEvents txtmcccode As common.UserControls.txtFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtmccname As common.Controls.MyLabel
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents txtchrg As common.MyNumBox
    Friend WithEvents txt_ltr As common.MyNumBox
    Friend WithEvents txt_km As common.MyNumBox
    Friend WithEvents btnexport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnimport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtdiesel As common.MyNumBox
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents txtavgkm As common.MyNumBox
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents rbtnratekm As common.Controls.MyCheckBox
    Friend WithEvents rbtnrateltr As common.Controls.MyCheckBox
    Friend WithEvents rbtnrental As common.Controls.MyCheckBox
    Friend WithEvents rbtndiesel As common.Controls.MyCheckBox
    Friend WithEvents rbtKmrange As common.Controls.MyCheckBox
    Friend WithEvents txtTankerNo As common.Controls.MyTextBox
    Friend WithEvents txtRentalAmt As common.MyNumBox
    Friend WithEvents lblRentalAmount As common.Controls.MyLabel
    Friend WithEvents lblRentalType As common.Controls.MyLabel
    Friend WithEvents cmbRentalType As common.Controls.MyComboBox
    Friend WithEvents cmbLtrKG As common.Controls.MyComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents FndRoute As common.UserControls.txtFinder
    Friend WithEvents MyLabel17 As common.Controls.MyLabel
    Friend WithEvents LblRoute As common.Controls.MyLabel
    Friend WithEvents chkIsAdditional As common.Controls.MyCheckBox
    Friend WithEvents Export_Vehical_Details As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Export_Slab_Details As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Import_Vehical_Details As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Import_Slab_Details As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents fndcode As common.UserControls.txtNavigator
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents txtMRPDRent As common.MyNumBox
    Friend WithEvents MyLabel21 As common.Controls.MyLabel
    Friend WithEvents chkMonthlyRentPlusDiesel As common.Controls.MyCheckBox
    Friend WithEvents MyLabel19 As common.Controls.MyLabel
    Friend WithEvents txtMRPDAverage As common.MyNumBox
    Friend WithEvents MyLabel20 As common.Controls.MyLabel
    Friend WithEvents txtMRPDDieselRate As common.MyNumBox
    Friend WithEvents txtVehicleWeight As common.MyNumBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents pageVehicleFitness As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gbVehFitNo As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents lblVehInsuranceNo As Telerik.WinControls.UI.RadLabel
    Friend WithEvents lblVehicleInsuranceDate As Telerik.WinControls.UI.RadLabel
    Friend WithEvents dtpInsurance As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents txtVehInsuranceNo As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents gbxVehInsInfo As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents lblVehFitNo As Telerik.WinControls.UI.RadLabel
    Friend WithEvents lblVehFitnessDate As Telerik.WinControls.UI.RadLabel
    Friend WithEvents txtVehFitnessNo As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents dtpVehFitness As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents btnHistory As Telerik.WinControls.UI.RadButton
    Friend WithEvents chkTwoWay As common.Controls.MyCheckBox
    Friend WithEvents txtEffectiveStartDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents lblVehicle As common.Controls.MyLabel
    Friend WithEvents txtVehicle As common.Controls.MyTextBox
End Class

