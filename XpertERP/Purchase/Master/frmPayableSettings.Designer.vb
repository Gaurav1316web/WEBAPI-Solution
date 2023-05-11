<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPayableSettings
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
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.ChkShowCostCenterAndHierarchyLevelInAPInvoiceEntry = New common.Controls.MyCheckBox()
        Me.chkManualEntryofBankDetailsinVM = New common.Controls.MyCheckBox()
        Me.chkPayVoucherAddressOnBankBasis = New common.Controls.MyCheckBox()
        Me.chkAllowVendorGrpDetailMandatory = New common.Controls.MyCheckBox()
        Me.txtdigits = New common.MyNumBox()
        Me.ChkAllowMandFields = New common.Controls.MyCheckBox()
        Me.chkAutoGenerateVCode = New common.Controls.MyCheckBox()
        Me.rdbtnsave = New Telerik.WinControls.UI.RadButton()
        Me.rdbtnclose = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.ChkShowCostCenterAndHierarchyLevelInAPInvoiceEntry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkManualEntryofBankDetailsinVM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPayVoucherAddressOnBankBasis, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAllowVendorGrpDetailMandatory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdigits, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkAllowMandFields, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAutoGenerateVCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(419, 235)
        Me.SplitContainer1.SplitterDistance = 203
        Me.SplitContainer1.TabIndex = 1
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.ChkShowCostCenterAndHierarchyLevelInAPInvoiceEntry)
        Me.RadGroupBox1.Controls.Add(Me.chkManualEntryofBankDetailsinVM)
        Me.RadGroupBox1.Controls.Add(Me.chkPayVoucherAddressOnBankBasis)
        Me.RadGroupBox1.Controls.Add(Me.chkAllowVendorGrpDetailMandatory)
        Me.RadGroupBox1.Controls.Add(Me.txtdigits)
        Me.RadGroupBox1.Controls.Add(Me.ChkAllowMandFields)
        Me.RadGroupBox1.Controls.Add(Me.chkAutoGenerateVCode)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(419, 203)
        Me.RadGroupBox1.TabIndex = 0
        '
        'ChkShowCostCenterAndHierarchyLevelInAPInvoiceEntry
        '
        Me.ChkShowCostCenterAndHierarchyLevelInAPInvoiceEntry.Location = New System.Drawing.Point(7, 115)
        Me.ChkShowCostCenterAndHierarchyLevelInAPInvoiceEntry.MyLinkLable1 = Nothing
        Me.ChkShowCostCenterAndHierarchyLevelInAPInvoiceEntry.MyLinkLable2 = Nothing
        Me.ChkShowCostCenterAndHierarchyLevelInAPInvoiceEntry.Name = "ChkShowCostCenterAndHierarchyLevelInAPInvoiceEntry"
        Me.ChkShowCostCenterAndHierarchyLevelInAPInvoiceEntry.Size = New System.Drawing.Size(309, 18)
        Me.ChkShowCostCenterAndHierarchyLevelInAPInvoiceEntry.TabIndex = 6
        Me.ChkShowCostCenterAndHierarchyLevelInAPInvoiceEntry.Tag1 = Nothing
        Me.ChkShowCostCenterAndHierarchyLevelInAPInvoiceEntry.Text = "Show Cost Center and  Hierarchy Level in AP Invoice Entry"
        '
        'chkManualEntryofBankDetailsinVM
        '
        Me.chkManualEntryofBankDetailsinVM.Location = New System.Drawing.Point(8, 97)
        Me.chkManualEntryofBankDetailsinVM.MyLinkLable1 = Nothing
        Me.chkManualEntryofBankDetailsinVM.MyLinkLable2 = Nothing
        Me.chkManualEntryofBankDetailsinVM.Name = "chkManualEntryofBankDetailsinVM"
        Me.chkManualEntryofBankDetailsinVM.Size = New System.Drawing.Size(284, 18)
        Me.chkManualEntryofBankDetailsinVM.TabIndex = 5
        Me.chkManualEntryofBankDetailsinVM.Tag1 = Nothing
        Me.chkManualEntryofBankDetailsinVM.Text = "Allow Manual Entry of Bank Details in Vendor Master"
        '
        'chkPayVoucherAddressOnBankBasis
        '
        Me.chkPayVoucherAddressOnBankBasis.Location = New System.Drawing.Point(8, 76)
        Me.chkPayVoucherAddressOnBankBasis.MyLinkLable1 = Nothing
        Me.chkPayVoucherAddressOnBankBasis.MyLinkLable2 = Nothing
        Me.chkPayVoucherAddressOnBankBasis.Name = "chkPayVoucherAddressOnBankBasis"
        Me.chkPayVoucherAddressOnBankBasis.Size = New System.Drawing.Size(241, 18)
        Me.chkPayVoucherAddressOnBankBasis.TabIndex = 4
        Me.chkPayVoucherAddressOnBankBasis.Tag1 = Nothing
        Me.chkPayVoucherAddressOnBankBasis.Text = "Address On Payment Voucher on Bank Basis"
        '
        'chkAllowVendorGrpDetailMandatory
        '
        Me.chkAllowVendorGrpDetailMandatory.Location = New System.Drawing.Point(8, 53)
        Me.chkAllowVendorGrpDetailMandatory.MyLinkLable1 = Nothing
        Me.chkAllowVendorGrpDetailMandatory.MyLinkLable2 = Nothing
        Me.chkAllowVendorGrpDetailMandatory.Name = "chkAllowVendorGrpDetailMandatory"
        Me.chkAllowVendorGrpDetailMandatory.Size = New System.Drawing.Size(252, 18)
        Me.chkAllowVendorGrpDetailMandatory.TabIndex = 3
        Me.chkAllowVendorGrpDetailMandatory.Tag1 = Nothing
        Me.chkAllowVendorGrpDetailMandatory.Text = " Allow Vendor Group Details  to be mandatory"
        '
        'txtdigits
        '
        Me.txtdigits.BackColor = System.Drawing.Color.White
        Me.txtdigits.CalculationExpression = Nothing
        Me.txtdigits.DecimalPlaces = 2
        Me.txtdigits.FieldCode = Nothing
        Me.txtdigits.FieldDesc = Nothing
        Me.txtdigits.FieldMaxLength = 0
        Me.txtdigits.FieldName = Nothing
        Me.txtdigits.isCalculatedField = False
        Me.txtdigits.IsSourceFromTable = False
        Me.txtdigits.IsSourceFromValueList = False
        Me.txtdigits.IsUnique = False
        Me.txtdigits.Location = New System.Drawing.Point(305, 8)
        Me.txtdigits.MaxLength = 10
        Me.txtdigits.MendatroryField = False
        Me.txtdigits.MyLinkLable1 = Nothing
        Me.txtdigits.MyLinkLable2 = Nothing
        Me.txtdigits.Name = "txtdigits"
        Me.txtdigits.ReferenceFieldDesc = Nothing
        Me.txtdigits.ReferenceFieldName = Nothing
        Me.txtdigits.ReferenceTableName = Nothing
        Me.txtdigits.Size = New System.Drawing.Size(53, 20)
        Me.txtdigits.TabIndex = 1
        Me.txtdigits.Text = "0"
        Me.txtdigits.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtdigits.Value = 0.0R
        '
        'ChkAllowMandFields
        '
        Me.ChkAllowMandFields.Location = New System.Drawing.Point(8, 29)
        Me.ChkAllowMandFields.MyLinkLable1 = Nothing
        Me.ChkAllowMandFields.MyLinkLable2 = Nothing
        Me.ChkAllowMandFields.Name = "ChkAllowMandFields"
        Me.ChkAllowMandFields.Size = New System.Drawing.Size(167, 18)
        Me.ChkAllowMandFields.TabIndex = 2
        Me.ChkAllowMandFields.Tag1 = Nothing
        Me.ChkAllowMandFields.Text = " Allow fields to be mandatory"
        '
        'chkAutoGenerateVCode
        '
        Me.chkAutoGenerateVCode.Location = New System.Drawing.Point(8, 8)
        Me.chkAutoGenerateVCode.MyLinkLable1 = Nothing
        Me.chkAutoGenerateVCode.MyLinkLable2 = Nothing
        Me.chkAutoGenerateVCode.Name = "chkAutoGenerateVCode"
        Me.chkAutoGenerateVCode.Size = New System.Drawing.Size(289, 18)
        Me.chkAutoGenerateVCode.TabIndex = 0
        Me.chkAutoGenerateVCode.Tag1 = Nothing
        Me.chkAutoGenerateVCode.Text = " Allow to auto generate vendor code with prefix digits"
        '
        'rdbtnsave
        '
        Me.rdbtnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdbtnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtnsave.Location = New System.Drawing.Point(7, 5)
        Me.rdbtnsave.Name = "rdbtnsave"
        Me.rdbtnsave.Size = New System.Drawing.Size(82, 18)
        Me.rdbtnsave.TabIndex = 0
        Me.rdbtnsave.Text = "Save"
        '
        'rdbtnclose
        '
        Me.rdbtnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdbtnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtnclose.Location = New System.Drawing.Point(333, 5)
        Me.rdbtnclose.Name = "rdbtnclose"
        Me.rdbtnclose.Size = New System.Drawing.Size(83, 18)
        Me.rdbtnclose.TabIndex = 1
        Me.rdbtnclose.Text = "Close"
        '
        'FrmPayableSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(419, 235)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmPayableSettings"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Payable Settings"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.ChkShowCostCenterAndHierarchyLevelInAPInvoiceEntry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkManualEntryofBankDetailsinVM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPayVoucherAddressOnBankBasis, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAllowVendorGrpDetailMandatory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdigits, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkAllowMandFields, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAutoGenerateVCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents chkAutoGenerateVCode As common.Controls.MyCheckBox
    Friend WithEvents rdbtnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents ChkAllowMandFields As common.Controls.MyCheckBox
    Friend WithEvents txtdigits As common.MyNumBox
    Friend WithEvents chkAllowVendorGrpDetailMandatory As common.Controls.MyCheckBox
    Friend WithEvents chkPayVoucherAddressOnBankBasis As common.Controls.MyCheckBox
    Friend WithEvents chkManualEntryofBankDetailsinVM As common.Controls.MyCheckBox
    Friend WithEvents ChkShowCostCenterAndHierarchyLevelInAPInvoiceEntry As common.Controls.MyCheckBox
End Class

