Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmItemIssueToAssembledAsset
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
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.lblAssetType = New common.Controls.MyLabel()
        Me.cboAssetType = New common.Controls.MyComboBox()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.UcItemBalance1 = New XpertERPEngine.ucItemBalance()
        Me.txtHCode = New common.UserControls.txtFinder()
        Me.lblHDesc = New common.Controls.MyLabel()
        Me.chkWithoutRefNo = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtComment = New common.Controls.MyTextBox()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.lblReq = New common.Controls.MyLabel()
        Me.fndReqNo = New common.UserControls.txtFinder()
        Me.lblReq3 = New common.Controls.MyLabel()
        Me.lblReqDate = New common.Controls.MyLabel()
        Me.lblReq2 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.RadLabel6 = New common.Controls.MyLabel()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.RadLabel9 = New common.Controls.MyLabel()
        Me.fndAssetCode = New common.UserControls.txtFinder()
        Me.lblAssetDesc = New common.Controls.MyLabel()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.lblRequestBy = New common.Controls.MyLabel()
        Me.chkOnHold = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadLabel15 = New common.Controls.MyLabel()
        Me.lblFromLocation = New common.Controls.MyLabel()
        Me.RadLabel8 = New common.Controls.MyLabel()
        Me.RadLabel29 = New common.Controls.MyLabel()
        Me.txtRequestBy = New common.UserControls.txtFinder()
        Me.txtRemarks = New common.Controls.MyTextBox()
        Me.cboDocType = New common.Controls.MyComboBox()
        Me.txtFromLocation = New common.UserControls.txtFinder()
        Me.UsLock1 = New common.usLock()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.txtTaxGroup = New common.UserControls.txtFinder()
        Me.lblTaxGrpName = New common.Controls.MyLabel()
        Me.RadLabel10 = New common.Controls.MyLabel()
        Me.gv2 = New common.UserControls.MyRadGridView()
        Me.RadLabel11 = New common.Controls.MyLabel()
        Me.pvpCustomFields = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcCustomFields1 = New ucCustomFields()
        Me.Attachments = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadLabel7 = New common.Controls.MyLabel()
        Me.RadLabel27 = New common.Controls.MyLabel()
        Me.lblTotRAmt = New common.Controls.MyLabel()
        Me.RadLabel25 = New common.Controls.MyLabel()
        Me.lblTaxAmt = New common.Controls.MyLabel()
        Me.lblAmtAfterDiscount = New common.Controls.MyLabel()
        Me.btnReverse = New Telerik.WinControls.UI.RadButton()
        Me.btnprint = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtCostCenter = New common.UserControls.txtFinder()
        Me.lblCostCenterDesc = New common.Controls.MyLabel()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.lblAssetType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboAssetType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblHDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkWithoutRefNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtComment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblReq, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblReq3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblReqDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblReq2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gv1.SuspendLayout()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAssetDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRequestBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkOnHold, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFromLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel29, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboDocType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.lblTaxGrpName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pvpCustomFields.SuspendLayout()
        Me.Attachments.SuspendLayout()
        Me.RadPageViewPage3.SuspendLayout()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel27, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotRAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTaxAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAmtAfterDiscount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCostCenterDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SplitContainer1.IsSplitterFixed = True
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnprint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(839, 567)
        Me.SplitContainer1.SplitterDistance = 533
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.pvpCustomFields)
        Me.RadPageView1.Controls.Add(Me.Attachments)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(839, 533)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.GroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.lblAssetType)
        Me.RadPageViewPage1.Controls.Add(Me.cboAssetType)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.UcItemBalance1)
        Me.RadPageViewPage1.Controls.Add(Me.chkWithoutRefNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtComment)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.lblReq)
        Me.RadPageViewPage1.Controls.Add(Me.fndReqNo)
        Me.RadPageViewPage1.Controls.Add(Me.lblReq3)
        Me.RadPageViewPage1.Controls.Add(Me.lblReqDate)
        Me.RadPageViewPage1.Controls.Add(Me.lblReq2)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel6)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.lblRequestBy)
        Me.RadPageViewPage1.Controls.Add(Me.chkOnHold)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel15)
        Me.RadPageViewPage1.Controls.Add(Me.lblFromLocation)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel8)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel29)
        Me.RadPageViewPage1.Controls.Add(Me.txtRequestBy)
        Me.RadPageViewPage1.Controls.Add(Me.txtRemarks)
        Me.RadPageViewPage1.Controls.Add(Me.cboDocType)
        Me.RadPageViewPage1.Controls.Add(Me.txtFromLocation)
        Me.RadPageViewPage1.Controls.Add(Me.UsLock1)
        Me.RadPageViewPage1.Controls.Add(Me.txtDocNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtDate)
        Me.RadPageViewPage1.Controls.Add(Me.btnAddNew)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(135.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(818, 485)
        Me.RadPageViewPage1.Text = "IssueTo Assemble Asset"
        Me.RadPageViewPage1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'lblAssetType
        '
        Me.lblAssetType.FieldName = Nothing
        Me.lblAssetType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAssetType.Location = New System.Drawing.Point(4, 73)
        Me.lblAssetType.Name = "lblAssetType"
        Me.lblAssetType.Size = New System.Drawing.Size(63, 16)
        Me.lblAssetType.TabIndex = 60
        Me.lblAssetType.Text = "Asset Type"
        '
        'cboAssetType
        '
        Me.cboAssetType.AutoCompleteDisplayMember = Nothing
        Me.cboAssetType.AutoCompleteValueMember = Nothing
        Me.cboAssetType.CalculationExpression = Nothing
        Me.cboAssetType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboAssetType.FieldCode = Nothing
        Me.cboAssetType.FieldDesc = Nothing
        Me.cboAssetType.FieldMaxLength = 0
        Me.cboAssetType.FieldName = Nothing
        Me.cboAssetType.isCalculatedField = False
        Me.cboAssetType.IsSourceFromTable = False
        Me.cboAssetType.IsSourceFromValueList = False
        Me.cboAssetType.IsUnique = False
        Me.cboAssetType.Location = New System.Drawing.Point(93, 70)
        Me.cboAssetType.MendatroryField = True
        Me.cboAssetType.MyLinkLable1 = Me.lblAssetType
        Me.cboAssetType.MyLinkLable2 = Nothing
        Me.cboAssetType.Name = "cboAssetType"
        Me.cboAssetType.ReferenceFieldDesc = Nothing
        Me.cboAssetType.ReferenceFieldName = Nothing
        Me.cboAssetType.ReferenceTableName = Nothing
        Me.cboAssetType.Size = New System.Drawing.Size(105, 20)
        Me.cboAssetType.TabIndex = 59
        '
        'MyLabel5
        '
        Me.MyLabel5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.ForeColor = System.Drawing.SystemColors.Highlight
        Me.MyLabel5.Location = New System.Drawing.Point(628, 471)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(197, 16)
        Me.MyLabel5.TabIndex = 58
        Me.MyLabel5.Text = "Press F4 to open serial item Details"
        '
        'UcItemBalance1
        '
        Me.UcItemBalance1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.UcItemBalance1.CommitedQty = False
        Me.UcItemBalance1.CommitedQtyLbl = False
        Me.UcItemBalance1.ItemCode = ""
        Me.UcItemBalance1.ItemMRP = 0.0R
        Me.UcItemBalance1.ItemName = ""
        Me.UcItemBalance1.Location = New System.Drawing.Point(3, 411)
        Me.UcItemBalance1.LocationCode = ""
        Me.UcItemBalance1.LocationName = ""
        Me.UcItemBalance1.MaximumSize = New System.Drawing.Size(710, 70)
        Me.UcItemBalance1.MinimumSize = New System.Drawing.Size(710, 70)
        Me.UcItemBalance1.Name = "UcItemBalance1"
        Me.UcItemBalance1.ShowCSADOQty = False
        Me.UcItemBalance1.ShowPOQty = False
        Me.UcItemBalance1.ShowSOQty = False
        Me.UcItemBalance1.Size = New System.Drawing.Size(710, 70)
        Me.UcItemBalance1.TabIndex = 42
        Me.UcItemBalance1.TransDate = New Date(CType(0, Long))
        Me.UcItemBalance1.TransNo = ""
        Me.UcItemBalance1.UOM = ""
        '
        'txtHCode
        '
        Me.txtHCode.CalculationExpression = Nothing
        Me.txtHCode.FieldCode = Nothing
        Me.txtHCode.FieldDesc = Nothing
        Me.txtHCode.FieldMaxLength = 0
        Me.txtHCode.FieldName = Nothing
        Me.txtHCode.isCalculatedField = False
        Me.txtHCode.IsSourceFromTable = False
        Me.txtHCode.IsSourceFromValueList = False
        Me.txtHCode.IsUnique = False
        Me.txtHCode.Location = New System.Drawing.Point(102, 17)
        Me.txtHCode.MendatroryField = False
        Me.txtHCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHCode.MyLinkLable2 = Me.lblHDesc
        Me.txtHCode.MyReadOnly = False
        Me.txtHCode.MyShowMasterFormButton = False
        Me.txtHCode.Name = "txtHCode"
        Me.txtHCode.ReferenceFieldDesc = Nothing
        Me.txtHCode.ReferenceFieldName = Nothing
        Me.txtHCode.ReferenceTableName = Nothing
        Me.txtHCode.Size = New System.Drawing.Size(144, 19)
        Me.txtHCode.TabIndex = 2
        Me.txtHCode.Value = ""
        '
        'lblHDesc
        '
        Me.lblHDesc.AutoSize = False
        Me.lblHDesc.BorderVisible = True
        Me.lblHDesc.FieldName = Nothing
        Me.lblHDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHDesc.Location = New System.Drawing.Point(252, 18)
        Me.lblHDesc.Name = "lblHDesc"
        Me.lblHDesc.Size = New System.Drawing.Size(353, 18)
        Me.lblHDesc.TabIndex = 3
        Me.lblHDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblHDesc.TextWrap = False
        '
        'chkWithoutRefNo
        '
        Me.chkWithoutRefNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkWithoutRefNo.Location = New System.Drawing.Point(575, 29)
        Me.chkWithoutRefNo.Name = "chkWithoutRefNo"
        Me.chkWithoutRefNo.Size = New System.Drawing.Size(132, 16)
        Me.chkWithoutRefNo.TabIndex = 41
        Me.chkWithoutRefNo.Text = "Without Reference No"
        '
        'txtComment
        '
        Me.txtComment.AutoSize = False
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
        Me.txtComment.Location = New System.Drawing.Point(93, 149)
        Me.txtComment.MaxLength = 200
        Me.txtComment.MendatroryField = False
        Me.txtComment.Multiline = True
        Me.txtComment.MyLinkLable1 = Me.RadLabel3
        Me.txtComment.MyLinkLable2 = Nothing
        Me.txtComment.Name = "txtComment"
        Me.txtComment.ReferenceFieldDesc = Nothing
        Me.txtComment.ReferenceFieldName = Nothing
        Me.txtComment.ReferenceTableName = Nothing
        Me.txtComment.Size = New System.Drawing.Size(720, 33)
        Me.txtComment.TabIndex = 40
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(4, 166)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(55, 16)
        Me.RadLabel3.TabIndex = 39
        Me.RadLabel3.Text = "Comment"
        '
        'lblReq
        '
        Me.lblReq.FieldName = Nothing
        Me.lblReq.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReq.Location = New System.Drawing.Point(4, 48)
        Me.lblReq.Name = "lblReq"
        Me.lblReq.Size = New System.Drawing.Size(80, 16)
        Me.lblReq.TabIndex = 35
        Me.lblReq.Text = "Requisition No"
        '
        'fndReqNo
        '
        Me.fndReqNo.CalculationExpression = Nothing
        Me.fndReqNo.FieldCode = Nothing
        Me.fndReqNo.FieldDesc = Nothing
        Me.fndReqNo.FieldMaxLength = 0
        Me.fndReqNo.FieldName = Nothing
        Me.fndReqNo.isCalculatedField = False
        Me.fndReqNo.IsSourceFromTable = False
        Me.fndReqNo.IsSourceFromValueList = False
        Me.fndReqNo.IsUnique = False
        Me.fndReqNo.Location = New System.Drawing.Point(93, 46)
        Me.fndReqNo.MendatroryField = False
        Me.fndReqNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndReqNo.MyLinkLable1 = Me.lblReq
        Me.fndReqNo.MyLinkLable2 = Me.lblHDesc
        Me.fndReqNo.MyReadOnly = False
        Me.fndReqNo.MyShowMasterFormButton = False
        Me.fndReqNo.Name = "fndReqNo"
        Me.fndReqNo.ReferenceFieldDesc = Nothing
        Me.fndReqNo.ReferenceFieldName = Nothing
        Me.fndReqNo.ReferenceTableName = Nothing
        Me.fndReqNo.Size = New System.Drawing.Size(103, 20)
        Me.fndReqNo.TabIndex = 34
        Me.fndReqNo.Value = ""
        '
        'lblReq3
        '
        Me.lblReq3.AutoSize = False
        Me.lblReq3.BorderVisible = True
        Me.lblReq3.FieldName = Nothing
        Me.lblReq3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReq3.Location = New System.Drawing.Point(466, 28)
        Me.lblReq3.Name = "lblReq3"
        Me.lblReq3.Size = New System.Drawing.Size(103, 18)
        Me.lblReq3.TabIndex = 5
        Me.lblReq3.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblReq3.TextWrap = False
        Me.lblReq3.Visible = False
        '
        'lblReqDate
        '
        Me.lblReqDate.AutoSize = False
        Me.lblReqDate.BorderVisible = True
        Me.lblReqDate.FieldName = Nothing
        Me.lblReqDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReqDate.Location = New System.Drawing.Point(202, 47)
        Me.lblReqDate.Name = "lblReqDate"
        Me.lblReqDate.Size = New System.Drawing.Size(170, 18)
        Me.lblReqDate.TabIndex = 4
        Me.lblReqDate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblReqDate.TextWrap = False
        '
        'lblReq2
        '
        Me.lblReq2.FieldName = Nothing
        Me.lblReq2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReq2.Location = New System.Drawing.Point(382, 29)
        Me.lblReq2.Name = "lblReq2"
        Me.lblReq2.Size = New System.Drawing.Size(80, 16)
        Me.lblReq2.TabIndex = 36
        Me.lblReq2.Text = "Requisition No"
        Me.lblReq2.Visible = False
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(4, 4)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(75, 16)
        Me.RadLabel1.TabIndex = 26
        Me.RadLabel1.Text = "Document No"
        '
        'RadLabel6
        '
        Me.RadLabel6.FieldName = Nothing
        Me.RadLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel6.Location = New System.Drawing.Point(4, 202)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(51, 16)
        Me.RadLabel6.TabIndex = 24
        Me.RadLabel6.Text = "Remarks"
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
        Me.RadGroupBox2.Location = New System.Drawing.Point(2, 215)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(815, 196)
        Me.RadGroupBox2.TabIndex = 18
        Me.RadGroupBox2.Text = "Item Details"
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Controls.Add(Me.RadLabel9)
        Me.gv1.Controls.Add(Me.fndAssetCode)
        Me.gv1.Controls.Add(Me.lblAssetDesc)
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(10, 20)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(795, 166)
        Me.gv1.TabIndex = 0
        Me.gv1.Text = "RadGridView1"
        '
        'RadLabel9
        '
        Me.RadLabel9.FieldName = Nothing
        Me.RadLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel9.Location = New System.Drawing.Point(216, 119)
        Me.RadLabel9.Name = "RadLabel9"
        Me.RadLabel9.Size = New System.Drawing.Size(65, 16)
        Me.RadLabel9.TabIndex = 21
        Me.RadLabel9.Text = "Asset Code"
        Me.RadLabel9.Visible = False
        '
        'fndAssetCode
        '
        Me.fndAssetCode.CalculationExpression = Nothing
        Me.fndAssetCode.FieldCode = Nothing
        Me.fndAssetCode.FieldDesc = Nothing
        Me.fndAssetCode.FieldMaxLength = 0
        Me.fndAssetCode.FieldName = Nothing
        Me.fndAssetCode.isCalculatedField = False
        Me.fndAssetCode.IsSourceFromTable = False
        Me.fndAssetCode.IsSourceFromValueList = False
        Me.fndAssetCode.IsUnique = False
        Me.fndAssetCode.Location = New System.Drawing.Point(306, 118)
        Me.fndAssetCode.MendatroryField = True
        Me.fndAssetCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndAssetCode.MyLinkLable1 = Me.RadLabel9
        Me.fndAssetCode.MyLinkLable2 = Me.lblAssetDesc
        Me.fndAssetCode.MyReadOnly = False
        Me.fndAssetCode.MyShowMasterFormButton = False
        Me.fndAssetCode.Name = "fndAssetCode"
        Me.fndAssetCode.ReferenceFieldDesc = Nothing
        Me.fndAssetCode.ReferenceFieldName = Nothing
        Me.fndAssetCode.ReferenceTableName = Nothing
        Me.fndAssetCode.Size = New System.Drawing.Size(103, 18)
        Me.fndAssetCode.TabIndex = 8
        Me.fndAssetCode.Value = ""
        Me.fndAssetCode.Visible = False
        '
        'lblAssetDesc
        '
        Me.lblAssetDesc.AutoSize = False
        Me.lblAssetDesc.BorderVisible = True
        Me.lblAssetDesc.FieldName = Nothing
        Me.lblAssetDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAssetDesc.Location = New System.Drawing.Point(413, 118)
        Me.lblAssetDesc.Name = "lblAssetDesc"
        Me.lblAssetDesc.Size = New System.Drawing.Size(171, 18)
        Me.lblAssetDesc.TabIndex = 9
        Me.lblAssetDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblAssetDesc.TextWrap = False
        Me.lblAssetDesc.Visible = False
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(377, 4)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 14
        Me.RadLabel4.Text = "Date"
        '
        'lblRequestBy
        '
        Me.lblRequestBy.AutoSize = False
        Me.lblRequestBy.BorderVisible = True
        Me.lblRequestBy.FieldName = Nothing
        Me.lblRequestBy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRequestBy.Location = New System.Drawing.Point(574, 50)
        Me.lblRequestBy.Name = "lblRequestBy"
        Me.lblRequestBy.Size = New System.Drawing.Size(238, 18)
        Me.lblRequestBy.TabIndex = 5
        Me.lblRequestBy.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblRequestBy.TextWrap = False
        '
        'chkOnHold
        '
        Me.chkOnHold.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOnHold.Location = New System.Drawing.Point(747, 29)
        Me.chkOnHold.Name = "chkOnHold"
        Me.chkOnHold.Size = New System.Drawing.Size(62, 16)
        Me.chkOnHold.TabIndex = 19
        Me.chkOnHold.Text = "On Hold"
        '
        'RadLabel15
        '
        Me.RadLabel15.FieldName = Nothing
        Me.RadLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel15.Location = New System.Drawing.Point(383, 51)
        Me.RadLabel15.Name = "RadLabel15"
        Me.RadLabel15.Size = New System.Drawing.Size(65, 16)
        Me.RadLabel15.TabIndex = 18
        Me.RadLabel15.Text = "Request By"
        '
        'lblFromLocation
        '
        Me.lblFromLocation.AutoSize = False
        Me.lblFromLocation.BorderVisible = True
        Me.lblFromLocation.FieldName = Nothing
        Me.lblFromLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFromLocation.Location = New System.Drawing.Point(202, 26)
        Me.lblFromLocation.Name = "lblFromLocation"
        Me.lblFromLocation.Size = New System.Drawing.Size(170, 18)
        Me.lblFromLocation.TabIndex = 7
        Me.lblFromLocation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblFromLocation.TextWrap = False
        '
        'RadLabel8
        '
        Me.RadLabel8.FieldName = Nothing
        Me.RadLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel8.Location = New System.Drawing.Point(4, 26)
        Me.RadLabel8.Name = "RadLabel8"
        Me.RadLabel8.Size = New System.Drawing.Size(49, 16)
        Me.RadLabel8.TabIndex = 20
        Me.RadLabel8.Text = "Location"
        '
        'RadLabel29
        '
        Me.RadLabel29.FieldName = Nothing
        Me.RadLabel29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel29.Location = New System.Drawing.Point(508, 4)
        Me.RadLabel29.Name = "RadLabel29"
        Me.RadLabel29.Size = New System.Drawing.Size(58, 16)
        Me.RadLabel29.TabIndex = 15
        Me.RadLabel29.Text = "Doc. Type"
        '
        'txtRequestBy
        '
        Me.txtRequestBy.CalculationExpression = Nothing
        Me.txtRequestBy.FieldCode = Nothing
        Me.txtRequestBy.FieldDesc = Nothing
        Me.txtRequestBy.FieldMaxLength = 0
        Me.txtRequestBy.FieldName = Nothing
        Me.txtRequestBy.isCalculatedField = False
        Me.txtRequestBy.IsSourceFromTable = False
        Me.txtRequestBy.IsSourceFromValueList = False
        Me.txtRequestBy.IsUnique = False
        Me.txtRequestBy.Location = New System.Drawing.Point(465, 50)
        Me.txtRequestBy.MendatroryField = False
        Me.txtRequestBy.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRequestBy.MyLinkLable1 = Me.RadLabel15
        Me.txtRequestBy.MyLinkLable2 = Me.lblRequestBy
        Me.txtRequestBy.MyReadOnly = False
        Me.txtRequestBy.MyShowMasterFormButton = False
        Me.txtRequestBy.Name = "txtRequestBy"
        Me.txtRequestBy.ReferenceFieldDesc = Nothing
        Me.txtRequestBy.ReferenceFieldName = Nothing
        Me.txtRequestBy.ReferenceTableName = Nothing
        Me.txtRequestBy.Size = New System.Drawing.Size(103, 18)
        Me.txtRequestBy.TabIndex = 4
        Me.txtRequestBy.Value = ""
        '
        'txtRemarks
        '
        Me.txtRemarks.AutoSize = False
        Me.txtRemarks.CalculationExpression = Nothing
        Me.txtRemarks.FieldCode = Nothing
        Me.txtRemarks.FieldDesc = Nothing
        Me.txtRemarks.FieldMaxLength = 0
        Me.txtRemarks.FieldName = Nothing
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.isCalculatedField = False
        Me.txtRemarks.IsSourceFromTable = False
        Me.txtRemarks.IsSourceFromValueList = False
        Me.txtRemarks.IsUnique = False
        Me.txtRemarks.Location = New System.Drawing.Point(93, 185)
        Me.txtRemarks.MaxLength = 200
        Me.txtRemarks.MendatroryField = False
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.MyLinkLable1 = Me.RadLabel6
        Me.txtRemarks.MyLinkLable2 = Nothing
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.ReferenceFieldDesc = Nothing
        Me.txtRemarks.ReferenceFieldName = Nothing
        Me.txtRemarks.ReferenceTableName = Nothing
        Me.txtRemarks.Size = New System.Drawing.Size(720, 33)
        Me.txtRemarks.TabIndex = 17
        '
        'cboDocType
        '
        Me.cboDocType.AutoCompleteDisplayMember = Nothing
        Me.cboDocType.AutoCompleteValueMember = Nothing
        Me.cboDocType.CalculationExpression = Nothing
        Me.cboDocType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboDocType.FieldCode = Nothing
        Me.cboDocType.FieldDesc = Nothing
        Me.cboDocType.FieldMaxLength = 0
        Me.cboDocType.FieldName = Nothing
        Me.cboDocType.isCalculatedField = False
        Me.cboDocType.IsSourceFromTable = False
        Me.cboDocType.IsSourceFromValueList = False
        Me.cboDocType.IsUnique = False
        Me.cboDocType.Location = New System.Drawing.Point(575, 2)
        Me.cboDocType.MendatroryField = True
        Me.cboDocType.MyLinkLable1 = Me.RadLabel29
        Me.cboDocType.MyLinkLable2 = Nothing
        Me.cboDocType.Name = "cboDocType"
        Me.cboDocType.ReferenceFieldDesc = Nothing
        Me.cboDocType.ReferenceFieldName = Nothing
        Me.cboDocType.ReferenceTableName = Nothing
        Me.cboDocType.Size = New System.Drawing.Size(134, 20)
        Me.cboDocType.TabIndex = 1
        '
        'txtFromLocation
        '
        Me.txtFromLocation.CalculationExpression = Nothing
        Me.txtFromLocation.FieldCode = Nothing
        Me.txtFromLocation.FieldDesc = Nothing
        Me.txtFromLocation.FieldMaxLength = 0
        Me.txtFromLocation.FieldName = Nothing
        Me.txtFromLocation.isCalculatedField = False
        Me.txtFromLocation.IsSourceFromTable = False
        Me.txtFromLocation.IsSourceFromValueList = False
        Me.txtFromLocation.IsUnique = False
        Me.txtFromLocation.Location = New System.Drawing.Point(93, 26)
        Me.txtFromLocation.MendatroryField = True
        Me.txtFromLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFromLocation.MyLinkLable1 = Me.RadLabel8
        Me.txtFromLocation.MyLinkLable2 = Me.lblFromLocation
        Me.txtFromLocation.MyReadOnly = False
        Me.txtFromLocation.MyShowMasterFormButton = False
        Me.txtFromLocation.Name = "txtFromLocation"
        Me.txtFromLocation.ReferenceFieldDesc = Nothing
        Me.txtFromLocation.ReferenceFieldName = Nothing
        Me.txtFromLocation.ReferenceTableName = Nothing
        Me.txtFromLocation.Size = New System.Drawing.Size(103, 18)
        Me.txtFromLocation.TabIndex = 6
        Me.txtFromLocation.Value = ""
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(715, 2)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(97, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 16
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(93, 2)
        Me.txtDocNo.MendatroryField = False
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Me.RadLabel1
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 32767
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(260, 20)
        Me.txtDocNo.TabIndex = 27
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
        Me.txtDate.Location = New System.Drawing.Point(409, 3)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.RadLabel4
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(77, 18)
        Me.txtDate.TabIndex = 0
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.XpertERPFixedAssets.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(351, 2)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(21, 21)
        Me.btnAddNew.TabIndex = 28
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.txtTaxGroup)
        Me.RadPageViewPage2.Controls.Add(Me.RadLabel10)
        Me.RadPageViewPage2.Controls.Add(Me.gv2)
        Me.RadPageViewPage2.Controls.Add(Me.lblTaxGrpName)
        Me.RadPageViewPage2.Controls.Add(Me.RadLabel11)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(70.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(818, 485)
        Me.RadPageViewPage2.Text = "Tax Details"
        '
        'txtTaxGroup
        '
        Me.txtTaxGroup.CalculationExpression = Nothing
        Me.txtTaxGroup.FieldCode = Nothing
        Me.txtTaxGroup.FieldDesc = Nothing
        Me.txtTaxGroup.FieldMaxLength = 0
        Me.txtTaxGroup.FieldName = Nothing
        Me.txtTaxGroup.isCalculatedField = False
        Me.txtTaxGroup.IsSourceFromTable = False
        Me.txtTaxGroup.IsSourceFromValueList = False
        Me.txtTaxGroup.IsUnique = False
        Me.txtTaxGroup.Location = New System.Drawing.Point(67, 3)
        Me.txtTaxGroup.MendatroryField = True
        Me.txtTaxGroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTaxGroup.MyLinkLable1 = Me.lblTaxGrpName
        Me.txtTaxGroup.MyLinkLable2 = Nothing
        Me.txtTaxGroup.MyReadOnly = False
        Me.txtTaxGroup.MyShowMasterFormButton = False
        Me.txtTaxGroup.Name = "txtTaxGroup"
        Me.txtTaxGroup.ReferenceFieldDesc = Nothing
        Me.txtTaxGroup.ReferenceFieldName = Nothing
        Me.txtTaxGroup.ReferenceTableName = Nothing
        Me.txtTaxGroup.Size = New System.Drawing.Size(143, 19)
        Me.txtTaxGroup.TabIndex = 0
        Me.txtTaxGroup.Value = ""
        '
        'lblTaxGrpName
        '
        Me.lblTaxGrpName.AutoSize = False
        Me.lblTaxGrpName.BorderVisible = True
        Me.lblTaxGrpName.FieldName = Nothing
        Me.lblTaxGrpName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTaxGrpName.Location = New System.Drawing.Point(218, 2)
        Me.lblTaxGrpName.Name = "lblTaxGrpName"
        Me.lblTaxGrpName.Size = New System.Drawing.Size(321, 20)
        Me.lblTaxGrpName.TabIndex = 1
        Me.lblTaxGrpName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTaxGrpName.TextWrap = False
        '
        'RadLabel10
        '
        Me.RadLabel10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadLabel10.FieldName = Nothing
        Me.RadLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel10.ForeColor = System.Drawing.SystemColors.Highlight
        Me.RadLabel10.Location = New System.Drawing.Point(660, 468)
        Me.RadLabel10.Name = "RadLabel10"
        Me.RadLabel10.Size = New System.Drawing.Size(155, 16)
        Me.RadLabel10.TabIndex = 3
        Me.RadLabel10.Text = "Double click To Chage Rate"
        '
        'gv2
        '
        Me.gv2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gv2.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv2.BeginEditMode = Telerik.WinControls.RadGridViewBeginEditMode.BeginEditOnEnter
        Me.gv2.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv2.ForeColor = System.Drawing.Color.Black
        Me.gv2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv2.Location = New System.Drawing.Point(0, 29)
        '
        '
        '
        Me.gv2.MasterTemplate.AllowAddNewRow = False
        Me.gv2.MasterTemplate.AllowDeleteRow = False
        Me.gv2.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv2.Name = "gv2"
        Me.gv2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv2.ShowHeaderCellButtons = True
        Me.gv2.Size = New System.Drawing.Size(815, 434)
        Me.gv2.TabIndex = 2
        Me.gv2.Text = "RadGridView1"
        '
        'RadLabel11
        '
        Me.RadLabel11.FieldName = Nothing
        Me.RadLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel11.Location = New System.Drawing.Point(1, 4)
        Me.RadLabel11.Name = "RadLabel11"
        Me.RadLabel11.Size = New System.Drawing.Size(60, 16)
        Me.RadLabel11.TabIndex = 4
        Me.RadLabel11.Text = "Tax Group"
        '
        'pvpCustomFields
        '
        Me.pvpCustomFields.Controls.Add(Me.UcCustomFields1)
        Me.pvpCustomFields.ItemSize = New System.Drawing.SizeF(86.0!, 28.0!)
        Me.pvpCustomFields.Location = New System.Drawing.Point(10, 37)
        Me.pvpCustomFields.Name = "pvpCustomFields"
        Me.pvpCustomFields.Size = New System.Drawing.Size(818, 485)
        Me.pvpCustomFields.Text = "Custom Fields"
        '
        'UcCustomFields1
        '
        Me.UcCustomFields1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcCustomFields1.Location = New System.Drawing.Point(0, 0)
        Me.UcCustomFields1.Name = "UcCustomFields1"
        Me.UcCustomFields1.Size = New System.Drawing.Size(818, 485)
        Me.UcCustomFields1.TabIndex = 1
        '
        'Attachments
        '
        Me.Attachments.Controls.Add(Me.UcAttachment1)
        Me.Attachments.ItemSize = New System.Drawing.SizeF(79.0!, 28.0!)
        Me.Attachments.Location = New System.Drawing.Point(10, 37)
        Me.Attachments.Name = "Attachments"
        Me.Attachments.Size = New System.Drawing.Size(818, 485)
        Me.Attachments.Text = "Attachments"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(818, 485)
        Me.UcAttachment1.TabIndex = 0
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.RadLabel7)
        Me.RadPageViewPage3.Controls.Add(Me.RadLabel27)
        Me.RadPageViewPage3.Controls.Add(Me.lblTotRAmt)
        Me.RadPageViewPage3.Controls.Add(Me.RadLabel25)
        Me.RadPageViewPage3.Controls.Add(Me.lblTaxAmt)
        Me.RadPageViewPage3.Controls.Add(Me.lblAmtAfterDiscount)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(818, 485)
        Me.RadPageViewPage3.Text = "Total"
        '
        'RadLabel7
        '
        Me.RadLabel7.FieldName = Nothing
        Me.RadLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel7.Location = New System.Drawing.Point(90, 23)
        Me.RadLabel7.Name = "RadLabel7"
        Me.RadLabel7.Size = New System.Drawing.Size(45, 16)
        Me.RadLabel7.TabIndex = 3
        Me.RadLabel7.Text = "Amount"
        '
        'RadLabel27
        '
        Me.RadLabel27.FieldName = Nothing
        Me.RadLabel27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel27.Location = New System.Drawing.Point(35, 83)
        Me.RadLabel27.Name = "RadLabel27"
        Me.RadLabel27.Size = New System.Drawing.Size(100, 16)
        Me.RadLabel27.TabIndex = 5
        Me.RadLabel27.Text = "Document Amount"
        '
        'lblTotRAmt
        '
        Me.lblTotRAmt.AutoSize = False
        Me.lblTotRAmt.BorderVisible = True
        Me.lblTotRAmt.FieldName = Nothing
        Me.lblTotRAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotRAmt.Location = New System.Drawing.Point(143, 83)
        Me.lblTotRAmt.Name = "lblTotRAmt"
        Me.lblTotRAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblTotRAmt.TabIndex = 2
        Me.lblTotRAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadLabel25
        '
        Me.RadLabel25.FieldName = Nothing
        Me.RadLabel25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel25.Location = New System.Drawing.Point(58, 53)
        Me.RadLabel25.Name = "RadLabel25"
        Me.RadLabel25.Size = New System.Drawing.Size(77, 16)
        Me.RadLabel25.TabIndex = 4
        Me.RadLabel25.Text = "+ Tax Amount"
        '
        'lblTaxAmt
        '
        Me.lblTaxAmt.AutoSize = False
        Me.lblTaxAmt.BorderVisible = True
        Me.lblTaxAmt.FieldName = Nothing
        Me.lblTaxAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTaxAmt.Location = New System.Drawing.Point(143, 53)
        Me.lblTaxAmt.Name = "lblTaxAmt"
        Me.lblTaxAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblTaxAmt.TabIndex = 1
        Me.lblTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAmtAfterDiscount
        '
        Me.lblAmtAfterDiscount.AutoSize = False
        Me.lblAmtAfterDiscount.BorderVisible = True
        Me.lblAmtAfterDiscount.FieldName = Nothing
        Me.lblAmtAfterDiscount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmtAfterDiscount.Location = New System.Drawing.Point(143, 23)
        Me.lblAmtAfterDiscount.Name = "lblAmtAfterDiscount"
        Me.lblAmtAfterDiscount.Size = New System.Drawing.Size(110, 18)
        Me.lblAmtAfterDiscount.TabIndex = 0
        Me.lblAmtAfterDiscount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnReverse
        '
        Me.btnReverse.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReverse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverse.Location = New System.Drawing.Point(296, 5)
        Me.btnReverse.Name = "btnReverse"
        Me.btnReverse.Size = New System.Drawing.Size(133, 22)
        Me.btnReverse.TabIndex = 139
        Me.btnReverse.Text = "Revese and Unpost"
        Me.btnReverse.Visible = False
        '
        'btnprint
        '
        Me.btnprint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnprint.Location = New System.Drawing.Point(221, 4)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(69, 22)
        Me.btnprint.TabIndex = 3
        Me.btnprint.Text = "Print"
        '
        'btnDelete
        '
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(149, 4)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 22)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "Delete"
        '
        'btnPost
        '
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(77, 4)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(69, 22)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(766, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(5, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.MyLabel2)
        Me.GroupBox1.Controls.Add(Me.txtCostCenter)
        Me.GroupBox1.Controls.Add(Me.lblCostCenterDesc)
        Me.GroupBox1.Controls.Add(Me.MyLabel1)
        Me.GroupBox1.Controls.Add(Me.txtHCode)
        Me.GroupBox1.Controls.Add(Me.lblHDesc)
        Me.GroupBox1.Location = New System.Drawing.Point(202, 69)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(610, 74)
        Me.GroupBox1.TabIndex = 61
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Issue To"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(9, 20)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(85, 16)
        Me.MyLabel1.TabIndex = 61
        Me.MyLabel1.Text = "Hierarchy Level"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(9, 44)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(67, 16)
        Me.MyLabel2.TabIndex = 64
        Me.MyLabel2.Text = "Cost Center"
        '
        'txtCostCenter
        '
        Me.txtCostCenter.CalculationExpression = Nothing
        Me.txtCostCenter.FieldCode = Nothing
        Me.txtCostCenter.FieldDesc = Nothing
        Me.txtCostCenter.FieldMaxLength = 0
        Me.txtCostCenter.FieldName = Nothing
        Me.txtCostCenter.isCalculatedField = False
        Me.txtCostCenter.IsSourceFromTable = False
        Me.txtCostCenter.IsSourceFromValueList = False
        Me.txtCostCenter.IsUnique = False
        Me.txtCostCenter.Location = New System.Drawing.Point(102, 42)
        Me.txtCostCenter.MendatroryField = False
        Me.txtCostCenter.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCostCenter.MyLinkLable1 = Nothing
        Me.txtCostCenter.MyLinkLable2 = Me.lblCostCenterDesc
        Me.txtCostCenter.MyReadOnly = False
        Me.txtCostCenter.MyShowMasterFormButton = False
        Me.txtCostCenter.Name = "txtCostCenter"
        Me.txtCostCenter.ReferenceFieldDesc = Nothing
        Me.txtCostCenter.ReferenceFieldName = Nothing
        Me.txtCostCenter.ReferenceTableName = Nothing
        Me.txtCostCenter.Size = New System.Drawing.Size(144, 20)
        Me.txtCostCenter.TabIndex = 62
        Me.txtCostCenter.Value = ""
        '
        'lblCostCenterDesc
        '
        Me.lblCostCenterDesc.AutoSize = False
        Me.lblCostCenterDesc.BorderVisible = True
        Me.lblCostCenterDesc.FieldName = Nothing
        Me.lblCostCenterDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCostCenterDesc.Location = New System.Drawing.Point(252, 42)
        Me.lblCostCenterDesc.Name = "lblCostCenterDesc"
        Me.lblCostCenterDesc.Size = New System.Drawing.Size(353, 18)
        Me.lblCostCenterDesc.TabIndex = 63
        Me.lblCostCenterDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCostCenterDesc.TextWrap = False
        '
        'frmItemIssueToAssembledAsset
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(839, 567)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmItemIssueToAssembledAsset"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Issue Item To Assemble Asset"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.lblAssetType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboAssetType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblHDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkWithoutRefNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtComment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblReq, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblReq3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblReqDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblReq2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gv1.ResumeLayout(False)
        Me.gv1.PerformLayout()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAssetDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRequestBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkOnHold, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFromLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel29, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboDocType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.RadPageViewPage2.PerformLayout()
        CType(Me.lblTaxGrpName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pvpCustomFields.ResumeLayout(False)
        Me.Attachments.ResumeLayout(False)
        Me.RadPageViewPage3.ResumeLayout(False)
        Me.RadPageViewPage3.PerformLayout()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel27, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotRAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTaxAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAmtAfterDiscount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCostCenterDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents chkOnHold As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents cboDocType As common.Controls.MyComboBox
    Friend WithEvents txtRemarks As common.Controls.MyTextBox
    Friend WithEvents txtRequestBy As common.UserControls.txtFinder
    Friend WithEvents btnprint As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndAssetCode As common.UserControls.txtFinder
    Friend WithEvents txtFromLocation As common.UserControls.txtFinder
    Public WithEvents txtHCode As common.UserControls.txtFinder
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage3 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents txtTaxGroup As common.UserControls.txtFinder
    Friend WithEvents gv2 As common.UserControls.MyRadGridView
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents lblHDesc As common.Controls.MyLabel
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents RadLabel6 As common.Controls.MyLabel
    Friend WithEvents RadLabel29 As common.Controls.MyLabel
    Friend WithEvents lblRequestBy As common.Controls.MyLabel
    Friend WithEvents RadLabel15 As common.Controls.MyLabel
    Friend WithEvents lblAssetDesc As common.Controls.MyLabel
    Friend WithEvents RadLabel9 As common.Controls.MyLabel
    Friend WithEvents RadLabel8 As common.Controls.MyLabel
    Friend WithEvents lblFromLocation As common.Controls.MyLabel
    Friend WithEvents RadLabel10 As common.Controls.MyLabel
    Friend WithEvents lblTaxGrpName As common.Controls.MyLabel
    Friend WithEvents RadLabel11 As common.Controls.MyLabel
    Friend WithEvents RadLabel7 As common.Controls.MyLabel
    Friend WithEvents RadLabel27 As common.Controls.MyLabel
    Friend WithEvents lblTotRAmt As common.Controls.MyLabel
    Friend WithEvents RadLabel25 As common.Controls.MyLabel
    Friend WithEvents lblTaxAmt As common.Controls.MyLabel
    Friend WithEvents lblAmtAfterDiscount As common.Controls.MyLabel
    Friend WithEvents lblReqDate As common.Controls.MyLabel
    Friend WithEvents lblReq As common.Controls.MyLabel
    Public WithEvents fndReqNo As common.UserControls.txtFinder
    Friend WithEvents lblReq3 As common.Controls.MyLabel
    Friend WithEvents lblReq2 As common.Controls.MyLabel
    Friend WithEvents txtComment As common.Controls.MyTextBox
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents chkWithoutRefNo As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents pvpCustomFields As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcCustomFields1 As ucCustomFields
    Friend WithEvents btnReverse As Telerik.WinControls.UI.RadButton
    Friend WithEvents Attachments As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents UcItemBalance1 As XpertERPEngine.ucItemBalance
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents lblAssetType As common.Controls.MyLabel
    Friend WithEvents cboAssetType As common.Controls.MyComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Public WithEvents txtCostCenter As common.UserControls.txtFinder
    Friend WithEvents lblCostCenterDesc As common.Controls.MyLabel
End Class

