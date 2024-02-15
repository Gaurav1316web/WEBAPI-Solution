Imports Telerik.WinControls.UI
Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmDailyMilkProducts
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
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem5 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem6 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem7 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem8 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem9 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.MyTextBox16 = New common.Controls.MyTextBox()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyLabel30 = New common.Controls.MyLabel()
        Me.MyLabel41 = New common.Controls.MyLabel()
        Me.MyLabel42 = New common.Controls.MyLabel()
        Me.MyLabel43 = New common.Controls.MyLabel()
        Me.RadDropDownList2 = New Telerik.WinControls.UI.RadDropDownList()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyLabel31 = New common.Controls.MyLabel()
        Me.MyLabel32 = New common.Controls.MyLabel()
        Me.MyLabel33 = New common.Controls.MyLabel()
        Me.MyLabel34 = New common.Controls.MyLabel()
        Me.MyLabel35 = New common.Controls.MyLabel()
        Me.MyLabel36 = New common.Controls.MyLabel()
        Me.RadDropDownList1 = New Telerik.WinControls.UI.RadDropDownList()
        Me.txtMilkDispConv = New common.MyNumBox()
        Me.txtRCDFunit5 = New common.MyNumBox()
        Me.txtRCDFunit4 = New common.MyNumBox()
        Me.txtRCDFunit3 = New common.MyNumBox()
        Me.txtRCDFunit2 = New common.MyNumBox()
        Me.txtRCDFunit1 = New common.MyNumBox()
        Me.txtRCDFunitName5 = New common.Controls.MyTextBox()
        Me.txtRCDFunitName4 = New common.Controls.MyTextBox()
        Me.txtRCDFunitName3 = New common.Controls.MyTextBox()
        Me.txtRCDFunitName2 = New common.Controls.MyTextBox()
        Me.txtRCDFunitName1 = New common.Controls.MyTextBox()
        Me.txtOwnMilkConv = New common.MyNumBox()
        Me.txtMilkReceiveConversion = New common.MyNumBox()
        Me.MyLabel22 = New common.Controls.MyLabel()
        Me.MyLabel18 = New common.Controls.MyLabel()
        Me.MyLabel19 = New common.Controls.MyLabel()
        Me.MyLabel21 = New common.Controls.MyLabel()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.grpGateEntryType = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyLabel20 = New common.Controls.MyLabel()
        Me.MyLabel17 = New common.Controls.MyLabel()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.MyLabel15 = New common.Controls.MyLabel()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.ddCreditCash = New Telerik.WinControls.UI.RadDropDownList()
        Me.txtSNF = New common.MyNumBox()
        Me.txtFAT = New common.MyNumBox()
        Me.txtMilkIssued = New common.MyNumBox()
        Me.txtSuppliestRMG = New common.MyNumBox()
        Me.txtSuppliestNMG = New common.MyNumBox()
        Me.txtLocalMilkMarketing = New common.MyNumBox()
        Me.txtMilkReceiptRMG = New common.MyNumBox()
        Me.txtMilkProcOwnDC = New common.MyNumBox()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtReportingDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.txtReportDate = New common.Controls.MyDateTimePicker()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.txt = New common.Controls.MyLabel()
        Me.lblfromDate = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.txtTableButter = New common.MyNumBox()
        Me.txtSMPReceipt = New common.MyNumBox()
        Me.txtSMPPurchase = New common.MyNumBox()
        Me.txtGheeReceipt = New common.MyNumBox()
        Me.txtGheePurchase = New common.MyNumBox()
        Me.txtRmrks = New common.Controls.MyTextBox()
        Me.MyLabel29 = New common.Controls.MyLabel()
        Me.MyLabel28 = New common.Controls.MyLabel()
        Me.MyLabel27 = New common.Controls.MyLabel()
        Me.MyLabel26 = New common.Controls.MyLabel()
        Me.MyLabel25 = New common.Controls.MyLabel()
        Me.MyLabel24 = New common.Controls.MyLabel()
        Me.MyLabel23 = New common.Controls.MyLabel()
        Me.btnReverse = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.MyTextBox16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.MyLabel30, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel41, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel42, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel43, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadDropDownList2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.MyLabel31, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel32, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel33, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel34, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel35, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel36, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadDropDownList1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMilkDispConv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRCDFunit5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRCDFunit4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRCDFunit3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRCDFunit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRCDFunit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRCDFunitName5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRCDFunitName4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRCDFunitName3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRCDFunitName2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRCDFunitName1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOwnMilkConv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMilkReceiveConversion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpGateEntryType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpGateEntryType.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddCreditCash, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSNF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFAT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMilkIssued, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSuppliestRMG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSuppliestNMG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLocalMilkMarketing, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMilkReceiptRMG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMilkProcOwnDC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtReportingDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtReportDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblfromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage3.SuspendLayout()
        CType(Me.txtTableButter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSMPReceipt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSMPPurchase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGheeReceipt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGheePurchase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRmrks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel29, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel28, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel27, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel26, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReverse)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(1087, 517)
        Me.SplitContainer1.SplitterDistance = 488
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
        Me.RadPageView1.Size = New System.Drawing.Size(1087, 488)
        Me.RadPageView1.TabIndex = 2
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.MyTextBox16)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel12)
        Me.RadPageViewPage1.Controls.Add(Me.grpGateEntryType)
        Me.RadPageViewPage1.Controls.Add(Me.txtReportingDate)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.UsLock1)
        Me.RadPageViewPage1.Controls.Add(Me.txtReportDate)
        Me.RadPageViewPage1.Controls.Add(Me.txtDocNo)
        Me.RadPageViewPage1.Controls.Add(Me.btnNew)
        Me.RadPageViewPage1.Controls.Add(Me.txt)
        Me.RadPageViewPage1.Controls.Add(Me.lblfromDate)
        Me.RadPageViewPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(70.0!, 26.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1066, 442)
        Me.RadPageViewPage1.Text = "Liquid Milk"
        '
        'MyTextBox16
        '
        Me.MyTextBox16.CalculationExpression = Nothing
        Me.MyTextBox16.FieldCode = Nothing
        Me.MyTextBox16.FieldDesc = Nothing
        Me.MyTextBox16.FieldMaxLength = 0
        Me.MyTextBox16.FieldName = Nothing
        Me.MyTextBox16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyTextBox16.isCalculatedField = False
        Me.MyTextBox16.IsSourceFromTable = False
        Me.MyTextBox16.IsSourceFromValueList = False
        Me.MyTextBox16.IsUnique = False
        Me.MyTextBox16.Location = New System.Drawing.Point(764, 132)
        Me.MyTextBox16.MaxLength = 200
        Me.MyTextBox16.MendatroryField = False
        Me.MyTextBox16.MyLinkLable1 = Nothing
        Me.MyTextBox16.MyLinkLable2 = Nothing
        Me.MyTextBox16.Name = "MyTextBox16"
        Me.MyTextBox16.ReferenceFieldDesc = Nothing
        Me.MyTextBox16.ReferenceFieldName = Nothing
        Me.MyTextBox16.ReferenceTableName = Nothing
        Me.MyTextBox16.Size = New System.Drawing.Size(197, 18)
        Me.MyTextBox16.TabIndex = 403
        Me.MyTextBox16.TabStop = False
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox4)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox3)
        Me.RadGroupBox1.Controls.Add(Me.txtMilkDispConv)
        Me.RadGroupBox1.Controls.Add(Me.txtRCDFunit5)
        Me.RadGroupBox1.Controls.Add(Me.txtRCDFunit4)
        Me.RadGroupBox1.Controls.Add(Me.txtRCDFunit3)
        Me.RadGroupBox1.Controls.Add(Me.txtRCDFunit2)
        Me.RadGroupBox1.Controls.Add(Me.txtRCDFunit1)
        Me.RadGroupBox1.Controls.Add(Me.txtRCDFunitName5)
        Me.RadGroupBox1.Controls.Add(Me.txtRCDFunitName4)
        Me.RadGroupBox1.Controls.Add(Me.txtRCDFunitName3)
        Me.RadGroupBox1.Controls.Add(Me.txtRCDFunitName2)
        Me.RadGroupBox1.Controls.Add(Me.txtRCDFunitName1)
        Me.RadGroupBox1.Controls.Add(Me.txtOwnMilkConv)
        Me.RadGroupBox1.Controls.Add(Me.txtMilkReceiveConversion)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel22)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel18)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel19)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel21)
        Me.RadGroupBox1.HeaderText = "Milk For Conversion"
        Me.RadGroupBox1.Location = New System.Drawing.Point(5, 219)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(658, 234)
        Me.RadGroupBox1.TabIndex = 402
        Me.RadGroupBox1.Text = "Milk For Conversion"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.MyLabel30)
        Me.RadGroupBox4.Controls.Add(Me.MyLabel41)
        Me.RadGroupBox4.Controls.Add(Me.MyLabel42)
        Me.RadGroupBox4.Controls.Add(Me.MyLabel43)
        Me.RadGroupBox4.Controls.Add(Me.RadDropDownList2)
        Me.RadGroupBox4.HeaderText = ""
        Me.RadGroupBox4.Location = New System.Drawing.Point(242, 26)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Size = New System.Drawing.Size(66, 64)
        Me.RadGroupBox4.TabIndex = 1473
        '
        'MyLabel30
        '
        Me.MyLabel30.FieldName = Nothing
        Me.MyLabel30.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel30.Location = New System.Drawing.Point(22, 44)
        Me.MyLabel30.Name = "MyLabel30"
        Me.MyLabel30.Size = New System.Drawing.Size(21, 18)
        Me.MyLabel30.TabIndex = 1069
        Me.MyLabel30.Text = "KG"
        '
        'MyLabel41
        '
        Me.MyLabel41.FieldName = Nothing
        Me.MyLabel41.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel41.Location = New System.Drawing.Point(21, 24)
        Me.MyLabel41.Name = "MyLabel41"
        Me.MyLabel41.Size = New System.Drawing.Size(21, 18)
        Me.MyLabel41.TabIndex = 1068
        Me.MyLabel41.Text = "KG"
        '
        'MyLabel42
        '
        Me.MyLabel42.FieldName = Nothing
        Me.MyLabel42.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel42.Location = New System.Drawing.Point(21, 2)
        Me.MyLabel42.Name = "MyLabel42"
        Me.MyLabel42.Size = New System.Drawing.Size(21, 18)
        Me.MyLabel42.TabIndex = 1067
        Me.MyLabel42.Text = "KG"
        '
        'MyLabel43
        '
        Me.MyLabel43.FieldName = Nothing
        Me.MyLabel43.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel43.Location = New System.Drawing.Point(5, 147)
        Me.MyLabel43.Name = "MyLabel43"
        Me.MyLabel43.Size = New System.Drawing.Size(66, 16)
        Me.MyLabel43.TabIndex = 362
        Me.MyLabel43.Text = "Credit/Cash"
        '
        'RadDropDownList2
        '
        Me.RadDropDownList2.AutoCompleteDisplayMember = Nothing
        Me.RadDropDownList2.AutoCompleteValueMember = Nothing
        Me.RadDropDownList2.DropDownAnimationEnabled = True
        Me.RadDropDownList2.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        RadListDataItem1.Text = "Credit"
        RadListDataItem2.Text = "Cash"
        RadListDataItem3.Text = "Both"
        Me.RadDropDownList2.Items.Add(RadListDataItem1)
        Me.RadDropDownList2.Items.Add(RadListDataItem2)
        Me.RadDropDownList2.Items.Add(RadListDataItem3)
        Me.RadDropDownList2.Location = New System.Drawing.Point(95, 146)
        Me.RadDropDownList2.Name = "RadDropDownList2"
        Me.RadDropDownList2.Size = New System.Drawing.Size(139, 20)
        Me.RadDropDownList2.TabIndex = 365
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.MyLabel31)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel32)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel33)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel34)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel35)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel36)
        Me.RadGroupBox3.Controls.Add(Me.RadDropDownList1)
        Me.RadGroupBox3.HeaderText = ""
        Me.RadGroupBox3.Location = New System.Drawing.Point(242, 113)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Size = New System.Drawing.Size(66, 110)
        Me.RadGroupBox3.TabIndex = 1472
        '
        'MyLabel31
        '
        Me.MyLabel31.FieldName = Nothing
        Me.MyLabel31.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel31.Location = New System.Drawing.Point(21, 90)
        Me.MyLabel31.Name = "MyLabel31"
        Me.MyLabel31.Size = New System.Drawing.Size(21, 18)
        Me.MyLabel31.TabIndex = 1071
        Me.MyLabel31.Text = "KG"
        '
        'MyLabel32
        '
        Me.MyLabel32.FieldName = Nothing
        Me.MyLabel32.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel32.Location = New System.Drawing.Point(22, 45)
        Me.MyLabel32.Name = "MyLabel32"
        Me.MyLabel32.Size = New System.Drawing.Size(21, 18)
        Me.MyLabel32.TabIndex = 1070
        Me.MyLabel32.Text = "KG"
        '
        'MyLabel33
        '
        Me.MyLabel33.FieldName = Nothing
        Me.MyLabel33.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel33.Location = New System.Drawing.Point(21, 67)
        Me.MyLabel33.Name = "MyLabel33"
        Me.MyLabel33.Size = New System.Drawing.Size(21, 18)
        Me.MyLabel33.TabIndex = 1069
        Me.MyLabel33.Text = "KG"
        '
        'MyLabel34
        '
        Me.MyLabel34.FieldName = Nothing
        Me.MyLabel34.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel34.Location = New System.Drawing.Point(21, 24)
        Me.MyLabel34.Name = "MyLabel34"
        Me.MyLabel34.Size = New System.Drawing.Size(21, 18)
        Me.MyLabel34.TabIndex = 1068
        Me.MyLabel34.Text = "KG"
        '
        'MyLabel35
        '
        Me.MyLabel35.FieldName = Nothing
        Me.MyLabel35.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel35.Location = New System.Drawing.Point(21, 2)
        Me.MyLabel35.Name = "MyLabel35"
        Me.MyLabel35.Size = New System.Drawing.Size(21, 18)
        Me.MyLabel35.TabIndex = 1067
        Me.MyLabel35.Text = "KG"
        '
        'MyLabel36
        '
        Me.MyLabel36.FieldName = Nothing
        Me.MyLabel36.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel36.Location = New System.Drawing.Point(5, 147)
        Me.MyLabel36.Name = "MyLabel36"
        Me.MyLabel36.Size = New System.Drawing.Size(66, 16)
        Me.MyLabel36.TabIndex = 362
        Me.MyLabel36.Text = "Credit/Cash"
        '
        'RadDropDownList1
        '
        Me.RadDropDownList1.AutoCompleteDisplayMember = Nothing
        Me.RadDropDownList1.AutoCompleteValueMember = Nothing
        Me.RadDropDownList1.DropDownAnimationEnabled = True
        Me.RadDropDownList1.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        RadListDataItem4.Text = "Credit"
        RadListDataItem5.Text = "Cash"
        RadListDataItem6.Text = "Both"
        Me.RadDropDownList1.Items.Add(RadListDataItem4)
        Me.RadDropDownList1.Items.Add(RadListDataItem5)
        Me.RadDropDownList1.Items.Add(RadListDataItem6)
        Me.RadDropDownList1.Location = New System.Drawing.Point(95, 146)
        Me.RadDropDownList1.Name = "RadDropDownList1"
        Me.RadDropDownList1.Size = New System.Drawing.Size(139, 20)
        Me.RadDropDownList1.TabIndex = 365
        '
        'txtMilkDispConv
        '
        Me.txtMilkDispConv.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtMilkDispConv.CalculationExpression = Nothing
        Me.txtMilkDispConv.DecimalPlaces = 5
        Me.txtMilkDispConv.FieldCode = Nothing
        Me.txtMilkDispConv.FieldDesc = Nothing
        Me.txtMilkDispConv.FieldMaxLength = 0
        Me.txtMilkDispConv.FieldName = Nothing
        Me.txtMilkDispConv.isCalculatedField = False
        Me.txtMilkDispConv.IsSourceFromTable = False
        Me.txtMilkDispConv.IsSourceFromValueList = False
        Me.txtMilkDispConv.IsUnique = False
        Me.txtMilkDispConv.Location = New System.Drawing.Point(314, 70)
        Me.txtMilkDispConv.MendatroryField = False
        Me.txtMilkDispConv.MyLinkLable1 = Nothing
        Me.txtMilkDispConv.MyLinkLable2 = Nothing
        Me.txtMilkDispConv.Name = "txtMilkDispConv"
        Me.txtMilkDispConv.ReferenceFieldDesc = Nothing
        Me.txtMilkDispConv.ReferenceFieldName = Nothing
        Me.txtMilkDispConv.ReferenceTableName = Nothing
        Me.txtMilkDispConv.Size = New System.Drawing.Size(160, 20)
        Me.txtMilkDispConv.TabIndex = 1471
        Me.txtMilkDispConv.Text = "0"
        Me.txtMilkDispConv.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMilkDispConv.Value = 0R
        '
        'txtRCDFunit5
        '
        Me.txtRCDFunit5.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtRCDFunit5.CalculationExpression = Nothing
        Me.txtRCDFunit5.DecimalPlaces = 5
        Me.txtRCDFunit5.FieldCode = Nothing
        Me.txtRCDFunit5.FieldDesc = Nothing
        Me.txtRCDFunit5.FieldMaxLength = 0
        Me.txtRCDFunit5.FieldName = Nothing
        Me.txtRCDFunit5.isCalculatedField = False
        Me.txtRCDFunit5.IsSourceFromTable = False
        Me.txtRCDFunit5.IsSourceFromValueList = False
        Me.txtRCDFunit5.IsUnique = False
        Me.txtRCDFunit5.Location = New System.Drawing.Point(314, 201)
        Me.txtRCDFunit5.MendatroryField = False
        Me.txtRCDFunit5.MyLinkLable1 = Nothing
        Me.txtRCDFunit5.MyLinkLable2 = Nothing
        Me.txtRCDFunit5.Name = "txtRCDFunit5"
        Me.txtRCDFunit5.ReferenceFieldDesc = Nothing
        Me.txtRCDFunit5.ReferenceFieldName = Nothing
        Me.txtRCDFunit5.ReferenceTableName = Nothing
        Me.txtRCDFunit5.Size = New System.Drawing.Size(160, 20)
        Me.txtRCDFunit5.TabIndex = 1470
        Me.txtRCDFunit5.Text = "0"
        Me.txtRCDFunit5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtRCDFunit5.Value = 0R
        '
        'txtRCDFunit4
        '
        Me.txtRCDFunit4.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtRCDFunit4.CalculationExpression = Nothing
        Me.txtRCDFunit4.DecimalPlaces = 5
        Me.txtRCDFunit4.FieldCode = Nothing
        Me.txtRCDFunit4.FieldDesc = Nothing
        Me.txtRCDFunit4.FieldMaxLength = 0
        Me.txtRCDFunit4.FieldName = Nothing
        Me.txtRCDFunit4.isCalculatedField = False
        Me.txtRCDFunit4.IsSourceFromTable = False
        Me.txtRCDFunit4.IsSourceFromValueList = False
        Me.txtRCDFunit4.IsUnique = False
        Me.txtRCDFunit4.Location = New System.Drawing.Point(314, 179)
        Me.txtRCDFunit4.MendatroryField = False
        Me.txtRCDFunit4.MyLinkLable1 = Nothing
        Me.txtRCDFunit4.MyLinkLable2 = Nothing
        Me.txtRCDFunit4.Name = "txtRCDFunit4"
        Me.txtRCDFunit4.ReferenceFieldDesc = Nothing
        Me.txtRCDFunit4.ReferenceFieldName = Nothing
        Me.txtRCDFunit4.ReferenceTableName = Nothing
        Me.txtRCDFunit4.Size = New System.Drawing.Size(160, 20)
        Me.txtRCDFunit4.TabIndex = 1469
        Me.txtRCDFunit4.Text = "0"
        Me.txtRCDFunit4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtRCDFunit4.Value = 0R
        '
        'txtRCDFunit3
        '
        Me.txtRCDFunit3.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtRCDFunit3.CalculationExpression = Nothing
        Me.txtRCDFunit3.DecimalPlaces = 5
        Me.txtRCDFunit3.FieldCode = Nothing
        Me.txtRCDFunit3.FieldDesc = Nothing
        Me.txtRCDFunit3.FieldMaxLength = 0
        Me.txtRCDFunit3.FieldName = Nothing
        Me.txtRCDFunit3.isCalculatedField = False
        Me.txtRCDFunit3.IsSourceFromTable = False
        Me.txtRCDFunit3.IsSourceFromValueList = False
        Me.txtRCDFunit3.IsUnique = False
        Me.txtRCDFunit3.Location = New System.Drawing.Point(314, 157)
        Me.txtRCDFunit3.MendatroryField = False
        Me.txtRCDFunit3.MyLinkLable1 = Nothing
        Me.txtRCDFunit3.MyLinkLable2 = Nothing
        Me.txtRCDFunit3.Name = "txtRCDFunit3"
        Me.txtRCDFunit3.ReferenceFieldDesc = Nothing
        Me.txtRCDFunit3.ReferenceFieldName = Nothing
        Me.txtRCDFunit3.ReferenceTableName = Nothing
        Me.txtRCDFunit3.Size = New System.Drawing.Size(160, 20)
        Me.txtRCDFunit3.TabIndex = 1468
        Me.txtRCDFunit3.Text = "0"
        Me.txtRCDFunit3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtRCDFunit3.Value = 0R
        '
        'txtRCDFunit2
        '
        Me.txtRCDFunit2.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtRCDFunit2.CalculationExpression = Nothing
        Me.txtRCDFunit2.DecimalPlaces = 5
        Me.txtRCDFunit2.FieldCode = Nothing
        Me.txtRCDFunit2.FieldDesc = Nothing
        Me.txtRCDFunit2.FieldMaxLength = 0
        Me.txtRCDFunit2.FieldName = Nothing
        Me.txtRCDFunit2.isCalculatedField = False
        Me.txtRCDFunit2.IsSourceFromTable = False
        Me.txtRCDFunit2.IsSourceFromValueList = False
        Me.txtRCDFunit2.IsUnique = False
        Me.txtRCDFunit2.Location = New System.Drawing.Point(314, 135)
        Me.txtRCDFunit2.MendatroryField = False
        Me.txtRCDFunit2.MyLinkLable1 = Nothing
        Me.txtRCDFunit2.MyLinkLable2 = Nothing
        Me.txtRCDFunit2.Name = "txtRCDFunit2"
        Me.txtRCDFunit2.ReferenceFieldDesc = Nothing
        Me.txtRCDFunit2.ReferenceFieldName = Nothing
        Me.txtRCDFunit2.ReferenceTableName = Nothing
        Me.txtRCDFunit2.Size = New System.Drawing.Size(160, 20)
        Me.txtRCDFunit2.TabIndex = 1467
        Me.txtRCDFunit2.Text = "0"
        Me.txtRCDFunit2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtRCDFunit2.Value = 0R
        '
        'txtRCDFunit1
        '
        Me.txtRCDFunit1.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtRCDFunit1.CalculationExpression = Nothing
        Me.txtRCDFunit1.DecimalPlaces = 5
        Me.txtRCDFunit1.FieldCode = Nothing
        Me.txtRCDFunit1.FieldDesc = Nothing
        Me.txtRCDFunit1.FieldMaxLength = 0
        Me.txtRCDFunit1.FieldName = Nothing
        Me.txtRCDFunit1.isCalculatedField = False
        Me.txtRCDFunit1.IsSourceFromTable = False
        Me.txtRCDFunit1.IsSourceFromValueList = False
        Me.txtRCDFunit1.IsUnique = False
        Me.txtRCDFunit1.Location = New System.Drawing.Point(314, 113)
        Me.txtRCDFunit1.MendatroryField = False
        Me.txtRCDFunit1.MyLinkLable1 = Nothing
        Me.txtRCDFunit1.MyLinkLable2 = Nothing
        Me.txtRCDFunit1.Name = "txtRCDFunit1"
        Me.txtRCDFunit1.ReferenceFieldDesc = Nothing
        Me.txtRCDFunit1.ReferenceFieldName = Nothing
        Me.txtRCDFunit1.ReferenceTableName = Nothing
        Me.txtRCDFunit1.Size = New System.Drawing.Size(160, 20)
        Me.txtRCDFunit1.TabIndex = 1466
        Me.txtRCDFunit1.Text = "0"
        Me.txtRCDFunit1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtRCDFunit1.Value = 0R
        '
        'txtRCDFunitName5
        '
        Me.txtRCDFunitName5.CalculationExpression = Nothing
        Me.txtRCDFunitName5.FieldCode = Nothing
        Me.txtRCDFunitName5.FieldDesc = Nothing
        Me.txtRCDFunitName5.FieldMaxLength = 0
        Me.txtRCDFunitName5.FieldName = Nothing
        Me.txtRCDFunitName5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRCDFunitName5.isCalculatedField = False
        Me.txtRCDFunitName5.IsSourceFromTable = False
        Me.txtRCDFunitName5.IsSourceFromValueList = False
        Me.txtRCDFunitName5.IsUnique = False
        Me.txtRCDFunitName5.Location = New System.Drawing.Point(10, 205)
        Me.txtRCDFunitName5.MaxLength = 200
        Me.txtRCDFunitName5.MendatroryField = False
        Me.txtRCDFunitName5.MyLinkLable1 = Nothing
        Me.txtRCDFunitName5.MyLinkLable2 = Nothing
        Me.txtRCDFunitName5.Name = "txtRCDFunitName5"
        Me.txtRCDFunitName5.ReferenceFieldDesc = Nothing
        Me.txtRCDFunitName5.ReferenceFieldName = Nothing
        Me.txtRCDFunitName5.ReferenceTableName = Nothing
        Me.txtRCDFunitName5.Size = New System.Drawing.Size(227, 18)
        Me.txtRCDFunitName5.TabIndex = 1465
        Me.txtRCDFunitName5.TabStop = False
        '
        'txtRCDFunitName4
        '
        Me.txtRCDFunitName4.CalculationExpression = Nothing
        Me.txtRCDFunitName4.FieldCode = Nothing
        Me.txtRCDFunitName4.FieldDesc = Nothing
        Me.txtRCDFunitName4.FieldMaxLength = 0
        Me.txtRCDFunitName4.FieldName = Nothing
        Me.txtRCDFunitName4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRCDFunitName4.isCalculatedField = False
        Me.txtRCDFunitName4.IsSourceFromTable = False
        Me.txtRCDFunitName4.IsSourceFromValueList = False
        Me.txtRCDFunitName4.IsUnique = False
        Me.txtRCDFunitName4.Location = New System.Drawing.Point(10, 181)
        Me.txtRCDFunitName4.MaxLength = 200
        Me.txtRCDFunitName4.MendatroryField = False
        Me.txtRCDFunitName4.MyLinkLable1 = Nothing
        Me.txtRCDFunitName4.MyLinkLable2 = Nothing
        Me.txtRCDFunitName4.Name = "txtRCDFunitName4"
        Me.txtRCDFunitName4.ReferenceFieldDesc = Nothing
        Me.txtRCDFunitName4.ReferenceFieldName = Nothing
        Me.txtRCDFunitName4.ReferenceTableName = Nothing
        Me.txtRCDFunitName4.Size = New System.Drawing.Size(227, 18)
        Me.txtRCDFunitName4.TabIndex = 1464
        Me.txtRCDFunitName4.TabStop = False
        '
        'txtRCDFunitName3
        '
        Me.txtRCDFunitName3.CalculationExpression = Nothing
        Me.txtRCDFunitName3.FieldCode = Nothing
        Me.txtRCDFunitName3.FieldDesc = Nothing
        Me.txtRCDFunitName3.FieldMaxLength = 0
        Me.txtRCDFunitName3.FieldName = Nothing
        Me.txtRCDFunitName3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRCDFunitName3.isCalculatedField = False
        Me.txtRCDFunitName3.IsSourceFromTable = False
        Me.txtRCDFunitName3.IsSourceFromValueList = False
        Me.txtRCDFunitName3.IsUnique = False
        Me.txtRCDFunitName3.Location = New System.Drawing.Point(10, 157)
        Me.txtRCDFunitName3.MaxLength = 200
        Me.txtRCDFunitName3.MendatroryField = False
        Me.txtRCDFunitName3.MyLinkLable1 = Nothing
        Me.txtRCDFunitName3.MyLinkLable2 = Nothing
        Me.txtRCDFunitName3.Name = "txtRCDFunitName3"
        Me.txtRCDFunitName3.ReferenceFieldDesc = Nothing
        Me.txtRCDFunitName3.ReferenceFieldName = Nothing
        Me.txtRCDFunitName3.ReferenceTableName = Nothing
        Me.txtRCDFunitName3.Size = New System.Drawing.Size(227, 18)
        Me.txtRCDFunitName3.TabIndex = 1463
        Me.txtRCDFunitName3.TabStop = False
        '
        'txtRCDFunitName2
        '
        Me.txtRCDFunitName2.CalculationExpression = Nothing
        Me.txtRCDFunitName2.FieldCode = Nothing
        Me.txtRCDFunitName2.FieldDesc = Nothing
        Me.txtRCDFunitName2.FieldMaxLength = 0
        Me.txtRCDFunitName2.FieldName = Nothing
        Me.txtRCDFunitName2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRCDFunitName2.isCalculatedField = False
        Me.txtRCDFunitName2.IsSourceFromTable = False
        Me.txtRCDFunitName2.IsSourceFromValueList = False
        Me.txtRCDFunitName2.IsUnique = False
        Me.txtRCDFunitName2.Location = New System.Drawing.Point(10, 135)
        Me.txtRCDFunitName2.MaxLength = 200
        Me.txtRCDFunitName2.MendatroryField = False
        Me.txtRCDFunitName2.MyLinkLable1 = Nothing
        Me.txtRCDFunitName2.MyLinkLable2 = Nothing
        Me.txtRCDFunitName2.Name = "txtRCDFunitName2"
        Me.txtRCDFunitName2.ReferenceFieldDesc = Nothing
        Me.txtRCDFunitName2.ReferenceFieldName = Nothing
        Me.txtRCDFunitName2.ReferenceTableName = Nothing
        Me.txtRCDFunitName2.Size = New System.Drawing.Size(227, 18)
        Me.txtRCDFunitName2.TabIndex = 1462
        Me.txtRCDFunitName2.TabStop = False
        '
        'txtRCDFunitName1
        '
        Me.txtRCDFunitName1.CalculationExpression = Nothing
        Me.txtRCDFunitName1.FieldCode = Nothing
        Me.txtRCDFunitName1.FieldDesc = Nothing
        Me.txtRCDFunitName1.FieldMaxLength = 0
        Me.txtRCDFunitName1.FieldName = Nothing
        Me.txtRCDFunitName1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRCDFunitName1.isCalculatedField = False
        Me.txtRCDFunitName1.IsSourceFromTable = False
        Me.txtRCDFunitName1.IsSourceFromValueList = False
        Me.txtRCDFunitName1.IsUnique = False
        Me.txtRCDFunitName1.Location = New System.Drawing.Point(10, 113)
        Me.txtRCDFunitName1.MaxLength = 200
        Me.txtRCDFunitName1.MendatroryField = False
        Me.txtRCDFunitName1.MyLinkLable1 = Nothing
        Me.txtRCDFunitName1.MyLinkLable2 = Nothing
        Me.txtRCDFunitName1.Name = "txtRCDFunitName1"
        Me.txtRCDFunitName1.ReferenceFieldDesc = Nothing
        Me.txtRCDFunitName1.ReferenceFieldName = Nothing
        Me.txtRCDFunitName1.ReferenceTableName = Nothing
        Me.txtRCDFunitName1.Size = New System.Drawing.Size(227, 18)
        Me.txtRCDFunitName1.TabIndex = 1461
        Me.txtRCDFunitName1.TabStop = False
        '
        'txtOwnMilkConv
        '
        Me.txtOwnMilkConv.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtOwnMilkConv.CalculationExpression = Nothing
        Me.txtOwnMilkConv.DecimalPlaces = 5
        Me.txtOwnMilkConv.FieldCode = Nothing
        Me.txtOwnMilkConv.FieldDesc = Nothing
        Me.txtOwnMilkConv.FieldMaxLength = 0
        Me.txtOwnMilkConv.FieldName = Nothing
        Me.txtOwnMilkConv.isCalculatedField = False
        Me.txtOwnMilkConv.IsSourceFromTable = False
        Me.txtOwnMilkConv.IsSourceFromValueList = False
        Me.txtOwnMilkConv.IsUnique = False
        Me.txtOwnMilkConv.Location = New System.Drawing.Point(314, 48)
        Me.txtOwnMilkConv.MendatroryField = False
        Me.txtOwnMilkConv.MyLinkLable1 = Nothing
        Me.txtOwnMilkConv.MyLinkLable2 = Nothing
        Me.txtOwnMilkConv.Name = "txtOwnMilkConv"
        Me.txtOwnMilkConv.ReferenceFieldDesc = Nothing
        Me.txtOwnMilkConv.ReferenceFieldName = Nothing
        Me.txtOwnMilkConv.ReferenceTableName = Nothing
        Me.txtOwnMilkConv.Size = New System.Drawing.Size(160, 20)
        Me.txtOwnMilkConv.TabIndex = 1460
        Me.txtOwnMilkConv.Text = "0"
        Me.txtOwnMilkConv.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtOwnMilkConv.Value = 0R
        '
        'txtMilkReceiveConversion
        '
        Me.txtMilkReceiveConversion.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtMilkReceiveConversion.CalculationExpression = Nothing
        Me.txtMilkReceiveConversion.DecimalPlaces = 5
        Me.txtMilkReceiveConversion.FieldCode = Nothing
        Me.txtMilkReceiveConversion.FieldDesc = Nothing
        Me.txtMilkReceiveConversion.FieldMaxLength = 0
        Me.txtMilkReceiveConversion.FieldName = Nothing
        Me.txtMilkReceiveConversion.isCalculatedField = False
        Me.txtMilkReceiveConversion.IsSourceFromTable = False
        Me.txtMilkReceiveConversion.IsSourceFromValueList = False
        Me.txtMilkReceiveConversion.IsUnique = False
        Me.txtMilkReceiveConversion.Location = New System.Drawing.Point(314, 26)
        Me.txtMilkReceiveConversion.MendatroryField = False
        Me.txtMilkReceiveConversion.MyLinkLable1 = Nothing
        Me.txtMilkReceiveConversion.MyLinkLable2 = Nothing
        Me.txtMilkReceiveConversion.Name = "txtMilkReceiveConversion"
        Me.txtMilkReceiveConversion.ReferenceFieldDesc = Nothing
        Me.txtMilkReceiveConversion.ReferenceFieldName = Nothing
        Me.txtMilkReceiveConversion.ReferenceTableName = Nothing
        Me.txtMilkReceiveConversion.Size = New System.Drawing.Size(160, 20)
        Me.txtMilkReceiveConversion.TabIndex = 1459
        Me.txtMilkReceiveConversion.Text = "0"
        Me.txtMilkReceiveConversion.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMilkReceiveConversion.Value = 0R
        '
        'MyLabel22
        '
        Me.MyLabel22.FieldName = Nothing
        Me.MyLabel22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel22.Location = New System.Drawing.Point(10, 69)
        Me.MyLabel22.Name = "MyLabel22"
        Me.MyLabel22.Size = New System.Drawing.Size(167, 16)
        Me.MyLabel22.TabIndex = 418
        Me.MyLabel22.Text = "Milk Dispatched For Conversion"
        '
        'MyLabel18
        '
        Me.MyLabel18.FieldName = Nothing
        Me.MyLabel18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel18.Location = New System.Drawing.Point(10, 91)
        Me.MyLabel18.Name = "MyLabel18"
        Me.MyLabel18.Size = New System.Drawing.Size(83, 16)
        Me.MyLabel18.TabIndex = 400
        Me.MyLabel18.Text = "To RCDF Units"
        '
        'MyLabel19
        '
        Me.MyLabel19.FieldName = Nothing
        Me.MyLabel19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel19.Location = New System.Drawing.Point(10, 48)
        Me.MyLabel19.Name = "MyLabel19"
        Me.MyLabel19.Size = New System.Drawing.Size(163, 16)
        Me.MyLabel19.TabIndex = 397
        Me.MyLabel19.Text = "Own Milk Used For Conversion"
        '
        'MyLabel21
        '
        Me.MyLabel21.FieldName = Nothing
        Me.MyLabel21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel21.Location = New System.Drawing.Point(10, 26)
        Me.MyLabel21.Name = "MyLabel21"
        Me.MyLabel21.Size = New System.Drawing.Size(157, 16)
        Me.MyLabel21.TabIndex = 0
        Me.MyLabel21.Text = "Milk Received For Conversion"
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel12.Location = New System.Drawing.Point(673, 133)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(90, 18)
        Me.MyLabel12.TabIndex = 400
        Me.MyLabel12.Text = "BULK SALE / PVT"
        '
        'grpGateEntryType
        '
        Me.grpGateEntryType.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpGateEntryType.Controls.Add(Me.RadGroupBox2)
        Me.grpGateEntryType.Controls.Add(Me.txtSNF)
        Me.grpGateEntryType.Controls.Add(Me.txtFAT)
        Me.grpGateEntryType.Controls.Add(Me.txtMilkIssued)
        Me.grpGateEntryType.Controls.Add(Me.txtSuppliestRMG)
        Me.grpGateEntryType.Controls.Add(Me.txtSuppliestNMG)
        Me.grpGateEntryType.Controls.Add(Me.txtLocalMilkMarketing)
        Me.grpGateEntryType.Controls.Add(Me.txtMilkReceiptRMG)
        Me.grpGateEntryType.Controls.Add(Me.txtMilkProcOwnDC)
        Me.grpGateEntryType.Controls.Add(Me.MyLabel11)
        Me.grpGateEntryType.Controls.Add(Me.MyLabel10)
        Me.grpGateEntryType.Controls.Add(Me.MyLabel9)
        Me.grpGateEntryType.Controls.Add(Me.MyLabel8)
        Me.grpGateEntryType.Controls.Add(Me.MyLabel7)
        Me.grpGateEntryType.Controls.Add(Me.MyLabel1)
        Me.grpGateEntryType.Controls.Add(Me.MyLabel6)
        Me.grpGateEntryType.Controls.Add(Me.MyLabel4)
        Me.grpGateEntryType.Controls.Add(Me.MyLabel5)
        Me.grpGateEntryType.HeaderText = "Liquid Milk"
        Me.grpGateEntryType.Location = New System.Drawing.Point(5, 35)
        Me.grpGateEntryType.Name = "grpGateEntryType"
        Me.grpGateEntryType.Size = New System.Drawing.Size(658, 178)
        Me.grpGateEntryType.TabIndex = 399
        Me.grpGateEntryType.Text = "Liquid Milk"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.MyLabel20)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel17)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel16)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel15)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel13)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel14)
        Me.RadGroupBox2.Controls.Add(Me.ddCreditCash)
        Me.RadGroupBox2.HeaderText = ""
        Me.RadGroupBox2.Location = New System.Drawing.Point(242, 32)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Size = New System.Drawing.Size(66, 136)
        Me.RadGroupBox2.TabIndex = 1461
        '
        'MyLabel20
        '
        Me.MyLabel20.FieldName = Nothing
        Me.MyLabel20.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel20.Location = New System.Drawing.Point(21, 115)
        Me.MyLabel20.Name = "MyLabel20"
        Me.MyLabel20.Size = New System.Drawing.Size(21, 18)
        Me.MyLabel20.TabIndex = 1072
        Me.MyLabel20.Text = "KG"
        '
        'MyLabel17
        '
        Me.MyLabel17.FieldName = Nothing
        Me.MyLabel17.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel17.Location = New System.Drawing.Point(21, 90)
        Me.MyLabel17.Name = "MyLabel17"
        Me.MyLabel17.Size = New System.Drawing.Size(21, 18)
        Me.MyLabel17.TabIndex = 1071
        Me.MyLabel17.Text = "KG"
        '
        'MyLabel16
        '
        Me.MyLabel16.FieldName = Nothing
        Me.MyLabel16.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel16.Location = New System.Drawing.Point(22, 45)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(17, 18)
        Me.MyLabel16.TabIndex = 1070
        Me.MyLabel16.Text = "LT"
        '
        'MyLabel15
        '
        Me.MyLabel15.FieldName = Nothing
        Me.MyLabel15.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel15.Location = New System.Drawing.Point(21, 67)
        Me.MyLabel15.Name = "MyLabel15"
        Me.MyLabel15.Size = New System.Drawing.Size(21, 18)
        Me.MyLabel15.TabIndex = 1069
        Me.MyLabel15.Text = "KG"
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel13.Location = New System.Drawing.Point(21, 24)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(21, 18)
        Me.MyLabel13.TabIndex = 1068
        Me.MyLabel13.Text = "KG"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(21, 2)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(21, 18)
        Me.MyLabel2.TabIndex = 1067
        Me.MyLabel2.Text = "KG"
        '
        'MyLabel14
        '
        Me.MyLabel14.FieldName = Nothing
        Me.MyLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel14.Location = New System.Drawing.Point(5, 147)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(66, 16)
        Me.MyLabel14.TabIndex = 362
        Me.MyLabel14.Text = "Credit/Cash"
        '
        'ddCreditCash
        '
        Me.ddCreditCash.AutoCompleteDisplayMember = Nothing
        Me.ddCreditCash.AutoCompleteValueMember = Nothing
        Me.ddCreditCash.DropDownAnimationEnabled = True
        Me.ddCreditCash.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        RadListDataItem7.Text = "Credit"
        RadListDataItem8.Text = "Cash"
        RadListDataItem9.Text = "Both"
        Me.ddCreditCash.Items.Add(RadListDataItem7)
        Me.ddCreditCash.Items.Add(RadListDataItem8)
        Me.ddCreditCash.Items.Add(RadListDataItem9)
        Me.ddCreditCash.Location = New System.Drawing.Point(95, 146)
        Me.ddCreditCash.Name = "ddCreditCash"
        Me.ddCreditCash.Size = New System.Drawing.Size(139, 20)
        Me.ddCreditCash.TabIndex = 365
        '
        'txtSNF
        '
        Me.txtSNF.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtSNF.CalculationExpression = Nothing
        Me.txtSNF.DecimalPlaces = 5
        Me.txtSNF.FieldCode = Nothing
        Me.txtSNF.FieldDesc = Nothing
        Me.txtSNF.FieldMaxLength = 0
        Me.txtSNF.FieldName = Nothing
        Me.txtSNF.isCalculatedField = False
        Me.txtSNF.IsSourceFromTable = False
        Me.txtSNF.IsSourceFromValueList = False
        Me.txtSNF.IsUnique = False
        Me.txtSNF.Location = New System.Drawing.Point(567, 34)
        Me.txtSNF.MendatroryField = False
        Me.txtSNF.MyLinkLable1 = Nothing
        Me.txtSNF.MyLinkLable2 = Nothing
        Me.txtSNF.Name = "txtSNF"
        Me.txtSNF.ReferenceFieldDesc = Nothing
        Me.txtSNF.ReferenceFieldName = Nothing
        Me.txtSNF.ReferenceTableName = Nothing
        Me.txtSNF.Size = New System.Drawing.Size(86, 20)
        Me.txtSNF.TabIndex = 1460
        Me.txtSNF.Text = "0"
        Me.txtSNF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSNF.Value = 0R
        '
        'txtFAT
        '
        Me.txtFAT.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtFAT.CalculationExpression = Nothing
        Me.txtFAT.DecimalPlaces = 5
        Me.txtFAT.FieldCode = Nothing
        Me.txtFAT.FieldDesc = Nothing
        Me.txtFAT.FieldMaxLength = 0
        Me.txtFAT.FieldName = Nothing
        Me.txtFAT.isCalculatedField = False
        Me.txtFAT.IsSourceFromTable = False
        Me.txtFAT.IsSourceFromValueList = False
        Me.txtFAT.IsUnique = False
        Me.txtFAT.Location = New System.Drawing.Point(479, 34)
        Me.txtFAT.MendatroryField = False
        Me.txtFAT.MyLinkLable1 = Nothing
        Me.txtFAT.MyLinkLable2 = Nothing
        Me.txtFAT.Name = "txtFAT"
        Me.txtFAT.ReferenceFieldDesc = Nothing
        Me.txtFAT.ReferenceFieldName = Nothing
        Me.txtFAT.ReferenceTableName = Nothing
        Me.txtFAT.Size = New System.Drawing.Size(86, 20)
        Me.txtFAT.TabIndex = 1459
        Me.txtFAT.Text = "0"
        Me.txtFAT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtFAT.Value = 0R
        '
        'txtMilkIssued
        '
        Me.txtMilkIssued.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtMilkIssued.CalculationExpression = Nothing
        Me.txtMilkIssued.DecimalPlaces = 5
        Me.txtMilkIssued.FieldCode = Nothing
        Me.txtMilkIssued.FieldDesc = Nothing
        Me.txtMilkIssued.FieldMaxLength = 0
        Me.txtMilkIssued.FieldName = Nothing
        Me.txtMilkIssued.isCalculatedField = False
        Me.txtMilkIssued.IsSourceFromTable = False
        Me.txtMilkIssued.IsSourceFromValueList = False
        Me.txtMilkIssued.IsUnique = False
        Me.txtMilkIssued.Location = New System.Drawing.Point(313, 148)
        Me.txtMilkIssued.MendatroryField = False
        Me.txtMilkIssued.MyLinkLable1 = Nothing
        Me.txtMilkIssued.MyLinkLable2 = Nothing
        Me.txtMilkIssued.Name = "txtMilkIssued"
        Me.txtMilkIssued.ReferenceFieldDesc = Nothing
        Me.txtMilkIssued.ReferenceFieldName = Nothing
        Me.txtMilkIssued.ReferenceTableName = Nothing
        Me.txtMilkIssued.Size = New System.Drawing.Size(160, 20)
        Me.txtMilkIssued.TabIndex = 1458
        Me.txtMilkIssued.Text = "0"
        Me.txtMilkIssued.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMilkIssued.Value = 0R
        '
        'txtSuppliestRMG
        '
        Me.txtSuppliestRMG.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtSuppliestRMG.CalculationExpression = Nothing
        Me.txtSuppliestRMG.DecimalPlaces = 5
        Me.txtSuppliestRMG.FieldCode = Nothing
        Me.txtSuppliestRMG.FieldDesc = Nothing
        Me.txtSuppliestRMG.FieldMaxLength = 0
        Me.txtSuppliestRMG.FieldName = Nothing
        Me.txtSuppliestRMG.isCalculatedField = False
        Me.txtSuppliestRMG.IsSourceFromTable = False
        Me.txtSuppliestRMG.IsSourceFromValueList = False
        Me.txtSuppliestRMG.IsUnique = False
        Me.txtSuppliestRMG.Location = New System.Drawing.Point(313, 122)
        Me.txtSuppliestRMG.MendatroryField = False
        Me.txtSuppliestRMG.MyLinkLable1 = Nothing
        Me.txtSuppliestRMG.MyLinkLable2 = Nothing
        Me.txtSuppliestRMG.Name = "txtSuppliestRMG"
        Me.txtSuppliestRMG.ReferenceFieldDesc = Nothing
        Me.txtSuppliestRMG.ReferenceFieldName = Nothing
        Me.txtSuppliestRMG.ReferenceTableName = Nothing
        Me.txtSuppliestRMG.Size = New System.Drawing.Size(160, 20)
        Me.txtSuppliestRMG.TabIndex = 1457
        Me.txtSuppliestRMG.Text = "0"
        Me.txtSuppliestRMG.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSuppliestRMG.Value = 0R
        '
        'txtSuppliestNMG
        '
        Me.txtSuppliestNMG.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtSuppliestNMG.CalculationExpression = Nothing
        Me.txtSuppliestNMG.DecimalPlaces = 5
        Me.txtSuppliestNMG.FieldCode = Nothing
        Me.txtSuppliestNMG.FieldDesc = Nothing
        Me.txtSuppliestNMG.FieldMaxLength = 0
        Me.txtSuppliestNMG.FieldName = Nothing
        Me.txtSuppliestNMG.isCalculatedField = False
        Me.txtSuppliestNMG.IsSourceFromTable = False
        Me.txtSuppliestNMG.IsSourceFromValueList = False
        Me.txtSuppliestNMG.IsUnique = False
        Me.txtSuppliestNMG.Location = New System.Drawing.Point(313, 100)
        Me.txtSuppliestNMG.MendatroryField = False
        Me.txtSuppliestNMG.MyLinkLable1 = Nothing
        Me.txtSuppliestNMG.MyLinkLable2 = Nothing
        Me.txtSuppliestNMG.Name = "txtSuppliestNMG"
        Me.txtSuppliestNMG.ReferenceFieldDesc = Nothing
        Me.txtSuppliestNMG.ReferenceFieldName = Nothing
        Me.txtSuppliestNMG.ReferenceTableName = Nothing
        Me.txtSuppliestNMG.Size = New System.Drawing.Size(160, 20)
        Me.txtSuppliestNMG.TabIndex = 1456
        Me.txtSuppliestNMG.Text = "0"
        Me.txtSuppliestNMG.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSuppliestNMG.Value = 0R
        '
        'txtLocalMilkMarketing
        '
        Me.txtLocalMilkMarketing.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtLocalMilkMarketing.CalculationExpression = Nothing
        Me.txtLocalMilkMarketing.DecimalPlaces = 5
        Me.txtLocalMilkMarketing.FieldCode = Nothing
        Me.txtLocalMilkMarketing.FieldDesc = Nothing
        Me.txtLocalMilkMarketing.FieldMaxLength = 0
        Me.txtLocalMilkMarketing.FieldName = Nothing
        Me.txtLocalMilkMarketing.isCalculatedField = False
        Me.txtLocalMilkMarketing.IsSourceFromTable = False
        Me.txtLocalMilkMarketing.IsSourceFromValueList = False
        Me.txtLocalMilkMarketing.IsUnique = False
        Me.txtLocalMilkMarketing.Location = New System.Drawing.Point(313, 78)
        Me.txtLocalMilkMarketing.MendatroryField = False
        Me.txtLocalMilkMarketing.MyLinkLable1 = Nothing
        Me.txtLocalMilkMarketing.MyLinkLable2 = Nothing
        Me.txtLocalMilkMarketing.Name = "txtLocalMilkMarketing"
        Me.txtLocalMilkMarketing.ReferenceFieldDesc = Nothing
        Me.txtLocalMilkMarketing.ReferenceFieldName = Nothing
        Me.txtLocalMilkMarketing.ReferenceTableName = Nothing
        Me.txtLocalMilkMarketing.Size = New System.Drawing.Size(160, 20)
        Me.txtLocalMilkMarketing.TabIndex = 1455
        Me.txtLocalMilkMarketing.Text = "0"
        Me.txtLocalMilkMarketing.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtLocalMilkMarketing.Value = 0R
        '
        'txtMilkReceiptRMG
        '
        Me.txtMilkReceiptRMG.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtMilkReceiptRMG.CalculationExpression = Nothing
        Me.txtMilkReceiptRMG.DecimalPlaces = 5
        Me.txtMilkReceiptRMG.FieldCode = Nothing
        Me.txtMilkReceiptRMG.FieldDesc = Nothing
        Me.txtMilkReceiptRMG.FieldMaxLength = 0
        Me.txtMilkReceiptRMG.FieldName = Nothing
        Me.txtMilkReceiptRMG.isCalculatedField = False
        Me.txtMilkReceiptRMG.IsSourceFromTable = False
        Me.txtMilkReceiptRMG.IsSourceFromValueList = False
        Me.txtMilkReceiptRMG.IsUnique = False
        Me.txtMilkReceiptRMG.Location = New System.Drawing.Point(313, 56)
        Me.txtMilkReceiptRMG.MendatroryField = False
        Me.txtMilkReceiptRMG.MyLinkLable1 = Nothing
        Me.txtMilkReceiptRMG.MyLinkLable2 = Nothing
        Me.txtMilkReceiptRMG.Name = "txtMilkReceiptRMG"
        Me.txtMilkReceiptRMG.ReferenceFieldDesc = Nothing
        Me.txtMilkReceiptRMG.ReferenceFieldName = Nothing
        Me.txtMilkReceiptRMG.ReferenceTableName = Nothing
        Me.txtMilkReceiptRMG.Size = New System.Drawing.Size(160, 20)
        Me.txtMilkReceiptRMG.TabIndex = 1454
        Me.txtMilkReceiptRMG.Text = "0"
        Me.txtMilkReceiptRMG.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMilkReceiptRMG.Value = 0R
        '
        'txtMilkProcOwnDC
        '
        Me.txtMilkProcOwnDC.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtMilkProcOwnDC.CalculationExpression = Nothing
        Me.txtMilkProcOwnDC.DecimalPlaces = 5
        Me.txtMilkProcOwnDC.FieldCode = Nothing
        Me.txtMilkProcOwnDC.FieldDesc = Nothing
        Me.txtMilkProcOwnDC.FieldMaxLength = 0
        Me.txtMilkProcOwnDC.FieldName = Nothing
        Me.txtMilkProcOwnDC.isCalculatedField = False
        Me.txtMilkProcOwnDC.IsSourceFromTable = False
        Me.txtMilkProcOwnDC.IsSourceFromValueList = False
        Me.txtMilkProcOwnDC.IsUnique = False
        Me.txtMilkProcOwnDC.Location = New System.Drawing.Point(313, 34)
        Me.txtMilkProcOwnDC.MendatroryField = False
        Me.txtMilkProcOwnDC.MyLinkLable1 = Nothing
        Me.txtMilkProcOwnDC.MyLinkLable2 = Nothing
        Me.txtMilkProcOwnDC.Name = "txtMilkProcOwnDC"
        Me.txtMilkProcOwnDC.ReferenceFieldDesc = Nothing
        Me.txtMilkProcOwnDC.ReferenceFieldName = Nothing
        Me.txtMilkProcOwnDC.ReferenceTableName = Nothing
        Me.txtMilkProcOwnDC.Size = New System.Drawing.Size(160, 20)
        Me.txtMilkProcOwnDC.TabIndex = 1453
        Me.txtMilkProcOwnDC.Text = "0"
        Me.txtMilkProcOwnDC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMilkProcOwnDC.Value = 0R
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel11.Location = New System.Drawing.Point(587, 13)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(35, 18)
        Me.MyLabel11.TabIndex = 1067
        Me.MyLabel11.Text = "SNF%"
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(502, 13)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(34, 18)
        Me.MyLabel10.TabIndex = 1066
        Me.MyLabel10.Text = "FAT%"
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(10, 144)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(186, 16)
        Me.MyLabel9.TabIndex = 409
        Me.MyLabel9.Text = "Milk Issued For Fresh Milk Products"
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(10, 122)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(183, 16)
        Me.MyLabel8.TabIndex = 406
        Me.MyLabel8.Text = "Suppliest To RMG (INTER UNION)"
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(10, 100)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(155, 16)
        Me.MyLabel7.TabIndex = 403
        Me.MyLabel7.Text = "Suppliest To NMG (MD/DMS)"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(10, 78)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(109, 16)
        Me.MyLabel1.TabIndex = 400
        Me.MyLabel1.Text = "Local Milk Marketing"
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(10, 56)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(116, 16)
        Me.MyLabel6.TabIndex = 397
        Me.MyLabel6.Text = "Milk Receipt(RMG IN)"
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(258, 13)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(31, 18)
        Me.MyLabel4.TabIndex = 396
        Me.MyLabel4.Text = "UNIT"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(10, 34)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(170, 16)
        Me.MyLabel5.TabIndex = 0
        Me.MyLabel5.Text = "Milk Procurement From Own DC"
        '
        'txtReportingDate
        '
        Me.txtReportingDate.CalculationExpression = Nothing
        Me.txtReportingDate.CustomFormat = "dd/MM/yyyy"
        Me.txtReportingDate.Enabled = False
        Me.txtReportingDate.FieldCode = Nothing
        Me.txtReportingDate.FieldDesc = Nothing
        Me.txtReportingDate.FieldMaxLength = 0
        Me.txtReportingDate.FieldName = Nothing
        Me.txtReportingDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReportingDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtReportingDate.isCalculatedField = False
        Me.txtReportingDate.IsSourceFromTable = False
        Me.txtReportingDate.IsSourceFromValueList = False
        Me.txtReportingDate.IsUnique = False
        Me.txtReportingDate.Location = New System.Drawing.Point(765, 35)
        Me.txtReportingDate.MendatroryField = False
        Me.txtReportingDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtReportingDate.MyLinkLable1 = Nothing
        Me.txtReportingDate.MyLinkLable2 = Nothing
        Me.txtReportingDate.Name = "txtReportingDate"
        Me.txtReportingDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtReportingDate.ReferenceFieldDesc = Nothing
        Me.txtReportingDate.ReferenceFieldName = Nothing
        Me.txtReportingDate.ReferenceTableName = Nothing
        Me.txtReportingDate.Size = New System.Drawing.Size(126, 18)
        Me.txtReportingDate.TabIndex = 398
        Me.txtReportingDate.TabStop = False
        Me.txtReportingDate.Text = "08/02/2024"
        Me.txtReportingDate.Value = New Date(2024, 2, 8, 0, 0, 0, 0)
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Location = New System.Drawing.Point(673, 35)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(82, 18)
        Me.MyLabel3.TabIndex = 397
        Me.MyLabel3.Text = "Reporting Date"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(957, 11)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(97, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 394
        '
        'txtReportDate
        '
        Me.txtReportDate.CalculationExpression = Nothing
        Me.txtReportDate.CustomFormat = "dd/MM/yyyy"
        Me.txtReportDate.Enabled = False
        Me.txtReportDate.FieldCode = Nothing
        Me.txtReportDate.FieldDesc = Nothing
        Me.txtReportDate.FieldMaxLength = 0
        Me.txtReportDate.FieldName = Nothing
        Me.txtReportDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReportDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtReportDate.isCalculatedField = False
        Me.txtReportDate.IsSourceFromTable = False
        Me.txtReportDate.IsSourceFromValueList = False
        Me.txtReportDate.IsUnique = False
        Me.txtReportDate.Location = New System.Drawing.Point(764, 10)
        Me.txtReportDate.MendatroryField = False
        Me.txtReportDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtReportDate.MyLinkLable1 = Nothing
        Me.txtReportDate.MyLinkLable2 = Nothing
        Me.txtReportDate.Name = "txtReportDate"
        Me.txtReportDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtReportDate.ReferenceFieldDesc = Nothing
        Me.txtReportDate.ReferenceFieldName = Nothing
        Me.txtReportDate.ReferenceTableName = Nothing
        Me.txtReportDate.Size = New System.Drawing.Size(126, 18)
        Me.txtReportDate.TabIndex = 389
        Me.txtReportDate.TabStop = False
        Me.txtReportDate.Text = "07/02/2024"
        Me.txtReportDate.Value = New Date(2024, 2, 7, 0, 0, 0, 0)
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(85, 9)
        Me.txtDocNo.MendatroryField = False
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Nothing
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 32767
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(252, 20)
        Me.txtDocNo.TabIndex = 387
        Me.txtDocNo.Value = ""
        '
        'btnNew
        '
        Me.btnNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.Image = Global.XpertERPMIS.My.Resources.Resources._new
        Me.btnNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnNew.Location = New System.Drawing.Point(337, 9)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(20, 21)
        Me.btnNew.TabIndex = 388
        '
        'txt
        '
        Me.txt.FieldName = Nothing
        Me.txt.Location = New System.Drawing.Point(673, 11)
        Me.txt.Name = "txt"
        Me.txt.Size = New System.Drawing.Size(67, 18)
        Me.txt.TabIndex = 2
        Me.txt.Text = "Report Date"
        '
        'lblfromDate
        '
        Me.lblfromDate.FieldName = Nothing
        Me.lblfromDate.Location = New System.Drawing.Point(5, 9)
        Me.lblfromDate.Name = "lblfromDate"
        Me.lblfromDate.Size = New System.Drawing.Size(77, 18)
        Me.lblfromDate.TabIndex = 0
        Me.lblfromDate.Text = "Document No"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(61.0!, 26.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1066, 442)
        Me.RadPageViewPage2.Text = "Products"
        '
        'gv1
        '
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1.Name = "gv1"
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(1066, 442)
        Me.gv1.TabIndex = 265
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.txtTableButter)
        Me.RadPageViewPage3.Controls.Add(Me.txtSMPReceipt)
        Me.RadPageViewPage3.Controls.Add(Me.txtSMPPurchase)
        Me.RadPageViewPage3.Controls.Add(Me.txtGheeReceipt)
        Me.RadPageViewPage3.Controls.Add(Me.txtGheePurchase)
        Me.RadPageViewPage3.Controls.Add(Me.txtRmrks)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel29)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel28)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel27)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel26)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel25)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel24)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel23)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(51.0!, 26.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(1066, 442)
        Me.RadPageViewPage3.Text = "Details"
        '
        'txtTableButter
        '
        Me.txtTableButter.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtTableButter.CalculationExpression = Nothing
        Me.txtTableButter.DecimalPlaces = 5
        Me.txtTableButter.FieldCode = Nothing
        Me.txtTableButter.FieldDesc = Nothing
        Me.txtTableButter.FieldMaxLength = 0
        Me.txtTableButter.FieldName = Nothing
        Me.txtTableButter.isCalculatedField = False
        Me.txtTableButter.IsSourceFromTable = False
        Me.txtTableButter.IsSourceFromValueList = False
        Me.txtTableButter.IsUnique = False
        Me.txtTableButter.Location = New System.Drawing.Point(257, 141)
        Me.txtTableButter.MendatroryField = False
        Me.txtTableButter.MyLinkLable1 = Nothing
        Me.txtTableButter.MyLinkLable2 = Nothing
        Me.txtTableButter.Name = "txtTableButter"
        Me.txtTableButter.ReferenceFieldDesc = Nothing
        Me.txtTableButter.ReferenceFieldName = Nothing
        Me.txtTableButter.ReferenceTableName = Nothing
        Me.txtTableButter.Size = New System.Drawing.Size(212, 20)
        Me.txtTableButter.TabIndex = 1458
        Me.txtTableButter.Text = "0"
        Me.txtTableButter.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTableButter.Value = 0R
        '
        'txtSMPReceipt
        '
        Me.txtSMPReceipt.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtSMPReceipt.CalculationExpression = Nothing
        Me.txtSMPReceipt.DecimalPlaces = 5
        Me.txtSMPReceipt.FieldCode = Nothing
        Me.txtSMPReceipt.FieldDesc = Nothing
        Me.txtSMPReceipt.FieldMaxLength = 0
        Me.txtSMPReceipt.FieldName = Nothing
        Me.txtSMPReceipt.isCalculatedField = False
        Me.txtSMPReceipt.IsSourceFromTable = False
        Me.txtSMPReceipt.IsSourceFromValueList = False
        Me.txtSMPReceipt.IsUnique = False
        Me.txtSMPReceipt.Location = New System.Drawing.Point(257, 118)
        Me.txtSMPReceipt.MendatroryField = False
        Me.txtSMPReceipt.MyLinkLable1 = Nothing
        Me.txtSMPReceipt.MyLinkLable2 = Nothing
        Me.txtSMPReceipt.Name = "txtSMPReceipt"
        Me.txtSMPReceipt.ReferenceFieldDesc = Nothing
        Me.txtSMPReceipt.ReferenceFieldName = Nothing
        Me.txtSMPReceipt.ReferenceTableName = Nothing
        Me.txtSMPReceipt.Size = New System.Drawing.Size(212, 20)
        Me.txtSMPReceipt.TabIndex = 1457
        Me.txtSMPReceipt.Text = "0"
        Me.txtSMPReceipt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSMPReceipt.Value = 0R
        '
        'txtSMPPurchase
        '
        Me.txtSMPPurchase.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtSMPPurchase.CalculationExpression = Nothing
        Me.txtSMPPurchase.DecimalPlaces = 5
        Me.txtSMPPurchase.FieldCode = Nothing
        Me.txtSMPPurchase.FieldDesc = Nothing
        Me.txtSMPPurchase.FieldMaxLength = 0
        Me.txtSMPPurchase.FieldName = Nothing
        Me.txtSMPPurchase.isCalculatedField = False
        Me.txtSMPPurchase.IsSourceFromTable = False
        Me.txtSMPPurchase.IsSourceFromValueList = False
        Me.txtSMPPurchase.IsUnique = False
        Me.txtSMPPurchase.Location = New System.Drawing.Point(257, 94)
        Me.txtSMPPurchase.MendatroryField = False
        Me.txtSMPPurchase.MyLinkLable1 = Nothing
        Me.txtSMPPurchase.MyLinkLable2 = Nothing
        Me.txtSMPPurchase.Name = "txtSMPPurchase"
        Me.txtSMPPurchase.ReferenceFieldDesc = Nothing
        Me.txtSMPPurchase.ReferenceFieldName = Nothing
        Me.txtSMPPurchase.ReferenceTableName = Nothing
        Me.txtSMPPurchase.Size = New System.Drawing.Size(212, 20)
        Me.txtSMPPurchase.TabIndex = 1456
        Me.txtSMPPurchase.Text = "0"
        Me.txtSMPPurchase.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSMPPurchase.Value = 0R
        '
        'txtGheeReceipt
        '
        Me.txtGheeReceipt.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtGheeReceipt.CalculationExpression = Nothing
        Me.txtGheeReceipt.DecimalPlaces = 5
        Me.txtGheeReceipt.FieldCode = Nothing
        Me.txtGheeReceipt.FieldDesc = Nothing
        Me.txtGheeReceipt.FieldMaxLength = 0
        Me.txtGheeReceipt.FieldName = Nothing
        Me.txtGheeReceipt.isCalculatedField = False
        Me.txtGheeReceipt.IsSourceFromTable = False
        Me.txtGheeReceipt.IsSourceFromValueList = False
        Me.txtGheeReceipt.IsUnique = False
        Me.txtGheeReceipt.Location = New System.Drawing.Point(257, 70)
        Me.txtGheeReceipt.MendatroryField = False
        Me.txtGheeReceipt.MyLinkLable1 = Nothing
        Me.txtGheeReceipt.MyLinkLable2 = Nothing
        Me.txtGheeReceipt.Name = "txtGheeReceipt"
        Me.txtGheeReceipt.ReferenceFieldDesc = Nothing
        Me.txtGheeReceipt.ReferenceFieldName = Nothing
        Me.txtGheeReceipt.ReferenceTableName = Nothing
        Me.txtGheeReceipt.Size = New System.Drawing.Size(212, 20)
        Me.txtGheeReceipt.TabIndex = 1455
        Me.txtGheeReceipt.Text = "0"
        Me.txtGheeReceipt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtGheeReceipt.Value = 0R
        '
        'txtGheePurchase
        '
        Me.txtGheePurchase.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtGheePurchase.CalculationExpression = Nothing
        Me.txtGheePurchase.DecimalPlaces = 5
        Me.txtGheePurchase.FieldCode = Nothing
        Me.txtGheePurchase.FieldDesc = Nothing
        Me.txtGheePurchase.FieldMaxLength = 0
        Me.txtGheePurchase.FieldName = Nothing
        Me.txtGheePurchase.isCalculatedField = False
        Me.txtGheePurchase.IsSourceFromTable = False
        Me.txtGheePurchase.IsSourceFromValueList = False
        Me.txtGheePurchase.IsUnique = False
        Me.txtGheePurchase.Location = New System.Drawing.Point(257, 47)
        Me.txtGheePurchase.MendatroryField = False
        Me.txtGheePurchase.MyLinkLable1 = Nothing
        Me.txtGheePurchase.MyLinkLable2 = Nothing
        Me.txtGheePurchase.Name = "txtGheePurchase"
        Me.txtGheePurchase.ReferenceFieldDesc = Nothing
        Me.txtGheePurchase.ReferenceFieldName = Nothing
        Me.txtGheePurchase.ReferenceTableName = Nothing
        Me.txtGheePurchase.Size = New System.Drawing.Size(212, 20)
        Me.txtGheePurchase.TabIndex = 1454
        Me.txtGheePurchase.Text = "0"
        Me.txtGheePurchase.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtGheePurchase.Value = 0R
        '
        'txtRmrks
        '
        Me.txtRmrks.CalculationExpression = Nothing
        Me.txtRmrks.FieldCode = Nothing
        Me.txtRmrks.FieldDesc = Nothing
        Me.txtRmrks.FieldMaxLength = 0
        Me.txtRmrks.FieldName = Nothing
        Me.txtRmrks.isCalculatedField = False
        Me.txtRmrks.IsSourceFromTable = False
        Me.txtRmrks.IsSourceFromValueList = False
        Me.txtRmrks.IsUnique = False
        Me.txtRmrks.Location = New System.Drawing.Point(77, 185)
        Me.txtRmrks.Margin = New System.Windows.Forms.Padding(2)
        Me.txtRmrks.MendatroryField = False
        Me.txtRmrks.MyLinkLable1 = Nothing
        Me.txtRmrks.MyLinkLable2 = Nothing
        Me.txtRmrks.Name = "txtRmrks"
        Me.txtRmrks.ReferenceFieldDesc = Nothing
        Me.txtRmrks.ReferenceFieldName = Nothing
        Me.txtRmrks.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtRmrks.RootElement.StretchVertically = True
        Me.txtRmrks.Size = New System.Drawing.Size(392, 88)
        Me.txtRmrks.TabIndex = 409
        '
        'MyLabel29
        '
        Me.MyLabel29.FieldName = Nothing
        Me.MyLabel29.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel29.Location = New System.Drawing.Point(3, 186)
        Me.MyLabel29.Name = "MyLabel29"
        Me.MyLabel29.Size = New System.Drawing.Size(59, 18)
        Me.MyLabel29.TabIndex = 408
        Me.MyLabel29.Text = "Remarks :-"
        '
        'MyLabel28
        '
        Me.MyLabel28.FieldName = Nothing
        Me.MyLabel28.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel28.Location = New System.Drawing.Point(3, 143)
        Me.MyLabel28.Name = "MyLabel28"
        Me.MyLabel28.Size = New System.Drawing.Size(178, 18)
        Me.MyLabel28.TabIndex = 406
        Me.MyLabel28.Text = "5. Table Butter Receipt Inter Union"
        '
        'MyLabel27
        '
        Me.MyLabel27.FieldName = Nothing
        Me.MyLabel27.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel27.Location = New System.Drawing.Point(3, 119)
        Me.MyLabel27.Name = "MyLabel27"
        Me.MyLabel27.Size = New System.Drawing.Size(232, 18)
        Me.MyLabel27.TabIndex = 404
        Me.MyLabel27.Text = "4. SMP Receipt Against Job Work/Conversion"
        '
        'MyLabel26
        '
        Me.MyLabel26.FieldName = Nothing
        Me.MyLabel26.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel26.Location = New System.Drawing.Point(3, 95)
        Me.MyLabel26.Name = "MyLabel26"
        Me.MyLabel26.Size = New System.Drawing.Size(177, 18)
        Me.MyLabel26.TabIndex = 402
        Me.MyLabel26.Text = "3. SMP Purchase From Inter Union"
        '
        'MyLabel25
        '
        Me.MyLabel25.FieldName = Nothing
        Me.MyLabel25.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel25.Location = New System.Drawing.Point(3, 71)
        Me.MyLabel25.Name = "MyLabel25"
        Me.MyLabel25.Size = New System.Drawing.Size(235, 18)
        Me.MyLabel25.TabIndex = 400
        Me.MyLabel25.Text = "2. Ghee Receipt Against Jon Work/Conversion"
        '
        'MyLabel24
        '
        Me.MyLabel24.FieldName = Nothing
        Me.MyLabel24.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel24.Location = New System.Drawing.Point(332, 24)
        Me.MyLabel24.Name = "MyLabel24"
        Me.MyLabel24.Size = New System.Drawing.Size(56, 18)
        Me.MyLabel24.TabIndex = 399
        Me.MyLabel24.Text = "UNIT : MT"
        '
        'MyLabel23
        '
        Me.MyLabel23.FieldName = Nothing
        Me.MyLabel23.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel23.Location = New System.Drawing.Point(3, 47)
        Me.MyLabel23.Name = "MyLabel23"
        Me.MyLabel23.Size = New System.Drawing.Size(168, 18)
        Me.MyLabel23.TabIndex = 397
        Me.MyLabel23.Text = "1. Ghee Purchase for Inter Union"
        '
        'btnReverse
        '
        Me.btnReverse.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReverse.Location = New System.Drawing.Point(216, 2)
        Me.btnReverse.Name = "btnReverse"
        Me.btnReverse.Size = New System.Drawing.Size(69, 20)
        Me.btnReverse.TabIndex = 388
        Me.btnReverse.Text = "Reverse"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Location = New System.Drawing.Point(147, 2)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 20)
        Me.btnDelete.TabIndex = 387
        Me.btnDelete.Text = "Delete"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Location = New System.Drawing.Point(76, 2)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(69, 20)
        Me.btnPost.TabIndex = 5
        Me.btnPost.Text = "Post"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(1011, 2)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(73, 20)
        Me.btnclose.TabIndex = 4
        Me.btnclose.Text = "Close"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(4, 2)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(70, 20)
        Me.btnsave.TabIndex = 3
        Me.btnsave.Text = "Save"
        '
        'frmDailyMilkProducts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1087, 517)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmDailyMilkProducts"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Daily Milk Products"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.MyTextBox16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.RadGroupBox4.PerformLayout()
        CType(Me.MyLabel30, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel41, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel42, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel43, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadDropDownList2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.MyLabel31, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel32, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel33, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel34, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel35, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel36, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadDropDownList1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMilkDispConv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRCDFunit5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRCDFunit4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRCDFunit3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRCDFunit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRCDFunit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRCDFunitName5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRCDFunitName4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRCDFunitName3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRCDFunitName2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRCDFunitName1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOwnMilkConv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMilkReceiveConversion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpGateEntryType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpGateEntryType.ResumeLayout(False)
        Me.grpGateEntryType.PerformLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddCreditCash, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSNF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFAT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMilkIssued, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSuppliestRMG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSuppliestNMG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLocalMilkMarketing, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMilkReceiptRMG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMilkProcOwnDC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtReportingDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtReportDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblfromDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage3.ResumeLayout(False)
        Me.RadPageViewPage3.PerformLayout()
        CType(Me.txtTableButter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSMPReceipt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSMPPurchase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGheeReceipt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGheePurchase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRmrks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel29, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel28, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel27, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel26, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents txt As common.Controls.MyLabel
    Friend WithEvents lblfromDate As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents btnDelete As RadButton
    Friend WithEvents btnPost As RadButton
    Friend WithEvents btnclose As RadButton
    Friend WithEvents btnsave As RadButton
    Friend WithEvents txtReportDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents btnNew As RadButton
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents RadPageViewPage2 As RadPageViewPage
    Friend WithEvents RadPageViewPage3 As RadPageViewPage
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtReportingDate As common.Controls.MyDateTimePicker
    Friend WithEvents grpGateEntryType As RadGroupBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox1 As RadGroupBox
    Friend WithEvents MyLabel18 As common.Controls.MyLabel
    Friend WithEvents MyLabel19 As common.Controls.MyLabel
    Friend WithEvents MyLabel21 As common.Controls.MyLabel
    Friend WithEvents MyLabel22 As common.Controls.MyLabel
    Friend WithEvents MyLabel24 As common.Controls.MyLabel
    Friend WithEvents MyLabel23 As common.Controls.MyLabel
    Friend WithEvents MyLabel28 As common.Controls.MyLabel
    Friend WithEvents MyLabel27 As common.Controls.MyLabel
    Friend WithEvents MyLabel26 As common.Controls.MyLabel
    Friend WithEvents MyLabel25 As common.Controls.MyLabel
    Friend WithEvents MyLabel29 As common.Controls.MyLabel
    Friend WithEvents txtRmrks As common.Controls.MyTextBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents txtMilkProcOwnDC As common.MyNumBox
    Friend WithEvents txtMilkIssued As common.MyNumBox
    Friend WithEvents txtSuppliestRMG As common.MyNumBox
    Friend WithEvents txtSuppliestNMG As common.MyNumBox
    Friend WithEvents txtLocalMilkMarketing As common.MyNumBox
    Friend WithEvents txtMilkReceiptRMG As common.MyNumBox
    Friend WithEvents txtOwnMilkConv As common.MyNumBox
    Friend WithEvents txtMilkReceiveConversion As common.MyNumBox
    Friend WithEvents txtRCDFunitName5 As common.Controls.MyTextBox
    Friend WithEvents txtRCDFunitName4 As common.Controls.MyTextBox
    Friend WithEvents txtRCDFunitName3 As common.Controls.MyTextBox
    Friend WithEvents txtRCDFunitName2 As common.Controls.MyTextBox
    Friend WithEvents txtRCDFunitName1 As common.Controls.MyTextBox
    Friend WithEvents txtMilkDispConv As common.MyNumBox
    Friend WithEvents txtRCDFunit5 As common.MyNumBox
    Friend WithEvents txtRCDFunit4 As common.MyNumBox
    Friend WithEvents txtRCDFunit3 As common.MyNumBox
    Friend WithEvents txtRCDFunit2 As common.MyNumBox
    Friend WithEvents txtRCDFunit1 As common.MyNumBox
    Friend WithEvents txtSNF As common.MyNumBox
    Friend WithEvents txtFAT As common.MyNumBox
    Friend WithEvents MyTextBox16 As common.Controls.MyTextBox
    Friend WithEvents txtGheePurchase As common.MyNumBox
    Friend WithEvents RadGroupBox2 As RadGroupBox
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents ddCreditCash As RadDropDownList
    Friend WithEvents MyLabel20 As common.Controls.MyLabel
    Friend WithEvents MyLabel17 As common.Controls.MyLabel
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox3 As RadGroupBox
    Friend WithEvents MyLabel31 As common.Controls.MyLabel
    Friend WithEvents MyLabel32 As common.Controls.MyLabel
    Friend WithEvents MyLabel33 As common.Controls.MyLabel
    Friend WithEvents MyLabel34 As common.Controls.MyLabel
    Friend WithEvents MyLabel35 As common.Controls.MyLabel
    Friend WithEvents MyLabel36 As common.Controls.MyLabel
    Friend WithEvents RadDropDownList1 As RadDropDownList
    Friend WithEvents RadGroupBox4 As RadGroupBox
    Friend WithEvents MyLabel41 As common.Controls.MyLabel
    Friend WithEvents MyLabel42 As common.Controls.MyLabel
    Friend WithEvents MyLabel43 As common.Controls.MyLabel
    Friend WithEvents RadDropDownList2 As RadDropDownList
    Friend WithEvents txtTableButter As common.MyNumBox
    Friend WithEvents txtSMPReceipt As common.MyNumBox
    Friend WithEvents txtSMPPurchase As common.MyNumBox
    Friend WithEvents txtGheeReceipt As common.MyNumBox
    Friend WithEvents MyLabel30 As common.Controls.MyLabel
    Friend WithEvents btnReverse As RadButton
End Class
