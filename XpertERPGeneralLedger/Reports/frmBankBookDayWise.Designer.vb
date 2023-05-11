<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmBankBookDayWise
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
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgBanks = New common.MyCheckBoxGrid()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.chkBanksSelect = New common.Controls.MyRadioButton()
        Me.chkBanksAll = New common.Controls.MyRadioButton()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.chkLocAll = New common.Controls.MyRadioButton()
        Me.cbgLocation = New common.MyCheckBoxGrid()
        Me.ddlBankType = New common.Controls.MyComboBox()
        Me.chkLocSelect = New common.Controls.MyRadioButton()
        Me.lbltype = New common.Controls.MyLabel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox()
        Me.dtFrm = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.dtTo = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkSummary = New common.Controls.MyCheckBox()
        Me.chkExcludeProvisionBank = New System.Windows.Forms.CheckBox()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkBanksSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkBanksAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlBankType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbltype, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        CType(Me.dtFrm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.chkSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.cbgBanks)
        Me.RadGroupBox1.Controls.Add(Me.Panel1)
        Me.RadGroupBox1.HeaderText = "Bank"
        Me.RadGroupBox1.Location = New System.Drawing.Point(7, 49)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(521, 159)
        Me.RadGroupBox1.TabIndex = 324
        Me.RadGroupBox1.Text = "Bank"
        '
        'cbgBanks
        '
        Me.cbgBanks.CheckedValue = Nothing
        Me.cbgBanks.DataSource = Nothing
        Me.cbgBanks.DisplayMember = "Name"
        Me.cbgBanks.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgBanks.Location = New System.Drawing.Point(10, 40)
        Me.cbgBanks.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgBanks.MyShowHeadrText = False
        Me.cbgBanks.Name = "cbgBanks"
        Me.cbgBanks.Size = New System.Drawing.Size(501, 109)
        Me.cbgBanks.TabIndex = 2
        Me.cbgBanks.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkBanksSelect)
        Me.Panel1.Controls.Add(Me.chkBanksAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(501, 20)
        Me.Panel1.TabIndex = 1
        '
        'chkBanksSelect
        '
        Me.chkBanksSelect.Location = New System.Drawing.Point(189, 1)
        Me.chkBanksSelect.MyLinkLable1 = Nothing
        Me.chkBanksSelect.MyLinkLable2 = Nothing
        Me.chkBanksSelect.Name = "chkBanksSelect"
        Me.chkBanksSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkBanksSelect.TabIndex = 2
        Me.chkBanksSelect.Text = "Select"
        '
        'chkBanksAll
        '
        Me.chkBanksAll.Location = New System.Drawing.Point(140, 1)
        Me.chkBanksAll.MyLinkLable1 = Nothing
        Me.chkBanksAll.MyLinkLable2 = Nothing
        Me.chkBanksAll.Name = "chkBanksAll"
        Me.chkBanksAll.Size = New System.Drawing.Size(33, 18)
        Me.chkBanksAll.TabIndex = 1
        Me.chkBanksAll.Text = "All"
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Location = New System.Drawing.Point(13, 14)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(34, 18)
        Me.RadLabel2.TabIndex = 315
        Me.RadLabel2.Text = "From:"
        '
        'chkLocAll
        '
        Me.chkLocAll.Location = New System.Drawing.Point(140, 1)
        Me.chkLocAll.MyLinkLable1 = Nothing
        Me.chkLocAll.MyLinkLable2 = Nothing
        Me.chkLocAll.Name = "chkLocAll"
        Me.chkLocAll.Size = New System.Drawing.Size(33, 18)
        Me.chkLocAll.TabIndex = 1
        Me.chkLocAll.Text = "All"
        '
        'cbgLocation
        '
        Me.cbgLocation.CheckedValue = Nothing
        Me.cbgLocation.DataSource = Nothing
        Me.cbgLocation.DisplayMember = "Name"
        Me.cbgLocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgLocation.Location = New System.Drawing.Point(10, 40)
        Me.cbgLocation.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgLocation.MyShowHeadrText = False
        Me.cbgLocation.Name = "cbgLocation"
        Me.cbgLocation.Size = New System.Drawing.Size(501, 109)
        Me.cbgLocation.TabIndex = 2
        Me.cbgLocation.ValueMember = "Code"
        '
        'ddlBankType
        '
        Me.ddlBankType.CalculationExpression = Nothing
        Me.ddlBankType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlBankType.FieldCode = Nothing
        Me.ddlBankType.FieldDesc = Nothing
        Me.ddlBankType.FieldMaxLength = 0
        Me.ddlBankType.FieldName = Nothing
        Me.ddlBankType.isCalculatedField = False
        Me.ddlBankType.IsSourceFromTable = False
        Me.ddlBankType.IsSourceFromValueList = False
        Me.ddlBankType.IsUnique = False
        RadListDataItem1.Text = "Bank"
        RadListDataItem2.Text = "Cash"
        RadListDataItem3.Text = "Petty Cash"
        RadListDataItem4.Enabled = False
        RadListDataItem4.Text = "Settlement"
        Me.ddlBankType.Items.Add(RadListDataItem1)
        Me.ddlBankType.Items.Add(RadListDataItem2)
        Me.ddlBankType.Items.Add(RadListDataItem3)
        Me.ddlBankType.Items.Add(RadListDataItem4)
        Me.ddlBankType.Location = New System.Drawing.Point(424, 14)
        Me.ddlBankType.MendatroryField = False
        Me.ddlBankType.MyLinkLable1 = Nothing
        Me.ddlBankType.MyLinkLable2 = Nothing
        Me.ddlBankType.Name = "ddlBankType"
        Me.ddlBankType.ReferenceFieldDesc = Nothing
        Me.ddlBankType.ReferenceFieldName = Nothing
        Me.ddlBankType.ReferenceTableName = Nothing
        Me.ddlBankType.Size = New System.Drawing.Size(106, 20)
        Me.ddlBankType.TabIndex = 325
        '
        'chkLocSelect
        '
        Me.chkLocSelect.Location = New System.Drawing.Point(189, 1)
        Me.chkLocSelect.MyLinkLable1 = Nothing
        Me.chkLocSelect.MyLinkLable2 = Nothing
        Me.chkLocSelect.Name = "chkLocSelect"
        Me.chkLocSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkLocSelect.TabIndex = 2
        Me.chkLocSelect.Text = "Select"
        '
        'lbltype
        '
        Me.lbltype.FieldName = Nothing
        Me.lbltype.Location = New System.Drawing.Point(387, 14)
        Me.lbltype.Name = "lbltype"
        Me.lbltype.Size = New System.Drawing.Size(30, 18)
        Me.lbltype.TabIndex = 317
        Me.lbltype.Text = "Type"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.chkLocSelect)
        Me.Panel3.Controls.Add(Me.chkLocAll)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(10, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(501, 20)
        Me.Panel3.TabIndex = 1
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.cbgLocation)
        Me.RadGroupBox5.Controls.Add(Me.Panel3)
        Me.RadGroupBox5.HeaderText = "Location"
        Me.RadGroupBox5.Location = New System.Drawing.Point(7, 214)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox5.Size = New System.Drawing.Size(521, 159)
        Me.RadGroupBox5.TabIndex = 323
        Me.RadGroupBox5.Text = "Location"
        '
        'dtFrm
        '
        Me.dtFrm.CustomFormat = "dd/MM/yyyy"
        Me.dtFrm.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtFrm.Location = New System.Drawing.Point(58, 14)
        Me.dtFrm.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtFrm.Name = "dtFrm"
        Me.dtFrm.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtFrm.Size = New System.Drawing.Size(87, 20)
        Me.dtFrm.TabIndex = 318
        Me.dtFrm.TabStop = False
        Me.dtFrm.Text = "31/08/2011"
        Me.dtFrm.Value = New Date(2011, 8, 31, 23, 50, 36, 937)
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Location = New System.Drawing.Point(166, 16)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(21, 18)
        Me.RadLabel3.TabIndex = 316
        Me.RadLabel3.Text = "To:"
        '
        'dtTo
        '
        Me.dtTo.CustomFormat = "dd/MM/yyyy"
        Me.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtTo.Location = New System.Drawing.Point(195, 14)
        Me.dtTo.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtTo.Name = "dtTo"
        Me.dtTo.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtTo.Size = New System.Drawing.Size(83, 20)
        Me.dtTo.TabIndex = 319
        Me.dtTo.TabStop = False
        Me.dtTo.Text = "31/08/2011"
        Me.dtTo.Value = New Date(2011, 8, 31, 23, 50, 36, 937)
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Location = New System.Drawing.Point(17, 4)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(72, 19)
        Me.btnReset.TabIndex = 322
        Me.btnReset.Text = "Reset"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(470, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(72, 19)
        Me.btnClose.TabIndex = 321
        Me.btnClose.Text = "Close"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(99, 4)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(72, 19)
        Me.btnPrint.TabIndex = 320
        Me.btnPrint.Text = "Print"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Size = New System.Drawing.Size(565, 429)
        Me.SplitContainer1.SplitterDistance = 398
        Me.SplitContainer1.TabIndex = 327
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.chkExcludeProvisionBank)
        Me.RadGroupBox2.Controls.Add(Me.chkSummary)
        Me.RadGroupBox2.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox2.Controls.Add(Me.RadGroupBox1)
        Me.RadGroupBox2.Controls.Add(Me.RadGroupBox5)
        Me.RadGroupBox2.Controls.Add(Me.RadLabel3)
        Me.RadGroupBox2.Controls.Add(Me.dtFrm)
        Me.RadGroupBox2.Controls.Add(Me.lbltype)
        Me.RadGroupBox2.Controls.Add(Me.dtTo)
        Me.RadGroupBox2.Controls.Add(Me.ddlBankType)
        Me.RadGroupBox2.HeaderText = ""
        Me.RadGroupBox2.Location = New System.Drawing.Point(12, 12)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(541, 383)
        Me.RadGroupBox2.TabIndex = 326
        '
        'chkSummary
        '
        Me.chkSummary.Location = New System.Drawing.Point(296, 14)
        Me.chkSummary.MyLinkLable1 = Nothing
        Me.chkSummary.MyLinkLable2 = Nothing
        Me.chkSummary.Name = "chkSummary"
        Me.chkSummary.Size = New System.Drawing.Size(67, 18)
        Me.chkSummary.TabIndex = 326
        Me.chkSummary.Tag1 = Nothing
        Me.chkSummary.Text = "Summary"
        '
        'chkExcludeProvisionBank
        '
        Me.chkExcludeProvisionBank.AutoSize = True
        Me.chkExcludeProvisionBank.Location = New System.Drawing.Point(387, 38)
        Me.chkExcludeProvisionBank.Name = "chkExcludeProvisionBank"
        Me.chkExcludeProvisionBank.Size = New System.Drawing.Size(143, 17)
        Me.chkExcludeProvisionBank.TabIndex = 358
        Me.chkExcludeProvisionBank.Text = "Exclude Provision Bank"
        Me.chkExcludeProvisionBank.UseVisualStyleBackColor = True
        '
        'FrmBankBookDayWise
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(565, 429)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmBankBookDayWise"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Bank/Cash Book Day Wise"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkBanksSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkBanksAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlBankType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbltype, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        CType(Me.dtFrm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.chkSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgBanks As common.MyCheckBoxGrid
    Friend WithEvents chkBanksSelect As common.Controls.MyRadioButton
    Friend WithEvents chkBanksAll As common.Controls.MyRadioButton
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents chkLocAll As common.Controls.MyRadioButton
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents ddlBankType As common.Controls.MyComboBox
    Friend WithEvents chkLocSelect As common.Controls.MyRadioButton
    Friend WithEvents lbltype As common.Controls.MyLabel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents RadGroupBox5 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents dtFrm As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents dtTo As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents chkSummary As common.Controls.MyCheckBox
    Friend WithEvents chkExcludeProvisionBank As CheckBox
End Class

