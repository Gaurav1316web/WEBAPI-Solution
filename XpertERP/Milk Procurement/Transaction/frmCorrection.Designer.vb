<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmCorrection
    Inherits XpertERPEngine.FrmMainTranScreen

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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkAddMissingSample = New common.Controls.MyCheckBox()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.cboShift = New common.Controls.MyComboBox()
        Me.lblBOMStatus = New common.Controls.MyLabel()
        Me.txtShiftDate = New common.Controls.MyDateTimePicker()
        Me.txtMCC = New common.UserControls.txtFinder()
        Me.lblMCCCode = New common.Controls.MyLabel()
        Me.lblMcc = New common.Controls.MyLabel()
        Me.lblRouteCode = New common.Controls.MyLabel()
        Me.txtVLC = New common.UserControls.txtFinder()
        Me.lblVLC = New common.Controls.MyLabel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkCorrection = New common.Controls.MyCheckBox()
        Me.chkRetesting = New common.Controls.MyCheckBox()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.lblUOM = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.lblSRNNo = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.cboMilkType = New common.Controls.MyComboBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.TxtFinder1 = New common.UserControls.txtFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtQty = New common.MyNumBox()
        Me.LblManualFAT_Per = New common.Controls.MyLabel()
        Me.txtSNF = New common.MyNumBox()
        Me.LblManualSNF_Per = New common.Controls.MyLabel()
        Me.txtFAT = New common.MyNumBox()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkAdjustOwnBMCFATSNF = New common.Controls.MyCheckBox()
        Me.txtVLCCMMCC = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.txtVLCCMToDate = New common.Controls.MyDateTimePicker()
        Me.cboVLCCMShift = New Telerik.WinControls.UI.RadDropDownList()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.txtVLCCMFromDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.RadButton2 = New Telerik.WinControls.UI.RadButton()
        Me.RadPageViewPage5 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.GroupBox91 = New System.Windows.Forms.GroupBox()
        Me.txtMPCMMCC = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel59 = New common.Controls.MyLabel()
        Me.txtMPCMToDate = New common.Controls.MyDateTimePicker()
        Me.cboMPCMShift = New Telerik.WinControls.UI.RadDropDownList()
        Me.MyLabel60 = New common.Controls.MyLabel()
        Me.txtMPCMFromDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel61 = New common.Controls.MyLabel()
        Me.RadButton290 = New Telerik.WinControls.UI.RadButton()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.GroupBox76 = New System.Windows.Forms.GroupBox()
        Me.chkDeleteBMCCollection = New common.Controls.MyCheckBox()
        Me.TxtMultiSelectFinder8 = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel40 = New common.Controls.MyLabel()
        Me.txtMCCToDate = New common.Controls.MyDateTimePicker()
        Me.txtFromShift = New Telerik.WinControls.UI.RadDropDownList()
        Me.MyLabel39 = New common.Controls.MyLabel()
        Me.txtMCCFromDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel41 = New common.Controls.MyLabel()
        Me.BulkDelete = New Telerik.WinControls.UI.RadButton()
        Me.RadPageViewPage4 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox7 = New Telerik.WinControls.UI.RadGroupBox()
        Me.btnExport = New Telerik.WinControls.UI.RadButton()
        Me.btnImport = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadButton3 = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.lblBMCRoute = New common.Controls.MyLabel()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.txtBMCDate = New common.Controls.MyDateTimePicker()
        Me.txtBMCBMC = New common.UserControls.txtFinder()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.lblBMCBMC = New common.Controls.MyLabel()
        Me.txtBMCRouteNo = New common.UserControls.txtFinder()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtRetestingCLR = New common.MyNumBox()
        Me.MyLabel22 = New common.Controls.MyLabel()
        Me.lblBMCStatus = New common.Controls.MyLabel()
        Me.MyLabel19 = New common.Controls.MyLabel()
        Me.lblBMCDetailNo = New common.Controls.MyLabel()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.MyLabel18 = New common.Controls.MyLabel()
        Me.lblBMCSno = New common.Controls.MyLabel()
        Me.MyLabel21 = New common.Controls.MyLabel()
        Me.lblBMCDocNo = New common.Controls.MyLabel()
        Me.lblBMCCorrBMC = New common.Controls.MyLabel()
        Me.cboBMCCorrMilkType = New common.Controls.MyComboBox()
        Me.MyLabel24 = New common.Controls.MyLabel()
        Me.RadButton4 = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel25 = New common.Controls.MyLabel()
        Me.RadButton5 = New Telerik.WinControls.UI.RadButton()
        Me.txtBMCCorrBMC = New common.UserControls.txtFinder()
        Me.MyLabel26 = New common.Controls.MyLabel()
        Me.txtBMCCorrQty = New common.MyNumBox()
        Me.MyLabel27 = New common.Controls.MyLabel()
        Me.txtBMCCorrSNF = New common.MyNumBox()
        Me.MyLabel28 = New common.Controls.MyLabel()
        Me.txtBMCCorrFAT = New common.MyNumBox()
        Me.RadPageViewPage6 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox8 = New Telerik.WinControls.UI.RadGroupBox()
        Me.btnTankerMilkImport = New Telerik.WinControls.UI.RadButton()
        Me.btnTankerMilkExport = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadButton6 = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel15 = New common.Controls.MyLabel()
        Me.lblBMCTankerRoute = New common.Controls.MyLabel()
        Me.MyLabel20 = New common.Controls.MyLabel()
        Me.txtBMCTankerDate = New common.Controls.MyDateTimePicker()
        Me.txtBMCTankerRoute = New common.UserControls.txtFinder()
        Me.RadGroupBox6 = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyLabel23 = New common.Controls.MyLabel()
        Me.MyLabel17 = New common.Controls.MyLabel()
        Me.lblBMCTankerSNFKG = New common.Controls.MyLabel()
        Me.MyLabel32 = New common.Controls.MyLabel()
        Me.lblBMCTankerFATKG = New common.Controls.MyLabel()
        Me.lblBMCTankerTripNo = New common.Controls.MyLabel()
        Me.MyLabel35 = New common.Controls.MyLabel()
        Me.lblBMCTankerDocNo = New common.Controls.MyLabel()
        Me.RadButton7 = New Telerik.WinControls.UI.RadButton()
        Me.RadButton8 = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel43 = New common.Controls.MyLabel()
        Me.txtBMCTankerQty = New common.MyNumBox()
        Me.MyLabel44 = New common.Controls.MyLabel()
        Me.txtBMCTankerSNF = New common.MyNumBox()
        Me.MyLabel45 = New common.Controls.MyLabel()
        Me.txtBMCTankerFAT = New common.MyNumBox()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.chkPreviousShift = New common.Controls.MyCheckBox()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.chkAddMissingSample, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboShift, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBOMStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtShiftDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMCCCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMcc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRouteCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVLC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.chkCorrection, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRetesting, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblUOM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSRNNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboMilkType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblManualFAT_Per, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSNF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblManualSNF_Per, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFAT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.chkAdjustOwnBMCFATSNF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVLCCMToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboVLCCMShift, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVLCCMFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage5.SuspendLayout()
        Me.GroupBox91.SuspendLayout()
        CType(Me.MyLabel59, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMPCMToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboMPCMShift, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel60, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMPCMFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel61, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton290, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        Me.GroupBox76.SuspendLayout()
        CType(Me.chkDeleteBMCCollection, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel40, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMCCToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromShift, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel39, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMCCFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel41, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BulkDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage4.SuspendLayout()
        CType(Me.RadGroupBox7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox7.SuspendLayout()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnImport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.RadButton3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBMCRoute, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBMCDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBMCBMC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.txtRetestingCLR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBMCStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBMCDetailNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBMCSno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBMCDocNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBMCCorrBMC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboBMCCorrMilkType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel26, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBMCCorrQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel27, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBMCCorrSNF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel28, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBMCCorrFAT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage6.SuspendLayout()
        CType(Me.RadGroupBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox8.SuspendLayout()
        CType(Me.btnTankerMilkImport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnTankerMilkExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        CType(Me.RadButton6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBMCTankerRoute, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBMCTankerDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox6.SuspendLayout()
        CType(Me.MyLabel23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBMCTankerSNFKG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel32, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBMCTankerFATKG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBMCTankerTripNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel35, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBMCTankerDocNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel43, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBMCTankerQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel44, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBMCTankerSNF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel45, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBMCTankerFAT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPreviousShift, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(667, 463)
        Me.SplitContainer1.SplitterDistance = 424
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage5)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage4)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage6)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage2
        Me.RadPageView1.Size = New System.Drawing.Size(667, 424)
        Me.RadPageView1.TabIndex = 2
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(91.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(646, 376)
        Me.RadPageViewPage1.Text = "VLC Correction"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.chkAddMissingSample)
        Me.RadGroupBox2.Controls.Add(Me.RadButton1)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox2.Controls.Add(Me.cboShift)
        Me.RadGroupBox2.Controls.Add(Me.lblBOMStatus)
        Me.RadGroupBox2.Controls.Add(Me.txtShiftDate)
        Me.RadGroupBox2.Controls.Add(Me.txtMCC)
        Me.RadGroupBox2.Controls.Add(Me.lblRouteCode)
        Me.RadGroupBox2.Controls.Add(Me.lblMCCCode)
        Me.RadGroupBox2.Controls.Add(Me.txtVLC)
        Me.RadGroupBox2.Controls.Add(Me.lblMcc)
        Me.RadGroupBox2.Controls.Add(Me.lblVLC)
        Me.RadGroupBox2.HeaderText = "Filter"
        Me.RadGroupBox2.Location = New System.Drawing.Point(3, 3)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Size = New System.Drawing.Size(566, 86)
        Me.RadGroupBox2.TabIndex = 0
        Me.RadGroupBox2.Text = "Filter"
        '
        'chkAddMissingSample
        '
        Me.chkAddMissingSample.Location = New System.Drawing.Point(381, 16)
        Me.chkAddMissingSample.MyLinkLable1 = Nothing
        Me.chkAddMissingSample.MyLinkLable2 = Nothing
        Me.chkAddMissingSample.Name = "chkAddMissingSample"
        Me.chkAddMissingSample.Size = New System.Drawing.Size(122, 18)
        Me.chkAddMissingSample.TabIndex = 369
        Me.chkAddMissingSample.Tag1 = Nothing
        Me.chkAddMissingSample.Text = "Add Missing Sample"
        '
        'RadButton1
        '
        Me.RadButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadButton1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton1.Location = New System.Drawing.Point(495, 61)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(66, 21)
        Me.RadButton1.TabIndex = 4
        Me.RadButton1.Text = ">>"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(5, 17)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(56, 16)
        Me.MyLabel3.TabIndex = 8
        Me.MyLabel3.Text = "Shift Date"
        '
        'cboShift
        '
        Me.cboShift.AutoCompleteDisplayMember = Nothing
        Me.cboShift.AutoCompleteValueMember = Nothing
        Me.cboShift.CalculationExpression = Nothing
        Me.cboShift.DropDownAnimationEnabled = True
        Me.cboShift.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboShift.FieldCode = Nothing
        Me.cboShift.FieldDesc = Nothing
        Me.cboShift.FieldMaxLength = 0
        Me.cboShift.FieldName = Nothing
        Me.cboShift.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboShift.isCalculatedField = False
        Me.cboShift.IsSourceFromTable = False
        Me.cboShift.IsSourceFromValueList = False
        Me.cboShift.IsUnique = False
        RadListDataItem1.Text = "M"
        RadListDataItem2.Text = "E"
        Me.cboShift.Items.Add(RadListDataItem1)
        Me.cboShift.Items.Add(RadListDataItem2)
        Me.cboShift.Location = New System.Drawing.Point(273, 16)
        Me.cboShift.MendatroryField = True
        Me.cboShift.MyLinkLable1 = Me.lblBOMStatus
        Me.cboShift.MyLinkLable2 = Nothing
        Me.cboShift.Name = "cboShift"
        Me.cboShift.ReferenceFieldDesc = Nothing
        Me.cboShift.ReferenceFieldName = Nothing
        Me.cboShift.ReferenceTableName = Nothing
        Me.cboShift.Size = New System.Drawing.Size(102, 18)
        Me.cboShift.TabIndex = 1
        '
        'lblBOMStatus
        '
        Me.lblBOMStatus.FieldName = Nothing
        Me.lblBOMStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBOMStatus.Location = New System.Drawing.Point(238, 17)
        Me.lblBOMStatus.Name = "lblBOMStatus"
        Me.lblBOMStatus.Size = New System.Drawing.Size(29, 16)
        Me.lblBOMStatus.TabIndex = 9
        Me.lblBOMStatus.Text = "Shift"
        '
        'txtShiftDate
        '
        Me.txtShiftDate.CalculationExpression = Nothing
        Me.txtShiftDate.CustomFormat = "dd/MM/yyyy"
        Me.txtShiftDate.FieldCode = Nothing
        Me.txtShiftDate.FieldDesc = Nothing
        Me.txtShiftDate.FieldMaxLength = 0
        Me.txtShiftDate.FieldName = Nothing
        Me.txtShiftDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShiftDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtShiftDate.isCalculatedField = False
        Me.txtShiftDate.IsSourceFromTable = False
        Me.txtShiftDate.IsSourceFromValueList = False
        Me.txtShiftDate.IsUnique = False
        Me.txtShiftDate.Location = New System.Drawing.Point(71, 16)
        Me.txtShiftDate.MendatroryField = True
        Me.txtShiftDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtShiftDate.MyLinkLable1 = Me.MyLabel3
        Me.txtShiftDate.MyLinkLable2 = Nothing
        Me.txtShiftDate.Name = "txtShiftDate"
        Me.txtShiftDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtShiftDate.ReferenceFieldDesc = Nothing
        Me.txtShiftDate.ReferenceFieldName = Nothing
        Me.txtShiftDate.ReferenceTableName = Nothing
        Me.txtShiftDate.Size = New System.Drawing.Size(156, 18)
        Me.txtShiftDate.TabIndex = 0
        Me.txtShiftDate.TabStop = False
        Me.txtShiftDate.Text = "03/05/2011"
        Me.txtShiftDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'txtMCC
        '
        Me.txtMCC.CalculationExpression = Nothing
        Me.txtMCC.FieldCode = Nothing
        Me.txtMCC.FieldDesc = Nothing
        Me.txtMCC.FieldMaxLength = 0
        Me.txtMCC.FieldName = Nothing
        Me.txtMCC.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtMCC.isCalculatedField = False
        Me.txtMCC.IsSourceFromTable = False
        Me.txtMCC.IsSourceFromValueList = False
        Me.txtMCC.IsUnique = False
        Me.txtMCC.Location = New System.Drawing.Point(71, 37)
        Me.txtMCC.MendatroryField = True
        Me.txtMCC.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMCC.MyLinkLable1 = Me.lblMCCCode
        Me.txtMCC.MyLinkLable2 = Me.lblMcc
        Me.txtMCC.MyReadOnly = False
        Me.txtMCC.MyShowMasterFormButton = False
        Me.txtMCC.Name = "txtMCC"
        Me.txtMCC.ReferenceFieldDesc = Nothing
        Me.txtMCC.ReferenceFieldName = Nothing
        Me.txtMCC.ReferenceTableName = Nothing
        Me.txtMCC.Size = New System.Drawing.Size(157, 21)
        Me.txtMCC.TabIndex = 2
        Me.txtMCC.Value = ""
        '
        'lblMCCCode
        '
        Me.lblMCCCode.FieldName = Nothing
        Me.lblMCCCode.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblMCCCode.Location = New System.Drawing.Point(5, 38)
        Me.lblMCCCode.Name = "lblMCCCode"
        Me.lblMCCCode.Size = New System.Drawing.Size(60, 18)
        Me.lblMCCCode.TabIndex = 7
        Me.lblMCCCode.Text = "MCC Code"
        '
        'lblMcc
        '
        Me.lblMcc.AutoSize = False
        Me.lblMcc.BorderVisible = True
        Me.lblMcc.FieldName = Nothing
        Me.lblMcc.Location = New System.Drawing.Point(234, 37)
        Me.lblMcc.Name = "lblMcc"
        Me.lblMcc.Size = New System.Drawing.Size(260, 21)
        Me.lblMcc.TabIndex = 10
        '
        'lblRouteCode
        '
        Me.lblRouteCode.FieldName = Nothing
        Me.lblRouteCode.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblRouteCode.Location = New System.Drawing.Point(5, 62)
        Me.lblRouteCode.Name = "lblRouteCode"
        Me.lblRouteCode.Size = New System.Drawing.Size(55, 18)
        Me.lblRouteCode.TabIndex = 6
        Me.lblRouteCode.Text = "VLC Code"
        '
        'txtVLC
        '
        Me.txtVLC.CalculationExpression = Nothing
        Me.txtVLC.FieldCode = Nothing
        Me.txtVLC.FieldDesc = Nothing
        Me.txtVLC.FieldMaxLength = 0
        Me.txtVLC.FieldName = Nothing
        Me.txtVLC.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtVLC.isCalculatedField = False
        Me.txtVLC.IsSourceFromTable = False
        Me.txtVLC.IsSourceFromValueList = False
        Me.txtVLC.IsUnique = False
        Me.txtVLC.Location = New System.Drawing.Point(71, 61)
        Me.txtVLC.MendatroryField = True
        Me.txtVLC.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVLC.MyLinkLable1 = Me.lblRouteCode
        Me.txtVLC.MyLinkLable2 = Nothing
        Me.txtVLC.MyReadOnly = False
        Me.txtVLC.MyShowMasterFormButton = False
        Me.txtVLC.Name = "txtVLC"
        Me.txtVLC.ReferenceFieldDesc = Nothing
        Me.txtVLC.ReferenceFieldName = Nothing
        Me.txtVLC.ReferenceTableName = Nothing
        Me.txtVLC.Size = New System.Drawing.Size(157, 21)
        Me.txtVLC.TabIndex = 3
        Me.txtVLC.Value = ""
        '
        'lblVLC
        '
        Me.lblVLC.AutoSize = False
        Me.lblVLC.BorderVisible = True
        Me.lblVLC.FieldName = Nothing
        Me.lblVLC.Location = New System.Drawing.Point(234, 61)
        Me.lblVLC.Name = "lblVLC"
        Me.lblVLC.Size = New System.Drawing.Size(260, 21)
        Me.lblVLC.TabIndex = 11
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.chkCorrection)
        Me.RadGroupBox1.Controls.Add(Me.chkRetesting)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel8)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel6)
        Me.RadGroupBox1.Controls.Add(Me.lblUOM)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel7)
        Me.RadGroupBox1.Controls.Add(Me.lblSRNNo)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel5)
        Me.RadGroupBox1.Controls.Add(Me.cboMilkType)
        Me.RadGroupBox1.Controls.Add(Me.btnnew)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel4)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.btnSave)
        Me.RadGroupBox1.Controls.Add(Me.TxtFinder1)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.txtQty)
        Me.RadGroupBox1.Controls.Add(Me.LblManualFAT_Per)
        Me.RadGroupBox1.Controls.Add(Me.txtSNF)
        Me.RadGroupBox1.Controls.Add(Me.txtFAT)
        Me.RadGroupBox1.Controls.Add(Me.LblManualSNF_Per)
        Me.RadGroupBox1.HeaderText = "Correction"
        Me.RadGroupBox1.Location = New System.Drawing.Point(3, 91)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(437, 201)
        Me.RadGroupBox1.TabIndex = 1
        Me.RadGroupBox1.Text = "Correction"
        '
        'chkCorrection
        '
        Me.chkCorrection.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCorrection.Enabled = False
        Me.chkCorrection.Location = New System.Drawing.Point(358, 22)
        Me.chkCorrection.MyLinkLable1 = Nothing
        Me.chkCorrection.MyLinkLable2 = Nothing
        Me.chkCorrection.Name = "chkCorrection"
        Me.chkCorrection.Size = New System.Drawing.Size(72, 18)
        Me.chkCorrection.TabIndex = 371
        Me.chkCorrection.Tag1 = Nothing
        Me.chkCorrection.Text = "Correction"
        Me.chkCorrection.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        Me.chkCorrection.Visible = False
        '
        'chkRetesting
        '
        Me.chkRetesting.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkRetesting.Enabled = False
        Me.chkRetesting.Location = New System.Drawing.Point(359, 21)
        Me.chkRetesting.MyLinkLable1 = Nothing
        Me.chkRetesting.MyLinkLable2 = Nothing
        Me.chkRetesting.Name = "chkRetesting"
        Me.chkRetesting.Size = New System.Drawing.Size(67, 18)
        Me.chkRetesting.TabIndex = 370
        Me.chkRetesting.Tag1 = Nothing
        Me.chkRetesting.Text = "Retesting"
        Me.chkRetesting.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel8.Location = New System.Drawing.Point(5, 140)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(59, 18)
        Me.MyLabel8.TabIndex = 18
        Me.MyLabel8.Text = "VLC Name"
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(175, 46)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(33, 16)
        Me.MyLabel6.TabIndex = 17
        Me.MyLabel6.Text = "UOM"
        '
        'lblUOM
        '
        Me.lblUOM.AutoSize = False
        Me.lblUOM.BorderVisible = True
        Me.lblUOM.FieldName = Nothing
        Me.lblUOM.Location = New System.Drawing.Point(241, 44)
        Me.lblUOM.Name = "lblUOM"
        Me.lblUOM.Size = New System.Drawing.Size(102, 21)
        Me.lblUOM.TabIndex = 16
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(5, 23)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(53, 16)
        Me.MyLabel7.TabIndex = 14
        Me.MyLabel7.Text = "Milk SRN"
        '
        'lblSRNNo
        '
        Me.lblSRNNo.AutoSize = False
        Me.lblSRNNo.BorderVisible = True
        Me.lblSRNNo.FieldName = Nothing
        Me.lblSRNNo.Location = New System.Drawing.Point(71, 21)
        Me.lblSRNNo.Name = "lblSRNNo"
        Me.lblSRNNo.Size = New System.Drawing.Size(272, 21)
        Me.lblSRNNo.TabIndex = 13
        '
        'MyLabel5
        '
        Me.MyLabel5.AutoSize = False
        Me.MyLabel5.BorderVisible = True
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Location = New System.Drawing.Point(71, 139)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(272, 21)
        Me.MyLabel5.TabIndex = 12
        '
        'cboMilkType
        '
        Me.cboMilkType.AutoCompleteDisplayMember = Nothing
        Me.cboMilkType.AutoCompleteValueMember = Nothing
        Me.cboMilkType.CalculationExpression = Nothing
        Me.cboMilkType.DropDownAnimationEnabled = True
        Me.cboMilkType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboMilkType.FieldCode = Nothing
        Me.cboMilkType.FieldDesc = Nothing
        Me.cboMilkType.FieldMaxLength = 0
        Me.cboMilkType.FieldName = Nothing
        Me.cboMilkType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboMilkType.isCalculatedField = False
        Me.cboMilkType.IsSourceFromTable = False
        Me.cboMilkType.IsSourceFromValueList = False
        Me.cboMilkType.IsUnique = False
        RadListDataItem3.Text = "M"
        RadListDataItem4.Text = "E"
        Me.cboMilkType.Items.Add(RadListDataItem3)
        Me.cboMilkType.Items.Add(RadListDataItem4)
        Me.cboMilkType.Location = New System.Drawing.Point(71, 92)
        Me.cboMilkType.MendatroryField = True
        Me.cboMilkType.MyLinkLable1 = Me.MyLabel4
        Me.cboMilkType.MyLinkLable2 = Nothing
        Me.cboMilkType.Name = "cboMilkType"
        Me.cboMilkType.ReferenceFieldDesc = Nothing
        Me.cboMilkType.ReferenceFieldName = Nothing
        Me.cboMilkType.ReferenceTableName = Nothing
        Me.cboMilkType.Size = New System.Drawing.Size(272, 18)
        Me.cboMilkType.TabIndex = 3
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(5, 93)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(55, 16)
        Me.MyLabel4.TabIndex = 7
        Me.MyLabel4.Text = "Milk Type"
        '
        'btnnew
        '
        Me.btnnew.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnnew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnnew.Location = New System.Drawing.Point(256, 173)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(88, 21)
        Me.btnnew.TabIndex = 6
        Me.btnnew.Text = "Reset"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel2.Location = New System.Drawing.Point(5, 115)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(55, 18)
        Me.MyLabel2.TabIndex = 6
        Me.MyLabel2.Text = "VLC Code"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(71, 173)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(88, 21)
        Me.btnSave.TabIndex = 5
        Me.btnSave.Text = "Apply"
        '
        'TxtFinder1
        '
        Me.TxtFinder1.CalculationExpression = Nothing
        Me.TxtFinder1.FieldCode = Nothing
        Me.TxtFinder1.FieldDesc = Nothing
        Me.TxtFinder1.FieldMaxLength = 0
        Me.TxtFinder1.FieldName = Nothing
        Me.TxtFinder1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.TxtFinder1.isCalculatedField = False
        Me.TxtFinder1.IsSourceFromTable = False
        Me.TxtFinder1.IsSourceFromValueList = False
        Me.TxtFinder1.IsUnique = False
        Me.TxtFinder1.Location = New System.Drawing.Point(71, 114)
        Me.TxtFinder1.MendatroryField = True
        Me.TxtFinder1.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFinder1.MyLinkLable1 = Me.MyLabel2
        Me.TxtFinder1.MyLinkLable2 = Nothing
        Me.TxtFinder1.MyReadOnly = False
        Me.TxtFinder1.MyShowMasterFormButton = False
        Me.TxtFinder1.Name = "TxtFinder1"
        Me.TxtFinder1.ReferenceFieldDesc = Nothing
        Me.TxtFinder1.ReferenceFieldName = Nothing
        Me.TxtFinder1.ReferenceTableName = Nothing
        Me.TxtFinder1.Size = New System.Drawing.Size(272, 21)
        Me.TxtFinder1.TabIndex = 4
        Me.TxtFinder1.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(5, 47)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(24, 16)
        Me.MyLabel1.TabIndex = 10
        Me.MyLabel1.Text = "Qty"
        '
        'txtQty
        '
        Me.txtQty.BackColor = System.Drawing.Color.White
        Me.txtQty.CalculationExpression = Nothing
        Me.txtQty.DecimalPlaces = 3
        Me.txtQty.FieldCode = Nothing
        Me.txtQty.FieldDesc = Nothing
        Me.txtQty.FieldMaxLength = 0
        Me.txtQty.FieldName = Nothing
        Me.txtQty.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtQty.isCalculatedField = False
        Me.txtQty.IsSourceFromTable = False
        Me.txtQty.IsSourceFromValueList = False
        Me.txtQty.IsUnique = False
        Me.txtQty.Location = New System.Drawing.Point(71, 45)
        Me.txtQty.MendatroryField = False
        Me.txtQty.MyLinkLable1 = Me.MyLabel1
        Me.txtQty.MyLinkLable2 = Nothing
        Me.txtQty.Name = "txtQty"
        Me.txtQty.ReferenceFieldDesc = Nothing
        Me.txtQty.ReferenceFieldName = Nothing
        Me.txtQty.ReferenceTableName = Nothing
        Me.txtQty.Size = New System.Drawing.Size(102, 20)
        Me.txtQty.TabIndex = 0
        Me.txtQty.Text = "0"
        Me.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtQty.Value = 0R
        '
        'LblManualFAT_Per
        '
        Me.LblManualFAT_Per.FieldName = Nothing
        Me.LblManualFAT_Per.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblManualFAT_Per.Location = New System.Drawing.Point(5, 70)
        Me.LblManualFAT_Per.Name = "LblManualFAT_Per"
        Me.LblManualFAT_Per.Size = New System.Drawing.Size(28, 16)
        Me.LblManualFAT_Per.TabIndex = 9
        Me.LblManualFAT_Per.Text = "FAT"
        '
        'txtSNF
        '
        Me.txtSNF.BackColor = System.Drawing.Color.White
        Me.txtSNF.CalculationExpression = Nothing
        Me.txtSNF.DecimalPlaces = 2
        Me.txtSNF.FieldCode = Nothing
        Me.txtSNF.FieldDesc = Nothing
        Me.txtSNF.FieldMaxLength = 0
        Me.txtSNF.FieldName = Nothing
        Me.txtSNF.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtSNF.isCalculatedField = False
        Me.txtSNF.IsSourceFromTable = False
        Me.txtSNF.IsSourceFromValueList = False
        Me.txtSNF.IsUnique = False
        Me.txtSNF.Location = New System.Drawing.Point(241, 68)
        Me.txtSNF.MendatroryField = False
        Me.txtSNF.MyLinkLable1 = Me.LblManualSNF_Per
        Me.txtSNF.MyLinkLable2 = Nothing
        Me.txtSNF.Name = "txtSNF"
        Me.txtSNF.ReferenceFieldDesc = Nothing
        Me.txtSNF.ReferenceFieldName = Nothing
        Me.txtSNF.ReferenceTableName = Nothing
        Me.txtSNF.Size = New System.Drawing.Size(102, 20)
        Me.txtSNF.TabIndex = 2
        Me.txtSNF.Text = "0"
        Me.txtSNF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSNF.Value = 0R
        '
        'LblManualSNF_Per
        '
        Me.LblManualSNF_Per.FieldName = Nothing
        Me.LblManualSNF_Per.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblManualSNF_Per.Location = New System.Drawing.Point(175, 70)
        Me.LblManualSNF_Per.Name = "LblManualSNF_Per"
        Me.LblManualSNF_Per.Size = New System.Drawing.Size(29, 16)
        Me.LblManualSNF_Per.TabIndex = 8
        Me.LblManualSNF_Per.Text = "SNF"
        '
        'txtFAT
        '
        Me.txtFAT.BackColor = System.Drawing.Color.White
        Me.txtFAT.CalculationExpression = Nothing
        Me.txtFAT.DecimalPlaces = 1
        Me.txtFAT.FieldCode = Nothing
        Me.txtFAT.FieldDesc = Nothing
        Me.txtFAT.FieldMaxLength = 0
        Me.txtFAT.FieldName = Nothing
        Me.txtFAT.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtFAT.isCalculatedField = False
        Me.txtFAT.IsSourceFromTable = False
        Me.txtFAT.IsSourceFromValueList = False
        Me.txtFAT.IsUnique = False
        Me.txtFAT.Location = New System.Drawing.Point(71, 68)
        Me.txtFAT.MendatroryField = False
        Me.txtFAT.MyLinkLable1 = Me.LblManualFAT_Per
        Me.txtFAT.MyLinkLable2 = Nothing
        Me.txtFAT.Name = "txtFAT"
        Me.txtFAT.ReferenceFieldDesc = Nothing
        Me.txtFAT.ReferenceFieldName = Nothing
        Me.txtFAT.ReferenceTableName = Nothing
        Me.txtFAT.Size = New System.Drawing.Size(102, 20)
        Me.txtFAT.TabIndex = 1
        Me.txtFAT.Text = "0"
        Me.txtFAT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtFAT.Value = 0R
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.GroupBox1)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(135.0!, 28.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(646, 376)
        Me.RadPageViewPage3.Text = "VLC Correction Multiple"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkAdjustOwnBMCFATSNF)
        Me.GroupBox1.Controls.Add(Me.txtVLCCMMCC)
        Me.GroupBox1.Controls.Add(Me.MyLabel9)
        Me.GroupBox1.Controls.Add(Me.txtVLCCMToDate)
        Me.GroupBox1.Controls.Add(Me.cboVLCCMShift)
        Me.GroupBox1.Controls.Add(Me.MyLabel10)
        Me.GroupBox1.Controls.Add(Me.txtVLCCMFromDate)
        Me.GroupBox1.Controls.Add(Me.MyLabel11)
        Me.GroupBox1.Controls.Add(Me.RadButton2)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(340, 96)
        Me.GroupBox1.TabIndex = 368
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Correction OF VLC Data"
        '
        'chkAdjustOwnBMCFATSNF
        '
        Me.chkAdjustOwnBMCFATSNF.Location = New System.Drawing.Point(149, 10)
        Me.chkAdjustOwnBMCFATSNF.MyLinkLable1 = Nothing
        Me.chkAdjustOwnBMCFATSNF.MyLinkLable2 = Nothing
        Me.chkAdjustOwnBMCFATSNF.Name = "chkAdjustOwnBMCFATSNF"
        Me.chkAdjustOwnBMCFATSNF.Size = New System.Drawing.Size(151, 18)
        Me.chkAdjustOwnBMCFATSNF.TabIndex = 370
        Me.chkAdjustOwnBMCFATSNF.Tag1 = Nothing
        Me.chkAdjustOwnBMCFATSNF.Text = "Adjust Own BMC FAT/SNF"
        '
        'txtVLCCMMCC
        '
        Me.txtVLCCMMCC.arrDispalyMember = Nothing
        Me.txtVLCCMMCC.arrValueMember = Nothing
        Me.txtVLCCMMCC.Location = New System.Drawing.Point(40, 52)
        Me.txtVLCCMMCC.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVLCCMMCC.MyLinkLable1 = Nothing
        Me.txtVLCCMMCC.MyLinkLable2 = Nothing
        Me.txtVLCCMMCC.MyNullText = "Please Select..."
        Me.txtVLCCMMCC.Name = "txtVLCCMMCC"
        Me.txtVLCCMMCC.Size = New System.Drawing.Size(259, 19)
        Me.txtVLCCMMCC.TabIndex = 346
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Location = New System.Drawing.Point(129, 31)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(17, 18)
        Me.MyLabel9.TabIndex = 330
        Me.MyLabel9.Text = "to"
        '
        'txtVLCCMToDate
        '
        Me.txtVLCCMToDate.CalculationExpression = Nothing
        Me.txtVLCCMToDate.CustomFormat = "dd/MM/yyyy"
        Me.txtVLCCMToDate.FieldCode = Nothing
        Me.txtVLCCMToDate.FieldDesc = Nothing
        Me.txtVLCCMToDate.FieldMaxLength = 0
        Me.txtVLCCMToDate.FieldName = Nothing
        Me.txtVLCCMToDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVLCCMToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtVLCCMToDate.isCalculatedField = False
        Me.txtVLCCMToDate.IsSourceFromTable = False
        Me.txtVLCCMToDate.IsSourceFromValueList = False
        Me.txtVLCCMToDate.IsUnique = False
        Me.txtVLCCMToDate.Location = New System.Drawing.Point(149, 31)
        Me.txtVLCCMToDate.MendatroryField = False
        Me.txtVLCCMToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtVLCCMToDate.MyLinkLable1 = Nothing
        Me.txtVLCCMToDate.MyLinkLable2 = Nothing
        Me.txtVLCCMToDate.Name = "txtVLCCMToDate"
        Me.txtVLCCMToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtVLCCMToDate.ReferenceFieldDesc = Nothing
        Me.txtVLCCMToDate.ReferenceFieldName = Nothing
        Me.txtVLCCMToDate.ReferenceTableName = Nothing
        Me.txtVLCCMToDate.Size = New System.Drawing.Size(87, 18)
        Me.txtVLCCMToDate.TabIndex = 329
        Me.txtVLCCMToDate.TabStop = False
        Me.txtVLCCMToDate.Text = "13/06/2011"
        Me.txtVLCCMToDate.Value = New Date(2011, 6, 13, 0, 0, 0, 0)
        '
        'cboVLCCMShift
        '
        Me.cboVLCCMShift.AutoCompleteDisplayMember = Nothing
        Me.cboVLCCMShift.AutoCompleteValueMember = Nothing
        Me.cboVLCCMShift.DropDownAnimationEnabled = True
        Me.cboVLCCMShift.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboVLCCMShift.Location = New System.Drawing.Point(239, 30)
        Me.cboVLCCMShift.Name = "cboVLCCMShift"
        Me.cboVLCCMShift.Size = New System.Drawing.Size(60, 20)
        Me.cboVLCCMShift.TabIndex = 328
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Location = New System.Drawing.Point(4, 32)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(30, 18)
        Me.MyLabel10.TabIndex = 60
        Me.MyLabel10.Text = "Date"
        '
        'txtVLCCMFromDate
        '
        Me.txtVLCCMFromDate.CalculationExpression = Nothing
        Me.txtVLCCMFromDate.CustomFormat = "dd/MM/yyyy"
        Me.txtVLCCMFromDate.FieldCode = Nothing
        Me.txtVLCCMFromDate.FieldDesc = Nothing
        Me.txtVLCCMFromDate.FieldMaxLength = 0
        Me.txtVLCCMFromDate.FieldName = Nothing
        Me.txtVLCCMFromDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVLCCMFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtVLCCMFromDate.isCalculatedField = False
        Me.txtVLCCMFromDate.IsSourceFromTable = False
        Me.txtVLCCMFromDate.IsSourceFromValueList = False
        Me.txtVLCCMFromDate.IsUnique = False
        Me.txtVLCCMFromDate.Location = New System.Drawing.Point(40, 31)
        Me.txtVLCCMFromDate.MendatroryField = False
        Me.txtVLCCMFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtVLCCMFromDate.MyLinkLable1 = Nothing
        Me.txtVLCCMFromDate.MyLinkLable2 = Nothing
        Me.txtVLCCMFromDate.Name = "txtVLCCMFromDate"
        Me.txtVLCCMFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtVLCCMFromDate.ReferenceFieldDesc = Nothing
        Me.txtVLCCMFromDate.ReferenceFieldName = Nothing
        Me.txtVLCCMFromDate.ReferenceTableName = Nothing
        Me.txtVLCCMFromDate.Size = New System.Drawing.Size(87, 18)
        Me.txtVLCCMFromDate.TabIndex = 59
        Me.txtVLCCMFromDate.TabStop = False
        Me.txtVLCCMFromDate.Text = "13/06/2011"
        Me.txtVLCCMFromDate.Value = New Date(2011, 6, 13, 0, 0, 0, 0)
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel11.Location = New System.Drawing.Point(5, 53)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(32, 16)
        Me.MyLabel11.TabIndex = 46
        Me.MyLabel11.Text = "MCC"
        '
        'RadButton2
        '
        Me.RadButton2.Location = New System.Drawing.Point(303, 31)
        Me.RadButton2.Name = "RadButton2"
        Me.RadButton2.Size = New System.Drawing.Size(31, 38)
        Me.RadButton2.TabIndex = 13
        Me.RadButton2.Text = ">>"
        '
        'RadPageViewPage5
        '
        Me.RadPageViewPage5.Controls.Add(Me.GroupBox91)
        Me.RadPageViewPage5.ItemSize = New System.Drawing.SizeF(132.0!, 28.0!)
        Me.RadPageViewPage5.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage5.Name = "RadPageViewPage5"
        Me.RadPageViewPage5.Size = New System.Drawing.Size(646, 376)
        Me.RadPageViewPage5.Text = "MP Correction Multiple"
        '
        'GroupBox91
        '
        Me.GroupBox91.Controls.Add(Me.txtMPCMMCC)
        Me.GroupBox91.Controls.Add(Me.MyLabel59)
        Me.GroupBox91.Controls.Add(Me.txtMPCMToDate)
        Me.GroupBox91.Controls.Add(Me.cboMPCMShift)
        Me.GroupBox91.Controls.Add(Me.MyLabel60)
        Me.GroupBox91.Controls.Add(Me.txtMPCMFromDate)
        Me.GroupBox91.Controls.Add(Me.MyLabel61)
        Me.GroupBox91.Controls.Add(Me.RadButton290)
        Me.GroupBox91.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox91.Name = "GroupBox91"
        Me.GroupBox91.Size = New System.Drawing.Size(340, 67)
        Me.GroupBox91.TabIndex = 367
        Me.GroupBox91.TabStop = False
        Me.GroupBox91.Text = "Correction OF MP/Farmer Data"
        '
        'txtMPCMMCC
        '
        Me.txtMPCMMCC.arrDispalyMember = Nothing
        Me.txtMPCMMCC.arrValueMember = Nothing
        Me.txtMPCMMCC.Location = New System.Drawing.Point(40, 40)
        Me.txtMPCMMCC.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMPCMMCC.MyLinkLable1 = Nothing
        Me.txtMPCMMCC.MyLinkLable2 = Nothing
        Me.txtMPCMMCC.MyNullText = "Please Select..."
        Me.txtMPCMMCC.Name = "txtMPCMMCC"
        Me.txtMPCMMCC.Size = New System.Drawing.Size(259, 19)
        Me.txtMPCMMCC.TabIndex = 346
        '
        'MyLabel59
        '
        Me.MyLabel59.FieldName = Nothing
        Me.MyLabel59.Location = New System.Drawing.Point(129, 19)
        Me.MyLabel59.Name = "MyLabel59"
        Me.MyLabel59.Size = New System.Drawing.Size(17, 18)
        Me.MyLabel59.TabIndex = 330
        Me.MyLabel59.Text = "to"
        '
        'txtMPCMToDate
        '
        Me.txtMPCMToDate.CalculationExpression = Nothing
        Me.txtMPCMToDate.CustomFormat = "dd/MM/yyyy"
        Me.txtMPCMToDate.FieldCode = Nothing
        Me.txtMPCMToDate.FieldDesc = Nothing
        Me.txtMPCMToDate.FieldMaxLength = 0
        Me.txtMPCMToDate.FieldName = Nothing
        Me.txtMPCMToDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMPCMToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtMPCMToDate.isCalculatedField = False
        Me.txtMPCMToDate.IsSourceFromTable = False
        Me.txtMPCMToDate.IsSourceFromValueList = False
        Me.txtMPCMToDate.IsUnique = False
        Me.txtMPCMToDate.Location = New System.Drawing.Point(149, 19)
        Me.txtMPCMToDate.MendatroryField = False
        Me.txtMPCMToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtMPCMToDate.MyLinkLable1 = Nothing
        Me.txtMPCMToDate.MyLinkLable2 = Nothing
        Me.txtMPCMToDate.Name = "txtMPCMToDate"
        Me.txtMPCMToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtMPCMToDate.ReferenceFieldDesc = Nothing
        Me.txtMPCMToDate.ReferenceFieldName = Nothing
        Me.txtMPCMToDate.ReferenceTableName = Nothing
        Me.txtMPCMToDate.Size = New System.Drawing.Size(87, 18)
        Me.txtMPCMToDate.TabIndex = 329
        Me.txtMPCMToDate.TabStop = False
        Me.txtMPCMToDate.Text = "13/06/2011"
        Me.txtMPCMToDate.Value = New Date(2011, 6, 13, 0, 0, 0, 0)
        '
        'cboMPCMShift
        '
        Me.cboMPCMShift.AutoCompleteDisplayMember = Nothing
        Me.cboMPCMShift.AutoCompleteValueMember = Nothing
        Me.cboMPCMShift.DropDownAnimationEnabled = True
        Me.cboMPCMShift.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboMPCMShift.Location = New System.Drawing.Point(239, 18)
        Me.cboMPCMShift.Name = "cboMPCMShift"
        Me.cboMPCMShift.Size = New System.Drawing.Size(60, 20)
        Me.cboMPCMShift.TabIndex = 328
        '
        'MyLabel60
        '
        Me.MyLabel60.FieldName = Nothing
        Me.MyLabel60.Location = New System.Drawing.Point(4, 20)
        Me.MyLabel60.Name = "MyLabel60"
        Me.MyLabel60.Size = New System.Drawing.Size(30, 18)
        Me.MyLabel60.TabIndex = 60
        Me.MyLabel60.Text = "Date"
        '
        'txtMPCMFromDate
        '
        Me.txtMPCMFromDate.CalculationExpression = Nothing
        Me.txtMPCMFromDate.CustomFormat = "dd/MM/yyyy"
        Me.txtMPCMFromDate.FieldCode = Nothing
        Me.txtMPCMFromDate.FieldDesc = Nothing
        Me.txtMPCMFromDate.FieldMaxLength = 0
        Me.txtMPCMFromDate.FieldName = Nothing
        Me.txtMPCMFromDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMPCMFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtMPCMFromDate.isCalculatedField = False
        Me.txtMPCMFromDate.IsSourceFromTable = False
        Me.txtMPCMFromDate.IsSourceFromValueList = False
        Me.txtMPCMFromDate.IsUnique = False
        Me.txtMPCMFromDate.Location = New System.Drawing.Point(40, 19)
        Me.txtMPCMFromDate.MendatroryField = False
        Me.txtMPCMFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtMPCMFromDate.MyLinkLable1 = Nothing
        Me.txtMPCMFromDate.MyLinkLable2 = Nothing
        Me.txtMPCMFromDate.Name = "txtMPCMFromDate"
        Me.txtMPCMFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtMPCMFromDate.ReferenceFieldDesc = Nothing
        Me.txtMPCMFromDate.ReferenceFieldName = Nothing
        Me.txtMPCMFromDate.ReferenceTableName = Nothing
        Me.txtMPCMFromDate.Size = New System.Drawing.Size(87, 18)
        Me.txtMPCMFromDate.TabIndex = 59
        Me.txtMPCMFromDate.TabStop = False
        Me.txtMPCMFromDate.Text = "13/06/2011"
        Me.txtMPCMFromDate.Value = New Date(2011, 6, 13, 0, 0, 0, 0)
        '
        'MyLabel61
        '
        Me.MyLabel61.FieldName = Nothing
        Me.MyLabel61.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel61.Location = New System.Drawing.Point(5, 41)
        Me.MyLabel61.Name = "MyLabel61"
        Me.MyLabel61.Size = New System.Drawing.Size(32, 16)
        Me.MyLabel61.TabIndex = 46
        Me.MyLabel61.Text = "MCC"
        '
        'RadButton290
        '
        Me.RadButton290.Location = New System.Drawing.Point(303, 19)
        Me.RadButton290.Name = "RadButton290"
        Me.RadButton290.Size = New System.Drawing.Size(31, 38)
        Me.RadButton290.TabIndex = 13
        Me.RadButton290.Text = ">>"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.GroupBox76)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(150.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(646, 376)
        Me.RadPageViewPage2.Text = "Delete VLC Collection Data"
        '
        'GroupBox76
        '
        Me.GroupBox76.Controls.Add(Me.chkPreviousShift)
        Me.GroupBox76.Controls.Add(Me.chkDeleteBMCCollection)
        Me.GroupBox76.Controls.Add(Me.TxtMultiSelectFinder8)
        Me.GroupBox76.Controls.Add(Me.MyLabel40)
        Me.GroupBox76.Controls.Add(Me.txtMCCToDate)
        Me.GroupBox76.Controls.Add(Me.txtFromShift)
        Me.GroupBox76.Controls.Add(Me.MyLabel39)
        Me.GroupBox76.Controls.Add(Me.txtMCCFromDate)
        Me.GroupBox76.Controls.Add(Me.MyLabel41)
        Me.GroupBox76.Controls.Add(Me.BulkDelete)
        Me.GroupBox76.Location = New System.Drawing.Point(3, 14)
        Me.GroupBox76.Name = "GroupBox76"
        Me.GroupBox76.Size = New System.Drawing.Size(370, 84)
        Me.GroupBox76.TabIndex = 80
        Me.GroupBox76.TabStop = False
        Me.GroupBox76.Text = "Delete MCC Milk Procurement Shift Collection"
        '
        'chkDeleteBMCCollection
        '
        Me.chkDeleteBMCCollection.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkDeleteBMCCollection.Location = New System.Drawing.Point(40, 60)
        Me.chkDeleteBMCCollection.MyLinkLable1 = Nothing
        Me.chkDeleteBMCCollection.MyLinkLable2 = Nothing
        Me.chkDeleteBMCCollection.Name = "chkDeleteBMCCollection"
        Me.chkDeleteBMCCollection.Size = New System.Drawing.Size(132, 18)
        Me.chkDeleteBMCCollection.TabIndex = 371
        Me.chkDeleteBMCCollection.Tag1 = Nothing
        Me.chkDeleteBMCCollection.Text = "Delete BMC Collection"
        Me.chkDeleteBMCCollection.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'TxtMultiSelectFinder8
        '
        Me.TxtMultiSelectFinder8.arrDispalyMember = Nothing
        Me.TxtMultiSelectFinder8.arrValueMember = Nothing
        Me.TxtMultiSelectFinder8.Location = New System.Drawing.Point(40, 40)
        Me.TxtMultiSelectFinder8.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMultiSelectFinder8.MyLinkLable1 = Nothing
        Me.TxtMultiSelectFinder8.MyLinkLable2 = Nothing
        Me.TxtMultiSelectFinder8.MyNullText = "Please Select..."
        Me.TxtMultiSelectFinder8.Name = "TxtMultiSelectFinder8"
        Me.TxtMultiSelectFinder8.Size = New System.Drawing.Size(279, 19)
        Me.TxtMultiSelectFinder8.TabIndex = 346
        '
        'MyLabel40
        '
        Me.MyLabel40.FieldName = Nothing
        Me.MyLabel40.Location = New System.Drawing.Point(129, 19)
        Me.MyLabel40.Name = "MyLabel40"
        Me.MyLabel40.Size = New System.Drawing.Size(17, 18)
        Me.MyLabel40.TabIndex = 330
        Me.MyLabel40.Text = "to"
        '
        'txtMCCToDate
        '
        Me.txtMCCToDate.CalculationExpression = Nothing
        Me.txtMCCToDate.CustomFormat = "dd/MMM/yyyy"
        Me.txtMCCToDate.FieldCode = Nothing
        Me.txtMCCToDate.FieldDesc = Nothing
        Me.txtMCCToDate.FieldMaxLength = 0
        Me.txtMCCToDate.FieldName = Nothing
        Me.txtMCCToDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMCCToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtMCCToDate.isCalculatedField = False
        Me.txtMCCToDate.IsSourceFromTable = False
        Me.txtMCCToDate.IsSourceFromValueList = False
        Me.txtMCCToDate.IsUnique = False
        Me.txtMCCToDate.Location = New System.Drawing.Point(149, 19)
        Me.txtMCCToDate.MendatroryField = False
        Me.txtMCCToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtMCCToDate.MyLinkLable1 = Nothing
        Me.txtMCCToDate.MyLinkLable2 = Nothing
        Me.txtMCCToDate.Name = "txtMCCToDate"
        Me.txtMCCToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtMCCToDate.ReferenceFieldDesc = Nothing
        Me.txtMCCToDate.ReferenceFieldName = Nothing
        Me.txtMCCToDate.ReferenceTableName = Nothing
        Me.txtMCCToDate.Size = New System.Drawing.Size(87, 18)
        Me.txtMCCToDate.TabIndex = 329
        Me.txtMCCToDate.TabStop = False
        Me.txtMCCToDate.Text = "13/Jun/2011"
        Me.txtMCCToDate.Value = New Date(2011, 6, 13, 0, 0, 0, 0)
        '
        'txtFromShift
        '
        Me.txtFromShift.AutoCompleteDisplayMember = Nothing
        Me.txtFromShift.AutoCompleteValueMember = Nothing
        Me.txtFromShift.DropDownAnimationEnabled = True
        Me.txtFromShift.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.txtFromShift.Location = New System.Drawing.Point(239, 18)
        Me.txtFromShift.Name = "txtFromShift"
        Me.txtFromShift.Size = New System.Drawing.Size(80, 20)
        Me.txtFromShift.TabIndex = 328
        '
        'MyLabel39
        '
        Me.MyLabel39.FieldName = Nothing
        Me.MyLabel39.Location = New System.Drawing.Point(4, 20)
        Me.MyLabel39.Name = "MyLabel39"
        Me.MyLabel39.Size = New System.Drawing.Size(30, 18)
        Me.MyLabel39.TabIndex = 60
        Me.MyLabel39.Text = "Date"
        '
        'txtMCCFromDate
        '
        Me.txtMCCFromDate.CalculationExpression = Nothing
        Me.txtMCCFromDate.CustomFormat = "dd/MMM/yyyy"
        Me.txtMCCFromDate.FieldCode = Nothing
        Me.txtMCCFromDate.FieldDesc = Nothing
        Me.txtMCCFromDate.FieldMaxLength = 0
        Me.txtMCCFromDate.FieldName = Nothing
        Me.txtMCCFromDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMCCFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtMCCFromDate.isCalculatedField = False
        Me.txtMCCFromDate.IsSourceFromTable = False
        Me.txtMCCFromDate.IsSourceFromValueList = False
        Me.txtMCCFromDate.IsUnique = False
        Me.txtMCCFromDate.Location = New System.Drawing.Point(40, 19)
        Me.txtMCCFromDate.MendatroryField = False
        Me.txtMCCFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtMCCFromDate.MyLinkLable1 = Nothing
        Me.txtMCCFromDate.MyLinkLable2 = Nothing
        Me.txtMCCFromDate.Name = "txtMCCFromDate"
        Me.txtMCCFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtMCCFromDate.ReferenceFieldDesc = Nothing
        Me.txtMCCFromDate.ReferenceFieldName = Nothing
        Me.txtMCCFromDate.ReferenceTableName = Nothing
        Me.txtMCCFromDate.Size = New System.Drawing.Size(87, 18)
        Me.txtMCCFromDate.TabIndex = 59
        Me.txtMCCFromDate.TabStop = False
        Me.txtMCCFromDate.Text = "13/Jun/2011"
        Me.txtMCCFromDate.Value = New Date(2011, 6, 13, 0, 0, 0, 0)
        '
        'MyLabel41
        '
        Me.MyLabel41.FieldName = Nothing
        Me.MyLabel41.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel41.Location = New System.Drawing.Point(5, 41)
        Me.MyLabel41.Name = "MyLabel41"
        Me.MyLabel41.Size = New System.Drawing.Size(32, 16)
        Me.MyLabel41.TabIndex = 46
        Me.MyLabel41.Text = "MCC"
        '
        'BulkDelete
        '
        Me.BulkDelete.Location = New System.Drawing.Point(325, 19)
        Me.BulkDelete.Name = "BulkDelete"
        Me.BulkDelete.Size = New System.Drawing.Size(40, 38)
        Me.BulkDelete.TabIndex = 13
        Me.BulkDelete.Text = "Delete"
        '
        'RadPageViewPage4
        '
        Me.RadPageViewPage4.Controls.Add(Me.RadGroupBox7)
        Me.RadPageViewPage4.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage4.Controls.Add(Me.RadGroupBox4)
        Me.RadPageViewPage4.ItemSize = New System.Drawing.SizeF(120.0!, 28.0!)
        Me.RadPageViewPage4.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage4.Name = "RadPageViewPage4"
        Me.RadPageViewPage4.Size = New System.Drawing.Size(646, 376)
        Me.RadPageViewPage4.Text = "BMC Milk Correction"
        '
        'RadGroupBox7
        '
        Me.RadGroupBox7.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox7.Controls.Add(Me.btnExport)
        Me.RadGroupBox7.Controls.Add(Me.btnImport)
        Me.RadGroupBox7.HeaderText = ""
        Me.RadGroupBox7.Location = New System.Drawing.Point(3, 314)
        Me.RadGroupBox7.Name = "RadGroupBox7"
        Me.RadGroupBox7.Size = New System.Drawing.Size(459, 30)
        Me.RadGroupBox7.TabIndex = 14
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExport.Location = New System.Drawing.Point(100, 4)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(91, 21)
        Me.btnExport.TabIndex = 12
        Me.btnExport.Text = "Export"
        Me.btnExport.Visible = False
        '
        'btnImport
        '
        Me.btnImport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnImport.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnImport.Location = New System.Drawing.Point(7, 4)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(88, 21)
        Me.btnImport.TabIndex = 13
        Me.btnImport.Text = "Import"
        Me.btnImport.Visible = False
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.RadButton3)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel12)
        Me.RadGroupBox3.Controls.Add(Me.lblBMCRoute)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel16)
        Me.RadGroupBox3.Controls.Add(Me.txtBMCDate)
        Me.RadGroupBox3.Controls.Add(Me.txtBMCBMC)
        Me.RadGroupBox3.Controls.Add(Me.txtBMCRouteNo)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel14)
        Me.RadGroupBox3.Controls.Add(Me.lblBMCBMC)
        Me.RadGroupBox3.HeaderText = "Filter"
        Me.RadGroupBox3.Location = New System.Drawing.Point(3, 3)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Size = New System.Drawing.Size(566, 86)
        Me.RadGroupBox3.TabIndex = 2
        Me.RadGroupBox3.Text = "Filter"
        '
        'RadButton3
        '
        Me.RadButton3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadButton3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton3.Location = New System.Drawing.Point(495, 61)
        Me.RadButton3.Name = "RadButton3"
        Me.RadButton3.Size = New System.Drawing.Size(66, 21)
        Me.RadButton3.TabIndex = 4
        Me.RadButton3.Text = ">>"
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel12.Location = New System.Drawing.Point(5, 17)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(56, 16)
        Me.MyLabel12.TabIndex = 8
        Me.MyLabel12.Text = "Shift Date"
        '
        'lblBMCRoute
        '
        Me.lblBMCRoute.AutoSize = False
        Me.lblBMCRoute.BorderVisible = True
        Me.lblBMCRoute.FieldName = Nothing
        Me.lblBMCRoute.Location = New System.Drawing.Point(234, 37)
        Me.lblBMCRoute.Name = "lblBMCRoute"
        Me.lblBMCRoute.Size = New System.Drawing.Size(260, 21)
        Me.lblBMCRoute.TabIndex = 11
        '
        'MyLabel16
        '
        Me.MyLabel16.FieldName = Nothing
        Me.MyLabel16.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel16.Location = New System.Drawing.Point(5, 38)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(36, 18)
        Me.MyLabel16.TabIndex = 6
        Me.MyLabel16.Text = "Route"
        '
        'txtBMCDate
        '
        Me.txtBMCDate.CalculationExpression = Nothing
        Me.txtBMCDate.CustomFormat = "dd/MM/yyyy"
        Me.txtBMCDate.FieldCode = Nothing
        Me.txtBMCDate.FieldDesc = Nothing
        Me.txtBMCDate.FieldMaxLength = 0
        Me.txtBMCDate.FieldName = Nothing
        Me.txtBMCDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBMCDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtBMCDate.isCalculatedField = False
        Me.txtBMCDate.IsSourceFromTable = False
        Me.txtBMCDate.IsSourceFromValueList = False
        Me.txtBMCDate.IsUnique = False
        Me.txtBMCDate.Location = New System.Drawing.Point(71, 16)
        Me.txtBMCDate.MendatroryField = True
        Me.txtBMCDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtBMCDate.MyLinkLable1 = Me.MyLabel12
        Me.txtBMCDate.MyLinkLable2 = Nothing
        Me.txtBMCDate.Name = "txtBMCDate"
        Me.txtBMCDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtBMCDate.ReferenceFieldDesc = Nothing
        Me.txtBMCDate.ReferenceFieldName = Nothing
        Me.txtBMCDate.ReferenceTableName = Nothing
        Me.txtBMCDate.Size = New System.Drawing.Size(156, 18)
        Me.txtBMCDate.TabIndex = 0
        Me.txtBMCDate.TabStop = False
        Me.txtBMCDate.Text = "03/05/2011"
        Me.txtBMCDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'txtBMCBMC
        '
        Me.txtBMCBMC.CalculationExpression = Nothing
        Me.txtBMCBMC.FieldCode = Nothing
        Me.txtBMCBMC.FieldDesc = Nothing
        Me.txtBMCBMC.FieldMaxLength = 0
        Me.txtBMCBMC.FieldName = Nothing
        Me.txtBMCBMC.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtBMCBMC.isCalculatedField = False
        Me.txtBMCBMC.IsSourceFromTable = False
        Me.txtBMCBMC.IsSourceFromValueList = False
        Me.txtBMCBMC.IsUnique = False
        Me.txtBMCBMC.Location = New System.Drawing.Point(71, 61)
        Me.txtBMCBMC.MendatroryField = True
        Me.txtBMCBMC.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBMCBMC.MyLinkLable1 = Me.MyLabel14
        Me.txtBMCBMC.MyLinkLable2 = Me.lblBMCBMC
        Me.txtBMCBMC.MyReadOnly = False
        Me.txtBMCBMC.MyShowMasterFormButton = False
        Me.txtBMCBMC.Name = "txtBMCBMC"
        Me.txtBMCBMC.ReferenceFieldDesc = Nothing
        Me.txtBMCBMC.ReferenceFieldName = Nothing
        Me.txtBMCBMC.ReferenceTableName = Nothing
        Me.txtBMCBMC.Size = New System.Drawing.Size(159, 21)
        Me.txtBMCBMC.TabIndex = 2
        Me.txtBMCBMC.Value = ""
        '
        'MyLabel14
        '
        Me.MyLabel14.FieldName = Nothing
        Me.MyLabel14.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel14.Location = New System.Drawing.Point(5, 62)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(30, 18)
        Me.MyLabel14.TabIndex = 7
        Me.MyLabel14.Text = "BMC"
        '
        'lblBMCBMC
        '
        Me.lblBMCBMC.AutoSize = False
        Me.lblBMCBMC.BorderVisible = True
        Me.lblBMCBMC.FieldName = Nothing
        Me.lblBMCBMC.Location = New System.Drawing.Point(234, 61)
        Me.lblBMCBMC.Name = "lblBMCBMC"
        Me.lblBMCBMC.Size = New System.Drawing.Size(260, 21)
        Me.lblBMCBMC.TabIndex = 10
        '
        'txtBMCRouteNo
        '
        Me.txtBMCRouteNo.CalculationExpression = Nothing
        Me.txtBMCRouteNo.FieldCode = Nothing
        Me.txtBMCRouteNo.FieldDesc = Nothing
        Me.txtBMCRouteNo.FieldMaxLength = 0
        Me.txtBMCRouteNo.FieldName = Nothing
        Me.txtBMCRouteNo.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtBMCRouteNo.isCalculatedField = False
        Me.txtBMCRouteNo.IsSourceFromTable = False
        Me.txtBMCRouteNo.IsSourceFromValueList = False
        Me.txtBMCRouteNo.IsUnique = False
        Me.txtBMCRouteNo.Location = New System.Drawing.Point(71, 37)
        Me.txtBMCRouteNo.MendatroryField = True
        Me.txtBMCRouteNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBMCRouteNo.MyLinkLable1 = Me.MyLabel16
        Me.txtBMCRouteNo.MyLinkLable2 = Nothing
        Me.txtBMCRouteNo.MyReadOnly = False
        Me.txtBMCRouteNo.MyShowMasterFormButton = False
        Me.txtBMCRouteNo.Name = "txtBMCRouteNo"
        Me.txtBMCRouteNo.ReferenceFieldDesc = Nothing
        Me.txtBMCRouteNo.ReferenceFieldName = Nothing
        Me.txtBMCRouteNo.ReferenceTableName = Nothing
        Me.txtBMCRouteNo.Size = New System.Drawing.Size(159, 21)
        Me.txtBMCRouteNo.TabIndex = 3
        Me.txtBMCRouteNo.Value = ""
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.txtRetestingCLR)
        Me.RadGroupBox4.Controls.Add(Me.MyLabel22)
        Me.RadGroupBox4.Controls.Add(Me.lblBMCStatus)
        Me.RadGroupBox4.Controls.Add(Me.MyLabel19)
        Me.RadGroupBox4.Controls.Add(Me.lblBMCDetailNo)
        Me.RadGroupBox4.Controls.Add(Me.MyLabel13)
        Me.RadGroupBox4.Controls.Add(Me.MyLabel18)
        Me.RadGroupBox4.Controls.Add(Me.lblBMCSno)
        Me.RadGroupBox4.Controls.Add(Me.MyLabel21)
        Me.RadGroupBox4.Controls.Add(Me.lblBMCDocNo)
        Me.RadGroupBox4.Controls.Add(Me.lblBMCCorrBMC)
        Me.RadGroupBox4.Controls.Add(Me.cboBMCCorrMilkType)
        Me.RadGroupBox4.Controls.Add(Me.MyLabel24)
        Me.RadGroupBox4.Controls.Add(Me.RadButton4)
        Me.RadGroupBox4.Controls.Add(Me.MyLabel25)
        Me.RadGroupBox4.Controls.Add(Me.RadButton5)
        Me.RadGroupBox4.Controls.Add(Me.txtBMCCorrBMC)
        Me.RadGroupBox4.Controls.Add(Me.MyLabel26)
        Me.RadGroupBox4.Controls.Add(Me.txtBMCCorrQty)
        Me.RadGroupBox4.Controls.Add(Me.MyLabel27)
        Me.RadGroupBox4.Controls.Add(Me.txtBMCCorrSNF)
        Me.RadGroupBox4.Controls.Add(Me.txtBMCCorrFAT)
        Me.RadGroupBox4.Controls.Add(Me.MyLabel28)
        Me.RadGroupBox4.HeaderText = "Correction"
        Me.RadGroupBox4.Location = New System.Drawing.Point(3, 91)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Size = New System.Drawing.Size(459, 208)
        Me.RadGroupBox4.TabIndex = 3
        Me.RadGroupBox4.Text = "Correction"
        '
        'txtRetestingCLR
        '
        Me.txtRetestingCLR.BackColor = System.Drawing.Color.White
        Me.txtRetestingCLR.CalculationExpression = Nothing
        Me.txtRetestingCLR.DecimalPlaces = 2
        Me.txtRetestingCLR.FieldCode = Nothing
        Me.txtRetestingCLR.FieldDesc = Nothing
        Me.txtRetestingCLR.FieldMaxLength = 0
        Me.txtRetestingCLR.FieldName = Nothing
        Me.txtRetestingCLR.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtRetestingCLR.isCalculatedField = False
        Me.txtRetestingCLR.IsSourceFromTable = False
        Me.txtRetestingCLR.IsSourceFromValueList = False
        Me.txtRetestingCLR.IsUnique = False
        Me.txtRetestingCLR.Location = New System.Drawing.Point(352, 67)
        Me.txtRetestingCLR.MendatroryField = False
        Me.txtRetestingCLR.MyLinkLable1 = Me.MyLabel22
        Me.txtRetestingCLR.MyLinkLable2 = Nothing
        Me.txtRetestingCLR.Name = "txtRetestingCLR"
        Me.txtRetestingCLR.ReferenceFieldDesc = Nothing
        Me.txtRetestingCLR.ReferenceFieldName = Nothing
        Me.txtRetestingCLR.ReferenceTableName = Nothing
        Me.txtRetestingCLR.Size = New System.Drawing.Size(102, 20)
        Me.txtRetestingCLR.TabIndex = 23
        Me.txtRetestingCLR.Text = "0"
        Me.txtRetestingCLR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtRetestingCLR.Value = 0R
        Me.txtRetestingCLR.Visible = False
        '
        'MyLabel22
        '
        Me.MyLabel22.FieldName = Nothing
        Me.MyLabel22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel22.Location = New System.Drawing.Point(320, 69)
        Me.MyLabel22.Name = "MyLabel22"
        Me.MyLabel22.Size = New System.Drawing.Size(29, 16)
        Me.MyLabel22.TabIndex = 24
        Me.MyLabel22.Text = "CLR"
        Me.MyLabel22.Visible = False
        '
        'lblBMCStatus
        '
        Me.lblBMCStatus.AutoSize = False
        Me.lblBMCStatus.BorderVisible = True
        Me.lblBMCStatus.Enabled = False
        Me.lblBMCStatus.FieldName = Nothing
        Me.lblBMCStatus.Location = New System.Drawing.Point(331, 21)
        Me.lblBMCStatus.Name = "lblBMCStatus"
        Me.lblBMCStatus.Size = New System.Drawing.Size(123, 21)
        Me.lblBMCStatus.TabIndex = 22
        '
        'MyLabel19
        '
        Me.MyLabel19.FieldName = Nothing
        Me.MyLabel19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel19.Location = New System.Drawing.Point(175, 47)
        Me.MyLabel19.Name = "MyLabel19"
        Me.MyLabel19.Size = New System.Drawing.Size(29, 16)
        Me.MyLabel19.TabIndex = 21
        Me.MyLabel19.Text = "DNo"
        '
        'lblBMCDetailNo
        '
        Me.lblBMCDetailNo.AutoSize = False
        Me.lblBMCDetailNo.BorderVisible = True
        Me.lblBMCDetailNo.FieldName = Nothing
        Me.lblBMCDetailNo.Location = New System.Drawing.Point(207, 45)
        Me.lblBMCDetailNo.Name = "lblBMCDetailNo"
        Me.lblBMCDetailNo.Size = New System.Drawing.Size(102, 21)
        Me.lblBMCDetailNo.TabIndex = 20
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel13.Location = New System.Drawing.Point(175, 23)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(28, 16)
        Me.MyLabel13.TabIndex = 19
        Me.MyLabel13.Text = "SNo"
        '
        'MyLabel18
        '
        Me.MyLabel18.FieldName = Nothing
        Me.MyLabel18.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel18.Location = New System.Drawing.Point(5, 145)
        Me.MyLabel18.Name = "MyLabel18"
        Me.MyLabel18.Size = New System.Drawing.Size(63, 18)
        Me.MyLabel18.TabIndex = 18
        Me.MyLabel18.Text = "BMC Name"
        '
        'lblBMCSno
        '
        Me.lblBMCSno.AutoSize = False
        Me.lblBMCSno.BorderVisible = True
        Me.lblBMCSno.FieldName = Nothing
        Me.lblBMCSno.Location = New System.Drawing.Point(207, 21)
        Me.lblBMCSno.Name = "lblBMCSno"
        Me.lblBMCSno.Size = New System.Drawing.Size(102, 21)
        Me.lblBMCSno.TabIndex = 16
        '
        'MyLabel21
        '
        Me.MyLabel21.FieldName = Nothing
        Me.MyLabel21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel21.Location = New System.Drawing.Point(5, 23)
        Me.MyLabel21.Name = "MyLabel21"
        Me.MyLabel21.Size = New System.Drawing.Size(44, 16)
        Me.MyLabel21.TabIndex = 14
        Me.MyLabel21.Text = "Doc No"
        '
        'lblBMCDocNo
        '
        Me.lblBMCDocNo.AutoSize = False
        Me.lblBMCDocNo.BorderVisible = True
        Me.lblBMCDocNo.FieldName = Nothing
        Me.lblBMCDocNo.Location = New System.Drawing.Point(71, 21)
        Me.lblBMCDocNo.Name = "lblBMCDocNo"
        Me.lblBMCDocNo.Size = New System.Drawing.Size(102, 21)
        Me.lblBMCDocNo.TabIndex = 13
        '
        'lblBMCCorrBMC
        '
        Me.lblBMCCorrBMC.AutoSize = False
        Me.lblBMCCorrBMC.BorderVisible = True
        Me.lblBMCCorrBMC.FieldName = Nothing
        Me.lblBMCCorrBMC.Location = New System.Drawing.Point(71, 144)
        Me.lblBMCCorrBMC.Name = "lblBMCCorrBMC"
        Me.lblBMCCorrBMC.Size = New System.Drawing.Size(239, 21)
        Me.lblBMCCorrBMC.TabIndex = 12
        '
        'cboBMCCorrMilkType
        '
        Me.cboBMCCorrMilkType.AutoCompleteDisplayMember = Nothing
        Me.cboBMCCorrMilkType.AutoCompleteValueMember = Nothing
        Me.cboBMCCorrMilkType.CalculationExpression = Nothing
        Me.cboBMCCorrMilkType.DropDownAnimationEnabled = True
        Me.cboBMCCorrMilkType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboBMCCorrMilkType.FieldCode = Nothing
        Me.cboBMCCorrMilkType.FieldDesc = Nothing
        Me.cboBMCCorrMilkType.FieldMaxLength = 0
        Me.cboBMCCorrMilkType.FieldName = Nothing
        Me.cboBMCCorrMilkType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboBMCCorrMilkType.isCalculatedField = False
        Me.cboBMCCorrMilkType.IsSourceFromTable = False
        Me.cboBMCCorrMilkType.IsSourceFromValueList = False
        Me.cboBMCCorrMilkType.IsUnique = False
        RadListDataItem5.Text = "M"
        RadListDataItem6.Text = "E"
        Me.cboBMCCorrMilkType.Items.Add(RadListDataItem5)
        Me.cboBMCCorrMilkType.Items.Add(RadListDataItem6)
        Me.cboBMCCorrMilkType.Location = New System.Drawing.Point(71, 94)
        Me.cboBMCCorrMilkType.MendatroryField = True
        Me.cboBMCCorrMilkType.MyLinkLable1 = Me.MyLabel24
        Me.cboBMCCorrMilkType.MyLinkLable2 = Nothing
        Me.cboBMCCorrMilkType.Name = "cboBMCCorrMilkType"
        Me.cboBMCCorrMilkType.ReferenceFieldDesc = Nothing
        Me.cboBMCCorrMilkType.ReferenceFieldName = Nothing
        Me.cboBMCCorrMilkType.ReferenceTableName = Nothing
        Me.cboBMCCorrMilkType.Size = New System.Drawing.Size(239, 18)
        Me.cboBMCCorrMilkType.TabIndex = 3
        '
        'MyLabel24
        '
        Me.MyLabel24.FieldName = Nothing
        Me.MyLabel24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel24.Location = New System.Drawing.Point(5, 95)
        Me.MyLabel24.Name = "MyLabel24"
        Me.MyLabel24.Size = New System.Drawing.Size(55, 16)
        Me.MyLabel24.TabIndex = 7
        Me.MyLabel24.Text = "Milk Type"
        '
        'RadButton4
        '
        Me.RadButton4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadButton4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton4.Location = New System.Drawing.Point(218, 175)
        Me.RadButton4.Name = "RadButton4"
        Me.RadButton4.Size = New System.Drawing.Size(91, 21)
        Me.RadButton4.TabIndex = 6
        Me.RadButton4.Text = "Reset"
        '
        'MyLabel25
        '
        Me.MyLabel25.FieldName = Nothing
        Me.MyLabel25.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel25.Location = New System.Drawing.Point(5, 118)
        Me.MyLabel25.Name = "MyLabel25"
        Me.MyLabel25.Size = New System.Drawing.Size(59, 18)
        Me.MyLabel25.TabIndex = 6
        Me.MyLabel25.Text = "BMC Code"
        '
        'RadButton5
        '
        Me.RadButton5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadButton5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton5.Location = New System.Drawing.Point(71, 175)
        Me.RadButton5.Name = "RadButton5"
        Me.RadButton5.Size = New System.Drawing.Size(88, 21)
        Me.RadButton5.TabIndex = 5
        Me.RadButton5.Text = "Apply"
        '
        'txtBMCCorrBMC
        '
        Me.txtBMCCorrBMC.CalculationExpression = Nothing
        Me.txtBMCCorrBMC.FieldCode = Nothing
        Me.txtBMCCorrBMC.FieldDesc = Nothing
        Me.txtBMCCorrBMC.FieldMaxLength = 0
        Me.txtBMCCorrBMC.FieldName = Nothing
        Me.txtBMCCorrBMC.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtBMCCorrBMC.isCalculatedField = False
        Me.txtBMCCorrBMC.IsSourceFromTable = False
        Me.txtBMCCorrBMC.IsSourceFromValueList = False
        Me.txtBMCCorrBMC.IsUnique = False
        Me.txtBMCCorrBMC.Location = New System.Drawing.Point(71, 117)
        Me.txtBMCCorrBMC.MendatroryField = True
        Me.txtBMCCorrBMC.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBMCCorrBMC.MyLinkLable1 = Me.MyLabel25
        Me.txtBMCCorrBMC.MyLinkLable2 = Nothing
        Me.txtBMCCorrBMC.MyReadOnly = False
        Me.txtBMCCorrBMC.MyShowMasterFormButton = False
        Me.txtBMCCorrBMC.Name = "txtBMCCorrBMC"
        Me.txtBMCCorrBMC.ReferenceFieldDesc = Nothing
        Me.txtBMCCorrBMC.ReferenceFieldName = Nothing
        Me.txtBMCCorrBMC.ReferenceTableName = Nothing
        Me.txtBMCCorrBMC.Size = New System.Drawing.Size(239, 21)
        Me.txtBMCCorrBMC.TabIndex = 4
        Me.txtBMCCorrBMC.Value = ""
        '
        'MyLabel26
        '
        Me.MyLabel26.FieldName = Nothing
        Me.MyLabel26.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel26.Location = New System.Drawing.Point(5, 47)
        Me.MyLabel26.Name = "MyLabel26"
        Me.MyLabel26.Size = New System.Drawing.Size(24, 16)
        Me.MyLabel26.TabIndex = 10
        Me.MyLabel26.Text = "Qty"
        '
        'txtBMCCorrQty
        '
        Me.txtBMCCorrQty.BackColor = System.Drawing.Color.White
        Me.txtBMCCorrQty.CalculationExpression = Nothing
        Me.txtBMCCorrQty.DecimalPlaces = 3
        Me.txtBMCCorrQty.FieldCode = Nothing
        Me.txtBMCCorrQty.FieldDesc = Nothing
        Me.txtBMCCorrQty.FieldMaxLength = 0
        Me.txtBMCCorrQty.FieldName = Nothing
        Me.txtBMCCorrQty.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtBMCCorrQty.isCalculatedField = False
        Me.txtBMCCorrQty.IsSourceFromTable = False
        Me.txtBMCCorrQty.IsSourceFromValueList = False
        Me.txtBMCCorrQty.IsUnique = False
        Me.txtBMCCorrQty.Location = New System.Drawing.Point(71, 45)
        Me.txtBMCCorrQty.MendatroryField = False
        Me.txtBMCCorrQty.MyLinkLable1 = Me.MyLabel26
        Me.txtBMCCorrQty.MyLinkLable2 = Nothing
        Me.txtBMCCorrQty.Name = "txtBMCCorrQty"
        Me.txtBMCCorrQty.ReadOnly = True
        Me.txtBMCCorrQty.ReferenceFieldDesc = Nothing
        Me.txtBMCCorrQty.ReferenceFieldName = Nothing
        Me.txtBMCCorrQty.ReferenceTableName = Nothing
        Me.txtBMCCorrQty.Size = New System.Drawing.Size(102, 20)
        Me.txtBMCCorrQty.TabIndex = 0
        Me.txtBMCCorrQty.Text = "0"
        Me.txtBMCCorrQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtBMCCorrQty.Value = 0R
        '
        'MyLabel27
        '
        Me.MyLabel27.FieldName = Nothing
        Me.MyLabel27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel27.Location = New System.Drawing.Point(5, 70)
        Me.MyLabel27.Name = "MyLabel27"
        Me.MyLabel27.Size = New System.Drawing.Size(28, 16)
        Me.MyLabel27.TabIndex = 9
        Me.MyLabel27.Text = "FAT"
        '
        'txtBMCCorrSNF
        '
        Me.txtBMCCorrSNF.BackColor = System.Drawing.Color.White
        Me.txtBMCCorrSNF.CalculationExpression = Nothing
        Me.txtBMCCorrSNF.DecimalPlaces = 2
        Me.txtBMCCorrSNF.FieldCode = Nothing
        Me.txtBMCCorrSNF.FieldDesc = Nothing
        Me.txtBMCCorrSNF.FieldMaxLength = 0
        Me.txtBMCCorrSNF.FieldName = Nothing
        Me.txtBMCCorrSNF.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtBMCCorrSNF.isCalculatedField = False
        Me.txtBMCCorrSNF.IsSourceFromTable = False
        Me.txtBMCCorrSNF.IsSourceFromValueList = False
        Me.txtBMCCorrSNF.IsUnique = False
        Me.txtBMCCorrSNF.Location = New System.Drawing.Point(207, 68)
        Me.txtBMCCorrSNF.MendatroryField = False
        Me.txtBMCCorrSNF.MyLinkLable1 = Me.MyLabel28
        Me.txtBMCCorrSNF.MyLinkLable2 = Nothing
        Me.txtBMCCorrSNF.Name = "txtBMCCorrSNF"
        Me.txtBMCCorrSNF.ReferenceFieldDesc = Nothing
        Me.txtBMCCorrSNF.ReferenceFieldName = Nothing
        Me.txtBMCCorrSNF.ReferenceTableName = Nothing
        Me.txtBMCCorrSNF.Size = New System.Drawing.Size(102, 20)
        Me.txtBMCCorrSNF.TabIndex = 2
        Me.txtBMCCorrSNF.Text = "0"
        Me.txtBMCCorrSNF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtBMCCorrSNF.Value = 0R
        '
        'MyLabel28
        '
        Me.MyLabel28.FieldName = Nothing
        Me.MyLabel28.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel28.Location = New System.Drawing.Point(175, 70)
        Me.MyLabel28.Name = "MyLabel28"
        Me.MyLabel28.Size = New System.Drawing.Size(29, 16)
        Me.MyLabel28.TabIndex = 8
        Me.MyLabel28.Text = "SNF"
        '
        'txtBMCCorrFAT
        '
        Me.txtBMCCorrFAT.BackColor = System.Drawing.Color.White
        Me.txtBMCCorrFAT.CalculationExpression = Nothing
        Me.txtBMCCorrFAT.DecimalPlaces = 2
        Me.txtBMCCorrFAT.FieldCode = Nothing
        Me.txtBMCCorrFAT.FieldDesc = Nothing
        Me.txtBMCCorrFAT.FieldMaxLength = 0
        Me.txtBMCCorrFAT.FieldName = Nothing
        Me.txtBMCCorrFAT.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtBMCCorrFAT.isCalculatedField = False
        Me.txtBMCCorrFAT.IsSourceFromTable = False
        Me.txtBMCCorrFAT.IsSourceFromValueList = False
        Me.txtBMCCorrFAT.IsUnique = False
        Me.txtBMCCorrFAT.Location = New System.Drawing.Point(71, 68)
        Me.txtBMCCorrFAT.MendatroryField = False
        Me.txtBMCCorrFAT.MyLinkLable1 = Me.MyLabel27
        Me.txtBMCCorrFAT.MyLinkLable2 = Nothing
        Me.txtBMCCorrFAT.Name = "txtBMCCorrFAT"
        Me.txtBMCCorrFAT.ReferenceFieldDesc = Nothing
        Me.txtBMCCorrFAT.ReferenceFieldName = Nothing
        Me.txtBMCCorrFAT.ReferenceTableName = Nothing
        Me.txtBMCCorrFAT.Size = New System.Drawing.Size(102, 20)
        Me.txtBMCCorrFAT.TabIndex = 1
        Me.txtBMCCorrFAT.Text = "0"
        Me.txtBMCCorrFAT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtBMCCorrFAT.Value = 0R
        '
        'RadPageViewPage6
        '
        Me.RadPageViewPage6.Controls.Add(Me.RadGroupBox8)
        Me.RadPageViewPage6.Controls.Add(Me.RadGroupBox5)
        Me.RadPageViewPage6.Controls.Add(Me.RadGroupBox6)
        Me.RadPageViewPage6.ItemSize = New System.Drawing.SizeF(156.0!, 28.0!)
        Me.RadPageViewPage6.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage6.Name = "RadPageViewPage6"
        Me.RadPageViewPage6.Size = New System.Drawing.Size(646, 376)
        Me.RadPageViewPage6.Text = "BMC Tanker Milk Correction "
        '
        'RadGroupBox8
        '
        Me.RadGroupBox8.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox8.Controls.Add(Me.btnTankerMilkImport)
        Me.RadGroupBox8.Controls.Add(Me.btnTankerMilkExport)
        Me.RadGroupBox8.HeaderText = ""
        Me.RadGroupBox8.Location = New System.Drawing.Point(3, 239)
        Me.RadGroupBox8.Name = "RadGroupBox8"
        Me.RadGroupBox8.Size = New System.Drawing.Size(320, 33)
        Me.RadGroupBox8.TabIndex = 24
        '
        'btnTankerMilkImport
        '
        Me.btnTankerMilkImport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTankerMilkImport.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTankerMilkImport.Location = New System.Drawing.Point(6, 7)
        Me.btnTankerMilkImport.Name = "btnTankerMilkImport"
        Me.btnTankerMilkImport.Size = New System.Drawing.Size(88, 21)
        Me.btnTankerMilkImport.TabIndex = 23
        Me.btnTankerMilkImport.Text = "Import"
        Me.btnTankerMilkImport.Visible = False
        '
        'btnTankerMilkExport
        '
        Me.btnTankerMilkExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTankerMilkExport.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTankerMilkExport.Location = New System.Drawing.Point(100, 7)
        Me.btnTankerMilkExport.Name = "btnTankerMilkExport"
        Me.btnTankerMilkExport.Size = New System.Drawing.Size(91, 21)
        Me.btnTankerMilkExport.TabIndex = 22
        Me.btnTankerMilkExport.Text = "Export"
        Me.btnTankerMilkExport.Visible = False
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.RadButton6)
        Me.RadGroupBox5.Controls.Add(Me.MyLabel15)
        Me.RadGroupBox5.Controls.Add(Me.lblBMCTankerRoute)
        Me.RadGroupBox5.Controls.Add(Me.MyLabel20)
        Me.RadGroupBox5.Controls.Add(Me.txtBMCTankerDate)
        Me.RadGroupBox5.Controls.Add(Me.txtBMCTankerRoute)
        Me.RadGroupBox5.HeaderText = "Filter"
        Me.RadGroupBox5.Location = New System.Drawing.Point(3, 3)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Size = New System.Drawing.Size(566, 64)
        Me.RadGroupBox5.TabIndex = 4
        Me.RadGroupBox5.Text = "Filter"
        '
        'RadButton6
        '
        Me.RadButton6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadButton6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton6.Location = New System.Drawing.Point(495, 37)
        Me.RadButton6.Name = "RadButton6"
        Me.RadButton6.Size = New System.Drawing.Size(66, 21)
        Me.RadButton6.TabIndex = 4
        Me.RadButton6.Text = ">>"
        '
        'MyLabel15
        '
        Me.MyLabel15.FieldName = Nothing
        Me.MyLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel15.Location = New System.Drawing.Point(5, 17)
        Me.MyLabel15.Name = "MyLabel15"
        Me.MyLabel15.Size = New System.Drawing.Size(56, 16)
        Me.MyLabel15.TabIndex = 8
        Me.MyLabel15.Text = "Shift Date"
        '
        'lblBMCTankerRoute
        '
        Me.lblBMCTankerRoute.AutoSize = False
        Me.lblBMCTankerRoute.BorderVisible = True
        Me.lblBMCTankerRoute.FieldName = Nothing
        Me.lblBMCTankerRoute.Location = New System.Drawing.Point(234, 37)
        Me.lblBMCTankerRoute.Name = "lblBMCTankerRoute"
        Me.lblBMCTankerRoute.Size = New System.Drawing.Size(260, 21)
        Me.lblBMCTankerRoute.TabIndex = 11
        '
        'MyLabel20
        '
        Me.MyLabel20.FieldName = Nothing
        Me.MyLabel20.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel20.Location = New System.Drawing.Point(5, 38)
        Me.MyLabel20.Name = "MyLabel20"
        Me.MyLabel20.Size = New System.Drawing.Size(36, 18)
        Me.MyLabel20.TabIndex = 6
        Me.MyLabel20.Text = "Route"
        '
        'txtBMCTankerDate
        '
        Me.txtBMCTankerDate.CalculationExpression = Nothing
        Me.txtBMCTankerDate.CustomFormat = "dd/MM/yyyy"
        Me.txtBMCTankerDate.FieldCode = Nothing
        Me.txtBMCTankerDate.FieldDesc = Nothing
        Me.txtBMCTankerDate.FieldMaxLength = 0
        Me.txtBMCTankerDate.FieldName = Nothing
        Me.txtBMCTankerDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBMCTankerDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtBMCTankerDate.isCalculatedField = False
        Me.txtBMCTankerDate.IsSourceFromTable = False
        Me.txtBMCTankerDate.IsSourceFromValueList = False
        Me.txtBMCTankerDate.IsUnique = False
        Me.txtBMCTankerDate.Location = New System.Drawing.Point(71, 16)
        Me.txtBMCTankerDate.MendatroryField = True
        Me.txtBMCTankerDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtBMCTankerDate.MyLinkLable1 = Me.MyLabel15
        Me.txtBMCTankerDate.MyLinkLable2 = Nothing
        Me.txtBMCTankerDate.Name = "txtBMCTankerDate"
        Me.txtBMCTankerDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtBMCTankerDate.ReferenceFieldDesc = Nothing
        Me.txtBMCTankerDate.ReferenceFieldName = Nothing
        Me.txtBMCTankerDate.ReferenceTableName = Nothing
        Me.txtBMCTankerDate.Size = New System.Drawing.Size(156, 18)
        Me.txtBMCTankerDate.TabIndex = 0
        Me.txtBMCTankerDate.TabStop = False
        Me.txtBMCTankerDate.Text = "03/05/2011"
        Me.txtBMCTankerDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'txtBMCTankerRoute
        '
        Me.txtBMCTankerRoute.CalculationExpression = Nothing
        Me.txtBMCTankerRoute.FieldCode = Nothing
        Me.txtBMCTankerRoute.FieldDesc = Nothing
        Me.txtBMCTankerRoute.FieldMaxLength = 0
        Me.txtBMCTankerRoute.FieldName = Nothing
        Me.txtBMCTankerRoute.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtBMCTankerRoute.isCalculatedField = False
        Me.txtBMCTankerRoute.IsSourceFromTable = False
        Me.txtBMCTankerRoute.IsSourceFromValueList = False
        Me.txtBMCTankerRoute.IsUnique = False
        Me.txtBMCTankerRoute.Location = New System.Drawing.Point(71, 37)
        Me.txtBMCTankerRoute.MendatroryField = True
        Me.txtBMCTankerRoute.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBMCTankerRoute.MyLinkLable1 = Me.MyLabel20
        Me.txtBMCTankerRoute.MyLinkLable2 = Nothing
        Me.txtBMCTankerRoute.MyReadOnly = False
        Me.txtBMCTankerRoute.MyShowMasterFormButton = False
        Me.txtBMCTankerRoute.Name = "txtBMCTankerRoute"
        Me.txtBMCTankerRoute.ReferenceFieldDesc = Nothing
        Me.txtBMCTankerRoute.ReferenceFieldName = Nothing
        Me.txtBMCTankerRoute.ReferenceTableName = Nothing
        Me.txtBMCTankerRoute.Size = New System.Drawing.Size(159, 21)
        Me.txtBMCTankerRoute.TabIndex = 3
        Me.txtBMCTankerRoute.Value = ""
        '
        'RadGroupBox6
        '
        Me.RadGroupBox6.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox6.Controls.Add(Me.MyLabel23)
        Me.RadGroupBox6.Controls.Add(Me.MyLabel17)
        Me.RadGroupBox6.Controls.Add(Me.lblBMCTankerSNFKG)
        Me.RadGroupBox6.Controls.Add(Me.MyLabel32)
        Me.RadGroupBox6.Controls.Add(Me.lblBMCTankerFATKG)
        Me.RadGroupBox6.Controls.Add(Me.lblBMCTankerTripNo)
        Me.RadGroupBox6.Controls.Add(Me.MyLabel35)
        Me.RadGroupBox6.Controls.Add(Me.lblBMCTankerDocNo)
        Me.RadGroupBox6.Controls.Add(Me.RadButton7)
        Me.RadGroupBox6.Controls.Add(Me.RadButton8)
        Me.RadGroupBox6.Controls.Add(Me.MyLabel43)
        Me.RadGroupBox6.Controls.Add(Me.txtBMCTankerQty)
        Me.RadGroupBox6.Controls.Add(Me.MyLabel44)
        Me.RadGroupBox6.Controls.Add(Me.txtBMCTankerSNF)
        Me.RadGroupBox6.Controls.Add(Me.txtBMCTankerFAT)
        Me.RadGroupBox6.Controls.Add(Me.MyLabel45)
        Me.RadGroupBox6.HeaderText = "Correction"
        Me.RadGroupBox6.Location = New System.Drawing.Point(3, 73)
        Me.RadGroupBox6.Name = "RadGroupBox6"
        Me.RadGroupBox6.Size = New System.Drawing.Size(320, 148)
        Me.RadGroupBox6.TabIndex = 5
        Me.RadGroupBox6.Text = "Correction"
        '
        'MyLabel23
        '
        Me.MyLabel23.FieldName = Nothing
        Me.MyLabel23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel23.Location = New System.Drawing.Point(175, 92)
        Me.MyLabel23.Name = "MyLabel23"
        Me.MyLabel23.Size = New System.Drawing.Size(48, 16)
        Me.MyLabel23.TabIndex = 21
        Me.MyLabel23.Text = "SNF KG"
        '
        'MyLabel17
        '
        Me.MyLabel17.FieldName = Nothing
        Me.MyLabel17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel17.Location = New System.Drawing.Point(175, 69)
        Me.MyLabel17.Name = "MyLabel17"
        Me.MyLabel17.Size = New System.Drawing.Size(45, 16)
        Me.MyLabel17.TabIndex = 21
        Me.MyLabel17.Text = "FAT Kg"
        '
        'lblBMCTankerSNFKG
        '
        Me.lblBMCTankerSNFKG.AutoSize = False
        Me.lblBMCTankerSNFKG.BorderVisible = True
        Me.lblBMCTankerSNFKG.FieldName = Nothing
        Me.lblBMCTankerSNFKG.Location = New System.Drawing.Point(224, 90)
        Me.lblBMCTankerSNFKG.Name = "lblBMCTankerSNFKG"
        Me.lblBMCTankerSNFKG.Size = New System.Drawing.Size(85, 21)
        Me.lblBMCTankerSNFKG.TabIndex = 20
        Me.lblBMCTankerSNFKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel32
        '
        Me.MyLabel32.FieldName = Nothing
        Me.MyLabel32.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel32.Location = New System.Drawing.Point(175, 47)
        Me.MyLabel32.Name = "MyLabel32"
        Me.MyLabel32.Size = New System.Drawing.Size(43, 16)
        Me.MyLabel32.TabIndex = 19
        Me.MyLabel32.Text = "Trip No"
        '
        'lblBMCTankerFATKG
        '
        Me.lblBMCTankerFATKG.AutoSize = False
        Me.lblBMCTankerFATKG.BorderVisible = True
        Me.lblBMCTankerFATKG.FieldName = Nothing
        Me.lblBMCTankerFATKG.Location = New System.Drawing.Point(224, 67)
        Me.lblBMCTankerFATKG.Name = "lblBMCTankerFATKG"
        Me.lblBMCTankerFATKG.Size = New System.Drawing.Size(85, 21)
        Me.lblBMCTankerFATKG.TabIndex = 20
        Me.lblBMCTankerFATKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblBMCTankerTripNo
        '
        Me.lblBMCTankerTripNo.AutoSize = False
        Me.lblBMCTankerTripNo.BorderVisible = True
        Me.lblBMCTankerTripNo.FieldName = Nothing
        Me.lblBMCTankerTripNo.Location = New System.Drawing.Point(224, 45)
        Me.lblBMCTankerTripNo.Name = "lblBMCTankerTripNo"
        Me.lblBMCTankerTripNo.Size = New System.Drawing.Size(85, 21)
        Me.lblBMCTankerTripNo.TabIndex = 16
        Me.lblBMCTankerTripNo.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        '
        'MyLabel35
        '
        Me.MyLabel35.FieldName = Nothing
        Me.MyLabel35.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel35.Location = New System.Drawing.Point(5, 23)
        Me.MyLabel35.Name = "MyLabel35"
        Me.MyLabel35.Size = New System.Drawing.Size(44, 16)
        Me.MyLabel35.TabIndex = 14
        Me.MyLabel35.Text = "Doc No"
        '
        'lblBMCTankerDocNo
        '
        Me.lblBMCTankerDocNo.AutoSize = False
        Me.lblBMCTankerDocNo.BorderVisible = True
        Me.lblBMCTankerDocNo.FieldName = Nothing
        Me.lblBMCTankerDocNo.Location = New System.Drawing.Point(71, 21)
        Me.lblBMCTankerDocNo.Name = "lblBMCTankerDocNo"
        Me.lblBMCTankerDocNo.Size = New System.Drawing.Size(238, 21)
        Me.lblBMCTankerDocNo.TabIndex = 13
        '
        'RadButton7
        '
        Me.RadButton7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadButton7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton7.Location = New System.Drawing.Point(166, 119)
        Me.RadButton7.Name = "RadButton7"
        Me.RadButton7.Size = New System.Drawing.Size(91, 21)
        Me.RadButton7.TabIndex = 6
        Me.RadButton7.Text = "Reset"
        '
        'RadButton8
        '
        Me.RadButton8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadButton8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton8.Location = New System.Drawing.Point(74, 119)
        Me.RadButton8.Name = "RadButton8"
        Me.RadButton8.Size = New System.Drawing.Size(88, 21)
        Me.RadButton8.TabIndex = 5
        Me.RadButton8.Text = "Apply"
        '
        'MyLabel43
        '
        Me.MyLabel43.FieldName = Nothing
        Me.MyLabel43.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel43.Location = New System.Drawing.Point(5, 47)
        Me.MyLabel43.Name = "MyLabel43"
        Me.MyLabel43.Size = New System.Drawing.Size(24, 16)
        Me.MyLabel43.TabIndex = 10
        Me.MyLabel43.Text = "Qty"
        '
        'txtBMCTankerQty
        '
        Me.txtBMCTankerQty.BackColor = System.Drawing.Color.White
        Me.txtBMCTankerQty.CalculationExpression = Nothing
        Me.txtBMCTankerQty.DecimalPlaces = 3
        Me.txtBMCTankerQty.FieldCode = Nothing
        Me.txtBMCTankerQty.FieldDesc = Nothing
        Me.txtBMCTankerQty.FieldMaxLength = 0
        Me.txtBMCTankerQty.FieldName = Nothing
        Me.txtBMCTankerQty.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtBMCTankerQty.isCalculatedField = False
        Me.txtBMCTankerQty.IsSourceFromTable = False
        Me.txtBMCTankerQty.IsSourceFromValueList = False
        Me.txtBMCTankerQty.IsUnique = False
        Me.txtBMCTankerQty.Location = New System.Drawing.Point(71, 45)
        Me.txtBMCTankerQty.MendatroryField = False
        Me.txtBMCTankerQty.MyLinkLable1 = Me.MyLabel43
        Me.txtBMCTankerQty.MyLinkLable2 = Nothing
        Me.txtBMCTankerQty.Name = "txtBMCTankerQty"
        Me.txtBMCTankerQty.ReadOnly = True
        Me.txtBMCTankerQty.ReferenceFieldDesc = Nothing
        Me.txtBMCTankerQty.ReferenceFieldName = Nothing
        Me.txtBMCTankerQty.ReferenceTableName = Nothing
        Me.txtBMCTankerQty.Size = New System.Drawing.Size(102, 20)
        Me.txtBMCTankerQty.TabIndex = 0
        Me.txtBMCTankerQty.Text = "0"
        Me.txtBMCTankerQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtBMCTankerQty.Value = 0R
        '
        'MyLabel44
        '
        Me.MyLabel44.FieldName = Nothing
        Me.MyLabel44.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel44.Location = New System.Drawing.Point(5, 70)
        Me.MyLabel44.Name = "MyLabel44"
        Me.MyLabel44.Size = New System.Drawing.Size(28, 16)
        Me.MyLabel44.TabIndex = 9
        Me.MyLabel44.Text = "FAT"
        '
        'txtBMCTankerSNF
        '
        Me.txtBMCTankerSNF.BackColor = System.Drawing.Color.White
        Me.txtBMCTankerSNF.CalculationExpression = Nothing
        Me.txtBMCTankerSNF.DecimalPlaces = 2
        Me.txtBMCTankerSNF.FieldCode = Nothing
        Me.txtBMCTankerSNF.FieldDesc = Nothing
        Me.txtBMCTankerSNF.FieldMaxLength = 0
        Me.txtBMCTankerSNF.FieldName = Nothing
        Me.txtBMCTankerSNF.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtBMCTankerSNF.isCalculatedField = False
        Me.txtBMCTankerSNF.IsSourceFromTable = False
        Me.txtBMCTankerSNF.IsSourceFromValueList = False
        Me.txtBMCTankerSNF.IsUnique = False
        Me.txtBMCTankerSNF.Location = New System.Drawing.Point(71, 90)
        Me.txtBMCTankerSNF.MendatroryField = False
        Me.txtBMCTankerSNF.MyLinkLable1 = Me.MyLabel45
        Me.txtBMCTankerSNF.MyLinkLable2 = Nothing
        Me.txtBMCTankerSNF.Name = "txtBMCTankerSNF"
        Me.txtBMCTankerSNF.ReferenceFieldDesc = Nothing
        Me.txtBMCTankerSNF.ReferenceFieldName = Nothing
        Me.txtBMCTankerSNF.ReferenceTableName = Nothing
        Me.txtBMCTankerSNF.Size = New System.Drawing.Size(102, 20)
        Me.txtBMCTankerSNF.TabIndex = 2
        Me.txtBMCTankerSNF.Text = "0"
        Me.txtBMCTankerSNF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtBMCTankerSNF.Value = 0R
        '
        'MyLabel45
        '
        Me.MyLabel45.FieldName = Nothing
        Me.MyLabel45.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel45.Location = New System.Drawing.Point(5, 92)
        Me.MyLabel45.Name = "MyLabel45"
        Me.MyLabel45.Size = New System.Drawing.Size(29, 16)
        Me.MyLabel45.TabIndex = 8
        Me.MyLabel45.Text = "SNF"
        '
        'txtBMCTankerFAT
        '
        Me.txtBMCTankerFAT.BackColor = System.Drawing.Color.White
        Me.txtBMCTankerFAT.CalculationExpression = Nothing
        Me.txtBMCTankerFAT.DecimalPlaces = 2
        Me.txtBMCTankerFAT.FieldCode = Nothing
        Me.txtBMCTankerFAT.FieldDesc = Nothing
        Me.txtBMCTankerFAT.FieldMaxLength = 0
        Me.txtBMCTankerFAT.FieldName = Nothing
        Me.txtBMCTankerFAT.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtBMCTankerFAT.isCalculatedField = False
        Me.txtBMCTankerFAT.IsSourceFromTable = False
        Me.txtBMCTankerFAT.IsSourceFromValueList = False
        Me.txtBMCTankerFAT.IsUnique = False
        Me.txtBMCTankerFAT.Location = New System.Drawing.Point(71, 68)
        Me.txtBMCTankerFAT.MendatroryField = False
        Me.txtBMCTankerFAT.MyLinkLable1 = Me.MyLabel44
        Me.txtBMCTankerFAT.MyLinkLable2 = Nothing
        Me.txtBMCTankerFAT.Name = "txtBMCTankerFAT"
        Me.txtBMCTankerFAT.ReferenceFieldDesc = Nothing
        Me.txtBMCTankerFAT.ReferenceFieldName = Nothing
        Me.txtBMCTankerFAT.ReferenceTableName = Nothing
        Me.txtBMCTankerFAT.Size = New System.Drawing.Size(102, 20)
        Me.txtBMCTankerFAT.TabIndex = 1
        Me.txtBMCTankerFAT.Text = "0"
        Me.txtBMCTankerFAT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtBMCTankerFAT.Value = 0R
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(596, 7)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 21)
        Me.btnclose.TabIndex = 0
        Me.btnclose.Text = "Close"
        '
        'chkPreviousShift
        '
        Me.chkPreviousShift.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkPreviousShift.Location = New System.Drawing.Point(178, 60)
        Me.chkPreviousShift.MyLinkLable1 = Nothing
        Me.chkPreviousShift.MyLinkLable2 = Nothing
        Me.chkPreviousShift.Name = "chkPreviousShift"
        Me.chkPreviousShift.Size = New System.Drawing.Size(138, 18)
        Me.chkPreviousShift.TabIndex = 372
        Me.chkPreviousShift.Tag1 = Nothing
        Me.chkPreviousShift.Text = "Check for previous shift"
        Me.chkPreviousShift.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'frmCorrection
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(667, 463)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmCorrection"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Milk Gate Entry In"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.chkAddMissingSample, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboShift, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBOMStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtShiftDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMCCCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMcc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRouteCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVLC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.chkCorrection, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRetesting, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblUOM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSRNNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboMilkType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblManualFAT_Per, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSNF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblManualSNF_Per, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFAT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage3.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.chkAdjustOwnBMCFATSNF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVLCCMToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboVLCCMShift, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVLCCMFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage5.ResumeLayout(False)
        Me.GroupBox91.ResumeLayout(False)
        Me.GroupBox91.PerformLayout()
        CType(Me.MyLabel59, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMPCMToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboMPCMShift, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel60, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMPCMFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel61, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton290, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.GroupBox76.ResumeLayout(False)
        Me.GroupBox76.PerformLayout()
        CType(Me.chkDeleteBMCCollection, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel40, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMCCToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromShift, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel39, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMCCFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel41, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BulkDelete, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage4.ResumeLayout(False)
        CType(Me.RadGroupBox7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox7.ResumeLayout(False)
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnImport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.RadButton3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBMCRoute, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBMCDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBMCBMC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.RadGroupBox4.PerformLayout()
        CType(Me.txtRetestingCLR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBMCStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBMCDetailNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBMCSno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBMCDocNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBMCCorrBMC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboBMCCorrMilkType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel26, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBMCCorrQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel27, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBMCCorrSNF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel28, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBMCCorrFAT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage6.ResumeLayout(False)
        CType(Me.RadGroupBox8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox8.ResumeLayout(False)
        CType(Me.btnTankerMilkImport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnTankerMilkExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.RadGroupBox5.PerformLayout()
        CType(Me.RadButton6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBMCTankerRoute, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBMCTankerDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox6.ResumeLayout(False)
        Me.RadGroupBox6.PerformLayout()
        CType(Me.MyLabel23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBMCTankerSNFKG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel32, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBMCTankerFATKG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBMCTankerTripNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel35, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBMCTankerDocNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel43, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBMCTankerQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel44, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBMCTankerSNF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel45, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBMCTankerFAT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPreviousShift, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblBOMStatus As common.Controls.MyLabel
    Friend WithEvents cboShift As common.Controls.MyComboBox
    Friend WithEvents lblMcc As common.Controls.MyLabel
    Friend WithEvents lblMCCCode As common.Controls.MyLabel
    Friend WithEvents txtMCC As common.UserControls.txtFinder
    Friend WithEvents lblRouteCode As common.Controls.MyLabel
    Friend WithEvents txtVLC As common.UserControls.txtFinder
    Friend WithEvents lblVLC As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtShiftDate As common.Controls.MyDateTimePicker
    Friend WithEvents LblManualFAT_Per As common.Controls.MyLabel
    Friend WithEvents txtFAT As common.MyNumBox
    Friend WithEvents LblManualSNF_Per As common.Controls.MyLabel
    Friend WithEvents txtSNF As common.MyNumBox
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtQty As common.MyNumBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents TxtFinder1 As common.UserControls.txtFinder
    Friend WithEvents cboMilkType As common.Controls.MyComboBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents lblSRNNo As common.Controls.MyLabel
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents lblUOM As common.Controls.MyLabel
    Friend WithEvents chkAddMissingSample As common.Controls.MyCheckBox
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents RadPageViewPage3 As RadPageViewPage
    Friend WithEvents RadPageViewPage5 As RadPageViewPage
    Friend WithEvents GroupBox91 As GroupBox
    Friend WithEvents txtMPCMMCC As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel59 As common.Controls.MyLabel
    Friend WithEvents txtMPCMToDate As common.Controls.MyDateTimePicker
    Friend WithEvents cboMPCMShift As RadDropDownList
    Friend WithEvents MyLabel60 As common.Controls.MyLabel
    Friend WithEvents txtMPCMFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel61 As common.Controls.MyLabel
    Friend WithEvents RadButton290 As RadButton
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents txtVLCCMMCC As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents txtVLCCMToDate As common.Controls.MyDateTimePicker
    Friend WithEvents cboVLCCMShift As RadDropDownList
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents txtVLCCMFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents RadButton2 As RadButton
    Friend WithEvents RadPageViewPage2 As RadPageViewPage
    Friend WithEvents GroupBox76 As GroupBox
    Friend WithEvents TxtMultiSelectFinder8 As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel40 As common.Controls.MyLabel
    Friend WithEvents txtMCCToDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtFromShift As RadDropDownList
    Friend WithEvents MyLabel39 As common.Controls.MyLabel
    Friend WithEvents txtMCCFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel41 As common.Controls.MyLabel
    Friend WithEvents BulkDelete As RadButton
    Friend WithEvents RadPageViewPage4 As RadPageViewPage
    Friend WithEvents RadGroupBox3 As RadGroupBox
    Friend WithEvents RadButton3 As RadButton
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents lblBMCRoute As common.Controls.MyLabel
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents txtBMCDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtBMCBMC As common.UserControls.txtFinder
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents lblBMCBMC As common.Controls.MyLabel
    Friend WithEvents txtBMCRouteNo As common.UserControls.txtFinder
    Friend WithEvents RadGroupBox4 As RadGroupBox
    Friend WithEvents MyLabel19 As common.Controls.MyLabel
    Friend WithEvents lblBMCDetailNo As common.Controls.MyLabel
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents MyLabel18 As common.Controls.MyLabel
    Friend WithEvents lblBMCSno As common.Controls.MyLabel
    Friend WithEvents MyLabel21 As common.Controls.MyLabel
    Friend WithEvents lblBMCDocNo As common.Controls.MyLabel
    Friend WithEvents lblBMCCorrBMC As common.Controls.MyLabel
    Friend WithEvents cboBMCCorrMilkType As common.Controls.MyComboBox
    Friend WithEvents MyLabel24 As common.Controls.MyLabel
    Friend WithEvents RadButton4 As RadButton
    Friend WithEvents MyLabel25 As common.Controls.MyLabel
    Friend WithEvents RadButton5 As RadButton
    Friend WithEvents txtBMCCorrBMC As common.UserControls.txtFinder
    Friend WithEvents MyLabel26 As common.Controls.MyLabel
    Friend WithEvents txtBMCCorrQty As common.MyNumBox
    Friend WithEvents MyLabel27 As common.Controls.MyLabel
    Friend WithEvents txtBMCCorrSNF As common.MyNumBox
    Friend WithEvents MyLabel28 As common.Controls.MyLabel
    Friend WithEvents txtBMCCorrFAT As common.MyNumBox
    Friend WithEvents chkAdjustOwnBMCFATSNF As common.Controls.MyCheckBox
    Friend WithEvents chkDeleteBMCCollection As common.Controls.MyCheckBox
    Friend WithEvents lblBMCStatus As common.Controls.MyLabel
    Friend WithEvents RadPageViewPage6 As RadPageViewPage
    Friend WithEvents RadGroupBox5 As RadGroupBox
    Friend WithEvents RadButton6 As RadButton
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
    Friend WithEvents lblBMCTankerRoute As common.Controls.MyLabel
    Friend WithEvents MyLabel20 As common.Controls.MyLabel
    Friend WithEvents txtBMCTankerDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtBMCTankerRoute As common.UserControls.txtFinder
    Friend WithEvents RadGroupBox6 As RadGroupBox
    Friend WithEvents MyLabel32 As common.Controls.MyLabel
    Friend WithEvents lblBMCTankerTripNo As common.Controls.MyLabel
    Friend WithEvents MyLabel35 As common.Controls.MyLabel
    Friend WithEvents lblBMCTankerDocNo As common.Controls.MyLabel
    Friend WithEvents RadButton7 As RadButton
    Friend WithEvents RadButton8 As RadButton
    Friend WithEvents MyLabel43 As common.Controls.MyLabel
    Friend WithEvents txtBMCTankerQty As common.MyNumBox
    Friend WithEvents MyLabel44 As common.Controls.MyLabel
    Friend WithEvents txtBMCTankerSNF As common.MyNumBox
    Friend WithEvents MyLabel45 As common.Controls.MyLabel
    Friend WithEvents txtBMCTankerFAT As common.MyNumBox
    Friend WithEvents MyLabel23 As common.Controls.MyLabel
    Friend WithEvents MyLabel17 As common.Controls.MyLabel
    Friend WithEvents lblBMCTankerSNFKG As common.Controls.MyLabel
    Friend WithEvents lblBMCTankerFATKG As common.Controls.MyLabel
    Friend WithEvents chkRetesting As common.Controls.MyCheckBox
    Friend WithEvents chkCorrection As common.Controls.MyCheckBox
    Friend WithEvents txtRetestingCLR As common.MyNumBox
    Friend WithEvents MyLabel22 As common.Controls.MyLabel
    Friend WithEvents btnExport As RadButton
    Friend WithEvents btnImport As RadButton
    Friend WithEvents btnTankerMilkImport As RadButton
    Friend WithEvents btnTankerMilkExport As RadButton
    Friend WithEvents RadGroupBox7 As RadGroupBox
    Friend WithEvents RadGroupBox8 As RadGroupBox
    Friend WithEvents chkPreviousShift As common.Controls.MyCheckBox
End Class
