<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmFormIssueDetails
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
        Dim GridViewTextBoxColumn1 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn2 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewDateTimeColumn1 As Telerik.WinControls.UI.GridViewDateTimeColumn = New Telerik.WinControls.UI.GridViewDateTimeColumn
        Dim GridViewDateTimeColumn2 As Telerik.WinControls.UI.GridViewDateTimeColumn = New Telerik.WinControls.UI.GridViewDateTimeColumn
        Dim GridViewTextBoxColumn3 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.MyLabel3 = New common.Controls.MyLabel
        Me.txtDemandNo = New common.Controls.MyTextBox
        Me.lblchkNo = New common.Controls.MyLabel
        Me.btnGo = New Telerik.WinControls.UI.RadButton
        Me.txtFormIssued = New common.Controls.MyTextBox
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.txtToNo = New common.Controls.MyTextBox
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.fndFormIssueNo = New common.UserControls.txtNavigator
        Me.lblpaymentno = New common.Controls.MyLabel
        Me.btnNew = New Telerik.WinControls.UI.RadButton
        Me.ddlFromType = New common.UserControls.txtFinder
        Me.lblcustomer = New common.Controls.MyLabel
        Me.DgvFormIssue = New common.UserControls.MyRadGridView
        Me.txtFromNo = New common.Controls.MyTextBox
        Me.lblUnApplyAmt = New common.Controls.MyLabel
        Me.txtFormSeries = New common.Controls.MyTextBox
        Me.txtCommetns = New common.Controls.MyTextBox
        Me.lblnarration = New common.Controls.MyLabel
        Me.txtRemarks = New common.Controls.MyTextBox
        Me.lblreference = New common.Controls.MyLabel
        Me.txtFormTypeDesc = New common.Controls.MyTextBox
        Me.dtpDemandDate = New common.Controls.MyDateTimePicker
        Me.lblpaymentpostdate = New common.Controls.MyLabel
        Me.btnDelete = New Telerik.WinControls.UI.RadButton
        Me.btnPost = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnSave = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDemandNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblchkNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFormIssued, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblpaymentno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblcustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvFormIssue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvFormIssue.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblUnApplyAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFormSeries, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCommetns, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblnarration, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblreference, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFormTypeDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDemandDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblpaymentpostdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox1.Controls.Add(Me.txtDemandNo)
        Me.RadGroupBox1.Controls.Add(Me.btnGo)
        Me.RadGroupBox1.Controls.Add(Me.txtFormIssued)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.txtToNo)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.fndFormIssueNo)
        Me.RadGroupBox1.Controls.Add(Me.lblpaymentno)
        Me.RadGroupBox1.Controls.Add(Me.btnNew)
        Me.RadGroupBox1.Controls.Add(Me.ddlFromType)
        Me.RadGroupBox1.Controls.Add(Me.DgvFormIssue)
        Me.RadGroupBox1.Controls.Add(Me.txtFromNo)
        Me.RadGroupBox1.Controls.Add(Me.lblUnApplyAmt)
        Me.RadGroupBox1.Controls.Add(Me.txtFormSeries)
        Me.RadGroupBox1.Controls.Add(Me.txtCommetns)
        Me.RadGroupBox1.Controls.Add(Me.lblnarration)
        Me.RadGroupBox1.Controls.Add(Me.lblchkNo)
        Me.RadGroupBox1.Controls.Add(Me.txtRemarks)
        Me.RadGroupBox1.Controls.Add(Me.lblreference)
        Me.RadGroupBox1.Controls.Add(Me.txtFormTypeDesc)
        Me.RadGroupBox1.Controls.Add(Me.lblcustomer)
        Me.RadGroupBox1.Controls.Add(Me.dtpDemandDate)
        Me.RadGroupBox1.Controls.Add(Me.lblpaymentpostdate)
        Me.RadGroupBox1.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(768, 463)
        Me.RadGroupBox1.TabIndex = 0
        '
        'MyLabel3
        '
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(13, 12)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(80, 16)
        Me.MyLabel3.TabIndex = 18
        Me.MyLabel3.Text = "Form Issue No"
        '
        'txtDemandNo
        '
        Me.txtDemandNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDemandNo.Location = New System.Drawing.Point(114, 34)
        Me.txtDemandNo.MaxLength = 6
        Me.txtDemandNo.MendatroryField = False
        Me.txtDemandNo.MyLinkLable1 = Me.lblchkNo
        Me.txtDemandNo.MyLinkLable2 = Nothing
        Me.txtDemandNo.Name = "txtDemandNo"
        Me.txtDemandNo.Size = New System.Drawing.Size(213, 18)
        Me.txtDemandNo.TabIndex = 3
        '
        'lblchkNo
        '
        Me.lblchkNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblchkNo.Location = New System.Drawing.Point(11, 82)
        Me.lblchkNo.Name = "lblchkNo"
        Me.lblchkNo.Size = New System.Drawing.Size(68, 16)
        Me.lblchkNo.TabIndex = 15
        Me.lblchkNo.Text = "Form Series"
        '
        'btnGo
        '
        Me.btnGo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnGo.Location = New System.Drawing.Point(721, 152)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(39, 24)
        Me.btnGo.TabIndex = 11
        Me.btnGo.Text = ">>"
        '
        'txtFormIssued
        '
        Me.txtFormIssued.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.txtFormIssued.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFormIssued.Location = New System.Drawing.Point(608, 104)
        Me.txtFormIssued.MendatroryField = False
        Me.txtFormIssued.MyLinkLable1 = Me.MyLabel2
        Me.txtFormIssued.MyLinkLable2 = Nothing
        Me.txtFormIssued.Name = "txtFormIssued"
        Me.txtFormIssued.ReadOnly = True
        Me.txtFormIssued.Size = New System.Drawing.Size(128, 18)
        Me.txtFormIssued.TabIndex = 8
        Me.txtFormIssued.Text = "0"
        Me.txtFormIssued.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(469, 106)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(133, 16)
        Me.MyLabel2.TabIndex = 20
        Me.MyLabel2.Text = "Total No of Forms Issued"
        Me.MyLabel2.TextAlignment = System.Drawing.ContentAlignment.TopCenter
        '
        'txtToNo
        '
        Me.txtToNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToNo.Location = New System.Drawing.Point(316, 106)
        Me.txtToNo.MaxLength = 6
        Me.txtToNo.MendatroryField = False
        Me.txtToNo.MyLinkLable1 = Me.MyLabel1
        Me.txtToNo.MyLinkLable2 = Nothing
        Me.txtToNo.Name = "txtToNo"
        Me.txtToNo.Size = New System.Drawing.Size(128, 18)
        Me.txtToNo.TabIndex = 7
        Me.txtToNo.Text = "0"
        Me.txtToNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(262, 106)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(37, 16)
        Me.MyLabel1.TabIndex = 19
        Me.MyLabel1.Text = "To No"
        Me.MyLabel1.TextAlignment = System.Drawing.ContentAlignment.TopCenter
        '
        'fndFormIssueNo
        '
        Me.fndFormIssueNo.Location = New System.Drawing.Point(113, 12)
        Me.fndFormIssueNo.MendatroryField = True
        Me.fndFormIssueNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.fndFormIssueNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndFormIssueNo.MyLinkLable1 = Me.lblpaymentno
        Me.fndFormIssueNo.MyLinkLable2 = Nothing
        Me.fndFormIssueNo.MyMaxLength = 30
        Me.fndFormIssueNo.MyReadOnly = False
        Me.fndFormIssueNo.Name = "fndFormIssueNo"
        Me.fndFormIssueNo.Size = New System.Drawing.Size(259, 20)
        Me.fndFormIssueNo.TabIndex = 0
        Me.fndFormIssueNo.Value = ""
        '
        'lblpaymentno
        '
        Me.lblpaymentno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpaymentno.Location = New System.Drawing.Point(13, 34)
        Me.lblpaymentno.Name = "lblpaymentno"
        Me.lblpaymentno.Size = New System.Drawing.Size(67, 16)
        Me.lblpaymentno.TabIndex = 17
        Me.lblpaymentno.Text = "Demand No"
        '
        'btnNew
        '
        Me.btnNew.BackgroundImage = Global.ERP.My.Resources.Resources._new
        Me.btnNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnNew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnNew.Location = New System.Drawing.Point(378, 12)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(15, 20)
        Me.btnNew.TabIndex = 1
        '
        'ddlFromType
        '
        Me.ddlFromType.Location = New System.Drawing.Point(114, 58)
        Me.ddlFromType.MendatroryField = True
        Me.ddlFromType.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddlFromType.MyLinkLable1 = Me.lblcustomer
        Me.ddlFromType.MyLinkLable2 = Nothing
        Me.ddlFromType.MyReadOnly = False
        Me.ddlFromType.Name = "ddlFromType"
        Me.ddlFromType.Size = New System.Drawing.Size(154, 19)
        Me.ddlFromType.TabIndex = 4
        Me.ddlFromType.Value = ""
        '
        'lblcustomer
        '
        Me.lblcustomer.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcustomer.Location = New System.Drawing.Point(12, 58)
        Me.lblcustomer.Name = "lblcustomer"
        Me.lblcustomer.Size = New System.Drawing.Size(61, 16)
        Me.lblcustomer.TabIndex = 16
        Me.lblcustomer.Text = "Form Type"
        '
        'DgvFormIssue
        '
        Me.DgvFormIssue.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DgvFormIssue.Cursor = System.Windows.Forms.Cursors.Default
        Me.DgvFormIssue.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.DgvFormIssue.ForeColor = System.Drawing.Color.Black
        Me.DgvFormIssue.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.DgvFormIssue.Location = New System.Drawing.Point(11, 182)
        '
        'DgvFormIssue
        '
        Me.DgvFormIssue.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        GridViewTextBoxColumn1.HeaderText = "Line No"
        GridViewTextBoxColumn1.Name = "lineno"
        GridViewTextBoxColumn1.ReadOnly = True
        GridViewTextBoxColumn1.Width = 80
        GridViewTextBoxColumn2.HeaderText = "No"
        GridViewTextBoxColumn2.Name = "No"
        GridViewTextBoxColumn2.Width = 150
        GridViewDateTimeColumn1.Format = System.Windows.Forms.DateTimePickerFormat.[Long]
        GridViewDateTimeColumn1.HeaderText = "Date"
        GridViewDateTimeColumn1.Name = "Date"
        GridViewDateTimeColumn1.Width = 70
        GridViewDateTimeColumn2.Format = System.Windows.Forms.DateTimePickerFormat.[Long]
        GridViewDateTimeColumn2.HeaderText = "Expiry Date"
        GridViewDateTimeColumn2.Name = "ExpiryDate"
        GridViewDateTimeColumn2.Width = 70
        GridViewTextBoxColumn3.HeaderText = "Remark's"
        GridViewTextBoxColumn3.Name = "Remarks"
        GridViewTextBoxColumn3.Width = 360
        Me.DgvFormIssue.MasterTemplate.Columns.AddRange(New Telerik.WinControls.UI.GridViewDataColumn() {GridViewTextBoxColumn1, GridViewTextBoxColumn2, GridViewDateTimeColumn1, GridViewDateTimeColumn2, GridViewTextBoxColumn3})
        Me.DgvFormIssue.MasterTemplate.EnableGrouping = False
        Me.DgvFormIssue.Name = "DgvFormIssue"
        Me.DgvFormIssue.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.DgvFormIssue.Size = New System.Drawing.Size(749, 268)
        Me.DgvFormIssue.TabIndex = 12
        Me.DgvFormIssue.TabStop = False
        Me.DgvFormIssue.Text = "RadGridView1"
        '
        'txtFromNo
        '
        Me.txtFromNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFromNo.Location = New System.Drawing.Point(113, 106)
        Me.txtFromNo.MaxLength = 6
        Me.txtFromNo.MendatroryField = False
        Me.txtFromNo.MyLinkLable1 = Me.lblUnApplyAmt
        Me.txtFromNo.MyLinkLable2 = Nothing
        Me.txtFromNo.Name = "txtFromNo"
        Me.txtFromNo.Size = New System.Drawing.Size(128, 18)
        Me.txtFromNo.TabIndex = 6
        Me.txtFromNo.Text = "0"
        Me.txtFromNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblUnApplyAmt
        '
        Me.lblUnApplyAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUnApplyAmt.Location = New System.Drawing.Point(11, 106)
        Me.lblUnApplyAmt.Name = "lblUnApplyAmt"
        Me.lblUnApplyAmt.Size = New System.Drawing.Size(50, 16)
        Me.lblUnApplyAmt.TabIndex = 14
        Me.lblUnApplyAmt.Text = "From No"
        Me.lblUnApplyAmt.TextAlignment = System.Drawing.ContentAlignment.TopCenter
        '
        'txtFormSeries
        '
        Me.txtFormSeries.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFormSeries.Location = New System.Drawing.Point(114, 82)
        Me.txtFormSeries.MaxLength = 6
        Me.txtFormSeries.MendatroryField = False
        Me.txtFormSeries.MyLinkLable1 = Me.lblchkNo
        Me.txtFormSeries.MyLinkLable2 = Nothing
        Me.txtFormSeries.Name = "txtFormSeries"
        Me.txtFormSeries.Size = New System.Drawing.Size(213, 18)
        Me.txtFormSeries.TabIndex = 5
        '
        'txtCommetns
        '
        Me.txtCommetns.AutoSize = False
        Me.txtCommetns.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCommetns.Location = New System.Drawing.Point(113, 154)
        Me.txtCommetns.MaxLength = 100
        Me.txtCommetns.MendatroryField = False
        Me.txtCommetns.Multiline = True
        Me.txtCommetns.MyLinkLable1 = Me.lblnarration
        Me.txtCommetns.MyLinkLable2 = Nothing
        Me.txtCommetns.Name = "txtCommetns"
        Me.txtCommetns.Size = New System.Drawing.Size(582, 20)
        Me.txtCommetns.TabIndex = 10
        '
        'lblnarration
        '
        Me.lblnarration.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblnarration.Location = New System.Drawing.Point(8, 158)
        Me.lblnarration.Name = "lblnarration"
        Me.lblnarration.Size = New System.Drawing.Size(61, 16)
        Me.lblnarration.TabIndex = 12
        Me.lblnarration.Text = "Comments"
        '
        'txtRemarks
        '
        Me.txtRemarks.AutoSize = False
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.Location = New System.Drawing.Point(113, 130)
        Me.txtRemarks.MaxLength = 100
        Me.txtRemarks.MendatroryField = False
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.MyLinkLable1 = Me.lblreference
        Me.txtRemarks.MyLinkLable2 = Nothing
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(581, 20)
        Me.txtRemarks.TabIndex = 9
        '
        'lblreference
        '
        Me.lblreference.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblreference.Location = New System.Drawing.Point(9, 132)
        Me.lblreference.Name = "lblreference"
        Me.lblreference.Size = New System.Drawing.Size(51, 16)
        Me.lblreference.TabIndex = 13
        Me.lblreference.Text = "Remarks"
        '
        'txtFormTypeDesc
        '
        Me.txtFormTypeDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFormTypeDesc.Location = New System.Drawing.Point(274, 58)
        Me.txtFormTypeDesc.MendatroryField = False
        Me.txtFormTypeDesc.MyLinkLable1 = Me.lblcustomer
        Me.txtFormTypeDesc.MyLinkLable2 = Nothing
        Me.txtFormTypeDesc.Name = "txtFormTypeDesc"
        Me.txtFormTypeDesc.ReadOnly = True
        Me.txtFormTypeDesc.Size = New System.Drawing.Size(421, 18)
        Me.txtFormTypeDesc.TabIndex = 1
        Me.txtFormTypeDesc.TabStop = False
        '
        'dtpDemandDate
        '
        Me.dtpDemandDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpDemandDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDemandDate.Location = New System.Drawing.Point(511, 12)
        Me.dtpDemandDate.MendatroryField = False
        Me.dtpDemandDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDemandDate.MyLinkLable1 = Me.lblpaymentpostdate
        Me.dtpDemandDate.MyLinkLable2 = Nothing
        Me.dtpDemandDate.Name = "dtpDemandDate"
        Me.dtpDemandDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDemandDate.Size = New System.Drawing.Size(91, 20)
        Me.dtpDemandDate.TabIndex = 2
        Me.dtpDemandDate.TabStop = False
        Me.dtpDemandDate.Text = "10/06/2011"
        Me.dtpDemandDate.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'lblpaymentpostdate
        '
        Me.lblpaymentpostdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblpaymentpostdate.Location = New System.Drawing.Point(419, 12)
        Me.lblpaymentpostdate.Name = "lblpaymentpostdate"
        Me.lblpaymentpostdate.Size = New System.Drawing.Size(79, 16)
        Me.lblpaymentpostdate.TabIndex = 2
        Me.lblpaymentpostdate.Text = "Demand Date"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(78, 5)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 22)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(153, 5)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(69, 22)
        Me.btnPost.TabIndex = 2
        Me.btnPost.Text = "Post"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(702, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(3, 5)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Size = New System.Drawing.Size(775, 508)
        Me.SplitContainer1.SplitterDistance = 476
        Me.SplitContainer1.TabIndex = 0
        '
        'FrmFormIssueDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(775, 508)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmFormIssueDetails"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Form Issue Detail"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDemandNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblchkNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFormIssued, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblpaymentno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblcustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvFormIssue.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvFormIssue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblUnApplyAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFormSeries, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCommetns, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblnarration, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblreference, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFormTypeDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDemandDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblpaymentpostdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents ddlFromType As common.UserControls.txtFinder
    Friend WithEvents lblcustomer As common.Controls.MyLabel
    Friend WithEvents DgvFormIssue As common.UserControls.MyRadGridView
    Friend WithEvents txtFromNo As common.Controls.MyTextBox
    Friend WithEvents lblUnApplyAmt As common.Controls.MyLabel
    Friend WithEvents txtFormSeries As common.Controls.MyTextBox
    Friend WithEvents lblchkNo As common.Controls.MyLabel
    Friend WithEvents txtCommetns As common.Controls.MyTextBox
    Friend WithEvents lblnarration As common.Controls.MyLabel
    Friend WithEvents txtRemarks As common.Controls.MyTextBox
    Friend WithEvents lblreference As common.Controls.MyLabel
    Friend WithEvents txtFormTypeDesc As common.Controls.MyTextBox
    Friend WithEvents dtpDemandDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblpaymentpostdate As common.Controls.MyLabel
    Friend WithEvents fndFormIssueNo As common.UserControls.txtNavigator
    Friend WithEvents lblpaymentno As common.Controls.MyLabel
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtToNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtFormIssued As common.Controls.MyTextBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtDemandNo As common.Controls.MyTextBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

