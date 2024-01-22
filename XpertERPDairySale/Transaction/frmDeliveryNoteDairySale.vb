' '' '' ''Created By priti for ticket BM00000003094
''updation by richa agarwal against ticket no BM00000004370
Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Net
Public Class frmDeliveryNoteDairySale
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim strOldVehicleNo As String = ""
    Dim dblCustOutstandingAmt As Double = 0
    Dim AllowWo_Outstanding As Boolean
    Dim Qry As String
    Public isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Private strExcise As Boolean
    Dim isCellValueChangedOpen As Boolean = False
    Const ReportID As String = "DeliveryFSItemGrid"
    Private AllowDelQtygreaterthanBookingQty As Boolean = False
    Private AllowRateEditable As Boolean = False
    Private AllowMRPEditable As Boolean = False
    Const colLineNo As String = "COLLNO"
    Const colICode As String = "COLICODE"
    Const colIName As String = "COLINAME"
    Const colIHSN As String = "colIHSN"
    Const colQty As String = "COLQTY"
    Const colAmt As String = "COLAMT"
    Const colUnit As String = "COLUNIT"
    Const colOrgUnit As String = "COLORGUNIT"
    Const colRate As String = "COLRate"
    Const colMRP As String = "ColMRP"
    Const colConvF As String = "colConvF"
    Const colPriceDateColumn As String = "pricedate"
    Const colPriceCOde As String = "colPriceCOde"
    Const colOrgCost As String = "colOrgCost"
    Const colbookingNo As String = "colbookingNo"
    Const colBookQty As String = "colBookQty"
    Const colBalanceQty As String = "colBalanceQty"
    Const colSchemeItem As String = "colSchemeItem"
    Public StrDocNo As String = ""
    Private AllowPrintButton As Boolean = False
#End Region

    Public Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmDeliveryOrderDairy)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag


    End Sub
    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)


        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colICode
        repoICode.Width = 100
        repoICode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item Description"
        repoIName.Name = colIName
        repoIName.Width = 150
        repoIName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIName)

        Dim repoIHSN As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIHSN.FormatString = ""
        repoIHSN.HeaderText = "HSN Code"
        repoIHSN.Name = colIHSN
        repoIHSN.Width = 150
        repoIHSN.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIHSN)

        Dim repoBookQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoBookQty = New GridViewDecimalColumn()
        repoBookQty.FormatString = ""
        repoBookQty.HeaderText = "Booking Qty"
        repoBookQty.Name = colBookQty
        repoBookQty.Width = 80
        repoBookQty.Minimum = 0
        repoBookQty.ReadOnly = True
        repoBookQty.IsVisible = False
        repoBookQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoBookQty)

        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Quantity"
        repoQty.Name = colQty
        repoQty.Width = 80
        repoQty.Minimum = 0
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoQty)

        Dim repoBalQty As GridViewDecimalColumn
        repoBalQty = New GridViewDecimalColumn()
        repoBalQty.FormatString = ""
        repoBalQty.WrapText = True
        repoBalQty.HeaderText = "Balance Quantity"
        repoBalQty.Name = colBalanceQty
        repoBalQty.Width = 80
        repoBalQty.Minimum = 0
        repoBalQty.IsVisible = False
        repoBalQty.ReadOnly = True
        repoBalQty.IsVisible = False
        repoBalQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoBalQty)

        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "UOM"
        repoUnit.Name = colUnit
        repoUnit.Width = 80
        repoUnit.ReadOnly = False
        repoUnit.HeaderImage = My.Resources.search4
        repoUnit.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(repoUnit)

        Dim repoOrgUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoOrgUnit.FormatString = ""
        repoOrgUnit.HeaderText = "ORG UOM"
        repoOrgUnit.Name = colOrgUnit
        repoOrgUnit.Width = 80
        repoOrgUnit.ReadOnly = False
        repoOrgUnit.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoOrgUnit)

        Dim repoRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRate = New GridViewDecimalColumn()
        repoRate.FormatString = ""
        repoRate.HeaderText = "Rate"
        repoRate.Name = colRate
        repoRate.Width = 80
        repoRate.Minimum = 0
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoRate)


        Dim repoMRP As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoMRP.FormatString = ""
        repoMRP.HeaderText = "MRP"
        repoMRP.Name = colMRP
        repoMRP.Width = 80
        repoMRP.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoMRP.ReadOnly = False
        repoMRP.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoMRP)

        Dim repoAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmt = New GridViewDecimalColumn()
        repoAmt.FormatString = ""
        repoAmt.HeaderText = "Amount"
        repoAmt.Name = colAmt
        repoAmt.Width = 80
        repoAmt.Minimum = 0
        repoIName.ReadOnly = True
        repoAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAmt)


        Dim repoConv As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoConv = New GridViewDecimalColumn()
        repoConv.FormatString = ""
        repoConv.HeaderText = "Conv. Factor"
        repoConv.Name = colConvF
        repoConv.Width = 80
        repoConv.Minimum = 0
        repoConv.ReadOnly = False
        repoConv.IsVisible = False
        repoConv.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoConv)


        Dim repoPriceDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoPriceDate.Format = DateTimePickerFormat.Custom
        repoPriceDate.CustomFormat = "dd-MM-yyyy"
        repoPriceDate.HeaderText = "Price Date"
        repoPriceDate.WrapText = True
        repoPriceDate.FormatString = "{0:d}"
        repoPriceDate.Name = colPriceDateColumn
        repoPriceDate.ReadOnly = True
        repoPriceDate.Width = 80
        gv1.MasterTemplate.Columns.Add(repoPriceDate)


        Dim repoPriceCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPriceCode.FormatString = ""
        repoPriceCode.HeaderText = "Price Code"
        repoPriceCode.Name = colPriceCOde
        repoPriceCode.IsVisible = False
        repoPriceCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoPriceCode)


        Dim repoBooking As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBooking.FormatString = ""
        repoBooking.HeaderText = "Booking No"
        repoBooking.Name = colbookingNo
        repoBooking.ReadOnly = True
        repoBooking.Width = 100
        gv1.MasterTemplate.Columns.Add(repoBooking)


        Dim repoOrgRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoOrgRate = New GridViewDecimalColumn()
        repoOrgRate.FormatString = ""
        repoOrgRate.HeaderText = "Org Basic Rate"
        repoOrgRate.Name = colOrgCost
        repoOrgRate.Width = 80
        repoOrgRate.Minimum = 0
        repoOrgRate.ReadOnly = True
        repoOrgRate.IsVisible = False
        repoOrgRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoOrgRate)

        Dim repoQtySchemeItem As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoQtySchemeItem.AllowSort = False
        repoQtySchemeItem.HeaderText = "Scheme Item"
        repoQtySchemeItem.Name = colSchemeItem
        repoQtySchemeItem.ReadOnly = True
        repoQtySchemeItem.Width = 96
        repoQtySchemeItem.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoQtySchemeItem)

        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40


    End Sub
    Public Shared Function GetFreightType() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr = dt.NewRow()
        dr("Code") = "Party"
        dr("Name") = "Party"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "US"
        dr("Name") = "US"
        dt.Rows.Add(dr)
        Return dt
    End Function
    Sub LoadFreightType()
        ddlFreight.DataSource = GetFreightType()
        ddlFreight.ValueMember = "Code"
        ddlFreight.DisplayMember = "Name"
        ddlFreight.Text = "Party"
    End Sub

    Public Shared Function GetStatusType() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr = dt.NewRow()
        dr("Code") = "Open"
        dr("Name") = "Open"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Approved"
        dr("Name") = "Approved"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Pending"
        dr("Name") = "Pending"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Posted"
        dr("Name") = "Posted"
        dt.Rows.Add(dr)

        Return dt
    End Function
    Sub LoadStatusType()
        ddlStatus.DataSource = GetStatusType()
        ddlStatus.ValueMember = "Code"
        ddlStatus.DisplayMember = "Name"
        ddlStatus.Text = "Open"
    End Sub
    Private Sub fndBookingNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndBookingNo._MYValidating
        If clsCommon.myLen(fndLocation.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select Location", Me.Text)
            fndLocation.Focus()
            Exit Sub
        ElseIf clsCommon.myLen(fndCustomerNo.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select Customer", Me.Text)
            fndCustomerNo.Focus()
            Exit Sub
        End If
        FillPriceCode()
        'SelectMRNItems()
        ''Dim qry As String = "SELECT distinct TSPL_BOOKING_MATSER.Document_No as Code, TSPL_BOOKING_MATSER.Document_Date as Date, TSPL_BOOKING_DETAIL.Cust_Code as Customer, TSPL_BOOKING_DETAIL.Loc_Code as Location " & _
        ''    "FROM TSPL_BOOKING_MATSER left outer JOIN TSPL_BOOKING_DETAIL ON TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No"
        ''Dim WhrCls As String = " Cust_Code='" & fndCustomerNo.Value & "' and Loc_Code='" & fndLocation.Value & "' and Booking_Qty > 0"
        ''If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
        ''    WhrCls += "  and  Loc_Code in (" + objCommonVar.strCurrUserLocations + ")"
        ''End If
        ''fndBookingNo.Value = clsCommon.ShowSelectForm("VendorMasteidfndr", qry, "Code", WhrCls, fndBookingNo.Value, "Code", isButtonClicked)
        ''If clsCommon.myLen(fndBookingNo.Value) > 0 Then
        ''    lblBookingDate.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_Date From TSPL_BOOKING_MATSER where Document_No='" + fndBookingNo.Value + "'"))
        ''    FillGrid()
        ''Else
        ''    lblBookingDate.Text = ""
        ''End If
    End Sub

    Private Sub fndCustomerNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndCustomerNo._MYValidating
        Dim strWhrClause As String = " TSPL_CUSTOMER_MASTER.Cust_Code in (select TSPL_BOOKING_DETAIL.Cust_Code from TSPL_BOOKING_DETAIL)"
        fndCustomerNo.Value = clsCustomerMaster.getFinder(strWhrClause, fndCustomerNo.Value, isButtonClicked)
        If clsCommon.myLen(fndCustomerNo.Value) > 0 Then
            lblCustomerName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_name From TSPL_CUSTOMER_MASTER where Cust_Code='" + fndCustomerNo.Value + "'"))
            '' Anubhooti 10-Sep-2014 BM00000003847
            TxtRouteNo.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" SELECT isnull(Route_No,'') As Route_No FROM TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.Cust_Code  ='" + fndCustomerNo.Value + "'"))
            If clsCommon.myLen(TxtRouteNo.Value) > 0 Then
                Qry = "SELECT Route_Desc,vehicle_code,Number,Transporter_Name,Capacity,TSPL_VEHICLE_MASTER.InOut  FROM TSPL_ROUTE_MASTER left outer join " & _
                "TSPL_VEHICLE_MASTER on TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id left outer join " & _
                "TSPL_TRANSPORT_MASTER on TSPL_VEHICLE_MASTER.Transport_Id=TSPL_TRANSPORT_MASTER.Transport_Id where  Route_No  ='" + clsCommon.myCstr(TxtRouteNo.Value) + "' "
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    lblRouteNo.Text = clsCommon.myCstr(dt.Rows(0)("Route_Desc"))
                    txtTransporterName.Text = clsCommon.myCstr(dt.Rows(0)("Transporter_Name"))
                    If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("InOut")), "I") = CompairStringResult.Equal Then
                        txtLorryNo.Value = clsCommon.myCstr(dt.Rows(0)("vehicle_code"))
                        lblVehicleNo.Text = clsCommon.myCstr(dt.Rows(0)("Number"))
                        txtVehicleCapacity.Text = clsCommon.myCstr(dt.Rows(0)("Capacity"))
                    Else
                        txtLorryNo.Value = ""
                        lblVehicleNo.Text = ""
                        txtVehicleCapacity.Text = ""
                    End If
                End If
            Else
                lblRouteNo.Text = ""
                lblVehicleNo.Text = ""
                txtLorryNo.Value = ""
                txtVehicleCapacity.Text = ""
                txtTransporterName.Text = ""
            End If
        Else
            lblCustomerName.Text = ""
        End If
    End Sub

    Sub FillPriceCode()
        Try

            If clsCommon.myLen(fndCustomerNo.Value) = 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select Customer ", Me.Text)
                fndCustomerNo.Focus()
            End If

            Dim qry As String = "select Cust_Code as Code,Customer_Name as Name,TSPL_CUSTOMER_MASTER.add1 +case when len(TSPL_CUSTOMER_MASTER.add2)>0 then ', '+TSPL_CUSTOMER_MASTER.add2 else '' end +case when LEN(isnull(TSPL_CUSTOMER_MASTER.Add3,''))>0 then ', '+isnull(TSPL_CUSTOMER_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER.City_Name)>0 then ', '+TSPL_CITY_MASTER.City_Name else ' ' end + case when len(TSPL_CUSTOMER_MASTER.State )>0 then TSPL_CUSTOMER_MASTER.State else '' end  as Address,TSPL_CUSTOMER_MASTER.Terms_Code as [Term Code] , TSPL_TERMS_MASTER.Terms_Desc as [Term Description] ,Tax_Group as [Tax Group],Tax_Group_Desc as [Tax Group Description],Salesman_Code as [Salesman Code],Salesman_Desc as Salesman  "
            qry += " from TSPL_CUSTOMER_MASTER "
            qry += " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_CUSTOMER_MASTER.City_Code"
            qry += " left outer join TSPL_TDS_STATE_MASTER on TSPL_TDS_STATE_MASTER.State_Code=TSPL_CUSTOMER_MASTER.State"
            qry += " left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=TSPL_CUSTOMER_MASTER.Terms_Code"
            qry += " left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_CUSTOMER_MASTER.Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S'"
            qry += " where 2=2 and TSPL_CUSTOMER_MASTER.Cust_Code ='" + fndCustomerNo.Value + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                lblTaxGrp.Text = clsCommon.myCstr(dt.Rows(0)("Tax Group"))
            Else
                lblTaxGrp.Text = clsCommon.myCstr(dt.Rows(0)("Tax Group"))
            End If

            '' priti change start here

            qry = "SELECT TSPL_LOCATION_MASTER.Excisable,TSPL_LOCATION_MASTER.State, " & _
            "TSPL_LOCATION_MASTER.Sales_Tax_Group as LocalTaxGroup,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc as Local_Tax_GroupName, " & _
            "TSPL_LOCATION_MASTER.Sales_Tax_GroupIS as InterstateTaxGroup,TSPL_TAX_GROUP_MASTERIS.Tax_Group_Desc as Interstate_Tax_GroupName " & _
            "FROM TSPL_LOCATION_MASTER left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_LOCATION_MASTER.Sales_Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S' left outer join TSPL_TAX_GROUP_MASTER as TSPL_TAX_GROUP_MASTERIS on TSPL_TAX_GROUP_MASTERIS.Tax_Group_Code=TSPL_LOCATION_MASTER.Sales_Tax_GroupIS and TSPL_TAX_GROUP_MASTERIS.Tax_Group_Type='S' WHERE TSPL_LOCATION_MASTER.Location_Code = '" + Convert.ToString(fndLocation.Value) + "'"
            Dim dtLocation As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim loc As String = clsCommon.myCstr(dtLocation.Rows(0)("Excisable"))
            Dim strLocState As String = clsCommon.myCstr(dtLocation.Rows(0)("State"))
            If clsCommon.CompairString(loc, "T") = CompairStringResult.Equal Then
                strExcise = True
            Else
                strExcise = False
            End If
            If clsCommon.myLen(fndCustomerNo.Value) > 0 Then
                qry = "select Price_Code,price_CodeNon,State,price_group_code from TSPL_CUSTOMER_MASTER where Cust_Code='" + fndCustomerNo.Value + "'"
                dt = clsDBFuncationality.GetDataTable(qry)

                If clsCommon.CompairString(loc, "T") = CompairStringResult.Equal OrElse clsCommon.CompairString(loc, "Y") = CompairStringResult.Equal Then
                    txtPriceCode.Text = clsCommon.myCstr(dt.Rows(0)("Price_Code"))
                Else
                    txtPriceCode.Text = clsCommon.myCstr(dt.Rows(0)("price_CodeNon"))
                End If
                If clsCommon.myLen(txtPriceCode.Text) = 0 Then
                    txtPriceCode.Text = clsCommon.myCstr(dt.Rows(0)("price_group_code"))
                End If

                If clsCommon.CompairString(clsCommon.myCstr(dtLocation.Rows(0)("State")), clsCommon.myCstr(dt.Rows(0)("State"))) = CompairStringResult.Equal Then
                    lblTaxGrp.Text = clsCommon.myCstr(dtLocation.Rows(0)("LocalTaxGroup"))
                Else
                    lblTaxGrp.Text = clsCommon.myCstr(dtLocation.Rows(0)("InterstateTaxGroup"))

                End If

            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub fndLocation__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndLocation._MYValidating
        Dim strWhrClause As String = " Location_Type='Physical' and CSA_Type='N' and Is_Section='N' and Is_Sub_Location='N' and TSPL_LOCATION_MASTER.Location_Code in (select TSPL_BOOKING_DETAIL.Loc_Code from TSPL_BOOKING_DETAIL)"
        fndLocation.Value = clsLocation.getFinder(strWhrClause, fndLocation.Value, isButtonClicked)
        If clsCommon.myLen(fndLocation.Value) > 0 Then
            lblLocationName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & fndLocation.Value & "'"))
        Else
            lblLocationName.Text = ""
        End If
    End Sub

    Private Sub txtDocNo__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Qry = "select count(*) from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE where Document_No='" + txtDocNo.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry))
            If count = 0 Then
                txtDocNo.MyReadOnly = False
            Else
                txtDocNo.MyReadOnly = True
            End If
            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    '' Anubhooti 16-Mar-2015 (Fetch Alies Name On Finder)
    Private Sub txtDocNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Qry = "SELECT Document_No as Code, CONVERT(varchar(10), Document_Date,103)+' '+ CONVERT(varchar(5), Document_Date,114) as Date, Customer_Code as Customer,Customer_Name as [Customer Name],ISNULL(TSPL_CUSTOMER_MASTER.Alies_Name,'') As [Alies Name], TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Location_Code as Location,TSPL_LOCATION_MASTER.Location_Desc as [Location Desc], Booking_No as [Booking No], Booking_Date as [Booking Date], Freight, Posted,case when TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Status=1 then 'Open' when TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Status=2 then 'Pending' when TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Status=3 then 'Approved' when TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Status=4 then 'Posted' end as Status FROM TSPL_DELIVERY_NOTE_MASTER_FRESHSALE  left outer join TSPL_CUSTOMER_MASTER on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code left outer join TSPL_location_MASTER on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Location_Code=TSPL_location_MASTER.Location_Code "

        Dim whrClas As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas = " TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If

        LoadData(clsCommon.ShowSelectForm("FSDeliDocNo", Qry, "Code", whrClas, txtDocNo.Value, "Code", isButtonClicked), NavigatorType.Current)
    End Sub

    Sub FillGrid()
        Try
            LoadBlankGrid()
            Dim intLine As Integer = 0

            'Qry = "select TSPL_BOOKING_DETAIL.Item_Code,Item_Desc,TSPL_BOOKING_DETAIL.Booking_Qty,2 as Rate,0 as Amount,TSPL_ITEM_MASTER.Unit_Code from TSPL_BOOKING_DETAIL " & _
            '"left outer join TSPL_ITEM_MASTER on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code  " & _
            '"where Document_No='" & fndBookingNo.Value & "' and Loc_Code='" & fndLocation.Value & "' and Cust_Code='" & fndCustomerNo.Value & "'  and Booking_Qty > 0"
            Qry = "select  TSPL_BOOKING_DETAIL.Item_Code,Item_Desc,TSPL_BOOKING_DETAIL.Booking_Qty,isnull(TSPL_ITEM_PRICE_MASTER.Item_Basic_Price,0) as Rate, " & _
            "isnull(TSPL_ITEM_PRICE_MASTER.Item_Basic_Net,0) as MRP,TSPL_BOOKING_DETAIL.Booking_Qty * isnull(TSPL_ITEM_PRICE_MASTER.Item_Baisc_Price,0) as Amount , " & _
            "TSPL_ITEM_MASTER.Unit_Code,TSPL_ITEM_PRICE_MASTER.Price_Code,TSPL_ITEM_PRICE_MASTER.Start_Date,TSPL_ITEM_UOM_DETAIL.Conversion_Factor from TSPL_BOOKING_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code " & _
            "left outer join TSPL_ITEM_PRICE_MASTER  on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_PRICE_MASTER.Item_Code   and " & _
            "TSPL_ITEM_MASTER.Unit_Code=TSPL_ITEM_PRICE_MASTER.UOM and   " & _
            "Start_Date =(SELECT  TSPL_ITEM_PRICE_MASTER.Start_Date AS Start_Date FROM TSPL_ITEM_PRICE_MASTER INNER Join  ( " & _
            "SELECT     Item_Code, UOM, MAX(Start_Date) AS MaxDateTime, max(Item_Basic_Net) as Item_Basic_Net,  Price_Code, Tax_group  FROM " & _
            "TSPL_ITEM_PRICE_MASTER  where Start_Date<='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'   GROUP BY Item_Code, UOM,  Price_Code, Tax_group,Item_Baisc_Price " & _
            ")  AS groupedP  ON TSPL_ITEM_PRICE_MASTER.Item_Code = groupedP.Item_Code AND  TSPL_ITEM_PRICE_MASTER.Start_Date = groupedP.MaxDateTime AND " & _
            "TSPL_ITEM_PRICE_MASTER.UOM = groupedP.UOM AND TSPL_ITEM_PRICE_MASTER.Item_Basic_Net = groupedP.Item_Basic_Net  AND " & _
            "TSPL_ITEM_PRICE_MASTER.Price_Code = groupedP.Price_Code and TSPL_ITEM_PRICE_MASTER.Tax_group = groupedP.Tax_group  " & _
            "where   TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_BOOKING_DETAIL.Item_Code and " & _
            "TSPL_ITEM_PRICE_MASTER.Price_Code='" & txtPriceCode.Text & "'  ) and Price_Code='" & txtPriceCode.Text & "' " & _
            "left  outer join TSPL_ITEM_UOM_DETAIL on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and TSPL_ITEM_MASTER.Unit_Code=TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
            "where Document_No='" & fndBookingNo.Value & "' and Loc_Code='" & fndLocation.Value & "' and Cust_Code='" & fndCustomerNo.Value & "'  and Booking_Qty > 0  order by Item_Code"
            Dim dt As DataTable
            dt = clsDBFuncationality.GetDataTable(Qry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                isInsideLoadData = True
                For Each dr As DataRow In dt.Rows
                    intLine += 1
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = intLine
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = clsCommon.myCstr(dr("Item_Code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = clsCommon.myCstr(dr("Item_Desc"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIHSN).Value = clsItemMaster.GetItemHSNCode(clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value), Nothing)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = clsCommon.myCdbl(dr("Booking_Qty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = clsCommon.myCstr(dr("Unit_Code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = clsCommon.myCdbl(dr("Rate"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colMRP).Value = clsCommon.myCdbl(dr("MRP"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colConvF).Value = clsCommon.myCdbl(dr("Conversion_Factor"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceCOde).Value = clsCommon.myCstr(dr("Price_Code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgCost).Value = clsCommon.myCdbl(dr("Rate"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgUnit).Value = clsCommon.myCstr(dr("Unit_Code"))
                    If dr("Start_Date") IsNot DBNull.Value Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceDateColumn).Value = clsCommon.myCDate(dr("Start_Date"))
                    End If
                Next
                For ii As Integer = 0 To gv1.RowCount - 1
                    UpdateCurrentRow(ii)
                Next
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    'Sub SelectMRNItems()
    '    isInsideLoadData = True
    '    Dim frm As New frmPendingBooking()
    '    frm.VendorCode = fndCustomerNo.Value
    '    frm.LocCode = fndLocation.Value
    '    frm.TaxCode = lblTaxGrp.Text
    '    frm.PriceCode = txtPriceCode.Text
    '    frm.strCurrCode = txtDocNo.Value
    '    frm.StartDate = txtDate.Value

    '    frm.ShowDialog()
    '    LoadBlankGrid()
    '    If frm.ArrReturn IsNot Nothing AndAlso frm.ArrReturn.Count > 0 Then

    '        ' Dim objOrderHead As clsDeliveryNoteFreshSale
    '        If frm.ArrReturn IsNot Nothing AndAlso frm.ArrReturn.Count > 0 Then
    '            If clsCommon.myLen(frm.ArrReturn(0).Document_No) > 0 Then


    '            End If

    '        End If
    '        If gv1.Rows.Count > 0 AndAlso clsCommon.myLen(gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value) <= 0 Then
    '            gv1.Rows.RemoveAt(gv1.Rows.Count - 1)
    '        End If


    '        'Dim mrnno As String

    '        Dim arr As New List(Of String)
    '        For Each obj As clsDeliveryNoteFreshSaleDetail In frm.ArrReturn
    '            'If IsValidItem(obj) Then
    '            gv1.Rows.AddNew()
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colbookingNo).Value = obj.Document_No
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = obj.Item_Code
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = obj.Item_Desc
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colIHSN).Value = clsItemMaster.GetItemHSNCode(obj.Item_Code, Nothing)
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = obj.Rate
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = obj.Unit_code
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colBookQty).Value = obj.Qty
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = obj.Balance_Qty
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colBalanceQty).Value = obj.Balance_Qty
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceCOde).Value = obj.Price_Code
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colMRP).Value = obj.MRP
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgCost).Value = obj.Rate
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgUnit).Value = obj.Unit_code
    '            If obj.Price_Date.HasValue Then
    '                gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceDateColumn).Value = obj.Price_Date
    '            End If
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colConvF).Value = obj.Conv_Factor

    '            'End If
    '        Next
    '    End If

    '    For ii As Integer = 0 To gv1.RowCount - 1
    '        UpdateCurrentRow(ii)
    '    Next

    '    isInsideLoadData = False
    '    UpdateAllTotals()
    '    RefreshReqNo()

    'End Sub
    'Function IsValidItem(ByVal obj As clsBookingEntry)
    '    'If clsCommon.myLen(txtTaxGroup.Value) <= 0 Then
    '    '    txtTaxGroup.Value = obj.SOTax_Group
    '    '    SetTaxDetails()
    '    'End If
    '    ''If Not clsCommon.CompairString(txtTaxGroup.Value, obj.MRNTax_Group) = CompairStringResult.Equal Then
    '    ''    common.clsCommon.MyMessageBoxShow("Item : " + obj.Item_Desc + " not Added Current Tax Group : " + txtTaxGroup.Value + " MRN No: " + obj.MRN_No + "  contain Tax Group :" + obj.MRNTax_Group + Environment.NewLine)
    '    ''    Return False
    '    ''End If
    '    For ii As Integer = 0 To gv1.RowCount - 1
    '        Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
    '        Dim strReqCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colbookingNo).Value)
    '        Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)
    '        Dim dblMRP As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colMRP).Value)
    '        If clsCommon.myLen(strReqCode) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value), "N") = CompairStringResult.Equal AndAlso clsCommon.CompairString(strReqCode, obj.Document_No) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strICode, obj.Item_Code) = CompairStringResult.Equal Then
    '            Dim strMsg As String = "Already exist at row no:" + clsCommon.myCstr(ii + 1) + Environment.NewLine + "Order No : " + obj.Document_No + "  Item : " + obj.Item_Desc + Environment.NewLine + ""
    '            If dblMRP > 0 Then
    '                strMsg = strMsg + Environment.NewLine + "MRP : " + clsCommon.myCstr(dblMRP)
    '            End If
    '            common.clsCommon.MyMessageBoxShow(strMsg)
    '            Return False
    '        End If
    '    Next
    '    Return True
    'End Function
    Sub RefreshReqNo()
        fndBookingNo.Value = ""
        If gv1.Rows.Count > 0 Then
            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim strReqNo As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colbookingNo).Value)
                If clsCommon.myLen(strReqNo) > 0 Then
                    fndBookingNo.Value = clsCommon.myCstr(strReqNo)
                    lblBookingDate.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_Date From TSPL_BOOKING_MATSER where Document_No='" + fndBookingNo.Value + "'"))
                    Exit Sub
                End If
            Next
        End If
    End Sub

    Private Sub gv1_CellFormatting(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.CellFormatting
        Try
            If e.Column Is gv1.Columns(colRate) Then
                If AllowRateEditable Then
                    gv1.CurrentRow.Cells(colRate).ReadOnly = False
                Else
                    gv1.CurrentRow.Cells(colRate).ReadOnly = True
                End If
            ElseIf e.Column Is gv1.Columns(colMRP) Then
                If AllowMRPEditable Then
                    gv1.CurrentRow.Cells(colMRP).ReadOnly = False
                Else
                    gv1.CurrentRow.Cells(colMRP).ReadOnly = True
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Sub gv1_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If Not isInsideLoadData Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(colQty) Then
                        UpdateCurrentRow(gv1.CurrentRow.Index)
                    ElseIf e.Column Is gv1.Columns(colUnit) Then
                        OpenUOMList(False)
                        UpdateCurrentRow(gv1.CurrentRow.Index)
                        UpdateAllTotals()
                    ElseIf e.Column Is gv1.Columns(colRate) Then
                        UpdateCurrentRow(gv1.CurrentRow.Index)
                        UpdateAllTotals()
                    End If
                    isCellValueChangedOpen = False
                End If


            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub
    Sub setBalance()
        UcItemBalance1.ItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
        UcItemBalance1.ItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
        UcItemBalance1.ItemMRP = clsCommon.myCdbl(gv1.CurrentRow.Cells(colMRP).Value)
        UcItemBalance1.LocationCode = fndLocation.Value
        UcItemBalance1.LocationName = lblLocationName.Text
        UcItemBalance1.UOM = clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value)
        UcItemBalance1.TransNo = txtDocNo.Value
        UcItemBalance1.TransDate = txtDate.Value
        UcItemBalance1.ShowSOQty = True
        UcItemBalance1.RefreshData()
    End Sub
    Private Sub gv1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv1.Click
        If gv1.CurrentRow IsNot Nothing Then
            setBalance()
        End If
    End Sub
    Private Sub gv1_CurrentRowChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentRowChangedEventArgs) Handles gv1.CurrentRowChanged
        If gv1.CurrentRow IsNot Nothing AndAlso Not e.CurrentRow.Index < 0 Then
            setBalance()
        End If
    End Sub
    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
        If clsCommon.myLen(strICode) > 0 Then
            'gv1.CurrentRow.Cells(colOrgUnit).Value = clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value)
            Dim qry As String = "select UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
            Dim whrCls As String = "Item_Code='" + strICode + "'"
            gv1.CurrentRow.Cells(colUnit).Value = clsCommon.ShowSelectForm("FSDelUnit", qry, "Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), "Code", isButtonClick)

            Dim dblConvF As Double
            dblConvF = GetConvFactor(gv1.CurrentRow.Cells(colUnit).Value, gv1.CurrentRow.Cells(colICode).Value)
            gv1.CurrentRow.Cells(colConvF).Value = dblConvF

        End If
    End Sub
    Private Function GetConvFactor(ByVal strUnit As String, ByVal strItem As String) As Double
        Dim dblConvF As Double = 0
        dblConvF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from tspl_item_uom_detail where Item_Code='" & strItem & "' and UOM_Code='" & strUnit & "'"))
        Return dblConvF
    End Function
    Private Function GetConvRate(ByVal IntRowNo As Integer) As Double
        Dim strItem As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colICode).Value)
        Dim strOrgUnit As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colOrgUnit).Value)
        Dim strCurrentUnit As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colUnit).Value)
        Dim dblCurrentConvF As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colConvF).Value)
        Dim dblRate As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colRate).Value)
        Dim dblConvRate As Double = 0
        If clsCommon.myLen(strOrgUnit) > 0 Then
            Dim dblOrgConvF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from tspl_item_uom_detail where Item_Code='" & strItem & "' and UOM_Code='" & strOrgUnit & "'"))
            Dim dblStockingUnitConvF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from tspl_item_uom_detail where Item_Code='" & strItem & "' and Stocking_Unit='Y' "))
            dblConvRate = (dblRate / dblOrgConvF) * dblStockingUnitConvF * dblCurrentConvF
        End If
        Return dblConvRate
    End Function
    Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer)
        Try
            Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value)
            Dim dblRate As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colRate).Value)
            Dim dblOrgBasicRate As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colOrgCost).Value)
            Dim dblConvF As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colConvF).Value)
            Dim dblConvBasicRate As Double = 0

            dblRate = GetConvRate(IntRowNo)
            Dim strOrgUnit As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colUnit).Value)
            Dim dblAmt As Double = dblQty * dblRate
            gv1.Rows(IntRowNo).Cells(colAmt).Value = Math.Round(dblAmt, 2)
            gv1.Rows(IntRowNo).Cells(colRate).Value = dblRate
            gv1.Rows(IntRowNo).Cells(colOrgUnit).Value = strOrgUnit

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub UpdateAllTotals()
        Try
            Dim dblTotAmt As Double = 0
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If (clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0) Then
                    dblTotAmt = dblTotAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmt).Value)
                End If
            Next
            If dblTotAmt > 0 Then
                lblTotRAmt1.Text = clsCommon.myFormat(dblTotAmt)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Function CustomerOutstandingAmount(ByVal strCustomer As String, ByVal ChekPostBtn As Boolean) As Boolean
        Try
            Dim dblOutstandingAmt As Double = 0
            Dim dblCreditLimit As Double = 0
            Dim dblSecurityAmount As Double = 0
            Dim dblPendingDeliveryAmt As Double = 0
            Dim dblAmt As Double = 0
            Dim qry As String = "select sum(case when RI=1 then 1 else -1  end *  OutStandingAmt) from ( " & _
            "select SUM(isnull(TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Total_Amt,0) ) as OutStandingAmt , 1 as RI from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE " & _
            "where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.posted=1  and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Customer_Code='" & strCustomer & "' " & _
            " union all " & _
           "select isnull(SUM(isnull(TSPL_RECEIPT_DETAIL.Applied_Amount,0) ),0) as OutStandingAmt ,-1 as RI  from   " & _
           "TSPL_Customer_Invoice_Head left outer join  TSPL_RECEIPT_DETAIL on TSPL_Customer_Invoice_Head.Document_No=TSPL_RECEIPT_DETAIL.Document_No  left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No=TSPL_RECEIPT_DETAIL.Receipt_No " & _
           "where  TSPL_RECEIPT_HEADER.Posted='Y'  and Against_Sale_No <> ''  and Customer_Code='" & strCustomer & "' " & _
           " union all " & _
           "select isnull(SUM(isnull(TSPL_RECEIPT_HEADER.Receipt_Amount,0) ),0) as OutStandingAmt ,-1 as RI  from  TSPL_RECEIPT_HEADER " & _
           "where  TSPL_RECEIPT_HEADER.Posted='Y'   and Receipt_Type='O' and Cust_Code='" & strCustomer & "' " & _
           " union all " & _
           "select isnull(SUM(isnull(TSPL_RECEIPT_HEADER.Receipt_Amount,0) ),0) as OutStandingAmt ,1 as RI  from  TSPL_RECEIPT_HEADER " & _
           "where  TSPL_RECEIPT_HEADER.Posted='Y'   and Receipt_Type='F' and Cust_Code='" & strCustomer & "' " & _
            " union all " & _
           "select isnull(SUM(isnull(TSPL_RECEIPT_HEADER.Receipt_Amount,0) ),0) as OutStandingAmt ,-1 as RI  from  TSPL_RECEIPT_HEADER " & _
           "where  TSPL_RECEIPT_HEADER.Posted='Y'  and Receipt_Type='P'  and SecurityDeposit='N'  and Cust_Code='" & strCustomer & "'" & _
           " union all " & _
           "select  sum(amount) as OutStandingAmt,-1 as RI from TSPL_BANK_GUARANTEE_MASTER where Type='Customer' and vendor_code='" & strCustomer & "' and Bank_Guarantee_Type='RC' " & _
           " union all " & _
           "select  sum(amount) as OutStandingAmt,1 as RI from TSPL_BANK_GUARANTEE_MASTER where Type='Customer' and vendor_code='" & strCustomer & "' and Bank_Guarantee_Type='RT' ) xxx "

            dblOutstandingAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            dblCreditLimit = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Credit_Limit from TSPL_CUSTOMER_MASTER where Cust_Code='" & strCustomer & "'"))
            dblSecurityAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select SUM(Receipt_Amount) from TSPL_RECEIPT_HEADER where Receipt_Type='P' and  SecurityDepositType not in ('C')  and Posted='Y' and Cust_Code='" & strCustomer & "'"))
            dblPendingDeliveryAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum(Total_Amt) from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE where  posted=0 and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No <> '" & txtDocNo.Value & "' and Customer_Code='" & strCustomer & "'"))

            dblAmt = dblCreditLimit + dblSecurityAmount - dblPendingDeliveryAmt - dblOutstandingAmt


            If dblAmt < lblTotRAmt1.Text AndAlso UsLock1.Status = ERPTransactionStatus.Open Then
                Dim dblNewCredtitLimit = dblAmt - lblTotRAmt1.Text
                common.clsCommon.MyMessageBoxShow(Me, "Please send for approval for increasing credit limit " + clsCommon.myCstr(dblNewCredtitLimit))
                Return False
            End If
            If dblAmt < lblTotRAmt1.Text And UsLock1.Status = ERPTransactionStatus.Pending Then
                Dim dblNewCredtitLimit = dblAmt - lblTotRAmt1.Text
                common.clsCommon.MyMessageBoxShow(Me, "Please increase credit limit " + clsCommon.myCstr(dblNewCredtitLimit) + " for customer " + fndCustomerNo.Value)
                Return False
            End If
            If ChekPostBtn = True Then
                Return True
            End If
            'Return True
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function
    Private Sub RadMenuItem1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        If clsCommon.myLen(ReportID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub
    Function GetBalanceBookingQty(ByVal strBookingCode As String, ByVal strICode As String, ByVal strLocNo As String, ByVal strCustNo As String) As Double
        Dim qry As String = "select SUM(qty * RI) as Balance from( " & _
            " select TSPL_BOOKING_DETAIL.Item_Code as ICode,TSPL_BOOKING_DETAIL.Booking_Qty as Qty,1 as RI from TSPL_BOOKING_DETAIL where Document_No='" + strBookingCode + "' and TSPL_BOOKING_DETAIL.Item_Code='" + strICode + "' and Cust_Code='" + strCustNo + "' and TSPL_BOOKING_DETAIL.Loc_Code='" & strLocNo & "' " & _
            " union all " & _
            "select TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Item_Code as ICode,TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Qty as Qty,-1 as RI from " & _
            "TSPL_DELIVERY_NOTE_MASTER_FRESHSALE left outer join TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE on  " & _
            "TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No=TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Document_No " & _
            "where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No='" + strBookingCode + "'   and TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Item_Code='" + strICode + "' and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Customer_Code='" & strCustNo & "' and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Location_Code='" & strLocNo & "' and TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Document_No not in ('" + txtDocNo.Value + "')  " & _
            ")Final"
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
    End Function
    Function AllowToSave() As Boolean
        Try
            UpdateAllTotals()
            If clsCommon.myLen(fndCustomerNo.Value) = 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select Customer", Me.Text)
                fndCustomerNo.Focus()
                Return False
            End If
            If clsCommon.myLen(fndLocation.Value) = 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select Location", Me.Text)
                fndLocation.Focus()
                Return False
            End If
            If clsCommon.myLen(fndBookingNo.Value) = 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select Booking No", Me.Text)
                fndBookingNo.Focus()
                Return False
            End If


            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
                Dim strIName As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colIName).Value)
                Dim strUnit As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)
                Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
                Dim dblMRP As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colMRP).Value)
                If clsCommon.myLen(strICode) > 0 AndAlso dblQty > 0 Then
                    If dblQty > 0 Then
                        If clsCommon.myLen(strUnit) <= 0 Then
                            common.clsCommon.MyMessageBoxShow(Me, "Please enter UOM for " + strIName + ". At Line No" + clsCommon.myCstr(ii + 1))
                            Return False
                        End If
                    End If
                    '' Anubhooti 12-Sep-2014 BM00000003890 (MRP Based on settings)
                    Dim AllowToEnterMRPManually As String
                    If clsCommon.myLen(strICode) > 0 Then
                        If AllowDelQtygreaterthanBookingQty = False Then
                            Dim dblPendingQTy = GetBalanceBookingQty(fndBookingNo.Value, strICode, fndLocation.Value, fndCustomerNo.Value)
                            If dblQty > dblPendingQTy Then
                                common.clsCommon.MyMessageBoxShow(Me, "Entered Quantity Can't be more than Pending Quantity." + Environment.NewLine + "Entered Quantity : " + clsCommon.myCstr(dblQty) + ". Pending Quantity : " + clsCommon.myCstr(dblPendingQTy))
                                gv1.Rows(ii).Cells(colQty).Value = dblPendingQTy
                                Return False
                            End If
                        End If

                        AllowToEnterMRPManually = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowToEnterMRPManually, clsFixedParameterCode.AllowToEnterMRPManually, Nothing))

                        Dim IsMRP As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select isnull(Is_MRP,0) AS  Is_MRP  from tspl_item_master Where Item_Code ='" + strICode + "'"))
                        If IsMRP = 1 Then
                            If dblMRP <= 0 Then
                                If clsCommon.CompairString(AllowToEnterMRPManually, "1") = CompairStringResult.Equal Then
                                    gv1.Columns(colMRP).ReadOnly = False
                                    common.clsCommon.MyMessageBoxShow(Me, "Please enter numeric value MRP for " + strIName + " at line no " + clsCommon.myCstr(ii + 1))
                                    Return False
                                Else
                                    gv1.Columns(colMRP).ReadOnly = True
                                    common.clsCommon.MyMessageBoxShow(Me, "Please make price code for customer '" & clsCommon.myCstr(fndCustomerNo.Value) & "'")
                                    Return False
                                End If
                            End If

                        End If

                    End If
                End If
            Next
            If funvalidatevehicle() = False Then
                Return False
            End If
            Return True
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
    End Function
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData(False)
    End Sub
    Sub SaveData(ByVal ChekPostBtn As Boolean)
        Try
            If (AllowToSave()) Then
                Dim obj As New clsDeliveryNoteFreshSale
                If AllowWo_Outstanding = False Then
                    If CustomerOutstandingAmount(fndCustomerNo.Value, True) = False Then
                        obj.CreditApproval_Reqd = "Y"
                        obj.Status = 2
                    End If
                End If
                obj.Price_code = txtPriceCode.Text
                obj.Credit_Limit = txtCreditLimit.Value
                obj.Document_No = txtDocNo.Value
                obj.Document_Date = txtDate.Value
                'obj.Status = ddlStatus.Text
                obj.Customer_Code = fndCustomerNo.Value
                obj.Location_Code = fndLocation.Value
                obj.Booking_No = fndBookingNo.Value
                obj.Booking_Date = lblBookingDate.Text
                obj.Vehicle_Capacity = clsCommon.myCdbl(txtVehicleCapacity.Text)
                '' Anubhooti 10-Sep-2014 BM00000003847
                obj.Lorry_No = txtLorryNo.Value
                obj.Route_No = TxtRouteNo.Value
                'obj.Road_Permit_No = txtRoadPermitNo.Text
                obj.Transporter_Name = txtTransporterName.Text
                obj.Freight = ddlFreight.Text
                obj.Freight_Amount = clsCommon.myCdbl(txtFreightAmt.Text)
                obj.Comments = txtComment.Text
                obj.OnHold = IIf(chkOnHold.Checked, "Y", "N")
                obj.Short_Close = IIf(chkShortClose.Checked, "Y", "N")
                obj.Total_Amt = lblTotRAmt1.Text
                obj.Arr = New List(Of clsDeliveryNoteFreshSaleDetail)

                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New clsDeliveryNoteFreshSaleDetail()
                    If (clsCommon.myCdbl(grow.Cells(colQty).Value)) > 0 Then
                        objTr.Line_No = clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                        objTr.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                        objTr.Booking_No = clsCommon.myCstr(grow.Cells(colbookingNo).Value)
                        objTr.Unit_code = clsCommon.myCstr(grow.Cells(colUnit).Value)
                        objTr.Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                        objTr.BookQty = clsCommon.myCdbl(grow.Cells(colBookQty).Value)
                        objTr.Balance_Qty = clsCommon.myCdbl(grow.Cells(colBalanceQty).Value)
                        objTr.Rate = clsCommon.myCdbl(grow.Cells(colRate).Value)
                        objTr.Amount = clsCommon.myCdbl(grow.Cells(colAmt).Value)
                        objTr.MRP = clsCommon.myCdbl(grow.Cells(colMRP).Value)
                        objTr.Conv_Factor = clsCommon.myCdbl(grow.Cells(colConvF).Value)
                        objTr.Price_Code = clsCommon.myCstr(grow.Cells(colPriceCOde).Value)
                        objTr.OrgUnit_code = clsCommon.myCstr(grow.Cells(colOrgUnit).Value)
                        If clsCommon.myLen(grow.Cells(colPriceDateColumn).Value) > 0 Then
                            objTr.Price_Date = clsCommon.myCDate(grow.Cells(colPriceDateColumn).Value, "dd-MMM-yyyy")
                        End If
                        If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                            obj.Arr.Add(objTr)
                        End If
                        objTr.OrgRate = clsCommon.myCdbl(grow.Cells(colOrgCost).Value)

                    End If
                Next
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please Fill at list one Item", Me.Text)
                    Return
                End If

                If (obj.SaveData(obj, isNewEntry)) Then
                    UcAttachment1.SaveData(obj.Document_No)
                    If ChekPostBtn = False Then
                        common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    End If
                    LoadData(obj.Document_No, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Sub AddNew()
        LoadBlankGrid()
        BlankAllControls()
        isNewEntry = True
        UcAttachment1.BlankAllControls()
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        txtFreightAmt.Enabled = False
        txtCreditLimit.Enabled = False
        UsLock1.Status = ERPTransactionStatus.Open
        lblVehicleNo.Enabled = True
        btnSave.Enabled = False
    End Sub
    Sub BlankAllControls()
        txtCreditLimit.Text = ""
        UsLock1.Status = ERPTransactionStatus.Open
        txtPriceCode.Text = ""
        lblTaxGrp.Text = ""
        ddlFreight.Text = "Party"
        ddlStatus.Text = "Open"
        ddlStatus.Enabled = False
        txtDate.Value = clsCommon.GETSERVERDATE()
        fndCustomerNo.Value = ""
        fndLocation.Value = ""
        fndBookingNo.Value = ""
        lblBookingDate.Text = ""
        lblCustomerName.Text = ""
        lblLocationName.Text = ""
        txtDocNo.Value = ""
        txtVehicleCapacity.Text = ""
        txtLorryNo.Value = ""
        lblVehicleNo.Text = ""
        TxtRouteNo.Value = ""
        lblRouteNo.Text = ""
        'txtRoadPermitNo.Text = ""
        txtTransporterName.Text = ""
        txtFreightAmt.Text = ""
        txtComment.Text = ""
        lblTotRAmt1.Text = ""
        chkOnHold.Checked = False
        chkShortClose.Checked = False
        txtShipToLocation.Value = ""
        lblShipToLocation.Text = ""

    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try


            BlankAllControls()
            LoadBlankGrid()
            Dim obj As New clsDeliveryNoteFreshSale
            obj = clsDeliveryNoteFreshSale.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then

                btnSave.Enabled = True
                btnPost.Enabled = True
                btnDelete.Enabled = True
                isInsideLoadData = True
                isNewEntry = False
                btnSave.Text = "Update"


                txtDocNo.Value = obj.Document_No
                txtDate.Value = obj.Document_Date
                txtPriceCode.Text = obj.Price_code
                'ddlStatus.Text = obj.Status
                fndCustomerNo.Value = obj.Customer_Code
                lblCustomerName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_name From TSPL_CUSTOMER_MASTER where Cust_Code='" + fndCustomerNo.Value + "'"))
                fndLocation.Value = obj.Location_Code
                lblLocationName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & fndLocation.Value & "'"))
                fndBookingNo.Value = obj.Booking_No
                lblBookingDate.Text = obj.Booking_Date
                txtVehicleCapacity.Text = obj.Vehicle_Capacity
                '' Anubhooti 10-Sep-2014 BM00000003847 (Chenged Manula Veh No to finder)
                txtLorryNo.Value = obj.Lorry_No
                strOldVehicleNo = obj.Lorry_No
                txtShipToLocation.Value = obj.Ship_To_Location
                lblShipToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_SHIP_TO_LOCATION.Ship_To_Desc FROM  TSPL_SHIP_TO_LOCATION WHERE Ship_To_Code  ='" + txtShipToLocation.Value + "'"))
                If clsCommon.myLen(txtLorryNo.Value) > 0 Then
                    lblVehicleNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ISNULL(Description,'') As Description from TSPL_VEHICLE_MASTER where Vehicle_Id='" & clsCommon.myCstr(txtLorryNo.Value) & "'"))
                End If

                TxtRouteNo.Value = obj.Route_No
                If clsCommon.myLen(TxtRouteNo.Value) > 0 Then
                    lblRouteNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ISNULL(Route_Desc,'') As Route_Desc from TSPL_ROUTE_MASTER where Route_No='" & clsCommon.myCstr(TxtRouteNo.Value) & "'"))
                End If
                'txtRoadPermitNo.Text = obj.Road_Permit_No
                txtTransporterName.Text = obj.Transporter_Name
                ddlFreight.Text = obj.Freight
                txtFreightAmt.Text = obj.Freight_Amount
                txtComment.Text = obj.Comments
                chkOnHold.Checked = IIf(obj.OnHold = "Y", True, False)
                chkShortClose.Checked = IIf(obj.Short_Close = "Y", True, False)
                lblTotRAmt1.Text = obj.Total_Amt
                'If clsCommon.CompairString(obj.Approval_Reqd, "Y") = CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.Is_Approved, "Y") = CompairStringResult.Equal And obj.Posted = 0 Then
                '    obj.Status = 3
                'End If
                'If obj.Status = 1 Then
                '    UsLock1.Status = ERPTransactionStatus.Open
                '    txtCreditLimit.Enabled = False
                'ElseIf obj.Status = 2 Then
                '    UsLock1.Status = ERPTransactionStatus.Pending
                '    btnDelete.Enabled = False
                '    btnPost.Enabled = False
                '    txtCreditLimit.Enabled = False
                'ElseIf obj.Status = 3 Then
                '    txtCreditLimit.Enabled = False
                '    UsLock1.Status = ERPTransactionStatus.Approved
                'ElseIf obj.Status = 4 OrElse obj.Posted = 1 Then
                '    btnSave.Enabled = False
                '    btnDelete.Enabled = False
                '    btnPost.Enabled = False
                '    txtCreditLimit.Enabled = False
                '    UsLock1.Status = ERPTransactionStatus.Posted
                'End If
                'If clsCommon.CompairString(obj.CreditApproval_Reqd, "Y") = CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.Is_Credit_Approved, "Y") = CompairStringResult.Equal And obj.Posted = 0 Then
                '    txtCreditLimit.Enabled = True
                'End If
                If obj.Status = 1 Then
                    UsLock1.Status = ERPTransactionStatus.Open
                    btnPost.Enabled = True
                ElseIf obj.Status = 2 Then
                    UsLock1.Status = ERPTransactionStatus.Pending
                    btnDelete.Enabled = False
                    btnPost.Enabled = False
                    clsCommon.MyMessageBoxShow(Me, "Approval is required for this order", Me.Text)
                ElseIf obj.Status = 3 Then
                    UsLock1.Status = ERPTransactionStatus.Approved
                    btnPost.Enabled = True
                ElseIf obj.Status = 4 OrElse obj.Posted = 1 Then
                    btnSave.Enabled = False
                    btnDelete.Enabled = False
                    btnPost.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Posted
                End If

                'If obj.Approvel_Required = 1 And obj.Is_Approved = 0 Then
                '    clsCommon.MyMessageBoxShow("Approval is required for this order")
                '    btnPost.Enabled = False
                '    'Else
                '    '    btnPost.Enabled = True
                'End If
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsDeliveryNoteFreshSaleDetail In obj.Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.Line_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Desc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIHSN).Value = clsItemMaster.GetItemHSNCode(objTr.Item_Code, Nothing)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colbookingNo).Value = objTr.Booking_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBookQty).Value = objTr.BookQty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBalanceQty).Value = objTr.Balance_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.Unit_code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = objTr.Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = objTr.Amount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMRP).Value = objTr.MRP
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colConvF).Value = objTr.Conv_Factor
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceCOde).Value = objTr.Price_Code
                        If objTr.Price_Date.HasValue Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceDateColumn).Value = objTr.Price_Date
                        End If
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgCost).Value = objTr.OrgRate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgUnit).Value = objTr.OrgUnit_code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeItem).Value = objTr.Scheme_Item
                    Next
                End If
                UcAttachment1.LoadData(obj.Document_No)
                CustomerOutstandingAmount(fndCustomerNo.Value, obj.Total_Amt, Nothing, txtDocNo.Value)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try

    End Sub
    Private Function CustomerOutstandingAmount(ByVal strCustomer As String, ByVal dblTotal As Double, ByVal Trans As SqlTransaction, ByVal strDoc As String) As Boolean
        Dim qry As String = String.Empty
        Try
            Dim dblOutstandingAmt As Double = 0
            Dim dblCreditLimit As Double = 0
            Dim dblSecurityAmount As Double = 0
            Dim dblPendingDeliveryAmt As Double = 0
            Dim dblAmt As Double = 0


            qry = "select sum(case when RI=1 then 1 else -1  end *  OutStandingAmt) from ( " & _
            "select SUM(isnull(TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Total_Amt,0) ) as OutStandingAmt , 1 as RI from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE " & _
            "where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.posted=1 and Sampling=0  and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Customer_Code='" & strCustomer & "'  and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No not in ('" & strDoc & "')  " & _
            " union all " & _
           "select isnull(SUM(isnull(TSPL_RECEIPT_DETAIL.Applied_Amount,0) ),0) as OutStandingAmt ,-1 as RI  from   " & _
           "TSPL_Customer_Invoice_Head left outer join  TSPL_RECEIPT_DETAIL on TSPL_Customer_Invoice_Head.Document_No=TSPL_RECEIPT_DETAIL.Document_No  left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No=TSPL_RECEIPT_DETAIL.Receipt_No " & _
           "where  TSPL_RECEIPT_HEADER.Posted='Y'  and Against_Sale_No <> ''  and Customer_Code='" & strCustomer & "' " & _
           " union all " & _
           "select isnull(SUM(isnull(TSPL_RECEIPT_HEADER.Receipt_Amount,0) ),0) as OutStandingAmt ,-1 as RI  from  TSPL_RECEIPT_HEADER " & _
           "where  TSPL_RECEIPT_HEADER.Posted='Y'   and Receipt_Type='O' and Cust_Code='" & strCustomer & "' " & _
           " union all " & _
           "select isnull(SUM(isnull(TSPL_RECEIPT_HEADER.Receipt_Amount,0) ),0) as OutStandingAmt ,1 as RI  from  TSPL_RECEIPT_HEADER " & _
           "where  TSPL_RECEIPT_HEADER.Posted='Y'   and Receipt_Type='F' and Cust_Code='" & strCustomer & "' " & _
            " union all " & _
           "select isnull(SUM(isnull(TSPL_RECEIPT_HEADER.Receipt_Amount,0) ),0) as OutStandingAmt ,-1 as RI  from  TSPL_RECEIPT_HEADER " & _
           "where  TSPL_RECEIPT_HEADER.Posted='Y'  and Receipt_Type='P'  and SecurityDeposit='N'  and Cust_Code='" & strCustomer & "'" & _
           " union all " & _
           "select  sum(amount) as OutStandingAmt,-1 as RI from TSPL_BANK_GUARANTEE_MASTER where Type='Customer' and vendor_code='" & strCustomer & "' and Bank_Guarantee_Type='RC' " & _
           " union all " & _
           "select  sum(amount) as OutStandingAmt,1 as RI from TSPL_BANK_GUARANTEE_MASTER where Type='Customer' and vendor_code='" & strCustomer & "' and Bank_Guarantee_Type='RT' ) xxx "

            dblOutstandingAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, Trans))
            dblCreditLimit = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Credit_Limit from TSPL_CUSTOMER_MASTER where Cust_Code='" & strCustomer & "'", Trans))
            dblSecurityAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select SUM(Receipt_Amount) from TSPL_RECEIPT_HEADER where Receipt_Type='P' and  SecurityDepositType  in ('S')  and Posted='Y' and Cust_Code='" & strCustomer & "'", Trans))
            dblPendingDeliveryAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum(Total_Amt) from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE where  posted=0 and Sampling=0 and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No <> '" & strDoc & "' and Customer_Code='" & strCustomer & "'", Trans))

            dblAmt = dblCreditLimit + dblSecurityAmount - dblPendingDeliveryAmt - dblOutstandingAmt
            dblCustOutstandingAmt = dblAmt
            txtCreditLimit.Value = dblCustOutstandingAmt
            If dblAmt < dblTotal Then
                Dim dblNewCredtitLimit = dblAmt - dblTotal
                Return False

            End If

            'Return True
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        Finally
            qry = Nothing
        End Try
        Return True
    End Function

    Private Sub BtnSend_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSend.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select Document No. First", Me.Text)
                txtDocNo.Focus()
                txtDocNo.Select()
                Return
            End If

            If Not (common.clsCommon.MyMessageBoxShow(Me, "Send E-Mail/SMS Of Respective Delivery No. " + txtDocNo.Value + "" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes) Then
                Return
            End If
            LoadData(txtDocNo.Value, NavigatorType.Current)
            Dim lstUsers As New List(Of String)
            lstUsers.Add(fndCustomerNo.Value)
            'SendEmail(lstUsers, False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub SetMailRight()
        'Dim obj As clsCheckMailSetting = clsCheckMailSetting.CheckMailRight()
        If objCommonVar.IsMailSend Then
            BtnSend.Enabled = True
        Else
            BtnSend.Enabled = False
        End If
    End Sub


    Private Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Sub PostData()
        Try
            Dim msg As String = ""
            Dim qry As String = ""
            Dim dt As DataTable = Nothing

            If (myMessages.postConfirm()) Then

                SaveData(True)
                If AllowWo_Outstanding = False Then
                    If CustomerOutstandingAmount(fndCustomerNo.Value, False) = False Then Exit Sub
                End If
                If (clsDeliveryNoteFreshSale.PostData(MyBase.Form_ID, txtDocNo.Value)) Then
                    msg = "Successfully Posted"
                    common.clsCommon.MyMessageBoxShow(msg)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
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
                If (clsDeliveryNoteFreshSale.DeleteData(txtDocNo.Value)) Then
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

    Private Sub ddlFreight_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlFreight.TextChanged
        If ddlFreight.Text = "US" Then
            txtFreightAmt.Enabled = True
        Else
            txtFreightAmt.Enabled = False
            txtFreightAmt.Text = 0
        End If
    End Sub

    Private Sub frmDeliveryNoteFreshSale_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            SaveData(False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled AndAlso MyBase.isPostFlag Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled AndAlso MyBase.isDeleteFlag Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        End If
    End Sub
    Sub CloseForm()
        Me.Close()
    End Sub
    Private Sub frmDeliveryNoteFreshSale_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AllowDelQtygreaterthanBookingQty = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.AllowDeliveryQtygreaterthanBookingQtyPS & "'")) = 0, False, True)
        AllowWo_Outstanding = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.AllowDispatchOutstandingFS & "'")) = 0, False, True)
        AllowRateEditable = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.IsItemRateEditableOnSales & "'")) = 0, False, True)
        AllowMRPEditable = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.IsItemMRPEditableOnSales & "'")) = 0, False, True)

        SetUserMgmtNew()
        SetMailRight()
        SetMaxlength()
        AddNew()
        LoadStatusType()
        LoadFreightType()
        UcAttachment1.Form_ID = MyBase.Form_ID
        RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Visible
        If clsCommon.myLen(StrDocNo) > 0 Then
            LoadData(StrDocNo, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
        btnUpdateVehicle.Visible = True
        AllowPrintButton = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowDairyDeliveryOrderPrint, clsFixedParameterCode.AllowDairyDeliveryOrderPrint, Nothing)) = 1, True, False)
        btnPrint.Visible = AllowPrintButton

    End Sub

    Private Sub EmailSmsSetting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EmailSmsSetting.Click
        Dim frm As New FrmMailSMSSettingNew2()
        frm.FormId = clsUserMgtCode.frmDeliveryNoteFreshSale
        frm.ShowDialog()
    End Sub

    Private Sub RadMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem4.Click
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
    End Sub

    Sub SetMaxlength()
        txtLorryNo1.MaxLength = 20
        'txtRoadPermitNo.MaxLength = 20
        txtTransporterName.MaxLength = 50
        txtComment.MaxLength = 200
    End Sub
    Private Sub BtnSendForApproval_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSendForApproval.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select Document No. First", Me.Text)
                txtDocNo.Focus()
                txtDocNo.Select()
                Return
            End If

            If Not (common.clsCommon.MyMessageBoxShow(Me, "Send E-Mail/SMS Of Respective Delivery No. " + txtDocNo.Value + "" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes) Then
                Return
            End If
            LoadData(txtDocNo.Value, NavigatorType.Current)

            Dim qry As String = "Select * from TSPL_APPROVAL_LEVEL_SCREEN where 1=1 and Trans_Code='" + MyBase.Form_ID + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            Dim lstUsers As New List(Of String)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    lstUsers.Add(dr("User_Code").ToString())
                Next
            End If

            If lstUsers.Count = 0 Then
                Throw New Exception("No Receiptent Found")
            End If
            'SendEmail(lstUsers, True)

            LoadData(txtDocNo.Value, NavigatorType.Current)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    'Private Sub SendEmail(ByVal lstUsers As List(Of String), ByVal isSendForApproval As Boolean)
    '    Try
    '        Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(clsUserMgtCode.frmDeliveryNoteFreshSale)

    '        If obj Is Nothing Then
    '            clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
    '            Return
    '        End If
    '        If clsCommon.myLen(obj.mailsubjct) <= 0 Then
    '            clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
    '            Return
    '        End If

    '        Dim strContactPerson As String = ""
    '        Dim strSubject As String = obj.mailsubjct.Replace(clsEmailSMSConstants.DeliveryNo, txtDocNo.Value)
    '        strSubject = strSubject.Replace(clsEmailSMSConstants.DeliveryDate, clsCommon.GetPrintDate(txtDate.Text, "dd/MMM/yyyy"))

    '        Dim strbody As String = obj.mailbody.Replace(clsEmailSMSConstants.DeliveryNo, txtDocNo.Value)
    '        strbody = strbody.Replace(clsEmailSMSConstants.DeliveryDate, clsCommon.GetPrintDate(txtDate.Text, "dd/MMM/yyyy"))
    '        strbody = strbody.Replace(clsEmailSMSConstants.CustomerNo, fndCustomerNo.Value)
    '        strbody = strbody.Replace(clsEmailSMSConstants.CustomerName, lblCustomerName.Text)
    '        strbody = strbody.Replace(clsEmailSMSConstants.LocationCode, fndLocation.Value)
    '        strbody = strbody.Replace(clsEmailSMSConstants.LocationName, lblLocationName.Text)
    '        strbody = strbody.Replace(clsEmailSMSConstants.BookingNo, fndBookingNo.Value)
    '        strbody = strbody.Replace(clsEmailSMSConstants.TotalAmount, lblTotRAmt1.Text)
    '        strbody = strbody.Replace(clsEmailSMSConstants.Form_Code, MyBase.Form_ID)

    '        Dim strRptPath As String = ""
    '        '' It will be done after creating report
    '        'If obj.atchmnt = "Y" Then
    '        '    attachQry = GetAttachQry()
    '        '    Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(attachQry)
    '        '    If dt1.Rows.Count > 0 Then
    '        '        strRptPath = NewSalesReportViewer.Emailreport(dt1, "crptShipment", "Shippment Detail")
    '        '    End If
    '        'End If

    '        Dim strPath As String = ""
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

    '            clsMailViaOutlook.SendEmail(strSubject, body, lstReceiptents, Nothing, strPath)
    '        Next


    '        clsCommon.MyMessageBoxShow("E-Mail Send Successfully", Me.Text)
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try

    'End Sub



    Private Sub txtLorryNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtLorryNo._MYValidating
        Dim qry As String = "Select Vehicle_Id,Description,model As Model,Vehicle_Type,Vehicle_Brand,Vehicle_Name ,Location  From TSPL_VEHICLE_MASTER"
        txtLorryNo.Value = clsCommon.ShowSelectForm("FSDelVehicle", qry, "Vehicle_Id", " InOut='I'", txtLorryNo.Value, "Vehicle_Id", isButtonClicked)
        If clsCommon.myLen(txtLorryNo.Value) > 0 Then
            lblVehicleNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Description as Name FROM TSPL_VEHICLE_MASTER Where Vehicle_Id='" + txtLorryNo.Value + "'"))
            txtTransporterName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Transporter_Name from TSPL_TRANSPORT_MASTER where Transport_Id =(Select Transport_Id from TSPL_VEHICLE_MASTER where Vehicle_Id ='" & txtLorryNo.Value & "')"))
        Else
            lblVehicleNo.Text = ""
            txtTransporterName.Text = ""
        End If
    End Sub
    Private Function funvalidatevehicle() As Boolean
        Dim count As Decimal = 0
        Dim segno As String = String.Empty
        Dim strvehiclenum As String = lblVehicleNo.Text
        Dim sql As String = "select segment_code from TSPL_GL_SEGMENT_CODE where segment_code  = '" + Convert.ToString(txtLorryNo.Value) + "' "
        If Not String.IsNullOrEmpty(connectSql.RunScalar(sql)) Then
            sql = "Select Number from TSPL_VEHICLE_MASTER where Vehicle_Id='" + txtLorryNo.Value + "'"
            lblVehicleNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(sql))
            Return True
        Else
            Dim strmessage As String = "This vehicle code doesn't exist" + Environment.NewLine
            strmessage += "Do you want to continue "



            If common.clsCommon.MyMessageBoxShow(Me, strmessage, Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then

                If clsCommon.myLen(lblVehicleNo.Text) <= 0 Then
                    lblVehicleNo.Focus()
                    Throw New Exception("Please Enter Vehicle No")
                End If


                txtLorryNo.Value = clsCommon.incval(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Max(Segment_code) from TSPL_GL_SEGMENT_CODE where Seg_No = 2 ")))
                Dim strSegmentName = clsDBFuncationality.getSingleValue("select Seg_Name from TSPL_GL_SEGMENT where Seg_No=2")
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Try
                    connectSql.RunSpTransaction(trans, "sp_tspl_gl_segmentcode_insert", New SqlParameter("@segno", "2"), New SqlParameter("@segmentname", strSegmentName), New SqlParameter("@segmentcode", txtLorryNo.Value), New SqlParameter("@desc", strvehiclenum), New SqlParameter("@acccode", "NULL"), New SqlParameter("@createdby", objCommonVar.CurrentUserCode), New SqlParameter("@createddate", connectSql.serverDate(trans)), New SqlParameter("@modifiedby", objCommonVar.CurrentUserCode), New SqlParameter("@modifieddate", connectSql.serverDate(trans)), New SqlParameter("@compcode", objCommonVar.CurrentCompanyCode))
                    connectSql.RunSpTransaction(trans, "SP_TSPL_VEHICLE_MASTER_INSERT", New SqlParameter("@Vehicle_Id", txtLorryNo.Value), New SqlParameter("@Model", ""), New SqlParameter("@Number", strvehiclenum), New SqlParameter("@Description", strvehiclenum), New SqlParameter("@Type", "H"), New SqlParameter("@Start_Date", Date.Now.ToString("dd/MM/yyyy")), New SqlParameter("@End_Date", Date.Now.ToString("dd/MM/yyyy")), New SqlParameter("@Vehicle_Reg_No", ""), New SqlParameter("@Vehicle_Chesis_No", ""), New SqlParameter("@Capacity", "0"), New SqlParameter("@Insurance", ""), New SqlParameter("@Pollution_Check", ""), New SqlParameter("@Fitness", Date.Now.ToString("dd/MM/yyyy")), New SqlParameter("@Trans_Type", ""), New SqlParameter("@Road_Tax", ""), New SqlParameter("@Transport_Id", ""), New SqlParameter("@Created_By", objCommonVar.CurrentUserCode), New SqlParameter("@Created_Date", connectSql.serverDate(trans)), New SqlParameter("@Modified_By", objCommonVar.CurrentUserCode), New SqlParameter("@Modified_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", objCommonVar.CurrentCompanyCode))

                    trans.Commit()
                Catch ex As Exception
                    txtLorryNo.Value = ""
                    trans.Rollback()
                    Throw New Exception(ex.Message)
                End Try

                'lblVhicleNo.Text = txtVehicleCode.Text + "-Hired"
                txtLorryNo.Text = txtLorryNo.Value
                Return True
            Else
                txtLorryNo.Value = String.Empty
                txtLorryNo.Text = txtLorryNo.Value
                Return False
            End If
        End If
    End Function
    Private Sub TxtRouteNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles TxtRouteNo._MYValidating
        Dim qry As String = "Select Route_No,Route_Desc AS Description,Type,Employee_Code As [Employee Code],Off_Day AS [Off Day],City_Code As [City Code],District,Category_Code As [Category Code],Length,Employee_Name As [Employee Name],Depot_Id As [Depot Id],Price_Code As [Price Code],Price_Code_Desc As [Price Code Desc],vehicle_code AS [Vehicle Code] From TSPL_ROUTE_MASTER "
        TxtRouteNo.Value = clsCommon.ShowSelectForm("FSDelRoute", qry, "Route_No", "", TxtRouteNo.Value, "Route_No", isButtonClicked)
        If clsCommon.myLen(TxtRouteNo.Value) > 0 Then
            qry = "SELECT Route_Desc,vehicle_code,Number,Transporter_Name,Capacity  FROM TSPL_ROUTE_MASTER left outer join " & _
            "TSPL_VEHICLE_MASTER on TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id left outer join " & _
            "TSPL_TRANSPORT_MASTER on TSPL_VEHICLE_MASTER.Transport_Id=TSPL_TRANSPORT_MASTER.Transport_Id where  Route_No  ='" + clsCommon.myCstr(TxtRouteNo.Value) + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                lblRouteNo.Text = clsCommon.myCstr(dt.Rows(0)("Route_Desc"))
                txtLorryNo.Value = clsCommon.myCstr(dt.Rows(0)("vehicle_code"))
                lblVehicleNo.Text = clsCommon.myCstr(dt.Rows(0)("Number"))
                txtVehicleCapacity.Text = clsCommon.myCstr(dt.Rows(0)("Capacity"))
                txtTransporterName.Text = clsCommon.myCstr(dt.Rows(0)("Transporter_Name"))
            End If
        Else
            lblRouteNo.Text = ""
        End If
    End Sub

    Private Sub RadPageViewPage1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles RadPageViewPage1.Paint

    End Sub

    Private Sub chkShortClose_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkShortClose.ToggleStateChanged

        If chkShortClose.Checked Then
            'If chkShortClose.Checked = True Then
            '    common.clsCommon.MyMessageBoxShow("Please enter Remarks for Closing Delivery Order")
            '    chkShortClose.Checked = False

            '    Return
            'End If
            If Not (common.clsCommon.MyMessageBoxShow(Me, "Want To Close Delivery Order No. " + txtDocNo.Value + "" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes) Then
                Return
            End If

            If clsCommon.myLen(txtDocNo.Value) = 0 Then
                clsCommon.MyMessageBoxShow(Me, "Select Delivery Order No for close.", Me.Text)
                txtDocNo.Focus()
                Return
            End If

            Dim AllowClose As Boolean = False
            For Each grow As GridViewRowInfo In gv1.Rows
                If clsCommon.myLen(grow.Cells(colICode).Value) > 0 And (clsCommon.myCdbl(grow.Cells(colQty).Value)) > 0 And clsCommon.myCstr(grow.Cells(colSchemeItem).Value) = "N" Then

                    Dim DblDispatchqty As Double = 0
                    DblDispatchqty = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum(Qty) as Qty from TSPL_SD_SHIPMENT_DETAIL where Scheme_Item ='N' AND  Delivery_Code='" & txtDocNo.Value & "' and item_code='" & clsCommon.myCstr(grow.Cells(colICode).Value) & "' and unit_code='" & clsCommon.myCstr(grow.Cells(colUnit).Value) & "' group by Delivery_Code,Item_Code,Unit_code"))

                    If DblDispatchqty < clsCommon.myCdbl(grow.Cells(colQty).Value) Then
                        AllowClose = True
                        Exit For
                    End If

                End If
            Next
            If AllowClose = False Then
                clsCommon.MyMessageBoxShow(Me, "Dispatch Already Created against this Delivery Order, Can't be Close.", Me.Text)
                Return
            End If
            'If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
            '    common.clsCommon.MyMessageBoxShow("Please Fill at list one Item")
            '    Return
            'End If

        End If

        If Not chkShortClose.Checked And btnSave.Enabled = False Then
            If Not (common.clsCommon.MyMessageBoxShow(Me, "Want To Open Delivery Order No. " + txtDocNo.Value + "" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes) Then
                Return
            End If
        End If

        Dim qry As String = "select count(*) from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE where Document_No='" + txtDocNo.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "'"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
        If check <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "There is no data found for closed", Me.Text)
            Return
        End If

        Dim obj As New clsDeliveryNoteFreshSale()
        obj.Short_Close = "N"
        If chkShortClose.Checked = True Then
            obj.Short_Close = "Y"
        End If

        If clsDeliveryNoteFreshSale.ClosedData(obj, txtDocNo.Value) Then
            If chkShortClose.Checked Then
                clsCommon.MyMessageBoxShow(Me, "Delivery Order No. " + txtDocNo.Value + " Is Closed Successfully", Me.Text)
                chkShortClose.Checked = True
                btnSave.Enabled = False
                btnPost.Enabled = False
                btnDelete.Enabled = False

            End If
            If Not chkShortClose.Checked And btnSave.Enabled = False Then
                clsCommon.MyMessageBoxShow(Me, "Delivery Order No. " + txtDocNo.Value + " Is Opened Successfully", Me.Text)
                chkShortClose.Checked = False
                If UsLock1.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                ElseIf Not UsLock1.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = True
                    btnPost.Enabled = True
                    btnDelete.Enabled = True

                End If
            End If
        End If

    End Sub

    Private Sub btnApproveCreditLimit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApproveCreditLimit.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select Document No. First", Me.Text)
                txtDocNo.Focus()
                txtDocNo.Select()
                Return
            End If

            If Not (common.clsCommon.MyMessageBoxShow(Me, "Send E-Mail/SMS for approval Of Respective Delivery No. " + txtDocNo.Value + "" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes) Then
                Return
            End If
            LoadData(txtDocNo.Value, NavigatorType.Current)

            Dim qry As String = "Select * from TSPL_CREDIT_LIMIT_APPROVAL_Detail where Module_Name='Fresh Sale' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            Dim lstUsers As New List(Of String)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    lstUsers.Add(dr("User_Code").ToString())
                Next
            End If

            If lstUsers.Count = 0 Then
                Throw New Exception("No Receiptent Found")
            End If
            'SendEmail(lstUsers, True)
            qry = "update TSPL_DELIVERY_NOTE_MASTER_FRESHSALE set CreditApproval_Reqd='Y',Status=2 where Document_No='" & txtDocNo.Value & "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
            LoadData(txtDocNo.Value, NavigatorType.Current)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub EmailSettingCreditApproval_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EmailSettingCreditApproval.Click
        Dim frm As New FrmMailSMSSettingNew2()
        frm.FormId = "DEL-NOT-FSCR"
        frm.ShowDialog()
    End Sub



    Private Sub lblVehicleNo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnUpdateVehicle_Click(sender As Object, e As EventArgs) Handles btnUpdateVehicle.Click
        Try
            'Dim strmessage As String = "This vehicle code doesn't exist" + Environment.NewLine
            If UsLock1.Status <> ERPTransactionStatus.Posted Then
                Dim strmessage As String = "Do you want to change vehicle for all customer ? "

                If common.clsCommon.MyMessageBoxShow(strmessage, Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                    clsDBFuncationality.ExecuteNonQuery("Update TSPL_BOOKING_DETAIL set Vehicle_Code='" & txtLorryNo.Value & "' where Document_No='" & fndBookingNo.Value & "' and Vehicle_Code='" & strOldVehicleNo & "' ")
                    clsDBFuncationality.ExecuteNonQuery("Update TSPL_DELIVERY_NOTE_MASTER_FRESHSALE set Lorry_No='" & txtLorryNo.Value & "' where Booking_No in ('" & fndBookingNo.Value & "') and Lorry_No='" & strOldVehicleNo & "' ")
                Else
                    clsDBFuncationality.ExecuteNonQuery("Update TSPL_BOOKING_DETAIL set Vehicle_Code='" & txtLorryNo.Value & "' where Document_No='" & fndBookingNo.Value & "' and Vehicle_Code='" & strOldVehicleNo & "' and Cust_Code='" & fndCustomerNo.Value & "'")
                    clsDBFuncationality.ExecuteNonQuery("Update TSPL_DELIVERY_NOTE_MASTER_FRESHSALE set Lorry_No='" & txtLorryNo.Value & "' where Booking_No in ('" & fndBookingNo.Value & "') and Customer_Code='" & fndCustomerNo.Value & "' ")

                End If

                clsCommon.MyMessageBoxShow(Me, "Vehicle updated successfully", Me.Text)
            Else
                clsCommon.MyMessageBoxShow(Me, "Document already Posted successfully", Me.Text)
            End If
           
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Delivery Order No not found to Print", Me.Text)
                Return
            End If
            Dim Qry As String = " SELECT  case when  TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Posted =1 then 'Approved' else 'Pending' end as Approved_Status, " & _
                                " TSPL_LOCATION_MASTER.Location_Code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_LOCATION_MASTER.Add1 as Location_Add1,TSPL_LOCATION_MASTER.Add2 as Location_Add2,TSPL_LOCATION_MASTER.Add3 as Location_Add3, " & _
                                " TSPL_LOCATION_MASTER.State as Location_State_Code,TSPL_LOCATION_MASTER.City_Code as Location_City_Code,TSPL_CITY_MASTER.City_Name as Location_City_Name,TSPL_LOCATION_MASTER.Pin_Code as Location_Pin,TSPL_LOCATION_MASTER.GSTNO as Location_GSTIN,TSPL_STATE_MASTER.STATE_CODE as Location_State_Code,TSPL_STATE_MASTER.GST_STATE_Code as Location_GST_State_Code,TSPL_STATE_MASTER.STATE_NAME as Location_State_Name, " & _
                                " TSPL_CUSTOMER_MASTER.Customer_Name , TSPL_CUSTOMER_MASTER.Add1 as Customer_Add1 , TSPL_CUSTOMER_MASTER.Add2 as Customer_Add2, TSPL_CUSTOMER_MASTER.Add3 as Customer_Add3, TSPL_CUSTOMER_MASTER.State as Customer_State,TSPL_CUSTOMER_MASTER.City_Code as Customer_City_Code,TSPL_CITY_MASTER_for_customer.City_Name, TSPL_CUSTOMER_MASTER.PIN_Code as Customer_Pin_code, TSPL_CUSTOMER_MASTER.GSTNO as Customer_GSTIN, TSPL_STATE_MASTER_for_customer.STATE_CODE as Customer_State_Code,TSPL_STATE_MASTER_for_customer.GST_STATE_Code as Customer_GST_State_Code, TSPL_STATE_MASTER_for_customer.STATE_NAME as Customer_State_Name,TSPL_CUSTOMER_MASTER.PAN, " & _
                                " TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No, convert (varchar, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_Date,103) as Document_Date ," & _
                                " TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Status, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Customer_Code, " & _
                                " TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Location_Code, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No,  " & _
                                " convert(varchar, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_Date,103) as  Booking_Date, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Vehicle_Capacity,  " & _
                                " TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Lorry_No,TSPL_VEHICLE_MASTER.Description as Lorry_Desc, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Road_Permit_No,  " & _
                                " TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Transporter_Name, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Freight,  " & _
                                " TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Freight_Amount, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Posted,  " & _
                                " TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Comments,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Total_Amt,  " & _
                                " convert (varchar,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Posting_Date, 103) as Posting_Date,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.OnHold,  " & _
                                " TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Short_Close, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Approval_Reqd, " & _
                                " TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Price_code, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Is_Approved, " & _
                                " TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.CreditApproval_Reqd, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Is_Credit_Approved  " & _
                                " ,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Route_No ,TSPL_ROUTE_MASTER.Route_Desc " & _
                                " ,TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.OrgUnit_code,TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Balance_Qty,TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.BookQty,TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Booking_No,TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Document_No,TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Line_No, TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.HSN_Code   ,TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Unit_code, TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Qty, TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Rate,TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Amount,tspl_item_master.Item_Desc,TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.MRP,TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Conv_Factor ,TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Price_Code, convert (varchar,TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Price_Date,103) as Price_Date,TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.OrgRate " & _
                                " , TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else '' end + case when LEN(TSPL_COMPANY_MASTER.City_Code  )>0 then ', '+TSPL_COMPANY_MASTER.City_Code  else '' end + case when len(TSPL_STATE_MASTER_Company.STATE_NAME  )>0 then ', '+TSPL_STATE_MASTER_Company.STATE_NAME  else '' end + case when LEN(TSPL_COMPANY_MASTER.Pincode)>0 then ' - '+TSPL_COMPANY_MASTER.Pincode else '' end as companyaddress,TSPL_COMPANY_MASTER.CINNo  as Company_cin,TSPL_COMPANY_MASTER.Pan_No as Company_pan,TSPL_COMPANY_MASTER.Logo_Img  " & _
                                " FROM TSPL_DELIVERY_NOTE_MASTER_FRESHSALE " & _
                                " left outer join TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE on TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Document_No =TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No " & _
                                " left outer join tspl_item_master on   TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.item_code=tspl_item_master.item_code  " & _
                                " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.location_code = TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Location_Code " & _
                                " left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE =TSPL_LOCATION_MASTER.State " & _
                                " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code = TSPL_LOCATION_MASTER.City_Code " & _
                                " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Customer_Code " & _
                                " left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_for_customer on TSPL_STATE_MASTER_for_customer.STATE_CODE =TSPL_CUSTOMER_MASTER.State " & _
                                " left outer join TSPL_CITY_MASTER as TSPL_CITY_MASTER_for_customer on TSPL_CITY_MASTER_for_customer.City_Code =  TSPL_CUSTOMER_MASTER.City_Code " & _
                                " left outer join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Lorry_No " & _
                                " left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Route_No " & _
                                " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Comp_Code " & _
                                " left outer join TSPL_STATE_MASTER  as TSPL_STATE_MASTER_Company on TSPL_STATE_MASTER_Company.STATE_CODE =TSPL_COMPANY_MASTER.State  " & _
                                " where 2=2   and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No = '" + txtDocNo.Value + "'  " & _
                                " order by TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No,TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Line_No asc  "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "crptDairyDeliveryOrder", "Delivery Order Dairy Sale", clsCommon.myCDate(dt.Rows(0)("Document_Date")))
                frmCRV = Nothing
                'frmCrystalReportViewer.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, Nothing, "crptDairyDeliveryOrder", "Delivery Order Dairy Sale", clsCommon.myCDate(dt.Rows(0)("Document_Date")), "", "rptCompanyAddress.rpt", clsERPFuncationality.CompanyAddresShowinFooter(), "MMM.rpt", Nothing)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

End Class
