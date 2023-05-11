<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPaymentDetail
    Inherits Telerik.WinControls.UI.RadForm

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
        Me.lblBankDesc = New common.Controls.MyLabel()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.lbldesc = New common.Controls.MyLabel()
        Me.txtPaymentMode = New common.UserControls.txtFinder()
        Me.txtBankCode = New common.UserControls.txtFinder()
        Me.lblpaymentcode = New common.Controls.MyLabel()
        Me.lblbankcode = New common.Controls.MyLabel()
        Me.btnCancel = New Telerik.WinControls.UI.RadButton()
        Me.btnOk = New Telerik.WinControls.UI.RadButton()
        Me.lblDocDate = New common.Controls.MyLabel()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        CType(Me.lblBankDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbldesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblpaymentcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblbankcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnOk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblBankDesc
        '
        Me.lblBankDesc.AutoSize = False
        Me.lblBankDesc.BorderVisible = True
        Me.lblBankDesc.FieldName = Nothing
        Me.lblBankDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBankDesc.Location = New System.Drawing.Point(254, 23)
        Me.lblBankDesc.Name = "lblBankDesc"
        Me.lblBankDesc.Size = New System.Drawing.Size(324, 18)
        Me.lblBankDesc.TabIndex = 26
        Me.lblBankDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDescription
        '
        Me.txtDescription.CalculationExpression = Nothing
        Me.txtDescription.FieldCode = Nothing
        Me.txtDescription.FieldDesc = Nothing
        Me.txtDescription.FieldMaxLength = 0
        Me.txtDescription.FieldName = Nothing
        Me.txtDescription.isCalculatedField = False
        Me.txtDescription.IsSourceFromTable = False
        Me.txtDescription.IsSourceFromValueList = False
        Me.txtDescription.IsUnique = False
        Me.txtDescription.Location = New System.Drawing.Point(92, 1)
        Me.txtDescription.MaxLength = 100
        Me.txtDescription.MendatroryField = False
        Me.txtDescription.MyLinkLable1 = Nothing
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReferenceFieldDesc = Nothing
        Me.txtDescription.ReferenceFieldName = Nothing
        Me.txtDescription.ReferenceTableName = Nothing
        Me.txtDescription.Size = New System.Drawing.Size(486, 20)
        Me.txtDescription.TabIndex = 23
        '
        'lbldesc
        '
        Me.lbldesc.FieldName = Nothing
        Me.lbldesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldesc.Location = New System.Drawing.Point(3, 3)
        Me.lbldesc.Name = "lbldesc"
        Me.lbldesc.Size = New System.Drawing.Size(63, 16)
        Me.lbldesc.TabIndex = 22
        Me.lbldesc.Text = "Description"
        '
        'txtPaymentMode
        '
        Me.txtPaymentMode.CalculationExpression = Nothing
        Me.txtPaymentMode.FieldCode = Nothing
        Me.txtPaymentMode.FieldDesc = Nothing
        Me.txtPaymentMode.FieldMaxLength = 0
        Me.txtPaymentMode.FieldName = Nothing
        Me.txtPaymentMode.isCalculatedField = False
        Me.txtPaymentMode.IsSourceFromTable = False
        Me.txtPaymentMode.IsSourceFromValueList = False
        Me.txtPaymentMode.IsUnique = False
        Me.txtPaymentMode.Location = New System.Drawing.Point(92, 44)
        Me.txtPaymentMode.MendatroryField = True
        Me.txtPaymentMode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPaymentMode.MyLinkLable1 = Nothing
        Me.txtPaymentMode.MyLinkLable2 = Nothing
        Me.txtPaymentMode.MyReadOnly = False
        Me.txtPaymentMode.MyShowMasterFormButton = False
        Me.txtPaymentMode.Name = "txtPaymentMode"
        Me.txtPaymentMode.ReferenceFieldDesc = Nothing
        Me.txtPaymentMode.ReferenceFieldName = Nothing
        Me.txtPaymentMode.ReferenceTableName = Nothing
        Me.txtPaymentMode.Size = New System.Drawing.Size(159, 18)
        Me.txtPaymentMode.TabIndex = 28
        Me.txtPaymentMode.Value = ""
        '
        'txtBankCode
        '
        Me.txtBankCode.CalculationExpression = Nothing
        Me.txtBankCode.FieldCode = Nothing
        Me.txtBankCode.FieldDesc = Nothing
        Me.txtBankCode.FieldMaxLength = 0
        Me.txtBankCode.FieldName = Nothing
        Me.txtBankCode.isCalculatedField = False
        Me.txtBankCode.IsSourceFromTable = False
        Me.txtBankCode.IsSourceFromValueList = False
        Me.txtBankCode.IsUnique = False
        Me.txtBankCode.Location = New System.Drawing.Point(92, 23)
        Me.txtBankCode.MendatroryField = True
        Me.txtBankCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankCode.MyLinkLable1 = Nothing
        Me.txtBankCode.MyLinkLable2 = Nothing
        Me.txtBankCode.MyReadOnly = False
        Me.txtBankCode.MyShowMasterFormButton = False
        Me.txtBankCode.Name = "txtBankCode"
        Me.txtBankCode.ReferenceFieldDesc = Nothing
        Me.txtBankCode.ReferenceFieldName = Nothing
        Me.txtBankCode.ReferenceTableName = Nothing
        Me.txtBankCode.Size = New System.Drawing.Size(159, 19)
        Me.txtBankCode.TabIndex = 25
        Me.txtBankCode.Value = ""
        '
        'lblpaymentcode
        '
        Me.lblpaymentcode.FieldName = Nothing
        Me.lblpaymentcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpaymentcode.Location = New System.Drawing.Point(4, 42)
        Me.lblpaymentcode.Name = "lblpaymentcode"
        Me.lblpaymentcode.Size = New System.Drawing.Size(82, 16)
        Me.lblpaymentcode.TabIndex = 27
        Me.lblpaymentcode.Text = "Payment Mode"
        '
        'lblbankcode
        '
        Me.lblbankcode.FieldName = Nothing
        Me.lblbankcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblbankcode.Location = New System.Drawing.Point(4, 23)
        Me.lblbankcode.Name = "lblbankcode"
        Me.lblbankcode.Size = New System.Drawing.Size(62, 16)
        Me.lblbankcode.TabIndex = 24
        Me.lblbankcode.Text = "Bank Code"
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnCancel.Location = New System.Drawing.Point(303, 75)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(130, 24)
        Me.btnCancel.TabIndex = 30
        Me.btnCancel.Text = "Esc : Cancel"
        '
        'btnOk
        '
        Me.btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnOk.Location = New System.Drawing.Point(167, 75)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(130, 24)
        Me.btnOk.TabIndex = 29
        Me.btnOk.Text = "F5 : OK"
        '
        'lblDocDate
        '
        Me.lblDocDate.FieldName = Nothing
        Me.lblDocDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocDate.Location = New System.Drawing.Point(255, 45)
        Me.lblDocDate.Name = "lblDocDate"
        Me.lblDocDate.Size = New System.Drawing.Size(78, 16)
        Me.lblDocDate.TabIndex = 32
        Me.lblDocDate.Text = "Payment Date"
        Me.lblDocDate.Visible = False
        '
        'txtDate
        '
        Me.txtDate.CalculationExpression = Nothing
        Me.txtDate.CustomFormat = "dd/MM/yyyy"
        Me.txtDate.FieldCode = Nothing
        Me.txtDate.FieldDesc = Nothing
        Me.txtDate.FieldMaxLength = 0
        Me.txtDate.FieldName = Nothing
        Me.txtDate.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDate.isCalculatedField = False
        Me.txtDate.IsSourceFromTable = False
        Me.txtDate.IsSourceFromValueList = False
        Me.txtDate.IsUnique = False
        Me.txtDate.Location = New System.Drawing.Point(337, 43)
        Me.txtDate.MendatroryField = True
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.lblDocDate
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(84, 20)
        Me.txtDate.TabIndex = 31
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "03/05/2011"
        Me.txtDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        Me.txtDate.Visible = False
        '
        'FrmPaymentDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(579, 100)
        Me.Controls.Add(Me.lblDocDate)
        Me.Controls.Add(Me.txtDate)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.lblBankDesc)
        Me.Controls.Add(Me.txtDescription)
        Me.Controls.Add(Me.lbldesc)
        Me.Controls.Add(Me.txtPaymentMode)
        Me.Controls.Add(Me.txtBankCode)
        Me.Controls.Add(Me.lblpaymentcode)
        Me.Controls.Add(Me.lblbankcode)
        Me.Name = "FrmPaymentDetail"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Payment Detail"
        CType(Me.lblBankDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbldesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblpaymentcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblbankcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnOk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblBankDesc As common.Controls.MyLabel
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents lbldesc As common.Controls.MyLabel
    Friend WithEvents txtPaymentMode As common.UserControls.txtFinder
    Friend WithEvents txtBankCode As common.UserControls.txtFinder
    Friend WithEvents lblpaymentcode As common.Controls.MyLabel
    Friend WithEvents lblbankcode As common.Controls.MyLabel
    Friend WithEvents btnCancel As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnOk As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblDocDate As Controls.MyLabel
    Friend WithEvents txtDate As Controls.MyDateTimePicker
End Class

