Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDesignationHierarchyMaster
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
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.desimport = New Telerik.WinControls.UI.RadMenuItem()
        Me.desexport = New Telerik.WinControls.UI.RadMenuItem()
        Me.desclose = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.lbldesid = New common.Controls.MyLabel()
        Me.ToolTipdesig = New System.Windows.Forms.ToolTip(Me.components)
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.gbdesignation = New Telerik.WinControls.UI.RadGroupBox()
        Me.LblDesignationDesc = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.LblHigherDesignationdesc = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lblFromLocation = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.LblLoc = New common.Controls.MyLabel()
        Me.CmbLevelCode = New common.Controls.MyComboBox()
        Me.RadLabel29 = New common.Controls.MyLabel()
        Me.FndhigherDesg = New common.UserControls.txtFinder()
        Me.cboDocType = New common.Controls.MyComboBox()
        Me.txtFromLocation = New common.UserControls.txtFinder()
        Me.fnddesig = New common.UserControls.txtNavigator()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbldesid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gbdesignation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbdesignation.SuspendLayout()
        CType(Me.LblDesignationDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblHigherDesignationdesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFromLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblLoc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbLevelCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel29, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboDocType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.desimport, Me.desexport, Me.desclose})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'desimport
        '
        Me.desimport.AccessibleDescription = "Import"
        Me.desimport.AccessibleName = "Import"
        Me.desimport.Name = "desimport"
        Me.desimport.Text = "Import"
        '
        'desexport
        '
        Me.desexport.AccessibleDescription = "Export"
        Me.desexport.AccessibleName = "Export"
        Me.desexport.Name = "desexport"
        Me.desexport.Text = "Export"
        '
        'desclose
        '
        Me.desclose.AccessibleDescription = "Close"
        Me.desclose.AccessibleName = "Close"
        Me.desclose.Name = "desclose"
        Me.desclose.Text = "Close"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(378, 3)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(96, 3)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(17, 3)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'lbldesid
        '
        Me.lbldesid.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldesid.Location = New System.Drawing.Point(7, 13)
        Me.lbldesid.Name = "lbldesid"
        Me.lbldesid.Size = New System.Drawing.Size(99, 16)
        Me.lbldesid.TabIndex = 4
        Me.lbldesid.Text = "Designation  Code"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(455, 20)
        Me.RadMenu1.TabIndex = 1
        Me.RadMenu1.Text = "RadMenu1"
        '
        'btnnew
        '
        Me.btnnew.Image = Global.XpertERPHRandPayroll.My.Resources.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(418, 11)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(15, 21)
        Me.btnnew.TabIndex = 1
        '
        'gbdesignation
        '
        Me.gbdesignation.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbdesignation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.gbdesignation.Controls.Add(Me.LblDesignationDesc)
        Me.gbdesignation.Controls.Add(Me.MyLabel5)
        Me.gbdesignation.Controls.Add(Me.LblHigherDesignationdesc)
        Me.gbdesignation.Controls.Add(Me.MyLabel1)
        Me.gbdesignation.Controls.Add(Me.lblFromLocation)
        Me.gbdesignation.Controls.Add(Me.MyLabel3)
        Me.gbdesignation.Controls.Add(Me.LblLoc)
        Me.gbdesignation.Controls.Add(Me.CmbLevelCode)
        Me.gbdesignation.Controls.Add(Me.RadLabel29)
        Me.gbdesignation.Controls.Add(Me.FndhigherDesg)
        Me.gbdesignation.Controls.Add(Me.cboDocType)
        Me.gbdesignation.Controls.Add(Me.txtFromLocation)
        Me.gbdesignation.Controls.Add(Me.fnddesig)
        Me.gbdesignation.Controls.Add(Me.lbldesid)
        Me.gbdesignation.Controls.Add(Me.btnnew)
        Me.gbdesignation.HeaderPosition = Telerik.WinControls.UI.HeaderPosition.Left
        Me.gbdesignation.HeaderText = ""
        Me.gbdesignation.Location = New System.Drawing.Point(3, 6)
        Me.gbdesignation.Name = "gbdesignation"
        Me.gbdesignation.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.gbdesignation.Size = New System.Drawing.Size(449, 112)
        Me.gbdesignation.TabIndex = 0
        '
        'LblDesignationDesc
        '
        Me.LblDesignationDesc.AutoSize = False
        Me.LblDesignationDesc.BorderVisible = True
        Me.LblDesignationDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDesignationDesc.Location = New System.Drawing.Point(116, 37)
        Me.LblDesignationDesc.Name = "LblDesignationDesc"
        Me.LblDesignationDesc.Size = New System.Drawing.Size(317, 18)
        Me.LblDesignationDesc.TabIndex = 43
        Me.LblDesignationDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblDesignationDesc.TextWrap = False
        '
        'MyLabel5
        '
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(7, 38)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel5.TabIndex = 42
        Me.MyLabel5.Text = "Description"
        '
        'LblHigherDesignationdesc
        '
        Me.LblHigherDesignationdesc.AutoSize = False
        Me.LblHigherDesignationdesc.BorderVisible = True
        Me.LblHigherDesignationdesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblHigherDesignationdesc.Location = New System.Drawing.Point(245, 85)
        Me.LblHigherDesignationdesc.Name = "LblHigherDesignationdesc"
        Me.LblHigherDesignationdesc.Size = New System.Drawing.Size(188, 18)
        Me.LblHigherDesignationdesc.TabIndex = 41
        Me.LblHigherDesignationdesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblHigherDesignationdesc.TextWrap = False
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(7, 86)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(103, 16)
        Me.MyLabel1.TabIndex = 39
        Me.MyLabel1.Text = "Higher Designation"
        '
        'lblFromLocation
        '
        Me.lblFromLocation.AutoSize = False
        Me.lblFromLocation.BorderVisible = True
        Me.lblFromLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFromLocation.Location = New System.Drawing.Point(245, 85)
        Me.lblFromLocation.Name = "lblFromLocation"
        Me.lblFromLocation.Size = New System.Drawing.Size(179, 18)
        Me.lblFromLocation.TabIndex = 41
        Me.lblFromLocation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblFromLocation.TextWrap = False
        '
        'MyLabel3
        '
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(7, 61)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel3.TabIndex = 40
        Me.MyLabel3.Text = "Level Code"
        '
        'LblLoc
        '
        Me.LblLoc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblLoc.Location = New System.Drawing.Point(7, 86)
        Me.LblLoc.Name = "LblLoc"
        Me.LblLoc.Size = New System.Drawing.Size(79, 16)
        Me.LblLoc.TabIndex = 39
        Me.LblLoc.Text = "From Location"
        '
        'CmbLevelCode
        '
        Me.CmbLevelCode.AutoCompleteDisplayMember = Nothing
        Me.CmbLevelCode.AutoCompleteValueMember = Nothing
        Me.CmbLevelCode.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.CmbLevelCode.Location = New System.Drawing.Point(116, 59)
        Me.CmbLevelCode.MendatroryField = True
        Me.CmbLevelCode.MyLinkLable1 = Me.MyLabel3
        Me.CmbLevelCode.MyLinkLable2 = Nothing
        Me.CmbLevelCode.Name = "CmbLevelCode"
        Me.CmbLevelCode.ReadOnly = True
        Me.CmbLevelCode.Size = New System.Drawing.Size(167, 20)
        Me.CmbLevelCode.TabIndex = 37
        '
        'RadLabel29
        '
        Me.RadLabel29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel29.Location = New System.Drawing.Point(543, 59)
        Me.RadLabel29.Name = "RadLabel29"
        Me.RadLabel29.Size = New System.Drawing.Size(58, 16)
        Me.RadLabel29.TabIndex = 40
        Me.RadLabel29.Text = "Doc. Type"
        '
        'FndhigherDesg
        '
        Me.FndhigherDesg.Location = New System.Drawing.Point(116, 85)
        Me.FndhigherDesg.MendatroryField = True
        Me.FndhigherDesg.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndhigherDesg.MyLinkLable1 = Me.MyLabel1
        Me.FndhigherDesg.MyLinkLable2 = Me.LblHigherDesignationdesc
        Me.FndhigherDesg.MyReadOnly = False
        Me.FndhigherDesg.MyShowMasterFormButton = False
        Me.FndhigherDesg.Name = "FndhigherDesg"
        Me.FndhigherDesg.Size = New System.Drawing.Size(123, 19)
        Me.FndhigherDesg.TabIndex = 38
        Me.FndhigherDesg.Value = ""
        '
        'cboDocType
        '
        Me.cboDocType.AutoCompleteDisplayMember = Nothing
        Me.cboDocType.AutoCompleteValueMember = Nothing
        Me.cboDocType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboDocType.Location = New System.Drawing.Point(610, 57)
        Me.cboDocType.MendatroryField = True
        Me.cboDocType.MyLinkLable1 = Me.RadLabel29
        Me.cboDocType.MyLinkLable2 = Nothing
        Me.cboDocType.Name = "cboDocType"
        Me.cboDocType.Size = New System.Drawing.Size(143, 20)
        Me.cboDocType.TabIndex = 37
        '
        'txtFromLocation
        '
        Me.txtFromLocation.Location = New System.Drawing.Point(116, 85)
        Me.txtFromLocation.MendatroryField = True
        Me.txtFromLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFromLocation.MyLinkLable1 = Me.LblLoc
        Me.txtFromLocation.MyLinkLable2 = Me.lblFromLocation
        Me.txtFromLocation.MyReadOnly = False
        Me.txtFromLocation.MyShowMasterFormButton = False
        Me.txtFromLocation.Name = "txtFromLocation"
        Me.txtFromLocation.Size = New System.Drawing.Size(123, 19)
        Me.txtFromLocation.TabIndex = 38
        Me.txtFromLocation.Value = ""
        '
        'fnddesig
        '
        Me.fnddesig.Location = New System.Drawing.Point(116, 11)
        Me.fnddesig.MendatroryField = True
        Me.fnddesig.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fnddesig.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fnddesig.MyLinkLable1 = Nothing
        Me.fnddesig.MyLinkLable2 = Nothing
        Me.fnddesig.MyMaxLength = 32767
        Me.fnddesig.MyReadOnly = False
        Me.fnddesig.Name = "fnddesig"
        Me.fnddesig.Size = New System.Drawing.Size(303, 21)
        Me.fnddesig.TabIndex = 0
        Me.fnddesig.Value = ""
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.gbdesignation)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(455, 153)
        Me.SplitContainer1.SplitterDistance = 124
        Me.SplitContainer1.TabIndex = 0
        '
        'frmDesignationHierarchyMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(455, 173)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmDesignationHierarchyMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Designation Master"
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbldesid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gbdesignation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbdesignation.ResumeLayout(False)
        Me.gbdesignation.PerformLayout()
        CType(Me.LblDesignationDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblHigherDesignationdesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFromLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblLoc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbLevelCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel29, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboDocType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents ToolTipdesig As System.Windows.Forms.ToolTip
    Friend WithEvents desimport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents desexport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents desclose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents gbdesignation As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents lbldesid As common.Controls.MyLabel
    Friend WithEvents fnddesig As common.UserControls.txtNavigator
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents LblDesignationDesc As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents LblHigherDesignationdesc As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents lblFromLocation As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents LblLoc As common.Controls.MyLabel
    Friend WithEvents CmbLevelCode As common.Controls.MyComboBox
    Friend WithEvents RadLabel29 As common.Controls.MyLabel
    Friend WithEvents FndhigherDesg As common.UserControls.txtFinder
    Friend WithEvents cboDocType As common.Controls.MyComboBox
    Friend WithEvents txtFromLocation As common.UserControls.txtFinder
End Class

