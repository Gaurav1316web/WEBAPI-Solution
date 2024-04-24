Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmBullMovement
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBullMovement))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.txtPeriod = New common.Controls.MyDateTimePicker()
        Me.lbleMovementType = New common.Controls.MyLabel()
        Me.txtMovementType = New common.UserControls.txtFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.LblBullName = New common.Controls.MyLabel()
        Me.LblShed = New common.Controls.MyLabel()
        Me.lblPeriod = New common.Controls.MyLabel()
        Me.TxtShed = New common.UserControls.txtFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtBullCode = New common.UserControls.txtFinder()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.lblCode = New common.Controls.MyLabel()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.btnReverse = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.txtPeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbleMovementType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblBullName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblShed, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtPeriod)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lbleMovementType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtMovementType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.LblShed)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblPeriod)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TxtShed)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.LblBullName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtBullCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnnew)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReverse)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(853, 424)
        Me.SplitContainer1.SplitterDistance = 352
        Me.SplitContainer1.TabIndex = 0
        '
        'txtPeriod
        '
        Me.txtPeriod.CalculationExpression = Nothing
        Me.txtPeriod.CustomFormat = "dd/MM/yyyy"
        Me.txtPeriod.FieldCode = Nothing
        Me.txtPeriod.FieldDesc = Nothing
        Me.txtPeriod.FieldMaxLength = 0
        Me.txtPeriod.FieldName = Nothing
        Me.txtPeriod.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPeriod.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtPeriod.isCalculatedField = False
        Me.txtPeriod.IsSourceFromTable = False
        Me.txtPeriod.IsSourceFromValueList = False
        Me.txtPeriod.IsUnique = False
        Me.txtPeriod.Location = New System.Drawing.Point(151, 126)
        Me.txtPeriod.MendatroryField = False
        Me.txtPeriod.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtPeriod.MyLinkLable1 = Nothing
        Me.txtPeriod.MyLinkLable2 = Nothing
        Me.txtPeriod.Name = "txtPeriod"
        Me.txtPeriod.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtPeriod.ReferenceFieldDesc = Nothing
        Me.txtPeriod.ReferenceFieldName = Nothing
        Me.txtPeriod.ReferenceTableName = Nothing
        Me.txtPeriod.Size = New System.Drawing.Size(126, 18)
        Me.txtPeriod.TabIndex = 452
        Me.txtPeriod.TabStop = False
        Me.txtPeriod.Text = "07/02/2024"
        Me.txtPeriod.Value = New Date(2024, 2, 7, 0, 0, 0, 0)
        '
        'lbleMovementType
        '
        Me.lbleMovementType.AutoSize = False
        Me.lbleMovementType.BorderVisible = True
        Me.lbleMovementType.FieldName = Nothing
        Me.lbleMovementType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbleMovementType.Location = New System.Drawing.Point(331, 99)
        Me.lbleMovementType.Name = "lbleMovementType"
        Me.lbleMovementType.Size = New System.Drawing.Size(307, 19)
        Me.lbleMovementType.TabIndex = 450
        Me.lbleMovementType.TextWrap = False
        '
        'txtMovementType
        '
        Me.txtMovementType.CalculationExpression = Nothing
        Me.txtMovementType.FieldCode = Nothing
        Me.txtMovementType.FieldDesc = Nothing
        Me.txtMovementType.FieldMaxLength = 0
        Me.txtMovementType.FieldName = Nothing
        Me.txtMovementType.isCalculatedField = False
        Me.txtMovementType.IsSourceFromTable = False
        Me.txtMovementType.IsSourceFromValueList = False
        Me.txtMovementType.IsUnique = False
        Me.txtMovementType.Location = New System.Drawing.Point(151, 99)
        Me.txtMovementType.MendatroryField = True
        Me.txtMovementType.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMovementType.MyLinkLable1 = Me.MyLabel2
        Me.txtMovementType.MyLinkLable2 = Me.LblBullName
        Me.txtMovementType.MyReadOnly = False
        Me.txtMovementType.MyShowMasterFormButton = False
        Me.txtMovementType.Name = "txtMovementType"
        Me.txtMovementType.ReferenceFieldDesc = Nothing
        Me.txtMovementType.ReferenceFieldName = Nothing
        Me.txtMovementType.ReferenceTableName = Nothing
        Me.txtMovementType.Size = New System.Drawing.Size(174, 20)
        Me.txtMovementType.TabIndex = 449
        Me.txtMovementType.Value = ""
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(21, 80)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(33, 16)
        Me.MyLabel2.TabIndex = 93
        Me.MyLabel2.Text = "Shed"
        '
        'LblBullName
        '
        Me.LblBullName.AutoSize = False
        Me.LblBullName.BorderVisible = True
        Me.LblBullName.FieldName = Nothing
        Me.LblBullName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBullName.Location = New System.Drawing.Point(331, 55)
        Me.LblBullName.Name = "LblBullName"
        Me.LblBullName.Size = New System.Drawing.Size(307, 19)
        Me.LblBullName.TabIndex = 89
        Me.LblBullName.TextWrap = False
        '
        'LblShed
        '
        Me.LblShed.AutoSize = False
        Me.LblShed.BorderVisible = True
        Me.LblShed.FieldName = Nothing
        Me.LblShed.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblShed.Location = New System.Drawing.Point(331, 76)
        Me.LblShed.Name = "LblShed"
        Me.LblShed.Size = New System.Drawing.Size(307, 19)
        Me.LblShed.TabIndex = 448
        Me.LblShed.TextWrap = False
        '
        'lblPeriod
        '
        Me.lblPeriod.FieldName = Nothing
        Me.lblPeriod.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPeriod.Location = New System.Drawing.Point(21, 126)
        Me.lblPeriod.Name = "lblPeriod"
        Me.lblPeriod.Size = New System.Drawing.Size(39, 16)
        Me.lblPeriod.TabIndex = 446
        Me.lblPeriod.Text = "Period"
        '
        'TxtShed
        '
        Me.TxtShed.CalculationExpression = Nothing
        Me.TxtShed.FieldCode = Nothing
        Me.TxtShed.FieldDesc = Nothing
        Me.TxtShed.FieldMaxLength = 0
        Me.TxtShed.FieldName = Nothing
        Me.TxtShed.isCalculatedField = False
        Me.TxtShed.IsSourceFromTable = False
        Me.TxtShed.IsSourceFromValueList = False
        Me.TxtShed.IsUnique = False
        Me.TxtShed.Location = New System.Drawing.Point(151, 76)
        Me.TxtShed.MendatroryField = True
        Me.TxtShed.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtShed.MyLinkLable1 = Me.MyLabel2
        Me.TxtShed.MyLinkLable2 = Me.LblBullName
        Me.TxtShed.MyReadOnly = False
        Me.TxtShed.MyShowMasterFormButton = False
        Me.TxtShed.Name = "TxtShed"
        Me.TxtShed.ReferenceFieldDesc = Nothing
        Me.TxtShed.ReferenceFieldName = Nothing
        Me.TxtShed.ReferenceTableName = Nothing
        Me.TxtShed.Size = New System.Drawing.Size(174, 20)
        Me.TxtShed.TabIndex = 92
        Me.TxtShed.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(21, 58)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(25, 16)
        Me.MyLabel1.TabIndex = 88
        Me.MyLabel1.Text = "Bull"
        '
        'txtBullCode
        '
        Me.txtBullCode.CalculationExpression = Nothing
        Me.txtBullCode.FieldCode = Nothing
        Me.txtBullCode.FieldDesc = Nothing
        Me.txtBullCode.FieldMaxLength = 0
        Me.txtBullCode.FieldName = Nothing
        Me.txtBullCode.isCalculatedField = False
        Me.txtBullCode.IsSourceFromTable = False
        Me.txtBullCode.IsSourceFromValueList = False
        Me.txtBullCode.IsUnique = False
        Me.txtBullCode.Location = New System.Drawing.Point(151, 54)
        Me.txtBullCode.MendatroryField = True
        Me.txtBullCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBullCode.MyLinkLable1 = Me.MyLabel1
        Me.txtBullCode.MyLinkLable2 = Me.LblBullName
        Me.txtBullCode.MyReadOnly = False
        Me.txtBullCode.MyShowMasterFormButton = False
        Me.txtBullCode.Name = "txtBullCode"
        Me.txtBullCode.ReferenceFieldDesc = Nothing
        Me.txtBullCode.ReferenceFieldName = Nothing
        Me.txtBullCode.ReferenceTableName = Nothing
        Me.txtBullCode.Size = New System.Drawing.Size(174, 20)
        Me.txtBullCode.TabIndex = 87
        Me.txtBullCode.Value = ""
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(477, 32)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 85
        Me.RadLabel4.Text = "Date"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(644, 30)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 86
        '
        'txtDate
        '
        Me.txtDate.CalculationExpression = Nothing
        Me.txtDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtDate.FieldCode = Nothing
        Me.txtDate.FieldDesc = Nothing
        Me.txtDate.FieldMaxLength = 0
        Me.txtDate.FieldName = Nothing
        Me.txtDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDate.isCalculatedField = False
        Me.txtDate.IsSourceFromTable = False
        Me.txtDate.IsSourceFromValueList = False
        Me.txtDate.IsUnique = False
        Me.txtDate.Location = New System.Drawing.Point(511, 31)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.RadLabel4
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(127, 18)
        Me.txtDate.TabIndex = 84
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "23/04/2024 12:00 AM"
        Me.txtDate.Value = New Date(2024, 4, 23, 0, 0, 0, 0)
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(853, 20)
        Me.RadMenu1.TabIndex = 82
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2, Me.RadMenuItem4})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        Me.RadMenuItem1.UseCompatibleTextRendering = False
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Export"
        Me.RadMenuItem2.UseCompatibleTextRendering = False
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Import"
        Me.RadMenuItem4.UseCompatibleTextRendering = False
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(151, 29)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 30
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(307, 21)
        Me.txtCode.TabIndex = 77
        Me.txtCode.Value = ""
        '
        'lblCode
        '
        Me.lblCode.FieldName = Nothing
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblCode.Location = New System.Drawing.Point(21, 36)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(34, 16)
        Me.lblCode.TabIndex = 81
        Me.lblCode.Text = "Code"
        '
        'btnnew
        '
        Me.btnnew.Image = CType(resources.GetObject("btnnew.Image"), System.Drawing.Image)
        Me.btnnew.Location = New System.Drawing.Point(458, 29)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(15, 21)
        Me.btnnew.TabIndex = 83
        '
        'btnReverse
        '
        Me.btnReverse.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReverse.Location = New System.Drawing.Point(239, 40)
        Me.btnReverse.Name = "btnReverse"
        Me.btnReverse.Size = New System.Drawing.Size(69, 20)
        Me.btnReverse.TabIndex = 389
        Me.btnReverse.Text = "Reverse"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Location = New System.Drawing.Point(164, 40)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(69, 20)
        Me.btnPost.TabIndex = 21
        Me.btnPost.Text = "Post"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(17, 40)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 18
        Me.btnsave.Text = "Save"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(92, 40)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 19
        Me.btndelete.Text = "Delete"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(775, 40)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 20
        Me.btnclose.Text = "Close"
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(21, 102)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(109, 16)
        Me.MyLabel4.TabIndex = 454
        Me.MyLabel4.Text = "Bull Movement Type"
        '
        'frmBullMovement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(853, 424)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmBullMovement"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "frmBullMovement"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.txtPeriod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbleMovementType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblBullName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblShed, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPeriod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem4 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents LblBullName As common.Controls.MyLabel
    Friend WithEvents txtBullCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents TxtShed As common.UserControls.txtFinder
    Friend WithEvents lblPeriod As common.Controls.MyLabel
    Friend WithEvents LblShed As common.Controls.MyLabel
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReverse As Telerik.WinControls.UI.RadButton
    Friend WithEvents lbleMovementType As common.Controls.MyLabel
    Friend WithEvents txtMovementType As common.UserControls.txtFinder
    Friend WithEvents txtPeriod As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
End Class
