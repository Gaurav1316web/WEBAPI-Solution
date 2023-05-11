Imports XpertERPEngine
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRequestForTrainingMaster
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
        Me.GrpEmp = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgEmp = New common.MyCheckBoxGrid()
        Me.GrpDept = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgDept = New common.MyCheckBoxGrid()
        Me.UsLock1 = New common.usLock()
        Me.LblEmployeeName = New common.Controls.MyLabel()
        Me.lblTrainingCourseName = New common.Controls.MyLabel()
        Me.FndEmployee = New common.UserControls.txtFinder()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.FndTrainingCourse = New common.UserControls.txtFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.DtpDate = New common.Controls.MyDateTimePicker()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.txt_remarks = New Telerik.WinControls.UI.RadTextBox()
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
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.GrpEmp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpEmp.SuspendLayout()
        CType(Me.GrpDept, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpDept.SuspendLayout()
        CType(Me.LblEmployeeName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTrainingCourseName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DtpDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_remarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_Name, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_Code, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.GrpEmp)
        Me.SplitContainer1.Panel1.Controls.Add(Me.GrpDept)
        Me.SplitContainer1.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.LblEmployeeName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblTrainingCourseName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.FndEmployee)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.FndTrainingCourse)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.DtpDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnnew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txt_remarks)
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
        Me.SplitContainer1.Size = New System.Drawing.Size(747, 412)
        Me.SplitContainer1.SplitterDistance = 377
        Me.SplitContainer1.TabIndex = 0
        '
        'GrpEmp
        '
        Me.GrpEmp.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.GrpEmp.Controls.Add(Me.cbgEmp)
        Me.GrpEmp.HeaderText = "Employee"
        Me.GrpEmp.Location = New System.Drawing.Point(313, 121)
        Me.GrpEmp.Name = "GrpEmp"
        Me.GrpEmp.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.GrpEmp.Size = New System.Drawing.Size(295, 222)
        Me.GrpEmp.TabIndex = 376
        Me.GrpEmp.Text = "Employee"
        '
        'cbgEmp
        '
        Me.cbgEmp.CheckedValue = Nothing
        Me.cbgEmp.DataSource = Nothing
        Me.cbgEmp.DisplayMember = "Name"
        Me.cbgEmp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgEmp.Location = New System.Drawing.Point(10, 20)
        Me.cbgEmp.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgEmp.MyShowHeadrText = False
        Me.cbgEmp.Name = "cbgEmp"
        Me.cbgEmp.Size = New System.Drawing.Size(275, 192)
        Me.cbgEmp.TabIndex = 1
        Me.cbgEmp.ValueMember = "Code"
        '
        'GrpDept
        '
        Me.GrpDept.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.GrpDept.Controls.Add(Me.cbgDept)
        Me.GrpDept.HeaderText = "Departments"
        Me.GrpDept.Location = New System.Drawing.Point(14, 121)
        Me.GrpDept.Name = "GrpDept"
        Me.GrpDept.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.GrpDept.Size = New System.Drawing.Size(295, 222)
        Me.GrpDept.TabIndex = 375
        Me.GrpDept.Text = "Departments"
        '
        'cbgDept
        '
        Me.cbgDept.CheckedValue = Nothing
        Me.cbgDept.DataSource = Nothing
        Me.cbgDept.DisplayMember = "Name"
        Me.cbgDept.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgDept.Location = New System.Drawing.Point(10, 20)
        Me.cbgDept.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgDept.MyShowHeadrText = False
        Me.cbgDept.Name = "cbgDept"
        Me.cbgDept.Size = New System.Drawing.Size(275, 192)
        Me.cbgDept.TabIndex = 1
        Me.cbgDept.ValueMember = "Code"
        '
        'UsLock1
        '
        Me.UsLock1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(642, 6)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 3
        '
        'LblEmployeeName
        '
        Me.LblEmployeeName.AutoSize = False
        Me.LblEmployeeName.BorderVisible = True
        Me.LblEmployeeName.Location = New System.Drawing.Point(485, 349)
        Me.LblEmployeeName.Name = "LblEmployeeName"
        Me.LblEmployeeName.Size = New System.Drawing.Size(48, 19)
        Me.LblEmployeeName.TabIndex = 374
        Me.LblEmployeeName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblEmployeeName.Visible = False
        '
        'lblTrainingCourseName
        '
        Me.lblTrainingCourseName.AutoSize = False
        Me.lblTrainingCourseName.BorderVisible = True
        Me.lblTrainingCourseName.Location = New System.Drawing.Point(361, 37)
        Me.lblTrainingCourseName.Name = "lblTrainingCourseName"
        Me.lblTrainingCourseName.Size = New System.Drawing.Size(247, 19)
        Me.lblTrainingCourseName.TabIndex = 372
        Me.lblTrainingCourseName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'FndEmployee
        '
        Me.FndEmployee.Location = New System.Drawing.Point(427, 349)
        Me.FndEmployee.MendatroryField = True
        Me.FndEmployee.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndEmployee.MyLinkLable1 = Me.MyLabel3
        Me.FndEmployee.MyLinkLable2 = Nothing
        Me.FndEmployee.MyReadOnly = False
        Me.FndEmployee.MyShowMasterFormButton = False
        Me.FndEmployee.Name = "FndEmployee"
        Me.FndEmployee.Size = New System.Drawing.Size(51, 19)
        Me.FndEmployee.TabIndex = 3
        Me.FndEmployee.Value = ""
        Me.FndEmployee.Visible = False
        '
        'MyLabel3
        '
        Me.MyLabel3.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel3.Location = New System.Drawing.Point(361, 349)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(55, 18)
        Me.MyLabel3.TabIndex = 68
        Me.MyLabel3.Text = "Employee"
        Me.MyLabel3.Visible = False
        '
        'FndTrainingCourse
        '
        Me.FndTrainingCourse.Location = New System.Drawing.Point(102, 36)
        Me.FndTrainingCourse.MendatroryField = True
        Me.FndTrainingCourse.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndTrainingCourse.MyLinkLable1 = Me.MyLabel2
        Me.FndTrainingCourse.MyLinkLable2 = Nothing
        Me.FndTrainingCourse.MyReadOnly = False
        Me.FndTrainingCourse.MyShowMasterFormButton = False
        Me.FndTrainingCourse.Name = "FndTrainingCourse"
        Me.FndTrainingCourse.Size = New System.Drawing.Size(248, 19)
        Me.FndTrainingCourse.TabIndex = 2
        Me.FndTrainingCourse.Value = ""
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel2.Location = New System.Drawing.Point(14, 36)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(85, 18)
        Me.MyLabel2.TabIndex = 65
        Me.MyLabel2.Text = "Training Course"
        '
        'MyLabel6
        '
        Me.MyLabel6.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel6.Location = New System.Drawing.Point(361, 13)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(30, 18)
        Me.MyLabel6.TabIndex = 62
        Me.MyLabel6.Text = "Date"
        '
        'DtpDate
        '
        Me.DtpDate.CustomFormat = "dd/MM/yyyy"
        Me.DtpDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpDate.Location = New System.Drawing.Point(397, 12)
        Me.DtpDate.MendatroryField = True
        Me.DtpDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DtpDate.MyLinkLable1 = Nothing
        Me.DtpDate.MyLinkLable2 = Nothing
        Me.DtpDate.Name = "DtpDate"
        Me.DtpDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DtpDate.Size = New System.Drawing.Size(211, 18)
        Me.DtpDate.TabIndex = 1
        Me.DtpDate.TabStop = False
        Me.DtpDate.Text = "03/05/2011"
        Me.DtpDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'btnnew
        '
        Me.btnnew.Image = Global.XpertERPHRandPayroll.My.Resources.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(336, 11)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(15, 20)
        Me.btnnew.TabIndex = 1
        '
        'txt_remarks
        '
        Me.txt_remarks.AutoSize = False
        Me.txt_remarks.Location = New System.Drawing.Point(102, 60)
        Me.txt_remarks.Modified = True
        Me.txt_remarks.Multiline = True
        Me.txt_remarks.Name = "txt_remarks"
        Me.txt_remarks.Size = New System.Drawing.Size(506, 53)
        Me.txt_remarks.TabIndex = 4
        '
        'lbl_Name
        '
        Me.lbl_Name.Location = New System.Drawing.Point(14, 61)
        Me.lbl_Name.Name = "lbl_Name"
        Me.lbl_Name.Size = New System.Drawing.Size(44, 18)
        Me.lbl_Name.TabIndex = 20
        Me.lbl_Name.Text = "Remark"
        '
        'lbl_Code
        '
        Me.lbl_Code.Location = New System.Drawing.Point(14, 12)
        Me.lbl_Code.Name = "lbl_Code"
        Me.lbl_Code.Size = New System.Drawing.Size(32, 18)
        Me.lbl_Code.TabIndex = 19
        Me.lbl_Code.Text = "Code"
        '
        'txt_Code
        '
        Me.txt_Code.Location = New System.Drawing.Point(102, 11)
        Me.txt_Code.MendatroryField = True
        Me.txt_Code.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txt_Code.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txt_Code.MyLinkLable1 = Nothing
        Me.txt_Code.MyLinkLable2 = Nothing
        Me.txt_Code.MyMaxLength = 32767
        Me.txt_Code.MyReadOnly = False
        Me.txt_Code.Name = "txt_Code"
        Me.txt_Code.Size = New System.Drawing.Size(234, 20)
        Me.txt_Code.TabIndex = 0
        Me.txt_Code.Value = ""
        '
        'BtnPost
        '
        Me.BtnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnPost.Location = New System.Drawing.Point(160, 5)
        Me.BtnPost.Name = "BtnPost"
        Me.BtnPost.Size = New System.Drawing.Size(68, 21)
        Me.BtnPost.TabIndex = 1
        Me.BtnPost.Text = "Post"
        '
        'BtnClose
        '
        Me.BtnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnClose.Location = New System.Drawing.Point(663, 5)
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
        Me.RadMenu1.Size = New System.Drawing.Size(747, 20)
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
        'frmRequestForTrainingMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(747, 432)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmRequestForTrainingMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Request For Training"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.GrpEmp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpEmp.ResumeLayout(False)
        CType(Me.GrpDept, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpDept.ResumeLayout(False)
        CType(Me.LblEmployeeName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTrainingCourseName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DtpDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_remarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_Name, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_Code, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents txt_remarks As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RMFile As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RMImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RMExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents DtpDate As common.Controls.MyDateTimePicker
    Friend WithEvents FndTrainingCourse As common.UserControls.txtFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents FndEmployee As common.UserControls.txtFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents BtnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblTrainingCourseName As common.Controls.MyLabel
    Friend WithEvents LblEmployeeName As common.Controls.MyLabel
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents GrpDept As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgDept As common.MyCheckBoxGrid
    Friend WithEvents GrpEmp As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgEmp As common.MyCheckBoxGrid
End Class

