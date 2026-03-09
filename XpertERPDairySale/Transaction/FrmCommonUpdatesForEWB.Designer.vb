<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCommonUpdatesForEWB
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
        Me.chkNoTranspoter = New Telerik.WinControls.UI.RadCheckBox()
        Me.MyLabel31 = New common.Controls.MyLabel()
        Me.lblTransporter = New common.Controls.MyLabel()
        Me.fndTransporter = New common.UserControls.txtFinder()
        Me.lblVehicleNo = New common.Controls.MyLabel()
        Me.txtManualVehicle = New common.Controls.MyTextBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.txtDocNo = New common.Controls.MyLabel()
        Me.txtVendorno = New common.Controls.MyLabel()
        CType(Me.chkNoTranspoter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel31, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTransporter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVehicleNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtManualVehicle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDocNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVendorno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'chkNoTranspoter
        '
        Me.chkNoTranspoter.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkNoTranspoter.Location = New System.Drawing.Point(438, 74)
        Me.chkNoTranspoter.Name = "chkNoTranspoter"
        Me.chkNoTranspoter.Size = New System.Drawing.Size(99, 16)
        Me.chkNoTranspoter.TabIndex = 1598
        Me.chkNoTranspoter.Text = "NO Transporter"
        '
        'MyLabel31
        '
        Me.MyLabel31.FieldName = Nothing
        Me.MyLabel31.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel31.Location = New System.Drawing.Point(36, 74)
        Me.MyLabel31.Name = "MyLabel31"
        Me.MyLabel31.Size = New System.Drawing.Size(77, 16)
        Me.MyLabel31.TabIndex = 1601
        Me.MyLabel31.Text = "Transporter Id"
        '
        'lblTransporter
        '
        Me.lblTransporter.AutoSize = False
        Me.lblTransporter.BorderVisible = True
        Me.lblTransporter.FieldName = Nothing
        Me.lblTransporter.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTransporter.Location = New System.Drawing.Point(237, 77)
        Me.lblTransporter.Name = "lblTransporter"
        Me.lblTransporter.Size = New System.Drawing.Size(172, 17)
        Me.lblTransporter.TabIndex = 1600
        Me.lblTransporter.TextWrap = False
        '
        'fndTransporter
        '
        Me.fndTransporter.CalculationExpression = Nothing
        Me.fndTransporter.FieldCode = Nothing
        Me.fndTransporter.FieldDesc = Nothing
        Me.fndTransporter.FieldMaxLength = 0
        Me.fndTransporter.FieldName = Nothing
        Me.fndTransporter.isCalculatedField = False
        Me.fndTransporter.IsSourceFromTable = False
        Me.fndTransporter.IsSourceFromValueList = False
        Me.fndTransporter.IsUnique = False
        Me.fndTransporter.Location = New System.Drawing.Point(116, 74)
        Me.fndTransporter.MendatroryField = False
        Me.fndTransporter.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndTransporter.MyLinkLable1 = Nothing
        Me.fndTransporter.MyLinkLable2 = Nothing
        Me.fndTransporter.MyReadOnly = False
        Me.fndTransporter.MyShowMasterFormButton = False
        Me.fndTransporter.Name = "fndTransporter"
        Me.fndTransporter.ReferenceFieldDesc = Nothing
        Me.fndTransporter.ReferenceFieldName = Nothing
        Me.fndTransporter.ReferenceTableName = Nothing
        Me.fndTransporter.Size = New System.Drawing.Size(115, 20)
        Me.fndTransporter.TabIndex = 1599
        Me.fndTransporter.Value = ""
        '
        'lblVehicleNo
        '
        Me.lblVehicleNo.FieldName = Nothing
        Me.lblVehicleNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVehicleNo.Location = New System.Drawing.Point(36, 101)
        Me.lblVehicleNo.Name = "lblVehicleNo"
        Me.lblVehicleNo.Size = New System.Drawing.Size(61, 16)
        Me.lblVehicleNo.TabIndex = 1603
        Me.lblVehicleNo.Text = "Vehicle No"
        '
        'txtManualVehicle
        '
        Me.txtManualVehicle.CalculationExpression = Nothing
        Me.txtManualVehicle.FieldCode = Nothing
        Me.txtManualVehicle.FieldDesc = Nothing
        Me.txtManualVehicle.FieldMaxLength = 0
        Me.txtManualVehicle.FieldName = Nothing
        Me.txtManualVehicle.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtManualVehicle.isCalculatedField = False
        Me.txtManualVehicle.IsSourceFromTable = False
        Me.txtManualVehicle.IsSourceFromValueList = False
        Me.txtManualVehicle.IsUnique = False
        Me.txtManualVehicle.Location = New System.Drawing.Point(115, 101)
        Me.txtManualVehicle.MaxLength = 200
        Me.txtManualVehicle.MendatroryField = False
        Me.txtManualVehicle.MyLinkLable1 = Me.lblVehicleNo
        Me.txtManualVehicle.MyLinkLable2 = Nothing
        Me.txtManualVehicle.Name = "txtManualVehicle"
        Me.txtManualVehicle.ReferenceFieldDesc = Nothing
        Me.txtManualVehicle.ReferenceFieldName = Nothing
        Me.txtManualVehicle.ReferenceTableName = Nothing
        Me.txtManualVehicle.Size = New System.Drawing.Size(116, 18)
        Me.txtManualVehicle.TabIndex = 1602
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtVendorno)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDocNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkNoTranspoter)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblVehicleNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndTransporter)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtManualVehicle)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblTransporter)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel31)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(800, 450)
        Me.SplitContainer1.SplitterDistance = 403
        Me.SplitContainer1.TabIndex = 1604
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(705, 9)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(71, 22)
        Me.btnClose.TabIndex = 11
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(12, 9)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(57, 22)
        Me.btnSave.TabIndex = 1604
        Me.btnSave.Text = "Save"
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDocNo.Location = New System.Drawing.Point(36, 52)
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(33, 16)
        Me.txtDocNo.TabIndex = 1605
        Me.txtDocNo.Text = "Code"
        '
        'txtVendorno
        '
        Me.txtVendorno.FieldName = Nothing
        Me.txtVendorno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVendorno.Location = New System.Drawing.Point(237, 52)
        Me.txtVendorno.Name = "txtVendorno"
        Me.txtVendorno.Size = New System.Drawing.Size(33, 16)
        Me.txtVendorno.TabIndex = 1606
        Me.txtVendorno.Text = "Code"
        '
        'FrmCommonUpdatesForEWB
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmCommonUpdatesForEWB"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Update Details"
        CType(Me.chkNoTranspoter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel31, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTransporter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVehicleNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtManualVehicle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDocNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVendorno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents chkNoTranspoter As RadCheckBox
    Friend WithEvents MyLabel31 As common.Controls.MyLabel
    Friend WithEvents lblTransporter As common.Controls.MyLabel
    Friend WithEvents fndTransporter As common.UserControls.txtFinder
    Friend WithEvents lblVehicleNo As common.Controls.MyLabel
    Friend WithEvents txtManualVehicle As common.Controls.MyTextBox
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents btnClose As RadButton
    Friend WithEvents btnSave As RadButton
    Friend WithEvents txtDocNo As common.Controls.MyLabel
    Friend WithEvents txtVendorno As common.Controls.MyLabel
End Class
