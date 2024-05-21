<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChapterHead
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmChapterHead))
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenuItem()
        Me.rdmenuimport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rdmenuexport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rdmenuexit = New Telerik.WinControls.UI.RadMenuItem()
        Me.lblDescription = New common.Controls.MyLabel()
        Me.rdbtnDelete = New Telerik.WinControls.UI.RadButton()
        Me.rdbtnSave = New Telerik.WinControls.UI.RadButton()
        Me.rdbtnClose = New Telerik.WinControls.UI.RadButton()
        Me.grpCustomer = New Telerik.WinControls.UI.RadGroupBox()
        Me.fndchapterhead = New common.UserControls.txtNavigator()
        Me.lblCustomerId = New common.Controls.MyLabel()
        Me.rdtxtchapterdesc = New common.Controls.MyTextBox()
        Me.rdbtnreset = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCustomer.SuspendLayout()
        CType(Me.lblCustomerId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdtxtchapterdesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenufile})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(427, 20)
        Me.RadMenu1.TabIndex = 0
        Me.RadMenu1.Text = "RadMenu1"
        '
        'rdmenufile
        '
        Me.rdmenufile.AccessibleDescription = "File"
        Me.rdmenufile.AccessibleName = "File"
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenuimport, Me.rdmenuexport, Me.rdmenuexit})
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Text = "File"
        '
        'rdmenuimport
        '
        Me.rdmenuimport.AccessibleDescription = "Import"
        Me.rdmenuimport.AccessibleName = "Import"
        Me.rdmenuimport.Name = "rdmenuimport"
        Me.rdmenuimport.Text = "Import"
        '
        'rdmenuexport
        '
        Me.rdmenuexport.AccessibleDescription = "Export"
        Me.rdmenuexport.AccessibleName = "Export"
        Me.rdmenuexport.Name = "rdmenuexport"
        Me.rdmenuexport.Text = "Export"
        '
        'rdmenuexit
        '
        Me.rdmenuexit.AccessibleDescription = "Exit"
        Me.rdmenuexit.AccessibleName = "Exit"
        Me.rdmenuexit.Name = "rdmenuexit"
        Me.rdmenuexit.Text = "Exit"
        '
        'lblDescription
        '
        Me.lblDescription.FieldName = Nothing
        Me.lblDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescription.Location = New System.Drawing.Point(13, 52)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(63, 16)
        Me.lblDescription.TabIndex = 5
        Me.lblDescription.Text = "Description"
        '
        'rdbtnDelete
        '
        Me.rdbtnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdbtnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtnDelete.Location = New System.Drawing.Point(73, 3)
        Me.rdbtnDelete.Name = "rdbtnDelete"
        Me.rdbtnDelete.Size = New System.Drawing.Size(68, 18)
        Me.rdbtnDelete.TabIndex = 1
        Me.rdbtnDelete.Text = " Delete"
        '
        'rdbtnSave
        '
        Me.rdbtnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdbtnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtnSave.Location = New System.Drawing.Point(3, 3)
        Me.rdbtnSave.Name = "rdbtnSave"
        Me.rdbtnSave.Size = New System.Drawing.Size(68, 18)
        Me.rdbtnSave.TabIndex = 0
        Me.rdbtnSave.Text = "Save"
        '
        'rdbtnClose
        '
        Me.rdbtnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdbtnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtnClose.Location = New System.Drawing.Point(351, 4)
        Me.rdbtnClose.Name = "rdbtnClose"
        Me.rdbtnClose.Size = New System.Drawing.Size(68, 18)
        Me.rdbtnClose.TabIndex = 2
        Me.rdbtnClose.Text = "Close"
        '
        'grpCustomer
        '
        Me.grpCustomer.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpCustomer.Controls.Add(Me.fndchapterhead)
        Me.grpCustomer.Controls.Add(Me.lblDescription)
        Me.grpCustomer.Controls.Add(Me.lblCustomerId)
        Me.grpCustomer.Controls.Add(Me.rdtxtchapterdesc)
        Me.grpCustomer.Controls.Add(Me.rdbtnreset)
        Me.grpCustomer.HeaderText = ""
        Me.grpCustomer.Location = New System.Drawing.Point(6, 9)
        Me.grpCustomer.Name = "grpCustomer"
        Me.grpCustomer.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.grpCustomer.Size = New System.Drawing.Size(413, 91)
        Me.grpCustomer.TabIndex = 0
        '
        'fndchapterhead
        '
        Me.fndchapterhead.FieldName = Nothing
        Me.fndchapterhead.Location = New System.Drawing.Point(91, 21)
        Me.fndchapterhead.MendatroryField = True
        Me.fndchapterhead.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.fndchapterhead.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndchapterhead.MyLinkLable1 = Me.lblCustomerId
        Me.fndchapterhead.MyLinkLable2 = Nothing
        Me.fndchapterhead.MyMaxLength = 32767
        Me.fndchapterhead.MyReadOnly = False
        Me.fndchapterhead.Name = "fndchapterhead"
        Me.fndchapterhead.Size = New System.Drawing.Size(253, 21)
        Me.fndchapterhead.TabIndex = 0
        Me.fndchapterhead.Value = ""
        '
        'lblCustomerId
        '
        Me.lblCustomerId.FieldName = Nothing
        Me.lblCustomerId.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerId.Location = New System.Drawing.Point(13, 23)
        Me.lblCustomerId.Name = "lblCustomerId"
        Me.lblCustomerId.Size = New System.Drawing.Size(33, 16)
        Me.lblCustomerId.TabIndex = 0
        Me.lblCustomerId.Text = "Code"
        '
        'rdtxtchapterdesc
        '
        Me.rdtxtchapterdesc.AutoSize = False
        Me.rdtxtchapterdesc.CalculationExpression = Nothing
        Me.rdtxtchapterdesc.FieldCode = Nothing
        Me.rdtxtchapterdesc.FieldDesc = Nothing
        Me.rdtxtchapterdesc.FieldMaxLength = 0
        Me.rdtxtchapterdesc.FieldName = Nothing
        Me.rdtxtchapterdesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdtxtchapterdesc.isCalculatedField = False
        Me.rdtxtchapterdesc.IsSourceFromTable = False
        Me.rdtxtchapterdesc.IsSourceFromValueList = False
        Me.rdtxtchapterdesc.IsUnique = False
        Me.rdtxtchapterdesc.Location = New System.Drawing.Point(91, 47)
        Me.rdtxtchapterdesc.MaxLength = 50
        Me.rdtxtchapterdesc.MendatroryField = False
        Me.rdtxtchapterdesc.Multiline = True
        Me.rdtxtchapterdesc.MyLinkLable1 = Me.lblDescription
        Me.rdtxtchapterdesc.MyLinkLable2 = Nothing
        Me.rdtxtchapterdesc.Name = "rdtxtchapterdesc"
        Me.rdtxtchapterdesc.ReferenceFieldDesc = Nothing
        Me.rdtxtchapterdesc.ReferenceFieldName = Nothing
        Me.rdtxtchapterdesc.ReferenceTableName = Nothing
        Me.rdtxtchapterdesc.Size = New System.Drawing.Size(274, 21)
        Me.rdtxtchapterdesc.TabIndex = 2
        Me.rdtxtchapterdesc.Text = " "
        '
        'rdbtnreset
        '
        Me.rdbtnreset.Image = CType(resources.GetObject("rdbtnreset.Image"), System.Drawing.Image)
        Me.rdbtnreset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.rdbtnreset.Location = New System.Drawing.Point(344, 21)
        Me.rdbtnreset.Name = "rdbtnreset"
        Me.rdbtnreset.Size = New System.Drawing.Size(21, 21)
        Me.rdbtnreset.TabIndex = 1
        Me.rdbtnreset.Text = " "
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnDelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(427, 133)
        Me.SplitContainer1.SplitterDistance = 104
        Me.SplitContainer1.TabIndex = 1
        '
        'frmChapterHead
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(427, 153)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmChapterHead"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Item Sub Group Type"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCustomer.ResumeLayout(False)
        Me.grpCustomer.PerformLayout()
        CType(Me.lblCustomerId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdtxtchapterdesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnreset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdmenuimport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdmenuexport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdmenuexit As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdbtnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents grpCustomer As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rdtxtchapterdesc As common.Controls.MyTextBox
    Friend WithEvents rdbtnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblDescription As common.Controls.MyLabel
    Friend WithEvents lblCustomerId As common.Controls.MyLabel
    Friend WithEvents fndchapterhead As common.UserControls.txtNavigator
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

