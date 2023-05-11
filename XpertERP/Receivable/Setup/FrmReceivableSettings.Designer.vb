<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmReceivableSettings
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
        Me.CHKShowVisiDetail = New common.Controls.MyCheckBox()
        Me.chkDoNotMergeAccountOfAPAR = New common.Controls.MyCheckBox()
        Me.chkAllowCustomerGrpDetailMandatory = New common.Controls.MyCheckBox()
        Me.txtdigits = New common.MyNumBox()
        Me.ChkAllowAutoCustCode = New common.Controls.MyCheckBox()
        Me.chkshowsaletypeinpaymenttermsReceivable = New common.Controls.MyCheckBox()
        Me.rdbtnsave = New Telerik.WinControls.UI.RadButton()
        Me.rdbtnclose = New Telerik.WinControls.UI.RadButton()
        Me.chkCustomerNameUnique = New common.Controls.MyCheckBox()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.CHKShowVisiDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDoNotMergeAccountOfAPAR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAllowCustomerGrpDetailMandatory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdigits, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkAllowAutoCustCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkshowsaletypeinpaymenttermsReceivable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCustomerNameUnique, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Size = New System.Drawing.Size(396, 208)
        Me.SplitContainer1.SplitterDistance = 176
        Me.SplitContainer1.TabIndex = 0
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.chkCustomerNameUnique)
        Me.RadGroupBox1.Controls.Add(Me.CHKShowVisiDetail)
        Me.RadGroupBox1.Controls.Add(Me.chkDoNotMergeAccountOfAPAR)
        Me.RadGroupBox1.Controls.Add(Me.chkAllowCustomerGrpDetailMandatory)
        Me.RadGroupBox1.Controls.Add(Me.txtdigits)
        Me.RadGroupBox1.Controls.Add(Me.ChkAllowAutoCustCode)
        Me.RadGroupBox1.Controls.Add(Me.chkshowsaletypeinpaymenttermsReceivable)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(396, 176)
        Me.RadGroupBox1.TabIndex = 0
        '
        'CHKShowVisiDetail
        '
        Me.CHKShowVisiDetail.Location = New System.Drawing.Point(8, 103)
        Me.CHKShowVisiDetail.MyLinkLable1 = Nothing
        Me.CHKShowVisiDetail.MyLinkLable2 = Nothing
        Me.CHKShowVisiDetail.Name = "CHKShowVisiDetail"
        Me.CHKShowVisiDetail.Size = New System.Drawing.Size(100, 18)
        Me.CHKShowVisiDetail.TabIndex = 6
        Me.CHKShowVisiDetail.Tag1 = Nothing
        Me.CHKShowVisiDetail.Text = "Show Visi Detail"
        '
        'chkDoNotMergeAccountOfAPAR
        '
        Me.chkDoNotMergeAccountOfAPAR.Location = New System.Drawing.Point(8, 79)
        Me.chkDoNotMergeAccountOfAPAR.MyLinkLable1 = Nothing
        Me.chkDoNotMergeAccountOfAPAR.MyLinkLable2 = Nothing
        Me.chkDoNotMergeAccountOfAPAR.Name = "chkDoNotMergeAccountOfAPAR"
        Me.chkDoNotMergeAccountOfAPAR.Size = New System.Drawing.Size(306, 18)
        Me.chkDoNotMergeAccountOfAPAR.TabIndex = 5
        Me.chkDoNotMergeAccountOfAPAR.Tag1 = Nothing
        Me.chkDoNotMergeAccountOfAPAR.Text = "Do Not Merge Account of AP/AR Invoice At Journal Entry"
        '
        'chkAllowCustomerGrpDetailMandatory
        '
        Me.chkAllowCustomerGrpDetailMandatory.Location = New System.Drawing.Point(8, 56)
        Me.chkAllowCustomerGrpDetailMandatory.MyLinkLable1 = Nothing
        Me.chkAllowCustomerGrpDetailMandatory.MyLinkLable2 = Nothing
        Me.chkAllowCustomerGrpDetailMandatory.Name = "chkAllowCustomerGrpDetailMandatory"
        Me.chkAllowCustomerGrpDetailMandatory.Size = New System.Drawing.Size(260, 18)
        Me.chkAllowCustomerGrpDetailMandatory.TabIndex = 4
        Me.chkAllowCustomerGrpDetailMandatory.Tag1 = Nothing
        Me.chkAllowCustomerGrpDetailMandatory.Text = "Allow Customer Group Details  to be mandatory"
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
        Me.txtdigits.Location = New System.Drawing.Point(310, 32)
        Me.txtdigits.MaxLength = 10
        Me.txtdigits.MendatroryField = False
        Me.txtdigits.MyLinkLable1 = Nothing
        Me.txtdigits.MyLinkLable2 = Nothing
        Me.txtdigits.Name = "txtdigits"
        Me.txtdigits.ReferenceFieldDesc = Nothing
        Me.txtdigits.ReferenceFieldName = Nothing
        Me.txtdigits.ReferenceTableName = Nothing
        Me.txtdigits.Size = New System.Drawing.Size(53, 20)
        Me.txtdigits.TabIndex = 2
        Me.txtdigits.Text = "0"
        Me.txtdigits.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtdigits.Value = 0.0R
        '
        'ChkAllowAutoCustCode
        '
        Me.ChkAllowAutoCustCode.Location = New System.Drawing.Point(8, 32)
        Me.ChkAllowAutoCustCode.MyLinkLable1 = Nothing
        Me.ChkAllowAutoCustCode.MyLinkLable2 = Nothing
        Me.ChkAllowAutoCustCode.Name = "ChkAllowAutoCustCode"
        Me.ChkAllowAutoCustCode.Size = New System.Drawing.Size(298, 18)
        Me.ChkAllowAutoCustCode.TabIndex = 1
        Me.ChkAllowAutoCustCode.Tag1 = Nothing
        Me.ChkAllowAutoCustCode.Text = "Allow to auto generate customer code with prefix digits"
        '
        'chkshowsaletypeinpaymenttermsReceivable
        '
        Me.chkshowsaletypeinpaymenttermsReceivable.Location = New System.Drawing.Point(8, 8)
        Me.chkshowsaletypeinpaymenttermsReceivable.MyLinkLable1 = Nothing
        Me.chkshowsaletypeinpaymenttermsReceivable.MyLinkLable2 = Nothing
        Me.chkshowsaletypeinpaymenttermsReceivable.Name = "chkshowsaletypeinpaymenttermsReceivable"
        Me.chkshowsaletypeinpaymenttermsReceivable.Size = New System.Drawing.Size(234, 18)
        Me.chkshowsaletypeinpaymenttermsReceivable.TabIndex = 0
        Me.chkshowsaletypeinpaymenttermsReceivable.Tag1 = Nothing
        Me.chkshowsaletypeinpaymenttermsReceivable.Text = "Allow to show Sale Type in Payment Terms"
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
        Me.rdbtnclose.Location = New System.Drawing.Point(310, 5)
        Me.rdbtnclose.Name = "rdbtnclose"
        Me.rdbtnclose.Size = New System.Drawing.Size(83, 18)
        Me.rdbtnclose.TabIndex = 1
        Me.rdbtnclose.Text = "Close"
        '
        'chkCustomerNameUnique
        '
        Me.chkCustomerNameUnique.Location = New System.Drawing.Point(8, 127)
        Me.chkCustomerNameUnique.MyLinkLable1 = Nothing
        Me.chkCustomerNameUnique.MyLinkLable2 = Nothing
        Me.chkCustomerNameUnique.Name = "chkCustomerNameUnique"
        Me.chkCustomerNameUnique.Size = New System.Drawing.Size(299, 18)
        Me.chkCustomerNameUnique.TabIndex = 7
        Me.chkCustomerNameUnique.Tag1 = Nothing
        Me.chkCustomerNameUnique.Text = "Customer Name Should be Unique on Customer Master"
        '
        'FrmReceivableSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(396, 208)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmReceivableSettings"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Receivable Settings"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.CHKShowVisiDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkDoNotMergeAccountOfAPAR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAllowCustomerGrpDetailMandatory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdigits, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkAllowAutoCustCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkshowsaletypeinpaymenttermsReceivable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCustomerNameUnique, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents rdbtnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents chkshowsaletypeinpaymenttermsReceivable As common.Controls.MyCheckBox
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents ChkAllowAutoCustCode As common.Controls.MyCheckBox
    Friend WithEvents txtdigits As common.MyNumBox
    Friend WithEvents chkAllowCustomerGrpDetailMandatory As common.Controls.MyCheckBox
    Friend WithEvents chkDoNotMergeAccountOfAPAR As common.Controls.MyCheckBox
    Friend WithEvents CHKShowVisiDetail As common.Controls.MyCheckBox
    Friend WithEvents chkCustomerNameUnique As common.Controls.MyCheckBox
End Class

