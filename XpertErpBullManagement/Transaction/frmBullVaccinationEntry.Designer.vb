Imports Telerik.WinControls.UI
Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmBullVaccinationEntry
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
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmimport = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.lblRegToDate = New common.Controls.MyLabel()
        Me.lblRegFromDate = New common.Controls.MyLabel()
        Me.lblDOB = New common.Controls.MyLabel()
        Me.lblSSCentre = New common.Controls.MyLabel()
        Me.lblBreed = New common.Controls.MyLabel()
        Me.lblSSBullId = New common.Controls.MyLabel()
        Me.lblPreBullId = New common.Controls.MyLabel()
        Me.RadLabel14 = New common.Controls.MyLabel()
        Me.txtRemarks = New common.Controls.MyTextBox()
        Me.lblStatus = New common.usLock()
        Me.lblBullAliasName = New common.Controls.MyLabel()
        Me.txtBullCode = New common.UserControls.txtFinder()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtdate = New common.Controls.MyDateTimePicker()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.lblCode = New common.Controls.MyLabel()
        Me.txtDocumentNo = New common.UserControls.txtNavigator()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.RadSplitButton1 = New Telerik.WinControls.UI.RadSplitButton()
        Me.btnExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnReverseUnpost = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.lblRegToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRegFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDOB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSSCentre, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBreed, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSSBullId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPreBullId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBullAliasName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReverseUnpost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmExport, Me.rmimport, Me.btnSaveLayout, Me.btnDeleteLayout})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        '
        'rmExport
        '
        Me.rmExport.Name = "rmExport"
        Me.rmExport.Text = "Export Blank Grid"
        '
        'rmimport
        '
        Me.rmimport.Name = "rmimport"
        Me.rmimport.Text = "Import Grid"
        '
        'btnSaveLayout
        '
        Me.btnSaveLayout.Name = "btnSaveLayout"
        Me.btnSaveLayout.Text = "Save Layout"
        '
        'btnDeleteLayout
        '
        Me.btnDeleteLayout.Name = "btnDeleteLayout"
        Me.btnDeleteLayout.Text = "Delete Layout"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(996, 20)
        Me.RadMenu1.TabIndex = 12
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadSplitButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReverseUnpost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(996, 489)
        Me.SplitContainer1.SplitterDistance = 444
        Me.SplitContainer1.TabIndex = 13
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblRegToDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblRegFromDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblDOB)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblSSCentre)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblBreed)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblSSBullId)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblPreBullId)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel14)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtRemarks)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblStatus)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblBullAliasName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtBullCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel6)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtdate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnAddNew)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDocumentNo)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gv1)
        Me.SplitContainer2.Size = New System.Drawing.Size(996, 444)
        Me.SplitContainer2.SplitterDistance = 141
        Me.SplitContainer2.TabIndex = 0
        '
        'lblRegToDate
        '
        Me.lblRegToDate.AutoSize = False
        Me.lblRegToDate.BorderVisible = True
        Me.lblRegToDate.FieldName = Nothing
        Me.lblRegToDate.Location = New System.Drawing.Point(509, 84)
        Me.lblRegToDate.Name = "lblRegToDate"
        Me.lblRegToDate.Size = New System.Drawing.Size(188, 19)
        Me.lblRegToDate.TabIndex = 43
        '
        'lblRegFromDate
        '
        Me.lblRegFromDate.AutoSize = False
        Me.lblRegFromDate.BorderVisible = True
        Me.lblRegFromDate.FieldName = Nothing
        Me.lblRegFromDate.Location = New System.Drawing.Point(316, 84)
        Me.lblRegFromDate.Name = "lblRegFromDate"
        Me.lblRegFromDate.Size = New System.Drawing.Size(188, 19)
        Me.lblRegFromDate.TabIndex = 42
        '
        'lblDOB
        '
        Me.lblDOB.AutoSize = False
        Me.lblDOB.BorderVisible = True
        Me.lblDOB.FieldName = Nothing
        Me.lblDOB.Location = New System.Drawing.Point(99, 84)
        Me.lblDOB.Name = "lblDOB"
        Me.lblDOB.Size = New System.Drawing.Size(209, 19)
        Me.lblDOB.TabIndex = 41
        '
        'lblSSCentre
        '
        Me.lblSSCentre.AutoSize = False
        Me.lblSSCentre.BorderVisible = True
        Me.lblSSCentre.FieldName = Nothing
        Me.lblSSCentre.Location = New System.Drawing.Point(509, 61)
        Me.lblSSCentre.Name = "lblSSCentre"
        Me.lblSSCentre.Size = New System.Drawing.Size(188, 19)
        Me.lblSSCentre.TabIndex = 39
        '
        'lblBreed
        '
        Me.lblBreed.AutoSize = False
        Me.lblBreed.BorderVisible = True
        Me.lblBreed.FieldName = Nothing
        Me.lblBreed.Location = New System.Drawing.Point(316, 61)
        Me.lblBreed.Name = "lblBreed"
        Me.lblBreed.Size = New System.Drawing.Size(188, 19)
        Me.lblBreed.TabIndex = 38
        '
        'lblSSBullId
        '
        Me.lblSSBullId.AutoSize = False
        Me.lblSSBullId.BorderVisible = True
        Me.lblSSBullId.FieldName = Nothing
        Me.lblSSBullId.Location = New System.Drawing.Point(99, 61)
        Me.lblSSBullId.Name = "lblSSBullId"
        Me.lblSSBullId.Size = New System.Drawing.Size(209, 19)
        Me.lblSSBullId.TabIndex = 37
        '
        'lblPreBullId
        '
        Me.lblPreBullId.AutoSize = False
        Me.lblPreBullId.BorderVisible = True
        Me.lblPreBullId.FieldName = Nothing
        Me.lblPreBullId.Location = New System.Drawing.Point(509, 38)
        Me.lblPreBullId.Name = "lblPreBullId"
        Me.lblPreBullId.Size = New System.Drawing.Size(188, 19)
        Me.lblPreBullId.TabIndex = 35
        '
        'RadLabel14
        '
        Me.RadLabel14.FieldName = Nothing
        Me.RadLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel14.Location = New System.Drawing.Point(12, 109)
        Me.RadLabel14.Name = "RadLabel14"
        Me.RadLabel14.Size = New System.Drawing.Size(51, 16)
        Me.RadLabel14.TabIndex = 34
        Me.RadLabel14.Text = "Remarks"
        '
        'txtRemarks
        '
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
        Me.txtRemarks.Location = New System.Drawing.Point(97, 108)
        Me.txtRemarks.MaxLength = 200
        Me.txtRemarks.MendatroryField = False
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.MyLinkLable1 = Me.RadLabel14
        Me.txtRemarks.MyLinkLable2 = Nothing
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.ReferenceFieldDesc = Nothing
        Me.txtRemarks.ReferenceFieldName = Nothing
        Me.txtRemarks.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtRemarks.RootElement.StretchVertically = True
        Me.txtRemarks.Size = New System.Drawing.Size(430, 25)
        Me.txtRemarks.TabIndex = 33
        '
        'lblStatus
        '
        Me.lblStatus.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lblStatus.Location = New System.Drawing.Point(534, 17)
        Me.lblStatus.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(147, 19)
        Me.lblStatus.Status = common.ERPTransactionStatus.Pending
        Me.lblStatus.TabIndex = 26
        '
        'lblBullAliasName
        '
        Me.lblBullAliasName.AutoSize = False
        Me.lblBullAliasName.BorderVisible = True
        Me.lblBullAliasName.FieldName = Nothing
        Me.lblBullAliasName.Location = New System.Drawing.Point(316, 38)
        Me.lblBullAliasName.Name = "lblBullAliasName"
        Me.lblBullAliasName.Size = New System.Drawing.Size(188, 19)
        Me.lblBullAliasName.TabIndex = 24
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
        Me.txtBullCode.Location = New System.Drawing.Point(99, 38)
        Me.txtBullCode.MendatroryField = True
        Me.txtBullCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBullCode.MyLinkLable1 = Nothing
        Me.txtBullCode.MyLinkLable2 = Nothing
        Me.txtBullCode.MyReadOnly = False
        Me.txtBullCode.MyShowMasterFormButton = False
        Me.txtBullCode.Name = "txtBullCode"
        Me.txtBullCode.ReferenceFieldDesc = Nothing
        Me.txtBullCode.ReferenceFieldName = Nothing
        Me.txtBullCode.ReferenceTableName = Nothing
        Me.txtBullCode.Size = New System.Drawing.Size(212, 19)
        Me.txtBullCode.TabIndex = 19
        Me.txtBullCode.Value = ""
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel6.Location = New System.Drawing.Point(12, 39)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(55, 16)
        Me.MyLabel6.TabIndex = 20
        Me.MyLabel6.Text = "Bull Code"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel2.Location = New System.Drawing.Point(378, 17)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(30, 16)
        Me.MyLabel2.TabIndex = 23
        Me.MyLabel2.Text = "Date"
        '
        'txtdate
        '
        Me.txtdate.CalculationExpression = Nothing
        Me.txtdate.CustomFormat = "dd/MM/yyyy"
        Me.txtdate.FieldCode = Nothing
        Me.txtdate.FieldDesc = Nothing
        Me.txtdate.FieldMaxLength = 0
        Me.txtdate.FieldName = Nothing
        Me.txtdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtdate.isCalculatedField = False
        Me.txtdate.IsSourceFromTable = False
        Me.txtdate.IsSourceFromValueList = False
        Me.txtdate.IsUnique = False
        Me.txtdate.Location = New System.Drawing.Point(414, 16)
        Me.txtdate.MendatroryField = True
        Me.txtdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtdate.MyLinkLable1 = Me.MyLabel2
        Me.txtdate.MyLinkLable2 = Nothing
        Me.txtdate.Name = "txtdate"
        Me.txtdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtdate.ReferenceFieldDesc = Nothing
        Me.txtdate.ReferenceFieldName = Nothing
        Me.txtdate.ReferenceTableName = Nothing
        Me.txtdate.Size = New System.Drawing.Size(90, 18)
        Me.txtdate.TabIndex = 18
        Me.txtdate.TabStop = False
        Me.txtdate.Text = "13/06/2011"
        Me.txtdate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(355, 16)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(21, 19)
        Me.btnAddNew.TabIndex = 22
        '
        'lblCode
        '
        Me.lblCode.FieldName = Nothing
        Me.lblCode.Location = New System.Drawing.Point(12, 16)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(77, 18)
        Me.lblCode.TabIndex = 21
        Me.lblCode.Text = "Document No"
        '
        'txtDocumentNo
        '
        Me.txtDocumentNo.FieldName = Nothing
        Me.txtDocumentNo.Location = New System.Drawing.Point(99, 15)
        Me.txtDocumentNo.MendatroryField = False
        Me.txtDocumentNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocumentNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocumentNo.MyLinkLable1 = Me.lblCode
        Me.txtDocumentNo.MyLinkLable2 = Nothing
        Me.txtDocumentNo.MyMaxLength = 32767
        Me.txtDocumentNo.MyReadOnly = False
        Me.txtDocumentNo.Name = "txtDocumentNo"
        Me.txtDocumentNo.Size = New System.Drawing.Size(256, 20)
        Me.txtDocumentNo.TabIndex = 25
        Me.txtDocumentNo.Value = ""
        '
        'gv1
        '
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(996, 299)
        Me.gv1.TabIndex = 1
        Me.gv1.TabStop = False
        '
        'RadSplitButton1
        '
        Me.RadSplitButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadSplitButton1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnExcel, Me.btnPDF})
        Me.RadSplitButton1.Location = New System.Drawing.Point(220, 12)
        Me.RadSplitButton1.Name = "RadSplitButton1"
        Me.RadSplitButton1.Size = New System.Drawing.Size(95, 22)
        Me.RadSplitButton1.TabIndex = 161
        Me.RadSplitButton1.Text = "Export"
        '
        'btnExcel
        '
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Text = "Excel"
        Me.btnExcel.UseCompatibleTextRendering = False
        '
        'btnPDF
        '
        Me.btnPDF.Name = "btnPDF"
        Me.btnPDF.Text = "PDF"
        Me.btnPDF.UseCompatibleTextRendering = False
        '
        'btnReverseUnpost
        '
        Me.btnReverseUnpost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReverseUnpost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverseUnpost.Location = New System.Drawing.Point(322, 12)
        Me.btnReverseUnpost.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnReverseUnpost.Name = "btnReverseUnpost"
        Me.btnReverseUnpost.Size = New System.Drawing.Size(122, 20)
        Me.btnReverseUnpost.TabIndex = 160
        Me.btnReverseUnpost.Text = "Reverse and Unpost"
        Me.btnReverseUnpost.Visible = False
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(147, 12)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(69, 20)
        Me.btnPost.TabIndex = 159
        Me.btnPost.Text = "Post"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(922, 12)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(69, 20)
        Me.btnclose.TabIndex = 158
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(76, 12)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(69, 20)
        Me.btndelete.TabIndex = 157
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(5, 12)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(69, 20)
        Me.btnsave.TabIndex = 156
        Me.btnsave.Text = "Save"
        '
        'frmBullVaccinationEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(996, 509)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmBullVaccinationEntry"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Bulk Sale Freight Master"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.lblRegToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRegFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDOB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSSCentre, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBreed, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSSBullId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPreBullId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBullAliasName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReverseUnpost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmimport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnSaveLayout As RadMenuItem
    Friend WithEvents btnDeleteLayout As RadMenuItem
    Friend WithEvents RadMenu1 As RadMenu
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents lblStatus As common.usLock
    Friend WithEvents lblBullAliasName As common.Controls.MyLabel
    Friend WithEvents txtBullCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtdate As common.Controls.MyDateTimePicker
    Friend WithEvents btnAddNew As RadButton
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents txtDocumentNo As common.UserControls.txtNavigator
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents RadSplitButton1 As RadSplitButton
    Friend WithEvents btnExcel As RadMenuItem
    Friend WithEvents btnPDF As RadMenuItem
    Friend WithEvents btnReverseUnpost As RadButton
    Friend WithEvents btnPost As RadButton
    Friend WithEvents btnclose As RadButton
    Friend WithEvents btndelete As RadButton
    Friend WithEvents btnsave As RadButton
    Friend WithEvents RadLabel14 As common.Controls.MyLabel
    Friend WithEvents txtRemarks As common.Controls.MyTextBox
    Friend WithEvents lblSSCentre As common.Controls.MyLabel
    Friend WithEvents lblBreed As common.Controls.MyLabel
    Friend WithEvents lblSSBullId As common.Controls.MyLabel
    Friend WithEvents lblPreBullId As common.Controls.MyLabel
    Friend WithEvents lblRegToDate As common.Controls.MyLabel
    Friend WithEvents lblRegFromDate As common.Controls.MyLabel
    Friend WithEvents lblDOB As common.Controls.MyLabel
End Class
