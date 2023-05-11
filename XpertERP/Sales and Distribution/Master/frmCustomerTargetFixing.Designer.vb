<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCustomerTargetFixing
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCustomerTargetFixing))
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.rdbFlavourwise = New Telerik.WinControls.UI.RadRadioButton
        Me.rdbPackwise = New Telerik.WinControls.UI.RadRadioButton
        Me.rdbItemwise = New Telerik.WinControls.UI.RadRadioButton
        Me.lblIncentive = New common.Controls.MyLabel
        Me.lblTarget = New common.Controls.MyLabel
        Me.txtIncentive = New common.MyNumBox
        Me.txtTarget = New common.MyNumBox
        Me.btnNew = New Telerik.WinControls.UI.RadButton
        Me.lblCustomerName = New common.Controls.MyLabel
        Me.txtDescription = New common.Controls.MyTextBox
        Me.lblRemarks = New common.Controls.MyLabel
        Me.lblMonthYear = New common.Controls.MyLabel
        Me.dtpMonthYear = New common.Controls.MyDateTimePicker
        Me.lbCustomerCode = New common.Controls.MyLabel
        Me.txtCustomerCode = New common.UserControls.txtFinder
        Me.txtCode = New common.UserControls.txtNavigator
        Me.lblCode = New common.Controls.MyLabel
        Me.gvItems = New common.UserControls.MyRadGridView
        Me.btnsave = New Telerik.WinControls.UI.RadButton
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.btndelete = New Telerik.WinControls.UI.RadButton
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.rdbFlavourwise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbPackwise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbItemwise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblIncentive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTarget, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIncentive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTarget, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomerName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMonthYear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpMonthYear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbCustomerCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvItems.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.RadGroupBox3.FooterImageIndex = -1
        Me.RadGroupBox3.FooterImageKey = ""
        Me.RadGroupBox3.HeaderImageIndex = -1
        Me.RadGroupBox3.HeaderImageKey = ""
        Me.RadGroupBox3.HeaderMargin = New System.Windows.Forms.Padding(0)
        Me.RadGroupBox3.HeaderText = ""
        Me.RadGroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        '
        '
        '
        Me.RadGroupBox3.RootElement.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(810, 507)
        Me.RadGroupBox3.TabIndex = 0
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdbFlavourwise)
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdbPackwise)
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdbItemwise)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblIncentive)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblTarget)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtIncentive)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtTarget)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCustomerName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblRemarks)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblMonthYear)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpMonthYear)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lbCustomerCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCustomerCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.gvItems)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(790, 477)
        Me.SplitContainer1.SplitterDistance = 430
        Me.SplitContainer1.TabIndex = 0
        '
        'rdbFlavourwise
        '
        Me.rdbFlavourwise.Location = New System.Drawing.Point(389, 106)
        Me.rdbFlavourwise.Name = "rdbFlavourwise"
        Me.rdbFlavourwise.Size = New System.Drawing.Size(110, 18)
        Me.rdbFlavourwise.TabIndex = 8
        Me.rdbFlavourwise.Text = "Flavourwise"
        '
        'rdbPackwise
        '
        Me.rdbPackwise.Location = New System.Drawing.Point(272, 106)
        Me.rdbPackwise.Name = "rdbPackwise"
        Me.rdbPackwise.Size = New System.Drawing.Size(110, 18)
        Me.rdbPackwise.TabIndex = 7
        Me.rdbPackwise.Text = "Packwise"
        '
        'rdbItemwise
        '
        Me.rdbItemwise.Location = New System.Drawing.Point(133, 106)
        Me.rdbItemwise.Name = "rdbItemwise"
        Me.rdbItemwise.Size = New System.Drawing.Size(110, 18)
        Me.rdbItemwise.TabIndex = 6
        Me.rdbItemwise.Text = "Itemwise"
        '
        'lblIncentive
        '
        Me.lblIncentive.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIncentive.Location = New System.Drawing.Point(260, 81)
        Me.lblIncentive.Name = "lblIncentive"
        Me.lblIncentive.Size = New System.Drawing.Size(52, 16)
        Me.lblIncentive.TabIndex = 206
        Me.lblIncentive.Text = "Incentive"
        '
        'lblTarget
        '
        Me.lblTarget.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTarget.Location = New System.Drawing.Point(16, 81)
        Me.lblTarget.Name = "lblTarget"
        Me.lblTarget.Size = New System.Drawing.Size(39, 16)
        Me.lblTarget.TabIndex = 205
        Me.lblTarget.Text = "Target"
        '
        'txtIncentive
        '
        Me.txtIncentive.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtIncentive.DecimalPlaces = 2
        Me.txtIncentive.Location = New System.Drawing.Point(319, 78)
        Me.txtIncentive.MaxLength = 6
        Me.txtIncentive.MendatroryField = True
        Me.txtIncentive.MyLinkLable1 = Me.lblIncentive
        Me.txtIncentive.MyLinkLable2 = Nothing
        Me.txtIncentive.Name = "txtIncentive"
        Me.txtIncentive.Size = New System.Drawing.Size(122, 20)
        Me.txtIncentive.TabIndex = 5
        Me.txtIncentive.Text = "0"
        Me.txtIncentive.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtIncentive.Value = 0
        '
        'txtTarget
        '
        Me.txtTarget.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtTarget.DecimalPlaces = 2
        Me.txtTarget.Location = New System.Drawing.Point(133, 78)
        Me.txtTarget.MaxLength = 6
        Me.txtTarget.MendatroryField = True
        Me.txtTarget.MyLinkLable1 = Me.lblTarget
        Me.txtTarget.MyLinkLable2 = Nothing
        Me.txtTarget.Name = "txtTarget"
        Me.txtTarget.Size = New System.Drawing.Size(122, 20)
        Me.txtTarget.TabIndex = 4
        Me.txtTarget.Text = "0"
        Me.txtTarget.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTarget.Value = 0
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(357, 6)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 20)
        Me.btnNew.TabIndex = 202
        Me.btnNew.Text = " "
        '
        'lblCustomerName
        '
        Me.lblCustomerName.AutoSize = False
        Me.lblCustomerName.BorderVisible = True
        Me.lblCustomerName.Location = New System.Drawing.Point(357, 31)
        Me.lblCustomerName.Name = "lblCustomerName"
        Me.lblCustomerName.Size = New System.Drawing.Size(222, 19)
        Me.lblCustomerName.TabIndex = 201
        Me.lblCustomerName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDescription
        '
        Me.txtDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.Location = New System.Drawing.Point(132, 134)
        Me.txtDescription.MaxLength = 49
        Me.txtDescription.MendatroryField = True
        Me.txtDescription.Multiline = True
        Me.txtDescription.MyLinkLable1 = Me.lblRemarks
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        '
        '
        '
        Me.txtDescription.RootElement.StretchVertically = True
        Me.txtDescription.Size = New System.Drawing.Size(313, 63)
        Me.txtDescription.TabIndex = 9
        '
        'lblRemarks
        '
        Me.lblRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRemarks.Location = New System.Drawing.Point(18, 134)
        Me.lblRemarks.Name = "lblRemarks"
        Me.lblRemarks.Size = New System.Drawing.Size(51, 16)
        Me.lblRemarks.TabIndex = 177
        Me.lblRemarks.Text = "Remarks"
        '
        'lblMonthYear
        '
        Me.lblMonthYear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMonthYear.Location = New System.Drawing.Point(14, 54)
        Me.lblMonthYear.Name = "lblMonthYear"
        Me.lblMonthYear.Size = New System.Drawing.Size(65, 16)
        Me.lblMonthYear.TabIndex = 164
        Me.lblMonthYear.Text = "Month/Year"
        '
        'dtpMonthYear
        '
        Me.dtpMonthYear.CustomFormat = "MMM/yyyy"
        Me.dtpMonthYear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpMonthYear.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpMonthYear.Location = New System.Drawing.Point(133, 54)
        Me.dtpMonthYear.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.dtpMonthYear.MendatroryField = True
        Me.dtpMonthYear.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpMonthYear.MyLinkLable1 = Me.lblMonthYear
        Me.dtpMonthYear.MyLinkLable2 = Nothing
        Me.dtpMonthYear.Name = "dtpMonthYear"
        Me.dtpMonthYear.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpMonthYear.Size = New System.Drawing.Size(130, 18)
        Me.dtpMonthYear.TabIndex = 3
        Me.dtpMonthYear.Text = "RadDateTimePicker2"
        Me.dtpMonthYear.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lbCustomerCode
        '
        Me.lbCustomerCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbCustomerCode.Location = New System.Drawing.Point(13, 33)
        Me.lbCustomerCode.Name = "lbCustomerCode"
        Me.lbCustomerCode.Size = New System.Drawing.Size(85, 16)
        Me.lbCustomerCode.TabIndex = 154
        Me.lbCustomerCode.Text = "Customer Code"
        '
        'txtCustomerCode
        '
        Me.txtCustomerCode.Location = New System.Drawing.Point(133, 31)
        Me.txtCustomerCode.MendatroryField = True
        Me.txtCustomerCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerCode.MyLinkLable1 = Me.lbCustomerCode
        Me.txtCustomerCode.MyLinkLable2 = Nothing
        Me.txtCustomerCode.MyReadOnly = False
        Me.txtCustomerCode.Name = "txtCustomerCode"
        Me.txtCustomerCode.Size = New System.Drawing.Size(221, 19)
        Me.txtCustomerCode.TabIndex = 1
        Me.txtCustomerCode.Value = ""
        '
        'txtCode
        '
        Me.txtCode.Location = New System.Drawing.Point(133, 6)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 12
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(221, 21)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'lblCode
        '
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCode.Location = New System.Drawing.Point(12, 11)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(69, 16)
        Me.lblCode.TabIndex = 161
        Me.lblCode.Text = "Target Code"
        '
        'gvItems
        '
        Me.gvItems.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gvItems.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvItems.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvItems.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvItems.ForeColor = System.Drawing.Color.Black
        Me.gvItems.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvItems.Location = New System.Drawing.Point(9, 203)
        '
        'gvItems
        '
        Me.gvItems.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvItems.MasterTemplate.AutoGenerateColumns = False
        Me.gvItems.MasterTemplate.EnableGrouping = False
        Me.gvItems.Name = "gvItems"
        Me.gvItems.RightToLeft = System.Windows.Forms.RightToLeft.No
        '
        '
        '
        Me.gvItems.RootElement.ForeColor = System.Drawing.Color.Black
        Me.gvItems.Size = New System.Drawing.Size(772, 219)
        Me.gvItems.TabIndex = 6
        Me.gvItems.Text = "RadGridView4"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(9, 16)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(715, 16)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 3
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(82, 16)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 2
        Me.btndelete.Text = "Delete"
        '
        'frmCustomerTargetFixing
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(810, 507)
        Me.Controls.Add(Me.RadGroupBox3)
        Me.Name = "frmCustomerTargetFixing"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Customer Target Fixing"
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.rdbFlavourwise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbPackwise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbItemwise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblIncentive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTarget, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIncentive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTarget, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomerName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMonthYear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpMonthYear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbCustomerCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvItems.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents lblRemarks As common.Controls.MyLabel
    Friend WithEvents lblMonthYear As common.Controls.MyLabel
    Friend WithEvents dtpMonthYear As common.Controls.MyDateTimePicker
    Friend WithEvents lbCustomerCode As common.Controls.MyLabel
    Friend WithEvents txtCustomerCode As common.UserControls.txtFinder
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents gvItems As common.UserControls.MyRadGridView
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblCustomerName As common.Controls.MyLabel
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblIncentive As common.Controls.MyLabel
    Friend WithEvents lblTarget As common.Controls.MyLabel
    Friend WithEvents txtIncentive As common.MyNumBox
    Friend WithEvents txtTarget As common.MyNumBox
    Friend WithEvents rdbItemwise As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbFlavourwise As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbPackwise As Telerik.WinControls.UI.RadRadioButton
End Class
