<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPriceMasterPlan
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.chkForPrice = New Telerik.WinControls.UI.RadCheckBox()
        Me.lblPricePlanCopyNo = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtRemarks = New common.Controls.MyTextBox()
        Me.fndPricePlanCopy = New common.UserControls.txtFinder()
        Me.lblPricePlanCopy = New common.Controls.MyLabel()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.txtEndDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.txtStartDate = New common.Controls.MyDateTimePicker()
        Me.chkAllUOM = New Telerik.WinControls.UI.RadCheckBox()
        Me.lblvandorno = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtFinder()
        Me.RadLabel15 = New common.Controls.MyLabel()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.chkBackCalculation = New Telerik.WinControls.UI.RadCheckBox()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtnBackCalculationWithoutTax = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtnBackCalculationWithTax = New Telerik.WinControls.UI.RadRadioButton()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.BtnHistory = New Telerik.WinControls.UI.RadButton()
        Me.RadLabel12 = New common.Controls.MyLabel()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.Filemenu = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem5 = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.chkForPrice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPricePlanCopyNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPricePlanCopy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEndDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtStartDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAllUOM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblvandorno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkBackCalculation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.rbtnBackCalculationWithoutTax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnBackCalculationWithTax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Panel1.Padding = New System.Windows.Forms.Padding(3)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnHistory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadLabel12)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.SplitContainer1.Size = New System.Drawing.Size(938, 469)
        Me.SplitContainer1.SplitterDistance = 435
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkForPrice)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblPricePlanCopyNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtRemarks)
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndPricePlanCopy)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtEndDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblPricePlanCopy)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtStartDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkAllUOM)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblvandorno)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtLocation)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel3)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel15)
        Me.SplitContainer2.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblLocation)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkBackCalculation)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnnew)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel4)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadGroupBox2)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gv1)
        Me.SplitContainer2.Size = New System.Drawing.Size(932, 429)
        Me.SplitContainer2.SplitterDistance = 118
        Me.SplitContainer2.TabIndex = 0
        '
        'chkForPrice
        '
        Me.chkForPrice.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkForPrice.Location = New System.Drawing.Point(598, 24)
        Me.chkForPrice.Name = "chkForPrice"
        Me.chkForPrice.Size = New System.Drawing.Size(68, 18)
        Me.chkForPrice.TabIndex = 3
        Me.chkForPrice.Text = "FOR Price"
        Me.chkForPrice.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'lblPricePlanCopyNo
        '
        Me.lblPricePlanCopyNo.AutoSize = False
        Me.lblPricePlanCopyNo.BorderVisible = True
        Me.lblPricePlanCopyNo.FieldName = Nothing
        Me.lblPricePlanCopyNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPricePlanCopyNo.Location = New System.Drawing.Point(643, 71)
        Me.lblPricePlanCopyNo.Name = "lblPricePlanCopyNo"
        Me.lblPricePlanCopyNo.Size = New System.Drawing.Size(185, 18)
        Me.lblPricePlanCopyNo.TabIndex = 18
        Me.lblPricePlanCopyNo.TextWrap = False
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(532, 71)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(105, 16)
        Me.MyLabel3.TabIndex = 17
        Me.MyLabel3.Text = "Price Plan Copy No"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(5, 71)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(51, 16)
        Me.MyLabel2.TabIndex = 16
        Me.MyLabel2.Text = "Remarks"
        '
        'txtRemarks
        '
        Me.txtRemarks.AutoSize = False
        Me.txtRemarks.CalculationExpression = Nothing
        Me.txtRemarks.FieldCode = Nothing
        Me.txtRemarks.FieldDesc = Nothing
        Me.txtRemarks.FieldMaxLength = 0
        Me.txtRemarks.FieldName = Nothing
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.isCalculatedField = False
        Me.txtRemarks.IsSourceFromTable = False
        Me.txtRemarks.IsSourceFromValueList = False
        Me.txtRemarks.IsUnique = False
        Me.txtRemarks.Location = New System.Drawing.Point(92, 68)
        Me.txtRemarks.MendatroryField = False
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.MyLinkLable1 = Nothing
        Me.txtRemarks.MyLinkLable2 = Nothing
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.ReferenceFieldDesc = Nothing
        Me.txtRemarks.ReferenceFieldName = Nothing
        Me.txtRemarks.ReferenceTableName = Nothing
        Me.txtRemarks.Size = New System.Drawing.Size(434, 45)
        Me.txtRemarks.TabIndex = 15
        '
        'fndPricePlanCopy
        '
        Me.fndPricePlanCopy.CalculationExpression = Nothing
        Me.fndPricePlanCopy.FieldCode = Nothing
        Me.fndPricePlanCopy.FieldDesc = Nothing
        Me.fndPricePlanCopy.FieldMaxLength = 0
        Me.fndPricePlanCopy.FieldName = Nothing
        Me.fndPricePlanCopy.isCalculatedField = False
        Me.fndPricePlanCopy.IsSourceFromTable = False
        Me.fndPricePlanCopy.IsSourceFromValueList = False
        Me.fndPricePlanCopy.IsUnique = False
        Me.fndPricePlanCopy.Location = New System.Drawing.Point(764, 25)
        Me.fndPricePlanCopy.MendatroryField = True
        Me.fndPricePlanCopy.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndPricePlanCopy.MyLinkLable1 = Me.lblPricePlanCopy
        Me.fndPricePlanCopy.MyLinkLable2 = Me.lblLocation
        Me.fndPricePlanCopy.MyReadOnly = False
        Me.fndPricePlanCopy.MyShowMasterFormButton = False
        Me.fndPricePlanCopy.Name = "fndPricePlanCopy"
        Me.fndPricePlanCopy.ReferenceFieldDesc = Nothing
        Me.fndPricePlanCopy.ReferenceFieldName = Nothing
        Me.fndPricePlanCopy.ReferenceTableName = Nothing
        Me.fndPricePlanCopy.Size = New System.Drawing.Size(164, 18)
        Me.fndPricePlanCopy.TabIndex = 11
        Me.fndPricePlanCopy.Value = ""
        '
        'lblPricePlanCopy
        '
        Me.lblPricePlanCopy.FieldName = Nothing
        Me.lblPricePlanCopy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPricePlanCopy.Location = New System.Drawing.Point(670, 26)
        Me.lblPricePlanCopy.Name = "lblPricePlanCopy"
        Me.lblPricePlanCopy.Size = New System.Drawing.Size(87, 16)
        Me.lblPricePlanCopy.TabIndex = 12
        Me.lblPricePlanCopy.Text = "Price Plan Copy"
        '
        'lblLocation
        '
        Me.lblLocation.AutoSize = False
        Me.lblLocation.BorderVisible = True
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(225, 25)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(301, 18)
        Me.lblLocation.TabIndex = 8
        Me.lblLocation.TextWrap = False
        '
        'txtEndDate
        '
        Me.txtEndDate.CalculationExpression = Nothing
        Me.txtEndDate.CustomFormat = "dd/MM/yyyy"
        Me.txtEndDate.FieldCode = Nothing
        Me.txtEndDate.FieldDesc = Nothing
        Me.txtEndDate.FieldMaxLength = 0
        Me.txtEndDate.FieldName = Nothing
        Me.txtEndDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtEndDate.isCalculatedField = False
        Me.txtEndDate.IsSourceFromTable = False
        Me.txtEndDate.IsSourceFromValueList = False
        Me.txtEndDate.IsUnique = False
        Me.txtEndDate.Location = New System.Drawing.Point(283, 46)
        Me.txtEndDate.MendatroryField = False
        Me.txtEndDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtEndDate.MyLinkLable1 = Me.RadLabel4
        Me.txtEndDate.MyLinkLable2 = Nothing
        Me.txtEndDate.Name = "txtEndDate"
        Me.txtEndDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtEndDate.ReferenceFieldDesc = Nothing
        Me.txtEndDate.ReferenceFieldName = Nothing
        Me.txtEndDate.ReferenceTableName = Nothing
        Me.txtEndDate.ShowCheckBox = True
        Me.txtEndDate.Size = New System.Drawing.Size(129, 18)
        Me.txtEndDate.TabIndex = 4
        Me.txtEndDate.TabStop = False
        Me.txtEndDate.Text = "13/06/2011"
        Me.txtEndDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(416, 5)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 11
        Me.RadLabel4.Text = "Date"
        '
        'txtStartDate
        '
        Me.txtStartDate.CalculationExpression = Nothing
        Me.txtStartDate.CustomFormat = "dd/MM/yyyy"
        Me.txtStartDate.FieldCode = Nothing
        Me.txtStartDate.FieldDesc = Nothing
        Me.txtStartDate.FieldMaxLength = 0
        Me.txtStartDate.FieldName = Nothing
        Me.txtStartDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtStartDate.isCalculatedField = False
        Me.txtStartDate.IsSourceFromTable = False
        Me.txtStartDate.IsSourceFromValueList = False
        Me.txtStartDate.IsUnique = False
        Me.txtStartDate.Location = New System.Drawing.Point(95, 46)
        Me.txtStartDate.MendatroryField = False
        Me.txtStartDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtStartDate.MyLinkLable1 = Me.RadLabel4
        Me.txtStartDate.MyLinkLable2 = Nothing
        Me.txtStartDate.Name = "txtStartDate"
        Me.txtStartDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtStartDate.ReferenceFieldDesc = Nothing
        Me.txtStartDate.ReferenceFieldName = Nothing
        Me.txtStartDate.ReferenceTableName = Nothing
        Me.txtStartDate.Size = New System.Drawing.Size(129, 18)
        Me.txtStartDate.TabIndex = 3
        Me.txtStartDate.TabStop = False
        Me.txtStartDate.Text = "13/06/2011"
        Me.txtStartDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'chkAllUOM
        '
        Me.chkAllUOM.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllUOM.Enabled = False
        Me.chkAllUOM.Location = New System.Drawing.Point(532, 25)
        Me.chkAllUOM.Name = "chkAllUOM"
        Me.chkAllUOM.Size = New System.Drawing.Size(63, 18)
        Me.chkAllUOM.TabIndex = 2
        Me.chkAllUOM.Text = "All UOM"
        Me.chkAllUOM.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'lblvandorno
        '
        Me.lblvandorno.FieldName = Nothing
        Me.lblvandorno.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblvandorno.Location = New System.Drawing.Point(5, 5)
        Me.lblvandorno.Name = "lblvandorno"
        Me.lblvandorno.Size = New System.Drawing.Size(80, 16)
        Me.lblvandorno.TabIndex = 11
        Me.lblvandorno.Text = "Planning Code"
        '
        'MyLabel1
        '
        Me.MyLabel1.BackColor = System.Drawing.Color.Transparent
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(225, 46)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(52, 18)
        Me.MyLabel1.TabIndex = 7
        Me.MyLabel1.Text = "End Date"
        '
        'txtLocation
        '
        Me.txtLocation.CalculationExpression = Nothing
        Me.txtLocation.FieldCode = Nothing
        Me.txtLocation.FieldDesc = Nothing
        Me.txtLocation.FieldMaxLength = 0
        Me.txtLocation.FieldName = Nothing
        Me.txtLocation.isCalculatedField = False
        Me.txtLocation.IsSourceFromTable = False
        Me.txtLocation.IsSourceFromValueList = False
        Me.txtLocation.IsUnique = False
        Me.txtLocation.Location = New System.Drawing.Point(95, 25)
        Me.txtLocation.MendatroryField = True
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.RadLabel15
        Me.txtLocation.MyLinkLable2 = Me.lblLocation
        Me.txtLocation.MyReadOnly = False
        Me.txtLocation.MyShowMasterFormButton = False
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.ReferenceFieldDesc = Nothing
        Me.txtLocation.ReferenceFieldName = Nothing
        Me.txtLocation.ReferenceTableName = Nothing
        Me.txtLocation.Size = New System.Drawing.Size(129, 18)
        Me.txtLocation.TabIndex = 1
        Me.txtLocation.Value = ""
        '
        'RadLabel15
        '
        Me.RadLabel15.FieldName = Nothing
        Me.RadLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel15.Location = New System.Drawing.Point(5, 26)
        Me.RadLabel15.Name = "RadLabel15"
        Me.RadLabel15.Size = New System.Drawing.Size(49, 16)
        Me.RadLabel15.TabIndex = 10
        Me.RadLabel15.Text = "Location"
        '
        'RadLabel3
        '
        Me.RadLabel3.BackColor = System.Drawing.Color.Transparent
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Location = New System.Drawing.Point(5, 46)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(56, 18)
        Me.RadLabel3.TabIndex = 9
        Me.RadLabel3.Text = "Start Date"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(532, 4)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(147, 19)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 14
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(95, 3)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblvandorno
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 32767
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(295, 20)
        Me.txtCode.TabIndex = 12
        Me.txtCode.TabStop = False
        Me.txtCode.Value = ""
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
        Me.txtDate.Location = New System.Drawing.Point(449, 4)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.RadLabel4
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(77, 18)
        Me.txtDate.TabIndex = 0
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'chkBackCalculation
        '
        Me.chkBackCalculation.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkBackCalculation.Enabled = False
        Me.chkBackCalculation.Location = New System.Drawing.Point(424, 46)
        Me.chkBackCalculation.Name = "chkBackCalculation"
        Me.chkBackCalculation.Size = New System.Drawing.Size(102, 18)
        Me.chkBackCalculation.TabIndex = 5
        Me.chkBackCalculation.Text = "Back Calculation"
        Me.chkBackCalculation.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'btnnew
        '
        Me.btnnew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnnew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnnew.Location = New System.Drawing.Point(389, 3)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(20, 20)
        Me.btnnew.TabIndex = 13
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.rbtnBackCalculationWithoutTax)
        Me.RadGroupBox2.Controls.Add(Me.rbtnBackCalculationWithTax)
        Me.RadGroupBox2.HeaderText = ""
        Me.RadGroupBox2.Location = New System.Drawing.Point(532, 43)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Size = New System.Drawing.Size(154, 26)
        Me.RadGroupBox2.TabIndex = 6
        Me.RadGroupBox2.Visible = False
        '
        'rbtnBackCalculationWithoutTax
        '
        Me.rbtnBackCalculationWithoutTax.Location = New System.Drawing.Point(66, 3)
        Me.rbtnBackCalculationWithoutTax.Name = "rbtnBackCalculationWithoutTax"
        Me.rbtnBackCalculationWithoutTax.Size = New System.Drawing.Size(81, 18)
        Me.rbtnBackCalculationWithoutTax.TabIndex = 1
        Me.rbtnBackCalculationWithoutTax.TabStop = False
        Me.rbtnBackCalculationWithoutTax.Text = "Without Tax"
        '
        'rbtnBackCalculationWithTax
        '
        Me.rbtnBackCalculationWithTax.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rbtnBackCalculationWithTax.Location = New System.Drawing.Point(4, 3)
        Me.rbtnBackCalculationWithTax.Name = "rbtnBackCalculationWithTax"
        Me.rbtnBackCalculationWithTax.Size = New System.Drawing.Size(61, 18)
        Me.rbtnBackCalculationWithTax.TabIndex = 0
        Me.rbtnBackCalculationWithTax.Text = "WithTax"
        Me.rbtnBackCalculationWithTax.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'gv1
        '
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Enabled = False
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(932, 307)
        Me.gv1.TabIndex = 0
        '
        'BtnHistory
        '
        Me.BtnHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnHistory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnHistory.Location = New System.Drawing.Point(791, 5)
        Me.BtnHistory.Name = "BtnHistory"
        Me.BtnHistory.Size = New System.Drawing.Size(64, 20)
        Me.BtnHistory.TabIndex = 53
        Me.BtnHistory.Text = "History"
        '
        'RadLabel12
        '
        Me.RadLabel12.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadLabel12.FieldName = Nothing
        Me.RadLabel12.Font = New System.Drawing.Font("Arial", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel12.ForeColor = System.Drawing.SystemColors.Highlight
        Me.RadLabel12.Location = New System.Drawing.Point(347, 12)
        Me.RadLabel12.Name = "RadLabel12"
        Me.RadLabel12.Size = New System.Drawing.Size(404, 13)
        Me.RadLabel12.TabIndex = 26
        Me.RadLabel12.Text = "Double click || 'Total Tax Amount' To Set Item wise Tax || 'SNo' To view Price Fo" &
    "r ALL UOM"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Location = New System.Drawing.Point(156, 5)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(73, 20)
        Me.btnPost.TabIndex = 2
        Me.btnPost.Text = "Post"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(861, 5)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(73, 20)
        Me.btnclose.TabIndex = 3
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(81, 5)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(73, 20)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(6, 5)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(73, 20)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.Filemenu})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(938, 20)
        Me.RadMenu1.TabIndex = 2
        '
        'Filemenu
        '
        Me.Filemenu.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItem2, Me.RadMenuItem3, Me.RadMenuItem4, Me.RadMenuItem5})
        Me.Filemenu.Name = "Filemenu"
        Me.Filemenu.Text = "File"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Export"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Import"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Export Grid Data"
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Save Layout"
        '
        'RadMenuItem5
        '
        Me.RadMenuItem5.Name = "RadMenuItem5"
        Me.RadMenuItem5.Text = "Delete Layout"
        '
        'frmPriceMasterPlan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(938, 489)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmPriceMasterPlan"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Price Plan Master"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.chkForPrice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPricePlanCopyNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPricePlanCopy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEndDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtStartDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAllUOM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblvandorno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkBackCalculation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.rbtnBackCalculationWithoutTax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnBackCalculationWithTax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblvandorno As common.Controls.MyLabel
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents RadLabel15 As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtFinder
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents chkBackCalculation As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents rbtnBackCalculationWithoutTax As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnBackCalculationWithTax As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents Filemenu As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents chkAllUOM As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents txtEndDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtStartDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel12 As common.Controls.MyLabel
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem4 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem5 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents fndPricePlanCopy As common.UserControls.txtFinder
    Friend WithEvents lblPricePlanCopy As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtRemarks As common.Controls.MyTextBox
    Friend WithEvents lblPricePlanCopyNo As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents BtnHistory As RadButton
    Friend WithEvents chkForPrice As RadCheckBox
End Class
