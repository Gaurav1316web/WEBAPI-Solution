<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmStoreIssue
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmStoreIssue))
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.chkTrading = New Telerik.WinControls.UI.RadCheckBox()
        Me.rdbAgainstBOMO = New System.Windows.Forms.RadioButton()
        Me.rdbAgainstReq = New System.Windows.Forms.RadioButton()
        Me.lblIssuedTo = New common.Controls.MyLabel()
        Me.txtIssuedTo = New common.UserControls.txtFinder()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.lblFromLocation = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtFromLocation = New common.UserControls.txtFinder()
        Me.btnShowItems = New Telerik.WinControls.UI.RadButton()
        Me.lblIssuedBy = New common.Controls.MyLabel()
        Me.txtIssuedBy = New common.UserControls.txtFinder()
        Me.RadLabel6 = New common.Controls.MyLabel()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.lblIssuedBy_T = New common.Controls.MyLabel()
        Me.lblExpiryDate = New common.Controls.MyLabel()
        Me.lblDescription = New common.Controls.MyLabel()
        Me.lblComment = New common.Controls.MyLabel()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.lblIssueNo = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtFinder()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.txtExpireDate = New common.Controls.MyDateTimePicker()
        Me.txtComment = New common.Controls.MyTextBox()
        Me.dtpDate = New common.Controls.MyDateTimePicker()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgReq = New common.MyCheckBoxGrid()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.gvIssue = New common.UserControls.MyRadGridView()
        Me.RadButton2 = New Telerik.WinControls.UI.RadButton()
        Me.UsLock1 = New common.usLock()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.btnShowInventory = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.chkTrading, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblIssuedTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFromLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnShowItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblIssuedBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblIssuedBy_T, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblExpiryDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblComment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblIssueNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtExpireDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtComment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvIssue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvIssue.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnShowInventory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.SplitContainer1)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(1017, 557)
        Me.RadGroupBox1.TabIndex = 0
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(10, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkTrading)
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdbAgainstBOMO)
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdbAgainstReq)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblIssuedTo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtIssuedTo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblFromLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtFromLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnShowItems)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblIssuedBy)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtIssuedBy)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblIssuedBy_T)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblExpiryDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblComment)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblIssueNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtExpireDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtComment)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnNew)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnShowInventory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(997, 527)
        Me.SplitContainer1.SplitterDistance = 489
        Me.SplitContainer1.TabIndex = 0
        '
        'chkTrading
        '
        Me.chkTrading.Location = New System.Drawing.Point(660, 3)
        Me.chkTrading.Name = "chkTrading"
        Me.chkTrading.Size = New System.Drawing.Size(58, 18)
        Me.chkTrading.TabIndex = 1
        Me.chkTrading.Text = "Trading"
        '
        'rdbAgainstBOMO
        '
        Me.rdbAgainstBOMO.AutoSize = True
        Me.rdbAgainstBOMO.Checked = True
        Me.rdbAgainstBOMO.Location = New System.Drawing.Point(226, 85)
        Me.rdbAgainstBOMO.Name = "rdbAgainstBOMO"
        Me.rdbAgainstBOMO.Size = New System.Drawing.Size(105, 17)
        Me.rdbAgainstBOMO.TabIndex = 11
        Me.rdbAgainstBOMO.TabStop = True
        Me.rdbAgainstBOMO.Text = "Against BO/MO"
        Me.rdbAgainstBOMO.UseVisualStyleBackColor = True
        Me.rdbAgainstBOMO.Visible = False
        '
        'rdbAgainstReq
        '
        Me.rdbAgainstReq.AutoSize = True
        Me.rdbAgainstReq.Location = New System.Drawing.Point(95, 86)
        Me.rdbAgainstReq.Name = "rdbAgainstReq"
        Me.rdbAgainstReq.Size = New System.Drawing.Size(126, 17)
        Me.rdbAgainstReq.TabIndex = 10
        Me.rdbAgainstReq.Text = "Against Requisition"
        Me.rdbAgainstReq.UseVisualStyleBackColor = True
        Me.rdbAgainstReq.Visible = False
        '
        'lblIssuedTo
        '
        Me.lblIssuedTo.AutoSize = False
        Me.lblIssuedTo.BorderVisible = True
        Me.lblIssuedTo.FieldName = Nothing
        Me.lblIssuedTo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIssuedTo.Location = New System.Drawing.Point(713, 22)
        Me.lblIssuedTo.Name = "lblIssuedTo"
        Me.lblIssuedTo.Size = New System.Drawing.Size(204, 20)
        Me.lblIssuedTo.TabIndex = 22
        Me.lblIssuedTo.TextWrap = False
        '
        'txtIssuedTo
        '
        Me.txtIssuedTo.CalculationExpression = Nothing
        Me.txtIssuedTo.FieldCode = Nothing
        Me.txtIssuedTo.FieldDesc = Nothing
        Me.txtIssuedTo.FieldMaxLength = 0
        Me.txtIssuedTo.FieldName = Nothing
        Me.txtIssuedTo.isCalculatedField = False
        Me.txtIssuedTo.IsSourceFromTable = False
        Me.txtIssuedTo.IsSourceFromValueList = False
        Me.txtIssuedTo.IsUnique = False
        Me.txtIssuedTo.Location = New System.Drawing.Point(562, 23)
        Me.txtIssuedTo.MendatroryField = True
        Me.txtIssuedTo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIssuedTo.MyLinkLable1 = Me.MyLabel3
        Me.txtIssuedTo.MyLinkLable2 = Me.lblFromLocation
        Me.txtIssuedTo.MyReadOnly = False
        Me.txtIssuedTo.MyShowMasterFormButton = False
        Me.txtIssuedTo.Name = "txtIssuedTo"
        Me.txtIssuedTo.ReferenceFieldDesc = Nothing
        Me.txtIssuedTo.ReferenceFieldName = Nothing
        Me.txtIssuedTo.ReferenceTableName = Nothing
        Me.txtIssuedTo.Size = New System.Drawing.Size(149, 18)
        Me.txtIssuedTo.TabIndex = 5
        Me.txtIssuedTo.Value = ""
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(13, 44)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(79, 16)
        Me.MyLabel3.TabIndex = 17
        Me.MyLabel3.Text = "From Location"
        '
        'lblFromLocation
        '
        Me.lblFromLocation.AutoSize = False
        Me.lblFromLocation.BorderVisible = True
        Me.lblFromLocation.FieldName = Nothing
        Me.lblFromLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFromLocation.Location = New System.Drawing.Point(249, 42)
        Me.lblFromLocation.Name = "lblFromLocation"
        Me.lblFromLocation.Size = New System.Drawing.Size(218, 20)
        Me.lblFromLocation.TabIndex = 19
        Me.lblFromLocation.TextWrap = False
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(494, 24)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(56, 16)
        Me.MyLabel5.TabIndex = 21
        Me.MyLabel5.Text = "Issued To"
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
        Me.txtFromLocation.Location = New System.Drawing.Point(95, 43)
        Me.txtFromLocation.MendatroryField = True
        Me.txtFromLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFromLocation.MyLinkLable1 = Me.MyLabel3
        Me.txtFromLocation.MyLinkLable2 = Me.lblFromLocation
        Me.txtFromLocation.MyReadOnly = False
        Me.txtFromLocation.MyShowMasterFormButton = False
        Me.txtFromLocation.Name = "txtFromLocation"
        Me.txtFromLocation.ReferenceFieldDesc = Nothing
        Me.txtFromLocation.ReferenceFieldName = Nothing
        Me.txtFromLocation.ReferenceTableName = Nothing
        Me.txtFromLocation.Size = New System.Drawing.Size(154, 18)
        Me.txtFromLocation.TabIndex = 6
        Me.txtFromLocation.Value = ""
        '
        'btnShowItems
        '
        Me.btnShowItems.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShowItems.Location = New System.Drawing.Point(922, 257)
        Me.btnShowItems.Name = "btnShowItems"
        Me.btnShowItems.Size = New System.Drawing.Size(68, 18)
        Me.btnShowItems.TabIndex = 13
        Me.btnShowItems.Text = ">>"
        '
        'lblIssuedBy
        '
        Me.lblIssuedBy.AutoSize = False
        Me.lblIssuedBy.BorderVisible = True
        Me.lblIssuedBy.FieldName = Nothing
        Me.lblIssuedBy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIssuedBy.Location = New System.Drawing.Point(249, 22)
        Me.lblIssuedBy.Name = "lblIssuedBy"
        Me.lblIssuedBy.Size = New System.Drawing.Size(218, 20)
        Me.lblIssuedBy.TabIndex = 22
        Me.lblIssuedBy.TextWrap = False
        '
        'txtIssuedBy
        '
        Me.txtIssuedBy.CalculationExpression = Nothing
        Me.txtIssuedBy.FieldCode = Nothing
        Me.txtIssuedBy.FieldDesc = Nothing
        Me.txtIssuedBy.FieldMaxLength = 0
        Me.txtIssuedBy.FieldName = Nothing
        Me.txtIssuedBy.isCalculatedField = False
        Me.txtIssuedBy.IsSourceFromTable = False
        Me.txtIssuedBy.IsSourceFromValueList = False
        Me.txtIssuedBy.IsUnique = False
        Me.txtIssuedBy.Location = New System.Drawing.Point(95, 23)
        Me.txtIssuedBy.MendatroryField = True
        Me.txtIssuedBy.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIssuedBy.MyLinkLable1 = Me.RadLabel6
        Me.txtIssuedBy.MyLinkLable2 = Me.lblLocation
        Me.txtIssuedBy.MyReadOnly = False
        Me.txtIssuedBy.MyShowMasterFormButton = False
        Me.txtIssuedBy.Name = "txtIssuedBy"
        Me.txtIssuedBy.ReferenceFieldDesc = Nothing
        Me.txtIssuedBy.ReferenceFieldName = Nothing
        Me.txtIssuedBy.ReferenceTableName = Nothing
        Me.txtIssuedBy.Size = New System.Drawing.Size(154, 18)
        Me.txtIssuedBy.TabIndex = 4
        Me.txtIssuedBy.Value = ""
        '
        'RadLabel6
        '
        Me.RadLabel6.FieldName = Nothing
        Me.RadLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel6.Location = New System.Drawing.Point(494, 44)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(65, 16)
        Me.RadLabel6.TabIndex = 18
        Me.RadLabel6.Text = "To Location"
        '
        'lblLocation
        '
        Me.lblLocation.AutoSize = False
        Me.lblLocation.BorderVisible = True
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(713, 42)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(204, 20)
        Me.lblLocation.TabIndex = 19
        Me.lblLocation.TextWrap = False
        '
        'lblIssuedBy_T
        '
        Me.lblIssuedBy_T.FieldName = Nothing
        Me.lblIssuedBy_T.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIssuedBy_T.Location = New System.Drawing.Point(13, 24)
        Me.lblIssuedBy_T.Name = "lblIssuedBy_T"
        Me.lblIssuedBy_T.Size = New System.Drawing.Size(56, 16)
        Me.lblIssuedBy_T.TabIndex = 20
        Me.lblIssuedBy_T.Text = "Issued By"
        '
        'lblExpiryDate
        '
        Me.lblExpiryDate.FieldName = Nothing
        Me.lblExpiryDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExpiryDate.Location = New System.Drawing.Point(494, 4)
        Me.lblExpiryDate.Name = "lblExpiryDate"
        Me.lblExpiryDate.Size = New System.Drawing.Size(65, 16)
        Me.lblExpiryDate.TabIndex = 25
        Me.lblExpiryDate.Text = "Expiry Date"
        '
        'lblDescription
        '
        Me.lblDescription.FieldName = Nothing
        Me.lblDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescription.Location = New System.Drawing.Point(13, 64)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(63, 16)
        Me.lblDescription.TabIndex = 15
        Me.lblDescription.Text = "Description"
        '
        'lblComment
        '
        Me.lblComment.FieldName = Nothing
        Me.lblComment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblComment.Location = New System.Drawing.Point(494, 65)
        Me.lblComment.Name = "lblComment"
        Me.lblComment.Size = New System.Drawing.Size(55, 16)
        Me.lblComment.TabIndex = 16
        Me.lblComment.Text = "Comment"
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(357, 4)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 24
        Me.RadLabel4.Text = "Date"
        '
        'lblIssueNo
        '
        Me.lblIssueNo.FieldName = Nothing
        Me.lblIssueNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIssueNo.Location = New System.Drawing.Point(13, 4)
        Me.lblIssueNo.Name = "lblIssueNo"
        Me.lblIssueNo.Size = New System.Drawing.Size(51, 16)
        Me.lblIssueNo.TabIndex = 23
        Me.lblIssueNo.Text = "Issue No"
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
        Me.txtLocation.Location = New System.Drawing.Point(562, 43)
        Me.txtLocation.MendatroryField = True
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.RadLabel6
        Me.txtLocation.MyLinkLable2 = Me.lblLocation
        Me.txtLocation.MyReadOnly = False
        Me.txtLocation.MyShowMasterFormButton = False
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.ReferenceFieldDesc = Nothing
        Me.txtLocation.ReferenceFieldName = Nothing
        Me.txtLocation.ReferenceTableName = Nothing
        Me.txtLocation.Size = New System.Drawing.Size(149, 18)
        Me.txtLocation.TabIndex = 7
        Me.txtLocation.Value = ""
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(95, 2)
        Me.txtCode.MendatroryField = False
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblIssueNo
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 30
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(230, 20)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'txtExpireDate
        '
        Me.txtExpireDate.CalculationExpression = Nothing
        Me.txtExpireDate.CustomFormat = "dd/MM/yyyy"
        Me.txtExpireDate.FieldCode = Nothing
        Me.txtExpireDate.FieldDesc = Nothing
        Me.txtExpireDate.FieldMaxLength = 0
        Me.txtExpireDate.FieldName = Nothing
        Me.txtExpireDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExpireDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtExpireDate.isCalculatedField = False
        Me.txtExpireDate.IsSourceFromTable = False
        Me.txtExpireDate.IsSourceFromValueList = False
        Me.txtExpireDate.IsUnique = False
        Me.txtExpireDate.Location = New System.Drawing.Point(562, 3)
        Me.txtExpireDate.MendatroryField = False
        Me.txtExpireDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtExpireDate.MyLinkLable1 = Me.lblExpiryDate
        Me.txtExpireDate.MyLinkLable2 = Nothing
        Me.txtExpireDate.Name = "txtExpireDate"
        Me.txtExpireDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtExpireDate.ReferenceFieldDesc = Nothing
        Me.txtExpireDate.ReferenceFieldName = Nothing
        Me.txtExpireDate.ReferenceTableName = Nothing
        Me.txtExpireDate.ShowCheckBox = True
        Me.txtExpireDate.Size = New System.Drawing.Size(92, 18)
        Me.txtExpireDate.TabIndex = 3
        Me.txtExpireDate.TabStop = False
        Me.txtExpireDate.Text = "13/06/2011"
        Me.txtExpireDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'txtComment
        '
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
        Me.txtComment.Location = New System.Drawing.Point(562, 64)
        Me.txtComment.MaxLength = 200
        Me.txtComment.MendatroryField = False
        Me.txtComment.MyLinkLable1 = Me.lblComment
        Me.txtComment.MyLinkLable2 = Nothing
        Me.txtComment.Name = "txtComment"
        Me.txtComment.ReferenceFieldDesc = Nothing
        Me.txtComment.ReferenceFieldName = Nothing
        Me.txtComment.ReferenceTableName = Nothing
        Me.txtComment.Size = New System.Drawing.Size(355, 18)
        Me.txtComment.TabIndex = 9
        '
        'dtpDate
        '
        Me.dtpDate.CalculationExpression = Nothing
        Me.dtpDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpDate.FieldCode = Nothing
        Me.dtpDate.FieldDesc = Nothing
        Me.dtpDate.FieldMaxLength = 0
        Me.dtpDate.FieldName = Nothing
        Me.dtpDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDate.isCalculatedField = False
        Me.dtpDate.IsSourceFromTable = False
        Me.dtpDate.IsSourceFromValueList = False
        Me.dtpDate.IsUnique = False
        Me.dtpDate.Location = New System.Drawing.Point(388, 3)
        Me.dtpDate.MendatroryField = False
        Me.dtpDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDate.MyLinkLable1 = Me.RadLabel4
        Me.dtpDate.MyLinkLable2 = Nothing
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDate.ReferenceFieldDesc = Nothing
        Me.dtpDate.ReferenceFieldName = Nothing
        Me.dtpDate.ReferenceTableName = Nothing
        Me.dtpDate.Size = New System.Drawing.Size(79, 18)
        Me.dtpDate.TabIndex = 2
        Me.dtpDate.TabStop = False
        Me.dtpDate.Text = "13/06/2011"
        Me.dtpDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'txtDescription
        '
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
        Me.txtDescription.Location = New System.Drawing.Point(95, 63)
        Me.txtDescription.MaxLength = 200
        Me.txtDescription.MendatroryField = False
        Me.txtDescription.MyLinkLable1 = Me.lblDescription
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReferenceFieldDesc = Nothing
        Me.txtDescription.ReferenceFieldName = Nothing
        Me.txtDescription.ReferenceTableName = Nothing
        Me.txtDescription.Size = New System.Drawing.Size(372, 18)
        Me.txtDescription.TabIndex = 8
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.cbgReq)
        Me.RadGroupBox4.HeaderText = "Select  Requisition"
        Me.RadGroupBox4.Location = New System.Drawing.Point(12, 108)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(905, 167)
        Me.RadGroupBox4.TabIndex = 12
        Me.RadGroupBox4.Text = "Select  Requisition"
        '
        'cbgReq
        '
        Me.cbgReq.CheckedValue = Nothing
        Me.cbgReq.DataSource = Nothing
        Me.cbgReq.DisplayMember = "Name"
        Me.cbgReq.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgReq.Location = New System.Drawing.Point(10, 20)
        Me.cbgReq.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgReq.MyShowHeadrText = False
        Me.cbgReq.Name = "cbgReq"
        Me.cbgReq.Size = New System.Drawing.Size(885, 137)
        Me.cbgReq.TabIndex = 0
        Me.cbgReq.ValueMember = "Code"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox3.Controls.Add(Me.gvIssue)
        Me.RadGroupBox3.Controls.Add(Me.RadButton2)
        Me.RadGroupBox3.HeaderText = "Issue Details"
        Me.RadGroupBox3.Location = New System.Drawing.Point(10, 275)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(981, 211)
        Me.RadGroupBox3.TabIndex = 14
        Me.RadGroupBox3.Text = "Issue Details"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(12, 89)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(2, 2)
        Me.MyLabel1.TabIndex = 19
        '
        'gvIssue
        '
        Me.gvIssue.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvIssue.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvIssue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvIssue.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvIssue.ForeColor = System.Drawing.Color.Black
        Me.gvIssue.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvIssue.Location = New System.Drawing.Point(10, 20)
        '
        '
        '
        Me.gvIssue.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvIssue.MasterTemplate.AllowAddNewRow = False
        Me.gvIssue.MasterTemplate.AutoGenerateColumns = False
        Me.gvIssue.MasterTemplate.EnableGrouping = False
        Me.gvIssue.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvIssue.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvIssue.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gvIssue.MyStopExport = False
        Me.gvIssue.Name = "gvIssue"
        Me.gvIssue.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvIssue.ShowHeaderCellButtons = True
        Me.gvIssue.Size = New System.Drawing.Size(961, 181)
        Me.gvIssue.TabIndex = 0
        '
        'RadButton2
        '
        Me.RadButton2.Image = CType(resources.GetObject("RadButton2.Image"), System.Drawing.Image)
        Me.RadButton2.Location = New System.Drawing.Point(352, -422)
        Me.RadButton2.Name = "RadButton2"
        Me.RadButton2.Size = New System.Drawing.Size(14, 20)
        Me.RadButton2.TabIndex = 17
        Me.RadButton2.Text = " "
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(819, 0)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 26
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(326, 2)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 20)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Text = " "
        '
        'btnShowInventory
        '
        Me.btnShowInventory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnShowInventory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShowInventory.Location = New System.Drawing.Point(303, 7)
        Me.btnShowInventory.Name = "btnShowInventory"
        Me.btnShowInventory.Size = New System.Drawing.Size(102, 20)
        Me.btnShowInventory.TabIndex = 6
        Me.btnShowInventory.Text = "Show Inventory"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(228, 8)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(69, 18)
        Me.btnPrint.TabIndex = 3
        Me.btnPrint.Text = "Print"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(80, 8)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(68, 18)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(8, 8)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(68, 18)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(925, 8)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(68, 18)
        Me.btnclose.TabIndex = 4
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(154, 8)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(68, 18)
        Me.btndelete.TabIndex = 2
        Me.btndelete.Text = "Delete"
        '
        'frmStoreIssue
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1017, 557)
        Me.Controls.Add(Me.RadGroupBox1)
        Me.Name = "frmStoreIssue"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Store Issue"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.chkTrading, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblIssuedTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFromLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnShowItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblIssuedBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblIssuedBy_T, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblExpiryDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblComment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblIssueNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtExpireDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtComment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvIssue.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvIssue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnShowInventory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents gvIssue As common.UserControls.MyRadGridView
    Friend WithEvents RadButton2 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgReq As common.MyCheckBoxGrid
    Friend WithEvents lblIssuedBy_T As common.Controls.MyLabel
    Friend WithEvents lblExpiryDate As common.Controls.MyLabel
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents lblDescription As common.Controls.MyLabel
    Friend WithEvents lblComment As common.Controls.MyLabel
    Friend WithEvents RadLabel6 As common.Controls.MyLabel
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents lblIssueNo As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtFinder
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents txtExpireDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtComment As common.Controls.MyTextBox
    Friend WithEvents dtpDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents lblIssuedBy As common.Controls.MyLabel
    Friend WithEvents txtIssuedBy As common.UserControls.txtFinder
    Friend WithEvents btnShowItems As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblIssuedTo As common.Controls.MyLabel
    Friend WithEvents txtIssuedTo As common.UserControls.txtFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents lblFromLocation As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtFromLocation As common.UserControls.txtFinder
    Friend WithEvents rdbAgainstBOMO As System.Windows.Forms.RadioButton
    Friend WithEvents rdbAgainstReq As System.Windows.Forms.RadioButton
    Friend WithEvents chkTrading As RadCheckBox
    Friend WithEvents btnShowInventory As RadButton
End Class
