<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmToolType
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
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenuItem
        Me.rdgpbxcustomeraccountset = New Telerik.WinControls.UI.RadGroupBox
        Me.fnduom = New common.UserControls.txtFinder
        Me.MyLabel13 = New common.Controls.MyLabel
        Me.txtuom = New common.Controls.MyTextBox
        Me.txtComments = New common.Controls.MyTextBox
        Me.MyLabel20 = New common.Controls.MyLabel
        Me.txtCost = New common.MyNumBox
        Me.MyLabel9 = New common.Controls.MyLabel
        Me.txtLastMaitainDate = New common.Controls.MyDateTimePicker
        Me.MyLabel5 = New common.Controls.MyLabel
        Me.cboStatus = New common.Controls.MyComboBox
        Me.MyLabel4 = New common.Controls.MyLabel
        Me.fndToolCode = New common.UserControls.txtNavigator
        Me.rdlblAccountsetcode = New common.Controls.MyLabel
        Me.rdlbldescription = New common.Controls.MyLabel
        Me.txtdescription = New common.Controls.MyTextBox
        Me.rdbtnnew = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.btnDelete = New Telerik.WinControls.UI.RadButton
        Me.rdbtnsave = New Telerik.WinControls.UI.RadButton
        Me.rdbtnclose = New Telerik.WinControls.UI.RadButton
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdgpbxcustomeraccountset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.rdgpbxcustomeraccountset.SuspendLayout()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtuom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtComments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLastMaitainDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlblAccountsetcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlbldescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenufile})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(632, 20)
        Me.RadMenu1.TabIndex = 4
        Me.RadMenu1.Text = "RadMenu1"
        '
        'rdmenufile
        '
        Me.rdmenufile.AccessibleDescription = "RadMenuItem1"
        Me.rdmenufile.AccessibleName = "RadMenuItem1"
        Me.rdmenufile.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Text = "File"
        Me.rdmenufile.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'rdgpbxcustomeraccountset
        '
        Me.rdgpbxcustomeraccountset.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.fnduom)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.txtuom)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.MyLabel13)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.txtComments)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.txtCost)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.MyLabel9)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.MyLabel20)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.txtLastMaitainDate)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.MyLabel5)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.cboStatus)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.MyLabel4)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.fndToolCode)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.rdlblAccountsetcode)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.rdlbldescription)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.txtdescription)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.rdbtnnew)
        Me.rdgpbxcustomeraccountset.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rdgpbxcustomeraccountset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdgpbxcustomeraccountset.HeaderText = ""
        Me.rdgpbxcustomeraccountset.Location = New System.Drawing.Point(0, 0)
        Me.rdgpbxcustomeraccountset.Name = "rdgpbxcustomeraccountset"
        Me.rdgpbxcustomeraccountset.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.rdgpbxcustomeraccountset.Size = New System.Drawing.Size(632, 341)
        Me.rdgpbxcustomeraccountset.TabIndex = 0
        '
        'fnduom
        '
        Me.fnduom.Location = New System.Drawing.Point(107, 57)
        Me.fnduom.MendatroryField = True
        Me.fnduom.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fnduom.MyLinkLable1 = Me.MyLabel13
        Me.fnduom.MyLinkLable2 = Nothing
        Me.fnduom.MyReadOnly = False
        Me.fnduom.Name = "fnduom"
        Me.fnduom.Size = New System.Drawing.Size(101, 19)
        Me.fnduom.TabIndex = 3
        Me.fnduom.Value = ""
        '
        'MyLabel13
        '
        Me.MyLabel13.Location = New System.Drawing.Point(24, 56)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(33, 18)
        Me.MyLabel13.TabIndex = 12
        Me.MyLabel13.Text = "UOM"
        '
        'txtuom
        '
        Me.txtuom.Location = New System.Drawing.Point(213, 57)
        Me.txtuom.MaxLength = 50
        Me.txtuom.MendatroryField = True
        Me.txtuom.MyLinkLable1 = Nothing
        Me.txtuom.MyLinkLable2 = Nothing
        Me.txtuom.Name = "txtuom"
        Me.txtuom.Size = New System.Drawing.Size(219, 20)
        Me.txtuom.TabIndex = 4
        Me.txtuom.TabStop = False
        '
        'txtComments
        '
        Me.txtComments.AutoScroll = True
        Me.txtComments.AutoSize = False
        Me.txtComments.Location = New System.Drawing.Point(105, 125)
        Me.txtComments.MaxLength = 500
        Me.txtComments.MendatroryField = False
        Me.txtComments.Multiline = True
        Me.txtComments.MyLinkLable1 = Me.MyLabel20
        Me.txtComments.MyLinkLable2 = Nothing
        Me.txtComments.Name = "txtComments"
        Me.txtComments.Size = New System.Drawing.Size(327, 85)
        Me.txtComments.TabIndex = 7
        '
        'MyLabel20
        '
        Me.MyLabel20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel20.Location = New System.Drawing.Point(24, 125)
        Me.MyLabel20.Name = "MyLabel20"
        Me.MyLabel20.Size = New System.Drawing.Size(61, 16)
        Me.MyLabel20.TabIndex = 9
        Me.MyLabel20.Text = "Comments"
        '
        'txtCost
        '
        Me.txtCost.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtCost.DecimalPlaces = 2
        Me.txtCost.Location = New System.Drawing.Point(106, 100)
        Me.txtCost.MendatroryField = True
        Me.txtCost.MyLinkLable1 = Me.MyLabel9
        Me.txtCost.MyLinkLable2 = Nothing
        Me.txtCost.Name = "txtCost"
        Me.txtCost.Size = New System.Drawing.Size(101, 20)
        Me.txtCost.TabIndex = 6
        Me.txtCost.Text = "0"
        Me.txtCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtCost.Value = 0
        '
        'MyLabel9
        '
        Me.MyLabel9.Location = New System.Drawing.Point(24, 100)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(71, 18)
        Me.MyLabel9.TabIndex = 10
        Me.MyLabel9.Text = "Cost per unit"
        '
        'txtLastMaitainDate
        '
        Me.txtLastMaitainDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtLastMaitainDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLastMaitainDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtLastMaitainDate.Location = New System.Drawing.Point(305, 80)
        Me.txtLastMaitainDate.MendatroryField = True
        Me.txtLastMaitainDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtLastMaitainDate.MyLinkLable1 = Me.MyLabel5
        Me.txtLastMaitainDate.MyLinkLable2 = Nothing
        Me.txtLastMaitainDate.Name = "txtLastMaitainDate"
        Me.txtLastMaitainDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtLastMaitainDate.Size = New System.Drawing.Size(80, 18)
        Me.txtLastMaitainDate.TabIndex = 5
        Me.txtLastMaitainDate.TabStop = False
        Me.txtLastMaitainDate.Text = "13/06/2011 11:29 AM"
        Me.txtLastMaitainDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'MyLabel5
        '
        Me.MyLabel5.Location = New System.Drawing.Point(214, 81)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(70, 18)
        Me.MyLabel5.TabIndex = 8
        Me.MyLabel5.Text = "Inactive date"
        '
        'cboStatus
        '
        Me.cboStatus.BackColor = System.Drawing.Color.Transparent
        Me.cboStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboStatus.Location = New System.Drawing.Point(107, 79)
        Me.cboStatus.MendatroryField = True
        Me.cboStatus.MyLinkLable1 = Me.MyLabel4
        Me.cboStatus.MyLinkLable2 = Nothing
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(100, 18)
        Me.cboStatus.TabIndex = 4
        '
        'MyLabel4
        '
        Me.MyLabel4.Location = New System.Drawing.Point(24, 78)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(37, 18)
        Me.MyLabel4.TabIndex = 11
        Me.MyLabel4.Text = "Status"
        '
        'fndToolCode
        '
        Me.fndToolCode.Location = New System.Drawing.Point(112, 10)
        Me.fndToolCode.MendatroryField = True
        Me.fndToolCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndToolCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndToolCode.MyLinkLable1 = Me.rdlblAccountsetcode
        Me.fndToolCode.MyLinkLable2 = Nothing
        Me.fndToolCode.MyMaxLength = 30
        Me.fndToolCode.MyReadOnly = False
        Me.fndToolCode.Name = "fndToolCode"
        Me.fndToolCode.Size = New System.Drawing.Size(202, 21)
        Me.fndToolCode.TabIndex = 0
        Me.fndToolCode.Value = ""
        '
        'rdlblAccountsetcode
        '
        Me.rdlblAccountsetcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.rdlblAccountsetcode.Location = New System.Drawing.Point(24, 12)
        Me.rdlblAccountsetcode.Name = "rdlblAccountsetcode"
        Me.rdlblAccountsetcode.Size = New System.Drawing.Size(88, 16)
        Me.rdlblAccountsetcode.TabIndex = 14
        Me.rdlblAccountsetcode.Text = "ToolType Code"
        '
        'rdlbldescription
        '
        Me.rdlbldescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdlbldescription.Location = New System.Drawing.Point(24, 34)
        Me.rdlbldescription.Name = "rdlbldescription"
        Me.rdlbldescription.Size = New System.Drawing.Size(63, 16)
        Me.rdlbldescription.TabIndex = 13
        Me.rdlbldescription.Text = "Description"
        '
        'txtdescription
        '
        Me.txtdescription.Location = New System.Drawing.Point(108, 34)
        Me.txtdescription.MaxLength = 50
        Me.txtdescription.MendatroryField = True
        Me.txtdescription.MyLinkLable1 = Me.rdlbldescription
        Me.txtdescription.MyLinkLable2 = Nothing
        Me.txtdescription.Name = "txtdescription"
        Me.txtdescription.Size = New System.Drawing.Size(324, 20)
        Me.txtdescription.TabIndex = 2
        '
        'rdbtnnew
        '
        Me.rdbtnnew.Image = Global.XpertERPProcessProduction.My.Resources.Resources._new
        Me.rdbtnnew.Location = New System.Drawing.Point(320, 12)
        Me.rdbtnnew.Name = "rdbtnnew"
        Me.rdbtnnew.Size = New System.Drawing.Size(18, 18)
        Me.rdbtnnew.TabIndex = 1
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdgpbxcustomeraccountset)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(632, 380)
        Me.SplitContainer1.SplitterDistance = 341
        Me.SplitContainer1.TabIndex = 6
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(87, 5)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(82, 18)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'rdbtnsave
        '
        Me.rdbtnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdbtnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtnsave.Location = New System.Drawing.Point(3, 5)
        Me.rdbtnsave.Name = "rdbtnsave"
        Me.rdbtnsave.Size = New System.Drawing.Size(82, 18)
        Me.rdbtnsave.TabIndex = 0
        Me.rdbtnsave.Text = "Save"
        '
        'rdbtnclose
        '
        Me.rdbtnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdbtnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtnclose.Location = New System.Drawing.Point(537, 6)
        Me.rdbtnclose.Name = "rdbtnclose"
        Me.rdbtnclose.Size = New System.Drawing.Size(83, 18)
        Me.rdbtnclose.TabIndex = 2
        Me.rdbtnclose.Text = "Close"
        '
        'FrmToolType
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(632, 400)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmToolType"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Tool Type"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdgpbxcustomeraccountset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.rdgpbxcustomeraccountset.ResumeLayout(False)
        Me.rdgpbxcustomeraccountset.PerformLayout()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtuom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtComments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLastMaitainDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlblAccountsetcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlbldescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnnew, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdgpbxcustomeraccountset As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtComments As common.Controls.MyTextBox
    Friend WithEvents MyLabel20 As common.Controls.MyLabel
    Friend WithEvents txtCost As common.MyNumBox
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents txtLastMaitainDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents cboStatus As common.Controls.MyComboBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents fndToolCode As common.UserControls.txtNavigator
    Friend WithEvents rdlblAccountsetcode As common.Controls.MyLabel
    Friend WithEvents rdlbldescription As common.Controls.MyLabel
    Friend WithEvents txtdescription As common.Controls.MyTextBox
    Friend WithEvents rdbtnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents fnduom As common.UserControls.txtFinder
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents txtuom As common.Controls.MyTextBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtnclose As Telerik.WinControls.UI.RadButton
End Class

