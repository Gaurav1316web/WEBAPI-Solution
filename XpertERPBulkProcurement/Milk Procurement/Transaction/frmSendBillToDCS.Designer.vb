<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmSendBillToDCS
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
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.lblMCCCode = New common.Controls.MyLabel()
        Me.fndMCCCode = New common.UserControls.txtFinder()
        Me.lblBOMStatus = New common.Controls.MyLabel()
        Me.cboShift = New common.Controls.MyComboBox()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.RadButton3 = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtMultDCS = New common.UserControls.txtMultiSelectFinder()
        Me.RadButton2 = New Telerik.WinControls.UI.RadButton()
        Me.btnSendBill = New Telerik.WinControls.UI.RadButton()
        Me.btnPrintBillMobUser = New Telerik.WinControls.UI.RadButton()
        Me.lblDocNo = New common.Controls.MyLabel()
        Me.fndPaymentProcessDocNo = New common.UserControls.txtFinder()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.txtSendBill = New System.Windows.Forms.TextBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtRemainingBill = New System.Windows.Forms.TextBox()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.lblMCCCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBOMStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboShift, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSendBill, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrintBillMobUser, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtRemainingBill)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtSendBill)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(800, 450)
        Me.SplitContainer1.SplitterDistance = 405
        Me.SplitContainer1.TabIndex = 0
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.lblMCCCode)
        Me.RadGroupBox2.Controls.Add(Me.fndMCCCode)
        Me.RadGroupBox2.Controls.Add(Me.lblBOMStatus)
        Me.RadGroupBox2.Controls.Add(Me.cboShift)
        Me.RadGroupBox2.Controls.Add(Me.RadLabel4)
        Me.RadGroupBox2.Controls.Add(Me.txtDate)
        Me.RadGroupBox2.Controls.Add(Me.RadButton3)
        Me.RadGroupBox2.HeaderText = "Generate DCS SMS"
        Me.RadGroupBox2.Location = New System.Drawing.Point(13, 136)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Size = New System.Drawing.Size(345, 71)
        Me.RadGroupBox2.TabIndex = 278
        Me.RadGroupBox2.Text = "Generate DCS SMS"
        '
        'lblMCCCode
        '
        Me.lblMCCCode.FieldName = Nothing
        Me.lblMCCCode.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblMCCCode.Location = New System.Drawing.Point(5, 38)
        Me.lblMCCCode.Name = "lblMCCCode"
        Me.lblMCCCode.Size = New System.Drawing.Size(30, 18)
        Me.lblMCCCode.TabIndex = 295
        Me.lblMCCCode.Text = "MCC"
        '
        'fndMCCCode
        '
        Me.fndMCCCode.CalculationExpression = Nothing
        Me.fndMCCCode.FieldCode = Nothing
        Me.fndMCCCode.FieldDesc = Nothing
        Me.fndMCCCode.FieldMaxLength = 0
        Me.fndMCCCode.FieldName = Nothing
        Me.fndMCCCode.isCalculatedField = False
        Me.fndMCCCode.IsSourceFromTable = False
        Me.fndMCCCode.IsSourceFromValueList = False
        Me.fndMCCCode.IsUnique = False
        Me.fndMCCCode.Location = New System.Drawing.Point(41, 38)
        Me.fndMCCCode.MendatroryField = True
        Me.fndMCCCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndMCCCode.MyLinkLable1 = Me.lblMCCCode
        Me.fndMCCCode.MyLinkLable2 = Nothing
        Me.fndMCCCode.MyReadOnly = False
        Me.fndMCCCode.MyShowMasterFormButton = False
        Me.fndMCCCode.Name = "fndMCCCode"
        Me.fndMCCCode.ReferenceFieldDesc = Nothing
        Me.fndMCCCode.ReferenceFieldName = Nothing
        Me.fndMCCCode.ReferenceTableName = Nothing
        Me.fndMCCCode.Size = New System.Drawing.Size(228, 19)
        Me.fndMCCCode.TabIndex = 294
        Me.fndMCCCode.Value = ""
        '
        'lblBOMStatus
        '
        Me.lblBOMStatus.FieldName = Nothing
        Me.lblBOMStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBOMStatus.Location = New System.Drawing.Point(130, 17)
        Me.lblBOMStatus.Name = "lblBOMStatus"
        Me.lblBOMStatus.Size = New System.Drawing.Size(29, 16)
        Me.lblBOMStatus.TabIndex = 292
        Me.lblBOMStatus.Text = "Shift"
        '
        'cboShift
        '
        Me.cboShift.AutoCompleteDisplayMember = Nothing
        Me.cboShift.AutoCompleteValueMember = Nothing
        Me.cboShift.CalculationExpression = Nothing
        Me.cboShift.DropDownAnimationEnabled = True
        Me.cboShift.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboShift.FieldCode = Nothing
        Me.cboShift.FieldDesc = Nothing
        Me.cboShift.FieldMaxLength = 0
        Me.cboShift.FieldName = Nothing
        Me.cboShift.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboShift.isCalculatedField = False
        Me.cboShift.IsSourceFromTable = False
        Me.cboShift.IsSourceFromValueList = False
        Me.cboShift.IsUnique = False
        RadListDataItem1.Text = "M"
        RadListDataItem2.Text = "E"
        Me.cboShift.Items.Add(RadListDataItem1)
        Me.cboShift.Items.Add(RadListDataItem2)
        Me.cboShift.Location = New System.Drawing.Point(164, 16)
        Me.cboShift.MendatroryField = True
        Me.cboShift.MyLinkLable1 = Me.lblBOMStatus
        Me.cboShift.MyLinkLable2 = Nothing
        Me.cboShift.Name = "cboShift"
        Me.cboShift.ReferenceFieldDesc = Nothing
        Me.cboShift.ReferenceFieldName = Nothing
        Me.cboShift.ReferenceTableName = Nothing
        Me.cboShift.Size = New System.Drawing.Size(105, 18)
        Me.cboShift.TabIndex = 291
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(5, 17)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 293
        Me.RadLabel4.Text = "Date"
        '
        'txtDate
        '
        Me.txtDate.CalculationExpression = Nothing
        Me.txtDate.CustomFormat = "dd/MM/yyyy"
        Me.txtDate.FieldCode = Nothing
        Me.txtDate.FieldDesc = Nothing
        Me.txtDate.FieldMaxLength = 0
        Me.txtDate.FieldName = Nothing
        Me.txtDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDate.isCalculatedField = False
        Me.txtDate.IsSourceFromTable = False
        Me.txtDate.IsSourceFromValueList = False
        Me.txtDate.IsUnique = False
        Me.txtDate.Location = New System.Drawing.Point(41, 16)
        Me.txtDate.MendatroryField = True
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.RadLabel4
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(83, 18)
        Me.txtDate.TabIndex = 290
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadButton3
        '
        Me.RadButton3.Location = New System.Drawing.Point(275, 15)
        Me.RadButton3.Name = "RadButton3"
        Me.RadButton3.Size = New System.Drawing.Size(55, 45)
        Me.RadButton3.TabIndex = 289
        Me.RadButton3.Text = "Generate SMS"
        Me.RadButton3.TextWrap = True
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.txtMultDCS)
        Me.RadGroupBox1.Controls.Add(Me.RadButton2)
        Me.RadGroupBox1.Controls.Add(Me.btnSendBill)
        Me.RadGroupBox1.Controls.Add(Me.btnPrintBillMobUser)
        Me.RadGroupBox1.Controls.Add(Me.lblDocNo)
        Me.RadGroupBox1.Controls.Add(Me.fndPaymentProcessDocNo)
        Me.RadGroupBox1.HeaderText = "Mobile User Bill "
        Me.RadGroupBox1.Location = New System.Drawing.Point(13, 22)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(345, 105)
        Me.RadGroupBox1.TabIndex = 277
        Me.RadGroupBox1.Text = "Mobile User Bill "
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(5, 42)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(27, 18)
        Me.MyLabel1.TabIndex = 387
        Me.MyLabel1.Text = "DCS"
        '
        'txtMultDCS
        '
        Me.txtMultDCS.arrDispalyMember = Nothing
        Me.txtMultDCS.arrValueMember = Nothing
        Me.txtMultDCS.Location = New System.Drawing.Point(69, 42)
        Me.txtMultDCS.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMultDCS.MyLinkLable1 = Nothing
        Me.txtMultDCS.MyLinkLable2 = Nothing
        Me.txtMultDCS.MyNullText = "Please select..."
        Me.txtMultDCS.Name = "txtMultDCS"
        Me.txtMultDCS.Size = New System.Drawing.Size(261, 19)
        Me.txtMultDCS.TabIndex = 386
        '
        'RadButton2
        '
        Me.RadButton2.Location = New System.Drawing.Point(256, 67)
        Me.RadButton2.Name = "RadButton2"
        Me.RadButton2.Size = New System.Drawing.Size(74, 20)
        Me.RadButton2.TabIndex = 290
        Me.RadButton2.Text = "Reset"
        '
        'btnSendBill
        '
        Me.btnSendBill.Location = New System.Drawing.Point(180, 67)
        Me.btnSendBill.Name = "btnSendBill"
        Me.btnSendBill.Size = New System.Drawing.Size(74, 20)
        Me.btnSendBill.TabIndex = 289
        Me.btnSendBill.Text = "Send Bill"
        Me.btnSendBill.Visible = False
        '
        'btnPrintBillMobUser
        '
        Me.btnPrintBillMobUser.Location = New System.Drawing.Point(73, 67)
        Me.btnPrintBillMobUser.Name = "btnPrintBillMobUser"
        Me.btnPrintBillMobUser.Size = New System.Drawing.Size(105, 20)
        Me.btnPrintBillMobUser.TabIndex = 288
        Me.btnPrintBillMobUser.Text = "Print Bill Mob. User"
        '
        'lblDocNo
        '
        Me.lblDocNo.FieldName = Nothing
        Me.lblDocNo.Location = New System.Drawing.Point(5, 21)
        Me.lblDocNo.Name = "lblDocNo"
        Me.lblDocNo.Size = New System.Drawing.Size(58, 18)
        Me.lblDocNo.TabIndex = 263
        Me.lblDocNo.Text = "Document"
        '
        'fndPaymentProcessDocNo
        '
        Me.fndPaymentProcessDocNo.CalculationExpression = Nothing
        Me.fndPaymentProcessDocNo.FieldCode = Nothing
        Me.fndPaymentProcessDocNo.FieldDesc = Nothing
        Me.fndPaymentProcessDocNo.FieldMaxLength = 0
        Me.fndPaymentProcessDocNo.FieldName = Nothing
        Me.fndPaymentProcessDocNo.isCalculatedField = False
        Me.fndPaymentProcessDocNo.IsSourceFromTable = False
        Me.fndPaymentProcessDocNo.IsSourceFromValueList = False
        Me.fndPaymentProcessDocNo.IsUnique = False
        Me.fndPaymentProcessDocNo.Location = New System.Drawing.Point(69, 20)
        Me.fndPaymentProcessDocNo.MendatroryField = True
        Me.fndPaymentProcessDocNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndPaymentProcessDocNo.MyLinkLable1 = Nothing
        Me.fndPaymentProcessDocNo.MyLinkLable2 = Nothing
        Me.fndPaymentProcessDocNo.MyReadOnly = False
        Me.fndPaymentProcessDocNo.MyShowMasterFormButton = False
        Me.fndPaymentProcessDocNo.Name = "fndPaymentProcessDocNo"
        Me.fndPaymentProcessDocNo.ReferenceFieldDesc = Nothing
        Me.fndPaymentProcessDocNo.ReferenceFieldName = Nothing
        Me.fndPaymentProcessDocNo.ReferenceTableName = Nothing
        Me.fndPaymentProcessDocNo.Size = New System.Drawing.Size(261, 19)
        Me.fndPaymentProcessDocNo.TabIndex = 276
        Me.fndPaymentProcessDocNo.Value = ""
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(720, 11)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 20)
        Me.btnClose.TabIndex = 9
        Me.btnClose.Text = "Close"
        '
        'txtSendBill
        '
        Me.txtSendBill.Location = New System.Drawing.Point(430, 41)
        Me.txtSendBill.Name = "txtSendBill"
        Me.txtSendBill.ReadOnly = True
        Me.txtSendBill.Size = New System.Drawing.Size(90, 20)
        Me.txtSendBill.TabIndex = 279
        Me.txtSendBill.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(377, 43)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(49, 18)
        Me.MyLabel2.TabIndex = 388
        Me.MyLabel2.Text = "Send Bill"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Location = New System.Drawing.Point(526, 42)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(77, 18)
        Me.MyLabel3.TabIndex = 389
        Me.MyLabel3.Text = "Remaining Bill"
        '
        'txtRemainingBill
        '
        Me.txtRemainingBill.Location = New System.Drawing.Point(609, 41)
        Me.txtRemainingBill.Name = "txtRemainingBill"
        Me.txtRemainingBill.ReadOnly = True
        Me.txtRemainingBill.Size = New System.Drawing.Size(90, 20)
        Me.txtRemainingBill.TabIndex = 390
        Me.txtRemainingBill.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'frmSendBillToDCS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmSendBillToDCS"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "frmSendBillToDCS"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.lblMCCCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBOMStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboShift, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSendBill, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrintBillMobUser, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents lblDocNo As common.Controls.MyLabel
    Friend WithEvents fndPaymentProcessDocNo As common.UserControls.txtFinder
    Friend WithEvents RadGroupBox1 As RadGroupBox
    Friend WithEvents btnPrintBillMobUser As RadButton
    Friend WithEvents btnSendBill As RadButton
    Friend WithEvents RadButton2 As RadButton
    Friend WithEvents btnClose As RadButton
    Friend WithEvents RadGroupBox2 As RadGroupBox
    Friend WithEvents RadButton3 As RadButton
    Friend WithEvents lblBOMStatus As common.Controls.MyLabel
    Friend WithEvents cboShift As common.Controls.MyComboBox
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblMCCCode As common.Controls.MyLabel
    Friend WithEvents fndMCCCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtMultDCS As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtSendBill As TextBox
    Friend WithEvents txtRemainingBill As TextBox
End Class
