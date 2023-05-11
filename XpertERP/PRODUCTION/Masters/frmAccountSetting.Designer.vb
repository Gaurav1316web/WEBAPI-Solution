<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAccountSetting
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
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenuItem
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.rdgpbxcustomeraccountset = New Telerik.WinControls.UI.RadGroupBox
        Me.txtWIPCategory = New common.Controls.MyTextBox
        Me.rdlbldescription = New common.Controls.MyLabel
        Me.fndWIPCategory = New common.UserControls.txtFinder
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.fndaccountsetcode = New common.UserControls.txtNavigator
        Me.rdlblAccountsetcode = New common.Controls.MyLabel
        Me.txtAccdescription = New common.Controls.MyTextBox
        Me.rdbtnnew = New Telerik.WinControls.UI.RadButton
        Me.rdgrpbxgeneralledgeraccounts = New Telerik.WinControls.UI.RadGroupBox
        Me.fndProductionVariance = New common.UserControls.txtFinder
        Me.MyLabel3 = New common.Controls.MyLabel
        Me.fndOverhead = New common.UserControls.txtFinder
        Me.lblcontainer = New common.Controls.MyLabel
        Me.fndMaterialVariance = New common.UserControls.txtFinder
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.fndSubContract = New common.UserControls.txtFinder
        Me.rdlblWriteoffs = New common.Controls.MyLabel
        Me.fndRunLabor = New common.UserControls.txtFinder
        Me.rdlblAdvance = New common.Controls.MyLabel
        Me.fndSetupLabor = New common.UserControls.txtFinder
        Me.rdlblrecieptdiscount = New common.Controls.MyLabel
        Me.fndWIP = New common.UserControls.txtFinder
        Me.WIP = New common.Controls.MyLabel
        Me.txtProductionvariance = New common.Controls.MyTextBox
        Me.txtOverhead = New common.Controls.MyTextBox
        Me.txtMaterialVariance = New common.Controls.MyTextBox
        Me.txtSubContract = New common.Controls.MyTextBox
        Me.txtRunLabor = New common.Controls.MyTextBox
        Me.txtSetupLabor = New common.Controls.MyTextBox
        Me.txtWIP = New common.Controls.MyTextBox
        Me.rdbtnsave = New Telerik.WinControls.UI.RadButton
        Me.rdbtndelete = New Telerik.WinControls.UI.RadButton
        Me.rdbtnclose = New Telerik.WinControls.UI.RadButton
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.rdgpbxcustomeraccountset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.rdgpbxcustomeraccountset.SuspendLayout()
        CType(Me.txtWIPCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlbldescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlblAccountsetcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAccdescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdgrpbxgeneralledgeraccounts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.rdgrpbxgeneralledgeraccounts.SuspendLayout()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblcontainer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlblWriteoffs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlblAdvance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlblrecieptdiscount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.WIP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtProductionvariance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOverhead, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMaterialVariance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSubContract, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRunLabor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSetupLabor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtWIP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenufile})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(676, 20)
        Me.RadMenu1.TabIndex = 0
        Me.RadMenu1.Text = "RadMenu1"
        '
        'rdmenufile
        '
        Me.rdmenufile.AccessibleDescription = "RadMenuItem1"
        Me.rdmenufile.AccessibleName = "RadMenuItem1"
        Me.rdmenufile.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Text = "File"
        Me.rdmenufile.Visibility = Telerik.WinControls.ElementVisibility.Visible
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdgpbxcustomeraccountset)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(676, 388)
        Me.SplitContainer1.SplitterDistance = 359
        Me.SplitContainer1.TabIndex = 3
        '
        'rdgpbxcustomeraccountset
        '
        Me.rdgpbxcustomeraccountset.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.txtWIPCategory)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.fndWIPCategory)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.MyLabel1)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.fndaccountsetcode)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.rdlblAccountsetcode)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.rdlbldescription)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.txtAccdescription)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.rdbtnnew)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.rdgrpbxgeneralledgeraccounts)
        Me.rdgpbxcustomeraccountset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdgpbxcustomeraccountset.HeaderText = ""
        Me.rdgpbxcustomeraccountset.Location = New System.Drawing.Point(19, 16)
        Me.rdgpbxcustomeraccountset.Name = "rdgpbxcustomeraccountset"
        Me.rdgpbxcustomeraccountset.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.rdgpbxcustomeraccountset.Size = New System.Drawing.Size(643, 329)
        Me.rdgpbxcustomeraccountset.TabIndex = 0
        '
        'txtWIPCategory
        '
        Me.txtWIPCategory.Location = New System.Drawing.Point(287, 60)
        Me.txtWIPCategory.MaxLength = 50
        Me.txtWIPCategory.MendatroryField = False
        Me.txtWIPCategory.MyLinkLable1 = Me.rdlbldescription
        Me.txtWIPCategory.MyLinkLable2 = Nothing
        Me.txtWIPCategory.Name = "txtWIPCategory"
        Me.txtWIPCategory.ReadOnly = True
        Me.txtWIPCategory.Size = New System.Drawing.Size(339, 20)
        Me.txtWIPCategory.TabIndex = 4
        Me.txtWIPCategory.TabStop = False
        '
        'rdlbldescription
        '
        Me.rdlbldescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdlbldescription.Location = New System.Drawing.Point(26, 42)
        Me.rdlbldescription.Name = "rdlbldescription"
        Me.rdlbldescription.Size = New System.Drawing.Size(63, 16)
        Me.rdlbldescription.TabIndex = 7
        Me.rdlbldescription.Text = "Description"
        '
        'fndWIPCategory
        '
        Me.fndWIPCategory.Location = New System.Drawing.Point(138, 61)
        Me.fndWIPCategory.MendatroryField = False
        Me.fndWIPCategory.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndWIPCategory.MyLinkLable1 = Me.MyLabel1
        Me.fndWIPCategory.MyLinkLable2 = Nothing
        Me.fndWIPCategory.MyReadOnly = False
        Me.fndWIPCategory.Name = "fndWIPCategory"
        Me.fndWIPCategory.Size = New System.Drawing.Size(143, 19)
        Me.fndWIPCategory.TabIndex = 3
        Me.fndWIPCategory.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(26, 64)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(77, 16)
        Me.MyLabel1.TabIndex = 6
        Me.MyLabel1.Text = "WIP Category"
        '
        'fndaccountsetcode
        '
        Me.fndaccountsetcode.Location = New System.Drawing.Point(138, 14)
        Me.fndaccountsetcode.MendatroryField = True
        Me.fndaccountsetcode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndaccountsetcode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndaccountsetcode.MyLinkLable1 = Me.rdlblAccountsetcode
        Me.fndaccountsetcode.MyLinkLable2 = Nothing
        Me.fndaccountsetcode.MyMaxLength = 30
        Me.fndaccountsetcode.MyReadOnly = False
        Me.fndaccountsetcode.Name = "fndaccountsetcode"
        Me.fndaccountsetcode.Size = New System.Drawing.Size(202, 21)
        Me.fndaccountsetcode.TabIndex = 0
        Me.fndaccountsetcode.Value = ""
        '
        'rdlblAccountsetcode
        '
        Me.rdlblAccountsetcode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.rdlblAccountsetcode.Location = New System.Drawing.Point(27, 23)
        Me.rdlblAccountsetcode.Name = "rdlblAccountsetcode"
        Me.rdlblAccountsetcode.Size = New System.Drawing.Size(93, 16)
        Me.rdlblAccountsetcode.TabIndex = 8
        Me.rdlblAccountsetcode.Text = "Account set code"
        '
        'txtAccdescription
        '
        Me.txtAccdescription.Location = New System.Drawing.Point(138, 38)
        Me.txtAccdescription.MaxLength = 50
        Me.txtAccdescription.MendatroryField = True
        Me.txtAccdescription.MyLinkLable1 = Me.rdlbldescription
        Me.txtAccdescription.MyLinkLable2 = Nothing
        Me.txtAccdescription.Name = "txtAccdescription"
        Me.txtAccdescription.Size = New System.Drawing.Size(488, 20)
        Me.txtAccdescription.TabIndex = 2
        '
        'rdbtnnew
        '
        Me.rdbtnnew.Image = Global.ERP.My.Resources.Resources._new
        Me.rdbtnnew.Location = New System.Drawing.Point(342, 15)
        Me.rdbtnnew.Name = "rdbtnnew"
        Me.rdbtnnew.Size = New System.Drawing.Size(18, 18)
        Me.rdbtnnew.TabIndex = 1
        '
        'rdgrpbxgeneralledgeraccounts
        '
        Me.rdgrpbxgeneralledgeraccounts.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.fndProductionVariance)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.fndOverhead)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.fndMaterialVariance)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.fndSubContract)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.fndRunLabor)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.fndSetupLabor)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.fndWIP)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.txtProductionvariance)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.txtOverhead)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.txtMaterialVariance)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.txtSubContract)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.MyLabel3)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.lblcontainer)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.txtRunLabor)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.txtSetupLabor)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.txtWIP)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.MyLabel2)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.rdlblWriteoffs)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.rdlblAdvance)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.rdlblrecieptdiscount)
        Me.rdgrpbxgeneralledgeraccounts.Controls.Add(Me.WIP)
        Me.rdgrpbxgeneralledgeraccounts.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdgrpbxgeneralledgeraccounts.HeaderText = "General Ledger Account"
        Me.rdgrpbxgeneralledgeraccounts.Location = New System.Drawing.Point(26, 86)
        Me.rdgrpbxgeneralledgeraccounts.Name = "rdgrpbxgeneralledgeraccounts"
        Me.rdgrpbxgeneralledgeraccounts.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.rdgrpbxgeneralledgeraccounts.Size = New System.Drawing.Size(613, 230)
        Me.rdgrpbxgeneralledgeraccounts.TabIndex = 5
        Me.rdgrpbxgeneralledgeraccounts.Text = "General Ledger Account"
        '
        'fndProductionVariance
        '
        Me.fndProductionVariance.Location = New System.Drawing.Point(127, 174)
        Me.fndProductionVariance.MendatroryField = False
        Me.fndProductionVariance.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndProductionVariance.MyLinkLable1 = Me.MyLabel3
        Me.fndProductionVariance.MyLinkLable2 = Nothing
        Me.fndProductionVariance.MyReadOnly = False
        Me.fndProductionVariance.Name = "fndProductionVariance"
        Me.fndProductionVariance.Size = New System.Drawing.Size(143, 19)
        Me.fndProductionVariance.TabIndex = 7
        Me.fndProductionVariance.Value = ""
        '
        'MyLabel3
        '
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(26, 175)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(108, 16)
        Me.MyLabel3.TabIndex = 8
        Me.MyLabel3.Text = "Production Variance"
        '
        'fndOverhead
        '
        Me.fndOverhead.Location = New System.Drawing.Point(127, 124)
        Me.fndOverhead.MendatroryField = False
        Me.fndOverhead.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndOverhead.MyLinkLable1 = Me.lblcontainer
        Me.fndOverhead.MyLinkLable2 = Nothing
        Me.fndOverhead.MyReadOnly = False
        Me.fndOverhead.Name = "fndOverhead"
        Me.fndOverhead.Size = New System.Drawing.Size(143, 19)
        Me.fndOverhead.TabIndex = 5
        Me.fndOverhead.Value = ""
        '
        'lblcontainer
        '
        Me.lblcontainer.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcontainer.Location = New System.Drawing.Point(26, 125)
        Me.lblcontainer.Name = "lblcontainer"
        Me.lblcontainer.Size = New System.Drawing.Size(56, 16)
        Me.lblcontainer.TabIndex = 10
        Me.lblcontainer.Text = "Overhead"
        '
        'fndMaterialVariance
        '
        Me.fndMaterialVariance.Location = New System.Drawing.Point(127, 148)
        Me.fndMaterialVariance.MendatroryField = False
        Me.fndMaterialVariance.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndMaterialVariance.MyLinkLable1 = Me.MyLabel2
        Me.fndMaterialVariance.MyLinkLable2 = Nothing
        Me.fndMaterialVariance.MyReadOnly = False
        Me.fndMaterialVariance.Name = "fndMaterialVariance"
        Me.fndMaterialVariance.Size = New System.Drawing.Size(143, 19)
        Me.fndMaterialVariance.TabIndex = 6
        Me.fndMaterialVariance.Value = ""
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(26, 149)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(94, 16)
        Me.MyLabel2.TabIndex = 9
        Me.MyLabel2.Text = "Material Variance"
        '
        'fndSubContract
        '
        Me.fndSubContract.Location = New System.Drawing.Point(127, 98)
        Me.fndSubContract.MendatroryField = False
        Me.fndSubContract.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndSubContract.MyLinkLable1 = Me.rdlblWriteoffs
        Me.fndSubContract.MyLinkLable2 = Nothing
        Me.fndSubContract.MyReadOnly = False
        Me.fndSubContract.Name = "fndSubContract"
        Me.fndSubContract.Size = New System.Drawing.Size(143, 19)
        Me.fndSubContract.TabIndex = 4
        Me.fndSubContract.Value = ""
        '
        'rdlblWriteoffs
        '
        Me.rdlblWriteoffs.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdlblWriteoffs.Location = New System.Drawing.Point(26, 99)
        Me.rdlblWriteoffs.Name = "rdlblWriteoffs"
        Me.rdlblWriteoffs.Size = New System.Drawing.Size(66, 16)
        Me.rdlblWriteoffs.TabIndex = 11
        Me.rdlblWriteoffs.Text = "SubConract"
        '
        'fndRunLabor
        '
        Me.fndRunLabor.Location = New System.Drawing.Point(125, 73)
        Me.fndRunLabor.MendatroryField = False
        Me.fndRunLabor.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndRunLabor.MyLinkLable1 = Me.rdlblAdvance
        Me.fndRunLabor.MyLinkLable2 = Nothing
        Me.fndRunLabor.MyReadOnly = False
        Me.fndRunLabor.Name = "fndRunLabor"
        Me.fndRunLabor.Size = New System.Drawing.Size(143, 19)
        Me.fndRunLabor.TabIndex = 3
        Me.fndRunLabor.Value = ""
        '
        'rdlblAdvance
        '
        Me.rdlblAdvance.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdlblAdvance.Location = New System.Drawing.Point(26, 74)
        Me.rdlblAdvance.Name = "rdlblAdvance"
        Me.rdlblAdvance.Size = New System.Drawing.Size(59, 16)
        Me.rdlblAdvance.TabIndex = 12
        Me.rdlblAdvance.Text = "Run Labor"
        '
        'fndSetupLabor
        '
        Me.fndSetupLabor.Location = New System.Drawing.Point(125, 48)
        Me.fndSetupLabor.MendatroryField = False
        Me.fndSetupLabor.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndSetupLabor.MyLinkLable1 = Me.rdlblrecieptdiscount
        Me.fndSetupLabor.MyLinkLable2 = Nothing
        Me.fndSetupLabor.MyReadOnly = False
        Me.fndSetupLabor.Name = "fndSetupLabor"
        Me.fndSetupLabor.Size = New System.Drawing.Size(143, 19)
        Me.fndSetupLabor.TabIndex = 2
        Me.fndSetupLabor.Value = ""
        '
        'rdlblrecieptdiscount
        '
        Me.rdlblrecieptdiscount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdlblrecieptdiscount.Location = New System.Drawing.Point(26, 49)
        Me.rdlblrecieptdiscount.Name = "rdlblrecieptdiscount"
        Me.rdlblrecieptdiscount.Size = New System.Drawing.Size(68, 16)
        Me.rdlblrecieptdiscount.TabIndex = 13
        Me.rdlblrecieptdiscount.Text = "Setup Labor"
        '
        'fndWIP
        '
        Me.fndWIP.Location = New System.Drawing.Point(125, 22)
        Me.fndWIP.MendatroryField = False
        Me.fndWIP.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndWIP.MyLinkLable1 = Me.WIP
        Me.fndWIP.MyLinkLable2 = Nothing
        Me.fndWIP.MyReadOnly = False
        Me.fndWIP.Name = "fndWIP"
        Me.fndWIP.Size = New System.Drawing.Size(143, 19)
        Me.fndWIP.TabIndex = 1
        Me.fndWIP.Value = ""
        '
        'WIP
        '
        Me.WIP.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.WIP.Location = New System.Drawing.Point(26, 23)
        Me.WIP.Name = "WIP"
        Me.WIP.Size = New System.Drawing.Size(28, 16)
        Me.WIP.TabIndex = 14
        Me.WIP.Text = "WIP"
        '
        'txtProductionvariance
        '
        Me.txtProductionvariance.Location = New System.Drawing.Point(278, 173)
        Me.txtProductionvariance.MendatroryField = False
        Me.txtProductionvariance.MyLinkLable1 = Me.MyLabel3
        Me.txtProductionvariance.MyLinkLable2 = Nothing
        Me.txtProductionvariance.Name = "txtProductionvariance"
        Me.txtProductionvariance.ReadOnly = True
        Me.txtProductionvariance.Size = New System.Drawing.Size(326, 20)
        Me.txtProductionvariance.TabIndex = 13
        Me.txtProductionvariance.TabStop = False
        '
        'txtOverhead
        '
        Me.txtOverhead.Location = New System.Drawing.Point(278, 123)
        Me.txtOverhead.MendatroryField = False
        Me.txtOverhead.MyLinkLable1 = Me.lblcontainer
        Me.txtOverhead.MyLinkLable2 = Nothing
        Me.txtOverhead.Name = "txtOverhead"
        Me.txtOverhead.ReadOnly = True
        Me.txtOverhead.Size = New System.Drawing.Size(326, 20)
        Me.txtOverhead.TabIndex = 9
        Me.txtOverhead.TabStop = False
        '
        'txtMaterialVariance
        '
        Me.txtMaterialVariance.Location = New System.Drawing.Point(278, 147)
        Me.txtMaterialVariance.MendatroryField = False
        Me.txtMaterialVariance.MyLinkLable1 = Me.MyLabel2
        Me.txtMaterialVariance.MyLinkLable2 = Nothing
        Me.txtMaterialVariance.Name = "txtMaterialVariance"
        Me.txtMaterialVariance.ReadOnly = True
        Me.txtMaterialVariance.Size = New System.Drawing.Size(326, 20)
        Me.txtMaterialVariance.TabIndex = 11
        Me.txtMaterialVariance.TabStop = False
        '
        'txtSubContract
        '
        Me.txtSubContract.Location = New System.Drawing.Point(278, 97)
        Me.txtSubContract.MendatroryField = False
        Me.txtSubContract.MyLinkLable1 = Me.rdlblWriteoffs
        Me.txtSubContract.MyLinkLable2 = Nothing
        Me.txtSubContract.Name = "txtSubContract"
        Me.txtSubContract.ReadOnly = True
        Me.txtSubContract.Size = New System.Drawing.Size(326, 20)
        Me.txtSubContract.TabIndex = 7
        Me.txtSubContract.TabStop = False
        '
        'txtRunLabor
        '
        Me.txtRunLabor.Location = New System.Drawing.Point(276, 72)
        Me.txtRunLabor.MendatroryField = False
        Me.txtRunLabor.MyLinkLable1 = Me.rdlblAdvance
        Me.txtRunLabor.MyLinkLable2 = Nothing
        Me.txtRunLabor.Name = "txtRunLabor"
        Me.txtRunLabor.ReadOnly = True
        Me.txtRunLabor.Size = New System.Drawing.Size(326, 20)
        Me.txtRunLabor.TabIndex = 5
        Me.txtRunLabor.TabStop = False
        '
        'txtSetupLabor
        '
        Me.txtSetupLabor.Location = New System.Drawing.Point(276, 47)
        Me.txtSetupLabor.MendatroryField = False
        Me.txtSetupLabor.MyLinkLable1 = Me.rdlblrecieptdiscount
        Me.txtSetupLabor.MyLinkLable2 = Nothing
        Me.txtSetupLabor.Name = "txtSetupLabor"
        Me.txtSetupLabor.ReadOnly = True
        Me.txtSetupLabor.Size = New System.Drawing.Size(326, 20)
        Me.txtSetupLabor.TabIndex = 3
        Me.txtSetupLabor.TabStop = False
        '
        'txtWIP
        '
        Me.txtWIP.Location = New System.Drawing.Point(275, 21)
        Me.txtWIP.MendatroryField = False
        Me.txtWIP.MyLinkLable1 = Me.WIP
        Me.txtWIP.MyLinkLable2 = Nothing
        Me.txtWIP.Name = "txtWIP"
        Me.txtWIP.ReadOnly = True
        Me.txtWIP.Size = New System.Drawing.Size(326, 20)
        Me.txtWIP.TabIndex = 1
        Me.txtWIP.TabStop = False
        '
        'rdbtnsave
        '
        Me.rdbtnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdbtnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtnsave.Location = New System.Drawing.Point(19, 4)
        Me.rdbtnsave.Name = "rdbtnsave"
        Me.rdbtnsave.Size = New System.Drawing.Size(82, 18)
        Me.rdbtnsave.TabIndex = 0
        Me.rdbtnsave.Text = "Save"
        '
        'rdbtndelete
        '
        Me.rdbtndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdbtndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtndelete.Location = New System.Drawing.Point(102, 4)
        Me.rdbtndelete.Name = "rdbtndelete"
        Me.rdbtndelete.Size = New System.Drawing.Size(80, 18)
        Me.rdbtndelete.TabIndex = 1
        Me.rdbtndelete.Text = "Delete"
        '
        'rdbtnclose
        '
        Me.rdbtnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdbtnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtnclose.Location = New System.Drawing.Point(575, 4)
        Me.rdbtnclose.Name = "rdbtnclose"
        Me.rdbtnclose.Size = New System.Drawing.Size(83, 18)
        Me.rdbtnclose.TabIndex = 2
        Me.rdbtnclose.Text = "Close"
        '
        'FrmAccountSetting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(676, 408)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmAccountSetting"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Account Set"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.rdgpbxcustomeraccountset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.rdgpbxcustomeraccountset.ResumeLayout(False)
        Me.rdgpbxcustomeraccountset.PerformLayout()
        CType(Me.txtWIPCategory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlbldescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlblAccountsetcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAccdescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdgrpbxgeneralledgeraccounts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.rdgrpbxgeneralledgeraccounts.ResumeLayout(False)
        Me.rdgrpbxgeneralledgeraccounts.PerformLayout()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblcontainer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlblWriteoffs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlblAdvance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlblrecieptdiscount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.WIP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtProductionvariance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOverhead, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMaterialVariance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSubContract, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRunLabor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSetupLabor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtWIP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents rdgpbxcustomeraccountset As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtWIPCategory As common.Controls.MyTextBox
    Friend WithEvents rdlbldescription As common.Controls.MyLabel
    Friend WithEvents fndWIPCategory As common.UserControls.txtFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents fndaccountsetcode As common.UserControls.txtNavigator
    Friend WithEvents rdlblAccountsetcode As common.Controls.MyLabel
    Friend WithEvents txtAccdescription As common.Controls.MyTextBox
    Friend WithEvents rdbtnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdgrpbxgeneralledgeraccounts As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents fndOverhead As common.UserControls.txtFinder
    Friend WithEvents lblcontainer As common.Controls.MyLabel
    Friend WithEvents fndSubContract As common.UserControls.txtFinder
    Friend WithEvents rdlblWriteoffs As common.Controls.MyLabel
    Friend WithEvents fndRunLabor As common.UserControls.txtFinder
    Friend WithEvents rdlblAdvance As common.Controls.MyLabel
    Friend WithEvents fndSetupLabor As common.UserControls.txtFinder
    Friend WithEvents rdlblrecieptdiscount As common.Controls.MyLabel
    Friend WithEvents fndWIP As common.UserControls.txtFinder
    Friend WithEvents WIP As common.Controls.MyLabel
    Friend WithEvents txtOverhead As common.Controls.MyTextBox
    Friend WithEvents txtSubContract As common.Controls.MyTextBox
    Friend WithEvents txtRunLabor As common.Controls.MyTextBox
    Friend WithEvents txtSetupLabor As common.Controls.MyTextBox
    Friend WithEvents txtWIP As common.Controls.MyTextBox
    Friend WithEvents rdbtnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndProductionVariance As common.UserControls.txtFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents fndMaterialVariance As common.UserControls.txtFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtProductionvariance As common.Controls.MyTextBox
    Friend WithEvents txtMaterialVariance As common.Controls.MyTextBox
End Class

