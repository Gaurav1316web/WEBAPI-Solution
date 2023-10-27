<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCostMaintainance
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmCostMaintainance))
        Me.rdgpbxcustomeraccountset = New Telerik.WinControls.UI.RadGroupBox
        Me.txtAverageCost_val = New common.MyNumBox
        Me.AverageCost = New common.Controls.MyLabel
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.MyLabel9 = New common.Controls.MyLabel
        Me.MyLabel6 = New common.Controls.MyLabel
        Me.txtActualCostVal = New common.MyNumBox
        Me.MyLabel5 = New common.Controls.MyLabel
        Me.MyLabel8 = New common.Controls.MyLabel
        Me.txtToolCost_A_val = New common.MyNumBox
        Me.MyLabel4 = New common.Controls.MyLabel
        Me.txtSubcontractCost_A_VAl = New common.MyNumBox
        Me.MyLabel3 = New common.Controls.MyLabel
        Me.txtToolType = New common.Controls.MyTextBox
        Me.MyLabel20 = New common.Controls.MyLabel
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.fndToolType = New common.UserControls.txtFinder
        Me.MyLabel7 = New common.Controls.MyLabel
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.txtMaterialCost_A = New common.MyNumBox
        Me.txtSetupCost_A_Val = New common.MyNumBox
        Me.txtToolCost_A = New common.MyNumBox
        Me.txtOnHandCost_A_val = New common.MyNumBox
        Me.txtPackagingCost_A = New common.MyNumBox
        Me.txtLabourCost_A_val = New common.MyNumBox
        Me.txtTotalActualCost = New common.MyNumBox
        Me.txtPackagingCost_A_val = New common.MyNumBox
        Me.txtLabourCost_A = New common.MyNumBox
        Me.txtMaterialCost_A_Val = New common.MyNumBox
        Me.txtSubcontractCost_A = New common.MyNumBox
        Me.txtSetupCost_A = New common.MyNumBox
        Me.txtOnHandCost_A = New common.MyNumBox
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.MaterialCost = New common.Controls.MyLabel
        Me.PackagingCost = New common.Controls.MyLabel
        Me.ToolCost = New common.Controls.MyLabel
        Me.LabourCost = New common.Controls.MyLabel
        Me.TotalStandardCost = New common.Controls.MyLabel
        Me.OverheadCost = New common.Controls.MyLabel
        Me.SubcontractCost = New common.Controls.MyLabel
        Me.SetupCost = New common.Controls.MyLabel
        Me.txtMaterialCost = New common.MyNumBox
        Me.txtToolCost = New common.MyNumBox
        Me.txtPackagingCost = New common.MyNumBox
        Me.txtTotalStandardCost = New common.MyNumBox
        Me.txtLabourCost = New common.MyNumBox
        Me.txtSubcontractCost = New common.MyNumBox
        Me.txtOverheadCost = New common.MyNumBox
        Me.txtSetupCost = New common.MyNumBox
        Me.fndItemCode = New common.UserControls.txtFinder
        Me.fnduom = New common.UserControls.txtFinder
        Me.MyLabel13 = New common.Controls.MyLabel
        Me.QuantityOnHand = New common.Controls.MyLabel
        Me.txtuom = New common.Controls.MyTextBox
        Me.rdlblAccountsetcode = New common.Controls.MyLabel
        Me.txtdescription = New common.Controls.MyTextBox
        Me.txtQtyOnHand = New common.MyNumBox
        Me.rdbtnnew = New Telerik.WinControls.UI.RadButton
        Me.txtAverageCost = New common.MyNumBox
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.btnDelete = New Telerik.WinControls.UI.RadButton
        Me.rdbtnsave = New Telerik.WinControls.UI.RadButton
        Me.rdbtnclose = New Telerik.WinControls.UI.RadButton
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem
        Me.RMImport = New Telerik.WinControls.UI.RadMenuItem
        Me.RMExport = New Telerik.WinControls.UI.RadMenuItem
        CType(Me.rdgpbxcustomeraccountset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.rdgpbxcustomeraccountset.SuspendLayout()
        CType(Me.txtAverageCost_val, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AverageCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtActualCostVal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToolCost_A_val, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSubcontractCost_A_VAl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToolType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMaterialCost_A, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSetupCost_A_Val, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToolCost_A, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOnHandCost_A_val, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPackagingCost_A, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLabourCost_A_val, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalActualCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPackagingCost_A_val, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLabourCost_A, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMaterialCost_A_Val, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSubcontractCost_A, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSetupCost_A, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOnHandCost_A, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.MaterialCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PackagingCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToolCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LabourCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TotalStandardCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OverheadCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SubcontractCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SetupCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMaterialCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToolCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPackagingCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalStandardCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLabourCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSubcontractCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOverheadCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSetupCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.QuantityOnHand, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtuom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlblAccountsetcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtQtyOnHand, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAverageCost, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'rdgpbxcustomeraccountset
        '
        Me.rdgpbxcustomeraccountset.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.txtAverageCost_val)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.RadGroupBox2)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.RadGroupBox1)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.fndItemCode)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.fnduom)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.QuantityOnHand)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.txtuom)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.MyLabel13)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.rdlblAccountsetcode)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.AverageCost)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.txtdescription)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.txtQtyOnHand)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.rdbtnnew)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.txtAverageCost)
        Me.rdgpbxcustomeraccountset.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rdgpbxcustomeraccountset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdgpbxcustomeraccountset.HeaderText = ""
        Me.rdgpbxcustomeraccountset.Location = New System.Drawing.Point(0, 0)
        Me.rdgpbxcustomeraccountset.Name = "rdgpbxcustomeraccountset"
        Me.rdgpbxcustomeraccountset.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.rdgpbxcustomeraccountset.Size = New System.Drawing.Size(689, 466)
        Me.rdgpbxcustomeraccountset.TabIndex = 0
        '
        'txtAverageCost_val
        '
        Me.txtAverageCost_val.BackColor = System.Drawing.Color.White
        Me.txtAverageCost_val.DecimalPlaces = 2
        Me.txtAverageCost_val.Location = New System.Drawing.Point(526, 331)
        Me.txtAverageCost_val.MendatroryField = False
        Me.txtAverageCost_val.MyLinkLable1 = Me.AverageCost
        Me.txtAverageCost_val.MyLinkLable2 = Nothing
        Me.txtAverageCost_val.Name = "txtAverageCost_val"
        Me.txtAverageCost_val.Size = New System.Drawing.Size(81, 20)
        Me.txtAverageCost_val.TabIndex = 8
        Me.txtAverageCost_val.TabStop = False
        Me.txtAverageCost_val.Text = "0"
        Me.txtAverageCost_val.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtAverageCost_val.Value = 0
        Me.txtAverageCost_val.Visible = False
        '
        'AverageCost
        '
        Me.AverageCost.Location = New System.Drawing.Point(312, 336)
        Me.AverageCost.Name = "AverageCost"
        Me.AverageCost.Size = New System.Drawing.Size(73, 18)
        Me.AverageCost.TabIndex = 9
        Me.AverageCost.Text = "Average Cost"
        Me.AverageCost.Visible = False
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.MyLabel9)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel6)
        Me.RadGroupBox2.Controls.Add(Me.txtActualCostVal)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel8)
        Me.RadGroupBox2.Controls.Add(Me.txtToolCost_A_val)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel4)
        Me.RadGroupBox2.Controls.Add(Me.txtSubcontractCost_A_VAl)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel5)
        Me.RadGroupBox2.Controls.Add(Me.txtToolType)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox2.Controls.Add(Me.fndToolType)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel20)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel7)
        Me.RadGroupBox2.Controls.Add(Me.txtMaterialCost_A)
        Me.RadGroupBox2.Controls.Add(Me.txtSetupCost_A_Val)
        Me.RadGroupBox2.Controls.Add(Me.txtToolCost_A)
        Me.RadGroupBox2.Controls.Add(Me.txtOnHandCost_A_val)
        Me.RadGroupBox2.Controls.Add(Me.txtPackagingCost_A)
        Me.RadGroupBox2.Controls.Add(Me.txtLabourCost_A_val)
        Me.RadGroupBox2.Controls.Add(Me.txtTotalActualCost)
        Me.RadGroupBox2.Controls.Add(Me.txtPackagingCost_A_val)
        Me.RadGroupBox2.Controls.Add(Me.txtLabourCost_A)
        Me.RadGroupBox2.Controls.Add(Me.txtMaterialCost_A_Val)
        Me.RadGroupBox2.Controls.Add(Me.txtSubcontractCost_A)
        Me.RadGroupBox2.Controls.Add(Me.txtSetupCost_A)
        Me.RadGroupBox2.Controls.Add(Me.txtOnHandCost_A)
        Me.RadGroupBox2.HeaderText = "Actual Cost"
        Me.RadGroupBox2.Location = New System.Drawing.Point(300, 64)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(320, 262)
        Me.RadGroupBox2.TabIndex = 5
        Me.RadGroupBox2.Text = "Actual Cost"
        '
        'MyLabel9
        '
        Me.MyLabel9.Location = New System.Drawing.Point(4, 67)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(72, 18)
        Me.MyLabel9.TabIndex = 24
        Me.MyLabel9.Text = "Material Cost"
        '
        'MyLabel6
        '
        Me.MyLabel6.Location = New System.Drawing.Point(4, 88)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(83, 18)
        Me.MyLabel6.TabIndex = 23
        Me.MyLabel6.Text = "Packaging Cost"
        '
        'txtActualCostVal
        '
        Me.txtActualCostVal.BackColor = System.Drawing.Color.White
        Me.txtActualCostVal.DecimalPlaces = 2
        Me.txtActualCostVal.Location = New System.Drawing.Point(227, 227)
        Me.txtActualCostVal.MendatroryField = False
        Me.txtActualCostVal.MyLinkLable1 = Me.MyLabel5
        Me.txtActualCostVal.MyLinkLable2 = Nothing
        Me.txtActualCostVal.Name = "txtActualCostVal"
        Me.txtActualCostVal.Size = New System.Drawing.Size(81, 20)
        Me.txtActualCostVal.TabIndex = 16
        Me.txtActualCostVal.Text = "0"
        Me.txtActualCostVal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtActualCostVal.Value = 0
        Me.txtActualCostVal.Visible = False
        '
        'MyLabel5
        '
        Me.MyLabel5.Location = New System.Drawing.Point(4, 228)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(39, 18)
        Me.MyLabel5.TabIndex = 17
        Me.MyLabel5.Text = "TOTAL"
        '
        'MyLabel8
        '
        Me.MyLabel8.Location = New System.Drawing.Point(4, 205)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(54, 18)
        Me.MyLabel8.TabIndex = 18
        Me.MyLabel8.Text = "Tool Cost"
        '
        'txtToolCost_A_val
        '
        Me.txtToolCost_A_val.BackColor = System.Drawing.Color.White
        Me.txtToolCost_A_val.DecimalPlaces = 2
        Me.txtToolCost_A_val.Location = New System.Drawing.Point(227, 204)
        Me.txtToolCost_A_val.MendatroryField = False
        Me.txtToolCost_A_val.MyLinkLable1 = Me.MyLabel8
        Me.txtToolCost_A_val.MyLinkLable2 = Nothing
        Me.txtToolCost_A_val.Name = "txtToolCost_A_val"
        Me.txtToolCost_A_val.Size = New System.Drawing.Size(81, 20)
        Me.txtToolCost_A_val.TabIndex = 14
        Me.txtToolCost_A_val.Text = "0"
        Me.txtToolCost_A_val.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtToolCost_A_val.Value = 0
        Me.txtToolCost_A_val.Visible = False
        '
        'MyLabel4
        '
        Me.MyLabel4.Location = New System.Drawing.Point(4, 134)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(66, 18)
        Me.MyLabel4.TabIndex = 21
        Me.MyLabel4.Text = "Labour Cost"
        '
        'txtSubcontractCost_A_VAl
        '
        Me.txtSubcontractCost_A_VAl.BackColor = System.Drawing.Color.White
        Me.txtSubcontractCost_A_VAl.DecimalPlaces = 2
        Me.txtSubcontractCost_A_VAl.Location = New System.Drawing.Point(227, 180)
        Me.txtSubcontractCost_A_VAl.MendatroryField = False
        Me.txtSubcontractCost_A_VAl.MyLinkLable1 = Me.MyLabel3
        Me.txtSubcontractCost_A_VAl.MyLinkLable2 = Nothing
        Me.txtSubcontractCost_A_VAl.Name = "txtSubcontractCost_A_VAl"
        Me.txtSubcontractCost_A_VAl.Size = New System.Drawing.Size(81, 20)
        Me.txtSubcontractCost_A_VAl.TabIndex = 12
        Me.txtSubcontractCost_A_VAl.Text = "0"
        Me.txtSubcontractCost_A_VAl.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSubcontractCost_A_VAl.Value = 0
        Me.txtSubcontractCost_A_VAl.Visible = False
        '
        'MyLabel3
        '
        Me.MyLabel3.Location = New System.Drawing.Point(4, 181)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(92, 18)
        Me.MyLabel3.TabIndex = 19
        Me.MyLabel3.Text = "Subcontract Cost"
        '
        'txtToolType
        '
        Me.txtToolType.Location = New System.Drawing.Point(94, 41)
        Me.txtToolType.MaxLength = 50
        Me.txtToolType.MendatroryField = False
        Me.txtToolType.MyLinkLable1 = Me.MyLabel20
        Me.txtToolType.MyLinkLable2 = Nothing
        Me.txtToolType.Name = "txtToolType"
        Me.txtToolType.Size = New System.Drawing.Size(214, 20)
        Me.txtToolType.TabIndex = 1
        '
        'MyLabel20
        '
        Me.MyLabel20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel20.Location = New System.Drawing.Point(4, 45)
        Me.MyLabel20.Name = "MyLabel20"
        Me.MyLabel20.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel20.TabIndex = 25
        Me.MyLabel20.Text = "Description"
        '
        'MyLabel2
        '
        Me.MyLabel2.Location = New System.Drawing.Point(4, 159)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(80, 18)
        Me.MyLabel2.TabIndex = 20
        Me.MyLabel2.Text = "Overhead Cost"
        '
        'fndToolType
        '
        Me.fndToolType.Location = New System.Drawing.Point(94, 18)
        Me.fndToolType.MendatroryField = False
        Me.fndToolType.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndToolType.MyLinkLable1 = Me.MyLabel7
        Me.fndToolType.MyLinkLable2 = Nothing
        Me.fndToolType.MyReadOnly = False
        Me.fndToolType.Name = "fndToolType"
        Me.fndToolType.Size = New System.Drawing.Size(214, 19)
        Me.fndToolType.TabIndex = 0
        Me.fndToolType.Value = ""
        '
        'MyLabel7
        '
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(4, 23)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(45, 16)
        Me.MyLabel7.TabIndex = 26
        Me.MyLabel7.Text = "MO No."
        '
        'MyLabel1
        '
        Me.MyLabel1.Location = New System.Drawing.Point(4, 110)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(62, 18)
        Me.MyLabel1.TabIndex = 22
        Me.MyLabel1.Text = "SetUp Cost"
        '
        'txtMaterialCost_A
        '
        Me.txtMaterialCost_A.BackColor = System.Drawing.Color.White
        Me.txtMaterialCost_A.DecimalPlaces = 2
        Me.txtMaterialCost_A.Location = New System.Drawing.Point(95, 66)
        Me.txtMaterialCost_A.MendatroryField = False
        Me.txtMaterialCost_A.MyLinkLable1 = Me.MyLabel9
        Me.txtMaterialCost_A.MyLinkLable2 = Nothing
        Me.txtMaterialCost_A.Name = "txtMaterialCost_A"
        Me.txtMaterialCost_A.Size = New System.Drawing.Size(126, 20)
        Me.txtMaterialCost_A.TabIndex = 1
        Me.txtMaterialCost_A.Text = "0"
        Me.txtMaterialCost_A.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMaterialCost_A.Value = 0
        '
        'txtSetupCost_A_Val
        '
        Me.txtSetupCost_A_Val.BackColor = System.Drawing.Color.White
        Me.txtSetupCost_A_Val.DecimalPlaces = 2
        Me.txtSetupCost_A_Val.Location = New System.Drawing.Point(227, 109)
        Me.txtSetupCost_A_Val.MendatroryField = False
        Me.txtSetupCost_A_Val.MyLinkLable1 = Me.MyLabel1
        Me.txtSetupCost_A_Val.MyLinkLable2 = Nothing
        Me.txtSetupCost_A_Val.Name = "txtSetupCost_A_Val"
        Me.txtSetupCost_A_Val.Size = New System.Drawing.Size(81, 20)
        Me.txtSetupCost_A_Val.TabIndex = 6
        Me.txtSetupCost_A_Val.Text = "0"
        Me.txtSetupCost_A_Val.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSetupCost_A_Val.Value = 0
        Me.txtSetupCost_A_Val.Visible = False
        '
        'txtToolCost_A
        '
        Me.txtToolCost_A.BackColor = System.Drawing.Color.White
        Me.txtToolCost_A.DecimalPlaces = 2
        Me.txtToolCost_A.Location = New System.Drawing.Point(95, 207)
        Me.txtToolCost_A.MendatroryField = False
        Me.txtToolCost_A.MyLinkLable1 = Me.MyLabel8
        Me.txtToolCost_A.MyLinkLable2 = Nothing
        Me.txtToolCost_A.Name = "txtToolCost_A"
        Me.txtToolCost_A.Size = New System.Drawing.Size(126, 20)
        Me.txtToolCost_A.TabIndex = 13
        Me.txtToolCost_A.Text = "0"
        Me.txtToolCost_A.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtToolCost_A.Value = 0
        '
        'txtOnHandCost_A_val
        '
        Me.txtOnHandCost_A_val.BackColor = System.Drawing.Color.White
        Me.txtOnHandCost_A_val.DecimalPlaces = 2
        Me.txtOnHandCost_A_val.Location = New System.Drawing.Point(227, 156)
        Me.txtOnHandCost_A_val.MendatroryField = False
        Me.txtOnHandCost_A_val.MyLinkLable1 = Me.MyLabel2
        Me.txtOnHandCost_A_val.MyLinkLable2 = Nothing
        Me.txtOnHandCost_A_val.Name = "txtOnHandCost_A_val"
        Me.txtOnHandCost_A_val.Size = New System.Drawing.Size(81, 20)
        Me.txtOnHandCost_A_val.TabIndex = 10
        Me.txtOnHandCost_A_val.Text = "0"
        Me.txtOnHandCost_A_val.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtOnHandCost_A_val.Value = 0
        Me.txtOnHandCost_A_val.Visible = False
        '
        'txtPackagingCost_A
        '
        Me.txtPackagingCost_A.BackColor = System.Drawing.Color.White
        Me.txtPackagingCost_A.DecimalPlaces = 2
        Me.txtPackagingCost_A.Location = New System.Drawing.Point(95, 89)
        Me.txtPackagingCost_A.MendatroryField = False
        Me.txtPackagingCost_A.MyLinkLable1 = Me.MyLabel6
        Me.txtPackagingCost_A.MyLinkLable2 = Nothing
        Me.txtPackagingCost_A.Name = "txtPackagingCost_A"
        Me.txtPackagingCost_A.Size = New System.Drawing.Size(126, 20)
        Me.txtPackagingCost_A.TabIndex = 3
        Me.txtPackagingCost_A.Text = "0"
        Me.txtPackagingCost_A.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtPackagingCost_A.Value = 0
        '
        'txtLabourCost_A_val
        '
        Me.txtLabourCost_A_val.BackColor = System.Drawing.Color.White
        Me.txtLabourCost_A_val.DecimalPlaces = 2
        Me.txtLabourCost_A_val.Location = New System.Drawing.Point(227, 132)
        Me.txtLabourCost_A_val.MendatroryField = False
        Me.txtLabourCost_A_val.MyLinkLable1 = Me.MyLabel4
        Me.txtLabourCost_A_val.MyLinkLable2 = Nothing
        Me.txtLabourCost_A_val.Name = "txtLabourCost_A_val"
        Me.txtLabourCost_A_val.Size = New System.Drawing.Size(81, 20)
        Me.txtLabourCost_A_val.TabIndex = 8
        Me.txtLabourCost_A_val.Text = "0"
        Me.txtLabourCost_A_val.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtLabourCost_A_val.Value = 0
        Me.txtLabourCost_A_val.Visible = False
        '
        'txtTotalActualCost
        '
        Me.txtTotalActualCost.BackColor = System.Drawing.Color.White
        Me.txtTotalActualCost.DecimalPlaces = 2
        Me.txtTotalActualCost.Location = New System.Drawing.Point(95, 230)
        Me.txtTotalActualCost.MendatroryField = False
        Me.txtTotalActualCost.MyLinkLable1 = Me.MyLabel5
        Me.txtTotalActualCost.MyLinkLable2 = Nothing
        Me.txtTotalActualCost.Name = "txtTotalActualCost"
        Me.txtTotalActualCost.Size = New System.Drawing.Size(126, 20)
        Me.txtTotalActualCost.TabIndex = 15
        Me.txtTotalActualCost.Text = "0"
        Me.txtTotalActualCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTotalActualCost.Value = 0
        '
        'txtPackagingCost_A_val
        '
        Me.txtPackagingCost_A_val.BackColor = System.Drawing.Color.White
        Me.txtPackagingCost_A_val.DecimalPlaces = 2
        Me.txtPackagingCost_A_val.Location = New System.Drawing.Point(227, 87)
        Me.txtPackagingCost_A_val.MendatroryField = False
        Me.txtPackagingCost_A_val.MyLinkLable1 = Nothing
        Me.txtPackagingCost_A_val.MyLinkLable2 = Nothing
        Me.txtPackagingCost_A_val.Name = "txtPackagingCost_A_val"
        Me.txtPackagingCost_A_val.Size = New System.Drawing.Size(81, 20)
        Me.txtPackagingCost_A_val.TabIndex = 4
        Me.txtPackagingCost_A_val.Text = "0"
        Me.txtPackagingCost_A_val.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtPackagingCost_A_val.Value = 0
        Me.txtPackagingCost_A_val.Visible = False
        '
        'txtLabourCost_A
        '
        Me.txtLabourCost_A.BackColor = System.Drawing.Color.White
        Me.txtLabourCost_A.DecimalPlaces = 2
        Me.txtLabourCost_A.Location = New System.Drawing.Point(95, 136)
        Me.txtLabourCost_A.MendatroryField = False
        Me.txtLabourCost_A.MyLinkLable1 = Me.MyLabel4
        Me.txtLabourCost_A.MyLinkLable2 = Nothing
        Me.txtLabourCost_A.Name = "txtLabourCost_A"
        Me.txtLabourCost_A.Size = New System.Drawing.Size(126, 20)
        Me.txtLabourCost_A.TabIndex = 7
        Me.txtLabourCost_A.Text = "0"
        Me.txtLabourCost_A.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtLabourCost_A.Value = 0
        '
        'txtMaterialCost_A_Val
        '
        Me.txtMaterialCost_A_Val.BackColor = System.Drawing.Color.White
        Me.txtMaterialCost_A_Val.DecimalPlaces = 2
        Me.txtMaterialCost_A_Val.Location = New System.Drawing.Point(227, 65)
        Me.txtMaterialCost_A_Val.MendatroryField = False
        Me.txtMaterialCost_A_Val.MyLinkLable1 = Me.MyLabel9
        Me.txtMaterialCost_A_Val.MyLinkLable2 = Nothing
        Me.txtMaterialCost_A_Val.Name = "txtMaterialCost_A_Val"
        Me.txtMaterialCost_A_Val.Size = New System.Drawing.Size(81, 20)
        Me.txtMaterialCost_A_Val.TabIndex = 2
        Me.txtMaterialCost_A_Val.Text = "0"
        Me.txtMaterialCost_A_Val.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMaterialCost_A_Val.Value = 0
        Me.txtMaterialCost_A_Val.Visible = False
        '
        'txtSubcontractCost_A
        '
        Me.txtSubcontractCost_A.BackColor = System.Drawing.Color.White
        Me.txtSubcontractCost_A.DecimalPlaces = 2
        Me.txtSubcontractCost_A.Location = New System.Drawing.Point(95, 183)
        Me.txtSubcontractCost_A.MendatroryField = False
        Me.txtSubcontractCost_A.MyLinkLable1 = Me.MyLabel3
        Me.txtSubcontractCost_A.MyLinkLable2 = Nothing
        Me.txtSubcontractCost_A.Name = "txtSubcontractCost_A"
        Me.txtSubcontractCost_A.Size = New System.Drawing.Size(126, 20)
        Me.txtSubcontractCost_A.TabIndex = 11
        Me.txtSubcontractCost_A.Text = "0"
        Me.txtSubcontractCost_A.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSubcontractCost_A.Value = 0
        '
        'txtSetupCost_A
        '
        Me.txtSetupCost_A.BackColor = System.Drawing.Color.White
        Me.txtSetupCost_A.DecimalPlaces = 2
        Me.txtSetupCost_A.Location = New System.Drawing.Point(95, 112)
        Me.txtSetupCost_A.MendatroryField = False
        Me.txtSetupCost_A.MyLinkLable1 = Me.MyLabel1
        Me.txtSetupCost_A.MyLinkLable2 = Nothing
        Me.txtSetupCost_A.Name = "txtSetupCost_A"
        Me.txtSetupCost_A.Size = New System.Drawing.Size(126, 20)
        Me.txtSetupCost_A.TabIndex = 5
        Me.txtSetupCost_A.Text = "0"
        Me.txtSetupCost_A.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSetupCost_A.Value = 0
        '
        'txtOnHandCost_A
        '
        Me.txtOnHandCost_A.BackColor = System.Drawing.Color.White
        Me.txtOnHandCost_A.DecimalPlaces = 2
        Me.txtOnHandCost_A.Location = New System.Drawing.Point(95, 159)
        Me.txtOnHandCost_A.MendatroryField = False
        Me.txtOnHandCost_A.MyLinkLable1 = Me.MyLabel2
        Me.txtOnHandCost_A.MyLinkLable2 = Nothing
        Me.txtOnHandCost_A.Name = "txtOnHandCost_A"
        Me.txtOnHandCost_A.Size = New System.Drawing.Size(126, 20)
        Me.txtOnHandCost_A.TabIndex = 9
        Me.txtOnHandCost_A.Text = "0"
        Me.txtOnHandCost_A.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtOnHandCost_A.Value = 0
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.MaterialCost)
        Me.RadGroupBox1.Controls.Add(Me.PackagingCost)
        Me.RadGroupBox1.Controls.Add(Me.ToolCost)
        Me.RadGroupBox1.Controls.Add(Me.LabourCost)
        Me.RadGroupBox1.Controls.Add(Me.TotalStandardCost)
        Me.RadGroupBox1.Controls.Add(Me.OverheadCost)
        Me.RadGroupBox1.Controls.Add(Me.SubcontractCost)
        Me.RadGroupBox1.Controls.Add(Me.SetupCost)
        Me.RadGroupBox1.Controls.Add(Me.txtMaterialCost)
        Me.RadGroupBox1.Controls.Add(Me.txtToolCost)
        Me.RadGroupBox1.Controls.Add(Me.txtPackagingCost)
        Me.RadGroupBox1.Controls.Add(Me.txtTotalStandardCost)
        Me.RadGroupBox1.Controls.Add(Me.txtLabourCost)
        Me.RadGroupBox1.Controls.Add(Me.txtSubcontractCost)
        Me.RadGroupBox1.Controls.Add(Me.txtOverheadCost)
        Me.RadGroupBox1.Controls.Add(Me.txtSetupCost)
        Me.RadGroupBox1.HeaderText = "Standard Cost"
        Me.RadGroupBox1.Location = New System.Drawing.Point(26, 64)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(268, 262)
        Me.RadGroupBox1.TabIndex = 3
        Me.RadGroupBox1.Text = "Standard Cost"
        '
        'MaterialCost
        '
        Me.MaterialCost.Location = New System.Drawing.Point(13, 71)
        Me.MaterialCost.Name = "MaterialCost"
        Me.MaterialCost.Size = New System.Drawing.Size(72, 18)
        Me.MaterialCost.TabIndex = 15
        Me.MaterialCost.Text = "Material Cost"
        '
        'PackagingCost
        '
        Me.PackagingCost.Location = New System.Drawing.Point(13, 92)
        Me.PackagingCost.Name = "PackagingCost"
        Me.PackagingCost.Size = New System.Drawing.Size(83, 18)
        Me.PackagingCost.TabIndex = 14
        Me.PackagingCost.Text = "Packaging Cost"
        '
        'ToolCost
        '
        Me.ToolCost.Location = New System.Drawing.Point(13, 209)
        Me.ToolCost.Name = "ToolCost"
        Me.ToolCost.Size = New System.Drawing.Size(54, 18)
        Me.ToolCost.TabIndex = 9
        Me.ToolCost.Text = "Tool Cost"
        '
        'LabourCost
        '
        Me.LabourCost.Location = New System.Drawing.Point(13, 138)
        Me.LabourCost.Name = "LabourCost"
        Me.LabourCost.Size = New System.Drawing.Size(66, 18)
        Me.LabourCost.TabIndex = 12
        Me.LabourCost.Text = "Labour Cost"
        '
        'TotalStandardCost
        '
        Me.TotalStandardCost.Location = New System.Drawing.Point(13, 232)
        Me.TotalStandardCost.Name = "TotalStandardCost"
        Me.TotalStandardCost.Size = New System.Drawing.Size(39, 18)
        Me.TotalStandardCost.TabIndex = 8
        Me.TotalStandardCost.Text = "TOTAL"
        '
        'OverheadCost
        '
        Me.OverheadCost.Location = New System.Drawing.Point(13, 163)
        Me.OverheadCost.Name = "OverheadCost"
        Me.OverheadCost.Size = New System.Drawing.Size(80, 18)
        Me.OverheadCost.TabIndex = 11
        Me.OverheadCost.Text = "Overhead Cost"
        '
        'SubcontractCost
        '
        Me.SubcontractCost.Location = New System.Drawing.Point(13, 185)
        Me.SubcontractCost.Name = "SubcontractCost"
        Me.SubcontractCost.Size = New System.Drawing.Size(92, 18)
        Me.SubcontractCost.TabIndex = 10
        Me.SubcontractCost.Text = "Subcontract Cost"
        '
        'SetupCost
        '
        Me.SetupCost.Location = New System.Drawing.Point(13, 114)
        Me.SetupCost.Name = "SetupCost"
        Me.SetupCost.Size = New System.Drawing.Size(62, 18)
        Me.SetupCost.TabIndex = 13
        Me.SetupCost.Text = "SetUp Cost"
        '
        'txtMaterialCost
        '
        Me.txtMaterialCost.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtMaterialCost.DecimalPlaces = 2
        Me.txtMaterialCost.Location = New System.Drawing.Point(104, 70)
        Me.txtMaterialCost.MendatroryField = True
        Me.txtMaterialCost.MyLinkLable1 = Me.MaterialCost
        Me.txtMaterialCost.MyLinkLable2 = Nothing
        Me.txtMaterialCost.Name = "txtMaterialCost"
        Me.txtMaterialCost.Size = New System.Drawing.Size(126, 20)
        Me.txtMaterialCost.TabIndex = 0
        Me.txtMaterialCost.Text = "0"
        Me.txtMaterialCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMaterialCost.Value = 0
        '
        'txtToolCost
        '
        Me.txtToolCost.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtToolCost.DecimalPlaces = 2
        Me.txtToolCost.Location = New System.Drawing.Point(104, 211)
        Me.txtToolCost.MendatroryField = True
        Me.txtToolCost.MyLinkLable1 = Me.ToolCost
        Me.txtToolCost.MyLinkLable2 = Nothing
        Me.txtToolCost.Name = "txtToolCost"
        Me.txtToolCost.Size = New System.Drawing.Size(126, 20)
        Me.txtToolCost.TabIndex = 6
        Me.txtToolCost.Text = "0"
        Me.txtToolCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtToolCost.Value = 0
        '
        'txtPackagingCost
        '
        Me.txtPackagingCost.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtPackagingCost.DecimalPlaces = 2
        Me.txtPackagingCost.Location = New System.Drawing.Point(104, 93)
        Me.txtPackagingCost.MendatroryField = True
        Me.txtPackagingCost.MyLinkLable1 = Me.PackagingCost
        Me.txtPackagingCost.MyLinkLable2 = Nothing
        Me.txtPackagingCost.Name = "txtPackagingCost"
        Me.txtPackagingCost.Size = New System.Drawing.Size(126, 20)
        Me.txtPackagingCost.TabIndex = 1
        Me.txtPackagingCost.Text = "0"
        Me.txtPackagingCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtPackagingCost.Value = 0
        '
        'txtTotalStandardCost
        '
        Me.txtTotalStandardCost.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtTotalStandardCost.DecimalPlaces = 2
        Me.txtTotalStandardCost.Location = New System.Drawing.Point(104, 234)
        Me.txtTotalStandardCost.MendatroryField = True
        Me.txtTotalStandardCost.MyLinkLable1 = Me.TotalStandardCost
        Me.txtTotalStandardCost.MyLinkLable2 = Nothing
        Me.txtTotalStandardCost.Name = "txtTotalStandardCost"
        Me.txtTotalStandardCost.ReadOnly = True
        Me.txtTotalStandardCost.Size = New System.Drawing.Size(126, 20)
        Me.txtTotalStandardCost.TabIndex = 7
        Me.txtTotalStandardCost.Text = "0"
        Me.txtTotalStandardCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTotalStandardCost.Value = 0
        '
        'txtLabourCost
        '
        Me.txtLabourCost.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtLabourCost.DecimalPlaces = 2
        Me.txtLabourCost.Location = New System.Drawing.Point(104, 140)
        Me.txtLabourCost.MendatroryField = True
        Me.txtLabourCost.MyLinkLable1 = Me.LabourCost
        Me.txtLabourCost.MyLinkLable2 = Nothing
        Me.txtLabourCost.Name = "txtLabourCost"
        Me.txtLabourCost.Size = New System.Drawing.Size(126, 20)
        Me.txtLabourCost.TabIndex = 3
        Me.txtLabourCost.Text = "0"
        Me.txtLabourCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtLabourCost.Value = 0
        '
        'txtSubcontractCost
        '
        Me.txtSubcontractCost.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtSubcontractCost.DecimalPlaces = 2
        Me.txtSubcontractCost.Location = New System.Drawing.Point(104, 187)
        Me.txtSubcontractCost.MendatroryField = True
        Me.txtSubcontractCost.MyLinkLable1 = Me.SubcontractCost
        Me.txtSubcontractCost.MyLinkLable2 = Nothing
        Me.txtSubcontractCost.Name = "txtSubcontractCost"
        Me.txtSubcontractCost.Size = New System.Drawing.Size(126, 20)
        Me.txtSubcontractCost.TabIndex = 5
        Me.txtSubcontractCost.Text = "0"
        Me.txtSubcontractCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSubcontractCost.Value = 0
        '
        'txtOverheadCost
        '
        Me.txtOverheadCost.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtOverheadCost.DecimalPlaces = 2
        Me.txtOverheadCost.Location = New System.Drawing.Point(104, 163)
        Me.txtOverheadCost.MendatroryField = True
        Me.txtOverheadCost.MyLinkLable1 = Me.OverheadCost
        Me.txtOverheadCost.MyLinkLable2 = Nothing
        Me.txtOverheadCost.Name = "txtOverheadCost"
        Me.txtOverheadCost.Size = New System.Drawing.Size(126, 20)
        Me.txtOverheadCost.TabIndex = 4
        Me.txtOverheadCost.Text = "0"
        Me.txtOverheadCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtOverheadCost.Value = 0
        '
        'txtSetupCost
        '
        Me.txtSetupCost.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtSetupCost.DecimalPlaces = 2
        Me.txtSetupCost.Location = New System.Drawing.Point(104, 116)
        Me.txtSetupCost.MendatroryField = True
        Me.txtSetupCost.MyLinkLable1 = Me.SetupCost
        Me.txtSetupCost.MyLinkLable2 = Nothing
        Me.txtSetupCost.Name = "txtSetupCost"
        Me.txtSetupCost.Size = New System.Drawing.Size(126, 20)
        Me.txtSetupCost.TabIndex = 2
        Me.txtSetupCost.Text = "0"
        Me.txtSetupCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSetupCost.Value = 0
        '
        'fndItemCode
        '
        Me.fndItemCode.Location = New System.Drawing.Point(107, 14)
        Me.fndItemCode.MendatroryField = True
        Me.fndItemCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndItemCode.MyLinkLable1 = Nothing
        Me.fndItemCode.MyLinkLable2 = Nothing
        Me.fndItemCode.MyReadOnly = False
        Me.fndItemCode.Name = "fndItemCode"
        Me.fndItemCode.Size = New System.Drawing.Size(125, 19)
        Me.fndItemCode.TabIndex = 0
        Me.fndItemCode.Value = ""
        '
        'fnduom
        '
        Me.fnduom.Location = New System.Drawing.Point(107, 38)
        Me.fnduom.MendatroryField = True
        Me.fnduom.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fnduom.MyLinkLable1 = Me.MyLabel13
        Me.fnduom.MyLinkLable2 = Nothing
        Me.fnduom.MyReadOnly = False
        Me.fnduom.Name = "fnduom"
        Me.fnduom.Size = New System.Drawing.Size(125, 19)
        Me.fnduom.TabIndex = 2
        Me.fnduom.Value = ""
        '
        'MyLabel13
        '
        Me.MyLabel13.Location = New System.Drawing.Point(26, 42)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(33, 18)
        Me.MyLabel13.TabIndex = 11
        Me.MyLabel13.Text = "UOM"
        '
        'QuantityOnHand
        '
        Me.QuantityOnHand.Location = New System.Drawing.Point(26, 336)
        Me.QuantityOnHand.Name = "QuantityOnHand"
        Me.QuantityOnHand.Size = New System.Drawing.Size(97, 18)
        Me.QuantityOnHand.TabIndex = 10
        Me.QuantityOnHand.Text = "Quantity On Hand"
        Me.QuantityOnHand.Visible = False
        '
        'txtuom
        '
        Me.txtuom.Location = New System.Drawing.Point(260, 37)
        Me.txtuom.MaxLength = 50
        Me.txtuom.MendatroryField = True
        Me.txtuom.MyLinkLable1 = Nothing
        Me.txtuom.MyLinkLable2 = Nothing
        Me.txtuom.Name = "txtuom"
        Me.txtuom.Size = New System.Drawing.Size(300, 20)
        Me.txtuom.TabIndex = 8
        Me.txtuom.TabStop = False
        '
        'rdlblAccountsetcode
        '
        Me.rdlblAccountsetcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.rdlblAccountsetcode.Location = New System.Drawing.Point(26, 17)
        Me.rdlblAccountsetcode.Name = "rdlblAccountsetcode"
        Me.rdlblAccountsetcode.Size = New System.Drawing.Size(61, 16)
        Me.rdlblAccountsetcode.TabIndex = 12
        Me.rdlblAccountsetcode.Text = "Item Code"
        '
        'txtdescription
        '
        Me.txtdescription.Location = New System.Drawing.Point(260, 13)
        Me.txtdescription.MaxLength = 50
        Me.txtdescription.MendatroryField = True
        Me.txtdescription.MyLinkLable1 = Nothing
        Me.txtdescription.MyLinkLable2 = Nothing
        Me.txtdescription.Name = "txtdescription"
        Me.txtdescription.Size = New System.Drawing.Size(300, 20)
        Me.txtdescription.TabIndex = 2
        Me.txtdescription.TabStop = False
        '
        'txtQtyOnHand
        '
        Me.txtQtyOnHand.BackColor = System.Drawing.Color.White
        Me.txtQtyOnHand.DecimalPlaces = 2
        Me.txtQtyOnHand.Location = New System.Drawing.Point(130, 336)
        Me.txtQtyOnHand.MendatroryField = False
        Me.txtQtyOnHand.MyLinkLable1 = Me.QuantityOnHand
        Me.txtQtyOnHand.MyLinkLable2 = Nothing
        Me.txtQtyOnHand.Name = "txtQtyOnHand"
        Me.txtQtyOnHand.Size = New System.Drawing.Size(126, 20)
        Me.txtQtyOnHand.TabIndex = 6
        Me.txtQtyOnHand.TabStop = False
        Me.txtQtyOnHand.Text = "0"
        Me.txtQtyOnHand.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtQtyOnHand.Value = 0
        Me.txtQtyOnHand.Visible = False
        '
        'rdbtnnew
        '
        Me.rdbtnnew.Image = CType(resources.GetObject("rdbtnnew.Image"), System.Drawing.Image)
        Me.rdbtnnew.Location = New System.Drawing.Point(238, 15)
        Me.rdbtnnew.Name = "rdbtnnew"
        Me.rdbtnnew.Size = New System.Drawing.Size(18, 18)
        Me.rdbtnnew.TabIndex = 1
        '
        'txtAverageCost
        '
        Me.txtAverageCost.BackColor = System.Drawing.Color.White
        Me.txtAverageCost.DecimalPlaces = 2
        Me.txtAverageCost.Location = New System.Drawing.Point(394, 332)
        Me.txtAverageCost.MendatroryField = False
        Me.txtAverageCost.MyLinkLable1 = Me.AverageCost
        Me.txtAverageCost.MyLinkLable2 = Nothing
        Me.txtAverageCost.Name = "txtAverageCost"
        Me.txtAverageCost.Size = New System.Drawing.Size(126, 20)
        Me.txtAverageCost.TabIndex = 7
        Me.txtAverageCost.TabStop = False
        Me.txtAverageCost.Text = "0"
        Me.txtAverageCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtAverageCost.Value = 0
        Me.txtAverageCost.Visible = False
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 27)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdgpbxcustomeraccountset)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(689, 515)
        Me.SplitContainer1.SplitterDistance = 466
        Me.SplitContainer1.TabIndex = 6
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(90, 15)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(82, 18)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'rdbtnsave
        '
        Me.rdbtnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdbtnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtnsave.Location = New System.Drawing.Point(6, 15)
        Me.rdbtnsave.Name = "rdbtnsave"
        Me.rdbtnsave.Size = New System.Drawing.Size(82, 18)
        Me.rdbtnsave.TabIndex = 0
        Me.rdbtnsave.Text = "Save"
        '
        'rdbtnclose
        '
        Me.rdbtnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdbtnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtnclose.Location = New System.Drawing.Point(594, 15)
        Me.rdbtnclose.Name = "rdbtnclose"
        Me.rdbtnclose.Size = New System.Drawing.Size(83, 18)
        Me.rdbtnclose.TabIndex = 2
        Me.rdbtnclose.Text = "Close"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(689, 20)
        Me.RadMenu1.TabIndex = 322
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Setting"
        Me.RadMenuItem3.AccessibleName = "Setting"
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RMImport, Me.RMExport})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "File"
        Me.RadMenuItem3.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RMImport
        '
        Me.RMImport.AccessibleDescription = "Import"
        Me.RMImport.AccessibleName = "Import"
        Me.RMImport.Name = "RMImport"
        Me.RMImport.Text = "Import"
        Me.RMImport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RMExport
        '
        Me.RMExport.AccessibleDescription = "Export"
        Me.RMExport.AccessibleName = "Export"
        Me.RMExport.Name = "RMExport"
        Me.RMExport.Text = "Export"
        Me.RMExport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'FrmCostMaintainance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(689, 542)
        Me.Controls.Add(Me.RadMenu1)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmCostMaintainance"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Cost Maintainance"
        CType(Me.rdgpbxcustomeraccountset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.rdgpbxcustomeraccountset.ResumeLayout(False)
        Me.rdgpbxcustomeraccountset.PerformLayout()
        CType(Me.txtAverageCost_val, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AverageCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtActualCostVal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToolCost_A_val, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSubcontractCost_A_VAl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToolType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMaterialCost_A, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSetupCost_A_Val, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToolCost_A, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOnHandCost_A_val, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPackagingCost_A, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLabourCost_A_val, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalActualCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPackagingCost_A_val, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLabourCost_A, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMaterialCost_A_Val, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSubcontractCost_A, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSetupCost_A, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOnHandCost_A, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.MaterialCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PackagingCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToolCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LabourCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TotalStandardCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OverheadCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SubcontractCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SetupCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMaterialCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToolCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPackagingCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalStandardCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLabourCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSubcontractCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOverheadCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSetupCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.QuantityOnHand, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtuom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlblAccountsetcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtQtyOnHand, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAverageCost, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rdgpbxcustomeraccountset As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents fnduom As common.UserControls.txtFinder
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents txtuom As common.Controls.MyTextBox
    Friend WithEvents txtToolType As common.Controls.MyTextBox
    Friend WithEvents fndToolType As common.UserControls.txtFinder
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents txtSetupCost As common.MyNumBox
    Friend WithEvents SetupCost As common.Controls.MyLabel
    Friend WithEvents txtOverheadCost As common.MyNumBox
    Friend WithEvents OverheadCost As common.Controls.MyLabel
    Friend WithEvents txtLabourCost As common.MyNumBox
    Friend WithEvents LabourCost As common.Controls.MyLabel
    Friend WithEvents txtPackagingCost As common.MyNumBox
    Friend WithEvents PackagingCost As common.Controls.MyLabel
    Friend WithEvents txtMaterialCost As common.MyNumBox
    Friend WithEvents MaterialCost As common.Controls.MyLabel
    Friend WithEvents rdlblAccountsetcode As common.Controls.MyLabel
    Friend WithEvents txtdescription As common.Controls.MyTextBox
    Friend WithEvents rdbtnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtSetupCost_A_Val As common.MyNumBox
    Friend WithEvents txtActualCostVal As common.MyNumBox
    Friend WithEvents txtToolCost_A_val As common.MyNumBox
    Friend WithEvents txtSubcontractCost_A_VAl As common.MyNumBox
    Friend WithEvents txtOnHandCost_A_val As common.MyNumBox
    Friend WithEvents txtLabourCost_A_val As common.MyNumBox
    Friend WithEvents txtPackagingCost_A_val As common.MyNumBox
    Friend WithEvents txtMaterialCost_A_Val As common.MyNumBox
    Friend WithEvents ToolCost As common.Controls.MyLabel
    Friend WithEvents TotalStandardCost As common.Controls.MyLabel
    Friend WithEvents SubcontractCost As common.Controls.MyLabel
    Friend WithEvents txtToolCost As common.MyNumBox
    Friend WithEvents txtTotalStandardCost As common.MyNumBox
    Friend WithEvents txtSubcontractCost As common.MyNumBox
    Friend WithEvents MyLabel20 As common.Controls.MyLabel
    Friend WithEvents txtAverageCost_val As common.MyNumBox
    Friend WithEvents QuantityOnHand As common.Controls.MyLabel
    Friend WithEvents AverageCost As common.Controls.MyLabel
    Friend WithEvents txtQtyOnHand As common.MyNumBox
    Friend WithEvents txtAverageCost As common.MyNumBox
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtMaterialCost_A As common.MyNumBox
    Friend WithEvents txtToolCost_A As common.MyNumBox
    Friend WithEvents txtPackagingCost_A As common.MyNumBox
    Friend WithEvents txtTotalActualCost As common.MyNumBox
    Friend WithEvents txtLabourCost_A As common.MyNumBox
    Friend WithEvents txtSubcontractCost_A As common.MyNumBox
    Friend WithEvents txtSetupCost_A As common.MyNumBox
    Friend WithEvents txtOnHandCost_A As common.MyNumBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndItemCode As common.UserControls.txtFinder
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RMImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RMExport As Telerik.WinControls.UI.RadMenuItem
End Class

