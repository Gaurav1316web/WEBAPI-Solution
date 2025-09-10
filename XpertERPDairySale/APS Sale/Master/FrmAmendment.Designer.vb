<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAmendment
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
        Me.txtTotalQty = New common.MyNumBox()
        Me.lblQty = New common.Controls.MyLabel()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.lblToDate = New common.Controls.MyLabel()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnOK = New Telerik.WinControls.UI.RadButton()
        Me.lblDocuemntCode = New common.Controls.MyLabel()
        CType(Me.txtTotalQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnOK, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocuemntCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtTotalQty
        '
        Me.txtTotalQty.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtTotalQty.CalculationExpression = Nothing
        Me.txtTotalQty.DecimalPlaces = 0
        Me.txtTotalQty.FieldCode = Nothing
        Me.txtTotalQty.FieldDesc = Nothing
        Me.txtTotalQty.FieldMaxLength = 0
        Me.txtTotalQty.FieldName = Nothing
        Me.txtTotalQty.isCalculatedField = False
        Me.txtTotalQty.IsSourceFromTable = False
        Me.txtTotalQty.IsSourceFromValueList = False
        Me.txtTotalQty.IsUnique = False
        Me.txtTotalQty.Location = New System.Drawing.Point(89, 19)
        Me.txtTotalQty.MendatroryField = False
        Me.txtTotalQty.MyLinkLable1 = Nothing
        Me.txtTotalQty.MyLinkLable2 = Nothing
        Me.txtTotalQty.Name = "txtTotalQty"
        Me.txtTotalQty.ReferenceFieldDesc = Nothing
        Me.txtTotalQty.ReferenceFieldName = Nothing
        Me.txtTotalQty.ReferenceTableName = Nothing
        Me.txtTotalQty.Size = New System.Drawing.Size(80, 20)
        Me.txtTotalQty.TabIndex = 1445
        Me.txtTotalQty.Text = "0"
        Me.txtTotalQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTotalQty.Value = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'lblQty
        '
        Me.lblQty.FieldName = Nothing
        Me.lblQty.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblQty.Location = New System.Drawing.Point(26, 21)
        Me.lblQty.Name = "lblQty"
        Me.lblQty.Size = New System.Drawing.Size(52, 16)
        Me.lblQty.TabIndex = 1444
        Me.lblQty.Text = "Total Qty"
        '
        'txtToDate
        '
        Me.txtToDate.CalculationExpression = Nothing
        Me.txtToDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtToDate.FieldCode = Nothing
        Me.txtToDate.FieldDesc = Nothing
        Me.txtToDate.FieldMaxLength = 0
        Me.txtToDate.FieldName = Nothing
        Me.txtToDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.isCalculatedField = False
        Me.txtToDate.IsSourceFromTable = False
        Me.txtToDate.IsSourceFromValueList = False
        Me.txtToDate.IsUnique = False
        Me.txtToDate.Location = New System.Drawing.Point(89, 42)
        Me.txtToDate.MendatroryField = False
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Me.lblToDate
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.ReferenceFieldDesc = Nothing
        Me.txtToDate.ReferenceFieldName = Nothing
        Me.txtToDate.ReferenceTableName = Nothing
        Me.txtToDate.Size = New System.Drawing.Size(81, 18)
        Me.txtToDate.TabIndex = 1447
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "13/06/2011 11:29 AM"
        Me.txtToDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'lblToDate
        '
        Me.lblToDate.FieldName = Nothing
        Me.lblToDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblToDate.Location = New System.Drawing.Point(26, 43)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(46, 16)
        Me.lblToDate.TabIndex = 1446
        Me.lblToDate.Text = "To Date"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(100, 73)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 1449
        Me.btnClose.Text = "Close"
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnOK.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOK.Location = New System.Drawing.Point(26, 73)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(69, 22)
        Me.btnOK.TabIndex = 1448
        Me.btnOK.Text = "OK"
        '
        'lblDocuemntCode
        '
        Me.lblDocuemntCode.AutoSize = False
        Me.lblDocuemntCode.BorderVisible = True
        Me.lblDocuemntCode.FieldName = Nothing
        Me.lblDocuemntCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocuemntCode.Location = New System.Drawing.Point(52, -3)
        Me.lblDocuemntCode.Name = "lblDocuemntCode"
        Me.lblDocuemntCode.Size = New System.Drawing.Size(114, 18)
        Me.lblDocuemntCode.TabIndex = 1450
        Me.lblDocuemntCode.TextWrap = False
        Me.lblDocuemntCode.Visible = False
        '
        'FrmAmendment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(248, 125)
        Me.Controls.Add(Me.lblDocuemntCode)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.txtToDate)
        Me.Controls.Add(Me.lblToDate)
        Me.Controls.Add(Me.txtTotalQty)
        Me.Controls.Add(Me.lblQty)
        Me.MaximizeBox = False
        Me.Name = "FrmAmendment"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Customer Tender Amendment"
        CType(Me.txtTotalQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnOK, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocuemntCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtTotalQty As common.MyNumBox
    Friend WithEvents lblQty As common.Controls.MyLabel
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblToDate As common.Controls.MyLabel
    Friend WithEvents btnClose As RadButton
    Friend WithEvents btnOK As RadButton
    Friend WithEvents lblDocuemntCode As common.Controls.MyLabel
End Class
