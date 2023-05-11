<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSalesmanTarget
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
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.txtAmount = New common.MyNumBox
        Me.lblSalesmanName = New common.Controls.MyLabel
        Me.lblAmount = New common.Controls.MyLabel
        Me.txtSalesmanCode = New common.UserControls.txtFinder
        Me.lblSalesman = New common.Controls.MyLabel
        Me.lblTragetNo = New common.Controls.MyLabel
        Me.ddlType = New common.Controls.MyComboBox
        Me.RadLabel29 = New common.Controls.MyLabel
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton
        Me.txtTargetNo = New common.UserControls.txtNavigator
        Me.rdlbltransferdate = New common.Controls.MyLabel
        Me.dtpTargetDate = New common.Controls.MyDateTimePicker
        Me.dgvItem = New common.UserControls.MyRadGridView
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem
        Me.btnSave = New Telerik.WinControls.UI.RadButton
        Me.btnDelete = New Telerik.WinControls.UI.RadButton
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.txtAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSalesmanName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSalesman, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTragetNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel29, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlbltransferdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpTargetDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvItem.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(701, 20)
        Me.RadMenu1.TabIndex = 3
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Setting"
        Me.RadMenuItem3.AccessibleName = "Setting"
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        Me.RadMenuItem3.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Save Layout"
        Me.RadMenuItem1.AccessibleName = "Save Layout"
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Save Layout"
        Me.RadMenuItem1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtAmount)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblSalesmanName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblAmount)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtSalesmanCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblTragetNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ddlType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnAddNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblSalesman)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtTargetNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel29)
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdlbltransferdate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpTargetDate)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.dgvItem)
        Me.SplitContainer1.Size = New System.Drawing.Size(701, 374)
        Me.SplitContainer1.SplitterDistance = 86
        Me.SplitContainer1.TabIndex = 0
        '
        'txtAmount
        '
        Me.txtAmount.BackColor = System.Drawing.Color.White
        Me.txtAmount.DecimalPlaces = 2
        Me.txtAmount.Location = New System.Drawing.Point(85, 55)
        Me.txtAmount.MendatroryField = False
        Me.txtAmount.MyLinkLable1 = Nothing
        Me.txtAmount.MyLinkLable2 = Nothing
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Size = New System.Drawing.Size(154, 20)
        Me.txtAmount.TabIndex = 6
        Me.txtAmount.Text = "0"
        Me.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtAmount.Value = 0
        '
        'lblSalesmanName
        '
        Me.lblSalesmanName.AutoSize = False
        Me.lblSalesmanName.BorderVisible = True
        Me.lblSalesmanName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSalesmanName.Location = New System.Drawing.Point(248, 35)
        Me.lblSalesmanName.Name = "lblSalesmanName"
        Me.lblSalesmanName.Size = New System.Drawing.Size(446, 18)
        Me.lblSalesmanName.TabIndex = 5
        Me.lblSalesmanName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSalesmanName.TextWrap = False
        '
        'lblAmount
        '
        Me.lblAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmount.Location = New System.Drawing.Point(6, 57)
        Me.lblAmount.Name = "lblAmount"
        Me.lblAmount.Size = New System.Drawing.Size(45, 16)
        Me.lblAmount.TabIndex = 11
        Me.lblAmount.Text = "Amount"
        '
        'txtSalesmanCode
        '
        Me.txtSalesmanCode.Location = New System.Drawing.Point(85, 34)
        Me.txtSalesmanCode.MendatroryField = True
        Me.txtSalesmanCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalesmanCode.MyLinkLable1 = Me.lblSalesman
        Me.txtSalesmanCode.MyLinkLable2 = Nothing
        Me.txtSalesmanCode.MyReadOnly = False
        Me.txtSalesmanCode.Name = "txtSalesmanCode"
        Me.txtSalesmanCode.Size = New System.Drawing.Size(154, 19)
        Me.txtSalesmanCode.TabIndex = 4
        Me.txtSalesmanCode.Value = ""
        '
        'lblSalesman
        '
        Me.lblSalesman.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSalesman.Location = New System.Drawing.Point(6, 35)
        Me.lblSalesman.Name = "lblSalesman"
        Me.lblSalesman.Size = New System.Drawing.Size(57, 16)
        Me.lblSalesman.TabIndex = 10
        Me.lblSalesman.Text = "Salesman"
        '
        'lblTragetNo
        '
        Me.lblTragetNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTragetNo.Location = New System.Drawing.Point(6, 13)
        Me.lblTragetNo.Name = "lblTragetNo"
        Me.lblTragetNo.Size = New System.Drawing.Size(57, 16)
        Me.lblTragetNo.TabIndex = 9
        Me.lblTragetNo.Text = "Target No"
        '
        'ddlType
        '
        Me.ddlType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlType.Location = New System.Drawing.Point(576, 10)
        Me.ddlType.MendatroryField = True
        Me.ddlType.MyLinkLable1 = Me.RadLabel29
        Me.ddlType.MyLinkLable2 = Nothing
        Me.ddlType.Name = "ddlType"
        Me.ddlType.Size = New System.Drawing.Size(118, 20)
        Me.ddlType.TabIndex = 3
        '
        'RadLabel29
        '
        Me.RadLabel29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel29.Location = New System.Drawing.Point(536, 12)
        Me.RadLabel29.Name = "RadLabel29"
        Me.RadLabel29.Size = New System.Drawing.Size(31, 16)
        Me.RadLabel29.TabIndex = 11
        Me.RadLabel29.Text = "Type"
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(327, 10)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 21)
        Me.btnAddNew.TabIndex = 1
        '
        'txtTargetNo
        '
        Me.txtTargetNo.Location = New System.Drawing.Point(85, 10)
        Me.txtTargetNo.MendatroryField = False
        Me.txtTargetNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtTargetNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtTargetNo.MyLinkLable1 = Me.lblTragetNo
        Me.txtTargetNo.MyLinkLable2 = Nothing
        Me.txtTargetNo.MyMaxLength = 32767
        Me.txtTargetNo.MyReadOnly = False
        Me.txtTargetNo.Name = "txtTargetNo"
        Me.txtTargetNo.Size = New System.Drawing.Size(237, 20)
        Me.txtTargetNo.TabIndex = 0
        Me.txtTargetNo.TabStop = False
        Me.txtTargetNo.Value = ""
        '
        'rdlbltransferdate
        '
        Me.rdlbltransferdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdlbltransferdate.Location = New System.Drawing.Point(365, 12)
        Me.rdlbltransferdate.Name = "rdlbltransferdate"
        Me.rdlbltransferdate.Size = New System.Drawing.Size(65, 16)
        Me.rdlbltransferdate.TabIndex = 12
        Me.rdlbltransferdate.Text = "Month/Year"
        '
        'dtpTargetDate
        '
        Me.dtpTargetDate.CustomFormat = "MMM/yyyy"
        Me.dtpTargetDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTargetDate.Location = New System.Drawing.Point(441, 10)
        Me.dtpTargetDate.MendatroryField = False
        Me.dtpTargetDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpTargetDate.MyLinkLable1 = Me.rdlbltransferdate
        Me.dtpTargetDate.MyLinkLable2 = Nothing
        Me.dtpTargetDate.Name = "dtpTargetDate"
        Me.dtpTargetDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpTargetDate.Size = New System.Drawing.Size(79, 20)
        Me.dtpTargetDate.TabIndex = 2
        Me.dtpTargetDate.TabStop = False
        Me.dtpTargetDate.Text = "Jun/2011"
        Me.dtpTargetDate.Value = New Date(2011, 6, 27, 0, 0, 0, 0)
        '
        'dgvItem
        '
        Me.dgvItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.dgvItem.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgvItem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvItem.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.dgvItem.ForeColor = System.Drawing.Color.Black
        Me.dgvItem.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dgvItem.Location = New System.Drawing.Point(0, 0)
        '
        'dgvItem
        '
        Me.dgvItem.MasterTemplate.AllowAddNewRow = False
        Me.dgvItem.MasterTemplate.AllowDeleteRow = False
        Me.dgvItem.MasterTemplate.EnableFiltering = True
        Me.dgvItem.Name = "dgvItem"
        Me.dgvItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.dgvItem.ShowGroupPanel = False
        Me.dgvItem.Size = New System.Drawing.Size(701, 284)
        Me.dgvItem.TabIndex = 0
        Me.dgvItem.TabStop = False
        Me.dgvItem.Text = "RadGridView1"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Setting"
        Me.RadMenuItem2.AccessibleName = "Setting"
        Me.RadMenuItem2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItem4})
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Setting"
        Me.RadMenuItem2.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.AccessibleDescription = "Delete Layout"
        Me.RadMenuItem4.AccessibleName = "Delete Layout"
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Delete Layout"
        Me.RadMenuItem4.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(6, 6)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(79, 6)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 22)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.btnClose)
        Me.RadGroupBox1.Controls.Add(Me.btnDelete)
        Me.RadGroupBox1.Controls.Add(Me.btnSave)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 394)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(701, 34)
        Me.RadGroupBox1.TabIndex = 1
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(625, 6)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'FrmSalesmanTarget
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(701, 428)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadGroupBox1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmSalesmanTarget"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Salesman Target"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.txtAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSalesmanName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSalesman, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTragetNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel29, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlbltransferdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpTargetDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvItem.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblAmount As common.Controls.MyLabel
    Friend WithEvents txtSalesmanCode As common.UserControls.txtFinder
    Friend WithEvents lblSalesman As common.Controls.MyLabel
    Friend WithEvents lblTragetNo As common.Controls.MyLabel
    Friend WithEvents ddlType As common.Controls.MyComboBox
    Friend WithEvents RadLabel29 As common.Controls.MyLabel
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtTargetNo As common.UserControls.txtNavigator
    Friend WithEvents rdlbltransferdate As common.Controls.MyLabel
    Friend WithEvents dtpTargetDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem4 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents dgvItem As common.UserControls.MyRadGridView
    Friend WithEvents lblSalesmanName As common.Controls.MyLabel
    Friend WithEvents txtAmount As common.MyNumBox
End Class

