<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmBankAdvise
    Inherits FrmMainTranScreen

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
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.lblDocNo = New common.Controls.MyLabel()
        Me.fndDocNo = New common.UserControls.txtNavigator()
        Me.lblPending = New common.usLock()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtPPArea = New System.Windows.Forms.TextBox()
        Me.txtMCC = New System.Windows.Forms.TextBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtPPToDate = New common.Controls.MyDateTimePicker()
        Me.txtPPMultDCS = New common.UserControls.txtMultiSelectFinder()
        Me.lblMCC2 = New common.Controls.MyLabel()
        Me.txtPPFromDate = New common.Controls.MyDateTimePicker()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.fndPaymentProcessNo = New common.UserControls.txtFinder()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtDocDate = New common.Controls.MyDateTimePicker()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPPToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMCC2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPPFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDocDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtRemarks)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDocNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndDocNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblPending)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndPaymentProcessNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDocDate)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Size = New System.Drawing.Size(772, 348)
        Me.SplitContainer1.SplitterDistance = 307
        Me.SplitContainer1.TabIndex = 0
        '
        'txtRemarks
        '
        Me.txtRemarks.Location = New System.Drawing.Point(104, 206)
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(415, 58)
        Me.txtRemarks.TabIndex = 308
        '
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Image = Global.XpertERPBulkProcurement.My.Resources.Resources._new
        Me.btnReset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReset.Location = New System.Drawing.Point(370, 20)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(19, 21)
        Me.btnReset.TabIndex = 305
        '
        'lblDocNo
        '
        Me.lblDocNo.FieldName = Nothing
        Me.lblDocNo.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblDocNo.Location = New System.Drawing.Point(9, 23)
        Me.lblDocNo.Name = "lblDocNo"
        Me.lblDocNo.Size = New System.Drawing.Size(88, 18)
        Me.lblDocNo.TabIndex = 263
        Me.lblDocNo.Text = "Document Code"
        '
        'fndDocNo
        '
        Me.fndDocNo.FieldName = Nothing
        Me.fndDocNo.Location = New System.Drawing.Point(104, 20)
        Me.fndDocNo.MendatroryField = False
        Me.fndDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.fndDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndDocNo.MyLinkLable1 = Me.lblDocNo
        Me.fndDocNo.MyLinkLable2 = Nothing
        Me.fndDocNo.MyMaxLength = 30
        Me.fndDocNo.MyReadOnly = False
        Me.fndDocNo.Name = "fndDocNo"
        Me.fndDocNo.Size = New System.Drawing.Size(265, 21)
        Me.fndDocNo.TabIndex = 262
        Me.fndDocNo.Value = ""
        '
        'lblPending
        '
        Me.lblPending.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lblPending.Location = New System.Drawing.Point(572, 20)
        Me.lblPending.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPending.Name = "lblPending"
        Me.lblPending.Size = New System.Drawing.Size(174, 20)
        Me.lblPending.Status = common.ERPTransactionStatus.Pending
        Me.lblPending.TabIndex = 306
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.txtPPArea)
        Me.RadGroupBox1.Controls.Add(Me.txtMCC)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel6)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel5)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel4)
        Me.RadGroupBox1.Controls.Add(Me.txtPPToDate)
        Me.RadGroupBox1.Controls.Add(Me.txtPPMultDCS)
        Me.RadGroupBox1.Controls.Add(Me.lblMCC2)
        Me.RadGroupBox1.Controls.Add(Me.txtPPFromDate)
        Me.RadGroupBox1.HeaderText = "Payment Process Details"
        Me.RadGroupBox1.Location = New System.Drawing.Point(104, 69)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(375, 131)
        Me.RadGroupBox1.TabIndex = 307
        Me.RadGroupBox1.Text = "Payment Process Details"
        '
        'txtPPArea
        '
        Me.txtPPArea.Location = New System.Drawing.Point(62, 96)
        Me.txtPPArea.Name = "txtPPArea"
        Me.txtPPArea.ReadOnly = True
        Me.txtPPArea.Size = New System.Drawing.Size(288, 20)
        Me.txtPPArea.TabIndex = 309
        '
        'txtMCC
        '
        Me.txtMCC.Location = New System.Drawing.Point(62, 51)
        Me.txtMCC.Name = "txtMCC"
        Me.txtMCC.ReadOnly = True
        Me.txtMCC.Size = New System.Drawing.Size(288, 20)
        Me.txtMCC.TabIndex = 308
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(22, 97)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(29, 18)
        Me.MyLabel1.TabIndex = 298
        Me.MyLabel1.Text = "Area"
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel6.Location = New System.Drawing.Point(22, 30)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(33, 16)
        Me.MyLabel6.TabIndex = 264
        Me.MyLabel6.Text = "From"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(164, 30)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(19, 16)
        Me.MyLabel5.TabIndex = 266
        Me.MyLabel5.Text = "To"
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(21, 76)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(27, 18)
        Me.MyLabel4.TabIndex = 294
        Me.MyLabel4.Text = "DCS"
        '
        'txtPPToDate
        '
        Me.txtPPToDate.CalculationExpression = Nothing
        Me.txtPPToDate.CustomFormat = "dd/MM/yyyy"
        Me.txtPPToDate.FieldCode = Nothing
        Me.txtPPToDate.FieldDesc = Nothing
        Me.txtPPToDate.FieldMaxLength = 0
        Me.txtPPToDate.FieldName = Nothing
        Me.txtPPToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtPPToDate.isCalculatedField = False
        Me.txtPPToDate.IsSourceFromTable = False
        Me.txtPPToDate.IsSourceFromValueList = False
        Me.txtPPToDate.IsUnique = False
        Me.txtPPToDate.Location = New System.Drawing.Point(198, 28)
        Me.txtPPToDate.MendatroryField = False
        Me.txtPPToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtPPToDate.MyLinkLable1 = Me.MyLabel5
        Me.txtPPToDate.MyLinkLable2 = Nothing
        Me.txtPPToDate.Name = "txtPPToDate"
        Me.txtPPToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtPPToDate.ReadOnly = True
        Me.txtPPToDate.ReferenceFieldDesc = Nothing
        Me.txtPPToDate.ReferenceFieldName = Nothing
        Me.txtPPToDate.ReferenceTableName = Nothing
        Me.txtPPToDate.Size = New System.Drawing.Size(84, 20)
        Me.txtPPToDate.TabIndex = 265
        Me.txtPPToDate.TabStop = False
        Me.txtPPToDate.Text = "10/06/2011"
        Me.txtPPToDate.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'txtPPMultDCS
        '
        Me.txtPPMultDCS.arrDispalyMember = Nothing
        Me.txtPPMultDCS.arrValueMember = Nothing
        Me.txtPPMultDCS.Location = New System.Drawing.Point(62, 74)
        Me.txtPPMultDCS.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPPMultDCS.MyLinkLable1 = Me.MyLabel4
        Me.txtPPMultDCS.MyLinkLable2 = Nothing
        Me.txtPPMultDCS.MyNullText = "Please Select..."
        Me.txtPPMultDCS.Name = "txtPPMultDCS"
        Me.txtPPMultDCS.Size = New System.Drawing.Size(288, 20)
        Me.txtPPMultDCS.TabIndex = 293
        '
        'lblMCC2
        '
        Me.lblMCC2.FieldName = Nothing
        Me.lblMCC2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMCC2.Location = New System.Drawing.Point(21, 53)
        Me.lblMCC2.Name = "lblMCC2"
        Me.lblMCC2.Size = New System.Drawing.Size(31, 16)
        Me.lblMCC2.TabIndex = 296
        Me.lblMCC2.Text = "BMC"
        '
        'txtPPFromDate
        '
        Me.txtPPFromDate.CalculationExpression = Nothing
        Me.txtPPFromDate.CustomFormat = "dd/MM/yyyy"
        Me.txtPPFromDate.FieldCode = Nothing
        Me.txtPPFromDate.FieldDesc = Nothing
        Me.txtPPFromDate.FieldMaxLength = 0
        Me.txtPPFromDate.FieldName = Nothing
        Me.txtPPFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtPPFromDate.isCalculatedField = False
        Me.txtPPFromDate.IsSourceFromTable = False
        Me.txtPPFromDate.IsSourceFromValueList = False
        Me.txtPPFromDate.IsUnique = False
        Me.txtPPFromDate.Location = New System.Drawing.Point(62, 28)
        Me.txtPPFromDate.MendatroryField = False
        Me.txtPPFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtPPFromDate.MyLinkLable1 = Me.MyLabel6
        Me.txtPPFromDate.MyLinkLable2 = Nothing
        Me.txtPPFromDate.Name = "txtPPFromDate"
        Me.txtPPFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtPPFromDate.ReadOnly = True
        Me.txtPPFromDate.ReferenceFieldDesc = Nothing
        Me.txtPPFromDate.ReferenceFieldName = Nothing
        Me.txtPPFromDate.ReferenceTableName = Nothing
        Me.txtPPFromDate.Size = New System.Drawing.Size(84, 20)
        Me.txtPPFromDate.TabIndex = 263
        Me.txtPPFromDate.TabStop = False
        Me.txtPPFromDate.Text = "10/06/2011"
        Me.txtPPFromDate.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblLocation.Location = New System.Drawing.Point(9, 47)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(139, 16)
        Me.lblLocation.TabIndex = 301
        Me.lblLocation.Text = "Payment Process Doc No."
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(9, 206)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(51, 16)
        Me.MyLabel2.TabIndex = 303
        Me.MyLabel2.Text = "Remarks"
        '
        'fndPaymentProcessNo
        '
        Me.fndPaymentProcessNo.CalculationExpression = Nothing
        Me.fndPaymentProcessNo.FieldCode = Nothing
        Me.fndPaymentProcessNo.FieldDesc = Nothing
        Me.fndPaymentProcessNo.FieldMaxLength = 0
        Me.fndPaymentProcessNo.FieldName = Nothing
        Me.fndPaymentProcessNo.isCalculatedField = False
        Me.fndPaymentProcessNo.IsSourceFromTable = False
        Me.fndPaymentProcessNo.IsSourceFromValueList = False
        Me.fndPaymentProcessNo.IsUnique = False
        Me.fndPaymentProcessNo.Location = New System.Drawing.Point(160, 46)
        Me.fndPaymentProcessNo.MendatroryField = True
        Me.fndPaymentProcessNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndPaymentProcessNo.MyLinkLable1 = Me.lblLocation
        Me.fndPaymentProcessNo.MyLinkLable2 = Nothing
        Me.fndPaymentProcessNo.MyReadOnly = False
        Me.fndPaymentProcessNo.MyShowMasterFormButton = False
        Me.fndPaymentProcessNo.Name = "fndPaymentProcessNo"
        Me.fndPaymentProcessNo.ReferenceFieldDesc = Nothing
        Me.fndPaymentProcessNo.ReferenceFieldName = Nothing
        Me.fndPaymentProcessNo.ReferenceTableName = Nothing
        Me.fndPaymentProcessNo.Size = New System.Drawing.Size(229, 19)
        Me.fndPaymentProcessNo.TabIndex = 300
        Me.fndPaymentProcessNo.Value = ""
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.MyLabel3.Location = New System.Drawing.Point(399, 23)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(31, 16)
        Me.MyLabel3.TabIndex = 299
        Me.MyLabel3.Text = "Date"
        '
        'txtDocDate
        '
        Me.txtDocDate.CalculationExpression = Nothing
        Me.txtDocDate.CustomFormat = "dd/MM/yyyy"
        Me.txtDocDate.FieldCode = Nothing
        Me.txtDocDate.FieldDesc = Nothing
        Me.txtDocDate.FieldMaxLength = 0
        Me.txtDocDate.FieldName = Nothing
        Me.txtDocDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDocDate.isCalculatedField = False
        Me.txtDocDate.IsSourceFromTable = False
        Me.txtDocDate.IsSourceFromValueList = False
        Me.txtDocDate.IsUnique = False
        Me.txtDocDate.Location = New System.Drawing.Point(435, 22)
        Me.txtDocDate.MendatroryField = False
        Me.txtDocDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDocDate.MyLinkLable1 = Me.MyLabel3
        Me.txtDocDate.MyLinkLable2 = Nothing
        Me.txtDocDate.Name = "txtDocDate"
        Me.txtDocDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDocDate.ReferenceFieldDesc = Nothing
        Me.txtDocDate.ReferenceFieldName = Nothing
        Me.txtDocDate.ReferenceTableName = Nothing
        Me.txtDocDate.Size = New System.Drawing.Size(84, 20)
        Me.txtDocDate.TabIndex = 298
        Me.txtDocDate.TabStop = False
        Me.txtDocDate.Text = "10/06/2011"
        Me.txtDocDate.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(9, 8)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(62, 22)
        Me.btnSave.TabIndex = 22
        Me.btnSave.Text = "Save"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(683, 8)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 22)
        Me.btnClose.TabIndex = 26
        Me.btnClose.Text = "Close"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(136, 8)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(68, 22)
        Me.btnPost.TabIndex = 24
        Me.btnPost.Text = "Post"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(72, 8)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(63, 22)
        Me.btnDelete.TabIndex = 23
        Me.btnDelete.Text = "Delete"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(205, 8)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(65, 22)
        Me.btnPrint.TabIndex = 25
        Me.btnPrint.Text = "Print"
        Me.btnPrint.Visible = False
        '
        'frmBankAdvise
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(772, 348)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmBankAdvise"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "frmBankAdvise"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPPToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMCC2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPPFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDocDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents lblDocNo As common.Controls.MyLabel
    Friend WithEvents fndDocNo As common.UserControls.txtNavigator
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtDocDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblMCC2 As common.Controls.MyLabel
    Friend WithEvents txtPPMultDCS As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents fndPaymentProcessNo As common.UserControls.txtFinder
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtPPToDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents txtPPFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents btnReset As RadButton
    Friend WithEvents lblPending As common.usLock
    Friend WithEvents RadGroupBox1 As RadGroupBox
    Friend WithEvents btnSave As RadButton
    Friend WithEvents btnClose As RadButton
    Friend WithEvents btnDelete As RadButton
    Friend WithEvents btnPrint As RadButton
    Friend WithEvents btnPost As RadButton
    Friend WithEvents txtMCC As TextBox
    Friend WithEvents txtRemarks As TextBox
    Friend WithEvents txtPPArea As TextBox
End Class
