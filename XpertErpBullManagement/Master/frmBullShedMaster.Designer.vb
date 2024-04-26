Imports XpertERPEngine
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmBullShedMaster
    'Inherits System.Windows.Forms.Form
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBullShedMaster))
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.cmbArea = New Telerik.WinControls.UI.RadDropDownList()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem()
        Me.lblType = New common.Controls.MyLabel()
        Me.fndCode = New common.UserControls.txtNavigator()
        Me.lblCode = New common.Controls.MyLabel()
        Me.txtname = New common.Controls.MyTextBox()
        Me.lblName = New common.Controls.MyLabel()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.txtValue = New common.Controls.MyTextBox()
        Me.lblValue = New common.Controls.MyLabel()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbArea, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(800, 450)
        Me.SplitContainer1.SplitterDistance = 400
        Me.SplitContainer1.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.txtValue)
        Me.Panel1.Controls.Add(Me.lblValue)
        Me.Panel1.Controls.Add(Me.btnnew)
        Me.Panel1.Controls.Add(Me.cmbArea)
        Me.Panel1.Controls.Add(Me.RadMenu1)
        Me.Panel1.Controls.Add(Me.lblType)
        Me.Panel1.Controls.Add(Me.fndCode)
        Me.Panel1.Controls.Add(Me.lblCode)
        Me.Panel1.Controls.Add(Me.txtname)
        Me.Panel1.Controls.Add(Me.lblName)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(800, 400)
        Me.Panel1.TabIndex = 0
        '
        'btnnew
        '
        Me.btnnew.Image = CType(resources.GetObject("btnnew.Image"), System.Drawing.Image)
        Me.btnnew.Location = New System.Drawing.Point(343, 39)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(17, 21)
        Me.btnnew.TabIndex = 0
        '
        'cmbArea
        '
        Me.cmbArea.AutoCompleteDisplayMember = Nothing
        Me.cmbArea.AutoCompleteValueMember = Nothing
        Me.cmbArea.DropDownAnimationEnabled = True
        RadListDataItem1.Text = "m2"
        RadListDataItem2.Text = "ft2"
        Me.cmbArea.Items.Add(RadListDataItem1)
        Me.cmbArea.Items.Add(RadListDataItem2)
        Me.cmbArea.Location = New System.Drawing.Point(92, 110)
        Me.cmbArea.Name = "cmbArea"
        Me.cmbArea.Size = New System.Drawing.Size(139, 20)
        Me.cmbArea.TabIndex = 447
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(800, 20)
        Me.RadMenu1.TabIndex = 446
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2, Me.RadMenuItem4})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        Me.RadMenuItem1.UseCompatibleTextRendering = False
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Export"
        Me.RadMenuItem2.UseCompatibleTextRendering = False
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Import"
        Me.RadMenuItem4.UseCompatibleTextRendering = False
        '
        'lblType
        '
        Me.lblType.FieldName = Nothing
        Me.lblType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblType.Location = New System.Drawing.Point(25, 112)
        Me.lblType.Name = "lblType"
        Me.lblType.Size = New System.Drawing.Size(30, 16)
        Me.lblType.TabIndex = 65
        Me.lblType.Text = "Area"
        '
        'fndCode
        '
        Me.fndCode.FieldName = Nothing
        Me.fndCode.Location = New System.Drawing.Point(92, 39)
        Me.fndCode.MendatroryField = True
        Me.fndCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndCode.MyLinkLable1 = Me.lblCode
        Me.fndCode.MyLinkLable2 = Nothing
        Me.fndCode.MyMaxLength = 30
        Me.fndCode.MyReadOnly = False
        Me.fndCode.Name = "fndCode"
        Me.fndCode.Size = New System.Drawing.Size(253, 21)
        Me.fndCode.TabIndex = 61
        Me.fndCode.Value = ""
        '
        'lblCode
        '
        Me.lblCode.FieldName = Nothing
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblCode.Location = New System.Drawing.Point(24, 41)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(33, 16)
        Me.lblCode.TabIndex = 64
        Me.lblCode.Text = "Code"
        '
        'txtname
        '
        Me.txtname.CalculationExpression = Nothing
        Me.txtname.FieldCode = Nothing
        Me.txtname.FieldDesc = Nothing
        Me.txtname.FieldMaxLength = 0
        Me.txtname.FieldName = Nothing
        Me.txtname.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtname.isCalculatedField = False
        Me.txtname.IsSourceFromTable = False
        Me.txtname.IsSourceFromValueList = False
        Me.txtname.IsUnique = False
        Me.txtname.Location = New System.Drawing.Point(92, 64)
        Me.txtname.MaxLength = 200
        Me.txtname.MendatroryField = True
        Me.txtname.MyLinkLable1 = Me.lblName
        Me.txtname.MyLinkLable2 = Nothing
        Me.txtname.Name = "txtname"
        Me.txtname.ReferenceFieldDesc = Nothing
        Me.txtname.ReferenceFieldName = Nothing
        Me.txtname.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtname.RootElement.StretchVertically = True
        Me.txtname.Size = New System.Drawing.Size(267, 21)
        Me.txtname.TabIndex = 62
        '
        'lblName
        '
        Me.lblName.FieldName = Nothing
        Me.lblName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblName.Location = New System.Drawing.Point(24, 66)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(36, 16)
        Me.lblName.TabIndex = 63
        Me.lblName.Text = "Name"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(709, 16)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 11
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(96, 16)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 10
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(24, 16)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 9
        Me.btnsave.Text = "Save"
        '
        'txtValue
        '
        Me.txtValue.CalculationExpression = Nothing
        Me.txtValue.FieldCode = Nothing
        Me.txtValue.FieldDesc = Nothing
        Me.txtValue.FieldMaxLength = 0
        Me.txtValue.FieldName = Nothing
        Me.txtValue.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtValue.isCalculatedField = False
        Me.txtValue.IsSourceFromTable = False
        Me.txtValue.IsSourceFromValueList = False
        Me.txtValue.IsUnique = False
        Me.txtValue.Location = New System.Drawing.Point(92, 87)
        Me.txtValue.MaxLength = 200
        Me.txtValue.MendatroryField = True
        Me.txtValue.MyLinkLable1 = Me.lblValue
        Me.txtValue.MyLinkLable2 = Nothing
        Me.txtValue.Name = "txtValue"
        Me.txtValue.ReferenceFieldDesc = Nothing
        Me.txtValue.ReferenceFieldName = Nothing
        Me.txtValue.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtValue.RootElement.StretchVertically = True
        Me.txtValue.Size = New System.Drawing.Size(139, 21)
        Me.txtValue.TabIndex = 448
        '
        'lblValue
        '
        Me.lblValue.FieldName = Nothing
        Me.lblValue.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblValue.Location = New System.Drawing.Point(25, 89)
        Me.lblValue.Name = "lblValue"
        Me.lblValue.Size = New System.Drawing.Size(35, 16)
        Me.lblValue.TabIndex = 449
        Me.lblValue.Text = "Value"
        '
        'frmBullShedMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmBullShedMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "frmBullShedMaster"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbArea, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents Panel1 As Panel
    Friend WithEvents cmbArea As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem4 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents lblType As common.Controls.MyLabel
    Friend WithEvents fndCode As common.UserControls.txtNavigator
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents txtname As common.Controls.MyTextBox
    Friend WithEvents lblName As common.Controls.MyLabel
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtValue As common.Controls.MyTextBox
    Friend WithEvents lblValue As common.Controls.MyLabel
End Class
