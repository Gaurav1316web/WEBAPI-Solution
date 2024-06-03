Imports XpertERPEngine
Imports XpertERPEngineFine
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmBullMasters
    'Inherits System.Windows.Forms.Form
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBullMasters))
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.lblBullSourcePrintStrew = New common.Controls.MyLabel()
        Me.lblDamLocation = New common.Controls.MyLabel()
        Me.lblBullRating = New common.Controls.MyLabel()
        Me.lblRemark = New common.Controls.MyLabel()
        Me.lblBullFRID = New common.Controls.MyLabel()
        Me.lblExistDate = New common.Controls.MyLabel()
        Me.lblStatusChangeDate = New common.Controls.MyLabel()
        Me.lblPenId = New common.Controls.MyLabel()
        Me.lblShedId = New common.Controls.MyLabel()
        Me.lblSSCentre = New common.Controls.MyLabel()
        Me.lblBullBookValue = New common.Controls.MyLabel()
        Me.lblExoticBullPercentage = New common.Controls.MyLabel()
        Me.lblBullSubStatus = New common.Controls.MyLabel()
        Me.lblBullAliasName = New common.Controls.MyLabel()
        Me.lblBullStatus = New common.Controls.MyLabel()
        Me.lblSSBullId = New common.Controls.MyLabel()
        Me.lblDOB = New common.Controls.MyLabel()
        Me.lblPrevBullId = New common.Controls.MyLabel()
        Me.lblBreed = New common.Controls.MyLabel()
        Me.lbl12DigitBullId = New common.Controls.MyLabel()
        Me.lblSubCategory = New common.Controls.MyLabel()
        Me.lblCountryCode = New common.Controls.MyLabel()
        Me.lblCategory = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.lblSpecies = New common.Controls.MyLabel()
        Me.lblSemen = New common.Controls.MyLabel()
        Me.lblbankcode = New common.Controls.MyLabel()
        Me.MyLabel15 = New common.Controls.MyLabel()
        Me.MyLabel32 = New common.Controls.MyLabel()
        Me.MyLabel28 = New common.Controls.MyLabel()
        Me.txtDateOfBirth = New common.Controls.MyDateTimePicker()
        Me.lblDateOfBirth2 = New common.Controls.MyLabel()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.lblSpecies1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.lblExitDate2 = New common.Controls.MyLabel()
        Me.lblStatusChangedDate2 = New common.Controls.MyLabel()
        Me.lblcode2 = New common.Controls.MyLabel()
        Me.lblRemark2 = New common.Controls.MyLabel()
        Me.lblBullSubStatus2 = New common.Controls.MyLabel()
        Me.lblBullStatus2 = New common.Controls.MyLabel()
        Me.lblPrevBullId2 = New common.Controls.MyLabel()
        Me.lblBullSourcePainting2 = New common.Controls.MyLabel()
        Me.lblDamLoactionYield2 = New common.Controls.MyLabel()
        Me.lblBullRating2 = New common.Controls.MyLabel()
        Me.lblPenId2 = New common.Controls.MyLabel()
        Me.lblShedId2 = New common.Controls.MyLabel()
        Me.lblSScentre2 = New common.Controls.MyLabel()
        Me.lblBullAliasName2 = New common.Controls.MyLabel()
        Me.lblSSBullId2 = New common.Controls.MyLabel()
        Me.lblSubCategory2 = New common.Controls.MyLabel()
        Me.lblCountryCode2 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.lblBreed2 = New common.Controls.MyLabel()
        Me.lblBullBookValue2 = New common.Controls.MyLabel()
        Me.lblRegistrationDate = New common.Controls.MyLabel()
        Me.lblCategory2 = New common.Controls.MyLabel()
        Me.lblBullRFID2 = New common.Controls.MyLabel()
        Me.lblExotic2 = New common.Controls.MyLabel()
        Me.lblspecies2 = New common.Controls.MyLabel()
        Me.IsSemen = New common.Controls.MyLabel()
        Me.txtRemark = New common.Controls.MyTextBox()
        Me.TXTPrevBull = New common.Controls.MyTextBox()
        Me.txtDamLocation = New common.Controls.MyTextBox()
        Me.txtBullAlias = New common.Controls.MyTextBox()
        Me.TXTSSbull = New common.Controls.MyTextBox()
        Me.txtBullBook = New common.Controls.MyTextBox()
        Me.txtBullRFID = New common.Controls.MyTextBox()
        Me.TXTExoticBloodPer = New common.Controls.MyTextBox()
        Me.fndCounty = New common.UserControls.txtFinder()
        Me.fndBullSourcePainting = New common.UserControls.txtFinder()
        Me.fndBullRating = New common.UserControls.txtFinder()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem()
        Me.TXTEndDate = New common.Controls.MyDateTimePicker()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.txtRegDate = New common.Controls.MyDateTimePicker()
        Me.txtStatusDateChanged = New common.Controls.MyDateTimePicker()
        Me.fndPenId = New common.UserControls.txtFinder()
        Me.fndShedId = New common.UserControls.txtFinder()
        Me.fndSSCentre = New common.UserControls.txtFinder()
        Me.fndSubStatus = New common.UserControls.txtFinder()
        Me.fndBullStatus = New common.UserControls.txtFinder()
        Me.fndBreed = New common.UserControls.txtFinder()
        Me.fndSubCategory = New common.UserControls.txtFinder()
        Me.fndCategory = New common.UserControls.txtFinder()
        Me.fndSpecies = New common.UserControls.txtFinder()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.fndCode = New common.UserControls.txtNavigator()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtLastDateBreeding = New common.Controls.MyDateTimePicker()
        Me.lblLastdayBreeding = New common.Controls.MyLabel()
        Me.lblPurchaseDate2 = New common.Controls.MyLabel()
        Me.lblPurchaseRequestDate2 = New common.Controls.MyLabel()
        Me.lblSourceName2 = New common.Controls.MyLabel()
        Me.lblBreedingValue2 = New common.Controls.MyLabel()
        Me.lblCapacityOfAverageMonthlyDoses = New common.Controls.MyLabel()
        Me.lblDosesProducedTillDate2 = New common.Controls.MyLabel()
        Me.lblSemenPrice2 = New common.Controls.MyLabel()
        Me.lblFirstCollectionDate2 = New common.Controls.MyLabel()
        Me.lblOwnerOfBull2 = New common.Controls.MyLabel()
        Me.lblSourceAddress2 = New common.Controls.MyLabel()
        Me.lblPurchaseRequestNo2 = New common.Controls.MyLabel()
        Me.txtProducedTillDate = New common.Controls.MyTextBox()
        Me.txtPurchaseDate = New common.Controls.MyDateTimePicker()
        Me.txtPurchaseRequestDate = New common.Controls.MyDateTimePicker()
        Me.txtPurchaseNo = New common.Controls.MyTextBox()
        Me.txtBreedingValue = New common.Controls.MyTextBox()
        Me.txtAveregeDoses = New common.Controls.MyTextBox()
        Me.txtFirstCollectionDate = New common.Controls.MyDateTimePicker()
        Me.txtSemenPrice = New common.Controls.MyTextBox()
        Me.txtOwnerName = New common.Controls.MyTextBox()
        Me.txtSourceName = New common.Controls.MyTextBox()
        Me.fndSourceName = New common.UserControls.txtFinder()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.lblWeight2 = New common.Controls.MyLabel()
        Me.lblBirthWeight2 = New common.Controls.MyLabel()
        Me.lblDamOrgin2 = New common.Controls.MyLabel()
        Me.chkProven = New Telerik.WinControls.UI.RadCheckBox()
        Me.lblNodau2 = New common.Controls.MyLabel()
        Me.chkGenomicTestedBulls = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkIsIBRBull = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkPercentageVerification = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkUnderProgenyTest = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkShouldbeshowninSireDirectory = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkSibilingTeasted = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkSonOfProvenSire = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkKaryotyping = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkGeneticDiseaseTeasting = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtMilkingDone = New common.Controls.MyTextBox()
        Me.txtWeightAtEntry = New common.Controls.MyTextBox()
        Me.txtBirthWeight = New common.Controls.MyTextBox()
        Me.txtlDamOrigin = New common.Controls.MyTextBox()
        Me.txtNodaughters = New common.Controls.MyTextBox()
        Me.RadPageViewPage4 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.gv2 = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage5 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.MyLabel21 = New common.Controls.MyLabel()
        Me.MyLabel20 = New common.Controls.MyLabel()
        Me.MyLabel19 = New common.Controls.MyLabel()
        Me.MyLabel18 = New common.Controls.MyLabel()
        Me.MyLabel17 = New common.Controls.MyLabel()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtQuarntine = New common.Controls.MyTextBox()
        Me.txtREARINGCentre = New common.Controls.MyTextBox()
        Me.txtNomalesborn = New common.Controls.MyTextBox()
        Me.txtNoabortions = New common.Controls.MyTextBox()
        Me.txtTotalConceptions = New common.Controls.MyTextBox()
        Me.txtNoofelitefemalescurrentlypregnant = New common.Controls.MyTextBox()
        Me.txtNoofFemaleCalves = New common.Controls.MyTextBox()
        Me.txtTotalHeiferConceptions = New common.Controls.MyTextBox()
        Me.txtNoundersemencollection = New common.Controls.MyTextBox()
        Me.txtPreQuarantine = New common.Controls.MyTextBox()
        Me.txtNoofinseminationcarriedout = New common.Controls.MyTextBox()
        Me.txtNoofmalecalves = New common.Controls.MyTextBox()
        Me.txtTotalHeiferAI = New common.Controls.MyTextBox()
        Me.txtTrainingCentre = New common.Controls.MyTextBox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.txtNoofmalesproduced = New common.Controls.MyTextBox()
        Me.txtDateofnominatedmatinginitiated = New common.Controls.MyTextBox()
        Me.txtTotalAI = New common.Controls.MyTextBox()
        Me.RadPageViewPage6 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.btnSelectPath1 = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me.lblBullSourcePrintStrew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDamLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBullRating, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRemark, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBullFRID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblExistDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblStatusChangeDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPenId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblShedId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSSCentre, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBullBookValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblExoticBullPercentage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBullSubStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBullAliasName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBullStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSSBullId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDOB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPrevBullId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBreed, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl12DigitBullId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSubCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCountryCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSpecies, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSemen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblbankcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel32, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel28, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDateOfBirth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDateOfBirth2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.lblSpecies1.SuspendLayout()
        CType(Me.lblExitDate2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblStatusChangedDate2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblcode2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRemark2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBullSubStatus2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBullStatus2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPrevBullId2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBullSourcePainting2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDamLoactionYield2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBullRating2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPenId2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblShedId2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSScentre2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBullAliasName2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSSBullId2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSubCategory2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCountryCode2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBreed2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBullBookValue2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRegistrationDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCategory2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBullRFID2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblExotic2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblspecies2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IsSemen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemark, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TXTPrevBull, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDamLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBullAlias, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TXTSSbull, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBullBook, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBullRFID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TXTExoticBloodPer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TXTEndDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRegDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtStatusDateChanged, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.txtLastDateBreeding, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLastdayBreeding, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPurchaseDate2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPurchaseRequestDate2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSourceName2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBreedingValue2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCapacityOfAverageMonthlyDoses, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDosesProducedTillDate2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSemenPrice2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFirstCollectionDate2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblOwnerOfBull2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSourceAddress2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPurchaseRequestNo2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtProducedTillDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPurchaseDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPurchaseRequestDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPurchaseNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBreedingValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAveregeDoses, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFirstCollectionDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSemenPrice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOwnerName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSourceName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblWeight2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBirthWeight2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDamOrgin2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkProven, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNodau2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkGenomicTestedBulls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIsIBRBull, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPercentageVerification, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkUnderProgenyTest, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkShouldbeshowninSireDirectory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSibilingTeasted, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSonOfProvenSire, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkKaryotyping, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkGeneticDiseaseTeasting, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMilkingDone, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtWeightAtEntry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBirthWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtlDamOrigin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNodaughters, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage4.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage5.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtQuarntine, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtREARINGCentre, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNomalesborn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoabortions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalConceptions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoofelitefemalescurrentlypregnant, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoofFemaleCalves, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalHeiferConceptions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoundersemencollection, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPreQuarantine, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoofinseminationcarriedout, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoofmalecalves, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalHeiferAI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTrainingCentre, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoofmalesproduced, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDateofnominatedmatinginitiated, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalAI, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage6.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSelectPath1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblBullSourcePrintStrew
        '
        Me.lblBullSourcePrintStrew.FieldName = Nothing
        Me.lblBullSourcePrintStrew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBullSourcePrintStrew.Location = New System.Drawing.Point(606, 258)
        Me.lblBullSourcePrintStrew.Name = "lblBullSourcePrintStrew"
        Me.lblBullSourcePrintStrew.Size = New System.Drawing.Size(157, 16)
        Me.lblBullSourcePrintStrew.TabIndex = 409
        Me.lblBullSourcePrintStrew.Text = "Bull Source for printing straws"
        '
        'lblDamLocation
        '
        Me.lblDamLocation.FieldName = Nothing
        Me.lblDamLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDamLocation.Location = New System.Drawing.Point(456, 258)
        Me.lblDamLocation.Name = "lblDamLocation"
        Me.lblDamLocation.Size = New System.Drawing.Size(112, 16)
        Me.lblDamLocation.TabIndex = 407
        Me.lblDamLocation.Text = "Dam's Loaction Yield"
        '
        'lblBullRating
        '
        Me.lblBullRating.FieldName = Nothing
        Me.lblBullRating.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBullRating.Location = New System.Drawing.Point(321, 258)
        Me.lblBullRating.Name = "lblBullRating"
        Me.lblBullRating.Size = New System.Drawing.Size(61, 16)
        Me.lblBullRating.TabIndex = 406
        Me.lblBullRating.Text = "Bull Rating"
        '
        'lblRemark
        '
        Me.lblRemark.FieldName = Nothing
        Me.lblRemark.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRemark.Location = New System.Drawing.Point(151, 258)
        Me.lblRemark.Name = "lblRemark"
        Me.lblRemark.Size = New System.Drawing.Size(46, 16)
        Me.lblRemark.TabIndex = 404
        Me.lblRemark.Text = "Remark"
        '
        'lblBullFRID
        '
        Me.lblBullFRID.FieldName = Nothing
        Me.lblBullFRID.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBullFRID.Location = New System.Drawing.Point(12, 258)
        Me.lblBullFRID.Name = "lblBullFRID"
        Me.lblBullFRID.Size = New System.Drawing.Size(55, 16)
        Me.lblBullFRID.TabIndex = 402
        Me.lblBullFRID.Text = "Bull RFID"
        '
        'lblExistDate
        '
        Me.lblExistDate.FieldName = Nothing
        Me.lblExistDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExistDate.Location = New System.Drawing.Point(854, 191)
        Me.lblExistDate.Name = "lblExistDate"
        Me.lblExistDate.Size = New System.Drawing.Size(58, 16)
        Me.lblExistDate.TabIndex = 401
        Me.lblExistDate.Text = "Exist Date"
        '
        'lblStatusChangeDate
        '
        Me.lblStatusChangeDate.FieldName = Nothing
        Me.lblStatusChangeDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatusChangeDate.Location = New System.Drawing.Point(731, 191)
        Me.lblStatusChangeDate.Name = "lblStatusChangeDate"
        Me.lblStatusChangeDate.Size = New System.Drawing.Size(108, 16)
        Me.lblStatusChangeDate.TabIndex = 400
        Me.lblStatusChangeDate.Text = "Status Change Date"
        '
        'lblPenId
        '
        Me.lblPenId.FieldName = Nothing
        Me.lblPenId.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPenId.Location = New System.Drawing.Point(609, 189)
        Me.lblPenId.Name = "lblPenId"
        Me.lblPenId.Size = New System.Drawing.Size(39, 16)
        Me.lblPenId.TabIndex = 398
        Me.lblPenId.Text = "Pen Id"
        '
        'lblShedId
        '
        Me.lblShedId.FieldName = Nothing
        Me.lblShedId.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblShedId.Location = New System.Drawing.Point(458, 190)
        Me.lblShedId.Name = "lblShedId"
        Me.lblShedId.Size = New System.Drawing.Size(45, 16)
        Me.lblShedId.TabIndex = 396
        Me.lblShedId.Text = "Shed Id"
        '
        'lblSSCentre
        '
        Me.lblSSCentre.FieldName = Nothing
        Me.lblSSCentre.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSSCentre.Location = New System.Drawing.Point(322, 192)
        Me.lblSSCentre.Name = "lblSSCentre"
        Me.lblSSCentre.Size = New System.Drawing.Size(58, 16)
        Me.lblSSCentre.TabIndex = 394
        Me.lblSSCentre.Text = "SS Centre"
        '
        'lblBullBookValue
        '
        Me.lblBullBookValue.FieldName = Nothing
        Me.lblBullBookValue.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBullBookValue.Location = New System.Drawing.Point(154, 191)
        Me.lblBullBookValue.Name = "lblBullBookValue"
        Me.lblBullBookValue.Size = New System.Drawing.Size(86, 16)
        Me.lblBullBookValue.TabIndex = 392
        Me.lblBullBookValue.Text = "Bull Book Value"
        '
        'lblExoticBullPercentage
        '
        Me.lblExoticBullPercentage.FieldName = Nothing
        Me.lblExoticBullPercentage.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExoticBullPercentage.Location = New System.Drawing.Point(9, 191)
        Me.lblExoticBullPercentage.Name = "lblExoticBullPercentage"
        Me.lblExoticBullPercentage.Size = New System.Drawing.Size(123, 16)
        Me.lblExoticBullPercentage.TabIndex = 390
        Me.lblExoticBullPercentage.Text = " Exotic Bull Percentage"
        '
        'lblBullSubStatus
        '
        Me.lblBullSubStatus.FieldName = Nothing
        Me.lblBullSubStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBullSubStatus.Location = New System.Drawing.Point(854, 124)
        Me.lblBullSubStatus.Name = "lblBullSubStatus"
        Me.lblBullSubStatus.Size = New System.Drawing.Size(84, 16)
        Me.lblBullSubStatus.TabIndex = 388
        Me.lblBullSubStatus.Text = "Bull Sub Status"
        '
        'lblBullAliasName
        '
        Me.lblBullAliasName.FieldName = Nothing
        Me.lblBullAliasName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBullAliasName.Location = New System.Drawing.Point(847, 65)
        Me.lblBullAliasName.Name = "lblBullAliasName"
        Me.lblBullAliasName.Size = New System.Drawing.Size(86, 16)
        Me.lblBullAliasName.TabIndex = 386
        Me.lblBullAliasName.Text = "Bull Alias Name"
        '
        'lblBullStatus
        '
        Me.lblBullStatus.FieldName = Nothing
        Me.lblBullStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBullStatus.Location = New System.Drawing.Point(731, 126)
        Me.lblBullStatus.Name = "lblBullStatus"
        Me.lblBullStatus.Size = New System.Drawing.Size(60, 16)
        Me.lblBullStatus.TabIndex = 384
        Me.lblBullStatus.Text = "Bull Status"
        '
        'lblSSBullId
        '
        Me.lblSSBullId.FieldName = Nothing
        Me.lblSSBullId.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSSBullId.Location = New System.Drawing.Point(732, 65)
        Me.lblSSBullId.Name = "lblSSBullId"
        Me.lblSSBullId.Size = New System.Drawing.Size(56, 16)
        Me.lblSSBullId.TabIndex = 383
        Me.lblSSBullId.Text = "SS Bull Id"
        '
        'lblDOB
        '
        Me.lblDOB.FieldName = Nothing
        Me.lblDOB.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDOB.Location = New System.Drawing.Point(609, 125)
        Me.lblDOB.Name = "lblDOB"
        Me.lblDOB.Size = New System.Drawing.Size(72, 16)
        Me.lblDOB.TabIndex = 380
        Me.lblDOB.Text = "Date Of Birth"
        '
        'lblPrevBullId
        '
        Me.lblPrevBullId.FieldName = Nothing
        Me.lblPrevBullId.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPrevBullId.Location = New System.Drawing.Point(597, 66)
        Me.lblPrevBullId.Name = "lblPrevBullId"
        Me.lblPrevBullId.Size = New System.Drawing.Size(64, 16)
        Me.lblPrevBullId.TabIndex = 378
        Me.lblPrevBullId.Text = "Prev Bull Id"
        '
        'lblBreed
        '
        Me.lblBreed.FieldName = Nothing
        Me.lblBreed.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBreed.Location = New System.Drawing.Point(458, 128)
        Me.lblBreed.Name = "lblBreed"
        Me.lblBreed.Size = New System.Drawing.Size(36, 16)
        Me.lblBreed.TabIndex = 376
        Me.lblBreed.Text = "Breed"
        '
        'lbl12DigitBullId
        '
        Me.lbl12DigitBullId.FieldName = Nothing
        Me.lbl12DigitBullId.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl12DigitBullId.Location = New System.Drawing.Point(453, 69)
        Me.lbl12DigitBullId.Name = "lbl12DigitBullId"
        Me.lbl12DigitBullId.Size = New System.Drawing.Size(79, 16)
        Me.lbl12DigitBullId.TabIndex = 374
        Me.lbl12DigitBullId.Text = "12 Digit Bull Id"
        '
        'lblSubCategory
        '
        Me.lblSubCategory.FieldName = Nothing
        Me.lblSubCategory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubCategory.Location = New System.Drawing.Point(319, 129)
        Me.lblSubCategory.Name = "lblSubCategory"
        Me.lblSubCategory.Size = New System.Drawing.Size(72, 16)
        Me.lblSubCategory.TabIndex = 373
        Me.lblSubCategory.Text = "SubCategory"
        '
        'lblCountryCode
        '
        Me.lblCountryCode.FieldName = Nothing
        Me.lblCountryCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCountryCode.Location = New System.Drawing.Point(321, 67)
        Me.lblCountryCode.Name = "lblCountryCode"
        Me.lblCountryCode.Size = New System.Drawing.Size(76, 16)
        Me.lblCountryCode.TabIndex = 370
        Me.lblCountryCode.Text = "Country Code"
        '
        'lblCategory
        '
        Me.lblCategory.FieldName = Nothing
        Me.lblCategory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCategory.Location = New System.Drawing.Point(154, 134)
        Me.lblCategory.Name = "lblCategory"
        Me.lblCategory.Size = New System.Drawing.Size(52, 16)
        Me.lblCategory.TabIndex = 368
        Me.lblCategory.Text = "Category"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(154, 71)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(94, 16)
        Me.MyLabel3.TabIndex = 367
        Me.MyLabel3.Text = "Registration Date"
        '
        'lblSpecies
        '
        Me.lblSpecies.FieldName = Nothing
        Me.lblSpecies.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSpecies.Location = New System.Drawing.Point(10, 134)
        Me.lblSpecies.Name = "lblSpecies"
        Me.lblSpecies.Size = New System.Drawing.Size(46, 16)
        Me.lblSpecies.TabIndex = 67
        Me.lblSpecies.Text = "Species"
        '
        'lblSemen
        '
        Me.lblSemen.FieldName = Nothing
        Me.lblSemen.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSemen.Location = New System.Drawing.Point(10, 71)
        Me.lblSemen.Name = "lblSemen"
        Me.lblSemen.Size = New System.Drawing.Size(124, 16)
        Me.lblSemen.TabIndex = 64
        Me.lblSemen.Text = "Is Semen/Bull Imported"
        '
        'lblbankcode
        '
        Me.lblbankcode.FieldName = Nothing
        Me.lblbankcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblbankcode.Location = New System.Drawing.Point(8, 38)
        Me.lblbankcode.Name = "lblbankcode"
        Me.lblbankcode.Size = New System.Drawing.Size(33, 16)
        Me.lblbankcode.TabIndex = 63
        Me.lblbankcode.Text = "Code"
        '
        'MyLabel15
        '
        Me.MyLabel15.FieldName = Nothing
        Me.MyLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel15.Location = New System.Drawing.Point(6, 18)
        Me.MyLabel15.Name = "MyLabel15"
        Me.MyLabel15.Size = New System.Drawing.Size(91, 16)
        Me.MyLabel15.TabIndex = 63
        Me.MyLabel15.Text = "Supervisor Code"
        '
        'MyLabel32
        '
        Me.MyLabel32.FieldName = Nothing
        Me.MyLabel32.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel32.Location = New System.Drawing.Point(14, 183)
        Me.MyLabel32.Name = "MyLabel32"
        Me.MyLabel32.Size = New System.Drawing.Size(84, 16)
        Me.MyLabel32.TabIndex = 400
        Me.MyLabel32.Text = "Company Bank"
        '
        'MyLabel28
        '
        Me.MyLabel28.FieldName = Nothing
        Me.MyLabel28.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel28.Location = New System.Drawing.Point(14, 17)
        Me.MyLabel28.Name = "MyLabel28"
        Me.MyLabel28.Size = New System.Drawing.Size(84, 16)
        Me.MyLabel28.TabIndex = 376
        Me.MyLabel28.Text = "Company Bank"
        '
        'txtDateOfBirth
        '
        Me.txtDateOfBirth.CalculationExpression = Nothing
        Me.txtDateOfBirth.CustomFormat = "dd/MM/yyyy"
        Me.txtDateOfBirth.FieldCode = Nothing
        Me.txtDateOfBirth.FieldDesc = Nothing
        Me.txtDateOfBirth.FieldMaxLength = 0
        Me.txtDateOfBirth.FieldName = Nothing
        Me.txtDateOfBirth.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateOfBirth.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDateOfBirth.isCalculatedField = False
        Me.txtDateOfBirth.IsSourceFromTable = False
        Me.txtDateOfBirth.IsSourceFromValueList = False
        Me.txtDateOfBirth.IsUnique = False
        Me.txtDateOfBirth.Location = New System.Drawing.Point(627, 248)
        Me.txtDateOfBirth.MendatroryField = False
        Me.txtDateOfBirth.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDateOfBirth.MyLinkLable1 = Me.lblDateOfBirth2
        Me.txtDateOfBirth.MyLinkLable2 = Nothing
        Me.txtDateOfBirth.Name = "txtDateOfBirth"
        Me.txtDateOfBirth.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDateOfBirth.ReferenceFieldDesc = Nothing
        Me.txtDateOfBirth.ReferenceFieldName = Nothing
        Me.txtDateOfBirth.ReferenceTableName = Nothing
        Me.txtDateOfBirth.Size = New System.Drawing.Size(159, 18)
        Me.txtDateOfBirth.TabIndex = 482
        Me.txtDateOfBirth.TabStop = False
        Me.txtDateOfBirth.Text = "13/06/2011"
        Me.txtDateOfBirth.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'lblDateOfBirth2
        '
        Me.lblDateOfBirth2.FieldName = Nothing
        Me.lblDateOfBirth2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblDateOfBirth2.Location = New System.Drawing.Point(461, 250)
        Me.lblDateOfBirth2.Name = "lblDateOfBirth2"
        Me.lblDateOfBirth2.Size = New System.Drawing.Size(72, 16)
        Me.lblDateOfBirth2.TabIndex = 527
        Me.lblDateOfBirth2.Text = "Date Of Birth"
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(996, 509)
        Me.SplitContainer1.SplitterDistance = 470
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.lblSpecies1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage4)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage5)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage6)
        Me.RadPageView1.DefaultPage = Me.lblSpecies1
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.lblSpecies1
        Me.RadPageView1.Size = New System.Drawing.Size(996, 470)
        Me.RadPageView1.TabIndex = 1
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ShowItemCloseButton = False
        '
        'lblSpecies1
        '
        Me.lblSpecies1.Controls.Add(Me.lblDateOfBirth2)
        Me.lblSpecies1.Controls.Add(Me.lblExitDate2)
        Me.lblSpecies1.Controls.Add(Me.lblStatusChangedDate2)
        Me.lblSpecies1.Controls.Add(Me.lblcode2)
        Me.lblSpecies1.Controls.Add(Me.lblRemark2)
        Me.lblSpecies1.Controls.Add(Me.lblBullSubStatus2)
        Me.lblSpecies1.Controls.Add(Me.lblBullStatus2)
        Me.lblSpecies1.Controls.Add(Me.lblPrevBullId2)
        Me.lblSpecies1.Controls.Add(Me.lblBullSourcePainting2)
        Me.lblSpecies1.Controls.Add(Me.lblDamLoactionYield2)
        Me.lblSpecies1.Controls.Add(Me.lblBullRating2)
        Me.lblSpecies1.Controls.Add(Me.lblPenId2)
        Me.lblSpecies1.Controls.Add(Me.lblShedId2)
        Me.lblSpecies1.Controls.Add(Me.lblSScentre2)
        Me.lblSpecies1.Controls.Add(Me.lblBullAliasName2)
        Me.lblSpecies1.Controls.Add(Me.lblSSBullId2)
        Me.lblSpecies1.Controls.Add(Me.lblSubCategory2)
        Me.lblSpecies1.Controls.Add(Me.lblCountryCode2)
        Me.lblSpecies1.Controls.Add(Me.MyLabel2)
        Me.lblSpecies1.Controls.Add(Me.lblBreed2)
        Me.lblSpecies1.Controls.Add(Me.lblBullBookValue2)
        Me.lblSpecies1.Controls.Add(Me.lblRegistrationDate)
        Me.lblSpecies1.Controls.Add(Me.lblCategory2)
        Me.lblSpecies1.Controls.Add(Me.lblBullRFID2)
        Me.lblSpecies1.Controls.Add(Me.lblExotic2)
        Me.lblSpecies1.Controls.Add(Me.lblspecies2)
        Me.lblSpecies1.Controls.Add(Me.IsSemen)
        Me.lblSpecies1.Controls.Add(Me.txtRemark)
        Me.lblSpecies1.Controls.Add(Me.TXTPrevBull)
        Me.lblSpecies1.Controls.Add(Me.txtDamLocation)
        Me.lblSpecies1.Controls.Add(Me.txtBullAlias)
        Me.lblSpecies1.Controls.Add(Me.TXTSSbull)
        Me.lblSpecies1.Controls.Add(Me.txtBullBook)
        Me.lblSpecies1.Controls.Add(Me.txtBullRFID)
        Me.lblSpecies1.Controls.Add(Me.TXTExoticBloodPer)
        Me.lblSpecies1.Controls.Add(Me.fndCounty)
        Me.lblSpecies1.Controls.Add(Me.fndBullSourcePainting)
        Me.lblSpecies1.Controls.Add(Me.fndBullRating)
        Me.lblSpecies1.Controls.Add(Me.RadMenu1)
        Me.lblSpecies1.Controls.Add(Me.TXTEndDate)
        Me.lblSpecies1.Controls.Add(Me.btnnew)
        Me.lblSpecies1.Controls.Add(Me.txtRegDate)
        Me.lblSpecies1.Controls.Add(Me.txtStatusDateChanged)
        Me.lblSpecies1.Controls.Add(Me.txtDateOfBirth)
        Me.lblSpecies1.Controls.Add(Me.fndPenId)
        Me.lblSpecies1.Controls.Add(Me.fndShedId)
        Me.lblSpecies1.Controls.Add(Me.fndSSCentre)
        Me.lblSpecies1.Controls.Add(Me.fndSubStatus)
        Me.lblSpecies1.Controls.Add(Me.fndBullStatus)
        Me.lblSpecies1.Controls.Add(Me.fndBreed)
        Me.lblSpecies1.Controls.Add(Me.fndSubCategory)
        Me.lblSpecies1.Controls.Add(Me.fndCategory)
        Me.lblSpecies1.Controls.Add(Me.fndSpecies)
        Me.lblSpecies1.Controls.Add(Me.RadioButton2)
        Me.lblSpecies1.Controls.Add(Me.RadioButton1)
        Me.lblSpecies1.Controls.Add(Me.fndCode)
        Me.lblSpecies1.ItemSize = New System.Drawing.SizeF(125.0!, 28.0!)
        Me.lblSpecies1.Location = New System.Drawing.Point(10, 37)
        Me.lblSpecies1.Name = "lblSpecies1"
        Me.lblSpecies1.Size = New System.Drawing.Size(975, 422)
        Me.lblSpecies1.Text = "Bull Basic Information"
        '
        'lblExitDate2
        '
        Me.lblExitDate2.FieldName = Nothing
        Me.lblExitDate2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblExitDate2.Location = New System.Drawing.Point(461, 325)
        Me.lblExitDate2.Name = "lblExitDate2"
        Me.lblExitDate2.Size = New System.Drawing.Size(52, 16)
        Me.lblExitDate2.TabIndex = 526
        Me.lblExitDate2.Text = "Exit Date"
        '
        'lblStatusChangedDate2
        '
        Me.lblStatusChangedDate2.FieldName = Nothing
        Me.lblStatusChangedDate2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblStatusChangedDate2.Location = New System.Drawing.Point(22, 361)
        Me.lblStatusChangedDate2.Name = "lblStatusChangedDate2"
        Me.lblStatusChangedDate2.Size = New System.Drawing.Size(114, 16)
        Me.lblStatusChangedDate2.TabIndex = 525
        Me.lblStatusChangedDate2.Text = "Status Changed Date"
        '
        'lblcode2
        '
        Me.lblcode2.FieldName = Nothing
        Me.lblcode2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblcode2.Location = New System.Drawing.Point(20, 29)
        Me.lblcode2.Name = "lblcode2"
        Me.lblcode2.Size = New System.Drawing.Size(33, 16)
        Me.lblcode2.TabIndex = 524
        Me.lblcode2.Text = "Code"
        '
        'lblRemark2
        '
        Me.lblRemark2.FieldName = Nothing
        Me.lblRemark2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblRemark2.Location = New System.Drawing.Point(461, 350)
        Me.lblRemark2.Name = "lblRemark2"
        Me.lblRemark2.Size = New System.Drawing.Size(46, 16)
        Me.lblRemark2.TabIndex = 523
        Me.lblRemark2.Text = "Remark"
        '
        'lblBullSubStatus2
        '
        Me.lblBullSubStatus2.FieldName = Nothing
        Me.lblBullSubStatus2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblBullSubStatus2.Location = New System.Drawing.Point(461, 299)
        Me.lblBullSubStatus2.Name = "lblBullSubStatus2"
        Me.lblBullSubStatus2.Size = New System.Drawing.Size(84, 16)
        Me.lblBullSubStatus2.TabIndex = 522
        Me.lblBullSubStatus2.Text = "Bull Sub Status"
        '
        'lblBullStatus2
        '
        Me.lblBullStatus2.FieldName = Nothing
        Me.lblBullStatus2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblBullStatus2.Location = New System.Drawing.Point(461, 273)
        Me.lblBullStatus2.Name = "lblBullStatus2"
        Me.lblBullStatus2.Size = New System.Drawing.Size(60, 16)
        Me.lblBullStatus2.TabIndex = 521
        Me.lblBullStatus2.Text = "Bull Status"
        '
        'lblPrevBullId2
        '
        Me.lblPrevBullId2.FieldName = Nothing
        Me.lblPrevBullId2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblPrevBullId2.Location = New System.Drawing.Point(461, 226)
        Me.lblPrevBullId2.Name = "lblPrevBullId2"
        Me.lblPrevBullId2.Size = New System.Drawing.Size(64, 16)
        Me.lblPrevBullId2.TabIndex = 520
        Me.lblPrevBullId2.Text = "Prev Bull Id"
        '
        'lblBullSourcePainting2
        '
        Me.lblBullSourcePainting2.FieldName = Nothing
        Me.lblBullSourcePainting2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblBullSourcePainting2.Location = New System.Drawing.Point(461, 201)
        Me.lblBullSourcePainting2.Name = "lblBullSourcePainting2"
        Me.lblBullSourcePainting2.Size = New System.Drawing.Size(160, 16)
        Me.lblBullSourcePainting2.TabIndex = 519
        Me.lblBullSourcePainting2.Text = "Bull Source For Painting Straw"
        '
        'lblDamLoactionYield2
        '
        Me.lblDamLoactionYield2.FieldName = Nothing
        Me.lblDamLoactionYield2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblDamLoactionYield2.Location = New System.Drawing.Point(461, 179)
        Me.lblDamLoactionYield2.Name = "lblDamLoactionYield2"
        Me.lblDamLoactionYield2.Size = New System.Drawing.Size(112, 16)
        Me.lblDamLoactionYield2.TabIndex = 518
        Me.lblDamLoactionYield2.Text = "Dam's Loaction Yield"
        '
        'lblBullRating2
        '
        Me.lblBullRating2.FieldName = Nothing
        Me.lblBullRating2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblBullRating2.Location = New System.Drawing.Point(461, 154)
        Me.lblBullRating2.Name = "lblBullRating2"
        Me.lblBullRating2.Size = New System.Drawing.Size(73, 16)
        Me.lblBullRating2.TabIndex = 517
        Me.lblBullRating2.Text = "Bull Rating(*)"
        '
        'lblPenId2
        '
        Me.lblPenId2.FieldName = Nothing
        Me.lblPenId2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblPenId2.Location = New System.Drawing.Point(461, 126)
        Me.lblPenId2.Name = "lblPenId2"
        Me.lblPenId2.Size = New System.Drawing.Size(39, 16)
        Me.lblPenId2.TabIndex = 516
        Me.lblPenId2.Text = "Pen Id"
        '
        'lblShedId2
        '
        Me.lblShedId2.FieldName = Nothing
        Me.lblShedId2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblShedId2.Location = New System.Drawing.Point(461, 100)
        Me.lblShedId2.Name = "lblShedId2"
        Me.lblShedId2.Size = New System.Drawing.Size(45, 16)
        Me.lblShedId2.TabIndex = 515
        Me.lblShedId2.Text = "Shed Id"
        '
        'lblSScentre2
        '
        Me.lblSScentre2.FieldName = Nothing
        Me.lblSScentre2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblSScentre2.Location = New System.Drawing.Point(461, 75)
        Me.lblSScentre2.Name = "lblSScentre2"
        Me.lblSScentre2.Size = New System.Drawing.Size(58, 16)
        Me.lblSScentre2.TabIndex = 514
        Me.lblSScentre2.Text = "SS Centre"
        '
        'lblBullAliasName2
        '
        Me.lblBullAliasName2.FieldName = Nothing
        Me.lblBullAliasName2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblBullAliasName2.Location = New System.Drawing.Point(22, 389)
        Me.lblBullAliasName2.Name = "lblBullAliasName2"
        Me.lblBullAliasName2.Size = New System.Drawing.Size(86, 16)
        Me.lblBullAliasName2.TabIndex = 513
        Me.lblBullAliasName2.Text = "Bull Alias Name"
        '
        'lblSSBullId2
        '
        Me.lblSSBullId2.FieldName = Nothing
        Me.lblSSBullId2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblSSBullId2.Location = New System.Drawing.Point(21, 336)
        Me.lblSSBullId2.Name = "lblSSBullId2"
        Me.lblSSBullId2.Size = New System.Drawing.Size(56, 16)
        Me.lblSSBullId2.TabIndex = 512
        Me.lblSSBullId2.Text = "SS Bull Id"
        '
        'lblSubCategory2
        '
        Me.lblSubCategory2.FieldName = Nothing
        Me.lblSubCategory2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblSubCategory2.Location = New System.Drawing.Point(21, 307)
        Me.lblSubCategory2.Name = "lblSubCategory2"
        Me.lblSubCategory2.Size = New System.Drawing.Size(75, 16)
        Me.lblSubCategory2.TabIndex = 511
        Me.lblSubCategory2.Text = "Sub Category"
        '
        'lblCountryCode2
        '
        Me.lblCountryCode2.FieldName = Nothing
        Me.lblCountryCode2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblCountryCode2.Location = New System.Drawing.Point(22, 282)
        Me.lblCountryCode2.Name = "lblCountryCode2"
        Me.lblCountryCode2.Size = New System.Drawing.Size(76, 16)
        Me.lblCountryCode2.TabIndex = 510
        Me.lblCountryCode2.Text = "Country Code"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel2.Location = New System.Drawing.Point(22, 255)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(36, 16)
        Me.MyLabel2.TabIndex = 509
        Me.MyLabel2.Text = "Breed"
        '
        'lblBreed2
        '
        Me.lblBreed2.FieldName = Nothing
        Me.lblBreed2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblBreed2.Location = New System.Drawing.Point(22, 255)
        Me.lblBreed2.Name = "lblBreed2"
        Me.lblBreed2.Size = New System.Drawing.Size(36, 16)
        Me.lblBreed2.TabIndex = 509
        Me.lblBreed2.Text = "Breed"
        '
        'lblBullBookValue2
        '
        Me.lblBullBookValue2.FieldName = Nothing
        Me.lblBullBookValue2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblBullBookValue2.Location = New System.Drawing.Point(22, 225)
        Me.lblBullBookValue2.Name = "lblBullBookValue2"
        Me.lblBullBookValue2.Size = New System.Drawing.Size(86, 16)
        Me.lblBullBookValue2.TabIndex = 508
        Me.lblBullBookValue2.Text = "Bull Book Value"
        '
        'lblRegistrationDate
        '
        Me.lblRegistrationDate.FieldName = Nothing
        Me.lblRegistrationDate.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblRegistrationDate.Location = New System.Drawing.Point(21, 176)
        Me.lblRegistrationDate.Name = "lblRegistrationDate"
        Me.lblRegistrationDate.Size = New System.Drawing.Size(94, 16)
        Me.lblRegistrationDate.TabIndex = 507
        Me.lblRegistrationDate.Text = "Registration Date"
        '
        'lblCategory2
        '
        Me.lblCategory2.FieldName = Nothing
        Me.lblCategory2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblCategory2.Location = New System.Drawing.Point(20, 199)
        Me.lblCategory2.Name = "lblCategory2"
        Me.lblCategory2.Size = New System.Drawing.Size(52, 16)
        Me.lblCategory2.TabIndex = 506
        Me.lblCategory2.Text = "Category"
        '
        'lblBullRFID2
        '
        Me.lblBullRFID2.FieldName = Nothing
        Me.lblBullRFID2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblBullRFID2.Location = New System.Drawing.Point(19, 149)
        Me.lblBullRFID2.Name = "lblBullRFID2"
        Me.lblBullRFID2.Size = New System.Drawing.Size(55, 16)
        Me.lblBullRFID2.TabIndex = 505
        Me.lblBullRFID2.Text = "Bull RFID"
        '
        'lblExotic2
        '
        Me.lblExotic2.FieldName = Nothing
        Me.lblExotic2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblExotic2.Location = New System.Drawing.Point(18, 124)
        Me.lblExotic2.Name = "lblExotic2"
        Me.lblExotic2.Size = New System.Drawing.Size(130, 16)
        Me.lblExotic2.TabIndex = 504
        Me.lblExotic2.Text = "Exotic Blood Percentage"
        '
        'lblspecies2
        '
        Me.lblspecies2.FieldName = Nothing
        Me.lblspecies2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblspecies2.Location = New System.Drawing.Point(18, 98)
        Me.lblspecies2.Name = "lblspecies2"
        Me.lblspecies2.Size = New System.Drawing.Size(46, 16)
        Me.lblspecies2.TabIndex = 503
        Me.lblspecies2.Text = "Species"
        '
        'IsSemen
        '
        Me.IsSemen.FieldName = Nothing
        Me.IsSemen.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.IsSemen.Location = New System.Drawing.Point(18, 76)
        Me.IsSemen.Name = "IsSemen"
        Me.IsSemen.Size = New System.Drawing.Size(124, 16)
        Me.IsSemen.TabIndex = 502
        Me.IsSemen.Text = "Is Semen/Bull Imported"
        '
        'txtRemark
        '
        Me.txtRemark.CalculationExpression = Nothing
        Me.txtRemark.FieldCode = Nothing
        Me.txtRemark.FieldDesc = Nothing
        Me.txtRemark.FieldMaxLength = 0
        Me.txtRemark.FieldName = Nothing
        Me.txtRemark.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemark.isCalculatedField = False
        Me.txtRemark.IsSourceFromTable = False
        Me.txtRemark.IsSourceFromValueList = False
        Me.txtRemark.IsUnique = False
        Me.txtRemark.Location = New System.Drawing.Point(626, 351)
        Me.txtRemark.MaxLength = 200
        Me.txtRemark.MendatroryField = True
        Me.txtRemark.MyLinkLable1 = Me.lblRemark2
        Me.txtRemark.MyLinkLable2 = Nothing
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.ReferenceFieldDesc = Nothing
        Me.txtRemark.ReferenceFieldName = Nothing
        Me.txtRemark.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtRemark.RootElement.StretchVertically = True
        Me.txtRemark.Size = New System.Drawing.Size(228, 54)
        Me.txtRemark.TabIndex = 500
        '
        'TXTPrevBull
        '
        Me.TXTPrevBull.CalculationExpression = Nothing
        Me.TXTPrevBull.FieldCode = Nothing
        Me.TXTPrevBull.FieldDesc = Nothing
        Me.TXTPrevBull.FieldMaxLength = 0
        Me.TXTPrevBull.FieldName = Nothing
        Me.TXTPrevBull.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTPrevBull.isCalculatedField = False
        Me.TXTPrevBull.IsSourceFromTable = False
        Me.TXTPrevBull.IsSourceFromValueList = False
        Me.TXTPrevBull.IsUnique = False
        Me.TXTPrevBull.Location = New System.Drawing.Point(627, 222)
        Me.TXTPrevBull.MaxLength = 200
        Me.TXTPrevBull.MendatroryField = True
        Me.TXTPrevBull.MyLinkLable1 = Me.lblPrevBullId2
        Me.TXTPrevBull.MyLinkLable2 = Nothing
        Me.TXTPrevBull.Name = "TXTPrevBull"
        Me.TXTPrevBull.ReferenceFieldDesc = Nothing
        Me.TXTPrevBull.ReferenceFieldName = Nothing
        Me.TXTPrevBull.ReferenceTableName = Nothing
        '
        '
        '
        Me.TXTPrevBull.RootElement.StretchVertically = True
        Me.TXTPrevBull.Size = New System.Drawing.Size(226, 20)
        Me.TXTPrevBull.TabIndex = 498
        '
        'txtDamLocation
        '
        Me.txtDamLocation.CalculationExpression = Nothing
        Me.txtDamLocation.FieldCode = Nothing
        Me.txtDamLocation.FieldDesc = Nothing
        Me.txtDamLocation.FieldMaxLength = 0
        Me.txtDamLocation.FieldName = Nothing
        Me.txtDamLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDamLocation.isCalculatedField = False
        Me.txtDamLocation.IsSourceFromTable = False
        Me.txtDamLocation.IsSourceFromValueList = False
        Me.txtDamLocation.IsUnique = False
        Me.txtDamLocation.Location = New System.Drawing.Point(628, 170)
        Me.txtDamLocation.MaxLength = 200
        Me.txtDamLocation.MendatroryField = True
        Me.txtDamLocation.MyLinkLable1 = Me.lblDamLoactionYield2
        Me.txtDamLocation.MyLinkLable2 = Nothing
        Me.txtDamLocation.Name = "txtDamLocation"
        Me.txtDamLocation.ReferenceFieldDesc = Nothing
        Me.txtDamLocation.ReferenceFieldName = Nothing
        Me.txtDamLocation.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtDamLocation.RootElement.StretchVertically = True
        Me.txtDamLocation.Size = New System.Drawing.Size(226, 20)
        Me.txtDamLocation.TabIndex = 497
        '
        'txtBullAlias
        '
        Me.txtBullAlias.CalculationExpression = Nothing
        Me.txtBullAlias.FieldCode = Nothing
        Me.txtBullAlias.FieldDesc = Nothing
        Me.txtBullAlias.FieldMaxLength = 0
        Me.txtBullAlias.FieldName = Nothing
        Me.txtBullAlias.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBullAlias.isCalculatedField = False
        Me.txtBullAlias.IsSourceFromTable = False
        Me.txtBullAlias.IsSourceFromValueList = False
        Me.txtBullAlias.IsUnique = False
        Me.txtBullAlias.Location = New System.Drawing.Point(155, 389)
        Me.txtBullAlias.MaxLength = 200
        Me.txtBullAlias.MendatroryField = True
        Me.txtBullAlias.MyLinkLable1 = Nothing
        Me.txtBullAlias.MyLinkLable2 = Nothing
        Me.txtBullAlias.Name = "txtBullAlias"
        Me.txtBullAlias.ReferenceFieldDesc = Nothing
        Me.txtBullAlias.ReferenceFieldName = Nothing
        Me.txtBullAlias.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtBullAlias.RootElement.StretchVertically = True
        Me.txtBullAlias.Size = New System.Drawing.Size(238, 18)
        Me.txtBullAlias.TabIndex = 496
        '
        'TXTSSbull
        '
        Me.TXTSSbull.CalculationExpression = Nothing
        Me.TXTSSbull.FieldCode = Nothing
        Me.TXTSSbull.FieldDesc = Nothing
        Me.TXTSSbull.FieldMaxLength = 0
        Me.TXTSSbull.FieldName = Nothing
        Me.TXTSSbull.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTSSbull.isCalculatedField = False
        Me.TXTSSbull.IsSourceFromTable = False
        Me.TXTSSbull.IsSourceFromValueList = False
        Me.TXTSSbull.IsUnique = False
        Me.TXTSSbull.Location = New System.Drawing.Point(155, 334)
        Me.TXTSSbull.MaxLength = 200
        Me.TXTSSbull.MendatroryField = True
        Me.TXTSSbull.MyLinkLable1 = Nothing
        Me.TXTSSbull.MyLinkLable2 = Nothing
        Me.TXTSSbull.Name = "TXTSSbull"
        Me.TXTSSbull.ReferenceFieldDesc = Nothing
        Me.TXTSSbull.ReferenceFieldName = Nothing
        Me.TXTSSbull.ReferenceTableName = Nothing
        '
        '
        '
        Me.TXTSSbull.RootElement.StretchVertically = True
        Me.TXTSSbull.Size = New System.Drawing.Size(238, 18)
        Me.TXTSSbull.TabIndex = 495
        '
        'txtBullBook
        '
        Me.txtBullBook.CalculationExpression = Nothing
        Me.txtBullBook.FieldCode = Nothing
        Me.txtBullBook.FieldDesc = Nothing
        Me.txtBullBook.FieldMaxLength = 0
        Me.txtBullBook.FieldName = Nothing
        Me.txtBullBook.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBullBook.isCalculatedField = False
        Me.txtBullBook.IsSourceFromTable = False
        Me.txtBullBook.IsSourceFromValueList = False
        Me.txtBullBook.IsUnique = False
        Me.txtBullBook.Location = New System.Drawing.Point(155, 225)
        Me.txtBullBook.MaxLength = 200
        Me.txtBullBook.MendatroryField = True
        Me.txtBullBook.MyLinkLable1 = Me.lblBullBookValue2
        Me.txtBullBook.MyLinkLable2 = Nothing
        Me.txtBullBook.Name = "txtBullBook"
        Me.txtBullBook.ReferenceFieldDesc = Nothing
        Me.txtBullBook.ReferenceFieldName = Nothing
        Me.txtBullBook.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtBullBook.RootElement.StretchVertically = True
        Me.txtBullBook.Size = New System.Drawing.Size(238, 18)
        Me.txtBullBook.TabIndex = 493
        '
        'txtBullRFID
        '
        Me.txtBullRFID.CalculationExpression = Nothing
        Me.txtBullRFID.FieldCode = Nothing
        Me.txtBullRFID.FieldDesc = Nothing
        Me.txtBullRFID.FieldMaxLength = 0
        Me.txtBullRFID.FieldName = Nothing
        Me.txtBullRFID.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBullRFID.isCalculatedField = False
        Me.txtBullRFID.IsSourceFromTable = False
        Me.txtBullRFID.IsSourceFromValueList = False
        Me.txtBullRFID.IsUnique = False
        Me.txtBullRFID.Location = New System.Drawing.Point(155, 149)
        Me.txtBullRFID.MaxLength = 200
        Me.txtBullRFID.MendatroryField = True
        Me.txtBullRFID.MyLinkLable1 = Me.lblBullRFID2
        Me.txtBullRFID.MyLinkLable2 = Nothing
        Me.txtBullRFID.Name = "txtBullRFID"
        Me.txtBullRFID.ReferenceFieldDesc = Nothing
        Me.txtBullRFID.ReferenceFieldName = Nothing
        Me.txtBullRFID.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtBullRFID.RootElement.StretchVertically = True
        Me.txtBullRFID.Size = New System.Drawing.Size(238, 18)
        Me.txtBullRFID.TabIndex = 492
        '
        'TXTExoticBloodPer
        '
        Me.TXTExoticBloodPer.CalculationExpression = Nothing
        Me.TXTExoticBloodPer.FieldCode = Nothing
        Me.TXTExoticBloodPer.FieldDesc = Nothing
        Me.TXTExoticBloodPer.FieldMaxLength = 0
        Me.TXTExoticBloodPer.FieldName = Nothing
        Me.TXTExoticBloodPer.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTExoticBloodPer.isCalculatedField = False
        Me.TXTExoticBloodPer.IsSourceFromTable = False
        Me.TXTExoticBloodPer.IsSourceFromValueList = False
        Me.TXTExoticBloodPer.IsUnique = False
        Me.TXTExoticBloodPer.Location = New System.Drawing.Point(155, 122)
        Me.TXTExoticBloodPer.MaxLength = 200
        Me.TXTExoticBloodPer.MendatroryField = True
        Me.TXTExoticBloodPer.MyLinkLable1 = Me.lblExotic2
        Me.TXTExoticBloodPer.MyLinkLable2 = Nothing
        Me.TXTExoticBloodPer.Name = "TXTExoticBloodPer"
        Me.TXTExoticBloodPer.ReferenceFieldDesc = Nothing
        Me.TXTExoticBloodPer.ReferenceFieldName = Nothing
        Me.TXTExoticBloodPer.ReferenceTableName = Nothing
        '
        '
        '
        Me.TXTExoticBloodPer.RootElement.StretchVertically = True
        Me.TXTExoticBloodPer.Size = New System.Drawing.Size(238, 20)
        Me.TXTExoticBloodPer.TabIndex = 491
        '
        'fndCounty
        '
        Me.fndCounty.CalculationExpression = Nothing
        Me.fndCounty.FieldCode = Nothing
        Me.fndCounty.FieldDesc = Nothing
        Me.fndCounty.FieldMaxLength = 0
        Me.fndCounty.FieldName = Nothing
        Me.fndCounty.isCalculatedField = False
        Me.fndCounty.IsSourceFromTable = False
        Me.fndCounty.IsSourceFromValueList = False
        Me.fndCounty.IsUnique = False
        Me.fndCounty.Location = New System.Drawing.Point(155, 280)
        Me.fndCounty.MendatroryField = True
        Me.fndCounty.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndCounty.MyLinkLable1 = Me.lblCountryCode2
        Me.fndCounty.MyLinkLable2 = Nothing
        Me.fndCounty.MyReadOnly = False
        Me.fndCounty.MyShowMasterFormButton = False
        Me.fndCounty.Name = "fndCounty"
        Me.fndCounty.ReferenceFieldDesc = Nothing
        Me.fndCounty.ReferenceFieldName = Nothing
        Me.fndCounty.ReferenceTableName = Nothing
        Me.fndCounty.Size = New System.Drawing.Size(238, 18)
        Me.fndCounty.TabIndex = 490
        Me.fndCounty.Value = ""
        '
        'fndBullSourcePainting
        '
        Me.fndBullSourcePainting.CalculationExpression = Nothing
        Me.fndBullSourcePainting.FieldCode = Nothing
        Me.fndBullSourcePainting.FieldDesc = Nothing
        Me.fndBullSourcePainting.FieldMaxLength = 0
        Me.fndBullSourcePainting.FieldName = Nothing
        Me.fndBullSourcePainting.isCalculatedField = False
        Me.fndBullSourcePainting.IsSourceFromTable = False
        Me.fndBullSourcePainting.IsSourceFromValueList = False
        Me.fndBullSourcePainting.IsUnique = False
        Me.fndBullSourcePainting.Location = New System.Drawing.Point(627, 197)
        Me.fndBullSourcePainting.MendatroryField = True
        Me.fndBullSourcePainting.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndBullSourcePainting.MyLinkLable1 = Me.lblBullSourcePainting2
        Me.fndBullSourcePainting.MyLinkLable2 = Nothing
        Me.fndBullSourcePainting.MyReadOnly = False
        Me.fndBullSourcePainting.MyShowMasterFormButton = False
        Me.fndBullSourcePainting.Name = "fndBullSourcePainting"
        Me.fndBullSourcePainting.ReferenceFieldDesc = Nothing
        Me.fndBullSourcePainting.ReferenceFieldName = Nothing
        Me.fndBullSourcePainting.ReferenceTableName = Nothing
        Me.fndBullSourcePainting.Size = New System.Drawing.Size(228, 18)
        Me.fndBullSourcePainting.TabIndex = 489
        Me.fndBullSourcePainting.Value = ""
        '
        'fndBullRating
        '
        Me.fndBullRating.CalculationExpression = Nothing
        Me.fndBullRating.FieldCode = Nothing
        Me.fndBullRating.FieldDesc = Nothing
        Me.fndBullRating.FieldMaxLength = 0
        Me.fndBullRating.FieldName = Nothing
        Me.fndBullRating.isCalculatedField = False
        Me.fndBullRating.IsSourceFromTable = False
        Me.fndBullRating.IsSourceFromValueList = False
        Me.fndBullRating.IsUnique = False
        Me.fndBullRating.Location = New System.Drawing.Point(627, 145)
        Me.fndBullRating.MendatroryField = True
        Me.fndBullRating.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndBullRating.MyLinkLable1 = Me.lblBullRating2
        Me.fndBullRating.MyLinkLable2 = Nothing
        Me.fndBullRating.MyReadOnly = False
        Me.fndBullRating.MyShowMasterFormButton = False
        Me.fndBullRating.Name = "fndBullRating"
        Me.fndBullRating.ReferenceFieldDesc = Nothing
        Me.fndBullRating.ReferenceFieldName = Nothing
        Me.fndBullRating.ReferenceTableName = Nothing
        Me.fndBullRating.Size = New System.Drawing.Size(227, 18)
        Me.fndBullRating.TabIndex = 488
        Me.fndBullRating.Value = ""
        '
        'RadMenu1
        '
        Me.RadMenu1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(975, 20)
        Me.RadMenu1.TabIndex = 487
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2, Me.RadMenuItem4})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        Me.RadMenuItem1.UseCompatibleTextRendering = False
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Export"
        Me.RadMenuItem2.UseCompatibleTextRendering = False
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Import"
        Me.RadMenuItem4.UseCompatibleTextRendering = False
        '
        'TXTEndDate
        '
        Me.TXTEndDate.CalculationExpression = Nothing
        Me.TXTEndDate.CustomFormat = "dd/MM/yyyy"
        Me.TXTEndDate.FieldCode = Nothing
        Me.TXTEndDate.FieldDesc = Nothing
        Me.TXTEndDate.FieldMaxLength = 0
        Me.TXTEndDate.FieldName = Nothing
        Me.TXTEndDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.TXTEndDate.isCalculatedField = False
        Me.TXTEndDate.IsSourceFromTable = False
        Me.TXTEndDate.IsSourceFromValueList = False
        Me.TXTEndDate.IsUnique = False
        Me.TXTEndDate.Location = New System.Drawing.Point(625, 321)
        Me.TXTEndDate.MendatroryField = False
        Me.TXTEndDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.TXTEndDate.MyLinkLable1 = Me.lblExitDate2
        Me.TXTEndDate.MyLinkLable2 = Nothing
        Me.TXTEndDate.Name = "TXTEndDate"
        Me.TXTEndDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.TXTEndDate.ReferenceFieldDesc = Nothing
        Me.TXTEndDate.ReferenceFieldName = Nothing
        Me.TXTEndDate.ReferenceTableName = Nothing
        Me.TXTEndDate.Size = New System.Drawing.Size(160, 18)
        Me.TXTEndDate.TabIndex = 486
        Me.TXTEndDate.TabStop = False
        Me.TXTEndDate.Text = "13/06/2011"
        Me.TXTEndDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'btnnew
        '
        Me.btnnew.Image = CType(resources.GetObject("btnnew.Image"), System.Drawing.Image)
        Me.btnnew.Location = New System.Drawing.Point(293, 24)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(20, 21)
        Me.btnnew.TabIndex = 485
        '
        'txtRegDate
        '
        Me.txtRegDate.CalculationExpression = Nothing
        Me.txtRegDate.CustomFormat = "dd/MM/yyyy"
        Me.txtRegDate.FieldCode = Nothing
        Me.txtRegDate.FieldDesc = Nothing
        Me.txtRegDate.FieldMaxLength = 0
        Me.txtRegDate.FieldName = Nothing
        Me.txtRegDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRegDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtRegDate.isCalculatedField = False
        Me.txtRegDate.IsSourceFromTable = False
        Me.txtRegDate.IsSourceFromValueList = False
        Me.txtRegDate.IsUnique = False
        Me.txtRegDate.Location = New System.Drawing.Point(155, 173)
        Me.txtRegDate.MendatroryField = False
        Me.txtRegDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtRegDate.MyLinkLable1 = Me.lblRegistrationDate
        Me.txtRegDate.MyLinkLable2 = Nothing
        Me.txtRegDate.Name = "txtRegDate"
        Me.txtRegDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtRegDate.ReferenceFieldDesc = Nothing
        Me.txtRegDate.ReferenceFieldName = Nothing
        Me.txtRegDate.ReferenceTableName = Nothing
        Me.txtRegDate.Size = New System.Drawing.Size(129, 18)
        Me.txtRegDate.TabIndex = 484
        Me.txtRegDate.TabStop = False
        Me.txtRegDate.Text = "13/06/2011"
        Me.txtRegDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'txtStatusDateChanged
        '
        Me.txtStatusDateChanged.CalculationExpression = Nothing
        Me.txtStatusDateChanged.CustomFormat = "dd/MM/yyyy"
        Me.txtStatusDateChanged.FieldCode = Nothing
        Me.txtStatusDateChanged.FieldDesc = Nothing
        Me.txtStatusDateChanged.FieldMaxLength = 0
        Me.txtStatusDateChanged.FieldName = Nothing
        Me.txtStatusDateChanged.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStatusDateChanged.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtStatusDateChanged.isCalculatedField = False
        Me.txtStatusDateChanged.IsSourceFromTable = False
        Me.txtStatusDateChanged.IsSourceFromValueList = False
        Me.txtStatusDateChanged.IsUnique = False
        Me.txtStatusDateChanged.Location = New System.Drawing.Point(155, 361)
        Me.txtStatusDateChanged.MendatroryField = False
        Me.txtStatusDateChanged.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtStatusDateChanged.MyLinkLable1 = Me.lblStatusChangedDate2
        Me.txtStatusDateChanged.MyLinkLable2 = Nothing
        Me.txtStatusDateChanged.Name = "txtStatusDateChanged"
        Me.txtStatusDateChanged.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtStatusDateChanged.ReferenceFieldDesc = Nothing
        Me.txtStatusDateChanged.ReferenceFieldName = Nothing
        Me.txtStatusDateChanged.ReferenceTableName = Nothing
        Me.txtStatusDateChanged.Size = New System.Drawing.Size(138, 18)
        Me.txtStatusDateChanged.TabIndex = 483
        Me.txtStatusDateChanged.TabStop = False
        Me.txtStatusDateChanged.Text = "13/06/2011"
        Me.txtStatusDateChanged.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'fndPenId
        '
        Me.fndPenId.CalculationExpression = Nothing
        Me.fndPenId.FieldCode = Nothing
        Me.fndPenId.FieldDesc = Nothing
        Me.fndPenId.FieldMaxLength = 0
        Me.fndPenId.FieldName = Nothing
        Me.fndPenId.isCalculatedField = False
        Me.fndPenId.IsSourceFromTable = False
        Me.fndPenId.IsSourceFromValueList = False
        Me.fndPenId.IsUnique = False
        Me.fndPenId.Location = New System.Drawing.Point(627, 121)
        Me.fndPenId.MendatroryField = True
        Me.fndPenId.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndPenId.MyLinkLable1 = Me.lblPenId2
        Me.fndPenId.MyLinkLable2 = Nothing
        Me.fndPenId.MyReadOnly = False
        Me.fndPenId.MyShowMasterFormButton = False
        Me.fndPenId.Name = "fndPenId"
        Me.fndPenId.ReferenceFieldDesc = Nothing
        Me.fndPenId.ReferenceFieldName = Nothing
        Me.fndPenId.ReferenceTableName = Nothing
        Me.fndPenId.Size = New System.Drawing.Size(227, 18)
        Me.fndPenId.TabIndex = 399
        Me.fndPenId.Value = ""
        '
        'fndShedId
        '
        Me.fndShedId.CalculationExpression = Nothing
        Me.fndShedId.FieldCode = Nothing
        Me.fndShedId.FieldDesc = Nothing
        Me.fndShedId.FieldMaxLength = 0
        Me.fndShedId.FieldName = Nothing
        Me.fndShedId.isCalculatedField = False
        Me.fndShedId.IsSourceFromTable = False
        Me.fndShedId.IsSourceFromValueList = False
        Me.fndShedId.IsUnique = False
        Me.fndShedId.Location = New System.Drawing.Point(628, 95)
        Me.fndShedId.MendatroryField = True
        Me.fndShedId.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndShedId.MyLinkLable1 = Me.lblShedId2
        Me.fndShedId.MyLinkLable2 = Nothing
        Me.fndShedId.MyReadOnly = False
        Me.fndShedId.MyShowMasterFormButton = False
        Me.fndShedId.Name = "fndShedId"
        Me.fndShedId.ReferenceFieldDesc = Nothing
        Me.fndShedId.ReferenceFieldName = Nothing
        Me.fndShedId.ReferenceTableName = Nothing
        Me.fndShedId.Size = New System.Drawing.Size(226, 18)
        Me.fndShedId.TabIndex = 397
        Me.fndShedId.Value = ""
        '
        'fndSSCentre
        '
        Me.fndSSCentre.CalculationExpression = Nothing
        Me.fndSSCentre.FieldCode = Nothing
        Me.fndSSCentre.FieldDesc = Nothing
        Me.fndSSCentre.FieldMaxLength = 0
        Me.fndSSCentre.FieldName = Nothing
        Me.fndSSCentre.isCalculatedField = False
        Me.fndSSCentre.IsSourceFromTable = False
        Me.fndSSCentre.IsSourceFromValueList = False
        Me.fndSSCentre.IsUnique = False
        Me.fndSSCentre.Location = New System.Drawing.Point(628, 70)
        Me.fndSSCentre.MendatroryField = True
        Me.fndSSCentre.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndSSCentre.MyLinkLable1 = Me.lblSScentre2
        Me.fndSSCentre.MyLinkLable2 = Nothing
        Me.fndSSCentre.MyReadOnly = False
        Me.fndSSCentre.MyShowMasterFormButton = False
        Me.fndSSCentre.Name = "fndSSCentre"
        Me.fndSSCentre.ReferenceFieldDesc = Nothing
        Me.fndSSCentre.ReferenceFieldName = Nothing
        Me.fndSSCentre.ReferenceTableName = Nothing
        Me.fndSSCentre.Size = New System.Drawing.Size(226, 18)
        Me.fndSSCentre.TabIndex = 395
        Me.fndSSCentre.Value = ""
        '
        'fndSubStatus
        '
        Me.fndSubStatus.CalculationExpression = Nothing
        Me.fndSubStatus.FieldCode = Nothing
        Me.fndSubStatus.FieldDesc = Nothing
        Me.fndSubStatus.FieldMaxLength = 0
        Me.fndSubStatus.FieldName = Nothing
        Me.fndSubStatus.isCalculatedField = False
        Me.fndSubStatus.IsSourceFromTable = False
        Me.fndSubStatus.IsSourceFromValueList = False
        Me.fndSubStatus.IsUnique = False
        Me.fndSubStatus.Location = New System.Drawing.Point(626, 296)
        Me.fndSubStatus.MendatroryField = True
        Me.fndSubStatus.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndSubStatus.MyLinkLable1 = Me.lblBullSubStatus2
        Me.fndSubStatus.MyLinkLable2 = Nothing
        Me.fndSubStatus.MyReadOnly = False
        Me.fndSubStatus.MyShowMasterFormButton = False
        Me.fndSubStatus.Name = "fndSubStatus"
        Me.fndSubStatus.ReferenceFieldDesc = Nothing
        Me.fndSubStatus.ReferenceFieldName = Nothing
        Me.fndSubStatus.ReferenceTableName = Nothing
        Me.fndSubStatus.Size = New System.Drawing.Size(228, 18)
        Me.fndSubStatus.TabIndex = 389
        Me.fndSubStatus.Value = ""
        '
        'fndBullStatus
        '
        Me.fndBullStatus.CalculationExpression = Nothing
        Me.fndBullStatus.FieldCode = Nothing
        Me.fndBullStatus.FieldDesc = Nothing
        Me.fndBullStatus.FieldMaxLength = 0
        Me.fndBullStatus.FieldName = Nothing
        Me.fndBullStatus.isCalculatedField = False
        Me.fndBullStatus.IsSourceFromTable = False
        Me.fndBullStatus.IsSourceFromValueList = False
        Me.fndBullStatus.IsUnique = False
        Me.fndBullStatus.Location = New System.Drawing.Point(626, 271)
        Me.fndBullStatus.MendatroryField = True
        Me.fndBullStatus.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndBullStatus.MyLinkLable1 = Me.lblBullStatus2
        Me.fndBullStatus.MyLinkLable2 = Nothing
        Me.fndBullStatus.MyReadOnly = False
        Me.fndBullStatus.MyShowMasterFormButton = False
        Me.fndBullStatus.Name = "fndBullStatus"
        Me.fndBullStatus.ReferenceFieldDesc = Nothing
        Me.fndBullStatus.ReferenceFieldName = Nothing
        Me.fndBullStatus.ReferenceTableName = Nothing
        Me.fndBullStatus.Size = New System.Drawing.Size(228, 18)
        Me.fndBullStatus.TabIndex = 385
        Me.fndBullStatus.Value = ""
        '
        'fndBreed
        '
        Me.fndBreed.CalculationExpression = Nothing
        Me.fndBreed.FieldCode = Nothing
        Me.fndBreed.FieldDesc = Nothing
        Me.fndBreed.FieldMaxLength = 0
        Me.fndBreed.FieldName = Nothing
        Me.fndBreed.isCalculatedField = False
        Me.fndBreed.IsSourceFromTable = False
        Me.fndBreed.IsSourceFromValueList = False
        Me.fndBreed.IsUnique = False
        Me.fndBreed.Location = New System.Drawing.Point(155, 253)
        Me.fndBreed.MendatroryField = True
        Me.fndBreed.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndBreed.MyLinkLable1 = Me.lblBreed
        Me.fndBreed.MyLinkLable2 = Nothing
        Me.fndBreed.MyReadOnly = False
        Me.fndBreed.MyShowMasterFormButton = False
        Me.fndBreed.Name = "fndBreed"
        Me.fndBreed.ReferenceFieldDesc = Nothing
        Me.fndBreed.ReferenceFieldName = Nothing
        Me.fndBreed.ReferenceTableName = Nothing
        Me.fndBreed.Size = New System.Drawing.Size(238, 18)
        Me.fndBreed.TabIndex = 377
        Me.fndBreed.Value = ""
        '
        'fndSubCategory
        '
        Me.fndSubCategory.CalculationExpression = Nothing
        Me.fndSubCategory.FieldCode = Nothing
        Me.fndSubCategory.FieldDesc = Nothing
        Me.fndSubCategory.FieldMaxLength = 0
        Me.fndSubCategory.FieldName = Nothing
        Me.fndSubCategory.isCalculatedField = False
        Me.fndSubCategory.IsSourceFromTable = False
        Me.fndSubCategory.IsSourceFromValueList = False
        Me.fndSubCategory.IsUnique = False
        Me.fndSubCategory.Location = New System.Drawing.Point(155, 307)
        Me.fndSubCategory.MendatroryField = True
        Me.fndSubCategory.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndSubCategory.MyLinkLable1 = Me.lblSubCategory2
        Me.fndSubCategory.MyLinkLable2 = Nothing
        Me.fndSubCategory.MyReadOnly = False
        Me.fndSubCategory.MyShowMasterFormButton = False
        Me.fndSubCategory.Name = "fndSubCategory"
        Me.fndSubCategory.ReferenceFieldDesc = Nothing
        Me.fndSubCategory.ReferenceFieldName = Nothing
        Me.fndSubCategory.ReferenceTableName = Nothing
        Me.fndSubCategory.Size = New System.Drawing.Size(238, 18)
        Me.fndSubCategory.TabIndex = 372
        Me.fndSubCategory.Value = ""
        '
        'fndCategory
        '
        Me.fndCategory.CalculationExpression = Nothing
        Me.fndCategory.FieldCode = Nothing
        Me.fndCategory.FieldDesc = Nothing
        Me.fndCategory.FieldMaxLength = 0
        Me.fndCategory.FieldName = Nothing
        Me.fndCategory.isCalculatedField = False
        Me.fndCategory.IsSourceFromTable = False
        Me.fndCategory.IsSourceFromValueList = False
        Me.fndCategory.IsUnique = False
        Me.fndCategory.Location = New System.Drawing.Point(155, 197)
        Me.fndCategory.MendatroryField = True
        Me.fndCategory.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndCategory.MyLinkLable1 = Me.lblCategory2
        Me.fndCategory.MyLinkLable2 = Nothing
        Me.fndCategory.MyReadOnly = False
        Me.fndCategory.MyShowMasterFormButton = False
        Me.fndCategory.Name = "fndCategory"
        Me.fndCategory.ReferenceFieldDesc = Nothing
        Me.fndCategory.ReferenceFieldName = Nothing
        Me.fndCategory.ReferenceTableName = Nothing
        Me.fndCategory.Size = New System.Drawing.Size(238, 18)
        Me.fndCategory.TabIndex = 369
        Me.fndCategory.Value = ""
        '
        'fndSpecies
        '
        Me.fndSpecies.CalculationExpression = Nothing
        Me.fndSpecies.FieldCode = Nothing
        Me.fndSpecies.FieldDesc = Nothing
        Me.fndSpecies.FieldMaxLength = 0
        Me.fndSpecies.FieldName = Nothing
        Me.fndSpecies.isCalculatedField = False
        Me.fndSpecies.IsSourceFromTable = False
        Me.fndSpecies.IsSourceFromValueList = False
        Me.fndSpecies.IsUnique = False
        Me.fndSpecies.Location = New System.Drawing.Point(155, 95)
        Me.fndSpecies.MendatroryField = True
        Me.fndSpecies.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndSpecies.MyLinkLable1 = Me.lblspecies2
        Me.fndSpecies.MyLinkLable2 = Nothing
        Me.fndSpecies.MyReadOnly = False
        Me.fndSpecies.MyShowMasterFormButton = False
        Me.fndSpecies.Name = "fndSpecies"
        Me.fndSpecies.ReferenceFieldDesc = Nothing
        Me.fndSpecies.ReferenceFieldName = Nothing
        Me.fndSpecies.ReferenceTableName = Nothing
        Me.fndSpecies.Size = New System.Drawing.Size(238, 19)
        Me.fndSpecies.TabIndex = 366
        Me.fndSpecies.Value = ""
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Location = New System.Drawing.Point(154, 76)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(40, 17)
        Me.RadioButton2.TabIndex = 66
        Me.RadioButton2.TabStop = True
        Me.RadioButton2.Text = "Yes"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Location = New System.Drawing.Point(310, 76)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(40, 17)
        Me.RadioButton1.TabIndex = 65
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "No"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'fndCode
        '
        Me.fndCode.FieldName = Nothing
        Me.fndCode.Location = New System.Drawing.Point(64, 24)
        Me.fndCode.MendatroryField = True
        Me.fndCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndCode.MyLinkLable1 = Nothing
        Me.fndCode.MyLinkLable2 = Me.lblcode2
        Me.fndCode.MyMaxLength = 30
        Me.fndCode.MyReadOnly = False
        Me.fndCode.Name = "fndCode"
        Me.fndCode.Size = New System.Drawing.Size(229, 21)
        Me.fndCode.TabIndex = 62
        Me.fndCode.TabStop = False
        Me.fndCode.Value = ""
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.GroupBox1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(96.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(975, 422)
        Me.RadPageViewPage2.Text = "Bull Information"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtLastDateBreeding)
        Me.GroupBox1.Controls.Add(Me.lblPurchaseDate2)
        Me.GroupBox1.Controls.Add(Me.lblPurchaseRequestDate2)
        Me.GroupBox1.Controls.Add(Me.lblSourceName2)
        Me.GroupBox1.Controls.Add(Me.lblBreedingValue2)
        Me.GroupBox1.Controls.Add(Me.lblLastdayBreeding)
        Me.GroupBox1.Controls.Add(Me.lblCapacityOfAverageMonthlyDoses)
        Me.GroupBox1.Controls.Add(Me.lblDosesProducedTillDate2)
        Me.GroupBox1.Controls.Add(Me.lblSemenPrice2)
        Me.GroupBox1.Controls.Add(Me.lblFirstCollectionDate2)
        Me.GroupBox1.Controls.Add(Me.lblOwnerOfBull2)
        Me.GroupBox1.Controls.Add(Me.lblSourceAddress2)
        Me.GroupBox1.Controls.Add(Me.lblPurchaseRequestNo2)
        Me.GroupBox1.Controls.Add(Me.txtProducedTillDate)
        Me.GroupBox1.Controls.Add(Me.txtPurchaseDate)
        Me.GroupBox1.Controls.Add(Me.txtPurchaseRequestDate)
        Me.GroupBox1.Controls.Add(Me.txtPurchaseNo)
        Me.GroupBox1.Controls.Add(Me.txtBreedingValue)
        Me.GroupBox1.Controls.Add(Me.txtAveregeDoses)
        Me.GroupBox1.Controls.Add(Me.txtFirstCollectionDate)
        Me.GroupBox1.Controls.Add(Me.txtSemenPrice)
        Me.GroupBox1.Controls.Add(Me.txtOwnerName)
        Me.GroupBox1.Controls.Add(Me.txtSourceName)
        Me.GroupBox1.Controls.Add(Me.fndSourceName)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 17)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(965, 356)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'txtLastDateBreeding
        '
        Me.txtLastDateBreeding.CalculationExpression = Nothing
        Me.txtLastDateBreeding.CustomFormat = "dd/MM/yyyy"
        Me.txtLastDateBreeding.FieldCode = Nothing
        Me.txtLastDateBreeding.FieldDesc = Nothing
        Me.txtLastDateBreeding.FieldMaxLength = 0
        Me.txtLastDateBreeding.FieldName = Nothing
        Me.txtLastDateBreeding.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLastDateBreeding.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtLastDateBreeding.isCalculatedField = False
        Me.txtLastDateBreeding.IsSourceFromTable = False
        Me.txtLastDateBreeding.IsSourceFromValueList = False
        Me.txtLastDateBreeding.IsUnique = False
        Me.txtLastDateBreeding.Location = New System.Drawing.Point(212, 215)
        Me.txtLastDateBreeding.MendatroryField = False
        Me.txtLastDateBreeding.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtLastDateBreeding.MyLinkLable1 = Me.lblLastdayBreeding
        Me.txtLastDateBreeding.MyLinkLable2 = Nothing
        Me.txtLastDateBreeding.Name = "txtLastDateBreeding"
        Me.txtLastDateBreeding.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtLastDateBreeding.ReferenceFieldDesc = Nothing
        Me.txtLastDateBreeding.ReferenceFieldName = Nothing
        Me.txtLastDateBreeding.ReferenceTableName = Nothing
        Me.txtLastDateBreeding.Size = New System.Drawing.Size(155, 18)
        Me.txtLastDateBreeding.TabIndex = 493
        Me.txtLastDateBreeding.TabStop = False
        Me.txtLastDateBreeding.Text = "13/06/2011"
        Me.txtLastDateBreeding.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'lblLastdayBreeding
        '
        Me.lblLastdayBreeding.FieldName = Nothing
        Me.lblLastdayBreeding.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblLastdayBreeding.Location = New System.Drawing.Point(11, 218)
        Me.lblLastdayBreeding.Name = "lblLastdayBreeding"
        Me.lblLastdayBreeding.Size = New System.Drawing.Size(120, 16)
        Me.lblLastdayBreeding.TabIndex = 531
        Me.lblLastdayBreeding.Text = "Last Date for Breading"
        '
        'lblPurchaseDate2
        '
        Me.lblPurchaseDate2.FieldName = Nothing
        Me.lblPurchaseDate2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblPurchaseDate2.Location = New System.Drawing.Point(10, 329)
        Me.lblPurchaseDate2.Name = "lblPurchaseDate2"
        Me.lblPurchaseDate2.Size = New System.Drawing.Size(81, 16)
        Me.lblPurchaseDate2.TabIndex = 534
        Me.lblPurchaseDate2.Text = "Purchase Date"
        '
        'lblPurchaseRequestDate2
        '
        Me.lblPurchaseRequestDate2.FieldName = Nothing
        Me.lblPurchaseRequestDate2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblPurchaseRequestDate2.Location = New System.Drawing.Point(6, 302)
        Me.lblPurchaseRequestDate2.Name = "lblPurchaseRequestDate2"
        Me.lblPurchaseRequestDate2.Size = New System.Drawing.Size(126, 16)
        Me.lblPurchaseRequestDate2.TabIndex = 533
        Me.lblPurchaseRequestDate2.Text = "Purchase Request Date"
        '
        'lblSourceName2
        '
        Me.lblSourceName2.FieldName = Nothing
        Me.lblSourceName2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblSourceName2.Location = New System.Drawing.Point(10, 275)
        Me.lblSourceName2.Name = "lblSourceName2"
        Me.lblSourceName2.Size = New System.Drawing.Size(75, 16)
        Me.lblSourceName2.TabIndex = 532
        Me.lblSourceName2.Text = "Source Name"
        '
        'lblBreedingValue2
        '
        Me.lblBreedingValue2.FieldName = Nothing
        Me.lblBreedingValue2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblBreedingValue2.Location = New System.Drawing.Point(9, 247)
        Me.lblBreedingValue2.Name = "lblBreedingValue2"
        Me.lblBreedingValue2.Size = New System.Drawing.Size(84, 16)
        Me.lblBreedingValue2.TabIndex = 532
        Me.lblBreedingValue2.Text = "Breeding Value"
        '
        'lblCapacityOfAverageMonthlyDoses
        '
        Me.lblCapacityOfAverageMonthlyDoses.FieldName = Nothing
        Me.lblCapacityOfAverageMonthlyDoses.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblCapacityOfAverageMonthlyDoses.Location = New System.Drawing.Point(9, 192)
        Me.lblCapacityOfAverageMonthlyDoses.Name = "lblCapacityOfAverageMonthlyDoses"
        Me.lblCapacityOfAverageMonthlyDoses.Size = New System.Drawing.Size(189, 16)
        Me.lblCapacityOfAverageMonthlyDoses.TabIndex = 530
        Me.lblCapacityOfAverageMonthlyDoses.Text = "Capacity Of Average Monthly Doses"
        '
        'lblDosesProducedTillDate2
        '
        Me.lblDosesProducedTillDate2.FieldName = Nothing
        Me.lblDosesProducedTillDate2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblDosesProducedTillDate2.Location = New System.Drawing.Point(9, 165)
        Me.lblDosesProducedTillDate2.Name = "lblDosesProducedTillDate2"
        Me.lblDosesProducedTillDate2.Size = New System.Drawing.Size(135, 16)
        Me.lblDosesProducedTillDate2.TabIndex = 530
        Me.lblDosesProducedTillDate2.Text = "Doses Produced Till Date"
        '
        'lblSemenPrice2
        '
        Me.lblSemenPrice2.FieldName = Nothing
        Me.lblSemenPrice2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblSemenPrice2.Location = New System.Drawing.Point(9, 135)
        Me.lblSemenPrice2.Name = "lblSemenPrice2"
        Me.lblSemenPrice2.Size = New System.Drawing.Size(124, 16)
        Me.lblSemenPrice2.TabIndex = 529
        Me.lblSemenPrice2.Text = "Semen Price Per Straw"
        '
        'lblFirstCollectionDate2
        '
        Me.lblFirstCollectionDate2.FieldName = Nothing
        Me.lblFirstCollectionDate2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblFirstCollectionDate2.Location = New System.Drawing.Point(9, 107)
        Me.lblFirstCollectionDate2.Name = "lblFirstCollectionDate2"
        Me.lblFirstCollectionDate2.Size = New System.Drawing.Size(108, 16)
        Me.lblFirstCollectionDate2.TabIndex = 528
        Me.lblFirstCollectionDate2.Text = "First Collection Date"
        '
        'lblOwnerOfBull2
        '
        Me.lblOwnerOfBull2.FieldName = Nothing
        Me.lblOwnerOfBull2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblOwnerOfBull2.Location = New System.Drawing.Point(9, 81)
        Me.lblOwnerOfBull2.Name = "lblOwnerOfBull2"
        Me.lblOwnerOfBull2.Size = New System.Drawing.Size(77, 16)
        Me.lblOwnerOfBull2.TabIndex = 527
        Me.lblOwnerOfBull2.Text = "Owner Of Bull"
        '
        'lblSourceAddress2
        '
        Me.lblSourceAddress2.FieldName = Nothing
        Me.lblSourceAddress2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblSourceAddress2.Location = New System.Drawing.Point(9, 54)
        Me.lblSourceAddress2.Name = "lblSourceAddress2"
        Me.lblSourceAddress2.Size = New System.Drawing.Size(87, 16)
        Me.lblSourceAddress2.TabIndex = 526
        Me.lblSourceAddress2.Text = "Source Address"
        '
        'lblPurchaseRequestNo2
        '
        Me.lblPurchaseRequestNo2.FieldName = Nothing
        Me.lblPurchaseRequestNo2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblPurchaseRequestNo2.Location = New System.Drawing.Point(9, 26)
        Me.lblPurchaseRequestNo2.Name = "lblPurchaseRequestNo2"
        Me.lblPurchaseRequestNo2.Size = New System.Drawing.Size(168, 16)
        Me.lblPurchaseRequestNo2.TabIndex = 525
        Me.lblPurchaseRequestNo2.Text = "Purchase Request No.(Unique*)"
        '
        'txtProducedTillDate
        '
        Me.txtProducedTillDate.CalculationExpression = Nothing
        Me.txtProducedTillDate.FieldCode = Nothing
        Me.txtProducedTillDate.FieldDesc = Nothing
        Me.txtProducedTillDate.FieldMaxLength = 0
        Me.txtProducedTillDate.FieldName = Nothing
        Me.txtProducedTillDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProducedTillDate.isCalculatedField = False
        Me.txtProducedTillDate.IsSourceFromTable = False
        Me.txtProducedTillDate.IsSourceFromValueList = False
        Me.txtProducedTillDate.IsUnique = False
        Me.txtProducedTillDate.Location = New System.Drawing.Point(212, 161)
        Me.txtProducedTillDate.MaxLength = 200
        Me.txtProducedTillDate.MendatroryField = True
        Me.txtProducedTillDate.MyLinkLable1 = Me.lblDosesProducedTillDate2
        Me.txtProducedTillDate.MyLinkLable2 = Nothing
        Me.txtProducedTillDate.Name = "txtProducedTillDate"
        Me.txtProducedTillDate.ReferenceFieldDesc = Nothing
        Me.txtProducedTillDate.ReferenceFieldName = Nothing
        Me.txtProducedTillDate.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtProducedTillDate.RootElement.StretchVertically = True
        Me.txtProducedTillDate.Size = New System.Drawing.Size(155, 20)
        Me.txtProducedTillDate.TabIndex = 498
        '
        'txtPurchaseDate
        '
        Me.txtPurchaseDate.CalculationExpression = Nothing
        Me.txtPurchaseDate.CustomFormat = "dd/MM/yyyy"
        Me.txtPurchaseDate.FieldCode = Nothing
        Me.txtPurchaseDate.FieldDesc = Nothing
        Me.txtPurchaseDate.FieldMaxLength = 0
        Me.txtPurchaseDate.FieldName = Nothing
        Me.txtPurchaseDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPurchaseDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtPurchaseDate.isCalculatedField = False
        Me.txtPurchaseDate.IsSourceFromTable = False
        Me.txtPurchaseDate.IsSourceFromValueList = False
        Me.txtPurchaseDate.IsUnique = False
        Me.txtPurchaseDate.Location = New System.Drawing.Point(212, 329)
        Me.txtPurchaseDate.MendatroryField = False
        Me.txtPurchaseDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtPurchaseDate.MyLinkLable1 = Me.lblPurchaseDate2
        Me.txtPurchaseDate.MyLinkLable2 = Nothing
        Me.txtPurchaseDate.Name = "txtPurchaseDate"
        Me.txtPurchaseDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtPurchaseDate.ReferenceFieldDesc = Nothing
        Me.txtPurchaseDate.ReferenceFieldName = Nothing
        Me.txtPurchaseDate.ReferenceTableName = Nothing
        Me.txtPurchaseDate.Size = New System.Drawing.Size(155, 18)
        Me.txtPurchaseDate.TabIndex = 497
        Me.txtPurchaseDate.TabStop = False
        Me.txtPurchaseDate.Text = "13/06/2011"
        Me.txtPurchaseDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'txtPurchaseRequestDate
        '
        Me.txtPurchaseRequestDate.CalculationExpression = Nothing
        Me.txtPurchaseRequestDate.CustomFormat = "dd/MM/yyyy"
        Me.txtPurchaseRequestDate.FieldCode = Nothing
        Me.txtPurchaseRequestDate.FieldDesc = Nothing
        Me.txtPurchaseRequestDate.FieldMaxLength = 0
        Me.txtPurchaseRequestDate.FieldName = Nothing
        Me.txtPurchaseRequestDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPurchaseRequestDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtPurchaseRequestDate.isCalculatedField = False
        Me.txtPurchaseRequestDate.IsSourceFromTable = False
        Me.txtPurchaseRequestDate.IsSourceFromValueList = False
        Me.txtPurchaseRequestDate.IsUnique = False
        Me.txtPurchaseRequestDate.Location = New System.Drawing.Point(212, 299)
        Me.txtPurchaseRequestDate.MendatroryField = False
        Me.txtPurchaseRequestDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtPurchaseRequestDate.MyLinkLable1 = Me.lblPurchaseRequestDate2
        Me.txtPurchaseRequestDate.MyLinkLable2 = Nothing
        Me.txtPurchaseRequestDate.Name = "txtPurchaseRequestDate"
        Me.txtPurchaseRequestDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtPurchaseRequestDate.ReferenceFieldDesc = Nothing
        Me.txtPurchaseRequestDate.ReferenceFieldName = Nothing
        Me.txtPurchaseRequestDate.ReferenceTableName = Nothing
        Me.txtPurchaseRequestDate.Size = New System.Drawing.Size(155, 18)
        Me.txtPurchaseRequestDate.TabIndex = 496
        Me.txtPurchaseRequestDate.TabStop = False
        Me.txtPurchaseRequestDate.Text = "13/06/2011"
        Me.txtPurchaseRequestDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'txtPurchaseNo
        '
        Me.txtPurchaseNo.CalculationExpression = Nothing
        Me.txtPurchaseNo.FieldCode = Nothing
        Me.txtPurchaseNo.FieldDesc = Nothing
        Me.txtPurchaseNo.FieldMaxLength = 0
        Me.txtPurchaseNo.FieldName = Nothing
        Me.txtPurchaseNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPurchaseNo.isCalculatedField = False
        Me.txtPurchaseNo.IsSourceFromTable = False
        Me.txtPurchaseNo.IsSourceFromValueList = False
        Me.txtPurchaseNo.IsUnique = False
        Me.txtPurchaseNo.Location = New System.Drawing.Point(212, 26)
        Me.txtPurchaseNo.MaxLength = 200
        Me.txtPurchaseNo.MendatroryField = True
        Me.txtPurchaseNo.MyLinkLable1 = Me.lblPurchaseRequestNo2
        Me.txtPurchaseNo.MyLinkLable2 = Nothing
        Me.txtPurchaseNo.Name = "txtPurchaseNo"
        Me.txtPurchaseNo.ReferenceFieldDesc = Nothing
        Me.txtPurchaseNo.ReferenceFieldName = Nothing
        Me.txtPurchaseNo.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtPurchaseNo.RootElement.StretchVertically = True
        Me.txtPurchaseNo.Size = New System.Drawing.Size(155, 20)
        Me.txtPurchaseNo.TabIndex = 495
        '
        'txtBreedingValue
        '
        Me.txtBreedingValue.CalculationExpression = Nothing
        Me.txtBreedingValue.FieldCode = Nothing
        Me.txtBreedingValue.FieldDesc = Nothing
        Me.txtBreedingValue.FieldMaxLength = 0
        Me.txtBreedingValue.FieldName = Nothing
        Me.txtBreedingValue.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBreedingValue.isCalculatedField = False
        Me.txtBreedingValue.IsSourceFromTable = False
        Me.txtBreedingValue.IsSourceFromValueList = False
        Me.txtBreedingValue.IsUnique = False
        Me.txtBreedingValue.Location = New System.Drawing.Point(212, 243)
        Me.txtBreedingValue.MaxLength = 200
        Me.txtBreedingValue.MendatroryField = True
        Me.txtBreedingValue.MyLinkLable1 = Me.lblBreedingValue2
        Me.txtBreedingValue.MyLinkLable2 = Nothing
        Me.txtBreedingValue.Name = "txtBreedingValue"
        Me.txtBreedingValue.ReferenceFieldDesc = Nothing
        Me.txtBreedingValue.ReferenceFieldName = Nothing
        Me.txtBreedingValue.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtBreedingValue.RootElement.StretchVertically = True
        Me.txtBreedingValue.Size = New System.Drawing.Size(155, 20)
        Me.txtBreedingValue.TabIndex = 494
        '
        'txtAveregeDoses
        '
        Me.txtAveregeDoses.CalculationExpression = Nothing
        Me.txtAveregeDoses.FieldCode = Nothing
        Me.txtAveregeDoses.FieldDesc = Nothing
        Me.txtAveregeDoses.FieldMaxLength = 0
        Me.txtAveregeDoses.FieldName = Nothing
        Me.txtAveregeDoses.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAveregeDoses.isCalculatedField = False
        Me.txtAveregeDoses.IsSourceFromTable = False
        Me.txtAveregeDoses.IsSourceFromValueList = False
        Me.txtAveregeDoses.IsUnique = False
        Me.txtAveregeDoses.Location = New System.Drawing.Point(212, 189)
        Me.txtAveregeDoses.MaxLength = 200
        Me.txtAveregeDoses.MendatroryField = True
        Me.txtAveregeDoses.MyLinkLable1 = Me.lblCapacityOfAverageMonthlyDoses
        Me.txtAveregeDoses.MyLinkLable2 = Nothing
        Me.txtAveregeDoses.Name = "txtAveregeDoses"
        Me.txtAveregeDoses.ReferenceFieldDesc = Nothing
        Me.txtAveregeDoses.ReferenceFieldName = Nothing
        Me.txtAveregeDoses.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtAveregeDoses.RootElement.StretchVertically = True
        Me.txtAveregeDoses.Size = New System.Drawing.Size(155, 20)
        Me.txtAveregeDoses.TabIndex = 492
        '
        'txtFirstCollectionDate
        '
        Me.txtFirstCollectionDate.CalculationExpression = Nothing
        Me.txtFirstCollectionDate.CustomFormat = "dd/MM/yyyy"
        Me.txtFirstCollectionDate.FieldCode = Nothing
        Me.txtFirstCollectionDate.FieldDesc = Nothing
        Me.txtFirstCollectionDate.FieldMaxLength = 0
        Me.txtFirstCollectionDate.FieldName = Nothing
        Me.txtFirstCollectionDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFirstCollectionDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFirstCollectionDate.isCalculatedField = False
        Me.txtFirstCollectionDate.IsSourceFromTable = False
        Me.txtFirstCollectionDate.IsSourceFromValueList = False
        Me.txtFirstCollectionDate.IsUnique = False
        Me.txtFirstCollectionDate.Location = New System.Drawing.Point(212, 107)
        Me.txtFirstCollectionDate.MendatroryField = False
        Me.txtFirstCollectionDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFirstCollectionDate.MyLinkLable1 = Me.lblFirstCollectionDate2
        Me.txtFirstCollectionDate.MyLinkLable2 = Nothing
        Me.txtFirstCollectionDate.Name = "txtFirstCollectionDate"
        Me.txtFirstCollectionDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFirstCollectionDate.ReferenceFieldDesc = Nothing
        Me.txtFirstCollectionDate.ReferenceFieldName = Nothing
        Me.txtFirstCollectionDate.ReferenceTableName = Nothing
        Me.txtFirstCollectionDate.Size = New System.Drawing.Size(155, 18)
        Me.txtFirstCollectionDate.TabIndex = 485
        Me.txtFirstCollectionDate.TabStop = False
        Me.txtFirstCollectionDate.Text = "13/06/2011"
        Me.txtFirstCollectionDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'txtSemenPrice
        '
        Me.txtSemenPrice.CalculationExpression = Nothing
        Me.txtSemenPrice.FieldCode = Nothing
        Me.txtSemenPrice.FieldDesc = Nothing
        Me.txtSemenPrice.FieldMaxLength = 0
        Me.txtSemenPrice.FieldName = Nothing
        Me.txtSemenPrice.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSemenPrice.isCalculatedField = False
        Me.txtSemenPrice.IsSourceFromTable = False
        Me.txtSemenPrice.IsSourceFromValueList = False
        Me.txtSemenPrice.IsUnique = False
        Me.txtSemenPrice.Location = New System.Drawing.Point(212, 131)
        Me.txtSemenPrice.MaxLength = 200
        Me.txtSemenPrice.MendatroryField = True
        Me.txtSemenPrice.MyLinkLable1 = Me.lblSemenPrice2
        Me.txtSemenPrice.MyLinkLable2 = Nothing
        Me.txtSemenPrice.Name = "txtSemenPrice"
        Me.txtSemenPrice.ReferenceFieldDesc = Nothing
        Me.txtSemenPrice.ReferenceFieldName = Nothing
        Me.txtSemenPrice.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtSemenPrice.RootElement.StretchVertically = True
        Me.txtSemenPrice.Size = New System.Drawing.Size(155, 20)
        Me.txtSemenPrice.TabIndex = 465
        '
        'txtOwnerName
        '
        Me.txtOwnerName.CalculationExpression = Nothing
        Me.txtOwnerName.FieldCode = Nothing
        Me.txtOwnerName.FieldDesc = Nothing
        Me.txtOwnerName.FieldMaxLength = 0
        Me.txtOwnerName.FieldName = Nothing
        Me.txtOwnerName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOwnerName.isCalculatedField = False
        Me.txtOwnerName.IsSourceFromTable = False
        Me.txtOwnerName.IsSourceFromValueList = False
        Me.txtOwnerName.IsUnique = False
        Me.txtOwnerName.Location = New System.Drawing.Point(212, 81)
        Me.txtOwnerName.MaxLength = 200
        Me.txtOwnerName.MendatroryField = True
        Me.txtOwnerName.MyLinkLable1 = Nothing
        Me.txtOwnerName.MyLinkLable2 = Nothing
        Me.txtOwnerName.Name = "txtOwnerName"
        Me.txtOwnerName.ReferenceFieldDesc = Nothing
        Me.txtOwnerName.ReferenceFieldName = Nothing
        Me.txtOwnerName.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtOwnerName.RootElement.StretchVertically = True
        Me.txtOwnerName.Size = New System.Drawing.Size(155, 20)
        Me.txtOwnerName.TabIndex = 464
        '
        'txtSourceName
        '
        Me.txtSourceName.CalculationExpression = Nothing
        Me.txtSourceName.FieldCode = Nothing
        Me.txtSourceName.FieldDesc = Nothing
        Me.txtSourceName.FieldMaxLength = 0
        Me.txtSourceName.FieldName = Nothing
        Me.txtSourceName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSourceName.isCalculatedField = False
        Me.txtSourceName.IsSourceFromTable = False
        Me.txtSourceName.IsSourceFromValueList = False
        Me.txtSourceName.IsUnique = False
        Me.txtSourceName.Location = New System.Drawing.Point(212, 53)
        Me.txtSourceName.MaxLength = 200
        Me.txtSourceName.MendatroryField = True
        Me.txtSourceName.MyLinkLable1 = Me.lblSourceAddress2
        Me.txtSourceName.MyLinkLable2 = Nothing
        Me.txtSourceName.Name = "txtSourceName"
        Me.txtSourceName.ReferenceFieldDesc = Nothing
        Me.txtSourceName.ReferenceFieldName = Nothing
        Me.txtSourceName.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtSourceName.RootElement.StretchVertically = True
        Me.txtSourceName.Size = New System.Drawing.Size(155, 20)
        Me.txtSourceName.TabIndex = 463
        '
        'fndSourceName
        '
        Me.fndSourceName.CalculationExpression = Nothing
        Me.fndSourceName.FieldCode = Nothing
        Me.fndSourceName.FieldDesc = Nothing
        Me.fndSourceName.FieldMaxLength = 0
        Me.fndSourceName.FieldName = Nothing
        Me.fndSourceName.isCalculatedField = False
        Me.fndSourceName.IsSourceFromTable = False
        Me.fndSourceName.IsSourceFromValueList = False
        Me.fndSourceName.IsUnique = False
        Me.fndSourceName.Location = New System.Drawing.Point(212, 273)
        Me.fndSourceName.MendatroryField = True
        Me.fndSourceName.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndSourceName.MyLinkLable1 = Me.lblSourceName2
        Me.fndSourceName.MyLinkLable2 = Nothing
        Me.fndSourceName.MyReadOnly = False
        Me.fndSourceName.MyShowMasterFormButton = False
        Me.fndSourceName.Name = "fndSourceName"
        Me.fndSourceName.ReferenceFieldDesc = Nothing
        Me.fndSourceName.ReferenceFieldName = Nothing
        Me.fndSourceName.ReferenceTableName = Nothing
        Me.fndSourceName.Size = New System.Drawing.Size(155, 18)
        Me.fndSourceName.TabIndex = 367
        Me.fndSourceName.Value = ""
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.GroupBox2)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(108.0!, 28.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(975, 422)
        Me.RadPageViewPage3.Text = "Bull General Detail"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.MyLabel7)
        Me.GroupBox2.Controls.Add(Me.lblWeight2)
        Me.GroupBox2.Controls.Add(Me.lblBirthWeight2)
        Me.GroupBox2.Controls.Add(Me.lblDamOrgin2)
        Me.GroupBox2.Controls.Add(Me.chkProven)
        Me.GroupBox2.Controls.Add(Me.lblNodau2)
        Me.GroupBox2.Controls.Add(Me.chkGenomicTestedBulls)
        Me.GroupBox2.Controls.Add(Me.chkIsIBRBull)
        Me.GroupBox2.Controls.Add(Me.chkPercentageVerification)
        Me.GroupBox2.Controls.Add(Me.chkUnderProgenyTest)
        Me.GroupBox2.Controls.Add(Me.chkShouldbeshowninSireDirectory)
        Me.GroupBox2.Controls.Add(Me.chkSibilingTeasted)
        Me.GroupBox2.Controls.Add(Me.chkSonOfProvenSire)
        Me.GroupBox2.Controls.Add(Me.chkKaryotyping)
        Me.GroupBox2.Controls.Add(Me.chkGeneticDiseaseTeasting)
        Me.GroupBox2.Controls.Add(Me.txtMilkingDone)
        Me.GroupBox2.Controls.Add(Me.txtWeightAtEntry)
        Me.GroupBox2.Controls.Add(Me.txtBirthWeight)
        Me.GroupBox2.Controls.Add(Me.txtlDamOrigin)
        Me.GroupBox2.Controls.Add(Me.txtNodaughters)
        Me.GroupBox2.Location = New System.Drawing.Point(3, 13)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(940, 180)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel7.Location = New System.Drawing.Point(7, 129)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(156, 16)
        Me.MyLabel7.TabIndex = 530
        Me.MyLabel7.Text = "No of Milking Done(Daughter)"
        '
        'lblWeight2
        '
        Me.lblWeight2.FieldName = Nothing
        Me.lblWeight2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblWeight2.Location = New System.Drawing.Point(7, 103)
        Me.lblWeight2.Name = "lblWeight2"
        Me.lblWeight2.Size = New System.Drawing.Size(85, 16)
        Me.lblWeight2.TabIndex = 529
        Me.lblWeight2.Text = "Weight At Entry"
        '
        'lblBirthWeight2
        '
        Me.lblBirthWeight2.FieldName = Nothing
        Me.lblBirthWeight2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblBirthWeight2.Location = New System.Drawing.Point(6, 81)
        Me.lblBirthWeight2.Name = "lblBirthWeight2"
        Me.lblBirthWeight2.Size = New System.Drawing.Size(68, 16)
        Me.lblBirthWeight2.TabIndex = 528
        Me.lblBirthWeight2.Text = "Birth Weight"
        '
        'lblDamOrgin2
        '
        Me.lblDamOrgin2.FieldName = Nothing
        Me.lblDamOrgin2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblDamOrgin2.Location = New System.Drawing.Point(7, 53)
        Me.lblDamOrgin2.Name = "lblDamOrgin2"
        Me.lblDamOrgin2.Size = New System.Drawing.Size(63, 16)
        Me.lblDamOrgin2.TabIndex = 527
        Me.lblDamOrgin2.Text = "Dam Origin"
        '
        'chkProven
        '
        Me.chkProven.Location = New System.Drawing.Point(563, 50)
        Me.chkProven.Name = "chkProven"
        Me.chkProven.Size = New System.Drawing.Size(142, 18)
        Me.chkProven.TabIndex = 480
        Me.chkProven.Text = "Proven(Doughter Based)"
        '
        'lblNodau2
        '
        Me.lblNodau2.FieldName = Nothing
        Me.lblNodau2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblNodau2.Location = New System.Drawing.Point(6, 28)
        Me.lblNodau2.Name = "lblNodau2"
        Me.lblNodau2.Size = New System.Drawing.Size(89, 16)
        Me.lblNodau2.TabIndex = 526
        Me.lblNodau2.Text = "No of Daughters"
        '
        'chkGenomicTestedBulls
        '
        Me.chkGenomicTestedBulls.Location = New System.Drawing.Point(402, 75)
        Me.chkGenomicTestedBulls.Name = "chkGenomicTestedBulls"
        Me.chkGenomicTestedBulls.Size = New System.Drawing.Size(127, 18)
        Me.chkGenomicTestedBulls.TabIndex = 475
        Me.chkGenomicTestedBulls.Text = "Genomic Tested Bulls"
        '
        'chkIsIBRBull
        '
        Me.chkIsIBRBull.Location = New System.Drawing.Point(564, 122)
        Me.chkIsIBRBull.Name = "chkIsIBRBull"
        Me.chkIsIBRBull.Size = New System.Drawing.Size(69, 18)
        Me.chkIsIBRBull.TabIndex = 483
        Me.chkIsIBRBull.Text = "Is IBR Bull"
        '
        'chkPercentageVerification
        '
        Me.chkPercentageVerification.Location = New System.Drawing.Point(563, 99)
        Me.chkPercentageVerification.Name = "chkPercentageVerification"
        Me.chkPercentageVerification.Size = New System.Drawing.Size(136, 18)
        Me.chkPercentageVerification.TabIndex = 482
        Me.chkPercentageVerification.Text = "Percentage Verification"
        '
        'chkUnderProgenyTest
        '
        Me.chkUnderProgenyTest.Location = New System.Drawing.Point(563, 73)
        Me.chkUnderProgenyTest.Name = "chkUnderProgenyTest"
        Me.chkUnderProgenyTest.Size = New System.Drawing.Size(119, 18)
        Me.chkUnderProgenyTest.TabIndex = 481
        Me.chkUnderProgenyTest.Text = "Under Progeny Test"
        '
        'chkShouldbeshowninSireDirectory
        '
        Me.chkShouldbeshowninSireDirectory.Location = New System.Drawing.Point(563, 26)
        Me.chkShouldbeshowninSireDirectory.Name = "chkShouldbeshowninSireDirectory"
        Me.chkShouldbeshowninSireDirectory.Size = New System.Drawing.Size(189, 18)
        Me.chkShouldbeshowninSireDirectory.TabIndex = 479
        Me.chkShouldbeshowninSireDirectory.Text = "Should be shown in Sire Directory"
        '
        'chkSibilingTeasted
        '
        Me.chkSibilingTeasted.IsThreeState = True
        Me.chkSibilingTeasted.Location = New System.Drawing.Point(402, 126)
        Me.chkSibilingTeasted.Name = "chkSibilingTeasted"
        Me.chkSibilingTeasted.Size = New System.Drawing.Size(130, 18)
        Me.chkSibilingTeasted.TabIndex = 478
        Me.chkSibilingTeasted.Text = "Sibiling(Sister)Teasted"
        '
        'chkSonOfProvenSire
        '
        Me.chkSonOfProvenSire.Location = New System.Drawing.Point(402, 51)
        Me.chkSonOfProvenSire.Name = "chkSonOfProvenSire"
        Me.chkSonOfProvenSire.Size = New System.Drawing.Size(114, 18)
        Me.chkSonOfProvenSire.TabIndex = 477
        Me.chkSonOfProvenSire.Text = "Son Of Proven Sire"
        '
        'chkKaryotyping
        '
        Me.chkKaryotyping.Location = New System.Drawing.Point(402, 100)
        Me.chkKaryotyping.Name = "chkKaryotyping"
        Me.chkKaryotyping.Size = New System.Drawing.Size(80, 18)
        Me.chkKaryotyping.TabIndex = 476
        Me.chkKaryotyping.Text = "Karyotyping"
        '
        'chkGeneticDiseaseTeasting
        '
        Me.chkGeneticDiseaseTeasting.Location = New System.Drawing.Point(402, 23)
        Me.chkGeneticDiseaseTeasting.Name = "chkGeneticDiseaseTeasting"
        Me.chkGeneticDiseaseTeasting.Size = New System.Drawing.Size(144, 18)
        Me.chkGeneticDiseaseTeasting.TabIndex = 474
        Me.chkGeneticDiseaseTeasting.Text = "Genetic Disease Teasting"
        '
        'txtMilkingDone
        '
        Me.txtMilkingDone.CalculationExpression = Nothing
        Me.txtMilkingDone.FieldCode = Nothing
        Me.txtMilkingDone.FieldDesc = Nothing
        Me.txtMilkingDone.FieldMaxLength = 0
        Me.txtMilkingDone.FieldName = Nothing
        Me.txtMilkingDone.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMilkingDone.isCalculatedField = False
        Me.txtMilkingDone.IsSourceFromTable = False
        Me.txtMilkingDone.IsSourceFromValueList = False
        Me.txtMilkingDone.IsUnique = False
        Me.txtMilkingDone.Location = New System.Drawing.Point(176, 125)
        Me.txtMilkingDone.MaxLength = 200
        Me.txtMilkingDone.MendatroryField = True
        Me.txtMilkingDone.MyLinkLable1 = Me.MyLabel7
        Me.txtMilkingDone.MyLinkLable2 = Nothing
        Me.txtMilkingDone.Name = "txtMilkingDone"
        Me.txtMilkingDone.ReferenceFieldDesc = Nothing
        Me.txtMilkingDone.ReferenceFieldName = Nothing
        Me.txtMilkingDone.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtMilkingDone.RootElement.StretchVertically = True
        Me.txtMilkingDone.Size = New System.Drawing.Size(155, 20)
        Me.txtMilkingDone.TabIndex = 468
        '
        'txtWeightAtEntry
        '
        Me.txtWeightAtEntry.CalculationExpression = Nothing
        Me.txtWeightAtEntry.FieldCode = Nothing
        Me.txtWeightAtEntry.FieldDesc = Nothing
        Me.txtWeightAtEntry.FieldMaxLength = 0
        Me.txtWeightAtEntry.FieldName = Nothing
        Me.txtWeightAtEntry.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWeightAtEntry.isCalculatedField = False
        Me.txtWeightAtEntry.IsSourceFromTable = False
        Me.txtWeightAtEntry.IsSourceFromValueList = False
        Me.txtWeightAtEntry.IsUnique = False
        Me.txtWeightAtEntry.Location = New System.Drawing.Point(176, 99)
        Me.txtWeightAtEntry.MaxLength = 200
        Me.txtWeightAtEntry.MendatroryField = True
        Me.txtWeightAtEntry.MyLinkLable1 = Me.lblWeight2
        Me.txtWeightAtEntry.MyLinkLable2 = Nothing
        Me.txtWeightAtEntry.Name = "txtWeightAtEntry"
        Me.txtWeightAtEntry.ReferenceFieldDesc = Nothing
        Me.txtWeightAtEntry.ReferenceFieldName = Nothing
        Me.txtWeightAtEntry.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtWeightAtEntry.RootElement.StretchVertically = True
        Me.txtWeightAtEntry.Size = New System.Drawing.Size(155, 20)
        Me.txtWeightAtEntry.TabIndex = 467
        '
        'txtBirthWeight
        '
        Me.txtBirthWeight.CalculationExpression = Nothing
        Me.txtBirthWeight.FieldCode = Nothing
        Me.txtBirthWeight.FieldDesc = Nothing
        Me.txtBirthWeight.FieldMaxLength = 0
        Me.txtBirthWeight.FieldName = Nothing
        Me.txtBirthWeight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBirthWeight.isCalculatedField = False
        Me.txtBirthWeight.IsSourceFromTable = False
        Me.txtBirthWeight.IsSourceFromValueList = False
        Me.txtBirthWeight.IsUnique = False
        Me.txtBirthWeight.Location = New System.Drawing.Point(176, 73)
        Me.txtBirthWeight.MaxLength = 200
        Me.txtBirthWeight.MendatroryField = True
        Me.txtBirthWeight.MyLinkLable1 = Me.lblBirthWeight2
        Me.txtBirthWeight.MyLinkLable2 = Nothing
        Me.txtBirthWeight.Name = "txtBirthWeight"
        Me.txtBirthWeight.ReferenceFieldDesc = Nothing
        Me.txtBirthWeight.ReferenceFieldName = Nothing
        Me.txtBirthWeight.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtBirthWeight.RootElement.StretchVertically = True
        Me.txtBirthWeight.Size = New System.Drawing.Size(155, 20)
        Me.txtBirthWeight.TabIndex = 466
        '
        'txtlDamOrigin
        '
        Me.txtlDamOrigin.CalculationExpression = Nothing
        Me.txtlDamOrigin.FieldCode = Nothing
        Me.txtlDamOrigin.FieldDesc = Nothing
        Me.txtlDamOrigin.FieldMaxLength = 0
        Me.txtlDamOrigin.FieldName = Nothing
        Me.txtlDamOrigin.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtlDamOrigin.isCalculatedField = False
        Me.txtlDamOrigin.IsSourceFromTable = False
        Me.txtlDamOrigin.IsSourceFromValueList = False
        Me.txtlDamOrigin.IsUnique = False
        Me.txtlDamOrigin.Location = New System.Drawing.Point(176, 47)
        Me.txtlDamOrigin.MaxLength = 200
        Me.txtlDamOrigin.MendatroryField = True
        Me.txtlDamOrigin.MyLinkLable1 = Me.lblDamOrgin2
        Me.txtlDamOrigin.MyLinkLable2 = Nothing
        Me.txtlDamOrigin.Name = "txtlDamOrigin"
        Me.txtlDamOrigin.ReferenceFieldDesc = Nothing
        Me.txtlDamOrigin.ReferenceFieldName = Nothing
        Me.txtlDamOrigin.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtlDamOrigin.RootElement.StretchVertically = True
        Me.txtlDamOrigin.Size = New System.Drawing.Size(155, 20)
        Me.txtlDamOrigin.TabIndex = 465
        '
        'txtNodaughters
        '
        Me.txtNodaughters.CalculationExpression = Nothing
        Me.txtNodaughters.FieldCode = Nothing
        Me.txtNodaughters.FieldDesc = Nothing
        Me.txtNodaughters.FieldMaxLength = 0
        Me.txtNodaughters.FieldName = Nothing
        Me.txtNodaughters.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNodaughters.isCalculatedField = False
        Me.txtNodaughters.IsSourceFromTable = False
        Me.txtNodaughters.IsSourceFromValueList = False
        Me.txtNodaughters.IsUnique = False
        Me.txtNodaughters.Location = New System.Drawing.Point(176, 21)
        Me.txtNodaughters.MaxLength = 200
        Me.txtNodaughters.MendatroryField = True
        Me.txtNodaughters.MyLinkLable1 = Me.lblNodau2
        Me.txtNodaughters.MyLinkLable2 = Nothing
        Me.txtNodaughters.Name = "txtNodaughters"
        Me.txtNodaughters.ReferenceFieldDesc = Nothing
        Me.txtNodaughters.ReferenceFieldName = Nothing
        Me.txtNodaughters.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtNodaughters.RootElement.StretchVertically = True
        Me.txtNodaughters.Size = New System.Drawing.Size(155, 20)
        Me.txtNodaughters.TabIndex = 464
        '
        'RadPageViewPage4
        '
        Me.RadPageViewPage4.Controls.Add(Me.Panel1)
        Me.RadPageViewPage4.ItemSize = New System.Drawing.SizeF(92.0!, 28.0!)
        Me.RadPageViewPage4.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage4.Name = "RadPageViewPage4"
        Me.RadPageViewPage4.Size = New System.Drawing.Size(975, 422)
        Me.RadPageViewPage4.Text = "Pedigree Detail"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.gv2)
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(972, 416)
        Me.Panel1.TabIndex = 69
        '
        'gv2
        '
        Me.gv2.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv2.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv2.ForeColor = System.Drawing.Color.Black
        Me.gv2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv2.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv2.MasterTemplate.AllowDeleteRow = False
        Me.gv2.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv2.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv2.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv2.MyStopExport = False
        Me.gv2.Name = "gv2"
        Me.gv2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv2.ShowGroupPanel = False
        Me.gv2.ShowHeaderCellButtons = True
        Me.gv2.Size = New System.Drawing.Size(972, 416)
        Me.gv2.TabIndex = 68
        Me.gv2.TabStop = False
        '
        'RadPageViewPage5
        '
        Me.RadPageViewPage5.Controls.Add(Me.GroupBox4)
        Me.RadPageViewPage5.ItemSize = New System.Drawing.SizeF(90.0!, 28.0!)
        Me.RadPageViewPage5.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage5.Name = "RadPageViewPage5"
        Me.RadPageViewPage5.Size = New System.Drawing.Size(975, 422)
        Me.RadPageViewPage5.Text = "Progeny Detail"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.MyLabel21)
        Me.GroupBox4.Controls.Add(Me.MyLabel20)
        Me.GroupBox4.Controls.Add(Me.MyLabel19)
        Me.GroupBox4.Controls.Add(Me.MyLabel18)
        Me.GroupBox4.Controls.Add(Me.MyLabel17)
        Me.GroupBox4.Controls.Add(Me.MyLabel16)
        Me.GroupBox4.Controls.Add(Me.MyLabel14)
        Me.GroupBox4.Controls.Add(Me.MyLabel13)
        Me.GroupBox4.Controls.Add(Me.MyLabel12)
        Me.GroupBox4.Controls.Add(Me.MyLabel11)
        Me.GroupBox4.Controls.Add(Me.MyLabel10)
        Me.GroupBox4.Controls.Add(Me.MyLabel9)
        Me.GroupBox4.Controls.Add(Me.MyLabel8)
        Me.GroupBox4.Controls.Add(Me.MyLabel6)
        Me.GroupBox4.Controls.Add(Me.MyLabel5)
        Me.GroupBox4.Controls.Add(Me.MyLabel4)
        Me.GroupBox4.Controls.Add(Me.MyLabel1)
        Me.GroupBox4.Controls.Add(Me.txtQuarntine)
        Me.GroupBox4.Controls.Add(Me.txtREARINGCentre)
        Me.GroupBox4.Controls.Add(Me.txtNomalesborn)
        Me.GroupBox4.Controls.Add(Me.txtNoabortions)
        Me.GroupBox4.Controls.Add(Me.txtTotalConceptions)
        Me.GroupBox4.Controls.Add(Me.txtNoofelitefemalescurrentlypregnant)
        Me.GroupBox4.Controls.Add(Me.txtNoofFemaleCalves)
        Me.GroupBox4.Controls.Add(Me.txtTotalHeiferConceptions)
        Me.GroupBox4.Controls.Add(Me.txtNoundersemencollection)
        Me.GroupBox4.Controls.Add(Me.txtPreQuarantine)
        Me.GroupBox4.Controls.Add(Me.txtNoofinseminationcarriedout)
        Me.GroupBox4.Controls.Add(Me.txtNoofmalecalves)
        Me.GroupBox4.Controls.Add(Me.txtTotalHeiferAI)
        Me.GroupBox4.Controls.Add(Me.txtTrainingCentre)
        Me.GroupBox4.Controls.Add(Me.Label37)
        Me.GroupBox4.Controls.Add(Me.txtNoofmalesproduced)
        Me.GroupBox4.Controls.Add(Me.txtDateofnominatedmatinginitiated)
        Me.GroupBox4.Controls.Add(Me.txtTotalAI)
        Me.GroupBox4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox4.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(975, 422)
        Me.GroupBox4.TabIndex = 0
        Me.GroupBox4.TabStop = False
        '
        'MyLabel21
        '
        Me.MyLabel21.FieldName = Nothing
        Me.MyLabel21.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel21.Location = New System.Drawing.Point(32, 336)
        Me.MyLabel21.Name = "MyLabel21"
        Me.MyLabel21.Size = New System.Drawing.Size(56, 16)
        Me.MyLabel21.TabIndex = 542
        Me.MyLabel21.Text = "Quarntine"
        '
        'MyLabel20
        '
        Me.MyLabel20.FieldName = Nothing
        Me.MyLabel20.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel20.Location = New System.Drawing.Point(32, 312)
        Me.MyLabel20.Name = "MyLabel20"
        Me.MyLabel20.Size = New System.Drawing.Size(194, 16)
        Me.MyLabel20.TabIndex = 542
        Me.MyLabel20.Text = "No of elite females currently pregnant"
        '
        'MyLabel19
        '
        Me.MyLabel19.FieldName = Nothing
        Me.MyLabel19.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel19.Location = New System.Drawing.Point(32, 286)
        Me.MyLabel19.Name = "MyLabel19"
        Me.MyLabel19.Size = New System.Drawing.Size(112, 16)
        Me.MyLabel19.TabIndex = 541
        Me.MyLabel19.Text = "No of Female Calves"
        '
        'MyLabel18
        '
        Me.MyLabel18.FieldName = Nothing
        Me.MyLabel18.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel18.Location = New System.Drawing.Point(32, 258)
        Me.MyLabel18.Name = "MyLabel18"
        Me.MyLabel18.Size = New System.Drawing.Size(131, 16)
        Me.MyLabel18.TabIndex = 540
        Me.MyLabel18.Text = "Total Heifer Conceptions"
        '
        'MyLabel17
        '
        Me.MyLabel17.FieldName = Nothing
        Me.MyLabel17.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel17.Location = New System.Drawing.Point(32, 232)
        Me.MyLabel17.Name = "MyLabel17"
        Me.MyLabel17.Size = New System.Drawing.Size(143, 16)
        Me.MyLabel17.TabIndex = 539
        Me.MyLabel17.Text = "No. under semen collection"
        '
        'MyLabel16
        '
        Me.MyLabel16.FieldName = Nothing
        Me.MyLabel16.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel16.Location = New System.Drawing.Point(32, 205)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(83, 16)
        Me.MyLabel16.TabIndex = 538
        Me.MyLabel16.Text = "Pre Quarantine"
        '
        'MyLabel14
        '
        Me.MyLabel14.FieldName = Nothing
        Me.MyLabel14.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel14.Location = New System.Drawing.Point(32, 179)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(157, 16)
        Me.MyLabel14.TabIndex = 537
        Me.MyLabel14.Text = "No of insemination carried out"
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel13.Location = New System.Drawing.Point(32, 153)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(108, 16)
        Me.MyLabel13.TabIndex = 536
        Me.MyLabel13.Text = "No. Of male calaves"
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel12.Location = New System.Drawing.Point(32, 127)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(79, 16)
        Me.MyLabel12.TabIndex = 535
        Me.MyLabel12.Text = "Total Heifer AI"
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel11.Location = New System.Drawing.Point(32, 101)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(107, 16)
        Me.MyLabel11.TabIndex = 534
        Me.MyLabel11.Text = "TRANING CENTRE"
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel10.Location = New System.Drawing.Point(32, 73)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(115, 16)
        Me.MyLabel10.TabIndex = 533
        Me.MyLabel10.Text = "No Of Male Produced"
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel9.Location = New System.Drawing.Point(31, 47)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(184, 16)
        Me.MyLabel9.TabIndex = 532
        Me.MyLabel9.Text = " Date Of nominated mating Initiated"
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel8.Location = New System.Drawing.Point(32, 22)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(45, 16)
        Me.MyLabel8.TabIndex = 531
        Me.MyLabel8.Text = "Total AI"
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel6.Location = New System.Drawing.Point(459, 101)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(95, 16)
        Me.MyLabel6.TabIndex = 530
        Me.MyLabel6.Text = "REARING Centre"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel5.Location = New System.Drawing.Point(459, 73)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(80, 16)
        Me.MyLabel5.TabIndex = 529
        Me.MyLabel5.Text = "No males born"
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel4.Location = New System.Drawing.Point(459, 47)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(75, 16)
        Me.MyLabel4.TabIndex = 528
        Me.MyLabel4.Text = "No. Abortions"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(459, 22)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(97, 16)
        Me.MyLabel1.TabIndex = 527
        Me.MyLabel1.Text = "Total Conceptions"
        '
        'txtQuarntine
        '
        Me.txtQuarntine.CalculationExpression = Nothing
        Me.txtQuarntine.FieldCode = Nothing
        Me.txtQuarntine.FieldDesc = Nothing
        Me.txtQuarntine.FieldMaxLength = 0
        Me.txtQuarntine.FieldName = Nothing
        Me.txtQuarntine.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtQuarntine.isCalculatedField = False
        Me.txtQuarntine.IsSourceFromTable = False
        Me.txtQuarntine.IsSourceFromValueList = False
        Me.txtQuarntine.IsUnique = False
        Me.txtQuarntine.Location = New System.Drawing.Point(240, 332)
        Me.txtQuarntine.MaxLength = 200
        Me.txtQuarntine.MendatroryField = True
        Me.txtQuarntine.MyLinkLable1 = Me.MyLabel21
        Me.txtQuarntine.MyLinkLable2 = Nothing
        Me.txtQuarntine.Name = "txtQuarntine"
        Me.txtQuarntine.ReferenceFieldDesc = Nothing
        Me.txtQuarntine.ReferenceFieldName = Nothing
        Me.txtQuarntine.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtQuarntine.RootElement.StretchVertically = True
        Me.txtQuarntine.Size = New System.Drawing.Size(155, 20)
        Me.txtQuarntine.TabIndex = 499
        '
        'txtREARINGCentre
        '
        Me.txtREARINGCentre.CalculationExpression = Nothing
        Me.txtREARINGCentre.FieldCode = Nothing
        Me.txtREARINGCentre.FieldDesc = Nothing
        Me.txtREARINGCentre.FieldMaxLength = 0
        Me.txtREARINGCentre.FieldName = Nothing
        Me.txtREARINGCentre.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtREARINGCentre.isCalculatedField = False
        Me.txtREARINGCentre.IsSourceFromTable = False
        Me.txtREARINGCentre.IsSourceFromValueList = False
        Me.txtREARINGCentre.IsUnique = False
        Me.txtREARINGCentre.Location = New System.Drawing.Point(569, 97)
        Me.txtREARINGCentre.MaxLength = 200
        Me.txtREARINGCentre.MendatroryField = True
        Me.txtREARINGCentre.MyLinkLable1 = Me.MyLabel6
        Me.txtREARINGCentre.MyLinkLable2 = Nothing
        Me.txtREARINGCentre.Name = "txtREARINGCentre"
        Me.txtREARINGCentre.ReferenceFieldDesc = Nothing
        Me.txtREARINGCentre.ReferenceFieldName = Nothing
        Me.txtREARINGCentre.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtREARINGCentre.RootElement.StretchVertically = True
        Me.txtREARINGCentre.Size = New System.Drawing.Size(155, 20)
        Me.txtREARINGCentre.TabIndex = 498
        '
        'txtNomalesborn
        '
        Me.txtNomalesborn.CalculationExpression = Nothing
        Me.txtNomalesborn.FieldCode = Nothing
        Me.txtNomalesborn.FieldDesc = Nothing
        Me.txtNomalesborn.FieldMaxLength = 0
        Me.txtNomalesborn.FieldName = Nothing
        Me.txtNomalesborn.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNomalesborn.isCalculatedField = False
        Me.txtNomalesborn.IsSourceFromTable = False
        Me.txtNomalesborn.IsSourceFromValueList = False
        Me.txtNomalesborn.IsUnique = False
        Me.txtNomalesborn.Location = New System.Drawing.Point(569, 69)
        Me.txtNomalesborn.MaxLength = 200
        Me.txtNomalesborn.MendatroryField = True
        Me.txtNomalesborn.MyLinkLable1 = Me.MyLabel5
        Me.txtNomalesborn.MyLinkLable2 = Nothing
        Me.txtNomalesborn.Name = "txtNomalesborn"
        Me.txtNomalesborn.ReferenceFieldDesc = Nothing
        Me.txtNomalesborn.ReferenceFieldName = Nothing
        Me.txtNomalesborn.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtNomalesborn.RootElement.StretchVertically = True
        Me.txtNomalesborn.Size = New System.Drawing.Size(155, 20)
        Me.txtNomalesborn.TabIndex = 497
        '
        'txtNoabortions
        '
        Me.txtNoabortions.CalculationExpression = Nothing
        Me.txtNoabortions.FieldCode = Nothing
        Me.txtNoabortions.FieldDesc = Nothing
        Me.txtNoabortions.FieldMaxLength = 0
        Me.txtNoabortions.FieldName = Nothing
        Me.txtNoabortions.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNoabortions.isCalculatedField = False
        Me.txtNoabortions.IsSourceFromTable = False
        Me.txtNoabortions.IsSourceFromValueList = False
        Me.txtNoabortions.IsUnique = False
        Me.txtNoabortions.Location = New System.Drawing.Point(569, 43)
        Me.txtNoabortions.MaxLength = 200
        Me.txtNoabortions.MendatroryField = True
        Me.txtNoabortions.MyLinkLable1 = Me.MyLabel4
        Me.txtNoabortions.MyLinkLable2 = Nothing
        Me.txtNoabortions.Name = "txtNoabortions"
        Me.txtNoabortions.ReferenceFieldDesc = Nothing
        Me.txtNoabortions.ReferenceFieldName = Nothing
        Me.txtNoabortions.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtNoabortions.RootElement.StretchVertically = True
        Me.txtNoabortions.Size = New System.Drawing.Size(155, 20)
        Me.txtNoabortions.TabIndex = 496
        '
        'txtTotalConceptions
        '
        Me.txtTotalConceptions.CalculationExpression = Nothing
        Me.txtTotalConceptions.FieldCode = Nothing
        Me.txtTotalConceptions.FieldDesc = Nothing
        Me.txtTotalConceptions.FieldMaxLength = 0
        Me.txtTotalConceptions.FieldName = Nothing
        Me.txtTotalConceptions.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalConceptions.isCalculatedField = False
        Me.txtTotalConceptions.IsSourceFromTable = False
        Me.txtTotalConceptions.IsSourceFromValueList = False
        Me.txtTotalConceptions.IsUnique = False
        Me.txtTotalConceptions.Location = New System.Drawing.Point(569, 18)
        Me.txtTotalConceptions.MaxLength = 200
        Me.txtTotalConceptions.MendatroryField = True
        Me.txtTotalConceptions.MyLinkLable1 = Me.MyLabel1
        Me.txtTotalConceptions.MyLinkLable2 = Nothing
        Me.txtTotalConceptions.Name = "txtTotalConceptions"
        Me.txtTotalConceptions.ReferenceFieldDesc = Nothing
        Me.txtTotalConceptions.ReferenceFieldName = Nothing
        Me.txtTotalConceptions.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtTotalConceptions.RootElement.StretchVertically = True
        Me.txtTotalConceptions.Size = New System.Drawing.Size(155, 20)
        Me.txtTotalConceptions.TabIndex = 495
        '
        'txtNoofelitefemalescurrentlypregnant
        '
        Me.txtNoofelitefemalescurrentlypregnant.CalculationExpression = Nothing
        Me.txtNoofelitefemalescurrentlypregnant.FieldCode = Nothing
        Me.txtNoofelitefemalescurrentlypregnant.FieldDesc = Nothing
        Me.txtNoofelitefemalescurrentlypregnant.FieldMaxLength = 0
        Me.txtNoofelitefemalescurrentlypregnant.FieldName = Nothing
        Me.txtNoofelitefemalescurrentlypregnant.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNoofelitefemalescurrentlypregnant.isCalculatedField = False
        Me.txtNoofelitefemalescurrentlypregnant.IsSourceFromTable = False
        Me.txtNoofelitefemalescurrentlypregnant.IsSourceFromValueList = False
        Me.txtNoofelitefemalescurrentlypregnant.IsUnique = False
        Me.txtNoofelitefemalescurrentlypregnant.Location = New System.Drawing.Point(240, 306)
        Me.txtNoofelitefemalescurrentlypregnant.MaxLength = 200
        Me.txtNoofelitefemalescurrentlypregnant.MendatroryField = True
        Me.txtNoofelitefemalescurrentlypregnant.MyLinkLable1 = Me.MyLabel20
        Me.txtNoofelitefemalescurrentlypregnant.MyLinkLable2 = Nothing
        Me.txtNoofelitefemalescurrentlypregnant.Name = "txtNoofelitefemalescurrentlypregnant"
        Me.txtNoofelitefemalescurrentlypregnant.ReferenceFieldDesc = Nothing
        Me.txtNoofelitefemalescurrentlypregnant.ReferenceFieldName = Nothing
        Me.txtNoofelitefemalescurrentlypregnant.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtNoofelitefemalescurrentlypregnant.RootElement.StretchVertically = True
        Me.txtNoofelitefemalescurrentlypregnant.Size = New System.Drawing.Size(155, 20)
        Me.txtNoofelitefemalescurrentlypregnant.TabIndex = 494
        '
        'txtNoofFemaleCalves
        '
        Me.txtNoofFemaleCalves.CalculationExpression = Nothing
        Me.txtNoofFemaleCalves.FieldCode = Nothing
        Me.txtNoofFemaleCalves.FieldDesc = Nothing
        Me.txtNoofFemaleCalves.FieldMaxLength = 0
        Me.txtNoofFemaleCalves.FieldName = Nothing
        Me.txtNoofFemaleCalves.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNoofFemaleCalves.isCalculatedField = False
        Me.txtNoofFemaleCalves.IsSourceFromTable = False
        Me.txtNoofFemaleCalves.IsSourceFromValueList = False
        Me.txtNoofFemaleCalves.IsUnique = False
        Me.txtNoofFemaleCalves.Location = New System.Drawing.Point(240, 280)
        Me.txtNoofFemaleCalves.MaxLength = 200
        Me.txtNoofFemaleCalves.MendatroryField = True
        Me.txtNoofFemaleCalves.MyLinkLable1 = Me.MyLabel19
        Me.txtNoofFemaleCalves.MyLinkLable2 = Nothing
        Me.txtNoofFemaleCalves.Name = "txtNoofFemaleCalves"
        Me.txtNoofFemaleCalves.ReferenceFieldDesc = Nothing
        Me.txtNoofFemaleCalves.ReferenceFieldName = Nothing
        Me.txtNoofFemaleCalves.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtNoofFemaleCalves.RootElement.StretchVertically = True
        Me.txtNoofFemaleCalves.Size = New System.Drawing.Size(155, 20)
        Me.txtNoofFemaleCalves.TabIndex = 493
        '
        'txtTotalHeiferConceptions
        '
        Me.txtTotalHeiferConceptions.CalculationExpression = Nothing
        Me.txtTotalHeiferConceptions.FieldCode = Nothing
        Me.txtTotalHeiferConceptions.FieldDesc = Nothing
        Me.txtTotalHeiferConceptions.FieldMaxLength = 0
        Me.txtTotalHeiferConceptions.FieldName = Nothing
        Me.txtTotalHeiferConceptions.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalHeiferConceptions.isCalculatedField = False
        Me.txtTotalHeiferConceptions.IsSourceFromTable = False
        Me.txtTotalHeiferConceptions.IsSourceFromValueList = False
        Me.txtTotalHeiferConceptions.IsUnique = False
        Me.txtTotalHeiferConceptions.Location = New System.Drawing.Point(240, 254)
        Me.txtTotalHeiferConceptions.MaxLength = 200
        Me.txtTotalHeiferConceptions.MendatroryField = True
        Me.txtTotalHeiferConceptions.MyLinkLable1 = Me.MyLabel18
        Me.txtTotalHeiferConceptions.MyLinkLable2 = Nothing
        Me.txtTotalHeiferConceptions.Name = "txtTotalHeiferConceptions"
        Me.txtTotalHeiferConceptions.ReferenceFieldDesc = Nothing
        Me.txtTotalHeiferConceptions.ReferenceFieldName = Nothing
        Me.txtTotalHeiferConceptions.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtTotalHeiferConceptions.RootElement.StretchVertically = True
        Me.txtTotalHeiferConceptions.Size = New System.Drawing.Size(155, 20)
        Me.txtTotalHeiferConceptions.TabIndex = 492
        '
        'txtNoundersemencollection
        '
        Me.txtNoundersemencollection.CalculationExpression = Nothing
        Me.txtNoundersemencollection.FieldCode = Nothing
        Me.txtNoundersemencollection.FieldDesc = Nothing
        Me.txtNoundersemencollection.FieldMaxLength = 0
        Me.txtNoundersemencollection.FieldName = Nothing
        Me.txtNoundersemencollection.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNoundersemencollection.isCalculatedField = False
        Me.txtNoundersemencollection.IsSourceFromTable = False
        Me.txtNoundersemencollection.IsSourceFromValueList = False
        Me.txtNoundersemencollection.IsUnique = False
        Me.txtNoundersemencollection.Location = New System.Drawing.Point(240, 228)
        Me.txtNoundersemencollection.MaxLength = 200
        Me.txtNoundersemencollection.MendatroryField = True
        Me.txtNoundersemencollection.MyLinkLable1 = Me.MyLabel17
        Me.txtNoundersemencollection.MyLinkLable2 = Nothing
        Me.txtNoundersemencollection.Name = "txtNoundersemencollection"
        Me.txtNoundersemencollection.ReferenceFieldDesc = Nothing
        Me.txtNoundersemencollection.ReferenceFieldName = Nothing
        Me.txtNoundersemencollection.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtNoundersemencollection.RootElement.StretchVertically = True
        Me.txtNoundersemencollection.Size = New System.Drawing.Size(155, 20)
        Me.txtNoundersemencollection.TabIndex = 491
        '
        'txtPreQuarantine
        '
        Me.txtPreQuarantine.CalculationExpression = Nothing
        Me.txtPreQuarantine.FieldCode = Nothing
        Me.txtPreQuarantine.FieldDesc = Nothing
        Me.txtPreQuarantine.FieldMaxLength = 0
        Me.txtPreQuarantine.FieldName = Nothing
        Me.txtPreQuarantine.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPreQuarantine.isCalculatedField = False
        Me.txtPreQuarantine.IsSourceFromTable = False
        Me.txtPreQuarantine.IsSourceFromValueList = False
        Me.txtPreQuarantine.IsUnique = False
        Me.txtPreQuarantine.Location = New System.Drawing.Point(240, 201)
        Me.txtPreQuarantine.MaxLength = 200
        Me.txtPreQuarantine.MendatroryField = True
        Me.txtPreQuarantine.MyLinkLable1 = Me.MyLabel16
        Me.txtPreQuarantine.MyLinkLable2 = Nothing
        Me.txtPreQuarantine.Name = "txtPreQuarantine"
        Me.txtPreQuarantine.ReferenceFieldDesc = Nothing
        Me.txtPreQuarantine.ReferenceFieldName = Nothing
        Me.txtPreQuarantine.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtPreQuarantine.RootElement.StretchVertically = True
        Me.txtPreQuarantine.Size = New System.Drawing.Size(155, 20)
        Me.txtPreQuarantine.TabIndex = 490
        '
        'txtNoofinseminationcarriedout
        '
        Me.txtNoofinseminationcarriedout.CalculationExpression = Nothing
        Me.txtNoofinseminationcarriedout.FieldCode = Nothing
        Me.txtNoofinseminationcarriedout.FieldDesc = Nothing
        Me.txtNoofinseminationcarriedout.FieldMaxLength = 0
        Me.txtNoofinseminationcarriedout.FieldName = Nothing
        Me.txtNoofinseminationcarriedout.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNoofinseminationcarriedout.isCalculatedField = False
        Me.txtNoofinseminationcarriedout.IsSourceFromTable = False
        Me.txtNoofinseminationcarriedout.IsSourceFromValueList = False
        Me.txtNoofinseminationcarriedout.IsUnique = False
        Me.txtNoofinseminationcarriedout.Location = New System.Drawing.Point(240, 175)
        Me.txtNoofinseminationcarriedout.MaxLength = 200
        Me.txtNoofinseminationcarriedout.MendatroryField = True
        Me.txtNoofinseminationcarriedout.MyLinkLable1 = Me.MyLabel14
        Me.txtNoofinseminationcarriedout.MyLinkLable2 = Nothing
        Me.txtNoofinseminationcarriedout.Name = "txtNoofinseminationcarriedout"
        Me.txtNoofinseminationcarriedout.ReferenceFieldDesc = Nothing
        Me.txtNoofinseminationcarriedout.ReferenceFieldName = Nothing
        Me.txtNoofinseminationcarriedout.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtNoofinseminationcarriedout.RootElement.StretchVertically = True
        Me.txtNoofinseminationcarriedout.Size = New System.Drawing.Size(155, 20)
        Me.txtNoofinseminationcarriedout.TabIndex = 489
        '
        'txtNoofmalecalves
        '
        Me.txtNoofmalecalves.CalculationExpression = Nothing
        Me.txtNoofmalecalves.FieldCode = Nothing
        Me.txtNoofmalecalves.FieldDesc = Nothing
        Me.txtNoofmalecalves.FieldMaxLength = 0
        Me.txtNoofmalecalves.FieldName = Nothing
        Me.txtNoofmalecalves.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNoofmalecalves.isCalculatedField = False
        Me.txtNoofmalecalves.IsSourceFromTable = False
        Me.txtNoofmalecalves.IsSourceFromValueList = False
        Me.txtNoofmalecalves.IsUnique = False
        Me.txtNoofmalecalves.Location = New System.Drawing.Point(240, 149)
        Me.txtNoofmalecalves.MaxLength = 200
        Me.txtNoofmalecalves.MendatroryField = True
        Me.txtNoofmalecalves.MyLinkLable1 = Me.MyLabel13
        Me.txtNoofmalecalves.MyLinkLable2 = Nothing
        Me.txtNoofmalecalves.Name = "txtNoofmalecalves"
        Me.txtNoofmalecalves.ReferenceFieldDesc = Nothing
        Me.txtNoofmalecalves.ReferenceFieldName = Nothing
        Me.txtNoofmalecalves.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtNoofmalecalves.RootElement.StretchVertically = True
        Me.txtNoofmalecalves.Size = New System.Drawing.Size(155, 20)
        Me.txtNoofmalecalves.TabIndex = 488
        '
        'txtTotalHeiferAI
        '
        Me.txtTotalHeiferAI.CalculationExpression = Nothing
        Me.txtTotalHeiferAI.FieldCode = Nothing
        Me.txtTotalHeiferAI.FieldDesc = Nothing
        Me.txtTotalHeiferAI.FieldMaxLength = 0
        Me.txtTotalHeiferAI.FieldName = Nothing
        Me.txtTotalHeiferAI.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalHeiferAI.isCalculatedField = False
        Me.txtTotalHeiferAI.IsSourceFromTable = False
        Me.txtTotalHeiferAI.IsSourceFromValueList = False
        Me.txtTotalHeiferAI.IsUnique = False
        Me.txtTotalHeiferAI.Location = New System.Drawing.Point(240, 123)
        Me.txtTotalHeiferAI.MaxLength = 200
        Me.txtTotalHeiferAI.MendatroryField = True
        Me.txtTotalHeiferAI.MyLinkLable1 = Me.MyLabel12
        Me.txtTotalHeiferAI.MyLinkLable2 = Nothing
        Me.txtTotalHeiferAI.Name = "txtTotalHeiferAI"
        Me.txtTotalHeiferAI.ReferenceFieldDesc = Nothing
        Me.txtTotalHeiferAI.ReferenceFieldName = Nothing
        Me.txtTotalHeiferAI.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtTotalHeiferAI.RootElement.StretchVertically = True
        Me.txtTotalHeiferAI.Size = New System.Drawing.Size(155, 20)
        Me.txtTotalHeiferAI.TabIndex = 487
        '
        'txtTrainingCentre
        '
        Me.txtTrainingCentre.CalculationExpression = Nothing
        Me.txtTrainingCentre.FieldCode = Nothing
        Me.txtTrainingCentre.FieldDesc = Nothing
        Me.txtTrainingCentre.FieldMaxLength = 0
        Me.txtTrainingCentre.FieldName = Nothing
        Me.txtTrainingCentre.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTrainingCentre.isCalculatedField = False
        Me.txtTrainingCentre.IsSourceFromTable = False
        Me.txtTrainingCentre.IsSourceFromValueList = False
        Me.txtTrainingCentre.IsUnique = False
        Me.txtTrainingCentre.Location = New System.Drawing.Point(240, 95)
        Me.txtTrainingCentre.MaxLength = 200
        Me.txtTrainingCentre.MendatroryField = True
        Me.txtTrainingCentre.MyLinkLable1 = Me.MyLabel11
        Me.txtTrainingCentre.MyLinkLable2 = Nothing
        Me.txtTrainingCentre.Name = "txtTrainingCentre"
        Me.txtTrainingCentre.ReferenceFieldDesc = Nothing
        Me.txtTrainingCentre.ReferenceFieldName = Nothing
        Me.txtTrainingCentre.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtTrainingCentre.RootElement.StretchVertically = True
        Me.txtTrainingCentre.Size = New System.Drawing.Size(155, 20)
        Me.txtTrainingCentre.TabIndex = 486
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(411, 182)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(0, 13)
        Me.Label37.TabIndex = 477
        '
        'txtNoofmalesproduced
        '
        Me.txtNoofmalesproduced.CalculationExpression = Nothing
        Me.txtNoofmalesproduced.FieldCode = Nothing
        Me.txtNoofmalesproduced.FieldDesc = Nothing
        Me.txtNoofmalesproduced.FieldMaxLength = 0
        Me.txtNoofmalesproduced.FieldName = Nothing
        Me.txtNoofmalesproduced.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNoofmalesproduced.isCalculatedField = False
        Me.txtNoofmalesproduced.IsSourceFromTable = False
        Me.txtNoofmalesproduced.IsSourceFromValueList = False
        Me.txtNoofmalesproduced.IsUnique = False
        Me.txtNoofmalesproduced.Location = New System.Drawing.Point(240, 69)
        Me.txtNoofmalesproduced.MaxLength = 200
        Me.txtNoofmalesproduced.MendatroryField = True
        Me.txtNoofmalesproduced.MyLinkLable1 = Me.MyLabel10
        Me.txtNoofmalesproduced.MyLinkLable2 = Nothing
        Me.txtNoofmalesproduced.Name = "txtNoofmalesproduced"
        Me.txtNoofmalesproduced.ReferenceFieldDesc = Nothing
        Me.txtNoofmalesproduced.ReferenceFieldName = Nothing
        Me.txtNoofmalesproduced.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtNoofmalesproduced.RootElement.StretchVertically = True
        Me.txtNoofmalesproduced.Size = New System.Drawing.Size(155, 20)
        Me.txtNoofmalesproduced.TabIndex = 470
        '
        'txtDateofnominatedmatinginitiated
        '
        Me.txtDateofnominatedmatinginitiated.CalculationExpression = Nothing
        Me.txtDateofnominatedmatinginitiated.FieldCode = Nothing
        Me.txtDateofnominatedmatinginitiated.FieldDesc = Nothing
        Me.txtDateofnominatedmatinginitiated.FieldMaxLength = 0
        Me.txtDateofnominatedmatinginitiated.FieldName = Nothing
        Me.txtDateofnominatedmatinginitiated.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateofnominatedmatinginitiated.isCalculatedField = False
        Me.txtDateofnominatedmatinginitiated.IsSourceFromTable = False
        Me.txtDateofnominatedmatinginitiated.IsSourceFromValueList = False
        Me.txtDateofnominatedmatinginitiated.IsUnique = False
        Me.txtDateofnominatedmatinginitiated.Location = New System.Drawing.Point(240, 43)
        Me.txtDateofnominatedmatinginitiated.MaxLength = 200
        Me.txtDateofnominatedmatinginitiated.MendatroryField = True
        Me.txtDateofnominatedmatinginitiated.MyLinkLable1 = Me.MyLabel9
        Me.txtDateofnominatedmatinginitiated.MyLinkLable2 = Nothing
        Me.txtDateofnominatedmatinginitiated.Name = "txtDateofnominatedmatinginitiated"
        Me.txtDateofnominatedmatinginitiated.ReferenceFieldDesc = Nothing
        Me.txtDateofnominatedmatinginitiated.ReferenceFieldName = Nothing
        Me.txtDateofnominatedmatinginitiated.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtDateofnominatedmatinginitiated.RootElement.StretchVertically = True
        Me.txtDateofnominatedmatinginitiated.Size = New System.Drawing.Size(155, 20)
        Me.txtDateofnominatedmatinginitiated.TabIndex = 468
        '
        'txtTotalAI
        '
        Me.txtTotalAI.CalculationExpression = Nothing
        Me.txtTotalAI.FieldCode = Nothing
        Me.txtTotalAI.FieldDesc = Nothing
        Me.txtTotalAI.FieldMaxLength = 0
        Me.txtTotalAI.FieldName = Nothing
        Me.txtTotalAI.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalAI.isCalculatedField = False
        Me.txtTotalAI.IsSourceFromTable = False
        Me.txtTotalAI.IsSourceFromValueList = False
        Me.txtTotalAI.IsUnique = False
        Me.txtTotalAI.Location = New System.Drawing.Point(240, 18)
        Me.txtTotalAI.MaxLength = 200
        Me.txtTotalAI.MendatroryField = True
        Me.txtTotalAI.MyLinkLable1 = Me.MyLabel8
        Me.txtTotalAI.MyLinkLable2 = Nothing
        Me.txtTotalAI.Name = "txtTotalAI"
        Me.txtTotalAI.ReferenceFieldDesc = Nothing
        Me.txtTotalAI.ReferenceFieldName = Nothing
        Me.txtTotalAI.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtTotalAI.RootElement.StretchVertically = True
        Me.txtTotalAI.Size = New System.Drawing.Size(155, 20)
        Me.txtTotalAI.TabIndex = 467
        '
        'RadPageViewPage6
        '
        Me.RadPageViewPage6.Controls.Add(Me.PictureBox1)
        Me.RadPageViewPage6.Controls.Add(Me.btnSelectPath1)
        Me.RadPageViewPage6.ItemSize = New System.Drawing.SizeF(90.0!, 28.0!)
        Me.RadPageViewPage6.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage6.Name = "RadPageViewPage6"
        Me.RadPageViewPage6.Size = New System.Drawing.Size(975, 422)
        Me.RadPageViewPage6.Text = "Picture Upload"
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(3, 34)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(965, 360)
        Me.PictureBox1.TabIndex = 10
        Me.PictureBox1.TabStop = False
        '
        'btnSelectPath1
        '
        Me.btnSelectPath1.Location = New System.Drawing.Point(8, 4)
        Me.btnSelectPath1.Name = "btnSelectPath1"
        Me.btnSelectPath1.Size = New System.Drawing.Size(130, 24)
        Me.btnSelectPath1.TabIndex = 9
        Me.btnSelectPath1.Text = "Select Image"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(104, 8)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 22)
        Me.btnDelete.TabIndex = 8
        Me.btnDelete.Text = "Delete"
        '
        'btnClose
        '
        Me.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(904, 9)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 10
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(29, 8)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 7
        Me.btnSave.Text = "Save"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'frmBullMasters
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(996, 509)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmBullMasters"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "frmBullMasters"
        CType(Me.lblBullSourcePrintStrew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDamLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBullRating, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRemark, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBullFRID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblExistDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblStatusChangeDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPenId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblShedId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSSCentre, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBullBookValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblExoticBullPercentage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBullSubStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBullAliasName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBullStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSSBullId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDOB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPrevBullId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBreed, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl12DigitBullId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSubCategory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCountryCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCategory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSpecies, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSemen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblbankcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel32, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel28, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDateOfBirth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDateOfBirth2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.lblSpecies1.ResumeLayout(False)
        Me.lblSpecies1.PerformLayout()
        CType(Me.lblExitDate2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblStatusChangedDate2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblcode2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRemark2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBullSubStatus2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBullStatus2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPrevBullId2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBullSourcePainting2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDamLoactionYield2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBullRating2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPenId2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblShedId2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSScentre2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBullAliasName2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSSBullId2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSubCategory2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCountryCode2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBreed2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBullBookValue2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRegistrationDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCategory2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBullRFID2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblExotic2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblspecies2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IsSemen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemark, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TXTPrevBull, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDamLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBullAlias, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TXTSSbull, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBullBook, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBullRFID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TXTExoticBloodPer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TXTEndDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRegDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtStatusDateChanged, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.txtLastDateBreeding, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLastdayBreeding, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPurchaseDate2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPurchaseRequestDate2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSourceName2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBreedingValue2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCapacityOfAverageMonthlyDoses, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDosesProducedTillDate2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSemenPrice2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFirstCollectionDate2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblOwnerOfBull2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSourceAddress2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPurchaseRequestNo2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtProducedTillDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPurchaseDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPurchaseRequestDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPurchaseNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBreedingValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAveregeDoses, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFirstCollectionDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSemenPrice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOwnerName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSourceName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage3.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblWeight2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBirthWeight2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDamOrgin2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkProven, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNodau2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkGenomicTestedBulls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIsIBRBull, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPercentageVerification, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkUnderProgenyTest, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkShouldbeshowninSireDirectory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSibilingTeasted, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSonOfProvenSire, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkKaryotyping, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkGeneticDiseaseTeasting, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMilkingDone, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtWeightAtEntry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBirthWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtlDamOrigin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNodaughters, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage4.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage5.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtQuarntine, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtREARINGCentre, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNomalesborn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoabortions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalConceptions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoofelitefemalescurrentlypregnant, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoofFemaleCalves, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalHeiferConceptions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoundersemencollection, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPreQuarantine, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoofinseminationcarriedout, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoofmalecalves, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalHeiferAI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTrainingCentre, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoofmalesproduced, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDateofnominatedmatinginitiated, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalAI, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage6.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSelectPath1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
    Friend WithEvents RadPageViewPage3 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents MyLabel32 As common.Controls.MyLabel
    Friend WithEvents MyLabel28 As common.Controls.MyLabel
    Friend WithEvents lblSpecies1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents lblCountryCode As common.Controls.MyLabel
    Friend WithEvents fndCategory As common.UserControls.txtFinder
    Friend WithEvents lblCategory As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents fndSpecies As common.UserControls.txtFinder
    Friend WithEvents lblSpecies As common.Controls.MyLabel
    Friend WithEvents RadioButton2 As RadioButton
    Friend WithEvents RadioButton1 As RadioButton
    Friend WithEvents lblSemen As common.Controls.MyLabel
    Friend WithEvents lblbankcode As common.Controls.MyLabel
    Friend WithEvents fndCode As common.UserControls.txtNavigator
    Friend WithEvents lblSubCategory As common.Controls.MyLabel
    Friend WithEvents fndSubCategory As common.UserControls.txtFinder
    Friend WithEvents lbl12DigitBullId As common.Controls.MyLabel
    Friend WithEvents lblBreed As common.Controls.MyLabel
    Friend WithEvents lblPrevBullId As common.Controls.MyLabel
    Friend WithEvents fndBreed As common.UserControls.txtFinder
    Friend WithEvents lblDOB As common.Controls.MyLabel
    Friend WithEvents lblSSBullId As common.Controls.MyLabel
    Friend WithEvents lblBullStatus As common.Controls.MyLabel
    Friend WithEvents fndBullStatus As common.UserControls.txtFinder
    Friend WithEvents lblBullAliasName As common.Controls.MyLabel
    Friend WithEvents lblBullSubStatus As common.Controls.MyLabel
    Friend WithEvents fndSubStatus As common.UserControls.txtFinder
    Friend WithEvents lblExoticBullPercentage As common.Controls.MyLabel
    Friend WithEvents lblBullBookValue As common.Controls.MyLabel
    Friend WithEvents lblSSCentre As common.Controls.MyLabel
    Friend WithEvents fndSSCentre As common.UserControls.txtFinder
    Friend WithEvents lblShedId As common.Controls.MyLabel
    Friend WithEvents lblPenId As common.Controls.MyLabel
    Friend WithEvents fndShedId As common.UserControls.txtFinder
    Friend WithEvents fndPenId As common.UserControls.txtFinder
    Friend WithEvents lblStatusChangeDate As common.Controls.MyLabel
    Friend WithEvents lblExistDate As common.Controls.MyLabel
    Friend WithEvents lblBullFRID As common.Controls.MyLabel
    Friend WithEvents lblRemark As common.Controls.MyLabel
    Friend WithEvents lblBullRating As common.Controls.MyLabel
    Friend WithEvents lblDamLocation As common.Controls.MyLabel
    Friend WithEvents lblBullSourcePrintStrew As common.Controls.MyLabel
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDateOfBirth As common.Controls.MyDateTimePicker
    Friend WithEvents txtStatusDateChanged As common.Controls.MyDateTimePicker
    Friend WithEvents txtRegDate As common.Controls.MyDateTimePicker
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents TXTEndDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem4 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents fndBullRating As common.UserControls.txtFinder
    Friend WithEvents fndBullSourcePainting As common.UserControls.txtFinder
    Friend WithEvents fndCounty As common.UserControls.txtFinder
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents fndSourceName As common.UserControls.txtFinder
    Friend WithEvents txtOwnerName As common.Controls.MyTextBox
    Friend WithEvents txtSourceName As common.Controls.MyTextBox
    Friend WithEvents TXTExoticBloodPer As common.Controls.MyTextBox
    Friend WithEvents txtBullRFID As common.Controls.MyTextBox
    Friend WithEvents txtBullBook As common.Controls.MyTextBox
    Friend WithEvents TXTSSbull As common.Controls.MyTextBox
    Friend WithEvents txtBullAlias As common.Controls.MyTextBox
    Friend WithEvents txtDamLocation As common.Controls.MyTextBox
    Friend WithEvents TXTPrevBull As common.Controls.MyTextBox
    Friend WithEvents txtFirstCollectionDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtSemenPrice As common.Controls.MyTextBox
    Friend WithEvents txtPurchaseDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtPurchaseRequestDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtPurchaseNo As common.Controls.MyTextBox
    Friend WithEvents txtBreedingValue As common.Controls.MyTextBox
    Friend WithEvents txtLastDateBreeding As common.Controls.MyDateTimePicker
    Friend WithEvents txtAveregeDoses As common.Controls.MyTextBox
    Friend WithEvents txtProducedTillDate As common.Controls.MyTextBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents txtMilkingDone As common.Controls.MyTextBox
    Friend WithEvents txtWeightAtEntry As common.Controls.MyTextBox
    Friend WithEvents txtBirthWeight As common.Controls.MyTextBox
    Friend WithEvents txtlDamOrigin As common.Controls.MyTextBox
    Friend WithEvents txtNodaughters As common.Controls.MyTextBox
    Friend WithEvents RadPageViewPage4 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents chkIsIBRBull As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkPercentageVerification As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkUnderProgenyTest As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkProven As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkShouldbeshowninSireDirectory As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkSibilingTeasted As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkSonOfProvenSire As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkKaryotyping As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkGenomicTestedBulls As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkGeneticDiseaseTeasting As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadPageViewPage5 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents txtDateofnominatedmatinginitiated As common.Controls.MyTextBox
    Friend WithEvents txtTotalAI As common.Controls.MyTextBox
    Friend WithEvents txtNoofmalesproduced As common.Controls.MyTextBox
    Friend WithEvents Label37 As Label
    Friend WithEvents txtQuarntine As common.Controls.MyTextBox
    Friend WithEvents txtREARINGCentre As common.Controls.MyTextBox
    Friend WithEvents txtNomalesborn As common.Controls.MyTextBox
    Friend WithEvents txtNoabortions As common.Controls.MyTextBox
    Friend WithEvents txtTotalConceptions As common.Controls.MyTextBox
    Friend WithEvents txtNoofelitefemalescurrentlypregnant As common.Controls.MyTextBox
    Friend WithEvents txtNoofFemaleCalves As common.Controls.MyTextBox
    Friend WithEvents txtTotalHeiferConceptions As common.Controls.MyTextBox
    Friend WithEvents txtNoundersemencollection As common.Controls.MyTextBox
    Friend WithEvents txtPreQuarantine As common.Controls.MyTextBox
    Friend WithEvents txtNoofinseminationcarriedout As common.Controls.MyTextBox
    Friend WithEvents txtNoofmalecalves As common.Controls.MyTextBox
    Friend WithEvents txtTotalHeiferAI As common.Controls.MyTextBox
    Friend WithEvents txtTrainingCentre As common.Controls.MyTextBox
    Friend WithEvents gv2 As common.UserControls.MyRadGridView
    Friend WithEvents Panel1 As Panel
    Friend WithEvents RadPageViewPage6 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents btnSelectPath1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents txtRemark As common.Controls.MyTextBox
    Friend WithEvents IsSemen As common.Controls.MyLabel
    Friend WithEvents lblspecies2 As common.Controls.MyLabel
    Friend WithEvents lblExotic2 As common.Controls.MyLabel
    Friend WithEvents lblBullRFID2 As common.Controls.MyLabel
    Friend WithEvents lblCategory2 As common.Controls.MyLabel
    Friend WithEvents lblRegistrationDate As common.Controls.MyLabel
    Friend WithEvents lblBullBookValue2 As common.Controls.MyLabel
    Friend WithEvents lblBreed2 As common.Controls.MyLabel
    Friend WithEvents lblCountryCode2 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents lblSubCategory2 As common.Controls.MyLabel
    Friend WithEvents lblSSBullId2 As common.Controls.MyLabel
    Friend WithEvents lblBullAliasName2 As common.Controls.MyLabel
    Friend WithEvents lblSScentre2 As common.Controls.MyLabel
    Friend WithEvents lblShedId2 As common.Controls.MyLabel
    Friend WithEvents lblPenId2 As common.Controls.MyLabel
    Friend WithEvents lblBullRating2 As common.Controls.MyLabel
    Friend WithEvents lblDamLoactionYield2 As common.Controls.MyLabel
    Friend WithEvents lblBullSourcePainting2 As common.Controls.MyLabel
    Friend WithEvents lblPrevBullId2 As common.Controls.MyLabel
    Friend WithEvents lblBullStatus2 As common.Controls.MyLabel
    Friend WithEvents lblBullSubStatus2 As common.Controls.MyLabel
    Friend WithEvents lblRemark2 As common.Controls.MyLabel
    Friend WithEvents lblcode2 As common.Controls.MyLabel
    Friend WithEvents lblStatusChangedDate2 As common.Controls.MyLabel
    Friend WithEvents lblExitDate2 As common.Controls.MyLabel
    Friend WithEvents lblDateOfBirth2 As common.Controls.MyLabel
    Friend WithEvents lblPurchaseRequestNo2 As common.Controls.MyLabel
    Friend WithEvents lblSourceAddress2 As common.Controls.MyLabel
    Friend WithEvents lblOwnerOfBull2 As common.Controls.MyLabel
    Friend WithEvents lblFirstCollectionDate2 As common.Controls.MyLabel
    Friend WithEvents lblSemenPrice2 As common.Controls.MyLabel
    Friend WithEvents lblDosesProducedTillDate2 As common.Controls.MyLabel
    Friend WithEvents lblCapacityOfAverageMonthlyDoses As common.Controls.MyLabel
    Friend WithEvents lblLastdayBreeding As common.Controls.MyLabel
    Friend WithEvents lblSourceName2 As common.Controls.MyLabel
    Friend WithEvents lblBreedingValue2 As common.Controls.MyLabel
    Friend WithEvents lblPurchaseRequestDate2 As common.Controls.MyLabel
    Friend WithEvents lblPurchaseDate2 As common.Controls.MyLabel
    Friend WithEvents lblNodau2 As common.Controls.MyLabel
    Friend WithEvents lblDamOrgin2 As common.Controls.MyLabel
    Friend WithEvents lblBirthWeight2 As common.Controls.MyLabel
    Friend WithEvents lblWeight2 As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents MyLabel17 As common.Controls.MyLabel
    Friend WithEvents MyLabel18 As common.Controls.MyLabel
    Friend WithEvents MyLabel19 As common.Controls.MyLabel
    Friend WithEvents MyLabel21 As common.Controls.MyLabel
    Friend WithEvents MyLabel20 As common.Controls.MyLabel
End Class
