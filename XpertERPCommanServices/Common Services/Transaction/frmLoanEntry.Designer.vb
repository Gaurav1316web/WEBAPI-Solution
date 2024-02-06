<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLoanEntry
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
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.lblCode = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.lblDocDate = New common.Controls.MyLabel()
        Me.cboTransactionType = New common.Controls.MyComboBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.cboLoanType = New common.Controls.MyComboBox()
        Me.lblBOMStatus = New common.Controls.MyLabel()
        Me.txtName = New common.Controls.MyTextBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.txtRemarks = New common.Controls.MyTextBox()
        Me.txtAccount = New common.UserControls.txtFinder()
        Me.lblAccount = New common.Controls.MyLabel()
        Me.lblAccountName = New common.Controls.MyLabel()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.txtInstallmentDate = New common.MyNumBox()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.lblInsatallmentAmount = New common.Controls.MyLabel()
        Me.txtLoanGivenOn = New common.Controls.MyDateTimePicker()
        Me.Txtdds = New common.Controls.MyLabel()
        Me.txtTenure = New common.MyNumBox()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.lblNoOfCans = New common.Controls.MyLabel()
        Me.txtLoanAmount = New common.MyNumBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtInterestRate = New common.MyNumBox()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv = New common.UserControls.MyRadGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.RadButton2 = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.BtnPost = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboTransactionType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboLoanType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBOMStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAccountName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtInstallmentDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblInsatallmentAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLoanGivenOn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Txtdds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTenure, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNoOfCans, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLoanAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtInterestRate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(720, 306)
        Me.SplitContainer1.SplitterDistance = 267
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(720, 267)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.lblCode)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.txtDate)
        Me.RadPageViewPage1.Controls.Add(Me.cboTransactionType)
        Me.RadPageViewPage1.Controls.Add(Me.lblDocDate)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.cboLoanType)
        Me.RadPageViewPage1.Controls.Add(Me.txtName)
        Me.RadPageViewPage1.Controls.Add(Me.lblBOMStatus)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.btnnew)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.txtCode)
        Me.RadPageViewPage1.Controls.Add(Me.txtRemarks)
        Me.RadPageViewPage1.Controls.Add(Me.txtAccount)
        Me.RadPageViewPage1.Controls.Add(Me.txtDescription)
        Me.RadPageViewPage1.Controls.Add(Me.lblAccount)
        Me.RadPageViewPage1.Controls.Add(Me.txtInstallmentDate)
        Me.RadPageViewPage1.Controls.Add(Me.lblAccountName)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel9)
        Me.RadPageViewPage1.Controls.Add(Me.UsLock1)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel8)
        Me.RadPageViewPage1.Controls.Add(Me.lblInsatallmentAmount)
        Me.RadPageViewPage1.Controls.Add(Me.txtLoanGivenOn)
        Me.RadPageViewPage1.Controls.Add(Me.Txtdds)
        Me.RadPageViewPage1.Controls.Add(Me.txtTenure)
        Me.RadPageViewPage1.Controls.Add(Me.lblNoOfCans)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel7)
        Me.RadPageViewPage1.Controls.Add(Me.txtLoanAmount)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.txtInterestRate)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(45.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(699, 219)
        Me.RadPageViewPage1.Text = "Detail"
        '
        'lblCode
        '
        Me.lblCode.FieldName = Nothing
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblCode.Location = New System.Drawing.Point(0, 3)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(75, 16)
        Me.lblCode.TabIndex = 26
        Me.lblCode.Text = "Document No"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(0, 160)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(51, 16)
        Me.MyLabel5.TabIndex = 19
        Me.MyLabel5.Text = "Remarks"
        '
        'txtDate
        '
        Me.txtDate.CalculationExpression = Nothing
        Me.txtDate.CustomFormat = "dd/MM/yyyy hh:mm:ss tt"
        Me.txtDate.FieldCode = Nothing
        Me.txtDate.FieldDesc = Nothing
        Me.txtDate.FieldMaxLength = 0
        Me.txtDate.FieldName = Nothing
        Me.txtDate.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDate.isCalculatedField = False
        Me.txtDate.IsSourceFromTable = False
        Me.txtDate.IsSourceFromValueList = False
        Me.txtDate.IsUnique = False
        Me.txtDate.Location = New System.Drawing.Point(440, 1)
        Me.txtDate.MendatroryField = True
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.lblDocDate
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(145, 20)
        Me.txtDate.TabIndex = 0
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "03/05/2011 12:00:00 AM"
        Me.txtDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblDocDate
        '
        Me.lblDocDate.FieldName = Nothing
        Me.lblDocDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocDate.Location = New System.Drawing.Point(404, 3)
        Me.lblDocDate.Name = "lblDocDate"
        Me.lblDocDate.Size = New System.Drawing.Size(30, 16)
        Me.lblDocDate.TabIndex = 29
        Me.lblDocDate.Text = "Date"
        '
        'cboTransactionType
        '
        Me.cboTransactionType.AutoCompleteDisplayMember = Nothing
        Me.cboTransactionType.AutoCompleteValueMember = Nothing
        Me.cboTransactionType.CalculationExpression = Nothing
        Me.cboTransactionType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboTransactionType.FieldCode = Nothing
        Me.cboTransactionType.FieldDesc = Nothing
        Me.cboTransactionType.FieldMaxLength = 0
        Me.cboTransactionType.FieldName = Nothing
        Me.cboTransactionType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboTransactionType.isCalculatedField = False
        Me.cboTransactionType.IsSourceFromTable = False
        Me.cboTransactionType.IsSourceFromValueList = False
        Me.cboTransactionType.IsUnique = False
        RadListDataItem1.Text = "M"
        RadListDataItem2.Text = "E"
        Me.cboTransactionType.Items.Add(RadListDataItem1)
        Me.cboTransactionType.Items.Add(RadListDataItem2)
        Me.cboTransactionType.Location = New System.Drawing.Point(110, 92)
        Me.cboTransactionType.MendatroryField = True
        Me.cboTransactionType.MyLinkLable1 = Me.MyLabel3
        Me.cboTransactionType.MyLinkLable2 = Nothing
        Me.cboTransactionType.Name = "cboTransactionType"
        Me.cboTransactionType.ReferenceFieldDesc = Nothing
        Me.cboTransactionType.ReferenceFieldName = Nothing
        Me.cboTransactionType.ReferenceTableName = Nothing
        Me.cboTransactionType.Size = New System.Drawing.Size(210, 18)
        Me.cboTransactionType.TabIndex = 4
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(0, 93)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(94, 16)
        Me.MyLabel3.TabIndex = 22
        Me.MyLabel3.Text = "Transaction Type"
        '
        'cboLoanType
        '
        Me.cboLoanType.AutoCompleteDisplayMember = Nothing
        Me.cboLoanType.AutoCompleteValueMember = Nothing
        Me.cboLoanType.CalculationExpression = Nothing
        Me.cboLoanType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboLoanType.FieldCode = Nothing
        Me.cboLoanType.FieldDesc = Nothing
        Me.cboLoanType.FieldMaxLength = 0
        Me.cboLoanType.FieldName = Nothing
        Me.cboLoanType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboLoanType.isCalculatedField = False
        Me.cboLoanType.IsSourceFromTable = False
        Me.cboLoanType.IsSourceFromValueList = False
        Me.cboLoanType.IsUnique = False
        RadListDataItem3.Text = "M"
        RadListDataItem4.Text = "E"
        Me.cboLoanType.Items.Add(RadListDataItem3)
        Me.cboLoanType.Items.Add(RadListDataItem4)
        Me.cboLoanType.Location = New System.Drawing.Point(110, 71)
        Me.cboLoanType.MendatroryField = True
        Me.cboLoanType.MyLinkLable1 = Me.lblBOMStatus
        Me.cboLoanType.MyLinkLable2 = Nothing
        Me.cboLoanType.Name = "cboLoanType"
        Me.cboLoanType.ReferenceFieldDesc = Nothing
        Me.cboLoanType.ReferenceFieldName = Nothing
        Me.cboLoanType.ReferenceTableName = Nothing
        Me.cboLoanType.Size = New System.Drawing.Size(210, 18)
        Me.cboLoanType.TabIndex = 3
        '
        'lblBOMStatus
        '
        Me.lblBOMStatus.FieldName = Nothing
        Me.lblBOMStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBOMStatus.Location = New System.Drawing.Point(0, 72)
        Me.lblBOMStatus.Name = "lblBOMStatus"
        Me.lblBOMStatus.Size = New System.Drawing.Size(60, 16)
        Me.lblBOMStatus.TabIndex = 23
        Me.lblBOMStatus.Text = "Loan Type"
        '
        'txtName
        '
        Me.txtName.CalculationExpression = Nothing
        Me.txtName.FieldCode = Nothing
        Me.txtName.FieldDesc = Nothing
        Me.txtName.FieldMaxLength = 0
        Me.txtName.FieldName = Nothing
        Me.txtName.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtName.isCalculatedField = False
        Me.txtName.IsSourceFromTable = False
        Me.txtName.IsSourceFromValueList = False
        Me.txtName.IsUnique = False
        Me.txtName.Location = New System.Drawing.Point(110, 48)
        Me.txtName.MaxLength = 100
        Me.txtName.MendatroryField = True
        Me.txtName.MyLinkLable1 = Nothing
        Me.txtName.MyLinkLable2 = Nothing
        Me.txtName.Name = "txtName"
        Me.txtName.ReferenceFieldDesc = Nothing
        Me.txtName.ReferenceFieldName = Nothing
        Me.txtName.ReferenceTableName = Nothing
        Me.txtName.Size = New System.Drawing.Size(580, 20)
        Me.txtName.TabIndex = 2
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(0, 50)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(36, 16)
        Me.MyLabel2.TabIndex = 24
        Me.MyLabel2.Text = "Name"
        '
        'btnnew
        '
        Me.btnnew.Image = Global.XpertERPCommanServices.My.Resources.Resources._new
        Me.btnnew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnnew.Location = New System.Drawing.Point(379, 1)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(20, 21)
        Me.btnnew.TabIndex = 28
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(0, 27)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel4.TabIndex = 25
        Me.MyLabel4.Text = "Description"
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(110, 1)
        Me.txtCode.MendatroryField = False
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 30
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(269, 21)
        Me.txtCode.TabIndex = 27
        Me.txtCode.Value = ""
        '
        'txtRemarks
        '
        Me.txtRemarks.CalculationExpression = Nothing
        Me.txtRemarks.FieldCode = Nothing
        Me.txtRemarks.FieldDesc = Nothing
        Me.txtRemarks.FieldMaxLength = 0
        Me.txtRemarks.FieldName = Nothing
        Me.txtRemarks.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtRemarks.isCalculatedField = False
        Me.txtRemarks.IsSourceFromTable = False
        Me.txtRemarks.IsSourceFromValueList = False
        Me.txtRemarks.IsUnique = False
        Me.txtRemarks.Location = New System.Drawing.Point(110, 158)
        Me.txtRemarks.MaxLength = 100
        Me.txtRemarks.MendatroryField = False
        Me.txtRemarks.MyLinkLable1 = Nothing
        Me.txtRemarks.MyLinkLable2 = Nothing
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.ReferenceFieldDesc = Nothing
        Me.txtRemarks.ReferenceFieldName = Nothing
        Me.txtRemarks.ReferenceTableName = Nothing
        Me.txtRemarks.Size = New System.Drawing.Size(580, 20)
        Me.txtRemarks.TabIndex = 11
        '
        'txtAccount
        '
        Me.txtAccount.CalculationExpression = Nothing
        Me.txtAccount.FieldCode = Nothing
        Me.txtAccount.FieldDesc = Nothing
        Me.txtAccount.FieldMaxLength = 0
        Me.txtAccount.FieldName = Nothing
        Me.txtAccount.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtAccount.isCalculatedField = False
        Me.txtAccount.IsSourceFromTable = False
        Me.txtAccount.IsSourceFromValueList = False
        Me.txtAccount.IsUnique = False
        Me.txtAccount.Location = New System.Drawing.Point(400, 91)
        Me.txtAccount.MendatroryField = True
        Me.txtAccount.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAccount.MyLinkLable1 = Me.lblAccount
        Me.txtAccount.MyLinkLable2 = Me.lblAccountName
        Me.txtAccount.MyReadOnly = False
        Me.txtAccount.MyShowMasterFormButton = False
        Me.txtAccount.Name = "txtAccount"
        Me.txtAccount.ReferenceFieldDesc = Nothing
        Me.txtAccount.ReferenceFieldName = Nothing
        Me.txtAccount.ReferenceTableName = Nothing
        Me.txtAccount.Size = New System.Drawing.Size(115, 20)
        Me.txtAccount.TabIndex = 5
        Me.txtAccount.Value = ""
        '
        'lblAccount
        '
        Me.lblAccount.FieldName = Nothing
        Me.lblAccount.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblAccount.Location = New System.Drawing.Point(347, 92)
        Me.lblAccount.Name = "lblAccount"
        Me.lblAccount.Size = New System.Drawing.Size(47, 18)
        Me.lblAccount.TabIndex = 14
        Me.lblAccount.Text = "Account"
        '
        'lblAccountName
        '
        Me.lblAccountName.AutoSize = False
        Me.lblAccountName.BorderVisible = True
        Me.lblAccountName.FieldName = Nothing
        Me.lblAccountName.Location = New System.Drawing.Point(517, 91)
        Me.lblAccountName.Name = "lblAccountName"
        Me.lblAccountName.Size = New System.Drawing.Size(172, 21)
        Me.lblAccountName.TabIndex = 17
        Me.lblAccountName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDescription
        '
        Me.txtDescription.CalculationExpression = Nothing
        Me.txtDescription.FieldCode = Nothing
        Me.txtDescription.FieldDesc = Nothing
        Me.txtDescription.FieldMaxLength = 0
        Me.txtDescription.FieldName = Nothing
        Me.txtDescription.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDescription.isCalculatedField = False
        Me.txtDescription.IsSourceFromTable = False
        Me.txtDescription.IsSourceFromValueList = False
        Me.txtDescription.IsUnique = False
        Me.txtDescription.Location = New System.Drawing.Point(110, 25)
        Me.txtDescription.MaxLength = 100
        Me.txtDescription.MendatroryField = False
        Me.txtDescription.MyLinkLable1 = Nothing
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReferenceFieldDesc = Nothing
        Me.txtDescription.ReferenceFieldName = Nothing
        Me.txtDescription.ReferenceTableName = Nothing
        Me.txtDescription.Size = New System.Drawing.Size(580, 20)
        Me.txtDescription.TabIndex = 1
        '
        'txtInstallmentDate
        '
        Me.txtInstallmentDate.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtInstallmentDate.CalculationExpression = Nothing
        Me.txtInstallmentDate.DecimalPlaces = 0
        Me.txtInstallmentDate.FieldCode = Nothing
        Me.txtInstallmentDate.FieldDesc = Nothing
        Me.txtInstallmentDate.FieldMaxLength = 0
        Me.txtInstallmentDate.FieldName = Nothing
        Me.txtInstallmentDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInstallmentDate.isCalculatedField = False
        Me.txtInstallmentDate.IsSourceFromTable = False
        Me.txtInstallmentDate.IsSourceFromValueList = False
        Me.txtInstallmentDate.IsUnique = False
        Me.txtInstallmentDate.Location = New System.Drawing.Point(613, 135)
        Me.txtInstallmentDate.MaxLength = 3
        Me.txtInstallmentDate.MendatroryField = True
        Me.txtInstallmentDate.MyLinkLable1 = Me.MyLabel9
        Me.txtInstallmentDate.MyLinkLable2 = Nothing
        Me.txtInstallmentDate.Name = "txtInstallmentDate"
        Me.txtInstallmentDate.ReferenceFieldDesc = Nothing
        Me.txtInstallmentDate.ReferenceFieldName = Nothing
        Me.txtInstallmentDate.ReferenceTableName = Nothing
        Me.txtInstallmentDate.Size = New System.Drawing.Size(76, 20)
        Me.txtInstallmentDate.TabIndex = 10
        Me.txtInstallmentDate.Text = "0"
        Me.txtInstallmentDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtInstallmentDate.Value = 0.0R
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel9.Location = New System.Drawing.Point(517, 137)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(94, 16)
        Me.MyLabel9.TabIndex = 15
        Me.MyLabel9.Text = "Insatallment Date"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(590, 1)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(100, 21)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 30
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(322, 137)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(77, 16)
        Me.MyLabel8.TabIndex = 12
        Me.MyLabel8.Text = "Loan Start On"
        '
        'lblInsatallmentAmount
        '
        Me.lblInsatallmentAmount.AutoSize = False
        Me.lblInsatallmentAmount.BorderVisible = True
        Me.lblInsatallmentAmount.FieldName = Nothing
        Me.lblInsatallmentAmount.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblInsatallmentAmount.Location = New System.Drawing.Point(110, 135)
        Me.lblInsatallmentAmount.Name = "lblInsatallmentAmount"
        Me.lblInsatallmentAmount.Size = New System.Drawing.Size(210, 21)
        Me.lblInsatallmentAmount.TabIndex = 18
        Me.lblInsatallmentAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtLoanGivenOn
        '
        Me.txtLoanGivenOn.CalculationExpression = Nothing
        Me.txtLoanGivenOn.CustomFormat = "dd/MM/yyyy"
        Me.txtLoanGivenOn.FieldCode = Nothing
        Me.txtLoanGivenOn.FieldDesc = Nothing
        Me.txtLoanGivenOn.FieldMaxLength = 0
        Me.txtLoanGivenOn.FieldName = Nothing
        Me.txtLoanGivenOn.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtLoanGivenOn.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtLoanGivenOn.isCalculatedField = False
        Me.txtLoanGivenOn.IsSourceFromTable = False
        Me.txtLoanGivenOn.IsSourceFromValueList = False
        Me.txtLoanGivenOn.IsUnique = False
        Me.txtLoanGivenOn.Location = New System.Drawing.Point(400, 136)
        Me.txtLoanGivenOn.MendatroryField = True
        Me.txtLoanGivenOn.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtLoanGivenOn.MyLinkLable1 = Me.MyLabel8
        Me.txtLoanGivenOn.MyLinkLable2 = Nothing
        Me.txtLoanGivenOn.Name = "txtLoanGivenOn"
        Me.txtLoanGivenOn.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtLoanGivenOn.ReferenceFieldDesc = Nothing
        Me.txtLoanGivenOn.ReferenceFieldName = Nothing
        Me.txtLoanGivenOn.ReferenceTableName = Nothing
        Me.txtLoanGivenOn.Size = New System.Drawing.Size(111, 20)
        Me.txtLoanGivenOn.TabIndex = 9
        Me.txtLoanGivenOn.TabStop = False
        Me.txtLoanGivenOn.Text = "03/05/2011"
        Me.txtLoanGivenOn.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'Txtdds
        '
        Me.Txtdds.FieldName = Nothing
        Me.Txtdds.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Txtdds.Location = New System.Drawing.Point(0, 136)
        Me.Txtdds.Name = "Txtdds"
        Me.Txtdds.Size = New System.Drawing.Size(111, 18)
        Me.Txtdds.TabIndex = 20
        Me.Txtdds.Text = "Insatallment Amount"
        '
        'txtTenure
        '
        Me.txtTenure.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtTenure.CalculationExpression = Nothing
        Me.txtTenure.DecimalPlaces = 0
        Me.txtTenure.FieldCode = Nothing
        Me.txtTenure.FieldDesc = Nothing
        Me.txtTenure.FieldMaxLength = 0
        Me.txtTenure.FieldName = Nothing
        Me.txtTenure.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTenure.isCalculatedField = False
        Me.txtTenure.IsSourceFromTable = False
        Me.txtTenure.IsSourceFromValueList = False
        Me.txtTenure.IsUnique = False
        Me.txtTenure.Location = New System.Drawing.Point(613, 113)
        Me.txtTenure.MaxLength = 3
        Me.txtTenure.MendatroryField = True
        Me.txtTenure.MyLinkLable1 = Me.MyLabel7
        Me.txtTenure.MyLinkLable2 = Nothing
        Me.txtTenure.Name = "txtTenure"
        Me.txtTenure.ReferenceFieldDesc = Nothing
        Me.txtTenure.ReferenceFieldName = Nothing
        Me.txtTenure.ReferenceTableName = Nothing
        Me.txtTenure.Size = New System.Drawing.Size(76, 20)
        Me.txtTenure.TabIndex = 8
        Me.txtTenure.Text = "0"
        Me.txtTenure.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTenure.Value = 0.0R
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel7.Location = New System.Drawing.Point(517, 115)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(90, 16)
        Me.MyLabel7.TabIndex = 16
        Me.MyLabel7.Text = "Tenure (Months)"
        '
        'lblNoOfCans
        '
        Me.lblNoOfCans.FieldName = Nothing
        Me.lblNoOfCans.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblNoOfCans.Location = New System.Drawing.Point(0, 115)
        Me.lblNoOfCans.Name = "lblNoOfCans"
        Me.lblNoOfCans.Size = New System.Drawing.Size(74, 16)
        Me.lblNoOfCans.TabIndex = 21
        Me.lblNoOfCans.Text = "Loan Amount"
        '
        'txtLoanAmount
        '
        Me.txtLoanAmount.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtLoanAmount.CalculationExpression = Nothing
        Me.txtLoanAmount.DecimalPlaces = 0
        Me.txtLoanAmount.FieldCode = Nothing
        Me.txtLoanAmount.FieldDesc = Nothing
        Me.txtLoanAmount.FieldMaxLength = 0
        Me.txtLoanAmount.FieldName = Nothing
        Me.txtLoanAmount.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLoanAmount.isCalculatedField = False
        Me.txtLoanAmount.IsSourceFromTable = False
        Me.txtLoanAmount.IsSourceFromValueList = False
        Me.txtLoanAmount.IsUnique = False
        Me.txtLoanAmount.Location = New System.Drawing.Point(110, 113)
        Me.txtLoanAmount.MaxLength = 10
        Me.txtLoanAmount.MendatroryField = True
        Me.txtLoanAmount.MyLinkLable1 = Me.lblNoOfCans
        Me.txtLoanAmount.MyLinkLable2 = Nothing
        Me.txtLoanAmount.Name = "txtLoanAmount"
        Me.txtLoanAmount.ReferenceFieldDesc = Nothing
        Me.txtLoanAmount.ReferenceFieldName = Nothing
        Me.txtLoanAmount.ReferenceTableName = Nothing
        Me.txtLoanAmount.Size = New System.Drawing.Size(210, 20)
        Me.txtLoanAmount.TabIndex = 6
        Me.txtLoanAmount.Text = "0"
        Me.txtLoanAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtLoanAmount.Value = 0.0R
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(323, 115)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(71, 16)
        Me.MyLabel1.TabIndex = 13
        Me.MyLabel1.Text = "Interest Rate"
        '
        'txtInterestRate
        '
        Me.txtInterestRate.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtInterestRate.CalculationExpression = Nothing
        Me.txtInterestRate.DecimalPlaces = 2
        Me.txtInterestRate.FieldCode = Nothing
        Me.txtInterestRate.FieldDesc = Nothing
        Me.txtInterestRate.FieldMaxLength = 0
        Me.txtInterestRate.FieldName = Nothing
        Me.txtInterestRate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInterestRate.isCalculatedField = False
        Me.txtInterestRate.IsSourceFromTable = False
        Me.txtInterestRate.IsSourceFromValueList = False
        Me.txtInterestRate.IsUnique = False
        Me.txtInterestRate.Location = New System.Drawing.Point(400, 113)
        Me.txtInterestRate.MaxLength = 5
        Me.txtInterestRate.MendatroryField = True
        Me.txtInterestRate.MyLinkLable1 = Me.MyLabel1
        Me.txtInterestRate.MyLinkLable2 = Nothing
        Me.txtInterestRate.Name = "txtInterestRate"
        Me.txtInterestRate.ReferenceFieldDesc = Nothing
        Me.txtInterestRate.ReferenceFieldName = Nothing
        Me.txtInterestRate.ReferenceTableName = Nothing
        Me.txtInterestRate.Size = New System.Drawing.Size(111, 20)
        Me.txtInterestRate.TabIndex = 7
        Me.txtInterestRate.Text = "0"
        Me.txtInterestRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtInterestRate.Value = 0.0R
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv)
        Me.RadPageViewPage2.Controls.Add(Me.Panel1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(118.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(699, 219)
        Me.RadPageViewPage2.Text = "Amortization Details"
        '
        'gv
        '
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Location = New System.Drawing.Point(0, 33)
        Me.gv.Name = "gv"
        Me.gv.Size = New System.Drawing.Size(699, 186)
        Me.gv.TabIndex = 0
        Me.gv.Text = "RadGridView1"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.RadButton1)
        Me.Panel1.Controls.Add(Me.RadButton2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(699, 33)
        Me.Panel1.TabIndex = 1
        '
        'RadButton1
        '
        Me.RadButton1.Location = New System.Drawing.Point(595, 4)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(101, 24)
        Me.RadButton1.TabIndex = 40
        Me.RadButton1.Text = "Export To Excel"
        '
        'RadButton2
        '
        Me.RadButton2.Location = New System.Drawing.Point(5, 4)
        Me.RadButton2.Name = "RadButton2"
        Me.RadButton2.Size = New System.Drawing.Size(194, 24)
        Me.RadButton2.TabIndex = 39
        Me.RadButton2.Text = "Amortization Vs Acutal Details"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(212, 7)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(66, 21)
        Me.btnPrint.TabIndex = 4
        Me.btnPrint.Text = "Print"
        Me.btnPrint.Visible = False
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(649, 7)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 21)
        Me.btnclose.TabIndex = 3
        Me.btnclose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(5, 7)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(66, 21)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        Me.btnSave.Visible = False
        '
        'BtnPost
        '
        Me.BtnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPost.Location = New System.Drawing.Point(143, 7)
        Me.BtnPost.Name = "BtnPost"
        Me.BtnPost.Size = New System.Drawing.Size(66, 21)
        Me.BtnPost.TabIndex = 2
        Me.BtnPost.Text = "Post"
        Me.BtnPost.Visible = False
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(74, 7)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 21)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'frmLoanEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(720, 306)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmLoanEntry"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Loan Entry"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboTransactionType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboLoanType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBOMStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAccountName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtInstallmentDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblInsatallmentAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLoanGivenOn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Txtdds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTenure, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNoOfCans, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLoanAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtInterestRate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents BtnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblBOMStatus As common.Controls.MyLabel
    Friend WithEvents cboLoanType As common.Controls.MyComboBox
    Friend WithEvents lblDocDate As common.Controls.MyLabel
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents lblAccountName As common.Controls.MyLabel
    Friend WithEvents lblAccount As common.Controls.MyLabel
    Friend WithEvents txtAccount As common.UserControls.txtFinder
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents Txtdds As common.Controls.MyLabel
    Friend WithEvents lblInsatallmentAmount As common.Controls.MyLabel
    Friend WithEvents txtInterestRate As common.MyNumBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtLoanAmount As common.MyNumBox
    Friend WithEvents lblNoOfCans As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtName As common.Controls.MyTextBox
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtRemarks As common.Controls.MyTextBox
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents txtInstallmentDate As common.MyNumBox
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents txtLoanGivenOn As common.Controls.MyDateTimePicker
    Friend WithEvents txtTenure As common.MyNumBox
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents cboTransactionType As common.Controls.MyComboBox
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents RadButton2 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
End Class
