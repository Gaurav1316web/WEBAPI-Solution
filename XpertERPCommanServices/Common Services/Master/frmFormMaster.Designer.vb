<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmFormMaster
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
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem5 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem6 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem7 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem8 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem9 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.ddlFormType = New Telerik.WinControls.UI.RadDropDownList
        Me.fndFormCode = New common.UserControls.txtNavigator
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.btnNew = New Telerik.WinControls.UI.RadButton
        Me.txtRemarks = New common.Controls.MyTextBox
        Me.RadLabel3 = New common.Controls.MyLabel
        Me.txtName = New common.Controls.MyTextBox
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnDelete = New Telerik.WinControls.UI.RadButton
        Me.btnSave = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem
        Me.rmImport = New Telerik.WinControls.UI.RadMenuItem
        Me.rmexport = New Telerik.WinControls.UI.RadMenuItem
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlFormType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.ddlFormType)
        Me.RadGroupBox1.Controls.Add(Me.fndFormCode)
        Me.RadGroupBox1.Controls.Add(Me.btnNew)
        Me.RadGroupBox1.Controls.Add(Me.txtRemarks)
        Me.RadGroupBox1.Controls.Add(Me.txtName)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel3)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(9, 26)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(444, 118)
        Me.RadGroupBox1.TabIndex = 0
        '
        'MyLabel1
        '
        Me.MyLabel1.Location = New System.Drawing.Point(13, 62)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(33, 18)
        Me.MyLabel1.TabIndex = 6
        Me.MyLabel1.Text = "Type "
        '
        'ddlFormType
        '
        Me.ddlFormType.AllowShowFocusCues = False
        Me.ddlFormType.AutoCompleteDisplayMember = Nothing
        Me.ddlFormType.AutoCompleteValueMember = Nothing
        Me.ddlFormType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlFormType.ForeColor = System.Drawing.Color.Black
        RadListDataItem1.Text = "C"
        RadListDataItem1.TextWrap = True
        RadListDataItem2.Text = "F"
        RadListDataItem2.TextWrap = True
        RadListDataItem3.Text = "38-Inward"
        RadListDataItem3.TextWrap = True
        RadListDataItem4.Text = "38-Outward"
        RadListDataItem4.TextWrap = True
        RadListDataItem5.Text = "E"
        RadListDataItem5.TextWrap = True
        RadListDataItem6.Text = "D"
        RadListDataItem6.TextWrap = True
        RadListDataItem7.Text = "E1"
        RadListDataItem7.TextWrap = True
        RadListDataItem8.Text = "H"
        RadListDataItem8.TextWrap = True
        RadListDataItem9.Text = "Others"
        RadListDataItem9.TextWrap = True
        Me.ddlFormType.Items.Add(RadListDataItem1)
        Me.ddlFormType.Items.Add(RadListDataItem2)
        Me.ddlFormType.Items.Add(RadListDataItem3)
        Me.ddlFormType.Items.Add(RadListDataItem4)
        Me.ddlFormType.Items.Add(RadListDataItem5)
        Me.ddlFormType.Items.Add(RadListDataItem6)
        Me.ddlFormType.Items.Add(RadListDataItem7)
        Me.ddlFormType.Items.Add(RadListDataItem8)
        Me.ddlFormType.Items.Add(RadListDataItem9)
        Me.ddlFormType.Location = New System.Drawing.Point(86, 62)
        Me.ddlFormType.Name = "ddlFormType"
        Me.ddlFormType.Size = New System.Drawing.Size(202, 20)
        Me.ddlFormType.TabIndex = 2
        '
        'fndFormCode
        '
        Me.fndFormCode.Location = New System.Drawing.Point(86, 13)
        Me.fndFormCode.MendatroryField = True
        Me.fndFormCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.fndFormCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndFormCode.MyLinkLable1 = Me.RadLabel1
        Me.fndFormCode.MyLinkLable2 = Nothing
        Me.fndFormCode.MyMaxLength = 30
        Me.fndFormCode.MyReadOnly = False
        Me.fndFormCode.Name = "fndFormCode"
        Me.fndFormCode.Size = New System.Drawing.Size(202, 20)
        Me.fndFormCode.TabIndex = 0
        Me.fndFormCode.Value = ""
        '
        'RadLabel1
        '
        Me.RadLabel1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.RadLabel1.Location = New System.Drawing.Point(13, 14)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(64, 18)
        Me.RadLabel1.TabIndex = 8
        Me.RadLabel1.Text = "Form Code "
        '
        'btnNew
        '
        Me.btnNew.Image = Global.XpertERPCommanServices.My.Resources.Resources._new
        Me.btnNew.Location = New System.Drawing.Point(291, 13)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(15, 20)
        Me.btnNew.TabIndex = 1
        '
        'txtRemarks
        '
        Me.txtRemarks.Location = New System.Drawing.Point(86, 85)
        Me.txtRemarks.MaxLength = 100
        Me.txtRemarks.MendatroryField = False
        Me.txtRemarks.MyLinkLable1 = Me.RadLabel3
        Me.txtRemarks.MyLinkLable2 = Nothing
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(346, 20)
        Me.txtRemarks.TabIndex = 3
        '
        'RadLabel3
        '
        Me.RadLabel3.Location = New System.Drawing.Point(13, 85)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(52, 18)
        Me.RadLabel3.TabIndex = 5
        Me.RadLabel3.Text = "Remarks "
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(86, 38)
        Me.txtName.MaxLength = 50
        Me.txtName.MendatroryField = True
        Me.txtName.MyLinkLable1 = Me.RadLabel2
        Me.txtName.MyLinkLable2 = Nothing
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(346, 20)
        Me.txtName.TabIndex = 1
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(13, 38)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(39, 18)
        Me.RadLabel2.TabIndex = 7
        Me.RadLabel2.Text = "Name "
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(375, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(80, 18)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Location = New System.Drawing.Point(94, 4)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(80, 18)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(6, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(80, 18)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(465, 192)
        Me.SplitContainer1.SplitterDistance = 162
        Me.SplitContainer1.TabIndex = 0
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(465, 20)
        Me.RadMenu1.TabIndex = 319
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Setting"
        Me.RadMenuItem3.AccessibleName = "Setting"
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmImport, Me.rmexport})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Files"
        Me.RadMenuItem3.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'rmImport
        '
        Me.rmImport.AccessibleDescription = "Import"
        Me.rmImport.AccessibleName = "Import"
        Me.rmImport.Name = "rmImport"
        Me.rmImport.Text = "Import"
        Me.rmImport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'rmexport
        '
        Me.rmexport.AccessibleDescription = "Export"
        Me.rmexport.AccessibleName = "Export"
        Me.rmexport.Name = "rmexport"
        Me.rmexport.Text = "Export"
        Me.rmexport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'FrmFormMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(465, 192)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmFormMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Form Master"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlFormType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtName As common.Controls.MyTextBox
    Friend WithEvents txtRemarks As common.Controls.MyTextBox
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents fndFormCode As common.UserControls.txtNavigator
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents ddlFormType As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmexport As Telerik.WinControls.UI.RadMenuItem
End Class

