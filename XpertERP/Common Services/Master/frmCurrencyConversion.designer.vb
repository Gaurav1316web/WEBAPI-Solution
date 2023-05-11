<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCurrencyConversion
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCurrencyConversion))
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.txtRate = New common.MyNumBox
        Me.lblNoOfEMI = New common.Controls.MyLabel
        Me.lblToCurrency = New common.Controls.MyLabel
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.txtToCurrency = New common.UserControls.txtFinder
        Me.lblFromCurrency = New common.Controls.MyLabel
        Me.lblPayHeadCode = New common.Controls.MyLabel
        Me.txtFromCurrency = New common.UserControls.txtFinder
        Me.lblApplicableDate = New common.Controls.MyLabel
        Me.dtpToDate = New common.Controls.MyDateTimePicker
        Me.lblIncrementDate = New common.Controls.MyLabel
        Me.dtpFromDate = New common.Controls.MyDateTimePicker
        Me.txtDescription = New common.Controls.MyTextBox
        Me.RadLabel3 = New common.Controls.MyLabel
        Me.RadLabel4 = New common.Controls.MyLabel
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton
        Me.btnNew = New Telerik.WinControls.UI.RadButton
        Me.txtCode = New common.UserControls.txtNavigator
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnDelete = New Telerik.WinControls.UI.RadButton
        Me.btnSave = New Telerik.WinControls.UI.RadButton
        Me.RadMenuItemExport = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.txtRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNoOfEMI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToCurrency, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFromCurrency, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPayHeadCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblApplicableDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblIncrementDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.txtRate)
        Me.RadGroupBox1.Controls.Add(Me.lblNoOfEMI)
        Me.RadGroupBox1.Controls.Add(Me.lblToCurrency)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.txtToCurrency)
        Me.RadGroupBox1.Controls.Add(Me.lblFromCurrency)
        Me.RadGroupBox1.Controls.Add(Me.lblPayHeadCode)
        Me.RadGroupBox1.Controls.Add(Me.txtFromCurrency)
        Me.RadGroupBox1.Controls.Add(Me.lblApplicableDate)
        Me.RadGroupBox1.Controls.Add(Me.dtpToDate)
        Me.RadGroupBox1.Controls.Add(Me.lblIncrementDate)
        Me.RadGroupBox1.Controls.Add(Me.dtpFromDate)
        Me.RadGroupBox1.Controls.Add(Me.txtDescription)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel3)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel4)
        Me.RadGroupBox1.Controls.Add(Me.RadButton1)
        Me.RadGroupBox1.Controls.Add(Me.btnNew)
        Me.RadGroupBox1.Controls.Add(Me.txtCode)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.HeaderText = " "
        Me.RadGroupBox1.Location = New System.Drawing.Point(4, 26)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(519, 178)
        Me.RadGroupBox1.TabIndex = 1
        Me.RadGroupBox1.Text = " "
        '
        'txtRate
        '
        Me.txtRate.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtRate.DecimalPlaces = 6
        Me.txtRate.Location = New System.Drawing.Point(109, 109)
        Me.txtRate.MaxLength = 18
        Me.txtRate.MendatroryField = True
        Me.txtRate.MyLinkLable1 = Me.lblNoOfEMI
        Me.txtRate.MyLinkLable2 = Nothing
        Me.txtRate.Name = "txtRate"
        Me.txtRate.Size = New System.Drawing.Size(161, 20)
        Me.txtRate.TabIndex = 6
        Me.txtRate.Text = "0"
        Me.txtRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtRate.Value = 0
        '
        'lblNoOfEMI
        '
        Me.lblNoOfEMI.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNoOfEMI.Location = New System.Drawing.Point(13, 111)
        Me.lblNoOfEMI.Name = "lblNoOfEMI"
        Me.lblNoOfEMI.Size = New System.Drawing.Size(30, 16)
        Me.lblNoOfEMI.TabIndex = 9
        Me.lblNoOfEMI.Text = "Rate"
        '
        'lblToCurrency
        '
        Me.lblToCurrency.AutoSize = False
        Me.lblToCurrency.BorderVisible = True
        Me.lblToCurrency.Location = New System.Drawing.Point(284, 88)
        Me.lblToCurrency.Name = "lblToCurrency"
        Me.lblToCurrency.Size = New System.Drawing.Size(216, 19)
        Me.lblToCurrency.TabIndex = 15
        Me.lblToCurrency.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(13, 89)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(68, 16)
        Me.MyLabel2.TabIndex = 11
        Me.MyLabel2.Text = "To Currency"
        '
        'txtToCurrency
        '
        Me.txtToCurrency.Enabled = False
        Me.txtToCurrency.Location = New System.Drawing.Point(108, 88)
        Me.txtToCurrency.MendatroryField = True
        Me.txtToCurrency.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToCurrency.MyLinkLable1 = Me.MyLabel2
        Me.txtToCurrency.MyLinkLable2 = Nothing
        Me.txtToCurrency.MyReadOnly = False
        Me.txtToCurrency.Name = "txtToCurrency"
        Me.txtToCurrency.Size = New System.Drawing.Size(162, 19)
        Me.txtToCurrency.TabIndex = 5
        Me.txtToCurrency.Value = ""
        '
        'lblFromCurrency
        '
        Me.lblFromCurrency.AutoSize = False
        Me.lblFromCurrency.BorderVisible = True
        Me.lblFromCurrency.Location = New System.Drawing.Point(284, 66)
        Me.lblFromCurrency.Name = "lblFromCurrency"
        Me.lblFromCurrency.Size = New System.Drawing.Size(216, 19)
        Me.lblFromCurrency.TabIndex = 16
        Me.lblFromCurrency.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPayHeadCode
        '
        Me.lblPayHeadCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPayHeadCode.Location = New System.Drawing.Point(13, 67)
        Me.lblPayHeadCode.Name = "lblPayHeadCode"
        Me.lblPayHeadCode.Size = New System.Drawing.Size(82, 16)
        Me.lblPayHeadCode.TabIndex = 12
        Me.lblPayHeadCode.Text = "From Currency"
        '
        'txtFromCurrency
        '
        Me.txtFromCurrency.Location = New System.Drawing.Point(108, 66)
        Me.txtFromCurrency.MendatroryField = True
        Me.txtFromCurrency.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFromCurrency.MyLinkLable1 = Me.lblPayHeadCode
        Me.txtFromCurrency.MyLinkLable2 = Nothing
        Me.txtFromCurrency.MyReadOnly = False
        Me.txtFromCurrency.Name = "txtFromCurrency"
        Me.txtFromCurrency.Size = New System.Drawing.Size(162, 19)
        Me.txtFromCurrency.TabIndex = 4
        Me.txtFromCurrency.Value = ""
        '
        'lblApplicableDate
        '
        Me.lblApplicableDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblApplicableDate.Location = New System.Drawing.Point(284, 47)
        Me.lblApplicableDate.Name = "lblApplicableDate"
        Me.lblApplicableDate.Size = New System.Drawing.Size(46, 16)
        Me.lblApplicableDate.TabIndex = 17
        Me.lblApplicableDate.Text = "To Date"
        Me.lblApplicableDate.Visible = False
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpToDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(344, 44)
        Me.dtpToDate.MendatroryField = True
        Me.dtpToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.MyLinkLable1 = Me.lblApplicableDate
        Me.dtpToDate.MyLinkLable2 = Nothing
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.Size = New System.Drawing.Size(156, 18)
        Me.dtpToDate.TabIndex = 3
        Me.dtpToDate.TabStop = False
        Me.dtpToDate.Text = "10/07/2013"
        Me.dtpToDate.Value = New Date(2013, 7, 10, 0, 0, 0, 0)
        Me.dtpToDate.Visible = False
        '
        'lblIncrementDate
        '
        Me.lblIncrementDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIncrementDate.Location = New System.Drawing.Point(13, 46)
        Me.lblIncrementDate.Name = "lblIncrementDate"
        Me.lblIncrementDate.Size = New System.Drawing.Size(88, 16)
        Me.lblIncrementDate.TabIndex = 13
        Me.lblIncrementDate.Text = "Applicable From"
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpFromDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(108, 45)
        Me.dtpFromDate.MendatroryField = True
        Me.dtpFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromDate.MyLinkLable1 = Me.lblIncrementDate
        Me.dtpFromDate.MyLinkLable2 = Nothing
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromDate.Size = New System.Drawing.Size(162, 18)
        Me.dtpFromDate.TabIndex = 2
        Me.dtpFromDate.TabStop = False
        Me.dtpFromDate.Text = "10/07/2013"
        Me.dtpFromDate.Value = New Date(2013, 7, 10, 0, 0, 0, 0)
        '
        'txtDescription
        '
        Me.txtDescription.Location = New System.Drawing.Point(108, 132)
        Me.txtDescription.MaxLength = 50
        Me.txtDescription.MendatroryField = False
        Me.txtDescription.MyLinkLable1 = Me.RadLabel3
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(389, 20)
        Me.txtDescription.TabIndex = 7
        '
        'RadLabel3
        '
        Me.RadLabel3.Location = New System.Drawing.Point(13, 133)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(44, 18)
        Me.RadLabel3.TabIndex = 8
        Me.RadLabel3.Text = "Remark"
        '
        'RadLabel4
        '
        Me.RadLabel4.Location = New System.Drawing.Point(14, 109)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(2, 2)
        Me.RadLabel4.TabIndex = 10
        '
        'RadButton1
        '
        Me.RadButton1.Image = CType(resources.GetObject("RadButton1.Image"), System.Drawing.Image)
        Me.RadButton1.Location = New System.Drawing.Point(352, -422)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(14, 20)
        Me.RadButton1.TabIndex = 17
        Me.RadButton1.Text = " "
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(333, 21)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 20)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Text = " "
        '
        'txtCode
        '
        Me.txtCode.Location = New System.Drawing.Point(108, 21)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.RadLabel1
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 12
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(222, 21)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(13, 22)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(32, 18)
        Me.RadLabel1.TabIndex = 14
        Me.RadLabel1.Text = "Code"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnClose.Location = New System.Drawing.Point(418, 12)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnDelete.Location = New System.Drawing.Point(74, 12)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 18)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnSave.Location = New System.Drawing.Point(3, 12)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 18)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'RadMenuItemExport
        '
        Me.RadMenuItemExport.AccessibleDescription = "File"
        Me.RadMenuItemExport.AccessibleName = "File"
        Me.RadMenuItemExport.Name = "RadMenuItemExport"
        Me.RadMenuItemExport.Text = "File"
        Me.RadMenuItemExport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Import"
        Me.RadMenuItem1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "File"
        Me.RadMenuItem2.AccessibleName = "File"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Export"
        Me.RadMenuItem2.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(529, 244)
        Me.SplitContainer1.SplitterDistance = 207
        Me.SplitContainer1.TabIndex = 0
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(529, 20)
        Me.RadMenu1.TabIndex = 1
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Setting"
        Me.RadMenuItem3.AccessibleName = "Setting"
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItem2})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        Me.RadMenuItem3.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'frmCurrencyConversion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(529, 244)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmCurrencyConversion"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Currency Conversion Rate"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.txtRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNoOfEMI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToCurrency, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFromCurrency, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPayHeadCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblApplicableDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblIncrementDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenuItemExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblToCurrency As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtToCurrency As common.UserControls.txtFinder
    Friend WithEvents lblFromCurrency As common.Controls.MyLabel
    Friend WithEvents lblPayHeadCode As common.Controls.MyLabel
    Friend WithEvents txtFromCurrency As common.UserControls.txtFinder
    Friend WithEvents lblApplicableDate As common.Controls.MyLabel
    Friend WithEvents dtpToDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblIncrementDate As common.Controls.MyLabel
    Friend WithEvents dtpFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtRate As common.MyNumBox
    Friend WithEvents lblNoOfEMI As common.Controls.MyLabel
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
End Class

