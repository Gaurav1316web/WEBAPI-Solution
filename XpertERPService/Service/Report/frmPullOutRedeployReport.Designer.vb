Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPullOutRedeployReport
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
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.DateRange = New Telerik.WinControls.UI.RadGroupBox
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.dtpToDate = New Telerik.WinControls.UI.RadDateTimePicker
        Me.dtpFrmDate = New Telerik.WinControls.UI.RadDateTimePicker
        Me.btnreset = New System.Windows.Forms.Button
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.btnClose = New System.Windows.Forms.Button
        Me.btnprint = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.cbgAssets = New common.MyCheckBoxGrid
        Me.Panel7 = New System.Windows.Forms.Panel
        Me.rbtnvselect = New common.Controls.MyRadioButton
        Me.rbtnvAll = New common.Controls.MyRadioButton
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.radioPulloutAndRedeploy = New System.Windows.Forms.RadioButton
        Me.radioPullout = New System.Windows.Forms.RadioButton
        Me.radioInstall = New System.Windows.Forms.RadioButton
        Me.CustomerGroupBox = New System.Windows.Forms.GroupBox
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.cbgCustomer = New common.MyCheckBoxGrid
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.MyRadioButton1 = New common.Controls.MyRadioButton
        Me.MyRadioButton2 = New common.Controls.MyRadioButton
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DateRange, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.DateRange.SuspendLayout()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFrmDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel7.SuspendLayout()
        CType(Me.rbtnvselect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnvAll, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.CustomerGroupBox.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.MyRadioButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyRadioButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(141, 14)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(19, 18)
        Me.RadLabel2.TabIndex = 3
        Me.RadLabel2.Text = "To"
        '
        'DateRange
        '
        Me.DateRange.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.DateRange.Controls.Add(Me.RadLabel2)
        Me.DateRange.Controls.Add(Me.RadLabel1)
        Me.DateRange.Controls.Add(Me.dtpToDate)
        Me.DateRange.Controls.Add(Me.dtpFrmDate)
        Me.DateRange.HeaderText = "Date Range"
        Me.DateRange.Location = New System.Drawing.Point(12, 77)
        Me.DateRange.Name = "DateRange"
        Me.DateRange.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.DateRange.Size = New System.Drawing.Size(262, 38)
        Me.DateRange.TabIndex = 322
        Me.DateRange.Text = "Date Range"
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(13, 14)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(32, 18)
        Me.RadLabel1.TabIndex = 2
        Me.RadLabel1.Text = "From"
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(166, 14)
        Me.dtpToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.Size = New System.Drawing.Size(84, 20)
        Me.dtpToDate.TabIndex = 1
        Me.dtpToDate.TabStop = False
        Me.dtpToDate.Text = "24/10/2011"
        Me.dtpToDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'dtpFrmDate
        '
        Me.dtpFrmDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpFrmDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrmDate.Location = New System.Drawing.Point(51, 14)
        Me.dtpFrmDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFrmDate.Name = "dtpFrmDate"
        Me.dtpFrmDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFrmDate.Size = New System.Drawing.Size(84, 20)
        Me.dtpFrmDate.TabIndex = 0
        Me.dtpFrmDate.TabStop = False
        Me.dtpFrmDate.Text = "24/10/2011"
        Me.dtpFrmDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'btnreset
        '
        Me.btnreset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreset.Location = New System.Drawing.Point(10, 4)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(75, 23)
        Me.btnreset.TabIndex = 1
        Me.btnreset.Text = ">>>"
        Me.btnreset.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.Controls.Add(Me.btnClose)
        Me.Panel2.Controls.Add(Me.btnreset)
        Me.Panel2.Controls.Add(Me.btnprint)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 632)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1070, 32)
        Me.Panel2.TabIndex = 324
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(983, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnprint
        '
        Me.btnprint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnprint.Location = New System.Drawing.Point(92, 4)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(75, 23)
        Me.btnprint.TabIndex = 0
        Me.btnprint.Text = "Print"
        Me.btnprint.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.cbgAssets)
        Me.Panel1.Controls.Add(Me.Panel7)
        Me.Panel1.Location = New System.Drawing.Point(6, 21)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(557, 226)
        Me.Panel1.TabIndex = 325
        '
        'cbgAssets
        '
        Me.cbgAssets.CheckedValue = Nothing
        Me.cbgAssets.DataSource = Nothing
        Me.cbgAssets.DisplayMember = "Name"
        Me.cbgAssets.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgAssets.Location = New System.Drawing.Point(0, 20)
        Me.cbgAssets.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgAssets.MyShowHeadrText = False
        Me.cbgAssets.Name = "cbgAssets"
        Me.cbgAssets.Size = New System.Drawing.Size(557, 206)
        Me.cbgAssets.TabIndex = 3
        Me.cbgAssets.ValueMember = "Code"
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.rbtnvselect)
        Me.Panel7.Controls.Add(Me.rbtnvAll)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel7.Location = New System.Drawing.Point(0, 0)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(557, 20)
        Me.Panel7.TabIndex = 2
        '
        'rbtnvselect
        '
        Me.rbtnvselect.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rbtnvselect.Location = New System.Drawing.Point(238, 1)
        Me.rbtnvselect.MyLinkLable1 = Nothing
        Me.rbtnvselect.MyLinkLable2 = Nothing
        Me.rbtnvselect.Name = "rbtnvselect"
        Me.rbtnvselect.Size = New System.Drawing.Size(62, 18)
        Me.rbtnvselect.TabIndex = 1
        Me.rbtnvselect.Text = "Selected"
        '
        'rbtnvAll
        '
        Me.rbtnvAll.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rbtnvAll.Location = New System.Drawing.Point(190, 1)
        Me.rbtnvAll.MyLinkLable1 = Nothing
        Me.rbtnvAll.MyLinkLable2 = Nothing
        Me.rbtnvAll.Name = "rbtnvAll"
        Me.rbtnvAll.Size = New System.Drawing.Size(33, 18)
        Me.rbtnvAll.TabIndex = 1
        Me.rbtnvAll.Text = "All"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Panel1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 362)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(579, 262)
        Me.GroupBox1.TabIndex = 326
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Select Assets"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.radioPulloutAndRedeploy)
        Me.GroupBox2.Controls.Add(Me.radioPullout)
        Me.GroupBox2.Controls.Add(Me.radioInstall)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 10)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(390, 34)
        Me.GroupBox2.TabIndex = 327
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Select Type"
        '
        'radioPulloutAndRedeploy
        '
        Me.radioPulloutAndRedeploy.AutoSize = True
        Me.radioPulloutAndRedeploy.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.radioPulloutAndRedeploy.Location = New System.Drawing.Point(202, 13)
        Me.radioPulloutAndRedeploy.Name = "radioPulloutAndRedeploy"
        Me.radioPulloutAndRedeploy.Size = New System.Drawing.Size(168, 17)
        Me.radioPulloutAndRedeploy.TabIndex = 2
        Me.radioPulloutAndRedeploy.TabStop = True
        Me.radioPulloutAndRedeploy.Text = "Pullout And Redeploy Both"
        Me.radioPulloutAndRedeploy.UseVisualStyleBackColor = True
        '
        'radioPullout
        '
        Me.radioPullout.AutoSize = True
        Me.radioPullout.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.radioPullout.Location = New System.Drawing.Point(105, 13)
        Me.radioPullout.Name = "radioPullout"
        Me.radioPullout.Size = New System.Drawing.Size(91, 17)
        Me.radioPullout.TabIndex = 1
        Me.radioPullout.TabStop = True
        Me.radioPullout.Text = "PullOut Only"
        Me.radioPullout.UseVisualStyleBackColor = True
        '
        'radioInstall
        '
        Me.radioInstall.AutoSize = True
        Me.radioInstall.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.radioInstall.Location = New System.Drawing.Point(16, 13)
        Me.radioInstall.Name = "radioInstall"
        Me.radioInstall.Size = New System.Drawing.Size(83, 17)
        Me.radioInstall.TabIndex = 0
        Me.radioInstall.TabStop = True
        Me.radioInstall.Text = "Install Only"
        Me.radioInstall.UseVisualStyleBackColor = True
        '
        'CustomerGroupBox
        '
        Me.CustomerGroupBox.Controls.Add(Me.Panel3)
        Me.CustomerGroupBox.Location = New System.Drawing.Point(10, 118)
        Me.CustomerGroupBox.Name = "CustomerGroupBox"
        Me.CustomerGroupBox.Size = New System.Drawing.Size(581, 240)
        Me.CustomerGroupBox.TabIndex = 329
        Me.CustomerGroupBox.TabStop = False
        Me.CustomerGroupBox.Text = "Select Customer "
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.cbgCustomer)
        Me.Panel3.Controls.Add(Me.Panel4)
        Me.Panel3.Location = New System.Drawing.Point(6, 12)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(571, 224)
        Me.Panel3.TabIndex = 325
        '
        'cbgCustomer
        '
        Me.cbgCustomer.CheckedValue = Nothing
        Me.cbgCustomer.DataSource = Nothing
        Me.cbgCustomer.DisplayMember = "Name"
        Me.cbgCustomer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgCustomer.Location = New System.Drawing.Point(0, 20)
        Me.cbgCustomer.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgCustomer.MyShowHeadrText = False
        Me.cbgCustomer.Name = "cbgCustomer"
        Me.cbgCustomer.Size = New System.Drawing.Size(571, 204)
        Me.cbgCustomer.TabIndex = 3
        Me.cbgCustomer.ValueMember = "Code"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.MyRadioButton1)
        Me.Panel4.Controls.Add(Me.MyRadioButton2)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(571, 20)
        Me.Panel4.TabIndex = 2
        '
        'MyRadioButton1
        '
        Me.MyRadioButton1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.MyRadioButton1.Location = New System.Drawing.Point(245, 1)
        Me.MyRadioButton1.MyLinkLable1 = Nothing
        Me.MyRadioButton1.MyLinkLable2 = Nothing
        Me.MyRadioButton1.Name = "MyRadioButton1"
        Me.MyRadioButton1.Size = New System.Drawing.Size(62, 18)
        Me.MyRadioButton1.TabIndex = 1
        Me.MyRadioButton1.Text = "Selected"
        '
        'MyRadioButton2
        '
        Me.MyRadioButton2.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.MyRadioButton2.Location = New System.Drawing.Point(197, 1)
        Me.MyRadioButton2.MyLinkLable1 = Nothing
        Me.MyRadioButton2.MyLinkLable2 = Nothing
        Me.MyRadioButton2.Name = "MyRadioButton2"
        Me.MyRadioButton2.Size = New System.Drawing.Size(33, 18)
        Me.MyRadioButton2.TabIndex = 1
        Me.MyRadioButton2.Text = "All"
        '
        'frmPullOutRedeployReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1070, 664)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.CustomerGroupBox)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.DateRange)
        Me.Controls.Add(Me.Panel2)
        Me.Name = "frmPullOutRedeployReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "PullOut/Redeploy"
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DateRange, System.ComponentModel.ISupportInitialize).EndInit()
        Me.DateRange.ResumeLayout(False)
        Me.DateRange.PerformLayout()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFrmDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        CType(Me.rbtnvselect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnvAll, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.CustomerGroupBox.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.MyRadioButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyRadioButton2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents DateRange As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents dtpToDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents dtpFrmDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents btnreset As System.Windows.Forms.Button
    Friend WithEvents btnprint As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents cbgAssets As common.MyCheckBoxGrid
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Protected WithEvents rbtnvselect As common.Controls.MyRadioButton
    Protected WithEvents rbtnvAll As common.Controls.MyRadioButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents radioPullout As System.Windows.Forms.RadioButton
    Friend WithEvents radioInstall As System.Windows.Forms.RadioButton
    Friend WithEvents radioPulloutAndRedeploy As System.Windows.Forms.RadioButton
    Friend WithEvents CustomerGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents cbgCustomer As common.MyCheckBoxGrid
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Protected WithEvents MyRadioButton1 As common.Controls.MyRadioButton
    Protected WithEvents MyRadioButton2 As common.Controls.MyRadioButton
    Friend WithEvents btnClose As System.Windows.Forms.Button
End Class

