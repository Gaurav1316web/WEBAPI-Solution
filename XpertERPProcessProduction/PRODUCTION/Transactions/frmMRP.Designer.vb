<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMRP
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
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition3 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.txtMRPDescription = New common.Controls.MyTextBox()
        Me.UsLock1 = New common.usLock()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.lblempcode = New common.Controls.MyLabel()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.pageGeneral = New Telerik.WinControls.UI.RadPageViewPage()
        Me.txtProductionPlanDesc = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.fndProductionPlan = New common.UserControls.txtFinder()
        Me.lblPackSizeDesc = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtPackSize = New common.MyNumBox()
        Me.lblLocationDesc = New common.Controls.MyLabel()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.fndLocation = New common.UserControls.txtFinder()
        Me.chkPendingSRN = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkIncludePO = New Telerik.WinControls.UI.RadCheckBox()
        Me.lblBOMDesc = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.fndBOM = New common.UserControls.txtFinder()
        Me.lblBuildQty = New common.Controls.MyLabel()
        Me.lblUnitName = New common.Controls.MyLabel()
        Me.txtBuildQty = New common.MyNumBox()
        Me.lblItemDesc = New common.Controls.MyLabel()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.lblDescription = New common.Controls.MyLabel()
        Me.lblFGItemCode = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.dtpToDate = New common.Controls.MyDateTimePicker()
        Me.lblFromDate = New common.Controls.MyLabel()
        Me.dtpFromDate = New common.Controls.MyDateTimePicker()
        Me.lblMRPDate = New common.Controls.MyLabel()
        Me.dtpMRPDate = New common.Controls.MyDateTimePicker()
        Me.fndItemToProduce = New common.UserControls.txtFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lblNoOfDays = New common.Controls.MyLabel()
        Me.pagePO = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.gvPO = New common.UserControls.MyRadGridView()
        Me.pagePendingSRN = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvSRN = New common.UserControls.MyRadGridView()
        Me.pageMRPDetail = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvMRPDetal = New common.UserControls.MyRadGridView()
        Me.btnUnpost = New Telerik.WinControls.UI.RadButton()
        Me.btnNext = New Telerik.WinControls.UI.RadButton()
        Me.btnRequisition = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        CType(Me.txtMRPDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblempcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.pageGeneral.SuspendLayout()
        CType(Me.txtProductionPlanDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPackSizeDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPackSize, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPendingSRN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIncludePO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBOMDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBuildQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblUnitName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBuildQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblItemDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFGItemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMRPDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpMRPDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNoOfDays, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pagePO.SuspendLayout()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.gvPO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvPO.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pagePendingSRN.SuspendLayout()
        CType(Me.gvSRN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvSRN.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pageMRPDetail.SuspendLayout()
        CType(Me.gvMRPDetal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvMRPDetal.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUnpost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNext, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRequisition, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtMRPDescription
        '
        Me.txtMRPDescription.CalculationExpression = Nothing
        Me.txtMRPDescription.FieldCode = Nothing
        Me.txtMRPDescription.FieldDesc = Nothing
        Me.txtMRPDescription.FieldMaxLength = 0
        Me.txtMRPDescription.FieldName = Nothing
        Me.txtMRPDescription.isCalculatedField = False
        Me.txtMRPDescription.IsSourceFromTable = False
        Me.txtMRPDescription.IsSourceFromValueList = False
        Me.txtMRPDescription.IsUnique = False
        Me.txtMRPDescription.Location = New System.Drawing.Point(378, 8)
        Me.txtMRPDescription.MaxLength = 50
        Me.txtMRPDescription.MendatroryField = False
        Me.txtMRPDescription.MyLinkLable1 = Nothing
        Me.txtMRPDescription.MyLinkLable2 = Nothing
        Me.txtMRPDescription.Name = "txtMRPDescription"
        Me.txtMRPDescription.ReferenceFieldDesc = Nothing
        Me.txtMRPDescription.ReferenceFieldName = Nothing
        Me.txtMRPDescription.ReferenceTableName = Nothing
        Me.txtMRPDescription.Size = New System.Drawing.Size(330, 20)
        Me.txtMRPDescription.TabIndex = 209
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(867, 8)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(91, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 208
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(102, 8)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblempcode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 12
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(255, 21)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'lblempcode
        '
        Me.lblempcode.FieldName = Nothing
        Me.lblempcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblempcode.Location = New System.Drawing.Point(9, 10)
        Me.lblempcode.Name = "lblempcode"
        Me.lblempcode.Size = New System.Drawing.Size(62, 16)
        Me.lblempcode.TabIndex = 206
        Me.lblempcode.Text = "MRP Code"
        '
        'btnnew
        '
        Me.btnnew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnnew.Image = Global.XpertERPProcessProduction.My.Resources.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(358, 8)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(16, 20)
        Me.btnnew.TabIndex = 205
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnUnpost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnNext)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnRequisition)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(986, 548)
        Me.SplitContainer1.SplitterDistance = 512
        Me.SplitContainer1.TabIndex = 203
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblempcode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnnew)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtMRPDescription)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.UsLock1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadPageView1)
        Me.SplitContainer2.Size = New System.Drawing.Size(986, 512)
        Me.SplitContainer2.SplitterDistance = 40
        Me.SplitContainer2.TabIndex = 204
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.pageGeneral)
        Me.RadPageView1.Controls.Add(Me.pagePO)
        Me.RadPageView1.Controls.Add(Me.pagePendingSRN)
        Me.RadPageView1.Controls.Add(Me.pageMRPDetail)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.pageMRPDetail
        Me.RadPageView1.Size = New System.Drawing.Size(986, 468)
        Me.RadPageView1.TabIndex = 0
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'pageGeneral
        '
        Me.pageGeneral.Controls.Add(Me.txtProductionPlanDesc)
        Me.pageGeneral.Controls.Add(Me.MyLabel5)
        Me.pageGeneral.Controls.Add(Me.fndProductionPlan)
        Me.pageGeneral.Controls.Add(Me.lblPackSizeDesc)
        Me.pageGeneral.Controls.Add(Me.MyLabel2)
        Me.pageGeneral.Controls.Add(Me.txtPackSize)
        Me.pageGeneral.Controls.Add(Me.lblLocationDesc)
        Me.pageGeneral.Controls.Add(Me.MyLabel6)
        Me.pageGeneral.Controls.Add(Me.fndLocation)
        Me.pageGeneral.Controls.Add(Me.chkPendingSRN)
        Me.pageGeneral.Controls.Add(Me.chkIncludePO)
        Me.pageGeneral.Controls.Add(Me.lblBOMDesc)
        Me.pageGeneral.Controls.Add(Me.MyLabel4)
        Me.pageGeneral.Controls.Add(Me.fndBOM)
        Me.pageGeneral.Controls.Add(Me.lblBuildQty)
        Me.pageGeneral.Controls.Add(Me.lblUnitName)
        Me.pageGeneral.Controls.Add(Me.txtBuildQty)
        Me.pageGeneral.Controls.Add(Me.lblItemDesc)
        Me.pageGeneral.Controls.Add(Me.txtDescription)
        Me.pageGeneral.Controls.Add(Me.lblDescription)
        Me.pageGeneral.Controls.Add(Me.lblFGItemCode)
        Me.pageGeneral.Controls.Add(Me.MyLabel7)
        Me.pageGeneral.Controls.Add(Me.dtpToDate)
        Me.pageGeneral.Controls.Add(Me.lblFromDate)
        Me.pageGeneral.Controls.Add(Me.dtpFromDate)
        Me.pageGeneral.Controls.Add(Me.lblMRPDate)
        Me.pageGeneral.Controls.Add(Me.dtpMRPDate)
        Me.pageGeneral.Controls.Add(Me.fndItemToProduce)
        Me.pageGeneral.Controls.Add(Me.MyLabel1)
        Me.pageGeneral.Controls.Add(Me.lblNoOfDays)
        Me.pageGeneral.ItemSize = New System.Drawing.SizeF(55.0!, 28.0!)
        Me.pageGeneral.Location = New System.Drawing.Point(10, 37)
        Me.pageGeneral.Name = "pageGeneral"
        Me.pageGeneral.Size = New System.Drawing.Size(965, 420)
        Me.pageGeneral.Text = "General"
        '
        'txtProductionPlanDesc
        '
        Me.txtProductionPlanDesc.AutoSize = False
        Me.txtProductionPlanDesc.BorderVisible = True
        Me.txtProductionPlanDesc.FieldName = Nothing
        Me.txtProductionPlanDesc.Location = New System.Drawing.Point(444, 27)
        Me.txtProductionPlanDesc.Name = "txtProductionPlanDesc"
        Me.txtProductionPlanDesc.Size = New System.Drawing.Size(254, 19)
        Me.txtProductionPlanDesc.TabIndex = 274
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(5, 30)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(86, 16)
        Me.MyLabel5.TabIndex = 273
        Me.MyLabel5.Text = "Production Plan"
        '
        'fndProductionPlan
        '
        Me.fndProductionPlan.CalculationExpression = Nothing
        Me.fndProductionPlan.FieldCode = Nothing
        Me.fndProductionPlan.FieldDesc = Nothing
        Me.fndProductionPlan.FieldMaxLength = 0
        Me.fndProductionPlan.FieldName = Nothing
        Me.fndProductionPlan.isCalculatedField = False
        Me.fndProductionPlan.IsSourceFromTable = False
        Me.fndProductionPlan.IsSourceFromValueList = False
        Me.fndProductionPlan.IsUnique = False
        Me.fndProductionPlan.Location = New System.Drawing.Point(219, 27)
        Me.fndProductionPlan.MendatroryField = False
        Me.fndProductionPlan.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndProductionPlan.MyLinkLable1 = Me.MyLabel5
        Me.fndProductionPlan.MyLinkLable2 = Nothing
        Me.fndProductionPlan.MyReadOnly = False
        Me.fndProductionPlan.MyShowMasterFormButton = False
        Me.fndProductionPlan.Name = "fndProductionPlan"
        Me.fndProductionPlan.ReferenceFieldDesc = Nothing
        Me.fndProductionPlan.ReferenceFieldName = Nothing
        Me.fndProductionPlan.ReferenceTableName = Nothing
        Me.fndProductionPlan.Size = New System.Drawing.Size(219, 19)
        Me.fndProductionPlan.TabIndex = 4
        Me.fndProductionPlan.Value = ""
        '
        'lblPackSizeDesc
        '
        Me.lblPackSizeDesc.AutoSize = False
        Me.lblPackSizeDesc.BorderVisible = True
        Me.lblPackSizeDesc.FieldName = Nothing
        Me.lblPackSizeDesc.Location = New System.Drawing.Point(444, 272)
        Me.lblPackSizeDesc.Name = "lblPackSizeDesc"
        Me.lblPackSizeDesc.Size = New System.Drawing.Size(254, 19)
        Me.lblPackSizeDesc.TabIndex = 271
        Me.lblPackSizeDesc.Visible = False
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel2.Location = New System.Drawing.Point(7, 275)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(57, 16)
        Me.MyLabel2.TabIndex = 270
        Me.MyLabel2.Text = "Pack Size"
        Me.MyLabel2.Visible = False
        '
        'txtPackSize
        '
        Me.txtPackSize.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtPackSize.CalculationExpression = Nothing
        Me.txtPackSize.DecimalPlaces = 0
        Me.txtPackSize.FieldCode = Nothing
        Me.txtPackSize.FieldDesc = Nothing
        Me.txtPackSize.FieldMaxLength = 0
        Me.txtPackSize.FieldName = Nothing
        Me.txtPackSize.isCalculatedField = False
        Me.txtPackSize.IsSourceFromTable = False
        Me.txtPackSize.IsSourceFromValueList = False
        Me.txtPackSize.IsUnique = False
        Me.txtPackSize.Location = New System.Drawing.Point(220, 271)
        Me.txtPackSize.MendatroryField = True
        Me.txtPackSize.MyLinkLable1 = Me.MyLabel2
        Me.txtPackSize.MyLinkLable2 = Nothing
        Me.txtPackSize.Name = "txtPackSize"
        Me.txtPackSize.ReferenceFieldDesc = Nothing
        Me.txtPackSize.ReferenceFieldName = Nothing
        Me.txtPackSize.ReferenceTableName = Nothing
        Me.txtPackSize.Size = New System.Drawing.Size(100, 20)
        Me.txtPackSize.TabIndex = 269
        Me.txtPackSize.Text = "0"
        Me.txtPackSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtPackSize.Value = 0R
        Me.txtPackSize.Visible = False
        '
        'lblLocationDesc
        '
        Me.lblLocationDesc.AutoSize = False
        Me.lblLocationDesc.BorderVisible = True
        Me.lblLocationDesc.FieldName = Nothing
        Me.lblLocationDesc.Location = New System.Drawing.Point(443, 159)
        Me.lblLocationDesc.Name = "lblLocationDesc"
        Me.lblLocationDesc.Size = New System.Drawing.Size(254, 19)
        Me.lblLocationDesc.TabIndex = 268
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(6, 162)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(79, 16)
        Me.MyLabel6.TabIndex = 267
        Me.MyLabel6.Text = "Location Code"
        '
        'fndLocation
        '
        Me.fndLocation.CalculationExpression = Nothing
        Me.fndLocation.FieldCode = Nothing
        Me.fndLocation.FieldDesc = Nothing
        Me.fndLocation.FieldMaxLength = 0
        Me.fndLocation.FieldName = Nothing
        Me.fndLocation.isCalculatedField = False
        Me.fndLocation.IsSourceFromTable = False
        Me.fndLocation.IsSourceFromValueList = False
        Me.fndLocation.IsUnique = False
        Me.fndLocation.Location = New System.Drawing.Point(219, 159)
        Me.fndLocation.MendatroryField = True
        Me.fndLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLocation.MyLinkLable1 = Me.MyLabel6
        Me.fndLocation.MyLinkLable2 = Nothing
        Me.fndLocation.MyReadOnly = False
        Me.fndLocation.MyShowMasterFormButton = False
        Me.fndLocation.Name = "fndLocation"
        Me.fndLocation.ReferenceFieldDesc = Nothing
        Me.fndLocation.ReferenceFieldName = Nothing
        Me.fndLocation.ReferenceTableName = Nothing
        Me.fndLocation.Size = New System.Drawing.Size(219, 19)
        Me.fndLocation.TabIndex = 9
        Me.fndLocation.Value = ""
        '
        'chkPendingSRN
        '
        Me.chkPendingSRN.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkPendingSRN.Location = New System.Drawing.Point(451, 5)
        Me.chkPendingSRN.Name = "chkPendingSRN"
        Me.chkPendingSRN.Size = New System.Drawing.Size(125, 18)
        Me.chkPendingSRN.TabIndex = 3
        Me.chkPendingSRN.Text = "Include Pending SRN"
        Me.chkPendingSRN.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'chkIncludePO
        '
        Me.chkIncludePO.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkIncludePO.Location = New System.Drawing.Point(370, 5)
        Me.chkIncludePO.Name = "chkIncludePO"
        Me.chkIncludePO.Size = New System.Drawing.Size(75, 18)
        Me.chkIncludePO.TabIndex = 2
        Me.chkIncludePO.Text = "Include PO"
        Me.chkIncludePO.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'lblBOMDesc
        '
        Me.lblBOMDesc.AutoSize = False
        Me.lblBOMDesc.BorderVisible = True
        Me.lblBOMDesc.FieldName = Nothing
        Me.lblBOMDesc.Location = New System.Drawing.Point(444, 139)
        Me.lblBOMDesc.Name = "lblBOMDesc"
        Me.lblBOMDesc.Size = New System.Drawing.Size(254, 19)
        Me.lblBOMDesc.TabIndex = 264
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(5, 139)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(60, 16)
        Me.MyLabel4.TabIndex = 263
        Me.MyLabel4.Text = "Bom Code"
        '
        'fndBOM
        '
        Me.fndBOM.CalculationExpression = Nothing
        Me.fndBOM.FieldCode = Nothing
        Me.fndBOM.FieldDesc = Nothing
        Me.fndBOM.FieldMaxLength = 0
        Me.fndBOM.FieldName = Nothing
        Me.fndBOM.isCalculatedField = False
        Me.fndBOM.IsSourceFromTable = False
        Me.fndBOM.IsSourceFromValueList = False
        Me.fndBOM.IsUnique = False
        Me.fndBOM.Location = New System.Drawing.Point(219, 136)
        Me.fndBOM.MendatroryField = True
        Me.fndBOM.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndBOM.MyLinkLable1 = Me.MyLabel4
        Me.fndBOM.MyLinkLable2 = Nothing
        Me.fndBOM.MyReadOnly = False
        Me.fndBOM.MyShowMasterFormButton = False
        Me.fndBOM.Name = "fndBOM"
        Me.fndBOM.ReferenceFieldDesc = Nothing
        Me.fndBOM.ReferenceFieldName = Nothing
        Me.fndBOM.ReferenceTableName = Nothing
        Me.fndBOM.Size = New System.Drawing.Size(219, 19)
        Me.fndBOM.TabIndex = 8
        Me.fndBOM.Value = ""
        '
        'lblBuildQty
        '
        Me.lblBuildQty.FieldName = Nothing
        Me.lblBuildQty.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblBuildQty.Location = New System.Drawing.Point(6, 186)
        Me.lblBuildQty.Name = "lblBuildQty"
        Me.lblBuildQty.Size = New System.Drawing.Size(106, 16)
        Me.lblBuildQty.TabIndex = 260
        Me.lblBuildQty.Text = "Quantity to Produce"
        '
        'lblUnitName
        '
        Me.lblUnitName.AutoSize = False
        Me.lblUnitName.BorderVisible = True
        Me.lblUnitName.FieldName = Nothing
        Me.lblUnitName.Location = New System.Drawing.Point(324, 182)
        Me.lblUnitName.Name = "lblUnitName"
        Me.lblUnitName.Size = New System.Drawing.Size(114, 19)
        Me.lblUnitName.TabIndex = 261
        '
        'txtBuildQty
        '
        Me.txtBuildQty.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtBuildQty.CalculationExpression = Nothing
        Me.txtBuildQty.DecimalPlaces = 0
        Me.txtBuildQty.FieldCode = Nothing
        Me.txtBuildQty.FieldDesc = Nothing
        Me.txtBuildQty.FieldMaxLength = 0
        Me.txtBuildQty.FieldName = Nothing
        Me.txtBuildQty.isCalculatedField = False
        Me.txtBuildQty.IsSourceFromTable = False
        Me.txtBuildQty.IsSourceFromValueList = False
        Me.txtBuildQty.IsUnique = False
        Me.txtBuildQty.Location = New System.Drawing.Point(218, 182)
        Me.txtBuildQty.MendatroryField = True
        Me.txtBuildQty.MyLinkLable1 = Me.lblBuildQty
        Me.txtBuildQty.MyLinkLable2 = Nothing
        Me.txtBuildQty.Name = "txtBuildQty"
        Me.txtBuildQty.ReferenceFieldDesc = Nothing
        Me.txtBuildQty.ReferenceFieldName = Nothing
        Me.txtBuildQty.ReferenceTableName = Nothing
        Me.txtBuildQty.Size = New System.Drawing.Size(100, 20)
        Me.txtBuildQty.TabIndex = 10
        Me.txtBuildQty.Text = "0"
        Me.txtBuildQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtBuildQty.Value = 0R
        '
        'lblItemDesc
        '
        Me.lblItemDesc.AutoSize = False
        Me.lblItemDesc.BorderVisible = True
        Me.lblItemDesc.FieldName = Nothing
        Me.lblItemDesc.Location = New System.Drawing.Point(444, 117)
        Me.lblItemDesc.Name = "lblItemDesc"
        Me.lblItemDesc.Size = New System.Drawing.Size(254, 19)
        Me.lblItemDesc.TabIndex = 258
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
        Me.txtDescription.Location = New System.Drawing.Point(218, 205)
        Me.txtDescription.MaxLength = 49
        Me.txtDescription.MendatroryField = False
        Me.txtDescription.Multiline = True
        Me.txtDescription.MyLinkLable1 = Me.lblDescription
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReferenceFieldDesc = Nothing
        Me.txtDescription.ReferenceFieldName = Nothing
        Me.txtDescription.ReferenceTableName = Nothing
        Me.txtDescription.Size = New System.Drawing.Size(483, 48)
        Me.txtDescription.TabIndex = 11
        '
        'lblDescription
        '
        Me.lblDescription.FieldName = Nothing
        Me.lblDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescription.Location = New System.Drawing.Point(6, 221)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(51, 16)
        Me.lblDescription.TabIndex = 62
        Me.lblDescription.Text = "Remarks"
        '
        'lblFGItemCode
        '
        Me.lblFGItemCode.FieldName = Nothing
        Me.lblFGItemCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFGItemCode.Location = New System.Drawing.Point(5, 118)
        Me.lblFGItemCode.Name = "lblFGItemCode"
        Me.lblFGItemCode.Size = New System.Drawing.Size(86, 16)
        Me.lblFGItemCode.TabIndex = 59
        Me.lblFGItemCode.Text = "Item to Produce"
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(7, 73)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(19, 16)
        Me.MyLabel7.TabIndex = 52
        Me.MyLabel7.Text = "To"
        '
        'dtpToDate
        '
        Me.dtpToDate.CalculationExpression = Nothing
        Me.dtpToDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpToDate.FieldCode = Nothing
        Me.dtpToDate.FieldDesc = Nothing
        Me.dtpToDate.FieldMaxLength = 0
        Me.dtpToDate.FieldName = Nothing
        Me.dtpToDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.isCalculatedField = False
        Me.dtpToDate.IsSourceFromTable = False
        Me.dtpToDate.IsSourceFromValueList = False
        Me.dtpToDate.IsUnique = False
        Me.dtpToDate.Location = New System.Drawing.Point(219, 71)
        Me.dtpToDate.MendatroryField = True
        Me.dtpToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.MyLinkLable1 = Me.lblFromDate
        Me.dtpToDate.MyLinkLable2 = Nothing
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.ReferenceFieldDesc = Nothing
        Me.dtpToDate.ReferenceFieldName = Nothing
        Me.dtpToDate.ReferenceTableName = Nothing
        Me.dtpToDate.Size = New System.Drawing.Size(143, 18)
        Me.dtpToDate.TabIndex = 6
        Me.dtpToDate.TabStop = False
        Me.dtpToDate.Text = "03/05/2011"
        Me.dtpToDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblFromDate
        '
        Me.lblFromDate.FieldName = Nothing
        Me.lblFromDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFromDate.Location = New System.Drawing.Point(6, 52)
        Me.lblFromDate.Name = "lblFromDate"
        Me.lblFromDate.Size = New System.Drawing.Size(33, 16)
        Me.lblFromDate.TabIndex = 50
        Me.lblFromDate.Text = "From"
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CalculationExpression = Nothing
        Me.dtpFromDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpFromDate.Enabled = False
        Me.dtpFromDate.FieldCode = Nothing
        Me.dtpFromDate.FieldDesc = Nothing
        Me.dtpFromDate.FieldMaxLength = 0
        Me.dtpFromDate.FieldName = Nothing
        Me.dtpFromDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.isCalculatedField = False
        Me.dtpFromDate.IsSourceFromTable = False
        Me.dtpFromDate.IsSourceFromValueList = False
        Me.dtpFromDate.IsUnique = False
        Me.dtpFromDate.Location = New System.Drawing.Point(219, 50)
        Me.dtpFromDate.MendatroryField = True
        Me.dtpFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromDate.MyLinkLable1 = Me.lblFromDate
        Me.dtpFromDate.MyLinkLable2 = Nothing
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromDate.ReadOnly = True
        Me.dtpFromDate.ReferenceFieldDesc = Nothing
        Me.dtpFromDate.ReferenceFieldName = Nothing
        Me.dtpFromDate.ReferenceTableName = Nothing
        Me.dtpFromDate.Size = New System.Drawing.Size(143, 18)
        Me.dtpFromDate.TabIndex = 5
        Me.dtpFromDate.TabStop = False
        Me.dtpFromDate.Text = "03/05/2011"
        Me.dtpFromDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblMRPDate
        '
        Me.lblMRPDate.FieldName = Nothing
        Me.lblMRPDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMRPDate.Location = New System.Drawing.Point(8, 8)
        Me.lblMRPDate.Name = "lblMRPDate"
        Me.lblMRPDate.Size = New System.Drawing.Size(58, 16)
        Me.lblMRPDate.TabIndex = 48
        Me.lblMRPDate.Text = "MRP Date"
        '
        'dtpMRPDate
        '
        Me.dtpMRPDate.CalculationExpression = Nothing
        Me.dtpMRPDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpMRPDate.FieldCode = Nothing
        Me.dtpMRPDate.FieldDesc = Nothing
        Me.dtpMRPDate.FieldMaxLength = 0
        Me.dtpMRPDate.FieldName = Nothing
        Me.dtpMRPDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpMRPDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpMRPDate.isCalculatedField = False
        Me.dtpMRPDate.IsSourceFromTable = False
        Me.dtpMRPDate.IsSourceFromValueList = False
        Me.dtpMRPDate.IsUnique = False
        Me.dtpMRPDate.Location = New System.Drawing.Point(221, 5)
        Me.dtpMRPDate.MendatroryField = True
        Me.dtpMRPDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpMRPDate.MyLinkLable1 = Me.lblMRPDate
        Me.dtpMRPDate.MyLinkLable2 = Nothing
        Me.dtpMRPDate.Name = "dtpMRPDate"
        Me.dtpMRPDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpMRPDate.ReferenceFieldDesc = Nothing
        Me.dtpMRPDate.ReferenceFieldName = Nothing
        Me.dtpMRPDate.ReferenceTableName = Nothing
        Me.dtpMRPDate.Size = New System.Drawing.Size(143, 18)
        Me.dtpMRPDate.TabIndex = 1
        Me.dtpMRPDate.TabStop = False
        Me.dtpMRPDate.Text = "03/05/2011"
        Me.dtpMRPDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'fndItemToProduce
        '
        Me.fndItemToProduce.CalculationExpression = Nothing
        Me.fndItemToProduce.FieldCode = Nothing
        Me.fndItemToProduce.FieldDesc = Nothing
        Me.fndItemToProduce.FieldMaxLength = 0
        Me.fndItemToProduce.FieldName = Nothing
        Me.fndItemToProduce.isCalculatedField = False
        Me.fndItemToProduce.IsSourceFromTable = False
        Me.fndItemToProduce.IsSourceFromValueList = False
        Me.fndItemToProduce.IsUnique = False
        Me.fndItemToProduce.Location = New System.Drawing.Point(219, 115)
        Me.fndItemToProduce.MendatroryField = True
        Me.fndItemToProduce.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndItemToProduce.MyLinkLable1 = Me.lblFGItemCode
        Me.fndItemToProduce.MyLinkLable2 = Nothing
        Me.fndItemToProduce.MyReadOnly = False
        Me.fndItemToProduce.MyShowMasterFormButton = False
        Me.fndItemToProduce.Name = "fndItemToProduce"
        Me.fndItemToProduce.ReferenceFieldDesc = Nothing
        Me.fndItemToProduce.ReferenceFieldName = Nothing
        Me.fndItemToProduce.ReferenceTableName = Nothing
        Me.fndItemToProduce.Size = New System.Drawing.Size(219, 19)
        Me.fndItemToProduce.TabIndex = 7
        Me.fndItemToProduce.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(5, 96)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(107, 16)
        Me.MyLabel1.TabIndex = 37
        Me.MyLabel1.Text = "MRP for No of Days"
        '
        'lblNoOfDays
        '
        Me.lblNoOfDays.AutoSize = False
        Me.lblNoOfDays.BorderVisible = True
        Me.lblNoOfDays.FieldName = Nothing
        Me.lblNoOfDays.Location = New System.Drawing.Point(219, 93)
        Me.lblNoOfDays.Name = "lblNoOfDays"
        Me.lblNoOfDays.Size = New System.Drawing.Size(218, 19)
        Me.lblNoOfDays.TabIndex = 5
        '
        'pagePO
        '
        Me.pagePO.Controls.Add(Me.RadPanel1)
        Me.pagePO.ItemSize = New System.Drawing.SizeF(93.0!, 28.0!)
        Me.pagePO.Location = New System.Drawing.Point(10, 37)
        Me.pagePO.Name = "pagePO"
        Me.pagePO.Size = New System.Drawing.Size(945, 411)
        Me.pagePO.Text = "Purchase Order"
        '
        'RadPanel1
        '
        Me.RadPanel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadPanel1.Controls.Add(Me.gvPO)
        Me.RadPanel1.Location = New System.Drawing.Point(3, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(939, 408)
        Me.RadPanel1.TabIndex = 6
        Me.RadPanel1.Text = "RadPanel1"
        '
        'gvPO
        '
        Me.gvPO.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvPO.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvPO.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvPO.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvPO.ForeColor = System.Drawing.Color.Black
        Me.gvPO.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvPO.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvPO.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvPO.MasterTemplate.AllowAddNewRow = False
        Me.gvPO.MasterTemplate.AutoGenerateColumns = False
        Me.gvPO.MasterTemplate.EnableGrouping = False
        Me.gvPO.MasterTemplate.EnableSorting = False
        Me.gvPO.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvPO.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gvPO.Name = "gvPO"
        Me.gvPO.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvPO.ShowHeaderCellButtons = True
        Me.gvPO.Size = New System.Drawing.Size(939, 408)
        Me.gvPO.TabIndex = 5
        Me.gvPO.TabStop = False
        '
        'pagePendingSRN
        '
        Me.pagePendingSRN.Controls.Add(Me.gvSRN)
        Me.pagePendingSRN.ItemSize = New System.Drawing.SizeF(82.0!, 28.0!)
        Me.pagePendingSRN.Location = New System.Drawing.Point(10, 37)
        Me.pagePendingSRN.Name = "pagePendingSRN"
        Me.pagePendingSRN.Size = New System.Drawing.Size(945, 411)
        Me.pagePendingSRN.Text = "Pending SRN"
        '
        'gvSRN
        '
        Me.gvSRN.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvSRN.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvSRN.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvSRN.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvSRN.ForeColor = System.Drawing.Color.Black
        Me.gvSRN.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvSRN.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvSRN.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvSRN.MasterTemplate.AllowAddNewRow = False
        Me.gvSRN.MasterTemplate.AutoGenerateColumns = False
        Me.gvSRN.MasterTemplate.EnableGrouping = False
        Me.gvSRN.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvSRN.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gvSRN.Name = "gvSRN"
        Me.gvSRN.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvSRN.ShowHeaderCellButtons = True
        Me.gvSRN.Size = New System.Drawing.Size(945, 411)
        Me.gvSRN.TabIndex = 4
        Me.gvSRN.TabStop = False
        '
        'pageMRPDetail
        '
        Me.pageMRPDetail.Controls.Add(Me.gvMRPDetal)
        Me.pageMRPDetail.ItemSize = New System.Drawing.SizeF(72.0!, 28.0!)
        Me.pageMRPDetail.Location = New System.Drawing.Point(10, 37)
        Me.pageMRPDetail.Name = "pageMRPDetail"
        Me.pageMRPDetail.Size = New System.Drawing.Size(965, 420)
        Me.pageMRPDetail.Text = "MRP Detail"
        '
        'gvMRPDetal
        '
        Me.gvMRPDetal.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvMRPDetal.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvMRPDetal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvMRPDetal.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvMRPDetal.ForeColor = System.Drawing.Color.Black
        Me.gvMRPDetal.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvMRPDetal.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvMRPDetal.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvMRPDetal.MasterTemplate.AllowAddNewRow = False
        Me.gvMRPDetal.MasterTemplate.AutoGenerateColumns = False
        Me.gvMRPDetal.MasterTemplate.EnableGrouping = False
        Me.gvMRPDetal.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvMRPDetal.MasterTemplate.ViewDefinition = TableViewDefinition3
        Me.gvMRPDetal.Name = "gvMRPDetal"
        Me.gvMRPDetal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvMRPDetal.ShowHeaderCellButtons = True
        Me.gvMRPDetal.Size = New System.Drawing.Size(965, 420)
        Me.gvMRPDetal.TabIndex = 5
        Me.gvMRPDetal.TabStop = False
        '
        'btnUnpost
        '
        Me.btnUnpost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnUnpost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUnpost.Location = New System.Drawing.Point(419, 5)
        Me.btnUnpost.Name = "btnUnpost"
        Me.btnUnpost.Size = New System.Drawing.Size(69, 22)
        Me.btnUnpost.TabIndex = 7
        Me.btnUnpost.Text = "Reverse"
        '
        'btnNext
        '
        Me.btnNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnNext.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNext.Location = New System.Drawing.Point(7, 5)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(68, 22)
        Me.btnNext.TabIndex = 5
        Me.btnNext.Text = ">>"
        '
        'btnRequisition
        '
        Me.btnRequisition.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRequisition.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRequisition.Location = New System.Drawing.Point(300, 5)
        Me.btnRequisition.Name = "btnRequisition"
        Me.btnRequisition.Size = New System.Drawing.Size(113, 22)
        Me.btnRequisition.TabIndex = 3
        Me.btnRequisition.Text = "Create Requisition"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(228, 5)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(68, 22)
        Me.btnPost.TabIndex = 2
        Me.btnPost.Text = "Post"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(80, 5)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(68, 22)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(911, 5)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(68, 22)
        Me.btnclose.TabIndex = 4
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(154, 5)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(68, 22)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'frmMRP
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(986, 548)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmMRP"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "MRP"
        CType(Me.txtMRPDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblempcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.pageGeneral.ResumeLayout(False)
        Me.pageGeneral.PerformLayout()
        CType(Me.txtProductionPlanDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPackSizeDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPackSize, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPendingSRN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIncludePO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBOMDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBuildQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblUnitName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBuildQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblItemDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFGItemCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMRPDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpMRPDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNoOfDays, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pagePO.ResumeLayout(False)
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        CType(Me.gvPO.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvPO, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pagePendingSRN.ResumeLayout(False)
        CType(Me.gvSRN.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvSRN, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pageMRPDetail.ResumeLayout(False)
        CType(Me.gvMRPDetal.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvMRPDetal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUnpost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNext, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRequisition, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents gvSRN As common.UserControls.MyRadGridView
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblNoOfDays As common.Controls.MyLabel
    Friend WithEvents btnRequisition As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents pageGeneral As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents pagePO As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents pagePendingSRN As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvPO As common.UserControls.MyRadGridView
    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents fndItemToProduce As common.UserControls.txtFinder
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents lblempcode As common.Controls.MyLabel
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblMRPDate As common.Controls.MyLabel
    Friend WithEvents dtpMRPDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblFromDate As common.Controls.MyLabel
    Friend WithEvents dtpFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents pageMRPDetail As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents dtpToDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblFGItemCode As common.Controls.MyLabel
    Friend WithEvents gvMRPDetal As common.UserControls.MyRadGridView
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents lblDescription As common.Controls.MyLabel
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents txtMRPDescription As common.Controls.MyTextBox
    Friend WithEvents lblItemDesc As common.Controls.MyLabel
    Friend WithEvents lblUnitName As common.Controls.MyLabel
    Friend WithEvents txtBuildQty As common.MyNumBox
    Friend WithEvents lblBuildQty As common.Controls.MyLabel
    Friend WithEvents lblBOMDesc As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents fndBOM As common.UserControls.txtFinder
    Friend WithEvents chkIncludePO As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkPendingSRN As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents lblLocationDesc As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents fndLocation As common.UserControls.txtFinder
    Friend WithEvents btnNext As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtPackSize As common.MyNumBox
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblPackSizeDesc As common.Controls.MyLabel
    Friend WithEvents txtProductionPlanDesc As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents fndProductionPlan As common.UserControls.txtFinder
    Friend WithEvents btnUnpost As RadButton
End Class
