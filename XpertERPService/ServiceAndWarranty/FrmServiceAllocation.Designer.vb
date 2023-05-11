<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmServiceAllocation
    Inherits XpertERPEngine.FrmMainTranScreen

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
        Me.GrpSerEnq = New System.Windows.Forms.GroupBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.LblIssueN = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.LblItemP = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.LblVehicle = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LblDealer = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LblCustGrp = New System.Windows.Forms.Label()
        Me.LblEmpName = New common.Controls.MyLabel()
        Me.TxtEmpCode = New common.UserControls.txtFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.LblSerDoc = New common.Controls.MyLabel()
        Me.TxtSerDoc = New common.UserControls.txtFinder()
        Me.MyLabel44 = New common.Controls.MyLabel()
        Me.lblMCCCode = New common.Controls.MyLabel()
        Me.txtcode = New common.UserControls.txtNavigator()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.dtpDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.GrpSerEnq.SuspendLayout()
        CType(Me.LblEmpName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblSerDoc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel44, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMCCCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.GrpSerEnq)
        Me.SplitContainer1.Panel1.Controls.Add(Me.LblEmpName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TxtEmpCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.LblSerDoc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TxtSerDoc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel44)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblMCCCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtcode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnnew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel12)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(605, 245)
        Me.SplitContainer1.SplitterDistance = 209
        Me.SplitContainer1.TabIndex = 0
        '
        'GrpSerEnq
        '
        Me.GrpSerEnq.Controls.Add(Me.Label10)
        Me.GrpSerEnq.Controls.Add(Me.LblIssueN)
        Me.GrpSerEnq.Controls.Add(Me.Label6)
        Me.GrpSerEnq.Controls.Add(Me.LblItemP)
        Me.GrpSerEnq.Controls.Add(Me.Label4)
        Me.GrpSerEnq.Controls.Add(Me.LblVehicle)
        Me.GrpSerEnq.Controls.Add(Me.Label2)
        Me.GrpSerEnq.Controls.Add(Me.LblDealer)
        Me.GrpSerEnq.Controls.Add(Me.Label1)
        Me.GrpSerEnq.Controls.Add(Me.LblCustGrp)
        Me.GrpSerEnq.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GrpSerEnq.Location = New System.Drawing.Point(5, 101)
        Me.GrpSerEnq.Name = "GrpSerEnq"
        Me.GrpSerEnq.Size = New System.Drawing.Size(597, 103)
        Me.GrpSerEnq.TabIndex = 133
        Me.GrpSerEnq.TabStop = False
        Me.GrpSerEnq.Text = "Service Enquiry Details"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(12, 81)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(81, 14)
        Me.Label10.TabIndex = 37
        Me.Label10.Text = "Issue Notice :"
        '
        'LblIssueN
        '
        Me.LblIssueN.AutoSize = True
        Me.LblIssueN.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblIssueN.Location = New System.Drawing.Point(136, 81)
        Me.LblIssueN.Name = "LblIssueN"
        Me.LblIssueN.Size = New System.Drawing.Size(63, 14)
        Me.LblIssueN.TabIndex = 36
        Me.LblIssueN.Text = "IssueNotice"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(354, 55)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(83, 14)
        Me.Label6.TabIndex = 35
        Me.Label6.Text = "Item Part No. :"
        '
        'LblItemP
        '
        Me.LblItemP.AutoSize = True
        Me.LblItemP.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblItemP.Location = New System.Drawing.Point(462, 55)
        Me.LblItemP.Name = "LblItemP"
        Me.LblItemP.Size = New System.Drawing.Size(32, 14)
        Me.LblItemP.TabIndex = 34
        Me.LblItemP.Text = "ItemP"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(12, 55)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(87, 14)
        Me.Label4.TabIndex = 33
        Me.Label4.Text = "Vehicle Name :"
        '
        'LblVehicle
        '
        Me.LblVehicle.AutoSize = True
        Me.LblVehicle.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblVehicle.Location = New System.Drawing.Point(136, 55)
        Me.LblVehicle.Name = "LblVehicle"
        Me.LblVehicle.Size = New System.Drawing.Size(42, 14)
        Me.LblVehicle.TabIndex = 32
        Me.LblVehicle.Text = "Vehicle"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(354, 30)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(82, 14)
        Me.Label2.TabIndex = 31
        Me.Label2.Text = "Dealer Name :"
        '
        'LblDealer
        '
        Me.LblDealer.AutoSize = True
        Me.LblDealer.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDealer.Location = New System.Drawing.Point(462, 30)
        Me.LblDealer.Name = "LblDealer"
        Me.LblDealer.Size = New System.Drawing.Size(38, 14)
        Me.LblDealer.TabIndex = 30
        Me.LblDealer.Text = "Dealer"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(106, 14)
        Me.Label1.TabIndex = 29
        Me.Label1.Text = "Customer Group :"
        '
        'LblCustGrp
        '
        Me.LblCustGrp.AutoSize = True
        Me.LblCustGrp.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCustGrp.Location = New System.Drawing.Point(136, 30)
        Me.LblCustGrp.Name = "LblCustGrp"
        Me.LblCustGrp.Size = New System.Drawing.Size(47, 14)
        Me.LblCustGrp.TabIndex = 28
        Me.LblCustGrp.Text = "CustGrp"
        '
        'LblEmpName
        '
        Me.LblEmpName.AutoSize = False
        Me.LblEmpName.BorderVisible = True
        Me.LblEmpName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblEmpName.Location = New System.Drawing.Point(299, 62)
        Me.LblEmpName.Name = "LblEmpName"
        Me.LblEmpName.Size = New System.Drawing.Size(303, 18)
        Me.LblEmpName.TabIndex = 132
        Me.LblEmpName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblEmpName.TextWrap = False
        '
        'TxtEmpCode
        '
        Me.TxtEmpCode.Location = New System.Drawing.Point(136, 62)
        Me.TxtEmpCode.MendatroryField = True
        Me.TxtEmpCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtEmpCode.MyLinkLable1 = Me.MyLabel2
        Me.TxtEmpCode.MyLinkLable2 = Nothing
        Me.TxtEmpCode.MyReadOnly = False
        Me.TxtEmpCode.MyShowMasterFormButton = False
        Me.TxtEmpCode.Name = "TxtEmpCode"
        Me.TxtEmpCode.Size = New System.Drawing.Size(159, 19)
        Me.TxtEmpCode.TabIndex = 130
        Me.TxtEmpCode.Value = ""
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(12, 63)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(64, 16)
        Me.MyLabel2.TabIndex = 131
        Me.MyLabel2.Text = "Engineer Id"
        '
        'LblSerDoc
        '
        Me.LblSerDoc.AutoSize = False
        Me.LblSerDoc.BorderVisible = True
        Me.LblSerDoc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblSerDoc.Location = New System.Drawing.Point(299, 39)
        Me.LblSerDoc.Name = "LblSerDoc"
        Me.LblSerDoc.Size = New System.Drawing.Size(303, 18)
        Me.LblSerDoc.TabIndex = 129
        Me.LblSerDoc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblSerDoc.TextWrap = False
        '
        'TxtSerDoc
        '
        Me.TxtSerDoc.Location = New System.Drawing.Point(136, 39)
        Me.TxtSerDoc.MendatroryField = True
        Me.TxtSerDoc.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSerDoc.MyLinkLable1 = Me.MyLabel44
        Me.TxtSerDoc.MyLinkLable2 = Nothing
        Me.TxtSerDoc.MyReadOnly = False
        Me.TxtSerDoc.MyShowMasterFormButton = False
        Me.TxtSerDoc.Name = "TxtSerDoc"
        Me.TxtSerDoc.Size = New System.Drawing.Size(159, 19)
        Me.TxtSerDoc.TabIndex = 125
        Me.TxtSerDoc.Value = ""
        '
        'MyLabel44
        '
        Me.MyLabel44.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel44.Location = New System.Drawing.Point(12, 40)
        Me.MyLabel44.Name = "MyLabel44"
        Me.MyLabel44.Size = New System.Drawing.Size(120, 16)
        Me.MyLabel44.TabIndex = 128
        Me.MyLabel44.Text = "Service Document No."
        '
        'lblMCCCode
        '
        Me.lblMCCCode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblMCCCode.Location = New System.Drawing.Point(12, 16)
        Me.lblMCCCode.Name = "lblMCCCode"
        Me.lblMCCCode.Size = New System.Drawing.Size(33, 16)
        Me.lblMCCCode.TabIndex = 126
        Me.lblMCCCode.Text = "Code"
        '
        'txtcode
        '
        Me.txtcode.Location = New System.Drawing.Point(136, 14)
        Me.txtcode.MendatroryField = True
        Me.txtcode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtcode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtcode.MyLinkLable1 = Me.lblMCCCode
        Me.txtcode.MyLinkLable2 = Nothing
        Me.txtcode.MyMaxLength = 30
        Me.txtcode.MyReadOnly = False
        Me.txtcode.Name = "txtcode"
        Me.txtcode.Size = New System.Drawing.Size(199, 21)
        Me.txtcode.TabIndex = 123
        Me.txtcode.Value = ""
        '
        'btnnew
        '
        Me.btnnew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnnew.Image = My.Resources._new
        Me.btnnew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnnew.Location = New System.Drawing.Point(335, 14)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(14, 21)
        Me.btnnew.TabIndex = 122
        '
        'dtpDate
        '
        Me.dtpDate.CustomFormat = "dd/MM/yyyy "
        Me.dtpDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDate.Location = New System.Drawing.Point(512, 15)
        Me.dtpDate.MendatroryField = True
        Me.dtpDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDate.MyLinkLable1 = Me.MyLabel12
        Me.dtpDate.MyLinkLable2 = Nothing
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDate.Size = New System.Drawing.Size(88, 18)
        Me.dtpDate.TabIndex = 124
        Me.dtpDate.TabStop = False
        Me.dtpDate.Text = "03/05/2011 "
        Me.dtpDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'MyLabel12
        '
        Me.MyLabel12.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel12.Location = New System.Drawing.Point(469, 16)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(30, 16)
        Me.MyLabel12.TabIndex = 127
        Me.MyLabel12.Text = "Date"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(9, 6)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 6
        Me.btnsave.Text = "Save"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(88, 6)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 7
        Me.btndelete.Text = "Delete"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(529, 6)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 8
        Me.btnclose.Text = "Close"
        '
        'FrmServiceAllocation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(605, 245)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmServiceAllocation"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Service Allocation"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.GrpSerEnq.ResumeLayout(False)
        Me.GrpSerEnq.PerformLayout()
        CType(Me.LblEmpName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblSerDoc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel44, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMCCCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents LblSerDoc As common.Controls.MyLabel
    Friend WithEvents TxtSerDoc As common.UserControls.txtFinder
    Friend WithEvents MyLabel44 As common.Controls.MyLabel
    Friend WithEvents lblMCCCode As common.Controls.MyLabel
    Friend WithEvents txtcode As common.UserControls.txtNavigator
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents dtpDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents LblEmpName As common.Controls.MyLabel
    Friend WithEvents TxtEmpCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents GrpSerEnq As System.Windows.Forms.GroupBox
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents LblCustGrp As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents LblDealer As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents LblVehicle As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents LblItemP As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents LblIssueN As System.Windows.Forms.Label
End Class
