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
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
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
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSendBill, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrintBillMobUser, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(800, 179)
        Me.SplitContainer1.SplitterDistance = 139
        Me.SplitContainer1.TabIndex = 0
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
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
        Me.btnClose.Location = New System.Drawing.Point(720, 12)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 20)
        Me.btnClose.TabIndex = 9
        Me.btnClose.Text = "Close"
        '
        'frmSendBillToDCS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 179)
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
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSendBill, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrintBillMobUser, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
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
End Class
