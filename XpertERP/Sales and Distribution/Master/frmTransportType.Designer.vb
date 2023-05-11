<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmTransportType
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmTransportType))
        Me.grpCustomer = New Telerik.WinControls.UI.RadGroupBox
        Me.fndTransportTypeId = New common.UserControls.txtNavigator
        Me.lblTransportTypeId = New common.Controls.MyLabel
        Me.lblDescription = New common.Controls.MyLabel
        Me.btnDelete = New Telerik.WinControls.UI.RadButton
        Me.txtDescription = New common.Controls.MyTextBox
        Me.btnNew = New Telerik.WinControls.UI.RadButton
        Me.btnSave = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem
        Me.menuImport = New Telerik.WinControls.UI.RadMenuItem
        Me.menuExport = New Telerik.WinControls.UI.RadMenuItem
        Me.menuClose = New Telerik.WinControls.UI.RadMenuItem
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.grpCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCustomer.SuspendLayout()
        CType(Me.lblTransportTypeId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpCustomer
        '
        Me.grpCustomer.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpCustomer.Controls.Add(Me.fndTransportTypeId)
        Me.grpCustomer.Controls.Add(Me.lblDescription)
        Me.grpCustomer.Controls.Add(Me.lblTransportTypeId)
        Me.grpCustomer.Controls.Add(Me.txtDescription)
        Me.grpCustomer.Controls.Add(Me.btnNew)
        Me.grpCustomer.FooterImageIndex = -1
        Me.grpCustomer.FooterImageKey = ""
        Me.grpCustomer.HeaderImageIndex = -1
        Me.grpCustomer.HeaderImageKey = ""
        Me.grpCustomer.HeaderMargin = New System.Windows.Forms.Padding(0)
        Me.grpCustomer.HeaderText = ""
        Me.grpCustomer.Location = New System.Drawing.Point(13, 13)
        Me.grpCustomer.Name = "grpCustomer"
        Me.grpCustomer.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        '
        '
        '
        Me.grpCustomer.RootElement.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.grpCustomer.Size = New System.Drawing.Size(374, 93)
        Me.grpCustomer.TabIndex = 0
        '
        'fndTransportTypeId
        '
        Me.fndTransportTypeId.Location = New System.Drawing.Point(103, 27)
        Me.fndTransportTypeId.MendatroryField = True
        Me.fndTransportTypeId.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndTransportTypeId.MyLinkLable1 = Me.lblTransportTypeId
        Me.fndTransportTypeId.MyLinkLable2 = Nothing
        Me.fndTransportTypeId.MyMaxLength = 32767
        Me.fndTransportTypeId.MyReadOnly = False
        Me.fndTransportTypeId.Name = "fndTransportTypeId"
        Me.fndTransportTypeId.Size = New System.Drawing.Size(202, 21)
        Me.fndTransportTypeId.TabIndex = 0
        Me.fndTransportTypeId.Value = ""
        '
        'lblTransportTypeId
        '
        Me.lblTransportTypeId.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblTransportTypeId.Location = New System.Drawing.Point(10, 27)
        Me.lblTransportTypeId.Name = "lblTransportTypeId"
        Me.lblTransportTypeId.Size = New System.Drawing.Size(89, 16)
        Me.lblTransportTypeId.TabIndex = 0
        Me.lblTransportTypeId.Text = "Transport Type"
        '
        'lblDescription
        '
        Me.lblDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescription.Location = New System.Drawing.Point(15, 54)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(63, 16)
        Me.lblDescription.TabIndex = 5
        Me.lblDescription.Text = "Description"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(83, 3)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 18)
        Me.btnDelete.TabIndex = 4
        Me.btnDelete.Text = "Delete"
        '
        'txtDescription
        '
        Me.txtDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.Location = New System.Drawing.Point(100, 52)
        Me.txtDescription.MaxLength = 50
        Me.txtDescription.MendatroryField = False
        Me.txtDescription.Multiline = True
        Me.txtDescription.MyLinkLable1 = Me.lblDescription
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        '
        '
        '
        Me.txtDescription.RootElement.StretchVertically = True
        Me.txtDescription.Size = New System.Drawing.Size(267, 21)
        Me.txtDescription.TabIndex = 2
        Me.txtDescription.Text = " "
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(306, 27)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(16, 23)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Text = " "
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(13, 3)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 18)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "Save"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(319, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(403, 20)
        Me.RadMenu1.TabIndex = 9
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.menuImport, Me.menuExport, Me.menuClose})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        Me.RadMenuItem1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'menuImport
        '
        Me.menuImport.AccessibleDescription = "Import.."
        Me.menuImport.AccessibleName = "Import.."
        Me.menuImport.Name = "menuImport"
        Me.menuImport.Text = "Import.."
        Me.menuImport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'menuExport
        '
        Me.menuExport.AccessibleDescription = "Export.."
        Me.menuExport.AccessibleName = "Export.."
        Me.menuExport.Name = "menuExport"
        Me.menuExport.Text = "Export.."
        Me.menuExport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'menuClose
        '
        Me.menuClose.AccessibleDescription = "Close"
        Me.menuClose.AccessibleName = "Close"
        Me.menuClose.Name = "menuClose"
        Me.menuClose.Text = "Close"
        Me.menuClose.Visibility = Telerik.WinControls.ElementVisibility.Visible
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.grpCustomer)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(403, 137)
        Me.SplitContainer1.SplitterDistance = 108
        Me.SplitContainer1.TabIndex = 10
        '
        'FrmTransportType
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(403, 157)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.KeyPreview = True
        Me.Name = "FrmTransportType"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Transport Type"
        CType(Me.grpCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCustomer.ResumeLayout(False)
        Me.grpCustomer.PerformLayout()
        CType(Me.lblTransportTypeId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grpCustomer As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents menuImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents menuExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents menuClose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents lblDescription As common.Controls.MyLabel
    Friend WithEvents lblTransportTypeId As common.Controls.MyLabel
    Friend WithEvents fndTransportTypeId As common.UserControls.txtNavigator
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

