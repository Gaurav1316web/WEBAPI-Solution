<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGateEntryReturnPS
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
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.txtRemarks = New common.Controls.MyTextBox()
        Me.lblWeighmentNo = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtCustomerName = New common.Controls.MyTextBox()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtCustomerCode = New common.Controls.MyTextBox()
        Me.fndRefDocNo = New common.UserControls.txtFinder()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.txtVehicleNo = New common.Controls.MyTextBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.lblStartDate = New common.Controls.MyLabel()
        Me.txtRefDocDate = New common.Controls.MyTextBox()
        Me.txtDocDate = New common.Controls.MyDateTimePicker()
        Me.fndDocNo = New common.UserControls.txtNavigator()
        Me.lblDocNo = New common.Controls.MyLabel()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblWeighmentNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustomerName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustomerCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVehicleNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblStartDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRefDocDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDocDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(836, 199)
        Me.SplitContainer1.SplitterDistance = 170
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(836, 170)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.txtRemarks)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.txtCustomerName)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.txtCustomerCode)
        Me.RadPageViewPage1.Controls.Add(Me.fndRefDocNo)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel6)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel7)
        Me.RadPageViewPage1.Controls.Add(Me.txtVehicleNo)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.lblStartDate)
        Me.RadPageViewPage1.Controls.Add(Me.txtRefDocDate)
        Me.RadPageViewPage1.Controls.Add(Me.txtDocDate)
        Me.RadPageViewPage1.Controls.Add(Me.lblWeighmentNo)
        Me.RadPageViewPage1.Controls.Add(Me.fndDocNo)
        Me.RadPageViewPage1.Controls.Add(Me.lblDocNo)
        Me.RadPageViewPage1.Controls.Add(Me.btnReset)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(46.0!, 24.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 33)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(815, 126)
        Me.RadPageViewPage1.Text = "Details"
        '
        'txtRemarks
        '
        Me.txtRemarks.CalculationExpression = Nothing
        Me.txtRemarks.FieldCode = Nothing
        Me.txtRemarks.FieldDesc = Nothing
        Me.txtRemarks.FieldMaxLength = 0
        Me.txtRemarks.FieldName = Nothing
        Me.txtRemarks.isCalculatedField = False
        Me.txtRemarks.IsSourceFromTable = False
        Me.txtRemarks.IsSourceFromValueList = False
        Me.txtRemarks.IsUnique = False
        Me.txtRemarks.Location = New System.Drawing.Point(88, 94)
        Me.txtRemarks.MaxLength = 200
        Me.txtRemarks.MendatroryField = False
        Me.txtRemarks.MyLinkLable1 = Me.lblWeighmentNo
        Me.txtRemarks.MyLinkLable2 = Nothing
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.ReferenceFieldDesc = Nothing
        Me.txtRemarks.ReferenceFieldName = Nothing
        Me.txtRemarks.ReferenceTableName = Nothing
        Me.txtRemarks.Size = New System.Drawing.Size(305, 20)
        Me.txtRemarks.TabIndex = 5
        '
        'lblWeighmentNo
        '
        Me.lblWeighmentNo.FieldName = Nothing
        Me.lblWeighmentNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWeighmentNo.Location = New System.Drawing.Point(408, 5)
        Me.lblWeighmentNo.Name = "lblWeighmentNo"
        Me.lblWeighmentNo.Size = New System.Drawing.Size(85, 16)
        Me.lblWeighmentNo.TabIndex = 410
        Me.lblWeighmentNo.Text = "Document Date"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(0, 98)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(51, 16)
        Me.MyLabel1.TabIndex = 427
        Me.MyLabel1.Text = "Remarks"
        '
        'txtCustomerName
        '
        Me.txtCustomerName.CalculationExpression = Nothing
        Me.txtCustomerName.FieldCode = Nothing
        Me.txtCustomerName.FieldDesc = Nothing
        Me.txtCustomerName.FieldMaxLength = 0
        Me.txtCustomerName.FieldName = Nothing
        Me.txtCustomerName.isCalculatedField = False
        Me.txtCustomerName.IsSourceFromTable = False
        Me.txtCustomerName.IsSourceFromValueList = False
        Me.txtCustomerName.IsUnique = False
        Me.txtCustomerName.Location = New System.Drawing.Point(503, 48)
        Me.txtCustomerName.MaxLength = 50
        Me.txtCustomerName.MendatroryField = False
        Me.txtCustomerName.MyLinkLable1 = Me.lblWeighmentNo
        Me.txtCustomerName.MyLinkLable2 = Nothing
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.ReadOnly = True
        Me.txtCustomerName.ReferenceFieldDesc = Nothing
        Me.txtCustomerName.ReferenceFieldName = Nothing
        Me.txtCustomerName.ReferenceTableName = Nothing
        Me.txtCustomerName.Size = New System.Drawing.Size(305, 20)
        Me.txtCustomerName.TabIndex = 3
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(1, 31)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel5.TabIndex = 426
        Me.MyLabel5.Text = "Ref Doc No"
        '
        'txtCustomerCode
        '
        Me.txtCustomerCode.CalculationExpression = Nothing
        Me.txtCustomerCode.FieldCode = Nothing
        Me.txtCustomerCode.FieldDesc = Nothing
        Me.txtCustomerCode.FieldMaxLength = 0
        Me.txtCustomerCode.FieldName = Nothing
        Me.txtCustomerCode.isCalculatedField = False
        Me.txtCustomerCode.IsSourceFromTable = False
        Me.txtCustomerCode.IsSourceFromValueList = False
        Me.txtCustomerCode.IsUnique = False
        Me.txtCustomerCode.Location = New System.Drawing.Point(88, 49)
        Me.txtCustomerCode.MaxLength = 200
        Me.txtCustomerCode.MendatroryField = False
        Me.txtCustomerCode.MyLinkLable1 = Me.lblWeighmentNo
        Me.txtCustomerCode.MyLinkLable2 = Nothing
        Me.txtCustomerCode.Name = "txtCustomerCode"
        Me.txtCustomerCode.ReferenceFieldDesc = Nothing
        Me.txtCustomerCode.ReferenceFieldName = Nothing
        Me.txtCustomerCode.ReferenceTableName = Nothing
        Me.txtCustomerCode.Size = New System.Drawing.Size(305, 20)
        Me.txtCustomerCode.TabIndex = 2
        '
        'fndRefDocNo
        '
        Me.fndRefDocNo.CalculationExpression = Nothing
        Me.fndRefDocNo.FieldCode = Nothing
        Me.fndRefDocNo.FieldDesc = Nothing
        Me.fndRefDocNo.FieldMaxLength = 0
        Me.fndRefDocNo.FieldName = Nothing
        Me.fndRefDocNo.isCalculatedField = False
        Me.fndRefDocNo.IsSourceFromTable = False
        Me.fndRefDocNo.IsSourceFromValueList = False
        Me.fndRefDocNo.IsUnique = False
        Me.fndRefDocNo.Location = New System.Drawing.Point(88, 27)
        Me.fndRefDocNo.MendatroryField = False
        Me.fndRefDocNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndRefDocNo.MyLinkLable1 = Nothing
        Me.fndRefDocNo.MyLinkLable2 = Nothing
        Me.fndRefDocNo.MyReadOnly = False
        Me.fndRefDocNo.MyShowMasterFormButton = False
        Me.fndRefDocNo.Name = "fndRefDocNo"
        Me.fndRefDocNo.ReferenceFieldDesc = Nothing
        Me.fndRefDocNo.ReferenceFieldName = Nothing
        Me.fndRefDocNo.ReferenceTableName = Nothing
        Me.fndRefDocNo.Size = New System.Drawing.Size(305, 19)
        Me.fndRefDocNo.TabIndex = 1
        Me.fndRefDocNo.Value = ""
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(408, 52)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(89, 16)
        Me.MyLabel6.TabIndex = 420
        Me.MyLabel6.Text = "Customer Name"
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(1, 54)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(85, 16)
        Me.MyLabel7.TabIndex = 419
        Me.MyLabel7.Text = "Customer Code"
        '
        'txtVehicleNo
        '
        Me.txtVehicleNo.CalculationExpression = Nothing
        Me.txtVehicleNo.FieldCode = Nothing
        Me.txtVehicleNo.FieldDesc = Nothing
        Me.txtVehicleNo.FieldMaxLength = 0
        Me.txtVehicleNo.FieldName = Nothing
        Me.txtVehicleNo.isCalculatedField = False
        Me.txtVehicleNo.IsSourceFromTable = False
        Me.txtVehicleNo.IsSourceFromValueList = False
        Me.txtVehicleNo.IsUnique = False
        Me.txtVehicleNo.Location = New System.Drawing.Point(88, 72)
        Me.txtVehicleNo.MaxLength = 50
        Me.txtVehicleNo.MendatroryField = False
        Me.txtVehicleNo.MyLinkLable1 = Me.lblWeighmentNo
        Me.txtVehicleNo.MyLinkLable2 = Nothing
        Me.txtVehicleNo.Name = "txtVehicleNo"
        Me.txtVehicleNo.ReferenceFieldDesc = Nothing
        Me.txtVehicleNo.ReferenceFieldName = Nothing
        Me.txtVehicleNo.ReferenceTableName = Nothing
        Me.txtVehicleNo.Size = New System.Drawing.Size(305, 20)
        Me.txtVehicleNo.TabIndex = 4
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(0, 76)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(61, 16)
        Me.MyLabel2.TabIndex = 416
        Me.MyLabel2.Text = "Vehicle No"
        '
        'lblStartDate
        '
        Me.lblStartDate.FieldName = Nothing
        Me.lblStartDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStartDate.Location = New System.Drawing.Point(408, 29)
        Me.lblStartDate.Name = "lblStartDate"
        Me.lblStartDate.Size = New System.Drawing.Size(74, 16)
        Me.lblStartDate.TabIndex = 414
        Me.lblStartDate.Text = "Ref Doc Date"
        '
        'txtRefDocDate
        '
        Me.txtRefDocDate.CalculationExpression = Nothing
        Me.txtRefDocDate.FieldCode = Nothing
        Me.txtRefDocDate.FieldDesc = Nothing
        Me.txtRefDocDate.FieldMaxLength = 0
        Me.txtRefDocDate.FieldName = Nothing
        Me.txtRefDocDate.isCalculatedField = False
        Me.txtRefDocDate.IsSourceFromTable = False
        Me.txtRefDocDate.IsSourceFromValueList = False
        Me.txtRefDocDate.IsUnique = False
        Me.txtRefDocDate.Location = New System.Drawing.Point(503, 26)
        Me.txtRefDocDate.MaxLength = 50
        Me.txtRefDocDate.MendatroryField = False
        Me.txtRefDocDate.MyLinkLable1 = Me.lblWeighmentNo
        Me.txtRefDocDate.MyLinkLable2 = Nothing
        Me.txtRefDocDate.Name = "txtRefDocDate"
        Me.txtRefDocDate.ReadOnly = True
        Me.txtRefDocDate.ReferenceFieldDesc = Nothing
        Me.txtRefDocDate.ReferenceFieldName = Nothing
        Me.txtRefDocDate.ReferenceTableName = Nothing
        Me.txtRefDocDate.Size = New System.Drawing.Size(305, 20)
        Me.txtRefDocDate.TabIndex = 413
        '
        'txtDocDate
        '
        Me.txtDocDate.CalculationExpression = Nothing
        Me.txtDocDate.CustomFormat = "dd/MM/yyyy"
        Me.txtDocDate.FieldCode = Nothing
        Me.txtDocDate.FieldDesc = Nothing
        Me.txtDocDate.FieldMaxLength = 0
        Me.txtDocDate.FieldName = Nothing
        Me.txtDocDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDocDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDocDate.isCalculatedField = False
        Me.txtDocDate.IsSourceFromTable = False
        Me.txtDocDate.IsSourceFromValueList = False
        Me.txtDocDate.IsUnique = False
        Me.txtDocDate.Location = New System.Drawing.Point(503, 4)
        Me.txtDocDate.MendatroryField = False
        Me.txtDocDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDocDate.MyLinkLable1 = Nothing
        Me.txtDocDate.MyLinkLable2 = Nothing
        Me.txtDocDate.Name = "txtDocDate"
        Me.txtDocDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDocDate.ReferenceFieldDesc = Nothing
        Me.txtDocDate.ReferenceFieldName = Nothing
        Me.txtDocDate.ReferenceTableName = Nothing
        Me.txtDocDate.Size = New System.Drawing.Size(79, 18)
        Me.txtDocDate.TabIndex = 411
        Me.txtDocDate.TabStop = False
        Me.txtDocDate.Text = "13/06/2011"
        Me.txtDocDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'fndDocNo
        '
        Me.fndDocNo.FieldName = Nothing
        Me.fndDocNo.Location = New System.Drawing.Point(88, 4)
        Me.fndDocNo.MendatroryField = False
        Me.fndDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.fndDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndDocNo.MyLinkLable1 = Me.lblDocNo
        Me.fndDocNo.MyLinkLable2 = Nothing
        Me.fndDocNo.MyMaxLength = 32767
        Me.fndDocNo.MyReadOnly = False
        Me.fndDocNo.Name = "fndDocNo"
        Me.fndDocNo.Size = New System.Drawing.Size(287, 18)
        Me.fndDocNo.TabIndex = 0
        Me.fndDocNo.Value = ""
        '
        'lblDocNo
        '
        Me.lblDocNo.FieldName = Nothing
        Me.lblDocNo.Location = New System.Drawing.Point(1, 5)
        Me.lblDocNo.Name = "lblDocNo"
        Me.lblDocNo.Size = New System.Drawing.Size(79, 18)
        Me.lblDocNo.TabIndex = 409
        Me.lblDocNo.Text = "Document No."
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(144, 3)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(68, 18)
        Me.btnPost.TabIndex = 2
        Me.btnPost.Text = "Post"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(765, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(3, 3)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 18)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(73, 3)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 18)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnReset
        '
        Me.btnReset.Image = Global.ERP.My.Resources.Resources._new
        Me.btnReset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReset.Location = New System.Drawing.Point(375, 4)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(18, 18)
        Me.btnReset.TabIndex = 412
        '
        'frmGateEntryReturnPS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(836, 199)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmGateEntryReturnPS"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Gate Return"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblWeighmentNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustomerName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustomerCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVehicleNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblStartDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRefDocDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDocDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDocDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblWeighmentNo As common.Controls.MyLabel
    Friend WithEvents fndDocNo As common.UserControls.txtNavigator
    Friend WithEvents lblDocNo As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents fndRefDocNo As common.UserControls.txtFinder
    Friend WithEvents txtVehicleNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents lblStartDate As common.Controls.MyLabel
    Friend WithEvents txtRefDocDate As common.Controls.MyTextBox
    Friend WithEvents txtCustomerName As common.Controls.MyTextBox
    Friend WithEvents txtCustomerCode As common.Controls.MyTextBox
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtRemarks As common.Controls.MyTextBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
End Class

