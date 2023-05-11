Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVehicleMaster
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
        Me.components = New System.ComponentModel.Container()
        Dim RadListDataItem6 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem7 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem8 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.rlblVehicleID = New common.Controls.MyLabel()
        Me.rlblModel = New common.Controls.MyLabel()
        Me.rlbltype = New common.Controls.MyLabel()
        Me.rlblNumber = New common.Controls.MyLabel()
        Me.rlblDescription = New common.Controls.MyLabel()
        Me.rtxtModel = New common.Controls.MyTextBox()
        Me.rtxtNumber = New common.Controls.MyTextBox()
        Me.rtxtDescription = New common.Controls.MyTextBox()
        Me.rbtndepot = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtnHire = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtnSave = New Telerik.WinControls.UI.RadButton()
        Me.rbtnDelete = New Telerik.WinControls.UI.RadButton()
        Me.rbtnClose = New Telerik.WinControls.UI.RadButton()
        Me.rbtnReset = New Telerik.WinControls.UI.RadButton()
        Me.rlblVehicleRegistrationNo = New common.Controls.MyLabel()
        Me.rtxtVehicle_registration_No = New common.Controls.MyTextBox()
        Me.rlblVehicleChechisNo = New common.Controls.MyLabel()
        Me.rtxtvehicle_Chechis_No = New common.Controls.MyTextBox()
        Me.ToolTip_Vehicle_Master = New System.Windows.Forms.ToolTip(Me.components)
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem_Import = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem_Export = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem_Close = New Telerik.WinControls.UI.RadMenuItem()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtVehicleWeight = New common.MyNumBox()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.pnlmt = New System.Windows.Forms.Panel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtmtvalue = New common.MyNumBox()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtmtcapacity = New common.MyNumBox()
        Me.fndemployee = New common.UserControls.txtFinder()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtSequenceNo = New common.MyNumBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.TxtCrateCapacity = New common.MyNumBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.rtxtCapacity = New common.MyNumBox()
        Me.lblCapacity = New common.Controls.MyLabel()
        Me.lblRoadTax_valid_till = New common.Controls.MyLabel()
        Me.lblRoadtax_valid_from = New common.Controls.MyLabel()
        Me.lblPollutonchk_valid_till = New common.Controls.MyLabel()
        Me.lblPollutionchk_valid_frm = New common.Controls.MyLabel()
        Me.lblFitness_valid_till = New common.Controls.MyLabel()
        Me.lblFitness_valid_from = New common.Controls.MyLabel()
        Me.lblInsurance_valid_till = New common.Controls.MyLabel()
        Me.lblInsurance_valid_from = New common.Controls.MyLabel()
        Me.txtRoad_tax_valid_till = New common.Controls.MyDateTimePicker()
        Me.txtPollution_valid_till = New common.Controls.MyDateTimePicker()
        Me.txtFitness_valid_till = New common.Controls.MyDateTimePicker()
        Me.txtInsurance_valid_till = New common.Controls.MyDateTimePicker()
        Me.txtRoad_tax_valid_from = New common.Controls.MyDateTimePicker()
        Me.txtPollution_valid_from = New common.Controls.MyDateTimePicker()
        Me.txtFitness_valid_from = New common.Controls.MyDateTimePicker()
        Me.txtInsurance_valid_from = New common.Controls.MyDateTimePicker()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.lblVehicleName = New common.Controls.MyLabel()
        Me.llVehicleNo = New common.Controls.MyLabel()
        Me.lblEngineNo = New common.Controls.MyLabel()
        Me.LblVehicleBrand = New common.Controls.MyLabel()
        Me.lblRegsteredOn = New common.Controls.MyLabel()
        Me.rtxtlocation = New common.Controls.MyTextBox()
        Me.rtxtvehicleNo = New common.Controls.MyTextBox()
        Me.rtxtvehicleName = New common.Controls.MyTextBox()
        Me.rtxtengineno = New common.Controls.MyTextBox()
        Me.rtxtRegistredOn = New common.Controls.MyTextBox()
        Me.rtxtvehiclebrand = New common.Controls.MyTextBox()
        Me.fndTransporter = New common.UserControls.txtFinder()
        Me.lblTransporter = New common.Controls.MyLabel()
        Me.fndVehicle_id = New common.UserControls.txtNavigator()
        Me.rtxtTranType = New common.Controls.MyTextBox()
        Me.lblTranType = New common.Controls.MyLabel()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
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
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.rbtnratekm = New common.Controls.MyCheckBox()
        Me.rbtKmrange = New common.Controls.MyCheckBox()
        Me.btnHistory = New Telerik.WinControls.UI.RadButton()
        CType(Me.rlblVehicleID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rlblModel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rlbltype, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rlblNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rlblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtxtModel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtxtNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtxtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtndepot, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnHire, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rlblVehicleRegistrationNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtxtVehicle_registration_No, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rlblVehicleChechisNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtxtvehicle_Chechis_No, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.txtVehicleWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlmt.SuspendLayout()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtmtvalue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtmtcapacity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSequenceNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtCrateCapacity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtxtCapacity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCapacity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRoadTax_valid_till, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRoadtax_valid_from, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPollutonchk_valid_till, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPollutionchk_valid_frm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFitness_valid_till, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFitness_valid_from, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblInsurance_valid_till, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblInsurance_valid_from, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRoad_tax_valid_till, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPollution_valid_till, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFitness_valid_till, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtInsurance_valid_till, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRoad_tax_valid_from, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPollution_valid_from, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFitness_valid_from, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtInsurance_valid_from, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVehicleName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.llVehicleNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEngineNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblVehicleBrand, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRegsteredOn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtxtlocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtxtvehicleNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtxtvehicleName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtxtengineno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtxtRegistredOn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtxtvehiclebrand, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTransporter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtxtTranType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTranType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
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
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnratekm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtKmrange, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'rlblVehicleID
        '
        Me.rlblVehicleID.FieldName = Nothing
        Me.rlblVehicleID.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.rlblVehicleID.Location = New System.Drawing.Point(13, 20)
        Me.rlblVehicleID.Name = "rlblVehicleID"
        Me.rlblVehicleID.Size = New System.Drawing.Size(77, 16)
        Me.rlblVehicleID.TabIndex = 0
        Me.rlblVehicleID.Text = "Vehicle Code"
        '
        'rlblModel
        '
        Me.rlblModel.FieldName = Nothing
        Me.rlblModel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rlblModel.Location = New System.Drawing.Point(376, 78)
        Me.rlblModel.Name = "rlblModel"
        Me.rlblModel.Size = New System.Drawing.Size(37, 16)
        Me.rlblModel.TabIndex = 16
        Me.rlblModel.Text = "Model"
        '
        'rlbltype
        '
        Me.rlbltype.FieldName = Nothing
        Me.rlbltype.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rlbltype.Location = New System.Drawing.Point(376, 40)
        Me.rlbltype.Name = "rlbltype"
        Me.rlbltype.Size = New System.Drawing.Size(72, 16)
        Me.rlbltype.TabIndex = 7
        Me.rlbltype.Text = "Vehicle Type"
        '
        'rlblNumber
        '
        Me.rlblNumber.FieldName = Nothing
        Me.rlblNumber.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rlblNumber.Location = New System.Drawing.Point(13, 40)
        Me.rlblNumber.Name = "rlblNumber"
        Me.rlblNumber.Size = New System.Drawing.Size(46, 16)
        Me.rlblNumber.TabIndex = 5
        Me.rlblNumber.Text = "Number"
        '
        'rlblDescription
        '
        Me.rlblDescription.FieldName = Nothing
        Me.rlblDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rlblDescription.Location = New System.Drawing.Point(376, 21)
        Me.rlblDescription.Name = "rlblDescription"
        Me.rlblDescription.Size = New System.Drawing.Size(63, 16)
        Me.rlblDescription.TabIndex = 3
        Me.rlblDescription.Text = "Description"
        '
        'rtxtModel
        '
        Me.rtxtModel.CalculationExpression = Nothing
        Me.rtxtModel.FieldCode = Nothing
        Me.rtxtModel.FieldDesc = Nothing
        Me.rtxtModel.FieldMaxLength = 0
        Me.rtxtModel.FieldName = Nothing
        Me.rtxtModel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtxtModel.isCalculatedField = False
        Me.rtxtModel.IsSourceFromTable = False
        Me.rtxtModel.IsSourceFromValueList = False
        Me.rtxtModel.IsUnique = False
        Me.rtxtModel.Location = New System.Drawing.Point(517, 76)
        Me.rtxtModel.MaxLength = 22
        Me.rtxtModel.MendatroryField = True
        Me.rtxtModel.MyLinkLable1 = Me.rlblModel
        Me.rtxtModel.MyLinkLable2 = Nothing
        Me.rtxtModel.Name = "rtxtModel"
        Me.rtxtModel.ReferenceFieldDesc = Nothing
        Me.rtxtModel.ReferenceFieldName = Nothing
        Me.rtxtModel.ReferenceTableName = Nothing
        Me.rtxtModel.Size = New System.Drawing.Size(200, 18)
        Me.rtxtModel.TabIndex = 9
        '
        'rtxtNumber
        '
        Me.rtxtNumber.CalculationExpression = Nothing
        Me.rtxtNumber.FieldCode = Nothing
        Me.rtxtNumber.FieldDesc = Nothing
        Me.rtxtNumber.FieldMaxLength = 0
        Me.rtxtNumber.FieldName = Nothing
        Me.rtxtNumber.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtxtNumber.isCalculatedField = False
        Me.rtxtNumber.IsSourceFromTable = False
        Me.rtxtNumber.IsSourceFromValueList = False
        Me.rtxtNumber.IsUnique = False
        Me.rtxtNumber.Location = New System.Drawing.Point(159, 39)
        Me.rtxtNumber.MaxLength = 11
        Me.rtxtNumber.MendatroryField = False
        Me.rtxtNumber.MyLinkLable1 = Me.rlblNumber
        Me.rtxtNumber.MyLinkLable2 = Nothing
        Me.rtxtNumber.Name = "rtxtNumber"
        Me.rtxtNumber.ReferenceFieldDesc = Nothing
        Me.rtxtNumber.ReferenceFieldName = Nothing
        Me.rtxtNumber.ReferenceTableName = Nothing
        Me.rtxtNumber.Size = New System.Drawing.Size(200, 18)
        Me.rtxtNumber.TabIndex = 3
        '
        'rtxtDescription
        '
        Me.rtxtDescription.AutoSize = False
        Me.rtxtDescription.CalculationExpression = Nothing
        Me.rtxtDescription.FieldCode = Nothing
        Me.rtxtDescription.FieldDesc = Nothing
        Me.rtxtDescription.FieldMaxLength = 0
        Me.rtxtDescription.FieldName = Nothing
        Me.rtxtDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtxtDescription.isCalculatedField = False
        Me.rtxtDescription.IsSourceFromTable = False
        Me.rtxtDescription.IsSourceFromValueList = False
        Me.rtxtDescription.IsUnique = False
        Me.rtxtDescription.Location = New System.Drawing.Point(517, 18)
        Me.rtxtDescription.MaxLength = 60
        Me.rtxtDescription.MendatroryField = False
        Me.rtxtDescription.Multiline = True
        Me.rtxtDescription.MyLinkLable1 = Me.rlblDescription
        Me.rtxtDescription.MyLinkLable2 = Nothing
        Me.rtxtDescription.Name = "rtxtDescription"
        Me.rtxtDescription.ReadOnly = True
        Me.rtxtDescription.ReferenceFieldDesc = Nothing
        Me.rtxtDescription.ReferenceFieldName = Nothing
        Me.rtxtDescription.ReferenceTableName = Nothing
        Me.rtxtDescription.Size = New System.Drawing.Size(199, 18)
        Me.rtxtDescription.TabIndex = 2
        Me.rtxtDescription.TabStop = False
        '
        'rbtndepot
        '
        Me.rbtndepot.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtndepot.Location = New System.Drawing.Point(517, 37)
        Me.rbtndepot.Name = "rbtndepot"
        Me.rbtndepot.Size = New System.Drawing.Size(50, 16)
        Me.rbtndepot.TabIndex = 4
        Me.rbtndepot.Text = "Depot"
        '
        'rbtnHire
        '
        Me.rbtnHire.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnHire.Location = New System.Drawing.Point(603, 37)
        Me.rbtnHire.Name = "rbtnHire"
        Me.rbtnHire.Size = New System.Drawing.Size(41, 16)
        Me.rbtnHire.TabIndex = 5
        Me.rbtnHire.Text = "Hire"
        '
        'rbtnSave
        '
        Me.rbtnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rbtnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnSave.Location = New System.Drawing.Point(17, 8)
        Me.rbtnSave.Name = "rbtnSave"
        Me.rbtnSave.Size = New System.Drawing.Size(68, 18)
        Me.rbtnSave.TabIndex = 0
        Me.rbtnSave.Text = "Save"
        '
        'rbtnDelete
        '
        Me.rbtnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rbtnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnDelete.Location = New System.Drawing.Point(89, 8)
        Me.rbtnDelete.Name = "rbtnDelete"
        Me.rbtnDelete.Size = New System.Drawing.Size(68, 18)
        Me.rbtnDelete.TabIndex = 1
        Me.rbtnDelete.Text = "Delete"
        '
        'rbtnClose
        '
        Me.rbtnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rbtnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnClose.Location = New System.Drawing.Point(712, 8)
        Me.rbtnClose.Name = "rbtnClose"
        Me.rbtnClose.Size = New System.Drawing.Size(68, 18)
        Me.rbtnClose.TabIndex = 2
        Me.rbtnClose.Text = "Close"
        '
        'rbtnReset
        '
        Me.rbtnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnReset.Image = Global.XpertERPSalesAndDistribution.My.Resources.Resources._new
        Me.rbtnReset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.rbtnReset.Location = New System.Drawing.Point(340, 18)
        Me.rbtnReset.Name = "rbtnReset"
        Me.rbtnReset.Size = New System.Drawing.Size(19, 18)
        Me.rbtnReset.TabIndex = 1
        '
        'rlblVehicleRegistrationNo
        '
        Me.rlblVehicleRegistrationNo.FieldName = Nothing
        Me.rlblVehicleRegistrationNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rlblVehicleRegistrationNo.Location = New System.Drawing.Point(13, 61)
        Me.rlblVehicleRegistrationNo.Name = "rlblVehicleRegistrationNo"
        Me.rlblVehicleRegistrationNo.Size = New System.Drawing.Size(128, 16)
        Me.rlblVehicleRegistrationNo.TabIndex = 10
        Me.rlblVehicleRegistrationNo.Text = "Vehicle Registration  No"
        '
        'rtxtVehicle_registration_No
        '
        Me.rtxtVehicle_registration_No.CalculationExpression = Nothing
        Me.rtxtVehicle_registration_No.FieldCode = Nothing
        Me.rtxtVehicle_registration_No.FieldDesc = Nothing
        Me.rtxtVehicle_registration_No.FieldMaxLength = 0
        Me.rtxtVehicle_registration_No.FieldName = Nothing
        Me.rtxtVehicle_registration_No.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtxtVehicle_registration_No.isCalculatedField = False
        Me.rtxtVehicle_registration_No.IsSourceFromTable = False
        Me.rtxtVehicle_registration_No.IsSourceFromValueList = False
        Me.rtxtVehicle_registration_No.IsUnique = False
        Me.rtxtVehicle_registration_No.Location = New System.Drawing.Point(159, 60)
        Me.rtxtVehicle_registration_No.MaxLength = 11
        Me.rtxtVehicle_registration_No.MendatroryField = False
        Me.rtxtVehicle_registration_No.MyLinkLable1 = Me.rlblVehicleRegistrationNo
        Me.rtxtVehicle_registration_No.MyLinkLable2 = Nothing
        Me.rtxtVehicle_registration_No.Name = "rtxtVehicle_registration_No"
        Me.rtxtVehicle_registration_No.ReferenceFieldDesc = Nothing
        Me.rtxtVehicle_registration_No.ReferenceFieldName = Nothing
        Me.rtxtVehicle_registration_No.ReferenceTableName = Nothing
        Me.rtxtVehicle_registration_No.Size = New System.Drawing.Size(200, 18)
        Me.rtxtVehicle_registration_No.TabIndex = 6
        '
        'rlblVehicleChechisNo
        '
        Me.rlblVehicleChechisNo.FieldName = Nothing
        Me.rlblVehicleChechisNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rlblVehicleChechisNo.Location = New System.Drawing.Point(13, 82)
        Me.rlblVehicleChechisNo.Name = "rlblVehicleChechisNo"
        Me.rlblVehicleChechisNo.Size = New System.Drawing.Size(99, 16)
        Me.rlblVehicleChechisNo.TabIndex = 14
        Me.rlblVehicleChechisNo.Text = "Vehicle Chasis No"
        '
        'rtxtvehicle_Chechis_No
        '
        Me.rtxtvehicle_Chechis_No.CalculationExpression = Nothing
        Me.rtxtvehicle_Chechis_No.FieldCode = Nothing
        Me.rtxtvehicle_Chechis_No.FieldDesc = Nothing
        Me.rtxtvehicle_Chechis_No.FieldMaxLength = 0
        Me.rtxtvehicle_Chechis_No.FieldName = Nothing
        Me.rtxtvehicle_Chechis_No.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtxtvehicle_Chechis_No.isCalculatedField = False
        Me.rtxtvehicle_Chechis_No.IsSourceFromTable = False
        Me.rtxtvehicle_Chechis_No.IsSourceFromValueList = False
        Me.rtxtvehicle_Chechis_No.IsUnique = False
        Me.rtxtvehicle_Chechis_No.Location = New System.Drawing.Point(159, 81)
        Me.rtxtvehicle_Chechis_No.MaxLength = 11
        Me.rtxtvehicle_Chechis_No.MendatroryField = True
        Me.rtxtvehicle_Chechis_No.MyLinkLable1 = Me.rlblVehicleChechisNo
        Me.rtxtvehicle_Chechis_No.MyLinkLable2 = Nothing
        Me.rtxtvehicle_Chechis_No.Name = "rtxtvehicle_Chechis_No"
        Me.rtxtvehicle_Chechis_No.ReferenceFieldDesc = Nothing
        Me.rtxtvehicle_Chechis_No.ReferenceFieldName = Nothing
        Me.rtxtvehicle_Chechis_No.ReferenceTableName = Nothing
        Me.rtxtvehicle_Chechis_No.Size = New System.Drawing.Size(200, 18)
        Me.rtxtvehicle_Chechis_No.TabIndex = 8
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem_Import, Me.RadMenuItem_Export, Me.RadMenuItem_Close})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'RadMenuItem_Import
        '
        Me.RadMenuItem_Import.AccessibleDescription = "Import"
        Me.RadMenuItem_Import.AccessibleName = "Import"
        Me.RadMenuItem_Import.Name = "RadMenuItem_Import"
        Me.RadMenuItem_Import.Text = "Import"
        '
        'RadMenuItem_Export
        '
        Me.RadMenuItem_Export.AccessibleDescription = "Export"
        Me.RadMenuItem_Export.AccessibleName = "Export"
        Me.RadMenuItem_Export.Name = "RadMenuItem_Export"
        Me.RadMenuItem_Export.Text = "Export"
        '
        'RadMenuItem_Close
        '
        Me.RadMenuItem_Close.AccessibleDescription = "Close"
        Me.RadMenuItem_Close.AccessibleName = "Close"
        Me.RadMenuItem_Close.Name = "RadMenuItem_Close"
        Me.RadMenuItem_Close.Text = "Close"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.txtVehicleWeight)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel11)
        Me.RadGroupBox1.Controls.Add(Me.pnlmt)
        Me.RadGroupBox1.Controls.Add(Me.fndemployee)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox1.Controls.Add(Me.txtSequenceNo)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.TxtCrateCapacity)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.rtxtCapacity)
        Me.RadGroupBox1.Controls.Add(Me.lblRoadTax_valid_till)
        Me.RadGroupBox1.Controls.Add(Me.lblRoadtax_valid_from)
        Me.RadGroupBox1.Controls.Add(Me.lblPollutonchk_valid_till)
        Me.RadGroupBox1.Controls.Add(Me.lblPollutionchk_valid_frm)
        Me.RadGroupBox1.Controls.Add(Me.lblFitness_valid_till)
        Me.RadGroupBox1.Controls.Add(Me.lblFitness_valid_from)
        Me.RadGroupBox1.Controls.Add(Me.lblInsurance_valid_till)
        Me.RadGroupBox1.Controls.Add(Me.lblInsurance_valid_from)
        Me.RadGroupBox1.Controls.Add(Me.txtRoad_tax_valid_till)
        Me.RadGroupBox1.Controls.Add(Me.txtPollution_valid_till)
        Me.RadGroupBox1.Controls.Add(Me.txtFitness_valid_till)
        Me.RadGroupBox1.Controls.Add(Me.txtInsurance_valid_till)
        Me.RadGroupBox1.Controls.Add(Me.txtRoad_tax_valid_from)
        Me.RadGroupBox1.Controls.Add(Me.txtPollution_valid_from)
        Me.RadGroupBox1.Controls.Add(Me.txtFitness_valid_from)
        Me.RadGroupBox1.Controls.Add(Me.txtInsurance_valid_from)
        Me.RadGroupBox1.Controls.Add(Me.lblLocation)
        Me.RadGroupBox1.Controls.Add(Me.lblVehicleName)
        Me.RadGroupBox1.Controls.Add(Me.llVehicleNo)
        Me.RadGroupBox1.Controls.Add(Me.lblEngineNo)
        Me.RadGroupBox1.Controls.Add(Me.LblVehicleBrand)
        Me.RadGroupBox1.Controls.Add(Me.lblRegsteredOn)
        Me.RadGroupBox1.Controls.Add(Me.rtxtlocation)
        Me.RadGroupBox1.Controls.Add(Me.rtxtvehicleNo)
        Me.RadGroupBox1.Controls.Add(Me.rtxtvehicleName)
        Me.RadGroupBox1.Controls.Add(Me.rtxtengineno)
        Me.RadGroupBox1.Controls.Add(Me.rtxtRegistredOn)
        Me.RadGroupBox1.Controls.Add(Me.rtxtvehiclebrand)
        Me.RadGroupBox1.Controls.Add(Me.fndTransporter)
        Me.RadGroupBox1.Controls.Add(Me.fndVehicle_id)
        Me.RadGroupBox1.Controls.Add(Me.lblTransporter)
        Me.RadGroupBox1.Controls.Add(Me.rtxtTranType)
        Me.RadGroupBox1.Controls.Add(Me.lblTranType)
        Me.RadGroupBox1.Controls.Add(Me.lblCapacity)
        Me.RadGroupBox1.Controls.Add(Me.rlblVehicleID)
        Me.RadGroupBox1.Controls.Add(Me.rlblModel)
        Me.RadGroupBox1.Controls.Add(Me.rtxtvehicle_Chechis_No)
        Me.RadGroupBox1.Controls.Add(Me.rlbltype)
        Me.RadGroupBox1.Controls.Add(Me.rlblVehicleChechisNo)
        Me.RadGroupBox1.Controls.Add(Me.rtxtVehicle_registration_No)
        Me.RadGroupBox1.Controls.Add(Me.rlblNumber)
        Me.RadGroupBox1.Controls.Add(Me.rlblVehicleRegistrationNo)
        Me.RadGroupBox1.Controls.Add(Me.rlblDescription)
        Me.RadGroupBox1.Controls.Add(Me.rbtnReset)
        Me.RadGroupBox1.Controls.Add(Me.rtxtModel)
        Me.RadGroupBox1.Controls.Add(Me.rtxtNumber)
        Me.RadGroupBox1.Controls.Add(Me.rbtnHire)
        Me.RadGroupBox1.Controls.Add(Me.rtxtDescription)
        Me.RadGroupBox1.Controls.Add(Me.rbtndepot)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(771, 369)
        Me.RadGroupBox1.TabIndex = 0
        Me.RadGroupBox1.ThemeName = "ControlDefault"
        '
        'txtVehicleWeight
        '
        Me.txtVehicleWeight.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtVehicleWeight.CalculationExpression = Nothing
        Me.txtVehicleWeight.DecimalPlaces = 0
        Me.txtVehicleWeight.FieldCode = Nothing
        Me.txtVehicleWeight.FieldDesc = Nothing
        Me.txtVehicleWeight.FieldMaxLength = 0
        Me.txtVehicleWeight.FieldName = Nothing
        Me.txtVehicleWeight.isCalculatedField = False
        Me.txtVehicleWeight.IsSourceFromTable = False
        Me.txtVehicleWeight.IsSourceFromValueList = False
        Me.txtVehicleWeight.IsUnique = False
        Me.txtVehicleWeight.Location = New System.Drawing.Point(160, 317)
        Me.txtVehicleWeight.MendatroryField = True
        Me.txtVehicleWeight.MyLinkLable1 = Me.MyLabel11
        Me.txtVehicleWeight.MyLinkLable2 = Nothing
        Me.txtVehicleWeight.Name = "txtVehicleWeight"
        Me.txtVehicleWeight.ReferenceFieldDesc = Nothing
        Me.txtVehicleWeight.ReferenceFieldName = Nothing
        Me.txtVehicleWeight.ReferenceTableName = Nothing
        Me.txtVehicleWeight.Size = New System.Drawing.Size(200, 20)
        Me.txtVehicleWeight.TabIndex = 58
        Me.txtVehicleWeight.Text = "0"
        Me.txtVehicleWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtVehicleWeight.Value = 0R
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel11.Location = New System.Drawing.Point(13, 319)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(82, 16)
        Me.MyLabel11.TabIndex = 59
        Me.MyLabel11.Text = "Vehicle Weight"
        '
        'pnlmt
        '
        Me.pnlmt.Controls.Add(Me.MyLabel4)
        Me.pnlmt.Controls.Add(Me.txtmtvalue)
        Me.pnlmt.Controls.Add(Me.MyLabel5)
        Me.pnlmt.Controls.Add(Me.txtmtcapacity)
        Me.pnlmt.Location = New System.Drawing.Point(373, 292)
        Me.pnlmt.Name = "pnlmt"
        Me.pnlmt.Size = New System.Drawing.Size(352, 44)
        Me.pnlmt.TabIndex = 57
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(3, 3)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(70, 16)
        Me.MyLabel4.TabIndex = 54
        Me.MyLabel4.Text = "MT Capacity"
        '
        'txtmtvalue
        '
        Me.txtmtvalue.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtmtvalue.CalculationExpression = Nothing
        Me.txtmtvalue.DecimalPlaces = 2
        Me.txtmtvalue.FieldCode = Nothing
        Me.txtmtvalue.FieldDesc = Nothing
        Me.txtmtvalue.FieldMaxLength = 18
        Me.txtmtvalue.FieldName = Nothing
        Me.txtmtvalue.isCalculatedField = False
        Me.txtmtvalue.IsSourceFromTable = False
        Me.txtmtvalue.IsSourceFromValueList = False
        Me.txtmtvalue.IsUnique = False
        Me.txtmtvalue.Location = New System.Drawing.Point(145, 23)
        Me.txtmtvalue.MendatroryField = False
        Me.txtmtvalue.MyLinkLable1 = Me.MyLabel5
        Me.txtmtvalue.MyLinkLable2 = Nothing
        Me.txtmtvalue.Name = "txtmtvalue"
        Me.txtmtvalue.ReferenceFieldDesc = Nothing
        Me.txtmtvalue.ReferenceFieldName = Nothing
        Me.txtmtvalue.ReferenceTableName = Nothing
        Me.txtmtvalue.Size = New System.Drawing.Size(200, 20)
        Me.txtmtvalue.TabIndex = 55
        Me.txtmtvalue.Text = "0"
        Me.txtmtvalue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtmtvalue.Value = 0R
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(3, 25)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(55, 16)
        Me.MyLabel5.TabIndex = 56
        Me.MyLabel5.Text = "MT Value"
        '
        'txtmtcapacity
        '
        Me.txtmtcapacity.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtmtcapacity.CalculationExpression = Nothing
        Me.txtmtcapacity.DecimalPlaces = 2
        Me.txtmtcapacity.FieldCode = Nothing
        Me.txtmtcapacity.FieldDesc = Nothing
        Me.txtmtcapacity.FieldMaxLength = 18
        Me.txtmtcapacity.FieldName = Nothing
        Me.txtmtcapacity.isCalculatedField = False
        Me.txtmtcapacity.IsSourceFromTable = False
        Me.txtmtcapacity.IsSourceFromValueList = False
        Me.txtmtcapacity.IsUnique = False
        Me.txtmtcapacity.Location = New System.Drawing.Point(145, 1)
        Me.txtmtcapacity.MendatroryField = False
        Me.txtmtcapacity.MyLinkLable1 = Me.MyLabel4
        Me.txtmtcapacity.MyLinkLable2 = Nothing
        Me.txtmtcapacity.Name = "txtmtcapacity"
        Me.txtmtcapacity.ReferenceFieldDesc = Nothing
        Me.txtmtcapacity.ReferenceFieldName = Nothing
        Me.txtmtcapacity.ReferenceTableName = Nothing
        Me.txtmtcapacity.Size = New System.Drawing.Size(200, 20)
        Me.txtmtcapacity.TabIndex = 53
        Me.txtmtcapacity.Text = "0"
        Me.txtmtcapacity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtmtcapacity.Value = 0R
        '
        'fndemployee
        '
        Me.fndemployee.CalculationExpression = Nothing
        Me.fndemployee.FieldCode = Nothing
        Me.fndemployee.FieldDesc = Nothing
        Me.fndemployee.FieldMaxLength = 0
        Me.fndemployee.FieldName = Nothing
        Me.fndemployee.isCalculatedField = False
        Me.fndemployee.IsSourceFromTable = False
        Me.fndemployee.IsSourceFromValueList = False
        Me.fndemployee.IsUnique = False
        Me.fndemployee.Location = New System.Drawing.Point(160, 295)
        Me.fndemployee.MendatroryField = False
        Me.fndemployee.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndemployee.MyLinkLable1 = Me.MyLabel3
        Me.fndemployee.MyLinkLable2 = Nothing
        Me.fndemployee.MyReadOnly = False
        Me.fndemployee.MyShowMasterFormButton = False
        Me.fndemployee.Name = "fndemployee"
        Me.fndemployee.ReferenceFieldDesc = Nothing
        Me.fndemployee.ReferenceFieldName = Nothing
        Me.fndemployee.ReferenceTableName = Nothing
        Me.fndemployee.Size = New System.Drawing.Size(200, 19)
        Me.fndemployee.TabIndex = 51
        Me.fndemployee.Value = ""
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(13, 295)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(77, 16)
        Me.MyLabel3.TabIndex = 52
        Me.MyLabel3.Text = "Employee No."
        '
        'txtSequenceNo
        '
        Me.txtSequenceNo.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtSequenceNo.CalculationExpression = Nothing
        Me.txtSequenceNo.DecimalPlaces = 0
        Me.txtSequenceNo.FieldCode = Nothing
        Me.txtSequenceNo.FieldDesc = Nothing
        Me.txtSequenceNo.FieldMaxLength = 0
        Me.txtSequenceNo.FieldName = Nothing
        Me.txtSequenceNo.isCalculatedField = False
        Me.txtSequenceNo.IsSourceFromTable = False
        Me.txtSequenceNo.IsSourceFromValueList = False
        Me.txtSequenceNo.IsUnique = False
        Me.txtSequenceNo.Location = New System.Drawing.Point(518, 271)
        Me.txtSequenceNo.MendatroryField = False
        Me.txtSequenceNo.MyLinkLable1 = Me.MyLabel2
        Me.txtSequenceNo.MyLinkLable2 = Nothing
        Me.txtSequenceNo.Name = "txtSequenceNo"
        Me.txtSequenceNo.ReferenceFieldDesc = Nothing
        Me.txtSequenceNo.ReferenceFieldName = Nothing
        Me.txtSequenceNo.ReferenceTableName = Nothing
        Me.txtSequenceNo.Size = New System.Drawing.Size(200, 20)
        Me.txtSequenceNo.TabIndex = 19
        Me.txtSequenceNo.Text = "0"
        Me.txtSequenceNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSequenceNo.Value = 0R
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(376, 273)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(75, 16)
        Me.MyLabel2.TabIndex = 20
        Me.MyLabel2.Text = "Sequence No"
        '
        'TxtCrateCapacity
        '
        Me.TxtCrateCapacity.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtCrateCapacity.CalculationExpression = Nothing
        Me.TxtCrateCapacity.DecimalPlaces = 0
        Me.TxtCrateCapacity.FieldCode = Nothing
        Me.TxtCrateCapacity.FieldDesc = Nothing
        Me.TxtCrateCapacity.FieldMaxLength = 0
        Me.TxtCrateCapacity.FieldName = Nothing
        Me.TxtCrateCapacity.isCalculatedField = False
        Me.TxtCrateCapacity.IsSourceFromTable = False
        Me.TxtCrateCapacity.IsSourceFromValueList = False
        Me.TxtCrateCapacity.IsUnique = False
        Me.TxtCrateCapacity.Location = New System.Drawing.Point(160, 272)
        Me.TxtCrateCapacity.MendatroryField = True
        Me.TxtCrateCapacity.MyLinkLable1 = Me.MyLabel1
        Me.TxtCrateCapacity.MyLinkLable2 = Nothing
        Me.TxtCrateCapacity.Name = "TxtCrateCapacity"
        Me.TxtCrateCapacity.ReferenceFieldDesc = Nothing
        Me.TxtCrateCapacity.ReferenceFieldName = Nothing
        Me.TxtCrateCapacity.ReferenceTableName = Nothing
        Me.TxtCrateCapacity.Size = New System.Drawing.Size(200, 20)
        Me.TxtCrateCapacity.TabIndex = 26
        Me.TxtCrateCapacity.Text = "0"
        Me.TxtCrateCapacity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtCrateCapacity.Value = 0R
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(13, 275)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(81, 16)
        Me.MyLabel1.TabIndex = 50
        Me.MyLabel1.Text = "Crate Capacity"
        '
        'rtxtCapacity
        '
        Me.rtxtCapacity.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.rtxtCapacity.CalculationExpression = Nothing
        Me.rtxtCapacity.DecimalPlaces = 0
        Me.rtxtCapacity.FieldCode = Nothing
        Me.rtxtCapacity.FieldDesc = Nothing
        Me.rtxtCapacity.FieldMaxLength = 0
        Me.rtxtCapacity.FieldName = Nothing
        Me.rtxtCapacity.isCalculatedField = False
        Me.rtxtCapacity.IsSourceFromTable = False
        Me.rtxtCapacity.IsSourceFromValueList = False
        Me.rtxtCapacity.IsUnique = False
        Me.rtxtCapacity.Location = New System.Drawing.Point(159, 102)
        Me.rtxtCapacity.MendatroryField = True
        Me.rtxtCapacity.MyLinkLable1 = Me.lblCapacity
        Me.rtxtCapacity.MyLinkLable2 = Nothing
        Me.rtxtCapacity.Name = "rtxtCapacity"
        Me.rtxtCapacity.ReferenceFieldDesc = Nothing
        Me.rtxtCapacity.ReferenceFieldName = Nothing
        Me.rtxtCapacity.ReferenceTableName = Nothing
        Me.rtxtCapacity.Size = New System.Drawing.Size(200, 20)
        Me.rtxtCapacity.TabIndex = 10
        Me.rtxtCapacity.Text = "0"
        Me.rtxtCapacity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.rtxtCapacity.Value = 0R
        '
        'lblCapacity
        '
        Me.lblCapacity.FieldName = Nothing
        Me.lblCapacity.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCapacity.Location = New System.Drawing.Point(13, 105)
        Me.lblCapacity.Name = "lblCapacity"
        Me.lblCapacity.Size = New System.Drawing.Size(50, 16)
        Me.lblCapacity.TabIndex = 18
        Me.lblCapacity.Text = "Capacity"
        '
        'lblRoadTax_valid_till
        '
        Me.lblRoadTax_valid_till.FieldName = Nothing
        Me.lblRoadTax_valid_till.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRoadTax_valid_till.Location = New System.Drawing.Point(376, 251)
        Me.lblRoadTax_valid_till.Name = "lblRoadTax_valid_till"
        Me.lblRoadTax_valid_till.Size = New System.Drawing.Size(101, 16)
        Me.lblRoadTax_valid_till.TabIndex = 48
        Me.lblRoadTax_valid_till.Text = "Road Tax Valid Till"
        Me.lblRoadTax_valid_till.TextWrap = False
        '
        'lblRoadtax_valid_from
        '
        Me.lblRoadtax_valid_from.FieldName = Nothing
        Me.lblRoadtax_valid_from.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRoadtax_valid_from.Location = New System.Drawing.Point(13, 251)
        Me.lblRoadtax_valid_from.Name = "lblRoadtax_valid_from"
        Me.lblRoadtax_valid_from.Size = New System.Drawing.Size(113, 16)
        Me.lblRoadtax_valid_from.TabIndex = 46
        Me.lblRoadtax_valid_from.Text = "Road Tax Valid From"
        Me.lblRoadtax_valid_from.TextWrap = False
        '
        'lblPollutonchk_valid_till
        '
        Me.lblPollutonchk_valid_till.FieldName = Nothing
        Me.lblPollutonchk_valid_till.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPollutonchk_valid_till.Location = New System.Drawing.Point(376, 230)
        Me.lblPollutonchk_valid_till.Name = "lblPollutonchk_valid_till"
        Me.lblPollutonchk_valid_till.Size = New System.Drawing.Size(128, 16)
        Me.lblPollutonchk_valid_till.TabIndex = 44
        Me.lblPollutonchk_valid_till.Text = "PollutionCheck Valid Till"
        Me.lblPollutonchk_valid_till.TextWrap = False
        '
        'lblPollutionchk_valid_frm
        '
        Me.lblPollutionchk_valid_frm.FieldName = Nothing
        Me.lblPollutionchk_valid_frm.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPollutionchk_valid_frm.Location = New System.Drawing.Point(13, 229)
        Me.lblPollutionchk_valid_frm.Name = "lblPollutionchk_valid_frm"
        Me.lblPollutionchk_valid_frm.Size = New System.Drawing.Size(140, 16)
        Me.lblPollutionchk_valid_frm.TabIndex = 42
        Me.lblPollutionchk_valid_frm.Text = "PollutionCheck Valid From"
        Me.lblPollutionchk_valid_frm.TextWrap = False
        '
        'lblFitness_valid_till
        '
        Me.lblFitness_valid_till.FieldName = Nothing
        Me.lblFitness_valid_till.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFitness_valid_till.Location = New System.Drawing.Point(376, 209)
        Me.lblFitness_valid_till.Name = "lblFitness_valid_till"
        Me.lblFitness_valid_till.Size = New System.Drawing.Size(89, 16)
        Me.lblFitness_valid_till.TabIndex = 40
        Me.lblFitness_valid_till.Text = "Fitness Valid Till"
        Me.lblFitness_valid_till.TextWrap = False
        '
        'lblFitness_valid_from
        '
        Me.lblFitness_valid_from.FieldName = Nothing
        Me.lblFitness_valid_from.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFitness_valid_from.Location = New System.Drawing.Point(13, 209)
        Me.lblFitness_valid_from.Name = "lblFitness_valid_from"
        Me.lblFitness_valid_from.Size = New System.Drawing.Size(101, 16)
        Me.lblFitness_valid_from.TabIndex = 38
        Me.lblFitness_valid_from.Text = "Fitness Valid From"
        Me.lblFitness_valid_from.TextWrap = False
        '
        'lblInsurance_valid_till
        '
        Me.lblInsurance_valid_till.FieldName = Nothing
        Me.lblInsurance_valid_till.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInsurance_valid_till.Location = New System.Drawing.Point(376, 188)
        Me.lblInsurance_valid_till.Name = "lblInsurance_valid_till"
        Me.lblInsurance_valid_till.Size = New System.Drawing.Size(102, 16)
        Me.lblInsurance_valid_till.TabIndex = 36
        Me.lblInsurance_valid_till.Text = "Insurance Valid Till"
        Me.lblInsurance_valid_till.TextWrap = False
        '
        'lblInsurance_valid_from
        '
        Me.lblInsurance_valid_from.FieldName = Nothing
        Me.lblInsurance_valid_from.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInsurance_valid_from.Location = New System.Drawing.Point(13, 188)
        Me.lblInsurance_valid_from.Name = "lblInsurance_valid_from"
        Me.lblInsurance_valid_from.Size = New System.Drawing.Size(114, 16)
        Me.lblInsurance_valid_from.TabIndex = 34
        Me.lblInsurance_valid_from.Text = "Insurance Valid From"
        Me.lblInsurance_valid_from.TextWrap = False
        '
        'txtRoad_tax_valid_till
        '
        Me.txtRoad_tax_valid_till.CalculationExpression = Nothing
        Me.txtRoad_tax_valid_till.CustomFormat = "dd/MM/yyyy"
        Me.txtRoad_tax_valid_till.FieldCode = Nothing
        Me.txtRoad_tax_valid_till.FieldDesc = Nothing
        Me.txtRoad_tax_valid_till.FieldMaxLength = 0
        Me.txtRoad_tax_valid_till.FieldName = Nothing
        Me.txtRoad_tax_valid_till.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRoad_tax_valid_till.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtRoad_tax_valid_till.isCalculatedField = False
        Me.txtRoad_tax_valid_till.IsSourceFromTable = False
        Me.txtRoad_tax_valid_till.IsSourceFromValueList = False
        Me.txtRoad_tax_valid_till.IsUnique = False
        Me.txtRoad_tax_valid_till.Location = New System.Drawing.Point(517, 249)
        Me.txtRoad_tax_valid_till.MendatroryField = False
        Me.txtRoad_tax_valid_till.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtRoad_tax_valid_till.MyLinkLable1 = Me.lblRoadTax_valid_till
        Me.txtRoad_tax_valid_till.MyLinkLable2 = Nothing
        Me.txtRoad_tax_valid_till.Name = "txtRoad_tax_valid_till"
        Me.txtRoad_tax_valid_till.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtRoad_tax_valid_till.ReferenceFieldDesc = Nothing
        Me.txtRoad_tax_valid_till.ReferenceFieldName = Nothing
        Me.txtRoad_tax_valid_till.ReferenceTableName = Nothing
        Me.txtRoad_tax_valid_till.ShowCheckBox = True
        Me.txtRoad_tax_valid_till.Size = New System.Drawing.Size(200, 18)
        Me.txtRoad_tax_valid_till.TabIndex = 25
        Me.txtRoad_tax_valid_till.TabStop = False
        Me.txtRoad_tax_valid_till.Text = "13/06/2011"
        Me.txtRoad_tax_valid_till.Value = New Date(2011, 6, 13, 0, 0, 0, 0)
        '
        'txtPollution_valid_till
        '
        Me.txtPollution_valid_till.CalculationExpression = Nothing
        Me.txtPollution_valid_till.CustomFormat = "dd/MM/yyyy"
        Me.txtPollution_valid_till.FieldCode = Nothing
        Me.txtPollution_valid_till.FieldDesc = Nothing
        Me.txtPollution_valid_till.FieldMaxLength = 0
        Me.txtPollution_valid_till.FieldName = Nothing
        Me.txtPollution_valid_till.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPollution_valid_till.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtPollution_valid_till.isCalculatedField = False
        Me.txtPollution_valid_till.IsSourceFromTable = False
        Me.txtPollution_valid_till.IsSourceFromValueList = False
        Me.txtPollution_valid_till.IsUnique = False
        Me.txtPollution_valid_till.Location = New System.Drawing.Point(517, 227)
        Me.txtPollution_valid_till.MendatroryField = False
        Me.txtPollution_valid_till.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtPollution_valid_till.MyLinkLable1 = Me.lblPollutonchk_valid_till
        Me.txtPollution_valid_till.MyLinkLable2 = Nothing
        Me.txtPollution_valid_till.Name = "txtPollution_valid_till"
        Me.txtPollution_valid_till.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtPollution_valid_till.ReferenceFieldDesc = Nothing
        Me.txtPollution_valid_till.ReferenceFieldName = Nothing
        Me.txtPollution_valid_till.ReferenceTableName = Nothing
        Me.txtPollution_valid_till.ShowCheckBox = True
        Me.txtPollution_valid_till.Size = New System.Drawing.Size(200, 18)
        Me.txtPollution_valid_till.TabIndex = 23
        Me.txtPollution_valid_till.TabStop = False
        Me.txtPollution_valid_till.Text = "13/06/2011"
        Me.txtPollution_valid_till.Value = New Date(2011, 6, 13, 0, 0, 0, 0)
        '
        'txtFitness_valid_till
        '
        Me.txtFitness_valid_till.CalculationExpression = Nothing
        Me.txtFitness_valid_till.CustomFormat = "dd/MM/yyyy"
        Me.txtFitness_valid_till.FieldCode = Nothing
        Me.txtFitness_valid_till.FieldDesc = Nothing
        Me.txtFitness_valid_till.FieldMaxLength = 0
        Me.txtFitness_valid_till.FieldName = Nothing
        Me.txtFitness_valid_till.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFitness_valid_till.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFitness_valid_till.isCalculatedField = False
        Me.txtFitness_valid_till.IsSourceFromTable = False
        Me.txtFitness_valid_till.IsSourceFromValueList = False
        Me.txtFitness_valid_till.IsUnique = False
        Me.txtFitness_valid_till.Location = New System.Drawing.Point(517, 207)
        Me.txtFitness_valid_till.MendatroryField = False
        Me.txtFitness_valid_till.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFitness_valid_till.MyLinkLable1 = Me.lblFitness_valid_till
        Me.txtFitness_valid_till.MyLinkLable2 = Nothing
        Me.txtFitness_valid_till.Name = "txtFitness_valid_till"
        Me.txtFitness_valid_till.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFitness_valid_till.ReferenceFieldDesc = Nothing
        Me.txtFitness_valid_till.ReferenceFieldName = Nothing
        Me.txtFitness_valid_till.ReferenceTableName = Nothing
        Me.txtFitness_valid_till.ShowCheckBox = True
        Me.txtFitness_valid_till.Size = New System.Drawing.Size(200, 18)
        Me.txtFitness_valid_till.TabIndex = 21
        Me.txtFitness_valid_till.TabStop = False
        Me.txtFitness_valid_till.Text = "13/06/2011"
        Me.txtFitness_valid_till.Value = New Date(2011, 6, 13, 0, 0, 0, 0)
        '
        'txtInsurance_valid_till
        '
        Me.txtInsurance_valid_till.CalculationExpression = Nothing
        Me.txtInsurance_valid_till.CustomFormat = "dd/MM/yyyy"
        Me.txtInsurance_valid_till.FieldCode = Nothing
        Me.txtInsurance_valid_till.FieldDesc = Nothing
        Me.txtInsurance_valid_till.FieldMaxLength = 0
        Me.txtInsurance_valid_till.FieldName = Nothing
        Me.txtInsurance_valid_till.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInsurance_valid_till.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtInsurance_valid_till.isCalculatedField = False
        Me.txtInsurance_valid_till.IsSourceFromTable = False
        Me.txtInsurance_valid_till.IsSourceFromValueList = False
        Me.txtInsurance_valid_till.IsUnique = False
        Me.txtInsurance_valid_till.Location = New System.Drawing.Point(517, 186)
        Me.txtInsurance_valid_till.MendatroryField = False
        Me.txtInsurance_valid_till.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtInsurance_valid_till.MyLinkLable1 = Me.lblInsurance_valid_till
        Me.txtInsurance_valid_till.MyLinkLable2 = Nothing
        Me.txtInsurance_valid_till.Name = "txtInsurance_valid_till"
        Me.txtInsurance_valid_till.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtInsurance_valid_till.ReferenceFieldDesc = Nothing
        Me.txtInsurance_valid_till.ReferenceFieldName = Nothing
        Me.txtInsurance_valid_till.ReferenceTableName = Nothing
        Me.txtInsurance_valid_till.ShowCheckBox = True
        Me.txtInsurance_valid_till.Size = New System.Drawing.Size(200, 18)
        Me.txtInsurance_valid_till.TabIndex = 19
        Me.txtInsurance_valid_till.TabStop = False
        Me.txtInsurance_valid_till.Text = "13/06/2011"
        Me.txtInsurance_valid_till.Value = New Date(2011, 6, 13, 0, 0, 0, 0)
        '
        'txtRoad_tax_valid_from
        '
        Me.txtRoad_tax_valid_from.CalculationExpression = Nothing
        Me.txtRoad_tax_valid_from.CustomFormat = "dd/MM/yyyy"
        Me.txtRoad_tax_valid_from.FieldCode = Nothing
        Me.txtRoad_tax_valid_from.FieldDesc = Nothing
        Me.txtRoad_tax_valid_from.FieldMaxLength = 0
        Me.txtRoad_tax_valid_from.FieldName = Nothing
        Me.txtRoad_tax_valid_from.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRoad_tax_valid_from.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtRoad_tax_valid_from.isCalculatedField = False
        Me.txtRoad_tax_valid_from.IsSourceFromTable = False
        Me.txtRoad_tax_valid_from.IsSourceFromValueList = False
        Me.txtRoad_tax_valid_from.IsUnique = False
        Me.txtRoad_tax_valid_from.Location = New System.Drawing.Point(159, 251)
        Me.txtRoad_tax_valid_from.MendatroryField = False
        Me.txtRoad_tax_valid_from.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtRoad_tax_valid_from.MyLinkLable1 = Me.lblRoadtax_valid_from
        Me.txtRoad_tax_valid_from.MyLinkLable2 = Nothing
        Me.txtRoad_tax_valid_from.Name = "txtRoad_tax_valid_from"
        Me.txtRoad_tax_valid_from.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtRoad_tax_valid_from.ReferenceFieldDesc = Nothing
        Me.txtRoad_tax_valid_from.ReferenceFieldName = Nothing
        Me.txtRoad_tax_valid_from.ReferenceTableName = Nothing
        Me.txtRoad_tax_valid_from.ShowCheckBox = True
        Me.txtRoad_tax_valid_from.Size = New System.Drawing.Size(200, 18)
        Me.txtRoad_tax_valid_from.TabIndex = 24
        Me.txtRoad_tax_valid_from.TabStop = False
        Me.txtRoad_tax_valid_from.Text = "13/06/2011"
        Me.txtRoad_tax_valid_from.Value = New Date(2011, 6, 13, 0, 0, 0, 0)
        '
        'txtPollution_valid_from
        '
        Me.txtPollution_valid_from.CalculationExpression = Nothing
        Me.txtPollution_valid_from.CustomFormat = "dd/MM/yyyy"
        Me.txtPollution_valid_from.FieldCode = Nothing
        Me.txtPollution_valid_from.FieldDesc = Nothing
        Me.txtPollution_valid_from.FieldMaxLength = 0
        Me.txtPollution_valid_from.FieldName = Nothing
        Me.txtPollution_valid_from.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPollution_valid_from.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtPollution_valid_from.isCalculatedField = False
        Me.txtPollution_valid_from.IsSourceFromTable = False
        Me.txtPollution_valid_from.IsSourceFromValueList = False
        Me.txtPollution_valid_from.IsUnique = False
        Me.txtPollution_valid_from.Location = New System.Drawing.Point(159, 230)
        Me.txtPollution_valid_from.MendatroryField = False
        Me.txtPollution_valid_from.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtPollution_valid_from.MyLinkLable1 = Me.lblPollutionchk_valid_frm
        Me.txtPollution_valid_from.MyLinkLable2 = Nothing
        Me.txtPollution_valid_from.Name = "txtPollution_valid_from"
        Me.txtPollution_valid_from.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtPollution_valid_from.ReferenceFieldDesc = Nothing
        Me.txtPollution_valid_from.ReferenceFieldName = Nothing
        Me.txtPollution_valid_from.ReferenceTableName = Nothing
        Me.txtPollution_valid_from.ShowCheckBox = True
        Me.txtPollution_valid_from.Size = New System.Drawing.Size(200, 18)
        Me.txtPollution_valid_from.TabIndex = 22
        Me.txtPollution_valid_from.TabStop = False
        Me.txtPollution_valid_from.Text = "13/06/2011"
        Me.txtPollution_valid_from.Value = New Date(2011, 6, 13, 0, 0, 0, 0)
        '
        'txtFitness_valid_from
        '
        Me.txtFitness_valid_from.CalculationExpression = Nothing
        Me.txtFitness_valid_from.CustomFormat = "dd/MM/yyyy"
        Me.txtFitness_valid_from.FieldCode = Nothing
        Me.txtFitness_valid_from.FieldDesc = Nothing
        Me.txtFitness_valid_from.FieldMaxLength = 0
        Me.txtFitness_valid_from.FieldName = Nothing
        Me.txtFitness_valid_from.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFitness_valid_from.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFitness_valid_from.isCalculatedField = False
        Me.txtFitness_valid_from.IsSourceFromTable = False
        Me.txtFitness_valid_from.IsSourceFromValueList = False
        Me.txtFitness_valid_from.IsUnique = False
        Me.txtFitness_valid_from.Location = New System.Drawing.Point(159, 209)
        Me.txtFitness_valid_from.MendatroryField = False
        Me.txtFitness_valid_from.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFitness_valid_from.MyLinkLable1 = Me.lblFitness_valid_from
        Me.txtFitness_valid_from.MyLinkLable2 = Nothing
        Me.txtFitness_valid_from.Name = "txtFitness_valid_from"
        Me.txtFitness_valid_from.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFitness_valid_from.ReferenceFieldDesc = Nothing
        Me.txtFitness_valid_from.ReferenceFieldName = Nothing
        Me.txtFitness_valid_from.ReferenceTableName = Nothing
        Me.txtFitness_valid_from.ShowCheckBox = True
        Me.txtFitness_valid_from.Size = New System.Drawing.Size(200, 18)
        Me.txtFitness_valid_from.TabIndex = 20
        Me.txtFitness_valid_from.TabStop = False
        Me.txtFitness_valid_from.Text = "13/06/2011"
        Me.txtFitness_valid_from.Value = New Date(2011, 6, 13, 0, 0, 0, 0)
        '
        'txtInsurance_valid_from
        '
        Me.txtInsurance_valid_from.CalculationExpression = Nothing
        Me.txtInsurance_valid_from.CustomFormat = "dd/MM/yyyy"
        Me.txtInsurance_valid_from.FieldCode = Nothing
        Me.txtInsurance_valid_from.FieldDesc = Nothing
        Me.txtInsurance_valid_from.FieldMaxLength = 0
        Me.txtInsurance_valid_from.FieldName = Nothing
        Me.txtInsurance_valid_from.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInsurance_valid_from.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtInsurance_valid_from.isCalculatedField = False
        Me.txtInsurance_valid_from.IsSourceFromTable = False
        Me.txtInsurance_valid_from.IsSourceFromValueList = False
        Me.txtInsurance_valid_from.IsUnique = False
        Me.txtInsurance_valid_from.Location = New System.Drawing.Point(159, 188)
        Me.txtInsurance_valid_from.MendatroryField = False
        Me.txtInsurance_valid_from.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtInsurance_valid_from.MyLinkLable1 = Me.lblInsurance_valid_from
        Me.txtInsurance_valid_from.MyLinkLable2 = Nothing
        Me.txtInsurance_valid_from.Name = "txtInsurance_valid_from"
        Me.txtInsurance_valid_from.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtInsurance_valid_from.ReferenceFieldDesc = Nothing
        Me.txtInsurance_valid_from.ReferenceFieldName = Nothing
        Me.txtInsurance_valid_from.ReferenceTableName = Nothing
        Me.txtInsurance_valid_from.ShowCheckBox = True
        Me.txtInsurance_valid_from.Size = New System.Drawing.Size(200, 18)
        Me.txtInsurance_valid_from.TabIndex = 18
        Me.txtInsurance_valid_from.TabStop = False
        Me.txtInsurance_valid_from.Text = "13/06/2011"
        Me.txtInsurance_valid_from.Value = New Date(2011, 6, 13, 0, 0, 0, 0)
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(376, 127)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 16)
        Me.lblLocation.TabIndex = 24
        Me.lblLocation.Text = "Location"
        '
        'lblVehicleName
        '
        Me.lblVehicleName.FieldName = Nothing
        Me.lblVehicleName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVehicleName.Location = New System.Drawing.Point(376, 147)
        Me.lblVehicleName.Name = "lblVehicleName"
        Me.lblVehicleName.Size = New System.Drawing.Size(77, 16)
        Me.lblVehicleName.TabIndex = 28
        Me.lblVehicleName.Text = "Vehicle Name"
        '
        'llVehicleNo
        '
        Me.llVehicleNo.FieldName = Nothing
        Me.llVehicleNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.llVehicleNo.Location = New System.Drawing.Point(376, 167)
        Me.llVehicleNo.Name = "llVehicleNo"
        Me.llVehicleNo.Size = New System.Drawing.Size(61, 16)
        Me.llVehicleNo.TabIndex = 32
        Me.llVehicleNo.Text = "Vehicle No"
        '
        'lblEngineNo
        '
        Me.lblEngineNo.FieldName = Nothing
        Me.lblEngineNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEngineNo.Location = New System.Drawing.Point(13, 169)
        Me.lblEngineNo.Name = "lblEngineNo"
        Me.lblEngineNo.Size = New System.Drawing.Size(59, 16)
        Me.lblEngineNo.TabIndex = 30
        Me.lblEngineNo.Text = "Engine No"
        '
        'LblVehicleBrand
        '
        Me.LblVehicleBrand.FieldName = Nothing
        Me.LblVehicleBrand.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblVehicleBrand.Location = New System.Drawing.Point(13, 147)
        Me.LblVehicleBrand.Name = "LblVehicleBrand"
        Me.LblVehicleBrand.Size = New System.Drawing.Size(77, 16)
        Me.LblVehicleBrand.TabIndex = 26
        Me.LblVehicleBrand.Text = "Vehicle Brand"
        '
        'lblRegsteredOn
        '
        Me.lblRegsteredOn.FieldName = Nothing
        Me.lblRegsteredOn.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRegsteredOn.Location = New System.Drawing.Point(13, 126)
        Me.lblRegsteredOn.Name = "lblRegsteredOn"
        Me.lblRegsteredOn.Size = New System.Drawing.Size(79, 16)
        Me.lblRegsteredOn.TabIndex = 22
        Me.lblRegsteredOn.Text = "Registered On"
        '
        'rtxtlocation
        '
        Me.rtxtlocation.CalculationExpression = Nothing
        Me.rtxtlocation.FieldCode = Nothing
        Me.rtxtlocation.FieldDesc = Nothing
        Me.rtxtlocation.FieldMaxLength = 0
        Me.rtxtlocation.FieldName = Nothing
        Me.rtxtlocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtxtlocation.isCalculatedField = False
        Me.rtxtlocation.IsSourceFromTable = False
        Me.rtxtlocation.IsSourceFromValueList = False
        Me.rtxtlocation.IsUnique = False
        Me.rtxtlocation.Location = New System.Drawing.Point(517, 122)
        Me.rtxtlocation.MaxLength = 11
        Me.rtxtlocation.MendatroryField = True
        Me.rtxtlocation.MyLinkLable1 = Me.lblLocation
        Me.rtxtlocation.MyLinkLable2 = Nothing
        Me.rtxtlocation.Name = "rtxtlocation"
        Me.rtxtlocation.ReferenceFieldDesc = Nothing
        Me.rtxtlocation.ReferenceFieldName = Nothing
        Me.rtxtlocation.ReferenceTableName = Nothing
        Me.rtxtlocation.Size = New System.Drawing.Size(201, 18)
        Me.rtxtlocation.TabIndex = 13
        '
        'rtxtvehicleNo
        '
        Me.rtxtvehicleNo.CalculationExpression = Nothing
        Me.rtxtvehicleNo.FieldCode = Nothing
        Me.rtxtvehicleNo.FieldDesc = Nothing
        Me.rtxtvehicleNo.FieldMaxLength = 0
        Me.rtxtvehicleNo.FieldName = Nothing
        Me.rtxtvehicleNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtxtvehicleNo.isCalculatedField = False
        Me.rtxtvehicleNo.IsSourceFromTable = False
        Me.rtxtvehicleNo.IsSourceFromValueList = False
        Me.rtxtvehicleNo.IsUnique = False
        Me.rtxtvehicleNo.Location = New System.Drawing.Point(517, 165)
        Me.rtxtvehicleNo.MaxLength = 11
        Me.rtxtvehicleNo.MendatroryField = True
        Me.rtxtvehicleNo.MyLinkLable1 = Me.llVehicleNo
        Me.rtxtvehicleNo.MyLinkLable2 = Nothing
        Me.rtxtvehicleNo.Name = "rtxtvehicleNo"
        Me.rtxtvehicleNo.ReferenceFieldDesc = Nothing
        Me.rtxtvehicleNo.ReferenceFieldName = Nothing
        Me.rtxtvehicleNo.ReferenceTableName = Nothing
        Me.rtxtvehicleNo.Size = New System.Drawing.Size(201, 18)
        Me.rtxtvehicleNo.TabIndex = 17
        '
        'rtxtvehicleName
        '
        Me.rtxtvehicleName.AcceptsReturn = True
        Me.rtxtvehicleName.CalculationExpression = Nothing
        Me.rtxtvehicleName.FieldCode = Nothing
        Me.rtxtvehicleName.FieldDesc = Nothing
        Me.rtxtvehicleName.FieldMaxLength = 0
        Me.rtxtvehicleName.FieldName = Nothing
        Me.rtxtvehicleName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtxtvehicleName.isCalculatedField = False
        Me.rtxtvehicleName.IsSourceFromTable = False
        Me.rtxtvehicleName.IsSourceFromValueList = False
        Me.rtxtvehicleName.IsUnique = False
        Me.rtxtvehicleName.Location = New System.Drawing.Point(517, 144)
        Me.rtxtvehicleName.MaxLength = 11
        Me.rtxtvehicleName.MendatroryField = True
        Me.rtxtvehicleName.MyLinkLable1 = Me.lblVehicleName
        Me.rtxtvehicleName.MyLinkLable2 = Nothing
        Me.rtxtvehicleName.Name = "rtxtvehicleName"
        Me.rtxtvehicleName.ReferenceFieldDesc = Nothing
        Me.rtxtvehicleName.ReferenceFieldName = Nothing
        Me.rtxtvehicleName.ReferenceTableName = Nothing
        Me.rtxtvehicleName.Size = New System.Drawing.Size(201, 18)
        Me.rtxtvehicleName.TabIndex = 15
        '
        'rtxtengineno
        '
        Me.rtxtengineno.CalculationExpression = Nothing
        Me.rtxtengineno.FieldCode = Nothing
        Me.rtxtengineno.FieldDesc = Nothing
        Me.rtxtengineno.FieldMaxLength = 0
        Me.rtxtengineno.FieldName = Nothing
        Me.rtxtengineno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtxtengineno.isCalculatedField = False
        Me.rtxtengineno.IsSourceFromTable = False
        Me.rtxtengineno.IsSourceFromValueList = False
        Me.rtxtengineno.IsUnique = False
        Me.rtxtengineno.Location = New System.Drawing.Point(159, 167)
        Me.rtxtengineno.MaxLength = 11
        Me.rtxtengineno.MendatroryField = True
        Me.rtxtengineno.MyLinkLable1 = Me.lblEngineNo
        Me.rtxtengineno.MyLinkLable2 = Nothing
        Me.rtxtengineno.Name = "rtxtengineno"
        Me.rtxtengineno.ReferenceFieldDesc = Nothing
        Me.rtxtengineno.ReferenceFieldName = Nothing
        Me.rtxtengineno.ReferenceTableName = Nothing
        Me.rtxtengineno.Size = New System.Drawing.Size(201, 18)
        Me.rtxtengineno.TabIndex = 16
        '
        'rtxtRegistredOn
        '
        Me.rtxtRegistredOn.CalculationExpression = Nothing
        Me.rtxtRegistredOn.FieldCode = Nothing
        Me.rtxtRegistredOn.FieldDesc = Nothing
        Me.rtxtRegistredOn.FieldMaxLength = 0
        Me.rtxtRegistredOn.FieldName = Nothing
        Me.rtxtRegistredOn.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtxtRegistredOn.isCalculatedField = False
        Me.rtxtRegistredOn.IsSourceFromTable = False
        Me.rtxtRegistredOn.IsSourceFromValueList = False
        Me.rtxtRegistredOn.IsUnique = False
        Me.rtxtRegistredOn.Location = New System.Drawing.Point(159, 125)
        Me.rtxtRegistredOn.MaxLength = 11
        Me.rtxtRegistredOn.MendatroryField = True
        Me.rtxtRegistredOn.MyLinkLable1 = Me.lblRegsteredOn
        Me.rtxtRegistredOn.MyLinkLable2 = Nothing
        Me.rtxtRegistredOn.Name = "rtxtRegistredOn"
        Me.rtxtRegistredOn.ReferenceFieldDesc = Nothing
        Me.rtxtRegistredOn.ReferenceFieldName = Nothing
        Me.rtxtRegistredOn.ReferenceTableName = Nothing
        Me.rtxtRegistredOn.Size = New System.Drawing.Size(201, 18)
        Me.rtxtRegistredOn.TabIndex = 12
        '
        'rtxtvehiclebrand
        '
        Me.rtxtvehiclebrand.CalculationExpression = Nothing
        Me.rtxtvehiclebrand.FieldCode = Nothing
        Me.rtxtvehiclebrand.FieldDesc = Nothing
        Me.rtxtvehiclebrand.FieldMaxLength = 0
        Me.rtxtvehiclebrand.FieldName = Nothing
        Me.rtxtvehiclebrand.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtxtvehiclebrand.isCalculatedField = False
        Me.rtxtvehiclebrand.IsSourceFromTable = False
        Me.rtxtvehiclebrand.IsSourceFromValueList = False
        Me.rtxtvehiclebrand.IsUnique = False
        Me.rtxtvehiclebrand.Location = New System.Drawing.Point(159, 146)
        Me.rtxtvehiclebrand.MaxLength = 11
        Me.rtxtvehiclebrand.MendatroryField = True
        Me.rtxtvehiclebrand.MyLinkLable1 = Me.LblVehicleBrand
        Me.rtxtvehiclebrand.MyLinkLable2 = Nothing
        Me.rtxtvehiclebrand.Name = "rtxtvehiclebrand"
        Me.rtxtvehiclebrand.ReferenceFieldDesc = Nothing
        Me.rtxtvehiclebrand.ReferenceFieldName = Nothing
        Me.rtxtvehiclebrand.ReferenceTableName = Nothing
        Me.rtxtvehiclebrand.Size = New System.Drawing.Size(201, 18)
        Me.rtxtvehiclebrand.TabIndex = 14
        '
        'fndTransporter
        '
        Me.fndTransporter.CalculationExpression = Nothing
        Me.fndTransporter.FieldCode = Nothing
        Me.fndTransporter.FieldDesc = Nothing
        Me.fndTransporter.FieldMaxLength = 0
        Me.fndTransporter.FieldName = Nothing
        Me.fndTransporter.isCalculatedField = False
        Me.fndTransporter.IsSourceFromTable = False
        Me.fndTransporter.IsSourceFromValueList = False
        Me.fndTransporter.IsUnique = False
        Me.fndTransporter.Location = New System.Drawing.Point(517, 53)
        Me.fndTransporter.MendatroryField = False
        Me.fndTransporter.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndTransporter.MyLinkLable1 = Me.lblTransporter
        Me.fndTransporter.MyLinkLable2 = Nothing
        Me.fndTransporter.MyReadOnly = False
        Me.fndTransporter.MyShowMasterFormButton = False
        Me.fndTransporter.Name = "fndTransporter"
        Me.fndTransporter.ReferenceFieldDesc = Nothing
        Me.fndTransporter.ReferenceFieldName = Nothing
        Me.fndTransporter.ReferenceTableName = Nothing
        Me.fndTransporter.Size = New System.Drawing.Size(200, 19)
        Me.fndTransporter.TabIndex = 7
        Me.fndTransporter.Value = ""
        '
        'lblTransporter
        '
        Me.lblTransporter.FieldName = Nothing
        Me.lblTransporter.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTransporter.Location = New System.Drawing.Point(376, 59)
        Me.lblTransporter.Name = "lblTransporter"
        Me.lblTransporter.Size = New System.Drawing.Size(67, 16)
        Me.lblTransporter.TabIndex = 12
        Me.lblTransporter.Text = "Transport Id"
        '
        'fndVehicle_id
        '
        Me.fndVehicle_id.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.fndVehicle_id.FieldName = Nothing
        Me.fndVehicle_id.Location = New System.Drawing.Point(159, 18)
        Me.fndVehicle_id.MendatroryField = False
        Me.fndVehicle_id.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndVehicle_id.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndVehicle_id.MyLinkLable1 = Me.rlblVehicleID
        Me.fndVehicle_id.MyLinkLable2 = Nothing
        Me.fndVehicle_id.MyMaxLength = 32767
        Me.fndVehicle_id.MyReadOnly = False
        Me.fndVehicle_id.Name = "fndVehicle_id"
        Me.fndVehicle_id.Size = New System.Drawing.Size(181, 18)
        Me.fndVehicle_id.TabIndex = 0
        Me.fndVehicle_id.TabStop = False
        Me.fndVehicle_id.Value = ""
        '
        'rtxtTranType
        '
        Me.rtxtTranType.CalculationExpression = Nothing
        Me.rtxtTranType.FieldCode = Nothing
        Me.rtxtTranType.FieldDesc = Nothing
        Me.rtxtTranType.FieldMaxLength = 0
        Me.rtxtTranType.FieldName = Nothing
        Me.rtxtTranType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtxtTranType.isCalculatedField = False
        Me.rtxtTranType.IsSourceFromTable = False
        Me.rtxtTranType.IsSourceFromValueList = False
        Me.rtxtTranType.IsUnique = False
        Me.rtxtTranType.Location = New System.Drawing.Point(517, 99)
        Me.rtxtTranType.MaxLength = 11
        Me.rtxtTranType.MendatroryField = False
        Me.rtxtTranType.MyLinkLable1 = Me.lblTranType
        Me.rtxtTranType.MyLinkLable2 = Nothing
        Me.rtxtTranType.Name = "rtxtTranType"
        Me.rtxtTranType.ReferenceFieldDesc = Nothing
        Me.rtxtTranType.ReferenceFieldName = Nothing
        Me.rtxtTranType.ReferenceTableName = Nothing
        Me.rtxtTranType.Size = New System.Drawing.Size(201, 18)
        Me.rtxtTranType.TabIndex = 11
        '
        'lblTranType
        '
        Me.lblTranType.FieldName = Nothing
        Me.lblTranType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTranType.Location = New System.Drawing.Point(376, 103)
        Me.lblTranType.Name = "lblTranType"
        Me.lblTranType.Size = New System.Drawing.Size(58, 16)
        Me.lblTranType.TabIndex = 20
        Me.lblTranType.Text = "Tran Type"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(792, 20)
        Me.RadMenu1.TabIndex = 0
        Me.RadMenu1.Text = "RadMenu1"
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnHistory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rbtnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rbtnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rbtnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(792, 454)
        Me.SplitContainer1.SplitterDistance = 417
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(792, 417)
        Me.RadPageView1.TabIndex = 58
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(89.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(771, 369)
        Me.RadPageViewPage1.Text = "Vehicle Details"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(88.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(771, 369)
        Me.RadPageViewPage2.Text = "Freight Details"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.chkIsAdditional)
        Me.RadGroupBox2.Controls.Add(Me.gv)
        Me.RadGroupBox2.Controls.Add(Me.GroupBox4)
        Me.RadGroupBox2.Controls.Add(Me.GroupBox3)
        Me.RadGroupBox2.Controls.Add(Me.GroupBox2)
        Me.RadGroupBox2.Controls.Add(Me.GroupBox1)
        Me.RadGroupBox2.Controls.Add(Me.rbtKmrange)
        Me.RadGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox2.HeaderText = "Basis of Freight Payments"
        Me.RadGroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Size = New System.Drawing.Size(771, 369)
        Me.RadGroupBox2.TabIndex = 68
        Me.RadGroupBox2.Text = "Basis of Freight Payments"
        '
        'chkIsAdditional
        '
        Me.chkIsAdditional.Location = New System.Drawing.Point(670, 23)
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
        Me.gv.Size = New System.Drawing.Size(495, 239)
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
        Me.GroupBox4.Size = New System.Drawing.Size(242, 102)
        Me.GroupBox4.TabIndex = 93
        Me.GroupBox4.TabStop = False
        '
        'rbtndiesel
        '
        Me.rbtndiesel.Location = New System.Drawing.Point(6, 12)
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
        Me.MyLabel8.Location = New System.Drawing.Point(6, 34)
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
        Me.txtchrg.Location = New System.Drawing.Point(119, 32)
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
        Me.MyLabel9.Location = New System.Drawing.Point(6, 57)
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
        Me.txtavgkm.Location = New System.Drawing.Point(119, 55)
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
        Me.MyLabel10.Location = New System.Drawing.Point(6, 80)
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
        Me.txtdiesel.Location = New System.Drawing.Point(119, 78)
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
        Me.GroupBox3.Location = New System.Drawing.Point(12, 112)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(241, 80)
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
        RadListDataItem6.Text = "Day"
        RadListDataItem7.Text = "Month"
        RadListDataItem8.Text = "Year"
        Me.cmbRentalType.Items.Add(RadListDataItem6)
        Me.cmbRentalType.Items.Add(RadListDataItem7)
        Me.cmbRentalType.Items.Add(RadListDataItem8)
        Me.cmbRentalType.Location = New System.Drawing.Point(95, 35)
        Me.cmbRentalType.MendatroryField = True
        Me.cmbRentalType.MyLinkLable1 = Nothing
        Me.cmbRentalType.MyLinkLable2 = Nothing
        Me.cmbRentalType.Name = "cmbRentalType"
        Me.cmbRentalType.ReferenceFieldDesc = Nothing
        Me.cmbRentalType.ReferenceFieldName = Nothing
        Me.cmbRentalType.ReferenceTableName = Nothing
        Me.cmbRentalType.Size = New System.Drawing.Size(139, 18)
        Me.cmbRentalType.TabIndex = 85
        '
        'rbtnrental
        '
        Me.rbtnrental.Location = New System.Drawing.Point(12, 12)
        Me.rbtnrental.MyLinkLable1 = Nothing
        Me.rbtnrental.MyLinkLable2 = Nothing
        Me.rbtnrental.Name = "rbtnrental"
        Me.rbtnrental.Size = New System.Drawing.Size(97, 18)
        Me.rbtnrental.TabIndex = 4
        Me.rbtnrental.Tag1 = Nothing
        Me.rbtnrental.Text = "On Rental Basis"
        '
        'lblRentalType
        '
        Me.lblRentalType.FieldName = Nothing
        Me.lblRentalType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRentalType.Location = New System.Drawing.Point(10, 35)
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
        Me.txtRentalAmt.Location = New System.Drawing.Point(95, 55)
        Me.txtRentalAmt.MendatroryField = False
        Me.txtRentalAmt.MyLinkLable1 = Nothing
        Me.txtRentalAmt.MyLinkLable2 = Nothing
        Me.txtRentalAmt.Name = "txtRentalAmt"
        Me.txtRentalAmt.ReferenceFieldDesc = Nothing
        Me.txtRentalAmt.ReferenceFieldName = Nothing
        Me.txtRentalAmt.ReferenceTableName = Nothing
        Me.txtRentalAmt.Size = New System.Drawing.Size(140, 20)
        Me.txtRentalAmt.TabIndex = 88
        Me.txtRentalAmt.Text = "0"
        Me.txtRentalAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtRentalAmt.Value = 0R
        '
        'lblRentalAmount
        '
        Me.lblRentalAmount.FieldName = Nothing
        Me.lblRentalAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRentalAmount.Location = New System.Drawing.Point(9, 55)
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
        Me.GroupBox2.Location = New System.Drawing.Point(12, 186)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(240, 56)
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
        Me.txt_ltr.Location = New System.Drawing.Point(61, 32)
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
        Me.MyLabel7.Location = New System.Drawing.Point(10, 33)
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
        RadListDataItem1.Text = "LTR"
        RadListDataItem2.Text = "KG"
        Me.cmbLtrKG.Items.Add(RadListDataItem1)
        Me.cmbLtrKG.Items.Add(RadListDataItem2)
        Me.cmbLtrKG.Location = New System.Drawing.Point(158, 33)
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
        Me.rbtnrateltr.Location = New System.Drawing.Point(10, 14)
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
        Me.GroupBox1.Location = New System.Drawing.Point(12, 236)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(241, 53)
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
        Me.txt_km.Location = New System.Drawing.Point(105, 29)
        Me.txt_km.MendatroryField = False
        Me.txt_km.MyLinkLable1 = Me.MyLabel6
        Me.txt_km.MyLinkLable2 = Nothing
        Me.txt_km.Name = "txt_km"
        Me.txt_km.ReferenceFieldDesc = Nothing
        Me.txt_km.ReferenceFieldName = Nothing
        Me.txt_km.ReferenceTableName = Nothing
        Me.txt_km.Size = New System.Drawing.Size(128, 20)
        Me.txt_km.TabIndex = 12
        Me.txt_km.Text = "0"
        Me.txt_km.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txt_km.Value = 0R
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(8, 32)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(77, 16)
        Me.MyLabel6.TabIndex = 64
        Me.MyLabel6.Text = "Rate Per K.M."
        '
        'rbtnratekm
        '
        Me.rbtnratekm.Location = New System.Drawing.Point(12, 12)
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
        'btnHistory
        '
        Me.btnHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnHistory.Location = New System.Drawing.Point(163, 8)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Size = New System.Drawing.Size(68, 18)
        Me.btnHistory.TabIndex = 37
        Me.btnHistory.Text = "&History"
        '
        'frmVehicleMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 474)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmVehicleMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Vehicle Master"
        CType(Me.rlblVehicleID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rlblModel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rlbltype, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rlblNumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rlblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtxtModel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtxtNumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtxtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtndepot, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnHire, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rlblVehicleRegistrationNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtxtVehicle_registration_No, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rlblVehicleChechisNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtxtvehicle_Chechis_No, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.txtVehicleWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlmt.ResumeLayout(False)
        Me.pnlmt.PerformLayout()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtmtvalue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtmtcapacity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSequenceNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtCrateCapacity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtxtCapacity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCapacity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRoadTax_valid_till, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRoadtax_valid_from, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPollutonchk_valid_till, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPollutionchk_valid_frm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFitness_valid_till, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFitness_valid_from, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblInsurance_valid_till, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblInsurance_valid_from, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRoad_tax_valid_till, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPollution_valid_till, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFitness_valid_till, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtInsurance_valid_till, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRoad_tax_valid_from, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPollution_valid_from, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFitness_valid_from, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtInsurance_valid_from, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVehicleName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.llVehicleNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEngineNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblVehicleBrand, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRegsteredOn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtxtlocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtxtvehicleNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtxtvehicleName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtxtengineno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtxtRegistredOn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtxtvehiclebrand, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTransporter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtxtTranType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTranType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
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
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnratekm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtKmrange, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rbtnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents rtxtModel As common.Controls.MyTextBox
    Friend WithEvents rtxtNumber As common.Controls.MyTextBox
    Friend WithEvents rtxtDescription As common.Controls.MyTextBox
    Friend WithEvents rbtndepot As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnHire As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents rbtnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents rbtnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents rtxtVehicle_registration_No As common.Controls.MyTextBox
    Friend WithEvents rtxtvehicle_Chechis_No As common.Controls.MyTextBox
    Friend WithEvents ToolTip_Vehicle_Master As System.Windows.Forms.ToolTip
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem_Import As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem_Export As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem_Close As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents rtxtTranType As common.Controls.MyTextBox
    Friend WithEvents rlblVehicleID As common.Controls.MyLabel
    Friend WithEvents rlblModel As common.Controls.MyLabel
    Friend WithEvents rlbltype As common.Controls.MyLabel
    Friend WithEvents rlblNumber As common.Controls.MyLabel
    Friend WithEvents rlblDescription As common.Controls.MyLabel
    Friend WithEvents rlblVehicleRegistrationNo As common.Controls.MyLabel
    Friend WithEvents rlblVehicleChechisNo As common.Controls.MyLabel
    Friend WithEvents lblCapacity As common.Controls.MyLabel
    Friend WithEvents lblTranType As common.Controls.MyLabel
    Friend WithEvents lblTransporter As common.Controls.MyLabel
    Friend WithEvents fndVehicle_id As common.UserControls.txtNavigator
    Friend WithEvents fndTransporter As common.UserControls.txtFinder
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents rtxtengineno As common.Controls.MyTextBox
    Friend WithEvents rtxtvehiclebrand As common.Controls.MyTextBox
    Friend WithEvents rtxtlocation As common.Controls.MyTextBox
    Friend WithEvents rtxtvehicleNo As common.Controls.MyTextBox
    Friend WithEvents rtxtvehicleName As common.Controls.MyTextBox
    Friend WithEvents rtxtRegistredOn As common.Controls.MyTextBox
    Friend WithEvents llVehicleNo As common.Controls.MyLabel
    Friend WithEvents lblEngineNo As common.Controls.MyLabel
    Friend WithEvents LblVehicleBrand As common.Controls.MyLabel
    Friend WithEvents lblRegsteredOn As common.Controls.MyLabel
    Friend WithEvents lblVehicleName As common.Controls.MyLabel
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents txtRoad_tax_valid_till As common.Controls.MyDateTimePicker
    Friend WithEvents txtPollution_valid_till As common.Controls.MyDateTimePicker
    Friend WithEvents txtFitness_valid_till As common.Controls.MyDateTimePicker
    Friend WithEvents txtInsurance_valid_till As common.Controls.MyDateTimePicker
    Friend WithEvents txtRoad_tax_valid_from As common.Controls.MyDateTimePicker
    Friend WithEvents txtPollution_valid_from As common.Controls.MyDateTimePicker
    Friend WithEvents txtFitness_valid_from As common.Controls.MyDateTimePicker
    Friend WithEvents txtInsurance_valid_from As common.Controls.MyDateTimePicker
    Friend WithEvents lblInsurance_valid_till As common.Controls.MyLabel
    Friend WithEvents lblInsurance_valid_from As common.Controls.MyLabel
    Friend WithEvents lblPollutionchk_valid_frm As common.Controls.MyLabel
    Friend WithEvents lblFitness_valid_till As common.Controls.MyLabel
    Friend WithEvents lblFitness_valid_from As common.Controls.MyLabel
    Friend WithEvents lblRoadTax_valid_till As common.Controls.MyLabel
    Friend WithEvents lblRoadtax_valid_from As common.Controls.MyLabel
    Friend WithEvents lblPollutonchk_valid_till As common.Controls.MyLabel
    Friend WithEvents rtxtCapacity As common.MyNumBox
    Friend WithEvents TxtCrateCapacity As common.MyNumBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents chkIsAdditional As common.Controls.MyCheckBox
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents rbtndiesel As common.Controls.MyCheckBox
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents txtchrg As common.MyNumBox
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents txtavgkm As common.MyNumBox
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents txtdiesel As common.MyNumBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbRentalType As common.Controls.MyComboBox
    Friend WithEvents rbtnrental As common.Controls.MyCheckBox
    Friend WithEvents lblRentalType As common.Controls.MyLabel
    Friend WithEvents txtRentalAmt As common.MyNumBox
    Friend WithEvents lblRentalAmount As common.Controls.MyLabel
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txt_ltr As common.MyNumBox
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents cmbLtrKG As common.Controls.MyComboBox
    Friend WithEvents rbtnrateltr As common.Controls.MyCheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txt_km As common.MyNumBox
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents rbtnratekm As common.Controls.MyCheckBox
    Friend WithEvents rbtKmrange As common.Controls.MyCheckBox
    Friend WithEvents txtSequenceNo As common.MyNumBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents fndemployee As common.UserControls.txtFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtmtvalue As common.MyNumBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtmtcapacity As common.MyNumBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents pnlmt As System.Windows.Forms.Panel
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents txtVehicleWeight As common.MyNumBox
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents btnHistory As RadButton
End Class

