<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMilkCollectionLevels
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
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.rdlbldescription = New common.Controls.MyLabel()
        Me.txtAccdescription = New common.Controls.MyTextBox()
        Me.lblHigherLevelDesg = New common.Controls.MyLabel()
        Me.lblHigherLevelDesgName = New common.Controls.MyLabel()
        Me.txtParentCode = New common.UserControls.txtFinder()
        Me.btnUser = New Telerik.WinControls.UI.RadButton()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.lblDesignation = New common.Controls.MyLabel()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.rdlbldescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAccdescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblHigherLevelDesg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblHigherLevelDesgName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUser, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDesignation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(340, 6)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 20)
        Me.btnAddNew.TabIndex = 1
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdlbldescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtAccdescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblHigherLevelDesg)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblHigherLevelDesgName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtParentCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnUser)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnAddNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDesignation)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(547, 139)
        Me.SplitContainer1.SplitterDistance = 110
        Me.SplitContainer1.TabIndex = 1
        '
        'rdlbldescription
        '
        Me.rdlbldescription.FieldName = Nothing
        Me.rdlbldescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdlbldescription.Location = New System.Drawing.Point(8, 35)
        Me.rdlbldescription.Name = "rdlbldescription"
        Me.rdlbldescription.Size = New System.Drawing.Size(63, 16)
        Me.rdlbldescription.TabIndex = 27
        Me.rdlbldescription.Text = "Description"
        '
        'txtAccdescription
        '
        Me.txtAccdescription.CalculationExpression = Nothing
        Me.txtAccdescription.FieldCode = Nothing
        Me.txtAccdescription.FieldDesc = Nothing
        Me.txtAccdescription.FieldMaxLength = 0
        Me.txtAccdescription.FieldName = Nothing
        Me.txtAccdescription.isCalculatedField = False
        Me.txtAccdescription.IsSourceFromTable = False
        Me.txtAccdescription.IsSourceFromValueList = False
        Me.txtAccdescription.IsUnique = False
        Me.txtAccdescription.Location = New System.Drawing.Point(94, 31)
        Me.txtAccdescription.MaxLength = 50
        Me.txtAccdescription.MendatroryField = True
        Me.txtAccdescription.MyLinkLable1 = Me.rdlbldescription
        Me.txtAccdescription.MyLinkLable2 = Nothing
        Me.txtAccdescription.Name = "txtAccdescription"
        Me.txtAccdescription.ReferenceFieldDesc = Nothing
        Me.txtAccdescription.ReferenceFieldName = Nothing
        Me.txtAccdescription.ReferenceTableName = Nothing
        Me.txtAccdescription.Size = New System.Drawing.Size(416, 20)
        Me.txtAccdescription.TabIndex = 1
        '
        'lblHigherLevelDesg
        '
        Me.lblHigherLevelDesg.FieldName = Nothing
        Me.lblHigherLevelDesg.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHigherLevelDesg.Location = New System.Drawing.Point(8, 56)
        Me.lblHigherLevelDesg.Name = "lblHigherLevelDesg"
        Me.lblHigherLevelDesg.Size = New System.Drawing.Size(70, 16)
        Me.lblHigherLevelDesg.TabIndex = 24
        Me.lblHigherLevelDesg.Text = "Parent Code"
        '
        'lblHigherLevelDesgName
        '
        Me.lblHigherLevelDesgName.AutoSize = False
        Me.lblHigherLevelDesgName.BorderVisible = True
        Me.lblHigherLevelDesgName.FieldName = Nothing
        Me.lblHigherLevelDesgName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHigherLevelDesgName.Location = New System.Drawing.Point(322, 54)
        Me.lblHigherLevelDesgName.Name = "lblHigherLevelDesgName"
        Me.lblHigherLevelDesgName.Size = New System.Drawing.Size(188, 19)
        Me.lblHigherLevelDesgName.TabIndex = 25
        Me.lblHigherLevelDesgName.TextWrap = False
        '
        'txtParentCode
        '
        Me.txtParentCode.CalculationExpression = Nothing
        Me.txtParentCode.FieldCode = Nothing
        Me.txtParentCode.FieldDesc = Nothing
        Me.txtParentCode.FieldMaxLength = 0
        Me.txtParentCode.FieldName = Nothing
        Me.txtParentCode.isCalculatedField = False
        Me.txtParentCode.IsSourceFromTable = False
        Me.txtParentCode.IsSourceFromValueList = False
        Me.txtParentCode.IsUnique = False
        Me.txtParentCode.Location = New System.Drawing.Point(94, 53)
        Me.txtParentCode.MendatroryField = True
        Me.txtParentCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtParentCode.MyLinkLable1 = Nothing
        Me.txtParentCode.MyLinkLable2 = Me.lblHigherLevelDesg
        Me.txtParentCode.MyReadOnly = False
        Me.txtParentCode.MyShowMasterFormButton = False
        Me.txtParentCode.Name = "txtParentCode"
        Me.txtParentCode.ReferenceFieldDesc = Nothing
        Me.txtParentCode.ReferenceFieldName = Nothing
        Me.txtParentCode.ReferenceTableName = Nothing
        Me.txtParentCode.Size = New System.Drawing.Size(223, 19)
        Me.txtParentCode.TabIndex = 2
        Me.txtParentCode.Value = ""
        '
        'btnUser
        '
        Me.btnUser.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUser.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnUser.Location = New System.Drawing.Point(516, 53)
        Me.btnUser.Name = "btnUser"
        Me.btnUser.Size = New System.Drawing.Size(20, 19)
        Me.btnUser.TabIndex = 5
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(94, 9)
        Me.txtCode.MendatroryField = False
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblDesignation
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 30
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(242, 20)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'lblDesignation
        '
        Me.lblDesignation.FieldName = Nothing
        Me.lblDesignation.Location = New System.Drawing.Point(7, 12)
        Me.lblDesignation.Name = "lblDesignation"
        Me.lblDesignation.Size = New System.Drawing.Size(32, 18)
        Me.lblDesignation.TabIndex = 11
        Me.lblDesignation.Text = "Code"
        '
        'btnClose
        '
        Me.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(475, 1)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(77, 1)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 22)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(2, 1)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'frmMilkCollectionLevels
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(547, 139)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmMilkCollectionLevels"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Milk Collection Levels"
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.rdlbldescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAccdescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblHigherLevelDesg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblHigherLevelDesgName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUser, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDesignation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents lblDesignation As common.Controls.MyLabel
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnUser As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblHigherLevelDesg As common.Controls.MyLabel
    Friend WithEvents lblHigherLevelDesgName As common.Controls.MyLabel
    Friend WithEvents txtParentCode As common.UserControls.txtFinder
    Friend WithEvents rdlbldescription As common.Controls.MyLabel
    Friend WithEvents txtAccdescription As common.Controls.MyTextBox
End Class

