<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPurchaseSchedule
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
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.txtRev_No = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.btnMonthly = New common.Controls.MyRadioButton()
        Me.txtPODesc = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtPONo = New common.UserControls.txtFinder()
        Me.dtpMonth = New common.Controls.MyDateTimePicker()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.btnDaily = New common.Controls.MyRadioButton()
        Me.btnWeek = New common.Controls.MyRadioButton()
        Me.UsLock1 = New common.usLock()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.cboPOType = New common.Controls.MyComboBox()
        Me.RadLabel8 = New common.Controls.MyLabel()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.lblVendorName = New common.Controls.MyLabel()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.txtDesc = New common.Controls.MyTextBox()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.txtVendorNo = New common.UserControls.txtFinder()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.btnSamePlan = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv_Vendor = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnunpost = New Telerik.WinControls.UI.RadButton()
        Me.btnpost = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.txtRev_No, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnMonthly, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPODesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpMonth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDaily, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnWeek, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboPOType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendorName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.btnSamePlan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.gv_Vendor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_Vendor.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage3.SuspendLayout()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnunpost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnpost, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Padding = New System.Windows.Forms.Padding(1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnunpost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnpost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Padding = New System.Windows.Forms.Padding(1)
        Me.SplitContainer1.Size = New System.Drawing.Size(867, 494)
        Me.SplitContainer1.SplitterDistance = 458
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(1, 1)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(865, 456)
        Me.RadPageView1.TabIndex = 0
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer2)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(89.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(844, 408)
        Me.RadPageViewPage1.Text = "PO Scheduling"
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtRev_No)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnMonthly)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtPODesc)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtPONo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.dtpMonth)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnDaily)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnWeek)
        Me.SplitContainer2.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer2.Panel1.Controls.Add(Me.cboPOType)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel8)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDocNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblVendorName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel4)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDesc)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtVendorNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel3)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadGroupBox2)
        Me.SplitContainer2.Panel2.Controls.Add(Me.Panel1)
        Me.SplitContainer2.Panel2.Padding = New System.Windows.Forms.Padding(1)
        Me.SplitContainer2.Size = New System.Drawing.Size(844, 408)
        Me.SplitContainer2.SplitterDistance = 150
        Me.SplitContainer2.TabIndex = 64
        '
        'txtRev_No
        '
        Me.txtRev_No.AutoSize = False
        Me.txtRev_No.BorderVisible = True
        Me.txtRev_No.FieldName = Nothing
        Me.txtRev_No.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRev_No.Location = New System.Drawing.Point(89, 127)
        Me.txtRev_No.Name = "txtRev_No"
        Me.txtRev_No.Size = New System.Drawing.Size(550, 18)
        Me.txtRev_No.TabIndex = 71
        Me.txtRev_No.TextWrap = False
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(7, 128)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(67, 16)
        Me.MyLabel2.TabIndex = 70
        Me.MyLabel2.Text = "Revision No"
        '
        'btnMonthly
        '
        Me.btnMonthly.Location = New System.Drawing.Point(532, 82)
        Me.btnMonthly.MyLinkLable1 = Nothing
        Me.btnMonthly.MyLinkLable2 = Nothing
        Me.btnMonthly.Name = "btnMonthly"
        Me.btnMonthly.Size = New System.Drawing.Size(110, 18)
        Me.btnMonthly.TabIndex = 8
        Me.btnMonthly.Text = "Monthly Schedule"
        '
        'txtPODesc
        '
        Me.txtPODesc.AutoSize = False
        Me.txtPODesc.BorderVisible = True
        Me.txtPODesc.FieldName = Nothing
        Me.txtPODesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPODesc.Location = New System.Drawing.Point(219, 106)
        Me.txtPODesc.Name = "txtPODesc"
        Me.txtPODesc.Size = New System.Drawing.Size(420, 18)
        Me.txtPODesc.TabIndex = 69
        Me.txtPODesc.TextWrap = False
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(7, 107)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(40, 16)
        Me.MyLabel3.TabIndex = 68
        Me.MyLabel3.Text = "PO No"
        '
        'txtPONo
        '
        Me.txtPONo.CalculationExpression = Nothing
        Me.txtPONo.FieldCode = Nothing
        Me.txtPONo.FieldDesc = Nothing
        Me.txtPONo.FieldMaxLength = 0
        Me.txtPONo.FieldName = Nothing
        Me.txtPONo.isCalculatedField = False
        Me.txtPONo.IsSourceFromTable = False
        Me.txtPONo.IsSourceFromValueList = False
        Me.txtPONo.IsUnique = False
        Me.txtPONo.Location = New System.Drawing.Point(89, 106)
        Me.txtPONo.MendatroryField = True
        Me.txtPONo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPONo.MyLinkLable1 = Me.MyLabel3
        Me.txtPONo.MyLinkLable2 = Me.txtPODesc
        Me.txtPONo.MyReadOnly = False
        Me.txtPONo.MyShowMasterFormButton = False
        Me.txtPONo.Name = "txtPONo"
        Me.txtPONo.ReferenceFieldDesc = Nothing
        Me.txtPONo.ReferenceFieldName = Nothing
        Me.txtPONo.ReferenceTableName = Nothing
        Me.txtPONo.Size = New System.Drawing.Size(129, 18)
        Me.txtPONo.TabIndex = 9
        Me.txtPONo.Value = ""
        '
        'dtpMonth
        '
        Me.dtpMonth.CalculationExpression = Nothing
        Me.dtpMonth.CustomFormat = "MMM yyyy"
        Me.dtpMonth.FieldCode = Nothing
        Me.dtpMonth.FieldDesc = Nothing
        Me.dtpMonth.FieldMaxLength = 0
        Me.dtpMonth.FieldName = Nothing
        Me.dtpMonth.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpMonth.isCalculatedField = False
        Me.dtpMonth.IsSourceFromTable = False
        Me.dtpMonth.IsSourceFromValueList = False
        Me.dtpMonth.IsUnique = False
        Me.dtpMonth.Location = New System.Drawing.Point(265, 82)
        Me.dtpMonth.MendatroryField = True
        Me.dtpMonth.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpMonth.MyLinkLable1 = Me.MyLabel1
        Me.dtpMonth.MyLinkLable2 = Nothing
        Me.dtpMonth.Name = "dtpMonth"
        Me.dtpMonth.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpMonth.ReferenceFieldDesc = Nothing
        Me.dtpMonth.ReferenceFieldName = Nothing
        Me.dtpMonth.ReferenceTableName = Nothing
        Me.dtpMonth.Size = New System.Drawing.Size(68, 18)
        Me.dtpMonth.TabIndex = 5
        Me.dtpMonth.TabStop = False
        Me.dtpMonth.Text = "Jun 2011"
        Me.dtpMonth.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(221, 82)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(40, 18)
        Me.MyLabel1.TabIndex = 66
        Me.MyLabel1.Text = "Month"
        '
        'btnDaily
        '
        Me.btnDaily.Location = New System.Drawing.Point(334, 82)
        Me.btnDaily.MyLinkLable1 = Nothing
        Me.btnDaily.MyLinkLable2 = Nothing
        Me.btnDaily.Name = "btnDaily"
        Me.btnDaily.Size = New System.Drawing.Size(93, 18)
        Me.btnDaily.TabIndex = 6
        Me.btnDaily.Text = "Daily Schedule"
        '
        'btnWeek
        '
        Me.btnWeek.Location = New System.Drawing.Point(428, 82)
        Me.btnWeek.MyLinkLable1 = Nothing
        Me.btnWeek.MyLinkLable2 = Nothing
        Me.btnWeek.Name = "btnWeek"
        Me.btnWeek.Size = New System.Drawing.Size(105, 18)
        Me.btnWeek.TabIndex = 7
        Me.btnWeek.Text = "Weekly Schedule"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(541, 12)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 63
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(7, 13)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(75, 16)
        Me.RadLabel1.TabIndex = 52
        Me.RadLabel1.Text = "Document No"
        '
        'btnNew
        '
        Me.btnNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnNew.Location = New System.Drawing.Point(342, 12)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 20)
        Me.btnNew.TabIndex = 0
        '
        'cboPOType
        '
        Me.cboPOType.AutoCompleteDisplayMember = Nothing
        Me.cboPOType.AutoCompleteValueMember = Nothing
        Me.cboPOType.CalculationExpression = Nothing
        Me.cboPOType.DropDownAnimationEnabled = True
        Me.cboPOType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboPOType.FieldCode = Nothing
        Me.cboPOType.FieldDesc = Nothing
        Me.cboPOType.FieldMaxLength = 0
        Me.cboPOType.FieldName = Nothing
        Me.cboPOType.isCalculatedField = False
        Me.cboPOType.IsSourceFromTable = False
        Me.cboPOType.IsSourceFromValueList = False
        Me.cboPOType.IsUnique = False
        Me.cboPOType.Location = New System.Drawing.Point(89, 81)
        Me.cboPOType.MendatroryField = True
        Me.cboPOType.MyLinkLable1 = Me.RadLabel8
        Me.cboPOType.MyLinkLable2 = Nothing
        Me.cboPOType.Name = "cboPOType"
        Me.cboPOType.ReferenceFieldDesc = Nothing
        Me.cboPOType.ReferenceFieldName = Nothing
        Me.cboPOType.ReferenceTableName = Nothing
        Me.cboPOType.Size = New System.Drawing.Size(129, 20)
        Me.cboPOType.TabIndex = 4
        '
        'RadLabel8
        '
        Me.RadLabel8.FieldName = Nothing
        Me.RadLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel8.Location = New System.Drawing.Point(7, 83)
        Me.RadLabel8.Name = "RadLabel8"
        Me.RadLabel8.Size = New System.Drawing.Size(51, 16)
        Me.RadLabel8.TabIndex = 62
        Me.RadLabel8.Text = "PO Type"
        '
        'txtDate
        '
        Me.txtDate.CalculationExpression = Nothing
        Me.txtDate.CustomFormat = "dd/MM/yyyy"
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
        Me.txtDate.Location = New System.Drawing.Point(453, 13)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.RadLabel4
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(83, 18)
        Me.txtDate.TabIndex = 1
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(418, 14)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 53
        Me.RadLabel4.Text = "Date"
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(89, 12)
        Me.txtDocNo.MendatroryField = False
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Me.RadLabel1
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 30
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(250, 20)
        Me.txtDocNo.TabIndex = 49
        Me.txtDocNo.Value = ""
        '
        'lblVendorName
        '
        Me.lblVendorName.AutoSize = False
        Me.lblVendorName.BorderVisible = True
        Me.lblVendorName.FieldName = Nothing
        Me.lblVendorName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVendorName.Location = New System.Drawing.Point(219, 58)
        Me.lblVendorName.Name = "lblVendorName"
        Me.lblVendorName.Size = New System.Drawing.Size(420, 18)
        Me.lblVendorName.TabIndex = 60
        Me.lblVendorName.TextWrap = False
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(7, 59)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(60, 16)
        Me.RadLabel2.TabIndex = 59
        Me.RadLabel2.Text = "Vendor No"
        '
        'txtDesc
        '
        Me.txtDesc.CalculationExpression = Nothing
        Me.txtDesc.FieldCode = Nothing
        Me.txtDesc.FieldDesc = Nothing
        Me.txtDesc.FieldMaxLength = 0
        Me.txtDesc.FieldName = Nothing
        Me.txtDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc.isCalculatedField = False
        Me.txtDesc.IsSourceFromTable = False
        Me.txtDesc.IsSourceFromValueList = False
        Me.txtDesc.IsUnique = False
        Me.txtDesc.Location = New System.Drawing.Point(87, 36)
        Me.txtDesc.MaxLength = 200
        Me.txtDesc.MendatroryField = False
        Me.txtDesc.MyLinkLable1 = Me.RadLabel3
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ReferenceFieldDesc = Nothing
        Me.txtDesc.ReferenceFieldName = Nothing
        Me.txtDesc.ReferenceTableName = Nothing
        Me.txtDesc.Size = New System.Drawing.Size(552, 18)
        Me.txtDesc.TabIndex = 2
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(7, 37)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel3.TabIndex = 55
        Me.RadLabel3.Text = "Description"
        '
        'txtVendorNo
        '
        Me.txtVendorNo.CalculationExpression = Nothing
        Me.txtVendorNo.FieldCode = Nothing
        Me.txtVendorNo.FieldDesc = Nothing
        Me.txtVendorNo.FieldMaxLength = 0
        Me.txtVendorNo.FieldName = Nothing
        Me.txtVendorNo.isCalculatedField = False
        Me.txtVendorNo.IsSourceFromTable = False
        Me.txtVendorNo.IsSourceFromValueList = False
        Me.txtVendorNo.IsUnique = False
        Me.txtVendorNo.Location = New System.Drawing.Point(89, 58)
        Me.txtVendorNo.MendatroryField = True
        Me.txtVendorNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVendorNo.MyLinkLable1 = Me.RadLabel2
        Me.txtVendorNo.MyLinkLable2 = Me.lblVendorName
        Me.txtVendorNo.MyReadOnly = False
        Me.txtVendorNo.MyShowMasterFormButton = False
        Me.txtVendorNo.Name = "txtVendorNo"
        Me.txtVendorNo.ReferenceFieldDesc = Nothing
        Me.txtVendorNo.ReferenceFieldName = Nothing
        Me.txtVendorNo.ReferenceTableName = Nothing
        Me.txtVendorNo.Size = New System.Drawing.Size(129, 18)
        Me.txtVendorNo.TabIndex = 3
        Me.txtVendorNo.Value = ""
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.gv1)
        Me.RadGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "Item Details"
        Me.RadGroupBox2.Location = New System.Drawing.Point(1, 1)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(842, 233)
        Me.RadGroupBox2.TabIndex = 0
        Me.RadGroupBox2.Text = "Item Details"
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(10, 20)
        '
        '
        '
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(822, 203)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.MyLabel10)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(1, 234)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(842, 19)
        Me.Panel1.TabIndex = 1
        '
        'MyLabel10
        '
        Me.MyLabel10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.ForeColor = System.Drawing.Color.Blue
        Me.MyLabel10.Location = New System.Drawing.Point(547, 1)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(290, 16)
        Me.MyLabel10.TabIndex = 47
        Me.MyLabel10.Text = "Click Alt+Shift+Control+F12 for Amend the Schedule"
        Me.MyLabel10.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.SplitContainer3)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(111.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(844, 408)
        Me.RadPageViewPage2.Text = "Plan given by Party"
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer3.IsSplitterFixed = True
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.btnSamePlan)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.RadGroupBox1)
        Me.SplitContainer3.Panel2.Padding = New System.Windows.Forms.Padding(1)
        Me.SplitContainer3.Size = New System.Drawing.Size(844, 408)
        Me.SplitContainer3.SplitterDistance = 31
        Me.SplitContainer3.TabIndex = 0
        '
        'btnSamePlan
        '
        Me.btnSamePlan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSamePlan.Location = New System.Drawing.Point(11, 4)
        Me.btnSamePlan.Name = "btnSamePlan"
        Me.btnSamePlan.Size = New System.Drawing.Size(276, 21)
        Me.btnSamePlan.TabIndex = 0
        Me.btnSamePlan.Text = "For same plan Click Here"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.gv_Vendor)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = "Item Details"
        Me.RadGroupBox1.Location = New System.Drawing.Point(1, 1)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(842, 371)
        Me.RadGroupBox1.TabIndex = 0
        Me.RadGroupBox1.Text = "Item Details"
        '
        'gv_Vendor
        '
        Me.gv_Vendor.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv_Vendor.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv_Vendor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv_Vendor.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv_Vendor.ForeColor = System.Drawing.Color.Black
        Me.gv_Vendor.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv_Vendor.Location = New System.Drawing.Point(10, 20)
        '
        '
        '
        Me.gv_Vendor.MasterTemplate.AllowDeleteRow = False
        Me.gv_Vendor.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv_Vendor.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv_Vendor.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gv_Vendor.MyStopExport = False
        Me.gv_Vendor.Name = "gv_Vendor"
        Me.gv_Vendor.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv_Vendor.ShowGroupPanel = False
        Me.gv_Vendor.ShowHeaderCellButtons = True
        Me.gv_Vendor.Size = New System.Drawing.Size(822, 341)
        Me.gv_Vendor.TabIndex = 0
        Me.gv_Vendor.TabStop = False
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.UcAttachment1)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(75.0!, 28.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(844, 408)
        Me.RadPageViewPage3.Text = "Attachment"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(844, 408)
        Me.UcAttachment1.TabIndex = 1
        Me.UcAttachment1.TabStop = False
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(785, 5)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(70, 21)
        Me.btnclose.TabIndex = 4
        Me.btnclose.Text = "Close"
        '
        'btnunpost
        '
        Me.btnunpost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnunpost.Location = New System.Drawing.Point(228, 5)
        Me.btnunpost.Name = "btnunpost"
        Me.btnunpost.Size = New System.Drawing.Size(70, 21)
        Me.btnunpost.TabIndex = 3
        Me.btnunpost.Text = "Amendment"
        Me.btnunpost.Visible = False
        '
        'btnpost
        '
        Me.btnpost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnpost.Location = New System.Drawing.Point(154, 5)
        Me.btnpost.Name = "btnpost"
        Me.btnpost.Size = New System.Drawing.Size(70, 21)
        Me.btnpost.TabIndex = 2
        Me.btnpost.Text = "Post"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(80, 5)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(70, 21)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(6, 5)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(70, 21)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'FrmPurchaseSchedule
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(867, 494)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmPurchaseSchedule"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmPurchaseSchedule"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.txtRev_No, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnMonthly, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPODesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpMonth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDaily, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnWeek, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboPOType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendorName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.btnSamePlan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.gv_Vendor.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_Vendor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage3.ResumeLayout(False)
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnunpost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnpost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnunpost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnpost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents txtDesc As common.Controls.MyTextBox
    Friend WithEvents cboPOType As common.Controls.MyComboBox
    Friend WithEvents RadLabel8 As common.Controls.MyLabel
    Friend WithEvents lblVendorName As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents txtVendorNo As common.UserControls.txtFinder
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents RadPageViewPage3 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv_Vendor As common.UserControls.MyRadGridView
    Friend WithEvents btnSamePlan As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDaily As common.Controls.MyRadioButton
    Friend WithEvents btnWeek As common.Controls.MyRadioButton
    Friend WithEvents dtpMonth As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtPODesc As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtPONo As common.UserControls.txtFinder
    Friend WithEvents btnMonthly As common.Controls.MyRadioButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents txtRev_No As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
End Class

