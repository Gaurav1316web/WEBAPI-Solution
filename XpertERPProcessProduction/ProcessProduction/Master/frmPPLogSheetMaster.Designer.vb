<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPPLogSheetMaster
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.MenuClose = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnexport = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnEx_detail = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnEx_User = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnimport = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnIm_Detail = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnIm_Users = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtISRef = New common.Controls.MyTextBox()
        Me.lblAliasName = New common.Controls.MyLabel()
        Me.txtClsRef = New common.Controls.MyTextBox()
        Me.txtAliasName = New common.Controls.MyTextBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.cmbtype = New common.Controls.MyComboBox()
        Me.chk_batch_no = New System.Windows.Forms.CheckBox()
        Me.chkReq_Para_Mst = New System.Windows.Forms.CheckBox()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgUsers = New common.MyCheckBoxGrid()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.chkSelect = New common.Controls.MyRadioButton()
        Me.chkAll = New common.Controls.MyRadioButton()
        Me.TxtDepart_desc = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.fndDepart_code = New common.UserControls.txtFinder()
        Me.chkIsmandatory = New System.Windows.Forms.CheckBox()
        Me.cbonature = New common.Controls.MyComboBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lblvandorno = New common.Controls.MyLabel()
        Me.txtdesc = New common.Controls.MyTextBox()
        Me.lblvendorname = New common.Controls.MyLabel()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.fndNo = New common.UserControls.txtNavigator()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
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
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtISRef, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAliasName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtClsRef, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAliasName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbtype, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.chkSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtDepart_desc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbonature, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblvandorno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblvendorname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
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
        Me.SplitContainer1.Panel1.Padding = New System.Windows.Forms.Padding(6)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(842, 526)
        Me.SplitContainer1.SplitterDistance = 490
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(6, 6)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadMenu1)
        Me.SplitContainer2.Panel1.Padding = New System.Windows.Forms.Padding(5)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadPageView1)
        Me.SplitContainer2.Panel2.Padding = New System.Windows.Forms.Padding(5)
        Me.SplitContainer2.Size = New System.Drawing.Size(830, 478)
        Me.SplitContainer2.SplitterDistance = 31
        Me.SplitContainer2.TabIndex = 18
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.MenuClose})
        Me.RadMenu1.Location = New System.Drawing.Point(5, 5)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(820, 20)
        Me.RadMenu1.TabIndex = 16
        '
        'MenuClose
        '
        Me.MenuClose.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnexport, Me.btnimport})
        Me.MenuClose.Name = "MenuClose"
        Me.MenuClose.Text = "File"
        '
        'btnexport
        '
        Me.btnexport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnEx_detail, Me.btnEx_User})
        Me.btnexport.Name = "btnexport"
        Me.btnexport.Text = "Export"
        '
        'btnEx_detail
        '
        Me.btnEx_detail.Name = "btnEx_detail"
        Me.btnEx_detail.Text = "Export Detail"
        '
        'btnEx_User
        '
        Me.btnEx_User.Name = "btnEx_User"
        Me.btnEx_User.Text = "Export Users"
        '
        'btnimport
        '
        Me.btnimport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnIm_Detail, Me.btnIm_Users})
        Me.btnimport.Name = "btnimport"
        Me.btnimport.Text = "Import"
        '
        'btnIm_Detail
        '
        Me.btnIm_Detail.Name = "btnIm_Detail"
        Me.btnIm_Detail.Text = "Import Detail"
        '
        'btnIm_Users
        '
        Me.btnIm_Users.Name = "btnIm_Users"
        Me.btnIm_Users.Text = "Import Users"
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(5, 5)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(820, 433)
        Me.RadPageView1.TabIndex = 0
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(45.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(799, 385)
        Me.RadPageViewPage1.Text = "Detail"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.MyLabel5)
        Me.RadGroupBox1.Controls.Add(Me.chkReq_Para_Mst)
        Me.RadGroupBox1.Controls.Add(Me.cmbtype)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel4)
        Me.RadGroupBox1.Controls.Add(Me.txtISRef)
        Me.RadGroupBox1.Controls.Add(Me.txtClsRef)
        Me.RadGroupBox1.Controls.Add(Me.txtAliasName)
        Me.RadGroupBox1.Controls.Add(Me.lblAliasName)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox1.Controls.Add(Me.chk_batch_no)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox2)
        Me.RadGroupBox1.Controls.Add(Me.TxtDepart_desc)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.fndDepart_code)
        Me.RadGroupBox1.Controls.Add(Me.chkIsmandatory)
        Me.RadGroupBox1.Controls.Add(Me.cbonature)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.lblvandorno)
        Me.RadGroupBox1.Controls.Add(Me.txtdesc)
        Me.RadGroupBox1.Controls.Add(Me.btnnew)
        Me.RadGroupBox1.Controls.Add(Me.lblvendorname)
        Me.RadGroupBox1.Controls.Add(Me.fndNo)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(8, 2)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(508, 384)
        Me.RadGroupBox1.TabIndex = 0
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(11, 158)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(41, 16)
        Me.MyLabel5.TabIndex = 41
        Me.MyLabel5.Text = "IS Ref."
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(10, 136)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel4.TabIndex = 40
        Me.MyLabel4.Text = "Clause Ref."
        '
        'txtISRef
        '
        Me.txtISRef.CalculationExpression = Nothing
        Me.txtISRef.FieldCode = Nothing
        Me.txtISRef.FieldDesc = Nothing
        Me.txtISRef.FieldMaxLength = 0
        Me.txtISRef.FieldName = Nothing
        Me.txtISRef.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtISRef.isCalculatedField = False
        Me.txtISRef.IsSourceFromTable = False
        Me.txtISRef.IsSourceFromValueList = False
        Me.txtISRef.IsUnique = False
        Me.txtISRef.Location = New System.Drawing.Point(86, 157)
        Me.txtISRef.MaxLength = 150
        Me.txtISRef.MendatroryField = True
        Me.txtISRef.MyLinkLable1 = Me.lblAliasName
        Me.txtISRef.MyLinkLable2 = Nothing
        Me.txtISRef.Name = "txtISRef"
        Me.txtISRef.ReferenceFieldDesc = Nothing
        Me.txtISRef.ReferenceFieldName = Nothing
        Me.txtISRef.ReferenceTableName = Nothing
        Me.txtISRef.Size = New System.Drawing.Size(383, 18)
        Me.txtISRef.TabIndex = 39
        Me.txtISRef.TabStop = False
        '
        'lblAliasName
        '
        Me.lblAliasName.FieldName = Nothing
        Me.lblAliasName.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblAliasName.Location = New System.Drawing.Point(11, 67)
        Me.lblAliasName.Name = "lblAliasName"
        Me.lblAliasName.Size = New System.Drawing.Size(64, 16)
        Me.lblAliasName.TabIndex = 36
        Me.lblAliasName.Text = "Alias Name"
        '
        'txtClsRef
        '
        Me.txtClsRef.CalculationExpression = Nothing
        Me.txtClsRef.FieldCode = Nothing
        Me.txtClsRef.FieldDesc = Nothing
        Me.txtClsRef.FieldMaxLength = 0
        Me.txtClsRef.FieldName = Nothing
        Me.txtClsRef.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtClsRef.isCalculatedField = False
        Me.txtClsRef.IsSourceFromTable = False
        Me.txtClsRef.IsSourceFromValueList = False
        Me.txtClsRef.IsUnique = False
        Me.txtClsRef.Location = New System.Drawing.Point(86, 135)
        Me.txtClsRef.MaxLength = 150
        Me.txtClsRef.MendatroryField = True
        Me.txtClsRef.MyLinkLable1 = Me.lblAliasName
        Me.txtClsRef.MyLinkLable2 = Nothing
        Me.txtClsRef.Name = "txtClsRef"
        Me.txtClsRef.ReferenceFieldDesc = Nothing
        Me.txtClsRef.ReferenceFieldName = Nothing
        Me.txtClsRef.ReferenceTableName = Nothing
        Me.txtClsRef.Size = New System.Drawing.Size(383, 18)
        Me.txtClsRef.TabIndex = 38
        Me.txtClsRef.TabStop = False
        '
        'txtAliasName
        '
        Me.txtAliasName.CalculationExpression = Nothing
        Me.txtAliasName.FieldCode = Nothing
        Me.txtAliasName.FieldDesc = Nothing
        Me.txtAliasName.FieldMaxLength = 0
        Me.txtAliasName.FieldName = Nothing
        Me.txtAliasName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAliasName.isCalculatedField = False
        Me.txtAliasName.IsSourceFromTable = False
        Me.txtAliasName.IsSourceFromValueList = False
        Me.txtAliasName.IsUnique = False
        Me.txtAliasName.Location = New System.Drawing.Point(86, 64)
        Me.txtAliasName.MaxLength = 150
        Me.txtAliasName.MendatroryField = True
        Me.txtAliasName.MyLinkLable1 = Me.lblAliasName
        Me.txtAliasName.MyLinkLable2 = Nothing
        Me.txtAliasName.Name = "txtAliasName"
        Me.txtAliasName.ReferenceFieldDesc = Nothing
        Me.txtAliasName.ReferenceFieldName = Nothing
        Me.txtAliasName.ReferenceTableName = Nothing
        Me.txtAliasName.Size = New System.Drawing.Size(383, 18)
        Me.txtAliasName.TabIndex = 37
        Me.txtAliasName.TabStop = False
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(12, 180)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(31, 16)
        Me.MyLabel3.TabIndex = 5
        Me.MyLabel3.Text = "Type"
        '
        'cmbtype
        '
        Me.cmbtype.AutoCompleteDisplayMember = Nothing
        Me.cmbtype.AutoCompleteValueMember = Nothing
        Me.cmbtype.CalculationExpression = Nothing
        Me.cmbtype.DropDownAnimationEnabled = True
        Me.cmbtype.FieldCode = Nothing
        Me.cmbtype.FieldDesc = Nothing
        Me.cmbtype.FieldMaxLength = 0
        Me.cmbtype.FieldName = Nothing
        Me.cmbtype.isCalculatedField = False
        Me.cmbtype.IsSourceFromTable = False
        Me.cmbtype.IsSourceFromValueList = False
        Me.cmbtype.IsUnique = False
        Me.cmbtype.Location = New System.Drawing.Point(86, 179)
        Me.cmbtype.MendatroryField = True
        Me.cmbtype.MyLinkLable1 = Me.MyLabel3
        Me.cmbtype.MyLinkLable2 = Nothing
        Me.cmbtype.Name = "cmbtype"
        Me.cmbtype.ReferenceFieldDesc = Nothing
        Me.cmbtype.ReferenceFieldName = Nothing
        Me.cmbtype.ReferenceTableName = Nothing
        Me.cmbtype.Size = New System.Drawing.Size(142, 20)
        Me.cmbtype.TabIndex = 6
        Me.cmbtype.Text = "MyComboBox1"
        '
        'chk_batch_no
        '
        Me.chk_batch_no.AutoSize = True
        Me.chk_batch_no.Location = New System.Drawing.Point(347, 88)
        Me.chk_batch_no.Name = "chk_batch_no"
        Me.chk_batch_no.Size = New System.Drawing.Size(98, 17)
        Me.chk_batch_no.TabIndex = 4
        Me.chk_batch_no.Text = "Pick Batch No."
        Me.chk_batch_no.UseVisualStyleBackColor = True
        '
        'chkReq_Para_Mst
        '
        Me.chkReq_Para_Mst.AutoSize = True
        Me.chkReq_Para_Mst.Location = New System.Drawing.Point(238, 181)
        Me.chkReq_Para_Mst.Name = "chkReq_Para_Mst"
        Me.chkReq_Para_Mst.Size = New System.Drawing.Size(178, 17)
        Me.chkReq_Para_Mst.TabIndex = 7
        Me.chkReq_Para_Mst.Text = "Required in Parameter Master"
        Me.chkReq_Para_Mst.UseVisualStyleBackColor = True
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.cbgUsers)
        Me.RadGroupBox2.Controls.Add(Me.Panel5)
        Me.RadGroupBox2.HeaderText = "Users"
        Me.RadGroupBox2.Location = New System.Drawing.Point(86, 203)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(383, 170)
        Me.RadGroupBox2.TabIndex = 8
        Me.RadGroupBox2.Text = "Users"
        '
        'cbgUsers
        '
        Me.cbgUsers.CheckedValue = Nothing
        Me.cbgUsers.DataSource = Nothing
        Me.cbgUsers.DisplayMember = "Name"
        Me.cbgUsers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgUsers.Location = New System.Drawing.Point(10, 40)
        Me.cbgUsers.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgUsers.MyShowHeadrText = False
        Me.cbgUsers.Name = "cbgUsers"
        Me.cbgUsers.Size = New System.Drawing.Size(363, 120)
        Me.cbgUsers.TabIndex = 0
        Me.cbgUsers.ValueMember = "Code"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.chkSelect)
        Me.Panel5.Controls.Add(Me.chkAll)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(10, 20)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(363, 20)
        Me.Panel5.TabIndex = 0
        '
        'chkSelect
        '
        Me.chkSelect.Location = New System.Drawing.Point(186, 1)
        Me.chkSelect.MyLinkLable1 = Nothing
        Me.chkSelect.MyLinkLable2 = Nothing
        Me.chkSelect.Name = "chkSelect"
        Me.chkSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkSelect.TabIndex = 1
        Me.chkSelect.Text = "Select"
        '
        'chkAll
        '
        Me.chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAll.Location = New System.Drawing.Point(63, 1)
        Me.chkAll.MyLinkLable1 = Nothing
        Me.chkAll.MyLinkLable2 = Nothing
        Me.chkAll.Name = "chkAll"
        Me.chkAll.Size = New System.Drawing.Size(33, 18)
        Me.chkAll.TabIndex = 0
        Me.chkAll.Text = "All"
        Me.chkAll.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'TxtDepart_desc
        '
        Me.TxtDepart_desc.AutoSize = False
        Me.TxtDepart_desc.BorderVisible = True
        Me.TxtDepart_desc.FieldName = Nothing
        Me.TxtDepart_desc.Location = New System.Drawing.Point(230, 111)
        Me.TxtDepart_desc.Name = "TxtDepart_desc"
        Me.TxtDepart_desc.Size = New System.Drawing.Size(239, 19)
        Me.TxtDepart_desc.TabIndex = 34
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(11, 111)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel2.TabIndex = 35
        Me.MyLabel2.Text = "Department"
        '
        'fndDepart_code
        '
        Me.fndDepart_code.CalculationExpression = Nothing
        Me.fndDepart_code.FieldCode = Nothing
        Me.fndDepart_code.FieldDesc = Nothing
        Me.fndDepart_code.FieldMaxLength = 0
        Me.fndDepart_code.FieldName = Nothing
        Me.fndDepart_code.isCalculatedField = False
        Me.fndDepart_code.IsSourceFromTable = False
        Me.fndDepart_code.IsSourceFromValueList = False
        Me.fndDepart_code.IsUnique = False
        Me.fndDepart_code.Location = New System.Drawing.Point(86, 111)
        Me.fndDepart_code.MendatroryField = True
        Me.fndDepart_code.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndDepart_code.MyLinkLable1 = Me.MyLabel2
        Me.fndDepart_code.MyLinkLable2 = Me.TxtDepart_desc
        Me.fndDepart_code.MyReadOnly = False
        Me.fndDepart_code.MyShowMasterFormButton = False
        Me.fndDepart_code.Name = "fndDepart_code"
        Me.fndDepart_code.ReferenceFieldDesc = Nothing
        Me.fndDepart_code.ReferenceFieldName = Nothing
        Me.fndDepart_code.ReferenceTableName = Nothing
        Me.fndDepart_code.Size = New System.Drawing.Size(142, 19)
        Me.fndDepart_code.TabIndex = 5
        Me.fndDepart_code.Value = ""
        '
        'chkIsmandatory
        '
        Me.chkIsmandatory.AutoSize = True
        Me.chkIsmandatory.Location = New System.Drawing.Point(248, 88)
        Me.chkIsmandatory.Name = "chkIsmandatory"
        Me.chkIsmandatory.Size = New System.Drawing.Size(93, 17)
        Me.chkIsmandatory.TabIndex = 3
        Me.chkIsmandatory.Text = "Is Mandatory"
        Me.chkIsmandatory.UseVisualStyleBackColor = True
        '
        'cbonature
        '
        Me.cbonature.AutoCompleteDisplayMember = Nothing
        Me.cbonature.AutoCompleteValueMember = Nothing
        Me.cbonature.CalculationExpression = Nothing
        Me.cbonature.DropDownAnimationEnabled = True
        Me.cbonature.FieldCode = Nothing
        Me.cbonature.FieldDesc = Nothing
        Me.cbonature.FieldMaxLength = 0
        Me.cbonature.FieldName = Nothing
        Me.cbonature.isCalculatedField = False
        Me.cbonature.IsSourceFromTable = False
        Me.cbonature.IsSourceFromValueList = False
        Me.cbonature.IsUnique = False
        Me.cbonature.Location = New System.Drawing.Point(86, 87)
        Me.cbonature.MendatroryField = True
        Me.cbonature.MyLinkLable1 = Me.MyLabel1
        Me.cbonature.MyLinkLable2 = Nothing
        Me.cbonature.Name = "cbonature"
        Me.cbonature.ReferenceFieldDesc = Nothing
        Me.cbonature.ReferenceFieldName = Nothing
        Me.cbonature.ReferenceTableName = Nothing
        Me.cbonature.Size = New System.Drawing.Size(142, 20)
        Me.cbonature.TabIndex = 2
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(11, 89)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(40, 16)
        Me.MyLabel1.TabIndex = 7
        Me.MyLabel1.Text = "Nature"
        '
        'lblvandorno
        '
        Me.lblvandorno.FieldName = Nothing
        Me.lblvandorno.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblvandorno.Location = New System.Drawing.Point(11, 14)
        Me.lblvandorno.Name = "lblvandorno"
        Me.lblvandorno.Size = New System.Drawing.Size(33, 16)
        Me.lblvandorno.TabIndex = 0
        Me.lblvandorno.Text = "Code"
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
        Me.txtdesc.Location = New System.Drawing.Point(86, 40)
        Me.txtdesc.MaxLength = 150
        Me.txtdesc.MendatroryField = True
        Me.txtdesc.MyLinkLable1 = Me.lblvendorname
        Me.txtdesc.MyLinkLable2 = Nothing
        Me.txtdesc.Name = "txtdesc"
        Me.txtdesc.ReferenceFieldDesc = Nothing
        Me.txtdesc.ReferenceFieldName = Nothing
        Me.txtdesc.ReferenceTableName = Nothing
        Me.txtdesc.Size = New System.Drawing.Size(383, 18)
        Me.txtdesc.TabIndex = 1
        Me.txtdesc.TabStop = False
        '
        'lblvendorname
        '
        Me.lblvendorname.FieldName = Nothing
        Me.lblvendorname.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblvendorname.Location = New System.Drawing.Point(11, 40)
        Me.lblvendorname.Name = "lblvendorname"
        Me.lblvendorname.Size = New System.Drawing.Size(63, 16)
        Me.lblvendorname.TabIndex = 3
        Me.lblvendorname.Text = "Description"
        '
        'btnnew
        '
        Me.btnnew.Image = Global.XpertERPProcessProduction.My.Resources.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(392, 14)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(15, 20)
        Me.btnnew.TabIndex = 0
        '
        'fndNo
        '
        Me.fndNo.FieldName = Nothing
        Me.fndNo.Location = New System.Drawing.Point(86, 13)
        Me.fndNo.MendatroryField = True
        Me.fndNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndNo.MyLinkLable1 = Me.lblvandorno
        Me.fndNo.MyLinkLable2 = Nothing
        Me.fndNo.MyMaxLength = 32767
        Me.fndNo.MyReadOnly = False
        Me.fndNo.Name = "fndNo"
        Me.fndNo.Size = New System.Drawing.Size(301, 21)
        Me.fndNo.TabIndex = 1
        Me.fndNo.TabStop = False
        Me.fndNo.Value = ""
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.UcAttachment1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(75.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(548, 332)
        Me.RadPageViewPage2.Text = "Attachment"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(548, 332)
        Me.UcAttachment1.TabIndex = 1
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(756, 5)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(78, 21)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(93, 5)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(78, 21)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(9, 5)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(78, 21)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'frmPPLogSheetMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(842, 526)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmPPLogSheetMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmParameterMaster"
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
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtISRef, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAliasName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtClsRef, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAliasName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbtype, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        CType(Me.chkSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtDepart_desc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbonature, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblvandorno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblvendorname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
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
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents MenuClose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents lblvandorno As common.Controls.MyLabel
    Friend WithEvents txtdesc As common.Controls.MyTextBox
    Friend WithEvents lblvendorname As common.Controls.MyLabel
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndNo As common.UserControls.txtNavigator
    Friend WithEvents cmbtype As common.Controls.MyComboBox
    Friend WithEvents btnexport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnimport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents cbonature As common.Controls.MyComboBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents chkIsmandatory As System.Windows.Forms.CheckBox
    Friend WithEvents chkReq_Para_Mst As System.Windows.Forms.CheckBox
    Friend WithEvents TxtDepart_desc As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents fndDepart_code As common.UserControls.txtFinder
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgUsers As common.MyCheckBoxGrid
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents chkSelect As common.Controls.MyRadioButton
    Friend WithEvents chkAll As common.Controls.MyRadioButton
    Friend WithEvents btnEx_detail As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnEx_User As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnIm_Detail As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnIm_Users As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents chk_batch_no As System.Windows.Forms.CheckBox
    Friend WithEvents lblAliasName As common.Controls.MyLabel
    Friend WithEvents txtAliasName As common.Controls.MyTextBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtISRef As common.Controls.MyTextBox
    Friend WithEvents txtClsRef As common.Controls.MyTextBox
End Class

