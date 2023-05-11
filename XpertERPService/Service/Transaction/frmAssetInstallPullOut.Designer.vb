Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAssetInstallPullOut
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
        Me.chkPullOut = New Telerik.WinControls.UI.RadRadioButton()
        Me.chkInstall = New Telerik.WinControls.UI.RadRadioButton()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.chkDispatchOnly = New System.Windows.Forms.CheckBox()
        Me.chkOldEntries = New System.Windows.Forms.CheckBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.chkPulloutAndInstall = New Telerik.WinControls.UI.RadRadioButton()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.lblRoute = New common.Controls.MyLabel()
        Me.fndCustomer = New common.UserControls.txtNavigator()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.lblCustomerName = New common.Controls.MyLabel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.lblRoute1 = New common.Controls.MyLabel()
        Me.lblCustomerName1 = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.fndCustomer1 = New common.UserControls.txtNavigator()
        Me.btnNew1 = New Telerik.WinControls.UI.RadButton()
        Me.dgvVisi = New common.UserControls.MyRadGridView()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.Export = New Telerik.WinControls.UI.RadMenuItem()
        Me.Install = New Telerik.WinControls.UI.RadMenuItem()
        Me.PullOut = New Telerik.WinControls.UI.RadMenuItem()
        Me.All = New Telerik.WinControls.UI.RadMenuItem()
        Me.PullOutAndInstall = New Telerik.WinControls.UI.RadMenuItem()
        Me.Import = New Telerik.WinControls.UI.RadMenuItem()
        Me.InstallSheet = New Telerik.WinControls.UI.RadMenuItem()
        Me.PullOutSheet = New Telerik.WinControls.UI.RadMenuItem()
        Me.PullOutAndinstallSheet = New Telerik.WinControls.UI.RadMenuItem()
        Me.RMInstallSheetAll = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        CType(Me.chkPullOut, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkInstall, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPulloutAndInstall, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRoute, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomerName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRoute1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomerName1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvVisi, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvVisi.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'chkPullOut
        '
        Me.chkPullOut.Location = New System.Drawing.Point(761, 4)
        Me.chkPullOut.Name = "chkPullOut"
        Me.chkPullOut.Size = New System.Drawing.Size(60, 18)
        Me.chkPullOut.TabIndex = 3
        Me.chkPullOut.Text = "Pull Out"
        '
        'chkInstall
        '
        Me.chkInstall.Location = New System.Drawing.Point(709, 4)
        Me.chkInstall.Name = "chkInstall"
        Me.chkInstall.Size = New System.Drawing.Size(49, 18)
        Me.chkInstall.TabIndex = 2
        Me.chkInstall.Text = "Install"
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkDispatchOnly)
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkOldEntries)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkPulloutAndInstall)
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkPullOut)
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkInstall)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblRoute)
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndCustomer)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblCustomerName)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.GroupBox1)
        Me.SplitContainer2.Panel2.Controls.Add(Me.dgvVisi)
        Me.SplitContainer2.Size = New System.Drawing.Size(964, 386)
        Me.SplitContainer2.SplitterDistance = 69
        Me.SplitContainer2.TabIndex = 0
        '
        'chkDispatchOnly
        '
        Me.chkDispatchOnly.AutoSize = True
        Me.chkDispatchOnly.Location = New System.Drawing.Point(529, 5)
        Me.chkDispatchOnly.Name = "chkDispatchOnly"
        Me.chkDispatchOnly.Size = New System.Drawing.Size(178, 17)
        Me.chkDispatchOnly.TabIndex = 102
        Me.chkDispatchOnly.Text = "Show Only Dispatched Assets"
        Me.chkDispatchOnly.UseVisualStyleBackColor = True
        Me.chkDispatchOnly.Visible = False
        '
        'chkOldEntries
        '
        Me.chkOldEntries.AutoSize = True
        Me.chkOldEntries.Location = New System.Drawing.Point(457, 5)
        Me.chkOldEntries.Name = "chkOldEntries"
        Me.chkOldEntries.Size = New System.Drawing.Size(74, 17)
        Me.chkOldEntries.TabIndex = 101
        Me.chkOldEntries.Text = "Old Entry"
        Me.chkOldEntries.UseVisualStyleBackColor = True
        '
        'MyLabel3
        '
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(6, 27)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(36, 16)
        Me.MyLabel3.TabIndex = 100
        Me.MyLabel3.Text = "Name"
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(6, 48)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(36, 16)
        Me.MyLabel2.TabIndex = 99
        Me.MyLabel2.Text = "Route"
        '
        'chkPulloutAndInstall
        '
        Me.chkPulloutAndInstall.Location = New System.Drawing.Point(822, 4)
        Me.chkPulloutAndInstall.Name = "chkPulloutAndInstall"
        Me.chkPulloutAndInstall.Size = New System.Drawing.Size(115, 18)
        Me.chkPulloutAndInstall.TabIndex = 4
        Me.chkPulloutAndInstall.Text = "Pull Out and Install"
        '
        'RadLabel1
        '
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(6, 5)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(76, 16)
        Me.RadLabel1.TabIndex = 0
        Me.RadLabel1.Text = "Customer No."
        '
        'lblRoute
        '
        Me.lblRoute.AutoSize = False
        Me.lblRoute.BorderVisible = True
        Me.lblRoute.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRoute.Location = New System.Drawing.Point(179, 46)
        Me.lblRoute.Name = "lblRoute"
        Me.lblRoute.Size = New System.Drawing.Size(505, 20)
        Me.lblRoute.TabIndex = 98
        Me.lblRoute.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblRoute.TextWrap = False
        '
        'fndCustomer
        '
        Me.fndCustomer.Location = New System.Drawing.Point(179, 3)
        Me.fndCustomer.MendatroryField = True
        Me.fndCustomer.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndCustomer.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndCustomer.MyLinkLable1 = Nothing
        Me.fndCustomer.MyLinkLable2 = Nothing
        Me.fndCustomer.MyMaxLength = 32767
        Me.fndCustomer.MyReadOnly = False
        Me.fndCustomer.Name = "fndCustomer"
        Me.fndCustomer.Size = New System.Drawing.Size(218, 20)
        Me.fndCustomer.TabIndex = 5
        Me.fndCustomer.Value = ""
        '
        'btnNew
        '
        Me.btnNew.Image = My.Resources._new
        Me.btnNew.Location = New System.Drawing.Point(403, 3)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(15, 20)
        Me.btnNew.TabIndex = 1
        '
        'lblCustomerName
        '
        Me.lblCustomerName.AutoSize = False
        Me.lblCustomerName.BorderVisible = True
        Me.lblCustomerName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerName.Location = New System.Drawing.Point(179, 25)
        Me.lblCustomerName.Name = "lblCustomerName"
        Me.lblCustomerName.Size = New System.Drawing.Size(505, 20)
        Me.lblCustomerName.TabIndex = 97
        Me.lblCustomerName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCustomerName.TextWrap = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.MyLabel1)
        Me.GroupBox1.Controls.Add(Me.MyLabel4)
        Me.GroupBox1.Controls.Add(Me.lblRoute1)
        Me.GroupBox1.Controls.Add(Me.lblCustomerName1)
        Me.GroupBox1.Controls.Add(Me.MyLabel7)
        Me.GroupBox1.Controls.Add(Me.fndCustomer1)
        Me.GroupBox1.Controls.Add(Me.btnNew1)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(648, 88)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Install To..."
        Me.GroupBox1.Visible = False
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(6, 43)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(36, 16)
        Me.MyLabel1.TabIndex = 100
        Me.MyLabel1.Text = "Name"
        '
        'MyLabel4
        '
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(6, 63)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(36, 16)
        Me.MyLabel4.TabIndex = 99
        Me.MyLabel4.Text = "Route"
        '
        'lblRoute1
        '
        Me.lblRoute1.AutoSize = False
        Me.lblRoute1.BorderVisible = True
        Me.lblRoute1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRoute1.Location = New System.Drawing.Point(136, 63)
        Me.lblRoute1.Name = "lblRoute1"
        Me.lblRoute1.Size = New System.Drawing.Size(505, 20)
        Me.lblRoute1.TabIndex = 98
        Me.lblRoute1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblRoute1.TextWrap = False
        '
        'lblCustomerName1
        '
        Me.lblCustomerName1.AutoSize = False
        Me.lblCustomerName1.BorderVisible = True
        Me.lblCustomerName1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerName1.Location = New System.Drawing.Point(136, 41)
        Me.lblCustomerName1.Name = "lblCustomerName1"
        Me.lblCustomerName1.Size = New System.Drawing.Size(505, 20)
        Me.lblCustomerName1.TabIndex = 97
        Me.lblCustomerName1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCustomerName1.TextWrap = False
        '
        'MyLabel7
        '
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(6, 21)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(76, 16)
        Me.MyLabel7.TabIndex = 96
        Me.MyLabel7.Text = "Customer No."
        '
        'fndCustomer1
        '
        Me.fndCustomer1.Location = New System.Drawing.Point(136, 19)
        Me.fndCustomer1.MendatroryField = True
        Me.fndCustomer1.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndCustomer1.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndCustomer1.MyLinkLable1 = Nothing
        Me.fndCustomer1.MyLinkLable2 = Nothing
        Me.fndCustomer1.MyMaxLength = 32767
        Me.fndCustomer1.MyReadOnly = False
        Me.fndCustomer1.Name = "fndCustomer1"
        Me.fndCustomer1.Size = New System.Drawing.Size(218, 20)
        Me.fndCustomer1.TabIndex = 0
        Me.fndCustomer1.TabStop = False
        Me.fndCustomer1.Value = ""
        '
        'btnNew1
        '
        Me.btnNew1.Image = My.Resources._new
        Me.btnNew1.Location = New System.Drawing.Point(360, 20)
        Me.btnNew1.Name = "btnNew1"
        Me.btnNew1.Size = New System.Drawing.Size(15, 20)
        Me.btnNew1.TabIndex = 1
        Me.btnNew1.TabStop = False
        '
        'dgvVisi
        '
        Me.dgvVisi.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.dgvVisi.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgvVisi.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvVisi.ForeColor = System.Drawing.Color.Black
        Me.dgvVisi.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dgvVisi.Location = New System.Drawing.Point(4, 99)
        '
        'dgvVisi
        '
        Me.dgvVisi.MasterTemplate.AllowDeleteRow = False
        Me.dgvVisi.Name = "dgvVisi"
        Me.dgvVisi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.dgvVisi.ShowGroupPanel = False
        Me.dgvVisi.Size = New System.Drawing.Size(957, 200)
        Me.dgvVisi.TabIndex = 1
        Me.dgvVisi.TabStop = False
        Me.dgvVisi.Text = "RadGridView1"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(964, 417)
        Me.SplitContainer1.SplitterDistance = 386
        Me.SplitContainer1.TabIndex = 92
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(892, 4)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 5
        Me.btnclose.Text = "Close"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(7, 4)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 3
        Me.btnsave.Text = "Save"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(79, 4)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 4
        Me.btndelete.Text = "Delete"
        Me.btndelete.Visible = False
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "File"
        Me.RadMenuItem2.AccessibleName = "File"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "File"
        '
        'RadMenu1
        '
        Me.RadMenu1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(964, 20)
        Me.RadMenu1.TabIndex = 93
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "File"
        Me.RadMenuItem3.AccessibleName = "File"
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.Export, Me.Import})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "File"
        '
        'Export
        '
        Me.Export.AccessibleDescription = "Export"
        Me.Export.AccessibleName = "Export"
        Me.Export.Items.AddRange(New Telerik.WinControls.RadItem() {Me.Install, Me.PullOut, Me.All, Me.PullOutAndInstall})
        Me.Export.Name = "Export"
        Me.Export.Text = "Export"
        '
        'Install
        '
        Me.Install.AccessibleDescription = "Install"
        Me.Install.AccessibleName = "Install"
        Me.Install.Name = "Install"
        Me.Install.Text = "Install"
        '
        'PullOut
        '
        Me.PullOut.AccessibleDescription = "Pull Out"
        Me.PullOut.AccessibleName = "Pull Out"
        Me.PullOut.Name = "PullOut"
        Me.PullOut.Text = "Pull Out"
        '
        'All
        '
        Me.All.AccessibleDescription = "All"
        Me.All.AccessibleName = "All"
        Me.All.Name = "All"
        Me.All.Text = "All"
        Me.All.Visibility = Telerik.WinControls.ElementVisibility.Hidden
        '
        'PullOutAndInstall
        '
        Me.PullOutAndInstall.AccessibleDescription = "Pull Out And Install"
        Me.PullOutAndInstall.AccessibleName = "Pull Out And Install"
        Me.PullOutAndInstall.Name = "PullOutAndInstall"
        Me.PullOutAndInstall.Text = "Pull Out And Install"
        Me.PullOutAndInstall.Visibility = Telerik.WinControls.ElementVisibility.Hidden
        '
        'Import
        '
        Me.Import.AccessibleDescription = "Import"
        Me.Import.AccessibleName = "Import"
        Me.Import.Items.AddRange(New Telerik.WinControls.RadItem() {Me.InstallSheet, Me.PullOutSheet, Me.PullOutAndinstallSheet, Me.RMInstallSheetAll})
        Me.Import.Name = "Import"
        Me.Import.Text = "Import"
        '
        'InstallSheet
        '
        Me.InstallSheet.AccessibleDescription = "Install Sheet"
        Me.InstallSheet.AccessibleName = "Install Sheet"
        Me.InstallSheet.Name = "InstallSheet"
        Me.InstallSheet.Text = "Install Sheet"
        '
        'PullOutSheet
        '
        Me.PullOutSheet.AccessibleDescription = "Pull Out Sheet"
        Me.PullOutSheet.AccessibleName = "Pull Out Sheet"
        Me.PullOutSheet.Name = "PullOutSheet"
        Me.PullOutSheet.Text = "Pull Out Sheet"
        '
        'PullOutAndinstallSheet
        '
        Me.PullOutAndinstallSheet.AccessibleDescription = "Pull Out And Install Sheet"
        Me.PullOutAndinstallSheet.AccessibleName = "Pull Out And Install Sheet"
        Me.PullOutAndinstallSheet.Name = "PullOutAndinstallSheet"
        Me.PullOutAndinstallSheet.Text = "Pull Out And Install Sheet"
        Me.PullOutAndinstallSheet.Visibility = Telerik.WinControls.ElementVisibility.Hidden
        '
        'RMInstallSheetAll
        '
        Me.RMInstallSheetAll.AccessibleDescription = "Install Sheet new"
        Me.RMInstallSheetAll.AccessibleName = "Install Sheet new"
        Me.RMInstallSheetAll.Name = "RMInstallSheetAll"
        Me.RMInstallSheetAll.Text = "Install Sheet new"
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
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
        Me.SplitContainer3.Panel2.Controls.Add(Me.SplitContainer1)
        Me.SplitContainer3.Size = New System.Drawing.Size(964, 446)
        Me.SplitContainer3.SplitterDistance = 25
        Me.SplitContainer3.TabIndex = 94
        '
        'frmAssetInstallPullOut
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(964, 446)
        Me.Controls.Add(Me.SplitContainer3)
        Me.Name = "frmAssetInstallPullOut"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Asset Install / PullOut"
        CType(Me.chkPullOut, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkInstall, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPulloutAndInstall, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRoute, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomerName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRoute1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomerName1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvVisi.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvVisi, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.PerformLayout()
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents chkPullOut As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents chkInstall As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents dgvVisi As common.UserControls.MyRadGridView
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents chkPulloutAndInstall As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents lblRoute1 As common.Controls.MyLabel
    Friend WithEvents lblCustomerName1 As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents fndCustomer1 As common.UserControls.txtNavigator
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents lblRoute As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents lblCustomerName As common.Controls.MyLabel
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndCustomer As common.UserControls.txtNavigator
    Friend WithEvents btnNew1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Export As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Import As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents All As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Install As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents PullOut As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents PullOutAndInstall As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents InstallSheet As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents PullOutSheet As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents PullOutAndinstallSheet As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents chkOldEntries As System.Windows.Forms.CheckBox
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents chkDispatchOnly As System.Windows.Forms.CheckBox
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents RMInstallSheetAll As Telerik.WinControls.UI.RadMenuItem
End Class

