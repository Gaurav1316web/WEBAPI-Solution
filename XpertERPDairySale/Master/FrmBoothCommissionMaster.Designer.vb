<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmBoothCommissionMaster
    Inherits FrmMainTranScreen

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.txtComment = New common.Controls.MyTextBox()
        Me.lblComment = New System.Windows.Forms.Label()
        Me.txtRemark = New common.Controls.MyTextBox()
        Me.lblRemark = New System.Windows.Forms.Label()
        Me.txtperDayQty = New System.Windows.Forms.TextBox()
        Me.lblMinPerDayQty = New common.Controls.MyLabel()
        Me.txtUOM = New common.UserControls.txtFinder()
        Me.lblCommissionUOM = New common.Controls.MyLabel()
        Me.chkMobileUser = New Telerik.WinControls.UI.RadCheckBox()
        Me.UsLock1 = New common.usLock()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.txtEndDate = New common.Controls.MyDateTimePicker()
        Me.txtStartDate = New common.Controls.MyDateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.lblDocCode = New common.Controls.MyLabel()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.btnpost = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.txtComment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemark, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMinPerDayQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCommissionUOM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMobileUser, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEndDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtStartDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnpost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnpost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(974, 530)
        Me.SplitContainer1.SplitterDistance = 492
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtComment)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblComment)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtRemark)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblRemark)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtperDayQty)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblMinPerDayQty)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtUOM)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblCommissionUOM)
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkMobileUser)
        Me.SplitContainer2.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadButton1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtEndDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtStartDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label4)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnAddNew)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDocNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblDocCode)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gv1)
        Me.SplitContainer2.Size = New System.Drawing.Size(974, 492)
        Me.SplitContainer2.SplitterDistance = 177
        Me.SplitContainer2.TabIndex = 177
        '
        'txtComment
        '
        Me.txtComment.CalculationExpression = Nothing
        Me.txtComment.FieldCode = Nothing
        Me.txtComment.FieldDesc = Nothing
        Me.txtComment.FieldMaxLength = 0
        Me.txtComment.FieldName = Nothing
        Me.txtComment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtComment.isCalculatedField = False
        Me.txtComment.IsSourceFromTable = False
        Me.txtComment.IsSourceFromValueList = False
        Me.txtComment.IsUnique = False
        Me.txtComment.Location = New System.Drawing.Point(117, 129)
        Me.txtComment.MaxLength = 200
        Me.txtComment.MendatroryField = False
        Me.txtComment.Multiline = True
        Me.txtComment.MyLinkLable1 = Nothing
        Me.txtComment.MyLinkLable2 = Nothing
        Me.txtComment.Name = "txtComment"
        Me.txtComment.ReferenceFieldDesc = Nothing
        Me.txtComment.ReferenceFieldName = Nothing
        Me.txtComment.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtComment.RootElement.StretchVertically = True
        Me.txtComment.Size = New System.Drawing.Size(254, 35)
        Me.txtComment.TabIndex = 1535
        '
        'lblComment
        '
        Me.lblComment.AutoSize = True
        Me.lblComment.Location = New System.Drawing.Point(7, 133)
        Me.lblComment.Name = "lblComment"
        Me.lblComment.Size = New System.Drawing.Size(56, 13)
        Me.lblComment.TabIndex = 1534
        Me.lblComment.Text = "Comment"
        '
        'txtRemark
        '
        Me.txtRemark.CalculationExpression = Nothing
        Me.txtRemark.FieldCode = Nothing
        Me.txtRemark.FieldDesc = Nothing
        Me.txtRemark.FieldMaxLength = 0
        Me.txtRemark.FieldName = Nothing
        Me.txtRemark.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemark.isCalculatedField = False
        Me.txtRemark.IsSourceFromTable = False
        Me.txtRemark.IsSourceFromValueList = False
        Me.txtRemark.IsUnique = False
        Me.txtRemark.Location = New System.Drawing.Point(117, 88)
        Me.txtRemark.MaxLength = 200
        Me.txtRemark.MendatroryField = False
        Me.txtRemark.Multiline = True
        Me.txtRemark.MyLinkLable1 = Nothing
        Me.txtRemark.MyLinkLable2 = Nothing
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.ReferenceFieldDesc = Nothing
        Me.txtRemark.ReferenceFieldName = Nothing
        Me.txtRemark.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtRemark.RootElement.StretchVertically = True
        Me.txtRemark.Size = New System.Drawing.Size(254, 35)
        Me.txtRemark.TabIndex = 1533
        '
        'lblRemark
        '
        Me.lblRemark.AutoSize = True
        Me.lblRemark.Location = New System.Drawing.Point(7, 92)
        Me.lblRemark.Name = "lblRemark"
        Me.lblRemark.Size = New System.Drawing.Size(45, 13)
        Me.lblRemark.TabIndex = 1532
        Me.lblRemark.Text = "Remark"
        '
        'txtperDayQty
        '
        Me.txtperDayQty.Location = New System.Drawing.Point(117, 65)
        Me.txtperDayQty.Name = "txtperDayQty"
        Me.txtperDayQty.Size = New System.Drawing.Size(95, 20)
        Me.txtperDayQty.TabIndex = 1531
        '
        'lblMinPerDayQty
        '
        Me.lblMinPerDayQty.FieldName = Nothing
        Me.lblMinPerDayQty.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMinPerDayQty.Location = New System.Drawing.Point(10, 65)
        Me.lblMinPerDayQty.Name = "lblMinPerDayQty"
        Me.lblMinPerDayQty.Size = New System.Drawing.Size(89, 16)
        Me.lblMinPerDayQty.TabIndex = 1530
        Me.lblMinPerDayQty.Text = "Min Per Day Qty"
        '
        'txtUOM
        '
        Me.txtUOM.CalculationExpression = Nothing
        Me.txtUOM.FieldCode = Nothing
        Me.txtUOM.FieldDesc = Nothing
        Me.txtUOM.FieldMaxLength = 0
        Me.txtUOM.FieldName = Nothing
        Me.txtUOM.isCalculatedField = False
        Me.txtUOM.IsSourceFromTable = False
        Me.txtUOM.IsSourceFromValueList = False
        Me.txtUOM.IsUnique = False
        Me.txtUOM.Location = New System.Drawing.Point(117, 40)
        Me.txtUOM.MendatroryField = False
        Me.txtUOM.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUOM.MyLinkLable1 = Nothing
        Me.txtUOM.MyLinkLable2 = Nothing
        Me.txtUOM.MyReadOnly = False
        Me.txtUOM.MyShowMasterFormButton = False
        Me.txtUOM.Name = "txtUOM"
        Me.txtUOM.ReferenceFieldDesc = Nothing
        Me.txtUOM.ReferenceFieldName = Nothing
        Me.txtUOM.ReferenceTableName = Nothing
        Me.txtUOM.Size = New System.Drawing.Size(248, 19)
        Me.txtUOM.TabIndex = 1529
        Me.txtUOM.Value = ""
        '
        'lblCommissionUOM
        '
        Me.lblCommissionUOM.FieldName = Nothing
        Me.lblCommissionUOM.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCommissionUOM.Location = New System.Drawing.Point(10, 41)
        Me.lblCommissionUOM.Name = "lblCommissionUOM"
        Me.lblCommissionUOM.Size = New System.Drawing.Size(106, 16)
        Me.lblCommissionUOM.TabIndex = 1528
        Me.lblCommissionUOM.Text = "Commission (UOM)"
        '
        'chkMobileUser
        '
        Me.chkMobileUser.Location = New System.Drawing.Point(377, 85)
        Me.chkMobileUser.Name = "chkMobileUser"
        Me.chkMobileUser.Size = New System.Drawing.Size(126, 18)
        Me.chkMobileUser.TabIndex = 1527
        Me.chkMobileUser.Text = "Only For Mobile User"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(429, 11)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(92, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 186
        '
        'RadButton1
        '
        Me.RadButton1.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.RadButton1.Location = New System.Drawing.Point(353, 12)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(20, 21)
        Me.RadButton1.TabIndex = 184
        Me.RadButton1.Text = "CC"
        '
        'txtEndDate
        '
        Me.txtEndDate.CalculationExpression = Nothing
        Me.txtEndDate.CustomFormat = "dd/MM/yyyy"
        Me.txtEndDate.FieldCode = Nothing
        Me.txtEndDate.FieldDesc = Nothing
        Me.txtEndDate.FieldMaxLength = 0
        Me.txtEndDate.FieldName = Nothing
        Me.txtEndDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtEndDate.isCalculatedField = False
        Me.txtEndDate.IsSourceFromTable = False
        Me.txtEndDate.IsSourceFromValueList = False
        Me.txtEndDate.IsUnique = False
        Me.txtEndDate.Location = New System.Drawing.Point(429, 61)
        Me.txtEndDate.MendatroryField = False
        Me.txtEndDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtEndDate.MyLinkLable1 = Nothing
        Me.txtEndDate.MyLinkLable2 = Nothing
        Me.txtEndDate.Name = "txtEndDate"
        Me.txtEndDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtEndDate.ReferenceFieldDesc = Nothing
        Me.txtEndDate.ReferenceFieldName = Nothing
        Me.txtEndDate.ReferenceTableName = Nothing
        Me.txtEndDate.ShowCheckBox = True
        Me.txtEndDate.Size = New System.Drawing.Size(92, 18)
        Me.txtEndDate.TabIndex = 183
        Me.txtEndDate.TabStop = False
        Me.txtEndDate.Text = "13/06/2011"
        Me.txtEndDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'txtStartDate
        '
        Me.txtStartDate.CalculationExpression = Nothing
        Me.txtStartDate.CustomFormat = "dd/MM/yyyy"
        Me.txtStartDate.FieldCode = Nothing
        Me.txtStartDate.FieldDesc = Nothing
        Me.txtStartDate.FieldMaxLength = 0
        Me.txtStartDate.FieldName = Nothing
        Me.txtStartDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtStartDate.isCalculatedField = False
        Me.txtStartDate.IsSourceFromTable = False
        Me.txtStartDate.IsSourceFromValueList = False
        Me.txtStartDate.IsUnique = False
        Me.txtStartDate.Location = New System.Drawing.Point(433, 37)
        Me.txtStartDate.MendatroryField = False
        Me.txtStartDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtStartDate.MyLinkLable1 = Nothing
        Me.txtStartDate.MyLinkLable2 = Nothing
        Me.txtStartDate.Name = "txtStartDate"
        Me.txtStartDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtStartDate.ReferenceFieldDesc = Nothing
        Me.txtStartDate.ReferenceFieldName = Nothing
        Me.txtStartDate.ReferenceTableName = Nothing
        Me.txtStartDate.Size = New System.Drawing.Size(87, 18)
        Me.txtStartDate.TabIndex = 180
        Me.txtStartDate.TabStop = False
        Me.txtStartDate.Text = "13/06/2011"
        Me.txtStartDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(373, 64)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(54, 13)
        Me.Label4.TabIndex = 182
        Me.Label4.Text = "End Date"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(374, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 13)
        Me.Label2.TabIndex = 181
        Me.Label2.Text = "Start Date"
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.XpertERPDairySale.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(333, 12)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 21)
        Me.btnAddNew.TabIndex = 179
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(105, 13)
        Me.txtDocNo.MendatroryField = False
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Nothing
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 30
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(228, 19)
        Me.txtDocNo.TabIndex = 178
        Me.txtDocNo.TabStop = False
        Me.txtDocNo.Value = ""
        '
        'lblDocCode
        '
        Me.lblDocCode.FieldName = Nothing
        Me.lblDocCode.Location = New System.Drawing.Point(10, 15)
        Me.lblDocCode.Name = "lblDocCode"
        Me.lblDocCode.Size = New System.Drawing.Size(88, 18)
        Me.lblDocCode.TabIndex = 177
        Me.lblDocCode.Text = "Document Code"
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gv1.MyExportAPI = False
        Me.gv1.MyExportFilePath = ""
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(974, 311)
        Me.gv1.TabIndex = 5
        Me.gv1.TabStop = False
        Me.gv1.VarID = ""
        '
        'btnpost
        '
        Me.btnpost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnpost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnpost.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnpost.Location = New System.Drawing.Point(168, 7)
        Me.btnpost.Name = "btnpost"
        Me.btnpost.Size = New System.Drawing.Size(73, 20)
        Me.btnpost.TabIndex = 9
        Me.btnpost.Text = "Post"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(889, 7)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(73, 20)
        Me.btnclose.TabIndex = 8
        Me.btnclose.Text = "Close"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(10, 7)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(73, 20)
        Me.btnsave.TabIndex = 7
        Me.btnsave.Text = "Save"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(89, 7)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(73, 20)
        Me.btndelete.TabIndex = 6
        Me.btndelete.Text = "Delete"
        '
        'FrmBoothCommissionMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(974, 530)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmBoothCommissionMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Booth Commission Master"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.txtComment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemark, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMinPerDayQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCommissionUOM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMobileUser, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEndDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtStartDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnpost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents RadButton1 As RadButton
    Friend WithEvents txtEndDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtStartDate As common.Controls.MyDateTimePicker
    Friend WithEvents Label4 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents btnAddNew As RadButton
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents lblDocCode As common.Controls.MyLabel
    Friend WithEvents chkMobileUser As RadCheckBox
    Friend WithEvents txtUOM As common.UserControls.txtFinder
    Friend WithEvents lblCommissionUOM As common.Controls.MyLabel
    Friend WithEvents lblMinPerDayQty As common.Controls.MyLabel
    Friend WithEvents txtperDayQty As TextBox
    Friend WithEvents txtRemark As common.Controls.MyTextBox
    Friend WithEvents lblRemark As Label
    Friend WithEvents txtComment As common.Controls.MyTextBox
    Friend WithEvents lblComment As Label
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnpost As RadButton
    Friend WithEvents btnclose As RadButton
    Friend WithEvents btnsave As RadButton
    Friend WithEvents btndelete As RadButton
End Class
