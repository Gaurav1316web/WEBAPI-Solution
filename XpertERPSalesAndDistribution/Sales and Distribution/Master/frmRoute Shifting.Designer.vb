<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmRoute_Shifting
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
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim GridViewTextBoxColumn1 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn2 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn3 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn4 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn5 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewCheckBoxColumn1 As Telerik.WinControls.UI.GridViewCheckBoxColumn = New Telerik.WinControls.UI.GridViewCheckBoxColumn()
        Dim GridViewTextBoxColumn6 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.lblRoutId = New common.Controls.MyLabel()
        Me.txtRouteDesc = New common.Controls.MyTextBox()
        Me.lblrutedesc = New common.Controls.MyLabel()
        Me.GrpBoxRouteDetail = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtNewRouteId = New common.UserControls.txtNavigator()
        Me.lblNewRouteId = New common.Controls.MyLabel()
        Me.txtRouteId = New common.UserControls.txtNavigator()
        Me.dtpStartDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.ddlRouteGroup = New Telerik.WinControls.UI.RadDropDownList()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.ddlstatus = New Telerik.WinControls.UI.RadDropDownList()
        Me.lblstatus = New common.Controls.MyLabel()
        Me.btnreset = New Telerik.WinControls.UI.RadButton()
        Me.GrpBoxDetail = New Telerik.WinControls.UI.RadGroupBox()
        Me.MasterTemplate = New common.UserControls.MyRadGridView()
        Me.lblNewRouteDesc = New common.Controls.MyLabel()
        Me.txtNewDesc = New common.Controls.MyTextBox()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        CType(Me.lblRoutId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRouteDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblrutedesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpBoxRouteDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpBoxRouteDetail.SuspendLayout()
        CType(Me.lblNewRouteId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpStartDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlRouteGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlstatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblstatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpBoxDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpBoxDetail.SuspendLayout()
        CType(Me.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MasterTemplate.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNewRouteDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNewDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblRoutId
        '
        Me.lblRoutId.FieldName = Nothing
        Me.lblRoutId.Location = New System.Drawing.Point(13, 33)
        Me.lblRoutId.Name = "lblRoutId"
        Me.lblRoutId.Size = New System.Drawing.Size(49, 18)
        Me.lblRoutId.TabIndex = 10
        Me.lblRoutId.Text = "Route Id"
        '
        'txtRouteDesc
        '
        Me.txtRouteDesc.CalculationExpression = Nothing
        Me.txtRouteDesc.FieldCode = Nothing
        Me.txtRouteDesc.FieldDesc = Nothing
        Me.txtRouteDesc.FieldMaxLength = 0
        Me.txtRouteDesc.FieldName = Nothing
        Me.txtRouteDesc.isCalculatedField = False
        Me.txtRouteDesc.IsSourceFromTable = False
        Me.txtRouteDesc.IsSourceFromValueList = False
        Me.txtRouteDesc.IsUnique = False
        Me.txtRouteDesc.Location = New System.Drawing.Point(423, 33)
        Me.txtRouteDesc.MendatroryField = False
        Me.txtRouteDesc.MyLinkLable1 = Me.lblrutedesc
        Me.txtRouteDesc.MyLinkLable2 = Nothing
        Me.txtRouteDesc.Name = "txtRouteDesc"
        Me.txtRouteDesc.ReferenceFieldDesc = Nothing
        Me.txtRouteDesc.ReferenceFieldName = Nothing
        Me.txtRouteDesc.ReferenceTableName = Nothing
        Me.txtRouteDesc.Size = New System.Drawing.Size(291, 20)
        Me.txtRouteDesc.TabIndex = 3
        '
        'lblrutedesc
        '
        Me.lblrutedesc.FieldName = Nothing
        Me.lblrutedesc.Location = New System.Drawing.Point(340, 33)
        Me.lblrutedesc.Name = "lblrutedesc"
        Me.lblrutedesc.Size = New System.Drawing.Size(63, 18)
        Me.lblrutedesc.TabIndex = 12
        Me.lblrutedesc.Text = "Route Desc"
        '
        'GrpBoxRouteDetail
        '
        Me.GrpBoxRouteDetail.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.GrpBoxRouteDetail.Controls.Add(Me.txtNewRouteId)
        Me.GrpBoxRouteDetail.Controls.Add(Me.txtRouteId)
        Me.GrpBoxRouteDetail.Controls.Add(Me.dtpStartDate)
        Me.GrpBoxRouteDetail.Controls.Add(Me.ddlRouteGroup)
        Me.GrpBoxRouteDetail.Controls.Add(Me.RadLabel2)
        Me.GrpBoxRouteDetail.Controls.Add(Me.RadLabel1)
        Me.GrpBoxRouteDetail.Controls.Add(Me.ddlstatus)
        Me.GrpBoxRouteDetail.Controls.Add(Me.lblstatus)
        Me.GrpBoxRouteDetail.Controls.Add(Me.btnreset)
        Me.GrpBoxRouteDetail.Controls.Add(Me.GrpBoxDetail)
        Me.GrpBoxRouteDetail.Controls.Add(Me.lblNewRouteDesc)
        Me.GrpBoxRouteDetail.Controls.Add(Me.txtNewDesc)
        Me.GrpBoxRouteDetail.Controls.Add(Me.lblNewRouteId)
        Me.GrpBoxRouteDetail.Controls.Add(Me.lblrutedesc)
        Me.GrpBoxRouteDetail.Controls.Add(Me.lblRoutId)
        Me.GrpBoxRouteDetail.Controls.Add(Me.txtRouteDesc)
        Me.GrpBoxRouteDetail.HeaderText = ""
        Me.GrpBoxRouteDetail.Location = New System.Drawing.Point(3, 3)
        Me.GrpBoxRouteDetail.Name = "GrpBoxRouteDetail"
        Me.GrpBoxRouteDetail.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.GrpBoxRouteDetail.Size = New System.Drawing.Size(727, 420)
        Me.GrpBoxRouteDetail.TabIndex = 0
        '
        'txtNewRouteId
        '
        Me.txtNewRouteId.FieldName = Nothing
        Me.txtNewRouteId.Location = New System.Drawing.Point(90, 60)
        Me.txtNewRouteId.MendatroryField = False
        Me.txtNewRouteId.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNewRouteId.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtNewRouteId.MyLinkLable1 = Me.lblNewRouteId
        Me.txtNewRouteId.MyLinkLable2 = Nothing
        Me.txtNewRouteId.MyMaxLength = 32767
        Me.txtNewRouteId.MyReadOnly = False
        Me.txtNewRouteId.Name = "txtNewRouteId"
        Me.txtNewRouteId.Size = New System.Drawing.Size(212, 21)
        Me.txtNewRouteId.TabIndex = 4
        Me.txtNewRouteId.Value = ""
        '
        'lblNewRouteId
        '
        Me.lblNewRouteId.FieldName = Nothing
        Me.lblNewRouteId.Location = New System.Drawing.Point(13, 61)
        Me.lblNewRouteId.Name = "lblNewRouteId"
        Me.lblNewRouteId.Size = New System.Drawing.Size(74, 18)
        Me.lblNewRouteId.TabIndex = 11
        Me.lblNewRouteId.Text = "New Route Id"
        '
        'txtRouteId
        '
        Me.txtRouteId.FieldName = Nothing
        Me.txtRouteId.Location = New System.Drawing.Point(89, 33)
        Me.txtRouteId.MendatroryField = True
        Me.txtRouteId.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRouteId.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtRouteId.MyLinkLable1 = Me.lblRoutId
        Me.txtRouteId.MyLinkLable2 = Nothing
        Me.txtRouteId.MyMaxLength = 30
        Me.txtRouteId.MyReadOnly = False
        Me.txtRouteId.Name = "txtRouteId"
        Me.txtRouteId.Size = New System.Drawing.Size(213, 21)
        Me.txtRouteId.TabIndex = 2
        Me.txtRouteId.Value = ""
        '
        'dtpStartDate
        '
        Me.dtpStartDate.CalculationExpression = Nothing
        Me.dtpStartDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtpStartDate.FieldCode = Nothing
        Me.dtpStartDate.FieldDesc = Nothing
        Me.dtpStartDate.FieldMaxLength = 0
        Me.dtpStartDate.FieldName = Nothing
        Me.dtpStartDate.isCalculatedField = False
        Me.dtpStartDate.IsSourceFromTable = False
        Me.dtpStartDate.IsSourceFromValueList = False
        Me.dtpStartDate.IsUnique = False
        Me.dtpStartDate.Location = New System.Drawing.Point(300, 397)
        Me.dtpStartDate.MendatroryField = False
        Me.dtpStartDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpStartDate.MyLinkLable1 = Me.RadLabel2
        Me.dtpStartDate.MyLinkLable2 = Nothing
        Me.dtpStartDate.Name = "dtpStartDate"
        Me.dtpStartDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpStartDate.ReferenceFieldDesc = Nothing
        Me.dtpStartDate.ReferenceFieldName = Nothing
        Me.dtpStartDate.ReferenceTableName = Nothing
        Me.dtpStartDate.Size = New System.Drawing.Size(116, 20)
        Me.dtpStartDate.TabIndex = 8
        Me.dtpStartDate.TabStop = False
        Me.dtpStartDate.Text = "Tuesday, March 13, 2012"
        Me.dtpStartDate.Value = New Date(2012, 3, 13, 12, 7, 19, 78)
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Location = New System.Drawing.Point(238, 399)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(56, 18)
        Me.RadLabel2.TabIndex = 15
        Me.RadLabel2.Text = "Start Date"
        '
        'ddlRouteGroup
        '
        Me.ddlRouteGroup.DropDownAnimationEnabled = True
        Me.ddlRouteGroup.Location = New System.Drawing.Point(93, 397)
        Me.ddlRouteGroup.Name = "ddlRouteGroup"
        Me.ddlRouteGroup.Size = New System.Drawing.Size(106, 20)
        Me.ddlRouteGroup.TabIndex = 7
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(13, 399)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(70, 18)
        Me.RadLabel1.TabIndex = 14
        Me.RadLabel1.Text = "Route Group"
        '
        'ddlstatus
        '
        Me.ddlstatus.DropDownAnimationEnabled = True
        Me.ddlstatus.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        RadListDataItem1.Text = "Select"
        RadListDataItem2.Text = "Shift"
        RadListDataItem3.Text = "Close"
        Me.ddlstatus.Items.Add(RadListDataItem1)
        Me.ddlstatus.Items.Add(RadListDataItem2)
        Me.ddlstatus.Items.Add(RadListDataItem3)
        Me.ddlstatus.Location = New System.Drawing.Point(93, 6)
        Me.ddlstatus.Name = "ddlstatus"
        Me.ddlstatus.Size = New System.Drawing.Size(120, 20)
        Me.ddlstatus.TabIndex = 0
        '
        'lblstatus
        '
        Me.lblstatus.FieldName = Nothing
        Me.lblstatus.Location = New System.Drawing.Point(13, 4)
        Me.lblstatus.Name = "lblstatus"
        Me.lblstatus.Size = New System.Drawing.Size(37, 18)
        Me.lblstatus.TabIndex = 9
        Me.lblstatus.Text = "Status"
        '
        'btnreset
        '
        Me.btnreset.Image = Global.XpertERPSalesAndDistribution.My.Resources.Resources._new
        Me.btnreset.Location = New System.Drawing.Point(219, 7)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(16, 18)
        Me.btnreset.TabIndex = 1
        '
        'GrpBoxDetail
        '
        Me.GrpBoxDetail.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.GrpBoxDetail.Controls.Add(Me.MasterTemplate)
        Me.GrpBoxDetail.HeaderText = ""
        Me.GrpBoxDetail.Location = New System.Drawing.Point(13, 89)
        Me.GrpBoxDetail.Name = "GrpBoxDetail"
        Me.GrpBoxDetail.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.GrpBoxDetail.Size = New System.Drawing.Size(706, 298)
        Me.GrpBoxDetail.TabIndex = 6
        '
        'MasterTemplate
        '
        Me.MasterTemplate.AccessibleName = "grdviewRouteDetail"
        Me.MasterTemplate.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.MasterTemplate.Cursor = System.Windows.Forms.Cursors.Default
        Me.MasterTemplate.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MasterTemplate.ForeColor = System.Drawing.Color.Black
        Me.MasterTemplate.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.MasterTemplate.Location = New System.Drawing.Point(13, 10)
        '
        '
        '
        Me.MasterTemplate.MasterTemplate.AllowAddNewRow = False
        GridViewTextBoxColumn1.HeaderText = "Customer"
        GridViewTextBoxColumn1.Name = "Customer"
        GridViewTextBoxColumn1.ReadOnly = True
        GridViewTextBoxColumn1.Width = 110
        GridViewTextBoxColumn2.HeaderText = "Name"
        GridViewTextBoxColumn2.Name = "Name"
        GridViewTextBoxColumn2.ReadOnly = True
        GridViewTextBoxColumn2.Width = 170
        GridViewTextBoxColumn3.HeaderText = "Existing Route"
        GridViewTextBoxColumn3.Name = "ExistingRoute"
        GridViewTextBoxColumn3.ReadOnly = True
        GridViewTextBoxColumn3.Width = 87
        GridViewTextBoxColumn4.HeaderText = "Existing Route Group"
        GridViewTextBoxColumn4.Name = "ExistRouteGroup"
        GridViewTextBoxColumn4.ReadOnly = True
        GridViewTextBoxColumn4.Width = 121
        GridViewTextBoxColumn5.HeaderText = "New Route"
        GridViewTextBoxColumn5.Name = "NewRoute"
        GridViewTextBoxColumn5.ReadOnly = True
        GridViewTextBoxColumn5.Width = 70
        GridViewCheckBoxColumn1.HeaderText = "Yes/No"
        GridViewCheckBoxColumn1.MinWidth = 20
        GridViewCheckBoxColumn1.Name = "YesNo"
        GridViewTextBoxColumn6.HeaderText = "Visi"
        GridViewTextBoxColumn6.Name = "VisiId"
        GridViewTextBoxColumn6.ReadOnly = True
        Me.MasterTemplate.MasterTemplate.Columns.AddRange(New Telerik.WinControls.UI.GridViewDataColumn() {GridViewTextBoxColumn1, GridViewTextBoxColumn2, GridViewTextBoxColumn3, GridViewTextBoxColumn4, GridViewTextBoxColumn5, GridViewCheckBoxColumn1, GridViewTextBoxColumn6})
        Me.MasterTemplate.MasterTemplate.EnableFiltering = True
        Me.MasterTemplate.MasterTemplate.EnableGrouping = False
        Me.MasterTemplate.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.MasterTemplate.MasterTemplate.ShowHeaderCellButtons = True
        Me.MasterTemplate.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.MasterTemplate.MyStopExport = False
        Me.MasterTemplate.Name = "MasterTemplate"
        Me.MasterTemplate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.MasterTemplate.ShowHeaderCellButtons = True
        Me.MasterTemplate.Size = New System.Drawing.Size(680, 275)
        Me.MasterTemplate.TabIndex = 0
        '
        'lblNewRouteDesc
        '
        Me.lblNewRouteDesc.FieldName = Nothing
        Me.lblNewRouteDesc.Location = New System.Drawing.Point(340, 59)
        Me.lblNewRouteDesc.Name = "lblNewRouteDesc"
        Me.lblNewRouteDesc.Size = New System.Drawing.Size(63, 18)
        Me.lblNewRouteDesc.TabIndex = 13
        Me.lblNewRouteDesc.Text = "Route Desc"
        '
        'txtNewDesc
        '
        Me.txtNewDesc.CalculationExpression = Nothing
        Me.txtNewDesc.FieldCode = Nothing
        Me.txtNewDesc.FieldDesc = Nothing
        Me.txtNewDesc.FieldMaxLength = 0
        Me.txtNewDesc.FieldName = Nothing
        Me.txtNewDesc.isCalculatedField = False
        Me.txtNewDesc.IsSourceFromTable = False
        Me.txtNewDesc.IsSourceFromValueList = False
        Me.txtNewDesc.IsUnique = False
        Me.txtNewDesc.Location = New System.Drawing.Point(424, 59)
        Me.txtNewDesc.MaxLength = 50
        Me.txtNewDesc.MendatroryField = False
        Me.txtNewDesc.MyLinkLable1 = Me.lblNewRouteDesc
        Me.txtNewDesc.MyLinkLable2 = Nothing
        Me.txtNewDesc.Name = "txtNewDesc"
        Me.txtNewDesc.ReferenceFieldDesc = Nothing
        Me.txtNewDesc.ReferenceFieldName = Nothing
        Me.txtNewDesc.ReferenceTableName = Nothing
        Me.txtNewDesc.Size = New System.Drawing.Size(290, 20)
        Me.txtNewDesc.TabIndex = 5
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(650, 1)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(80, 21)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(3, 1)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 21)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.GrpBoxRouteDetail)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(733, 460)
        Me.SplitContainer1.SplitterDistance = 431
        Me.SplitContainer1.TabIndex = 0
        '
        'FrmRoute_Shifting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(733, 460)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmRoute_Shifting"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Outlet  Shifting"
        CType(Me.lblRoutId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRouteDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblrutedesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrpBoxRouteDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpBoxRouteDetail.ResumeLayout(False)
        Me.GrpBoxRouteDetail.PerformLayout()
        CType(Me.lblNewRouteId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpStartDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlRouteGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlstatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblstatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrpBoxDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpBoxDetail.ResumeLayout(False)
        CType(Me.MasterTemplate.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNewRouteDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNewDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtRouteDesc As common.Controls.MyTextBox
    Friend WithEvents GrpBoxRouteDetail As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtNewDesc As common.Controls.MyTextBox
    Friend WithEvents GrpBoxDetail As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents grdviewRouteDetail As common.UserControls.MyRadGridView
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents MasterTemplate As common.UserControls.MyRadGridView
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents ddlstatus As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents dtpStartDate As common.Controls.MyDateTimePicker
    Friend WithEvents ddlRouteGroup As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents lblRoutId As common.Controls.MyLabel
    Friend WithEvents lblNewRouteDesc As common.Controls.MyLabel
    Friend WithEvents lblNewRouteId As common.Controls.MyLabel
    Friend WithEvents lblrutedesc As common.Controls.MyLabel
    Friend WithEvents lblstatus As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents txtRouteId As common.UserControls.txtNavigator
    Friend WithEvents txtNewRouteId As common.UserControls.txtNavigator
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

