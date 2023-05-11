Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmEmployeeTransfer
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
        Me.lblChangedDivisionName = New common.Controls.MyLabel()
        Me.fndChangedDivision = New common.UserControls.txtFinder()
        Me.lblChangedDivision = New common.Controls.MyLabel()
        Me.lblCurrentDivisionName = New common.Controls.MyLabel()
        Me.lblCurrentDivisionCode = New common.Controls.MyLabel()
        Me.lblCurrentDivision = New common.Controls.MyLabel()
        Me.lblPreviousSalary = New common.Controls.MyLabel()
        Me.lblEffDate = New common.Controls.MyLabel()
        Me.txtEffDate = New common.Controls.MyDateTimePicker()
        Me.lblSalaryAffected = New common.Controls.MyLabel()
        Me.gbSalary = New System.Windows.Forms.GroupBox()
        Me.lblSalaryCode = New common.Controls.MyLabel()
        Me.btnProceed = New Telerik.WinControls.UI.RadButton()
        Me.cboSalary = New common.Controls.MyComboBox()
        Me.lblDocType = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.lblDate = New common.Controls.MyLabel()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.lblDescription = New common.Controls.MyLabel()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.lblChangedDepartmentName = New common.Controls.MyLabel()
        Me.lblChangedDepartment = New common.Controls.MyLabel()
        Me.fndChangedDepartment = New common.UserControls.txtFinder()
        Me.lblChangedDesignationName = New common.Controls.MyLabel()
        Me.lblChangedDesignation = New common.Controls.MyLabel()
        Me.fndChangedDesignation = New common.UserControls.txtFinder()
        Me.cboDocType = New common.Controls.MyComboBox()
        Me.lblEmployeeName = New common.Controls.MyLabel()
        Me.lblEmployeeCode = New common.Controls.MyLabel()
        Me.fndEmployeeCode = New common.UserControls.txtFinder()
        Me.lblChangedLocationName = New common.Controls.MyLabel()
        Me.lblChangedLocation = New common.Controls.MyLabel()
        Me.fndChangedLocation = New common.UserControls.txtFinder()
        Me.lblLocationName = New common.Controls.MyLabel()
        Me.lblLocationCode = New common.Controls.MyLabel()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.lblDesignationName = New common.Controls.MyLabel()
        Me.lblDesignationCode = New common.Controls.MyLabel()
        Me.lblDesignation = New common.Controls.MyLabel()
        Me.lblDepartmentName = New common.Controls.MyLabel()
        Me.lblDepartmentCode = New common.Controls.MyLabel()
        Me.lblDepartment = New common.Controls.MyLabel()
        Me.lblDocCode = New common.Controls.MyLabel()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.lblChangedDivisionName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblChangedDivision, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCurrentDivisionName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCurrentDivisionCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCurrentDivision, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPreviousSalary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEffDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEffDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSalaryAffected, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbSalary.SuspendLayout()
        CType(Me.lblSalaryCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnProceed, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboSalary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblChangedDepartmentName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblChangedDepartment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblChangedDesignationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblChangedDesignation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboDocType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmployeeName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmployeeCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblChangedLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblChangedLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDesignationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDesignationCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDesignation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDepartmentName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDepartmentCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDepartment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblChangedDivisionName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndChangedDivision)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblChangedDivision)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCurrentDivisionName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCurrentDivisionCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCurrentDivision)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblPreviousSalary)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblEffDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtEffDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblSalaryAffected)
        Me.SplitContainer1.Panel1.Controls.Add(Me.gbSalary)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboSalary)
        Me.SplitContainer1.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblChangedDepartmentName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblChangedDepartment)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndChangedDepartment)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblChangedDesignationName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblChangedDesignation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndChangedDesignation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboDocType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDocType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblEmployeeName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblEmployeeCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndEmployeeCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblChangedLocationName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblChangedLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndChangedLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLocationName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLocationCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDesignationName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDesignationCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDesignation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDepartmentName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDepartmentCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDepartment)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDocCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnnew)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(1069, 435)
        Me.SplitContainer1.SplitterDistance = 402
        Me.SplitContainer1.TabIndex = 0
        '
        'lblChangedDivisionName
        '
        Me.lblChangedDivisionName.AutoSize = False
        Me.lblChangedDivisionName.BorderVisible = True
        Me.lblChangedDivisionName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChangedDivisionName.Location = New System.Drawing.Point(315, 274)
        Me.lblChangedDivisionName.Name = "lblChangedDivisionName"
        Me.lblChangedDivisionName.Size = New System.Drawing.Size(180, 18)
        Me.lblChangedDivisionName.TabIndex = 430
        Me.lblChangedDivisionName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblChangedDivisionName.TextWrap = False
        '
        'fndChangedDivision
        '
        Me.fndChangedDivision.Location = New System.Drawing.Point(128, 274)
        Me.fndChangedDivision.MendatroryField = True
        Me.fndChangedDivision.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndChangedDivision.MyLinkLable1 = Nothing
        Me.fndChangedDivision.MyLinkLable2 = Nothing
        Me.fndChangedDivision.MyReadOnly = False
        Me.fndChangedDivision.MyShowMasterFormButton = False
        Me.fndChangedDivision.Name = "fndChangedDivision"
        Me.fndChangedDivision.Size = New System.Drawing.Size(181, 19)
        Me.fndChangedDivision.TabIndex = 429
        Me.fndChangedDivision.Value = ""
        '
        'lblChangedDivision
        '
        Me.lblChangedDivision.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChangedDivision.Location = New System.Drawing.Point(14, 277)
        Me.lblChangedDivision.Name = "lblChangedDivision"
        Me.lblChangedDivision.Size = New System.Drawing.Size(92, 16)
        Me.lblChangedDivision.TabIndex = 428
        Me.lblChangedDivision.Text = "Changed Divsion"
        '
        'lblCurrentDivisionName
        '
        Me.lblCurrentDivisionName.AutoSize = False
        Me.lblCurrentDivisionName.BorderVisible = True
        Me.lblCurrentDivisionName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrentDivisionName.Location = New System.Drawing.Point(316, 187)
        Me.lblCurrentDivisionName.Name = "lblCurrentDivisionName"
        Me.lblCurrentDivisionName.Size = New System.Drawing.Size(180, 18)
        Me.lblCurrentDivisionName.TabIndex = 427
        Me.lblCurrentDivisionName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCurrentDivisionName.TextWrap = False
        '
        'lblCurrentDivisionCode
        '
        Me.lblCurrentDivisionCode.AutoSize = False
        Me.lblCurrentDivisionCode.BorderVisible = True
        Me.lblCurrentDivisionCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrentDivisionCode.Location = New System.Drawing.Point(130, 187)
        Me.lblCurrentDivisionCode.Name = "lblCurrentDivisionCode"
        Me.lblCurrentDivisionCode.Size = New System.Drawing.Size(180, 18)
        Me.lblCurrentDivisionCode.TabIndex = 426
        Me.lblCurrentDivisionCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCurrentDivisionCode.TextWrap = False
        '
        'lblCurrentDivision
        '
        Me.lblCurrentDivision.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblCurrentDivision.Location = New System.Drawing.Point(14, 189)
        Me.lblCurrentDivision.Name = "lblCurrentDivision"
        Me.lblCurrentDivision.Size = New System.Drawing.Size(87, 16)
        Me.lblCurrentDivision.TabIndex = 425
        Me.lblCurrentDivision.Text = "Current Division"
        '
        'lblPreviousSalary
        '
        Me.lblPreviousSalary.AutoSize = False
        Me.lblPreviousSalary.BorderVisible = True
        Me.lblPreviousSalary.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPreviousSalary.Location = New System.Drawing.Point(317, 294)
        Me.lblPreviousSalary.Name = "lblPreviousSalary"
        Me.lblPreviousSalary.Size = New System.Drawing.Size(180, 18)
        Me.lblPreviousSalary.TabIndex = 424
        Me.lblPreviousSalary.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblPreviousSalary.TextWrap = False
        '
        'lblEffDate
        '
        Me.lblEffDate.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblEffDate.Location = New System.Drawing.Point(427, 36)
        Me.lblEffDate.Name = "lblEffDate"
        Me.lblEffDate.Size = New System.Drawing.Size(77, 16)
        Me.lblEffDate.TabIndex = 423
        Me.lblEffDate.Text = "Effective Date"
        '
        'txtEffDate
        '
        Me.txtEffDate.CustomFormat = "dd/MM/yyyy"
        Me.txtEffDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtEffDate.Location = New System.Drawing.Point(507, 35)
        Me.txtEffDate.MendatroryField = False
        Me.txtEffDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtEffDate.MyLinkLable1 = Me.lblEffDate
        Me.txtEffDate.MyLinkLable2 = Nothing
        Me.txtEffDate.Name = "txtEffDate"
        Me.txtEffDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtEffDate.Size = New System.Drawing.Size(142, 20)
        Me.txtEffDate.TabIndex = 422
        Me.txtEffDate.TabStop = False
        Me.txtEffDate.Text = "16/11/2011"
        Me.txtEffDate.Value = New Date(2011, 11, 16, 11, 21, 56, 285)
        '
        'lblSalaryAffected
        '
        Me.lblSalaryAffected.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSalaryAffected.Location = New System.Drawing.Point(16, 293)
        Me.lblSalaryAffected.Name = "lblSalaryAffected"
        Me.lblSalaryAffected.Size = New System.Drawing.Size(83, 16)
        Me.lblSalaryAffected.TabIndex = 158
        Me.lblSalaryAffected.Text = "Salary Affected"
        '
        'gbSalary
        '
        Me.gbSalary.Controls.Add(Me.lblSalaryCode)
        Me.gbSalary.Controls.Add(Me.btnProceed)
        Me.gbSalary.Location = New System.Drawing.Point(127, 313)
        Me.gbSalary.Name = "gbSalary"
        Me.gbSalary.Size = New System.Drawing.Size(370, 40)
        Me.gbSalary.TabIndex = 421
        Me.gbSalary.TabStop = False
        '
        'lblSalaryCode
        '
        Me.lblSalaryCode.AutoSize = False
        Me.lblSalaryCode.BorderVisible = True
        Me.lblSalaryCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSalaryCode.Location = New System.Drawing.Point(92, 16)
        Me.lblSalaryCode.Name = "lblSalaryCode"
        Me.lblSalaryCode.Size = New System.Drawing.Size(272, 18)
        Me.lblSalaryCode.TabIndex = 160
        Me.lblSalaryCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSalaryCode.TextWrap = False
        '
        'btnProceed
        '
        Me.btnProceed.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnProceed.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnProceed.Location = New System.Drawing.Point(6, 16)
        Me.btnProceed.Name = "btnProceed"
        Me.btnProceed.Size = New System.Drawing.Size(80, 18)
        Me.btnProceed.TabIndex = 159
        Me.btnProceed.Text = "Proceed"
        '
        'cboSalary
        '
        Me.cboSalary.AutoCompleteDisplayMember = Nothing
        Me.cboSalary.AutoCompleteValueMember = Nothing
        Me.cboSalary.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboSalary.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        RadListDataItem1.Text = "Yes"
        RadListDataItem2.Text = "No"
        Me.cboSalary.Items.Add(RadListDataItem1)
        Me.cboSalary.Items.Add(RadListDataItem2)
        Me.cboSalary.Location = New System.Drawing.Point(128, 294)
        Me.cboSalary.MendatroryField = False
        Me.cboSalary.MyLinkLable1 = Me.lblDocType
        Me.cboSalary.MyLinkLable2 = Nothing
        Me.cboSalary.Name = "cboSalary"
        Me.cboSalary.Size = New System.Drawing.Size(184, 18)
        Me.cboSalary.TabIndex = 160
        '
        'lblDocType
        '
        Me.lblDocType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocType.Location = New System.Drawing.Point(13, 37)
        Me.lblDocType.Name = "lblDocType"
        Me.lblDocType.Size = New System.Drawing.Size(86, 16)
        Me.lblDocType.TabIndex = 150
        Me.lblDocType.Text = "Document Type"
        '
        'UsLock1
        '
        Me.UsLock1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(933, 14)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 420
        '
        'lblDate
        '
        Me.lblDate.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblDate.Location = New System.Drawing.Point(427, 17)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(30, 16)
        Me.lblDate.TabIndex = 419
        Me.lblDate.Text = "Date"
        '
        'txtDate
        '
        Me.txtDate.CustomFormat = "dd/MM/yyyy"
        Me.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDate.Location = New System.Drawing.Point(507, 15)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.lblDate
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.Size = New System.Drawing.Size(142, 20)
        Me.txtDate.TabIndex = 418
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "16/11/2011"
        Me.txtDate.Value = New Date(2011, 11, 16, 11, 21, 56, 285)
        '
        'lblDescription
        '
        Me.lblDescription.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblDescription.Location = New System.Drawing.Point(16, 59)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(63, 16)
        Me.lblDescription.TabIndex = 412
        Me.lblDescription.Text = "Description"
        '
        'txtDescription
        '
        Me.txtDescription.AutoSize = False
        Me.txtDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.Location = New System.Drawing.Point(131, 59)
        Me.txtDescription.MaxLength = 200
        Me.txtDescription.MendatroryField = True
        Me.txtDescription.Multiline = True
        Me.txtDescription.MyLinkLable1 = Me.lblDescription
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(282, 33)
        Me.txtDescription.TabIndex = 413
        '
        'lblChangedDepartmentName
        '
        Me.lblChangedDepartmentName.AutoSize = False
        Me.lblChangedDepartmentName.BorderVisible = True
        Me.lblChangedDepartmentName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChangedDepartmentName.Location = New System.Drawing.Point(316, 206)
        Me.lblChangedDepartmentName.Name = "lblChangedDepartmentName"
        Me.lblChangedDepartmentName.Size = New System.Drawing.Size(180, 18)
        Me.lblChangedDepartmentName.TabIndex = 156
        Me.lblChangedDepartmentName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblChangedDepartmentName.TextWrap = False
        '
        'lblChangedDepartment
        '
        Me.lblChangedDepartment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChangedDepartment.Location = New System.Drawing.Point(14, 211)
        Me.lblChangedDepartment.Name = "lblChangedDepartment"
        Me.lblChangedDepartment.Size = New System.Drawing.Size(114, 16)
        Me.lblChangedDepartment.TabIndex = 154
        Me.lblChangedDepartment.Text = "Changed Department"
        '
        'fndChangedDepartment
        '
        Me.fndChangedDepartment.Location = New System.Drawing.Point(130, 207)
        Me.fndChangedDepartment.MendatroryField = True
        Me.fndChangedDepartment.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndChangedDepartment.MyLinkLable1 = Nothing
        Me.fndChangedDepartment.MyLinkLable2 = Nothing
        Me.fndChangedDepartment.MyReadOnly = False
        Me.fndChangedDepartment.MyShowMasterFormButton = False
        Me.fndChangedDepartment.Name = "fndChangedDepartment"
        Me.fndChangedDepartment.Size = New System.Drawing.Size(180, 18)
        Me.fndChangedDepartment.TabIndex = 155
        Me.fndChangedDepartment.Value = ""
        '
        'lblChangedDesignationName
        '
        Me.lblChangedDesignationName.AutoSize = False
        Me.lblChangedDesignationName.BorderVisible = True
        Me.lblChangedDesignationName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChangedDesignationName.Location = New System.Drawing.Point(315, 229)
        Me.lblChangedDesignationName.Name = "lblChangedDesignationName"
        Me.lblChangedDesignationName.Size = New System.Drawing.Size(180, 18)
        Me.lblChangedDesignationName.TabIndex = 153
        Me.lblChangedDesignationName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblChangedDesignationName.TextWrap = False
        '
        'lblChangedDesignation
        '
        Me.lblChangedDesignation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChangedDesignation.Location = New System.Drawing.Point(14, 232)
        Me.lblChangedDesignation.Name = "lblChangedDesignation"
        Me.lblChangedDesignation.Size = New System.Drawing.Size(115, 16)
        Me.lblChangedDesignation.TabIndex = 151
        Me.lblChangedDesignation.Text = "Changed Designation"
        '
        'fndChangedDesignation
        '
        Me.fndChangedDesignation.Location = New System.Drawing.Point(130, 230)
        Me.fndChangedDesignation.MendatroryField = True
        Me.fndChangedDesignation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndChangedDesignation.MyLinkLable1 = Nothing
        Me.fndChangedDesignation.MyLinkLable2 = Nothing
        Me.fndChangedDesignation.MyReadOnly = False
        Me.fndChangedDesignation.MyShowMasterFormButton = False
        Me.fndChangedDesignation.Name = "fndChangedDesignation"
        Me.fndChangedDesignation.Size = New System.Drawing.Size(180, 18)
        Me.fndChangedDesignation.TabIndex = 152
        Me.fndChangedDesignation.Value = ""
        '
        'cboDocType
        '
        Me.cboDocType.AutoCompleteDisplayMember = Nothing
        Me.cboDocType.AutoCompleteValueMember = Nothing
        Me.cboDocType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboDocType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        RadListDataItem3.Text = "Transfer Letter(For Location)"
        RadListDataItem4.Text = "Promotion Letter"
        RadListDataItem5.Text = "Transfer Letter(For Department)"
        Me.cboDocType.Items.Add(RadListDataItem3)
        Me.cboDocType.Items.Add(RadListDataItem4)
        Me.cboDocType.Items.Add(RadListDataItem5)
        Me.cboDocType.Location = New System.Drawing.Point(131, 36)
        Me.cboDocType.MendatroryField = False
        Me.cboDocType.MyLinkLable1 = Me.lblDocType
        Me.cboDocType.MyLinkLable2 = Nothing
        Me.cboDocType.Name = "cboDocType"
        Me.cboDocType.Size = New System.Drawing.Size(282, 18)
        Me.cboDocType.TabIndex = 149
        '
        'lblEmployeeName
        '
        Me.lblEmployeeName.AutoSize = False
        Me.lblEmployeeName.BorderVisible = True
        Me.lblEmployeeName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmployeeName.Location = New System.Drawing.Point(315, 98)
        Me.lblEmployeeName.Name = "lblEmployeeName"
        Me.lblEmployeeName.Size = New System.Drawing.Size(180, 18)
        Me.lblEmployeeName.TabIndex = 148
        Me.lblEmployeeName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblEmployeeName.TextWrap = False
        '
        'lblEmployeeCode
        '
        Me.lblEmployeeCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmployeeCode.Location = New System.Drawing.Point(14, 100)
        Me.lblEmployeeCode.Name = "lblEmployeeCode"
        Me.lblEmployeeCode.Size = New System.Drawing.Size(57, 16)
        Me.lblEmployeeCode.TabIndex = 146
        Me.lblEmployeeCode.Text = "Employee"
        '
        'fndEmployeeCode
        '
        Me.fndEmployeeCode.Location = New System.Drawing.Point(130, 99)
        Me.fndEmployeeCode.MendatroryField = True
        Me.fndEmployeeCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndEmployeeCode.MyLinkLable1 = Nothing
        Me.fndEmployeeCode.MyLinkLable2 = Nothing
        Me.fndEmployeeCode.MyReadOnly = False
        Me.fndEmployeeCode.MyShowMasterFormButton = False
        Me.fndEmployeeCode.Name = "fndEmployeeCode"
        Me.fndEmployeeCode.Size = New System.Drawing.Size(180, 18)
        Me.fndEmployeeCode.TabIndex = 147
        Me.fndEmployeeCode.Value = ""
        '
        'lblChangedLocationName
        '
        Me.lblChangedLocationName.AutoSize = False
        Me.lblChangedLocationName.BorderVisible = True
        Me.lblChangedLocationName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChangedLocationName.Location = New System.Drawing.Point(316, 252)
        Me.lblChangedLocationName.Name = "lblChangedLocationName"
        Me.lblChangedLocationName.Size = New System.Drawing.Size(180, 18)
        Me.lblChangedLocationName.TabIndex = 145
        Me.lblChangedLocationName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblChangedLocationName.TextWrap = False
        '
        'lblChangedLocation
        '
        Me.lblChangedLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChangedLocation.Location = New System.Drawing.Point(13, 255)
        Me.lblChangedLocation.Name = "lblChangedLocation"
        Me.lblChangedLocation.Size = New System.Drawing.Size(98, 16)
        Me.lblChangedLocation.TabIndex = 143
        Me.lblChangedLocation.Text = "Changed Location"
        '
        'fndChangedLocation
        '
        Me.fndChangedLocation.Location = New System.Drawing.Point(129, 252)
        Me.fndChangedLocation.MendatroryField = True
        Me.fndChangedLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndChangedLocation.MyLinkLable1 = Nothing
        Me.fndChangedLocation.MyLinkLable2 = Nothing
        Me.fndChangedLocation.MyReadOnly = False
        Me.fndChangedLocation.MyShowMasterFormButton = False
        Me.fndChangedLocation.Name = "fndChangedLocation"
        Me.fndChangedLocation.Size = New System.Drawing.Size(180, 18)
        Me.fndChangedLocation.TabIndex = 144
        Me.fndChangedLocation.Value = ""
        '
        'lblLocationName
        '
        Me.lblLocationName.AutoSize = False
        Me.lblLocationName.BorderVisible = True
        Me.lblLocationName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocationName.Location = New System.Drawing.Point(315, 163)
        Me.lblLocationName.Name = "lblLocationName"
        Me.lblLocationName.Size = New System.Drawing.Size(180, 18)
        Me.lblLocationName.TabIndex = 142
        Me.lblLocationName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblLocationName.TextWrap = False
        '
        'lblLocationCode
        '
        Me.lblLocationCode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblLocationCode.Location = New System.Drawing.Point(12, 167)
        Me.lblLocationCode.Name = "lblLocationCode"
        Me.lblLocationCode.Size = New System.Drawing.Size(90, 16)
        Me.lblLocationCode.TabIndex = 140
        Me.lblLocationCode.Text = "Current Location"
        '
        'lblLocation
        '
        Me.lblLocation.AutoSize = False
        Me.lblLocation.BorderVisible = True
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(129, 166)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(180, 18)
        Me.lblLocation.TabIndex = 141
        Me.lblLocation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblLocation.TextWrap = False
        '
        'lblDesignationName
        '
        Me.lblDesignationName.AutoSize = False
        Me.lblDesignationName.BorderVisible = True
        Me.lblDesignationName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDesignationName.Location = New System.Drawing.Point(315, 141)
        Me.lblDesignationName.Name = "lblDesignationName"
        Me.lblDesignationName.Size = New System.Drawing.Size(180, 18)
        Me.lblDesignationName.TabIndex = 139
        Me.lblDesignationName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblDesignationName.TextWrap = False
        '
        'lblDesignationCode
        '
        Me.lblDesignationCode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblDesignationCode.Location = New System.Drawing.Point(12, 146)
        Me.lblDesignationCode.Name = "lblDesignationCode"
        Me.lblDesignationCode.Size = New System.Drawing.Size(107, 16)
        Me.lblDesignationCode.TabIndex = 137
        Me.lblDesignationCode.Text = "Current Designation"
        '
        'lblDesignation
        '
        Me.lblDesignation.AutoSize = False
        Me.lblDesignation.BorderVisible = True
        Me.lblDesignation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDesignation.Location = New System.Drawing.Point(130, 143)
        Me.lblDesignation.Name = "lblDesignation"
        Me.lblDesignation.Size = New System.Drawing.Size(180, 18)
        Me.lblDesignation.TabIndex = 138
        Me.lblDesignation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblDesignation.TextWrap = False
        '
        'lblDepartmentName
        '
        Me.lblDepartmentName.AutoSize = False
        Me.lblDepartmentName.BorderVisible = True
        Me.lblDepartmentName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDepartmentName.Location = New System.Drawing.Point(315, 120)
        Me.lblDepartmentName.Name = "lblDepartmentName"
        Me.lblDepartmentName.Size = New System.Drawing.Size(180, 18)
        Me.lblDepartmentName.TabIndex = 136
        Me.lblDepartmentName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblDepartmentName.TextWrap = False
        '
        'lblDepartmentCode
        '
        Me.lblDepartmentCode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblDepartmentCode.Location = New System.Drawing.Point(12, 124)
        Me.lblDepartmentCode.Name = "lblDepartmentCode"
        Me.lblDepartmentCode.Size = New System.Drawing.Size(109, 16)
        Me.lblDepartmentCode.TabIndex = 134
        Me.lblDepartmentCode.Text = "Current Department "
        '
        'lblDepartment
        '
        Me.lblDepartment.AutoSize = False
        Me.lblDepartment.BorderVisible = True
        Me.lblDepartment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDepartment.Location = New System.Drawing.Point(130, 120)
        Me.lblDepartment.Name = "lblDepartment"
        Me.lblDepartment.Size = New System.Drawing.Size(180, 18)
        Me.lblDepartment.TabIndex = 135
        Me.lblDepartment.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblDepartment.TextWrap = False
        '
        'lblDocCode
        '
        Me.lblDocCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblDocCode.Location = New System.Drawing.Point(12, 15)
        Me.lblDocCode.Name = "lblDocCode"
        Me.lblDocCode.Size = New System.Drawing.Size(93, 16)
        Me.lblDocCode.TabIndex = 56
        Me.lblDocCode.Text = "Document Code"
        '
        'txtCode
        '
        Me.txtCode.Location = New System.Drawing.Point(131, 13)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblDocCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 12
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(266, 21)
        Me.txtCode.TabIndex = 54
        Me.txtCode.Value = ""
        '
        'btnnew
        '
        Me.btnnew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnnew.Image = Global.XpertERPHRandPayroll.My.Resources.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(397, 14)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(16, 20)
        Me.btnnew.TabIndex = 55
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(155, 5)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(66, 18)
        Me.btnPrint.TabIndex = 8
        Me.btnPrint.Text = "Print"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(84, 5)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(66, 18)
        Me.btnPost.TabIndex = 7
        Me.btnPost.Text = "Post"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(14, 5)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 4
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(989, 5)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 6
        Me.btnclose.Text = "Close"
        '
        'FrmEmployeeTransfer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1069, 435)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmEmployeeTransfer"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmEmployeeTransfer"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.lblChangedDivisionName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblChangedDivision, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCurrentDivisionName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCurrentDivisionCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCurrentDivision, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPreviousSalary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEffDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEffDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSalaryAffected, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbSalary.ResumeLayout(False)
        CType(Me.lblSalaryCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnProceed, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboSalary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblChangedDepartmentName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblChangedDepartment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblChangedDesignationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblChangedDesignation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboDocType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmployeeName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmployeeCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblChangedLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblChangedLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDesignationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDesignationCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDesignation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDepartmentName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDepartmentCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDepartment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblDocCode As common.Controls.MyLabel
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblDepartmentName As common.Controls.MyLabel
    Friend WithEvents lblDepartmentCode As common.Controls.MyLabel
    Friend WithEvents lblDepartment As common.Controls.MyLabel
    Friend WithEvents lblLocationName As common.Controls.MyLabel
    Friend WithEvents lblLocationCode As common.Controls.MyLabel
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents lblDesignationName As common.Controls.MyLabel
    Friend WithEvents lblDesignationCode As common.Controls.MyLabel
    Friend WithEvents lblDesignation As common.Controls.MyLabel
    Friend WithEvents lblChangedLocationName As common.Controls.MyLabel
    Friend WithEvents lblChangedLocation As common.Controls.MyLabel
    Friend WithEvents fndChangedLocation As common.UserControls.txtFinder
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblEmployeeName As common.Controls.MyLabel
    Friend WithEvents lblEmployeeCode As common.Controls.MyLabel
    Friend WithEvents fndEmployeeCode As common.UserControls.txtFinder
    Friend WithEvents cboDocType As common.Controls.MyComboBox
    Friend WithEvents lblDocType As common.Controls.MyLabel
    Friend WithEvents lblChangedDesignationName As common.Controls.MyLabel
    Friend WithEvents lblChangedDesignation As common.Controls.MyLabel
    Friend WithEvents fndChangedDesignation As common.UserControls.txtFinder
    Friend WithEvents lblChangedDepartmentName As common.Controls.MyLabel
    Friend WithEvents lblChangedDepartment As common.Controls.MyLabel
    Friend WithEvents fndChangedDepartment As common.UserControls.txtFinder
    Friend WithEvents CboSalaryAffected As common.Controls.MyComboBox
    Friend WithEvents lblSalaryAffected As common.Controls.MyLabel
    Friend WithEvents btnProceed As Telerik.WinControls.UI.RadButton
    Friend WithEvents cboSalary As common.Controls.MyComboBox
    Friend WithEvents lblDescription As common.Controls.MyLabel
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents lblDate As common.Controls.MyLabel
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents gbSalary As System.Windows.Forms.GroupBox
    Friend WithEvents lblSalaryCode As common.Controls.MyLabel
    Friend WithEvents lblEffDate As common.Controls.MyLabel
    Friend WithEvents txtEffDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblPreviousSalary As common.Controls.MyLabel
    Friend WithEvents lblCurrentDivisionName As common.Controls.MyLabel
    Friend WithEvents lblCurrentDivisionCode As common.Controls.MyLabel
    Friend WithEvents lblCurrentDivision As common.Controls.MyLabel
    Friend WithEvents fndChangedDivision As common.UserControls.txtFinder
    Friend WithEvents lblChangedDivision As common.Controls.MyLabel
    Friend WithEvents lblChangedDivisionName As common.Controls.MyLabel
End Class

