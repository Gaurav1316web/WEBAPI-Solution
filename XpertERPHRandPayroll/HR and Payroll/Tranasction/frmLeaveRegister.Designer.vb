Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLeaveRegister
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
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.UsLock1 = New common.usLock()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.lblRemarks = New common.Controls.MyLabel()
        Me.lblEnteredBy = New common.Controls.MyLabel()
        Me.findEnteredBy = New common.UserControls.txtFinder()
        Me.lblLeaveDate = New common.Controls.MyLabel()
        Me.dtpLeaveDate = New common.Controls.MyDateTimePicker()
        Me.findLeaveRegisterCode = New common.UserControls.txtNavigator()
        Me.lblLeaveRegisterCode = New common.Controls.MyLabel()
        Me.gvLeaveRegister = New common.UserControls.MyRadGridView()
        Me.ddlFromPayPeriod = New common.Controls.MyComboBox()
        Me.lblPayPeriod = New common.Controls.MyLabel()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEnteredBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLeaveDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpLeaveDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLeaveRegisterCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvLeaveRegister, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvLeaveRegister.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlFromPayPeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPayPeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.RadGroupBox3.HeaderText = ""
        Me.RadGroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(862, 522)
        Me.RadGroupBox3.TabIndex = 65
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(10, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblRemarks)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblEnteredBy)
        Me.SplitContainer1.Panel1.Controls.Add(Me.findEnteredBy)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLeaveDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpLeaveDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.findLeaveRegisterCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLeaveRegisterCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.gvLeaveRegister)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ddlFromPayPeriod)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblPayPeriod)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(842, 492)
        Me.SplitContainer1.SplitterDistance = 442
        Me.SplitContainer1.TabIndex = 0
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(735, 19)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 18)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 209
        '
        'txtDescription
        '
        Me.txtDescription.AutoSize = False
        Me.txtDescription.CalculationExpression = Nothing
        Me.txtDescription.FieldCode = Nothing
        Me.txtDescription.FieldDesc = Nothing
        Me.txtDescription.FieldMaxLength = 0
        Me.txtDescription.FieldName = Nothing
        Me.txtDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.isCalculatedField = False
        Me.txtDescription.IsSourceFromTable = False
        Me.txtDescription.IsSourceFromValueList = False
        Me.txtDescription.IsUnique = False
        Me.txtDescription.Location = New System.Drawing.Point(468, 46)
        Me.txtDescription.MaxLength = 49
        Me.txtDescription.MendatroryField = True
        Me.txtDescription.Multiline = True
        Me.txtDescription.MyLinkLable1 = Me.lblRemarks
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReferenceFieldDesc = Nothing
        Me.txtDescription.ReferenceFieldName = Nothing
        Me.txtDescription.ReferenceTableName = Nothing
        Me.txtDescription.Size = New System.Drawing.Size(365, 45)
        Me.txtDescription.TabIndex = 4
        '
        'lblRemarks
        '
        Me.lblRemarks.FieldName = Nothing
        Me.lblRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRemarks.Location = New System.Drawing.Point(372, 48)
        Me.lblRemarks.Name = "lblRemarks"
        Me.lblRemarks.Size = New System.Drawing.Size(51, 16)
        Me.lblRemarks.TabIndex = 177
        Me.lblRemarks.Text = "Remarks"
        '
        'lblEnteredBy
        '
        Me.lblEnteredBy.FieldName = Nothing
        Me.lblEnteredBy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEnteredBy.Location = New System.Drawing.Point(12, 72)
        Me.lblEnteredBy.Name = "lblEnteredBy"
        Me.lblEnteredBy.Size = New System.Drawing.Size(62, 16)
        Me.lblEnteredBy.TabIndex = 165
        Me.lblEnteredBy.Text = "Entered By"
        '
        'findEnteredBy
        '
        Me.findEnteredBy.CalculationExpression = Nothing
        Me.findEnteredBy.FieldCode = Nothing
        Me.findEnteredBy.FieldDesc = Nothing
        Me.findEnteredBy.FieldMaxLength = 0
        Me.findEnteredBy.FieldName = Nothing
        Me.findEnteredBy.isCalculatedField = False
        Me.findEnteredBy.IsSourceFromTable = False
        Me.findEnteredBy.IsSourceFromValueList = False
        Me.findEnteredBy.IsUnique = False
        Me.findEnteredBy.Location = New System.Drawing.Point(133, 72)
        Me.findEnteredBy.MendatroryField = True
        Me.findEnteredBy.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.findEnteredBy.MyLinkLable1 = Me.lblEnteredBy
        Me.findEnteredBy.MyLinkLable2 = Nothing
        Me.findEnteredBy.MyReadOnly = False
        Me.findEnteredBy.MyShowMasterFormButton = False
        Me.findEnteredBy.Name = "findEnteredBy"
        Me.findEnteredBy.ReferenceFieldDesc = Nothing
        Me.findEnteredBy.ReferenceFieldName = Nothing
        Me.findEnteredBy.ReferenceTableName = Nothing
        Me.findEnteredBy.Size = New System.Drawing.Size(221, 19)
        Me.findEnteredBy.TabIndex = 2
        Me.findEnteredBy.Value = ""
        '
        'lblLeaveDate
        '
        Me.lblLeaveDate.FieldName = Nothing
        Me.lblLeaveDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLeaveDate.Location = New System.Drawing.Point(12, 48)
        Me.lblLeaveDate.Name = "lblLeaveDate"
        Me.lblLeaveDate.Size = New System.Drawing.Size(64, 16)
        Me.lblLeaveDate.TabIndex = 164
        Me.lblLeaveDate.Text = "Leave Date"
        '
        'dtpLeaveDate
        '
        Me.dtpLeaveDate.CalculationExpression = Nothing
        Me.dtpLeaveDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpLeaveDate.FieldCode = Nothing
        Me.dtpLeaveDate.FieldDesc = Nothing
        Me.dtpLeaveDate.FieldMaxLength = 0
        Me.dtpLeaveDate.FieldName = Nothing
        Me.dtpLeaveDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpLeaveDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpLeaveDate.isCalculatedField = False
        Me.dtpLeaveDate.IsSourceFromTable = False
        Me.dtpLeaveDate.IsSourceFromValueList = False
        Me.dtpLeaveDate.IsUnique = False
        Me.dtpLeaveDate.Location = New System.Drawing.Point(133, 46)
        Me.dtpLeaveDate.MendatroryField = True
        Me.dtpLeaveDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpLeaveDate.MyLinkLable1 = Me.lblLeaveDate
        Me.dtpLeaveDate.MyLinkLable2 = Nothing
        Me.dtpLeaveDate.Name = "dtpLeaveDate"
        Me.dtpLeaveDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpLeaveDate.ReferenceFieldDesc = Nothing
        Me.dtpLeaveDate.ReferenceFieldName = Nothing
        Me.dtpLeaveDate.ReferenceTableName = Nothing
        Me.dtpLeaveDate.Size = New System.Drawing.Size(130, 18)
        Me.dtpLeaveDate.TabIndex = 1
        Me.dtpLeaveDate.TabStop = False
        Me.dtpLeaveDate.Text = "28/06/2013"
        Me.dtpLeaveDate.Value = New Date(2013, 6, 28, 0, 0, 0, 0)
        '
        'findLeaveRegisterCode
        '
        Me.findLeaveRegisterCode.FieldName = Nothing
        Me.findLeaveRegisterCode.Location = New System.Drawing.Point(133, 19)
        Me.findLeaveRegisterCode.MendatroryField = True
        Me.findLeaveRegisterCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.findLeaveRegisterCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.findLeaveRegisterCode.MyLinkLable1 = Me.lblLeaveRegisterCode
        Me.findLeaveRegisterCode.MyLinkLable2 = Nothing
        Me.findLeaveRegisterCode.MyMaxLength = 12
        Me.findLeaveRegisterCode.MyReadOnly = False
        Me.findLeaveRegisterCode.Name = "findLeaveRegisterCode"
        Me.findLeaveRegisterCode.Size = New System.Drawing.Size(221, 21)
        Me.findLeaveRegisterCode.TabIndex = 0
        Me.findLeaveRegisterCode.Value = ""
        '
        'lblLeaveRegisterCode
        '
        Me.lblLeaveRegisterCode.FieldName = Nothing
        Me.lblLeaveRegisterCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLeaveRegisterCode.Location = New System.Drawing.Point(12, 24)
        Me.lblLeaveRegisterCode.Name = "lblLeaveRegisterCode"
        Me.lblLeaveRegisterCode.Size = New System.Drawing.Size(113, 16)
        Me.lblLeaveRegisterCode.TabIndex = 161
        Me.lblLeaveRegisterCode.Text = "Leave Register Code"
        '
        'gvLeaveRegister
        '
        Me.gvLeaveRegister.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gvLeaveRegister.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvLeaveRegister.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvLeaveRegister.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvLeaveRegister.ForeColor = System.Drawing.Color.Black
        Me.gvLeaveRegister.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvLeaveRegister.Location = New System.Drawing.Point(9, 97)
        '
        '
        '
        Me.gvLeaveRegister.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvLeaveRegister.MasterTemplate.AllowAddNewRow = False
        Me.gvLeaveRegister.MasterTemplate.AllowEditRow = False
        Me.gvLeaveRegister.MasterTemplate.AutoGenerateColumns = False
        Me.gvLeaveRegister.MasterTemplate.EnableGrouping = False
        Me.gvLeaveRegister.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvLeaveRegister.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvLeaveRegister.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gvLeaveRegister.MyStopExport = False
        Me.gvLeaveRegister.Name = "gvLeaveRegister"
        Me.gvLeaveRegister.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvLeaveRegister.ShowHeaderCellButtons = True
        Me.gvLeaveRegister.Size = New System.Drawing.Size(824, 341)
        Me.gvLeaveRegister.TabIndex = 5
        '
        'ddlFromPayPeriod
        '
        Me.ddlFromPayPeriod.CalculationExpression = Nothing
        Me.ddlFromPayPeriod.DropDownAnimationEnabled = True
        Me.ddlFromPayPeriod.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlFromPayPeriod.FieldCode = Nothing
        Me.ddlFromPayPeriod.FieldDesc = Nothing
        Me.ddlFromPayPeriod.FieldMaxLength = 0
        Me.ddlFromPayPeriod.FieldName = Nothing
        Me.ddlFromPayPeriod.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddlFromPayPeriod.isCalculatedField = False
        Me.ddlFromPayPeriod.IsSourceFromTable = False
        Me.ddlFromPayPeriod.IsSourceFromValueList = False
        Me.ddlFromPayPeriod.IsUnique = False
        RadListDataItem3.Text = "Yes"
        RadListDataItem4.Text = "No"
        Me.ddlFromPayPeriod.Items.Add(RadListDataItem3)
        Me.ddlFromPayPeriod.Items.Add(RadListDataItem4)
        Me.ddlFromPayPeriod.Location = New System.Drawing.Point(468, 19)
        Me.ddlFromPayPeriod.MendatroryField = True
        Me.ddlFromPayPeriod.MyLinkLable1 = Me.lblPayPeriod
        Me.ddlFromPayPeriod.MyLinkLable2 = Nothing
        Me.ddlFromPayPeriod.Name = "ddlFromPayPeriod"
        Me.ddlFromPayPeriod.ReferenceFieldDesc = Nothing
        Me.ddlFromPayPeriod.ReferenceFieldName = Nothing
        Me.ddlFromPayPeriod.ReferenceTableName = Nothing
        Me.ddlFromPayPeriod.Size = New System.Drawing.Size(261, 18)
        Me.ddlFromPayPeriod.TabIndex = 3
        '
        'lblPayPeriod
        '
        Me.lblPayPeriod.FieldName = Nothing
        Me.lblPayPeriod.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPayPeriod.Location = New System.Drawing.Point(372, 21)
        Me.lblPayPeriod.Name = "lblPayPeriod"
        Me.lblPayPeriod.Size = New System.Drawing.Size(62, 16)
        Me.lblPayPeriod.TabIndex = 153
        Me.lblPayPeriod.Text = "Pay Period"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(78, 19)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(66, 18)
        Me.btnPost.TabIndex = 3
        Me.btnPost.Text = "Post"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(9, 19)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(767, 19)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(147, 19)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'frmLeaveRegister
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(862, 522)
        Me.Controls.Add(Me.RadGroupBox3)
        Me.Name = "frmLeaveRegister"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Leave Register"
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEnteredBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLeaveDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpLeaveDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLeaveRegisterCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvLeaveRegister.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvLeaveRegister, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlFromPayPeriod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPayPeriod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents lblEnteredBy As common.Controls.MyLabel
    Friend WithEvents findEnteredBy As common.UserControls.txtFinder
    Friend WithEvents lblLeaveDate As common.Controls.MyLabel
    Friend WithEvents dtpLeaveDate As common.Controls.MyDateTimePicker
    Friend WithEvents findLeaveRegisterCode As common.UserControls.txtNavigator
    Friend WithEvents lblLeaveRegisterCode As common.Controls.MyLabel
    Friend WithEvents gvLeaveRegister As common.UserControls.MyRadGridView
    Friend WithEvents ddlFromPayPeriod As common.Controls.MyComboBox
    Friend WithEvents lblPayPeriod As common.Controls.MyLabel
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
End Class
