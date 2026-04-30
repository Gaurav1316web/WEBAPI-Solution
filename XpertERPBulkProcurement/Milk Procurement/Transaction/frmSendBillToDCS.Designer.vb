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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RadButton4 = New Telerik.WinControls.UI.RadButton()
        Me.RadButton3 = New Telerik.WinControls.UI.RadButton()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.txtREILDCS = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtREILBMC = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.txtREILToDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.txtREILFromDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.RadButton5 = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkSendBillOnWA = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkInactive = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtRemainingBill = New System.Windows.Forms.TextBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtMultDCS = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.RadButton2 = New Telerik.WinControls.UI.RadButton()
        Me.txtSendBill = New System.Windows.Forms.TextBox()
        Me.btnSendBill = New Telerik.WinControls.UI.RadButton()
        Me.btnPrintBillMobUser = New Telerik.WinControls.UI.RadButton()
        Me.lblDocNo = New common.Controls.MyLabel()
        Me.fndPaymentProcessDocNo = New common.UserControls.txtFinder()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.RadButton6 = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.RadButton4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtREILToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtREILFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.chkSendBillOnWA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkInactive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSendBill, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrintBillMobUser, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton6, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBox1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(800, 323)
        Me.SplitContainer1.SplitterDistance = 283
        Me.SplitContainer1.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RadButton6)
        Me.GroupBox1.Controls.Add(Me.RadButton4)
        Me.GroupBox1.Controls.Add(Me.RadButton3)
        Me.GroupBox1.Controls.Add(Me.RadButton1)
        Me.GroupBox1.Controls.Add(Me.txtREILDCS)
        Me.GroupBox1.Controls.Add(Me.MyLabel4)
        Me.GroupBox1.Controls.Add(Me.txtREILBMC)
        Me.GroupBox1.Controls.Add(Me.MyLabel9)
        Me.GroupBox1.Controls.Add(Me.txtREILToDate)
        Me.GroupBox1.Controls.Add(Me.MyLabel10)
        Me.GroupBox1.Controls.Add(Me.txtREILFromDate)
        Me.GroupBox1.Controls.Add(Me.MyLabel11)
        Me.GroupBox1.Controls.Add(Me.RadButton5)
        Me.GroupBox1.Location = New System.Drawing.Point(18, 117)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(322, 151)
        Me.GroupBox1.TabIndex = 369
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Get Farmer Collection from REIL Server"
        '
        'RadButton4
        '
        Me.RadButton4.Location = New System.Drawing.Point(181, 94)
        Me.RadButton4.Name = "RadButton4"
        Me.RadButton4.Size = New System.Drawing.Size(88, 20)
        Me.RadButton4.TabIndex = 350
        Me.RadButton4.Text = "Farmer Sale"
        '
        'RadButton3
        '
        Me.RadButton3.Location = New System.Drawing.Point(95, 94)
        Me.RadButton3.Name = "RadButton3"
        Me.RadButton3.Size = New System.Drawing.Size(85, 20)
        Me.RadButton3.TabIndex = 350
        Me.RadButton3.Text = "Local Sale"
        '
        'RadButton1
        '
        Me.RadButton1.Location = New System.Drawing.Point(5, 94)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(89, 20)
        Me.RadButton1.TabIndex = 349
        Me.RadButton1.Text = "Milk Collection"
        '
        'txtREILDCS
        '
        Me.txtREILDCS.arrDispalyMember = Nothing
        Me.txtREILDCS.arrValueMember = Nothing
        Me.txtREILDCS.Location = New System.Drawing.Point(40, 69)
        Me.txtREILDCS.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtREILDCS.MyLinkLable1 = Nothing
        Me.txtREILDCS.MyLinkLable2 = Nothing
        Me.txtREILDCS.MyNullText = "ALL"
        Me.txtREILDCS.Name = "txtREILDCS"
        Me.txtREILDCS.Size = New System.Drawing.Size(196, 19)
        Me.txtREILDCS.TabIndex = 348
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(4, 70)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(30, 16)
        Me.MyLabel4.TabIndex = 347
        Me.MyLabel4.Text = "DCS"
        '
        'txtREILBMC
        '
        Me.txtREILBMC.arrDispalyMember = Nothing
        Me.txtREILBMC.arrValueMember = Nothing
        Me.txtREILBMC.Location = New System.Drawing.Point(40, 46)
        Me.txtREILBMC.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtREILBMC.MyLinkLable1 = Nothing
        Me.txtREILBMC.MyLinkLable2 = Nothing
        Me.txtREILBMC.MyNullText = "ALL"
        Me.txtREILBMC.Name = "txtREILBMC"
        Me.txtREILBMC.Size = New System.Drawing.Size(196, 19)
        Me.txtREILBMC.TabIndex = 346
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Location = New System.Drawing.Point(129, 24)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(17, 18)
        Me.MyLabel9.TabIndex = 330
        Me.MyLabel9.Text = "to"
        '
        'txtREILToDate
        '
        Me.txtREILToDate.CalculationExpression = Nothing
        Me.txtREILToDate.CustomFormat = "dd/MM/yyyy"
        Me.txtREILToDate.FieldCode = Nothing
        Me.txtREILToDate.FieldDesc = Nothing
        Me.txtREILToDate.FieldMaxLength = 0
        Me.txtREILToDate.FieldName = Nothing
        Me.txtREILToDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtREILToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtREILToDate.isCalculatedField = False
        Me.txtREILToDate.IsSourceFromTable = False
        Me.txtREILToDate.IsSourceFromValueList = False
        Me.txtREILToDate.IsUnique = False
        Me.txtREILToDate.Location = New System.Drawing.Point(149, 24)
        Me.txtREILToDate.MendatroryField = False
        Me.txtREILToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtREILToDate.MyLinkLable1 = Nothing
        Me.txtREILToDate.MyLinkLable2 = Nothing
        Me.txtREILToDate.Name = "txtREILToDate"
        Me.txtREILToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtREILToDate.ReferenceFieldDesc = Nothing
        Me.txtREILToDate.ReferenceFieldName = Nothing
        Me.txtREILToDate.ReferenceTableName = Nothing
        Me.txtREILToDate.Size = New System.Drawing.Size(87, 18)
        Me.txtREILToDate.TabIndex = 329
        Me.txtREILToDate.TabStop = False
        Me.txtREILToDate.Text = "13/06/2011"
        Me.txtREILToDate.Value = New Date(2011, 6, 13, 0, 0, 0, 0)
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Location = New System.Drawing.Point(4, 24)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(30, 18)
        Me.MyLabel10.TabIndex = 60
        Me.MyLabel10.Text = "Date"
        '
        'txtREILFromDate
        '
        Me.txtREILFromDate.CalculationExpression = Nothing
        Me.txtREILFromDate.CustomFormat = "dd/MM/yyyy"
        Me.txtREILFromDate.FieldCode = Nothing
        Me.txtREILFromDate.FieldDesc = Nothing
        Me.txtREILFromDate.FieldMaxLength = 0
        Me.txtREILFromDate.FieldName = Nothing
        Me.txtREILFromDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtREILFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtREILFromDate.isCalculatedField = False
        Me.txtREILFromDate.IsSourceFromTable = False
        Me.txtREILFromDate.IsSourceFromValueList = False
        Me.txtREILFromDate.IsUnique = False
        Me.txtREILFromDate.Location = New System.Drawing.Point(40, 24)
        Me.txtREILFromDate.MendatroryField = False
        Me.txtREILFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtREILFromDate.MyLinkLable1 = Nothing
        Me.txtREILFromDate.MyLinkLable2 = Nothing
        Me.txtREILFromDate.Name = "txtREILFromDate"
        Me.txtREILFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtREILFromDate.ReferenceFieldDesc = Nothing
        Me.txtREILFromDate.ReferenceFieldName = Nothing
        Me.txtREILFromDate.ReferenceTableName = Nothing
        Me.txtREILFromDate.Size = New System.Drawing.Size(87, 18)
        Me.txtREILFromDate.TabIndex = 59
        Me.txtREILFromDate.TabStop = False
        Me.txtREILFromDate.Text = "13/06/2011"
        Me.txtREILFromDate.Value = New Date(2011, 6, 13, 0, 0, 0, 0)
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel11.Location = New System.Drawing.Point(4, 47)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(32, 16)
        Me.MyLabel11.TabIndex = 46
        Me.MyLabel11.Text = "MCC"
        '
        'RadButton5
        '
        Me.RadButton5.Location = New System.Drawing.Point(242, 46)
        Me.RadButton5.Name = "RadButton5"
        Me.RadButton5.Size = New System.Drawing.Size(68, 43)
        Me.RadButton5.TabIndex = 13
        Me.RadButton5.Text = ">>"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.chkSendBillOnWA)
        Me.RadGroupBox1.Controls.Add(Me.chkInactive)
        Me.RadGroupBox1.Controls.Add(Me.txtRemainingBill)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox1.Controls.Add(Me.txtMultDCS)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.RadButton2)
        Me.RadGroupBox1.Controls.Add(Me.txtSendBill)
        Me.RadGroupBox1.Controls.Add(Me.btnSendBill)
        Me.RadGroupBox1.Controls.Add(Me.btnPrintBillMobUser)
        Me.RadGroupBox1.Controls.Add(Me.lblDocNo)
        Me.RadGroupBox1.Controls.Add(Me.fndPaymentProcessDocNo)
        Me.RadGroupBox1.HeaderText = "Mobile User Bill "
        Me.RadGroupBox1.Location = New System.Drawing.Point(13, 6)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(678, 105)
        Me.RadGroupBox1.TabIndex = 277
        Me.RadGroupBox1.Text = "Mobile User Bill "
        '
        'chkSendBillOnWA
        '
        Me.chkSendBillOnWA.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSendBillOnWA.Location = New System.Drawing.Point(345, 61)
        Me.chkSendBillOnWA.Name = "chkSendBillOnWA"
        Me.chkSendBillOnWA.Size = New System.Drawing.Size(136, 16)
        Me.chkSendBillOnWA.TabIndex = 392
        Me.chkSendBillOnWA.Text = "Send Bill on WhatsApp"
        Me.chkSendBillOnWA.Visible = False
        '
        'chkInactive
        '
        Me.chkInactive.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkInactive.Location = New System.Drawing.Point(345, 43)
        Me.chkInactive.Name = "chkInactive"
        Me.chkInactive.Size = New System.Drawing.Size(139, 16)
        Me.chkInactive.TabIndex = 391
        Me.chkInactive.Text = "Not Send To Mobile Bill"
        '
        'txtRemainingBill
        '
        Me.txtRemainingBill.Location = New System.Drawing.Point(577, 19)
        Me.txtRemainingBill.Name = "txtRemainingBill"
        Me.txtRemainingBill.ReadOnly = True
        Me.txtRemainingBill.Size = New System.Drawing.Size(90, 20)
        Me.txtRemainingBill.TabIndex = 390
        Me.txtRemainingBill.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
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
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Location = New System.Drawing.Point(494, 20)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(77, 18)
        Me.MyLabel3.TabIndex = 389
        Me.MyLabel3.Text = "Remaining Bill"
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
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(345, 21)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(49, 18)
        Me.MyLabel2.TabIndex = 388
        Me.MyLabel2.Text = "Send Bill"
        '
        'RadButton2
        '
        Me.RadButton2.Location = New System.Drawing.Point(256, 67)
        Me.RadButton2.Name = "RadButton2"
        Me.RadButton2.Size = New System.Drawing.Size(74, 20)
        Me.RadButton2.TabIndex = 290
        Me.RadButton2.Text = "Reset"
        '
        'txtSendBill
        '
        Me.txtSendBill.Location = New System.Drawing.Point(398, 19)
        Me.txtSendBill.Name = "txtSendBill"
        Me.txtSendBill.ReadOnly = True
        Me.txtSendBill.Size = New System.Drawing.Size(90, 20)
        Me.txtSendBill.TabIndex = 279
        Me.txtSendBill.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
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
        Me.btnPrintBillMobUser.Location = New System.Drawing.Point(69, 67)
        Me.btnPrintBillMobUser.Name = "btnPrintBillMobUser"
        Me.btnPrintBillMobUser.Size = New System.Drawing.Size(109, 20)
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
        Me.btnClose.Location = New System.Drawing.Point(720, 12)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 20)
        Me.btnClose.TabIndex = 9
        Me.btnClose.Text = "Close"
        '
        'RadButton6
        '
        Me.RadButton6.Location = New System.Drawing.Point(6, 120)
        Me.RadButton6.Name = "RadButton6"
        Me.RadButton6.Size = New System.Drawing.Size(89, 20)
        Me.RadButton6.TabIndex = 351
        Me.RadButton6.Text = "New Farmer"
        '
        'frmSendBillToDCS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 323)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmSendBillToDCS"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "frmSendBillToDCS"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.RadButton4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtREILToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtREILFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.chkSendBillOnWA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkInactive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSendBill, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrintBillMobUser, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton6, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtMultDCS As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtSendBill As TextBox
    Friend WithEvents txtRemainingBill As TextBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents txtREILDCS As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtREILBMC As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents txtREILToDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents txtREILFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents RadButton5 As RadButton
    Friend WithEvents RadButton4 As RadButton
    Friend WithEvents RadButton3 As RadButton
    Friend WithEvents RadButton1 As RadButton
    Friend WithEvents chkInactive As RadCheckBox
    Friend WithEvents chkSendBillOnWA As RadCheckBox
    Friend WithEvents RadButton6 As RadButton
End Class
