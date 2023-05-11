Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAppointmentLetterHR
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.MyLabel3 = New common.Controls.MyLabel
        Me.txtAppcode = New common.UserControls.txtNavigator
        Me.btnnew = New Telerik.WinControls.UI.RadButton
        Me.UcRequisitionDetail1 = New XpertERPHRandPayroll.ucRequisitionDetail
        Me.txtFixedCTC = New common.MyNumBox
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.dtpDOJ = New common.Controls.MyDateTimePicker
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.MyLabel12 = New common.Controls.MyLabel
        Me.dtpAppointmentDate = New common.Controls.MyDateTimePicker
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.RadMenu = New Telerik.WinControls.UI.RadMenuItem
        Me.rmEmail = New Telerik.WinControls.UI.RadMenuItem
        Me.btnPrint = New Telerik.WinControls.UI.RadButton
        Me.btnsave = New Telerik.WinControls.UI.RadButton
        Me.btnsendmail = New Telerik.WinControls.UI.RadButton
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFixedCTC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDOJ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpAppointmentDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsendmail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsendmail)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(716, 245)
        Me.SplitContainer1.SplitterDistance = 203
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer3.IsSplitterFixed = True
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.txtFixedCTC)
        Me.SplitContainer3.Panel2.Controls.Add(Me.dtpDOJ)
        Me.SplitContainer3.Panel2.Controls.Add(Me.MyLabel2)
        Me.SplitContainer3.Panel2.Controls.Add(Me.MyLabel1)
        Me.SplitContainer3.Panel2.Controls.Add(Me.MyLabel12)
        Me.SplitContainer3.Panel2.Controls.Add(Me.dtpAppointmentDate)
        Me.SplitContainer3.Size = New System.Drawing.Size(716, 183)
        Me.SplitContainer3.SplitterDistance = 126
        Me.SplitContainer3.TabIndex = 341
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtAppcode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnnew)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.UcRequisitionDetail1)
        Me.SplitContainer2.Size = New System.Drawing.Size(716, 126)
        Me.SplitContainer2.SplitterDistance = 32
        Me.SplitContainer2.TabIndex = 340
        '
        'MyLabel3
        '
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(15, 8)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(83, 16)
        Me.MyLabel3.TabIndex = 334
        Me.MyLabel3.Text = "Applicant Code"
        '
        'txtAppcode
        '
        Me.txtAppcode.Location = New System.Drawing.Point(106, 6)
        Me.txtAppcode.MendatroryField = True
        Me.txtAppcode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAppcode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtAppcode.MyLinkLable1 = Me.MyLabel3
        Me.txtAppcode.MyLinkLable2 = Nothing
        Me.txtAppcode.MyMaxLength = 30
        Me.txtAppcode.MyReadOnly = False
        Me.txtAppcode.Name = "txtAppcode"
        Me.txtAppcode.Size = New System.Drawing.Size(202, 21)
        Me.txtAppcode.TabIndex = 0
        Me.txtAppcode.TabStop = False
        Me.txtAppcode.Value = ""
        '
        'btnnew
        '
        Me.btnnew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnnew.Image = Global.XpertERPHRandPayroll.My.Resources.Resources._new
        Me.btnnew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnnew.Location = New System.Drawing.Point(308, 6)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(14, 21)
        Me.btnnew.TabIndex = 1
        '
        'UcRequisitionDetail1
        '
        Me.UcRequisitionDetail1.AppCode = ""
        Me.UcRequisitionDetail1.AppDate = ""
        Me.UcRequisitionDetail1.AppName = ""
        Me.UcRequisitionDetail1.DateofBirth = ""
        Me.UcRequisitionDetail1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcRequisitionDetail1.Email = ""
        Me.UcRequisitionDetail1.Location = New System.Drawing.Point(0, 0)
        Me.UcRequisitionDetail1.Name = "UcRequisitionDetail1"
        Me.UcRequisitionDetail1.ReqCode = ""
        Me.UcRequisitionDetail1.Size = New System.Drawing.Size(716, 90)
        Me.UcRequisitionDetail1.TabIndex = 333
        Me.UcRequisitionDetail1.TelephoneNo = ""
        '
        'txtFixedCTC
        '
        Me.txtFixedCTC.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtFixedCTC.DecimalPlaces = 3
        Me.txtFixedCTC.Location = New System.Drawing.Point(590, 8)
        Me.txtFixedCTC.MendatroryField = True
        Me.txtFixedCTC.MyLinkLable1 = Me.MyLabel2
        Me.txtFixedCTC.MyLinkLable2 = Nothing
        Me.txtFixedCTC.Name = "txtFixedCTC"
        Me.txtFixedCTC.ReadOnly = True
        Me.txtFixedCTC.Size = New System.Drawing.Size(118, 20)
        Me.txtFixedCTC.TabIndex = 3
        Me.txtFixedCTC.Text = "0"
        Me.txtFixedCTC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtFixedCTC.Value = 0
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(421, 10)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(159, 16)
        Me.MyLabel2.TabIndex = 336
        Me.MyLabel2.Text = "Enter fixed CTC (In Rs/Month)"
        '
        'dtpDOJ
        '
        Me.dtpDOJ.CustomFormat = "dd/MM/yyyy "
        Me.dtpDOJ.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDOJ.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDOJ.Location = New System.Drawing.Point(140, 29)
        Me.dtpDOJ.MendatroryField = True
        Me.dtpDOJ.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDOJ.MyLinkLable1 = Me.MyLabel1
        Me.dtpDOJ.MyLinkLable2 = Nothing
        Me.dtpDOJ.Name = "dtpDOJ"
        Me.dtpDOJ.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDOJ.Size = New System.Drawing.Size(86, 18)
        Me.dtpDOJ.TabIndex = 4
        Me.dtpDOJ.TabStop = False
        Me.dtpDOJ.Text = "03/05/2011 "
        Me.dtpDOJ.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(8, 30)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(82, 16)
        Me.MyLabel1.TabIndex = 339
        Me.MyLabel1.Text = "Date of Joining"
        '
        'MyLabel12
        '
        Me.MyLabel12.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel12.Location = New System.Drawing.Point(8, 8)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(129, 16)
        Me.MyLabel12.TabIndex = 338
        Me.MyLabel12.Text = "Appointment Letter Date"
        '
        'dtpAppointmentDate
        '
        Me.dtpAppointmentDate.CustomFormat = "dd/MM/yyyy "
        Me.dtpAppointmentDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpAppointmentDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpAppointmentDate.Location = New System.Drawing.Point(140, 7)
        Me.dtpAppointmentDate.MendatroryField = True
        Me.dtpAppointmentDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpAppointmentDate.MyLinkLable1 = Me.MyLabel12
        Me.dtpAppointmentDate.MyLinkLable2 = Nothing
        Me.dtpAppointmentDate.Name = "dtpAppointmentDate"
        Me.dtpAppointmentDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpAppointmentDate.Size = New System.Drawing.Size(86, 18)
        Me.dtpAppointmentDate.TabIndex = 2
        Me.dtpAppointmentDate.TabStop = False
        Me.dtpAppointmentDate.Text = "03/05/2011 "
        Me.dtpAppointmentDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenu})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(716, 20)
        Me.RadMenu1.TabIndex = 335
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenu
        '
        Me.RadMenu.AccessibleDescription = "Setting"
        Me.RadMenu.AccessibleName = "Setting"
        Me.RadMenu.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmEmail})
        Me.RadMenu.Name = "RadMenu"
        Me.RadMenu.Text = "Settings"
        Me.RadMenu.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'rmEmail
        '
        Me.rmEmail.AccessibleDescription = "MAi"
        Me.rmEmail.AccessibleName = "MAi"
        Me.rmEmail.Name = "rmEmail"
        Me.rmEmail.Text = "Email Settings"
        Me.rmEmail.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(75, 11)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(66, 18)
        Me.btnPrint.TabIndex = 6
        Me.btnPrint.Text = "Print"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(7, 11)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 5
        Me.btnsave.Text = "Save"
        '
        'btnsendmail
        '
        Me.btnsendmail.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsendmail.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsendmail.Location = New System.Drawing.Point(143, 11)
        Me.btnsendmail.Name = "btnsendmail"
        Me.btnsendmail.Size = New System.Drawing.Size(66, 18)
        Me.btnsendmail.TabIndex = 7
        Me.btnsendmail.Text = "Send Mail"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(645, 11)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 8
        Me.btnclose.Text = "Close"
        '
        'frmAppointmentLetterHR
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(716, 245)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmAppointmentLetterHR"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Appointment Letter"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.Panel2.PerformLayout()
        Me.SplitContainer3.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFixedCTC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDOJ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpAppointmentDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsendmail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents dtpDOJ As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents dtpAppointmentDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents txtFixedCTC As common.MyNumBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenu As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtAppcode As common.UserControls.txtNavigator
    Friend WithEvents UcRequisitionDetail1 As XpertERPHRandPayroll.ucRequisitionDetail
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsendmail As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents rmEmail As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
End Class

