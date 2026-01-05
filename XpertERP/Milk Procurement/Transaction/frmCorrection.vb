Imports common
Imports System.Data.SqlClient
Public Class frmCorrection
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Public errorControl As clsErrorControl = New clsErrorControl()
    Dim isPickServerDateWithNoChange As Boolean = False
    Dim dclCorrectionFactor As Decimal = 0
    Dim MilkWeight_Setting As Double
    Dim settMaxReceiveSNFPer As Decimal = 0
    Dim settMaxFATPerLimit As Decimal = 0
    Dim settMaxSNFPerLimit As Decimal = 0
    Dim IsRoundOffPaiseAmount As Boolean

    Dim MultipleFinderFillAuto As Boolean = False
    Dim SettMilkCollectionFATSNFType As Integer
    Dim SettFATSNFNoDecimalMCC As Boolean
    Dim SettShowAllMCC As Boolean
    Dim corrFactor As Decimal = 0
    Dim isPickCLRInsteadOfSNF As Boolean = False
    Dim DtError As DataTable
    Dim dr As DataRow
    Dim isCorrection As Integer = 0
    Dim StrPermission As String
    Dim Remark As String

    Const colSAVLCUploaderCode As String = "colSAVLCUploaderCode"
    Const colSAVLCCode As String = "colSAVLCCode"
    Const colSAVLCName As String = "colSAVLCName"
    Const colOwnDCS As String = "colOwnDCS"
    Const colSAQty As String = "colSAQty"
    Const colSAFATPer As String = "colSAFATPer"
    Const colSASNFPer As String = "colSASNFPer"
    Const colSAFATKG As String = "colSAFATKG"
    Const colSASNFKG As String = "colSASNFKG"
    Const colSARate As String = "colSARate"
    Const colSAAmount As String = "colSAAmount"
    Dim SettShowAllDCS As Boolean
#End Region

    Private Sub frmMilkGateEntryIn_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim id As String = Form_ID
        Try
            Dim coll As Dictionary(Of String, String)
            coll = New Dictionary(Of String, String)
            coll.Add("PK_Id", "integer NOT NULL identity NOT FOR REPLICATION primary key")
            coll.Add("Document_No", "Varchar(30) not null references TSPL_MILK_COLLECTION_DCS(Document_No)")
            coll.Add("VLC_Code", "Varchar(30) not null references TSPL_VLC_MASTER_HEAD(VLC_Code)")
            coll.Add("Qty", "Decimal(18,2) null")
            coll.Add("FAT", "Decimal(18,2) null")
            coll.Add("SNF", "Decimal(18,2) null")
            coll.Add("FATKG", "Decimal(18,3) null")
            coll.Add("SNFKG", "Decimal(18,3) null")
            coll.Add("Rate", "Decimal(18,2) null")
            coll.Add("Amount", "Decimal(18,2) null")
            coll.Add("Created_By", "varchar(12)  Not NULL")
            coll.Add("Created_Date", "datetime  Not NULL")
            clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_MILK_COLLECTION_DCS_SUSPENSE_ADJUSTMENT", coll, Nothing, False, False, "", "", "", False)

            coll = New Dictionary(Of String, String)()
            coll.Add("Is_Suspense_Adjustment_CR_Note", "integer not null default 0")
            coll.Add("Is_Suspense_Adjustment_DR_Note", "integer not null default 0")
            clsCommonFunctionality.CreateOrAlterTable(False, False, "TSPL_DEDUCTION_MASTER", coll, "", True, False, "", "", "", True)


            SettShowAllDCS = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowAllDCS, clsFixedParameterCode.ShowAllDCS, Nothing))
            SettMilkCollectionFATSNFType = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkCollectionFATSNFType, clsFixedParameterCode.MilkCollectionFATSNFType, Nothing))
            SettFATSNFNoDecimalMCC = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.FATSNFNoDecimalMCC, clsFixedParameterCode.FATSNFNoDecimalMCC, Nothing))
            SettShowAllMCC = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowAllMCC, clsFixedParameterCode.ShowAllMCC, Nothing))
            MultipleFinderFillAuto = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MultipleFinderFillAuto, clsFixedParameterCode.MultipleFinderFillAuto, Nothing)) = 1)
            MilkWeight_Setting = clsFixedParameter.GetData(clsFixedParameterType.Milk_Can_Weight_Ratio, clsFixedParameterCode.MilkSetting, Nothing)
            dclCorrectionFactor = clsFixedParameter.GetData(clsFixedParameterType.defaultCorrectionFactor, clsFixedParameterCode.MilkSetting, Nothing)
            settMaxReceiveSNFPer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MaxReceiveSNFPer, clsFixedParameterCode.MaxReceiveSNFPer, Nothing))
            settMaxFATPerLimit = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MaxFATPerLimit, clsFixedParameterCode.MaxFATPerLimit, Nothing))
            settMaxSNFPerLimit = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MaxSNFPerLimit, clsFixedParameterCode.MaxSNFPerLimit, Nothing))
            IsRoundOffPaiseAmount = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RoundOffPaiseAmount, clsFixedParameterCode.RoundOffPaiseAmount, Nothing)) = 1
            corrFactor = clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.defaultCorrectionFactor, clsFixedParameterCode.MilkSetting, Nothing))
            isPickCLRInsteadOfSNF = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProcuremntPickCLRInsteadOfSNF, clsFixedParameterCode.MilkProcuremntPickCLRInsteadOfSNF, Nothing)) > 0)
            RadPageView1.Pages("RadPageViewPage7").Item.Visibility = ElementVisibility.Collapsed
            If clsCommon.CompairString(Form_ID, clsUserMgtCode.MilkProcurementCorrection) = CompairStringResult.Equal Then
                'If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDP") = CompairStringResult.Equal Then
                chkAddMissingSample.Visible = False
                chkAdjustOwnBMCFATSNF.Visible = True
                chkDeleteBMCCollection.Visible = True
                txtFromShift.Enabled = True
                chkAvgFATSNF.Visible = False
                If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "GNG") = CompairStringResult.Equal Then
                    chkAvgFATSNF.Visible = True
                    chkAdjustOwnBMCFATSNF.Visible = False
                End If
                'Else
                '    chkAddMissingSample.Visible = True
                '    chkAdjustOwnBMCFATSNF.Visible = False
                '    chkDeleteBMCCollection.Visible = False
                '    txtFromShift.Enabled = True
                '    chkAvgFATSNF.Visible = False
                'End If

                RadPageView1.Pages("RadPageViewPage2").Item.Visibility = ElementVisibility.Collapsed
                If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "JPR") = CompairStringResult.Equal Then
                    RadPageView1.Pages("RadPageViewPage7").Item.Visibility = ElementVisibility.Visible
                    chkDeleteDCSCollection.Visible = True
                End If

                SetUserMgmtNew()
                LoadMilkType()
                LoadMilkTypeBMC()
                LoadShiftWithBoth()
                LoadShift()
                LoadShiftCAP()
                LoadMilkTypeCAP()

                StrPermission = clsERPFuncationality.UserWiseAvailableLocationCode()
                ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
                ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
                txtShiftDate.Value = clsCommon.GETSERVERDATE()
                txtBMCDate.Value = txtShiftDate.Value
                txtBMCTankerDate.Value = txtShiftDate.Value
                txtCAPShiftDate.Value = txtShiftDate.Value
                cboShift.SelectedValue = "M"
                RadGroupBox1.Enabled = False
                RadGroupBox2.Enabled = True
                txtShiftDate.Focus()

                RadGroupBox3.Enabled = True
                RadGroupBox4.Enabled = False
                RadButton7.PerformClick()

                txtVLCCMFromDate.Value = txtShiftDate.Value
                txtVLCCMToDate.Value = txtShiftDate.Value
                txtMPCMFromDate.Value = txtShiftDate.Value
                txtMPCMToDate.Value = txtShiftDate.Value
                txtMCC.Visible = Not MultipleFinderFillAuto
                lblMcc.Visible = Not MultipleFinderFillAuto
                lblMCCCode.Visible = Not MultipleFinderFillAuto

                txtCAPMCC.Visible = Not MultipleFinderFillAuto
                lblCAPMcc.Visible = Not MultipleFinderFillAuto
                MyLabel31.Visible = Not MultipleFinderFillAuto

                LoadShiftFrom()
                chkRetesting.Checked = False
                chkRetesting.Visible = False
                chkCorrection.Visible = True
                chkCorrection.Checked = True
                RadPageView1.SelectedPage = RadPageViewPage1
                btnExport.Visible = False
                btnImport.Visible = False
                btnTankerMilkExport.Visible = False
                btnTankerMilkImport.Visible = False
                txtBMCTankerQty.ReadOnly = False
                txtBMCCorrQty.ReadOnly = False

            ElseIf clsCommon.CompairString(Form_ID, clsUserMgtCode.MilkRetesting) = CompairStringResult.Equal Then

                If isPickCLRInsteadOfSNF Then
                    txtRetestingCLR.Visible = True
                    MyLabel22.Visible = True
                Else
                    txtRetestingCLR.Visible = False
                    MyLabel22.Visible = False
                End If
                chkRetesting.Visible = False
                chkRetesting.Checked = False
                chkCorrection.Visible = False
                chkCorrection.Checked = False
                txtShiftDate.Value = clsCommon.GETSERVERDATE()
                txtBMCDate.Value = txtShiftDate.Value
                txtBMCTankerDate.Value = clsCommon.GETSERVERDATE()
                cboShift.SelectedValue = "M"
                RadGroupBox1.Enabled = False
                RadGroupBox2.Enabled = True
                txtShiftDate.Focus()
                LoadShift()
                LoadMilkTypeBMC()
                chkAddMissingSample.Visible = False
                RadPageViewPage1.Text = "Milk Retesting"
                RadGroupBox1.HeaderText = "Milk Retesting"
                RadPageView1.Pages("RadPageViewPage1").Item.Visibility = ElementVisibility.Collapsed
                RadPageView1.Pages("RadPageViewPage2").Item.Visibility = ElementVisibility.Collapsed
                RadPageView1.Pages("RadPageViewPage3").Item.Visibility = ElementVisibility.Collapsed
                'RadPageView1.Pages("RadPageViewPage4").Item.Visibility = ElementVisibility.Collapsed
                RadPageView1.Pages("RadPageViewPage5").Item.Visibility = ElementVisibility.Collapsed
                RadPageViewPage4.Text = "BMC Milk Retesting"
                RadGroupBox4.HeaderText = "Retesting"
                RadPageView1.SelectedPage = RadPageViewPage4
                btnExport.Visible = True
                btnImport.Visible = True
                btnTankerMilkExport.Visible = True
                btnTankerMilkImport.Visible = True
                chkMarkAsSuspence.Visible = False
                chkMarkAsAdulteration.Visible = False
            End If

            If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "AJM") = CompairStringResult.Equal Then
                chkMarkAsAdulteration.Visible = True

            End If

            If clsCommon.CompairString(MyBase.Form_ID, clsUserMgtCode.MilkProcurementCorrection) = CompairStringResult.Equal Then
                isCorrection = 1
            ElseIf clsCommon.CompairString(MyBase.Form_ID, clsUserMgtCode.MilkRetesting) = CompairStringResult.Equal Then
                isCorrection = 2
            Else
                isCorrection = 0
            End If
            LoadReject()
            'RadPageView1.SelectedPage = RadPageViewPage1
            If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "UDP") = CompairStringResult.Equal Then
                chkAdjustOwnBMCFATSNF.Visible = True
            Else
                chkAdjustOwnBMCFATSNF.Visible = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub LoadReject()
        Dim qry As String = "select Code,description as Name from TSPL_MILK_REJECT_TYPE order by SNo"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim dr As DataRow = dt.NewRow
            dr("Code") = ""
            dr("Name") = "Good"
            dt.Rows.InsertAt(dr, 0)
        End If
        cboRejectType.DataSource = dt.Copy()
        cboRejectType.ValueMember = "Code"
        cboRejectType.DisplayMember = "Name"


        cboCAPRejectType.DataSource = dt.Copy()
        cboCAPRejectType.ValueMember = "Code"
        cboCAPRejectType.DisplayMember = "Name"
    End Sub

    Sub LoadShiftFrom()
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Shift")

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "B"
        dr("Shift") = "Both"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "M"
        dr("Shift") = "Morning"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "E"
        dr("Shift") = "Evening"
        dt.Rows.Add(dr)

        txtFromShift.DataSource = dt
        txtFromShift.ValueMember = "Code"

        txtMCCFromDate.Value = DateTime.Now
        txtMCCToDate.Value = txtMCCFromDate.Value
    End Sub
    Sub LoadMilkType()
        If objCommonVar.DisplayTypeInMilkReceipt Then
            cboMilkType.DataSource = clsMilkReceiptMCC.GetMilkType()
        Else
            cboMilkType.DataSource = clsMilkReceiptMCC.GetDockCollectionMilkType(True)
        End If
        cboMilkType.ValueMember = "Code"
        cboMilkType.DisplayMember = "Name"


    End Sub

    Sub LoadMilkTypeCAP()
        If objCommonVar.DisplayTypeInMilkReceipt Then
            cboCAPMilkType.DataSource = clsMilkReceiptMCC.GetMilkType()
        Else
            cboCAPMilkType.DataSource = clsMilkReceiptMCC.GetDockCollectionMilkType(True)
        End If
        cboCAPMilkType.ValueMember = "Code"
        cboCAPMilkType.DisplayMember = "Name"


    End Sub

    Sub LoadMilkTypeBMC()
        cboBMCCorrMilkType.DataSource = clsMilkReceiptMCC.GetReject(True)
        cboBMCCorrMilkType.ValueMember = "Code"
        cboBMCCorrMilkType.DisplayMember = "Name"
    End Sub
    Private Sub LoadShiftCAP()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "M"
        dr("Name") = "Morning"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "E"
        dr("Name") = "Evening"
        dt.Rows.Add(dr)

        cboCAPShift.DataSource = dt
        cboCAPShift.ValueMember = "Code"
        cboCAPShift.DisplayMember = "Name"
    End Sub
    Private Sub LoadShift()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "M"
        dr("Name") = "Morning"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "E"
        dr("Name") = "Evening"
        dt.Rows.Add(dr)

        cboShift.DataSource = dt
        cboShift.ValueMember = "Code"
        cboShift.DisplayMember = "Name"
    End Sub

    Sub LoadShiftWithBoth()
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Shift")

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "B"
        dr("Shift") = "Both"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "M"
        dr("Shift") = "Morning"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "E"
        dr("Shift") = "Evening"
        dt.Rows.Add(dr)

        cboMPCMShift.DataSource = dt.Copy
        cboMPCMShift.ValueMember = "Code"

        cboVLCCMShift.DataSource = dt.Copy
        cboVLCCMShift.ValueMember = "Code"
    End Sub


    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnExport.Visible = MyBase.isExport
        btnImport.Visible = MyBase.isExport
    End Sub

    Private Sub txtMCC__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtMCC._MYValidating
        Dim qry As String = ""
        Dim arrMCCRights As ArrayList = clsMCCCodes.GetUserHavingMCCRights()

        qry = "select * from ( select tspl_mcc_master.MCC_Code as [Code] ,tspl_mcc_master.MCC_Type as [Mcc Type] ,tspl_mcc_master.MCC_NAME as [Mcc Name] ,tspl_mcc_master.Chilling_Vendor as [Chilling Vendor] ,tspl_mcc_master.Add1 as [Address1] ,tspl_mcc_master.Add2 as [Address2] ,tspl_mcc_master.Tehsil as [Tehsil] ,tspl_mcc_master.City_code as [City Code] ,tspl_mcc_master.State_Code as [State Code] ,tspl_mcc_master.Country_code as [Country Code] ,tspl_mcc_master.Pin_code as [Pin Code],tspl_mcc_master.Pan_No as [Pan No] ,tspl_mcc_master.Telphone as [Telphone] ,tspl_mcc_master.Email as [Email] ,tspl_mcc_master.Fax as [Fax] ,tspl_mcc_master.MCC_Area as [Mcc Area] ,tspl_mcc_master.Area_Of_Store as [Area Of Store] ,tspl_mcc_master.Area_Of_Office as [Area Of Office] ,tspl_mcc_master.Open_Area_For_tanker as [Open Area For Tanker] ,tspl_mcc_master.Area_Of_LAB as [Area Of Lab] ,tspl_mcc_master.No_Of_SILO as [No Of Silo] ,tspl_mcc_master.Total_Storage_capacity as [Total Storage Capacity] ,tspl_mcc_master.Area_Of_Receiving_DOCK as [Area Of Receiving Dock] ,tspl_mcc_master.No_Of_Chiller as [No Of Chiller] ,tspl_mcc_master.Chiller_Brand_Name as [Chiller Brand Name] ,tspl_mcc_master.Chiller_Capacity as [Chiller Capacity] ,tspl_mcc_master.No_Of_MilkPump as [No Of Milkpump] ,tspl_mcc_master.MilkPump_Capacity as [Milkpump Capacity] ,tspl_mcc_master.DripSaver as [Drip Saver] ,tspl_mcc_master.CanWasher as [Can Washer] ,tspl_mcc_master.CanScrubber as [Can Scrubber] ,tspl_mcc_master.FSSAI_NO as [FSSAI No] ,tspl_mcc_master.ETP as [ETP] ,tspl_mcc_master.Earthing as [Earthing] ,tspl_mcc_master.Coil_Length as [Coil Length] ,tspl_mcc_master.Electricity_Connection as [Electricity Connection] ,tspl_mcc_master.Boiler as [Boiler] ,tspl_mcc_master.NoOfDG as [No. of DG] ,tspl_mcc_master.NoOfCompressor as [No. of Compressor] ,tspl_mcc_master.PayeeName as [Payee Name] ,tspl_mcc_master.BankName as [Bank Name] ,tspl_mcc_master.BankBranch as [Bank Branch] ,tspl_mcc_master.BankCityCode as [Bank City Code] ,tspl_mcc_master.BankStateCode as [Bank State Code] ,tspl_mcc_master.IFCICode as [IFCI Code] ,tspl_mcc_master.AccountNO as [Account No] ,tspl_mcc_master.Created_By as [Created By] ,tspl_mcc_master.Created_Date as [Created Date] ,tspl_mcc_master.Modified_By as [Modified By] ,tspl_mcc_master.Modified_Date as [Modified Date] ,tspl_mcc_master.Comp_Code as [Company Code],tspl_mcc_master.mcc_code_vlc_uploader as [MCC Code For VLC Uploder],tspl_mcc_master.Plant_Code AS [Plant Code],TSPL_LOCATION_MASTER_PLANT.Location_Desc AS [Plant Name] from tspl_mcc_master LEFT JOIN TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_PLANT ON TSPL_LOCATION_MASTER_PLANT.Location_Code=tspl_mcc_master.Plant_Code  inner join tspl_location_master on tspl_location_master.location_Code= tspl_mcc_master.mcc_Code where tspl_mcc_master.mcc_Code in (" & StrPermission & ") " _
        & " and (  tspl_mcc_master.mcc_Code in (" & clsCommon.GetMulcallString(arrMCCRights) & ")))xx "

        txtMCC.Value = clsCommon.ShowSelectForm("frmCorrection@MCC", qry, "Code", "", txtMCC.Value, "", isButtonClicked)
        If txtMCC.Value IsNot Nothing AndAlso clsCommon.myLen(txtMCC.Value) > 0 Then
            lblMcc.Text = clsDBFuncationality.getSingleValue(" select MCC_NAME from TSPL_Mcc_MASTER where MCC_Code = '" + txtMCC.Value + "'", Nothing)
        End If
    End Sub

    Private Sub txtRoute__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtVLC._MYValidating
        vlcUploaderFinder(txtVLC, lblVLC, isButtonClicked)
    End Sub

    Sub vlcUploaderFinder(ByVal finder As common.UserControls.txtFinder, ByVal label As common.Controls.MyLabel, ByVal isButtonClicked As Boolean)
        Try
            If clsCommon.myLen(txtMCC.Value) <= 0 AndAlso Not MultipleFinderFillAuto Then
                txtMCC.Focus()
                Throw New Exception("Please provide MCC code ")
            End If
            Dim qry As String = "select TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [Uploader_Code], TSPL_VLC_MASTER_HEAD.VLC_Code AS [DCS Code],TSPL_VLC_MASTER_HEAD.VLC_Name as [DCS NAME], TSPL_VLC_MASTER_HEAD.Route_Code as [Route Code],TSPL_MCC_ROUTE_MASTER.Route_Name,TSPL_MCC_ROUTE_MASTER.Vehicle_Code,TSPL_VLC_MASTER_HEAD.VSP_Code AS [Secretary Code],TSPL_VENDOR_MASTER.Vendor_Name as [Secretary Name] " + Environment.NewLine +
            " from TSPL_VLC_MASTER_HEAD" + Environment.NewLine +
            " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code  " + Environment.NewLine +
            " left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_VLC_MASTER_HEAD.Route_Code  " + Environment.NewLine +
            " left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC " + Environment.NewLine

            Dim whrCls As String = "  isnull(TSPL_VLC_MASTER_HEAD.IsSuspense,0)=0  "
            If Not MultipleFinderFillAuto Then
                whrCls += " and TSPL_VLC_MASTER_HEAD.MCC  ='" + txtMCC.Value + "'"
            End If

            finder.Value = clsCommon.ShowSelectForm("SMSRNUdC", qry, "Uploader_Code", whrCls, finder.Value, "Uploader_Code", isButtonClicked)
            qry = "select TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VLC_MASTER_HEAD.MCC from TSPL_VLC_MASTER_HEAD where TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader='" + finder.Value + "' "
            If Not MultipleFinderFillAuto Then
                whrCls += "  TSPL_VLC_MASTER_HEAD.MCC  ='" + txtMCC.Value + "'"
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                finder.Tag = clsCommon.myCstr(dt.Rows(0)("VLC_Code"))
                label.Text = clsCommon.myCstr(dt.Rows(0)("VLC_Name"))
                If MultipleFinderFillAuto Then
                    txtMCC.Value = clsCommon.myCstr(dt.Rows(0)("MCC"))
                End If
            Else
                finder.Tag = Nothing
                label.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub frmMilkGateEntryIn_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnclose.Enabled Then
            CloseForm()
        ElseIf e.Shift AndAlso e.Alt AndAlso e.KeyCode = Keys.D Then
            Dim pwd As New FrmPWD(Nothing)
            pwd.strCode = clsFixedParameterCode.DeleteMccMilkShiftPassword
            pwd.strType = clsFixedParameterType.DeleteMccMilkShiftPassword
            pwd.ShowDialog()
            If pwd.isPasswordCorrect Then
                RadPageView1.Pages("RadPageViewPage2").Item.Visibility = ElementVisibility.Visible
                RadPageView1.SelectedPage = RadPageViewPage2
            Else
                RadPageView1.Pages("RadPageViewPage2").Item.Visibility = ElementVisibility.Collapsed
                RadPageView1.SelectedPage = RadPageViewPage1
            End If
            'ElseIf e.Alt AndAlso e.Control AndAlso e.Shift AndAlso e.KeyCode = Keys.F7 Then
            '    Dim pwd As New FrmPWD(Nothing)
            '    pwd.strCode = clsFixedParameterCode.MilkProcuremntPickCLRInsteadOfSNF
            '    pwd.strType = clsFixedParameterType.MilkProcuremntPickCLRInsteadOfSNF
            '    pwd.ShowDialog()
            '    If pwd.isPasswordCorrect Then
            '        Dim frm As New frmSetting
            '        frm.strFormID = Me.Form_ID
            '        frm.ShowDialog()
            '        If frm.isDataSaved Then
            '            clsCommon.MyMessageBoxShow("Setting saved successfully." + Environment.NewLine + Me.Text + " will close automatic For apply new settings")
            '            clsERPFuncationality.closeForm(Me)
            '        End If
            '    End If
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If clsCommon.CompairString(Form_ID, clsUserMgtCode.MilkRetesting) = CompairStringResult.Equal Then
            ShowRemarks()
        Else
            SaveData()
        End If
    End Sub
    Private Sub ShowRemarks()
        Try
            Dim obj As New clsMilkSRNMCC
            Dim qry As String = ""
            Dim Reason As String = ""
            Dim frm As New FrmFreeTxtBox1
            frm.Text = "Remarks for Retesting"
            frm.ShowDialog()
            If clsCommon.myLen(frm.strRmks) <= 0 Then
                Exit Sub
            Else
                If frm.strRmks IsNot Nothing Then
                    obj.Reason = "1"

                End If
            End If
            SaveData()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SaveData()
        Try
            If isNewEntry = False AndAlso clsCommon.CompairString(objCommonVar.CurrComp_Code1, "JPR") = CompairStringResult.Equal Then
                'Dim obj As New clsMilkCollectionMCC
                Dim qry As String = ""
                Dim Reason As String = ""
                Dim frm As New FrmFreeTxtBox1
                frm.Text = "Remarks for Update"
                frm.ShowDialog()
                If clsCommon.myLen(frm.strRmks) <= 0 Then
                    Exit Sub
                Else
                    Remark = frm.strRmks
                End If
            End If
            If chkAddMissingSample.Checked Then
                If txtQty.Value <= 0 Then
                    txtQty.Focus()
                    Throw New Exception("Please Enter Qty")
                End If
                If txtFAT.Value <= 0 Then
                    txtFAT.Focus()
                    Throw New Exception("Please Enter FAT %")
                End If
                If txtSNF.Value <= 0 Then
                    txtSNF.Focus()
                    Throw New Exception("Please Enter SNF %")
                End If
                If clsCommon.myLen(cboMilkType.SelectedValue) <= 0 Then
                    cboMilkType.Focus()
                    Throw New Exception("Please Enter Milk Type")
                End If
                If clsCommon.myLen(TxtFinder1.Value) <= 0 OrElse clsCommon.myLen(TxtFinder1.Tag) <= 0 Then
                    TxtFinder1.Focus()
                    Throw New Exception("Please Enter VLC")
                End If
                If clsCommon.myLen(txtRoute.Value) <= 0 Then
                    txtRoute.Focus()
                    Throw New Exception("Please Enter route")
                End If
                Dim obj As New clsMilkProcurementUploaderHead()
                obj.Document_No = "" ''To be Generated
                obj.Document_Date = clsCommon.GETSERVERDATE()
                obj.Description = "Missing Sample Added By " + objCommonVar.CurrentUserCode + "[" + objCommonVar.CurrentUser + "]"
                obj.MCC_Code = txtMCC.Value
                obj.Dock_Code = ""
                obj.Reject = False
                obj.Arr = New List(Of clsMilkProcurementUploaderDetail)

                Dim objTr As New clsMilkProcurementUploaderDetail()
                objTr.SNo = 1
                objTr.Shift_Date = txtShiftDate.Value
                objTr.Shift = clsCommon.myCstr(cboShift.SelectedValue)
                objTr.Dock_Collection_Milk_Type = clsCommon.myCstr(cboMilkType.SelectedValue)
                objTr.VLC_Code = clsCommon.myCstr(TxtFinder1.Tag)
                objTr.No_Of_Cans = 1
                objTr.Milk_Weight = txtQty.Value
                objTr.FAT = Math.Round(txtFAT.Value, 1, MidpointRounding.ToEven)
                objTr.SNF = Math.Round(txtSNF.Value, IIf(objCommonVar.MilkProcurementSNF2DecimalPlaces, 2, 1), MidpointRounding.ToEven)
                'objTr.Reject_Defaulter = clsCommon.myCstr(gv1.Rows(ii).Cells(colRejectDefaulter).Value)
                objTr.Reject_Type = clsCommon.myCstr(cboRejectType.SelectedValue)
                objTr.Bulk_Route_Code = txtRoute.Value
                'objTr.arrQCParameter = GetParamCollection(ii)
                obj.Arr.Add(objTr)
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    Throw New Exception("Please Fill at list one Item")
                End If
                Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin
                Try
                    obj.SaveData(obj, True, tran)
                    clsMilkProcurementUploaderHead.PostData(obj.Document_No, tran)
                    tran.Commit()
                Catch ex As Exception
                    tran.Rollback()
                    Throw New Exception(ex.Message)
                End Try
                Dim qry As String = "Select TSPL_MILK_SRN_HEAD.DOC_CODE from  TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL" + Environment.NewLine +
                "left outer join TSPL_MILK_RECEIPT_DETAIL On TSPL_MILK_RECEIPT_DETAIL.Against_Uploader_TR_No=TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No" + Environment.NewLine +
                "left outer join TSPL_MILK_SAMPLE_HEAD On TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE=TSPL_MILK_RECEIPT_DETAIL.DOC_CODE" + Environment.NewLine +
                "left outer join TSPL_MILK_SRN_HEAD On TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE=TSPL_MILK_SAMPLE_HEAD.DOC_CODE And TSPL_MILK_SRN_HEAD.SAMPLE_NO=TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO" + Environment.NewLine +
                "where TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No='" + obj.Document_No + "'"
                lblSRNNo.Text = clsDBFuncationality.getSingleValue(qry)
                If clsCommon.myLen(lblSRNNo.Text) > 0 Then
                    chkAddMissingSample.Checked = False
                End If
            Else
                If clsCommon.myLen(txtSuspenceRemarks.Text) <= 0 Then
                    If chkMarkAsAdulteration.Checked Then
                        txtSuspenceRemarks.Focus()
                        Throw New Exception("Please fill remarks")
                    Else
                        txtSuspenceRemarks.Text = ""
                    End If
                End If

                Dim CorrTypeSRNQty As Boolean = True
                Dim CorrTypeSRNFATSNF As Boolean = True
                Dim CorrTypeSRNVLC As Boolean = True

                If clsCommon.myCdbl(txtQty.Tag) = txtQty.Value Then
                    CorrTypeSRNQty = False
                End If
                If clsCommon.myCdbl(txtFAT.Tag) = txtFAT.Value AndAlso clsCommon.myCdbl(txtSNF.Tag) = txtSNF.Value AndAlso clsCommon.CompairString(clsCommon.myCstr(cboMilkType.Tag), clsCommon.myCstr(cboMilkType.SelectedValue)) = CompairStringResult.Equal Then
                    CorrTypeSRNFATSNF = False
                End If
                If clsCommon.CompairString(clsCommon.myCstr(TxtFinder1.Tag), clsCommon.myCstr(txtVLC.Tag)) = CompairStringResult.Equal Then
                    CorrTypeSRNVLC = False
                End If
                Dim qry As String = "select TSPL_MILK_COLLECTION_DCS_DETAIL.Suspence_VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VLC_MASTER_HEAD.VLC_Name
from TSPL_MILK_SRN_DETAIL
left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE
left outer join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Uploader_TR_No
left outer join TSPL_MILK_SHIFT_UPLOADER_DETAIL on TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Shift_Uploader_TR_No 
left outer join TSPL_MILK_COLLECTION_DCS_DETAIL on TSPL_MILK_COLLECTION_DCS_DETAIL.PK_Id=TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Against_Milk_Collection_DCS_Detail or TSPL_MILK_COLLECTION_DCS_DETAIL.PK_Id=TSPL_MILK_SHIFT_UPLOADER_DETAIL.Against_Milk_Collection_DCS_Detail
left outer join TSPL_VLC_MASTER_HEAD  on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_COLLECTION_DCS_DETAIL.Suspence_VLC_Code
where TSPL_MILK_SRN_HEAD.DOC_CODE='" + lblSRNNo.Text + "' and TSPL_MILK_COLLECTION_DCS_DETAIL.Suspence=1 "
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Suspence_VLC_Code")), clsCommon.myCstr(TxtFinder1.Tag)) = CompairStringResult.Equal Then
                        qry = "SRN [" + lblSRNNo.Text + "] is belongs to suspence.Original DCS is " + clsCommon.myCstr(dt.Rows(0)("VLC_Code_VLC_Uploader")) + " [" + clsCommon.myCstr(dt.Rows(0)("VLC_Name")) + "] is diffent from selected DCS " + Environment.NewLine + " Do you want to continue ? "
                        If clsCommon.MyMessageBoxShow(Me, qry, Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error) Then
                            Exit Sub
                        End If
                    End If
                End If
                Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin
                Try
                    clsMilkSRNMCC.Correction(lblSRNNo.Text, CorrTypeSRNQty, CorrTypeSRNFATSNF, CorrTypeSRNVLC, txtQty.Value, clsCommon.myCstr(cboMilkType.SelectedValue), txtFAT.Value, txtSNF.Value, TxtFinder1.Value, False, tran, False, Form_ID, clsCommon.myCstr(cboRejectType.SelectedValue), Remark, chkMarkAsSuspence.Checked, chkMarkAsAdulteration.Checked, txtSuspenceRemarks.Text, txtRoute.Value)
                    tran.Commit()
                Catch ex As Exception
                    tran.Rollback()
                    Throw New Exception(ex.Message)
                End Try
            End If
            clsCommon.MyMessageBoxShow(Me, "Data corrected sucessfully", Me.Text)
            btnSave.Enabled = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        CloseForm()
    End Sub

    Sub CloseForm()
        Me.Close()
        GC.Collect()
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Try
            If clsCommon.myLen(txtMCC.Value) <= 0 AndAlso Not MultipleFinderFillAuto Then
                txtMCC.Focus()
                Throw New Exception("Please enter MCC")
            End If
            If clsCommon.myLen(cboShift.SelectedValue) <= 0 Then
                cboShift.Focus()
                Throw New Exception("Please Select shift")
            End If
            If chkAddMissingSample.Checked Then
                RadGroupBox2.Enabled = False
                RadGroupBox1.Enabled = True
                Exit Sub
            End If
            If clsCommon.myLen(txtVLC.Tag) <= 0 Then
                txtVLC.Focus()
                Throw New Exception("Please enter VLC")
            End If
            Dim qry As String = Nothing
            qry = "select TSPL_MILK_SRN_HEAD.DOC_CODE as SRNNo,TSPL_MILK_SRN_HEAD.Dock_Collection_Milk_Type as MilkType
,TSPL_MILK_SRN_DETAIL.Qty,TSPL_MILK_SRN_DETAIL.UOM_Code,TSPL_MILK_SRN_DETAIL.FAT_PER,TSPL_MILK_SRN_DETAIL.SNF_PER
,TSPL_MILK_SRN_DETAIL.Retesting_FAT,TSPL_MILK_SRN_DETAIL.Retesting_SNF,TSPL_MILK_SRN_DETAIL.Retesting_OR_Correction_Status
,(Case When Retesting_OR_Correction_Status=1 Then TSPL_MILK_SRN_DETAIL.Retesting_FAT Else (Case When Retesting_OR_Correction_Status=2 Then TSPL_MILK_SRN_DETAIL.FAT_PER Else TSPL_MILK_SRN_DETAIL.Retesting_FAT End)End) As FAT
,(Case When Retesting_OR_Correction_Status=1 Then TSPL_MILK_SRN_DETAIL.Retesting_SNF Else (Case When Retesting_OR_Correction_Status=2 Then TSPL_MILK_SRN_DETAIL.SNF_PER Else TSPL_MILK_SRN_DETAIL.Retesting_SNF End) End) As SNF 
,case when TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No is not null then TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Reject_Type else TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type end Reject_Type
,TSPL_MILK_SRN_HEAD.ROUTE_CODE
from TSPL_MILK_SRN_DETAIL
left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE
left outer join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Uploader_TR_No
left outer join TSPL_MILK_SHIFT_UPLOADER_DETAIL on TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Shift_Uploader_TR_No"
            Dim whr As String = " convert(date, TSPL_MILK_SRN_HEAD.DOC_DATE,106)='" + clsCommon.GetPrintDate(txtShiftDate.Value, "dd/MMM/yyyy") + "' and TSPL_MILK_SRN_HEAD.SHIFT='" + clsCommon.myCstr(cboShift.SelectedValue) + "' and TSPL_MILK_SRN_HEAD.VLC_CODE='" + clsCommon.myCstr(txtVLC.Tag) + "'"
            If Not MultipleFinderFillAuto Then
                whr += " and TSPL_MILK_SRN_HEAD.MCC_CODE='" + txtMCC.Value + "' "
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry + " Where " + whr)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Milk SRN found")
            End If
            Dim srnNo As String = ""
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If dt.Rows.Count = 1 Then
                    srnNo = clsCommon.myCstr(dt.Rows(0)("SRNNo"))
                Else
                    srnNo = clsCommon.ShowSelectForm("SRNCorrf", qry, "SRNNo", whr, srnNo, "SRNNo", True)
                End If
            End If
            If clsCommon.myLen(srnNo) > 0 Then
                dt = clsDBFuncationality.GetDataTable(qry + " where " + whr + " and TSPL_MILK_SRN_HEAD.DOC_CODE='" + srnNo + "'")
                lblSRNNo.Text = clsCommon.myCstr(dt.Rows(0)("SRNNo"))
                txtQty.Value = clsCommon.myCdbl(dt.Rows(0)("Qty"))
                txtQty.Tag = clsCommon.myCdbl(dt.Rows(0)("Qty"))
                lblUOM.Text = clsCommon.myCstr(dt.Rows(0)("UOM_Code"))

                If clsCommon.myCdbl(dt.Rows(0)("Retesting_OR_Correction_Status")) > 0 Then
                    txtFAT.Value = clsCommon.myCdbl(dt.Rows(0)("FAT"))
                    txtFAT.Tag = clsCommon.myCdbl(dt.Rows(0)("FAT"))
                    txtSNF.Value = clsCommon.myCdbl(dt.Rows(0)("SNF"))
                    txtSNF.Tag = clsCommon.myCdbl(dt.Rows(0)("SNF"))
                Else
                    txtFAT.Value = clsCommon.myCdbl(dt.Rows(0)("FAT_PER"))
                    txtFAT.Tag = clsCommon.myCdbl(dt.Rows(0)("FAT_PER"))
                    txtSNF.Value = clsCommon.myCdbl(dt.Rows(0)("SNF_PER"))
                    txtSNF.Tag = clsCommon.myCdbl(dt.Rows(0)("SNF_PER"))
                End If
                cboMilkType.SelectedValue = clsCommon.myCstr(dt.Rows(0)("MilkType"))
                cboMilkType.Tag = clsCommon.myCstr(dt.Rows(0)("MilkType"))
                cboRejectType.SelectedValue = clsCommon.myCstr(dt.Rows(0)("Reject_Type"))
                cboRejectType.Tag = clsCommon.myCstr(dt.Rows(0)("Reject_Type"))
                txtRoute.Value = clsCommon.myCstr(dt.Rows(0)("ROUTE_CODE"))

                TxtFinder1.Value = txtVLC.Value
                TxtFinder1.Tag = txtVLC.Tag
                MyLabel5.Text = lblVLC.Text
                RadGroupBox2.Enabled = False
                RadGroupBox1.Enabled = True
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnnew_Click_1(sender As Object, e As EventArgs) Handles btnnew.Click
        lblSRNNo.Text = Nothing
        txtQty.Value = Nothing
        txtQty.Tag = Nothing
        txtFAT.Value = Nothing
        txtFAT.Tag = Nothing
        txtSNF.Value = Nothing
        txtSNF.Tag = Nothing
        cboMilkType.SelectedValue = Nothing
        cboMilkType.Tag = Nothing
        cboRejectType.SelectedValue = Nothing
        cboRejectType.Tag = Nothing
        txtRoute.Value = Nothing

        TxtFinder1.Value = Nothing
        TxtFinder1.Tag = Nothing
        MyLabel5.Text = Nothing
        RadGroupBox2.Enabled = True
        RadGroupBox1.Enabled = False
        btnSave.Enabled = True
        If clsCommon.CompairString(Form_ID, clsUserMgtCode.MilkProcurementCorrection) = CompairStringResult.Equal Then
            chkRetesting.Checked = False
            chkRetesting.Visible = False
            chkCorrection.Visible = True
            chkCorrection.Checked = True
        ElseIf clsCommon.CompairString(Form_ID, clsUserMgtCode.MilkRetesting) = CompairStringResult.Equal Then
            chkRetesting.Checked = True
            chkRetesting.Visible = True
            chkCorrection.Visible = False
            chkCorrection.Checked = False
        End If
    End Sub

    Private Sub TxtFinder1__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtFinder1._MYValidating
        vlcUploaderFinder(TxtFinder1, MyLabel5, isButtonClicked)
    End Sub

    Private Sub txtMPCMMCC__My_Click(sender As Object, e As EventArgs) Handles txtMPCMMCC._My_Click
        MCCFinder(txtMPCMMCC)
    End Sub



    Private Sub RadButton290_Click(sender As Object, e As EventArgs) Handles RadButton290.Click
        If txtMPCMMCC.arrValueMember Is Nothing OrElse txtMPCMMCC.arrValueMember.Count < 0 Then
            txtMPCMMCC.Focus()
            clsCommon.MyMessageBoxShow(Me, "Please First select MCC", Me.Text)
        End If
        If clsCommon.MyMessageBoxShow("Correct Pro data." + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If


        Dim settBennyImportPickRateFromPrice As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.BennyImportPickRateFromPrice, clsFixedParameterCode.BennyImportPickRateFromPrice, Nothing)) = 1)
        If Not settBennyImportPickRateFromPrice Then
            clsCommon.MyMessageBoxShow("This utility is not fou you", Me.Text)
            Exit Sub
        End If
        Dim tran As SqlTransaction = Nothing
        Try
            If txtMPCMMCC.arrValueMember IsNot Nothing AndAlso txtMPCMMCC.arrValueMember.Count > 0 Then
                For Each strMCC_Code As String In txtMPCMMCC.arrValueMember
                    clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.FrmVLCDataUploaderManual, strMCC_Code, txtMPCMToDate.Value, tran)
                Next
            End If

            Dim qry As String = "select  * from ExplodeDates('" + clsCommon.GetPrintDate(txtMPCMFromDate.Value, "dd/MMM/yyyy") + "','" + clsCommon.GetPrintDate(txtMPCMToDate.Value, "dd/MMM/yyyy") + "')"
            Dim dtDate As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
            If dtDate Is Nothing OrElse dtDate.Rows.Count <= 0 Then
                Throw New Exception("Not Date found between from and To Date")
            End If
            For Each drDate As DataRow In dtDate.Rows
                Dim TransDate As Date = clsCommon.myCDate(drDate(0))
                For Each strMCCcode As String In txtMPCMMCC.arrValueMember
                    Dim strShiftCon As String = ""
                    If Not clsCommon.CompairString(clsCommon.myCstr(cboMPCMShift.SelectedValue), "B") = CompairStringResult.Equal Then
                        strShiftCon = " and TSPL_VLC_DATA_UPLOADER.SHIFT='" + clsCommon.myCstr(cboMPCMShift.SelectedValue) + "'"
                    End If
                    qry = "select Doc_No,PK_ID,qty,fat,snf,shift,VLC_CODE from tspl_vlc_data_uploader where mcc_Code='" + strMCCcode + "' and doc_date='" + clsCommon.GetPrintDate(clsCommon.myCDate(TransDate), "dd/MMM/yyyy") + "' " + strShiftCon + " "
                    Dim dtUploader As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
                    If dtUploader IsNot Nothing AndAlso dtUploader.Rows.Count > 0 Then
                        For Each drUploader As DataRow In dtUploader.Rows
                            Dim FAT As Decimal = Math.Truncate(clsCommon.myCdbl(drUploader("FAT")) * 10) / 10
                            Dim SNF As Decimal = Math.Truncate(clsCommon.myCdbl(drUploader("SNF")) * 10) / 10
                            Dim dblRate As Decimal = clsEkoPro.getRateFromUploaderShiftWise(FAT, SNF, strMCCcode, clsfrmVLCMaster.getVLCCodeForVLCUploaderCode(clsCommon.myCstr(drUploader("VLC_CODE")), strMCCcode, Nothing), clsCommon.myCstr(drUploader("SHIFT")), TransDate, Nothing, "M", settBennyImportPickRateFromPrice)
                            Dim dblAmt As Decimal = Math.Round(dblRate * clsCommon.myCdbl(drUploader("qty")), 2, MidpointRounding.ToEven)
                            Dim coll As Hashtable = New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "FAT", FAT)
                            clsCommon.AddColumnsForChange(coll, "SNF", SNF)
                            clsCommon.AddColumnsForChange(coll, "Rate", dblRate)
                            clsCommon.AddColumnsForChange(coll, "Amount", dblAmt)
                            clsCommonFunctionality.UpdateDataTable(coll, "tspl_vlc_data_uploader", OMInsertOrUpdate.Update, " Doc_No ='" + clsCommon.myCstr(drUploader("Doc_No")) + "'  and  PK_ID='" + clsCommon.myCstr(drUploader("PK_ID")) + "'", tran)
                        Next
                    End If

                    strShiftCon = ""
                    If Not clsCommon.CompairString(clsCommon.myCstr(cboMPCMShift.SelectedValue), "B") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(clsCommon.myCstr(cboMPCMShift.SelectedValue), "M") = CompairStringResult.Equal Then
                            strShiftCon = " and TSPL_VLC_DATA_UPLOADER_MASTER.Shift='MORNING'"
                        Else
                            strShiftCon = " and TSPL_VLC_DATA_UPLOADER_MASTER.Shift='EVENING'"
                        End If
                    End If
                    qry = "select TSPL_VLC_DATA_UPLOADER_DETAIL.Document_Code,TSPL_VLC_DATA_UPLOADER_DETAIL.PK_Id,TSPL_VLC_DATA_UPLOADER_DETAIL.Qty,TSPL_VLC_DATA_UPLOADER_DETAIL.FatPer,TSPL_VLC_DATA_UPLOADER_DETAIL.SNFPer,TSPL_VLC_DATA_UPLOADER_MASTER.shift,TSPL_VLC_DATA_UPLOADER_MASTER.VLC_Code from TSPL_VLC_DATA_UPLOADER_DETAIL left outer join TSPL_VLC_DATA_UPLOADER_MASTER on TSPL_VLC_DATA_UPLOADER_MASTER.Document_Code=TSPL_VLC_DATA_UPLOADER_DETAIL.Document_Code left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_VLC_DATA_UPLOADER_MASTER.VLC_Code where TSPL_VLC_MASTER_HEAD.MCC='" + strMCCcode + "' and convert(date, Document_Date,103)='" + clsCommon.GetPrintDate(TransDate, "dd/MMM/yyyy") + "' " + strShiftCon + " "
                    dtUploader = clsDBFuncationality.GetDataTable(qry, tran)
                    If dtUploader IsNot Nothing AndAlso dtUploader.Rows.Count > 0 Then
                        For Each drUploader As DataRow In dtUploader.Rows
                            Dim FAT As Decimal = Math.Truncate(clsCommon.myCdbl(drUploader("FatPer")) * 10) / 10
                            Dim SNF As Decimal = Math.Truncate(clsCommon.myCdbl(drUploader("SNFPer")) * 10) / 10
                            Dim dblRate As Decimal = clsEkoPro.getRateFromUploaderShiftWise(FAT, SNF, strMCCcode, clsCommon.myCstr(drUploader("VLC_CODE")), IIf(clsCommon.myCstr(drUploader("SHIFT")).ToString.Contains("M"), "M", "E"), TransDate, Nothing, "M", settBennyImportPickRateFromPrice)
                            Dim dblAmt As Decimal = Math.Round(dblRate * clsCommon.myCdbl(drUploader("Qty")), 2, MidpointRounding.ToEven)
                            Dim coll As Hashtable = New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "FatPer", FAT)
                            clsCommon.AddColumnsForChange(coll, "SNFPer", SNF)
                            clsCommon.AddColumnsForChange(coll, "Rate", dblRate)
                            clsCommon.AddColumnsForChange(coll, "Amount", dblAmt)
                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VLC_DATA_UPLOADER_DETAIL", OMInsertOrUpdate.Update, " Document_Code ='" + clsCommon.myCstr(drUploader("Document_Code")) + "'  and  PK_ID='" + clsCommon.myCstr(drUploader("PK_Id")) + "'", tran)
                        Next
                    End If

                Next
            Next

            clsCommon.MyMessageBoxShow(Me, "Successfully Updated", Me.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtVLCCMMCC__My_Click(sender As Object, e As EventArgs) Handles txtVLCCMMCC._My_Click
        MCCFinder(txtVLCCMMCC)
    End Sub

    Sub MCCFinder(ByVal txt As common.UserControls.txtMultiSelectFinder)
        Dim qry As String = "select MCC_Code,MCC_NAME,Mcc_Code_VLC_Uploader as Uploader from TSPL_MCC_MASTER"
        txt.arrValueMember = clsCommon.ShowMultipleSelectForm("MCC@forrm", qry, "MCC_Code", "MCC_NAME", txt.arrValueMember, txt.arrDispalyMember)

    End Sub

    Private Sub RadButton2_Click(sender As Object, e As EventArgs) Handles RadButton2.Click
        Try
            If chkAdjustOwnBMCFATSNF.Checked Then
                If True Then
                    If txtVLCCMMCC.arrValueMember Is Nothing OrElse txtVLCCMMCC.arrValueMember.Count < 0 Then
                        txtVLCCMMCC.Focus()
                        Throw New Exception("Please First select MCC")
                    End If
                    If clsCommon.MyMessageBoxShow("Auto Adjust Own DCS FAT/SNF." + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                        Try

                            For Each strMCC_Code As String In txtVLCCMMCC.arrValueMember
                                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.frmMilkReceipt, strMCC_Code, txtVLCCMToDate.Value, Nothing)
                                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.frmMilkSample, strMCC_Code, txtVLCCMToDate.Value, Nothing)
                            Next
                            Dim qry As String = "select distinct TSPL_MILK_COLLECTION_DCS.Document_No from 
TSPL_MILK_COLLECTION_DCS_MCC_DETAIL
left outer join TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No
left outer join TSPL_MILK_COLLECTION_MCC_DETAIL on TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id=TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail
where CONVERT(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103)>='" + clsCommon.GetPrintDate(txtVLCCMFromDate.Value, "dd/MMM/yyyy") + "' and CONVERT(date, TSPL_MILK_COLLECTION_DCS.Document_Date,103)<='" + clsCommon.GetPrintDate(txtVLCCMToDate.Value, "dd/MMM/yyyy") + "' and  TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code in (" + clsCommon.GetMulcallString(txtVLCCMMCC.arrValueMember) + ")"
                            Dim dtDOC As DataTable = clsDBFuncationality.GetDataTable(qry)
                            If dtDOC Is Nothing OrElse dtDOC.Rows.Count <= 0 Then
                                Throw New Exception("Not Date found between from and To Date")
                            End If
                            For Each drDOC As DataRow In dtDOC.Rows
                                CorrectOwnDCSDocuemnt(clsCommon.myCstr(drDOC("Document_No")))
                            Next
                            clsCommon.MyMessageBoxShow(Me, "Successfully Updated", Me.Text)
                        Catch ex As Exception
                            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                        End Try
                    End If
                End If
            ElseIf chkAvgFATSNF.Checked Then
                If True Then
                    If txtVLCCMMCC.arrValueMember Is Nothing OrElse txtVLCCMMCC.arrValueMember.Count < 0 Then
                        txtVLCCMMCC.Focus()
                        Throw New Exception("Please First select MCC")
                    End If
                    If clsCommon.MyMessageBoxShow("Set Average FAT/SNF is DCS having more than one Sample in a shift." + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                        Try
                            For Each strMCC_Code As String In txtVLCCMMCC.arrValueMember
                                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.frmMilkReceipt, strMCC_Code, txtVLCCMToDate.Value, Nothing)
                                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.frmMilkSample, strMCC_Code, txtVLCCMToDate.Value, Nothing)
                            Next
                            Dim qry As String = "With CTERawData as (
select TSPL_MILK_SRN_HEAD.MCC_CODE,TSPL_MILK_SRN_HEAD.DOC_CODE,convert(date, TSPL_MILK_SRN_HEAD.DOC_DATE,103) as DOC_DATE,TSPL_MILK_SRN_HEAD.SHIFT,TSPL_MILK_SRN_HEAD.VLC_CODE,TSPL_MILK_SRN_DETAIL.FAT_PER,TSPL_MILK_SRN_DETAIL.SNF_PER,TSPL_MILK_SRN_DETAIL.Qty, TSPL_MILK_SRN_DETAIL.FAT_KG,TSPL_MILK_SRN_DETAIL.SNF_KG,TSPL_MILK_SRN_HEAD.Dock_Collection_Milk_Type
from TSPL_MILK_SRN_DETAIL 
left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE  
left outer join TSPL_MILK_SHIFT_UPLOADER_DETAIL on TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Shift_Uploader_TR_No
left outer join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Uploader_TR_No
where TSPL_MILK_SRN_HEAD.MCC_CODE in (" + clsCommon.GetMulcallString(txtVLCCMMCC.arrValueMember) + ") 
and TSPL_MILK_SRN_HEAD.DOC_DATE>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtVLCCMFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' 
and TSPL_MILK_SRN_HEAD.DOC_DATE<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtVLCCMToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  
and len(isnull( TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type,''))<=0 and   len(isnull( TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Reject_Type,''))<=0   "
                            If Not clsCommon.CompairString(clsCommon.myCstr(cboMPCMShift.SelectedValue), "B") = CompairStringResult.Equal Then
                                qry += " and TSPL_MILK_SRN_HEAD.SHIFT='" + clsCommon.myCstr(cboVLCCMShift.SelectedValue) + "'"
                            End If
                            qry += " )
select CTERawData.DOC_CODE,CTERawData.Dock_Collection_Milk_Type,TabDCS.Tot_FAT_PER,TabDCS.Tot_SNF_PER from CTERawData 
inner join (select DOC_DATE,SHIFT,VLC_CODE,SUM(Qty) as Tot_Qty ,case when SUM(Qty)>0 then  (SUM(FAT_KG)*100/SUM(Qty)) else 0 end as Tot_FAT_PER,SUM(FAT_KG) as Tot_FAT_KG ,case when SUM(Qty)>0 then    (SUM(SNF_KG)*100/SUM(Qty))   else 0 end as Tot_SNF_PER,SUM(SNF_KG) as Tot_SNF_KG
from CTERawData 
group by DOC_DATE,SHIFT,VLC_CODE having sum(1)>1) as TabDCS on TabDCS.DOC_DATE=CTERawData.DOC_DATE and  TabDCS.SHIFT=CTERawData.SHIFT and  TabDCS.VLC_CODE=CTERawData.VLC_CODE order by CTERawData.VLC_CODE"
                            Dim dtUploader As DataTable = clsDBFuncationality.GetDataTable(qry)
                            If dtUploader IsNot Nothing AndAlso dtUploader.Rows.Count > 0 Then
                                For Each drUploader As DataRow In dtUploader.Rows
                                    Dim dclFATPer As Decimal = clsCommon.myCDecimal(drUploader("Tot_FAT_PER"))
                                    Dim dclSNFPer As Decimal = clsCommon.myCDecimal(drUploader("Tot_SNF_PER"))
                                    dclFATPer = clsCommon.myRoundOFF(dclFATPer, 1, 5)
                                    dclSNFPer = clsCommon.myRoundOFF(dclSNFPer, 1, 5)

                                    clsMilkSRNMCC.Correction(clsCommon.myCstr(drUploader("DOC_CODE")), clsCommon.myCstr(drUploader("Dock_Collection_Milk_Type")), dclFATPer, dclSNFPer, Remark)
                                Next
                            End If
                            clsCommon.MyMessageBoxShow(Me, "Successfully Updated", Me.Text)
                        Catch ex As Exception
                            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                        End Try
                    End If
                End If
            Else
                If True Then
                    If txtVLCCMMCC.arrValueMember Is Nothing OrElse txtVLCCMMCC.arrValueMember.Count < 0 Then
                        txtVLCCMMCC.Focus()
                        Throw New Exception("Please First select MCC")
                    End If
                    If clsCommon.MyMessageBoxShow("Correct VLC data." + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                        Try
                            For Each strMCC_Code As String In txtVLCCMMCC.arrValueMember
                                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.frmMilkReceipt, strMCC_Code, txtVLCCMToDate.Value, Nothing)
                                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.frmMilkSample, strMCC_Code, txtVLCCMToDate.Value, Nothing)
                            Next
                            Dim qry As String = "select  * from ExplodeDates('" + clsCommon.GetPrintDate(txtVLCCMFromDate.Value, "dd/MMM/yyyy") + "','" + clsCommon.GetPrintDate(txtVLCCMToDate.Value, "dd/MMM/yyyy") + "')"
                            Dim dtDate As DataTable = clsDBFuncationality.GetDataTable(qry)
                            If dtDate Is Nothing OrElse dtDate.Rows.Count <= 0 Then
                                Throw New Exception("Not Date found between from and To Date")
                            End If
                            For Each drDate As DataRow In dtDate.Rows
                                Dim TransDate As Date = clsCommon.myCDate(drDate(0))
                                For Each strMCCcode As String In txtVLCCMMCC.arrValueMember
                                    qry = "select TSPL_MILK_SRN_HEAD.DOC_CODE,TSPL_MILK_SRN_DETAIL.FAT_PER,TSPL_MILK_SRN_DETAIL.SNF_PER,TSPL_MILK_SRN_HEAD.Dock_Collection_Milk_Type 
from TSPL_MILK_SRN_DETAIL 
left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE  
where TSPL_MILK_SRN_HEAD.MCC_CODE='" + strMCCcode + "' and TSPL_MILK_SRN_HEAD.DOC_DATE>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(TransDate), "dd/MMM/yyyy hh:mm:ss tt") + "' and TSPL_MILK_SRN_HEAD.DOC_DATE<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(TransDate), "dd/MMM/yyyy hh:mm:ss tt") + "'"
                                    If Not clsCommon.CompairString(clsCommon.myCstr(cboMPCMShift.SelectedValue), "B") = CompairStringResult.Equal Then
                                        qry += " and TSPL_MILK_SRN_HEAD.SHIFT='" + clsCommon.myCstr(cboVLCCMShift.SelectedValue) + "'"
                                    End If
                                    Dim dtUploader As DataTable = clsDBFuncationality.GetDataTable(qry)
                                    If dtUploader IsNot Nothing AndAlso dtUploader.Rows.Count > 0 Then
                                        For Each drUploader As DataRow In dtUploader.Rows
                                            clsMilkSRNMCC.Correction(clsCommon.myCstr(drUploader("DOC_CODE")), clsCommon.myCstr(drUploader("Dock_Collection_Milk_Type")), clsCommon.myCdbl(drUploader("FAT_PER")), clsCommon.myCdbl(drUploader("SNF_PER")), Remark)
                                        Next
                                    End If
                                Next
                            Next
                            clsCommon.MyMessageBoxShow(Me, "Successfully Updated", Me.Text)
                        Catch ex As Exception
                            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                        End Try
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub CorrectOwnDCSDocuemnt(strDocNo As String)
        ''If Doc any change also do in CorrectOwnDCSDocuemntTrans
        Dim settMaxFATPerLimit As Decimal = clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.MaxFATPerLimit, clsFixedParameterCode.MaxFATPerLimit, Nothing))
        Dim settMaxSNFPerLimit As Decimal = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MaxSNFPerLimit, clsFixedParameterCode.MaxSNFPerLimit, Nothing))
        Dim isPickCLRInsteadOfSNF As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProcuremntPickCLRInsteadOfSNF, clsFixedParameterCode.MilkProcuremntPickCLRInsteadOfSNF, Nothing)) > 0)

        Dim settSNFDecimalPlace As Integer = clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.SNFDecimalPlaces, clsFixedParameterCode.SNFDecimalPlaces, Nothing))
        Dim qry As String = "select xx.* from (
select max(case when isOwnBMC=1 then x.PK_Id else '' end) as PK_Id, max(isOwnBMC) as isOwnBMC, 
sum(MCCQty) as MCCQty,sum(Qty) as TotQty,sum(Qty)-sum(MCCQty) as DiffQty,
sum(MCCFATKG) as MCCFATKG,sum(FATKG) as TotFATKG,sum(FATKG)-sum(MCCFATKG) as DiffFATKG,
sum(MCCSNFKG) as MCCSNFKG,sum(SNFKG) as TotSNFKG,sum(SNFKG)-sum(MCCSNFKG) as DiffSNFKG,
max(case when isOwnBMC=1 then x.VLC_Code else '' end ) as VLC_Code,max(Document_No)Document_No
from (
select TSPL_MILK_COLLECTION_DCS_DETAIL.PK_Id, case when isnull(TSPL_VLC_MASTER_HEAD.isOwnBMC,0)=1 and TSPL_VLC_MASTER_HEAD.MCC=Tab.MCC_Code then 1 else 0 end as isOwnBMC,0.00 as MCCQty,0.00 as MCCFATKG,0.00 as MCCSNFKG,TSPL_MILK_COLLECTION_DCS_DETAIL.Qty,TSPL_MILK_COLLECTION_DCS_DETAIL.FATKG,TSPL_MILK_COLLECTION_DCS_DETAIL.SNFKG,TSPL_MILK_COLLECTION_DCS_DETAIL.VLC_Code,TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
from TSPL_MILK_COLLECTION_DCS_DETAIL
left outer join TSPL_MILK_COLLECTION_DCS on  TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_COLLECTION_DCS_DETAIL.VLC_Code
left outer join (select Document_No,max(MCC_Code) as MCC_Code from (select TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No,TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code from TSPL_MILK_COLLECTION_DCS_MCC_DETAIL 
inner join TSPL_MILK_COLLECTION_MCC_DETAIL on TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id=TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail )xx group by Document_No )Tab on Tab.Document_No= TSPL_MILK_COLLECTION_DCS.Document_No
where TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No='" + strDocNo + "' 
union all
select 0 as PK_Id, 0 as isOwnBMC,TSPL_MILK_COLLECTION_MCC_DETAIL.Qty as MCCQty,TSPL_MILK_COLLECTION_MCC_DETAIL.FATKG as MCCFATKG,TSPL_MILK_COLLECTION_MCC_DETAIL.SNFKG as MCCSNFKG,0.00 as Qty,0.00 as FATKG,0.00 as SNFKG,'' as VLC_Code,TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No from
TSPL_MILK_COLLECTION_DCS_MCC_DETAIL  
left outer join TSPL_MILK_COLLECTION_MCC_DETAIL on TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id=TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail
where TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No='" + strDocNo + "' 
)x
)xx"
        Dim dtDCS As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dtDCS IsNot Nothing AndAlso dtDCS.Rows.Count > 0 Then
            For Each drDCS As DataRow In dtDCS.Rows
                If clsCommon.myCdbl(drDCS("isOwnBMC")) = 0 Then
                    If clsCommon.MyMessageBoxShow(Me, "OwnBmc Data is not Punched for [" + clsCommon.myCstr(dtDCS.Rows(0)("Document_No")) + "]" + Environment.NewLine + "Do You Want To Continue? ", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
                        Exit Sub
                    End If
                Else
                    If Math.Abs(clsCommon.myCdbl(drDCS("DiffFATKG"))) > 0 OrElse Math.Abs(clsCommon.myCdbl(drDCS("DiffSNFKG"))) > 0 Then
                        qry = "select xx.*,TSPL_MILK_SRN_HEAD.DOC_CODE,TSPL_MILK_SRN_HEAD.Dock_Collection_Milk_Type from
(select PK_Id,Qty,FATKG,SNFKG,Shift,TSPL_MILK_REJECT_TYPE.Code as Milk_Type  
from TSPL_MILK_COLLECTION_DCS_DETAIL 
left outer join TSPL_MILK_REJECT_TYPE on TSPL_MILK_REJECT_TYPE.Code=TSPL_MILK_COLLECTION_DCS_DETAIL.Milk_Type 
WHERE VLC_Code =  ( SELECT VLC_Code FROM TSPL_MILK_COLLECTION_DCS_DETAIL where PK_Id=" + clsCommon.myCstr(drDCS("PK_Id")) + ")  
AND Document_No = ( SELECT Document_No FROM TSPL_MILK_COLLECTION_DCS_DETAIL WHERE PK_Id =" + clsCommon.myCstr(drDCS("PK_Id")) + ") )xx
left outer join TSPL_MILK_SHIFT_UPLOADER_DETAIL on TSPL_MILK_SHIFT_UPLOADER_DETAIL.Against_Milk_Collection_DCS_Detail=xx.PK_Id
left outer join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Against_Milk_Collection_DCS_Detail=xx.PK_Id
OUTER APPLY ( SELECT TOP 1 SRN.DOC_CODE,SRN.Dock_Collection_Milk_Type FROM TSPL_MILK_SRN_HEAD SRN WHERE SRN.Against_Shift_Uploader_TR_No = TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No  OR SRN.Against_Uploader_TR_No = TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No ) TSPL_MILK_SRN_HEAD
order by  xx.Shift desc,xx.Qty "
                        Dim dtDetail As DataTable = clsDBFuncationality.GetDataTable(qry)
                        If dtDetail IsNot Nothing AndAlso dtDetail.Rows.Count > 0 Then
                            For indx As Integer = 0 To dtDetail.Rows.Count - 1
                                Dim Qty As Decimal = clsCommon.myCDecimal(dtDetail.Rows(indx)("Qty"))
                                If (clsCommon.myCdbl(drDCS("DiffQty"))) > 0 Then
                                    Qty = clsCommon.myCDecimal(dtDetail.Rows(indx)("Qty")) - clsCommon.myCDecimal(drDCS("DiffQty"))
                                    If Qty < 0 Then
                                        Qty = clsCommon.myCDecimal(dtDetail.Rows(indx)("Qty"))
                                    End If
                                End If
                                Dim FATKG As Decimal = (clsCommon.myCDecimal(dtDetail.Rows(indx)("FATKG")) - clsCommon.myCDecimal(drDCS("DiffFATKG")))
                                Dim SNFKG As Decimal = (clsCommon.myCDecimal(dtDetail.Rows(indx)("SNFKG")) - clsCommon.myCDecimal(drDCS("DiffSNFKG")))
                                If FATKG < 0 OrElse SNFKG < 0 Then
                                    Continue For
                                End If
                                Dim FAT As Decimal = Math.Round(clsCommon.myCDivide((100 * FATKG), Qty), 1, MidpointRounding.AwayFromZero)
                                Dim SNF As Decimal = Math.Round(clsCommon.myCDivide((100 * SNFKG), Qty), settSNFDecimalPlace, MidpointRounding.AwayFromZero)
                                'If settMaxFATPerLimit > 0 Then
                                '    If FAT > settMaxFATPerLimit Then
                                '        FAT = settMaxFATPerLimit
                                '        FATKG = Math.Round((Qty * FAT / 100), 3, MidpointRounding.AwayFromZero)
                                '    End If
                                'End If
                                'If settMaxSNFPerLimit > 0 AndAlso Not isPickCLRInsteadOfSNF Then
                                '    If SNF > settMaxSNFPerLimit Then
                                '        SNF = settMaxSNFPerLimit
                                '        SNFKG = Math.Round((Qty * SNF / 100), 3, MidpointRounding.AwayFromZero)
                                '    End If
                                'End If
                                Dim strRejectType As String = clsCommon.myCstr(dtDetail.Rows(indx)("Milk_Type"))
                                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                                Try
                                    qry = "update TSPL_MILK_COLLECTION_DCS_DETAIL set Own_Qty= case when Own_Qty is null then Qty else Own_Qty end,Own_FAT= case when Own_FAT is null then FAT else Own_FAT end,Own_SNF= case when Own_SNF is null then SNF else Own_SNF end,Own_FATKG= case when Own_FATKG is null then FATKG else Own_FATKG end,Own_SNFKG= case when Own_SNFKG is null then SNFKG else Own_SNFKG end where PK_Id=" + clsCommon.myCstr(dtDetail.Rows(indx)("PK_Id")) + ""
                                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                                    If clsCommon.myLen(dtDetail.Rows(indx)("DOC_CODE")) > 0 Then
                                        clsMilkSRNMCC.Correction(clsCommon.myCstr(dtDetail.Rows(indx)("DOC_CODE")), True, True, False, Qty, clsCommon.myCstr(dtDetail.Rows(indx)("Dock_Collection_Milk_Type")), FAT, SNF, "", False, trans, True, Form_ID, strRejectType, Remark)
                                    Else
                                        Dim coll As New Hashtable()
                                        clsCommon.AddColumnsForChange(coll, "Qty", Qty)
                                        clsCommon.AddColumnsForChange(coll, "FAT", FAT)
                                        clsCommon.AddColumnsForChange(coll, "SNF", SNF)
                                        clsCommon.AddColumnsForChange(coll, "FATKG", FATKG)
                                        clsCommon.AddColumnsForChange(coll, "SNFKG", SNFKG)
                                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_COLLECTION_DCS_DETAIL", OMInsertOrUpdate.Update, "PK_Id='" + clsCommon.myCstr(dtDetail.Rows(indx)("PK_Id")) + "'", trans)
                                    End If
                                    trans.Commit()
                                    Exit For
                                Catch ex As Exception
                                    trans.Rollback()
                                    Throw New Exception(ex.Message)
                                End Try
                            Next
                        End If
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub CorrectOwnDCSDocuemntTrans(strDocNo As String, trans As SqlTransaction)
        ''If Doc any change also do in CorrectOwnDCSDocuemnt

        Dim settSNFDecimalPlace As Integer = clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.SNFDecimalPlaces, clsFixedParameterCode.SNFDecimalPlaces, trans))
        Dim settMaxFATPerLimit As Decimal = clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.MaxFATPerLimit, clsFixedParameterCode.MaxFATPerLimit, trans))
        Dim settMaxSNFPerLimit As Decimal = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MaxSNFPerLimit, clsFixedParameterCode.MaxSNFPerLimit, trans))
        Dim isPickCLRInsteadOfSNF As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProcuremntPickCLRInsteadOfSNF, clsFixedParameterCode.MilkProcuremntPickCLRInsteadOfSNF, trans)) > 0)

        Dim qry As String = "select xx.* from (
select max(case when isOwnBMC=1 then x.PK_Id else '' end) as PK_Id, max(isOwnBMC) as isOwnBMC, 
sum(MCCQty) as MCCQty,sum(Qty) as TotQty,sum(Qty)-sum(MCCQty) as DiffQty,
sum(MCCFATKG) as MCCFATKG,sum(FATKG) as TotFATKG,sum(FATKG)-sum(MCCFATKG) as DiffFATKG,
sum(MCCSNFKG) as MCCSNFKG,sum(SNFKG) as TotSNFKG,sum(SNFKG)-sum(MCCSNFKG) as DiffSNFKG,
max(case when isOwnBMC=1 then x.VLC_Code else '' end ) as VLC_Code,max(Document_No)Document_No
from (
select TSPL_MILK_COLLECTION_DCS_DETAIL.PK_Id, case when isnull(TSPL_VLC_MASTER_HEAD.isOwnBMC,0)=1 and TSPL_VLC_MASTER_HEAD.MCC=Tab.MCC_Code then 1 else 0 end as isOwnBMC,0.00 as MCCQty,0.00 as MCCFATKG,0.00 as MCCSNFKG,TSPL_MILK_COLLECTION_DCS_DETAIL.Qty,TSPL_MILK_COLLECTION_DCS_DETAIL.FATKG,TSPL_MILK_COLLECTION_DCS_DETAIL.SNFKG,TSPL_MILK_COLLECTION_DCS_DETAIL.VLC_Code,TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
from TSPL_MILK_COLLECTION_DCS_DETAIL
left outer join TSPL_MILK_COLLECTION_DCS on  TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_COLLECTION_DCS_DETAIL.VLC_Code
left outer join (select Document_No,max(MCC_Code) as MCC_Code from (select TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No,TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code from TSPL_MILK_COLLECTION_DCS_MCC_DETAIL 
inner join TSPL_MILK_COLLECTION_MCC_DETAIL on TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id=TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail )xx group by Document_No )Tab on Tab.Document_No= TSPL_MILK_COLLECTION_DCS.Document_No
where TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No='" + strDocNo + "' 
union all
select 0 as PK_Id, 0 as isOwnBMC,TSPL_MILK_COLLECTION_MCC_DETAIL.Qty as MCCQty,TSPL_MILK_COLLECTION_MCC_DETAIL.FATKG as MCCFATKG,TSPL_MILK_COLLECTION_MCC_DETAIL.SNFKG as MCCSNFKG,0.00 as Qty,0.00 as FATKG,0.00 as SNFKG,'' as VLC_Code,TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No from
TSPL_MILK_COLLECTION_DCS_MCC_DETAIL  
left outer join TSPL_MILK_COLLECTION_MCC_DETAIL on TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id=TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail
where TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No='" + strDocNo + "' 
)x
)xx"
        Dim dtDCS As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dtDCS IsNot Nothing AndAlso dtDCS.Rows.Count > 0 Then
            For Each drDCS As DataRow In dtDCS.Rows
                If clsCommon.myCdbl(drDCS("isOwnBMC")) = 0 Then
                    Exit Sub
                Else
                    Dim isThereOnlyOneRowOfOwnDCS As Boolean = False
                    Dim ROIncreaseAfter As Integer = 6
                    If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "JPR") = CompairStringResult.Equal Then
                        ROIncreaseAfter = 5
                        isThereOnlyOneRowOfOwnDCS = (clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.isThereOnlyOneRowOfOwnDCS, clsFixedParameterCode.isThereOnlyOneRowOfOwnDCS, trans)) = 1)
                        If isThereOnlyOneRowOfOwnDCS Then
                            qry = "select count(1) as cnt from TSPL_MILK_COLLECTION_DCS_DETAIL where Document_No='" + strDocNo + "'"
                            isThereOnlyOneRowOfOwnDCS = (clsCommon.myCDecimal(clsDBFuncationality.getSingleValue(qry, trans)) = 1)
                        End If
                    End If

                    If Math.Abs(clsCommon.myCdbl(drDCS("DiffFATKG"))) > 0 OrElse Math.Abs(clsCommon.myCdbl(drDCS("DiffSNFKG"))) > 0 Then
                        qry = "select xx.*,TSPL_MILK_SRN_HEAD.DOC_CODE,TSPL_MILK_SRN_HEAD.Dock_Collection_Milk_Type from
(select PK_Id,Qty,FATKG,SNFKG,Shift,TSPL_MILK_REJECT_TYPE.Code as Milk_Type,TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No as DCSDocNo  
from TSPL_MILK_COLLECTION_DCS_DETAIL 
left outer join TSPL_MILK_REJECT_TYPE on TSPL_MILK_REJECT_TYPE.Code=TSPL_MILK_COLLECTION_DCS_DETAIL.Milk_Type 
WHERE VLC_Code =  ( SELECT VLC_Code FROM TSPL_MILK_COLLECTION_DCS_DETAIL where PK_Id=" + clsCommon.myCstr(drDCS("PK_Id")) + ")  
AND Document_No = ( SELECT Document_No FROM TSPL_MILK_COLLECTION_DCS_DETAIL WHERE PK_Id =" + clsCommon.myCstr(drDCS("PK_Id")) + ") )xx
left outer join TSPL_MILK_SHIFT_UPLOADER_DETAIL on TSPL_MILK_SHIFT_UPLOADER_DETAIL.Against_Milk_Collection_DCS_Detail=xx.PK_Id
left outer join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Against_Milk_Collection_DCS_Detail=xx.PK_Id
OUTER APPLY ( SELECT TOP 1 SRN.DOC_CODE,SRN.Dock_Collection_Milk_Type FROM TSPL_MILK_SRN_HEAD SRN WHERE SRN.Against_Shift_Uploader_TR_No = TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No  OR SRN.Against_Uploader_TR_No = TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No ) TSPL_MILK_SRN_HEAD
order by  xx.Shift desc,xx.Qty "
                        Dim dtDetail As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        If dtDetail IsNot Nothing AndAlso dtDetail.Rows.Count > 0 Then
                            For indx As Integer = 0 To dtDetail.Rows.Count - 1
                                Dim Qty As Decimal = clsCommon.myCDecimal(dtDetail.Rows(indx)("Qty"))
                                If (clsCommon.myCdbl(drDCS("DiffQty"))) <> 0 Then
                                    Qty = clsCommon.myCDecimal(dtDetail.Rows(indx)("Qty")) - clsCommon.myCDecimal(drDCS("DiffQty"))
                                    If Qty < 0 Then
                                        Qty = clsCommon.myCDecimal(dtDetail.Rows(indx)("Qty"))
                                    End If
                                End If
                                Dim FATKG As Decimal = (clsCommon.myCDecimal(dtDetail.Rows(indx)("FATKG")) - clsCommon.myCDecimal(drDCS("DiffFATKG")))
                                Dim SNFKG As Decimal = (clsCommon.myCDecimal(dtDetail.Rows(indx)("SNFKG")) - clsCommon.myCDecimal(drDCS("DiffSNFKG")))
                                If FATKG < 0 OrElse SNFKG < 0 Then
                                    Continue For
                                End If
                                'Dim FAT As Decimal = Math.Round(clsCommon.myCDivide((100 * FATKG), Qty), 1, MidpointRounding.AwayFromZero)
                                'Dim SNF As Decimal = Math.Round(clsCommon.myCDivide((100 * SNFKG), Qty), settSNFDecimalPlace, MidpointRounding.AwayFromZero)
                                Dim FAT As Decimal = 0
                                Dim SNF As Decimal = 0
                                If isThereOnlyOneRowOfOwnDCS Then
                                    FAT = Math.Round(clsCommon.myCDivide((100 * FATKG), Qty), 2, MidpointRounding.ToEven)
                                    SNF = Math.Round(clsCommon.myCDivide((100 * SNFKG), Qty), (settSNFDecimalPlace + 1), MidpointRounding.ToEven)

                                    FAT = clsCommon.myRoundOFF(FAT, 1, ROIncreaseAfter)
                                    SNF = clsCommon.myRoundOFF(SNF, settSNFDecimalPlace, ROIncreaseAfter)
                                Else
                                    FAT = clsCommon.myRoundOFF(clsCommon.myCDivide((100 * FATKG), Qty), 1, ROIncreaseAfter)
                                    SNF = clsCommon.myRoundOFF(clsCommon.myCDivide((100 * SNFKG), Qty), settSNFDecimalPlace, ROIncreaseAfter)
                                End If

                                If settMaxFATPerLimit > 0 Then
                                    If FAT > settMaxFATPerLimit Then
                                        FAT = settMaxFATPerLimit
                                    End If
                                End If
                                FATKG = Math.Round((Qty * FAT / 100), 3, MidpointRounding.AwayFromZero)
                                If settMaxSNFPerLimit > 0 AndAlso Not isPickCLRInsteadOfSNF Then
                                    If SNF > settMaxSNFPerLimit Then
                                        SNF = settMaxSNFPerLimit
                                    End If
                                End If
                                SNFKG = Math.Round((Qty * SNF / 100), 3, MidpointRounding.AwayFromZero)
                                Dim strRejectType As String = clsCommon.myCstr(dtDetail.Rows(indx)("Milk_Type"))
                                Try
                                    qry = "update TSPL_MILK_COLLECTION_DCS_DETAIL set Own_Qty= case when Own_Qty is null then Qty else Own_Qty end,Own_FAT= case when Own_FAT is null then FAT else Own_FAT end,Own_SNF= case when Own_SNF is null then SNF else Own_SNF end,Own_FATKG= case when Own_FATKG is null then FATKG else Own_FATKG end,Own_SNFKG= case when Own_SNFKG is null then SNFKG else Own_SNFKG end where PK_Id=" + clsCommon.myCstr(dtDetail.Rows(indx)("PK_Id")) + ""
                                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                                    If clsCommon.myLen(dtDetail.Rows(indx)("DOC_CODE")) > 0 Then
                                        clsMilkSRNMCC.Correction(clsCommon.myCstr(dtDetail.Rows(indx)("DOC_CODE")), True, True, False, Qty, clsCommon.myCstr(dtDetail.Rows(indx)("Dock_Collection_Milk_Type")), FAT, SNF, "", False, trans, True, Form_ID, strRejectType, Remark)
                                    Else
                                        Dim coll As New Hashtable()
                                        clsCommon.AddColumnsForChange(coll, "Qty", Qty)
                                        clsCommon.AddColumnsForChange(coll, "FAT", FAT)
                                        clsCommon.AddColumnsForChange(coll, "SNF", SNF)
                                        clsCommon.AddColumnsForChange(coll, "FATKG", FATKG)
                                        clsCommon.AddColumnsForChange(coll, "SNFKG", SNFKG)
                                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_COLLECTION_DCS_DETAIL", OMInsertOrUpdate.Update, "PK_Id='" + clsCommon.myCstr(dtDetail.Rows(indx)("PK_Id")) + "'", trans)

                                        clsMilkCollectionDCS.HistoryUpdate(clsCommon.myCstr(dtDetail.Rows(indx)("DCSDocNo")), trans)
                                    End If

                                    Exit For
                                Catch ex As Exception
                                    Throw New Exception(ex.Message)
                                End Try
                            Next
                        End If
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub BulkDelete_Click(sender As Object, e As EventArgs) Handles BulkDelete.Click
        Try
            If clsCommon.MyMessageBoxShow("Delete the collection data", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
            clsMilkShiftUploaderHead.DeleteCollectionData(TxtMultiSelectFinder8.arrValueMember, txtMCCFromDate.Value, txtMCCToDate.Value, clsCommon.myCstr(txtFromShift.SelectedValue), chkDeleteBMCCollection.Checked, chkPreviousShift.Checked, chkDeleteDCSCollection.Checked)
            clsCommon.MyMessageBoxShow(Me, "Successfully Deleted", Me.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try


    End Sub


    Private Sub TxtMultiSelectFinder8__My_Click(sender As Object, e As EventArgs) Handles TxtMultiSelectFinder8._My_Click
        Dim qry As String = "select MCC_Code,MCC_NAME,Mcc_Code_VLC_Uploader as Uploader from TSPL_MCC_MASTER"
        TxtMultiSelectFinder8.arrValueMember = clsCommon.ShowMultipleSelectForm("BulkMCC@Uti1", qry, "MCC_Code", "MCC_NAME", TxtMultiSelectFinder8.arrValueMember, TxtMultiSelectFinder8.arrDispalyMember)
    End Sub

    Private Sub txtRouteNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtBMCRouteNo._MYValidating
        Try
            Dim qry As String = " select ROUTE_NO as Code,ROUTE_NAME as Name from  TSPL_BULK_ROUTE_MASTER "
            Dim whrCls As String = ""
            If Not SettShowAllMCC Then
                whrCls = "exists(select 1 from TSPL_BULK_ROUTE_MASTER_MCC where TSPL_BULK_ROUTE_MASTER_MCC.ROUTE_NO=TSPL_BULK_ROUTE_MASTER.ROUTE_NO )"
            End If

            txtBMCRouteNo.Value = clsCommon.ShowSelectForm("dd22ShUp1", qry, "Code", whrCls, txtBMCRouteNo.Value, "Code", isButtonClicked)
            lblBMCRoute.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ROUTE_NAME from TSPL_BULK_ROUTE_MASTER where ROUTE_NO='" + txtBMCRouteNo.Value + "'"))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub



    Private Sub txtDCSBMC__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtBMCBMC._MYValidating
        Try
            'If clsCommon.myLen(txtBMCRouteNo.Value) <= 0 Then
            '    txtBMCRouteNo.Focus()
            '    Throw New Exception("Please provide Route code ")
            'End If
            'Dim whr As String = "len(isnull(TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader,''))>0 "
            ''If Not SettShowAllMCC Then
            ''    whr += " and TSPL_BULK_ROUTE_MASTER_MCC.ROUTE_NO='" + txtBMCRouteNo.Value + "' "
            ''End If
            'txtBMCBMC.Value = clsMccMaster.getFinderUploader(whr, txtBMCBMC.Value, isButtonClicked)
            'Dim dt As DataTable = clsDBFuncationality.GetDataTable("select TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader,TSPL_MCC_MASTER.MCC_Code,TSPL_MCC_MASTER.MCC_NAME from TSPL_MCC_MASTER where Mcc_Code_VLC_Uploader='" + clsCommon.myCstr(txtBMCBMC.Value) + "'")
            'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            '    txtBMCBMC.Tag = clsCommon.myCstr(dt.Rows(0)("MCC_Code"))
            '    lblBMCBMC.Text = clsCommon.myCstr(dt.Rows(0)("MCC_NAME"))
            'End If

            Dim qry As String = "select * from (
select max(MCC) as MCC,MCC_Code,max(MCC_NAME) as MCC_NAME,Route_Code from (
select  TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader as MCC, TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code,TSPL_MCC_MASTER.MCC_NAME,TSPL_MILK_COLLECTION_MCC.Route_Code,TSPL_MILK_COLLECTION_MCC.Tanker_No,TSPL_MILK_COLLECTION_MCC_DETAIL.Sample_No,TSPL_MILK_COLLECTION_MCC.Trip_No
from TSPL_MILK_COLLECTION_MCC_DETAIL
left outer join TSPL_MILK_COLLECTION_MCC on TSPL_MILK_COLLECTION_MCC.Document_No=TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No
left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code
where  TSPL_MILK_COLLECTION_MCC.Status=1 and convert(date, TSPL_MILK_COLLECTION_MCC.Document_Date,106)='" + clsCommon.GetPrintDate(txtBMCDate.Value, "dd/MMM/yyyy") + "' 
)xx group by MCC_Code,Route_Code 
)xxx"
            Dim dr As DataRow = clsCommon.ShowSelectFormForRow("corrbmmfd", qry)
            If dr IsNot Nothing Then
                txtBMCBMC.Value = clsCommon.myCstr(dr("MCC"))
                txtBMCBMC.Tag = clsCommon.myCstr(dr("MCC_Code"))
                lblBMCBMC.Text = clsCommon.myCstr(dr("MCC_NAME"))
                txtBMCRouteNo.Value = clsCommon.myCstr(dr("Route_Code"))
            Else
                txtBMCBMC.Tag = ""
                lblBMCBMC.Text = ""
                txtBMCRouteNo.Value = ""
                txtBMCBMC.Value = ""
            End If

            'txtBMCBMC.Value = clsCommon.ShowSelectFormFromDT("BMC@corf", dt, "MCC", txtBMCBMC.Value, isButtonClicked, "")
            'Dim dr As DataRow() = clsCommon.MyDTSelect(dt, "MCC='" + txtBMCBMC.Value + "'")
            'If dr IsNot Nothing AndAlso dr.Length > 0 Then
            '    txtBMCBMC.Tag = clsCommon.myCstr(dr(0)("MCC_Code"))
            '    lblBMCBMC.Text = clsCommon.myCstr(dr(0)("MCC_NAME"))
            '    txtBMCRouteNo.Value = clsCommon.myCstr(dr(0)("Route_Code"))
            'Else
            '    txtBMCBMC.Tag = ""
            '    lblBMCBMC.Text = ""
            '    txtBMCRouteNo.Value = ""
            '    txtBMCBMC.Value = ""
            'End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDCSCorrBMC__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtBMCCorrBMC._MYValidating
        Try
            If clsCommon.myLen(txtBMCRouteNo.Value) <= 0 Then
                txtBMCRouteNo.Focus()
                Throw New Exception("Please provide Route code ")
            End If
            Dim whr As String = "len(isnull(TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader,''))>0 "
            If Not SettShowAllMCC Then
                whr += " and TSPL_BULK_ROUTE_MASTER_MCC.ROUTE_NO='" + txtBMCRouteNo.Value + "' "
            End If
            txtBMCCorrBMC.Value = clsMccMaster.getFinderUploader(whr, txtBMCCorrBMC.Value, isButtonClicked)
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader,TSPL_MCC_MASTER.MCC_Code,TSPL_MCC_MASTER.MCC_NAME from TSPL_MCC_MASTER where Mcc_Code_VLC_Uploader='" + clsCommon.myCstr(txtBMCCorrBMC.Value) + "'")
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                txtBMCCorrBMC.Tag = clsCommon.myCstr(dt.Rows(0)("MCC_Code"))
                lblBMCCorrBMC.Text = clsCommon.myCstr(dt.Rows(0)("MCC_NAME"))
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton3_Click(sender As Object, e As EventArgs) Handles RadButton3.Click
        Try
            If clsCommon.myLen(txtBMCRouteNo.Value) <= 0 Then
                txtBMCRouteNo.Focus()
                Throw New Exception("Please enter Route")
            End If
            If clsCommon.myLen(txtBMCBMC.Value) <= 0 Then
                txtBMCBMC.Focus()
                Throw New Exception("Please enter BMC")
            End If

            '            Dim qry As String = "select TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No,TSPL_MILK_COLLECTION_MCC.Document_Date,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader as MCC, TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code,TSPL_MCC_MASTER.MCC_NAME,TSPL_MILK_COLLECTION_MCC_DETAIL.Milk_Type,TSPL_MILK_COLLECTION_MCC_DETAIL.Qty,TSPL_MILK_COLLECTION_MCC_DETAIL.FAT,TSPL_MILK_COLLECTION_MCC_DETAIL.SNF,TSPL_MILK_COLLECTION_MCC_DETAIL.SNo,TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id
            ',TSPL_MILK_COLLECTION_MCC_DETAIL.IsUpdatedFromCorrection,TSPL_MILK_COLLECTION_MCC_DETAIL.Retesting_OR_Correction,
            'TSPL_MILK_COLLECTION_MCC_DETAIL.Retesting_FAT,TSPL_MILK_COLLECTION_MCC_DETAIL.Retesting_SNF,TSPL_MILK_COLLECTION_MCC_DETAIL.Retesting_CLR
            'from TSPL_MILK_COLLECTION_MCC_DETAIL
            'left outer join  TSPL_MILK_COLLECTION_MCC on TSPL_MILK_COLLECTION_MCC.Document_No=TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No
            'left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code"
            Dim qry As String = "select TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No,TSPL_MILK_COLLECTION_MCC.Document_Date,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader as MCC, TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code,TSPL_MCC_MASTER.MCC_NAME,TSPL_MILK_COLLECTION_MCC_DETAIL.Milk_Type,TSPL_MILK_COLLECTION_MCC_DETAIL.Qty,TSPL_MILK_COLLECTION_MCC_DETAIL.FAT,TSPL_MILK_COLLECTION_MCC_DETAIL.SNF,TSPL_MILK_COLLECTION_MCC_DETAIL.SNo,TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id
,TSPL_MILK_COLLECTION_MCC_DETAIL.IsUpdatedFromCorrection,TSPL_MILK_COLLECTION_MCC_DETAIL.Retesting_OR_Correction,
TSPL_MILK_COLLECTION_MCC_DETAIL.Retesting_FAT,TSPL_MILK_COLLECTION_MCC_DETAIL.Retesting_SNF,TSPL_MILK_COLLECTION_MCC_DETAIL.Retesting_CLR,TSPL_MILK_COLLECTION_MCC_DETAIL.Gaze_Qty as [Gaze Qty],TSPL_MILK_COLLECTION_MCC.Trip_No as [TripNo],TSPL_MILK_COLLECTION_MCC.Route_Code as [Route],TSPL_MILK_COLLECTION_MCC.Tanker_No as [Tanker No],TSPL_MILK_COLLECTION_MCC.Vehicle_No as [Vehicle No],TSPL_MILK_COLLECTION_MCC_DETAIL.Sample_No as [Sample No]
from TSPL_MILK_COLLECTION_MCC_DETAIL
left outer join  TSPL_MILK_COLLECTION_MCC on TSPL_MILK_COLLECTION_MCC.Document_No=TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No
left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code"
            Dim whr As String = " TSPL_MILK_COLLECTION_MCC.Status=1 and TSPL_MILK_COLLECTION_MCC.Route_Code='" + txtBMCRouteNo.Value + "' and TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code='" + clsCommon.myCstr(txtBMCBMC.Tag) + "'  and convert(date, TSPL_MILK_COLLECTION_MCC.Document_Date,106)='" + clsCommon.GetPrintDate(txtBMCDate.Value, "dd/MMM/yyyy") + "' "


            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry + " Where " + whr)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No DCS Milk Collection found")
            End If
            Dim strPK_Id As String = ""
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If dt.Rows.Count = 1 Then
                    strPK_Id = clsCommon.myCstr(dt.Rows(0)("PK_Id"))
                Else
                    strPK_Id = clsCommon.ShowSelectForm("DCSCorrf", qry, "PK_Id", whr, strPK_Id, "PK_Id", True)
                End If
            End If
            If clsCommon.myLen(strPK_Id) > 0 Then
                dt = clsDBFuncationality.GetDataTable(qry + " where " + whr + " and TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id='" + strPK_Id + "'")
                lblBMCDocNo.Text = clsCommon.myCstr(dt.Rows(0)("Document_No"))
                lblBMCSno.Text = clsCommon.myCstr(dt.Rows(0)("SNo"))
                lblBMCDetailNo.Text = clsCommon.myCstr(dt.Rows(0)("PK_Id"))
                txtBMCCorrQty.Value = clsCommon.myCdbl(dt.Rows(0)("Qty"))
                txtBMCCorrFAT.Value = clsCommon.myCdbl(dt.Rows(0)("FAT"))
                txtBMCCorrSNF.Value = clsCommon.myCdbl(dt.Rows(0)("SNF"))
                If isPickCLRInsteadOfSNF Then
                    txtRetestingCLR.Value = clsEkoPro.getClrOnCalculation(txtBMCCorrFAT.Value, txtBMCCorrSNF.Value, corrFactor)
                End If
                cboBMCCorrMilkType.SelectedValue = clsCommon.myCstr(dt.Rows(0)("Milk_Type"))
                txtBMCCorrBMC.Value = txtBMCBMC.Value
                txtBMCCorrBMC.Tag = txtBMCBMC.Tag
                lblBMCCorrBMC.Text = lblBMCBMC.Text
                RadGroupBox3.Enabled = False
                RadGroupBox4.Enabled = True
                lblBMCStatus.Text = IIf(Convert.ToInt32(dt.Compute("MAX([IsUpdatedFromCorrection])", "")) >= 1, "Upadated", "")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton5_Click(sender As Object, e As EventArgs) Handles RadButton5.Click
        Try


            If isNewEntry = False AndAlso clsCommon.CompairString(objCommonVar.CurrComp_Code1, "JPR") = CompairStringResult.Equal Then
                Dim Reason As String = ""
                Dim frm As New FrmFreeTxtBox1
                frm.Text = "Remarks for Update"
                frm.ShowDialog()
                If clsCommon.myLen(frm.strRmks) <= 0 Then
                    Exit Sub
                Else
                    Remark = frm.strRmks
                End If
            End If
            If clsCommon.myLen(lblBMCDocNo.Text) <= 0 Then
                Throw New Exception("Document No can't be blank")
            End If
            If clsCommon.myLen(lblBMCDetailNo.Text) <= 0 Then
                Throw New Exception("DCSNo can't be blank")
            End If
            If clsCommon.myLen(txtBMCCorrBMC.Value) <= 0 Then
                Throw New Exception("Please select " + txtBMCCorrBMC.MyLinkLable1.Text)
            End If
            If clsCommon.myLen(cboBMCCorrMilkType.SelectedItem.Text) <= 0 Then
                Throw New Exception("Please select " + cboBMCCorrMilkType.MyLinkLable1.Text)
            End If
            If txtBMCCorrQty.Value < 0 Then
                Throw New Exception("Qty Can't be -ve")
            End If
            If txtBMCCorrFAT.Value < 0 Then
                Throw New Exception("FAT % Can't be -ve")
            End If
            If txtBMCCorrSNF.Value < 0 Then
                Throw New Exception("SNF % Can't be -ve")
            End If
            Dim qry As String = "PK_Id=" + lblBMCDetailNo.Text + ""
            Dim Arr As List(Of clsMilkCollectionMCCDetail) = clsMilkCollectionMCCDetail.GetData(lblBMCDocNo.Text, qry, Nothing)
            If Arr Is Nothing OrElse Arr.Count <= 0 Then
                Throw New Exception("Please select Valid document to correct")
            End If
            qry = "select TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE
from TSPL_MILK_COLLECTION_MCC_DETAIL
left outer join TSPL_MILK_COLLECTION_DCS_MCC_DETAIL on TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail=TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id 
left outer join TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No
left outer join TSPL_MILK_COLLECTION_DCS_DETAIL on TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No=TSPL_MILK_COLLECTION_DCS.Document_No
left outer join TSPL_MILK_SHIFT_UPLOADER_DETAIL on TSPL_MILK_SHIFT_UPLOADER_DETAIL.Against_Milk_Collection_DCS_Detail=TSPL_MILK_COLLECTION_DCS_DETAIL.PK_Id
left outer join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Against_Milk_Collection_DCS_Detail=TSPL_MILK_COLLECTION_DCS_DETAIL.PK_Id
inner join TSPL_MILK_SRN_HEAD on  TSPL_MILK_SRN_HEAD.Against_Shift_Uploader_TR_No=TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No 
left outer join TSPL_MILK_PURCHASE_INVOICE_DETAIL on TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE=TSPL_MILK_SRN_HEAD.DOC_CODE
where TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE is not null and TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id=" + lblBMCDetailNo.Text + "
union all
select TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE
from TSPL_MILK_COLLECTION_MCC_DETAIL
left outer join TSPL_MILK_COLLECTION_DCS_MCC_DETAIL on TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail=TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id 
left outer join TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No
left outer join TSPL_MILK_COLLECTION_DCS_DETAIL on TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No=TSPL_MILK_COLLECTION_DCS.Document_No
left outer join TSPL_MILK_SHIFT_UPLOADER_DETAIL on TSPL_MILK_SHIFT_UPLOADER_DETAIL.Against_Milk_Collection_DCS_Detail=TSPL_MILK_COLLECTION_DCS_DETAIL.PK_Id
left outer join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Against_Milk_Collection_DCS_Detail=TSPL_MILK_COLLECTION_DCS_DETAIL.PK_Id
inner join TSPL_MILK_SRN_HEAD on  TSPL_MILK_SRN_HEAD.Against_Uploader_TR_No= TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No
left outer join TSPL_MILK_PURCHASE_INVOICE_DETAIL on TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE=TSPL_MILK_SRN_HEAD.DOC_CODE
where TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE is not null and TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id=" + lblBMCDetailNo.Text + ""
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Throw New Exception("Milk Purchase Invoice Generated [" + clsCommon.myCstr(dt.Rows(0)("DOC_CODE")) + "]")
            End If
            Arr(0).MCC_Code = txtBMCCorrBMC.Tag
            Arr(0).Milk_Type = (cboBMCCorrMilkType.SelectedValue)
            Arr(0).Qty = txtBMCCorrQty.Value
            If clsCommon.CompairString(Form_ID, clsUserMgtCode.MilkRetesting) = CompairStringResult.Equal Then
                Arr(0).Retesting_FAT = txtBMCCorrFAT.Value
                Arr(0).Retesting_SNF = txtBMCCorrSNF.Value
                Arr(0).Retesting_CLR = txtRetestingCLR.Value
                Arr(0).Retesting_OR_Correction = 1

                Arr(0).FAT = Arr(0).Retesting_FAT
                Arr(0).SNF = Arr(0).Retesting_SNF
            Else
                Arr(0).Correction_Qty = txtBMCCorrQty.Value
                Arr(0).Correction_FAT = txtBMCCorrFAT.Value
                Arr(0).Correction_SNF = txtBMCCorrSNF.Value
                Arr(0).Retesting_OR_Correction = 2

                Arr(0).FAT = Arr(0).Correction_FAT
                Arr(0).SNF = Arr(0).Correction_SNF
            End If

            Arr(0).FATKG = Math.Round(Arr(0).Qty * Arr(0).FAT / 100, 3, MidpointRounding.ToEven)
            Arr(0).SNFKG = Math.Round(Arr(0).Qty * Arr(0).SNF / 100, 3, MidpointRounding.ToEven)

            Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try

                clsMilkCollectionMCCDetail.SaveData(lblBMCDocNo.Text, txtBMCDate.Value, Arr, True, tran, isCorrection, isNewEntry, Remark)
                clsMilkCollectionMCC.HistoryUpdate(lblBMCDocNo.Text, tran)
                If (clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.AdjustFATSNFINOwnVSP, clsFixedParameterCode.AdjustFATSNFINOwnVSP, tran)) = 1) Then
                    qry = "select Document_No from TSPL_MILK_COLLECTION_DCS_MCC_DETAIL  where Against_Milk_Collection_MCC_Detail=" + lblBMCDetailNo.Text + ""
                    dt = clsDBFuncationality.GetDataTable(qry, tran)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        CorrectOwnDCSDocuemntTrans(clsCommon.myCstr(dt.Rows(0)("Document_No")), tran)
                    End If
                End If
                tran.Commit()
            Catch ex As Exception
                tran.Rollback()
                Throw New Exception(ex.Message)
            End Try
            clsCommon.MyMessageBoxShow(Me, "Data corrected sucessfully", Me.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton4_Click(sender As Object, e As EventArgs) Handles RadButton4.Click
        lblBMCStatus.Text = ""
        lblBMCDocNo.Text = Nothing
        txtBMCCorrQty.Value = Nothing
        txtBMCCorrQty.Tag = Nothing
        txtBMCCorrFAT.Value = Nothing
        txtBMCCorrFAT.Tag = Nothing
        txtBMCCorrSNF.Value = Nothing
        txtBMCCorrSNF.Tag = Nothing
        cboBMCCorrMilkType.SelectedValue = Nothing
        cboBMCCorrMilkType.Tag = Nothing
        txtBMCCorrBMC.Value = Nothing
        txtBMCCorrBMC.Tag = Nothing
        lblBMCSno.Text = Nothing
        lblBMCDetailNo.Text = Nothing
        RadGroupBox3.Enabled = True
        RadGroupBox4.Enabled = False
    End Sub

    Private Sub txtBMCTankerRoute__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtBMCTankerRoute._MYValidating
        Try
            Dim qry As String = " select ROUTE_NO as Code,ROUTE_NAME as Name from  TSPL_BULK_ROUTE_MASTER "
            Dim whrCls As String = ""
            If Not SettShowAllMCC Then
                whrCls = "exists(select 1 from TSPL_BULK_ROUTE_MASTER_MCC where TSPL_BULK_ROUTE_MASTER_MCC.ROUTE_NO=TSPL_BULK_ROUTE_MASTER.ROUTE_NO )"
            End If

            txtBMCTankerRoute.Value = clsCommon.ShowSelectForm("dd33ShUp1", qry, "Code", whrCls, txtBMCTankerRoute.Value, "Code", isButtonClicked)
            lblBMCTankerRoute.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ROUTE_NAME from TSPL_BULK_ROUTE_MASTER where ROUTE_NO='" + txtBMCTankerRoute.Value + "'"))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton6_Click(sender As Object, e As EventArgs) Handles RadButton6.Click
        Try
            If clsCommon.myLen(txtBMCTankerRoute.Value) <= 0 Then
                txtBMCTankerRoute.Focus()
                Throw New Exception("Please enter Route")
            End If


            Dim qry As String = "select TSPL_MILK_COLLECTION_MCC.Document_No,TSPL_MILK_COLLECTION_MCC.Document_Date,TSPL_MILK_COLLECTION_MCC.Trip_No,TSPL_MILK_COLLECTION_MCC.Route_Code,TSPL_MILK_COLLECTION_MCC.Tanker_No,TSPL_MILK_COLLECTION_MCC.Vehicle_No,TSPL_MILK_COLLECTION_MCC.Entered_Qty
                                ,Case When TSPL_MILK_COLLECTION_MCC.Entered_Qty>0 Then cast(TSPL_MILK_COLLECTION_MCC.Entered_FATKg*100/TSPL_MILK_COLLECTION_MCC.Entered_Qty as decimal(18,2)) Else 0 End as FATPer
                                ,TSPL_MILK_COLLECTION_MCC.Entered_FATKg as FATKg
                                ,Case When TSPL_MILK_COLLECTION_MCC.Entered_Qty>0 Then cast(TSPL_MILK_COLLECTION_MCC.Entered_SNFKg*100/TSPL_MILK_COLLECTION_MCC.Entered_Qty as decimal(18,2))  Else 0 End as SNFPer
                                ,TSPL_MILK_COLLECTION_MCC.Entered_SNFKg as SNFKG
                                from  TSPL_MILK_COLLECTION_MCC"
            Dim whr As String = " CONVERT(Date, TSPL_MILK_COLLECTION_MCC.Document_Date,103)='" + clsCommon.GetPrintDate(txtBMCTankerDate.Value, "dd/MMM/yyyy") + "' and TSPL_MILK_COLLECTION_MCC.Route_Code='" + txtBMCTankerRoute.Value + "' and Status=1"


            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry + " Where " + whr)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No BMC Milk Collection found")
            End If
            Dim strPK_Id As String = ""
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If dt.Rows.Count = 1 Then
                    strPK_Id = clsCommon.myCstr(dt.Rows(0)("Document_No"))
                Else
                    strPK_Id = clsCommon.ShowSelectForm("BMCfindco", qry, "Document_No", whr, strPK_Id, "Document_No", True)
                End If
            End If
            If clsCommon.myLen(strPK_Id) > 0 Then
                dt = clsDBFuncationality.GetDataTable(qry + " where " + whr + " and TSPL_MILK_COLLECTION_MCC.Document_No='" + strPK_Id + "'")

                lblBMCTankerDocNo.Text = clsCommon.myCstr(dt.Rows(0)("Document_No"))
                lblBMCTankerTripNo.Text = clsCommon.myCstr(dt.Rows(0)("Trip_No"))
                txtBMCTankerQty.Value = clsCommon.myCDecimal(dt.Rows(0)("Entered_Qty"))
                txtBMCTankerFAT.Value = clsCommon.myCdbl(dt.Rows(0)("FATPer"))
                lblBMCTankerFATKG.Text = clsCommon.myCdbl(dt.Rows(0)("FATKg"))
                txtBMCTankerSNF.Value = clsCommon.myCdbl(dt.Rows(0)("SNFPer"))
                lblBMCTankerSNFKG.Text = clsCommon.myCdbl(dt.Rows(0)("SNFKG"))
                RadGroupBox5.Enabled = False
                RadGroupBox6.Enabled = True
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtBMCTankerFAT_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtBMCTankerFAT.Validating
        lblBMCTankerFATKG.Text = Math.Round((txtBMCTankerQty.Value * txtBMCTankerFAT.Value / 100), 3, MidpointRounding.ToEven)
    End Sub


    Private Sub txtBMCTankerSNF_TextChanged(sender As Object, e As EventArgs) Handles txtBMCTankerSNF.TextChanged
        lblBMCTankerSNFKG.Text = Math.Round((txtBMCTankerQty.Value * txtBMCTankerSNF.Value / 100), 3, MidpointRounding.ToEven)
    End Sub

    Private Sub RadButton7_Click(sender As Object, e As EventArgs) Handles RadButton7.Click
        lblBMCTankerDocNo.Text = ""
        lblBMCTankerTripNo.Text = ""
        lblBMCDetailNo.Text = ""
        txtBMCTankerQty.Value = Nothing
        txtBMCTankerFAT.Value = Nothing
        lblBMCTankerFATKG.Text = Nothing
        txtBMCTankerSNF.Value = Nothing
        lblBMCTankerSNFKG.Text = Nothing
        RadGroupBox5.Enabled = True
        RadGroupBox6.Enabled = False
    End Sub
    Private Sub RadButton8_Click(sender As Object, e As EventArgs) Handles RadButton8.Click
        Try
            Dim Trans As SqlTransaction = Nothing
            If clsCommon.myLen(lblBMCTankerDocNo.Text) <= 0 Then
                Throw New Exception("Document No can't be blank")
            End If
            If txtBMCTankerQty.Value <= 0 Then
                Throw New Exception("Qty Can't be Zero/-ve")
            End If
            If txtBMCTankerFAT.Value <= 0 Then
                Throw New Exception("FAT % Can't be Zero/-ve")
            End If
            If txtBMCTankerSNF.Value <= 0 Then
                Throw New Exception("SNF % Can't be Zero/-ve")
            End If
            If clsCommon.myCDecimal(lblBMCTankerFATKG.Text) <= 0 Then
                Throw New Exception("FAT KG Can't be Zero/-ve")
            End If
            If clsCommon.myCDecimal(lblBMCTankerSNFKG.Text) <= 0 Then
                Throw New Exception("SNF KG Can't be Zero/-ve")
            End If
            If Not (clsCommon.CompairString(objCommonVar.CurrComp_Code1, "JPR") = CompairStringResult.Equal) Then
                Dim qry As String = "select TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE
from TSPL_MILK_COLLECTION_MCC_DETAIL
left outer join TSPL_MILK_COLLECTION_DCS_MCC_DETAIL on TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail=TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id 
left outer join TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No
left outer join TSPL_MILK_COLLECTION_DCS_DETAIL on TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No=TSPL_MILK_COLLECTION_DCS.Document_No
left outer join TSPL_MILK_SHIFT_UPLOADER_DETAIL on TSPL_MILK_SHIFT_UPLOADER_DETAIL.Against_Milk_Collection_DCS_Detail=TSPL_MILK_COLLECTION_DCS_DETAIL.PK_Id
left outer join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Against_Milk_Collection_DCS_Detail=TSPL_MILK_COLLECTION_DCS_DETAIL.PK_Id
inner join TSPL_MILK_SRN_HEAD on  TSPL_MILK_SRN_HEAD.Against_Shift_Uploader_TR_No=TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No 
left outer join TSPL_MILK_PURCHASE_INVOICE_DETAIL on TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE=TSPL_MILK_SRN_HEAD.DOC_CODE
where TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE is not null and TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No='" + lblBMCTankerDocNo.Text + "'
union all
select TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE
from TSPL_MILK_COLLECTION_MCC_DETAIL
left outer join TSPL_MILK_COLLECTION_DCS_MCC_DETAIL on TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail=TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id 
left outer join TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No
left outer join TSPL_MILK_COLLECTION_DCS_DETAIL on TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No=TSPL_MILK_COLLECTION_DCS.Document_No
left outer join TSPL_MILK_SHIFT_UPLOADER_DETAIL on TSPL_MILK_SHIFT_UPLOADER_DETAIL.Against_Milk_Collection_DCS_Detail=TSPL_MILK_COLLECTION_DCS_DETAIL.PK_Id
left outer join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Against_Milk_Collection_DCS_Detail=TSPL_MILK_COLLECTION_DCS_DETAIL.PK_Id
inner join TSPL_MILK_SRN_HEAD on  TSPL_MILK_SRN_HEAD.Against_Uploader_TR_No= TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No
left outer join TSPL_MILK_PURCHASE_INVOICE_DETAIL on TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE=TSPL_MILK_SRN_HEAD.DOC_CODE
where TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE is not null and TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No='" + lblBMCTankerDocNo.Text + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Throw New Exception("Milk Purchase Invoice Generated [" + clsCommon.myCstr(dt.Rows(0)("DOC_CODE")) + "]")
                End If
            End If

            Dim obj As New clsMilkCollectionMCC
            obj.Document_No = lblBMCTankerDocNo.Text
            If isCorrection = 1 Then
                obj.Entered_Qty = clsCommon.myCDecimal(txtBMCTankerQty.Value)
                obj.Entered_FATKg = clsCommon.myCDecimal(lblBMCTankerFATKG.Text)
                obj.Entered_SNFKg = clsCommon.myCDecimal(lblBMCTankerSNFKG.Text)
                obj.Correction_Qty = clsCommon.myCDecimal(txtBMCTankerQty.Value)
                obj.Correction_FAT = clsCommon.myCDecimal(txtBMCTankerFAT.Value)
                obj.Correction_SNF = clsCommon.myCDecimal(txtBMCTankerSNF.Value)
            ElseIf isCorrection = 2 Then
                obj.Entered_Qty = clsCommon.myCDecimal(txtBMCTankerQty.Value)
                obj.Entered_FATKg = clsCommon.myCDecimal(lblBMCTankerFATKG.Text)
                obj.Entered_SNFKg = clsCommon.myCDecimal(lblBMCTankerSNFKG.Text)
                obj.Retesting_FAT = clsCommon.myCDecimal(txtBMCTankerFAT.Value)
                obj.Retesting_SNF = clsCommon.myCDecimal(txtBMCTankerSNF.Value)
                Dim corrFactor As Double = clsFixedParameter.GetData(clsFixedParameterType.defaultCorrectionFactor, clsFixedParameterCode.MilkSetting, Trans)
                Dim CLR As Decimal = clsEkoPro.getClrOnCalculation(obj.Retesting_FAT, obj.Retesting_SNF, corrFactor)
                obj.Retesting_CLR = clsCommon.myCDecimal(CLR)
            End If
            clsMilkCollectionMCC.CorrectionData(obj, isCorrection)
            clsCommon.MyMessageBoxShow(Me, "Data corrected sucessfully", Me.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtBMCCorrSNF_Validated(sender As Object, e As EventArgs) Handles txtBMCCorrSNF.Validated
        Try
            If isPickCLRInsteadOfSNF AndAlso clsCommon.myCdbl(txtBMCCorrFAT.Value) > 0 AndAlso clsCommon.myCdbl(txtBMCCorrSNF.Value) > 0 Then
                txtRetestingCLR.Value = clsEkoPro.getClrOnCalculation(txtBMCCorrFAT.Value, txtBMCCorrSNF.Value, corrFactor)
            Else
                txtRetestingCLR.Value = 0
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtRetestingCLR_Validated(sender As Object, e As EventArgs) Handles txtRetestingCLR.Validated
        Try
            If isPickCLRInsteadOfSNF AndAlso clsCommon.myCdbl(txtBMCCorrFAT.Value) > 0 AndAlso clsCommon.myCdbl(txtRetestingCLR.Value) > 0 Then
                txtBMCCorrSNF.Value = clsEkoPro.getSnfOnCalculation(clsCommon.myCdbl(txtBMCCorrFAT.Value), clsCommon.myCdbl(txtRetestingCLR.Value), corrFactor)
            Else
                txtBMCCorrSNF.Value = 0
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtBMCCorrFAT_Validated(sender As Object, e As EventArgs) Handles txtBMCCorrFAT.Validated
        Try
            If isPickCLRInsteadOfSNF AndAlso clsCommon.myCdbl(txtBMCCorrFAT.Value) > 0 AndAlso clsCommon.myCdbl(txtRetestingCLR.Value) > 0 Then
                txtBMCCorrSNF.Value = clsEkoPro.getSnfOnCalculation(clsCommon.myCdbl(txtBMCCorrFAT.Value), clsCommon.myCdbl(txtRetestingCLR.Value), corrFactor)
                'Else
                '    txtBMCCorrSNF.Value = 0
            End If

            If isPickCLRInsteadOfSNF AndAlso clsCommon.myCdbl(txtBMCCorrFAT.Value) > 0 AndAlso clsCommon.myCdbl(txtBMCCorrSNF.Value) > 0 Then
                txtRetestingCLR.Value = clsEkoPro.getClrOnCalculation(txtBMCCorrFAT.Value, txtBMCCorrSNF.Value, corrFactor)
            Else
                txtRetestingCLR.Value = 0
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            If clsCommon.myLen(txtBMCDate.Value) <= 0 Then
                txtBMCRouteNo.Focus()
                Throw New Exception("Please enter date")
            End If
            If clsCommon.MyMessageBoxShow("Export data of " + clsCommon.GetPrintDate(txtBMCDate.Value, "dd/MM/yyyy") + " Date.", Me.Text, MessageBoxButtons.YesNo) = DialogResult.No Then
                Exit Sub
            End If
            Dim qry As String = "select TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id,TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No,TSPL_MILK_COLLECTION_MCC.Document_Date,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader as MCC, TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code,TSPL_MCC_MASTER.MCC_NAME
,TSPL_MILK_COLLECTION_MCC.Route_Code,TSPL_MILK_COLLECTION_MCC.Tanker_No,TSPL_MILK_COLLECTION_MCC.Trip_No
,TSPL_MILK_COLLECTION_MCC_DETAIL.Milk_Type,TSPL_MILK_COLLECTION_MCC_DETAIL.Qty,TSPL_MILK_COLLECTION_MCC_DETAIL.FAT,TSPL_MILK_COLLECTION_MCC_DETAIL.SNF,TSPL_MILK_COLLECTION_MCC_DETAIL.SNo,TSPL_MILK_COLLECTION_MCC_DETAIL.Sample_No
from TSPL_MILK_COLLECTION_MCC_DETAIL
left outer join  TSPL_MILK_COLLECTION_MCC on TSPL_MILK_COLLECTION_MCC.Document_No=TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No
left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code  
where TSPL_MILK_COLLECTION_MCC.Status=1 and convert(date, TSPL_MILK_COLLECTION_MCC.Document_Date,106)='" + clsCommon.GetPrintDate(txtBMCDate.Value, "dd/MMM/yyyy") + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No DCS Milk Collection found to export.")
            Else
                transportSql.ExporttoExcel(dt, Me)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        Dim gv As New UserControls.MyRadGridView
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "PK_Id", "Document_No", "Document_Date", "MCC", "MCC_Code", "MCC_NAME", "Route_Code", "Tanker_No", "Trip_No", "Milk_Type", "Qty", "FAT", "SNF", "SNo", "Sample_No") Then
            Try
                Dim CheckDocument As String = Nothing
                DtError = New DataTable
                DtError.Columns.Add("Code", GetType(String))
                DtError.Columns.Add("Error", GetType(String))
                For Each grow As GridViewRowInfo In gv.Rows
                    Try
                        If clsCommon.myLen(grow.Cells("Document_No").Value) <= 0 Then
                            Throw New Exception("Document No cannot be blank.")
                        End If
                        Dim qry As String = "PK_Id=" + clsCommon.myCstr(grow.Cells("PK_Id").Value) + ""
                        Dim Arr As List(Of clsMilkCollectionMCCDetail) = clsMilkCollectionMCCDetail.GetData(clsCommon.myCstr(grow.Cells("Document_No").Value), qry, Nothing)
                        If Arr Is Nothing OrElse Arr.Count <= 0 Then
                            Throw New Exception("Please select Valid document to correct")
                        End If

                        qry = "select Status from TSPL_MILK_COLLECTION_MCC  where Document_No='" + Arr(0).Document_No + "'"
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                            Throw New Exception("Please select Valid header document to correct")
                        ElseIf clsCommon.myCDecimal(dt.Rows(0)("Status")) <> 1 Then
                            Throw New Exception("Document [" + Arr(0).Document_No + "] should be posted")
                        End If

                        qry = "select TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE
from TSPL_MILK_COLLECTION_MCC_DETAIL
left outer join TSPL_MILK_COLLECTION_DCS_MCC_DETAIL on TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail=TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id 
left outer join TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No
left outer join TSPL_MILK_COLLECTION_DCS_DETAIL on TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No=TSPL_MILK_COLLECTION_DCS.Document_No
left outer join TSPL_MILK_SHIFT_UPLOADER_DETAIL on TSPL_MILK_SHIFT_UPLOADER_DETAIL.Against_Milk_Collection_DCS_Detail=TSPL_MILK_COLLECTION_DCS_DETAIL.PK_Id
left outer join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Against_Milk_Collection_DCS_Detail=TSPL_MILK_COLLECTION_DCS_DETAIL.PK_Id
inner join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.Against_Uploader_TR_No=TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No 
left outer join TSPL_MILK_PURCHASE_INVOICE_DETAIL on TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE=TSPL_MILK_SRN_HEAD.DOC_CODE
where TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE is not null and TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id=" + clsCommon.myCstr(grow.Cells("PK_Id").Value) + "
union all
select TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE
from TSPL_MILK_COLLECTION_MCC_DETAIL
left outer join TSPL_MILK_COLLECTION_DCS_MCC_DETAIL on TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail=TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id 
left outer join TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No
left outer join TSPL_MILK_COLLECTION_DCS_DETAIL on TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No=TSPL_MILK_COLLECTION_DCS.Document_No
left outer join TSPL_MILK_SHIFT_UPLOADER_DETAIL on TSPL_MILK_SHIFT_UPLOADER_DETAIL.Against_Milk_Collection_DCS_Detail=TSPL_MILK_COLLECTION_DCS_DETAIL.PK_Id
left outer join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Against_Milk_Collection_DCS_Detail=TSPL_MILK_COLLECTION_DCS_DETAIL.PK_Id
inner join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.Against_Shift_Uploader_TR_No=TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No
left outer join TSPL_MILK_PURCHASE_INVOICE_DETAIL on TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE=TSPL_MILK_SRN_HEAD.DOC_CODE
where TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE is not null and TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id=" + clsCommon.myCstr(grow.Cells("PK_Id").Value) + ""
                        dt = clsDBFuncationality.GetDataTable(qry)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            Throw New Exception("Milk Purchase Invoice Generated.")
                        End If
                        Arr(0).MCC_Code = clsCommon.myCstr(grow.Cells("MCC_Code").Value)
                        Arr(0).Milk_Type = clsCommon.myCstr(grow.Cells("Milk_Type").Value)
                        Dim QtyQry As String = "select TSPL_MILK_COLLECTION_MCC_DETAIL.Qty from TSPL_MILK_COLLECTION_MCC_DETAIL
                                left outer join  TSPL_MILK_COLLECTION_MCC on TSPL_MILK_COLLECTION_MCC.Document_No=TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No
                                left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code where convert(date, TSPL_MILK_COLLECTION_MCC.Document_Date,106)='" + clsCommon.GetPrintDate(grow.Cells("Document_Date").Value, "dd/MMM/yyyy") + "' And TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id=" + clsCommon.myCstr(grow.Cells("PK_Id").Value) + " "
                        Dim Qty As Decimal = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue(QtyQry))
                        'Arr(0).Qty = clsCommon.myCDecimal(grow.Cells("Qty").Value)
                        Arr(0).Qty = Qty
                        If clsCommon.CompairString(Form_ID, clsUserMgtCode.MilkRetesting) = CompairStringResult.Equal Then
                            Arr(0).Retesting_FAT = clsCommon.myCDecimal(grow.Cells("FAT").Value)
                            Arr(0).Retesting_SNF = clsCommon.myCDecimal(grow.Cells("SNF").Value)
                            Arr(0).Retesting_CLR = clsEkoPro.getClrOnCalculation(clsCommon.myCDecimal(grow.Cells("FAT").Value), clsCommon.myCDecimal(grow.Cells("SNF").Value), corrFactor)
                            Arr(0).Retesting_OR_Correction = 1
                        Else
                            Arr(0).Correction_FAT = clsCommon.myCDecimal(grow.Cells("FAT").Value)
                            Arr(0).Correction_SNF = clsCommon.myCDecimal(grow.Cells("SNF").Value)
                            Arr(0).Retesting_OR_Correction = 2
                        End If
                        Arr(0).FAT = clsCommon.myCDecimal(grow.Cells("FAT").Value)
                        Arr(0).SNF = clsCommon.myCDecimal(grow.Cells("SNF").Value)
                        Arr(0).FATKG = Math.Round(Arr(0).Qty * Arr(0).FAT / 100, 3, MidpointRounding.ToEven)
                        Arr(0).SNFKG = Math.Round(Arr(0).Qty * Arr(0).SNF / 100, 3, MidpointRounding.ToEven)

                        Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin
                        Try
                            clsMilkCollectionMCCDetail.SaveData(clsCommon.myCstr(grow.Cells("Document_No").Value), txtBMCDate.Value, Arr, True, Nothing, isCorrection, False)
                            clsMilkCollectionMCC.HistoryUpdate(clsCommon.myCstr(grow.Cells("Document_No").Value), tran)

                            If (clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.AdjustFATSNFINOwnVSP, clsFixedParameterCode.AdjustFATSNFINOwnVSP, tran)) = 1) Then
                                qry = "select Document_No from TSPL_MILK_COLLECTION_DCS_MCC_DETAIL  where Against_Milk_Collection_MCC_Detail=" + clsCommon.myCstr(grow.Cells("PK_Id").Value) + ""
                                dt = clsDBFuncationality.GetDataTable(qry, tran)
                                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                    CorrectOwnDCSDocuemntTrans(clsCommon.myCstr(dt.Rows(0)("Document_No")), tran)
                                End If
                            End If

                            tran.Commit()
                        Catch ex As Exception
                            tran.Rollback()
                            Throw New Exception(ex.Message)
                        End Try
                    Catch ex As Exception
                        dr = DtError.NewRow()
                        dr("Code") = clsCommon.myCstr(grow.Cells("Document_No").Value)
                        dr("Error") = ex.Message
                        DtError.Rows.Add(dr)
                    End Try
                Next
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Finally
                If DtError IsNot Nothing AndAlso DtError.Rows.Count > 0 Then
                    Dim frm As New FrmFreeGrid()
                    frm.strFormName = Me.Text
                    frm.dt = DtError
                    frm.ReportID = Form_ID
                    frm.ShowDialog()
                End If
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub btnTankerMilkExport_Click(sender As Object, e As EventArgs) Handles btnTankerMilkExport.Click
        Try
            If clsCommon.myLen(txtBMCTankerDate.Value) <= 0 Then
                txtBMCTankerRoute.Focus()
                Throw New Exception("Please enter Date")
            End If
            If clsCommon.MyMessageBoxShow("Export data of " + clsCommon.GetPrintDate(txtBMCTankerDate.Value, "dd/MM/yyyy") + " Date.", Me.Text, MessageBoxButtons.YesNo) = DialogResult.No Then
                Exit Sub
            End If
            Dim qry As String = "select TSPL_MILK_COLLECTION_MCC.Document_No,TSPL_MILK_COLLECTION_MCC.Document_Date,TSPL_MILK_COLLECTION_MCC.Trip_No,TSPL_MILK_COLLECTION_MCC.Route_Code,TSPL_MILK_COLLECTION_MCC.Tanker_No,TSPL_MILK_COLLECTION_MCC.Vehicle_No,TSPL_MILK_COLLECTION_MCC.Entered_Qty
                                ,Case When TSPL_MILK_COLLECTION_MCC.Entered_Qty>0 Then cast(TSPL_MILK_COLLECTION_MCC.Entered_FATKg*100/TSPL_MILK_COLLECTION_MCC.Entered_Qty as decimal(18,2)) Else 0 End as FATPer                                
                                ,Case When TSPL_MILK_COLLECTION_MCC.Entered_Qty>0 Then cast(TSPL_MILK_COLLECTION_MCC.Entered_SNFKg*100/TSPL_MILK_COLLECTION_MCC.Entered_Qty as decimal(18,2))  Else 0 End as SNFPer                                
                                from  TSPL_MILK_COLLECTION_MCC"
            qry += " where CONVERT(Date, TSPL_MILK_COLLECTION_MCC.Document_Date,103)='" + clsCommon.GetPrintDate(txtBMCTankerDate.Value, "dd/MMM/yyyy") + "' And Status=1"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No BMC Milk Collection found")
            Else
                transportSql.ExporttoExcel(dt, Me)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub btnTankerMilkImport_Click(sender As Object, e As EventArgs) Handles btnTankerMilkImport.Click
        Dim gv As New UserControls.MyRadGridView
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "Document_No", "Document_Date", "Trip_No", "Route_Code", "Tanker_No", "Vehicle_No", "Entered_Qty", "FATPer", "SNFPer") Then
            Try
                Dim CheckDocument As String = Nothing
                DtError = New DataTable
                DtError.Columns.Add("Code", GetType(String))
                DtError.Columns.Add("Error", GetType(String))
                For Each grow As GridViewRowInfo In gv.Rows
                    Try
                        If clsCommon.myLen(grow.Cells("Document_No").Value) <= 0 Then
                            Throw New Exception("Document No cannot be blank.")
                        End If

                        Dim qry As String = "select TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE
from TSPL_MILK_COLLECTION_MCC_DETAIL
left outer join TSPL_MILK_COLLECTION_DCS_MCC_DETAIL on TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail=TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id 
left outer join TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No
left outer join TSPL_MILK_COLLECTION_DCS_DETAIL on TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No=TSPL_MILK_COLLECTION_DCS.Document_No
left outer join TSPL_MILK_SHIFT_UPLOADER_DETAIL on TSPL_MILK_SHIFT_UPLOADER_DETAIL.Against_Milk_Collection_DCS_Detail=TSPL_MILK_COLLECTION_DCS_DETAIL.PK_Id
left outer join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Against_Milk_Collection_DCS_Detail=TSPL_MILK_COLLECTION_DCS_DETAIL.PK_Id
inner join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.Against_Uploader_TR_No=TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No  
left outer join TSPL_MILK_PURCHASE_INVOICE_DETAIL on TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE=TSPL_MILK_SRN_HEAD.DOC_CODE
where TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE is not null and TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No='" + clsCommon.myCstr(grow.Cells("Document_No").Value) + "'
union all
select TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE
from TSPL_MILK_COLLECTION_MCC_DETAIL
left outer join TSPL_MILK_COLLECTION_DCS_MCC_DETAIL on TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail=TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id 
left outer join TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No
left outer join TSPL_MILK_COLLECTION_DCS_DETAIL on TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No=TSPL_MILK_COLLECTION_DCS.Document_No
left outer join TSPL_MILK_SHIFT_UPLOADER_DETAIL on TSPL_MILK_SHIFT_UPLOADER_DETAIL.Against_Milk_Collection_DCS_Detail=TSPL_MILK_COLLECTION_DCS_DETAIL.PK_Id
left outer join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Against_Milk_Collection_DCS_Detail=TSPL_MILK_COLLECTION_DCS_DETAIL.PK_Id
inner join TSPL_MILK_SRN_HEAD on  TSPL_MILK_SRN_HEAD.Against_Shift_Uploader_TR_No=TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No
left outer join TSPL_MILK_PURCHASE_INVOICE_DETAIL on TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE=TSPL_MILK_SRN_HEAD.DOC_CODE
where TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE is not null and TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No='" + clsCommon.myCstr(grow.Cells("Document_No").Value) + "'"

                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            Throw New Exception("Milk Purchase Invoice Generated.")
                        End If
                        Dim obj As New clsMilkCollectionMCC

                        obj.Document_No = clsCommon.myCstr(grow.Cells("Document_No").Value)
                        'Dim Qty As Decimal = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue("select TSPL_MILK_COLLECTION_MCC.Entered_Qty from  TSPL_MILK_COLLECTION_MCC where CONVERT(Date, TSPL_MILK_COLLECTION_MCC.Document_Date,103)='" + clsCommon.GetPrintDate(clsCommon.myCstr(grow.Cells("Document_Date").Value), "dd/MMM/yyyy") + "' And Status=1 and TSPL_MILK_COLLECTION_MCC.Document_No='" + clsCommon.myCstr(grow.Cells("Document_No").Value) + "'"))
                        If isCorrection = 1 Then
                            obj.Entered_Qty = clsCommon.myCDecimal(grow.Cells("Entered_Qty").Value)
                            obj.Entered_FATKg = Math.Round((obj.Entered_Qty * clsCommon.myCDecimal(grow.Cells("FATPer").Value) / 100), 3, MidpointRounding.ToEven)
                            obj.Entered_SNFKg = Math.Round((obj.Entered_Qty * clsCommon.myCDecimal(grow.Cells("SNFPer").Value) / 100), 3, MidpointRounding.ToEven)
                            obj.Correction_Qty = clsCommon.myCDecimal(grow.Cells("Correction_Qty").Value)
                            obj.Correction_FAT = clsCommon.myCDecimal(grow.Cells("FATPer").Value)
                            obj.Correction_SNF = clsCommon.myCDecimal(grow.Cells("SNFPer").Value)

                        ElseIf isCorrection = 2 Then
                            obj.Entered_Qty = clsCommon.myCDecimal(grow.Cells("Entered_Qty").Value)
                            obj.Entered_FATKg = Math.Round((obj.Entered_Qty * clsCommon.myCDecimal(grow.Cells("FATPer").Value) / 100), 3, MidpointRounding.ToEven)
                            obj.Entered_SNFKg = Math.Round((obj.Entered_Qty * clsCommon.myCDecimal(grow.Cells("SNFPer").Value) / 100), 3, MidpointRounding.ToEven)
                            obj.Retesting_FAT = clsCommon.myCDecimal(grow.Cells("FATPer").Value)
                            obj.Retesting_SNF = clsCommon.myCDecimal(grow.Cells("SNFPer").Value)
                        End If

                        'obj.Document_No = clsCommon.myCstr(grow.Cells("Document_No").Value)
                        'obj.Entered_Qty = clsCommon.myCDecimal(grow.Cells("Entered_Qty").Value)
                        'obj.Entered_FATKg = clsCommon.myCDecimal(grow.Cells("FATKg").Value)
                        'obj.Entered_SNFKg = clsCommon.myCDecimal(grow.Cells("SNFKg").Value)

                        'Math.Round((txtBMCTankerQty.Value * txtBMCTankerFAT.Value / 100), 3, MidpointRounding.ToEven)
                        'lblBMCTankerDocNo.Text = clsCommon.myCstr(grow.Cells("Document_No"))
                        'lblBMCTankerTripNo.Text = clsCommon.myCstr(grow.Cells("Trip_No"))
                        'txtBMCTankerQty.Value = clsCommon.myCDecimal(grow.Cells("Entered_Qty"))
                        'txtBMCTankerFAT.Value = clsCommon.myCdbl(grow.Cells("FATPer"))
                        'lblBMCTankerFATKG.Text = clsCommon.myCdbl(grow.Cells("FATKg"))
                        'txtBMCTankerSNF.Value = clsCommon.myCdbl(grow.Cells("SNFPer"))
                        'lblBMCTankerSNFKG.Text = clsCommon.myCdbl(grow.Cells("SNFKG"))

                        clsMilkCollectionMCC.CorrectionData(obj, isCorrection)
                        'clsCommon.MyMessageBoxShow(Me, "Data corrected sucessfully", Me.Text)
                    Catch ex As Exception
                        dr = DtError.NewRow()
                        dr("Code") = clsCommon.myCstr(grow.Cells("Document_No").Value)
                        dr("Error") = ex.Message
                        DtError.Rows.Add(dr)
                    End Try
                Next
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Finally
                If DtError IsNot Nothing AndAlso DtError.Rows.Count > 0 Then
                    Dim frm As New FrmFreeGrid()
                    frm.strFormName = Me.Text
                    frm.dt = DtError
                    frm.ReportID = Form_ID
                    frm.ShowDialog()
                End If
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub txtBMCTankerQty_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtBMCTankerQty.Validating
        lblBMCTankerFATKG.Text = Math.Round((txtBMCTankerQty.Value * txtBMCTankerFAT.Value / 100), 3, MidpointRounding.ToEven)
        lblBMCTankerSNFKG.Text = Math.Round((txtBMCTankerQty.Value * txtBMCTankerSNF.Value / 100), 3, MidpointRounding.ToEven)
    End Sub

    Private Sub chkMarkAsAdulteration_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkMarkAsAdulteration.ToggleStateChanged
        txtSuspenceRemarks.Visible = chkMarkAsAdulteration.Checked
        RadLabel14.Visible = chkMarkAsAdulteration.Checked
    End Sub

    Private Sub txtCAPMCC__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCAPMCC._MYValidating
        Dim qry As String = ""
        Dim arrMCCRights As ArrayList = clsMCCCodes.GetUserHavingMCCRights()
        qry = "select * from ( select tspl_mcc_master.MCC_Code as [Code] ,tspl_mcc_master.MCC_Type as [Mcc Type] ,tspl_mcc_master.MCC_NAME as [Mcc Name] ,tspl_mcc_master.Chilling_Vendor as [Chilling Vendor] ,tspl_mcc_master.Add1 as [Address1] ,tspl_mcc_master.Add2 as [Address2] ,tspl_mcc_master.Tehsil as [Tehsil] ,tspl_mcc_master.City_code as [City Code] ,tspl_mcc_master.State_Code as [State Code] ,tspl_mcc_master.Country_code as [Country Code] ,tspl_mcc_master.Pin_code as [Pin Code],tspl_mcc_master.Pan_No as [Pan No] ,tspl_mcc_master.Telphone as [Telphone] ,tspl_mcc_master.Email as [Email] ,tspl_mcc_master.Fax as [Fax] ,tspl_mcc_master.MCC_Area as [Mcc Area] ,tspl_mcc_master.Area_Of_Store as [Area Of Store] ,tspl_mcc_master.Area_Of_Office as [Area Of Office] ,tspl_mcc_master.Open_Area_For_tanker as [Open Area For Tanker] ,tspl_mcc_master.Area_Of_LAB as [Area Of Lab] ,tspl_mcc_master.No_Of_SILO as [No Of Silo] ,tspl_mcc_master.Total_Storage_capacity as [Total Storage Capacity] ,tspl_mcc_master.Area_Of_Receiving_DOCK as [Area Of Receiving Dock] ,tspl_mcc_master.No_Of_Chiller as [No Of Chiller] ,tspl_mcc_master.Chiller_Brand_Name as [Chiller Brand Name] ,tspl_mcc_master.Chiller_Capacity as [Chiller Capacity] ,tspl_mcc_master.No_Of_MilkPump as [No Of Milkpump] ,tspl_mcc_master.MilkPump_Capacity as [Milkpump Capacity] ,tspl_mcc_master.DripSaver as [Drip Saver] ,tspl_mcc_master.CanWasher as [Can Washer] ,tspl_mcc_master.CanScrubber as [Can Scrubber] ,tspl_mcc_master.FSSAI_NO as [FSSAI No] ,tspl_mcc_master.ETP as [ETP] ,tspl_mcc_master.Earthing as [Earthing] ,tspl_mcc_master.Coil_Length as [Coil Length] ,tspl_mcc_master.Electricity_Connection as [Electricity Connection] ,tspl_mcc_master.Boiler as [Boiler] ,tspl_mcc_master.NoOfDG as [No. of DG] ,tspl_mcc_master.NoOfCompressor as [No. of Compressor] ,tspl_mcc_master.PayeeName as [Payee Name] ,tspl_mcc_master.BankName as [Bank Name] ,tspl_mcc_master.BankBranch as [Bank Branch] ,tspl_mcc_master.BankCityCode as [Bank City Code] ,tspl_mcc_master.BankStateCode as [Bank State Code] ,tspl_mcc_master.IFCICode as [IFCI Code] ,tspl_mcc_master.AccountNO as [Account No] ,tspl_mcc_master.Created_By as [Created By] ,tspl_mcc_master.Created_Date as [Created Date] ,tspl_mcc_master.Modified_By as [Modified By] ,tspl_mcc_master.Modified_Date as [Modified Date] ,tspl_mcc_master.Comp_Code as [Company Code],tspl_mcc_master.mcc_code_vlc_uploader as [MCC Code For VLC Uploder],tspl_mcc_master.Plant_Code AS [Plant Code],TSPL_LOCATION_MASTER_PLANT.Location_Desc AS [Plant Name] from tspl_mcc_master LEFT JOIN TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_PLANT ON TSPL_LOCATION_MASTER_PLANT.Location_Code=tspl_mcc_master.Plant_Code  inner join tspl_location_master on tspl_location_master.location_Code= tspl_mcc_master.mcc_Code where tspl_mcc_master.mcc_Code in (" & StrPermission & ") " _
        & " and (  tspl_mcc_master.mcc_Code in (" & clsCommon.GetMulcallString(arrMCCRights) & ")))xx "
        txtCAPMCC.Value = clsCommon.ShowSelectForm("frmCorrection@MCC", qry, "Code", "", txtCAPMCC.Value, "", isButtonClicked)
        If txtCAPMCC.Value IsNot Nothing AndAlso clsCommon.myLen(txtCAPMCC.Value) > 0 Then
            lblMcc.Text = clsDBFuncationality.getSingleValue(" select MCC_NAME from TSPL_Mcc_MASTER where MCC_Code = '" + txtCAPMCC.Value + "'", Nothing)
        End If
    End Sub

    Private Sub txtCAPVLC__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCAPVLC._MYValidating
        Try
            If clsCommon.myLen(txtCAPMCC.Value) <= 0 AndAlso Not MultipleFinderFillAuto Then
                txtCAPMCC.Focus()
                Throw New Exception("Please provide MCC code ")
            End If
            Dim qry As String = "select TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [Uploader_Code], TSPL_VLC_MASTER_HEAD.VLC_Code AS [DCS Code],TSPL_VLC_MASTER_HEAD.VLC_Name as [DCS NAME], TSPL_VLC_MASTER_HEAD.Route_Code as [Route Code],TSPL_MCC_ROUTE_MASTER.Route_Name,TSPL_MCC_ROUTE_MASTER.Vehicle_Code,TSPL_VLC_MASTER_HEAD.VSP_Code AS [Secretary Code],TSPL_VENDOR_MASTER.Vendor_Name as [Secretary Name] " + Environment.NewLine +
            " from TSPL_VLC_MASTER_HEAD" + Environment.NewLine +
            " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code  " + Environment.NewLine +
            " left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_VLC_MASTER_HEAD.Route_Code  " + Environment.NewLine +
            " left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC " + Environment.NewLine

            Dim whrCls As String = "  isnull(TSPL_VLC_MASTER_HEAD.IsSuspense,0)=0  "
            If Not MultipleFinderFillAuto Then
                whrCls += " and TSPL_VLC_MASTER_HEAD.MCC  ='" + txtCAPMCC.Value + "'"
            End If

            txtCAPVLC.Value = clsCommon.ShowSelectForm("SMSRNaC", qry, "Uploader_Code", whrCls, txtCAPVLC.Value, "Uploader_Code", isButtonClicked)
            qry = "select TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VLC_MASTER_HEAD.MCC from TSPL_VLC_MASTER_HEAD where TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader='" + txtCAPVLC.Value + "' "
            If Not MultipleFinderFillAuto Then
                whrCls += "  TSPL_VLC_MASTER_HEAD.MCC  ='" + txtCAPMCC.Value + "'"
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                txtCAPVLC.Tag = clsCommon.myCstr(dt.Rows(0)("VLC_Code"))
                lblCAPVLC.Text = clsCommon.myCstr(dt.Rows(0)("VLC_Name"))
                If MultipleFinderFillAuto Then
                    txtCAPMCC.Value = clsCommon.myCstr(dt.Rows(0)("MCC"))
                End If
            Else
                txtCAPVLC.Tag = Nothing
                lblCAPVLC.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton9_Click(sender As Object, e As EventArgs) Handles RadButton9.Click
        Try
            If clsCommon.myLen(txtCAPMCC.Value) <= 0 AndAlso Not MultipleFinderFillAuto Then
                txtCAPMCC.Focus()
                Throw New Exception("Please enter MCC")
            End If
            If clsCommon.myLen(cboCAPShift.SelectedValue) <= 0 Then
                cboCAPShift.Focus()
                Throw New Exception("Please Select shift")
            End If
            If clsCommon.myLen(txtCAPVLC.Tag) <= 0 Then
                txtCAPVLC.Focus()
                Throw New Exception("Please enter VLC")
            End If
            Dim qry As String = Nothing
            qry = "select TSPL_MILK_SRN_HEAD.DOC_CODE as SRNNo,TSPL_MILK_SRN_HEAD.Dock_Collection_Milk_Type as MilkType
,TSPL_MILK_SRN_DETAIL.Qty,TSPL_MILK_SRN_DETAIL.Item_Code, TSPL_MILK_SRN_DETAIL.UOM_Code,TSPL_MILK_SRN_DETAIL.FAT_PER,TSPL_MILK_SRN_DETAIL.SNF_PER
,TSPL_MILK_SRN_DETAIL.Retesting_FAT,TSPL_MILK_SRN_DETAIL.Retesting_SNF,TSPL_MILK_SRN_DETAIL.Retesting_OR_Correction_Status
,(Case When Retesting_OR_Correction_Status=1 Then TSPL_MILK_SRN_DETAIL.Retesting_FAT Else (Case When Retesting_OR_Correction_Status=2 Then TSPL_MILK_SRN_DETAIL.FAT_PER Else TSPL_MILK_SRN_DETAIL.Retesting_FAT End)End) As FAT
,(Case When Retesting_OR_Correction_Status=1 Then TSPL_MILK_SRN_DETAIL.Retesting_SNF Else (Case When Retesting_OR_Correction_Status=2 Then TSPL_MILK_SRN_DETAIL.SNF_PER Else TSPL_MILK_SRN_DETAIL.Retesting_SNF End) End) As SNF 
,case when TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No is not null then TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Reject_Type else TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type end Reject_Type
from TSPL_MILK_SRN_DETAIL
left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE
left outer join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Uploader_TR_No
left outer join TSPL_MILK_SHIFT_UPLOADER_DETAIL on TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Shift_Uploader_TR_No
inner join TSPL_MILK_PURCHASE_INVOICE_DETAIL on TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE 
left outer join TSPL_MILK_SRN_CORRECTION_AFTER_PROCESS on TSPL_MILK_SRN_CORRECTION_AFTER_PROCESS.DOC_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE "
            Dim whr As String = "  convert(date, TSPL_MILK_SRN_HEAD.DOC_DATE,106)='" + clsCommon.GetPrintDate(txtCAPShiftDate.Value, "dd/MMM/yyyy") + "' and TSPL_MILK_SRN_HEAD.SHIFT='" + clsCommon.myCstr(cboCAPShift.SelectedValue) + "' and TSPL_MILK_SRN_HEAD.VLC_CODE='" + clsCommon.myCstr(txtCAPVLC.Tag) + "' and TSPL_MILK_SRN_CORRECTION_AFTER_PROCESS.DOC_CODE is null "
            If Not MultipleFinderFillAuto Then
                whr += " and TSPL_MILK_SRN_HEAD.MCC_CODE='" + txtCAPMCC.Value + "' "
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry + " Where " + whr)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Milk SRN found")
            End If

            Dim srnNo As String = ""
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If dt.Rows.Count = 1 Then
                    srnNo = clsCommon.myCstr(dt.Rows(0)("SRNNo"))
                Else
                    srnNo = clsCommon.ShowSelectForm("SRNCorrfcap", qry, "SRNNo", whr, srnNo, "SRNNo", True)
                End If
            End If
            If clsCommon.myLen(srnNo) > 0 Then
                dt = clsDBFuncationality.GetDataTable(qry + " where " + whr + " and TSPL_MILK_SRN_HEAD.DOC_CODE='" + srnNo + "'")
                lblCAPSRNNo.Text = clsCommon.myCstr(dt.Rows(0)("SRNNo"))
                txtCAPQty.Value = clsCommon.myCdbl(dt.Rows(0)("Qty"))
                txtCAPQty.Tag = clsCommon.myCdbl(dt.Rows(0)("Qty"))
                lblCAPUOM.Tag = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
                lblCAPUOM.Text = clsCommon.myCstr(dt.Rows(0)("UOM_Code"))
                If clsCommon.myCdbl(dt.Rows(0)("Retesting_OR_Correction_Status")) > 0 Then
                    txtCAPFAT.Value = clsCommon.myCdbl(dt.Rows(0)("FAT"))
                    txtCAPFAT.Tag = clsCommon.myCdbl(dt.Rows(0)("FAT"))
                    txtCAPSNF.Value = clsCommon.myCdbl(dt.Rows(0)("SNF"))
                    txtCAPSNF.Tag = clsCommon.myCdbl(dt.Rows(0)("SNF"))
                Else
                    txtCAPFAT.Value = clsCommon.myCdbl(dt.Rows(0)("FAT_PER"))
                    txtCAPFAT.Tag = clsCommon.myCdbl(dt.Rows(0)("FAT_PER"))
                    txtCAPSNF.Value = clsCommon.myCdbl(dt.Rows(0)("SNF_PER"))
                    txtCAPSNF.Tag = clsCommon.myCdbl(dt.Rows(0)("SNF_PER"))
                End If
                cboCAPMilkType.SelectedValue = clsCommon.myCstr(dt.Rows(0)("MilkType"))
                cboCAPMilkType.Tag = clsCommon.myCstr(dt.Rows(0)("MilkType"))
                cboCAPRejectType.SelectedValue = clsCommon.myCstr(dt.Rows(0)("Reject_Type"))
                cboCAPRejectType.Tag = clsCommon.myCstr(dt.Rows(0)("Reject_Type"))

                TxtCAPDCSCode.Value = txtCAPVLC.Value
                TxtCAPDCSCode.Tag = txtCAPVLC.Tag
                lblCAPDCSName.Text = lblCAPVLC.Text

                RadGroupBox9.Enabled = False
                RadGroupBox10.Enabled = True

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton11_Click(sender As Object, e As EventArgs) Handles btnCAPSave.Click
        Try
            Try
                If txtCAPQty.Value <= 0 Then
                    Throw New Exception("Please enter Qty")
                End If
                If txtCAPFAT.Value <= 0 Then
                    Throw New Exception("Please enter FAT")
                End If
                If txtCAPSNF.Value <= 0 Then
                    Throw New Exception("Please enter SNF")
                End If
                If clsCommon.myLen(cboCAPMilkType.SelectedValue) <= 0 Then
                    Throw New Exception("Please select Milk type")
                End If
                If clsCommon.myLen(TxtCAPDCSCode.Value) <= 0 Then
                    Throw New Exception("Please select DCS")
                End If
                If clsCommon.myLen(TxtCAPRemarks.Text) <= 0 Then
                    Throw New Exception("Please enter remarks")
                End If
                If clsCommon.MyMessageBoxShow(Me, "Please verify the filled details:" + Environment.NewLine + "DCS [" & TxtCAPDCSCode.Value & "], Quantity [" & txtCAPQty.Value & "], FAT [" & txtCAPFAT.Value & "], SNF [" & txtCAPSNF.Value & "] " + Environment.NewLine + "Note: This correction is one-time only and cannot be changed later. Are you sure you want to proceed with these changes?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes Then
                    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                    Try
                        Dim DCSChangesDrNoteAmount As Decimal = 0
                        Dim dcsDRCR_Amt As Decimal = 0
                        Dim servdate As DateTime = clsCommon.GETSERVERDATE(trans)
                        Dim PickPriceFromFATAndSNF As Boolean = (clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.MilkProcuremntPickCLRInsteadOfSNF, clsFixedParameterCode.PickPriceFromFATAndSNF, trans)) > 0)
                        Dim qry As String = "select TSPL_MILK_SRN_HEAD.MCC_CODE from TSPL_MILK_PURCHASE_INVOICE_DETAIL
 left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE
 where TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE='" + lblCAPSRNNo.Text + "'"
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                            Throw New Exception("Please select srn whose invoice is genereated")
                        End If
                        txtCAPMCC.Value = clsCommon.myCstr(dt.Rows(0)("MCC_CODE"))

                        qry = "select 1 from TSPL_MILK_SRN_CORRECTION_AFTER_PROCESS where DOC_CODE='" + lblCAPSRNNo.Text + "'"
                        dt = clsDBFuncationality.GetDataTable(qry, trans)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            Throw New Exception("Correction after process is already applied.")
                        End If

                        Dim RCDFQCControl As Decimal = clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.RCDFControl, clsFixedParameterCode.MaxFATPerLimit, trans))
                        If RCDFQCControl > 0 Then
                            If txtCAPFAT.Value > RCDFQCControl Then
                                Throw New Exception("As per RCDF QC policy FAT % can't be more than [" + clsCommon.myCstr(RCDFQCControl) + "]")
                            End If
                        End If

                        RCDFQCControl = clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.RCDFControl, clsFixedParameterCode.MaxSNFPerLimit, trans))
                        If RCDFQCControl > 0 Then
                            If txtCAPSNF.Value > RCDFQCControl Then
                                Throw New Exception("As per RCDF QC policy SNF % can't be more than [" + clsCommon.myCstr(RCDFQCControl) + "]")
                            End If
                        End If

                        Dim coll As New Hashtable()
                        clsCommon.AddColumnsForChange(coll, "DOC_CODE", lblCAPSRNNo.Text)
                        clsCommon.AddColumnsForChange(coll, "VLC_CODE", clsCommon.myCstr(TxtCAPDCSCode.Tag))
                        clsCommon.AddColumnsForChange(coll, "Qty", txtCAPQty.Value)
                        clsCommon.AddColumnsForChange(coll, "Uom_Code", lblCAPUOM.Text)
                        clsCommon.AddColumnsForChange(coll, "FAT_PER", txtCAPFAT.Value)
                        Dim dblKGQty As Decimal = 0
                        Dim conv_fac As Decimal = clsWeightConversionInfo.GetConversion_factor(clsCommon.myCstr(lblCAPUOM.Tag), lblCAPUOM.Text, IIf(clsCommon.CompairString(lblCAPUOM.Text, "KG") = CompairStringResult.Equal, "LTR", "KG"), trans)

                        If clsCommon.CompairString(lblCAPUOM.Text, "KG") = CompairStringResult.Equal Then
                            dblKGQty = txtCAPQty.Value
                            clsCommon.AddColumnsForChange(coll, "ACC_QTY", dblKGQty)
                            clsCommon.AddColumnsForChange(coll, "ACC_Qty_LTR", txtCAPQty.Value * conv_fac)
                        Else
                            dblKGQty = txtCAPQty.Value * conv_fac
                            clsCommon.AddColumnsForChange(coll, "ACC_Qty_LTR", txtCAPQty.Value)
                            clsCommon.AddColumnsForChange(coll, "ACC_QTY", dblKGQty)
                        End If
                        clsCommon.AddColumnsForChange(coll, "FAT_KG", clsERPFuncationality.myFloor(dblKGQty * txtCAPFAT.Value / 100, objCommonVar.MilkSRNFATSNFDecimalPlaces))
                        clsCommon.AddColumnsForChange(coll, "SNF_PER", txtCAPSNF.Value)
                        clsCommon.AddColumnsForChange(coll, "SNF_KG", clsERPFuncationality.myFloor(dblKGQty * txtCAPSNF.Value / 100, objCommonVar.MilkSRNFATSNFDecimalPlaces))
                        Dim clr As Decimal = clsEkoPro.getClrOnCalculation(txtCAPFAT.Value, txtCAPSNF.Value, corrFactor)
                        clsCommon.AddColumnsForChange(coll, "CLR", clr)
                        Dim strPriceCode As String = ""
                        Dim Rate As Decimal = clsEkoPro.getRateAndPriceCodeFromUploaderShiftWise(txtCAPQty.Value, strPriceCode, txtCAPFAT.Value, txtCAPSNF.Value, txtCAPMCC.Value, txtCAPVLC.Value, IIf(clsCommon.myCstr(cboCAPShift.SelectedValue).Contains("M"), "M", "E"), txtCAPShiftDate.Value, trans, clsCommon.myCstr(cboCAPMilkType.SelectedValue), 0, 0)
                        clsCommon.AddColumnsForChange(coll, "Price_Code", strPriceCode)
                        clsCommon.AddColumnsForChange(coll, "RATE", Rate)
                        Dim Amt As Decimal = Rate * txtCAPQty.Value
                        clsCommon.AddColumnsForChange(coll, "AMOUNT", Amt)
                        clsCommon.AddColumnsForChange(coll, "Remarks", TxtCAPRemarks.Text)
                        clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(servdate, "dd/MMM/yyyy hh:mm:ss tt"))

                        qry = "select top 1 AMOUNT from (
select TSPL_MILK_SRN_HEAD.DOC_DATE,Qty,FAT_PER,SNF_PER,RATE, AMOUNT,1 as RI from TSPL_MILK_SRN_DETAIL 
left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE
where TSPL_MILK_SRN_DETAIL.DOC_CODE='" & lblCAPSRNNo.Text & "'
union all
select TSPL_MILK_SRN_CORRECTION_AFTER_PROCESS.Created_Date, OwnDCS_Qty,OwnDCS_FAT_PER,OwnDCS_SNF_PER, OwnDCS_RATE, OwnDCS_AMOUNT,2 as RI 
from TSPL_MILK_SRN_CORRECTION_AFTER_PROCESS where  OwnDCS_DOC_CODE='" & lblCAPSRNNo.Text & "'
) xx order by DOC_DATE desc"
                        dcsDRCR_Amt = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue(qry, trans))
                        If clsCommon.CompairString(clsCommon.myCstr(txtCAPVLC.Tag), clsCommon.myCstr(TxtCAPDCSCode.Tag)) = CompairStringResult.Equal Then
                            dcsDRCR_Amt = Amt - dcsDRCR_Amt
                        Else
                            DCSChangesDrNoteAmount = dcsDRCR_Amt
                            dcsDRCR_Amt = Amt
                        End If
                        clsCommon.AddColumnsForChange(coll, "DRCR_Amt", dcsDRCR_Amt)
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_SRN_CORRECTION_AFTER_PROCESS", OMInsertOrUpdate.Insert, "", trans)

                        Dim dclDRCROwnDCS As Decimal = 0
                        Dim isOwnBMC As Boolean = False
                        Dim strOwnBMC As String = ""
                        Dim strDCSDocNo As String = ""
                        qry = "select TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No,TSPL_MILK_SRN_HEAD.VLC_CODE,tabMCC.MCC_Code, (case when TSPL_VLC_MASTER_HEAD.isOwnBMC=1 and TSPL_VLC_MASTER_HEAD.MCCOwnBMC=tabMCC.MCC_Code then 1 else 0 end) isOwnBMC   
from TSPL_MILK_SRN_HEAD
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_SRN_HEAD.VLC_CODE
left outer join TSPL_MILK_SHIFT_UPLOADER_DETAIL on TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Shift_Uploader_TR_No
left outer join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Uploader_TR_No
left outer join TSPL_MILK_COLLECTION_DCS_DETAIL on TSPL_MILK_COLLECTION_DCS_DETAIL .PK_Id=COALESCE( TSPL_MILK_SHIFT_UPLOADER_DETAIL.Against_Milk_Collection_DCS_Detail,TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Against_Milk_Collection_DCS_Detail)
left outer join (select TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No,max(TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code) as MCC_Code
from TSPL_MILK_COLLECTION_DCS_MCC_DETAIL 
left outer join TSPL_MILK_COLLECTION_MCC_DETAIL on TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id=TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail
group by TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No 
) as tabMCC on tabMCC.Document_No=TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
where TSPL_MILK_SRN_HEAD.DOC_CODE in ('" + lblCAPSRNNo.Text + "')"
                        dt = clsDBFuncationality.GetDataTable(qry, trans)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            isOwnBMC = (clsCommon.myCdbl(dt.Rows(0)("isOwnBMC")) = 1)
                            strOwnBMC = clsfrmVLCMaster.OwnBMCCodeByMCC(clsCommon.myCstr(dt.Rows(0)("MCC_Code")), trans)
                            strDCSDocNo = clsCommon.myCstr(dt.Rows(0)("Document_No"))
                        End If

                        If Not isOwnBMC Then
                            Dim settSNFDecimalPlace As Integer = clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.SNFDecimalPlaces, clsFixedParameterCode.SNFDecimalPlaces, trans))
                            Dim settMaxFATPerLimit As Decimal = clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.MaxFATPerLimit, clsFixedParameterCode.MaxFATPerLimit, trans))
                            Dim settMaxSNFPerLimit As Decimal = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MaxSNFPerLimit, clsFixedParameterCode.MaxSNFPerLimit, trans))
                            Dim isPickCLRInsteadOfSNF As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProcuremntPickCLRInsteadOfSNF, clsFixedParameterCode.MilkProcuremntPickCLRInsteadOfSNF, trans)) > 0)

                            qry = "select xx.* from (
select max(case when isOwnBMC=1 then x.PK_Id else '' end) as PK_Id, max(isOwnBMC) as isOwnBMC, 
sum(MCCQty) as MCCQty,sum(Qty) as TotQty,sum(Qty)-sum(MCCQty) as DiffQty,
sum(MCCFATKG) as MCCFATKG,sum(FATKG) as TotFATKG,sum(FATKG)-sum(MCCFATKG) as DiffFATKG,
sum(MCCSNFKG) as MCCSNFKG,sum(SNFKG) as TotSNFKG,sum(SNFKG)-sum(MCCSNFKG) as DiffSNFKG,
max(case when isOwnBMC=1 then x.VLC_Code else '' end ) as VLC_Code,max(Document_No)Document_No
from (
select TSPL_MILK_COLLECTION_DCS_DETAIL.PK_Id, case when isnull(TSPL_VLC_MASTER_HEAD.isOwnBMC,0)=1 and TSPL_VLC_MASTER_HEAD.MCC=Tab.MCC_Code then 1 else 0 end as isOwnBMC,0.00 as MCCQty,0.00 as MCCFATKG,0.00 as MCCSNFKG
,COALESCE( TSPL_MILK_SRN_CORRECTION_AFTER_PROCESS.Qty,TSPL_MILK_SRN_DETAIL.Qty,TSPL_MILK_COLLECTION_DCS_DETAIL.Qty) as Qty
,COALESCE(TSPL_MILK_SRN_CORRECTION_AFTER_PROCESS.FAT_KG,TSPL_MILK_SRN_DETAIL.FAT_KG,TSPL_MILK_COLLECTION_DCS_DETAIL.FATKG ) as FATKG
,COALESCE(TSPL_MILK_SRN_CORRECTION_AFTER_PROCESS.SNF_KG,TSPL_MILK_SRN_DETAIL.SNF_KG,TSPL_MILK_COLLECTION_DCS_DETAIL.SNFKG ) as SNFKG
,TSPL_MILK_COLLECTION_DCS_DETAIL.VLC_Code,TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
from TSPL_MILK_SRN_HEAD
left outer join TSPL_MILK_SRN_DETAIL on TSPL_MILK_SRN_DETAIL.DOC_CODE=TSPL_MILK_SRN_HEAD.DOC_CODE
left outer join TSPL_MILK_SRN_CORRECTION_AFTER_PROCESS on TSPL_MILK_SRN_CORRECTION_AFTER_PROCESS.DOC_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE
left outer join TSPL_MILK_SHIFT_UPLOADER_DETAIL on TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Shift_Uploader_TR_No
left outer join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Uploader_TR_No
left outer join TSPL_MILK_COLLECTION_DCS_DETAIL on TSPL_MILK_COLLECTION_DCS_DETAIL .PK_Id=COALESCE( TSPL_MILK_SHIFT_UPLOADER_DETAIL.Against_Milk_Collection_DCS_Detail,TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Against_Milk_Collection_DCS_Detail)
left outer join TSPL_MILK_COLLECTION_DCS on  TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_COLLECTION_DCS_DETAIL.VLC_Code
left outer join (
select Document_No,max(MCC_Code) as MCC_Code from (

select TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No,TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code 
from TSPL_MILK_COLLECTION_DCS_MCC_DETAIL 
inner join TSPL_MILK_COLLECTION_MCC_DETAIL on TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id=TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail 
)xx group by Document_No 
) Tab on Tab.Document_No= TSPL_MILK_COLLECTION_DCS.Document_No
where TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No='" + strDCSDocNo + "' 
union all
select 0 as PK_Id, 0 as isOwnBMC,TSPL_MILK_COLLECTION_MCC_DETAIL.Qty as MCCQty,TSPL_MILK_COLLECTION_MCC_DETAIL.FATKG as MCCFATKG,TSPL_MILK_COLLECTION_MCC_DETAIL.SNFKG as MCCSNFKG,0.00 as Qty,0.00 as FATKG,0.00 as SNFKG,'' as VLC_Code,TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No from
TSPL_MILK_COLLECTION_DCS_MCC_DETAIL  
left outer join TSPL_MILK_COLLECTION_MCC_DETAIL on TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id=TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail
where TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No='" + strDCSDocNo + "' 
)x
)xx"
                            Dim dtDCS As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                            If dtDCS IsNot Nothing AndAlso dtDCS.Rows.Count > 0 Then
                                For Each drDCS As DataRow In dtDCS.Rows
                                    If clsCommon.myCdbl(drDCS("isOwnBMC")) = 1 Then
                                        If Math.Abs(clsCommon.myCdbl(drDCS("DiffFATKG"))) > 0 OrElse Math.Abs(clsCommon.myCdbl(drDCS("DiffSNFKG"))) > 0 Then
                                            qry = "select xx.*,TSPL_MILK_SRN_HEAD.DOC_CODE,TSPL_MILK_SRN_HEAD.Dock_Collection_Milk_Type from
(select PK_Id,Qty,FATKG,SNFKG,Shift,TSPL_MILK_REJECT_TYPE.Code as Milk_Type,TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No as DCSDocNo  
from TSPL_MILK_COLLECTION_DCS_DETAIL 
left outer join TSPL_MILK_REJECT_TYPE on TSPL_MILK_REJECT_TYPE.Code=TSPL_MILK_COLLECTION_DCS_DETAIL.Milk_Type 
WHERE VLC_Code =  ( SELECT VLC_Code FROM TSPL_MILK_COLLECTION_DCS_DETAIL where PK_Id=" + clsCommon.myCstr(drDCS("PK_Id")) + ")  
AND Document_No = ( SELECT Document_No FROM TSPL_MILK_COLLECTION_DCS_DETAIL WHERE PK_Id =" + clsCommon.myCstr(drDCS("PK_Id")) + ") )xx
left outer join TSPL_MILK_SHIFT_UPLOADER_DETAIL on TSPL_MILK_SHIFT_UPLOADER_DETAIL.Against_Milk_Collection_DCS_Detail=xx.PK_Id
left outer join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Against_Milk_Collection_DCS_Detail=xx.PK_Id
OUTER APPLY ( SELECT TOP 1 SRN.DOC_CODE,SRN.Dock_Collection_Milk_Type FROM TSPL_MILK_SRN_HEAD SRN WHERE SRN.Against_Shift_Uploader_TR_No = TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No  OR SRN.Against_Uploader_TR_No = TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No ) TSPL_MILK_SRN_HEAD
order by  xx.Shift desc,xx.Qty "
                                            Dim dtDetail As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                            If dtDetail IsNot Nothing AndAlso dtDetail.Rows.Count > 0 Then
                                                For indx As Integer = 0 To dtDetail.Rows.Count - 1
                                                    Dim Qty As Decimal = clsCommon.myCDecimal(dtDetail.Rows(indx)("Qty"))
                                                    If (clsCommon.myCdbl(drDCS("DiffQty"))) <> 0 Then
                                                        Qty = clsCommon.myCDecimal(dtDetail.Rows(indx)("Qty")) - clsCommon.myCDecimal(drDCS("DiffQty"))
                                                        If Qty < 0 Then
                                                            Qty = clsCommon.myCDecimal(dtDetail.Rows(indx)("Qty"))
                                                        End If
                                                    End If
                                                    Dim FATKG As Decimal = (clsCommon.myCDecimal(dtDetail.Rows(indx)("FATKG")) - clsCommon.myCDecimal(drDCS("DiffFATKG")))
                                                    Dim SNFKG As Decimal = (clsCommon.myCDecimal(dtDetail.Rows(indx)("SNFKG")) - clsCommon.myCDecimal(drDCS("DiffSNFKG")))
                                                    If FATKG < 0 OrElse SNFKG < 0 Then
                                                        Continue For
                                                    End If
                                                    'Dim FAT As Decimal = Math.Round(clsCommon.myCDivide((100 * FATKG), Qty), 1, MidpointRounding.AwayFromZero)
                                                    'Dim SNF As Decimal = Math.Round(clsCommon.myCDivide((100 * SNFKG), Qty), settSNFDecimalPlace, MidpointRounding.AwayFromZero)
                                                    Dim ROIncreaseAfter As Integer = 5
                                                    Dim FAT As Decimal = clsCommon.myRoundOFF(clsCommon.myCDivide((100 * FATKG), Qty), 1, ROIncreaseAfter)
                                                    Dim SNF As Decimal = clsCommon.myRoundOFF(clsCommon.myCDivide((100 * SNFKG), Qty), settSNFDecimalPlace, ROIncreaseAfter)

                                                    If settMaxFATPerLimit > 0 Then
                                                        If FAT > settMaxFATPerLimit Then
                                                            FAT = settMaxFATPerLimit
                                                        End If
                                                    End If
                                                    FATKG = Math.Round((Qty * FAT / 100), 3, MidpointRounding.AwayFromZero)
                                                    If settMaxSNFPerLimit > 0 AndAlso Not isPickCLRInsteadOfSNF Then
                                                        If SNF > settMaxSNFPerLimit Then
                                                            SNF = settMaxSNFPerLimit
                                                        End If
                                                    End If
                                                    SNFKG = Math.Round((Qty * SNF / 100), 3, MidpointRounding.AwayFromZero)
                                                    Dim strRejectType As String = clsCommon.myCstr(dtDetail.Rows(indx)("Milk_Type"))
                                                    Try
                                                        coll = New Hashtable()
                                                        clsCommon.AddColumnsForChange(coll, "OwnDCS_DOC_CODE", clsCommon.myCstr(dtDetail.Rows(indx)("DOC_CODE")))
                                                        clsCommon.AddColumnsForChange(coll, "OwnDCS_Qty", Qty)
                                                        clsCommon.AddColumnsForChange(coll, "OwnDCS_FAT_PER", FAT)
                                                        clsCommon.AddColumnsForChange(coll, "OwnDCS_SNF_PER", SNF)
                                                        clsCommon.AddColumnsForChange(coll, "OwnDCS_FAT_KG", FATKG)
                                                        clsCommon.AddColumnsForChange(coll, "OwnDCS_SNF_KG", SNFKG)
                                                        If clsCommon.CompairString(lblCAPUOM.Text, "KG") = CompairStringResult.Equal Then
                                                            dblKGQty = Qty
                                                            clsCommon.AddColumnsForChange(coll, "OwnDCS_ACC_Qty", dblKGQty)
                                                            clsCommon.AddColumnsForChange(coll, "OwnDCS_ACC_Qty_LTR", Qty * conv_fac)
                                                        Else
                                                            dblKGQty = Qty * conv_fac
                                                            clsCommon.AddColumnsForChange(coll, "OwnDCS_ACC_Qty_LTR", Qty)
                                                            clsCommon.AddColumnsForChange(coll, "OwnDCS_ACC_Qty", dblKGQty)
                                                        End If
                                                        clr = clsEkoPro.getClrOnCalculation(FAT, SNF, corrFactor)
                                                        clsCommon.AddColumnsForChange(coll, "OwnDCS_CLR", clr)
                                                        strPriceCode = ""
                                                        Rate = clsEkoPro.getRateAndPriceCodeFromUploaderShiftWise(Qty, strPriceCode, FAT, SNF, "", strOwnBMC, IIf(clsCommon.myCstr(cboCAPShift.SelectedValue).Contains("M"), "M", "E"), txtCAPShiftDate.Value, trans, clsCommon.myCstr(cboCAPMilkType.SelectedValue), 0, 0)
                                                        clsCommon.AddColumnsForChange(coll, "OwnDCS_Price_Code", strPriceCode)
                                                        clsCommon.AddColumnsForChange(coll, "OwnDCS_RATE", Rate)
                                                        Dim OwnDCSAmt As Decimal = Math.Round(Rate * Qty, 2, MidpointRounding.AwayFromZero)
                                                        clsCommon.AddColumnsForChange(coll, "OwnDCS_AMOUNT", OwnDCSAmt)

                                                        qry = "select top 1 AMOUNT from (
select TSPL_MILK_SRN_HEAD.DOC_DATE,Qty,FAT_PER,SNF_PER,RATE, AMOUNT,1 as RI from TSPL_MILK_SRN_DETAIL 
left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE
where TSPL_MILK_SRN_DETAIL.DOC_CODE='" + clsCommon.myCstr(dtDetail.Rows(indx)("DOC_CODE")) + "'
union all
select TSPL_MILK_SRN_CORRECTION_AFTER_PROCESS.Created_Date, OwnDCS_Qty,OwnDCS_FAT_PER,OwnDCS_SNF_PER, OwnDCS_RATE, OwnDCS_AMOUNT,2 as RI
from TSPL_MILK_SRN_CORRECTION_AFTER_PROCESS where  OwnDCS_DOC_CODE='" + clsCommon.myCstr(dtDetail.Rows(indx)("DOC_CODE")) + "'
union all
select TSPL_MILK_SRN_CORRECTION_AFTER_PROCESS.Created_Date, Qty, FAT_PER,SNF_PER, RATE, AMOUNT,3 as RI from TSPL_MILK_SRN_CORRECTION_AFTER_PROCESS where DOC_CODE='" + clsCommon.myCstr(dtDetail.Rows(indx)("DOC_CODE")) + "'
) xx order by DOC_DATE desc"
                                                        dclDRCROwnDCS = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue(qry, trans))
                                                        dclDRCROwnDCS = OwnDCSAmt - dclDRCROwnDCS
                                                        clsCommon.AddColumnsForChange(coll, "OwnDCS_DRCR_Amt", dclDRCROwnDCS)
                                                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_SRN_CORRECTION_AFTER_PROCESS", OMInsertOrUpdate.Update, "DOC_CODE='" + lblCAPSRNNo.Text + "'", trans)
                                                        Exit For
                                                    Catch ex As Exception
                                                        Throw New Exception(ex.Message)
                                                    End Try
                                                Next
                                            End If
                                        End If
                                    End If
                                Next
                            End If
                        End If

                        If DCSChangesDrNoteAmount > 0 Then
                            Dim objVendorInvHead As New clsVedorInvoiceHead()
                            objVendorInvHead.isDeduction = 1
                            objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(servdate, "dd/MMM/yyyy")
                            qry = "select vsp_code from tspl_Vlc_master_head where vlc_code='" + clsCommon.myCstr(txtCAPVLC.Tag) + "' "
                            objVendorInvHead.Vendor_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                            objVendorInvHead.Vendor_Name = clsVendorMaster.GetName(objVendorInvHead.Vendor_Code, trans)
                            objVendorInvHead.Vendor_Invoice_No = "" ''No Need to send vendor invoice no because it is of debit note type
                            objVendorInvHead.Invoice_Type = "AP"
                            objVendorInvHead.Vendor_Invoice_Date = objVendorInvHead.Invoice_Entry_Date
                            objVendorInvHead.loc_code = clsLocation.GetSegmentCode(txtCAPMCC.Value, trans) 'obj.MCC_CODE
                            objVendorInvHead.Description = "AP Debit Note Against Correction After Process DCS Change "
                            objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
                            If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                                Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
                            End If
                            objVendorInvHead.Document_Type = "D" ''For Purchase Invoice Type
                            objVendorInvHead.RefDocType = "CAP-MSN-CDCS"
                            objVendorInvHead.RefDocNo = lblCAPSRNNo.Text
                            objVendorInvHead.On_Hold = False
                            objVendorInvHead.Due_Date = objVendorInvHead.Invoice_Entry_Date
                            dt = clsDBFuncationality.GetDataTable("select Acct_Set_Code,Payable_Account,Discount_Account,Deduction_ACCOUNT from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
                            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                                objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, txtCAPMCC.Value, trans)
                                If clsCommon.myCDecimal(objVendorInvHead.Discount_Amount) > 0 Then
                                    objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                                    objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, txtCAPMCC.Value, trans)
                                End If
                            End If
                            If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                                Throw New Exception("Please set the vendor payable Account")
                            End If
                            objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
                            Dim ii As Integer = 0
                            Dim isFirstTime As Boolean = True
                            objVendorInvHead.Total_Landed_Amt = 0
                            objVendorInvHead.ArrAssetEMI = New List(Of clsAPInvoiceAssetEMIDetails)()
                            If True Then
                                ''Set AP Invvoice Detail Table
                                Dim dtDed As DataTable = clsDBFuncationality.GetDataTable("select code,GL_Account_Code from TSPL_DEDUCTION_MASTER  where Is_Correction_After_Process_DR_Note=1", trans)
                                If dtDed Is Nothing OrElse dtDed.Rows.Count <= 0 Then
                                    Throw New Exception("Please make Deduction Correction After Process Dr Note")
                                End If
                                If clsCommon.myLen(clsCommon.myCstr(dtDed.Rows(0)("GL_Account_Code"))) <= 0 Then
                                    Throw New Exception("Please set GL Account for deduction [" + clsCommon.myCstr(dtDed.Rows(0)("code")) + "]")
                                End If

                                ii = ii + 1
                                Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                                objVendorInvDetail.Detail_Line_No = ii
                                objVendorInvDetail.DeductionCode = clsCommon.myCstr(dtDed.Rows(0)("code"))
                                objVendorInvDetail.DeductionDesc = ClsDeductionMaster.GetName(objVendorInvDetail.DeductionCode, trans)
                                objVendorInvDetail.GL_Account_Code = clsCommon.myCstr(dtDed.Rows(0)("GL_Account_Code"))
                                objVendorInvDetail.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvDetail.GL_Account_Code, txtCAPMCC.Value, trans)
                                objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(objVendorInvDetail.GL_Account_Code, trans)


                                objVendorInvDetail.Amount = DCSChangesDrNoteAmount
                                objVendorInvDetail.Discount_Per = 0
                                objVendorInvDetail.Discount = 0
                                objVendorInvDetail.Amount_less_Discount = DCSChangesDrNoteAmount
                                objVendorInvDetail.Total_Tax = 0
                                objVendorInvDetail.Total_Amount = DCSChangesDrNoteAmount
                                objVendorInvDetail.Landed_Amount = DCSChangesDrNoteAmount
                                ''End of Set AP Invvoice Detail Table
                                If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                                    objVendorInvHead.Arr.Add(objVendorInvDetail)
                                End If

                                ''Set AP Invvoice Header Table
                                objVendorInvHead.Total_Landed_Amt += DCSChangesDrNoteAmount
                                objVendorInvHead.Discount_Base += DCSChangesDrNoteAmount
                                objVendorInvHead.Discount_Amount += 0
                                objVendorInvHead.Amount_Less_Discount += DCSChangesDrNoteAmount
                                objVendorInvHead.Document_Total += DCSChangesDrNoteAmount
                                objVendorInvHead.Balance_Amt += DCSChangesDrNoteAmount
                                ''End of Set AP Invvoice Header Table

                                objVendorInvHead.Empty_Amount = 0 'obj.Tot_Empty_Amount
                                If objVendorInvHead.Empty_Amount > 0 Then
                                    If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                                        Throw New Exception("Please set Inventory Control Empties")
                                    End If
                                    objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
                                End If
                            End If
                            If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                                Throw New Exception("No GL Account Found For AP Invoice")
                            End If
                            objVendorInvHead.ApplicableFrom = objVendorInvHead.Invoice_Entry_Date
                            'objVendorInvHead.Main_VSP_Milk_AP_Invoice_No = clsVedorInvoiceHead.GetMainVSPMilkAPInvoiceNo(ToDate, objVendorInvHead.Vendor_Code, trans)
                            objVendorInvHead.SaveData(objVendorInvHead, True, trans)
                            clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans)
                        End If

                        If dcsDRCR_Amt > 0 Then
                            Dim objVendorInvHead As New clsVedorInvoiceHead()
                            'objVendorInvHead.isDeduction = 1
                            objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(servdate, "dd/MMM/yyyy")
                            qry = "select vsp_code from tspl_Vlc_master_head where vlc_code='" + clsCommon.myCstr(TxtCAPDCSCode.Tag) + "' "
                            objVendorInvHead.Vendor_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                            objVendorInvHead.Vendor_Name = clsVendorMaster.GetName(objVendorInvHead.Vendor_Code, trans)
                            objVendorInvHead.Vendor_Invoice_No = "" ''No Need to send vendor invoice no because it is of debit note type
                            objVendorInvHead.Invoice_Type = "AP"
                            objVendorInvHead.Vendor_Invoice_Date = objVendorInvHead.Invoice_Entry_Date
                            objVendorInvHead.loc_code = clsLocation.GetSegmentCode(txtCAPMCC.Value, trans) 'obj.MCC_CODE
                            objVendorInvHead.Description = "AP Credit Note Against Correction After Process "
                            objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
                            If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                                Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
                            End If
                            objVendorInvHead.Document_Type = "C" ''For Purchase Invoice Type
                            objVendorInvHead.RefDocType = "CAP-MSN"
                            objVendorInvHead.RefDocNo = lblCAPSRNNo.Text
                            objVendorInvHead.On_Hold = False
                            objVendorInvHead.Due_Date = objVendorInvHead.Invoice_Entry_Date
                            dt = clsDBFuncationality.GetDataTable("select Acct_Set_Code,Payable_Account,Discount_Account,Deduction_ACCOUNT from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
                            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                                objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, txtCAPMCC.Value, trans)
                                If clsCommon.myCDecimal(objVendorInvHead.Discount_Amount) > 0 Then
                                    objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                                    objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, txtCAPMCC.Value, trans)
                                End If
                            End If
                            If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                                Throw New Exception("Please set the vendor payable Account")
                            End If
                            objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
                            Dim ii As Integer = 0
                            Dim isFirstTime As Boolean = True
                            objVendorInvHead.Total_Landed_Amt = 0
                            objVendorInvHead.ArrAssetEMI = New List(Of clsAPInvoiceAssetEMIDetails)()
                            If True Then
                                ''Set AP Invvoice Detail Table
                                Dim dtDed As DataTable = clsDBFuncationality.GetDataTable("select code,GL_Account_Code from TSPL_DEDUCTION_MASTER  where Is_Correction_After_Process_CR_Note=1", trans)
                                If dtDed Is Nothing OrElse dtDed.Rows.Count <= 0 Then
                                    Throw New Exception("Please make Deduction of Own BMC Milk Reject Type [ " + clsCommon.myCstr(dr("Milk_Type")) + " ]")
                                End If
                                If clsCommon.myLen(clsCommon.myCstr(dtDed.Rows(0)("GL_Account_Code"))) <= 0 Then
                                    Throw New Exception("Please set GL Account for deduction [" + clsCommon.myCstr(dtDed.Rows(0)("code")) + "]")
                                End If

                                ii = ii + 1
                                Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                                objVendorInvDetail.Detail_Line_No = ii
                                objVendorInvDetail.DeductionCode = clsCommon.myCstr(dtDed.Rows(0)("code"))
                                objVendorInvDetail.DeductionDesc = ClsDeductionMaster.GetName(objVendorInvDetail.DeductionCode, trans)
                                objVendorInvDetail.GL_Account_Code = clsCommon.myCstr(dtDed.Rows(0)("GL_Account_Code"))
                                objVendorInvDetail.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvDetail.GL_Account_Code, txtCAPMCC.Value, trans)
                                objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(objVendorInvDetail.GL_Account_Code, trans)
                                objVendorInvDetail.Amount = dcsDRCR_Amt
                                objVendorInvDetail.Discount_Per = 0
                                objVendorInvDetail.Discount = 0
                                objVendorInvDetail.Amount_less_Discount = dcsDRCR_Amt
                                objVendorInvDetail.Total_Tax = 0
                                objVendorInvDetail.Total_Amount = dcsDRCR_Amt
                                objVendorInvDetail.Landed_Amount = dcsDRCR_Amt
                                ''End of Set AP Invvoice Detail Table
                                If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                                    objVendorInvHead.Arr.Add(objVendorInvDetail)
                                End If

                                ''Set AP Invvoice Header Table
                                objVendorInvHead.Total_Landed_Amt += dcsDRCR_Amt
                                objVendorInvHead.Discount_Base += dcsDRCR_Amt
                                objVendorInvHead.Discount_Amount += 0
                                objVendorInvHead.Amount_Less_Discount += dcsDRCR_Amt
                                objVendorInvHead.Document_Total += dcsDRCR_Amt
                                objVendorInvHead.Balance_Amt += dcsDRCR_Amt
                                ''End of Set AP Invvoice Header Table
                                objVendorInvHead.Empty_Amount = 0 'obj.Tot_Empty_Amount
                                If objVendorInvHead.Empty_Amount > 0 Then
                                    If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                                        Throw New Exception("Please set Inventory Control Empties")
                                    End If
                                    objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
                                End If
                            End If
                            If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                                Throw New Exception("No GL Account Found For AP Invoice")
                            End If
                            objVendorInvHead.ApplicableFrom = objVendorInvHead.Invoice_Entry_Date
                            'objVendorInvHead.Main_VSP_Milk_AP_Invoice_No = clsVedorInvoiceHead.GetMainVSPMilkAPInvoiceNo(ToDate, objVendorInvHead.Vendor_Code, trans)
                            objVendorInvHead.SaveData(objVendorInvHead, True, trans)
                            clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans)

                        ElseIf dcsDRCR_Amt < 0 Then
                            dcsDRCR_Amt = Math.Abs(dcsDRCR_Amt)

                            Dim objVendorInvHead As New clsVedorInvoiceHead()
                            objVendorInvHead.isDeduction = 1
                            objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(servdate, "dd/MMM/yyyy")
                            qry = "select vsp_code from tspl_Vlc_master_head where vlc_code='" + clsCommon.myCstr(TxtCAPDCSCode.Tag) + "' "
                            objVendorInvHead.Vendor_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                            objVendorInvHead.Vendor_Name = clsVendorMaster.GetName(objVendorInvHead.Vendor_Code, trans)
                            objVendorInvHead.Vendor_Invoice_No = "" ''No Need to send vendor invoice no because it is of debit note type
                            objVendorInvHead.Invoice_Type = "AP"
                            objVendorInvHead.Vendor_Invoice_Date = objVendorInvHead.Invoice_Entry_Date
                            objVendorInvHead.loc_code = clsLocation.GetSegmentCode(txtCAPMCC.Value, trans) 'obj.MCC_CODE
                            objVendorInvHead.Description = "AP Debit Note Against Correction After Process "
                            objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
                            If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                                Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
                            End If
                            objVendorInvHead.Document_Type = "D" ''For Purchase Invoice Type
                            objVendorInvHead.RefDocType = "CAP-MSN"
                            objVendorInvHead.RefDocNo = lblCAPSRNNo.Text
                            objVendorInvHead.On_Hold = False
                            objVendorInvHead.Due_Date = objVendorInvHead.Invoice_Entry_Date
                            dt = clsDBFuncationality.GetDataTable("select Acct_Set_Code,Payable_Account,Discount_Account,Deduction_ACCOUNT from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
                            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                                objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, txtCAPMCC.Value, trans)
                                If clsCommon.myCDecimal(objVendorInvHead.Discount_Amount) > 0 Then
                                    objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                                    objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, txtCAPMCC.Value, trans)
                                End If
                            End If
                            If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                                Throw New Exception("Please set the vendor payable Account")
                            End If
                            objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
                            Dim ii As Integer = 0
                            Dim isFirstTime As Boolean = True
                            objVendorInvHead.Total_Landed_Amt = 0
                            objVendorInvHead.ArrAssetEMI = New List(Of clsAPInvoiceAssetEMIDetails)()
                            If True Then
                                ''Set AP Invvoice Detail Table
                                Dim dtDed As DataTable = clsDBFuncationality.GetDataTable("select code,GL_Account_Code from TSPL_DEDUCTION_MASTER  where Is_Correction_After_Process_DR_Note=1", trans)
                                If dtDed Is Nothing OrElse dtDed.Rows.Count <= 0 Then
                                    Throw New Exception("Please make Deduction Correction After Process Dr Note")
                                End If
                                If clsCommon.myLen(clsCommon.myCstr(dtDed.Rows(0)("GL_Account_Code"))) <= 0 Then
                                    Throw New Exception("Please set GL Account for deduction [" + clsCommon.myCstr(dtDed.Rows(0)("code")) + "]")
                                End If

                                ii = ii + 1
                                Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                                objVendorInvDetail.Detail_Line_No = ii
                                objVendorInvDetail.DeductionCode = clsCommon.myCstr(dtDed.Rows(0)("code"))
                                objVendorInvDetail.DeductionDesc = ClsDeductionMaster.GetName(objVendorInvDetail.DeductionCode, trans)
                                objVendorInvDetail.GL_Account_Code = clsCommon.myCstr(dtDed.Rows(0)("GL_Account_Code"))
                                objVendorInvDetail.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvDetail.GL_Account_Code, txtCAPMCC.Value, trans)
                                objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(objVendorInvDetail.GL_Account_Code, trans)


                                objVendorInvDetail.Amount = dcsDRCR_Amt
                                objVendorInvDetail.Discount_Per = 0
                                objVendorInvDetail.Discount = 0
                                objVendorInvDetail.Amount_less_Discount = dcsDRCR_Amt
                                objVendorInvDetail.Total_Tax = 0
                                objVendorInvDetail.Total_Amount = dcsDRCR_Amt
                                objVendorInvDetail.Landed_Amount = dcsDRCR_Amt
                                ''End of Set AP Invvoice Detail Table
                                If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                                    objVendorInvHead.Arr.Add(objVendorInvDetail)
                                End If

                                ''Set AP Invvoice Header Table
                                objVendorInvHead.Total_Landed_Amt += dcsDRCR_Amt
                                objVendorInvHead.Discount_Base += dcsDRCR_Amt
                                objVendorInvHead.Discount_Amount += 0
                                objVendorInvHead.Amount_Less_Discount += dcsDRCR_Amt
                                objVendorInvHead.Document_Total += dcsDRCR_Amt
                                objVendorInvHead.Balance_Amt += dcsDRCR_Amt
                                ''End of Set AP Invvoice Header Table

                                objVendorInvHead.Empty_Amount = 0 'obj.Tot_Empty_Amount
                                If objVendorInvHead.Empty_Amount > 0 Then
                                    If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                                        Throw New Exception("Please set Inventory Control Empties")
                                    End If
                                    objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
                                End If
                            End If
                            If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                                Throw New Exception("No GL Account Found For AP Invoice")
                            End If
                            objVendorInvHead.ApplicableFrom = objVendorInvHead.Invoice_Entry_Date
                            'objVendorInvHead.Main_VSP_Milk_AP_Invoice_No = clsVedorInvoiceHead.GetMainVSPMilkAPInvoiceNo(ToDate, objVendorInvHead.Vendor_Code, trans)
                            objVendorInvHead.SaveData(objVendorInvHead, True, trans)
                            clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans)
                        End If

                        If dclDRCROwnDCS <> 0 Then
                            Dim ii As Integer = 0
                            If dclDRCROwnDCS > 0 Then
                                dclDRCROwnDCS = dclDRCROwnDCS

                                Dim objVendorInvHead As New clsVedorInvoiceHead()
                                'objVendorInvHead.isDeduction = 1
                                objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(servdate, "dd/MMM/yyyy")
                                qry = "select vsp_code from tspl_Vlc_master_head where vlc_code='" + strOwnBMC + "' "
                                objVendorInvHead.Vendor_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                                objVendorInvHead.Vendor_Name = clsVendorMaster.GetName(objVendorInvHead.Vendor_Code, trans)
                                objVendorInvHead.Vendor_Invoice_No = "" ''No Need to send vendor invoice no because it is of debit note type
                                objVendorInvHead.Invoice_Type = "AP"
                                objVendorInvHead.Vendor_Invoice_Date = objVendorInvHead.Invoice_Entry_Date
                                objVendorInvHead.loc_code = clsLocation.GetSegmentCode(txtCAPMCC.Value, trans) 'obj.MCC_CODE
                                objVendorInvHead.Description = "AP Credit Note of OWN DCS Against Correction After Process "
                                objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
                                If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                                    Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
                                End If
                                objVendorInvHead.Document_Type = "C" ''For Purchase Invoice Type
                                objVendorInvHead.RefDocType = "CAP-OMSN"
                                objVendorInvHead.RefDocNo = lblCAPSRNNo.Text
                                objVendorInvHead.On_Hold = False
                                objVendorInvHead.Due_Date = objVendorInvHead.Invoice_Entry_Date
                                dt = clsDBFuncationality.GetDataTable("select Acct_Set_Code,Payable_Account,Discount_Account,Deduction_ACCOUNT from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
                                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                    objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                                    objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, txtCAPMCC.Value, trans)
                                    If clsCommon.myCDecimal(objVendorInvHead.Discount_Amount) > 0 Then
                                        objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                                        objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, txtCAPMCC.Value, trans)
                                    End If
                                End If
                                If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                                    Throw New Exception("Please set the vendor payable Account")
                                End If
                                objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
                                ii = 0
                                objVendorInvHead.Total_Landed_Amt = 0
                                objVendorInvHead.ArrAssetEMI = New List(Of clsAPInvoiceAssetEMIDetails)()
                                If True Then
                                    ''Set AP Invvoice Detail Table
                                    Dim dtDed As DataTable = clsDBFuncationality.GetDataTable("select code,GL_Account_Code from TSPL_DEDUCTION_MASTER  where Is_Correction_After_Process_CR_Note=1", trans)
                                    If dtDed Is Nothing OrElse dtDed.Rows.Count <= 0 Then
                                        Throw New Exception("Please make Deduction of Own BMC Milk Reject Type [ " + clsCommon.myCstr(dr("Milk_Type")) + " ]")
                                    End If
                                    If clsCommon.myLen(clsCommon.myCstr(dtDed.Rows(0)("GL_Account_Code"))) <= 0 Then
                                        Throw New Exception("Please set GL Account for deduction [" + clsCommon.myCstr(dtDed.Rows(0)("code")) + "]")
                                    End If

                                    ii = ii + 1
                                    Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                                    objVendorInvDetail.Detail_Line_No = ii
                                    objVendorInvDetail.DeductionCode = clsCommon.myCstr(dtDed.Rows(0)("code"))
                                    objVendorInvDetail.DeductionDesc = ClsDeductionMaster.GetName(objVendorInvDetail.DeductionCode, trans)
                                    objVendorInvDetail.GL_Account_Code = clsCommon.myCstr(dtDed.Rows(0)("GL_Account_Code"))
                                    objVendorInvDetail.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvDetail.GL_Account_Code, txtCAPMCC.Value, trans)
                                    objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(objVendorInvDetail.GL_Account_Code, trans)
                                    objVendorInvDetail.Amount = dclDRCROwnDCS
                                    objVendorInvDetail.Discount_Per = 0
                                    objVendorInvDetail.Discount = 0
                                    objVendorInvDetail.Amount_less_Discount = dclDRCROwnDCS
                                    objVendorInvDetail.Total_Tax = 0
                                    objVendorInvDetail.Total_Amount = dclDRCROwnDCS
                                    objVendorInvDetail.Landed_Amount = dclDRCROwnDCS
                                    ''End of Set AP Invvoice Detail Table
                                    If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                                        objVendorInvHead.Arr.Add(objVendorInvDetail)
                                    End If

                                    ''Set AP Invvoice Header Table
                                    objVendorInvHead.Total_Landed_Amt += dclDRCROwnDCS
                                    objVendorInvHead.Discount_Base += dclDRCROwnDCS
                                    objVendorInvHead.Discount_Amount += 0
                                    objVendorInvHead.Amount_Less_Discount += dclDRCROwnDCS
                                    objVendorInvHead.Document_Total += dclDRCROwnDCS
                                    objVendorInvHead.Balance_Amt += dclDRCROwnDCS
                                    ''End of Set AP Invvoice Header Table
                                    objVendorInvHead.Empty_Amount = 0 'obj.Tot_Empty_Amount
                                    If objVendorInvHead.Empty_Amount > 0 Then
                                        If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                                            Throw New Exception("Please set Inventory Control Empties")
                                        End If
                                        objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
                                    End If
                                End If
                                If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                                    Throw New Exception("No GL Account Found For AP Invoice")
                                End If
                                objVendorInvHead.ApplicableFrom = objVendorInvHead.Invoice_Entry_Date
                                'objVendorInvHead.Main_VSP_Milk_AP_Invoice_No = clsVedorInvoiceHead.GetMainVSPMilkAPInvoiceNo(ToDate, objVendorInvHead.Vendor_Code, trans)
                                objVendorInvHead.SaveData(objVendorInvHead, True, trans)
                                clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans)
                            End If

                            If dclDRCROwnDCS < 0 Then
                                dclDRCROwnDCS = Math.Abs(dclDRCROwnDCS)
                                Dim objVendorInvHead As New clsVedorInvoiceHead()
                                objVendorInvHead.isDeduction = 1
                                objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(servdate, "dd/MMM/yyyy")
                                qry = "select vsp_code from tspl_Vlc_master_head where vlc_code='" + strOwnBMC + "' "
                                objVendorInvHead.Vendor_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                                objVendorInvHead.Vendor_Name = clsVendorMaster.GetName(objVendorInvHead.Vendor_Code, trans)
                                objVendorInvHead.Vendor_Invoice_No = "" ''No Need to send vendor invoice no because it is of debit note type
                                objVendorInvHead.Invoice_Type = "AP"
                                objVendorInvHead.Vendor_Invoice_Date = objVendorInvHead.Invoice_Entry_Date
                                objVendorInvHead.loc_code = clsLocation.GetSegmentCode(txtCAPMCC.Value, trans) 'obj.MCC_CODE
                                objVendorInvHead.Description = "AP Debit Note Against OWN DCS Correction After Process "
                                objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
                                If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                                    Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
                                End If
                                objVendorInvHead.Document_Type = "D" ''For Purchase Invoice Type
                                objVendorInvHead.RefDocType = "CAP-OMSN"
                                objVendorInvHead.RefDocNo = lblCAPSRNNo.Text
                                objVendorInvHead.On_Hold = False
                                objVendorInvHead.Due_Date = objVendorInvHead.Invoice_Entry_Date
                                dt = clsDBFuncationality.GetDataTable("select Acct_Set_Code,Payable_Account,Discount_Account,Deduction_ACCOUNT from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
                                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                    objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                                    objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, txtCAPMCC.Value, trans)
                                    If clsCommon.myCDecimal(objVendorInvHead.Discount_Amount) > 0 Then
                                        objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                                        objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, txtCAPMCC.Value, trans)
                                    End If
                                End If
                                If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                                    Throw New Exception("Please set the vendor payable Account")
                                End If
                                objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
                                ii = 0
                                objVendorInvHead.Total_Landed_Amt = 0
                                objVendorInvHead.ArrAssetEMI = New List(Of clsAPInvoiceAssetEMIDetails)()
                                If True Then
                                    ''Set AP Invvoice Detail Table
                                    Dim dtDed As DataTable = clsDBFuncationality.GetDataTable("select code,GL_Account_Code from TSPL_DEDUCTION_MASTER  where Is_Correction_After_Process_DR_Note=1", trans)
                                    If dtDed Is Nothing OrElse dtDed.Rows.Count <= 0 Then
                                        Throw New Exception("Please make Deduction Correction After Process Dr Note")
                                    End If
                                    If clsCommon.myLen(clsCommon.myCstr(dtDed.Rows(0)("GL_Account_Code"))) <= 0 Then
                                        Throw New Exception("Please set GL Account for deduction [" + clsCommon.myCstr(dtDed.Rows(0)("code")) + "]")
                                    End If

                                    ii = ii + 1
                                    Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                                    objVendorInvDetail.Detail_Line_No = ii
                                    objVendorInvDetail.DeductionCode = clsCommon.myCstr(dtDed.Rows(0)("code"))
                                    objVendorInvDetail.DeductionDesc = ClsDeductionMaster.GetName(objVendorInvDetail.DeductionCode, trans)
                                    objVendorInvDetail.GL_Account_Code = clsCommon.myCstr(dtDed.Rows(0)("GL_Account_Code"))
                                    objVendorInvDetail.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvDetail.GL_Account_Code, txtCAPMCC.Value, trans)
                                    objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(objVendorInvDetail.GL_Account_Code, trans)


                                    objVendorInvDetail.Amount = dclDRCROwnDCS
                                    objVendorInvDetail.Discount_Per = 0
                                    objVendorInvDetail.Discount = 0
                                    objVendorInvDetail.Amount_less_Discount = dclDRCROwnDCS
                                    objVendorInvDetail.Total_Tax = 0
                                    objVendorInvDetail.Total_Amount = dclDRCROwnDCS
                                    objVendorInvDetail.Landed_Amount = dclDRCROwnDCS
                                    ''End of Set AP Invvoice Detail Table
                                    If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                                        objVendorInvHead.Arr.Add(objVendorInvDetail)
                                    End If

                                    ''Set AP Invvoice Header Table
                                    objVendorInvHead.Total_Landed_Amt += dclDRCROwnDCS
                                    objVendorInvHead.Discount_Base += dclDRCROwnDCS
                                    objVendorInvHead.Discount_Amount += 0
                                    objVendorInvHead.Amount_Less_Discount += dclDRCROwnDCS
                                    objVendorInvHead.Document_Total += dclDRCROwnDCS
                                    objVendorInvHead.Balance_Amt += dclDRCROwnDCS
                                    ''End of Set AP Invvoice Header Table

                                    objVendorInvHead.Empty_Amount = 0 'obj.Tot_Empty_Amount
                                    If objVendorInvHead.Empty_Amount > 0 Then
                                        If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                                            Throw New Exception("Please set Inventory Control Empties")
                                        End If
                                        objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
                                    End If
                                End If
                                If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                                    Throw New Exception("No GL Account Found For AP Invoice")
                                End If
                                objVendorInvHead.ApplicableFrom = objVendorInvHead.Invoice_Entry_Date
                                'objVendorInvHead.Main_VSP_Milk_AP_Invoice_No = clsVedorInvoiceHead.GetMainVSPMilkAPInvoiceNo(ToDate, objVendorInvHead.Vendor_Code, trans)
                                objVendorInvHead.SaveData(objVendorInvHead, True, trans)
                                clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans)
                            End If
                        End If

                        trans.Commit()
                    Catch ex As Exception
                        trans.Rollback()
                        Throw New Exception(ex.Message)
                    End Try
                    clsCommon.MyMessageBoxShow(Me, "Data corrected sucessfully", Me.Text)
                    btnSave.Enabled = False
                End If


            Catch ex As Exception
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton10_Click(sender As Object, e As EventArgs) Handles RadButton10.Click
        lblCAPSRNNo.Text = Nothing
        txtCAPQty.Value = Nothing
        txtCAPQty.Tag = Nothing
        txtCAPFAT.Value = Nothing
        txtCAPFAT.Tag = Nothing
        txtCAPSNF.Value = Nothing
        txtCAPSNF.Tag = Nothing
        cboCAPMilkType.SelectedValue = Nothing
        cboCAPMilkType.Tag = Nothing
        cboCAPRejectType.SelectedValue = Nothing
        cboCAPRejectType.Tag = Nothing
        TxtCAPDCSCode.Value = Nothing
        TxtCAPDCSCode.Tag = Nothing
        lblCAPDCSName.Text = Nothing
        RadGroupBox9.Enabled = True
        RadGroupBox10.Enabled = False
        btnCAPSave.Enabled = True
        TxtCAPRemarks.Text = ""
    End Sub

    Private Sub TxtCAPDCSCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtCAPDCSCode._MYValidating
        vlcUploaderFinder(TxtCAPDCSCode, lblCAPDCSName, isButtonClicked)
    End Sub

    Private Sub txtRoute__MYValidating_1(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtRoute._MYValidating
        Try
            Dim qry As String = " select ROUTE_NO as Code,ROUTE_NAME as Name from  TSPL_BULK_ROUTE_MASTER "
            Dim whrCls As String = ""
            txtRoute.Value = clsCommon.ShowSelectForm("corRoutFnd", qry, "Code", whrCls, txtRoute.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtSATruckSheet__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtSATruckSheet._MYValidating
        Try
            Dim qry As String = "select * from (select DocumentNo,max(DocumentDate) as DocumentDate,max(BMCCode) as BMCCode,max(BMCUploaderNo) as BMCUploaderNo,max(BMC) as BMC,sum(Qty) as Qty,case when sum(Qty)=0 then 0.0 else cast( sum(FATKG)*100/sum(Qty) as decimal(18,2)) end as FAT
,case when sum(Qty)=0 then 0.0 else cast( sum(SNFKG)*100/sum(Qty) as decimal(18,2)) end as SNF
,sum(FATKG) as FATKG,sum(SNFKG) as SNFKG,max(Price_Code) as Price_Code,sum(AMOUNT) as AMOUNT,max(Suspense_VLC_Code) as Suspense_VLC_Code
 from (
select TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No as DocumentNo,TSPL_MILK_COLLECTION_DCS.Document_Date as DocumentDate
,TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code as BMCCode,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader as BMCUploaderNo,TSPL_MCC_MASTER.MCC_NAME as BMC
,TSPL_MILK_COLLECTION_DCS_DETAIL.Qty,TSPL_MILK_COLLECTION_DCS_DETAIL.FAT,TSPL_MILK_COLLECTION_DCS_DETAIL.SNF,TSPL_MILK_COLLECTION_DCS_DETAIL.FATKG,TSPL_MILK_COLLECTION_DCS_DETAIL.SNFKG
,COALESCE(TSPL_MILK_SRN_DETAIL_SUP.Price_Code,TSPL_MILK_SRN_DETAIL_UP.Price_Code) as Price_Code
,COALESCE(TSPL_MILK_SRN_DETAIL_SUP.AMOUNT,TSPL_MILK_SRN_DETAIL_UP.AMOUNT) as AMOUNT,TSPL_MILK_COLLECTION_DCS_DETAIL.VLC_Code as Suspense_VLC_Code
from TSPL_MILK_COLLECTION_DCS_DETAIL
left outer join TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
left outer join (select Document_No,min(Against_Milk_Collection_MCC_Detail)Against_Milk_Collection_MCC_Detail from TSPL_MILK_COLLECTION_DCS_MCC_DETAIL group by Document_No ) as TSPL_MILK_COLLECTION_DCS_MCC_DETAIL on TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No=TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
left outer join TSPL_MILK_COLLECTION_MCC_DETAIL on TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id=TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail
left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code
left outer join TSPL_MILK_SHIFT_UPLOADER_DETAIL on TSPL_MILK_SHIFT_UPLOADER_DETAIL.Against_Milk_Collection_DCS_Detail=TSPL_MILK_COLLECTION_DCS_DETAIL.PK_Id
left outer join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Against_Milk_Collection_DCS_Detail=TSPL_MILK_COLLECTION_DCS_DETAIL.PK_Id
left join TSPL_MILK_SRN_HEAD as TSPL_MILK_SRN_HEAD_UP on  TSPL_MILK_SRN_HEAD_UP.Against_Uploader_TR_No= TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No
left outer join TSPL_MILK_SRN_DETAIL as TSPL_MILK_SRN_DETAIL_UP on TSPL_MILK_SRN_DETAIL_UP.DOC_CODE=TSPL_MILK_SRN_HEAD_UP.DOC_CODE
left join TSPL_MILK_SRN_HEAD as TSPL_MILK_SRN_HEAD_SUP on  TSPL_MILK_SRN_HEAD_SUP.Against_Shift_Uploader_TR_No= TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No
left outer join TSPL_MILK_SRN_DETAIL as TSPL_MILK_SRN_DETAIL_SUP on TSPL_MILK_SRN_DETAIL_SUP.DOC_CODE=TSPL_MILK_SRN_HEAD_SUP.DOC_CODE
where  TSPL_MILK_COLLECTION_DCS.Status=1  and TSPL_MILK_COLLECTION_DCS_DETAIL.Suspence=1
) xx group by DocumentNo
) xxx "
            txtSATruckSheet.Value = clsCommon.ShowSelectForm("CorrSATrShet", qry, "DocumentNo", "", txtSATruckSheet.Value, "DocumentNo", isButtonClicked, "DocumentDate")
            If clsCommon.myLen(txtSATruckSheet.Value) > 0 Then
                qry += " where DocumentNo='" + txtSATruckSheet.Value + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    lblSADate.Text = clsCommon.GetPrintDate(dt.Rows(0)("DocumentDate"), "dd/MM/yyyy")

                    lblSABMCUploaderNo.Text = clsCommon.myCstr(dt.Rows(0)("BMCUploaderNo"))
                    lblSABMCCode.Text = clsCommon.myCstr(dt.Rows(0)("BMCCode"))
                    lblSABMCName.Text = clsCommon.myCstr(dt.Rows(0)("BMC"))


                    lblSABMCCode.Tag = clsCommon.myCstr(dt.Rows(0)("Suspense_VLC_Code"))

                    lblSAQty.Text = clsCommon.myCstr(dt.Rows(0)("Qty"))
                    'lblSAAdjustQty.Text = clsCommon.myCstr(dt.Rows(0)(""))
                    'lblSAPendingQty.Text = clsCommon.myCstr(dt.Rows(0)(""))

                    lblSAFATKg.Text = clsCommon.myCstr(dt.Rows(0)("FATKG"))
                    'lblSAAdjustFATKG.Text = clsCommon.myCstr(dt.Rows(0)(""))
                    'lblSAPendingFATKG.Text = clsCommon.myCstr(dt.Rows(0)(""))

                    lblSASNFKg.Text = clsCommon.myCstr(dt.Rows(0)("SNFKG"))
                    'lblSAAdjustSNFKG.Text = clsCommon.myCstr(dt.Rows(0)(""))
                    'lblSAPendingSNFKG.Text = clsCommon.myCstr(dt.Rows(0)(""))

                    lblSAAmount.Text = clsCommon.myCstr(dt.Rows(0)("AMOUNT"))
                    'lblSAAdjustAmount.Text = clsCommon.myCstr(dt.Rows(0)(""))
                    'lblSAPendingAmount.Text = clsCommon.myCstr(dt.Rows(0)(""))


                    lblSAFATPer.Text = clsCommon.myCstr(dt.Rows(0)("FAT"))
                    'lblSAAdjustFATPer.Text = clsCommon.myCstr(dt.Rows(0)(""))
                    'lblSAPendingFATPer.Text = clsCommon.myCstr(dt.Rows(0)(""))

                    lblSASNFPer.Text = clsCommon.myCstr(dt.Rows(0)("SNF"))
                    'lblSAAdjustSNFPer.Text = clsCommon.myCstr(dt.Rows(0)(""))
                    'lblSAPendingSNFPer.Text = clsCommon.myCstr(dt.Rows(0)(""))

                    qry = "select sum(Qty) as Qty,sum(FATKG) as FATKG,sum(SNFKG) as SNFKG,sum(Amount) as Amount From TSPL_MILK_COLLECTION_DCS_SUSPENSE_ADJUSTMENT
where  Document_No='" + txtSATruckSheet.Value + "'"
                    dt = clsDBFuncationality.GetDataTable(qry)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        lblSAAdjustQty.Tag = clsCommon.myCDecimal(dt.Rows(0)("Qty"))
                        lblSAAdjustFATKG.Tag = clsCommon.myCDecimal(dt.Rows(0)("FATKG"))
                        lblSAAdjustSNFKG.Tag = clsCommon.myCDecimal(dt.Rows(0)("SNFKG"))
                        lblSAAdjustAmount.Tag = clsCommon.myCDecimal(dt.Rows(0)("Amount"))
                    End If
                    UpdateAllTotal()
                End If
            End If
            LoadBlankGridSA()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub



    Sub LoadBlankGridSA()
        gvSA.Rows.Clear()
        gvSA.Columns.Clear()

        Dim repoNumBox As GridViewDecimalColumn = New GridViewDecimalColumn()


        Dim repoTextBox2 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTextBox2.FormatString = ""
        repoTextBox2.HeaderText = "DCS"
        repoTextBox2.Name = colSAVLCUploaderCode
        repoTextBox2.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoTextBox2.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTextBox2.Width = 150
        gvSA.MasterTemplate.Columns.Add(repoTextBox2)

        Dim repoTextBox As New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "DCS Code"
        repoTextBox.Name = colSAVLCCode
        repoTextBox.IsVisible = False
        repoTextBox.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTextBox.ReadOnly = True
        gvSA.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "DCS Name"
        repoTextBox.Name = colSAVLCName
        repoTextBox.Width = 200
        repoTextBox.IsVisible = True
        repoTextBox.ReadOnly = True
        gvSA.MasterTemplate.Columns.Add(repoTextBox)

        Dim repoCheckBox As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoCheckBox.HeaderText = "Own BMC"
        repoCheckBox.Name = colOwnDCS
        repoCheckBox.ReadOnly = True
        repoCheckBox.IsVisible = True
        repoCheckBox.Width = 50
        gvSA.MasterTemplate.Columns.Add(repoCheckBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = "{0:n3}"
        repoNumBox.HeaderText = "Qty"
        repoNumBox.Name = colSAQty
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 3
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoNumBox.IsVisible = True
        gvSA.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = "{0:n1}"
        repoNumBox.HeaderText = "Fat %"
        repoNumBox.Name = colSAFATPer
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.Maximum = 100
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 1
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoNumBox.IsVisible = True
        repoNumBox.ReadOnly = False
        gvSA.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = "{0:n1}"
        repoNumBox.HeaderText = "SNF %"
        repoNumBox.Name = colSASNFPer
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.Maximum = 100
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 1
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoNumBox.IsVisible = True
        repoNumBox.ReadOnly = False
        gvSA.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = "{0:n3}"
        repoNumBox.HeaderText = "Fat KG"
        repoNumBox.Name = colSAFATKG
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.Maximum = 9999
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 3
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoNumBox.IsVisible = True
        repoNumBox.ReadOnly = True
        gvSA.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = "{0:n3}"
        repoNumBox.HeaderText = "SNF KG"
        repoNumBox.Name = colSASNFKG
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.Maximum = 9999
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 3
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoNumBox.IsVisible = True
        repoNumBox.ReadOnly = True
        gvSA.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = "{0:n3}"
        repoNumBox.HeaderText = "Rate"
        repoNumBox.Name = colSARate
        repoNumBox.Width = 100
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 2
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoNumBox.IsVisible = True
        repoNumBox.ReadOnly = True
        gvSA.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = "{0:n2}"
        repoNumBox.HeaderText = "Amount"
        repoNumBox.Name = colSAAmount
        repoNumBox.Width = 100
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 2
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoNumBox.IsVisible = True
        repoNumBox.ReadOnly = True
        gvSA.MasterTemplate.Columns.Add(repoNumBox)

        gvSA.AllowAddNewRow = False
        gvSA.ShowGroupPanel = False
        gvSA.AllowColumnReorder = False
        gvSA.AllowRowReorder = False
        gvSA.EnableSorting = False
        gvSA.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvSA.MasterTemplate.ShowRowHeaderColumn = False
        gvSA.TableElement.TableHeaderHeight = 40

        gvSA.AllowDeleteRow = True
        gvSA.Rows.AddNew()
    End Sub

    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Private Sub gvSA_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvSA.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gvSA.Columns(colSAVLCUploaderCode) Then
                        OpenVLCFinder(False)
                    ElseIf e.Column Is gvSA.Columns(colSAQty) OrElse e.Column Is gvSA.Columns(colSAFATPer) OrElse e.Column Is gvSA.Columns(colSASNFPer) Then
                        UpdateCurrentRow(gvSA.CurrentRow.Index)
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            isCellValueChangedOpen = False
        End Try
    End Sub
    Sub UpdateCurrentRow(ByVal ii As Integer)
        gvSA.Rows(ii).Cells(colSAFATKG).Value = Math.Round(clsCommon.myCDecimal(gvSA.Rows(ii).Cells(colSAQty).Value) * clsCommon.myCDecimal(gvSA.Rows(ii).Cells(colSAFATPer).Value) / 100, 3, MidpointRounding.ToEven)
        gvSA.Rows(ii).Cells(colSASNFKG).Value = Math.Round(clsCommon.myCDecimal(gvSA.Rows(ii).Cells(colSAQty).Value) * clsCommon.myCDecimal(gvSA.Rows(ii).Cells(colSASNFPer).Value) / 100, 3, MidpointRounding.ToEven)
        Dim strPriceCode As String = ""
        gvSA.Rows(ii).Cells(colSARate).Value = clsEkoPro.getRateAndPriceCodeFromUploaderShiftWise(clsCommon.myCdbl(gvSA.Rows(ii).Cells(colSAQty).Value), strPriceCode, clsCommon.myCdbl(gvSA.Rows(ii).Cells(colSAFATPer).Value), clsCommon.myCdbl(gvSA.Rows(ii).Cells(colSASNFPer).Value), lblMCCCode.Text, clsCommon.myCstr(gvSA.Rows(ii).Cells(colSAVLCCode).Value), "M", clsCommon.myCDate(lblSADate.Text), Nothing, "M")
        gvSA.Rows(ii).Cells(colSAAmount).Value = clsCommon.myCDecimal(gvSA.Rows(ii).Cells(colSAQty).Value) * clsCommon.myCDecimal(gvSA.Rows(ii).Cells(colSARate).Value)
        UpdateAllTotal()
    End Sub
    Private Sub UpdateAllTotal()
        Dim TotSAQty As Decimal = clsCommon.myCDecimal(lblSAAdjustQty.Tag)
        Dim TotSAFATKG As Decimal = clsCommon.myCDecimal(lblSAAdjustFATKG.Tag)
        Dim TotSASNFKG As Decimal = clsCommon.myCDecimal(lblSAAdjustSNFKG.Tag)
        Dim TotSAAmt As Decimal = clsCommon.myCDecimal(lblSAAdjustAmount.Tag)

        For ii As Integer = 0 To gvSA.Rows.Count - 1
            If clsCommon.myCDecimal(gvSA.Rows(ii).Cells(colSAQty).Value) > 0 Then
                TotSAQty += clsCommon.myCDecimal(gvSA.Rows(ii).Cells(colSAQty).Value)
                TotSAFATKG += clsCommon.myCDecimal(gvSA.Rows(ii).Cells(colSAFATKG).Value)
                TotSASNFKG += clsCommon.myCDecimal(gvSA.Rows(ii).Cells(colSASNFKG).Value)
                TotSAAmt += clsCommon.myCDecimal(gvSA.Rows(ii).Cells(colSAAmount).Value)
            End If
        Next

        lblSAAdjustQty.Text = clsCommon.myCstr(Math.Round(TotSAQty, 2, MidpointRounding.ToEven))
        lblSAAdjustFATKG.Text = clsCommon.myCstr(Math.Round((TotSAFATKG), 3, MidpointRounding.ToEven))
        lblSAAdjustSNFKG.Text = clsCommon.myCstr(Math.Round((TotSASNFKG), 3, MidpointRounding.ToEven))
        lblSAAdjustAmount.Text = clsCommon.myCstr(Math.Round(TotSAAmt, 2, MidpointRounding.ToEven))

        lblSAPendingQty.Text = clsCommon.myCstr(Math.Round((clsCommon.myCDecimal(lblSAQty.Text) - clsCommon.myCDecimal(lblSAAdjustQty.Text)), 2, MidpointRounding.ToEven))
        lblSAPendingFATKG.Text = clsCommon.myCstr(Math.Round((clsCommon.myCDecimal(lblSAFATKg.Text) - clsCommon.myCDecimal(lblSAAdjustFATKG.Text)), 3, MidpointRounding.ToEven))
        lblSAPendingSNFKG.Text = clsCommon.myCstr(Math.Round((clsCommon.myCDecimal(lblSASNFKg.Text) - clsCommon.myCDecimal(lblSAAdjustSNFKG.Text)), 3, MidpointRounding.ToEven))
        lblSAPendingAmount.Text = clsCommon.myCstr(Math.Round((clsCommon.myCDecimal(lblSAAmount.Text) - clsCommon.myCDecimal(lblSAAdjustAmount.Text)), 2, MidpointRounding.ToEven))

        lblSAAdjustFATPer.Text = clsCommon.myCstr(Math.Round(clsCommon.myCDivide(clsCommon.myCDecimal(lblSAAdjustFATKG.Text) * 100, clsCommon.myCDecimal(lblSAAdjustQty.Text)), 1, MidpointRounding.ToEven))
        lblSAAdjustSNFPer.Text = clsCommon.myCstr(Math.Round(clsCommon.myCDivide(clsCommon.myCDecimal(lblSAAdjustSNFKG.Text) * 100, clsCommon.myCDecimal(lblSAAdjustQty.Text)), 1, MidpointRounding.ToEven))

        lblSAPendingFATPer.Text = clsCommon.myCstr(Math.Round(clsCommon.myCDivide(clsCommon.myCDecimal(lblSAPendingFATKG.Text) * 100, clsCommon.myCDecimal(lblSAPendingQty.Text)), 1, MidpointRounding.ToEven))
        lblSAPendingSNFPer.Text = clsCommon.myCstr(Math.Round(clsCommon.myCDivide(clsCommon.myCDecimal(lblSAPendingSNFKG.Text) * 100, clsCommon.myCDecimal(lblSAPendingQty.Text)), 1, MidpointRounding.ToEven))
    End Sub
    Sub OpenVLCFinder(ByVal isButtonClick As Boolean)
        If clsCommon.myLen(lblSABMCCode.Text) <= 0 Then
            txtSATruckSheet.Focus()
            Throw New Exception("Please select Truck Sheet")
        End If

        Dim qry As String = "select TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [Uploader_Code],TSPL_VLC_MASTER_HEAD.VSP_Code,TSPL_VENDOR_MASTER.Vendor_Name as [VSP Name] " + Environment.NewLine +
        " from TSPL_VLC_MASTER_HEAD" + Environment.NewLine +
        " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code  " + Environment.NewLine +
        " left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC " + Environment.NewLine
        Dim whrCls As String = " isnull(TSPL_VLC_MASTER_HEAD.Active,0)=1 "
        If Not SettShowAllDCS Then
            whrCls += " and TSPL_VLC_MASTER_HEAD.MCC  ='" + lblSABMCCode.Text + "'"
        End If
        gvSA.CurrentRow.Cells(colSAVLCUploaderCode).Value = clsCommon.ShowSelectForm("CorrVuSA", qry, "Uploader_Code", whrCls, clsCommon.myCstr(gvSA.CurrentRow.Cells(colSAVLCUploaderCode).Value), "Uploader_Code", isButtonClick)

        qry = "select TSPL_VLC_MASTER_HEAD.VLC_Code,VLC_Name,Apply_Cow_Price,MCC,(case when isnull(TSPL_VLC_MASTER_HEAD.isOwnBMC,0)=1 and TSPL_VLC_MASTER_HEAD.MCCOwnBMC='" + clsCommon.myCstr(lblSABMCCode.Text) + "' then 1 else 0 end) as isOwnBMC from TSPL_VLC_MASTER_HEAD where TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader='" + clsCommon.myCstr(gvSA.CurrentRow.Cells(colSAVLCUploaderCode).Value) + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            gvSA.CurrentRow.Cells(colSAVLCCode).Value = clsCommon.myCstr(dt.Rows(0)("VLC_Code"))
            gvSA.CurrentRow.Cells(colSAVLCName).Value = clsCommon.myCstr(dt.Rows(0)("VLC_Name"))
            gvSA.CurrentRow.Cells(colOwnDCS).Value = clsCommon.myCBool(dt.Rows(0)("isOwnBMC"))
            If Not clsCommon.CompairString(lblSABMCCode.Text, clsCommon.myCstr(dt.Rows(0)("MCC"))) = CompairStringResult.Equal Then
                If clsCommon.MyMessageBoxShow(Me, "DCS does not belong to BMC [" + lblSABMCUploaderNo.Text + "]" + Environment.NewLine + "Do You Want To Continue? ", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
                    gvSA.Rows.Remove(gvSA.CurrentRow)
                End If
            End If
        End If
    End Sub
    Private Sub gv1_CellValidated(sender As Object, e As CellValidatedEventArgs) Handles gvSA.CellValidated
        Try
            SetGridFocus()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub SetGridFocus()
        If gvSA.CurrentCell IsNot Nothing Then
            If gvSA.CurrentCell.ColumnInfo.Name = colSAVLCUploaderCode Then
                gvSA.CurrentColumn = gvSA.Columns(colSAQty)
            ElseIf (gvSA.CurrentCell.ColumnInfo.Name = colSAQty) Then
                gvSA.CurrentColumn = gvSA.Columns(colSAFATPer)
            ElseIf (gvSA.CurrentCell.ColumnInfo.Name = colSAFATPer) Then
                gvSA.CurrentColumn = gvSA.Columns(colSASNFPer)
            ElseIf (gvSA.CurrentCell.ColumnInfo.Name = colSASNFPer) Then
                If gvSA.Rows.Count > gvSA.CurrentRow.Index + 1 Then
                    gvSA.CurrentRow = gvSA.Rows(gvSA.CurrentRow.Index + 1)
                    gvSA.CurrentColumn = gvSA.Columns(colSAVLCUploaderCode)
                End If
            End If
        End If
    End Sub
    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvSA.CurrentColumnChanged
        If gvSA.RowCount > 0 Then
            Dim intCurrRow As Integer = gvSA.CurrentRow.Index
            If intCurrRow = gvSA.Rows.Count - 1 Then
                gvSA.Rows.AddNew()
                gvSA.CurrentRow = gvSA.Rows(intCurrRow)
            End If
        End If
    End Sub
    Private Sub RadButton11_Click_1(sender As Object, e As EventArgs) Handles RadButton11.Click
        Try
            Dim Arr As New List(Of clsMilkCollectionDCSSuspenceAdjustment)
            For ii As Integer = 0 To gvSA.Rows.Count - 1
                If clsCommon.myLen(gvSA.Rows(ii).Cells(colSAVLCCode).Value) > 0 AndAlso clsCommon.myCDecimal(gvSA.Rows(ii).Cells(colSAQty).Value) > 0 AndAlso
                    clsCommon.myCDecimal(gvSA.Rows(ii).Cells(colSAFATPer).Value) > 0 AndAlso clsCommon.myCDecimal(gvSA.Rows(ii).Cells(colSASNFPer).Value) > 0 Then
                    Dim obj As New clsMilkCollectionDCSSuspenceAdjustment
                    obj.VLC_Code = clsCommon.myCstr(gvSA.Rows(ii).Cells(colSAVLCCode).Value)
                    obj.Qty = clsCommon.myCDecimal(gvSA.Rows(ii).Cells(colSAQty).Value)
                    obj.FAT = clsCommon.myCstr(gvSA.Rows(ii).Cells(colSAFATPer).Value)
                    obj.SNF = clsCommon.myCstr(gvSA.Rows(ii).Cells(colSASNFPer).Value)
                    obj.FATKG = clsCommon.myCstr(gvSA.Rows(ii).Cells(colSAFATKG).Value)
                    obj.SNFKG = clsCommon.myCstr(gvSA.Rows(ii).Cells(colSASNFKG).Value)
                    obj.Rate = clsCommon.myCstr(gvSA.Rows(ii).Cells(colSARate).Value)
                    obj.Amount = clsCommon.myCstr(gvSA.Rows(ii).Cells(colSAAmount).Value)
                    Arr.Add(obj)
                End If
            Next
            If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                If clsCommon.MyMessageBoxShow(Me, "Adjusting " & Arr.Count & " DCS against Suspense Entry." & Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, Telerik.WinControls.RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    clsMilkCollectionDCSSuspenceAdjustment.SaveData(txtSATruckSheet.Value, clsCommon.myCstr(lblSABMCCode.Tag), lblSABMCCode.Text, Arr)
                    clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)

                    txtSATruckSheet.Value = ""
                    lblSADate.Text = ""
                    lblSABMCUploaderNo.Text = ""
                    lblSABMCCode.Text = ""
                    lblSABMCName.Text = ""
                    lblSABMCCode.Tag = ""
                    lblSAQty.Text = ""
                    lblSAAdjustQty.Text = ""
                    lblSAPendingQty.Text = ""
                    lblSAFATKg.Text = ""
                    lblSAAdjustFATKG.Text = ""
                    lblSAPendingFATKG.Text = ""
                    lblSASNFKg.Text = ""
                    lblSAAdjustSNFKG.Text = ""
                    lblSAPendingSNFKG.Text = ""
                    lblSAAmount.Text = ""
                    lblSAAdjustAmount.Text = ""
                    lblSAPendingAmount.Text = ""
                    lblSAFATPer.Text = ""
                    lblSAAdjustFATPer.Text = ""
                    lblSAPendingFATPer.Text = ""
                    lblSASNFPer.Text = ""
                    lblSAAdjustSNFPer.Text = ""
                    lblSAPendingSNFPer.Text = ""
                    lblSAAdjustQty.Tag = ""
                    lblSAAdjustFATKG.Tag = ""
                    lblSAAdjustSNFKG.Tag = ""
                    lblSAAdjustAmount.Tag = ""
                    LoadBlankGridSA()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class