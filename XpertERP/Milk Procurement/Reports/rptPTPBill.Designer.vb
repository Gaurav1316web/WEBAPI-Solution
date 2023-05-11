<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class rptPTPBill
    Inherits XpertERPEngine.FrmMainTranScreen

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.pnlMilkReceipts = New System.Windows.Forms.Panel()
        Me.RadLabel14 = New common.Controls.MyLabel()
        Me.txtTaxText = New common.Controls.MyTextBox()
        Me.txtTaxPer = New common.MyNumBox()
        Me.lblConvRate = New common.Controls.MyLabel()
        Me.lblRoute = New common.Controls.MyTextBox()
        Me.lblMCC = New common.Controls.MyTextBox()
        Me.txtRoute = New common.UserControls.txtFinder()
        Me.txtMCC = New common.UserControls.txtFinder()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.lblPlant = New common.Controls.MyLabel()
        Me.txtShed = New common.UserControls.txtFinder()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lblShed = New common.Controls.MyTextBox()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.btnDotMatrixPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.pnlMilkReceipts.SuspendLayout()
        CType(Me.RadLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTaxText, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTaxPer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblConvRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRoute, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMCC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPlant, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblShed, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDotMatrixPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.pnlMilkReceipts)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDotMatrixPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(556, 222)
        Me.SplitContainer1.SplitterDistance = 187
        Me.SplitContainer1.TabIndex = 0
        '
        'pnlMilkReceipts
        '
        Me.pnlMilkReceipts.Controls.Add(Me.RadLabel14)
        Me.pnlMilkReceipts.Controls.Add(Me.txtTaxText)
        Me.pnlMilkReceipts.Controls.Add(Me.txtTaxPer)
        Me.pnlMilkReceipts.Controls.Add(Me.lblConvRate)
        Me.pnlMilkReceipts.Controls.Add(Me.lblRoute)
        Me.pnlMilkReceipts.Controls.Add(Me.lblMCC)
        Me.pnlMilkReceipts.Controls.Add(Me.txtRoute)
        Me.pnlMilkReceipts.Controls.Add(Me.txtMCC)
        Me.pnlMilkReceipts.Controls.Add(Me.MyLabel3)
        Me.pnlMilkReceipts.Controls.Add(Me.MyLabel5)
        Me.pnlMilkReceipts.Controls.Add(Me.MyLabel2)
        Me.pnlMilkReceipts.Controls.Add(Me.lblPlant)
        Me.pnlMilkReceipts.Controls.Add(Me.txtShed)
        Me.pnlMilkReceipts.Controls.Add(Me.txtToDate)
        Me.pnlMilkReceipts.Controls.Add(Me.MyLabel1)
        Me.pnlMilkReceipts.Controls.Add(Me.lblShed)
        Me.pnlMilkReceipts.Controls.Add(Me.txtFromDate)
        Me.pnlMilkReceipts.Location = New System.Drawing.Point(12, 12)
        Me.pnlMilkReceipts.Name = "pnlMilkReceipts"
        Me.pnlMilkReceipts.Size = New System.Drawing.Size(537, 163)
        Me.pnlMilkReceipts.TabIndex = 0
        '
        'RadLabel14
        '
        Me.RadLabel14.FieldName = Nothing
        Me.RadLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel14.Location = New System.Drawing.Point(3, 94)
        Me.RadLabel14.Name = "RadLabel14"
        Me.RadLabel14.Size = New System.Drawing.Size(50, 16)
        Me.RadLabel14.TabIndex = 15
        Me.RadLabel14.Text = "Tax Text"
        '
        'txtTaxText
        '
        Me.txtTaxText.CalculationExpression = Nothing
        Me.txtTaxText.FieldCode = Nothing
        Me.txtTaxText.FieldDesc = Nothing
        Me.txtTaxText.FieldMaxLength = 0
        Me.txtTaxText.FieldName = Nothing
        Me.txtTaxText.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTaxText.isCalculatedField = False
        Me.txtTaxText.IsSourceFromTable = False
        Me.txtTaxText.IsSourceFromValueList = False
        Me.txtTaxText.IsUnique = False
        Me.txtTaxText.Location = New System.Drawing.Point(53, 93)
        Me.txtTaxText.MaxLength = 200
        Me.txtTaxText.MendatroryField = True
        Me.txtTaxText.MyLinkLable1 = Me.RadLabel14
        Me.txtTaxText.MyLinkLable2 = Nothing
        Me.txtTaxText.Name = "txtTaxText"
        Me.txtTaxText.ReferenceFieldDesc = Nothing
        Me.txtTaxText.ReferenceFieldName = Nothing
        Me.txtTaxText.ReferenceTableName = Nothing
        Me.txtTaxText.Size = New System.Drawing.Size(454, 18)
        Me.txtTaxText.TabIndex = 4
        '
        'txtTaxPer
        '
        Me.txtTaxPer.BackColor = System.Drawing.Color.White
        Me.txtTaxPer.CalculationExpression = Nothing
        Me.txtTaxPer.DecimalPlaces = 2
        Me.txtTaxPer.FieldCode = Nothing
        Me.txtTaxPer.FieldDesc = Nothing
        Me.txtTaxPer.FieldMaxLength = 0
        Me.txtTaxPer.FieldName = Nothing
        Me.txtTaxPer.isCalculatedField = False
        Me.txtTaxPer.IsSourceFromTable = False
        Me.txtTaxPer.IsSourceFromValueList = False
        Me.txtTaxPer.IsUnique = False
        Me.txtTaxPer.Location = New System.Drawing.Point(53, 114)
        Me.txtTaxPer.MendatroryField = True
        Me.txtTaxPer.MyLinkLable1 = Nothing
        Me.txtTaxPer.MyLinkLable2 = Nothing
        Me.txtTaxPer.Name = "txtTaxPer"
        Me.txtTaxPer.ReferenceFieldDesc = Nothing
        Me.txtTaxPer.ReferenceFieldName = Nothing
        Me.txtTaxPer.ReferenceTableName = Nothing
        Me.txtTaxPer.Size = New System.Drawing.Size(198, 20)
        Me.txtTaxPer.TabIndex = 5
        Me.txtTaxPer.Text = "0"
        Me.txtTaxPer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTaxPer.Value = 0R
        '
        'lblConvRate
        '
        Me.lblConvRate.FieldName = Nothing
        Me.lblConvRate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblConvRate.Location = New System.Drawing.Point(3, 116)
        Me.lblConvRate.Name = "lblConvRate"
        Me.lblConvRate.Size = New System.Drawing.Size(38, 16)
        Me.lblConvRate.TabIndex = 16
        Me.lblConvRate.Text = "Tax %"
        '
        'lblRoute
        '
        Me.lblRoute.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.lblRoute.CalculationExpression = Nothing
        Me.lblRoute.Enabled = False
        Me.lblRoute.FieldCode = Nothing
        Me.lblRoute.FieldDesc = Nothing
        Me.lblRoute.FieldMaxLength = 0
        Me.lblRoute.FieldName = Nothing
        Me.lblRoute.isCalculatedField = False
        Me.lblRoute.IsSourceFromTable = False
        Me.lblRoute.IsSourceFromValueList = False
        Me.lblRoute.IsUnique = False
        Me.lblRoute.Location = New System.Drawing.Point(254, 70)
        Me.lblRoute.MendatroryField = False
        Me.lblRoute.MyLinkLable1 = Nothing
        Me.lblRoute.MyLinkLable2 = Nothing
        Me.lblRoute.Name = "lblRoute"
        Me.lblRoute.ReferenceFieldDesc = Nothing
        Me.lblRoute.ReferenceFieldName = Nothing
        Me.lblRoute.ReferenceTableName = Nothing
        Me.lblRoute.Size = New System.Drawing.Size(253, 20)
        Me.lblRoute.TabIndex = 6
        '
        'lblMCC
        '
        Me.lblMCC.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.lblMCC.CalculationExpression = Nothing
        Me.lblMCC.Enabled = False
        Me.lblMCC.FieldCode = Nothing
        Me.lblMCC.FieldDesc = Nothing
        Me.lblMCC.FieldMaxLength = 0
        Me.lblMCC.FieldName = Nothing
        Me.lblMCC.isCalculatedField = False
        Me.lblMCC.IsSourceFromTable = False
        Me.lblMCC.IsSourceFromValueList = False
        Me.lblMCC.IsUnique = False
        Me.lblMCC.Location = New System.Drawing.Point(254, 48)
        Me.lblMCC.MendatroryField = False
        Me.lblMCC.MyLinkLable1 = Nothing
        Me.lblMCC.MyLinkLable2 = Nothing
        Me.lblMCC.Name = "lblMCC"
        Me.lblMCC.ReferenceFieldDesc = Nothing
        Me.lblMCC.ReferenceFieldName = Nothing
        Me.lblMCC.ReferenceTableName = Nothing
        Me.lblMCC.Size = New System.Drawing.Size(253, 20)
        Me.lblMCC.TabIndex = 7
        '
        'txtRoute
        '
        Me.txtRoute.CalculationExpression = Nothing
        Me.txtRoute.FieldCode = Nothing
        Me.txtRoute.FieldDesc = Nothing
        Me.txtRoute.FieldMaxLength = 0
        Me.txtRoute.FieldName = Nothing
        Me.txtRoute.isCalculatedField = False
        Me.txtRoute.IsSourceFromTable = False
        Me.txtRoute.IsSourceFromValueList = False
        Me.txtRoute.IsUnique = False
        Me.txtRoute.Location = New System.Drawing.Point(53, 71)
        Me.txtRoute.MendatroryField = True
        Me.txtRoute.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRoute.MyLinkLable1 = Nothing
        Me.txtRoute.MyLinkLable2 = Nothing
        Me.txtRoute.MyReadOnly = False
        Me.txtRoute.MyShowMasterFormButton = False
        Me.txtRoute.Name = "txtRoute"
        Me.txtRoute.ReferenceFieldDesc = Nothing
        Me.txtRoute.ReferenceFieldName = Nothing
        Me.txtRoute.ReferenceTableName = Nothing
        Me.txtRoute.Size = New System.Drawing.Size(198, 19)
        Me.txtRoute.TabIndex = 3
        Me.txtRoute.Value = ""
        '
        'txtMCC
        '
        Me.txtMCC.CalculationExpression = Nothing
        Me.txtMCC.FieldCode = Nothing
        Me.txtMCC.FieldDesc = Nothing
        Me.txtMCC.FieldMaxLength = 0
        Me.txtMCC.FieldName = Nothing
        Me.txtMCC.isCalculatedField = False
        Me.txtMCC.IsSourceFromTable = False
        Me.txtMCC.IsSourceFromValueList = False
        Me.txtMCC.IsUnique = False
        Me.txtMCC.Location = New System.Drawing.Point(53, 49)
        Me.txtMCC.MendatroryField = True
        Me.txtMCC.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMCC.MyLinkLable1 = Nothing
        Me.txtMCC.MyLinkLable2 = Nothing
        Me.txtMCC.MyReadOnly = False
        Me.txtMCC.MyShowMasterFormButton = False
        Me.txtMCC.Name = "txtMCC"
        Me.txtMCC.ReferenceFieldDesc = Nothing
        Me.txtMCC.ReferenceFieldName = Nothing
        Me.txtMCC.ReferenceTableName = Nothing
        Me.txtMCC.Size = New System.Drawing.Size(198, 19)
        Me.txtMCC.TabIndex = 2
        Me.txtMCC.Value = ""
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel3.Location = New System.Drawing.Point(3, 71)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(36, 18)
        Me.MyLabel3.TabIndex = 14
        Me.MyLabel3.Text = "Route"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel5.Location = New System.Drawing.Point(3, 49)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(30, 18)
        Me.MyLabel5.TabIndex = 13
        Me.MyLabel5.Text = "MCC"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(140, 6)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(19, 16)
        Me.MyLabel2.TabIndex = 0
        Me.MyLabel2.Text = "To"
        '
        'lblPlant
        '
        Me.lblPlant.FieldName = Nothing
        Me.lblPlant.Location = New System.Drawing.Point(3, 27)
        Me.lblPlant.Name = "lblPlant"
        Me.lblPlant.Size = New System.Drawing.Size(31, 18)
        Me.lblPlant.TabIndex = 12
        Me.lblPlant.Text = "Shed"
        '
        'txtShed
        '
        Me.txtShed.CalculationExpression = Nothing
        Me.txtShed.FieldCode = Nothing
        Me.txtShed.FieldDesc = Nothing
        Me.txtShed.FieldMaxLength = 0
        Me.txtShed.FieldName = Nothing
        Me.txtShed.isCalculatedField = False
        Me.txtShed.IsSourceFromTable = False
        Me.txtShed.IsSourceFromValueList = False
        Me.txtShed.IsUnique = False
        Me.txtShed.Location = New System.Drawing.Point(53, 27)
        Me.txtShed.MendatroryField = True
        Me.txtShed.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShed.MyLinkLable1 = Nothing
        Me.txtShed.MyLinkLable2 = Nothing
        Me.txtShed.MyReadOnly = False
        Me.txtShed.MyShowMasterFormButton = False
        Me.txtShed.Name = "txtShed"
        Me.txtShed.ReferenceFieldDesc = Nothing
        Me.txtShed.ReferenceFieldName = Nothing
        Me.txtShed.ReferenceTableName = Nothing
        Me.txtShed.Size = New System.Drawing.Size(198, 19)
        Me.txtShed.TabIndex = 1
        Me.txtShed.Value = ""
        '
        'txtToDate
        '
        Me.txtToDate.CalculationExpression = Nothing
        Me.txtToDate.CustomFormat = "dd/MM/yyyy"
        Me.txtToDate.FieldCode = Nothing
        Me.txtToDate.FieldDesc = Nothing
        Me.txtToDate.FieldMaxLength = 0
        Me.txtToDate.FieldName = Nothing
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.isCalculatedField = False
        Me.txtToDate.IsSourceFromTable = False
        Me.txtToDate.IsSourceFromValueList = False
        Me.txtToDate.IsUnique = False
        Me.txtToDate.Location = New System.Drawing.Point(167, 4)
        Me.txtToDate.MendatroryField = False
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Me.MyLabel2
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.ReferenceFieldDesc = Nothing
        Me.txtToDate.ReferenceFieldName = Nothing
        Me.txtToDate.ReferenceTableName = Nothing
        Me.txtToDate.Size = New System.Drawing.Size(79, 20)
        Me.txtToDate.TabIndex = 9
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "10/06/2011"
        Me.txtToDate.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(3, 6)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(33, 16)
        Me.MyLabel1.TabIndex = 11
        Me.MyLabel1.Text = "From"
        '
        'lblShed
        '
        Me.lblShed.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.lblShed.CalculationExpression = Nothing
        Me.lblShed.Enabled = False
        Me.lblShed.FieldCode = Nothing
        Me.lblShed.FieldDesc = Nothing
        Me.lblShed.FieldMaxLength = 0
        Me.lblShed.FieldName = Nothing
        Me.lblShed.isCalculatedField = False
        Me.lblShed.IsSourceFromTable = False
        Me.lblShed.IsSourceFromValueList = False
        Me.lblShed.IsUnique = False
        Me.lblShed.Location = New System.Drawing.Point(254, 26)
        Me.lblShed.MendatroryField = False
        Me.lblShed.MyLinkLable1 = Nothing
        Me.lblShed.MyLinkLable2 = Nothing
        Me.lblShed.Name = "lblShed"
        Me.lblShed.ReferenceFieldDesc = Nothing
        Me.lblShed.ReferenceFieldName = Nothing
        Me.lblShed.ReferenceTableName = Nothing
        Me.lblShed.Size = New System.Drawing.Size(253, 20)
        Me.lblShed.TabIndex = 8
        '
        'txtFromDate
        '
        Me.txtFromDate.CalculationExpression = Nothing
        Me.txtFromDate.CustomFormat = "dd/MM/yyyy"
        Me.txtFromDate.FieldCode = Nothing
        Me.txtFromDate.FieldDesc = Nothing
        Me.txtFromDate.FieldMaxLength = 0
        Me.txtFromDate.FieldName = Nothing
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.isCalculatedField = False
        Me.txtFromDate.IsSourceFromTable = False
        Me.txtFromDate.IsSourceFromValueList = False
        Me.txtFromDate.IsUnique = False
        Me.txtFromDate.Location = New System.Drawing.Point(53, 4)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Me.MyLabel1
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.ReferenceFieldDesc = Nothing
        Me.txtFromDate.ReferenceFieldName = Nothing
        Me.txtFromDate.ReferenceTableName = Nothing
        Me.txtFromDate.Size = New System.Drawing.Size(79, 20)
        Me.txtFromDate.TabIndex = 0
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "10/06/2011"
        Me.txtFromDate.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'btnDotMatrixPrint
        '
        Me.btnDotMatrixPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDotMatrixPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDotMatrixPrint.Location = New System.Drawing.Point(9, 5)
        Me.btnDotMatrixPrint.Name = "btnDotMatrixPrint"
        Me.btnDotMatrixPrint.Size = New System.Drawing.Size(71, 22)
        Me.btnDotMatrixPrint.TabIndex = 48
        Me.btnDotMatrixPrint.Text = "Print"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(474, 5)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(71, 22)
        Me.btnclose.TabIndex = 44
        Me.btnclose.Text = "Close"
        '
        'rptPTPBill
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(556, 222)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "rptPTPBill"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "MCC Wise Abstract Report"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.pnlMilkReceipts.ResumeLayout(False)
        Me.pnlMilkReceipts.PerformLayout()
        CType(Me.RadLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTaxText, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTaxPer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblConvRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRoute, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMCC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPlant, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblShed, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDotMatrixPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblPlant As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtShed As common.UserControls.txtFinder
    Friend WithEvents lblShed As common.Controls.MyTextBox
    Friend WithEvents btnDotMatrixPrint As RadButton
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents lblMCC As common.Controls.MyTextBox
    Friend WithEvents txtMCC As common.UserControls.txtFinder
    Friend WithEvents pnlMilkReceipts As Panel
    Friend WithEvents lblRoute As common.Controls.MyTextBox
    Friend WithEvents txtRoute As common.UserControls.txtFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtTaxPer As common.MyNumBox
    Friend WithEvents lblConvRate As common.Controls.MyLabel
    Friend WithEvents RadLabel14 As common.Controls.MyLabel
    Friend WithEvents txtTaxText As common.Controls.MyTextBox
End Class

