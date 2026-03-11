<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmBoothCommission
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
        Dim TableViewDefinition3 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition4 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem()
        Me.EmailSmsSetting = New Telerik.WinControls.UI.RadMenuItem()
        Me.EmailSettingCreditApproval = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.lblShiftType = New Telerik.WinControls.UI.RadLabel()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.lblAbandonmentNo = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.RadPageViewPage7 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.txtComment = New common.Controls.MyTextBox()
        Me.lblComment = New System.Windows.Forms.Label()
        Me.txtRemark = New common.Controls.MyTextBox()
        Me.lblRemark = New System.Windows.Forms.Label()
        Me.txtMonthYear = New common.Controls.MyDateTimePicker()
        Me.lblFromDate = New System.Windows.Forms.Label()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnreverse = New Telerik.WinControls.UI.RadButton()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.gv2 = New common.UserControls.MyRadGridView()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblShiftType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAbandonmentNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage7.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.txtComment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemark, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMonthYear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(931, 20)
        Me.RadMenu1.TabIndex = 7
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItem4, Me.EmailSmsSetting, Me.EmailSettingCreditApproval})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        Me.RadMenuItem3.UseCompatibleTextRendering = False
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Save Layout"
        Me.RadMenuItem1.UseCompatibleTextRendering = False
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Delete Layout"
        Me.RadMenuItem4.UseCompatibleTextRendering = False
        '
        'EmailSmsSetting
        '
        Me.EmailSmsSetting.Name = "EmailSmsSetting"
        Me.EmailSmsSetting.Text = "Email/SMS Setting"
        Me.EmailSmsSetting.UseCompatibleTextRendering = False
        Me.EmailSmsSetting.Visibility = Telerik.WinControls.ElementVisibility.Hidden
        '
        'EmailSettingCreditApproval
        '
        Me.EmailSettingCreditApproval.Name = "EmailSettingCreditApproval"
        Me.EmailSettingCreditApproval.Text = "Email setting for Credit Approval"
        Me.EmailSettingCreditApproval.UseCompatibleTextRendering = False
        Me.EmailSettingCreditApproval.Visibility = Telerik.WinControls.ElementVisibility.Hidden
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnreverse)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(931, 430)
        Me.SplitContainer1.SplitterDistance = 389
        Me.SplitContainer1.TabIndex = 8
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage7)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(931, 389)
        Me.RadPageView1.TabIndex = 1
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer2)
        Me.RadPageViewPage1.Controls.Add(Me.lblShiftType)
        Me.RadPageViewPage1.Controls.Add(Me.lblAbandonmentNo)
        Me.RadPageViewPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(111.0!, 26.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(910, 343)
        Me.RadPageViewPage1.Text = "Booth Commission"
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(372, 6)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 1
        Me.RadLabel4.Text = "Date"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(538, 7)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(97, 16)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 1587
        '
        'lblShiftType
        '
        Me.lblShiftType.Location = New System.Drawing.Point(686, -3)
        Me.lblShiftType.Name = "lblShiftType"
        Me.lblShiftType.Size = New System.Drawing.Size(2, 2)
        Me.lblShiftType.TabIndex = 1536
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
        Me.txtDate.Location = New System.Drawing.Point(405, 5)
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
        Me.txtDate.TabIndex = 1524
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011 11:29 AM"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'lblAbandonmentNo
        '
        Me.lblAbandonmentNo.FieldName = Nothing
        Me.lblAbandonmentNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAbandonmentNo.Location = New System.Drawing.Point(764, 11)
        Me.lblAbandonmentNo.Name = "lblAbandonmentNo"
        Me.lblAbandonmentNo.Size = New System.Drawing.Size(2, 2)
        Me.lblAbandonmentNo.TabIndex = 27
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(11, 7)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(33, 16)
        Me.RadLabel1.TabIndex = 22
        Me.RadLabel1.Text = "Code"
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(61, 4)
        Me.txtDocNo.MendatroryField = False
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Me.RadLabel1
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 32767
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(269, 22)
        Me.txtDocNo.TabIndex = 0
        Me.txtDocNo.TabStop = False
        Me.txtDocNo.Value = ""
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.XpertERPDairySale.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(329, 4)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 21)
        Me.btnAddNew.TabIndex = 1
        '
        'RadPageViewPage7
        '
        Me.RadPageViewPage7.Controls.Add(Me.gv2)
        Me.RadPageViewPage7.ItemSize = New System.Drawing.SizeF(85.0!, 26.0!)
        Me.RadPageViewPage7.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage7.Name = "RadPageViewPage7"
        Me.RadPageViewPage7.Size = New System.Drawing.Size(910, 343)
        Me.RadPageViewPage7.Text = "Day Wise Qty"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnGo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtMonthYear)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblFromDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtComment)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblComment)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtRemark)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblRemark)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnAddNew)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDocNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel4)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDate)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gv1)
        Me.SplitContainer2.Size = New System.Drawing.Size(910, 343)
        Me.SplitContainer2.SplitterDistance = 112
        Me.SplitContainer2.TabIndex = 1588
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
        Me.txtComment.Location = New System.Drawing.Point(61, 84)
        Me.txtComment.MaxLength = 200
        Me.txtComment.MendatroryField = False
        Me.txtComment.Multiline = True
        Me.txtComment.MyLinkLable1 = Nothing
        Me.txtComment.MyLinkLable2 = Nothing
        Me.txtComment.Name = "txtComment"
        Me.txtComment.ReferenceFieldDesc = Nothing
        Me.txtComment.ReferenceFieldName = Nothing
        Me.txtComment.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtComment.RootElement.StretchVertically = True
        Me.txtComment.Size = New System.Drawing.Size(269, 22)
        Me.txtComment.TabIndex = 1591
        '
        'lblComment
        '
        Me.lblComment.AutoSize = True
        Me.lblComment.Location = New System.Drawing.Point(11, 87)
        Me.lblComment.Name = "lblComment"
        Me.lblComment.Size = New System.Drawing.Size(51, 14)
        Me.lblComment.TabIndex = 1590
        Me.lblComment.Text = "Comment"
        '
        'txtRemark
        '
        Me.txtRemark.CalculationExpression = Nothing
        Me.txtRemark.FieldCode = Nothing
        Me.txtRemark.FieldDesc = Nothing
        Me.txtRemark.FieldMaxLength = 0
        Me.txtRemark.FieldName = Nothing
        Me.txtRemark.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemark.isCalculatedField = False
        Me.txtRemark.IsSourceFromTable = False
        Me.txtRemark.IsSourceFromValueList = False
        Me.txtRemark.IsUnique = False
        Me.txtRemark.Location = New System.Drawing.Point(61, 56)
        Me.txtRemark.MaxLength = 200
        Me.txtRemark.MendatroryField = False
        Me.txtRemark.Multiline = True
        Me.txtRemark.MyLinkLable1 = Nothing
        Me.txtRemark.MyLinkLable2 = Nothing
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.ReferenceFieldDesc = Nothing
        Me.txtRemark.ReferenceFieldName = Nothing
        Me.txtRemark.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtRemark.RootElement.StretchVertically = True
        Me.txtRemark.Size = New System.Drawing.Size(269, 22)
        Me.txtRemark.TabIndex = 1589
        '
        'lblRemark
        '
        Me.lblRemark.AutoSize = True
        Me.lblRemark.Location = New System.Drawing.Point(11, 61)
        Me.lblRemark.Name = "lblRemark"
        Me.lblRemark.Size = New System.Drawing.Size(43, 14)
        Me.lblRemark.TabIndex = 1588
        Me.lblRemark.Text = "Remark"
        '
        'txtMonthYear
        '
        Me.txtMonthYear.CalculationExpression = Nothing
        Me.txtMonthYear.CustomFormat = "MMM/yyyy"
        Me.txtMonthYear.FieldCode = Nothing
        Me.txtMonthYear.FieldDesc = Nothing
        Me.txtMonthYear.FieldMaxLength = 0
        Me.txtMonthYear.FieldName = Nothing
        Me.txtMonthYear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMonthYear.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtMonthYear.isCalculatedField = False
        Me.txtMonthYear.IsSourceFromTable = False
        Me.txtMonthYear.IsSourceFromValueList = False
        Me.txtMonthYear.IsUnique = False
        Me.txtMonthYear.Location = New System.Drawing.Point(61, 32)
        Me.txtMonthYear.MendatroryField = False
        Me.txtMonthYear.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtMonthYear.MyLinkLable1 = Nothing
        Me.txtMonthYear.MyLinkLable2 = Nothing
        Me.txtMonthYear.Name = "txtMonthYear"
        Me.txtMonthYear.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtMonthYear.ReferenceFieldDesc = Nothing
        Me.txtMonthYear.ReferenceFieldName = Nothing
        Me.txtMonthYear.ReferenceTableName = Nothing
        Me.txtMonthYear.Size = New System.Drawing.Size(269, 18)
        Me.txtMonthYear.TabIndex = 1592
        Me.txtMonthYear.TabStop = False
        Me.txtMonthYear.Text = "Jun/2011"
        Me.txtMonthYear.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'lblFromDate
        '
        Me.lblFromDate.AutoSize = True
        Me.lblFromDate.Location = New System.Drawing.Point(11, 35)
        Me.lblFromDate.Name = "lblFromDate"
        Me.lblFromDate.Size = New System.Drawing.Size(36, 14)
        Me.lblFromDate.TabIndex = 1593
        Me.lblFromDate.Text = "Month"
        '
        'btnGo
        '
        Me.btnGo.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(345, 85)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(57, 22)
        Me.btnGo.TabIndex = 1594
        Me.btnGo.Text = ">>"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(133, 8)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(61, 22)
        Me.btnDelete.TabIndex = 5
        Me.btnDelete.Text = "Delete"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(69, 8)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(60, 22)
        Me.btnPost.TabIndex = 4
        Me.btnPost.Text = "Post"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(10, 8)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(57, 22)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "Save"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(849, 8)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(71, 22)
        Me.btnClose.TabIndex = 11
        Me.btnClose.Text = "Close"
        '
        'btnreverse
        '
        Me.btnreverse.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnreverse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnreverse.Location = New System.Drawing.Point(697, 8)
        Me.btnreverse.Name = "btnreverse"
        Me.btnreverse.Size = New System.Drawing.Size(136, 22)
        Me.btnreverse.TabIndex = 22
        Me.btnreverse.Text = "Reverse/Unpost"
        Me.btnreverse.Visible = False
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
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition3
        Me.gv1.MyExportAPI = False
        Me.gv1.MyExportFilePath = ""
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(910, 227)
        Me.gv1.TabIndex = 18
        Me.gv1.TabStop = False
        Me.gv1.VarID = ""
        '
        'gv2
        '
        Me.gv2.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv2.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv2.ForeColor = System.Drawing.Color.Black
        Me.gv2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv2.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv2.MasterTemplate.AllowDeleteRow = False
        Me.gv2.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv2.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv2.MasterTemplate.ViewDefinition = TableViewDefinition4
        Me.gv2.MyExportAPI = False
        Me.gv2.MyExportFilePath = ""
        Me.gv2.MyStopExport = False
        Me.gv2.Name = "gv2"
        Me.gv2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv2.ShowGroupPanel = False
        Me.gv2.ShowHeaderCellButtons = True
        Me.gv2.Size = New System.Drawing.Size(910, 343)
        Me.gv2.TabIndex = 19
        Me.gv2.TabStop = False
        Me.gv2.VarID = ""
        '
        'FrmBoothCommission
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(931, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmBoothCommission"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmBoothCommission"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblShiftType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAbandonmentNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage7.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.txtComment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemark, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMonthYear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RadMenu1 As RadMenu
    Friend WithEvents RadMenuItem3 As RadMenuItem
    Friend WithEvents RadMenuItem1 As RadMenuItem
    Friend WithEvents RadMenuItem4 As RadMenuItem
    Friend WithEvents EmailSmsSetting As RadMenuItem
    Friend WithEvents EmailSettingCreditApproval As RadMenuItem
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents btnAddNew As RadButton
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblShiftType As RadLabel
    Friend WithEvents lblAbandonmentNo As common.Controls.MyLabel
    Friend WithEvents RadPageViewPage7 As RadPageViewPage
    Friend WithEvents txtComment As common.Controls.MyTextBox
    Friend WithEvents lblComment As Label
    Friend WithEvents txtRemark As common.Controls.MyTextBox
    Friend WithEvents lblRemark As Label
    Friend WithEvents txtMonthYear As common.Controls.MyDateTimePicker
    Friend WithEvents lblFromDate As Label
    Friend WithEvents btnGo As RadButton
    Friend WithEvents btnSave As RadButton
    Friend WithEvents btnDelete As RadButton
    Friend WithEvents btnPost As RadButton
    Friend WithEvents btnClose As RadButton
    Friend WithEvents btnreverse As RadButton
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents gv2 As common.UserControls.MyRadGridView
End Class
