<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmCreateBMCDCSbyMobile
    Inherits FrmMainTranScreen
    'Inherits System.Windows.Forms.Form

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
        Me.lblDate = New common.Controls.MyLabel()
        Me.txtdate = New common.Controls.MyDateTimePicker()
        Me.btnBlankSheetImportUploder = New Telerik.WinControls.UI.RadButton()
        Me.btnBlankSheetUploder = New Telerik.WinControls.UI.RadButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBlankSheetImportUploder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBlankSheetUploder, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblDate
        '
        Me.lblDate.FieldName = Nothing
        Me.lblDate.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblDate.Location = New System.Drawing.Point(23, 32)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(30, 16)
        Me.lblDate.TabIndex = 12
        Me.lblDate.Text = "Date"
        '
        'txtdate
        '
        Me.txtdate.CalculationExpression = Nothing
        Me.txtdate.CustomFormat = "dd/MM/yyyy"
        Me.txtdate.FieldCode = Nothing
        Me.txtdate.FieldDesc = Nothing
        Me.txtdate.FieldMaxLength = 0
        Me.txtdate.FieldName = Nothing
        Me.txtdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtdate.isCalculatedField = False
        Me.txtdate.IsSourceFromTable = False
        Me.txtdate.IsSourceFromValueList = False
        Me.txtdate.IsUnique = False
        Me.txtdate.Location = New System.Drawing.Point(59, 31)
        Me.txtdate.MendatroryField = True
        Me.txtdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtdate.MyLinkLable1 = Me.lblDate
        Me.txtdate.MyLinkLable2 = Nothing
        Me.txtdate.Name = "txtdate"
        Me.txtdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtdate.ReferenceFieldDesc = Nothing
        Me.txtdate.ReferenceFieldName = Nothing
        Me.txtdate.ReferenceTableName = Nothing
        Me.txtdate.Size = New System.Drawing.Size(85, 18)
        Me.txtdate.TabIndex = 11
        Me.txtdate.TabStop = False
        Me.txtdate.Text = "16/06/2023"
        Me.txtdate.Value = New Date(2023, 6, 16, 0, 0, 0, 0)
        '
        'btnBlankSheetImportUploder
        '
        Me.btnBlankSheetImportUploder.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBlankSheetImportUploder.Location = New System.Drawing.Point(234, 29)
        Me.btnBlankSheetImportUploder.Name = "btnBlankSheetImportUploder"
        Me.btnBlankSheetImportUploder.Size = New System.Drawing.Size(78, 22)
        Me.btnBlankSheetImportUploder.TabIndex = 15
        Me.btnBlankSheetImportUploder.Text = "Import"
        '
        'btnBlankSheetUploder
        '
        Me.btnBlankSheetUploder.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBlankSheetUploder.Location = New System.Drawing.Point(154, 29)
        Me.btnBlankSheetUploder.Name = "btnBlankSheetUploder"
        Me.btnBlankSheetUploder.Size = New System.Drawing.Size(78, 22)
        Me.btnBlankSheetUploder.TabIndex = 14
        Me.btnBlankSheetUploder.Text = "Export"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RadButton1)
        Me.GroupBox1.Controls.Add(Me.lblDate)
        Me.GroupBox1.Controls.Add(Me.btnBlankSheetImportUploder)
        Me.GroupBox1.Controls.Add(Me.txtdate)
        Me.GroupBox1.Controls.Add(Me.btnBlankSheetUploder)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(800, 450)
        Me.GroupBox1.TabIndex = 16
        Me.GroupBox1.TabStop = False
        '
        'RadButton1
        '
        Me.RadButton1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton1.Location = New System.Drawing.Point(314, 29)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(78, 22)
        Me.RadButton1.TabIndex = 16
        Me.RadButton1.Text = "Close"
        '
        'FrmCreateBMCDCSbyMobile
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "FrmCreateBMCDCSbyMobile"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmCreateBMCDCSbyMobile"
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBlankSheetImportUploder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBlankSheetUploder, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lblDate As common.Controls.MyLabel
    Friend WithEvents txtdate As common.Controls.MyDateTimePicker
    Friend WithEvents btnBlankSheetImportUploder As RadButton
    Friend WithEvents btnBlankSheetUploder As RadButton
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents RadButton1 As RadButton
End Class
