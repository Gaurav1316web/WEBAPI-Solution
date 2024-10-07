<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCustomerEmployeeMapping
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.fndcustomer = New common.UserControls.txtNavigator()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtempdesc = New common.Controls.MyTextBox()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.txtcustdes = New common.Controls.MyTextBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.fndEmployee = New common.UserControls.txtFinder()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtempdesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcustdes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(630, 140)
        Me.SplitContainer1.SplitterDistance = 99
        Me.SplitContainer1.TabIndex = 0
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(630, 20)
        Me.RadMenu1.TabIndex = 324
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmImport, Me.rmExport})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        Me.RadMenuItem3.UseCompatibleTextRendering = False
        '
        'rmImport
        '
        Me.rmImport.Name = "rmImport"
        Me.rmImport.Text = "Import"
        Me.rmImport.UseCompatibleTextRendering = False
        '
        'rmExport
        '
        Me.rmExport.Name = "rmExport"
        Me.rmExport.Text = "Export"
        Me.rmExport.UseCompatibleTextRendering = False
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.fndcustomer)
        Me.RadGroupBox2.Controls.Add(Me.txtempdesc)
        Me.RadGroupBox2.Controls.Add(Me.btnnew)
        Me.RadGroupBox2.Controls.Add(Me.txtcustdes)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox2.Controls.Add(Me.fndEmployee)
        Me.RadGroupBox2.HeaderText = ""
        Me.RadGroupBox2.Location = New System.Drawing.Point(4, 32)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(622, 66)
        Me.RadGroupBox2.TabIndex = 2
        '
        'fndcustomer
        '
        Me.fndcustomer.FieldName = Nothing
        Me.fndcustomer.Location = New System.Drawing.Point(94, 11)
        Me.fndcustomer.MendatroryField = True
        Me.fndcustomer.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndcustomer.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndcustomer.MyLinkLable1 = Me.MyLabel1
        Me.fndcustomer.MyLinkLable2 = Nothing
        Me.fndcustomer.MyMaxLength = 32767
        Me.fndcustomer.MyReadOnly = False
        Me.fndcustomer.Name = "fndcustomer"
        Me.fndcustomer.Size = New System.Drawing.Size(202, 21)
        Me.fndcustomer.TabIndex = 0
        Me.fndcustomer.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.MyLabel1.Location = New System.Drawing.Point(4, 13)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(94, 16)
        Me.MyLabel1.TabIndex = 38
        Me.MyLabel1.Text = "Customer  Code"
        '
        'txtempdesc
        '
        Me.txtempdesc.CalculationExpression = Nothing
        Me.txtempdesc.FieldCode = Nothing
        Me.txtempdesc.FieldDesc = Nothing
        Me.txtempdesc.FieldMaxLength = 0
        Me.txtempdesc.FieldName = Nothing
        Me.txtempdesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtempdesc.isCalculatedField = False
        Me.txtempdesc.IsSourceFromTable = False
        Me.txtempdesc.IsSourceFromValueList = False
        Me.txtempdesc.IsUnique = False
        Me.txtempdesc.Location = New System.Drawing.Point(312, 38)
        Me.txtempdesc.MaxLength = 49
        Me.txtempdesc.MendatroryField = False
        Me.txtempdesc.MyLinkLable1 = Nothing
        Me.txtempdesc.MyLinkLable2 = Nothing
        Me.txtempdesc.Name = "txtempdesc"
        Me.txtempdesc.ReadOnly = True
        Me.txtempdesc.ReferenceFieldDesc = Nothing
        Me.txtempdesc.ReferenceFieldName = Nothing
        Me.txtempdesc.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtempdesc.RootElement.StretchVertically = True
        Me.txtempdesc.Size = New System.Drawing.Size(276, 20)
        Me.txtempdesc.TabIndex = 4
        '
        'btnnew
        '
        Me.btnnew.Image = Global.XpertERPSalesAndDistribution.My.Resources.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(295, 10)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(15, 21)
        Me.btnnew.TabIndex = 1
        '
        'txtcustdes
        '
        Me.txtcustdes.CalculationExpression = Nothing
        Me.txtcustdes.FieldCode = Nothing
        Me.txtcustdes.FieldDesc = Nothing
        Me.txtcustdes.FieldMaxLength = 0
        Me.txtcustdes.FieldName = Nothing
        Me.txtcustdes.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcustdes.isCalculatedField = False
        Me.txtcustdes.IsSourceFromTable = False
        Me.txtcustdes.IsSourceFromValueList = False
        Me.txtcustdes.IsUnique = False
        Me.txtcustdes.Location = New System.Drawing.Point(312, 11)
        Me.txtcustdes.MaxLength = 49
        Me.txtcustdes.MendatroryField = False
        Me.txtcustdes.MyLinkLable1 = Nothing
        Me.txtcustdes.MyLinkLable2 = Nothing
        Me.txtcustdes.Name = "txtcustdes"
        Me.txtcustdes.ReadOnly = True
        Me.txtcustdes.ReferenceFieldDesc = Nothing
        Me.txtcustdes.ReferenceFieldName = Nothing
        Me.txtcustdes.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtcustdes.RootElement.StretchVertically = True
        Me.txtcustdes.Size = New System.Drawing.Size(276, 20)
        Me.txtcustdes.TabIndex = 2
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(4, 40)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(90, 16)
        Me.MyLabel2.TabIndex = 39
        Me.MyLabel2.Text = "Employee  Code"
        '
        'fndEmployee
        '
        Me.fndEmployee.CalculationExpression = Nothing
        Me.fndEmployee.FieldCode = Nothing
        Me.fndEmployee.FieldDesc = Nothing
        Me.fndEmployee.FieldMaxLength = 0
        Me.fndEmployee.FieldName = Nothing
        Me.fndEmployee.isCalculatedField = False
        Me.fndEmployee.IsSourceFromTable = False
        Me.fndEmployee.IsSourceFromValueList = False
        Me.fndEmployee.IsUnique = False
        Me.fndEmployee.Location = New System.Drawing.Point(94, 38)
        Me.fndEmployee.MendatroryField = False
        Me.fndEmployee.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndEmployee.MyLinkLable1 = Me.MyLabel2
        Me.fndEmployee.MyLinkLable2 = Nothing
        Me.fndEmployee.MyReadOnly = False
        Me.fndEmployee.MyShowMasterFormButton = False
        Me.fndEmployee.Name = "fndEmployee"
        Me.fndEmployee.ReferenceFieldDesc = Nothing
        Me.fndEmployee.ReferenceFieldName = Nothing
        Me.fndEmployee.ReferenceTableName = Nothing
        Me.fndEmployee.Size = New System.Drawing.Size(202, 21)
        Me.fndEmployee.TabIndex = 3
        Me.fndEmployee.Value = ""
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(26, 9)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 8
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(539, 9)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 10
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(93, 9)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 9
        Me.btndelete.Text = "Delete"
        '
        'frmCustomerEmployeeMapping
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(630, 140)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmCustomerEmployeeMapping"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "frmCustomerEmployeeMapping"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtempdesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcustdes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents RadGroupBox2 As RadGroupBox
    Friend WithEvents fndcustomer As common.UserControls.txtNavigator
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtempdesc As common.Controls.MyTextBox
    Friend WithEvents btnnew As RadButton
    Friend WithEvents txtcustdes As common.Controls.MyTextBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents fndEmployee As common.UserControls.txtFinder
    Friend WithEvents RadMenu1 As RadMenu
    Friend WithEvents RadMenuItem3 As RadMenuItem
    Friend WithEvents rmImport As RadMenuItem
    Friend WithEvents rmExport As RadMenuItem
    Friend WithEvents btnsave As RadButton
    Friend WithEvents btnclose As RadButton
    Friend WithEvents btndelete As RadButton
End Class
