Imports XpertERPEngine
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSalesReturnNew
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSalesReturnNew))
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.txtshellqty = New common.MyNumBox()
        Me.txtInvoiceDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel40 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtKMReading = New common.Controls.MyTextBox()
        Me.RadLabel34 = New common.Controls.MyLabel()
        Me.txtSchemeSampleCode = New common.Controls.MyTextBox()
        Me.RadLabel35 = New common.Controls.MyLabel()
        Me.txtModeOfTransport = New common.Controls.MyTextBox()
        Me.RadLabel29 = New common.Controls.MyLabel()
        Me.RadLabel8 = New common.Controls.MyLabel()
        Me.lblVehicleNo = New common.Controls.MyLabel()
        Me.lblSalesman = New common.Controls.MyLabel()
        Me.txtVehicleNo = New common.UserControls.txtFinder()
        Me.RadLabel5 = New common.Controls.MyLabel()
        Me.txtSalesman = New common.UserControls.txtFinder()
        Me.RadLabel7 = New common.Controls.MyLabel()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.txtInvoiceNo = New common.UserControls.txtFinder()
        Me.RadLabel24 = New common.Controls.MyLabel()
        Me.RadLabel13 = New common.Controls.MyLabel()
        Me.RadLabel12 = New common.Controls.MyLabel()
        Me.lblRouteNo = New common.Controls.MyLabel()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.RadLabel14 = New common.Controls.MyLabel()
        Me.RadLabel6 = New common.Controls.MyLabel()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.RadLabel15 = New common.Controls.MyLabel()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.RadLabel23 = New common.Controls.MyLabel()
        Me.lblCustomerName = New common.Controls.MyLabel()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.txtRefNo = New common.Controls.MyTextBox()
        Me.txtPriceCode = New common.Controls.MyTextBox()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.txtRemarks = New common.Controls.MyTextBox()
        Me.txtCustomerNo = New common.UserControls.txtFinder()
        Me.txtRouteNo = New common.UserControls.txtFinder()
        Me.txtLocation = New common.UserControls.txtFinder()
        Me.UsLock1 = New common.usLock()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.txtCustomerPONO = New common.Controls.MyTextBox()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.txtTaxGroup = New common.UserControls.txtFinder()
        Me.RadLabel11 = New common.Controls.MyLabel()
        Me.lblTaxGrpName = New common.Controls.MyLabel()
        Me.RadLabel10 = New common.Controls.MyLabel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtTermCode = New common.UserControls.txtFinder()
        Me.RadLabel16 = New common.Controls.MyLabel()
        Me.lblTermName = New common.Controls.MyLabel()
        Me.txtDueDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel17 = New common.Controls.MyLabel()
        Me.gv2 = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage4 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.BntContainerDeposit = New Telerik.WinControls.UI.RadButton()
        Me.btnReverseAndRecreate = New Telerik.WinControls.UI.RadButton()
        Me.btnRecreateJournalEntry = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lblNetAmt = New common.Controls.MyLabel()
        Me.lblEmptyValue = New common.Controls.MyLabel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblCustAccount = New common.Controls.MyLabel()
        Me.RadLabel26 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.lblAdditionalCharges = New common.Controls.MyLabel()
        Me.lblLevel1_User_code = New common.Controls.MyLabel()
        Me.RadLabel20 = New common.Controls.MyLabel()
        Me.RadLabel53 = New common.Controls.MyLabel()
        Me.RadLabel52 = New common.Controls.MyLabel()
        Me.lblLevel2_User_code = New common.Controls.MyLabel()
        Me.lblLevel3_User_code = New common.Controls.MyLabel()
        Me.lblLevel4_User_code = New common.Controls.MyLabel()
        Me.RadLabel9 = New common.Controls.MyLabel()
        Me.RadLabel28 = New common.Controls.MyLabel()
        Me.lblLevel5_User_code = New common.Controls.MyLabel()
        Me.lblDetailDiscountAmt = New common.Controls.MyLabel()
        Me.lblLevel1_User_Commission = New common.Controls.MyLabel()
        Me.RadLabel47 = New common.Controls.MyLabel()
        Me.RadLabel18 = New common.Controls.MyLabel()
        Me.lblLevel2_User_Commission = New common.Controls.MyLabel()
        Me.RadLabel33 = New common.Controls.MyLabel()
        Me.lblOtherCharges = New common.Controls.MyLabel()
        Me.RadLabel45 = New common.Controls.MyLabel()
        Me.lblAssessableAmt = New common.Controls.MyLabel()
        Me.RadLabel27 = New common.Controls.MyLabel()
        Me.lblFreightAmount = New common.Controls.MyLabel()
        Me.RadLabel19 = New common.Controls.MyLabel()
        Me.lblLevel3_User_Commission = New common.Controls.MyLabel()
        Me.RadLabel36 = New common.Controls.MyLabel()
        Me.RadLabel43 = New common.Controls.MyLabel()
        Me.RadLabel42 = New common.Controls.MyLabel()
        Me.lblLevel5_User_Commission = New common.Controls.MyLabel()
        Me.lblLevel4_User_Commission = New common.Controls.MyLabel()
        Me.RadLabel39 = New common.Controls.MyLabel()
        Me.RadLabel37 = New common.Controls.MyLabel()
        Me.lblTPT = New common.Controls.MyLabel()
        Me.RadLabel32 = New common.Controls.MyLabel()
        Me.lblTotAmt = New common.Controls.MyLabel()
        Me.RadLabel30 = New common.Controls.MyLabel()
        Me.lblDetaolTotAmt = New common.Controls.MyLabel()
        Me.RadLabel25 = New common.Controls.MyLabel()
        Me.lblTaxAmt = New common.Controls.MyLabel()
        Me.lblDiscountAmt = New common.Controls.MyLabel()
        Me.RadLabel22 = New common.Controls.MyLabel()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnViewTDSDetails = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.txtshellqty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtInvoiceDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel40, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtKMReading, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel34, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSchemeSampleCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel35, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtModeOfTransport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel29, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVehicleNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSalesman, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRouteNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomerName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRefNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPriceCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustomerPONO, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.RadLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTaxGrpName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.RadLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTermName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDueDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage4.SuspendLayout()
        CType(Me.BntContainerDeposit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReverseAndRecreate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRecreateJournalEntry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNetAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmptyValue, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.lblCustAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel26, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAdditionalCharges, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLevel1_User_code, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel53, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel52, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLevel2_User_code, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLevel3_User_code, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLevel4_User_code, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel28, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLevel5_User_code, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDetailDiscountAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLevel1_User_Commission, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel47, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLevel2_User_Commission, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel33, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblOtherCharges, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel45, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAssessableAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel27, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFreightAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLevel3_User_Commission, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel36, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel43, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel42, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLevel5_User_Commission, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLevel4_User_Commission, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel39, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel37, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTPT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel32, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel30, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDetaolTotAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTaxAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDiscountAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnViewTDSDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnViewTDSDetails)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(978, 437)
        Me.SplitContainer1.SplitterDistance = 403
        Me.SplitContainer1.TabIndex = 1
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage4)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(978, 403)
        Me.RadPageView1.TabIndex = 0
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.txtshellqty)
        Me.RadPageViewPage1.Controls.Add(Me.txtInvoiceDate)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.txtKMReading)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel40)
        Me.RadPageViewPage1.Controls.Add(Me.txtSchemeSampleCode)
        Me.RadPageViewPage1.Controls.Add(Me.txtModeOfTransport)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel8)
        Me.RadPageViewPage1.Controls.Add(Me.lblVehicleNo)
        Me.RadPageViewPage1.Controls.Add(Me.lblSalesman)
        Me.RadPageViewPage1.Controls.Add(Me.txtVehicleNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtSalesman)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel7)
        Me.RadPageViewPage1.Controls.Add(Me.txtDate)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel35)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel34)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel29)
        Me.RadPageViewPage1.Controls.Add(Me.txtInvoiceNo)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel24)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel13)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel12)
        Me.RadPageViewPage1.Controls.Add(Me.lblRouteNo)
        Me.RadPageViewPage1.Controls.Add(Me.lblLocation)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel14)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel6)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel15)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel23)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.lblCustomerName)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.btnAddNew)
        Me.RadPageViewPage1.Controls.Add(Me.txtRefNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtPriceCode)
        Me.RadPageViewPage1.Controls.Add(Me.txtDescription)
        Me.RadPageViewPage1.Controls.Add(Me.txtRemarks)
        Me.RadPageViewPage1.Controls.Add(Me.txtCustomerNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtRouteNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtLocation)
        Me.RadPageViewPage1.Controls.Add(Me.UsLock1)
        Me.RadPageViewPage1.Controls.Add(Me.txtDocNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtCustomerPONO)
        Me.RadPageViewPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(82.0!, 26.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(957, 357)
        Me.RadPageViewPage1.Text = "Sales Return"
        '
        'txtshellqty
        '
        Me.txtshellqty.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtshellqty.CalculationExpression = Nothing
        Me.txtshellqty.DecimalPlaces = 0
        Me.txtshellqty.FieldCode = Nothing
        Me.txtshellqty.FieldDesc = Nothing
        Me.txtshellqty.FieldMaxLength = 0
        Me.txtshellqty.FieldName = Nothing
        Me.txtshellqty.isCalculatedField = False
        Me.txtshellqty.IsSourceFromTable = False
        Me.txtshellqty.IsSourceFromValueList = False
        Me.txtshellqty.IsUnique = False
        Me.txtshellqty.Location = New System.Drawing.Point(299, 116)
        Me.txtshellqty.MendatroryField = True
        Me.txtshellqty.MyLinkLable1 = Nothing
        Me.txtshellqty.MyLinkLable2 = Nothing
        Me.txtshellqty.Name = "txtshellqty"
        Me.txtshellqty.ReferenceFieldDesc = Nothing
        Me.txtshellqty.ReferenceFieldName = Nothing
        Me.txtshellqty.ReferenceTableName = Nothing
        Me.txtshellqty.Size = New System.Drawing.Size(39, 20)
        Me.txtshellqty.TabIndex = 45
        Me.txtshellqty.Text = "0"
        Me.txtshellqty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtshellqty.Value = 0R
        '
        'txtInvoiceDate
        '
        Me.txtInvoiceDate.CalculationExpression = Nothing
        Me.txtInvoiceDate.CustomFormat = "dd/MM/yyyy"
        Me.txtInvoiceDate.Enabled = False
        Me.txtInvoiceDate.FieldCode = Nothing
        Me.txtInvoiceDate.FieldDesc = Nothing
        Me.txtInvoiceDate.FieldMaxLength = 0
        Me.txtInvoiceDate.FieldName = Nothing
        Me.txtInvoiceDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvoiceDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtInvoiceDate.isCalculatedField = False
        Me.txtInvoiceDate.IsSourceFromTable = False
        Me.txtInvoiceDate.IsSourceFromValueList = False
        Me.txtInvoiceDate.IsUnique = False
        Me.txtInvoiceDate.Location = New System.Drawing.Point(828, 117)
        Me.txtInvoiceDate.MendatroryField = False
        Me.txtInvoiceDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtInvoiceDate.MyLinkLable1 = Me.RadLabel40
        Me.txtInvoiceDate.MyLinkLable2 = Nothing
        Me.txtInvoiceDate.Name = "txtInvoiceDate"
        Me.txtInvoiceDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtInvoiceDate.ReferenceFieldDesc = Nothing
        Me.txtInvoiceDate.ReferenceFieldName = Nothing
        Me.txtInvoiceDate.ReferenceTableName = Nothing
        Me.txtInvoiceDate.Size = New System.Drawing.Size(79, 18)
        Me.txtInvoiceDate.TabIndex = 17
        Me.txtInvoiceDate.TabStop = False
        Me.txtInvoiceDate.Text = "13/06/2011"
        Me.txtInvoiceDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel40
        '
        Me.RadLabel40.FieldName = Nothing
        Me.RadLabel40.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel40.Location = New System.Drawing.Point(760, 118)
        Me.RadLabel40.Name = "RadLabel40"
        Me.RadLabel40.Size = New System.Drawing.Size(69, 16)
        Me.RadLabel40.TabIndex = 41
        Me.RadLabel40.Text = "Invoice Date"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(241, 118)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(51, 18)
        Me.MyLabel2.TabIndex = 44
        Me.MyLabel2.Text = "Shell Qty"
        '
        'txtKMReading
        '
        Me.txtKMReading.CalculationExpression = Nothing
        Me.txtKMReading.Enabled = False
        Me.txtKMReading.FieldCode = Nothing
        Me.txtKMReading.FieldDesc = Nothing
        Me.txtKMReading.FieldMaxLength = 0
        Me.txtKMReading.FieldName = Nothing
        Me.txtKMReading.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtKMReading.isCalculatedField = False
        Me.txtKMReading.IsSourceFromTable = False
        Me.txtKMReading.IsSourceFromValueList = False
        Me.txtKMReading.IsUnique = False
        Me.txtKMReading.Location = New System.Drawing.Point(97, 117)
        Me.txtKMReading.MaxLength = 50
        Me.txtKMReading.MendatroryField = False
        Me.txtKMReading.MyLinkLable1 = Me.RadLabel34
        Me.txtKMReading.MyLinkLable2 = Nothing
        Me.txtKMReading.Name = "txtKMReading"
        Me.txtKMReading.ReferenceFieldDesc = Nothing
        Me.txtKMReading.ReferenceFieldName = Nothing
        Me.txtKMReading.ReferenceTableName = Nothing
        Me.txtKMReading.Size = New System.Drawing.Size(124, 18)
        Me.txtKMReading.TabIndex = 15
        '
        'RadLabel34
        '
        Me.RadLabel34.FieldName = Nothing
        Me.RadLabel34.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel34.Location = New System.Drawing.Point(3, 117)
        Me.RadLabel34.Name = "RadLabel34"
        Me.RadLabel34.Size = New System.Drawing.Size(69, 16)
        Me.RadLabel34.TabIndex = 26
        Me.RadLabel34.Text = "Km Reading"
        '
        'txtSchemeSampleCode
        '
        Me.txtSchemeSampleCode.CalculationExpression = Nothing
        Me.txtSchemeSampleCode.Enabled = False
        Me.txtSchemeSampleCode.FieldCode = Nothing
        Me.txtSchemeSampleCode.FieldDesc = Nothing
        Me.txtSchemeSampleCode.FieldMaxLength = 0
        Me.txtSchemeSampleCode.FieldName = Nothing
        Me.txtSchemeSampleCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSchemeSampleCode.isCalculatedField = False
        Me.txtSchemeSampleCode.IsSourceFromTable = False
        Me.txtSchemeSampleCode.IsSourceFromValueList = False
        Me.txtSchemeSampleCode.IsUnique = False
        Me.txtSchemeSampleCode.Location = New System.Drawing.Point(628, 117)
        Me.txtSchemeSampleCode.MaxLength = 50
        Me.txtSchemeSampleCode.MendatroryField = False
        Me.txtSchemeSampleCode.MyLinkLable1 = Me.RadLabel35
        Me.txtSchemeSampleCode.MyLinkLable2 = Nothing
        Me.txtSchemeSampleCode.Name = "txtSchemeSampleCode"
        Me.txtSchemeSampleCode.ReferenceFieldDesc = Nothing
        Me.txtSchemeSampleCode.ReferenceFieldName = Nothing
        Me.txtSchemeSampleCode.ReferenceTableName = Nothing
        Me.txtSchemeSampleCode.Size = New System.Drawing.Size(124, 18)
        Me.txtSchemeSampleCode.TabIndex = 16
        '
        'RadLabel35
        '
        Me.RadLabel35.FieldName = Nothing
        Me.RadLabel35.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel35.Location = New System.Drawing.Point(531, 118)
        Me.RadLabel35.Name = "RadLabel35"
        Me.RadLabel35.Size = New System.Drawing.Size(101, 16)
        Me.RadLabel35.TabIndex = 38
        Me.RadLabel35.Text = "Sch. Sample Code"
        '
        'txtModeOfTransport
        '
        Me.txtModeOfTransport.CalculationExpression = Nothing
        Me.txtModeOfTransport.Enabled = False
        Me.txtModeOfTransport.FieldCode = Nothing
        Me.txtModeOfTransport.FieldDesc = Nothing
        Me.txtModeOfTransport.FieldMaxLength = 0
        Me.txtModeOfTransport.FieldName = Nothing
        Me.txtModeOfTransport.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtModeOfTransport.isCalculatedField = False
        Me.txtModeOfTransport.IsSourceFromTable = False
        Me.txtModeOfTransport.IsSourceFromValueList = False
        Me.txtModeOfTransport.IsUnique = False
        Me.txtModeOfTransport.Location = New System.Drawing.Point(628, 98)
        Me.txtModeOfTransport.MaxLength = 50
        Me.txtModeOfTransport.MendatroryField = False
        Me.txtModeOfTransport.MyLinkLable1 = Me.RadLabel29
        Me.txtModeOfTransport.MyLinkLable2 = Nothing
        Me.txtModeOfTransport.Name = "txtModeOfTransport"
        Me.txtModeOfTransport.ReferenceFieldDesc = Nothing
        Me.txtModeOfTransport.ReferenceFieldName = Nothing
        Me.txtModeOfTransport.ReferenceTableName = Nothing
        Me.txtModeOfTransport.Size = New System.Drawing.Size(124, 18)
        Me.txtModeOfTransport.TabIndex = 13
        '
        'RadLabel29
        '
        Me.RadLabel29.FieldName = Nothing
        Me.RadLabel29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel29.Location = New System.Drawing.Point(531, 98)
        Me.RadLabel29.Name = "RadLabel29"
        Me.RadLabel29.Size = New System.Drawing.Size(99, 16)
        Me.RadLabel29.TabIndex = 37
        Me.RadLabel29.Text = "Mode of Transport"
        '
        'RadLabel8
        '
        Me.RadLabel8.FieldName = Nothing
        Me.RadLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel8.Location = New System.Drawing.Point(3, 80)
        Me.RadLabel8.Name = "RadLabel8"
        Me.RadLabel8.Size = New System.Drawing.Size(57, 16)
        Me.RadLabel8.TabIndex = 24
        Me.RadLabel8.Text = "Salesman"
        '
        'lblVehicleNo
        '
        Me.lblVehicleNo.AutoSize = False
        Me.lblVehicleNo.BorderVisible = True
        Me.lblVehicleNo.FieldName = Nothing
        Me.lblVehicleNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVehicleNo.Location = New System.Drawing.Point(241, 98)
        Me.lblVehicleNo.Name = "lblVehicleNo"
        Me.lblVehicleNo.Size = New System.Drawing.Size(289, 18)
        Me.lblVehicleNo.TabIndex = 31
        Me.lblVehicleNo.TextWrap = False
        '
        'lblSalesman
        '
        Me.lblSalesman.AutoSize = False
        Me.lblSalesman.BorderVisible = True
        Me.lblSalesman.FieldName = Nothing
        Me.lblSalesman.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSalesman.Location = New System.Drawing.Point(241, 79)
        Me.lblSalesman.Name = "lblSalesman"
        Me.lblSalesman.Size = New System.Drawing.Size(289, 18)
        Me.lblSalesman.TabIndex = 30
        Me.lblSalesman.TextWrap = False
        '
        'txtVehicleNo
        '
        Me.txtVehicleNo.CalculationExpression = Nothing
        Me.txtVehicleNo.Enabled = False
        Me.txtVehicleNo.FieldCode = Nothing
        Me.txtVehicleNo.FieldDesc = Nothing
        Me.txtVehicleNo.FieldMaxLength = 0
        Me.txtVehicleNo.FieldName = Nothing
        Me.txtVehicleNo.isCalculatedField = False
        Me.txtVehicleNo.IsSourceFromTable = False
        Me.txtVehicleNo.IsSourceFromValueList = False
        Me.txtVehicleNo.IsUnique = False
        Me.txtVehicleNo.Location = New System.Drawing.Point(97, 98)
        Me.txtVehicleNo.MendatroryField = False
        Me.txtVehicleNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVehicleNo.MyLinkLable1 = Me.RadLabel5
        Me.txtVehicleNo.MyLinkLable2 = Me.lblVehicleNo
        Me.txtVehicleNo.MyReadOnly = False
        Me.txtVehicleNo.MyShowMasterFormButton = False
        Me.txtVehicleNo.Name = "txtVehicleNo"
        Me.txtVehicleNo.ReferenceFieldDesc = Nothing
        Me.txtVehicleNo.ReferenceFieldName = Nothing
        Me.txtVehicleNo.ReferenceTableName = Nothing
        Me.txtVehicleNo.Size = New System.Drawing.Size(143, 18)
        Me.txtVehicleNo.TabIndex = 12
        Me.txtVehicleNo.Value = ""
        '
        'RadLabel5
        '
        Me.RadLabel5.FieldName = Nothing
        Me.RadLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel5.Location = New System.Drawing.Point(3, 101)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(61, 16)
        Me.RadLabel5.TabIndex = 25
        Me.RadLabel5.Text = "Vehicle No"
        '
        'txtSalesman
        '
        Me.txtSalesman.CalculationExpression = Nothing
        Me.txtSalesman.Enabled = False
        Me.txtSalesman.FieldCode = Nothing
        Me.txtSalesman.FieldDesc = Nothing
        Me.txtSalesman.FieldMaxLength = 0
        Me.txtSalesman.FieldName = Nothing
        Me.txtSalesman.isCalculatedField = False
        Me.txtSalesman.IsSourceFromTable = False
        Me.txtSalesman.IsSourceFromValueList = False
        Me.txtSalesman.IsUnique = False
        Me.txtSalesman.Location = New System.Drawing.Point(97, 79)
        Me.txtSalesman.MendatroryField = False
        Me.txtSalesman.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalesman.MyLinkLable1 = Me.RadLabel8
        Me.txtSalesman.MyLinkLable2 = Me.lblSalesman
        Me.txtSalesman.MyReadOnly = False
        Me.txtSalesman.MyShowMasterFormButton = False
        Me.txtSalesman.Name = "txtSalesman"
        Me.txtSalesman.ReferenceFieldDesc = Nothing
        Me.txtSalesman.ReferenceFieldName = Nothing
        Me.txtSalesman.ReferenceTableName = Nothing
        Me.txtSalesman.Size = New System.Drawing.Size(143, 18)
        Me.txtSalesman.TabIndex = 10
        Me.txtSalesman.Value = ""
        '
        'RadLabel7
        '
        Me.RadLabel7.FieldName = Nothing
        Me.RadLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel7.Location = New System.Drawing.Point(3, 61)
        Me.RadLabel7.Name = "RadLabel7"
        Me.RadLabel7.Size = New System.Drawing.Size(54, 16)
        Me.RadLabel7.TabIndex = 23
        Me.RadLabel7.Text = "Route No"
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
        Me.txtDate.Location = New System.Drawing.Point(406, 2)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.RadLabel4
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(126, 18)
        Me.txtDate.TabIndex = 2
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011 11:29 AM"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(375, 3)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 39
        Me.RadLabel4.Text = "Date"
        '
        'txtInvoiceNo
        '
        Me.txtInvoiceNo.CalculationExpression = Nothing
        Me.txtInvoiceNo.FieldCode = Nothing
        Me.txtInvoiceNo.FieldDesc = Nothing
        Me.txtInvoiceNo.FieldMaxLength = 0
        Me.txtInvoiceNo.FieldName = Nothing
        Me.txtInvoiceNo.isCalculatedField = False
        Me.txtInvoiceNo.IsSourceFromTable = False
        Me.txtInvoiceNo.IsSourceFromValueList = False
        Me.txtInvoiceNo.IsUnique = False
        Me.txtInvoiceNo.Location = New System.Drawing.Point(628, 2)
        Me.txtInvoiceNo.MendatroryField = True
        Me.txtInvoiceNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvoiceNo.MyLinkLable1 = Me.RadLabel24
        Me.txtInvoiceNo.MyLinkLable2 = Nothing
        Me.txtInvoiceNo.MyReadOnly = True
        Me.txtInvoiceNo.MyShowMasterFormButton = False
        Me.txtInvoiceNo.Name = "txtInvoiceNo"
        Me.txtInvoiceNo.ReferenceFieldDesc = Nothing
        Me.txtInvoiceNo.ReferenceFieldName = Nothing
        Me.txtInvoiceNo.ReferenceTableName = Nothing
        Me.txtInvoiceNo.Size = New System.Drawing.Size(154, 19)
        Me.txtInvoiceNo.TabIndex = 3
        Me.txtInvoiceNo.Value = ""
        '
        'RadLabel24
        '
        Me.RadLabel24.FieldName = Nothing
        Me.RadLabel24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel24.Location = New System.Drawing.Point(531, 3)
        Me.RadLabel24.Name = "RadLabel24"
        Me.RadLabel24.Size = New System.Drawing.Size(60, 16)
        Me.RadLabel24.TabIndex = 32
        Me.RadLabel24.Text = "Invoice No"
        '
        'RadLabel13
        '
        Me.RadLabel13.FieldName = Nothing
        Me.RadLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel13.Location = New System.Drawing.Point(531, 80)
        Me.RadLabel13.Name = "RadLabel13"
        Me.RadLabel13.Size = New System.Drawing.Size(51, 16)
        Me.RadLabel13.TabIndex = 36
        Me.RadLabel13.Text = "Remarks"
        '
        'RadLabel12
        '
        Me.RadLabel12.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadLabel12.FieldName = Nothing
        Me.RadLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel12.ForeColor = System.Drawing.SystemColors.Highlight
        Me.RadLabel12.Location = New System.Drawing.Point(636, 344)
        Me.RadLabel12.Name = "RadLabel12"
        Me.RadLabel12.Size = New System.Drawing.Size(318, 16)
        Me.RadLabel12.TabIndex = 19
        Me.RadLabel12.Text = "Double click on Tax Amount Column To Set Item wise Tax"
        Me.RadLabel12.Visible = False
        '
        'lblRouteNo
        '
        Me.lblRouteNo.AutoSize = False
        Me.lblRouteNo.BorderVisible = True
        Me.lblRouteNo.FieldName = Nothing
        Me.lblRouteNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRouteNo.Location = New System.Drawing.Point(241, 60)
        Me.lblRouteNo.Name = "lblRouteNo"
        Me.lblRouteNo.Size = New System.Drawing.Size(289, 18)
        Me.lblRouteNo.TabIndex = 29
        Me.lblRouteNo.TextWrap = False
        '
        'lblLocation
        '
        Me.lblLocation.AutoSize = False
        Me.lblLocation.BorderVisible = True
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(241, 41)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(289, 18)
        Me.lblLocation.TabIndex = 28
        Me.lblLocation.TextWrap = False
        '
        'RadLabel14
        '
        Me.RadLabel14.FieldName = Nothing
        Me.RadLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel14.Location = New System.Drawing.Point(531, 61)
        Me.RadLabel14.Name = "RadLabel14"
        Me.RadLabel14.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel14.TabIndex = 35
        Me.RadLabel14.Text = "Description"
        '
        'RadLabel6
        '
        Me.RadLabel6.FieldName = Nothing
        Me.RadLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel6.Location = New System.Drawing.Point(531, 42)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(76, 16)
        Me.RadLabel6.TabIndex = 34
        Me.RadLabel6.Text = "Reference No"
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(531, 23)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(92, 16)
        Me.RadLabel3.TabIndex = 33
        Me.RadLabel3.Text = "Customer PO No"
        '
        'RadLabel15
        '
        Me.RadLabel15.FieldName = Nothing
        Me.RadLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel15.Location = New System.Drawing.Point(3, 42)
        Me.RadLabel15.Name = "RadLabel15"
        Me.RadLabel15.Size = New System.Drawing.Size(49, 16)
        Me.RadLabel15.TabIndex = 22
        Me.RadLabel15.Text = "Location"
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
        Me.RadGroupBox2.Location = New System.Drawing.Point(2, 139)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(952, 204)
        Me.RadGroupBox2.TabIndex = 18
        Me.RadGroupBox2.Text = "Item Details"
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
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(932, 174)
        Me.gv1.TabIndex = 0
        '
        'RadLabel23
        '
        Me.RadLabel23.FieldName = Nothing
        Me.RadLabel23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel23.Location = New System.Drawing.Point(760, 102)
        Me.RadLabel23.Name = "RadLabel23"
        Me.RadLabel23.Size = New System.Drawing.Size(62, 16)
        Me.RadLabel23.TabIndex = 40
        Me.RadLabel23.Text = "Price Code"
        '
        'lblCustomerName
        '
        Me.lblCustomerName.AutoSize = False
        Me.lblCustomerName.BorderVisible = True
        Me.lblCustomerName.FieldName = Nothing
        Me.lblCustomerName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerName.Location = New System.Drawing.Point(241, 22)
        Me.lblCustomerName.Name = "lblCustomerName"
        Me.lblCustomerName.Size = New System.Drawing.Size(289, 18)
        Me.lblCustomerName.TabIndex = 27
        Me.lblCustomerName.TextWrap = False
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(3, 23)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(55, 16)
        Me.RadLabel2.TabIndex = 21
        Me.RadLabel2.Text = "Customer"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(3, 3)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(75, 16)
        Me.RadLabel1.TabIndex = 20
        Me.RadLabel1.Text = "Document No"
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = CType(resources.GetObject("btnAddNew.Image"), System.Drawing.Image)
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(349, 1)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 21)
        Me.btnAddNew.TabIndex = 1
        '
        'txtRefNo
        '
        Me.txtRefNo.CalculationExpression = Nothing
        Me.txtRefNo.FieldCode = Nothing
        Me.txtRefNo.FieldDesc = Nothing
        Me.txtRefNo.FieldMaxLength = 0
        Me.txtRefNo.FieldName = Nothing
        Me.txtRefNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefNo.isCalculatedField = False
        Me.txtRefNo.IsSourceFromTable = False
        Me.txtRefNo.IsSourceFromValueList = False
        Me.txtRefNo.IsUnique = False
        Me.txtRefNo.Location = New System.Drawing.Point(628, 41)
        Me.txtRefNo.MaxLength = 50
        Me.txtRefNo.MendatroryField = False
        Me.txtRefNo.MyLinkLable1 = Me.RadLabel6
        Me.txtRefNo.MyLinkLable2 = Nothing
        Me.txtRefNo.Name = "txtRefNo"
        Me.txtRefNo.ReferenceFieldDesc = Nothing
        Me.txtRefNo.ReferenceFieldName = Nothing
        Me.txtRefNo.ReferenceTableName = Nothing
        Me.txtRefNo.Size = New System.Drawing.Size(326, 18)
        Me.txtRefNo.TabIndex = 7
        '
        'txtPriceCode
        '
        Me.txtPriceCode.CalculationExpression = Nothing
        Me.txtPriceCode.Enabled = False
        Me.txtPriceCode.FieldCode = Nothing
        Me.txtPriceCode.FieldDesc = Nothing
        Me.txtPriceCode.FieldMaxLength = 0
        Me.txtPriceCode.FieldName = Nothing
        Me.txtPriceCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPriceCode.isCalculatedField = False
        Me.txtPriceCode.IsSourceFromTable = False
        Me.txtPriceCode.IsSourceFromValueList = False
        Me.txtPriceCode.IsUnique = False
        Me.txtPriceCode.Location = New System.Drawing.Point(828, 98)
        Me.txtPriceCode.MaxLength = 50
        Me.txtPriceCode.MendatroryField = False
        Me.txtPriceCode.MyLinkLable1 = Me.RadLabel23
        Me.txtPriceCode.MyLinkLable2 = Nothing
        Me.txtPriceCode.Name = "txtPriceCode"
        Me.txtPriceCode.ReferenceFieldDesc = Nothing
        Me.txtPriceCode.ReferenceFieldName = Nothing
        Me.txtPriceCode.ReferenceTableName = Nothing
        Me.txtPriceCode.Size = New System.Drawing.Size(124, 18)
        Me.txtPriceCode.TabIndex = 14
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
        Me.txtDescription.Location = New System.Drawing.Point(628, 60)
        Me.txtDescription.MaxLength = 200
        Me.txtDescription.MendatroryField = False
        Me.txtDescription.MyLinkLable1 = Me.RadLabel14
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReferenceFieldDesc = Nothing
        Me.txtDescription.ReferenceFieldName = Nothing
        Me.txtDescription.ReferenceTableName = Nothing
        Me.txtDescription.Size = New System.Drawing.Size(326, 18)
        Me.txtDescription.TabIndex = 9
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
        Me.txtRemarks.Location = New System.Drawing.Point(628, 79)
        Me.txtRemarks.MaxLength = 200
        Me.txtRemarks.MendatroryField = False
        Me.txtRemarks.MyLinkLable1 = Me.RadLabel13
        Me.txtRemarks.MyLinkLable2 = Nothing
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.ReferenceFieldDesc = Nothing
        Me.txtRemarks.ReferenceFieldName = Nothing
        Me.txtRemarks.ReferenceTableName = Nothing
        Me.txtRemarks.Size = New System.Drawing.Size(326, 18)
        Me.txtRemarks.TabIndex = 11
        '
        'txtCustomerNo
        '
        Me.txtCustomerNo.CalculationExpression = Nothing
        Me.txtCustomerNo.Enabled = False
        Me.txtCustomerNo.FieldCode = Nothing
        Me.txtCustomerNo.FieldDesc = Nothing
        Me.txtCustomerNo.FieldMaxLength = 0
        Me.txtCustomerNo.FieldName = Nothing
        Me.txtCustomerNo.isCalculatedField = False
        Me.txtCustomerNo.IsSourceFromTable = False
        Me.txtCustomerNo.IsSourceFromValueList = False
        Me.txtCustomerNo.IsUnique = False
        Me.txtCustomerNo.Location = New System.Drawing.Point(97, 22)
        Me.txtCustomerNo.MendatroryField = False
        Me.txtCustomerNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerNo.MyLinkLable1 = Me.RadLabel2
        Me.txtCustomerNo.MyLinkLable2 = Me.lblCustomerName
        Me.txtCustomerNo.MyReadOnly = False
        Me.txtCustomerNo.MyShowMasterFormButton = False
        Me.txtCustomerNo.Name = "txtCustomerNo"
        Me.txtCustomerNo.ReferenceFieldDesc = Nothing
        Me.txtCustomerNo.ReferenceFieldName = Nothing
        Me.txtCustomerNo.ReferenceTableName = Nothing
        Me.txtCustomerNo.Size = New System.Drawing.Size(143, 18)
        Me.txtCustomerNo.TabIndex = 4
        Me.txtCustomerNo.Value = ""
        '
        'txtRouteNo
        '
        Me.txtRouteNo.CalculationExpression = Nothing
        Me.txtRouteNo.Enabled = False
        Me.txtRouteNo.FieldCode = Nothing
        Me.txtRouteNo.FieldDesc = Nothing
        Me.txtRouteNo.FieldMaxLength = 0
        Me.txtRouteNo.FieldName = Nothing
        Me.txtRouteNo.isCalculatedField = False
        Me.txtRouteNo.IsSourceFromTable = False
        Me.txtRouteNo.IsSourceFromValueList = False
        Me.txtRouteNo.IsUnique = False
        Me.txtRouteNo.Location = New System.Drawing.Point(97, 60)
        Me.txtRouteNo.MendatroryField = False
        Me.txtRouteNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRouteNo.MyLinkLable1 = Me.RadLabel7
        Me.txtRouteNo.MyLinkLable2 = Me.lblRouteNo
        Me.txtRouteNo.MyReadOnly = False
        Me.txtRouteNo.MyShowMasterFormButton = False
        Me.txtRouteNo.Name = "txtRouteNo"
        Me.txtRouteNo.ReferenceFieldDesc = Nothing
        Me.txtRouteNo.ReferenceFieldName = Nothing
        Me.txtRouteNo.ReferenceTableName = Nothing
        Me.txtRouteNo.Size = New System.Drawing.Size(143, 18)
        Me.txtRouteNo.TabIndex = 8
        Me.txtRouteNo.Value = ""
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
        Me.txtLocation.Location = New System.Drawing.Point(97, 41)
        Me.txtLocation.MendatroryField = False
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.RadLabel15
        Me.txtLocation.MyLinkLable2 = Me.lblLocation
        Me.txtLocation.MyReadOnly = False
        Me.txtLocation.MyShowMasterFormButton = False
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.ReferenceFieldDesc = Nothing
        Me.txtLocation.ReferenceFieldName = Nothing
        Me.txtLocation.ReferenceTableName = Nothing
        Me.txtLocation.Size = New System.Drawing.Size(143, 18)
        Me.txtLocation.TabIndex = 6
        Me.txtLocation.Value = ""
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(851, 1)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(101, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 42
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(97, 1)
        Me.txtDocNo.MendatroryField = False
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Me.RadLabel1
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 30
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(252, 20)
        Me.txtDocNo.TabIndex = 0
        Me.txtDocNo.Value = ""
        '
        'txtCustomerPONO
        '
        Me.txtCustomerPONO.CalculationExpression = Nothing
        Me.txtCustomerPONO.Enabled = False
        Me.txtCustomerPONO.FieldCode = Nothing
        Me.txtCustomerPONO.FieldDesc = Nothing
        Me.txtCustomerPONO.FieldMaxLength = 0
        Me.txtCustomerPONO.FieldName = Nothing
        Me.txtCustomerPONO.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerPONO.isCalculatedField = False
        Me.txtCustomerPONO.IsSourceFromTable = False
        Me.txtCustomerPONO.IsSourceFromValueList = False
        Me.txtCustomerPONO.IsUnique = False
        Me.txtCustomerPONO.Location = New System.Drawing.Point(628, 22)
        Me.txtCustomerPONO.MaxLength = 200
        Me.txtCustomerPONO.MendatroryField = False
        Me.txtCustomerPONO.MyLinkLable1 = Me.RadLabel3
        Me.txtCustomerPONO.MyLinkLable2 = Nothing
        Me.txtCustomerPONO.Name = "txtCustomerPONO"
        Me.txtCustomerPONO.ReferenceFieldDesc = Nothing
        Me.txtCustomerPONO.ReferenceFieldName = Nothing
        Me.txtCustomerPONO.ReferenceTableName = Nothing
        Me.txtCustomerPONO.Size = New System.Drawing.Size(326, 18)
        Me.txtCustomerPONO.TabIndex = 5
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.txtTaxGroup)
        Me.RadPageViewPage2.Controls.Add(Me.RadLabel10)
        Me.RadPageViewPage2.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage2.Controls.Add(Me.gv2)
        Me.RadPageViewPage2.Controls.Add(Me.lblTaxGrpName)
        Me.RadPageViewPage2.Controls.Add(Me.RadLabel11)
        Me.RadPageViewPage2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(47.0!, 26.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(957, 357)
        Me.RadPageViewPage2.Text = "Taxes"
        '
        'txtTaxGroup
        '
        Me.txtTaxGroup.CalculationExpression = Nothing
        Me.txtTaxGroup.Enabled = False
        Me.txtTaxGroup.FieldCode = Nothing
        Me.txtTaxGroup.FieldDesc = Nothing
        Me.txtTaxGroup.FieldMaxLength = 0
        Me.txtTaxGroup.FieldName = Nothing
        Me.txtTaxGroup.isCalculatedField = False
        Me.txtTaxGroup.IsSourceFromTable = False
        Me.txtTaxGroup.IsSourceFromValueList = False
        Me.txtTaxGroup.IsUnique = False
        Me.txtTaxGroup.Location = New System.Drawing.Point(69, 3)
        Me.txtTaxGroup.MendatroryField = True
        Me.txtTaxGroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTaxGroup.MyLinkLable1 = Me.RadLabel11
        Me.txtTaxGroup.MyLinkLable2 = Me.lblTaxGrpName
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
        'RadLabel11
        '
        Me.RadLabel11.FieldName = Nothing
        Me.RadLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel11.Location = New System.Drawing.Point(3, 6)
        Me.RadLabel11.Name = "RadLabel11"
        Me.RadLabel11.Size = New System.Drawing.Size(60, 16)
        Me.RadLabel11.TabIndex = 4
        Me.RadLabel11.Text = "Tax Group"
        '
        'lblTaxGrpName
        '
        Me.lblTaxGrpName.AutoSize = False
        Me.lblTaxGrpName.BorderVisible = True
        Me.lblTaxGrpName.FieldName = Nothing
        Me.lblTaxGrpName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTaxGrpName.Location = New System.Drawing.Point(220, 3)
        Me.lblTaxGrpName.Name = "lblTaxGrpName"
        Me.lblTaxGrpName.Size = New System.Drawing.Size(321, 20)
        Me.lblTaxGrpName.TabIndex = 1
        Me.lblTaxGrpName.TextWrap = False
        Me.lblTaxGrpName.Visible = False
        '
        'RadLabel10
        '
        Me.RadLabel10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadLabel10.FieldName = Nothing
        Me.RadLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel10.ForeColor = System.Drawing.SystemColors.Highlight
        Me.RadLabel10.Location = New System.Drawing.Point(799, 253)
        Me.RadLabel10.Name = "RadLabel10"
        Me.RadLabel10.Size = New System.Drawing.Size(155, 16)
        Me.RadLabel10.TabIndex = 4
        Me.RadLabel10.Text = "Double click To Chage Rate"
        Me.RadLabel10.Visible = False
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox1.Controls.Add(Me.txtTermCode)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel16)
        Me.RadGroupBox1.Controls.Add(Me.txtDueDate)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel17)
        Me.RadGroupBox1.Controls.Add(Me.lblTermName)
        Me.RadGroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = "Terms"
        Me.RadGroupBox1.Location = New System.Drawing.Point(1, 266)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(957, 87)
        Me.RadGroupBox1.TabIndex = 2
        Me.RadGroupBox1.Text = "Terms"
        '
        'txtTermCode
        '
        Me.txtTermCode.CalculationExpression = Nothing
        Me.txtTermCode.Enabled = False
        Me.txtTermCode.FieldCode = Nothing
        Me.txtTermCode.FieldDesc = Nothing
        Me.txtTermCode.FieldMaxLength = 0
        Me.txtTermCode.FieldName = Nothing
        Me.txtTermCode.isCalculatedField = False
        Me.txtTermCode.IsSourceFromTable = False
        Me.txtTermCode.IsSourceFromValueList = False
        Me.txtTermCode.IsUnique = False
        Me.txtTermCode.Location = New System.Drawing.Point(68, 23)
        Me.txtTermCode.MendatroryField = False
        Me.txtTermCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTermCode.MyLinkLable1 = Me.RadLabel16
        Me.txtTermCode.MyLinkLable2 = Me.lblTermName
        Me.txtTermCode.MyReadOnly = False
        Me.txtTermCode.MyShowMasterFormButton = False
        Me.txtTermCode.Name = "txtTermCode"
        Me.txtTermCode.ReferenceFieldDesc = Nothing
        Me.txtTermCode.ReferenceFieldName = Nothing
        Me.txtTermCode.ReferenceTableName = Nothing
        Me.txtTermCode.Size = New System.Drawing.Size(143, 19)
        Me.txtTermCode.TabIndex = 0
        Me.txtTermCode.Value = ""
        '
        'RadLabel16
        '
        Me.RadLabel16.FieldName = Nothing
        Me.RadLabel16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel16.Location = New System.Drawing.Point(6, 26)
        Me.RadLabel16.Name = "RadLabel16"
        Me.RadLabel16.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel16.TabIndex = 3
        Me.RadLabel16.Text = "Term Code"
        '
        'lblTermName
        '
        Me.lblTermName.AutoSize = False
        Me.lblTermName.BorderVisible = True
        Me.lblTermName.FieldName = Nothing
        Me.lblTermName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTermName.Location = New System.Drawing.Point(220, 23)
        Me.lblTermName.Name = "lblTermName"
        Me.lblTermName.Size = New System.Drawing.Size(321, 20)
        Me.lblTermName.TabIndex = 1
        Me.lblTermName.TextWrap = False
        Me.lblTermName.Visible = False
        '
        'txtDueDate
        '
        Me.txtDueDate.CalculationExpression = Nothing
        Me.txtDueDate.CustomFormat = "dd-MM-yyyy"
        Me.txtDueDate.FieldCode = Nothing
        Me.txtDueDate.FieldDesc = Nothing
        Me.txtDueDate.FieldMaxLength = 0
        Me.txtDueDate.FieldName = Nothing
        Me.txtDueDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDueDate.isCalculatedField = False
        Me.txtDueDate.IsSourceFromTable = False
        Me.txtDueDate.IsSourceFromValueList = False
        Me.txtDueDate.IsUnique = False
        Me.txtDueDate.Location = New System.Drawing.Point(70, 57)
        Me.txtDueDate.MendatroryField = False
        Me.txtDueDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDueDate.MyLinkLable1 = Me.RadLabel17
        Me.txtDueDate.MyLinkLable2 = Nothing
        Me.txtDueDate.Name = "txtDueDate"
        Me.txtDueDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDueDate.ReferenceFieldDesc = Nothing
        Me.txtDueDate.ReferenceFieldName = Nothing
        Me.txtDueDate.ReferenceTableName = Nothing
        Me.txtDueDate.Size = New System.Drawing.Size(81, 18)
        Me.txtDueDate.TabIndex = 2
        Me.txtDueDate.TabStop = False
        Me.txtDueDate.Text = "13-06-2011"
        Me.txtDueDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel17
        '
        Me.RadLabel17.FieldName = Nothing
        Me.RadLabel17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel17.Location = New System.Drawing.Point(6, 58)
        Me.RadLabel17.Name = "RadLabel17"
        Me.RadLabel17.Size = New System.Drawing.Size(54, 16)
        Me.RadLabel17.TabIndex = 4
        Me.RadLabel17.Text = "Due Date"
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
        Me.gv2.Location = New System.Drawing.Point(2, 34)
        '
        '
        '
        Me.gv2.MasterTemplate.AllowAddNewRow = False
        Me.gv2.MasterTemplate.AllowDeleteRow = False
        Me.gv2.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv2.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv2.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gv2.MyStopExport = False
        Me.gv2.Name = "gv2"
        Me.gv2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv2.ShowHeaderCellButtons = True
        Me.gv2.Size = New System.Drawing.Size(952, 215)
        Me.gv2.TabIndex = 3
        '
        'RadPageViewPage4
        '
        Me.RadPageViewPage4.Controls.Add(Me.BntContainerDeposit)
        Me.RadPageViewPage4.Controls.Add(Me.btnReverseAndRecreate)
        Me.RadPageViewPage4.Controls.Add(Me.btnRecreateJournalEntry)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage4.Controls.Add(Me.lblNetAmt)
        Me.RadPageViewPage4.Controls.Add(Me.lblEmptyValue)
        Me.RadPageViewPage4.Controls.Add(Me.Panel1)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel39)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel37)
        Me.RadPageViewPage4.Controls.Add(Me.lblTPT)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel32)
        Me.RadPageViewPage4.Controls.Add(Me.lblTotAmt)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel30)
        Me.RadPageViewPage4.Controls.Add(Me.lblDetaolTotAmt)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel25)
        Me.RadPageViewPage4.Controls.Add(Me.lblTaxAmt)
        Me.RadPageViewPage4.Controls.Add(Me.lblDiscountAmt)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel22)
        Me.RadPageViewPage4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage4.ItemSize = New System.Drawing.SizeF(41.0!, 26.0!)
        Me.RadPageViewPage4.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage4.Name = "RadPageViewPage4"
        Me.RadPageViewPage4.Size = New System.Drawing.Size(957, 354)
        Me.RadPageViewPage4.Text = "Total"
        '
        'BntContainerDeposit
        '
        Me.BntContainerDeposit.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BntContainerDeposit.Location = New System.Drawing.Point(13, 259)
        Me.BntContainerDeposit.Name = "BntContainerDeposit"
        Me.BntContainerDeposit.Size = New System.Drawing.Size(261, 22)
        Me.BntContainerDeposit.TabIndex = 17
        Me.BntContainerDeposit.Text = "Conainer Deposit Correction"
        Me.BntContainerDeposit.Visible = False
        '
        'btnReverseAndRecreate
        '
        Me.btnReverseAndRecreate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverseAndRecreate.Location = New System.Drawing.Point(13, 231)
        Me.btnReverseAndRecreate.Name = "btnReverseAndRecreate"
        Me.btnReverseAndRecreate.Size = New System.Drawing.Size(261, 22)
        Me.btnReverseAndRecreate.TabIndex = 16
        Me.btnReverseAndRecreate.Text = "Reverse And Recreate"
        Me.btnReverseAndRecreate.Visible = False
        '
        'btnRecreateJournalEntry
        '
        Me.btnRecreateJournalEntry.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRecreateJournalEntry.Location = New System.Drawing.Point(13, 203)
        Me.btnRecreateJournalEntry.Name = "btnRecreateJournalEntry"
        Me.btnRecreateJournalEntry.Size = New System.Drawing.Size(261, 22)
        Me.btnRecreateJournalEntry.TabIndex = 15
        Me.btnRecreateJournalEntry.Text = "Recreate Journal Entry"
        Me.btnRecreateJournalEntry.Visible = False
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(84, 178)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(74, 16)
        Me.MyLabel1.TabIndex = 14
        Me.MyLabel1.Text = "Total Amount"
        '
        'lblNetAmt
        '
        Me.lblNetAmt.AutoSize = False
        Me.lblNetAmt.BorderVisible = True
        Me.lblNetAmt.FieldName = Nothing
        Me.lblNetAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNetAmt.Location = New System.Drawing.Point(164, 177)
        Me.lblNetAmt.Name = "lblNetAmt"
        Me.lblNetAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblNetAmt.TabIndex = 13
        Me.lblNetAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblEmptyValue
        '
        Me.lblEmptyValue.AutoSize = False
        Me.lblEmptyValue.BorderVisible = True
        Me.lblEmptyValue.FieldName = Nothing
        Me.lblEmptyValue.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmptyValue.Location = New System.Drawing.Point(164, 151)
        Me.lblEmptyValue.Name = "lblEmptyValue"
        Me.lblEmptyValue.Size = New System.Drawing.Size(110, 18)
        Me.lblEmptyValue.TabIndex = 4
        Me.lblEmptyValue.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lblCustAccount)
        Me.Panel1.Controls.Add(Me.RadLabel26)
        Me.Panel1.Controls.Add(Me.MyLabel3)
        Me.Panel1.Controls.Add(Me.lblAdditionalCharges)
        Me.Panel1.Controls.Add(Me.lblLevel1_User_code)
        Me.Panel1.Controls.Add(Me.RadLabel20)
        Me.Panel1.Controls.Add(Me.RadLabel53)
        Me.Panel1.Controls.Add(Me.RadLabel52)
        Me.Panel1.Controls.Add(Me.lblLevel2_User_code)
        Me.Panel1.Controls.Add(Me.lblLevel3_User_code)
        Me.Panel1.Controls.Add(Me.lblLevel4_User_code)
        Me.Panel1.Controls.Add(Me.RadLabel9)
        Me.Panel1.Controls.Add(Me.RadLabel28)
        Me.Panel1.Controls.Add(Me.lblLevel5_User_code)
        Me.Panel1.Controls.Add(Me.lblDetailDiscountAmt)
        Me.Panel1.Controls.Add(Me.lblLevel1_User_Commission)
        Me.Panel1.Controls.Add(Me.RadLabel47)
        Me.Panel1.Controls.Add(Me.RadLabel18)
        Me.Panel1.Controls.Add(Me.lblLevel2_User_Commission)
        Me.Panel1.Controls.Add(Me.RadLabel33)
        Me.Panel1.Controls.Add(Me.lblOtherCharges)
        Me.Panel1.Controls.Add(Me.RadLabel45)
        Me.Panel1.Controls.Add(Me.lblAssessableAmt)
        Me.Panel1.Controls.Add(Me.RadLabel27)
        Me.Panel1.Controls.Add(Me.lblFreightAmount)
        Me.Panel1.Controls.Add(Me.RadLabel19)
        Me.Panel1.Controls.Add(Me.lblLevel3_User_Commission)
        Me.Panel1.Controls.Add(Me.RadLabel36)
        Me.Panel1.Controls.Add(Me.RadLabel43)
        Me.Panel1.Controls.Add(Me.RadLabel42)
        Me.Panel1.Controls.Add(Me.lblLevel5_User_Commission)
        Me.Panel1.Controls.Add(Me.lblLevel4_User_Commission)
        Me.Panel1.Location = New System.Drawing.Point(323, 8)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(568, 326)
        Me.Panel1.TabIndex = 6
        Me.Panel1.Visible = False
        '
        'lblCustAccount
        '
        Me.lblCustAccount.AutoSize = False
        Me.lblCustAccount.BorderVisible = True
        Me.lblCustAccount.FieldName = Nothing
        Me.lblCustAccount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustAccount.Location = New System.Drawing.Point(442, 83)
        Me.lblCustAccount.Name = "lblCustAccount"
        Me.lblCustAccount.Size = New System.Drawing.Size(110, 18)
        Me.lblCustAccount.TabIndex = 16
        Me.lblCustAccount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadLabel26
        '
        Me.RadLabel26.FieldName = Nothing
        Me.RadLabel26.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel26.Location = New System.Drawing.Point(334, 37)
        Me.RadLabel26.Name = "RadLabel26"
        Me.RadLabel26.Size = New System.Drawing.Size(102, 16)
        Me.RadLabel26.TabIndex = 18
        Me.RadLabel26.Text = "Additional Charges"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(335, 83)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(99, 16)
        Me.MyLabel3.TabIndex = 17
        Me.MyLabel3.Text = "Customer Account"
        '
        'lblAdditionalCharges
        '
        Me.lblAdditionalCharges.AutoSize = False
        Me.lblAdditionalCharges.BorderVisible = True
        Me.lblAdditionalCharges.FieldName = Nothing
        Me.lblAdditionalCharges.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAdditionalCharges.Location = New System.Drawing.Point(441, 37)
        Me.lblAdditionalCharges.Name = "lblAdditionalCharges"
        Me.lblAdditionalCharges.Size = New System.Drawing.Size(110, 18)
        Me.lblAdditionalCharges.TabIndex = 3
        Me.lblAdditionalCharges.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLevel1_User_code
        '
        Me.lblLevel1_User_code.AutoSize = False
        Me.lblLevel1_User_code.BorderVisible = True
        Me.lblLevel1_User_code.FieldName = Nothing
        Me.lblLevel1_User_code.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLevel1_User_code.Location = New System.Drawing.Point(172, 13)
        Me.lblLevel1_User_code.Name = "lblLevel1_User_code"
        Me.lblLevel1_User_code.Size = New System.Drawing.Size(110, 18)
        Me.lblLevel1_User_code.TabIndex = 0
        Me.lblLevel1_User_code.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadLabel20
        '
        Me.RadLabel20.FieldName = Nothing
        Me.RadLabel20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel20.Location = New System.Drawing.Point(33, 39)
        Me.RadLabel20.Name = "RadLabel20"
        Me.RadLabel20.Size = New System.Drawing.Size(101, 16)
        Me.RadLabel20.TabIndex = 17
        Me.RadLabel20.Text = "Level1_User_code"
        '
        'RadLabel53
        '
        Me.RadLabel53.FieldName = Nothing
        Me.RadLabel53.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel53.Location = New System.Drawing.Point(33, 65)
        Me.RadLabel53.Name = "RadLabel53"
        Me.RadLabel53.Size = New System.Drawing.Size(101, 16)
        Me.RadLabel53.TabIndex = 19
        Me.RadLabel53.Text = "Level3_User_code"
        '
        'RadLabel52
        '
        Me.RadLabel52.FieldName = Nothing
        Me.RadLabel52.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel52.Location = New System.Drawing.Point(33, 91)
        Me.RadLabel52.Name = "RadLabel52"
        Me.RadLabel52.Size = New System.Drawing.Size(101, 16)
        Me.RadLabel52.TabIndex = 21
        Me.RadLabel52.Text = "Level4_User_code"
        '
        'lblLevel2_User_code
        '
        Me.lblLevel2_User_code.AutoSize = False
        Me.lblLevel2_User_code.BorderVisible = True
        Me.lblLevel2_User_code.FieldName = Nothing
        Me.lblLevel2_User_code.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLevel2_User_code.Location = New System.Drawing.Point(172, 39)
        Me.lblLevel2_User_code.Name = "lblLevel2_User_code"
        Me.lblLevel2_User_code.Size = New System.Drawing.Size(110, 18)
        Me.lblLevel2_User_code.TabIndex = 2
        Me.lblLevel2_User_code.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLevel3_User_code
        '
        Me.lblLevel3_User_code.AutoSize = False
        Me.lblLevel3_User_code.BorderVisible = True
        Me.lblLevel3_User_code.FieldName = Nothing
        Me.lblLevel3_User_code.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLevel3_User_code.Location = New System.Drawing.Point(172, 65)
        Me.lblLevel3_User_code.Name = "lblLevel3_User_code"
        Me.lblLevel3_User_code.Size = New System.Drawing.Size(110, 18)
        Me.lblLevel3_User_code.TabIndex = 4
        Me.lblLevel3_User_code.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLevel4_User_code
        '
        Me.lblLevel4_User_code.AutoSize = False
        Me.lblLevel4_User_code.BorderVisible = True
        Me.lblLevel4_User_code.FieldName = Nothing
        Me.lblLevel4_User_code.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLevel4_User_code.Location = New System.Drawing.Point(172, 91)
        Me.lblLevel4_User_code.Name = "lblLevel4_User_code"
        Me.lblLevel4_User_code.Size = New System.Drawing.Size(110, 18)
        Me.lblLevel4_User_code.TabIndex = 6
        Me.lblLevel4_User_code.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadLabel9
        '
        Me.RadLabel9.FieldName = Nothing
        Me.RadLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel9.Location = New System.Drawing.Point(311, 14)
        Me.RadLabel9.Name = "RadLabel9"
        Me.RadLabel9.Size = New System.Drawing.Size(125, 16)
        Me.RadLabel9.TabIndex = 16
        Me.RadLabel9.Text = "Detail Discount Amount"
        '
        'RadLabel28
        '
        Me.RadLabel28.FieldName = Nothing
        Me.RadLabel28.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel28.Location = New System.Drawing.Point(33, 13)
        Me.RadLabel28.Name = "RadLabel28"
        Me.RadLabel28.Size = New System.Drawing.Size(101, 16)
        Me.RadLabel28.TabIndex = 15
        Me.RadLabel28.Text = "Level1_User_code"
        '
        'lblLevel5_User_code
        '
        Me.lblLevel5_User_code.AutoSize = False
        Me.lblLevel5_User_code.BorderVisible = True
        Me.lblLevel5_User_code.FieldName = Nothing
        Me.lblLevel5_User_code.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLevel5_User_code.Location = New System.Drawing.Point(172, 117)
        Me.lblLevel5_User_code.Name = "lblLevel5_User_code"
        Me.lblLevel5_User_code.Size = New System.Drawing.Size(110, 18)
        Me.lblLevel5_User_code.TabIndex = 7
        Me.lblLevel5_User_code.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDetailDiscountAmt
        '
        Me.lblDetailDiscountAmt.AutoSize = False
        Me.lblDetailDiscountAmt.BorderVisible = True
        Me.lblDetailDiscountAmt.FieldName = Nothing
        Me.lblDetailDiscountAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDetailDiscountAmt.Location = New System.Drawing.Point(441, 14)
        Me.lblDetailDiscountAmt.Name = "lblDetailDiscountAmt"
        Me.lblDetailDiscountAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblDetailDiscountAmt.TabIndex = 1
        Me.lblDetailDiscountAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLevel1_User_Commission
        '
        Me.lblLevel1_User_Commission.AutoSize = False
        Me.lblLevel1_User_Commission.BorderVisible = True
        Me.lblLevel1_User_Commission.FieldName = Nothing
        Me.lblLevel1_User_Commission.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLevel1_User_Commission.Location = New System.Drawing.Point(172, 143)
        Me.lblLevel1_User_Commission.Name = "lblLevel1_User_Commission"
        Me.lblLevel1_User_Commission.Size = New System.Drawing.Size(110, 18)
        Me.lblLevel1_User_Commission.TabIndex = 8
        Me.lblLevel1_User_Commission.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadLabel47
        '
        Me.RadLabel47.FieldName = Nothing
        Me.RadLabel47.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel47.Location = New System.Drawing.Point(33, 143)
        Me.RadLabel47.Name = "RadLabel47"
        Me.RadLabel47.Size = New System.Drawing.Size(138, 16)
        Me.RadLabel47.TabIndex = 23
        Me.RadLabel47.Text = "Level1_User_Commission"
        '
        'RadLabel18
        '
        Me.RadLabel18.FieldName = Nothing
        Me.RadLabel18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel18.Location = New System.Drawing.Point(33, 295)
        Me.RadLabel18.Name = "RadLabel18"
        Me.RadLabel18.Size = New System.Drawing.Size(80, 16)
        Me.RadLabel18.TabIndex = 29
        Me.RadLabel18.Text = "Other Charges"
        '
        'lblLevel2_User_Commission
        '
        Me.lblLevel2_User_Commission.AutoSize = False
        Me.lblLevel2_User_Commission.BorderVisible = True
        Me.lblLevel2_User_Commission.FieldName = Nothing
        Me.lblLevel2_User_Commission.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLevel2_User_Commission.Location = New System.Drawing.Point(172, 169)
        Me.lblLevel2_User_Commission.Name = "lblLevel2_User_Commission"
        Me.lblLevel2_User_Commission.Size = New System.Drawing.Size(110, 18)
        Me.lblLevel2_User_Commission.TabIndex = 9
        Me.lblLevel2_User_Commission.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadLabel33
        '
        Me.RadLabel33.FieldName = Nothing
        Me.RadLabel33.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel33.Location = New System.Drawing.Point(33, 247)
        Me.RadLabel33.Name = "RadLabel33"
        Me.RadLabel33.Size = New System.Drawing.Size(138, 16)
        Me.RadLabel33.TabIndex = 27
        Me.RadLabel33.Text = "Level5_User_Commission"
        '
        'lblOtherCharges
        '
        Me.lblOtherCharges.AutoSize = False
        Me.lblOtherCharges.BorderVisible = True
        Me.lblOtherCharges.FieldName = Nothing
        Me.lblOtherCharges.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOtherCharges.Location = New System.Drawing.Point(172, 295)
        Me.lblOtherCharges.Name = "lblOtherCharges"
        Me.lblOtherCharges.Size = New System.Drawing.Size(110, 18)
        Me.lblOtherCharges.TabIndex = 14
        Me.lblOtherCharges.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadLabel45
        '
        Me.RadLabel45.FieldName = Nothing
        Me.RadLabel45.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel45.Location = New System.Drawing.Point(33, 169)
        Me.RadLabel45.Name = "RadLabel45"
        Me.RadLabel45.Size = New System.Drawing.Size(138, 16)
        Me.RadLabel45.TabIndex = 24
        Me.RadLabel45.Text = "Level2_User_Commission"
        '
        'lblAssessableAmt
        '
        Me.lblAssessableAmt.AutoSize = False
        Me.lblAssessableAmt.BorderVisible = True
        Me.lblAssessableAmt.FieldName = Nothing
        Me.lblAssessableAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAssessableAmt.Location = New System.Drawing.Point(441, 61)
        Me.lblAssessableAmt.Name = "lblAssessableAmt"
        Me.lblAssessableAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblAssessableAmt.TabIndex = 5
        Me.lblAssessableAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadLabel27
        '
        Me.RadLabel27.FieldName = Nothing
        Me.RadLabel27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel27.Location = New System.Drawing.Point(33, 269)
        Me.RadLabel27.Name = "RadLabel27"
        Me.RadLabel27.Size = New System.Drawing.Size(84, 16)
        Me.RadLabel27.TabIndex = 28
        Me.RadLabel27.Text = "Freight Amount"
        '
        'lblFreightAmount
        '
        Me.lblFreightAmount.AutoSize = False
        Me.lblFreightAmount.BorderVisible = True
        Me.lblFreightAmount.FieldName = Nothing
        Me.lblFreightAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFreightAmount.Location = New System.Drawing.Point(172, 269)
        Me.lblFreightAmount.Name = "lblFreightAmount"
        Me.lblFreightAmount.Size = New System.Drawing.Size(110, 18)
        Me.lblFreightAmount.TabIndex = 13
        Me.lblFreightAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadLabel19
        '
        Me.RadLabel19.FieldName = Nothing
        Me.RadLabel19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel19.Location = New System.Drawing.Point(330, 61)
        Me.RadLabel19.Name = "RadLabel19"
        Me.RadLabel19.Size = New System.Drawing.Size(106, 16)
        Me.RadLabel19.TabIndex = 20
        Me.RadLabel19.Text = "Assessable Amount"
        '
        'lblLevel3_User_Commission
        '
        Me.lblLevel3_User_Commission.AutoSize = False
        Me.lblLevel3_User_Commission.BorderVisible = True
        Me.lblLevel3_User_Commission.FieldName = Nothing
        Me.lblLevel3_User_Commission.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLevel3_User_Commission.Location = New System.Drawing.Point(172, 195)
        Me.lblLevel3_User_Commission.Name = "lblLevel3_User_Commission"
        Me.lblLevel3_User_Commission.Size = New System.Drawing.Size(110, 18)
        Me.lblLevel3_User_Commission.TabIndex = 10
        Me.lblLevel3_User_Commission.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadLabel36
        '
        Me.RadLabel36.FieldName = Nothing
        Me.RadLabel36.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel36.Location = New System.Drawing.Point(33, 221)
        Me.RadLabel36.Name = "RadLabel36"
        Me.RadLabel36.Size = New System.Drawing.Size(138, 16)
        Me.RadLabel36.TabIndex = 26
        Me.RadLabel36.Text = "Level4_User_Commission"
        '
        'RadLabel43
        '
        Me.RadLabel43.FieldName = Nothing
        Me.RadLabel43.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel43.Location = New System.Drawing.Point(33, 117)
        Me.RadLabel43.Name = "RadLabel43"
        Me.RadLabel43.Size = New System.Drawing.Size(101, 16)
        Me.RadLabel43.TabIndex = 22
        Me.RadLabel43.Text = "Level5_User_code"
        '
        'RadLabel42
        '
        Me.RadLabel42.FieldName = Nothing
        Me.RadLabel42.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel42.Location = New System.Drawing.Point(33, 195)
        Me.RadLabel42.Name = "RadLabel42"
        Me.RadLabel42.Size = New System.Drawing.Size(138, 16)
        Me.RadLabel42.TabIndex = 25
        Me.RadLabel42.Text = "Level3_User_Commission"
        '
        'lblLevel5_User_Commission
        '
        Me.lblLevel5_User_Commission.AutoSize = False
        Me.lblLevel5_User_Commission.BorderVisible = True
        Me.lblLevel5_User_Commission.FieldName = Nothing
        Me.lblLevel5_User_Commission.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLevel5_User_Commission.Location = New System.Drawing.Point(172, 247)
        Me.lblLevel5_User_Commission.Name = "lblLevel5_User_Commission"
        Me.lblLevel5_User_Commission.Size = New System.Drawing.Size(110, 18)
        Me.lblLevel5_User_Commission.TabIndex = 12
        Me.lblLevel5_User_Commission.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLevel4_User_Commission
        '
        Me.lblLevel4_User_Commission.AutoSize = False
        Me.lblLevel4_User_Commission.BorderVisible = True
        Me.lblLevel4_User_Commission.FieldName = Nothing
        Me.lblLevel4_User_Commission.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLevel4_User_Commission.Location = New System.Drawing.Point(172, 221)
        Me.lblLevel4_User_Commission.Name = "lblLevel4_User_Commission"
        Me.lblLevel4_User_Commission.Size = New System.Drawing.Size(110, 18)
        Me.lblLevel4_User_Commission.TabIndex = 11
        Me.lblLevel4_User_Commission.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadLabel39
        '
        Me.RadLabel39.FieldName = Nothing
        Me.RadLabel39.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel39.Location = New System.Drawing.Point(61, 152)
        Me.RadLabel39.Name = "RadLabel39"
        Me.RadLabel39.Size = New System.Drawing.Size(97, 16)
        Me.RadLabel39.TabIndex = 11
        Me.RadLabel39.Text = "Container Deposit"
        '
        'RadLabel37
        '
        Me.RadLabel37.FieldName = Nothing
        Me.RadLabel37.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel37.Location = New System.Drawing.Point(130, 100)
        Me.RadLabel37.Name = "RadLabel37"
        Me.RadLabel37.Size = New System.Drawing.Size(28, 16)
        Me.RadLabel37.TabIndex = 10
        Me.RadLabel37.Text = "TPT"
        '
        'lblTPT
        '
        Me.lblTPT.AutoSize = False
        Me.lblTPT.BorderVisible = True
        Me.lblTPT.FieldName = Nothing
        Me.lblTPT.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTPT.Location = New System.Drawing.Point(164, 99)
        Me.lblTPT.Name = "lblTPT"
        Me.lblTPT.Size = New System.Drawing.Size(110, 18)
        Me.lblTPT.TabIndex = 3
        Me.lblTPT.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadLabel32
        '
        Me.RadLabel32.FieldName = Nothing
        Me.RadLabel32.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel32.Location = New System.Drawing.Point(44, 126)
        Me.RadLabel32.Name = "RadLabel32"
        Me.RadLabel32.Size = New System.Drawing.Size(114, 16)
        Me.RadLabel32.TabIndex = 12
        Me.RadLabel32.Text = "Sales Return Amount"
        '
        'lblTotAmt
        '
        Me.lblTotAmt.AutoSize = False
        Me.lblTotAmt.BorderVisible = True
        Me.lblTotAmt.FieldName = Nothing
        Me.lblTotAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotAmt.Location = New System.Drawing.Point(164, 125)
        Me.lblTotAmt.Name = "lblTotAmt"
        Me.lblTotAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblTotAmt.TabIndex = 5
        Me.lblTotAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadLabel30
        '
        Me.RadLabel30.FieldName = Nothing
        Me.RadLabel30.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel30.Location = New System.Drawing.Point(13, 48)
        Me.RadLabel30.Name = "RadLabel30"
        Me.RadLabel30.Size = New System.Drawing.Size(145, 16)
        Me.RadLabel30.TabIndex = 8
        Me.RadLabel30.Text = "Invoice Detail Total Amount"
        '
        'lblDetaolTotAmt
        '
        Me.lblDetaolTotAmt.AutoSize = False
        Me.lblDetaolTotAmt.BorderVisible = True
        Me.lblDetaolTotAmt.FieldName = Nothing
        Me.lblDetaolTotAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDetaolTotAmt.Location = New System.Drawing.Point(164, 47)
        Me.lblDetaolTotAmt.Name = "lblDetaolTotAmt"
        Me.lblDetaolTotAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblDetaolTotAmt.TabIndex = 1
        Me.lblDetaolTotAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadLabel25
        '
        Me.RadLabel25.FieldName = Nothing
        Me.RadLabel25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel25.Location = New System.Drawing.Point(91, 74)
        Me.RadLabel25.Name = "RadLabel25"
        Me.RadLabel25.Size = New System.Drawing.Size(67, 16)
        Me.RadLabel25.TabIndex = 9
        Me.RadLabel25.Text = "Tax Amount"
        '
        'lblTaxAmt
        '
        Me.lblTaxAmt.AutoSize = False
        Me.lblTaxAmt.BorderVisible = True
        Me.lblTaxAmt.FieldName = Nothing
        Me.lblTaxAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTaxAmt.Location = New System.Drawing.Point(164, 73)
        Me.lblTaxAmt.Name = "lblTaxAmt"
        Me.lblTaxAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblTaxAmt.TabIndex = 2
        Me.lblTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDiscountAmt
        '
        Me.lblDiscountAmt.AutoSize = False
        Me.lblDiscountAmt.BorderVisible = True
        Me.lblDiscountAmt.FieldName = Nothing
        Me.lblDiscountAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDiscountAmt.Location = New System.Drawing.Point(164, 21)
        Me.lblDiscountAmt.Name = "lblDiscountAmt"
        Me.lblDiscountAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblDiscountAmt.TabIndex = 0
        Me.lblDiscountAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadLabel22
        '
        Me.RadLabel22.FieldName = Nothing
        Me.RadLabel22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel22.Location = New System.Drawing.Point(66, 22)
        Me.RadLabel22.Name = "RadLabel22"
        Me.RadLabel22.Size = New System.Drawing.Size(92, 16)
        Me.RadLabel22.TabIndex = 7
        Me.RadLabel22.Text = "Discount Amount"
        '
        'btnPrint
        '
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(290, 4)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(69, 22)
        Me.btnPrint.TabIndex = 4
        Me.btnPrint.Text = "Print"
        '
        'btnViewTDSDetails
        '
        Me.btnViewTDSDetails.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnViewTDSDetails.Location = New System.Drawing.Point(215, 4)
        Me.btnViewTDSDetails.Name = "btnViewTDSDetails"
        Me.btnViewTDSDetails.Size = New System.Drawing.Size(69, 22)
        Me.btnViewTDSDetails.TabIndex = 3
        Me.btnViewTDSDetails.Text = "View TDS"
        Me.btnViewTDSDetails.Visible = False
        '
        'btnDelete
        '
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(145, 4)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 22)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "Delete"
        '
        'btnPost
        '
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(75, 4)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(69, 22)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(901, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 5
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
        'frmSalesReturnNew
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(978, 437)
        Me.Controls.Add(Me.SplitContainer1)
        Me.MinimumSize = New System.Drawing.Size(890, 467)
        Me.Name = "frmSalesReturnNew"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Sales Return"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.txtshellqty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtInvoiceDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel40, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtKMReading, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel34, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSchemeSampleCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel35, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtModeOfTransport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel29, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVehicleNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSalesman, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRouteNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomerName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRefNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPriceCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustomerPONO, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.RadPageViewPage2.PerformLayout()
        CType(Me.RadLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTaxGrpName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.RadLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTermName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDueDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage4.ResumeLayout(False)
        Me.RadPageViewPage4.PerformLayout()
        CType(Me.BntContainerDeposit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReverseAndRecreate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRecreateJournalEntry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNetAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmptyValue, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.lblCustAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel26, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAdditionalCharges, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLevel1_User_code, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel53, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel52, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLevel2_User_code, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLevel3_User_code, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLevel4_User_code, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel28, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLevel5_User_code, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDetailDiscountAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLevel1_User_Commission, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel47, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLevel2_User_Commission, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel33, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblOtherCharges, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel45, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAssessableAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel27, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFreightAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLevel3_User_Commission, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel36, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel43, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel42, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLevel5_User_Commission, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLevel4_User_Commission, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel39, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel37, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTPT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel32, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel30, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDetaolTotAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTaxAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDiscountAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnViewTDSDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage4 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtCustomerPONO As common.Controls.MyTextBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents gv2 As common.UserControls.MyRadGridView
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDueDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents txtCustomerNo As common.UserControls.txtFinder
    Friend WithEvents txtRouteNo As common.UserControls.txtFinder
    Friend WithEvents txtLocation As common.UserControls.txtFinder
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents txtRemarks As common.Controls.MyTextBox
    Friend WithEvents txtTaxGroup As common.UserControls.txtFinder
    Friend WithEvents txtTermCode As common.UserControls.txtFinder
    Friend WithEvents btnViewTDSDetails As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtRefNo As common.Controls.MyTextBox
    Friend WithEvents txtPriceCode As common.Controls.MyTextBox
    Friend WithEvents txtInvoiceNo As common.UserControls.txtFinder
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtSalesman As common.UserControls.txtFinder
    Friend WithEvents txtVehicleNo As common.UserControls.txtFinder
    Friend WithEvents txtModeOfTransport As common.Controls.MyTextBox
    Friend WithEvents txtKMReading As common.Controls.MyTextBox
    Friend WithEvents txtSchemeSampleCode As common.Controls.MyTextBox
    Friend WithEvents txtInvoiceDate As common.Controls.MyDateTimePicker
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents lblCustomerName As common.Controls.MyLabel
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents lblTaxGrpName As common.Controls.MyLabel
    Friend WithEvents RadLabel11 As common.Controls.MyLabel
    Friend WithEvents RadLabel25 As common.Controls.MyLabel
    Friend WithEvents lblTaxAmt As common.Controls.MyLabel
    Friend WithEvents lblDetailDiscountAmt As common.Controls.MyLabel
    Friend WithEvents lblDiscountAmt As common.Controls.MyLabel
    Friend WithEvents lblAssessableAmt As common.Controls.MyLabel
    Friend WithEvents RadLabel22 As common.Controls.MyLabel
    Friend WithEvents RadLabel19 As common.Controls.MyLabel
    Friend WithEvents RadLabel27 As common.Controls.MyLabel
    Friend WithEvents lblFreightAmount As common.Controls.MyLabel
    Friend WithEvents RadLabel17 As common.Controls.MyLabel
    Friend WithEvents lblTermName As common.Controls.MyLabel
    Friend WithEvents RadLabel16 As common.Controls.MyLabel
    Friend WithEvents RadLabel9 As common.Controls.MyLabel
    Friend WithEvents RadLabel10 As common.Controls.MyLabel
    Friend WithEvents RadLabel15 As common.Controls.MyLabel
    Friend WithEvents lblRouteNo As common.Controls.MyLabel
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents RadLabel14 As common.Controls.MyLabel
    Friend WithEvents RadLabel6 As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadLabel12 As common.Controls.MyLabel
    Friend WithEvents RadLabel13 As common.Controls.MyLabel
    Friend WithEvents RadLabel5 As common.Controls.MyLabel
    Friend WithEvents RadLabel23 As common.Controls.MyLabel
    Friend WithEvents RadLabel24 As common.Controls.MyLabel
    Friend WithEvents RadLabel29 As common.Controls.MyLabel
    Friend WithEvents RadLabel7 As common.Controls.MyLabel
    Friend WithEvents lblSalesman As common.Controls.MyLabel
    Friend WithEvents RadLabel8 As common.Controls.MyLabel
    Friend WithEvents lblVehicleNo As common.Controls.MyLabel
    Friend WithEvents RadLabel32 As common.Controls.MyLabel
    Friend WithEvents lblTotAmt As common.Controls.MyLabel
    Friend WithEvents RadLabel30 As common.Controls.MyLabel
    Friend WithEvents RadLabel26 As common.Controls.MyLabel
    Friend WithEvents lblDetaolTotAmt As common.Controls.MyLabel
    Friend WithEvents lblAdditionalCharges As common.Controls.MyLabel
    Friend WithEvents RadLabel18 As common.Controls.MyLabel
    Friend WithEvents lblOtherCharges As common.Controls.MyLabel
    Friend WithEvents RadLabel35 As common.Controls.MyLabel
    Friend WithEvents RadLabel34 As common.Controls.MyLabel
    Friend WithEvents RadLabel39 As common.Controls.MyLabel
    Friend WithEvents lblEmptyValue As common.Controls.MyLabel
    Friend WithEvents RadLabel37 As common.Controls.MyLabel
    Friend WithEvents lblTPT As common.Controls.MyLabel
    Friend WithEvents RadLabel40 As common.Controls.MyLabel
    Friend WithEvents lblLevel1_User_code As common.Controls.MyLabel
    Friend WithEvents RadLabel20 As common.Controls.MyLabel
    Friend WithEvents RadLabel53 As common.Controls.MyLabel
    Friend WithEvents RadLabel52 As common.Controls.MyLabel
    Friend WithEvents lblLevel2_User_code As common.Controls.MyLabel
    Friend WithEvents lblLevel3_User_code As common.Controls.MyLabel
    Friend WithEvents lblLevel4_User_code As common.Controls.MyLabel
    Friend WithEvents RadLabel28 As common.Controls.MyLabel
    Friend WithEvents lblLevel5_User_code As common.Controls.MyLabel
    Friend WithEvents lblLevel1_User_Commission As common.Controls.MyLabel
    Friend WithEvents RadLabel47 As common.Controls.MyLabel
    Friend WithEvents lblLevel2_User_Commission As common.Controls.MyLabel
    Friend WithEvents RadLabel33 As common.Controls.MyLabel
    Friend WithEvents RadLabel45 As common.Controls.MyLabel
    Friend WithEvents lblLevel3_User_Commission As common.Controls.MyLabel
    Friend WithEvents RadLabel36 As common.Controls.MyLabel
    Friend WithEvents RadLabel43 As common.Controls.MyLabel
    Friend WithEvents RadLabel42 As common.Controls.MyLabel
    Friend WithEvents lblLevel5_User_Commission As common.Controls.MyLabel
    Friend WithEvents lblLevel4_User_Commission As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents lblNetAmt As common.Controls.MyLabel
    Friend WithEvents lblCustAccount As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents btnRecreateJournalEntry As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReverseAndRecreate As Telerik.WinControls.UI.RadButton
    Friend WithEvents BntContainerDeposit As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyNumBox1 As common.MyNumBox
    Friend WithEvents txtshellqty As common.MyNumBox
End Class

