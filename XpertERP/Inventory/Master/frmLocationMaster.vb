
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports System.Text.RegularExpressions
Imports System.Globalization
Imports common

Public Class frmLocationMaster
    Inherits FrmMainTranScreen


#Region "Constructor"
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub
#End Region
#Region "Variables"
    Dim intShowOptionofDispatchFromDOGP As Integer = 0
    Dim strDispatchRef As String = Nothing
    Const CatcolCode As String = "CatcolCode"
    Const CatcolCodeDesc As String = "CatcolCodeDesc"
    Const CatcolValue As String = "CatcolValue"
    Const CatcolValueDesc As String = "CatcolValueDesc"

    Dim isEdit As Boolean = False
    Dim userCode, companyCode As String
    Dim sql As String
    Dim ds As DataSet
    Dim dr As DataTable
    Dim objstr As String = "Tecxpert Software Pvt Ltd"
    Const colTTaxRate As String = "TAXRATE"
    Const colTaxLineNo As String = "colTaxLineNo"
    Const ColApply As String = "ColApply"
    Const colTTaxAutCode = "colTTaxAutCode"

    Private isInsideLoadData As Boolean = False
    Private isCellValueChangedOpen As Boolean = False

    Private CheckedChange As Boolean = True



    Const colSelect As String = "colSelect"
    Const colGrpCode As String = "colTaxGrpCode"
    Const colGrpDesc As String = "colTaxGrpDesc"
    Const colTaxCode As String = "colTaxCode"
    Const colTaxDesc As String = "colTaxDesc"
    Const colTaxRate As String = "colTaxRate"
    Const colDefault As String = "colDefault"
    Const colDefaultGST As String = "colDefaultGST"


    Const colSelectItem As String = "colSelectItem"
    Const colItemCode As String = "colItemCode"
    Const colItemDesc As String = "colItemDesc"

    Public PlantDepotMappingMandatory As Boolean = False


    Dim InterState As String = Nothing
    Dim LocalState As String = Nothing


#End Region
#Region "Page Load"

    Private Sub LoadCSACommissionType()
        Dim qry As String = "select '' as Code,'None' as Name union all select 'R' as Code,'Rs.' as Name union all select 'P' as Code,'%(Pers)' as Name"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        cmbComm_Type.DataSource = Nothing

        cmbComm_Type.DataSource = dt
        cmbComm_Type.DisplayMember = "Name"
        cmbComm_Type.ValueMember = "Code"
    End Sub

    Sub LoadLocationCategory()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = Nothing

        dr = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "MCC"
        dr("Name") = "MCC"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "HO"
        dr("Name") = "HO"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "PLANT"
        dr("Name") = "PLANT"
        dt.Rows.Add(dr)

        cmbloc_cate.DataSource = Nothing

        cmbloc_cate.DataSource = dt
        cmbloc_cate.DisplayMember = "Name"
        cmbloc_cate.ValueMember = "Code"
    End Sub

    Public Sub SetLength()
        fndLocation.MyMaxLength = 12
        txtLocationDesc.MaxLength = 50
        txtAdd1.MaxLength = 50
        txtAdd2.MaxLength = 50
        txtAdd3.MaxLength = 50
        txtAdd4.MaxLength = 50
        txtCity.MaxLength = 12
        TxtHOAdd1.MaxLength = 50
        TxtHoAdd2.MaxLength = 50
        txtNearestCity.MaxLength = 30
        TxtESICNo.Text = 30
        TxtPFNo.Text = 30
        txtstateprovince.MaxLength = 100
        txtCountry.MaxLength = 50
        ''txtTelephone.MaxLength = 50
        ''txtPhone1.MaxLength = 20
        ''txtPhone2.MaxLength = 20
        txtEmail.MaxLength = 50
        txtTinNo.MaxLength = 30
        txtCSTNo.MaxLength = 30
        txtTanNo.MaxLength = 30
        txtTcanNo.MaxLength = 30
        txtServiceTaxRegN.MaxLength = 30
        txtEccNumber.MaxLength = 100
        txtRegistration.MaxLength = 100
        txtCommissionerate.MaxLength = 100
        txtRangeCode.MaxLength = 100
        txtRangeName.MaxLength = 100
        txtRangeAddress.MaxLength = 100
        txtDivisionCode.MaxLength = 100
        txtDivisionName.MaxLength = 100
        txtDivisionAddress.MaxLength = 100
    End Sub

    Private Sub RadForm1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InterState = "IGST"
        LocalState = "SGST,'CGST','UGST'"

        '= KUNAL > TICKET : BM00000009585 ===
        checkCommissonReq.Visible = False
        txtcsa_commision.Enabled = False
        txtcsa_comm_type.Enabled = False
        cmbComm_Type.Enabled = False

        '====================================
        ' cmbComm_Type.Enabled = True
        SetUserMgmtNew()
        'LoadBlankSaleGridTax()
        LoadCSACommissionType()
        LoadBlankGrid(gvPurchaseTaxInterState)
        LoadBlankGrid(gvPurchaseTaxLocal)
        LoadBlankGrid(gvSaleTaxInterState)
        LoadBlankGrid(gvSaleTaxLocal)
        'done by stuti on 05/10/2016 against ticket no
        PlantDepotMappingMandatory = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PlantDepotMappingMandatory, clsFixedParameterCode.PlantDepotMappingMandatory, Nothing)) = 1, True, False)

        '===end here
        '' new tab transfer tax
        LoadBlankGrid(gvTransferTaxInterState)
        LoadBlankGrid(gvTransferTaxLocal)

        txtcommsn_code.MendatroryField = True

        LoadTaxGrpHeader(gvPurchaseLocal, "P", "'IGST'")
        LoadTaxGrpHeader(gvPurchaseInterState, "P", "'SGST','CGST','UGST'")

        LoadTaxGrpHeader(gvSaleLocal, "S", "'IGST'")
        LoadTaxGrpHeader(gvSaleInterState, "S", "'SGST','CGST','UGST'")

        '' new tab transfer tax
        LoadTaxGrpHeader(gvTransferLocal, "T", "'IGST'")
        LoadTaxGrpHeader(gvTransferInterState, "T", "'SGST','CGST','UGST'")

        AddHandler fndLocation.KeyPress, AddressOf Location_keypress
        'AddHandler fndLocation.TextChanged, AddressOf TextChanged


        AddHandler fndLocationSegmentCode.Leave, AddressOf fndLocationSegmentCode_Leave
        AddHandler fndLocationSegmentCode.KeyPress, AddressOf fndLocationSegmentCode_KeyPress



        'AddHandler fndSalesTaxGroup.Leave, AddressOf fndSalesTaxGroup_Leave
        'AddHandler fndSalesTaxGroup.KeyPress, AddressOf fndSalesTaxGroup_KeyPress
        'gvSaleTax.Rows.AddNew()
        'gvPurchaseTax.Rows.AddNew()
        fndLocation.MyCharacterCasing = CharacterCasing.Upper
        btnSave.Enabled = True
        chkInactive.Enabled = True
        ddlLocationType.Enabled = True
        fndLocation.MyMaxLength = 12
        txtLocationDesc.Enabled = True
        txtInactiveDate.Visible = False
        btnDelete.Enabled = False
        'txtLocationDesc.ReadOnly = True
        txtstateprovince.ReadOnly = True

        If clsCommon.CompairString(MDI.IsLoc_Third_Party, "YES") = CompairStringResult.Equal Then
            RadGroupBox1.Visible = True
        Else
            RadGroupBox1.Visible = False
        End If

        LoadLocationCategory()
        '' Anubhooti 30-July-2014
        funReset()
        ''
        RadPageViewPage4.Item.Visibility = ElementVisibility.Collapsed
        If MDI.IsLoaction_NLevel = "YES" Then
            RadPageViewPage4.Item.Visibility = ElementVisibility.Visible
        End If
        '----------For Custom Fields----------
        RadPageView1.Pages("pvpCustomFields").Item.Visibility = MyBase.customFieldTabProperty
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.Report_ID = MyBase.Form_ID

            UcCustomFields1.LoadCustomControls()
        End If
        '---------End of For Custom Fields----
        '' Anubhooti 31-July-2014
        RadPageView1.SelectedPage = Details

        txtcommsn_code.Visible = False
        txtcommsn_desc.Visible = False
        MyLabel10.Visible = False
        If clsCommon.myCBool(IIf(clsFixedParameter.GetData(clsFixedParameterType.AP_INV_COMMSN, clsFixedParameterCode.AP_INV_COMMSN, Nothing) = "0", False, True)) = True Then
            txtcommsn_code.MendatroryField = True
        Else
            txtcommsn_code.MendatroryField = False
        End If
        intShowOptionofDispatchFromDOGP = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SHowOptionOnLocationForDairyDispatchfromDOorGatepass, clsFixedParameterCode.SHowOptionOnLocationForDairyDispatchfromDOorGatepass, Nothing)) = "1", 1, 0)

        If intShowOptionofDispatchFromDOGP = 1 Then
            grpDairyDispatchFromDO.Visible = True
            rbtnDispatchfromDO.Visible = True
            rbtnDispatchFromGAtepass.Visible = True
        Else
            grpDairyDispatchFromDO.Visible = False
            rbtnDispatchfromDO.Visible = False
            rbtnDispatchFromGAtepass.Visible = False
        End If
        If objCommonVar.RCDFCFP = True Then
            txt_capacity.Enabled = True
        Else
            txt_capacity.Enabled = False
        End If

    End Sub


#End Region
#Region "Button Click Event"
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If allowToSave() Then SaveData()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message.ToString())
        End Try
    End Sub



    Function allowToSave() As Boolean


        btnSave.Focus()
        If chkExcisable.Checked = True Then
            'If clsCommon.myLen(fndSalesTaxGroup.Value) <= 0 Then
            '    RadPageView1.SelectedPage = RadPageViewPage1
            '    myMessages.blankValue("Sale Tax Group")
            '    fndSalesTaxGroup.Focus()
            '    Return False
            'End If
            'If clsCommon.myLen(fndSaleTaxGroupIS.Value) <= 0 Then
            '    RadPageView1.SelectedPage = RadPageViewPage1
            '    myMessages.blankValue("Sale Tax Group")
            '    fndSaleTaxGroupIS.Focus()
            '    Return False
            'End If
        End If
        If PlantDepotMappingMandatory Then
            If TxtMultiLocation.arrValueMember Is Nothing AndAlso TxtMultiLocation.arrValueMember.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Select depot type location to map with plant type ", Me.Text)
                Return False
            End If
        End If

        If TxtMultiLocation.arrValueMember IsNot Nothing AndAlso TxtMultiLocation.arrValueMember.Count > 0 Then
            Dim strcode = clsDBFuncationality.getSingleValue("select depot_location_code from tspl_location_plantdepot_detail where depot_location_code in (" + clsCommon.GetMulcallString(TxtMultiLocation.arrValueMember) + ")")
            If clsCommon.myLen(strcode) > 0 Then
                clsCommon.MyMessageBoxShow(Me, "Depot type location already mapped with another plant type", Me.Text)
                Return False
            End If
            Dim strc1 = clsDBFuncationality.getSingleValue("select location_code from tspl_location_master where location_code='" + clsCommon.myCstr(fndLocation.Value) + "'")
            If clsCommon.myLen(strc1) > 0 Then
                Dim strce = clsDBFuncationality.getSingleValue("select location_code from TSPL_LOCATION_MASTER where Type ='Plant' and ((GIT_Type='N') or ISNULL(GIT_Type,'')='') and ((CSA_Type='N') or ISNULL(CSA_Type,'')='') and ((Is_Section='N') or ISNULL(Is_Section,'')='') and ((Is_Sub_Location = 'N') or ISNULL(Is_Sub_Location,'')='') and location_code='" + clsCommon.myCstr(fndLocation.Value) + "'")
                If clsCommon.myLen(strce) > 0 Then
                Else
                    clsCommon.MyMessageBoxShow(Me, "Location should not GIT,CSA,Section and Sub location but must be of plant type.", Me.Text)
                    Return False
                End If
            End If
        End If

        '====end here=====

        If intShowOptionofDispatchFromDOGP = 1 Then
            If clsCommon.CompairString(strDispatchRef, "D") = CompairStringResult.Equal AndAlso rbtnDispatchfromDO.IsChecked = False Then
                Dim strCode = clsDBFuncationality.getSingleValue("select  top 1 TSPL_SD_SHIPMENT_HEAD.DOCUMENT_CODE from TSPL_SD_SHIPMENT_HEAD left outer join  TSPL_SD_SHIPMENT_DETAIL on " &
                 "TSPL_SD_SHIPMENT_HEAD.DOCUMENT_CODE=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE  where Delivery_Code <> '' and Bill_To_Location='" & fndLocation.Value & "' ")
                If clsCommon.myLen(strCode) > 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Dispatch From DO, Cannot change this setting. Location is already in use", Me.Text)
                    Return False
                End If
            ElseIf clsCommon.CompairString(strDispatchRef, "G") = CompairStringResult.Equal AndAlso rbtnDispatchFromGAtepass.IsChecked = False Then
                Dim strCode = clsDBFuncationality.getSingleValue("select top 1 TSPL_SD_SHIPMENT_HEAD.DOCUMENT_CODE from TSPL_SD_SHIPMENT_HEAD left outer join  TSPL_SD_SHIPMENT_DETAIL on " &
                 "TSPL_SD_SHIPMENT_HEAD.DOCUMENT_CODE=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE where TSPL_SD_SHIPMENT_HEAD.GatePass_No  <> '' and Bill_To_Location='" & fndLocation.Value & "'")
                If clsCommon.myLen(strCode) > 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Dispatch From Gatepass, Cannot change setting. Location is already in use", Me.Text)
                    Return False
                End If
            End If
        End If

        'Sanjay BHA/13/07/18-000159 Check Section and Main location combination already made  ''richa do it setting based as per ranjana mam BHA/02/08/18-000214
        If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowmultipleconsumptionLocation, clsFixedParameterCode.AllowmultipleconsumptionLocation, Nothing)), "0") = CompairStringResult.Equal Then
            If chkconsumption.Checked And clsCommon.myLen(TxtSection.Value) > 0 And clsCommon.myLen(TxtMainLoc.Value) > 0 Then
                Dim qry As String = clsDBFuncationality.getSingleValue(clsCommon.myCstr("Select count(*)  from TSPL_LOCATION_MASTER where Is_Consumption_Location=1 and Section_Code='" & TxtSection.Value & "' and Main_Location_Code='" & TxtMainLoc.Value & "' and Location_code<> '" & fndLocation.Value & "'"))
                If qry > 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "One Consumption location is already made for this section code and main location")
                    Return False
                End If
            End If
        End If
        If clsCommon.myLen(fndLocation.Value) <= 0 Then
            RadPageView1.SelectedPage = Details
            myMessages.blankValue("Location Code")
            fndLocation.Focus()
            Return False
        ElseIf clsCommon.myLen(txtLocShortName.Text) <= 0 Then
            txtLocShortName.Text = txtLocationDesc.Text

        ElseIf clsCommon.myLen(fndstateprovince.Value) <= 0 Then
            RadPageView1.SelectedPage = Details
            myMessages.blankValue("State Code")
            fndstateprovince.Focus()
            Return False
        ElseIf (clsCommon.CompairString(clsCommon.myCstr(ddlLocationType.Text), "Logical") = CompairStringResult.Equal And chkExcisable.Checked = True) Then
            RadPageView1.SelectedPage = Details
            common.clsCommon.MyMessageBoxShow(Me, "Excisable can not be checked with location type logical")
            Return False
        ElseIf chkExcisable.Checked = True Then
            If clsCommon.myLen(txtEccNumber.Text) <= 0 Then
                RadPageView1.SelectedPage = Details
                myMessages.blankValue("ECC number")
                txtEccNumber.Focus()
                Return False
            ElseIf clsCommon.myLen(txtEccNumber.Text) < 15 Then
                RadPageView1.SelectedPage = Details
                common.clsCommon.MyMessageBoxShow(Me, "ECC number must have minimum of 15 character")
                Return False
            End If
        ElseIf chkInsurance.Checked = True Then
            If clsCommon.myLen(txtInsurance.Text) = 0 Then
                myMessages.blankValue("Insurance No")
                txtInsurance.Focus()
                Return False
            End If
            If txtFromDate.Value.Date > txtToDate.Value.Date Then
                common.clsCommon.MyMessageBoxShow(Me, "Insurance To date can not be before than from date.")
                txtToDate.Focus()
                Return False
            End If
        End If


        If clsCommon.CompairString(clsCommon.myCstr(ddlLocationType.Text), "Physical") = CompairStringResult.Equal Then
            If clsCommon.myLen(fndLocationSegmentCode.Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage2
                myMessages.blankValue("Location Segment Code")
                fndLocationSegmentCode.Focus()
                Return False
            End If
        End If
        Dim check As Match = Regex.Match(txtEmail.Text, "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")
        If check.Success = False And txtEmail.Text <> "" Then
            RadPageView1.SelectedPage = Details
            common.clsCommon.MyMessageBoxShow(Me, "Please insert the proper format of e-mail address", Me.Text)
            txtEmail.Text = ""
            txtEmail.Focus()
            Return False
        End If



        If chkthirdparty.Checked AndAlso clsCommon.myLen(txtvndrcode.Value) <= 0 Then
            RadPageView1.SelectedPage = Details
            common.clsCommon.MyMessageBoxShow(Me, "Please select vendor for third party location", Me.Text)
            txtvndrcode.Focus()
            txtvndrcode.Select()
            Return False
        End If
        If clsCommon.CompairString(cmbloc_cate.Text, "MCC") = CompairStringResult.Equal AndAlso clsCommon.CompairString(ddlType.Text, "PLANT") = CompairStringResult.Equal Then
            RadPageView1.SelectedPage = Details
            common.clsCommon.MyMessageBoxShow(Me, "The Location Of Category MCC can Not Have Type PLANT", Me.Text)
            ddlType.Text = ""
            Return False
        End If
        If clsCommon.myLen(txtZipPostalCode.Text) > 0 Then
            If clsCommon.myLen(txtZipPostalCode.Text) <> 6 Then
                RadPageView1.SelectedPage = Details
                common.clsCommon.MyMessageBoxShow(Me, "Invalid Zip/Postal Code.Please Enter Zip/Postal Code 6 Digit.", Me.Text)
                txtZipPostalCode.Focus()
                txtZipPostalCode.Select()
                Return False
            End If
        End If
        If clsCommon.myLen(txtCategoryStructureCode.Value) > 0 Then
            If clsCommon.myLen(gvCategory.Rows(0).Cells(0).Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage4
                clsCommon.MyMessageBoxShow(Me, "First mapped Location category values with Location category structure", Me.Text)
                gvCategory.Focus()
                gvCategory.Select()
                Return False
            End If

            For Each grow As GridViewRowInfo In gvCategory.Rows
                If clsCommon.myLen(grow.Cells(CatcolCode).Value) > 0 AndAlso clsCommon.myLen(grow.Cells(CatcolValue).Value) <= 0 Then
                    RadPageView1.SelectedPage = RadPageViewPage4
                    clsCommon.MyMessageBoxShow(Me, "Please select category values", Me.Text)
                    gvCategory.Focus()
                    gvCategory.Select()
                    Return False
                End If
            Next
        End If
        If rdbbtnSection.IsChecked = True AndAlso (clsCommon.myLen(TxtMainLoc.Value) <= 0 Or clsCommon.myLen(TxtSection.Value) <= 0) Then
            RadPageView1.SelectedPage = Details
            common.clsCommon.MyMessageBoxShow("Main Location And Section Code can not be left blank", Me.Text)
            TxtMainLoc.Focus()
            TxtMainLoc.Select()
            Return False
        End If
        If rdbbtnSubLoc.IsChecked = True AndAlso clsCommon.myLen(TxtMainLoc.Value) <= 0 Then
            RadPageView1.SelectedPage = Details
            common.clsCommon.MyMessageBoxShow(Me, "Main Location can not be left blank", Me.Text)
            TxtMainLoc.Focus()
            TxtMainLoc.Select()
            Return False
        End If
        Dim MainLocSegmentCode As String
        Dim SubLocVendorState As String = Nothing
        Dim SubLocState As String = Nothing

        If rdbbtnSection.IsChecked = True Or rdbbtnSubLoc.IsChecked = True Then
            MainLocSegmentCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ISNULL( Loc_Segment_Code,'') As Loc_Segment_Code  from TSPL_LOCATION_MASTER Where Location_Code='" & clsCommon.myCstr(TxtMainLoc.Value) & "'"))
            SubLocVendorState = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select TSPL_VENDOR_MASTER.state_code from TSPL_VENDOR_MASTER where Vendor_Code='" & FndJobworkVendor.Value & "'"))
            SubLocState = clsCommon.myCstr(fndstateprovince.Value)
            ' If clsCommon.myCstr(TxtMainLoc.Value) <> clsCommon.myCstr(fndLocationSegmentCode.Value) Then'
            If ChkIsJobwork.Checked = True Then
                If clsCommon.CompairString(clsCommon.myCstr(fndLocationSegmentCode.Value), MainLocSegmentCode) = CompairStringResult.Equal Then
                    RadPageView1.SelectedPage = Details
                    common.clsCommon.MyMessageBoxShow(Me, "Please check ! Main location segment code and location segment code should not be same", Me.Text)
                    TxtMainLoc.Focus()
                    TxtMainLoc.Select()
                    Return False
                End If
                If clsCommon.CompairString(SubLocVendorState, SubLocState) <> CompairStringResult.Equal Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please check ! Job Work Vendor State and State/Provience should be same.", Me.Text)
                    Return False
                End If
            Else
                If clsCommon.CompairString(clsCommon.myCstr(fndLocationSegmentCode.Value), MainLocSegmentCode) <> CompairStringResult.Equal Then
                    RadPageView1.SelectedPage = Details
                    common.clsCommon.MyMessageBoxShow(Me, "Please check ! Main location segment code and location segment code should  be same", Me.Text)
                    TxtMainLoc.Focus()
                    TxtMainLoc.Select()
                    Return False
                End If
            End If
            If clsCommon.CompairString(clsCommon.myCstr(fndLocation.Value), clsCommon.myCstr(TxtMainLoc.Value)) = CompairStringResult.Equal Then
                RadPageView1.SelectedPage = Details
                common.clsCommon.MyMessageBoxShow(Me, "Please check ! Main location code and location code can not be same", Me.Text)
                TxtMainLoc.Focus()
                TxtMainLoc.Select()
                Return False
            End If
            If clsCommon.CompairString(ddlLocationType.Text, "Virtual") = CompairStringResult.Equal And chkUseInJobWork.Checked Then
                Dim strVirualLoc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Code from tspl_location_master where  isnull(is_sub_location,'N')='Y' and  Main_Location_Code='" & clsCommon.myCstr(TxtMainLoc.Value) & "' and Location_Type='Virtual' and UseInJobWork=1  and Location_Code <> '" & clsCommon.myCstr(fndLocation.Value) & "'"))
                If clsCommon.myLen(strVirualLoc) > 0 Then
                    RadPageView1.SelectedPage = Details
                    common.clsCommon.MyMessageBoxShow(Me, "Please check ! " & strVirualLoc & " Virtual location already exits for main location " & clsCommon.myCstr(TxtMainLoc.Value) & " Job Type and use in Job work is On.", Me.Text)
                    TxtMainLoc.Focus()
                    TxtMainLoc.Select()
                    Return False
                End If
            End If
        End If

        '==================CSA Check======================================================
        If chkCSA.Checked AndAlso clsCommon.myLen(fndCustomer.Value) <= 0 Then
            RadPageView1.SelectedPage = Details
            clsCommon.MyMessageBoxShow("Select customer detail for CSA", Me.Text)
            fndCustomer.Focus()
            fndCustomer.Select()
            Return False
        End If
        If chkCSA.Checked AndAlso checkCommissonReq.Checked AndAlso clsCommon.myCdbl(txtcsa_commision.Text) <= 0 Then
            RadPageView1.SelectedPage = Details
            clsCommon.MyMessageBoxShow(Me, "Fill commission rate for CSA", Me.Text)
            txtcsa_commision.Focus()
            txtcsa_commision.Select()
            Return False
        End If
        If chkCSA.Checked AndAlso checkCommissonReq.Checked AndAlso clsCommon.myLen(txtcsa_comm_type.Text) <= 0 Then
            RadPageView1.SelectedPage = Details
            clsCommon.MyMessageBoxShow(Me, "Set RT unit in Unit of Measurement for CSA commission rate per unit", Me.Text)
            txtcsa_comm_type.Focus()
            txtcsa_comm_type.Select()
            Return False
        End If
        If chkCSA.Checked AndAlso checkCommissonReq.Checked AndAlso clsCommon.myLen(cmbComm_Type.SelectedValue) <= 0 Then
            RadPageView1.SelectedPage = Details
            clsCommon.MyMessageBoxShow(Me, "Select commission rate type for CSA", Me.Text)
            cmbComm_Type.Select()
            Return False
        End If

        If chkCSA.Checked AndAlso clsCommon.myLen(txtvndrcode.Value) <= 0 Then
            RadPageView1.SelectedPage = Details
            clsCommon.MyMessageBoxShow(Me, "Select vendor detail for CSA", Me.Text)
            txtvndrcode.Focus()
            txtvndrcode.Select()
            Return False
        End If

        If chkCSA.Checked AndAlso clsCommon.myLen(txtcommsn_code.Value) <= 0 AndAlso txtcommsn_code.MendatroryField = True Then
            RadPageView1.SelectedPage = Details
            clsCommon.MyMessageBoxShow(Me, "Select Commission A/c Detail For CSA", Me.Text)
            txtcommsn_code.Focus()
            txtcommsn_code.Select()
            Return False
        End If

        If chkconsumption.Checked AndAlso clsCommon.myLen(TxtSection.Value) <= 0 Then
            RadPageView1.SelectedPage = Details
            clsCommon.MyMessageBoxShow(Me, "Select section for consumption location.")
            TxtSection.Focus()
            TxtSection.Select()
            Return False
        End If
        If ChkIsJobwork.Checked Then
            'If clsCommon.myLen(clsCommon.myCstr(FndJobworkItem.Value)) <= 0 Then
            '    RadPageView1.SelectedPage = RadPageViewPage12
            '    clsCommon.MyMessageBoxShow("Select Jobwork Item ")
            '    FndJobworkItem.Focus()
            '    FndJobworkItem.Select()
            '    Return False
            'End If
            If gvItem.CheckedValue.Count <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage12
                clsCommon.MyMessageBoxShow(Me, "Select Jobwork Item ")
                gvItem.Focus()
                gvItem.Select()
                Return False
            End If
            If clsCommon.myLen(clsCommon.myCstr(FndJobworkVendor.Value)) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage12
                clsCommon.MyMessageBoxShow(Me, "Select Jobwork Vendor")
                FndJobworkVendor.Focus()
                FndJobworkVendor.Select()
                Return False
            End If
        End If

        If objCommonVar.GSTApplicable = True Then
            Dim GSTFinal As String = ""
            If chkregistered.Checked = True AndAlso clsCommon.myLen(txtGSTEntityNo.Text) = 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Enter Valid GST No.")
                Return False
            End If
            If clsCommon.myLen(txtGSTEntityNo.Text) > 0 Then
                GSTFinal = clsCommon.myCstr(txtGstState.Text) + clsCommon.myCstr(txtGSTPANNO.Text) + clsCommon.myCstr(txtGSTEntityNo.Text) + clsCommon.myCstr(txtGSTBlank.Text) + clsCommon.myCstr(txtGSTDegit.Text)
                txtGstNo.Text = GSTFinal
                If clsERPFuncationality.ValidationGSTNO(txtGstState.Text, txtGSTPANNO.Text, GSTFinal, Nothing) = False Then
                    Return False
                End If
            End If
        End If

        Return checkLocationWiseTax("Sale Tax Local", gvSaleTaxLocal) AndAlso checkLocationWiseTax("Sale Tax Inter State", gvSaleTaxInterState) AndAlso checkLocationWiseTax("Purchase Tax Local", gvPurchaseTaxLocal) AndAlso checkLocationWiseTax("Purchase Tax Inter State", gvPurchaseTaxInterState) AndAlso checkLocationWiseTax("Transfer Tax Local", gvTransferTaxLocal) AndAlso checkLocationWiseTax("Transfer Tax Inter State", gvTransferTaxInterState) AndAlso checkDefaultGrp("Sale Tax Local", gvSaleLocal) AndAlso checkDefaultGrp("Sale Tax Inter State", gvSaleInterState) AndAlso checkDefaultGrp("Purchase Tax Local", gvPurchaseLocal) AndAlso checkDefaultGrp("Purchase Tax Inter State", gvPurchaseInterState) AndAlso checkDefaultGrp("Transfer Tax Local", gvTransferLocal) AndAlso checkDefaultGrp("Transfer Tax Inter State", gvTransferInterState)
    End Function

    Function checkLocationWiseTax(ByVal strGridName As String, ByVal gv As RadGridView) As Boolean
        For ii As Integer = 0 To gv.Rows.Count - 1
            Dim strTaxGrp As String = clsCommon.myCstr(gv.Rows(ii).Cells(colGrpCode).Value)
            Dim strTax As String = clsCommon.myCstr(gv.Rows(ii).Cells(colTaxCode).Value)
            Dim isFound As Boolean = False
            For jj As Integer = 0 To gv.Rows.Count - 1
                Dim isSelectInner As Boolean = clsCommon.myCBool(gv.Rows(jj).Cells(colSelect).Value)
                Dim strTaxGrpInner As String = clsCommon.myCstr(gv.Rows(jj).Cells(colGrpCode).Value)
                Dim strTaxInner As String = clsCommon.myCstr(gv.Rows(jj).Cells(colTaxCode).Value)
                Dim strTaxRateInner As Double = clsCommon.myCdbl(gv.Rows(jj).Cells(colTaxRate).Value)
                Dim isDefaultInner As Boolean
                isDefaultInner = clsCommon.myCBool(gv.Rows(jj).Cells(colDefault).Value)

                If clsCommon.CompairString(strTaxGrp, strTaxGrpInner) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strTax, strTaxInner) = CompairStringResult.Equal Then
                    If isSelectInner AndAlso isDefaultInner Then
                        isFound = True
                    End If
                End If
            Next
            If Not isFound Then
                clsCommon.MyMessageBoxShow(strGridName + Environment.NewLine + "Please select at least one row with default rate." + Environment.NewLine + "Group" + strTaxGrp + " and tax " + strTax)
                Return False
            End If
        Next
        Return True
    End Function

    Function checkDefaultGrp(ByVal strGridName As String, ByVal gv As RadGridView) As Boolean
        Dim IsSelectAtLeastOneGrp As Boolean = False
        For ii As Integer = 0 To gv.Rows.Count - 1
            If clsCommon.myCBool(gv.Rows(ii).Cells("Sel").Value) Then
                IsSelectAtLeastOneGrp = True
            End If
            If clsCommon.myCBool(gv.Rows(ii).Cells("Is_Default").Value) Then
                Return True
            End If
            If clsCommon.myCBool(gv.Rows(ii).Cells("Is_Default_GST").Value) Then
                Return True
            End If
        Next
        If IsSelectAtLeastOneGrp Then
            clsCommon.MyMessageBoxShow(Me, "Please select at least one default tax group for " + strGridName)
        End If

        Return IIf(IsSelectAtLeastOneGrp, False, True)
    End Function

    Sub SaveData()

        If MyBase.isModifyonPasswordFlag Then
            If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.locationMaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
            Else
                Return
            End If
        End If

        Dim arr As New List(Of clsLocation)
        Dim obj_Jowork As New List(Of ClsLocation_JobworkItem)
        Dim obj As clsLocation = New clsLocation()
        Dim Arrplantdepotmapping As New List(Of clsLocationPlantDepotMapping)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try

            If rbtnDispatchfromDO.IsChecked = True Then
                obj.DairyDispatchFromDO = 1
            Else
                obj.DairyDispatchFromDO = 0
            End If
            If chkExcisable.Checked = True Then
                obj.Excisable = "T"
            Else
                obj.Excisable = "F"
            End If
            If chkduty.Checked = True Then
                obj.DutyPaid = "Y"
            Else
                obj.DutyPaid = "N"
            End If
            If chkParlour.Checked = True Then
                obj.IsParlour = "Y"
            Else
                obj.IsParlour = "N"
            End If
            Dim dt2 As String = Format(Date.Today, "dd/MM/yyyy")
            If chkInactive.Checked = True Then
                obj.Loc_Status = "Y"
                obj.Status_Date = dt2
            Else
                obj.Loc_Status = "N"
            End If

            If clsCommon.CompairString(cmbNoOfShift.Text, "Select") = CompairStringResult.Equal Then
                obj.No_Of_Shift = Nothing
            Else
                obj.No_Of_Shift = CInt(cmbNoOfShift.Text)
            End If

            obj.loc_categry = clsCommon.myCstr(cmbloc_cate.SelectedValue)

            obj.Location_Code = clsCommon.myCstr(fndLocation.Value)
            obj.Location_Desc = clsCommon.myCstr(txtLocationDesc.Text)

            obj.Short_Name = clsCommon.myCstr(txtLocShortName.Text)
            obj.PAN_No = clsCommon.myCstr(txtPANNo.Text)
            obj.Add1 = clsCommon.myCstr(txtAdd1.Text)
            obj.Add2 = clsCommon.myCstr(txtAdd2.Text)
            obj.Add3 = clsCommon.myCstr(txtAdd3.Text)
            obj.Add4 = clsCommon.myCstr(txtAdd4.Text)
            obj.HOAdd1 = clsCommon.myCstr(TxtHOAdd1.Text)
            obj.HOAdd2 = clsCommon.myCstr(TxtHoAdd2.Text)
            obj.City_Code = clsCommon.myCstr(txtCity.Text)
            obj.State = clsCommon.myCstr(fndstateprovince.Value)
            obj.Pin_Code = clsCommon.myCstr(txtZipPostalCode.Text)
            obj.Country = clsCommon.myCstr(txtCountry.Text)
            obj.Telphone = clsCommon.myCstr(txtTelephone.Text)
            obj.Email = clsCommon.myCstr(txtEmail.Text)
            obj.Location_Type = clsCommon.myCstr(ddlLocationType.Text)
            obj.Loc_Segment_Code = clsCommon.myCstr(fndLocationSegmentCode.Value)
            obj.Type = clsCommon.myCstr(ddlType.Text)
            'obj.Purchase_Tax_Group = clsCommon.myCstr(fndPurchaseTaxGroup.Value)
            'obj.Sales_Tax_Group = clsCommon.myCstr(fndSalesTaxGroup.Value)
            obj.Ecc_Number = clsCommon.myCstr(txtEccNumber.Text)
            obj.Registration_Number = clsCommon.myCstr(txtRegistration.Text)
            obj.Commissionerate = clsCommon.myCstr(txtCommissionerate.Text)
            obj.Range_Code = clsCommon.myCstr(txtRangeCode.Text)
            obj.Range_Name = clsCommon.myCstr(txtRangeName.Text)
            obj.Range_Address = clsCommon.myCstr(txtRangeAddress.Text)
            obj.Division_Code = clsCommon.myCstr(txtDivisionCode.Text)
            obj.Division_Name = clsCommon.myCstr(txtDivisionName.Text)
            obj.Division_Address = clsCommon.myCstr(txtDivisionAddress.Text)
            obj.TIN_No = clsCommon.myCstr(txtTinNo.Text)
            obj.TAN_No = clsCommon.myCstr(txtTanNo.Text)
            obj.TCAN_No = clsCommon.myCstr(txtTcanNo.Text)
            obj.Service_Tax_Reg_No = clsCommon.myCstr(txtServiceTaxRegN.Text)
            'obj.Purchase_Tax_GroupIS = clsCommon.myCstr(fndPurchaseTaxGroupIS.Value)
            'obj.Sales_Tax_GroupIS = clsCommon.myCstr(fndSaleTaxGroupIS.Value)
            obj.Stock_Transfer_Filled_Ac = clsCommon.myCstr(fndStkTrnsfrFilledAc.Value)
            obj.Stock_Transfer_Empty_Ac = clsCommon.myCstr(fndStkTrnsfrEmptyAc.Value)
            obj.NearestCity = clsCommon.myCstr(txtNearestCity.Text)
            obj.ESIC_No = clsCommon.myCstr(TxtESICNo.Text)
            obj.PF_No = clsCommon.myCstr(TxtPFNo.Text)
            obj.accountholdername = txtBankaccHolderName.Text
            obj.bankaccno = txtBankAccNo.Text
            obj.bankifsccode = txtBankIFSCCode.Text
            obj.BankUPI_ID = txtBankUPI_Id.Text

            obj.bankBank = txtBank.Text
            obj.bankBranch = txtBranch.Text
            obj.bankACType = txtACType.Text
            obj.CST_No = clsCommon.myCstr(txtCSTNo.Text)
            obj.Phone1 = clsCommon.myCstr(txtPhone1.Text)
            obj.Phone2 = clsCommon.myCstr(txtPhone2.Text)
            If chkGITType.Checked = True Then
                obj.GIT_Type = "Y"
            End If
            obj.GIT_Location = clsCommon.myCstr(fndGITLocation.Value)
            obj.Rejected_Type = IIf(chkRejected.Checked, "Y", "N")
            obj.CSA_Type = IIf(chkCSA.Checked, "Y", "N")

            obj.Vendor_Commsn_ACC = clsCommon.myCstr(txtcommsn_code.Value)
            obj.Vendor_Commsn_ACC_Desc = clsCommon.myCstr(txtcommsn_desc.Text)
            obj.csa_commision_rate = clsCommon.myCdbl(txtcsa_commision.Text)
            obj.csa_commision_type = clsCommon.myCstr(txtcsa_comm_type.Text)
            obj.CSA_Commission_RS_PERS = clsCommon.myCstr(cmbComm_Type.SelectedValue)
            obj.vendrocode = ""
            If chkthirdparty.Checked OrElse chkCSA.Checked Then
                obj.vendrocode = clsCommon.myCstr(txtvndrcode.Value)
            End If

            obj.Rejected_Location = fndRejectedLoc.Value
            obj.Cust_Code = fndCustomer.Value

            '' Anubhooti 31-July-2014 BM00000003350
            If rdbbtnSection.IsChecked = True Then
                obj.Is_Section = "Y"
            Else
                obj.Is_Section = "N"
            End If
            If rdbbtnSubLoc.IsChecked = True Then
                obj.Is_Sub_Location = "Y"
            Else
                obj.Is_Sub_Location = "N"
            End If
            If chkSubLocationWise.Checked = True Then
                obj.IsSubLocationWise = "Y"
            Else
                obj.IsSubLocationWise = "N"
            End If
            obj.Silo_Capacity = clsCommon.myCdbl(txt_capacity.Text)

            If String.IsNullOrEmpty(clsCommon.myCstr(TxtSection.Value)) Or clsCommon.myLen(TxtSection.Value) <= 0 Then
                obj.Section_Code = Nothing
            Else
                obj.Section_Code = clsCommon.myCstr(TxtSection.Value)
            End If
            If String.IsNullOrEmpty(clsCommon.myCstr(TxtMainLoc.Value)) Or clsCommon.myLen(TxtMainLoc.Value) <= 0 Then
                obj.Main_Location_Code = Nothing
            Else
                obj.Main_Location_Code = clsCommon.myCstr(TxtMainLoc.Value)
            End If
            'obj.Section_Code = clsCommon.myCstr(TxtSection.Value)
            'obj.Main_Location_Code = clsCommon.myCstr(TxtMainLoc.Value)

            obj.Stock_Transfer_ac = clsCommon.myCstr(fndStkTrnsfrAc.Value)
            obj.Loss_Ac = clsCommon.myCstr(fndLossAc.Value)
            obj.Is_Consumption_Location = CInt(clsCommon.myCstr(IIf(chkconsumption.Checked = True, "1", "0")))
            obj.UseInJobWork = CInt(clsCommon.myCstr(IIf(chkUseInJobWork.Checked = True, "1", "0")))

            If chkInsurance.Checked = True Then
                obj.Is_Insurance = CInt(clsCommon.myCstr(IIf(chkInsurance.Checked = True, "1", "0")))
                obj.InsuranceFromDate = txtFromDate.Value
                obj.InsuranceToDate = txtToDate.Value
                obj.InsuranceNo = clsCommon.myCstr(txtInsurance.Text)
            Else
                obj.Is_Insurance = CInt(clsCommon.myCstr(IIf(chkInsurance.Checked = True, "1", "0")))
                obj.InsuranceFromDate = Nothing
                obj.InsuranceToDate = Nothing
                obj.InsuranceNo = Nothing
            End If



            If objCommonVar.GSTApplicable AndAlso clsCommon.myLen(txtGSTEntityNo.Text) > 0 Then
                obj.GSTEntity = clsCommon.myCstr(txtGSTEntityNo.Text)
                obj.GSTBlank = clsCommon.myCstr(txtGSTBlank.Text)
                obj.GSTDegit = clsCommon.myCstr(txtGSTDegit.Text)
                obj.GSTNO = clsCommon.myCstr(txtGstState.Text) + clsCommon.myCstr(txtGSTPANNO.Text) + clsCommon.myCstr(txtGSTEntityNo.Text) + clsCommon.myCstr(txtGSTBlank.Text) + clsCommon.myCstr(txtGSTDegit.Text)
            End If
            obj.Is_Registered = IIf(chkregistered.Checked = True, 1, 0)
            obj.IsMainPlant = IIf(chkIsMainPlant.Checked = True, 1, 0)

            obj.Arr = New List(Of clsLocationWiseTax)
            For ii As Integer = 0 To gvPurchaseTaxLocal.Rows.Count - 1
                If clsCommon.myCBool(gvPurchaseTaxLocal.Rows(ii).Cells(colSelect).Value) Then
                    Dim objTr As New clsLocationWiseTax
                    objTr.Tax_Group_Code = clsCommon.myCstr(gvPurchaseTaxLocal.Rows(ii).Cells(colGrpCode).Value)
                    objTr.Tax_Code = clsCommon.myCstr(gvPurchaseTaxLocal.Rows(ii).Cells(colTaxCode).Value)
                    objTr.TAX_Rate = clsCommon.myCdbl(gvPurchaseTaxLocal.Rows(ii).Cells(colTaxRate).Value)
                    objTr.Is_Default_Tax = clsCommon.myCBool(gvPurchaseTaxLocal.Rows(ii).Cells(colDefault).Value)
                    objTr.Is_Default_Tax_Group = IsGrpDefaultGroup(gvPurchaseLocal, objTr.Tax_Group_Code)
                    objTr.Is_Default_Tax_Group_GST = IsGrpDefaultGroupGST(gvPurchaseLocal, objTr.Tax_Group_Code)
                    objTr.Tax_Type = "P"
                    objTr.Tax_Category = "L"
                    obj.Arr.Add(objTr)
                End If
            Next
            For ii As Integer = 0 To gvPurchaseTaxInterState.Rows.Count - 1
                If clsCommon.myCBool(gvPurchaseTaxInterState.Rows(ii).Cells(colSelect).Value) Then
                    Dim objTr As New clsLocationWiseTax
                    objTr.Tax_Group_Code = clsCommon.myCstr(gvPurchaseTaxInterState.Rows(ii).Cells(colGrpCode).Value)
                    objTr.Tax_Code = clsCommon.myCstr(gvPurchaseTaxInterState.Rows(ii).Cells(colTaxCode).Value)
                    objTr.TAX_Rate = clsCommon.myCdbl(gvPurchaseTaxInterState.Rows(ii).Cells(colTaxRate).Value)
                    objTr.Is_Default_Tax = clsCommon.myCBool(gvPurchaseTaxInterState.Rows(ii).Cells(colDefault).Value)
                    objTr.Is_Default_Tax_Group = IsGrpDefaultGroup(gvPurchaseInterState, objTr.Tax_Group_Code)
                    objTr.Is_Default_Tax_Group_GST = IsGrpDefaultGroupGST(gvPurchaseInterState, objTr.Tax_Group_Code)
                    objTr.Tax_Type = "P"
                    objTr.Tax_Category = "I"
                    obj.Arr.Add(objTr)
                End If
            Next
            For ii As Integer = 0 To gvSaleTaxLocal.Rows.Count - 1
                If clsCommon.myCBool(gvSaleTaxLocal.Rows(ii).Cells(colSelect).Value) Then
                    Dim objTr As New clsLocationWiseTax
                    objTr.Tax_Group_Code = clsCommon.myCstr(gvSaleTaxLocal.Rows(ii).Cells(colGrpCode).Value)
                    objTr.Tax_Code = clsCommon.myCstr(gvSaleTaxLocal.Rows(ii).Cells(colTaxCode).Value)
                    objTr.TAX_Rate = clsCommon.myCdbl(gvSaleTaxLocal.Rows(ii).Cells(colTaxRate).Value)
                    objTr.Is_Default_Tax = clsCommon.myCBool(gvSaleTaxLocal.Rows(ii).Cells(colDefault).Value)
                    objTr.Is_Default_Tax_Group = IsGrpDefaultGroup(gvSaleLocal, objTr.Tax_Group_Code)
                    objTr.Is_Default_Tax_Group_GST = IsGrpDefaultGroupGST(gvSaleLocal, objTr.Tax_Group_Code)
                    objTr.Tax_Type = "S"
                    objTr.Tax_Category = "L"
                    obj.Arr.Add(objTr)
                End If
            Next
            ''richa agawral items detail
            obj.ArrItem = New List(Of clsLocationWiseItems)
            For ii As Integer = 0 To gvSaleItemDetailsLocal.Rows.Count - 1
                If clsCommon.myCBool(gvSaleItemDetailsLocal.Rows(ii).Cells("Sel").Value) Then
                    Dim objTr As New clsLocationWiseItems
                    objTr.Location_Code = clsCommon.myCstr(fndLocation.Value)
                    objTr.Item_code = clsCommon.myCstr(gvSaleItemDetailsLocal.Rows(ii).Cells("Item Code").Value)
                    objTr.Item_desc = clsCommon.myCstr(gvSaleItemDetailsLocal.Rows(ii).Cells("Description").Value)
                    objTr.Item_Category = "L"
                    objTr.Item_Type = "S"
                    obj.ArrItem.Add(objTr)
                End If
            Next

            'stuti
            obj.ArrMappingPlantDepot = New List(Of clsLocationPlantDepotMapping)
            If TxtMultiLocation.arrValueMember IsNot Nothing Then
                For ii As Integer = 0 To TxtMultiLocation.arrValueMember.Count - 1
                    Dim objPD As New clsLocationPlantDepotMapping
                    objPD.Plant_Location_Code = clsCommon.myCstr(fndLocation.Value)
                    objPD.Depot_Location_Code = clsCommon.myCstr(TxtMultiLocation.arrValueMember.Item(ii).ToString())
                    obj.ArrMappingPlantDepot.Add(objPD)
                Next
            End If

            '===end here===

            For ii As Integer = 0 To gvSaleItemDetailsInterState.Rows.Count - 1
                If clsCommon.myCBool(gvSaleItemDetailsInterState.Rows(ii).Cells("Sel").Value) Then
                    Dim objTr As New clsLocationWiseItems
                    objTr.Location_Code = clsCommon.myCstr(fndLocation.Value)
                    objTr.Item_code = clsCommon.myCstr(gvSaleItemDetailsInterState.Rows(ii).Cells("Item Code").Value)
                    objTr.Item_desc = clsCommon.myCstr(gvSaleItemDetailsInterState.Rows(ii).Cells("Description").Value)
                    objTr.Item_Category = "I"
                    objTr.Item_Type = "S"
                    obj.ArrItem.Add(objTr)
                End If
            Next

            For ii As Integer = 0 To gvSaleTaxInterState.Rows.Count - 1
                If clsCommon.myCBool(gvSaleTaxInterState.Rows(ii).Cells(colSelect).Value) Then
                    Dim objTr As New clsLocationWiseTax
                    objTr.Tax_Group_Code = clsCommon.myCstr(gvSaleTaxInterState.Rows(ii).Cells(colGrpCode).Value)
                    objTr.Tax_Code = clsCommon.myCstr(gvSaleTaxInterState.Rows(ii).Cells(colTaxCode).Value)
                    objTr.TAX_Rate = clsCommon.myCdbl(gvSaleTaxInterState.Rows(ii).Cells(colTaxRate).Value)
                    objTr.Is_Default_Tax = clsCommon.myCBool(gvSaleTaxInterState.Rows(ii).Cells(colDefault).Value)
                    objTr.Is_Default_Tax_Group = IsGrpDefaultGroup(gvSaleInterState, objTr.Tax_Group_Code)
                    objTr.Is_Default_Tax_Group_GST = IsGrpDefaultGroupGST(gvSaleInterState, objTr.Tax_Group_Code)
                    objTr.Tax_Type = "S"
                    objTr.Tax_Category = "I"
                    obj.Arr.Add(objTr)
                End If
            Next

            '' new tab transfer tax
            For ii As Integer = 0 To gvTransferTaxLocal.Rows.Count - 1
                If clsCommon.myCBool(gvTransferTaxLocal.Rows(ii).Cells(colSelect).Value) Then
                    Dim objTr As New clsLocationWiseTax
                    objTr.Tax_Group_Code = clsCommon.myCstr(gvTransferTaxLocal.Rows(ii).Cells(colGrpCode).Value)
                    objTr.Tax_Code = clsCommon.myCstr(gvTransferTaxLocal.Rows(ii).Cells(colTaxCode).Value)
                    objTr.TAX_Rate = clsCommon.myCdbl(gvTransferTaxLocal.Rows(ii).Cells(colTaxRate).Value)
                    objTr.Is_Default_Tax = clsCommon.myCBool(gvTransferTaxLocal.Rows(ii).Cells(colDefault).Value)
                    objTr.Is_Default_Tax_Group = IsGrpDefaultGroup(gvTransferLocal, objTr.Tax_Group_Code)
                    objTr.Is_Default_Tax_Group_GST = IsGrpDefaultGroupGST(gvTransferLocal, objTr.Tax_Group_Code)
                    objTr.Tax_Type = "T"
                    objTr.Tax_Category = "L"
                    obj.Arr.Add(objTr)
                End If
            Next
            For ii As Integer = 0 To gvTransferTaxInterState.Rows.Count - 1
                If clsCommon.myCBool(gvTransferTaxInterState.Rows(ii).Cells(colSelect).Value) Then
                    Dim objTr As New clsLocationWiseTax
                    objTr.Tax_Group_Code = clsCommon.myCstr(gvTransferTaxInterState.Rows(ii).Cells(colGrpCode).Value)
                    objTr.Tax_Code = clsCommon.myCstr(gvTransferTaxInterState.Rows(ii).Cells(colTaxCode).Value)
                    objTr.TAX_Rate = clsCommon.myCdbl(gvTransferTaxInterState.Rows(ii).Cells(colTaxRate).Value)
                    objTr.Is_Default_Tax = clsCommon.myCBool(gvTransferTaxInterState.Rows(ii).Cells(colDefault).Value)
                    objTr.Is_Default_Tax_Group = IsGrpDefaultGroup(gvTransferInterState, objTr.Tax_Group_Code)
                    objTr.Is_Default_Tax_Group_GST = IsGrpDefaultGroupGST(gvTransferInterState, objTr.Tax_Group_Code)
                    objTr.Tax_Type = "T"
                    objTr.Tax_Category = "I"
                    obj.Arr.Add(objTr)
                End If
            Next

            '---------------------------N-Level Cat-------------------------
            obj.Loc_Cat_structr_Code = clsCommon.myCstr(txtCategoryStructureCode.Value)

            If clsCommon.myLen(obj.Loc_Cat_structr_Code) > 0 Then
                obj.ArrCategoryStr = New List(Of clsLocation)
                For Each grow As GridViewRowInfo In gvCategory.Rows
                    Dim objtr As New clsLocation()

                    objtr.Loc_Category_Code = clsCommon.myCstr(grow.Cells(CatcolCode).Value)
                    objtr.Loc_Cagetory_Values = clsCommon.myCstr(grow.Cells(CatcolValue).Value)

                    If clsCommon.myLen(objtr.Loc_Category_Code) > 0 Then
                        obj.ArrCategoryStr.Add(objtr)
                    End If
                Next
            End If
            If ChkIsJobwork.Checked Then
                obj.Is_JobWork = IIf(ChkIsJobwork.Checked, 1, 0)
                obj.JobWork_Vendor = FndJobworkVendor.Value
                obj.Jobwork_Item = FndJobworkItem.Value
                obj.arr_JobworkItem = New List(Of ClsLocation_JobworkItem)
                For Each row As String In gvItem.CheckedValue
                    Dim objj As New ClsLocation_JobworkItem
                    objj.Location_Code = clsCommon.myCstr(fndLocation.Value)
                    objj.Jobwork_Item = clsCommon.myCstr(row)
                    'obj_Jowork.Add(objj)
                    obj.arr_JobworkItem.Add(objj)
                Next

            End If
            obj.Uploader_No = txtUploaderNo.Text
            If txtMpCollectionRunningDate.Checked Then
                obj.MP_Collection_Running_Date = txtMpCollectionRunningDate.Value
            Else
                obj.MP_Collection_Running_Date = Nothing
            End If

            If clsLocation.SaveData(obj, trans, False) Then
                ''For Custom Fields
                Dim arrCustomFields As New List(Of clsCustomFieldValues)
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.GetData(arrCustomFields)
                End If
                clsCustomFieldValues.SaveData(MyBase.Form_ID, fndLocation.Value, arrCustomFields, trans)
                btnSave.Text = "Update"
                btnDelete.Enabled = True
                trans.Commit()
                myMessages.insert()

                UcAttachment1.SaveData(fndLocation.Value)
            End If
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.locationMaster)
        If Not (MyBase.isReadFlag) Then
            '--------richa Ticket no. BM00000003121 15/07/2014 
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 02/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnSave.Visible = True Then
            MenuImport.Enabled = True
            MenuExport.Enabled = True
        Else
            MenuImport.Enabled = False
            MenuExport.Enabled = False
        End If
        '--------------------------------------------------
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        funReset()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If fndLocation.Value <> "" Then
            If myMessages.deleteConfirm() Then
                funDelete()
            End If
        End If
    End Sub
    Private Sub MenuClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuClose.Click
        Me.Close()
    End Sub
#End Region
    'fill Location Details
    Private Sub funFill()
        Try
            chkconsumption.Checked = False
            Dim strexcisable As Char
            Dim strDuty As Char
            Dim obj As clsLocation = New clsLocation()
            Dim arrplantdepot As New DataTable
            Dim arrlist As New ArrayList
            'Dim obj As clsLocation = New clsLocation()
            arrplantdepot = clsDBFuncationality.GetDataTable("Select tspl_location_plantdepot_detail.depot_location_code as [Code],tspl_location_master.location_desc as [Name] from tspl_location_plantdepot_detail join tspl_location_master on tspl_location_master.Location_code=tspl_location_plantdepot_detail.depot_location_code where plant_location_code='" + clsCommon.myCstr(fndLocation.Value) + "'")
            For Each row As DataRow In arrplantdepot.Rows
                arrlist.Add(row("Code").ToString())
            Next
            TxtMultiLocation.arrValueMember = arrlist
            TxtMultiLocation.arrDispalyMember = arrlist

            dr = clsDBFuncationality.GetDataTable("select  Location_Desc,TSPL_LOCATION_MASTER.Add1 ,TSPL_LOCATION_MASTER.Add2,Add3,Add4,TSPL_LOCATION_MASTER.City_Code as City_Name,State,TSPL_MCC_MAster.Pin_code ,Country ,TSPL_LOCATION_MASTER.telphone,TSPL_LOCATION_MASTER.Email ,Location_Type ,Loc_Status ,Status_date,Excisable ,Loc_Segment_Code ,Type ,Purchase_Tax_Group ,Sales_Tax_Group ,Ecc_Number ,Registration_Number ,Commissionerate ,Range_Code ,Range_Name ,Range_Address ,Division_Code ,Division_Name ,Division_Address ,TIN_No ,TAN_No ,TCAN_No ,Service_Tax_Reg_No,DutyPaid, Purchase_Tax_GroupIS, Sales_Tax_GroupIS, Stock_Transfer_Filled_Ac, Stock_Transfer_Empty_Ac,GIT_Type,GIT_location, CST_No, Phone1, Phone2,vendor_code,Location_Category,Rejected_Type,Rejected_Location,CSA_Type,Cust_Code,Category_Struct_Code,Is_Section,Is_Sub_Location,Section_Code,Main_Location_Code,CSA_Commision_Rate,CSA_Commision_Type,Commision_Acc,stock_transfer_ac,Loss_ac,CSA_Commission_RS_PERS,Is_Consumption_Location,HoAdd1,HoAdd2,NearestCity,ESIC_NO,PF_NO,is_Jobwork,Jobwork_Vendor,Jobwork_Item,DairyDispatchFromDO,tspl_location_master.Loc_Short_Name,tspl_location_master.GSTNO,tspl_location_master.GSTEntity,tspl_location_master.GSTBlank,tspl_location_master.GSTDegit,tspl_location_master.Registered,isnull(UseInJobWork,0) as UseInJobWork,isnull(TSPL_LOCATION_MASTER.Silo_Capacity,0) as Silo_Capacity,isnull(TSPL_LOCATION_MASTER.Is_Insurance,0) as Is_Insurance,isnull(TSPL_LOCATION_MASTER.InsuranceNo,'') as InsuranceNo,TSPL_LOCATION_MASTER.InsuranceFromDate,TSPL_LOCATION_MASTER.InsuranceToDate,IsParlour,IsSubLocationWise,TSPL_LOCATION_MASTER.accountholdername, TSPL_LOCATION_MASTER.bankaccno, TSPL_LOCATION_MASTER.bankifsccode,TSPL_LOCATION_MASTER.BankUPI_ID,isnull(TSPL_LOCATION_MASTER.IsMainPlant,0) as IsMainPlant,TSPL_LOCATION_MASTER.MP_Collection_Running_Date,TSPL_LOCATION_MASTER.Uploader_No,TSPL_LOCATION_MASTER.Bank,TSPL_LOCATION_MASTER.Branch,TSPL_LOCATION_MASTER.ACType,No_Of_Shift,TSPL_LOCATION_MASTER.PAN_NO  from TSPL_LOCATION_MASTER left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_LOCATION_MASTER.Location_Code where TSPL_LOCATION_MASTER.Location_Code='" + fndLocation.Value + "'")
            'obj=clsLocation.GetData()
            For Each row As DataRow In dr.Rows
                isInsideLoadData = True
                txtLocationDesc.Text = row(0).ToString()
                txtLocShortName.Text = clsCommon.myCstr(row("Loc_Short_Name"))
                '===============Added by preeti
                Dim GSTState As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select tspl_state_master.GST_STATE_Code  from tspl_state_master where STATE_CODE ='" + row(6).ToString() + "'"))
                If clsCommon.myLen(GSTState) > 0 Then
                    txtGstState.Text = GSTState
                End If
                txtPANNo.Text = clsCommon.myCstr(row("PAN_NO"))
                If clsCommon.myLen(txtPANNo.Text) <= 0 Then
                    Dim CompanyPan As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select tspl_company_master.Pan_No from tspl_company_master where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"))
                    txtGSTPANNO.Text = CompanyPan
                Else
                    If clsCommon.myLen(txtPANNo.Text) > 0 Then
                        txtGSTPANNO.Text = txtPANNo.Text
                    End If
                End If
                txtGSTEntityNo.Text = clsCommon.myCstr(row("GSTEntity"))
                If clsCommon.myLen(txtGSTEntityNo.Text) > 0 Then
                    txtGSTBlank.Text = clsCommon.myCstr(row("GSTBlank"))
                Else
                    txtGSTBlank.Text = "Z"

                End If

                txtGSTDegit.Text = clsCommon.myCstr(row("GSTDegit"))
                txtGstNo.Text = clsCommon.myCstr(row("GSTNO"))
                chkregistered.Checked = IIf(clsCommon.myCstr(row("Registered")) = "1", True, False)
                '================================================
                txtAdd1.Text = row(1).ToString()
                txtAdd2.Text = row(2).ToString()
                txtAdd3.Text = row(3).ToString()
                txtAdd4.Text = row(4).ToString()
                txtCity.Text = row(5).ToString()
                TxtHOAdd1.Text = row("HOAdd1").ToString()
                TxtHoAdd2.Text = row("HOAdd2").ToString()
                txtNearestCity.Text = row("NearestCity").ToString()
                TxtESICNo.Text = clsCommon.myCstr(row("ESIC_NO"))
                TxtPFNo.Text = clsCommon.myCstr(row("PF_No"))
                chkthirdparty.Checked = False
                txtvndrcode.Value = row("vendor_code").ToString()
                If clsCommon.myLen(txtvndrcode.Value) > 0 Then
                    chkthirdparty.Checked = True
                    txtvndrname.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select vendor_name from TSPL_VENDOR_MASTER where vendor_code='" + txtvndrcode.Value + "'"))
                End If

                fndstateprovince.Value = row(6).ToString()
                If clsCommon.myCdbl(row(7).ToString()) <> 0 Then
                    txtZipPostalCode.Text = row(7).ToString()
                Else
                    txtZipPostalCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Pin_Code from tspl_location_master where location_code='" & fndLocation.Value & "'"))
                End If

                txtCountry.Text = row(8).ToString()
                txtTelephone.Text = row(9).ToString()
                txtPhone1.Text = clsCommon.myCstr(row("Phone1"))
                txtPhone2.Text = clsCommon.myCstr(row("Phone2"))
                cmbloc_cate.SelectedValue = clsCommon.myCstr(row("Location_Category"))
                cmbNoOfShift.SelectedValue = IIf(clsCommon.myCDecimal(row("No_Of_Shift")) > 0, clsCommon.myCstr(row("No_Of_Shift")), "Select")
                txtcsa_commision.Text = clsCommon.myCdbl(row("CSA_Commision_Rate"))
                txtcsa_comm_type.Text = clsCommon.myCstr(row("CSA_Commision_Type"))
                cmbComm_Type.SelectedValue = clsCommon.myCstr(row("CSA_Commission_RS_PERS"))
                txtcommsn_code.Value = clsCommon.myCstr(row("Commision_Acc"))
                txtcommsn_desc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from TSPL_GL_ACCOUNTS where account_code='" + txtcommsn_code.Value + "'"))

                fndStkTrnsfrAc.Value = clsCommon.myCstr(row("stock_transfer_ac"))
                fndLossAc.Value = clsCommon.myCstr(row("Loss_Ac"))

                'Ticket- BHA/27/07/18-000198
                txt_capacity.Text = clsCommon.myCdbl(row("Silo_Capacity"))
                'Ticket- BHA/27/07/18-000198
                chkIsMainPlant.Checked = IIf(clsCommon.myCstr(row("IsMainPlant")) = "1", True, False)
                isEdit = True
                chk_HO.Checked = False
                If clsCommon.CompairString(cmbloc_cate.SelectedValue, "HO") = CompairStringResult.Equal Then
                    chk_HO.Checked = True
                End If
                isEdit = False

                chk_HO.Enabled = True
                If clsCommon.CompairString(cmbloc_cate.SelectedValue, "MCC") = CompairStringResult.Equal Then
                    chk_HO.Enabled = False
                End If

                txtEmail.Text = row(10).ToString()

                ddlLocationType.Text = row(11).ToString()
                Dim obo As String
                obo = row(12).ToString()
                ' Dim Y As String
                If obo = "Y" Then
                    chkInactive.Checked = True
                    txtInactiveDate.Visible = True
                    txtInactiveDate.Text = row(13)
                Else
                    chkInactive.Checked = False
                    txtInactiveDate.Visible = False

                End If
                If rbtnDispatchfromDO.IsChecked = True Then
                    strDispatchRef = "D"
                Else
                    strDispatchRef = "G"
                End If
                chkconsumption.Checked = IIf(clsCommon.myCstr(row("Is_Consumption_Location")) = "1", True, False)
                chkUseInJobWork.Checked = IIf(clsCommon.myCstr(row("UseInJobWork")) = "1", True, False)
                btnSave.Enabled = True
                btnSave.Text = "Update"
                btnDelete.Enabled = True
                txtLocationDesc.Enabled = True
                ddlLocationType.Enabled = True
                chkInactive.Enabled = True
                strexcisable = row(14).ToString()
                If strexcisable = "T" Then
                    chkExcisable.Checked = True
                Else
                    chkExcisable.Checked = False
                End If
                fndLocationSegmentCode.Value = row(15).ToString()
                ddlType.Text = row(16).ToString()
                If clsCommon.CompairString(ddlType.Text, "Plant") = CompairStringResult.Equal Then
                    TxtMultiLocation.Visible = True
                    MyLabel17.Visible = True
                Else
                    TxtMultiLocation.Visible = False
                    MyLabel17.Visible = False
                End If
                'fndPurchaseTaxGroup.Value = row(17).ToString()
                'fndSalesTaxGroup.Value = row(18).ToString()
                txtEccNumber.Text = row(19).ToString()
                txtRegistration.Text = row(20).ToString()
                txtCommissionerate.Text = row(21).ToString()
                txtRangeCode.Text = row(22).ToString()
                txtRangeName.Text = row(23).ToString()
                txtRangeAddress.Text = row(24).ToString()
                txtDivisionCode.Text = row(25).ToString()
                txtDivisionName.Text = row(26).ToString()
                txtDivisionAddress.Text = row(27).ToString()
                txtTinNo.Text = row(28).ToString()
                txtTanNo.Text = row(29).ToString()
                txtTcanNo.Text = row(30).ToString()
                txtCSTNo.Text = clsCommon.myCstr(row("CST_No"))
                txtServiceTaxRegN.Text = row(31).ToString()
                strDuty = row(32).ToString()
                'fndPurchaseTaxGroupIS.Value = row(33).ToString
                'fndSaleTaxGroupIS.Value = row(34).ToString
                fndStkTrnsfrFilledAc.Value = row(35).ToString
                fndStkTrnsfrEmptyAc.Value = row(36).ToString
                fndGITLocation.Value = row("GIT_location").ToString()

                txtBankaccHolderName.Text = clsCommon.myCstr(row("accountholdername"))
                txtBankAccNo.Text = clsCommon.myCstr(row("bankaccno"))
                txtBankIFSCCode.Text = clsCommon.myCstr(row("bankifsccode"))
                txtBankUPI_Id.Text = clsCommon.myCstr(row("BankUPI_ID"))

                txtBank.Text = clsCommon.myCstr(row("Bank"))
                txtBranch.Text = clsCommon.myCstr(row("Branch"))
                txtACType.Text = clsCommon.myCstr(row("ACType"))

                If row("GIT_Type").ToString() = "Y" Then
                    chkGITType.Checked = True
                Else
                    chkGITType.Checked = False
                End If
                If strDuty = "Y" Then
                    chkduty.Checked = True
                Else
                    chkduty.Checked = False
                End If
                rbtnDispatchfromDO.IsChecked = IIf(row("DairyDispatchFromDO").ToString = "1", True, False)
                rbtnDispatchFromGAtepass.IsChecked = IIf(row("DairyDispatchFromDO").ToString = "0", True, False)
                '' Anubhooti 31-July-2014 BM00000003350
                If row("Is_Section").ToString() = "Y" Then
                    rdbbtnSection.IsChecked = True
                Else
                    rdbbtnSection.IsChecked = False
                End If
                If row("Is_Sub_Location").ToString() = "Y" Then
                    rdbbtnSubLoc.IsChecked = True
                Else
                    rdbbtnSubLoc.IsChecked = False
                End If
                If row("IsParlour").ToString() = "Y" Then
                    chkParlour.Checked = True
                Else
                    chkParlour.Checked = False
                End If
                If row("IsSubLocationWise").ToString() = "Y" Then
                    chkSubLocationWise.IsChecked = True
                Else
                    chkSubLocationWise.IsChecked = False
                End If
                If row("Is_Section").ToString() = "Y" AndAlso row("Is_Sub_Location").ToString() = "N" Then
                    TxtSection.Enabled = True
                    TxtMainLoc.Enabled = True
                ElseIf row("Is_Section").ToString() = "N" AndAlso row("Is_Sub_Location").ToString() = "Y" Then
                    TxtSection.Enabled = False
                    TxtMainLoc.Enabled = True
                ElseIf (row("Is_Section").ToString().ToUpper().Trim() = "N".ToUpper().Trim() Or row("Is_Section").ToString().Trim() = "".Trim()) AndAlso (row("Is_Sub_Location").ToString().ToUpper().Trim() = "N".ToUpper().Trim() Or row("Is_Sub_Location").ToString().Trim() = "".Trim()) Then
                    TxtSection.Enabled = False
                    TxtMainLoc.Enabled = False
                End If
                TxtSection.Value = row("Section_Code").ToString()
                TxtMainLoc.Value = row("Main_Location_Code").ToString()
                ''
                chkRejected.Checked = IIf(row("Rejected_Type").ToString = "Y", True, False)
                chkCSA.Checked = IIf(row("CSA_Type").ToString = "Y", True, False)
                cmbComm_Type.SelectedValue = clsCommon.myCstr(row("CSA_Commission_RS_PERS"))
                ChkIsJobwork.Checked = IIf(row("Is_Jobwork").ToString = "1", True, False)
                FndJobworkVendor.Value = clsCommon.myCstr(row("Jobwork_Vendor").ToString)
                FndJobworkItem.Value = clsCommon.myCstr(row("Jobwork_Item").ToString)
                TxtJobworkItem.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Desc From tspl_Item_Master where item_Code='" & FndJobworkItem.Value & "'"))
                TxtJObworkVendor.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Name From tspl_Vendor_Master where Vendor_Code='" & FndJobworkVendor.Value & "'"))

                If row("MP_Collection_Running_Date") IsNot DBNull.Value Then
                    txtMpCollectionRunningDate.Value = clsCommon.myCDate(row("MP_Collection_Running_Date"))
                    txtMpCollectionRunningDate.Checked = True
                Else
                    txtMpCollectionRunningDate.Checked = False
                End If
                txtUploaderNo.Text = clsCommon.myCstr(row("Uploader_No"))


                If ChkIsJobwork.Checked Then
                    Dim qry As String = "select * from TSPL_LOCATION_MASTER_Jobwork_Item where TSPL_LOCATION_MASTER_Jobwork_Item.Main_location_code='" + fndLocation.Value + "'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    Dim arr_Jobwork As New ArrayList
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        For Each dr As DataRow In dt.Rows
                            arr_Jobwork.Add(clsCommon.myCstr(dr("Jobwork_Item")))
                        Next
                    End If
                    gvItem.CheckedValue = arr_Jobwork
                End If
                If chkCSA.Checked Then
                    fndCustomer.MendatroryField = True
                    MyLabel8.Visible = True
                    txtcsa_comm_type.Visible = True
                    txtcsa_commision.Visible = True
                    cmbComm_Type.Visible = True
                    RadGroupBox1.Visible = True
                    RadGroupBox1.Text = "CSA Vendor Detail"
                    chkthirdparty.Visible = False
                    txtcommsn_code.Enabled = True
                    txtcommsn_code.Visible = True
                    txtcommsn_desc.Visible = True
                    MyLabel10.Visible = True

                    If clsCommon.CompairString(MDI.IsLoc_Third_Party, "YES") <> CompairStringResult.Equal Then
                        chkthirdparty.Checked = False
                    End If
                    txtvndrcode.Value = row("vendor_code").ToString()
                    txtvndrname.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select vendor_name from TSPL_VENDOR_MASTER where vendor_code='" + txtvndrcode.Value + "'"))
                    txtvndrcode.Enabled = True
                    txtvndrname.Enabled = True
                ElseIf Not chkCSA.Checked Then
                    fndCustomer.MendatroryField = False
                    MyLabel8.Visible = False
                    txtcsa_comm_type.Visible = False
                    cmbComm_Type.Visible = False
                    txtcsa_commision.Visible = False
                    RadGroupBox1.Visible = False
                    chkthirdparty.Visible = True
                    txtvndrcode.Enabled = False
                    txtvndrname.Enabled = False
                    txtcommsn_code.Enabled = False
                    txtcommsn_code.Visible = False
                    txtcommsn_desc.Visible = False
                    MyLabel10.Visible = False
                End If

                fndRejectedLoc.Value = row("Rejected_Location").ToString()
                fndCustomer.Value = row("Cust_Code").ToString()
                'txtstateprovince.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select state_name from TSPL_TDS_STATE_MASTER  where state_code='" + fndstateprovince.Value + "'"))
                '' Anubhooti 13-Jan-2014 (Old fetching of state desp was from TDS_State But according to code state coming from state master then it should be from state master)
                If clsCommon.myLen(fndstateprovince.Value) > 0 Then
                    txtstateprovince.Text = clsDBFuncationality.getSingleValue("select ISNULL(STATE_NAME,'') STATE_NAME from TSPL_STATE_MASTER  where STATE_CODE='" + fndstateprovince.Value + "'")
                Else
                    txtstateprovince.Text = ""
                End If


                txtCategoryStructureCode.Value = clsCommon.myCstr(row("Category_Struct_Code"))
                lblCategoryStructureCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from TSPL_ITEM_CATEGORY_STRUCTURE where item_category_struct_code='" + txtCategoryStructureCode.Value + "' and isnull(form_type,'item')='location'"))

                chkInsurance.Checked = IIf(row("Is_Insurance").ToString = "1", True, False)
                If chkInsurance.Checked = True Then
                    txtInsurance.Text = clsCommon.myCstr(row("InsuranceNo"))
                    txtFromDate.Value = clsCommon.myCDate(row("InsuranceFromDate"))
                    txtToDate.Value = clsCommon.myCDate(row("InsuranceToDate"))
                Else
                    txtInsurance.Text = ""
                    txtFromDate.Value = clsCommon.GETSERVERDATE
                    txtToDate.Value = clsCommon.GETSERVERDATE
                End If

            Next

            'OpenPurchaseTaxRateList(fndLocation.Value)
            'OpenSaleTaxRateList(fndLocation.Value)

            LoadBlankGridCat()
            If clsCommon.myLen(txtCategoryStructureCode.Value) > 0 Then
                Dim qry As String = "select TSPL_LOCATION_CATEGORY_MASTER.*,TSPL_ITEM_CATEGORY_LEVEL.description as cat_desc,TSPL_ITEM_CATEGORY_LEVEL_VALUES.description from TSPL_LOCATION_CATEGORY_MASTER left outer join TSPL_ITEM_CATEGORY_LEVEL on TSPL_ITEM_CATEGORY_LEVEL.item_category_code=TSPL_LOCATION_CATEGORY_MASTER.Category_Code and isnull(TSPL_ITEM_CATEGORY_LEVEL.form_type,'Item')='location' left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on isnull(TSPL_ITEM_CATEGORY_LEVEL_VALUES.form_type,'Item')='location' and TSPL_ITEM_CATEGORY_LEVEL_VALUES.form_type=TSPL_ITEM_CATEGORY_LEVEL.form_type and TSPL_ITEM_CATEGORY_LEVEL.item_category_code=TSPL_ITEM_CATEGORY_LEVEL_VALUES.item_category_code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.code=TSPL_LOCATION_CATEGORY_MASTER.Category_Code_Values and TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_LOCATION_CATEGORY_MASTER.Category_Code where TSPL_LOCATION_CATEGORY_MASTER.location_code='" + fndLocation.Value + "' and TSPL_LOCATION_CATEGORY_MASTER.Category_Struct_Code='" + txtCategoryStructureCode.Value + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

                gvCategory.Rows.Clear()

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        gvCategory.Rows.AddNew()
                        gvCategory.Rows(gvCategory.Rows.Count - 1).Cells(CatcolCode).Value = clsCommon.myCstr(dr("Category_Code"))
                        gvCategory.Rows(gvCategory.Rows.Count - 1).Cells(CatcolCodeDesc).Value = clsCommon.myCstr(dr("cat_desc"))
                        gvCategory.Rows(gvCategory.Rows.Count - 1).Cells(CatcolValue).Value = clsCommon.myCstr(dr("Category_Code_Values"))
                        gvCategory.Rows(gvCategory.Rows.Count - 1).Cells(CatcolValueDesc).Value = clsCommon.myCstr(dr("description"))
                    Next
                End If
            End If
            ''For Custom Fields
            If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                UcCustomFields1.LoadData(fndLocation.Value)
            End If

            UcAttachment1.LoadData(fndLocation.Value)



            LoadBlankGrid(gvPurchaseTaxInterState)
            LoadBlankGrid(gvPurchaseTaxLocal)
            LoadBlankGrid(gvSaleTaxInterState)
            LoadBlankGrid(gvSaleTaxLocal)

            '' new tab transfer tax
            LoadBlankGrid(gvTransferTaxInterState)
            LoadBlankGrid(gvTransferTaxLocal)
            ''end new tab transfer tax

            ''RICHA 
            LoadItemLocal()
            LoadItemInter()
            ''----------

            LoadTaxGrpHeader(gvPurchaseLocal, "P", "'IGST'")
            LoadTaxGrpHeader(gvPurchaseInterState, "P", "'SGST','CGST','UGST'")
            LoadTaxGrpHeader(gvSaleLocal, "S", "'IGST'")
            LoadTaxGrpHeader(gvSaleInterState, "S", "'SGST','CGST','UGST'")

            '' new tab transfer tax
            LoadTaxGrpHeader(gvTransferLocal, "T", "'IGST'")
            LoadTaxGrpHeader(gvTransferInterState, "T", "'SGST','CGST','UGST'")

            If clsCommon.myLen(fndLocation.Value) > 0 Then
                LoadDataInGrid(gvPurchaseLocal, gvPurchaseTaxLocal, "P", "L")
                LoadDataInGrid(gvPurchaseInterState, gvPurchaseTaxInterState, "P", "I")
                LoadDataInGrid(gvSaleLocal, gvSaleTaxLocal, "S", "L")
                LoadDataInGrid(gvSaleInterState, gvSaleTaxInterState, "S", "I")

                '' new tab transfer tax
                LoadDataInGrid(gvTransferLocal, gvTransferTaxLocal, "T", "L")
                LoadDataInGrid(gvTransferInterState, gvTransferTaxInterState, "T", "I")
            End If
            If clsCommon.CompairString(ddlLocationType.Text, "Virtual") = CompairStringResult.Equal Then
                chkUseInJobWork.Visible = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub LoadDataInGrid(ByVal gvHeader As RadGridView, ByVal gvDetail As RadGridView, ByVal strTaxType As String, ByVal strCatgType As String)
        Dim dt As DataTable = clsLocationWiseTax.GetTaxGroup(fndLocation.Value, strTaxType, strCatgType)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                Dim strGrpCode As String = clsCommon.myCstr(dr("Tax_Group_Code"))
                For ii As Integer = 0 To gvHeader.Rows.Count - 1
                    If clsCommon.CompairString(strGrpCode, clsCommon.myCstr(gvHeader.Rows(ii).Cells("Tax_Group_Code").Value)) = CompairStringResult.Equal Then
                        gvHeader.CurrentRow = gvHeader.Rows(ii)
                        gvHeader.CurrentColumn = gvHeader.Columns("Sel")
                        gvHeader.Rows(ii).Cells("Sel").Value = True
                        gvHeader.CurrentColumn = gvHeader.Columns("Is_Default")
                        gvHeader.Rows(ii).Cells("Is_Default").Value = clsCommon.myCBool(dr("Is_Default_Tax_Group"))

                        gvHeader.CurrentColumn = gvHeader.Columns("Is_Default_GST")
                        gvHeader.Rows(ii).Cells("Is_Default_GST").Value = clsCommon.myCBool(dr("Is_Default_Tax_Group_GST"))

                        Dim dtTaxRate As DataTable = clsLocationWiseTax.GetTaxWithRate(fndLocation.Value, strGrpCode, strTaxType, strCatgType)
                        If dtTaxRate IsNot Nothing AndAlso dtTaxRate.Rows.Count > 0 Then
                            For Each drRate As DataRow In dtTaxRate.Rows
                                For jj As Integer = 0 To gvDetail.Rows.Count - 1
                                    If clsCommon.CompairString(strGrpCode, clsCommon.myCstr(gvDetail.Rows(jj).Cells(colGrpCode).Value)) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(drRate("Tax_Code")), clsCommon.myCstr(gvDetail.Rows(jj).Cells(colTaxCode).Value)) = CompairStringResult.Equal AndAlso clsCommon.myCdbl(drRate("TAX_Rate")) = clsCommon.myCdbl(gvDetail.Rows(jj).Cells(colTaxRate).Value) Then
                                        gvDetail.Rows(jj).Cells(colSelect).Value = True
                                        gvDetail.Rows(jj).Cells(colDefault).Value = IIf(clsCommon.myCdbl(drRate("Is_Default_Tax")), 1, 0)
                                        Exit For
                                    End If
                                Next
                            Next
                        End If
                        Exit For
                    End If
                Next
            Next
        End If
    End Sub
    'reset All Locatin Details
    Private Sub funReset()
        txt_capacity.Text = "0"
        'gvSaleTax.Rows.AddNew()
        'gvPurchaseTax.Rows.AddNew()
        chkUseInJobWork.Checked = False
        chkUseInJobWork.Visible = False
        chkParlour.Checked = False
        txtLocShortName.Text = ""
        rbtnDispatchfromDO.IsChecked = True
        rbtnDispatchFromGAtepass.IsChecked = False
        txtcommsn_code.Value = ""
        txtcommsn_desc.Text = ""
        txtCategoryStructureCode.Value = ""
        lblCategoryStructureCode.Text = ""
        LoadBlankGridCat()
        'LoadBlankPurchaseGridTax()
        'LoadBlankSaleGridTax()
        chk_HO.Checked = False
        chk_HO.Enabled = True
        cmbloc_cate.SelectedValue = ""
        txtvndrcode.Value = ""
        txtvndrname.Text = ""
        chkthirdparty.Checked = False
        fndLocation.Value = ""
        fndLocation.MyReadOnly = False
        txtLocationDesc.Text = ""
        txtAdd1.Text = ""
        txtAdd2.Text = ""
        txtAdd3.Text = ""
        txtAdd4.Text = ""
        txtCity.Text = ""
        TxtHOAdd1.Text = ""
        TxtHoAdd2.Text = ""
        txtcsa_commision.Text = 0
        txtcsa_comm_type.Text = ""
        cmbComm_Type.SelectedValue = ""
        txtcsa_comm_type.Enabled = True
        txtNearestCity.Text = ""
        TxtESICNo.Text = ""
        TxtPFNo.Text = ""
        fndstateprovince.Value = ""
        txtstateprovince.Text = ""
        txtZipPostalCode.Text = ""
        txtCountry.Text = ""
        txtTelephone.Text = "(+__)__________"
        txtEmail.Text = ""
        ddlLocationType.Text = "Physical"
        fndLocationSegmentCode.Value = ""
        ddlType.Text = "Depot"
        'fndPurchaseTaxGroup.Value = ""
        'fndSalesTaxGroup.Value = ""
        'fndPurchaseTaxGroupIS.Value = ""
        'fndSaleTaxGroupIS.Value = ""
        txtEccNumber.Text = ""
        txtRegistration.Text = ""
        txtCommissionerate.Text = ""
        txtRangeCode.Text = ""
        txtRangeName.Text = ""
        txtRangeAddress.Text = ""
        txtDivisionCode.Text = ""
        txtDivisionName.Text = ""
        txtDivisionAddress.Text = ""
        txtTinNo.Text = ""
        txtTanNo.Text = ""
        txtTcanNo.Text = ""
        txtServiceTaxRegN.Text = ""
        btnSave.Enabled = True
        btnDelete.Enabled = False
        btnSave.Text = "Save"
        ddlLocationType.Enabled = True
        chkInactive.Checked = False
        chkExcisable.Checked = False
        chkduty.Checked = False
        txtLocationDesc.Enabled = True
        txtInactiveDate.Visible = False
        fndStkTrnsfrFilledAc.Value = ""
        fndStkTrnsfrEmptyAc.Value = ""
        fndGITLocation.Value = ""
        txtPANNo.Text = ""
        txtCSTNo.Text = ""
        txtPhone1.Text = "(+__)__________"
        txtPhone2.Text = "(+__)__________"
        TxtSection.Value = ""
        TxtMainLoc.Value = ""
        rdbbtnSection.IsChecked = False
        rdbbtnSubLoc.IsChecked = False
        chkSubLocationWise.IsChecked = False
        TxtMainLoc.Enabled = False
        TxtSection.Enabled = False

        fndCustomer.MendatroryField = False
        MyLabel8.Visible = False
        txtcsa_comm_type.Visible = False
        txtcsa_commision.Visible = False
        cmbComm_Type.Visible = False
        RadGroupBox1.Visible = False
        chkthirdparty.Visible = True


        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.BlankAllControls()
        End If
        chkGITType.Checked = False
        fndLocation.Focus()
        chkCSA.Checked = False

        chkRejected.Checked = False
        fndRejectedLoc.Value = ""
        fndCustomer.Value = ""
        fndStkTrnsfrAc.Value = ""
        fndLossAc.Value = ""

        ''richa agarwal against ticket no BM00000004527 13/11/2014
        fndRejectedLoc.Enabled = True
        '=======Rohit  Jul 27,2015
        ChkIsJobwork.Checked = False
        FndJobworkVendor.Value = Nothing
        FndJobworkItem.Value = Nothing

        TxtJObworkVendor.Text = Nothing
        ''-----------------------------------------------
        txtstateprovince.ReadOnly = True

        UcAttachment1.Form_ID = Me.Form_ID
        UcAttachment1.BlankAllControls()

        LoadBlankGrid(gvPurchaseTaxInterState)
        LoadBlankGrid(gvPurchaseTaxLocal)
        LoadBlankGrid(gvSaleTaxInterState)
        LoadBlankGrid(gvSaleTaxLocal)
        '' new tab transfer tax
        LoadBlankGrid(gvTransferTaxInterState)
        LoadBlankGrid(gvTransferTaxLocal)
        ''end new tab transfer tax

        LoadTaxGrpHeader(gvPurchaseLocal, "P", "'IGST'")
        LoadTaxGrpHeader(gvPurchaseInterState, "P", "'SGST','CGST','UGST'")
        LoadTaxGrpHeader(gvSaleLocal, "S", "'IGST'")
        LoadTaxGrpHeader(gvSaleInterState, "S", "'SGST','CGST','UGST'")
        '' new tab transfer tax
        LoadTaxGrpHeader(gvTransferLocal, "T", "'IGST'")
        LoadTaxGrpHeader(gvTransferInterState, "T", "'SGST','CGST','UGST'")

        chkconsumption.Checked = False
        LoadItem()
        LoadItemLocal()
        LoadItemInter()
        If clsCommon.CompairString(ddlType.Text, "Plant") = CompairStringResult.Equal Then
            TxtMultiLocation.Visible = True
            MyLabel17.Visible = True
        Else
            TxtMultiLocation.Visible = False
            MyLabel17.Visible = False
        End If
        If objCommonVar.GSTApplicable Then
            GBGST.Enabled = True
            txtGstState.Text = ""
            txtGSTPANNO.Text = ""
            txtGSTEntityNo.Text = ""
            txtGSTBlank.Text = "Z"
            txtGSTDegit.Text = ""

        Else
            GBGST.Enabled = False
        End If
        chkregistered.Checked = False
        txtFromDate.Value = clsCommon.GETSERVERDATE
        txtToDate.Value = txtFromDate.Value
        txtInsurance.Text = ""
        chkInsurance.Checked = False
        RadGroupBoxInsurance.Enabled = False
        txtBankaccHolderName.Text = ""
        txtBankAccNo.Text = ""
        txtBankIFSCCode.Text = ""
        txtBankUPI_Id.Text = ""
        chkIsMainPlant.Checked = False
        txtBank.Text = ""
        txtBranch.Text = ""
        txtACType.Text = ""
        txtUploaderNo.Text = ""
        txtMpCollectionRunningDate.Checked = False
        txtMpCollectionRunningDate.Value = txtFromDate.Value
    End Sub
    'delete Location Details
    Private Sub funDelete()
        Try
            Dim qst As String
            Dim dpt As String
            qst = "select From_Location,To_Location from tspl_transfer_head where (From_Location  ='" + fndLocation.Value + "' or To_Location ='" + fndLocation.Value + "')"
            dpt = clsDBFuncationality.getSingleValue(qst)
            If clsCommon.myLen(dpt) > 0 AndAlso dpt IsNot Nothing Then
                common.clsCommon.MyMessageBoxShow(Me, "This Record Cannot be deleted." + Environment.NewLine + "This is already in Process")
                Return
            End If
            qst = "select Bill_To_Location from TSPL_SRN_HEAD where Bill_To_Location ='" + fndLocation.Value + "'"
            dpt = clsDBFuncationality.getSingleValue(qst)
            If clsCommon.myLen(dpt) > 0 AndAlso dpt IsNot Nothing Then
                common.clsCommon.MyMessageBoxShow(Me, "This Record Cannot be deleted." + Environment.NewLine + "This is already in Process")
                Return
            End If
            qst = "select Location from TSPL_PR_DETAIL where Location ='" + fndLocation.Value + "'"
            dpt = clsDBFuncationality.getSingleValue(qst)
            If clsCommon.myLen(dpt) > 0 AndAlso dpt IsNot Nothing Then
                common.clsCommon.MyMessageBoxShow(Me, "This Record Cannot be deleted." + Environment.NewLine + "This is already in Process")
                Return
            End If
            qst = "select Loc_Code from TSPL_SCRAPINVOICE_HEAD where Loc_Code ='" + fndLocation.Value + "'"
            dpt = clsDBFuncationality.getSingleValue(qst)
            If clsCommon.myLen(dpt) > 0 AndAlso dpt IsNot Nothing Then
                common.clsCommon.MyMessageBoxShow(Me, "This Record Cannot be deleted." + Environment.NewLine + "This is already in Process")
                Return
            End If
            qst = "select Location_Code from TSPL_ADJUSTMENT_DETAIL where Location_Code ='" + fndLocation.Value + "'"
            dpt = clsDBFuncationality.getSingleValue(qst)
            If clsCommon.myLen(dpt) > 0 AndAlso dpt IsNot Nothing Then
                common.clsCommon.MyMessageBoxShow("This Record Cannot be deleted." + Environment.NewLine + "This is already in Process")
                Return
            End If
            connectSql.RunSp("sp_TSPL_LOCATION_MASTER_delete", New SqlParameter("@LocCode", fndLocation.Value))

            Dim qry As String = "delete from TSPL_LOCATION_CATEGORY_MASTER where location_code='" + fndLocation.Value + "' and Category_Struct_Code='" + txtCategoryStructureCode.Value + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)

            myMessages.delete()
            btnSave.Enabled = True
            btnSave.Text = "Save"
            btnDelete.Enabled = False
            funReset()
        Catch ex As Exception
            'MessageBox.Show(ex.Message.ToString())
            '' Anubhooti 06-Oct-2014 BM00000004189 
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
#Region "KeyPress Event"

    Private Sub txtZipPostalCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If IsNumeric(e.KeyChar) = True Then
            e.Handled = False
        ElseIf Microsoft.VisualBasic.Asc(e.KeyChar) = 8 Then
        Else
            e.Handled = True
        End If
    End Sub
    Private Sub Location_keypress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub fndPurchaseTaxGroupIS_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub fndSaleTaxGroupIS_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

#End Region
#Region "CheckBox Click Event"
    Private Sub chkInactive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkInactive.Click
        If chkInactive.Checked = True Then
            txtInactiveDate.Visible = False
            txtInactiveDate.Text = ""
        Else
            Dim dt As String
            dt = Format(Date.Today, "dd/MM/yyyy")
            txtInactiveDate.Visible = True
            txtInactiveDate.Text = dt
        End If
    End Sub
#End Region
#Region "Finder MouseHover Event"
    Private Sub fndLocation_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim toolTip1 As New ToolTip()
        toolTip1.ShowAlways = True
        toolTip1.SetToolTip(fndLocation, "Finder")
    End Sub
#End Region

    Private Sub frmLocationMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Control And e.KeyCode.ToString() = "S" Then
            If fndLocation.Value <> "" Then
                If allowToSave() Then SaveData()
            Else
                myMessages.blankValue("Location Code")
                fndLocation.Focus()
            End If
        ElseIf e.Control And e.KeyCode.ToString() = "U" Then
            If fndLocation.Value <> "" Then
                If allowToSave() Then SaveData()
            Else
                myMessages.blankValue("Location Code")
                fndLocation.Focus()
            End If
        ElseIf e.Control And e.KeyCode.ToString() = "D" Then
            If fndLocation.Value <> "" Then
                If myMessages.deleteConfirm() Then
                    funDelete()
                End If
            Else
                myMessages.blankValue("Location Code")
                fndLocation.Focus()
            End If
        End If
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso MyBase.isModifyFlag Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isPostFlag AndAlso btnSave.Enabled Then
            If allowToSave() Then SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub
    Private Sub fndLocationSegmentCode_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If fndLocationSegmentCode.Value = "" Then
        Else
            Dim strvalue As String
            Dim str As String = "select Segment_code from TSPL_GL_SEGMENT_CODE where Segment_code='" + fndLocationSegmentCode.Value + "'"
            strvalue = clsDBFuncationality.getSingleValue(str)
            If strvalue <> "" Then
            Else : str = ""
                common.clsCommon.MyMessageBoxShow(Me, "Segment code does not exist in Master Table")
                fndLocationSegmentCode.Value = ""
                fndLocationSegmentCode.Focus()
            End If
        End If
    End Sub
    Private Sub fndLocationSegmentCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    'Private Sub fndPurchaseTaxGroupIS__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean)
    '    'Dim qry As String = "SELECT TAX_Group_Code as Code ,Tax_Group_Desc as Description From TSPL_TAX_GROUP_MASTER  "
    '    'fndPurchaseTaxGroupIS.Value = clsCommon.ShowSelectForm("LocationMasterPG", qry, "Code", "Tax_Group_Type = 'P'", fndPurchaseTaxGroupIS.Value, "Code", isButtonClicked)
    '    fndPurchaseTaxGroupIS.Value = clsTaxGroupMaster.getFinder("Tax_Group_Type = 'P'", fndPurchaseTaxGroupIS.Value, isButtonClicked)
    'End Sub
    'Private Sub fndPurchaseTaxGroup_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If fndPurchaseTaxGroup.Value = "" Then
    '    Else
    '        Dim strvalue As String
    '        Dim strQuery As String = "select TAX_Group_Code from TSPL_TAX_GROUP_MASTER where TAX_Group_Code ='" + fndPurchaseTaxGroup.Value + "'"
    '        strvalue = clsDBFuncationality.getSingleValue(strQuery)
    '        If strvalue <> "" Then
    '        Else
    '            strQuery = ""
    '            common.clsCommon.MyMessageBoxShow("Purchase tax group does not exist in master table")
    '            fndPurchaseTaxGroup.Value = ""
    '        End If
    '    End If
    'End Sub
    Private Sub fndPurchaseTaxGroup_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub
    'Private Sub fndSaleTaxGroupIS__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean)
    '    'Dim qry As String = "SELECT TAX_Group_Code as Code ,Tax_Group_Desc as Description From TSPL_TAX_GROUP_MASTER  "
    '    'fndSaleTaxGroupIS.Value = clsCommon.ShowSelectForm("SalTaxGp", qry, "Code", "Tax_Group_Type = 'S'", fndSaleTaxGroupIS.Value, "Code", isButtonClicked)
    '    fndSaleTaxGroupIS.Value = clsTaxGroupMaster.getFinder("Tax_Group_Type = 'S'", fndSaleTaxGroupIS.Value, isButtonClicked)
    'End Sub
    'Private Sub fndSalesTaxGroup_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If fndSalesTaxGroup.Value = "" Then
    '    Else
    '        Dim strvalue As String
    '        Dim strQuery As String = "select TAX_Group_Code from TSPL_TAX_GROUP_MASTER where TAX_Group_Code ='" + fndSalesTaxGroup.Value + "'"
    '        strvalue = clsDBFuncationality.getSingleValue(strQuery)
    '        If strvalue <> "" Then
    '        Else
    '            strQuery = ""
    '            common.clsCommon.MyMessageBoxShow("Sales tax group does not exist in master table")
    '            fndSalesTaxGroup.Value = ""
    '        End If
    '    End If
    'End Sub
    Private Sub fndSalesTaxGroup_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub
    ' Modify By : Prabhakar Ref Ticket : BM00000010125
    Private Sub txtZipPostalCode_KeyPress_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtZipPostalCode.KeyPress
        'If ((e.KeyChar >= Chr(48) And e.KeyChar <= Chr(57)) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(48) Or e.KeyChar = Chr(45)) Then
        'Else
        '    e.Handled = True
        'End If
        If Not Char.IsNumber(e.KeyChar) AndAlso Not e.KeyChar = Chr(Keys.Back) Then e.Handled = True
    End Sub
    Private Sub chkExcisable_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkExcisable.Click
        If chkExcisable.Checked = True Then
            globalFunc.mandatoryText(txtEccNumber)
        Else
            txtEccNumber.BackColor = Color.White
        End If
    End Sub
    Private Sub fndstateprovince__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndstateprovince._MYValidating
        'Dim qry As String = "select state_code as Code,state_name as [State Name] from TSPL_TDS_STATE_MASTER  "
        'fndstateprovince.Value = clsCommon.ShowSelectForm("LmSate", qry, "Code", "", fndstateprovince.Value, "[State Name]", isButtonClicked)
        fndstateprovince.Value = clsStateMaster.getFinder("", fndstateprovince.Value, isButtonClicked)
        ' txtstateprovince.Text = clsDBFuncationality.getSingleValue("select state_name from TSPL_TDS_STATE_MASTER  where state_code='" + fndstateprovince.Value + "'")
        '' Anubhooti 13-Jan-2014 (Old fetching of state desp was from TDS_State But according to code state coming from state master then it should be from state master)
        If clsCommon.myLen(fndstateprovince.Value) > 0 Then
            txtstateprovince.Text = clsDBFuncationality.getSingleValue("select ISNULL(STATE_NAME,'') STATE_NAME from TSPL_STATE_MASTER  where STATE_CODE='" + fndstateprovince.Value + "'")
            txtGstState.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select tspl_state_master.GST_STATE_Code  from tspl_state_master where STATE_CODE ='" + fndstateprovince.Value + "'"))

            Dim CompanyPan As String = clsDBFuncationality.getSingleValue("Select tspl_company_master.Pan_No from tspl_company_master where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'")
            txtGSTPANNO.Text = CompanyPan
        Else
            txtstateprovince.Text = ""
            txtGSTDegit.Text = ""
            txtGSTPANNO.Text = ""

        End If

    End Sub
    Private Sub chkExcisable_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkExcisable.ToggleStateChanged
        If chkExcisable.Checked = True Then
            chkduty.Checked = False
        End If
    End Sub
    Private Sub chkduty_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkduty.ToggleStateChanged
        If chkduty.Checked = True Then
            chkExcisable.Checked = False
        End If
    End Sub
    Private Sub fndStkTrnsfrFilledAc__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndStkTrnsfrFilledAc._MYValidating
        Dim qry As String = "SELECT Account_Code as Code, Description  FROM TSPL_GL_ACCOUNTS "
        fndStkTrnsfrFilledAc.Value = clsCommon.ShowSelectForm("GLAccSelector1", qry, "Code", " ", fndStkTrnsfrFilledAc.Value, "Code", isButtonClicked)
    End Sub

    Private Sub fndStkTrnsfrEmptyAc__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndStkTrnsfrEmptyAc._MYValidating
        Dim qry As String = "SELECT Account_Code as Code, Description  FROM TSPL_GL_ACCOUNTS "
        fndStkTrnsfrEmptyAc.Value = clsCommon.ShowSelectForm("GLAccSelector2", qry, "Code", " ", fndStkTrnsfrEmptyAc.Value, "Code", isButtonClicked)
    End Sub
    Private Sub fndStkTrnsfrFilledAc_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles fndStkTrnsfrFilledAc.KeyPress
        If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub
    Private Sub fndStkTrnsfrEmptyAc_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles fndStkTrnsfrEmptyAc.KeyPress
        If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub
    Private Sub fndLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndLocation._MYValidating
        Dim str As String = "select count(*) from TSPL_LOCATION_MASTER where Location_Code ='" + fndLocation.Value + "' "
        Dim strWhrCls As String = "Segment_name='Location' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 Then
            fndLocation.MyReadOnly = False
            txtLocationDesc.ReadOnly = False
        Else
            fndLocation.MyReadOnly = True
            'txtLocationDesc.ReadOnly = True
        End If
        If fndLocation.MyReadOnly OrElse isButtonClicked Then
            'Dim qry As String = "select Segment_code as Location,Description  from TSPL_GL_SEGMENT_CODE "
            'fndLocation.Value = clsCommon.ShowSelectForm("LocatMast", qry, "Location", strWhrCls, fndLocation.Value, "Location", isButtonClicked)
            'Dim qry As String = "select Location_Code as Location,Location_Desc as Description,Type as [Type],Location_Type as [Location Type],Loc_Segment_Code as [Location Segment Code]   from TSPL_LOCATION_MASTER "
            'fndLocation.Value = clsCommon.ShowSelectForm("LocatMast", qry, "Location", "", fndLocation.Value, "Location_Code", isButtonClicked)

            'KUNAL > DATE : 16-01-2017 > CLIENT > JAKSON
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "JAKSON") = CompairStringResult.Equal Then
                fndLocation.Value = clsLocation.getFinder(" tspl_location_master.Location_Type = 'Physical'", fndLocation.Value, isButtonClicked)
            Else
                fndLocation.Value = clsLocation.getFinder("", fndLocation.Value, isButtonClicked)
            End If


            'LoadData()
            'fndLocation.Value = clsGLSegmentCode.getFinder(strWhrCls, fndLocation.Value, isButtonClicked)
            If clsCommon.CompairString(clsCommon.myCstr(fndLocation.Value), "") = CompairStringResult.Equal Then
                funReset()
            Else
                funFill()
            End If
        End If
    End Sub
    Private Sub fndLocation__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndLocation._MYNavigator
        Dim qst As String = "select Location_Code as Location,Location_Desc as Description,Type as [Type],Location_Type as [Location Type],Loc_Segment_Code as [Location Segment Code]   from TSPL_LOCATION_MASTER  where 2=2 "
        Select Case NavType
            Case NavigatorType.Current
                qst += " and TSPL_LOCATION_MASTER .Location_Code in ('" + fndLocation.Value + "')"
            Case NavigatorType.Next
                qst += " and TSPL_LOCATION_MASTER .Location_Code in (select min(Location_Code ) from TSPL_LOCATION_MASTER where Location_Code  >'" + fndLocation.Value + "')"
            Case NavigatorType.First
                qst += " and TSPL_LOCATION_MASTER .Location_Code in (select MIN(Location_Code ) from TSPL_LOCATION_MASTER)"
            Case NavigatorType.Last
                qst += " and TSPL_LOCATION_MASTER .Location_Code in (select Max(Location_Code ) from TSPL_LOCATION_MASTER)"
            Case NavigatorType.Previous
                qst += " and TSPL_LOCATION_MASTER .Location_Code in (select Max(Location_Code ) from TSPL_LOCATION_MASTER where Location_Code  <'" + fndLocation.Value + "')"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            fndLocation.Value = clsCommon.myCstr(dt.Rows(0)("Location"))
            txtLocationDesc.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
        End If
        If clsCommon.CompairString(clsCommon.myCstr(fndLocation.Value), "") = CompairStringResult.Equal Then
            funReset()
        Else
            funFill()
        End If
    End Sub
    Private Sub fndLocationSegmentCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndLocationSegmentCode._MYValidating
        If fndLocationSegmentCode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = "select Segment_code as code,Description from TSPL_GL_SEGMENT_CODE  "
            fndLocationSegmentCode.Value = clsCommon.ShowSelectForm("LocatSegCode", qry, "code", "Seg_No='7'", fndLocationSegmentCode.Value, "Segment_code", isButtonClicked)
            LoadSegMentCodeData()
        End If
    End Sub
    Sub LoadSegMentCodeData()
        If fndLocationSegmentCode.Value = "" Then
        Else
            Dim str As String = "select Segment_code from TSPL_GL_SEGMENT_CODE where Segment_code='" + fndLocationSegmentCode.Value + "'"
            Dim strvalue As String = clsDBFuncationality.getSingleValue(str)
            If strvalue <> "" Then
            Else : str = ""
                common.clsCommon.MyMessageBoxShow(Me, "Segment code does not exist in Master Table")
                fndLocationSegmentCode.Value = ""
                fndLocationSegmentCode.Focus()
            End If
        End If
    End Sub
    'Private Sub fndPurchaseTaxGroup__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean)
    '    'Dim qry As String = "  SELECT TAX_Group_Code as code,Tax_Group_Desc as Description From TSPL_TAX_GROUP_MASTER  "
    '    'fndPurchaseTaxGroup.Value = clsCommon.ShowSelectForm("LocationMasterPT", qry, "code", "Tax_Group_Type = 'P'", fndPurchaseTaxGroup.Value, "TAX_Group_Code", isButtonClicked)
    '    fndPurchaseTaxGroup.Value = clsTaxGroupMaster.getFinder("Tax_Group_Type = 'P'", fndPurchaseTaxGroup.Value, isButtonClicked)
    '    LoadPurchaseData()
    '    OpenPurchaseTaxRateList("")
    'End Sub
    'Sub LoadPurchaseData()
    '    If fndPurchaseTaxGroup.Value = "" Then
    '    Else
    '        Dim strQuery As String = "select TAX_Group_Code from TSPL_TAX_GROUP_MASTER where TAX_Group_Code ='" + fndPurchaseTaxGroup.Value + "'"
    '        Dim strvalue As String = clsDBFuncationality.getSingleValue(strQuery)
    '        If strvalue <> "" Then
    '        Else
    '            strQuery = ""
    '            common.clsCommon.MyMessageBoxShow("Purchase tax group does not exist in master table")
    '            fndPurchaseTaxGroup.Value = ""
    '        End If
    '    End If
    'End Sub


    Private Sub fndGITLocation__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndGITLocation._MYValidating
        'Dim qry As String = "select Location_Code ,Location_Desc ,Location_Type  from TSPL_LOCATION_MASTER "
        'fndGITLocation.Value = clsCommon.ShowSelectForm("GITLocation", qry, "Location_Code", "GIT_Type  ='Y'", fndGITLocation.Value, "Location_Code", isButtonClicked)
        fndGITLocation.Value = clsLocation.getFinder("GIT_Type  ='Y'", fndGITLocation.Value, isButtonClicked)
    End Sub

    Private Sub chkthirdparty_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkthirdparty.ToggleStateChanged
        If chkthirdparty.Checked Then
            txtvndrcode.Enabled = True
            txtvndrname.Enabled = True
        Else
            txtvndrcode.Enabled = False
            txtvndrname.Enabled = False
        End If
    End Sub

    Private Sub txtvndrcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtvndrcode._MYValidating
        Dim qry As String = "select vendor_code as Code,vendor_name as Vendor,(add1+' '+add2+' '+add3) as Address,closing_date as [Closing Date],vendor_group_code as [Group Code],vendor_group_code_desc as [Group Description],city_code_desc as [City],State,phone1 as [Phone No],Fax,Email,contact_person_name as [Contact Person],contact_person_phone as [Contact No] from tspl_vendor_master"
        Dim whrcls As String = ""
        If chkCSA.Checked Then
            whrcls = " isnull(csa_type,'N')='Y'"
        End If
        txtvndrcode.Value = clsCommon.ShowSelectForm("VNDFND", qry, "Code", whrcls, txtvndrcode.Value, "Code", isButtonClicked)

        If txtvndrcode IsNot Nothing Then
            txtvndrname.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select vendor_name from tspl_vendor_master where vendor_code='" + txtvndrcode.Value + "'"))
            If chkCSA.Checked Then
                txtcommsn_code.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Commission_ACCOUNT from TSPL_VENDOR_ACCOUNT_SET where Acct_Set_Code in (select Vendor_Account from TSPL_VENDOR_MASTER where vendor_code='" + txtvndrcode.Value + "')"))
                txtcommsn_desc.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + txtcommsn_code.Value + "' ")
            End If
        End If
    End Sub

    Private Sub chk_HO_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chk_HO.ToggleStateChanged
        If isEdit Then
            Return
        End If
        If chk_HO.Checked Then
            cmbloc_cate.SelectedValue = "HO"
        ElseIf chk_HO.Checked = False AndAlso clsCommon.CompairString(cmbloc_cate.SelectedValue, "MCC") <> CompairStringResult.Equal Then
            cmbloc_cate.SelectedValue = ""
        End If
    End Sub

    Private Sub gvTaxRate_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs)

    End Sub

    Private Sub gvTaxRate_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs)
        'If gvSaleTax.RowCount > 0 Then
        '    Dim intCurrRow As Integer = gvSaleTax.CurrentRow.Index
        '    gvSaleTax.CurrentRow.Cells(colTaxLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
        '    If intCurrRow = gvSaleTax.Rows.Count - 1 Then
        '        gvSaleTax.Rows.AddNew()
        '        gvSaleTax.CurrentRow = gvSaleTax.Rows(intCurrRow        fndGITLocation.Value = clsLocation.getFinder("GIT_Type  ='Y'", fndGITLocation.Value, isButtonClicked)

        '    End If
        'End If
    End Sub

    Private Sub fndRejectedLoc__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndRejectedLoc._MYValidating
        '' Anubhooti 10-Oct-2014 (Mapped one Rejected Location to only one location code )
        fndRejectedLoc.Value = clsLocation.getFinder("Rejected_Type  ='Y' and State='" & fndstateprovince.Value & "' and Location_Code  not in ( Select ISNULL(Rejected_Location,'') As Rejected_Location  From TSPL_LOCATION_MASTER Where Location_Code  not in ('" & clsCommon.myCstr(fndLocation.Value) & "') ) ", fndRejectedLoc.Value, isButtonClicked)
    End Sub

    Private Sub fndCustomer__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndCustomer._MYValidating
        fndCustomer.Value = clsCustomerMaster.getFinder("CSA_Type  ='Y' ", fndCustomer.Value, isButtonClicked)
        If chkCSA.Checked Then
            txtcsa_comm_type.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select unit_code from tspl_unit_master where isnull(rt_rate,'N')='Y'"))
        End If
    End Sub

    Sub LoadBlankGridCat()
        gvCategory.Rows.Clear()
        gvCategory.Columns.Clear()

        Dim repoCatCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCatCode.FormatString = ""
        repoCatCode.HeaderText = "Category Code"
        repoCatCode.Name = CatcolCode
        repoCatCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoCatCode.Width = 100L
        repoCatCode.ReadOnly = True
        gvCategory.MasterTemplate.Columns.Add(repoCatCode)

        Dim repoCatCodeDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCatCodeDesc.FormatString = ""
        repoCatCodeDesc.HeaderText = "Category Description"
        repoCatCodeDesc.ReadOnly = True
        repoCatCodeDesc.Name = CatcolCodeDesc

        repoCatCodeDesc.Width = 200
        gvCategory.MasterTemplate.Columns.Add(repoCatCodeDesc)

        Dim repoCatValue As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCatValue.FormatString = ""
        repoCatValue.HeaderText = "Category Value"
        repoCatValue.Name = CatcolValue
        repoCatValue.Width = 100
        repoCatValue.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoCatValue.TextImageRelation = TextImageRelation.TextBeforeImage
        repoCatValue.ReadOnly = False
        gvCategory.MasterTemplate.Columns.Add(repoCatValue)

        Dim repoCatValueDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCatValueDesc.FormatString = ""
        repoCatValueDesc.HeaderText = "Category Value Description"
        repoCatValueDesc.Name = CatcolValueDesc
        repoCatValueDesc.Width = 200
        repoCatValueDesc.ReadOnly = True
        gvCategory.MasterTemplate.Columns.Add(repoCatValueDesc)

        gvCategory.AllowAddNewRow = False
        gvCategory.ShowGroupPanel = False
        gvCategory.AllowColumnReorder = False
        gvCategory.AllowRowReorder = False
        gvCategory.EnableSorting = False
        gvCategory.EnableFiltering = True
        gvCategory.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvCategory.MasterTemplate.ShowRowHeaderColumn = False
    End Sub

    Private Sub txtCategoryStructureCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCategoryStructureCode._MYValidating
        Dim qry As String = "select ITEM_CATEGORY_STRUCT_CODE as Code,DESCRIPTION from TSPL_ITEM_CATEGORY_STRUCTURE"
        txtCategoryStructureCode.Value = clsCommon.ShowSelectForm("ITEMMASTERCATSTRU", qry, "Code", " isnull(form_type,'item')='location'", txtCategoryStructureCode.Value, "Code", isButtonClicked)
        LoadBlankGridCat()
        If clsCommon.myLen(txtCategoryStructureCode.Value) > 0 Then
            lblCategoryStructureCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select DESCRIPTION from TSPL_ITEM_CATEGORY_STRUCTURE where ITEM_CATEGORY_STRUCT_CODE='" + txtCategoryStructureCode.Value + "' and isnull(form_type,'Item')='location'"))

            qry = "select TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_CODE ,TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION  "
            qry += " from TSPL_ITEM_CATEGORY_STRUCT_DETAIL"
            qry += " left outer join TSPL_ITEM_CATEGORY_LEVEL on  TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE=TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_CODE and TSPL_ITEM_CATEGORY_LEVEL.form_type=TSPL_ITEM_CATEGORY_STRUCT_DETAIL.form_type"
            qry += " where TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_STRUCT_CODE='" + txtCategoryStructureCode.Value + "' and isnull(TSPL_ITEM_CATEGORY_STRUCT_DETAIL.form_type,'Item')='location' order by TSPL_ITEM_CATEGORY_STRUCT_DETAIL.CATEGORY_LEVEL"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    gvCategory.Rows.AddNew()
                    gvCategory.Rows(gvCategory.RowCount - 1).Cells(CatcolCode).Value = clsCommon.myCstr(dr("ITEM_CATEGORY_CODE"))
                    gvCategory.Rows(gvCategory.RowCount - 1).Cells(CatcolCodeDesc).Value = clsCommon.myCstr(dr("DESCRIPTION"))
                Next
            End If
        Else
            lblCategoryStructureCode.Text = ""
        End If
    End Sub

    Sub OpenCatValueList(ByVal isButtonClick As Boolean)

        gvCategory.CurrentRow.Cells(CatcolValueDesc).Value = ""
        Dim qry As String = " select CODE,DESCRIPTION from TSPL_ITEM_CATEGORY_LEVEL_VALUES"
        gvCategory.CurrentRow.Cells(CatcolValue).Value = clsCommon.ShowSelectForm("itemMAsCatFind", qry, "Code", " ITEM_CATEGORY_CODE='" + clsCommon.myCstr(gvCategory.CurrentRow.Cells(CatcolCode).Value) + "' and isnull(form_type,'Item')='location'", clsCommon.myCstr(gvCategory.CurrentRow.Cells(CatcolValue).Value), "Code", isButtonClick)
        qry = "select DESCRIPTION from TSPL_ITEM_CATEGORY_LEVEL_VALUES where ITEM_CATEGORY_CODE='" + clsCommon.myCstr(gvCategory.CurrentRow.Cells(CatcolCode).Value) + "' and CODE='" + clsCommon.myCstr(gvCategory.CurrentRow.Cells(CatcolValue).Value) + "' and isnull(form_type,'Item')='location'"
        gvCategory.CurrentRow.Cells(CatcolValueDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
    End Sub

    Private Sub gvCategory_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvCategory.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gvCategory.Columns(CatcolValue) Then
                        OpenCatValueList(False)
                    End If
                    isCellValueChangedOpen = False
                End If
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub

    Private Sub radgroupbox_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radgroupbox.Click

    End Sub
    '' Anubhooti 31-July-2014
    'Private Sub ChkSection_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkSection.CheckStateChanged
    '    If ChkSection.Checked = True Then
    '        TxtSection.Enabled = True
    '        ChkSubLoc.Checked = False
    '        TxtMainLoc.Enabled = True
    '        TxtSection.Value = ""
    '        TxtMainLoc.Value = ""
    '    Else
    '        TxtSection.Enabled = False
    '        ChkSubLoc.Checked = True
    '        'TxtMainLoc.Enabled = False
    '    End If
    'End Sub
    Private Sub TxtSection__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles TxtSection._MYValidating
        Dim qry As String = " Select Section_Code As Code,Description  from TSPL_SECTION_MASTER  "
        TxtSection.Value = clsCommon.ShowSelectForm("SectionMaster", qry, "code", "", TxtSection.Value, "Code", isButtonClicked)
    End Sub

    Private Sub TxtMainLoc__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles TxtMainLoc._MYValidating
        Dim qry As String = " Select Location_Code As Code,Location_Desc  from TSPL_LOCATION_MASTER  "
        Dim WhrCls As String = " Is_Section = 'N' AND Is_Sub_Location = 'N '"
        If clsCommon.myLen(fndLocation.Value) > 0 AndAlso clsCommon.CompairString(btnSave.Text, "Update") = CompairStringResult.Equal Then
            WhrCls = WhrCls + "  AND Location_Code Not In ('" & fndLocation.Value & "')"
        End If

        If rdbbtnSection.IsChecked = True Then
            TxtMainLoc.Value = clsCommon.ShowSelectForm("LocationMaster", qry, "code", WhrCls, TxtMainLoc.Value, "Code", isButtonClicked)
        ElseIf rdbbtnSubLoc.IsChecked = True Then
            TxtMainLoc.Value = clsCommon.ShowSelectForm("LocationMaster", qry, "code", WhrCls, TxtMainLoc.Value, "Code", isButtonClicked)
        End If

    End Sub


    'Private Sub ChkSubLoc_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkSubLoc.CheckStateChanged
    '    If ChkSubLoc.Checked = True Then
    '        TxtSection.Enabled = False
    '        ChkSection.Checked = False
    '        TxtMainLoc.Enabled = True
    '        TxtSection.Value = ""
    '        TxtMainLoc.Value = ""
    '    Else
    '        TxtSection.Enabled = True
    '        ChkSection.Checked = True
    '        ' TxtMainLoc.Enabled = False
    '    End If
    'End Sub

    Private Sub rdbbtnSection_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbbtnSection.CheckStateChanged
        If rdbbtnSection.IsChecked = True Then
            TxtSection.Enabled = True
            TxtMainLoc.Enabled = True
            TxtSection.Value = ""
            TxtMainLoc.Value = ""
            txt_capacity.Enabled = True
        Else
            TxtSection.Enabled = False
            TxtMainLoc.Enabled = False
            If rdbbtnSubLoc.IsChecked = True Then
                txt_capacity.Enabled = True
            Else
                txt_capacity.Text = 0
                txt_capacity.Enabled = False
            End If
        End If
    End Sub

    Private Sub rdbbtnSubLoc_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbbtnSubLoc.CheckStateChanged
        If rdbbtnSubLoc.IsChecked = True Then
            TxtSection.Enabled = False
            TxtMainLoc.Enabled = True
            TxtSection.Value = ""
            TxtMainLoc.Value = ""
            txt_capacity.Enabled = True
        Else
            If rdbbtnSection.IsChecked = True Then
                txt_capacity.Enabled = True
            Else
                txt_capacity.Text = 0
                txt_capacity.Enabled = False
            End If
            TxtSection.Enabled = True
        End If
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        rdbbtnSection.IsChecked = False
        rdbbtnSubLoc.IsChecked = False
        TxtSection.Value = ""
        TxtMainLoc.Value = ""
        TxtSection.Enabled = False
        TxtMainLoc.Enabled = False
        txtFromDate.Value = clsCommon.GETSERVERDATE
        txtToDate.Value = clsCommon.GETSERVERDATE
        chkInsurance.Checked = False
        txtInsurance.Text = ""
    End Sub

    Private Sub chkCSA_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCSA.ToggleStateChanged
        If chkCSA.Checked Then
            fndCustomer.MendatroryField = True
            MyLabel8.Visible = True
            txtcsa_comm_type.Visible = True
            txtcsa_commision.Visible = True
            cmbComm_Type.Visible = True
            RadGroupBox1.Visible = True
            RadGroupBox1.Text = "CSA Vendor Detail"
            chkthirdparty.Visible = False
            txtvndrcode.Enabled = True
            txtvndrname.Enabled = True
            txtcommsn_code.Enabled = True
            txtcommsn_code.Visible = True
            txtcommsn_desc.Visible = True
            MyLabel10.Visible = True

            '= KUNAL > TICKET : BM00000009585 ===
            checkCommissonReq.Visible = True
            txtcsa_commision.Enabled = True
            txtcsa_comm_type.Enabled = True
            cmbComm_Type.Enabled = True
            cmbComm_Type.Text = "None"
            txtcsa_comm_type.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select unit_code from tspl_unit_master where isnull(rt_rate,'N')='Y'"))

        ElseIf Not chkCSA.Checked Then
            fndCustomer.MendatroryField = False
            MyLabel8.Visible = False
            '= KUNAL > TICKET : BM00000009585 ===
            checkCommissonReq.Checked = CheckState.Unchecked
            txtcsa_commision.Enabled = False
            txtcsa_comm_type.Enabled = False
            cmbComm_Type.Enabled = False
            cmbComm_Type.Text = "None"
            txtcsa_comm_type.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select unit_code from tspl_unit_master where isnull(rt_rate,'N')='Y'"))
            checkCommissonReq.Visible = False

            txtcsa_comm_type.Visible = False
            txtcsa_commision.Visible = False
            cmbComm_Type.Visible = False
            RadGroupBox1.Visible = False
            chkthirdparty.Visible = True
            txtvndrcode.Enabled = False
            txtvndrname.Enabled = False
            txtcommsn_code.Enabled = False
            txtcommsn_code.Visible = False
            txtcommsn_desc.Visible = False
            MyLabel10.Visible = False
            txtcommsn_code.Value = ""
            txtcommsn_desc.Text = ""
            txtcsa_comm_type.Text = ""
            cmbComm_Type.SelectedValue = ""
        End If
    End Sub

    Private Sub txtcommsn_code__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtcommsn_code._MYValidating
        Dim qry As String = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts "
        txtcommsn_code.Value = clsCommon.ShowSelectForm("CNSfnd", qry, "AccountCode", "", txtcommsn_code.Value, "", isButtonClicked)
        txtcommsn_desc.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + txtcommsn_code.Value + "' ")
    End Sub

    Private Sub fndStkTrnsfrAc__MYOpenMasterForm(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndStkTrnsfrAc._MYOpenMasterForm

    End Sub

    Private Sub fndStkTrnsfrAc__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndStkTrnsfrAc._MYValidating
        Dim qry As String = "SELECT Account_Code as Code, Description  FROM TSPL_GL_ACCOUNTS "
        fndStkTrnsfrAc.Value = clsCommon.ShowSelectForm("StkTrnsfrAc", qry, "Code", " ", fndStkTrnsfrAc.Value, "Code", isButtonClicked)
    End Sub

    Private Sub fndLossAc__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndLossAc._MYValidating
        Dim qry As String = "SELECT Account_Code as Code, Description  FROM TSPL_GL_ACCOUNTS "
        fndLossAc.Value = clsCommon.ShowSelectForm("LossAc", qry, "Code", " ", fndLossAc.Value, "Code", isButtonClicked)
    End Sub
    ''richa agarwal against ticket no BM00000004527 13/11/2014
    Private Sub chkRejected_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkRejected.ToggleStateChanged
        fndRejectedLoc.Enabled = Not chkRejected.Checked
    End Sub
    ''-----------------------------------------------



    Sub LoadBlankGrid(ByVal gv As RadGridView)
        gv.Rows.Clear()
        gv.Columns.Clear()

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.FormatString = ""
        repoSelect.HeaderText = " "
        repoSelect.Name = colSelect
        repoSelect.Width = 50
        repoSelect.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoSelect)

        Dim repoGrpCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoGrpCode.FormatString = ""
        repoGrpCode.HeaderText = "Tax Group"
        repoGrpCode.ReadOnly = True
        repoGrpCode.Name = colGrpCode
        repoGrpCode.Width = 100
        gv.MasterTemplate.Columns.Add(repoGrpCode)

        Dim repoGrpDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoGrpDesc.FormatString = ""
        repoGrpDesc.HeaderText = "Tax Group Description"
        repoGrpDesc.ReadOnly = True
        repoGrpDesc.Name = colGrpDesc
        repoGrpDesc.Width = 150
        gv.MasterTemplate.Columns.Add(repoGrpDesc)

        Dim repoTax As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax.FormatString = ""
        repoTax.HeaderText = "Tax"
        repoTax.Name = colTaxCode
        repoTax.Width = 100
        repoTax.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoTax.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTax.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoTax)

        Dim repoTaxDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTaxDesc.FormatString = ""
        repoTaxDesc.HeaderText = "Tax Desc"
        repoTaxDesc.Name = colTaxDesc
        repoTaxDesc.Width = 150
        repoTaxDesc.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoTaxDesc)

        Dim repoRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRate.FormatString = ""
        repoRate.HeaderText = "Rate"
        repoRate.Name = colTaxRate
        repoRate.ReadOnly = True
        repoRate.Width = 80
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoRate)


        Dim repoDefault As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoDefault.FormatString = ""
        repoDefault.HeaderText = "Default"
        repoDefault.Name = colDefault
        repoDefault.Width = 50
        repoDefault.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoDefault)

        'Dim repoDefaultGST As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        'repoDefaultGST.FormatString = ""
        'repoDefaultGST.HeaderText = "Default GST"
        'repoDefaultGST.Name = colDefaultGST
        'repoDefaultGST.Width = 50
        'repoDefaultGST.ReadOnly = False
        'gv.MasterTemplate.Columns.Add(repoDefaultGST)

        gv.AllowAddNewRow = False
        gv.ShowGroupPanel = False
        gv.AllowColumnReorder = False
        gv.AllowRowReorder = False
        gv.EnableSorting = False
        gv.EnableFiltering = True
        gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv.MasterTemplate.ShowRowHeaderColumn = False
    End Sub

    Private Sub gvPurchaseLocal_CellFormatting(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gvPurchaseLocal.CellFormatting
        HeaderFormatingEvent(gvPurchaseLocal, e)
    End Sub

    Private Sub gvPurchaseInterState_CellFormatting(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gvPurchaseInterState.CellFormatting
        HeaderFormatingEvent(gvPurchaseInterState, e)
    End Sub

    Private Sub gvPurchaseTaxLocal_CellFormatting(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gvPurchaseTaxLocal.CellFormatting
        DetailFormatingEvent(gvPurchaseTaxLocal, e)
    End Sub

    Private Sub gvPurchaseTaxInterState_CellFormatting(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gvPurchaseTaxInterState.CellFormatting
        DetailFormatingEvent(gvPurchaseTaxInterState, e)
    End Sub

    Private Sub gvPurchaseLocal_ValueChanging(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.ValueChangingEventArgs) Handles gvPurchaseLocal.ValueChanging
        HeaderValueChangeEvent(gvPurchaseLocal, e, gvPurchaseTaxLocal, "P")
    End Sub

    Private Sub gvPurchaseInterState_ValueChanging(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.ValueChangingEventArgs) Handles gvPurchaseInterState.ValueChanging
        HeaderValueChangeEvent(gvPurchaseInterState, e, gvPurchaseTaxInterState, "P")
    End Sub

    Private Sub gvPurchaseTaxLocal_ValueChanging(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.ValueChangingEventArgs) Handles gvPurchaseTaxLocal.ValueChanging
        DetailValueChangeEvent(e, gvPurchaseTaxLocal)
    End Sub

    Private Sub gvPurchaseTaxInterState_ValueChanging(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.ValueChangingEventArgs) Handles gvPurchaseTaxInterState.ValueChanging
        DetailValueChangeEvent(e, gvPurchaseTaxInterState)
    End Sub

    Sub LoadTaxGrpHeader(ByVal gv As RadGridView, ByVal TaxType As String, ByVal StateOrInterstate As String)
        Dim qry As String = "select CAST( 0 as Bit) as Sel,Tax_Group_Code,Tax_Group_Desc,Cast (0 as Bit) as Is_Default,Cast (0 as Bit) as Is_Default_GST "
        qry += " from TSPL_TAX_GROUP_MASTER where 2=2 and Tax_Group_Type='" + TaxType + "'"


        qry += "   and not exists(" &
        " select 1 from TSPL_TAX_GROUP_DETAILS left join TSPL_TAX_MASTER on TSPL_TAX_MASTER.Tax_Code =TSPL_TAX_GROUP_DETAILS.Tax_Code " &
        " where TSPL_TAX_GROUP_DETAILS.Tax_Group_Code=TSPL_TAX_GROUP_MASTER.Tax_Group_Code and TSPL_TAX_GROUP_DETAILS.Tax_Group_Type=TSPL_TAX_GROUP_MASTER.Tax_Group_Type and TSPL_TAX_MASTER.Type in (" & StateOrInterstate & ")  ) "

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        gv.DataSource = dt


        gv.Columns("Sel").HeaderText = " "
        gv.Columns("Sel").Width = 50
        gv.Columns("Sel").ReadOnly = False
        gv.Columns("Tax_Group_Code").HeaderText = "Tax Group"
        gv.Columns("Tax_Group_Code").Width = 100
        gv.Columns("Tax_Group_Code").ReadOnly = True
        gv.Columns("Tax_Group_Desc").HeaderText = "Tax Group Desc"
        gv.Columns("Tax_Group_Desc").Width = 150
        gv.Columns("Tax_Group_Desc").ReadOnly = True
        gv.Columns("Is_Default").HeaderText = "Default"
        gv.Columns("Is_Default").Width = 80
        gv.Columns("Is_Default").ReadOnly = False
        gv.Columns("Is_Default_GST").HeaderText = "Default GST"
        gv.Columns("Is_Default_GST").Width = 120
        gv.Columns("Is_Default_GST").ReadOnly = False






        gv.AllowAddNewRow = False
        gv.ShowGroupPanel = False
        gv.AllowColumnReorder = False
        gv.AllowRowReorder = False
        gv.EnableSorting = False
        gv.EnableFiltering = True
        gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv.MasterTemplate.ShowRowHeaderColumn = False
    End Sub

    Sub LoadTaxGrpDetail(ByVal gvDetail As RadGridView, ByVal strGrpCode As String, ByVal e As Telerik.WinControls.UI.ValueChangingEventArgs, ByVal strTaxType As String)
        If e.NewValue Then
            Dim qry As String = "select  TSPL_TAX_GROUP_DETAILS.Tax_Group_Code,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc,TSPL_TAX_RATES.Tax_Code,TSPL_TAX_MASTER.Tax_Code_Desc,TSPL_TAX_RATES.Tax_Rate" &
            " from TSPL_TAX_RATES " &
            " left outer join TSPL_TAX_MASTER on TSPL_TAX_MASTER.Tax_Code=TSPL_TAX_RATES.Tax_Code   " &
            " left outer join TSPL_TAX_GROUP_DETAILS on TSPL_TAX_GROUP_DETAILS.Tax_Code=TSPL_TAX_RATES.Tax_Code " &
            " left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_TAX_GROUP_DETAILS.Tax_Group_Code " &
            " where TSPL_TAX_GROUP_MASTER.Tax_Group_Code='" + strGrpCode + "' and TSPL_TAX_GROUP_DETAILS.Tax_Group_Type='" + strTaxType + "' and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='" + strTaxType + "' and TSPL_TAX_RATES.Tax_Type='" + strTaxType + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    gvDetail.Rows.AddNew()
                    gvDetail.Rows(gvDetail.RowCount - 1).Cells(colGrpCode).Value = clsCommon.myCstr(dr("Tax_Group_Code"))
                    gvDetail.Rows(gvDetail.RowCount - 1).Cells(colGrpDesc).Value = clsCommon.myCstr(dr("Tax_Group_Desc"))
                    gvDetail.Rows(gvDetail.RowCount - 1).Cells(colTaxCode).Value = clsCommon.myCstr(dr("Tax_Code"))
                    gvDetail.Rows(gvDetail.RowCount - 1).Cells(colTaxDesc).Value = clsCommon.myCstr(dr("Tax_Code_Desc"))
                    gvDetail.Rows(gvDetail.RowCount - 1).Cells(colTaxRate).Value = clsCommon.myCdbl(dr("Tax_Rate"))
                Next
            End If
        Else
            For ii As Integer = gvDetail.RowCount - 1 To 0 Step -1
                If clsCommon.CompairString(clsCommon.myCstr(gvDetail.Rows(ii).Cells(colGrpCode).Value), strGrpCode) = CompairStringResult.Equal Then
                    gvDetail.Rows.RemoveAt(ii)
                End If
            Next
        End If
    End Sub

    Sub HeaderValueChangeEvent(ByVal gvMain As RadGridView, ByVal e As Telerik.WinControls.UI.ValueChangingEventArgs, ByVal gvDetail As RadGridView, ByVal strTaxType As String)
        Try
            If gvMain.CurrentColumn Is gvMain.Columns("Sel") Then
                LoadTaxGrpDetail(gvDetail, clsCommon.myCstr(gvMain.CurrentRow.Cells("Tax_Group_Code").Value), e, strTaxType)
            ElseIf gvMain.CurrentColumn Is gvMain.Columns("Is_Default") Then 'Or gvMain.CurrentColumn Is gvMain.Columns("Is_Default_GST")
                If e.NewValue Then
                    For ii As Integer = 0 To gvMain.Rows.Count - 1
                        gvMain.Rows(ii).Cells("Is_Default").Value = False

                    Next
                End If
            ElseIf gvMain.CurrentColumn Is gvMain.Columns("Is_Default_GST") Then 'Or gvMain.CurrentColumn Is gvMain.Columns("Is_Default_GST")
                If e.NewValue Then
                    For ii As Integer = 0 To gvMain.Rows.Count - 1

                        gvMain.Rows(ii).Cells("Is_Default_GST").Value = False
                    Next
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Sub DetailValueChangeEvent(ByVal e As Telerik.WinControls.UI.ValueChangingEventArgs, ByVal gvDetail As RadGridView)
        Try
            If gvDetail.CurrentColumn Is gvDetail.Columns(colDefault) Then
                If e.NewValue Then
                    Dim strCurrentGrpCode As String = clsCommon.myCstr(gvDetail.CurrentRow.Cells(colGrpCode).Value)
                    Dim strCurrentTaxCode As String = clsCommon.myCstr(gvDetail.CurrentRow.Cells(colTaxCode).Value)
                    For ii As Integer = 0 To gvDetail.Rows.Count - 1
                        If clsCommon.CompairString(strCurrentGrpCode, clsCommon.myCstr(gvDetail.Rows(ii).Cells(colGrpCode).Value)) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strCurrentTaxCode, clsCommon.myCstr(gvDetail.Rows(ii).Cells(colTaxCode).Value)) = CompairStringResult.Equal Then
                            gvDetail.Rows(ii).Cells(colDefault).Value = False
                        End If
                    Next
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Sub HeaderFormatingEvent(ByVal gv As RadGridView, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs)
        Try
            If e.Column.Index >= 0 Then
                If (e.Column Is gv.Columns("Is_Default")) Or (e.Column Is gv.Columns("Is_Default_GST")) Then
                    gv.CurrentRow.Cells("Is_Default").ReadOnly = Not clsCommon.myCBool(gv.CurrentRow.Cells("Sel").Value)
                    gv.CurrentRow.Cells("Is_Default_GST").ReadOnly = Not clsCommon.myCBool(gv.CurrentRow.Cells("Sel").Value)
                ElseIf (e.Column Is gv.Columns("Sel")) Then
                    If Not clsCommon.myCBool(gv.CurrentRow.Cells("Sel").Value) Then
                        gv.CurrentRow.Cells("Is_Default").Value = False
                        gv.CurrentRow.Cells("Is_Default_GST").Value = False
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Sub DetailFormatingEvent(ByVal gv As RadGridView, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs)
        Try
            If e.Column.Index >= 0 Then
                If (e.Column Is gv.Columns(colDefault)) Then
                    gv.CurrentRow.Cells(colDefault).ReadOnly = Not clsCommon.myCBool(gv.CurrentRow.Cells(colSelect).Value)
                ElseIf (e.Column Is gv.Columns(colSelect)) Then
                    If Not clsCommon.myCBool(gv.CurrentRow.Cells(colSelect).Value) Then
                        gv.CurrentRow.Cells(colDefault).Value = False
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gvSaleLocal_ValueChanging(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.ValueChangingEventArgs) Handles gvSaleLocal.ValueChanging
        HeaderValueChangeEvent(gvSaleLocal, e, gvSaleTaxLocal, "S")
    End Sub

    Private Sub gvSaleLocal_CellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gvSaleLocal.CellFormatting
        HeaderFormatingEvent(gvSaleLocal, e)
    End Sub

    Private Sub gvSaleInterState_ValueChanging(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.ValueChangingEventArgs) Handles gvSaleInterState.ValueChanging
        HeaderValueChangeEvent(gvSaleInterState, e, gvSaleTaxInterState, "S")
    End Sub

    Private Sub gvSaleInterState_CellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gvSaleInterState.CellFormatting
        HeaderFormatingEvent(gvSaleInterState, e)
    End Sub

    Private Sub gvSaleTaxInterState_ValueChanging(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.ValueChangingEventArgs) Handles gvSaleTaxInterState.ValueChanging
        DetailValueChangeEvent(e, gvSaleTaxInterState)
    End Sub

    Private Sub gvSaleTaxInterState_CellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gvSaleTaxInterState.CellFormatting
        DetailFormatingEvent(gvSaleTaxInterState, e)
    End Sub

    Private Sub gvSaleTaxLocal_CellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gvSaleTaxLocal.CellFormatting
        DetailFormatingEvent(gvSaleTaxLocal, e)
    End Sub

    Private Sub gvSaleTaxLocal_ValueChanging(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.ValueChangingEventArgs) Handles gvSaleTaxLocal.ValueChanging
        DetailValueChangeEvent(e, gvSaleTaxLocal)
    End Sub

    Function IsGrpDefaultGroup(ByVal gvMaster As RadGridView, ByVal strGrpCode As String) As Boolean
        For ii As Integer = 0 To gvMaster.Rows.Count - 1
            Dim strInnerGrpCode As String = clsCommon.myCstr(gvMaster.Rows(ii).Cells("Tax_Group_Code").Value)


            If clsCommon.CompairString(strGrpCode, strInnerGrpCode) = CompairStringResult.Equal Then
                Return clsCommon.myCBool(gvMaster.Rows(ii).Cells("Is_Default").Value)
            End If
        Next
        Return False
    End Function
    Function IsGrpDefaultGroupGST(ByVal gvMaster As RadGridView, ByVal strGrpCode As String) As Boolean
        For ii As Integer = 0 To gvMaster.Rows.Count - 1
            Dim strInnerGrpCode As String = clsCommon.myCstr(gvMaster.Rows(ii).Cells("Tax_Group_Code").Value)

            If clsCommon.CompairString(strGrpCode, strInnerGrpCode) = CompairStringResult.Equal Then
                Return clsCommon.myCBool(gvMaster.Rows(ii).Cells("Is_Default_GST").Value)
            End If

        Next
        Return False
    End Function
    '' events for transfer tax 
    Private Sub gvTransferLocal_CellFormatting(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gvTransferLocal.CellFormatting
        HeaderFormatingEvent(gvTransferLocal, e)
    End Sub

    Private Sub gvTransferLocal_ValueChanging(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.ValueChangingEventArgs) Handles gvTransferLocal.ValueChanging
        HeaderValueChangeEvent(gvTransferLocal, e, gvTransferTaxLocal, "T")
    End Sub

    Private Sub gvTransferInterState_CellFormatting(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gvTransferInterState.CellFormatting
        HeaderFormatingEvent(gvTransferInterState, e)
    End Sub

    Private Sub gvTransferInterState_ValueChanging(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.ValueChangingEventArgs) Handles gvTransferInterState.ValueChanging
        HeaderValueChangeEvent(gvTransferInterState, e, gvTransferTaxInterState, "T")
    End Sub

    Private Sub gvTransferTaxLocal_CellFormatting(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gvTransferTaxLocal.CellFormatting
        DetailFormatingEvent(gvTransferTaxLocal, e)
    End Sub

    Private Sub gvTransferTaxLocal_ValueChanging(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.ValueChangingEventArgs) Handles gvTransferTaxLocal.ValueChanging
        DetailValueChangeEvent(e, gvTransferTaxLocal)
    End Sub

    Private Sub gvTransferTaxInterState_CellFormatting(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gvTransferTaxInterState.CellFormatting
        DetailFormatingEvent(gvTransferTaxInterState, e)
    End Sub

    Private Sub gvTransferTaxInterState_ValueChanging(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.ValueChangingEventArgs) Handles gvTransferTaxInterState.ValueChanging
        DetailValueChangeEvent(e, gvTransferTaxInterState)
    End Sub

    Private Sub ChkIsJobwork_CheckStateChanged(sender As Object, e As EventArgs) Handles ChkIsJobwork.CheckStateChanged
        If ChkIsJobwork.Checked Then
            GrpJobwork.Enabled = True
        Else
            GrpJobwork.Enabled = False
            FndJobworkVendor.Value = Nothing
            FndJobworkItem.Value = Nothing
            TxtJobworkItem.Text = Nothing
            TxtJObworkVendor.Text = Nothing
            gvItem.UnCheckedAll()
        End If
    End Sub

    Private Sub FndJobworkVendor__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles FndJobworkVendor._MYValidating
        Try
            Dim qst As String = "select Vendor_Code as [Code],Vendor_Name as [Vendor Name],Vendor_Group_Code as [Vendor Group],(select case when Status='N' then 'Active' else 'In.Active' end ) as [Status] from TSPL_VENDOR_MASTER  "
            FndJobworkVendor.Value = clsCommon.ShowSelectForm("JWVen", qst, "Code", " (form_type='ALL' or form_type='VSP')", FndJobworkVendor.Value, "Code", isButtonClicked)
            TxtJObworkVendor.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_name from tspl_vendor_master where vendor_code='" & clsCommon.myCstr(FndJobworkVendor.Value) & "'"))
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FndJobworkItem__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles FndJobworkItem._MYValidating
        Try
            FndJobworkItem.Value = clsItemMaster.getFinder("", FndJobworkItem.Value, isButtonClicked)
            TxtJobworkItem.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Desc from tspl_Item_master where Item_code='" & clsCommon.myCstr(FndJobworkItem.Value) & "'"))
        Catch ex As Exception
        End Try
    End Sub

    Sub LoadItem()
        Dim qry As String = " select item_code ,item_Desc  from TSPL_ITEM_MASTER order by Item_Code "
        gvItem.DataSource = clsDBFuncationality.GetDataTable(qry)
        gvItem.ValueMember = "item_code"
        gvItem.DisplayMember = "item_Desc"
    End Sub

    Sub LoadItemLocal()
        gvSaleItemDetailsLocal.DataSource = Nothing

        ' Dim qry As String = "SELECT cast(0 as bit) as Sel,Item_Code As [Item Code],Item_Desc as Description FROM TSPL_ITEM_MASTER where Is_FreshItem=0 and Active =1 and Product_Type not in ('MI') and Item_Type in ('F','T') and Is_Serial_Item=0 "
        Dim qry As String = "Select Final.* from (sELECT cast(1 as bit) as Sel,TSPL_LOCATION_WISE_ITEM_MASTER.Item_Code As [Item Code],TSPL_LOCATION_WISE_ITEM_MASTER.Item_Desc as Description FROM TSPL_LOCATION_WISE_ITEM_MASTER where isnull(TSPL_LOCATION_WISE_ITEM_MASTER.Item_Category,'')='L' and TSPL_LOCATION_WISE_ITEM_MASTER.Location_Code='" & clsCommon.myCstr(fndLocation.Value) & "'" &
       " union all " &
       " SELECT cast(0 as bit) as Sel,TSPL_ITEM_MASTER.Item_Code As [Item Code],TSPL_ITEM_MASTER.Item_Desc as Description FROM TSPL_ITEM_MASTER where Is_FreshItem=0 and Product_Type not in ('MI') and Item_Type in ('F','T') and Is_Serial_Item=0 and Active =1 and TSPL_ITEM_MASTER.Item_Code not in (sELECT TSPL_LOCATION_WISE_ITEM_MASTER.Item_Code FROM TSPL_LOCATION_WISE_ITEM_MASTER where isnull(TSPL_LOCATION_WISE_ITEM_MASTER.Item_Category,'')='L' and TSPL_LOCATION_WISE_ITEM_MASTER.Location_Code='" & clsCommon.myCstr(fndLocation.Value) & "')) Final ORDER BY fINAL.[Item Code] "

        gvSaleItemDetailsLocal.DataSource = clsDBFuncationality.GetDataTable(qry)

        gvSaleItemDetailsLocal.Columns("Sel").HeaderText = " "
        gvSaleItemDetailsLocal.Columns("Sel").Width = 50
        gvSaleItemDetailsLocal.Columns("Sel").ReadOnly = False

        gvSaleItemDetailsLocal.Columns("Item Code").HeaderText = "Item Code"
        gvSaleItemDetailsLocal.Columns("Item Code").Width = 100
        gvSaleItemDetailsLocal.Columns("Item Code").ReadOnly = True

        gvSaleItemDetailsLocal.Columns("Description").HeaderText = "Item Desc"
        gvSaleItemDetailsLocal.Columns("Description").Width = 200
        gvSaleItemDetailsLocal.Columns("Description").ReadOnly = True

        gvSaleItemDetailsLocal.AllowAddNewRow = False
        gvSaleItemDetailsLocal.ShowGroupPanel = False
        gvSaleItemDetailsLocal.AllowColumnReorder = False
        gvSaleItemDetailsLocal.AllowRowReorder = False
        gvSaleItemDetailsLocal.EnableSorting = False
        gvSaleItemDetailsLocal.EnableFiltering = True
        gvSaleItemDetailsLocal.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvSaleItemDetailsLocal.MasterTemplate.ShowRowHeaderColumn = False
    End Sub

    Sub LoadItemInter()
        gvSaleItemDetailsInterState.DataSource = Nothing

        'Dim qry As String = "SELECT cast(0 as bit) as Sel,Item_Code As [Item Code],Item_Desc as Description FROM TSPL_ITEM_MASTER where Is_FreshItem=0 and Active =1 and Product_Type not in ('MI') and Item_Type in ('F','T') and Is_Serial_Item=0 "
        Dim qry As String = "Select Final.* from (sELECT cast(1 as bit) as Sel,TSPL_LOCATION_WISE_ITEM_MASTER.Item_Code As [Item Code],TSPL_LOCATION_WISE_ITEM_MASTER.Item_Desc as Description FROM TSPL_LOCATION_WISE_ITEM_MASTER where isnull(TSPL_LOCATION_WISE_ITEM_MASTER.Item_Category,'')='I' and TSPL_LOCATION_WISE_ITEM_MASTER.Location_Code='" & clsCommon.myCstr(fndLocation.Value) & "'" &
        " union all " &
        " SELECT cast(0 as bit) as Sel,TSPL_ITEM_MASTER.Item_Code As [Item Code],TSPL_ITEM_MASTER.Item_Desc as Description FROM TSPL_ITEM_MASTER where Is_FreshItem=0 and Product_Type not in ('MI') and Item_Type in ('F','T') and Is_Serial_Item=0 and Active =1 and TSPL_ITEM_MASTER.Item_Code not in (sELECT TSPL_LOCATION_WISE_ITEM_MASTER.Item_Code FROM TSPL_LOCATION_WISE_ITEM_MASTER where isnull(TSPL_LOCATION_WISE_ITEM_MASTER.Item_Category,'')='I' and TSPL_LOCATION_WISE_ITEM_MASTER.Location_Code='" & clsCommon.myCstr(fndLocation.Value) & "')) Final ORDER BY fINAL.[Item Code] "
        gvSaleItemDetailsInterState.DataSource = clsDBFuncationality.GetDataTable(qry)

        gvSaleItemDetailsInterState.Columns("Sel").HeaderText = " "
        gvSaleItemDetailsInterState.Columns("Sel").Width = 50
        gvSaleItemDetailsInterState.Columns("Sel").ReadOnly = False

        gvSaleItemDetailsInterState.Columns("Item Code").HeaderText = "Item Code"
        gvSaleItemDetailsInterState.Columns("Item Code").Width = 100
        gvSaleItemDetailsInterState.Columns("Item Code").ReadOnly = True

        gvSaleItemDetailsInterState.Columns("Description").HeaderText = "Item Desc"
        gvSaleItemDetailsInterState.Columns("Description").Width = 200
        gvSaleItemDetailsInterState.Columns("Description").ReadOnly = True

        gvSaleItemDetailsInterState.AllowAddNewRow = False
        gvSaleItemDetailsInterState.ShowGroupPanel = False
        gvSaleItemDetailsInterState.AllowColumnReorder = False
        gvSaleItemDetailsInterState.AllowRowReorder = False
        gvSaleItemDetailsInterState.EnableSorting = False
        gvSaleItemDetailsInterState.EnableFiltering = True
        gvSaleItemDetailsInterState.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvSaleItemDetailsInterState.MasterTemplate.ShowRowHeaderColumn = False
    End Sub

    '= KUNAL > TICKET : BM00000009585 ===
    Private Sub checkCommissonReq_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles checkCommissonReq.ToggleStateChanged
        Try
            If checkCommissonReq.Checked Then
                txtcsa_commision.Enabled = True
                txtcsa_comm_type.Enabled = True
                cmbComm_Type.Enabled = True
                cmbComm_Type.Text = "None"
                txtcsa_comm_type.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select unit_code from tspl_unit_master where isnull(rt_rate,'N')='Y'"))

            Else
                txtcsa_commision.Enabled = False
                txtcsa_comm_type.Enabled = False
                cmbComm_Type.Enabled = False
                cmbComm_Type.Text = "None"
                txtcsa_comm_type.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select unit_code from tspl_unit_master where isnull(rt_rate,'N')='Y'"))
                txtcsa_commision.Text = 0
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub TxtMultiLocation__My_Click(sender As Object, e As EventArgs) Handles TxtMultiLocation._My_Click
        Dim qry As String = "select Location_Code as [Code] ,Location_Desc as [Name] from TSPL_LOCATION_MASTER where Type ='Depot' and ((GIT_Type='N') or ISNULL(GIT_Type,'')='') and ((CSA_Type='N') or ISNULL(CSA_Type,'')='') and ((Is_Section='N') or ISNULL(Is_Section,'')='') and ((Is_Sub_Location = 'N') or ISNULL(Is_Sub_Location,'')='')"
        TxtMultiLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("LOCFND", qry, "Code", "Name", TxtMultiLocation.arrValueMember, TxtMultiLocation.arrDispalyMember)
    End Sub

    Private Sub ddlType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles ddlType.SelectedIndexChanged
        If clsCommon.CompairString(ddlType.Text, "Plant") = CompairStringResult.Equal Then
            TxtMultiLocation.Visible = True
            MyLabel17.Visible = True
        Else
            TxtMultiLocation.Visible = False
            MyLabel17.Visible = False
        End If
    End Sub

    Private Sub btnimportlocation_Click(sender As Object, e As EventArgs) Handles btnimportlocation.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        '''''''''''' retrieving associated custom field list
        Dim drr As DataTable
        Dim qry1 As String = "select TSPL_CUSTOM_FIELD_HEAD.Name,TSPL_CUSTOM_FIELD_HEAD.Code,TSPL_CUSTOM_FIELD_MAPPING.Program_Code  from TSPL_CUSTOM_FIELD_HEAD left outer join TSPL_CUSTOM_FIELD_MAPPING on TSPL_CUSTOM_FIELD_MAPPING.Custom_Field_Code =TSPL_CUSTOM_FIELD_HEAD.Code WHERE PROGRAM_CODE='" & MyBase.Form_ID & "'"
        drr = clsDBFuncationality.GetDataTable(qry1)
        Dim Count As Integer
        If objCommonVar.GSTApplicable Then
            Count = 57
        Else
            Count = 56
        End If
        Dim str(0 To ((Count + drr.Rows.Count) - 1)) As String
        str(0) = "Location_Code"
        str(1) = "Location_Desc"
        str(2) = "Loc_Short_Name"
        str(3) = "Add1"
        str(4) = "Add2"
        str(5) = "Add3"
        str(6) = "Add4"
        str(7) = "City_Code"
        str(8) = "State"
        str(9) = "Pin_Code"
        str(10) = "Country"
        str(11) = "Telphone"
        str(12) = "Email"
        str(13) = "Location_Type"
        str(14) = "Loc_Status"
        str(15) = "Status_Date"
        str(16) = "Excisable"
        str(17) = "Loc Segment Code"
        str(18) = "Type"
        str(19) = "Purchase Tax Group"
        str(20) = "Sales Tax Group"
        str(21) = "Ecc Number"
        str(22) = "Registration Number"
        str(23) = "Commissionerate"
        str(24) = "Range Code"
        str(25) = "Range Name"
        str(26) = "Range Address"
        str(27) = "Division Code"
        str(28) = "Division Name"
        str(29) = "Division Address"
        str(30) = "TIN No"
        str(31) = "TAN No"
        str(32) = "TCAN No"
        str(33) = "Service Tax Reg No"
        str(34) = "Duty Paid"
        str(35) = "Purchase Tax GroupIS"
        str(36) = "Sales Tax GroupIS"
        str(37) = "Stock Transfer Filled Account"
        str(38) = "Stock Transfer Empty Account"
        str(39) = "Third Party Location Vendor"
        str(40) = "Location Category HO-MCC"
        str(41) = "Category_Struct_Code"
        '' Anubhooti 31-July-2014
        str(42) = "Is_Section"
        str(43) = "Is_Sub_Location"
        str(44) = "Section Code"
        str(45) = "Main Location Code"
        str(46) = "is_consumption_location"
        str(47) = "ESIC No"
        str(48) = "PF No"

        str(49) = "DairyDispatchFromDO"
        str(50) = "Is_Insurance"
        str(51) = "InsuranceNo"
        str(52) = "InsuranceFromDate"
        str(53) = "InsuranceToDate"
        str(54) = "IsSubLocationWise"
        str(55) = "IsMainPlant"
        If objCommonVar.GSTApplicable Then
            str(56) = "GSTNo"

        End If
        Dim CountIntegr As Integer = 0
        If objCommonVar.GSTApplicable Then
            CountIntegr = 56
        Else
            CountIntegr = 55
        End If
        Dim i As Integer = CountIntegr
        For Each row As DataRow In drr.Rows
            str(i) = row(0).ToString
            i = i + 1
        Next
        Dim linno As Integer = 1
        If transportSql.importExcel(gv, str) Then
            Dim trans As SqlTransaction = Nothing
            Try
                trans = clsDBFuncationality.GetTransactin()

                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim strLocation As String
                    Dim strLDesc As String
                    Dim strLocShortName As String = Nothing
                    Dim strAdd1 As String
                    Dim strAdd2 As String
                    Dim strAdd3 As String
                    Dim strAdd4 As String
                    Dim strCity As String
                    Dim strStateProvince As String
                    Dim strPostalCode As String
                    Dim strCountry As String
                    Dim strTelephone As String
                    Dim strEmail As String
                    Dim strLocationType As String
                    Dim strloc_category As String
                    Dim is_consumption As String = Nothing

                    Dim Section_Code As String
                    Dim Main_Location_Code As String
                    Dim DairyDispatchFromDO As Integer = 0
                    Dim GSTNo As String = Nothing
                    Dim GSTEntity As String = Nothing
                    Dim GSTBlank As String = Nothing
                    Dim GSTdigit As String = Nothing

                    Dim thirdparty As String = ""
                    Dim Is_Insurance As Integer = 0
                    Dim InsuranceNo As String = ""
                    Dim InsuranceFromDate As Date?
                    Dim InsuranceToDate As Date?

                    Is_Insurance = clsCommon.myCdbl(grow.Cells("Is_Insurance").Value)
                    If Is_Insurance = 1 Then
                        If clsCommon.myLen(grow.Cells("InsuranceNo").Value.ToString()) = 0 Then
                            Throw New Exception("Insurance No cannot be blank ")
                        End If
                        If clsCommon.myCDate(grow.Cells("InsuranceFromDate").Value) > clsCommon.myCDate(grow.Cells("InsuranceToDate").Value) Then
                            Throw New Exception("Insurance To date can not be before than from date.")
                        End If

                        InsuranceNo = clsCommon.myCstr(grow.Cells("InsuranceNo").Value.ToString())
                        InsuranceFromDate = clsCommon.myCDate(grow.Cells("InsuranceFromDate").Value)
                        InsuranceToDate = clsCommon.myCDate(grow.Cells("InsuranceToDate").Value)
                    Else
                        InsuranceNo = Nothing
                        InsuranceFromDate = Nothing
                        InsuranceToDate = Nothing
                    End If


                    thirdparty = clsCommon.myCstr(grow.Cells("Third Party Location Vendor").Value.ToString())

                    strloc_category = clsCommon.myCstr(grow.Cells("Location Category HO-MCC").Value)

                    If clsCommon.myCstr(grow.Cells(0).Value) = String.Empty Then
                        Throw New Exception("Location Code cannot be blank ")
                    ElseIf clsCommon.myLen(grow.Cells(0).Value) > 12 Then
                        Throw New Exception("Location code can be of maximum length 12 ")
                    Else
                        strLocation = grow.Cells(0).Value.ToString().ToUpper()
                    End If
                    If clsCommon.myLen(grow.Cells(1).Value) > 50 Then
                        Throw New Exception("Description cannot be greater than 50 length ")
                    Else
                        strLDesc = clsCommon.myCstr(grow.Cells(1).Value)
                    End If
                    If clsCommon.myLen(grow.Cells(3).Value) > 50 Then
                        Throw New Exception("Address1 cannot be greater than 50 length ")
                    Else
                        strAdd1 = grow.Cells(3).Value.ToString()
                    End If
                    If clsCommon.myLen(grow.Cells(4).Value) > 50 Then
                        Throw New Exception("Address2 cannot be greater than 50 length ")
                    Else
                        strAdd2 = clsCommon.myCstr(grow.Cells(4).Value)
                    End If
                    If clsCommon.myLen(grow.Cells(5).Value) > 50 Then
                        Throw New Exception("Address3 cannot be greater than 50 length ")

                    Else
                        strAdd3 = clsCommon.myCstr(grow.Cells(5).Value)
                    End If
                    If clsCommon.myLen(grow.Cells(6).Value) > 50 Then
                        Throw New Exception("Address4 cannot be greater than 50 length ")

                    Else
                        strAdd4 = clsCommon.myCstr(grow.Cells(6).Value)
                    End If
                    If clsCommon.myLen(grow.Cells(7).Value) > 50 Then
                        Throw New Exception("City cannot be greater than 50 length ")
                    Else
                        strCity = clsCommon.myCstr(grow.Cells(7).Value)
                    End If
                    If clsCommon.myLen(grow.Cells(8).Value) > 50 Then
                        Throw New Exception("State/Province cannot be greater than 50 length ")
                    Else
                        strStateProvince = clsCommon.myCstr(grow.Cells(8).Value)
                        If clsCommon.myLen(strStateProvince) > 0 Then
                            If clsDBFuncationality.getSingleValue("select count(*) from TSPL_STATE_MASTER where state_code='" & strStateProvince & "'", trans) = 0 Then
                                Throw New Exception("Invalid State/Province Code  ")
                            End If
                        Else
                            Throw New Exception("State/Province cannot be blank ")
                        End If
                    End If
                    If clsCommon.myLen(grow.Cells(9).Value) > 9 Then
                        Throw New Exception("Postal Code cannot be greater than 9 length ")
                    ElseIf Not IsNumeric(grow.Cells(9).Value) Then
                        Throw New Exception("Char value not allowed in Postal Code. ")
                    Else
                        strPostalCode = grow.Cells(9).Value
                    End If
                    If clsCommon.myLen(grow.Cells(10).Value) > 50 Then
                        Throw New Exception("Country cannot be greater than 50 length ")
                    Else
                        strCountry = clsCommon.myCstr(grow.Cells(10).Value)
                    End If
                    If clsCommon.myLen(grow.Cells(11).Value) > 50 Then
                        Throw New Exception("Telephone cannot be greater than 50 length ")
                    Else
                        strTelephone = clsCommon.myCstr(grow.Cells(11).Value)
                    End If
                    If clsCommon.myLen(grow.Cells(12).Value) > 50 Then
                        Throw New Exception("Email cannot be greater than 50 length ")
                    ElseIf clsCommon.myCstr(grow.Cells(12).Value) <> String.Empty Then
                        strEmail = clsCommon.myCstr(grow.Cells(12).Value)
                        Dim re As Regex = New Regex("\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")
                        If Not re.IsMatch(strEmail) Then
                            Throw New Exception("Email has some incorrect values ")
                        End If
                    Else
                        strEmail = grow.Cells(12).Value.ToString()
                    End If
                    '' Anubhooti 07-Oct-2014 (Error occured in case of blank location type)
                    If clsCommon.CompairString(clsCommon.myCstr(grow.Cells(13).Value), "Physical") = CompairStringResult.Equal Then
                        Dim locsegcode = clsCommon.myCstr(grow.Cells("Loc Segment Code").Value)
                        If clsCommon.myLen(locsegcode) <= 0 Then
                            Throw New Exception("There Must be Location Segment Code for Location Type Physical ")
                        End If

                        strLocationType = clsCommon.myCstr(grow.Cells(13).Value)
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(grow.Cells(13).Value), "Logical") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Excisable").Value.ToString()), "T") = CompairStringResult.Equal Then
                            Throw New Exception("Logical Location Can not be Excisable  ")
                        End If
                        strLocationType = clsCommon.myCstr(grow.Cells(13).Value)
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(grow.Cells(13).Value), "WorkOrder") = CompairStringResult.Equal Then
                        strLocationType = clsCommon.myCstr(grow.Cells(13).Value)
                        '----Updation by Richa Agarwal - against Ticket No. BM00000003586 on 29/08/2014
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(grow.Cells(13).Value), "Virtual") = CompairStringResult.Equal Then
                        strLocationType = clsCommon.myCstr(grow.Cells(13).Value)
                    Else
                        Throw New Exception("Location Type must be either Physical/ Logical/ WorkOrder or Virtual. ")
                    End If
                    ''---------------------------------------------------------------
                    Dim strLocationStatus As String = clsCommon.myCstr(grow.Cells(14).Value)
                    If (strLocationStatus = "Y") Then
                        strLocationStatus = "Y"
                    ElseIf (strLocationStatus = "N") Then
                        strLocationStatus = "N"
                    Else
                        Throw New Exception("Location Status must be either Y or N ")
                    End If
                    Dim aa As String = grow.Cells(15).Value.ToString()
                    Dim strStatusDate As String = Format(aa, "dd/MM/yyyy")

                    If strStatusDate <> Format(Date.Today, "dd/MM/yyyy") Then

                        strStatusDate = Format(Date.Today, "dd/MM/yyyy")
                    Else
                        strStatusDate = Format(Date.Today, "dd/MM/yyyy")
                    End If
                    Dim strExcisable As String = clsCommon.myCstr(grow.Cells(16).Value)
                    If (strExcisable = "T") Then
                        If clsCommon.myLen(clsCommon.myCstr(grow.Cells("Sales Tax Group").Value)) <= 0 Then
                            Throw New Exception("Blank Sale Tax Group Is not acceptable For Excisable Locations ")
                        End If
                        If clsCommon.myLen(clsCommon.myCstr(grow.Cells("Sales Tax GroupIS").Value)) <= 0 Then
                            Throw New Exception("Blank Sales Tax GroupIS field not acceptable For Excisable Locations ")
                        End If
                        If clsCommon.myLen(clsCommon.myCstr(grow.Cells("Ecc Number").Value)) <= 0 Then
                            Throw New Exception("Blank ECC number field not acceptable For Excisable Locations ")
                        End If
                        If clsCommon.myLen(clsCommon.myCstr(grow.Cells("Ecc Number").Value)) < 15 Then
                            Throw New Exception(" ECC number field Can not be of Less than 15 character ")
                        End If
                        strExcisable = "T"
                    ElseIf (strExcisable = "F") Then
                        strExcisable = "F"
                    Else
                        Throw New Exception("Excisable must be either T or F ")
                    End If
                    Dim strLocSegCode As String = clsCommon.myCstr(grow.Cells(17).Value)
                    If clsCommon.myLen(strLocSegCode) > 12 Then
                        Throw New Exception("Segment Code Can not be greater than 12 ")
                    End If
                    Dim strtype As String = clsCommon.myCstr(grow.Cells(18).Value)
                    If (strtype = "Depot") Then
                        strtype = "Depot"
                    ElseIf (strtype = "Manufacturing Unit") Then
                        strtype = "Manufacturing Unit"
                    ElseIf (strtype = "") Then
                        strtype = ""
                    ElseIf clsCommon.CompairString(strtype, "PLANT") = CompairStringResult.Equal Then
                        strtype = "PLANT"
                    Else
                        Throw New Exception("Type must be either Depot or Manufacturing Unit Or PLANT ")
                        Exit Sub
                    End If
                    Dim strPurchaseTaxgroup As String = clsCommon.myCstr(grow.Cells(19).Value)
                    If clsCommon.myLen(strPurchaseTaxgroup) > 12 Then
                        Throw New Exception("Purchase Tax group Can not be greater than 12 ")
                    End If
                    If clsCommon.myLen(strPurchaseTaxgroup) > 0 Then
                        If clsDBFuncationality.getSingleValue(" SELECT count(*) From TSPL_TAX_GROUP_MASTER where tax_group_code='" & strPurchaseTaxgroup & "'", trans) = 0 Then
                            Throw New Exception("Invalid Purchase Tax Group")
                        End If
                    End If
                    Dim strSalesTaxgroup As String = clsCommon.myCstr(grow.Cells(20).Value)
                    If clsCommon.myLen(strSalesTaxgroup) > 12 Then
                        Throw New Exception("Sales Tax group Can not be greater than 12 ")
                    End If
                    If clsCommon.myLen(strSalesTaxgroup) > 0 Then
                        If clsDBFuncationality.getSingleValue(" SELECT count(*) From TSPL_TAX_GROUP_MASTER where tax_group_code='" & strSalesTaxgroup & "'", trans) = 0 Then
                            Throw New Exception("Invalid Sale Tax Group ")
                        End If
                    End If
                    Dim strECCNum As String = clsCommon.myCstr(grow.Cells(21).Value)
                    If strECCNum.Length > 100 Then
                        Throw New Exception("Ecc number group Can not be greater than 100 ")
                    End If
                    Dim strRegisNo As String = clsCommon.myCstr(grow.Cells(22).Value)
                    If strRegisNo.Length > 100 Then
                        Throw New Exception("Registration Number Can not be greater than 100 ")
                    End If
                    Dim strComRate As String = clsCommon.myCstr(grow.Cells(23).Value)
                    If strECCNum.Length > 100 Then
                        Throw New Exception("Ecc number group Can not be greater than 100 ")
                    End If
                    Dim strRangeCode As String = clsCommon.myCstr(grow.Cells(24).Value)
                    If strRangeCode.Length > 100 Then
                        Throw New Exception("Range Code Can not be greater than 100 ")
                    End If
                    Dim strRangeName As String = clsCommon.myCstr(grow.Cells(25).Value)
                    If strRangeName.Length > 100 Then
                        Throw New Exception("Range name group Can not be greater than 100 ")
                    End If
                    Dim strRangeAddress As String = clsCommon.myCstr(grow.Cells(26).Value)
                    If strRangeAddress.Length > 100 Then
                        Throw New Exception("Range address Can not be greater than 100 ")
                    End If
                    Dim strDivisionCode As String = clsCommon.myCstr(grow.Cells(27).Value)
                    If strDivisionCode.Length > 100 Then
                        Throw New Exception("Division Code Can not be greater than 100 ")
                    End If
                    Dim strDivisionName As String = clsCommon.myCstr(grow.Cells(28).Value)
                    If strDivisionName.Length > 100 Then
                        Throw New Exception("Division name group Can not be greater than 100 ")
                    End If
                    Dim strDivisionAddress As String = clsCommon.myCstr(grow.Cells(29).Value)
                    If strDivisionAddress.Length > 100 Then
                        Throw New Exception("Division address Can not be greater than 100 ")
                    End If
                    Dim strtinno As String = clsCommon.myCstr(grow.Cells(30).Value)
                    If strtinno.Length > 30 Then
                        Throw New Exception(" TIN No Can not be greater than 30 ")
                    End If
                    Dim strtanno As String = clsCommon.myCstr(grow.Cells(31).Value)
                    If strtanno.Length > 30 Then
                        Throw New Exception(" TAN No Can not be greater than 30 ")
                    End If
                    Dim strtcanno As String = clsCommon.myCstr(grow.Cells(32).Value)
                    If strtcanno.Length > 30 Then
                        Throw New Exception(" TCAN No Can not be greater than 30 ")
                    End If
                    Dim strServiceTaxReg As String = clsCommon.myCstr(grow.Cells(33).Value)
                    If strServiceTaxReg.Length > 30 Then
                        Throw New Exception(" Service Tax Reg No Can not be greater than 30 ")
                    End If
                    Dim strduty As String = clsCommon.myCstr(grow.Cells(34).Value)
                    If (strduty = "Y") Then
                        strduty = "Y"
                    ElseIf (strduty = "N" Or strduty = "") Then
                        strduty = "N"
                    Else
                        Throw New Exception("Excisable must be either Y or N ")
                    End If
                    Dim PurchaseTxGrpIS As String = clsCommon.myCstr(grow.Cells(35).Value)
                    If PurchaseTxGrpIS.Length > 12 Then
                        Throw New Exception(" 'Purchase Tax GroupIS' Can not be greater than 12 ")
                    End If
                    If clsCommon.myLen(PurchaseTxGrpIS) > 0 Then
                        If clsDBFuncationality.getSingleValue(" SELECT count(*) From TSPL_TAX_GROUP_MASTER where tax_group_code='" & PurchaseTxGrpIS & "'", trans) = 0 Then
                            Throw New Exception("Invalid Purchase Tax GroupIS ")
                        End If
                    End If
                    Dim SalesTxGrpIS As String = clsCommon.myCstr(grow.Cells(36).Value)
                    If SalesTxGrpIS.Length > 12 Then
                        Throw New Exception(" 'Sales Tax GroupIS' Can not be greater than 12 ")
                    End If
                    If clsCommon.myLen(SalesTxGrpIS) > 0 Then
                        If clsDBFuncationality.getSingleValue(" SELECT count(*) From TSPL_TAX_GROUP_MASTER where tax_group_code='" & SalesTxGrpIS & "'", trans) = 0 Then
                            Throw New Exception("Invalid Sale Tax GroupIS ")
                        End If
                    End If
                    Dim strStkTrnsfrFilledAc As String = clsCommon.myCstr(grow.Cells(37).Value)
                    If strStkTrnsfrFilledAc.Length > 50 Then
                        Throw New Exception(" 'Stock Transfer Filled Account' Can not be greater than 50 ")
                    End If
                    If clsCommon.myLen(strStkTrnsfrFilledAc) > 0 Then
                        If clsDBFuncationality.getSingleValue(" SELECT count(*)  FROM TSPL_GL_ACCOUNTS where Account_code='" & strStkTrnsfrFilledAc & "'", trans) = 0 Then
                            Throw New Exception("Invalid Stock Transfer filled Accoun ")
                        End If
                    End If
                    Dim strStkTrnsfrEmptyAc As String = clsCommon.myCstr(grow.Cells(38).Value)
                    If strStkTrnsfrEmptyAc.Length > 50 Then
                        Throw New Exception(" 'Stock Transfer Empty Account' Can not be greater than 50 ")
                    End If
                    If clsCommon.myLen(strStkTrnsfrEmptyAc) > 0 Then
                        If clsDBFuncationality.getSingleValue(" SELECT count(*)  FROM TSPL_GL_ACCOUNTS where Account_code='" & strStkTrnsfrEmptyAc & "'", trans) = 0 Then
                            Throw New Exception("Invalid Stock Transfer Empty Accoun ")
                        End If
                    End If
                    Dim Datee As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")

                    Dim cate_struct_code As String = ""
                    cate_struct_code = clsCommon.myCstr(grow.Cells("Category_Struct_Code").Value)

                    If clsCommon.myLen(cate_struct_code) > 0 Then
                        qry1 = "select count(*) from TSPL_ITEM_CATEGORY_STRUCTURE where item_category_struct_code='" + cate_struct_code + "' and isnull(form_type,'item')='location'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry1, trans)

                        If check <= 0 Then
                            Throw New Exception("First create Location category structure and mapped levels")
                        End If
                    End If

                    '' Anubhooti 1-Aug-2014
                    Dim strIsSection As String = clsCommon.myCstr(grow.Cells(42).Value)
                    If strIsSection.Length > 1 Then
                        Throw New Exception(" 'Is Section' length Can not be greater than 1 ")
                    End If
                    Dim strIsSubLoc As String = clsCommon.myCstr(grow.Cells(43).Value)
                    If strIsSubLoc.Length > 1 Then
                        Throw New Exception(" 'Is Sub Location' length Can not be greater than 1 ")
                    End If

                    If clsCommon.myCstr(strIsSection).ToUpper().Trim() = "Y".ToUpper().Trim() AndAlso clsCommon.myCstr(strIsSubLoc).ToUpper().Trim() = "Y".ToUpper().Trim() Then
                        Throw New Exception("Please Check ! Either Section Or Sub Location Should be 'Y'.")
                    End If

                    Dim strSection As String = clsCommon.myCstr(grow.Cells(44).Value)
                    If strSection.Length > 30 Then
                        Throw New Exception(" 'Section' length Can not be greater than 30 ")
                    End If
                    If clsCommon.myLen(strSection) > 0 Then
                        If clsDBFuncationality.getSingleValue(" SELECT count(*)  FROM TSPL_SECTION_MASTER where Section_Code='" & strSection & "'", trans) = 0 Then
                            Throw New Exception("Invalid Section.Please make it entry first. ")
                        End If
                    End If
                    Dim strMainLoc As String = clsCommon.myCstr(grow.Cells(45).Value)
                    If strMainLoc.Length > 12 Then
                        Throw New Exception(" 'Sub Location' length Can not be greater than 12 ")
                    End If
                    If clsCommon.myLen(strMainLoc) > 0 Then
                        If clsDBFuncationality.getSingleValue(" SELECT count(*)  FROM TSPL_LOCATION_MASTER where Location_Code='" & strMainLoc & "'", trans) = 0 Then
                            Throw New Exception("Invalid Main Location.Please make it entry first. ")
                        End If
                        If clsCommon.CompairString(strLocation, strMainLoc) = CompairStringResult.Equal Then
                            Throw New Exception("Location code and main location code can not be same.")
                        End If
                        If clsDBFuncationality.getSingleValue(" SELECT count(*)  FROM TSPL_LOCATION_MASTER where Location_Code='" & strMainLoc & "' AND Is_Section ='N' AND Is_Sub_Location='N' ", trans) = 0 Then
                            Throw New Exception("Please check ! main location is either section or sub location.")
                        End If
                    End If
                    If clsCommon.CompairString(strMainLoc, clsCommon.myCstr(grow.Cells(0).Value)) = CompairStringResult.Equal Then
                        Throw New Exception("Please check ! location code and main location can not be same.")
                    End If

                    If clsCommon.myCstr(strIsSection).ToUpper().Trim() = "Y".ToUpper().Trim() AndAlso (clsCommon.myLen(strMainLoc) <= 0 Or clsCommon.myLen(strSection) <= 0) Then
                        Throw New Exception("Please Check ! Section And Main Location can not be left blank When Is_Section is 'Y'.")
                    End If

                    If clsCommon.myCstr(strIsSubLoc).ToUpper().Trim() = "Y".ToUpper().Trim() AndAlso clsCommon.myLen(strMainLoc) <= 0 Then
                        Throw New Exception("Please Check ! Main Location can not be left blank When Is_Sublocation is 'Y'.")
                    End If

                    If (clsCommon.myLen(strMainLoc) > 0 AndAlso clsCommon.myLen(strSection) > 0) Then
                        If (clsCommon.myCstr(strIsSection).ToUpper().Trim() = "N".ToUpper().Trim() Or clsCommon.myCstr(strIsSection).Trim() = "") Then
                            Throw New Exception("Please Check ! Is_Section should be 'Y' when Section/Main Location is not empty.")
                        End If
                    ElseIf (clsCommon.myLen(strMainLoc) <= 0 AndAlso clsCommon.myLen(strSection) > 0) Then
                        If (clsCommon.myCstr(strIsSection).ToUpper().Trim() = "N".ToUpper().Trim() Or clsCommon.myCstr(strIsSection).Trim() = "") AndAlso (clsCommon.myCstr(strIsSubLoc).ToUpper().Trim() = "N".ToUpper().Trim() Or clsCommon.myCstr(strIsSubLoc).Trim() = "") Then
                            Throw New Exception("Please Check ! Is_Sub_Location should be 'Y' when Main Location is not empty.")
                        End If
                        'ElseIf (clsCommon.myLen(strMainLoc) > 0 AndAlso clsCommon.myLen(strSection) >= 0) Then
                        '    If (clsCommon.myCstr(strIsSection).ToUpper().Trim() = "N".ToUpper().Trim() Or clsCommon.myCstr(strIsSection).Trim() = "") Then
                        '        Throw New Exception("Please Check ! Is_Section should be 'Y' when Section is not empty.")
                        '    End If
                    End If

                    'If clsCommon.myLen(strMainLoc) > 0 AndAlso (clsCommon.myCstr(strIsSubLoc).ToUpper().Trim() = "N".ToUpper().Trim() Or clsCommon.myLen(strIsSubLoc) <= 0) Then
                    '    Throw New Exception("Please Check ! Is_Sublocation should be 'Y' when Section/Main Location is not empty.")
                    'End If
                    Dim MainLocSegment As String = ""
                    If clsCommon.myLen(strMainLoc) > 0 Then
                        MainLocSegment = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ISNULL( Loc_Segment_Code,'') As Loc_Segment_Code  from TSPL_LOCATION_MASTER Where Location_Code='" & strMainLoc & "'", trans))
                    End If

                    If clsCommon.myCstr(strIsSection).ToUpper().Trim() = "Y".ToUpper().Trim() Or clsCommon.myCstr(strIsSubLoc).ToUpper().Trim() = "Y".ToUpper().Trim() Then
                        If clsCommon.myCstr(MainLocSegment) <> clsCommon.myCstr(grow.Cells("Loc Segment Code").Value) Then
                            Throw New Exception("Please check ! Main location segment code and location segment code should be same")
                        End If
                    End If

                    If (clsCommon.myCstr(strIsSection).ToUpper().Trim() = "N".ToUpper().Trim() Or clsCommon.myLen(strIsSection) <= 0) AndAlso (clsCommon.myCstr(strIsSubLoc).ToUpper().Trim() = "N".ToUpper().Trim() Or clsCommon.myLen(strIsSubLoc) <= 0) Then
                        Section_Code = "NULL"
                        Main_Location_Code = "NULL"
                    ElseIf (clsCommon.myCstr(strIsSection).ToUpper().Trim() = "Y".ToUpper().Trim() Or clsCommon.myLen(strIsSection) <= 0) AndAlso (clsCommon.myCstr(strIsSubLoc).ToUpper().Trim() = "N".ToUpper().Trim() Or clsCommon.myLen(strIsSubLoc) <= 0) Then
                        Section_Code = "'" & strSection & "'"
                        Main_Location_Code = "'" & strMainLoc & "'"
                    ElseIf (clsCommon.myCstr(strIsSection).ToUpper().Trim() = "N".ToUpper().Trim() Or clsCommon.myLen(strIsSection) <= 0) AndAlso (clsCommon.myCstr(strIsSubLoc).ToUpper().Trim() = "Y".ToUpper().Trim() Or clsCommon.myLen(strIsSubLoc) <= 0) Then
                        Section_Code = "NULL"
                        Main_Location_Code = "'" & strMainLoc & "'"
                    Else
                        Section_Code = "'" & strSection & "'"
                        Main_Location_Code = "'" & strMainLoc & "'"
                    End If

                    is_consumption = clsCommon.myCstr(grow.Cells("is_consumption_location").Value)
                    If clsCommon.myLen(is_consumption) > 0 AndAlso clsCommon.CompairString(strIsSection, "Y") = CompairStringResult.Equal AndAlso clsCommon.CompairString(is_consumption, "0") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(is_consumption, "1") <> CompairStringResult.Equal Then
                        Throw New Exception("Fill 0 or 1 in is_consumption_location column for section " + strSection + ".")
                    End If
                    If clsCommon.CompairString(strIsSection, "Y") <> CompairStringResult.Equal OrElse (clsCommon.CompairString(is_consumption, "0") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(is_consumption, "1") <> CompairStringResult.Equal) Then
                        is_consumption = "0"
                    End If
                    ''
                    '' ESIC No 
                    Dim strESICNo As String = clsCommon.myCstr(grow.Cells("ESIC No").Value)
                    If strESICNo.Length > 30 Then
                        Throw New Exception(" 'ESIC No' length Can not be greater than 30 ")
                    End If
                    '' PF No 
                    Dim strPFNo As String = clsCommon.myCstr(grow.Cells("PF No").Value)
                    If strPFNo.Length > 30 Then
                        Throw New Exception(" 'PF No' length Can not be greater than 30 ")
                    End If
                    If Not IsNumeric(grow.Cells(49).Value) Then
                        Throw New Exception("Char value not allowed in DairyDispatchFromDO. ")
                    Else
                        DairyDispatchFromDO = clsCommon.myCdbl(grow.Cells(49).Value)
                    End If

                    ''==============================================================================================
                    strLocShortName = clsCommon.myCstr(grow.Cells("Loc_Short_Name").Value).Replace("'", "`")

                    If clsCommon.myLen(strLocShortName) <= 0 Then
                        strLocShortName = strLDesc
                        If clsCommon.myLen(strLocShortName) > 50 Then
                            strLocShortName = strLocShortName.Substring(0, 50)
                        End If
                    End If

                    If clsCommon.myLen(strLocShortName) > 50 Then
                        Throw New Exception(" Location Short Name length Can not be greater than 50 ")
                    End If

                    Dim strIsSubLocationWise As String = clsCommon.myCstr(grow.Cells(54).Value)
                    If strIsSubLocationWise.Length > 1 Then
                        Throw New Exception(" 'Is Sub Location Wise' length Can not be greater than 1 ")
                    End If

                    Dim IsMainPlant As Integer = 0
                    If Not IsNumeric(grow.Cells("IsMainPlant").Value) Then
                        Throw New Exception("Char value not allowed in IsMainPlant. ")
                    Else
                        IsMainPlant = clsCommon.myCdbl(grow.Cells("IsMainPlant").Value)
                    End If
                    '=============Added by Preeti Gupta================

                    If objCommonVar.GSTApplicable Then
                        GSTNo = clsCommon.myCstr(grow.Cells("GSTNo").Value)
                        Dim GSTState As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select tspl_state_master.GST_STATE_Code  from tspl_state_master where STATE_CODE ='" + strStateProvince + "'", trans))
                        Dim CompanyPan As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select tspl_company_master.Pan_No from tspl_company_master where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'", trans))
                        If clsCommon.myLen(GSTNo) > 0 Then
                            clsERPFuncationality.ValidationGSTNO(GSTState, CompanyPan, GSTNo, trans)
                            GSTEntity = clsCommon.myCstr(GSTNo.Trim().Substring(12, 1))
                            GSTBlank = clsCommon.myCstr(GSTNo.Trim().Substring(13, 1))
                            GSTdigit = clsCommon.myCstr(GSTNo.Trim().Substring(14, 1))
                        End If

                    End If

                    ''==============================================================================================


                    '' Anubhooti 1-Aug-2014 BM00000003362(Resolved Err While Exporting)
                    Dim sql1 As String = "select COUNT(*) from TSPL_LOCATION_MASTER  where Location_Code='" + strLocation + "'"
                    i = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(sql1, trans))

                    If i > 0 Then
                        If intShowOptionofDispatchFromDOGP = 1 Then
                            Dim strDispatchRef As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select DairyDispatchFromDO from TSPL_LOCATION_MASTER where Location_Code='" & strLocation & "'", trans))
                            If strDispatchRef = 1 AndAlso DairyDispatchFromDO = 0 Then
                                Dim strCode = clsDBFuncationality.getSingleValue("select  top 1 TSPL_SD_SHIPMENT_HEAD.DOCUMENT_CODE from TSPL_SD_SHIPMENT_HEAD left outer join  TSPL_SD_SHIPMENT_DETAIL on " &
                                 "TSPL_SD_SHIPMENT_HEAD.DOCUMENT_CODE=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE  where Delivery_Code <> '' and Bill_To_Location='" & strLocation & "' ", trans)
                                If clsCommon.myLen(strCode) > 0 Then
                                    Throw New Exception("Dispatch From DO, Cannot change this setting. Location is already in use")
                                End If
                            ElseIf strDispatchRef = 0 AndAlso DairyDispatchFromDO = 1 Then
                                Dim strCode = clsDBFuncationality.getSingleValue("select top 1 TSPL_SD_SHIPMENT_HEAD.DOCUMENT_CODE from TSPL_SD_SHIPMENT_HEAD left outer join  TSPL_SD_SHIPMENT_DETAIL on " &
                                 "TSPL_SD_SHIPMENT_HEAD.DOCUMENT_CODE=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE where GatePass_No <> '' and Bill_To_Location='" & strLocation & "'", trans)
                                If clsCommon.myLen(strCode) > 0 Then
                                    Throw New Exception("Dispatch From Gatepass, Cannot change setting. Location is already in use")
                                End If
                            End If
                        End If
                    End If


                    If i = 0 Then
                        sql1 = "insert into tspl_location_master(Location_code,Location_desc,add1,add2,add3,add4,city_code,state,pin_code,country,telphone,email,location_type,loc_status,status_date,excisable,loc_segment_code,type,purchase_tax_group,sales_tax_group,ecc_number,registration_number,commissionerate,range_code,range_name,range_address,division_code,division_name,division_address,created_by,created_date,tin_no,tan_no,tcan_no,service_tax_reg_no,dutypaid,purchase_tax_groupIs,sales_tax_groupIs,stock_transfer_filled_ac,stock_transfer_empty_ac,modify_by,modify_date,comp_code,vendor_code,location_category,Category_Struct_Code,Is_Section,Is_Sub_Location,Section_Code,Main_Location_Code,is_consumption_location,ESIC_NO,PF_NO,DairyDispatchFromDO,Loc_Short_Name,GSTNo,GSTEntity,gstblank,GSTDegit,Is_Insurance,InsuranceNo,InsuranceFromDate,InsuranceToDate,IsSubLocationWise,IsMainPlant ) values("
                        sql1 = sql1 & "'" & strLocation & "',"
                        sql1 = sql1 & "'" & strLDesc & "',"
                        sql1 = sql1 & "'" & strAdd1 & "',"
                        sql1 = sql1 & "'" & strAdd2 & "',"
                        sql1 = sql1 & "'" & strAdd3 & "',"
                        sql1 = sql1 & "'" & strAdd4 & "',"
                        sql1 = sql1 & "'" & strCity & "',"
                        sql1 = sql1 & "'" & strStateProvince & "',"
                        sql1 = sql1 & "'" & strPostalCode & "',"
                        sql1 = sql1 & "'" & strCountry & "',"
                        sql1 = sql1 & "'" & strTelephone & "',"
                        sql1 = sql1 & "'" & strEmail & "',"
                        sql1 = sql1 & "'" & strLocationType & "',"
                        sql1 = sql1 & "'" & strLocationStatus & "',"
                        sql1 = sql1 & "'" & strStatusDate & "',"
                        sql1 = sql1 & "'" & strExcisable & "',"
                        sql1 = sql1 & "'" & strLocSegCode & "',"
                        sql1 = sql1 & "'" & strtype & "',"
                        sql1 = sql1 & "'" & strPurchaseTaxgroup & "',"
                        sql1 = sql1 & "'" & strSalesTaxgroup & "',"
                        sql1 = sql1 & "'" & strECCNum & "',"
                        sql1 = sql1 & "'" & strRegisNo & "',"
                        sql1 = sql1 & "'" & strComRate & "',"
                        sql1 = sql1 & "'" & strRangeCode & "',"
                        sql1 = sql1 & "'" & strRangeName & "',"
                        sql1 = sql1 & "'" & strRangeAddress & "',"
                        sql1 = sql1 & "'" & strDivisionCode & "',"
                        sql1 = sql1 & "'" & strDivisionName & "',"
                        sql1 = sql1 & "'" & strDivisionAddress & "',"
                        sql1 = sql1 & "'" & userCode & "',"
                        sql1 = sql1 & "'" & Datee & "',"
                        sql1 = sql1 & "'" & strtinno & "',"
                        sql1 = sql1 & "'" & strtanno & "',"
                        sql1 = sql1 & "'" & strtcanno & "',"
                        sql1 = sql1 & "'" & strServiceTaxReg & "',"
                        sql1 = sql1 & "'" & strduty & "',"
                        sql1 = sql1 & "'" & strPurchaseTaxgroup & "',"
                        sql1 = sql1 & "'" & strSalesTaxgroup & "',"
                        sql1 = sql1 & "'" & strStkTrnsfrFilledAc & "',"
                        sql1 = sql1 & "'" & strStkTrnsfrEmptyAc & "',"
                        sql1 = sql1 & "'" & userCode & "',"
                        sql1 = sql1 & "'" & Datee & "','','" & thirdparty & "','" & strloc_category & "','" & cate_struct_code & "','" & strIsSection & "','" & strIsSubLoc & "'," & Section_Code & "," + Main_Location_Code + ",'" + is_consumption + "','" + strESICNo + "','" + strPFNo + "'," & DairyDispatchFromDO & ",'" + strLocShortName + "','" + GSTNo + "','" + GSTEntity + "','" + GSTBlank + "','" + GSTdigit + "',"
                        sql1 = sql1 & "'" & Is_Insurance & "',"
                        If Is_Insurance = 1 Then
                            sql1 = sql1 & "'" & InsuranceNo & "',"
                            sql1 = sql1 & " convert(date,'" & InsuranceFromDate & "',103) ,"
                            sql1 = sql1 & " convert(date,'" & InsuranceToDate & "',103),"
                        Else
                            sql1 = sql1 & "NULL,"
                            sql1 = sql1 & "NULL,"
                            sql1 = sql1 & "NULL,"
                        End If
                        sql1 = sql1 & "'" & strIsSubLocationWise & "',"
                        sql1 = sql1 & "'" & IsMainPlant & "'"
                        sql1 = sql1 & ")"
                    Else
                        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strLocation, "TSPL_LOCATION_MASTER", "Location_Code", trans)
                        sql1 = "update tspl_location_master set "
                        sql1 = sql1 & "Location_desc='" & strLDesc & "', "
                        sql1 = sql1 & "add1='" & strAdd1 & "', "
                        sql1 = sql1 & "add2='" & strAdd2 & "', "
                        sql1 = sql1 & "add3='" & strAdd3 & "', "
                        sql1 = sql1 & "add4='" & strAdd4 & "', "
                        sql1 = sql1 & "city_code='" & strCity & "', "
                        sql1 = sql1 & "state='" & strStateProvince & "', "
                        sql1 = sql1 & "pin_code='" & strPostalCode & "', "
                        sql1 = sql1 & "country='" & strCountry & "', "
                        sql1 = sql1 & "telphone='" & strTelephone & "', "
                        sql1 = sql1 & "email='" & strEmail & "', "
                        sql1 = sql1 & "location_type='" & strLocationType & "', "
                        sql1 = sql1 & "loc_status='" & strLocationStatus & "', "
                        sql1 = sql1 & "status_date='" & strStatusDate & "', "
                        sql1 = sql1 & "excisable='" & strExcisable & "', "
                        sql1 = sql1 & "loc_segment_code='" & strLocSegCode & "', "
                        sql1 = sql1 & "type='" & strtype & "', "
                        sql1 = sql1 & "purchase_tax_group='" & strPurchaseTaxgroup & "', "
                        sql1 = sql1 & "sales_tax_group='" & strSalesTaxgroup & "', "
                        sql1 = sql1 & "ecc_number='" & strECCNum & "', "
                        sql1 = sql1 & "registration_number='" & strRegisNo & "', "
                        sql1 = sql1 & "commissionerate='" & strComRate & "', "
                        sql1 = sql1 & "range_code='" & strRangeCode & "', "
                        sql1 = sql1 & "range_name='" & strRangeName & "', "
                        sql1 = sql1 & "range_address='" & strRangeAddress & "', "
                        sql1 = sql1 & "division_code='" & strDivisionCode & "', "
                        sql1 = sql1 & "division_name='" & strDivisionName & "', "
                        sql1 = sql1 & "division_address='" & strDivisionAddress & "', "
                        sql1 = sql1 & "Modify_by='" & userCode & "', "
                        sql1 = sql1 & "modify_date='" & Datee & "', "
                        sql1 = sql1 & "tin_no='" & strtinno & "', "
                        sql1 = sql1 & "tan_no='" & strtanno & "', "
                        sql1 = sql1 & "tcan_no='" & strtcanno & "', "
                        sql1 = sql1 & "service_tax_reg_no='" & strServiceTaxReg & "', "
                        sql1 = sql1 & "dutypaid='" & strduty & "', "
                        sql1 = sql1 & "purchase_tax_groupIs='" & PurchaseTxGrpIS & "', "
                        sql1 = sql1 & "sales_tax_groupIs='" & SalesTxGrpIS & "', "
                        sql1 = sql1 & "stock_transfer_filled_ac='" & strStkTrnsfrFilledAc & "', "

                        sql1 = sql1 & "Is_Section='" & strIsSection & "', "
                        sql1 = sql1 & "Is_Sub_Location='" & strIsSubLoc & "', "
                        sql1 = sql1 & "Section_Code=" & Section_Code & ", "
                        sql1 = sql1 & "Main_Location_Code=" & Main_Location_Code & ", "

                        sql1 = sql1 & "stock_transfer_empty_ac='" & strStkTrnsfrEmptyAc & "',vendor_code='" & thirdparty & "',location_category='" & strloc_category & "',Category_Struct_Code='" + cate_struct_code + "',is_consumption_location='" + is_consumption + "',ESIC_NO='" & strESICNo & "',PF_NO='" & strPFNo & "',DairyDispatchFromDO=" & DairyDispatchFromDO & ",Loc_Short_Name='" + strLocShortName + "',GSTNo='" + GSTNo + "',GSTEntity='" + GSTEntity + "',gstblank='" + GSTBlank + "',GSTDegit='" + GSTdigit + "',"
                        sql1 = sql1 & "Is_Insurance='" & Is_Insurance & "',"
                        If Is_Insurance = 1 Then
                            sql1 = sql1 & "InsuranceNo='" & InsuranceNo & "',"
                            sql1 = sql1 & "InsuranceFromDate=convert(date,'" & InsuranceFromDate & "',103),"
                            sql1 = sql1 & "InsuranceToDate=convert(date,'" & InsuranceToDate & "',103),"
                        Else
                            sql1 = sql1 & "InsuranceNo=NULL,"
                            sql1 = sql1 & "InsuranceFromDate=NULL,"
                            sql1 = sql1 & "InsuranceToDate=NULL,"
                        End If
                        sql1 = sql1 & "IsSubLocationWise='" & strIsSubLocationWise & "',"
                        sql1 = sql1 & "IsMainPlant='" & IsMainPlant & "'"
                        sql1 = sql1 & " where location_code='" & strLocation & "'"
                    End If
                    clsDBFuncationality.ExecuteNonQuery(sql1, trans)
                    ''''''''''''''  retrieving associated custom field data from shee for table
                    Dim j As Integer = 38
                    For Each row As DataRow In drr.Rows
                        sql1 = "select COUNT(*) from TSPL_CUSTOM_FIELD_VALUES  where transaction_Code='" + strLocation + "' and program_code='" & MyBase.Form_ID & "' and custom_field_code='" & row(1).ToString & "'"
                        i = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(sql1, trans))
                        If i = 0 Then
                            sql1 = "insert into tspl_custom_field_values values('" & MyBase.Form_ID & "','" & strLocation & "','" & row(1).ToString & "','" & grow.Cells(j).Value & "','0')"
                        Else
                            sql1 = "update tspl_custom_field_values set value='" & grow.Cells(j).Value & "' where transaction_Code='" + strLocation + "' and program_code='" & MyBase.Form_ID & "' and custom_field_code='" & row(1).ToString & "'"
                        End If
                        clsDBFuncationality.ExecuteNonQuery(sql1, trans)
                        j = j + 1
                    Next
                    ''''''''''''''''End of custom field data retrieval
                    linno = linno + 1
                Next
                '' Anubhooti 31-Oct-2014
                Dim DuplicateEntry As String = ""
                DuplicateEntry = " select Location_Code , SUM(1) as Repeated from TSPL_LOCATION_MASTER group by Location_Code,Is_Section,Is_Sub_Location  having SUM(1) > 1 and Is_Section='N'  AND Is_Sub_Location ='N'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(DuplicateEntry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Throw New Exception("Please check ! location (" & clsCommon.myCstr(dt.Rows(0)("Location_Code")) & ")  repeated " & clsCommon.myCstr(dt.Rows(0)("Repeated")) & " times as section or sub location.")
                End If
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow("Line-" & linno & " : " & ex.Message)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub btnimportlocationmap_Click(sender As Object, e As EventArgs) Handles btnimportlocationmap.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Plant Location Code", "Depot Location Code") Then
            Try
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New clsLocationPlantDepotMapping()


                    Dim strPlantLocationCode As String = clsCommon.myCstr(grow.Cells("Plant Location Code").Value)
                    If clsCommon.CompairString(clsCommon.myCstr(strPlantLocationCode), "") <> CompairStringResult.Equal Then
                        Dim dt As DataTable
                        dt = clsDBFuncationality.GetDataTable("select * from TSPL_LOCATION_MASTER where LOCATION_CODE='" + clsCommon.myCstr(strPlantLocationCode) + "'")
                        If dt.Rows.Count <= 0 Then
                            Throw New Exception("Plant Location Code not exists in location master.")
                        Else
                            Dim strcode1 = clsDBFuncationality.getSingleValue("select Location_Code as [Code] ,Location_Desc as [Name] from TSPL_LOCATION_MASTER where Type ='Plant' and ((GIT_Type='N') or ISNULL(GIT_Type,'')='') and ((CSA_Type='N') or ISNULL(CSA_Type,'')='') and ((Is_Section='N') or ISNULL(Is_Section,'')='') and ((Is_Sub_Location = 'N') or ISNULL(Is_Sub_Location,'')='') and location_code='" + strPlantLocationCode + "'")
                            If clsCommon.myLen(strcode1) > 0 Then
                                obj.Plant_Location_Code = strPlantLocationCode
                            Else
                                Throw New Exception("Plant type location should not be GIT,CSA,SECTION and Sub Location.")
                            End If

                        End If
                    End If

                    Dim strDepotLocationCode As String = clsCommon.myCstr(grow.Cells("Depot Location Code").Value)
                    If clsCommon.CompairString(clsCommon.myCstr(strDepotLocationCode), "") <> CompairStringResult.Equal Then
                        Dim dt As DataTable
                        dt = clsDBFuncationality.GetDataTable("select * from TSPL_LOCATION_MASTER where LOCATION_CODE='" + clsCommon.myCstr(strDepotLocationCode) + "'")
                        If dt.Rows.Count <= 0 Then
                            Throw New Exception("Depot Location Code not exists in location master.")
                        Else
                            Dim strcode = clsDBFuncationality.getSingleValue("select depot_location_code from tspl_location_plantdepot_detail where depot_location_code='" + strDepotLocationCode + "' and plant_location_code<>'" + strPlantLocationCode + "'")
                            If clsCommon.myLen(strcode) > 0 Then
                                Throw New Exception("Depot type location already mapped with another plant type")
                            Else
                                Dim strcode1 = clsDBFuncationality.getSingleValue("select Location_Code as [Code] ,Location_Desc as [Name] from TSPL_LOCATION_MASTER where Type ='Depot' and ((GIT_Type='N') or ISNULL(GIT_Type,'')='') and ((CSA_Type='N') or ISNULL(CSA_Type,'')='') and ((Is_Section='N') or ISNULL(Is_Section,'')='') and ((Is_Sub_Location = 'N') or ISNULL(Is_Sub_Location,'')='') and location_code='" + strDepotLocationCode + "'")
                                If clsCommon.myLen(strcode1) > 0 Then

                                    obj.Depot_Location_Code = strDepotLocationCode
                                Else

                                    Throw New Exception("Depot type location should not be GIT,CSA,SECTION and Sub Location.")
                                End If

                            End If

                        End If
                    End If



                    Dim qry As String = Nothing
                    qry = "Delete from tspl_location_plantdepot_detail where plant_location_code='" + obj.Plant_Location_Code + "' and depot_location_code='" + obj.Depot_Location_Code + "'"

                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Plant_Location_Code", obj.Plant_Location_Code, True)
                    clsCommon.AddColumnsForChange(coll, "Depot_Location_Code", obj.Depot_Location_Code, True)
                    clsCommonFunctionality.UpdateDataTable(coll, "tspl_location_plantdepot_detail", OMInsertOrUpdate.Insert, "")

                Next
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub btnexportlocation_Click(sender As Object, e As EventArgs) Handles btnexportlocation.Click
        'Dim path As String

        'sql = "SELECT  [Location_Code],[Location_Desc] ,[Add1],[Add2],[Add3],[Add4],[City_Code],[State],[Pin_Code],[Country],[Telphone],[Email]," & _
        '   "[Location_Type],[Loc_Status],[Status_Date],[Excisable],Loc_Segment_Code AS [Loc Segment Code],Type,Purchase_Tax_Group AS [Purchase Tax Group],Sales_Tax_Group AS [Sales Tax Group], Ecc_Number AS [Ecc Number],Registration_Number AS [Registration Number],Commissionerate,Range_Code AS [Range Code],Range_Name AS [Range Name],Range_Address AS [Range Address],Division_Code AS [Division Code],Division_Name AS [Division Name],Division_Address AS [Division Address],TIN_NO as [TIN No],tan_no as [TAN No],tcan_no as [TCAN No],service_tax_reg_no as [Service Tax Reg No],DutyPaid as [Duty Paid], Purchase_Tax_GroupIS as [Purchase Tax GroupIS], Sales_Tax_GroupIS as [Sales Tax GroupIS], Stock_Transfer_Filled_Ac as [Stock Transfer Filled Account], Stock_Transfer_Empty_Ac as [Stock Transfer Empty Account],vendor_code as [Third Party Location Vendor] FROM TSPL_LOCATION_MASTER "
        '' Anubhooti 30-July-2014 
        sql = " SELECT TSPL_LOCATION_MASTER.Location_Code,TSPL_LOCATION_MASTER.Location_Desc,tspl_location_master.Loc_Short_Name ,TSPL_LOCATION_MASTER.Add1 ,TSPL_LOCATION_MASTER.Add2,TSPL_LOCATION_MASTER.Add3,TSPL_LOCATION_MASTER.Add4,TSPL_LOCATION_MASTER.City_Code,TSPL_LOCATION_MASTER.State,TSPL_LOCATION_MASTER.Pin_Code,TSPL_LOCATION_MASTER.Country,TSPL_LOCATION_MASTER.Telphone,TSPL_LOCATION_MASTER.Email,TSPL_LOCATION_MASTER.Location_Type, case when  TSPL_LOCATION_MASTER.Loc_Status is null then 'N' when TSPL_LOCATION_MASTER.Loc_Status ='' then 'N'  else TSPL_LOCATION_MASTER.Loc_Status end Loc_Status ,TSPL_LOCATION_MASTER.Status_Date,TSPL_LOCATION_MASTER.Excisable,TSPL_LOCATION_MASTER.Loc_Segment_Code AS [Loc Segment Code],TSPL_LOCATION_MASTER.Type,TSPL_LOCATION_MASTER.Purchase_Tax_Group AS [Purchase Tax Group],TSPL_LOCATION_MASTER.Sales_Tax_Group AS [Sales Tax Group],TSPL_LOCATION_MASTER. Ecc_Number AS [Ecc Number],TSPL_LOCATION_MASTER.Registration_Number AS [Registration Number],TSPL_LOCATION_MASTER.Commissionerate,TSPL_LOCATION_MASTER.Range_Code AS [Range Code],TSPL_LOCATION_MASTER.Range_Name AS [Range Name],TSPL_LOCATION_MASTER.Range_Address AS [Range Address],TSPL_LOCATION_MASTER.Division_Code AS [Division Code],TSPL_LOCATION_MASTER.Division_Name AS [Division Name],TSPL_LOCATION_MASTER.Division_Address AS [Division Address],TSPL_LOCATION_MASTER.TIN_No as [TIN No],TSPL_LOCATION_MASTER.TAN_No as [TAN No],TSPL_LOCATION_MASTER.TCAN_No as [TCAN No],TSPL_LOCATION_MASTER.Service_Tax_Reg_No as [Service Tax Reg No],TSPL_LOCATION_MASTER.DutyPaid as [Duty Paid], TSPL_LOCATION_MASTER.Purchase_Tax_GroupIS as [Purchase Tax GroupIS], TSPL_LOCATION_MASTER.Sales_Tax_GroupIS as [Sales Tax GroupIS],TSPL_LOCATION_MASTER.Stock_Transfer_Filled_Ac as [Stock Transfer Filled Account],TSPL_LOCATION_MASTER. Stock_Transfer_Empty_Ac as [Stock Transfer Empty Account],TSPL_LOCATION_MASTER.vendor_code as [Third Party Location Vendor],TSPL_LOCATION_MASTER.Location_Category as [Location Category HO-MCC],Category_Struct_Code,Is_Section,Is_Sub_Location,CASE When Is_Section ='Y' THEN  Section_Code else '' end AS [Section Code],CASE When Is_Section  ='Y' OR Is_Sub_Location  ='Y' THEN  Main_Location_Code  else '' end AS [Main Location Code],tspl_location_master.is_consumption_location,ISNULL(ESIC_NO,'') AS [ESIC No],ISNULL(PF_NO,'') AS [PF No],isnull(tspl_location_master.DairyDispatchFromDO ,0) as DairyDispatchFromDO"
        sql += ",isnull(TSPL_LOCATION_MASTER.Is_Insurance,0) as Is_Insurance,TSPL_LOCATION_MASTER.InsuranceNo,TSPL_LOCATION_MASTER.InsuranceFromDate,TSPL_LOCATION_MASTER.InsuranceToDate,TSPL_LOCATION_MASTER.IsSubLocationWise,isnull(TSPL_LOCATION_MASTER.IsMainPlant,0) as IsMainPlant"
        If objCommonVar.GSTApplicable Then
            sql += "  ,tspl_location_master.GSTNo as GSTNo "
        End If
        sql += "  FROM TSPL_LOCATION_MASTER "

        '''''''''''' exporting data with custom field

        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            transportSql.ExporttoExcelWithCustomField(sql, "", "", Me, MyBase.Form_ID, "Location_Code")
        Else
            ListImpExpColumnsMandatory = New List(Of String)({"InsuranceNo", "Location_Code", "State", "Loc Segment Code", "Loc_Status", "Sales Tax Group", "Sales Tax GroupIS", "Ecc Number"})
            ListImpExpColumnsSuperMandatory = New List(Of String)({"Location_Code"})
            transportSql.ExporttoExcel(sql, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)
        End If
    End Sub

    Private Sub btnexportlocationmap_Click(sender As Object, e As EventArgs) Handles btnexportlocationmap.Click
        Dim qry As String = Nothing
        qry = "Select plant_location_code as [Plant Location Code],depot_location_code as [Depot Location Code] from tspl_location_plantdepot_detail"
        transportSql.ExporttoExcel(qry, Me)
    End Sub
    Sub checkLocalorInterstate(ByVal TaxGroup As String, ByVal Tax_Group_Type As String)
        Dim qry As String = "select TSPL_TAX_GROUP_MASTER.Tax_Group_Code ,TSPL_TAX_GROUP_DETAILS.Tax_Code,TSPL_TAX_MASTER.Type ,case when TSPL_TAX_MASTER.Type ='Y' then 'IGST' " &
                            " else case when TSPL_TAX_MASTER.Type='X' then 'SGST'" &
                             " else case when TSPL_TAX_MASTER.Type='Z' then 'CGST'" &
                             " else case when TSPL_TAX_MASTER.Type='B' then 'UGST'" &
                             " end end end end  as typeName  from TSPL_TAX_GROUP_MASTER" &
                             " left join  TSPL_TAX_GROUP_DETAILS on TSPL_TAX_GROUP_DETAILS.Tax_Group_Code =TSPL_TAX_GROUP_MASTER.Tax_Group_Code " &
                             " left join TSPL_TAX_MASTER on TSPL_TAX_MASTER.Tax_Code =TSPL_TAX_GROUP_DETAILS.Tax_Code " &
                            " where TSPL_TAX_GROUP_MASTER.Tax_Group_Code='" + TaxGroup + "' and TSPL_TAX_GROUP_MASTER.Tax_Group_Type ='" + Tax_Group_Type + "' and TSPL_TAX_MASTER.GSTActive =1 " &
                            " and  TSPL_TAX_GROUP_DETAILS.Tax_Group_Type  ='" + Tax_Group_Type + "' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)



    End Sub

    Private Sub ddlLocationType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles ddlLocationType.SelectedIndexChanged

        If clsCommon.CompairString(ddlLocationType.Text, "Virtual") = CompairStringResult.Equal Then
            chkUseInJobWork.Visible = True
        Else
            chkUseInJobWork.Checked = False
            chkUseInJobWork.Visible = False
        End If
    End Sub

    Private Sub ChkIsJobwork_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles ChkIsJobwork.ToggleStateChanged
        ChkIsJobWorkVisible()
    End Sub

    Private Sub chkUseInJobWork_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkUseInJobWork.ToggleStateChanged
        ChkIsJobWorkVisible()
    End Sub
    Sub ChkIsJobWorkVisible()
        If clsCommon.CompairString(ddlLocationType.Text, "Virtual") = CompairStringResult.Equal AndAlso chkUseInJobWork.Checked = True Then
            ChkIsJobwork.Enabled = False
            ChkIsJobwork.Checked = False
            'FndJobworkVendor.Value = ""
            'gvItem.DataSource = Nothing
        Else
            ChkIsJobwork.Enabled = True
            'ChkIsJobwork.Checked = True

        End If
    End Sub
    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(fndLocation.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Select Location")
                Exit Sub
            End If
            clsERPFuncationalityOLD.ShowHistoryData(fndLocation.Value, "Location_Code", "TSPL_Location_MASTER")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub
    Private Sub txtPANNo_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtPANNo.Validating
        If clsCommon.myLen(txtPANNo.Text) > 0 Then
            If clsCommon.myLen(txtPANNo.Text) < 10 Then
                clsCommon.MyMessageBoxShow(Me, "PAN number should have max. 10 length.", Me.Text)
                txtPANNo.Focus()
                txtPANNo.Select()
                Return
            End If
            Dim panNumber As String = txtPANNo.Text ' Assuming txtPANNo.Text contains the PAN number.
            Dim checkPan As New System.Text.RegularExpressions.Regex("^([A-Z]){5}([0-9]){4}([A-Z]){1}?$")
            If checkPan.IsMatch(panNumber) Then
                txtGSTPANNO.Text = txtPANNo.Text
            Else
                clsCommon.MyMessageBoxShow(Me, "Please enter valid PAN No.", Me.Text)
            End If
        End If
    End Sub
    Private Sub chkInsurance_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkInsurance.ToggleStateChanged
        If chkInsurance.Checked = False Then
            txtFromDate.Value = clsCommon.GETSERVERDATE
            txtToDate.Value = clsCommon.GETSERVERDATE
            txtInsurance.Text = ""
            RadGroupBoxInsurance.Enabled = False
        Else
            RadGroupBoxInsurance.Enabled = True
        End If
    End Sub
End Class
