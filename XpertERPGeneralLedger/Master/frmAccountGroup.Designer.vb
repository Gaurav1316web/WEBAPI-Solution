<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAccountGroup
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
        Me.components = New System.ComponentModel.Container()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.accgrpim = New Telerik.WinControls.UI.RadMenuItem()
        Me.accgpex = New Telerik.WinControls.UI.RadMenuItem()
        Me.accgpclose = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.lbldes = New common.Controls.MyLabel()
        Me.txtdes = New common.Controls.MyTextBox()
        Me.lblaccgp = New common.Controls.MyLabel()
        Me.ToolTipaccgp = New System.Windows.Forms.ToolTip(Me.components)
        Me.gbaccgroup = New Telerik.WinControls.UI.RadGroupBox()
        Me.lblAccMain = New common.Controls.MyLabel()
        Me.txtAccMainGrp = New common.UserControls.txtFinder()
        Me.RadLabel23 = New common.Controls.MyLabel()
        Me.fndaccgp = New common.UserControls.txtNavigator()
        Me.btnChangeOrder = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbldes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblaccgp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gbaccgroup, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbaccgroup.SuspendLayout()
        CType(Me.lblAccMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnChangeOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(522, 20)
        Me.RadMenu1.TabIndex = 0
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.accgrpim, Me.accgpex, Me.accgpclose})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'accgrpim
        '
        Me.accgrpim.Name = "accgrpim"
        Me.accgrpim.Text = "Import"
        '
        'accgpex
        '
        Me.accgpex.Name = "accgpex"
        Me.accgpex.Text = "Export"
        '
        'accgpclose
        '
        Me.accgpclose.Name = "accgpclose"
        Me.accgpclose.Text = "Close"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(439, 5)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 6
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(78, 5)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 4
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(9, 5)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 3
        Me.btnsave.Text = "Save"
        '
        'btnnew
        '
        Me.btnnew.Image = Global.XpertERPGeneralLedger.My.Resources.Resources._new
        Me.btnnew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnnew.Location = New System.Drawing.Point(316, 20)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(20, 21)
        Me.btnnew.TabIndex = 1
        '
        'lbldes
        '
        Me.lbldes.FieldName = Nothing
        Me.lbldes.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldes.Location = New System.Drawing.Point(9, 43)
        Me.lbldes.Name = "lbldes"
        Me.lbldes.Size = New System.Drawing.Size(63, 16)
        Me.lbldes.TabIndex = 36
        Me.lbldes.Text = "Description"
        '
        'txtdes
        '
        Me.txtdes.CalculationExpression = Nothing
        Me.txtdes.FieldCode = Nothing
        Me.txtdes.FieldDesc = Nothing
        Me.txtdes.FieldMaxLength = 0
        Me.txtdes.FieldName = Nothing
        Me.txtdes.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdes.isCalculatedField = False
        Me.txtdes.IsSourceFromTable = False
        Me.txtdes.IsSourceFromValueList = False
        Me.txtdes.IsUnique = False
        Me.txtdes.Location = New System.Drawing.Point(129, 43)
        Me.txtdes.MaxLength = 49
        Me.txtdes.MendatroryField = False
        Me.txtdes.MyLinkLable1 = Me.lbldes
        Me.txtdes.MyLinkLable2 = Nothing
        Me.txtdes.Name = "txtdes"
        Me.txtdes.ReferenceFieldDesc = Nothing
        Me.txtdes.ReferenceFieldName = Nothing
        Me.txtdes.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtdes.RootElement.StretchVertically = True
        Me.txtdes.Size = New System.Drawing.Size(377, 20)
        Me.txtdes.TabIndex = 2
        '
        'lblaccgp
        '
        Me.lblaccgp.FieldName = Nothing
        Me.lblaccgp.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblaccgp.Location = New System.Drawing.Point(9, 22)
        Me.lblaccgp.Name = "lblaccgp"
        Me.lblaccgp.Size = New System.Drawing.Size(82, 16)
        Me.lblaccgp.TabIndex = 37
        Me.lblaccgp.Text = "Account Group"
        '
        'gbaccgroup
        '
        Me.gbaccgroup.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbaccgroup.Controls.Add(Me.lblAccMain)
        Me.gbaccgroup.Controls.Add(Me.txtAccMainGrp)
        Me.gbaccgroup.Controls.Add(Me.RadLabel23)
        Me.gbaccgroup.Controls.Add(Me.fndaccgp)
        Me.gbaccgroup.Controls.Add(Me.lblaccgp)
        Me.gbaccgroup.Controls.Add(Me.txtdes)
        Me.gbaccgroup.Controls.Add(Me.lbldes)
        Me.gbaccgroup.Controls.Add(Me.btnnew)
        Me.gbaccgroup.HeaderText = ""
        Me.gbaccgroup.Location = New System.Drawing.Point(4, 4)
        Me.gbaccgroup.Name = "gbaccgroup"
        Me.gbaccgroup.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.gbaccgroup.Size = New System.Drawing.Size(515, 94)
        Me.gbaccgroup.TabIndex = 0
        '
        'lblAccMain
        '
        Me.lblAccMain.AutoSize = False
        Me.lblAccMain.BorderVisible = True
        Me.lblAccMain.FieldName = Nothing
        Me.lblAccMain.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAccMain.Location = New System.Drawing.Point(276, 66)
        Me.lblAccMain.Name = "lblAccMain"
        Me.lblAccMain.Size = New System.Drawing.Size(230, 18)
        Me.lblAccMain.TabIndex = 42
        Me.lblAccMain.TextWrap = False
        '
        'txtAccMainGrp
        '
        Me.txtAccMainGrp.CalculationExpression = Nothing
        Me.txtAccMainGrp.FieldCode = Nothing
        Me.txtAccMainGrp.FieldDesc = Nothing
        Me.txtAccMainGrp.FieldMaxLength = 0
        Me.txtAccMainGrp.FieldName = Nothing
        Me.txtAccMainGrp.isCalculatedField = False
        Me.txtAccMainGrp.IsSourceFromTable = False
        Me.txtAccMainGrp.IsSourceFromValueList = False
        Me.txtAccMainGrp.IsUnique = False
        Me.txtAccMainGrp.Location = New System.Drawing.Point(129, 65)
        Me.txtAccMainGrp.MendatroryField = True
        Me.txtAccMainGrp.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAccMainGrp.MyLinkLable1 = Nothing
        Me.txtAccMainGrp.MyLinkLable2 = Nothing
        Me.txtAccMainGrp.MyReadOnly = False
        Me.txtAccMainGrp.MyShowMasterFormButton = False
        Me.txtAccMainGrp.Name = "txtAccMainGrp"
        Me.txtAccMainGrp.ReferenceFieldDesc = Nothing
        Me.txtAccMainGrp.ReferenceFieldName = Nothing
        Me.txtAccMainGrp.ReferenceTableName = Nothing
        Me.txtAccMainGrp.Size = New System.Drawing.Size(143, 19)
        Me.txtAccMainGrp.TabIndex = 4
        Me.txtAccMainGrp.Value = ""
        '
        'RadLabel23
        '
        Me.RadLabel23.FieldName = Nothing
        Me.RadLabel23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel23.Location = New System.Drawing.Point(9, 67)
        Me.RadLabel23.Name = "RadLabel23"
        Me.RadLabel23.Size = New System.Drawing.Size(109, 16)
        Me.RadLabel23.TabIndex = 41
        Me.RadLabel23.Text = "Account Main Group"
        '
        'fndaccgp
        '
        Me.fndaccgp.FieldName = Nothing
        Me.fndaccgp.Location = New System.Drawing.Point(129, 20)
        Me.fndaccgp.MendatroryField = True
        Me.fndaccgp.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndaccgp.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndaccgp.MyLinkLable1 = Me.lblaccgp
        Me.fndaccgp.MyLinkLable2 = Nothing
        Me.fndaccgp.MyMaxLength = 30
        Me.fndaccgp.MyReadOnly = False
        Me.fndaccgp.Name = "fndaccgp"
        Me.fndaccgp.Size = New System.Drawing.Size(187, 21)
        Me.fndaccgp.TabIndex = 1
        Me.fndaccgp.TabStop = False
        Me.fndaccgp.Value = ""
        '
        'btnChangeOrder
        '
        Me.btnChangeOrder.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnChangeOrder.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnChangeOrder.Location = New System.Drawing.Point(147, 5)
        Me.btnChangeOrder.Name = "btnChangeOrder"
        Me.btnChangeOrder.Size = New System.Drawing.Size(139, 18)
        Me.btnChangeOrder.TabIndex = 5
        Me.btnChangeOrder.Text = "Change Print Order"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.gbaccgroup)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnChangeOrder)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(522, 142)
        Me.SplitContainer1.SplitterDistance = 109
        Me.SplitContainer1.TabIndex = 1
        '
        'frmAccountGroup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(522, 162)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmAccountGroup"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Account Group"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbldes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblaccgp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gbaccgroup, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbaccgroup.ResumeLayout(False)
        Me.gbaccgroup.PerformLayout()
        CType(Me.lblAccMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnChangeOrder, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents accgrpim As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents accgpex As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents accgpclose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtdes As common.Controls.MyTextBox
    Friend WithEvents ToolTipaccgp As System.Windows.Forms.ToolTip
    Friend WithEvents gbaccgroup As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnChangeOrder As Telerik.WinControls.UI.RadButton
    Friend WithEvents lbldes As common.Controls.MyLabel
    Friend WithEvents lblaccgp As common.Controls.MyLabel
    Friend WithEvents fndaccgp As common.UserControls.txtNavigator
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtAccMainGrp As common.UserControls.txtFinder
    Friend WithEvents RadLabel23 As common.Controls.MyLabel
    Friend WithEvents lblAccMain As common.Controls.MyLabel
End Class

