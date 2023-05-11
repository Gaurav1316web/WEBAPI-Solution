<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frmpacktype
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
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.btnsave = New Telerik.WinControls.UI.RadButton
        Me.btndelete = New Telerik.WinControls.UI.RadButton
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.RadLabel3 = New common.Controls.MyLabel
        Me.RadLabel4 = New common.Controls.MyLabel
        Me.ddlfathercode = New common.Controls.MyComboBox
        Me.ddlmothercode = New common.Controls.MyComboBox
        Me.ddlfinishedgood = New common.Controls.MyComboBox
        Me.btnreset = New Telerik.WinControls.UI.RadButton
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem
        Me.mnimport = New Telerik.WinControls.UI.RadMenuItem
        Me.mnexport = New Telerik.WinControls.UI.RadMenuItem
        Me.mnclose = New Telerik.WinControls.UI.RadMenuItem
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.fndclasstype = New common.UserControls.txtFinder
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlfathercode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlmothercode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlfinishedgood, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadLabel1
        '
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(13, 23)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel1.TabIndex = 3
        Me.RadLabel1.Text = "Class Type"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(3, 3)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(68, 18)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(77, 3)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(68, 18)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(302, 3)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(68, 18)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'RadLabel2
        '
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(13, 47)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(80, 16)
        Me.RadLabel2.TabIndex = 8
        Me.RadLabel2.Text = "Finished Good"
        '
        'RadLabel3
        '
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(13, 71)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(40, 16)
        Me.RadLabel3.TabIndex = 11
        Me.RadLabel3.Text = "Parent"
        '
        'RadLabel4
        '
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(13, 95)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(32, 16)
        Me.RadLabel4.TabIndex = 12
        Me.RadLabel4.Text = "Child"
        '
        'ddlfathercode
        '
        Me.ddlfathercode.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlfathercode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddlfathercode.Location = New System.Drawing.Point(103, 71)
        Me.ddlfathercode.MendatroryField = False
        Me.ddlfathercode.MyLinkLable1 = Me.RadLabel3
        Me.ddlfathercode.MyLinkLable2 = Nothing
        Me.ddlfathercode.Name = "ddlfathercode"
        Me.ddlfathercode.Size = New System.Drawing.Size(224, 18)
        Me.ddlfathercode.TabIndex = 3
        Me.ddlfathercode.Text = "Select"
        '
        'ddlmothercode
        '
        Me.ddlmothercode.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlmothercode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddlmothercode.Location = New System.Drawing.Point(103, 95)
        Me.ddlmothercode.MendatroryField = False
        Me.ddlmothercode.MyLinkLable1 = Me.RadLabel4
        Me.ddlmothercode.MyLinkLable2 = Nothing
        Me.ddlmothercode.Name = "ddlmothercode"
        Me.ddlmothercode.Size = New System.Drawing.Size(224, 18)
        Me.ddlmothercode.TabIndex = 4
        Me.ddlmothercode.Text = "Select"
        '
        'ddlfinishedgood
        '
        Me.ddlfinishedgood.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlfinishedgood.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddlfinishedgood.Location = New System.Drawing.Point(103, 47)
        Me.ddlfinishedgood.MendatroryField = False
        Me.ddlfinishedgood.MyLinkLable1 = Me.RadLabel2
        Me.ddlfinishedgood.MyLinkLable2 = Nothing
        Me.ddlfinishedgood.Name = "ddlfinishedgood"
        Me.ddlfinishedgood.Size = New System.Drawing.Size(224, 18)
        Me.ddlfinishedgood.TabIndex = 2
        Me.ddlfinishedgood.Text = "Select"
        '
        'btnreset
        '
        Me.btnreset.BackgroundImage = Global.ERP.My.Resources.Resources._new
        Me.btnreset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnreset.Image = Global.ERP.My.Resources.Resources._new
        Me.btnreset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnreset.Location = New System.Drawing.Point(327, 21)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(22, 20)
        Me.btnreset.TabIndex = 1
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(380, 20)
        Me.RadMenu1.TabIndex = 14
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "mnfile"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.mnimport, Me.mnexport, Me.mnclose})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        Me.RadMenuItem1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'mnimport
        '
        Me.mnimport.AccessibleDescription = "Import"
        Me.mnimport.AccessibleName = "Import"
        Me.mnimport.Name = "mnimport"
        Me.mnimport.Text = "Import"
        Me.mnimport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'mnexport
        '
        Me.mnexport.AccessibleDescription = "Export"
        Me.mnexport.AccessibleName = "Export"
        Me.mnexport.Name = "mnexport"
        Me.mnexport.Text = "Export"
        Me.mnexport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'mnclose
        '
        Me.mnclose.AccessibleDescription = "Close"
        Me.mnclose.AccessibleName = "Close"
        Me.mnclose.Name = "mnclose"
        Me.mnclose.Text = "Close"
        Me.mnclose.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.fndclasstype)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.Controls.Add(Me.btnreset)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox1.Controls.Add(Me.ddlmothercode)
        Me.RadGroupBox1.Controls.Add(Me.ddlfinishedgood)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel4)
        Me.RadGroupBox1.Controls.Add(Me.ddlfathercode)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel3)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(6, 3)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(364, 136)
        Me.RadGroupBox1.TabIndex = 0
        '
        'fndclasstype
        '
        Me.fndclasstype.Location = New System.Drawing.Point(103, 22)
        Me.fndclasstype.MendatroryField = True
        Me.fndclasstype.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndclasstype.MyLinkLable1 = Me.RadLabel1
        Me.fndclasstype.MyLinkLable2 = Nothing
        Me.fndclasstype.MyReadOnly = False
        Me.fndclasstype.Name = "fndclasstype"
        Me.fndclasstype.Size = New System.Drawing.Size(224, 18)
        Me.fndclasstype.TabIndex = 0
        Me.fndclasstype.Value = ""
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(380, 179)
        Me.SplitContainer1.SplitterDistance = 143
        Me.SplitContainer1.TabIndex = 15
        '
        'Frmpacktype
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(380, 199)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "Frmpacktype"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Pack Type"
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlfathercode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlmothercode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlfinishedgood, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents ddlfathercode As common.Controls.MyComboBox
    Friend WithEvents ddlmothercode As common.Controls.MyComboBox
    Friend WithEvents ddlfinishedgood As common.Controls.MyComboBox
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnimport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnexport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnclose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents fndclasstype As common.UserControls.txtFinder
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

