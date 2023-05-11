Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGrievanceLogging
    Inherits FrmMainTranScreen 'Telerik.WinControls.UI.RadForm

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
        Me.TxtRemark = New common.Controls.MyTextBox()
        Me.RadLabel1 = New Telerik.WinControls.UI.RadLabel()
        Me.LblToDepartment = New common.Controls.MyLabel()
        Me.TxtToDepartment = New common.UserControls.txtFinder()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.LblFrmDepartment = New common.Controls.MyLabel()
        Me.TxtFrmDepartment = New common.UserControls.txtFinder()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.LblGrievanceType = New common.Controls.MyLabel()
        Me.TxtGrievancceType = New common.UserControls.txtFinder()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.lblAppliedBy = New common.Controls.MyLabel()
        Me.TxtApplied_By = New common.UserControls.txtFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txt_Name = New common.Controls.MyTextBox()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.lbl_Name = New Telerik.WinControls.UI.RadLabel()
        Me.lbl_Code = New Telerik.WinControls.UI.RadLabel()
        Me.txt_Code = New common.UserControls.txtNavigator()
        Me.BtnPost = New Telerik.WinControls.UI.RadButton()
        Me.BtnClose = New Telerik.WinControls.UI.RadButton()
        Me.BtnDelete = New Telerik.WinControls.UI.RadButton()
        Me.Btnsave = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RMFile = New Telerik.WinControls.UI.RadMenuItem()
        Me.RMImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.RMExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.TxtRemark, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblToDepartment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblFrmDepartment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblGrievanceType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAppliedBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_Name, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_Name, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_Code, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TxtRemark)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.LblToDepartment)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TxtToDepartment)
        Me.SplitContainer1.Panel1.Controls.Add(Me.LblFrmDepartment)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TxtFrmDepartment)
        Me.SplitContainer1.Panel1.Controls.Add(Me.LblGrievanceType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TxtGrievancceType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblAppliedBy)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TxtApplied_By)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txt_Name)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnnew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lbl_Name)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lbl_Code)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txt_Code)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(686, 242)
        Me.SplitContainer1.SplitterDistance = 207
        Me.SplitContainer1.TabIndex = 0
        '
        'TxtRemark
        '
        Me.TxtRemark.AutoSize = False
        Me.TxtRemark.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRemark.Location = New System.Drawing.Point(121, 152)
        Me.TxtRemark.MaxLength = 150
        Me.TxtRemark.MendatroryField = False
        Me.TxtRemark.Multiline = True
        Me.TxtRemark.MyLinkLable1 = Nothing
        Me.TxtRemark.MyLinkLable2 = Nothing
        Me.TxtRemark.Name = "TxtRemark"
        Me.TxtRemark.Size = New System.Drawing.Size(548, 49)
        Me.TxtRemark.TabIndex = 21
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(18, 155)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(49, 18)
        Me.RadLabel1.TabIndex = 22
        Me.RadLabel1.Text = "Remarks"
        '
        'LblToDepartment
        '
        Me.LblToDepartment.AutoSize = False
        Me.LblToDepartment.BorderVisible = True
        Me.LblToDepartment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblToDepartment.Location = New System.Drawing.Point(356, 128)
        Me.LblToDepartment.Name = "LblToDepartment"
        Me.LblToDepartment.Size = New System.Drawing.Size(313, 18)
        Me.LblToDepartment.TabIndex = 39
        Me.LblToDepartment.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblToDepartment.TextWrap = False
        '
        'TxtToDepartment
        '
        Me.TxtToDepartment.Location = New System.Drawing.Point(121, 128)
        Me.TxtToDepartment.MendatroryField = True
        Me.TxtToDepartment.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtToDepartment.MyLinkLable1 = Me.MyLabel4
        Me.TxtToDepartment.MyLinkLable2 = Me.LblToDepartment
        Me.TxtToDepartment.MyReadOnly = False
        Me.TxtToDepartment.MyShowMasterFormButton = False
        Me.TxtToDepartment.Name = "TxtToDepartment"
        Me.TxtToDepartment.Size = New System.Drawing.Size(216, 18)
        Me.TxtToDepartment.TabIndex = 37
        Me.TxtToDepartment.Value = ""
        '
        'MyLabel4
        '
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(18, 129)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(85, 16)
        Me.MyLabel4.TabIndex = 38
        Me.MyLabel4.Text = "For Department"
        '
        'LblFrmDepartment
        '
        Me.LblFrmDepartment.AutoSize = False
        Me.LblFrmDepartment.BorderVisible = True
        Me.LblFrmDepartment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFrmDepartment.Location = New System.Drawing.Point(356, 104)
        Me.LblFrmDepartment.Name = "LblFrmDepartment"
        Me.LblFrmDepartment.Size = New System.Drawing.Size(313, 18)
        Me.LblFrmDepartment.TabIndex = 36
        Me.LblFrmDepartment.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblFrmDepartment.TextWrap = False
        '
        'TxtFrmDepartment
        '
        Me.TxtFrmDepartment.Location = New System.Drawing.Point(121, 104)
        Me.TxtFrmDepartment.MendatroryField = True
        Me.TxtFrmDepartment.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFrmDepartment.MyLinkLable1 = Me.MyLabel5
        Me.TxtFrmDepartment.MyLinkLable2 = Me.LblFrmDepartment
        Me.TxtFrmDepartment.MyReadOnly = False
        Me.TxtFrmDepartment.MyShowMasterFormButton = False
        Me.TxtFrmDepartment.Name = "TxtFrmDepartment"
        Me.TxtFrmDepartment.Size = New System.Drawing.Size(216, 18)
        Me.TxtFrmDepartment.TabIndex = 34
        Me.TxtFrmDepartment.Value = ""
        '
        'MyLabel5
        '
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(18, 106)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(95, 16)
        Me.MyLabel5.TabIndex = 35
        Me.MyLabel5.Text = "From Department"
        '
        'LblGrievanceType
        '
        Me.LblGrievanceType.AutoSize = False
        Me.LblGrievanceType.BorderVisible = True
        Me.LblGrievanceType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblGrievanceType.Location = New System.Drawing.Point(356, 57)
        Me.LblGrievanceType.Name = "LblGrievanceType"
        Me.LblGrievanceType.Size = New System.Drawing.Size(313, 18)
        Me.LblGrievanceType.TabIndex = 36
        Me.LblGrievanceType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblGrievanceType.TextWrap = False
        '
        'TxtGrievancceType
        '
        Me.TxtGrievancceType.Location = New System.Drawing.Point(121, 57)
        Me.TxtGrievancceType.MendatroryField = True
        Me.TxtGrievancceType.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtGrievancceType.MyLinkLable1 = Me.MyLabel3
        Me.TxtGrievancceType.MyLinkLable2 = Me.LblGrievanceType
        Me.TxtGrievancceType.MyReadOnly = False
        Me.TxtGrievancceType.MyShowMasterFormButton = False
        Me.TxtGrievancceType.Name = "TxtGrievancceType"
        Me.TxtGrievancceType.Size = New System.Drawing.Size(216, 18)
        Me.TxtGrievancceType.TabIndex = 34
        Me.TxtGrievancceType.Value = ""
        '
        'MyLabel3
        '
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(18, 59)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(86, 16)
        Me.MyLabel3.TabIndex = 35
        Me.MyLabel3.Text = "Grievance Type"
        '
        'lblAppliedBy
        '
        Me.lblAppliedBy.AutoSize = False
        Me.lblAppliedBy.BorderVisible = True
        Me.lblAppliedBy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAppliedBy.Location = New System.Drawing.Point(356, 80)
        Me.lblAppliedBy.Name = "lblAppliedBy"
        Me.lblAppliedBy.Size = New System.Drawing.Size(313, 18)
        Me.lblAppliedBy.TabIndex = 33
        Me.lblAppliedBy.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblAppliedBy.TextWrap = False
        '
        'TxtApplied_By
        '
        Me.TxtApplied_By.Location = New System.Drawing.Point(121, 80)
        Me.TxtApplied_By.MendatroryField = True
        Me.TxtApplied_By.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtApplied_By.MyLinkLable1 = Me.MyLabel1
        Me.TxtApplied_By.MyLinkLable2 = Me.lblAppliedBy
        Me.TxtApplied_By.MyReadOnly = False
        Me.TxtApplied_By.MyShowMasterFormButton = False
        Me.TxtApplied_By.Name = "TxtApplied_By"
        Me.TxtApplied_By.Size = New System.Drawing.Size(216, 18)
        Me.TxtApplied_By.TabIndex = 31
        Me.TxtApplied_By.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(18, 82)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(60, 16)
        Me.MyLabel1.TabIndex = 32
        Me.MyLabel1.Text = "Applied By"
        '
        'txt_Name
        '
        Me.txt_Name.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_Name.Location = New System.Drawing.Point(121, 35)
        Me.txt_Name.MaxLength = 150
        Me.txt_Name.MendatroryField = True
        Me.txt_Name.MyLinkLable1 = Nothing
        Me.txt_Name.MyLinkLable2 = Nothing
        Me.txt_Name.Name = "txt_Name"
        Me.txt_Name.Size = New System.Drawing.Size(548, 18)
        Me.txt_Name.TabIndex = 4
        '
        'btnnew
        '
        Me.btnnew.Image = Global.XpertERPHRandPayroll.My.Resources.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(322, 10)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(15, 20)
        Me.btnnew.TabIndex = 1
        '
        'lbl_Name
        '
        Me.lbl_Name.Location = New System.Drawing.Point(18, 35)
        Me.lbl_Name.Name = "lbl_Name"
        Me.lbl_Name.Size = New System.Drawing.Size(63, 18)
        Me.lbl_Name.TabIndex = 20
        Me.lbl_Name.Text = "Description"
        '
        'lbl_Code
        '
        Me.lbl_Code.Location = New System.Drawing.Point(18, 11)
        Me.lbl_Code.Name = "lbl_Code"
        Me.lbl_Code.Size = New System.Drawing.Size(32, 18)
        Me.lbl_Code.TabIndex = 19
        Me.lbl_Code.Text = "Code"
        '
        'txt_Code
        '
        Me.txt_Code.Location = New System.Drawing.Point(121, 10)
        Me.txt_Code.MendatroryField = True
        Me.txt_Code.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txt_Code.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txt_Code.MyLinkLable1 = Nothing
        Me.txt_Code.MyLinkLable2 = Nothing
        Me.txt_Code.MyMaxLength = 32767
        Me.txt_Code.MyReadOnly = False
        Me.txt_Code.Name = "txt_Code"
        Me.txt_Code.Size = New System.Drawing.Size(201, 20)
        Me.txt_Code.TabIndex = 1
        Me.txt_Code.Value = ""
        '
        'BtnPost
        '
        Me.BtnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnPost.Location = New System.Drawing.Point(160, 5)
        Me.BtnPost.Name = "BtnPost"
        Me.BtnPost.Size = New System.Drawing.Size(67, 20)
        Me.BtnPost.TabIndex = 2
        Me.BtnPost.Text = "Post"
        '
        'BtnClose
        '
        Me.BtnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnClose.Location = New System.Drawing.Point(602, 5)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(73, 21)
        Me.BtnClose.TabIndex = 2
        Me.BtnClose.Text = "Close"
        '
        'BtnDelete
        '
        Me.BtnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnDelete.Location = New System.Drawing.Point(87, 5)
        Me.BtnDelete.Name = "BtnDelete"
        Me.BtnDelete.Size = New System.Drawing.Size(67, 20)
        Me.BtnDelete.TabIndex = 1
        Me.BtnDelete.Text = "Delete"
        '
        'Btnsave
        '
        Me.Btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Btnsave.Location = New System.Drawing.Point(13, 5)
        Me.Btnsave.Name = "Btnsave"
        Me.Btnsave.Size = New System.Drawing.Size(68, 21)
        Me.Btnsave.TabIndex = 0
        Me.Btnsave.Text = "Save"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RMFile})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(686, 20)
        Me.RadMenu1.TabIndex = 1
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RMFile
        '
        Me.RMFile.AccessibleDescription = "File"
        Me.RMFile.AccessibleName = "File"
        Me.RMFile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RMImport, Me.RMExport})
        Me.RMFile.Name = "RMFile"
        Me.RMFile.Text = "File"
        '
        'RMImport
        '
        Me.RMImport.AccessibleDescription = "Import"
        Me.RMImport.AccessibleName = "Import"
        Me.RMImport.Name = "RMImport"
        Me.RMImport.Text = "Import"
        '
        'RMExport
        '
        Me.RMExport.AccessibleDescription = "Export"
        Me.RMExport.AccessibleName = "Export"
        Me.RMExport.Name = "RMExport"
        Me.RMExport.Text = "Export"
        '
        'RadLabel4
        '
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(352, 13)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 41
        Me.RadLabel4.Text = "Date"
        '
        'txtDate
        '
        Me.txtDate.CustomFormat = "dd/MM/yyyy"
        Me.txtDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDate.Location = New System.Drawing.Point(384, 12)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.RadLabel4
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.Size = New System.Drawing.Size(139, 18)
        Me.txtDate.TabIndex = 40
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'frmGrievanceLogging
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(686, 262)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmGrievanceLogging"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Grievance Logging"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.TxtRemark, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblToDepartment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblFrmDepartment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblGrievanceType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAppliedBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_Name, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_Name, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_Code, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer

    Friend WithEvents Btnsave As Telerik.WinControls.UI.RadButton

    Friend WithEvents BtnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents BtnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadLabel2 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents txtdescription As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents lbl_Code As Telerik.WinControls.UI.RadLabel
    Friend WithEvents txt_Code As common.UserControls.txtNavigator
    Friend WithEvents lbl_Name As Telerik.WinControls.UI.RadLabel
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RMFile As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RMImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RMExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents txt_Name As common.Controls.MyTextBox
    Friend WithEvents lblAppliedBy As common.Controls.MyLabel
    Friend WithEvents TxtApplied_By As common.UserControls.txtFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents LblGrievanceType As common.Controls.MyLabel
    Friend WithEvents TxtGrievancceType As common.UserControls.txtFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents LblFrmDepartment As common.Controls.MyLabel
    Friend WithEvents TxtFrmDepartment As common.UserControls.txtFinder
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents LblToDepartment As common.Controls.MyLabel
    Friend WithEvents TxtToDepartment As common.UserControls.txtFinder
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents TxtRemark As common.Controls.MyTextBox
    Friend WithEvents RadLabel1 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents BtnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
End Class

