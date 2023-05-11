<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMerchantPaymentTerms
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
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem
        Me.RMImport = New Telerik.WinControls.UI.RadMenuItem
        Me.RMExport = New Telerik.WinControls.UI.RadMenuItem
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.cboTermsType = New common.Controls.MyComboBox
        Me.lblTermsType = New common.Controls.MyLabel
        Me.fndTermsCode = New common.UserControls.txtNavigator
        Me.lblcode = New common.Controls.MyLabel
        Me.TxtDescription = New common.Controls.MyTextBox
        Me.lblname = New common.Controls.MyLabel
        Me.btnnew = New Telerik.WinControls.UI.RadButton
        Me.btnsave = New Telerik.WinControls.UI.RadButton
        Me.btndelete = New Telerik.WinControls.UI.RadButton
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.cboTermsType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTermsType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(436, 20)
        Me.RadMenu1.TabIndex = 1
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RMImport, Me.RMExport})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        Me.RadMenuItem1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RMImport
        '
        Me.RMImport.AccessibleDescription = "Import"
        Me.RMImport.AccessibleName = "Import"
        Me.RMImport.Name = "RMImport"
        Me.RMImport.Text = "Import"
        Me.RMImport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RMExport
        '
        Me.RMExport.AccessibleDescription = "Export"
        Me.RMExport.AccessibleName = "Export"
        Me.RMExport.Name = "RMExport"
        Me.RMExport.Text = "Export"
        Me.RMExport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboTermsType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblTermsType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndTermsCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblcode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TxtDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblname)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnnew)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(436, 124)
        Me.SplitContainer1.SplitterDistance = 95
        Me.SplitContainer1.TabIndex = 0
        '
        'cboTermsType
        '
        Me.cboTermsType.AllowShowFocusCues = False
        Me.cboTermsType.AutoCompleteDisplayMember = Nothing
        Me.cboTermsType.AutoCompleteValueMember = Nothing
        Me.cboTermsType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboTermsType.Location = New System.Drawing.Point(86, 56)
        Me.cboTermsType.MendatroryField = False
        Me.cboTermsType.MyLinkLable1 = Nothing
        Me.cboTermsType.MyLinkLable2 = Nothing
        Me.cboTermsType.Name = "cboTermsType"
        Me.cboTermsType.Size = New System.Drawing.Size(236, 20)
        Me.cboTermsType.TabIndex = 6
        '
        'lblTermsType
        '
        Me.lblTermsType.Location = New System.Drawing.Point(9, 57)
        Me.lblTermsType.Name = "lblTermsType"
        Me.lblTermsType.Size = New System.Drawing.Size(30, 18)
        Me.lblTermsType.TabIndex = 5
        Me.lblTermsType.Text = "Type"
        '
        'fndTermsCode
        '
        Me.fndTermsCode.Location = New System.Drawing.Point(86, 7)
        Me.fndTermsCode.MendatroryField = True
        Me.fndTermsCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndTermsCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndTermsCode.MyLinkLable1 = Nothing
        Me.fndTermsCode.MyLinkLable2 = Nothing
        Me.fndTermsCode.MyMaxLength = 32767
        Me.fndTermsCode.MyReadOnly = False
        Me.fndTermsCode.Name = "fndTermsCode"
        Me.fndTermsCode.Size = New System.Drawing.Size(215, 21)
        Me.fndTermsCode.TabIndex = 1
        Me.fndTermsCode.Value = ""
        '
        'lblcode
        '
        Me.lblcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcode.Location = New System.Drawing.Point(9, 9)
        Me.lblcode.Name = "lblcode"
        Me.lblcode.Size = New System.Drawing.Size(33, 16)
        Me.lblcode.TabIndex = 0
        Me.lblcode.Text = "Code"
        '
        'TxtDescription
        '
        Me.TxtDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDescription.Location = New System.Drawing.Point(86, 32)
        Me.TxtDescription.MaxLength = 200
        Me.TxtDescription.MendatroryField = True
        Me.TxtDescription.MyLinkLable1 = Nothing
        Me.TxtDescription.MyLinkLable2 = Nothing
        Me.TxtDescription.Name = "TxtDescription"
        '
        '
        '
        Me.TxtDescription.RootElement.StretchVertically = True
        Me.TxtDescription.Size = New System.Drawing.Size(339, 20)
        Me.TxtDescription.TabIndex = 4
        '
        'lblname
        '
        Me.lblname.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblname.Location = New System.Drawing.Point(9, 34)
        Me.lblname.Name = "lblname"
        Me.lblname.Size = New System.Drawing.Size(63, 16)
        Me.lblname.TabIndex = 3
        Me.lblname.Text = "Description"
        '
        'btnnew
        '
        Me.btnnew.Image = XpertERPSalesAndDistribution.My.Resources.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(304, 7)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(15, 20)
        Me.btnnew.TabIndex = 2
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(7, 3)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(80, 3)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(360, 3)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'FrmMerchantPaymentTerms
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(436, 144)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmMerchantPaymentTerms"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmMerchant Payment Terms"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.cboTermsType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTermsType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents fndTermsCode As common.UserControls.txtNavigator
    Friend WithEvents lblcode As common.Controls.MyLabel
    Friend WithEvents TxtDescription As common.Controls.MyTextBox
    Friend WithEvents lblname As common.Controls.MyLabel
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents cboTermsType As common.Controls.MyComboBox
    Friend WithEvents lblTermsType As common.Controls.MyLabel
    Friend WithEvents RMImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RMExport As Telerik.WinControls.UI.RadMenuItem
End Class

