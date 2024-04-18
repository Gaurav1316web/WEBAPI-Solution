Imports XpertERPEngine
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmBullTestParameter
    'Inherits System.Windows.Forms.Form
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
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.txtPeriodcity = New System.Windows.Forms.MaskedTextBox()
        Me.lblPeriodcity = New common.Controls.MyLabel()
        Me.txtType = New Telerik.WinControls.UI.RadDropDownList()
        Me.lblType = New common.Controls.MyLabel()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.lblCode = New common.Controls.MyLabel()
        Me.txtname = New common.Controls.MyTextBox()
        Me.lblName = New common.Controls.MyLabel()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPeriodcity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnnew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtPeriodcity)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblPeriodcity)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtname)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblName)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(592, 270)
        Me.SplitContainer1.SplitterDistance = 227
        Me.SplitContainer1.TabIndex = 0
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(592, 20)
        Me.RadMenu1.TabIndex = 445
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
        'btnnew
        '
        Me.btnnew.Image = Global.XpertERPFixedAssets.My.Resources.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(333, 40)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(15, 22)
        Me.btnnew.TabIndex = 444
        '
        'txtPeriodcity
        '
        Me.txtPeriodcity.Location = New System.Drawing.Point(80, 112)
        Me.txtPeriodcity.Name = "txtPeriodcity"
        Me.txtPeriodcity.Size = New System.Drawing.Size(139, 20)
        Me.txtPeriodcity.TabIndex = 443
        '
        'lblPeriodcity
        '
        Me.lblPeriodcity.FieldName = Nothing
        Me.lblPeriodcity.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPeriodcity.Location = New System.Drawing.Point(12, 115)
        Me.lblPeriodcity.Name = "lblPeriodcity"
        Me.lblPeriodcity.Size = New System.Drawing.Size(56, 16)
        Me.lblPeriodcity.TabIndex = 441
        Me.lblPeriodcity.Text = "Periodcity"
        '
        'txtType
        '
        Me.txtType.AutoCompleteDisplayMember = Nothing
        Me.txtType.AutoCompleteValueMember = Nothing
        Me.txtType.DropDownAnimationEnabled = True
        RadListDataItem1.Text = "Boolean"
        RadListDataItem2.Text = "Range"
        RadListDataItem3.Text = "Alpha Numeric"
        Me.txtType.Items.Add(RadListDataItem1)
        Me.txtType.Items.Add(RadListDataItem2)
        Me.txtType.Items.Add(RadListDataItem3)
        Me.txtType.Location = New System.Drawing.Point(80, 88)
        Me.txtType.Name = "txtType"
        Me.txtType.Size = New System.Drawing.Size(139, 20)
        Me.txtType.TabIndex = 439
        '
        'lblType
        '
        Me.lblType.FieldName = Nothing
        Me.lblType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblType.Location = New System.Drawing.Point(12, 92)
        Me.lblType.Name = "lblType"
        Me.lblType.Size = New System.Drawing.Size(31, 16)
        Me.lblType.TabIndex = 60
        Me.lblType.Text = "Type"
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(80, 40)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 30
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(253, 21)
        Me.txtCode.TabIndex = 56
        Me.txtCode.Value = ""
        '
        'lblCode
        '
        Me.lblCode.FieldName = Nothing
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCode.Location = New System.Drawing.Point(12, 41)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(33, 16)
        Me.lblCode.TabIndex = 59
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
        Me.txtname.Location = New System.Drawing.Point(80, 65)
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
        Me.txtname.Size = New System.Drawing.Size(253, 21)
        Me.txtname.TabIndex = 57
        '
        'lblName
        '
        Me.lblName.FieldName = Nothing
        Me.lblName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblName.Location = New System.Drawing.Point(12, 66)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(36, 16)
        Me.lblName.TabIndex = 58
        Me.lblName.Text = "Name"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(495, 9)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 9
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(84, 9)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 8
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(12, 9)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 7
        Me.btnsave.Text = "Save"
        '
        'frmBullTestParameter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(592, 270)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmBullTestParameter"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "frmBullTaxParameter"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPeriodcity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents txtname As common.Controls.MyTextBox
    Friend WithEvents lblName As common.Controls.MyLabel
    Friend WithEvents lblType As common.Controls.MyLabel
    Friend WithEvents txtType As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblPeriodcity As common.Controls.MyLabel
    Friend WithEvents txtPeriodcity As MaskedTextBox
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem4 As Telerik.WinControls.UI.RadMenuItem
End Class
