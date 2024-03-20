<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmTransferIncompleteRemarks1
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmTransferIncompleteRemarks1))
        Me.fndTransferNo = New common.UserControls.txtNavigator()
        Me.lblTransferNo = New Telerik.WinControls.UI.RadLabel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rtftxtInvoiceremarks = New System.Windows.Forms.RichTextBox()
        Me.rtftxtQuickRemarks = New System.Windows.Forms.RichTextBox()
        Me.txtRouteDesc = New common.Controls.MyTextBox()
        Me.txtSalesmanDesc = New common.Controls.MyTextBox()
        Me.txtSalesManCode = New common.Controls.MyTextBox()
        Me.txtRouteNo = New common.Controls.MyTextBox()
        Me.dtptransferDate = New common.Controls.MyDateTimePicker()
        Me.lblSaleManDesc = New Telerik.WinControls.UI.RadLabel()
        Me.lblSalesmanCode = New Telerik.WinControls.UI.RadLabel()
        Me.btnreset = New Telerik.WinControls.UI.RadButton()
        Me.lblInvoiceRemarks = New Telerik.WinControls.UI.RadLabel()
        Me.lblQuickSettlementremarks = New Telerik.WinControls.UI.RadLabel()
        Me.lblRouteDesc = New Telerik.WinControls.UI.RadLabel()
        Me.lblRouteNo = New Telerik.WinControls.UI.RadLabel()
        Me.lblTransferdate = New Telerik.WinControls.UI.RadLabel()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        CType(Me.lblTransferNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.txtRouteDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSalesmanDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSalesManCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRouteNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtptransferDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSaleManDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSalesmanCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblInvoiceRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblQuickSettlementremarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRouteDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRouteNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTransferdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'fndTransferNo
        '
        Me.fndTransferNo.FieldName = Nothing
        Me.fndTransferNo.Location = New System.Drawing.Point(71, 10)
        Me.fndTransferNo.MendatroryField = False
        Me.fndTransferNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.fndTransferNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndTransferNo.MyLinkLable1 = Nothing
        Me.fndTransferNo.MyLinkLable2 = Nothing
        Me.fndTransferNo.MyMaxLength = 30
        Me.fndTransferNo.MyReadOnly = False
        Me.fndTransferNo.Name = "fndTransferNo"
        Me.fndTransferNo.Size = New System.Drawing.Size(230, 21)
        Me.fndTransferNo.TabIndex = 0
        Me.fndTransferNo.Value = ""
        '
        'lblTransferNo
        '
        Me.lblTransferNo.Location = New System.Drawing.Point(6, 10)
        Me.lblTransferNo.Name = "lblTransferNo"
        Me.lblTransferNo.Size = New System.Drawing.Size(65, 18)
        Me.lblTransferNo.TabIndex = 1
        Me.lblTransferNo.Text = "Transfer No"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.rtftxtInvoiceremarks)
        Me.RadGroupBox1.Controls.Add(Me.rtftxtQuickRemarks)
        Me.RadGroupBox1.Controls.Add(Me.txtRouteDesc)
        Me.RadGroupBox1.Controls.Add(Me.txtSalesmanDesc)
        Me.RadGroupBox1.Controls.Add(Me.txtSalesManCode)
        Me.RadGroupBox1.Controls.Add(Me.txtRouteNo)
        Me.RadGroupBox1.Controls.Add(Me.dtptransferDate)
        Me.RadGroupBox1.Controls.Add(Me.lblSaleManDesc)
        Me.RadGroupBox1.Controls.Add(Me.lblSalesmanCode)
        Me.RadGroupBox1.Controls.Add(Me.btnreset)
        Me.RadGroupBox1.Controls.Add(Me.lblInvoiceRemarks)
        Me.RadGroupBox1.Controls.Add(Me.lblQuickSettlementremarks)
        Me.RadGroupBox1.Controls.Add(Me.lblRouteDesc)
        Me.RadGroupBox1.Controls.Add(Me.lblRouteNo)
        Me.RadGroupBox1.Controls.Add(Me.lblTransferdate)
        Me.RadGroupBox1.Controls.Add(Me.lblTransferNo)
        Me.RadGroupBox1.Controls.Add(Me.fndTransferNo)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(502, 539)
        Me.RadGroupBox1.TabIndex = 0
        '
        'rtftxtInvoiceremarks
        '
        Me.rtftxtInvoiceremarks.Location = New System.Drawing.Point(13, 327)
        Me.rtftxtInvoiceremarks.Name = "rtftxtInvoiceremarks"
        Me.rtftxtInvoiceremarks.Size = New System.Drawing.Size(477, 171)
        Me.rtftxtInvoiceremarks.TabIndex = 10
        Me.rtftxtInvoiceremarks.Text = ""
        '
        'rtftxtQuickRemarks
        '
        Me.rtftxtQuickRemarks.Location = New System.Drawing.Point(13, 126)
        Me.rtftxtQuickRemarks.Name = "rtftxtQuickRemarks"
        Me.rtftxtQuickRemarks.Size = New System.Drawing.Size(477, 171)
        Me.rtftxtQuickRemarks.TabIndex = 9
        Me.rtftxtQuickRemarks.Text = ""
        '
        'txtRouteDesc
        '
        Me.txtRouteDesc.CalculationExpression = Nothing
        Me.txtRouteDesc.FieldCode = Nothing
        Me.txtRouteDesc.FieldDesc = Nothing
        Me.txtRouteDesc.FieldMaxLength = 0
        Me.txtRouteDesc.FieldName = Nothing
        Me.txtRouteDesc.isCalculatedField = False
        Me.txtRouteDesc.IsSourceFromTable = False
        Me.txtRouteDesc.IsSourceFromValueList = False
        Me.txtRouteDesc.IsUnique = False
        Me.txtRouteDesc.Location = New System.Drawing.Point(254, 41)
        Me.txtRouteDesc.MendatroryField = False
        Me.txtRouteDesc.MyLinkLable1 = Nothing
        Me.txtRouteDesc.MyLinkLable2 = Nothing
        Me.txtRouteDesc.Name = "txtRouteDesc"
        Me.txtRouteDesc.ReadOnly = True
        Me.txtRouteDesc.ReferenceFieldDesc = Nothing
        Me.txtRouteDesc.ReferenceFieldName = Nothing
        Me.txtRouteDesc.ReferenceTableName = Nothing
        Me.txtRouteDesc.Size = New System.Drawing.Size(236, 20)
        Me.txtRouteDesc.TabIndex = 4
        '
        'txtSalesmanDesc
        '
        Me.txtSalesmanDesc.CalculationExpression = Nothing
        Me.txtSalesmanDesc.FieldCode = Nothing
        Me.txtSalesmanDesc.FieldDesc = Nothing
        Me.txtSalesmanDesc.FieldMaxLength = 0
        Me.txtSalesmanDesc.FieldName = Nothing
        Me.txtSalesmanDesc.isCalculatedField = False
        Me.txtSalesmanDesc.IsSourceFromTable = False
        Me.txtSalesmanDesc.IsSourceFromValueList = False
        Me.txtSalesmanDesc.IsUnique = False
        Me.txtSalesmanDesc.Location = New System.Drawing.Point(254, 69)
        Me.txtSalesmanDesc.MendatroryField = False
        Me.txtSalesmanDesc.MyLinkLable1 = Nothing
        Me.txtSalesmanDesc.MyLinkLable2 = Nothing
        Me.txtSalesmanDesc.Name = "txtSalesmanDesc"
        Me.txtSalesmanDesc.ReadOnly = True
        Me.txtSalesmanDesc.ReferenceFieldDesc = Nothing
        Me.txtSalesmanDesc.ReferenceFieldName = Nothing
        Me.txtSalesmanDesc.ReferenceTableName = Nothing
        Me.txtSalesmanDesc.Size = New System.Drawing.Size(236, 20)
        Me.txtSalesmanDesc.TabIndex = 6
        '
        'txtSalesManCode
        '
        Me.txtSalesManCode.CalculationExpression = Nothing
        Me.txtSalesManCode.FieldCode = Nothing
        Me.txtSalesManCode.FieldDesc = Nothing
        Me.txtSalesManCode.FieldMaxLength = 0
        Me.txtSalesManCode.FieldName = Nothing
        Me.txtSalesManCode.isCalculatedField = False
        Me.txtSalesManCode.IsSourceFromTable = False
        Me.txtSalesManCode.IsSourceFromValueList = False
        Me.txtSalesManCode.IsUnique = False
        Me.txtSalesManCode.Location = New System.Drawing.Point(92, 69)
        Me.txtSalesManCode.MendatroryField = False
        Me.txtSalesManCode.MyLinkLable1 = Nothing
        Me.txtSalesManCode.MyLinkLable2 = Nothing
        Me.txtSalesManCode.Name = "txtSalesManCode"
        Me.txtSalesManCode.ReadOnly = True
        Me.txtSalesManCode.ReferenceFieldDesc = Nothing
        Me.txtSalesManCode.ReferenceFieldName = Nothing
        Me.txtSalesManCode.ReferenceTableName = Nothing
        Me.txtSalesManCode.Size = New System.Drawing.Size(87, 20)
        Me.txtSalesManCode.TabIndex = 5
        '
        'txtRouteNo
        '
        Me.txtRouteNo.CalculationExpression = Nothing
        Me.txtRouteNo.FieldCode = Nothing
        Me.txtRouteNo.FieldDesc = Nothing
        Me.txtRouteNo.FieldMaxLength = 0
        Me.txtRouteNo.FieldName = Nothing
        Me.txtRouteNo.isCalculatedField = False
        Me.txtRouteNo.IsSourceFromTable = False
        Me.txtRouteNo.IsSourceFromValueList = False
        Me.txtRouteNo.IsUnique = False
        Me.txtRouteNo.Location = New System.Drawing.Point(92, 43)
        Me.txtRouteNo.MendatroryField = False
        Me.txtRouteNo.MyLinkLable1 = Nothing
        Me.txtRouteNo.MyLinkLable2 = Nothing
        Me.txtRouteNo.Name = "txtRouteNo"
        Me.txtRouteNo.ReadOnly = True
        Me.txtRouteNo.ReferenceFieldDesc = Nothing
        Me.txtRouteNo.ReferenceFieldName = Nothing
        Me.txtRouteNo.ReferenceTableName = Nothing
        Me.txtRouteNo.Size = New System.Drawing.Size(87, 20)
        Me.txtRouteNo.TabIndex = 3
        '
        'dtptransferDate
        '
        Me.dtptransferDate.CalculationExpression = Nothing
        Me.dtptransferDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtptransferDate.Enabled = False
        Me.dtptransferDate.FieldCode = Nothing
        Me.dtptransferDate.FieldDesc = Nothing
        Me.dtptransferDate.FieldMaxLength = 0
        Me.dtptransferDate.FieldName = Nothing
        Me.dtptransferDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtptransferDate.isCalculatedField = False
        Me.dtptransferDate.IsSourceFromTable = False
        Me.dtptransferDate.IsSourceFromValueList = False
        Me.dtptransferDate.IsUnique = False
        Me.dtptransferDate.Location = New System.Drawing.Point(410, 10)
        Me.dtptransferDate.MendatroryField = False
        Me.dtptransferDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtptransferDate.MyLinkLable1 = Nothing
        Me.dtptransferDate.MyLinkLable2 = Nothing
        Me.dtptransferDate.Name = "dtptransferDate"
        Me.dtptransferDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtptransferDate.ReferenceFieldDesc = Nothing
        Me.dtptransferDate.ReferenceFieldName = Nothing
        Me.dtptransferDate.ReferenceTableName = Nothing
        Me.dtptransferDate.Size = New System.Drawing.Size(80, 20)
        Me.dtptransferDate.TabIndex = 2
        Me.dtptransferDate.TabStop = False
        Me.dtptransferDate.Text = "22/06/2012 12:00 AM"
        Me.dtptransferDate.Value = New Date(2012, 6, 22, 0, 0, 0, 0)
        '
        'lblSaleManDesc
        '
        Me.lblSaleManDesc.Location = New System.Drawing.Point(185, 71)
        Me.lblSaleManDesc.Name = "lblSaleManDesc"
        Me.lblSaleManDesc.Size = New System.Drawing.Size(63, 18)
        Me.lblSaleManDesc.TabIndex = 4
        Me.lblSaleManDesc.Text = "Description"
        '
        'lblSalesmanCode
        '
        Me.lblSalesmanCode.Location = New System.Drawing.Point(6, 69)
        Me.lblSalesmanCode.Name = "lblSalesmanCode"
        Me.lblSalesmanCode.Size = New System.Drawing.Size(83, 18)
        Me.lblSalesmanCode.TabIndex = 3
        Me.lblSalesmanCode.Text = "Salesman Code"
        '
        'btnreset
        '
        Me.btnreset.Image = CType(resources.GetObject("btnreset.Image"), System.Drawing.Image)
        Me.btnreset.Location = New System.Drawing.Point(307, 10)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(17, 21)
        Me.btnreset.TabIndex = 1
        '
        'lblInvoiceRemarks
        '
        Me.lblInvoiceRemarks.Location = New System.Drawing.Point(6, 303)
        Me.lblInvoiceRemarks.Name = "lblInvoiceRemarks"
        Me.lblInvoiceRemarks.Size = New System.Drawing.Size(87, 18)
        Me.lblInvoiceRemarks.TabIndex = 8
        Me.lblInvoiceRemarks.Text = "Invoice Remarks"
        '
        'lblQuickSettlementremarks
        '
        Me.lblQuickSettlementremarks.Location = New System.Drawing.Point(6, 102)
        Me.lblQuickSettlementremarks.Name = "lblQuickSettlementremarks"
        Me.lblQuickSettlementremarks.Size = New System.Drawing.Size(138, 18)
        Me.lblQuickSettlementremarks.TabIndex = 7
        Me.lblQuickSettlementremarks.Text = "Quick Settlement Remarks"
        '
        'lblRouteDesc
        '
        Me.lblRouteDesc.Location = New System.Drawing.Point(185, 41)
        Me.lblRouteDesc.Name = "lblRouteDesc"
        Me.lblRouteDesc.Size = New System.Drawing.Size(63, 18)
        Me.lblRouteDesc.TabIndex = 3
        Me.lblRouteDesc.Text = "Description"
        '
        'lblRouteNo
        '
        Me.lblRouteNo.Location = New System.Drawing.Point(6, 45)
        Me.lblRouteNo.Name = "lblRouteNo"
        Me.lblRouteNo.Size = New System.Drawing.Size(54, 18)
        Me.lblRouteNo.TabIndex = 2
        Me.lblRouteNo.Text = "Route No"
        '
        'lblTransferdate
        '
        Me.lblTransferdate.Location = New System.Drawing.Point(330, 10)
        Me.lblTransferdate.Name = "lblTransferdate"
        Me.lblTransferdate.Size = New System.Drawing.Size(73, 18)
        Me.lblTransferdate.TabIndex = 2
        Me.lblTransferdate.Text = "Transfer Date"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(439, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(66, 22)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(79, 3)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 22)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(3, 3)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(66, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(508, 545)
        Me.SplitContainer1.SplitterDistance = 513
        Me.SplitContainer1.TabIndex = 0
        '
        'FrmTransferIncompleteRemarks1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(508, 545)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmTransferIncompleteRemarks1"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Transfer Incomplete Remarks"
        CType(Me.lblTransferNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.txtRouteDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSalesmanDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSalesManCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRouteNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtptransferDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSaleManDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSalesmanCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblInvoiceRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblQuickSettlementremarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRouteDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRouteNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTransferdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents fndTransferNo As common.UserControls.txtNavigator
    Friend WithEvents lblTransferNo As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents lblTransferdate As Telerik.WinControls.UI.RadLabel
    Friend WithEvents lblRouteDesc As Telerik.WinControls.UI.RadLabel
    Friend WithEvents lblRouteNo As Telerik.WinControls.UI.RadLabel
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblInvoiceRemarks As Telerik.WinControls.UI.RadLabel
    Friend WithEvents lblQuickSettlementremarks As Telerik.WinControls.UI.RadLabel
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblSalesmanCode As Telerik.WinControls.UI.RadLabel
    Friend WithEvents lblSaleManDesc As Telerik.WinControls.UI.RadLabel
    Friend WithEvents txtRouteDesc As common.Controls.MyTextBox
    Friend WithEvents txtSalesmanDesc As common.Controls.MyTextBox
    Friend WithEvents txtSalesManCode As common.Controls.MyTextBox
    Friend WithEvents txtRouteNo As common.Controls.MyTextBox
    Friend WithEvents dtptransferDate As common.Controls.MyDateTimePicker
    Friend WithEvents rtftxtInvoiceremarks As System.Windows.Forms.RichTextBox
    Friend WithEvents rtftxtQuickRemarks As System.Windows.Forms.RichTextBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

