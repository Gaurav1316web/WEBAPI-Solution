Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLeaveOpeningBalance
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLeaveOpeningBalance))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.dtpOpeningDate = New common.Controls.MyDateTimePicker()
        Me.lblPayPeriodName = New common.Controls.MyLabel()
        Me.fndPayPeriod = New common.UserControls.txtFinder()
        Me.lblPayPeriod = New common.Controls.MyLabel()
        Me.lblLeaveDesc = New common.Controls.MyLabel()
        Me.lblEmpName = New common.Controls.MyLabel()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.txtEmpCode = New common.UserControls.txtFinder()
        Me.RadMenu2 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuItemImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuItemExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuItemClose = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtLeaveCode = New common.UserControls.txtFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtOpeningBalance = New common.MyNumBox()
        Me.lblOpeningDate = New common.Controls.MyLabel()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.dtpOpeningDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPayPeriodName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPayPeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLeaveDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmpName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOpeningBalance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblOpeningDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpOpeningDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblPayPeriodName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndPayPeriod)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblPayPeriod)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLeaveDesc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblEmpName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtEmpCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtLeaveCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtOpeningBalance)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblOpeningDate)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(631, 237)
        Me.SplitContainer1.SplitterDistance = 197
        Me.SplitContainer1.TabIndex = 0
        '
        'dtpOpeningDate
        '
        Me.dtpOpeningDate.CalculationExpression = Nothing
        Me.dtpOpeningDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtpOpeningDate.FieldCode = Nothing
        Me.dtpOpeningDate.FieldDesc = Nothing
        Me.dtpOpeningDate.FieldMaxLength = 0
        Me.dtpOpeningDate.FieldName = Nothing
        Me.dtpOpeningDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpOpeningDate.isCalculatedField = False
        Me.dtpOpeningDate.IsSourceFromTable = False
        Me.dtpOpeningDate.IsSourceFromValueList = False
        Me.dtpOpeningDate.IsUnique = False
        Me.dtpOpeningDate.Location = New System.Drawing.Point(133, 119)
        Me.dtpOpeningDate.MendatroryField = False
        Me.dtpOpeningDate.MyLinkLable1 = Nothing
        Me.dtpOpeningDate.MyLinkLable2 = Nothing
        Me.dtpOpeningDate.Name = "dtpOpeningDate"
        Me.dtpOpeningDate.ReferenceFieldDesc = Nothing
        Me.dtpOpeningDate.ReferenceFieldName = Nothing
        Me.dtpOpeningDate.ReferenceTableName = Nothing
        Me.dtpOpeningDate.Size = New System.Drawing.Size(195, 20)
        Me.dtpOpeningDate.TabIndex = 176
        Me.dtpOpeningDate.TabStop = False
        Me.dtpOpeningDate.Text = "30/10/2015"
        Me.dtpOpeningDate.Value = New Date(2015, 10, 30, 11, 14, 30, 0)
        '
        'lblPayPeriodName
        '
        Me.lblPayPeriodName.AutoSize = False
        Me.lblPayPeriodName.BorderVisible = True
        Me.lblPayPeriodName.FieldName = Nothing
        Me.lblPayPeriodName.Location = New System.Drawing.Point(336, 97)
        Me.lblPayPeriodName.Name = "lblPayPeriodName"
        Me.lblPayPeriodName.Size = New System.Drawing.Size(288, 19)
        Me.lblPayPeriodName.TabIndex = 175
        '
        'fndPayPeriod
        '
        Me.fndPayPeriod.CalculationExpression = Nothing
        Me.fndPayPeriod.FieldCode = Nothing
        Me.fndPayPeriod.FieldDesc = Nothing
        Me.fndPayPeriod.FieldMaxLength = 0
        Me.fndPayPeriod.FieldName = Nothing
        Me.fndPayPeriod.isCalculatedField = False
        Me.fndPayPeriod.IsSourceFromTable = False
        Me.fndPayPeriod.IsSourceFromValueList = False
        Me.fndPayPeriod.IsUnique = False
        Me.fndPayPeriod.Location = New System.Drawing.Point(133, 97)
        Me.fndPayPeriod.MendatroryField = True
        Me.fndPayPeriod.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndPayPeriod.MyLinkLable1 = Nothing
        Me.fndPayPeriod.MyLinkLable2 = Nothing
        Me.fndPayPeriod.MyReadOnly = False
        Me.fndPayPeriod.MyShowMasterFormButton = False
        Me.fndPayPeriod.Name = "fndPayPeriod"
        Me.fndPayPeriod.ReferenceFieldDesc = Nothing
        Me.fndPayPeriod.ReferenceFieldName = Nothing
        Me.fndPayPeriod.ReferenceTableName = Nothing
        Me.fndPayPeriod.Size = New System.Drawing.Size(195, 19)
        Me.fndPayPeriod.TabIndex = 174
        Me.fndPayPeriod.Value = ""
        '
        'lblPayPeriod
        '
        Me.lblPayPeriod.FieldName = Nothing
        Me.lblPayPeriod.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPayPeriod.Location = New System.Drawing.Point(15, 100)
        Me.lblPayPeriod.Name = "lblPayPeriod"
        Me.lblPayPeriod.Size = New System.Drawing.Size(92, 16)
        Me.lblPayPeriod.TabIndex = 173
        Me.lblPayPeriod.Text = "Pay Period Code"
        '
        'lblLeaveDesc
        '
        Me.lblLeaveDesc.AutoSize = False
        Me.lblLeaveDesc.BorderVisible = True
        Me.lblLeaveDesc.FieldName = Nothing
        Me.lblLeaveDesc.Location = New System.Drawing.Point(336, 75)
        Me.lblLeaveDesc.Name = "lblLeaveDesc"
        Me.lblLeaveDesc.Size = New System.Drawing.Size(288, 19)
        Me.lblLeaveDesc.TabIndex = 172
        '
        'lblEmpName
        '
        Me.lblEmpName.AutoSize = False
        Me.lblEmpName.BorderVisible = True
        Me.lblEmpName.FieldName = Nothing
        Me.lblEmpName.Location = New System.Drawing.Point(336, 52)
        Me.lblEmpName.Name = "lblEmpName"
        Me.lblEmpName.Size = New System.Drawing.Size(288, 19)
        Me.lblEmpName.TabIndex = 170
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(12, 56)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(87, 16)
        Me.MyLabel6.TabIndex = 171
        Me.MyLabel6.Text = "Employee Code"
        '
        'txtEmpCode
        '
        Me.txtEmpCode.CalculationExpression = Nothing
        Me.txtEmpCode.FieldCode = Nothing
        Me.txtEmpCode.FieldDesc = Nothing
        Me.txtEmpCode.FieldMaxLength = 0
        Me.txtEmpCode.FieldName = Nothing
        Me.txtEmpCode.isCalculatedField = False
        Me.txtEmpCode.IsSourceFromTable = False
        Me.txtEmpCode.IsSourceFromValueList = False
        Me.txtEmpCode.IsUnique = False
        Me.txtEmpCode.Location = New System.Drawing.Point(133, 52)
        Me.txtEmpCode.MendatroryField = True
        Me.txtEmpCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmpCode.MyLinkLable1 = Me.MyLabel6
        Me.txtEmpCode.MyLinkLable2 = Me.lblEmpName
        Me.txtEmpCode.MyReadOnly = False
        Me.txtEmpCode.MyShowMasterFormButton = False
        Me.txtEmpCode.Name = "txtEmpCode"
        Me.txtEmpCode.ReferenceFieldDesc = Nothing
        Me.txtEmpCode.ReferenceFieldName = Nothing
        Me.txtEmpCode.ReferenceTableName = Nothing
        Me.txtEmpCode.Size = New System.Drawing.Size(197, 19)
        Me.txtEmpCode.TabIndex = 169
        Me.txtEmpCode.Value = ""
        '
        'RadMenu2
        '
        Me.RadMenu2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu2.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu2.Name = "RadMenu2"
        Me.RadMenu2.Size = New System.Drawing.Size(631, 20)
        Me.RadMenu2.TabIndex = 168
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.MenuItemImport, Me.MenuItemExport, Me.MenuItemClose})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "File"
        '
        'MenuItemImport
        '
        Me.MenuItemImport.Name = "MenuItemImport"
        Me.MenuItemImport.Text = "Import"
        '
        'MenuItemExport
        '
        Me.MenuItemExport.Name = "MenuItemExport"
        Me.MenuItemExport.Text = "Export"
        '
        'MenuItemClose
        '
        Me.MenuItemClose.Name = "MenuItemClose"
        Me.MenuItemClose.Text = "Close"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(336, 27)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 20)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Text = " "
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(131, 26)
        Me.txtCode.MendatroryField = False
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.MyLabel4
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 30
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(199, 21)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(12, 30)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(33, 16)
        Me.MyLabel4.TabIndex = 153
        Me.MyLabel4.Text = "Code"
        '
        'txtLeaveCode
        '
        Me.txtLeaveCode.CalculationExpression = Nothing
        Me.txtLeaveCode.FieldCode = Nothing
        Me.txtLeaveCode.FieldDesc = Nothing
        Me.txtLeaveCode.FieldMaxLength = 0
        Me.txtLeaveCode.FieldName = Nothing
        Me.txtLeaveCode.isCalculatedField = False
        Me.txtLeaveCode.IsSourceFromTable = False
        Me.txtLeaveCode.IsSourceFromValueList = False
        Me.txtLeaveCode.IsUnique = False
        Me.txtLeaveCode.Location = New System.Drawing.Point(133, 76)
        Me.txtLeaveCode.MendatroryField = True
        Me.txtLeaveCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLeaveCode.MyLinkLable1 = Me.MyLabel2
        Me.txtLeaveCode.MyLinkLable2 = Nothing
        Me.txtLeaveCode.MyReadOnly = False
        Me.txtLeaveCode.MyShowMasterFormButton = False
        Me.txtLeaveCode.Name = "txtLeaveCode"
        Me.txtLeaveCode.ReferenceFieldDesc = Nothing
        Me.txtLeaveCode.ReferenceFieldName = Nothing
        Me.txtLeaveCode.ReferenceTableName = Nothing
        Me.txtLeaveCode.Size = New System.Drawing.Size(195, 19)
        Me.txtLeaveCode.TabIndex = 3
        Me.txtLeaveCode.Value = ""
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(12, 78)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(67, 16)
        Me.MyLabel2.TabIndex = 147
        Me.MyLabel2.Text = "Leave Code"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(12, 150)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(93, 16)
        Me.MyLabel3.TabIndex = 162
        Me.MyLabel3.Text = "Opening Balance"
        '
        'txtOpeningBalance
        '
        Me.txtOpeningBalance.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtOpeningBalance.CalculationExpression = Nothing
        Me.txtOpeningBalance.DecimalPlaces = 2
        Me.txtOpeningBalance.FieldCode = Nothing
        Me.txtOpeningBalance.FieldDesc = Nothing
        Me.txtOpeningBalance.FieldMaxLength = 0
        Me.txtOpeningBalance.FieldName = Nothing
        Me.txtOpeningBalance.isCalculatedField = False
        Me.txtOpeningBalance.IsSourceFromTable = False
        Me.txtOpeningBalance.IsSourceFromValueList = False
        Me.txtOpeningBalance.IsUnique = False
        Me.txtOpeningBalance.Location = New System.Drawing.Point(133, 145)
        Me.txtOpeningBalance.MaxLength = 6
        Me.txtOpeningBalance.MendatroryField = True
        Me.txtOpeningBalance.MyLinkLable1 = Me.MyLabel4
        Me.txtOpeningBalance.MyLinkLable2 = Nothing
        Me.txtOpeningBalance.Name = "txtOpeningBalance"
        Me.txtOpeningBalance.ReferenceFieldDesc = Nothing
        Me.txtOpeningBalance.ReferenceFieldName = Nothing
        Me.txtOpeningBalance.ReferenceTableName = Nothing
        Me.txtOpeningBalance.Size = New System.Drawing.Size(197, 20)
        Me.txtOpeningBalance.TabIndex = 5
        Me.txtOpeningBalance.Text = "0"
        Me.txtOpeningBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtOpeningBalance.Value = 0R
        '
        'lblOpeningDate
        '
        Me.lblOpeningDate.FieldName = Nothing
        Me.lblOpeningDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOpeningDate.Location = New System.Drawing.Point(12, 125)
        Me.lblOpeningDate.Name = "lblOpeningDate"
        Me.lblOpeningDate.Size = New System.Drawing.Size(76, 16)
        Me.lblOpeningDate.TabIndex = 155
        Me.lblOpeningDate.Text = "Opening Date"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(75, 6)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 22)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(3, 6)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 22)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(558, 6)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 22)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'frmLeaveOpeningBalance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(631, 237)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmLeaveOpeningBalance"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Leave Opening Balance"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.dtpOpeningDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPayPeriodName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPayPeriod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLeaveDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmpName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOpeningBalance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblOpeningDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtOpeningBalance As common.MyNumBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents lblOpeningDate As common.Controls.MyLabel
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtLeaveCode As common.UserControls.txtFinder
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents RadMenu2 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuItemImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuItemExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuItemClose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents lblEmpName As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents txtEmpCode As common.UserControls.txtFinder
    Friend WithEvents lblLeaveDesc As common.Controls.MyLabel
    Friend WithEvents lblPayPeriod As common.Controls.MyLabel
    Friend WithEvents lblPayPeriodName As common.Controls.MyLabel
    Friend WithEvents fndPayPeriod As common.UserControls.txtFinder
    Friend WithEvents dtpOpeningDate As common.Controls.MyDateTimePicker
End Class
