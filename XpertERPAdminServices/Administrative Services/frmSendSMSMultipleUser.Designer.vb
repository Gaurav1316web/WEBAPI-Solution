<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSendSMSMultipleUser
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
        Me.lblNoOfCharater = New common.Controls.MyLabel()
        Me.MyLabel17 = New common.Controls.MyLabel()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtSMSText = New common.Controls.MyTextBox()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtMobileNo = New common.Controls.MyTextBox()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnClear = New Telerik.WinControls.UI.RadButton()
        Me.btnSendSMS = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.lblNoOfCharater, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.txtSMSText, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.txtMobileNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSendSMS, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblNoOfCharater)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel17)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClear)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSendSMS)
        Me.SplitContainer1.Size = New System.Drawing.Size(745, 500)
        Me.SplitContainer1.SplitterDistance = 466
        Me.SplitContainer1.TabIndex = 0
        '
        'lblNoOfCharater
        '
        Me.lblNoOfCharater.FieldName = Nothing
        Me.lblNoOfCharater.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNoOfCharater.ForeColor = System.Drawing.Color.Maroon
        Me.lblNoOfCharater.Location = New System.Drawing.Point(140, 438)
        Me.lblNoOfCharater.Name = "lblNoOfCharater"
        Me.lblNoOfCharater.Size = New System.Drawing.Size(13, 18)
        Me.lblNoOfCharater.TabIndex = 93
        Me.lblNoOfCharater.Text = "0"
        '
        'MyLabel17
        '
        Me.MyLabel17.FieldName = Nothing
        Me.MyLabel17.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel17.Location = New System.Drawing.Point(14, 438)
        Me.MyLabel17.Name = "MyLabel17"
        Me.MyLabel17.Size = New System.Drawing.Size(128, 18)
        Me.MyLabel17.TabIndex = 92
        Me.MyLabel17.Text = "No of SMS Characters :"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(745, 20)
        Me.RadMenu1.TabIndex = 24
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2, Me.RadMenuItem3})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Import Phone No"
        Me.RadMenuItem2.AccessibleName = "Import Phone No"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Import Phone No"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Import Blank Sheet Phone No"
        Me.RadMenuItem3.AccessibleName = "Import Blank Sheet Phone No"
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Import Blank Sheet Phone No"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.txtSMSText)
        Me.RadGroupBox2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "SMS Text"
        Me.RadGroupBox2.Location = New System.Drawing.Point(12, 143)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Size = New System.Drawing.Size(716, 289)
        Me.RadGroupBox2.TabIndex = 23
        Me.RadGroupBox2.Text = "SMS Text"
        '
        'txtSMSText
        '
        Me.txtSMSText.AcceptsReturn = True
        Me.txtSMSText.AcceptsTab = True
        Me.txtSMSText.AutoSize = False
        Me.txtSMSText.CalculationExpression = Nothing
        Me.txtSMSText.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSMSText.FieldCode = Nothing
        Me.txtSMSText.FieldDesc = Nothing
        Me.txtSMSText.FieldMaxLength = 0
        Me.txtSMSText.FieldName = Nothing
        Me.txtSMSText.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSMSText.isCalculatedField = False
        Me.txtSMSText.IsSourceFromTable = False
        Me.txtSMSText.IsSourceFromValueList = False
        Me.txtSMSText.IsUnique = False
        Me.txtSMSText.Location = New System.Drawing.Point(2, 18)
        Me.txtSMSText.MendatroryField = True
        Me.txtSMSText.Multiline = True
        Me.txtSMSText.MyLinkLable1 = Nothing
        Me.txtSMSText.MyLinkLable2 = Nothing
        Me.txtSMSText.Name = "txtSMSText"
        Me.txtSMSText.ReferenceFieldDesc = Nothing
        Me.txtSMSText.ReferenceFieldName = Nothing
        Me.txtSMSText.ReferenceTableName = Nothing
        Me.txtSMSText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtSMSText.Size = New System.Drawing.Size(712, 269)
        Me.txtSMSText.TabIndex = 7
        Me.txtSMSText.Tag = ""
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.txtMobileNo)
        Me.RadGroupBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = "Mobile  No"
        Me.RadGroupBox1.Location = New System.Drawing.Point(12, 26)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(716, 111)
        Me.RadGroupBox1.TabIndex = 22
        Me.RadGroupBox1.Text = "Mobile  No"
        '
        'txtMobileNo
        '
        Me.txtMobileNo.AcceptsReturn = True
        Me.txtMobileNo.AcceptsTab = True
        Me.txtMobileNo.AutoSize = False
        Me.txtMobileNo.CalculationExpression = Nothing
        Me.txtMobileNo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtMobileNo.FieldCode = Nothing
        Me.txtMobileNo.FieldDesc = Nothing
        Me.txtMobileNo.FieldMaxLength = 0
        Me.txtMobileNo.FieldName = Nothing
        Me.txtMobileNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMobileNo.isCalculatedField = False
        Me.txtMobileNo.IsSourceFromTable = False
        Me.txtMobileNo.IsSourceFromValueList = False
        Me.txtMobileNo.IsUnique = False
        Me.txtMobileNo.Location = New System.Drawing.Point(2, 18)
        Me.txtMobileNo.MendatroryField = True
        Me.txtMobileNo.Multiline = True
        Me.txtMobileNo.MyLinkLable1 = Nothing
        Me.txtMobileNo.MyLinkLable2 = Nothing
        Me.txtMobileNo.Name = "txtMobileNo"
        Me.txtMobileNo.ReferenceFieldDesc = Nothing
        Me.txtMobileNo.ReferenceFieldName = Nothing
        Me.txtMobileNo.ReferenceTableName = Nothing
        Me.txtMobileNo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMobileNo.Size = New System.Drawing.Size(712, 91)
        Me.txtMobileNo.TabIndex = 8
        Me.txtMobileNo.Tag = ""
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(662, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(80, 23)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "Close"
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnClear.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClear.Location = New System.Drawing.Point(89, 3)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(80, 23)
        Me.btnClear.TabIndex = 3
        Me.btnClear.Text = "Clear"
        '
        'btnSendSMS
        '
        Me.btnSendSMS.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSendSMS.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSendSMS.Location = New System.Drawing.Point(3, 3)
        Me.btnSendSMS.Name = "btnSendSMS"
        Me.btnSendSMS.Size = New System.Drawing.Size(80, 23)
        Me.btnSendSMS.TabIndex = 2
        Me.btnSendSMS.Text = "Send SMS"
        '
        'FrmSendSMSMultipleUser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(745, 500)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmSendSMSMultipleUser"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Send SMS"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.lblNoOfCharater, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.txtSMSText, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.txtMobileNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSendSMS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnClear As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSendSMS As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtSMSText As common.Controls.MyTextBox
    Friend WithEvents txtMobileNo As common.Controls.MyTextBox
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents lblNoOfCharater As common.Controls.MyLabel
    Friend WithEvents MyLabel17 As common.Controls.MyLabel
End Class

