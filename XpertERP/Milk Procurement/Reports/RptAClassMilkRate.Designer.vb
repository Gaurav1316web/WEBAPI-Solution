<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RptAClassMilkRate
    Inherits XpertERPEngine.FrmMainTranScreen

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
        Me.txtMilkTypeCode = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.txtVendorCode = New common.UserControls.txtMultiSelectFinder()
        Me.lblvendorCode = New common.Controls.MyLabel()
        Me.txtPriceCode = New common.UserControls.txtMultiSelectFinder()
        Me.lblPriceCode = New common.Controls.MyLabel()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtnMccPriceChart = New System.Windows.Forms.RadioButton()
        Me.rbtnAclassMilkRate = New System.Windows.Forms.RadioButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.fndPriceCode = New common.UserControls.txtFinder()
        Me.fndIncentiveCode = New common.UserControls.txtFinder()
        Me.txtDock = New common.Controls.MyTextBox()
        Me.lblName = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblvendorCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPriceCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(960, 382)
        Me.SplitContainer1.SplitterDistance = 348
        Me.SplitContainer1.TabIndex = 0
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.txtMilkTypeCode)
        Me.RadGroupBox1.Controls.Add(Me.txtVendorCode)
        Me.RadGroupBox1.Controls.Add(Me.txtPriceCode)
        Me.RadGroupBox1.Controls.Add(Me.lblvendorCode)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel7)
        Me.RadGroupBox1.Controls.Add(Me.lblPriceCode)
        Me.RadGroupBox1.HeaderText = "A Class Milk Rate"
        Me.RadGroupBox1.Location = New System.Drawing.Point(10, 46)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(463, 98)
        Me.RadGroupBox1.TabIndex = 1
        Me.RadGroupBox1.Text = "A Class Milk Rate"
        '
        'txtMilkTypeCode
        '
        Me.txtMilkTypeCode.arrDispalyMember = Nothing
        Me.txtMilkTypeCode.arrValueMember = Nothing
        Me.txtMilkTypeCode.Location = New System.Drawing.Point(103, 68)
        Me.txtMilkTypeCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMilkTypeCode.MyLinkLable1 = Me.MyLabel7
        Me.txtMilkTypeCode.MyLinkLable2 = Nothing
        Me.txtMilkTypeCode.MyNullText = "All"
        Me.txtMilkTypeCode.Name = "txtMilkTypeCode"
        Me.txtMilkTypeCode.Size = New System.Drawing.Size(350, 19)
        Me.txtMilkTypeCode.TabIndex = 347
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(5, 68)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(85, 16)
        Me.MyLabel7.TabIndex = 285
        Me.MyLabel7.Text = "Milk Type Code"
        '
        'txtVendorCode
        '
        Me.txtVendorCode.arrDispalyMember = Nothing
        Me.txtVendorCode.arrValueMember = Nothing
        Me.txtVendorCode.Location = New System.Drawing.Point(103, 44)
        Me.txtVendorCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVendorCode.MyLinkLable1 = Me.lblvendorCode
        Me.txtVendorCode.MyLinkLable2 = Nothing
        Me.txtVendorCode.MyNullText = "All"
        Me.txtVendorCode.Name = "txtVendorCode"
        Me.txtVendorCode.Size = New System.Drawing.Size(350, 19)
        Me.txtVendorCode.TabIndex = 346
        '
        'lblvendorCode
        '
        Me.lblvendorCode.FieldName = Nothing
        Me.lblvendorCode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblvendorCode.Location = New System.Drawing.Point(5, 46)
        Me.lblvendorCode.Name = "lblvendorCode"
        Me.lblvendorCode.Size = New System.Drawing.Size(73, 16)
        Me.lblvendorCode.TabIndex = 287
        Me.lblvendorCode.Text = "Vendor Code"
        '
        'txtPriceCode
        '
        Me.txtPriceCode.arrDispalyMember = Nothing
        Me.txtPriceCode.arrValueMember = Nothing
        Me.txtPriceCode.Location = New System.Drawing.Point(103, 17)
        Me.txtPriceCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPriceCode.MyLinkLable1 = Me.lblPriceCode
        Me.txtPriceCode.MyLinkLable2 = Nothing
        Me.txtPriceCode.MyNullText = "All"
        Me.txtPriceCode.Name = "txtPriceCode"
        Me.txtPriceCode.Size = New System.Drawing.Size(350, 19)
        Me.txtPriceCode.TabIndex = 345
        '
        'lblPriceCode
        '
        Me.lblPriceCode.FieldName = Nothing
        Me.lblPriceCode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblPriceCode.Location = New System.Drawing.Point(5, 20)
        Me.lblPriceCode.Name = "lblPriceCode"
        Me.lblPriceCode.Size = New System.Drawing.Size(62, 16)
        Me.lblPriceCode.TabIndex = 17
        Me.lblPriceCode.Text = "Price Code"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(10, 6)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(73, 20)
        Me.btnPrint.TabIndex = 5
        Me.btnPrint.Text = "Print"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(875, 6)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(73, 20)
        Me.btnclose.TabIndex = 6
        Me.btnclose.Text = "Close"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox2.Controls.Add(Me.lblName)
        Me.RadGroupBox2.Controls.Add(Me.txtDock)
        Me.RadGroupBox2.Controls.Add(Me.fndIncentiveCode)
        Me.RadGroupBox2.Controls.Add(Me.fndPriceCode)
        Me.RadGroupBox2.HeaderText = "MCC Price Chart"
        Me.RadGroupBox2.Location = New System.Drawing.Point(483, 46)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Size = New System.Drawing.Size(466, 98)
        Me.RadGroupBox2.TabIndex = 2
        Me.RadGroupBox2.Text = "MCC Price Chart"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.rbtnMccPriceChart)
        Me.RadGroupBox3.Controls.Add(Me.rbtnAclassMilkRate)
        Me.RadGroupBox3.HeaderText = ""
        Me.RadGroupBox3.Location = New System.Drawing.Point(10, 9)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Size = New System.Drawing.Size(463, 34)
        Me.RadGroupBox3.TabIndex = 3
        '
        'rbtnMccPriceChart
        '
        Me.rbtnMccPriceChart.AutoSize = True
        Me.rbtnMccPriceChart.Location = New System.Drawing.Point(156, 9)
        Me.rbtnMccPriceChart.Name = "rbtnMccPriceChart"
        Me.rbtnMccPriceChart.Size = New System.Drawing.Size(135, 17)
        Me.rbtnMccPriceChart.TabIndex = 344
        Me.rbtnMccPriceChart.TabStop = True
        Me.rbtnMccPriceChart.Text = "MCC Wise Price Chart"
        Me.rbtnMccPriceChart.UseVisualStyleBackColor = True
        '
        'rbtnAclassMilkRate
        '
        Me.rbtnAclassMilkRate.AutoSize = True
        Me.rbtnAclassMilkRate.Location = New System.Drawing.Point(13, 9)
        Me.rbtnAclassMilkRate.Name = "rbtnAclassMilkRate"
        Me.rbtnAclassMilkRate.Size = New System.Drawing.Size(112, 17)
        Me.rbtnAclassMilkRate.TabIndex = 343
        Me.rbtnAclassMilkRate.TabStop = True
        Me.rbtnAclassMilkRate.Text = "A Class Milk Rate"
        Me.rbtnAclassMilkRate.UseVisualStyleBackColor = True
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(87, 7)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(73, 20)
        Me.btnReset.TabIndex = 7
        Me.btnReset.Text = "Reset"
        '
        'fndPriceCode
        '
        Me.fndPriceCode.CalculationExpression = Nothing
        Me.fndPriceCode.FieldCode = Nothing
        Me.fndPriceCode.FieldDesc = Nothing
        Me.fndPriceCode.FieldMaxLength = 0
        Me.fndPriceCode.FieldName = Nothing
        Me.fndPriceCode.isCalculatedField = False
        Me.fndPriceCode.IsSourceFromTable = False
        Me.fndPriceCode.IsSourceFromValueList = False
        Me.fndPriceCode.IsUnique = False
        Me.fndPriceCode.Location = New System.Drawing.Point(108, 17)
        Me.fndPriceCode.MendatroryField = True
        Me.fndPriceCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndPriceCode.MyLinkLable1 = Nothing
        Me.fndPriceCode.MyLinkLable2 = Nothing
        Me.fndPriceCode.MyReadOnly = False
        Me.fndPriceCode.MyShowMasterFormButton = False
        Me.fndPriceCode.Name = "fndPriceCode"
        Me.fndPriceCode.ReferenceFieldDesc = Nothing
        Me.fndPriceCode.ReferenceFieldName = Nothing
        Me.fndPriceCode.ReferenceTableName = Nothing
        Me.fndPriceCode.Size = New System.Drawing.Size(308, 18)
        Me.fndPriceCode.TabIndex = 76
        Me.fndPriceCode.Value = ""
        '
        'fndIncentiveCode
        '
        Me.fndIncentiveCode.CalculationExpression = Nothing
        Me.fndIncentiveCode.FieldCode = Nothing
        Me.fndIncentiveCode.FieldDesc = Nothing
        Me.fndIncentiveCode.FieldMaxLength = 0
        Me.fndIncentiveCode.FieldName = Nothing
        Me.fndIncentiveCode.isCalculatedField = False
        Me.fndIncentiveCode.IsSourceFromTable = False
        Me.fndIncentiveCode.IsSourceFromValueList = False
        Me.fndIncentiveCode.IsUnique = False
        Me.fndIncentiveCode.Location = New System.Drawing.Point(108, 46)
        Me.fndIncentiveCode.MendatroryField = True
        Me.fndIncentiveCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndIncentiveCode.MyLinkLable1 = Nothing
        Me.fndIncentiveCode.MyLinkLable2 = Nothing
        Me.fndIncentiveCode.MyReadOnly = False
        Me.fndIncentiveCode.MyShowMasterFormButton = False
        Me.fndIncentiveCode.Name = "fndIncentiveCode"
        Me.fndIncentiveCode.ReferenceFieldDesc = Nothing
        Me.fndIncentiveCode.ReferenceFieldName = Nothing
        Me.fndIncentiveCode.ReferenceTableName = Nothing
        Me.fndIncentiveCode.Size = New System.Drawing.Size(308, 18)
        Me.fndIncentiveCode.TabIndex = 77
        Me.fndIncentiveCode.Value = ""
        '
        'txtDock
        '
        Me.txtDock.CalculationExpression = Nothing
        Me.txtDock.FieldCode = Nothing
        Me.txtDock.FieldDesc = Nothing
        Me.txtDock.FieldMaxLength = 0
        Me.txtDock.FieldName = Nothing
        Me.txtDock.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDock.isCalculatedField = False
        Me.txtDock.IsSourceFromTable = False
        Me.txtDock.IsSourceFromValueList = False
        Me.txtDock.IsUnique = False
        Me.txtDock.Location = New System.Drawing.Point(108, 70)
        Me.txtDock.MaxLength = 100
        Me.txtDock.MendatroryField = False
        Me.txtDock.MyLinkLable1 = Nothing
        Me.txtDock.MyLinkLable2 = Nothing
        Me.txtDock.Name = "txtDock"
        Me.txtDock.ReferenceFieldDesc = Nothing
        Me.txtDock.ReferenceFieldName = Nothing
        Me.txtDock.ReferenceTableName = Nothing
        Me.txtDock.Size = New System.Drawing.Size(308, 18)
        Me.txtDock.TabIndex = 78
        '
        'lblName
        '
        Me.lblName.FieldName = Nothing
        Me.lblName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblName.Location = New System.Drawing.Point(5, 21)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(62, 16)
        Me.lblName.TabIndex = 79
        Me.lblName.Text = "Price Code"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(5, 46)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(82, 16)
        Me.MyLabel1.TabIndex = 80
        Me.MyLabel1.Text = "Incentive Code"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(5, 70)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(32, 16)
        Me.MyLabel2.TabIndex = 81
        Me.MyLabel2.Text = "Dock"
        '
        'RptAClassMilkRate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(960, 382)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "RptAClassMilkRate"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "RptAClassMilkRate"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblvendorCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPriceCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDock, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents lblvendorCode As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents lblPriceCode As common.Controls.MyLabel
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtMilkTypeCode As common.UserControls.txtMultiSelectFinder
    Friend WithEvents txtVendorCode As common.UserControls.txtMultiSelectFinder
    Friend WithEvents txtPriceCode As common.UserControls.txtMultiSelectFinder
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rbtnMccPriceChart As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnAclassMilkRate As System.Windows.Forms.RadioButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndIncentiveCode As common.UserControls.txtFinder
    Friend WithEvents fndPriceCode As common.UserControls.txtFinder
    Friend WithEvents txtDock As common.Controls.MyTextBox
    Friend WithEvents lblName As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
End Class

