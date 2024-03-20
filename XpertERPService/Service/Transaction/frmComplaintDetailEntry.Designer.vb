Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmComplaintDetailEntry
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
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtworkorderno = New common.Controls.MyTextBox()
        Me.MyLabel25 = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtbillno = New common.Controls.MyTextBox()
        Me.txtaddamt = New common.Controls.MyTextBox()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.txtbillamt = New common.Controls.MyTextBox()
        Me.chksparepart = New common.Controls.MyCheckBox()
        Me.txtpendcode = New common.UserControls.txtFinder()
        Me.MyLabel22 = New common.Controls.MyLabel()
        Me.txtpendrsn = New common.Controls.MyLabel()
        Me.txtsecresn = New common.Controls.MyLabel()
        Me.txtresponse = New common.Controls.MyTextBox()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.txtseccode = New common.UserControls.txtFinder()
        Me.MyLabel21 = New common.Controls.MyLabel()
        Me.txtprimarydesc = New common.Controls.MyLabel()
        Me.txtreptserialno = New common.Controls.MyLabel()
        Me.txtprimarycode = New common.UserControls.txtFinder()
        Me.MyLabel27 = New common.Controls.MyLabel()
        Me.txtapexno = New common.Controls.MyComboBox()
        Me.rdpending = New common.Controls.MyRadioButton()
        Me.MyLabel17 = New common.Controls.MyLabel()
        Me.MyLabel20 = New common.Controls.MyLabel()
        Me.rdcomplt = New common.Controls.MyRadioButton()
        Me.txtcmpldt = New common.Controls.MyDateTimePicker()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtsrvcdlr = New common.UserControls.txtFinder()
        Me.txtsrvcdlrname = New common.Controls.MyLabel()
        Me.txtremarks = New common.Controls.MyTextBox()
        Me.MyLabel23 = New common.Controls.MyLabel()
        Me.rdnotcmplt = New common.Controls.MyRadioButton()
        Me.MyLabel18 = New common.Controls.MyLabel()
        Me.txttdmdesc = New common.Controls.MyLabel()
        Me.txttdmcode = New common.UserControls.txtFinder()
        Me.MyLabel19 = New common.Controls.MyLabel()
        Me.txtcompgivento = New common.Controls.MyTextBox()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.txtcompgivenby = New common.Controls.MyTextBox()
        Me.MyLabel15 = New common.Controls.MyLabel()
        Me.txtphnno = New common.Controls.MyTextBox()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.txtdistributor = New common.Controls.MyLabel()
        Me.Type = New common.Controls.MyLabel()
        Me.txttagno = New common.Controls.MyTextBox()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.txtserialno = New common.Controls.MyTextBox()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.txtSize = New common.Controls.MyLabel()
        Me.txtmodel = New common.Controls.MyLabel()
        Me.txtmake = New common.Controls.MyLabel()
        Me.txtassetdesc = New common.Controls.MyLabel()
        Me.txtassetcode = New common.UserControls.txtFinder()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.txtcmplntdesc = New common.Controls.MyLabel()
        Me.txtcomplntcode = New common.UserControls.txtFinder()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.txtcountry = New common.Controls.MyLabel()
        Me.txtstate = New common.Controls.MyLabel()
        Me.txtcity = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtoutletadd = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtoutletdesc = New common.Controls.MyLabel()
        Me.txtoutletcode = New common.UserControls.txtFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtWorkOrder1 = New common.Controls.MyTextBox()
        Me.MyLabel46 = New common.Controls.MyLabel()
        Me.MyLabel47 = New common.Controls.MyLabel()
        Me.txtBillNo1 = New common.Controls.MyTextBox()
        Me.txtAdditionalCharge = New common.Controls.MyTextBox()
        Me.MyLabel48 = New common.Controls.MyLabel()
        Me.MyLabel49 = New common.Controls.MyLabel()
        Me.txtBillAmt1 = New common.Controls.MyTextBox()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv2 = New common.UserControls.MyRadGridView()
        Me.txtPending1 = New common.Controls.MyTextBox()
        Me.MyLabel45 = New common.Controls.MyLabel()
        Me.txtResponseTime = New common.Controls.MyTextBox()
        Me.MyLabel42 = New common.Controls.MyLabel()
        Me.MyLabel43 = New common.Controls.MyLabel()
        Me.txtCompleteDate = New common.Controls.MyDateTimePicker()
        Me.txtRemarks1 = New common.Controls.MyTextBox()
        Me.MyLabel44 = New common.Controls.MyLabel()
        Me.ChkUserSparePart = New common.Controls.MyCheckBox()
        Me.rbtnPending = New common.Controls.MyRadioButton()
        Me.rbtnComleted = New common.Controls.MyRadioButton()
        Me.rbtnNotCompleted = New common.Controls.MyRadioButton()
        Me.txtService1 = New common.Controls.MyTextBox()
        Me.txtfranchise1 = New common.Controls.MyTextBox()
        Me.txtSecondary1 = New common.Controls.MyTextBox()
        Me.txtComplaintType = New common.Controls.MyTextBox()
        Me.txtprimary1 = New common.Controls.MyTextBox()
        Me.txtComplaintGivento = New common.Controls.MyTextBox()
        Me.MyLabel41 = New common.Controls.MyLabel()
        Me.txtComplaintGivenBy = New common.Controls.MyTextBox()
        Me.rptdSerialNo = New common.Controls.MyLabel()
        Me.MyLabel39 = New common.Controls.MyLabel()
        Me.cboApex1 = New common.Controls.MyComboBox()
        Me.MyLabel30 = New common.Controls.MyLabel()
        Me.txtTag1 = New common.Controls.MyTextBox()
        Me.txtSerialNo1 = New common.Controls.MyTextBox()
        Me.txtSize1 = New common.Controls.MyTextBox()
        Me.txtModel1 = New common.Controls.MyTextBox()
        Me.txtMake1 = New common.Controls.MyTextBox()
        Me.txtAssetType1 = New common.Controls.MyTextBox()
        Me.MyLabel38 = New common.Controls.MyLabel()
        Me.txtLocation1 = New common.Controls.MyTextBox()
        Me.txtCountry1 = New common.Controls.MyTextBox()
        Me.txtState1 = New common.Controls.MyTextBox()
        Me.txtCity1 = New common.Controls.MyTextBox()
        Me.txtPhn1 = New common.Controls.MyTextBox()
        Me.MyLabel32 = New common.Controls.MyLabel()
        Me.MyLabel33 = New common.Controls.MyLabel()
        Me.MyLabel34 = New common.Controls.MyLabel()
        Me.MyLabel35 = New common.Controls.MyLabel()
        Me.MyLabel36 = New common.Controls.MyLabel()
        Me.MyLabel37 = New common.Controls.MyLabel()
        Me.MyLabel26 = New common.Controls.MyLabel()
        Me.MyLabel28 = New common.Controls.MyLabel()
        Me.MyLabel29 = New common.Controls.MyLabel()
        Me.lblCity = New common.Controls.MyLabel()
        Me.MyLabel31 = New common.Controls.MyLabel()
        Me.txtType = New common.Controls.MyTextBox()
        Me.lblType = New common.Controls.MyLabel()
        Me.txtOutletName = New common.Controls.MyTextBox()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.txtdesc = New common.Controls.MyTextBox()
        Me.MyLabel24 = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.txtcomid = New common.UserControls.txtNavigator()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnsend = New Telerik.WinControls.UI.RadButton()
        Me.btnCopy = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkManual = New common.Controls.MyCheckBox()
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenufile = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.txtworkorderno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtbillno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtaddamt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtbillamt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chksparepart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtpendrsn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtsecresn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtresponse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtprimarydesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtreptserialno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel27, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtapexno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdpending, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdcomplt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcmpldt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtsrvcdlrname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtremarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdnotcmplt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txttdmdesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcompgivento, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcompgivenby, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtphnno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdistributor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Type, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txttagno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtserialno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSize, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtmodel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtmake, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtassetdesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcmplntdesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcountry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtstate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtoutletadd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtoutletdesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        CType(Me.txtWorkOrder1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel46, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel47, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBillNo1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAdditionalCharge, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel48, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel49, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBillAmt1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPending1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel45, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtResponseTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel42, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel43, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCompleteDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemarks1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel44, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkUserSparePart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnPending, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnComleted, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnNotCompleted, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtService1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtfranchise1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSecondary1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtComplaintType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtprimary1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtComplaintGivento, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel41, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtComplaintGivenBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rptdSerialNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel39, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboApex1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel30, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTag1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSerialNo1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSize1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtModel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMake1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAssetType1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel38, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLocation1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCountry1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtState1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCity1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPhn1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel32, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel33, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel34, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel35, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel36, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel37, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel26, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel28, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel29, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel31, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOutletName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.btnsend, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCopy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.chkManual, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPageView1
        '
        Me.RadPageView1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Location = New System.Drawing.Point(3, 91)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage2
        Me.RadPageView1.Size = New System.Drawing.Size(780, 537)
        Me.RadPageView1.TabIndex = 0
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.chksparepart)
        Me.RadPageViewPage1.Controls.Add(Me.txtpendcode)
        Me.RadPageViewPage1.Controls.Add(Me.txtsecresn)
        Me.RadPageViewPage1.Controls.Add(Me.txtresponse)
        Me.RadPageViewPage1.Controls.Add(Me.txtpendrsn)
        Me.RadPageViewPage1.Controls.Add(Me.txtseccode)
        Me.RadPageViewPage1.Controls.Add(Me.txtprimarydesc)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel13)
        Me.RadPageViewPage1.Controls.Add(Me.txtreptserialno)
        Me.RadPageViewPage1.Controls.Add(Me.txtprimarycode)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel22)
        Me.RadPageViewPage1.Controls.Add(Me.txtapexno)
        Me.RadPageViewPage1.Controls.Add(Me.rdpending)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel17)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel20)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel27)
        Me.RadPageViewPage1.Controls.Add(Me.rdcomplt)
        Me.RadPageViewPage1.Controls.Add(Me.txtcmpldt)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.txtsrvcdlr)
        Me.RadPageViewPage1.Controls.Add(Me.txtremarks)
        Me.RadPageViewPage1.Controls.Add(Me.rdnotcmplt)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel23)
        Me.RadPageViewPage1.Controls.Add(Me.txtsrvcdlrname)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel18)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel21)
        Me.RadPageViewPage1.Controls.Add(Me.txttdmdesc)
        Me.RadPageViewPage1.Controls.Add(Me.txttdmcode)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel19)
        Me.RadPageViewPage1.Controls.Add(Me.txtcompgivento)
        Me.RadPageViewPage1.Controls.Add(Me.txtcompgivenby)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel16)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel15)
        Me.RadPageViewPage1.Controls.Add(Me.txtphnno)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel14)
        Me.RadPageViewPage1.Controls.Add(Me.txtdistributor)
        Me.RadPageViewPage1.Controls.Add(Me.Type)
        Me.RadPageViewPage1.Controls.Add(Me.txttagno)
        Me.RadPageViewPage1.Controls.Add(Me.txtserialno)
        Me.RadPageViewPage1.Controls.Add(Me.txtSize)
        Me.RadPageViewPage1.Controls.Add(Me.txtmodel)
        Me.RadPageViewPage1.Controls.Add(Me.txtmake)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel10)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel6)
        Me.RadPageViewPage1.Controls.Add(Me.txtassetdesc)
        Me.RadPageViewPage1.Controls.Add(Me.txtassetcode)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel9)
        Me.RadPageViewPage1.Controls.Add(Me.txtcmplntdesc)
        Me.RadPageViewPage1.Controls.Add(Me.txtcomplntcode)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel8)
        Me.RadPageViewPage1.Controls.Add(Me.txtcountry)
        Me.RadPageViewPage1.Controls.Add(Me.txtstate)
        Me.RadPageViewPage1.Controls.Add(Me.txtcity)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.txtoutletadd)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.txtoutletdesc)
        Me.RadPageViewPage1.Controls.Add(Me.txtoutletcode)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(100.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(759, 489)
        Me.RadPageViewPage1.Text = "Complaint Detail"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.gv1)
        Me.RadGroupBox2.HeaderText = "Spare Part(s)"
        Me.RadGroupBox2.Location = New System.Drawing.Point(3, 353)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(9)
        Me.RadGroupBox2.Size = New System.Drawing.Size(480, 136)
        Me.RadGroupBox2.TabIndex = 18
        Me.RadGroupBox2.Text = "Spare Part(s)"
        '
        'gv1
        '
        Me.gv1.Location = New System.Drawing.Point(6, 22)
        '
        '
        '
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowFilteringRow = False
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(469, 109)
        Me.gv1.TabIndex = 0
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.txtworkorderno)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel25)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel5)
        Me.RadGroupBox1.Controls.Add(Me.txtbillno)
        Me.RadGroupBox1.Controls.Add(Me.txtaddamt)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel7)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel11)
        Me.RadGroupBox1.Controls.Add(Me.txtbillamt)
        Me.RadGroupBox1.HeaderText = "Completion Detail"
        Me.RadGroupBox1.Location = New System.Drawing.Point(490, 352)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(257, 137)
        Me.RadGroupBox1.TabIndex = 72
        Me.RadGroupBox1.Text = "Completion Detail"
        Me.RadGroupBox1.Visible = False
        '
        'txtworkorderno
        '
        Me.txtworkorderno.CalculationExpression = Nothing
        Me.txtworkorderno.FieldCode = Nothing
        Me.txtworkorderno.FieldDesc = Nothing
        Me.txtworkorderno.FieldMaxLength = 0
        Me.txtworkorderno.FieldName = Nothing
        Me.txtworkorderno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtworkorderno.isCalculatedField = False
        Me.txtworkorderno.IsSourceFromTable = False
        Me.txtworkorderno.IsSourceFromValueList = False
        Me.txtworkorderno.IsUnique = False
        Me.txtworkorderno.Location = New System.Drawing.Point(110, 45)
        Me.txtworkorderno.MaxLength = 50
        Me.txtworkorderno.MendatroryField = False
        Me.txtworkorderno.MyLinkLable1 = Me.MyLabel25
        Me.txtworkorderno.MyLinkLable2 = Nothing
        Me.txtworkorderno.Name = "txtworkorderno"
        Me.txtworkorderno.ReferenceFieldDesc = Nothing
        Me.txtworkorderno.ReferenceFieldName = Nothing
        Me.txtworkorderno.ReferenceTableName = Nothing
        Me.txtworkorderno.Size = New System.Drawing.Size(136, 18)
        Me.txtworkorderno.TabIndex = 2
        '
        'MyLabel25
        '
        Me.MyLabel25.FieldName = Nothing
        Me.MyLabel25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel25.Location = New System.Drawing.Point(5, 45)
        Me.MyLabel25.Name = "MyLabel25"
        Me.MyLabel25.Size = New System.Drawing.Size(85, 16)
        Me.MyLabel25.TabIndex = 70
        Me.MyLabel25.Text = "Work Order No."
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(5, 69)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(42, 16)
        Me.MyLabel5.TabIndex = 63
        Me.MyLabel5.Text = "Bill No."
        '
        'txtbillno
        '
        Me.txtbillno.CalculationExpression = Nothing
        Me.txtbillno.FieldCode = Nothing
        Me.txtbillno.FieldDesc = Nothing
        Me.txtbillno.FieldMaxLength = 0
        Me.txtbillno.FieldName = Nothing
        Me.txtbillno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbillno.isCalculatedField = False
        Me.txtbillno.IsSourceFromTable = False
        Me.txtbillno.IsSourceFromValueList = False
        Me.txtbillno.IsUnique = False
        Me.txtbillno.Location = New System.Drawing.Point(110, 67)
        Me.txtbillno.MaxLength = 50
        Me.txtbillno.MendatroryField = False
        Me.txtbillno.MyLinkLable1 = Me.MyLabel5
        Me.txtbillno.MyLinkLable2 = Nothing
        Me.txtbillno.Name = "txtbillno"
        Me.txtbillno.ReferenceFieldDesc = Nothing
        Me.txtbillno.ReferenceFieldName = Nothing
        Me.txtbillno.ReferenceTableName = Nothing
        Me.txtbillno.Size = New System.Drawing.Size(137, 18)
        Me.txtbillno.TabIndex = 3
        '
        'txtaddamt
        '
        Me.txtaddamt.CalculationExpression = Nothing
        Me.txtaddamt.FieldCode = Nothing
        Me.txtaddamt.FieldDesc = Nothing
        Me.txtaddamt.FieldMaxLength = 0
        Me.txtaddamt.FieldName = Nothing
        Me.txtaddamt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtaddamt.isCalculatedField = False
        Me.txtaddamt.IsSourceFromTable = False
        Me.txtaddamt.IsSourceFromValueList = False
        Me.txtaddamt.IsUnique = False
        Me.txtaddamt.Location = New System.Drawing.Point(110, 110)
        Me.txtaddamt.MaxLength = 50
        Me.txtaddamt.MendatroryField = False
        Me.txtaddamt.MyLinkLable1 = Me.MyLabel11
        Me.txtaddamt.MyLinkLable2 = Nothing
        Me.txtaddamt.Name = "txtaddamt"
        Me.txtaddamt.ReferenceFieldDesc = Nothing
        Me.txtaddamt.ReferenceFieldName = Nothing
        Me.txtaddamt.ReferenceTableName = Nothing
        Me.txtaddamt.Size = New System.Drawing.Size(137, 18)
        Me.txtaddamt.TabIndex = 5
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel11.Location = New System.Drawing.Point(5, 110)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(102, 16)
        Me.MyLabel11.TabIndex = 65
        Me.MyLabel11.Text = "Additional Charges"
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(5, 91)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel7.TabIndex = 64
        Me.MyLabel7.Text = "Bill Amount"
        '
        'txtbillamt
        '
        Me.txtbillamt.CalculationExpression = Nothing
        Me.txtbillamt.FieldCode = Nothing
        Me.txtbillamt.FieldDesc = Nothing
        Me.txtbillamt.FieldMaxLength = 0
        Me.txtbillamt.FieldName = Nothing
        Me.txtbillamt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbillamt.isCalculatedField = False
        Me.txtbillamt.IsSourceFromTable = False
        Me.txtbillamt.IsSourceFromValueList = False
        Me.txtbillamt.IsUnique = False
        Me.txtbillamt.Location = New System.Drawing.Point(110, 89)
        Me.txtbillamt.MaxLength = 50
        Me.txtbillamt.MendatroryField = False
        Me.txtbillamt.MyLinkLable1 = Me.MyLabel7
        Me.txtbillamt.MyLinkLable2 = Nothing
        Me.txtbillamt.Name = "txtbillamt"
        Me.txtbillamt.ReferenceFieldDesc = Nothing
        Me.txtbillamt.ReferenceFieldName = Nothing
        Me.txtbillamt.ReferenceTableName = Nothing
        Me.txtbillamt.Size = New System.Drawing.Size(137, 18)
        Me.txtbillamt.TabIndex = 4
        '
        'chksparepart
        '
        Me.chksparepart.Location = New System.Drawing.Point(283, 276)
        Me.chksparepart.MyLinkLable1 = Nothing
        Me.chksparepart.MyLinkLable2 = Nothing
        Me.chksparepart.Name = "chksparepart"
        Me.chksparepart.Size = New System.Drawing.Size(100, 18)
        Me.chksparepart.TabIndex = 15
        Me.chksparepart.Tag1 = Nothing
        Me.chksparepart.Text = "Used Spare Part"
        '
        'txtpendcode
        '
        Me.txtpendcode.CalculationExpression = Nothing
        Me.txtpendcode.Enabled = False
        Me.txtpendcode.FieldCode = Nothing
        Me.txtpendcode.FieldDesc = Nothing
        Me.txtpendcode.FieldMaxLength = 0
        Me.txtpendcode.FieldName = Nothing
        Me.txtpendcode.isCalculatedField = False
        Me.txtpendcode.IsSourceFromTable = False
        Me.txtpendcode.IsSourceFromValueList = False
        Me.txtpendcode.IsUnique = False
        Me.txtpendcode.Location = New System.Drawing.Point(112, 305)
        Me.txtpendcode.MendatroryField = True
        Me.txtpendcode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpendcode.MyLinkLable1 = Me.MyLabel22
        Me.txtpendcode.MyLinkLable2 = Me.txtpendrsn
        Me.txtpendcode.MyReadOnly = False
        Me.txtpendcode.MyShowMasterFormButton = False
        Me.txtpendcode.Name = "txtpendcode"
        Me.txtpendcode.ReferenceFieldDesc = Nothing
        Me.txtpendcode.ReferenceFieldName = Nothing
        Me.txtpendcode.ReferenceTableName = Nothing
        Me.txtpendcode.Size = New System.Drawing.Size(143, 18)
        Me.txtpendcode.TabIndex = 14
        Me.txtpendcode.Value = ""
        '
        'MyLabel22
        '
        Me.MyLabel22.FieldName = Nothing
        Me.MyLabel22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel22.Location = New System.Drawing.Point(0, 305)
        Me.MyLabel22.Name = "MyLabel22"
        Me.MyLabel22.Size = New System.Drawing.Size(90, 16)
        Me.MyLabel22.TabIndex = 53
        Me.MyLabel22.Text = "Pending Reason"
        '
        'txtpendrsn
        '
        Me.txtpendrsn.AutoSize = False
        Me.txtpendrsn.BorderVisible = True
        Me.txtpendrsn.Enabled = False
        Me.txtpendrsn.FieldName = Nothing
        Me.txtpendrsn.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpendrsn.Location = New System.Drawing.Point(256, 305)
        Me.txtpendrsn.Name = "txtpendrsn"
        Me.txtpendrsn.Size = New System.Drawing.Size(242, 18)
        Me.txtpendrsn.TabIndex = 74
        Me.txtpendrsn.TextWrap = False
        '
        'txtsecresn
        '
        Me.txtsecresn.AutoSize = False
        Me.txtsecresn.BorderVisible = True
        Me.txtsecresn.FieldName = Nothing
        Me.txtsecresn.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsecresn.Location = New System.Drawing.Point(256, 182)
        Me.txtsecresn.Name = "txtsecresn"
        Me.txtsecresn.Size = New System.Drawing.Size(242, 18)
        Me.txtsecresn.TabIndex = 44
        Me.txtsecresn.TextWrap = False
        '
        'txtresponse
        '
        Me.txtresponse.CalculationExpression = Nothing
        Me.txtresponse.FieldCode = Nothing
        Me.txtresponse.FieldDesc = Nothing
        Me.txtresponse.FieldMaxLength = 0
        Me.txtresponse.FieldName = Nothing
        Me.txtresponse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtresponse.isCalculatedField = False
        Me.txtresponse.IsSourceFromTable = False
        Me.txtresponse.IsSourceFromValueList = False
        Me.txtresponse.IsUnique = False
        Me.txtresponse.Location = New System.Drawing.Point(617, 299)
        Me.txtresponse.MaxLength = 50
        Me.txtresponse.MendatroryField = False
        Me.txtresponse.MyLinkLable1 = Me.MyLabel13
        Me.txtresponse.MyLinkLable2 = Nothing
        Me.txtresponse.Name = "txtresponse"
        Me.txtresponse.ReferenceFieldDesc = Nothing
        Me.txtresponse.ReferenceFieldName = Nothing
        Me.txtresponse.ReferenceTableName = Nothing
        Me.txtresponse.Size = New System.Drawing.Size(130, 18)
        Me.txtresponse.TabIndex = 49
        Me.txtresponse.TabStop = False
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel13.Location = New System.Drawing.Point(526, 300)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(86, 16)
        Me.MyLabel13.TabIndex = 60
        Me.MyLabel13.Text = "Response Time"
        '
        'txtseccode
        '
        Me.txtseccode.CalculationExpression = Nothing
        Me.txtseccode.FieldCode = Nothing
        Me.txtseccode.FieldDesc = Nothing
        Me.txtseccode.FieldMaxLength = 0
        Me.txtseccode.FieldName = Nothing
        Me.txtseccode.isCalculatedField = False
        Me.txtseccode.IsSourceFromTable = False
        Me.txtseccode.IsSourceFromValueList = False
        Me.txtseccode.IsUnique = False
        Me.txtseccode.Location = New System.Drawing.Point(112, 182)
        Me.txtseccode.MendatroryField = False
        Me.txtseccode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtseccode.MyLinkLable1 = Me.MyLabel21
        Me.txtseccode.MyLinkLable2 = Me.txtsecresn
        Me.txtseccode.MyReadOnly = False
        Me.txtseccode.MyShowMasterFormButton = False
        Me.txtseccode.Name = "txtseccode"
        Me.txtseccode.ReferenceFieldDesc = Nothing
        Me.txtseccode.ReferenceFieldName = Nothing
        Me.txtseccode.ReferenceTableName = Nothing
        Me.txtseccode.Size = New System.Drawing.Size(143, 18)
        Me.txtseccode.TabIndex = 8
        Me.txtseccode.Value = ""
        '
        'MyLabel21
        '
        Me.MyLabel21.FieldName = Nothing
        Me.MyLabel21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel21.Location = New System.Drawing.Point(0, 182)
        Me.MyLabel21.Name = "MyLabel21"
        Me.MyLabel21.Size = New System.Drawing.Size(103, 16)
        Me.MyLabel21.TabIndex = 52
        Me.MyLabel21.Text = "Secondary Reason"
        '
        'txtprimarydesc
        '
        Me.txtprimarydesc.AutoSize = False
        Me.txtprimarydesc.BorderVisible = True
        Me.txtprimarydesc.FieldName = Nothing
        Me.txtprimarydesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtprimarydesc.Location = New System.Drawing.Point(256, 137)
        Me.txtprimarydesc.Name = "txtprimarydesc"
        Me.txtprimarydesc.Size = New System.Drawing.Size(242, 18)
        Me.txtprimarydesc.TabIndex = 43
        Me.txtprimarydesc.TextWrap = False
        '
        'txtreptserialno
        '
        Me.txtreptserialno.AutoSize = False
        Me.txtreptserialno.FieldName = Nothing
        Me.txtreptserialno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtreptserialno.Location = New System.Drawing.Point(506, 138)
        Me.txtreptserialno.Name = "txtreptserialno"
        Me.txtreptserialno.Size = New System.Drawing.Size(242, 18)
        Me.txtreptserialno.TabIndex = 41
        Me.txtreptserialno.Text = "Serial No. Repeated 0 Times"
        Me.txtreptserialno.TextWrap = False
        '
        'txtprimarycode
        '
        Me.txtprimarycode.CalculationExpression = Nothing
        Me.txtprimarycode.FieldCode = Nothing
        Me.txtprimarycode.FieldDesc = Nothing
        Me.txtprimarycode.FieldMaxLength = 0
        Me.txtprimarycode.FieldName = Nothing
        Me.txtprimarycode.isCalculatedField = False
        Me.txtprimarycode.IsSourceFromTable = False
        Me.txtprimarycode.IsSourceFromValueList = False
        Me.txtprimarycode.IsUnique = False
        Me.txtprimarycode.Location = New System.Drawing.Point(112, 137)
        Me.txtprimarycode.MendatroryField = False
        Me.txtprimarycode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtprimarycode.MyLinkLable1 = Me.MyLabel27
        Me.txtprimarycode.MyLinkLable2 = Me.txtprimarydesc
        Me.txtprimarycode.MyReadOnly = False
        Me.txtprimarycode.MyShowMasterFormButton = False
        Me.txtprimarycode.Name = "txtprimarycode"
        Me.txtprimarycode.ReferenceFieldDesc = Nothing
        Me.txtprimarycode.ReferenceFieldName = Nothing
        Me.txtprimarycode.ReferenceTableName = Nothing
        Me.txtprimarycode.Size = New System.Drawing.Size(143, 18)
        Me.txtprimarycode.TabIndex = 6
        Me.txtprimarycode.Value = ""
        '
        'MyLabel27
        '
        Me.MyLabel27.FieldName = Nothing
        Me.MyLabel27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel27.Location = New System.Drawing.Point(0, 139)
        Me.MyLabel27.Name = "MyLabel27"
        Me.MyLabel27.Size = New System.Drawing.Size(87, 16)
        Me.MyLabel27.TabIndex = 41
        Me.MyLabel27.Text = "Primary Reason"
        '
        'txtapexno
        '
        Me.txtapexno.CalculationExpression = Nothing
        Me.txtapexno.DropDownAnimationEnabled = True
        Me.txtapexno.FieldCode = Nothing
        Me.txtapexno.FieldDesc = Nothing
        Me.txtapexno.FieldMaxLength = 0
        Me.txtapexno.FieldName = Nothing
        Me.txtapexno.isCalculatedField = False
        Me.txtapexno.IsSourceFromTable = False
        Me.txtapexno.IsSourceFromValueList = False
        Me.txtapexno.IsUnique = False
        Me.txtapexno.Location = New System.Drawing.Point(595, 112)
        Me.txtapexno.MendatroryField = True
        Me.txtapexno.MyLinkLable1 = Nothing
        Me.txtapexno.MyLinkLable2 = Nothing
        Me.txtapexno.Name = "txtapexno"
        Me.txtapexno.ReferenceFieldDesc = Nothing
        Me.txtapexno.ReferenceFieldName = Nothing
        Me.txtapexno.ReferenceTableName = Nothing
        Me.txtapexno.Size = New System.Drawing.Size(153, 20)
        Me.txtapexno.TabIndex = 5
        '
        'rdpending
        '
        Me.rdpending.Location = New System.Drawing.Point(117, 276)
        Me.rdpending.MyLinkLable1 = Nothing
        Me.rdpending.MyLinkLable2 = Nothing
        Me.rdpending.Name = "rdpending"
        Me.rdpending.Size = New System.Drawing.Size(61, 18)
        Me.rdpending.TabIndex = 11
        Me.rdpending.Text = "Pending"
        '
        'MyLabel17
        '
        Me.MyLabel17.FieldName = Nothing
        Me.MyLabel17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel17.Location = New System.Drawing.Point(0, 251)
        Me.MyLabel17.Name = "MyLabel17"
        Me.MyLabel17.Size = New System.Drawing.Size(96, 16)
        Me.MyLabel17.TabIndex = 50
        Me.MyLabel17.Text = "Service Executive"
        '
        'MyLabel20
        '
        Me.MyLabel20.FieldName = Nothing
        Me.MyLabel20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel20.Location = New System.Drawing.Point(516, 276)
        Me.MyLabel20.Name = "MyLabel20"
        Me.MyLabel20.Size = New System.Drawing.Size(88, 16)
        Me.MyLabel20.TabIndex = 59
        Me.MyLabel20.Text = "Completed Date"
        '
        'rdcomplt
        '
        Me.rdcomplt.Location = New System.Drawing.Point(192, 276)
        Me.rdcomplt.MyLinkLable1 = Nothing
        Me.rdcomplt.MyLinkLable2 = Nothing
        Me.rdcomplt.Name = "rdcomplt"
        Me.rdcomplt.Size = New System.Drawing.Size(75, 18)
        Me.rdcomplt.TabIndex = 12
        Me.rdcomplt.Text = "Completed"
        '
        'txtcmpldt
        '
        Me.txtcmpldt.CalculationExpression = Nothing
        Me.txtcmpldt.CustomFormat = "dd/MM/yyyy h:mm:ss tt"
        Me.txtcmpldt.FieldCode = Nothing
        Me.txtcmpldt.FieldDesc = Nothing
        Me.txtcmpldt.FieldMaxLength = 0
        Me.txtcmpldt.FieldName = Nothing
        Me.txtcmpldt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcmpldt.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtcmpldt.isCalculatedField = False
        Me.txtcmpldt.IsSourceFromTable = False
        Me.txtcmpldt.IsSourceFromValueList = False
        Me.txtcmpldt.IsUnique = False
        Me.txtcmpldt.Location = New System.Drawing.Point(607, 275)
        Me.txtcmpldt.MendatroryField = False
        Me.txtcmpldt.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtcmpldt.MyLinkLable1 = Me.MyLabel20
        Me.txtcmpldt.MyLinkLable2 = Nothing
        Me.txtcmpldt.Name = "txtcmpldt"
        Me.txtcmpldt.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtcmpldt.ReferenceFieldDesc = Nothing
        Me.txtcmpldt.ReferenceFieldName = Nothing
        Me.txtcmpldt.ReferenceTableName = Nothing
        Me.txtcmpldt.Size = New System.Drawing.Size(141, 18)
        Me.txtcmpldt.TabIndex = 13
        Me.txtcmpldt.TabStop = False
        Me.txtcmpldt.Text = "13/06/2011 11:29:49 AM"
        Me.txtcmpldt.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(-1, 91)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(99, 16)
        Me.MyLabel4.TabIndex = 30
        Me.MyLabel4.Text = "Make/ Model/ Size"
        '
        'txtsrvcdlr
        '
        Me.txtsrvcdlr.CalculationExpression = Nothing
        Me.txtsrvcdlr.FieldCode = Nothing
        Me.txtsrvcdlr.FieldDesc = Nothing
        Me.txtsrvcdlr.FieldMaxLength = 0
        Me.txtsrvcdlr.FieldName = Nothing
        Me.txtsrvcdlr.isCalculatedField = False
        Me.txtsrvcdlr.IsSourceFromTable = False
        Me.txtsrvcdlr.IsSourceFromValueList = False
        Me.txtsrvcdlr.IsUnique = False
        Me.txtsrvcdlr.Location = New System.Drawing.Point(112, 250)
        Me.txtsrvcdlr.MendatroryField = True
        Me.txtsrvcdlr.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsrvcdlr.MyLinkLable1 = Me.MyLabel17
        Me.txtsrvcdlr.MyLinkLable2 = Me.txtsrvcdlrname
        Me.txtsrvcdlr.MyReadOnly = False
        Me.txtsrvcdlr.MyShowMasterFormButton = False
        Me.txtsrvcdlr.Name = "txtsrvcdlr"
        Me.txtsrvcdlr.ReferenceFieldDesc = Nothing
        Me.txtsrvcdlr.ReferenceFieldName = Nothing
        Me.txtsrvcdlr.ReferenceTableName = Nothing
        Me.txtsrvcdlr.Size = New System.Drawing.Size(143, 18)
        Me.txtsrvcdlr.TabIndex = 15
        Me.txtsrvcdlr.Value = ""
        '
        'txtsrvcdlrname
        '
        Me.txtsrvcdlrname.AutoSize = False
        Me.txtsrvcdlrname.BorderVisible = True
        Me.txtsrvcdlrname.Enabled = False
        Me.txtsrvcdlrname.FieldName = Nothing
        Me.txtsrvcdlrname.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsrvcdlrname.Location = New System.Drawing.Point(256, 250)
        Me.txtsrvcdlrname.Name = "txtsrvcdlrname"
        Me.txtsrvcdlrname.Size = New System.Drawing.Size(242, 18)
        Me.txtsrvcdlrname.TabIndex = 52
        Me.txtsrvcdlrname.TextWrap = False
        '
        'txtremarks
        '
        Me.txtremarks.AutoSize = False
        Me.txtremarks.CalculationExpression = Nothing
        Me.txtremarks.FieldCode = Nothing
        Me.txtremarks.FieldDesc = Nothing
        Me.txtremarks.FieldMaxLength = 0
        Me.txtremarks.FieldName = Nothing
        Me.txtremarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtremarks.isCalculatedField = False
        Me.txtremarks.IsSourceFromTable = False
        Me.txtremarks.IsSourceFromValueList = False
        Me.txtremarks.IsUnique = False
        Me.txtremarks.Location = New System.Drawing.Point(111, 328)
        Me.txtremarks.MaxLength = 50
        Me.txtremarks.MendatroryField = False
        Me.txtremarks.Multiline = True
        Me.txtremarks.MyLinkLable1 = Me.MyLabel23
        Me.txtremarks.MyLinkLable2 = Nothing
        Me.txtremarks.Name = "txtremarks"
        Me.txtremarks.ReferenceFieldDesc = Nothing
        Me.txtremarks.ReferenceFieldName = Nothing
        Me.txtremarks.ReferenceTableName = Nothing
        Me.txtremarks.Size = New System.Drawing.Size(636, 19)
        Me.txtremarks.TabIndex = 17
        '
        'MyLabel23
        '
        Me.MyLabel23.FieldName = Nothing
        Me.MyLabel23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel23.Location = New System.Drawing.Point(-1, 329)
        Me.MyLabel23.Name = "MyLabel23"
        Me.MyLabel23.Size = New System.Drawing.Size(51, 16)
        Me.MyLabel23.TabIndex = 55
        Me.MyLabel23.Text = "Remarks"
        '
        'rdnotcmplt
        '
        Me.rdnotcmplt.Location = New System.Drawing.Point(1, 276)
        Me.rdnotcmplt.MyLinkLable1 = Nothing
        Me.rdnotcmplt.MyLinkLable2 = Nothing
        Me.rdnotcmplt.Name = "rdnotcmplt"
        Me.rdnotcmplt.Size = New System.Drawing.Size(97, 18)
        Me.rdnotcmplt.TabIndex = 118
        Me.rdnotcmplt.Text = "Not Completed"
        Me.rdnotcmplt.Visible = False
        '
        'MyLabel18
        '
        Me.MyLabel18.FieldName = Nothing
        Me.MyLabel18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel18.Location = New System.Drawing.Point(464, 114)
        Me.MyLabel18.Name = "MyLabel18"
        Me.MyLabel18.Size = New System.Drawing.Size(123, 16)
        Me.MyLabel18.TabIndex = 51
        Me.MyLabel18.Text = "Apex Pending W/O No."
        '
        'txttdmdesc
        '
        Me.txttdmdesc.AutoSize = False
        Me.txttdmdesc.BorderVisible = True
        Me.txttdmdesc.Enabled = False
        Me.txttdmdesc.FieldName = Nothing
        Me.txttdmdesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txttdmdesc.Location = New System.Drawing.Point(256, 228)
        Me.txttdmdesc.Name = "txttdmdesc"
        Me.txttdmdesc.Size = New System.Drawing.Size(242, 18)
        Me.txttdmdesc.TabIndex = 55
        Me.txttdmdesc.TextWrap = False
        '
        'txttdmcode
        '
        Me.txttdmcode.CalculationExpression = Nothing
        Me.txttdmcode.FieldCode = Nothing
        Me.txttdmcode.FieldDesc = Nothing
        Me.txttdmcode.FieldMaxLength = 0
        Me.txttdmcode.FieldName = Nothing
        Me.txttdmcode.isCalculatedField = False
        Me.txttdmcode.IsSourceFromTable = False
        Me.txttdmcode.IsSourceFromValueList = False
        Me.txttdmcode.IsUnique = False
        Me.txttdmcode.Location = New System.Drawing.Point(112, 228)
        Me.txttdmcode.MendatroryField = True
        Me.txttdmcode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txttdmcode.MyLinkLable1 = Me.MyLabel19
        Me.txttdmcode.MyLinkLable2 = Me.txttdmdesc
        Me.txttdmcode.MyReadOnly = False
        Me.txttdmcode.MyShowMasterFormButton = False
        Me.txttdmcode.Name = "txttdmcode"
        Me.txttdmcode.ReferenceFieldDesc = Nothing
        Me.txttdmcode.ReferenceFieldName = Nothing
        Me.txttdmcode.ReferenceTableName = Nothing
        Me.txttdmcode.Size = New System.Drawing.Size(143, 18)
        Me.txttdmcode.TabIndex = 16
        Me.txttdmcode.Value = ""
        '
        'MyLabel19
        '
        Me.MyLabel19.FieldName = Nothing
        Me.MyLabel19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel19.Location = New System.Drawing.Point(0, 229)
        Me.MyLabel19.Name = "MyLabel19"
        Me.MyLabel19.Size = New System.Drawing.Size(56, 16)
        Me.MyLabel19.TabIndex = 53
        Me.MyLabel19.Text = "Franchise"
        '
        'txtcompgivento
        '
        Me.txtcompgivento.CalculationExpression = Nothing
        Me.txtcompgivento.FieldCode = Nothing
        Me.txtcompgivento.FieldDesc = Nothing
        Me.txtcompgivento.FieldMaxLength = 0
        Me.txtcompgivento.FieldName = Nothing
        Me.txtcompgivento.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcompgivento.isCalculatedField = False
        Me.txtcompgivento.IsSourceFromTable = False
        Me.txtcompgivento.IsSourceFromValueList = False
        Me.txtcompgivento.IsUnique = False
        Me.txtcompgivento.Location = New System.Drawing.Point(493, 205)
        Me.txtcompgivento.MaxLength = 50
        Me.txtcompgivento.MendatroryField = False
        Me.txtcompgivento.MyLinkLable1 = Me.MyLabel16
        Me.txtcompgivento.MyLinkLable2 = Nothing
        Me.txtcompgivento.Name = "txtcompgivento"
        Me.txtcompgivento.ReferenceFieldDesc = Nothing
        Me.txtcompgivento.ReferenceFieldName = Nothing
        Me.txtcompgivento.ReferenceTableName = Nothing
        Me.txtcompgivento.Size = New System.Drawing.Size(255, 18)
        Me.txtcompgivento.TabIndex = 10
        '
        'MyLabel16
        '
        Me.MyLabel16.FieldName = Nothing
        Me.MyLabel16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel16.Location = New System.Drawing.Point(377, 206)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(106, 16)
        Me.MyLabel16.TabIndex = 44
        Me.MyLabel16.Text = "Complaint Given To"
        '
        'txtcompgivenby
        '
        Me.txtcompgivenby.CalculationExpression = Nothing
        Me.txtcompgivenby.FieldCode = Nothing
        Me.txtcompgivenby.FieldDesc = Nothing
        Me.txtcompgivenby.FieldMaxLength = 0
        Me.txtcompgivenby.FieldName = Nothing
        Me.txtcompgivenby.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcompgivenby.isCalculatedField = False
        Me.txtcompgivenby.IsSourceFromTable = False
        Me.txtcompgivenby.IsSourceFromValueList = False
        Me.txtcompgivenby.IsUnique = False
        Me.txtcompgivenby.Location = New System.Drawing.Point(112, 205)
        Me.txtcompgivenby.MaxLength = 50
        Me.txtcompgivenby.MendatroryField = False
        Me.txtcompgivenby.MyLinkLable1 = Me.MyLabel15
        Me.txtcompgivenby.MyLinkLable2 = Nothing
        Me.txtcompgivenby.Name = "txtcompgivenby"
        Me.txtcompgivenby.ReferenceFieldDesc = Nothing
        Me.txtcompgivenby.ReferenceFieldName = Nothing
        Me.txtcompgivenby.ReferenceTableName = Nothing
        Me.txtcompgivenby.Size = New System.Drawing.Size(255, 18)
        Me.txtcompgivenby.TabIndex = 9
        '
        'MyLabel15
        '
        Me.MyLabel15.FieldName = Nothing
        Me.MyLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel15.Location = New System.Drawing.Point(0, 205)
        Me.MyLabel15.Name = "MyLabel15"
        Me.MyLabel15.Size = New System.Drawing.Size(106, 16)
        Me.MyLabel15.TabIndex = 43
        Me.MyLabel15.Text = "Complaint Given By"
        '
        'txtphnno
        '
        Me.txtphnno.CalculationExpression = Nothing
        Me.txtphnno.FieldCode = Nothing
        Me.txtphnno.FieldDesc = Nothing
        Me.txtphnno.FieldMaxLength = 0
        Me.txtphnno.FieldName = Nothing
        Me.txtphnno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtphnno.isCalculatedField = False
        Me.txtphnno.IsSourceFromTable = False
        Me.txtphnno.IsSourceFromValueList = False
        Me.txtphnno.IsUnique = False
        Me.txtphnno.Location = New System.Drawing.Point(558, 47)
        Me.txtphnno.MaxLength = 50
        Me.txtphnno.MendatroryField = False
        Me.txtphnno.MyLinkLable1 = Me.MyLabel14
        Me.txtphnno.MyLinkLable2 = Nothing
        Me.txtphnno.Name = "txtphnno"
        Me.txtphnno.ReferenceFieldDesc = Nothing
        Me.txtphnno.ReferenceFieldName = Nothing
        Me.txtphnno.ReferenceTableName = Nothing
        Me.txtphnno.Size = New System.Drawing.Size(190, 18)
        Me.txtphnno.TabIndex = 1
        '
        'MyLabel14
        '
        Me.MyLabel14.FieldName = Nothing
        Me.MyLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel14.Location = New System.Drawing.Point(495, 47)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(60, 16)
        Me.MyLabel14.TabIndex = 45
        Me.MyLabel14.Text = "Phone No."
        '
        'txtdistributor
        '
        Me.txtdistributor.AutoSize = False
        Me.txtdistributor.BorderVisible = True
        Me.txtdistributor.FieldName = Nothing
        Me.txtdistributor.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdistributor.Location = New System.Drawing.Point(538, 3)
        Me.txtdistributor.Name = "txtdistributor"
        Me.txtdistributor.Size = New System.Drawing.Size(210, 18)
        Me.txtdistributor.TabIndex = 48
        Me.txtdistributor.TextWrap = False
        '
        'Type
        '
        Me.Type.FieldName = Nothing
        Me.Type.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Type.Location = New System.Drawing.Point(501, 3)
        Me.Type.Name = "Type"
        Me.Type.Size = New System.Drawing.Size(31, 16)
        Me.Type.TabIndex = 30
        Me.Type.Text = "Type"
        '
        'txttagno
        '
        Me.txttagno.CalculationExpression = Nothing
        Me.txttagno.FieldCode = Nothing
        Me.txttagno.FieldDesc = Nothing
        Me.txttagno.FieldMaxLength = 0
        Me.txttagno.FieldName = Nothing
        Me.txttagno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txttagno.isCalculatedField = False
        Me.txttagno.IsSourceFromTable = False
        Me.txttagno.IsSourceFromValueList = False
        Me.txttagno.IsUnique = False
        Me.txttagno.Location = New System.Drawing.Point(314, 114)
        Me.txttagno.MaxLength = 50
        Me.txttagno.MendatroryField = False
        Me.txttagno.MyLinkLable1 = Me.MyLabel10
        Me.txttagno.MyLinkLable2 = Nothing
        Me.txttagno.Name = "txttagno"
        Me.txttagno.ReferenceFieldDesc = Nothing
        Me.txttagno.ReferenceFieldName = Nothing
        Me.txttagno.ReferenceTableName = Nothing
        Me.txttagno.Size = New System.Drawing.Size(146, 18)
        Me.txttagno.TabIndex = 4
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(262, 114)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(46, 16)
        Me.MyLabel10.TabIndex = 44
        Me.MyLabel10.Text = "Tag No."
        '
        'txtserialno
        '
        Me.txtserialno.CalculationExpression = Nothing
        Me.txtserialno.FieldCode = Nothing
        Me.txtserialno.FieldDesc = Nothing
        Me.txtserialno.FieldMaxLength = 0
        Me.txtserialno.FieldName = Nothing
        Me.txtserialno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtserialno.isCalculatedField = False
        Me.txtserialno.IsSourceFromTable = False
        Me.txtserialno.IsSourceFromValueList = False
        Me.txtserialno.IsUnique = False
        Me.txtserialno.Location = New System.Drawing.Point(112, 114)
        Me.txtserialno.MaxLength = 50
        Me.txtserialno.MendatroryField = False
        Me.txtserialno.MyLinkLable1 = Me.MyLabel6
        Me.txtserialno.MyLinkLable2 = Nothing
        Me.txtserialno.Name = "txtserialno"
        Me.txtserialno.ReferenceFieldDesc = Nothing
        Me.txtserialno.ReferenceFieldName = Nothing
        Me.txtserialno.ReferenceTableName = Nothing
        Me.txtserialno.Size = New System.Drawing.Size(146, 18)
        Me.txtserialno.TabIndex = 3
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(0, 114)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(56, 16)
        Me.MyLabel6.TabIndex = 42
        Me.MyLabel6.Text = "Serial No."
        '
        'txtSize
        '
        Me.txtSize.AutoSize = False
        Me.txtSize.BorderVisible = True
        Me.txtSize.FieldName = Nothing
        Me.txtSize.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSize.Location = New System.Drawing.Point(538, 90)
        Me.txtSize.Name = "txtSize"
        Me.txtSize.Size = New System.Drawing.Size(210, 18)
        Me.txtSize.TabIndex = 45
        Me.txtSize.TextWrap = False
        '
        'txtmodel
        '
        Me.txtmodel.AutoSize = False
        Me.txtmodel.BorderVisible = True
        Me.txtmodel.FieldName = Nothing
        Me.txtmodel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtmodel.Location = New System.Drawing.Point(325, 90)
        Me.txtmodel.Name = "txtmodel"
        Me.txtmodel.Size = New System.Drawing.Size(210, 18)
        Me.txtmodel.TabIndex = 45
        Me.txtmodel.TextWrap = False
        '
        'txtmake
        '
        Me.txtmake.AutoSize = False
        Me.txtmake.BorderVisible = True
        Me.txtmake.FieldName = Nothing
        Me.txtmake.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtmake.Location = New System.Drawing.Point(112, 90)
        Me.txtmake.Name = "txtmake"
        Me.txtmake.Size = New System.Drawing.Size(210, 18)
        Me.txtmake.TabIndex = 44
        Me.txtmake.TextWrap = False
        '
        'txtassetdesc
        '
        Me.txtassetdesc.AutoSize = False
        Me.txtassetdesc.BorderVisible = True
        Me.txtassetdesc.FieldName = Nothing
        Me.txtassetdesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtassetdesc.Location = New System.Drawing.Point(256, 68)
        Me.txtassetdesc.Name = "txtassetdesc"
        Me.txtassetdesc.Size = New System.Drawing.Size(242, 18)
        Me.txtassetdesc.TabIndex = 43
        Me.txtassetdesc.TextWrap = False
        '
        'txtassetcode
        '
        Me.txtassetcode.CalculationExpression = Nothing
        Me.txtassetcode.FieldCode = Nothing
        Me.txtassetcode.FieldDesc = Nothing
        Me.txtassetcode.FieldMaxLength = 0
        Me.txtassetcode.FieldName = Nothing
        Me.txtassetcode.isCalculatedField = False
        Me.txtassetcode.IsSourceFromTable = False
        Me.txtassetcode.IsSourceFromValueList = False
        Me.txtassetcode.IsUnique = False
        Me.txtassetcode.Location = New System.Drawing.Point(112, 68)
        Me.txtassetcode.MendatroryField = True
        Me.txtassetcode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtassetcode.MyLinkLable1 = Me.MyLabel9
        Me.txtassetcode.MyLinkLable2 = Me.txtassetdesc
        Me.txtassetcode.MyReadOnly = False
        Me.txtassetcode.MyShowMasterFormButton = False
        Me.txtassetcode.Name = "txtassetcode"
        Me.txtassetcode.ReferenceFieldDesc = Nothing
        Me.txtassetcode.ReferenceFieldName = Nothing
        Me.txtassetcode.ReferenceTableName = Nothing
        Me.txtassetcode.Size = New System.Drawing.Size(143, 18)
        Me.txtassetcode.TabIndex = 2
        Me.txtassetcode.Value = ""
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(0, 71)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel9.TabIndex = 41
        Me.MyLabel9.Text = "Asset Type"
        '
        'txtcmplntdesc
        '
        Me.txtcmplntdesc.AutoSize = False
        Me.txtcmplntdesc.BorderVisible = True
        Me.txtcmplntdesc.FieldName = Nothing
        Me.txtcmplntdesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcmplntdesc.Location = New System.Drawing.Point(256, 159)
        Me.txtcmplntdesc.Name = "txtcmplntdesc"
        Me.txtcmplntdesc.Size = New System.Drawing.Size(242, 18)
        Me.txtcmplntdesc.TabIndex = 40
        Me.txtcmplntdesc.TextWrap = False
        '
        'txtcomplntcode
        '
        Me.txtcomplntcode.CalculationExpression = Nothing
        Me.txtcomplntcode.FieldCode = Nothing
        Me.txtcomplntcode.FieldDesc = Nothing
        Me.txtcomplntcode.FieldMaxLength = 0
        Me.txtcomplntcode.FieldName = Nothing
        Me.txtcomplntcode.isCalculatedField = False
        Me.txtcomplntcode.IsSourceFromTable = False
        Me.txtcomplntcode.IsSourceFromValueList = False
        Me.txtcomplntcode.IsUnique = False
        Me.txtcomplntcode.Location = New System.Drawing.Point(112, 159)
        Me.txtcomplntcode.MendatroryField = False
        Me.txtcomplntcode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcomplntcode.MyLinkLable1 = Me.MyLabel8
        Me.txtcomplntcode.MyLinkLable2 = Me.txtcmplntdesc
        Me.txtcomplntcode.MyReadOnly = False
        Me.txtcomplntcode.MyShowMasterFormButton = False
        Me.txtcomplntcode.Name = "txtcomplntcode"
        Me.txtcomplntcode.ReferenceFieldDesc = Nothing
        Me.txtcomplntcode.ReferenceFieldName = Nothing
        Me.txtcomplntcode.ReferenceTableName = Nothing
        Me.txtcomplntcode.Size = New System.Drawing.Size(143, 18)
        Me.txtcomplntcode.TabIndex = 7
        Me.txtcomplntcode.Value = ""
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(0, 161)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(86, 16)
        Me.MyLabel8.TabIndex = 38
        Me.MyLabel8.Text = "Complaint Type"
        '
        'txtcountry
        '
        Me.txtcountry.AutoSize = False
        Me.txtcountry.BorderVisible = True
        Me.txtcountry.FieldName = Nothing
        Me.txtcountry.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcountry.Location = New System.Drawing.Point(538, 25)
        Me.txtcountry.Name = "txtcountry"
        Me.txtcountry.Size = New System.Drawing.Size(210, 18)
        Me.txtcountry.TabIndex = 34
        Me.txtcountry.TextWrap = False
        '
        'txtstate
        '
        Me.txtstate.AutoSize = False
        Me.txtstate.BorderVisible = True
        Me.txtstate.FieldName = Nothing
        Me.txtstate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtstate.Location = New System.Drawing.Point(325, 25)
        Me.txtstate.Name = "txtstate"
        Me.txtstate.Size = New System.Drawing.Size(210, 18)
        Me.txtstate.TabIndex = 31
        Me.txtstate.TextWrap = False
        '
        'txtcity
        '
        Me.txtcity.AutoSize = False
        Me.txtcity.BorderVisible = True
        Me.txtcity.FieldName = Nothing
        Me.txtcity.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcity.Location = New System.Drawing.Point(112, 25)
        Me.txtcity.Name = "txtcity"
        Me.txtcity.Size = New System.Drawing.Size(210, 18)
        Me.txtcity.TabIndex = 31
        Me.txtcity.TextWrap = False
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(0, 25)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(104, 16)
        Me.MyLabel3.TabIndex = 29
        Me.MyLabel3.Text = "City /State/ Country"
        '
        'txtoutletadd
        '
        Me.txtoutletadd.AutoSize = False
        Me.txtoutletadd.BorderVisible = True
        Me.txtoutletadd.FieldName = Nothing
        Me.txtoutletadd.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtoutletadd.Location = New System.Drawing.Point(112, 47)
        Me.txtoutletadd.Name = "txtoutletadd"
        Me.txtoutletadd.Size = New System.Drawing.Size(383, 18)
        Me.txtoutletadd.TabIndex = 31
        Me.txtoutletadd.TextWrap = False
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(0, 48)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(49, 16)
        Me.MyLabel2.TabIndex = 29
        Me.MyLabel2.Text = "Location"
        '
        'txtoutletdesc
        '
        Me.txtoutletdesc.AutoSize = False
        Me.txtoutletdesc.BorderVisible = True
        Me.txtoutletdesc.FieldName = Nothing
        Me.txtoutletdesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtoutletdesc.Location = New System.Drawing.Point(256, 3)
        Me.txtoutletdesc.Name = "txtoutletdesc"
        Me.txtoutletdesc.Size = New System.Drawing.Size(242, 18)
        Me.txtoutletdesc.TabIndex = 30
        Me.txtoutletdesc.TextWrap = False
        '
        'txtoutletcode
        '
        Me.txtoutletcode.CalculationExpression = Nothing
        Me.txtoutletcode.FieldCode = Nothing
        Me.txtoutletcode.FieldDesc = Nothing
        Me.txtoutletcode.FieldMaxLength = 0
        Me.txtoutletcode.FieldName = Nothing
        Me.txtoutletcode.isCalculatedField = False
        Me.txtoutletcode.IsSourceFromTable = False
        Me.txtoutletcode.IsSourceFromValueList = False
        Me.txtoutletcode.IsUnique = False
        Me.txtoutletcode.Location = New System.Drawing.Point(112, 3)
        Me.txtoutletcode.MendatroryField = True
        Me.txtoutletcode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtoutletcode.MyLinkLable1 = Me.MyLabel1
        Me.txtoutletcode.MyLinkLable2 = Me.txtoutletdesc
        Me.txtoutletcode.MyReadOnly = False
        Me.txtoutletcode.MyShowMasterFormButton = False
        Me.txtoutletcode.Name = "txtoutletcode"
        Me.txtoutletcode.ReferenceFieldDesc = Nothing
        Me.txtoutletcode.ReferenceFieldName = Nothing
        Me.txtoutletcode.ReferenceTableName = Nothing
        Me.txtoutletcode.Size = New System.Drawing.Size(143, 18)
        Me.txtoutletcode.TabIndex = 0
        Me.txtoutletcode.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(0, 3)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(70, 16)
        Me.MyLabel1.TabIndex = 28
        Me.MyLabel1.Text = "Outlet Name"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.RadGroupBox5)
        Me.RadPageViewPage2.Controls.Add(Me.RadGroupBox4)
        Me.RadPageViewPage2.Controls.Add(Me.txtPending1)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel45)
        Me.RadPageViewPage2.Controls.Add(Me.txtResponseTime)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel42)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel43)
        Me.RadPageViewPage2.Controls.Add(Me.txtCompleteDate)
        Me.RadPageViewPage2.Controls.Add(Me.txtRemarks1)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel44)
        Me.RadPageViewPage2.Controls.Add(Me.ChkUserSparePart)
        Me.RadPageViewPage2.Controls.Add(Me.rbtnPending)
        Me.RadPageViewPage2.Controls.Add(Me.rbtnComleted)
        Me.RadPageViewPage2.Controls.Add(Me.rbtnNotCompleted)
        Me.RadPageViewPage2.Controls.Add(Me.txtService1)
        Me.RadPageViewPage2.Controls.Add(Me.txtfranchise1)
        Me.RadPageViewPage2.Controls.Add(Me.txtSecondary1)
        Me.RadPageViewPage2.Controls.Add(Me.txtComplaintType)
        Me.RadPageViewPage2.Controls.Add(Me.txtprimary1)
        Me.RadPageViewPage2.Controls.Add(Me.txtComplaintGivento)
        Me.RadPageViewPage2.Controls.Add(Me.txtComplaintGivenBy)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel41)
        Me.RadPageViewPage2.Controls.Add(Me.rptdSerialNo)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel39)
        Me.RadPageViewPage2.Controls.Add(Me.cboApex1)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel30)
        Me.RadPageViewPage2.Controls.Add(Me.txtTag1)
        Me.RadPageViewPage2.Controls.Add(Me.txtSerialNo1)
        Me.RadPageViewPage2.Controls.Add(Me.txtSize1)
        Me.RadPageViewPage2.Controls.Add(Me.txtModel1)
        Me.RadPageViewPage2.Controls.Add(Me.txtMake1)
        Me.RadPageViewPage2.Controls.Add(Me.txtAssetType1)
        Me.RadPageViewPage2.Controls.Add(Me.txtLocation1)
        Me.RadPageViewPage2.Controls.Add(Me.txtCountry1)
        Me.RadPageViewPage2.Controls.Add(Me.txtState1)
        Me.RadPageViewPage2.Controls.Add(Me.txtCity1)
        Me.RadPageViewPage2.Controls.Add(Me.txtPhn1)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel38)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel32)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel33)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel34)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel35)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel36)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel37)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel26)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel28)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel29)
        Me.RadPageViewPage2.Controls.Add(Me.lblCity)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel31)
        Me.RadPageViewPage2.Controls.Add(Me.txtType)
        Me.RadPageViewPage2.Controls.Add(Me.lblType)
        Me.RadPageViewPage2.Controls.Add(Me.txtOutletName)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel12)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(140.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(759, 489)
        Me.RadPageViewPage2.Text = "Manual Complaint Detail"
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.txtWorkOrder1)
        Me.RadGroupBox5.Controls.Add(Me.MyLabel46)
        Me.RadGroupBox5.Controls.Add(Me.MyLabel47)
        Me.RadGroupBox5.Controls.Add(Me.txtBillNo1)
        Me.RadGroupBox5.Controls.Add(Me.txtAdditionalCharge)
        Me.RadGroupBox5.Controls.Add(Me.MyLabel49)
        Me.RadGroupBox5.Controls.Add(Me.MyLabel48)
        Me.RadGroupBox5.Controls.Add(Me.txtBillAmt1)
        Me.RadGroupBox5.HeaderText = "Completion Detail"
        Me.RadGroupBox5.Location = New System.Drawing.Point(494, 342)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Size = New System.Drawing.Size(247, 137)
        Me.RadGroupBox5.TabIndex = 132
        Me.RadGroupBox5.Text = "Completion Detail"
        Me.RadGroupBox5.Visible = False
        '
        'txtWorkOrder1
        '
        Me.txtWorkOrder1.CalculationExpression = Nothing
        Me.txtWorkOrder1.FieldCode = Nothing
        Me.txtWorkOrder1.FieldDesc = Nothing
        Me.txtWorkOrder1.FieldMaxLength = 0
        Me.txtWorkOrder1.FieldName = Nothing
        Me.txtWorkOrder1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWorkOrder1.isCalculatedField = False
        Me.txtWorkOrder1.IsSourceFromTable = False
        Me.txtWorkOrder1.IsSourceFromValueList = False
        Me.txtWorkOrder1.IsUnique = False
        Me.txtWorkOrder1.Location = New System.Drawing.Point(110, 45)
        Me.txtWorkOrder1.MaxLength = 50
        Me.txtWorkOrder1.MendatroryField = False
        Me.txtWorkOrder1.MyLinkLable1 = Me.MyLabel46
        Me.txtWorkOrder1.MyLinkLable2 = Nothing
        Me.txtWorkOrder1.Name = "txtWorkOrder1"
        Me.txtWorkOrder1.ReferenceFieldDesc = Nothing
        Me.txtWorkOrder1.ReferenceFieldName = Nothing
        Me.txtWorkOrder1.ReferenceTableName = Nothing
        Me.txtWorkOrder1.Size = New System.Drawing.Size(132, 18)
        Me.txtWorkOrder1.TabIndex = 0
        '
        'MyLabel46
        '
        Me.MyLabel46.FieldName = Nothing
        Me.MyLabel46.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel46.Location = New System.Drawing.Point(5, 45)
        Me.MyLabel46.Name = "MyLabel46"
        Me.MyLabel46.Size = New System.Drawing.Size(85, 16)
        Me.MyLabel46.TabIndex = 70
        Me.MyLabel46.Text = "Work Order No."
        '
        'MyLabel47
        '
        Me.MyLabel47.FieldName = Nothing
        Me.MyLabel47.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel47.Location = New System.Drawing.Point(5, 69)
        Me.MyLabel47.Name = "MyLabel47"
        Me.MyLabel47.Size = New System.Drawing.Size(42, 16)
        Me.MyLabel47.TabIndex = 63
        Me.MyLabel47.Text = "Bill No."
        '
        'txtBillNo1
        '
        Me.txtBillNo1.CalculationExpression = Nothing
        Me.txtBillNo1.FieldCode = Nothing
        Me.txtBillNo1.FieldDesc = Nothing
        Me.txtBillNo1.FieldMaxLength = 0
        Me.txtBillNo1.FieldName = Nothing
        Me.txtBillNo1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillNo1.isCalculatedField = False
        Me.txtBillNo1.IsSourceFromTable = False
        Me.txtBillNo1.IsSourceFromValueList = False
        Me.txtBillNo1.IsUnique = False
        Me.txtBillNo1.Location = New System.Drawing.Point(110, 67)
        Me.txtBillNo1.MaxLength = 50
        Me.txtBillNo1.MendatroryField = False
        Me.txtBillNo1.MyLinkLable1 = Me.MyLabel47
        Me.txtBillNo1.MyLinkLable2 = Nothing
        Me.txtBillNo1.Name = "txtBillNo1"
        Me.txtBillNo1.ReferenceFieldDesc = Nothing
        Me.txtBillNo1.ReferenceFieldName = Nothing
        Me.txtBillNo1.ReferenceTableName = Nothing
        Me.txtBillNo1.Size = New System.Drawing.Size(132, 18)
        Me.txtBillNo1.TabIndex = 1
        '
        'txtAdditionalCharge
        '
        Me.txtAdditionalCharge.CalculationExpression = Nothing
        Me.txtAdditionalCharge.FieldCode = Nothing
        Me.txtAdditionalCharge.FieldDesc = Nothing
        Me.txtAdditionalCharge.FieldMaxLength = 0
        Me.txtAdditionalCharge.FieldName = Nothing
        Me.txtAdditionalCharge.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdditionalCharge.isCalculatedField = False
        Me.txtAdditionalCharge.IsSourceFromTable = False
        Me.txtAdditionalCharge.IsSourceFromValueList = False
        Me.txtAdditionalCharge.IsUnique = False
        Me.txtAdditionalCharge.Location = New System.Drawing.Point(110, 110)
        Me.txtAdditionalCharge.MaxLength = 50
        Me.txtAdditionalCharge.MendatroryField = False
        Me.txtAdditionalCharge.MyLinkLable1 = Me.MyLabel48
        Me.txtAdditionalCharge.MyLinkLable2 = Nothing
        Me.txtAdditionalCharge.Name = "txtAdditionalCharge"
        Me.txtAdditionalCharge.ReferenceFieldDesc = Nothing
        Me.txtAdditionalCharge.ReferenceFieldName = Nothing
        Me.txtAdditionalCharge.ReferenceTableName = Nothing
        Me.txtAdditionalCharge.Size = New System.Drawing.Size(132, 18)
        Me.txtAdditionalCharge.TabIndex = 3
        '
        'MyLabel48
        '
        Me.MyLabel48.FieldName = Nothing
        Me.MyLabel48.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel48.Location = New System.Drawing.Point(5, 110)
        Me.MyLabel48.Name = "MyLabel48"
        Me.MyLabel48.Size = New System.Drawing.Size(102, 16)
        Me.MyLabel48.TabIndex = 65
        Me.MyLabel48.Text = "Additional Charges"
        '
        'MyLabel49
        '
        Me.MyLabel49.FieldName = Nothing
        Me.MyLabel49.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel49.Location = New System.Drawing.Point(5, 91)
        Me.MyLabel49.Name = "MyLabel49"
        Me.MyLabel49.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel49.TabIndex = 64
        Me.MyLabel49.Text = "Bill Amount"
        '
        'txtBillAmt1
        '
        Me.txtBillAmt1.CalculationExpression = Nothing
        Me.txtBillAmt1.FieldCode = Nothing
        Me.txtBillAmt1.FieldDesc = Nothing
        Me.txtBillAmt1.FieldMaxLength = 0
        Me.txtBillAmt1.FieldName = Nothing
        Me.txtBillAmt1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillAmt1.isCalculatedField = False
        Me.txtBillAmt1.IsSourceFromTable = False
        Me.txtBillAmt1.IsSourceFromValueList = False
        Me.txtBillAmt1.IsUnique = False
        Me.txtBillAmt1.Location = New System.Drawing.Point(110, 89)
        Me.txtBillAmt1.MaxLength = 50
        Me.txtBillAmt1.MendatroryField = False
        Me.txtBillAmt1.MyLinkLable1 = Me.MyLabel49
        Me.txtBillAmt1.MyLinkLable2 = Nothing
        Me.txtBillAmt1.Name = "txtBillAmt1"
        Me.txtBillAmt1.ReferenceFieldDesc = Nothing
        Me.txtBillAmt1.ReferenceFieldName = Nothing
        Me.txtBillAmt1.ReferenceTableName = Nothing
        Me.txtBillAmt1.Size = New System.Drawing.Size(132, 18)
        Me.txtBillAmt1.TabIndex = 2
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.gv2)
        Me.RadGroupBox4.HeaderText = "Spare Part(s)"
        Me.RadGroupBox4.Location = New System.Drawing.Point(8, 342)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(9)
        Me.RadGroupBox4.Size = New System.Drawing.Size(480, 136)
        Me.RadGroupBox4.TabIndex = 131
        Me.RadGroupBox4.Text = "Spare Part(s)"
        '
        'gv2
        '
        Me.gv2.Location = New System.Drawing.Point(6, 22)
        '
        '
        '
        Me.gv2.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv2.MasterTemplate.ShowFilteringRow = False
        Me.gv2.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv2.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gv2.MyStopExport = False
        Me.gv2.Name = "gv2"
        Me.gv2.ShowHeaderCellButtons = True
        Me.gv2.Size = New System.Drawing.Size(469, 109)
        Me.gv2.TabIndex = 0
        '
        'txtPending1
        '
        Me.txtPending1.CalculationExpression = Nothing
        Me.txtPending1.FieldCode = Nothing
        Me.txtPending1.FieldDesc = Nothing
        Me.txtPending1.FieldMaxLength = 0
        Me.txtPending1.FieldName = Nothing
        Me.txtPending1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPending1.isCalculatedField = False
        Me.txtPending1.IsSourceFromTable = False
        Me.txtPending1.IsSourceFromValueList = False
        Me.txtPending1.IsUnique = False
        Me.txtPending1.Location = New System.Drawing.Point(113, 294)
        Me.txtPending1.MaxLength = 50
        Me.txtPending1.MendatroryField = False
        Me.txtPending1.MyLinkLable1 = Me.MyLabel6
        Me.txtPending1.MyLinkLable2 = Nothing
        Me.txtPending1.Name = "txtPending1"
        Me.txtPending1.ReferenceFieldDesc = Nothing
        Me.txtPending1.ReferenceFieldName = Nothing
        Me.txtPending1.ReferenceTableName = Nothing
        Me.txtPending1.Size = New System.Drawing.Size(330, 18)
        Me.txtPending1.TabIndex = 25
        '
        'MyLabel45
        '
        Me.MyLabel45.FieldName = Nothing
        Me.MyLabel45.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel45.Location = New System.Drawing.Point(4, 298)
        Me.MyLabel45.Name = "MyLabel45"
        Me.MyLabel45.Size = New System.Drawing.Size(90, 16)
        Me.MyLabel45.TabIndex = 129
        Me.MyLabel45.Text = "Pending Reason"
        '
        'txtResponseTime
        '
        Me.txtResponseTime.CalculationExpression = Nothing
        Me.txtResponseTime.FieldCode = Nothing
        Me.txtResponseTime.FieldDesc = Nothing
        Me.txtResponseTime.FieldMaxLength = 0
        Me.txtResponseTime.FieldName = Nothing
        Me.txtResponseTime.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtResponseTime.isCalculatedField = False
        Me.txtResponseTime.IsSourceFromTable = False
        Me.txtResponseTime.IsSourceFromValueList = False
        Me.txtResponseTime.IsUnique = False
        Me.txtResponseTime.Location = New System.Drawing.Point(586, 294)
        Me.txtResponseTime.MaxLength = 50
        Me.txtResponseTime.MendatroryField = False
        Me.txtResponseTime.MyLinkLable1 = Me.MyLabel42
        Me.txtResponseTime.MyLinkLable2 = Nothing
        Me.txtResponseTime.Name = "txtResponseTime"
        Me.txtResponseTime.ReferenceFieldDesc = Nothing
        Me.txtResponseTime.ReferenceFieldName = Nothing
        Me.txtResponseTime.ReferenceTableName = Nothing
        Me.txtResponseTime.Size = New System.Drawing.Size(155, 18)
        Me.txtResponseTime.TabIndex = 26
        Me.txtResponseTime.TabStop = False
        '
        'MyLabel42
        '
        Me.MyLabel42.FieldName = Nothing
        Me.MyLabel42.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel42.Location = New System.Drawing.Point(494, 294)
        Me.MyLabel42.Name = "MyLabel42"
        Me.MyLabel42.Size = New System.Drawing.Size(86, 16)
        Me.MyLabel42.TabIndex = 128
        Me.MyLabel42.Text = "Response Time"
        '
        'MyLabel43
        '
        Me.MyLabel43.FieldName = Nothing
        Me.MyLabel43.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel43.Location = New System.Drawing.Point(494, 272)
        Me.MyLabel43.Name = "MyLabel43"
        Me.MyLabel43.Size = New System.Drawing.Size(88, 16)
        Me.MyLabel43.TabIndex = 127
        Me.MyLabel43.Text = "Completed Date"
        '
        'txtCompleteDate
        '
        Me.txtCompleteDate.CalculationExpression = Nothing
        Me.txtCompleteDate.CustomFormat = "dd/MM/yyyy h:mm:ss tt"
        Me.txtCompleteDate.FieldCode = Nothing
        Me.txtCompleteDate.FieldDesc = Nothing
        Me.txtCompleteDate.FieldMaxLength = 0
        Me.txtCompleteDate.FieldName = Nothing
        Me.txtCompleteDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompleteDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtCompleteDate.isCalculatedField = False
        Me.txtCompleteDate.IsSourceFromTable = False
        Me.txtCompleteDate.IsSourceFromValueList = False
        Me.txtCompleteDate.IsUnique = False
        Me.txtCompleteDate.Location = New System.Drawing.Point(588, 272)
        Me.txtCompleteDate.MendatroryField = False
        Me.txtCompleteDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtCompleteDate.MyLinkLable1 = Me.MyLabel43
        Me.txtCompleteDate.MyLinkLable2 = Nothing
        Me.txtCompleteDate.Name = "txtCompleteDate"
        Me.txtCompleteDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtCompleteDate.ReferenceFieldDesc = Nothing
        Me.txtCompleteDate.ReferenceFieldName = Nothing
        Me.txtCompleteDate.ReferenceTableName = Nothing
        Me.txtCompleteDate.Size = New System.Drawing.Size(153, 18)
        Me.txtCompleteDate.TabIndex = 24
        Me.txtCompleteDate.TabStop = False
        Me.txtCompleteDate.Text = "13/06/2011 11:29:49 AM"
        Me.txtCompleteDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'txtRemarks1
        '
        Me.txtRemarks1.AutoSize = False
        Me.txtRemarks1.CalculationExpression = Nothing
        Me.txtRemarks1.FieldCode = Nothing
        Me.txtRemarks1.FieldDesc = Nothing
        Me.txtRemarks1.FieldMaxLength = 0
        Me.txtRemarks1.FieldName = Nothing
        Me.txtRemarks1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks1.isCalculatedField = False
        Me.txtRemarks1.IsSourceFromTable = False
        Me.txtRemarks1.IsSourceFromValueList = False
        Me.txtRemarks1.IsUnique = False
        Me.txtRemarks1.Location = New System.Drawing.Point(113, 316)
        Me.txtRemarks1.MaxLength = 50
        Me.txtRemarks1.MendatroryField = False
        Me.txtRemarks1.Multiline = True
        Me.txtRemarks1.MyLinkLable1 = Me.MyLabel44
        Me.txtRemarks1.MyLinkLable2 = Nothing
        Me.txtRemarks1.Name = "txtRemarks1"
        Me.txtRemarks1.ReferenceFieldDesc = Nothing
        Me.txtRemarks1.ReferenceFieldName = Nothing
        Me.txtRemarks1.ReferenceTableName = Nothing
        Me.txtRemarks1.Size = New System.Drawing.Size(629, 19)
        Me.txtRemarks1.TabIndex = 27
        '
        'MyLabel44
        '
        Me.MyLabel44.FieldName = Nothing
        Me.MyLabel44.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel44.Location = New System.Drawing.Point(8, 320)
        Me.MyLabel44.Name = "MyLabel44"
        Me.MyLabel44.Size = New System.Drawing.Size(51, 16)
        Me.MyLabel44.TabIndex = 126
        Me.MyLabel44.Text = "Remarks"
        '
        'ChkUserSparePart
        '
        Me.ChkUserSparePart.Location = New System.Drawing.Point(286, 272)
        Me.ChkUserSparePart.MyLinkLable1 = Nothing
        Me.ChkUserSparePart.MyLinkLable2 = Nothing
        Me.ChkUserSparePart.Name = "ChkUserSparePart"
        Me.ChkUserSparePart.Size = New System.Drawing.Size(100, 18)
        Me.ChkUserSparePart.TabIndex = 23
        Me.ChkUserSparePart.Tag1 = Nothing
        Me.ChkUserSparePart.Text = "Used Spare Part"
        '
        'rbtnPending
        '
        Me.rbtnPending.Location = New System.Drawing.Point(120, 272)
        Me.rbtnPending.MyLinkLable1 = Nothing
        Me.rbtnPending.MyLinkLable2 = Nothing
        Me.rbtnPending.Name = "rbtnPending"
        Me.rbtnPending.Size = New System.Drawing.Size(61, 18)
        Me.rbtnPending.TabIndex = 21
        Me.rbtnPending.Text = "Pending"
        '
        'rbtnComleted
        '
        Me.rbtnComleted.Location = New System.Drawing.Point(195, 272)
        Me.rbtnComleted.MyLinkLable1 = Nothing
        Me.rbtnComleted.MyLinkLable2 = Nothing
        Me.rbtnComleted.Name = "rbtnComleted"
        Me.rbtnComleted.Size = New System.Drawing.Size(75, 18)
        Me.rbtnComleted.TabIndex = 22
        Me.rbtnComleted.Text = "Completed"
        '
        'rbtnNotCompleted
        '
        Me.rbtnNotCompleted.Location = New System.Drawing.Point(4, 272)
        Me.rbtnNotCompleted.MyLinkLable1 = Nothing
        Me.rbtnNotCompleted.MyLinkLable2 = Nothing
        Me.rbtnNotCompleted.Name = "rbtnNotCompleted"
        Me.rbtnNotCompleted.Size = New System.Drawing.Size(97, 18)
        Me.rbtnNotCompleted.TabIndex = 122
        Me.rbtnNotCompleted.Text = "Not Completed"
        Me.rbtnNotCompleted.Visible = False
        '
        'txtService1
        '
        Me.txtService1.CalculationExpression = Nothing
        Me.txtService1.FieldCode = Nothing
        Me.txtService1.FieldDesc = Nothing
        Me.txtService1.FieldMaxLength = 0
        Me.txtService1.FieldName = Nothing
        Me.txtService1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtService1.isCalculatedField = False
        Me.txtService1.IsSourceFromTable = False
        Me.txtService1.IsSourceFromValueList = False
        Me.txtService1.IsUnique = False
        Me.txtService1.Location = New System.Drawing.Point(113, 249)
        Me.txtService1.MaxLength = 50
        Me.txtService1.MendatroryField = False
        Me.txtService1.MyLinkLable1 = Me.MyLabel6
        Me.txtService1.MyLinkLable2 = Nothing
        Me.txtService1.Name = "txtService1"
        Me.txtService1.ReferenceFieldDesc = Nothing
        Me.txtService1.ReferenceFieldName = Nothing
        Me.txtService1.ReferenceTableName = Nothing
        Me.txtService1.Size = New System.Drawing.Size(330, 18)
        Me.txtService1.TabIndex = 20
        '
        'txtfranchise1
        '
        Me.txtfranchise1.CalculationExpression = Nothing
        Me.txtfranchise1.FieldCode = Nothing
        Me.txtfranchise1.FieldDesc = Nothing
        Me.txtfranchise1.FieldMaxLength = 0
        Me.txtfranchise1.FieldName = Nothing
        Me.txtfranchise1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtfranchise1.isCalculatedField = False
        Me.txtfranchise1.IsSourceFromTable = False
        Me.txtfranchise1.IsSourceFromValueList = False
        Me.txtfranchise1.IsUnique = False
        Me.txtfranchise1.Location = New System.Drawing.Point(113, 225)
        Me.txtfranchise1.MaxLength = 50
        Me.txtfranchise1.MendatroryField = False
        Me.txtfranchise1.MyLinkLable1 = Me.MyLabel6
        Me.txtfranchise1.MyLinkLable2 = Nothing
        Me.txtfranchise1.Name = "txtfranchise1"
        Me.txtfranchise1.ReferenceFieldDesc = Nothing
        Me.txtfranchise1.ReferenceFieldName = Nothing
        Me.txtfranchise1.ReferenceTableName = Nothing
        Me.txtfranchise1.Size = New System.Drawing.Size(330, 18)
        Me.txtfranchise1.TabIndex = 19
        '
        'txtSecondary1
        '
        Me.txtSecondary1.CalculationExpression = Nothing
        Me.txtSecondary1.FieldCode = Nothing
        Me.txtSecondary1.FieldDesc = Nothing
        Me.txtSecondary1.FieldMaxLength = 0
        Me.txtSecondary1.FieldName = Nothing
        Me.txtSecondary1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSecondary1.isCalculatedField = False
        Me.txtSecondary1.IsSourceFromTable = False
        Me.txtSecondary1.IsSourceFromValueList = False
        Me.txtSecondary1.IsUnique = False
        Me.txtSecondary1.Location = New System.Drawing.Point(113, 178)
        Me.txtSecondary1.MaxLength = 50
        Me.txtSecondary1.MendatroryField = False
        Me.txtSecondary1.MyLinkLable1 = Me.MyLabel6
        Me.txtSecondary1.MyLinkLable2 = Nothing
        Me.txtSecondary1.Name = "txtSecondary1"
        Me.txtSecondary1.ReferenceFieldDesc = Nothing
        Me.txtSecondary1.ReferenceFieldName = Nothing
        Me.txtSecondary1.ReferenceTableName = Nothing
        Me.txtSecondary1.Size = New System.Drawing.Size(330, 18)
        Me.txtSecondary1.TabIndex = 16
        '
        'txtComplaintType
        '
        Me.txtComplaintType.CalculationExpression = Nothing
        Me.txtComplaintType.FieldCode = Nothing
        Me.txtComplaintType.FieldDesc = Nothing
        Me.txtComplaintType.FieldMaxLength = 0
        Me.txtComplaintType.FieldName = Nothing
        Me.txtComplaintType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtComplaintType.isCalculatedField = False
        Me.txtComplaintType.IsSourceFromTable = False
        Me.txtComplaintType.IsSourceFromValueList = False
        Me.txtComplaintType.IsUnique = False
        Me.txtComplaintType.Location = New System.Drawing.Point(113, 158)
        Me.txtComplaintType.MaxLength = 50
        Me.txtComplaintType.MendatroryField = False
        Me.txtComplaintType.MyLinkLable1 = Me.MyLabel6
        Me.txtComplaintType.MyLinkLable2 = Nothing
        Me.txtComplaintType.Name = "txtComplaintType"
        Me.txtComplaintType.ReferenceFieldDesc = Nothing
        Me.txtComplaintType.ReferenceFieldName = Nothing
        Me.txtComplaintType.ReferenceTableName = Nothing
        Me.txtComplaintType.Size = New System.Drawing.Size(330, 18)
        Me.txtComplaintType.TabIndex = 15
        '
        'txtprimary1
        '
        Me.txtprimary1.CalculationExpression = Nothing
        Me.txtprimary1.FieldCode = Nothing
        Me.txtprimary1.FieldDesc = Nothing
        Me.txtprimary1.FieldMaxLength = 0
        Me.txtprimary1.FieldName = Nothing
        Me.txtprimary1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtprimary1.isCalculatedField = False
        Me.txtprimary1.IsSourceFromTable = False
        Me.txtprimary1.IsSourceFromValueList = False
        Me.txtprimary1.IsUnique = False
        Me.txtprimary1.Location = New System.Drawing.Point(113, 138)
        Me.txtprimary1.MaxLength = 50
        Me.txtprimary1.MendatroryField = False
        Me.txtprimary1.MyLinkLable1 = Me.MyLabel6
        Me.txtprimary1.MyLinkLable2 = Nothing
        Me.txtprimary1.Name = "txtprimary1"
        Me.txtprimary1.ReferenceFieldDesc = Nothing
        Me.txtprimary1.ReferenceFieldName = Nothing
        Me.txtprimary1.ReferenceTableName = Nothing
        Me.txtprimary1.Size = New System.Drawing.Size(330, 18)
        Me.txtprimary1.TabIndex = 14
        '
        'txtComplaintGivento
        '
        Me.txtComplaintGivento.CalculationExpression = Nothing
        Me.txtComplaintGivento.FieldCode = Nothing
        Me.txtComplaintGivento.FieldDesc = Nothing
        Me.txtComplaintGivento.FieldMaxLength = 0
        Me.txtComplaintGivento.FieldName = Nothing
        Me.txtComplaintGivento.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtComplaintGivento.isCalculatedField = False
        Me.txtComplaintGivento.IsSourceFromTable = False
        Me.txtComplaintGivento.IsSourceFromValueList = False
        Me.txtComplaintGivento.IsUnique = False
        Me.txtComplaintGivento.Location = New System.Drawing.Point(494, 202)
        Me.txtComplaintGivento.MaxLength = 50
        Me.txtComplaintGivento.MendatroryField = False
        Me.txtComplaintGivento.MyLinkLable1 = Me.MyLabel41
        Me.txtComplaintGivento.MyLinkLable2 = Nothing
        Me.txtComplaintGivento.Name = "txtComplaintGivento"
        Me.txtComplaintGivento.ReferenceFieldDesc = Nothing
        Me.txtComplaintGivento.ReferenceFieldName = Nothing
        Me.txtComplaintGivento.ReferenceTableName = Nothing
        Me.txtComplaintGivento.Size = New System.Drawing.Size(247, 18)
        Me.txtComplaintGivento.TabIndex = 18
        '
        'MyLabel41
        '
        Me.MyLabel41.FieldName = Nothing
        Me.MyLabel41.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel41.Location = New System.Drawing.Point(380, 203)
        Me.MyLabel41.Name = "MyLabel41"
        Me.MyLabel41.Size = New System.Drawing.Size(106, 16)
        Me.MyLabel41.TabIndex = 78
        Me.MyLabel41.Text = "Complaint Given To"
        '
        'txtComplaintGivenBy
        '
        Me.txtComplaintGivenBy.CalculationExpression = Nothing
        Me.txtComplaintGivenBy.FieldCode = Nothing
        Me.txtComplaintGivenBy.FieldDesc = Nothing
        Me.txtComplaintGivenBy.FieldMaxLength = 0
        Me.txtComplaintGivenBy.FieldName = Nothing
        Me.txtComplaintGivenBy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtComplaintGivenBy.isCalculatedField = False
        Me.txtComplaintGivenBy.IsSourceFromTable = False
        Me.txtComplaintGivenBy.IsSourceFromValueList = False
        Me.txtComplaintGivenBy.IsUnique = False
        Me.txtComplaintGivenBy.Location = New System.Drawing.Point(113, 202)
        Me.txtComplaintGivenBy.MaxLength = 50
        Me.txtComplaintGivenBy.MendatroryField = False
        Me.txtComplaintGivenBy.MyLinkLable1 = Me.MyLabel15
        Me.txtComplaintGivenBy.MyLinkLable2 = Nothing
        Me.txtComplaintGivenBy.Name = "txtComplaintGivenBy"
        Me.txtComplaintGivenBy.ReferenceFieldDesc = Nothing
        Me.txtComplaintGivenBy.ReferenceFieldName = Nothing
        Me.txtComplaintGivenBy.ReferenceTableName = Nothing
        Me.txtComplaintGivenBy.Size = New System.Drawing.Size(257, 18)
        Me.txtComplaintGivenBy.TabIndex = 17
        '
        'rptdSerialNo
        '
        Me.rptdSerialNo.AutoSize = False
        Me.rptdSerialNo.FieldName = Nothing
        Me.rptdSerialNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rptdSerialNo.Location = New System.Drawing.Point(465, 142)
        Me.rptdSerialNo.Name = "rptdSerialNo"
        Me.rptdSerialNo.Size = New System.Drawing.Size(242, 18)
        Me.rptdSerialNo.TabIndex = 75
        Me.rptdSerialNo.Text = "Serial No. Repeated 0 Times"
        Me.rptdSerialNo.TextWrap = False
        '
        'MyLabel39
        '
        Me.MyLabel39.FieldName = Nothing
        Me.MyLabel39.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel39.Location = New System.Drawing.Point(265, 120)
        Me.MyLabel39.Name = "MyLabel39"
        Me.MyLabel39.Size = New System.Drawing.Size(46, 16)
        Me.MyLabel39.TabIndex = 74
        Me.MyLabel39.Text = "Tag No."
        '
        'cboApex1
        '
        Me.cboApex1.CalculationExpression = Nothing
        Me.cboApex1.DropDownAnimationEnabled = True
        Me.cboApex1.FieldCode = Nothing
        Me.cboApex1.FieldDesc = Nothing
        Me.cboApex1.FieldMaxLength = 0
        Me.cboApex1.FieldName = Nothing
        Me.cboApex1.isCalculatedField = False
        Me.cboApex1.IsSourceFromTable = False
        Me.cboApex1.IsSourceFromValueList = False
        Me.cboApex1.IsUnique = False
        Me.cboApex1.Location = New System.Drawing.Point(596, 118)
        Me.cboApex1.MendatroryField = True
        Me.cboApex1.MyLinkLable1 = Nothing
        Me.cboApex1.MyLinkLable2 = Nothing
        Me.cboApex1.Name = "cboApex1"
        Me.cboApex1.ReferenceFieldDesc = Nothing
        Me.cboApex1.ReferenceFieldName = Nothing
        Me.cboApex1.ReferenceTableName = Nothing
        Me.cboApex1.Size = New System.Drawing.Size(145, 20)
        Me.cboApex1.TabIndex = 13
        '
        'MyLabel30
        '
        Me.MyLabel30.FieldName = Nothing
        Me.MyLabel30.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel30.Location = New System.Drawing.Point(465, 118)
        Me.MyLabel30.Name = "MyLabel30"
        Me.MyLabel30.Size = New System.Drawing.Size(123, 16)
        Me.MyLabel30.TabIndex = 73
        Me.MyLabel30.Text = "Apex Pending W/O No."
        '
        'txtTag1
        '
        Me.txtTag1.CalculationExpression = Nothing
        Me.txtTag1.FieldCode = Nothing
        Me.txtTag1.FieldDesc = Nothing
        Me.txtTag1.FieldMaxLength = 0
        Me.txtTag1.FieldName = Nothing
        Me.txtTag1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTag1.isCalculatedField = False
        Me.txtTag1.IsSourceFromTable = False
        Me.txtTag1.IsSourceFromValueList = False
        Me.txtTag1.IsUnique = False
        Me.txtTag1.Location = New System.Drawing.Point(315, 118)
        Me.txtTag1.MaxLength = 50
        Me.txtTag1.MendatroryField = False
        Me.txtTag1.MyLinkLable1 = Me.MyLabel10
        Me.txtTag1.MyLinkLable2 = Nothing
        Me.txtTag1.Name = "txtTag1"
        Me.txtTag1.ReferenceFieldDesc = Nothing
        Me.txtTag1.ReferenceFieldName = Nothing
        Me.txtTag1.ReferenceTableName = Nothing
        Me.txtTag1.Size = New System.Drawing.Size(128, 18)
        Me.txtTag1.TabIndex = 12
        '
        'txtSerialNo1
        '
        Me.txtSerialNo1.CalculationExpression = Nothing
        Me.txtSerialNo1.FieldCode = Nothing
        Me.txtSerialNo1.FieldDesc = Nothing
        Me.txtSerialNo1.FieldMaxLength = 0
        Me.txtSerialNo1.FieldName = Nothing
        Me.txtSerialNo1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSerialNo1.isCalculatedField = False
        Me.txtSerialNo1.IsSourceFromTable = False
        Me.txtSerialNo1.IsSourceFromValueList = False
        Me.txtSerialNo1.IsUnique = False
        Me.txtSerialNo1.Location = New System.Drawing.Point(113, 118)
        Me.txtSerialNo1.MaxLength = 50
        Me.txtSerialNo1.MendatroryField = False
        Me.txtSerialNo1.MyLinkLable1 = Me.MyLabel6
        Me.txtSerialNo1.MyLinkLable2 = Nothing
        Me.txtSerialNo1.Name = "txtSerialNo1"
        Me.txtSerialNo1.ReferenceFieldDesc = Nothing
        Me.txtSerialNo1.ReferenceFieldName = Nothing
        Me.txtSerialNo1.ReferenceTableName = Nothing
        Me.txtSerialNo1.Size = New System.Drawing.Size(146, 18)
        Me.txtSerialNo1.TabIndex = 11
        '
        'txtSize1
        '
        Me.txtSize1.CalculationExpression = Nothing
        Me.txtSize1.FieldCode = Nothing
        Me.txtSize1.FieldDesc = Nothing
        Me.txtSize1.FieldMaxLength = 0
        Me.txtSize1.FieldName = Nothing
        Me.txtSize1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSize1.isCalculatedField = False
        Me.txtSize1.IsSourceFromTable = False
        Me.txtSize1.IsSourceFromValueList = False
        Me.txtSize1.IsUnique = False
        Me.txtSize1.Location = New System.Drawing.Point(512, 93)
        Me.txtSize1.MaxLength = 50
        Me.txtSize1.MendatroryField = False
        Me.txtSize1.MyLinkLable1 = Me.MyLabel6
        Me.txtSize1.MyLinkLable2 = Nothing
        Me.txtSize1.Name = "txtSize1"
        Me.txtSize1.ReferenceFieldDesc = Nothing
        Me.txtSize1.ReferenceFieldName = Nothing
        Me.txtSize1.ReferenceTableName = Nothing
        Me.txtSize1.Size = New System.Drawing.Size(229, 18)
        Me.txtSize1.TabIndex = 10
        '
        'txtModel1
        '
        Me.txtModel1.CalculationExpression = Nothing
        Me.txtModel1.FieldCode = Nothing
        Me.txtModel1.FieldDesc = Nothing
        Me.txtModel1.FieldMaxLength = 0
        Me.txtModel1.FieldName = Nothing
        Me.txtModel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtModel1.isCalculatedField = False
        Me.txtModel1.IsSourceFromTable = False
        Me.txtModel1.IsSourceFromValueList = False
        Me.txtModel1.IsUnique = False
        Me.txtModel1.Location = New System.Drawing.Point(313, 93)
        Me.txtModel1.MaxLength = 50
        Me.txtModel1.MendatroryField = False
        Me.txtModel1.MyLinkLable1 = Me.MyLabel6
        Me.txtModel1.MyLinkLable2 = Nothing
        Me.txtModel1.Name = "txtModel1"
        Me.txtModel1.ReferenceFieldDesc = Nothing
        Me.txtModel1.ReferenceFieldName = Nothing
        Me.txtModel1.ReferenceTableName = Nothing
        Me.txtModel1.Size = New System.Drawing.Size(193, 18)
        Me.txtModel1.TabIndex = 9
        '
        'txtMake1
        '
        Me.txtMake1.CalculationExpression = Nothing
        Me.txtMake1.FieldCode = Nothing
        Me.txtMake1.FieldDesc = Nothing
        Me.txtMake1.FieldMaxLength = 0
        Me.txtMake1.FieldName = Nothing
        Me.txtMake1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMake1.isCalculatedField = False
        Me.txtMake1.IsSourceFromTable = False
        Me.txtMake1.IsSourceFromValueList = False
        Me.txtMake1.IsUnique = False
        Me.txtMake1.Location = New System.Drawing.Point(113, 93)
        Me.txtMake1.MaxLength = 50
        Me.txtMake1.MendatroryField = False
        Me.txtMake1.MyLinkLable1 = Me.MyLabel6
        Me.txtMake1.MyLinkLable2 = Nothing
        Me.txtMake1.Name = "txtMake1"
        Me.txtMake1.ReferenceFieldDesc = Nothing
        Me.txtMake1.ReferenceFieldName = Nothing
        Me.txtMake1.ReferenceTableName = Nothing
        Me.txtMake1.Size = New System.Drawing.Size(194, 18)
        Me.txtMake1.TabIndex = 8
        '
        'txtAssetType1
        '
        Me.txtAssetType1.CalculationExpression = Nothing
        Me.txtAssetType1.FieldCode = Nothing
        Me.txtAssetType1.FieldDesc = Nothing
        Me.txtAssetType1.FieldMaxLength = 0
        Me.txtAssetType1.FieldName = Nothing
        Me.txtAssetType1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAssetType1.isCalculatedField = False
        Me.txtAssetType1.IsSourceFromTable = False
        Me.txtAssetType1.IsSourceFromValueList = False
        Me.txtAssetType1.IsUnique = False
        Me.txtAssetType1.Location = New System.Drawing.Point(113, 72)
        Me.txtAssetType1.MaxLength = 50
        Me.txtAssetType1.MendatroryField = False
        Me.txtAssetType1.MyLinkLable1 = Me.MyLabel38
        Me.txtAssetType1.MyLinkLable2 = Nothing
        Me.txtAssetType1.Name = "txtAssetType1"
        Me.txtAssetType1.ReferenceFieldDesc = Nothing
        Me.txtAssetType1.ReferenceFieldName = Nothing
        Me.txtAssetType1.ReferenceTableName = Nothing
        Me.txtAssetType1.Size = New System.Drawing.Size(330, 18)
        Me.txtAssetType1.TabIndex = 7
        '
        'MyLabel38
        '
        Me.MyLabel38.FieldName = Nothing
        Me.MyLabel38.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel38.Location = New System.Drawing.Point(449, 50)
        Me.MyLabel38.Name = "MyLabel38"
        Me.MyLabel38.Size = New System.Drawing.Size(60, 16)
        Me.MyLabel38.TabIndex = 61
        Me.MyLabel38.Text = "Phone No."
        '
        'txtLocation1
        '
        Me.txtLocation1.CalculationExpression = Nothing
        Me.txtLocation1.FieldCode = Nothing
        Me.txtLocation1.FieldDesc = Nothing
        Me.txtLocation1.FieldMaxLength = 0
        Me.txtLocation1.FieldName = Nothing
        Me.txtLocation1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation1.isCalculatedField = False
        Me.txtLocation1.IsSourceFromTable = False
        Me.txtLocation1.IsSourceFromValueList = False
        Me.txtLocation1.IsUnique = False
        Me.txtLocation1.Location = New System.Drawing.Point(113, 49)
        Me.txtLocation1.MaxLength = 50
        Me.txtLocation1.MendatroryField = False
        Me.txtLocation1.MyLinkLable1 = Me.MyLabel38
        Me.txtLocation1.MyLinkLable2 = Nothing
        Me.txtLocation1.Name = "txtLocation1"
        Me.txtLocation1.ReferenceFieldDesc = Nothing
        Me.txtLocation1.ReferenceFieldName = Nothing
        Me.txtLocation1.ReferenceTableName = Nothing
        Me.txtLocation1.Size = New System.Drawing.Size(330, 18)
        Me.txtLocation1.TabIndex = 5
        '
        'txtCountry1
        '
        Me.txtCountry1.CalculationExpression = Nothing
        Me.txtCountry1.FieldCode = Nothing
        Me.txtCountry1.FieldDesc = Nothing
        Me.txtCountry1.FieldMaxLength = 0
        Me.txtCountry1.FieldName = Nothing
        Me.txtCountry1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCountry1.isCalculatedField = False
        Me.txtCountry1.IsSourceFromTable = False
        Me.txtCountry1.IsSourceFromValueList = False
        Me.txtCountry1.IsUnique = False
        Me.txtCountry1.Location = New System.Drawing.Point(512, 27)
        Me.txtCountry1.MaxLength = 50
        Me.txtCountry1.MendatroryField = False
        Me.txtCountry1.MyLinkLable1 = Me.MyLabel38
        Me.txtCountry1.MyLinkLable2 = Nothing
        Me.txtCountry1.Name = "txtCountry1"
        Me.txtCountry1.ReferenceFieldDesc = Nothing
        Me.txtCountry1.ReferenceFieldName = Nothing
        Me.txtCountry1.ReferenceTableName = Nothing
        Me.txtCountry1.Size = New System.Drawing.Size(229, 18)
        Me.txtCountry1.TabIndex = 4
        '
        'txtState1
        '
        Me.txtState1.CalculationExpression = Nothing
        Me.txtState1.FieldCode = Nothing
        Me.txtState1.FieldDesc = Nothing
        Me.txtState1.FieldMaxLength = 0
        Me.txtState1.FieldName = Nothing
        Me.txtState1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtState1.isCalculatedField = False
        Me.txtState1.IsSourceFromTable = False
        Me.txtState1.IsSourceFromValueList = False
        Me.txtState1.IsUnique = False
        Me.txtState1.Location = New System.Drawing.Point(313, 27)
        Me.txtState1.MaxLength = 50
        Me.txtState1.MendatroryField = False
        Me.txtState1.MyLinkLable1 = Me.MyLabel38
        Me.txtState1.MyLinkLable2 = Nothing
        Me.txtState1.Name = "txtState1"
        Me.txtState1.ReferenceFieldDesc = Nothing
        Me.txtState1.ReferenceFieldName = Nothing
        Me.txtState1.ReferenceTableName = Nothing
        Me.txtState1.Size = New System.Drawing.Size(193, 18)
        Me.txtState1.TabIndex = 3
        '
        'txtCity1
        '
        Me.txtCity1.CalculationExpression = Nothing
        Me.txtCity1.FieldCode = Nothing
        Me.txtCity1.FieldDesc = Nothing
        Me.txtCity1.FieldMaxLength = 0
        Me.txtCity1.FieldName = Nothing
        Me.txtCity1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCity1.isCalculatedField = False
        Me.txtCity1.IsSourceFromTable = False
        Me.txtCity1.IsSourceFromValueList = False
        Me.txtCity1.IsUnique = False
        Me.txtCity1.Location = New System.Drawing.Point(113, 27)
        Me.txtCity1.MaxLength = 50
        Me.txtCity1.MendatroryField = False
        Me.txtCity1.MyLinkLable1 = Me.MyLabel38
        Me.txtCity1.MyLinkLable2 = Nothing
        Me.txtCity1.Name = "txtCity1"
        Me.txtCity1.ReferenceFieldDesc = Nothing
        Me.txtCity1.ReferenceFieldName = Nothing
        Me.txtCity1.ReferenceTableName = Nothing
        Me.txtCity1.Size = New System.Drawing.Size(194, 18)
        Me.txtCity1.TabIndex = 2
        '
        'txtPhn1
        '
        Me.txtPhn1.CalculationExpression = Nothing
        Me.txtPhn1.FieldCode = Nothing
        Me.txtPhn1.FieldDesc = Nothing
        Me.txtPhn1.FieldMaxLength = 0
        Me.txtPhn1.FieldName = Nothing
        Me.txtPhn1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPhn1.isCalculatedField = False
        Me.txtPhn1.IsSourceFromTable = False
        Me.txtPhn1.IsSourceFromValueList = False
        Me.txtPhn1.IsUnique = False
        Me.txtPhn1.Location = New System.Drawing.Point(512, 50)
        Me.txtPhn1.MaxLength = 50
        Me.txtPhn1.MendatroryField = False
        Me.txtPhn1.MyLinkLable1 = Me.MyLabel38
        Me.txtPhn1.MyLinkLable2 = Nothing
        Me.txtPhn1.Name = "txtPhn1"
        Me.txtPhn1.ReferenceFieldDesc = Nothing
        Me.txtPhn1.ReferenceFieldName = Nothing
        Me.txtPhn1.ReferenceTableName = Nothing
        Me.txtPhn1.Size = New System.Drawing.Size(229, 18)
        Me.txtPhn1.TabIndex = 6
        '
        'MyLabel32
        '
        Me.MyLabel32.FieldName = Nothing
        Me.MyLabel32.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel32.Location = New System.Drawing.Point(3, 250)
        Me.MyLabel32.Name = "MyLabel32"
        Me.MyLabel32.Size = New System.Drawing.Size(96, 16)
        Me.MyLabel32.TabIndex = 57
        Me.MyLabel32.Text = "Service Executive"
        '
        'MyLabel33
        '
        Me.MyLabel33.FieldName = Nothing
        Me.MyLabel33.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel33.Location = New System.Drawing.Point(3, 138)
        Me.MyLabel33.Name = "MyLabel33"
        Me.MyLabel33.Size = New System.Drawing.Size(87, 16)
        Me.MyLabel33.TabIndex = 55
        Me.MyLabel33.Text = "Primary Reason"
        '
        'MyLabel34
        '
        Me.MyLabel34.FieldName = Nothing
        Me.MyLabel34.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel34.Location = New System.Drawing.Point(3, 181)
        Me.MyLabel34.Name = "MyLabel34"
        Me.MyLabel34.Size = New System.Drawing.Size(103, 16)
        Me.MyLabel34.TabIndex = 58
        Me.MyLabel34.Text = "Secondary Reason"
        '
        'MyLabel35
        '
        Me.MyLabel35.FieldName = Nothing
        Me.MyLabel35.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel35.Location = New System.Drawing.Point(3, 228)
        Me.MyLabel35.Name = "MyLabel35"
        Me.MyLabel35.Size = New System.Drawing.Size(56, 16)
        Me.MyLabel35.TabIndex = 59
        Me.MyLabel35.Text = "Franchise"
        '
        'MyLabel36
        '
        Me.MyLabel36.FieldName = Nothing
        Me.MyLabel36.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel36.Location = New System.Drawing.Point(3, 204)
        Me.MyLabel36.Name = "MyLabel36"
        Me.MyLabel36.Size = New System.Drawing.Size(106, 16)
        Me.MyLabel36.TabIndex = 56
        Me.MyLabel36.Text = "Complaint Given By"
        '
        'MyLabel37
        '
        Me.MyLabel37.FieldName = Nothing
        Me.MyLabel37.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel37.Location = New System.Drawing.Point(3, 160)
        Me.MyLabel37.Name = "MyLabel37"
        Me.MyLabel37.Size = New System.Drawing.Size(86, 16)
        Me.MyLabel37.TabIndex = 54
        Me.MyLabel37.Text = "Complaint Type"
        '
        'MyLabel26
        '
        Me.MyLabel26.FieldName = Nothing
        Me.MyLabel26.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel26.Location = New System.Drawing.Point(2, 93)
        Me.MyLabel26.Name = "MyLabel26"
        Me.MyLabel26.Size = New System.Drawing.Size(99, 16)
        Me.MyLabel26.TabIndex = 45
        Me.MyLabel26.Text = "Make/ Model/ Size"
        '
        'MyLabel28
        '
        Me.MyLabel28.FieldName = Nothing
        Me.MyLabel28.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel28.Location = New System.Drawing.Point(3, 116)
        Me.MyLabel28.Name = "MyLabel28"
        Me.MyLabel28.Size = New System.Drawing.Size(56, 16)
        Me.MyLabel28.TabIndex = 47
        Me.MyLabel28.Text = "Serial No."
        '
        'MyLabel29
        '
        Me.MyLabel29.FieldName = Nothing
        Me.MyLabel29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel29.Location = New System.Drawing.Point(3, 73)
        Me.MyLabel29.Name = "MyLabel29"
        Me.MyLabel29.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel29.TabIndex = 46
        Me.MyLabel29.Text = "Asset Type"
        '
        'lblCity
        '
        Me.lblCity.FieldName = Nothing
        Me.lblCity.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCity.Location = New System.Drawing.Point(3, 27)
        Me.lblCity.Name = "lblCity"
        Me.lblCity.Size = New System.Drawing.Size(104, 16)
        Me.lblCity.TabIndex = 43
        Me.lblCity.Text = "City /State/ Country"
        '
        'MyLabel31
        '
        Me.MyLabel31.FieldName = Nothing
        Me.MyLabel31.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel31.Location = New System.Drawing.Point(3, 50)
        Me.MyLabel31.Name = "MyLabel31"
        Me.MyLabel31.Size = New System.Drawing.Size(49, 16)
        Me.MyLabel31.TabIndex = 44
        Me.MyLabel31.Text = "Location"
        '
        'txtType
        '
        Me.txtType.CalculationExpression = Nothing
        Me.txtType.FieldCode = Nothing
        Me.txtType.FieldDesc = Nothing
        Me.txtType.FieldMaxLength = 0
        Me.txtType.FieldName = Nothing
        Me.txtType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtType.isCalculatedField = False
        Me.txtType.IsSourceFromTable = False
        Me.txtType.IsSourceFromValueList = False
        Me.txtType.IsUnique = False
        Me.txtType.Location = New System.Drawing.Point(486, 3)
        Me.txtType.MaxLength = 50
        Me.txtType.MendatroryField = False
        Me.txtType.MyLinkLable1 = Me.MyLabel10
        Me.txtType.MyLinkLable2 = Nothing
        Me.txtType.Name = "txtType"
        Me.txtType.ReferenceFieldDesc = Nothing
        Me.txtType.ReferenceFieldName = Nothing
        Me.txtType.ReferenceTableName = Nothing
        Me.txtType.Size = New System.Drawing.Size(255, 18)
        Me.txtType.TabIndex = 1
        '
        'lblType
        '
        Me.lblType.FieldName = Nothing
        Me.lblType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblType.Location = New System.Drawing.Point(449, 3)
        Me.lblType.Name = "lblType"
        Me.lblType.Size = New System.Drawing.Size(31, 16)
        Me.lblType.TabIndex = 31
        Me.lblType.Text = "Type"
        '
        'txtOutletName
        '
        Me.txtOutletName.CalculationExpression = Nothing
        Me.txtOutletName.FieldCode = Nothing
        Me.txtOutletName.FieldDesc = Nothing
        Me.txtOutletName.FieldMaxLength = 0
        Me.txtOutletName.FieldName = Nothing
        Me.txtOutletName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOutletName.isCalculatedField = False
        Me.txtOutletName.IsSourceFromTable = False
        Me.txtOutletName.IsSourceFromValueList = False
        Me.txtOutletName.IsUnique = False
        Me.txtOutletName.Location = New System.Drawing.Point(113, 3)
        Me.txtOutletName.MaxLength = 50
        Me.txtOutletName.MendatroryField = False
        Me.txtOutletName.MyLinkLable1 = Me.MyLabel10
        Me.txtOutletName.MyLinkLable2 = Nothing
        Me.txtOutletName.Name = "txtOutletName"
        Me.txtOutletName.ReferenceFieldDesc = Nothing
        Me.txtOutletName.ReferenceFieldName = Nothing
        Me.txtOutletName.ReferenceTableName = Nothing
        Me.txtOutletName.Size = New System.Drawing.Size(330, 18)
        Me.txtOutletName.TabIndex = 0
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel12.Location = New System.Drawing.Point(3, 3)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(70, 16)
        Me.MyLabel12.TabIndex = 30
        Me.MyLabel12.Text = "Outlet Name"
        '
        'txtdesc
        '
        Me.txtdesc.CalculationExpression = Nothing
        Me.txtdesc.FieldCode = Nothing
        Me.txtdesc.FieldDesc = Nothing
        Me.txtdesc.FieldMaxLength = 0
        Me.txtdesc.FieldName = Nothing
        Me.txtdesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdesc.isCalculatedField = False
        Me.txtdesc.IsSourceFromTable = False
        Me.txtdesc.IsSourceFromValueList = False
        Me.txtdesc.IsUnique = False
        Me.txtdesc.Location = New System.Drawing.Point(81, 28)
        Me.txtdesc.MaxLength = 50
        Me.txtdesc.MendatroryField = False
        Me.txtdesc.MyLinkLable1 = Me.MyLabel24
        Me.txtdesc.MyLinkLable2 = Nothing
        Me.txtdesc.Name = "txtdesc"
        Me.txtdesc.ReferenceFieldDesc = Nothing
        Me.txtdesc.ReferenceFieldName = Nothing
        Me.txtdesc.ReferenceTableName = Nothing
        Me.txtdesc.Size = New System.Drawing.Size(367, 18)
        Me.txtdesc.TabIndex = 2
        '
        'MyLabel24
        '
        Me.MyLabel24.FieldName = Nothing
        Me.MyLabel24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel24.Location = New System.Drawing.Point(5, 29)
        Me.MyLabel24.Name = "MyLabel24"
        Me.MyLabel24.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel24.TabIndex = 29
        Me.MyLabel24.Text = "Description"
        '
        'UsLock1
        '
        Me.UsLock1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(679, 31)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 57
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(357, 8)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(84, 16)
        Me.RadLabel4.TabIndex = 26
        Me.RadLabel4.Text = "Complaint Date"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(5, 6)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(70, 16)
        Me.RadLabel1.TabIndex = 27
        Me.RadLabel1.Text = "Complaint Id"
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.XpertERPService.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(335, 5)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(18, 21)
        Me.btnAddNew.TabIndex = 0
        '
        'txtcomid
        '
        Me.txtcomid.FieldName = Nothing
        Me.txtcomid.Location = New System.Drawing.Point(81, 5)
        Me.txtcomid.MendatroryField = False
        Me.txtcomid.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtcomid.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtcomid.MyLinkLable1 = Me.RadLabel1
        Me.txtcomid.MyLinkLable2 = Nothing
        Me.txtcomid.MyMaxLength = 30
        Me.txtcomid.MyReadOnly = False
        Me.txtcomid.Name = "txtcomid"
        Me.txtcomid.Size = New System.Drawing.Size(252, 20)
        Me.txtcomid.TabIndex = 0
        Me.txtcomid.Value = ""
        '
        'txtDate
        '
        Me.txtDate.CalculationExpression = Nothing
        Me.txtDate.CustomFormat = "dd/MM/yyyy h:mm:ss tt"
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
        Me.txtDate.Location = New System.Drawing.Point(448, 7)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.RadLabel4
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(149, 18)
        Me.txtDate.TabIndex = 1
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "08/04/2014 11:28:14 AM"
        Me.txtDate.Value = New Date(2014, 4, 8, 11, 28, 14, 0)
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnsend)
        Me.Panel1.Controls.Add(Me.btnCopy)
        Me.Panel1.Controls.Add(Me.btnDelete)
        Me.Panel1.Controls.Add(Me.btnPost)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Controls.Add(Me.btnSave)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(780, 26)
        Me.Panel1.TabIndex = 1
        '
        'btnsend
        '
        Me.btnsend.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsend.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsend.Location = New System.Drawing.Point(309, 2)
        Me.btnsend.Name = "btnsend"
        Me.btnsend.Size = New System.Drawing.Size(105, 19)
        Me.btnsend.TabIndex = 4
        Me.btnsend.Text = "Send E-Mail/SMS"
        '
        'btnCopy
        '
        Me.btnCopy.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCopy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCopy.Location = New System.Drawing.Point(234, 2)
        Me.btnCopy.Name = "btnCopy"
        Me.btnCopy.Size = New System.Drawing.Size(69, 19)
        Me.btnCopy.TabIndex = 3
        Me.btnCopy.Text = "Copy"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(159, 2)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 19)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "Delete"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(82, 2)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(69, 19)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(708, 2)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 19)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(7, 2)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 19)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdmenufile)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer1.Panel1.Padding = New System.Windows.Forms.Padding(3)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Panel1)
        Me.SplitContainer1.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.SplitContainer1.Size = New System.Drawing.Size(786, 665)
        Me.SplitContainer1.SplitterDistance = 629
        Me.SplitContainer1.TabIndex = 2
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.chkManual)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel24)
        Me.RadGroupBox3.Controls.Add(Me.txtdesc)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel4)
        Me.RadGroupBox3.Controls.Add(Me.txtcomid)
        Me.RadGroupBox3.Controls.Add(Me.txtDate)
        Me.RadGroupBox3.Controls.Add(Me.btnAddNew)
        Me.RadGroupBox3.HeaderText = ""
        Me.RadGroupBox3.Location = New System.Drawing.Point(8, 28)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Size = New System.Drawing.Size(606, 51)
        Me.RadGroupBox3.TabIndex = 0
        '
        'chkManual
        '
        Me.chkManual.Location = New System.Drawing.Point(454, 27)
        Me.chkManual.MyLinkLable1 = Nothing
        Me.chkManual.MyLinkLable2 = Nothing
        Me.chkManual.Name = "chkManual"
        Me.chkManual.Size = New System.Drawing.Size(112, 18)
        Me.chkManual.TabIndex = 3
        Me.chkManual.Tag1 = Nothing
        Me.chkManual.Text = "Manual Complaint"
        '
        'rdmenufile
        '
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenufile})
        Me.rdmenufile.Location = New System.Drawing.Point(3, 3)
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Size = New System.Drawing.Size(780, 20)
        Me.rdmenufile.TabIndex = 105
        '
        'RadMenufile
        '
        Me.RadMenufile.AccessibleDescription = "File"
        Me.RadMenufile.AccessibleName = "File"
        Me.RadMenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenufile.Name = "RadMenufile"
        Me.RadMenufile.Text = "Setting"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Email And SMS Setting"
        '
        'FrmComplaintDetailEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(786, 665)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmComplaintDetailEntry"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Complaint Detail Entry"
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.txtworkorderno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtbillno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtaddamt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtbillamt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chksparepart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtpendrsn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtsecresn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtresponse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtprimarydesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtreptserialno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel27, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtapexno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdpending, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdcomplt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcmpldt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtsrvcdlrname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtremarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdnotcmplt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txttdmdesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcompgivento, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcompgivenby, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtphnno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdistributor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Type, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txttagno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtserialno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSize, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtmodel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtmake, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtassetdesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcmplntdesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcountry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtstate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtoutletadd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtoutletdesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.RadPageViewPage2.PerformLayout()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.RadGroupBox5.PerformLayout()
        CType(Me.txtWorkOrder1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel46, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel47, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBillNo1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAdditionalCharge, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel48, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel49, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBillAmt1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPending1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel45, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtResponseTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel42, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel43, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCompleteDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemarks1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel44, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkUserSparePart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnPending, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnComleted, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnNotCompleted, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtService1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtfranchise1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSecondary1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtComplaintType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtprimary1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtComplaintGivento, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel41, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtComplaintGivenBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rptdSerialNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel39, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboApex1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel30, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTag1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSerialNo1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSize1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtModel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMake1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAssetType1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel38, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLocation1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCountry1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtState1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCity1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPhn1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel32, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel33, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel34, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel35, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel36, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel37, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel26, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel28, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel29, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel31, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOutletName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.btnsend, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCopy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.chkManual, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtcomid As common.UserControls.txtNavigator
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtoutletdesc As common.Controls.MyLabel
    Friend WithEvents txtoutletcode As common.UserControls.txtFinder
    Friend WithEvents txtoutletadd As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtcountry As common.Controls.MyLabel
    Friend WithEvents txtstate As common.Controls.MyLabel
    Friend WithEvents txtcity As common.Controls.MyLabel
    Friend WithEvents txtcmplntdesc As common.Controls.MyLabel
    Friend WithEvents txtcomplntcode As common.UserControls.txtFinder
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents txtassetdesc As common.Controls.MyLabel
    Friend WithEvents txtassetcode As common.UserControls.txtFinder
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents txtSize As common.Controls.MyLabel
    Friend WithEvents txtmodel As common.Controls.MyLabel
    Friend WithEvents txtmake As common.Controls.MyLabel
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents txttagno As common.Controls.MyTextBox
    Friend WithEvents txtserialno As common.Controls.MyTextBox
    Friend WithEvents txtdistributor As common.Controls.MyLabel
    Friend WithEvents Type As common.Controls.MyLabel
    Friend WithEvents txtphnno As common.Controls.MyTextBox
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents txtcompgivento As common.Controls.MyTextBox
    Friend WithEvents txtcompgivenby As common.Controls.MyTextBox
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
    Friend WithEvents txtsrvcdlrname As common.Controls.MyLabel
    Friend WithEvents txtsrvcdlr As common.UserControls.txtFinder
    Friend WithEvents MyLabel17 As common.Controls.MyLabel
    Friend WithEvents txttdmdesc As common.Controls.MyLabel
    Friend WithEvents txttdmcode As common.UserControls.txtFinder
    Friend WithEvents MyLabel19 As common.Controls.MyLabel
    Friend WithEvents rdcomplt As common.Controls.MyRadioButton
    Friend WithEvents MyLabel20 As common.Controls.MyLabel
    Friend WithEvents txtcmpldt As common.Controls.MyDateTimePicker
    Friend WithEvents rdnotcmplt As common.Controls.MyRadioButton
    Friend WithEvents MyLabel18 As common.Controls.MyLabel
    Friend WithEvents txtremarks As common.Controls.MyTextBox
    Friend WithEvents MyLabel23 As common.Controls.MyLabel
    Friend WithEvents MyLabel22 As common.Controls.MyLabel
    Friend WithEvents MyLabel21 As common.Controls.MyLabel
    Friend WithEvents rdpending As common.Controls.MyRadioButton
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents txtaddamt As common.Controls.MyTextBox
    Friend WithEvents txtbillamt As common.Controls.MyTextBox
    Friend WithEvents txtbillno As common.Controls.MyTextBox
    Friend WithEvents txtapexno As common.Controls.MyComboBox
    Friend WithEvents chksparepart As common.Controls.MyCheckBox
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents txtresponse As common.Controls.MyTextBox
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents txtreptserialno As common.Controls.MyLabel
    Friend WithEvents txtdesc As common.Controls.MyTextBox
    Friend WithEvents MyLabel24 As common.Controls.MyLabel
    Friend WithEvents txtworkorderno As common.Controls.MyTextBox
    Friend WithEvents MyLabel25 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtprimarydesc As common.Controls.MyLabel
    Friend WithEvents txtprimarycode As common.UserControls.txtFinder
    Friend WithEvents MyLabel27 As common.Controls.MyLabel
    Friend WithEvents txtsecresn As common.Controls.MyLabel
    Friend WithEvents txtseccode As common.UserControls.txtFinder
    Friend WithEvents txtpendcode As common.UserControls.txtFinder
    Friend WithEvents txtpendrsn As common.Controls.MyLabel
    Friend WithEvents btnCopy As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenufile As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnsend As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents txtSize1 As common.Controls.MyTextBox
    Friend WithEvents txtModel1 As common.Controls.MyTextBox
    Friend WithEvents txtMake1 As common.Controls.MyTextBox
    Friend WithEvents txtAssetType1 As common.Controls.MyTextBox
    Friend WithEvents MyLabel38 As common.Controls.MyLabel
    Friend WithEvents txtLocation1 As common.Controls.MyTextBox
    Friend WithEvents txtCountry1 As common.Controls.MyTextBox
    Friend WithEvents txtState1 As common.Controls.MyTextBox
    Friend WithEvents txtCity1 As common.Controls.MyTextBox
    Friend WithEvents txtPhn1 As common.Controls.MyTextBox
    Friend WithEvents MyLabel32 As common.Controls.MyLabel
    Friend WithEvents MyLabel33 As common.Controls.MyLabel
    Friend WithEvents MyLabel34 As common.Controls.MyLabel
    Friend WithEvents MyLabel35 As common.Controls.MyLabel
    Friend WithEvents MyLabel36 As common.Controls.MyLabel
    Friend WithEvents MyLabel37 As common.Controls.MyLabel
    Friend WithEvents MyLabel26 As common.Controls.MyLabel
    Friend WithEvents MyLabel28 As common.Controls.MyLabel
    Friend WithEvents MyLabel29 As common.Controls.MyLabel
    Friend WithEvents lblCity As common.Controls.MyLabel
    Friend WithEvents MyLabel31 As common.Controls.MyLabel
    Friend WithEvents txtType As common.Controls.MyTextBox
    Friend WithEvents lblType As common.Controls.MyLabel
    Friend WithEvents txtOutletName As common.Controls.MyTextBox
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents MyLabel39 As common.Controls.MyLabel
    Friend WithEvents cboApex1 As common.Controls.MyComboBox
    Friend WithEvents MyLabel30 As common.Controls.MyLabel
    Friend WithEvents txtTag1 As common.Controls.MyTextBox
    Friend WithEvents txtSerialNo1 As common.Controls.MyTextBox
    Friend WithEvents txtComplaintGivento As common.Controls.MyTextBox
    Friend WithEvents MyLabel41 As common.Controls.MyLabel
    Friend WithEvents txtComplaintGivenBy As common.Controls.MyTextBox
    Friend WithEvents rptdSerialNo As common.Controls.MyLabel
    Friend WithEvents txtSecondary1 As common.Controls.MyTextBox
    Friend WithEvents txtComplaintType As common.Controls.MyTextBox
    Friend WithEvents txtprimary1 As common.Controls.MyTextBox
    Friend WithEvents txtPending1 As common.Controls.MyTextBox
    Friend WithEvents MyLabel45 As common.Controls.MyLabel
    Friend WithEvents txtResponseTime As common.Controls.MyTextBox
    Friend WithEvents MyLabel42 As common.Controls.MyLabel
    Friend WithEvents MyLabel43 As common.Controls.MyLabel
    Friend WithEvents txtCompleteDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtRemarks1 As common.Controls.MyTextBox
    Friend WithEvents MyLabel44 As common.Controls.MyLabel
    Friend WithEvents ChkUserSparePart As common.Controls.MyCheckBox
    Friend WithEvents rbtnPending As common.Controls.MyRadioButton
    Friend WithEvents rbtnComleted As common.Controls.MyRadioButton
    Friend WithEvents rbtnNotCompleted As common.Controls.MyRadioButton
    Friend WithEvents txtService1 As common.Controls.MyTextBox
    Friend WithEvents txtfranchise1 As common.Controls.MyTextBox
    Friend WithEvents RadGroupBox5 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtWorkOrder1 As common.Controls.MyTextBox
    Friend WithEvents MyLabel46 As common.Controls.MyLabel
    Friend WithEvents MyLabel47 As common.Controls.MyLabel
    Friend WithEvents txtBillNo1 As common.Controls.MyTextBox
    Friend WithEvents txtAdditionalCharge As common.Controls.MyTextBox
    Friend WithEvents MyLabel48 As common.Controls.MyLabel
    Friend WithEvents MyLabel49 As common.Controls.MyLabel
    Friend WithEvents txtBillAmt1 As common.Controls.MyTextBox
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv2 As common.UserControls.MyRadGridView
    Friend WithEvents chkManual As common.Controls.MyCheckBox
End Class

