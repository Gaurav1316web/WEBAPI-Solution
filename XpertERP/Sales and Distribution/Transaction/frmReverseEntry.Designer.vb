<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmReverseEntry
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
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.dtpReverseDate = New common.Controls.MyDateTimePicker
        Me.lblShipDate = New common.Controls.MyLabel
        Me.fndTransferNo = New common.UserControls.txtFinder
        Me.lblTransferNo = New common.Controls.MyLabel
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.dtpReverseDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblShipDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTransferNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.dtpReverseDate)
        Me.RadGroupBox1.Controls.Add(Me.fndTransferNo)
        Me.RadGroupBox1.Controls.Add(Me.lblShipDate)
        Me.RadGroupBox1.Controls.Add(Me.lblTransferNo)
        Me.RadGroupBox1.FooterImageIndex = -1
        Me.RadGroupBox1.FooterImageKey = ""
        Me.RadGroupBox1.HeaderImageIndex = -1
        Me.RadGroupBox1.HeaderImageKey = ""
        Me.RadGroupBox1.HeaderMargin = New System.Windows.Forms.Padding(0)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 2)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        '
        '
        '
        Me.RadGroupBox1.RootElement.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(448, 90)
        Me.RadGroupBox1.TabIndex = 0
        '
        'dtpReverseDate
        '
        'Me.dtpReverseDate.Culture = New System.Globalization.CultureInfo("en-IN")
        Me.dtpReverseDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpReverseDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpReverseDate.Location = New System.Drawing.Point(129, 43)
        Me.dtpReverseDate.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.dtpReverseDate.MendatroryField = False
        Me.dtpReverseDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpReverseDate.MyLinkLable1 = Me.lblShipDate
        Me.dtpReverseDate.MyLinkLable2 = Nothing
        Me.dtpReverseDate.Name = "dtpReverseDate"
        Me.dtpReverseDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpReverseDate.Size = New System.Drawing.Size(128, 18)
        Me.dtpReverseDate.TabIndex = 20
        Me.dtpReverseDate.Value = New Date(2011, 5, 18, 14, 11, 58, 609)
        '
        'lblShipDate
        '
        Me.lblShipDate.Location = New System.Drawing.Point(13, 43)
        Me.lblShipDate.Name = "lblShipDate"
        Me.lblShipDate.Size = New System.Drawing.Size(71, 18)
        Me.lblShipDate.TabIndex = 21
        Me.lblShipDate.Text = "Reverse Date"
        '
        'fndTransferNo
        '
        Me.fndTransferNo.Location = New System.Drawing.Point(129, 8)
        Me.fndTransferNo.MendatroryField = True
        Me.fndTransferNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndTransferNo.MyLinkLable1 = Me.lblTransferNo
        Me.fndTransferNo.MyLinkLable2 = Nothing
        Me.fndTransferNo.MyReadOnly = False
        Me.fndTransferNo.Name = "fndTransferNo"
        Me.fndTransferNo.Size = New System.Drawing.Size(133, 20)
        Me.fndTransferNo.TabIndex = 28
        Me.fndTransferNo.Value = ""
        '
        'lblTransferNo
        '
        Me.lblTransferNo.Location = New System.Drawing.Point(13, 10)
        Me.lblTransferNo.Name = "lblTransferNo"
        Me.lblTransferNo.Size = New System.Drawing.Size(110, 18)
        Me.lblTransferNo.TabIndex = 29
        Me.lblTransferNo.Text = "Quick Settlement No"
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(9, 98)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 20
        Me.btnSave.Text = "Reverse"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(373, 98)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 21
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'FrmReverseEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(451, 127)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.RadGroupBox1)
        Me.Name = "FrmReverseEntry"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Reverse Quick Settlement"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.dtpReverseDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblShipDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTransferNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents fndTransferNo As common.UserControls.txtFinder
    Friend WithEvents lblTransferNo As common.Controls.MyLabel
    Friend WithEvents dtpReverseDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblShipDate As common.Controls.MyLabel
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
End Class

