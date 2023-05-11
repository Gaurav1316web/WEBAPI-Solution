Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLTAClaim
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLTAClaim))
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.lblTotalAmount = New common.Controls.MyLabel
        Me.lblTotal = New common.Controls.MyLabel
        Me.dtToDate = New common.Controls.MyDateTimePicker
        Me.lblToDate = New common.Controls.MyLabel
        Me.dtFDate = New common.Controls.MyDateTimePicker
        Me.gvClaimPeriod = New common.UserControls.MyRadGridView
        Me.txtDOJ = New common.Controls.MyLabel
        Me.lblDeptCode = New common.Controls.MyLabel
        Me.lblDesignationCode = New common.Controls.MyLabel
        Me.lblDeptName = New common.Controls.MyLabel
        Me.UsLock1 = New common.usLock
        Me.btnNew = New Telerik.WinControls.UI.RadButton
        Me.lblDesignationName = New common.Controls.MyLabel
        Me.lblEmpName = New common.Controls.MyLabel
        Me.lblFromPayPeriod = New common.Controls.MyLabel
        Me.lblDesignation = New common.Controls.MyLabel
        Me.lblDOJ = New common.Controls.MyLabel
        Me.lblEmpCode = New common.Controls.MyLabel
        Me.txtEmpCode = New common.UserControls.txtFinder
        Me.txtCode = New common.UserControls.txtNavigator
        Me.lblLTACode = New common.Controls.MyLabel
        Me.lblDepartment = New common.Controls.MyLabel
        Me.btnPrint = New Telerik.WinControls.UI.RadButton
        Me.btnShow = New Telerik.WinControls.UI.RadButton
        Me.btnPost = New Telerik.WinControls.UI.RadButton
        Me.btnsave = New Telerik.WinControls.UI.RadButton
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.btndelete = New Telerik.WinControls.UI.RadButton
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.lblTotalAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtFDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvClaimPeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvClaimPeriod.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDOJ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDeptCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDesignationCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDeptName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDesignationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmpName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFromPayPeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDesignation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDOJ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmpCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLTACode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDepartment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnShow, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.SplitContainer1)
        Me.RadGroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox3.HeaderText = ""
        Me.RadGroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(587, 536)
        Me.RadGroupBox3.TabIndex = 64
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(10, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblTotalAmount)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblTotal)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtToDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblToDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtFDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.gvClaimPeriod)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDOJ)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDeptCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDesignationCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDeptName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDesignationName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblEmpName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblFromPayPeriod)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDesignation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDOJ)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblEmpCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtEmpCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLTACode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDepartment)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnShow)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(567, 506)
        Me.SplitContainer1.SplitterDistance = 464
        Me.SplitContainer1.TabIndex = 157
        '
        'lblTotalAmount
        '
        Me.lblTotalAmount.AutoSize = False
        Me.lblTotalAmount.BorderVisible = True
        Me.lblTotalAmount.Location = New System.Drawing.Point(154, 139)
        Me.lblTotalAmount.Name = "lblTotalAmount"
        Me.lblTotalAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotalAmount.Size = New System.Drawing.Size(140, 19)
        Me.lblTotalAmount.TabIndex = 202
        Me.lblTotalAmount.Text = "0"
        Me.lblTotalAmount.TextAlignment = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotal
        '
        Me.lblTotal.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTotal.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotal.Location = New System.Drawing.Point(14, 142)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(131, 16)
        Me.lblTotal.TabIndex = 178
        Me.lblTotal.Text = "Medical Amount Payable"
        '
        'dtToDate
        '
        Me.dtToDate.CustomFormat = "dd/MM/yyyy"
        Me.dtToDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtToDate.Location = New System.Drawing.Point(411, 163)
        Me.dtToDate.MendatroryField = True
        Me.dtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtToDate.MyLinkLable1 = Nothing
        Me.dtToDate.MyLinkLable2 = Nothing
        Me.dtToDate.Name = "dtToDate"
        Me.dtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtToDate.Size = New System.Drawing.Size(132, 18)
        Me.dtToDate.TabIndex = 210
        Me.dtToDate.TabStop = False
        Me.dtToDate.Text = "03/05/2011"
        Me.dtToDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblToDate
        '
        Me.lblToDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblToDate.Location = New System.Drawing.Point(300, 165)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(46, 16)
        Me.lblToDate.TabIndex = 178
        Me.lblToDate.Text = "To Date"
        '
        'dtFDate
        '
        Me.dtFDate.CustomFormat = "dd/MM/yyyy"
        Me.dtFDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtFDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtFDate.Location = New System.Drawing.Point(114, 165)
        Me.dtFDate.MendatroryField = True
        Me.dtFDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtFDate.MyLinkLable1 = Nothing
        Me.dtFDate.MyLinkLable2 = Nothing
        Me.dtFDate.Name = "dtFDate"
        Me.dtFDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtFDate.Size = New System.Drawing.Size(132, 18)
        Me.dtFDate.TabIndex = 209
        Me.dtFDate.TabStop = False
        Me.dtFDate.Text = "03/05/2011"
        Me.dtFDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'gvClaimPeriod
        '
        Me.gvClaimPeriod.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gvClaimPeriod.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvClaimPeriod.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvClaimPeriod.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvClaimPeriod.ForeColor = System.Drawing.Color.Black
        Me.gvClaimPeriod.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvClaimPeriod.Location = New System.Drawing.Point(14, 197)
        '
        'gvClaimPeriod
        '
        Me.gvClaimPeriod.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvClaimPeriod.MasterTemplate.AllowAddNewRow = False
        Me.gvClaimPeriod.MasterTemplate.EnableGrouping = False
        Me.gvClaimPeriod.Name = "gvClaimPeriod"
        Me.gvClaimPeriod.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvClaimPeriod.Size = New System.Drawing.Size(529, 260)
        Me.gvClaimPeriod.TabIndex = 208
        Me.gvClaimPeriod.Text = "RadGridView1"
        '
        'txtDOJ
        '
        Me.txtDOJ.AutoSize = False
        Me.txtDOJ.BorderVisible = True
        Me.txtDOJ.Location = New System.Drawing.Point(114, 69)
        Me.txtDOJ.Name = "txtDOJ"
        Me.txtDOJ.Size = New System.Drawing.Size(180, 19)
        Me.txtDOJ.TabIndex = 202
        Me.txtDOJ.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDeptCode
        '
        Me.lblDeptCode.AutoSize = False
        Me.lblDeptCode.BorderVisible = True
        Me.lblDeptCode.Location = New System.Drawing.Point(114, 115)
        Me.lblDeptCode.Name = "lblDeptCode"
        Me.lblDeptCode.Size = New System.Drawing.Size(180, 19)
        Me.lblDeptCode.TabIndex = 201
        Me.lblDeptCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDesignationCode
        '
        Me.lblDesignationCode.AutoSize = False
        Me.lblDesignationCode.BorderVisible = True
        Me.lblDesignationCode.Location = New System.Drawing.Point(114, 90)
        Me.lblDesignationCode.Name = "lblDesignationCode"
        Me.lblDesignationCode.Size = New System.Drawing.Size(180, 19)
        Me.lblDesignationCode.TabIndex = 201
        Me.lblDesignationCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDeptName
        '
        Me.lblDeptName.AutoSize = False
        Me.lblDeptName.BorderVisible = True
        Me.lblDeptName.Location = New System.Drawing.Point(300, 115)
        Me.lblDeptName.Name = "lblDeptName"
        Me.lblDeptName.Size = New System.Drawing.Size(243, 19)
        Me.lblDeptName.TabIndex = 201
        Me.lblDeptName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(361, 17)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 204
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(341, 18)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 20)
        Me.btnNew.TabIndex = 0
        Me.btnNew.Text = " "
        '
        'lblDesignationName
        '
        Me.lblDesignationName.AutoSize = False
        Me.lblDesignationName.BorderVisible = True
        Me.lblDesignationName.Location = New System.Drawing.Point(300, 90)
        Me.lblDesignationName.Name = "lblDesignationName"
        Me.lblDesignationName.Size = New System.Drawing.Size(243, 19)
        Me.lblDesignationName.TabIndex = 200
        Me.lblDesignationName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblEmpName
        '
        Me.lblEmpName.AutoSize = False
        Me.lblEmpName.BorderVisible = True
        Me.lblEmpName.Location = New System.Drawing.Point(300, 45)
        Me.lblEmpName.Name = "lblEmpName"
        Me.lblEmpName.Size = New System.Drawing.Size(243, 19)
        Me.lblEmpName.TabIndex = 199
        Me.lblEmpName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFromPayPeriod
        '
        Me.lblFromPayPeriod.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFromPayPeriod.Location = New System.Drawing.Point(14, 165)
        Me.lblFromPayPeriod.Name = "lblFromPayPeriod"
        Me.lblFromPayPeriod.Size = New System.Drawing.Size(60, 16)
        Me.lblFromPayPeriod.TabIndex = 177
        Me.lblFromPayPeriod.Text = "From Date"
        '
        'lblDesignation
        '
        Me.lblDesignation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDesignation.Location = New System.Drawing.Point(14, 93)
        Me.lblDesignation.Name = "lblDesignation"
        Me.lblDesignation.Size = New System.Drawing.Size(66, 16)
        Me.lblDesignation.TabIndex = 165
        Me.lblDesignation.Text = "Designation"
        '
        'lblDOJ
        '
        Me.lblDOJ.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDOJ.Location = New System.Drawing.Point(14, 69)
        Me.lblDOJ.Name = "lblDOJ"
        Me.lblDOJ.Size = New System.Drawing.Size(82, 16)
        Me.lblDOJ.TabIndex = 164
        Me.lblDOJ.Text = "Date of Joining"
        '
        'lblEmpCode
        '
        Me.lblEmpCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmpCode.Location = New System.Drawing.Point(13, 48)
        Me.lblEmpCode.Name = "lblEmpCode"
        Me.lblEmpCode.Size = New System.Drawing.Size(87, 16)
        Me.lblEmpCode.TabIndex = 154
        Me.lblEmpCode.Text = "Employee Code"
        '
        'txtEmpCode
        '
        Me.txtEmpCode.Location = New System.Drawing.Point(114, 45)
        Me.txtEmpCode.MendatroryField = True
        Me.txtEmpCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmpCode.MyLinkLable1 = Me.lblEmpCode
        Me.txtEmpCode.MyLinkLable2 = Nothing
        Me.txtEmpCode.MyReadOnly = False
        Me.txtEmpCode.Name = "txtEmpCode"
        Me.txtEmpCode.Size = New System.Drawing.Size(180, 19)
        Me.txtEmpCode.TabIndex = 1
        Me.txtEmpCode.Value = ""
        '
        'txtCode
        '
        Me.txtCode.Location = New System.Drawing.Point(114, 17)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblLTACode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 12
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(221, 21)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'lblLTACode
        '
        Me.lblLTACode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLTACode.Location = New System.Drawing.Point(14, 24)
        Me.lblLTACode.Name = "lblLTACode"
        Me.lblLTACode.Size = New System.Drawing.Size(57, 16)
        Me.lblLTACode.TabIndex = 161
        Me.lblLTACode.Text = "LTA Code"
        '
        'lblDepartment
        '
        Me.lblDepartment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDepartment.Location = New System.Drawing.Point(14, 118)
        Me.lblDepartment.Name = "lblDepartment"
        Me.lblDepartment.Size = New System.Drawing.Size(65, 16)
        Me.lblDepartment.TabIndex = 153
        Me.lblDepartment.Text = "Department"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Enabled = False
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(295, 11)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(66, 18)
        Me.btnPrint.TabIndex = 18
        Me.btnPrint.Text = "Print"
        '
        'btnShow
        '
        Me.btnShow.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnShow.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShow.Location = New System.Drawing.Point(14, 11)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(66, 18)
        Me.btnShow.TabIndex = 17
        Me.btnShow.Text = "Show"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(154, 11)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(66, 18)
        Me.btnPost.TabIndex = 132
        Me.btnPost.Text = "Post"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(85, 11)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 16
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(477, 11)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 18
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(223, 11)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 17
        Me.btndelete.Text = "Delete"
        '
        'frmLTAClaim
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(587, 536)
        Me.Controls.Add(Me.RadGroupBox3)
        Me.Name = "frmLTAClaim"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "LTA Claim"
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.lblTotalAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtFDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvClaimPeriod.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvClaimPeriod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDOJ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDeptCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDesignationCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDeptName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDesignationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmpName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFromPayPeriod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDesignation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDOJ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmpCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLTACode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDepartment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnShow, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblFromPayPeriod As common.Controls.MyLabel
    Friend WithEvents lblDesignation As common.Controls.MyLabel
    Friend WithEvents lblDOJ As common.Controls.MyLabel
    Friend WithEvents lblEmpCode As common.Controls.MyLabel
    Friend WithEvents txtEmpCode As common.UserControls.txtFinder
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents lblLTACode As common.Controls.MyLabel
    Friend WithEvents lblDepartment As common.Controls.MyLabel
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblDesignationName As common.Controls.MyLabel
    Friend WithEvents lblEmpName As common.Controls.MyLabel
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblDeptCode As common.Controls.MyLabel
    Friend WithEvents lblDesignationCode As common.Controls.MyLabel
    Friend WithEvents lblDeptName As common.Controls.MyLabel
    Friend WithEvents txtDOJ As common.Controls.MyLabel
    Friend WithEvents gvClaimPeriod As common.UserControls.MyRadGridView
    Friend WithEvents btnShow As Telerik.WinControls.UI.RadButton
    Friend WithEvents dtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblToDate As common.Controls.MyLabel
    Friend WithEvents dtFDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblTotal As common.Controls.MyLabel
    Friend WithEvents lblTotalAmount As common.Controls.MyLabel
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
End Class
