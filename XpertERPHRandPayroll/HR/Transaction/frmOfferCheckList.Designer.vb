Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmOfferCheckList
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
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.fndaccountsetcode = New common.UserControls.txtNavigator()
        Me.UcRequisitionDetail1 = New XpertERPHRandPayroll.ucRequisitionDetail()
        Me.rdbtnreset = New Telerik.WinControls.UI.RadButton()
        Me.UsLock1 = New common.usLock()
        Me.lbldate = New common.Controls.MyLabel()
        Me.lblApplicantCode = New common.Controls.MyLabel()
        Me.gv = New common.UserControls.MyRadGridView()
        Me.btnRejected = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.TxtDate = New common.Controls.MyDateTimePicker()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.rdbtnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbldate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblApplicantCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRejected, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(942, 20)
        Me.RadMenu1.TabIndex = 3
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Setting"
        Me.RadMenuItem1.AccessibleName = "Setting"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmSaveLayout, Me.RadMenuItem2})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Setting"
        '
        'rmSaveLayout
        '
        Me.rmSaveLayout.AccessibleDescription = "Save Layout"
        Me.rmSaveLayout.AccessibleName = "Save Layout"
        Me.rmSaveLayout.Name = "rmSaveLayout"
        Me.rmSaveLayout.Text = "Save Layout"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Delete Layout"
        Me.RadMenuItem2.AccessibleName = "Delete Layout"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Delete Layout"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnRejected)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(942, 472)
        Me.SplitContainer1.SplitterDistance = 426
        Me.SplitContainer1.TabIndex = 4
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.TxtDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndaccountsetcode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.UcRequisitionDetail1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.rdbtnreset)
        Me.SplitContainer2.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lbldate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblApplicantCode)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gv)
        Me.SplitContainer2.Size = New System.Drawing.Size(942, 426)
        Me.SplitContainer2.SplitterDistance = 135
        Me.SplitContainer2.TabIndex = 0
        '
        'fndaccountsetcode
        '
        Me.fndaccountsetcode.Location = New System.Drawing.Point(101, 10)
        Me.fndaccountsetcode.MendatroryField = True
        Me.fndaccountsetcode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndaccountsetcode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndaccountsetcode.MyLinkLable1 = Nothing
        Me.fndaccountsetcode.MyLinkLable2 = Nothing
        Me.fndaccountsetcode.MyMaxLength = 12
        Me.fndaccountsetcode.MyReadOnly = False
        Me.fndaccountsetcode.Name = "fndaccountsetcode"
        Me.fndaccountsetcode.Size = New System.Drawing.Size(202, 21)
        Me.fndaccountsetcode.TabIndex = 1
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
        Me.UcRequisitionDetail1.Location = New System.Drawing.Point(12, 37)
        Me.UcRequisitionDetail1.Name = "UcRequisitionDetail1"
        Me.UcRequisitionDetail1.ReqCode = ""
        Me.UcRequisitionDetail1.Size = New System.Drawing.Size(921, 94)
        Me.UcRequisitionDetail1.TabIndex = 31
        Me.UcRequisitionDetail1.TelephoneNo = ""
        '
        'rdbtnreset
        '
        Me.rdbtnreset.Image = Global.XpertERPHRandPayroll.My.Resources.Resources._new
        Me.rdbtnreset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.rdbtnreset.Location = New System.Drawing.Point(303, 10)
        Me.rdbtnreset.Name = "rdbtnreset"
        Me.rdbtnreset.Size = New System.Drawing.Size(19, 21)
        Me.rdbtnreset.TabIndex = 2
        '
        'UsLock1
        '
        Me.UsLock1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(836, 7)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 1
        '
        'lbldate
        '
        Me.lbldate.Location = New System.Drawing.Point(348, 11)
        Me.lbldate.Name = "lbldate"
        Me.lbldate.Size = New System.Drawing.Size(30, 18)
        Me.lbldate.TabIndex = 3
        Me.lbldate.Text = "Date"
        '
        'lblApplicantCode
        '
        Me.lblApplicantCode.Location = New System.Drawing.Point(12, 11)
        Me.lblApplicantCode.Name = "lblApplicantCode"
        Me.lblApplicantCode.Size = New System.Drawing.Size(83, 18)
        Me.lblApplicantCode.TabIndex = 0
        Me.lblApplicantCode.Text = "Applicant Code"
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
        Me.gv.Size = New System.Drawing.Size(942, 287)
        Me.gv.TabIndex = 0
        Me.gv.Text = "gv"
        '
        'btnRejected
        '
        Me.btnRejected.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRejected.Location = New System.Drawing.Point(156, 11)
        Me.btnRejected.Name = "btnRejected"
        Me.btnRejected.Size = New System.Drawing.Size(66, 18)
        Me.btnRejected.TabIndex = 2
        Me.btnRejected.Text = "Rejected"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(12, 11)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(66, 18)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Location = New System.Drawing.Point(84, 11)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(66, 18)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(864, 11)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(66, 18)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "Close"
        '
        'TxtDate
        '
        Me.TxtDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TxtDate.CustomFormat = "dd/MM/yyyy"
        Me.TxtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.TxtDate.Location = New System.Drawing.Point(384, 10)
        Me.TxtDate.MendatroryField = False
        Me.TxtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.TxtDate.MyLinkLable1 = Nothing
        Me.TxtDate.MyLinkLable2 = Nothing
        Me.TxtDate.Name = "TxtDate"
        Me.TxtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.TxtDate.Size = New System.Drawing.Size(83, 20)
        Me.TxtDate.TabIndex = 32
        Me.TxtDate.TabStop = False
        Me.TxtDate.Text = "16/11/2011"
        Me.TxtDate.Value = New Date(2011, 11, 16, 11, 21, 56, 285)
        '
        'FrmOfferCheckList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(942, 492)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmOfferCheckList"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Offer check list"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.rdbtnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbldate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblApplicantCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRejected, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents fndaccountsetcode As common.UserControls.txtNavigator
    Friend WithEvents rdbtnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents lbldate As common.Controls.MyLabel
    Friend WithEvents lblApplicantCode As common.Controls.MyLabel
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents btnRejected As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents rmSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents UcRequisitionDetail1 As XpertERPHRandPayroll.ucRequisitionDetail
    Friend WithEvents TxtDate As common.Controls.MyDateTimePicker
End Class

