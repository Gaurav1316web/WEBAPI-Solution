Imports common
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Net
Imports System.IO
Imports System.Globalization
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Imports XpertERPEngine
Imports System
''Checkin sanjay 22/06/2020
Public Class frmCashIndentBookingMobApp
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim FlagFirstRecord As Boolean = False
    Dim RecordCount As Integer = 0
    Dim FlagCreateDo As Boolean = False
    Dim TotalQuantity As Integer = 0
    Dim TotalCan As Integer = 0
    Dim TotalBox As Integer = 0
    Dim TotalCrate As Integer = 0
    Dim DOmsg As String = ""
    Private DOCreated As Boolean = False
    Dim blnSaveTotalQTy As Boolean = False
    Dim CheckOutstandingOnbooking As Integer = 0
    Dim dblNetAmt As Decimal = 0
    Dim GSTStatus As Boolean = False
    Dim AllowFreshPriceChartonProductSale As Integer = 0
    Dim AllowFreshPriceChartonBookingProductSale As Integer = 0
    Public PrintTruckSheetAfterGenerate As Boolean = False
    Dim vaddnew As String = "Y"
    Dim attachqry As String = ""
    Private StrSql As String
    Public StrDocNo As String
    Public strExcise As Boolean
    Dim blnPageLoad As Boolean = False
    Dim CreateCommonDairyDispatchforFreshAmbient As Integer = 0
    Private PurchaseOneItemOneVendor As Boolean = False
    Private blnBackCalculation As Boolean = False
    Private IsBatchMFDEXDmandatory As Boolean = False
    Private AutoScheme As Boolean = False
    Private ItemRateEditable As Boolean = False
    Private ItemMRPEditable As Boolean = False
    Const colOrgUnit As String = "COLORGUNIT"
    Public intMRPwithabatement As Integer
    Private intApprovel_Required As Integer = 0
    Public Const RowTypeItem As String = "Item"
    Public Const RowTypeMisc As String = "Misc"
    Private isCellValueChangedOpen As Boolean = False
    Dim iStxtTaxGroup_TxtChangedComplete As Boolean = True
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Private isPageLoadData As Boolean = False
    Dim AllowWo_Outstanding As Boolean
    Dim EnableCustomerPODetailonDairyBooking As Integer = 0
    Dim CheckAvgQtyOnDairyBooking As Boolean = False
    Const colLineNo As String = "COLLNO"
    Const colICode As String = "COLICODE"
    Const colIName As String = "COLINAME"
    Const colIShortName As String = "COLISHORTNAME"
    Const colIHSN As String = "colIHSN"
    Const ColAvailableQty As String = "ColAvailableQty"
    Const colPreviousQty As String = "colPreviousQty"
    Const ColAvgQty As String = "ColAvgQty"
    Const colQty As String = "COLQTY"
    Const colUnit As String = "COLUNIT"
    Const colRate As String = "COLRATE"

    Const colAmt As String = "COLAMT"

    Const colDisc_Scheme_Type As String = "colDisc_Scheme_Type"
    Const colDisc_Scheme_Code As String = "colDisc_Scheme_Code"
    Const colDisc_Scheme_Pers As String = "colDisc_Scheme_Pers"
    Const colDisc_Scheme_Amount As String = "colDisc_Scheme_Amount"

    Const colSellingRate As String = "colSellingRate"
    Const colOrgRate As String = "colOrgRate"
    Const colTBaseAmt As String = "TAXBASEAMT"
    Const colTTaxAmt As String = "TAXAMT"
    Const colSchemeType As String = "colSchemeType"
    Const colTax_NonTax As String = "colTax_NonTax"
    Const colFreshAmbient As String = "colFreshAmbient"
    Const colRemarks As String = "colRemarks"
    Const colItemBasicPrice As String = "colItemBasicPrice"
    Const colAmountWithTax As String = "colAmountWithTax"
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Dim repoBalQty As GridViewDecimalColumn
    Dim repoComplete As GridViewTextBoxColumn
    Dim strVehicleCode As String = ""
    Dim strVehicleDesc As String = ""
    Dim strRoutecode As String = ""
    Dim strRouteDesc As String = ""
    Dim Price_code As String = ""
    Dim dblCustOutstandingAmt As Double = 0
    Dim DOStatus As Integer = 0
    Dim BookingStatus As String = 0
    Dim Is_Cancelled As Integer = 0
    Dim DoNotConsiderCustomerCreditLimit As Integer = 0
    Dim SettTagMultipleRouteWithCustomer As Boolean = False
    Dim SettDairyBookingTolleranceQty As Decimal = 0.0
    Private DonotAllowtoChangeUOMinDairyBookingCustomer As Boolean = False
    Private ShowBookingTypeDropDownonDairyBookingCustomer As Boolean = False
    Dim ShowMulMRPOfSameItemOnDairyBookingCustomer As Boolean = False
    Dim AllowZeroQtyOnDairyBooking As Boolean = False
    Dim DonotIncludeSecurityInCustomerOutstanding As Boolean = False
    Dim IsLoadBookingType As Boolean = False
    Dim EnableItemShortDescriptionInBooking As Boolean = False
#End Region
    Public Sub SetUserMgmtNew()

        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        If MyBase.isReverse Then
            btnreverse.Enabled = True
        Else
            btnreverse.Enabled = False
        End If
    End Sub


    Private Sub fndRouteNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtRouteNo._MYValidating

        Dim qry As String = "Select TSPL_ROUTE_MASTER.Route_No as Code,Route_Desc as Description,Type,Employee_Code as 'Employee Code',Off_Day as 'Off Day' from TSPL_ROUTE_MASTER"
        Dim strWhere As String = ""
        If SettTagMultipleRouteWithCustomer AndAlso clsCommon.myLen(txtVendorNo.Value) > 0 Then
            qry += " inner join TSPL_Customer_Route_Master on TSPL_Customer_Route_Master.Route_No=TSPL_ROUTE_MASTER.Route_No "
            strWhere = " TSPL_Customer_Route_Master.cust_Code='" + txtVendorNo.Value + "'"
        End If
        txtRouteNo.Value = clsCommon.ShowSelectForm("DSRouteFinder", qry, "Code", strWhere, txtRouteNo.Value, "", isButtonClicked)
        fndRouteNo_TextChanged()
        lblroutecode.Text = txtRouteNo.Value
        lblroutename.Text = lblRouteDesc.Text
        If SettTagMultipleRouteWithCustomer Then
            If clsCommon.myLen(txtVendorNo.Value) > 0 Then
                qry = "select TSPL_CUSTOMER_MASTER.Cust_Code as Code " & Environment.NewLine &
                " from TSPL_CUSTOMER_MASTER " & Environment.NewLine &
                " inner join TSPL_Customer_Route_Master on TSPL_Customer_Route_Master.cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code" & Environment.NewLine &
                " where TSPL_Customer_Route_Master.Route_No='" & txtRouteNo.Value & "' and TSPL_Customer_Route_Master.cust_Code='" & txtVendorNo.Value & "' "

                txtVendorNo.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, Nothing))
                lblVendorName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code='" + txtVendorNo.Value + "'"))
                setRouteDetail(txtVendorNo.Value, txtRouteNo.Value)
            Else
                txtVendorNo.Value = ""
                lblVendorName.Text = ""
            End If
        End If
    End Sub
    Private Sub fndRouteNo_TextChanged()
        Dim sql As String = "Select Route_Desc,Employee_Code from TSPL_ROUTE_MASTER where Route_No='" + txtRouteNo.Value + "'"
        Dim dr1 As DataTable = clsDBFuncationality.GetDataTable(sql)
        If dr1 IsNot Nothing AndAlso dr1.Rows.Count > 0 Then
            lblRouteDesc.Text = dr1.Rows(0)(0).ToString()
        Else
            lblRouteDesc.Text = String.Empty
        End If
    End Sub


    Private Sub FrmAPInvoiceEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        isPageLoadData = True
        CreateCommonDairyDispatchforFreshAmbient = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateCommonDairyDispatchforFreshAmbient, clsFixedParameterCode.CreateCommonDairyDispatchforFreshAmbient, Nothing))
        CheckOutstandingOnbooking = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CheckOutstandingCreditLimitOnBooking, clsFixedParameterCode.CheckOutstandingCreditLimitOnBooking, Nothing))
        AllowWo_Outstanding = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.AllowDispatchOutstandingFS & "'")) = 0, False, True)
        EnableCustomerPODetailonDairyBooking = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.EnableCustomerPODetailonDairyBooking, clsFixedParameterCode.EnableCustomerPODetailonDairyBooking, Nothing))
        CheckAvgQtyOnDairyBooking = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.DonotCheckAvgQtyOnDairyBooking & "'")) = 0, False, True)
        DoNotConsiderCustomerCreditLimit = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DoNotConsiderCustomerCreditLimit, clsFixedParameterCode.DoNotConsiderCustomerCreditLimit, Nothing))
        SettTagMultipleRouteWithCustomer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TagMultipleRouteWithCustomer, clsFixedParameterCode.TagMultipleRouteWithCustomer, Nothing))
        SettDairyBookingTolleranceQty = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DairyBookingTolleranceQty, clsFixedParameterCode.DairyBookingTolleranceQty, Nothing))
        DonotAllowtoChangeUOMinDairyBookingCustomer = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DonotAllowtoChangeUOMinDairyBookingCustomer, clsFixedParameterCode.DonotAllowtoChangeUOMinDairyBookingCustomer, Nothing)) = 1, True, False)
        ShowBookingTypeDropDownonDairyBookingCustomer = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowBookingTypeDropDownonDairyBookingCustomer, clsFixedParameterCode.ShowBookingTypeDropDownonDairyBookingCustomer, Nothing)) = 1, True, False)
        ShowMulMRPOfSameItemOnDairyBookingCustomer = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowMulMRPOfSameItemOnDairyBookingCustomer, clsFixedParameterCode.ShowMulMRPOfSameItemOnDairyBookingCustomer, Nothing)) = 1, True, False)
        AllowZeroQtyOnDairyBooking = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowZeroQtyOnDairyBooking, clsFixedParameterCode.AllowZeroQtyOnDairyBooking, Nothing)) = 1, True, False)
        PrintTruckSheetAfterGenerate = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PrintTruckSheetAfterGenerate, clsFixedParameterCode.PrintTruckSheetAfterGenerate, Nothing)) = 1, True, False)
        DonotIncludeSecurityInCustomerOutstanding = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DonotIncludeSecurityInCustomerOutstanding, clsFixedParameterCode.DonotIncludeSecurityInCustomerOutstanding, Nothing)) = 1, True, False)
        EnableItemShortDescriptionInBooking = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.EnableItemShortDescriptionInBooking, clsFixedParameterCode.EnableItemShortDescriptionInBooking, Nothing)) = 1, True, False)
        SetMailRight()
        SetUserMgmtNew()
        btnCopy.Visible = True

        If EnableCustomerPODetailonDairyBooking = 1 Then
            MyLabel2.Visible = True
            txtSalesman.Visible = True
            lblSalesman.Visible = True
            MyLabel12.Visible = True
            txtPONo.Visible = True
            MyLabel4.Visible = True
            txtCustPODate.Visible = True
        Else
            MyLabel2.Visible = False
            txtSalesman.Visible = False
            lblSalesman.Visible = False
            MyLabel12.Visible = False
            txtPONo.Visible = False
            MyLabel4.Visible = False
            txtCustPODate.Visible = False
        End If

        txtVendorNo.MendatroryField = True
        'LoadModeOfTrasport()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")

        RadPageView1.SelectedPage = RadPageViewPage1

        LoadBlankGrid()

        AddNew()

        intMRPwithabatement = clsDBFuncationality.getSingleValue("select IsMRPwithAbatement from TSPL_INV_PARAMETERS")

        blnBackCalculation = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IsRateBackCalculation from TSPL_inv_parameters")) = 0, False, True)
        If ShowBookingTypeDropDownonDairyBookingCustomer = True Then
            'Me.RadPageViewPage2.Controls.Add(Me.RadGroupBox2)
            'Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(76.0!, 26.0!)
            'Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 35)
            'Me.RadPageViewPage2.Name = "RadPageViewPage2"
            'Me.RadPageViewPage2.Size = New System.Drawing.Size(1024, 409)
            'Me.RadPageViewPage2.Text = "Item Details"

            Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
            Me.RadGroupBox2.Controls.Add(Me.gv1)
            Me.RadGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
            Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.RadGroupBox2.HeaderText = "Item Details"
            Me.RadGroupBox2.Location = New System.Drawing.Point(0, 0)
            Me.RadGroupBox2.Name = "RadGroupBox2"
            Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
            Me.RadGroupBox2.Size = New System.Drawing.Size(1024, 409)
            Me.RadGroupBox2.TabIndex = 28
            Me.RadGroupBox2.Text = "Item Details"

        Else
            'RadPageView1.Pages("RadPageViewPage2").Item.Visibility = ElementVisibility.Collapsed
        End If
        RadPageView1.Pages("RadPageViewPage4").Item.Visibility = ElementVisibility.Collapsed
        RadPageView1.Pages("RadPageViewPage3").Item.Visibility = ElementVisibility.Collapsed

        If clsCommon.myLen(StrDocNo) > 0 Then
            LoadData(StrDocNo, NavigatorType.Current)
        End If

        If CreateCommonDairyDispatchforFreshAmbient = 1 Then
            ItemTypePanel.Visible = False
        Else
            ItemTypePanel.Visible = True
        End If


        txtLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
        If clsCommon.myLen(txtLocation.Value) > 0 Then
            lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_Location_Master where Location_Code='" + txtLocation.Value + "' "))
        End If
        RadMenuItem3.Visibility = ElementVisibility.Collapsed
        isPageLoadData = False

        Try
            If clsCommon.myLen(Me.Tag) > 0 Then
                LoadData(Me.Tag.ToString(), NavigatorType.Current)
            End If
        Catch ex As Exception
        End Try
        If ShowBookingTypeDropDownonDairyBookingCustomer Then
            txtVendorNo.TabIndex = 0
            txtVendorNo.Focus()
        End If
    End Sub


    Sub BlankAllControls()
        'VendorCodeForChangeIndent = ""
        txtBOstatus.Text = ""
        txtDOStatus.Text = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        lblTotRAmt1.Text = ""
        lblTotalDocAmt.Text = ""
        txtDocNo.Value = ""
        lblDONumber.Text = ""
        txtRouteNo.Value = ""
        lblRouteDesc.Text = ""
        txtVendorNo.Value = ""
        lblVendorName.Text = ""
        lblroutecode.Text = ""
        lblroutename.Text = ""
        lblvehiclecode.Text = ""
        lblvehicleName.Text = ""
        'txtLocation.Value = ""
        'lblLocation.Text = ""
        txtPriceCode.Text = ""
        txtPONo.Text = ""
        txtCustPODate.Checked = False
        txtCustPODate.Value = clsCommon.GETSERVERDATE()
    End Sub

    Sub LoadBlankGrid()
        Dim qry As String = String.Empty
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim TAX_PAID As New GridViewComboBoxColumn
        Dim BOOK_RATE_UOM As New GridViewTextBoxColumn

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "SNo"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 35
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colICode
        repoICode.HeaderImage = My.Resources.search4
        repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoICode.Width = 100
        repoICode.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item Description"
        repoIName.Name = colIName
        repoIName.Width = 150
        repoIName.ReadOnly = True
        repoIName.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoIName)

        Dim repoIShortName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIShortName.FormatString = ""
        repoIShortName.HeaderText = "Item Short Desc"
        repoIShortName.Name = colIShortName
        repoIShortName.Width = 120
        repoIShortName.ReadOnly = IIf(EnableItemShortDescriptionInBooking = True, False, True)
        repoIShortName.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoIShortName)

        'SKG
        'Dim repoIShortName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        'repoIShortName.FormatString = ""
        'repoIShortName.HeaderText = "Item Short Description"
        'repoIShortName.Name = colIShortName
        'repoIShortName.Width = 150
        'repoIShortName.IsVisible = True
        'gv1.MasterTemplate.Columns.Add(repoIShortName)
        'SKG

        Dim repoIHSN As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIHSN.FormatString = ""
        repoIHSN.HeaderText = "HSN Code"
        repoIHSN.Name = colIHSN
        repoIHSN.Width = 150
        repoIHSN.ReadOnly = True
        repoIHSN.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoIHSN)


        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "UOM"
        repoUnit.Name = colUnit
        repoUnit.Width = 80
        repoUnit.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoUnit)

        Dim repoAvgQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAvgQty.FormatString = ""
        repoAvgQty.HeaderText = "Average Qty"
        repoAvgQty.Name = ColAvgQty
        repoAvgQty.Width = 100
        repoAvgQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAvgQty.ReadOnly = True
        repoAvgQty.IsVisible = False
        repoAvgQty.VisibleInColumnChooser = True
        gv1.MasterTemplate.Columns.Add(repoAvgQty)


        Dim repoActualBalQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoActualBalQty.FormatString = ""
        repoActualBalQty.HeaderText = "Available Qty"
        repoActualBalQty.Name = ColAvailableQty
        repoActualBalQty.Width = 100
        repoActualBalQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoActualBalQty.ReadOnly = True
        repoActualBalQty.IsVisible = False
        repoActualBalQty.VisibleInColumnChooser = True
        gv1.MasterTemplate.Columns.Add(repoActualBalQty)

        Dim repopREVIOUSQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repopREVIOUSQty = New GridViewDecimalColumn()
        repopREVIOUSQty.FormatString = ""
        repopREVIOUSQty.HeaderText = "Pre Qty"
        repopREVIOUSQty.Name = colPreviousQty
        repopREVIOUSQty.Width = 50
        repopREVIOUSQty.Minimum = 0
        repopREVIOUSQty.ReadOnly = True
        repopREVIOUSQty.Step = 0
        repopREVIOUSQty.IsVisible = True
        repopREVIOUSQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repopREVIOUSQty.BestFit()
        gv1.MasterTemplate.Columns.Add(repopREVIOUSQty)

        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Qty"
        repoQty.Name = colQty
        repoQty.Width = 50
        repoQty.Minimum = 0
        repoQty.ShowUpDownButtons = False
        repoQty.Step = 0
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoQty.BestFit()
        gv1.MasterTemplate.Columns.Add(repoQty)


        Dim repoRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRate = New GridViewDecimalColumn()
        repoRate.FormatString = ""
        repoRate.HeaderText = "Rate"
        repoRate.Name = colRate
        repoRate.Width = 50
        repoRate.Minimum = 0
        If ShowMulMRPOfSameItemOnDairyBookingCustomer = True AndAlso clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Customer_category,'') from tspl_customer_master where cust_code='" & clsCommon.myCstr(txtVendorNo.Value) & "' ")), "Others") = CompairStringResult.Equal Then
            repoRate.ReadOnly = False
        Else
            repoRate.ReadOnly = True
        End If

        repoRate.IsVisible = True
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoRate.BestFit()
        gv1.MasterTemplate.Columns.Add(repoRate)


        Dim repoAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmt = New GridViewDecimalColumn()
        repoAmt.FormatString = ""
        repoAmt.HeaderText = "Amount"
        repoAmt.Name = colAmt
        repoAmt.Width = 80
        repoAmt.Minimum = 0
        repoAmt.ReadOnly = True
        repoAmt.IsVisible = False
        repoAmt.VisibleInColumnChooser = True
        repoAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAmt)


        Dim repoTBaseAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTBaseAmt = New GridViewDecimalColumn()
        repoTBaseAmt.FormatString = ""
        repoTBaseAmt.HeaderText = "Tax Base Amt"
        repoTBaseAmt.Name = colTBaseAmt
        repoTBaseAmt.Width = 80
        repoTBaseAmt.Minimum = 0
        repoTBaseAmt.ReadOnly = True
        repoTBaseAmt.IsVisible = False
        repoTBaseAmt.VisibleInColumnChooser = True
        repoTBaseAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTBaseAmt)

        Dim repoTTaxAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTTaxAmt = New GridViewDecimalColumn()
        repoTTaxAmt.FormatString = ""
        repoTTaxAmt.HeaderText = "Tax Amt"
        repoTTaxAmt.Name = colTTaxAmt
        repoTTaxAmt.Width = 80
        repoTTaxAmt.Minimum = 0
        repoTTaxAmt.ReadOnly = True
        repoTTaxAmt.IsVisible = False
        repoTTaxAmt.VisibleInColumnChooser = True
        repoTTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTTaxAmt)


        Dim repoOrgRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoOrgRate = New GridViewDecimalColumn()
        repoOrgRate.FormatString = ""
        repoOrgRate.HeaderText = "Org Basic Rate"
        repoOrgRate.Name = colOrgRate
        repoOrgRate.Width = 80
        repoOrgRate.Minimum = 0
        repoOrgRate.ReadOnly = True
        repoOrgRate.IsVisible = False
        repoOrgRate.VisibleInColumnChooser = True
        repoOrgRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoOrgRate)


        Dim repoSellingRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSellingRate = New GridViewDecimalColumn()
        repoSellingRate.FormatString = ""
        repoSellingRate.HeaderText = "Selling Rate"
        repoSellingRate.Name = colSellingRate
        repoSellingRate.Width = 80
        repoSellingRate.Minimum = 0
        repoSellingRate.ReadOnly = True
        repoSellingRate.IsVisible = False
        repoSellingRate.VisibleInColumnChooser = False
        repoSellingRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoSellingRate)

        'colItemBasicPrice

        Dim repoItemBasicPrice As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoItemBasicPrice = New GridViewDecimalColumn()
        repoItemBasicPrice.FormatString = ""
        repoItemBasicPrice.HeaderText = "Basic Price"
        repoItemBasicPrice.Name = colItemBasicPrice
        repoItemBasicPrice.Width = 80
        repoItemBasicPrice.Minimum = 0
        repoItemBasicPrice.ReadOnly = True
        repoItemBasicPrice.IsVisible = False
        repoItemBasicPrice.VisibleInColumnChooser = False
        repoItemBasicPrice.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoItemBasicPrice)

        'colAmountWithTax
        Dim repoAmountWithTax As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmountWithTax = New GridViewDecimalColumn()
        repoAmountWithTax.FormatString = ""
        repoAmountWithTax.HeaderText = "Amount with Tax"
        repoAmountWithTax.Name = colAmountWithTax
        repoAmountWithTax.Width = 80
        repoAmountWithTax.Minimum = 0
        repoAmountWithTax.ReadOnly = True
        repoAmountWithTax.IsVisible = False
        repoAmountWithTax.VisibleInColumnChooser = False
        repoAmountWithTax.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAmountWithTax)

        Dim repoSchemeType As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSchemeType.AllowSort = False
        repoSchemeType.HeaderText = "Scheme Type"
        repoSchemeType.Name = colSchemeType
        repoSchemeType.ReadOnly = True
        repoSchemeType.Width = 96
        repoSchemeType.IsVisible = False
        repoSchemeType.VisibleInColumnChooser = True
        gv1.MasterTemplate.Columns.Add(repoSchemeType)


        Dim repoDisc_Scheme_Type As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDisc_Scheme_Type.HeaderText = "Cash Scheme Type"
        repoDisc_Scheme_Type.Name = colDisc_Scheme_Type
        repoDisc_Scheme_Type.Width = 80
        repoDisc_Scheme_Type.ReadOnly = True
        repoDisc_Scheme_Type.IsVisible = False
        repoDisc_Scheme_Type.VisibleInColumnChooser = True
        gv1.MasterTemplate.Columns.Add(repoDisc_Scheme_Type)

        Dim repoDisc_Scheme_Pers As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDisc_Scheme_Pers.HeaderText = "Cash Scheme %."
        repoDisc_Scheme_Pers.MinWidth = 4
        repoDisc_Scheme_Pers.Name = colDisc_Scheme_Pers
        repoDisc_Scheme_Pers.ReadOnly = True
        repoDisc_Scheme_Pers.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDisc_Scheme_Pers.Width = 54
        repoDisc_Scheme_Pers.IsVisible = False
        repoDisc_Scheme_Pers.VisibleInColumnChooser = True
        gv1.MasterTemplate.Columns.Add(repoDisc_Scheme_Pers)

        Dim repoDisc_Scheme_Amount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDisc_Scheme_Amount.HeaderText = "Cash Scheme Amt."
        repoDisc_Scheme_Amount.MinWidth = 4
        repoDisc_Scheme_Amount.Name = colDisc_Scheme_Amount
        repoDisc_Scheme_Amount.ReadOnly = True
        repoDisc_Scheme_Amount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDisc_Scheme_Amount.Width = 54
        repoDisc_Scheme_Amount.IsVisible = False
        repoDisc_Scheme_Amount.VisibleInColumnChooser = True
        gv1.MasterTemplate.Columns.Add(repoDisc_Scheme_Amount)

        Dim repoDisc_Scheme_Code As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDisc_Scheme_Code.HeaderText = "Cash Scheme Code"
        repoDisc_Scheme_Code.Name = colDisc_Scheme_Code
        repoDisc_Scheme_Code.Width = 80
        repoDisc_Scheme_Code.ReadOnly = True
        repoDisc_Scheme_Code.IsVisible = False
        repoDisc_Scheme_Code.VisibleInColumnChooser = True
        gv1.MasterTemplate.Columns.Add(repoDisc_Scheme_Code)

        'sanjay
        Dim repoTax_NonTax As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax_NonTax.HeaderText = "Tax-NonTax"
        repoTax_NonTax.MinWidth = 4
        repoTax_NonTax.Name = colTax_NonTax
        repoTax_NonTax.ReadOnly = True
        repoTax_NonTax.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTax_NonTax.Width = 54
        repoTax_NonTax.IsVisible = False
        repoTax_NonTax.VisibleInColumnChooser = True
        gv1.MasterTemplate.Columns.Add(repoTax_NonTax)

        Dim repoFreshAmbient As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoFreshAmbient.HeaderText = "Fresh/Ambient"
        repoFreshAmbient.Name = colFreshAmbient
        repoFreshAmbient.Width = 80
        repoFreshAmbient.ReadOnly = True
        repoFreshAmbient.IsVisible = False
        repoFreshAmbient.VisibleInColumnChooser = True
        gv1.MasterTemplate.Columns.Add(repoFreshAmbient)


        Dim repoRemarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRemarks.FormatString = ""
        repoRemarks.HeaderText = "Remarks"
        repoRemarks.Name = colRemarks
        repoRemarks.Width = 200
        repoRemarks.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoRemarks)

        ' colItemBasicPrice 

        clsCustomFieldGrid.LoadBlankGrid(gv1, MyBase.ArrDetailFields)

        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        ReStoreGridLayout()
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            'gv1.CurrentRow.Cells(colDisPer).Value
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column.FieldName.StartsWith("_CFLD_") Then
                        clsCustomFieldGrid.getFinderForCustomFieldGrid(gv1, e.Column.Name.ToString, MyBase.Form_ID)
                    End If

                    If e.Column Is gv1.Columns(colIShortName) OrElse e.Column Is gv1.Columns(colUnit) OrElse e.Column Is gv1.Columns(colICode) OrElse e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colRate) Then


                        If e.Column Is gv1.Columns(colICode) Then
                            If clsCommon.myLen(txtVendorNo.Value) <= 0 Then
                                common.clsCommon.MyMessageBoxShow(Me, "Please select Customer First", Me.Text)
                                isCellValueChangedOpen = False
                                Exit Sub
                            End If
                            If clsCommon.myLen(txtLocation.Value) <= 0 Then
                                common.clsCommon.MyMessageBoxShow(Me, "Please select Location First", Me.Text)
                                isCellValueChangedOpen = False
                                Exit Sub
                            End If
                            OpenItemList(False)
                            Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                            Dim strIUOM As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value)
                            ItemPrice(strICode, strIUOM, clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value), gv1.CurrentRow.Index)

                            'SKG
                        ElseIf e.Column Is gv1.Columns(colIShortName) Then
                            If clsCommon.myLen(txtVendorNo.Value) <= 0 Then
                                common.clsCommon.MyMessageBoxShow(Me, "Please select Customer First", Me.Text)
                                isCellValueChangedOpen = False
                                Exit Sub
                            End If
                            If clsCommon.myLen(txtLocation.Value) <= 0 Then
                                common.clsCommon.MyMessageBoxShow(Me, "Please select Location First", Me.Text)
                                isCellValueChangedOpen = False
                                Exit Sub
                            End If
                            gv1.CurrentRow.Cells(colICode).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select distinct Item_Code from tspl_item_master where Short_Description='" + gv1.CurrentRow.Cells(colIShortName).Value + "'"))
                            OpenItemList(False)
                            Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                            Dim strIUOM As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value)
                            ItemPrice(strICode, strIUOM, clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value), gv1.CurrentRow.Index)
                            'SKG

                        ElseIf e.Column Is gv1.Columns(colUnit) Then
                            Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)

                            If clsCommon.myLen(strICode) > 0 Then
                                OpenUOMList(False)

                            Else
                                Throw New Exception("Please fill item first.")
                                isCellValueChangedOpen = False
                                Exit Sub
                            End If

                            Dim strIUOM As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value)
                            ItemPrice(strICode, strIUOM, clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value), gv1.CurrentRow.Index)
                            UpdateCurrentRowAvgQty(gv1.CurrentRow.Index)
                        ElseIf e.Column Is gv1.Columns(colQty) Then
                            '    Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                            '    Dim strIUOM As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value)
                            '    ItemPrice(strICode, strIUOM, clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value), gv1.CurrentRow.Index)
                            'gv1.CurrentRow.Cells(colOrgCost).Value = gv1.CurrentRow.Cells(colRate).Value
                            If ShowBookingTypeDropDownonDairyBookingCustomer = True Then
                                Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                                Dim strIUOM As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value)
                                ItemPrice(strICode, strIUOM, clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value), gv1.CurrentRow.Index)
                            End If
                            UpdateCurrentRow(gv1.CurrentRow.Index)
                            UpdateCurrentRowAvgQty(gv1.CurrentRow.Index)
                            UpdateAllTotals()
                        ElseIf e.Column Is gv1.Columns(colRate) Then
                            If ShowMulMRPOfSameItemOnDairyBookingCustomer Then
                                If clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)) <= 0 Then
                                    common.clsCommon.MyMessageBoxShow(Me, "Please select Item First", Me.Text)
                                    isCellValueChangedOpen = False
                                    Exit Sub
                                End If
                                If clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value)) <= 0 Then
                                    common.clsCommon.MyMessageBoxShow(Me, "Please select Item Uom First", Me.Text)
                                    isCellValueChangedOpen = False
                                    Exit Sub
                                End If

                                Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                                Dim strIUOM As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value)
                                ItemPriceFinderForRateSelection(strICode, strIUOM, clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value), gv1.CurrentRow.Index)
                                UpdateCurrentRow(gv1.CurrentRow.Index)
                                UpdateCurrentRowAvgQty(gv1.CurrentRow.Index)
                                UpdateAllTotals()
                            End If
                        End If

                    End If

                End If
                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub


    Sub RefreshSerialNo()
        Dim intSerialNo As Integer
        For intCount As Integer = 0 To gv1.Rows.Count - 1
            intSerialNo += 1
            gv1.Rows(intCount).Cells(colLineNo).Value = intSerialNo
        Next


    End Sub


    Sub AddCustomerDisc(ByVal isButtonClick As Boolean)

    End Sub

    Sub OpenItemList(ByVal isButtonClick As Boolean)
        Dim strTax As String = Nothing
        GSTStatus = clsERPFuncationality.GetGSTStatus(txtDate.Value)
        Dim whrCls As String = ""
        If CreateCommonDairyDispatchforFreshAmbient = 0 Then
            whrCls = IIf(rbtn_Fresh.IsChecked = True, " isnull(Is_FreshItem,0)=1 ", " isnull(Is_Ambient,0)=1 ")
        End If
        ' done by priti BHA/14/06/18-000053,ERO/18/06/18-000347
        If clsCommon.myLen(whrCls) > 0 Then
            whrCls += "  and isnull(TSPL_ITEM_MASTER.CAN,0)=0  and isnull(TSPL_ITEM_MASTER.CRATE,0)=0 "
        Else
            whrCls += "  isnull(TSPL_ITEM_MASTER.CAN,0)=0  and isnull(TSPL_ITEM_MASTER.CRATE,0)=0 "
        End If

        'Sanjay BHA/09/07/18-000142
        whrCls += " and isnull(TSPL_ITEM_MASTER.item_type,'')='F' "
        'Sanjay BHA/09/07/18-000142

        'Ticket No-ALF/05/03/19-000093 show only active item
        whrCls += " and tspl_item_master.Active=1 "


        '"Is_FreshItem=0 and Product_Type not in ('MI')  and Item_Type in ('F','T','S') and Is_Serial_Item=0 " & strTax
        gv1.CurrentRow.Cells(colICode).Value = clsItemMaster.getFinder(whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), False)
        If GSTStatus = False Then
            If CheckItemtaxType() = False Then
                Exit Sub
            End If
        End If
        gv1.CurrentRow.Cells(colIName).Value = clsDBFuncationality.getSingleValue("select Item_Desc from TSPL_ITEM_MASTER where Item_Code='" & gv1.CurrentRow.Cells(colICode).Value & "' ")
        gv1.CurrentRow.Cells(colIShortName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select distinct Short_Description from tspl_item_master where item_code='" + gv1.CurrentRow.Cells(colICode).Value + "'"))
        gv1.CurrentRow.Cells(colIHSN).Value = clsItemMaster.GetItemHSNCode(gv1.CurrentRow.Cells(colICode).Value, Nothing)
        gv1.CurrentRow.Cells(colUnit).Value = clsDBFuncationality.getSingleValue("select UOM_Code from TSPL_ITEM_UOM_DETAIL where Default_UOM=1 and Item_Code='" & gv1.CurrentRow.Cells(colICode).Value & "' ")
        gv1.CurrentRow.Cells(ColAvailableQty).Value = clsItemLocationDetails.getBalance(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), txtLocation.Value, txtDocNo.Value, txtDate.Value, Nothing, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), 0)
        gv1.CurrentRow.Cells(colTax_NonTax).Value = clsDBFuncationality.getSingleValue("select IsTaxable from TSPL_ITEM_MASTER where Item_Code='" & gv1.CurrentRow.Cells(colICode).Value & "' ")
        gv1.CurrentRow.Cells(colFreshAmbient).Value = clsDBFuncationality.getSingleValue("select case when Is_Ambient=1 then 'PS' WHEN Is_FreshItem=1 THEN 'FS' ELSE '' END from TSPL_ITEM_MASTER where Item_Code='" & gv1.CurrentRow.Cells(colICode).Value & "' ")
        For i As Integer = 0 To gv1.Rows.Count - 1
            If chkSampling.Checked = False And DonotAllowtoChangeUOMinDairyBookingCustomer = True Then
                gv1.Rows(i).Cells(colUnit).ReadOnly = True
            Else
                gv1.Rows(i).Cells(colUnit).ReadOnly = False
            End If
        Next
    End Sub

    Public Sub ItemPrice(ByVal strItem As String, ByVal strUnit As String, ByVal intQty As Decimal, ByVal introw As Integer)

        'Pick Rate
        'Dim Price_code As String = clsDBFuncationality.getSingleValue("select price_CodeNon from tspl_customer_master where cust_code='" & txtVendorNo.Value & "'")
        Dim dt As New DataTable()
        Dim dblRate As Double = 0
        Dim dblTotal As Double = 0
        Dim dblItemBasicPrice As Double = 0
        Dim qry As String = ""

        qry = " Select Is_With_Tax, RowNo, Item_Price_ID, XXXE.Item_Code, UOM, Start_Date, Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,XXXE.TAX1_Rate, " &
        " XXXE.TAX2_Rate,XXXE.TAX3_Rate,XXXE.TAX4_Rate,XXXE.TAX5_Rate, " &
        "  XXXE.TAX6_Rate,XXXE.TAX7_Rate,XXXE.TAX8_Rate,XXXE.TAX9_Rate, " &
        " XXXE.TAX10_Rate,XXXE.TAX1 ,XXXE.TAX2,XXXE.TAX3, " &
        " XXXE.TAX4,XXXE.TAX5,XXXE.TAX6,XXXE.TAX7, " &
        " XXXE.TAX8,XXXE.TAX9,XXXE.TAX10,XXXE.TAX1_Amt, " &
        " XXXE.TAX2_Amt,XXXE.TAX3_Amt,XXXE.TAX4_Amt  from ( " &
        "Select ROW_NUMBER() OVER (Partition By TSPL_ITEM_PRICE_MASTER.Item_Code ORDER BY TSPL_ITEM_PRICE_MASTER.Item_Code,  " &
        "Start_Date Desc) as RowNo,Is_With_Tax, Item_Price_ID, TSPL_ITEM_PRICE_MASTER.Item_Code, UOM, Start_Date,  " &
        "Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,TSPL_ITEM_PRICE_MASTER.TAX1_Rate,  " &
        "TSPL_ITEM_PRICE_MASTER.TAX2_Rate,TSPL_ITEM_PRICE_MASTER.TAX3_Rate,TSPL_ITEM_PRICE_MASTER.TAX4_Rate,TSPL_ITEM_PRICE_MASTER.TAX5_Rate,  " &
        " TSPL_ITEM_PRICE_MASTER.TAX6_Rate, TSPL_ITEM_PRICE_MASTER.TAX7_Rate, TSPL_ITEM_PRICE_MASTER.TAX8_Rate, TSPL_ITEM_PRICE_MASTER.TAX9_Rate, " &
        " TSPL_ITEM_PRICE_MASTER.TAX10_Rate, TSPL_ITEM_PRICE_MASTER.TAX1, TSPL_ITEM_PRICE_MASTER.TAX2, TSPL_ITEM_PRICE_MASTER.TAX3, " &
        " TSPL_ITEM_PRICE_MASTER.TAX4, TSPL_ITEM_PRICE_MASTER.TAX5, TSPL_ITEM_PRICE_MASTER.TAX6, TSPL_ITEM_PRICE_MASTER.TAX7, " &
        " TSPL_ITEM_PRICE_MASTER.TAX8,TSPL_ITEM_PRICE_MASTER.TAX9,TSPL_ITEM_PRICE_MASTER.TAX10,TSPL_ITEM_PRICE_MASTER.TAX1_Amt , TSPL_ITEM_PRICE_MASTER.TAX2_Amt ,TSPL_ITEM_PRICE_MASTER.TAX3_Amt ,TSPL_ITEM_PRICE_MASTER.TAX4_Amt from TSPL_ITEM_PRICE_MASTER  left  outer join  " &
        "TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and  " &
        "TSPL_ITEM_PRICE_MASTER.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code   where  Start_Date<='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  and (End_Date >= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  or End_date is null)  and  " &
        "TSPL_ITEM_PRICE_MASTER.Price_Code='" & Price_code & "' and UOM='" & strUnit & "' and TSPL_ITEM_PRICE_MASTER.item_code='" & strItem & "' AND Location_Code='" & clsCommon.myCstr(txtLocation.Value) & "'  " &
        ") XXXE WHERE RowNo=1  "
        dt = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            dblRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
            If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0).Item("Is_With_Tax")), "N") = CompairStringResult.Equal Then
                dblItemBasicPrice = Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("Item_Basic_Price")) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX1_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX2_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX3_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX4_Amt")), 2), 2)
            Else
                dblItemBasicPrice = clsCommon.myCdbl(dt.Rows(0).Item("Item_Basic_Price"))
            End If

            If dblRate = 0 Then
                Throw New Exception("Please Fill Selling Price for Location " & txtLocation.Value & "  for item " & clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value) & ".")
                Exit Sub
            End If
            'End If

        Else
            Throw New Exception("Please create Price chart for customer " & txtVendorNo.Value & " for Location " & txtLocation.Value & "  for item " & clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value) & ".")
            blnSaveTotalQTy = False
            Exit Sub

        End If

        '''''''''''scheme
        Dim obj_Cash As clsSchemeApplyOnDairy = Nothing
        Dim tax As Double = 0
        Dim tax_on_amt As Decimal = 0
        obj_Cash = clsSchemeApplyOnDairy.GetDiscountSchemeData(strItem, strUnit, intQty, txtVendorNo.Value, Nothing, Nothing)
        If obj_Cash IsNot Nothing Then
            gv1.CurrentRow.Cells(colDisc_Scheme_Amount).Value = obj_Cash.Cash_Amt 'objBookingitem.Disc_Scheme_Amount = obj_Cash.Cash_Amt
            gv1.CurrentRow.Cells(colDisc_Scheme_Pers).Value = obj_Cash.Cash_Pers  'objBookingitem.Disc_Scheme_Pers = obj_Cash.Cash_Pers
            gv1.CurrentRow.Cells(colDisc_Scheme_Code).Value = obj_Cash.Schm_Code        ' objBookingitem.Disc_Scheme_Code = obj_Cash.Schm_Code
            If clsCommon.myCdbl(obj_Cash.Cash_Pers) <> 0 Then
                gv1.CurrentRow.Cells(colDisc_Scheme_Type).Value = "P"
                gv1.CurrentRow.Cells(colDisc_Scheme_Amount).Value = System.Math.Round((dblRate * obj_Cash.Cash_Pers) / 100, 2)
            ElseIf clsCommon.myCdbl(obj_Cash.Cash_Amt) <> 0 Then
                gv1.CurrentRow.Cells(colDisc_Scheme_Type).Value = "A"
            End If

            dblRate = dblRate - gv1.CurrentRow.Cells(colDisc_Scheme_Amount).Value
            tax_on_amt = dblRate
            'Dim Alltax As Double = System.Math.Round((clsCommon.myCdbl(dt.Rows(0).Item("TAX1_Rate")) / 100 + clsCommon.myCdbl(dt.Rows(0).Item("TAX2_Rate")) / 100 + clsCommon.myCdbl(dt.Rows(0).Item("TAX3_Rate")) / 100 + clsCommon.myCdbl(dt.Rows(0).Item("TAX4_Rate")) / 100 + clsCommon.myCdbl(dt.Rows(0).Item("TAX5_Rate")) / 100 + clsCommon.myCdbl(dt.Rows(0).Item("TAX6_Rate")) / 100 + clsCommon.myCdbl(dt.Rows(0).Item("TAX7_Rate")) / 100 + clsCommon.myCdbl(dt.Rows(0).Item("TAX8_Rate")) / 100 + clsCommon.myCdbl(dt.Rows(0).Item("TAX9_Rate")) / 100 + clsCommon.myCdbl(dt.Rows(0).Item("TAX10_Rate"))), 3)
            'tax = System.Math.Round((dblRate * Alltax), 3)
            'dblRate = dblRate + tax


        End If


        ''''''''''''scheme


        dt = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            gv1.CurrentRow.Cells(colSellingRate).Value = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
            gv1.CurrentRow.Cells(colOrgRate).Value = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
            If ShowMulMRPOfSameItemOnDairyBookingCustomer = True Then
                isCellValueChangedOpen = True
            End If
            gv1.CurrentRow.Cells(colRate).Value = dblRate
            If ShowMulMRPOfSameItemOnDairyBookingCustomer = True Then
                isCellValueChangedOpen = False
            End If
            gv1.CurrentRow.Cells(colTBaseAmt).Value = tax_on_amt
            gv1.CurrentRow.Cells(colTTaxAmt).Value = tax
            gv1.CurrentRow.Cells(colItemBasicPrice).Value = dblItemBasicPrice
            gv1.CurrentRow.Cells(colAmountWithTax).Value = dblItemBasicPrice * intQty
            UpdateCurrentRow(gv1.CurrentRow.Index)

            Dim DOCdateCurrent As Date? = Nothing
            DOCdateCurrent = clsCommon.GETSERVERDATE()
            ' Query to get scheme type of Item
            Dim qryScheme As String = "Select TSPL_SCHEME_MASTER_NEW.Scheme_Type from TSPL_SCHEME_MASTER_NEW left outer join TSPL_SCHEME_DETAIL_NEW on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_MASTER_NEW.Scheme_Code "
            qryScheme += " left outer join tspl_item_master on tspl_item_master.item_code=TSPL_SCHEME_DETAIL_NEW.Item_Code "
            qryScheme += " where TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select scheme_code from (select ROW_NUMBER() over (partition by scheme_type order by start_date desc) as sno, TSPL_SCHEME_MASTER_NEW.Scheme_Code from "
            qryScheme += " TSPL_SCHEME_MASTER_NEW where TSPL_SCHEME_MASTER_NEW.Scheme_Type='Quantitive' and TSPL_SCHEME_MASTER_NEW.Status='Active' and  TSPL_SCHEME_MASTER_NEW.Start_Date<='" & clsCommon.GetPrintDate(DOCdateCurrent, "dd/MMM/yyyy") & "'  and (TSPL_SCHEME_MASTER_NEW.End_Date >= '" & clsCommon.GetPrintDate(DOCdateCurrent, "dd/MMM/yyyy") & "'  or TSPL_SCHEME_MASTER_NEW.End_date is null) and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select TSPL_SCHEME_DETAIL_NEW.Scheme_Code from "
            qryScheme += " TSPL_SCHEME_DETAIL_NEW left outer join TSPL_SCHEME_BENEFICIARY on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_BENEFICIARY.Scheme_Code where MainItem_Code='" + strItem + "' and Cust_Code='" + txtVendorNo.Value + "'))a where a.sno=1)"
            qryScheme += " and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select Scheme_Code from TSPL_SCHEME_BENEFICIARY where Cust_Code='" + txtVendorNo.Value + "') and TSPL_SCHEME_MASTER_NEW.Status='Active'"
            qryScheme += " and TSPL_SCHEME_DETAIL_NEW.MainItem_Code='" + strItem + "' "


            qryScheme += " order by TSPL_SCHEME_MASTER_NEW.Scheme_Code"
            Dim SchemeType As String = clsDBFuncationality.getSingleValue(qryScheme)
            If clsCommon.myLen(clsCommon.myCstr(SchemeType)) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(SchemeType), "Quantitive") = CompairStringResult.Equal Then
                Dim objD As clsSchemeApplyOnDairy = Nothing
                objD = clsSchemeApplyOnDairy.GetSchemeData(clsCommon.myCstr(strItem), clsCommon.myCstr(strUnit), clsCommon.myCstr(intQty), txtVendorNo.Value, clsCommon.myCstr(SchemeType), Nothing, Nothing, Nothing)

                If objD IsNot Nothing AndAlso objD.Arr.Count > 0 Then
                    For Each objtrScheme As clsSchemeApplyOnDairy In objD.Arr
                        gv1.CurrentRow.Cells(colSchemeType).Value = objtrScheme.schm_Type
                    Next

                End If
            End If

        Else
            clsCommon.MyMessageBoxShow(Me, "Please create Price chart for customer " & clsCommon.myCstr(txtVendorNo.Value) & " for Location " & clsCommon.myCstr(txtLocation.Value) & "  for item " & gv1.Rows(introw).Cells(colICode).Value & ".", Me.Text)
            gv1.CurrentRow.Cells(colICode).Value = Nothing
            gv1.CurrentRow.Cells(colUnit).Value = Nothing
            gv1.CurrentRow.Cells(colIName).Value = Nothing
            gv1.CurrentRow.Cells(colIHSN).Value = Nothing
            gv1.CurrentRow.Cells(colRate).Value = 0
            gv1.CurrentRow.Cells(colOrgRate).Value = 0
            gv1.CurrentRow.Cells(colTBaseAmt).Value = 0
            gv1.CurrentRow.Cells(colTTaxAmt).Value = 0

            Exit Sub
        End If

        'Return dblRate
    End Sub
    Private Sub ItemPriceFinderForRateSelection(ByVal strItem As String, ByVal strUnit As String, ByVal intQty As Decimal, ByVal introw As Integer)
        Dim dt As New DataTable()
        Dim dblRate As Double = 0
        Dim dblTotal As Double = 0
        Dim dblItemBasicPrice As Double = 0
        Dim qry As String = ""

        qry = " Select Is_With_Tax, RowNo, Item_Price_ID, XXXE.Item_Code, UOM, Start_Date, Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,XXXE.TAX1_Rate, " &
        " XXXE.TAX2_Rate,XXXE.TAX3_Rate,XXXE.TAX4_Rate,XXXE.TAX5_Rate, " &
        "  XXXE.TAX6_Rate,XXXE.TAX7_Rate,XXXE.TAX8_Rate,XXXE.TAX9_Rate, " &
        " XXXE.TAX10_Rate,XXXE.TAX1 ,XXXE.TAX2,XXXE.TAX3, " &
        " XXXE.TAX4,XXXE.TAX5,XXXE.TAX6,XXXE.TAX7, " &
        " XXXE.TAX8,XXXE.TAX9,XXXE.TAX10,XXXE.TAX1_Amt, " &
        " XXXE.TAX2_Amt,XXXE.TAX3_Amt,XXXE.TAX4_Amt  from ( " &
        "Select ROW_NUMBER() OVER (Partition By TSPL_ITEM_PRICE_MASTER.Item_Code ORDER BY TSPL_ITEM_PRICE_MASTER.Item_Code,  " &
        "Start_Date Desc) as RowNo,Is_With_Tax, Item_Price_ID, TSPL_ITEM_PRICE_MASTER.Item_Code, UOM, Start_Date,  " &
        "Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,TSPL_ITEM_PRICE_MASTER.TAX1_Rate,  " &
        "TSPL_ITEM_PRICE_MASTER.TAX2_Rate,TSPL_ITEM_PRICE_MASTER.TAX3_Rate,TSPL_ITEM_PRICE_MASTER.TAX4_Rate,TSPL_ITEM_PRICE_MASTER.TAX5_Rate,  " &
        " TSPL_ITEM_PRICE_MASTER.TAX6_Rate, TSPL_ITEM_PRICE_MASTER.TAX7_Rate, TSPL_ITEM_PRICE_MASTER.TAX8_Rate, TSPL_ITEM_PRICE_MASTER.TAX9_Rate, " &
        " TSPL_ITEM_PRICE_MASTER.TAX10_Rate, TSPL_ITEM_PRICE_MASTER.TAX1, TSPL_ITEM_PRICE_MASTER.TAX2, TSPL_ITEM_PRICE_MASTER.TAX3, " &
        " TSPL_ITEM_PRICE_MASTER.TAX4, TSPL_ITEM_PRICE_MASTER.TAX5, TSPL_ITEM_PRICE_MASTER.TAX6, TSPL_ITEM_PRICE_MASTER.TAX7, " &
        " TSPL_ITEM_PRICE_MASTER.TAX8,TSPL_ITEM_PRICE_MASTER.TAX9,TSPL_ITEM_PRICE_MASTER.TAX10,TSPL_ITEM_PRICE_MASTER.TAX1_Amt , TSPL_ITEM_PRICE_MASTER.TAX2_Amt ,TSPL_ITEM_PRICE_MASTER.TAX3_Amt ,TSPL_ITEM_PRICE_MASTER.TAX4_Amt from TSPL_ITEM_PRICE_MASTER  left  outer join  " &
        "TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and  " &
        "TSPL_ITEM_PRICE_MASTER.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code   where  Start_Date<='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  and (End_Date >= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  or End_date is null)  and  " &
        "TSPL_ITEM_PRICE_MASTER.Price_Code='" & Price_code & "' and UOM='" & strUnit & "' and TSPL_ITEM_PRICE_MASTER.item_code='" & strItem & "' AND Location_Code='" & clsCommon.myCstr(txtLocation.Value) & "'  " &
        ") XXXE "

        Dim dr As DataRow = Nothing
        dr = clsCommon.ShowSelectFormForRow("ItemrateFinder", qry)
        If Not dr Is Nothing Then

            dblRate = clsCommon.myCdbl(dr("Item_Selling_Price"))
            If clsCommon.CompairString(clsCommon.myCstr(dr("Is_With_Tax")), "N") = CompairStringResult.Equal Then
                dblItemBasicPrice = Math.Round(clsCommon.myCdbl(dr("Item_Basic_Price")) + Math.Round(clsCommon.myCdbl(dr("TAX1_Amt")), 2) + Math.Round(clsCommon.myCdbl(dr("TAX2_Amt")), 2) + Math.Round(clsCommon.myCdbl(dr("TAX3_Amt")), 2) + Math.Round(clsCommon.myCdbl(dr("TAX4_Amt")), 2), 2)
            Else
                dblItemBasicPrice = clsCommon.myCdbl(dr("Item_Basic_Price"))
            End If

            If dblRate = 0 Then
                Throw New Exception("Please Fill Selling Price for Location " & txtLocation.Value & "  for item " & clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value) & ".")
                Exit Sub
            End If
        Else

            Throw New Exception("Please create Price chart for customer " & txtVendorNo.Value & " for Location " & txtLocation.Value & "  for item " & clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value) & ".")
            blnSaveTotalQTy = False
            Exit Sub
        End If

        '''''''''''scheme
        Dim obj_Cash As clsSchemeApplyOnDairy = Nothing
        Dim tax As Double = 0
        Dim tax_on_amt As Decimal = 0
        obj_Cash = clsSchemeApplyOnDairy.GetDiscountSchemeData(strItem, strUnit, intQty, txtVendorNo.Value, Nothing, Nothing)
        If obj_Cash IsNot Nothing Then
            gv1.CurrentRow.Cells(colDisc_Scheme_Amount).Value = obj_Cash.Cash_Amt 'objBookingitem.Disc_Scheme_Amount = obj_Cash.Cash_Amt
            gv1.CurrentRow.Cells(colDisc_Scheme_Pers).Value = obj_Cash.Cash_Pers  'objBookingitem.Disc_Scheme_Pers = obj_Cash.Cash_Pers
            gv1.CurrentRow.Cells(colDisc_Scheme_Code).Value = obj_Cash.Schm_Code        ' objBookingitem.Disc_Scheme_Code = obj_Cash.Schm_Code
            If clsCommon.myCdbl(obj_Cash.Cash_Pers) <> 0 Then
                gv1.CurrentRow.Cells(colDisc_Scheme_Type).Value = "P"
                gv1.CurrentRow.Cells(colDisc_Scheme_Amount).Value = System.Math.Round((dblRate * obj_Cash.Cash_Pers) / 100, 2)
            ElseIf clsCommon.myCdbl(obj_Cash.Cash_Amt) <> 0 Then
                gv1.CurrentRow.Cells(colDisc_Scheme_Type).Value = "A"
            End If

            dblRate = dblRate - gv1.CurrentRow.Cells(colDisc_Scheme_Amount).Value
            tax_on_amt = dblRate

        End If

        ''''''''''''scheme

        qry += " where RowNo='" & clsCommon.myCstr(dr("RowNo")) & "'"
        dt = clsDBFuncationality.GetDataTable(qry)


        If dt.Rows.Count > 0 Then
            gv1.CurrentRow.Cells(colSellingRate).Value = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
            gv1.CurrentRow.Cells(colOrgRate).Value = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
            isCellValueChangedOpen = True
            gv1.CurrentRow.Cells(colRate).Value = dblRate
            isCellValueChangedOpen = False
            gv1.CurrentRow.Cells(colTBaseAmt).Value = tax_on_amt
            gv1.CurrentRow.Cells(colTTaxAmt).Value = tax
            gv1.CurrentRow.Cells(colItemBasicPrice).Value = dblItemBasicPrice
            gv1.CurrentRow.Cells(colAmountWithTax).Value = dblItemBasicPrice * intQty
            UpdateCurrentRow(gv1.CurrentRow.Index)

            Dim DOCdateCurrent As Date? = Nothing
            DOCdateCurrent = clsCommon.GETSERVERDATE()
            ' Query to get scheme type of Item
            Dim qryScheme As String = "Select TSPL_SCHEME_MASTER_NEW.Scheme_Type from TSPL_SCHEME_MASTER_NEW left outer join TSPL_SCHEME_DETAIL_NEW on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_MASTER_NEW.Scheme_Code "
            qryScheme += " left outer join tspl_item_master on tspl_item_master.item_code=TSPL_SCHEME_DETAIL_NEW.Item_Code "
            qryScheme += " where TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select scheme_code from (select ROW_NUMBER() over (partition by scheme_type order by start_date desc) as sno, TSPL_SCHEME_MASTER_NEW.Scheme_Code from "
            qryScheme += " TSPL_SCHEME_MASTER_NEW where TSPL_SCHEME_MASTER_NEW.Scheme_Type='Quantitive' and TSPL_SCHEME_MASTER_NEW.Status='Active' and  TSPL_SCHEME_MASTER_NEW.Start_Date<='" & clsCommon.GetPrintDate(DOCdateCurrent, "dd/MMM/yyyy") & "'  and (TSPL_SCHEME_MASTER_NEW.End_Date >= '" & clsCommon.GetPrintDate(DOCdateCurrent, "dd/MMM/yyyy") & "'  or TSPL_SCHEME_MASTER_NEW.End_date is null) and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select TSPL_SCHEME_DETAIL_NEW.Scheme_Code from "
            qryScheme += " TSPL_SCHEME_DETAIL_NEW left outer join TSPL_SCHEME_BENEFICIARY on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_BENEFICIARY.Scheme_Code where MainItem_Code='" + strItem + "' and Cust_Code='" + txtVendorNo.Value + "'))a where a.sno=1)"
            qryScheme += " and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select Scheme_Code from TSPL_SCHEME_BENEFICIARY where Cust_Code='" + txtVendorNo.Value + "') and TSPL_SCHEME_MASTER_NEW.Status='Active'"
            qryScheme += " and TSPL_SCHEME_DETAIL_NEW.MainItem_Code='" + strItem + "' "


            qryScheme += " order by TSPL_SCHEME_MASTER_NEW.Scheme_Code"
            Dim SchemeType As String = clsDBFuncationality.getSingleValue(qryScheme)
            If clsCommon.myLen(clsCommon.myCstr(SchemeType)) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(SchemeType), "Quantitive") = CompairStringResult.Equal Then
                Dim objD As clsSchemeApplyOnDairy = Nothing
                objD = clsSchemeApplyOnDairy.GetSchemeData(clsCommon.myCstr(strItem), clsCommon.myCstr(strUnit), clsCommon.myCstr(intQty), txtVendorNo.Value, clsCommon.myCstr(SchemeType), Nothing, Nothing, Nothing)

                If objD IsNot Nothing AndAlso objD.Arr.Count > 0 Then
                    For Each objtrScheme As clsSchemeApplyOnDairy In objD.Arr
                        gv1.CurrentRow.Cells(colSchemeType).Value = objtrScheme.schm_Type
                    Next

                End If
            End If

        Else
            clsCommon.MyMessageBoxShow(Me, "Please create Price chart for customer " & clsCommon.myCstr(txtVendorNo.Value) & " for Location " & clsCommon.myCstr(txtLocation.Value) & "  for item " & gv1.Rows(introw).Cells(colICode).Value & ".", Me.Text)
            gv1.CurrentRow.Cells(colICode).Value = Nothing
            gv1.CurrentRow.Cells(colUnit).Value = Nothing
            gv1.CurrentRow.Cells(colIName).Value = Nothing
            gv1.CurrentRow.Cells(colIHSN).Value = Nothing
            gv1.CurrentRow.Cells(colRate).Value = 0
            gv1.CurrentRow.Cells(colOrgRate).Value = 0
            gv1.CurrentRow.Cells(colTBaseAmt).Value = 0
            gv1.CurrentRow.Cells(colTTaxAmt).Value = 0

            Exit Sub
        End If

    End Sub
    Sub OpenItemUOMList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
        Dim qry As String = "select UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
        Dim whrCls As String = "Item_Code='" + strICode + "'"
        gv1.CurrentRow.Cells(colUnit).Value = clsCommon.ShowSelectForm("PS-BOItemUOMFndr", qry, "Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), "Code", isButtonClick)
    End Sub

    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
        If clsCommon.myLen(strICode) > 0 Then
            Dim qry As String = "select UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
            Dim whrCls As String = "Item_Code='" + strICode + "'"
            gv1.CurrentRow.Cells(colUnit).Value = clsCommon.ShowSelectForm("PS-BOUOMFndr", qry, "Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), "Code", isButtonClick)


            gv1.CurrentRow.Cells(ColAvailableQty).Value = clsItemLocationDetails.getBalance(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), txtLocation.Value, txtDocNo.Value, txtDate.Value, Nothing, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), 0)
            ItemPrice(gv1.CurrentRow.Cells(colICode).Value, gv1.CurrentRow.Cells(colUnit).Value, gv1.CurrentRow.Cells(colQty).Value, gv1.CurrentRow.Index)
            'End If
            'End If
            UpdateCurrentRow(gv1.CurrentRow.Index) ''-1 is for current row
            UpdateAllTotals()
        End If
    End Sub

    Private Function GetConvFactor(ByVal strUnit As String, ByVal strItem As String) As Double
        Dim dblConvF As Double = 0
        dblConvF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from tspl_item_uom_detail where Item_Code='" & strItem & "' and UOM_Code='" & strUnit & "'"))
        Return dblConvF
    End Function

    Private Sub SetBlankOfItemColumns()
        gv1.CurrentRow.Cells(colICode).Value = ""
        gv1.CurrentRow.Cells(colIName).Value = ""
        gv1.CurrentRow.Cells(colIHSN).Value = ""
        gv1.CurrentRow.Cells(colUnit).Value = ""
        gv1.CurrentRow.Cells(colOrgUnit).Value = ""
        'gv1.CurrentRow.Cells(colMRP).Value = 0
        'gv1.CurrentRow.Cells(colPriceDateColumn).Value = 0
        'gv1.CurrentRow.Cells(colPriceCOde).Value = 0
        'gv1.CurrentRow.Cells(colSchemeApplicable).Value = "No"
        'gv1.CurrentRow.Cells(colSchemeItem).Value = "No"
        'gv1.CurrentRow.Cells(colRate).Value = 0
        'gv1.CurrentRow.Cells(colMRP).Value = 0
        ''gv1.CurrentRow.Cells(colAssessableRate).Value = 0
    End Sub

    Private Sub gv1_UserAddedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserAddedRow
        For i As Integer = 0 To gv1.Rows.Count - 1
            gv1.Rows(0).Cells(0).Value = 1
            If i <> 0 Then
                gv1.Rows(i).Cells(colLineNo).Value = i + 1
            End If
        Next
    End Sub

    Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer)
        'If clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value) > 0 AndAlso clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colRate).Value) > 0 Then
        gv1.Rows(IntRowNo).Cells(colAmt).Value = Math.Round(clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value) * clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colRate).Value), 2)
        gv1.Rows(IntRowNo).Cells(colAmountWithTax).Value = Math.Round(clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value) * clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colItemBasicPrice).Value), 2)
        'End If


    End Sub

    Private Function GetCurrentRowTotalTaxAmt(ByVal IntRowNo As Integer) As Double
        Dim dblTotTax As Double = 0
        For ii As Integer = 1 To 10
            Dim strii As String = clsCommon.myCstr(ii)
            If IntRowNo < 0 Then
                dblTotTax = dblTotTax + clsCommon.myCdbl(gv1.CurrentRow.Cells(clsCommon.myCstr("COLTAXAMT" + strii)).Value)
            Else
                dblTotTax = dblTotTax + clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXAMT" + strii)).Value)
            End If
        Next
        Return dblTotTax
    End Function

    Private Function GetCurrentRowSurTaxAmt(ByVal IntRowNo As Integer, ByVal intEndCol As Integer, ByVal strSurTaxCode As String) As Double
        For ii As Integer = 1 To intEndCol
            Dim strii As String = clsCommon.myCstr(ii)
            If IntRowNo < 0 Then
                If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strii)).Value)) = CompairStringResult.Equal Then
                    Return clsCommon.myCdbl(gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxAmt" + strii)).Value)
                End If
            Else
                If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTax" + strii)).Value)) = CompairStringResult.Equal Then
                    Return clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + strii)).Value)
                End If
            End If
        Next
        Return 0
    End Function

    Private Function GetCurrentRowOtherTaxAmt(ByVal IntRowNo As Integer, ByVal intEndCol As Integer, ByVal arrTaxableAuth As List(Of String)) As Double
        Dim dblRet As Double = 0
        For Each strTaxAuth As String In arrTaxableAuth
            For ii As Integer = 1 To intEndCol
                Dim strii As String = clsCommon.myCstr(ii)
                If IntRowNo < 0 Then
                    If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strii)).Value)) = CompairStringResult.Equal Then
                        dblRet = dblRet + clsCommon.myCdbl(gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxAmt" + strii)).Value)
                    End If
                Else
                    If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTax" + strii)).Value)) = CompairStringResult.Equal Then
                        dblRet = dblRet + clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + strii)).Value)
                    End If
                End If
            Next
        Next
        Return dblRet
    End Function

    Private Sub BlankTaxDetails(ByVal intRowNo As Integer)
        BlankTaxDetails(intRowNo, True)
    End Sub
    Private Sub BlankTaxDetails(ByVal intRowNo As Integer, ByVal isBlankRate As Boolean)
        For ii As Integer = 1 To 10
            Dim strII As String = clsCommon.myCstr(ii)
            If intRowNo < 0 Then
                gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strII)).Value = Nothing
                gv1.CurrentRow.Cells(clsCommon.myCstr("COLTAXBASEAMT" + strII)).Value = Nothing
                If isBlankRate Then
                    gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = Nothing
                End If
                gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxAmt" + strII)).Value = Nothing
                gv1.CurrentRow.Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = Nothing
                gv1.CurrentRow.Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = Nothing
                gv1.CurrentRow.Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = Nothing
            Else
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTax" + strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("COLTAXBASEAMT" + strII)).Value = Nothing
                If isBlankRate Then
                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = Nothing
                End If
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxAmt" + strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = Nothing
            End If
        Next

    End Sub
    Private Sub UpdateAllTotals()
        dblNetAmt = 0
        TotalQuantity = 0
        TotalCan = 0
        TotalBox = 0
        TotalCrate = 0
        Dim dblTotalDocAmt As Decimal = 0
        'Dim qryCrate As String
        'Dim qryCAN As String
        'Dim qryBox As String
        For i As Int16 = 0 To gv1.Rows.Count - 1
            dblNetAmt = dblNetAmt + clsCommon.myCdbl(gv1.Rows(i).Cells(colAmt).Value)
            TotalQuantity = TotalQuantity + clsCommon.myCdbl(gv1.Rows(i).Cells(colQty).Value)
            dblTotalDocAmt = dblTotalDocAmt + clsCommon.myCdbl(gv1.Rows(i).Cells(colAmountWithTax).Value)
            'qryCrate = clsDBFuncationality.getSingleValue("select * from tspl_unit_master where unit_code='" & clsCommon.myCstr(gv1.Rows(i).Cells(colUnit).Value) & "'  and crate_type='Y'")
            'qryCAN = clsDBFuncationality.getSingleValue("select * from tspl_unit_master where unit_code='" & clsCommon.myCstr(gv1.Rows(i).Cells(colUnit).Value) & "'  and can_type='Y'")
            'qryBox = clsDBFuncationality.getSingleValue("select * from tspl_unit_master where unit_code='" & clsCommon.myCstr(gv1.Rows(i).Cells(colUnit).Value) & "'  and Box_type='Y'")

            If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(i).Cells(colUnit).Value), "Can") = CompairStringResult.Equal Then
                TotalCan = TotalCan + clsCommon.myCdbl(gv1.Rows(i).Cells(colQty).Value)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(i).Cells(colUnit).Value), "Box") = CompairStringResult.Equal Then
                TotalBox = TotalBox + clsCommon.myCdbl(gv1.Rows(i).Cells(colQty).Value)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(i).Cells(colUnit).Value), "Crate") = CompairStringResult.Equal Then
                'TotalCrate = TotalCrate + clsCommon.myCdbl(gv1.Rows(i).Cells(colQty).Value)
            End If

            ''richa agarwal 29 May,2020 VIJ/01/06/20-001157
            Dim dblTotalCrateRowWise As Double = 0
            If clsCommon.myLen(clsCommon.myCstr(gv1.Rows(i).Cells(colICode).Value)) > 0 Then
                Dim ItemCrateType As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IS_CrateType  from TSPL_ITEM_MASTER Where Item_Code  ='" & clsCommon.myCstr(gv1.Rows(i).Cells(colICode).Value) & "'"))
                If ItemCrateType = 1 Then
                    Dim IsStockingUnit As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stocking_Unit from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(gv1.Rows(i).Cells(colICode).Value) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code  ='" & clsCommon.myCstr(gv1.Rows(i).Cells(colUnit).Value) & "'"))
                    Dim CrateConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(gv1.Rows(i).Cells(colICode).Value) & "' and tspl_unit_master.Crate_Type ='Y' "))
                    Dim ItemConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(gv1.Rows(i).Cells(colICode).Value) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code ='" & clsCommon.myCstr(gv1.Rows(i).Cells(colUnit).Value) & "' "))

                    If CrateConvFactor > 0 And ItemConvFactor > 0 Then
                        Dim DispatchQty As Double = gv1.Rows(i).Cells(colQty).Value * ItemConvFactor
                        If DispatchQty >= CrateConvFactor Then
                            dblTotalCrateRowWise = Math.Floor(DispatchQty / CrateConvFactor)
                        Else
                            dblTotalCrateRowWise = 0
                        End If
                    Else
                        clsCommon.MyMessageBoxShow(Me, "Please fill conversion factor for this unit at line no." & i + 1 & "")
                    End If
                End If
            End If


            TotalCrate = TotalCrate + dblTotalCrateRowWise




        Next
        lblTotRAmt1.Text = Math.Round(clsCommon.myCdbl(dblNetAmt), 2)
        lblTotalDocAmt.Text = Math.Round(clsCommon.myCdbl(dblTotalDocAmt), 2)
        txtCan.Text = Math.Round(clsCommon.myCdbl(TotalCan), 2)
        txtBox.Text = Math.Round(clsCommon.myCdbl(TotalBox), 2)
        txtCrate.Text = Math.Round(clsCommon.myCdbl(TotalCrate), 2)

    End Sub

    'Private Function GetBaseOtherTaxableAmount(ByVal intEndCol As Integer) As Double
    ''Dim dblRetVal As Double = 0
    ''For ii As Integer = 0 To intEndCol - 1
    ''    If clsCommon.myCBool(gv2.Rows(ii).Cells(colTIsTaxable).Value) Then
    ''        dblRetVal = dblRetVal + clsCommon.myCdbl(clsCommon.myCstr(gv2.Rows(ii).Cells(colTTaxAmt).Value))
    ''    End If
    ''Next
    ''Return dblRetVal
    'End Function

    'Private Function GetBaseTaxAmount(ByVal intEndCol As Integer, ByVal strSurTaxCode As String) As Double
    'For ii As Integer = 0 To intEndCol
    '    If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gv2.Rows(ii).Cells(colTTaxAutCode).Value)) = CompairStringResult.Equal Then
    '        Return clsCommon.myCdbl(clsCommon.myCstr(gv2.Rows(ii).Cells(colTTaxAmt).Value))
    '    End If
    'Next
    'Return 0
    'End Function

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub

    Sub AddNew()
        'VendorCodeForChangeIndent = ""
        lblCancelStatus.Text = ""
        lblCreatedDateAndTime.Text = ""
        lblUploadingDate.Text = ""
        Is_Cancelled = 0
        FlagCreateDo = False
        btnCopy.Enabled = True
        DOStatus = 0
        BookingStatus = 0
        txtDate.Value = clsCommon.GETSERVERDATE()
        GSTStatus = clsERPFuncationality.GetGSTStatus(txtDate.Value)
        'chkIsTaxable.Enabled = True
        BlankAllControls()
        LoadBlankGrid()
        ' LoadBlankGridTax()
        'LoadBlankGridAC()
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        btnCopy.Enabled = True
        txtDate.Focus()
        gv1.Rows.AddNew()
        'gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = RowTypeItem

        txtDate.Enabled = True
        txtVendorNo.Enabled = True
        txtLocation.Enabled = True
        txtDocNo.Value = ""
        txtShipToLocation.Value = ""
        lblShipToLocation.Text = ""
        lblDONumber.Text = ""
        txtRouteNo.Value = ""
        lblRouteDesc.Text = ""
        txtVendorNo.Value = ""
        lblVendorName.Text = ""
        lblroutecode.Text = ""
        lblroutename.Text = ""
        lblvehiclecode.Text = ""
        lblvehicleName.Text = ""
        'txtLocation.Value = ""
        'lblLocation.Text = ""
        txtPriceCode.Text = ""
        txtPONo.Text = ""
        txtCan.Text = 0
        txtCrate.Text = 0
        txtBox.Text = 0
        txtCustPODate.Checked = False
        txtCustPODate.Value = clsCommon.GETSERVERDATE()

        txtEx_Factory_Date.Checked = False
        txtEx_Factory_Date.Value = clsCommon.GETSERVERDATE()
        LblUpdatedVehicleCode.Text = ""
        LblUpdatedVehicleDesc.Text = ""
        chkSampling.Checked = False
        ItemTypePanel.Enabled = True
        If ShowBookingTypeDropDownonDairyBookingCustomer = True Then
            cmbBookingType.Visible = True
            MyLabel21.Visible = True
            MyLabel2.Text = "Booth Station"
            txtSalesman.Visible = False
            lblSalesman.Visible = False
            lblBoothStation.Visible = True
        Else
            cmbBookingType.Visible = False
            MyLabel21.Visible = False
            MyLabel2.Text = "Salesman"
            txtSalesman.Visible = True
            lblSalesman.Visible = True
            lblBoothStation.Visible = False
        End If
        'LoadBookingType()
        cmbBookingType.Text = "Select"
        lblBoothStation.Text = ""
        ''richa VIJ/03/12/19-000091
        lblCreditLimit.Text = ""
        lblAdvanceSecurity.Text = ""
        lblReverseAdvanceSec.Text = ""
        lblPendingDO.Text = ""
        lblLedgerOutstanding.Text = ""
        lblShortcloseDO.Text = ""
        lblRefund.Text = ""
        lblReverseRefund.Text = ""
        lblARSecurity.Text = ""
        lblTotalOutstansing.Text = ""
        chkGatePass.Checked = False
        IsLoadBookingType = False
        If ShowBookingTypeDropDownonDairyBookingCustomer Then
            txtVendorNo.Focus()
        End If
    End Sub

    Function AllowToSave(ByVal trans As SqlTransaction) As Boolean
        Try

            'Sanjay Ticket No- ERO/12/07/18-000371
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                If Is_Cancelled = 1 Then
                    clsCommon.MyMessageBoxShow(Me, "Booking Cancelled,Can not Update", Me.Text)
                    'Exit Function
                    Return False
                End If
            End If
            'Sanjay Ticket No- ERO/12/07/18-000371

            If clsCommon.myLen(txtVendorNo.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Customer", Me.Text)
                txtVendorNo.Focus()
                Return False
            End If

            If clsCommon.myLen(txtVendorNo.Value) > 0 Then
                If clsCommon.myLen(strRoutecode) = 0 Then
                    Throw New Exception("Please Map Route for customer ")
                    blnSaveTotalQTy = False
                    Exit Function
                End If
            End If

            If clsCommon.myLen(txtVendorNo.Value) > 0 Then
                If clsCommon.myLen(strVehicleCode) = 0 Then
                    Throw New Exception("Please enter Vehicle ")
                    blnSaveTotalQTy = False
                    Exit Function
                End If
            End If

            If clsCommon.myLen(txtLocation.Value) = 0 Then
                Throw New Exception("Please enter Location ")
                Exit Function
            End If


            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                Dim STRSQL As String = "select count(*) as cc from TSPL_BOOKING_DETAIL left join TSPL_BOOKING_MATSER ON TSPL_BOOKING_MATSER.Document_No=TSPL_BOOKING_DETAIL.Document_No left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.cust_code=TSPL_BOOKING_DETAIL.cust_code where TSPL_BOOKING_MATSER.From_Screen_Code ='BOOK-DS-CU' and TSPL_BOOKING_MATSER.location_code ='" & txtLocation.Value & "' and TSPL_BOOKING_DETAIL.Cust_Code='" & txtVendorNo.Value & "' AND convert(date,TSPL_BOOKING_MATSER.Document_Date ,103)=convert(date,'" & txtDate.Value & "',103) and TSPL_CUSTOMER_MASTER.customer_category<>'Others'"
                Dim TempBookingExist As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(STRSQL, trans))
                If TempBookingExist > 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Booking already exist for Date [" + clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") + "]", Me.Text)
                    btn_ChangeIndent.Focus()
                    Return False
                End If
            End If

            If Not isNewEntry AndAlso clsCommon.myLen(txtDocNo.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Booking Not found to save", Me.Text)
                txtDocNo.Focus()
                Return False
            End If

            If ShowBookingTypeDropDownonDairyBookingCustomer = True Then
                If clsCommon.CompairString(cmbBookingType.Text, "Select") = CompairStringResult.Equal Then
                    Throw New Exception("Please Select Booking Type ")
                End If

            End If

            ''richa agarwal VIJ/28/11/19-000081 
            If EnableCustomerPODetailonDairyBooking = 1 Then
                If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_CUSTOMER_GROUP_MASTER.PONOMandatory  from TSPL_CUSTOMER_MASTER left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code =TSPL_CUSTOMER_MASTER.Cust_Group_Code where TSPL_CUSTOMER_MASTER.cust_code='" & txtVendorNo.Value & "'", trans)), "1") = CompairStringResult.Equal Then
                    If clsCommon.myLen(txtPONo.Text) = 0 Then
                        Throw New Exception("Please enter Customer PO No ")
                    End If
                    If txtCustPODate.Checked = False Then
                        Throw New Exception("Please select Customer PO Date ")
                    End If
                End If
            End If

            '''''''
            Dim dblQuantity As Double = 0
            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
                Dim strIName As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colIName).Value)
                Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
                Dim AvgQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(ColAvgQty).Value)
                Dim dblrate As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colRate).Value)
                Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)
                Dim strRemarks As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colRemarks).Value)
                dblQuantity = dblQuantity + dblQty
                If (clsCommon.myLen(strICode) > 0) Then
                    If clsCommon.myLen(strUOM) <= 0 Then
                        common.clsCommon.MyMessageBoxShow(Me, "Please enter Booked Quantity UOM for " + strIName + ". At Line No" + clsCommon.myCstr(ii + 1), Me.Text)
                        Return False
                    End If
                    If ShowBookingTypeDropDownonDairyBookingCustomer = True Then
                        If dblQty > 0 AndAlso dblrate <= 0 Then
                            clsCommon.MyMessageBoxShow(Me, "Please enter Booked Rate for " + strIName + ". At Line No" + clsCommon.myCstr(ii + 1), Me.Text)
                            Return False
                        End If
                    Else
                        If dblQty <= 0 Then
                            common.clsCommon.MyMessageBoxShow(Me, "Please enter Booked Quantity for " + strIName + ". At Line No" + clsCommon.myCstr(ii + 1), Me.Text)
                            Return False
                        End If

                        If dblrate <= 0 Then
                            clsCommon.MyMessageBoxShow(Me, "Please enter Booked Rate for " + strIName + ". At Line No" + clsCommon.myCstr(ii + 1), Me.Text)
                            Return False
                        End If
                    End If


                    'Sanjay Ticket No- BHA/28/06/18-000106 Client- Bharat Dairy, Setting for check Average Quantity ''richa this check will not be worked for those booking which are created through Mobile app ERO/27/08/19-001005
                    If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(BookingThrough,'')  from TSPL_BOOKING_MATSER   where Document_No ='" & txtDocNo.Value & "' ", trans)), "App") <> CompairStringResult.Equal Then
                        If CheckAvgQtyOnDairyBooking = True Then
                            If (FlagCreateDo = False) And (FlagFirstRecord = False) Then 'And (AvgQty > 0)
                                ''  ERO/29/07/19-000971 by Balwinder on 30/07/2019
                                If Math.Abs(dblQty - AvgQty) > SettDairyBookingTolleranceQty Then
                                    If common.clsCommon.MyMessageBoxShow(Me, "Booking Quantity Should be in Average Quantity [" + clsCommon.myCstr(IIf(AvgQty - SettDairyBookingTolleranceQty < 0, 0, (AvgQty - SettDairyBookingTolleranceQty))) + " - " + clsCommon.myCstr(AvgQty + SettDairyBookingTolleranceQty) + " ] for " + strIName + ". At Line No" + clsCommon.myCstr(ii + 1) + vbNewLine + " Do you want to continue? ", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                                        Return False
                                    Else
                                        Dim frm As New FrmFreeTxtBox1
                                        frm.Text = "Remarks"
                                        frm.strRmks = strRemarks
                                        frm.ShowDialog()
                                        gv1.Rows(ii).Cells(colRemarks).Value = frm.strRmks

                                    End If
                                End If

                                'If dblQty > AvgQty Then
                                '    If common.clsCommon.MyMessageBoxShow("Booking Quantity is more than Average Quantity for " + strIName + ". At Line No" + clsCommon.myCstr(ii + 1) + vbNewLine + " Do you want to continue? ", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                                '        Return False
                                '    Else
                                '        Dim frm As New FrmFreeTxtBox1
                                '        frm.Text = "Remarks"
                                '        frm.strRmks = strRemarks
                                '        frm.ShowDialog()
                                '        gv1.Rows(ii).Cells(colRemarks).Value = frm.strRmks

                                '    End If
                                'End If

                                ''Sanjay Ticket No- ERO/12/07/18-000371  Client - Erode, Message on Less Qty than Average
                                'If dblQty < AvgQty Then
                                '    If common.clsCommon.MyMessageBoxShow("Booking Quantity is less than Average Quantity for " + strIName + ". At Line No" + clsCommon.myCstr(ii + 1) + vbNewLine + " Do you want to continue? ", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                                '        Return False
                                '    Else
                                '        Dim frm As New FrmFreeTxtBox1
                                '        frm.Text = "Remarks"
                                '        frm.strRmks = strRemarks
                                '        frm.ShowDialog()
                                '        gv1.Rows(ii).Cells(colRemarks).Value = frm.strRmks

                                '    End If
                                'End If
                                ''Sanjay Ticket No- ERO/12/07/18-000371  Client - Erode

                            End If
                        End If
                    End If
                End If


                If (clsCommon.myLen(strICode) > 0) Then

                    For jj As Integer = 0 To gv1.Rows.Count - 1
                        If jj = ii Then
                            Continue For
                        End If
                        Dim strInnerICode As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colICode).Value)
                        Dim strInnerUOM As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colUnit).Value)

                        If clsCommon.CompairString(strICode, strInnerICode) = CompairStringResult.Equal Then
                            If ShowMulMRPOfSameItemOnDairyBookingCustomer = True AndAlso clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Customer_category,'') from tspl_customer_master where cust_code='" & clsCommon.myCstr(txtVendorNo.Value) & "' ", trans)), "Others") = CompairStringResult.Equal Then
                                Dim dblInnerRate As Double = clsCommon.myCdbl(gv1.Rows(jj).Cells(colRate).Value)
                                If clsCommon.CompairString(dblrate, dblInnerRate) = CompairStringResult.Equal Then
                                    Dim Msg As String = "Same Item Rate at Row No " + clsCommon.myCstr(ii + 1) + " And " + clsCommon.myCstr(jj + 1)
                                    common.clsCommon.MyMessageBoxShow(Msg)
                                    Return False
                                End If
                            Else
                                Dim Msg As String = "Same Item Exist at Row No " + clsCommon.myCstr(ii + 1) + " And " + clsCommon.myCstr(jj + 1)
                                common.clsCommon.MyMessageBoxShow(Msg)
                                Return False
                            End If


                        End If

                    Next
                End If
            Next
            If ShowBookingTypeDropDownonDairyBookingCustomer = True Then
                If dblQuantity <= 0 AndAlso AllowZeroQtyOnDairyBooking = False Then
                    clsCommon.MyMessageBoxShow(Me, "Please enter Qunatity at least in one row", Me.Text)
                    Return False
                End If
            End If
            '' 18 SEP,2020 NOT CHECKED CONDITION FOR CUSTOMER CATEGORY OTHERS AND NULL
            If PrintTruckSheetAfterGenerate = True AndAlso (clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Customer_category,'') from tspl_customer_master where cust_code='" & clsCommon.myCstr(txtVendorNo.Value) & "' ")), "Others") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Customer_category,'') from tspl_customer_master where cust_code='" & clsCommon.myCstr(txtVendorNo.Value) & "' ")), "") <> CompairStringResult.Equal) Then
                Dim isTruckSheetGenerated As Boolean = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select max(truckSheetGenerate) from TSPL_BOOKING_MATSER where convert(date,Document_Date ,103)=convert(date,'" & txtDate.Value & "',103)")) = 1, True, False)
                If isTruckSheetGenerated = True And chkGatePass.Checked = False Then
                    clsCommon.MyMessageBoxShow(Me, "Truck sheet has been generated for date " & txtDate.Value & "..Please create Gate Pass. ", Me.Text)
                    Return False
                End If
            End If

            UpdateAllTotals()
            'Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        RecordCount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_BOOKING_MATSER "))
        If RecordCount = 0 Then
            FlagFirstRecord = True
        Else
            FlagFirstRecord = False
        End If
        SavingData(False)
    End Sub

    Sub SavingData(ByVal ChekBtnPost As Boolean)
        If (SaveData(False)) Then
            If ChekBtnPost = False Then
                common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                btnAddNew.Focus()
            End If

        End If
    End Sub

    Private Function SaveData(ByVal isDoAbandomentNo As Boolean) As Boolean
        Try
            Dim qry As String = ""
            blnSaveTotalQTy = True
            BookingStatus = 0
            'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

            If (AllowToSave(Nothing)) Then

                Dim obj As New clsBookingEntryDairySale()
                ' done by priti ERO/12/07/18-000373 for sampling booking
                obj.IsSampling = IIf(chkSampling.Checked, 1, 0)
                obj.Document_No = txtDocNo.Value
                obj.Document_Date = txtDate.Value
                obj.location_code = txtLocation.Value
                obj.AgainstGatePass = IIf(chkGatePass.Checked, 1, 0)
                'obj.Cust_Group_Code = txtCustGrp.Value
                obj.Is_Taxable = 2   ' 2 for Taxable and NonTaxable item in a single booking
                If CreateCommonDairyDispatchforFreshAmbient = 1 Then
                    obj.TRANSACTION_TYPE = ""
                Else
                    obj.TRANSACTION_TYPE = IIf(rbtn_Fresh.IsChecked = True, "FS", "PS")
                End If

                obj.From_Screen_code = clsUserMgtCode.frmDairyBookingCustomer

                If EnableCustomerPODetailonDairyBooking = 1 Then
                    obj.SalesmanCode = txtSalesman.Value
                    obj.Cust_PO_No = txtPONo.Text
                    If txtCustPODate.Checked Then
                        obj.Podate = txtCustPODate.Value
                    End If
                End If
                If clsCommon.myLen(lblUploadingDate.Text) > 0 Then
                    obj.Uploading_date = lblUploadingDate.Text
                End If
                obj.TotalCAN = txtCan.Text
                obj.TotalCrate = txtCrate.Text
                obj.TotalBox = txtBox.Text

                If txtEx_Factory_Date.Checked = True Then
                    obj.Ex_Factory_Date = txtEx_Factory_Date.Value
                End If
                obj.Booking_Type = IIf(cmbBookingType.Text = "Select", "", cmbBookingType.Text)
                obj.Ship_To_Location = txtShipToLocation.Value
                obj.Arr = New List(Of clsBookingDetailDairySale)
                Dim intLine As Integer = 0
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New clsBookingDetailDairySale()
                    objTr.Booking_Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)

                    If (objTr.Booking_Qty) > 0 OrElse (AllowZeroQtyOnDairyBooking = True AndAlso clsCommon.myCdbl(lblTotRAmt1.Text) <= 0 AndAlso clsCommon.myLen(clsCommon.myCstr(grow.Cells(colICode).Value)) > 0) Then
                        intLine += 1
                        objTr.Line_No = grow.Cells(colLineNo).Value
                        'Dim objBookingCust As clsBookingTemp = TryCast(gv1.Columns(ii).Tag, clsBookingTemp)
                        objTr.Cust_Code = txtVendorNo.Value
                        '================Added by preeti gupta Against Ticket no[BHA/01/08/18-000206]
                        objTr.Route_No = lblroutecode.Text
                        '===========================================================
                        objTr.Sampling = 0
                        objTr.Loc_Code = txtLocation.Value
                        objTr.Total_Qty = TotalQuantity

                        'Dim objBookingitem As clsBookingTemp = TryCast(grow.Cells(ii).Tag, clsBookingTemp)
                        objTr.Vehicle_Code = strVehicleCode
                        objTr.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                        objTr.Short_Description = ""  'clsCommon.myCstr(grow.Cells(colIShortName).Value)
                        objTr.Unit_code = clsCommon.myCstr(grow.Cells(colUnit).Value)

                        objTr.PreviousBookingQty = clsCommon.myCdbl(grow.Cells(colPreviousQty).Value)

                        objTr.Tax_On_Amount = clsCommon.myCdbl(grow.Cells(colTBaseAmt).Value)
                        objTr.Tax_Amount = clsCommon.myCdbl(grow.Cells(colTTaxAmt).Value)
                        'sanjay

                        objTr.Item_Rate = clsCommon.myCdbl(grow.Cells(colRate).Value)

                        objTr.Disc_Scheme_Amount = clsCommon.myCdbl(grow.Cells(colDisc_Scheme_Amount).Value)
                        objTr.Disc_Scheme_Code = clsCommon.myCstr(grow.Cells(colDisc_Scheme_Code).Value)
                        objTr.Disc_Scheme_Pers = clsCommon.myCdbl(grow.Cells(colDisc_Scheme_Pers).Value)
                        objTr.SchemeType = clsCommon.myCstr(grow.Cells(colSchemeType).Value)
                        objTr.OrgRate = clsCommon.myCdbl(grow.Cells(colOrgRate).Value)
                        objTr.SellingPrice = clsCommon.myCdbl(grow.Cells(colSellingRate).Value)

                        objTr.Tax_NonTax = clsCommon.myCdbl(grow.Cells(colTax_NonTax).Value)
                        objTr.FreshAmbient = clsCommon.myCstr(grow.Cells(colFreshAmbient).Value)

                        'Sanjay Ticket No- ERO/12/07/18-000371
                        objTr.Remarks = clsCommon.myCstr(grow.Cells(colRemarks).Value)
                        'Sanjay Ticket No- ERO/12/07/18-000371
                        objTr.Price_with_Tax = clsCommon.myCdbl(grow.Cells(colItemBasicPrice).Value)
                        objTr.Amount_with_Tax = clsCommon.myCdbl(grow.Cells(colAmountWithTax).Value)

                        If BookingStatus = 0 Then
                            objTr.Booking_Status = 1
                        End If

                        If clsCommon.myLen(txtDocNo.Value) > 0 Then
                            BookingStatus = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select  Booking_Status from TSPL_BOOKING_DETAIL where Document_No='" & txtDocNo.Value & "'  and Cust_Code='" & objTr.Cust_Code & "'"))
                        End If

                        If BookingStatus = 3 Then
                            objTr.Booking_Status = 3
                        ElseIf BookingStatus = 5 Then
                            objTr.Booking_Status = 5
                        Else
                            objTr.Booking_Status = objTr.Booking_Status
                        End If
                        '''''''''
                        BookingStatus = objTr.Booking_Status

                        objTr.DocumentAmount = clsCommon.myCdbl(lblTotRAmt1.Text)
                        Dim DOCdateCurrent As Date? = Nothing
                        DOCdateCurrent = clsCommon.GETSERVERDATE()
                        ' Query to get scheme type of Item
                        Dim qryScheme As String = "Select TSPL_SCHEME_MASTER_NEW.Scheme_Type from TSPL_SCHEME_MASTER_NEW left outer join TSPL_SCHEME_DETAIL_NEW on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_MASTER_NEW.Scheme_Code "
                        qryScheme += " left outer join tspl_item_master on tspl_item_master.item_code=TSPL_SCHEME_DETAIL_NEW.Item_Code "
                        qryScheme += " where TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select scheme_code from (select ROW_NUMBER() over (partition by scheme_type order by start_date desc) as sno, TSPL_SCHEME_MASTER_NEW.Scheme_Code from "
                        qryScheme += " TSPL_SCHEME_MASTER_NEW where TSPL_SCHEME_MASTER_NEW.Scheme_Type='Quantitive' and TSPL_SCHEME_MASTER_NEW.Status='Active' and  TSPL_SCHEME_MASTER_NEW.Start_Date<='" & clsCommon.GetPrintDate(DOCdateCurrent, "dd/MMM/yyyy") & "'  and (TSPL_SCHEME_MASTER_NEW.End_Date >= '" & clsCommon.GetPrintDate(DOCdateCurrent, "dd/MMM/yyyy") & "'  or TSPL_SCHEME_MASTER_NEW.End_date is null) and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select TSPL_SCHEME_DETAIL_NEW.Scheme_Code from "
                        qryScheme += " TSPL_SCHEME_DETAIL_NEW left outer join TSPL_SCHEME_BENEFICIARY on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_BENEFICIARY.Scheme_Code where MainItem_Code='" + clsCommon.myCstr(grow.Cells(colICode).Value) + "' and Cust_Code='" + txtVendorNo.Value + "'))a where a.sno=1)"
                        qryScheme += " and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select Scheme_Code from TSPL_SCHEME_BENEFICIARY where Cust_Code='" + objTr.Cust_Code + "') and TSPL_SCHEME_MASTER_NEW.Status='Active'"
                        qryScheme += " and TSPL_SCHEME_DETAIL_NEW.MainItem_Code='" + clsCommon.myCstr(grow.Cells(colICode).Value) + "' "


                        qryScheme += " order by TSPL_SCHEME_MASTER_NEW.Scheme_Code"
                        Dim SchemeType As String = clsDBFuncationality.getSingleValue(qryScheme)
                        If clsCommon.myLen(clsCommon.myCstr(SchemeType)) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(SchemeType), "Quantitive") = CompairStringResult.Equal Then
                            Dim objD As clsSchemeApplyOnDairy = Nothing
                            objD = clsSchemeApplyOnDairy.GetSchemeData(clsCommon.myCstr(grow.Cells(colICode).Value), clsCommon.myCstr(grow.Cells(colUnit).Value), clsCommon.myCdbl(grow.Cells(colQty).Value), txtVendorNo.Value, clsCommon.myCstr(grow.Cells(colSchemeType).Value), Nothing, Nothing)

                            If objD IsNot Nothing AndAlso objD.Arr.Count > 0 Then
                                For Each objtrScheme As clsSchemeApplyOnDairy In objD.Arr
                                    objTr.SchemeType = objtrScheme.schm_Type
                                    objTr.Scheme_Item_Code = objtrScheme.Schm_Icode
                                    objTr.Scheme_Qty = objtrScheme.Schm_Qty
                                    objTr.Scheme_Item_UOM = objtrScheme.Schm_Item_Uom
                                    objTr.Scheme_Code = objtrScheme.Schm_Code
                                Next

                            End If
                        End If
                        'End of Scheme Type of Detail

                    End If
                    If (clsCommon.myLen(objTr.Cust_Code) > 0) Then
                        obj.Arr.Add(objTr)
                    End If
                Next

                If (clsCommon.myCdbl(lblTotRAmt1.Text)) > 0 Then
                    If AllowWo_Outstanding = False Then
                        If CheckOutstandingOnbooking = 1 AndAlso ((BookingStatus = 1 OrElse BookingStatus = 2) AndAlso (BookingStatus <> 5)) Then
                            ' CustomerOutstandingAmount(txtVendorNo.Value, Nothing)
                        End If
                    End If
                End If
                obj.Credit_Limit = clsCommon.myCdbl(lblCreditLimit.Text)
                obj.Advance_Security = clsCommon.myCdbl(lblAdvanceSecurity.Text)
                obj.Revese_Adv_Security = clsCommon.myCdbl(lblReverseAdvanceSec.Text)
                obj.AR_Credit_Security = clsCommon.myCdbl(lblARSecurity.Text)
                obj.Pending_Posted_DO = clsCommon.myCdbl(lblPendingDO.Text)
                obj.UnPostedDispatch = clsCommon.myCdbl(lblShortcloseDO.Text)
                obj.Ledger_Outstansing = clsCommon.myCdbl(lblLedgerOutstanding.Text)
                obj.Refund_Security = clsCommon.myCdbl(lblRefund.Text)
                obj.Reverse_Refund_Sec = clsCommon.myCdbl(lblReverseRefund.Text)
                obj.Total_Outstanding = clsCommon.myCdbl(lblTotalOutstansing.Text)

                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please Fill at list one Item", Me.Text)
                    Return False
                End If
                If (obj.SaveData(obj, isNewEntry)) = True Then

                    Dim intSampling As Integer = 0

                    Dim dblQty As Double = 0
                    Dim dblRate As Double = 0
                    Dim dblAmount As Double = 0
                    Dim dblTotal As Double = 0
                    qry = "Delete from TSPL_TRANSACTION_APPROVAL where Document_No='" & obj.Document_No & "' "
                    clsDBFuncationality.ExecuteNonQuery(qry)
                    'For ii As Integer = 8 To gv1.Columns.Count - 1
                    'Dim objBookingCust As clsBookingTemp = TryCast(gv1.Columns(ii).Tag, clsBookingTemp)
                    If (clsCommon.myCdbl(lblTotRAmt1.Text)) > 0 Then
                        '' Performa Invoice No save only in case of GKD client
                        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GKD") = CompairStringResult.Equal Then
                            Dim strPerformaInvoiceNo = clsERPFuncationality.GetNextCode(Nothing, clsCommon.myCDate(txtDate.Value), clsDocType.frmPerformaInvoiceBooking, "", txtLocation.Value)
                            qry = "Update TSPL_BOOKING_DETAIL set DocumentAmount=" & clsCommon.myCdbl(lblTotRAmt1.Text) & ",Performance_Invoice_no='" & strPerformaInvoiceNo & "' where   Document_No='" & obj.Document_No & "' and Cust_Code='" & txtVendorNo.Value & "' and    Loc_Code='" & clsCommon.myCstr(txtLocation.Value) & "' and isnull(Scheme_Item,'N')='N' and isnull(FOC_Item,0)=0 "
                            clsDBFuncationality.ExecuteNonQuery(qry)
                        End If

                        If AllowWo_Outstanding = False Then
                            'Booking STATUS 1 -open 2 - Park 3 - approved 4 - posted 5 - rejected

                            If CheckOutstandingOnbooking = 1 AndAlso ((BookingStatus = 1 OrElse BookingStatus = 2) AndAlso (BookingStatus <> 5)) Then
                                If CustomerOutstandingAmount(txtVendorNo.Value, Nothing) = False Then
                                    qry = "Update TSPL_BOOKING_DETAIL set CreditApproval_Reqd='Y',Booking_Status=2 where   Document_No='" & obj.Document_No & "' and Cust_Code='" & txtVendorNo.Value & "' and    Loc_Code='" & clsCommon.myCstr(txtLocation.Value) & "'"
                                    clsDBFuncationality.ExecuteNonQuery(qry)

                                    qry = "insert into TSPL_TRANSACTION_APPROVAL(Screen_Name,Program_Code,Document_No,Doc_Date,approval_type,Approve,Created_By,Created_Date,Modified_By,Modified_Date,Comp_Code,Cust_Code,Loc_Code) " &
                               "values ('Booking Dairy','" & clsUserMgtCode.frmbookingdairy & "','" & obj.Document_No & "', " &
                               "'" & clsCommon.GetPrintDate(txtDate.Value, "dd-MMM-yyyy") & "','Credit Limit',0,'" + objCommonVar.CurrentUserCode + "', " &
                               "'" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt") + "', " &
                               "'" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt") + "', " &
                               "'" & objCommonVar.CurrentCompanyCode & "','" & txtVendorNo.Value & "','" & txtLocation.Value & "')"
                                    clsDBFuncationality.ExecuteNonQuery(qry)

                                    '' create sms content
                                    Dim dtSMSEmail As DataTable = clsDBFuncationality.GetDataTable("SELECT SMS_Text,EMail_Text from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmbookingdairy + "'")
                                    Dim strSMSContent As String = ""
                                    Dim strEMailContent As String = ""
                                    If dtSMSEmail.Rows.Count > 0 Then
                                        strSMSContent = clsCommon.myCstr(dtSMSEmail.Rows(0).Item("SMS_Text"))
                                        strEMailContent = clsCommon.myCstr(dtSMSEmail.Rows(0).Item("EMail_Text"))
                                    End If

                                    'SMSCode Start
                                    If clsCommon.myLen(strSMSContent) > 0 Then
                                        Dim objSMSH As New clsSMSHead()
                                        objSMSH.SMS_Text = strSMSContent
                                        objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy"))
                                        objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Doc_No, clsCommon.myCstr(txtDocNo.Value))
                                        objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.BookingAmount, clsCommon.myCdbl(lblTotRAmt1.Text))
                                        objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Cust_Code, clsCommon.myCstr(txtVendorNo.Value))
                                        objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Cust_Name, clsCommon.myCstr(lblVendorName.Text))
                                        objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Loc_Code, clsCommon.myCstr(txtLocation.Value))
                                        objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Loc_Name, clsCommon.myCstr(lblLocation.Text))
                                        objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Approval_Type, "Approved")
                                        CreateSMSContent(objSMSH.SMS_Text, Nothing)
                                        'obj1.SMS_Content = objSMSH.SMS_Text
                                    End If

                                    'email content Start
                                    If clsCommon.myLen(strEMailContent) > 0 Then
                                        Dim objEmailH As New clsEMailHead
                                        objEmailH.Email_Text = strEMailContent
                                        objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy"))
                                        objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Doc_No, txtDocNo.Value)
                                        objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.BookingAmount, clsCommon.myCdbl(lblTotRAmt1.Text))
                                        objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Cust_Code, clsCommon.myCstr(txtVendorNo.Value))
                                        objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Cust_Name, clsCommon.myCstr(lblVendorName.Text))
                                        objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Loc_Code, clsCommon.myCstr(txtLocation.Value))
                                        objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Loc_Name, clsCommon.myCstr(lblLocation.Text))
                                        objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Approval_Type, "Approved")
                                        CreateEmailContent(objEmailH.Email_Text, Nothing)
                                    End If


                                Else

                                    qry = "Update TSPL_BOOKING_DETAIL set CreditApproval_Reqd='N',Booking_Status=1 where   Document_No='" & obj.Document_No & "' and Cust_Code='" & txtVendorNo.Value & "' and    Loc_Code='" & clsCommon.myCstr(txtLocation.Value) & "' "
                                    clsDBFuncationality.ExecuteNonQuery(qry)
                                End If
                            End If
                        End If
                    End If
                    ''============25/04/2018 Send Notification Alert for Ex Factory Date Entry, It show alert date befor one day of EX_factory_Date===========
                    Dim strNotificationOn As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_On from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmDairyBookingCustomer + "'", Nothing))
                    If clsCommon.CompairString(strNotificationOn, "S") = CompairStringResult.Equal Then
                        If txtEx_Factory_Date.Checked = True Then
                            Dim Booking_Id As String = obj.Document_No
                            Dim Booking_Date As DateTime = clsCommon.myCDate(txtDate.Value)
                            Dim Ex_Factory_Date As DateTime = Nothing
                            If clsCommon.myLen(txtEx_Factory_Date.Value) > 0 Then
                                Ex_Factory_Date = clsCommon.myCDate(txtEx_Factory_Date.Value)
                            End If
                            CreateNotificationContentEMP(Booking_Id, Booking_Date, Ex_Factory_Date, Nothing)
                        End If
                    End If
                    ''=============================================
                    'Next
                    LoadData(obj.Document_No, NavigatorType.Current)
                    Return True
                End If
                'FlagCopy = False
            Else
                Return False
            End If
            blnSaveTotalQTy = True
        Catch ex As Exception
            blnSaveTotalQTy = True
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return False
    End Function

    Public Shared Sub CreateSMSContent(ByVal strSMSContent As String, ByVal trans As SqlTransaction)
        If clsCommon.myLen(strSMSContent) > 0 Then
            Dim objSMSH As New clsSMSHead()
            objSMSH.SMS_Text = strSMSContent
            objSMSH.arrMobilNo = New List(Of String)()
            objSMSH.SaveData(clsUserMgtCode.frmbookingdairy, objSMSH, trans)
            objSMSH = Nothing
        End If
    End Sub
    Public Shared Sub CreateEmailContent(ByVal strEmailContent As String, ByVal trans As SqlTransaction)
        'MailCode Start
        If clsCommon.myLen(strEmailContent) > 0 Then
            Dim qry As String = "SELECT EMail_Subject from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmbookingdairy + "'"
            Dim EmailSubject As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT EMail_Subject from TSPL_ES_Content where Form_ID='" & clsUserMgtCode.frmbookingdairy & "'", trans))
            Dim objSMSH As New clsEMailHead()
            objSMSH.Email_Text = strEmailContent
            objSMSH.Email_Subject = EmailSubject
            objSMSH.arrEMail = New List(Of String)()
            objSMSH.SaveData(clsUserMgtCode.frmbookingdairy, objSMSH, trans)
            objSMSH = Nothing
        End If
    End Sub


    'Private Function CustomerOutstandingAmount(ByVal strCustomer As String, ByVal dblTotal As Double, ByVal Trans As SqlTransaction, ByVal strDoc As String, ByVal blnBeforSave As Boolean, Optional ByVal intColumn As Integer = 0) As Boolean
    '    Dim qry As String = String.Empty
    '    Try
    '        Dim dblOutstandingAmt As Double = 0
    '        Dim dblCreditLimit As Double = 0
    '        Dim dblSecurityAmount As Double = 0
    '        Dim dblPendingDeliveryAmt As Double = 0
    '        Dim dblAmt As Double = 0

    '        If CheckOutstandingOnbooking = 0 Then
    '            qry = "select sum(case when RI=1 then 1 else -1  end *  OutStandingAmt) from ( " & _
    '       "select SUM(isnull(TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Total_Amt,0) ) as OutStandingAmt , 1 as RI from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE " & _
    '        "where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.posted=1 and Sampling=0  and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Customer_Code='" & strCustomer & "'  and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No not in ('" & strDoc & "')  " & _
    '       " union all " & _
    '      "select isnull(SUM(isnull(TSPL_RECEIPT_DETAIL.Applied_Amount,0) ),0) as OutStandingAmt ,-1 as RI  from   " & _
    '      "TSPL_Customer_Invoice_Head left outer join  TSPL_RECEIPT_DETAIL on TSPL_Customer_Invoice_Head.Document_No=TSPL_RECEIPT_DETAIL.Document_No  left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No=TSPL_RECEIPT_DETAIL.Receipt_No " & _
    '      "where  TSPL_RECEIPT_HEADER.Posted='Y'  and Against_Sale_No <> ''  and Customer_Code='" & strCustomer & "' " & _
    '      " union all " & _
    '      "select isnull(SUM(isnull(TSPL_RECEIPT_HEADER.Receipt_Amount,0) ),0) as OutStandingAmt ,-1 as RI  from  TSPL_RECEIPT_HEADER " & _
    '      "where  TSPL_RECEIPT_HEADER.Posted='Y'   and Receipt_Type='O' and Cust_Code='" & strCustomer & "' " & _
    '      " union all " & _
    '      "select isnull(SUM(isnull(TSPL_RECEIPT_HEADER.Receipt_Amount,0) ),0) as OutStandingAmt ,1 as RI  from  TSPL_RECEIPT_HEADER " & _
    '      "where  TSPL_RECEIPT_HEADER.Posted='Y'   and Receipt_Type='F' and Cust_Code='" & strCustomer & "' " & _
    '       " union all " & _
    '      "select isnull(SUM(isnull(TSPL_RECEIPT_HEADER.Receipt_Amount,0) ),0) as OutStandingAmt ,-1 as RI  from  TSPL_RECEIPT_HEADER " & _
    '      "where  TSPL_RECEIPT_HEADER.Posted='Y'  and Receipt_Type='P'  and SecurityDeposit='N'  and Cust_Code='" & strCustomer & "'" & _
    '      " union all " & _
    '      "select  sum(amount) as OutStandingAmt,-1 as RI from TSPL_BANK_GUARANTEE_MASTER where Type='Customer' and vendor_code='" & strCustomer & "' and Bank_Guarantee_Type='RC' " & _
    '      " union all " & _
    '      "select  sum(amount) as OutStandingAmt,1 as RI from TSPL_BANK_GUARANTEE_MASTER where Type='Customer' and vendor_code='" & strCustomer & "' and Bank_Guarantee_Type='RT' ) xxx "

    '        Else
    '            qry = "select sum(case when RI=1 then 1 else -1  end *  OutStandingAmt) from ( " & _
    '       "select SUM(isnull (isnull(TSPL_BOOKING_DETAIL.Booking_Qty,0) * isnull(TSPL_BOOKING_DETAIL.Item_Rate,0) ,0) ) as OutStandingAmt , 1 as RI from TSPL_BOOKING_DETAIL " & _
    '       "where  TSPL_BOOKING_DETAIL.Cust_Code='" & strCustomer & "'  and TSPL_BOOKING_DETAIL.Document_No not in ('" & strDoc & "')  " & _
    '       " union all " & _
    '      "select isnull(SUM(isnull(TSPL_RECEIPT_DETAIL.Applied_Amount,0) ),0) as OutStandingAmt ,-1 as RI  from   " & _
    '      "TSPL_Customer_Invoice_Head left outer join  TSPL_RECEIPT_DETAIL on TSPL_Customer_Invoice_Head.Document_No=TSPL_RECEIPT_DETAIL.Document_No  left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No=TSPL_RECEIPT_DETAIL.Receipt_No " & _
    '      "where  TSPL_RECEIPT_HEADER.Posted='Y'  and Against_Sale_No <> ''  and Customer_Code='" & strCustomer & "' " & _
    '      " union all " & _
    '      "select isnull(SUM(isnull(TSPL_RECEIPT_HEADER.Receipt_Amount,0) ),0) as OutStandingAmt ,-1 as RI  from  TSPL_RECEIPT_HEADER " & _
    '      "where  TSPL_RECEIPT_HEADER.Posted='Y'   and Receipt_Type='O' and Cust_Code='" & strCustomer & "' " & _
    '      " union all " & _
    '      "select isnull(SUM(isnull(TSPL_RECEIPT_HEADER.Receipt_Amount,0) ),0) as OutStandingAmt ,1 as RI  from  TSPL_RECEIPT_HEADER " & _
    '      "where  TSPL_RECEIPT_HEADER.Posted='Y'   and Receipt_Type='F' and Cust_Code='" & strCustomer & "' " & _
    '       " union all " & _
    '      "select isnull(SUM(isnull(TSPL_RECEIPT_HEADER.Receipt_Amount,0) ),0) as OutStandingAmt ,-1 as RI  from  TSPL_RECEIPT_HEADER " & _
    '      "where  TSPL_RECEIPT_HEADER.Posted='Y'  and Receipt_Type='P'  and SecurityDeposit='N'  and Cust_Code='" & strCustomer & "'" & _
    '      " union all " & _
    '      "select  sum(amount) as OutStandingAmt,-1 as RI from TSPL_BANK_GUARANTEE_MASTER where Type='Customer' and vendor_code='" & strCustomer & "' and Bank_Guarantee_Type='RC' " & _
    '      " union all " & _
    '      "select  sum(amount) as OutStandingAmt,1 as RI from TSPL_BANK_GUARANTEE_MASTER where Type='Customer' and vendor_code='" & strCustomer & "' and Bank_Guarantee_Type='RT' ) xxx "

    '        End If

    '        dblOutstandingAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, Trans))
    '        dblCreditLimit = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Credit_Limit from TSPL_CUSTOMER_MASTER where Cust_Code='" & strCustomer & "'", Trans))
    '        dblSecurityAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select SUM(Receipt_Amount) from TSPL_RECEIPT_HEADER where Receipt_Type='P' and  SecurityDepositType  in ('S')  and Posted='Y' and Cust_Code='" & strCustomer & "'", Trans))
    '        dblPendingDeliveryAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum(Total_Amt) from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE where  posted=0 and Sampling=0 and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No <> '" & strDoc & "' and Customer_Code='" & strCustomer & "'", Trans))

    '        dblAmt = dblCreditLimit + dblSecurityAmount - dblPendingDeliveryAmt - dblOutstandingAmt
    '        dblCustOutstandingAmt = dblAmt

    '        If blnSaveTotalQTy = True Then
    '            If dblAmt < dblTotal Then
    '                Dim dblNewCredtitLimit = dblAmt - dblTotal
    '                'common.clsCommon.MyMessageBoxShow("Please send for approval for increasing credit limit " + clsCommon.myCstr(dblNewCredtitLimit))
    '                Return False
    '            End If
    '        Else
    '            If dblAmt < dblTotal AndAlso dblTotal > 0 Then
    '                If blnPageLoad = False Then
    '                    Dim strCustName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER  where Cust_Code='" & strCustomer & "'", Trans))
    '                    clsCommon.MyMessageBoxShow("Please increase your credit limit for customer " + strCustName)
    '                End If
    '            End If
    '        End If

    '        Return True
    '    Catch ex As Exception
    '        Common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
    '    Finally
    '        qry = Nothing
    '    End Try
    'End Function

    ' done by priti BHA/15/10/18-000627

    Private Function CustomerOutstandingAmount(ByVal strCustomer As String, ByVal trans As SqlTransaction) As Boolean
        'Ticket No-MIL/08/04/19-000062,sanjay, Pass transaction object in function
        'Try


        '    'If throughShipment = False Then
        '    'PendingAfterApproval = 0
        '    Dim dblOutstandingAmt As Double = 0
        '    Dim dblCreditLimit As Double = 0
        '    Dim dblSecurityAmount As Double = 0
        '    Dim dblRefundAmount As Double = 0
        '    Dim dblReverseSecurityAmount As Double = 0
        '    Dim dblReverseRefundAmount As Double = 0
        '    Dim dblPendingDeliveryAmt As Double = 0
        '    Dim dblARSecurityAmt As Double = 0
        '    Dim dblAmt As Double = 0
        '    Dim qry As String = ""
        '    Dim dblShortCloseDoDispatch As Double = 0
        '    Dim dblBookingAmt As Double = 0
        '    Dim strCreditLimit As String = ""
        '    If DoNotConsiderCustomerCreditLimit = 1 Then
        '        strCreditLimit = " and CheckCreditLimit=1 "
        '    End If
        '    Dim strIsParent As String = clsDBFuncationality.getSingleValue("select Parent_Customer_YN from TSPL_CUSTOMER_MASTER where Cust_Code='" & strCustomer & "' ", trans)
        '    Dim strParentCust As String = clsDBFuncationality.getSingleValue("select Parent_Customer_No from TSPL_CUSTOMER_MASTER where Parent_Customer_YN='N' and Parent_Customer_No <> '' and Cust_Code='" & strCustomer & "'   and Credit_Limit=0", trans)
        '    If clsCommon.CompairString(strIsParent, "Y") = CompairStringResult.Equal Then
        '        strParentCust = strCustomer
        '    End If
        '    Dim strcustomerfilter As String = String.Empty
        '    strcustomerfilter = "'" + strCustomer + "'"
        '    If clsCommon.myLen(strParentCust) > 0 OrElse clsCommon.CompairString(strIsParent, "Y") = CompairStringResult.Equal Then

        '        Dim strDate As Date = txtDate.Value.AddDays(1)

        '        qry = "Select  ( SUM(convert(decimal(18,2),OpngBal)) + SUM(convert(decimal(18,2),DrAmt)) ) -SUM(convert(decimal(18,2),CrAmt))  as BalAmt From ( " &
        '            "Select MAX(TSPL_CUSTOMER_MASTER.Cust_Group_Code) as Cust_Group_Code, ACode, MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as AName, '' as CurrencyCode,  " &
        '            "null as ConvRate, SUM(DrAmt* Final.ConvRate)-SUM(CrAmt) as OpngBal, 0 as DrAmt, 0 as CrAmt, 0 as [Sales], 0 as CollectionRefund, 0 as DrNote,  " &
        '            "0 as CrNote, MAX(tspl_customer_master.Cust_Category_Code) as Cust_Category_Code,MAX(CUST_CATEGORY_DESC) as Cust_Category_Desc,  " &
        '            "MAX(tspl_customer_master.Cust_Type_Code) As Cust_Type_Code,MAX(Cust_Type_Desc) As Cust_Type_Desc from   " &
        '            "(" & clsCustomerMaster.GetCustomerBaseQry(False, False, "", False, "ConvRate", strcustomerfilter, True, clsCommon.GetPrintDate(txtDate.Value.AddDays(1), "dd/MMM/yyyy"), "", False, False, True, trans) & "   " &
        '            " ) Final left outer join TSPL_CUSTOMER_MASTER on final.ACode=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code " &
        '            "Left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No =Final.DocNo  LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=Final.Bank_Code " &
        '            "where  CONVERT(DATE,final.DocDate,103) <= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "' AND LEN(ACode)>0 and ACode in ('" & strCustomer & "')   AND TSPL_CUSTOMER_MASTER.Status='N' GROUP BY ACode " &
        '            ") XXX GROUP BY ACode ORDER BY ACode"


        '        dblOutstandingAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
        '        dblCreditLimit = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Credit_Limit from TSPL_CUSTOMER_MASTER where Cust_Code='" & strCustomer & "' " & strCreditLimit & "", trans))

        '        If DonotIncludeSecurityInCustomerOutstanding = False Then
        '            dblSecurityAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select SUM(Receipt_Amount) from TSPL_RECEIPT_HEADER where Receipt_Type='P' and  SecurityDeposit='Y'  and Posted='Y' and Cust_Code='" & strCustomer & "'", trans))
        '            dblReverseSecurityAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select SUM(Receipt_Amount) from TSPL_BANK_REVERSE inner join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No=TSPL_BANK_REVERSE.Document_No where Source_Type='AR' and  Receipt_Type='P' and  SecurityDeposit='Y'  and isnull(TSPL_BANK_REVERSE.Post,'N')='P' and TSPL_BANK_REVERSE.Cust_Code='" & strCustomer & "'", trans))

        '            dblRefundAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select SUM(Receipt_Amount) from TSPL_RECEIPT_HEADER where Receipt_Type='F' and SecurityDeposit='Y'  and Posted='Y' and Cust_Code='" & strCustomer & "'", trans))
        '            dblReverseRefundAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select SUM(Receipt_Amount) from TSPL_BANK_REVERSE inner join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No=TSPL_BANK_REVERSE.Document_No where Source_Type='AR' and Receipt_Type='F' and SecurityDeposit='Y'  and isnull(TSPL_BANK_REVERSE.Post,'N')='P' and TSPL_BANK_REVERSE.Cust_Code='" & strCustomer & "'", trans))
        '            dblARSecurityAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum(Document_Total) from tspl_customer_invoice_head where document_type='C'  and isnull(Against_Security_Receipt_No,'') <> '' and Status=1  and TSPL_Customer_Invoice_Head.Customer_Code='" & strCustomer & "'", trans))
        '        End If

        '        If rbtn_Fresh.IsChecked Then
        '            qry = "select sum(Total_Amt) from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE " &
        '            "where posted=1 and Document_No not in (Select isnull(TSPL_SD_SHIPMENT_DETAIL.Delivery_Code,'') from TSPL_SD_SHIPMENT_HEAD left outer join TSPL_SD_SHIPMENT_DETAIL on " &
        '            "TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE where TSPL_SD_SHIPMENT_HEAD.Status=1 ) " &
        '            "and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.customer_code='" & strCustomer & "' and  " &
        '            " isnull(TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Short_Close,'N')='N' "
        '            'and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Delivery_date >= '" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy") & "' "
        '            dblPendingDeliveryAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))

        '            qry = "select SUM(isnull(TSPL_SD_SHIPMENT_DETAIL.Item_Net_Amt,0) ) as OutStandingAmt , 1 as RI from TSPL_SD_SHIPMENT_HEAD  " &
        '            "left outer join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_HEAD.document_code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE " &
        '            "left outer join TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE on TSPL_SD_SHIPMENT_DETAIL.Delivery_Code=TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code " &
        '            "where  TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Customer_Code='" & strCustomer & "' and  " &
        '            "isnull(TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Short_Close,'N')='Y' and  TSPL_SD_SHIPMENT_HEAD.Status=0  "
        '            dblShortCloseDoDispatch = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))

        '        Else
        '            qry = "select sum(Total_Amt) from TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE " &
        '            "where posted=1 and document_code not in (Select isnull(TSPL_SD_SHIPMENT_DETAIL.Delivery_Code_PS,'') from TSPL_SD_SHIPMENT_HEAD left outer join TSPL_SD_SHIPMENT_DETAIL on " &
        '            "TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE where TSPL_SD_SHIPMENT_HEAD.Status=1 ) " &
        '            "and TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.customer_code='" & strCustomer & "' and  " &
        '            " isnull(TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Short_Close,'N')='N' "
        '            'and TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Delivery_date >= '" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy") & "' "
        '            dblPendingDeliveryAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))

        '            qry = "select SUM(isnull(TSPL_SD_SHIPMENT_DETAIL.Item_Net_Amt,0) ) as OutStandingAmt , 1 as RI from TSPL_SD_SHIPMENT_HEAD  " &
        '            "left outer join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_HEAD.document_code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE " &
        '            "left outer join TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE on TSPL_SD_SHIPMENT_DETAIL.Delivery_Code_PS=TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code " &
        '            "where  TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Customer_Code='" & strCustomer & "' and  " &
        '            "isnull(TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Short_Close,'N')='Y' and  TSPL_SD_SHIPMENT_HEAD.Status=0  "
        '            dblShortCloseDoDispatch = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))

        '        End If



        '    Else

        '        qry = "Select  ( SUM(convert(decimal(18,2),OpngBal)) + SUM(convert(decimal(18,2),DrAmt)) ) -SUM(convert(decimal(18,2),CrAmt))  as BalAmt From ( " &
        '            "Select MAX(TSPL_CUSTOMER_MASTER.Cust_Group_Code) as Cust_Group_Code, ACode, MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as AName, '' as CurrencyCode,  " &
        '            "null as ConvRate, SUM(DrAmt* Final.ConvRate)-SUM(CrAmt) as OpngBal, 0 as DrAmt, 0 as CrAmt, 0 as [Sales], 0 as CollectionRefund, 0 as DrNote,  " &
        '            "0 as CrNote, MAX(tspl_customer_master.Cust_Category_Code) as Cust_Category_Code,MAX(CUST_CATEGORY_DESC) as Cust_Category_Desc,  " &
        '            "MAX(tspl_customer_master.Cust_Type_Code) As Cust_Type_Code,MAX(Cust_Type_Desc) As Cust_Type_Desc from   " &
        '            "(" & clsCustomerMaster.GetCustomerBaseQry(False, False, "", False, "ConvRate", strcustomerfilter, True, clsCommon.GetPrintDate(txtDate.Value.AddDays(1), "dd/MMM/yyyy"), "", False, False, True, trans) & "   " &
        '            " ) Final left outer join TSPL_CUSTOMER_MASTER on final.ACode=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code " &
        '            "Left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No =Final.DocNo  LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=Final.Bank_Code " &
        '            "where  CONVERT(DATE,final.DocDate,103) <= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "' AND LEN(ACode)>0 and ACode in ('" & strCustomer & "')   AND TSPL_CUSTOMER_MASTER.Status='N' GROUP BY ACode " &
        '            ") XXX GROUP BY ACode ORDER BY ACode"


        '        dblOutstandingAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
        '        dblCreditLimit = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Credit_Limit from TSPL_CUSTOMER_MASTER where Cust_Code='" & strCustomer & "' " & strCreditLimit & "", trans))

        '        If DonotIncludeSecurityInCustomerOutstanding = False Then
        '            dblSecurityAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select SUM(Receipt_Amount) from TSPL_RECEIPT_HEADER where Receipt_Type='P' and SecurityDeposit='Y'  and Posted='Y' and Cust_Code='" & strCustomer & "'", trans))
        '            dblReverseSecurityAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select SUM(Receipt_Amount) from TSPL_BANK_REVERSE inner join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No=TSPL_BANK_REVERSE.Document_No where Source_Type='AR' and  Receipt_Type='P' and  SecurityDeposit='Y'  and isnull(TSPL_BANK_REVERSE.Post,'N')='P' and TSPL_BANK_REVERSE.Cust_Code='" & strCustomer & "'", trans))

        '            dblRefundAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select SUM(Receipt_Amount) from TSPL_RECEIPT_HEADER where Receipt_Type='F' and SecurityDeposit='Y'  and Posted='Y' and Cust_Code='" & strCustomer & "'", trans))
        '            dblReverseRefundAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select SUM(Receipt_Amount) from TSPL_BANK_REVERSE inner join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No=TSPL_BANK_REVERSE.Document_No where Source_Type='AR' and Receipt_Type='F' and SecurityDeposit='Y'  and isnull(TSPL_BANK_REVERSE.Post,'N')='P' and TSPL_BANK_REVERSE.Cust_Code='" & strCustomer & "'", trans))
        '            dblARSecurityAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum(Document_Total) from tspl_customer_invoice_head where document_type='C'  and isnull(Against_Security_Receipt_No,'') <> '' and Status=1  and TSPL_Customer_Invoice_Head.Customer_Code='" & strCustomer & "'", trans))
        '        End If

        '        qry = "select sum(Total_Amt) from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE " &
        '        "where posted=1 and Document_No not in (Select isnull(TSPL_SD_SHIPMENT_DETAIL.Delivery_Code,'') from TSPL_SD_SHIPMENT_HEAD left outer join TSPL_SD_SHIPMENT_DETAIL on " &
        '        "TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE where TSPL_SD_SHIPMENT_HEAD.Status=1 ) " &
        '        "and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.customer_code='" & strCustomer & "' and  " &
        '        " isnull(TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Short_Close,'N')='N' "
        '        'and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Delivery_date >= '" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy") & "' "
        '        dblPendingDeliveryAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))

        '        qry = "select SUM(isnull(TSPL_SD_SHIPMENT_DETAIL.Item_Net_Amt,0) ) as OutStandingAmt , 1 as RI from TSPL_SD_SHIPMENT_HEAD  " &
        '        "left outer join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_HEAD.document_code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE " &
        '        "left outer join TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE on TSPL_SD_SHIPMENT_DETAIL.Delivery_Code=TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code " &
        '        "where  TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Customer_Code='" & strCustomer & "' and  " &
        '        "isnull(TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Short_Close,'N')='Y' and  TSPL_SD_SHIPMENT_HEAD.Status=0  "
        '        dblShortCloseDoDispatch = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))

        '    End If
        '    dblBookingAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select isnull( sum(Booking_Qty * Item_Rate) ,0 ) from TSPL_BOOKING_DETAIL where Booking_Status=4 and isnull(DO_Posted,0)=0 and  TSPL_BOOKING_DETAIL.Cust_Code='" & strCustomer & "'  and " &
        '                    "TSPL_BOOKING_DETAIL.Document_No not in ('" & txtDocNo.Value & "') ", trans))
        '    dblAmt = dblCreditLimit + dblSecurityAmount - dblReverseSecurityAmount - dblPendingDeliveryAmt - dblOutstandingAmt - dblShortCloseDoDispatch - dblRefundAmount + dblReverseRefundAmount - dblARSecurityAmt - dblBookingAmt

        '    ''richa VIJ/03/12/19-000091
        '    If isInsideLoadData = False Then
        '        lblCreditLimit.Text = dblCreditLimit
        '        lblAdvanceSecurity.Text = dblSecurityAmount
        '        lblReverseAdvanceSec.Text = dblReverseSecurityAmount
        '        lblPendingDO.Text = dblPendingDeliveryAmt
        '        lblLedgerOutstanding.Text = dblOutstandingAmt
        '        lblShortcloseDO.Text = dblShortCloseDoDispatch
        '        lblRefund.Text = dblRefundAmount
        '        lblReverseRefund.Text = dblReverseRefundAmount
        '        lblARSecurity.Text = dblARSecurityAmt
        '        lblTotalOutstansing.Text = dblAmt
        '    End If

        '    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select  isnull(tspl_customer_master.CheckCreditLimit,0) as CheckCreditLimit from tspl_customer_master where cust_code='" + strCustomer + "'", trans)) = 0 Then
        '        Return True
        '    End If

        '    If dblAmt < clsCommon.myCdbl(lblTotRAmt1.Text) Then
        '        Dim dblNewCredtitLimit = dblAmt - clsCommon.myCdbl(lblTotRAmt1.Text)
        '        'common.clsCommon.MyMessageBoxShow("Please send for approval for increasing credit limit " + clsCommon.myCstr(dblNewCredtitLimit))
        '        Return False

        '    End If
        '    'Return True
        'Catch ex As Exception
        '    common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        '    Return False
        'End Try
        Return True
    End Function

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            Dim dblTotalDocAmt As Decimal = 0
            Dim qry As String = ""
            Dim obj As New clsBookingEntryDairySale
            'Dim intRow As Integer
            obj = clsBookingEntryDairySale.GetData(strCode, NavTyep, clsUserMgtCode.frmDairyBookingCustomer)

            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then

                btnSave.Enabled = True
                btnPost.Enabled = True
                btnDelete.Enabled = True
                isInsideLoadData = True
                btnCopy.Enabled = False
                isNewEntry = False
                btnSave.Text = "Update"
                BlankAllControls()

                LoadBlankGrid()
                chkSampling.Checked = IIf(obj.IsSampling = 1, True, False)
                chkGatePass.Checked = IIf(obj.AgainstGatePass = 1, True, False)
                txtLocation.Enabled = False
                txtVendorNo.Enabled = False
                txtDocNo.Value = obj.Document_No
                txtDate.Value = obj.Document_Date
                If EnableCustomerPODetailonDairyBooking = 1 Then
                    txtSalesman.Value = obj.SalesmanCode
                    If obj.Podate IsNot Nothing Then
                        txtCustPODate.Value = obj.Podate
                        txtCustPODate.Checked = True
                    End If
                    txtPONo.Text = obj.Cust_PO_No
                End If

                lblDONumber.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(Delivery_No,'') from TSPL_BOOKING_DETAIL where Document_No='" + txtDocNo.Value + "'"))
                'DOStatus = clsDBFuncationality.getSingleValue("select isnull(DO_Posted,0) from TSPL_BOOKING_DETAIL where Document_No ='" & txtDocNo.Value & "' and cust_code='" & txtVendorNo.Value & "'")
                If obj.Posted = 1 Then
                    btnSave.Enabled = False
                    btnDelete.Enabled = False
                    btnPost.Enabled = False
                    btnCreateDO.Enabled = True
                    Dim DOStatus1 = clsDBFuncationality.getSingleValue("select top 1  Document_No from TSPL_BOOKING_DETAIL where DO_Posted <> 4 and Document_No='" & txtDocNo.Value & "'")
                    If clsCommon.myLen(DOStatus1) = 0 Then
                        btnCreateDO.Enabled = False
                    End If
                End If

                'If obj.TRANSACTION_TYPE = "FS" Then
                '    rbtn_Fresh.IsChecked = True
                'ElseIf obj.TRANSACTION_TYPE = "PS" Then
                '    rbtn_Ambient.IsChecked = True
                'End If


                If clsCommon.myLen(obj.Ex_Factory_Date) = 0 Then
                    txtEx_Factory_Date.Checked = False
                Else
                    txtEx_Factory_Date.Checked = True
                    txtEx_Factory_Date.Value = obj.Ex_Factory_Date
                End If
                lblCreatedDateAndTime.Text = obj.Created_Date
                cmbBookingType.Text = IIf(obj.Booking_Type = "", "Select", obj.Booking_Type)
                txtLocation.Value = obj.location_code
                lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
                txtCan.Text = obj.TotalCAN
                txtCrate.Text = obj.TotalCrate
                txtBox.Text = obj.TotalBox
                txtShipToLocation.Value = obj.Ship_To_Location
                lblShipToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_SHIP_TO_LOCATION.Ship_To_Desc FROM  TSPL_SHIP_TO_LOCATION WHERE Ship_To_Code  ='" + txtShipToLocation.Value + "'"))
                If obj.Uploading_date IsNot Nothing Then
                    lblUploadingDate.Text = obj.Uploading_date
                End If

                'Sanjay ERO/12/07/18-000371
                Is_Cancelled = obj.Is_Cancelled
                lblCancelStatus.Text = IIf(obj.Is_Cancelled = 1, "Cancel", "")
                If obj.Is_Cancelled = 1 Then
                    btnCancel.Enabled = False
                Else
                    btnCancel.Enabled = True
                End If
                'Sanjay ERO/12/07/18-000371

                GSTStatus = clsERPFuncationality.GetGSTStatus(txtDate.Value)

                ''''''''''''''''''''''''''''
                lblCreditLimit.Text = clsCommon.myCdbl(obj.Credit_Limit)
                lblAdvanceSecurity.Text = clsCommon.myCdbl(obj.Advance_Security)
                lblReverseAdvanceSec.Text = clsCommon.myCdbl(obj.Revese_Adv_Security)
                lblARSecurity.Text = clsCommon.myCdbl(obj.AR_Credit_Security)
                lblPendingDO.Text = clsCommon.myCdbl(obj.Pending_Posted_DO)
                lblShortcloseDO.Text = clsCommon.myCdbl(obj.UnPostedDispatch)
                lblLedgerOutstanding.Text = clsCommon.myCdbl(obj.Ledger_Outstansing)
                lblRefund.Text = clsCommon.myCdbl(obj.Refund_Security)
                lblReverseRefund.Text = clsCommon.myCdbl(obj.Reverse_Refund_Sec)
                lblTotalOutstansing.Text = clsCommon.myCdbl(obj.Total_Outstanding)
                '''''''''''''''''''''''''''

                qry = "SELECT TSPL_BOOKING_DETAIL.*,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_ROUTE_MASTER.Route_Desc FROM TSPL_BOOKING_DETAIL LEFT JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_BOOKING_DETAIL.Cust_Code left outer join " &
                        " TSPL_ROUTE_MASTER on TSPL_BOOKING_DETAIL.Route_No=TSPL_ROUTE_MASTER.Route_No WHERE Document_No='" + txtDocNo.Value + "' and scheme_item='N '"
                Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry)
                txtVendorNo.Value = clsCommon.myCstr(dt2.Rows(0)("Cust_Code"))
                lblVendorName.Text = clsCommon.myCstr(dt2.Rows(0)("Customer_Name"))
                '====================Added by preeti Gupta Against Ticket no[BHA/01/08/18-000206]=
                lblBoothStation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  isnull(Zone_Code,'') +  case when len(oldname )>0 then ', ' +isnull(oldname,'') else '' end from TSPL_CUSTOMER_MASTER where Cust_Code='" + txtVendorNo.Value + "'"))
                lblroutecode.Text = clsCommon.myCstr(dt2.Rows(0)("Route_No"))
                lblroutename.Text = clsCommon.myCstr(dt2.Rows(0)("Route_Desc"))
                strRoutecode = clsCommon.myCstr(dt2.Rows(0)("Route_No"))
                strRouteDesc = clsCommon.myCstr(dt2.Rows(0)("Route_Desc"))
                '===============================================================
                lblTotRAmt1.Text = clsCommon.myCstr(dt2.Rows(0)("DocumentAmount"))
                BookingStatus = clsCommon.myCstr(dt2.Rows(0)("Booking_Status"))
                DOStatus = clsCommon.myCstr(dt2.Rows(0)("DO_Posted"))
                ''richa agarwal ERO/21/05/19-000609 21 May,2019 add updated vehicle No according to DO
                LblUpdatedVehicleCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Lorry_No from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE where Booking_No ='" + txtDocNo.Value + "'"))
                LblUpdatedVehicleDesc.Text = clsCommon.myCstr(ClsVehicleMaster.GetName(LblUpdatedVehicleCode.Text, Nothing))
                setRouteDetail(txtVendorNo.Value, lblroutecode.Text)
                ''richa TEC/01/10/19-001025
                txtRouteNo.Value = clsCommon.myCstr(dt2.Rows(0)("Route_No"))
                lblRouteDesc.Text = clsCommon.myCstr(dt2.Rows(0)("Route_Desc"))
                ' done by priti BHA/14/06/18-000052
                If clsCommon.myLen(BookingStatus) > 0 Then
                    If BookingStatus = 1 Then
                        txtBOstatus.Text = "Open"
                    ElseIf BookingStatus = 2 Then
                        txtBOstatus.Text = "Pending"
                    ElseIf BookingStatus = 3 Then
                        txtBOstatus.Text = "Approved"
                    ElseIf BookingStatus = 4 Then
                        txtBOstatus.Text = "Posted"
                    ElseIf BookingStatus = 5 Then
                        txtBOstatus.Text = "Rejected"
                    End If
                End If
                If clsCommon.myLen(BookingStatus) > 0 Then
                    If DOStatus = 1 Then
                        txtDOStatus.Text = "Open"
                    ElseIf DOStatus = 2 Then
                        txtDOStatus.Text = "Pending"
                    ElseIf DOStatus = 3 Then
                        txtDOStatus.Text = "Approved"
                    ElseIf DOStatus = 4 Then
                        txtDOStatus.Text = "Posted"
                    ElseIf DOStatus = 5 Then
                        txtDOStatus.Text = "Rejected"
                    End If
                End If
                For jj As Integer = 0 To dt2.Rows.Count() - 1
                    gv1.Rows.AddNew()

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count 'clsCommon.myCdbl(dt2.Rows(jj)("Line_No"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = clsCommon.myCstr(dt2.Rows(jj)("Item_Code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select distinct item_desc from tspl_item_master where item_code='" + clsCommon.myCstr(dt2.Rows(jj)("Item_Code")) + "'"))
                    'SKG
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIShortName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select distinct Short_Description from tspl_item_master where item_code='" + clsCommon.myCstr(dt2.Rows(jj)("Item_Code")) + "'"))
                    'SKG
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIHSN).Value = clsItemMaster.GetItemHSNCode(clsCommon.myCstr(dt2.Rows(jj)("Item_Code")), Nothing)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(ColAvailableQty).Value = clsItemLocationDetails.getBalance(clsCommon.myCstr(dt2.Rows(jj)("Item_Code")), txtLocation.Value, txtDocNo.Value, txtDate.Value, Nothing, clsCommon.myCstr(dt2.Rows(jj)("Unit_Code")), 0)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPreviousQty).Value = clsCommon.myCdbl(dt2.Rows(jj)("PreviousBookingQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = clsCommon.myCdbl(dt2.Rows(jj)("Booking_Qty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgRate).Value = clsCommon.myCdbl(dt2.Rows(jj)("OrgRate"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = clsCommon.myCstr(dt2.Rows(jj)("Unit_Code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = clsCommon.myCdbl(dt2.Rows(jj)("Item_Rate"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSellingRate).Value = clsCommon.myCdbl(dt2.Rows(jj)("Item_Selling_Price"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = Math.Round(clsCommon.myCdbl(dt2.Rows(jj)("Booking_Qty")) * clsCommon.myCdbl(dt2.Rows(jj)("Item_Rate")), 2)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTBaseAmt).Value = clsCommon.myCdbl(dt2.Rows(jj)("Tax_On_Amount"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTTaxAmt).Value = clsCommon.myCdbl(dt2.Rows(jj)("Tax_Amount"))
                    UpdateCurrentRowAvgQty(gv1.CurrentRow.Index)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDisc_Scheme_Amount).Value = clsCommon.myCdbl(dt2.Rows(jj)("Disc_Scheme_Amount"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDisc_Scheme_Code).Value = clsCommon.myCstr(dt2.Rows(jj)("Disc_Scheme_Code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDisc_Scheme_Pers).Value = clsCommon.myCdbl(dt2.Rows(jj)("Disc_Scheme_Pers"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDisc_Scheme_Type).Value = clsCommon.myCstr(dt2.Rows(jj)("Disc_Scheme_Type"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeType).Value = clsCommon.myCstr(dt2.Rows(jj)("Scheme_Type"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTax_NonTax).Value = clsCommon.myCdbl(dt2.Rows(jj)("Tax_NonTax"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colFreshAmbient).Value = clsCommon.myCstr(dt2.Rows(jj)("FreshAmbient"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = clsCommon.myCstr(dt2.Rows(jj)("Remarks"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemBasicPrice).Value = clsCommon.myCdbl(dt2.Rows(jj)("Price_with_Tax"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAmountWithTax).Value = clsCommon.myCdbl(dt2.Rows(jj)("Amount_with_Tax"))
                    dblTotalDocAmt = dblTotalDocAmt + clsCommon.myCdbl(gv1.Rows(gv1.Rows.Count - 1).Cells(colAmountWithTax).Value)
                    ''RICHA 06 JUNE,2020
                    If chkSampling.Checked = False And DonotAllowtoChangeUOMinDairyBookingCustomer = True Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).ReadOnly = True
                    Else
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).ReadOnly = False
                    End If
                Next
                lblTotalDocAmt.Text = Math.Round(clsCommon.myCdbl(dblTotalDocAmt), 2)
                ''to show all items other than booking in case of customer type other than others 25 Feb,2020
                If clsCommon.CompairString(txtBOstatus.Text, "Posted") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(txtBOstatus.Text, "Rejected") <> CompairStringResult.Equal Then
                    If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Customer_category,'') from tspl_customer_master where cust_code='" & clsCommon.myCstr(txtVendorNo.Value) & "' ")), "Others") <> CompairStringResult.Equal Then
                        qry = "  select tspl_item_master.item_code,tspl_item_master.Item_Desc,tspl_item_master.Short_Description ,TSPL_ITEM_UOM_DETAIL.UOM_Code ,tspl_item_master.IsTaxable," & Environment.NewLine &
                        " case when tspl_item_master.Is_Ambient=1 then 'PS' WHEN tspl_item_master.Is_FreshItem=1 THEN 'FS' ELSE '' END  IsFreshAmbient,tspl_item_master.HSN_Code,0 as Booking_Qty,tspl_item_master.Sku_Seq   from tspl_item_master " & Environment.NewLine &
                        " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code =tspl_item_master.Item_Code " & Environment.NewLine &
                        " where isnull(tspl_item_master.Chilled_Freezen,0)=1 and isnull(TSPL_ITEM_MASTER.item_type,'')='F'  and tspl_item_master.Active=1 and TSPL_ITEM_UOM_DETAIL.Default_UOM=1 " & Environment.NewLine &
                        " and tspl_item_master.Item_Code not in ( select tspl_booking_detail.item_code from tspl_booking_detail" & Environment.NewLine &
                        " LEFT OUTER JOIN tspl_item_master ON tspl_item_master.item_code=tspl_booking_detail.iTEM_CODE " & Environment.NewLine &
                        " where tspl_booking_detail.Document_No='" & clsCommon.myCstr(txtDocNo.Value) & "' AND tspl_booking_detail.Scheme_Item ='N') order by Sku_Seq" & Environment.NewLine
                        dt2 = clsDBFuncationality.GetDataTable(qry)
                        If dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0 Then
                            For Each dr As DataRow In dt2.Rows
                                isCellValueChangedOpen = True
                                gv1.Rows.AddNew()
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = clsCommon.myCstr(dr("item_code"))
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = clsCommon.myCstr(dr("Item_Desc"))
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colIShortName).Value = clsCommon.myCstr(dr("Short_Description"))
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colIHSN).Value = clsCommon.myCstr(dr("HSN_Code"))
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = clsCommon.myCstr(dr("UOM_Code"))
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colTax_NonTax).Value = clsCommon.myCstr(dr("IsTaxable"))
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colFreshAmbient).Value = clsCommon.myCstr(dr("IsFreshAmbient"))
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colPreviousQty).Value = clsCommon.myCdbl(dr("Booking_Qty"))
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = clsCommon.myCdbl(dr("Booking_Qty"))
                                If clsCommon.myCdbl(clsCommon.myCdbl(dr("Booking_Qty"))) > 0 Then
                                    ItemPrice(clsCommon.myCstr(dr("item_code")), clsCommon.myCstr(dr("Unit_code")), clsCommon.myCdbl(dr("Booking_Qty")), gv1.Rows.Count - 1)
                                End If
                                isCellValueChangedOpen = False
                            Next
                        End If
                    Else
                        qry = " select tspl_item_master.item_code,tspl_item_master.Item_Desc,tspl_item_master.Short_Description ,TSPL_ITEM_UOM_DETAIL.UOM_Code ,tspl_item_master.IsTaxable," & Environment.NewLine &
                      " case when tspl_item_master.Is_Ambient=1 then 'PS' WHEN tspl_item_master.Is_FreshItem=1 THEN 'FS' ELSE '' END  IsFreshAmbient,tspl_item_master.HSN_Code,0 as Booking_Qty,tspl_item_master.Marketing_Seq   from tspl_item_master " & Environment.NewLine &
                      " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code =tspl_item_master.Item_Code " & Environment.NewLine &
                      " where isnull(tspl_item_master.Item_Type,'')='F' and isnull(tspl_item_master.TypeOfItm,'')='A' and tspl_item_master.Active=1 and TSPL_ITEM_UOM_DETAIL.Default_UOM=1 " & Environment.NewLine &
                      " and tspl_item_master.Item_Code not in ( select tspl_booking_detail.item_code from tspl_booking_detail" & Environment.NewLine &
                      " LEFT OUTER JOIN tspl_item_master ON tspl_item_master.item_code=tspl_booking_detail.iTEM_CODE " & Environment.NewLine &
                      " where tspl_booking_detail.Document_No='" & clsCommon.myCstr(txtDocNo.Value) & "' AND tspl_booking_detail.Scheme_Item ='N')" & Environment.NewLine &
                      " order by Marketing_Seq"

                        dt2 = clsDBFuncationality.GetDataTable(qry)
                        If dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0 Then
                            For Each dr As DataRow In dt2.Rows
                                isCellValueChangedOpen = True
                                gv1.Rows.AddNew()
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = clsCommon.myCstr(dr("item_code"))
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = clsCommon.myCstr(dr("Item_Desc"))
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colIShortName).Value = clsCommon.myCstr(dr("Short_Description"))
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colIHSN).Value = clsCommon.myCstr(dr("HSN_Code"))
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = clsCommon.myCstr(dr("UOM_Code"))
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colTax_NonTax).Value = clsCommon.myCstr(dr("IsTaxable"))
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colFreshAmbient).Value = clsCommon.myCstr(dr("IsFreshAmbient"))
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colPreviousQty).Value = clsCommon.myCdbl(dr("Booking_Qty"))
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = clsCommon.myCdbl(dr("Booking_Qty"))
                                If clsCommon.myCdbl(clsCommon.myCdbl(dr("Booking_Qty"))) > 0 Then
                                    ItemPrice(clsCommon.myCstr(dr("item_code")), clsCommon.myCstr(dr("Unit_code")), clsCommon.myCdbl(dr("Booking_Qty")), gv1.Rows.Count - 1)
                                End If
                                isCellValueChangedOpen = False
                            Next
                        End If
                    End If
                End If
                ItemTypePanel.Enabled = False

            End If

            '' CustomerOutstandingAmount(txtVendorNo.Value, Nothing)
            gv1.Rows.AddNew()
            If gv1.Rows.Count > 0 Then
                gv1.Focus()
                gv1.CurrentRow = gv1.Rows(0)
                gv1.CurrentColumn = gv1.Columns(colQty)
            End If
            'End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub

    Sub CloseForm()
        Me.Close()
    End Sub


    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click

        'Sanjay Ticket No- ERO/12/07/18-000371
        If clsCommon.myLen(txtDocNo.Value) > 0 Then
            If Is_Cancelled = 1 Then
                clsCommon.MyMessageBoxShow(Me, "Booking Cancelled,Can not Post.", Me.Text)
                Exit Sub
            End If
            'Sanjay Ticket No- BHA/03/10/18-000589
            If BookingStatus = 2 Then
                clsCommon.MyMessageBoxShow(Me, "Booking is pending for approval,Can not Post.", Me.Text)
                Exit Sub
            End If
            'Sanjay Ticket No- BHA/03/10/18-000589
        End If
        'Sanjay Ticket No- ERO/12/07/18-000371

        ''richa 14 Dec,2018 ERO/11/12/18-000434
        Dim strOrderBookingPosted As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select case when ISNULL(Route_Time,'')<>''  then CASE WHEN CAST(GETDATE() AS TIME)<=CAST(Route_Time  AS TIME) THEN 'Y' ELSE 'N' END else 'Y' end  from tspl_route_MASTER where Route_No ='" & clsCommon.myCstr(lblroutecode.Text) & "'"))
        If clsCommon.CompairString(strOrderBookingPosted, "N") = CompairStringResult.Equal Then
            clsCommon.MyMessageBoxShow(Me, "Booking time has exceeded for Route " & lblroutecode.Text & ", it cannot be posted.", Me.Text)
            Exit Sub
        End If
        ''--------------------------------------
        PostData()

    End Sub

    Sub PostData()
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (myMessages.postConfirm()) Then

                If clsCommon.myLen(txtDocNo.Value) > 0 Then
                    Dim qry = "Update TSPL_BOOKING_MATSER set Posted=1, " &
                     "Modified_By='" + objCommonVar.CurrentUserCode + "', " &
                     "Modified_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "' " &
                     "where Document_No='" + txtDocNo.Value + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    'For ii As Integer = 8 To gv1.Columns.Count - 1
                    'Dim objBooking As clsBookingTemp = TryCast(gv1.Columns(ii).Tag, clsBookingTemp)
                    Dim dblTotalAmt As Double = clsCommon.myCdbl(lblTotRAmt1.Text)
                    Dim strBookingStatus As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select  Booking_Status from TSPL_BOOKING_DETAIL where Document_No='" & txtDocNo.Value & "'  and Cust_Code='" & txtVendorNo.Value & "'", trans))
                    If (dblTotalAmt > 0 OrElse AllowZeroQtyOnDairyBooking = True) AndAlso (strBookingStatus = 1 OrElse strBookingStatus = 3) Then
                        qry = "Update TSPL_BOOKING_DETAIL set Booking_Status=4 where Cust_Code='" & txtVendorNo.Value & "' and Document_No='" + txtDocNo.Value + "'  and Booking_Status<>5"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    End If
                    'Next
                    '== Notification regarding
                    Dim strNotificationOn As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_On from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmDairyBookingCustomer + "'", trans))
                    If clsCommon.CompairString(strNotificationOn, "P") = CompairStringResult.Equal Then
                        If txtEx_Factory_Date.Checked = True Then
                            Dim Booking_Id As String = txtDocNo.Value
                            Dim Booking_Date As DateTime = clsCommon.myCDate(txtDate.Value)
                            Dim Ex_Factory_Date As DateTime = Nothing
                            If clsCommon.myLen(txtEx_Factory_Date.Value) > 0 Then
                                Ex_Factory_Date = clsCommon.myCDate(txtEx_Factory_Date.Value)
                            End If
                            CreateNotificationContentEMP(Booking_Id, Booking_Date, Ex_Factory_Date, trans)
                        End If
                    End If
                    '== Complete
                    trans.Commit()
                    Dim msg = "Successfully Posted"
                    common.clsCommon.MyMessageBoxShow(Me, msg, Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                    btnCreateDO.Enabled = True
                Else
                    Throw New Exception("No Data found to Post")
                End If
            End If
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Private Shared Function CreateNotificationContentEMP(ByVal Booking_Id As String, ByVal Booking_Date As DateTime, ByVal Ex_Factory_Date As DateTime, ByVal trans As SqlTransaction) As Boolean
        Dim strNotifiContent As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_Text from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmDairyBookingCustomer + "'", trans))
        Dim strNotifi_DetalContent As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_Detail_Text from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmDairyBookingCustomer + "'", trans))
        Dim strNotifiCaption As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_Caption from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmDairyBookingCustomer + "'", trans))
        Dim strNotificationOn As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_On from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmDairyBookingCustomer + "'", trans))

        If clsCommon.myLen(strNotifiContent) > 0 Then
            Dim objNotification As New clsNotificationHead()
            objNotification.Notification_Text = strNotifiContent
            objNotification.Notification_Caption = strNotifiCaption
            objNotification.Notification_On = strNotificationOn
            objNotification.Notification_Detail_Text = strNotifi_DetalContent
            objNotification.Notification_ConditionDate = clsCommon.myCDate(Ex_Factory_Date)
            objNotification.Notification_Text = objNotification.Notification_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Doc_No, clsCommon.myCstr(Booking_Id))
            objNotification.Notification_Text = objNotification.Notification_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Doc_Date, clsCommon.myCstr(Booking_Date))
            objNotification.Notification_Text = objNotification.Notification_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Ex_Factory_Date, clsCommon.myCstr(clsCommon.myCDate(Ex_Factory_Date)))
            objNotification.SaveData(clsUserMgtCode.frmDairyBookingCustomer, objNotification, trans)
            objNotification = Nothing
            Return True
        End If
        Return False
    End Function
    'Sub Export()
    '    If gv1.Rows.Count > 0 Then
    '        ExportToExcel()
    '    Else
    '        Common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
    '    End If
    'End Sub
    'Private Sub ExportToExcel()
    '    Try
    '        Dim strCreatedBy As String = clsDBFuncationality.getSingleValue("Select Created_By from TSPL_BOOKING_MATSER where Document_No='" + txtDocNo.Value + "'")
    '        Dim arrHeader As List(Of String) = New List(Of String)()
    '        Dim strtemp As String = "Booking No : " + txtDocNo.Value
    '        arrHeader.Add(strtemp)
    '        strtemp = "Booking Date : " + clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy")
    '        arrHeader.Add(strtemp)
    '        strtemp = "Created By : " + strCreatedBy
    '        arrHeader.Add(strtemp)
    '        strtemp = "Transaction Type : " + IIf(rbtn_Fresh.IsChecked = True, "Fresh", "Ambient")
    '        arrHeader.Add(strtemp)

    '        clsCommon.MyExportToExcelGrid("Booking Entry", gv1, arrHeader, Me.Text)

    '    Catch ex As Exception
    '        Common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
    '    End Try
    'End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If clsCancelLog.CheckForReasonOnDelete() Then
                    '' REASON FOR DELETE 
                    Dim frm As New FrmFreeTxtBox1
                    frm.Text = "Remarks for Delete"
                    frm.ShowDialog()
                    If clsCommon.myLen(frm.strRmks) <= 0 Then
                        Exit Sub
                    Else
                        Reason = frm.strRmks
                    End If
                End If
                If (clsBookingEntryDairySale.DeleteData(txtDocNo.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtDocNo.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function

    Private Sub gv1_CellEditorInitialized(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellEditorInitialized
        If TypeOf Me.gv1.CurrentColumn Is GridViewMultiComboBoxColumn Then
            Dim editor As RadMultiColumnComboBoxElement = DirectCast(Me.gv1.ActiveEditor, RadMultiColumnComboBoxElement)
            editor.AutoSizeDropDownToBestFit = True
            editor.EditorControl.MasterTemplate.BestFitColumns()
            editor.DropDownStyle = RadDropDownStyle.DropDown
            editor.AutoFilter = True
            If editor.EditorControl.MasterTemplate.FilterDescriptors.Count = 0 Then
                Dim autoFilter As FilterDescriptor = New FilterDescriptor("Description", FilterOperator.StartsWith, "")
                autoFilter.IsFilterEditor = True
                editor.EditorControl.FilterDescriptors.Add(autoFilter)
            End If
        End If
    End Sub

    Private Function funSetUserAccess() As Boolean
        Try
            btnSave.Visible = True
            btnDelete.Visible = True
            btnPost.Visible = True

            Dim strRights As String
            Dim strTemp() As String
            Dim strProgCode = "PO-ODR"
            strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete & "," & enuUserRights.enuAuthorised
            strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
            strTemp = Split(strRights, ",")
            If strTemp(0) = "0" Then
                MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
                funSetUserAccess = False
                blnRead = False
                Me.Close()
                Exit Function
            Else
                blnRead = True
            End If
            If strTemp(1) = "0" Then 'Grant modify access
                btnSave.Visible = False
            End If
            If strTemp(2) = "0" Then 'Grant modify access
                btnDelete.Visible = False
            End If
            If strTemp(3) = "0" Then 'Grant Authorize access
                btnPost.Visible = False
            End If
            funSetUserAccess = True
        Catch er As Exception

        End Try
        Return funSetUserAccess
    End Function

    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Try

            Dim qry As String = "select count(*) from TSPL_BOOKING_MATSER where Document_No='" + txtDocNo.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "' and TSPL_BOOKING_MATSER.From_Screen_code='" & clsUserMgtCode.frmDairyBookingCustomer & "'"
            Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If check > 0 Then
                txtDocNo.MyReadOnly = True
            ElseIf check <= 0 Then
                txtDocNo.MyReadOnly = False
            End If

            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub



    ' Ticket : TEC/05/09/19-001000 By Prabhakar
    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDocNo._MYValidating
        Dim qry As String = "select distinct TSPL_BOOKING_MATSER.Document_No as DocumentNo,convert(varchar(12),TSPL_BOOKING_MATSER.Document_date,103) as Document_date,TSPL_CUSTOMER_MASTER.Cust_Code as Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_BOOKING_MATSER.location_code as Location  ,case when isnull(TSPL_BOOKING_MATSER.Is_Cancelled,0)=1 then 'Cancel' when TSPL_BOOKING_MATSER.Posted=1 then 'posted' else 'Unposted' end as Posted ,case when isnull(TBL_DELIVERY_NO.Delivery_No,'')='' then NULL else TBL_DELIVERY_NO.Delivery_No end as [Delivery No],TSPL_CUSTOMER_MASTER.Cust_Category_Code as [Customer Category Code],TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY as [Booking Type] from TSPL_BOOKING_MATSER" &
         " left join TSPL_BOOKING_DETAIL on TSPL_BOOKING_DETAIL.Document_No=TSPL_BOOKING_MATSER.Document_No " &
         " left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_BOOKING_DETAIL.Cust_Code " &
         " left join (select distinct Document_No,isnull (Delivery_No,'') as Delivery_No from TSPL_BOOKING_DETAIL  ) as TBL_DELIVERY_NO on TBL_DELIVERY_NO.Document_No = TSPL_BOOKING_MATSER.Document_No "
        Dim whrClas As String = " TSPL_BOOKING_MATSER.comp_code='" + objCommonVar.CurrentCompanyCode + "' and From_Screen_code='" & clsUserMgtCode.frmDairyBookingCustomer & "'"
        '-------richa 17/12/2019 show customer according to customer permission Ticket No. ---------
        Dim strwherecls As String = ""
        strwherecls = Xtra.CustomerPermission()
        If clsCommon.myLen(strwherecls) > 0 Then
            whrClas += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" + strwherecls + ")"
        End If
        Dim strDocNo As String = clsCommon.ShowSelectForm("PSBookingOrderNoFndd", qry, "DocumentNo", whrClas, txtDocNo.Value, "DocumentNo", isButtonClicked, "Document_date")
        If clsCommon.myLen(strDocNo) > 0 Then
            LoadData(strDocNo, NavigatorType.Current)
        End If
    End Sub

    Private Sub FrmAPInvoiceEntry_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F2 AndAlso gv1.CurrentCell IsNot Nothing AndAlso gv1.CurrentColumn Is gv1.Columns(colUnit) Then
            isCellValueChangedOpen = True
            'If chkItemwise.Checked Then
            gv1.CurrentColumn = gv1.Columns(colIName)
            OpenItemUOMList(True)
            gv1.CurrentColumn = gv1.Columns(colUnit)
            'Else
            '    gv1.CurrentColumn = gv1.Columns(colIName)
            '    OpenItemGrpUOMList(True)
            '    gv1.CurrentColumn = gv1.Columns(colUnit)
            'End If
            setGridFocus()
            isCellValueChangedOpen = False
        End If

        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            SavingData(False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled AndAlso MyBase.isPostFlag Then
            'Sanjay Ticket No- ERO/12/07/18-000371
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                If Is_Cancelled = 1 Then
                    clsCommon.MyMessageBoxShow(Me, "Booking Cancelled,Can not Post.", Me.Text)
                    Exit Sub
                End If
                'Sanjay Ticket No- BHA/03/10/18-000589
                If BookingStatus = 2 Then
                    clsCommon.MyMessageBoxShow(Me, "Booking is pending for approval,Can not Post.", Me.Text)
                    Exit Sub
                End If
                'Sanjay Ticket No- BHA/03/10/18-000589
            End If
            'Sanjay Ticket No- ERO/12/07/18-000371

            PostData()
            'skg
        ElseIf e.Alt AndAlso e.KeyCode = Keys.F AndAlso btnCreateDO.Enabled Then
            btnCreateDO.PerformClick()
            'skg
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled AndAlso MyBase.isDeleteFlag Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        ElseIf e.Control AndAlso e.KeyCode = Keys.F7 Then
            'SelectRequistionItems()
        ElseIf e.Alt AndAlso e.Control AndAlso e.Shift AndAlso e.KeyCode = Keys.T Then
            'chkRateDefaultSetting.Visible = Not chkRateDefaultSetting.Visible
            'chkRateUserCustomer.Visible = Not chkRateUserCustomer.Visible
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            If MyBase.isReverse Then

                Dim frm As New FrmPWD(Nothing)
                frm.strType = "SIRC"
                frm.strCode = "SIReversAndCreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnreverse.Visible = True
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
                'MessageBox.Show("You are not authorized to perform this action.", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        'Add Tool tip Task No- TEC/18/05/18-000237
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine +
                              "TSPL_BOOKING_MATSER " + Environment.NewLine +
                              "TSPL_BOOKING_DETAIL " + Environment.NewLine +
                              "TSPL_GATEPASS_MASTER_DAIRYSALE (For Gate Pass Document) " + Environment.NewLine +
                              "TSPL_GATEPASS_DETAIL_DAIRYSALE (For Gate Pass Document) " + Environment.NewLine +
                              "Press Alt+F for Create DO/Post DO Trasnaction" + Environment.NewLine +
                              "TSPL_DELIVERY_NOTE_MASTER_FRESHSALE " + Environment.NewLine +
                              "TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE " + Environment.NewLine +
                              "TSPL_TRANSACTION_APPROVAL (For Approving Pending Document) ")
            'Add Tool tip Task No- TEC/18/05/18-000237
        End If
    End Sub

    Private Sub BlankControlOnCustomer()

        lblVendorName.Text = ""

        LoadBlankGrid()
        'LoadBlankGridTax()
        gv1.Rows.AddNew()

    End Sub
    Private Sub txtVendorNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtVendorNo._MYValidating
        Dim qry As String = ""
        BlankControlOnCustomer()
        If SettTagMultipleRouteWithCustomer Then
            If clsCommon.myLen(txtRouteNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please first select route", Me.Text)
                txtRouteNo.Focus()
                Exit Sub
            End If
            qry = "select TSPL_CUSTOMER_MASTER.Cust_Code as Code,TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],TSPL_CUSTOMER_MASTER.Alies_Name as [Short Name],TSPL_CUSTOMER_MASTER.Route_No"
            qry += " ,TSPL_CUSTOMER_MASTER.Zone_Code as [Zone Code],TSPL_ZONE_MASTER.Description as [Zone Name]"
            qry += " from TSPL_CUSTOMER_MASTER "
            qry += " inner join TSPL_Customer_Route_Master on TSPL_Customer_Route_Master.cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code"
            qry += " left join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code=TSPL_CUSTOMER_MASTER.Zone_Code"
            Dim WhrCls As String = ""
            If clsCommon.myLen(txtRouteNo.Value) > 0 Then
                WhrCls = "  TSPL_CUSTOMER_MASTER.Status='N' and TSPL_Customer_Route_Master.Route_No='" & txtRouteNo.Value & "' "
            Else
                WhrCls = "  TSPL_CUSTOMER_MASTER.Status='N' "
            End If


            txtVendorNo.Value = clsCommon.ShowSelectForm("CustomerFnder1", qry, "Code", WhrCls, txtVendorNo.Value, "Code", isButtonClicked)
            lblVendorName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code='" + txtVendorNo.Value + "'"))
            setRouteDetail(txtVendorNo.Value, txtRouteNo.Value)
            gv1.DataSource = Nothing
        Else
            qry = "select Cust_Code as Code,TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],TSPL_CUSTOMER_MASTER.Alies_Name as [Short Name],TSPL_CUSTOMER_MASTER.Route_No"
            qry += " ,TSPL_CUSTOMER_MASTER.Zone_Code as [Zone Code],TSPL_ZONE_MASTER.Description as [Zone Name],isnull(TSPL_CUSTOMER_MASTER.Customer_Category,'') as [Customer Category],tspl_customer_master.Cust_Group_Code as [Customer Group Code] "
            qry += " from TSPL_CUSTOMER_MASTER "
            qry += " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_CUSTOMER_MASTER.City_Code"
            qry += " left join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code=TSPL_CUSTOMER_MASTER.Zone_Code"
            Dim WhrCls As String = ""
            If clsCommon.myLen(txtRouteNo.Value) > 0 Then
                WhrCls = "  TSPL_CUSTOMER_MASTER.Status='N' and TSPL_CUSTOMER_MASTER.Route_No='" & txtRouteNo.Value & "' "
            Else
                WhrCls = "  TSPL_CUSTOMER_MASTER.Status='N' "
            End If
            '-------richa 17/12/2019 show customer according to custoer permission Ticket No. ---------
            Dim strwherecls As String = ""
            strwherecls = Xtra.CustomerPermission()
            If clsCommon.myLen(strwherecls) > 0 Then
                WhrCls += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" + strwherecls + ")"
            End If

            txtVendorNo.Value = clsCommon.ShowSelectForm("CustomerFnder1", qry, "Code", WhrCls, txtVendorNo.Value, "Code", isButtonClicked)
            lblVendorName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code='" + txtVendorNo.Value + "'"))
            lblBoothStation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  isnull(Zone_Code,'') +  case when len(oldname )>0 then ', ' +isnull(oldname,'') else '' end from TSPL_CUSTOMER_MASTER where Cust_Code='" + txtVendorNo.Value + "'"))
            setRouteDetail(txtVendorNo.Value, txtRouteNo.Value)
            gv1.DataSource = Nothing
            If clsCommon.myLen(txtVendorNo.Value) > 0 Then
                ''richa VIJ/18/12/19-000123
                If ShowBookingTypeDropDownonDairyBookingCustomer = True Then
                    LoadBlankGrid()
                    LoadBookingType()
                    LoadItemsFromPreviousBooking()
                End If

                'Vijaya, Load booking if already exist
                If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                    Dim STRSQL As String = "select TSPL_BOOKING_DETAIL.Document_No from TSPL_BOOKING_DETAIL left join TSPL_BOOKING_MATSER ON TSPL_BOOKING_MATSER.Document_No=TSPL_BOOKING_DETAIL.Document_No left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.cust_code=TSPL_BOOKING_DETAIL.cust_code where TSPL_BOOKING_MATSER.From_Screen_Code ='BOOK-DS-CU' and TSPL_BOOKING_MATSER.location_code ='" & txtLocation.Value & "' and TSPL_BOOKING_DETAIL.Cust_Code='" & txtVendorNo.Value & "' AND convert(date,TSPL_BOOKING_MATSER.Document_Date ,103)=convert(date,'" & txtDate.Value & "',103) and TSPL_CUSTOMER_MASTER.customer_category<>'Others'"
                    Dim TempBookingExist As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(STRSQL))
                    If clsCommon.myLen(TempBookingExist) > 0 Then
                        LoadData(TempBookingExist, NavigatorType.Current)
                    End If
                End If
                'Load booking if already exist
                'RadPageView1.SelectedPage = RadPageViewPage2
            Else
                txtVendorNo.Focus()
            End If
        End If
    End Sub

    Sub setRouteDetail(ByVal strVendorCode As String, ByVal strtRouteCode As String)
        Dim qry As String = ""
        If SettTagMultipleRouteWithCustomer Then
            qry = "select TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_ROUTE_MASTER.vehicle_code,TSPL_VEHICLE_MASTER.Vehicle_No,TSPL_CUSTOMER_MASTER.Zone_Code,TSPL_Customer_Route_Master.Route_No,TSPL_VEHICLE_MASTER.Number,TSPL_ROUTE_MASTER.Route_Desc,tspl_customer_master.price_CodeNon " + Environment.NewLine +
            "from TSPL_CUSTOMER_MASTER" + Environment.NewLine +
            "inner join TSPL_Customer_Route_Master on TSPL_Customer_Route_Master.cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code" + Environment.NewLine +
            "left outer join TSPL_ROUTE_MASTER on TSPL_Customer_Route_Master.Route_No=TSPL_ROUTE_MASTER.Route_No " + Environment.NewLine +
            "left outer join TSPL_VEHICLE_MASTER on TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id " + Environment.NewLine +
            "where TSPL_CUSTOMER_MASTER.Cust_Code='" + strVendorCode + "' and TSPL_Customer_Route_Master.Route_No='" + strtRouteCode + "' "
        Else
            qry = "select Customer_Name,vehicle_code,TSPL_VEHICLE_MASTER.Vehicle_No,Zone_Code,TSPL_CUSTOMER_MASTER.Route_No,Number,TSPL_ROUTE_MASTER.Route_Desc,tspl_customer_master.price_CodeNon from TSPL_CUSTOMER_MASTER left outer join " &
            "TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No left outer join TSPL_VEHICLE_MASTER on " &
            "TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id where Cust_Code='" & strVendorCode & "'"
        End If

        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
            strVehicleCode = clsCommon.myCstr(dt1.Rows(0)("vehicle_code"))
            lblvehiclecode.Text = clsCommon.myCstr(dt1.Rows(0)("vehicle_code"))
            strVehicleDesc = clsCommon.myCstr(dt1.Rows(0)("Number"))
            lblvehicleName.Text = clsCommon.myCstr(dt1.Rows(0)("Number"))
            strRoutecode = clsCommon.myCstr(dt1.Rows(0)("Route_No"))
            lblroutecode.Text = clsCommon.myCstr(dt1.Rows(0)("Route_No"))
            strRouteDesc = clsCommon.myCstr(dt1.Rows(0)("Route_Desc"))
            lblroutename.Text = clsCommon.myCstr(dt1.Rows(0)("Route_Desc"))
            txtRouteNo.Value = clsCommon.myCstr(dt1.Rows(0)("Route_No"))
            lblRouteDesc.Text = clsCommon.myCstr(dt1.Rows(0)("Route_Desc"))
            Price_code = clsCommon.myCstr(dt1.Rows(0)("price_CodeNon"))
            txtPriceCode.Text = clsCommon.myCstr(dt1.Rows(0)("price_CodeNon"))
        End If
    End Sub




    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                myMessages.blankValue("Booking not found to Print")
            Else
                LoadData(txtDocNo.Value, NavigatorType.Current)
                Export()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub Export()
        If gv1.Rows.Count > 0 Then
            ExportToExcel()
        Else
            common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
        End If
    End Sub
    Private Sub ExportToExcel()
        Try
            Dim strCreatedBy As String = clsDBFuncationality.getSingleValue("Select Created_By from TSPL_BOOKING_MATSER where Document_No='" + txtDocNo.Value + "'")
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strtemp As String = "Booking No : " + txtDocNo.Value
            arrHeader.Add(strtemp)
            strtemp = "Booking Date : " + clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy")
            arrHeader.Add(strtemp)
            strtemp = "Customer Name : " + clsCommon.myCstr(lblVendorName.Text)
            arrHeader.Add(strtemp)
            strtemp = "Location : " + clsCommon.myCstr(lblLocation.Text)
            arrHeader.Add(strtemp)
            strtemp = "Document Amount : " + clsCommon.myCdbl(lblTotRAmt1.Text)
            arrHeader.Add(strtemp)
            strtemp = "Created By : " + strCreatedBy
            arrHeader.Add(strtemp)
            'strtemp = "Transaction Type : " + IIf(rbtn_Fresh.IsChecked = True, "Fresh", "Ambient")
            'arrHeader.Add(strtemp)

            clsCommon.MyExportToExcelGrid("Booking Entry", gv1, arrHeader, Me.Text)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error, Me.Text)
        End Try
    End Sub


    'Private Sub funPrint()

    '    Try
    '        Dim dtBarCode As New DataTable

    '        dtBarCode.Columns.Add("BarCodeImage", GetType(Byte()))
    '        Dim bytes() As Byte
    '        Dim BitmapConverter As System.ComponentModel.TypeConverter = System.ComponentModel.TypeDescriptor.GetConverter(clsCommon.MyBarcodeImage(txtDocNo.Value, 1, False).[GetType]())
    '        bytes = DirectCast(BitmapConverter.ConvertTo(clsCommon.MyBarcodeImage(txtDocNo.Value, 1, False), GetType(Byte())), Byte())
    '        Dim Qry As String
    '        Dim FooterText As String
    '        Dim frm As New frmPurchaseOrder
    '        frm.strFormId = MyBase.Form_ID
    '        Qry = ""
    '        'Qry = "select Footer_Text  from TSPL_Crystal_Report_Footer_Setting where Frm_ID ='" + Form_ID + "' "
    '        'Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(Qry)
    '        'FooterText = dt1.Rows(0).Item("Footer_Text")
    '        FooterText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Footer_Text  from TSPL_Crystal_Report_Footer_Setting where Frm_ID ='" + Form_ID + "'"))
    '        Qry = " select TSPL_BOOKING_DETAIL_PRODUCTSALE.line_no,'" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MM/yyyy") + "' as RunDate,TSPL_BOOKING_MASTER_PRODUCTSALE.Cust_PO_No,'" + FooterText + "' as FooterText , TSPL_BOOKING_MASTER_PRODUCTSALE.Abandonment_No, TSPL_BOOKING_MASTER_PRODUCTSALE.Dept_Desc ,TSPL_BOOKING_MASTER_PRODUCTSALE.Delivery_date ,TSPL_BOOKING_MASTER_PRODUCTSALE.Remarks ,(case when TSPL_BOOKING_MASTER_PRODUCTSALE .status =1 then TSPL_BOOKING_MASTER_PRODUCTSALE.Modify_By else '' end) as Modify_By ,TSPL_BOOKING_DETAIL_PRODUCTSALE.Landing_Cost,TSPL_BOOKING_MASTER_PRODUCTSALE.Created_By , "
    '        Qry += " TSPL_BOOKING_MASTER_PRODUCTSALE.Terms_Code,TSPL_SHIP_TO_LOCATION.Telphone as ShipLoc_telphone,TSPL_LOCATION_MASTER.Service_Tax_Reg_No ,TSPL_SHIP_TO_LOCATION.Ship_To_Type_Desc as ShipLoc_Name,TSPL_SHIP_TO_LOCATION.email as shipLoc_Email,TSPL_LOCATION_MASTER.cst_no as CST_LST,TSPL_LOCATION_MASTER.Registration_Number,TSPL_LOCATION_MASTER.Add1 as loc_add1 ,TSPL_LOCATION_MASTER.add4 as loc_add4,TSPL_LOCATION_MASTER.Add2 as loc_add2,TSPL_LOCATION_MASTER.Add3 as loc_add3,TSPL_LOCATION_MASTER.Email as loc_email,TSPL_LOCATION_MASTER.TAN_No as loc_Fax , TSPL_BOOKING_DETAIL_PRODUCTSALE.TAX1_Rate,TSPL_BOOKING_DETAIL_PRODUCTSALE.PrincipleDesc ,TSPL_COMPANY_MASTER.Add2 as comp_Add2 ,TSPL_COMPANY_MASTER.Add3 as comp_Add3 ,ISNULL(TSPL_COMPANY_MASTER.Phone1,'')+ Case When ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as Company_Phone,TSPL_COMPANY_MASTER.Email as Comp_Email,TSPL_COMPANY_MASTER.Fax as comp_Fax,ISNULL(TSPL_CUSTOMER_MASTER.Phone1,'')+ Case When ISNULL(TSPL_CUSTOMER_MASTER.Phone2,'')<>'' Then ', '+ TSPL_CUSTOMER_MASTER.Phone2 Else'' End as Cust_Phone ,TSPL_CUSTOMER_MASTER.Email as Cust_Email,TSPL_CUSTOMER_MASTER.Fax as cust_Fax,(TSPL_BOOKING_DETAIL_PRODUCTSALE.Total_Tax_Amt /(case when TSPL_BOOKING_DETAIL_PRODUCTSALE.Amt_Less_Discount<=0 then 1 else TSPL_BOOKING_DETAIL_PRODUCTSALE.Amt_Less_Discount end)) *100 as Tax,((((TSPL_BOOKING_DETAIL_PRODUCTSALE.Total_Tax_Amt /(case when TSPL_BOOKING_DETAIL_PRODUCTSALE.Amt_Less_Discount<=0 then 1 else TSPL_BOOKING_DETAIL_PRODUCTSALE.Amt_Less_Discount end)) *100)*TSPL_BOOKING_DETAIL_PRODUCTSALE.Item_Cost/100) +TSPL_BOOKING_DETAIL_PRODUCTSALE.Item_Cost) as landing_Rate,(TSPL_BOOKING_DETAIL_PRODUCTSALE.Total_Tax_Amt /(case when TSPL_BOOKING_DETAIL_PRODUCTSALE.Amt_Less_Discount<=0 then 1 else TSPL_BOOKING_DETAIL_PRODUCTSALE.Amt_Less_Discount end) *100) as Tax_Lan,TSPL_BOOKING_MASTER_PRODUCTSALE.Mode_Of_Transport as  ModeofTransport,TSPL_BOOKING_DETAIL_PRODUCTSALE .Specification as  specification,  "
    '        Qry += " TSPL_BOOKING_MASTER_PRODUCTSALE.Document_Code as DocNo ,TSPL_SHIP_TO_LOCATION.add1 +case when len(TSPL_SHIP_TO_LOCATION.add2)>0 then ', '+TSPL_SHIP_TO_LOCATION.add2 else '' end +case when LEN(isnull(TSPL_SHIP_TO_LOCATION.Add3,''))>0 then ', '+isnull(TSPL_SHIP_TO_LOCATION.Add3,'') else ' ' end  + case when len(TSPL_SHIP_TO_LOCATION.State )>0 then TSPL_SHIP_TO_LOCATION.State else '' end  as Ship_address, TSPL_SHIP_TO_LOCATION.Tin_No as Ship_ToLocation_Tin_No, convert(varchar(10),TSPL_BOOKING_MASTER_PRODUCTSALE .Document_Date,103)  as po_date ,case TSPL_BOOKING_MASTER_PRODUCTSALE .SalesOrder_Type when 'L'then 'Local' when 'I' then 'Import' when 'J' then 'Job Work' when 'O' then 'Open' when 'S' then 'Specific'else 'Null' end as po_type , "
    '        Qry += " TSPL_BOOKING_MASTER_PRODUCTSALE.Customer_Code, TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_BOOKING_DETAIL_PRODUCTSALE.Item_Net_Amt as Total_Item_Net_amt,TSPL_BOOKING_DETAIL_PRODUCTSALE.MRP ,TSPL_BOOKING_DETAIL_PRODUCTSALE.Item_Cost, TSPL_BOOKING_MASTER_PRODUCTSALE .Terms_Code as termscode ,TSPL_BOOKING_MASTER_PRODUCTSALE .Ref_No as ref_no ,TSPL_BOOKING_MASTER_PRODUCTSALE .Comments as comments , "
    '        Qry += " TSPL_BOOKING_MASTER_PRODUCTSALE .Discount_Amt as dis_amt,TSPL_BOOKING_DETAIL_PRODUCTSALE .Disc_Amt  as dis_amt1,TSPL_BOOKING_MASTER_PRODUCTSALE.Amount_Less_Discount  as aftrdiscount ,TSPL_BOOKING_MASTER_PRODUCTSALE .Total_Amt as Total_amount,TSPL_BOOKING_MASTER_PRODUCTSALE.Discount_Base as bfrdisc_amount, "
    '        Qry += " tax1.Tax_Code_Desc as tax1name,isnull (TSPL_BOOKING_MASTER_PRODUCTSALE.tax1_amt,0) as txt1amt, "
    '        Qry += " tax2.Tax_Code_Desc as tax2name,isnull (TSPL_BOOKING_MASTER_PRODUCTSALE.tax2_amt,0) as txt2amt, "
    '        Qry += " tax3.Tax_Code_Desc as tax3name,isnull (TSPL_BOOKING_MASTER_PRODUCTSALE.tax3_amt,0) as txt3amt, "
    '        Qry += " tax4.Tax_Code_Desc as tax4name,isnull (TSPL_BOOKING_MASTER_PRODUCTSALE.tax4_amt,0) as txt4amt, "
    '        Qry += " tax5.Tax_Code_Desc as tax5name,isnull (TSPL_BOOKING_MASTER_PRODUCTSALE.tax5_amt,0) as txt5amt, "
    '        Qry += " tax6.Tax_Code_Desc as tax6name,isnull (TSPL_BOOKING_MASTER_PRODUCTSALE.tax6_amt,0) as txt6amt, "
    '        Qry += " tax7.Tax_Code_Desc as tax7name,isnull (TSPL_BOOKING_MASTER_PRODUCTSALE.tax7_amt,0) as txt7amt, "
    '        Qry += " tax8.Tax_Code_Desc as tax8name,isnull (TSPL_BOOKING_MASTER_PRODUCTSALE.tax8_amt,0) as txt8amt,  "
    '        Qry += " tax9.Tax_Code_Desc as tax9name,isnull (TSPL_BOOKING_MASTER_PRODUCTSALE.tax9_amt,0) as txt9amt, "
    '        Qry += " tax10.Tax_Code_Desc as tax10name,isnull (TSPL_BOOKING_MASTER_PRODUCTSALE.tax10_amt,0) as txt10amt, "
    '        Qry += " isnull(TSPL_BOOKING_MASTER_PRODUCTSALE .Total_Tax_Amt,0) as total_tax_amt, TSPL_BOOKING_MASTER_PRODUCTSALE.Total_Amt as DocAmt,  TSPL_COMPANY_MASTER.Comp_Name as compname,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,ISNULL(tspl_company_Master.ADD1,'') as address1,TSPL_BOOKING_DETAIL_PRODUCTSALE.item_code as item_code, TSPL_ITEM_MASTER.Item_Desc   as itemdesc, TSPL_BOOKING_DETAIL_PRODUCTSALE.Row_Type,TSPL_BOOKING_DETAIL_PRODUCTSALE.Qty as qty,TSPL_BOOKING_DETAIL_PRODUCTSALE.scheme_item as freeitem,TSPL_BOOKING_DETAIL_PRODUCTSALE.unit_code as uom,TSPL_BOOKING_DETAIL_PRODUCTSALE.item_cost as itemcost,TSPL_BOOKING_DETAIL_PRODUCTSALE.amount as amount ,"
    '        Qry += " TSPL_LOCATION_MASTER.TIN_No ,ISNULL(TSPL_LOCATION_MASTER.Phone1,'')+ Case When ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'' Then ', '+ TSPL_LOCATION_MASTER.Phone2 Else'' End as Location_Phone ,(Select Add1+Case When ISNULL(Add2,'')='' Then '' else ', '+Add2+ Case When ISNULL(Add3,'')='' Then '' Else ', '+Add3+ Case When ISNULL(Pin_Code,'')='' Then '' else '-'+CONVERT(varchar, Pin_Code) End End End from TSPL_LOCATION_MASTER Where TSPL_LOCATION_MASTER.Location_Code = TSPL_BOOKING_MASTER_PRODUCTSALE.Bill_To_Location ) as [BillToAddress],TSPL_CUSTOMER_MASTER.add1 +case when len(TSPL_CUSTOMER_MASTER.add2)>0 then ', '+TSPL_CUSTOMER_MASTER.add2 else '' end +case when LEN(isnull(TSPL_CUSTOMER_MASTER.Add3,''))>0 then ', '+isnull(TSPL_CUSTOMER_MASTER.Add3,'') else ' ' end  + case when len(TSPL_CUSTOMER_MASTER.State )>0 then TSPL_CUSTOMER_MASTER.State else '' end  as Cust_address,TSPL_CUSTOMER_MASTER.Lst_No as cust_Lst_No,TSPL_CUSTOMER_MASTER.Tin_No as Cust_Tin_No  ,TSPL_CUSTOMER_MASTER.CST as Cust_Cst,"
    '        Qry += " (Select Add1+Case When ISNULL(Add2,'')='' Then '' else ', '+Add2+ Case When ISNULL(Add3,'')='' Then '' Else ', '+Add3+ Case When ISNULL(Pin_Code,'')='' Then '' else '-'+CONVERT(varchar, Pin_Code) End End End from TSPL_LOCATION_MASTER Where TSPL_LOCATION_MASTER.Location_Code = TSPL_BOOKING_MASTER_PRODUCTSALE.Ship_To_Location ) as [ShipToAddress], TSPL_BOOKING_MASTER_PRODUCTSALE.TAX1,TSPL_BOOKING_MASTER_PRODUCTSALE.TAX2,TSPL_BOOKING_MASTER_PRODUCTSALE.TAX3,TSPL_BOOKING_MASTER_PRODUCTSALE.TAX4,TSPL_BOOKING_MASTER_PRODUCTSALE.TAX5,TSPL_BOOKING_MASTER_PRODUCTSALE.Total_Add_Charge "
    '        Qry += " from TSPL_BOOKING_DETAIL_PRODUCTSALE "
    '        Qry += " left outer join TSPL_BOOKING_MASTER_PRODUCTSALE  on TSPL_BOOKING_MASTER_PRODUCTSALE.Document_Code  =TSPL_BOOKING_DETAIL_PRODUCTSALE.Document_Code  "
    '        Qry += " left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_BOOKING_MASTER_PRODUCTSALE.tax1 "
    '        Qry += " left outer join  TSPL_SHIP_TO_LOCATION on TSPL_BOOKING_MASTER_PRODUCTSALE.Ship_To_Location =TSPL_SHIP_TO_LOCATION.Ship_To_Code "

    '        Qry += " left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_BOOKING_MASTER_PRODUCTSALE.tax2 "
    '        Qry += " left outer join tspl_tax_master as tax3 on tax3.Tax_Code=TSPL_BOOKING_MASTER_PRODUCTSALE .TAX3 "
    '        Qry += " left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_BOOKING_MASTER_PRODUCTSALE .tax4 "
    '        Qry += " left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_BOOKING_MASTER_PRODUCTSALE .tax5 "
    '        Qry += " left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_BOOKING_MASTER_PRODUCTSALE .TAX6 "
    '        Qry += " left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_BOOKING_MASTER_PRODUCTSALE .TAX7 "
    '        Qry += " left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =TSPL_BOOKING_MASTER_PRODUCTSALE .TAX8 "
    '        Qry += " left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_BOOKING_MASTER_PRODUCTSALE .TAX9 "
    '        Qry += " left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_BOOKING_MASTER_PRODUCTSALE .TAX10    "
    '        Qry += " left outer join TSPL_COMPANY_MASTER on  tspl_company_Master.Comp_Code = TSPL_BOOKING_MASTER_PRODUCTSALE.comp_code "
    '        Qry += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_BOOKING_MASTER_PRODUCTSALE.Customer_Code  "
    '        Qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=  TSPL_BOOKING_MASTER_PRODUCTSALE.Bill_To_Location"
    '        Qry += " Left Outer Join TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_BOOKING_DETAIL_PRODUCTSALE.Item_Code  where 2=2 "


    '        ''If txtReqNo.Value <> "" Then            Why you make check here of ReqNo--Balwinder to Pankaj
    '        Qry += "  and  TSPL_BOOKING_MASTER_PRODUCTSALE.Document_Code = '" + txtDocNo.Value + "'"
    '        '' End If

    '        'Qry = "Select *, Case When ISNULL(ShipToAddress,'')='' Then BillToAddress Else ShipToAddress End as [ShipToAddress] from ( " + Qry + " ) XXX"
    '        Qry = "Select *, Case When ISNULL(ShipToAddress,'')='' Then BillToAddress Else ShipToAddress End as [ShipToAddress] from ( " + Qry + " ) XXX,(select document_code,sum(TAX1_Amt) as t1,sum(TAX2_Amt) as t2,sum(TAX3_Amt) as t3,sum(TAX4_Amt) as t4,sum(TAX5_Amt) as t5,sum(TAX6_Amt) as t6,sum(TAX7_Amt) as t7,sum(TAX8_Amt) as t8,sum(TAX9_Amt) as t9,sum(TAX10_Amt) as t10,sum(Amount) as detamt,(sum(Disc_Amt)+sum(cust_discount)) as Disc_Amt,sum(Amt_Less_Discount) as Amt_Less_Discount,sum(Total_Tax_Amt) as totaltaxamt,sum(Item_Net_Amt) as Item_Net_Amt,sum(Total_Basic_Amt) as Total_Basic_Amt from TSPL_BOOKING_DETAIL_PRODUCTSALE where Scheme_Item='N' group by document_code)b where docno=b.document_code order by xxx.line_no"
    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
    '        dt.Columns.Add("BarCodeImage", GetType(Byte()))
    '        For Each dr As DataRow In dt.Rows
    '            dr("BarCodeImage") = bytes
    '        Next
    '        If dt.Rows.Count > 0 Then
    '            'SetItemWiseTax(dt, txtDocNo.Value)
    '            Dim frmCRV As New frmCrystalReportViewer()
    '            frmCRV.funreport(CrystalReportFolder.NewSalesReports, dt, "crptSalesOrderReport", "Sales Order")
    '            frmCRV = Nothing
    '        End If

    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub


    Private Sub gv1_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserDeletedRow
        UpdateAllTotals()
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(colLineNo).Value = ii
        Next
        'RefreshReqNo()
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then

            e.Cancel = True
            'ElseIf clsCommon.myCBool(gv1.CurrentRow.Cells(colItemUsedINGRN).Value) Then
            '    common.clsCommon.MyMessageBoxShow("Can't Delete The Current Row.This Item is Used In GRN")
            '    e.Cancel = True
        End If
    End Sub


    Private Sub gv1_CellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.CellFormatting
        Try

        Catch ex As Exception
            'common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub setGridFocus()
        Dim intCurrRow As Integer = gv1.CurrentRow.Index
        gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
        If intCurrRow = gv1.Rows.Count - 1 Then
            gv1.Rows.AddNew()
            'gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = RowTypeItem
            gv1.CurrentRow = gv1.Rows(intCurrRow)
        End If
        If clsCommon.myLen(gv1.CurrentRow.Cells(colICode).Value) > 0 Then
            If gv1.CurrentColumn Is gv1.Columns(colICode) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow)
                gv1.CurrentColumn = gv1.Columns(colQty)
            ElseIf gv1.CurrentColumn Is gv1.Columns(colQty) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow)
                gv1.CurrentColumn = gv1.Columns(colRate)

            End If
        End If
    End Sub


    Private Sub PrintAmendment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If clsCommon.myLen(txtDocNo.Value) <= 0 Then
        '    myMessages.blankValue("Purchase Order No not found to Print")
        'Else
        '    FrmPurchaseOrderReport.PrintAbandoment(txtDocNo.Value)
        'End If
    End Sub

    Private Sub txtDocNo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDocNo.Load

    End Sub

    Private Sub btnAmendment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub




    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ''Printing the amendment
        'If clsCommon.myLen(txtDocNo.Value) <= 0 Then
        '    common.clsCommon.MyMessageBoxShow("Purchase Order No not found to Print")
        'Else
        '    FrmPurchaseOrderReport.PrintAbandoment(txtDocNo.Value)
        'End If
    End Sub

    Dim i As Integer


    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                'gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = RowTypeItem
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub




    Private Sub gv1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv1.Click
        If gv1.CurrentRow IsNot Nothing Then
            'setBalance()
        End If
    End Sub

    Private Sub gv1_CurrentRowChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentRowChangedEventArgs) Handles gv1.CurrentRowChanged
        'If gv1.CurrentRow IsNot Nothing AndAlso Not e.CurrentRow.Index < 0 Then
        '    setBalance()
        'End If
    End Sub

    Private Sub txtDiscAmt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDiscAmt.Click

    End Sub



    Private Sub txtDiscPer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDiscPer.Click

    End Sub


    Private Sub RadLabel1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadLabel1.Click

    End Sub

    '--------------------------------------BM00000002443 Done By Monika 30/04/2014----------------------------------'
#Region "New Mail System"
    Private Sub RadMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem3.Click
        Dim frm As New FrmMailSMSSettingNew2()
        frm.FormId = clsUserMgtCode.frmDairyBookingCustomer
        frm.ShowDialog()
    End Sub

    Private Sub btnpreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Select Document No. First", Me.Text)
            txtDocNo.Focus()
            txtDocNo.Select()
            Return
        End If

        'attachqry = GetAtchmentPrintQuery(txtDocNo.Value)
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(attachqry)

        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            Dim frmCRV As New frmCrystalReportViewer()
            System.Diagnostics.Process.Start(frmCRV.EmailAttachment(CrystalReportFolder.NewSalesReports, dt1, "crptSalesOrderReport", "Sales Order"))
            frmCRV = Nothing
        End If
    End Sub

    'Private Sub btnsend_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
    '            clsCommon.MyMessageBoxShow("Please Select Document No. First", Me.Text)
    '            'txtReqNo.Focus()
    '            'txtReqNo.Select()
    '            Return
    '        End If

    '        If Not (common.clsCommon.MyMessageBoxShow("Send E-Mail/SMS Of Respective Sale Order No. " + txtDocNo.Value + "" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes) Then
    '            Return
    '        End If
    '        LoadData(txtDocNo.Value, NavigatorType.Current)
    '        Dim lstUsers As New List(Of String)
    '        lstUsers.Add(txtVendorNo.Value)
    '        SendSMSandEmail(lstUsers, False)
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub
    'Private Sub btnSendForApproval_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
    '            clsCommon.MyMessageBoxShow("Please Select Document No. First", Me.Text)
    '            'txtReqNo.Focus()
    '            'txtReqNo.Select()
    '            Return
    '        End If

    '        If Not (common.clsCommon.MyMessageBoxShow("Send E-Mail/SMS Of Respective Booking No. " + txtDocNo.Value + "" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes) Then
    '            Return
    '        End If
    '        LoadData(txtDocNo.Value, NavigatorType.Current)

    '        Dim qry As String = "Select * from TSPL_APPROVAL_LEVEL_SCREEN where 1=1 and Trans_Code='" + MyBase.Form_ID + "' "
    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

    '        Dim lstUsers As New List(Of String)
    '        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '            For Each dr As DataRow In dt.Rows
    '                lstUsers.Add(dr("User_Code").ToString())
    '            Next
    '        End If

    '        If lstUsers.Count = 0 Then
    '            Throw New Exception("No Receiptent Found")
    '        End If
    '        SendSMSandEmail(lstUsers, True)
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub
    'Private Sub SendSMSandEmail(ByVal lstUsers As List(Of String), ByVal isSendForApproval As Boolean)
    '    Try
    '        Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(clsUserMgtCode.frmSNSalesOrder)

    '        If obj Is Nothing Then
    '            clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
    '            Return
    '        End If
    '        If clsCommon.myLen(obj.mailsubjct) <= 0 Then
    '            clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
    '            Return
    '        End If

    '        Dim strContactPerson As String = ""
    '        Dim strSubject As String = obj.mailsubjct.Replace(clsEmailSMSConstants.SaleOrderNo, txtDocNo.Value)
    '        strSubject = strSubject.Replace(clsEmailSMSConstants.SaleOrderDate, clsCommon.GetPrintDate(txtDate.Text, "dd/MMM/yyyy"))

    '        Dim strbody As String = obj.mailbody.Replace(clsEmailSMSConstants.SaleOrderNo, txtDocNo.Value)
    '        strbody = strbody.Replace(clsEmailSMSConstants.SaleOrderDate, clsCommon.GetPrintDate(txtDate.Text, "dd/MMM/yyyy"))
    '        strbody = strbody.Replace(clsEmailSMSConstants.CustomerNo, txtVendorNo.Value)
    '        strbody = strbody.Replace(clsEmailSMSConstants.CustomerName, lblVendorName.Text)
    '        strbody = strbody.Replace(clsEmailSMSConstants.TotalAmount, lblTotRAmt.Text)
    '        strbody = strbody.Replace(clsEmailSMSConstants.Form_Code, MyBase.Form_ID)

    '        '------------------------code for attchament-------------------------------------
    '        Dim strRptPath As String = ""
    '        If obj.atchmnt = "Y" Then
    '            'attachqry = GetAtchmentPrintQuery(txtDocNo.Value)
    '            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(attachqry)
    '            If dt1.Rows.Count > 0 Then
    '                'SetItemWiseTax(dt1, txtDocNo.Value)
    '                Dim frmCRV As New frmCrystalReportViewer()
    '                strRptPath = frmCRV.EmailAttachment(CrystalReportFolder.NewSalesReports, dt1, "crptSalesOrderReport", "Sales Order")
    '                frmCRV = Nothing
    '            End If
    '        End If
    '        '---------------------------------------------------------------------------

    '        For Each strUser As String In lstUsers
    '            'lstUsers.Add(dr("User_Code").ToString())
    '            Dim lstReceiptents As New List(Of String)
    '            Dim qry As String = ""
    '            Dim emailId As String = ""
    '            If isSendForApproval Then
    '                strContactPerson = strUser
    '                qry = "select E_Mail from TSPL_USER_MASTER where User_Code in ('" + strUser + "') "
    '                emailId = clsDBFuncationality.getSingleValue(qry)
    '            Else
    '                strContactPerson = clsDBFuncationality.getSingleValue("select upper(substring(Contact_Person_Name,1,1)) + lower(substring(Contact_Person_Name,2,49)) from TSPL_customer_MASTER where cust_code ='" & strUser & "' ")
    '                emailId = clsDBFuncationality.getSingleValue("select Email from TSPL_customer_MASTER where cust_code ='" & strUser & "' ")
    '            End If

    '            strbody = strbody.Replace(clsEmailSMSConstants.ContactPerson, strContactPerson)
    '            lstReceiptents.Add(emailId)

    '            Dim body As String = strbody.Replace(clsEmailSMSConstants.UserCode, strUser)

    '            clsMailViaOutlook.SendEmail(strSubject, body, lstReceiptents, Nothing, strRptPath)
    '        Next
    '        clsCommon.MyMessageBoxShow("E-Mail Send Successfully", Me.Text)
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try

    'End Sub

    Public Sub SetMailRight()


    End Sub


#End Region



    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If

    End Sub

    Private Sub RadMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem4.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)

        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub

    Private Sub RadMenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem5.Click
        Dim frm As New FrmCrptFooter
        frm.strFormId = MyBase.Form_ID
        frm.ShowDialog()
    End Sub


    Private Sub btnHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim frm As New FrmSaleHistory
        frm.strFormId = MyBase.Form_ID
        frm.strCustId = txtVendorNo.Value
        frm.strCustName = lblVendorName.Text
        Dim strvendor As String = txtVendorNo.Value
        frm.ShowDialog()
        frm.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub lblTotRAmt1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub



    Private Sub txtDate_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtDate.Validating
        'SETGSTControl()
    End Sub


    Private Function txtRefNo() As Object
        Throw New NotImplementedException
    End Function


    Private Sub txtLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocation._MYValidating
        Dim qry As String = " select Location_Code as [Code],Location_Desc as [Description],Loc_Short_Name as [Short Name],Add1,Add2,Add3,Add4,City_Code as [City Code],State,Pin_Code as [Pin Code],Country,Telphone,Email,Location_Type as [Location Type],Loc_Status as [Location Status],Status_Date as [Status Date],Excisable,Loc_Segment_Code as [Location Segment Code],Type,Purchase_Tax_Group as [Purchase Tax Group],Sales_Tax_Group as [Sales Tax Group],Ecc_Number as [ECC Number],Registration_Number as [Registration Number],Commissionerate as [Commission Rate],Range_Code as [Range Code],Range_Name as [Range Name],Range_Address as [Range Address],Division_Code as [Division Code],Division_Name as [Division Name],Division_Address as [Division Address],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_code as [Company Code],TIN_No as [TIN No],TAN_No as [TAN No],TCAN_No as [TCAN No],Service_Tax_Reg_No as [Service Tax Registration No],DutyPaid as [Duty Paid],Purchase_Tax_GroupIS as [Purchase Tax Group Inter State],Sales_Tax_GroupIS as [Sales Tax Group Inter State],Stock_Transfer_Filled_Ac as [Stock Transfer Filled Account],Stock_Transfer_Empty_Ac as [Stock Transfer Empty Account],GIT_Location as [GIT Location],GIT_Type as [GIT Type],Rejected_Type as [Rejected Type],Rejected_Location as [Rejected Location],CSA_Type as [CSA Type],Cust_Code as [Cust Code],MCC_Type as [MCC Type],CST_No as [CST No],Phone1,Phone2  from TSPL_Location_MASTER"
        Dim WhrCls As String = " Loc_Status='N' and Location_Type='Physical' and Is_Section='N' and Is_Sub_Location='N' and CSA_Type <>'Y' and DutyPaid <>'Y' and Rejected_Type <>'Y' and GIT_Type<>'Y'"
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtLocation.Value = clsCommon.ShowSelectForm("MulDS-BOLocFndr", qry, "Code", WhrCls, txtLocation.Value, "Code", isButtonClicked)
        lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
        gv1.DataSource = Nothing
        If gv1.Rows.Count > 0 Then
            gv1.Focus()
            gv1.Rows(0).Cells(1).BeginEdit()

        End If
    End Sub

    Private Function CheckItemtaxType() As Boolean
        Throw New NotImplementedException
    End Function

    Private Sub btnCreateDO_Click(sender As Object, e As EventArgs) Handles btnCreateDO.Click

        RecordCount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_BOOKING_MATSER "))
        If RecordCount = 0 Then
            FlagFirstRecord = True
        Else
            FlagFirstRecord = False
        End If

        If Is_Cancelled = 1 Then
            clsCommon.MyMessageBoxShow(Me, "Booking Cancelled,Can not Creat/Post DO", Me.Text)
            Exit Sub
        End If
        If BookingStatus = 1 Then
            clsCommon.MyMessageBoxShow(Me, "Please Post booking before creating DO", Me.Text)
            Exit Sub
        ElseIf BookingStatus = 2 Then
            clsCommon.MyMessageBoxShow(Me, "Please Approve and post booking before creating DO", Me.Text)
            Exit Sub
        ElseIf BookingStatus = 3 Then
            clsCommon.MyMessageBoxShow(Me, "Please post booking before creating DO", Me.Text)
            Exit Sub
        End If
        If DOStatus = 2 Then
            clsCommon.MyMessageBoxShow(Me, "DO is pending for approval.", Me.Text)
            Exit Sub
        End If

        Dim qry As String = ""
        FlagCreateDo = True
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Try

            If CreateDO(False, Nothing, txtDocNo.Value) Then

                'trans.Commit()
                If clsCommon.myLen(DOmsg) > 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, DOmsg, Me.Text)
                End If
                If DOCreated = True Then
                    Dim msg = "Successfully created"
                    common.clsCommon.MyMessageBoxShow(Me, msg, Me.Text)
                    msg = Nothing
                End If
                LoadData(txtDocNo.Value, NavigatorType.Current)
            Else
                'trans.Rollback()
            End If
        Catch ex As Exception
            'trans.Rollback()
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            qry = Nothing
            FlagCreateDo = False
        End Try
    End Sub
    ''richa ERO/03/12/19-001138
    Private Function CreateDO(ByVal ChekPostBtn As Boolean, ByVal trans As SqlTransaction, ByVal strBookingNo As String)
        Try
            blnSaveTotalQTy = True
            Dim qry As String = String.Empty
            DOmsg = String.Empty
            Dim dblTotal_Qty As Double = 0
            Dim blnRatezero As Boolean = False
            'lblDONumber.Text = clsDBFuncationality.getSingleValue("select isnull(Delivery_No,'') from TSPL_BOOKING_DETAIL where Document_No ='" & txtDocNo.Value & "' and cust_code='" & txtVendorNo.Value & "'", trans)
            If (AllowToSave(trans)) Then


                'For ii As Integer = 8 To gv1.Columns.Count - 1
                'Dim objBookingCust As clsBookingTemp = TryCast(gv1.Columns(ii).Tag, clsBookingTemp)
                If clsCommon.myCdbl(lblTotRAmt1.Text) > 0 AndAlso BookingStatus = 4 And BookingStatus <> 5 Then
                    If (clsCommon.myLen(lblDONumber.Text) > 0 AndAlso DOStatus <> 4) OrElse (clsCommon.myLen(lblDONumber.Text) = 0 And clsCommon.myCdbl(lblTotRAmt1.Text) > 0) Then
                        Dim dblCustTotalQty As Double = 0
                        For Each grow As GridViewRowInfo In gv1.Rows
                            If clsCommon.myCdbl(grow.Cells(colQty).Value) > 0 Then
                                dblCustTotalQty += grow.Cells(colQty).Value
                            End If
                        Next
                        If clsCommon.myCdbl(dblCustTotalQty) > 0 Then
                            If clsCommon.myLen(lblDONumber.Text) = 0 Then
                                Dim obj As New clsDeliveryNoteDairySale
                                dblTotal_Qty = 0
                                'obj.Price_code = clsDBFuncationality.getSingleValue("select price_CodeNon from tspl_customer_master where cust_code='" & clsCommon.myCstr(grow.Cells(1).Value) & "'", trans)
                                obj.Credit_Limit = 0
                                obj.Document_Date = txtDate.Value
                                obj.Customer_Code = txtVendorNo.Value
                                obj.Location_Code = txtLocation.Value
                                dblTotal_Qty = dblCustTotalQty
                                obj.Sampling = IIf(chkSampling.Checked, 1, 0)
                                obj.Booking_No = txtDocNo.Value
                                obj.Booking_Date = txtDate.Value
                                obj.Ship_To_Location = txtShipToLocation.Value
                                obj.Vehicle_Capacity = 0
                                obj.Lorry_No = clsDBFuncationality.getSingleValue("select Vehicle_Code from TSPL_BOOKING_DETAIL where Document_No ='" & txtDocNo.Value & "' and cust_code='" & txtVendorNo.Value & "'", trans)
                                obj.Route_No = lblroutecode.Text
                                obj.Transporter_Name = obj.Lorry_No
                                obj.Price_code = txtPriceCode.Text
                                obj.Freight = ""
                                obj.Freight_Amount = 0
                                obj.Comments = ""
                                obj.OnHold = "N"
                                obj.Short_Close = "N"
                                obj.Total_Amt = 0

                                If CreateCommonDairyDispatchforFreshAmbient = 1 Then
                                    obj.TRANSACTION_TYPE = ""
                                Else
                                    obj.TRANSACTION_TYPE = IIf(rbtn_Fresh.IsChecked = True, "FS", "PS")
                                End If

                                obj.From_Screen_code = clsUserMgtCode.frmDairyBookingCustomer
                                obj.SalesmanCode = txtSalesman.Value
                                obj.Cust_PO_No = txtPONo.Text
                                If txtCustPODate.Checked Then
                                    obj.Podate = txtCustPODate.Value
                                End If
                                obj.Arr = New List(Of clsDeliveryNoteDairySaleDetail)
                                Dim intLineNo As Integer = 1
                                Dim dblTotal As Double = 0
                                blnRatezero = False
                                DOCreated = False
                                For Each grow As GridViewRowInfo In gv1.Rows
                                    Dim objTr As New clsDeliveryNoteDairySaleDetail()
                                    If (clsCommon.myCdbl(grow.Cells(colQty).Value)) > 0 Then

                                        objTr.Line_No = intLineNo
                                        objTr.Sampling = 0
                                        'Dim objBookingitem As clsBookingTemp = TryCast(grow.Cells(ii).Tag, clsBookingTemp)
                                        objTr.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                                        objTr.Unit_code = clsCommon.myCstr(grow.Cells(colUnit).Value)
                                        objTr.Booking_No = clsCommon.myCstr(txtDocNo.Value)
                                        objTr.Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                                        objTr.BookQty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                                        objTr.Balance_Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                                        objTr.OrgUnit_code = clsCommon.myCdbl(grow.Cells(colQty).Value)
                                        'Dim objBookingItemRate As clsBookingTemp = TryCast(grow.Cells(ii).Tag, clsBookingTemp)
                                        'sanjay


                                        Dim dblRate As Double = 0

                                        'dblRate = objBookingitem.ItemRate

                                        'Dim tax_on_amt As Decimal = 0
                                        Dim dt As New DataTable()

                                        'Dim dblTotal As Double = 0
                                        Dim Price_code As String = clsDBFuncationality.getSingleValue("select price_CodeNon from tspl_customer_master where cust_code='" & txtVendorNo.Value & "'", trans)
                                        Dim strCustName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER  where Cust_Code='" & txtVendorNo.Value & "'", trans))

                                        qry = "Select RowNo, Item_Price_ID, XXXE.Item_Code, UOM, Start_Date, Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,Item_MRP,XXXE.TAX1_Rate, XXXE.TAX2_Rate,XXXE.TAX3_Rate,XXXE.TAX4_Rate,XXXE.TAX5_Rate, " &
                       "XXXE.TAX6_Rate,XXXE.TAX7_Rate,XXXE.TAX8_Rate,XXXE.TAX9_Rate, XXXE.TAX10_Rate,XXXE.TAX1 ,XXXE.TAX2,XXXE.TAX3, XXXE.TAX4,XXXE.TAX5,XXXE.TAX6,XXXE.TAX7,XXXE.TAX8,XXXE.TAX9,XXXE.TAX10 from ( " &
                       "Select ROW_NUMBER() OVER (Partition By TSPL_ITEM_PRICE_MASTER.Item_Code ORDER BY TSPL_ITEM_PRICE_MASTER.Item_Code,  " &
                       "Start_Date Desc) as RowNo, Item_Price_ID, TSPL_ITEM_PRICE_MASTER.Item_Code, UOM, Start_Date,  " &
                       "Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,Item_MRP ,TSPL_ITEM_PRICE_MASTER.TAX1_Rate,TSPL_ITEM_PRICE_MASTER.TAX2_Rate,TSPL_ITEM_PRICE_MASTER.TAX3_Rate,TSPL_ITEM_PRICE_MASTER.TAX4_Rate,TSPL_ITEM_PRICE_MASTER.TAX5_Rate,  " &
                         "TSPL_ITEM_PRICE_MASTER.TAX6_Rate, TSPL_ITEM_PRICE_MASTER.TAX7_Rate, TSPL_ITEM_PRICE_MASTER.TAX8_Rate, TSPL_ITEM_PRICE_MASTER.TAX9_Rate, " &
                         "TSPL_ITEM_PRICE_MASTER.TAX10_Rate, TSPL_ITEM_PRICE_MASTER.TAX1, TSPL_ITEM_PRICE_MASTER.TAX2, TSPL_ITEM_PRICE_MASTER.TAX3, " &
                         "TSPL_ITEM_PRICE_MASTER.TAX4, TSPL_ITEM_PRICE_MASTER.TAX5, TSPL_ITEM_PRICE_MASTER.TAX6, TSPL_ITEM_PRICE_MASTER.TAX7,TSPL_ITEM_PRICE_MASTER.TAX8,TSPL_ITEM_PRICE_MASTER.TAX9,TSPL_ITEM_PRICE_MASTER.TAX10 from TSPL_ITEM_PRICE_MASTER  left  outer join  " &
                       "TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and  " &
                       "TSPL_ITEM_PRICE_MASTER.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code    where  Start_Date<='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  and (End_Date >= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  or End_date is null) and  " &
                       "TSPL_ITEM_PRICE_MASTER.Price_Code='" & Price_code & "' and UOM='" & clsCommon.myCstr(grow.Cells(colUnit).Value) & "' and TSPL_ITEM_PRICE_MASTER.item_code='" & clsCommon.myCstr(grow.Cells(colICode).Value) & "' AND Location_Code='" & clsCommon.myCstr(txtLocation.Value) & "'  "

                                        If ShowMulMRPOfSameItemOnDairyBookingCustomer = True AndAlso clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Customer_category,'') from tspl_customer_master where cust_code='" & clsCommon.myCstr(txtVendorNo.Value) & "' ", trans)), "Others") = CompairStringResult.Equal Then
                                            qry += " and round(TSPL_ITEM_PRICE_MASTER.Item_Selling_Price,2) =" & clsCommon.myCdbl(grow.Cells(colRate).Value) & " "
                                        End If
                                        qry += ") XXXE WHERE RowNo=1  "


                                        dt = clsDBFuncationality.GetDataTable(qry, trans)
                                        If dt.Rows.Count > 0 Then
                                            'If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GHO") = CompairStringResult.Equal Or CalculateTaxRatefromItemwsieTaxOnSale = True Then
                                            '    dblRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Basic_Price"))
                                            '    If dblRate = 0 Then
                                            '        Throw New Exception("Please Fill Basic Price for Location " & txtLocation.Value & "  for item " & gv1.Rows(intRow).Cells(colICode).Value & ".")
                                            '        Exit Sub
                                            '    End If
                                            'Else

                                            ''--------
                                            dblRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
                                            If dblRate = 0 Then
                                                Throw New Exception("Please Fill Selling Price for Location " & txtLocation.Value & "  for item " & clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value) & ".")
                                                Exit Function
                                            End If
                                            'End If
                                            objTr.MRP = clsCommon.myCdbl(dt.Rows(0).Item("Item_MRP"))
                                            objTr.Price_Date = clsCommon.myCstr(dt.Rows(0).Item("Start_Date"))
                                        Else
                                            Throw New Exception("Please create Price chart for customer " & txtVendorNo.Value & " for Location " & txtLocation.Value & "  for item " & clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value) & ".")
                                            blnSaveTotalQTy = False
                                            Exit Function
                                        End If


                                        '''''''''''scheme
                                        Dim obj_Cash As clsSchemeApplyOnDairy = Nothing
                                        Dim tax As Double = 0
                                        Dim tax_on_amt As Decimal = 0
                                        obj_Cash = clsSchemeApplyOnDairy.GetDiscountSchemeData(clsCommon.myCstr(grow.Cells(colICode).Value), clsCommon.myCstr(grow.Cells(colUnit).Value), clsCommon.myCdbl(grow.Cells(colQty).Value), txtVendorNo.Value, Nothing, trans)
                                        If obj_Cash IsNot Nothing Then
                                            gv1.CurrentRow.Cells(colDisc_Scheme_Amount).Value = clsCommon.myCdbl(obj_Cash.Cash_Amt) 'objBookingitem.Disc_Scheme_Amount = obj_Cash.Cash_Amt
                                            gv1.CurrentRow.Cells(colDisc_Scheme_Pers).Value = clsCommon.myCdbl(obj_Cash.Cash_Pers)  'objBookingitem.Disc_Scheme_Pers = obj_Cash.Cash_Pers
                                            gv1.CurrentRow.Cells(colDisc_Scheme_Code).Value = obj_Cash.Schm_Code        ' objBookingitem.Disc_Scheme_Code = obj_Cash.Schm_Code
                                            If clsCommon.myCdbl(obj_Cash.Cash_Pers) <> 0 Then
                                                gv1.CurrentRow.Cells(colDisc_Scheme_Type).Value = "P"
                                                gv1.CurrentRow.Cells(colDisc_Scheme_Amount).Value = System.Math.Round((dblRate * obj_Cash.Cash_Pers) / 100, 2)
                                            ElseIf clsCommon.myCdbl(obj_Cash.Cash_Amt) <> 0 Then
                                                gv1.CurrentRow.Cells(colDisc_Scheme_Type).Value = "A"
                                            End If

                                            dblRate = dblRate - gv1.CurrentRow.Cells(colDisc_Scheme_Amount).Value
                                            tax_on_amt = dblRate
                                            'Dim Alltax As Double = System.Math.Round((clsCommon.myCdbl(dt.Rows(0).Item("TAX1_Rate")) / 100 + clsCommon.myCdbl(dt.Rows(0).Item("TAX2_Rate")) / 100 + clsCommon.myCdbl(dt.Rows(0).Item("TAX3_Rate")) / 100 + clsCommon.myCdbl(dt.Rows(0).Item("TAX4_Rate")) / 100 + clsCommon.myCdbl(dt.Rows(0).Item("TAX5_Rate")) / 100 + clsCommon.myCdbl(dt.Rows(0).Item("TAX6_Rate")) / 100 + clsCommon.myCdbl(dt.Rows(0).Item("TAX7_Rate")) / 100 + clsCommon.myCdbl(dt.Rows(0).Item("TAX8_Rate")) / 100 + clsCommon.myCdbl(dt.Rows(0).Item("TAX9_Rate")) / 100 + clsCommon.myCdbl(dt.Rows(0).Item("TAX10_Rate"))), 3)
                                            'tax = System.Math.Round((dblRate * Alltax), 3)
                                            'dblRate = dblRate + tax


                                        End If


                                        ''''''''''''scheme


                                        'dt = clsDBFuncationality.GetDataTable(qry)
                                        'If dt.Rows.Count > 0 Then
                                        ' done by priti ERO/07/05/18-000297
                                        gv1.CurrentRow.Cells(colSellingRate).Value = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
                                        gv1.CurrentRow.Cells(colOrgRate).Value = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
                                        If ShowMulMRPOfSameItemOnDairyBookingCustomer = True Then
                                            isCellValueChangedOpen = True
                                        End If
                                        gv1.CurrentRow.Cells(colRate).Value = dblRate
                                        If ShowMulMRPOfSameItemOnDairyBookingCustomer = True Then
                                            isCellValueChangedOpen = False
                                        End If

                                        Dim DOCdateCurrent As Date? = Nothing
                                        DOCdateCurrent = clsCommon.GETSERVERDATE(trans)
                                        ' Query to get scheme type of Item
                                        Dim qryScheme As String = "Select TSPL_SCHEME_MASTER_NEW.Scheme_Type from TSPL_SCHEME_MASTER_NEW left outer join TSPL_SCHEME_DETAIL_NEW on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_MASTER_NEW.Scheme_Code "
                                        qryScheme += " left outer join tspl_item_master on tspl_item_master.item_code=TSPL_SCHEME_DETAIL_NEW.Item_Code "
                                        qryScheme += " where TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select scheme_code from (select ROW_NUMBER() over (partition by scheme_type order by start_date desc) as sno, TSPL_SCHEME_MASTER_NEW.Scheme_Code from "
                                        qryScheme += " TSPL_SCHEME_MASTER_NEW where TSPL_SCHEME_MASTER_NEW.Scheme_Type='Quantitive' and TSPL_SCHEME_MASTER_NEW.Status='Active' and  TSPL_SCHEME_MASTER_NEW.Start_Date<='" & clsCommon.GetPrintDate(DOCdateCurrent, "dd/MMM/yyyy") & "'  and (TSPL_SCHEME_MASTER_NEW.End_Date >= '" & clsCommon.GetPrintDate(DOCdateCurrent, "dd/MMM/yyyy") & "'  or TSPL_SCHEME_MASTER_NEW.End_date is null) and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select TSPL_SCHEME_DETAIL_NEW.Scheme_Code from "
                                        qryScheme += " TSPL_SCHEME_DETAIL_NEW left outer join TSPL_SCHEME_BENEFICIARY on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_BENEFICIARY.Scheme_Code where MainItem_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value) + "' and Cust_Code='" + txtVendorNo.Value + "'))a where a.sno=1)"
                                        qryScheme += " and TSPL_SCHEME_MASTER_NEW.Scheme_Code in (select Scheme_Code from TSPL_SCHEME_BENEFICIARY where Cust_Code='" + txtVendorNo.Value + "') and TSPL_SCHEME_MASTER_NEW.Status='Active'"
                                        qryScheme += " and TSPL_SCHEME_DETAIL_NEW.MainItem_Code='" + gv1.CurrentRow.Cells(colICode).Value + "' "


                                        qryScheme += " order by TSPL_SCHEME_MASTER_NEW.Scheme_Code"
                                        Dim SchemeType As String = clsDBFuncationality.getSingleValue(qryScheme, trans)
                                        If clsCommon.myLen(clsCommon.myCstr(SchemeType)) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(SchemeType), "Quantitive") = CompairStringResult.Equal Then
                                            Dim objD As clsSchemeApplyOnDairy = Nothing
                                            objD = clsSchemeApplyOnDairy.GetSchemeData(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value), txtVendorNo.Value, clsCommon.myCstr(gv1.CurrentRow.Cells(colSchemeType).Value), Nothing, Nothing, trans)

                                            If objD IsNot Nothing AndAlso objD.Arr.Count > 0 Then
                                                For Each objtrScheme As clsSchemeApplyOnDairy In objD.Arr
                                                    gv1.CurrentRow.Cells(colSchemeType).Value = objtrScheme.schm_Type
                                                    objTr.Scheme_Item_Code = objtrScheme.Schm_Icode
                                                    objTr.Scheme_Qty = objtrScheme.Schm_Qty
                                                    objTr.Scheme_Item_UOM = objtrScheme.Schm_Item_Uom
                                                    objTr.Scheme_Code = objtrScheme.Schm_Code
                                                Next

                                            End If
                                        End If


                                        objTr.Rate = clsCommon.myCdbl(grow.Cells(colRate).Value)
                                        'sanjay

                                        objTr.OrgRate = clsCommon.myCdbl(grow.Cells(colOrgRate).Value)
                                        objTr.SellingPrice = clsCommon.myCdbl(grow.Cells(colSellingRate).Value)

                                        objTr.Tax_NonTax = clsCommon.myCdbl(grow.Cells(colTax_NonTax).Value)
                                        objTr.FreshAmbient = clsCommon.myCstr(grow.Cells(colFreshAmbient).Value)

                                        objTr.Amount = clsCommon.myCdbl(clsCommon.myCdbl(grow.Cells(colAmt).Value))
                                        dblTotal += objTr.Amount

                                        'End If
                                        If objTr.Rate = 0 Then
                                            blnRatezero = True
                                            DOmsg += "Please create Price chart for customer " & txtVendorNo.Value & " for Location " & txtLocation.Value & "  for item " & objTr.Item_Code & "." + Environment.NewLine
                                        End If
                                        objTr.Price_Code = obj.Price_code
                                        objTr.OrgUnit_code = clsCommon.myCstr(clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value))

                                        If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                                            obj.Arr.Add(objTr)
                                        End If
                                        objTr.OrgRate = objTr.OrgRate
                                        intLineNo += 1
                                    End If

                                    obj.Total_Amt = dblTotal

                                    If ShowMulMRPOfSameItemOnDairyBookingCustomer Then
                                        qry = "Update TSPL_BOOKING_DETAIL set DO_Qty=" & objTr.Qty & ",Booking_Qty=" & objTr.Qty & ",Total_Qty=" & dblTotal_Qty & " where item_code='" & objTr.Item_Code & "' and Scheme_Item<>'Y' and vehicle_code='" & obj.Lorry_No & "' and  Document_No='" & txtDocNo.Value & "' and Cust_Code='" & obj.Customer_Code & "' and    Loc_Code='" & obj.Location_Code & "' and Sampling =" & obj.Sampling & " and Line_No='" & objTr.Line_No & "'"
                                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                                    Else
                                        qry = "Update TSPL_BOOKING_DETAIL set DO_Qty=" & objTr.Qty & ",Booking_Qty=" & objTr.Qty & ",Total_Qty=" & dblTotal_Qty & " where item_code='" & objTr.Item_Code & "' and Scheme_Item<>'Y' and vehicle_code='" & obj.Lorry_No & "' and  Document_No='" & txtDocNo.Value & "' and Cust_Code='" & obj.Customer_Code & "' and    Loc_Code='" & obj.Location_Code & "' and Sampling =" & obj.Sampling & ""
                                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                                    End If



                                Next

                                If (obj.Arr IsNot Nothing OrElse obj.Arr.Count > 0) Then
                                    If blnRatezero = False Then
                                        If CheckOutstandingOnbooking = 0 Then
                                            If AllowWo_Outstanding = False Then
                                                If CustomerOutstandingAmount(txtVendorNo.Value, trans) = False Then
                                                    obj.CreditApproval_Reqd = "Y"
                                                    obj.Status = 2
                                                End If
                                            End If
                                        End If
                                        Dim trans1 As SqlTransaction = clsDBFuncationality.GetTransactin()
                                        If (obj.SaveData(obj, True, trans1)) Then
                                            lblDONumber.Text = obj.Document_No
                                            qry = "Update TSPL_BOOKING_DETAIL set DO_Posted=" & obj.Status & ", Delivery_No='" & obj.Document_No & "',DocumentAmount=" & clsCommon.myCdbl(lblTotRAmt1.Text) & " where Document_No='" & txtDocNo.Value & "' and Cust_Code='" & txtVendorNo.Value & "' and    Loc_Code='" & txtLocation.Value & "' " ' and vehicle_code='" & lblvehiclecode.Text & "' and Sampling='" & 0 & "'
                                            clsDBFuncationality.ExecuteNonQuery(qry, trans1)
                                            qry = "Update TSPL_BOOKING_MATSER set CreateDO_Automatic=1 where Document_No='" & txtDocNo.Value & "' "
                                            clsDBFuncationality.ExecuteNonQuery(qry, trans1)
                                            DOCreated = True
                                            If (clsDeliveryNoteDairySale.PostData("DEL-NOTE-FS", obj.Document_No, trans1, 1)) Then
                                                'common.clsCommon.MyMessageBoxShow("Data Saved Successfully")

                                            End If
                                            trans1.Commit()
                                        Else
                                            trans1.Rollback()
                                        End If
                                    End If
                                End If

                                'DO STATUS 1 -open 2 - pending 3 - approved 4 - posted
                            ElseIf (DOStatus = 3) Then
                                If (clsDeliveryNoteDairySale.PostData("DEL-NOTE-FS", lblDONumber.Text, 1)) Then
                                    'common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                                    DOCreated = True
                                End If
                            End If
                        End If
                    End If
                End If
                'Next
            End If
            blnSaveTotalQTy = False
            'clsCommon.MyMessageBoxShow(msg)
            Return True
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            blnSaveTotalQTy = False
            Return False
        End Try
    End Function


    Private Sub MyLabel2_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnCopy_Click(sender As Object, e As EventArgs) Handles btnCopy.Click
        Try
            Dim PreviousDate As Date = clsCommon.GETSERVERDATE()
            PreviousDate = PreviousDate.AddDays(-1)
            Dim qry1 As String = "select distinct TSPL_BOOKING_MATSER.Document_No as DocumentNo,convert(varchar(12),TSPL_BOOKING_MATSER.Document_date,103) as Document_date,TSPL_CUSTOMER_MASTER.Customer_Name,(select isnull((Select distinct '['+TSPL_LOCATION_MASTER.Location_Desc +']  ' from TSPL_booking_detail left outer join TSPL_LOCATION_MASTER on TSPL_BOOKING_DETAIL.Loc_Code=TSPL_LOCATION_MASTER.Location_Code where TSPL_booking_detail.Document_No=TSPL_BOOKING_MATSER.Document_No   for xml path('')),'')  ) as Location  ,case when TSPL_BOOKING_MATSER.Posted=1 then 'posted' else 'Unposted' end as Posted from TSPL_BOOKING_MATSER"
            qry1 += " left join TSPL_BOOKING_DETAIL on TSPL_BOOKING_DETAIL.Document_No=TSPL_BOOKING_MATSER.Document_No "
            qry1 += " left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_BOOKING_DETAIL.Cust_Code "
            Dim whrClas As String = " TSPL_BOOKING_MATSER.comp_code='" + objCommonVar.CurrentCompanyCode + "' and TSPL_BOOKING_MATSER.Is_Taxable=2 "
            whrClas += " and convert(date,TSPL_BOOKING_MATSER.Document_Date,103) = convert(date,('" + PreviousDate + "'),103)"

            If clsCommon.myLen(txtVendorNo.Value) > 0 Then
                whrClas += " and TSPL_BOOKING_DETAIL.Cust_Code='" + txtVendorNo.Value + "'"
            End If

            Dim strCode As String = ""

            strCode = clsCommon.ShowSelectForm("PSBookingOrderNoFndd1", qry1, "DocumentNo", whrClas, "", "DocumentNo", True)
            If clsCommon.myLen(strCode) > 0 Then


                'FlagCopy = True
                Dim qry As String = ""
                Dim obj As New clsBookingEntryDairySale
                'Dim intRow As Integer
                obj = clsBookingEntryDairySale.GetData(strCode, NavigatorType.Current)

                If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then

                    btnSave.Enabled = True
                    btnPost.Enabled = True
                    btnDelete.Enabled = True
                    isInsideLoadData = False
                    ' btnCopy.Enabled = True
                    isNewEntry = True
                    'btnSave.Text = "Update"
                    BlankAllControls()

                    LoadBlankGrid()
                    txtLocation.Enabled = False
                    txtVendorNo.Enabled = False

                    'If obj.TRANSACTION_TYPE = "FS" Then
                    '    rbtn_Fresh.IsChecked = True
                    'ElseIf obj.TRANSACTION_TYPE = "PS" Then
                    '    rbtn_Ambient.IsChecked = True
                    'End If


                    txtLocation.Value = obj.location_code
                    lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))

                    GSTStatus = clsERPFuncationality.GetGSTStatus(txtDate.Value)



                    qry = "SELECT TSPL_BOOKING_DETAIL.*,TSPL_CUSTOMER_MASTER.Customer_Name FROM TSPL_BOOKING_DETAIL LEFT JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_BOOKING_DETAIL.Cust_Code WHERE Document_No='" + strCode + "' and scheme_item='N '"
                    Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry)
                    txtVendorNo.Value = clsCommon.myCstr(dt2.Rows(0)("Cust_Code"))
                    lblVendorName.Text = clsCommon.myCstr(dt2.Rows(0)("Customer_Name"))
                    lblTotRAmt1.Text = clsCommon.myCstr(dt2.Rows(0)("DocumentAmount"))
                    BookingStatus = 0 'clsCommon.myCstr(dt2.Rows(0)("Booking_Status"))
                    DOStatus = 0 'clsCommon.myCstr(dt2.Rows(0)("DO_Posted"))
                    qry = "select Customer_Name,vehicle_code,TSPL_VEHICLE_MASTER.Vehicle_No,Zone_Code,TSPL_CUSTOMER_MASTER.Route_No,Number,TSPL_ROUTE_MASTER.Route_Desc,tspl_customer_master.price_CodeNon from TSPL_CUSTOMER_MASTER left outer join " &
                            "TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No left outer join TSPL_VEHICLE_MASTER on " &
                            "TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id where Cust_Code='" & txtVendorNo.Value & "'"
                    Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
                    If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                        strVehicleCode = clsCommon.myCstr(dt1.Rows(0)("vehicle_code"))
                        lblvehiclecode.Text = clsCommon.myCstr(dt1.Rows(0)("vehicle_code"))
                        strVehicleDesc = clsCommon.myCstr(dt1.Rows(0)("Number"))
                        lblvehicleName.Text = clsCommon.myCstr(dt1.Rows(0)("Number"))
                        strRoutecode = clsCommon.myCstr(dt1.Rows(0)("Route_No"))
                        lblroutecode.Text = clsCommon.myCstr(dt1.Rows(0)("Route_No"))
                        strRouteDesc = clsCommon.myCstr(dt1.Rows(0)("Route_Desc"))
                        lblroutename.Text = clsCommon.myCstr(dt1.Rows(0)("Route_Desc"))
                        Price_code = clsCommon.myCstr(dt1.Rows(0)("price_CodeNon"))
                        txtPriceCode.Text = clsCommon.myCstr(dt1.Rows(0)("price_CodeNon"))
                    End If



                    For jj As Integer = 0 To dt2.Rows.Count() - 1
                        gv1.Rows.AddNew()

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count 'clsCommon.myCdbl(dt2.Rows(jj)("Line_No"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = clsCommon.myCstr(dt2.Rows(jj)("Item_Code"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select distinct item_desc from tspl_item_master where item_code='" + clsCommon.myCstr(dt2.Rows(jj)("Item_Code")) + "'"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIHSN).Value = clsItemMaster.GetItemHSNCode(clsCommon.myCstr(dt2.Rows(jj)("Item_Code")), Nothing)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColAvailableQty).Value = clsItemLocationDetails.getBalance(clsCommon.myCstr(dt2.Rows(jj)("Item_Code")), txtLocation.Value, txtDocNo.Value, txtDate.Value, Nothing, clsCommon.myCstr(dt2.Rows(jj)("Unit_Code")), 0)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = clsCommon.myCdbl(dt2.Rows(jj)("Booking_Qty"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgRate).Value = clsCommon.myCdbl(dt2.Rows(jj)("OrgRate"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = clsCommon.myCstr(dt2.Rows(jj)("Unit_Code"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = clsCommon.myCdbl(dt2.Rows(jj)("Item_Rate"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSellingRate).Value = clsCommon.myCdbl(dt2.Rows(jj)("Item_Selling_Price"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = Math.Round(clsCommon.myCdbl(dt2.Rows(jj)("Booking_Qty")) * clsCommon.myCdbl(dt2.Rows(jj)("Item_Rate")), 2)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTBaseAmt).Value = clsCommon.myCdbl(dt2.Rows(jj)("Tax_On_Amount"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTTaxAmt).Value = clsCommon.myCdbl(dt2.Rows(jj)("Tax_Amount"))

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDisc_Scheme_Amount).Value = clsCommon.myCdbl(dt2.Rows(jj)("Disc_Scheme_Amount"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDisc_Scheme_Code).Value = clsCommon.myCstr(dt2.Rows(jj)("Disc_Scheme_Code"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDisc_Scheme_Pers).Value = clsCommon.myCdbl(dt2.Rows(jj)("Disc_Scheme_Pers"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDisc_Scheme_Type).Value = clsCommon.myCstr(dt2.Rows(jj)("Disc_Scheme_Type"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeType).Value = clsCommon.myCstr(dt2.Rows(jj)("Scheme_Type"))

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax_NonTax).Value = clsCommon.myCstr(dt2.Rows(jj)("Tax_NonTax"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFreshAmbient).Value = clsCommon.myCdbl(dt2.Rows(jj)("FreshAmbient"))

                        'gv1.CurrentRow.Cells(ColAvgQty).Value = clsDBFuncationality.getSingleValue(qry)
                        UpdateCurrentRowAvgQty(gv1.CurrentRow.Index)
                    Next

                End If
                gv1.Rows.AddNew()
                If gv1.Rows.Count > 0 Then
                    gv1.Focus()
                    gv1.CurrentRow = gv1.Rows(0)
                    gv1.CurrentColumn = gv1.Columns(colQty)
                End If
                'End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub UpdateCurrentRowAvgQty(ByVal IntRowNo As Integer)
        Dim qry As String = ""
        If clsCommon.myLen(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colICode).Value)) > 0 AndAlso clsCommon.myLen(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colUnit).Value)) > 0 Then
            'ERO/30/07/18-000389 Change average qty calculation method
            'qry = "Select isnull(avg(XXXE.Booking_Qty),0) from ( " & _
            '               " Select ROW_NUMBER() OVER (Partition By TSPL_BOOKING_DETAIL.item_code ORDER BY TSPL_BOOKING_DETAIL.item_code,TSPL_BOOKING_MATSER.document_date Desc) as RowNo,TSPL_BOOKING_DETAIL.Booking_Qty " & _
            '               " from TSPL_BOOKING_DETAIL  " & _
            '               " left join TSPL_BOOKING_MATSER on TSPL_BOOKING_MATSER.Document_No=TSPL_BOOKING_DETAIL.Document_No " & _
            '               " left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_BOOKING_DETAIL.Item_Code " & _
            '               " where TSPL_BOOKING_DETAIL.Item_Code='" & clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colICode).Value) & "'" & _
            '               " and TSPL_BOOKING_DETAIL.Unit_code='" & clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colUnit).Value) & "' " & _
            '               " and TSPL_BOOKING_DETAIL.Cust_Code='" & txtVendorNo.Value & "' " & _
            '               " and TSPL_BOOKING_DETAIL.Loc_Code='" & txtLocation.Value & "' " & _
            '               " ) XXXE WHERE RowNo in (1,2,3) "
            'If clsCommon.myLen(lblDONumber.Text) = 0 Then
            '    qry = "select isnull(sum(TSPL_BOOKING_DETAIL.Booking_Qty)/3,0) " & _
            '          " from TSPL_BOOKING_DETAIL left join TSPL_BOOKING_MATSER on TSPL_BOOKING_MATSER.Document_No=TSPL_BOOKING_DETAIL.Document_No " & _
            '          " left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_BOOKING_DETAIL.Item_Code " & _
            '          " where TSPL_BOOKING_DETAIL.Item_Code='" & clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colICode).Value) & "'" & _
            '          " and TSPL_BOOKING_DETAIL.Unit_code='" & clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colUnit).Value) & "' " & _
            '          " and TSPL_BOOKING_DETAIL.Cust_Code='" & txtVendorNo.Value & "' " & _
            '          " and TSPL_BOOKING_DETAIL.Loc_Code='" & txtLocation.Value & "' " & _
            '          " and isnull(TSPL_BOOKING_MATSER.Is_Cancelled,0) = 0 " & _
            '          " and convert(date,TSPL_BOOKING_MATSER.Document_Date,103) >= convert(date,'" + txtDate.Value.AddDays(-3) + "',103)" & _
            '          " and convert(date,TSPL_BOOKING_MATSER.Document_Date,103) <= convert(date,'" + txtDate.Value.AddDays(-1) + "',103)"
            'Else
            qry = "select isnull(sum(TSPL_BOOKING_DETAIL.Booking_Qty)/3,0) " &
                " from TSPL_BOOKING_DETAIL left join TSPL_BOOKING_MATSER on TSPL_BOOKING_MATSER.Document_No=TSPL_BOOKING_DETAIL.Document_No " &
                 " left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_BOOKING_DETAIL.Item_Code " &
                " where TSPL_BOOKING_DETAIL.Item_Code='" & clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colICode).Value) & "'" &
                " and TSPL_BOOKING_DETAIL.Unit_code='" & clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colUnit).Value) & "' " &
                " and TSPL_BOOKING_DETAIL.Cust_Code='" & txtVendorNo.Value & "' " &
                " and TSPL_BOOKING_DETAIL.Loc_Code='" & txtLocation.Value & "' " &
                " and isnull(TSPL_BOOKING_MATSER.Is_Cancelled,0) = 0 " &
                " and convert(date,TSPL_BOOKING_MATSER.Document_Date,103) >= convert(date,'" + txtDate.Value.AddDays(-3) + "',103)" &
                " and convert(date,TSPL_BOOKING_MATSER.Document_Date,103) <= convert(date,'" + txtDate.Value.AddDays(-1) + "',103)" &
                " and  not exists (select 1 from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE " &
                " where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No=isnull(TSPL_BOOKING_DETAIL.Delivery_No,'') " &
                " and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Short_Close='Y') "


            'End If
            gv1.Rows(IntRowNo).Cells(ColAvgQty).Value = Math.Round(clsDBFuncationality.getSingleValue(qry), 2)
        End If
    End Sub


    Private Sub txtSalesman__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)
        Dim qry As String = "select EMP_CODE as Code,Emp_Name as Name from TSPL_EMPLOYEE_MASTER"
        Dim whrcls As String = "Emp_type='Salesman'"
        txtSalesman.Value = clsCommon.ShowSelectForm("DBC-SNOSaleman", qry, "Code", whrcls, txtSalesman.Value, "Code", isButtonClicked)
        lblSalesman.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Emp_Name as Name from TSPL_EMPLOYEE_MASTER where EMP_CODE ='" + txtSalesman.Value + "' and Emp_type='Salesman'"))
    End Sub

    Private Sub txtLocation_Leave(sender As Object, e As EventArgs) Handles txtLocation.Leave
        If clsCommon.myLen(txtLocation.Value) > 0 Then
            If gv1.Rows.Count > 0 Then
                gv1.Focus()
                gv1.CurrentRow = gv1.Rows(0)
                gv1.CurrentColumn = gv1.Columns(colICode)
            End If
            'gv1.Rows(0).Cells(colICode).IsSelected = True
        End If
    End Sub


    Private Sub MyLabel12_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Dim isSaved As Boolean = True
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                If clsCommon.myLen(lblDONumber.Text) > 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Booking Can not cancelled, DO already created!", Me.Text)
                    Exit Sub
                End If

                If common.clsCommon.MyMessageBoxShow("Do you want to cancel the Booking?", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                    Dim qrySaveCancel = "Update tspl_booking_matser set Is_Cancelled=1 where Document_No='" & txtDocNo.Value & "'"
                    isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qrySaveCancel)
                    If isSaved = True Then
                        clsCommon.MyMessageBoxShow(Me, "Booking cancelled successfully!", Me.Text)
                        LoadData(txtDocNo.Value, NavigatorType.Current)
                    End If

                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try


    End Sub
    ''richa ERO/11/12/18-000431 -------------- 11 Dec,2018
    Private Sub btnreverse_Click(sender As Object, e As EventArgs) Handles btnreverse.Click
        Try
            If common.clsCommon.MyMessageBoxShow(Me, "Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                '' REASON FOR DELETE 
                Dim Reason As String = ""
                Dim frm As New FrmFreeTxtBox1
                frm.Text = "Remarks for Reverse"
                frm.ShowDialog()
                If clsCommon.myLen(frm.strRmks) <= 0 Then
                    Exit Sub
                Else
                    Reason = frm.strRmks
                End If
                If clsBookingEntryDairySale.ReverseAndUnpost(txtDocNo.Value) Then
                    saveCancelLog(Reason, "Reverse And Recreate", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    '====added by preeti gupta Against ticket no[TEC/05/06/19-000520,TEC/30/07/19-000968]
    Private Sub rbtn_Fresh_ToggleStateChanged(sender As Object, args As StateChangedEventArgs)
        LoadBlankGrid()
        gv1.Rows.AddNew()
    End Sub

    Private Sub rbtn_Ambient_ToggleStateChanged(sender As Object, args As StateChangedEventArgs)
        LoadBlankGrid()
        gv1.Rows.AddNew()
    End Sub
    ''richa 16 Sep,2019 ERO/11/09/19-001027
    Private Sub chkSampling_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkSampling.ToggleStateChanged
        For i As Integer = 0 To gv1.Rows.Count - 1
            If chkSampling.Checked = False And DonotAllowtoChangeUOMinDairyBookingCustomer = True Then
                gv1.Rows(i).Cells(colUnit).ReadOnly = True
            Else
                gv1.Rows(i).Cells(colUnit).ReadOnly = False
            End If
        Next
    End Sub

    Private Sub cmbBookingType_Leave(sender As Object, e As EventArgs) Handles cmbBookingType.Leave
        If clsCommon.CompairString(cmbBookingType.Text, "Select") <> CompairStringResult.Equal Then
            If gv1.Rows.Count > 0 Then
                gv1.Focus()
                gv1.CurrentRow = gv1.Rows(0)
                gv1.CurrentColumn = gv1.Columns(colQty)
            End If
        End If
    End Sub


    Private Sub cmbBookingType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles cmbBookingType.SelectedIndexChanged
        Try
            ''richa VIJ/09/12/19-000099
            If clsCommon.myLen(txtVendorNo.Value) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Customer_category,'') from tspl_customer_master where cust_code='" & clsCommon.myCstr(txtVendorNo.Value) & "' ")), "Others") = CompairStringResult.Equal Then
                txtDate.Value = clsCommon.GETSERVERDATE()
            ElseIf clsCommon.CompairString(cmbBookingType.Text, "Select") <> CompairStringResult.Equal Then
                txtDate.Value = clsCommon.GETSERVERDATE().AddDays(1)
            Else
                txtDate.Value = clsCommon.GETSERVERDATE()
            End If
            If IsLoadBookingType = False Then
                LoadItemsFromPreviousBooking()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    ''richa agarwal VIJ/28/11/19-000081 29 Nov,2019
    Sub LoadItemsFromPreviousBooking()
        Try
            If clsCommon.myLen(txtLocation.Value) <= 0 Then
                Throw New Exception("Please select Location")
            End If
            If clsCommon.myLen(txtVendorNo.Value) <= 0 Then
                Throw New Exception("Please select Customer")
            End If

            Dim qry As String = "select TOP 1 TSPL_BOOKING_MATSER.Document_No  from TSPL_BOOKING_MATSER" & Environment.NewLine &
            " LEFT OUTER JOIN TSPL_BOOKING_DETAIL ON TSPL_BOOKING_MATSER.Document_No =TSPL_BOOKING_DETAIL.Document_No " & Environment.NewLine &
            " where TSPL_BOOKING_MATSER.From_Screen_Code ='BOOK-DS-CU'  and TSPL_BOOKING_MATSER.location_code ='" & txtLocation.Value & "' and TSPL_BOOKING_MATSER.Booking_Type ='" & clsCommon.myCstr(cmbBookingType.Text) & "' and TSPL_BOOKING_DETAIL .Cust_Code ='" & txtVendorNo.Value & "'" & Environment.NewLine &
            " order by TSPL_BOOKING_MATSER.Document_Date desc"

            Dim strBookingNo As String = clsDBFuncationality.getSingleValue(qry, Nothing)
            If clsCommon.CompairString(clsCommon.myCstr(cmbBookingType.Text), "FESTIVE ORDER") = CompairStringResult.Equal Then
                strBookingNo = Nothing
            End If
            If PrintTruckSheetAfterGenerate = True AndAlso clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Customer_category,'') from tspl_customer_master where cust_code='" & clsCommon.myCstr(txtVendorNo.Value) & "' ")), "Others") <> CompairStringResult.Equal Then
                Dim isTruckSheetGenerated As Boolean = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select max(truckSheetGenerate) from TSPL_BOOKING_MATSER where convert(date,Document_Date ,103)=convert(date,'" & txtDate.Value & "',103)")) = 1, True, False)
                If isTruckSheetGenerated = True Then
                    strBookingNo = Nothing
                End If
            End If

            If clsCommon.myLen(strBookingNo) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Customer_category,'') from tspl_customer_master where cust_code='" & clsCommon.myCstr(txtVendorNo.Value) & "' ")), "Others") <> CompairStringResult.Equal Then

                If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Customer_category,'') from tspl_customer_master where cust_code='" & clsCommon.myCstr(txtVendorNo.Value) & "' ")), "Others") <> CompairStringResult.Equal Then
                    qry = "SELECT FINAL.Item_Code,FINAL.Item_Desc,FINAL.Short_Description,FINAL.Unit_code,FINAL.IsTaxable,FINAL.IsFreshAmbient,FINAL.HSN_Code,FINAL.Booking_Qty FROM ( " &
                        "select CASE WHEN Z.Booking_Qty>0 THEN 1 ELSE 0 END AS HasQty,* from ( select tspl_booking_detail.item_code,tspl_item_master.Item_Desc,tspl_item_master.Short_Description ,tspl_booking_detail.Unit_code ,tspl_item_master.IsTaxable," & Environment.NewLine &
                " case when tspl_item_master.Is_Ambient=1 then 'PS' WHEN tspl_item_master.Is_FreshItem=1 THEN 'FS' ELSE '' END  IsFreshAmbient,tspl_item_master.HSN_Code,tspl_booking_detail.Booking_Qty,tspl_item_master.Sku_Seq from tspl_booking_detail " & Environment.NewLine &
                " LEFT OUTER JOIN tspl_item_master ON tspl_item_master.item_code=tspl_booking_detail.iTEM_CODE  " & Environment.NewLine &
                " where tspl_booking_detail.Document_No='" & strBookingNo & "' AND tspl_booking_detail.Scheme_Item ='N'" & Environment.NewLine &
                " union " & Environment.NewLine &
                " select tspl_item_master.item_code,tspl_item_master.Item_Desc,tspl_item_master.Short_Description ,TSPL_ITEM_UOM_DETAIL.UOM_Code ,tspl_item_master.IsTaxable," & Environment.NewLine &
                " case when tspl_item_master.Is_Ambient=1 then 'PS' WHEN tspl_item_master.Is_FreshItem=1 THEN 'FS' ELSE '' END  IsFreshAmbient,tspl_item_master.HSN_Code,0 as Booking_Qty,tspl_item_master.Sku_Seq   from tspl_item_master " & Environment.NewLine &
                " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code =tspl_item_master.Item_Code " & Environment.NewLine &
                " where isnull(tspl_item_master.Chilled_Freezen,0)=1 and isnull(TSPL_ITEM_MASTER.item_type,'')='F'  and tspl_item_master.Active=1 and TSPL_ITEM_UOM_DETAIL.Default_UOM=1" & Environment.NewLine &
                " and tspl_item_master.Item_Code not in ( select tspl_booking_detail.item_code from tspl_booking_detail" & Environment.NewLine &
                " LEFT OUTER JOIN tspl_item_master ON tspl_item_master.item_code=tspl_booking_detail.iTEM_CODE " & Environment.NewLine &
                " where tspl_booking_detail.Document_No='" & strBookingNo & "' AND tspl_booking_detail.Scheme_Item ='N'))z" & Environment.NewLine &
                "  )FINAL order by FINAL.HasQty desc,FINAL.Sku_Seq asc"
                Else
                    'qry = "select tspl_booking_detail.item_code,tspl_item_master.Item_Desc,tspl_item_master.Short_Description ,tspl_booking_detail.Unit_code ,tspl_item_master.IsTaxable," & Environment.NewLine & _
                    '" case when tspl_item_master.Is_Ambient=1 then 'PS' WHEN tspl_item_master.Is_FreshItem=1 THEN 'FS' ELSE '' END  IsFreshAmbient,tspl_item_master.HSN_Code,tspl_booking_detail.Booking_Qty   from tspl_booking_detail" & Environment.NewLine & _
                    '" LEFT OUTER JOIN tspl_item_master ON tspl_item_master.item_code=tspl_booking_detail.iTEM_CODE " & Environment.NewLine & _
                    '" where tspl_booking_detail.Document_No='" & strBookingNo & "' AND tspl_booking_detail.Scheme_Item ='N'"
                    qry = "SELECT FINAL.Item_Code,FINAL.Item_Desc,FINAL.Short_Description,FINAL.Unit_code,FINAL.IsTaxable,FINAL.IsFreshAmbient,FINAL.HSN_Code,FINAL.Booking_Qty FROM ( " &
                         "select CASE WHEN Z.Booking_Qty>0 THEN 1 ELSE 0 END AS HasQty,* from ( select tspl_booking_detail.item_code,tspl_item_master.Item_Desc,tspl_item_master.Short_Description ,tspl_booking_detail.Unit_code ,tspl_item_master.IsTaxable," & Environment.NewLine &
                  " case when tspl_item_master.Is_Ambient=1 then 'PS' WHEN tspl_item_master.Is_FreshItem=1 THEN 'FS' ELSE '' END  IsFreshAmbient,tspl_item_master.HSN_Code,tspl_booking_detail.Booking_Qty,tspl_item_master.Marketing_Seq   from tspl_booking_detail" & Environment.NewLine &
                  " LEFT OUTER JOIN tspl_item_master ON tspl_item_master.item_code=tspl_booking_detail.iTEM_CODE " & Environment.NewLine &
                  " where tspl_booking_detail.Document_No='" & strBookingNo & "' AND tspl_booking_detail.Scheme_Item ='N'" & Environment.NewLine &
                  " union " & Environment.NewLine &
                  " select tspl_item_master.item_code,tspl_item_master.Item_Desc,tspl_item_master.Short_Description ,TSPL_ITEM_UOM_DETAIL.UOM_Code ,tspl_item_master.IsTaxable," & Environment.NewLine &
                  " case when tspl_item_master.Is_Ambient=1 then 'PS' WHEN tspl_item_master.Is_FreshItem=1 THEN 'FS' ELSE '' END  IsFreshAmbient,tspl_item_master.HSN_Code,0 as Booking_Qty,tspl_item_master.Marketing_Seq   from tspl_item_master " & Environment.NewLine &
                  " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code =tspl_item_master.Item_Code " & Environment.NewLine &
                  " where isnull(tspl_item_master.Item_Type,'')='F' and isnull(tspl_item_master.TypeOfItm,'')='A' and tspl_item_master.Active=1 and TSPL_ITEM_UOM_DETAIL.Default_UOM=1 " & Environment.NewLine &
                  " and tspl_item_master.Item_Code not in ( select tspl_booking_detail.item_code from tspl_booking_detail" & Environment.NewLine &
                  " LEFT OUTER JOIN tspl_item_master ON tspl_item_master.item_code=tspl_booking_detail.iTEM_CODE " & Environment.NewLine &
                  " where tspl_booking_detail.Document_No='" & strBookingNo & "' AND tspl_booking_detail.Scheme_Item ='N'))z" & Environment.NewLine &
                  "  )FINAL order by FINAL.HasQty desc,FINAL.Marketing_Seq asc"

                End If

                'qry = "select tspl_booking_detail.item_code,tspl_item_master.Item_Desc,tspl_item_master.Short_Description ,tspl_booking_detail.Unit_code ,tspl_item_master.IsTaxable," & Environment.NewLine & _
                '" case when tspl_item_master.Is_Ambient=1 then 'PS' WHEN tspl_item_master.Is_FreshItem=1 THEN 'FS' ELSE '' END  IsFreshAmbient,tspl_item_master.HSN_Code,tspl_booking_detail.Booking_Qty   from tspl_booking_detail" & Environment.NewLine & _
                '" LEFT OUTER JOIN tspl_item_master ON tspl_item_master.item_code=tspl_booking_detail.iTEM_CODE " & Environment.NewLine & _
                '" where tspl_booking_detail.Document_No='" & strBookingNo & "' AND tspl_booking_detail.Scheme_Item ='N'"



                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, Nothing)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    LoadBlankGrid()
                    isInsideLoadData = True
                    Dim i As Integer = 0
                    For Each dr As DataRow In dt.Rows
                        isCellValueChangedOpen = True
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = i + 1
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = clsCommon.myCstr(dr("item_code"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = clsCommon.myCstr(dr("Item_Desc"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIShortName).Value = clsCommon.myCstr(dr("Short_Description"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIHSN).Value = clsCommon.myCstr(dr("HSN_Code"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = clsCommon.myCstr(dr("Unit_code"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax_NonTax).Value = clsCommon.myCstr(dr("IsTaxable"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFreshAmbient).Value = clsCommon.myCstr(dr("IsFreshAmbient"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPreviousQty).Value = clsCommon.myCdbl(dr("Booking_Qty"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = clsCommon.myCdbl(dr("Booking_Qty"))
                        If clsCommon.myCdbl(clsCommon.myCdbl(dr("Booking_Qty"))) > 0 Then
                            ItemPrice(clsCommon.myCstr(dr("item_code")), clsCommon.myCstr(dr("Unit_code")), clsCommon.myCdbl(dr("Booking_Qty")), gv1.Rows.Count - 1)
                        End If
                        isCellValueChangedOpen = False
                        i = i + 1
                    Next
                    isInsideLoadData = False
                    UpdateAllTotals()
                Else
                    gv1.DataSource = Nothing
                End If
            Else
                If clsCommon.myLen(txtVendorNo.Value) > 0 Then
                    Dim strCustomerCategory As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Customer_category,'') from tspl_customer_master where cust_code='" & clsCommon.myCstr(txtVendorNo.Value) & "'"))
                    If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Customer_category,'') from tspl_customer_master where cust_code='" & clsCommon.myCstr(txtVendorNo.Value) & "' ")), "Others") <> CompairStringResult.Equal Then

                        qry = "select tspl_item_master.item_code,tspl_item_master.Item_Desc,tspl_item_master.Short_Description ,case when len (isnull(TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Default_UOM ,'')) > 0 then TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Default_UOM else TSPL_ITEM_UOM_DETAIL.UOM_Code end as UOM_Code ,tspl_item_master.IsTaxable," & Environment.NewLine &
                  " case when tspl_item_master.Is_Ambient=1 then 'PS' WHEN tspl_item_master.Is_FreshItem=1 THEN 'FS' ELSE '' END  IsFreshAmbient,tspl_item_master.HSN_Code  from tspl_item_master " & Environment.NewLine &
                  " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code =tspl_item_master.Item_Code " & Environment.NewLine &
                  " left outer join TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS on TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Item_Code = tspl_item_master.Item_Code and TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Customer_Category = '" + strCustomerCategory + "' " & Environment.NewLine &
                  " where isnull(tspl_item_master.Chilled_Freezen,0)=1 and isnull(TSPL_ITEM_MASTER.item_type,'')='F'  and tspl_item_master.Active=1 and TSPL_ITEM_UOM_DETAIL.Default_UOM=1  order by tspl_item_master.Sku_Seq"

                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, Nothing)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            LoadBlankGrid()
                            isInsideLoadData = True
                            Dim i As Integer = 0
                            For Each dr As DataRow In dt.Rows
                                isCellValueChangedOpen = True
                                gv1.Rows.AddNew()
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = i + 1
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = clsCommon.myCstr(dr("item_code"))
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = clsCommon.myCstr(dr("Item_Desc"))
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colIShortName).Value = clsCommon.myCstr(dr("Short_Description"))
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colIHSN).Value = clsCommon.myCstr(dr("HSN_Code"))
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = clsCommon.myCstr(dr("UOM_Code"))
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colTax_NonTax).Value = clsCommon.myCstr(dr("IsTaxable"))
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colFreshAmbient).Value = clsCommon.myCstr(dr("IsFreshAmbient"))
                                isCellValueChangedOpen = False
                                i = i + 1
                            Next
                            isInsideLoadData = False
                            UpdateAllTotals()
                        Else
                            gv1.DataSource = Nothing
                        End If

                    Else

                        qry = " select tspl_item_master.item_code,tspl_item_master.Item_Desc,tspl_item_master.Short_Description ,case when len (isnull(TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Default_UOM ,'')) > 0 then TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Default_UOM else TSPL_ITEM_UOM_DETAIL.UOM_Code end as UOM_Code ,tspl_item_master.IsTaxable," & Environment.NewLine &
                " case when tspl_item_master.Is_Ambient=1 then 'PS' WHEN tspl_item_master.Is_FreshItem=1 THEN 'FS' ELSE '' END  IsFreshAmbient,tspl_item_master.HSN_Code  from tspl_item_master " & Environment.NewLine &
                " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code =tspl_item_master.Item_Code " & Environment.NewLine &
                " left outer join TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS on TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Item_Code = tspl_item_master.Item_Code and TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Customer_Category = '" + strCustomerCategory + "' " & Environment.NewLine &
                " where isnull(tspl_item_master.Item_Type,'')='F' and isnull(tspl_item_master.TypeOfItm,'')='A' and tspl_item_master.Active=1 and TSPL_ITEM_UOM_DETAIL.Default_UOM=1 order by Marketing_Seq"

                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, Nothing)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            LoadBlankGrid()
                            isInsideLoadData = True
                            Dim i As Integer = 0
                            For Each dr As DataRow In dt.Rows
                                isCellValueChangedOpen = True
                                gv1.Rows.AddNew()
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = i + 1
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = clsCommon.myCstr(dr("item_code"))
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = clsCommon.myCstr(dr("Item_Desc"))
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colIShortName).Value = clsCommon.myCstr(dr("Short_Description"))
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colIHSN).Value = clsCommon.myCstr(dr("HSN_Code"))
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = clsCommon.myCstr(dr("UOM_Code"))
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colTax_NonTax).Value = clsCommon.myCstr(dr("IsTaxable"))
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colFreshAmbient).Value = clsCommon.myCstr(dr("IsFreshAmbient"))
                                isCellValueChangedOpen = False
                                i = i + 1
                            Next
                            isInsideLoadData = False
                            UpdateAllTotals()
                        Else
                            gv1.DataSource = Nothing
                        End If


                    End If
                End If
            End If

            If gv1.Rows.Count > 0 Then
                gv1.Focus()
                gv1.CurrentRow = gv1.Rows(0)
                gv1.CurrentColumn = gv1.Columns(colQty)
            End If
            cmbBookingType.Focus()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Sub LoadBookingType()
        Try
            IsLoadBookingType = True
            'If clsCommon.myLen(txtVendorNo.Value) <= 0 Then
            '    Throw New Exception("Please select Customer First")
            'End If
            Dim CustomerCategory As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Customer_category,'') from tspl_customer_master where cust_code='" & clsCommon.myCstr(txtVendorNo.Value) & "' "))
            Dim dt As DataTable = New DataTable()
            dt.Columns.Add("Code", GetType(String))
            dt.Columns.Add("Name", GetType(String))

            Dim dr As DataRow = dt.NewRow()
            If clsCommon.CompairString(CustomerCategory, "Vendor") = CompairStringResult.Equal Then
                dr = dt.NewRow()
                dr("Code") = "CASH"
                dr("Name") = "CASH"
                dt.Rows.Add(dr)

                dr = dt.NewRow()
                dr("Code") = "FESTIVE ORDER"
                dr("Name") = "FESTIVE ORDER"
                dt.Rows.Add(dr)
            ElseIf clsCommon.CompairString(CustomerCategory, "Institution SO") = CompairStringResult.Equal Then
                dr = dt.NewRow()
                dr("Code") = "SO"
                dr("Name") = "SO"
                dt.Rows.Add(dr)
            ElseIf clsCommon.CompairString(CustomerCategory, "Institution CR") = CompairStringResult.Equal Then
                dr = dt.NewRow()
                dr("Code") = "CR"
                dr("Name") = "CR"
                dt.Rows.Add(dr)

            ElseIf clsCommon.CompairString(CustomerCategory, "Distributor") = CompairStringResult.Equal Then
                dr = dt.NewRow()
                dr("Code") = "CASH"
                dr("Name") = "CASH"
                dt.Rows.Add(dr)

            Else
                dr("Code") = ""
                dr("Name") = "Select"
                dt.Rows.Add(dr)

                dr = dt.NewRow()
                dr("Code") = "CASH"
                dr("Name") = "CASH"
                dt.Rows.Add(dr)

                dr = dt.NewRow()
                dr("Code") = "FESTIVE ORDER"
                dr("Name") = "FESTIVE ORDER"
                dt.Rows.Add(dr)

                dr = dt.NewRow()
                dr("Code") = "CR"
                dr("Name") = "CR"
                dt.Rows.Add(dr)

                dr = dt.NewRow()
                dr("Code") = "SO"
                dr("Name") = "SO"
                dt.Rows.Add(dr)

            End If
            cmbBookingType.DataSource = dt
            cmbBookingType.ValueMember = "Code"
            cmbBookingType.DisplayMember = "Name"
            IsLoadBookingType = False
        Catch ex As Exception
            IsLoadBookingType = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub


    Private Sub btn_ChangeIndent_Click(sender As Object, e As EventArgs) Handles btn_ChangeIndent.Click
        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
            If clsCommon.myLen(txtVendorNo.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Customer and then click on Change Indent.", Me.Text)
                txtVendorNo.Focus()
                Exit Sub
            End If
            Dim STRSQL As String = ""
            'If ShowBookingTypeDropDownonDairyBookingCustomer = True AndAlso clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Customer_category,'') from tspl_customer_master where cust_code='" & clsCommon.myCstr(txtVendorNo.Value) & "' ")), "Others") = CompairStringResult.Equal Then
            '    STRSQL = "select TOP 1 TSPL_BOOKING_MATSER.Document_No from TSPL_BOOKING_DETAIL left join TSPL_BOOKING_MATSER ON TSPL_BOOKING_MATSER.Document_No=TSPL_BOOKING_DETAIL.Document_No left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.cust_code=TSPL_BOOKING_DETAIL.cust_code where TSPL_BOOKING_MATSER.From_Screen_Code ='BOOK-DS-CU' and TSPL_BOOKING_MATSER.location_code ='" & txtLocation.Value & "' and TSPL_BOOKING_DETAIL.Cust_Code='" & clsCommon.myCstr(txtVendorNo.Value) & "' AND convert(date,TSPL_BOOKING_MATSER.Document_Date ,103)=convert(date,'" & txtDate.Value & "',103) and TSPL_CUSTOMER_MASTER.customer_category<>'Others' order by TSPL_BOOKING_MATSER.Document_Date desc"
            'Else
            STRSQL = "select TOP 1 TSPL_BOOKING_MATSER.Document_No from TSPL_BOOKING_DETAIL left join TSPL_BOOKING_MATSER ON TSPL_BOOKING_MATSER.Document_No=TSPL_BOOKING_DETAIL.Document_No left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.cust_code=TSPL_BOOKING_DETAIL.cust_code where TSPL_BOOKING_MATSER.From_Screen_Code ='BOOK-DS-CU' and TSPL_BOOKING_MATSER.location_code ='" & txtLocation.Value & "' and TSPL_BOOKING_DETAIL.Cust_Code='" & clsCommon.myCstr(txtVendorNo.Value) & "' AND convert(date,TSPL_BOOKING_MATSER.Document_Date ,103)=convert(date,'" & txtDate.Value & "',103) and TSPL_CUSTOMER_MASTER.customer_category<>'Others' order by TSPL_BOOKING_MATSER.Document_Date desc"
            'End If

            Dim TempBookingNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(STRSQL))
            If clsCommon.myLen(TempBookingNo) > 0 Then
                LoadData(TempBookingNo, NavigatorType.Current)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "Booking Not found for Date [" + clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") + "]", Me.Text)
            End If
        End If
    End Sub

    'Private Sub txtVendorNo_Leave(sender As Object, e As EventArgs) Handles txtVendorNo.Leave
    '    If clsCommon.myLen(txtVendorNo.Value) > 0 Then
    '        cmbBookingType.Focus()
    '    Else
    '        txtVendorNo.Focus()
    '    End If
    'End Sub
    Private Sub txtShipToLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtShipToLocation._MYValidating
        If clsCommon.myLen(txtLocation.Value) = 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Location first", Me.Text)
            txtLocation.Focus()
            Exit Sub
        End If
        If clsCommon.myLen(txtVendorNo.Value) = 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Customer first", Me.Text)
            txtVendorNo.Focus()
            Exit Sub
        End If


        Dim qry As String = " select distinct TSPL_SHIP_TO_LOCATION.Ship_To_Code as [Code],TSPL_SHIP_TO_LOCATION.Ship_To_Desc as [Description],TSPL_SHIP_TO_LOCATION.Ship_To_Type_Code as[Customer Code] ,TSPL_SHIP_TO_LOCATION.Ship_To_Type_Desc  as [Customer Discription], replace(case when ISNULL (TSPL_CUSTOMER_MASTER.Add1,'')='' then '' else TSPL_CUSTOMER_MASTER.add1 +',' end + case when ISNULL (TSPL_CUSTOMER_MASTER.Add2,'')='' then '' else TSPL_CUSTOMER_MASTER.add2 +',' end + case when ISNULL (TSPL_CUSTOMER_MASTER.Add3,'')='' then '' else TSPL_CUSTOMER_MASTER.add3 +',' end  ,',,',',') as [Customer Address] ,replace(case when ISNULL (TSPL_SHIP_TO_LOCATION.Add1,'')='' then '' else TSPL_SHIP_TO_LOCATION.add1 +',' end + case when ISNULL (TSPL_SHIP_TO_LOCATION.Add2,'')='' then '' else TSPL_SHIP_TO_LOCATION.add2 +',' end + case when ISNULL (TSPL_SHIP_TO_LOCATION.Add3,'')='' then '' else TSPL_SHIP_TO_LOCATION.add3 +',' end + case when ISNULL (TSPL_SHIP_TO_LOCATION.Add4,'')='' then '' else TSPL_SHIP_TO_LOCATION.add4 +',' end ,',,',',') as [Ship to Address],TSPL_SHIP_TO_LOCATION.CST_No as [CST NO],TSPL_SHIP_TO_LOCATION.Tin_No as [TIN No]  from TSPL_SHIP_TO_LOCATION left outer join TSPL_SHIP_TO_LOCATION_LOCATIONS on TSPL_SHIP_TO_LOCATION.Ship_To_Code=TSPL_SHIP_TO_LOCATION_LOCATIONS.Ship_To_Code left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SHIP_TO_LOCATION.Ship_To_Type_Code "
        txtShipToLocation.Value = clsCommon.ShowSelectForm("DbS-ShipShindrlter", qry, "Code", "Ship_To_Type_Code='" & clsCommon.myCstr(txtVendorNo.Value) & "' and (TSPL_SHIP_TO_LOCATION_LOCATIONS.Loc_Code ='" & txtLocation.Value & "' or  TSPL_SHIP_TO_LOCATION.Loc_Code='" & txtShipToLocation.Value & "' )", txtShipToLocation.Value, "Code", isButtonClicked)
        lblShipToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Ship_To_Desc from TSPL_SHIP_TO_LOCATION where Ship_To_Code='" + txtShipToLocation.Value + "'"))

    End Sub


End Class
