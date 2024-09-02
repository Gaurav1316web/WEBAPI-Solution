<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmSendDBTToJanaadhar
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
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.btnGenerateBill = New Telerik.WinControls.UI.RadButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.cboUnion = New common.Controls.MyComboBox()
        CType(Me.btnGenerateBill, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboUnion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnGenerateBill
        '
        Me.btnGenerateBill.Location = New System.Drawing.Point(127, 76)
        Me.btnGenerateBill.Name = "btnGenerateBill"
        Me.btnGenerateBill.Size = New System.Drawing.Size(336, 23)
        Me.btnGenerateBill.TabIndex = 0
        Me.btnGenerateBill.Text = "Send Pending DBT To Janaadhar"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 279)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(612, 33)
        Me.Panel1.TabIndex = 1
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(534, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "Close"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(127, 52)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(39, 16)
        Me.MyLabel2.TabIndex = 33
        Me.MyLabel2.Text = "Union "
        '
        'cboUnion
        '
        Me.cboUnion.AutoCompleteDisplayMember = Nothing
        Me.cboUnion.AutoCompleteValueMember = Nothing
        Me.cboUnion.CalculationExpression = Nothing
        Me.cboUnion.DropDownAnimationEnabled = True
        Me.cboUnion.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboUnion.FieldCode = Nothing
        Me.cboUnion.FieldDesc = Nothing
        Me.cboUnion.FieldMaxLength = 0
        Me.cboUnion.FieldName = Nothing
        Me.cboUnion.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboUnion.isCalculatedField = False
        Me.cboUnion.IsSourceFromTable = False
        Me.cboUnion.IsSourceFromValueList = False
        Me.cboUnion.IsUnique = False
        RadListDataItem1.Text = "M"
        RadListDataItem2.Text = "E"
        Me.cboUnion.Items.Add(RadListDataItem1)
        Me.cboUnion.Items.Add(RadListDataItem2)
        Me.cboUnion.Location = New System.Drawing.Point(173, 51)
        Me.cboUnion.MendatroryField = True
        Me.cboUnion.MyLinkLable1 = Me.MyLabel2
        Me.cboUnion.MyLinkLable2 = Nothing
        Me.cboUnion.Name = "cboUnion"
        Me.cboUnion.ReferenceFieldDesc = Nothing
        Me.cboUnion.ReferenceFieldName = Nothing
        Me.cboUnion.ReferenceTableName = Nothing
        Me.cboUnion.Size = New System.Drawing.Size(290, 18)
        Me.cboUnion.TabIndex = 32
        '
        'FrmSendDBTToJanaadhar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(612, 312)
        Me.Controls.Add(Me.MyLabel2)
        Me.Controls.Add(Me.cboUnion)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnGenerateBill)
        Me.Name = "FrmSendDBTToJanaadhar"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Send DBT to Janaadhar"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.btnGenerateBill, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboUnion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnGenerateBill As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents Panel1 As Panel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents cboUnion As common.Controls.MyComboBox
End Class

