Imports common
Imports System.Data.SqlClient

Public Class frmTempleteImportMP
    Inherits Telerik.WinControls.UI.RadForm
#Region "variable"
    ''Public Const colMCCUploaderCode As String = "MCC Uploader Code#"
    ''Public Const colMCCCode As String = "MCC Code#"
    ''Public Const colMCCType As String = "MCC Type#"
    ''Public Const colMCCName As String = "MCC Name#"
    ''Public Const colMCCChillingVendorCode As String = "MCC Chilling Vendor Code#"
    ''Public Const colMCCAddress1 As String = "MCC Address1#"
    ''Public Const colMCCAddress2 As String = "MCC Address2#"
    ''Public Const colMCCTehsil As String = "MCC Tehsil#"
    ''Public Const colMCCCity As String = "MCC City Code#"
    ''Public Const colMCCState As String = "MCC State Code#"
    ''Public Const colMCCCountry As String = "MCC Country Code#"
    ''Public Const colMCCPinCode As String = "MCC Pin Code#"
    ''Public Const colMCCTelphone As String = "MCC Telphone#"
    ''Public Const colMCCEmail As String = "MCC Email#"
    ''Public Const colMCCFax As String = "MCC Fax#"
    ''Public Const colMccSuperArea As String = "MCC Super Area#"
    ''Public Const colMccSuperAreaUOM As String = "MCC Super Area UOM(Sq. Ft./Sq. Mt.)#"
    ''Public Const colMCCAreaOfStore As String = "MCC Area Of Store#"
    ''Public Const colMCCAreaOfOffice As String = "MCC Area Of Office#"
    ''Public Const colMCCAreaOfOfficeUOM As String = "MCC Area of Office UOM(Sq. Ft./Sq. Mt.)#"
    ''Public Const colMCCOpenAreaForTanker As String = "MCC Open Area For Tanker#"
    ''Public Const colMCCOpenAreaForTankerUOM As String = "MCC Open Area For Tanker UOM(Sq. Ft./Sq. Mt.)#"
    ''Public Const colMCCAreaOfLab As String = "MCC Area Of Lab#"
    ''Public Const colMCCAreaOfLabUOM As String = "MCC Area of Lab UOM(Sq. Ft./Sq. Mt.)#"
    ''Public Const colMCCTotalStorageCapacity As String = "MCC Total Storage Capacity#"
    ''Public Const colMCCAreaOfReceivingDock As String = "MCC Area Of Receiving Dock#"
    ''Public Const colMCCAreaOfReceivingDockUOM As String = "MCC Area of Receiving Dock UOM(Sq. Ft./Sq. Mt.)#"
    ''Public Const colMCCDripSaver As String = "MCC Drip Saver (Yes/No)#"
    ''Public Const colMCCCanWasher As String = "MCC Can Washer (Yes/No)#"
    ''Public Const colMCCCanScrubber As String = "MCC Can Scrubber (Yes/No)#"
    ''Public Const colMCCFssaiNo As String = "MCC Fssai No#"
    ''Public Const colMCCETP As String = "MCC ETP (Yes/No)#"
    ''Public Const colMCCEarthing As String = "MCC Earthing (Yes/No)#"
    ''Public Const colMCCCoilLength As String = "MCC Coil Length#"
    ''Public Const colMCCElectricityConnection As String = "MCC Electricity Connection#"
    ''Public Const colMCCBoiler As String = "MCC Boiler (Yes/No)#"
    ''Public Const colMCCIndustryType As String = "MCC Industry Type#"
    ''Public Const colMCCPropName As String = "MCC Prop Name#"
    ''Public Const colMCCPartnerName As String = "MCC Partner Name#"
    ''Public Const colMCCDirectorName As String = "MCC Director Name#"
    ''Public Const colMCCMonthlyProvision As String = "MCC Monthly Provision(Y/N)#"
    ''Public Const colMCCChillingCharges As String = "MCC Chilling Charges#"
    ''Public Const colMCCChillingOn As String = "MCC Chilling On#"
    ''Public Const colMCCChillingMinGuaranteedAvgQty As String = "MCC Chilling Min. Guaranteed Avg. Qty#"
    ''Public Const colMCCChillingOnUOMKGLTR As String = "MCC Chilling On UOM(KG/LTR)#"
    ''Public Const colMCCChillingOnQty As String = "MCC Chilling On Qty#"
    ''Public Const colMCCChillingOnUOMHandledDispatched As String = "MCC Chilling On UOM(Handled/Dispatched)#"
    ''Public Const colMCCChillingMinGuaranteedPeriod As String = "MCC Chilling Min. Guaranteed Period#"
    ''Public Const colMCCChillingMinGuaranteedPeriodUOM As String = "MCC Chilling Min. Guaranteed Period UOM (Day/Month/Year)#"
    ''Public Const colMCCRateofLeaseCharges As String = "MCC Rate of Lease Charges#"
    ''Public Const colMCCRateofLeasedChargesUOM As String = "MCC Rate of Leased Charges UOM(Day/Month/Year)#"
    ''Public Const colMCCAreaofStoreUOM As String = "MCC Area of Store UOM(Sq. Ft./Sq. Mt.)#"
    ''Public Const colMCCAgreement_Status As String = "MCC Agreement_Status#"
    ''Public Const colMCCAgreement_Date As String = "MCC Agreement_Date#"
    ''Public Const colMCCAgreementExpiryDate As String = "MCC Agreement Expiry Date#"
    ''Public Const colMCCSecurity_Status As String = "MCC Security_Status#"
    ''Public Const colMCCCheque_Amt As String = "MCC Cheque_Amt#"
    ''Public Const colMCCCheque_No As String = "MCC Cheque_No#"
    ''Public Const colMCCCheque_Date As String = "MCC Cheque_Date#"
    ''Public Const colMCCChillingStartingDate As String = "MCC Chilling Starting Date#"
    ''Public Const colMCCIsTruckSheetMandatory As String = "MCC Is Truck Sheet Mandatory#"
    ''Public Const colMCCWeighingComPort As String = "MCC Weighing ComPort#"
    ''Public Const colMCCWeighingMachine As String = "MCC Weighing Machine#"
    ''Public Const colMCCSampleComPort As String = "MCC Sample ComPort#"
    ''Public Const colMCCSampleMachine As String = "MCC Sample Machine#"
    ''Public Const colMCCPaymentCycle As String = "MCC Payment Cycle#"
    ''Public Const colMCCIncentiveCode As String = "MCC Incentive Code#"
    ''Public Const colMCCShiftMorningOpeningTime As String = "MCC Shift Morning Opening Time#"
    ''Public Const colMCCShiftMorningClosingTime As String = "MCC Shift Morning Closing Time#"
    ''Public Const colMCCShiftEveningOpeningTime As String = "MCC Shift Evening Opening Time#"
    ''Public Const colMCCShiftEveningClosingTime As String = "MCC Shift Evening Closing Time#"
    ''Public Const colMCCRM As String = "MCC RM#"
    ''Public Const colMCCRequiredGateEntry As String = "MCC Required Gate Entry(Yes/No)#"
    ''Public Const colMCCAllowAutoMilkIn As String = "MCC AllowAutoMilkIn#"
    ''Public Const colMCCAutoIn_Location As String = "MCC AutoIn_Location#"
    ''Public Const colMCCSILOIn_Location As String = "MCC SILOIn_Location#"
    ''Public Const colMCCApplyReceiptWeightTolerance As String = "MCC ApplyReceiptWeightTolerance(Y/N)#"
    ''Public Const colMCCReceiptWeightToleranceValue As String = "MCC ReceiptWeightToleranceValue#"
    ''Public Const colMCCApplyFailedSample As String = "MCC Apply Failed Sample(Y/N)#"
    ''Public Const colMCCFailedSampleFAT As String = "MCC Failed Sample FAT %#"
    ''Public Const colMCCFailedSampleSNF As String = "MCC Failed Sample SNF %#"
    ''Public Const colMCCLocSegmentCode As String = "MCC Loc Segment Code#"
    ''Public Const colMCCBMCC As String = "MCC MCC(1)/BMCC(0)#"
    ''Public Const colMCCCommissionRate As String = "MCC CommissionRate#"
    ''Public Const colMCCCommissionMinimumShiftInPaymentCycle As String = "MCC CommissionMinimumShiftInPaymentCycle#"
    ''Public Const colMCCCommissionMinimumQtyInShift As String = "MCC CommissionMinimumQtyInShift#"
    ''Public Const colMCCCommissionNoOfPaymentCycleForNewVSP As String = "MCC CommissionNoOfPaymentCycleForNewVSP#"
    ''Public Const colMCCDeductionMinimumFATPer As String = "MCC DeductionMinimumFATPer#"
    ''Public Const colMCCDeductionMinimumSNFPer As String = "MCC DeductionMinimumSNFPer#"
    ''Public Const colMCCDeductionNoOfPaymentCycleForNewVSP As String = "MCC DeductionNoOfPaymentCycleForNewVSP#"
    ''Public Const colMCCPlant As String = "MCC Plant#"
    ''Public Const colMCCMorningShiftOpeningTime As String = "MCC Morning Shift Opening Time#"
    ''Public Const colMCCMorningShiftClosingTime As String = "MCC Morning Shift Closing Time#"
    ''Public Const colMCCEveningShiftOpeningTime As String = "MCC Evening Shift Opening Time#"
    ''Public Const colMCCEveningShiftClosingTime As String = "MCC Evening Shift Closing Time#"
    'Public Const colDCSRouteCode As String = "DCS Route Code"
    'Public Const colDCSRouteName As String = "DCS Route Name"
    'Public Const colDCSRouteDistance As String = "DCS Route Distance"
    'Public Const colDCSRouteEffectiveStartDate As String = "DCS Route Effective Start Date"
    'Public Const colDCSVehicle As String = "DCS Vehicle"
    'Public Const colDCSVehiclePaymentBasis As String = "DCS Vehicle payment basis"
    'Public Const colDCSPaymentPerKm As String = "DCS Payment Per Km"
    'Public Const colDCSVehicleEffectiveStartDate As String = "DCS Vehicle Effective Start Date"
    'Public Const colDCSTransporterCode As String = "DCS Transporter Code"
    'Public Const colDCSTransporterName As String = "DCS Transporter Name"
    ''Public Const colDCSTransporterGroupCode As String = "DCS Transporter group code#"
    ''Public Const colDCSVLCCode As String = "DCS VLC Code#"
    ''Public Const colDCSVLCName As String = "DCS VLC Name#"
    ''Public Const colDCSUploaderCode As String = "DCS Uploader Code#"
    ''Public Const colDCSVillageName As String = "DCS Village Name#"
    ''Public Const colDCSVSPCode As String = "DCS VSP Code#"
    ''Public Const colDCSVSPName As String = "DCS VSP Name#"
    ''Public Const colDCSType As String = "DCS Type#"
    ''Public Const colDCSVSPAddress As String = "DCS VSP Address#"
    ''Public Const colDCSState As String = "DCS State#"
    ''Public Const colDCSVSPGroupCode As String = "DCS Group Code#"
    ''Public Const colDCSCreatecustomer As String = "DCS Create customer#"
    ''Public Const colDCSCustomerGroupCode As String = "DCS Customer Group Code#"
    ''Public Const colVSPPaymentType As String = "DCS Payment type#"
    ''Public Const colDCSBankCode As String = "DCS Bank Code#"
    ''Public Const colDCSBankName As String = "DCS Bank Name#"
    ''Public Const colDCSIFSCCode As String = "DCS IFSC Code#"
    ''Public Const colDCSBranchName As String = "DCS Branch Name#"
    ''Public Const colDCSAccountNo As String = "DCS Account No#"
    ''Public Const colDCSBuffalowTIP As String = "DCS Buffalow TIP#"
    ''Public Const colDCSCowTIP As String = "DCS Cow TIP#"

    ''Public Const colMPUploaderCode As String = "Farmer MP Uploader Code#"
    ''Public Const colMPCode As String = "Farmer Code#"
    ''Public Const colMPName As String = "Farmer Name#"
    ''Public Const colMPFatherName As String = "Farmer Father Name#"
    ''Public Const colMPAddress1 As String = "Farmer Address1#"
    ''Public Const colMPAddress2 As String = "Farmer Address2#"
    ''Public Const colMPZila As String = "Farmer Zila#"
    ''Public Const colMPTehsil As String = "Farmer Tehsil#"
    ''Public Const colMPCityCode As String = "Farmer City Code#"
    ''Public Const colMPStateCode As String = "Farmer State Code#"
    ''Public Const colMPCountryCode As String = "Farmer Country Code#"
    ''Public Const colMPPinCode As String = "Farmer Pin Code#"
    ''Public Const colMPTelphone As String = "Farmer Telphone#"
    ''Public Const colMPEmail As String = "Farmer Email#"
    ''Public Const colMPAadharNo As String = "Farmer AadharNo#"
    ''Public Const colMPJanAadharNo As String = "Farmer JanAadharNo#"
    ''Public Const colMPDateofBirth As String = "Farmer Date of Birth#"
    ''Public Const colMPEducation As String = "Farmer Education#"
    ''Public Const colMPLandHolding As String = "Farmer Land Holding#"
    ''Public Const colMPNoOfMilchAnimal As String = "Farmer No Of Milch Animal#"
    ''Public Const colMPTotalMilkProduction As String = "Farmer Total Milk Production#"
    ''Public Const colMPMilkForSelfConsumption As String = "Farmer Milk For Self Consumption#"
    ''Public Const colMPMilkForSale As String = "Farmer Milk For Sale#"
    ''Public Const colMPPayeeName As String = "Farmer Payee Name#"
    ''Public Const colMPBankCode As String = "Farmer Bank Code#"
    ''Public Const colMPBankBranch As String = "Farmer Bank Branch#"
    ''Public Const colMPBankCityCode As String = "Farmer Bank City Code#"
    ''Public Const colMPBankStateCode As String = "Farmer Bank State Code#"
    ''Public Const colMPIFSCCode As String = "Farmer IFSC Code#"
    ''Public Const colMPAccountNo As String = "Farmer Account No#"
    ''Public Const colMPCustomerAccSet As String = "Farmer Customer Acc Set#"
    ''Public Const colMPVendorAccSet As String = "Farmer Vendor Acc Set#"
    ''Public Const colMPTOLERANCE As String = "Farmer TOLERANCE#"


    Public Const colError As String = "Error"
    Public Const colOK As String = "OK"
    Dim SettBankIFSCCodeValidateByService As Boolean = False
    Dim FromID As String = "MP-IMP-TMP"
    Dim isVarified As Boolean = False

    Dim settApplyEffectiveStartDate As Boolean = False
    Dim UseVLCUploaderCodeMPUploaderCodeInMCCProcurement As Boolean = False
    Dim SettJanAadharNoMandatory As Boolean = False
    Dim EnableBankFromMaster As Boolean
    Dim IncentiveAccNoMandatoryInMPMaster As Boolean = False

    Dim arrExistCols As New List(Of String)
    Dim arrMandatoryCols As New List(Of String)


#End Region

    Private Sub FrmPrefixImport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        settApplyEffectiveStartDate = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyEffectiveStartDate, clsFixedParameterCode.ApplyEffectiveStartDate, Nothing)) > 0, True, False)
        UseVLCUploaderCodeMPUploaderCodeInMCCProcurement = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.UseVLCUploaderCodeMPUploaderCodeInMCCProcurement, clsFixedParameterCode.UseVLCUploaderCodeMPUploaderCodeInMCCProcurement, Nothing)) = 1, True, False)
        SettJanAadharNoMandatory = (clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.JanAadharNoMandatory, clsFixedParameterCode.JanAadharNoMandatory, Nothing)) > 0)
        EnableBankFromMaster = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.EnableBankFromMaster & "'")) = 0, False, True)
        IncentiveAccNoMandatoryInMPMaster = (clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.IncentiveAccNoMandatoryInMPMaster, clsFixedParameterCode.IncentiveAccNoMandatoryInMPMaster, Nothing)) > 0)
        'btnSave.Visible = MyBase.isUpdateFlag
        SettBankIFSCCodeValidateByService = (clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.BankIFSCCodeValidateByService, clsFixedParameterCode.BankIFSCCodeValidateByService, Nothing)) > 1) ''Means 2 ERP or 3 Service And ERP


    End Sub

    Private Sub RadButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnVerify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVerify.Click
        AllowToSave()
    End Sub

    Function AllowToSave() As Boolean
        Try
            Dim ii As Integer = 0
            Dim jj As Integer = 0
            'Dim qry As String
            If gv1.Rows.Count <= 0 Then
                Throw New Exception("Please select excel Sheet to import")
            End If
            Dim objDefaultTemplate As clsExportTemplate = clsExportTemplate.GetDefaultData(FromID)
            If objDefaultTemplate Is Nothing AndAlso clsCommon.myLen(objDefaultTemplate.Export_Code) <= 0 Then
                Throw New Exception("Please set Default Template")
            End If
            If clsCommon.myLen(txtTemplete.Value) <= 0 Then
                Throw New Exception("Please select Templete")
            Else
                For ii = 0 To gv1.Columns.Count - 1
                    gv1.Columns(ii).Tag = Nothing
                Next
                Dim obj As clsExportTemplate = clsExportTemplate.GetData(txtTemplete.Value, FromID, "", NavigatorType.Current)
                If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Export_Code) > 0) Then
                    If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                        For Each objTr As clsExportTemplateDetail In obj.Arr
                            If clsCommon.myLen(objTr.Column_Header) > 0 Then
                                Dim flag As Boolean = False
                                For ii = 0 To gv1.Columns.Count - 1
                                    If clsCommon.CompairString(gv1.Columns(ii).HeaderText, objTr.Column_Header) = CompairStringResult.Equal Then
                                        arrExistCols.Add(objTr.Column_Name)
                                        gv1.Columns(ii).Name = objTr.Column_Name
                                        gv1.Columns(ii).Tag = objTr
                                        flag = True
                                        Exit For
                                    End If
                                Next
                                If Not flag Then
                                    Throw New Exception("Your Excel sheet Not having Excel column [" + objTr.Column_Header + "] And Original Column [" + objTr.Column_Name + "]")
                                End If
                            End If
                        Next
                    End If
                Else
                    Throw New Exception("Templete Data Not found")
                End If

                obj = clsExportTemplate.GetDefaultData(FromID)
                If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Export_Code) > 0) Then
                    If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                        For Each objTr As clsExportTemplateDetail In obj.Arr
                            If clsCommon.myLen(objTr.Column_Name) > 0 Then
                                arrExistCols.Add(objTr.Column_Name)
                            End If
                        Next
                    End If
                Else
                    Throw New Exception("Please set Default Templete")
                End If
            End If

            clsCommon.ProgressBarPercentShow()

            Try
                For ii = 0 To objDefaultTemplate.Arr.Count - 1
                    clsCommon.ProgressBarPercentUpdate((ii * 100 / gv1.RowCount - 1), "Set Default Values " + clsCommon.myCstr(ii) + "/" + clsCommon.myCstr(objDefaultTemplate.Arr.Count - 1))
                    If arrExistCols.Contains(objDefaultTemplate.Arr(ii).Column_Name) Then
                        For jj = 0 To gv1.Rows.Count - 1
                            gv1.Rows(jj).Cells(objDefaultTemplate.Arr(ii).Column_Name).Value = objDefaultTemplate.Arr(ii).Column_Header
                        Next
                    End If
                Next
                clsCommon.ProgressBarPercentHide()
            Catch ex As Exception
                clsCommon.ProgressBarPercentHide()
                Throw New Exception(ex.Message)
            End Try


            clsCommon.ProgressBarPercentShow()

            isVarified = True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            clsCommon.ProgressBarPercentHide()
        End Try

        Return True
    End Function

    Function CheckMCC(ByVal ii As Integer) As Boolean
        Try
            Dim qry As String = ""
            clsCommon.ProgressBarPercentUpdate((ii * 100 / gv1.RowCount - 1), "Validating " + clsCommon.myCstr(ii) + "/" + clsCommon.myCstr(gv1.RowCount - 1))
            gv1.Rows(ii).Cells(colError).Value = ""
            gv1.Rows(ii).Cells(colOK).Value = 1
            Dim flag As Boolean = True
            If arrExistCols.Contains(clsMasterDefault.colMCCCode) Then
                If clsCommon.myLen(gv1.Rows(ii).Cells(clsMasterDefault.colMCCCode).Value) > 0 Then
                    qry = "select MCC_Code from TSPL_MCC_MASTER where MCC_Code='" + clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCCode).Value) + "'"
                    qry = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                    If clsCommon.myLen(gv1.Rows(ii).Cells(clsMasterDefault.colMCCCode).Value) <= 0 Then
                        Throw New Exception("Invalid " + gv1.Columns(clsMasterDefault.colMCCCode).HeaderText)
                    Else
                        flag = False
                    End If
                End If
            End If
            If arrExistCols.Contains(clsMasterDefault.colMCCName) Then
                qry = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCName).Value)
                If clsCommon.myLen(qry) <= 0 Then
                    Throw New Exception("Mcc Name Can Not Be Left Blank")
                End If
                If clsCommon.myLen(qry) > 50 Then
                    Throw New Exception("Mcc Name Can Not Be Larger Then 50 Charachter")
                End If
                If arrExistCols.Contains(clsMasterDefault.colMCCCode) Then
                    qry = clsDBFuncationality.getSingleValue("select mcc_code from tspl_mcc_master where MCC_NAME='" & qry & "'")
                    If clsCommon.myLen(qry) > 0 Then
                        gv1.Rows(ii).Cells(clsMasterDefault.colMCCCode).Value = qry
                        flag = False
                    End If
                End If
            End If
            ''Means MCC need to create
            If True Then
                If arrExistCols.Contains(clsMasterDefault.colMCCState) Then
                    If clsCommon.myLen(gv1.Rows(ii).Cells(clsMasterDefault.colMCCState).Value) <= 0 AndAlso objCommonVar.ApplyDefaultsInMaster = True Then
                        gv1.Rows(ii).Cells(clsMasterDefault.colMCCState).Value = clsStateMaster.GetDefault()
                    End If
                    If clsCommon.myLen(gv1.Rows(ii).Cells(clsMasterDefault.colMCCState).Value) <= 0 Then
                        Throw New Exception("State Code Can Not Be Left Blank")
                    End If
                    If clsCommon.myLen(gv1.Rows(ii).Cells(clsMasterDefault.colMCCState).Value) > 20 Then
                        Throw New Exception("State Code Can Not Be Larger Then 20 Charachter")
                    End If
                    If clsDBFuncationality.getSingleValue("select count(*) from tspl_state_master where state_code='" & clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCState).Value) & "'") = 0 Then
                        Throw New Exception("State Code Could Not Found In Master")
                    End If
                    If clsDBFuncationality.getSingleValue("select count(*) from tspl_state_master where state_code='" & clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCState).Value) & "'  ") = 0 Then
                        Throw New Exception("Invaid State Code : " & clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCState).Value) & Environment.NewLine)
                    End If
                End If
                If arrExistCols.Contains(clsMasterDefault.colMCCType) Then
                    qry = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCType).Value)
                    If clsCommon.myLen(qry) <= 0 AndAlso objCommonVar.ApplyDefaultsInMaster Then
                        qry = "Co. Owned"
                    End If
                    If clsCommon.myLen(qry) <= 0 Then
                        Throw New Exception("MCC Type Can Not Be Left Blank")
                    End If
                    If clsCommon.CompairString(qry, "Co. Owned") = CompairStringResult.Equal Or clsCommon.CompairString(qry, "Co. Leased") = CompairStringResult.Equal Or clsCommon.CompairString(qry, "Chilling Basis") = CompairStringResult.Equal Or clsCommon.CompairString(qry, "Federation") = CompairStringResult.Equal Or clsCommon.CompairString(qry, "PPP") = CompairStringResult.Equal Or clsCommon.CompairString(qry, "IKP") = CompairStringResult.Equal Or clsCommon.CompairString(qry, "MPCS") = CompairStringResult.Equal Then
                        If arrExistCols.Contains(clsMasterDefault.colMCCChillingVendorCode) Then
                            If clsCommon.myLen(gv1.Rows(ii).Cells(clsMasterDefault.colMCCChillingVendorCode).Value) <= 0 AndAlso (clsCommon.CompairString(qry, "Chilling Basis") = CompairStringResult.Equal Or clsCommon.CompairString(qry, "Co. Leased") = CompairStringResult.Equal) Then
                                Throw New Exception("When MCC type is Chilling Basis/Co. Leased, It Must be Specified the chilling Vendor Code ")
                            End If
                        End If
                    Else
                        Throw New Exception("MCC Type Can be Either of Co. Owned/Co. Leased/Chilling Basis/Federation/PPP/IKP/MPCS ")
                    End If
                    gv1.Rows(ii).Cells(clsMasterDefault.colMCCType).Value = qry
                End If
                If arrExistCols.Contains(clsMasterDefault.colMCCAddress1) Then
                    qry = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCAddress1).Value)
                    If clsCommon.myLen(qry) <= 0 Then
                        Throw New Exception("Address1 Can Not Be Left Blank")
                    End If
                    If clsCommon.myLen(qry) > 50 Then
                        Throw New Exception("Address1 Can Not Be Larger Then 50 Charachter")
                    End If
                End If
                If arrExistCols.Contains(clsMasterDefault.colMCCPinCode) Then
                    qry = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCPinCode).Value)
                    If clsCommon.myLen(qry) > 0 AndAlso (clsCommon.myLen(qry) < 6 Or clsCommon.myLen(qry) > 6) Then
                        Throw New Exception("Pin Code Must be 6 Char Length")
                    End If
                End If
                If arrExistCols.Contains(clsMasterDefault.colMCCCity) Then
                    qry = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCCity).Value)
                    If clsCommon.myLen(qry) <= 0 AndAlso objCommonVar.ApplyDefaultsInMaster = True Then
                        qry = clsCityMaster.GetDefault()
                    End If
                    If clsCommon.myLen(qry) <= 0 Then
                        Throw New Exception("City Code Can Not Be Left Blank")
                    End If
                    If clsCommon.myLen(qry) > 20 Then
                        Throw New Exception("City Code Can Not Be Larger Then 20 Charachter")
                    End If
                    qry = clsDBFuncationality.getSingleValue("select city_code from tspl_city_master where city_code='" & qry & "'")
                    If clsCommon.myLen(qry) <= 0 Then
                        Throw New Exception("City Code Could Not Found In Master")
                    End If
                    If arrExistCols.Contains(clsMasterDefault.colMCCState) Then
                        If clsDBFuncationality.getSingleValue("select count(*) from tspl_city_master where city_code='" & qry & "' and state_code='" & clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCState).Value) & "'") = 0 Then
                            Throw New Exception("Invaid City Code : " & qry & " Against State Code: " & clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCState).Value) & Environment.NewLine & " This City Is not Mapped With Specified State First Map it in City Master ")
                        End If
                    End If
                    gv1.Rows(ii).Cells(clsMasterDefault.colMCCCity).Value = qry
                End If
                If arrExistCols.Contains(clsMasterDefault.colMCCCountry) Then
                    qry = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCCountry).Value)
                    If clsCommon.myLen(qry) <= 0 AndAlso objCommonVar.ApplyDefaultsInMaster = True Then
                        qry = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 COUNTRY_CODE from TSPL_COUNTRY_MASTER"))
                    End If
                    If clsCommon.myLen(qry) <= 0 Then
                        Throw New Exception("Country Code Can Not Be Left Blank")
                    End If
                    If clsCommon.myLen(qry) > 20 Then
                        Throw New Exception("Country Code Can Not Be Larger Then 20 Charachter")
                    End If
                    If clsDBFuncationality.getSingleValue("select count(*) from tspl_country_master where country_code='" & qry & "'") = 0 Then
                        Throw New Exception("Country Code Could Not Found In Master")
                    End If
                    gv1.Rows(ii).Cells(clsMasterDefault.colMCCCountry).Value = qry
                End If
                If arrExistCols.Contains(clsMasterDefault.colMCCEmail) Then
                    If clsCommon.myLen(gv1.Rows(ii).Cells(clsMasterDefault.colMCCEmail).Value) > 0 Then
                        Dim check As System.Text.RegularExpressions.Match = System.Text.RegularExpressions.Regex.Match(gv1.Rows(ii).Cells(clsMasterDefault.colMCCEmail).Value, "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")
                        If check.Success Then
                            gv1.Rows(ii).Cells(clsMasterDefault.colMCCEmail).Value = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCEmail).Value)
                        Else
                            Throw New Exception("Email Is In Invalid Format. It Shoud Be as UserName@Domain Format ")
                        End If
                    End If
                End If
                If arrExistCols.Contains(clsMasterDefault.colMCCDripSaver) Then
                    If clsCommon.myLen(gv1.Rows(ii).Cells(clsMasterDefault.colMCCDripSaver).Value) <= 0 Then
                        gv1.Rows(ii).Cells(clsMasterDefault.colMCCDripSaver).Value = "No"
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCDripSaver).Value), "Yes") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCDripSaver).Value), "No") = CompairStringResult.Equal Then
                    Else
                        Throw New Exception("Drip Saver is Manadatory, and it must be Either Yes Or No")
                    End If

                End If
                If arrExistCols.Contains(clsMasterDefault.colMCCCanWasher) Then
                    qry = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCCanWasher).Value)
                    If clsCommon.myLen(qry) <= 0 Then
                        qry = "No"
                    ElseIf clsCommon.CompairString(qry, "Yes") = CompairStringResult.Equal Or clsCommon.CompairString(qry, "No") = CompairStringResult.Equal Then
                    Else
                        Throw New Exception("Can Wahser is Manadatory, and it must be Either Yes Or No")
                    End If
                    gv1.Rows(ii).Cells(clsMasterDefault.colMCCCanWasher).Value = qry

                End If
                If arrExistCols.Contains(clsMasterDefault.colMCCCanScrubber) Then
                    qry = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCCanScrubber).Value)
                    If clsCommon.myLen(qry) <= 0 Then
                        qry = "No"
                    ElseIf clsCommon.CompairString(qry, "Yes") = CompairStringResult.Equal Or clsCommon.CompairString(qry, "No") = CompairStringResult.Equal Then
                    Else
                        Throw New Exception("Can Scrubber is Manadatory, and it must be Either Yes Or No")
                    End If
                    gv1.Rows(ii).Cells(clsMasterDefault.colMCCCanScrubber).Value = qry
                End If
                If arrExistCols.Contains(clsMasterDefault.colMCCETP) Then
                    qry = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCETP).Value)
                    If clsCommon.myLen(qry) <= 0 Then
                        qry = "No"
                    ElseIf clsCommon.CompairString(qry, "Yes") = CompairStringResult.Equal Or clsCommon.CompairString(qry, "No") = CompairStringResult.Equal Then
                    Else
                        Throw New Exception("ETP is Manadatory, and it must be Either Yes Or No")
                    End If
                    gv1.Rows(ii).Cells(clsMasterDefault.colMCCETP).Value = qry
                End If
                If arrExistCols.Contains(clsMasterDefault.colMCCEarthing) Then
                    qry = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCEarthing).Value)
                    If clsCommon.myLen(qry) <= 0 Then
                        qry = "No"
                    ElseIf clsCommon.CompairString(qry, "Yes") = CompairStringResult.Equal Or clsCommon.CompairString(qry, "No") = CompairStringResult.Equal Then
                    Else
                        Throw New Exception("Earthing is Manadatory, and it must be Either Yes Or No")
                    End If
                    gv1.Rows(ii).Cells(clsMasterDefault.colMCCEarthing).Value = qry
                End If
                If arrExistCols.Contains(clsMasterDefault.colMCCBoiler) Then
                    qry = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCBoiler).Value)
                    If clsCommon.myLen(qry) <= 0 Then
                        qry = "No"
                    ElseIf clsCommon.CompairString(qry, "Yes") = CompairStringResult.Equal Or clsCommon.CompairString(qry, "No") = CompairStringResult.Equal Then
                    Else
                        Throw New Exception("Boiler is Manadatory, and it must be Either Yes Or No")
                    End If
                    gv1.Rows(ii).Cells(clsMasterDefault.colMCCBoiler).Value = qry
                End If
                If arrExistCols.Contains(clsMasterDefault.colMCCAgreement_Status) Then
                    qry = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCAgreement_Status).Value)
                    If clsCommon.myLen(qry) <= 0 Then
                        qry = "NO"
                    End If
                    If clsCommon.CompairString(qry, "YES") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(qry, "NO") <> CompairStringResult.Equal Then
                        Throw New Exception("Status Of Agreement Should Be Either YES Or NO")
                    End If
                    gv1.Rows(ii).Cells(clsMasterDefault.colMCCAgreement_Status).Value = qry
                End If
                If arrExistCols.Contains(clsMasterDefault.colMCCAgreement_Date) Then
                    gv1.Rows(ii).Cells(clsMasterDefault.colMCCAgreement_Date).Value = clsCommon.GETSERVERDATE()
                End If
                If arrExistCols.Contains(clsMasterDefault.colMCCAgreementExpiryDate) Then
                    gv1.Rows(ii).Cells(clsMasterDefault.colMCCAgreementExpiryDate).Value = clsCommon.GETSERVERDATE()
                End If
                If arrExistCols.Contains(clsMasterDefault.colMCCSecurity_Status) Then
                    qry = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCSecurity_Status).Value)
                    If clsCommon.myLen(qry) <= 0 Then
                        qry = "NO"
                    End If
                    If clsCommon.CompairString(qry, "YES") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(qry, "NO") <> CompairStringResult.Equal Then
                        Throw New Exception("Status Of Security Should Be Either YES Or NO")
                    End If
                    gv1.Rows(ii).Cells(clsMasterDefault.colMCCSecurity_Status).Value = qry
                End If
                If arrExistCols.Contains(clsMasterDefault.colMCCLocSegmentCode) Then
                    qry = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCLocSegmentCode).Value)
                    If clsCommon.myLen(qry) <= 0 Then
                        qry = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 Segment_code from TSPL_GL_SEGMENT_CODE where seg_no=7 and len(Segment_code)>0 "))
                    End If
                    If clsCommon.myLen(qry) <= 0 Then
                        Throw New Exception("Please Provide " + gv1.Columns(clsMasterDefault.colMCCLocSegmentCode).HeaderText)
                    End If
                    gv1.Rows(ii).Cells(clsMasterDefault.colMCCLocSegmentCode).Value = qry
                End If
                If arrExistCols.Contains(clsMasterDefault.colMCCPaymentCycle) Then
                    qry = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCLocSegmentCode).Value)
                    If clsCommon.myLen(qry) <= 0 AndAlso objCommonVar.ApplyDefaultsInMaster = True Then
                        qry = clsPaymentCycleMaster.GetDefault()
                    Else
                        qry = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCPaymentCycle).Value)
                    End If
                    gv1.Rows(ii).Cells(clsMasterDefault.colMCCPaymentCycle).Value = qry
                End If

                If arrExistCols.Contains(clsMasterDefault.colMCCMorningShiftOpeningTime) Then
                    qry = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCMorningShiftOpeningTime).Value)
                    If clsCommon.myLen(qry) <= 0 Then
                        Throw New Exception("Morning Shift Opening Time Can Not Be Left Blank (Format - 00:00:00 AM)")
                    End If
                End If
                If arrExistCols.Contains(clsMasterDefault.colMCCMorningShiftClosingTime) Then
                    qry = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCMorningShiftClosingTime).Value)
                    If clsCommon.myLen(qry) <= 0 Then
                        Throw New Exception("Morning Shift Closing Time Can Not Be Left Blank (Format - 00:00:00 AM)")
                    End If
                End If
                If arrExistCols.Contains(clsMasterDefault.colMCCEveningShiftOpeningTime) Then
                    qry = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCEveningShiftOpeningTime).Value)
                    If clsCommon.myLen(qry) <= 0 Then
                        Throw New Exception("Evening Shift Opening Time Can Not Be Left Blank (Format - 00:00:00 AM)")
                    End If
                End If
                If arrExistCols.Contains(clsMasterDefault.colMCCEveningShiftClosingTime) Then
                    qry = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCEveningShiftClosingTime).Value)
                    If clsCommon.myLen(qry) <= 0 Then
                        Throw New Exception("Evening Shift Closing Time Can Not Be Left Blank (Format - 00:00:00 AM)")
                    End If
                End If
            End If

        Catch ex As Exception
            gv1.Rows(ii).Cells(colError).Value = ex.Message
            gv1.Rows(ii).Cells(colOK).Value = 0
        End Try
        Return True
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If AllowToSave() Then
                If isVarified Then
                    Dim dtDefaultUOM As New DataTable
                    Dim qry As String = ""
                    If objCommonVar.ApplyDefaultsInMaster = True Then
                        qry = "select * from TSPL_UNIT_MASTER  WHERE IsDefault=1"
                        dtDefaultUOM = clsDBFuncationality.GetDataTable(qry)
                    End If
                    Dim strdate As Date = clsCommon.GETSERVERDATE(Nothing, "dd/MMM/yyyy")
                    If gv1.Rows.Count > 0 Then
                        For ii As Integer = 0 To gv1.RowCount - 1
                            CheckMCC(ii)
                            If clsCommon.myCdbl(gv1.Rows(ii).Cells(colOK).Value) = 1 Then
                                Try
                                    If clsCommon.myLen(gv1.Rows(ii).Cells(clsMasterDefault.colMCCCode).Value) <= 0 Then
                                        qry = "select MCC_Code from TSPL_MCC_MASTER where MCC_Code='" + clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCCode).Value) + "'"
                                        gv1.Rows(ii).Cells(clsMasterDefault.colMCCCode).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                                    End If
                                    If clsCommon.myLen(gv1.Rows(ii).Cells(clsMasterDefault.colMCCCode).Value) <= 0 Then ''Create MCC and its needd master
                                        Dim obj As New clsMccMaster()
                                        obj.State_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCState).Value)
                                        If clsCommon.myLen(obj.State_Code) <= 0 AndAlso objCommonVar.ApplyDefaultsInMaster Then
                                            obj.State_Code = clsStateMaster.GetDefault()
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCBMCC) Then
                                            obj.Is_MCC = IIf(clsCommon.myCdbl(gv1.Rows(ii).Cells(clsMasterDefault.colMCCBMCC).Value) = 0, 0, 1)
                                            If obj.Is_MCC = 1 Then
                                                obj.MCC_Code = clsERPFuncationality.GetNextCode(Nothing, strdate, clsDocType.MCCMaster, "", obj.State_Code, False, True, True)
                                            Else
                                                obj.MCC_Code = clsERPFuncationality.GetNextCode(Nothing, strdate, clsDocType.MCCMaster, clsDocTransactionType.BMCU, obj.State_Code, False, True, True)
                                            End If
                                        Else
                                            obj.MCC_Code = clsERPFuncationality.GetNextCode(Nothing, strdate, clsDocType.MCCMaster, "", obj.State_Code, False, True, True)
                                        End If

                                        If clsCommon.myLen(obj.MCC_Code) <= 0 Then
                                            Throw New Exception("Error In Document Code Genertion")
                                        End If
                                        obj.MCC_Type = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCType).Value)
                                        If clsCommon.myLen(obj.MCC_Type) <= 0 Then
                                            obj.MCC_Type = "Co. Owned"
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCChillingVendorCode) Then
                                            obj.Chilling_Vendor = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCChillingVendorCode).Value)
                                        End If
                                        obj.MCC_NAME = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCName).Value)
                                        obj.Add1 = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCAddress1).Value)
                                        If arrExistCols.Contains(clsMasterDefault.colMCCAddress2) Then
                                            obj.Add2 = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCAddress2).Value)
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCTehsil) Then
                                            obj.Tehsil = gv1.Rows(ii).Cells(clsMasterDefault.colMCCTehsil).Value
                                        End If
                                        obj.Pin_code = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCPinCode).Value)
                                        obj.City_code = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCCity).Value)
                                        If clsCommon.myLen(obj.City_code) < objCommonVar.ApplyDefaultsInMaster Then
                                            obj.City_code = clsCityMaster.GetDefault()
                                        End If
                                        obj.Country_code = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCCountry).Value)
                                        If clsCommon.myLen(obj.Country_code) < objCommonVar.ApplyDefaultsInMaster Then
                                            obj.Country_code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 COUNTRY_CODE from TSPL_COUNTRY_MASTER"))
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCTelphone) Then
                                            obj.Telphone = gv1.Rows(ii).Cells(clsMasterDefault.colMCCTelphone).Value
                                        End If
                                        obj.Email = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCEmail).Value)
                                        If arrExistCols.Contains(clsMasterDefault.colMCCFax) Then
                                            obj.Fax = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCFax).Value)
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMccSuperArea) Then
                                            obj.MCC_Area = clsCommon.myCdbl(gv1.Rows(ii).Cells(clsMasterDefault.colMccSuperArea).Value)
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCAreaOfStore) Then
                                            obj.Area_Of_Store = clsCommon.myCdbl(gv1.Rows(ii).Cells(clsMasterDefault.colMCCAreaOfStore).Value)
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCAreaOfOffice) Then
                                            obj.Area_Of_Office = clsCommon.myCdbl(gv1.Rows(ii).Cells(clsMasterDefault.colMCCAreaOfOffice).Value)
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCOpenAreaForTanker) Then
                                            obj.Open_Area_For_tanker = clsCommon.myCdbl(gv1.Rows(ii).Cells(clsMasterDefault.colMCCOpenAreaForTanker).Value)
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCAreaOfLab) Then
                                            obj.Area_Of_LAB = clsCommon.myCdbl(gv1.Rows(ii).Cells(clsMasterDefault.colMCCAreaOfLab).Value)
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCTotalStorageCapacity) Then
                                            obj.Total_Storage_capacity = clsCommon.myCdbl(gv1.Rows(ii).Cells(clsMasterDefault.colMCCTotalStorageCapacity).Value)
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCAreaOfReceivingDock) Then
                                            obj.Area_Of_Receiving_DOCK = clsCommon.myCdbl(gv1.Rows(ii).Cells(clsMasterDefault.colMCCAreaOfReceivingDock).Value)
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCFssaiNo) Then
                                            obj.FSSAI_NO = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCFssaiNo).Value)
                                        End If
                                        obj.DripSaver = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCDripSaver).Value)
                                        obj.CanWasher = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCCanWasher).Value)
                                        obj.CanScrubber = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCCanScrubber).Value)
                                        obj.ETP = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCETP).Value)
                                        obj.Earthing = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCEarthing).Value)
                                        obj.Boiler = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCBoiler).Value)
                                        If arrExistCols.Contains(clsMasterDefault.colMCCApplyFailedSample) Then
                                            obj.Failed_Sample_Apply = (clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCApplyFailedSample).Value), "Y") = CompairStringResult.Equal)
                                            If obj.Failed_Sample_Apply Then
                                                If arrExistCols.Contains(clsMasterDefault.colMCCFailedSampleFAT) Then
                                                    obj.Failed_Sample_FAT = clsCommon.myCDecimal(gv1.Rows(ii).Cells(clsMasterDefault.colMCCFailedSampleFAT).Value)
                                                    If obj.Failed_Sample_FAT <= 0 Then
                                                        Throw New Exception("Please provide Failed Sample FAT %")
                                                    End If
                                                End If
                                                If arrExistCols.Contains(clsMasterDefault.colMCCFailedSampleSNF) Then
                                                    obj.Failed_Sample_SNF = clsCommon.myCDecimal(gv1.Rows(ii).Cells(clsMasterDefault.colMCCFailedSampleSNF).Value)
                                                    If obj.Failed_Sample_SNF <= 0 Then
                                                        Throw New Exception("Please provide Failed Sample SNF %")
                                                    End If
                                                End If
                                            End If
                                        End If



                                        obj.agreemnt = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCAgreement_Status).Value)
                                        obj.agrmnt_date = clsCommon.myCDate(gv1.Rows(ii).Cells(clsMasterDefault.colMCCAgreement_Date).Value)
                                        obj.expired_date = clsCommon.myCDate(gv1.Rows(ii).Cells(clsMasterDefault.colMCCAgreementExpiryDate).Value)


                                        obj.secutiy = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCSecurity_Status).Value)
                                        If arrExistCols.Contains(clsMasterDefault.colMCCCheque_Amt) Then
                                            obj.chq_amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(clsMasterDefault.colMCCCheque_Amt).Value)
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCCheque_No) Then
                                            obj.chq_no = clsCommon.myCdbl(gv1.Rows(ii).Cells(clsMasterDefault.colMCCCheque_No).Value)
                                        End If
                                        If clsCommon.myLen(obj.chq_no) > 0 Then
                                            If arrExistCols.Contains(clsMasterDefault.colMCCCheque_Date) Then
                                                Try
                                                    obj.chq_date = clsCommon.myCDate(gv1.Rows(ii).Cells(clsMasterDefault.colMCCCheque_Date).Value)
                                                Catch ex As Exception
                                                    obj.chq_date = clsCommon.GETSERVERDATE()
                                                End Try

                                            End If
                                        End If

                                        If clsCommon.CompairString(obj.secutiy, "YES") = CompairStringResult.Equal AndAlso (clsCommon.myCdbl(obj.chq_amt) <= 0 Or clsCommon.myLen(obj.chq_no) <= 0) Then
                                            Throw New Exception("Please Fill Cheque Amount And Cheque No./Date")
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCIndustryType) Then
                                            obj.industry_type = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCIndustryType).Value)
                                        End If

                                        If arrExistCols.Contains(clsMasterDefault.colMCCMonthlyProvision) Then
                                            If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCMonthlyProvision).Value), "Y") = CompairStringResult.Equal Then
                                                obj.is_Chilling_Provision_Monthly = True
                                            Else
                                                obj.is_Chilling_Provision_Monthly = False
                                            End If
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCChillingCharges) Then
                                            obj.chilling_rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(clsMasterDefault.colMCCChillingCharges).Value)
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCChillingOnQty) Then
                                            obj.chilling_qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(clsMasterDefault.colMCCChillingOnQty).Value)
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCChillingOn) Then
                                            obj.chilling_kg_ltr = clsCommon.myCdbl(gv1.Rows(ii).Cells(clsMasterDefault.colMCCChillingOn).Value)
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCChillingMinGuaranteedAvgQty) Then
                                            obj.chilling_assur_qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(clsMasterDefault.colMCCChillingMinGuaranteedAvgQty).Value)
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCChillingMinGuaranteedPeriod) Then
                                            obj.chilling_assur_period = clsCommon.myCdbl(gv1.Rows(ii).Cells(clsMasterDefault.colMCCChillingMinGuaranteedPeriod).Value)
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCChillingStartingDate) Then
                                            If clsCommon.myLen(gv1.Rows(ii).Cells(clsMasterDefault.colMCCChillingStartingDate).Value) > 0 Then
                                                obj.Chilling_Period_Starting_Date = clsCommon.GetPrintDate(gv1.Rows(ii).Cells(clsMasterDefault.colMCCChillingStartingDate).Value, "dd-MMM-yyyy")
                                            End If
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCRateofLeaseCharges) Then
                                            obj.lease_rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(clsMasterDefault.colMCCRateofLeaseCharges).Value)
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCChillingOnUOMHandledDispatched) Then
                                            obj.Unit_ChillingOnQty = IIf(clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCChillingOnUOMHandledDispatched).Value) = "Handled", "H", "D")
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCChillingOnUOMKGLTR) Then
                                            obj.Unit_ChillingOn = IIf(clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCChillingOnUOMKGLTR).Value) = "KG", "K", "L")
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCChillingMinGuaranteedPeriodUOM) Then
                                            obj.Unit_ChillingMinGuaranteePeriod = IIf(clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCChillingMinGuaranteedPeriodUOM).Value) = "Day", "D", IIf(clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCChillingMinGuaranteedPeriodUOM).Value) = "Month", "M", "Y"))
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCRateofLeasedChargesUOM) Then
                                            obj.Unit_RateOfLeasedCharges = IIf(clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCRateofLeasedChargesUOM).Value) = "Day", "D", IIf(clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCRateofLeasedChargesUOM).Value) = "Month", "M", "Y"))
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCAreaofStoreUOM) Then
                                            obj.Unit_AreaOfStore = IIf(clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCAreaofStoreUOM).Value) = "Sq. Mt.", "M", "F")
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCAreaOfReceivingDockUOM) Then
                                            obj.Unit_AreaOfReceivingDock = IIf(clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCAreaOfReceivingDockUOM).Value) = "Sq. Mt.", "M", "F")
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCAreaOfOfficeUOM) Then
                                            obj.Unit_AreaOfOffice = IIf(clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCAreaOfOfficeUOM).Value) = "Sq. Mt.", "M", "F")
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCAreaOfLabUOM) Then
                                            obj.Unit_AreaOfLab = IIf(clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCAreaOfLabUOM).Value) = "Sq. Mt.", "M", "F")
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCOpenAreaForTankerUOM) Then
                                            obj.Unit_OpenAreaForTankerMovement = IIf(clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCOpenAreaForTankerUOM).Value) = "Sq. Mt.", "M", "F")
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMccSuperAreaUOM) Then
                                            obj.Unit_MccSuperArea = IIf(clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMccSuperAreaUOM).Value) = "Sq. Mt.", "M", "F")
                                        End If

                                        If arrExistCols.Contains(clsMasterDefault.colMCCWeighingMachine) Then
                                            obj.Weighing_Machine = IIf(clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCWeighingMachine).Value), "Prompt") = CompairStringResult.Equal, "P", IIf(clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCWeighingMachine).Value), "Delta") = CompairStringResult.Equal, "D", IIf(clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCWeighingMachine).Value), "Panasonic") = CompairStringResult.Equal, "B", IIf(clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCWeighingMachine).Value), "Supertech") = CompairStringResult.Equal, "S", "C"))))
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCSampleMachine) Then
                                            obj.Sample_Machine = IIf(clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCSampleMachine).Value), "Kanha") = CompairStringResult.Equal, "K", IIf(clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCSampleMachine).Value), "Everest New") = CompairStringResult.Equal, "N", "E"))
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCWeighingComPort) Then
                                            obj.Weighing_Comport = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCWeighingComPort).Value)
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCSampleComPort) Then
                                            obj.Sample_comport = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCSampleComPort).Value)
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCPaymentCycle) Then
                                            If clsCommon.myLen(obj.Payment_Cycle) <= 0 AndAlso objCommonVar.ApplyDefaultsInMaster = True Then
                                                obj.Payment_Cycle = clsPaymentCycleMaster.GetDefault()
                                            Else
                                                obj.Payment_Cycle = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCPaymentCycle).Value)
                                            End If
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCShiftMorningOpeningTime) Then
                                            obj.Shift_Opening_Time = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCShiftMorningOpeningTime).Value)
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCShiftMorningClosingTime) Then
                                            obj.Shift_Closing_Time = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCShiftMorningClosingTime).Value)
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCShiftEveningOpeningTime) Then
                                            obj.Shift_Eve_Opening_Time = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCShiftEveningOpeningTime).Value)
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCShiftEveningClosingTime) Then
                                            obj.Shift_Eve_Closing_Time = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCShiftEveningClosingTime).Value)
                                        End If

                                        If arrExistCols.Contains(clsMasterDefault.colMCCRequiredGateEntry) Then
                                            obj.is_Reuired_Gate_Entry = IIf(clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCRequiredGateEntry).Value), "Yes") = CompairStringResult.Equal, True, False)
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCRM) Then
                                            obj.EMP_CODE = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCRM).Value)
                                        End If
                                        obj.MCC_Code_VLC_Uploader = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCUploaderCode).Value)
                                        obj.Loc_Segment_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCLocSegmentCode).Value)
                                        If clsCommon.myLen(obj.Loc_Segment_Code) <= 0 AndAlso objCommonVar.ApplyDefaultsInMaster Then
                                            obj.Loc_Segment_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 Segment_code from TSPL_GL_SEGMENT_CODE where seg_no=7 and len(Segment_code)>0 "))
                                        End If
                                        If True Then
                                            Dim isValidSegmentcode As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("Select Count (*) from TSPL_GL_SEGMENT_CODE where Seg_No='7' and Segment_code = '" + obj.Loc_Segment_Code + "'"))
                                            If isValidSegmentcode = False AndAlso objCommonVar.ApplyDefaultsInMaster = True Then
                                                'Create Segment code
                                                Dim coll As New Hashtable()
                                                coll = New Hashtable()
                                                clsCommon.AddColumnsForChange(coll, "Seg_No", "7")
                                                clsCommon.AddColumnsForChange(coll, "Segment_Code", obj.Loc_Segment_Code)
                                                clsCommon.AddColumnsForChange(coll, "Segment_Name", "Location")
                                                clsCommon.AddColumnsForChange(coll, "Description", obj.MCC_NAME)
                                                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"))
                                                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                                                clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"))
                                                clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
                                                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                                                clsCommon.AddColumnsForChange(coll, "GIT", "N")
                                                clsCommon.AddColumnsForChange(coll, "STATE_CODE", obj.State_Code)
                                                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GL_SEGMENT_CODE", OMInsertOrUpdate.Insert, "")

                                            ElseIf isValidSegmentcode = False Then
                                                Throw New Exception("Invalid Loc Segment Code.")
                                            End If
                                        End If

                                        'Create GL Security
                                        qry = "select count(User_Code) from TSPL_GL_SEGMENT_PERMISSION where User_Code='" + objCommonVar.CurrentUserCode + "' and GL_Segment='7' and Segment_code = '" + obj.Loc_Segment_Code + "'"
                                        Dim check1 As Integer = CInt(clsDBFuncationality.getSingleValue(qry))
                                        If check1 <= 0 AndAlso objCommonVar.ApplyDefaultsInMaster = True Then
                                            Dim coll As New Hashtable()
                                            coll = New Hashtable()
                                            clsCommon.AddColumnsForChange(coll, "User_Code", objCommonVar.CurrentUserCode)
                                            clsCommon.AddColumnsForChange(coll, "GL_Segment", "7")
                                            clsCommon.AddColumnsForChange(coll, "Segment_Code", obj.Loc_Segment_Code)
                                            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"))
                                            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                                            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"))
                                            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
                                            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                                            clsCommon.AddColumnsForChange(coll, "Default_Segment", "N")
                                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GL_SEGMENT_PERMISSION", OMInsertOrUpdate.Insert, "")
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCBMCC) Then
                                            obj.Is_MCC = IIf(clsCommon.myCdbl(gv1.Rows(ii).Cells(clsMasterDefault.colMCCBMCC).Value) = 0, 0, 1)
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCIsTruckSheetMandatory) Then
                                            obj.Is_Truck_Sheet = IIf(clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCIsTruckSheetMandatory).Value), "Yes") = CompairStringResult.Equal, 1, 0)
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCAllowAutoMilkIn) Then
                                            obj.AllowAutoMilkIn = clsCommon.myCdbl(gv1.Rows(ii).Cells(clsMasterDefault.colMCCAllowAutoMilkIn).Value)
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCAutoIn_Location) Then
                                            obj.AutoIn_Location = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCAutoIn_Location).Value)
                                            qry = "select 1  from TSPL_LOCATION_MASTER where Location_Code='" + obj.AutoIn_Location + "' and Location_Category='MCC'"
                                            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                                            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                                                If arrExistCols.Contains(clsMasterDefault.colMCCSILOIn_Location) Then
                                                    obj.SILOIn_Location = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCSILOIn_Location).Value)
                                                End If
                                            Else
                                                obj.SILOIn_Location = ""
                                            End If

                                            If clsCommon.myCdbl(obj.AllowAutoMilkIn) = 1 Then
                                                If clsCommon.myLen(gv1.Rows(ii).Cells(clsMasterDefault.colMCCAutoIn_Location).Value) <= 0 Then
                                                    Throw New Exception("Allow auto Milk is true So Auto In Location cannot be blank")
                                                End If
                                                If arrExistCols.Contains(clsMasterDefault.colMCCSILOIn_Location) Then
                                                    If clsCommon.myLen(gv1.Rows(ii).Cells(clsMasterDefault.colMCCSILOIn_Location).Value) <= 0 Then
                                                        Throw New Exception("Allow auto Milk is true So Silo In Location cannot be blank")
                                                    End If
                                                End If
                                            End If
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCApplyReceiptWeightTolerance) Then
                                            obj.Receipt_Weight_tolerance_Apply = IIf(clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCApplyReceiptWeightTolerance).Value), "Y") = CompairStringResult.Equal, True, False)
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCReceiptWeightToleranceValue) Then
                                            obj.Receipt_Weight_tolerance_Value = clsCommon.myCdbl(gv1.Rows(ii).Cells(clsMasterDefault.colMCCReceiptWeightToleranceValue).Value)
                                        End If

                                        If obj.Receipt_Weight_tolerance_Apply Then
                                            If obj.Receipt_Weight_tolerance_Value < 0 Then
                                                Throw New Exception("Value of ReceiptWeightToleranceValue can't be -ve")
                                            End If
                                        End If

                                        If arrExistCols.Contains(clsMasterDefault.colMCCCommissionRate) Then
                                            obj.Commission_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(clsMasterDefault.colMCCCommissionRate).Value)
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCCommissionMinimumShiftInPaymentCycle) Then
                                            obj.Commission_Minimum_Shift_In_Payment_Cycle = clsCommon.myCdbl(gv1.Rows(ii).Cells(clsMasterDefault.colMCCCommissionMinimumShiftInPaymentCycle).Value)
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCCommissionMinimumQtyInShift) Then
                                            obj.Commission_Minimum_Qty_In_Shift = clsCommon.myCdbl(gv1.Rows(ii).Cells(clsMasterDefault.colMCCCommissionMinimumQtyInShift).Value)
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCCommissionNoOfPaymentCycleForNewVSP) Then
                                            obj.Commission_No_Of_Payment_Cycle_For_New_VSP = clsCommon.myCdbl(gv1.Rows(ii).Cells(clsMasterDefault.colMCCCommissionNoOfPaymentCycleForNewVSP).Value)
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCDeductionMinimumFATPer) Then
                                            obj.Deduction_Minimum_FAT_Per = clsCommon.myCdbl(gv1.Rows(ii).Cells(clsMasterDefault.colMCCDeductionMinimumFATPer).Value)
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCDeductionMinimumSNFPer) Then
                                            obj.Deduction_Minimum_SNF_Per = clsCommon.myCdbl(gv1.Rows(ii).Cells(clsMasterDefault.colMCCDeductionMinimumSNFPer).Value)

                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCDeductionNoOfPaymentCycleForNewVSP) Then
                                            obj.Deduction_No_Of_Payment_Cycle_For_New_VSP = clsCommon.myCdbl(gv1.Rows(ii).Cells(clsMasterDefault.colMCCDeductionNoOfPaymentCycleForNewVSP).Value)
                                        End If

                                        If arrExistCols.Contains(clsMasterDefault.colMCCPlant) Then
                                            obj.Plant_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCPlant).Value)
                                            If clsCommon.myLen(obj.Plant_Code) <= 0 Then
                                                Throw New Exception("Please define Main Plant in location master")
                                            End If
                                            obj.Plant_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select location_code from tspl_location_master where location_code='" + obj.Plant_Code + "'"))
                                            If clsCommon.myLen(obj.Plant_Code) <= 0 Then
                                                Throw New Exception("Invalid location [" + clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCPlant).Value) + "]")
                                            End If
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCMorningShiftOpeningTime) Then
                                            obj.Shift_Opening_Time = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCMorningShiftOpeningTime).Value)
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCMorningShiftClosingTime) Then
                                            obj.Shift_Closing_Time = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCMorningShiftClosingTime).Value)
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCEveningShiftOpeningTime) Then
                                            obj.Shift_Eve_Opening_Time = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCEveningShiftOpeningTime).Value)
                                        End If
                                        If arrExistCols.Contains(clsMasterDefault.colMCCEveningShiftClosingTime) Then
                                            obj.Shift_Eve_Closing_Time = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCEveningShiftClosingTime).Value)
                                        End If

                                        If True Then
                                            Dim objgn As clsGenSetDetail
                                            obj.arrGenSetDetail = New List(Of clsGenSetDetail)
                                            For j As Integer = 0 To obj.NoOfDG
                                                objgn = New clsGenSetDetail()
                                                objgn.Prog_Code = clsUserMgtCode.frmMCCMaster
                                                objgn.Trans_Code = obj.MCC_Code
                                                objgn.Line_No = (j + 1)
                                                objgn.Gen_Set_Desc = "N/A"
                                                obj.arrGenSetDetail.Add(objgn)
                                            Next
                                            Dim objcomp As clsCompressorDetail
                                            obj.arrCompressorDetail = New List(Of clsCompressorDetail)
                                            For j As Integer = 0 To obj.NoOfCompressor
                                                objcomp = New clsCompressorDetail
                                                objcomp.Prog_Code = clsUserMgtCode.frmMCCMaster
                                                objcomp.Trans_Code = obj.MCC_Code
                                                objcomp.Line_No = (j + 1)
                                                objcomp.Compressor_Desc = "N/A"
                                                obj.arrCompressorDetail.Add(objcomp)
                                            Next
                                            Dim objSilo As clsSiloDetail
                                            obj.arrSiloDetail = New List(Of clsSiloDetail)
                                            For j As Integer = 0 To obj.No_Of_SILO
                                                objSilo = New clsSiloDetail()
                                                objSilo.Prog_Code = clsUserMgtCode.frmMCCMaster
                                                objSilo.Trans_Code = obj.MCC_Code
                                                objSilo.Line_No = (j + 1)
                                                objSilo.Silo_Desc = "N/A"
                                                objSilo.Silo_Area = 0
                                                objSilo.Silo_Unit = ""
                                                obj.arrSiloDetail.Add(objSilo)
                                            Next
                                            Dim objmilkpump As clsMilkPumpDetail
                                            obj.arrMilkPumpDetail = New List(Of clsMilkPumpDetail)
                                            For j As Integer = 0 To obj.No_Of_MilkPump
                                                objmilkpump = New clsMilkPumpDetail()
                                                objmilkpump.Prog_Code = clsUserMgtCode.frmMCCMaster
                                                objmilkpump.Trans_Code = obj.MCC_Code
                                                objmilkpump.Line_No = (j + 1)
                                                objmilkpump.Pump_Desc = "N/A"
                                                objmilkpump.Pump_Area = 0
                                                objmilkpump.Pump_Unit = ""
                                                obj.arrMilkPumpDetail.Add(objmilkpump)
                                            Next

                                            Dim objChiller As clsChillerDetail
                                            obj.arrChillerDetail = New List(Of clsChillerDetail)
                                            For j As Integer = 0 To obj.No_Of_Chiller
                                                objChiller = New clsChillerDetail()
                                                ' objChiller = New clsMilkPumpDetail()
                                                objChiller.Prog_Code = clsUserMgtCode.frmMCCMaster
                                                objChiller.Trans_Code = obj.MCC_Code
                                                objChiller.Chiller_Desc = "N/A"
                                                objChiller.Chiller_Brand = ""
                                                objChiller.Chiller_Capacity = 0
                                                obj.arrChillerDetail.Add(objChiller)
                                            Next

                                            Dim objMccUOM As clsMccUOMDetails
                                            obj.ArrUomDetails = New List(Of clsMccUOMDetails)




                                            If objCommonVar.ApplyDefaultsInMaster = True Then
                                                If dtDefaultUOM IsNot Nothing AndAlso dtDefaultUOM.Rows.Count > 0 Then
                                                    objMccUOM = New clsMccUOMDetails()
                                                    objMccUOM.Mcc_Code = obj.MCC_Code
                                                    objMccUOM.UOM_Code = clsCommon.myCstr(dtDefaultUOM.Rows(0)("Unit_code"))
                                                    objMccUOM.UOM_Description = clsCommon.myCstr(dtDefaultUOM.Rows(0)("Unit_Desc"))
                                                    objMccUOM.Stocking_Unit = "Y"
                                                    objMccUOM.Conversion_Factor = clsCommon.myCdbl(dtDefaultUOM.Rows(0)("Conv_Factor"))
                                                    obj.ArrUomDetails.Add(objMccUOM)
                                                End If
                                            ElseIf arrExistCols.Contains(clsMasterDefault.colMCCChillingOnUOMKGLTR) AndAlso clsCommon.myLen(gv1.Rows(ii).Cells(clsMasterDefault.colMCCChillingOnUOMKGLTR).Value) > 0 Then
                                                qry = "select * from TSPL_UNIT_MASTER  WHERE unit_code='" + clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCChillingOnUOMKGLTR).Value) + "'"
                                                Dim dttemp As DataTable = clsDBFuncationality.GetDataTable(qry)
                                                objMccUOM = New clsMccUOMDetails()
                                                objMccUOM.Mcc_Code = obj.MCC_Code
                                                objMccUOM.UOM_Code = clsCommon.myCstr(dttemp.Rows(0)("Unit_code"))
                                                objMccUOM.UOM_Description = clsCommon.myCstr(dttemp.Rows(0)("Unit_Desc"))
                                                objMccUOM.Stocking_Unit = "Y"
                                                objMccUOM.Conversion_Factor = clsCommon.myCdbl(dttemp.Rows(0)("Conv_Factor"))
                                                obj.ArrUomDetails.Add(objMccUOM)

                                            End If

                                            obj.arrChequeDetail = New List(Of clsMCCChequeDetails)


                                            obj.isNewEntry = True
                                            obj.Modified_By = objCommonVar.CurrentUserCode
                                            obj.Modified_Date = clsCommon.GetPrintDate(strdate, "dd/MMM/yyyy")
                                            obj.Comp_Code = objCommonVar.CurrentCompanyCode

                                            obj.Created_By = objCommonVar.CurrentUserCode
                                            obj.Created_Date = clsCommon.GetPrintDate(strdate, "dd/MMM/yyyy")

                                            clsMccMaster.SaveData(obj)
                                        End If
                                    End If

                                    ''Create VSP,VLC,PTM,Route etc 
                                    If True Then
                                        Dim trans As SqlTransaction = Nothing
                                        ''Primary Transport Master
                                        Dim strvendorNo As String = String.Empty
                                        Dim strvendorname1 As String = String.Empty
                                        Dim strvendorname As String = String.Empty
                                        Dim StrVdrNo As String = String.Empty
                                        Dim check As Integer = 0
                                        Dim i2 As Integer = 0
                                        Dim coll As New Hashtable()
                                        Dim strBrachName As String = String.Empty
                                        Dim strIFSCCode As String = String.Empty
                                        Dim strbank As String = String.Empty
                                        Dim statecode As String = String.Empty
                                        Dim state As String = String.Empty
                                        Dim country As String = String.Empty
                                        Dim closing_date As String = String.Empty
                                        Dim strgroupCode As String = String.Empty
                                        Dim strgroupDes As String = String.Empty
                                        Dim CityCode As String = String.Empty
                                        Dim CityName As String = String.Empty
                                        Dim PC_CODE As String = String.Empty
                                        Dim StrTempVSPName As String = String.Empty
                                        ''Try
                                        ''    StrTempVSPName = clsCommon.myCstr(gv1.Rows(ii).Cells(colDCSVSPName).Value).Replace(" ", "")
                                        ''    StrTempVSPName = StrTempVSPName.Replace("'", "")
                                        ''    'strvendorNo = clsCommon.myCstr(gv1.Rows(ii).Cells(colDCSTransporterCode).Value)
                                        ''    'If strvendorNo.Length > 12 Then
                                        ''    '    Throw New Exception("Check the length of Transporter Code,")
                                        ''    'End If

                                        ''    'strvendorname1 = clsCommon.myCstr(gv1.Rows(ii).Cells(colDCSTransporterName).Value)
                                        ''    'strvendorname = strvendorname1.Replace("'", "''")
                                        ''    'If strvendorname.Length > 100 Then
                                        ''    '    Throw New Exception("Length of Transporter Name can not be greater than 100.,")
                                        ''    'End If
                                        ''    'If String.IsNullOrEmpty(strvendorname) AndAlso objCommonVar.ApplyDefaultsInMaster = True Then
                                        ''    '    strvendorname = StrTempVSPName
                                        ''    '    gv1.Rows(ii).Cells(colDCSTransporterCode).Value = StrTempVSPName
                                        ''    '    gv1.Rows(ii).Cells(colDCSTransporterName).Value = StrTempVSPName
                                        ''    'End If
                                        ''    'If String.IsNullOrEmpty(strvendorname) Then
                                        ''    '    Throw New Exception("Transporter Name can not be blank")
                                        ''    'End If
                                        ''    closing_date = System.DateTime.Now.Date

                                        ''    strgroupCode = clsCommon.myCstr(gv1.Rows(ii).Cells(colDCSTransporterGroupCode).Value)
                                        ''    If String.IsNullOrEmpty(strgroupCode) Then
                                        ''        Throw New Exception(" Transporter group code can not be blank")
                                        ''    End If
                                        ''    Dim i As Integer
                                        ''    qry = "select Count(*)from TSPL_VENDOR_GROUP  where Ven_Group_Code ='" + strgroupCode + "'"
                                        ''    i = connectSql.RunScalar(trans, qry)
                                        ''    If i = 0 Then
                                        ''        Throw New Exception("Vendor group code does not exist : " + strgroupCode + "")
                                        ''    Else
                                        ''    End If
                                        ''    If strgroupCode.Length > 12 Then
                                        ''        Throw New Exception("Check the length of Group Code")
                                        ''    End If
                                        ''    strgroupDes = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  group_Desc from TSPL_VENDOR_GROUP  where Ven_Group_Code ='" + strgroupCode + "'", trans))

                                        ''    If arrExistCols.Contains(colDCSState) Then
                                        ''        statecode = clsCommon.myCstr(gv1.Rows(ii).Cells(colDCSState).Value)
                                        ''        check = 0

                                        ''        If clsCommon.myLen(statecode) <= 0 AndAlso objCommonVar.ApplyDefaultsInMaster = True Then
                                        ''            statecode = clsCommon.myCstr(clsStateMaster.GetDefault(trans))
                                        ''        End If
                                        ''        If clsCommon.myLen(statecode) > 0 Then
                                        ''            qry = "select STATE_CODE,STATE_NAME,COUNTRY_CODE from tspl_state_master where  state_code='" + statecode + "'"
                                        ''            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                        ''            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                                        ''                Throw New Exception("State Code Does Not Exist,Please Make Its Master First")
                                        ''            End If
                                        ''            statecode = clsCommon.myCstr(dt.Rows(0)("STATE_CODE"))
                                        ''            state = clsCommon.myCstr(dt.Rows(0)("STATE_NAME"))
                                        ''            country = clsCommon.myCstr(dt.Rows(0)("COUNTRY_CODE"))
                                        ''        End If
                                        ''        gv1.Rows(ii).Cells(colDCSState).Value = statecode
                                        ''    End If

                                        ''    If arrExistCols.Contains(colDCSBankCode) Then
                                        ''        strbank = clsCommon.myCstr(gv1.Rows(ii).Cells(colDCSBankCode).Value)
                                        ''        If strbank.Length > 30 Then
                                        ''            Throw New Exception("Check the length of bank code")
                                        ''        End If
                                        ''    End If
                                        ''    If arrExistCols.Contains(colDCSIFSCCode) Then
                                        ''        strIFSCCode = clsCommon.myCstr(gv1.Rows(ii).Cells(colDCSIFSCCode).Value)
                                        ''    End If
                                        ''    If arrExistCols.Contains(colDCSBranchName) Then
                                        ''        strBrachName = clsCommon.myCstr(gv1.Rows(ii).Cells(colDCSBranchName).Value)
                                        ''        If clsCommon.myLen(strBrachName) > 100 Then
                                        ''            Throw New Exception("Branch Name should be max 100 character")
                                        ''        End If
                                        ''    End If





                                        If objCommonVar.ApplyDefaultsInMaster = True Then
                                            CityCode = clsCommon.myCstr(clsCityMaster.GetDefault(trans))
                                            CityName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select City_Name from TSPL_CITY_MASTER where City_Code='" + CityCode + "'", trans))
                                            PC_CODE = clsCommon.myCstr(clsPaymentCycleMaster.GetDefault(trans))

                                            country = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 COUNTRY_CODE from TSPL_COUNTRY_MASTER", trans))
                                        End If

                                        ''    coll = New Hashtable()
                                        ''    clsCommon.AddColumnsForChange(coll, "Vendor_Name", strvendorname)
                                        ''    clsCommon.AddColumnsForChange(coll, "Closing_Date", closing_date)
                                        ''    clsCommon.AddColumnsForChange(coll, "State", state)
                                        ''    clsCommon.AddColumnsForChange(coll, "Country", country)
                                        ''    clsCommon.AddColumnsForChange(coll, "form_type", "PTM")
                                        ''    clsCommon.AddColumnsForChange(coll, "state_code", statecode, True)
                                        ''    clsCommon.AddColumnsForChange(coll, "City_Code", CityCode, True)
                                        ''    clsCommon.AddColumnsForChange(coll, "City_Code_Desc", CityName, True)
                                        ''    clsCommon.AddColumnsForChange(coll, "PC_CODE", PC_CODE, True)
                                        ''    clsCommon.AddColumnsForChange(coll, "Vendor_Group_Code", strgroupCode)
                                        ''    clsCommon.AddColumnsForChange(coll, "Vendor_Group_Code_Desc", strgroupDes)
                                        ''    clsCommon.AddColumnsForChange(coll, "Start_Date", Nothing, True)
                                        ''    clsCommon.AddColumnsForChange(coll, "End_Date", Nothing, True)
                                        ''    clsCommon.AddColumnsForChange(coll, "is_Head_Load", "F")
                                        ''    clsCommon.AddColumnsForChange(coll, "Status", "N")
                                        ''    clsCommon.AddColumnsForChange(coll, "Onhold", "N")
                                        ''    clsCommon.AddColumnsForChange(coll, "Transporter", "Y")
                                        ''    clsCommon.AddColumnsForChange(coll, "Currency_Code", objCommonVar.BaseCurrencyCode)
                                        ''    clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                                        ''    clsCommon.AddColumnsForChange(coll, "modify_by", objCommonVar.CurrentUserCode)
                                        ''    clsCommon.AddColumnsForChange(coll, "modify_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                                        ''    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                                        ''    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

                                        ''    qry = "select count(*) from TSPL_VENDOR_MASTER  where  Vendor_Name ='" + strvendorname + "' and form_type='PTM' "
                                        ''    i2 = CInt(connectSql.RunScalar(trans, qry))

                                        ''    StrVdrNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Code from TSPL_VENDOR_MASTER  where  Vendor_Name ='" + strvendorname + "' and form_type='PTM'", trans))

                                        ''    If (i2 = 0) Then
                                        ''        StrVdrNo = clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.PTMMASTER, "", "")
                                        ''        clsCommon.AddColumnsForChange(coll, "Vendor_Code", StrVdrNo)
                                        ''        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VENDOR_MASTER", OMInsertOrUpdate.Insert, "", trans)
                                        ''    Else
                                        ''        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VENDOR_MASTER", OMInsertOrUpdate.Update, "vendor_code='" + StrVdrNo + "' and form_type='PTM'", trans)
                                        ''    End If
                                        ''    trans.Commit()

                                        ''Catch ex As Exception
                                        ''    trans.Rollback()
                                        ''    Throw New Exception(ex.Message)
                                        ''End Try
                                        ''end of Primary Transporter Master

                                        ''Primary Transporter Vehiclee Master
                                        'trans = Nothing
                                        'Dim obj As clsfrmPrimaryTransporterVehicalMaster

                                        'obj = New clsfrmPrimaryTransporterVehicalMaster()
                                        'Dim index As Integer = 0

                                        'obj.docno = clsCommon.myCstr(gv1.Rows(ii).Cells(colDCSVehicle).Value)
                                        'If clsCommon.myLen(obj.docno) <= 0 AndAlso objCommonVar.ApplyDefaultsInMaster = True Then
                                        '    gv1.Rows(ii).Cells(colDCSVehicle).Value = StrTempVSPName
                                        '    obj.docno = StrTempVSPName
                                        '    obj.primarycode = StrVdrNo
                                        '    obj.primaryname = StrTempVSPName
                                        'Else
                                        '    obj.primarycode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Code from TSPL_VENDOR_MASTER  where  Vendor_Name ='" + strvendorname + "'", trans))
                                        '    obj.primaryname = strvendorname
                                        'End If

                                        'If clsCommon.myLen(obj.docno) <= 0 Then
                                        '    Throw New Exception("Please Fill Vehicle No.(Code)")
                                        'End If
                                        'If clsCommon.myLen(obj.docno) > 30 Then
                                        '    Throw New Exception("Length of Vehicle No.(Code) Should Not Exceed 30 Characters")
                                        'End If
                                        'If clsCommon.myLen(obj.docno) > 0 Then
                                        '    index = obj.docno.IndexOf(" ")
                                        '    If index > 0 AndAlso index < clsCommon.myLen(obj.docno) Then
                                        '        Throw New Exception("There Should Be No white Space Between Vehicle No.")
                                        '    End If
                                        'End If


                                        'If clsCommon.myLen(obj.primarycode) <= 0 AndAlso clsCommon.myLen(obj.primaryname) <= 0 Then
                                        '    Throw New Exception("Please Fill Primary Transporter Code/Name ")
                                        'End If
                                        'If clsCommon.myLen(obj.primarycode) > 0 Then
                                        '    qry = "select count(*) from tspl_vendor_master where vendor_code='" + obj.primarycode + "' and form_type='PTM'"
                                        '    index = clsDBFuncationality.getSingleValue(qry, trans)

                                        '    If index <= 0 Then
                                        '        qry = "select vendor_code from tspl_vendor_master where vendor_name='" + obj.primaryname + "' and form_type='PTM'"
                                        '        obj.primarycode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

                                        '        If obj.primarycode IsNot Nothing AndAlso clsCommon.myLen(obj.primarycode) <= 0 Then
                                        '            Throw New Exception("Filled Primary Transporter Code Is Invalid Or Does Not Exist")
                                        '        End If
                                        '    End If
                                        'ElseIf clsCommon.myLen(obj.primaryname) > 0 Then
                                        '    qry = "select vendor_code from tspl_vendor_master where vendor_name='" + obj.primaryname + "' and form_type='PTM'"
                                        '    obj.primarycode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

                                        '    If obj.primarycode IsNot Nothing AndAlso clsCommon.myLen(obj.primarycode) <= 0 Then
                                        '        Throw New Exception("Filled Primary Transporter Code/Name Is Invalid Or Does Not Exist")
                                        '    End If
                                        'End If
                                        ''-------------------------------------------------------------

                                        'obj.mcccode = clsCommon.myCstr(gv1.Rows(ii).Cells(colMCCCode).Value)
                                        'obj.mccname = clsCommon.myCstr(gv1.Rows(ii).Cells(colMCCName).Value).Replace("'", "`")
                                        'If clsCommon.myLen(obj.mcccode) <= 0 AndAlso clsCommon.myLen(obj.mccname) <= 0 Then
                                        '    Throw New Exception("Please Fill MCC Code/Name")
                                        'End If
                                        'If clsCommon.myLen(obj.mcccode) > 0 Then
                                        '    qry = "select count(*) from tspl_mcc_master where mcc_code='" + obj.mcccode + "'"
                                        '    index = clsDBFuncationality.getSingleValue(qry, trans)

                                        '    If index <= 0 Then
                                        '        qry = "select mcc_code from tspl_mcc_master where mcc_name='" + obj.mccname + "'"
                                        '        obj.mcccode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

                                        '        If obj.mcccode IsNot Nothing AndAlso clsCommon.myLen(obj.mcccode) <= 0 Then
                                        '            Throw New Exception("Filled MCC Code Is Invalid Or Does Not Exist")
                                        '        End If
                                        '    End If
                                        'ElseIf clsCommon.myLen(obj.mccname) > 0 Then
                                        '    qry = "select mcc_code from tspl_mcc_master where mcc_name='" + obj.mccname + "'"
                                        '    obj.mcccode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

                                        '    If obj.mcccode IsNot Nothing AndAlso clsCommon.myLen(obj.mcccode) <= 0 Then
                                        '        Throw New Exception("Filled MCC Code/Name Is Invalid Or Does Not Exist")
                                        '    End If
                                        'End If
                                        ''---------------------

                                        ''------------check primary transporter mapped with other mcc-----------------
                                        'Dim checkmcccode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select mcc_code from tspl_primary_vehicle_master where vendor_code='" + obj.primarycode + "'", trans))
                                        'If clsCommon.myLen(checkmcccode) > 0 AndAlso clsCommon.CompairString(checkmcccode, obj.mcccode) <> CompairStringResult.Equal Then
                                        '    Throw New Exception("Filled MCC Code/Name Is Invalid" + Environment.NewLine + "Primary Transporter Code Is Mapped With Other MCC Code i.e (" + checkmcccode + ") ")
                                        'End If
                                        ''------------------------------------------------------------------------
                                        'If arrExistCols.Contains(colDCSPaymentPerKm) Then
                                        '    obj.pricekm = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDCSPaymentPerKm).Value)
                                        'End If
                                        'If arrExistCols.Contains(colDCSVehiclePaymentBasis) Then
                                        '    obj.status = clsCommon.myCstr(gv1.Rows(ii).Cells(colDCSVehiclePaymentBasis).Value)
                                        '    If clsCommon.CompairString(obj.status, "Day/Diesel") = CompairStringResult.Equal Then
                                        '        'If obj.chagrshift <= 0 Then
                                        '        '    Throw New Exception("Please Fill Charges per Day ")
                                        '        'End If
                                        '        'If obj.avgrate <= 0 Then
                                        '        '    Throw New Exception("Please Fill Average KM per Ltr ")
                                        '        'End If
                                        '        'If obj.dieselrate <= 0 Then
                                        '        '    Throw New Exception("Please Fill Rate of Diesel ")
                                        '        'End If
                                        '    ElseIf clsCommon.CompairString(obj.status, "Rental") = CompairStringResult.Equal Then
                                        '        'If obj.RentalAmount <= 0 Then
                                        '        '    Throw New Exception("Please Fill Rental Amount ")
                                        '        'End If
                                        '        'If Not (clsCommon.CompairString(obj.RentalType, "Day") = CompairStringResult.Equal Or clsCommon.CompairString(obj.RentalType, "Month") = CompairStringResult.Equal Or clsCommon.CompairString(obj.RentalType, "Year") = CompairStringResult.Equal) Then
                                        '        '    Throw New Exception("Rental Type should be Day,Month,Year  ")
                                        '        'End If
                                        '    ElseIf clsCommon.CompairString(obj.status, "Rate/Ltr") = CompairStringResult.Equal Then
                                        '        'If obj.Price_Ltr_KG <= 0 Then
                                        '        '    Throw New Exception("Please Fill Price Ltr/KG ")
                                        '        'End If
                                        '        'If Not (clsCommon.CompairString(obj.Rate_Type, "LTR") = CompairStringResult.Equal Or clsCommon.CompairString(obj.RentalType, "KG") = CompairStringResult.Equal) Then
                                        '        '    Throw New Exception("Rate Type should be LTR,KG  ")
                                        '        'End If
                                        '    ElseIf clsCommon.CompairString(obj.status, "Rate/K.M") = CompairStringResult.Equal Then
                                        '        If obj.pricekm <= 0 Then
                                        '            Throw New Exception("Please Fill Rate per KM ")
                                        '        End If
                                        '    ElseIf clsCommon.CompairString(obj.status, "Rental/Diesel") = CompairStringResult.Equal Then
                                        '        'If obj.RentalAmount <= 0 Then
                                        '        '    Throw New Exception("Please Fill Rental Amount ")
                                        '        'End If
                                        '        'If obj.avgrate <= 0 Then
                                        '        '    Throw New Exception("Please Fill Average KM per Ltr ")
                                        '        'End If
                                        '        'If obj.dieselrate <= 0 Then
                                        '        '    Throw New Exception("Please Fill Rate of Diesel ")
                                        '        'End If
                                        '    ElseIf clsCommon.CompairString(obj.status, "KM_Range") = CompairStringResult.Equal Then
                                        '    ElseIf clsCommon.myLen(obj.status) > 0 Then
                                        '        Throw New Exception("Payment method should be Day/Diesel,Rate/K.M,Rate/Ltr,Rental,Rental/Diesel ")
                                        '    End If
                                        'End If


                                        'If arrExistCols.Contains(colDCSVehicleEffectiveStartDate) Then
                                        '    If settApplyEffectiveStartDate = True Then
                                        '        If clsCommon.myLen(gv1.Rows(ii).Cells(colDCSVehicleEffectiveStartDate).Value) <= 0 Then
                                        '            Throw New Exception("Please Fill Vehicle Effective Start Date ")
                                        '        End If
                                        '        obj.Effective_Start_Date = clsCommon.GetPrintDate(gv1.Rows(ii).Cells(colDCSVehicleEffectiveStartDate).Value, "dd/MMM/yyyy")
                                        '    End If
                                        'End If



                                        'qry = "select count(*) from TSPL_Primary_Vehicle_Master where vehicle_code='" + obj.docno + "'"
                                        'check = clsDBFuncationality.getSingleValue(qry, trans)
                                        Dim isNewEntry As Boolean = True
                                        'If check > 0 Then
                                        '    isNewEntry = False
                                        'Else
                                        '    isNewEntry = True
                                        'End If
                                        'If clsCommon.myLen(obj.docno) > 0 Then
                                        '    'trans = clsDBFuncationality.GetTransactin()
                                        '    If clsfrmPrimaryTransporterVehicalMaster.SaveData(obj.docno, isNewEntry, obj) Then
                                        '    Else
                                        '        Throw New Exception("No Data Transfer")
                                        '    End If
                                        'End If


                                        '''----------- end of Primary Transporter Vehicle Master


                                        '' Milk Route Master
                                        'trans = Nothing
                                        'Dim objMRM As clsfrmMilkRouteMaster
                                        'objMRM = New clsfrmMilkRouteMaster()
                                        'clsfrmMilkRouteMaster.arr_VLC_Detail = Nothing
                                        'objMRM.code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDCSRouteCode).Value)
                                        'objMRM = clsfrmMilkRouteMaster.GetData(objMRM.code, Nothing, NavigatorType.Current)
                                        'If objMRM Is Nothing Then
                                        '    objMRM = New clsfrmMilkRouteMaster
                                        'End If
                                        'If clsCommon.myLen(objMRM.code) <= 0 AndAlso objCommonVar.ApplyDefaultsInMaster = True Then
                                        '    objMRM.code = StrTempVSPName
                                        '    objMRM.desc = StrTempVSPName
                                        '    objMRM.vehiclecode = StrTempVSPName
                                        'Else
                                        '    objMRM.code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDCSRouteCode).Value)
                                        '    objMRM.desc = clsCommon.myCstr(gv1.Rows(ii).Cells(colDCSRouteName).Value)
                                        '    objMRM.vehiclecode = clsCommon.myCstr(gv1.Rows(ii).Cells(colDCSVehicle).Value)
                                        'End If

                                        'If clsCommon.myLen(objMRM.desc) <= 0 Or clsCommon.myLen(objMRM.desc) > 150 Then
                                        '    Throw New Exception("Please Fill Route Name(Max. 150 Characters)")
                                        'End If

                                        'check = 0


                                        'objMRM.Active = 1

                                        'If clsCommon.myLen(objMRM.vehiclecode) <= 0 Then
                                        '    Throw New Exception("Please Fill Vehicle Details")
                                        'End If
                                        'If clsCommon.myLen(objMRM.vehiclecode) > 0 Then
                                        '    qry = "select count(*) from tspl_primary_vehicle_master where vehicle_code='" + objMRM.vehiclecode + "'"
                                        '    check = clsDBFuncationality.getSingleValue(qry)
                                        '    If check <= 0 Then
                                        '        Throw New Exception("Filled Vehicle Code Is Invalid Or Does Not Exist in Master")
                                        '    End If
                                        'End If
                                        'objMRM.mcccode = clsCommon.myCstr(gv1.Rows(ii).Cells(colMCCCode).Value)
                                        'objMRM.mccname = clsCommon.myCstr(gv1.Rows(ii).Cells(colMCCName).Value)
                                        'If clsCommon.myLen(objMRM.mcccode) <= 0 AndAlso clsCommon.myLen(objMRM.mccname) <= 0 Then
                                        '    Throw New Exception("Please Fill MCC Details ")
                                        'End If
                                        'If clsCommon.myLen(objMRM.mcccode) > 0 Then
                                        '    qry = "select count(*) from tspl_mcc_master where mcc_code='" + objMRM.mcccode + "'"
                                        '    check = clsDBFuncationality.getSingleValue(qry)

                                        '    If check <= 0 Then
                                        '        qry = "select count(*) from tspl_mcc_master where mcc_name='" + objMRM.mccname + "'"
                                        '        check = clsDBFuncationality.getSingleValue(qry)

                                        '        If check <= 0 Then
                                        '            objMRM.mcccode = ""
                                        '            Throw New Exception("Filled MCC Details Is Invalid Or Does Not Exist,See ")
                                        '        End If
                                        '    End If
                                        'End If
                                        'If clsCommon.myLen(objMRM.mcccode) <= 0 Then
                                        '    qry = "select count(*) from tspl_mcc_master where mcc_name='" + objMRM.mccname + "'"
                                        '    check = clsDBFuncationality.getSingleValue(qry)

                                        '    If check <= 0 Then
                                        '        Throw New Exception("Filled MCC Details Is Invalid Or Does Not Exist,See ")
                                        '    End If
                                        'End If
                                        'If arrExistCols.Contains(colDCSRouteDistance) Then
                                        '    objMRM.kilometer = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDCSRouteDistance).Value)
                                        '    If clsCommon.myLen(objMRM.kilometer) <= 0 Or clsCommon.myCdbl(objMRM.kilometer) <= 0 Then
                                        '        Throw New Exception("Please Fill Route Distance And It Should Be Greater Than Zero(0) ")
                                        '    End If
                                        'End If


                                        'qry = "select count(*) from tspl_mcc_route_master where route_Name='" + objMRM.desc + "'"
                                        'check = clsDBFuncationality.getSingleValue(qry)
                                        'If arrExistCols.Contains(colDCSRouteEffectiveStartDate) Then
                                        '    If settApplyEffectiveStartDate = True Then
                                        '        If clsCommon.myLen(gv1.Rows(ii).Cells(colDCSRouteEffectiveStartDate).Value) <= 0 Then
                                        '            Throw New Exception("Please Fill Route Effective Start Date ")
                                        '        End If
                                        '        objMRM.Effective_Start_Date = clsCommon.GetPrintDate(gv1.Rows(ii).Cells(colDCSRouteEffectiveStartDate).Value, "dd/MMM/yyyy")
                                        '    End If
                                        'End If
                                        ''' Dim isNewEntry As Boolean = True
                                        'If check <= 0 Then
                                        '    isNewEntry = True
                                        'Else
                                        '    isNewEntry = False
                                        'End If

                                        ''trans = clsDBFuncationality.GetTransactin()
                                        'If clsfrmMilkRouteMaster.SaveData(objMRM.code, objMRM, isNewEntry, True) Then
                                        'Else
                                        '    Throw New Exception("No Data Transfer")
                                        'End If
                                        ''end of Milk Route master

                                        ''VSP Master

                                        trans = clsDBFuncationality.GetTransactin()
                                        Try
                                            Dim VlcUploaderCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colDCSUploaderCode).Value)
                                            qry = "select VLC_Code,VLC_Name,VSP_Code from TSPL_VLC_MASTER_HEAD where VLC_Code_VLC_Uploader='" + VlcUploaderCode + "'"
                                            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                                gv1.Rows(ii).Cells(clsMasterDefault.colDCSVLCCode).Value = clsCommon.myCstr(dt.Rows(0)("VLC_Code"))
                                                gv1.Rows(ii).Cells(clsMasterDefault.colDCSVLCName).Value = clsCommon.myCstr(dt.Rows(0)("VLC_Name"))

                                                gv1.Rows(ii).Cells(clsMasterDefault.colDCSVSPCode).Value = clsCommon.myCstr(dt.Rows(0)("VSP_Code"))
                                                gv1.Rows(ii).Cells(clsMasterDefault.colDCSVSPName).Value = clsCommon.myCstr(dt.Rows(0)("VLC_Name"))
                                            End If

                                            strvendorNo = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colDCSVSPCode).Value)
                                            If strvendorNo.Length > 12 Then
                                                Throw New Exception("Check the length of VSP Code,")
                                            End If

                                            If clsCommon.myLen(gv1.Rows(ii).Cells(clsMasterDefault.colDCSVSPName).Value) <= 0 Then
                                                gv1.Rows(ii).Cells(clsMasterDefault.colDCSVSPName).Value = gv1.Rows(ii).Cells(clsMasterDefault.colDCSVLCName).Value
                                            End If
                                            strvendorname1 = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colDCSVSPName).Value)
                                            strvendorname = strvendorname1.Replace("'", "''")
                                            If strvendorname.Length > 100 Then
                                                Throw New Exception("Length of VSP Name can not be greater than 100.,")
                                            End If

                                            If String.IsNullOrEmpty(strvendorname) Then
                                                Throw New Exception("VSP Name can not be blank")
                                            End If
                                            Dim add1 As String = ""
                                            If arrExistCols.Contains(clsMasterDefault.colDCSVSPAddress) Then
                                                add1 = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colDCSVSPAddress).Value)
                                            End If

                                            closing_date = System.DateTime.Now.Date
                                            If arrExistCols.Contains(clsMasterDefault.colDCSState) Then

                                                statecode = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colDCSState).Value)
                                                If clsCommon.myLen(statecode) <= 0 AndAlso objCommonVar.ApplyDefaultsInMaster Then
                                                    statecode = clsStateMaster.GetDefault(trans)
                                                End If
                                                check = 0
                                                If clsCommon.myLen(statecode) > 0 Then
                                                    qry = "select STATE_CODE,STATE_NAME from tspl_state_master where  state_code='" + statecode + "'"
                                                    dt = clsDBFuncationality.GetDataTable(qry, trans)
                                                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                                                        Throw New Exception("State Code Does Not Exist,Please Make Its Master First")
                                                    End If
                                                    statecode = clsCommon.myCstr(dt.Rows(0)("STATE_CODE"))
                                                    state = clsCommon.myCstr(dt.Rows(0)("STATE_NAME"))
                                                End If
                                            End If
                                            Dim vsppaymnt As String = ""
                                            If arrExistCols.Contains(clsMasterDefault.colVSPPaymentType) Then
                                                vsppaymnt = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colVSPPaymentType).Value).Replace("'", "`")
                                            End If

                                            'Dim jointname As String = clsCommon.myCstr(gv1.Rows(ii).Cells("Joint_Name").Value).Replace("'", "`")

                                            'If clsCommon.CompairString(vsppaymnt, "Different") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(vsppaymnt, "Self") <> CompairStringResult.Equal Then
                                            '    Throw New Exception("Fill Self/Different in vsp payment ")
                                            'End If
                                            Dim NameOfBank As String = ""
                                            Dim AccountNo As String = ""

                                            If arrExistCols.Contains(clsMasterDefault.colDCSBankCode) Then
                                                strbank = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colDCSBankCode).Value)
                                                If clsCommon.myLen(strbank) > 0 Then
                                                    If String.IsNullOrEmpty(strbank) Then
                                                        Throw New Exception("Bank Code can not be blank")
                                                    End If
                                                    Dim EnableBankFromMaster As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.EnableBankFromMaster, clsFixedParameterCode.EnableBankFromMaster, trans)) = 1, True, False)
                                                    Dim i5 As String
                                                    If EnableBankFromMaster = True Then
                                                        Dim qry7 As String = "select COUNT(*) from tspl_vendor_bank_master  where Bank_Code ='" + strbank + "'"
                                                        i5 = connectSql.RunScalar(trans, qry7)
                                                        If i5 = 0 Then
                                                            Throw New Exception("Bank code does not exist : " + strbank + "")
                                                        End If
                                                    End If
                                                    If strbank.Length > 30 Then
                                                        Throw New Exception("Check the length of bank code")
                                                    End If
                                                End If

                                            End If
                                            Dim strAccNo As String = ""
                                            If arrExistCols.Contains(clsMasterDefault.colDCSAccountNo) Then
                                                strAccNo = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colDCSAccountNo).Value)
                                                'If clsCommon.myLen(strAccNo) > 50 Then
                                                '    Throw New Exception("Account No. should be max 50 character.")
                                                'End If
                                            End If
                                            Dim strBName As String = ""
                                            If arrExistCols.Contains(clsMasterDefault.colDCSBankName) Then
                                                strBName = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colDCSBankName).Value)
                                                'If clsCommon.myLen(strBName) > 50 Then
                                                '    Throw New Exception("Bank Name should be max 50 character.")
                                                'End If
                                            End If
                                            If arrExistCols.Contains(clsMasterDefault.colDCSIFSCCode) Then
                                                strIFSCCode = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colDCSIFSCCode).Value)
                                                'If clsCommon.myLen(strIFSCCode) > 100 Then
                                                '    Throw New Exception("IFSC Code should be max 100 character")
                                                'End If
                                            End If
                                            If arrExistCols.Contains(clsMasterDefault.colDCSBranchName) Then
                                                strBrachName = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colDCSBranchName).Value)
                                                'If clsCommon.myLen(strBrachName) > 100 Then
                                                '    Throw New Exception("Branch Name should be max 100 character")
                                                'End If
                                            End If


                                            strgroupCode = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colDCSVSPGroupCode).Value)
                                            If String.IsNullOrEmpty(strgroupCode) AndAlso objCommonVar.ApplyDefaultsInMaster = True Then
                                                strgroupCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Ven_Group_Code from Tspl_vendor_group where Default_VSP=1", trans))
                                            End If
                                            If String.IsNullOrEmpty(strgroupCode) Then
                                                Throw New Exception("VSP Group Code can not be blank")
                                            End If
                                            Dim i As Integer
                                            qry = "select Count(*)from TSPL_VENDOR_GROUP  where Ven_Group_Code ='" + strgroupCode + "'"
                                            i = connectSql.RunScalar(trans, qry)
                                            If i = 0 Then
                                                Throw New Exception("Vendor Group Code does not exist : " + strgroupCode + "")
                                            Else
                                            End If
                                            If strgroupCode.Length > 12 Then
                                                Throw New Exception("Check the length of VSP Group Code")
                                            End If

                                            strgroupDes = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  group_Desc from TSPL_VENDOR_GROUP  where Ven_Group_Code ='" + strgroupCode + "'", trans))


                                            coll = New Hashtable()
                                            clsCommon.AddColumnsForChange(coll, "Vendor_Name", strvendorname)
                                            clsCommon.AddColumnsForChange(coll, "add1", add1)
                                            clsCommon.AddColumnsForChange(coll, "Closing_Date", closing_date)
                                            clsCommon.AddColumnsForChange(coll, "State", state)
                                            clsCommon.AddColumnsForChange(coll, "Country", country)
                                            clsCommon.AddColumnsForChange(coll, "form_type", "VSP")
                                            clsCommon.AddColumnsForChange(coll, "state_code", statecode)
                                            clsCommon.AddColumnsForChange(coll, "City_Code", CityCode, True)
                                            clsCommon.AddColumnsForChange(coll, "City_Code_Desc", CityName, True)
                                            clsCommon.AddColumnsForChange(coll, "vsp_payment", vsppaymnt, True)
                                            clsCommon.AddColumnsForChange(coll, "Branch_Name", strBrachName, True)
                                            clsCommon.AddColumnsForChange(coll, "Account_No", strAccNo, True)
                                            clsCommon.AddColumnsForChange(coll, "IFSC_Code", strIFSCCode, True)
                                            clsCommon.AddColumnsForChange(coll, "Start_Date", Nothing, True)
                                            clsCommon.AddColumnsForChange(coll, "End_Date", Nothing, True)
                                            clsCommon.AddColumnsForChange(coll, "Nature", "E")
                                            clsCommon.AddColumnsForChange(coll, "is_Head_Load", "F")
                                            clsCommon.AddColumnsForChange(coll, "Status", "N")
                                            clsCommon.AddColumnsForChange(coll, "Onhold", "N")
                                            clsCommon.AddColumnsForChange(coll, "Bank_Code", strbank)
                                            clsCommon.AddColumnsForChange(coll, "Vendor_Group_Code", strgroupCode)
                                            clsCommon.AddColumnsForChange(coll, "Vendor_Group_Code_Desc", strgroupDes)
                                            If arrExistCols.Contains(clsMasterDefault.colDCSBuffalowTIP) Then
                                                clsCommon.AddColumnsForChange(coll, "Tip_Buffalo", clsCommon.myCdbl(gv1.Rows(ii).Cells(clsMasterDefault.colDCSBuffalowTIP).Value))
                                            End If
                                            If arrExistCols.Contains(clsMasterDefault.colDCSCowTIP) Then
                                                clsCommon.AddColumnsForChange(coll, "Tip_Cow", clsCommon.myCdbl(gv1.Rows(ii).Cells(clsMasterDefault.colDCSCowTIP).Value))
                                            End If
                                            clsCommon.AddColumnsForChange(coll, "Currency_Code", objCommonVar.BaseCurrencyCode)
                                            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                                            clsCommon.AddColumnsForChange(coll, "modify_by", objCommonVar.CurrentUserCode)
                                            clsCommon.AddColumnsForChange(coll, "modify_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                                            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)

                                            qry = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colDCSType).Value)
                                            If Not (clsCommon.CompairString(qry, "PDCS") = CompairStringResult.Equal OrElse clsCommon.CompairString(qry, "Registered") = CompairStringResult.Equal OrElse clsCommon.CompairString(qry, "CLUSTER") = CompairStringResult.Equal) Then
                                                Throw New Exception("DCS Type Should be Registered/PDCS/CLUSTER ")
                                            End If
                                            clsCommon.AddColumnsForChange(coll, "Registered_PDCS_CLUSTER", qry)
                                            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

                                            StrVdrNo = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colDCSVSPCode).Value)
                                            If (clsCommon.myLen(StrVdrNo) <= 0) Then
                                                StrVdrNo = clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.VSPMASTER, IIf(clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colDCSType).Value), "PDCS") = CompairStringResult.Equal, clsDocTransactionType.PDCS, clsDocTransactionType.Registered), "")
                                                clsCommon.AddColumnsForChange(coll, "Vendor_Code", StrVdrNo)
                                                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VENDOR_MASTER", OMInsertOrUpdate.Insert, "", trans)
                                                gv1.Rows(ii).Cells(clsMasterDefault.colDCSVSPCode).Value = StrVdrNo
                                            Else
                                                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VENDOR_MASTER", OMInsertOrUpdate.Update, "vendor_code='" + StrVdrNo + "' and form_type='VSP'", trans)
                                            End If



                                            '' End of VSP Master
                                            ''create customer as VSP
                                            If arrExistCols.Contains(clsMasterDefault.colDCSCreatecustomer) Then
                                                Dim createCustomer = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colDCSCreatecustomer).Value)
                                                If clsCommon.myLen(createCustomer) <= 0 Then
                                                    createCustomer = "0"
                                                End If
                                                If clsCommon.CompairString(createCustomer, "0") <> CompairStringResult.Equal And clsCommon.CompairString(createCustomer, "1") <> CompairStringResult.Equal Then
                                                    Throw New Exception("Please Fill Create customer And It Should Be 0 or 1 ")
                                                End If
                                                If clsCommon.CompairString(createCustomer, "1") = CompairStringResult.Equal Then
                                                    Dim objCustomer As New clsCustomerMaster()
                                                    objCustomer.Cust_Code = StrVdrNo
                                                    objCustomer.Customer_Name = strvendorname
                                                    objCustomer.Add1 = add1
                                                    objCustomer.State = statecode
                                                    objCustomer.CUSTOMER_FORM_TYPE = "VSP"
                                                    strgroupCode = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colDCSCustomerGroupCode).Value)
                                                    If String.IsNullOrEmpty(strgroupCode) AndAlso objCommonVar.ApplyDefaultsInMaster = True Then
                                                        strgroupCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Cust_Group_Code from TSPL_CUSTOMER_GROUP_MASTER where Default_VSP=1", trans))
                                                    End If

                                                    If String.IsNullOrEmpty(strgroupCode) Then
                                                        Throw New Exception("Customer Group Code can not be blank")
                                                    End If

                                                    qry = "select Count(*) from TSPL_CUSTOMER_GROUP_MASTER  where Cust_Group_Code ='" + strgroupCode + "'"
                                                    i = connectSql.RunScalar(trans, qry)
                                                    If i = 0 Then
                                                        Throw New Exception("Customer Group Code does not exist : " + strgroupCode + "")
                                                    Else
                                                    End If
                                                    If strgroupCode.Length > 12 Then
                                                        Throw New Exception("Check the length of Customer Group Code")
                                                    End If

                                                    Dim strCmd1 As String = " SELECT Cust_Group_Code as [Customer Gruop Code],Cust_Group_Desc as [Description]," &
                                                      " Tax_Group as [Tax Group],Cust_Account as [Account Set],Terms_Code as [Terms Code] FROM [TSPL_CUSTOMER_GROUP_MASTER] where Cust_Group_Code='" + strgroupCode + "' "
                                                    Dim myDs As DataSet = connectSql.RunSQLReturnDS(trans, strCmd1)
                                                    If myDs.Tables(0).Rows.Count > 0 Then
                                                        Dim row As DataRow = myDs.Tables(0).Rows(0)
                                                        objCustomer.Cust_Group_Code = clsCommon.myCstr(row(0).ToString().Trim())
                                                        objCustomer.Tax_Group = clsCommon.myCstr(row(2).ToString().Trim())
                                                        objCustomer.Cust_Account = clsCommon.myCstr(row(3).ToString().Trim())
                                                        objCustomer.Terms_Code = clsCommon.myCstr(row(4).ToString().Trim())
                                                    End If
                                                    objCustomer.Credit_Customer = "N"

                                                    objCustomer.LastInvoice_No = Nothing
                                                    objCustomer.LastInvoice_Date = Nothing
                                                    objCustomer.Inter_Branch = "N"

                                                    objCustomer.IsDistributor = "N"

                                                    objCustomer.prntcustyn = "N"

                                                    objCustomer.CSA_Type = "N"
                                                    objCustomer.ManualCustomer = "N"

                                                    objCustomer.Comp_Code = objCommonVar.CurrentCompanyCode

                                                    Dim arrDBName As New List(Of String)
                                                    arrDBName.Add(clsCommon.myCstr(objCommonVar.CurrDatabase))

                                                    qry = "select count(*) from TSPL_CUSTOMER_MASTER where cust_code ='" + StrVdrNo + "' and CUSTOMER_FORM_TYPE='VSP'"
                                                    i2 = CInt(connectSql.RunScalar(trans, qry))
                                                    If (i2 = 0) Then
                                                        objCustomer.SaveData(objCustomer, objCustomer.ArrVisi, True, arrDBName, trans)
                                                    Else
                                                        objCustomer.SaveData(objCustomer, objCustomer.ArrVisi, False, arrDBName, trans)
                                                    End If

                                                    'Customer Vendor mapping
                                                    i2 = clsDBFuncationality.getSingleValue("Select COUNT(*) from TSPL_CUSTOMER_VENDOR_MAPPING WHERE cust_code='" + StrVdrNo + "'", trans)
                                                    If i2 = 0 Then
                                                        qry = "insert into TSPL_CUSTOMER_VENDOR_MAPPING values('" + StrVdrNo + "','" + StrVdrNo + "') "
                                                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                                                    End If
                                                End If
                                            End If


                                            ''-----create customer as VSP



                                            '' Village Master
                                            Dim objVillage As New clsfrmVillageMaster
                                            objVillage.villname = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colDCSVillageName).Value)
                                            If clsCommon.myLen(objVillage.villname) <= 0 AndAlso objCommonVar.ApplyDefaultsInMaster = True Then
                                                objVillage.villname = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colDCSVLCName).Value)
                                            End If
                                            If clsCommon.myLen(objVillage.villname) <= 0 Then
                                                Throw New Exception("Please Fill Village Name")
                                            End If
                                            If clsCommon.myLen(objVillage.villname) > 150 Then
                                                Throw New Exception("Length Of Village Name Should Not Exceed 150 Characters")
                                            End If
                                            ' objVillage.citycode = clsCommon.myCstr(gv1.Rows(ii).Cells("city_code").Value)

                                            objVillage.statecode = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colDCSState).Value)
                                            If clsCommon.myLen(objVillage.statecode) <= 0 AndAlso objCommonVar.ApplyDefaultsInMaster Then
                                                objVillage.statecode = clsStateMaster.GetDefault(trans)
                                            End If
                                            If clsCommon.myLen(objVillage.statecode) > 0 Then
                                                qry = "select state_code from tspl_state_master where state_code='" + objVillage.statecode + "'"
                                                objVillage.statecode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                                                If clsCommon.myLen(objVillage.statecode) <= 0 AndAlso objCommonVar.ApplyDefaultsInMaster Then
                                                    objVillage.statecode = clsStateMaster.GetDefault(trans)
                                                End If
                                                If clsCommon.myLen(objVillage.statecode) <= 0 Then
                                                    Throw New Exception("First Create State Master(" + objVillage.statecode + " Does Not Exist In Master)")
                                                End If
                                            End If
                                            If objCommonVar.ApplyDefaultsInMaster = True Then
                                                objVillage.citycode = clsCommon.myCstr(clsCityMaster.GetDefault(trans))
                                            End If
                                            isNewEntry = True
                                            objVillage.villcode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Village_Code from TSPL_VILLAGE_MASTER  where  Village_Name ='" + objVillage.villname + "'", trans))
                                            objVillage.countrycode = clsStateMaster.GetData(objVillage.statecode, NavigatorType.Current, trans).COUNTRY_CODE
                                            If clsCommon.myLen(objVillage.villcode) > 0 Then
                                                isNewEntry = False
                                            End If
                                            clsfrmVillageMaster.SaveData(objVillage, isNewEntry, trans)

                                            '' End of Village MAster 




                                            '' VLC Master


                                            Dim mcccode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMCCCode).Value)
                                            If clsCommon.myLen(mcccode) <= 0 Then
                                                Throw New Exception("Please Fill MCC Code")
                                            End If
                                            If clsCommon.myLen(objVillage.villname) <= 0 Then
                                                Throw New Exception("Please Fill Village Name At Line No.")
                                            End If
                                            If clsCommon.myLen(objVillage.villname) > 150 Then
                                                Throw New Exception("Length Of Village Name Should Not Exceed 150 Characters")
                                            End If





                                            If clsCommon.myLen(VlcUploaderCode) <= 0 Then
                                                Throw New Exception("Length Of Village Name Should Not Exceed 150 Characters")
                                            End If
                                            Dim villcode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ViLLage_Code from TSPL_VILLAGE_MASTER  where  Village_Name ='" + clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colDCSVillageName).Value) + "'", trans))

                                            If clsCommon.myLen(villcode) <= 0 AndAlso objCommonVar.ApplyDefaultsInMaster = True Then
                                                villcode = objVillage.villcode
                                            End If

                                            Dim vspcode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Code from TSPL_VENDOR_MASTER  where  Vendor_Name ='" + clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colDCSVSPName).Value) + "' and Form_type='VSP'", trans))

                                            Dim MilkRouteCode As String = clsDBFuncationality.getSingleValue("Select top 1 Route_Code from TSPL_MCC_ROUTE_MASTER ", trans)

                                            If clsCommon.myLen(MilkRouteCode) <= 0 AndAlso objCommonVar.ApplyDefaultsInMaster = True Then
                                                MilkRouteCode = clsDBFuncationality.getSingleValue("Select Route_Code from TSPL_MCC_ROUTE_MASTER where route_name='" & StrTempVSPName & "' ", trans)
                                            End If


                                            Dim isSaved As Boolean = True
                                            'qry = "select VLC_Code from TSPL_VLC_MASTER_HEAD where vlc_Name='" + clsCommon.myCstr(gv1.Rows(ii).Cells(colDCSVLCName).Value) + "'"
                                            'Dim VLCCode As String = clsDBFuncationality.getSingleValue(qry, trans)
                                            Dim VLCCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colDCSVLCCode).Value)
                                            If clsCommon.myLen(VLCCode) <= 0 Then
                                                VLCCode = mcccode & "/" & clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.VLCMASTER, "", "")
                                            End If
                                            gv1.Rows(ii).Cells(clsMasterDefault.colDCSVLCCode).Value = VLCCode
                                            coll = New Hashtable()
                                            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                                            clsCommon.AddColumnsForChange(coll, "vlc_code", VLCCode)
                                            clsCommon.AddColumnsForChange(coll, "vlc_name", clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colDCSVLCName).Value))

                                            clsCommon.AddColumnsForChange(coll, "VSP_Code", vspcode)
                                            clsCommon.AddColumnsForChange(coll, "village_code", villcode)

                                            clsCommon.AddColumnsForChange(coll, "MCC", mcccode)
                                            clsCommon.AddColumnsForChange(coll, "Route_Code", MilkRouteCode)

                                            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                                            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))
                                            qry = "select count(VLC_Code) from TSPL_VLC_MASTER_HEAD where vlc_Name='" + clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colDCSVLCName).Value) + "'"
                                            check = CInt(clsDBFuncationality.getSingleValue(qry, trans))

                                            If check <= 0 Then
                                                clsCommon.AddColumnsForChange(coll, "Price_Code", Nothing, True)
                                                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                                                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))
                                                If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.AutoUpdateVLCUploaderCodeInVLCMaster, clsFixedParameterCode.AutoUpdateVLCUploaderCodeInVLCMaster, trans), "1") = CompairStringResult.Equal Then
                                                    VlcUploaderCode = clsfrmVLCMaster.GetCodeNumPart(VLCCode)
                                                    clsCommon.AddColumnsForChange(coll, "VLC_Code_VLC_Uploader", VlcUploaderCode)
                                                Else
                                                    clsCommon.AddColumnsForChange(coll, "VLC_Code_VLC_Uploader", VlcUploaderCode)
                                                End If
                                                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VLC_MASTER_HEAD", OMInsertOrUpdate.Insert, "", trans)

                                            Else

                                                clsCommon.AddColumnsForChange(coll, "VLC_Code_VLC_Uploader", VlcUploaderCode)
                                                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VLC_MASTER_HEAD", OMInsertOrUpdate.Update, " TSPL_VLC_MASTER_HEAD.vlc_code='" + VLCCode + "'", trans)
                                            End If

                                            'Create User
                                            qry = "select count(User_Code) from TSPL_USER_MASTER where User_Code='" + VlcUploaderCode + "'"
                                            check = CInt(clsDBFuncationality.getSingleValue(qry, trans))
                                            If check <= 0 AndAlso objCommonVar.ApplyDefaultsInMaster = True Then
                                                coll = New Hashtable()
                                                clsCommon.AddColumnsForChange(coll, "User_Code", VlcUploaderCode)
                                                clsCommon.AddColumnsForChange(coll, "User_Name", strvendorname)
                                                clsCommon.AddColumnsForChange(coll, "Password", clsCommon.EncryptString(VlcUploaderCode))
                                                clsCommon.AddColumnsForChange(coll, "Default_Location", mcccode, True)
                                                clsCommon.AddColumnsForChange(coll, "User_APP_Type", "V", True)
                                                clsCommon.AddColumnsForChange(coll, "Vendor_Code", vspcode, True)
                                                clsCommon.AddColumnsForChange(coll, "User_Type", "")
                                                clsCommon.AddColumnsForChange(coll, "EMP_CODE", "")
                                                clsCommon.AddColumnsForChange(coll, "Emp_Name", "")
                                                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                                                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                                                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                                                clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
                                                clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                                                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_USER_MASTER", OMInsertOrUpdate.Insert, "", trans)
                                            End If


                                            clsfrmVLCMaster.SaveVLCPriceCode(VLCCode, vspcode, mcccode, trans)



                                            '' End Of VLC Master



                                            If clsCommon.myLen(MilkRouteCode) > 0 Then
                                                ''MILK ROUTE VLC MAPPING DETAIL
                                                qry = "select count(*) from TSPL_MCC_ROUTE_VLC_MAPPING where route_code='" & MilkRouteCode & "' and vlc_code='" & VLCCode & "'"
                                                check = clsDBFuncationality.getSingleValue(qry, trans)

                                                coll = New Hashtable()
                                                clsCommon.AddColumnsForChange(coll, "Is_Active", 1)
                                                clsCommon.AddColumnsForChange(coll, "vlc_code", VLCCode)

                                                If check <= 0 Then
                                                    clsCommon.AddColumnsForChange(coll, "route_code", MilkRouteCode)
                                                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_ROUTE_VLC_MAPPING", OMInsertOrUpdate.Insert, "", trans)
                                                Else
                                                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_ROUTE_VLC_MAPPING", OMInsertOrUpdate.Update, " route_code='" & MilkRouteCode & "' and vlc_code='" & VLCCode & "'", trans)
                                                End If
                                                ''END OF MILK ROUTE VLC MAPPING DETAIL
                                            End If




                                            trans.Commit()

                                        Catch ex As Exception
                                            trans.Rollback()
                                            Throw New Exception(ex.Message)
                                        End Try
                                    End If

                                    'Save MP Data
                                    If True Then
                                        Dim trans = clsDBFuncationality.GetTransactin()
                                        Try
                                            Dim obj As New clsMpMaster()
                                            Dim strData As String = ""
                                            If UseVLCUploaderCodeMPUploaderCodeInMCCProcurement = True Then
                                                If clsCommon.myLen(clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMPUploaderCode).Value)) <= 0 Then
                                                    Throw New Exception("MP Uploader Code Can Not Be Left Blank")
                                                End If
                                                strData = clsDBFuncationality.getSingleValue("select VLC_Code from TSPL_VLC_MASTER_HEAD where VLC_Code_VLC_Uploader='" & clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colDCSUploaderCode).Value) & "'", trans)
                                                If clsCommon.myLen(strData) <= 0 Then
                                                    Throw New Exception("VLC Uploader Code Not Found")
                                                End If
                                            Else
                                                strData = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colDCSVLCCode).Value)
                                                If clsCommon.myLen(strData) <= 0 Then
                                                    Throw New Exception("VLC Code Can Not Be Left Blank")
                                                End If
                                                If clsCommon.myLen(strData) > 30 Then
                                                    Throw New Exception("VLC Code Can Not Be Larger Then 30 Charachter")
                                                End If
                                            End If

                                            obj.MCC_Code = strData
                                            strData = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMPCode).Value)

                                            obj.MP_Code = strData

                                            strData = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMPName).Value)
                                            If clsCommon.myLen(strData) <= 0 Then
                                                Throw New Exception("MP Name Can Not Be Left Blank")
                                            End If
                                            If clsCommon.myLen(strData) > 50 Then
                                                Throw New Exception("MP Name Can Not Be Larger Then 50 Charachter")
                                            End If
                                            obj.MP_Name = strData
                                            If arrExistCols.Contains(clsMasterDefault.colMPFatherName) Then
                                                If clsCommon.myLen(gv1.Rows(ii).Cells(clsMasterDefault.colMPFatherName).Value) > 0 Then
                                                    obj.Father_Name = gv1.Rows(ii).Cells(clsMasterDefault.colMPFatherName).Value
                                                End If
                                            End If



                                            strData = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMPAddress1).Value)
                                            If clsCommon.myLen(strData) <= 0 Then
                                                Throw New Exception("Address1 Can Not Be Left Blank")
                                            End If
                                            If clsCommon.myLen(strData) > 50 Then
                                                Throw New Exception("Address1 Can Not Be Larger Then 50 Charachter")
                                            End If
                                            obj.Add1 = strData

                                            If arrExistCols.Contains(clsMasterDefault.colMPAddress2) Then
                                                If clsCommon.myLen(gv1.Rows(ii).Cells(clsMasterDefault.colMPAddress2).Value) > 0 Then
                                                    obj.Add2 = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMPAddress2).Value)
                                                End If
                                            End If
                                            If arrExistCols.Contains(clsMasterDefault.colMPZila) Then
                                                If clsCommon.myLen(gv1.Rows(ii).Cells(clsMasterDefault.colMPZila).Value) > 0 Then
                                                    obj.Zila = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMPZila).Value)
                                                End If
                                            End If
                                            If arrExistCols.Contains(clsMasterDefault.colMPTehsil) Then
                                                If clsCommon.myLen(gv1.Rows(ii).Cells(clsMasterDefault.colMPTehsil).Value) > 0 Then
                                                    obj.Tehsil = gv1.Rows(ii).Cells(clsMasterDefault.colMPTehsil).Value
                                                End If
                                            End If
                                            If arrExistCols.Contains(clsMasterDefault.colMPPinCode) Then
                                                strData = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMPPinCode).Value)
                                                obj.Pin_code = strData
                                            End If
                                            If arrExistCols.Contains(clsMasterDefault.colMPCityCode) Then
                                                strData = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMPCityCode).Value)
                                                obj.City_code = strData
                                            End If

                                            If arrExistCols.Contains(clsMasterDefault.colMPStateCode) Then
                                                strData = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMPStateCode).Value)
                                                obj.State_Code = strData
                                            End If
                                            If arrExistCols.Contains(clsMasterDefault.colMPCountryCode) Then
                                                strData = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMPCountryCode).Value)
                                                obj.Country_code = strData
                                            End If
                                            If arrExistCols.Contains(clsMasterDefault.colMPTelphone) Then
                                                If clsCommon.myLen(gv1.Rows(ii).Cells(clsMasterDefault.colMPTelphone).Value) > 0 Then
                                                    obj.Telphone = gv1.Rows(ii).Cells(clsMasterDefault.colMPTelphone).Value
                                                End If
                                            End If
                                            If arrExistCols.Contains(clsMasterDefault.colMPEmail) Then
                                                If clsCommon.myLen(gv1.Rows(ii).Cells(clsMasterDefault.colMPEmail).Value) > 0 Then
                                                    Dim check As System.Text.RegularExpressions.Match = System.Text.RegularExpressions.Regex.Match(gv1.Rows(ii).Cells(clsMasterDefault.colMPEmail).Value, "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")
                                                    If check.Success Then
                                                        obj.Email = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMPEmail).Value)
                                                    Else
                                                        Throw New Exception("Email Is In Invalid Format. It Shoud Be as UserName@Domain Format ")
                                                    End If
                                                End If
                                            End If
                                            If arrExistCols.Contains(clsMasterDefault.colMPAadharNo) Then
                                                If clsCommon.myLen(gv1.Rows(ii).Cells(clsMasterDefault.colMPAadharNo).Value) > 0 Then
                                                    'If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select count(*) from tspl_mp_master where fax='" & clsCommon.myCstr(gv1.Rows(ii).Cells(colMPAadharNo).Value) & "' and MP_Code<>'" & clsCommon.myCstr(obj.MP_Code) & "'", trans)) > 0 Then
                                                    '    Throw New Exception("Same Aadhar No is exist with another MP so please change Aadhar No because Aadhar No is unique.")
                                                    'End If
                                                    obj.Fax = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMPAadharNo).Value)
                                                End If
                                            End If

                                            If arrExistCols.Contains(clsMasterDefault.colMPJanAadharNo) Then
                                                If clsCommon.myLen(gv1.Rows(ii).Cells(clsMasterDefault.colMPJanAadharNo).Value) > 0 Then
                                                    'If clsCommon.myLen(gv1.Rows(ii).Cells(colMPJanAadharNo).Value) <> 10 Then
                                                    '    Throw New Exception("Invalid Jan Aadhar No.Please Enter 10 Digit Jan Aadhar No")
                                                    'End If
                                                    obj.Jan_Aadhar_No = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMPJanAadharNo).Value)
                                                    'Else
                                                    '    If SettJanAadharNoMandatory Then
                                                    '        Throw New Exception("Please Fill Jan Aadhar No")
                                                    '    End If
                                                End If
                                            End If
                                            If arrExistCols.Contains(clsMasterDefault.colMPDateofBirth) Then
                                                If clsCommon.myLen(gv1.Rows(ii).Cells(clsMasterDefault.colMPDateofBirth).Value) > 0 Then
                                                    If IsDate(gv1.Rows(ii).Cells(clsMasterDefault.colMPDateofBirth).Value) Then
                                                        obj.DOB = clsCommon.myCDate(gv1.Rows(ii).Cells(clsMasterDefault.colMPDateofBirth).Value, "dd/MMM/yyyy")
                                                    Else
                                                        Throw New Exception("Invalid Date Found For Date Of Birth")
                                                    End If
                                                End If
                                            End If


                                            'MP Uploader Code
                                            If clsCommon.myLen(gv1.Rows(ii).Cells(clsMasterDefault.colMPUploaderCode).Value) > 0 Then
                                                obj.MP_CODE_VLC_UPLOADER = gv1.Rows(ii).Cells(clsMasterDefault.colMPUploaderCode).Value
                                            Else
                                                Throw New Exception("Please Fill MP Uploader Code")
                                            End If

                                            Dim qqq As String = "select COUNT(*) from TSPL_MP_MASTER where MP_Code_vlc_uploader='" & obj.MP_CODE_VLC_UPLOADER & "' and mp_code<>'" & obj.MP_Code & "' and vlc_Code='" & obj.MCC_Code & "'"
                                            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qqq, trans)) >= 1 Then
                                                Throw New Exception("Duplicate MP uploader Code")
                                            End If
                                            If arrExistCols.Contains(clsMasterDefault.colMPEducation) Then
                                                If clsCommon.myLen(gv1.Rows(ii).Cells(clsMasterDefault.colMPEducation).Value) > 0 Then
                                                    obj.Education = gv1.Rows(ii).Cells(clsMasterDefault.colMPEducation).Value
                                                End If
                                            End If
                                            If arrExistCols.Contains(clsMasterDefault.colMPTOLERANCE) Then
                                                If clsCommon.myLen(gv1.Rows(ii).Cells(clsMasterDefault.colMPTOLERANCE).Value) > 0 Then
                                                    If IsNumeric(gv1.Rows(ii).Cells(clsMasterDefault.colMPTOLERANCE).Value) = False Then
                                                        Throw New Exception("TOLERANCE value should be Numeric")
                                                    End If
                                                    If clsCommon.myCdbl(gv1.Rows(ii).Cells(clsMasterDefault.colMPTOLERANCE).Value) > 100 Then
                                                        Throw New Exception("TOLERANCE value should be less then 100.")
                                                    End If
                                                    obj.TOLERANCE = clsCommon.myCdbl(gv1.Rows(ii).Cells(clsMasterDefault.colMPTOLERANCE).Value)
                                                End If
                                            End If
                                            If arrExistCols.Contains(clsMasterDefault.colMPTOLERANCE) Then
                                                obj.Land_Holding = clsCommon.myCdbl(gv1.Rows(ii).Cells(clsMasterDefault.colMPTOLERANCE).Value)
                                            End If
                                            obj.No_Of_Animal = 0
                                            If arrExistCols.Contains(clsMasterDefault.colMPNoOfMilchAnimal) Then
                                                obj.No_Of_breedable_milk_animal = clsCommon.myCdbl(gv1.Rows(ii).Cells(clsMasterDefault.colMPNoOfMilchAnimal).Value)
                                            End If
                                            If arrExistCols.Contains(clsMasterDefault.colMPTotalMilkProduction) Then
                                                obj.Milk_production = clsCommon.myCdbl(gv1.Rows(ii).Cells(clsMasterDefault.colMPTotalMilkProduction).Value)
                                            End If
                                            If arrExistCols.Contains(clsMasterDefault.colMPMilkForSelfConsumption) Then
                                                obj.Milk_Home_consumption = clsCommon.myCdbl(gv1.Rows(ii).Cells(clsMasterDefault.colMPMilkForSelfConsumption).Value)
                                            End If
                                            If arrExistCols.Contains(clsMasterDefault.colMPMilkForSale) Then
                                                obj.Milk_For_sale = clsCommon.myCdbl(gv1.Rows(ii).Cells(clsMasterDefault.colMPMilkForSale).Value)
                                            End If
                                            If arrExistCols.Contains(clsMasterDefault.colMPPayeeName) Then
                                                obj.PayeeName = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMPPayeeName).Value)
                                            End If
                                            If arrExistCols.Contains(clsMasterDefault.colMPCustomerAccSet) Then
                                                obj.Cust_Account = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMPCustomerAccSet).Value)
                                                If clsCommon.myLen(obj.Cust_Account) > 0 Then
                                                    '' check valid acc set
                                                    qqq = "select Cust_Account from TSPL_CUSTOMER_ACCOUNT_SET where Cust_Account='" & obj.Cust_Account & "' "
                                                    Dim checkCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qqq, trans))
                                                    If clsCommon.myLen(checkCode) <= 0 Then
                                                        Throw New Exception("Invalid Customer Account Set- " & obj.Cust_Account & "")
                                                    End If
                                                End If
                                            End If
                                            If arrExistCols.Contains(clsMasterDefault.colMPVendorAccSet) Then
                                                obj.Acct_Set_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMPVendorAccSet).Value)
                                                If clsCommon.myLen(obj.Acct_Set_Code) > 0 Then
                                                    '' check valid acc set
                                                    qqq = "select Acct_Set_Code from TSPL_VENDOR_ACCOUNT_SET where Acct_Set_Code='" & obj.Acct_Set_Code & "' "
                                                    Dim checkCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qqq, trans))
                                                    If clsCommon.myLen(checkCode) <= 0 Then
                                                        Throw New Exception("Invalid Vendor Account Set- " & obj.Acct_Set_Code & " ")
                                                    End If
                                                End If
                                            End If
                                            If SettBankIFSCCodeValidateByService Then
                                                If clsCommon.myLen(clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMPIFSCCode).Value)) > 0 Then
                                                    Dim arrFilter As New Dictionary(Of String, String)
                                                    arrFilter.Add("IFSC", clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMPIFSCCode).Value))
                                                    arrFilter.Add("OutPutType", "1")
                                                    Dim dt As DataTable = Xtra.GetDataFromAPI(EnumAPI.BankIFSC, "GetIFSC", arrFilter)
                                                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                                                        Throw New Exception("Invalid IFSC Code")
                                                    End If
                                                    If clsCommon.CompairString(dt.Rows(0)("Result"), "true") = CompairStringResult.Equal Then
                                                        obj.BankName = clsCommon.myCstr(dt.Rows(0)("BANK"))
                                                        obj.BankBranch = clsCommon.myCstr(dt.Rows(0)("BRANCH"))
                                                        obj.BankStateCode = clsCommon.myCstr(dt.Rows(0)("STATE"))
                                                        obj.BankCityCode = clsCommon.myCstr(dt.Rows(0)("CITY"))
                                                        obj.IFCICode = clsCommon.myCstr(dt.Rows(0)("IFSC"))
                                                        If clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMPAccountNo).Value).Contains("E+") OrElse clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMPAccountNo).Value).Contains("E-") Then
                                                            obj.AccountNO = Decimal.Parse(clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMPAccountNo).Value), System.Globalization.NumberStyles.Float)
                                                        Else
                                                            obj.AccountNO = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMPAccountNo).Value)
                                                        End If
                                                    Else
                                                        Throw New Exception(dt.Rows(0)("Response"))
                                                    End If
                                                End If
                                            ElseIf clsCommon.myLen(gv1.Rows(ii).Cells(clsMasterDefault.colMPBankCode).Value) > 0 AndAlso EnableBankFromMaster = True Then
                                                If clsDBFuncationality.getSingleValue("select count(*) from TSPL_Vendor_Bank_Master where bank_code='" & gv1.Rows(ii).Cells(clsMasterDefault.colMPBankCode).Value & "'", trans) > 0 Then
                                                    Dim bnkDt As DataTable = clsDBFuncationality.GetDataTable("select * from TSPL_Vendor_Bank_Master where bank_code='" & gv1.Rows(ii).Cells(clsMasterDefault.colMPBankCode).Value & "'", trans)
                                                    obj.BankName = clsCommon.myCstr(bnkDt.Rows(0)("BANK_CODE"))
                                                    obj.BankCityCode = clsCommon.myCstr(bnkDt.Rows(0)("City_Code"))
                                                    obj.BankStateCode = clsCommon.myCstr(bnkDt.Rows(0)("State_Code"))
                                                    If clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMPAccountNo).Value).Contains("E+") OrElse clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMPAccountNo).Value).Contains("E-") Then
                                                        obj.AccountNO = Decimal.Parse(clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMPAccountNo).Value), System.Globalization.NumberStyles.Float)
                                                    Else
                                                        obj.AccountNO = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMPAccountNo).Value)
                                                    End If
                                                    obj.BankBranch = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMPBankBranch).Value)
                                                    obj.IFCICode = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMPIFSCCode).Value)
                                                    If clsCommon.myLen(obj.IFCICode) > 0 Then
                                                        qqq = "select Bank_IFSC_Code from TSPL_Vendor_Bank_Branch_Details where Bank_code ='" & gv1.Rows(ii).Cells(clsMasterDefault.colMPBankCode).Value & "' and Bank_IFSC_Code = '" & gv1.Rows(ii).Cells(clsMasterDefault.colMPIFSCCode).Value & "' "
                                                        Dim checkCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qqq, trans))
                                                        If clsCommon.myLen(checkCode) <= 0 Then
                                                            Throw New Exception("Invalid IFSC Code for Bank Code - " & gv1.Rows(ii).Cells(clsMasterDefault.colMPBankCode).Value & " ")
                                                        End If

                                                        If clsCommon.myLen(obj.BankBranch) <= 0 Then
                                                            qqq = "select Branch_Name from TSPL_Vendor_Bank_Branch_Details where Bank_code ='" & gv1.Rows(ii).Cells(clsMasterDefault.colMPBankCode).Value & "' and Bank_IFSC_Code = '" & gv1.Rows(ii).Cells(clsMasterDefault.colMPIFSCCode).Value & "' "
                                                            Dim BranchCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qqq, trans))
                                                            If clsCommon.myLen(BranchCode) > 0 Then
                                                                obj.BankBranch = BranchCode
                                                                gv1.Rows(ii).Cells(clsMasterDefault.colMPBankBranch).Value = BranchCode
                                                            End If
                                                        End If

                                                    End If
                                                    If clsCommon.myLen(obj.BankBranch) > 0 Then
                                                        qqq = "select Branch_Name from TSPL_Vendor_Bank_Branch_Details where Bank_code ='" & gv1.Rows(ii).Cells(clsMasterDefault.colMPBankCode).Value & "' and Branch_Name = '" & gv1.Rows(ii).Cells(clsMasterDefault.colMPBankBranch).Value & "' "
                                                        Dim checkCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qqq, trans))
                                                        If clsCommon.myLen(checkCode) <= 0 Then
                                                            Throw New Exception("Invalid Bank Branch for Bank Code - " & gv1.Rows(ii).Cells(clsMasterDefault.colMPBankCode).Value & " ")
                                                        End If
                                                        If clsCommon.myLen(obj.IFCICode) <= 0 Then
                                                            qqq = "select Bank_IFSC_Code from TSPL_Vendor_Bank_Branch_Details where Bank_code ='" & gv1.Rows(ii).Cells(clsMasterDefault.colMPBankCode).Value & "' and Branch_Name = '" & gv1.Rows(ii).Cells(clsMasterDefault.colMPBankBranch).Value & "' "
                                                            Dim IFSCCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qqq, trans))
                                                            If clsCommon.myLen(IFSCCode) > 0 Then
                                                                obj.IFCICode = IFSCCode
                                                                gv1.Rows(ii).Cells(clsMasterDefault.colMPIFSCCode).Value = IFSCCode
                                                            End If
                                                        End If

                                                    End If

                                                    If clsCommon.myLen(obj.BankBranch) > 0 AndAlso clsCommon.myLen(obj.IFCICode) > 0 Then
                                                        qqq = "select Branch_Name from TSPL_Vendor_Bank_Branch_Details where Bank_code ='" & gv1.Rows(ii).Cells(clsMasterDefault.colMPBankCode).Value & "' and Branch_Name = '" & gv1.Rows(ii).Cells(clsMasterDefault.colMPBankBranch).Value & "' and Bank_IFSC_Code = '" & gv1.Rows(ii).Cells(clsMasterDefault.colMPIFSCCode).Value & "' "
                                                        Dim checkCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qqq, trans))
                                                        If clsCommon.myLen(checkCode) <= 0 Then
                                                            Throw New Exception("Invalid IFSC Code for Branch - [" & gv1.Rows(ii).Cells(clsMasterDefault.colMPBankBranch).Value & "] ,Bank Code - " & gv1.Rows(ii).Cells(clsMasterDefault.colMPBankCode).Value & "")
                                                        End If
                                                    End If
                                                End If
                                            Else
                                                If EnableBankFromMaster = False Then
                                                    obj.BankName = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMPBankCode).Value)
                                                    obj.BankCityCode = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMPBankCityCode).Value)
                                                    obj.BankStateCode = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMPBankStateCode).Value)
                                                    If clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMPAccountNo).Value).Contains("E+") OrElse clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMPAccountNo).Value).Contains("E-") Then
                                                        obj.AccountNO = Decimal.Parse(clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMPAccountNo).Value), System.Globalization.NumberStyles.Float)
                                                    Else
                                                        obj.AccountNO = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMPAccountNo).Value)
                                                    End If
                                                    obj.BankBranch = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMPBankBranch).Value)
                                                    obj.IFCICode = clsCommon.myCstr(gv1.Rows(ii).Cells(clsMasterDefault.colMPIFSCCode).Value)
                                                End If
                                            End If
                                            If clsMpMaster.IsDBTDone(obj.MP_Code, trans) Then
                                                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select AccountNO,IFCICode From TSPL_MP_MASTER where MP_Code='" + obj.MP_Code + "'", trans)
                                                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                                    obj.AccountNO = clsCommon.myCstr(dt.Rows(0)("AccountNO"))
                                                    obj.IFCICode = clsCommon.myCstr(dt.Rows(0)("IFCICode"))
                                                End If
                                            End If

                                            'If clsCommon.myLen(obj.AccountNO) > 0 Then
                                            '    If clsCommon.MySpecialChars(obj.AccountNO, EnumSpecialChactersType.Digit) Then
                                            '        Throw New Exception("Invalid AccountNO [" + clsCommon.myCstr(gv1.Rows(ii).Cells(colMPAccountNo).Value) + "]")
                                            '    End If
                                            'End If

                                            obj.ArrMPIncentiveMapping = New ArrayList()



                                            '=====================================================================================

                                            If clsDBFuncationality.getSingleValue("select count(*) from tspl_mp_master where mp_code='" & obj.MP_Code & "'", trans) = 0 Then
                                                Dim dt As Date = clsCommon.GETSERVERDATE(trans, "dd/MMM/yyyy")
                                                If clsCommon.myLen(obj.MP_Code) <= 0 Then
                                                    obj.MP_Code = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.MPMaster, "", "")
                                                End If

                                                If obj.No_Of_breedable_milk_animal > 0 Then
                                                    Dim objAnimal As clsAnimalDetails
                                                    obj.arrAnimalDetail = New List(Of clsAnimalDetails)
                                                    For j As Integer = 0 To obj.No_Of_breedable_milk_animal
                                                        objAnimal = New clsAnimalDetails
                                                        objAnimal.Prog_Code = clsUserMgtCode.frmMPMaster
                                                        objAnimal.Trans_Code = obj.MP_Code
                                                        objAnimal.Line_No = (j + 1)
                                                        objAnimal.Bread_of_Animal = "N/A"
                                                        objAnimal.Type_Of_Animal = "N/A"
                                                        obj.arrAnimalDetail.Add(objAnimal)
                                                    Next
                                                End If

                                                obj.isNewEntry = True
                                                obj.Modified_By = objCommonVar.CurrentUserCode
                                                obj.Modified_Date = clsCommon.GetPrintDate(strdate, "dd/MMM/yyyy")
                                                obj.Comp_Code = objCommonVar.CurrentCompanyCode
                                                obj.Created_By = objCommonVar.CurrentUserCode
                                                obj.Created_Date = clsCommon.GetPrintDate(strdate, "dd/MMM/yyyy")
                                                clsMpMaster.SaveData(obj, trans)
                                            Else
                                                obj.Modified_By = objCommonVar.CurrentUserCode
                                                obj.Modified_Date = clsCommon.GetPrintDate(strdate, "dd/MMM/yyyy")
                                                obj.isNewEntry = False
                                                clsMpMaster.SaveData(obj, trans)
                                            End If
                                            trans.Commit()
                                        Catch ex As Exception
                                            trans.Rollback()
                                            Throw New Exception(ex.Message)
                                        End Try
                                    End If

                                    gv1.Rows(ii).Cells(colError).Value = "Saved"
                                    gv1.Rows(ii).Cells(colOK).Value = 1
                                Catch ex As Exception
                                    gv1.Rows(ii).Cells(colError).Value = ex.Message
                                    gv1.Rows(ii).Cells(colOK).Value = 0
                                End Try
                            End If
                        Next
                    End If
                    clsCommon.ProgressBarPercentHide()
                    clsCommon.MyMessageBoxShow(Me, "Data Saved successfully", Me.Text)
                End If
            End If
        Catch ex As Exception
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv_RowFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.RowFormattingEventArgs) Handles gv1.RowFormatting
        Try
            'If clsCommon.myCdbl(e.RowElement.RowInfo.Cells("IsOK").Value) = 1 Then
            '    e.RowElement.DrawFill = True
            '    e.RowElement.GradientStyle = GradientStyles.Solid
            '    e.RowElement.ForeColor = Color.Black
            '    e.RowElement.BackColor = Color.LightGreen
            'ElseIf clsCommon.myCdbl(e.RowElement.RowInfo.Cells("IsOK").Value) = 2 Then
            '    e.RowElement.DrawFill = True
            '    e.RowElement.GradientStyle = GradientStyles.Solid
            '    e.RowElement.ForeColor = Color.Black
            '    e.RowElement.BackColor = Color.MistyRose
            'Else
            '    e.RowElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local)
            '    e.RowElement.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local)
            '    e.RowElement.ResetValue(LightVisualElement.ForeColorProperty, ValueResetFlags.Local)
            '    e.RowElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.Local)
            'End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub RadButton2_Click(sender As Object, e As EventArgs)
        '        Try
        '            loadBlankGrid()
        '            Dim qry As String = "select TSPL_MP_MASTER.MP_Code,TSPL_MP_MASTER.MP_Code_VLC_Uploader,TSPL_MP_MASTER.MP_Name,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_MP_MASTER.IFCICode,TSPL_MP_MASTER.BankName,TSPL_MP_MASTER.BankBranch,TSPL_MP_MASTER.BankCityCode,TSPL_MP_MASTER.BankStateCode from TSPL_MP_MASTER
        'left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MP_MASTER.VLC_Code
        'where 2=2  "
        '            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
        '                qry += "  and TSPL_VLC_MASTER_HEAD.MCC in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")"
        '            End If
        '            If txtVLC.arrValueMember IsNot Nothing AndAlso txtVLC.arrValueMember.Count > 0 Then
        '                qry += "and TSPL_MP_MASTER.VLC_Code in (" + clsCommon.GetMulcallString(txtVLC.arrValueMember) + ")"
        '            End If
        '            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        '            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
        '                Throw New Exception("No Data Found")
        '            End If
        '            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
        '                For ii As Integer = 0 To dt.Rows.Count - 1
        '                    gv1.Rows.AddNew()

        '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colMPCode).Value = clsCommon.myCstr(dt.Rows(ii)("MP_Code"))
        '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colMPUploader).Value = clsCommon.myCstr(dt.Rows(ii)("MP_Code_VLC_Uploader"))
        '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colMPName).Value = clsCommon.myCstr(dt.Rows(ii)("MP_Name"))

        '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSocietyUploader).Value = clsCommon.myCstr(dt.Rows(ii)("VLC_Code_VLC_Uploader"))
        '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSocietyName).Value = clsCommon.myCstr(dt.Rows(ii)("VLC_Name"))

        '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIFSC).Value = clsCommon.myCstr(dt.Rows(ii)("IFCICode"))
        '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colBankName).Value = clsCommon.myCstr(dt.Rows(ii)("BankName"))
        '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colBankBranch).Value = clsCommon.myCstr(dt.Rows(ii)("BankBranch"))

        '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colBankCity).Value = clsCommon.myCstr(dt.Rows(ii)("BankCityCode"))
        '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colBankState).Value = clsCommon.myCstr(dt.Rows(ii)("BankStateCode"))
        '                Next
        '            End If
        '        Catch ex As Exception
        '            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        '        End Try
    End Sub

    'Sub loadBlankGrid()
    '    gv1.Rows.Clear()
    '    gv1.Columns.Clear()
    '    gv1.DataSource = Nothing


    '    Dim repoTxt As New GridViewTextBoxColumn()
    '    repoTxt.FormatString = ""
    '    repoTxt.HeaderText = colMPCode
    '    repoTxt.Name = colMPCode
    '    repoTxt.ReadOnly = True
    '    repoTxt.IsVisible = True
    '    repoTxt.Width = 100
    '    repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
    '    gv1.Columns.Add(repoTxt)

    '    repoTxt = New GridViewTextBoxColumn()
    '    repoTxt.FormatString = ""
    '    repoTxt.HeaderText = colMPUploader
    '    repoTxt.Name = colMPUploader
    '    repoTxt.ReadOnly = True
    '    repoTxt.IsVisible = True
    '    repoTxt.Width = 150
    '    repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
    '    gv1.Columns.Add(repoTxt)

    '    repoTxt = New GridViewTextBoxColumn()
    '    repoTxt.FormatString = ""
    '    repoTxt.HeaderText = colMPName
    '    repoTxt.Name = colMPName
    '    repoTxt.Width = 150
    '    repoTxt.ReadOnly = True
    '    repoTxt.IsVisible = True
    '    repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
    '    gv1.Columns.Add(repoTxt)

    '    repoTxt = New GridViewTextBoxColumn()
    '    repoTxt.FormatString = ""
    '    repoTxt.HeaderText = colSocietyUploader
    '    repoTxt.Name = colSocietyUploader
    '    repoTxt.ReadOnly = True
    '    repoTxt.IsVisible = True
    '    repoTxt.Width = 150
    '    repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
    '    gv1.Columns.Add(repoTxt)

    '    repoTxt = New GridViewTextBoxColumn()
    '    repoTxt.FormatString = ""
    '    repoTxt.HeaderText = colSocietyName
    '    repoTxt.Name = colSocietyName
    '    repoTxt.Width = 150
    '    repoTxt.ReadOnly = True
    '    repoTxt.IsVisible = True
    '    repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
    '    gv1.Columns.Add(repoTxt)

    '    repoTxt = New GridViewTextBoxColumn()
    '    repoTxt.FormatString = ""
    '    repoTxt.HeaderText = colIFSC
    '    repoTxt.Name = colIFSC
    '    repoTxt.Width = 150
    '    repoTxt.ReadOnly = False
    '    repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
    '    gv1.Columns.Add(repoTxt)

    '    repoTxt = New GridViewTextBoxColumn()
    '    repoTxt.FormatString = ""
    '    repoTxt.HeaderText = colBankName
    '    repoTxt.Name = colBankName
    '    repoTxt.Width = 150
    '    repoTxt.ReadOnly = True
    '    repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
    '    gv1.Columns.Add(repoTxt)

    '    repoTxt = New GridViewTextBoxColumn()
    '    repoTxt.FormatString = ""
    '    repoTxt.HeaderText = colBankBranch
    '    repoTxt.Name = colBankBranch
    '    repoTxt.Width = 150
    '    repoTxt.ReadOnly = True
    '    repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
    '    gv1.Columns.Add(repoTxt)

    '    repoTxt = New GridViewTextBoxColumn()
    '    repoTxt.FormatString = ""
    '    repoTxt.HeaderText = colBankCity
    '    repoTxt.Name = colBankCity
    '    repoTxt.Width = 150
    '    repoTxt.ReadOnly = True
    '    repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
    '    gv1.Columns.Add(repoTxt)

    '    repoTxt = New GridViewTextBoxColumn()
    '    repoTxt.FormatString = ""
    '    repoTxt.HeaderText = colBankState
    '    repoTxt.Name = colBankState
    '    repoTxt.Width = 150
    '    repoTxt.ReadOnly = True
    '    repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
    '    gv1.Columns.Add(repoTxt)

    '    repoTxt = New GridViewTextBoxColumn()
    '    repoTxt.FormatString = ""
    '    repoTxt.HeaderText = colError
    '    repoTxt.Name = colError
    '    repoTxt.Width = 200
    '    repoTxt.ReadOnly = True
    '    repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
    '    gv1.Columns.Add(repoTxt)

    '    Dim repoNum As New GridViewDecimalColumn
    '    repoNum.FormatString = ""
    '    repoNum.HeaderText = colOK
    '    repoNum.Name = colOK
    '    repoNum.IsVisible = False
    '    repoNum.ReadOnly = False
    '    repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '    gv1.Columns.Add(repoNum)

    '    gv1.AllowAddNewRow = False
    '    gv1.AllowDeleteRow = True
    '    gv1.AllowRowReorder = False
    '    gv1.ShowGroupPanel = False
    '    gv1.EnableFiltering = True
    '    gv1.ShowFilteringRow = True
    '    gv1.EnableSorting = False
    '    gv1.EnableGrouping = False
    '    gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
    '    gv1.GridBehavior = New MyBehavior()
    'End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        Try
            transportSql.exportdata(gv1, "", Me.Text, False, Nothing, False, False, False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        'Try
        '    Dim gvImport As New RadGridView()
        '    Me.Controls.Add(gvImport)
        '    loadBlankGrid()
        '    If transportSql.importExcel(gvImport, colMPCode, colMPUploader, colMPName, colSocietyUploader, colSocietyName, colIFSC, colBankName, colBankBranch, colBankCity, colBankState) Then

        '        Dim ii As Integer = 0
        '        Try
        '            Dim qry As String = ""
        '            Dim ErrCount As Integer = 0
        '            clsCommon.ProgressBarShow()
        '            For ii = 0 To gvImport.RowCount - 1
        '                If clsCommon.myLen(gvImport.Rows(ii).Cells(colMPCode).Value) > 0 Then
        '                    qry = "select MP_Code,MP_Name,MP_Code_VLC_Uploader from TSPL_MP_MASTER where MP_Code='" + clsCommon.myCstr(gvImport.Rows(ii).Cells(colMPCode).Value) + "'"
        '                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        '                    If dt Is Nothing AndAlso dt.Rows.Count <= 0 Then
        '                        Throw New Exception("Invalid MP [" + clsCommon.myCstr(gvImport.Rows(ii).Cells(colMPCode).Value) + "]")
        '                    End If
        '                    gv1.Rows.AddNew()
        '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colMPCode).Value = clsCommon.myCstr(dt.Rows(0)("MP_Code"))
        '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colMPUploader).Value = clsCommon.myCstr(dt.Rows(0)("MP_Code_VLC_Uploader"))
        '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colMPName).Value = clsCommon.myCstr(dt.Rows(0)("MP_Name"))

        '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSocietyUploader).Value = clsCommon.myCstr(gvImport.Rows(ii).Cells(colSocietyUploader).Value)
        '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSocietyName).Value = clsCommon.myCstr(gvImport.Rows(ii).Cells(colSocietyName).Value)

        '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIFSC).Value = clsCommon.myCstr(gvImport.Rows(ii).Cells(colIFSC).Value)
        '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colBankName).Value = clsCommon.myCstr(gvImport.Rows(ii).Cells(colBankName).Value)
        '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colBankBranch).Value = clsCommon.myCstr(gvImport.Rows(ii).Cells(colBankBranch).Value)

        '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colBankCity).Value = clsCommon.myCstr(gvImport.Rows(ii).Cells(colBankCity).Value)
        '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colBankState).Value = clsCommon.myCstr(gvImport.Rows(ii).Cells(colBankState).Value)
        '                End If
        '            Next
        '        Catch ex As Exception
        '            clsCommon.ProgressBarHide()
        '            Throw New Exception("Error at Row No" + clsCommon.myCstr(ii + 1) + Environment.NewLine + ex.Message)
        '        Finally
        '            clsCommon.ProgressBarHide()
        '        End Try
        '    End If
        '    Me.Controls.Remove(gvImport)
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        'End Try
    End Sub



    Private Sub txtTemplete__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtTemplete._MYValidating
        Try
            isVarified = False
            Dim str As String = "select count(*) from TSPL_EXPORT_TEMPLATE_HEAD where Export_Code ='" + txtTemplete.Value + "' and Is_Default_Value=0 "
            Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
            If no = 0 AndAlso isButtonClicked = False Then
                txtTemplete.MyReadOnly = False
            Else
                txtTemplete.MyReadOnly = True
            End If
            If txtTemplete.MyReadOnly OrElse isButtonClicked Then
                txtTemplete.Value = clsExportTemplate.GetFinder(" TSPL_EXPORT_TEMPLATE_HEAD.Program_Code='" + FromID + "' and TSPL_EXPORT_TEMPLATE_HEAD.Created_By='" & objCommonVar.CurrentUserCode & "' and Is_Default_Value=0  ", txtTemplete.Value, isButtonClicked)
                'LoadData(txtTemplete.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub RadButton3_Click_1(sender As Object, e As EventArgs) Handles RadButton3.Click
        isVarified = False
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        transportSql.LoadDocument(gv1, "BankReco")

        gv1.BestFitColumns()


        Dim repoTxt As New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCCode
        repoTxt.Name = clsMasterDefault.colMCCCode
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCType
        repoTxt.Name = clsMasterDefault.colMCCType
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCChillingVendorCode
        repoTxt.Name = clsMasterDefault.colMCCChillingVendorCode
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCAddress1
        repoTxt.Name = clsMasterDefault.colMCCAddress1
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCAddress2
        repoTxt.Name = clsMasterDefault.colMCCAddress2
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCTehsil
        repoTxt.Name = clsMasterDefault.colMCCTehsil
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCCity
        repoTxt.Name = clsMasterDefault.colMCCCity
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCState
        repoTxt.Name = clsMasterDefault.colMCCState
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCCountry
        repoTxt.Name = clsMasterDefault.colMCCCountry
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCPinCode
        repoTxt.Name = clsMasterDefault.colMCCPinCode
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCTelphone
        repoTxt.Name = clsMasterDefault.colMCCTelphone
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCEmail
        repoTxt.Name = clsMasterDefault.colMCCEmail
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCFax
        repoTxt.Name = clsMasterDefault.colMCCFax
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMccSuperArea
        repoTxt.Name = clsMasterDefault.colMccSuperArea
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCAreaOfStore
        repoTxt.Name = clsMasterDefault.colMCCAreaOfStore
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCAreaOfOffice
        repoTxt.Name = clsMasterDefault.colMCCAreaOfOffice
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCOpenAreaForTanker
        repoTxt.Name = clsMasterDefault.colMCCOpenAreaForTanker
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCAreaOfLab
        repoTxt.Name = clsMasterDefault.colMCCAreaOfLab
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCTotalStorageCapacity
        repoTxt.Name = clsMasterDefault.colMCCTotalStorageCapacity
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCAreaOfReceivingDock
        repoTxt.Name = clsMasterDefault.colMCCAreaOfReceivingDock
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCDripSaver
        repoTxt.Name = clsMasterDefault.colMCCDripSaver
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCCanWasher
        repoTxt.Name = clsMasterDefault.colMCCCanWasher
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCCanScrubber
        repoTxt.Name = clsMasterDefault.colMCCCanScrubber
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)
        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCFssaiNo
        repoTxt.Name = clsMasterDefault.colMCCFssaiNo
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCETP
        repoTxt.Name = clsMasterDefault.colMCCETP
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCEarthing
        repoTxt.Name = clsMasterDefault.colMCCEarthing
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCCoilLength
        repoTxt.Name = clsMasterDefault.colMCCCoilLength
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCElectricityConnection
        repoTxt.Name = clsMasterDefault.colMCCElectricityConnection
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCBoiler
        repoTxt.Name = clsMasterDefault.colMCCBoiler
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCIndustryType
        repoTxt.Name = clsMasterDefault.colMCCIndustryType
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCPropName
        repoTxt.Name = clsMasterDefault.colMCCPropName
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCPartnerName
        repoTxt.Name = clsMasterDefault.colMCCPartnerName
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCDirectorName
        repoTxt.Name = clsMasterDefault.colMCCDirectorName
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCMonthlyProvision
        repoTxt.Name = clsMasterDefault.colMCCMonthlyProvision
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCChillingCharges
        repoTxt.Name = clsMasterDefault.colMCCChillingCharges
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCChillingOn
        repoTxt.Name = clsMasterDefault.colMCCChillingOn
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCChillingMinGuaranteedAvgQty
        repoTxt.Name = clsMasterDefault.colMCCChillingMinGuaranteedAvgQty
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCChillingOnUOMKGLTR
        repoTxt.Name = clsMasterDefault.colMCCChillingOnUOMKGLTR
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCChillingOnQty
        repoTxt.Name = clsMasterDefault.colMCCChillingOnQty
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCChillingOnUOMHandledDispatched
        repoTxt.Name = clsMasterDefault.colMCCChillingOnUOMHandledDispatched
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCChillingMinGuaranteedPeriod
        repoTxt.Name = clsMasterDefault.colMCCChillingMinGuaranteedPeriod
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)


        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCChillingMinGuaranteedPeriodUOM
        repoTxt.Name = clsMasterDefault.colMCCChillingMinGuaranteedPeriodUOM
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCRateofLeaseCharges
        repoTxt.Name = clsMasterDefault.colMCCRateofLeaseCharges
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCAgreement_Status
        repoTxt.Name = clsMasterDefault.colMCCAgreement_Status
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCAgreement_Date
        repoTxt.Name = clsMasterDefault.colMCCAgreement_Date
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCAgreementExpiryDate
        repoTxt.Name = clsMasterDefault.colMCCAgreementExpiryDate
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCSecurity_Status
        repoTxt.Name = clsMasterDefault.colMCCSecurity_Status
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCCheque_Amt
        repoTxt.Name = clsMasterDefault.colMCCCheque_Amt
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCCheque_No
        repoTxt.Name = clsMasterDefault.colMCCCheque_No
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCCheque_Date
        repoTxt.Name = clsMasterDefault.colMCCCheque_Date
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCChillingStartingDate
        repoTxt.Name = clsMasterDefault.colMCCChillingStartingDate
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCIsTruckSheetMandatory
        repoTxt.Name = clsMasterDefault.colMCCIsTruckSheetMandatory
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCWeighingComPort
        repoTxt.Name = clsMasterDefault.colMCCWeighingComPort
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCSampleComPort
        repoTxt.Name = clsMasterDefault.colMCCSampleComPort
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCPaymentCycle
        repoTxt.Name = clsMasterDefault.colMCCPaymentCycle
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCIncentiveCode
        repoTxt.Name = clsMasterDefault.colMCCIncentiveCode
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCShiftMorningOpeningTime
        repoTxt.Name = clsMasterDefault.colMCCShiftMorningOpeningTime
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCShiftMorningClosingTime
        repoTxt.Name = clsMasterDefault.colMCCShiftMorningClosingTime
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCShiftEveningOpeningTime
        repoTxt.Name = clsMasterDefault.colMCCShiftEveningOpeningTime
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCShiftEveningClosingTime
        repoTxt.Name = clsMasterDefault.colMCCShiftEveningClosingTime
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCRM
        repoTxt.Name = clsMasterDefault.colMCCRM
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCRequiredGateEntry
        repoTxt.Name = clsMasterDefault.colMCCRequiredGateEntry
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCAllowAutoMilkIn
        repoTxt.Name = clsMasterDefault.colMCCAllowAutoMilkIn
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCAutoIn_Location
        repoTxt.Name = clsMasterDefault.colMCCAutoIn_Location
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCSILOIn_Location
        repoTxt.Name = clsMasterDefault.colMCCSILOIn_Location
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCApplyReceiptWeightTolerance
        repoTxt.Name = clsMasterDefault.colMCCApplyReceiptWeightTolerance
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCReceiptWeightToleranceValue
        repoTxt.Name = clsMasterDefault.colMCCReceiptWeightToleranceValue
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCApplyFailedSample
        repoTxt.Name = clsMasterDefault.colMCCApplyFailedSample
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCFailedSampleFAT
        repoTxt.Name = clsMasterDefault.colMCCFailedSampleFAT
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCFailedSampleSNF
        repoTxt.Name = clsMasterDefault.colMCCFailedSampleSNF
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCLocSegmentCode
        repoTxt.Name = clsMasterDefault.colMCCLocSegmentCode
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCCommissionRate
        repoTxt.Name = clsMasterDefault.colMCCCommissionRate
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCCommissionMinimumShiftInPaymentCycle
        repoTxt.Name = clsMasterDefault.colMCCCommissionMinimumShiftInPaymentCycle
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCCommissionMinimumQtyInShift
        repoTxt.Name = clsMasterDefault.colMCCCommissionMinimumQtyInShift
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCCommissionNoOfPaymentCycleForNewVSP
        repoTxt.Name = clsMasterDefault.colMCCCommissionNoOfPaymentCycleForNewVSP
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCDeductionMinimumFATPer
        repoTxt.Name = clsMasterDefault.colMCCDeductionMinimumFATPer
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCDeductionMinimumSNFPer
        repoTxt.Name = clsMasterDefault.colMCCDeductionMinimumSNFPer
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCDeductionNoOfPaymentCycleForNewVSP
        repoTxt.Name = clsMasterDefault.colMCCDeductionNoOfPaymentCycleForNewVSP
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCRateofLeasedChargesUOM
        repoTxt.Name = clsMasterDefault.colMCCRateofLeasedChargesUOM
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCAreaofStoreUOM
        repoTxt.Name = clsMasterDefault.colMCCAreaofStoreUOM
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCAreaOfReceivingDockUOM
        repoTxt.Name = clsMasterDefault.colMCCAreaOfReceivingDockUOM
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCAreaOfOfficeUOM
        repoTxt.Name = clsMasterDefault.colMCCAreaOfOfficeUOM
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCAreaOfLabUOM
        repoTxt.Name = clsMasterDefault.colMCCAreaOfLabUOM
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCOpenAreaForTankerUOM
        repoTxt.Name = clsMasterDefault.colMCCOpenAreaForTankerUOM
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)


        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMccSuperAreaUOM
        repoTxt.Name = clsMasterDefault.colMccSuperAreaUOM
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCWeighingMachine
        repoTxt.Name = clsMasterDefault.colMCCWeighingMachine
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCSampleMachine
        repoTxt.Name = clsMasterDefault.colMCCSampleMachine
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCPlant
        repoTxt.Name = clsMasterDefault.colMCCPlant
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)



        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCMorningShiftOpeningTime
        repoTxt.Name = clsMasterDefault.colMCCMorningShiftOpeningTime
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCMorningShiftClosingTime
        repoTxt.Name = clsMasterDefault.colMCCMorningShiftClosingTime
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCEveningShiftOpeningTime
        repoTxt.Name = clsMasterDefault.colMCCEveningShiftOpeningTime
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMCCEveningShiftClosingTime
        repoTxt.Name = clsMasterDefault.colMCCEveningShiftClosingTime
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        'repoTxt = New GridViewTextBoxColumn()
        'repoTxt.FormatString = ""
        'repoTxt.HeaderText = colDCSRouteCode
        'repoTxt.Name = colDCSRouteCode
        'repoTxt.IsVisible = False
        'repoTxt.ReadOnly = True
        'repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        'gv1.Columns.Add(repoTxt)

        'repoTxt = New GridViewTextBoxColumn()
        'repoTxt.FormatString = ""
        'repoTxt.HeaderText = colDCSRouteName
        'repoTxt.Name = colDCSRouteName
        'repoTxt.IsVisible = False
        'repoTxt.ReadOnly = True
        'repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        'gv1.Columns.Add(repoTxt)

        'repoTxt = New GridViewTextBoxColumn()
        'repoTxt.FormatString = ""
        'repoTxt.HeaderText = colDCSRouteDistance
        'repoTxt.Name = colDCSRouteDistance
        'repoTxt.IsVisible = False
        'repoTxt.ReadOnly = True
        'repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        'gv1.Columns.Add(repoTxt)

        'repoTxt = New GridViewTextBoxColumn()
        'repoTxt.FormatString = ""
        'repoTxt.HeaderText = colDCSRouteEffectiveStartDate
        'repoTxt.Name = colDCSRouteEffectiveStartDate
        'repoTxt.IsVisible = False
        'repoTxt.ReadOnly = True
        'repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        'gv1.Columns.Add(repoTxt)

        'repoTxt = New GridViewTextBoxColumn()
        'repoTxt.FormatString = ""
        'repoTxt.HeaderText = colDCSVehicle
        'repoTxt.Name = colDCSVehicle
        'repoTxt.IsVisible = False
        'repoTxt.ReadOnly = True
        'repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        'gv1.Columns.Add(repoTxt)

        'repoTxt = New GridViewTextBoxColumn()
        'repoTxt.FormatString = ""
        'repoTxt.HeaderText = colDCSVehiclePaymentBasis
        'repoTxt.Name = colDCSVehiclePaymentBasis
        'repoTxt.IsVisible = False
        'repoTxt.ReadOnly = True
        'repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        'gv1.Columns.Add(repoTxt)




        'repoTxt = New GridViewTextBoxColumn()
        'repoTxt.FormatString = ""
        'repoTxt.HeaderText = colDCSPaymentPerKm
        'repoTxt.Name = colDCSPaymentPerKm
        'repoTxt.IsVisible = False
        'repoTxt.ReadOnly = True
        'repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        'gv1.Columns.Add(repoTxt)

        'repoTxt = New GridViewTextBoxColumn()
        'repoTxt.FormatString = ""
        'repoTxt.HeaderText = colDCSVehicleEffectiveStartDate
        'repoTxt.Name = colDCSVehicleEffectiveStartDate
        'repoTxt.IsVisible = False
        'repoTxt.ReadOnly = True
        'repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        'gv1.Columns.Add(repoTxt)

        'repoTxt = New GridViewTextBoxColumn()
        'repoTxt.FormatString = ""
        'repoTxt.HeaderText = colDCSTransporterCode
        'repoTxt.Name = colDCSTransporterCode
        'repoTxt.IsVisible = False
        'repoTxt.ReadOnly = True
        'repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        'gv1.Columns.Add(repoTxt)

        'repoTxt = New GridViewTextBoxColumn()
        'repoTxt.FormatString = ""
        'repoTxt.HeaderText = colDCSTransporterName
        'repoTxt.Name = colDCSTransporterName
        'repoTxt.IsVisible = False
        'repoTxt.ReadOnly = True
        'repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        'gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colDCSTransporterGroupCode
        repoTxt.Name = clsMasterDefault.colDCSTransporterGroupCode
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colDCSVLCCode
        repoTxt.Name = clsMasterDefault.colDCSVLCCode
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colDCSVillageName
        repoTxt.Name = clsMasterDefault.colDCSVillageName
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colDCSVSPCode
        repoTxt.Name = clsMasterDefault.colDCSVSPCode
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colDCSVSPName
        repoTxt.Name = clsMasterDefault.colDCSVSPName
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colDCSVSPAddress
        repoTxt.Name = clsMasterDefault.colDCSVSPAddress
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colDCSState
        repoTxt.Name = clsMasterDefault.colDCSState
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colDCSVSPGroupCode
        repoTxt.Name = clsMasterDefault.colDCSVSPGroupCode
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colDCSCreatecustomer
        repoTxt.Name = clsMasterDefault.colDCSCreatecustomer
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colDCSCustomerGroupCode
        repoTxt.Name = clsMasterDefault.colDCSCustomerGroupCode
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colVSPPaymentType
        repoTxt.Name = clsMasterDefault.colVSPPaymentType
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colDCSBankCode
        repoTxt.Name = clsMasterDefault.colDCSBankCode
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colDCSBankName
        repoTxt.Name = clsMasterDefault.colDCSBankName
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colDCSIFSCCode
        repoTxt.Name = clsMasterDefault.colDCSIFSCCode
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colDCSBranchName
        repoTxt.Name = clsMasterDefault.colDCSBranchName
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colDCSAccountNo
        repoTxt.Name = clsMasterDefault.colDCSAccountNo
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colDCSBuffalowTIP
        repoTxt.Name = clsMasterDefault.colDCSBuffalowTIP
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colDCSCowTIP
        repoTxt.Name = clsMasterDefault.colDCSCowTIP
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMPCode
        repoTxt.Name = clsMasterDefault.colMPCode
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMPFatherName
        repoTxt.Name = clsMasterDefault.colMPFatherName
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMPAddress1
        repoTxt.Name = clsMasterDefault.colMPAddress1
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMPAddress2
        repoTxt.Name = clsMasterDefault.colMPAddress2
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMPZila
        repoTxt.Name = clsMasterDefault.colMPZila
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMPTehsil
        repoTxt.Name = clsMasterDefault.colMPTehsil
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMPCityCode
        repoTxt.Name = clsMasterDefault.colMPCityCode
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMPStateCode
        repoTxt.Name = clsMasterDefault.colMPStateCode
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMPCountryCode
        repoTxt.Name = clsMasterDefault.colMPCountryCode
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMPPinCode
        repoTxt.Name = clsMasterDefault.colMPPinCode
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMPEmail
        repoTxt.Name = clsMasterDefault.colMPEmail
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMPDateofBirth
        repoTxt.Name = clsMasterDefault.colMPDateofBirth
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMPEducation
        repoTxt.Name = clsMasterDefault.colMPEducation
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMPLandHolding
        repoTxt.Name = clsMasterDefault.colMPLandHolding
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMPNoOfMilchAnimal
        repoTxt.Name = clsMasterDefault.colMPNoOfMilchAnimal
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMPTotalMilkProduction
        repoTxt.Name = clsMasterDefault.colMPTotalMilkProduction
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMPMilkForSelfConsumption
        repoTxt.Name = clsMasterDefault.colMPMilkForSelfConsumption
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMPMilkForSale
        repoTxt.Name = clsMasterDefault.colMPMilkForSale
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMPPayeeName
        repoTxt.Name = clsMasterDefault.colMPPayeeName
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMPBankCityCode
        repoTxt.Name = clsMasterDefault.colMPBankCityCode
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMPBankStateCode
        repoTxt.Name = clsMasterDefault.colMPBankStateCode
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMPCustomerAccSet
        repoTxt.Name = clsMasterDefault.colMPCustomerAccSet
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMPVendorAccSet
        repoTxt.Name = clsMasterDefault.colMPVendorAccSet
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = clsMasterDefault.colMPTOLERANCE
        repoTxt.Name = clsMasterDefault.colMPTOLERANCE
        repoTxt.IsVisible = False
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)



        Dim repoNum As New GridViewDecimalColumn
        repoNum.FormatString = ""
        repoNum.HeaderText = "Valid"
        repoNum.Name = colOK
        repoNum.Width = 100
        repoNum.IsVisible = True
        repoNum.ReadOnly = False
        repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(repoNum)

        repoTxt = New GridViewTextBoxColumn()
        repoTxt.FormatString = ""
        repoTxt.HeaderText = colError
        repoTxt.Name = colError
        repoTxt.Width = 1000
        repoTxt.ReadOnly = True
        repoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(repoTxt)





        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
        Next
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.ShowFilteringRow = True
        gv1.EnableFiltering = True
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.ShowFilteringRow = True


        ''//Set default values
        If True Then

        End If
    End Sub

    Private Sub rbtnReset_Click(sender As Object, e As EventArgs) Handles rbtnReset.Click
        Dim frm As New frmMakeTempleteImportMP(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, FromID)
        frm.dtColsExcel = New DataTable
        frm.dtColsExcel.Columns.Add("Code", GetType(String))
        For ii As Integer = 0 To gv1.Columns.Count - 1
            If gv1.Columns(ii).IsVisible Then
                If gv1.Columns(ii) Is gv1.Columns(colOK) Then
                    Continue For
                End If
                If gv1.Columns(ii) Is gv1.Columns(colError) Then
                    Continue For
                End If
                Dim dr As DataRow = frm.dtColsExcel.NewRow()
                dr("Code") = gv1.Columns(ii).Name
                frm.dtColsExcel.Rows.Add(dr)
            End If
        Next
        frm.arrColsOrginal = New Dictionary(Of String, Boolean)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCUploaderCode, True)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCBMCC, True)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCName, True)
        frm.arrColsOrginal.Add(clsMasterDefault.colDCSUploaderCode, True)
        frm.arrColsOrginal.Add(clsMasterDefault.colDCSVLCName, True)
        frm.arrColsOrginal.Add(clsMasterDefault.colDCSType, True)
        frm.arrColsOrginal.Add(clsMasterDefault.colMPUploaderCode, True)
        frm.arrColsOrginal.Add(clsMasterDefault.colMPName, True)
        frm.arrColsOrginal.Add(clsMasterDefault.colMPTelphone, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMPAadharNo, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMPJanAadharNo, SettJanAadharNoMandatory)
        frm.arrColsOrginal.Add(clsMasterDefault.colMPBankCode, True)
        frm.arrColsOrginal.Add(clsMasterDefault.colMPBankBranch, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMPIFSCCode, True)
        frm.arrColsOrginal.Add(clsMasterDefault.colMPAccountNo, True)
        frm.strDocNoForOpen = txtTemplete.Value
        frm.IsDefaultValue = False
        frm.ShowDialog()
    End Sub

    Private Sub RadButton2_Click_1(sender As Object, e As EventArgs) Handles RadButton2.Click
        Dim frm As New frmMakeTempleteImportMP(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, FromID)
        frm.arrColsOrginal = New Dictionary(Of String, Boolean)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCCode, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCType, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCChillingVendorCode, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCAddress1, True)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCAddress2, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCTehsil, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCCity, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCState, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCCountry, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCPinCode, True)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCTelphone, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCEmail, True)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCFax, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMccSuperArea, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCAreaOfStore, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCAreaOfOffice, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCOpenAreaForTanker, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCAreaOfLab, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCTotalStorageCapacity, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCAreaOfReceivingDock, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCDripSaver, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCCanWasher, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCCanScrubber, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCFssaiNo, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCETP, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCEarthing, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCCoilLength, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCElectricityConnection, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCBoiler, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCIndustryType, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCPropName, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCPartnerName, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCDirectorName, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCMonthlyProvision, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCChillingCharges, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCChillingOn, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCChillingMinGuaranteedAvgQty, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCChillingOnUOMKGLTR, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCChillingOnQty, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCChillingOnUOMHandledDispatched, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCChillingMinGuaranteedPeriod, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCChillingMinGuaranteedPeriodUOM, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCRateofLeaseCharges, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCAgreement_Status, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCAgreement_Date, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCAgreementExpiryDate, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCSecurity_Status, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCCheque_Amt, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCCheque_No, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCCheque_Date, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCChillingStartingDate, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCIsTruckSheetMandatory, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCWeighingComPort, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCSampleComPort, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCPaymentCycle, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCIncentiveCode, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCShiftMorningOpeningTime, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCShiftMorningClosingTime, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCShiftEveningOpeningTime, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCShiftEveningClosingTime, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCRM, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCRequiredGateEntry, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCAllowAutoMilkIn, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCAutoIn_Location, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCSILOIn_Location, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCApplyReceiptWeightTolerance, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCReceiptWeightToleranceValue, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCApplyFailedSample, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCFailedSampleFAT, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCFailedSampleSNF, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCLocSegmentCode, False)

        frm.arrColsOrginal.Add(clsMasterDefault.colMCCCommissionRate, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCCommissionMinimumShiftInPaymentCycle, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCCommissionMinimumQtyInShift, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCCommissionNoOfPaymentCycleForNewVSP, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCDeductionMinimumFATPer, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCDeductionMinimumSNFPer, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCDeductionNoOfPaymentCycleForNewVSP, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCRateofLeasedChargesUOM, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCAreaofStoreUOM, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCAreaOfReceivingDockUOM, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCAreaOfOfficeUOM, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCAreaOfLabUOM, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCOpenAreaForTankerUOM, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMccSuperAreaUOM, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCWeighingMachine, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCSampleMachine, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCPlant, True)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCMorningShiftOpeningTime, True)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCMorningShiftClosingTime, True)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCEveningShiftOpeningTime, True)
        frm.arrColsOrginal.Add(clsMasterDefault.colMCCEveningShiftClosingTime, True)

        'frm.arrColsOrginal.Add(colDCSRouteCode, True)
        'frm.arrColsOrginal.Add(colDCSRouteName, True)
        'frm.arrColsOrginal.Add(colDCSRouteDistance, False)
        'frm.arrColsOrginal.Add(colDCSRouteEffectiveStartDate, False)
        'frm.arrColsOrginal.Add(colDCSVehicle, True)
        'frm.arrColsOrginal.Add(colDCSVehiclePaymentBasis, False)
        'frm.arrColsOrginal.Add(colDCSPaymentPerKm, False)
        'frm.arrColsOrginal.Add(colDCSVehicleEffectiveStartDate, False)
        'frm.arrColsOrginal.Add(colDCSTransporterCode, True)
        'frm.arrColsOrginal.Add(colDCSTransporterName, True)

        frm.arrColsOrginal.Add(clsMasterDefault.colDCSTransporterGroupCode, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colDCSVLCCode, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colDCSVillageName, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colDCSVSPCode, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colDCSVSPName, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colDCSVSPAddress, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colDCSState, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colDCSVSPGroupCode, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colDCSCreatecustomer, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colDCSCustomerGroupCode, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colVSPPaymentType, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colDCSBankCode, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colDCSBankName, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colDCSIFSCCode, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colDCSBranchName, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colDCSAccountNo, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colDCSBuffalowTIP, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colDCSCowTIP, False)


        frm.arrColsOrginal.Add(clsMasterDefault.colMPCode, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMPFatherName, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMPAddress1, True)
        frm.arrColsOrginal.Add(clsMasterDefault.colMPAddress2, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMPZila, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMPTehsil, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMPCityCode, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMPStateCode, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMPCountryCode, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMPPinCode, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMPEmail, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMPDateofBirth, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMPEducation, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMPLandHolding, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMPNoOfMilchAnimal, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMPTotalMilkProduction, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMPMilkForSelfConsumption, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMPMilkForSale, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMPPayeeName, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMPBankCityCode, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMPBankStateCode, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMPCustomerAccSet, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMPVendorAccSet, False)
        frm.arrColsOrginal.Add(clsMasterDefault.colMPTOLERANCE, False)

        frm.strDocNoForOpen = txtTemplete.Value
        frm.IsDefaultValue = True
        frm.ShowDialog()
    End Sub
End Class
