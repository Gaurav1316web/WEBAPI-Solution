<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmExpiryDate
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.LblDocDate = New common.Controls.MyLabel
        Me.lblExpiry = New common.Controls.MyLabel
        Me.FndDocumnetNo = New common.UserControls.txtFinder
        Me.lblScreenName = New common.Controls.MyLabel
        Me.txtNewExpiryDate = New common.Controls.MyDateTimePicker
        Me.lblNewExpiryDate = New common.Controls.MyLabel
        Me.cmbScreenName = New common.Controls.MyComboBox
        Me.lblDocumentNo = New common.Controls.MyLabel
        Me.lblExpiryDate = New common.Controls.MyLabel
        Me.lblDocumentDate = New common.Controls.MyLabel
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        Me.btnsave = New Telerik.WinControls.UI.RadButton
        Me.btndelete = New Telerik.WinControls.UI.RadButton
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.LblDocDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblExpiry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblScreenName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNewExpiryDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNewExpiryDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbScreenName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocumentNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblExpiryDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocumentDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(1053, 448)
        Me.SplitContainer1.SplitterDistance = 401
        Me.SplitContainer1.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.LblDocDate)
        Me.GroupBox1.Controls.Add(Me.lblExpiry)
        Me.GroupBox1.Controls.Add(Me.FndDocumnetNo)
        Me.GroupBox1.Controls.Add(Me.lblScreenName)
        Me.GroupBox1.Controls.Add(Me.txtNewExpiryDate)
        Me.GroupBox1.Controls.Add(Me.cmbScreenName)
        Me.GroupBox1.Controls.Add(Me.lblNewExpiryDate)
        Me.GroupBox1.Controls.Add(Me.lblDocumentNo)
        Me.GroupBox1.Controls.Add(Me.lblExpiryDate)
        Me.GroupBox1.Controls.Add(Me.lblDocumentDate)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 8)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(433, 140)
        Me.GroupBox1.TabIndex = 123
        Me.GroupBox1.TabStop = False
        '
        'LblDocDate
        '
        Me.LblDocDate.AutoSize = False
        Me.LblDocDate.BorderVisible = True
        Me.LblDocDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDocDate.Location = New System.Drawing.Point(135, 69)
        Me.LblDocDate.Name = "LblDocDate"
        Me.LblDocDate.Size = New System.Drawing.Size(97, 18)
        Me.LblDocDate.TabIndex = 125
        Me.LblDocDate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblDocDate.TextWrap = False
        '
        'lblExpiry
        '
        Me.lblExpiry.AutoSize = False
        Me.lblExpiry.BorderVisible = True
        Me.lblExpiry.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExpiry.Location = New System.Drawing.Point(135, 94)
        Me.lblExpiry.Name = "lblExpiry"
        Me.lblExpiry.Size = New System.Drawing.Size(97, 18)
        Me.lblExpiry.TabIndex = 124
        Me.lblExpiry.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblExpiry.TextWrap = False
        '
        'FndDocumnetNo
        '
        Me.FndDocumnetNo.Location = New System.Drawing.Point(135, 45)
        Me.FndDocumnetNo.MendatroryField = True
        Me.FndDocumnetNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndDocumnetNo.MyLinkLable1 = Nothing
        Me.FndDocumnetNo.MyLinkLable2 = Nothing
        Me.FndDocumnetNo.MyReadOnly = False
        Me.FndDocumnetNo.MyShowMasterFormButton = False
        Me.FndDocumnetNo.Name = "FndDocumnetNo"
        Me.FndDocumnetNo.Size = New System.Drawing.Size(180, 18)
        Me.FndDocumnetNo.TabIndex = 123
        Me.FndDocumnetNo.Value = ""
        '
        'lblScreenName
        '
        Me.lblScreenName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblScreenName.Location = New System.Drawing.Point(51, 21)
        Me.lblScreenName.Name = "lblScreenName"
        Me.lblScreenName.Size = New System.Drawing.Size(75, 16)
        Me.lblScreenName.TabIndex = 62
        Me.lblScreenName.Text = "Screen Name"
        '
        'txtNewExpiryDate
        '
        Me.txtNewExpiryDate.CustomFormat = "dd/MM/yyyy "
        Me.txtNewExpiryDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewExpiryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtNewExpiryDate.Location = New System.Drawing.Point(135, 117)
        Me.txtNewExpiryDate.MendatroryField = True
        Me.txtNewExpiryDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtNewExpiryDate.MyLinkLable1 = Me.lblNewExpiryDate
        Me.txtNewExpiryDate.MyLinkLable2 = Nothing
        Me.txtNewExpiryDate.Name = "txtNewExpiryDate"
        Me.txtNewExpiryDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtNewExpiryDate.Size = New System.Drawing.Size(97, 18)
        Me.txtNewExpiryDate.TabIndex = 121
        Me.txtNewExpiryDate.TabStop = False
        Me.txtNewExpiryDate.Text = "03/05/2011 "
        Me.txtNewExpiryDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblNewExpiryDate
        '
        Me.lblNewExpiryDate.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblNewExpiryDate.Location = New System.Drawing.Point(35, 118)
        Me.lblNewExpiryDate.Name = "lblNewExpiryDate"
        Me.lblNewExpiryDate.Size = New System.Drawing.Size(91, 16)
        Me.lblNewExpiryDate.TabIndex = 122
        Me.lblNewExpiryDate.Text = "New Expiry Date"
        '
        'cmbScreenName
        '
        Me.cmbScreenName.AllowShowFocusCues = False
        Me.cmbScreenName.AutoCompleteDisplayMember = Nothing
        Me.cmbScreenName.AutoCompleteValueMember = Nothing
        Me.cmbScreenName.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbScreenName.Location = New System.Drawing.Point(135, 19)
        Me.cmbScreenName.MendatroryField = True
        Me.cmbScreenName.MyLinkLable1 = Me.lblScreenName
        Me.cmbScreenName.MyLinkLable2 = Nothing
        Me.cmbScreenName.Name = "cmbScreenName"
        Me.cmbScreenName.Size = New System.Drawing.Size(293, 20)
        Me.cmbScreenName.TabIndex = 61
        '
        'lblDocumentNo
        '
        Me.lblDocumentNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocumentNo.Location = New System.Drawing.Point(47, 44)
        Me.lblDocumentNo.Name = "lblDocumentNo"
        Me.lblDocumentNo.Size = New System.Drawing.Size(79, 16)
        Me.lblDocumentNo.TabIndex = 116
        Me.lblDocumentNo.Text = "Document No."
        '
        'lblExpiryDate
        '
        Me.lblExpiryDate.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblExpiryDate.Location = New System.Drawing.Point(61, 95)
        Me.lblExpiryDate.Name = "lblExpiryDate"
        Me.lblExpiryDate.Size = New System.Drawing.Size(65, 16)
        Me.lblExpiryDate.TabIndex = 120
        Me.lblExpiryDate.Text = "Expiry Date"
        '
        'lblDocumentDate
        '
        Me.lblDocumentDate.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblDocumentDate.Location = New System.Drawing.Point(41, 69)
        Me.lblDocumentDate.Name = "lblDocumentDate"
        Me.lblDocumentDate.Size = New System.Drawing.Size(85, 16)
        Me.lblDocumentDate.TabIndex = 118
        Me.lblDocumentDate.Text = "Document Date"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(156, 13)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(66, 18)
        Me.btnReset.TabIndex = 6
        Me.btnReset.Text = "Reset"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(12, 13)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 3
        Me.btnsave.Text = "Save"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(84, 13)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 4
        Me.btndelete.Text = "Delete"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(975, 13)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 5
        Me.btnclose.Text = "Close"
        '
        'FrmExpiryDate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1053, 448)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmExpiryDate"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmExpiryDate"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.LblDocDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblExpiry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblScreenName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNewExpiryDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNewExpiryDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbScreenName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocumentNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblExpiryDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocumentDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents cmbScreenName As common.Controls.MyComboBox
    Friend WithEvents lblScreenName As common.Controls.MyLabel
    Friend WithEvents lblDocumentNo As common.Controls.MyLabel
    Friend WithEvents txtNewExpiryDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblNewExpiryDate As common.Controls.MyLabel
    Friend WithEvents lblExpiryDate As common.Controls.MyLabel
    Friend WithEvents lblDocumentDate As common.Controls.MyLabel
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents FndDocumnetNo As common.UserControls.txtFinder
    Friend WithEvents LblDocDate As common.Controls.MyLabel
    Friend WithEvents lblExpiry As common.Controls.MyLabel
End Class

