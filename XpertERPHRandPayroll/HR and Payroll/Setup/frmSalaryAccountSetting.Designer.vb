Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSalaryAccountSetting
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
        Me.rdbtnclose = New Telerik.WinControls.UI.RadButton()
        Me.rdbtndelete = New Telerik.WinControls.UI.RadButton()
        Me.rdbtnsave = New Telerik.WinControls.UI.RadButton()
        Me.rdgpbxcustomeraccountset = New Telerik.WinControls.UI.RadGroupBox()
        Me.fndSourceCode = New common.UserControls.txtFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtSourceCodeName = New common.Controls.MyTextBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtBankGLAccount = New common.Controls.MyTextBox()
        Me.lblBankAccount = New common.Controls.MyLabel()
        Me.fndBankAccount = New common.UserControls.txtFinder()
        Me.txtBankAccountName = New common.Controls.MyTextBox()
        Me.txtSalaryPayableAccountDesc = New common.Controls.MyTextBox()
        Me.rdlbldescription = New common.Controls.MyLabel()
        Me.fndSalaryPayableAccount = New common.UserControls.txtFinder()
        Me.lblSalaryPayableAccount = New common.Controls.MyLabel()
        Me.fndaccountsetcode = New common.UserControls.txtNavigator()
        Me.rdlblAccountsetcode = New common.Controls.MyLabel()
        Me.txtAccdescription = New common.Controls.MyTextBox()
        Me.rdbtnnew = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.lblPFPayableAccDesc = New common.Controls.MyTextBox()
        Me.fndPFPayableAcc = New common.UserControls.txtFinder()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.lblESIPayableAccDesc = New common.Controls.MyTextBox()
        Me.fndESIPayableAcc = New common.UserControls.txtFinder()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.lblOthrPayableDesc = New common.Controls.MyTextBox()
        Me.fndOthrPayable = New common.UserControls.txtFinder()
        Me.MyLabel5 = New common.Controls.MyLabel()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdgpbxcustomeraccountset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.rdgpbxcustomeraccountset.SuspendLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSourceCodeName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBankGLAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBankAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBankAccountName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSalaryPayableAccountDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlbldescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSalaryPayableAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlblAccountsetcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAccdescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPFPayableAccDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblESIPayableAccDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblOthrPayableDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'rdbtnclose
        '
        Me.rdbtnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdbtnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtnclose.Location = New System.Drawing.Point(648, 4)
        Me.rdbtnclose.Name = "rdbtnclose"
        Me.rdbtnclose.Size = New System.Drawing.Size(83, 18)
        Me.rdbtnclose.TabIndex = 2
        Me.rdbtnclose.Text = "Close"
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
        'rdgpbxcustomeraccountset
        '
        Me.rdgpbxcustomeraccountset.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.lblOthrPayableDesc)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.fndOthrPayable)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.lblESIPayableAccDesc)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.fndESIPayableAcc)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.MyLabel5)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.MyLabel4)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.lblPFPayableAccDesc)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.fndPFPayableAcc)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.MyLabel3)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.fndSourceCode)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.MyLabel2)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.txtSourceCodeName)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.MyLabel1)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.txtBankGLAccount)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.fndBankAccount)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.lblBankAccount)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.txtBankAccountName)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.txtSalaryPayableAccountDesc)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.fndSalaryPayableAccount)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.lblSalaryPayableAccount)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.fndaccountsetcode)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.rdlblAccountsetcode)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.rdlbldescription)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.txtAccdescription)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.rdbtnnew)
        Me.rdgpbxcustomeraccountset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdgpbxcustomeraccountset.HeaderText = ""
        Me.rdgpbxcustomeraccountset.Location = New System.Drawing.Point(19, 16)
        Me.rdgpbxcustomeraccountset.Name = "rdgpbxcustomeraccountset"
        Me.rdgpbxcustomeraccountset.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.rdgpbxcustomeraccountset.Size = New System.Drawing.Size(727, 235)
        Me.rdgpbxcustomeraccountset.TabIndex = 0
        '
        'fndSourceCode
        '
        Me.fndSourceCode.Location = New System.Drawing.Point(199, 127)
        Me.fndSourceCode.MendatroryField = False
        Me.fndSourceCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndSourceCode.MyLinkLable1 = Me.MyLabel2
        Me.fndSourceCode.MyLinkLable2 = Nothing
        Me.fndSourceCode.MyReadOnly = False
        Me.fndSourceCode.MyShowMasterFormButton = False
        Me.fndSourceCode.Name = "fndSourceCode"
        Me.fndSourceCode.Size = New System.Drawing.Size(185, 19)
        Me.fndSourceCode.TabIndex = 8
        Me.fndSourceCode.Value = ""
        '
        'MyLabel2
        '
        Me.MyLabel2.Location = New System.Drawing.Point(21, 127)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(70, 18)
        Me.MyLabel2.TabIndex = 137
        Me.MyLabel2.Text = "Source Code"
        '
        'txtSourceCodeName
        '
        Me.txtSourceCodeName.Location = New System.Drawing.Point(386, 127)
        Me.txtSourceCodeName.MaxLength = 55
        Me.txtSourceCodeName.MendatroryField = False
        Me.txtSourceCodeName.MyLinkLable1 = Me.MyLabel2
        Me.txtSourceCodeName.MyLinkLable2 = Nothing
        Me.txtSourceCodeName.Name = "txtSourceCodeName"
        Me.txtSourceCodeName.ReadOnly = True
        Me.txtSourceCodeName.Size = New System.Drawing.Size(282, 20)
        Me.txtSourceCodeName.TabIndex = 9
        Me.txtSourceCodeName.TabStop = False
        '
        'MyLabel1
        '
        Me.MyLabel1.Location = New System.Drawing.Point(20, 107)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(91, 18)
        Me.MyLabel1.TabIndex = 136
        Me.MyLabel1.Text = "Bank GL Account"
        '
        'txtBankGLAccount
        '
        Me.txtBankGLAccount.Location = New System.Drawing.Point(199, 103)
        Me.txtBankGLAccount.MaxLength = 55
        Me.txtBankGLAccount.MendatroryField = False
        Me.txtBankGLAccount.MyLinkLable1 = Me.lblBankAccount
        Me.txtBankGLAccount.MyLinkLable2 = Nothing
        Me.txtBankGLAccount.Name = "txtBankGLAccount"
        Me.txtBankGLAccount.ReadOnly = True
        Me.txtBankGLAccount.Size = New System.Drawing.Size(185, 20)
        Me.txtBankGLAccount.TabIndex = 7
        Me.txtBankGLAccount.TabStop = False
        '
        'lblBankAccount
        '
        Me.lblBankAccount.Location = New System.Drawing.Point(20, 82)
        Me.lblBankAccount.Name = "lblBankAccount"
        Me.lblBankAccount.Size = New System.Drawing.Size(75, 18)
        Me.lblBankAccount.TabIndex = 132
        Me.lblBankAccount.Text = "Bank Account"
        '
        'fndBankAccount
        '
        Me.fndBankAccount.Location = New System.Drawing.Point(199, 82)
        Me.fndBankAccount.MendatroryField = False
        Me.fndBankAccount.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndBankAccount.MyLinkLable1 = Me.lblBankAccount
        Me.fndBankAccount.MyLinkLable2 = Nothing
        Me.fndBankAccount.MyReadOnly = False
        Me.fndBankAccount.MyShowMasterFormButton = False
        Me.fndBankAccount.Name = "fndBankAccount"
        Me.fndBankAccount.Size = New System.Drawing.Size(185, 19)
        Me.fndBankAccount.TabIndex = 5
        Me.fndBankAccount.Value = ""
        '
        'txtBankAccountName
        '
        Me.txtBankAccountName.Location = New System.Drawing.Point(386, 82)
        Me.txtBankAccountName.MaxLength = 55
        Me.txtBankAccountName.MendatroryField = False
        Me.txtBankAccountName.MyLinkLable1 = Me.lblBankAccount
        Me.txtBankAccountName.MyLinkLable2 = Nothing
        Me.txtBankAccountName.Name = "txtBankAccountName"
        Me.txtBankAccountName.ReadOnly = True
        Me.txtBankAccountName.Size = New System.Drawing.Size(282, 20)
        Me.txtBankAccountName.TabIndex = 6
        Me.txtBankAccountName.TabStop = False
        '
        'txtSalaryPayableAccountDesc
        '
        Me.txtSalaryPayableAccountDesc.Location = New System.Drawing.Point(386, 60)
        Me.txtSalaryPayableAccountDesc.MaxLength = 50
        Me.txtSalaryPayableAccountDesc.MendatroryField = False
        Me.txtSalaryPayableAccountDesc.MyLinkLable1 = Me.rdlbldescription
        Me.txtSalaryPayableAccountDesc.MyLinkLable2 = Nothing
        Me.txtSalaryPayableAccountDesc.Name = "txtSalaryPayableAccountDesc"
        Me.txtSalaryPayableAccountDesc.ReadOnly = True
        Me.txtSalaryPayableAccountDesc.Size = New System.Drawing.Size(282, 20)
        Me.txtSalaryPayableAccountDesc.TabIndex = 4
        Me.txtSalaryPayableAccountDesc.TabStop = False
        '
        'rdlbldescription
        '
        Me.rdlbldescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdlbldescription.Location = New System.Drawing.Point(20, 40)
        Me.rdlbldescription.Name = "rdlbldescription"
        Me.rdlbldescription.Size = New System.Drawing.Size(63, 16)
        Me.rdlbldescription.TabIndex = 9
        Me.rdlbldescription.Text = "Description"
        '
        'fndSalaryPayableAccount
        '
        Me.fndSalaryPayableAccount.Location = New System.Drawing.Point(199, 61)
        Me.fndSalaryPayableAccount.MendatroryField = False
        Me.fndSalaryPayableAccount.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndSalaryPayableAccount.MyLinkLable1 = Me.lblSalaryPayableAccount
        Me.fndSalaryPayableAccount.MyLinkLable2 = Nothing
        Me.fndSalaryPayableAccount.MyReadOnly = False
        Me.fndSalaryPayableAccount.MyShowMasterFormButton = False
        Me.fndSalaryPayableAccount.Name = "fndSalaryPayableAccount"
        Me.fndSalaryPayableAccount.Size = New System.Drawing.Size(185, 19)
        Me.fndSalaryPayableAccount.TabIndex = 3
        Me.fndSalaryPayableAccount.Value = ""
        '
        'lblSalaryPayableAccount
        '
        Me.lblSalaryPayableAccount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSalaryPayableAccount.Location = New System.Drawing.Point(20, 62)
        Me.lblSalaryPayableAccount.Name = "lblSalaryPayableAccount"
        Me.lblSalaryPayableAccount.Size = New System.Drawing.Size(126, 16)
        Me.lblSalaryPayableAccount.TabIndex = 13
        Me.lblSalaryPayableAccount.Text = "Salary Payable Account"
        '
        'fndaccountsetcode
        '
        Me.fndaccountsetcode.Location = New System.Drawing.Point(199, 14)
        Me.fndaccountsetcode.MendatroryField = True
        Me.fndaccountsetcode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndaccountsetcode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndaccountsetcode.MyLinkLable1 = Me.rdlblAccountsetcode
        Me.fndaccountsetcode.MyLinkLable2 = Nothing
        Me.fndaccountsetcode.MyMaxLength = 30
        Me.fndaccountsetcode.MyReadOnly = False
        Me.fndaccountsetcode.Name = "fndaccountsetcode"
        Me.fndaccountsetcode.Size = New System.Drawing.Size(185, 21)
        Me.fndaccountsetcode.TabIndex = 0
        Me.fndaccountsetcode.Value = ""
        '
        'rdlblAccountsetcode
        '
        Me.rdlblAccountsetcode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.rdlblAccountsetcode.Location = New System.Drawing.Point(20, 20)
        Me.rdlblAccountsetcode.Name = "rdlblAccountsetcode"
        Me.rdlblAccountsetcode.Size = New System.Drawing.Size(93, 16)
        Me.rdlblAccountsetcode.TabIndex = 8
        Me.rdlblAccountsetcode.Text = "Account set code"
        '
        'txtAccdescription
        '
        Me.txtAccdescription.Location = New System.Drawing.Point(199, 38)
        Me.txtAccdescription.MaxLength = 50
        Me.txtAccdescription.MendatroryField = True
        Me.txtAccdescription.MyLinkLable1 = Me.rdlbldescription
        Me.txtAccdescription.MyLinkLable2 = Nothing
        Me.txtAccdescription.Name = "txtAccdescription"
        Me.txtAccdescription.Size = New System.Drawing.Size(469, 20)
        Me.txtAccdescription.TabIndex = 2
        '
        'rdbtnnew
        '
        Me.rdbtnnew.Image = Global.XpertERPHRandPayroll.My.Resources.Resources._new
        Me.rdbtnnew.Location = New System.Drawing.Point(342, 15)
        Me.rdbtnnew.Name = "rdbtnnew"
        Me.rdbtnnew.Size = New System.Drawing.Size(18, 18)
        Me.rdbtnnew.TabIndex = 1
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
        Me.SplitContainer1.Size = New System.Drawing.Size(749, 283)
        Me.SplitContainer1.SplitterDistance = 254
        Me.SplitContainer1.TabIndex = 3
        '
        'RadMenu1
        '
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(749, 20)
        Me.RadMenu1.TabIndex = 2
        Me.RadMenu1.Text = "RadMenu1"
        '
        'lblPFPayableAccDesc
        '
        Me.lblPFPayableAccDesc.Location = New System.Drawing.Point(386, 149)
        Me.lblPFPayableAccDesc.MaxLength = 50
        Me.lblPFPayableAccDesc.MendatroryField = False
        Me.lblPFPayableAccDesc.MyLinkLable1 = Me.rdlbldescription
        Me.lblPFPayableAccDesc.MyLinkLable2 = Nothing
        Me.lblPFPayableAccDesc.Name = "lblPFPayableAccDesc"
        Me.lblPFPayableAccDesc.ReadOnly = True
        Me.lblPFPayableAccDesc.Size = New System.Drawing.Size(282, 20)
        Me.lblPFPayableAccDesc.TabIndex = 139
        Me.lblPFPayableAccDesc.TabStop = False
        '
        'fndPFPayableAcc
        '
        Me.fndPFPayableAcc.Location = New System.Drawing.Point(199, 150)
        Me.fndPFPayableAcc.MendatroryField = False
        Me.fndPFPayableAcc.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndPFPayableAcc.MyLinkLable1 = Me.MyLabel3
        Me.fndPFPayableAcc.MyLinkLable2 = Nothing
        Me.fndPFPayableAcc.MyReadOnly = False
        Me.fndPFPayableAcc.MyShowMasterFormButton = False
        Me.fndPFPayableAcc.Name = "fndPFPayableAcc"
        Me.fndPFPayableAcc.Size = New System.Drawing.Size(185, 19)
        Me.fndPFPayableAcc.TabIndex = 138
        Me.fndPFPayableAcc.Value = ""
        '
        'MyLabel3
        '
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(20, 152)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(160, 16)
        Me.MyLabel3.TabIndex = 140
        Me.MyLabel3.Text = "Employer PF Payable Account"
        '
        'lblESIPayableAccDesc
        '
        Me.lblESIPayableAccDesc.Location = New System.Drawing.Point(386, 172)
        Me.lblESIPayableAccDesc.MaxLength = 50
        Me.lblESIPayableAccDesc.MendatroryField = False
        Me.lblESIPayableAccDesc.MyLinkLable1 = Me.rdlbldescription
        Me.lblESIPayableAccDesc.MyLinkLable2 = Nothing
        Me.lblESIPayableAccDesc.Name = "lblESIPayableAccDesc"
        Me.lblESIPayableAccDesc.ReadOnly = True
        Me.lblESIPayableAccDesc.Size = New System.Drawing.Size(282, 20)
        Me.lblESIPayableAccDesc.TabIndex = 142
        Me.lblESIPayableAccDesc.TabStop = False
        '
        'fndESIPayableAcc
        '
        Me.fndESIPayableAcc.Location = New System.Drawing.Point(199, 173)
        Me.fndESIPayableAcc.MendatroryField = False
        Me.fndESIPayableAcc.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndESIPayableAcc.MyLinkLable1 = Me.MyLabel4
        Me.fndESIPayableAcc.MyLinkLable2 = Nothing
        Me.fndESIPayableAcc.MyReadOnly = False
        Me.fndESIPayableAcc.MyShowMasterFormButton = False
        Me.fndESIPayableAcc.Name = "fndESIPayableAcc"
        Me.fndESIPayableAcc.Size = New System.Drawing.Size(185, 19)
        Me.fndESIPayableAcc.TabIndex = 141
        Me.fndESIPayableAcc.Value = ""
        '
        'MyLabel4
        '
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(20, 175)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(164, 16)
        Me.MyLabel4.TabIndex = 143
        Me.MyLabel4.Text = "Employer ESI Payable Account"
        '
        'lblOthrPayableDesc
        '
        Me.lblOthrPayableDesc.Location = New System.Drawing.Point(386, 195)
        Me.lblOthrPayableDesc.MaxLength = 50
        Me.lblOthrPayableDesc.MendatroryField = False
        Me.lblOthrPayableDesc.MyLinkLable1 = Me.rdlbldescription
        Me.lblOthrPayableDesc.MyLinkLable2 = Nothing
        Me.lblOthrPayableDesc.Name = "lblOthrPayableDesc"
        Me.lblOthrPayableDesc.ReadOnly = True
        Me.lblOthrPayableDesc.Size = New System.Drawing.Size(282, 20)
        Me.lblOthrPayableDesc.TabIndex = 145
        Me.lblOthrPayableDesc.TabStop = False
        '
        'fndOthrPayable
        '
        Me.fndOthrPayable.Location = New System.Drawing.Point(199, 196)
        Me.fndOthrPayable.MendatroryField = False
        Me.fndOthrPayable.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndOthrPayable.MyLinkLable1 = Me.MyLabel5
        Me.fndOthrPayable.MyLinkLable2 = Nothing
        Me.fndOthrPayable.MyReadOnly = False
        Me.fndOthrPayable.MyShowMasterFormButton = False
        Me.fndOthrPayable.Name = "fndOthrPayable"
        Me.fndOthrPayable.Size = New System.Drawing.Size(185, 19)
        Me.fndOthrPayable.TabIndex = 144
        Me.fndOthrPayable.Value = ""
        '
        'MyLabel5
        '
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(20, 198)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(130, 16)
        Me.MyLabel5.TabIndex = 146
        Me.MyLabel5.Text = "Employer Other Payable"
        '
        'frmSalaryAccountSetting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(749, 303)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmSalaryAccountSetting"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Salary Account Setting"
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdgpbxcustomeraccountset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.rdgpbxcustomeraccountset.ResumeLayout(False)
        Me.rdgpbxcustomeraccountset.PerformLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSourceCodeName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBankGLAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBankAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBankAccountName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSalaryPayableAccountDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlbldescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSalaryPayableAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlblAccountsetcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAccdescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnnew, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPFPayableAccDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblESIPayableAccDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblOthrPayableDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rdbtnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdgpbxcustomeraccountset As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtBankGLAccount As common.Controls.MyTextBox
    Friend WithEvents lblBankAccount As common.Controls.MyLabel
    Friend WithEvents fndBankAccount As common.UserControls.txtFinder
    Friend WithEvents txtBankAccountName As common.Controls.MyTextBox
    Friend WithEvents txtSalaryPayableAccountDesc As common.Controls.MyTextBox
    Friend WithEvents rdlbldescription As common.Controls.MyLabel
    Friend WithEvents fndSalaryPayableAccount As common.UserControls.txtFinder
    Friend WithEvents lblSalaryPayableAccount As common.Controls.MyLabel
    Friend WithEvents fndaccountsetcode As common.UserControls.txtNavigator
    Friend WithEvents rdlblAccountsetcode As common.Controls.MyLabel
    Friend WithEvents txtAccdescription As common.Controls.MyTextBox
    Friend WithEvents rdbtnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents fndSourceCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtSourceCodeName As common.Controls.MyTextBox
    Friend WithEvents lblPFPayableAccDesc As common.Controls.MyTextBox
    Friend WithEvents fndPFPayableAcc As common.UserControls.txtFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents lblESIPayableAccDesc As common.Controls.MyTextBox
    Friend WithEvents fndESIPayableAcc As common.UserControls.txtFinder
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents lblOthrPayableDesc As common.Controls.MyTextBox
    Friend WithEvents fndOthrPayable As common.UserControls.txtFinder
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
End Class

