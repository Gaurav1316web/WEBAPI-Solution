<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmBillChecklist
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.txtPlantHeadRemarks = New common.Controls.MyTextBox()
        Me.txtCheckedByAc = New common.Controls.MyTextBox()
        Me.txtProductor = New common.Controls.MyTextBox()
        Me.txtCheckedBy = New common.Controls.MyTextBox()
        Me.lbl_Remarks = New common.Controls.MyLabel()
        Me.lbl_Party = New common.Controls.MyLabel()
        Me.lbl_PO = New common.Controls.MyLabel()
        Me.lbl_ServiceBillNo = New common.Controls.MyLabel()
        Me.lbl_Billno = New common.Controls.MyLabel()
        Me.lbl_Value = New common.Controls.MyLabel()
        Me.lbl_GRN = New common.Controls.MyLabel()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.lblDocCode = New common.Controls.MyLabel()
        Me.rdbAPInvoice = New System.Windows.Forms.RadioButton()
        Me.rdbSRN = New System.Windows.Forms.RadioButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblfromDate = New common.Controls.MyLabel()
        Me.lblToDate = New common.Controls.MyLabel()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.btn_Go = New Telerik.WinControls.UI.RadButton()
        Me.fndDocCode = New common.UserControls.txtFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lblPlantRem = New common.Controls.MyLabel()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.REMARK = New common.Controls.MyLabel()
        Me.lblCheckCode1 = New common.Controls.MyLabel()
        Me.gvDocs = New common.UserControls.MyRadGridView()
        Me.btn_Reset = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.txtPlantHeadRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCheckedByAc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtProductor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCheckedBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_Remarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_Party, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_PO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_ServiceBillNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_Billno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_Value, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_GRN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.lblDocCode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.lblfromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_Go, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPlantRem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.REMARK, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCheckCode1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDocs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDocs.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_Reset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btn_Reset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(770, 513)
        Me.SplitContainer1.SplitterDistance = 481
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtPlantHeadRemarks)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtCheckedByAc)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtProductor)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtCheckedBy)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lbl_Remarks)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lbl_Party)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lbl_PO)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lbl_ServiceBillNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lbl_Billno)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lbl_Value)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lbl_GRN)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel9)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel6)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadGroupBox1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblPlantRem)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel8)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer2.Panel1.Controls.Add(Me.REMARK)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblCheckCode1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gvDocs)
        Me.SplitContainer2.Size = New System.Drawing.Size(770, 481)
        Me.SplitContainer2.SplitterDistance = 400
        Me.SplitContainer2.TabIndex = 614
        '
        'txtPlantHeadRemarks
        '
        Me.txtPlantHeadRemarks.AutoSize = False
        Me.txtPlantHeadRemarks.CalculationExpression = Nothing
        Me.txtPlantHeadRemarks.FieldCode = Nothing
        Me.txtPlantHeadRemarks.FieldDesc = Nothing
        Me.txtPlantHeadRemarks.FieldMaxLength = 0
        Me.txtPlantHeadRemarks.FieldName = Nothing
        Me.txtPlantHeadRemarks.isCalculatedField = False
        Me.txtPlantHeadRemarks.IsSourceFromTable = False
        Me.txtPlantHeadRemarks.IsSourceFromValueList = False
        Me.txtPlantHeadRemarks.IsUnique = False
        Me.txtPlantHeadRemarks.Location = New System.Drawing.Point(155, 360)
        Me.txtPlantHeadRemarks.MaxLength = 200
        Me.txtPlantHeadRemarks.MendatroryField = True
        Me.txtPlantHeadRemarks.Multiline = True
        Me.txtPlantHeadRemarks.MyLinkLable1 = Nothing
        Me.txtPlantHeadRemarks.MyLinkLable2 = Nothing
        Me.txtPlantHeadRemarks.Name = "txtPlantHeadRemarks"
        Me.txtPlantHeadRemarks.ReferenceFieldDesc = Nothing
        Me.txtPlantHeadRemarks.ReferenceFieldName = Nothing
        Me.txtPlantHeadRemarks.ReferenceTableName = Nothing
        Me.txtPlantHeadRemarks.Size = New System.Drawing.Size(359, 34)
        Me.txtPlantHeadRemarks.TabIndex = 635
        '
        'txtCheckedByAc
        '
        Me.txtCheckedByAc.CalculationExpression = Nothing
        Me.txtCheckedByAc.FieldCode = Nothing
        Me.txtCheckedByAc.FieldDesc = Nothing
        Me.txtCheckedByAc.FieldMaxLength = 0
        Me.txtCheckedByAc.FieldName = Nothing
        Me.txtCheckedByAc.isCalculatedField = False
        Me.txtCheckedByAc.IsSourceFromTable = False
        Me.txtCheckedByAc.IsSourceFromValueList = False
        Me.txtCheckedByAc.IsUnique = False
        Me.txtCheckedByAc.Location = New System.Drawing.Point(155, 334)
        Me.txtCheckedByAc.MaxLength = 50
        Me.txtCheckedByAc.MendatroryField = True
        Me.txtCheckedByAc.MyLinkLable1 = Nothing
        Me.txtCheckedByAc.MyLinkLable2 = Nothing
        Me.txtCheckedByAc.Name = "txtCheckedByAc"
        Me.txtCheckedByAc.ReferenceFieldDesc = Nothing
        Me.txtCheckedByAc.ReferenceFieldName = Nothing
        Me.txtCheckedByAc.ReferenceTableName = Nothing
        Me.txtCheckedByAc.Size = New System.Drawing.Size(359, 20)
        Me.txtCheckedByAc.TabIndex = 634
        '
        'txtProductor
        '
        Me.txtProductor.CalculationExpression = Nothing
        Me.txtProductor.FieldCode = Nothing
        Me.txtProductor.FieldDesc = Nothing
        Me.txtProductor.FieldMaxLength = 0
        Me.txtProductor.FieldName = Nothing
        Me.txtProductor.isCalculatedField = False
        Me.txtProductor.IsSourceFromTable = False
        Me.txtProductor.IsSourceFromValueList = False
        Me.txtProductor.IsUnique = False
        Me.txtProductor.Location = New System.Drawing.Point(155, 309)
        Me.txtProductor.MaxLength = 50
        Me.txtProductor.MendatroryField = True
        Me.txtProductor.MyLinkLable1 = Nothing
        Me.txtProductor.MyLinkLable2 = Nothing
        Me.txtProductor.Name = "txtProductor"
        Me.txtProductor.ReferenceFieldDesc = Nothing
        Me.txtProductor.ReferenceFieldName = Nothing
        Me.txtProductor.ReferenceTableName = Nothing
        Me.txtProductor.Size = New System.Drawing.Size(359, 20)
        Me.txtProductor.TabIndex = 633
        '
        'txtCheckedBy
        '
        Me.txtCheckedBy.CalculationExpression = Nothing
        Me.txtCheckedBy.FieldCode = Nothing
        Me.txtCheckedBy.FieldDesc = Nothing
        Me.txtCheckedBy.FieldMaxLength = 0
        Me.txtCheckedBy.FieldName = Nothing
        Me.txtCheckedBy.isCalculatedField = False
        Me.txtCheckedBy.IsSourceFromTable = False
        Me.txtCheckedBy.IsSourceFromValueList = False
        Me.txtCheckedBy.IsUnique = False
        Me.txtCheckedBy.Location = New System.Drawing.Point(155, 284)
        Me.txtCheckedBy.MaxLength = 50
        Me.txtCheckedBy.MendatroryField = True
        Me.txtCheckedBy.MyLinkLable1 = Nothing
        Me.txtCheckedBy.MyLinkLable2 = Nothing
        Me.txtCheckedBy.Name = "txtCheckedBy"
        Me.txtCheckedBy.ReferenceFieldDesc = Nothing
        Me.txtCheckedBy.ReferenceFieldName = Nothing
        Me.txtCheckedBy.ReferenceTableName = Nothing
        Me.txtCheckedBy.Size = New System.Drawing.Size(359, 20)
        Me.txtCheckedBy.TabIndex = 632
        '
        'lbl_Remarks
        '
        Me.lbl_Remarks.AutoSize = False
        Me.lbl_Remarks.BorderVisible = True
        Me.lbl_Remarks.FieldName = Nothing
        Me.lbl_Remarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Remarks.Location = New System.Drawing.Point(155, 235)
        Me.lbl_Remarks.Name = "lbl_Remarks"
        Me.lbl_Remarks.Size = New System.Drawing.Size(359, 43)
        Me.lbl_Remarks.TabIndex = 631
        Me.lbl_Remarks.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lbl_Remarks.TextWrap = False
        '
        'lbl_Party
        '
        Me.lbl_Party.AutoSize = False
        Me.lbl_Party.BorderVisible = True
        Me.lbl_Party.FieldName = Nothing
        Me.lbl_Party.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Party.Location = New System.Drawing.Point(155, 122)
        Me.lbl_Party.Name = "lbl_Party"
        Me.lbl_Party.Size = New System.Drawing.Size(210, 18)
        Me.lbl_Party.TabIndex = 630
        Me.lbl_Party.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lbl_Party.TextWrap = False
        '
        'lbl_PO
        '
        Me.lbl_PO.AutoSize = False
        Me.lbl_PO.BorderVisible = True
        Me.lbl_PO.FieldName = Nothing
        Me.lbl_PO.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PO.Location = New System.Drawing.Point(155, 144)
        Me.lbl_PO.Name = "lbl_PO"
        Me.lbl_PO.Size = New System.Drawing.Size(210, 18)
        Me.lbl_PO.TabIndex = 629
        Me.lbl_PO.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lbl_PO.TextWrap = False
        '
        'lbl_ServiceBillNo
        '
        Me.lbl_ServiceBillNo.AutoSize = False
        Me.lbl_ServiceBillNo.BorderVisible = True
        Me.lbl_ServiceBillNo.FieldName = Nothing
        Me.lbl_ServiceBillNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_ServiceBillNo.Location = New System.Drawing.Point(155, 211)
        Me.lbl_ServiceBillNo.Name = "lbl_ServiceBillNo"
        Me.lbl_ServiceBillNo.Size = New System.Drawing.Size(210, 18)
        Me.lbl_ServiceBillNo.TabIndex = 628
        Me.lbl_ServiceBillNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lbl_ServiceBillNo.TextWrap = False
        '
        'lbl_Billno
        '
        Me.lbl_Billno.AutoSize = False
        Me.lbl_Billno.BorderVisible = True
        Me.lbl_Billno.FieldName = Nothing
        Me.lbl_Billno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Billno.Location = New System.Drawing.Point(694, 122)
        Me.lbl_Billno.Name = "lbl_Billno"
        Me.lbl_Billno.Size = New System.Drawing.Size(46, 18)
        Me.lbl_Billno.TabIndex = 627
        Me.lbl_Billno.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lbl_Billno.TextWrap = False
        Me.lbl_Billno.Visible = False
        '
        'lbl_Value
        '
        Me.lbl_Value.AutoSize = False
        Me.lbl_Value.BorderVisible = True
        Me.lbl_Value.FieldName = Nothing
        Me.lbl_Value.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Value.Location = New System.Drawing.Point(155, 189)
        Me.lbl_Value.Name = "lbl_Value"
        Me.lbl_Value.Size = New System.Drawing.Size(210, 18)
        Me.lbl_Value.TabIndex = 626
        Me.lbl_Value.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lbl_Value.TextWrap = False
        '
        'lbl_GRN
        '
        Me.lbl_GRN.AutoSize = False
        Me.lbl_GRN.BorderVisible = True
        Me.lbl_GRN.FieldName = Nothing
        Me.lbl_GRN.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_GRN.Location = New System.Drawing.Point(155, 166)
        Me.lbl_GRN.Name = "lbl_GRN"
        Me.lbl_GRN.Size = New System.Drawing.Size(210, 18)
        Me.lbl_GRN.TabIndex = 624
        Me.lbl_GRN.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lbl_GRN.TextWrap = False
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(638, 124)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(50, 16)
        Me.MyLabel9.TabIndex = 623
        Me.MyLabel9.Text = "BILL NO"
        Me.MyLabel9.Visible = False
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(21, 191)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(43, 16)
        Me.MyLabel6.TabIndex = 622
        Me.MyLabel6.Text = "VALUE"
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(21, 213)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(132, 16)
        Me.MyLabel4.TabIndex = 621
        Me.MyLabel4.Text = "BILL/SERVICE BILL NO."
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(21, 313)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(79, 16)
        Me.MyLabel3.TabIndex = 620
        Me.MyLabel3.Text = "PRODUCTOR"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(21, 288)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(83, 16)
        Me.MyLabel2.TabIndex = 619
        Me.MyLabel2.Text = "CHECKED BY "
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.lblDocCode)
        Me.RadGroupBox1.Controls.Add(Me.rdbAPInvoice)
        Me.RadGroupBox1.Controls.Add(Me.rdbSRN)
        Me.RadGroupBox1.Controls.Add(Me.Panel1)
        Me.RadGroupBox1.Controls.Add(Me.btn_Go)
        Me.RadGroupBox1.Controls.Add(Me.fndDocCode)
        Me.RadGroupBox1.HeaderText = "Date Range"
        Me.RadGroupBox1.Location = New System.Drawing.Point(21, 12)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(432, 101)
        Me.RadGroupBox1.TabIndex = 618
        Me.RadGroupBox1.Text = "Date Range"
        '
        'lblDocCode
        '
        Me.lblDocCode.FieldName = Nothing
        Me.lblDocCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocCode.Location = New System.Drawing.Point(11, 73)
        Me.lblDocCode.Name = "lblDocCode"
        Me.lblDocCode.Size = New System.Drawing.Size(88, 16)
        Me.lblDocCode.TabIndex = 624
        Me.lblDocCode.Text = "Document Code"
        '
        'rdbAPInvoice
        '
        Me.rdbAPInvoice.AutoSize = True
        Me.rdbAPInvoice.Location = New System.Drawing.Point(162, 18)
        Me.rdbAPInvoice.Name = "rdbAPInvoice"
        Me.rdbAPInvoice.Size = New System.Drawing.Size(77, 17)
        Me.rdbAPInvoice.TabIndex = 31
        Me.rdbAPInvoice.Text = "AP Invoice"
        Me.rdbAPInvoice.UseVisualStyleBackColor = True
        '
        'rdbSRN
        '
        Me.rdbSRN.AutoSize = True
        Me.rdbSRN.Checked = True
        Me.rdbSRN.Location = New System.Drawing.Point(9, 18)
        Me.rdbSRN.Name = "rdbSRN"
        Me.rdbSRN.Size = New System.Drawing.Size(128, 17)
        Me.rdbSRN.TabIndex = 30
        Me.rdbSRN.TabStop = True
        Me.rdbSRN.Text = "Store Received Note"
        Me.rdbSRN.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lblfromDate)
        Me.Panel1.Controls.Add(Me.lblToDate)
        Me.Panel1.Controls.Add(Me.txtToDate)
        Me.Panel1.Controls.Add(Me.txtFromDate)
        Me.Panel1.Location = New System.Drawing.Point(8, 40)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(306, 27)
        Me.Panel1.TabIndex = 29
        '
        'lblfromDate
        '
        Me.lblfromDate.FieldName = Nothing
        Me.lblfromDate.Location = New System.Drawing.Point(3, 4)
        Me.lblfromDate.Name = "lblfromDate"
        Me.lblfromDate.Size = New System.Drawing.Size(59, 18)
        Me.lblfromDate.TabIndex = 13
        Me.lblfromDate.Text = "From Date"
        '
        'lblToDate
        '
        Me.lblToDate.FieldName = Nothing
        Me.lblToDate.Location = New System.Drawing.Point(164, 5)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(45, 18)
        Me.lblToDate.TabIndex = 14
        Me.lblToDate.Text = "To Date"
        '
        'txtToDate
        '
        Me.txtToDate.CalculationExpression = Nothing
        Me.txtToDate.CustomFormat = "dd-MM-yyyy"
        Me.txtToDate.FieldCode = Nothing
        Me.txtToDate.FieldDesc = Nothing
        Me.txtToDate.FieldMaxLength = 0
        Me.txtToDate.FieldName = Nothing
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.isCalculatedField = False
        Me.txtToDate.IsSourceFromTable = False
        Me.txtToDate.IsSourceFromValueList = False
        Me.txtToDate.IsUnique = False
        Me.txtToDate.Location = New System.Drawing.Point(215, 3)
        Me.txtToDate.MendatroryField = False
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Nothing
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.ReferenceFieldDesc = Nothing
        Me.txtToDate.ReferenceFieldName = Nothing
        Me.txtToDate.ReferenceTableName = Nothing
        Me.txtToDate.Size = New System.Drawing.Size(82, 20)
        Me.txtToDate.TabIndex = 1
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "17-12-2011"
        Me.txtToDate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'txtFromDate
        '
        Me.txtFromDate.CalculationExpression = Nothing
        Me.txtFromDate.CustomFormat = "dd-MM-yyyy"
        Me.txtFromDate.FieldCode = Nothing
        Me.txtFromDate.FieldDesc = Nothing
        Me.txtFromDate.FieldMaxLength = 0
        Me.txtFromDate.FieldName = Nothing
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.isCalculatedField = False
        Me.txtFromDate.IsSourceFromTable = False
        Me.txtFromDate.IsSourceFromValueList = False
        Me.txtFromDate.IsUnique = False
        Me.txtFromDate.Location = New System.Drawing.Point(76, 3)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Nothing
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.ReferenceFieldDesc = Nothing
        Me.txtFromDate.ReferenceFieldName = Nothing
        Me.txtFromDate.ReferenceTableName = Nothing
        Me.txtFromDate.Size = New System.Drawing.Size(82, 20)
        Me.txtFromDate.TabIndex = 0
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "17-12-2011"
        Me.txtFromDate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'btn_Go
        '
        Me.btn_Go.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Go.Location = New System.Drawing.Point(313, 73)
        Me.btn_Go.Name = "btn_Go"
        Me.btn_Go.Size = New System.Drawing.Size(105, 19)
        Me.btn_Go.TabIndex = 614
        Me.btn_Go.Text = ">>"
        '
        'fndDocCode
        '
        Me.fndDocCode.CalculationExpression = Nothing
        Me.fndDocCode.FieldCode = Nothing
        Me.fndDocCode.FieldDesc = Nothing
        Me.fndDocCode.FieldMaxLength = 0
        Me.fndDocCode.FieldName = Nothing
        Me.fndDocCode.isCalculatedField = False
        Me.fndDocCode.IsSourceFromTable = False
        Me.fndDocCode.IsSourceFromValueList = False
        Me.fndDocCode.IsUnique = False
        Me.fndDocCode.Location = New System.Drawing.Point(105, 73)
        Me.fndDocCode.MendatroryField = True
        Me.fndDocCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndDocCode.MyLinkLable1 = Nothing
        Me.fndDocCode.MyLinkLable2 = Nothing
        Me.fndDocCode.MyReadOnly = False
        Me.fndDocCode.MyShowMasterFormButton = False
        Me.fndDocCode.Name = "fndDocCode"
        Me.fndDocCode.ReferenceFieldDesc = Nothing
        Me.fndDocCode.ReferenceFieldName = Nothing
        Me.fndDocCode.ReferenceTableName = Nothing
        Me.fndDocCode.Size = New System.Drawing.Size(200, 19)
        Me.fndDocCode.TabIndex = 615
        Me.fndDocCode.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(21, 168)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(51, 16)
        Me.MyLabel1.TabIndex = 617
        Me.MyLabel1.Text = "GRN NO"
        '
        'lblPlantRem
        '
        Me.lblPlantRem.FieldName = Nothing
        Me.lblPlantRem.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPlantRem.Location = New System.Drawing.Point(21, 363)
        Me.lblPlantRem.Name = "lblPlantRem"
        Me.lblPlantRem.Size = New System.Drawing.Size(136, 16)
        Me.lblPlantRem.TabIndex = 14
        Me.lblPlantRem.Text = "PLANT HEAD REMARKS"
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(21, 338)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(105, 16)
        Me.MyLabel8.TabIndex = 612
        Me.MyLabel8.Text = "CHECKED  BY A/C"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(21, 122)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(99, 16)
        Me.MyLabel5.TabIndex = 11
        Me.MyLabel5.Text = "NAME OF PARTY"
        '
        'REMARK
        '
        Me.REMARK.FieldName = Nothing
        Me.REMARK.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.REMARK.Location = New System.Drawing.Point(21, 237)
        Me.REMARK.Name = "REMARK"
        Me.REMARK.Size = New System.Drawing.Size(55, 16)
        Me.REMARK.TabIndex = 610
        Me.REMARK.Text = "REMARK"
        '
        'lblCheckCode1
        '
        Me.lblCheckCode1.FieldName = Nothing
        Me.lblCheckCode1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCheckCode1.Location = New System.Drawing.Point(21, 146)
        Me.lblCheckCode1.Name = "lblCheckCode1"
        Me.lblCheckCode1.Size = New System.Drawing.Size(46, 16)
        Me.lblCheckCode1.TabIndex = 10
        Me.lblCheckCode1.Text = "PO NO."
        '
        'gvDocs
        '
        Me.gvDocs.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvDocs.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvDocs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvDocs.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvDocs.ForeColor = System.Drawing.Color.Black
        Me.gvDocs.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvDocs.Location = New System.Drawing.Point(0, 0)
        '
        'gvDocs
        '
        Me.gvDocs.MasterTemplate.AllowDeleteRow = False
        Me.gvDocs.MasterTemplate.EnableFiltering = True
        Me.gvDocs.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvDocs.Name = "gvDocs"
        Me.gvDocs.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvDocs.ShowGroupPanel = False
        Me.gvDocs.ShowHeaderCellButtons = True
        Me.gvDocs.Size = New System.Drawing.Size(770, 77)
        Me.gvDocs.TabIndex = 607
        Me.gvDocs.TabStop = False
        Me.gvDocs.Text = "RadGridView1"
        '
        'btn_Reset
        '
        Me.btn_Reset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Reset.Location = New System.Drawing.Point(75, 5)
        Me.btn_Reset.Name = "btn_Reset"
        Me.btn_Reset.Size = New System.Drawing.Size(69, 18)
        Me.btn_Reset.TabIndex = 625
        Me.btn_Reset.Text = "Reset"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(3, 5)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(66, 18)
        Me.btnPrint.TabIndex = 0
        Me.btnPrint.Text = "Print"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(701, 5)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 1
        Me.btnclose.Text = "Close"
        '
        'frmBillChecklist
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(770, 513)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmBillChecklist"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Bill Checklist"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.txtPlantHeadRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCheckedByAc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtProductor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCheckedBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_Remarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_Party, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_PO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_ServiceBillNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_Billno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_Value, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_GRN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.lblDocCode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.lblfromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_Go, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPlantRem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.REMARK, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCheckCode1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDocs.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDocs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_Reset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblPlantRem As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents lblCheckCode1 As common.Controls.MyLabel
    Friend WithEvents gvDocs As common.UserControls.MyRadGridView
    Friend WithEvents REMARK As common.Controls.MyLabel
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents btn_Go As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndDocCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox1 As RadGroupBox
    Friend WithEvents lblDocCode As common.Controls.MyLabel
    Friend WithEvents rdbAPInvoice As RadioButton
    Friend WithEvents rdbSRN As RadioButton
    Friend WithEvents Panel1 As Panel
    Friend WithEvents lblfromDate As common.Controls.MyLabel
    Friend WithEvents lblToDate As common.Controls.MyLabel
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents lbl_Remarks As common.Controls.MyLabel
    Friend WithEvents lbl_Party As common.Controls.MyLabel
    Friend WithEvents lbl_PO As common.Controls.MyLabel
    Friend WithEvents lbl_ServiceBillNo As common.Controls.MyLabel
    Friend WithEvents lbl_Billno As common.Controls.MyLabel
    Friend WithEvents lbl_Value As common.Controls.MyLabel
    Friend WithEvents lbl_GRN As common.Controls.MyLabel
    Friend WithEvents txtPlantHeadRemarks As common.Controls.MyTextBox
    Friend WithEvents txtCheckedByAc As common.Controls.MyTextBox
    Friend WithEvents txtProductor As common.Controls.MyTextBox
    Friend WithEvents txtCheckedBy As common.Controls.MyTextBox
    Friend WithEvents btn_Reset As RadButton
End Class

