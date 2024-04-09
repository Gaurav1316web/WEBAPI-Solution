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
#End Region

    Private Sub frmMilkGateEntryIn_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim id As String = Form_ID
        Try
            If clsCommon.CompairString(Form_ID, clsUserMgtCode.MilkProcurementCorrection) = CompairStringResult.Equal Then
                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDP") = CompairStringResult.Equal Then
                    chkAddMissingSample.Visible = False
                    chkAdjustOwnBMCFATSNF.Visible = True
                    chkDeleteBMCCollection.Visible = True
                    txtFromShift.Enabled = False
                Else
                    chkAddMissingSample.Visible = True
                    chkAdjustOwnBMCFATSNF.Visible = False
                    chkDeleteBMCCollection.Visible = False
                    txtFromShift.Enabled = True
                End If
                SettMilkCollectionFATSNFType = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkCollectionFATSNFType, clsFixedParameterCode.MilkCollectionFATSNFType, Nothing))
                SettFATSNFNoDecimalMCC = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.FATSNFNoDecimalMCC, clsFixedParameterCode.FATSNFNoDecimalMCC, Nothing))
                SettShowAllMCC = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowAllMCC, clsFixedParameterCode.ShowAllMCC, Nothing))

                MultipleFinderFillAuto = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MultipleFinderFillAuto, clsFixedParameterCode.MultipleFinderFillAuto, Nothing)) = 1)

                RadPageView1.Pages("RadPageViewPage2").Item.Visibility = ElementVisibility.Collapsed
                SetUserMgmtNew()
                LoadMilkType()
                LoadMilkTypeBMC()
                LoadShiftWithBoth()
                LoadShift()
                MilkWeight_Setting = clsFixedParameter.GetData(clsFixedParameterType.Milk_Can_Weight_Ratio, clsFixedParameterCode.MilkSetting, Nothing)
                dclCorrectionFactor = clsFixedParameter.GetData(clsFixedParameterType.defaultCorrectionFactor, clsFixedParameterCode.MilkSetting, Nothing)
                settMaxReceiveSNFPer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MaxReceiveSNFPer, clsFixedParameterCode.MaxReceiveSNFPer, Nothing))
                settMaxFATPerLimit = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MaxFATPerLimit, clsFixedParameterCode.MaxFATPerLimit, Nothing))
                settMaxSNFPerLimit = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MaxSNFPerLimit, clsFixedParameterCode.MaxSNFPerLimit, Nothing))
                IsRoundOffPaiseAmount = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RoundOffPaiseAmount, clsFixedParameterCode.RoundOffPaiseAmount, Nothing)) = 1
                ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
                ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
                txtShiftDate.Value = clsCommon.GETSERVERDATE()
                txtBMCDate.Value = txtShiftDate.Value
                txtBMCTankerDate.Value = txtShiftDate.Value
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
                corrFactor = clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.defaultCorrectionFactor, clsFixedParameterCode.MilkSetting, Nothing))
                isPickCLRInsteadOfSNF = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProcuremntPickCLRInsteadOfSNF, clsFixedParameterCode.MilkProcuremntPickCLRInsteadOfSNF, Nothing)) > 0)
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
            End If
            If clsCommon.CompairString(MyBase.Form_ID, "MLK-PRO-COR") = CompairStringResult.Equal Then
                isCorrection = 1
            ElseIf clsCommon.CompairString(MyBase.Form_ID, "MLK-RE-TST") = CompairStringResult.Equal Then
                isCorrection = 2
            Else
                isCorrection = 0
            End If

            'RadPageView1.SelectedPage = RadPageViewPage1
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    'Private Sub CreateTable()
    '    Dim coll As Dictionary(Of String, String)
    '    coll = New Dictionary(Of String, String)
    '    coll.Add("PK_Id", "integer NOT NULL identity NOT FOR REPLICATION primary key")
    '    coll.Add("Document_No", "Varchar(30) not null references TSPL_MILK_COLLECTION_MCC(Document_No)")
    '    coll.Add("SNo", "Integer NULL")
    '    coll.Add("MCC_Code", "Varchar(30) not null references TSPL_MCC_MASTER(MCC_Code)")
    '    coll.Add("Milk_Type", "char(5) NOT NULL Default 'M'")
    '    coll.Add("Qty", "Decimal(18,2) null")
    '    coll.Add("FAT", "Decimal(18,2) null")
    '    coll.Add("SNF", "Decimal(18,2) null")
    '    coll.Add("FATKG", "Decimal(18,3) null")
    '    coll.Add("SNFKG", "Decimal(18,3) null")
    '    coll.Add("Gaze_Reading", "Decimal(18,1) null")
    '    coll.Add("Silo_Capacity", "integer null")
    '    coll.Add("Temp", "Decimal(18,2) null")
    '    coll.Add("Sample_No", "integer null")
    '    coll.Add("Gaze_Reading_Code", "Varchar(30) null REFERENCES TSPL_GAZE_READING(Code)")
    '    coll.Add("IsUpdatedFromCorrection", "Integer NOT NULL DEFAULT 0")
    '    coll.Add("Against_Multiple_Days", "integer NULL references TSPL_MILK_COLLECTION_MCC_MULTIPLE_DAYS_DETAIL(PK_Id)")
    '    coll.Add("REF_PK_ID_BMCDCS_TRIP", "integer NULL references TSPL_MILK_COLLECTION_BMCDCS_TRIP(PK_ID)")
    '    coll.Add("Machine_FAT", "Decimal(18,2) null")
    '    coll.Add("Machine_SNF", "Decimal(18,2) null")
    '    coll.Add("Retesting_FAT", "Decimal(18,2) null")
    '    coll.Add("Retesting_SNF", "Decimal(18,2) null")
    '    coll.Add("Retesting_CLR", "Decimal(18,2) null")
    '    coll.Add("Retesting_OR_Correction", "integer null")
    '    coll.Add("Correction_FAT", "Decimal(18,2) null")
    '    coll.Add("Correction_SNF", "Decimal(18,2) null")
    '    clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_MILK_COLLECTION_MCC_DETAIL", coll, Nothing, True, False, "TSPL_MILK_COLLECTION_MCC", "Document_No", "")
    'End Sub
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

    Sub LoadMilkTypeBMC()
        cboBMCCorrMilkType.DataSource = clsMilkReceiptMCC.GetReject(True)
        cboBMCCorrMilkType.ValueMember = "Code"
        cboBMCCorrMilkType.DisplayMember = "Name"
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
        'txtMCC.Value = clsMccMaster.getFinder("", txtMCC.Value, isButtonClicked)
        Dim qry As String = ""
        Dim arrLoc As String = ""
        Dim obj As New clsMCCCodes()
        obj = clsMCCCodes.GetData(True)
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
            arrLoc = obj.arrLocCodes
        End If

        qry = "select * from ( select tspl_mcc_master.MCC_Code as [Code] ,tspl_mcc_master.MCC_Type as [Mcc Type] ,tspl_mcc_master.MCC_NAME as [Mcc Name] ,tspl_mcc_master.Chilling_Vendor as [Chilling Vendor] ,tspl_mcc_master.Add1 as [Address1] ,tspl_mcc_master.Add2 as [Address2] ,tspl_mcc_master.Tehsil as [Tehsil] ,tspl_mcc_master.City_code as [City Code] ,tspl_mcc_master.State_Code as [State Code] ,tspl_mcc_master.Country_code as [Country Code] ,tspl_mcc_master.Pin_code as [Pin Code],tspl_mcc_master.Pan_No as [Pan No] ,tspl_mcc_master.Telphone as [Telphone] ,tspl_mcc_master.Email as [Email] ,tspl_mcc_master.Fax as [Fax] ,tspl_mcc_master.MCC_Area as [Mcc Area] ,tspl_mcc_master.Area_Of_Store as [Area Of Store] ,tspl_mcc_master.Area_Of_Office as [Area Of Office] ,tspl_mcc_master.Open_Area_For_tanker as [Open Area For Tanker] ,tspl_mcc_master.Area_Of_LAB as [Area Of Lab] ,tspl_mcc_master.No_Of_SILO as [No Of Silo] ,tspl_mcc_master.Total_Storage_capacity as [Total Storage Capacity] ,tspl_mcc_master.Area_Of_Receiving_DOCK as [Area Of Receiving Dock] ,tspl_mcc_master.No_Of_Chiller as [No Of Chiller] ,tspl_mcc_master.Chiller_Brand_Name as [Chiller Brand Name] ,tspl_mcc_master.Chiller_Capacity as [Chiller Capacity] ,tspl_mcc_master.No_Of_MilkPump as [No Of Milkpump] ,tspl_mcc_master.MilkPump_Capacity as [Milkpump Capacity] ,tspl_mcc_master.DripSaver as [Drip Saver] ,tspl_mcc_master.CanWasher as [Can Washer] ,tspl_mcc_master.CanScrubber as [Can Scrubber] ,tspl_mcc_master.FSSAI_NO as [FSSAI No] ,tspl_mcc_master.ETP as [ETP] ,tspl_mcc_master.Earthing as [Earthing] ,tspl_mcc_master.Coil_Length as [Coil Length] ,tspl_mcc_master.Electricity_Connection as [Electricity Connection] ,tspl_mcc_master.Boiler as [Boiler] ,tspl_mcc_master.NoOfDG as [No. of DG] ,tspl_mcc_master.NoOfCompressor as [No. of Compressor] ,tspl_mcc_master.PayeeName as [Payee Name] ,tspl_mcc_master.BankName as [Bank Name] ,tspl_mcc_master.BankBranch as [Bank Branch] ,tspl_mcc_master.BankCityCode as [Bank City Code] ,tspl_mcc_master.BankStateCode as [Bank State Code] ,tspl_mcc_master.IFCICode as [IFCI Code] ,tspl_mcc_master.AccountNO as [Account No] ,tspl_mcc_master.Created_By as [Created By] ,tspl_mcc_master.Created_Date as [Created Date] ,tspl_mcc_master.Modified_By as [Modified By] ,tspl_mcc_master.Modified_Date as [Modified Date] ,tspl_mcc_master.Comp_Code as [Company Code],tspl_mcc_master.mcc_code_vlc_uploader as [MCC Code For VLC Uploder],tspl_mcc_master.Plant_Code AS [Plant Code],TSPL_LOCATION_MASTER_PLANT.Location_Desc AS [Plant Name] from tspl_mcc_master LEFT JOIN TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_PLANT ON TSPL_LOCATION_MASTER_PLANT.Location_Code=tspl_mcc_master.Plant_Code  inner join tspl_location_master on tspl_location_master.location_Code= tspl_mcc_master.mcc_Code " _
        & " and (tspl_location_master.loc_segment_Code in (" & arrLoc & ") or tspl_mcc_master.mcc_Code in (" & arrLoc & ")))xx "

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
            Dim qry As String = "select TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [Uploader_Code], TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name, TSPL_VLC_MASTER_HEAD.Route_Code as [Route Code],TSPL_MCC_ROUTE_MASTER.Route_Name,TSPL_MCC_ROUTE_MASTER.Vehicle_Code,TSPL_VLC_MASTER_HEAD.VSP_Code,TSPL_VENDOR_MASTER.Vendor_Name as [VSP Name] " + Environment.NewLine +
            " from TSPL_VLC_MASTER_HEAD" + Environment.NewLine +
            " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code  " + Environment.NewLine +
            " left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_VLC_MASTER_HEAD.Route_Code  " + Environment.NewLine +
            " left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC " + Environment.NewLine

            Dim whrCls As String = ""
            If Not MultipleFinderFillAuto Then
                whrCls += "  TSPL_VLC_MASTER_HEAD.MCC  ='" + txtMCC.Value + "'"
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
        ElseIf e.Control AndAlso e.Alt AndAlso e.KeyCode = Keys.D Then
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
        ElseIf e.Alt AndAlso e.Control AndAlso e.Shift AndAlso e.KeyCode = Keys.F7 Then
            Dim pwd As New FrmPWD(Nothing)
            pwd.strCode = clsFixedParameterCode.MilkProcuremntPickCLRInsteadOfSNF
            pwd.strType = clsFixedParameterType.MilkProcuremntPickCLRInsteadOfSNF
            pwd.ShowDialog()
            If pwd.isPasswordCorrect Then
                Dim frm As New frmSetting
                frm.strFormID = Me.Form_ID
                frm.ShowDialog()
                If frm.isDataSaved Then
                    clsCommon.MyMessageBoxShow("Setting saved successfully." + Environment.NewLine + Me.Text + " will close automatic For apply new settings")
                    clsERPFuncationality.closeForm(Me)
                End If
            End If
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
                'objTr.Reject_Type = clsCommon.myCstr(gv1.Rows(ii).Cells(colRejectRejectType).Value)
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
                'If clsCommon.CompairString(Form_ID, clsUserMgtCode.MilkRetesting) = CompairStringResult.Equal Then
                '    If chkRetesting.Checked Then
                '        'Dim obj As New clsMilkSRNMCCDetail()
                '        Dim obj As New clsMilkSRNMCC()
                '        obj.Retesting_Status = 1
                '        obj.arrList.Add(obj)
                '    End If
                'ElseIf clsCommon.CompairString(Form_ID, clsUserMgtCode.MilkProcurementCorrection) = CompairStringResult.Equal Then
                '    If chkCorrection.Checked Then
                '        'Dim obj As New clsMilkSRNMCCDetail()
                '        Dim obj As New clsMilkSRNMCC()
                '        obj.Correction_Status = 2
                '        obj.arrList.Add(obj)
                '    End If
                'End If
                clsMilkSRNMCC.Correction(lblSRNNo.Text, CorrTypeSRNQty, CorrTypeSRNFATSNF, CorrTypeSRNVLC, txtQty.Value, clsCommon.myCstr(cboMilkType.SelectedValue), txtFAT.Value, txtSNF.Value, clsCommon.myCstr(TxtFinder1.Value), Form_ID)
            End If
            clsCommon.MyMessageBoxShow(Me, "Data corrected sucessfully", Me.Text)
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
            qry = "select TSPL_MILK_SRN_HEAD.DOC_CODE as SRNNo,TSPL_MILK_SRN_HEAD.Dock_Collection_Milk_Type as MilkType,TSPL_MILK_SRN_DETAIL.Qty,TSPL_MILK_SRN_DETAIL.UOM_Code,TSPL_MILK_SRN_DETAIL.FAT_PER,TSPL_MILK_SRN_DETAIL.SNF_PER,TSPL_MILK_SRN_DETAIL.Retesting_FAT,TSPL_MILK_SRN_DETAIL.Retesting_SNF,TSPL_MILK_SRN_DETAIL.Retesting_OR_Correction_Status,(Case When Retesting_OR_Correction_Status=1 Then TSPL_MILK_SRN_DETAIL.Retesting_FAT Else (Case When Retesting_OR_Correction_Status=2 Then TSPL_MILK_SRN_DETAIL.FAT_PER Else TSPL_MILK_SRN_DETAIL.Retesting_FAT End)End) As FAT,
(Case When Retesting_OR_Correction_Status=1 Then TSPL_MILK_SRN_DETAIL.Retesting_SNF Else (Case When Retesting_OR_Correction_Status=2 Then TSPL_MILK_SRN_DETAIL.SNF_PER Else TSPL_MILK_SRN_DETAIL.Retesting_SNF End) End) As SNF from TSPL_MILK_SRN_DETAIL" + Environment.NewLine +
                    "left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE"

            Dim whr As String = " convert(date, TSPL_MILK_SRN_HEAD.DOC_DATE,106)='" + clsCommon.GetPrintDate(txtShiftDate.Value, "dd/MMM/yyyy") + "' and TSPL_MILK_SRN_HEAD.SHIFT='" + clsCommon.myCstr(cboShift.SelectedValue) + "' and TSPL_MILK_SRN_HEAD.VLC_CODE='" + clsCommon.myCstr(txtVLC.Tag) + "' and TSPL_MILK_SRN_HEAD.Against_Reject_No is null"

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

                'If clsCommon.myCdbl(dt.Rows(0)("Retesting_OR_Correction_Status")) = 1 Then
                '    txtFAT.Value = clsCommon.myCdbl(dt.Rows(0)("Retesting_FAT"))
                '    txtFAT.Tag = clsCommon.myCdbl(dt.Rows(0)("Retesting_FAT"))
                '    txtSNF.Value = clsCommon.myCdbl(dt.Rows(0)("Retesting_SNF"))
                '    txtSNF.Tag = clsCommon.myCdbl(dt.Rows(0)("Retesting_SNF"))
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
                'txtRetestFAT.Value = clsCommon.myCdbl(dt.Rows(0)("Retesting_FAT"))
                'txtRetestSNF.Value = clsCommon.myCdbl(dt.Rows(0)("Retesting_FAT"))
                cboMilkType.SelectedValue = clsCommon.myCstr(dt.Rows(0)("MilkType"))
                cboMilkType.Tag = clsCommon.myCstr(dt.Rows(0)("MilkType"))
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
        TxtFinder1.Value = Nothing
        TxtFinder1.Tag = Nothing
        MyLabel5.Text = Nothing
        RadGroupBox2.Enabled = True
        RadGroupBox1.Enabled = False
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
                            Dim settSNFDecimalPlace As Integer = clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.SNFDecimalPlaces, clsFixedParameterCode.SNFDecimalPlaces, Nothing))
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
                                qry = "select xx.* from (
select max(case when isOwnBMC=1 then x.PK_Id else '' end) as PK_Id, max(isOwnBMC) as isOwnBMC, 
sum(MCCQty) as MCCQty,sum(Qty) as TotQty,sum(Qty)-sum(MCCQty) as DiffQty,
sum(MCCFATKG) as MCCFATKG,sum(FATKG) as TotFATKG,sum(FATKG)-sum(MCCFATKG) as DiffFATKG,
sum(MCCSNFKG) as MCCSNFKG,sum(SNFKG) as TotSNFKG,sum(SNFKG)-sum(MCCSNFKG) as DiffSNFKG,
max(case when isOwnBMC=1 then x.VLC_Code else '' end ) as VLC_Code
from (
select TSPL_MILK_COLLECTION_DCS_DETAIL.PK_Id, case when isnull(TSPL_VLC_MASTER_HEAD.isOwnBMC,0)=1 and TSPL_VLC_MASTER_HEAD.MCC=Tab.MCC_Code then 1 else 0 end as isOwnBMC,0.00 as MCCQty,0.00 as MCCFATKG,0.00 as MCCSNFKG,TSPL_MILK_COLLECTION_DCS_DETAIL.Qty,TSPL_MILK_COLLECTION_DCS_DETAIL.FATKG,TSPL_MILK_COLLECTION_DCS_DETAIL.SNFKG,TSPL_MILK_COLLECTION_DCS_DETAIL.VLC_Code
from TSPL_MILK_COLLECTION_DCS_DETAIL
left outer join TSPL_MILK_COLLECTION_DCS on  TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_COLLECTION_DCS_DETAIL.VLC_Code
left outer join (select Document_No,max(MCC_Code) as MCC_Code from (select TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No,TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code from TSPL_MILK_COLLECTION_DCS_MCC_DETAIL 
inner join TSPL_MILK_COLLECTION_MCC_DETAIL on TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id=TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail )xx group by Document_No )Tab on Tab.Document_No= TSPL_MILK_COLLECTION_DCS.Document_No
where TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No='" + clsCommon.myCstr(drDOC("Document_No")) + "' 
union all
select 0 as PK_Id, 0 as isOwnBMC,TSPL_MILK_COLLECTION_MCC_DETAIL.Qty as MCCQty,TSPL_MILK_COLLECTION_MCC_DETAIL.FATKG as MCCFATKG,TSPL_MILK_COLLECTION_MCC_DETAIL.SNFKG as MCCSNFKG,0.00 as Qty,0.00 as FATKG,0.00 as SNFKG,'' as VLC_Code from
TSPL_MILK_COLLECTION_DCS_MCC_DETAIL  
left outer join TSPL_MILK_COLLECTION_MCC_DETAIL on TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id=TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail
where TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No='" + clsCommon.myCstr(drDOC("Document_No")) + "' 
)x
)xx"
                                Dim dtDCS As DataTable = clsDBFuncationality.GetDataTable(qry)
                                If dtDCS IsNot Nothing AndAlso dtDCS.Rows.Count > 0 Then
                                    For Each drDCS As DataRow In dtDCS.Rows
                                        If clsCommon.myCdbl(drDCS("isOwnBMC")) = 1 Then
                                            If Math.Abs(clsCommon.myCdbl(drDCS("DiffFATKG"))) > 0 OrElse Math.Abs(clsCommon.myCdbl(drDCS("DiffSNFKG"))) > 0 Then
                                                qry = "select xx.*,TSPL_MILK_SRN_HEAD.DOC_CODE,TSPL_MILK_SRN_HEAD.Dock_Collection_Milk_Type from
	(select PK_Id,Qty,FATKG,SNFKG,Shift 
	 from TSPL_MILK_COLLECTION_DCS_DETAIL where exists(select 1 from TSPL_MILK_COLLECTION_DCS_DETAIL as  inn where inn.PK_Id=" + clsCommon.myCstr(drDCS("PK_Id")) + " and inn.VLC_Code=TSPL_MILK_COLLECTION_DCS_DETAIL.VLC_Code and inn.Document_No= TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No)
	 )xx
	 left outer join TSPL_MILK_SHIFT_UPLOADER_DETAIL on TSPL_MILK_SHIFT_UPLOADER_DETAIL.Against_Milk_Collection_DCS_Detail=xx.PK_Id
left outer join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Against_Milk_Collection_DCS_Detail=xx.PK_Id
left outer join TSPL_MILK_RECEIPT_DETAIL on TSPL_MILK_RECEIPT_DETAIL.Against_Uploader_TR_No=TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No or TSPL_MILK_RECEIPT_DETAIL.Against_Shift_Uploader_TR_No=TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No
left outer join TSPL_MILK_SAMPLE_HEAD on TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE=TSPL_MILK_RECEIPT_DETAIL.DOC_CODE
left outer join TSPL_MILK_SAMPLE_DETAIL on TSPL_MILK_SAMPLE_DETAIL.DOC_CODE=TSPL_MILK_SAMPLE_HEAD.DOC_CODE and TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO=TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO
left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.SAMPLE_NO=TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO and TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE=TSPL_MILK_SAMPLE_DETAIL.DOC_CODE
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

                                                        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                                                        Try
                                                            qry = "update TSPL_MILK_COLLECTION_DCS_DETAIL set Own_Qty= case when Own_Qty is null then Qty else Own_Qty end,Own_FAT= case when Own_FAT is null then FAT else Own_FAT end,Own_SNF= case when Own_SNF is null then SNF else Own_SNF end,Own_FATKG= case when Own_FATKG is null then FATKG else Own_FATKG end,Own_SNFKG= case when Own_SNFKG is null then SNFKG else Own_SNFKG end where PK_Id=" + clsCommon.myCstr(dtDetail.Rows(indx)("PK_Id")) + ""
                                                            clsDBFuncationality.ExecuteNonQuery(qry, trans)

                                                            clsMilkSRNMCC.Correction(clsCommon.myCstr(dtDetail.Rows(indx)("DOC_CODE")), True, True, False, Qty, clsCommon.myCstr(dtDetail.Rows(indx)("Dock_Collection_Milk_Type")), FAT, SNF, "", False, trans, True, Form_ID)
                                                            Dim coll As New Hashtable()
                                                            clsCommon.AddColumnsForChange(coll, "Qty", Qty)
                                                            clsCommon.AddColumnsForChange(coll, "FAT", FAT)
                                                            clsCommon.AddColumnsForChange(coll, "SNF", SNF)
                                                            clsCommon.AddColumnsForChange(coll, "FATKG", FATKG)
                                                            clsCommon.AddColumnsForChange(coll, "SNFKG", SNFKG)
                                                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_COLLECTION_DCS_DETAIL", OMInsertOrUpdate.Update, "PK_Id='" + clsCommon.myCstr(dtDetail.Rows(indx)("PK_Id")) + "'", trans)

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
                            Next
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
where TSPL_MILK_SRN_HEAD.Against_Reject_No is null and TSPL_MILK_SRN_HEAD.MCC_CODE='" + strMCCcode + "' and TSPL_MILK_SRN_HEAD.DOC_DATE>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtVLCCMFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and TSPL_MILK_SRN_HEAD.DOC_DATE<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtVLCCMToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'"
                                    If Not clsCommon.CompairString(clsCommon.myCstr(cboMPCMShift.SelectedValue), "B") = CompairStringResult.Equal Then
                                        qry += " and TSPL_MILK_SRN_HEAD.SHIFT='" + clsCommon.myCstr(cboVLCCMShift.SelectedValue) + "'"
                                    End If
                                    Dim dtUploader As DataTable = clsDBFuncationality.GetDataTable(qry)
                                    If dtUploader IsNot Nothing AndAlso dtUploader.Rows.Count > 0 Then
                                        For Each drUploader As DataRow In dtUploader.Rows
                                            clsMilkSRNMCC.Correction(clsCommon.myCstr(drUploader("DOC_CODE")), clsCommon.myCstr(drUploader("Dock_Collection_Milk_Type")), clsCommon.myCdbl(drUploader("FAT_PER")), clsCommon.myCdbl(drUploader("SNF_PER")))
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

    Private Sub BulkDelete_Click(sender As Object, e As EventArgs) Handles BulkDelete.Click
        Try
            If clsCommon.MyMessageBoxShow("Delete the collection data", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
            clsMilkShiftUploaderHead.DeleteCollectionData(TxtMultiSelectFinder8.arrValueMember, txtMCCFromDate.Value, txtMCCToDate.Value, clsCommon.myCstr(txtFromShift.SelectedValue), chkDeleteBMCCollection.Checked, chkPreviousShift.Checked)
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
            If clsCommon.myLen(txtBMCRouteNo.Value) <= 0 Then
                txtBMCRouteNo.Focus()
                Throw New Exception("Please provide Route code ")
            End If
            Dim whr As String = "len(isnull(TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader,''))>0 "
            'If Not SettShowAllMCC Then
            '    whr += " and TSPL_BULK_ROUTE_MASTER_MCC.ROUTE_NO='" + txtBMCRouteNo.Value + "' "
            'End If
            txtBMCBMC.Value = clsMccMaster.getFinderUploader(whr, txtBMCBMC.Value, isButtonClicked)
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader,TSPL_MCC_MASTER.MCC_Code,TSPL_MCC_MASTER.MCC_NAME from TSPL_MCC_MASTER where Mcc_Code_VLC_Uploader='" + clsCommon.myCstr(txtBMCBMC.Value) + "'")
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                txtBMCBMC.Tag = clsCommon.myCstr(dt.Rows(0)("MCC_Code"))
                lblBMCBMC.Text = clsCommon.myCstr(dt.Rows(0)("MCC_NAME"))
            End If
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

            Dim qry As String = "select TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No,TSPL_MILK_COLLECTION_MCC.Document_Date,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader as MCC, TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code,TSPL_MCC_MASTER.MCC_NAME,TSPL_MILK_COLLECTION_MCC_DETAIL.Milk_Type,TSPL_MILK_COLLECTION_MCC_DETAIL.Qty,TSPL_MILK_COLLECTION_MCC_DETAIL.FAT,TSPL_MILK_COLLECTION_MCC_DETAIL.SNF,TSPL_MILK_COLLECTION_MCC_DETAIL.SNo,TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id
,TSPL_MILK_COLLECTION_MCC_DETAIL.IsUpdatedFromCorrection,TSPL_MILK_COLLECTION_MCC_DETAIL.Retesting_OR_Correction,
TSPL_MILK_COLLECTION_MCC_DETAIL.Retesting_FAT,TSPL_MILK_COLLECTION_MCC_DETAIL.Retesting_SNF,TSPL_MILK_COLLECTION_MCC_DETAIL.Retesting_CLR
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
left outer join TSPL_MILK_RECEIPT_DETAIL on TSPL_MILK_RECEIPT_DETAIL.Against_Shift_Uploader_TR_No=TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No or TSPL_MILK_RECEIPT_DETAIL.Against_Uploader_TR_No=TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No 
left outer join TSPL_MILK_SAMPLE_HEAD on TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE=TSPL_MILK_RECEIPT_DETAIL.DOC_CODE
left outer join TSPL_MILK_SAMPLE_DETAIL on TSPL_MILK_SAMPLE_DETAIL.DOC_CODE=TSPL_MILK_SAMPLE_HEAD.DOC_CODE and TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO=TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO
left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE=TSPL_MILK_SAMPLE_HEAD.DOC_CODE and TSPL_MILK_SRN_HEAD.SAMPLE_NO=TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO
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
            Else
                Arr(0).Correction_Qty = txtBMCCorrQty.Value
                Arr(0).Correction_FAT = txtBMCCorrFAT.Value
                Arr(0).Correction_SNF = txtBMCCorrSNF.Value
                Arr(0).Retesting_OR_Correction = 2
            End If
            'Arr(0).FAT = txtBMCCorrFAT.Value
            'Arr(0).SNF = txtBMCCorrSNF.Value
            Arr(0).FATKG = Math.Round(Arr(0).Qty * Arr(0).FAT / 100, 3, MidpointRounding.ToEven)
            Arr(0).SNFKG = Math.Round(Arr(0).Qty * Arr(0).SNF / 100, 3, MidpointRounding.ToEven)
            clsMilkCollectionMCCDetail.SaveData(lblBMCDocNo.Text, txtBMCDate.Value, Arr, True, Nothing, isCorrection)
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

            Dim qry As String = "select TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE
from TSPL_MILK_COLLECTION_MCC_DETAIL
left outer join TSPL_MILK_COLLECTION_DCS_MCC_DETAIL on TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail=TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id 
left outer join TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No
left outer join TSPL_MILK_COLLECTION_DCS_DETAIL on TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No=TSPL_MILK_COLLECTION_DCS.Document_No
left outer join TSPL_MILK_SHIFT_UPLOADER_DETAIL on TSPL_MILK_SHIFT_UPLOADER_DETAIL.Against_Milk_Collection_DCS_Detail=TSPL_MILK_COLLECTION_DCS_DETAIL.PK_Id
left outer join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Against_Milk_Collection_DCS_Detail=TSPL_MILK_COLLECTION_DCS_DETAIL.PK_Id
left outer join TSPL_MILK_RECEIPT_DETAIL on TSPL_MILK_RECEIPT_DETAIL.Against_Shift_Uploader_TR_No=TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No or TSPL_MILK_RECEIPT_DETAIL.Against_Uploader_TR_No=TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No 
left outer join TSPL_MILK_SAMPLE_HEAD on TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE=TSPL_MILK_RECEIPT_DETAIL.DOC_CODE
left outer join TSPL_MILK_SAMPLE_DETAIL on TSPL_MILK_SAMPLE_DETAIL.DOC_CODE=TSPL_MILK_SAMPLE_HEAD.DOC_CODE and TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO=TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO
left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE=TSPL_MILK_SAMPLE_HEAD.DOC_CODE and TSPL_MILK_SRN_HEAD.SAMPLE_NO=TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO
left outer join TSPL_MILK_PURCHASE_INVOICE_DETAIL on TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE=TSPL_MILK_SRN_HEAD.DOC_CODE
where TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE is not null and TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No='" + lblBMCTankerDocNo.Text + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Throw New Exception("Milk Purchase Invoice Generated [" + clsCommon.myCstr(dt.Rows(0)("DOC_CODE")) + "]")
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
                Dim CLR As Decimal
                CLR = clsEkoPro.getClrOnCalculation(obj.Retesting_FAT, obj.Retesting_SNF, corrFactor)

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
            Dim qry As String = "select TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id,TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No,TSPL_MILK_COLLECTION_MCC.Document_Date,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader as MCC, TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code,TSPL_MCC_MASTER.MCC_NAME,TSPL_MILK_COLLECTION_MCC_DETAIL.Milk_Type,TSPL_MILK_COLLECTION_MCC_DETAIL.Qty,TSPL_MILK_COLLECTION_MCC_DETAIL.FAT,TSPL_MILK_COLLECTION_MCC_DETAIL.SNF,TSPL_MILK_COLLECTION_MCC_DETAIL.SNo
                                from TSPL_MILK_COLLECTION_MCC_DETAIL
                                left outer join  TSPL_MILK_COLLECTION_MCC on TSPL_MILK_COLLECTION_MCC.Document_No=TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No
                                left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code"
            qry += " where convert(date, TSPL_MILK_COLLECTION_MCC.Document_Date,106)='" + clsCommon.GetPrintDate(txtBMCDate.Value, "dd/MMM/yyyy") + "' "

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
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "PK_Id", "Document_No", "Document_Date", "MCC", "MCC_Code", "MCC_NAME", "Milk_Type", "Qty", "FAT", "SNF", "SNo") Then
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
                        qry = "select TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE
                            from TSPL_MILK_COLLECTION_MCC_DETAIL
                            left outer join TSPL_MILK_COLLECTION_DCS_MCC_DETAIL on TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail=TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id 
                            left outer join TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No
                            left outer join TSPL_MILK_COLLECTION_DCS_DETAIL on TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No=TSPL_MILK_COLLECTION_DCS.Document_No
                            left outer join TSPL_MILK_SHIFT_UPLOADER_DETAIL on TSPL_MILK_SHIFT_UPLOADER_DETAIL.Against_Milk_Collection_DCS_Detail=TSPL_MILK_COLLECTION_DCS_DETAIL.PK_Id
                            left outer join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Against_Milk_Collection_DCS_Detail=TSPL_MILK_COLLECTION_DCS_DETAIL.PK_Id
                            left outer join TSPL_MILK_RECEIPT_DETAIL on TSPL_MILK_RECEIPT_DETAIL.Against_Shift_Uploader_TR_No=TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No or TSPL_MILK_RECEIPT_DETAIL.Against_Uploader_TR_No=TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No 
                            left outer join TSPL_MILK_SAMPLE_HEAD on TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE=TSPL_MILK_RECEIPT_DETAIL.DOC_CODE
                            left outer join TSPL_MILK_SAMPLE_DETAIL on TSPL_MILK_SAMPLE_DETAIL.DOC_CODE=TSPL_MILK_SAMPLE_HEAD.DOC_CODE and TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO=TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO
                            left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE=TSPL_MILK_SAMPLE_HEAD.DOC_CODE and TSPL_MILK_SRN_HEAD.SAMPLE_NO=TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO
                            left outer join TSPL_MILK_PURCHASE_INVOICE_DETAIL on TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE=TSPL_MILK_SRN_HEAD.DOC_CODE
                            where TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE is not null and TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id=" + clsCommon.myCstr(grow.Cells("PK_Id").Value) + ""
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            'Throw New Exception("Milk Purchase Invoice Generated [" + clsCommon.myCstr(grow.Cells("Document_No").Value) + "]")
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
                        clsMilkCollectionMCCDetail.SaveData(clsCommon.myCstr(grow.Cells("Document_No").Value), txtBMCDate.Value, Arr, True, isCorrection)
                        'clsCommon.MyMessageBoxShow(Me, "Data corrected sucessfully", Me.Text)
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
        Dim gv As New RadGridView()
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
                                            left outer join TSPL_MILK_RECEIPT_DETAIL on TSPL_MILK_RECEIPT_DETAIL.Against_Shift_Uploader_TR_No=TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No or TSPL_MILK_RECEIPT_DETAIL.Against_Uploader_TR_No=TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No 
                                            left outer join TSPL_MILK_SAMPLE_HEAD on TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE=TSPL_MILK_RECEIPT_DETAIL.DOC_CODE
                                            left outer join TSPL_MILK_SAMPLE_DETAIL on TSPL_MILK_SAMPLE_DETAIL.DOC_CODE=TSPL_MILK_SAMPLE_HEAD.DOC_CODE and TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO=TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO
                                            left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE=TSPL_MILK_SAMPLE_HEAD.DOC_CODE and TSPL_MILK_SRN_HEAD.SAMPLE_NO=TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO
                                            left outer join TSPL_MILK_PURCHASE_INVOICE_DETAIL on TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE=TSPL_MILK_SRN_HEAD.DOC_CODE
                                            where TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE is not null and TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No='" + clsCommon.myCstr(grow.Cells("Document_No").Value) + "'"
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            Throw New Exception("Milk Purchase Invoice Generated.")
                        End If
                        Dim obj As New clsMilkCollectionMCC

                        obj.Document_No = clsCommon.myCstr(grow.Cells("Document_No").Value)
                        Dim Qty As Decimal = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue("select TSPL_MILK_COLLECTION_MCC.Entered_Qty from  TSPL_MILK_COLLECTION_MCC where CONVERT(Date, TSPL_MILK_COLLECTION_MCC.Document_Date,103)='" + clsCommon.GetPrintDate(clsCommon.myCstr(grow.Cells("Document_Date").Value), "dd/MMM/yyyy") + "' And Status=1 and TSPL_MILK_COLLECTION_MCC.Document_No='" + clsCommon.myCstr(grow.Cells("Document_No").Value) + "'"))
                        If isCorrection = 1 Then
                            obj.Entered_Qty = clsCommon.myCDecimal(grow.Cells("Entered_Qty").Value)
                            obj.Entered_FATKg = Math.Round((Qty * clsCommon.myCDecimal(grow.Cells("FATPer").Value) / 100), 3, MidpointRounding.ToEven)
                            obj.Entered_SNFKg = Math.Round((Qty * clsCommon.myCDecimal(grow.Cells("SNFPer").Value) / 100), 3, MidpointRounding.ToEven)
                            obj.Correction_Qty = clsCommon.myCDecimal(grow.Cells("Correction_Qty").Value)
                            obj.Correction_FAT = clsCommon.myCDecimal(grow.Cells("FATPer").Value)
                            obj.Correction_SNF = clsCommon.myCDecimal(grow.Cells("SNFPer").Value)

                        ElseIf isCorrection = 2 Then
                            obj.Entered_Qty = clsCommon.myCDecimal(grow.Cells("Entered_Qty").Value)
                            obj.Entered_FATKg = Math.Round((Qty * clsCommon.myCDecimal(grow.Cells("FATPer").Value) / 100), 3, MidpointRounding.ToEven)
                            obj.Entered_SNFKg = Math.Round((Qty * clsCommon.myCDecimal(grow.Cells("SNFPer").Value) / 100), 3, MidpointRounding.ToEven)
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

    Private Sub txtBMCTankerQty_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtBMCTankerQty.Validating
        lblBMCTankerFATKG.Text = Math.Round((txtBMCTankerQty.Value * txtBMCTankerFAT.Value / 100), 3, MidpointRounding.ToEven)
        lblBMCTankerSNFKG.Text = Math.Round((txtBMCTankerQty.Value * txtBMCTankerSNF.Value / 100), 3, MidpointRounding.ToEven)
    End Sub
End Class