Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmItemChargeCategoryMaster
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
        Me.lblchrcat = New common.Controls.MyLabel()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.lblchrdesc = New common.Controls.MyLabel()
        Me.txtchrdesc = New common.Controls.MyTextBox()
        Me.fndchrcat = New common.UserControls.txtNavigator()
        Me.lblglacc = New common.Controls.MyLabel()
        Me.lblGLDesc = New common.Controls.MyLabel()
        Me.txtGLCode = New common.UserControls.txtFinder()
        Me.RadMenufile = New Telerik.WinControls.UI.RadMenuItem()
        Me.Rdmenuexport = New Telerik.WinControls.UI.RadMenuItem()
        Me.Rdmenuimport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rbmenuexit = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenu()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        CType(Me.lblchrcat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblchrdesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtchrdesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblglacc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGLDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblchrcat
        '
        Me.lblchrcat.FieldName = Nothing
        Me.lblchrcat.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblchrcat.Location = New System.Drawing.Point(6, 10)
        Me.lblchrcat.Name = "lblchrcat"
        Me.lblchrcat.Size = New System.Drawing.Size(92, 16)
        Me.lblchrcat.TabIndex = 7
        Me.lblchrcat.Text = "Charge Category"
        '
        'btnnew
        '
        Me.btnnew.Image = Global.XpertERPService.My.Resources.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(410, 10)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(15, 20)
        Me.btnnew.TabIndex = 1
        '
        'lblchrdesc
        '
        Me.lblchrdesc.FieldName = Nothing
        Me.lblchrdesc.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblchrdesc.Location = New System.Drawing.Point(6, 35)
        Me.lblchrdesc.Name = "lblchrdesc"
        Me.lblchrdesc.Size = New System.Drawing.Size(63, 16)
        Me.lblchrdesc.TabIndex = 6
        Me.lblchrdesc.Text = "Description"
        '
        'txtchrdesc
        '
        Me.txtchrdesc.CalculationExpression = Nothing
        Me.txtchrdesc.FieldCode = Nothing
        Me.txtchrdesc.FieldDesc = Nothing
        Me.txtchrdesc.FieldMaxLength = 0
        Me.txtchrdesc.FieldName = Nothing
        Me.txtchrdesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtchrdesc.isCalculatedField = False
        Me.txtchrdesc.IsSourceFromTable = False
        Me.txtchrdesc.IsSourceFromValueList = False
        Me.txtchrdesc.IsUnique = False
        Me.txtchrdesc.Location = New System.Drawing.Point(104, 35)
        Me.txtchrdesc.MaxLength = 50
        Me.txtchrdesc.MendatroryField = False
        Me.txtchrdesc.MyLinkLable1 = Me.lblchrdesc
        Me.txtchrdesc.MyLinkLable2 = Nothing
        Me.txtchrdesc.Name = "txtchrdesc"
        Me.txtchrdesc.ReferenceFieldDesc = Nothing
        Me.txtchrdesc.ReferenceFieldName = Nothing
        Me.txtchrdesc.ReferenceTableName = Nothing
        Me.txtchrdesc.Size = New System.Drawing.Size(321, 18)
        Me.txtchrdesc.TabIndex = 2
        '
        'fndchrcat
        '
        Me.fndchrcat.FieldName = Nothing
        Me.fndchrcat.Location = New System.Drawing.Point(104, 8)
        Me.fndchrcat.MendatroryField = True
        Me.fndchrcat.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndchrcat.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndchrcat.MyLinkLable1 = Me.lblchrcat
        Me.fndchrcat.MyLinkLable2 = Nothing
        Me.fndchrcat.MyMaxLength = 30
        Me.fndchrcat.MyReadOnly = False
        Me.fndchrcat.Name = "fndchrcat"
        Me.fndchrcat.Size = New System.Drawing.Size(300, 21)
        Me.fndchrcat.TabIndex = 0
        Me.fndchrcat.Value = ""
        '
        'lblglacc
        '
        Me.lblglacc.FieldName = Nothing
        Me.lblglacc.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblglacc.Location = New System.Drawing.Point(6, 57)
        Me.lblglacc.Name = "lblglacc"
        Me.lblglacc.Size = New System.Drawing.Size(66, 16)
        Me.lblglacc.TabIndex = 5
        Me.lblglacc.Text = "GL-Account"
        '
        'lblGLDesc
        '
        Me.lblGLDesc.AutoSize = False
        Me.lblGLDesc.BorderVisible = True
        Me.lblGLDesc.FieldName = Nothing
        Me.lblGLDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGLDesc.Location = New System.Drawing.Point(104, 81)
        Me.lblGLDesc.Name = "lblGLDesc"
        Me.lblGLDesc.Size = New System.Drawing.Size(321, 18)
        Me.lblGLDesc.TabIndex = 8
        Me.lblGLDesc.TextWrap = False
        '
        'txtGLCode
        '
        Me.txtGLCode.CalculationExpression = Nothing
        Me.txtGLCode.FieldCode = Nothing
        Me.txtGLCode.FieldDesc = Nothing
        Me.txtGLCode.FieldMaxLength = 0
        Me.txtGLCode.FieldName = Nothing
        Me.txtGLCode.isCalculatedField = False
        Me.txtGLCode.IsSourceFromTable = False
        Me.txtGLCode.IsSourceFromValueList = False
        Me.txtGLCode.IsUnique = False
        Me.txtGLCode.Location = New System.Drawing.Point(104, 56)
        Me.txtGLCode.MendatroryField = True
        Me.txtGLCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGLCode.MyLinkLable1 = Me.lblglacc
        Me.txtGLCode.MyLinkLable2 = Me.lblGLDesc
        Me.txtGLCode.MyReadOnly = False
        Me.txtGLCode.MyShowMasterFormButton = False
        Me.txtGLCode.Name = "txtGLCode"
        Me.txtGLCode.ReferenceFieldDesc = Nothing
        Me.txtGLCode.ReferenceFieldName = Nothing
        Me.txtGLCode.ReferenceTableName = Nothing
        Me.txtGLCode.Size = New System.Drawing.Size(154, 19)
        Me.txtGLCode.TabIndex = 3
        Me.txtGLCode.Value = ""
        '
        'RadMenufile
        '
        Me.RadMenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.Rdmenuexport, Me.Rdmenuimport, Me.rbmenuexit})
        Me.RadMenufile.Name = "RadMenufile"
        Me.RadMenufile.Text = "File"
        '
        'Rdmenuexport
        '
        Me.Rdmenuexport.Name = "Rdmenuexport"
        Me.Rdmenuexport.Text = "Export"
        '
        'Rdmenuimport
        '
        Me.Rdmenuimport.Name = "Rdmenuimport"
        Me.Rdmenuimport.Text = "Import"
        '
        'rbmenuexit
        '
        Me.rbmenuexit.Name = "rbmenuexit"
        Me.rbmenuexit.Text = "Exit"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(376, 9)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(70, 20)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(90, 9)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(70, 20)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(12, 9)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(70, 20)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'rdmenufile
        '
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenufile})
        Me.rdmenufile.Location = New System.Drawing.Point(0, 0)
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Size = New System.Drawing.Size(456, 20)
        Me.rdmenufile.TabIndex = 1
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
        Me.SplitContainer1.Panel1.Padding = New System.Windows.Forms.Padding(3)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.SplitContainer1.Size = New System.Drawing.Size(456, 195)
        Me.SplitContainer1.SplitterDistance = 152
        Me.SplitContainer1.TabIndex = 0
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.lblchrcat)
        Me.RadGroupBox1.Controls.Add(Me.lblGLDesc)
        Me.RadGroupBox1.Controls.Add(Me.fndchrcat)
        Me.RadGroupBox1.Controls.Add(Me.txtchrdesc)
        Me.RadGroupBox1.Controls.Add(Me.txtGLCode)
        Me.RadGroupBox1.Controls.Add(Me.btnnew)
        Me.RadGroupBox1.Controls.Add(Me.lblchrdesc)
        Me.RadGroupBox1.Controls.Add(Me.lblglacc)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(6, 11)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(440, 110)
        Me.RadGroupBox1.TabIndex = 111
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(6, 82)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(81, 16)
        Me.MyLabel1.TabIndex = 4
        Me.MyLabel1.Text = "GL Description"
        '
        'frmItemChargeCategoryMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(456, 215)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.rdmenufile)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Name = "frmItemChargeCategoryMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Item Charge Franchise Mapping Master"
        CType(Me.lblchrcat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblchrdesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtchrdesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblglacc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGLDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblchrcat As common.Controls.MyLabel
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblchrdesc As common.Controls.MyLabel
    Friend WithEvents txtchrdesc As common.Controls.MyTextBox
    Friend WithEvents fndchrcat As common.UserControls.txtNavigator
    Friend WithEvents lblglacc As common.Controls.MyLabel
    Friend WithEvents lblGLDesc As common.Controls.MyLabel
    Friend WithEvents txtGLCode As common.UserControls.txtFinder
    Friend WithEvents RadMenufile As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenu
    Friend WithEvents Rdmenuexport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Rdmenuimport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rbmenuexit As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
End Class

