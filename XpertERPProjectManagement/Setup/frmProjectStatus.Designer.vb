<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmProjectStatus
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
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.rdgpbxcustomeraccountset = New Telerik.WinControls.UI.RadGroupBox
        Me.ddlProjStatus = New common.Controls.MyComboBox
        Me.lblJobType = New common.Controls.MyLabel
        Me.MyLabel3 = New common.Controls.MyLabel
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.lblDate = New common.Controls.MyLabel
        Me.lblUser = New common.Controls.MyLabel
        Me.rdbtnnew = New Telerik.WinControls.UI.RadButton
        Me.WIP = New common.Controls.MyLabel
        Me.txtProjectdesc = New common.Controls.MyTextBox
        Me.fndProject = New common.UserControls.txtFinder
        Me.btnSave = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.rdgpbxcustomeraccountset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.rdgpbxcustomeraccountset.SuspendLayout()
        CType(Me.ddlProjStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblJobType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblUser, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.WIP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtProjectdesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdgpbxcustomeraccountset)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(567, 137)
        Me.SplitContainer1.SplitterDistance = 108
        Me.SplitContainer1.TabIndex = 7
        '
        'rdgpbxcustomeraccountset
        '
        Me.rdgpbxcustomeraccountset.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.ddlProjStatus)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.MyLabel3)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.MyLabel2)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.lblJobType)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.lblDate)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.lblUser)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.rdbtnnew)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.WIP)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.txtProjectdesc)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.fndProject)
        Me.rdgpbxcustomeraccountset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdgpbxcustomeraccountset.HeaderText = ""
        Me.rdgpbxcustomeraccountset.Location = New System.Drawing.Point(3, 3)
        Me.rdgpbxcustomeraccountset.Name = "rdgpbxcustomeraccountset"
        Me.rdgpbxcustomeraccountset.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.rdgpbxcustomeraccountset.Size = New System.Drawing.Size(561, 99)
        Me.rdgpbxcustomeraccountset.TabIndex = 0
        '
        'ddlProjStatus
        '
        Me.ddlProjStatus.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlProjStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        RadListDataItem1.Text = "Open"
        RadListDataItem1.TextWrap = True
        RadListDataItem2.Text = "Approved"
        RadListDataItem2.TextWrap = True
        RadListDataItem3.Text = "On Hold"
        RadListDataItem3.TextWrap = True
        RadListDataItem4.Text = "Discountinued"
        RadListDataItem4.TextWrap = True
        Me.ddlProjStatus.Items.Add(RadListDataItem1)
        Me.ddlProjStatus.Items.Add(RadListDataItem2)
        Me.ddlProjStatus.Items.Add(RadListDataItem3)
        Me.ddlProjStatus.Items.Add(RadListDataItem4)
        Me.ddlProjStatus.Location = New System.Drawing.Point(59, 28)
        Me.ddlProjStatus.MendatroryField = True
        Me.ddlProjStatus.MyLinkLable1 = Me.lblJobType
        Me.ddlProjStatus.MyLinkLable2 = Nothing
        Me.ddlProjStatus.Name = "ddlProjStatus"
        Me.ddlProjStatus.Size = New System.Drawing.Size(219, 18)
        Me.ddlProjStatus.TabIndex = 3
        '
        'lblJobType
        '
        Me.lblJobType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblJobType.Location = New System.Drawing.Point(9, 29)
        Me.lblJobType.Name = "lblJobType"
        Me.lblJobType.Size = New System.Drawing.Size(38, 16)
        Me.lblJobType.TabIndex = 217
        Me.lblJobType.Text = "Status"
        '
        'MyLabel3
        '
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(9, 69)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(30, 16)
        Me.MyLabel3.TabIndex = 12
        Me.MyLabel3.Text = "Date"
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(9, 49)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(30, 16)
        Me.MyLabel2.TabIndex = 12
        Me.MyLabel2.Text = "User"
        '
        'lblDate
        '
        Me.lblDate.AutoSize = False
        Me.lblDate.BorderVisible = True
        Me.lblDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate.Location = New System.Drawing.Point(59, 68)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(289, 18)
        Me.lblDate.TabIndex = 5
        Me.lblDate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblDate.TextWrap = False
        '
        'lblUser
        '
        Me.lblUser.AutoSize = False
        Me.lblUser.BorderVisible = True
        Me.lblUser.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUser.Location = New System.Drawing.Point(59, 48)
        Me.lblUser.Name = "lblUser"
        Me.lblUser.Size = New System.Drawing.Size(289, 18)
        Me.lblUser.TabIndex = 4
        Me.lblUser.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblUser.TextWrap = False
        '
        'rdbtnnew
        '
        Me.rdbtnnew.Image = My.Resources._new
        Me.rdbtnnew.Location = New System.Drawing.Point(539, 7)
        Me.rdbtnnew.Name = "rdbtnnew"
        Me.rdbtnnew.Size = New System.Drawing.Size(18, 18)
        Me.rdbtnnew.TabIndex = 2
        '
        'WIP
        '
        Me.WIP.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.WIP.Location = New System.Drawing.Point(9, 9)
        Me.WIP.Name = "WIP"
        Me.WIP.Size = New System.Drawing.Size(41, 16)
        Me.WIP.TabIndex = 11
        Me.WIP.Text = "Project"
        '
        'txtProjectdesc
        '
        Me.txtProjectdesc.Location = New System.Drawing.Point(210, 5)
        Me.txtProjectdesc.MendatroryField = False
        Me.txtProjectdesc.MyLinkLable1 = Me.WIP
        Me.txtProjectdesc.MyLinkLable2 = Nothing
        Me.txtProjectdesc.Name = "txtProjectdesc"
        Me.txtProjectdesc.ReadOnly = True
        Me.txtProjectdesc.Size = New System.Drawing.Size(326, 20)
        Me.txtProjectdesc.TabIndex = 1
        Me.txtProjectdesc.TabStop = False
        '
        'fndProject
        '
        Me.fndProject.Location = New System.Drawing.Point(59, 7)
        Me.fndProject.MendatroryField = False
        Me.fndProject.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndProject.MyLinkLable1 = Me.WIP
        Me.fndProject.MyLinkLable2 = Nothing
        Me.fndProject.MyReadOnly = False
        Me.fndProject.Name = "fndProject"
        Me.fndProject.Size = New System.Drawing.Size(143, 19)
        Me.fndProject.TabIndex = 0
        Me.fndProject.Value = ""
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(9, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(82, 18)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(473, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(83, 18)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "Close"
        '
        'FrmProjectStatus
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(567, 137)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmProjectStatus"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Project Status"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.rdgpbxcustomeraccountset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.rdgpbxcustomeraccountset.ResumeLayout(False)
        Me.rdgpbxcustomeraccountset.PerformLayout()
        CType(Me.ddlProjStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblJobType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblUser, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.WIP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtProjectdesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents rdgpbxcustomeraccountset As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rdbtnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents WIP As common.Controls.MyLabel
    Friend WithEvents txtProjectdesc As common.Controls.MyTextBox
    Friend WithEvents fndProject As common.UserControls.txtFinder
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents lblDate As common.Controls.MyLabel
    Friend WithEvents lblUser As common.Controls.MyLabel
    Friend WithEvents ddlProjStatus As common.Controls.MyComboBox
    Friend WithEvents lblJobType As common.Controls.MyLabel
End Class

