<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCityMaster
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
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.lbldes = New common.Controls.MyLabel()
        Me.txtdes = New common.Controls.MyTextBox()
        Me.lblid = New common.Controls.MyLabel()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.cityimport = New Telerik.WinControls.UI.RadMenuItem()
        Me.cityexport = New Telerik.WinControls.UI.RadMenuItem()
        Me.cityclose = New Telerik.WinControls.UI.RadMenuItem()
        Me.ToolTipcity = New System.Windows.Forms.ToolTip(Me.components)
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.gbcity = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtregionname = New common.Controls.MyLabel()
        Me.txtregioncode = New common.Controls.MyLabel()
        Me.lblState = New common.Controls.MyLabel()
        Me.txtState = New common.UserControls.txtFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.fndcity = New common.UserControls.txtNavigator()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvDB = New common.UserControls.MyRadGridView()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.lblDistrictName = New common.Controls.MyLabel()
        Me.txtDistrict = New common.UserControls.txtFinder()
        Me.MyLabel3 = New common.Controls.MyLabel()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbldes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gbcity, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbcity.SuspendLayout()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtregionname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtregioncode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblState, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.gvDB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDB.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.lblDistrictName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(446, 7)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(75, 8)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(6, 8)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'lbldes
        '
        Me.lbldes.FieldName = Nothing
        Me.lbldes.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldes.Location = New System.Drawing.Point(9, 31)
        Me.lbldes.Name = "lbldes"
        Me.lbldes.Size = New System.Drawing.Size(63, 16)
        Me.lbldes.TabIndex = 6
        Me.lbldes.Text = "Description"
        '
        'txtdes
        '
        Me.txtdes.BackColor = System.Drawing.Color.Transparent
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
        Me.txtdes.Location = New System.Drawing.Point(91, 30)
        Me.txtdes.MaxLength = 49
        Me.txtdes.MendatroryField = True
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
        Me.txtdes.Size = New System.Drawing.Size(364, 18)
        Me.txtdes.TabIndex = 2
        '
        'lblid
        '
        Me.lblid.FieldName = Nothing
        Me.lblid.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblid.Location = New System.Drawing.Point(9, 11)
        Me.lblid.Name = "lblid"
        Me.lblid.Size = New System.Drawing.Size(56, 16)
        Me.lblid.TabIndex = 7
        Me.lblid.Text = "City Code"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.cityimport, Me.cityexport, Me.cityclose})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'cityimport
        '
        Me.cityimport.AccessibleDescription = "Import"
        Me.cityimport.AccessibleName = "Import"
        Me.cityimport.Name = "cityimport"
        Me.cityimport.Text = "Import"
        '
        'cityexport
        '
        Me.cityexport.AccessibleDescription = "Export"
        Me.cityexport.AccessibleName = "Export"
        Me.cityexport.Name = "cityexport"
        Me.cityexport.Text = "Export"
        '
        'cityclose
        '
        Me.cityclose.AccessibleDescription = "Close"
        Me.cityclose.AccessibleName = "Close"
        Me.cityclose.Name = "cityclose"
        Me.cityclose.Text = "Close"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(519, 20)
        Me.RadMenu1.TabIndex = 1
        Me.RadMenu1.Text = "RadMenu1"
        '
        'btnnew
        '
        Me.btnnew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(297, 7)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(15, 20)
        Me.btnnew.TabIndex = 1
        '
        'gbcity
        '
        Me.gbcity.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbcity.BackColor = System.Drawing.Color.Transparent
        Me.gbcity.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.gbcity.Controls.Add(Me.lblDistrictName)
        Me.gbcity.Controls.Add(Me.txtDistrict)
        Me.gbcity.Controls.Add(Me.MyLabel3)
        Me.gbcity.Controls.Add(Me.MyLabel4)
        Me.gbcity.Controls.Add(Me.txtregionname)
        Me.gbcity.Controls.Add(Me.txtregioncode)
        Me.gbcity.Controls.Add(Me.lblState)
        Me.gbcity.Controls.Add(Me.txtState)
        Me.gbcity.Controls.Add(Me.MyLabel1)
        Me.gbcity.Controls.Add(Me.fndcity)
        Me.gbcity.Controls.Add(Me.RadGroupBox4)
        Me.gbcity.Controls.Add(Me.lblid)
        Me.gbcity.Controls.Add(Me.txtdes)
        Me.gbcity.Controls.Add(Me.lbldes)
        Me.gbcity.Controls.Add(Me.btnnew)
        Me.gbcity.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gbcity.HeaderText = ""
        Me.gbcity.Location = New System.Drawing.Point(3, 3)
        Me.gbcity.Name = "gbcity"
        Me.gbcity.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.gbcity.Size = New System.Drawing.Size(509, 395)
        Me.gbcity.TabIndex = 0
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Location = New System.Drawing.Point(9, 73)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(41, 18)
        Me.MyLabel4.TabIndex = 28
        Me.MyLabel4.Text = "Region"
        '
        'txtregionname
        '
        Me.txtregionname.AutoSize = False
        Me.txtregionname.BorderVisible = True
        Me.txtregionname.FieldName = Nothing
        Me.txtregionname.Location = New System.Drawing.Point(230, 73)
        Me.txtregionname.Name = "txtregionname"
        Me.txtregionname.Size = New System.Drawing.Size(225, 18)
        Me.txtregionname.TabIndex = 27
        Me.txtregionname.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtregioncode
        '
        Me.txtregioncode.AutoSize = False
        Me.txtregioncode.BorderVisible = True
        Me.txtregioncode.FieldName = Nothing
        Me.txtregioncode.Location = New System.Drawing.Point(91, 73)
        Me.txtregioncode.Name = "txtregioncode"
        Me.txtregioncode.Size = New System.Drawing.Size(133, 18)
        Me.txtregioncode.TabIndex = 27
        Me.txtregioncode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblState
        '
        Me.lblState.AutoSize = False
        Me.lblState.BorderVisible = True
        Me.lblState.FieldName = Nothing
        Me.lblState.Location = New System.Drawing.Point(230, 51)
        Me.lblState.Name = "lblState"
        Me.lblState.Size = New System.Drawing.Size(225, 18)
        Me.lblState.TabIndex = 26
        Me.lblState.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtState
        '
        Me.txtState.CalculationExpression = Nothing
        Me.txtState.FieldCode = Nothing
        Me.txtState.FieldDesc = Nothing
        Me.txtState.FieldMaxLength = 0
        Me.txtState.FieldName = Nothing
        Me.txtState.isCalculatedField = False
        Me.txtState.IsSourceFromTable = False
        Me.txtState.IsSourceFromValueList = False
        Me.txtState.IsUnique = False
        Me.txtState.Location = New System.Drawing.Point(91, 51)
        Me.txtState.MendatroryField = True
        Me.txtState.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtState.MyLinkLable1 = Me.lblState
        Me.txtState.MyLinkLable2 = Me.MyLabel1
        Me.txtState.MyReadOnly = False
        Me.txtState.MyShowMasterFormButton = False
        Me.txtState.Name = "txtState"
        Me.txtState.ReferenceFieldDesc = Nothing
        Me.txtState.ReferenceFieldName = Nothing
        Me.txtState.ReferenceTableName = Nothing
        Me.txtState.Size = New System.Drawing.Size(133, 18)
        Me.txtState.TabIndex = 3
        Me.txtState.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(9, 51)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(32, 18)
        Me.MyLabel1.TabIndex = 5
        Me.MyLabel1.Text = "State"
        '
        'fndcity
        '
        Me.fndcity.FieldName = Nothing
        Me.fndcity.Location = New System.Drawing.Point(91, 7)
        Me.fndcity.MendatroryField = True
        Me.fndcity.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndcity.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndcity.MyLinkLable1 = Me.lblid
        Me.fndcity.MyLinkLable2 = Nothing
        Me.fndcity.MyMaxLength = 50
        Me.fndcity.MyReadOnly = False
        Me.fndcity.Name = "fndcity"
        Me.fndcity.Size = New System.Drawing.Size(202, 18)
        Me.fndcity.TabIndex = 0
        Me.fndcity.Value = ""
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.gvDB)
        Me.RadGroupBox4.HeaderText = "Replicate In Other Companies"
        Me.RadGroupBox4.Location = New System.Drawing.Point(6, 131)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(496, 259)
        Me.RadGroupBox4.TabIndex = 4
        Me.RadGroupBox4.Text = "Replicate In Other Companies"
        '
        'gvDB
        '
        Me.gvDB.BackColor = System.Drawing.Color.Transparent
        Me.gvDB.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvDB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvDB.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gvDB.ForeColor = System.Drawing.Color.Black
        Me.gvDB.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvDB.Location = New System.Drawing.Point(10, 20)
        '
        'gvDB
        '
        Me.gvDB.MasterTemplate.AllowAddNewRow = False
        Me.gvDB.MasterTemplate.EnableFiltering = True
        Me.gvDB.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvDB.Name = "gvDB"
        Me.gvDB.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvDB.ShowGroupPanel = False
        Me.gvDB.ShowHeaderCellButtons = True
        Me.gvDB.Size = New System.Drawing.Size(476, 229)
        Me.gvDB.TabIndex = 0
        Me.gvDB.TabStop = False
        Me.gvDB.Text = "RadGridView1"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.gbcity)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(519, 435)
        Me.SplitContainer1.SplitterDistance = 402
        Me.SplitContainer1.TabIndex = 2
        '
        'lblDistrictName
        '
        Me.lblDistrictName.AutoSize = False
        Me.lblDistrictName.BorderVisible = True
        Me.lblDistrictName.FieldName = Nothing
        Me.lblDistrictName.Location = New System.Drawing.Point(230, 94)
        Me.lblDistrictName.Name = "lblDistrictName"
        Me.lblDistrictName.Size = New System.Drawing.Size(225, 18)
        Me.lblDistrictName.TabIndex = 31
        Me.lblDistrictName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDistrict
        '
        Me.txtDistrict.CalculationExpression = Nothing
        Me.txtDistrict.FieldCode = Nothing
        Me.txtDistrict.FieldDesc = Nothing
        Me.txtDistrict.FieldMaxLength = 0
        Me.txtDistrict.FieldName = Nothing
        Me.txtDistrict.isCalculatedField = False
        Me.txtDistrict.IsSourceFromTable = False
        Me.txtDistrict.IsSourceFromValueList = False
        Me.txtDistrict.IsUnique = False
        Me.txtDistrict.Location = New System.Drawing.Point(91, 94)
        Me.txtDistrict.MendatroryField = False
        Me.txtDistrict.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDistrict.MyLinkLable1 = Me.lblDistrictName
        Me.txtDistrict.MyLinkLable2 = Me.MyLabel3
        Me.txtDistrict.MyReadOnly = False
        Me.txtDistrict.MyShowMasterFormButton = False
        Me.txtDistrict.Name = "txtDistrict"
        Me.txtDistrict.ReferenceFieldDesc = Nothing
        Me.txtDistrict.ReferenceFieldName = Nothing
        Me.txtDistrict.ReferenceTableName = Nothing
        Me.txtDistrict.Size = New System.Drawing.Size(133, 18)
        Me.txtDistrict.TabIndex = 29
        Me.txtDistrict.Value = ""
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Location = New System.Drawing.Point(9, 94)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(41, 18)
        Me.MyLabel3.TabIndex = 30
        Me.MyLabel3.Text = "District"
        '
        'frmCityMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(519, 455)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmCityMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "City Master"
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbldes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gbcity, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbcity.ResumeLayout(False)
        Me.gbcity.PerformLayout()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtregionname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtregioncode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblState, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        CType(Me.gvDB.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDB, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.lblDistrictName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtdes As common.Controls.MyTextBox
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents ToolTipcity As System.Windows.Forms.ToolTip
    Friend WithEvents cityimport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents cityexport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents cityclose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents gbcity As Telerik.WinControls.UI.RadGroupBox
    'Friend WithEvents Office2007SilverTheme1 As Telerik.WinControls.Themes.Office2007SilverTheme
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvDB As common.UserControls.MyRadGridView
    Friend WithEvents lbldes As common.Controls.MyLabel
    Friend WithEvents lblid As common.Controls.MyLabel
    Friend WithEvents fndcity As common.UserControls.txtNavigator
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblState As common.Controls.MyLabel
    Friend WithEvents txtState As common.UserControls.txtFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtregionname As common.Controls.MyLabel
    Friend WithEvents txtregioncode As common.Controls.MyLabel
    Friend WithEvents lblDistrictName As common.Controls.MyLabel
    Friend WithEvents txtDistrict As common.UserControls.txtFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
End Class

