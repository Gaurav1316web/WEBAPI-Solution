Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmJoiningChecklist
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
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem
        Me.RSaveLayout = New Telerik.WinControls.UI.RadMenuItem
        Me.RDeleteLayout = New Telerik.WinControls.UI.RadMenuItem
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.fndaccountsetcode = New common.UserControls.txtNavigator
        Me.UcRequisitionDetail1 = New XpertERPHRandPayroll.ucRequisitionDetail
        Me.UsLock1 = New common.usLock
        Me.rdbtnreset = New Telerik.WinControls.UI.RadButton
        Me.lblApplicantCode = New common.Controls.MyLabel
        Me.lbldate = New common.Controls.MyLabel
        Me.Txtdate = New common.Controls.MyDateTimePicker
        Me.gv = New common.UserControls.MyRadGridView
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.btnSave = New Telerik.WinControls.UI.RadButton
        Me.btnRejected = New Telerik.WinControls.UI.RadButton
        Me.btnPost = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.rdbtnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblApplicantCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbldate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Txtdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRejected, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(820, 20)
        Me.RadMenu1.TabIndex = 2
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Setting"
        Me.RadMenuItem1.AccessibleName = "Setting"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RSaveLayout, Me.RDeleteLayout})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Setting"
        Me.RadMenuItem1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RSaveLayout
        '
        Me.RSaveLayout.AccessibleDescription = "SaveLayout"
        Me.RSaveLayout.AccessibleName = "SaveLayout"
        Me.RSaveLayout.Name = "RSaveLayout"
        Me.RSaveLayout.Text = "SaveLayout"
        Me.RSaveLayout.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RDeleteLayout
        '
        Me.RDeleteLayout.AccessibleDescription = "Delete Layout"
        Me.RDeleteLayout.AccessibleName = "Delete Layout"
        Me.RDeleteLayout.Name = "RDeleteLayout"
        Me.RDeleteLayout.Text = "Delete Layout"
        Me.RDeleteLayout.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndaccountsetcode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.UcRequisitionDetail1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.rdbtnreset)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblApplicantCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lbldate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Txtdate)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gv)
        Me.SplitContainer2.Size = New System.Drawing.Size(820, 433)
        Me.SplitContainer2.SplitterDistance = 138
        Me.SplitContainer2.TabIndex = 0
        '
        'fndaccountsetcode
        '
        Me.fndaccountsetcode.Location = New System.Drawing.Point(101, 12)
        Me.fndaccountsetcode.MendatroryField = True
        Me.fndaccountsetcode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndaccountsetcode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndaccountsetcode.MyLinkLable1 = Nothing
        Me.fndaccountsetcode.MyLinkLable2 = Nothing
        Me.fndaccountsetcode.MyMaxLength = 12
        Me.fndaccountsetcode.MyReadOnly = False
        Me.fndaccountsetcode.Name = "fndaccountsetcode"
        Me.fndaccountsetcode.Size = New System.Drawing.Size(202, 21)
        Me.fndaccountsetcode.TabIndex = 0
        Me.fndaccountsetcode.TabStop = False
        Me.fndaccountsetcode.Value = ""
        '
        'UcRequisitionDetail1
        '
        Me.UcRequisitionDetail1.AppCode = ""
        Me.UcRequisitionDetail1.AppDate = ""
        Me.UcRequisitionDetail1.AppName = ""
        Me.UcRequisitionDetail1.DateofBirth = ""
        Me.UcRequisitionDetail1.Email = ""
        Me.UcRequisitionDetail1.Location = New System.Drawing.Point(5, 37)
        Me.UcRequisitionDetail1.Name = "UcRequisitionDetail1"
        Me.UcRequisitionDetail1.ReqCode = ""
        Me.UcRequisitionDetail1.Size = New System.Drawing.Size(812, 98)
        Me.UcRequisitionDetail1.TabIndex = 111
        Me.UcRequisitionDetail1.TelephoneNo = ""
        '
        'UsLock1
        '
        Me.UsLock1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(710, 13)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 110
        '
        'rdbtnreset
        '
        Me.rdbtnreset.Image = Global.XpertERPHRandPayroll.My.Resources.Resources._new
        Me.rdbtnreset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.rdbtnreset.Location = New System.Drawing.Point(303, 12)
        Me.rdbtnreset.Name = "rdbtnreset"
        Me.rdbtnreset.Size = New System.Drawing.Size(19, 21)
        Me.rdbtnreset.TabIndex = 1
        '
        'lblApplicantCode
        '
        Me.lblApplicantCode.Location = New System.Drawing.Point(12, 15)
        Me.lblApplicantCode.Name = "lblApplicantCode"
        Me.lblApplicantCode.Size = New System.Drawing.Size(83, 18)
        Me.lblApplicantCode.TabIndex = 14
        Me.lblApplicantCode.Text = "Applicant Code"
        '
        'lbldate
        '
        Me.lbldate.Location = New System.Drawing.Point(351, 15)
        Me.lbldate.Name = "lbldate"
        Me.lbldate.Size = New System.Drawing.Size(30, 18)
        Me.lbldate.TabIndex = 16
        Me.lbldate.Text = "Date"
        '
        'Txtdate
        '
        Me.Txtdate.CustomFormat = "dd-MM-yyyy"
        Me.Txtdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Txtdate.Location = New System.Drawing.Point(387, 13)
        Me.Txtdate.MendatroryField = False
        Me.Txtdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.Txtdate.MyLinkLable1 = Nothing
        Me.Txtdate.MyLinkLable2 = Nothing
        Me.Txtdate.Name = "Txtdate"
        Me.Txtdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.Txtdate.Size = New System.Drawing.Size(79, 20)
        Me.Txtdate.TabIndex = 2
        Me.Txtdate.TabStop = False
        Me.Txtdate.Text = "17-12-2011"
        Me.Txtdate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'gv
        '
        Me.gv.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gv.ForeColor = System.Drawing.Color.Black
        Me.gv.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv.Location = New System.Drawing.Point(0, 0)
        '
        'gv
        '
        Me.gv.MasterTemplate.AllowAddNewRow = False
        Me.gv.MasterTemplate.AllowDeleteRow = False
        Me.gv.MasterTemplate.EnableFiltering = True
        Me.gv.Name = "gv"
        Me.gv.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv.ShowGroupPanel = False
        Me.gv.Size = New System.Drawing.Size(820, 291)
        Me.gv.TabIndex = 4
        Me.gv.Text = "gv"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnRejected)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(820, 478)
        Me.SplitContainer1.SplitterDistance = 433
        Me.SplitContainer1.TabIndex = 0
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(11, 11)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(66, 18)
        Me.btnSave.TabIndex = 2
        Me.btnSave.Text = "Save"
        '
        'btnRejected
        '
        Me.btnRejected.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRejected.Location = New System.Drawing.Point(155, 11)
        Me.btnRejected.Name = "btnRejected"
        Me.btnRejected.Size = New System.Drawing.Size(66, 18)
        Me.btnRejected.TabIndex = 2
        Me.btnRejected.Text = "Rejected"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Location = New System.Drawing.Point(83, 11)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(66, 18)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(730, 11)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(66, 18)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "Close"
        '
        'FrmJoiningChecklist
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(820, 498)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmJoiningChecklist"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmJoiningChecklist"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.rdbtnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblApplicantCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbldate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Txtdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRejected, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents fndaccountsetcode As common.UserControls.txtNavigator
    Friend WithEvents rdbtnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents Txtdate As common.Controls.MyDateTimePicker
    Friend WithEvents lbldate As common.Controls.MyLabel
    Friend WithEvents lblApplicantCode As common.Controls.MyLabel
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnRejected As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents RSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents UcRequisitionDetail1 As XpertERPHRandPayroll.ucRequisitionDetail
End Class

