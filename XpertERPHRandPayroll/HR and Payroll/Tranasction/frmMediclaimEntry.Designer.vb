Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMediclaimEntry
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmMediclaimEntry))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.txttodate = New common.Controls.MyDateTimePicker
        Me.MyLabel3 = New common.Controls.MyLabel
        Me.txtfromdate = New common.Controls.MyDateTimePicker
        Me.lblAttendanceCode = New common.Controls.MyLabel
        Me.txtclaimamt = New common.Controls.MyTextBox
        Me.MyLabel4 = New common.Controls.MyLabel
        Me.MyLabel5 = New common.Controls.MyLabel
        Me.txtdesig = New common.Controls.MyLabel
        Me.btngo = New Telerik.WinControls.UI.RadButton
        Me.txtdepart = New common.Controls.MyLabel
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.txtdate = New common.Controls.MyDateTimePicker
        Me.MyLabel8 = New common.Controls.MyLabel
        Me.MyLabel7 = New common.Controls.MyLabel
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.txtdesc = New common.Controls.MyTextBox
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.txtdoj = New common.Controls.MyLabel
        Me.txtCode = New common.UserControls.txtNavigator
        Me.txtempname = New common.Controls.MyLabel
        Me.txtempcode = New common.UserControls.txtFinder
        Me.lblPayPeriod = New common.Controls.MyLabel
        Me.btnNew = New Telerik.WinControls.UI.RadButton
        Me.gv1 = New common.UserControls.MyRadGridView
        Me.UsLock1 = New common.usLock
        Me.btnPost = New Telerik.WinControls.UI.RadButton
        Me.btnsave = New Telerik.WinControls.UI.RadButton
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.btndelete = New Telerik.WinControls.UI.RadButton
        Me.btnprint = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.txttodate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtfromdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAttendanceCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtclaimamt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdesig, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btngo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdepart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdoj, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtempname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPayPeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.gv1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer1.Panel1.Padding = New System.Windows.Forms.Padding(3)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnprint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.SplitContainer1.Size = New System.Drawing.Size(852, 584)
        Me.SplitContainer1.SplitterDistance = 540
        Me.SplitContainer1.TabIndex = 0
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.txttodate)
        Me.RadGroupBox1.Controls.Add(Me.txtfromdate)
        Me.RadGroupBox1.Controls.Add(Me.lblAttendanceCode)
        Me.RadGroupBox1.Controls.Add(Me.txtclaimamt)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel5)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel4)
        Me.RadGroupBox1.Controls.Add(Me.txtdesig)
        Me.RadGroupBox1.Controls.Add(Me.btngo)
        Me.RadGroupBox1.Controls.Add(Me.txtdepart)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.txtdate)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel8)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel7)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.Controls.Add(Me.txtdesc)
        Me.RadGroupBox1.Controls.Add(Me.txtdoj)
        Me.RadGroupBox1.Controls.Add(Me.txtCode)
        Me.RadGroupBox1.Controls.Add(Me.txtempname)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox1.Controls.Add(Me.txtempcode)
        Me.RadGroupBox1.Controls.Add(Me.lblPayPeriod)
        Me.RadGroupBox1.Controls.Add(Me.btnNew)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(6, 6)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(600, 235)
        Me.RadGroupBox1.TabIndex = 233
        '
        'txttodate
        '
        Me.txttodate.CustomFormat = "dd/MM/yyyy"
        Me.txttodate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txttodate.Location = New System.Drawing.Point(107, 180)
        Me.txttodate.MendatroryField = False
        Me.txttodate.MyLinkLable1 = Me.MyLabel3
        Me.txttodate.MyLinkLable2 = Nothing
        Me.txttodate.Name = "txttodate"
        Me.txttodate.Size = New System.Drawing.Size(158, 20)
        Me.txttodate.TabIndex = 5
        Me.txttodate.TabStop = False
        Me.txttodate.Text = "23/04/2014"
        Me.txttodate.Value = New Date(2014, 4, 23, 15, 16, 19, 779)
        '
        'MyLabel3
        '
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(376, 12)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(30, 16)
        Me.MyLabel3.TabIndex = 225
        Me.MyLabel3.Text = "Date"
        '
        'txtfromdate
        '
        Me.txtfromdate.CustomFormat = "dd/MM/yyyy"
        Me.txtfromdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtfromdate.Location = New System.Drawing.Point(107, 155)
        Me.txtfromdate.MendatroryField = False
        Me.txtfromdate.MyLinkLable1 = Me.MyLabel3
        Me.txtfromdate.MyLinkLable2 = Nothing
        Me.txtfromdate.Name = "txtfromdate"
        Me.txtfromdate.Size = New System.Drawing.Size(158, 20)
        Me.txtfromdate.TabIndex = 4
        Me.txtfromdate.TabStop = False
        Me.txtfromdate.Text = "23/04/2014"
        Me.txtfromdate.Value = New Date(2014, 4, 23, 15, 16, 19, 779)
        '
        'lblAttendanceCode
        '
        Me.lblAttendanceCode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblAttendanceCode.Location = New System.Drawing.Point(5, 12)
        Me.lblAttendanceCode.Name = "lblAttendanceCode"
        Me.lblAttendanceCode.Size = New System.Drawing.Size(87, 16)
        Me.lblAttendanceCode.TabIndex = 181
        Me.lblAttendanceCode.Text = "Mediclaim Code"
        '
        'txtclaimamt
        '
        Me.txtclaimamt.Location = New System.Drawing.Point(107, 204)
        Me.txtclaimamt.MendatroryField = False
        Me.txtclaimamt.MyLinkLable1 = Me.MyLabel4
        Me.txtclaimamt.MyLinkLable2 = Nothing
        Me.txtclaimamt.Name = "txtclaimamt"
        Me.txtclaimamt.ReadOnly = True
        Me.txtclaimamt.Size = New System.Drawing.Size(163, 20)
        Me.txtclaimamt.TabIndex = 232
        '
        'MyLabel4
        '
        Me.MyLabel4.Location = New System.Drawing.Point(6, 204)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(86, 18)
        Me.MyLabel4.TabIndex = 231
        Me.MyLabel4.Text = "Total Claim Amt"
        '
        'MyLabel5
        '
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(6, 107)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel5.TabIndex = 201
        Me.MyLabel5.Text = "Department"
        '
        'txtdesig
        '
        Me.txtdesig.AutoSize = False
        Me.txtdesig.BorderVisible = True
        Me.txtdesig.Location = New System.Drawing.Point(107, 84)
        Me.txtdesig.Name = "txtdesig"
        Me.txtdesig.Size = New System.Drawing.Size(461, 19)
        Me.txtdesig.TabIndex = 200
        Me.txtdesig.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btngo
        '
        Me.btngo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btngo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btngo.Location = New System.Drawing.Point(494, 204)
        Me.btngo.Name = "btngo"
        Me.btngo.Size = New System.Drawing.Size(74, 18)
        Me.btngo.TabIndex = 6
        Me.btngo.Text = ">>"
        '
        'txtdepart
        '
        Me.txtdepart.AutoSize = False
        Me.txtdepart.BorderVisible = True
        Me.txtdepart.Location = New System.Drawing.Point(107, 107)
        Me.txtdepart.Name = "txtdepart"
        Me.txtdepart.Size = New System.Drawing.Size(461, 19)
        Me.txtdepart.TabIndex = 202
        Me.txtdepart.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(6, 84)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(66, 16)
        Me.MyLabel2.TabIndex = 199
        Me.MyLabel2.Text = "Designation"
        '
        'txtdate
        '
        Me.txtdate.CustomFormat = "dd/MM/yyyy"
        Me.txtdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtdate.Location = New System.Drawing.Point(409, 10)
        Me.txtdate.MendatroryField = False
        Me.txtdate.MyLinkLable1 = Me.MyLabel3
        Me.txtdate.MyLinkLable2 = Nothing
        Me.txtdate.Name = "txtdate"
        Me.txtdate.Size = New System.Drawing.Size(158, 20)
        Me.txtdate.TabIndex = 1
        Me.txtdate.TabStop = False
        Me.txtdate.Text = "23/04/2014"
        Me.txtdate.Value = New Date(2014, 4, 23, 15, 16, 19, 779)
        '
        'MyLabel8
        '
        Me.MyLabel8.Location = New System.Drawing.Point(6, 180)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(85, 18)
        Me.MyLabel8.TabIndex = 222
        Me.MyLabel8.Text = "To Claim Period"
        '
        'MyLabel7
        '
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(6, 132)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(84, 16)
        Me.MyLabel7.TabIndex = 203
        Me.MyLabel7.Text = "Date Of Joining"
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(6, 155)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(99, 18)
        Me.RadLabel1.TabIndex = 218
        Me.RadLabel1.Text = "From Claim Period"
        '
        'txtdesc
        '
        Me.txtdesc.Location = New System.Drawing.Point(107, 35)
        Me.txtdesc.MendatroryField = False
        Me.txtdesc.MyLinkLable1 = Me.MyLabel1
        Me.txtdesc.MyLinkLable2 = Nothing
        Me.txtdesc.Name = "txtdesc"
        Me.txtdesc.Size = New System.Drawing.Size(461, 20)
        Me.txtdesc.TabIndex = 2
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(6, 37)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel1.TabIndex = 197
        Me.MyLabel1.Text = "Description"
        '
        'txtdoj
        '
        Me.txtdoj.AutoSize = False
        Me.txtdoj.BorderVisible = True
        Me.txtdoj.Location = New System.Drawing.Point(107, 131)
        Me.txtdoj.Name = "txtdoj"
        Me.txtdoj.Size = New System.Drawing.Size(191, 19)
        Me.txtdoj.TabIndex = 204
        Me.txtdoj.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCode
        '
        Me.txtCode.Location = New System.Drawing.Point(106, 9)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblAttendanceCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 12
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(244, 21)
        Me.txtCode.TabIndex = 1
        Me.txtCode.Value = ""
        '
        'txtempname
        '
        Me.txtempname.AutoSize = False
        Me.txtempname.BorderVisible = True
        Me.txtempname.Location = New System.Drawing.Point(276, 60)
        Me.txtempname.Name = "txtempname"
        Me.txtempname.Size = New System.Drawing.Size(292, 19)
        Me.txtempname.TabIndex = 196
        Me.txtempname.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtempcode
        '
        Me.txtempcode.Location = New System.Drawing.Point(107, 60)
        Me.txtempcode.MendatroryField = True
        Me.txtempcode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtempcode.MyLinkLable1 = Me.lblPayPeriod
        Me.txtempcode.MyLinkLable2 = Me.txtempname
        Me.txtempcode.MyReadOnly = False
        Me.txtempcode.Name = "txtempcode"
        Me.txtempcode.Size = New System.Drawing.Size(163, 19)
        Me.txtempcode.TabIndex = 3
        Me.txtempcode.Value = ""
        '
        'lblPayPeriod
        '
        Me.lblPayPeriod.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPayPeriod.Location = New System.Drawing.Point(6, 61)
        Me.lblPayPeriod.Name = "lblPayPeriod"
        Me.lblPayPeriod.Size = New System.Drawing.Size(90, 16)
        Me.lblPayPeriod.TabIndex = 183
        Me.lblPayPeriod.Text = "Employee Name"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(356, 9)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 20)
        Me.btnNew.TabIndex = 0
        Me.btnNew.Text = " "
        '
        'gv1
        '
        Me.gv1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.gv1.Location = New System.Drawing.Point(6, 247)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.AllowDragToGroup = False
        Me.gv1.Name = "gv1"
        Me.gv1.Size = New System.Drawing.Size(838, 287)
        Me.gv1.TabIndex = 0
        Me.gv1.Text = "RadGridView1"
        '
        'UsLock1
        '
        Me.UsLock1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(754, 10)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(91, 21)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 230
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(88, 7)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(74, 22)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(9, 7)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(74, 22)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(771, 7)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(74, 22)
        Me.btnclose.TabIndex = 3
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(167, 7)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(74, 22)
        Me.btndelete.TabIndex = 2
        Me.btndelete.Text = "Delete"
        '
        'btnprint
        '
        Me.btnprint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnprint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnprint.Location = New System.Drawing.Point(247, 7)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(74, 22)
        Me.btnprint.TabIndex = 4
        Me.btnprint.Text = "print"
        '
        'FrmMediclaimEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(852, 584)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmMediclaimEntry"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Mediclaim Entry"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.txttodate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtfromdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAttendanceCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtclaimamt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdesig, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btngo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdepart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdoj, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtempname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPayPeriod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents lblAttendanceCode As common.Controls.MyLabel
    Friend WithEvents txtempcode As common.UserControls.txtFinder
    Friend WithEvents lblPayPeriod As common.Controls.MyLabel
    Friend WithEvents txtempname As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtdoj As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents txtdepart As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtdesig As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtdesc As common.Controls.MyTextBox
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents txtdate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents btngo As Telerik.WinControls.UI.RadButton
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents txtclaimamt As common.Controls.MyTextBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txttodate As common.Controls.MyDateTimePicker
    Friend WithEvents txtfromdate As common.Controls.MyDateTimePicker
    Friend WithEvents btnprint As Telerik.WinControls.UI.RadButton
End Class

