<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmProcessProductionLogSheet
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmProcessProductionLogSheet))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.txtdesc = New common.Controls.MyTextBox
        Me.lblvendorname = New common.Controls.MyLabel
        Me.chkMannual = New common.Controls.MyCheckBox
        Me.cbodiff = New common.Controls.MyComboBox
        Me.MyLabel5 = New common.Controls.MyLabel
        Me.txtdiff = New common.MyNumBox
        Me.btngo = New Telerik.WinControls.UI.RadButton
        Me.txtend_time = New common.Controls.MyDateTimePicker
        Me.MyLabel4 = New common.Controls.MyLabel
        Me.txtstart_time = New common.Controls.MyDateTimePicker
        Me.MyLabel3 = New common.Controls.MyLabel
        Me.txtstagecode = New common.UserControls.txtFinder
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.txtstagename = New common.Controls.MyLabel
        Me.lblCode = New common.Controls.MyLabel
        Me.lblBomDate = New common.Controls.MyLabel
        Me.dtpDate = New common.Controls.MyDateTimePicker
        Me.btnNew = New Telerik.WinControls.UI.RadButton
        Me.txtCode = New common.UserControls.txtNavigator
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.gv = New common.UserControls.MyRadGridView
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage
        Me.txtsequnce = New common.MyNumBox
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.gv_Param = New common.UserControls.MyRadGridView
        Me.lblMasterItem = New common.Controls.MyLabel
        Me.txtcategorycode = New common.UserControls.txtFinder
        Me.txtcategoryname = New common.Controls.MyLabel
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.btndelete = New Telerik.WinControls.UI.RadButton
        Me.btnsave = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.txtdesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblvendorname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMannual, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbodiff, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdiff, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btngo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtend_time, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtstart_time, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtstagename, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBomDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage3.SuspendLayout()
        CType(Me.txtsequnce, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv_Param, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_Param.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMasterItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcategoryname, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu1)
        Me.SplitContainer1.Panel1.Padding = New System.Windows.Forms.Padding(3)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtsequnce)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lblMasterItem)
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtcategorycode)
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtcategoryname)
        Me.SplitContainer1.Size = New System.Drawing.Size(754, 435)
        Me.SplitContainer1.SplitterDistance = 397
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(3, 23)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage3
        Me.RadPageView1.Size = New System.Drawing.Size(748, 371)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer2)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(85.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(727, 323)
        Me.RadPageViewPage1.Text = "QC Log Sheet"
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtdesc)
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkMannual)
        Me.SplitContainer2.Panel1.Controls.Add(Me.cbodiff)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblvendorname)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtdiff)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btngo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtend_time)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtstart_time)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtstagecode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtstagename)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblBomDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.dtpDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer2.Panel1.Padding = New System.Windows.Forms.Padding(3)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadGroupBox1)
        Me.SplitContainer2.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.SplitContainer2.Size = New System.Drawing.Size(727, 323)
        Me.SplitContainer2.SplitterDistance = 105
        Me.SplitContainer2.TabIndex = 0
        '
        'txtdesc
        '
        Me.txtdesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdesc.Location = New System.Drawing.Point(129, 37)
        Me.txtdesc.MaxLength = 150
        Me.txtdesc.MendatroryField = False
        Me.txtdesc.MyLinkLable1 = Me.lblvendorname
        Me.txtdesc.MyLinkLable2 = Nothing
        Me.txtdesc.Name = "txtdesc"
        Me.txtdesc.Size = New System.Drawing.Size(532, 18)
        Me.txtdesc.TabIndex = 2
        Me.txtdesc.TabStop = False
        '
        'lblvendorname
        '
        Me.lblvendorname.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblvendorname.Location = New System.Drawing.Point(9, 37)
        Me.lblvendorname.Name = "lblvendorname"
        Me.lblvendorname.Size = New System.Drawing.Size(63, 16)
        Me.lblvendorname.TabIndex = 48
        Me.lblvendorname.Text = "Description"
        '
        'chkMannual
        '
        Me.chkMannual.Location = New System.Drawing.Point(589, 58)
        Me.chkMannual.MyLinkLable1 = Nothing
        Me.chkMannual.MyLinkLable2 = Nothing
        Me.chkMannual.Name = "chkMannual"
        Me.chkMannual.Size = New System.Drawing.Size(75, 18)
        Me.chkMannual.TabIndex = 4
        Me.chkMannual.Tag1 = Nothing
        Me.chkMannual.Text = "Is Mannual"
        '
        'cbodiff
        '
        Me.cbodiff.AllowShowFocusCues = False
        Me.cbodiff.AutoCompleteDisplayMember = Nothing
        Me.cbodiff.AutoCompleteValueMember = Nothing
        Me.cbodiff.Location = New System.Drawing.Point(555, 80)
        Me.cbodiff.MendatroryField = False
        Me.cbodiff.MyLinkLable1 = Me.MyLabel5
        Me.cbodiff.MyLinkLable2 = Nothing
        Me.cbodiff.Name = "cbodiff"
        Me.cbodiff.Size = New System.Drawing.Size(80, 20)
        Me.cbodiff.TabIndex = 8
        Me.cbodiff.Text = "MyComboBox1"
        '
        'MyLabel5
        '
        Me.MyLabel5.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel5.Location = New System.Drawing.Point(451, 80)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(57, 18)
        Me.MyLabel5.TabIndex = 44
        Me.MyLabel5.Text = "Difference"
        '
        'txtdiff
        '
        Me.txtdiff.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtdiff.DecimalPlaces = 0
        Me.txtdiff.Location = New System.Drawing.Point(511, 80)
        Me.txtdiff.MaxLength = 3
        Me.txtdiff.MendatroryField = True
        Me.txtdiff.MyLinkLable1 = Me.MyLabel5
        Me.txtdiff.MyLinkLable2 = Nothing
        Me.txtdiff.Name = "txtdiff"
        Me.txtdiff.Size = New System.Drawing.Size(43, 20)
        Me.txtdiff.TabIndex = 7
        Me.txtdiff.Text = "0"
        Me.txtdiff.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtdiff.Value = 0
        '
        'btngo
        '
        Me.btngo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btngo.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btngo.Location = New System.Drawing.Point(636, 80)
        Me.btngo.Name = "btngo"
        Me.btngo.Size = New System.Drawing.Size(25, 20)
        Me.btngo.TabIndex = 9
        Me.btngo.Text = ">>"
        '
        'txtend_time
        '
        Me.txtend_time.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtend_time.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtend_time.Location = New System.Drawing.Point(320, 80)
        Me.txtend_time.MendatroryField = True
        Me.txtend_time.MinDate = New Date(1973, 1, 1, 0, 0, 0, 0)
        Me.txtend_time.MyLinkLable1 = Me.MyLabel4
        Me.txtend_time.MyLinkLable2 = Nothing
        Me.txtend_time.Name = "txtend_time"
        Me.txtend_time.NullDate = New Date(1973, 1, 1, 0, 0, 0, 0)
        Me.txtend_time.ShowUpDown = True
        Me.txtend_time.Size = New System.Drawing.Size(130, 20)
        Me.txtend_time.TabIndex = 6
        Me.txtend_time.TabStop = False
        Me.txtend_time.Text = "13/08/2014 12:23 PM"
        Me.txtend_time.Value = New Date(2014, 8, 13, 12, 23, 18, 908)
        '
        'MyLabel4
        '
        Me.MyLabel4.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel4.Location = New System.Drawing.Point(261, 82)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(52, 18)
        Me.MyLabel4.TabIndex = 43
        Me.MyLabel4.Text = "End Time"
        '
        'txtstart_time
        '
        Me.txtstart_time.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtstart_time.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtstart_time.Location = New System.Drawing.Point(129, 81)
        Me.txtstart_time.MendatroryField = True
        Me.txtstart_time.MinDate = New Date(1973, 1, 1, 0, 0, 0, 0)
        Me.txtstart_time.MyLinkLable1 = Me.MyLabel3
        Me.txtstart_time.MyLinkLable2 = Nothing
        Me.txtstart_time.Name = "txtstart_time"
        Me.txtstart_time.NullDate = New Date(1973, 1, 1, 0, 0, 0, 0)
        Me.txtstart_time.ShowUpDown = True
        Me.txtstart_time.Size = New System.Drawing.Size(130, 20)
        Me.txtstart_time.TabIndex = 5
        Me.txtstart_time.TabStop = False
        Me.txtstart_time.Text = "13/08/2014 12:23 PM"
        Me.txtstart_time.Value = New Date(2014, 8, 13, 12, 23, 18, 908)
        '
        'MyLabel3
        '
        Me.MyLabel3.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel3.Location = New System.Drawing.Point(9, 82)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(57, 18)
        Me.MyLabel3.TabIndex = 42
        Me.MyLabel3.Text = "Start Time"
        '
        'txtstagecode
        '
        Me.txtstagecode.Location = New System.Drawing.Point(129, 58)
        Me.txtstagecode.MendatroryField = True
        Me.txtstagecode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtstagecode.MyLinkLable1 = Me.MyLabel1
        Me.txtstagecode.MyLinkLable2 = Me.txtstagename
        Me.txtstagecode.MyReadOnly = False
        Me.txtstagecode.MyShowMasterFormButton = False
        Me.txtstagecode.Name = "txtstagecode"
        Me.txtstagecode.Size = New System.Drawing.Size(153, 19)
        Me.txtstagecode.TabIndex = 3
        Me.txtstagecode.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(9, 58)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(66, 18)
        Me.MyLabel1.TabIndex = 41
        Me.MyLabel1.Text = "Stage Detail"
        '
        'txtstagename
        '
        Me.txtstagename.AutoSize = False
        Me.txtstagename.BorderVisible = True
        Me.txtstagename.Location = New System.Drawing.Point(286, 58)
        Me.txtstagename.Name = "txtstagename"
        Me.txtstagename.Size = New System.Drawing.Size(300, 19)
        Me.txtstagename.TabIndex = 40
        Me.txtstagename.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCode
        '
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCode.Location = New System.Drawing.Point(9, 15)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(88, 16)
        Me.lblCode.TabIndex = 9
        Me.lblCode.Text = "Log Sheet Code"
        '
        'lblBomDate
        '
        Me.lblBomDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBomDate.Location = New System.Drawing.Point(542, 16)
        Me.lblBomDate.Name = "lblBomDate"
        Me.lblBomDate.Size = New System.Drawing.Size(30, 16)
        Me.lblBomDate.TabIndex = 10
        Me.lblBomDate.Text = "Date"
        '
        'dtpDate
        '
        Me.dtpDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDate.Location = New System.Drawing.Point(575, 15)
        Me.dtpDate.MendatroryField = True
        Me.dtpDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDate.MyLinkLable1 = Me.lblBomDate
        Me.dtpDate.MyLinkLable2 = Nothing
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDate.Size = New System.Drawing.Size(86, 18)
        Me.dtpDate.TabIndex = 1
        Me.dtpDate.TabStop = False
        Me.dtpDate.Text = "03/05/2011"
        Me.dtpDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(426, 13)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 20)
        Me.btnNew.TabIndex = 0
        Me.btnNew.Text = " "
        '
        'txtCode
        '
        Me.txtCode.Location = New System.Drawing.Point(129, 13)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 30
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(292, 21)
        Me.txtCode.TabIndex = 8
        Me.txtCode.Value = ""
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.gv)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = "QC Detail"
        Me.RadGroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(721, 208)
        Me.RadGroupBox1.TabIndex = 0
        Me.RadGroupBox1.Text = "QC Detail"
        '
        'gv
        '
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Location = New System.Drawing.Point(2, 18)
        '
        'gv
        '
        Me.gv.MasterTemplate.AllowDragToGroup = False
        Me.gv.MasterTemplate.EnableFiltering = True
        Me.gv.MasterTemplate.EnableGrouping = False
        Me.gv.Name = "gv"
        Me.gv.ShowGroupPanel = False
        Me.gv.Size = New System.Drawing.Size(717, 188)
        Me.gv.TabIndex = 0
        Me.gv.Text = "RadGridView1"
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(105.0!, 28.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(727, 323)
        Me.RadPageViewPage3.Text = "Parameter Option"
        '
        'txtsequnce
        '
        Me.txtsequnce.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtsequnce.DecimalPlaces = 0
        Me.txtsequnce.Location = New System.Drawing.Point(421, 10)
        Me.txtsequnce.MaxLength = 10
        Me.txtsequnce.MendatroryField = True
        Me.txtsequnce.MyLinkLable1 = Me.MyLabel2
        Me.txtsequnce.MyLinkLable2 = Nothing
        Me.txtsequnce.Name = "txtsequnce"
        Me.txtsequnce.Size = New System.Drawing.Size(28, 20)
        Me.txtsequnce.TabIndex = 3
        Me.txtsequnce.Text = "0"
        Me.txtsequnce.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtsequnce.Value = 0
        Me.txtsequnce.Visible = False
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel2.Location = New System.Drawing.Point(340, 10)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(75, 18)
        Me.MyLabel2.TabIndex = 45
        Me.MyLabel2.Text = "Sequence No."
        Me.MyLabel2.Visible = False
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.gv_Param)
        Me.RadGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox2.HeaderText = "Parameter Option"
        Me.RadGroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Size = New System.Drawing.Size(727, 323)
        Me.RadGroupBox2.TabIndex = 0
        Me.RadGroupBox2.Text = "Parameter Option"
        '
        'gv_Param
        '
        Me.gv_Param.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv_Param.Location = New System.Drawing.Point(2, 18)
        '
        'gv_Param
        '
        Me.gv_Param.MasterTemplate.AllowDragToGroup = False
        Me.gv_Param.MasterTemplate.EnableFiltering = True
        Me.gv_Param.MasterTemplate.EnableGrouping = False
        Me.gv_Param.Name = "gv_Param"
        Me.gv_Param.ShowGroupPanel = False
        Me.gv_Param.Size = New System.Drawing.Size(723, 303)
        Me.gv_Param.TabIndex = 0
        Me.gv_Param.Text = "RadGridView1"
        '
        'lblMasterItem
        '
        Me.lblMasterItem.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblMasterItem.Location = New System.Drawing.Point(186, 9)
        Me.lblMasterItem.Name = "lblMasterItem"
        Me.lblMasterItem.Size = New System.Drawing.Size(109, 18)
        Me.lblMasterItem.TabIndex = 38
        Me.lblMasterItem.Text = "Production Category"
        Me.lblMasterItem.Visible = False
        '
        'txtcategorycode
        '
        Me.txtcategorycode.Location = New System.Drawing.Point(299, 10)
        Me.txtcategorycode.MendatroryField = True
        Me.txtcategorycode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcategorycode.MyLinkLable1 = Me.lblMasterItem
        Me.txtcategorycode.MyLinkLable2 = Me.txtcategoryname
        Me.txtcategorycode.MyReadOnly = False
        Me.txtcategorycode.MyShowMasterFormButton = False
        Me.txtcategorycode.Name = "txtcategorycode"
        Me.txtcategorycode.Size = New System.Drawing.Size(19, 19)
        Me.txtcategorycode.TabIndex = 1
        Me.txtcategorycode.Value = ""
        Me.txtcategorycode.Visible = False
        '
        'txtcategoryname
        '
        Me.txtcategoryname.AutoSize = False
        Me.txtcategoryname.BorderVisible = True
        Me.txtcategoryname.Location = New System.Drawing.Point(324, 10)
        Me.txtcategoryname.Name = "txtcategoryname"
        Me.txtcategoryname.Size = New System.Drawing.Size(10, 19)
        Me.txtcategoryname.TabIndex = 37
        Me.txtcategoryname.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtcategoryname.Visible = False
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.UcAttachment1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(75.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(727, 323)
        Me.RadPageViewPage2.Text = "Attachment"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(727, 323)
        Me.UcAttachment1.TabIndex = 5
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenufile})
        Me.RadMenu1.Location = New System.Drawing.Point(3, 3)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(748, 20)
        Me.RadMenu1.TabIndex = 1
        Me.RadMenu1.Text = "RadMenu1"
        Me.RadMenu1.Visible = False
        '
        'rdmenufile
        '
        Me.rdmenufile.AccessibleDescription = "RadMenuItem1"
        Me.rdmenufile.AccessibleName = "RadMenuItem1"
        Me.rdmenufile.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Text = "File"
        Me.rdmenufile.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "RadMenuItem1"
        Me.RadMenuItem1.AccessibleName = "RadMenuItem1"
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "RadMenuItem1"
        Me.RadMenuItem1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(672, 8)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(73, 20)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(88, 8)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(73, 20)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(9, 8)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(73, 20)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'FrmProcessProductionLogSheet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(754, 435)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmProcessProductionLogSheet"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmProcessProductionLogSheet"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.txtdesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblvendorname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMannual, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbodiff, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdiff, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btngo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtend_time, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtstart_time, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtstagename, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBomDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage3.ResumeLayout(False)
        CType(Me.txtsequnce, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv_Param.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_Param, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMasterItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcategoryname, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents lblBomDate As common.Controls.MyLabel
    Friend WithEvents dtpDate As common.Controls.MyDateTimePicker
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtstagecode As common.UserControls.txtFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtstagename As common.Controls.MyLabel
    Friend WithEvents txtcategorycode As common.UserControls.txtFinder
    Friend WithEvents lblMasterItem As common.Controls.MyLabel
    Friend WithEvents txtcategoryname As common.Controls.MyLabel
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents txtend_time As common.Controls.MyDateTimePicker
    Friend WithEvents txtstart_time As common.Controls.MyDateTimePicker
    Friend WithEvents btngo As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtdiff As common.MyNumBox
    Friend WithEvents cbodiff As common.Controls.MyComboBox
    Friend WithEvents txtsequnce As common.MyNumBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv_Param As common.UserControls.MyRadGridView
    Friend WithEvents RadPageViewPage3 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents chkMannual As common.Controls.MyCheckBox
    Friend WithEvents txtdesc As common.Controls.MyTextBox
    Friend WithEvents lblvendorname As common.Controls.MyLabel
End Class

