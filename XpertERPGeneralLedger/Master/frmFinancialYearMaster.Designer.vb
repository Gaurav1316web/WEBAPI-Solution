<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFinancialYearMaster
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
        Me.btnSave = New Telerik.WinControls.UI.RadButton
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.txtName = New Telerik.WinControls.UI.RadTextBox
        Me.btnDelete = New Telerik.WinControls.UI.RadButton
        Me.txtCode = New common.UserControls.txtNavigator
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.txtStartDate = New common.Controls.MyDateTimePicker
        Me.MyLabel3 = New common.Controls.MyLabel
        Me.txtEndDate = New common.Controls.MyDateTimePicker
        Me.ChkCurrFin = New Telerik.WinControls.UI.RadCheckBox
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtStartDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEndDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkCurrFin, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(8, 8)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = My.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(264, 11)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 20)
        Me.btnAddNew.TabIndex = 1
        '
        'RadLabel2
        '
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(15, 13)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(33, 16)
        Me.RadLabel2.TabIndex = 35
        Me.RadLabel2.Text = "Code"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(369, 8)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(71, 22)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(77, 38)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(362, 20)
        Me.txtName.TabIndex = 3
        '
        'btnDelete
        '
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(81, 8)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 22)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'txtCode
        '
        Me.txtCode.Location = New System.Drawing.Point(77, 11)
        Me.txtCode.MendatroryField = False
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Nothing
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 32767
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(187, 20)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(15, 40)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(36, 16)
        Me.MyLabel1.TabIndex = 43
        Me.MyLabel1.Text = "Name"
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(15, 66)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(56, 18)
        Me.MyLabel2.TabIndex = 48
        Me.MyLabel2.Text = "Start Date"
        '
        'txtStartDate
        '
        Me.txtStartDate.CustomFormat = "dd/MM/yyyy"
        Me.txtStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtStartDate.Location = New System.Drawing.Point(77, 65)
        Me.txtStartDate.MendatroryField = False
        Me.txtStartDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtStartDate.MyLinkLable1 = Me.MyLabel2
        Me.txtStartDate.MyLinkLable2 = Nothing
        Me.txtStartDate.Name = "txtStartDate"
        Me.txtStartDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtStartDate.Size = New System.Drawing.Size(80, 20)
        Me.txtStartDate.TabIndex = 4
        Me.txtStartDate.TabStop = False
        Me.txtStartDate.Text = "28/06/2012"
        Me.txtStartDate.Value = New Date(2012, 6, 28, 14, 31, 57, 31)
        '
        'MyLabel3
        '
        Me.MyLabel3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(174, 66)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(52, 18)
        Me.MyLabel3.TabIndex = 45
        Me.MyLabel3.Text = "End Date"
        '
        'txtEndDate
        '
        Me.txtEndDate.CustomFormat = "dd/MM/yyyy"
        Me.txtEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtEndDate.Location = New System.Drawing.Point(232, 65)
        Me.txtEndDate.MendatroryField = False
        Me.txtEndDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtEndDate.MyLinkLable1 = Me.MyLabel3
        Me.txtEndDate.MyLinkLable2 = Nothing
        Me.txtEndDate.Name = "txtEndDate"
        Me.txtEndDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtEndDate.ReadOnly = True
        Me.txtEndDate.Size = New System.Drawing.Size(80, 20)
        Me.txtEndDate.TabIndex = 5
        Me.txtEndDate.TabStop = False
        Me.txtEndDate.Text = "28/06/2012"
        Me.txtEndDate.Value = New Date(2012, 6, 28, 14, 31, 57, 31)
        '
        'ChkCurrFin
        '
        Me.ChkCurrFin.Location = New System.Drawing.Point(311, 12)
        Me.ChkCurrFin.Name = "ChkCurrFin"
        Me.ChkCurrFin.Size = New System.Drawing.Size(129, 18)
        Me.ChkCurrFin.TabIndex = 2
        Me.ChkCurrFin.Text = "Current Financial Year"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ChkCurrFin)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtStartDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnAddNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtEndDate)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(451, 139)
        Me.SplitContainer1.SplitterDistance = 97
        Me.SplitContainer1.TabIndex = 50
        '
        'frmFinancialYearMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(451, 139)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmFinancialYearMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = " Financial Year Master"
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtStartDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEndDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkCurrFin, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtName As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtStartDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtEndDate As common.Controls.MyDateTimePicker
    Friend WithEvents ChkCurrFin As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

