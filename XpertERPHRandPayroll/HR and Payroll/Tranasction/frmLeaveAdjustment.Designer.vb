Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLeaveAdjustment
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLeaveAdjustment))
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.UsLock1 = New common.usLock
        Me.txtAdjustAvail = New common.MyNumBox
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.txtAdjustAlloted = New common.MyNumBox
        Me.MyLabel3 = New common.Controls.MyLabel
        Me.btnNew = New Telerik.WinControls.UI.RadButton
        Me.txtReason = New common.Controls.MyTextBox
        Me.RadLabel3 = New common.Controls.MyLabel
        Me.dtpAdjustDate = New common.Controls.MyDateTimePicker
        Me.lblOpeningDate = New common.Controls.MyLabel
        Me.lblPayPeriodName = New common.Controls.MyLabel
        Me.lblEmpName = New common.Controls.MyLabel
        Me.lblLeaveName = New common.Controls.MyLabel
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.txtLeaveCode = New common.UserControls.txtFinder
        Me.txtPayPeriodCode = New common.UserControls.txtFinder
        Me.MyLabel4 = New common.Controls.MyLabel
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.txtEmpCode = New common.UserControls.txtFinder
        Me.txtCode = New common.UserControls.txtNavigator
        Me.lblLeaveAdj = New common.Controls.MyLabel
        Me.btnPost = New Telerik.WinControls.UI.RadButton
        Me.btnsave = New Telerik.WinControls.UI.RadButton
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.btndelete = New Telerik.WinControls.UI.RadButton
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.txtAdjustAvail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAdjustAlloted, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtReason, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpAdjustDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblOpeningDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPayPeriodName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmpName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLeaveName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLeaveAdj, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.RadGroupBox3.Size = New System.Drawing.Size(683, 487)
        Me.RadGroupBox3.TabIndex = 62
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(10, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtAdjustAvail)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtAdjustAlloted)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtReason)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpAdjustDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblOpeningDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblPayPeriodName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblEmpName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLeaveName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtLeaveCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtPayPeriodCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtEmpCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLeaveAdj)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel4)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(663, 457)
        Me.SplitContainer1.SplitterDistance = 414
        Me.SplitContainer1.TabIndex = 0
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(472, 16)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 188
        '
        'txtAdjustAvail
        '
        Me.txtAdjustAvail.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtAdjustAvail.DecimalPlaces = 2
        Me.txtAdjustAvail.Location = New System.Drawing.Point(155, 138)
        Me.txtAdjustAvail.MaxLength = 6
        Me.txtAdjustAvail.MendatroryField = True
        Me.txtAdjustAvail.MyLinkLable1 = Me.MyLabel2
        Me.txtAdjustAvail.MyLinkLable2 = Nothing
        Me.txtAdjustAvail.Name = "txtAdjustAvail"
        Me.txtAdjustAvail.Size = New System.Drawing.Size(221, 20)
        Me.txtAdjustAvail.TabIndex = 9
        Me.txtAdjustAvail.Text = "0"
        Me.txtAdjustAvail.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtAdjustAvail.Value = 0
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(11, 65)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(67, 16)
        Me.MyLabel2.TabIndex = 163
        Me.MyLabel2.Text = "Leave Code"
        '
        'txtAdjustAlloted
        '
        Me.txtAdjustAlloted.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtAdjustAlloted.DecimalPlaces = 2
        Me.txtAdjustAlloted.Location = New System.Drawing.Point(155, 112)
        Me.txtAdjustAlloted.MaxLength = 6
        Me.txtAdjustAlloted.MendatroryField = True
        Me.txtAdjustAlloted.MyLinkLable1 = Me.MyLabel3
        Me.txtAdjustAlloted.MyLinkLable2 = Nothing
        Me.txtAdjustAlloted.Name = "txtAdjustAlloted"
        Me.txtAdjustAlloted.Size = New System.Drawing.Size(221, 20)
        Me.txtAdjustAlloted.TabIndex = 8
        Me.txtAdjustAlloted.Text = "0"
        Me.txtAdjustAlloted.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtAdjustAlloted.Value = 0
        '
        'MyLabel3
        '
        Me.MyLabel3.Location = New System.Drawing.Point(11, 113)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(77, 18)
        Me.MyLabel3.TabIndex = 167
        Me.MyLabel3.Text = "Adjust Alloted"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(381, 16)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 20)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Text = " "
        '
        'txtReason
        '
        Me.txtReason.Location = New System.Drawing.Point(155, 188)
        Me.txtReason.MaxLength = 50
        Me.txtReason.MendatroryField = True
        Me.txtReason.MyLinkLable1 = Me.RadLabel3
        Me.txtReason.MyLinkLable2 = Nothing
        Me.txtReason.Name = "txtReason"
        Me.txtReason.Size = New System.Drawing.Size(221, 20)
        Me.txtReason.TabIndex = 11
        '
        'RadLabel3
        '
        Me.RadLabel3.Location = New System.Drawing.Point(11, 189)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(74, 18)
        Me.RadLabel3.TabIndex = 174
        Me.RadLabel3.Text = "Leave Reason"
        '
        'dtpAdjustDate
        '
        Me.dtpAdjustDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpAdjustDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpAdjustDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpAdjustDate.Location = New System.Drawing.Point(155, 164)
        Me.dtpAdjustDate.MendatroryField = True
        Me.dtpAdjustDate.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.dtpAdjustDate.MyLinkLable1 = Me.lblOpeningDate
        Me.dtpAdjustDate.MyLinkLable2 = Nothing
        Me.dtpAdjustDate.Name = "dtpAdjustDate"
        Me.dtpAdjustDate.NullDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.dtpAdjustDate.Size = New System.Drawing.Size(220, 18)
        Me.dtpAdjustDate.TabIndex = 10
        Me.dtpAdjustDate.TabStop = False
        Me.dtpAdjustDate.Text = "03/05/2011"
        Me.dtpAdjustDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblOpeningDate
        '
        Me.lblOpeningDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOpeningDate.Location = New System.Drawing.Point(11, 165)
        Me.lblOpeningDate.Name = "lblOpeningDate"
        Me.lblOpeningDate.Size = New System.Drawing.Size(90, 16)
        Me.lblOpeningDate.TabIndex = 172
        Me.lblOpeningDate.Text = "Adjustment Date"
        '
        'lblPayPeriodName
        '
        Me.lblPayPeriodName.AutoSize = False
        Me.lblPayPeriodName.BorderVisible = True
        Me.lblPayPeriodName.Location = New System.Drawing.Point(381, 88)
        Me.lblPayPeriodName.Name = "lblPayPeriodName"
        Me.lblPayPeriodName.Size = New System.Drawing.Size(189, 18)
        Me.lblPayPeriodName.TabIndex = 7
        Me.lblPayPeriodName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblEmpName
        '
        Me.lblEmpName.AutoSize = False
        Me.lblEmpName.BorderVisible = True
        Me.lblEmpName.Location = New System.Drawing.Point(381, 40)
        Me.lblEmpName.Name = "lblEmpName"
        Me.lblEmpName.Size = New System.Drawing.Size(189, 18)
        Me.lblEmpName.TabIndex = 3
        Me.lblEmpName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblLeaveName
        '
        Me.lblLeaveName.AutoSize = False
        Me.lblLeaveName.BorderVisible = True
        Me.lblLeaveName.Location = New System.Drawing.Point(381, 64)
        Me.lblLeaveName.Name = "lblLeaveName"
        Me.lblLeaveName.Size = New System.Drawing.Size(189, 18)
        Me.lblLeaveName.TabIndex = 5
        Me.lblLeaveName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(11, 139)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(78, 18)
        Me.RadLabel2.TabIndex = 164
        Me.RadLabel2.Text = "Adjust Availed"
        '
        'txtLeaveCode
        '
        Me.txtLeaveCode.Location = New System.Drawing.Point(155, 64)
        Me.txtLeaveCode.MendatroryField = True
        Me.txtLeaveCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLeaveCode.MyLinkLable1 = Me.MyLabel2
        Me.txtLeaveCode.MyLinkLable2 = Me.lblLeaveName
        Me.txtLeaveCode.MyReadOnly = False
        Me.txtLeaveCode.Name = "txtLeaveCode"
        Me.txtLeaveCode.Size = New System.Drawing.Size(221, 19)
        Me.txtLeaveCode.TabIndex = 4
        Me.txtLeaveCode.Value = ""
        '
        'txtPayPeriodCode
        '
        Me.txtPayPeriodCode.Location = New System.Drawing.Point(156, 86)
        Me.txtPayPeriodCode.MendatroryField = True
        Me.txtPayPeriodCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPayPeriodCode.MyLinkLable1 = Me.MyLabel4
        Me.txtPayPeriodCode.MyLinkLable2 = Me.lblPayPeriodName
        Me.txtPayPeriodCode.MyReadOnly = False
        Me.txtPayPeriodCode.Name = "txtPayPeriodCode"
        Me.txtPayPeriodCode.Size = New System.Drawing.Size(221, 19)
        Me.txtPayPeriodCode.TabIndex = 6
        Me.txtPayPeriodCode.Value = ""
        '
        'MyLabel4
        '
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(11, 89)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(92, 16)
        Me.MyLabel4.TabIndex = 153
        Me.MyLabel4.Text = "Pay Period Code"
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(11, 41)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(87, 16)
        Me.MyLabel1.TabIndex = 160
        Me.MyLabel1.Text = "Employee Code"
        '
        'txtEmpCode
        '
        Me.txtEmpCode.Location = New System.Drawing.Point(155, 40)
        Me.txtEmpCode.MendatroryField = True
        Me.txtEmpCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmpCode.MyLinkLable1 = Me.MyLabel1
        Me.txtEmpCode.MyLinkLable2 = Me.lblEmpName
        Me.txtEmpCode.MyReadOnly = False
        Me.txtEmpCode.Name = "txtEmpCode"
        Me.txtEmpCode.Size = New System.Drawing.Size(221, 19)
        Me.txtEmpCode.TabIndex = 2
        Me.txtEmpCode.Value = ""
        '
        'txtCode
        '
        Me.txtCode.Location = New System.Drawing.Point(155, 15)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblLeaveAdj
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 12
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(221, 21)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'lblLeaveAdj
        '
        Me.lblLeaveAdj.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLeaveAdj.Location = New System.Drawing.Point(11, 17)
        Me.lblLeaveAdj.Name = "lblLeaveAdj"
        Me.lblLeaveAdj.Size = New System.Drawing.Size(127, 16)
        Me.lblLeaveAdj.TabIndex = 158
        Me.lblLeaveAdj.Text = "Leave Adjustment Code"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(78, 12)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(66, 18)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(9, 12)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(588, 12)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 3
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(147, 12)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 2
        Me.btndelete.Text = "Delete"
        '
        'frmLeaveAdjustment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(683, 487)
        Me.Controls.Add(Me.RadGroupBox3)
        Me.Name = "frmLeaveAdjustment"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Leave Adjustment"
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.txtAdjustAvail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAdjustAlloted, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtReason, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpAdjustDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblOpeningDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPayPeriodName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmpName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLeaveName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLeaveAdj, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtEmpCode As common.UserControls.txtFinder
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents lblLeaveAdj As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtPayPeriodCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtLeaveCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents lblPayPeriodName As common.Controls.MyLabel
    Friend WithEvents lblEmpName As common.Controls.MyLabel
    Friend WithEvents lblLeaveName As common.Controls.MyLabel
    Friend WithEvents dtpAdjustDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblOpeningDate As common.Controls.MyLabel
    Friend WithEvents txtReason As common.Controls.MyTextBox
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtAdjustAvail As common.MyNumBox
    Friend WithEvents txtAdjustAlloted As common.MyNumBox
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
End Class
