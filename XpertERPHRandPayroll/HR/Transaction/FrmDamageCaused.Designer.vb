Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDamageCaused
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
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lblPayPeriod = New common.Controls.MyLabel()
        Me.txtPayPeriod = New common.UserControls.txtFinder()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.lbldateOfamtRealised = New common.Controls.MyLabel()
        Me.txtAmtDate = New common.Controls.MyDateTimePicker()
        Me.txtNoOfInstallment = New common.Controls.MyTextBox()
        Me.lblDescription = New common.Controls.MyLabel()
        Me.txtDedImposed = New common.Controls.MyTextBox()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.lblDamageName = New common.Controls.MyLabel()
        Me.lblDamage = New common.Controls.MyLabel()
        Me.txtDamageCode = New common.UserControls.txtFinder()
        Me.lblDepartmentName = New common.Controls.MyLabel()
        Me.lblDepartment = New common.Controls.MyLabel()
        Me.lblDepartmentCode = New common.Controls.MyLabel()
        Me.lblDate = New common.Controls.MyLabel()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.lblEmployeeName = New common.Controls.MyLabel()
        Me.lblEmployeeCode = New common.Controls.MyLabel()
        Me.fndEmployeeCode = New common.UserControls.txtFinder()
        Me.lblSex = New common.Controls.MyLabel()
        Me.lblSexN = New common.Controls.MyLabel()
        Me.lblFather = New common.Controls.MyLabel()
        Me.lblFatherName = New common.Controls.MyLabel()
        Me.lblDocCode = New common.Controls.MyLabel()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        'CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPayPeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbldateOfamtRealised, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAmtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoOfInstallment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDedImposed, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDamageName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDamage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDepartmentName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDepartment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDepartmentCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmployeeName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmployeeCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSex, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSexN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFather, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFatherName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblPayPeriod)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtPayPeriod)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lbldateOfamtRealised)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtAmtDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtNoOfInstallment)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDedImposed)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnnew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDamageName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDamage)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDamageCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDepartmentName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDepartment)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDepartmentCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblEmployeeName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblEmployeeCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndEmployeeCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblSex)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblSexN)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblFather)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblFatherName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDocCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(1037, 414)
        Me.SplitContainer1.SplitterDistance = 377
        Me.SplitContainer1.TabIndex = 0
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(6, 167)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(45, 16)
        Me.MyLabel2.TabIndex = 451
        Me.MyLabel2.Text = "Amount"
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(8, 189)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(94, 16)
        Me.MyLabel1.TabIndex = 450
        Me.MyLabel1.Text = "No. Of Instalment"
        '
        'lblPayPeriod
        '
        Me.lblPayPeriod.AutoSize = False
        Me.lblPayPeriod.BorderVisible = True
        Me.lblPayPeriod.Location = New System.Drawing.Point(307, 211)
        Me.lblPayPeriod.Name = "lblPayPeriod"
        Me.lblPayPeriod.Size = New System.Drawing.Size(191, 18)
        Me.lblPayPeriod.TabIndex = 448
        Me.lblPayPeriod.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblPayPeriod.Visible = False
        '
        'txtPayPeriod
        '
        Me.txtPayPeriod.Location = New System.Drawing.Point(122, 211)
        Me.txtPayPeriod.MendatroryField = True
        Me.txtPayPeriod.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPayPeriod.MyLinkLable1 = Me.MyLabel5
        Me.txtPayPeriod.MyLinkLable2 = Me.lblPayPeriod
        Me.txtPayPeriod.MyReadOnly = False
        Me.txtPayPeriod.MyShowMasterFormButton = False
        Me.txtPayPeriod.Name = "txtPayPeriod"
        Me.txtPayPeriod.Size = New System.Drawing.Size(181, 18)
        Me.txtPayPeriod.TabIndex = 447
        Me.txtPayPeriod.Value = ""
        Me.txtPayPeriod.Visible = False
        '
        'MyLabel5
        '
        Me.MyLabel5.Location = New System.Drawing.Point(7, 211)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(88, 18)
        Me.MyLabel5.TabIndex = 449
        Me.MyLabel5.Text = "Pay Period Code"
        Me.MyLabel5.Visible = False
        '
        'UsLock1
        '
        Me.UsLock1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(900, 8)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 446
        '
        'lbldateOfamtRealised
        '
        Me.lbldateOfamtRealised.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lbldateOfamtRealised.Location = New System.Drawing.Point(303, 164)
        Me.lbldateOfamtRealised.Name = "lbldateOfamtRealised"
        Me.lbldateOfamtRealised.Size = New System.Drawing.Size(116, 16)
        Me.lbldateOfamtRealised.TabIndex = 445
        Me.lbldateOfamtRealised.Text = "Date Of Amt Realised"
        '
        'txtAmtDate
        '
        Me.txtAmtDate.CustomFormat = "dd/MM/yyyy"
        Me.txtAmtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtAmtDate.Location = New System.Drawing.Point(420, 161)
        Me.txtAmtDate.MendatroryField = False
        Me.txtAmtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtAmtDate.MyLinkLable1 = Me.lbldateOfamtRealised
        Me.txtAmtDate.MyLinkLable2 = Nothing
        Me.txtAmtDate.Name = "txtAmtDate"
        Me.txtAmtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtAmtDate.Size = New System.Drawing.Size(78, 20)
        Me.txtAmtDate.TabIndex = 444
        Me.txtAmtDate.TabStop = False
        Me.txtAmtDate.Text = "16/11/2011"
        Me.txtAmtDate.Value = New Date(2011, 11, 16, 11, 21, 56, 285)
        '
        'txtNoOfInstallment
        '
        Me.txtNoOfInstallment.AutoSize = False
        Me.txtNoOfInstallment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNoOfInstallment.Location = New System.Drawing.Point(122, 186)
        Me.txtNoOfInstallment.MaxLength = 200
        Me.txtNoOfInstallment.MendatroryField = True
        Me.txtNoOfInstallment.Multiline = True
        Me.txtNoOfInstallment.MyLinkLable1 = Me.lblDescription
        Me.txtNoOfInstallment.MyLinkLable2 = Nothing
        Me.txtNoOfInstallment.Name = "txtNoOfInstallment"
        Me.txtNoOfInstallment.Size = New System.Drawing.Size(180, 19)
        Me.txtNoOfInstallment.TabIndex = 443
        '
        'lblDescription
        '
        Me.lblDescription.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblDescription.Location = New System.Drawing.Point(8, 34)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(63, 16)
        Me.lblDescription.TabIndex = 429
        Me.lblDescription.Text = "Description"
        '
        'txtDedImposed
        '
        Me.txtDedImposed.AutoSize = False
        Me.txtDedImposed.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDedImposed.Location = New System.Drawing.Point(122, 162)
        Me.txtDedImposed.MaxLength = 200
        Me.txtDedImposed.MendatroryField = True
        Me.txtDedImposed.Multiline = True
        Me.txtDedImposed.MyLinkLable1 = Me.lblDescription
        Me.txtDedImposed.MyLinkLable2 = Nothing
        Me.txtDedImposed.Name = "txtDedImposed"
        Me.txtDedImposed.Size = New System.Drawing.Size(180, 19)
        Me.txtDedImposed.TabIndex = 441
        '
        'btnnew
        '
        Me.btnnew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnnew.Image = Global.XpertERPHRandPayroll.My.Resources.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(482, 7)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(16, 20)
        Me.btnnew.TabIndex = 440
        '
        'lblDamageName
        '
        Me.lblDamageName.AutoSize = False
        Me.lblDamageName.BorderVisible = True
        Me.lblDamageName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDamageName.Location = New System.Drawing.Point(307, 140)
        Me.lblDamageName.Name = "lblDamageName"
        Me.lblDamageName.Size = New System.Drawing.Size(191, 18)
        Me.lblDamageName.TabIndex = 438
        Me.lblDamageName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblDamageName.TextWrap = False
        '
        'lblDamage
        '
        Me.lblDamage.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDamage.Location = New System.Drawing.Point(6, 145)
        Me.lblDamage.Name = "lblDamage"
        Me.lblDamage.Size = New System.Drawing.Size(91, 16)
        Me.lblDamage.TabIndex = 436
        Me.lblDamage.Text = "Damage Caused"
        '
        'txtDamageCode
        '
        Me.txtDamageCode.Location = New System.Drawing.Point(122, 141)
        Me.txtDamageCode.MendatroryField = True
        Me.txtDamageCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDamageCode.MyLinkLable1 = Nothing
        Me.txtDamageCode.MyLinkLable2 = Nothing
        Me.txtDamageCode.MyReadOnly = False
        Me.txtDamageCode.MyShowMasterFormButton = False
        Me.txtDamageCode.Name = "txtDamageCode"
        Me.txtDamageCode.Size = New System.Drawing.Size(180, 18)
        Me.txtDamageCode.TabIndex = 437
        Me.txtDamageCode.Value = ""
        '
        'lblDepartmentName
        '
        Me.lblDepartmentName.AutoSize = False
        Me.lblDepartmentName.BorderVisible = True
        Me.lblDepartmentName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDepartmentName.Location = New System.Drawing.Point(308, 118)
        Me.lblDepartmentName.Name = "lblDepartmentName"
        Me.lblDepartmentName.Size = New System.Drawing.Size(190, 18)
        Me.lblDepartmentName.TabIndex = 435
        Me.lblDepartmentName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblDepartmentName.TextWrap = False
        '
        'lblDepartment
        '
        Me.lblDepartment.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblDepartment.Location = New System.Drawing.Point(7, 123)
        Me.lblDepartment.Name = "lblDepartment"
        Me.lblDepartment.Size = New System.Drawing.Size(65, 16)
        Me.lblDepartment.TabIndex = 433
        Me.lblDepartment.Text = "Department"
        '
        'lblDepartmentCode
        '
        Me.lblDepartmentCode.AutoSize = False
        Me.lblDepartmentCode.BorderVisible = True
        Me.lblDepartmentCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDepartmentCode.Location = New System.Drawing.Point(122, 118)
        Me.lblDepartmentCode.Name = "lblDepartmentCode"
        Me.lblDepartmentCode.Size = New System.Drawing.Size(179, 18)
        Me.lblDepartmentCode.TabIndex = 434
        Me.lblDepartmentCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblDepartmentCode.TextWrap = False
        '
        'lblDate
        '
        Me.lblDate.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblDate.Location = New System.Drawing.Point(518, 11)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(30, 16)
        Me.lblDate.TabIndex = 432
        Me.lblDate.Text = "Date"
        '
        'txtDate
        '
        Me.txtDate.CustomFormat = "dd/MM/yyyy"
        Me.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDate.Location = New System.Drawing.Point(554, 8)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.lblDate
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.Size = New System.Drawing.Size(142, 20)
        Me.txtDate.TabIndex = 431
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "16/11/2011"
        Me.txtDate.Value = New Date(2011, 11, 16, 11, 21, 56, 285)
        '
        'txtDescription
        '
        Me.txtDescription.AutoSize = False
        Me.txtDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.Location = New System.Drawing.Point(123, 31)
        Me.txtDescription.MaxLength = 200
        Me.txtDescription.MendatroryField = True
        Me.txtDescription.Multiline = True
        Me.txtDescription.MyLinkLable1 = Me.lblDescription
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(375, 33)
        Me.txtDescription.TabIndex = 430
        '
        'lblEmployeeName
        '
        Me.lblEmployeeName.AutoSize = False
        Me.lblEmployeeName.BorderVisible = True
        Me.lblEmployeeName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmployeeName.Location = New System.Drawing.Point(307, 69)
        Me.lblEmployeeName.Name = "lblEmployeeName"
        Me.lblEmployeeName.Size = New System.Drawing.Size(191, 18)
        Me.lblEmployeeName.TabIndex = 428
        Me.lblEmployeeName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblEmployeeName.TextWrap = False
        '
        'lblEmployeeCode
        '
        Me.lblEmployeeCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmployeeCode.Location = New System.Drawing.Point(9, 74)
        Me.lblEmployeeCode.Name = "lblEmployeeCode"
        Me.lblEmployeeCode.Size = New System.Drawing.Size(57, 16)
        Me.lblEmployeeCode.TabIndex = 426
        Me.lblEmployeeCode.Text = "Employee"
        '
        'fndEmployeeCode
        '
        Me.fndEmployeeCode.Location = New System.Drawing.Point(122, 70)
        Me.fndEmployeeCode.MendatroryField = True
        Me.fndEmployeeCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndEmployeeCode.MyLinkLable1 = Nothing
        Me.fndEmployeeCode.MyLinkLable2 = Nothing
        Me.fndEmployeeCode.MyReadOnly = False
        Me.fndEmployeeCode.MyShowMasterFormButton = False
        Me.fndEmployeeCode.Name = "fndEmployeeCode"
        Me.fndEmployeeCode.Size = New System.Drawing.Size(180, 18)
        Me.fndEmployeeCode.TabIndex = 427
        Me.fndEmployeeCode.Value = ""
        '
        'lblSex
        '
        Me.lblSex.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblSex.Location = New System.Drawing.Point(374, 96)
        Me.lblSex.Name = "lblSex"
        Me.lblSex.Size = New System.Drawing.Size(26, 16)
        Me.lblSex.TabIndex = 424
        Me.lblSex.Text = "Sex"
        '
        'lblSexN
        '
        Me.lblSexN.AutoSize = False
        Me.lblSexN.BorderVisible = True
        Me.lblSexN.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSexN.Location = New System.Drawing.Point(406, 94)
        Me.lblSexN.Name = "lblSexN"
        Me.lblSexN.Size = New System.Drawing.Size(92, 18)
        Me.lblSexN.TabIndex = 425
        Me.lblSexN.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSexN.TextWrap = False
        '
        'lblFather
        '
        Me.lblFather.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblFather.Location = New System.Drawing.Point(7, 101)
        Me.lblFather.Name = "lblFather"
        Me.lblFather.Size = New System.Drawing.Size(72, 16)
        Me.lblFather.TabIndex = 422
        Me.lblFather.Text = "Father Name"
        '
        'lblFatherName
        '
        Me.lblFatherName.AutoSize = False
        Me.lblFatherName.BorderVisible = True
        Me.lblFatherName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFatherName.Location = New System.Drawing.Point(122, 94)
        Me.lblFatherName.Name = "lblFatherName"
        Me.lblFatherName.Size = New System.Drawing.Size(246, 18)
        Me.lblFatherName.TabIndex = 423
        Me.lblFatherName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblFatherName.TextWrap = False
        '
        'lblDocCode
        '
        Me.lblDocCode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblDocCode.Location = New System.Drawing.Point(6, 12)
        Me.lblDocCode.Name = "lblDocCode"
        Me.lblDocCode.Size = New System.Drawing.Size(88, 16)
        Me.lblDocCode.TabIndex = 421
        Me.lblDocCode.Text = "Document Code"
        '
        'txtCode
        '
        Me.txtCode.Location = New System.Drawing.Point(123, 7)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblDocCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 12
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(360, 21)
        Me.txtCode.TabIndex = 420
        Me.txtCode.Value = ""
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(77, 7)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 13
        Me.btndelete.Text = "Delete"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(221, 7)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(66, 18)
        Me.btnPrint.TabIndex = 12
        Me.btnPrint.Text = "Print"
        Me.btnPrint.Visible = False
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(149, 7)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(66, 18)
        Me.btnPost.TabIndex = 11
        Me.btnPost.Text = "Post"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(6, 7)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 9
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(959, 7)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 10
        Me.btnclose.Text = "Close"
        '
        'FrmDamageCaused
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1037, 414)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmDamageCaused"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmDamageCaused"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        'CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPayPeriod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbldateOfamtRealised, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAmtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoOfInstallment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDedImposed, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDamageName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDamage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDepartmentName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDepartment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDepartmentCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmployeeName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmployeeCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSex, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSexN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFather, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFatherName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblDate As common.Controls.MyLabel
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblDescription As common.Controls.MyLabel
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents lblEmployeeName As common.Controls.MyLabel
    Friend WithEvents lblEmployeeCode As common.Controls.MyLabel
    Friend WithEvents fndEmployeeCode As common.UserControls.txtFinder
    Friend WithEvents lblSex As common.Controls.MyLabel
    Friend WithEvents lblSexN As common.Controls.MyLabel
    Friend WithEvents lblFather As common.Controls.MyLabel
    Friend WithEvents lblFatherName As common.Controls.MyLabel
    Friend WithEvents lblDocCode As common.Controls.MyLabel
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents lblDamageName As common.Controls.MyLabel
    Friend WithEvents lblDamage As common.Controls.MyLabel
    Friend WithEvents txtDamageCode As common.UserControls.txtFinder
    Friend WithEvents lblDepartmentName As common.Controls.MyLabel
    Friend WithEvents lblDepartment As common.Controls.MyLabel
    Friend WithEvents lblDepartmentCode As common.Controls.MyLabel
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents lbldateOfamtRealised As common.Controls.MyLabel
    Friend WithEvents txtAmtDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtNoOfInstallment As common.Controls.MyTextBox
    Friend WithEvents txtDedImposed As common.Controls.MyTextBox
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblPayPeriod As common.Controls.MyLabel
    Friend WithEvents txtPayPeriod As common.UserControls.txtFinder
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
End Class
