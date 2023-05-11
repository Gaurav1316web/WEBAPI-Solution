<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmGenerateFreshBooking
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
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem5 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem6 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem7 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.lblDate = New common.Controls.MyLabel()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.txtCustomerNo = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.cmbBookingType = New common.Controls.MyComboBox()
        Me.MyLabel21 = New common.Controls.MyLabel()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtFinder()
        Me.RadLabel15 = New common.Controls.MyLabel()
        Me.lblRouteDesc = New common.Controls.MyLabel()
        Me.lblRouteNo = New common.Controls.MyLabel()
        Me.txtRouteNo = New common.UserControls.txtFinder()
        Me.btnShow = New Telerik.WinControls.UI.RadButton()
        Me.txtBookingDate = New common.Controls.MyDateTimePicker()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.lblToDate = New common.Controls.MyLabel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnRefresh = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.fndRouteNo = New common.UserControls.txtMultiSelectFinder()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbBookingType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRouteDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRouteNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnShow, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBookingDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblDate
        '
        Me.lblDate.FieldName = Nothing
        Me.lblDate.Location = New System.Drawing.Point(6, 92)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(30, 18)
        Me.lblDate.TabIndex = 5
        Me.lblDate.Text = "Date"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndRouteNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCustomerNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cmbBookingType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel21)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel15)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblRouteDesc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblRouteNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtRouteNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnShow)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtBookingDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblToDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDate)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Panel1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.gv1)
        Me.SplitContainer1.Size = New System.Drawing.Size(765, 525)
        Me.SplitContainer1.SplitterDistance = 138
        Me.SplitContainer1.TabIndex = 0
        '
        'txtCustomerNo
        '
        Me.txtCustomerNo.arrDispalyMember = Nothing
        Me.txtCustomerNo.arrValueMember = Nothing
        Me.txtCustomerNo.Location = New System.Drawing.Point(100, 26)
        Me.txtCustomerNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerNo.MyLinkLable1 = Me.MyLabel1
        Me.txtCustomerNo.MyLinkLable2 = Nothing
        Me.txtCustomerNo.MyNullText = "All"
        Me.txtCustomerNo.Name = "txtCustomerNo"
        Me.txtCustomerNo.Size = New System.Drawing.Size(370, 19)
        Me.txtCustomerNo.TabIndex = 1466
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(6, 26)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(55, 18)
        Me.MyLabel1.TabIndex = 1465
        Me.MyLabel1.Text = "Customer"
        '
        'cmbBookingType
        '
        Me.cmbBookingType.AutoCompleteDisplayMember = Nothing
        Me.cmbBookingType.AutoCompleteValueMember = Nothing
        Me.cmbBookingType.CalculationExpression = Nothing
        Me.cmbBookingType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbBookingType.FieldCode = Nothing
        Me.cmbBookingType.FieldDesc = Nothing
        Me.cmbBookingType.FieldMaxLength = 0
        Me.cmbBookingType.FieldName = Nothing
        Me.cmbBookingType.isCalculatedField = False
        Me.cmbBookingType.IsSourceFromTable = False
        Me.cmbBookingType.IsSourceFromValueList = False
        Me.cmbBookingType.IsUnique = False
        RadListDataItem1.Text = "Select"
        RadListDataItem2.Text = "CD"
        RadListDataItem3.Text = "CR"
        RadListDataItem4.Text = "SO"
        RadListDataItem5.Text = "Cash"
        RadListDataItem6.Text = "Festive Offer"
        RadListDataItem7.Text = "Distributor"
        Me.cmbBookingType.Items.Add(RadListDataItem1)
        Me.cmbBookingType.Items.Add(RadListDataItem2)
        Me.cmbBookingType.Items.Add(RadListDataItem3)
        Me.cmbBookingType.Items.Add(RadListDataItem4)
        Me.cmbBookingType.Items.Add(RadListDataItem5)
        Me.cmbBookingType.Items.Add(RadListDataItem6)
        Me.cmbBookingType.Items.Add(RadListDataItem7)
        Me.cmbBookingType.Location = New System.Drawing.Point(100, 68)
        Me.cmbBookingType.MendatroryField = True
        Me.cmbBookingType.MyLinkLable1 = Nothing
        Me.cmbBookingType.MyLinkLable2 = Nothing
        Me.cmbBookingType.Name = "cmbBookingType"
        Me.cmbBookingType.ReferenceFieldDesc = Nothing
        Me.cmbBookingType.ReferenceFieldName = Nothing
        Me.cmbBookingType.ReferenceTableName = Nothing
        Me.cmbBookingType.Size = New System.Drawing.Size(143, 20)
        Me.cmbBookingType.TabIndex = 1463
        '
        'MyLabel21
        '
        Me.MyLabel21.FieldName = Nothing
        Me.MyLabel21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel21.Location = New System.Drawing.Point(6, 70)
        Me.MyLabel21.Name = "MyLabel21"
        Me.MyLabel21.Size = New System.Drawing.Size(75, 16)
        Me.MyLabel21.TabIndex = 1464
        Me.MyLabel21.Text = "Booking Type"
        '
        'lblLocation
        '
        Me.lblLocation.AutoSize = False
        Me.lblLocation.BorderVisible = True
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(243, 48)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(227, 18)
        Me.lblLocation.TabIndex = 163
        Me.lblLocation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblLocation.TextWrap = False
        '
        'txtLocation
        '
        Me.txtLocation.CalculationExpression = Nothing
        Me.txtLocation.FieldCode = Nothing
        Me.txtLocation.FieldDesc = Nothing
        Me.txtLocation.FieldMaxLength = 0
        Me.txtLocation.FieldName = Nothing
        Me.txtLocation.isCalculatedField = False
        Me.txtLocation.IsSourceFromTable = False
        Me.txtLocation.IsSourceFromValueList = False
        Me.txtLocation.IsUnique = False
        Me.txtLocation.Location = New System.Drawing.Point(100, 47)
        Me.txtLocation.MendatroryField = True
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.RadLabel15
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.MyReadOnly = False
        Me.txtLocation.MyShowMasterFormButton = False
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.ReferenceFieldDesc = Nothing
        Me.txtLocation.ReferenceFieldName = Nothing
        Me.txtLocation.ReferenceTableName = Nothing
        Me.txtLocation.Size = New System.Drawing.Size(142, 19)
        Me.txtLocation.TabIndex = 162
        Me.txtLocation.Value = ""
        '
        'RadLabel15
        '
        Me.RadLabel15.FieldName = Nothing
        Me.RadLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel15.Location = New System.Drawing.Point(6, 50)
        Me.RadLabel15.Name = "RadLabel15"
        Me.RadLabel15.Size = New System.Drawing.Size(49, 16)
        Me.RadLabel15.TabIndex = 161
        Me.RadLabel15.Text = "Location"
        '
        'lblRouteDesc
        '
        Me.lblRouteDesc.AutoSize = False
        Me.lblRouteDesc.BorderVisible = True
        Me.lblRouteDesc.FieldName = Nothing
        Me.lblRouteDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRouteDesc.Location = New System.Drawing.Point(528, 73)
        Me.lblRouteDesc.Name = "lblRouteDesc"
        Me.lblRouteDesc.Size = New System.Drawing.Size(227, 18)
        Me.lblRouteDesc.TabIndex = 160
        Me.lblRouteDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblRouteDesc.TextWrap = False
        Me.lblRouteDesc.Visible = False
        '
        'lblRouteNo
        '
        Me.lblRouteNo.FieldName = Nothing
        Me.lblRouteNo.Location = New System.Drawing.Point(6, 5)
        Me.lblRouteNo.Name = "lblRouteNo"
        Me.lblRouteNo.Size = New System.Drawing.Size(54, 18)
        Me.lblRouteNo.TabIndex = 159
        Me.lblRouteNo.Text = "Route No"
        '
        'txtRouteNo
        '
        Me.txtRouteNo.CalculationExpression = Nothing
        Me.txtRouteNo.FieldCode = Nothing
        Me.txtRouteNo.FieldDesc = Nothing
        Me.txtRouteNo.FieldMaxLength = 0
        Me.txtRouteNo.FieldName = Nothing
        Me.txtRouteNo.isCalculatedField = False
        Me.txtRouteNo.IsSourceFromTable = False
        Me.txtRouteNo.IsSourceFromValueList = False
        Me.txtRouteNo.IsUnique = False
        Me.txtRouteNo.Location = New System.Drawing.Point(385, 72)
        Me.txtRouteNo.MendatroryField = True
        Me.txtRouteNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRouteNo.MyLinkLable1 = Me.lblRouteNo
        Me.txtRouteNo.MyLinkLable2 = Nothing
        Me.txtRouteNo.MyReadOnly = False
        Me.txtRouteNo.MyShowMasterFormButton = False
        Me.txtRouteNo.Name = "txtRouteNo"
        Me.txtRouteNo.ReferenceFieldDesc = Nothing
        Me.txtRouteNo.ReferenceFieldName = Nothing
        Me.txtRouteNo.ReferenceTableName = Nothing
        Me.txtRouteNo.Size = New System.Drawing.Size(142, 19)
        Me.txtRouteNo.TabIndex = 158
        Me.txtRouteNo.Value = ""
        Me.txtRouteNo.Visible = False
        '
        'btnShow
        '
        Me.btnShow.Location = New System.Drawing.Point(248, 99)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(77, 33)
        Me.btnShow.TabIndex = 3
        Me.btnShow.Text = ">>"
        '
        'txtBookingDate
        '
        Me.txtBookingDate.CalculationExpression = Nothing
        Me.txtBookingDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtBookingDate.Enabled = False
        Me.txtBookingDate.FieldCode = Nothing
        Me.txtBookingDate.FieldDesc = Nothing
        Me.txtBookingDate.FieldMaxLength = 0
        Me.txtBookingDate.FieldName = Nothing
        Me.txtBookingDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtBookingDate.isCalculatedField = False
        Me.txtBookingDate.IsSourceFromTable = False
        Me.txtBookingDate.IsSourceFromValueList = False
        Me.txtBookingDate.IsUnique = False
        Me.txtBookingDate.Location = New System.Drawing.Point(99, 112)
        Me.txtBookingDate.MendatroryField = False
        Me.txtBookingDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtBookingDate.MyLinkLable1 = Nothing
        Me.txtBookingDate.MyLinkLable2 = Nothing
        Me.txtBookingDate.Name = "txtBookingDate"
        Me.txtBookingDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtBookingDate.ReferenceFieldDesc = Nothing
        Me.txtBookingDate.ReferenceFieldName = Nothing
        Me.txtBookingDate.ReferenceTableName = Nothing
        Me.txtBookingDate.Size = New System.Drawing.Size(143, 20)
        Me.txtBookingDate.TabIndex = 2
        Me.txtBookingDate.TabStop = False
        Me.txtBookingDate.Text = "03/08/2012 12:41 PM"
        Me.txtBookingDate.Value = New Date(2012, 8, 3, 12, 41, 27, 281)
        '
        'txtDate
        '
        Me.txtDate.CalculationExpression = Nothing
        Me.txtDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtDate.FieldCode = Nothing
        Me.txtDate.FieldDesc = Nothing
        Me.txtDate.FieldMaxLength = 0
        Me.txtDate.FieldName = Nothing
        Me.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDate.isCalculatedField = False
        Me.txtDate.IsSourceFromTable = False
        Me.txtDate.IsSourceFromValueList = False
        Me.txtDate.IsUnique = False
        Me.txtDate.Location = New System.Drawing.Point(99, 90)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Nothing
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(143, 20)
        Me.txtDate.TabIndex = 0
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "03/08/2012 12:41 PM"
        Me.txtDate.Value = New Date(2012, 8, 3, 12, 41, 27, 281)
        '
        'lblToDate
        '
        Me.lblToDate.FieldName = Nothing
        Me.lblToDate.Location = New System.Drawing.Point(6, 114)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(74, 18)
        Me.lblToDate.TabIndex = 4
        Me.lblToDate.Text = "Booking Date"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnRefresh)
        Me.Panel1.Controls.Add(Me.btnSave)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 354)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(765, 29)
        Me.Panel1.TabIndex = 1
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.Location = New System.Drawing.Point(87, 3)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(75, 23)
        Me.btnRefresh.TabIndex = 2
        Me.btnRefresh.Text = "Refresh"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(6, 3)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "Save"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(678, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 0
        Me.btnClose.Text = "Close"
        '
        'gv1
        '
        Me.gv1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.EnableFiltering = True
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(765, 351)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        Me.gv1.Text = "RadGridView1"
        '
        'fndRouteNo
        '
        Me.fndRouteNo.arrDispalyMember = Nothing
        Me.fndRouteNo.arrValueMember = Nothing
        Me.fndRouteNo.Location = New System.Drawing.Point(99, 5)
        Me.fndRouteNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndRouteNo.MyLinkLable1 = Me.MyLabel1
        Me.fndRouteNo.MyLinkLable2 = Nothing
        Me.fndRouteNo.MyNullText = "All"
        Me.fndRouteNo.Name = "fndRouteNo"
        Me.fndRouteNo.Size = New System.Drawing.Size(370, 19)
        Me.fndRouteNo.TabIndex = 1467
        '
        'FrmGenerateFreshBooking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(765, 525)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmGenerateFreshBooking"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Generate Booking"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbBookingType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRouteDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRouteNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnShow, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBookingDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblDate As common.Controls.MyLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtBookingDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblToDate As common.Controls.MyLabel
    Friend WithEvents btnShow As Telerik.WinControls.UI.RadButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtFinder
    Friend WithEvents RadLabel15 As common.Controls.MyLabel
    Friend WithEvents lblRouteDesc As common.Controls.MyLabel
    Friend WithEvents lblRouteNo As common.Controls.MyLabel
    Friend WithEvents txtRouteNo As common.UserControls.txtFinder
    Friend WithEvents cmbBookingType As common.Controls.MyComboBox
    Friend WithEvents MyLabel21 As common.Controls.MyLabel
    Friend WithEvents txtCustomerNo As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents btnRefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndRouteNo As common.UserControls.txtMultiSelectFinder
End Class

