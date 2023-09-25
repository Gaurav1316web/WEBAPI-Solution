<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmTender
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
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition3 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.cboTenderType = New common.Controls.MyComboBox()
        Me.RadLabel29 = New common.Controls.MyLabel()
        Me.cboItemType = New common.Controls.MyComboBox()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.btnCopy = New Telerik.WinControls.UI.RadButton()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.txtItem = New common.UserControls.txtFinder()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.btnSubmit = New Telerik.WinControls.UI.RadButton()
        Me.UsLock1 = New common.usLock()
        Me.lblAbandonmentNo = New common.Controls.MyLabel()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.lblTenderSqNo = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtFieldValue1 = New common.Controls.MyTextBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.lblTenderSeqNo = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtFieldValue3 = New common.Controls.MyTextBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtFieldValue6 = New common.Controls.MyTextBox()
        Me.txtFieldValue4 = New common.Controls.MyTextBox()
        Me.txtFieldValue2 = New common.Controls.MyTextBox()
        Me.txtFieldValue5 = New common.Controls.MyTextBox()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv2 = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.MyLabel22 = New common.Controls.MyLabel()
        Me.MyLabel20 = New common.Controls.MyLabel()
        Me.MyLabel19 = New common.Controls.MyLabel()
        Me.MyLabel18 = New common.Controls.MyLabel()
        Me.MyLabel15 = New common.Controls.MyLabel()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.txtOtherInfo5 = New System.Windows.Forms.TextBox()
        Me.txtOtherInfo4 = New System.Windows.Forms.TextBox()
        Me.txtOtherInfo2 = New System.Windows.Forms.TextBox()
        Me.txtOtherInfo7 = New System.Windows.Forms.TextBox()
        Me.txtOtherInfo9 = New System.Windows.Forms.TextBox()
        Me.txtOtherInfo10 = New System.Windows.Forms.TextBox()
        Me.txtOtherInfo8 = New System.Windows.Forms.TextBox()
        Me.txtOtherInfo6 = New System.Windows.Forms.TextBox()
        Me.txtOtherInfo3 = New System.Windows.Forms.TextBox()
        Me.txtOtherInfo1 = New System.Windows.Forms.TextBox()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvSchedule = New common.UserControls.MyRadGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblExtensionDays = New common.Controls.MyLabel()
        Me.txtExtensionDays = New common.MyNumBox()
        Me.btnExtensionUpdate = New Telerik.WinControls.UI.RadButton()
        Me.btnApplyAll = New Telerik.WinControls.UI.RadButton()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.txtScheduleStartDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.MyLabel38 = New common.Controls.MyLabel()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.lblCreatedByValue = New common.Controls.MyLabel()
        Me.lblTotalDocAmt = New common.Controls.MyLabel()
        Me.btnreverse = New Telerik.WinControls.UI.RadButton()
        Me.RadLabel10 = New common.Controls.MyLabel()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem5 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem7 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem8 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem9 = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuClose = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.ExportData = New Telerik.WinControls.UI.RadMenuItem()
        Me.ExportBlankSheet = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem6 = New Telerik.WinControls.UI.RadMenuItem()
        Me.MyLabel17 = New common.Controls.MyLabel()
        Me.cboMode = New common.Controls.MyComboBox()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboTenderType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel29, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboItemType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCopy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSubmit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAbandonmentNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTenderSqNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFieldValue1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTenderSeqNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFieldValue3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFieldValue6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFieldValue4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFieldValue2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFieldValue5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage3.SuspendLayout()
        CType(Me.gvSchedule, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvSchedule.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.lblExtensionDays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtExtensionDays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExtensionUpdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnApplyAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtScheduleStartDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel38, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCreatedByValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotalDocAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboMode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 25)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.MyLabel11)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lblCreatedByValue)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lblTotalDocAmt)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnreverse)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadLabel10)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(1098, 523)
        Me.SplitContainer1.SplitterDistance = 492
        Me.SplitContainer1.TabIndex = 1
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1098, 492)
        Me.RadPageView1.TabIndex = 0
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer2)
        Me.RadPageViewPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(52.0!, 26.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1077, 446)
        Me.RadPageViewPage1.Text = "Tender"
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel17)
        Me.SplitContainer2.Panel1.Controls.Add(Me.cboMode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel16)
        Me.SplitContainer2.Panel1.Controls.Add(Me.cboTenderType)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel29)
        Me.SplitContainer2.Panel1.Controls.Add(Me.cboItemType)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnCopy)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtItem)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDocNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnAddNew)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnSubmit)
        Me.SplitContainer2.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblAbandonmentNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel14)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel6)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblTenderSqNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtFieldValue1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblTenderSeqNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtFieldValue3)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtFieldValue6)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtFieldValue4)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtFieldValue2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtFieldValue5)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.SplitContainer3)
        Me.SplitContainer2.Size = New System.Drawing.Size(1077, 446)
        Me.SplitContainer2.SplitterDistance = 118
        Me.SplitContainer2.TabIndex = 1515
        '
        'MyLabel16
        '
        Me.MyLabel16.FieldName = Nothing
        Me.MyLabel16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel16.Location = New System.Drawing.Point(6, 32)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(31, 16)
        Me.MyLabel16.TabIndex = 1521
        Me.MyLabel16.Text = "Type"
        '
        'cboTenderType
        '
        Me.cboTenderType.AutoCompleteDisplayMember = Nothing
        Me.cboTenderType.AutoCompleteValueMember = Nothing
        Me.cboTenderType.CalculationExpression = Nothing
        Me.cboTenderType.DropDownAnimationEnabled = True
        Me.cboTenderType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboTenderType.FieldCode = Nothing
        Me.cboTenderType.FieldDesc = Nothing
        Me.cboTenderType.FieldMaxLength = 0
        Me.cboTenderType.FieldName = Nothing
        Me.cboTenderType.isCalculatedField = False
        Me.cboTenderType.IsSourceFromTable = False
        Me.cboTenderType.IsSourceFromValueList = False
        Me.cboTenderType.IsUnique = False
        Me.cboTenderType.Location = New System.Drawing.Point(67, 30)
        Me.cboTenderType.MendatroryField = True
        Me.cboTenderType.MyLinkLable1 = Me.MyLabel16
        Me.cboTenderType.MyLinkLable2 = Nothing
        Me.cboTenderType.Name = "cboTenderType"
        Me.cboTenderType.ReferenceFieldDesc = Nothing
        Me.cboTenderType.ReferenceFieldName = Nothing
        Me.cboTenderType.ReferenceTableName = Nothing
        Me.cboTenderType.Size = New System.Drawing.Size(245, 20)
        Me.cboTenderType.TabIndex = 1520
        '
        'RadLabel29
        '
        Me.RadLabel29.FieldName = Nothing
        Me.RadLabel29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel29.Location = New System.Drawing.Point(6, 53)
        Me.RadLabel29.Name = "RadLabel29"
        Me.RadLabel29.Size = New System.Drawing.Size(57, 16)
        Me.RadLabel29.TabIndex = 1519
        Me.RadLabel29.Text = "Item Type"
        '
        'cboItemType
        '
        Me.cboItemType.AutoCompleteDisplayMember = Nothing
        Me.cboItemType.AutoCompleteValueMember = Nothing
        Me.cboItemType.CalculationExpression = Nothing
        Me.cboItemType.DropDownAnimationEnabled = True
        Me.cboItemType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboItemType.FieldCode = Nothing
        Me.cboItemType.FieldDesc = Nothing
        Me.cboItemType.FieldMaxLength = 0
        Me.cboItemType.FieldName = Nothing
        Me.cboItemType.isCalculatedField = False
        Me.cboItemType.IsSourceFromTable = False
        Me.cboItemType.IsSourceFromValueList = False
        Me.cboItemType.IsUnique = False
        Me.cboItemType.Location = New System.Drawing.Point(67, 51)
        Me.cboItemType.MendatroryField = True
        Me.cboItemType.MyLinkLable1 = Me.RadLabel29
        Me.cboItemType.MyLinkLable2 = Nothing
        Me.cboItemType.Name = "cboItemType"
        Me.cboItemType.ReferenceFieldDesc = Nothing
        Me.cboItemType.ReferenceFieldName = Nothing
        Me.cboItemType.ReferenceTableName = Nothing
        Me.cboItemType.Size = New System.Drawing.Size(245, 20)
        Me.cboItemType.TabIndex = 1518
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
        Me.txtDate.Location = New System.Drawing.Point(482, 10)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.MyLabel4
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(79, 18)
        Me.txtDate.TabIndex = 0
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(402, 11)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(69, 16)
        Me.MyLabel4.TabIndex = 1517
        Me.MyLabel4.Text = "Tender Date"
        '
        'btnCopy
        '
        Me.btnCopy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCopy.Location = New System.Drawing.Point(372, 10)
        Me.btnCopy.Name = "btnCopy"
        Me.btnCopy.Size = New System.Drawing.Size(23, 19)
        Me.btnCopy.TabIndex = 1515
        Me.btnCopy.Text = "CC"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(6, 11)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(33, 16)
        Me.RadLabel1.TabIndex = 22
        Me.RadLabel1.Text = "Code"
        '
        'txtItem
        '
        Me.txtItem.CalculationExpression = Nothing
        Me.txtItem.FieldCode = Nothing
        Me.txtItem.FieldDesc = Nothing
        Me.txtItem.FieldMaxLength = 0
        Me.txtItem.FieldName = Nothing
        Me.txtItem.isCalculatedField = False
        Me.txtItem.IsSourceFromTable = False
        Me.txtItem.IsSourceFromValueList = False
        Me.txtItem.IsUnique = False
        Me.txtItem.Location = New System.Drawing.Point(67, 94)
        Me.txtItem.MendatroryField = True
        Me.txtItem.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItem.MyLinkLable1 = Me.RadLabel2
        Me.txtItem.MyLinkLable2 = Nothing
        Me.txtItem.MyReadOnly = False
        Me.txtItem.MyShowMasterFormButton = False
        Me.txtItem.Name = "txtItem"
        Me.txtItem.ReferenceFieldDesc = Nothing
        Me.txtItem.ReferenceFieldName = Nothing
        Me.txtItem.ReferenceTableName = Nothing
        Me.txtItem.Size = New System.Drawing.Size(245, 18)
        Me.txtItem.TabIndex = 1514
        Me.txtItem.Value = ""
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(6, 95)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(28, 16)
        Me.RadLabel2.TabIndex = 1513
        Me.RadLabel2.Text = "Item"
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(67, 10)
        Me.txtDocNo.MendatroryField = False
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Me.RadLabel1
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 32767
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(285, 19)
        Me.txtDocNo.TabIndex = 0
        Me.txtDocNo.TabStop = False
        Me.txtDocNo.Value = ""
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.XpertERPPurchase.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(352, 10)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 19)
        Me.btnAddNew.TabIndex = 1
        '
        'btnSubmit
        '
        Me.btnSubmit.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSubmit.Location = New System.Drawing.Point(312, 94)
        Me.btnSubmit.Name = "btnSubmit"
        Me.btnSubmit.Size = New System.Drawing.Size(83, 18)
        Me.btnSubmit.TabIndex = 1512
        Me.btnSubmit.Text = "Submit"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(572, 10)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(96, 19)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 1510
        '
        'lblAbandonmentNo
        '
        Me.lblAbandonmentNo.FieldName = Nothing
        Me.lblAbandonmentNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAbandonmentNo.Location = New System.Drawing.Point(775, 18)
        Me.lblAbandonmentNo.Name = "lblAbandonmentNo"
        Me.lblAbandonmentNo.Size = New System.Drawing.Size(2, 2)
        Me.lblAbandonmentNo.TabIndex = 27
        '
        'MyLabel14
        '
        Me.MyLabel14.FieldName = Nothing
        Me.MyLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel14.Location = New System.Drawing.Point(705, 73)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(72, 16)
        Me.MyLabel14.TabIndex = 1509
        Me.MyLabel14.Text = "Field Value 6"
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(705, 53)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(72, 16)
        Me.MyLabel6.TabIndex = 1508
        Me.MyLabel6.Text = "Field Value 5"
        '
        'lblTenderSqNo
        '
        Me.lblTenderSqNo.FieldName = Nothing
        Me.lblTenderSqNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTenderSqNo.Location = New System.Drawing.Point(705, 11)
        Me.lblTenderSqNo.Name = "lblTenderSqNo"
        Me.lblTenderSqNo.Size = New System.Drawing.Size(67, 16)
        Me.lblTenderSqNo.TabIndex = 1442
        Me.lblTenderSqNo.Text = "Tender SNo"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(705, 32)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(72, 16)
        Me.MyLabel5.TabIndex = 1507
        Me.MyLabel5.Text = "Field Value 4"
        '
        'txtFieldValue1
        '
        Me.txtFieldValue1.CalculationExpression = Nothing
        Me.txtFieldValue1.FieldCode = Nothing
        Me.txtFieldValue1.FieldDesc = Nothing
        Me.txtFieldValue1.FieldMaxLength = 0
        Me.txtFieldValue1.FieldName = Nothing
        Me.txtFieldValue1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFieldValue1.isCalculatedField = False
        Me.txtFieldValue1.IsSourceFromTable = False
        Me.txtFieldValue1.IsSourceFromValueList = False
        Me.txtFieldValue1.IsUnique = False
        Me.txtFieldValue1.Location = New System.Drawing.Point(477, 31)
        Me.txtFieldValue1.MaxLength = 30
        Me.txtFieldValue1.MendatroryField = False
        Me.txtFieldValue1.MyLinkLable1 = Nothing
        Me.txtFieldValue1.MyLinkLable2 = Nothing
        Me.txtFieldValue1.Name = "txtFieldValue1"
        Me.txtFieldValue1.ReferenceFieldDesc = Nothing
        Me.txtFieldValue1.ReferenceFieldName = Nothing
        Me.txtFieldValue1.ReferenceTableName = Nothing
        Me.txtFieldValue1.Size = New System.Drawing.Size(225, 18)
        Me.txtFieldValue1.TabIndex = 1444
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(402, 73)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(72, 16)
        Me.MyLabel3.TabIndex = 1506
        Me.MyLabel3.Text = "Field Value 3"
        '
        'lblTenderSeqNo
        '
        Me.lblTenderSeqNo.AutoSize = False
        Me.lblTenderSeqNo.BorderVisible = True
        Me.lblTenderSeqNo.FieldName = Nothing
        Me.lblTenderSeqNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTenderSeqNo.Location = New System.Drawing.Point(789, 10)
        Me.lblTenderSeqNo.Name = "lblTenderSeqNo"
        Me.lblTenderSeqNo.Size = New System.Drawing.Size(225, 19)
        Me.lblTenderSeqNo.TabIndex = 1442
        Me.lblTenderSeqNo.TextWrap = False
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(402, 53)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(72, 16)
        Me.MyLabel2.TabIndex = 1505
        Me.MyLabel2.Text = "Field Value 2"
        '
        'txtFieldValue3
        '
        Me.txtFieldValue3.CalculationExpression = Nothing
        Me.txtFieldValue3.FieldCode = Nothing
        Me.txtFieldValue3.FieldDesc = Nothing
        Me.txtFieldValue3.FieldMaxLength = 0
        Me.txtFieldValue3.FieldName = Nothing
        Me.txtFieldValue3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFieldValue3.isCalculatedField = False
        Me.txtFieldValue3.IsSourceFromTable = False
        Me.txtFieldValue3.IsSourceFromValueList = False
        Me.txtFieldValue3.IsUnique = False
        Me.txtFieldValue3.Location = New System.Drawing.Point(477, 72)
        Me.txtFieldValue3.MaxLength = 30
        Me.txtFieldValue3.MendatroryField = False
        Me.txtFieldValue3.MyLinkLable1 = Nothing
        Me.txtFieldValue3.MyLinkLable2 = Nothing
        Me.txtFieldValue3.Name = "txtFieldValue3"
        Me.txtFieldValue3.ReferenceFieldDesc = Nothing
        Me.txtFieldValue3.ReferenceFieldName = Nothing
        Me.txtFieldValue3.ReferenceTableName = Nothing
        Me.txtFieldValue3.Size = New System.Drawing.Size(225, 18)
        Me.txtFieldValue3.TabIndex = 1499
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(402, 32)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(72, 16)
        Me.MyLabel1.TabIndex = 1504
        Me.MyLabel1.Text = "Field Value 1"
        '
        'txtFieldValue6
        '
        Me.txtFieldValue6.CalculationExpression = Nothing
        Me.txtFieldValue6.FieldCode = Nothing
        Me.txtFieldValue6.FieldDesc = Nothing
        Me.txtFieldValue6.FieldMaxLength = 0
        Me.txtFieldValue6.FieldName = Nothing
        Me.txtFieldValue6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFieldValue6.isCalculatedField = False
        Me.txtFieldValue6.IsSourceFromTable = False
        Me.txtFieldValue6.IsSourceFromValueList = False
        Me.txtFieldValue6.IsUnique = False
        Me.txtFieldValue6.Location = New System.Drawing.Point(789, 72)
        Me.txtFieldValue6.MaxLength = 30
        Me.txtFieldValue6.MendatroryField = False
        Me.txtFieldValue6.MyLinkLable1 = Nothing
        Me.txtFieldValue6.MyLinkLable2 = Nothing
        Me.txtFieldValue6.Name = "txtFieldValue6"
        Me.txtFieldValue6.ReferenceFieldDesc = Nothing
        Me.txtFieldValue6.ReferenceFieldName = Nothing
        Me.txtFieldValue6.ReferenceTableName = Nothing
        Me.txtFieldValue6.Size = New System.Drawing.Size(225, 18)
        Me.txtFieldValue6.TabIndex = 1500
        '
        'txtFieldValue4
        '
        Me.txtFieldValue4.CalculationExpression = Nothing
        Me.txtFieldValue4.FieldCode = Nothing
        Me.txtFieldValue4.FieldDesc = Nothing
        Me.txtFieldValue4.FieldMaxLength = 0
        Me.txtFieldValue4.FieldName = Nothing
        Me.txtFieldValue4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFieldValue4.isCalculatedField = False
        Me.txtFieldValue4.IsSourceFromTable = False
        Me.txtFieldValue4.IsSourceFromValueList = False
        Me.txtFieldValue4.IsUnique = False
        Me.txtFieldValue4.Location = New System.Drawing.Point(789, 31)
        Me.txtFieldValue4.MaxLength = 30
        Me.txtFieldValue4.MendatroryField = False
        Me.txtFieldValue4.MyLinkLable1 = Nothing
        Me.txtFieldValue4.MyLinkLable2 = Nothing
        Me.txtFieldValue4.Name = "txtFieldValue4"
        Me.txtFieldValue4.ReferenceFieldDesc = Nothing
        Me.txtFieldValue4.ReferenceFieldName = Nothing
        Me.txtFieldValue4.ReferenceTableName = Nothing
        Me.txtFieldValue4.Size = New System.Drawing.Size(225, 18)
        Me.txtFieldValue4.TabIndex = 1503
        '
        'txtFieldValue2
        '
        Me.txtFieldValue2.CalculationExpression = Nothing
        Me.txtFieldValue2.FieldCode = Nothing
        Me.txtFieldValue2.FieldDesc = Nothing
        Me.txtFieldValue2.FieldMaxLength = 0
        Me.txtFieldValue2.FieldName = Nothing
        Me.txtFieldValue2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFieldValue2.isCalculatedField = False
        Me.txtFieldValue2.IsSourceFromTable = False
        Me.txtFieldValue2.IsSourceFromValueList = False
        Me.txtFieldValue2.IsUnique = False
        Me.txtFieldValue2.Location = New System.Drawing.Point(477, 52)
        Me.txtFieldValue2.MaxLength = 30
        Me.txtFieldValue2.MendatroryField = False
        Me.txtFieldValue2.MyLinkLable1 = Nothing
        Me.txtFieldValue2.MyLinkLable2 = Nothing
        Me.txtFieldValue2.Name = "txtFieldValue2"
        Me.txtFieldValue2.ReferenceFieldDesc = Nothing
        Me.txtFieldValue2.ReferenceFieldName = Nothing
        Me.txtFieldValue2.ReferenceTableName = Nothing
        Me.txtFieldValue2.Size = New System.Drawing.Size(225, 18)
        Me.txtFieldValue2.TabIndex = 1501
        '
        'txtFieldValue5
        '
        Me.txtFieldValue5.CalculationExpression = Nothing
        Me.txtFieldValue5.FieldCode = Nothing
        Me.txtFieldValue5.FieldDesc = Nothing
        Me.txtFieldValue5.FieldMaxLength = 0
        Me.txtFieldValue5.FieldName = Nothing
        Me.txtFieldValue5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFieldValue5.isCalculatedField = False
        Me.txtFieldValue5.IsSourceFromTable = False
        Me.txtFieldValue5.IsSourceFromValueList = False
        Me.txtFieldValue5.IsUnique = False
        Me.txtFieldValue5.Location = New System.Drawing.Point(789, 52)
        Me.txtFieldValue5.MaxLength = 30
        Me.txtFieldValue5.MendatroryField = False
        Me.txtFieldValue5.MyLinkLable1 = Nothing
        Me.txtFieldValue5.MyLinkLable2 = Nothing
        Me.txtFieldValue5.Name = "txtFieldValue5"
        Me.txtFieldValue5.ReferenceFieldDesc = Nothing
        Me.txtFieldValue5.ReferenceFieldName = Nothing
        Me.txtFieldValue5.ReferenceTableName = Nothing
        Me.txtFieldValue5.Size = New System.Drawing.Size(225, 18)
        Me.txtFieldValue5.TabIndex = 1502
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer3.IsSplitterFixed = True
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.RadGroupBox2)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.RadGroupBox1)
        Me.SplitContainer3.Size = New System.Drawing.Size(1077, 324)
        Me.SplitContainer3.SplitterDistance = 165
        Me.SplitContainer3.TabIndex = 0
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.gv1)
        Me.RadGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "Entry"
        Me.RadGroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(1077, 165)
        Me.RadGroupBox2.TabIndex = 28
        Me.RadGroupBox2.Text = "Entry"
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
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(1057, 135)
        Me.gv1.TabIndex = 17
        Me.gv1.TabStop = False
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.gv2)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = "Save"
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(1077, 155)
        Me.RadGroupBox1.TabIndex = 1511
        Me.RadGroupBox1.Text = "Save"
        '
        'gv2
        '
        Me.gv2.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv2.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv2.ForeColor = System.Drawing.Color.Black
        Me.gv2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv2.Location = New System.Drawing.Point(10, 20)
        '
        '
        '
        Me.gv2.MasterTemplate.AllowDeleteRow = False
        Me.gv2.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv2.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv2.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gv2.Name = "gv2"
        Me.gv2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv2.ShowGroupPanel = False
        Me.gv2.ShowHeaderCellButtons = True
        Me.gv2.Size = New System.Drawing.Size(1057, 125)
        Me.gv2.TabIndex = 17
        Me.gv2.TabStop = False
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel22)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel20)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel19)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel18)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel15)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel13)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel10)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel9)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel8)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel7)
        Me.RadPageViewPage2.Controls.Add(Me.txtOtherInfo5)
        Me.RadPageViewPage2.Controls.Add(Me.txtOtherInfo4)
        Me.RadPageViewPage2.Controls.Add(Me.txtOtherInfo2)
        Me.RadPageViewPage2.Controls.Add(Me.txtOtherInfo7)
        Me.RadPageViewPage2.Controls.Add(Me.txtOtherInfo9)
        Me.RadPageViewPage2.Controls.Add(Me.txtOtherInfo10)
        Me.RadPageViewPage2.Controls.Add(Me.txtOtherInfo8)
        Me.RadPageViewPage2.Controls.Add(Me.txtOtherInfo6)
        Me.RadPageViewPage2.Controls.Add(Me.txtOtherInfo3)
        Me.RadPageViewPage2.Controls.Add(Me.txtOtherInfo1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(104.0!, 26.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1077, 446)
        Me.RadPageViewPage2.Text = "Other Information"
        '
        'MyLabel22
        '
        Me.MyLabel22.FieldName = Nothing
        Me.MyLabel22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel22.Location = New System.Drawing.Point(15, 334)
        Me.MyLabel22.Name = "MyLabel22"
        Me.MyLabel22.Size = New System.Drawing.Size(70, 16)
        Me.MyLabel22.TabIndex = 149
        Me.MyLabel22.Text = "Other Info 5"
        '
        'MyLabel20
        '
        Me.MyLabel20.FieldName = Nothing
        Me.MyLabel20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel20.Location = New System.Drawing.Point(15, 246)
        Me.MyLabel20.Name = "MyLabel20"
        Me.MyLabel20.Size = New System.Drawing.Size(70, 16)
        Me.MyLabel20.TabIndex = 148
        Me.MyLabel20.Text = "Other Info 4"
        '
        'MyLabel19
        '
        Me.MyLabel19.FieldName = Nothing
        Me.MyLabel19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel19.Location = New System.Drawing.Point(15, 161)
        Me.MyLabel19.Name = "MyLabel19"
        Me.MyLabel19.Size = New System.Drawing.Size(70, 16)
        Me.MyLabel19.TabIndex = 147
        Me.MyLabel19.Text = "Other Info 3"
        '
        'MyLabel18
        '
        Me.MyLabel18.FieldName = Nothing
        Me.MyLabel18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel18.Location = New System.Drawing.Point(15, 79)
        Me.MyLabel18.Name = "MyLabel18"
        Me.MyLabel18.Size = New System.Drawing.Size(70, 16)
        Me.MyLabel18.TabIndex = 146
        Me.MyLabel18.Text = "Other Info 2"
        '
        'MyLabel15
        '
        Me.MyLabel15.FieldName = Nothing
        Me.MyLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel15.Location = New System.Drawing.Point(15, 3)
        Me.MyLabel15.Name = "MyLabel15"
        Me.MyLabel15.Size = New System.Drawing.Size(70, 16)
        Me.MyLabel15.TabIndex = 145
        Me.MyLabel15.Text = "Other Info 1"
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel13.Location = New System.Drawing.Point(567, 330)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(76, 16)
        Me.MyLabel13.TabIndex = 144
        Me.MyLabel13.Text = "Other Info 10"
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(567, 246)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(70, 16)
        Me.MyLabel10.TabIndex = 143
        Me.MyLabel10.Text = "Other Info 9"
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(567, 161)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(70, 16)
        Me.MyLabel9.TabIndex = 142
        Me.MyLabel9.Text = "Other Info 8"
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(567, 79)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(70, 16)
        Me.MyLabel8.TabIndex = 141
        Me.MyLabel8.Text = "Other Info 7"
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(567, 3)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(70, 16)
        Me.MyLabel7.TabIndex = 140
        Me.MyLabel7.Text = "Other Info 6"
        '
        'txtOtherInfo5
        '
        Me.txtOtherInfo5.Location = New System.Drawing.Point(129, 330)
        Me.txtOtherInfo5.Multiline = True
        Me.txtOtherInfo5.Name = "txtOtherInfo5"
        Me.txtOtherInfo5.Size = New System.Drawing.Size(400, 70)
        Me.txtOtherInfo5.TabIndex = 12
        '
        'txtOtherInfo4
        '
        Me.txtOtherInfo4.Location = New System.Drawing.Point(129, 246)
        Me.txtOtherInfo4.Multiline = True
        Me.txtOtherInfo4.Name = "txtOtherInfo4"
        Me.txtOtherInfo4.Size = New System.Drawing.Size(400, 70)
        Me.txtOtherInfo4.TabIndex = 11
        '
        'txtOtherInfo2
        '
        Me.txtOtherInfo2.Location = New System.Drawing.Point(129, 79)
        Me.txtOtherInfo2.Multiline = True
        Me.txtOtherInfo2.Name = "txtOtherInfo2"
        Me.txtOtherInfo2.Size = New System.Drawing.Size(400, 70)
        Me.txtOtherInfo2.TabIndex = 10
        '
        'txtOtherInfo7
        '
        Me.txtOtherInfo7.Location = New System.Drawing.Point(674, 79)
        Me.txtOtherInfo7.Multiline = True
        Me.txtOtherInfo7.Name = "txtOtherInfo7"
        Me.txtOtherInfo7.Size = New System.Drawing.Size(400, 70)
        Me.txtOtherInfo7.TabIndex = 9
        '
        'txtOtherInfo9
        '
        Me.txtOtherInfo9.Location = New System.Drawing.Point(674, 246)
        Me.txtOtherInfo9.Multiline = True
        Me.txtOtherInfo9.Name = "txtOtherInfo9"
        Me.txtOtherInfo9.Size = New System.Drawing.Size(400, 70)
        Me.txtOtherInfo9.TabIndex = 8
        '
        'txtOtherInfo10
        '
        Me.txtOtherInfo10.Location = New System.Drawing.Point(674, 330)
        Me.txtOtherInfo10.Multiline = True
        Me.txtOtherInfo10.Name = "txtOtherInfo10"
        Me.txtOtherInfo10.Size = New System.Drawing.Size(400, 70)
        Me.txtOtherInfo10.TabIndex = 7
        '
        'txtOtherInfo8
        '
        Me.txtOtherInfo8.Location = New System.Drawing.Point(674, 161)
        Me.txtOtherInfo8.Multiline = True
        Me.txtOtherInfo8.Name = "txtOtherInfo8"
        Me.txtOtherInfo8.Size = New System.Drawing.Size(400, 70)
        Me.txtOtherInfo8.TabIndex = 6
        '
        'txtOtherInfo6
        '
        Me.txtOtherInfo6.Location = New System.Drawing.Point(674, 3)
        Me.txtOtherInfo6.Multiline = True
        Me.txtOtherInfo6.Name = "txtOtherInfo6"
        Me.txtOtherInfo6.Size = New System.Drawing.Size(400, 70)
        Me.txtOtherInfo6.TabIndex = 5
        '
        'txtOtherInfo3
        '
        Me.txtOtherInfo3.Location = New System.Drawing.Point(129, 161)
        Me.txtOtherInfo3.Multiline = True
        Me.txtOtherInfo3.Name = "txtOtherInfo3"
        Me.txtOtherInfo3.Size = New System.Drawing.Size(400, 70)
        Me.txtOtherInfo3.TabIndex = 4
        '
        'txtOtherInfo1
        '
        Me.txtOtherInfo1.Location = New System.Drawing.Point(129, 3)
        Me.txtOtherInfo1.Multiline = True
        Me.txtOtherInfo1.Name = "txtOtherInfo1"
        Me.txtOtherInfo1.Size = New System.Drawing.Size(400, 70)
        Me.txtOtherInfo1.TabIndex = 3
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.gvSchedule)
        Me.RadPageViewPage3.Controls.Add(Me.Panel1)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel38)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(84.0!, 26.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(1077, 446)
        Me.RadPageViewPage3.Text = "Set Schedule"
        '
        'gvSchedule
        '
        Me.gvSchedule.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvSchedule.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvSchedule.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvSchedule.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvSchedule.ForeColor = System.Drawing.Color.Black
        Me.gvSchedule.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvSchedule.Location = New System.Drawing.Point(0, 28)
        '
        '
        '
        Me.gvSchedule.MasterTemplate.AllowDeleteRow = False
        Me.gvSchedule.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvSchedule.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvSchedule.MasterTemplate.ViewDefinition = TableViewDefinition3
        Me.gvSchedule.Name = "gvSchedule"
        Me.gvSchedule.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvSchedule.ShowGroupPanel = False
        Me.gvSchedule.ShowHeaderCellButtons = True
        Me.gvSchedule.Size = New System.Drawing.Size(1077, 405)
        Me.gvSchedule.TabIndex = 18
        Me.gvSchedule.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lblExtensionDays)
        Me.Panel1.Controls.Add(Me.txtExtensionDays)
        Me.Panel1.Controls.Add(Me.btnExtensionUpdate)
        Me.Panel1.Controls.Add(Me.btnApplyAll)
        Me.Panel1.Controls.Add(Me.RadButton1)
        Me.Panel1.Controls.Add(Me.txtScheduleStartDate)
        Me.Panel1.Controls.Add(Me.MyLabel12)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1077, 28)
        Me.Panel1.TabIndex = 0
        '
        'lblExtensionDays
        '
        Me.lblExtensionDays.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblExtensionDays.FieldName = Nothing
        Me.lblExtensionDays.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExtensionDays.Location = New System.Drawing.Point(731, 8)
        Me.lblExtensionDays.Name = "lblExtensionDays"
        Me.lblExtensionDays.Size = New System.Drawing.Size(85, 16)
        Me.lblExtensionDays.TabIndex = 1524
        Me.lblExtensionDays.Text = "Extension Days"
        '
        'txtExtensionDays
        '
        Me.txtExtensionDays.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtExtensionDays.BackColor = System.Drawing.Color.White
        Me.txtExtensionDays.CalculationExpression = Nothing
        Me.txtExtensionDays.DecimalPlaces = 0
        Me.txtExtensionDays.FieldCode = Nothing
        Me.txtExtensionDays.FieldDesc = Nothing
        Me.txtExtensionDays.FieldMaxLength = 0
        Me.txtExtensionDays.FieldName = Nothing
        Me.txtExtensionDays.isCalculatedField = False
        Me.txtExtensionDays.IsSourceFromTable = False
        Me.txtExtensionDays.IsSourceFromValueList = False
        Me.txtExtensionDays.IsUnique = False
        Me.txtExtensionDays.Location = New System.Drawing.Point(822, 5)
        Me.txtExtensionDays.MaxLength = 2
        Me.txtExtensionDays.MendatroryField = False
        Me.txtExtensionDays.MyLinkLable1 = Me.lblExtensionDays
        Me.txtExtensionDays.MyLinkLable2 = Nothing
        Me.txtExtensionDays.Name = "txtExtensionDays"
        Me.txtExtensionDays.ReferenceFieldDesc = Nothing
        Me.txtExtensionDays.ReferenceFieldName = Nothing
        Me.txtExtensionDays.ReferenceTableName = Nothing
        Me.txtExtensionDays.Size = New System.Drawing.Size(47, 20)
        Me.txtExtensionDays.TabIndex = 1523
        Me.txtExtensionDays.Text = "0"
        Me.txtExtensionDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtExtensionDays.Value = 0R
        '
        'btnExtensionUpdate
        '
        Me.btnExtensionUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExtensionUpdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExtensionUpdate.Location = New System.Drawing.Point(936, 7)
        Me.btnExtensionUpdate.Name = "btnExtensionUpdate"
        Me.btnExtensionUpdate.Size = New System.Drawing.Size(133, 18)
        Me.btnExtensionUpdate.TabIndex = 1522
        Me.btnExtensionUpdate.Text = "Update Extension Days"
        '
        'btnApplyAll
        '
        Me.btnApplyAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnApplyAll.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnApplyAll.Location = New System.Drawing.Point(875, 7)
        Me.btnApplyAll.Name = "btnApplyAll"
        Me.btnApplyAll.Size = New System.Drawing.Size(55, 18)
        Me.btnApplyAll.TabIndex = 1521
        Me.btnApplyAll.Text = "Apply All"
        '
        'RadButton1
        '
        Me.RadButton1.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.RadButton1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton1.Location = New System.Drawing.Point(202, 5)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(55, 18)
        Me.RadButton1.TabIndex = 1520
        Me.RadButton1.Text = ">>"
        '
        'txtScheduleStartDate
        '
        Me.txtScheduleStartDate.CalculationExpression = Nothing
        Me.txtScheduleStartDate.CustomFormat = "dd/MM/yyyy"
        Me.txtScheduleStartDate.FieldCode = Nothing
        Me.txtScheduleStartDate.FieldDesc = Nothing
        Me.txtScheduleStartDate.FieldMaxLength = 0
        Me.txtScheduleStartDate.FieldName = Nothing
        Me.txtScheduleStartDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtScheduleStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtScheduleStartDate.isCalculatedField = False
        Me.txtScheduleStartDate.IsSourceFromTable = False
        Me.txtScheduleStartDate.IsSourceFromValueList = False
        Me.txtScheduleStartDate.IsUnique = False
        Me.txtScheduleStartDate.Location = New System.Drawing.Point(120, 5)
        Me.txtScheduleStartDate.MendatroryField = False
        Me.txtScheduleStartDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtScheduleStartDate.MyLinkLable1 = Me.MyLabel12
        Me.txtScheduleStartDate.MyLinkLable2 = Nothing
        Me.txtScheduleStartDate.Name = "txtScheduleStartDate"
        Me.txtScheduleStartDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtScheduleStartDate.ReferenceFieldDesc = Nothing
        Me.txtScheduleStartDate.ReferenceFieldName = Nothing
        Me.txtScheduleStartDate.ReferenceTableName = Nothing
        Me.txtScheduleStartDate.Size = New System.Drawing.Size(79, 18)
        Me.txtScheduleStartDate.TabIndex = 1518
        Me.txtScheduleStartDate.TabStop = False
        Me.txtScheduleStartDate.Text = "13/06/2011"
        Me.txtScheduleStartDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel12.Location = New System.Drawing.Point(5, 6)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(108, 16)
        Me.MyLabel12.TabIndex = 1519
        Me.MyLabel12.Text = "Schedule Start Date"
        '
        'MyLabel38
        '
        Me.MyLabel38.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.MyLabel38.FieldName = Nothing
        Me.MyLabel38.Font = New System.Drawing.Font("Arial", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel38.ForeColor = System.Drawing.SystemColors.Highlight
        Me.MyLabel38.Location = New System.Drawing.Point(0, 433)
        Me.MyLabel38.Name = "MyLabel38"
        Me.MyLabel38.Size = New System.Drawing.Size(1077, 13)
        Me.MyLabel38.TabIndex = 1521
        Me.MyLabel38.Text = "Press F5 To View Penelty Details"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(202, 2)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 22)
        Me.btnPrint.TabIndex = 1502
        Me.btnPrint.Text = "Print"
        '
        'MyLabel11
        '
        Me.MyLabel11.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel11.Location = New System.Drawing.Point(770, 5)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(138, 16)
        Me.MyLabel11.TabIndex = 126
        Me.MyLabel11.Text = "Total Document Amount"
        '
        'lblCreatedByValue
        '
        Me.lblCreatedByValue.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblCreatedByValue.FieldName = Nothing
        Me.lblCreatedByValue.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCreatedByValue.Location = New System.Drawing.Point(882, 2)
        Me.lblCreatedByValue.Name = "lblCreatedByValue"
        Me.lblCreatedByValue.Size = New System.Drawing.Size(2, 2)
        Me.lblCreatedByValue.TabIndex = 1501
        '
        'lblTotalDocAmt
        '
        Me.lblTotalDocAmt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTotalDocAmt.AutoSize = False
        Me.lblTotalDocAmt.BorderVisible = True
        Me.lblTotalDocAmt.FieldName = Nothing
        Me.lblTotalDocAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalDocAmt.Location = New System.Drawing.Point(914, 4)
        Me.lblTotalDocAmt.Name = "lblTotalDocAmt"
        Me.lblTotalDocAmt.Size = New System.Drawing.Size(104, 19)
        Me.lblTotalDocAmt.TabIndex = 136
        Me.lblTotalDocAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnreverse
        '
        Me.btnreverse.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreverse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnreverse.Location = New System.Drawing.Point(272, 2)
        Me.btnreverse.Name = "btnreverse"
        Me.btnreverse.Size = New System.Drawing.Size(73, 22)
        Me.btnreverse.TabIndex = 21
        Me.btnreverse.Text = "Reverse"
        Me.btnreverse.Visible = False
        '
        'RadLabel10
        '
        Me.RadLabel10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadLabel10.FieldName = Nothing
        Me.RadLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel10.ForeColor = System.Drawing.SystemColors.Highlight
        Me.RadLabel10.Location = New System.Drawing.Point(582, 5)
        Me.RadLabel10.Name = "RadLabel10"
        Me.RadLabel10.Size = New System.Drawing.Size(158, 16)
        Me.RadLabel10.TabIndex = 1485
        Me.RadLabel10.Text = "Press Alt+S To Save/Update"
        Me.RadLabel10.Visible = False
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(133, 2)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(67, 22)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "Delete"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(68, 2)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(63, 22)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(1024, 2)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 10
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(5, 2)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(61, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.RadMenu1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1098, 25)
        Me.Panel2.TabIndex = 5
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2, Me.RadMenuItem7})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1098, 20)
        Me.RadMenu1.TabIndex = 0
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3, Me.RadMenuItem1, Me.RadMenuItem4, Me.RadMenuItem5})
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Setting"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "E-Mail/SMS Setting"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Save Layout"
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.AccessibleDescription = "RadMenuItem4"
        Me.RadMenuItem4.AccessibleName = "RadMenuItem4"
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Delete Layout"
        '
        'RadMenuItem5
        '
        Me.RadMenuItem5.AccessibleDescription = "RadMenuItem5"
        Me.RadMenuItem5.AccessibleName = "RadMenuItem5"
        Me.RadMenuItem5.Name = "RadMenuItem5"
        Me.RadMenuItem5.Text = "Footer Setting"
        '
        'RadMenuItem7
        '
        Me.RadMenuItem7.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem8, Me.RadMenuItem9})
        Me.RadMenuItem7.Name = "RadMenuItem7"
        Me.RadMenuItem7.Text = "File"
        '
        'RadMenuItem8
        '
        Me.RadMenuItem8.AccessibleDescription = "import"
        Me.RadMenuItem8.AccessibleName = "import"
        Me.RadMenuItem8.Name = "RadMenuItem8"
        Me.RadMenuItem8.Text = "Import"
        '
        'RadMenuItem9
        '
        Me.RadMenuItem9.Name = "RadMenuItem9"
        Me.RadMenuItem9.Text = "Export"
        '
        'MenuClose
        '
        Me.MenuClose.DisplayStyle = Telerik.WinControls.DisplayStyle.Text
        Me.MenuClose.Items.AddRange(New Telerik.WinControls.RadItem() {Me.MenuImport, Me.MenuExport, Me.RadMenuItem6})
        Me.MenuClose.Name = "MenuClose"
        Me.MenuClose.Text = "File"
        Me.MenuClose.UseCompatibleTextRendering = False
        '
        'MenuImport
        '
        Me.MenuImport.AccessibleDescription = "MenuImport"
        Me.MenuImport.AccessibleName = "RadMenuItem2"
        Me.MenuImport.Name = "MenuImport"
        Me.MenuImport.Text = "Import"
        Me.MenuImport.UseCompatibleTextRendering = False
        '
        'MenuExport
        '
        Me.MenuExport.AccessibleDescription = "MenuExport"
        Me.MenuExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.ExportData, Me.ExportBlankSheet})
        Me.MenuExport.Name = "MenuExport"
        Me.MenuExport.Text = "Export"
        Me.MenuExport.UseCompatibleTextRendering = False
        '
        'ExportData
        '
        Me.ExportData.Name = "ExportData"
        Me.ExportData.Text = "Export Data"
        Me.ExportData.UseCompatibleTextRendering = False
        '
        'ExportBlankSheet
        '
        Me.ExportBlankSheet.Name = "ExportBlankSheet"
        Me.ExportBlankSheet.Text = "Export Blank Sheet"
        Me.ExportBlankSheet.UseCompatibleTextRendering = False
        '
        'RadMenuItem6
        '
        Me.RadMenuItem6.AccessibleDescription = "RadMenuItem4"
        Me.RadMenuItem6.Name = "RadMenuItem6"
        Me.RadMenuItem6.Text = "Close"
        Me.RadMenuItem6.UseCompatibleTextRendering = False
        '
        'MyLabel17
        '
        Me.MyLabel17.FieldName = Nothing
        Me.MyLabel17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel17.Location = New System.Drawing.Point(5, 74)
        Me.MyLabel17.Name = "MyLabel17"
        Me.MyLabel17.Size = New System.Drawing.Size(35, 16)
        Me.MyLabel17.TabIndex = 1523
        Me.MyLabel17.Text = "Mode"
        '
        'cboMode
        '
        Me.cboMode.AutoCompleteDisplayMember = Nothing
        Me.cboMode.AutoCompleteValueMember = Nothing
        Me.cboMode.CalculationExpression = Nothing
        Me.cboMode.DropDownAnimationEnabled = True
        Me.cboMode.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboMode.FieldCode = Nothing
        Me.cboMode.FieldDesc = Nothing
        Me.cboMode.FieldMaxLength = 0
        Me.cboMode.FieldName = Nothing
        Me.cboMode.isCalculatedField = False
        Me.cboMode.IsSourceFromTable = False
        Me.cboMode.IsSourceFromValueList = False
        Me.cboMode.IsUnique = False
        Me.cboMode.Location = New System.Drawing.Point(66, 72)
        Me.cboMode.MendatroryField = True
        Me.cboMode.MyLinkLable1 = Me.MyLabel17
        Me.cboMode.MyLinkLable2 = Nothing
        Me.cboMode.Name = "cboMode"
        Me.cboMode.ReferenceFieldDesc = Nothing
        Me.cboMode.ReferenceFieldName = Nothing
        Me.cboMode.ReferenceTableName = Nothing
        Me.cboMode.Size = New System.Drawing.Size(245, 20)
        Me.cboMode.TabIndex = 1522
        '
        'frmTender
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1098, 548)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.Panel2)
        Me.MinimumSize = New System.Drawing.Size(890, 467)
        Me.Name = "frmTender"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Tender"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboTenderType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel29, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboItemType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCopy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSubmit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAbandonmentNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTenderSqNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFieldValue1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTenderSeqNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFieldValue3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFieldValue6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFieldValue4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFieldValue2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFieldValue5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.RadPageViewPage2.PerformLayout()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage3.ResumeLayout(False)
        Me.RadPageViewPage3.PerformLayout()
        CType(Me.gvSchedule.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvSchedule, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.lblExtensionDays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtExtensionDays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExtensionUpdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnApplyAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtScheduleStartDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel38, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCreatedByValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotalDocAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboMode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents lblAbandonmentNo As common.Controls.MyLabel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem4 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem5 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents lblTenderSqNo As common.Controls.MyLabel
    Friend WithEvents txtFieldValue1 As common.Controls.MyTextBox
    Friend WithEvents btnreverse As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblTenderSeqNo As common.Controls.MyLabel
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents lblTotalDocAmt As common.Controls.MyLabel
    Friend WithEvents RadPageViewPage2 As RadPageViewPage
    Friend WithEvents lblCreatedByValue As common.Controls.MyLabel
    Friend WithEvents txtOtherInfo5 As TextBox
    Friend WithEvents txtOtherInfo4 As TextBox
    Friend WithEvents txtOtherInfo2 As TextBox
    Friend WithEvents txtOtherInfo7 As TextBox
    Friend WithEvents txtOtherInfo9 As TextBox
    Friend WithEvents txtOtherInfo10 As TextBox
    Friend WithEvents txtOtherInfo8 As TextBox
    Friend WithEvents txtOtherInfo6 As TextBox
    Friend WithEvents txtOtherInfo3 As TextBox
    Friend WithEvents txtOtherInfo1 As TextBox
    Friend WithEvents MyLabel22 As common.Controls.MyLabel
    Friend WithEvents MyLabel20 As common.Controls.MyLabel
    Friend WithEvents MyLabel19 As common.Controls.MyLabel
    Friend WithEvents MyLabel18 As common.Controls.MyLabel
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtFieldValue4 As common.Controls.MyTextBox
    Friend WithEvents txtFieldValue5 As common.Controls.MyTextBox
    Friend WithEvents txtFieldValue2 As common.Controls.MyTextBox
    Friend WithEvents txtFieldValue6 As common.Controls.MyTextBox
    Friend WithEvents txtFieldValue3 As common.Controls.MyTextBox
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents RadGroupBox1 As RadGroupBox
    Friend WithEvents gv2 As common.UserControls.MyRadGridView
    Friend WithEvents btnSubmit As RadButton
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents txtItem As common.UserControls.txtFinder
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents SplitContainer3 As SplitContainer
    Friend WithEvents btnPrint As RadButton
    Friend WithEvents btnCopy As RadButton
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents RadPageViewPage3 As RadPageViewPage
    Friend WithEvents gvSchedule As common.UserControls.MyRadGridView
    Friend WithEvents Panel1 As Panel
    Friend WithEvents txtScheduleStartDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents RadButton1 As RadButton
    Friend WithEvents RadLabel10 As common.Controls.MyLabel
    Friend WithEvents RadLabel29 As common.Controls.MyLabel
    Friend WithEvents cboItemType As common.Controls.MyComboBox
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents cboTenderType As common.Controls.MyComboBox
    Friend WithEvents MyLabel38 As common.Controls.MyLabel
    Friend WithEvents btnExtensionUpdate As RadButton
    Friend WithEvents btnApplyAll As RadButton
    Friend WithEvents txtExtensionDays As common.MyNumBox
    Friend WithEvents lblExtensionDays As common.Controls.MyLabel
    Friend WithEvents MenuClose As RadMenuItem
    Friend WithEvents MenuImport As RadMenuItem
    Friend WithEvents MenuExport As RadMenuItem
    Friend WithEvents ExportData As RadMenuItem
    Friend WithEvents ExportBlankSheet As RadMenuItem
    Friend WithEvents RadMenuItem6 As RadMenuItem
    Friend WithEvents RadMenu1 As RadMenu
    Friend WithEvents RadMenuItem7 As RadMenuItem
    Friend WithEvents RadMenuItem8 As RadMenuItem
    Friend WithEvents RadMenuItem9 As RadMenuItem
    Friend WithEvents MyLabel17 As common.Controls.MyLabel
    Friend WithEvents cboMode As common.Controls.MyComboBox
End Class

