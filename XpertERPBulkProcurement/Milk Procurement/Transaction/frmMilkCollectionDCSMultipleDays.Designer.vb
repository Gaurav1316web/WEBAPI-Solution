<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMilkCollectionDCSMultipleDays
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
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.gv1 = New Telerik.WinControls.UI.RadGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtTotEnteredFATPer = New common.MyNumBox()
        Me.txtTotEnteredSNFPer = New common.MyNumBox()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.txtTankerNo = New common.UserControls.txtFinder()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.lblRoute = New common.Controls.MyLabel()
        Me.txtRoute = New common.UserControls.txtFinder()
        Me.txtTotPendingSNFPer = New common.Controls.MyLabel()
        Me.txtTotPendingFATPer = New common.Controls.MyLabel()
        Me.txtTotPendingFAT = New common.Controls.MyLabel()
        Me.txtTotPendingSNF = New common.Controls.MyLabel()
        Me.txtTotPendingQty = New common.Controls.MyLabel()
        Me.txtTotEnteredSNF = New common.MyNumBox()
        Me.txtTotReceivedSNF = New common.Controls.MyLabel()
        Me.txtTotReceivedFAT = New common.Controls.MyLabel()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.txtTotEnteredFAT = New common.MyNumBox()
        Me.txtTotReceivedQty = New common.Controls.MyLabel()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.txtTotEnteredQty = New common.MyNumBox()
        Me.MyLabel17 = New common.Controls.MyLabel()
        Me.MyLabel18 = New common.Controls.MyLabel()
        Me.txtVehicleNo = New common.Controls.MyTextBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.cboFATSNFType = New common.Controls.MyComboBox()
        Me.lblMCC = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtMCC = New common.UserControls.txtFinder()
        Me.UsLock1 = New common.usLock()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.txtDesc = New common.Controls.MyTextBox()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnHistory = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.txtTripNo = New common.MyNumBox()
        Me.MyLabel5 = New common.Controls.MyLabel()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.txtTotEnteredFATPer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotEnteredSNFPer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRoute, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotPendingSNFPer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotPendingFATPer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotPendingFAT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotPendingSNF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotPendingQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotEnteredSNF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotReceivedSNF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotReceivedFAT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotEnteredFAT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotReceivedQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotEnteredQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVehicleNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboFATSNFType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMCC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTripNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.gv1.MasterTemplate.EnableAlternatingRowColor = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.Size = New System.Drawing.Size(695, 224)
        Me.gv1.TabIndex = 2
        Me.gv1.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.txtTripNo)
        Me.Panel1.Controls.Add(Me.MyLabel5)
        Me.Panel1.Controls.Add(Me.txtTotEnteredFATPer)
        Me.Panel1.Controls.Add(Me.txtTotEnteredSNFPer)
        Me.Panel1.Controls.Add(Me.MyLabel6)
        Me.Panel1.Controls.Add(Me.txtTankerNo)
        Me.Panel1.Controls.Add(Me.MyLabel9)
        Me.Panel1.Controls.Add(Me.lblRoute)
        Me.Panel1.Controls.Add(Me.txtRoute)
        Me.Panel1.Controls.Add(Me.txtTotPendingSNFPer)
        Me.Panel1.Controls.Add(Me.txtTotPendingFATPer)
        Me.Panel1.Controls.Add(Me.txtTotPendingFAT)
        Me.Panel1.Controls.Add(Me.txtTotPendingSNF)
        Me.Panel1.Controls.Add(Me.txtTotPendingQty)
        Me.Panel1.Controls.Add(Me.txtTotEnteredSNF)
        Me.Panel1.Controls.Add(Me.txtTotReceivedSNF)
        Me.Panel1.Controls.Add(Me.txtTotReceivedFAT)
        Me.Panel1.Controls.Add(Me.MyLabel14)
        Me.Panel1.Controls.Add(Me.txtTotEnteredFAT)
        Me.Panel1.Controls.Add(Me.txtTotReceivedQty)
        Me.Panel1.Controls.Add(Me.MyLabel16)
        Me.Panel1.Controls.Add(Me.txtTotEnteredQty)
        Me.Panel1.Controls.Add(Me.MyLabel17)
        Me.Panel1.Controls.Add(Me.MyLabel18)
        Me.Panel1.Controls.Add(Me.txtVehicleNo)
        Me.Panel1.Controls.Add(Me.MyLabel1)
        Me.Panel1.Controls.Add(Me.cboFATSNFType)
        Me.Panel1.Controls.Add(Me.lblMCC)
        Me.Panel1.Controls.Add(Me.MyLabel3)
        Me.Panel1.Controls.Add(Me.txtMCC)
        Me.Panel1.Controls.Add(Me.UsLock1)
        Me.Panel1.Controls.Add(Me.RadLabel3)
        Me.Panel1.Controls.Add(Me.RadLabel4)
        Me.Panel1.Controls.Add(Me.RadLabel1)
        Me.Panel1.Controls.Add(Me.txtDocNo)
        Me.Panel1.Controls.Add(Me.txtDate)
        Me.Panel1.Controls.Add(Me.txtDesc)
        Me.Panel1.Controls.Add(Me.btnAddNew)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(695, 133)
        Me.Panel1.TabIndex = 0
        '
        'txtTotEnteredFATPer
        '
        Me.txtTotEnteredFATPer.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtTotEnteredFATPer.CalculationExpression = Nothing
        Me.txtTotEnteredFATPer.DecimalPlaces = 2
        Me.txtTotEnteredFATPer.FieldCode = Nothing
        Me.txtTotEnteredFATPer.FieldDesc = Nothing
        Me.txtTotEnteredFATPer.FieldMaxLength = 0
        Me.txtTotEnteredFATPer.FieldName = Nothing
        Me.txtTotEnteredFATPer.isCalculatedField = False
        Me.txtTotEnteredFATPer.IsSourceFromTable = False
        Me.txtTotEnteredFATPer.IsSourceFromValueList = False
        Me.txtTotEnteredFATPer.IsUnique = False
        Me.txtTotEnteredFATPer.Location = New System.Drawing.Point(298, 90)
        Me.txtTotEnteredFATPer.MendatroryField = True
        Me.txtTotEnteredFATPer.MyLinkLable1 = Nothing
        Me.txtTotEnteredFATPer.MyLinkLable2 = Nothing
        Me.txtTotEnteredFATPer.Name = "txtTotEnteredFATPer"
        Me.txtTotEnteredFATPer.ReferenceFieldDesc = Nothing
        Me.txtTotEnteredFATPer.ReferenceFieldName = Nothing
        Me.txtTotEnteredFATPer.ReferenceTableName = Nothing
        Me.txtTotEnteredFATPer.Size = New System.Drawing.Size(45, 20)
        Me.txtTotEnteredFATPer.TabIndex = 6
        Me.txtTotEnteredFATPer.Text = "0"
        Me.txtTotEnteredFATPer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTotEnteredFATPer.Value = 0R
        '
        'txtTotEnteredSNFPer
        '
        Me.txtTotEnteredSNFPer.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtTotEnteredSNFPer.CalculationExpression = Nothing
        Me.txtTotEnteredSNFPer.DecimalPlaces = 2
        Me.txtTotEnteredSNFPer.FieldCode = Nothing
        Me.txtTotEnteredSNFPer.FieldDesc = Nothing
        Me.txtTotEnteredSNFPer.FieldMaxLength = 0
        Me.txtTotEnteredSNFPer.FieldName = Nothing
        Me.txtTotEnteredSNFPer.isCalculatedField = False
        Me.txtTotEnteredSNFPer.IsSourceFromTable = False
        Me.txtTotEnteredSNFPer.IsSourceFromValueList = False
        Me.txtTotEnteredSNFPer.IsUnique = False
        Me.txtTotEnteredSNFPer.Location = New System.Drawing.Point(298, 111)
        Me.txtTotEnteredSNFPer.MendatroryField = True
        Me.txtTotEnteredSNFPer.MyLinkLable1 = Nothing
        Me.txtTotEnteredSNFPer.MyLinkLable2 = Nothing
        Me.txtTotEnteredSNFPer.Name = "txtTotEnteredSNFPer"
        Me.txtTotEnteredSNFPer.ReferenceFieldDesc = Nothing
        Me.txtTotEnteredSNFPer.ReferenceFieldName = Nothing
        Me.txtTotEnteredSNFPer.ReferenceTableName = Nothing
        Me.txtTotEnteredSNFPer.Size = New System.Drawing.Size(45, 20)
        Me.txtTotEnteredSNFPer.TabIndex = 8
        Me.txtTotEnteredSNFPer.Text = "0"
        Me.txtTotEnteredSNFPer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTotEnteredSNFPer.Value = 0R
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel6.Location = New System.Drawing.Point(6, 49)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(58, 18)
        Me.MyLabel6.TabIndex = 20
        Me.MyLabel6.Text = "Tanker No"
        '
        'txtTankerNo
        '
        Me.txtTankerNo.CalculationExpression = Nothing
        Me.txtTankerNo.FieldCode = Nothing
        Me.txtTankerNo.FieldDesc = Nothing
        Me.txtTankerNo.FieldMaxLength = 0
        Me.txtTankerNo.FieldName = Nothing
        Me.txtTankerNo.isCalculatedField = False
        Me.txtTankerNo.IsSourceFromTable = False
        Me.txtTankerNo.IsSourceFromValueList = False
        Me.txtTankerNo.IsUnique = False
        Me.txtTankerNo.Location = New System.Drawing.Point(85, 48)
        Me.txtTankerNo.MendatroryField = True
        Me.txtTankerNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTankerNo.MyLinkLable1 = Me.MyLabel6
        Me.txtTankerNo.MyLinkLable2 = Nothing
        Me.txtTankerNo.MyReadOnly = False
        Me.txtTankerNo.MyShowMasterFormButton = False
        Me.txtTankerNo.Name = "txtTankerNo"
        Me.txtTankerNo.ReferenceFieldDesc = Nothing
        Me.txtTankerNo.ReferenceFieldName = Nothing
        Me.txtTankerNo.ReferenceTableName = Nothing
        Me.txtTankerNo.Size = New System.Drawing.Size(210, 20)
        Me.txtTankerNo.TabIndex = 2
        Me.txtTankerNo.Value = ""
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel9.Location = New System.Drawing.Point(6, 27)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(36, 18)
        Me.MyLabel9.TabIndex = 21
        Me.MyLabel9.Text = "Route"
        '
        'lblRoute
        '
        Me.lblRoute.AutoSize = False
        Me.lblRoute.BorderVisible = True
        Me.lblRoute.FieldName = Nothing
        Me.lblRoute.Location = New System.Drawing.Point(298, 26)
        Me.lblRoute.Name = "lblRoute"
        Me.lblRoute.Size = New System.Drawing.Size(189, 20)
        Me.lblRoute.TabIndex = 69
        Me.lblRoute.TextWrap = False
        '
        'txtRoute
        '
        Me.txtRoute.CalculationExpression = Nothing
        Me.txtRoute.FieldCode = Nothing
        Me.txtRoute.FieldDesc = Nothing
        Me.txtRoute.FieldMaxLength = 0
        Me.txtRoute.FieldName = Nothing
        Me.txtRoute.isCalculatedField = False
        Me.txtRoute.IsSourceFromTable = False
        Me.txtRoute.IsSourceFromValueList = False
        Me.txtRoute.IsUnique = False
        Me.txtRoute.Location = New System.Drawing.Point(85, 26)
        Me.txtRoute.MendatroryField = True
        Me.txtRoute.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRoute.MyLinkLable1 = Me.MyLabel9
        Me.txtRoute.MyLinkLable2 = Nothing
        Me.txtRoute.MyReadOnly = False
        Me.txtRoute.MyShowMasterFormButton = False
        Me.txtRoute.Name = "txtRoute"
        Me.txtRoute.ReferenceFieldDesc = Nothing
        Me.txtRoute.ReferenceFieldName = Nothing
        Me.txtRoute.ReferenceTableName = Nothing
        Me.txtRoute.Size = New System.Drawing.Size(210, 20)
        Me.txtRoute.TabIndex = 1
        Me.txtRoute.Value = ""
        '
        'txtTotPendingSNFPer
        '
        Me.txtTotPendingSNFPer.AutoSize = False
        Me.txtTotPendingSNFPer.BorderVisible = True
        Me.txtTotPendingSNFPer.FieldName = Nothing
        Me.txtTotPendingSNFPer.Location = New System.Drawing.Point(484, 111)
        Me.txtTotPendingSNFPer.Name = "txtTotPendingSNFPer"
        Me.txtTotPendingSNFPer.Size = New System.Drawing.Size(48, 20)
        Me.txtTotPendingSNFPer.TabIndex = 14
        Me.txtTotPendingSNFPer.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.txtTotPendingSNFPer.TextWrap = False
        '
        'txtTotPendingFATPer
        '
        Me.txtTotPendingFATPer.AutoSize = False
        Me.txtTotPendingFATPer.BorderVisible = True
        Me.txtTotPendingFATPer.FieldName = Nothing
        Me.txtTotPendingFATPer.Location = New System.Drawing.Point(484, 90)
        Me.txtTotPendingFATPer.Name = "txtTotPendingFATPer"
        Me.txtTotPendingFATPer.Size = New System.Drawing.Size(48, 20)
        Me.txtTotPendingFATPer.TabIndex = 13
        Me.txtTotPendingFATPer.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.txtTotPendingFATPer.TextWrap = False
        '
        'txtTotPendingFAT
        '
        Me.txtTotPendingFAT.AutoSize = False
        Me.txtTotPendingFAT.BorderVisible = True
        Me.txtTotPendingFAT.FieldName = Nothing
        Me.txtTotPendingFAT.Location = New System.Drawing.Point(436, 90)
        Me.txtTotPendingFAT.Name = "txtTotPendingFAT"
        Me.txtTotPendingFAT.Size = New System.Drawing.Size(48, 20)
        Me.txtTotPendingFAT.TabIndex = 11
        Me.txtTotPendingFAT.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.txtTotPendingFAT.TextWrap = False
        '
        'txtTotPendingSNF
        '
        Me.txtTotPendingSNF.AutoSize = False
        Me.txtTotPendingSNF.BorderVisible = True
        Me.txtTotPendingSNF.FieldName = Nothing
        Me.txtTotPendingSNF.Location = New System.Drawing.Point(436, 111)
        Me.txtTotPendingSNF.Name = "txtTotPendingSNF"
        Me.txtTotPendingSNF.Size = New System.Drawing.Size(48, 20)
        Me.txtTotPendingSNF.TabIndex = 12
        Me.txtTotPendingSNF.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.txtTotPendingSNF.TextWrap = False
        '
        'txtTotPendingQty
        '
        Me.txtTotPendingQty.AutoSize = False
        Me.txtTotPendingQty.BorderVisible = True
        Me.txtTotPendingQty.FieldName = Nothing
        Me.txtTotPendingQty.Location = New System.Drawing.Point(195, 90)
        Me.txtTotPendingQty.Name = "txtTotPendingQty"
        Me.txtTotPendingQty.Size = New System.Drawing.Size(64, 20)
        Me.txtTotPendingQty.TabIndex = 15
        Me.txtTotPendingQty.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.txtTotPendingQty.TextWrap = False
        '
        'txtTotEnteredSNF
        '
        Me.txtTotEnteredSNF.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtTotEnteredSNF.CalculationExpression = Nothing
        Me.txtTotEnteredSNF.DecimalPlaces = 3
        Me.txtTotEnteredSNF.FieldCode = Nothing
        Me.txtTotEnteredSNF.FieldDesc = Nothing
        Me.txtTotEnteredSNF.FieldMaxLength = 0
        Me.txtTotEnteredSNF.FieldName = Nothing
        Me.txtTotEnteredSNF.isCalculatedField = False
        Me.txtTotEnteredSNF.IsSourceFromTable = False
        Me.txtTotEnteredSNF.IsSourceFromValueList = False
        Me.txtTotEnteredSNF.IsUnique = False
        Me.txtTotEnteredSNF.Location = New System.Drawing.Point(343, 111)
        Me.txtTotEnteredSNF.MendatroryField = True
        Me.txtTotEnteredSNF.MyLinkLable1 = Nothing
        Me.txtTotEnteredSNF.MyLinkLable2 = Nothing
        Me.txtTotEnteredSNF.Name = "txtTotEnteredSNF"
        Me.txtTotEnteredSNF.ReferenceFieldDesc = Nothing
        Me.txtTotEnteredSNF.ReferenceFieldName = Nothing
        Me.txtTotEnteredSNF.ReferenceTableName = Nothing
        Me.txtTotEnteredSNF.Size = New System.Drawing.Size(45, 20)
        Me.txtTotEnteredSNF.TabIndex = 9
        Me.txtTotEnteredSNF.Text = "0"
        Me.txtTotEnteredSNF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTotEnteredSNF.Value = 0R
        '
        'txtTotReceivedSNF
        '
        Me.txtTotReceivedSNF.AutoSize = False
        Me.txtTotReceivedSNF.BorderVisible = True
        Me.txtTotReceivedSNF.FieldName = Nothing
        Me.txtTotReceivedSNF.Location = New System.Drawing.Point(388, 111)
        Me.txtTotReceivedSNF.Name = "txtTotReceivedSNF"
        Me.txtTotReceivedSNF.Size = New System.Drawing.Size(48, 20)
        Me.txtTotReceivedSNF.TabIndex = 10
        Me.txtTotReceivedSNF.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.txtTotReceivedSNF.TextWrap = False
        '
        'txtTotReceivedFAT
        '
        Me.txtTotReceivedFAT.AutoSize = False
        Me.txtTotReceivedFAT.BorderVisible = True
        Me.txtTotReceivedFAT.FieldName = Nothing
        Me.txtTotReceivedFAT.Location = New System.Drawing.Point(388, 90)
        Me.txtTotReceivedFAT.Name = "txtTotReceivedFAT"
        Me.txtTotReceivedFAT.Size = New System.Drawing.Size(48, 20)
        Me.txtTotReceivedFAT.TabIndex = 9
        Me.txtTotReceivedFAT.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.txtTotReceivedFAT.TextWrap = False
        '
        'MyLabel14
        '
        Me.MyLabel14.FieldName = Nothing
        Me.MyLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel14.Location = New System.Drawing.Point(267, 113)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(29, 16)
        Me.MyLabel14.TabIndex = 74
        Me.MyLabel14.Text = "SNF"
        '
        'txtTotEnteredFAT
        '
        Me.txtTotEnteredFAT.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtTotEnteredFAT.CalculationExpression = Nothing
        Me.txtTotEnteredFAT.DecimalPlaces = 3
        Me.txtTotEnteredFAT.FieldCode = Nothing
        Me.txtTotEnteredFAT.FieldDesc = Nothing
        Me.txtTotEnteredFAT.FieldMaxLength = 0
        Me.txtTotEnteredFAT.FieldName = Nothing
        Me.txtTotEnteredFAT.isCalculatedField = False
        Me.txtTotEnteredFAT.IsSourceFromTable = False
        Me.txtTotEnteredFAT.IsSourceFromValueList = False
        Me.txtTotEnteredFAT.IsUnique = False
        Me.txtTotEnteredFAT.Location = New System.Drawing.Point(343, 90)
        Me.txtTotEnteredFAT.MendatroryField = True
        Me.txtTotEnteredFAT.MyLinkLable1 = Nothing
        Me.txtTotEnteredFAT.MyLinkLable2 = Nothing
        Me.txtTotEnteredFAT.Name = "txtTotEnteredFAT"
        Me.txtTotEnteredFAT.ReferenceFieldDesc = Nothing
        Me.txtTotEnteredFAT.ReferenceFieldName = Nothing
        Me.txtTotEnteredFAT.ReferenceTableName = Nothing
        Me.txtTotEnteredFAT.Size = New System.Drawing.Size(45, 20)
        Me.txtTotEnteredFAT.TabIndex = 7
        Me.txtTotEnteredFAT.Text = "0"
        Me.txtTotEnteredFAT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTotEnteredFAT.Value = 0R
        '
        'txtTotReceivedQty
        '
        Me.txtTotReceivedQty.AutoSize = False
        Me.txtTotReceivedQty.BorderVisible = True
        Me.txtTotReceivedQty.FieldName = Nothing
        Me.txtTotReceivedQty.Location = New System.Drawing.Point(131, 90)
        Me.txtTotReceivedQty.Name = "txtTotReceivedQty"
        Me.txtTotReceivedQty.Size = New System.Drawing.Size(64, 20)
        Me.txtTotReceivedQty.TabIndex = 16
        Me.txtTotReceivedQty.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.txtTotReceivedQty.TextWrap = False
        '
        'MyLabel16
        '
        Me.MyLabel16.FieldName = Nothing
        Me.MyLabel16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel16.Location = New System.Drawing.Point(267, 92)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(28, 16)
        Me.MyLabel16.TabIndex = 61
        Me.MyLabel16.Text = "FAT"
        '
        'txtTotEnteredQty
        '
        Me.txtTotEnteredQty.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtTotEnteredQty.CalculationExpression = Nothing
        Me.txtTotEnteredQty.DecimalPlaces = 3
        Me.txtTotEnteredQty.FieldCode = Nothing
        Me.txtTotEnteredQty.FieldDesc = Nothing
        Me.txtTotEnteredQty.FieldMaxLength = 0
        Me.txtTotEnteredQty.FieldName = Nothing
        Me.txtTotEnteredQty.isCalculatedField = False
        Me.txtTotEnteredQty.IsSourceFromTable = False
        Me.txtTotEnteredQty.IsSourceFromValueList = False
        Me.txtTotEnteredQty.IsUnique = False
        Me.txtTotEnteredQty.Location = New System.Drawing.Point(85, 90)
        Me.txtTotEnteredQty.MendatroryField = True
        Me.txtTotEnteredQty.MyLinkLable1 = Nothing
        Me.txtTotEnteredQty.MyLinkLable2 = Nothing
        Me.txtTotEnteredQty.Name = "txtTotEnteredQty"
        Me.txtTotEnteredQty.ReferenceFieldDesc = Nothing
        Me.txtTotEnteredQty.ReferenceFieldName = Nothing
        Me.txtTotEnteredQty.ReferenceTableName = Nothing
        Me.txtTotEnteredQty.Size = New System.Drawing.Size(45, 20)
        Me.txtTotEnteredQty.TabIndex = 5
        Me.txtTotEnteredQty.Text = "0"
        Me.txtTotEnteredQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTotEnteredQty.Value = 0R
        '
        'MyLabel17
        '
        Me.MyLabel17.FieldName = Nothing
        Me.MyLabel17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel17.Location = New System.Drawing.Point(6, 92)
        Me.MyLabel17.Name = "MyLabel17"
        Me.MyLabel17.Size = New System.Drawing.Size(51, 16)
        Me.MyLabel17.TabIndex = 18
        Me.MyLabel17.Text = "Total KG"
        '
        'MyLabel18
        '
        Me.MyLabel18.FieldName = Nothing
        Me.MyLabel18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel18.Location = New System.Drawing.Point(301, 50)
        Me.MyLabel18.Name = "MyLabel18"
        Me.MyLabel18.Size = New System.Drawing.Size(61, 16)
        Me.MyLabel18.TabIndex = 68
        Me.MyLabel18.Text = "Vehicle No"
        '
        'txtVehicleNo
        '
        Me.txtVehicleNo.CalculationExpression = Nothing
        Me.txtVehicleNo.FieldCode = Nothing
        Me.txtVehicleNo.FieldDesc = Nothing
        Me.txtVehicleNo.FieldMaxLength = 0
        Me.txtVehicleNo.FieldName = Nothing
        Me.txtVehicleNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVehicleNo.isCalculatedField = False
        Me.txtVehicleNo.IsSourceFromTable = False
        Me.txtVehicleNo.IsSourceFromValueList = False
        Me.txtVehicleNo.IsUnique = False
        Me.txtVehicleNo.Location = New System.Drawing.Point(368, 49)
        Me.txtVehicleNo.MaxLength = 200
        Me.txtVehicleNo.MendatroryField = True
        Me.txtVehicleNo.MyLinkLable1 = Me.MyLabel18
        Me.txtVehicleNo.MyLinkLable2 = Nothing
        Me.txtVehicleNo.Name = "txtVehicleNo"
        Me.txtVehicleNo.ReferenceFieldDesc = Nothing
        Me.txtVehicleNo.ReferenceFieldName = Nothing
        Me.txtVehicleNo.ReferenceTableName = Nothing
        Me.txtVehicleNo.Size = New System.Drawing.Size(119, 18)
        Me.txtVehicleNo.TabIndex = 3
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(497, 28)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(82, 16)
        Me.MyLabel1.TabIndex = 27
        Me.MyLabel1.Text = "FAT/SNF Type"
        '
        'cboFATSNFType
        '
        Me.cboFATSNFType.AutoCompleteDisplayMember = Nothing
        Me.cboFATSNFType.AutoCompleteValueMember = Nothing
        Me.cboFATSNFType.CalculationExpression = Nothing
        Me.cboFATSNFType.DropDownAnimationEnabled = True
        Me.cboFATSNFType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboFATSNFType.Enabled = False
        Me.cboFATSNFType.FieldCode = Nothing
        Me.cboFATSNFType.FieldDesc = Nothing
        Me.cboFATSNFType.FieldMaxLength = 0
        Me.cboFATSNFType.FieldName = Nothing
        Me.cboFATSNFType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboFATSNFType.isCalculatedField = False
        Me.cboFATSNFType.IsSourceFromTable = False
        Me.cboFATSNFType.IsSourceFromValueList = False
        Me.cboFATSNFType.IsUnique = False
        RadListDataItem1.Text = "M"
        RadListDataItem2.Text = "E"
        Me.cboFATSNFType.Items.Add(RadListDataItem1)
        Me.cboFATSNFType.Items.Add(RadListDataItem2)
        Me.cboFATSNFType.Location = New System.Drawing.Point(584, 27)
        Me.cboFATSNFType.MendatroryField = True
        Me.cboFATSNFType.MyLinkLable1 = Me.MyLabel1
        Me.cboFATSNFType.MyLinkLable2 = Nothing
        Me.cboFATSNFType.Name = "cboFATSNFType"
        Me.cboFATSNFType.ReferenceFieldDesc = Nothing
        Me.cboFATSNFType.ReferenceFieldName = Nothing
        Me.cboFATSNFType.ReferenceTableName = Nothing
        Me.cboFATSNFType.Size = New System.Drawing.Size(106, 18)
        Me.cboFATSNFType.TabIndex = 28
        '
        'lblMCC
        '
        Me.lblMCC.AutoSize = False
        Me.lblMCC.BorderVisible = True
        Me.lblMCC.FieldName = Nothing
        Me.lblMCC.Location = New System.Drawing.Point(298, 69)
        Me.lblMCC.Name = "lblMCC"
        Me.lblMCC.Size = New System.Drawing.Size(189, 20)
        Me.lblMCC.TabIndex = 15
        Me.lblMCC.TextWrap = False
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel3.Location = New System.Drawing.Point(6, 70)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(58, 18)
        Me.MyLabel3.TabIndex = 19
        Me.MyLabel3.Text = "BMC/MCC"
        '
        'txtMCC
        '
        Me.txtMCC.CalculationExpression = Nothing
        Me.txtMCC.FieldCode = Nothing
        Me.txtMCC.FieldDesc = Nothing
        Me.txtMCC.FieldMaxLength = 0
        Me.txtMCC.FieldName = Nothing
        Me.txtMCC.isCalculatedField = False
        Me.txtMCC.IsSourceFromTable = False
        Me.txtMCC.IsSourceFromValueList = False
        Me.txtMCC.IsUnique = False
        Me.txtMCC.Location = New System.Drawing.Point(85, 70)
        Me.txtMCC.MendatroryField = True
        Me.txtMCC.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMCC.MyLinkLable1 = Me.MyLabel3
        Me.txtMCC.MyLinkLable2 = Nothing
        Me.txtMCC.MyReadOnly = False
        Me.txtMCC.MyShowMasterFormButton = False
        Me.txtMCC.Name = "txtMCC"
        Me.txtMCC.ReferenceFieldDesc = Nothing
        Me.txtMCC.ReferenceFieldName = Nothing
        Me.txtMCC.ReferenceTableName = Nothing
        Me.txtMCC.Size = New System.Drawing.Size(210, 18)
        Me.txtMCC.TabIndex = 4
        Me.txtMCC.Value = ""
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(493, 5)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(86, 19)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 26
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(6, 113)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel3.TabIndex = 17
        Me.RadLabel3.Text = "Description"
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(361, 6)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 25
        Me.RadLabel4.Text = "Date"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(6, 6)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(75, 16)
        Me.RadLabel1.TabIndex = 22
        Me.RadLabel1.Text = "Document No"
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(85, 5)
        Me.txtDocNo.MendatroryField = False
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Me.RadLabel1
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 32767
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(250, 19)
        Me.txtDocNo.TabIndex = 23
        Me.txtDocNo.Value = ""
        '
        'txtDate
        '
        Me.txtDate.CalculationExpression = Nothing
        Me.txtDate.CustomFormat = "dd/MM/yyyy"
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
        Me.txtDate.Location = New System.Drawing.Point(397, 5)
        Me.txtDate.MendatroryField = True
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.RadLabel4
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(90, 18)
        Me.txtDate.TabIndex = 0
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'txtDesc
        '
        Me.txtDesc.CalculationExpression = Nothing
        Me.txtDesc.FieldCode = Nothing
        Me.txtDesc.FieldDesc = Nothing
        Me.txtDesc.FieldMaxLength = 0
        Me.txtDesc.FieldName = Nothing
        Me.txtDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc.isCalculatedField = False
        Me.txtDesc.IsSourceFromTable = False
        Me.txtDesc.IsSourceFromValueList = False
        Me.txtDesc.IsUnique = False
        Me.txtDesc.Location = New System.Drawing.Point(85, 112)
        Me.txtDesc.MaxLength = 200
        Me.txtDesc.MendatroryField = False
        Me.txtDesc.MyLinkLable1 = Me.RadLabel3
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ReferenceFieldDesc = Nothing
        Me.txtDesc.ReferenceFieldName = Nothing
        Me.txtDesc.ReferenceTableName = Nothing
        Me.txtDesc.Size = New System.Drawing.Size(174, 18)
        Me.txtDesc.TabIndex = 10
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.XpertERPBulkProcurement.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(335, 5)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 19)
        Me.btnAddNew.TabIndex = 24
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.gv1)
        Me.Panel2.Controls.Add(Me.Panel3)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 133)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(695, 252)
        Me.Panel2.TabIndex = 1
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.btnSave)
        Me.Panel3.Controls.Add(Me.btnClose)
        Me.Panel3.Controls.Add(Me.btnHistory)
        Me.Panel3.Controls.Add(Me.btnDelete)
        Me.Panel3.Controls.Add(Me.btnPost)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Location = New System.Drawing.Point(0, 224)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(695, 28)
        Me.Panel3.TabIndex = 40
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(3, 3)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(72, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(620, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(72, 22)
        Me.btnClose.TabIndex = 6
        Me.btnClose.Text = "Close"
        '
        'btnHistory
        '
        Me.btnHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnHistory.Location = New System.Drawing.Point(225, 3)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Size = New System.Drawing.Size(72, 22)
        Me.btnHistory.TabIndex = 39
        Me.btnHistory.Text = "&History"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(77, 3)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(72, 22)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(151, 3)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(72, 22)
        Me.btnPost.TabIndex = 2
        Me.btnPost.Text = "Post"
        Me.btnPost.Visible = False
        '
        'txtTripNo
        '
        Me.txtTripNo.BackColor = System.Drawing.Color.White
        Me.txtTripNo.CalculationExpression = Nothing
        Me.txtTripNo.DecimalPlaces = 2
        Me.txtTripNo.FieldCode = Nothing
        Me.txtTripNo.FieldDesc = Nothing
        Me.txtTripNo.FieldMaxLength = 0
        Me.txtTripNo.FieldName = Nothing
        Me.txtTripNo.isCalculatedField = False
        Me.txtTripNo.IsSourceFromTable = False
        Me.txtTripNo.IsSourceFromValueList = False
        Me.txtTripNo.IsUnique = False
        Me.txtTripNo.Location = New System.Drawing.Point(584, 48)
        Me.txtTripNo.MendatroryField = False
        Me.txtTripNo.MyLinkLable1 = Nothing
        Me.txtTripNo.MyLinkLable2 = Nothing
        Me.txtTripNo.Name = "txtTripNo"
        Me.txtTripNo.ReferenceFieldDesc = Nothing
        Me.txtTripNo.ReferenceFieldName = Nothing
        Me.txtTripNo.ReferenceTableName = Nothing
        Me.txtTripNo.Size = New System.Drawing.Size(106, 20)
        Me.txtTripNo.TabIndex = 1425
        Me.txtTripNo.Text = "1"
        Me.txtTripNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTripNo.Value = 1.0R
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(497, 50)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(43, 16)
        Me.MyLabel5.TabIndex = 1424
        Me.MyLabel5.Text = "Trip No"
        '
        'frmMilkCollectionDCSMultipleDays
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(695, 385)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmMilkCollectionDCSMultipleDays"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DCS Milk Collection"
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.txtTotEnteredFATPer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotEnteredSNFPer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRoute, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotPendingSNFPer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotPendingFATPer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotPendingFAT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotPendingSNF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotPendingQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotEnteredSNF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotReceivedSNF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotReceivedFAT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotEnteredFAT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotReceivedQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotEnteredQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVehicleNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboFATSNFType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMCC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTripNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents gv1 As Telerik.WinControls.UI.RadGridView
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtDesc As common.Controls.MyTextBox
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtMCC As common.UserControls.txtFinder
    Friend WithEvents lblMCC As common.Controls.MyLabel
    Friend WithEvents btnHistory As RadButton
    Friend WithEvents Panel3 As Panel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents cboFATSNFType As common.Controls.MyComboBox
    Friend WithEvents txtTotEnteredFATPer As common.MyNumBox
    Friend WithEvents txtTotEnteredSNFPer As common.MyNumBox
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents txtTankerNo As common.UserControls.txtFinder
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents lblRoute As common.Controls.MyLabel
    Friend WithEvents txtRoute As common.UserControls.txtFinder
    Friend WithEvents txtTotPendingSNFPer As common.Controls.MyLabel
    Friend WithEvents txtTotPendingFATPer As common.Controls.MyLabel
    Friend WithEvents txtTotPendingFAT As common.Controls.MyLabel
    Friend WithEvents txtTotPendingSNF As common.Controls.MyLabel
    Friend WithEvents txtTotPendingQty As common.Controls.MyLabel
    Friend WithEvents txtTotEnteredSNF As common.MyNumBox
    Friend WithEvents txtTotReceivedSNF As common.Controls.MyLabel
    Friend WithEvents txtTotReceivedFAT As common.Controls.MyLabel
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents txtTotEnteredFAT As common.MyNumBox
    Friend WithEvents txtTotReceivedQty As common.Controls.MyLabel
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents txtTotEnteredQty As common.MyNumBox
    Friend WithEvents MyLabel17 As common.Controls.MyLabel
    Friend WithEvents MyLabel18 As common.Controls.MyLabel
    Friend WithEvents txtVehicleNo As common.Controls.MyTextBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents txtTripNo As common.MyNumBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
End Class

