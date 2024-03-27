<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSecondarySettingForQC
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
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.FndQCNo = New common.UserControls.txtFinder()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.lblPending = New common.usLock()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.GrpControlSample = New System.Windows.Forms.GroupBox()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.txtRcptControlSampleSNF = New common.Controls.MyTextBox()
        Me.txtRcptControlSampleFAT = New common.Controls.MyTextBox()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.txtDispControlSampleSNF = New common.Controls.MyTextBox()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.txtDispControlSampleFAT = New common.Controls.MyTextBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.fndTankerNo = New common.UserControls.txtFinder()
        Me.TxtDeductionAmount = New common.MyNumBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lblQcAcceptedOrRejected = New common.Controls.MyLabel()
        Me.dtpWeighmentDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtDipValue = New common.Controls.MyTextBox()
        Me.lblGateEntryNO = New common.Controls.MyLabel()
        Me.lblDipValue = New common.Controls.MyLabel()
        Me.lblChallanDate = New common.Controls.MyLabel()
        Me.fndLocation = New common.UserControls.txtFinder()
        Me.lblChallanNo = New common.Controls.MyLabel()
        Me.fndVendor = New common.UserControls.txtFinder()
        Me.lblVendor = New common.Controls.MyLabel()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.txtWeighmentNo = New common.Controls.MyTextBox()
        Me.lblTankerNo = New common.Controls.MyLabel()
        Me.dtpChallanDate = New common.Controls.MyDateTimePicker()
        Me.lblDateAndTime = New common.Controls.MyLabel()
        Me.txtChallanNo = New common.Controls.MyTextBox()
        Me.lblLocationName = New common.Controls.MyLabel()
        Me.lblQcInDateAndTime = New common.Controls.MyLabel()
        Me.dtpGateEntryDateTime = New common.Controls.MyDateTimePicker()
        Me.lblVendorName = New common.Controls.MyLabel()
        Me.lblStatus = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.dtpQCInDateTime = New common.Controls.MyDateTimePicker()
        Me.dtpQCOutDateTime = New common.Controls.MyDateTimePicker()
        Me.lblQCOutDateAndTime = New common.Controls.MyLabel()
        Me.fndGateEntryNo = New common.UserControls.txtFinder()
        Me.lblStatusValue = New common.Controls.MyLabel()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.btnReverse = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpControlSample.SuspendLayout()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRcptControlSampleSNF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRcptControlSampleFAT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDispControlSampleSNF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDispControlSampleFAT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtDeductionAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblQcAcceptedOrRejected, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpWeighmentDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDipValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGateEntryNO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDipValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblChallanDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblChallanNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtWeighmentNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTankerNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpChallanDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDateAndTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtChallanNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblQcInDateAndTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpGateEntryDateTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendorName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpQCInDateTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpQCOutDateTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblQCOutDateAndTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblStatusValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReverse)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(1011, 529)
        Me.SplitContainer1.SplitterDistance = 492
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1011, 492)
        Me.RadPageView1.TabIndex = 1
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.FndQCNo)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.lblPending)
        Me.RadPageViewPage1.Controls.Add(Me.txtDate)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.txtDocNo)
        Me.RadPageViewPage1.Controls.Add(Me.btnAddNew)
        Me.RadPageViewPage1.Controls.Add(Me.GrpControlSample)
        Me.RadPageViewPage1.Controls.Add(Me.fndTankerNo)
        Me.RadPageViewPage1.Controls.Add(Me.TxtDeductionAmount)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.lblQcAcceptedOrRejected)
        Me.RadPageViewPage1.Controls.Add(Me.dtpWeighmentDate)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.txtDipValue)
        Me.RadPageViewPage1.Controls.Add(Me.lblGateEntryNO)
        Me.RadPageViewPage1.Controls.Add(Me.lblDipValue)
        Me.RadPageViewPage1.Controls.Add(Me.lblChallanDate)
        Me.RadPageViewPage1.Controls.Add(Me.fndLocation)
        Me.RadPageViewPage1.Controls.Add(Me.lblChallanNo)
        Me.RadPageViewPage1.Controls.Add(Me.fndVendor)
        Me.RadPageViewPage1.Controls.Add(Me.lblVendor)
        Me.RadPageViewPage1.Controls.Add(Me.lblLocation)
        Me.RadPageViewPage1.Controls.Add(Me.txtWeighmentNo)
        Me.RadPageViewPage1.Controls.Add(Me.lblTankerNo)
        Me.RadPageViewPage1.Controls.Add(Me.dtpChallanDate)
        Me.RadPageViewPage1.Controls.Add(Me.lblDateAndTime)
        Me.RadPageViewPage1.Controls.Add(Me.txtChallanNo)
        Me.RadPageViewPage1.Controls.Add(Me.lblLocationName)
        Me.RadPageViewPage1.Controls.Add(Me.lblQcInDateAndTime)
        Me.RadPageViewPage1.Controls.Add(Me.dtpGateEntryDateTime)
        Me.RadPageViewPage1.Controls.Add(Me.lblVendorName)
        Me.RadPageViewPage1.Controls.Add(Me.lblStatus)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.dtpQCInDateTime)
        Me.RadPageViewPage1.Controls.Add(Me.dtpQCOutDateTime)
        Me.RadPageViewPage1.Controls.Add(Me.lblQCOutDateAndTime)
        Me.RadPageViewPage1.Controls.Add(Me.fndGateEntryNo)
        Me.RadPageViewPage1.Controls.Add(Me.lblStatusValue)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(149.0!, 26.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(990, 446)
        Me.RadPageViewPage1.Text = "Secondary Setting For QC"
        '
        'FndQCNo
        '
        Me.FndQCNo.CalculationExpression = Nothing
        Me.FndQCNo.FieldCode = Nothing
        Me.FndQCNo.FieldDesc = Nothing
        Me.FndQCNo.FieldMaxLength = 0
        Me.FndQCNo.FieldName = Nothing
        Me.FndQCNo.isCalculatedField = False
        Me.FndQCNo.IsSourceFromTable = False
        Me.FndQCNo.IsSourceFromValueList = False
        Me.FndQCNo.IsUnique = False
        Me.FndQCNo.Location = New System.Drawing.Point(133, 25)
        Me.FndQCNo.MendatroryField = True
        Me.FndQCNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndQCNo.MyLinkLable1 = Nothing
        Me.FndQCNo.MyLinkLable2 = Nothing
        Me.FndQCNo.MyReadOnly = False
        Me.FndQCNo.MyShowMasterFormButton = False
        Me.FndQCNo.Name = "FndQCNo"
        Me.FndQCNo.ReferenceFieldDesc = Nothing
        Me.FndQCNo.ReferenceFieldName = Nothing
        Me.FndQCNo.ReferenceTableName = Nothing
        Me.FndQCNo.Size = New System.Drawing.Size(190, 20)
        Me.FndQCNo.TabIndex = 351
        Me.FndQCNo.Value = ""
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(13, 27)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(41, 16)
        Me.MyLabel5.TabIndex = 350
        Me.MyLabel5.Text = "QC No"
        '
        'lblPending
        '
        Me.lblPending.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lblPending.Location = New System.Drawing.Point(591, 0)
        Me.lblPending.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPending.Name = "lblPending"
        Me.lblPending.Size = New System.Drawing.Size(92, 22)
        Me.lblPending.Status = common.ERPTransactionStatus.Pending
        Me.lblPending.TabIndex = 349
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
        Me.txtDate.Location = New System.Drawing.Point(446, 2)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.RadLabel4
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(126, 18)
        Me.txtDate.TabIndex = 345
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011 11:29 AM"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(415, 3)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 348
        Me.RadLabel4.Text = "Date"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(13, 3)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(75, 16)
        Me.RadLabel1.TabIndex = 347
        Me.RadLabel1.Text = "Document No"
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(133, 1)
        Me.txtDocNo.MendatroryField = False
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Me.RadLabel1
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 30
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(252, 20)
        Me.txtDocNo.TabIndex = 344
        Me.txtDocNo.Value = ""
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.XpertERPBulkProcurement.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(387, 1)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 21)
        Me.btnAddNew.TabIndex = 346
        '
        'GrpControlSample
        '
        Me.GrpControlSample.Controls.Add(Me.MyLabel12)
        Me.GrpControlSample.Controls.Add(Me.txtRcptControlSampleSNF)
        Me.GrpControlSample.Controls.Add(Me.txtRcptControlSampleFAT)
        Me.GrpControlSample.Controls.Add(Me.MyLabel13)
        Me.GrpControlSample.Controls.Add(Me.txtDispControlSampleSNF)
        Me.GrpControlSample.Controls.Add(Me.MyLabel11)
        Me.GrpControlSample.Controls.Add(Me.txtDispControlSampleFAT)
        Me.GrpControlSample.Controls.Add(Me.MyLabel4)
        Me.GrpControlSample.Location = New System.Drawing.Point(13, 209)
        Me.GrpControlSample.Name = "GrpControlSample"
        Me.GrpControlSample.Size = New System.Drawing.Size(484, 59)
        Me.GrpControlSample.TabIndex = 343
        Me.GrpControlSample.TabStop = False
        Me.GrpControlSample.Text = "Control Sample"
        Me.GrpControlSample.Visible = False
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Location = New System.Drawing.Point(243, 34)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(156, 18)
        Me.MyLabel12.TabIndex = 367
        Me.MyLabel12.Text = "Receipt Control Sample SNF%"
        '
        'txtRcptControlSampleSNF
        '
        Me.txtRcptControlSampleSNF.CalculationExpression = Nothing
        Me.txtRcptControlSampleSNF.FieldCode = Nothing
        Me.txtRcptControlSampleSNF.FieldDesc = Nothing
        Me.txtRcptControlSampleSNF.FieldMaxLength = 0
        Me.txtRcptControlSampleSNF.FieldName = Nothing
        Me.txtRcptControlSampleSNF.isCalculatedField = False
        Me.txtRcptControlSampleSNF.IsSourceFromTable = False
        Me.txtRcptControlSampleSNF.IsSourceFromValueList = False
        Me.txtRcptControlSampleSNF.IsUnique = False
        Me.txtRcptControlSampleSNF.Location = New System.Drawing.Point(408, 35)
        Me.txtRcptControlSampleSNF.MaxLength = 50
        Me.txtRcptControlSampleSNF.MendatroryField = False
        Me.txtRcptControlSampleSNF.MyLinkLable1 = Nothing
        Me.txtRcptControlSampleSNF.MyLinkLable2 = Nothing
        Me.txtRcptControlSampleSNF.Name = "txtRcptControlSampleSNF"
        Me.txtRcptControlSampleSNF.ReferenceFieldDesc = Nothing
        Me.txtRcptControlSampleSNF.ReferenceFieldName = Nothing
        Me.txtRcptControlSampleSNF.ReferenceTableName = Nothing
        Me.txtRcptControlSampleSNF.Size = New System.Drawing.Size(63, 20)
        Me.txtRcptControlSampleSNF.TabIndex = 366
        '
        'txtRcptControlSampleFAT
        '
        Me.txtRcptControlSampleFAT.CalculationExpression = Nothing
        Me.txtRcptControlSampleFAT.FieldCode = Nothing
        Me.txtRcptControlSampleFAT.FieldDesc = Nothing
        Me.txtRcptControlSampleFAT.FieldMaxLength = 0
        Me.txtRcptControlSampleFAT.FieldName = Nothing
        Me.txtRcptControlSampleFAT.isCalculatedField = False
        Me.txtRcptControlSampleFAT.IsSourceFromTable = False
        Me.txtRcptControlSampleFAT.IsSourceFromValueList = False
        Me.txtRcptControlSampleFAT.IsUnique = False
        Me.txtRcptControlSampleFAT.Location = New System.Drawing.Point(174, 35)
        Me.txtRcptControlSampleFAT.MaxLength = 50
        Me.txtRcptControlSampleFAT.MendatroryField = False
        Me.txtRcptControlSampleFAT.MyLinkLable1 = Nothing
        Me.txtRcptControlSampleFAT.MyLinkLable2 = Nothing
        Me.txtRcptControlSampleFAT.Name = "txtRcptControlSampleFAT"
        Me.txtRcptControlSampleFAT.ReferenceFieldDesc = Nothing
        Me.txtRcptControlSampleFAT.ReferenceFieldName = Nothing
        Me.txtRcptControlSampleFAT.ReferenceTableName = Nothing
        Me.txtRcptControlSampleFAT.Size = New System.Drawing.Size(63, 20)
        Me.txtRcptControlSampleFAT.TabIndex = 364
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Location = New System.Drawing.Point(7, 34)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(155, 18)
        Me.MyLabel13.TabIndex = 365
        Me.MyLabel13.Text = "Receipt Control Sample FAT%"
        '
        'txtDispControlSampleSNF
        '
        Me.txtDispControlSampleSNF.CalculationExpression = Nothing
        Me.txtDispControlSampleSNF.FieldCode = Nothing
        Me.txtDispControlSampleSNF.FieldDesc = Nothing
        Me.txtDispControlSampleSNF.FieldMaxLength = 0
        Me.txtDispControlSampleSNF.FieldName = Nothing
        Me.txtDispControlSampleSNF.isCalculatedField = False
        Me.txtDispControlSampleSNF.IsSourceFromTable = False
        Me.txtDispControlSampleSNF.IsSourceFromValueList = False
        Me.txtDispControlSampleSNF.IsUnique = False
        Me.txtDispControlSampleSNF.Location = New System.Drawing.Point(408, 12)
        Me.txtDispControlSampleSNF.MaxLength = 50
        Me.txtDispControlSampleSNF.MendatroryField = False
        Me.txtDispControlSampleSNF.MyLinkLable1 = Nothing
        Me.txtDispControlSampleSNF.MyLinkLable2 = Nothing
        Me.txtDispControlSampleSNF.Name = "txtDispControlSampleSNF"
        Me.txtDispControlSampleSNF.ReadOnly = True
        Me.txtDispControlSampleSNF.ReferenceFieldDesc = Nothing
        Me.txtDispControlSampleSNF.ReferenceFieldName = Nothing
        Me.txtDispControlSampleSNF.ReferenceTableName = Nothing
        Me.txtDispControlSampleSNF.Size = New System.Drawing.Size(63, 20)
        Me.txtDispControlSampleSNF.TabIndex = 362
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Location = New System.Drawing.Point(241, 12)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(162, 18)
        Me.MyLabel11.TabIndex = 363
        Me.MyLabel11.Text = "Dispatch Control Sample SNF%"
        '
        'txtDispControlSampleFAT
        '
        Me.txtDispControlSampleFAT.CalculationExpression = Nothing
        Me.txtDispControlSampleFAT.FieldCode = Nothing
        Me.txtDispControlSampleFAT.FieldDesc = Nothing
        Me.txtDispControlSampleFAT.FieldMaxLength = 0
        Me.txtDispControlSampleFAT.FieldName = Nothing
        Me.txtDispControlSampleFAT.isCalculatedField = False
        Me.txtDispControlSampleFAT.IsSourceFromTable = False
        Me.txtDispControlSampleFAT.IsSourceFromValueList = False
        Me.txtDispControlSampleFAT.IsUnique = False
        Me.txtDispControlSampleFAT.Location = New System.Drawing.Point(174, 12)
        Me.txtDispControlSampleFAT.MaxLength = 50
        Me.txtDispControlSampleFAT.MendatroryField = False
        Me.txtDispControlSampleFAT.MyLinkLable1 = Nothing
        Me.txtDispControlSampleFAT.MyLinkLable2 = Nothing
        Me.txtDispControlSampleFAT.Name = "txtDispControlSampleFAT"
        Me.txtDispControlSampleFAT.ReadOnly = True
        Me.txtDispControlSampleFAT.ReferenceFieldDesc = Nothing
        Me.txtDispControlSampleFAT.ReferenceFieldName = Nothing
        Me.txtDispControlSampleFAT.ReferenceTableName = Nothing
        Me.txtDispControlSampleFAT.Size = New System.Drawing.Size(63, 20)
        Me.txtDispControlSampleFAT.TabIndex = 360
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Location = New System.Drawing.Point(7, 13)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(161, 18)
        Me.MyLabel4.TabIndex = 361
        Me.MyLabel4.Text = "Dispatch Control Sample FAT%"
        '
        'fndTankerNo
        '
        Me.fndTankerNo.CalculationExpression = Nothing
        Me.fndTankerNo.Enabled = False
        Me.fndTankerNo.FieldCode = Nothing
        Me.fndTankerNo.FieldDesc = Nothing
        Me.fndTankerNo.FieldMaxLength = 0
        Me.fndTankerNo.FieldName = Nothing
        Me.fndTankerNo.isCalculatedField = False
        Me.fndTankerNo.IsSourceFromTable = False
        Me.fndTankerNo.IsSourceFromValueList = False
        Me.fndTankerNo.IsUnique = False
        Me.fndTankerNo.Location = New System.Drawing.Point(472, 26)
        Me.fndTankerNo.MendatroryField = False
        Me.fndTankerNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndTankerNo.MyLinkLable1 = Nothing
        Me.fndTankerNo.MyLinkLable2 = Nothing
        Me.fndTankerNo.MyReadOnly = False
        Me.fndTankerNo.MyShowMasterFormButton = False
        Me.fndTankerNo.Name = "fndTankerNo"
        Me.fndTankerNo.ReferenceFieldDesc = Nothing
        Me.fndTankerNo.ReferenceFieldName = Nothing
        Me.fndTankerNo.ReferenceTableName = Nothing
        Me.fndTankerNo.Size = New System.Drawing.Size(190, 19)
        Me.fndTankerNo.TabIndex = 342
        Me.fndTankerNo.Value = ""
        '
        'TxtDeductionAmount
        '
        Me.TxtDeductionAmount.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtDeductionAmount.CalculationExpression = Nothing
        Me.TxtDeductionAmount.DecimalPlaces = 2
        Me.TxtDeductionAmount.Enabled = False
        Me.TxtDeductionAmount.FieldCode = Nothing
        Me.TxtDeductionAmount.FieldDesc = Nothing
        Me.TxtDeductionAmount.FieldMaxLength = 0
        Me.TxtDeductionAmount.FieldName = Nothing
        Me.TxtDeductionAmount.isCalculatedField = False
        Me.TxtDeductionAmount.IsSourceFromTable = False
        Me.TxtDeductionAmount.IsSourceFromValueList = False
        Me.TxtDeductionAmount.IsUnique = False
        Me.TxtDeductionAmount.Location = New System.Drawing.Point(475, 116)
        Me.TxtDeductionAmount.MendatroryField = False
        Me.TxtDeductionAmount.MyLinkLable1 = Nothing
        Me.TxtDeductionAmount.MyLinkLable2 = Nothing
        Me.TxtDeductionAmount.Name = "TxtDeductionAmount"
        Me.TxtDeductionAmount.ReadOnly = True
        Me.TxtDeductionAmount.ReferenceFieldDesc = Nothing
        Me.TxtDeductionAmount.ReferenceFieldName = Nothing
        Me.TxtDeductionAmount.ReferenceTableName = Nothing
        Me.TxtDeductionAmount.Size = New System.Drawing.Size(262, 20)
        Me.TxtDeductionAmount.TabIndex = 339
        Me.TxtDeductionAmount.Text = "0"
        Me.TxtDeductionAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtDeductionAmount.Value = 0R
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(347, 118)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(99, 16)
        Me.MyLabel1.TabIndex = 340
        Me.MyLabel1.Text = "Deduction Amount"
        '
        'lblQcAcceptedOrRejected
        '
        Me.lblQcAcceptedOrRejected.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lblQcAcceptedOrRejected.FieldName = Nothing
        Me.lblQcAcceptedOrRejected.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblQcAcceptedOrRejected.Location = New System.Drawing.Point(722, 2)
        Me.lblQcAcceptedOrRejected.Name = "lblQcAcceptedOrRejected"
        Me.lblQcAcceptedOrRejected.Size = New System.Drawing.Size(2, 2)
        Me.lblQcAcceptedOrRejected.TabIndex = 338
        '
        'dtpWeighmentDate
        '
        Me.dtpWeighmentDate.CalculationExpression = Nothing
        Me.dtpWeighmentDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpWeighmentDate.Enabled = False
        Me.dtpWeighmentDate.FieldCode = Nothing
        Me.dtpWeighmentDate.FieldDesc = Nothing
        Me.dtpWeighmentDate.FieldMaxLength = 0
        Me.dtpWeighmentDate.FieldName = Nothing
        Me.dtpWeighmentDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpWeighmentDate.isCalculatedField = False
        Me.dtpWeighmentDate.IsSourceFromTable = False
        Me.dtpWeighmentDate.IsSourceFromValueList = False
        Me.dtpWeighmentDate.IsUnique = False
        Me.dtpWeighmentDate.Location = New System.Drawing.Point(641, 138)
        Me.dtpWeighmentDate.MendatroryField = False
        Me.dtpWeighmentDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpWeighmentDate.MyLinkLable1 = Nothing
        Me.dtpWeighmentDate.MyLinkLable2 = Nothing
        Me.dtpWeighmentDate.Name = "dtpWeighmentDate"
        Me.dtpWeighmentDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpWeighmentDate.ReadOnly = True
        Me.dtpWeighmentDate.ReferenceFieldDesc = Nothing
        Me.dtpWeighmentDate.ReferenceFieldName = Nothing
        Me.dtpWeighmentDate.ReferenceTableName = Nothing
        Me.dtpWeighmentDate.Size = New System.Drawing.Size(96, 20)
        Me.dtpWeighmentDate.TabIndex = 333
        Me.dtpWeighmentDate.TabStop = False
        Me.dtpWeighmentDate.Text = "10/06/2011"
        Me.dtpWeighmentDate.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(610, 140)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(30, 16)
        Me.MyLabel3.TabIndex = 328
        Me.MyLabel3.Text = "Date"
        '
        'txtDipValue
        '
        Me.txtDipValue.CalculationExpression = Nothing
        Me.txtDipValue.FieldCode = Nothing
        Me.txtDipValue.FieldDesc = Nothing
        Me.txtDipValue.FieldMaxLength = 0
        Me.txtDipValue.FieldName = Nothing
        Me.txtDipValue.isCalculatedField = False
        Me.txtDipValue.IsSourceFromTable = False
        Me.txtDipValue.IsSourceFromValueList = False
        Me.txtDipValue.IsUnique = False
        Me.txtDipValue.Location = New System.Drawing.Point(133, 116)
        Me.txtDipValue.MaxLength = 50
        Me.txtDipValue.MendatroryField = False
        Me.txtDipValue.MyLinkLable1 = Nothing
        Me.txtDipValue.MyLinkLable2 = Nothing
        Me.txtDipValue.Name = "txtDipValue"
        Me.txtDipValue.ReadOnly = True
        Me.txtDipValue.ReferenceFieldDesc = Nothing
        Me.txtDipValue.ReferenceFieldName = Nothing
        Me.txtDipValue.ReferenceTableName = Nothing
        Me.txtDipValue.Size = New System.Drawing.Size(191, 20)
        Me.txtDipValue.TabIndex = 337
        '
        'lblGateEntryNO
        '
        Me.lblGateEntryNO.FieldName = Nothing
        Me.lblGateEntryNO.Location = New System.Drawing.Point(13, 71)
        Me.lblGateEntryNO.Name = "lblGateEntryNO"
        Me.lblGateEntryNO.Size = New System.Drawing.Size(82, 18)
        Me.lblGateEntryNO.TabIndex = 312
        Me.lblGateEntryNO.Text = "Gate Entry No. "
        '
        'lblDipValue
        '
        Me.lblDipValue.FieldName = Nothing
        Me.lblDipValue.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDipValue.Location = New System.Drawing.Point(13, 118)
        Me.lblDipValue.Name = "lblDipValue"
        Me.lblDipValue.Size = New System.Drawing.Size(57, 16)
        Me.lblDipValue.TabIndex = 336
        Me.lblDipValue.Text = "DIP Value"
        '
        'lblChallanDate
        '
        Me.lblChallanDate.FieldName = Nothing
        Me.lblChallanDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChallanDate.Location = New System.Drawing.Point(347, 95)
        Me.lblChallanDate.Name = "lblChallanDate"
        Me.lblChallanDate.Size = New System.Drawing.Size(72, 16)
        Me.lblChallanDate.TabIndex = 320
        Me.lblChallanDate.Text = "Challan Date"
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
        Me.fndLocation.Location = New System.Drawing.Point(133, 183)
        Me.fndLocation.MendatroryField = False
        Me.fndLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLocation.MyLinkLable1 = Nothing
        Me.fndLocation.MyLinkLable2 = Nothing
        Me.fndLocation.MyReadOnly = True
        Me.fndLocation.MyShowMasterFormButton = False
        Me.fndLocation.Name = "fndLocation"
        Me.fndLocation.ReferenceFieldDesc = Nothing
        Me.fndLocation.ReferenceFieldName = Nothing
        Me.fndLocation.ReferenceTableName = Nothing
        Me.fndLocation.Size = New System.Drawing.Size(190, 19)
        Me.fndLocation.TabIndex = 335
        Me.fndLocation.Value = ""
        '
        'lblChallanNo
        '
        Me.lblChallanNo.FieldName = Nothing
        Me.lblChallanNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChallanNo.Location = New System.Drawing.Point(13, 95)
        Me.lblChallanNo.Name = "lblChallanNo"
        Me.lblChallanNo.Size = New System.Drawing.Size(62, 16)
        Me.lblChallanNo.TabIndex = 319
        Me.lblChallanNo.Text = "Challan No"
        '
        'fndVendor
        '
        Me.fndVendor.CalculationExpression = Nothing
        Me.fndVendor.Enabled = False
        Me.fndVendor.FieldCode = Nothing
        Me.fndVendor.FieldDesc = Nothing
        Me.fndVendor.FieldMaxLength = 0
        Me.fndVendor.FieldName = Nothing
        Me.fndVendor.isCalculatedField = False
        Me.fndVendor.IsSourceFromTable = False
        Me.fndVendor.IsSourceFromValueList = False
        Me.fndVendor.IsUnique = False
        Me.fndVendor.Location = New System.Drawing.Point(133, 161)
        Me.fndVendor.MendatroryField = False
        Me.fndVendor.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndVendor.MyLinkLable1 = Nothing
        Me.fndVendor.MyLinkLable2 = Nothing
        Me.fndVendor.MyReadOnly = True
        Me.fndVendor.MyShowMasterFormButton = False
        Me.fndVendor.Name = "fndVendor"
        Me.fndVendor.ReferenceFieldDesc = Nothing
        Me.fndVendor.ReferenceFieldName = Nothing
        Me.fndVendor.ReferenceTableName = Nothing
        Me.fndVendor.Size = New System.Drawing.Size(190, 19)
        Me.fndVendor.TabIndex = 334
        Me.fndVendor.Value = ""
        '
        'lblVendor
        '
        Me.lblVendor.FieldName = Nothing
        Me.lblVendor.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVendor.Location = New System.Drawing.Point(13, 162)
        Me.lblVendor.Name = "lblVendor"
        Me.lblVendor.Size = New System.Drawing.Size(43, 16)
        Me.lblVendor.TabIndex = 316
        Me.lblVendor.Text = "Vendor"
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(13, 184)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 16)
        Me.lblLocation.TabIndex = 314
        Me.lblLocation.Text = "Location"
        '
        'txtWeighmentNo
        '
        Me.txtWeighmentNo.CalculationExpression = Nothing
        Me.txtWeighmentNo.Enabled = False
        Me.txtWeighmentNo.FieldCode = Nothing
        Me.txtWeighmentNo.FieldDesc = Nothing
        Me.txtWeighmentNo.FieldMaxLength = 0
        Me.txtWeighmentNo.FieldName = Nothing
        Me.txtWeighmentNo.isCalculatedField = False
        Me.txtWeighmentNo.IsSourceFromTable = False
        Me.txtWeighmentNo.IsSourceFromValueList = False
        Me.txtWeighmentNo.IsUnique = False
        Me.txtWeighmentNo.Location = New System.Drawing.Point(475, 138)
        Me.txtWeighmentNo.MaxLength = 50
        Me.txtWeighmentNo.MendatroryField = False
        Me.txtWeighmentNo.MyLinkLable1 = Nothing
        Me.txtWeighmentNo.MyLinkLable2 = Nothing
        Me.txtWeighmentNo.Name = "txtWeighmentNo"
        Me.txtWeighmentNo.ReadOnly = True
        Me.txtWeighmentNo.ReferenceFieldDesc = Nothing
        Me.txtWeighmentNo.ReferenceFieldName = Nothing
        Me.txtWeighmentNo.ReferenceTableName = Nothing
        Me.txtWeighmentNo.Size = New System.Drawing.Size(130, 20)
        Me.txtWeighmentNo.TabIndex = 332
        '
        'lblTankerNo
        '
        Me.lblTankerNo.FieldName = Nothing
        Me.lblTankerNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTankerNo.Location = New System.Drawing.Point(347, 27)
        Me.lblTankerNo.Name = "lblTankerNo"
        Me.lblTankerNo.Size = New System.Drawing.Size(59, 16)
        Me.lblTankerNo.TabIndex = 318
        Me.lblTankerNo.Text = "Tanker No"
        '
        'dtpChallanDate
        '
        Me.dtpChallanDate.CalculationExpression = Nothing
        Me.dtpChallanDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpChallanDate.Enabled = False
        Me.dtpChallanDate.FieldCode = Nothing
        Me.dtpChallanDate.FieldDesc = Nothing
        Me.dtpChallanDate.FieldMaxLength = 0
        Me.dtpChallanDate.FieldName = Nothing
        Me.dtpChallanDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpChallanDate.isCalculatedField = False
        Me.dtpChallanDate.IsSourceFromTable = False
        Me.dtpChallanDate.IsSourceFromValueList = False
        Me.dtpChallanDate.IsUnique = False
        Me.dtpChallanDate.Location = New System.Drawing.Point(475, 93)
        Me.dtpChallanDate.MendatroryField = False
        Me.dtpChallanDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpChallanDate.MyLinkLable1 = Nothing
        Me.dtpChallanDate.MyLinkLable2 = Nothing
        Me.dtpChallanDate.Name = "dtpChallanDate"
        Me.dtpChallanDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpChallanDate.ReadOnly = True
        Me.dtpChallanDate.ReferenceFieldDesc = Nothing
        Me.dtpChallanDate.ReferenceFieldName = Nothing
        Me.dtpChallanDate.ReferenceTableName = Nothing
        Me.dtpChallanDate.Size = New System.Drawing.Size(262, 20)
        Me.dtpChallanDate.TabIndex = 331
        Me.dtpChallanDate.TabStop = False
        Me.dtpChallanDate.Text = "10/06/2011"
        Me.dtpChallanDate.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'lblDateAndTime
        '
        Me.lblDateAndTime.FieldName = Nothing
        Me.lblDateAndTime.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDateAndTime.Location = New System.Drawing.Point(347, 72)
        Me.lblDateAndTime.Name = "lblDateAndTime"
        Me.lblDateAndTime.Size = New System.Drawing.Size(91, 16)
        Me.lblDateAndTime.TabIndex = 313
        Me.lblDateAndTime.Text = "Gate Entry Date "
        '
        'txtChallanNo
        '
        Me.txtChallanNo.CalculationExpression = Nothing
        Me.txtChallanNo.Enabled = False
        Me.txtChallanNo.FieldCode = Nothing
        Me.txtChallanNo.FieldDesc = Nothing
        Me.txtChallanNo.FieldMaxLength = 0
        Me.txtChallanNo.FieldName = Nothing
        Me.txtChallanNo.isCalculatedField = False
        Me.txtChallanNo.IsSourceFromTable = False
        Me.txtChallanNo.IsSourceFromValueList = False
        Me.txtChallanNo.IsUnique = False
        Me.txtChallanNo.Location = New System.Drawing.Point(133, 93)
        Me.txtChallanNo.MaxLength = 50
        Me.txtChallanNo.MendatroryField = False
        Me.txtChallanNo.MyLinkLable1 = Nothing
        Me.txtChallanNo.MyLinkLable2 = Nothing
        Me.txtChallanNo.Name = "txtChallanNo"
        Me.txtChallanNo.ReadOnly = True
        Me.txtChallanNo.ReferenceFieldDesc = Nothing
        Me.txtChallanNo.ReferenceFieldName = Nothing
        Me.txtChallanNo.ReferenceTableName = Nothing
        Me.txtChallanNo.Size = New System.Drawing.Size(191, 20)
        Me.txtChallanNo.TabIndex = 330
        '
        'lblLocationName
        '
        Me.lblLocationName.AutoSize = False
        Me.lblLocationName.BorderVisible = True
        Me.lblLocationName.FieldName = Nothing
        Me.lblLocationName.Location = New System.Drawing.Point(347, 183)
        Me.lblLocationName.Name = "lblLocationName"
        Me.lblLocationName.Size = New System.Drawing.Size(390, 19)
        Me.lblLocationName.TabIndex = 315
        '
        'lblQcInDateAndTime
        '
        Me.lblQcInDateAndTime.FieldName = Nothing
        Me.lblQcInDateAndTime.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblQcInDateAndTime.Location = New System.Drawing.Point(13, 50)
        Me.lblQcInDateAndTime.Name = "lblQcInDateAndTime"
        Me.lblQcInDateAndTime.Size = New System.Drawing.Size(66, 16)
        Me.lblQcInDateAndTime.TabIndex = 323
        Me.lblQcInDateAndTime.Text = "QC In Date "
        '
        'dtpGateEntryDateTime
        '
        Me.dtpGateEntryDateTime.CalculationExpression = Nothing
        Me.dtpGateEntryDateTime.CustomFormat = "dd/MM/yyyy hh:mm:ss tt"
        Me.dtpGateEntryDateTime.Enabled = False
        Me.dtpGateEntryDateTime.FieldCode = Nothing
        Me.dtpGateEntryDateTime.FieldDesc = Nothing
        Me.dtpGateEntryDateTime.FieldMaxLength = 0
        Me.dtpGateEntryDateTime.FieldName = Nothing
        Me.dtpGateEntryDateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpGateEntryDateTime.isCalculatedField = False
        Me.dtpGateEntryDateTime.IsSourceFromTable = False
        Me.dtpGateEntryDateTime.IsSourceFromValueList = False
        Me.dtpGateEntryDateTime.IsUnique = False
        Me.dtpGateEntryDateTime.Location = New System.Drawing.Point(475, 70)
        Me.dtpGateEntryDateTime.MendatroryField = False
        Me.dtpGateEntryDateTime.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpGateEntryDateTime.MyLinkLable1 = Nothing
        Me.dtpGateEntryDateTime.MyLinkLable2 = Nothing
        Me.dtpGateEntryDateTime.Name = "dtpGateEntryDateTime"
        Me.dtpGateEntryDateTime.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpGateEntryDateTime.ReadOnly = True
        Me.dtpGateEntryDateTime.ReferenceFieldDesc = Nothing
        Me.dtpGateEntryDateTime.ReferenceFieldName = Nothing
        Me.dtpGateEntryDateTime.ReferenceTableName = Nothing
        Me.dtpGateEntryDateTime.Size = New System.Drawing.Size(261, 20)
        Me.dtpGateEntryDateTime.TabIndex = 329
        Me.dtpGateEntryDateTime.TabStop = False
        Me.dtpGateEntryDateTime.Text = "10/06/2011 11:51:56 AM"
        Me.dtpGateEntryDateTime.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'lblVendorName
        '
        Me.lblVendorName.AutoSize = False
        Me.lblVendorName.BorderVisible = True
        Me.lblVendorName.FieldName = Nothing
        Me.lblVendorName.Location = New System.Drawing.Point(346, 161)
        Me.lblVendorName.Name = "lblVendorName"
        Me.lblVendorName.Size = New System.Drawing.Size(391, 19)
        Me.lblVendorName.TabIndex = 317
        '
        'lblStatus
        '
        Me.lblStatus.FieldName = Nothing
        Me.lblStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(13, 140)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(99, 16)
        Me.lblStatus.TabIndex = 321
        Me.lblStatus.Text = "Weighment Status"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(347, 140)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(81, 16)
        Me.MyLabel2.TabIndex = 327
        Me.MyLabel2.Text = "Weighment No"
        '
        'dtpQCInDateTime
        '
        Me.dtpQCInDateTime.CalculationExpression = Nothing
        Me.dtpQCInDateTime.CustomFormat = "dd/MM/yyyy hh:mm:ss tt"
        Me.dtpQCInDateTime.FieldCode = Nothing
        Me.dtpQCInDateTime.FieldDesc = Nothing
        Me.dtpQCInDateTime.FieldMaxLength = 0
        Me.dtpQCInDateTime.FieldName = Nothing
        Me.dtpQCInDateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpQCInDateTime.isCalculatedField = False
        Me.dtpQCInDateTime.IsSourceFromTable = False
        Me.dtpQCInDateTime.IsSourceFromValueList = False
        Me.dtpQCInDateTime.IsUnique = False
        Me.dtpQCInDateTime.Location = New System.Drawing.Point(133, 48)
        Me.dtpQCInDateTime.MendatroryField = False
        Me.dtpQCInDateTime.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpQCInDateTime.MyLinkLable1 = Nothing
        Me.dtpQCInDateTime.MyLinkLable2 = Nothing
        Me.dtpQCInDateTime.Name = "dtpQCInDateTime"
        Me.dtpQCInDateTime.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpQCInDateTime.ReadOnly = True
        Me.dtpQCInDateTime.ReferenceFieldDesc = Nothing
        Me.dtpQCInDateTime.ReferenceFieldName = Nothing
        Me.dtpQCInDateTime.ReferenceTableName = Nothing
        Me.dtpQCInDateTime.Size = New System.Drawing.Size(190, 20)
        Me.dtpQCInDateTime.TabIndex = 324
        Me.dtpQCInDateTime.TabStop = False
        Me.dtpQCInDateTime.Text = "10/06/2011 12:00:00 AM"
        Me.dtpQCInDateTime.Value = New Date(2011, 6, 10, 0, 0, 0, 0)
        '
        'dtpQCOutDateTime
        '
        Me.dtpQCOutDateTime.CalculationExpression = Nothing
        Me.dtpQCOutDateTime.CustomFormat = "dd/MM/yyyy hh:mm:ss tt"
        Me.dtpQCOutDateTime.FieldCode = Nothing
        Me.dtpQCOutDateTime.FieldDesc = Nothing
        Me.dtpQCOutDateTime.FieldMaxLength = 0
        Me.dtpQCOutDateTime.FieldName = Nothing
        Me.dtpQCOutDateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpQCOutDateTime.isCalculatedField = False
        Me.dtpQCOutDateTime.IsSourceFromTable = False
        Me.dtpQCOutDateTime.IsSourceFromValueList = False
        Me.dtpQCOutDateTime.IsUnique = False
        Me.dtpQCOutDateTime.Location = New System.Drawing.Point(475, 48)
        Me.dtpQCOutDateTime.MendatroryField = False
        Me.dtpQCOutDateTime.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpQCOutDateTime.MyLinkLable1 = Nothing
        Me.dtpQCOutDateTime.MyLinkLable2 = Nothing
        Me.dtpQCOutDateTime.Name = "dtpQCOutDateTime"
        Me.dtpQCOutDateTime.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpQCOutDateTime.ReadOnly = True
        Me.dtpQCOutDateTime.ReferenceFieldDesc = Nothing
        Me.dtpQCOutDateTime.ReferenceFieldName = Nothing
        Me.dtpQCOutDateTime.ReferenceTableName = Nothing
        Me.dtpQCOutDateTime.Size = New System.Drawing.Size(260, 20)
        Me.dtpQCOutDateTime.TabIndex = 326
        Me.dtpQCOutDateTime.TabStop = False
        Me.dtpQCOutDateTime.Text = "10/06/2011 11:51:56 AM"
        Me.dtpQCOutDateTime.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'lblQCOutDateAndTime
        '
        Me.lblQCOutDateAndTime.FieldName = Nothing
        Me.lblQCOutDateAndTime.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblQCOutDateAndTime.Location = New System.Drawing.Point(346, 50)
        Me.lblQCOutDateAndTime.Name = "lblQCOutDateAndTime"
        Me.lblQCOutDateAndTime.Size = New System.Drawing.Size(75, 16)
        Me.lblQCOutDateAndTime.TabIndex = 325
        Me.lblQCOutDateAndTime.Text = "QC Out Date "
        '
        'fndGateEntryNo
        '
        Me.fndGateEntryNo.CalculationExpression = Nothing
        Me.fndGateEntryNo.Enabled = False
        Me.fndGateEntryNo.FieldCode = Nothing
        Me.fndGateEntryNo.FieldDesc = Nothing
        Me.fndGateEntryNo.FieldMaxLength = 0
        Me.fndGateEntryNo.FieldName = Nothing
        Me.fndGateEntryNo.isCalculatedField = False
        Me.fndGateEntryNo.IsSourceFromTable = False
        Me.fndGateEntryNo.IsSourceFromValueList = False
        Me.fndGateEntryNo.IsUnique = False
        Me.fndGateEntryNo.Location = New System.Drawing.Point(133, 71)
        Me.fndGateEntryNo.MendatroryField = False
        Me.fndGateEntryNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndGateEntryNo.MyLinkLable1 = Nothing
        Me.fndGateEntryNo.MyLinkLable2 = Nothing
        Me.fndGateEntryNo.MyReadOnly = True
        Me.fndGateEntryNo.MyShowMasterFormButton = False
        Me.fndGateEntryNo.Name = "fndGateEntryNo"
        Me.fndGateEntryNo.ReferenceFieldDesc = Nothing
        Me.fndGateEntryNo.ReferenceFieldName = Nothing
        Me.fndGateEntryNo.ReferenceTableName = Nothing
        Me.fndGateEntryNo.Size = New System.Drawing.Size(190, 19)
        Me.fndGateEntryNo.TabIndex = 311
        Me.fndGateEntryNo.Value = ""
        '
        'lblStatusValue
        '
        Me.lblStatusValue.AutoSize = False
        Me.lblStatusValue.BorderVisible = True
        Me.lblStatusValue.FieldName = Nothing
        Me.lblStatusValue.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblStatusValue.Location = New System.Drawing.Point(133, 139)
        Me.lblStatusValue.Name = "lblStatusValue"
        Me.lblStatusValue.Size = New System.Drawing.Size(192, 19)
        Me.lblStatusValue.TabIndex = 322
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox2.Controls.Add(Me.gv1)
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "Item Details"
        Me.RadGroupBox2.Location = New System.Drawing.Point(2, 274)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(985, 169)
        Me.RadGroupBox2.TabIndex = 14
        Me.RadGroupBox2.Text = "Item Details"
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(10, 20)
        '
        '
        '
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(965, 139)
        Me.gv1.TabIndex = 13
        Me.gv1.TabStop = False
        '
        'btnReverse
        '
        Me.btnReverse.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReverse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverse.Location = New System.Drawing.Point(254, 8)
        Me.btnReverse.Name = "btnReverse"
        Me.btnReverse.Size = New System.Drawing.Size(135, 18)
        Me.btnReverse.TabIndex = 10
        Me.btnReverse.Text = "Reverse and Recreate"
        Me.btnReverse.Visible = False
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(175, 6)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(73, 20)
        Me.btnPost.TabIndex = 9
        Me.btnPost.Text = "Post"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(922, 6)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(73, 20)
        Me.btnclose.TabIndex = 8
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(96, 6)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(73, 20)
        Me.btndelete.TabIndex = 7
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(15, 6)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(73, 20)
        Me.btnsave.TabIndex = 6
        Me.btnsave.Text = "Save"
        '
        'FrmSecondarySettingForQC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1011, 529)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmSecondarySettingForQC"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Secondary Setting For QC"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpControlSample.ResumeLayout(False)
        Me.GrpControlSample.PerformLayout()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRcptControlSampleSNF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRcptControlSampleFAT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDispControlSampleSNF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDispControlSampleFAT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtDeductionAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblQcAcceptedOrRejected, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpWeighmentDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDipValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGateEntryNO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDipValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblChallanDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblChallanNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtWeighmentNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTankerNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpChallanDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDateAndTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtChallanNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblQcInDateAndTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpGateEntryDateTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendorName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpQCInDateTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpQCOutDateTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblQCOutDateAndTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblStatusValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents GrpControlSample As System.Windows.Forms.GroupBox
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents txtRcptControlSampleSNF As common.Controls.MyTextBox
    Friend WithEvents txtRcptControlSampleFAT As common.Controls.MyTextBox
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents txtDispControlSampleSNF As common.Controls.MyTextBox
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents txtDispControlSampleFAT As common.Controls.MyTextBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents fndTankerNo As common.UserControls.txtFinder
    Friend WithEvents TxtDeductionAmount As common.MyNumBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents lblQcAcceptedOrRejected As common.Controls.MyLabel
    Friend WithEvents dtpWeighmentDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtDipValue As common.Controls.MyTextBox
    Friend WithEvents lblGateEntryNO As common.Controls.MyLabel
    Friend WithEvents lblDipValue As common.Controls.MyLabel
    Friend WithEvents lblChallanDate As common.Controls.MyLabel
    Friend WithEvents fndLocation As common.UserControls.txtFinder
    Friend WithEvents lblChallanNo As common.Controls.MyLabel
    Friend WithEvents fndVendor As common.UserControls.txtFinder
    Friend WithEvents lblVendor As common.Controls.MyLabel
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents txtWeighmentNo As common.Controls.MyTextBox
    Friend WithEvents lblTankerNo As common.Controls.MyLabel
    Friend WithEvents dtpChallanDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblDateAndTime As common.Controls.MyLabel
    Friend WithEvents txtChallanNo As common.Controls.MyTextBox
    Friend WithEvents lblLocationName As common.Controls.MyLabel
    Friend WithEvents lblQcInDateAndTime As common.Controls.MyLabel
    Friend WithEvents dtpGateEntryDateTime As common.Controls.MyDateTimePicker
    Friend WithEvents lblVendorName As common.Controls.MyLabel
    Friend WithEvents lblStatus As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents dtpQCInDateTime As common.Controls.MyDateTimePicker
    Friend WithEvents dtpQCOutDateTime As common.Controls.MyDateTimePicker
    Friend WithEvents lblQCOutDateAndTime As common.Controls.MyLabel
    Friend WithEvents fndGateEntryNo As common.UserControls.txtFinder
    Friend WithEvents lblStatusValue As common.Controls.MyLabel
    Friend WithEvents lblPending As common.usLock
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents FndQCNo As common.UserControls.txtFinder
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReverse As Telerik.WinControls.UI.RadButton
End Class
