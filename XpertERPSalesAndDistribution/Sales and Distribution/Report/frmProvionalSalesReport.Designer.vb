Imports XpertERPEngine
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmProvionalSalesReport
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnprint = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.DtpTodate = New common.Controls.MyDateTimePicker()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.dtpFdate = New common.Controls.MyDateTimePicker()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.fndTransfer = New common.UserControls.txtFinder()
        Me.GroupBox1.SuspendLayout()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DtpTodate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.fndTransfer)
        Me.GroupBox1.Controls.Add(Me.btnprint)
        Me.GroupBox1.Controls.Add(Me.btnClose)
        Me.GroupBox1.Controls.Add(Me.btnReset)
        Me.GroupBox1.Controls.Add(Me.DtpTodate)
        Me.GroupBox1.Controls.Add(Me.RadLabel3)
        Me.GroupBox1.Controls.Add(Me.dtpFdate)
        Me.GroupBox1.Controls.Add(Me.RadLabel1)
        Me.GroupBox1.Controls.Add(Me.RadLabel2)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 11)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(468, 104)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'btnprint
        '
        Me.btnprint.Location = New System.Drawing.Point(15, 74)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(78, 22)
        Me.btnprint.TabIndex = 100
        Me.btnprint.Text = "Print"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(377, 74)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 101
        Me.btnClose.Text = "Close"
        '
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Image = Global.XpertERPSalesAndDistribution.My.Resources.Resources._new
        Me.btnReset.Location = New System.Drawing.Point(293, 20)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(17, 19)
        Me.btnReset.TabIndex = 41
        '
        'DtpTodate
        '
        Me.DtpTodate.CalculationExpression = Nothing
        Me.DtpTodate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.DtpTodate.FieldCode = Nothing
        Me.DtpTodate.FieldDesc = Nothing
        Me.DtpTodate.FieldMaxLength = 0
        Me.DtpTodate.FieldName = Nothing
        Me.DtpTodate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtpTodate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpTodate.isCalculatedField = False
        Me.DtpTodate.IsSourceFromTable = False
        Me.DtpTodate.IsSourceFromValueList = False
        Me.DtpTodate.IsUnique = False
        Me.DtpTodate.Location = New System.Drawing.Point(318, 50)
        Me.DtpTodate.MendatroryField = False
        Me.DtpTodate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DtpTodate.MyLinkLable1 = Nothing
        Me.DtpTodate.MyLinkLable2 = Nothing
        Me.DtpTodate.Name = "DtpTodate"
        Me.DtpTodate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DtpTodate.ReferenceFieldDesc = Nothing
        Me.DtpTodate.ReferenceFieldName = Nothing
        Me.DtpTodate.ReferenceTableName = Nothing
        Me.DtpTodate.Size = New System.Drawing.Size(128, 18)
        Me.DtpTodate.TabIndex = 99
        Me.DtpTodate.TabStop = False
        Me.DtpTodate.Text = "18/05/2011 02:11 PM"
        Me.DtpTodate.Value = New Date(2011, 5, 18, 14, 11, 58, 609)
        Me.DtpTodate.Visible = False
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(15, 22)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(66, 16)
        Me.RadLabel3.TabIndex = 39
        Me.RadLabel3.Text = "Transfer No"
        '
        'dtpFdate
        '
        Me.dtpFdate.CalculationExpression = Nothing
        Me.dtpFdate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtpFdate.FieldCode = Nothing
        Me.dtpFdate.FieldDesc = Nothing
        Me.dtpFdate.FieldMaxLength = 0
        Me.dtpFdate.FieldName = Nothing
        Me.dtpFdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFdate.isCalculatedField = False
        Me.dtpFdate.IsSourceFromTable = False
        Me.dtpFdate.IsSourceFromValueList = False
        Me.dtpFdate.IsUnique = False
        Me.dtpFdate.Location = New System.Drawing.Point(96, 50)
        Me.dtpFdate.MendatroryField = False
        Me.dtpFdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFdate.MyLinkLable1 = Nothing
        Me.dtpFdate.MyLinkLable2 = Nothing
        Me.dtpFdate.Name = "dtpFdate"
        Me.dtpFdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFdate.ReferenceFieldDesc = Nothing
        Me.dtpFdate.ReferenceFieldName = Nothing
        Me.dtpFdate.ReferenceTableName = Nothing
        Me.dtpFdate.Size = New System.Drawing.Size(128, 18)
        Me.dtpFdate.TabIndex = 98
        Me.dtpFdate.TabStop = False
        Me.dtpFdate.Text = "18/05/2011 02:11 PM"
        Me.dtpFdate.Value = New Date(2011, 5, 18, 14, 11, 58, 609)
        Me.dtpFdate.Visible = False
        '
        'RadLabel1
        '
        Me.RadLabel1.BackColor = System.Drawing.Color.Transparent
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(266, 50)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(46, 16)
        Me.RadLabel1.TabIndex = 96
        Me.RadLabel1.Text = "To Date"
        Me.RadLabel1.Visible = False
        '
        'RadLabel2
        '
        Me.RadLabel2.BackColor = System.Drawing.Color.Transparent
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(15, 50)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(60, 16)
        Me.RadLabel2.TabIndex = 97
        Me.RadLabel2.Text = "From Date"
        Me.RadLabel2.Visible = False
        '
        'fndTransfer
        '
        Me.fndTransfer.CalculationExpression = Nothing
        Me.fndTransfer.FieldCode = Nothing
        Me.fndTransfer.FieldDesc = Nothing
        Me.fndTransfer.FieldMaxLength = 0
        Me.fndTransfer.FieldName = Nothing
        Me.fndTransfer.isCalculatedField = False
        Me.fndTransfer.IsSourceFromTable = False
        Me.fndTransfer.IsSourceFromValueList = False
        Me.fndTransfer.IsUnique = False
        Me.fndTransfer.Location = New System.Drawing.Point(96, 19)
        Me.fndTransfer.MendatroryField = True
        Me.fndTransfer.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndTransfer.MyLinkLable1 = Nothing
        Me.fndTransfer.MyLinkLable2 = Nothing
        Me.fndTransfer.MyReadOnly = False
        Me.fndTransfer.MyShowMasterFormButton = False
        Me.fndTransfer.Name = "fndTransfer"
        Me.fndTransfer.ReferenceFieldDesc = Nothing
        Me.fndTransfer.ReferenceFieldName = Nothing
        Me.fndTransfer.ReferenceTableName = Nothing
        Me.fndTransfer.Size = New System.Drawing.Size(191, 19)
        Me.fndTransfer.TabIndex = 102
        Me.fndTransfer.Value = ""
        '
        'FrmProvionalSalesReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(489, 127)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "FrmProvionalSalesReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Tag = ""
        Me.Text = "Proviosional Sales Report"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DtpTodate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnprint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents DtpTodate As common.Controls.MyDateTimePicker
    Friend WithEvents dtpFdate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents fndTransfer As common.UserControls.txtFinder
End Class

