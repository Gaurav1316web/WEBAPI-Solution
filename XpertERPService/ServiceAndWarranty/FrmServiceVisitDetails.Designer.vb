Imports Common
Imports XpertERPEngine
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmServiceVisitDetails
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.GrpSerEnq = New System.Windows.Forms.GroupBox()
        Me.LblRoutedEng = New Common.Controls.MyLabel()
        Me.TxtRoutedEng = New Common.UserControls.txtFinder()
        Me.MyLabel2 = New Common.Controls.MyLabel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.LblCustGrp = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.LblDealerN = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LblDateofSale = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LblEnqD = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnShowDoc = New Telerik.WinControls.UI.RadButton()
        Me.BtnDeleteDoc = New Telerik.WinControls.UI.RadButton()
        Me.dtpSerDate = New Common.Controls.MyDateTimePicker()
        Me.MyLabel3 = New Common.Controls.MyLabel()
        Me.TxtSVRNo = New Common.Controls.MyTextBox()
        Me.MyLabel8 = New Common.Controls.MyLabel()
        Me.btnBrowse = New Telerik.WinControls.UI.RadButton()
        Me.txtBrowse = New Common.Controls.MyTextBox()
        Me.lblMCCCode = New Common.Controls.MyLabel()
        Me.TxtSerPlace = New Common.Controls.MyTextBox()
        Me.MyLabel1 = New Common.Controls.MyLabel()
        Me.MyLabel12 = New Common.Controls.MyLabel()
        Me.dtpDate = New Common.Controls.MyDateTimePicker()
        Me.LblEnqNo = New Common.Controls.MyLabel()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.TxtEnqNo = New Common.UserControls.txtFinder()
        Me.MyLabel44 = New Common.Controls.MyLabel()
        Me.txtcode = New Common.UserControls.txtNavigator()
        Me.MyLabel7 = New Common.Controls.MyLabel()
        Me.TxtRemarks = New Common.Controls.MyTextBox()
        Me.MyLabel19 = New Common.Controls.MyLabel()
        Me.CmbStatus = New Common.Controls.MyComboBox()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvMainItem = New Telerik.WinControls.UI.RadGridView()
        Me.RadPageViewPage4 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvChildItem = New Telerik.WinControls.UI.RadGridView()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.GrpSerEnq.SuspendLayout()
        CType(Me.LblRoutedEng, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.btnShowDoc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnDeleteDoc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpSerDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtSVRNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBrowse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBrowse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMCCCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtSerPlace, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblEnqNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel44, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage3.SuspendLayout()
        CType(Me.gvMainItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvMainItem.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage4.SuspendLayout()
        CType(Me.gvChildItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvChildItem.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(817, 451)
        Me.SplitContainer1.SplitterDistance = 408
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage4)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(817, 408)
        Me.RadPageView1.TabIndex = 158
        Me.RadPageView1.Text = "Compressor"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.GrpSerEnq)
        Me.RadPageViewPage1.Controls.Add(Me.GroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.lblMCCCode)
        Me.RadPageViewPage1.Controls.Add(Me.TxtSerPlace)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel12)
        Me.RadPageViewPage1.Controls.Add(Me.dtpDate)
        Me.RadPageViewPage1.Controls.Add(Me.LblEnqNo)
        Me.RadPageViewPage1.Controls.Add(Me.btnnew)
        Me.RadPageViewPage1.Controls.Add(Me.TxtEnqNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtcode)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel44)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel7)
        Me.RadPageViewPage1.Controls.Add(Me.TxtRemarks)
        Me.RadPageViewPage1.Controls.Add(Me.CmbStatus)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel19)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(92.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(796, 360)
        Me.RadPageViewPage1.Text = "General Details"
        '
        'GrpSerEnq
        '
        Me.GrpSerEnq.Controls.Add(Me.LblRoutedEng)
        Me.GrpSerEnq.Controls.Add(Me.TxtRoutedEng)
        Me.GrpSerEnq.Controls.Add(Me.MyLabel2)
        Me.GrpSerEnq.Controls.Add(Me.Label6)
        Me.GrpSerEnq.Controls.Add(Me.LblCustGrp)
        Me.GrpSerEnq.Controls.Add(Me.Label4)
        Me.GrpSerEnq.Controls.Add(Me.LblDealerN)
        Me.GrpSerEnq.Controls.Add(Me.Label2)
        Me.GrpSerEnq.Controls.Add(Me.LblDateofSale)
        Me.GrpSerEnq.Controls.Add(Me.Label1)
        Me.GrpSerEnq.Controls.Add(Me.LblEnqD)
        Me.GrpSerEnq.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GrpSerEnq.Location = New System.Drawing.Point(1, 109)
        Me.GrpSerEnq.Name = "GrpSerEnq"
        Me.GrpSerEnq.Size = New System.Drawing.Size(587, 132)
        Me.GrpSerEnq.TabIndex = 151
        Me.GrpSerEnq.TabStop = False
        Me.GrpSerEnq.Text = "Service Enquiry Details"
        '
        'LblRoutedEng
        '
        Me.LblRoutedEng.AutoSize = False
        Me.LblRoutedEng.BorderVisible = True
        Me.LblRoutedEng.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblRoutedEng.Location = New System.Drawing.Point(267, 99)
        Me.LblRoutedEng.Name = "LblRoutedEng"
        Me.LblRoutedEng.Size = New System.Drawing.Size(307, 18)
        Me.LblRoutedEng.TabIndex = 157
        Me.LblRoutedEng.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblRoutedEng.TextWrap = False
        '
        'TxtRoutedEng
        '
        Me.TxtRoutedEng.Location = New System.Drawing.Point(111, 99)
        Me.TxtRoutedEng.MendatroryField = True
        Me.TxtRoutedEng.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRoutedEng.MyLinkLable1 = Me.MyLabel2
        Me.TxtRoutedEng.MyLinkLable2 = Nothing
        Me.TxtRoutedEng.MyReadOnly = False
        Me.TxtRoutedEng.MyShowMasterFormButton = False
        Me.TxtRoutedEng.Name = "TxtRoutedEng"
        Me.TxtRoutedEng.Size = New System.Drawing.Size(146, 19)
        Me.TxtRoutedEng.TabIndex = 155
        Me.TxtRoutedEng.Value = ""
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(9, 100)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(104, 16)
        Me.MyLabel2.TabIndex = 156
        Me.MyLabel2.Text = "Routed Engineer :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(325, 67)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(106, 14)
        Me.Label6.TabIndex = 35
        Me.Label6.Text = "Customer Group :"
        '
        'LblCustGrp
        '
        Me.LblCustGrp.AutoSize = True
        Me.LblCustGrp.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCustGrp.Location = New System.Drawing.Point(437, 67)
        Me.LblCustGrp.Name = "LblCustGrp"
        Me.LblCustGrp.Size = New System.Drawing.Size(86, 14)
        Me.LblCustGrp.TabIndex = 34
        Me.LblCustGrp.Text = "CustomerGroup "
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(9, 67)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(82, 14)
        Me.Label4.TabIndex = 33
        Me.Label4.Text = "Dealer Name :"
        '
        'LblDealerN
        '
        Me.LblDealerN.AutoSize = True
        Me.LblDealerN.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDealerN.Location = New System.Drawing.Point(111, 67)
        Me.LblDealerN.Name = "LblDealerN"
        Me.LblDealerN.Size = New System.Drawing.Size(68, 14)
        Me.LblDealerN.TabIndex = 32
        Me.LblDealerN.Text = "DealerName "
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(353, 30)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 14)
        Me.Label2.TabIndex = 31
        Me.Label2.Text = "Date of Sale :"
        '
        'LblDateofSale
        '
        Me.LblDateofSale.AutoSize = True
        Me.LblDateofSale.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDateofSale.Location = New System.Drawing.Point(437, 30)
        Me.LblDateofSale.Name = "LblDateofSale"
        Me.LblDateofSale.Size = New System.Drawing.Size(60, 14)
        Me.LblDateofSale.TabIndex = 30
        Me.LblDateofSale.Text = "DateofSale"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(9, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 14)
        Me.Label1.TabIndex = 29
        Me.Label1.Text = "Enquiry Date :"
        '
        'LblEnqD
        '
        Me.LblEnqD.AutoSize = True
        Me.LblEnqD.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblEnqD.Location = New System.Drawing.Point(111, 30)
        Me.LblEnqD.Name = "LblEnqD"
        Me.LblEnqD.Size = New System.Drawing.Size(68, 14)
        Me.LblEnqD.TabIndex = 28
        Me.LblEnqD.Text = "EnquiryDate "
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnShowDoc)
        Me.GroupBox1.Controls.Add(Me.BtnDeleteDoc)
        Me.GroupBox1.Controls.Add(Me.dtpSerDate)
        Me.GroupBox1.Controls.Add(Me.MyLabel3)
        Me.GroupBox1.Controls.Add(Me.TxtSVRNo)
        Me.GroupBox1.Controls.Add(Me.MyLabel8)
        Me.GroupBox1.Controls.Add(Me.btnBrowse)
        Me.GroupBox1.Controls.Add(Me.txtBrowse)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(1, 275)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(587, 84)
        Me.GroupBox1.TabIndex = 157
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "SVR Details"
        '
        'btnShowDoc
        '
        Me.btnShowDoc.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnShowDoc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShowDoc.Location = New System.Drawing.Point(440, 50)
        Me.btnShowDoc.Name = "btnShowDoc"
        Me.btnShowDoc.Size = New System.Drawing.Size(66, 18)
        Me.btnShowDoc.TabIndex = 140
        Me.btnShowDoc.Text = "Show"
        '
        'BtnDeleteDoc
        '
        Me.BtnDeleteDoc.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnDeleteDoc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnDeleteDoc.Location = New System.Drawing.Point(509, 50)
        Me.BtnDeleteDoc.Name = "BtnDeleteDoc"
        Me.BtnDeleteDoc.Size = New System.Drawing.Size(66, 18)
        Me.BtnDeleteDoc.TabIndex = 141
        Me.BtnDeleteDoc.Text = "Delete"
        '
        'dtpSerDate
        '
        Me.dtpSerDate.CustomFormat = "dd/MM/yyyy "
        Me.dtpSerDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpSerDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpSerDate.Location = New System.Drawing.Point(487, 23)
        Me.dtpSerDate.MendatroryField = True
        Me.dtpSerDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpSerDate.MyLinkLable1 = Me.MyLabel3
        Me.dtpSerDate.MyLinkLable2 = Nothing
        Me.dtpSerDate.Name = "dtpSerDate"
        Me.dtpSerDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpSerDate.Size = New System.Drawing.Size(87, 18)
        Me.dtpSerDate.TabIndex = 138
        Me.dtpSerDate.TabStop = False
        Me.dtpSerDate.Text = "03/05/2011 "
        Me.dtpSerDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'MyLabel3
        '
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel3.Location = New System.Drawing.Point(411, 24)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(71, 16)
        Me.MyLabel3.TabIndex = 139
        Me.MyLabel3.Text = "Service Date"
        '
        'TxtSVRNo
        '
        Me.TxtSVRNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.TxtSVRNo.Location = New System.Drawing.Point(111, 22)
        Me.TxtSVRNo.MaxLength = 50
        Me.TxtSVRNo.MendatroryField = False
        Me.TxtSVRNo.MyLinkLable1 = Me.MyLabel8
        Me.TxtSVRNo.MyLinkLable2 = Nothing
        Me.TxtSVRNo.Name = "TxtSVRNo"
        Me.TxtSVRNo.Size = New System.Drawing.Size(149, 20)
        Me.TxtSVRNo.TabIndex = 136
        '
        'MyLabel8
        '
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(9, 24)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(50, 16)
        Me.MyLabel8.TabIndex = 137
        Me.MyLabel8.Text = "SVR No."
        '
        'btnBrowse
        '
        Me.btnBrowse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowse.Location = New System.Drawing.Point(9, 50)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(66, 18)
        Me.btnBrowse.TabIndex = 131
        Me.btnBrowse.Text = "Browse"
        '
        'txtBrowse
        '
        Me.txtBrowse.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtBrowse.Location = New System.Drawing.Point(111, 49)
        Me.txtBrowse.MendatroryField = False
        Me.txtBrowse.MyLinkLable1 = Nothing
        Me.txtBrowse.MyLinkLable2 = Nothing
        Me.txtBrowse.Name = "txtBrowse"
        Me.txtBrowse.Size = New System.Drawing.Size(323, 20)
        Me.txtBrowse.TabIndex = 132
        '
        'lblMCCCode
        '
        Me.lblMCCCode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblMCCCode.Location = New System.Drawing.Point(9, 3)
        Me.lblMCCCode.Name = "lblMCCCode"
        Me.lblMCCCode.Size = New System.Drawing.Size(33, 16)
        Me.lblMCCCode.TabIndex = 131
        Me.lblMCCCode.Text = "Code"
        '
        'TxtSerPlace
        '
        Me.TxtSerPlace.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.TxtSerPlace.Location = New System.Drawing.Point(111, 247)
        Me.TxtSerPlace.MaxLength = 250
        Me.TxtSerPlace.MendatroryField = False
        Me.TxtSerPlace.MyLinkLable1 = Me.MyLabel1
        Me.TxtSerPlace.MyLinkLable2 = Nothing
        Me.TxtSerPlace.Name = "TxtSerPlace"
        Me.TxtSerPlace.Size = New System.Drawing.Size(464, 20)
        Me.TxtSerPlace.TabIndex = 155
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(9, 249)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(75, 16)
        Me.MyLabel1.TabIndex = 156
        Me.MyLabel1.Text = "Service Place"
        '
        'MyLabel12
        '
        Me.MyLabel12.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel12.Location = New System.Drawing.Point(449, 3)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(30, 16)
        Me.MyLabel12.TabIndex = 132
        Me.MyLabel12.Text = "Date"
        '
        'dtpDate
        '
        Me.dtpDate.CustomFormat = "dd/MM/yyyy "
        Me.dtpDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDate.Location = New System.Drawing.Point(492, 2)
        Me.dtpDate.MendatroryField = True
        Me.dtpDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDate.MyLinkLable1 = Me.MyLabel12
        Me.dtpDate.MyLinkLable2 = Nothing
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDate.Size = New System.Drawing.Size(83, 18)
        Me.dtpDate.TabIndex = 130
        Me.dtpDate.TabStop = False
        Me.dtpDate.Text = "03/05/2011 "
        Me.dtpDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'LblEnqNo
        '
        Me.LblEnqNo.AutoSize = False
        Me.LblEnqNo.BorderVisible = True
        Me.LblEnqNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblEnqNo.Location = New System.Drawing.Point(268, 80)
        Me.LblEnqNo.Name = "LblEnqNo"
        Me.LblEnqNo.Size = New System.Drawing.Size(307, 18)
        Me.LblEnqNo.TabIndex = 150
        Me.LblEnqNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblEnqNo.TextWrap = False
        '
        'btnnew
        '
        Me.btnnew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnnew.Image = My.Resources._new
        Me.btnnew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnnew.Location = New System.Drawing.Point(312, 1)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(14, 21)
        Me.btnnew.TabIndex = 128
        '
        'TxtEnqNo
        '
        Me.TxtEnqNo.Location = New System.Drawing.Point(111, 80)
        Me.TxtEnqNo.MendatroryField = True
        Me.TxtEnqNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtEnqNo.MyLinkLable1 = Me.MyLabel44
        Me.TxtEnqNo.MyLinkLable2 = Nothing
        Me.TxtEnqNo.MyReadOnly = False
        Me.TxtEnqNo.MyShowMasterFormButton = False
        Me.TxtEnqNo.Name = "TxtEnqNo"
        Me.TxtEnqNo.Size = New System.Drawing.Size(151, 19)
        Me.TxtEnqNo.TabIndex = 148
        Me.TxtEnqNo.Value = ""
        '
        'MyLabel44
        '
        Me.MyLabel44.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel44.Location = New System.Drawing.Point(9, 81)
        Me.MyLabel44.Name = "MyLabel44"
        Me.MyLabel44.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel44.TabIndex = 149
        Me.MyLabel44.Text = "Enquiry No."
        '
        'txtcode
        '
        Me.txtcode.Location = New System.Drawing.Point(111, 1)
        Me.txtcode.MendatroryField = True
        Me.txtcode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtcode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtcode.MyLinkLable1 = Me.lblMCCCode
        Me.txtcode.MyLinkLable2 = Nothing
        Me.txtcode.MyMaxLength = 30
        Me.txtcode.MyReadOnly = False
        Me.txtcode.Name = "txtcode"
        Me.txtcode.Size = New System.Drawing.Size(199, 21)
        Me.txtcode.TabIndex = 129
        Me.txtcode.Value = ""
        '
        'MyLabel7
        '
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(9, 30)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(38, 16)
        Me.MyLabel7.TabIndex = 145
        Me.MyLabel7.Text = "Status"
        '
        'TxtRemarks
        '
        Me.TxtRemarks.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.TxtRemarks.Location = New System.Drawing.Point(111, 54)
        Me.TxtRemarks.MaxLength = 100
        Me.TxtRemarks.MendatroryField = False
        Me.TxtRemarks.MyLinkLable1 = Me.MyLabel19
        Me.TxtRemarks.MyLinkLable2 = Nothing
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(464, 20)
        Me.TxtRemarks.TabIndex = 146
        '
        'MyLabel19
        '
        Me.MyLabel19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel19.Location = New System.Drawing.Point(9, 56)
        Me.MyLabel19.Name = "MyLabel19"
        Me.MyLabel19.Size = New System.Drawing.Size(51, 16)
        Me.MyLabel19.TabIndex = 147
        Me.MyLabel19.Text = "Remarks"
        '
        'CmbStatus
        '
        Me.CmbStatus.AutoCompleteDisplayMember = Nothing
        Me.CmbStatus.AutoCompleteValueMember = Nothing
        Me.CmbStatus.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.CmbStatus.Location = New System.Drawing.Point(111, 28)
        Me.CmbStatus.MendatroryField = True
        Me.CmbStatus.MyLinkLable1 = Me.MyLabel7
        Me.CmbStatus.MyLinkLable2 = Nothing
        Me.CmbStatus.Name = "CmbStatus"
        Me.CmbStatus.Size = New System.Drawing.Size(150, 20)
        Me.CmbStatus.TabIndex = 144
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.gvMainItem)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(67.0!, 28.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(796, 403)
        Me.RadPageViewPage3.Text = "Main Item"
        '
        'gvMainItem
        '
        Me.gvMainItem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvMainItem.Location = New System.Drawing.Point(0, 0)
        Me.gvMainItem.Name = "gvMainItem"
        Me.gvMainItem.Size = New System.Drawing.Size(796, 403)
        Me.gvMainItem.TabIndex = 1
        '
        'RadPageViewPage4
        '
        Me.RadPageViewPage4.Controls.Add(Me.gvChildItem)
        Me.RadPageViewPage4.ItemSize = New System.Drawing.SizeF(72.0!, 28.0!)
        Me.RadPageViewPage4.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage4.Name = "RadPageViewPage4"
        Me.RadPageViewPage4.Size = New System.Drawing.Size(796, 403)
        Me.RadPageViewPage4.Text = "Child Items"
        '
        'gvChildItem
        '
        Me.gvChildItem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvChildItem.Location = New System.Drawing.Point(0, 0)
        Me.gvChildItem.Name = "gvChildItem"
        Me.gvChildItem.Size = New System.Drawing.Size(796, 403)
        Me.gvChildItem.TabIndex = 1
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(9, 8)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 3
        Me.btnsave.Text = "Save"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(82, 8)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 4
        Me.btndelete.Text = "Delete"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(741, 8)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 5
        Me.btnclose.Text = "Close"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'FrmServiceVisitDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(817, 451)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmServiceVisitDetails"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Service Visit Details"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        Me.GrpSerEnq.ResumeLayout(False)
        Me.GrpSerEnq.PerformLayout()
        CType(Me.LblRoutedEng, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.btnShowDoc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnDeleteDoc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpSerDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtSVRNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBrowse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBrowse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMCCCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtSerPlace, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblEnqNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel44, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbStatus, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage3.ResumeLayout(False)
        CType(Me.gvMainItem.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvMainItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage4.ResumeLayout(False)
        CType(Me.gvChildItem.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvChildItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblMCCCode As Common.Controls.MyLabel
    Friend WithEvents txtcode As Common.UserControls.txtNavigator
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents dtpDate As Common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel12 As Common.Controls.MyLabel
    Friend WithEvents CmbStatus As Common.Controls.MyComboBox
    Friend WithEvents MyLabel7 As Common.Controls.MyLabel
    Friend WithEvents TxtRemarks As Common.Controls.MyTextBox
    Friend WithEvents MyLabel19 As Common.Controls.MyLabel
    Friend WithEvents LblEnqNo As Common.Controls.MyLabel
    Friend WithEvents TxtEnqNo As Common.UserControls.txtFinder
    Friend WithEvents MyLabel44 As Common.Controls.MyLabel
    Friend WithEvents GrpSerEnq As System.Windows.Forms.GroupBox
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents LblCustGrp As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents LblDealerN As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents LblDateofSale As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents LblEnqD As System.Windows.Forms.Label
    Friend WithEvents TxtSerPlace As Common.Controls.MyTextBox
    Friend WithEvents MyLabel1 As Common.Controls.MyLabel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnShowDoc As Telerik.WinControls.UI.RadButton
    Friend WithEvents BtnDeleteDoc As Telerik.WinControls.UI.RadButton
    Friend WithEvents dtpSerDate As Common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel3 As Common.Controls.MyLabel
    Friend WithEvents TxtSVRNo As Common.Controls.MyTextBox
    Friend WithEvents MyLabel8 As Common.Controls.MyLabel
    Friend WithEvents btnBrowse As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtBrowse As Common.Controls.MyTextBox
    Friend WithEvents LblRoutedEng As Common.Controls.MyLabel
    Friend WithEvents TxtRoutedEng As Common.UserControls.txtFinder
    Friend WithEvents MyLabel2 As Common.Controls.MyLabel
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage3 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage4 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvChildItem As Telerik.WinControls.UI.RadGridView
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents gvMainItem As Telerik.WinControls.UI.RadGridView
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
End Class
