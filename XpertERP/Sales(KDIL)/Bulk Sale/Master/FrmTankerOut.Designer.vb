<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmTankerOut
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.lblGateEntryNo = New common.Controls.MyLabel()
        Me.lblCustomerCode = New common.Controls.MyLabel()
        Me.lblLocationCode = New common.Controls.MyLabel()
        Me.chkGateOut = New common.Controls.MyCheckBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.fndGateEntryIn = New common.UserControls.txtFinder()
        Me.lblTanker = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.FndCustomer = New common.UserControls.txtFinder()
        Me.LblCustomer = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.fndTanker = New common.UserControls.txtFinder()
        Me.lblLocationBulk = New common.Controls.MyLabel()
        Me.fndLocation = New common.UserControls.txtFinder()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.lblDateAndTimeBulk = New common.Controls.MyLabel()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.fndGateOutNo = New common.UserControls.txtNavigator()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvManualSeal = New common.UserControls.MyRadGridView()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.lblGateEntryNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomerCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkGateOut, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTanker, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDateAndTimeBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.gvManualSeal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvManualSeal.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblGateEntryNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCustomerCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLocationCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkGateOut)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndGateEntryIn)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblTanker)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.FndCustomer)
        Me.SplitContainer1.Panel1.Controls.Add(Me.LblCustomer)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndTanker)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLocationBulk)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDateAndTimeBulk)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndGateOutNo)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(674, 512)
        Me.SplitContainer1.SplitterDistance = 471
        Me.SplitContainer1.TabIndex = 0
        '
        'lblGateEntryNo
        '
        Me.lblGateEntryNo.AutoSize = False
        Me.lblGateEntryNo.BorderVisible = True
        Me.lblGateEntryNo.FieldName = Nothing
        Me.lblGateEntryNo.Location = New System.Drawing.Point(125, 87)
        Me.lblGateEntryNo.Name = "lblGateEntryNo"
        Me.lblGateEntryNo.Size = New System.Drawing.Size(147, 19)
        Me.lblGateEntryNo.TabIndex = 19
        Me.lblGateEntryNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCustomerCode
        '
        Me.lblCustomerCode.AutoSize = False
        Me.lblCustomerCode.BorderVisible = True
        Me.lblCustomerCode.FieldName = Nothing
        Me.lblCustomerCode.Location = New System.Drawing.Point(125, 112)
        Me.lblCustomerCode.Name = "lblCustomerCode"
        Me.lblCustomerCode.Size = New System.Drawing.Size(147, 19)
        Me.lblCustomerCode.TabIndex = 18
        Me.lblCustomerCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblLocationCode
        '
        Me.lblLocationCode.AutoSize = False
        Me.lblLocationCode.BorderVisible = True
        Me.lblLocationCode.FieldName = Nothing
        Me.lblLocationCode.Location = New System.Drawing.Point(125, 137)
        Me.lblLocationCode.Name = "lblLocationCode"
        Me.lblLocationCode.Size = New System.Drawing.Size(147, 19)
        Me.lblLocationCode.TabIndex = 17
        Me.lblLocationCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkGateOut
        '
        Me.chkGateOut.Location = New System.Drawing.Point(278, 62)
        Me.chkGateOut.MyLinkLable1 = Nothing
        Me.chkGateOut.MyLinkLable2 = Nothing
        Me.chkGateOut.Name = "chkGateOut"
        Me.chkGateOut.Size = New System.Drawing.Size(65, 18)
        Me.chkGateOut.TabIndex = 7
        Me.chkGateOut.Tag1 = Nothing
        Me.chkGateOut.Text = "Gate Out"
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel4.Location = New System.Drawing.Point(12, 88)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(78, 16)
        Me.MyLabel4.TabIndex = 5
        Me.MyLabel4.Text = "Gate Entry No"
        '
        'fndGateEntryIn
        '
        Me.fndGateEntryIn.CalculationExpression = Nothing
        Me.fndGateEntryIn.Enabled = False
        Me.fndGateEntryIn.FieldCode = Nothing
        Me.fndGateEntryIn.FieldDesc = Nothing
        Me.fndGateEntryIn.FieldMaxLength = 0
        Me.fndGateEntryIn.FieldName = Nothing
        Me.fndGateEntryIn.isCalculatedField = False
        Me.fndGateEntryIn.IsSourceFromTable = False
        Me.fndGateEntryIn.IsSourceFromValueList = False
        Me.fndGateEntryIn.IsUnique = False
        Me.fndGateEntryIn.Location = New System.Drawing.Point(503, 35)
        Me.fndGateEntryIn.MendatroryField = False
        Me.fndGateEntryIn.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndGateEntryIn.MyLinkLable1 = Me.MyLabel4
        Me.fndGateEntryIn.MyLinkLable2 = Nothing
        Me.fndGateEntryIn.MyReadOnly = False
        Me.fndGateEntryIn.MyShowMasterFormButton = False
        Me.fndGateEntryIn.Name = "fndGateEntryIn"
        Me.fndGateEntryIn.ReferenceFieldDesc = Nothing
        Me.fndGateEntryIn.ReferenceFieldName = Nothing
        Me.fndGateEntryIn.ReferenceTableName = Nothing
        Me.fndGateEntryIn.Size = New System.Drawing.Size(147, 19)
        Me.fndGateEntryIn.TabIndex = 6
        Me.fndGateEntryIn.Value = ""
        Me.fndGateEntryIn.Visible = False
        '
        'lblTanker
        '
        Me.lblTanker.AutoSize = False
        Me.lblTanker.BorderVisible = True
        Me.lblTanker.FieldName = Nothing
        Me.lblTanker.Location = New System.Drawing.Point(278, 159)
        Me.lblTanker.Name = "lblTanker"
        Me.lblTanker.Size = New System.Drawing.Size(381, 19)
        Me.lblTanker.TabIndex = 16
        Me.lblTanker.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTanker.Visible = False
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel3.Location = New System.Drawing.Point(12, 113)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(55, 16)
        Me.MyLabel3.TabIndex = 8
        Me.MyLabel3.Text = "Customer"
        '
        'FndCustomer
        '
        Me.FndCustomer.CalculationExpression = Nothing
        Me.FndCustomer.Enabled = False
        Me.FndCustomer.FieldCode = Nothing
        Me.FndCustomer.FieldDesc = Nothing
        Me.FndCustomer.FieldMaxLength = 0
        Me.FndCustomer.FieldName = Nothing
        Me.FndCustomer.isCalculatedField = False
        Me.FndCustomer.IsSourceFromTable = False
        Me.FndCustomer.IsSourceFromValueList = False
        Me.FndCustomer.IsUnique = False
        Me.FndCustomer.Location = New System.Drawing.Point(619, 63)
        Me.FndCustomer.MendatroryField = False
        Me.FndCustomer.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndCustomer.MyLinkLable1 = Me.MyLabel3
        Me.FndCustomer.MyLinkLable2 = Nothing
        Me.FndCustomer.MyReadOnly = False
        Me.FndCustomer.MyShowMasterFormButton = False
        Me.FndCustomer.Name = "FndCustomer"
        Me.FndCustomer.ReferenceFieldDesc = Nothing
        Me.FndCustomer.ReferenceFieldName = Nothing
        Me.FndCustomer.ReferenceTableName = Nothing
        Me.FndCustomer.Size = New System.Drawing.Size(31, 19)
        Me.FndCustomer.TabIndex = 9
        Me.FndCustomer.Value = ""
        Me.FndCustomer.Visible = False
        '
        'LblCustomer
        '
        Me.LblCustomer.AutoSize = False
        Me.LblCustomer.BorderVisible = True
        Me.LblCustomer.FieldName = Nothing
        Me.LblCustomer.Location = New System.Drawing.Point(278, 112)
        Me.LblCustomer.Name = "LblCustomer"
        Me.LblCustomer.Size = New System.Drawing.Size(381, 19)
        Me.LblCustomer.TabIndex = 10
        Me.LblCustomer.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel2.Location = New System.Drawing.Point(12, 63)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(41, 16)
        Me.MyLabel2.TabIndex = 14
        Me.MyLabel2.Text = "Tanker"
        '
        'fndTanker
        '
        Me.fndTanker.CalculationExpression = Nothing
        Me.fndTanker.FieldCode = Nothing
        Me.fndTanker.FieldDesc = Nothing
        Me.fndTanker.FieldMaxLength = 0
        Me.fndTanker.FieldName = Nothing
        Me.fndTanker.isCalculatedField = False
        Me.fndTanker.IsSourceFromTable = False
        Me.fndTanker.IsSourceFromValueList = False
        Me.fndTanker.IsUnique = False
        Me.fndTanker.Location = New System.Drawing.Point(125, 62)
        Me.fndTanker.MendatroryField = True
        Me.fndTanker.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndTanker.MyLinkLable1 = Me.MyLabel2
        Me.fndTanker.MyLinkLable2 = Nothing
        Me.fndTanker.MyReadOnly = False
        Me.fndTanker.MyShowMasterFormButton = False
        Me.fndTanker.Name = "fndTanker"
        Me.fndTanker.ReferenceFieldDesc = Nothing
        Me.fndTanker.ReferenceFieldName = Nothing
        Me.fndTanker.ReferenceTableName = Nothing
        Me.fndTanker.Size = New System.Drawing.Size(147, 19)
        Me.fndTanker.TabIndex = 15
        Me.fndTanker.Value = ""
        '
        'lblLocationBulk
        '
        Me.lblLocationBulk.FieldName = Nothing
        Me.lblLocationBulk.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblLocationBulk.Location = New System.Drawing.Point(12, 138)
        Me.lblLocationBulk.Name = "lblLocationBulk"
        Me.lblLocationBulk.Size = New System.Drawing.Size(49, 16)
        Me.lblLocationBulk.TabIndex = 11
        Me.lblLocationBulk.Text = "Location"
        '
        'fndLocation
        '
        Me.fndLocation.CalculationExpression = Nothing
        Me.fndLocation.Enabled = False
        Me.fndLocation.FieldCode = Nothing
        Me.fndLocation.FieldDesc = Nothing
        Me.fndLocation.FieldMaxLength = 0
        Me.fndLocation.FieldName = Nothing
        Me.fndLocation.isCalculatedField = False
        Me.fndLocation.IsSourceFromTable = False
        Me.fndLocation.IsSourceFromValueList = False
        Me.fndLocation.IsUnique = False
        Me.fndLocation.Location = New System.Drawing.Point(609, 88)
        Me.fndLocation.MendatroryField = False
        Me.fndLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLocation.MyLinkLable1 = Me.lblLocationBulk
        Me.fndLocation.MyLinkLable2 = Nothing
        Me.fndLocation.MyReadOnly = False
        Me.fndLocation.MyShowMasterFormButton = False
        Me.fndLocation.Name = "fndLocation"
        Me.fndLocation.ReferenceFieldDesc = Nothing
        Me.fndLocation.ReferenceFieldName = Nothing
        Me.fndLocation.ReferenceTableName = Nothing
        Me.fndLocation.Size = New System.Drawing.Size(41, 19)
        Me.fndLocation.TabIndex = 12
        Me.fndLocation.Value = ""
        Me.fndLocation.Visible = False
        '
        'lblLocation
        '
        Me.lblLocation.AutoSize = False
        Me.lblLocation.BorderVisible = True
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Location = New System.Drawing.Point(278, 137)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(381, 19)
        Me.lblLocation.TabIndex = 13
        Me.lblLocation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(11, 11)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(70, 16)
        Me.MyLabel1.TabIndex = 0
        Me.MyLabel1.Text = "Gate Out No"
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
        Me.txtDate.Location = New System.Drawing.Point(125, 36)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.lblDateAndTimeBulk
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(151, 20)
        Me.txtDate.TabIndex = 4
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "10/06/2011 11:51 AM"
        Me.txtDate.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'lblDateAndTimeBulk
        '
        Me.lblDateAndTimeBulk.FieldName = Nothing
        Me.lblDateAndTimeBulk.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDateAndTimeBulk.Location = New System.Drawing.Point(11, 38)
        Me.lblDateAndTimeBulk.Name = "lblDateAndTimeBulk"
        Me.lblDateAndTimeBulk.Size = New System.Drawing.Size(79, 16)
        Me.lblDateAndTimeBulk.TabIndex = 3
        Me.lblDateAndTimeBulk.Text = "Gate Out Date"
        '
        'btnNew
        '
        Me.btnNew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnNew.Location = New System.Drawing.Point(346, 9)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(18, 20)
        Me.btnNew.TabIndex = 2
        '
        'fndGateOutNo
        '
        Me.fndGateOutNo.FieldName = Nothing
        Me.fndGateOutNo.Location = New System.Drawing.Point(125, 9)
        Me.fndGateOutNo.MendatroryField = False
        Me.fndGateOutNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndGateOutNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndGateOutNo.MyLinkLable1 = Nothing
        Me.fndGateOutNo.MyLinkLable2 = Nothing
        Me.fndGateOutNo.MyMaxLength = 12
        Me.fndGateOutNo.MyReadOnly = False
        Me.fndGateOutNo.Name = "fndGateOutNo"
        Me.fndGateOutNo.Size = New System.Drawing.Size(215, 21)
        Me.fndGateOutNo.TabIndex = 1
        Me.fndGateOutNo.Value = ""
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(589, 7)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(73, 20)
        Me.btnclose.TabIndex = 1
        Me.btnclose.Text = "Close"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(12, 7)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(73, 20)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.gvManualSeal)
        Me.RadGroupBox3.HeaderText = "Manual Seal"
        Me.RadGroupBox3.Location = New System.Drawing.Point(3, 184)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Size = New System.Drawing.Size(668, 273)
        Me.RadGroupBox3.TabIndex = 218
        Me.RadGroupBox3.Text = "Manual Seal"
        '
        'gvManualSeal
        '
        Me.gvManualSeal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvManualSeal.Location = New System.Drawing.Point(2, 18)
        '
        'gvManualSeal
        '
        Me.gvManualSeal.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvManualSeal.Name = "gvManualSeal"
        Me.gvManualSeal.ShowHeaderCellButtons = True
        Me.gvManualSeal.Size = New System.Drawing.Size(664, 253)
        Me.gvManualSeal.TabIndex = 202
        Me.gvManualSeal.Text = "RadGridView1"
        '
        'FrmTankerOut
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(674, 512)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmTankerOut"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Tanker Out"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.lblGateEntryNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomerCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkGateOut, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTanker, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationBulk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDateAndTimeBulk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        CType(Me.gvManualSeal.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvManualSeal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblTanker As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents FndCustomer As common.UserControls.txtFinder
    Friend WithEvents LblCustomer As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents fndTanker As common.UserControls.txtFinder
    Friend WithEvents lblLocationBulk As common.Controls.MyLabel
    Friend WithEvents fndLocation As common.UserControls.txtFinder
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblDateAndTimeBulk As common.Controls.MyLabel
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndGateOutNo As common.UserControls.txtNavigator
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents fndGateEntryIn As common.UserControls.txtFinder
    Friend WithEvents chkGateOut As common.Controls.MyCheckBox
    Friend WithEvents lblGateEntryNo As common.Controls.MyLabel
    Friend WithEvents lblCustomerCode As common.Controls.MyLabel
    Friend WithEvents lblLocationCode As common.Controls.MyLabel
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvManualSeal As common.UserControls.MyRadGridView
End Class

