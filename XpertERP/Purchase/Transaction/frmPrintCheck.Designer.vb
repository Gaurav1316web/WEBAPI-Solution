<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrintCheck
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.chkNotForHighValue = New Telerik.WinControls.UI.RadCheckBox
        Me.chkAccPayee = New Telerik.WinControls.UI.RadCheckBox
        Me.lblCheckDesc = New common.Controls.MyLabel
        Me.fndCheckCode = New common.UserControls.txtFinder
        Me.lblCheckCode1 = New common.Controls.MyLabel
        Me.txtNextCheckNumber = New common.Controls.MyTextBox
        Me.MyLabel6 = New common.Controls.MyLabel
        Me.txtBankCode = New common.Controls.MyTextBox
        Me.MyLabel5 = New common.Controls.MyLabel
        Me.txtPrintStatus = New common.Controls.MyTextBox
        Me.MyLabel4 = New common.Controls.MyLabel
        Me.txtPrintedBy = New common.Controls.MyTextBox
        Me.MyLabel3 = New common.Controls.MyLabel
        Me.dtpPrintDate = New common.Controls.MyDateTimePicker
        Me.lblpaymentdate = New common.Controls.MyLabel
        Me.txtDocCode = New common.Controls.MyTextBox
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.txtDocType = New common.Controls.MyTextBox
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.txtPrintingId = New common.Controls.MyTextBox
        Me.lbldesc = New common.Controls.MyLabel
        Me.btnPrint = New Telerik.WinControls.UI.RadButton
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.chkNotForHighValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAccPayee, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCheckDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCheckCode1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNextCheckNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBankCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPrintStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPrintedBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpPrintDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblpaymentdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDocCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDocType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPrintingId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbldesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkNotForHighValue)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkAccPayee)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCheckDesc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndCheckCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCheckCode1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtNextCheckNumber)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtBankCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtPrintStatus)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtPrintedBy)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpPrintDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblpaymentdate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDocCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDocType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtPrintingId)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lbldesc)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(343, 291)
        Me.SplitContainer1.SplitterDistance = 259
        Me.SplitContainer1.TabIndex = 0
        '
        'chkNotForHighValue
        '
        Me.chkNotForHighValue.Enabled = False
        Me.chkNotForHighValue.Location = New System.Drawing.Point(211, 229)
        Me.chkNotForHighValue.Name = "chkNotForHighValue"
        Me.chkNotForHighValue.Size = New System.Drawing.Size(116, 18)
        Me.chkNotForHighValue.TabIndex = 606
        Me.chkNotForHighValue.Text = "Not For High Value"
        '
        'chkAccPayee
        '
        Me.chkAccPayee.Enabled = False
        Me.chkAccPayee.Location = New System.Drawing.Point(134, 229)
        Me.chkAccPayee.Name = "chkAccPayee"
        Me.chkAccPayee.Size = New System.Drawing.Size(71, 18)
        Me.chkAccPayee.TabIndex = 605
        Me.chkAccPayee.Text = "A/C Payee"
        '
        'lblCheckDesc
        '
        Me.lblCheckDesc.AutoSize = False
        Me.lblCheckDesc.BorderVisible = True
        Me.lblCheckDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCheckDesc.Location = New System.Drawing.Point(135, 185)
        Me.lblCheckDesc.Name = "lblCheckDesc"
        Me.lblCheckDesc.Size = New System.Drawing.Size(180, 18)
        Me.lblCheckDesc.TabIndex = 604
        Me.lblCheckDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'fndCheckCode
        '
        Me.fndCheckCode.Location = New System.Drawing.Point(135, 164)
        Me.fndCheckCode.MendatroryField = True
        Me.fndCheckCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndCheckCode.MyLinkLable1 = Nothing
        Me.fndCheckCode.MyLinkLable2 = Nothing
        Me.fndCheckCode.MyReadOnly = False
        Me.fndCheckCode.MyShowMasterFormButton = False
        Me.fndCheckCode.Name = "fndCheckCode"
        Me.fndCheckCode.Size = New System.Drawing.Size(180, 19)
        Me.fndCheckCode.TabIndex = 7
        Me.fndCheckCode.Value = ""
        '
        'lblCheckCode1
        '
        Me.lblCheckCode1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCheckCode1.Location = New System.Drawing.Point(22, 165)
        Me.lblCheckCode1.Name = "lblCheckCode1"
        Me.lblCheckCode1.Size = New System.Drawing.Size(69, 16)
        Me.lblCheckCode1.TabIndex = 10
        Me.lblCheckCode1.Text = "Check Code"
        '
        'txtNextCheckNumber
        '
        Me.txtNextCheckNumber.Enabled = False
        Me.txtNextCheckNumber.Location = New System.Drawing.Point(135, 205)
        Me.txtNextCheckNumber.MaxLength = 100
        Me.txtNextCheckNumber.MendatroryField = False
        Me.txtNextCheckNumber.MyLinkLable1 = Nothing
        Me.txtNextCheckNumber.MyLinkLable2 = Nothing
        Me.txtNextCheckNumber.Name = "txtNextCheckNumber"
        Me.txtNextCheckNumber.Size = New System.Drawing.Size(181, 20)
        Me.txtNextCheckNumber.TabIndex = 8
        '
        'MyLabel6
        '
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(22, 207)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(108, 16)
        Me.MyLabel6.TabIndex = 9
        Me.MyLabel6.Text = "Next Check Number"
        '
        'txtBankCode
        '
        Me.txtBankCode.Enabled = False
        Me.txtBankCode.Location = New System.Drawing.Point(135, 142)
        Me.txtBankCode.MaxLength = 100
        Me.txtBankCode.MendatroryField = False
        Me.txtBankCode.MyLinkLable1 = Nothing
        Me.txtBankCode.MyLinkLable2 = Nothing
        Me.txtBankCode.Name = "txtBankCode"
        Me.txtBankCode.Size = New System.Drawing.Size(181, 20)
        Me.txtBankCode.TabIndex = 6
        '
        'MyLabel5
        '
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(22, 144)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(62, 16)
        Me.MyLabel5.TabIndex = 11
        Me.MyLabel5.Text = "Bank Code"
        '
        'txtPrintStatus
        '
        Me.txtPrintStatus.Enabled = False
        Me.txtPrintStatus.Location = New System.Drawing.Point(135, 120)
        Me.txtPrintStatus.MaxLength = 100
        Me.txtPrintStatus.MendatroryField = False
        Me.txtPrintStatus.MyLinkLable1 = Nothing
        Me.txtPrintStatus.MyLinkLable2 = Nothing
        Me.txtPrintStatus.Name = "txtPrintStatus"
        Me.txtPrintStatus.Size = New System.Drawing.Size(181, 20)
        Me.txtPrintStatus.TabIndex = 5
        '
        'MyLabel4
        '
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(22, 122)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel4.TabIndex = 12
        Me.MyLabel4.Text = "Print Status"
        '
        'txtPrintedBy
        '
        Me.txtPrintedBy.Enabled = False
        Me.txtPrintedBy.Location = New System.Drawing.Point(135, 98)
        Me.txtPrintedBy.MaxLength = 100
        Me.txtPrintedBy.MendatroryField = False
        Me.txtPrintedBy.MyLinkLable1 = Nothing
        Me.txtPrintedBy.MyLinkLable2 = Nothing
        Me.txtPrintedBy.Name = "txtPrintedBy"
        Me.txtPrintedBy.Size = New System.Drawing.Size(181, 20)
        Me.txtPrintedBy.TabIndex = 4
        '
        'MyLabel3
        '
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(22, 100)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(58, 16)
        Me.MyLabel3.TabIndex = 13
        Me.MyLabel3.Text = "Printed By"
        '
        'dtpPrintDate
        '
        Me.dtpPrintDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtpPrintDate.Enabled = False
        Me.dtpPrintDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpPrintDate.Location = New System.Drawing.Point(135, 76)
        Me.dtpPrintDate.MendatroryField = False
        Me.dtpPrintDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpPrintDate.MyLinkLable1 = Nothing
        Me.dtpPrintDate.MyLinkLable2 = Nothing
        Me.dtpPrintDate.Name = "dtpPrintDate"
        Me.dtpPrintDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpPrintDate.Size = New System.Drawing.Size(132, 20)
        Me.dtpPrintDate.TabIndex = 3
        Me.dtpPrintDate.TabStop = False
        Me.dtpPrintDate.Text = "10/06/2011 11:51 AM"
        Me.dtpPrintDate.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'lblpaymentdate
        '
        Me.lblpaymentdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpaymentdate.Location = New System.Drawing.Point(22, 78)
        Me.lblpaymentdate.Name = "lblpaymentdate"
        Me.lblpaymentdate.Size = New System.Drawing.Size(57, 16)
        Me.lblpaymentdate.TabIndex = 14
        Me.lblpaymentdate.Text = "Print Date"
        '
        'txtDocCode
        '
        Me.txtDocCode.Enabled = False
        Me.txtDocCode.Location = New System.Drawing.Point(135, 54)
        Me.txtDocCode.MaxLength = 100
        Me.txtDocCode.MendatroryField = False
        Me.txtDocCode.MyLinkLable1 = Nothing
        Me.txtDocCode.MyLinkLable2 = Nothing
        Me.txtDocCode.Name = "txtDocCode"
        Me.txtDocCode.Size = New System.Drawing.Size(181, 20)
        Me.txtDocCode.TabIndex = 2
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(22, 56)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(88, 16)
        Me.MyLabel2.TabIndex = 15
        Me.MyLabel2.Text = "Document Code"
        '
        'txtDocType
        '
        Me.txtDocType.Enabled = False
        Me.txtDocType.Location = New System.Drawing.Point(135, 32)
        Me.txtDocType.MaxLength = 100
        Me.txtDocType.MendatroryField = False
        Me.txtDocType.MyLinkLable1 = Nothing
        Me.txtDocType.MyLinkLable2 = Nothing
        Me.txtDocType.Name = "txtDocType"
        Me.txtDocType.Size = New System.Drawing.Size(181, 20)
        Me.txtDocType.TabIndex = 1
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(22, 34)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(86, 16)
        Me.MyLabel1.TabIndex = 16
        Me.MyLabel1.Text = "Document Type"
        '
        'txtPrintingId
        '
        Me.txtPrintingId.Enabled = False
        Me.txtPrintingId.Location = New System.Drawing.Point(135, 10)
        Me.txtPrintingId.MaxLength = 100
        Me.txtPrintingId.MendatroryField = False
        Me.txtPrintingId.MyLinkLable1 = Nothing
        Me.txtPrintingId.MyLinkLable2 = Nothing
        Me.txtPrintingId.Name = "txtPrintingId"
        Me.txtPrintingId.Size = New System.Drawing.Size(181, 20)
        Me.txtPrintingId.TabIndex = 0
        '
        'lbldesc
        '
        Me.lbldesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldesc.Location = New System.Drawing.Point(22, 12)
        Me.lbldesc.Name = "lbldesc"
        Me.lbldesc.Size = New System.Drawing.Size(57, 16)
        Me.lbldesc.TabIndex = 17
        Me.lbldesc.Text = "Printing Id"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(3, 5)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(66, 18)
        Me.btnPrint.TabIndex = 0
        Me.btnPrint.Text = "Print"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(274, 5)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 1
        Me.btnclose.Text = "Close"
        '
        'frmPrintCheck
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(343, 291)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmPrintCheck"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Print Check"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.chkNotForHighValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAccPayee, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCheckDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCheckCode1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNextCheckNumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBankCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPrintStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPrintedBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpPrintDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblpaymentdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDocCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDocType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPrintingId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbldesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtPrintingId As common.Controls.MyTextBox
    Friend WithEvents lbldesc As common.Controls.MyLabel
    Friend WithEvents txtDocType As common.Controls.MyTextBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtDocCode As common.Controls.MyTextBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents dtpPrintDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblpaymentdate As common.Controls.MyLabel
    Friend WithEvents txtPrintedBy As common.Controls.MyTextBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtPrintStatus As common.Controls.MyTextBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtBankCode As common.Controls.MyTextBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtNextCheckNumber As common.Controls.MyTextBox
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents lblCheckDesc As common.Controls.MyLabel
    Friend WithEvents fndCheckCode As common.UserControls.txtFinder
    Friend WithEvents lblCheckCode1 As common.Controls.MyLabel
    Friend WithEvents chkAccPayee As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkNotForHighValue As Telerik.WinControls.UI.RadCheckBox
End Class

