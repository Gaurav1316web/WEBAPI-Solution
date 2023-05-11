Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEmployeeShiftChange
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEmployeeShiftChange))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.UsLock1 = New common.usLock
        Me.btnNew = New Telerik.WinControls.UI.RadButton
        Me.txtDescription = New common.Controls.MyTextBox
        Me.lblRemarks = New common.Controls.MyLabel
        Me.lblAppDate = New common.Controls.MyLabel
        Me.dtpAllowanceDate = New common.Controls.MyDateTimePicker
        Me.txtCode = New common.UserControls.txtNavigator
        Me.lblCode = New common.Controls.MyLabel
        Me.gvAllowance = New common.UserControls.MyRadGridView
        Me.btnPost = New Telerik.WinControls.UI.RadButton
        Me.btnsave = New Telerik.WinControls.UI.RadButton
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.btndelete = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAppDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpAllowanceDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvAllowance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvAllowance.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblRemarks)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblAppDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpAllowanceDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.gvAllowance)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(810, 507)
        Me.SplitContainer1.SplitterDistance = 460
        Me.SplitContainer1.TabIndex = 0
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(703, 12)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 18)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 212
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(357, 10)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 20)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Text = " "
        '
        'txtDescription
        '
        Me.txtDescription.AutoSize = False
        Me.txtDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.Location = New System.Drawing.Point(133, 57)
        Me.txtDescription.MaxLength = 49
        Me.txtDescription.MendatroryField = True
        Me.txtDescription.Multiline = True
        Me.txtDescription.MyLinkLable1 = Me.lblRemarks
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(313, 63)
        Me.txtDescription.TabIndex = 9
        '
        'lblRemarks
        '
        Me.lblRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRemarks.Location = New System.Drawing.Point(14, 60)
        Me.lblRemarks.Name = "lblRemarks"
        Me.lblRemarks.Size = New System.Drawing.Size(51, 16)
        Me.lblRemarks.TabIndex = 177
        Me.lblRemarks.Text = "Remarks"
        '
        'lblAppDate
        '
        Me.lblAppDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAppDate.Location = New System.Drawing.Point(14, 35)
        Me.lblAppDate.Name = "lblAppDate"
        Me.lblAppDate.Size = New System.Drawing.Size(86, 16)
        Me.lblAppDate.TabIndex = 164
        Me.lblAppDate.Text = "Applicable Date"
        '
        'dtpAllowanceDate
        '
        Me.dtpAllowanceDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpAllowanceDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpAllowanceDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpAllowanceDate.Location = New System.Drawing.Point(133, 33)
        Me.dtpAllowanceDate.MendatroryField = True
        Me.dtpAllowanceDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpAllowanceDate.MyLinkLable1 = Me.lblAppDate
        Me.dtpAllowanceDate.MyLinkLable2 = Nothing
        Me.dtpAllowanceDate.Name = "dtpAllowanceDate"
        Me.dtpAllowanceDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpAllowanceDate.Size = New System.Drawing.Size(130, 18)
        Me.dtpAllowanceDate.TabIndex = 6
        Me.dtpAllowanceDate.TabStop = False
        Me.dtpAllowanceDate.Text = "03/05/2011"
        Me.dtpAllowanceDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'txtCode
        '
        Me.txtCode.Location = New System.Drawing.Point(133, 9)
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
        Me.lblCode.Location = New System.Drawing.Point(14, 12)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(33, 16)
        Me.lblCode.TabIndex = 161
        Me.lblCode.Text = "Code"
        '
        'gvAllowance
        '
        Me.gvAllowance.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gvAllowance.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvAllowance.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvAllowance.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvAllowance.ForeColor = System.Drawing.Color.Black
        Me.gvAllowance.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvAllowance.Location = New System.Drawing.Point(9, 126)
        '
        'gvAllowance
        '
        Me.gvAllowance.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvAllowance.MasterTemplate.AutoGenerateColumns = False
        Me.gvAllowance.MasterTemplate.EnableGrouping = False
        Me.gvAllowance.Name = "gvAllowance"
        Me.gvAllowance.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvAllowance.Size = New System.Drawing.Size(792, 331)
        Me.gvAllowance.TabIndex = 10
        Me.gvAllowance.TabStop = False
        Me.gvAllowance.Text = "RadGridView4"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(78, 16)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(66, 18)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
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
        Me.btnclose.Location = New System.Drawing.Point(735, 16)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 3
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(147, 16)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 2
        Me.btndelete.Text = "Delete"
        '
        'frmEmployeeShiftChange
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(810, 507)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmEmployeeShiftChange"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Employee Shift Change"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAppDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpAllowanceDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvAllowance.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvAllowance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents lblRemarks As common.Controls.MyLabel
    Friend WithEvents lblAppDate As common.Controls.MyLabel
    Friend WithEvents dtpAllowanceDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents gvAllowance As common.UserControls.MyRadGridView
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
End Class
