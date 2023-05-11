Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSchmeMaster
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
        Dim GridViewTextBoxColumn1 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn2 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewDecimalColumn1 As Telerik.WinControls.UI.GridViewDecimalColumn = New Telerik.WinControls.UI.GridViewDecimalColumn
        Dim GridViewComboBoxColumn1 As Telerik.WinControls.UI.GridViewComboBoxColumn = New Telerik.WinControls.UI.GridViewComboBoxColumn
        Dim GridViewComboBoxColumn2 As Telerik.WinControls.UI.GridViewComboBoxColumn = New Telerik.WinControls.UI.GridViewComboBoxColumn
        Dim GridViewComboBoxColumn3 As Telerik.WinControls.UI.GridViewComboBoxColumn = New Telerik.WinControls.UI.GridViewComboBoxColumn
        Dim GridViewTextBoxColumn3 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem5 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        ' Me.grdfndSch = New finder.gridFinder
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.txtconvrate = New common.Controls.MyTextBox
        Me.RadLabel7 = New common.Controls.MyLabel
        Me.lblConv = New common.Controls.MyLabel
        Me.txtmrpbottle = New common.Controls.MyTextBox
        Me.lblmrp2 = New common.Controls.MyLabel
        Me.fndScheme = New common.UserControls.txtNavigator
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.fndUnit = New common.UserControls.txtFinder
        Me.lblUnit = New common.Controls.MyLabel
        Me.fndMainItem = New common.UserControls.txtFinder
        Me.RadLabel6 = New common.Controls.MyLabel
        Me.RadLabel10 = New common.Controls.MyLabel
        Me.RadCheckBox1 = New Telerik.WinControls.UI.RadCheckBox
        Me.txtAmount = New common.Controls.MyTextBox
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox
        Me.gvDB = New common.UserControls.MyRadGridView
        Me.RadTreeView1 = New Telerik.WinControls.UI.RadTreeView
        Me.lblitem = New common.Controls.MyLabel
        Me.RadLabel13 = New common.Controls.MyLabel
        Me.RadLabel12 = New common.Controls.MyLabel
        Me.txtcuscategory = New common.Controls.MyTextBox
        Me.fndcuscategory = New common.UserControls.txtFinder
        Me.RadLabel3 = New common.Controls.MyLabel
        Me.ddlBasicPrice = New common.Controls.MyComboBox
        Me.RadLabel11 = New common.Controls.MyLabel
        Me.ddlmrp = New common.Controls.MyComboBox
        Me.lblmrp = New common.Controls.MyLabel
        Me.txtComment = New common.Controls.MyTextBox
        Me.RadLabel9 = New common.Controls.MyLabel
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.grdScheme = New common.UserControls.MyRadGridView
        Me.txtMDesc = New common.Controls.MyTextBox
        Me.RadLabel8 = New common.Controls.MyLabel
        Me.txtQuatity = New common.Controls.MyTextBox
        Me.dtpEnd = New common.Controls.MyDateTimePicker
        Me.RadLabel5 = New common.Controls.MyLabel
        Me.dtpStart = New common.Controls.MyDateTimePicker
        Me.txtDesc = New common.Controls.MyTextBox
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.ddlType = New common.Controls.MyComboBox
        Me.RadLabel4 = New common.Controls.MyLabel
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.btnDelete = New Telerik.WinControls.UI.RadButton
        Me.btnSave = New Telerik.WinControls.UI.RadButton
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.Filemenu = New Telerik.WinControls.UI.RadMenuItem
        Me.importmenu = New Telerik.WinControls.UI.RadMenuItem
        Me.exportmenu = New Telerik.WinControls.UI.RadMenuItem
        Me.Exitmenu = New Telerik.WinControls.UI.RadMenuItem
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.txtconvrate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblConv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtmrpbottle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblmrp2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadCheckBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.gvDB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDB.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadTreeView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblitem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcuscategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlBasicPrice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlmrp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblmrp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtComment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdScheme, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdScheme.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtQuatity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpEnd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpStart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdfndSch
        '
        'Me.grdfndSch.Caption = Nothing
        'Me.grdfndSch.ConnectionString = Nothing
        'Me.grdfndSch.Location = New System.Drawing.Point(263, 83)
        'Me.grdfndSch.Margin = New System.Windows.Forms.Padding(0)
        'Me.grdfndSch.Name = "grdfndSch"
        'Me.grdfndSch.Query = Nothing
        'Me.grdfndSch.ResultDT = Nothing
        'Me.grdfndSch.SelectedRowDR = Nothing
        'Me.grdfndSch.SelectedValue = Nothing
        'Me.grdfndSch.SelectedValue1 = Nothing
        'Me.grdfndSch.Size = New System.Drawing.Size(15, 15)
        'Me.grdfndSch.TabIndex = 0
        'Me.grdfndSch.ValueToSelect = Nothing
        'Me.grdfndSch.ValueToSelect1 = Nothing
        'Me.grdfndSch.Visible = False
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.txtconvrate)
        Me.RadGroupBox1.Controls.Add(Me.lblConv)
        Me.RadGroupBox1.Controls.Add(Me.txtmrpbottle)
        Me.RadGroupBox1.Controls.Add(Me.lblmrp2)
        Me.RadGroupBox1.Controls.Add(Me.fndScheme)
        Me.RadGroupBox1.Controls.Add(Me.fndUnit)
        Me.RadGroupBox1.Controls.Add(Me.fndMainItem)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel10)
        Me.RadGroupBox1.Controls.Add(Me.RadCheckBox1)
        Me.RadGroupBox1.Controls.Add(Me.txtAmount)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox4)
        Me.RadGroupBox1.Controls.Add(Me.RadTreeView1)
        Me.RadGroupBox1.Controls.Add(Me.lblitem)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel13)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel12)
        Me.RadGroupBox1.Controls.Add(Me.txtcuscategory)
        Me.RadGroupBox1.Controls.Add(Me.fndcuscategory)
        Me.RadGroupBox1.Controls.Add(Me.ddlBasicPrice)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel11)
        Me.RadGroupBox1.Controls.Add(Me.ddlmrp)
        Me.RadGroupBox1.Controls.Add(Me.lblmrp)
        Me.RadGroupBox1.Controls.Add(Me.lblUnit)
        Me.RadGroupBox1.Controls.Add(Me.txtComment)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel9)
        Me.RadGroupBox1.Controls.Add(Me.btnReset)
        Me.RadGroupBox1.Controls.Add(Me.GroupBox1)
        Me.RadGroupBox1.Controls.Add(Me.txtMDesc)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel8)
        Me.RadGroupBox1.Controls.Add(Me.txtQuatity)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel7)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel6)
        Me.RadGroupBox1.Controls.Add(Me.dtpEnd)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel5)
        Me.RadGroupBox1.Controls.Add(Me.dtpStart)
        Me.RadGroupBox1.Controls.Add(Me.txtDesc)
        Me.RadGroupBox1.Controls.Add(Me.ddlType)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel4)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel3)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.FooterImageIndex = -1
        Me.RadGroupBox1.FooterImageKey = ""
        Me.RadGroupBox1.HeaderImageIndex = -1
        Me.RadGroupBox1.HeaderImageKey = ""
        Me.RadGroupBox1.HeaderMargin = New System.Windows.Forms.Padding(0)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        '
        '
        '
        Me.RadGroupBox1.RootElement.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(830, 493)
        Me.RadGroupBox1.TabIndex = 0
        '
        'txtconvrate
        '
        Me.txtconvrate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtconvrate.Location = New System.Drawing.Point(134, 170)
        Me.txtconvrate.MendatroryField = False
        Me.txtconvrate.MyLinkLable1 = Me.RadLabel7
        Me.txtconvrate.MyLinkLable2 = Nothing
        Me.txtconvrate.Name = "txtconvrate"
        Me.txtconvrate.Size = New System.Drawing.Size(106, 18)
        Me.txtconvrate.TabIndex = 11
        '
        'RadLabel7
        '
        Me.RadLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel7.Location = New System.Drawing.Point(420, 79)
        Me.RadLabel7.Name = "RadLabel7"
        Me.RadLabel7.Size = New System.Drawing.Size(48, 16)
        Me.RadLabel7.TabIndex = 23
        Me.RadLabel7.Text = "Quantity"
        '
        'lblConv
        '
        Me.lblConv.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblConv.Location = New System.Drawing.Point(25, 170)
        Me.lblConv.Name = "lblConv"
        Me.lblConv.Size = New System.Drawing.Size(63, 16)
        Me.lblConv.TabIndex = 28
        Me.lblConv.Text = "Conv. Rate"
        '
        'txtmrpbottle
        '
        Me.txtmrpbottle.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtmrpbottle.Location = New System.Drawing.Point(135, 146)
        Me.txtmrpbottle.MendatroryField = False
        Me.txtmrpbottle.MyLinkLable1 = Me.RadLabel7
        Me.txtmrpbottle.MyLinkLable2 = Nothing
        Me.txtmrpbottle.Name = "txtmrpbottle"
        Me.txtmrpbottle.Size = New System.Drawing.Size(106, 18)
        Me.txtmrpbottle.TabIndex = 10
        '
        'lblmrp2
        '
        Me.lblmrp2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblmrp2.Location = New System.Drawing.Point(25, 148)
        Me.lblmrp2.Name = "lblmrp2"
        Me.lblmrp2.Size = New System.Drawing.Size(81, 16)
        Me.lblmrp2.TabIndex = 27
        Me.lblmrp2.Text = "MRP in Bottles"
        '
        'fndScheme
        '
        Me.fndScheme.Location = New System.Drawing.Point(134, 9)
        Me.fndScheme.MendatroryField = True
        Me.fndScheme.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.fndScheme.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndScheme.MyLinkLable1 = Me.RadLabel1
        Me.fndScheme.MyLinkLable2 = Nothing
        Me.fndScheme.MyMaxLength = 32767
        Me.fndScheme.MyReadOnly = False
        Me.fndScheme.Name = "fndScheme"
        Me.fndScheme.Size = New System.Drawing.Size(218, 18)
        Me.fndScheme.TabIndex = 0
        Me.fndScheme.Value = ""
        '
        'RadLabel1
        '
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.RadLabel1.Location = New System.Drawing.Point(25, 10)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(78, 16)
        Me.RadLabel1.TabIndex = 30
        Me.RadLabel1.Text = "Scheme Code"
        '
        'fndUnit
        '
        Me.fndUnit.Location = New System.Drawing.Point(134, 74)
        Me.fndUnit.MendatroryField = True
        Me.fndUnit.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndUnit.MyLinkLable1 = Me.lblUnit
        Me.fndUnit.MyLinkLable2 = Nothing
        Me.fndUnit.MyReadOnly = False
        Me.fndUnit.Name = "fndUnit"
        Me.fndUnit.Size = New System.Drawing.Size(218, 18)
        Me.fndUnit.TabIndex = 8
        Me.fndUnit.Value = ""
        '
        'lblUnit
        '
        Me.lblUnit.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUnit.Location = New System.Drawing.Point(25, 77)
        Me.lblUnit.Name = "lblUnit"
        Me.lblUnit.Size = New System.Drawing.Size(26, 16)
        Me.lblUnit.TabIndex = 27
        Me.lblUnit.Text = "Unit"
        '
        'fndMainItem
        '
        Me.fndMainItem.Location = New System.Drawing.Point(135, 52)
        Me.fndMainItem.MendatroryField = True
        Me.fndMainItem.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndMainItem.MyLinkLable1 = Me.RadLabel6
        Me.fndMainItem.MyLinkLable2 = Nothing
        Me.fndMainItem.MyReadOnly = False
        Me.fndMainItem.Name = "fndMainItem"
        Me.fndMainItem.Size = New System.Drawing.Size(218, 18)
        Me.fndMainItem.TabIndex = 6
        Me.fndMainItem.Value = ""
        '
        'RadLabel6
        '
        Me.RadLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel6.Location = New System.Drawing.Point(25, 55)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(56, 16)
        Me.RadLabel6.TabIndex = 28
        Me.RadLabel6.Text = "Main Item"
        '
        'RadLabel10
        '
        Me.RadLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel10.Location = New System.Drawing.Point(423, 146)
        Me.RadLabel10.Name = "RadLabel10"
        Me.RadLabel10.Size = New System.Drawing.Size(45, 16)
        Me.RadLabel10.TabIndex = 20
        Me.RadLabel10.Text = "Amount"
        Me.RadLabel10.Visible = False
        '
        'RadCheckBox1
        '
        Me.RadCheckBox1.Location = New System.Drawing.Point(597, 145)
        Me.RadCheckBox1.Name = "RadCheckBox1"
        Me.RadCheckBox1.Size = New System.Drawing.Size(29, 18)
        Me.RadCheckBox1.TabIndex = 18
        Me.RadCheckBox1.Text = "%"
        Me.RadCheckBox1.Visible = False
        '
        'txtAmount
        '
        Me.txtAmount.Location = New System.Drawing.Point(533, 144)
        Me.txtAmount.MendatroryField = False
        Me.txtAmount.MyLinkLable1 = Me.RadLabel10
        Me.txtAmount.MyLinkLable2 = Nothing
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Size = New System.Drawing.Size(58, 20)
        Me.txtAmount.TabIndex = 14
        Me.txtAmount.Visible = False
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.gvDB)
        Me.RadGroupBox4.FooterImageIndex = -1
        Me.RadGroupBox4.FooterImageKey = ""
        Me.RadGroupBox4.HeaderImageIndex = -1
        Me.RadGroupBox4.HeaderImageKey = ""
        Me.RadGroupBox4.HeaderMargin = New System.Windows.Forms.Padding(0)
        Me.RadGroupBox4.HeaderText = "Replicate In Other Companies"
        Me.RadGroupBox4.Location = New System.Drawing.Point(415, 166)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        '
        '
        '
        Me.RadGroupBox4.RootElement.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(388, 150)
        Me.RadGroupBox4.TabIndex = 91
        Me.RadGroupBox4.Text = "Replicate In Other Companies"
        '
        'gvDB
        '
        Me.gvDB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvDB.Location = New System.Drawing.Point(10, 20)
        '
        'gvDB
        '
        Me.gvDB.MasterTemplate.AllowAddNewRow = False
        Me.gvDB.Name = "gvDB"
        Me.gvDB.ShowGroupPanel = False
        Me.gvDB.Size = New System.Drawing.Size(368, 120)
        Me.gvDB.TabIndex = 0
        Me.gvDB.Text = "RadGridView1"
        '
        'RadTreeView1
        '
        Me.RadTreeView1.CheckBoxes = True
        Me.RadTreeView1.Location = New System.Drawing.Point(135, 191)
        Me.RadTreeView1.Name = "RadTreeView1"
        Me.RadTreeView1.Size = New System.Drawing.Size(218, 120)
        Me.RadTreeView1.TabIndex = 17
        Me.RadTreeView1.Text = "RadTreeView1"
        '
        'lblitem
        '
        Me.lblitem.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblitem.Location = New System.Drawing.Point(25, 194)
        Me.lblitem.Name = "lblitem"
        Me.lblitem.Size = New System.Drawing.Size(28, 16)
        Me.lblitem.TabIndex = 44
        Me.lblitem.Text = "Item"
        '
        'RadLabel13
        '
        Me.RadLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel13.Location = New System.Drawing.Point(25, 124)
        Me.RadLabel13.Name = "RadLabel13"
        Me.RadLabel13.Size = New System.Drawing.Size(104, 16)
        Me.RadLabel13.TabIndex = 42
        Me.RadLabel13.Text = "Customer Category"
        '
        'RadLabel12
        '
        Me.RadLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel12.Location = New System.Drawing.Point(420, 124)
        Me.RadLabel12.Name = "RadLabel12"
        Me.RadLabel12.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel12.TabIndex = 41
        Me.RadLabel12.Text = "Description"
        '
        'txtcuscategory
        '
        Me.txtcuscategory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcuscategory.Location = New System.Drawing.Point(506, 124)
        Me.txtcuscategory.MendatroryField = False
        Me.txtcuscategory.MyLinkLable1 = Me.RadLabel12
        Me.txtcuscategory.MyLinkLable2 = Nothing
        Me.txtcuscategory.Name = "txtcuscategory"
        Me.txtcuscategory.Size = New System.Drawing.Size(297, 18)
        Me.txtcuscategory.TabIndex = 13
        '
        'fndcuscategory
        '
        Me.fndcuscategory.Location = New System.Drawing.Point(135, 121)
        Me.fndcuscategory.MendatroryField = True
        Me.fndcuscategory.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndcuscategory.MyLinkLable1 = Me.RadLabel3
        Me.fndcuscategory.MyLinkLable2 = Nothing
        Me.fndcuscategory.MyReadOnly = False
        Me.fndcuscategory.Name = "fndcuscategory"
        Me.fndcuscategory.Size = New System.Drawing.Size(218, 18)
        Me.fndcuscategory.TabIndex = 12
        Me.fndcuscategory.Value = ""
        '
        'RadLabel3
        '
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(420, 33)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(78, 16)
        Me.RadLabel3.TabIndex = 24
        Me.RadLabel3.Text = "Scheme  Date"
        '
        'ddlBasicPrice
        '
        Me.ddlBasicPrice.Location = New System.Drawing.Point(628, 144)
        Me.ddlBasicPrice.MendatroryField = False
        Me.ddlBasicPrice.MyLinkLable1 = Nothing
        Me.ddlBasicPrice.MyLinkLable2 = Nothing
        Me.ddlBasicPrice.Name = "ddlBasicPrice"
        Me.ddlBasicPrice.ShowImageInEditorArea = True
        Me.ddlBasicPrice.Size = New System.Drawing.Size(50, 20)
        Me.ddlBasicPrice.TabIndex = 15
        Me.ddlBasicPrice.Text = "0"
        Me.ddlBasicPrice.Visible = False
        '
        'RadLabel11
        '
        Me.RadLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel11.Location = New System.Drawing.Point(684, 146)
        Me.RadLabel11.Name = "RadLabel11"
        Me.RadLabel11.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel11.TabIndex = 16
        Me.RadLabel11.Text = "Basic Price"
        Me.RadLabel11.Visible = False
        '
        'ddlmrp
        '
        Me.ddlmrp.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlmrp.Location = New System.Drawing.Point(134, 97)
        Me.ddlmrp.MendatroryField = False
        Me.ddlmrp.MyLinkLable1 = Me.lblmrp
        Me.ddlmrp.MyLinkLable2 = Nothing
        Me.ddlmrp.Name = "ddlmrp"
        Me.ddlmrp.ShowImageInEditorArea = True
        Me.ddlmrp.Size = New System.Drawing.Size(218, 20)
        Me.ddlmrp.TabIndex = 10
        Me.ddlmrp.Text = "Select"
        '
        'lblmrp
        '
        Me.lblmrp.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblmrp.Location = New System.Drawing.Point(25, 99)
        Me.lblmrp.Name = "lblmrp"
        Me.lblmrp.Size = New System.Drawing.Size(31, 16)
        Me.lblmrp.TabIndex = 26
        Me.lblmrp.Text = "MRP"
        '
        'txtComment
        '
        Me.txtComment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtComment.Location = New System.Drawing.Point(506, 101)
        Me.txtComment.MendatroryField = False
        Me.txtComment.MyLinkLable1 = Me.RadLabel9
        Me.txtComment.MyLinkLable2 = Nothing
        Me.txtComment.Name = "txtComment"
        Me.txtComment.Size = New System.Drawing.Size(297, 18)
        Me.txtComment.TabIndex = 11
        '
        'RadLabel9
        '
        Me.RadLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel9.Location = New System.Drawing.Point(420, 102)
        Me.RadLabel9.Name = "RadLabel9"
        Me.RadLabel9.Size = New System.Drawing.Size(55, 16)
        Me.RadLabel9.TabIndex = 22
        Me.RadLabel9.Text = "Comment"
        '
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Image = Global.XpertERPSalesAndDistribution.My.Resources.Resources._new
        Me.btnReset.Location = New System.Drawing.Point(356, 9)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(17, 19)
        Me.btnReset.TabIndex = 1
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.grdScheme)
        Me.GroupBox1.Location = New System.Drawing.Point(23, 312)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(794, 184)
        Me.GroupBox1.TabIndex = 14
        Me.GroupBox1.Text = "Scheme Details"
        '
        'grdScheme
        '
        Me.grdScheme.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdScheme.Cursor = System.Windows.Forms.Cursors.Default
        Me.grdScheme.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.grdScheme.ForeColor = System.Drawing.SystemColors.ControlText
        Me.grdScheme.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.grdScheme.Location = New System.Drawing.Point(12, 18)
        '
        'grdScheme
        '
        Me.grdScheme.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        GridViewTextBoxColumn1.FormatString = ""
        GridViewTextBoxColumn1.HeaderText = "Item Code"
        GridViewTextBoxColumn1.Name = "itemCode"
        GridViewTextBoxColumn1.Width = 94
        GridViewTextBoxColumn2.FormatString = ""
        GridViewTextBoxColumn2.HeaderText = "Description"
        GridViewTextBoxColumn2.Name = "description"
        GridViewTextBoxColumn2.Width = 215
        GridViewDecimalColumn1.FormatString = ""
        GridViewDecimalColumn1.HeaderText = "Quantity"
        GridViewDecimalColumn1.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        GridViewDecimalColumn1.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        GridViewDecimalColumn1.Name = "qty"
        GridViewDecimalColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        GridViewComboBoxColumn1.DisplayMember = Nothing
        GridViewComboBoxColumn1.FormatString = ""
        GridViewComboBoxColumn1.HeaderText = "Price Date"
        GridViewComboBoxColumn1.Name = "priceDate"
        GridViewComboBoxColumn1.ValueMember = Nothing
        GridViewComboBoxColumn1.Width = 100
        GridViewComboBoxColumn2.DisplayMember = Nothing
        GridViewComboBoxColumn2.FormatString = ""
        GridViewComboBoxColumn2.HeaderText = "Unit"
        GridViewComboBoxColumn2.Name = "unitCode"
        GridViewComboBoxColumn2.ValueMember = Nothing
        GridViewComboBoxColumn2.Width = 62
        GridViewComboBoxColumn3.DisplayMember = Nothing
        GridViewComboBoxColumn3.FormatString = ""
        GridViewComboBoxColumn3.HeaderText = "MRP"
        GridViewComboBoxColumn3.Name = "mrp"
        GridViewComboBoxColumn3.ValueMember = Nothing
        GridViewComboBoxColumn3.Width = 73
        GridViewTextBoxColumn3.FormatString = ""
        GridViewTextBoxColumn3.HeaderText = "Remarks"
        GridViewTextBoxColumn3.Name = "remarks"
        GridViewTextBoxColumn3.Width = 224
        Me.grdScheme.MasterTemplate.Columns.AddRange(New Telerik.WinControls.UI.GridViewDataColumn() {GridViewTextBoxColumn1, GridViewTextBoxColumn2, GridViewDecimalColumn1, GridViewComboBoxColumn1, GridViewComboBoxColumn2, GridViewComboBoxColumn3, GridViewTextBoxColumn3})
        Me.grdScheme.MasterTemplate.EnableGrouping = False
        Me.grdScheme.Name = "grdScheme"
        Me.grdScheme.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.grdScheme.Size = New System.Drawing.Size(768, 157)
        Me.grdScheme.TabIndex = 0
        Me.grdScheme.Text = "RadGridView1"
        '
        'txtMDesc
        '
        Me.txtMDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMDesc.Location = New System.Drawing.Point(506, 55)
        Me.txtMDesc.MendatroryField = False
        Me.txtMDesc.MyLinkLable1 = Me.RadLabel8
        Me.txtMDesc.MyLinkLable2 = Nothing
        Me.txtMDesc.Name = "txtMDesc"
        Me.txtMDesc.Size = New System.Drawing.Size(297, 18)
        Me.txtMDesc.TabIndex = 7
        '
        'RadLabel8
        '
        Me.RadLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel8.Location = New System.Drawing.Point(420, 55)
        Me.RadLabel8.Name = "RadLabel8"
        Me.RadLabel8.Size = New System.Drawing.Size(88, 16)
        Me.RadLabel8.TabIndex = 25
        Me.RadLabel8.Text = "Item Description"
        '
        'txtQuatity
        '
        Me.txtQuatity.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtQuatity.Location = New System.Drawing.Point(506, 79)
        Me.txtQuatity.MendatroryField = False
        Me.txtQuatity.MyLinkLable1 = Me.RadLabel7
        Me.txtQuatity.MyLinkLable2 = Nothing
        Me.txtQuatity.Name = "txtQuatity"
        Me.txtQuatity.Size = New System.Drawing.Size(106, 18)
        Me.txtQuatity.TabIndex = 9
        '
        'dtpEnd
        '
        Me.dtpEnd.CustomFormat = "dd/MM/yyyy"
        Me.dtpEnd.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.[Long]
        Me.dtpEnd.Location = New System.Drawing.Point(700, 32)
        Me.dtpEnd.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.dtpEnd.MendatroryField = False
        Me.dtpEnd.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpEnd.MyLinkLable1 = Me.RadLabel5
        Me.dtpEnd.MyLinkLable2 = Nothing
        Me.dtpEnd.Name = "dtpEnd"
        Me.dtpEnd.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpEnd.ShowCheckBox = True
        Me.dtpEnd.Size = New System.Drawing.Size(103, 18)
        Me.dtpEnd.TabIndex = 5
        Me.dtpEnd.Text = "RadDateTimePicker2"
        Me.dtpEnd.Value = New Date(2011, 5, 17, 21, 26, 29, 812)
        '
        'RadLabel5
        '
        Me.RadLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel5.Location = New System.Drawing.Point(618, 33)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(77, 16)
        Me.RadLabel5.TabIndex = 19
        Me.RadLabel5.Text = "InActive  Date"
        '
        'dtpStart
        '
        Me.dtpStart.CustomFormat = "dd/MM/yyyy"
        Me.dtpStart.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.[Long]
        Me.dtpStart.Location = New System.Drawing.Point(506, 32)
        Me.dtpStart.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.dtpStart.MendatroryField = False
        Me.dtpStart.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpStart.MyLinkLable1 = Me.RadLabel3
        Me.dtpStart.MyLinkLable2 = Nothing
        Me.dtpStart.Name = "dtpStart"
        Me.dtpStart.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpStart.Size = New System.Drawing.Size(106, 18)
        Me.dtpStart.TabIndex = 4
        Me.dtpStart.Text = "RadDateTimePicker1"
        Me.dtpStart.Value = New Date(2011, 5, 17, 21, 26, 29, 812)
        '
        'txtDesc
        '
        Me.txtDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc.Location = New System.Drawing.Point(506, 9)
        Me.txtDesc.MendatroryField = False
        Me.txtDesc.MyLinkLable1 = Me.RadLabel2
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.Size = New System.Drawing.Size(297, 18)
        Me.txtDesc.TabIndex = 2
        '
        'RadLabel2
        '
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(420, 10)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel2.TabIndex = 1
        Me.RadLabel2.Text = "Description"
        '
        'ddlType
        '
        Me.ddlType.DropDownAnimationEnabled = True
        Me.ddlType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        RadListDataItem1.Selected = True
        RadListDataItem1.Text = "Select"
        RadListDataItem1.TextWrap = True
        RadListDataItem2.Text = "Quantitative Discount"
        RadListDataItem2.TextWrap = True
        RadListDataItem3.Text = "Cash Discount"
        RadListDataItem3.TextWrap = True
        RadListDataItem4.Text = "Sampling"
        RadListDataItem4.TextWrap = True
        RadListDataItem5.Text = "Promotional"
        RadListDataItem5.TextWrap = True
        Me.ddlType.Items.Add(RadListDataItem1)
        Me.ddlType.Items.Add(RadListDataItem2)
        Me.ddlType.Items.Add(RadListDataItem3)
        Me.ddlType.Items.Add(RadListDataItem4)
        Me.ddlType.Items.Add(RadListDataItem5)
        Me.ddlType.Location = New System.Drawing.Point(135, 32)
        Me.ddlType.MendatroryField = False
        Me.ddlType.MyLinkLable1 = Me.RadLabel4
        Me.ddlType.MyLinkLable2 = Nothing
        Me.ddlType.Name = "ddlType"
        Me.ddlType.ShowImageInEditorArea = True
        Me.ddlType.Size = New System.Drawing.Size(218, 18)
        Me.ddlType.TabIndex = 3
        Me.ddlType.Text = "Quantitative Discount"
        '
        'RadLabel4
        '
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(25, 33)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(76, 16)
        Me.RadLabel4.TabIndex = 29
        Me.RadLabel4.Text = "Scheme Type"
        '
        'btnclose
        '
        Me.btnclose.AccessibleDescription = ""
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(783, 3)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 20
        Me.btnclose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.AccessibleDescription = ""
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(70, 3)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(66, 18)
        Me.btnDelete.TabIndex = 19
        Me.btnDelete.Text = "Delete"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(3, 3)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(66, 18)
        Me.btnSave.TabIndex = 18
        Me.btnSave.Text = "Save"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.Filemenu})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(852, 20)
        Me.RadMenu1.TabIndex = 1
        Me.RadMenu1.Text = "menu"
        '
        'Filemenu
        '
        Me.Filemenu.AccessibleDescription = "File"
        Me.Filemenu.AccessibleName = "File"
        Me.Filemenu.Items.AddRange(New Telerik.WinControls.RadItem() {Me.importmenu, Me.exportmenu, Me.Exitmenu})
        Me.Filemenu.Name = "Filemenu"
        Me.Filemenu.Text = "File"
        Me.Filemenu.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'importmenu
        '
        Me.importmenu.AccessibleDescription = "Import"
        Me.importmenu.AccessibleName = "Import"
        Me.importmenu.Name = "importmenu"
        Me.importmenu.Text = "Import"
        Me.importmenu.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'exportmenu
        '
        Me.exportmenu.AccessibleDescription = "Export"
        Me.exportmenu.AccessibleName = "Export"
        Me.exportmenu.Name = "exportmenu"
        Me.exportmenu.Text = "Export"
        Me.exportmenu.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'Exitmenu
        '
        Me.Exitmenu.AccessibleDescription = "Exit"
        Me.Exitmenu.AccessibleName = "Exit"
        Me.Exitmenu.Name = "Exitmenu"
        Me.Exitmenu.Text = "Exit"
        Me.Exitmenu.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(852, 542)
        Me.SplitContainer1.SplitterDistance = 513
        Me.SplitContainer1.TabIndex = 2
        '
        'FrmSchmeMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(852, 562)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.KeyPreview = True
        Me.Name = "FrmSchmeMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Scheme Master"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.txtconvrate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblConv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtmrpbottle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblmrp2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblUnit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadCheckBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        CType(Me.gvDB.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadTreeView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblitem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcuscategory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlBasicPrice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlmrp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblmrp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtComment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.grdScheme.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdScheme, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtQuatity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpEnd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpStart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    ' Friend WithEvents grdfndSch As finder.gridFinder
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtAmount As common.Controls.MyTextBox
    Friend WithEvents txtComment As common.Controls.MyTextBox
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents grdScheme As common.UserControls.MyRadGridView
    Friend WithEvents txtMDesc As common.Controls.MyTextBox
    Friend WithEvents txtQuatity As common.Controls.MyTextBox
    Friend WithEvents dtpEnd As common.Controls.MyDateTimePicker
    Friend WithEvents dtpStart As common.Controls.MyDateTimePicker
    Friend WithEvents txtDesc As common.Controls.MyTextBox
    Friend WithEvents ddlType As common.Controls.MyComboBox
    Friend WithEvents RadCheckBox1 As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents ddlmrp As common.Controls.MyComboBox
    Friend WithEvents MasterTemplate As common.UserControls.MyRadGridView
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents Filemenu As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents importmenu As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents exportmenu As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Exitmenu As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents ddlBasicPrice As common.Controls.MyComboBox
    Friend WithEvents txtcuscategory As common.Controls.MyTextBox
    Friend WithEvents fndcuscategory As common.UserControls.txtFinder
    Friend WithEvents RadTreeView1 As Telerik.WinControls.UI.RadTreeView
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvDB As common.UserControls.MyRadGridView
    Friend WithEvents RadLabel10 As common.Controls.MyLabel
    Friend WithEvents RadLabel9 As common.Controls.MyLabel
    Friend WithEvents RadLabel8 As common.Controls.MyLabel
    Friend WithEvents RadLabel7 As common.Controls.MyLabel
    Friend WithEvents RadLabel6 As common.Controls.MyLabel
    Friend WithEvents RadLabel5 As common.Controls.MyLabel
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents lblUnit As common.Controls.MyLabel
    Friend WithEvents lblmrp As common.Controls.MyLabel
    Friend WithEvents RadLabel11 As common.Controls.MyLabel
    Friend WithEvents RadLabel13 As common.Controls.MyLabel
    Friend WithEvents RadLabel12 As common.Controls.MyLabel
    Friend WithEvents lblitem As common.Controls.MyLabel
    Friend WithEvents fndMainItem As common.UserControls.txtFinder
    Friend WithEvents fndUnit As common.UserControls.txtFinder
    Friend WithEvents fndScheme As common.UserControls.txtNavigator
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblmrp2 As common.Controls.MyLabel
    Friend WithEvents txtconvrate As common.Controls.MyTextBox
    Friend WithEvents lblConv As common.Controls.MyLabel
    Friend WithEvents txtmrpbottle As common.Controls.MyTextBox
End Class

