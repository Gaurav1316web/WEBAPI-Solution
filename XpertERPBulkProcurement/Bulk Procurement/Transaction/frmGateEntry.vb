' Created By Pankaj Jha on 24/06/204 Against Ticket No: BM00000002720
''richa agarwal BM00000009808,BM00000009809
'======================Modifiy by preeti gupta against ticket no[BM00000008127,ALF/06/08/18-000079]
'' Parteek ticket no BM00000009863 ,SWA/13/08/18-000042
' script created by priti SWA/27/08/18-000047 for bulk procurement insert data chamber wise
Imports System.Data.SqlClient
Imports common
Imports System.IO

Public Class FrmGateEntry
    Inherits FrmMainTranScreen
    Dim FinalChamberwise As Integer = 0
    Dim ShowFATSNFPerOnBulkProcInGateIN As Integer = 0
    Dim ForceToSelectIteminGateEntry As Integer = 0
    Dim GateEntryChamberwisewithManualTankerEntry As Integer = 0
    Dim AllowtoChangeFatANdSNFPerforHighClassVendorinGE As Boolean = False
    Dim ShowGateEntryType As Integer = 0
    Dim AllowBulkProcMCCwithoutTankerDispatch As Integer = 0
    Dim allowManualrate As Integer = 0
    Dim AllowGateEntryAgainstPO As Integer = 0
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isIntimationReqd As Integer = 0
    Dim AllowJobWorkonGateEntryBulkProc As Integer = 0
    Dim TankerFromMaster As Integer = 0
    Dim MCCChamberwise As Integer = 0
    Dim CreateMCCTankerGateOutBasedOnBulkRouteMaster As Boolean = False
    Dim CreateProvisionOfTankerDispatchWithClosingKM As Boolean = False
    Dim CreateProvisionforBulkContractorInGateIn As Boolean = False
    Public Const colSlNo As String = "SLNO"
    Public Const colChamberSealNo As String = "colChamberSealNo"
    Public Const colItemCode As String = "ItemCode"
    Public Const colItemDesc As String = "ItemDesc"
    Public Const colRate As String = "colRate"
    Public Const colQty As String = "Qty"
    Public Const colUOM As String = "UOM"
    Public Const colHSN As String = "HSN"
    Public Const colDIPValue As String = "colDIPValue"
    Public Const ColSealNo As String = "ColSealNo"
    Public Const colFat As String = "colFAT"
    Public Const colSNF As String = "colSNF"
    Public Const colFatKG As String = "colFATKG"
    Public Const colSNFKG As String = "colSNFKG"
    Public Const colChamberDesc As String = "colChamberDesc"
    Public Const colMilkTypeCode As String = "colMilkTypeCode"
    Public Const colDIPStatus As String = "colDIPStatus"
    Public Const colSampleLifted As String = "colSampleLifted"
    Public strDocCode As String = ""
    Public isCellValueChangedOpen = False
    Public strLoggedInTo As String = String.Empty ' It Will Store Either MCC or Plant as Login Location
    Public strLoginMccOrPlantCode As String = String.Empty 'It Will store the Location Code of Currently Logged In Mcc or Pant
    Public strLoginMccOrPlantDesc As String = String.Empty 'It Will store the Location Desc of Currently Logged In Mcc or Pant
    Public errorControl As clsErrorControl = New clsErrorControl()
    Public obj As clsGateEntry = Nothing
    Dim insideLoadData As Boolean = False
    Dim isHideRateDispatchCentreCode As Boolean = False
    Dim ShowTankerWithoutCheckingAnyValidation As Boolean = False
    Dim intBulkProcRunOneTypeGateEntry As Integer
    Dim isHighClass As String = ""

    Private Sub FrmGateEntry_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim qry As String = ""

        ForceToSelectIteminGateEntry = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ForceToSelectIteminGateEntry, clsFixedParameterCode.ForceToSelectIteminGateEntry, Nothing))
        GateEntryChamberwisewithManualTankerEntry = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GateEntryChamberwisewithManualTankerEntry, clsFixedParameterCode.GateEntryChamberwisewithManualTankerEntry, Nothing))
        ShowGateEntryType = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowGateEntryTypeonGateEntryBulkProc, clsFixedParameterCode.ShowGateEntryTypeonGateEntryBulkProc, Nothing))
        AllowBulkProcMCCwithoutTankerDispatch = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowBulkProcMCCwithoutTankerDispatch, clsFixedParameterCode.AllowBulkProcMCCwithoutTankerDispatch, Nothing))
        AllowGateEntryAgainstPO = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowGateEntryAgainstPO, clsFixedParameterCode.AllowGateEntryAgainstPO, Nothing))
        allowManualrate = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowManualPriceONBulkPO, clsFixedParameterCode.AllowManualPriceONBulkPO, Nothing))
        ShowTankerWithoutCheckingAnyValidation = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowTankerWithoutCheckingAnyValidation, clsFixedParameterCode.ShowTankerWithoutCheckingAnyValidation, Nothing)) = 0, False, True)
        AllowtoChangeFatANdSNFPerforHighClassVendorinGE = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowtoChangeFatANdSNFPerforHighClassVendorinGE, clsFixedParameterCode.AllowtoChangeFatANdSNFPerforHighClassVendorinGE, Nothing)) = 0, False, True)
        CreateMCCTankerGateOutBasedOnBulkRouteMaster = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateMCCTankerGateOutBasedOnBulkRouteMaster, clsFixedParameterCode.CreateMCCTankerGateOutBasedOnBulkRouteMaster, Nothing)) = 1, True, False)
        CreateProvisionOfTankerDispatchWithClosingKM = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateProvisionOfTankerDispatchWithClosingKM, clsFixedParameterCode.CreateProvisionOfTankerDispatchWithClosingKM, Nothing)) = 0, False, True)
        CreateProvisionforBulkContractorInGateIn = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateProvisionforBulkContractorInGateIn, clsFixedParameterCode.CreateProvisionforBulkContractorInGateIn, Nothing)) = 0, False, True)
        SetUserMgmtNew()
        'If clsERPFuncationality.isCurrentUserMCC() Then
        '    chkBulkMilkProc.Enabled = False
        '    chkMccProc.IsChecked = True
        'Else
        '    chkBulkMilkProc.Enabled = True
        '    chkBulkMilkProc.IsChecked = True
        'End If
        MCCChamberwise = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsChamberWiseTanker, clsFixedParameterCode.IsChamberWiseTanker, Nothing))

        AllowJobWorkonGateEntryBulkProc = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowJobWorkonGateEntryBulkProc, clsFixedParameterCode.AllowJobWorkonGateEntryBulkProc, Nothing))
        isIntimationReqd = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.isIntimationRequired, clsFixedParameterCode.isIntimationRequired, Nothing))
        TankerFromMaster = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GateEntryTankerFromTankerMaster, clsFixedParameterCode.GateEntryTankerFromTankerMaster, Nothing))

        intBulkProcRunOneTypeGateEntry = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.BulkProcRunOneTypeGateEntry, clsFixedParameterCode.BulkProcRunOneTypeGateEntry, Nothing))
        If intBulkProcRunOneTypeGateEntry = 1 Then
            grpGateEntryType.Visible = False
            chkBulkMilkProc.IsChecked = True
        ElseIf intBulkProcRunOneTypeGateEntry = 2 Then
            grpGateEntryType.Visible = False
            chkMccProc.IsChecked = True
        Else
            grpGateEntryType.Visible = True
        End If
        'If AllowJobWorkonGateEntryBulkProc = 1 Then
        chkJobWork.Visible = False
        ' End If
        If TankerFromMaster = 0 AndAlso isIntimationReqd = 0 Then
            SplitContainer4.SplitterDistance = 170
            Panel1.Visible = False
        Else
            Panel1.Visible = True
        End If
        If isIntimationReqd = 1 Then
            lblIntimationNo.Visible = True
            labIntimation.Visible = True
        Else
            lblIntimationNo.Visible = False
            labIntimation.Visible = False
        End If

        LoadStatus()
        LoadGEType()
        reset()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S to Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D to Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C to Close The Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N For New")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P To Post Transaction")
        If clsCommon.myLen(strDocCode) > 0 Then
            Dim DocType As String = clsDBFuncationality.getSingleValue("select doc_type from tspl_gate_entry_details where gate_entry_no='" & strDocCode & "'")
            If clsCommon.CompairString(DocType, "BulkProc") = CompairStringResult.Equal Then
                chkBulkMilkProc.IsChecked = True
            Else
                chkMccProc.IsChecked = True
            End If
            LoadData(strDocCode, DocType, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            Dim DocType As String = clsDBFuncationality.getSingleValue("select doc_type from tspl_gate_entry_details where gate_entry_no='" & clsCommon.myCstr(Me.Tag) & "'")
            If clsCommon.CompairString(DocType, "BulkProc") = CompairStringResult.Equal Then
                chkBulkMilkProc.IsChecked = True
            Else
                chkMccProc.IsChecked = True
            End If
            LoadData(clsCommon.myCstr(Me.Tag), DocType, NavigatorType.Current)
        End If
        isHideRateDispatchCentreCode = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.HideRateDispatchCentreCode, clsFixedParameterCode.HideRateDispatchCentreCode, Nothing))
        If isHideRateDispatchCentreCode = True Then
            MyLabel3.Visible = False
            txtDispatchCentreCode.Visible = False
        End If
        If ShowGateEntryType = 1 Then
            Panel4.Visible = True
        Else
            Panel4.Visible = False
        End If
        '==========update by preeti Gupta Agaisnt ticket no[ERO/18/07/19-000957]
        ShowFATSNFPerOnBulkProcInGateIN = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowFATSNFPerOnBulkProcInGateIN, clsFixedParameterCode.ShowFATSNFPerOnBulkProcInGateIN, Nothing))
        If IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowGenerateReferenceNoForBulkGateEntry, clsFixedParameterCode.AllowGenerateReferenceNoForBulkGateEntry, Nothing)) = 0, False, True) = True Then
            dtpDateAndTimeBulk.Enabled = False
        Else
            dtpDateAndTimeBulk.Enabled = True
        End If
        If IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TollTaxMaster, clsFixedParameterCode.TollTaxMaster, Nothing)) = 0, False, True) = False Then
            MyLabel25.Visible = True
            txtTollAmount.Visible = True
            btnUpdateAfterPost.Enabled = False
            btnClKM.Enabled = False
        Else
            MyLabel25.Visible = False
            txtTollAmount.Visible = False
            btnUpdateAfterPost.Enabled = True
            btnClKM.Enabled = True
        End If
        If AllowBulkProcMCCwithoutTankerDispatch = 1 Then
            lblRoute.Visible = True
            txtRoute.Visible = True
            chkNetWeight.Checked = True
        Else
            lblRoute.Visible = False
            txtRoute.Visible = False
        End If
    End Sub

    Private Sub chkBulkMilkProc_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkBulkMilkProc.ToggleStateChanged
        'done by priti ERO/10/05/18-000306
        If chkBulkMilkProc.IsChecked Then
            If TankerFromMaster = 1 Then
                FinalChamberwise = 1
            Else
                FinalChamberwise = 0
            End If
        ElseIf chkMccProc.IsChecked Then
            If MCCChamberwise = 1 Then
                FinalChamberwise = 1
            Else
                FinalChamberwise = 0
            End If
        End If

        If chkBulkMilkProc.IsChecked Then
            fndChallanNoMcc.Visible = False
            txtChallanNoBulk.Visible = True
            txtChallanNoBulk.Text = "ND"
            If AllowGateEntryAgainstPO = 0 Then
                fndVendorBulk.Enabled = True
            End If
            lblVendorBulk.Text = "Vendor"
            dtpChallanDateBulk.Enabled = True
            txtTankerNoBulk.Visible = True
            chkTankerReturn.Visible = True
            If AllowJobWorkonGateEntryBulkProc = 1 Then
                chkJobWork.Visible = True
                chkJobWork.ReadOnly = False
                txtSubLocation.Enabled = True
            End If
            chkNetWeight.Checked = False
            chkNetWeight.Enabled = False
        ElseIf chkMccProc.IsChecked Then

            lblVendorBulk.Text = "From MCC"
            If AllowJobWorkonGateEntryBulkProc = 1 Then
                chkJobWork.Visible = True
                chkJobWork.ReadOnly = True
                txtVendorCode.ReadOnly = True
                txtSubLocation.Enabled = False

            End If
            fndChallanNoMcc.Visible = True
            txtChallanNoBulk.Visible = False
            txtChallanNoBulk.Text = ""
            fndVendorBulk.Enabled = True
            dtpChallanDateBulk.Enabled = False
            txtTankerNoBulk.Visible = False
            chkTankerReturn.Visible = False
            If AllowBulkProcMCCwithoutTankerDispatch = 1 Then
                fndChallanNoMcc.Visible = False
                txtChallanNoBulk.Visible = True
                txtChallanNoBulk.Text = ""
                txtTankerNoBulk.Visible = True
                fndTankerNo.Visible = False
                txtTankerNoBulk.ReadOnly = False
                dtpChallanDateBulk.Enabled = True
            End If
            chkNetWeight.Enabled = True
        End If

        If chkBulkMilkProc.IsChecked OrElse chkMccProc.IsChecked Then
            reset()
            loadBlankGvItemBulk()
            If TankerFromMaster = 1 And GateEntryChamberwisewithManualTankerEntry = 0 Then
                txtTankerNoBulk.Visible = False
                fndTankerNo.Visible = True
            End If
        End If
        ' done by priti BHA/02/07/18-000118 tanker no manually in chamber wise bulk proc.
        If GateEntryChamberwisewithManualTankerEntry = 1 And chkBulkMilkProc.IsChecked = True And TankerFromMaster = 1 Then
            Panel5.Visible = True
        Else
            Panel5.Visible = False
        End If
        If chkBulkMilkProc.IsChecked = True AndAlso CreateProvisionforBulkContractorInGateIn = True Then
            chkAgainstGateOutNo.Visible = True
            txtGateOutNo.Visible = True
            lblGateOut.Visible = True
        Else
            chkAgainstGateOutNo.Visible = False
            txtGateOutNo.Visible = False
            lblGateOut.Visible = False
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        'Me.Close()
        'Me.Dispose()
        'GC.Collect()
        clsERPFuncationality.closeForm(Me)
    End Sub

    Private Sub fndLocationBulk__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndLocationBulk._MYValidating
        Dim strLocations = String.Empty
        If Not clsMccMaster.isCurrentUserHO() Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                strLocations = "and location_code in (" & objCommonVar.strCurrUserLocations & ")"
            End If
        End If
        fndLocationBulk.Value = clsLocation.getFinder("(type='PLANT' or Location_category='MCC')" & strLocations, fndLocationBulk.Value, isButtonClicked)
        If clsCommon.myLen(fndLocationBulk.Value) > 0 Then
            lblLocationDecBulk.Text = clsLocation.GetName(fndLocationBulk.Value, Nothing)
        Else
            lblLocationDecBulk.Text = ""
        End If
        strLocations = Nothing
    End Sub
    Sub UpdateCurrentLoginStatus()
        Dim strQry As String = "select case when isnull(Type,'')='PLANT' then type when ISNULL(Location_Category ,'')='MCC' then  Location_Category else 'NA'end    from TSPL_LOCATION_MASTER left outer join  tspl_user_master on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where TSPL_USER_MASTER.User_Code ='" & objCommonVar.CurrentUserCode & "'"
        strLoggedInTo = clsDBFuncationality.getSingleValue(strQry)
        If clsCommon.CompairString(strLoggedInTo, "MCC") = CompairStringResult.Equal Or clsCommon.CompairString(strLoggedInTo, "PLANT") = CompairStringResult.Equal Then
            Dim qry As String = "select location_Code  from TSPL_LOCATION_MASTER left outer join  tspl_user_master on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where TSPL_USER_MASTER.User_Code ='" & objCommonVar.CurrentUserCode & "'"
            strLoginMccOrPlantCode = clsDBFuncationality.getSingleValue(qry)
            strLoginMccOrPlantDesc = clsLocation.GetName(strLoginMccOrPlantCode, Nothing)
        Else
            strLoginMccOrPlantCode = "NA"
            strLoginMccOrPlantDesc = "NA"
        End If
        'lblLoggedMccOrPlantName.Text = strLoginMccOrPlantDesc & "(Location: " & strLoggedInTo & ")"
        strQry = Nothing
    End Sub
    Private Sub fndVendorBulk__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndVendorBulk._MYValidating

        Try
            If chkBulkMilkProc.IsChecked Then
                If FinalChamberwise = 1 Then
                    If ShowGateEntryType = 1 Then
                        If clsCommon.myLen(cmbGEType.SelectedValue) <= 0 Then
                            cmbGEType.Focus()
                            Throw New Exception("Please select Gate Entry Type")
                        End If
                    End If
                    If clsCommon.CompairString(cmbGEType.SelectedValue, "P") = CompairStringResult.Equal Then
                        fndVendorBulk.Value = clsVendorMaster.getFinder(" TSPL_VENDOR_MASTER.Vendor_Type_CHA in ('M') AND Status='N' ", fndVendorBulk.Value, isButtonClicked)
                    ElseIf clsCommon.CompairString(cmbGEType.SelectedValue, "J") = CompairStringResult.Equal Then
                        fndVendorBulk.Value = clsVendorMaster.getFinder(" TSPL_VENDOR_MASTER.Vendor_Type_CHA in ('J') AND Status='N'  ", fndVendorBulk.Value, isButtonClicked)
                    ElseIf ShowGateEntryType = 0 Then
                        fndVendorBulk.Value = clsVendorMaster.getFinder(" TSPL_VENDOR_MASTER.Vendor_Type_CHA in ('M') AND Status='N' ", fndVendorBulk.Value, isButtonClicked)
                    End If

                Else
                    fndVendorBulk.Value = clsVendorMaster.getFinder(" TSPL_VENDOR_MASTER.Vendor_Type in ('A','B')  AND Status='N' ", fndVendorBulk.Value, isButtonClicked)
                End If

                If clsCommon.myLen(fndVendorBulk.Value) > 0 Then
                    lblVendorNameBulk.Text = clsVendorMaster.GetName(fndVendorBulk.Value, Nothing)
                Else
                    lblVendorNameBulk.Text = ""
                End If
            ElseIf chkMccProc.IsChecked Then
                fndVendorBulk.Value = clsLocation.getFinder(" Location_category='MCC' ", fndVendorBulk.Value, isButtonClicked)
                If clsCommon.myLen(fndVendorBulk.Value) > 0 Then
                    lblVendorNameBulk.Text = clsLocation.GetName(fndVendorBulk.Value, Nothing)
                Else
                    lblVendorNameBulk.Text = ""
                End If
            End If
            ' done by priti BHA/21/06/18-000074 
            ' changes done for bharat based on weight BHA/31/07/18-000205
            If GateEntryChamberwisewithManualTankerEntry = 1 Then
                Dim qry = "select TSPL_VENDOR_MASTER.Bulk_ROUTE_NO,TSPL_BULK_ROUTE_MASTER.Distance,TSPL_BULK_ROUTE_MASTER.Rate, " &
                    "TSPL_BULK_ROUTE_MASTER.Amount,isnull(TSPL_BULK_ROUTE_MASTER.Weight,0) as Weight FROM TSPL_VENDOR_MASTER left join TSPL_BULK_ROUTE_MASTER on " &
                    "TSPL_VENDOR_MASTER.Bulk_ROUTE_NO=TSPL_BULK_ROUTE_MASTER.ROUTE_NO where Vendor_Code='" & clsCommon.myCstr(fndVendorBulk.Value) & "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    txtBulkRouteNo.Text = clsCommon.myCstr(dt.Rows(0)("Bulk_ROUTE_NO"))
                    txtdistance.Text = clsCommon.myCdbl(dt.Rows(0)("Distance"))
                    txtRate.Text = clsCommon.myCdbl(dt.Rows(0)("Rate"))
                    txtAmount.Text = clsCommon.myCdbl(dt.Rows(0)("Amount"))
                    txtWeight.Text = clsCommon.myCdbl(dt.Rows(0)("Weight"))
                End If
            End If
            Try
                clsDBFuncationality.ExecuteNonQuery("update TSPL_DOCPREFIX_MASTER set Location_Code = ''   where Doc_Type = 'Gate Entry'  and Doc_Trans_Type = 'Reference No'")
            Catch ex As Exception

            End Try
            isHighClass = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select case when isnull(IsHighClass,0)=0 then 'N' else 'Y' end from tspl_vendor_master where Vendor_code='" & fndVendorBulk.Value & "' "))
            If clsCommon.CompairString(isHighClass, "Y") = CompairStringResult.Equal Then
                gvItemBulk.Columns(colUOM).ReadOnly = False
            Else
                gvItemBulk.Columns(colUOM).ReadOnly = True
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub


    Private Sub txtTankerNoBulk_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTankerNoBulk.KeyPress
        If (Asc(e.KeyChar) >= 48 AndAlso Asc(e.KeyChar) <= 57) OrElse (Asc(e.KeyChar) >= 65 AndAlso Asc(e.KeyChar) <= 90) OrElse (Asc(e.KeyChar) >= 97 AndAlso Asc(e.KeyChar) <= 122) OrElse Asc(e.KeyChar) = Keys.Back OrElse Asc(e.KeyChar) = Keys.Delete OrElse Asc(e.KeyChar) = 22 OrElse Asc(e.KeyChar) = 3 Then
        Else
            e.Handled = True
        End If
    End Sub
    Sub reset()
        BtnResetProv.Enabled = False
        lblClosingDate.Text = ""
        txtOpeningKM.Text = 0
        txtClosingKM.Text = 0
        txtTollAmount.Text = 0
        chkNetWeight.Checked = False
        txtWeight.Text = ""
        lblRefrenceNo.Text = ""
        txtCAN.Value = 0
        txtNoofChamber.Value = 0
        txtBulkRouteNo.Text = ""
        txtRate.Text = ""
        txtdistance.Text = ""
        txtAmount.Text = ""
        txtTransport.Text = ""
        txtTransportName.Text = ""
        txtProvisionNo.Text = ""

        txtSubLocation.Enabled = True
        Panel3.Enabled = True
        chkJobWork.Checked = False
        txtSubLocation.Value = ""
        Panel3.Visible = False
        txtVendorCode.Text = ""
        txtvndrname.Text = ""
        txtPO.Value = ""
        lblPOBalanceQty.Text = 0
        lblPOQty.Text = 0
        btnUpdateFatSnfForContractor.Visible = False
        txtPriceCode.Enabled = False
        btnUpdatePrice.Visible = False
        Panel2.Visible = False
        chkBulkMilkProc.Enabled = True
        chkMccProc.Enabled = True
        fndTankerNo.Enabled = True
        txtSealValue.Text = ""
        lblTotalQTy.Text = 0
        txtSupplierCode.Value = ""
        lblSupplierName.Text = ""
        fndTankerNo.Enabled = True
        lblIntimationNo.Text = ""
        txtMilktypeCode.Value = ""
        lblMilkTypeCode.Text = ""
        lblMilkType.Text = ""
        cmbSealStatus.SelectedValue = ""
        cmbGEType.SelectedValue = ""
        txtDispatchCentreCode.Text = ""
        fndLocationBulk.Enabled = True
        fndVendorBulk.Enabled = True
        fndTankerNo.Value = ""
        fndGateEntryNO.Value = ""
        btnReverse.Visible = False
        fndGateEntryNO.MyReadOnly = False
        Dim dt As Date = clsCommon.GETSERVERDATE(Nothing, "dd/MM/yyyy hh:mm:ss tt")
        fndChallanNoMcc.Value = ""
        txtChallanNoBulk.Text = ""
        txtChallanNoBulk.ReadOnly = False
        dtpChallanDateBulk.Value = dt
        fndLocationBulk.Value = ""
        lblLocationDecBulk.Text = ""
        fndVendorBulk.Value = ""
        lblVendorNameBulk.Text = ""
        txtTankerNoBulk.Text = ""
        loadBlankGv()
        btnSave.Enabled = True
        btnSave.Text = "Save"
        btnPost.Enabled = False
        btn_amendment.Enabled = False
        btnUpdateFatSnfForContractor.Visible = False
        btnDelete.Enabled = False
        dtpDateAndTimeBulk.Value = dt
        dtpDateAndTimeBulk.Enabled = True
        lblPending.Status = ERPTransactionStatus.Pending
        UpdateCurrentLoginStatus()
        fndChallanNoMcc.Enabled = False
        '=========Added by preeti gupta==================
        Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, Nothing)

        If DateTime = "1" Then
            dtpDateAndTimeBulk.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Else
            dtpDateAndTimeBulk.CustomFormat = "dd/MM/yyyy"
        End If
        '==========================================================
        If chkBulkMilkProc.IsChecked Then
            dtpChallanDateBulk.ReadOnly = False
            txtTankerNoBulk.ReadOnly = False
            fndTankerNo.Visible = False
            RadPageView1.Pages("RadPageViewPage2").Item.Visibility = ElementVisibility.Hidden
            RadPageView1.Pages("RadPageViewPage2").Item.Visibility = ElementVisibility.Collapsed
            If AllowJobWorkonGateEntryBulkProc = 1 Then
                chkJobWork.Visible = True
            End If

        Else
            RadPageView1.Pages("RadPageViewPage2").Item.Visibility = ElementVisibility.Visible
            loadBlankGridManualSeal()
            loadBlankGridPaperSeal()
            fndTankerNo.Visible = True
            txtTankerNoBulk.Visible = False
            dtpChallanDateBulk.ReadOnly = True
            txtTankerNoBulk.ReadOnly = True
            If AllowBulkProcMCCwithoutTankerDispatch = 1 Then
                fndChallanNoMcc.Visible = False
                txtChallanNoBulk.Visible = True
                txtChallanNoBulk.Text = ""
                txtTankerNoBulk.Visible = True
                txtTankerNoBulk.ReadOnly = False
                fndTankerNo.Visible = False
                dtpChallanDateBulk.Enabled = True
            End If
        End If
        If FinalChamberwise = 1 And chkBulkMilkProc.IsChecked And GateEntryChamberwisewithManualTankerEntry = 0 Then
            txtTankerNoBulk.Visible = False
            fndTankerNo.Visible = True
            loadBlankGvItemBulk()

        End If
        If GateEntryChamberwisewithManualTankerEntry = 1 And chkBulkMilkProc.IsChecked = True And FinalChamberwise = 1 Then
            Panel5.Visible = True
        Else
            Panel5.Visible = False
        End If
        If AllowGateEntryAgainstPO = 1 Then
            If chkBulkMilkProc.IsChecked Then
                Panel2.Visible = True
                fndVendorBulk.Enabled = False
                fndLocationBulk.Enabled = False
                cmbGEType.Enabled = False
                txtMilktypeCode.Enabled = False
            Else
                Panel2.Visible = False
                fndVendorBulk.Enabled = True
                fndLocationBulk.Enabled = True
                cmbGEType.Enabled = True
                txtMilktypeCode.Enabled = True
            End If
        End If
        If FinalChamberwise = 0 Then
            If ShowGateEntryType = 1 And chkBulkMilkProc.IsChecked Then
                cmbGEType.Enabled = True
            Else
                cmbGEType.Enabled = False
            End If
        ElseIf FinalChamberwise = 1 Then
            If chkBulkMilkProc.IsChecked Then
                cmbGEType.Enabled = True
            Else
                cmbGEType.Enabled = False
            End If
        End If
        chkAgainstGateOutNo.Visible = False
        txtGateOutNo.Visible = False
        lblGateOut.Visible = False

        chkTankerReturn.Checked = False
        If chkBulkMilkProc.IsChecked = True AndAlso CreateProvisionforBulkContractorInGateIn = True Then
            chkAgainstGateOutNo.Visible = True
            txtGateOutNo.Visible = True
            lblGateOut.Visible = True
        Else
            chkAgainstGateOutNo.Visible = False
            txtGateOutNo.Visible = False
            lblGateOut.Visible = False
        End If
        chkAgainstGateOutNo.Checked = False
        txtGateOutNo.Value = ""
        txtRoute.Value = ""
        If AllowBulkProcMCCwithoutTankerDispatch = 1 Then
            chkNetWeight.Checked = True
        End If
        dt = Nothing
        fndLocationBulk.Value = clsGateEntry.getUsersDefaultLocation()
        lblLocationDecBulk.Text = clsLocation.GetName(fndLocationBulk.Value, Nothing)
        RadPageView1.SelectedPage = RadPageViewPage1
        FindAndRestoreGridLayout(Me)
        'FindAndSetTabStopFalse(Me)
        'ReStoreGridLayout()
    End Sub
    Sub loadBlankGv()
        If chkBulkMilkProc.IsChecked Then
            loadBlankGvItemBulk()
        Else
            loadBlankGvItemMCC()
        End If
        'ReStoreGridLayout()
    End Sub
    Sub loadBlankGvItemBulk()
        gvItemBulk.Tag = "Bulk"
        gvItemBulk.Rows.Clear()
        gvItemBulk.Columns.Clear()
        gvItemBulk.DataSource = Nothing
        gvItemBulk.Columns.Add(colSlNo, "SL. NO.")
        gvItemBulk.Columns(colSlNo).Width = 60
        gvItemBulk.Columns(colSlNo).ReadOnly = True

        gvItemBulk.Columns.Add(colItemCode, "Item Code")
        gvItemBulk.Columns(colItemCode).Width = 100
        gvItemBulk.Columns(colItemCode).HeaderImage = My.Resources.search4
        gvItemBulk.Columns(colItemCode).TextImageRelation = TextImageRelation.TextBeforeImage
        gvItemBulk.Columns(colItemCode).ReadOnly = False

        gvItemBulk.Columns.Add(colItemDesc, "Item Desc")
        gvItemBulk.Columns(colItemDesc).Width = 320
        gvItemBulk.Columns(colItemDesc).ReadOnly = True


        gvItemBulk.Columns.Add(colChamberDesc, "Chamber Desc")
        gvItemBulk.Columns(colChamberDesc).Width = 150
        gvItemBulk.Columns(colChamberDesc).ReadOnly = IIf(GateEntryChamberwisewithManualTankerEntry = 1, False, True)
        gvItemBulk.Columns(colChamberDesc).IsVisible = False

        gvItemBulk.Columns.Add(colMilkTypeCode, "Milk Type")
        gvItemBulk.Columns(colMilkTypeCode).Width = 150
        gvItemBulk.Columns(colMilkTypeCode).HeaderImage = My.Resources.search4
        gvItemBulk.Columns(colMilkTypeCode).TextImageRelation = TextImageRelation.TextBeforeImage
        gvItemBulk.Columns(colMilkTypeCode).ReadOnly = True
        gvItemBulk.Columns(colMilkTypeCode).IsVisible = False

        gvItemBulk.Columns.Add(colUOM, "UOM")
        gvItemBulk.Columns(colUOM).Width = 100
        gvItemBulk.Columns(colUOM).HeaderImage = My.Resources.search4
        gvItemBulk.Columns(colUOM).TextImageRelation = TextImageRelation.TextBeforeImage
        gvItemBulk.Columns(colUOM).ReadOnly = True

        gvItemBulk.Columns.Add(colHSN, "HSN Code")
        gvItemBulk.Columns(colHSN).Width = 100
        gvItemBulk.Columns(colHSN).ReadOnly = True

        Dim repoNumBox As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNumBox.HeaderText = "Challan Qty"
        repoNumBox.Name = colQty
        repoNumBox.Width = 150
        repoNumBox.ReadOnly = False
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItemBulk.MasterTemplate.Columns.Add(repoNumBox)

        gvItemBulk.Columns.Add(colRate, "Rate")
        gvItemBulk.Columns(colRate).Width = 150
        gvItemBulk.Columns(colRate).ReadOnly = False
        If allowManualrate = 1 Then
            gvItemBulk.Columns(colRate).IsVisible = True
        Else
            gvItemBulk.Columns(colRate).IsVisible = False
        End If
        If isHideRateDispatchCentreCode = True Then
            gvItemBulk.Columns(colRate).IsVisible = False
        End If

        gvItemBulk.Columns.Add(colFat, "FAT (%)")
        gvItemBulk.Columns(colFat).Width = 100
        gvItemBulk.Columns(colFat).ReadOnly = False
        gvItemBulk.Columns(colFat).IsVisible = False

        gvItemBulk.Columns.Add(colSNF, "SNF (%)")
        gvItemBulk.Columns(colSNF).Width = 100
        gvItemBulk.Columns(colSNF).ReadOnly = False
        gvItemBulk.Columns(colSNF).IsVisible = False

        gvItemBulk.Columns.Add(colFatKG, "FAT (KG)")
        gvItemBulk.Columns(colFatKG).Width = 100
        gvItemBulk.Columns(colFatKG).ReadOnly = True
        gvItemBulk.Columns(colFatKG).IsVisible = False

        gvItemBulk.Columns.Add(colSNFKG, "SNF (KG)")
        gvItemBulk.Columns(colSNFKG).Width = 100
        gvItemBulk.Columns(colSNFKG).ReadOnly = True
        gvItemBulk.Columns(colSNFKG).IsVisible = False


        Dim repoRowType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoRowType.FormatString = ""
        repoRowType.HeaderText = "Status"
        repoRowType.Name = colDIPStatus
        repoRowType.Width = 80
        repoRowType.ReadOnly = False
        repoRowType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoRowType.DataSource = LoadDIPStatus()
        repoRowType.ValueMember = "Code"
        repoRowType.DisplayMember = "Name"
        repoRowType.IsVisible = False
        gvItemBulk.MasterTemplate.Columns.Add(repoRowType) '2

        gvItemBulk.Columns.Add(colDIPValue, "DIP Value")
        gvItemBulk.Columns(colDIPValue).Width = 100
        gvItemBulk.Columns(colDIPValue).ReadOnly = False
        gvItemBulk.Columns(colDIPValue).IsVisible = False

        gvItemBulk.Columns.Add(colChamberSealNo, "Seal No")
        gvItemBulk.Columns(colChamberSealNo).Width = 100
        gvItemBulk.Columns(colChamberSealNo).ReadOnly = False
        gvItemBulk.Columns(colChamberSealNo).IsVisible = False



        Dim repoRowType1 As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoRowType1.FormatString = ""
        repoRowType1.HeaderText = "Sample Lifted"
        repoRowType1.Name = colSampleLifted
        repoRowType1.Width = 80
        repoRowType1.ReadOnly = False
        repoRowType1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoRowType1.DataSource = LoadSampleLifted()
        repoRowType1.ValueMember = "Code"
        repoRowType1.DisplayMember = "Name"
        repoRowType1.IsVisible = False
        gvItemBulk.MasterTemplate.Columns.Add(repoRowType1) '2

        If FinalChamberwise = 1 Then
            gvItemBulk.Columns(colFat).IsVisible = True
            gvItemBulk.Columns(colSNF).IsVisible = True
            repoRowType.IsVisible = True
            repoRowType1.IsVisible = True
            gvItemBulk.Columns(colDIPValue).IsVisible = True
            gvItemBulk.Columns(colChamberSealNo).IsVisible = True
            gvItemBulk.Columns(colChamberDesc).IsVisible = True
            gvItemBulk.Columns(colMilkTypeCode).IsVisible = True
        End If
        If (ShowFATSNFPerOnBulkProcInGateIN = 1 AndAlso chkBulkMilkProc.IsChecked = True) Then
            gvItemBulk.Columns(colFat).IsVisible = True
            gvItemBulk.Columns(colSNF).IsVisible = True
            gvItemBulk.Columns(colFatKG).IsVisible = True
            gvItemBulk.Columns(colSNFKG).IsVisible = True
        End If

        gvItemBulk.Rows.AddNew()
        gvItemBulk.Rows(0).Cells(colSlNo).Value = "1"
        RadGroupBox1.Width = Me.Width - 200
        gvItemBulk.AllowAddNewRow = False
        'gvItemBulk.AllowColumnReorder = False
        gvItemBulk.AllowDeleteRow = False
        gvItemBulk.AllowRowReorder = False
        gvItemBulk.ShowGroupPanel = False
        'gvItemBulk.AllowColumnChooser = False
        gvItemBulk.EnableFiltering = False
        gvItemBulk.EnableSorting = False
        gvItemBulk.EnableGrouping = False
    End Sub
    Sub LoadGEType()
        cmbGEType.DataSource = Nothing
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = Nothing
        dr = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "None"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "P"
        dr("Name") = "Purchase"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "J"
        dr("Name") = "Job Work"
        dt.Rows.Add(dr)

        cmbGEType.DataSource = dt
        cmbGEType.DisplayMember = "Name"
        cmbGEType.ValueMember = "Code"
    End Sub
    Sub LoadStatus()
        cmbSealStatus.DataSource = Nothing
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = Nothing
        dr = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Y"
        dr("Name") = "OK"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "N"
        dr("Name") = "Not Ok"
        dt.Rows.Add(dr)

        cmbSealStatus.DataSource = dt
        cmbSealStatus.DisplayMember = "Name"
        cmbSealStatus.ValueMember = "Code"
    End Sub
    Private Function LoadDIPStatus() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = Nothing
        dr = dt.NewRow()
        dr("Code") = "F"
        dr("Name") = "Full"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "H"
        dr("Name") = "Half"
        dt.Rows.Add(dr)
        Return dt
    End Function
    Private Function LoadSampleLifted() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = Nothing
        dr = dt.NewRow()
        dr("Code") = "Y"
        dr("Name") = "Yes"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "N"
        dr("Name") = "No"
        dt.Rows.Add(dr)
        Return dt
    End Function
    Sub loadBlankGvItemMCC()
        gvItemBulk.Tag = "MCC"
        gvItemBulk.Rows.Clear()
        gvItemBulk.Columns.Clear()
        gvItemBulk.DataSource = Nothing
        gvItemBulk.Columns.Add(colSlNo, "SL. NO.")
        gvItemBulk.Columns(colSlNo).Width = 60
        gvItemBulk.Columns(colSlNo).ReadOnly = True

        gvItemBulk.Columns.Add(colItemCode, "Item Code")
        gvItemBulk.Columns(colItemCode).Width = 100
        gvItemBulk.Columns(colItemCode).ReadOnly = IIf(AllowBulkProcMCCwithoutTankerDispatch = 1, False, True)


        gvItemBulk.Columns.Add(colItemDesc, "Item Desc")
        gvItemBulk.Columns(colItemDesc).Width = 320
        gvItemBulk.Columns(colItemDesc).ReadOnly = True

        gvItemBulk.Columns.Add(colChamberDesc, "Chamber Desc")
        gvItemBulk.Columns(colChamberDesc).Width = 150
        gvItemBulk.Columns(colChamberDesc).ReadOnly = True
        gvItemBulk.Columns(colChamberDesc).IsVisible = False
        gvItemBulk.Columns(colChamberDesc).ReadOnly = IIf(AllowBulkProcMCCwithoutTankerDispatch = 1, False, True)

        gvItemBulk.Columns.Add(colMilkTypeCode, "Milk Type")
        gvItemBulk.Columns(colMilkTypeCode).Width = 150
        gvItemBulk.Columns(colMilkTypeCode).HeaderImage = My.Resources.search4
        gvItemBulk.Columns(colMilkTypeCode).TextImageRelation = TextImageRelation.TextBeforeImage
        gvItemBulk.Columns(colMilkTypeCode).ReadOnly = True
        gvItemBulk.Columns(colMilkTypeCode).IsVisible = False

        gvItemBulk.Columns.Add(colUOM, "UOM")
        gvItemBulk.Columns(colUOM).Width = 100
        gvItemBulk.Columns(colUOM).ReadOnly = IIf(AllowBulkProcMCCwithoutTankerDispatch = 1, False, True)

        gvItemBulk.Columns.Add(colHSN, "HSN Code")
        gvItemBulk.Columns(colHSN).Width = 100
        gvItemBulk.Columns(colHSN).ReadOnly = True

        Dim repoNumBox As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNumBox.HeaderText = "Challan Qty"
        repoNumBox.Name = colQty
        repoNumBox.Width = 150
        repoNumBox.ReadOnly = IIf(AllowBulkProcMCCwithoutTankerDispatch = 1, False, True)
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItemBulk.MasterTemplate.Columns.Add(repoNumBox)

        gvItemBulk.Columns.Add(colFat, "FAT (%)")
        gvItemBulk.Columns(colFat).Width = 100
        gvItemBulk.Columns(colFat).ReadOnly = False
        'gvItemBulk.Columns(colFat).IsVisible = False

        gvItemBulk.Columns.Add(colSNF, "SNF (%)")
        gvItemBulk.Columns(colSNF).Width = 100
        gvItemBulk.Columns(colSNF).ReadOnly = False
        'gvItemBulk.Columns(colSNF).IsVisible = False

        gvItemBulk.Columns.Add(colFatKG, "FAT (KG)")
        gvItemBulk.Columns(colFatKG).Width = 100
        gvItemBulk.Columns(colFatKG).ReadOnly = True
        'gvItemBulk.Columns(colFatKG).IsVisible = False

        gvItemBulk.Columns.Add(colSNFKG, "SNF (KG)")
        gvItemBulk.Columns(colSNFKG).Width = 100
        gvItemBulk.Columns(colSNFKG).ReadOnly = True
        'gvItemBulk.Columns(colSNFKG).IsVisible = False

        Dim repoRowType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoRowType.FormatString = ""
        repoRowType.HeaderText = "Status"
        repoRowType.Name = colDIPStatus
        repoRowType.Width = 80
        repoRowType.ReadOnly = False
        repoRowType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoRowType.DataSource = LoadDIPStatus()
        repoRowType.ValueMember = "Code"
        repoRowType.DisplayMember = "Name"
        repoRowType.IsVisible = False
        gvItemBulk.MasterTemplate.Columns.Add(repoRowType) '2

        gvItemBulk.Columns.Add(colDIPValue, "DIP Value")
        gvItemBulk.Columns(colDIPValue).Width = 100
        gvItemBulk.Columns(colDIPValue).ReadOnly = False
        gvItemBulk.Columns(colDIPValue).IsVisible = False

        gvItemBulk.Columns.Add(colChamberSealNo, "Seal No")
        gvItemBulk.Columns(colChamberSealNo).Width = 100
        gvItemBulk.Columns(colChamberSealNo).ReadOnly = False
        gvItemBulk.Columns(colChamberSealNo).IsVisible = False



        Dim repoRowType1 As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoRowType1.FormatString = ""
        repoRowType1.HeaderText = "Sample Lifted"
        repoRowType1.Name = colSampleLifted
        repoRowType1.Width = 80
        repoRowType1.ReadOnly = False
        repoRowType1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoRowType1.DataSource = LoadSampleLifted()
        repoRowType1.ValueMember = "Code"
        repoRowType1.DisplayMember = "Name"
        repoRowType1.IsVisible = False
        gvItemBulk.MasterTemplate.Columns.Add(repoRowType1) '2

        If (MCCChamberwise = 1 AndAlso chkMccProc.IsChecked = True) Then
            gvItemBulk.Columns(colFat).IsVisible = True
            gvItemBulk.Columns(colSNF).IsVisible = True
            repoRowType.IsVisible = True
            repoRowType1.IsVisible = True
            gvItemBulk.Columns(colDIPValue).IsVisible = True
            gvItemBulk.Columns(colChamberSealNo).IsVisible = True
            gvItemBulk.Columns(colMilkTypeCode).IsVisible = True
            gvItemBulk.Columns(colChamberDesc).IsVisible = True
        End If
        If AllowBulkProcMCCwithoutTankerDispatch = 1 Then
            gvItemBulk.Columns(colFat).IsVisible = True
            gvItemBulk.Columns(colSNF).IsVisible = True
        End If
        gvItemBulk.Rows.AddNew()
        gvItemBulk.Rows(0).Cells(colSlNo).Value = "1"
        'RadGroupBox4.Width = Me.Width - 200
        gvItemBulk.AllowAddNewRow = False
        'gvItemBulk.AllowColumnReorder = False
        gvItemBulk.AllowDeleteRow = False
        gvItemBulk.AllowRowReorder = False
        gvItemBulk.ShowGroupPanel = False
        'gvItemBulk.AllowColumnChooser = False
        gvItemBulk.EnableFiltering = False
        gvItemBulk.EnableSorting = False
        gvItemBulk.EnableGrouping = False

    End Sub



    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset()
    End Sub

    Private Sub FrmGateEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F2 AndAlso gvItemBulk.CurrentCell IsNot Nothing Then
            If Not isCellValueChangedOpen Then
                isCellValueChangedOpen = True
                If gvItemBulk.CurrentColumn Is gvItemBulk.Columns(colItemCode) AndAlso chkBulkMilkProc.IsChecked Then
                    gvItemBulk.CurrentRow.Cells(colItemCode).Value = clsItemMaster.getFinder("Product_Type ='mi'", gvItemBulk.CurrentRow.Cells(colItemCode).Value, True)
                    gvItemBulk.CurrentRow.Cells(colItemCode).EndEdit()
                    If clsCommon.myLen(gvItemBulk.CurrentRow.Cells(colItemCode).Value) > 0 Then
                        gvItemBulk.CurrentRow.Cells(colItemDesc).Value = clsCommon.myCstr(clsItemMaster.GetItemName(gvItemBulk.CurrentRow.Cells(colItemCode).Value, Nothing))
                        gvItemBulk.CurrentRow.Cells(colUOM).Value = clsItemMaster.GetStockUnit(gvItemBulk.CurrentRow.Cells(colItemCode).Value, Nothing)
                    Else
                        gvItemBulk.CurrentRow.Cells(colItemDesc).Value = ""
                        gvItemBulk.CurrentRow.Cells(colUOM).Value = ""
                    End If
                End If
            End If
            isCellValueChangedOpen = False
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnReset.Enabled Then
            btnReset_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            btnSave_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled AndAlso MyBase.isPostFlag Then
            btnPost_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled AndAlso MyBase.isDeleteFlag Then
            btnDelete_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            btnClose_Click(sender, e)
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine +
                                              "tspl_Gate_entry_details " + Environment.NewLine +
                                              "TSPL_Gate_Entry_Chember_Details (  Only in case of chamber wise setting ON) " + Environment.NewLine +
                                              "TSPL_GATE_ENTRY_DETAILS_HISTORY ( For History) " + Environment.NewLine +
                                              "TSPL_Gate_Entry_Price_Chart ( For Price chart mapped with gate entry If chamber setting is ON and contractor type.) " + Environment.NewLine +
                                              "IsChamberWiseTanker - Chamber wise setiing for MCC " + Environment.NewLine +
                                              "GateEntryTankerFromTankerMaster - Chamber wise setiing for Contract tanker  ")

            Dim frm As New FrmPWD(Nothing)
            frm.strType = "SIRC"
            frm.strCode = "SIReversAndCreate"
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnReverse.Visible = True
                BtnResetProv.Visible = True
            End If
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F11 Then
            If AllowAmendmentWithPasssword(MyBase.Form_ID, Nothing) Then
                btn_amendment.Visible = True
            Else
                btn_amendment.Visible = False
            End If
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F10 Then
            If AllowtoChangeFatANdSNFPerforHighClassVendorinGE Then

                Dim frm As New FrmPWD(Nothing)
                frm.strType = "PwdAllowtoChangeFatANdSNFPerforHighClassVendorinGE"
                frm.strCode = "PwdAllowtoChangeFatANdSNFPerforHighClassVendorinGE"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnUpdateFatSnfForContractor.Visible = True

                End If
            Else
                btnUpdateFatSnfForContractor.Visible = False

            End If
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F3 Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = "GEUpdateAfterPost"
            frm.strCode = "GEUpdateAfterPost"
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnUpdate.Visible = True
                chkBulkMilkProc.Enabled = False
                chkMccProc.Enabled = False
                fndTankerNo.Enabled = False
            End If
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F9 Then
            If clsCommon.myLen(fndGateEntryNO.Value) > 0 AndAlso clsCommon.myLen(txtPriceCode.Value) Then
                Dim strSRNNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 SRN_NO from TSPL_Bulk_MILK_SRN where gate_entry_no='" & fndGateEntryNO.Value & "'"))
                If clsCommon.myLen(strSRNNo) = 0 Then
                    Dim frm As New FrmPWD(Nothing)
                    frm.strType = "GEUpdatePriceChart"
                    frm.strCode = "GEUpdatePriceChart"
                    frm.ShowDialog()
                    If frm.isPasswordCorrect Then
                        btnUpdatePrice.Visible = True
                        txtPriceCode.Enabled = True
                    End If
                Else
                    clsCommon.MyMessageBoxShow("You cannot change Price code. Bulk milk SRN is already created.")
                End If
            End If
        End If
    End Sub


    Public Sub SetUserMgmtNew()
        Try
            ''MyBase.SetUserMgmt(clsUserMgtCode.frmGateEntry)
            If Not (MyBase.isReadFlag) Then
                Throw New Exception("Permission Denied")

            End If
            btnSave.Visible = MyBase.isModifyFlag
            btnDelete.Visible = MyBase.isDeleteFlag
            If MyBase.isReverse Then
                btnReverse.Enabled = True
                BtnResetProv.Enabled = True
            Else
                btnReverse.Enabled = False
                BtnResetProv.Enabled = False
            End If
            btnPost.Visible = MyBase.isPostFlag
            btnPrint.Visible = MyBase.isPrintFlag
            btn_amendment.Visible = MyBase.isAmendmentFlag
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub btnReverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReverse.Click
        Try
            If common.clsCommon.MyMessageBoxShow("Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                If clsGateEntry.ReverseAndUnpost(fndGateEntryNO.Value) Then
                    common.clsCommon.MyMessageBoxShow("Successfully Reversed and Recreated", Me.Text)
                    LoadData(fndGateEntryNO.Value, IIf(chkMccProc.IsChecked, "MccProc", "BulkProc"), NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub gvItemBulk_CellEndEdit(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvItemBulk.CellEndEdit
        If Not isCellValueChangedOpen Then
            isCellValueChangedOpen = True
            If e.Column Is gvItemBulk.Columns(colMilkTypeCode) AndAlso chkBulkMilkProc.IsChecked Then
                gvItemBulk.CurrentRow.Cells(colMilkTypeCode).Value = clsMilkTypeMaster.getFinder("", gvItemBulk.CurrentRow.Cells(colMilkTypeCode).Value, False)
            End If
            ' done by priti BHA/21/06/18-000078 for Different item in chamber wise.
            If e.Column Is gvItemBulk.Columns(colItemCode) AndAlso (chkBulkMilkProc.IsChecked OrElse AllowBulkProcMCCwithoutTankerDispatch = 1) Then
                Dim whr As String = " Product_Type ='mi' and exists (select  1 from tspl_item_uom_detail where tspl_item_uom_detail.Item_Code=TSPL_ITEM_MASTER.Item_Code and UOM_Code='KG')"
                gvItemBulk.CurrentRow.Cells(colItemCode).Value = clsItemMaster.getFinder("Product_Type ='mi'", gvItemBulk.CurrentRow.Cells(colItemCode).Value, False)
                If clsCommon.myLen(gvItemBulk.CurrentRow.Cells(colItemCode).Value) > 0 Then
                    gvItemBulk.CurrentRow.Cells(colItemDesc).Value = clsCommon.myCstr(clsItemMaster.GetItemName(gvItemBulk.CurrentRow.Cells(colItemCode).Value, Nothing))
                    gvItemBulk.CurrentRow.Cells(colUOM).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select UOM_Code   from TSPL_ITEM_UOM_DETAIL where Item_Code='" & gvItemBulk.CurrentRow.Cells(colItemCode).Value & "' and Default_UOM='1' "))
                    gvItemBulk.CurrentRow.Cells(colHSN).Value = clsItemMaster.GetItemHSNCode(gvItemBulk.CurrentRow.Cells(colItemCode).Value, Nothing)
                    If clsCommon.myLen(gvItemBulk.CurrentRow.Cells(colUOM).Value) <= 0 Then
                        gvItemBulk.CurrentRow.Cells(colUOM).Value = clsItemMaster.GetStockUnit(gvItemBulk.CurrentRow.Cells(colItemCode).Value, Nothing)
                    End If

                Else

                    gvItemBulk.CurrentRow.Cells(colItemDesc).Value = ""
                    gvItemBulk.CurrentRow.Cells(colUOM).Value = ""
                    gvItemBulk.CurrentRow.Cells(colQty).Value = "0"
                    gvItemBulk.CurrentRow.Cells(colFat).Value = ""
                    gvItemBulk.CurrentRow.Cells(colFatKG).Value = ""
                    gvItemBulk.CurrentRow.Cells(colSNF).Value = ""
                    gvItemBulk.CurrentRow.Cells(colSNFKG).Value = ""
                End If
            ElseIf e.Column Is gvItemBulk.Columns(colUOM) AndAlso (chkBulkMilkProc.IsChecked OrElse AllowBulkProcMCCwithoutTankerDispatch = 1) Then
                OpenUOMList(False)
            ElseIf e.Column Is gvItemBulk.Columns(colFat) Or e.Column Is gvItemBulk.Columns(colQty) Then
                ''richa show qty from totalqty_in_kg column in gate entry table in case of MCC PRoc 27 June,2019
                If (TankerFromMaster = 1 OrElse isIntimationReqd = 1 OrElse MCCChamberwise = 1) Then
                    If clsCommon.myCdbl(gvItemBulk.CurrentRow.Cells(colQty).Value) > 0 Then
                        UpdateTotal()
                    End If
                End If
                gvItemBulk.Rows(0).Cells(colFatKG).Value = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colFat).Value) * clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colQty).Value) / 100
                gvItemBulk.Rows(0).Cells(colSNFKG).Value = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colSNF).Value) * clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colQty).Value) / 100
            ElseIf e.Column Is gvItemBulk.Columns(colSNF) Or e.Column Is gvItemBulk.Columns(colQty) Then
                gvItemBulk.Rows(0).Cells(colFatKG).Value = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colFat).Value) * clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colQty).Value) / 100
                gvItemBulk.Rows(0).Cells(colSNFKG).Value = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colSNF).Value) * clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colQty).Value) / 100
            End If
        End If
        isCellValueChangedOpen = False
    End Sub
    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gvItemBulk.CurrentRow.Cells(colItemCode).Value)
        If clsCommon.myLen(strICode) > 0 Then
            'Dim qry As String = "select UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
            'Dim whrCls As String = "Item_Code='" + strICode + "'"
            'gvItemBulk.CurrentRow.Cells(colUOM).Value = clsCommon.ShowSelectForm("MilkBulkGateIn@fndnder", qry, "Code", whrCls, clsCommon.myCstr(gvItemBulk.CurrentRow.Cells(colItemCode).Value), "Code", isButtonClick)

            Dim qry As String = "select TSPL_ITEM_UOM_DETAIL.UOM_Code as Code , TSPL_UNIT_MASTER.Unit_Desc as [Description]  from TSPL_ITEM_UOM_DETAIL left outer join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code "
            Dim whrCls As String = "Item_Code='" + strICode + "'"
            gvItemBulk.CurrentRow.Cells(colUOM).Value = clsCommon.ShowSelectForm("MilkBulkGateIn@fndnder", qry, "Code", whrCls, clsCommon.myCstr(gvItemBulk.CurrentRow.Cells(colItemCode).Value), "Code", isButtonClick)
            If clsCommon.myLen(clsCommon.myCstr(gvItemBulk.CurrentRow.Cells(colUOM).Value)) <= 0 Then
                gvItemBulk.CurrentRow.Cells(colUOM).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select UOM_Code   from TSPL_ITEM_UOM_DETAIL where Item_Code='" & gvItemBulk.CurrentRow.Cells(colItemCode).Value & "' and Default_UOM='1' "))
            End If
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            Dim arr As List(Of String) = New List(Of String)
            If clsCommon.myLen(fndGateEntryNO.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Enter  Gate Entry No To delete ")
            Else
                Dim isUsed As Integer = clsDBFuncationality.getSingleValue("select SUM(row_Count ) from (select COUNT(*) as row_Count from  TSPL_Weighment_Detail where gate_entry_no='" & fndGateEntryNO.Value & "' union all select COUNT(*) as row_Count from tspl_quality_check where gate_entry_no='" & fndGateEntryNO.Value & "') xx ")
                If isUsed > 0 Then
                    clsCommon.MyMessageBoxShow("Gate Entry No is in use")
                    Exit Sub
                End If
                If myMessages.deleteConfirm() Then
                    arr.Add(fndGateEntryNO.Value)
                    If clsERPFuncationalityOLD.AddToHistory(arr, clsUserMgtCode.frmGateEntry, Nothing) Then
                        If clsGateEntry.deleteData(fndGateEntryNO.Value, Nothing) Then
                            reset()
                            myMessages.delete()
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub
    Function allowToSave() As Boolean
        Try
            '===============Preeti Gupta==================================
            If AllowFutureDateTransaction(dtpDateAndTimeBulk.Value, Nothing) = False Then
                dtpDateAndTimeBulk.Select()
                Return False
            End If
            '=======================================================
            If chkBulkMilkProc.IsChecked = False AndAlso chkMccProc.IsChecked = False Then
                errorControl.SetError(chkBulkMilkProc, "Please Select Gate Entry Type Either Bulk Procurement Or Mcc Procurement ")
                Throw New Exception("Please Select Gate Entry Type Either Bulk Procurement Or Mcc Procurement ")
            Else
                errorControl.SetError(chkBulkMilkProc, "")
            End If

            If CreateProvisionforBulkContractorInGateIn = True And chkAgainstGateOutNo.Checked = True And chkBulkMilkProc.IsChecked = True Then
                If clsCommon.myLen(txtGateOutNo.Value) <= 0 Then
                    errorControl.SetError(txtGateOutNo, "Please select the Gate Out No.It is mandatory ")
                    txtGateOutNo.Focus()
                    Throw New Exception("Please select the Gate Out No.It is mandatory")
                Else
                    errorControl.SetError(txtGateOutNo, "")
                End If
            End If
            If chkBulkMilkProc.IsChecked Then
                If clsCommon.myLen(fndLocationBulk.Value) <= 0 Then
                    errorControl.SetError(fndLocationBulk, "Please select the location.It is mandatory ")
                    fndLocationBulk.Focus()
                    Throw New Exception("Please select the location.It is mandatory")
                Else
                    errorControl.SetError(fndLocationBulk, "")
                End If
                If clsCommon.myLen(fndVendorBulk.Value) <= 0 Then
                    errorControl.SetError(fndVendorBulk, "Please select the vendor.It is mandatory ")
                    fndVendorBulk.Focus()
                    Throw New Exception("Please select the vendor.It is manadatory")
                Else
                    errorControl.SetError(fndVendorBulk, "")
                End If

                If clsCommon.myLen(txtTankerNoBulk.Text) <= 0 Then
                    errorControl.SetError(txtTankerNoBulk, "Please enter the tanker no. It is mandatory ")
                    txtTankerNoBulk.Focus()
                    Throw New Exception("Please enter the tanker no. It is mandatory ")
                Else
                    errorControl.SetError(txtTankerNoBulk, "")
                End If
                If ShowGateEntryType = 1 Then
                    If clsCommon.CompairString(cmbGEType.SelectedValue, "") = CompairStringResult.Equal Then
                        errorControl.SetError(cmbGEType, "Please Select gate entry type ")
                        cmbGEType.Focus()
                        Throw New Exception("Please Select gate entry type ")
                    Else
                        errorControl.SetError(cmbGEType, "")
                    End If
                    ''richa agarwal 02 Jan,2019 BHA/02/01/19-000772
                    If clsCommon.CompairString(cmbGEType.SelectedValue, "P") = CompairStringResult.Equal Then
                        If clsCommon.myLen(clsCommon.myCstr(txtBulkRouteNo.Text)) > 0 Then
                            Dim strTransportTanker As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Tanker_Id from TSPL_TRANSPORT__TANKER_DETAILS where Tanker_Id ='" & clsCommon.myCstr(txtTankerNoBulk.Text) & "'"))
                            If clsCommon.myLen(strTransportTanker) <= 0 Then
                                errorControl.SetError(txtTankerNoBulk, "Please enter Tanker ")
                                txtTankerNoBulk.Focus()
                                Throw New Exception("Please enter only those Tanker's which are entered in Tanker Transporter Master.")
                            End If
                        End If
                    End If
                    ''--------------------------
                End If
                If (TankerFromMaster = 1 OrElse isIntimationReqd = 1) AndAlso chkBulkMilkProc.IsChecked = True AndAlso ShowGateEntryType = 1 Then

                    If clsCommon.CompairString(cmbGEType.SelectedValue, "P") <> CompairStringResult.Equal Then
                        If clsCommon.myLen(txtSupplierCode.Value) <= 0 Then
                            errorControl.SetError(txtSupplierCode, "Please select the Supplier.It is mandatory ")
                            txtSupplierCode.Focus()

                            Throw New Exception("Please select the Supplier.It is mandatory")
                        Else
                            errorControl.SetError(txtSupplierCode, "")
                        End If
                    End If

                    If clsCommon.myLen(txtMilktypeCode.Value) <= 0 Then
                        errorControl.SetError(txtMilktypeCode, "Please select the MilkType.It is mandatory ")
                        txtMilktypeCode.Focus()
                        Throw New Exception("Please select the MilkType.It is mandatory")
                    Else
                        errorControl.SetError(txtMilktypeCode, "")
                    End If
                    If isHideRateDispatchCentreCode = False Then
                        If clsCommon.myLen(txtDispatchCentreCode.Text) <= 0 Then
                            errorControl.SetError(txtDispatchCentreCode, "Please enter Dispatch Centre No ")
                            txtDispatchCentreCode.Focus()
                            Throw New Exception("Please enter Dispatch Centre No ")
                        Else
                            errorControl.SetError(txtDispatchCentreCode, "")
                        End If
                    End If

                    If GateEntryChamberwisewithManualTankerEntry = 1 Then
                        If clsCommon.myCdbl(txtNoofChamber.Value) = 0 Then
                            txtNoofChamber.Focus()
                            Throw New Exception("Please enter No of Chamber ")
                        End If
                        For ii As Integer = 0 To gvItemBulk.Rows.Count - 1
                            Dim strChamberDesc As String = clsCommon.myCstr(gvItemBulk.Rows(ii).Cells(colChamberDesc).Value)
                            If clsCommon.myLen(strChamberDesc) = 0 Then
                                Throw New Exception("Please enter Chamber Desc ")
                            End If
                            For jj As Integer = ii + 1 To gvItemBulk.Rows.Count - 1
                                Dim strInnnerChamberDesc As String = clsCommon.myCstr(gvItemBulk.Rows(jj).Cells(colChamberDesc).Value)
                                If clsCommon.CompairString(strChamberDesc, strInnnerChamberDesc) = CompairStringResult.Equal Then
                                    Throw New Exception("Chamber " + strChamberDesc + " is repeated at Row No" + clsCommon.myCstr(ii + 1) + " and " + clsCommon.myCstr(jj + 1))

                                End If
                            Next
                        Next
                    End If
                    Dim intcount As Integer = 0
                    For ii As Integer = 0 To gvItemBulk.Rows.Count - 1
                        Dim dblQty As Double = clsCommon.myCdbl(gvItemBulk.Rows(ii).Cells(colQty).Value)
                        If dblQty > 0 Then
                            intcount += 1
                        End If
                        Dim strChamberDesc As String = clsCommon.myCstr(gvItemBulk.Rows(ii).Cells(colChamberDesc).Value)
                    Next
                    If intcount = 0 Then
                        Throw New Exception("Please enter atleast one chamber qty. ")
                    ElseIf intcount <> gvItemBulk.Rows.Count Then
                        If clsCommon.MyMessageBoxShow("You have not entered qty for all chamber, Want to Continue", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                            Return True
                        Else
                            Return False
                        End If
                    End If
                    If AllowGateEntryAgainstPO = 1 And chkBulkMilkProc.IsChecked Then
                        lblPOBalanceQty.Text = GetBalancePOQty(txtPO.Value)
                        If clsCommon.myCdbl(lblTotalQTy.Text) > clsCommon.myCdbl(lblPOBalanceQty.Text) Then
                            Throw New Exception("Total Qty should not be greater than PO Balance Qty")
                        End If
                    End If
                End If

                '' Work done agaist ticket no. BHA/22/08/18-000475 client Bharat on 22/08/2018 

                If GateEntryChamberwisewithManualTankerEntry = 1 Then
                    If clsCommon.myCdbl(txtNoofChamber.Value) = 0 Then
                        txtNoofChamber.Focus()
                        Throw New Exception("Please enter No of Chamber ")
                    End If
                    For ii As Integer = 0 To gvItemBulk.Rows.Count - 1
                        Dim strChamberDesc As String = clsCommon.myCstr(gvItemBulk.Rows(ii).Cells(colChamberDesc).Value)
                        If clsCommon.myLen(strChamberDesc) = 0 Then
                            Throw New Exception("Please enter Chamber Desc ")
                        End If
                        For jj As Integer = ii + 1 To gvItemBulk.Rows.Count - 1
                            Dim strInnnerChamberDesc As String = clsCommon.myCstr(gvItemBulk.Rows(jj).Cells(colChamberDesc).Value)
                            If clsCommon.CompairString(strChamberDesc, strInnnerChamberDesc) = CompairStringResult.Equal Then
                                Throw New Exception("Chamber " + strChamberDesc + " is repeated at Row No" + clsCommon.myCstr(ii + 1) + " and " + clsCommon.myCstr(jj + 1))

                            End If
                        Next
                    Next
                End If

                '' End code

                If clsCommon.myLen(txtChallanNoBulk.Text) <= 0 Then
                    errorControl.SetError(txtChallanNoBulk, "Please enter the challan no or 'ND' as challan no if there is 'No Document' ")
                    txtChallanNoBulk.Focus()
                    Throw New Exception("Please enter the challan no or 'ND' as challan no if there is 'No Document'")
                Else
                    errorControl.SetError(txtChallanNoBulk, "")
                End If

                '======Sanjeet=====================
                If chkJobWork.Checked Then
                    If clsCommon.myLen(txtSubLocation.Value) = 0 Then
                        Throw New Exception("Please Select Sub Location for Location - " & fndLocationBulk.Value & " ")
                    End If
                End If
                '================================

                If clsCommon.CompairString(txtChallanNoBulk.Text, "ND") = CompairStringResult.Equal Then

                Else
                    'If dtpChallanDateBulk.Value > dtpDateAndTimeBulk.Value Then
                    '    Throw New Exception("Challan date can't be greater than gate entry date")
                    'End If
                End If

                If clsCommon.myLen(gvItemBulk.Rows(0).Cells(colItemCode).Value) <= 0 Then
                    Throw New Exception("Please Enter Item Code At Row No 1 in Item Grid")
                End If

                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from tspl_item_master where item_code='" & gvItemBulk.Rows(0).Cells(colItemCode).Value & "'")) = 0 Then
                    Throw New Exception("Please Enter Valid Item Code At Row No 1 in Item Grid")
                End If

                If clsCommon.myLen(gvItemBulk.Rows(0).Cells(colQty).Value) <= 0 Then
                    If (Not clsCommon.CompairString(txtChallanNoBulk.Text, "ND") = CompairStringResult.Equal) AndAlso clsCommon.myLen(txtChallanNoBulk.Text) > 0 Then
                        Throw New Exception("Please Enter Item Qty At Row No 1 in Item Grid")
                    End If
                End If
                If IsNumeric(gvItemBulk.Rows(0).Cells(colQty).Value) = False Then
                    If (Not clsCommon.CompairString(txtChallanNoBulk.Text, "ND") = CompairStringResult.Equal) AndAlso clsCommon.myLen(txtChallanNoBulk.Text) > 0 Then
                        Throw New Exception(" Item Qty Must be a Number At Row No 1 in Item Grid")
                    End If
                End If

                If clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colQty).Value) <= 0 Then
                    If (Not clsCommon.CompairString(txtChallanNoBulk.Text, "ND") = CompairStringResult.Equal) AndAlso clsCommon.myLen(txtChallanNoBulk.Text) > 0 Then
                        Throw New Exception(" Item Qty Must be a Number and Not Zero or Negative At Row No 1 in Item Grid")
                    End If
                End If


            ElseIf chkMccProc.IsChecked Then
                If clsCommon.myLen(fndGateEntryNO.Value) = 0 Then
                    Dim intCount As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from Tspl_Gate_Entry_Details where Challan_No='" & fndChallanNoMcc.Value & "'"))
                    If intCount > 0 Then
                        Throw New Exception("Gate entry has been created against this challan no.")
                    End If
                End If


                If (clsCommon.myCDate(clsCommon.GetPrintDate(dtpDateAndTimeBulk.Value, "dd/MMM/yyyy"))) < (clsCommon.myCDate(clsCommon.GetPrintDate(dtpChallanDateBulk.Value, "dd/MMM/yyyy"))) Then
                    dtpDateAndTimeBulk.Focus()
                    Throw New Exception("Please enter the Gate entry Date Greater then Challan Date. ")
                Else

                End If
                If AllowBulkProcMCCwithoutTankerDispatch = 0 Then
                    If clsCommon.myLen(fndTankerNo.Value) <= 0 Then
                        errorControl.SetError(fndTankerNo, "Please enter the tanker no. It is mandatory ")
                        fndTankerNo.Focus()
                        Throw New Exception("Please enter the tanker no. It is mandatory ")
                    Else
                        errorControl.SetError(fndTankerNo, "")
                    End If
                End If

                If TankerFromMaster = 1 Then
                    If clsCommon.myLen(txtMilktypeCode.Value) <= 0 Then
                        errorControl.SetError(txtMilktypeCode, "Please select the MilkType.It is mandatory ")
                        txtMilktypeCode.Focus()
                        Throw New Exception("Please select the MilkType.It is mandatory")
                    Else
                        errorControl.SetError(txtMilktypeCode, "")
                    End If
                End If
                If AllowBulkProcMCCwithoutTankerDispatch = 0 Then
                    If clsCommon.myLen(fndChallanNoMcc.Value) <= 0 Then
                        errorControl.SetError(fndChallanNoMcc, "Please enter the challan no. It is mandatory ")
                        fndChallanNoMcc.Focus()
                        Throw New Exception("Please enter the challan no. It is mandatory ")
                    Else
                        errorControl.SetError(fndChallanNoMcc, "")
                    End If
                Else
                    If clsCommon.myLen(txtChallanNoBulk.Text) <= 0 Then
                        errorControl.SetError(txtChallanNoBulk, "Please enter the challan no or 'ND' as challan no if there is 'No Document' ")
                        txtChallanNoBulk.Focus()
                        Throw New Exception("Please enter the challan no or 'ND' as challan no if there is 'No Document'")
                    Else
                        errorControl.SetError(txtChallanNoBulk, "")
                    End If
                    Dim strExist As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Gate_Entry_No from Tspl_Gate_Entry_Details where Challan_No='" & txtChallanNoBulk.Text & "' and  Gate_Entry_No not in ('" & fndGateEntryNO.Value & "')", Nothing))
                    If clsCommon.myLen(strExist) > 0 Then
                        errorControl.SetError(txtChallanNoBulk, "Please enter another challan no .This challan no is alsready exist in Gate entry " & strExist)
                        txtChallanNoBulk.Focus()
                        Throw New Exception("Please enter another challan no .This challan no is already exist in Gate entry " & strExist)
                    End If

                    If clsCommon.myLen(fndLocationBulk.Value) <= 0 Then
                        errorControl.SetError(fndLocationBulk, "Please select the location.It is mandatory ")
                        fndLocationBulk.Focus()
                        Throw New Exception("Please select the location.It is mandatory")
                    Else
                        errorControl.SetError(fndLocationBulk, "")
                    End If
                    If clsCommon.myLen(fndVendorBulk.Value) <= 0 Then
                        errorControl.SetError(fndVendorBulk, "Please select the From location.It is mandatory ")
                        fndVendorBulk.Focus()
                        Throw New Exception("Please select the From location.It is mandatory")
                    Else
                        errorControl.SetError(fndVendorBulk, "")
                    End If

                    Dim intCOunt As Integer = 0

                    For ii As Integer = 0 To gvItemBulk.Rows.Count - 1
                        Dim Icode As String = clsCommon.myCstr(gvItemBulk.Rows(ii).Cells(colItemCode).Value)
                        If clsCommon.myLen(Icode) > 0 Then
                            Dim strChamberDes As String = clsCommon.myCstr(gvItemBulk.Rows(ii).Cells(colChamberDesc).Value)
                            If clsCommon.myLen(strChamberDes) = 0 Then
                                Throw New Exception("Please enter chamber desc.It is mandatory ")
                            End If
                            intCOunt += 1
                            For jj As Integer = 0 To gvItemBulk.Rows.Count - 1
                                If jj = ii Then
                                    Continue For
                                End If
                                Dim strInnerIcode As String = clsCommon.myCstr(gvItemBulk.Rows(jj).Cells(colItemCode).Value)
                                If clsCommon.myLen(strInnerIcode) > 0 Then
                                    Dim strInnerChamberDesc As String = clsCommon.myCstr(gvItemBulk.Rows(jj).Cells(colChamberDesc).Value)
                                    If clsCommon.CompairString(strChamberDes, strInnerChamberDesc) = CompairStringResult.Equal Then
                                        Dim Msg As String = "Same Chamber Exist at Row No " + clsCommon.myCstr(ii + 1) + " And " + clsCommon.myCstr(jj + 1)
                                        Throw New Exception(Msg)
                                    End If
                                End If
                            Next
                        End If
                    Next
                    If intCOunt = 0 Then
                        Throw New Exception("Please FIll at list one item.It is mandatory ")
                    End If
                End If

            End If

            If clsCommon.myCDate(dtpDateAndTimeBulk.Value, "dd/MMM/yyyy") > clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy") Then
                dtpDateAndTimeBulk.Value = clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
                Throw New Exception(" Gate Entry Date Can not be upcoming Date ")
            End If
            If clsCommon.myCDate(dtpDateAndTimeBulk.Value, "dd/MMM/yyyy") < clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy") And clsCommon.myLen(fndGateEntryNO.Value) = 0 Then
                If clsFixedParameter.GetData(clsFixedParameterType.MilkProc, clsFixedParameterCode.AllowGateEntryInPrevDate, Nothing) = 0 Then
                    dtpDateAndTimeBulk.Value = clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
                    Throw New Exception(" Gate Entry Date Can not be Prev Date, Please Contact to Administrator ")
                End If
            End If
            If clsCommon.CompairString(txtChallanNoBulk.Text, "ND") = CompairStringResult.Equal Then

            Else
                'If dtpChallanDateBulk.Value > dtpDateAndTimeBulk.Value Then
                '    Throw New Exception("Challan date can't be greater than gate entry date")
                'End If
            End If

            '' done by richa 23 Jan,2019 against UDL/22/01/19-000260 (remove condition clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal because this condition should be checked in every case)
            ''By Balwinder it should not check itself document no.
            If ShowTankerWithoutCheckingAnyValidation = False Then
                If btnSave.Enabled Then
                    Dim isTankerGateOut As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(xxx.tanker_No) from (select Tanker_No  from Tspl_Gate_Entry_Details where Gate_Entry_No not in (select Gate_Entry_No from TSPL_Gate_Out) and Doc_Type='BulkProc' and Gate_Entry_No not in ('" + fndGateEntryNO.Value + "') )xxx where Tanker_No='" & txtTankerNoBulk.Text & "'"))
                    If isTankerGateOut >= 1 Then
                        txtTankerNoBulk.Text = ""
                        fndTankerNo.Value = ""
                        Throw New Exception("Please enter another Tanker No.It is in use at some other plant ")
                    End If
                End If
            End If

            If clsCommon.CompairString(cmbGEType.SelectedValue, "J") = CompairStringResult.Equal AndAlso chkJobWork.Checked Then
                Throw New Exception("Gate entry Type as  Job Work and Is JobWork On cannot be selected  at same time.")
            End If
            If clsERPFuncationality.GetGSTStatus(clsCommon.myCDate(dtpDateAndTimeBulk.Value)) Then
                For ii As Integer = 0 To gvItemBulk.Rows.Count - 1
                    ' done by priti BHA/17/08/18-000455 to forcibly select item in contract type for bharat
                    If ForceToSelectIteminGateEntry = 1 Then
                        Dim ICode As String = clsCommon.myCstr(gvItemBulk.Rows(ii).Cells(colItemCode).Value)
                        If clsCommon.myLen(ICode) <= 0 Then
                            Throw New Exception("Please Enter Item Code At Line No : " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                        End If
                    End If

                    If clsCommon.myLen(gvItemBulk.Rows(ii).Cells(colItemCode).Value) > 0 Then
                        Dim HSNCode As String = clsItemMaster.GetItemHSNCode(gvItemBulk.Rows(ii).Cells(colItemCode).Value, Nothing)
                        gvItemBulk.Rows(ii).Cells(colHSN).Value = HSNCode
                        If clsCommon.myLen(HSNCode) <= 0 Then
                            clsCommon.MyMessageBoxShow("HSN Code is Mandatory. At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                            Return False
                        End If
                    End If
                Next
            End If
            ' done by priti BHA/27/06/18-000093 for  mcc can
            ' changes done for bharat provision calculation based on weight BHA/31/07/18-000205
            If chkMccProc.IsChecked = True Then
                Dim dblCans As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select No_Of_CANS from TSPL_MCC_Dispatch_Challan where chalan_no='" & fndChallanNoMcc.Value & "'", Nothing))
                If dblCans > 0 AndAlso clsCommon.myCdbl(txtCAN.Value) = 0 Then
                    txtCAN.Focus()
                    Throw New Exception("Please enter no of CANS. ")
                End If
            End If
            Dim dblWt As Double = clsCommon.myCdbl(txtWeight.Text)
            Dim dblTotQty As Double = clsCommon.myCdbl(lblTotalQTy.Text)
            Dim dblRATE As Double = clsCommon.myCdbl(txtRate.Text)
            Dim dbldistance As Double = clsCommon.myCdbl(txtdistance.Text)
            If chkBulkMilkProc.IsChecked = True AndAlso dblWt > 0 Then
                If dblTotQty > dblWt And dblTotQty > 0 Then
                    txtAmount.Text = Math.Round((dblTotQty / dblWt) * dblRATE * dbldistance, 2)
                Else
                    txtAmount.Text = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select TSPL_BULK_ROUTE_MASTER.Amount FROM TSPL_VENDOR_MASTER left join TSPL_BULK_ROUTE_MASTER on " &
                  "TSPL_VENDOR_MASTER.Bulk_ROUTE_NO=TSPL_BULK_ROUTE_MASTER.ROUTE_NO where Vendor_Code='" & clsCommon.myCstr(fndVendorBulk.Value) & "'"))
                End If
            End If
            '' richa BHA/09/10/18-000613 on 18 Oct,2018
            If chkBulkMilkProc.IsChecked = True Then
                Dim isHighClassVendor As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select case when isnull(IsHighClass,0)=0 then 'N' else 'Y' end from tspl_vendor_master where Vendor_code='" & fndVendorBulk.Value & "' "))
                If clsCommon.CompairString(isHighClassVendor, "Y") = CompairStringResult.Equal Then
                    For jj As Integer = 0 To gvItemBulk.Rows.Count - 1
                        If clsCommon.myLen(gvItemBulk.Rows(jj).Cells(colFat).Value) <= 0 Then
                            Throw New Exception("Please enter Fat % at row No." & jj + 1)
                        End If
                        If clsCommon.myLen(gvItemBulk.Rows(jj).Cells(colSNF).Value) <= 0 Then
                            Throw New Exception("Please enter SNF % at row No." & jj + 1)
                        End If
                    Next

                End If
            End If
            ''---------------------------

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
            Return False
        End Try
    End Function

    Private Function SaveData(ByVal isPost As Boolean, Optional ByVal isamendment As Boolean = False) As Boolean
        Dim trans As SqlTransaction = Nothing
        Try
            obj = New clsGateEntry()
            If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                obj.isNewEntry = True
            Else
                obj.isNewEntry = False
            End If
            trans = clsDBFuncationality.GetTransactin()
            Dim dt As Date = clsCommon.GETSERVERDATE(trans, "dd/MMM/yyyy hh:mm:ss tt")
            If obj.isNewEntry Then
                Dim isPODocumentTypeWise As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.BulkProcurementCounterOnEntryType, clsFixedParameterCode.BulkProcurementCounterOnEntryType, trans)) = 0, False, True) ''Make Setting Balwinder
                If isPODocumentTypeWise AndAlso chkBulkMilkProc.IsChecked Then
                    If clsCommon.CompairString(cmbGEType.SelectedValue, "") = CompairStringResult.Equal Then
                        cmbGEType.SelectedValue = "P"
                    End If
                    If clsCommon.myLen(cmbGEType.SelectedValue) <= 0 Then
                        cmbGEType.Focus()
                        Throw New Exception("Please select Gate Entry Type")
                    End If
                    If clsCommon.CompairString(clsCommon.myCstr(cmbGEType.SelectedValue), "P") = CompairStringResult.Equal Then
                        obj.Gate_Entry_No = clsERPFuncationality.GetNextCode(trans, dtpDateAndTimeBulk.Value, clsDocType.GateEntry, clsDocTransactionType.BulkProcPurchase, fndLocationBulk.Value)
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(cmbGEType.SelectedValue), "J") = CompairStringResult.Equal Then
                        obj.Gate_Entry_No = clsERPFuncationality.GetNextCode(trans, dtpDateAndTimeBulk.Value, clsDocType.GateEntry, clsDocTransactionType.BulkProcJobWork, fndLocationBulk.Value)
                    Else
                        cmbGEType.Focus()
                        Throw New Exception("Wrong Gate Entry Type")
                    End If
                Else
                    If chkJobWork.Checked Then
                        obj.Gate_Entry_No = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(dtpDateAndTimeBulk.Value, "dd/MMM/yyyy hh:mm:ss tt"), clsDocType.GateEntry, IIf(chkMccProc.IsChecked, clsDocTransactionType.MCCProcJobWorkOutward, clsDocTransactionType.BulkProcJobWorkOutward), txtSubLocation.Value)
                    Else
                        obj.Gate_Entry_No = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(dtpDateAndTimeBulk.Value, "dd/MMM/yyyy hh:mm:ss tt"), clsDocType.GateEntry, IIf(chkMccProc.IsChecked, clsDocTransactionType.MccProc, clsDocTransactionType.BulkProc), fndLocationBulk.Value)
                    End If
                End If


                If clsCommon.myLen(obj.Gate_Entry_No) <= 0 Then
                    clsCommon.MyMessageBoxShow("Error in Gate Entry  No genertion", Me.Text)
                    Return False
                End If
            Else
                obj.Gate_Entry_No = clsCommon.myCstr(fndGateEntryNO.Value)
            End If
            fndGateEntryNO.Value = obj.Gate_Entry_No
            obj.Toll_Amount = clsCommon.myCdbl(txtTollAmount.Text)
            obj.openingKM = clsCommon.myCdbl(txtOpeningKM.Text)
            obj.closingKM = clsCommon.myCdbl(txtClosingKM.Text)
            'obj.Reference_No = lblRefrenceNo.Text
            If chkBulkMilkProc.IsChecked Then
                obj.Doc_Type = "BulkProc"
                obj.IsAgainstJobWork = IIf(chkJobWork.Checked, 1, 0)
                obj.Sublocation_Code = txtSubLocation.Value
                obj.Date_And_Time = clsCommon.GetPrintDate(dtpDateAndTimeBulk.Value, "dd/MMM/yyyy hh:mm:ss tt")
                obj.location_Code = clsCommon.myCstr(fndLocationBulk.Value)
                obj.Location_Desc = clsCommon.myCstr(lblLocationDecBulk.Text)
                obj.Vendor_Code = clsCommon.myCstr(fndVendorBulk.Value)
                obj.Vendor_Desc = clsCommon.myCstr(lblVendorNameBulk.Text)
                obj.Tanker_No = clsCommon.myCstr(txtTankerNoBulk.Text)
                obj.Challan_No = clsCommon.myCstr(txtChallanNoBulk.Text)
                obj.Challan_Date = clsCommon.GetPrintDate(dtpChallanDateBulk.Value, "dd/MMM/yyyy")
                obj.Item_Code = clsCommon.myCstr(gvItemBulk.Rows(0).Cells(colItemCode).Value)
                obj.Item_Desc = clsCommon.myCstr(gvItemBulk.Rows(0).Cells(colItemDesc).Value)
                obj.UOM = clsCommon.myCstr(gvItemBulk.Rows(0).Cells(colUOM).Value)
                obj.Qty_In_Kg = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colQty).Value)
                obj.fat_per = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colFat).Value)
                obj.snf_Per = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colSNF).Value)
                obj.Tanker_Return = IIf(chkTankerReturn.Checked, 1, 0)
                obj.Gate_Entry_Type = cmbGEType.SelectedValue
                obj.No_Of_CAN = clsCommon.myCdbl(txtCAN.Value)
                obj.NO_OF_CHAMBER = clsCommon.myCdbl(txtNoofChamber.Value)
                obj.Bulk_ROUTE_NO = clsCommon.myCstr(txtBulkRouteNo.Text)
                obj.Rate = clsCommon.myCdbl(txtRate.Text)
                obj.Distance = clsCommon.myCdbl(txtdistance.Text)
                obj.Amount = clsCommon.myCdbl(txtAmount.Text)
                obj.Weight = clsCommon.myCdbl(txtWeight.Text)
                obj.Transpoter_Id = clsCommon.myCstr(txtTransport.Text)
                obj.ProvisionNo = clsCommon.myCstr(txtProvisionNo.Text)
            ElseIf chkMccProc.IsChecked Then
                obj.Doc_Type = "MccProc"
                obj.IsAgainstJobWork = IIf(chkJobWork.Checked, 1, 0)
                obj.Sublocation_Code = txtSubLocation.Value
                obj.Date_And_Time = clsCommon.GetPrintDate(dtpDateAndTimeBulk.Value, "dd/MMM/yyyy hh:mm:ss tt")
                obj.Tanker_No = clsCommon.myCstr(txtTankerNoBulk.Text)
                obj.Dispatched_From_Mcc = clsCommon.myCstr(fndVendorBulk.Value)
                obj.No_Of_CAN = clsCommon.myCdbl(txtCAN.Value)
                If AllowBulkProcMCCwithoutTankerDispatch = 1 Then
                    obj.Challan_No = clsCommon.myCstr(txtChallanNoBulk.Text)
                Else
                    obj.Challan_No = clsCommon.myCstr(fndChallanNoMcc.Value)
                End If

                obj.Challan_Date = clsCommon.GetPrintDate(dtpChallanDateBulk.Value, "dd/MMM/yyyy")
                obj.Item_Code = clsCommon.myCstr(gvItemBulk.Rows(0).Cells(colItemCode).Value)
                obj.Item_Desc = clsCommon.myCstr(gvItemBulk.Rows(0).Cells(colItemDesc).Value)
                obj.UOM = clsCommon.myCstr(gvItemBulk.Rows(0).Cells(colUOM).Value)
                obj.Qty_In_Kg = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colQty).Value)
                obj.fat_per = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colFat).Value)
                obj.snf_Per = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colSNF).Value)
                obj.location_Code = clsCommon.myCstr(fndLocationBulk.Value)
                obj.Location_Desc = clsCommon.myCstr(lblLocationDecBulk.Text)
                obj.IsNetWeight = IIf(chkNetWeight.Checked, 1, 0)
            End If
            If Not isPost Then
                obj.isPosted = 0
            End If
            obj.Modify_By = objCommonVar.CurrentUserCode
            obj.Modify_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
            obj.comp_code = objCommonVar.CurrentCompanyCode
            If obj.isNewEntry Then
                obj.Created_By = objCommonVar.CurrentUserCode
                obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
            End If
            ''richa agarwal remove setting as per Ranjana Mam
            'If (isIntimationReqd = 1) OrElse (FinalChamberwise = 1) Then
            obj.Intimation_No = lblIntimationNo.Text
            obj.Supplier_Code = txtSupplierCode.Value
            obj.MIKL_TYPE_CODE = txtMilktypeCode.Value
            obj.Seal_Status = cmbSealStatus.SelectedValue
            obj.Dispatch_Centre_Code = txtDispatchCentreCode.Text
            obj.TotalQty_In_Kg = clsCommon.myCdbl(lblTotalQTy.Text)
            obj.SealNo_Header = clsCommon.myCstr(txtSealValue.Text)
            obj.PO_No = clsCommon.myCstr(txtPO.Value)
            obj.Arr = New List(Of clsGateEntryChemberNoDetails)
            Dim intLine As Integer = 0
            For Each grow As GridViewRowInfo In gvItemBulk.Rows
                Dim objTr As New clsGateEntryChemberNoDetails()
                If isPost Then
                    If clsCommon.myCdbl(grow.Cells(colQty).Value) > 0 Then
                        intLine = intLine + 1
                        objTr.Line_No = intLine
                        objTr.Chamber_Desc = clsCommon.myCstr(grow.Cells(colChamberDesc).Value)
                        objTr.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                        objTr.UOM = clsCommon.myCstr(grow.Cells(colUOM).Value)
                        objTr.fat_per = clsCommon.myCdbl(grow.Cells(colFat).Value)
                        objTr.snf_Per = clsCommon.myCdbl(grow.Cells(colSNF).Value)
                        objTr.Chamber_Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                        objTr.DIP_Status = clsCommon.myCstr(grow.Cells(colDIPStatus).Value)
                        objTr.Sample_Lifted = clsCommon.myCstr(grow.Cells(colSampleLifted).Value)
                        objTr.MIKL_TYPE_CODE = clsCommon.myCstr(grow.Cells(colMilkTypeCode).Value)
                        objTr.Dip_value = clsCommon.myCstr(grow.Cells(colDIPValue).Value)
                        objTr.Seal_No = clsCommon.myCstr(grow.Cells(colChamberSealNo).Value)
                    End If
                Else
                    objTr.Line_No = clsCommon.myCdbl(grow.Cells(colSlNo).Value)
                    objTr.Chamber_Desc = clsCommon.myCstr(grow.Cells(colChamberDesc).Value)
                    objTr.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                    objTr.UOM = clsCommon.myCstr(grow.Cells(colUOM).Value)
                    objTr.fat_per = clsCommon.myCdbl(grow.Cells(colFat).Value)
                    objTr.snf_Per = clsCommon.myCdbl(grow.Cells(colSNF).Value)
                    objTr.Chamber_Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    objTr.DIP_Status = clsCommon.myCstr(grow.Cells(colDIPStatus).Value)
                    objTr.Sample_Lifted = clsCommon.myCstr(grow.Cells(colSampleLifted).Value)
                    objTr.MIKL_TYPE_CODE = clsCommon.myCstr(grow.Cells(colMilkTypeCode).Value)
                    objTr.Dip_value = clsCommon.myCstr(grow.Cells(colDIPValue).Value)
                    objTr.Seal_No = clsCommon.myCstr(grow.Cells(colChamberSealNo).Value)
                End If

                If chkBulkMilkProc.IsChecked = True Then
                    objTr.ManualRate = clsCommon.myCdbl(grow.Cells(colRate).Value)
                End If

                If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                    obj.Arr.Add(objTr)
                End If
            Next
            'End If
            obj.Against_Gate_Out = txtGateOutNo.Value
            obj.IsAgainstGateOut = IIf(chkAgainstGateOutNo.Checked = True, 1, 0)
            obj.ROUTE_NO = txtRoute.Value
            If clsGateEntry.saveData(obj, trans, False, isamendment) Then
                trans.Commit()
                If Not isPost Then
                    If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                        myMessages.insert()
                    Else
                        myMessages.update()
                    End If
                End If
                btnSave.Text = "Update"
                fndGateEntryNO.MyReadOnly = True
                btnDelete.Enabled = True
                btnPost.Enabled = True
                btn_amendment.Enabled = False
                obj = Nothing
                Return True
                Exit Function
                LoadData(obj.Gate_Entry_No, obj.Doc_Type, NavigatorType.Current)
            End If
            'trans.Rollback()
            clsCommon.MyMessageBoxShow("Data Not Saved ")
            btnSave.Text = "Save"
            btnDelete.Enabled = False
            btnPost.Enabled = False
            btn_amendment.Enabled = False
            fndGateEntryNO.MyReadOnly = False
            'Return False
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)

        End Try
        Return False
    End Function

    Sub postData()
        Try
            Dim strDocType As String = String.Empty
            If chkBulkMilkProc.IsChecked Then
                strDocType = "BulkProc"
            ElseIf chkMccProc.IsChecked Then
                strDocType = "MccProc"
            End If
            Dim qry As String = ""
            Dim msg As String = ""
            Dim dt As DataTable = Nothing
            If (myMessages.postConfirm()) Then
                If Not allowToSave() Then
                    Exit Sub
                End If
                If SaveData(True) Then
                    If (clsGateEntry.postData(fndGateEntryNO.Value, strDocType, Me.Form_ID)) Then
                        If chkNetWeight.Checked = True Then
                            msg = "Successfully Posted, First do QC !"
                        Else
                            msg = "Successfully Posted"
                        End If
                    Else
                        qry = "select No_Of_Level, LEVEL from TSPL_APPROVAL_LEVEL_SCREEN where User_Code='" + objCommonVar.CurrentUserCode + "' and Trans_Code='" + MyBase.Form_ID + "' "
                        dt = clsDBFuncationality.GetDataTable(qry)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            Dim level As String = dt.Rows(0)("LEVEL").ToString()
                            Dim NoOflevel As Integer = clsCommon.myCdbl(dt.Rows(0)("No_Of_Level"))
                            If clsCommon.CompairString(level, "Level1") = CompairStringResult.Equal Then
                                msg = "Level 1 Approval done. "
                                If NoOflevel = 1 Then
                                    msg += "Successfully Posted. "
                                Else
                                    msg += "Level 2 Approval Required."
                                End If
                            ElseIf clsCommon.CompairString(level, "Level2") = CompairStringResult.Equal Then
                                msg = "Level 2 Approval done. "
                                If NoOflevel = 2 Then
                                    msg += "Successfully Posted "
                                Else
                                    msg += "Level 3 Approval Required."
                                End If
                            Else
                                msg = "Level 3 Approval done. Successfully Posted. "
                            End If
                        End If
                    End If
                    common.clsCommon.MyMessageBoxShow(msg)
                    LoadData(fndGateEntryNO.Value, strDocType, NavigatorType.Current)
                End If
            End If
            dt = Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Sub UpdateTotal()
        Dim dblTotalQty As Double = 0
        For ii As Integer = 0 To gvItemBulk.Rows.Count - 1
            dblTotalQty = dblTotalQty + clsCommon.myCdbl(gvItemBulk.Rows(ii).Cells(colQty).Value)
        Next
        lblTotalQTy.Text = clsCommon.myFormat(dblTotalQty)
        Dim dblWt As Double = clsCommon.myCdbl(txtWeight.Text)
        Dim dblTotQty As Double = clsCommon.myCdbl(lblTotalQTy.Text)
        Dim dblRATE As Double = clsCommon.myCdbl(txtRate.Text)
        Dim dbldistance As Double = clsCommon.myCdbl(txtdistance.Text)
        If chkBulkMilkProc.IsChecked = True AndAlso dblWt > 0 Then
            If dblTotQty > dblWt And dblTotQty > 0 Then
                txtAmount.Text = Math.Round((dblTotQty / dblWt) * dblRATE * dbldistance, 2)
            Else
                txtAmount.Text = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select TSPL_BULK_ROUTE_MASTER.Amount FROM TSPL_VENDOR_MASTER left join TSPL_BULK_ROUTE_MASTER on " &
              "TSPL_VENDOR_MASTER.Bulk_ROUTE_NO=TSPL_BULK_ROUTE_MASTER.ROUTE_NO where Vendor_Code='" & clsCommon.myCstr(fndVendorBulk.Value) & "'"))
            End If
        End If
    End Sub
    Sub LoadData(ByVal strGateEntryNo As String, ByVal docType As String, ByVal nav As NavigatorType)
        Dim obj As clsGateEntry = Nothing
        btnUpdateFatSnfForContractor.Visible = False
        If chkBulkMilkProc.IsChecked Then
            obj = clsGateEntry.getData(strGateEntryNo, "BulkProc", nav)

            If obj IsNot Nothing Then
                Panel3.Enabled = False
                insideLoadData = True
                txtSubLocation.Value = obj.Sublocation_Code
                chkNetWeight.Enabled = False
                chkNetWeight.Checked = False
                chkJobWork.Visible = IIf(obj.IsAgainstJobWork = 1, True, False)
                chkJobWork.Checked = IIf(obj.IsAgainstJobWork = 1, True, False)
                If clsCommon.myLen(txtSubLocation.Value) > 0 Then
                    lblSubLocation.Text = clsLocation.GetName(txtSubLocation.Value, Nothing)
                    txtVendorCode.Text = clsDBFuncationality.getSingleValue("select Jobwork_Vendor from TSPL_LOCATION_MASTER WHERE Location_Code='" & txtSubLocation.Value & "'")
                    If clsCommon.myLen(txtSubLocation.Value) > 0 Then
                        txtvndrname.Text = clsDBFuncationality.getSingleValue("select Vendor_Name from TSPL_VENDOR_MASTER WHERE Vendor_Code='" & txtVendorCode.Text & "'")
                    Else
                        txtvndrname.Text = ""
                    End If
                    txtSubLocation.Enabled = False
                Else
                    lblSubLocation.Text = ""
                End If
                cmbGEType.SelectedValue = obj.Gate_Entry_Type
                fndLocationBulk.Enabled = False
                fndGateEntryNO.Value = obj.Gate_Entry_No
                lblRefrenceNo.Text = obj.Reference_No
                dtpDateAndTimeBulk.Value = obj.Date_And_Time
                fndLocationBulk.Value = obj.location_Code
                lblLocationDecBulk.Text = obj.Location_Desc
                fndVendorBulk.Value = obj.Vendor_Code
                lblVendorNameBulk.Text = obj.Vendor_Desc
                txtTankerNoBulk.Text = obj.Tanker_No
                txtChallanNoBulk.Text = obj.Challan_No
                dtpChallanDateBulk.Value = obj.Challan_Date
                chkTankerReturn.Checked = IIf(obj.Tanker_Return = 1, True, False)
                txtPO.Value = obj.PO_No

                If clsCommon.myLen(txtPO.Value) > 0 Then
                    lblPOQty.Text = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Qty from TSPL_PO_BULK_MASTER where PO_No= '" & clsCommon.myCstr(txtPO.Value) & "'"))
                    lblPOBalanceQty.Text = GetBalancePOQty(txtPO.Value)
                End If
                txtRoute.Value = obj.ROUTE_NO
                txtCAN.Value = obj.No_Of_CAN
                txtNoofChamber.Value = obj.NO_OF_CHAMBER
                txtBulkRouteNo.Text = obj.Bulk_ROUTE_NO
                txtRate.Text = obj.Rate
                txtdistance.Text = obj.Distance
                txtAmount.Text = obj.Amount
                txtWeight.Text = obj.Weight
                txtTransport.Text = obj.Transpoter_Id
                txtProvisionNo.Text = obj.ProvisionNo
                txtTransportName.Text = clsDBFuncationality.getSingleValue("select Transpoter_Name from TSPL_TRANSPORT__TANKER_DETAILS where Transpoter_Id='" & obj.Transpoter_Id & "'")
                lblClosingDate.Text = clsCommon.myCstr(obj.Closing_Date)
                txtOpeningKM.Text = clsCommon.myCdbl(obj.openingKM)
                txtClosingKM.Text = clsCommon.myCdbl(obj.closingKM)
                txtTollAmount.Text = clsCommon.myCdbl(obj.Toll_Amount)
                loadBlankGvItemBulk()
                If (isIntimationReqd = 0 AndAlso FinalChamberwise = 0) Then
                    gvItemBulk.Rows(0).Cells(colItemCode).Value = obj.Item_Code
                    gvItemBulk.Rows(0).Cells(colHSN).Value = clsItemMaster.GetItemHSNCode(obj.Item_Code, Nothing)
                    gvItemBulk.Rows(0).Cells(colItemDesc).Value = obj.Item_Desc
                    gvItemBulk.Rows(0).Cells(colUOM).Value = obj.UOM
                    gvItemBulk.Rows(0).Cells(colQty).Value = obj.Qty_In_Kg
                    gvItemBulk.Rows(0).Cells(colFat).Value = obj.fat_per
                    gvItemBulk.Rows(0).Cells(colSNF).Value = obj.snf_Per
                    gvItemBulk.Rows(0).Cells(colFatKG).Value = obj.fat_per * obj.Qty_In_Kg / 100
                    gvItemBulk.Rows(0).Cells(colSNFKG).Value = obj.snf_Per * obj.Qty_In_Kg / 100
                End If

                If obj.isPosted = 1 Then
                    lblPending.Status = ERPTransactionStatus.Approved
                    btnSave.Enabled = False
                    btnDelete.Enabled = False
                    btnPost.Enabled = False
                    btn_amendment.Enabled = True
                    BtnResetProv.Enabled = True
                Else
                    lblPending.Status = ERPTransactionStatus.Pending
                    btnSave.Enabled = True
                    btnDelete.Enabled = True
                    btnPost.Enabled = True
                    btn_amendment.Enabled = False
                    BtnResetProv.Enabled = False
                End If
                btnSave.Text = "Update"
                ''richa agarwal remove settings as per Ranjana Mam 
                'If (isIntimationReqd = 1 OrElse FinalChamberwise = 1) Then
                fndTankerNo.Value = obj.Tanker_No
                lblIntimationNo.Text = obj.Intimation_No
                txtSupplierCode.Value = obj.Supplier_Code
                txtMilktypeCode.Value = obj.MIKL_TYPE_CODE
                cmbSealStatus.SelectedValue = obj.Seal_Status
                txtDispatchCentreCode.Text = obj.Dispatch_Centre_Code
                lblTotalQTy.Text = obj.TotalQty_In_Kg
                txtSealValue.Text = obj.SealNo_Header
                lblSupplierName.Text = clsSupplierMaster.getSupplierName(txtSupplierCode.Value, Nothing)
                lblMilkTypeCode.Text = clsMilkTypeMaster.getMilkTypeName(txtMilktypeCode.Value, Nothing)
                lblMilkType.Text = clsMilkTypeMaster.getMilkType(txtMilktypeCode.Value, Nothing)
                txtPriceCode.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select top 1 price_code from TSPL_GATE_ENTRY_PRICE_CHART where ge_code='" & fndGateEntryNO.Value & "'"))
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    gvItemBulk.Rows.Clear()
                    For Each objTr As clsGateEntryChemberNoDetails In obj.Arr
                        gvItemBulk.Rows.AddNew()
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colSlNo).Value = objTr.Line_No
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colItemCode).Value = objTr.Item_Code
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colHSN).Value = clsItemMaster.GetItemHSNCode(objTr.Item_Code, Nothing)
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colDIPStatus).Value = objTr.DIP_Status
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colSampleLifted).Value = objTr.Sample_Lifted
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colItemDesc).Value = clsIntimation.getItemName(objTr.Item_Code, Nothing)
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colUOM).Value = objTr.UOM
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colQty).Value = objTr.Chamber_Qty
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colChamberDesc).Value = objTr.Chamber_Desc
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colMilkTypeCode).Value = objTr.MIKL_TYPE_CODE
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colChamberSealNo).Value = objTr.Seal_No
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colDIPValue).Value = objTr.Dip_value
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colFat).Value = objTr.fat_per
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colSNF).Value = objTr.snf_Per
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colFatKG).Value = objTr.fat_per * objTr.Chamber_Qty / 100
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colSNFKG).Value = objTr.snf_Per * objTr.Chamber_Qty / 100
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colRate).Value = objTr.ManualRate
                    Next
                End If

                txtGateOutNo.Value = obj.Against_Gate_Out
                chkAgainstGateOutNo.Checked = IIf(obj.IsAgainstGateOut = 1, True, False)

                isHighClass = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select case when isnull(IsHighClass,0)=0 then 'N' else 'Y' end from tspl_vendor_master where Vendor_code='" & fndVendorBulk.Value & "' "))
                If clsCommon.CompairString(isHighClass, "Y") = CompairStringResult.Equal Then
                    gvItemBulk.Columns(colUOM).ReadOnly = False
                Else
                    gvItemBulk.Columns(colUOM).ReadOnly = True
                End If
                insideLoadData = False
                'End If
            Else
                reset()
            End If
        ElseIf chkMccProc.IsChecked Then
            obj = clsGateEntry.getData(strGateEntryNo, "MccProc", nav)
            If obj IsNot Nothing Then
                insideLoadData = True
                '=======Sanjeet==========================
                chkNetWeight.Enabled = True
                chkNetWeight.Checked = IIf(obj.IsNetWeight = 1, True, False)
                txtSubLocation.Value = obj.Sublocation_Code
                chkJobWork.Checked = IIf(obj.IsAgainstJobWork = 1, True, False)
                If clsCommon.myLen(txtSubLocation.Value) > 0 Then
                    lblSubLocation.Text = clsLocation.GetName(txtSubLocation.Value, Nothing)
                    txtVendorCode.Text = clsDBFuncationality.getSingleValue("select Jobwork_Vendor from TSPL_LOCATION_MASTER WHERE Location_Code='" & txtSubLocation.Value & "'")
                    If clsCommon.myLen(txtSubLocation.Value) > 0 Then
                        txtvndrname.Text = clsDBFuncationality.getSingleValue("select Vendor_Name from TSPL_VENDOR_MASTER WHERE Vendor_Code='" & txtVendorCode.Text & "'")
                    Else
                        txtvndrname.Text = ""
                    End If
                Else
                    lblSubLocation.Text = ""
                End If
                '====================================
                txtCAN.Value = obj.No_Of_CAN
                lblRefrenceNo.Text = obj.Reference_No
                fndGateEntryNO.Value = obj.Gate_Entry_No
                dtpDateAndTimeBulk.Value = obj.Date_And_Time
                fndLocationBulk.Value = obj.location_Code
                lblLocationDecBulk.Text = obj.Location_Desc
                fndVendorBulk.Value = obj.Dispatched_From_Mcc
                fndVendorBulk.Enabled = False
                lblVendorNameBulk.Text = clsLocation.GetName(obj.Dispatched_From_Mcc, Nothing)
                txtTankerNoBulk.Text = obj.Tanker_No
                fndTankerNo.Value = obj.Tanker_No
                txtRoute.Value = obj.ROUTE_NO
                If AllowBulkProcMCCwithoutTankerDispatch = 1 Then
                    txtChallanNoBulk.Text = obj.Challan_No
                Else
                    fndChallanNoMcc.Value = obj.Challan_No
                End If
                fndLocationBulk.Enabled = False
                dtpChallanDateBulk.Value = obj.Challan_Date
                lblClosingDate.Text = clsCommon.myCstr(obj.Closing_Date)
                txtOpeningKM.Text = clsCommon.myCdbl(obj.openingKM)
                txtClosingKM.Text = clsCommon.myCdbl(obj.closingKM)
                txtTollAmount.Text = clsCommon.myCdbl(obj.Toll_Amount)
                loadBlankGvItemMCC()
                If (MCCChamberwise = 0 AndAlso chkMccProc.IsChecked = True) Then
                    gvItemBulk.Rows(0).Cells(colItemCode).Value = obj.Item_Code
                    gvItemBulk.Rows(0).Cells(colItemDesc).Value = obj.Item_Desc
                    gvItemBulk.Rows(0).Cells(colHSN).Value = clsItemMaster.GetItemHSNCode(obj.Item_Code, Nothing)
                    gvItemBulk.Rows(0).Cells(colUOM).Value = obj.UOM
                    gvItemBulk.Rows(0).Cells(colQty).Value = obj.Qty_In_Kg
                    gvItemBulk.Rows(0).Cells(colFat).Value = obj.fat_per
                    gvItemBulk.Rows(0).Cells(colSNF).Value = obj.snf_Per
                    gvItemBulk.Rows(0).Cells(colFatKG).Value = obj.fat_per * obj.Qty_In_Kg / 100
                    gvItemBulk.Rows(0).Cells(colSNFKG).Value = obj.snf_Per * obj.Qty_In_Kg / 100

                End If

                Dim objDis As clsMccDispatch = clsMccDispatch.getData(obj.Challan_No, NavigatorType.Current)

                gvManualSeal.Rows(0).Cells(ColSealNo).Value = clsCommon.myCstr(objDis.Seal_No1)
                gvManualSeal.Rows(1).Cells(ColSealNo).Value = clsCommon.myCstr(objDis.Seal_No2)
                gvManualSeal.Rows(2).Cells(ColSealNo).Value = clsCommon.myCstr(objDis.Seal_No3)
                gvManualSeal.Rows(3).Cells(ColSealNo).Value = clsCommon.myCstr(objDis.Seal_No4)
                gvManualSeal.Rows(4).Cells(ColSealNo).Value = clsCommon.myCstr(objDis.Seal_No5)
                gvManualSeal.Rows(5).Cells(ColSealNo).Value = clsCommon.myCstr(objDis.Seal_No6)
                gvManualSeal.Rows(6).Cells(ColSealNo).Value = clsCommon.myCstr(objDis.Seal_No7)
                gvManualSeal.Rows(7).Cells(ColSealNo).Value = clsCommon.myCstr(objDis.Seal_No8)
                gvManualSeal.Rows(8).Cells(ColSealNo).Value = clsCommon.myCstr(objDis.Seal_No9)
                gvManualSeal.Rows(9).Cells(ColSealNo).Value = clsCommon.myCstr(objDis.Seal_No10)

                If objDis.arrPaperSeal IsNot Nothing AndAlso objDis.arrPaperSeal.Count > 0 Then
                    For i As Integer = 0 To objDis.arrPaperSeal.Count - 1
                        gvPaperSeal.Rows(i).Cells(ColSealNo).Value = objDis.arrPaperSeal(i).Seal_No
                    Next
                End If

                txtSupplierCode.Value = obj.Supplier_Code
                txtMilktypeCode.Value = obj.MIKL_TYPE_CODE
                cmbSealStatus.SelectedValue = obj.Seal_Status
                'cmbGEType.SelectedValue = obj.Gate_Entry_Type
                txtDispatchCentreCode.Text = obj.Dispatch_Centre_Code
                lblTotalQTy.Text = obj.TotalQty_In_Kg
                txtSealValue.Text = obj.SealNo_Header
                lblSupplierName.Text = clsSupplierMaster.getSupplierName(txtSupplierCode.Value, Nothing)
                lblMilkTypeCode.Text = clsMilkTypeMaster.getMilkTypeName(txtMilktypeCode.Value, Nothing)
                lblMilkType.Text = clsMilkTypeMaster.getMilkType(txtMilktypeCode.Value, Nothing)
                If (MCCChamberwise = 1 AndAlso chkMccProc.IsChecked = True) Then
                    If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                        gvItemBulk.Rows.Clear()
                        For Each objTr As clsGateEntryChemberNoDetails In obj.Arr
                            gvItemBulk.Rows.AddNew()
                            gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colSlNo).Value = objTr.Line_No
                            gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colItemCode).Value = objTr.Item_Code
                            gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colHSN).Value = clsItemMaster.GetItemHSNCode(objTr.Item_Code, Nothing)
                            gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colDIPStatus).Value = objTr.DIP_Status
                            gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colSampleLifted).Value = objTr.Sample_Lifted
                            gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colItemDesc).Value = clsIntimation.getItemName(objTr.Item_Code, Nothing)
                            gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colUOM).Value = objTr.UOM
                            gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colQty).Value = objTr.Chamber_Qty
                            gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colChamberDesc).Value = objTr.Chamber_Desc
                            gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colMilkTypeCode).Value = objTr.MIKL_TYPE_CODE
                            gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colChamberSealNo).Value = objTr.Seal_No
                            gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colDIPValue).Value = objTr.Dip_value
                            gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colFat).Value = objTr.fat_per
                            gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colSNF).Value = objTr.snf_Per
                            gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colFatKG).Value = objTr.fat_per * objTr.Chamber_Qty / 100
                            gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colSNFKG).Value = objTr.snf_Per * objTr.Chamber_Qty / 100
                        Next
                    End If
                    insideLoadData = False
                End If
                If obj.isPosted = 1 Then
                    lblPending.Status = ERPTransactionStatus.Approved
                    btnSave.Enabled = False
                    btnDelete.Enabled = False
                    btnPost.Enabled = False
                    btn_amendment.Enabled = True
                    BtnResetProv.Enabled = True
                Else
                    lblPending.Status = ERPTransactionStatus.Pending
                    btnSave.Enabled = True
                    btnDelete.Enabled = True
                    btnPost.Enabled = True
                    btn_amendment.Enabled = False
                    BtnResetProv.Enabled = False
                End If
                btnSave.Text = "Update"
            Else
                reset()
            End If
        Else
            clsCommon.MyMessageBoxShow("Please Select Gate Entry Type Either MCC Procurement or Bulk Procurement ")
            Exit Sub
        End If
        DisableIntimationControls()
        obj = Nothing
    End Sub
    Sub DisableIntimationControls()
        If clsCommon.myLen(lblIntimationNo.Text) > 0 Then
            fndTankerNo.Enabled = False
            txtSupplierCode.Enabled = False
            txtDispatchCentreCode.Enabled = False
            txtMilktypeCode.Enabled = False
            cmbGEType.Enabled = False
        ElseIf TankerFromMaster = 0 AndAlso isIntimationReqd = 0 Then
            txtSupplierCode.Enabled = False
            txtDispatchCentreCode.Enabled = False
            txtMilktypeCode.Enabled = False
            If ShowGateEntryType = 1 Then
                cmbGEType.Enabled = True
            Else
                cmbGEType.Enabled = False
            End If

        ElseIf (TankerFromMaster = 1 OrElse isIntimationReqd = 1) Then
            txtSupplierCode.Enabled = True
            txtDispatchCentreCode.Enabled = True
            txtMilktypeCode.Enabled = True
            cmbGEType.Enabled = True
        End If

    End Sub
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If allowToSave() Then
            If SaveData(False) Then
                If AllowBulkProcMCCwithoutTankerDispatch = 1 Then
                    If clsCommon.myLen(fndGateEntryNO.Value) > 0 Then
                        Dim strDocType As String = String.Empty
                        If chkBulkMilkProc.IsChecked Then
                            strDocType = "BulkProc"
                        ElseIf chkMccProc.IsChecked Then
                            strDocType = "MccProc"
                        End If
                        LoadData(fndGateEntryNO.Value, strDocType, NavigatorType.Current)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub fndGateEntryNO__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndGateEntryNO._MYNavigator
        Dim strDocType As String = String.Empty
        If chkBulkMilkProc.IsChecked Then
            strDocType = "BulkProc"
        ElseIf chkMccProc.IsChecked Then
            strDocType = "MccProc"
        Else
            clsCommon.MyMessageBoxShow("Please select Gate Entry Type as Bulk Procurment or Mcc Procurement")
            Exit Sub
        End If
        LoadData(fndGateEntryNO.Value, strDocType, NavType)
    End Sub

    Private Sub fndGateEntryNO__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndGateEntryNO._MYValidating
        Dim strDocType As String = String.Empty
        If chkBulkMilkProc.IsChecked Then
            strDocType = "BulkProc"
        ElseIf chkMccProc.IsChecked Then
            strDocType = "MccProc"
        Else
            clsCommon.MyMessageBoxShow("Please select Gate Entry Type as Bulk Procurment or Mcc Procurement")
            Exit Sub
        End If
        Dim whrcls As String = " 2=2 "
        If (Not clsMccMaster.isCurrentUserHO()) AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrcls += " and Location_Code in (" & objCommonVar.strCurrUserLocations & ") and doc_type='" & strDocType & "'"
        Else
            whrcls += " and doc_type='" & strDocType & "'"
        End If
        If intBulkProcRunOneTypeGateEntry = 1 Or intBulkProcRunOneTypeGateEntry = 2 Then
            whrcls += " and doc_type='" & strDocType & "'"
        End If
        fndGateEntryNO.Value = clsGateEntry.getFinder(whrcls, fndGateEntryNO.Value, isButtonClicked)
        If clsCommon.myLen(fndGateEntryNO.Value) > 0 Then
            LoadData(fndGateEntryNO.Value, strDocType, NavigatorType.Current)
        Else
            reset()
        End If
    End Sub

    Private Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPost.Click
        postData()
    End Sub

    Private Sub grpBulkProc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub gvItemBulk_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gvItemBulk.CurrentColumnChanged
        If AllowBulkProcMCCwithoutTankerDispatch = 1 Then
            If gvItemBulk.RowCount > 0 Then
                Dim intCurrRow As Integer = gvItemBulk.CurrentRow.Index
                gvItemBulk.CurrentRow.Cells(colSlNo).Value = clsCommon.myCdbl(intCurrRow + 1)
                If intCurrRow = gvItemBulk.Rows.Count - 1 Then
                    gvItemBulk.Rows.AddNew()
                    gvItemBulk.CurrentRow = gvItemBulk.Rows(intCurrRow)
                End If
            End If
        End If
    End Sub

    Private Sub gvItemBulk_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gvItemBulk.KeyDown

    End Sub

    Private Sub chkMccProc_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkMccProc.ToggleStateChanged
        If chkBulkMilkProc.IsChecked Then
            If TankerFromMaster = 1 Then
                FinalChamberwise = 1
            Else
                FinalChamberwise = 0
            End If
        ElseIf chkMccProc.IsChecked Then
            If MCCChamberwise = 1 Then
                FinalChamberwise = 1
            Else
                FinalChamberwise = 0
            End If
        End If

        If chkBulkMilkProc.IsChecked Then
            fndChallanNoMcc.Visible = False
            txtChallanNoBulk.Visible = True
            fndVendorBulk.Enabled = True
            lblVendorBulk.Text = "Vendor"
            dtpChallanDateBulk.Enabled = True
            If AllowJobWorkonGateEntryBulkProc = 1 Then
                chkJobWork.Visible = True
                chkJobWork.Enabled = True
                txtSubLocation.Enabled = True
            End If
            chkNetWeight.Checked = False
            chkNetWeight.Enabled = False
        ElseIf chkMccProc.IsChecked Then
            lblVendorBulk.Text = "From MCC"
            If AllowJobWorkonGateEntryBulkProc = 1 Then
                chkJobWork.Visible = True
                chkJobWork.Enabled = False
                txtVendorCode.ReadOnly = True
                txtSubLocation.Enabled = False
            End If
            fndChallanNoMcc.Visible = True
            txtChallanNoBulk.Visible = False
            fndVendorBulk.Enabled = True
            dtpChallanDateBulk.Enabled = False
            If AllowBulkProcMCCwithoutTankerDispatch = 1 Then
                fndChallanNoMcc.Visible = False
                txtChallanNoBulk.Visible = True
                txtChallanNoBulk.Text = ""
                txtTankerNoBulk.Visible = True
                fndTankerNo.Visible = False
                txtTankerNoBulk.ReadOnly = False
                dtpChallanDateBulk.Enabled = True
            End If
            chkNetWeight.Enabled = True
        End If
        If chkBulkMilkProc.IsChecked OrElse chkMccProc.IsChecked Then
            reset()
        End If
        If GateEntryChamberwisewithManualTankerEntry = 1 And chkBulkMilkProc.IsChecked = True And TankerFromMaster = 1 Then
            Panel5.Visible = True
        Else
            Panel5.Visible = False
        End If
        If CreateMCCTankerGateOutBasedOnBulkRouteMaster = True Then
            RadGroupBox4.Visible = True
        Else
            RadGroupBox4.Visible = False
        End If
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvItemBulk.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvItemBulk.Columns.Count - 1 Step ii + 1
                        gvItemBulk.Columns(ii).IsVisible = False
                        gvItemBulk.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gvItemBulk.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
            RadPageViewPage1.Text = "Gate" & Environment.NewLine & "Entry"
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub mnuSaveLayout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuSaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gvItemBulk.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = clsUserMgtCode.frmGateEntry
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gvItemBulk.SaveLayout(obj.GridLayout)
            obj.GridColumns = gvItemBulk.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub mnuDeleteLayout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        ReStoreGridLayout()
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub mnuExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuExit.Click
        Me.Close()
        GC.Collect()
    End Sub

    Sub loadBlankGridManualSeal()
        gvManualSeal.Rows.Clear()
        gvManualSeal.Columns.Clear()

        Dim repoSLno As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSLno.HeaderText = "SL.No "
        repoSLno.Name = colSlNo
        repoSLno.ReadOnly = True
        repoSLno.Width = 100
        repoSLno.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvManualSeal.MasterTemplate.Columns.Add(repoSLno)


        Dim repoCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCode.FormatString = ""
        repoCode.HeaderText = "Seal No"
        repoCode.Name = ColSealNo
        repoCode.Width = 280
        repoCode.ReadOnly = True
        gvManualSeal.MasterTemplate.Columns.Add(repoCode)

        For i As Integer = 1 To 10
            gvManualSeal.Rows.AddNew()
            gvManualSeal.Rows(i - 1).Cells(colSlNo).Value = i.ToString
            gvManualSeal.Rows(i - 1).Cells(ColSealNo).Value = ""
        Next
        gvManualSeal.AllowAddNewRow = False
        gvManualSeal.AllowDeleteRow = False
        gvManualSeal.ShowGroupPanel = False
        gvManualSeal.AllowColumnReorder = False
        gvManualSeal.AllowRowReorder = False
        gvManualSeal.EnableSorting = False
        gvManualSeal.MasterTemplate.ShowRowHeaderColumn = False
        gvManualSeal.MasterTemplate.ShowColumnHeaders = True
        gvManualSeal.EnableAlternatingRowColor = True
        gvManualSeal.EnableFiltering = False
        gvManualSeal.ShowFilteringRow = False
        gvManualSeal.TableElement.TableHeaderHeight = 40
    End Sub

    Sub loadBlankGridPaperSeal()
        gvPaperSeal.Rows.Clear()
        gvPaperSeal.Columns.Clear()

        Dim repoSLno As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSLno.HeaderText = "SL.No "
        repoSLno.Name = colSlNo
        repoSLno.ReadOnly = True
        repoSLno.Width = 100
        repoSLno.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvPaperSeal.MasterTemplate.Columns.Add(repoSLno)


        Dim repoCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCode.FormatString = ""
        repoCode.HeaderText = "Seal No"
        'repoCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        'repoCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoCode.Name = ColSealNo
        repoCode.Width = 280
        repoCode.ReadOnly = True
        gvPaperSeal.MasterTemplate.Columns.Add(repoCode)

        For i As Integer = 1 To 10
            gvPaperSeal.Rows.AddNew()
            gvPaperSeal.Rows(i - 1).Cells(colSlNo).Value = i.ToString
            gvPaperSeal.Rows(i - 1).Cells(ColSealNo).Value = ""
        Next
        gvPaperSeal.AllowAddNewRow = False
        gvPaperSeal.AllowDeleteRow = False
        gvPaperSeal.ShowGroupPanel = False
        gvPaperSeal.AllowColumnReorder = False
        gvPaperSeal.AllowRowReorder = False
        gvPaperSeal.EnableSorting = False
        gvPaperSeal.MasterTemplate.ShowRowHeaderColumn = False
        gvPaperSeal.MasterTemplate.ShowColumnHeaders = True
        gvPaperSeal.EnableAlternatingRowColor = True
        gvPaperSeal.EnableFiltering = False
        gvPaperSeal.ShowFilteringRow = False
        gvPaperSeal.TableElement.TableHeaderHeight = 40
    End Sub

    Private Sub txtChallanNoBulk_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtChallanNoBulk.Validating
        If clsCommon.myLen(txtChallanNoBulk.Text) <= 0 Then
            txtChallanNoBulk.Text = "ND"
        End If
    End Sub



    Private Sub txtTankerNoBulk_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtTankerNoBulk.Validating
        Try
            If btnSave.Enabled Then
                Dim isTankerGateOut As Double = 0
                If ShowTankerWithoutCheckingAnyValidation = False Then
                    If chkBulkMilkProc.IsChecked Then
                        isTankerGateOut = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(xxx.tanker_No) from (select Tanker_No  from Tspl_Gate_Entry_Details where Gate_Entry_No not in (select Gate_Entry_No from TSPL_Gate_Out) and Doc_Type='BulkProc' AND Gate_Entry_No<>'" & fndGateEntryNO.Value & "')xxx where Tanker_No='" & txtTankerNoBulk.Text & "'"))
                        If isTankerGateOut >= 1 Then
                            'errorControl.SetError(txtTankerNoBulk, "Please enter another Tanker No.It is in use at some other plant ")
                            'txtTankerNoBulk.Focus()
                            txtTankerNoBulk.Text = ""
                            Throw New Exception("Please enter another Tanker No.It is in use at some other plant ")
                        End If
                    Else
                        If AllowBulkProcMCCwithoutTankerDispatch = 1 Then
                            isTankerGateOut = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(xxx.tanker_No) from (select Tanker_No  from Tspl_Gate_Entry_Details where Gate_Entry_No not in (select Gate_Entry_No from TSPL_Gate_Out) and Doc_Type='MccProc' AND Gate_Entry_No<>'" & fndGateEntryNO.Value & "' )xxx where Tanker_No='" & txtTankerNoBulk.Text & "'"))
                            If isTankerGateOut >= 1 Then
                                'errorControl.SetError(txtTankerNoBulk, "Please enter another Tanker No.It is in use at some other plant ")
                                'txtTankerNoBulk.Focus()
                                txtTankerNoBulk.Text = ""
                                Throw New Exception("Please enter another Tanker No.It is in use at some other plant ")
                            End If
                        End If
                    End If
                End If

                If clsCommon.myLen(txtTankerNoBulk.Text) > 0 Then
                    For i As Integer = 1 To txtTankerNoBulk.Text.Length
                        If (Asc(Microsoft.VisualBasic.Mid(txtTankerNoBulk.Text, i, 1)) >= 48 AndAlso Asc(Microsoft.VisualBasic.Mid(txtTankerNoBulk.Text, i, 1)) <= 57) OrElse (Asc(Microsoft.VisualBasic.Mid(txtTankerNoBulk.Text, i, 1)) >= 65 AndAlso Asc(Microsoft.VisualBasic.Mid(txtTankerNoBulk.Text, i, 1)) <= 90) OrElse (Asc(Microsoft.VisualBasic.Mid(txtTankerNoBulk.Text, i, 1)) >= 97 AndAlso Asc(Microsoft.VisualBasic.Mid(txtTankerNoBulk.Text, i, 1)) <= 122) Then
                        Else
                            Throw New Exception("Tanker no must only contain Alphabates and numbers, Not any blank space and symbol ")

                        End If
                    Next
                End If
            End If
            ' done by priti BHA/21/06/18-000074 
            If FinalChamberwise = 1 And GateEntryChamberwisewithManualTankerEntry = 1 Then
                If clsCommon.myLen(txtTankerNoBulk.Text) > 0 Then
                    Dim qry = "select Transpoter_Id,Transpoter_Name from TSPL_TRANSPORT__TANKER_DETAILS where Tanker_Id='" & clsCommon.myCstr(txtTankerNoBulk.Text) & "'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        txtTransport.Text = clsCommon.myCstr(dt.Rows(0)("Transpoter_Id"))
                        txtTransportName.Text = clsCommon.myCstr(dt.Rows(0)("Transpoter_Name"))
                    End If
                End If
            End If
            If chkBulkMilkProc.IsChecked = True AndAlso CreateProvisionforBulkContractorInGateIn = True Then
                txtOpeningKM.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select TSPL_MCC_TANKER_GATE_OUT.opening_km from TSPL_MCC_TANKER_GATE_OUT left join tspl_mcc_dispatch_challan on tspl_mcc_dispatch_challan.Against_Gate_Out=TSPL_MCC_TANKER_GATE_OUT.Gate_out_No where tspl_mcc_dispatch_challan.Chalan_No='" & fndChallanNoMcc.Value & "'"))
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            txtTankerNoBulk.Text = ""
            txtTankerNoBulk.Focus()
        End Try

    End Sub
    Private Sub txtSupplierCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtSupplierCode._MYValidating
        If clsCommon.myLen(fndVendorBulk.Value) = 0 Then
            clsCommon.MyMessageBoxShow("Please select Vendor", Me.Text)
            fndVendorBulk.Focus()
            Exit Sub
        End If
        Dim whr As String = " Vendor_Code='" & fndVendorBulk.Value & "'"
        txtSupplierCode.Value = clsSupplierMaster.getFinder(whr, txtSupplierCode.Value, isButtonClicked)
        If clsCommon.myLen(txtSupplierCode.Value) > 0 Then
            lblSupplierName.Text = clsSupplierMaster.getSupplierName(txtSupplierCode.Value, Nothing)
        End If
    End Sub

    Private Sub txtMilktypeCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtMilktypeCode._MYValidating
        Dim whr As String = ""
        txtMilktypeCode.Value = clsMilkTypeMaster.getFinder(whr, txtMilktypeCode.Value, isButtonClicked)
        If clsCommon.myLen(txtMilktypeCode.Value) > 0 Then
            lblMilkTypeCode.Text = clsMilkTypeMaster.getMilkTypeName(txtMilktypeCode.Value, Nothing)
            lblMilkType.Text = clsMilkTypeMaster.getMilkType(txtMilktypeCode.Value, Nothing)
            If gvItemBulk.Rows.Count > 0 Then
                For i As Integer = 0 To gvItemBulk.Rows.Count - 1
                    gvItemBulk.Rows(i).Cells(colMilkTypeCode).Value = clsCommon.myCstr(txtMilktypeCode.Value)
                Next
            End If
        End If
    End Sub
    Private Sub fndTankerNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndTankerNo._MYValidating
        If clsCommon.myLen(fndLocationBulk.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please select Location First.")
            fndLocationBulk.Focus()
            Exit Sub
        End If
        If chkBulkMilkProc.IsChecked = False AndAlso chkMccProc.IsChecked = False Then
            clsCommon.MyMessageBoxShow("Please select Gate Entry Type.")
            fndLocationBulk.Focus()
            Exit Sub
        End If

        ''richa 22 Sep,2016 
        If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowTankerBasedonVendorofGE, clsFixedParameterCode.AllowTankerBasedonVendorofGE, Nothing)), "1") = CompairStringResult.Equal AndAlso chkBulkMilkProc.IsChecked = True Then
            If clsCommon.myLen(fndVendorBulk.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please select Vendor First.")
                fndVendorBulk.Focus()
                Exit Sub
            End If
        End If

        Dim whr As String = ""
        If clsCommon.myLen(fndVendorBulk.Value) > 0 Then
            whr = " and TSPL_MCC_Dispatch_Challan.mcc_code='" & fndVendorBulk.Value & "'"
        End If
        'fndTankerNo.Value = clsMccDispatch.getFinder(" TSPL_MCC_Dispatch_Challan.mcc_or_plant_code ='" & fndLocationBulk.Value & "' " & whr & " and isPosted=1 and Chalan_NO not in (select distinct challan_no from tspl_gate_entry_details where tspl_gate_entry_details.gate_entry_No<>'" & fndGateEntryNO.Value & "') ", fndTankerNo.Value, isButtonClicked)

        If MCCChamberwise = 0 Then
            'fndChallanNoMcc.Value = clsMccDispatch.getTankerFinder(" TSPL_MCC_Dispatch_Challan.mcc_or_plant_code ='" & fndLocationBulk.Value & "' " & whr & " and isPosted=1 and Chalan_NO not in (select distinct challan_no from tspl_gate_entry_details where tspl_gate_entry_details.gate_entry_No<>'" & fndGateEntryNO.Value & "') and Chalan_NO not in (select distinct Challan_No from TSPL_MCC_DISPATCH_TRANSFER where isPosted=0) and not exists(select 1 from TSPL_MCC_Dispatch_Challan_Return where TSPL_MCC_Dispatch_Challan_Return.Challan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO )  and not exists(select 1 from TSPL_MCC_Tanker_Dispatch_Return_head where TSPL_MCC_Tanker_Dispatch_Return_head.Chalan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO )  ", fndTankerNo.Value)
            fndChallanNoMcc.Value = clsMccDispatch.getTankerFinder(" TSPL_MCC_Dispatch_Challan.mcc_or_plant_code ='" & fndLocationBulk.Value & "' " & whr & " and isPosted=1 and Chalan_NO not in (select distinct challan_no from tspl_gate_entry_details where tspl_gate_entry_details.gate_entry_No<>'" & fndGateEntryNO.Value & "' and tspl_gate_entry_details.In_Return=0 ) and Chalan_NO not in (select distinct Challan_No from TSPL_MCC_DISPATCH_TRANSFER where isPosted=0) and not exists(select 1 from TSPL_MCC_Dispatch_Challan_Return where TSPL_MCC_Dispatch_Challan_Return.Challan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO )  and not exists(select 1 from TSPL_MCC_Tanker_Dispatch_Return_head where TSPL_MCC_Tanker_Dispatch_Return_head.Chalan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO )  ", fndTankerNo.Value)
        Else
            If Not chkBulkMilkProc.IsChecked Then
                fndChallanNoMcc.Value = clsMccDispatch.getTankerFinder(" TSPL_MCC_Dispatch_Challan.mcc_or_plant_code ='" & fndLocationBulk.Value & "' " & whr & " and isPosted=1 and Chalan_NO not in (select distinct challan_no from tspl_gate_entry_details where tspl_gate_entry_details.gate_entry_No<>'" & fndGateEntryNO.Value & "') and Chalan_NO not in (select distinct Challan_No from TSPL_MCC_DISPATCH_TRANSFER where isPosted=0) and not exists(select 1 from TSPL_MCC_Dispatch_Challan_Return where TSPL_MCC_Dispatch_Challan_Return.Challan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO ) ", fndTankerNo.Value)
            End If
        End If
        If clsCommon.myLen(fndChallanNoMcc.Value) > 0 Then
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select  isnull(TSPL_MCC_Dispatch_Challan.IsAgainstJobWork,0) as IsAgainstJobWork,TSPL_MCC_Dispatch_Challan.Sublocation_Code,TSPL_MCC_Dispatch_Challan.mcc_code,mcc_name,TSPL_MCC_Dispatch_Challan.dispatch_date,TSPL_MCC_Dispatch_Challan.uom_Code,TSPL_MCC_Dispatch_Challan.tanker_no,TSPL_MCC_Dispatch_Challan.item_code,TSPL_MCC_Dispatch_Challan.item_desc,TSPL_MCC_Dispatch_Challan.net_qty,TSPL_MCC_Dispatch_Challan.no_of_cans from TSPL_MCC_Dispatch_Challan   where TSPL_MCC_Dispatch_Challan.chalan_no='" & fndChallanNoMcc.Value & "'")
            fndVendorBulk.Value = dt.Rows(0)("mcc_code")
            fndVendorBulk.Enabled = False
            lblVendorNameBulk.Text = dt.Rows(0)("mcc_name")
            dtpChallanDateBulk.Value = dt.Rows(0)("dispatch_date")
            txtTankerNoBulk.Text = dt.Rows(0)("tanker_no")
            fndTankerNo.Value = dt.Rows(0)("tanker_no")
            txtCAN.Value = clsCommon.myCdbl(dt.Rows(0)("no_of_cans"))
            fndLocationBulk.Enabled = False
            '======Sanjeet=======================
            chkJobWork.Checked = IIf(dt.Rows(0)("IsAgainstJobWork") = 1, True, False)
            If dt.Rows(0)("IsAgainstJobWork") = 1 Then
                Panel3.Visible = True
                If clsCommon.myLen(dt.Rows(0)("Sublocation_Code")) > 0 Then
                    Dim StrJobWorkQry As String = "select TSPL_LOCATION_MASTER.Location_Code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_VENDOR_MASTER.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name from TSPL_LOCATION_MASTER left outer join TSPL_VENDOR_MASTER ON TSPL_LOCATION_MASTER.Jobwork_Vendor=TSPL_VENDOR_MASTER.Vendor_Code " &
                                                 " WHERE TSPL_LOCATION_MASTER.Location_Code='" & clsCommon.myCstr(dt.Rows(0)("Sublocation_Code")) & "'"
                    Dim dtj As DataTable = clsDBFuncationality.GetDataTable(StrJobWorkQry)
                    If dtj.Rows.Count > 0 Then

                        txtSubLocation.Value = clsCommon.myCstr(dtj.Rows(0)("Location_Code"))
                        lblSubLocation.Text = clsCommon.myCstr(dtj.Rows(0)("Location_Desc"))
                        txtVendorCode.Text = clsCommon.myCstr(dtj.Rows(0)("Vendor_Code"))
                        txtvndrname.Text = clsCommon.myCstr(dtj.Rows(0)("Vendor_Name"))
                    Else
                        txtSubLocation.Value = ""
                        lblSubLocation.Text = ""
                        txtVendorCode.Text = ""
                        txtvndrname.Text = ""
                    End If

                End If
            Else
                Panel3.Visible = False
                txtSubLocation.Value = ""
                lblSubLocation.Text = ""
                txtVendorCode.Text = ""
                txtvndrname.Text = ""
            End If
            '=====================================
            Dim objDis As clsMccDispatch = clsMccDispatch.getData(fndChallanNoMcc.Value, NavigatorType.Current)
            If objDis.arr IsNot Nothing AndAlso objDis.arr.Count > 0 Then
                Dim intLineNo As Integer = 0
                gvItemBulk.Rows.Clear()
                For Each objtr As clsMCCDispatchDetail In objDis.arr
                    gvItemBulk.Rows.AddNew()
                    intLineNo += 1
                    gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colSlNo).Value = intLineNo
                    gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colItemCode).Value = objtr.Item_Code
                    gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colHSN).Value = clsItemMaster.GetItemHSNCode(objtr.Item_Code, Nothing)
                    gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colItemDesc).Value = objtr.Item_Name
                    gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colChamberDesc).Value = objtr.Chamber_Description
                    gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colUOM).Value = objtr.Item_UOM
                    gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colQty).Value = objtr.Qty_KG
                    gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colFat).Value = clsDBFuncationality.getSingleValue("select TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.param_field_value from TSPL_Mcc_Dispatch_Chalan_Parameter_Detail where chalan_no='" & fndChallanNoMcc.Value & "' and param_type='FAT' and SNo='" & objtr.SNo & "'")
                    gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colSNF).Value = clsDBFuncationality.getSingleValue("select TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.param_field_value from TSPL_Mcc_Dispatch_Chalan_Parameter_Detail where chalan_no='" & fndChallanNoMcc.Value & "' and param_type='SNF' and SNo='" & objtr.SNo & "'")
                    gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colFatKG).Value = clsCommon.myCdbl(gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colFat).Value) * clsCommon.myCdbl(gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colQty).Value) / 100
                    gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colSNFKG).Value = clsCommon.myCdbl(gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colSNF).Value) * clsCommon.myCdbl(gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colQty).Value) / 100
                Next
            Else
                gvItemBulk.Rows(0).Cells(colItemCode).Value = dt.Rows(0)("item_code")
                gvItemBulk.Rows(0).Cells(colItemDesc).Value = dt.Rows(0)("item_desc")
                gvItemBulk.Rows(0).Cells(colHSN).Value = clsItemMaster.GetItemHSNCode(gvItemBulk.Rows(0).Cells(colItemCode).Value, Nothing)
                gvItemBulk.Rows(0).Cells(colUOM).Value = clsCommon.myCstr(dt.Rows(0)("uom_Code"))
                gvItemBulk.Rows(0).Cells(colQty).Value = dt.Rows(0)("net_qty")
                gvItemBulk.Rows(0).Cells(colFat).Value = clsDBFuncationality.getSingleValue("select TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.param_field_value from TSPL_Mcc_Dispatch_Chalan_Parameter_Detail where chalan_no='" & fndChallanNoMcc.Value & "' and param_type='FAT'")
                gvItemBulk.Rows(0).Cells(colSNF).Value = clsDBFuncationality.getSingleValue("select TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.param_field_value from TSPL_Mcc_Dispatch_Chalan_Parameter_Detail where chalan_no='" & fndChallanNoMcc.Value & "' and param_type='SNF'")
                gvItemBulk.Rows(0).Cells(colFatKG).Value = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colFat).Value) * clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colQty).Value) / 100
                gvItemBulk.Rows(0).Cells(colSNFKG).Value = clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colSNF).Value) * clsCommon.myCdbl(gvItemBulk.Rows(0).Cells(colQty).Value) / 100
            End If
            gvManualSeal.Rows(0).Cells(ColSealNo).Value = clsCommon.myCstr(objDis.Seal_No1)
            gvManualSeal.Rows(1).Cells(ColSealNo).Value = clsCommon.myCstr(objDis.Seal_No2)
            gvManualSeal.Rows(2).Cells(ColSealNo).Value = clsCommon.myCstr(objDis.Seal_No3)
            gvManualSeal.Rows(3).Cells(ColSealNo).Value = clsCommon.myCstr(objDis.Seal_No4)
            gvManualSeal.Rows(4).Cells(ColSealNo).Value = clsCommon.myCstr(objDis.Seal_No5)
            gvManualSeal.Rows(5).Cells(ColSealNo).Value = clsCommon.myCstr(objDis.Seal_No6)
            gvManualSeal.Rows(6).Cells(ColSealNo).Value = clsCommon.myCstr(objDis.Seal_No7)
            gvManualSeal.Rows(7).Cells(ColSealNo).Value = clsCommon.myCstr(objDis.Seal_No8)
            gvManualSeal.Rows(8).Cells(ColSealNo).Value = clsCommon.myCstr(objDis.Seal_No9)
            gvManualSeal.Rows(9).Cells(ColSealNo).Value = clsCommon.myCstr(objDis.Seal_No10)
            If objDis.arrPaperSeal IsNot Nothing AndAlso objDis.arrPaperSeal.Count > 0 Then
                For i As Integer = 0 To objDis.arrPaperSeal.Count - 1
                    gvPaperSeal.Rows(i).Cells(ColSealNo).Value = objDis.arrPaperSeal(i).Seal_No
                Next
            End If
            dt = Nothing
            If CreateMCCTankerGateOutBasedOnBulkRouteMaster = True Then
                txtOpeningKM.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select TSPL_MCC_TANKER_GATE_OUT.opening_km from TSPL_MCC_TANKER_GATE_OUT left join tspl_mcc_dispatch_challan on tspl_mcc_dispatch_challan.Against_Gate_Out=TSPL_MCC_TANKER_GATE_OUT.Gate_out_No where tspl_mcc_dispatch_challan.Chalan_No='" & fndChallanNoMcc.Value & "'"))
            End If
        Else
            If (MCCChamberwise = 0 OrElse chkMccProc.IsChecked) Then
                fndVendorBulk.Value = ""
                lblVendorNameBulk.Text = ""
                'dtpChallanDateBulk.Value = ""
                txtTankerNoBulk.Text = ""
                fndTankerNo.Value = ""
                gvItemBulk.Rows(0).Cells(colItemCode).Value = ""
                gvItemBulk.Rows(0).Cells(colHSN).Value = ""
                gvItemBulk.Rows(0).Cells(colItemDesc).Value = ""
                gvItemBulk.Rows(0).Cells(colUOM).Value = ""
                gvItemBulk.Rows(0).Cells(colQty).Value = "0"
                gvItemBulk.Rows(0).Cells(colFat).Value = ""
                gvItemBulk.Rows(0).Cells(colSNF).Value = ""
                gvItemBulk.Rows(0).Cells(colFatKG).Value = ""
                gvItemBulk.Rows(0).Cells(colSNFKG).Value = ""
            Else
                ''richa 22 Sep,2016 
                If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowTankerBasedonVendorofGE, clsFixedParameterCode.AllowTankerBasedonVendorofGE, Nothing)), "1") = CompairStringResult.Equal AndAlso chkBulkMilkProc.IsChecked = True Then
                    fndTankerNo.Value = clsContractTankerHead.getTankerFinderBasedonVendor("(TSPL_CONTRACT_TANKER_MASTER.tanker_code  not in (select Tspl_Gate_Entry_Details.Tanker_No from Tspl_Gate_Entry_Details ) or TSPL_CONTRACT_TANKER_MASTER.tanker_code   in (select TSPL_Gate_Out.Tanker_No from TSPL_Gate_Out )  ) and TSPL_CONTRACT_TANKER_VENDOR_DETAIL.Vendor_Code='" & clsCommon.myCstr(fndVendorBulk.Value) & "'", fndTankerNo.Value, isButtonClicked)
                Else
                    fndTankerNo.Value = clsContractTankerHead.getFinder("( tanker_code  not in (select Tanker_No from Tspl_Gate_Entry_Details ) or tanker_code   in (select Tanker_No from TSPL_Gate_Out )  ) ", fndTankerNo.Value, isButtonClicked)
                End If

                If clsCommon.myLen(fndTankerNo.Value) > 0 Then
                    txtTankerNoBulk.Text = fndTankerNo.Value
                    LoadGrid(fndTankerNo.Value)
                End If
            End If

        End If

        txtMilktypeCode.Value = ""
        lblMilkTypeCode.Text = ""
        lblMilkType.Text = ""
    End Sub
    Sub LoadGrid(ByVal strTankerCode As String)
        If clsCommon.myLen(strTankerCode) > 0 Then
            loadBlankGv()
            If AllowGateEntryAgainstPO = 0 Then
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select LINE_NO,CHAMBER_DESC,(select Description from TSPL_FIXED_PARAMETER where Type='MCCDefaultMilkItem' and Code='MilkSetting') as ItemCode from TSPL_CONTRACT_TANKER_DETAIL where TANKER_CODE='" & fndTankerNo.Value & "'")
                If dt.Rows.Count > 0 Then
                    gvItemBulk.Rows.Clear()
                    Dim intLineNo As Integer = 0
                    For Each dr As DataRow In dt.Rows
                        gvItemBulk.Rows.AddNew()
                        intLineNo += 1
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colSlNo).Value = intLineNo
                        ' done by priti BHA/17/08/18-000455 to forcibly select item in contract type for bharat
                        If ForceToSelectIteminGateEntry = 0 Then
                            gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colItemCode).Value = clsCommon.myCstr(dr("ItemCode"))
                            gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colHSN).Value = clsItemMaster.GetItemHSNCode(gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colItemCode).Value, Nothing)
                            gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colItemDesc).Value = clsIntimation.getItemName(clsCommon.myCstr(dr("ItemCode")), Nothing)
                            gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colUOM).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select UOM_Code   from TSPL_ITEM_UOM_DETAIL where Item_Code='" & clsCommon.myCstr(dr("ItemCode")) & "' and Default_UOM='1' "))
                            If clsCommon.myLen(gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colUOM).Value) <= 0 Then
                                gvItemBulk.CurrentRow.Cells(colUOM).Value = clsItemMaster.GetStockUnit(gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colItemCode).Value, Nothing)
                            End If
                        End If
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colChamberDesc).Value = clsCommon.myCstr(dr("CHAMBER_DESC"))
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colMilkTypeCode).Value = clsCommon.myCstr(txtMilktypeCode.Value)
                    Next
                End If
            Else
                If allowManualrate = 1 Then
                    If clsCommon.myLen(txtPO.Value) = 0 Then
                        clsCommon.MyMessageBoxShow("Please Select PO No first", Me.Text)
                        fndTankerNo.Value = ""
                        Exit Sub
                    End If
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable("select LINE_NO,CHAMBER_DESC,(select top 1 Item_Code from TSPL_PO_BULK_DETAIL where PO_No='" & txtPO.Value & "') as ItemCode from TSPL_CONTRACT_TANKER_DETAIL where TANKER_CODE='" & fndTankerNo.Value & "'")
                    If dt.Rows.Count > 0 Then
                        gvItemBulk.Rows.Clear()
                        Dim intLineNo As Integer = 0
                        For Each dr As DataRow In dt.Rows
                            gvItemBulk.Rows.AddNew()
                            intLineNo += 1
                            gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colSlNo).Value = intLineNo
                            ' done by priti BHA/17/08/18-000455 to forcibly select item in contract type for bharat
                            If ForceToSelectIteminGateEntry = 0 Then
                                gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colItemCode).Value = clsCommon.myCstr(dr("ItemCode"))
                                gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colHSN).Value = clsItemMaster.GetItemHSNCode(gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colItemCode).Value, Nothing)
                                gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colItemDesc).Value = clsIntimation.getItemName(clsCommon.myCstr(dr("ItemCode")), Nothing)
                                gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colUOM).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select UOM_Code   from TSPL_ITEM_UOM_DETAIL where Item_Code='" & clsCommon.myCstr(dr("ItemCode")) & "' and Default_UOM='1' "))
                                If clsCommon.myLen(gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colUOM).Value) <= 0 Then
                                    gvItemBulk.CurrentRow.Cells(colUOM).Value = clsItemMaster.GetStockUnit(gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colItemCode).Value, Nothing)
                                End If
                            End If
                            gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colChamberDesc).Value = clsCommon.myCstr(dr("CHAMBER_DESC"))
                            gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colMilkTypeCode).Value = clsCommon.myCstr(txtMilktypeCode.Value)
                            gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colRate).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 ManualRate  from TSPL_PO_BULK_DETAIL where PO_No='" & txtPO.Value & "'"))
                        Next
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub dtpDateAndTimeBulk_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles dtpDateAndTimeBulk.Validating
        Try
            If clsCommon.myCDate(dtpDateAndTimeBulk.Value, "dd/MMM/yyyy") > clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy") Then
                'dtpDateAndTimeBulk.Value = clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
                Throw New Exception(" Gate Entry Date Can not be upcoming Date ")
            End If
            If clsCommon.myCDate(dtpDateAndTimeBulk.Value, "dd/MMM/yyyy") < clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy") Then
                If clsFixedParameter.GetData(clsFixedParameterType.MilkProc, clsFixedParameterCode.AllowGateEntryInPrevDate, Nothing) = 0 Then
                    dtpDateAndTimeBulk.Value = clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
                    Throw New Exception(" Gate Entry Date Can not be Prev Date, Please Contact to Administrator ")
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            dtpDateAndTimeBulk.Value = clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy hh:mm:ss tt")
        End Try
    End Sub


    Private Sub MyLabel2_Click(sender As Object, e As EventArgs) Handles MyLabel2.Click

    End Sub
    'KUNAL > TICKET : BM00000009843 > DATE : 17-NOV-2016  Ticket : BHA/16/07/18-000173                                                                      ssssssssssssssss
    Sub PrintData(ByVal GateEntryNo As String, ByVal DocumentType As String)

        Try
            If clsCommon.myLen(GateEntryNo) <= 0 Then
                Throw New Exception("Not found anything to print")
            Else
                ' Ticket No : BHA/03/07/18-000124 Create New Print Format For Bharat and Add Chamber_Qty , snf_Per , snf_Per column 
                Dim qry As String = ""
                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "BHAD") = CompairStringResult.Equal Then
                    qry = " select g.Tanker_No ,g.Gate_Entry_No [Gate-In No] , isnull(o.Doc_No ,'') [Gate-Out No] , (CASE WHEN G.doc_type='MccProc' THEN G.Dispatched_From_Mcc ELSE g.Vendor_Code END) AS  [Vendor Code] ," &
         "(CASE WHEN G.Doc_Type='MccProc' then TSPL_MCC_MASTER.MCC_NAME else  g.Vendor_Desc end) as [Vendor Name] , COALESCE(G.Supplier_Code ,'')  AS Sub_Vendor_Code, COALESCE(S.DESCRIPTION ,'')  AS Sub_Vendor_Code_Desc, CONVERT (varchar, g.Date_And_Time,103)  [Gate-In Date] , " &
         "RIGHT(CONVERT(VARCHAR(26), g.Date_And_Time, 109),14) [Gate-In Time], CONVERT (varchar , O.Doc_Date ,103 ) [Gate-Out Date], RIGHT(CONVERT(VARCHAR(26),  O.Doc_Date, 109),14) [Gate-Out Time], CM.Item_Code [Item Code] , " &
         "TSPL_ITEM_MASTER.Item_Desc [Item Desc]  , g.location_Code [Location] , g.Location_Desc [Loc Desc] , g.comp_code [Company Code] , c.Comp_Name [Comp Desc] , CONCAT(c.Add1 , ' ' , c.Add2 , ' ', c.Add3 , ' , ', c.State ) as [Company Address] , " &
         "g.Doc_Type [Doc Type],G.snf_Per,g.fat_per,g.MIKL_TYPE_CODE,G.Gate_Entry_Type,G.Seal_Status,G.TotalQty_In_Kg ,CM.Chamber_Desc,tspl_item_master.HSN_Code as HSNCode,CM.UOM, CM.Chamber_Qty,CM.snf_Per as snf_Per_CM, CM.fat_per as fat_per_CM , CM.Line_No,C.Logo_img, isnull(G.No_Of_CAN,'') as No_Of_CAN from Tspl_Gate_Entry_Details G LEFT JOIN TSPL_SUPPLIER_MASTER S ON G.Supplier_Code = S.SUPPLIER_CODE   LEFT JOIN  TSPL_COMPANY_MASTER C on g.comp_code = c.Comp_Code LEFT JOIN TSPL_Gate_Out O ON G.Gate_Entry_No = O.Gate_Entry_No LEFT OUTER JOIN TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=G.Dispatched_From_Mcc LEFT JOIN TSPL_Gate_Entry_Chember_Details CM ON CM.GE_Code=G.Gate_Entry_No   left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_code=CM.item_code  where 1=1 and G.doc_type='" + DocumentType + "' and g.gate_entry_no in ('" + GateEntryNo + "')"
                Else
                    qry = " select 'In' as PrintType,g.Tanker_No ,g.Gate_Entry_No [Gate-In No] , isnull(o.Doc_No ,'') [Gate-Out No] , (CASE WHEN G.doc_type='MccProc' THEN G.Dispatched_From_Mcc ELSE g.Vendor_Code END) AS  [Vendor Code] ," &
         "(CASE WHEN G.Doc_Type='MccProc' then TSPL_MCC_MASTER.MCC_NAME else  g.Vendor_Desc end) as [Vendor Name] , COALESCE(G.Supplier_Code ,'')  AS Sub_Vendor_Code, COALESCE(S.DESCRIPTION ,'')  AS Sub_Vendor_Code_Desc, CONVERT (varchar, g.Date_And_Time,103)  [Gate-In Date] , " &
         "RIGHT(CONVERT(VARCHAR(26), g.Date_And_Time, 109),14) [Gate-In Time], CONVERT (varchar , O.Doc_Date ,103 ) [Gate-Out Date], RIGHT(CONVERT(VARCHAR(26),  O.Doc_Date, 109),14) [Gate-Out Time], g.Item_Code [Item Code] , " &
         "g.Item_Desc [Item Desc]  , g.location_Code [Location] , g.Location_Desc [Loc Desc] , g.comp_code [Company Code] , c.Comp_Name [Comp Desc] , CONCAT(c.Add1 , ' ' , c.Add2 , ' ', c.Add3 , ' , ', c.State ) as [Company Address] , " &
         "g.Doc_Type [Doc Type],G.snf_Per,g.fat_per,g.MIKL_TYPE_CODE,G.Gate_Entry_Type,G.Seal_Status,G.TotalQty_In_Kg ,CM.Chamber_Desc,tspl_item_master.HSN_Code as HSNCode,CM.UOM, CM.Chamber_Qty, CM.snf_Per as snf_Per_CM, CM.fat_Per as fat_per_CM, C.Logo_img from Tspl_Gate_Entry_Details G LEFT JOIN TSPL_SUPPLIER_MASTER S ON G.Supplier_Code = S.SUPPLIER_CODE left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_code=g.item_code LEFT JOIN  TSPL_COMPANY_MASTER C on g.comp_code = c.Comp_Code LEFT JOIN TSPL_Gate_Out O ON G.Gate_Entry_No = O.Gate_Entry_No LEFT OUTER JOIN TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=G.Dispatched_From_Mcc LEFT JOIN TSPL_Gate_Entry_Chember_Details CM ON CM.GE_Code=G.Gate_Entry_No where 1=1 and G.doc_type='" + DocumentType + "' and g.gate_entry_no in ('" + GateEntryNo + "')"
                End If

                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "crptGateInMilkProc", "Milk Procurement Bulk Gate In", clsCommon.myCDate(dt.Rows(0)("Gate-In Date")))
                frmCRV = Nothing
            End If
        Catch ex As Exception
            RadMessageBox.Show(ex.Message, Me.Text)
        End Try
    End Sub
    'KUNAL > TICKET : BM00000009843 > DATE : 17-NOV-2016
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            Dim DocuentType As String = Nothing
            If chkBulkMilkProc.IsChecked Then
                DocuentType = "BulkProc"
            Else
                DocuentType = "MccProc"
            End If
            If clsCommon.myLen(DocuentType) > 0 Then
                PrintData(fndGateEntryNO.Value, DocuentType)

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.fndGateEntryNO.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            Dim Reason As String = ""
            If clsCommon.myLen(fndGateEntryNO.Value) = 0 Then
                Throw New Exception("Please Select Gate Entry No for update.")
            ElseIf btnPost.Enabled = True Then
                Throw New Exception("This entry is already unposted.")
            End If
            Dim strWeighmentNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Weighment_No from TSPL_Weighment_Detail where gate_entry_no='" & fndGateEntryNO.Value & "'"))
            Dim strQCNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select QC_No from TSPL_QUALITY_CHECK where gate_entry_no='" & fndGateEntryNO.Value & "'"))
            If clsCommon.myLen(strWeighmentNo) = 0 AndAlso clsCommon.myLen(strQCNo) = 0 Then

                If allowToSave() Then
                    If clsCancelLog.CheckForReasonOnUpdateAfterPost() Then
                        '' REASON FOR DELETE 
                        Dim frm As New FrmFreeTxtBox1
                        frm.Text = "Remarks for Update"
                        frm.ShowDialog()
                        If clsCommon.myLen(frm.strRmks) <= 0 Then
                            Exit Sub
                        Else
                            Reason = frm.strRmks
                        End If
                    End If
                    Dim strDocType As String = Nothing
                    If chkBulkMilkProc.IsChecked Then
                        strDocType = "BulkProc"
                    ElseIf chkMccProc.IsChecked Then
                        strDocType = "MccProc"
                    End If

                    If SaveData(False, True) Then
                        clsGateEntry.postData(fndGateEntryNO.Value, strDocType, Me.Form_ID)
                        saveCancelLog(Reason, "GEBP Update", Nothing)
                        clsCommon.MyMessageBoxShow("Amended Successfully.")
                        LoadData(fndGateEntryNO.Value, strDocType, NavigatorType.Current)
                    End If
                End If
            Else
                If clsCommon.myLen(strWeighmentNo) > 0 Then
                    Throw New Exception("Gate Entry is Used in Weighment No - " & strWeighmentNo)
                ElseIf clsCommon.myLen(strQCNo) > 0 Then
                    Throw New Exception("Gate Entry is Used in QC No - " & strQCNo)
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub cmbGEType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles cmbGEType.SelectedIndexChanged
        If insideLoadData = False Then
            fndVendorBulk.Value = Nothing
            lblVendorNameBulk.Text = ""
        End If
    End Sub

    Private Sub btn_amendment_Click(sender As Object, e As EventArgs) Handles btn_amendment.Click
        btnUpdate_Click(sender, e)
    End Sub

    Private Sub txtPO__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtPO._MYValidating
        Dim whr As String = ""

        Try
            Dim qry As String = " select * from (select PO_No as PONo,Date_And_Time,max(aa.Vendor_Code) as [Vendor Code],max(Vendor_Name) as [Vendor Desc], " &
                "max(aa.location_Code) as [location Code],max(Location_Desc) as [Location Desc], " &
                "sum(Qty* case when RI=1 then 1 else 0 end) as POQty, " &
                "SUM(Qty* case when RI=-1 then 1 else 0 end) as GEQty, " &
                "SUM(Unapproved) as UnapprovedQty, " &
                "SUM((Qty *RI)- Unapproved) as PedningQty  from ( " &
                "select PO_No,Date_And_Time,Vendor_Code,location_Code,MIKL_TYPE_CODE,Qty,0 AS Unapproved,1 as RI from TSPL_PO_BULK_MASTER where isPosted=1 " &
                "union all " &
                "select PO_No,Date_And_Time,Vendor_Code,location_Code,Tspl_Gate_Entry_Details.MIKL_TYPE_CODE,Chamber_Qty as Qty, " &
                "Chamber_Qty AS Unapproved,-1 as RI  from Tspl_Gate_Entry_Details left outer join TSPL_GATE_ENTRY_CHEMBER_DETAILS on " &
                "Tspl_Gate_Entry_Details.Gate_Entry_No=TSPL_GATE_ENTRY_CHEMBER_DETAILS.GE_Code WHERE Tspl_Gate_Entry_Details.isPosted=0 AND " &
                "ISNULL(Tspl_Gate_Entry_Details.PO_No,'') <> '' " &
                "union all " &
                "select PO_No,Date_And_Time,Vendor_Code,location_Code,Tspl_Gate_Entry_Details.MIKL_TYPE_CODE,Chamber_Qty as Qty, " &
                "0 AS Unapproved,-1 as RI  from Tspl_Gate_Entry_Details left outer join TSPL_GATE_ENTRY_CHEMBER_DETAILS on " &
                "Tspl_Gate_Entry_Details.Gate_Entry_No=TSPL_GATE_ENTRY_CHEMBER_DETAILS.GE_Code  WHERE Tspl_Gate_Entry_Details.isPosted=1  AND " &
                "ISNULL(Tspl_Gate_Entry_Details.PO_No,'') <> '') AA left outer join TSPL_LOCATION_MASTER on aa.location_Code=TSPL_LOCATION_MASTER.Location_Code " &
                "left outer join TSPL_VENDOR_MASTER on aa.Vendor_Code=TSPL_VENDOR_MASTER.Vendor_Code group by PO_No,Date_And_Time having sum((Qty *RI)-Unapproved) <> 0  ) " &
                "final "
            txtPO.Value = clsCommon.ShowSelectForm("POBulkGateEntry", qry, "PONo", whr, txtPO.Value, "PONo", isButtonClicked)
            If clsCommon.myLen(txtPO.Value) > 0 Then
                loadBlankGvItemBulk()
                Dim obj As clsPOBulkProc = Nothing
                obj = clsPOBulkProc.getData(txtPO.Value, "BulkProc", NavigatorType.Current)
                If obj IsNot Nothing Then
                    insideLoadData = True
                    txtPO.Value = obj.PO_No
                    dtpDateAndTimeBulk.Value = obj.Date_And_Time
                    fndLocationBulk.Value = obj.location_Code
                    fndVendorBulk.Value = obj.Vendor_Code
                    lblLocationDecBulk.Text = clsLocation.GetName(fndLocationBulk.Value, Nothing)
                    lblVendorNameBulk.Text = clsVendorMaster.GetName(fndVendorBulk.Value, Nothing)
                    'txtRate.Text = obj.Rate
                    'txtPriceCode.Text = obj.Price_Code
                    txtMilktypeCode.Value = obj.MIKL_TYPE_CODE
                    cmbGEType.SelectedValue = obj.Gate_Entry_Type
                    lblPOQty.Text = obj.Qty
                    lblPOBalanceQty.Text = GetBalancePOQty(txtPO.Value)
                    If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                        gvItemBulk.Rows.Clear()
                        For Each objTr As clsPOBulkProcDetails In obj.Arr
                            gvItemBulk.Rows.AddNew()
                            gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colSlNo).Value = objTr.Line_No
                            gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colItemCode).Value = objTr.Item_Code
                            gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colHSN).Value = clsItemMaster.GetItemHSNCode(objTr.Item_Code, Nothing)
                            gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colItemDesc).Value = clsIntimation.getItemName(objTr.Item_Code, Nothing)
                            gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colUOM).Value = objTr.UOM
                            gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colQty).Value = objTr.Qty
                            gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colRate).Value = objTr.ManualRate
                        Next
                    End If
                    insideLoadData = False

                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Function GetBalancePOQty(ByVal strPOCode As String) As Double
        Dim qry As String = "select  SUM((Qty *RI)- Unapproved) as PedningQty  from ( " &
        "select PO_No,Qty,0 AS Unapproved,1 as RI from TSPL_PO_BULK_MASTER where isPosted=1 and PO_No='" & strPOCode & "' " &
        "union all select PO_No,0 as Qty, Chamber_Qty AS Unapproved,-1 as RI  from Tspl_Gate_Entry_Details left outer join " &
        "TSPL_GATE_ENTRY_CHEMBER_DETAILS on Tspl_Gate_Entry_Details.Gate_Entry_No=TSPL_GATE_ENTRY_CHEMBER_DETAILS.GE_Code " &
        "WHERE Tspl_Gate_Entry_Details.isPosted=0 AND ISNULL(Tspl_Gate_Entry_Details.PO_No,'') <> '' and Tspl_Gate_Entry_Details.PO_No='" & strPOCode & "' and Tspl_Gate_Entry_Details.Gate_Entry_No not in ('" & clsCommon.myCstr(fndGateEntryNO.Value) & "') " &
        " union all select PO_No,Chamber_Qty as Qty, 0 AS Unapproved,-1 as RI  from Tspl_Gate_Entry_Details left outer join " &
        "TSPL_GATE_ENTRY_CHEMBER_DETAILS on Tspl_Gate_Entry_Details.Gate_Entry_No=TSPL_GATE_ENTRY_CHEMBER_DETAILS.GE_Code " &
        "WHERE Tspl_Gate_Entry_Details.isPosted=1  AND ISNULL(Tspl_Gate_Entry_Details.PO_No,'') <> '' and Tspl_Gate_Entry_Details.PO_No='" & strPOCode & "' and Tspl_Gate_Entry_Details.Gate_Entry_No not in ('" & clsCommon.myCstr(fndGateEntryNO.Value) & "') ) AA  group by PO_No"
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
    End Function

    Private Sub txtPriceCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtPriceCode._MYValidating
        If clsCommon.myLen(fndGateEntryNO.Value) = 0 Then
            clsCommon.MyMessageBoxShow("Please select Document No", Me.Text)
            fndVendorBulk.Focus()
            Exit Sub
        End If
        Dim whr As String = " TSPL_Bulk_Price_MASTER.Posted=1  and effective_Date<='" & clsCommon.GetPrintDate(dtpDateAndTimeBulk.Value, "dd/MMM/yyyy hh:mm tt") & "' and " &
            "expirydate >= '" & clsCommon.GetPrintDate(dtpDateAndTimeBulk.Value, "dd/MMM/yyyy hh:mm tt") & "' and " &
            "TSPL_Bulk_Price_MASTER.Milk_Type_Code='" & txtMilktypeCode.Value & "' " &
            "and TSPL_Bulk_Price_MASTER.Price_Code in (select Tspl_Vendor_Price_Chart_mapping.PriceCode from " &
            "Tspl_Vendor_Price_Chart_mapping where TSPL_VENDOR_PRICE_CHART_MAPPING.Posted=1 and " &
            "Tspl_Vendor_Price_Chart_mapping.VendorCode='" & fndVendorBulk.Value & "')"
        txtPriceCode.Value = clsPriceChartBulkProc.getFinder(whr, txtPriceCode.Value, isButtonClicked)

    End Sub

    Private Sub btnUpdatePrice_Click(sender As Object, e As EventArgs) Handles btnUpdatePrice.Click
        Try
            If clsCommon.myLen(txtPriceCode.Value) = 0 Then
                Throw New Exception("Please Select Price Code first.")
            End If
            Dim trans As SqlTransaction = Nothing
            trans = clsDBFuncationality.GetTransactin()

            If clsGateEntryPriceDetails.SaveData(fndGateEntryNO.Value, trans, txtPriceCode.Value) Then
                trans.Commit()
            Else
                trans.Rollback()
            End If
        Catch ex As Exception

            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub chkJobWork_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkJobWork.ToggleStateChanged
        If chkJobWork.Checked Then
            Panel3.Visible = True
        Else
            Panel3.Visible = False
            txtSubLocation.Value = ""
            lblSubLocation.Text = ""
            txtVendorCode.Text = ""
            txtvndrname.Text = ""
        End If
    End Sub

    Private Sub txtSubLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtSubLocation._MYValidating
        Dim strLocations = String.Empty
        If Not clsMccMaster.isCurrentUserHO() Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                strLocations = "and location_code in (" & objCommonVar.strCurrUserLocations & ")"
            End If
        End If
        If clsCommon.myLen(fndLocationBulk.Value) = 0 Then
            clsCommon.MyMessageBoxShow("Please select location code before sub location", Me.Text)
            Exit Sub
        End If
        txtSubLocation.Value = clsLocation.getFinder("(Main_Location_Code='" & fndLocationBulk.Value & "' and Is_Jobwork=1 and isnull(Is_Sub_Location,'N')='Y')" & strLocations, txtSubLocation.Value, isButtonClicked)
        If clsCommon.myLen(txtSubLocation.Value) > 0 Then
            lblSubLocation.Text = clsLocation.GetName(txtSubLocation.Value, Nothing)
            txtVendorCode.Text = clsDBFuncationality.getSingleValue("select Jobwork_Vendor from TSPL_LOCATION_MASTER WHERE Location_Code='" & txtSubLocation.Value & "'")
            If clsCommon.myLen(txtSubLocation.Value) > 0 Then
                txtvndrname.Text = clsDBFuncationality.getSingleValue("select Vendor_Name from TSPL_VENDOR_MASTER WHERE Vendor_Code='" & txtVendorCode.Text & "'")
            Else
                txtvndrname.Text = ""
            End If
        Else
            lblSubLocation.Text = ""
            txtVendorCode.Text = ""
        End If
        strLocations = Nothing


    End Sub

    Private Sub txtNoofChamber_TextChanged(sender As Object, e As EventArgs) Handles txtNoofChamber.TextChanged
        Try
            Dim strItem = clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Type='MCCDefaultMilkItem' and Code='MilkSetting'")
            If clsCommon.myCdbl(txtNoofChamber.Value) > 0 Then
                gvItemBulk.Rows.Clear()
                Dim intLineNo As Integer = 0
                Dim intChamberNo As Integer = txtNoofChamber.Value
                For intcount As Integer = 0 To intChamberNo - 1
                    gvItemBulk.Rows.AddNew()
                    intLineNo += 1
                    If ForceToSelectIteminGateEntry = 0 Then
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colItemCode).Value = strItem
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colHSN).Value = clsItemMaster.GetItemHSNCode(strItem, Nothing)
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colItemDesc).Value = clsIntimation.getItemName(strItem, Nothing)
                        gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colUOM).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select UOM_Code   from TSPL_ITEM_UOM_DETAIL where Item_Code='" & strItem & "' and Default_UOM='1' "))
                        If clsCommon.myLen(gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colUOM).Value) <= 0 Then
                            gvItemBulk.CurrentRow.Cells(colUOM).Value = clsItemMaster.GetStockUnit(strItem, Nothing)
                        End If
                    End If
                    gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colSlNo).Value = intLineNo
                    gvItemBulk.Rows(gvItemBulk.Rows.Count - 1).Cells(colMilkTypeCode).Value = clsCommon.myCstr(txtMilktypeCode.Value)
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    ''richa ERO/08/11/21-001531 on 16 Nov,2021
    Private Sub btnUpdateFatSnfForContractor_Click(sender As Object, e As EventArgs) Handles btnUpdateFatSnfForContractor.Click
        Try
            Dim Reason As String = ""
            If clsCommon.myLen(fndGateEntryNO.Value) = 0 Then
                Throw New Exception("Please Select Gate Entry No for update.")
            End If
            If btnPost.Enabled = True Then
                Throw New Exception("Entry should be Posted.")
            End If
            If chkBulkMilkProc.IsChecked Then
                Dim strsrn_no = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select srn_no from tspl_bulk_milk_srn where gate_entry_no='" & fndGateEntryNO.Value & "'"))
                Dim isHighClass As Boolean = IIf(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(isHighClass,0) from TSPL_VENDOR_MASTER where vendor_code='" & fndVendorBulk.Value & "'")) = 1, True, False)
                If clsCommon.myLen(strsrn_no) = 0 AndAlso isHighClass = True Then

                    If clsCancelLog.CheckForReasonOnUpdateAfterPost() Then
                        '' REASON FOR DELETE 
                        Dim frm As New FrmFreeTxtBox1
                        frm.Text = "Remarks for Update"
                        frm.ShowDialog()
                        If clsCommon.myLen(frm.strRmks) <= 0 Then
                            Exit Sub
                        Else
                            Reason = frm.strRmks
                        End If
                    End If
                    Dim strDocType As String = Nothing
                    If chkBulkMilkProc.IsChecked Then
                        strDocType = "BulkProc"
                    ElseIf chkMccProc.IsChecked Then
                        strDocType = "MccProc"
                    End If

                    If UpdateFatAndSnfInCaseOFcontractor() Then
                        saveCancelLog(Reason, "GEBP Update", Nothing)
                        clsCommon.MyMessageBoxShow("Fat and Snf Updated Successfully.")
                        LoadData(fndGateEntryNO.Value, strDocType, NavigatorType.Current)
                    End If
                Else
                    If clsCommon.myLen(strsrn_no) > 0 Then
                        Throw New Exception("Gate Entry is Used in SRN No - " & strsrn_no)
                    End If
                    If isHighClass = False Then
                        Throw New Exception("Vendor should be of High Class")
                    End If
                End If
            Else
                Throw New Exception("Fat and Snf should be changed in case of " & chkBulkMilkProc.Text & "")
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Function UpdateFatAndSnfInCaseOFcontractor() As Boolean
        Dim trans As SqlTransaction = Nothing
        Try

            trans = clsDBFuncationality.GetTransactin()

            Dim intLine As Integer = 0
            For Each grow As GridViewRowInfo In gvItemBulk.Rows
                Dim objTr As New clsGateEntryChemberNoDetails()
                If clsCommon.myCdbl(grow.Cells(colQty).Value) > 0 Then
                    intLine = intLine + 1

                    clsDBFuncationality.ExecuteNonQuery("update TSPL_GATE_ENTRY_CHEMBER_DETAILS set fat_per=" & clsCommon.myCdbl(grow.Cells(colFat).Value) & ",snf_Per =" & clsCommon.myCdbl(grow.Cells(colSNF).Value) & " where GE_Code ='" & fndGateEntryNO.Value & "' and Item_Code ='" & clsCommon.myCstr(grow.Cells(colItemCode).Value) & "' and Line_No ='" & intLine & "' and Chamber_Desc = '" & clsCommon.myCstr(grow.Cells(colChamberDesc).Value) & "' ", trans)
                    If intLine = 1 Then
                        clsDBFuncationality.ExecuteNonQuery("update Tspl_Gate_Entry_Details set Modify_By='" + objCommonVar.CurrentUserCode + "' ,Modify_Date='" & clsCommon.GETSERVERDATE(trans, "dd/MMM/yyyy hh:mm:ss tt") & "', fat_per=" & clsCommon.myCdbl(grow.Cells(colFat).Value) & ",snf_Per =" & clsCommon.myCdbl(grow.Cells(colSNF).Value) & " where Gate_Entry_No ='" & fndGateEntryNO.Value & "'  ", trans)
                    End If
                End If

            Next
            trans.Commit()

        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)

        End Try
        Return True
    End Function

    Private Sub btnClKM_Click(sender As Object, e As EventArgs) Handles btnClKM.Click
        If CreateProvisionOfTankerDispatchWithClosingKM = True And chkMccProc.IsChecked = True Then
            Dim strProvNo As String = clsDBFuncationality.getSingleValue("select Doc_No from TSPL_PROVISION_ENTRY where Ref_Doc_No='" + fndChallanNoMcc.Value + "' and Prov_type='Freight'")
            If clsCommon.myLen(strProvNo) > 0 Then
                clsCommon.MyMessageBoxShow("Provision Entry Already Exist", Me.Text)
                Exit Sub
            End If
            If (clsCommon.myLen(fndChallanNoMcc.Value) <= 0) Then
                clsCommon.MyMessageBoxShow("Document No not found", Me.Text)
                Exit Sub
            End If

            If (clsCommon.myCdbl(txtOpeningKM.Text) <= 0) Then
                clsCommon.MyMessageBoxShow("Enter Opening KM", Me.Text)
                txtOpeningKM.Focus()
                Exit Sub
            End If

            If (clsCommon.myCdbl(txtClosingKM.Text) <= 0) Then
                clsCommon.MyMessageBoxShow("Enter Closing KM", Me.Text)
                txtClosingKM.Focus()
                Exit Sub
            End If
            If (clsCommon.myCdbl(txtClosingKM.Text) <= clsCommon.myCdbl(txtOpeningKM.Text)) Then
                clsCommon.MyMessageBoxShow("Closing KM must be greater than Opening KM", Me.Text)
                txtClosingKM.Focus()
                Exit Sub
            End If
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try

                Dim obj As New clsMccDispatch
                obj.openingKM = txtOpeningKM.Text
                obj.closingKM = txtClosingKM.Text
                obj.Toll_Amount = txtTollAmount.Text
                obj.Closing_Date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")

                clsMccDispatch.UpdateAfterPosting(obj, fndChallanNoMcc.Value, trans)

                Dim objGateEntry As New clsGateEntry
                objGateEntry.openingKM = txtOpeningKM.Text
                objGateEntry.closingKM = txtClosingKM.Text
                objGateEntry.Toll_Amount = txtTollAmount.Text
                objGateEntry.Closing_Date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")

                clsGateEntry.UpdateAfterPosting(objGateEntry, fndGateEntryNO.Value, trans)
                clsMccDispatch.CreateProvison(fndChallanNoMcc.Value, MyBase.Form_ID, trans)
                trans.Commit()
                clsCommon.MyMessageBoxShow("Provision Created successfully", Me.Text)
                LoadData(fndGateEntryNO.Value, IIf(chkMccProc.IsChecked, "MccProc", "BulkProc"), NavigatorType.Current)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        ElseIf CreateProvisionforBulkContractorInGateIn = True AndAlso chkBulkMilkProc.IsChecked = True Then
            Dim strProvNo As String = clsDBFuncationality.getSingleValue("select Doc_No from TSPL_PROVISION_ENTRY where Ref_Doc_No='" + fndGateEntryNO.Value + "' and Prov_type='Bulk Proc'")
            If clsCommon.myLen(strProvNo) > 0 Then
                clsCommon.MyMessageBoxShow("Provision Entry Already Exist", Me.Text)
                Exit Sub
            End If
            If (clsCommon.myLen(fndGateEntryNO.Value) <= 0) Then
                clsCommon.MyMessageBoxShow("Document No not found", Me.Text)
                Exit Sub
            End If

            If (clsCommon.myCdbl(txtOpeningKM.Text) <= 0) Then
                clsCommon.MyMessageBoxShow("Enter Opening KM", Me.Text)
                txtOpeningKM.Focus()
                Exit Sub
            End If

            If (clsCommon.myCdbl(txtClosingKM.Text) <= 0) Then
                clsCommon.MyMessageBoxShow("Enter Closing KM", Me.Text)
                txtClosingKM.Focus()
                Exit Sub
            End If
            If (clsCommon.myCdbl(txtClosingKM.Text) <= clsCommon.myCdbl(txtOpeningKM.Text)) Then
                clsCommon.MyMessageBoxShow("Closing KM must be greater than Opening KM", Me.Text)
                txtClosingKM.Focus()
                Exit Sub
            End If
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try


                Dim objGateEntry As New clsGateEntry
                objGateEntry.openingKM = txtOpeningKM.Text
                objGateEntry.closingKM = txtClosingKM.Text
                objGateEntry.Toll_Amount = txtTollAmount.Text
                objGateEntry.Closing_Date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")

                clsGateEntry.UpdateAfterPosting(objGateEntry, fndGateEntryNO.Value, trans)
                clsGateEntry.CreateProvison(fndGateEntryNO.Value, MyBase.Form_ID, trans)
                trans.Commit()
                clsCommon.MyMessageBoxShow("Provision Created successfully", Me.Text)
                LoadData(fndGateEntryNO.Value, IIf(chkMccProc.IsChecked, "MccProc", "BulkProc"), NavigatorType.Current)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        End If
    End Sub

    Private Sub btnUpdateAfterPost_Click(sender As Object, e As EventArgs) Handles btnUpdateAfterPost.Click
        Try
            If (clsCommon.myCdbl(txtOpeningKM.Text) <= 0) Then
                clsCommon.MyMessageBoxShow("Enter Opening KM", Me.Text)
                txtOpeningKM.Focus()
                Exit Sub
            End If
            If (clsCommon.myCdbl(txtClosingKM.Text) <= clsCommon.myCdbl(txtOpeningKM.Text)) Then
                clsCommon.MyMessageBoxShow("Closing KM must be greater than Opening KM", Me.Text)
                txtClosingKM.Focus()
                Exit Sub
            End If

            If chkBulkMilkProc.IsChecked = True AndAlso CreateProvisionforBulkContractorInGateIn = True AndAlso chkAgainstGateOutNo.Checked = True AndAlso clsCommon.myLen(fndGateEntryNO.Value) > 0 Then
                Dim objGateEntry As New clsGateEntry
                objGateEntry.openingKM = txtOpeningKM.Text
                objGateEntry.closingKM = txtClosingKM.Text
                clsGateEntry.UpdateAfterPosting(objGateEntry, fndGateEntryNO.Value, Nothing)
                clsCommon.MyMessageBoxShow("Information updated successfully.")
            ElseIf clsCommon.myLen(fndChallanNoMcc.Value) > 0 AndAlso clsCommon.myLen(fndGateEntryNO.Value) > 0 Then
                Dim obj As New clsMccDispatch
                obj.openingKM = txtOpeningKM.Text
                obj.closingKM = txtClosingKM.Text
                Dim objGateEntry As New clsGateEntry
                objGateEntry.openingKM = txtOpeningKM.Text
                objGateEntry.closingKM = txtClosingKM.Text
                clsGateEntry.UpdateAfterPosting(objGateEntry, fndGateEntryNO.Value, Nothing)
                If clsMccDispatch.UpdateAfterPosting(obj, fndChallanNoMcc.Value, Nothing) Then
                    clsCommon.MyMessageBoxShow("Information updated successfully.")
                End If
            Else
                Throw New Exception("Document no not found")
            End If


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Private Sub BtnResetProv_Click(sender As Object, e As EventArgs) Handles BtnResetProv.Click
        If CreateProvisionOfTankerDispatchWithClosingKM = True Then
            If (clsCommon.myLen(fndChallanNoMcc.Value) <= 0) Then
                clsCommon.MyMessageBoxShow("Document No not found", Me.Text)
                Exit Sub
            End If
            'Dim strAPINVNo As String = clsDBFuncationality.getSingleValue("select Doc_No from TSPL_PROVISION_ENTRY where Ref_Doc_No='" + fndChalanNo.Value + "' and Prov_type='Freight'", trans)
            'If clsCommon.myLen(strAPINVNo) > 0 Then
            '    clsCommon.MyMessageBoxShow("AP Invoice already generated - " & strAPINVNo, Me.Text)
            '    Exit Sub
            'End If
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try
                Dim Qry1 As String = "update tspl_gate_entry_details set ClosingKM=0 , Closing_Date = null where Gate_Entry_No='" + fndGateEntryNO.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry1, trans)
                Dim Qry As String = "update TSPL_MCC_DISPATCH_CHALLAN set ClosingKM=0 , Closing_Date = null where Chalan_NO='" + fndChallanNoMcc.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Dim strProvNo As String = clsDBFuncationality.getSingleValue("select Doc_No from TSPL_PROVISION_ENTRY where Ref_Doc_No='" + fndChallanNoMcc.Value + "' and Prov_type='Freight'", trans)
                If clsCommon.myLen(strProvNo) > 0 Then

                    Qry = "delete from tspl_provision_entry where Ref_Doc_No ='" + fndChallanNoMcc.Value + "'"
                    clsDBFuncationality.ExecuteNonQuery(Qry, trans)

                    Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='PR-EN' and Source_Doc_No='" + strProvNo + "'", trans)
                    If clsCommon.myLen(VoucherNo) > 0 Then
                        Qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                        clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                        Qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                        clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                    End If

                    'clsProvisionEntry.ReverseAndUnpost(strProvNo, trans)
                    'clsProvisionEntry.deleteData(strProvNo, trans)
                    clsCommon.MyMessageBoxShow("Provision Delete successfully", Me.Text)
                End If

                trans.Commit()
                LoadData(fndGateEntryNO.Value, IIf(chkMccProc.IsChecked, "MccProc", "BulkProc"), NavigatorType.Current)
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        End If
    End Sub

    Private Sub txtGateOutNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtGateOutNo._MYValidating
        Dim strwhrclause As String = String.Empty
        Dim qry As String = "select GATE_OUT_NO as Code from TSPL_MCC_TANKER_GATE_OUT "
        strwhrclause = " TSPL_MCC_TANKER_GATE_OUT.IsContractor=1 and TSPL_MCC_TANKER_GATE_OUT.GATE_OUT_NO not in (select Against_Gate_Out from tspl_gate_entry_details where IsAgainstGateOut=1 and isnull(Against_Gate_Out,'')<>'' and Gate_Entry_No <>'" & fndGateEntryNO.Value & "')"
        txtGateOutNo.Value = clsCommon.ShowSelectForm("GateOutNo", qry, "Code", strwhrclause, txtGateOutNo.Value, "Code", isButtonClicked)
        If clsCommon.myLen(txtGateOutNo.Value) > 0 Then
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select * from TSPL_MCC_TANKER_GATE_OUT where GATE_OUT_NO ='" & txtGateOutNo.Value & "'")
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                'fndLocationBulk.Value = clsCommon.myCstr(dt.Rows(0)("LOCATION_CODE"))
                txtTankerNoBulk.Text = clsCommon.myCstr(dt.Rows(0)("TANKER_NO"))
                txtOpeningKM.Text = clsCommon.myCdbl(dt.Rows(0)("Opening_Km"))
                txtTollAmount.Text = clsCommon.myCdbl(dt.Rows(0)("TollAmount"))
            End If
        End If
    End Sub

    Private Sub chkForContractor_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkAgainstGateOutNo.ToggleStateChanged
        If CreateProvisionforBulkContractorInGateIn Then
            If chkAgainstGateOutNo.Checked = True Then
                txtGateOutNo.Visible = True
                lblGateOut.Visible = True
            Else
                txtGateOutNo.Visible = False
                lblGateOut.Visible = False
            End If
        Else
            txtGateOutNo.Visible = False
            lblGateOut.Visible = False
        End If
    End Sub

    Private Sub txtRoute__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtRoute._MYValidating
        Try
            Dim qry As String = " select ROUTE_NO as Code,ROUTE_NAME as Name from  TSPL_BULK_ROUTE_MASTER "
            Dim whrCls As String = "exists(select 1 from TSPL_BULK_ROUTE_MASTER_MCC where TSPL_BULK_ROUTE_MASTER_MCC.ROUTE_NO=TSPL_BULK_ROUTE_MASTER.ROUTE_NO )"
            txtRoute.Value = clsCommon.ShowSelectForm("ddGEShUp", qry, "Code", whrCls, txtRoute.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
