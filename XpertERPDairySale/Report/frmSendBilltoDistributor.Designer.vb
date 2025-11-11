<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSendBilltoDistributor
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtnIceCream = New System.Windows.Forms.RadioButton()
        Me.rbtnProduct = New System.Windows.Forms.RadioButton()
        Me.rbtnMilk = New System.Windows.Forms.RadioButton()
        Me.chkResendBill = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.txtDate1 = New common.Controls.MyDateTimePicker()
        Me.txtMultDoc = New common.UserControls.txtMultiSelectFinder()
        Me.btnResetDemand = New Telerik.WinControls.UI.RadButton()
        Me.btnSendDemandBill = New Telerik.WinControls.UI.RadButton()
        Me.lblDocNo = New common.Controls.MyLabel()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtDate2 = New common.Controls.MyDateTimePicker()
        Me.txtMultInvoice = New common.UserControls.txtMultiSelectFinder()
        Me.btnResetInvoice = New Telerik.WinControls.UI.RadButton()
        Me.btnSendInvoice = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtDate3 = New common.Controls.MyDateTimePicker()
        Me.txtMultGatePass = New common.UserControls.txtMultiSelectFinder()
        Me.btnResetGatePass = New Telerik.WinControls.UI.RadButton()
        Me.btnSendGatePass = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.chkResendBill, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnResetDemand, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSendDemandBill, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnResetInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSendInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnResetGatePass, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSendGatePass, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(800, 450)
        Me.SplitContainer1.SplitterDistance = 402
        Me.SplitContainer1.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.RadGroupBox4)
        Me.Panel1.Controls.Add(Me.chkResendBill)
        Me.Panel1.Controls.Add(Me.RadGroupBox1)
        Me.Panel1.Controls.Add(Me.RadGroupBox2)
        Me.Panel1.Controls.Add(Me.RadGroupBox3)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(800, 402)
        Me.Panel1.TabIndex = 392
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.rbtnIceCream)
        Me.RadGroupBox4.Controls.Add(Me.rbtnProduct)
        Me.RadGroupBox4.Controls.Add(Me.rbtnMilk)
        Me.RadGroupBox4.HeaderText = ""
        Me.RadGroupBox4.Location = New System.Drawing.Point(14, 12)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Size = New System.Drawing.Size(344, 29)
        Me.RadGroupBox4.TabIndex = 1469
        '
        'rbtnIceCream
        '
        Me.rbtnIceCream.AutoSize = True
        Me.rbtnIceCream.Location = New System.Drawing.Point(240, 6)
        Me.rbtnIceCream.Name = "rbtnIceCream"
        Me.rbtnIceCream.Size = New System.Drawing.Size(74, 17)
        Me.rbtnIceCream.TabIndex = 2
        Me.rbtnIceCream.TabStop = True
        Me.rbtnIceCream.Text = "Ice Cream"
        Me.rbtnIceCream.UseVisualStyleBackColor = True
        '
        'rbtnProduct
        '
        Me.rbtnProduct.AutoSize = True
        Me.rbtnProduct.Location = New System.Drawing.Point(131, 6)
        Me.rbtnProduct.Name = "rbtnProduct"
        Me.rbtnProduct.Size = New System.Drawing.Size(65, 17)
        Me.rbtnProduct.TabIndex = 1
        Me.rbtnProduct.TabStop = True
        Me.rbtnProduct.Text = "Product"
        Me.rbtnProduct.UseVisualStyleBackColor = True
        '
        'rbtnMilk
        '
        Me.rbtnMilk.AutoSize = True
        Me.rbtnMilk.Checked = True
        Me.rbtnMilk.Location = New System.Drawing.Point(30, 6)
        Me.rbtnMilk.Name = "rbtnMilk"
        Me.rbtnMilk.Size = New System.Drawing.Size(47, 17)
        Me.rbtnMilk.TabIndex = 0
        Me.rbtnMilk.TabStop = True
        Me.rbtnMilk.Text = "Milk"
        Me.rbtnMilk.UseVisualStyleBackColor = True
        '
        'chkResendBill
        '
        Me.chkResendBill.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkResendBill.Location = New System.Drawing.Point(387, 21)
        Me.chkResendBill.Name = "chkResendBill"
        Me.chkResendBill.Size = New System.Drawing.Size(77, 16)
        Me.chkResendBill.TabIndex = 1468
        Me.chkResendBill.Text = "Resend Bill"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.MyLabel10)
        Me.RadGroupBox1.Controls.Add(Me.txtDate1)
        Me.RadGroupBox1.Controls.Add(Me.txtMultDoc)
        Me.RadGroupBox1.Controls.Add(Me.btnResetDemand)
        Me.RadGroupBox1.Controls.Add(Me.btnSendDemandBill)
        Me.RadGroupBox1.Controls.Add(Me.lblDocNo)
        Me.RadGroupBox1.HeaderText = "Demand Bill"
        Me.RadGroupBox1.Location = New System.Drawing.Point(14, 45)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(344, 105)
        Me.RadGroupBox1.TabIndex = 278
        Me.RadGroupBox1.Text = "Demand Bill"
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Location = New System.Drawing.Point(5, 20)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(30, 18)
        Me.MyLabel10.TabIndex = 388
        Me.MyLabel10.Text = "Date"
        '
        'txtDate1
        '
        Me.txtDate1.CalculationExpression = Nothing
        Me.txtDate1.CustomFormat = "dd/MM/yyyy"
        Me.txtDate1.FieldCode = Nothing
        Me.txtDate1.FieldDesc = Nothing
        Me.txtDate1.FieldMaxLength = 0
        Me.txtDate1.FieldName = Nothing
        Me.txtDate1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDate1.isCalculatedField = False
        Me.txtDate1.IsSourceFromTable = False
        Me.txtDate1.IsSourceFromValueList = False
        Me.txtDate1.IsUnique = False
        Me.txtDate1.Location = New System.Drawing.Point(69, 19)
        Me.txtDate1.MendatroryField = False
        Me.txtDate1.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate1.MyLinkLable1 = Nothing
        Me.txtDate1.MyLinkLable2 = Nothing
        Me.txtDate1.Name = "txtDate1"
        Me.txtDate1.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate1.ReferenceFieldDesc = Nothing
        Me.txtDate1.ReferenceFieldName = Nothing
        Me.txtDate1.ReferenceTableName = Nothing
        Me.txtDate1.Size = New System.Drawing.Size(87, 18)
        Me.txtDate1.TabIndex = 387
        Me.txtDate1.TabStop = False
        Me.txtDate1.Text = "13/06/2011"
        Me.txtDate1.Value = New Date(2011, 6, 13, 0, 0, 0, 0)
        '
        'txtMultDoc
        '
        Me.txtMultDoc.arrDispalyMember = Nothing
        Me.txtMultDoc.arrValueMember = Nothing
        Me.txtMultDoc.Location = New System.Drawing.Point(69, 42)
        Me.txtMultDoc.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMultDoc.MyLinkLable1 = Nothing
        Me.txtMultDoc.MyLinkLable2 = Nothing
        Me.txtMultDoc.MyNullText = "Please select..."
        Me.txtMultDoc.Name = "txtMultDoc"
        Me.txtMultDoc.Size = New System.Drawing.Size(261, 19)
        Me.txtMultDoc.TabIndex = 386
        '
        'btnResetDemand
        '
        Me.btnResetDemand.Location = New System.Drawing.Point(256, 67)
        Me.btnResetDemand.Name = "btnResetDemand"
        Me.btnResetDemand.Size = New System.Drawing.Size(74, 20)
        Me.btnResetDemand.TabIndex = 290
        Me.btnResetDemand.Text = "Reset"
        '
        'btnSendDemandBill
        '
        Me.btnSendDemandBill.Location = New System.Drawing.Point(69, 67)
        Me.btnSendDemandBill.Name = "btnSendDemandBill"
        Me.btnSendDemandBill.Size = New System.Drawing.Size(74, 20)
        Me.btnSendDemandBill.TabIndex = 288
        Me.btnSendDemandBill.Text = "Send PDF"
        '
        'lblDocNo
        '
        Me.lblDocNo.FieldName = Nothing
        Me.lblDocNo.Location = New System.Drawing.Point(5, 42)
        Me.lblDocNo.Name = "lblDocNo"
        Me.lblDocNo.Size = New System.Drawing.Size(58, 18)
        Me.lblDocNo.TabIndex = 263
        Me.lblDocNo.Text = "Document"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox2.Controls.Add(Me.txtDate2)
        Me.RadGroupBox2.Controls.Add(Me.txtMultInvoice)
        Me.RadGroupBox2.Controls.Add(Me.btnResetInvoice)
        Me.RadGroupBox2.Controls.Add(Me.btnSendInvoice)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel7)
        Me.RadGroupBox2.HeaderText = "Invoice Bill"
        Me.RadGroupBox2.Location = New System.Drawing.Point(14, 154)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Size = New System.Drawing.Size(344, 105)
        Me.RadGroupBox2.TabIndex = 279
        Me.RadGroupBox2.Text = "Invoice Bill"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(5, 21)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(30, 18)
        Me.MyLabel1.TabIndex = 390
        Me.MyLabel1.Text = "Date"
        '
        'txtDate2
        '
        Me.txtDate2.CalculationExpression = Nothing
        Me.txtDate2.CustomFormat = "dd/MM/yyyy"
        Me.txtDate2.FieldCode = Nothing
        Me.txtDate2.FieldDesc = Nothing
        Me.txtDate2.FieldMaxLength = 0
        Me.txtDate2.FieldName = Nothing
        Me.txtDate2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDate2.isCalculatedField = False
        Me.txtDate2.IsSourceFromTable = False
        Me.txtDate2.IsSourceFromValueList = False
        Me.txtDate2.IsUnique = False
        Me.txtDate2.Location = New System.Drawing.Point(69, 20)
        Me.txtDate2.MendatroryField = False
        Me.txtDate2.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate2.MyLinkLable1 = Nothing
        Me.txtDate2.MyLinkLable2 = Nothing
        Me.txtDate2.Name = "txtDate2"
        Me.txtDate2.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate2.ReferenceFieldDesc = Nothing
        Me.txtDate2.ReferenceFieldName = Nothing
        Me.txtDate2.ReferenceTableName = Nothing
        Me.txtDate2.Size = New System.Drawing.Size(87, 18)
        Me.txtDate2.TabIndex = 389
        Me.txtDate2.TabStop = False
        Me.txtDate2.Text = "13/06/2011"
        Me.txtDate2.Value = New Date(2011, 6, 13, 0, 0, 0, 0)
        '
        'txtMultInvoice
        '
        Me.txtMultInvoice.arrDispalyMember = Nothing
        Me.txtMultInvoice.arrValueMember = Nothing
        Me.txtMultInvoice.Location = New System.Drawing.Point(69, 42)
        Me.txtMultInvoice.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMultInvoice.MyLinkLable1 = Nothing
        Me.txtMultInvoice.MyLinkLable2 = Nothing
        Me.txtMultInvoice.MyNullText = "Please select..."
        Me.txtMultInvoice.Name = "txtMultInvoice"
        Me.txtMultInvoice.Size = New System.Drawing.Size(261, 19)
        Me.txtMultInvoice.TabIndex = 386
        '
        'btnResetInvoice
        '
        Me.btnResetInvoice.Location = New System.Drawing.Point(256, 67)
        Me.btnResetInvoice.Name = "btnResetInvoice"
        Me.btnResetInvoice.Size = New System.Drawing.Size(74, 20)
        Me.btnResetInvoice.TabIndex = 290
        Me.btnResetInvoice.Text = "Reset"
        '
        'btnSendInvoice
        '
        Me.btnSendInvoice.Location = New System.Drawing.Point(69, 67)
        Me.btnSendInvoice.Name = "btnSendInvoice"
        Me.btnSendInvoice.Size = New System.Drawing.Size(74, 20)
        Me.btnSendInvoice.TabIndex = 288
        Me.btnSendInvoice.Text = "Send Bill"
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Location = New System.Drawing.Point(5, 42)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(58, 18)
        Me.MyLabel7.TabIndex = 263
        Me.MyLabel7.Text = "Document"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.MyLabel4)
        Me.RadGroupBox3.Controls.Add(Me.txtDate3)
        Me.RadGroupBox3.Controls.Add(Me.txtMultGatePass)
        Me.RadGroupBox3.Controls.Add(Me.btnResetGatePass)
        Me.RadGroupBox3.Controls.Add(Me.btnSendGatePass)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel11)
        Me.RadGroupBox3.HeaderText = "Gatepass Bill"
        Me.RadGroupBox3.Location = New System.Drawing.Point(14, 263)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Size = New System.Drawing.Size(344, 105)
        Me.RadGroupBox3.TabIndex = 280
        Me.RadGroupBox3.Text = "Gatepass Bill"
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Location = New System.Drawing.Point(5, 21)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(30, 18)
        Me.MyLabel4.TabIndex = 390
        Me.MyLabel4.Text = "Date"
        '
        'txtDate3
        '
        Me.txtDate3.CalculationExpression = Nothing
        Me.txtDate3.CustomFormat = "dd/MM/yyyy"
        Me.txtDate3.FieldCode = Nothing
        Me.txtDate3.FieldDesc = Nothing
        Me.txtDate3.FieldMaxLength = 0
        Me.txtDate3.FieldName = Nothing
        Me.txtDate3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate3.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDate3.isCalculatedField = False
        Me.txtDate3.IsSourceFromTable = False
        Me.txtDate3.IsSourceFromValueList = False
        Me.txtDate3.IsUnique = False
        Me.txtDate3.Location = New System.Drawing.Point(69, 20)
        Me.txtDate3.MendatroryField = False
        Me.txtDate3.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate3.MyLinkLable1 = Nothing
        Me.txtDate3.MyLinkLable2 = Nothing
        Me.txtDate3.Name = "txtDate3"
        Me.txtDate3.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate3.ReferenceFieldDesc = Nothing
        Me.txtDate3.ReferenceFieldName = Nothing
        Me.txtDate3.ReferenceTableName = Nothing
        Me.txtDate3.Size = New System.Drawing.Size(87, 18)
        Me.txtDate3.TabIndex = 389
        Me.txtDate3.TabStop = False
        Me.txtDate3.Text = "13/06/2011"
        Me.txtDate3.Value = New Date(2011, 6, 13, 0, 0, 0, 0)
        '
        'txtMultGatePass
        '
        Me.txtMultGatePass.arrDispalyMember = Nothing
        Me.txtMultGatePass.arrValueMember = Nothing
        Me.txtMultGatePass.Location = New System.Drawing.Point(69, 42)
        Me.txtMultGatePass.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMultGatePass.MyLinkLable1 = Nothing
        Me.txtMultGatePass.MyLinkLable2 = Nothing
        Me.txtMultGatePass.MyNullText = "Please select..."
        Me.txtMultGatePass.Name = "txtMultGatePass"
        Me.txtMultGatePass.Size = New System.Drawing.Size(261, 19)
        Me.txtMultGatePass.TabIndex = 386
        '
        'btnResetGatePass
        '
        Me.btnResetGatePass.Location = New System.Drawing.Point(256, 67)
        Me.btnResetGatePass.Name = "btnResetGatePass"
        Me.btnResetGatePass.Size = New System.Drawing.Size(74, 20)
        Me.btnResetGatePass.TabIndex = 290
        Me.btnResetGatePass.Text = "Reset"
        '
        'btnSendGatePass
        '
        Me.btnSendGatePass.Location = New System.Drawing.Point(69, 67)
        Me.btnSendGatePass.Name = "btnSendGatePass"
        Me.btnSendGatePass.Size = New System.Drawing.Size(74, 20)
        Me.btnSendGatePass.TabIndex = 288
        Me.btnSendGatePass.Text = "Send PDF"
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Location = New System.Drawing.Point(5, 42)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(58, 18)
        Me.MyLabel11.TabIndex = 263
        Me.MyLabel11.Text = "Document"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(714, 12)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 20)
        Me.btnClose.TabIndex = 10
        Me.btnClose.Text = "Close"
        '
        'frmSendBilltoDistributor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmSendBilltoDistributor"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "frmSendBilltoDistributor"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.RadGroupBox4.PerformLayout()
        CType(Me.chkResendBill, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnResetDemand, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSendDemandBill, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnResetInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSendInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnResetGatePass, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSendGatePass, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents RadGroupBox1 As RadGroupBox
    Friend WithEvents txtMultDoc As common.UserControls.txtMultiSelectFinder
    Friend WithEvents btnResetDemand As RadButton
    Friend WithEvents btnSendDemandBill As RadButton
    Friend WithEvents lblDocNo As common.Controls.MyLabel
    Friend WithEvents btnClose As RadButton
    Friend WithEvents RadGroupBox3 As RadGroupBox
    Friend WithEvents txtMultGatePass As common.UserControls.txtMultiSelectFinder
    Friend WithEvents btnResetGatePass As RadButton
    Friend WithEvents btnSendGatePass As RadButton
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox2 As RadGroupBox
    Friend WithEvents txtMultInvoice As common.UserControls.txtMultiSelectFinder
    Friend WithEvents btnResetInvoice As RadButton
    Friend WithEvents btnSendInvoice As RadButton
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents txtDate1 As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtDate2 As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtDate3 As common.Controls.MyDateTimePicker
    Friend WithEvents chkResendBill As RadCheckBox
    Friend WithEvents RadGroupBox4 As RadGroupBox
    Friend WithEvents rbtnProduct As RadioButton
    Friend WithEvents rbtnMilk As RadioButton
    Friend WithEvents rbtnIceCream As RadioButton
End Class
