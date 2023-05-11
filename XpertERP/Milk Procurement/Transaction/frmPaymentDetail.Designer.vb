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
        Me.lblBankDesc = New common.Controls.MyLabel
        Me.txtDescription = New common.Controls.MyTextBox
        Me.lbldesc = New common.Controls.MyLabel
        Me.txtPaymentMode = New common.UserControls.txtFinder
        Me.txtBankCode = New common.UserControls.txtFinder
        Me.lblpaymentcode = New common.Controls.MyLabel
        Me.lblbankcode = New common.Controls.MyLabel
        Me.btnCancel = New Telerik.WinControls.UI.RadButton
        Me.btnOk = New Telerik.WinControls.UI.RadButton
        CType(Me.lblBankDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbldesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblpaymentcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblbankcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnOk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblBankDesc
        '
        Me.lblBankDesc.AutoSize = False
        Me.lblBankDesc.BorderVisible = True
        Me.lblBankDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBankDesc.Location = New System.Drawing.Point(254, 23)
        Me.lblBankDesc.Name = "lblBankDesc"
        Me.lblBankDesc.Size = New System.Drawing.Size(324, 18)
        Me.lblBankDesc.TabIndex = 26
        Me.lblBankDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDescription
        '
        Me.txtDescription.Location = New System.Drawing.Point(92, 1)
        Me.txtDescription.MaxLength = 100
        Me.txtDescription.MendatroryField = False
        Me.txtDescription.MyLinkLable1 = Nothing
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(486, 20)
        Me.txtDescription.TabIndex = 23
        '
        'lbldesc
        '
        Me.lbldesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldesc.Location = New System.Drawing.Point(3, 3)
        Me.lbldesc.Name = "lbldesc"
        Me.lbldesc.Size = New System.Drawing.Size(63, 16)
        Me.lbldesc.TabIndex = 22
        Me.lbldesc.Text = "Description"
        '
        'txtPaymentMode
        '
        Me.txtPaymentMode.Location = New System.Drawing.Point(92, 44)
        Me.txtPaymentMode.MendatroryField = True
        Me.txtPaymentMode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPaymentMode.MyLinkLable1 = Nothing
        Me.txtPaymentMode.MyLinkLable2 = Nothing
        Me.txtPaymentMode.MyReadOnly = False
        Me.txtPaymentMode.MyShowMasterFormButton = False
        Me.txtPaymentMode.Name = "txtPaymentMode"
        Me.txtPaymentMode.Size = New System.Drawing.Size(159, 18)
        Me.txtPaymentMode.TabIndex = 28
        Me.txtPaymentMode.Value = ""
        '
        'txtBankCode
        '
        Me.txtBankCode.Location = New System.Drawing.Point(92, 23)
        Me.txtBankCode.MendatroryField = True
        Me.txtBankCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankCode.MyLinkLable1 = Nothing
        Me.txtBankCode.MyLinkLable2 = Nothing
        Me.txtBankCode.MyReadOnly = False
        Me.txtBankCode.MyShowMasterFormButton = False
        Me.txtBankCode.Name = "txtBankCode"
        Me.txtBankCode.Size = New System.Drawing.Size(159, 19)
        Me.txtBankCode.TabIndex = 25
        Me.txtBankCode.Value = ""
        '
        'lblpaymentcode
        '
        Me.lblpaymentcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpaymentcode.Location = New System.Drawing.Point(4, 42)
        Me.lblpaymentcode.Name = "lblpaymentcode"
        Me.lblpaymentcode.Size = New System.Drawing.Size(82, 16)
        Me.lblpaymentcode.TabIndex = 27
        Me.lblpaymentcode.Text = "Payment Mode"
        '
        'lblbankcode
        '
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
        'FrmPaymentDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(579, 100)
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

End Class

