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
Public Class frmGatePassDairySale
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
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
    Const colHCode As String = "CODE"
    Const colHDate As String = "DATE"
    Const colICode As String = "COLICODE"
    Const colIName As String = "COLINAME"
    Const colhsncode As String = "colhsncode"
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
    Public StrDocNo As String = ""
    Const colSellingPrice As String = "colSellingRate"
    Const colFromSchemeCode As String = "colFromSchemeCode"
    Const colSchemeableItem As String = "colSchemeableItem"
    Const colSchemeType As String = "colSchemeType"
    Const colSchemeApplicable As String = "colSchemeApplicable"
    Const colSchmCodeType As String = "SchmCodeType"
    Const colSchemeIUOM As String = "colSchemeIUOM"
    Const colSchemeIQty As String = "colSchemeIQty"
    Const colSchemeICode As String = "colSchemeICode"
    Const colSchemeICodeDes As String = "colSchemeICodeDes"
    Const ColFOC As String = "ColFOC"
    Const colCustCode As String = "colCustCode"
    Const colCustName As String = "colCustName"
    Const colLocCode As String = "colLocCode"
    Const colLocName As String = "colLocName"
    Const colBookingDate As String = "colBookingDate"
    Private CreateLaodinSlipVehicleWise As Boolean = False
    Private RouteCodeNotMandatoryOnLoadINSlip As Boolean = False
    Private VehicleCodeNotMandatoryOnLoadINSlip As Boolean = False
#End Region

    Public Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmGatePassDairy)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag


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

        Dim repoCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCode.FormatString = ""
        repoCode.HeaderText = "Booking No"
        repoCode.Name = colHCode
        repoCode.Width = 170
        repoCode.ReadOnly = True
        repoCode.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoCode)

        Dim repoDate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDate.FormatString = ""
        repoDate.HeaderText = "Date"
        repoDate.Name = colHDate
        repoDate.Width = 70
        repoDate.ReadOnly = True
        repoDate.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoDate)

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

        Dim repoHSNCODE As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoHSNCODE.FormatString = ""
        repoHSNCODE.HeaderText = "HSN CODE"
        repoHSNCODE.Name = colhsncode
        repoHSNCODE.Width = 100
        repoHSNCODE.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoHSNCODE)

        Dim repoBookQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoBookQty = New GridViewDecimalColumn()
        repoBookQty.FormatString = ""
        repoBookQty.HeaderText = "Booking Qty"
        repoBookQty.Name = colBookQty
        repoBookQty.Width = 80
        repoBookQty.Minimum = 0
        repoBookQty.ReadOnly = True
        repoBookQty.IsVisible = True
        repoBookQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoBookQty)

        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "UOM"
        repoUnit.Name = colUnit
        repoUnit.Width = 80
        repoUnit.ReadOnly = True
        repoUnit.HeaderImage = My.Resources.search4
        repoUnit.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(repoUnit)

        Dim repofoc As GridViewDecimalColumn = New GridViewDecimalColumn()
        repofoc.FormatString = ""
        repofoc.HeaderText = "foc"
        repofoc.Name = ColFOC
        repofoc.ReadOnly = True
        repofoc.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repofoc)

        Dim repoScheme As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoScheme.FormatString = ""
        repoScheme.HeaderText = "Scheme Item"
        repoScheme.Name = colSchemeableItem
        repoScheme.Width = 80
        repoScheme.ReadOnly = True
        repoScheme.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoScheme)

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
    Private Sub fndBookingNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndDeliveryNo._MYValidating
        Try
            If clsCommon.myLen(txtLorryNo.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Vechile No.", Me.Text)
                txtLorryNo.Focus()
            Else
                'Dim WhrCls As String = "Document_No not in (select isnull(Delivery_Code,'') from TSPL_SD_SHIPMENT_DETAIL) order by Document_Date"

                'If clsCommon.myLen(txtLorryNo.Value) > 0 Then
                '    WhrCls += " and Lorry_No='" & txtLorryNo.Value & "' "
                'End If
                ''If clsCommon.myLen(fndLocation.Value) > 0 Then
                ''    WhrCls += " and Location_Code='" & fndLocation.Value & "' "
                ''End If
                ''If clsCommon.myLen(fndCustomerNo.Value) > 0 Then
                ''    WhrCls += " and Customer_Code='" & fndCustomerNo.Value & "' "
                ''End If
                ' SelectMRNItems()
                'Dim Qry As String = "select distinct TSPL_BOOKING_MATSER.Document_No as Code,CONVERT(VARCHAR(15),TSPL_BOOKING_MATSER.Document_Date,103) AS Date,TSPL_BOOKING_MATSER.location_code " & _
                '                " from TSPL_BOOKING_MATSER LEFT OUTER JOIN  tspl_booking_detail ON TSPL_BOOKING_MATSER.Document_No=tspl_booking_detail.Document_No " & _
                '                " LEFT OUTER join TSPL_VEHICLE_MASTER ON  tspl_booking_detail.Vehicle_Code=TSPL_VEHICLE_MASTER.Vehicle_Id left outer join TSPL_CUSTOMER_MASTER ON tspl_booking_detail.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code  left outer join tspl_item_master on tspl_booking_detail.item_code=tspl_item_master.item_code "
                'Dim whrClas As String = " tspl_booking_detail.Vehicle_Code='" & txtLorryNo.Value & "' and TSPL_BOOKING_MATSER.Document_No not in(select TSPL_GATEPASS_DETAIL_DAIRYSALE.Delivery_Code from TSPL_GATEPASS_DETAIL_DAIRYSALE " & _
                '" where tspl_booking_detail.Vehicle_Code='" & txtLorryNo.Value & "')"

                Dim Qry As String = "select distinct TSPL_BOOKING_MATSER.Document_No as Code,CONVERT(VARCHAR(15),TSPL_BOOKING_MATSER.Document_Date,103) AS Date,TSPL_BOOKING_MATSER.location_code " &
                               " from TSPL_BOOKING_MATSER LEFT OUTER JOIN  tspl_booking_detail ON TSPL_BOOKING_MATSER.Document_No=tspl_booking_detail.Document_No " &
                               " LEFT OUTER join TSPL_VEHICLE_MASTER ON  tspl_booking_detail.Vehicle_Code=TSPL_VEHICLE_MASTER.Vehicle_Id left outer join TSPL_CUSTOMER_MASTER ON tspl_booking_detail.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code  left outer join tspl_item_master on tspl_booking_detail.item_code=tspl_item_master.item_code "
                Dim whrClas As String = " tspl_booking_detail.foc_item<>1 and tspl_booking_detail.Vehicle_Code='" & txtLorryNo.Value & "' and not exists " &
                " (select TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code,TSPL_GATEPASS_DETAIL_DAIRYSALE.Item_Code,TSPL_GATEPASS_DETAIL_DAIRYSALE.Unit_code,TSPL_GATEPASS_MASTER_DAIRYSALE.Vehicle_Code from TSPL_GATEPASS_MASTER_DAIRYSALE inner join TSPL_GATEPASS_DETAIL_DAIRYSALE ON TSPL_GATEPASS_DETAIL_DAIRYSALE.Document_No= TSPL_GATEPASS_MASTER_DAIRYSALE.Document_No " &
                " where TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code=TSPL_BOOKING_MATSER.Document_No and TSPL_GATEPASS_DETAIL_DAIRYSALE.Item_Code=tspl_booking_detail.Item_Code and TSPL_GATEPASS_DETAIL_DAIRYSALE.Unit_code=tspl_booking_detail.Unit_code and TSPL_GATEPASS_MASTER_DAIRYSALE.Vehicle_Code=tspl_booking_detail.Vehicle_Code) "

                fndDeliveryNo.Value = clsCommon.ShowSelectForm("DSGatePassVehicle", Qry, "Code", whrClas, fndDeliveryNo.Value, "Code", isButtonClicked)
                If clsCommon.myLen(fndDeliveryNo.Value) > 0 Then
                    lblDODate.Text = clsCommon.myCDate(clsDBFuncationality.getSingleValue("Select TSPL_BOOKING_MATSER.Document_Date from TSPL_BOOKING_MATSER where Document_No ='" & fndDeliveryNo.Value & "'"), Nothing)
                    GetBookingDetail(fndDeliveryNo.Value, txtLorryNo.Value)
                Else
                    lblDODate.Text = ""
                    Throw New Exception("No record found.")
                End If

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub GetBookingDetail(ByVal strBookingNo As String, ByVal StrvechilNo As String)
        LoadBlankGrid()
        Dim Qry As String = " select max(code) AS code,max(Date) as Date,ICode,max(IName) as IName,max(HSN_Code) as HSN_Code,sum(Qty) as Qty,Unit ,MAX(Loc_Code) AS Loc_Code,max(Vehicle_Code) as Vehicle_Code,foc_item,Scheme_Item " &
            " from (select TSPL_BOOKING_MATSER.Document_No as Code,CONVERT(VARCHAR(15),TSPL_BOOKING_MATSER.Document_Date,103) AS Date ," &
          " tspl_booking_detail.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name,tspl_booking_detail.Loc_Code,tspl_booking_detail.Item_Code as ICode ," &
          " tspl_item_master.Item_Desc as IName ,tspl_item_master.HSN_Code,tspl_booking_detail.Booking_Qty as Qty,tspl_booking_detail.Unit_code as Unit," &
          " tspl_booking_detail.Item_Rate  as Rate,tspl_booking_detail.Vehicle_Code,tspl_booking_detail.foc_item ,tspl_booking_detail.Scheme_Item " &
          " from TSPL_BOOKING_MATSER LEFT OUTER JOIN  tspl_booking_detail ON TSPL_BOOKING_MATSER.Document_No=tspl_booking_detail.Document_No " &
          " LEFT OUTER join TSPL_VEHICLE_MASTER ON  tspl_booking_detail.Vehicle_Code=TSPL_VEHICLE_MASTER.Vehicle_Id left outer join TSPL_CUSTOMER_MASTER ON tspl_booking_detail.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code  left outer join tspl_item_master on tspl_booking_detail.item_code=tspl_item_master.item_code   "
        Qry += " where tspl_booking_detail.Document_No='" & strBookingNo & "' AND  tspl_booking_detail.Vehicle_Code='" + StrvechilNo + "'"
        Qry += " ) as Final  group by  final.ICode,final.Unit,final.Vehicle_Code ,Final.foc_item,Final.Scheme_Item "
        Dim dtAllData As DataTable = clsDBFuncationality.GetDataTable(Qry)
        If dtAllData Is Nothing OrElse dtAllData.Rows.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "No record found.", Me.Text)
            Me.Close()
        Else
            fndLocation.Value = clsCommon.myCstr(dtAllData.Rows(0)("Loc_Code"))
            lblLocationName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Location_Desc  FROM TSPL_LOCATION_MASTER WHERE Location_Code='" + fndLocation.Value + "'"))
            For Each dr As DataRow In dtAllData.Rows
                gv1.Rows.AddNew()
                gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = clsCommon.myCdbl(gv1.Rows.Count)
                gv1.Rows(gv1.Rows.Count - 1).Cells(colHCode).Value = clsCommon.myCstr(dr("Code"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colHDate).Value = clsCommon.myCstr(dr("Date"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = clsCommon.myCstr(dr("ICode"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = clsCommon.myCstr(dr("IName"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colhsncode).Value = clsCommon.myCstr(dr("HSN_Code"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colBookQty).Value = clsCommon.myCdbl(dr("Qty"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = clsCommon.myCstr(dr("Unit"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(ColFOC).Value = clsCommon.myCstr(dr("foc_item"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeableItem).Value = clsCommon.myCstr(dr("Scheme_Item"))
            Next
        End If
    End Sub
    Sub GetMultiBookingDetail(ByVal strBookingNo As ArrayList, ByVal StrvechilNo As String)
        LoadBlankGrid()
        Dim Qry As String = " select max(code) AS code,max(Date) as Date,ICode,max(IName) as IName,max(HSN_Code) as HSN_Code,sum(Qty) as Qty,Unit ,MAX(Loc_Code) AS Loc_Code,max(Vehicle_Code) as Vehicle_Code,foc_item,Scheme_Item " &
            " from (select TSPL_BOOKING_MATSER.Document_No as Code,CONVERT(VARCHAR(15),TSPL_BOOKING_MATSER.Document_Date,103) AS Date ," &
          " tspl_booking_detail.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name,tspl_booking_detail.Loc_Code,tspl_booking_detail.Item_Code as ICode ," &
          " tspl_item_master.Item_Desc as IName ,tspl_item_master.HSN_Code,tspl_booking_detail.Booking_Qty as Qty,tspl_booking_detail.Unit_code as Unit," &
          " tspl_booking_detail.Item_Rate  as Rate,tspl_booking_detail.Vehicle_Code,tspl_booking_detail.foc_item ,tspl_booking_detail.Scheme_Item " &
          " from TSPL_BOOKING_MATSER LEFT OUTER JOIN  tspl_booking_detail ON TSPL_BOOKING_MATSER.Document_No=tspl_booking_detail.Document_No " &
          " LEFT OUTER join TSPL_VEHICLE_MASTER ON  tspl_booking_detail.Vehicle_Code=TSPL_VEHICLE_MASTER.Vehicle_Id left outer join TSPL_CUSTOMER_MASTER ON tspl_booking_detail.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code  left outer join tspl_item_master on tspl_booking_detail.item_code=tspl_item_master.item_code   "
        Qry += " where tspl_booking_detail.Document_No in (" + clsCommon.GetMulcallString(txtmultiBooking.arrValueMember) + ")"

        If VehicleCodeNotMandatoryOnLoadINSlip = True Then
            If clsCommon.myLen(StrvechilNo) > 0 Then
                Qry += " and tspl_booking_detail.Vehicle_Code='" & StrvechilNo & "' "
            End If
        Else
            Qry += " and tspl_booking_detail.Vehicle_Code='" & StrvechilNo & "' "
        End If

        Qry += " ) as Final  group by  final.ICode,final.Unit,final.Vehicle_Code ,Final.foc_item,Final.Scheme_Item "
        Dim dtAllData As DataTable = clsDBFuncationality.GetDataTable(Qry)
        If dtAllData Is Nothing OrElse dtAllData.Rows.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("No record found.")
            Me.Close()
        Else
            fndLocation.Value = clsCommon.myCstr(dtAllData.Rows(0)("Loc_Code"))
            lblLocationName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Location_Desc  FROM TSPL_LOCATION_MASTER WHERE Location_Code='" + fndLocation.Value + "'"))
            For Each dr As DataRow In dtAllData.Rows
                gv1.Rows.AddNew()
                gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = clsCommon.myCdbl(gv1.Rows.Count)
                gv1.Rows(gv1.Rows.Count - 1).Cells(colHCode).Value = clsCommon.myCstr(dr("Code"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colHDate).Value = clsCommon.myCstr(dr("Date"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = clsCommon.myCstr(dr("ICode"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = clsCommon.myCstr(dr("IName"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colhsncode).Value = clsCommon.myCstr(dr("HSN_Code"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colBookQty).Value = clsCommon.myCdbl(dr("Qty"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = clsCommon.myCstr(dr("Unit"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(ColFOC).Value = clsCommon.myCstr(dr("foc_item"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeableItem).Value = clsCommon.myCstr(dr("Scheme_Item"))
            Next
        End If
    End Sub

    Private Sub fndCustomerNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndCustomerNo._MYValidating
        Dim strWhrClause As String = " TSPL_CUSTOMER_MASTER.Cust_Code in (select TSPL_BOOKING_DETAIL.Cust_Code from TSPL_BOOKING_DETAIL)"
        fndCustomerNo.Value = clsCustomerMaster.getFinder(strWhrClause, fndCustomerNo.Value, isButtonClicked)
        If clsCommon.myLen(fndCustomerNo.Value) > 0 Then
            lblCustomerName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_name From TSPL_CUSTOMER_MASTER where Cust_Code='" + fndCustomerNo.Value + "'"))
            '' Anubhooti 10-Sep-2014 BM00000003847
            TxtRouteNo.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" SELECT isnull(Route_No,'') As Route_No FROM TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.Cust_Code  ='" + fndCustomerNo.Value + "'"))
            If clsCommon.myLen(TxtRouteNo.Value) > 0 Then
                Qry = "SELECT Route_Desc,vehicle_code,Number,Transporter_Name,Capacity,TSPL_VEHICLE_MASTER.InOut  FROM TSPL_ROUTE_MASTER left outer join " &
                "TSPL_VEHICLE_MASTER on TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id left outer join " &
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

            qry = "SELECT TSPL_LOCATION_MASTER.Excisable,TSPL_LOCATION_MASTER.State, " &
            "TSPL_LOCATION_MASTER.Sales_Tax_Group as LocalTaxGroup,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc as Local_Tax_GroupName, " &
            "TSPL_LOCATION_MASTER.Sales_Tax_GroupIS as InterstateTaxGroup,TSPL_TAX_GROUP_MASTERIS.Tax_Group_Desc as Interstate_Tax_GroupName " &
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
        fndLocation.Value = clsCommon.myCstr(clsLocation.getFinder(strWhrClause, fndLocation.Value, isButtonClicked))
        If clsCommon.myLen(fndLocation.Value) > 0 Then
            lblLocationName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & fndLocation.Value & "'"))
        Else
            lblLocationName.Text = ""
        End If
    End Sub

    Private Sub txtDocNo__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Qry = "select count(*) from TSPL_GATEPASS_MASTER_DAIRYSALE where Document_No='" + txtDocNo.Value + "'"
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
        Qry = "SELECT Document_No as Code, CONVERT(varchar(10), Document_Date,103)+' '+ CONVERT(varchar(5), Document_Date,114) as Date, Customer_Code as Customer,Customer_Name as [Customer Name],ISNULL(TSPL_CUSTOMER_MASTER.Alies_Name,'') As [Alies Name], TSPL_GATEPASS_MASTER_DAIRYSALE.Location_Code as Location,TSPL_LOCATION_MASTER.Location_Desc as [Location Desc], Delivery_Code as [Delivery No], TSPL_GATEPASS_MASTER_DAIRYSALE.Against_Demand As [Demand No], Freight, Posted FROM TSPL_GATEPASS_MASTER_DAIRYSALE  left outer join TSPL_CUSTOMER_MASTER on TSPL_GATEPASS_MASTER_DAIRYSALE.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code left outer join TSPL_location_MASTER on TSPL_GATEPASS_MASTER_DAIRYSALE.Location_Code=TSPL_location_MASTER.Location_Code "

        Dim whrClas As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas = " TSPL_GATEPASS_MASTER_DAIRYSALE.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If

        LoadData(clsCommon.ShowSelectForm("DSGatepassDocNo", Qry, "Code", whrClas, txtDocNo.Value, "Code", isButtonClicked), NavigatorType.Current)
    End Sub

    Sub FillGrid()
        Try
            LoadBlankGrid()
            Dim intLine As Integer = 0

            ''Qry = "select TSPL_BOOKING_DETAIL.Item_Code,Item_Desc,TSPL_BOOKING_DETAIL.Booking_Qty,2 as Rate,0 as Amount,TSPL_ITEM_MASTER.Unit_Code from TSPL_BOOKING_DETAIL " & _
            ''"left outer join TSPL_ITEM_MASTER on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code  " & _
            ''"where Document_No='" & fndBookingNo.Value & "' and Loc_Code='" & fndLocation.Value & "' and Cust_Code='" & fndCustomerNo.Value & "'  and Booking_Qty > 0"
            'Qry = "select  TSPL_BOOKING_DETAIL.Item_Code,Item_Desc,TSPL_BOOKING_DETAIL.Booking_Qty,isnull(TSPL_ITEM_PRICE_MASTER.Item_Basic_Price,0) as Rate, " & _
            '"isnull(TSPL_ITEM_PRICE_MASTER.Item_Basic_Net,0) as MRP,TSPL_BOOKING_DETAIL.Booking_Qty * isnull(TSPL_ITEM_PRICE_MASTER.Item_Baisc_Price,0) as Amount , " & _
            '"TSPL_ITEM_MASTER.Unit_Code,TSPL_ITEM_PRICE_MASTER.Price_Code,TSPL_ITEM_PRICE_MASTER.Start_Date,TSPL_ITEM_UOM_DETAIL.Conversion_Factor from TSPL_BOOKING_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code " & _
            '"left outer join TSPL_ITEM_PRICE_MASTER  on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_PRICE_MASTER.Item_Code   and " & _
            '"TSPL_ITEM_MASTER.Unit_Code=TSPL_ITEM_PRICE_MASTER.UOM and   " & _
            '"Start_Date =(SELECT  TSPL_ITEM_PRICE_MASTER.Start_Date AS Start_Date FROM TSPL_ITEM_PRICE_MASTER INNER Join  ( " & _
            '"SELECT     Item_Code, UOM, MAX(Start_Date) AS MaxDateTime, max(Item_Basic_Net) as Item_Basic_Net,  Price_Code, Tax_group  FROM " & _
            '"TSPL_ITEM_PRICE_MASTER  where Start_Date<='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'   GROUP BY Item_Code, UOM,  Price_Code, Tax_group,Item_Baisc_Price " & _
            '")  AS groupedP  ON TSPL_ITEM_PRICE_MASTER.Item_Code = groupedP.Item_Code AND  TSPL_ITEM_PRICE_MASTER.Start_Date = groupedP.MaxDateTime AND " & _
            '"TSPL_ITEM_PRICE_MASTER.UOM = groupedP.UOM AND TSPL_ITEM_PRICE_MASTER.Item_Basic_Net = groupedP.Item_Basic_Net  AND " & _
            '"TSPL_ITEM_PRICE_MASTER.Price_Code = groupedP.Price_Code and TSPL_ITEM_PRICE_MASTER.Tax_group = groupedP.Tax_group  " & _
            '"where   TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_BOOKING_DETAIL.Item_Code and " & _
            '"TSPL_ITEM_PRICE_MASTER.Price_Code='" & txtPriceCode.Text & "'  ) and Price_Code='" & txtPriceCode.Text & "' " & _
            '"left  outer join TSPL_ITEM_UOM_DETAIL on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and TSPL_ITEM_MASTER.Unit_Code=TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
            '"where Document_No='" & fndDeliveryNo.Value & "' and Loc_Code='" & fndLocation.Value & "' and Cust_Code='" & fndCustomerNo.Value & "'  and Booking_Qty > 0  order by Item_Code"

            Qry = "select TSPL_BOOKING_DETAIL.Scheme_Code, TSPL_BOOKING_DETAIL.Item_Code,Item_Desc,TSPL_BOOKING_DETAIL.Booking_Qty,isnull(TSPL_ITEM_PRICE_MASTER.Item_Basic_Price,0) as Rate,isnull(TSPL_ITEM_PRICE_MASTER.Item_Selling_Price,0) as Item_Selling_Price, " &
            "isnull(TSPL_ITEM_PRICE_MASTER.Item_Basic_Net,0) as MRP,TSPL_BOOKING_DETAIL.Booking_Qty * isnull(TSPL_ITEM_PRICE_MASTER.Item_Baisc_Price,0) as Amount , " &
            "TSPL_ITEM_MASTER.Unit_Code,TSPL_ITEM_PRICE_MASTER.Price_Code,TSPL_ITEM_PRICE_MASTER.Start_Date,TSPL_ITEM_UOM_DETAIL.Conversion_Factor from TSPL_BOOKING_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code " &
            "left outer join TSPL_ITEM_PRICE_MASTER  on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_PRICE_MASTER.Item_Code   and " &
            "TSPL_ITEM_MASTER.Unit_Code=TSPL_ITEM_PRICE_MASTER.UOM and   " &
            "Start_Date =(SELECT  TSPL_ITEM_PRICE_MASTER.Start_Date AS Start_Date FROM TSPL_ITEM_PRICE_MASTER INNER Join  ( " &
            "SELECT     Item_Code, UOM, MAX(Start_Date) AS MaxDateTime, max(Item_Basic_Net) as Item_Basic_Net,  Price_Code, Tax_group  FROM " &
            "TSPL_ITEM_PRICE_MASTER  where Start_Date<='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'   GROUP BY Item_Code, UOM,  Price_Code, Tax_group,Item_Baisc_Price " &
            ")  AS groupedP  ON TSPL_ITEM_PRICE_MASTER.Item_Code = groupedP.Item_Code AND  TSPL_ITEM_PRICE_MASTER.Start_Date = groupedP.MaxDateTime AND " &
            "TSPL_ITEM_PRICE_MASTER.UOM = groupedP.UOM AND TSPL_ITEM_PRICE_MASTER.Item_Basic_Net = groupedP.Item_Basic_Net  AND " &
            "TSPL_ITEM_PRICE_MASTER.Price_Code = groupedP.Price_Code and TSPL_ITEM_PRICE_MASTER.Tax_group = groupedP.Tax_group  " &
            "where   TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_BOOKING_DETAIL.Item_Code and " &
            "TSPL_ITEM_PRICE_MASTER.Price_Code='" & txtPriceCode.Text & "'  ) and Price_Code='" & txtPriceCode.Text & "' " &
            "left  outer join TSPL_ITEM_UOM_DETAIL on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and TSPL_ITEM_MASTER.Unit_Code=TSPL_ITEM_UOM_DETAIL.UOM_Code " &
            "where Document_No='" & fndDeliveryNo.Value & "' and Loc_Code='" & fndLocation.Value & "' and Cust_Code='" & fndCustomerNo.Value & "'  and Booking_Qty > 0 isnull(TSPL_BOOKING_DETAIL.Scheme_Item,'N')<>'Y' and isnull(TSPL_BOOKING_DETAIL.FOC_Item,'0')<>'1' order by Item_Code"
            Dim dt As DataTable
            dt = clsDBFuncationality.GetDataTable(Qry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                isInsideLoadData = True
                intLine += 1
                For Each dr As DataRow In dt.Rows
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = intLine
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = clsCommon.myCstr(dr("Item_Code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = clsCommon.myCstr(dr("Item_Desc"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = clsCommon.myCdbl(dr("Booking_Qty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = clsCommon.myCstr(dr("Unit_Code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = clsCommon.myCdbl(dr("Rate"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSellingPrice).Value = clsCommon.myCdbl(dr("Item_Selling_Price"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colMRP).Value = clsCommon.myCdbl(dr("MRP"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colConvF).Value = clsCommon.myCdbl(dr("Conversion_Factor"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceCOde).Value = clsCommon.myCstr(dr("Price_Code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgCost).Value = clsCommon.myCdbl(dr("Rate"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgUnit).Value = clsCommon.myCstr(dr("Unit_Code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colFromSchemeCode).Value = clsCommon.myCstr(dr("Scheme_Code"))
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

    Sub SelectMRNItems()
        isInsideLoadData = True
        Dim frm As New frmPendingDeliveryForGatePass()
        frm.VendorCode = fndCustomerNo.Value
        frm.LocCode = fndLocation.Value
        frm.VehicleCode = txtLorryNo.Value

        frm.ShowDialog()
        LoadBlankGrid()
        Dim obj As clsDeliveryNoteDairySale = Nothing
        If frm.ArrReturn IsNot Nothing AndAlso frm.ArrReturn.Count > 0 Then
            If clsCommon.myLen(frm.ArrReturn(0).Document_No) > 0 Then
                obj = clsDeliveryNoteDairySale.GetData(frm.ArrReturn(0).Document_No, NavigatorType.Current)
                If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0 Then
                    fndCustomerNo.MyReadOnly = True
                    fndLocation.MyReadOnly = True
                    txtLorryNo.MyReadOnly = True
                    txtPriceCode.Text = obj.Price_code
                    fndCustomerNo.Value = obj.Customer_Code
                    lblCustomerName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_name From TSPL_CUSTOMER_MASTER where Cust_Code='" + fndCustomerNo.Value + "'"))
                    fndLocation.Value = obj.Location_Code
                    lblLocationName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & fndLocation.Value & "'"))
                    fndDeliveryNo.Value = obj.Document_No
                    lblDODate.Text = obj.Document_Date
                    txtVehicleCapacity.Text = obj.Vehicle_Capacity
                    '' Anubhooti 10-Sep-2014 BM00000003847 (Chenged Manula Veh No to finder)
                    txtLorryNo.Value = obj.Lorry_No
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

                End If

            End If
            If gv1.Rows.Count > 0 AndAlso clsCommon.myLen(gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value) <= 0 Then
                gv1.Rows.RemoveAt(gv1.Rows.Count - 1)
            End If


            'Dim mrnno As String

            Dim arr As New List(Of String)
            For Each objTr As clsDeliveryNoteDairySaleDetail In frm.ArrReturn
                'If IsValidItem(obj) Then
                gv1.Rows.AddNew()
                gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                gv1.Rows(gv1.Rows.Count - 1).Cells(colHCode).Value = objTr.Booking_No
                gv1.Rows(gv1.Rows.Count - 1).Cells(colHDate).Value = objTr.BookingDate
                gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Desc
                gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = objTr.Rate
                gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.Unit_code
                gv1.Rows(gv1.Rows.Count - 1).Cells(colBookQty).Value = objTr.Qty
                gv1.Rows(gv1.Rows.Count - 1).Cells(colCustCode).Value = objTr.Cust_Code
                gv1.Rows(gv1.Rows.Count - 1).Cells(colCustName).Value = objTr.Cust_Name
                gv1.Rows(gv1.Rows.Count - 1).Cells(colLocCode).Value = objTr.Loc_Code
                gv1.Rows(gv1.Rows.Count - 1).Cells(colLocName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Location_Desc  FROM TSPL_LOCATION_MASTER WHERE Location_Code ='" + objTr.Loc_Code + "' "))
                'gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.Balance_Qty
                'gv1.Rows(gv1.Rows.Count - 1).Cells(colBalanceQty).Value = objTr.Balance_Qty
                'gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceCOde).Value = objTr.Price_Code
                'gv1.Rows(gv1.Rows.Count - 1).Cells(colMRP).Value = objTr.MRP
                'gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgCost).Value = objTr.Rate
                'gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgUnit).Value = objTr.Unit_code
                'If objTr.Price_Date.HasValue Then
                '    gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceDateColumn).Value = objTr.Price_Date
                'End If
                'gv1.Rows(gv1.Rows.Count - 1).Cells(colConvF).Value = objTr.Conv_Factor
                'gv1.Rows(gv1.Rows.Count - 1).Cells(colFromSchemeCode).Value = objTr.Scheme_Code
                ''gv1.Rows(gv1.Rows.Count - 1).Cells(colSchmCodeType).Value = objTr.Scheme_Type
                ''gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeICode).Value = objTr.Scheme_Item_Code
                ''gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeIUOM).Value = objTr.Scheme_Item_UOM
                ''gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeIQty).Value = objTr.Scheme_Qty
                ''gv1.Rows(gv1.Rows.Count - 1).Cells(ColFOC).Value = objTr.FOC_Item

                'gv1.Rows(gv1.Rows.Count - 1).Cells(colSellingPrice).Value = objTr.SellingPrice
                'If gv1.CurrentRow.Cells(colFromSchemeCode).Value <> "" Then
                '    gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeApplicable).Value = "Yes"
                'Else
                '    gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeApplicable).Value = "No"
                'End If
                'findQtyandPromoSchemeCode(False, gv1.CurrentRow.Cells(colFromSchemeCode).Value)

                ''End If
            Next
        End If

        'For ii As Integer = 0 To gv1.RowCount - 1
        '    UpdateCurrentRow(ii)
        'Next

        isInsideLoadData = False
        ' UpdateAllTotals()
        RefreshReqNo()
    End Sub

    Private Sub findQtyandPromoSchemeCode(ByVal isButtonClick As Boolean, Optional ByVal SchemeCode As String = Nothing)
        ' Dim dr1 As DataTable
        'Dim schemeCodeCol As String
        Dim LocCodeCol As String
        Dim LocNameCol As String
        Dim intRow As Integer
        Dim strOrderCode As String = ""
        LocCodeCol = fndLocation.Value
        LocNameCol = lblLocationName.Text
        Try
            Dim Index As Integer = gv1.CurrentRow.Index

            If gv1.CurrentRow.Cells(colFromSchemeCode).Value = "" Then

                Dim QryScheme As String = "select isnull(Scheme_Code,'') as Scheme_Code from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE inner join TSPL_DELIVERY_NOTE_Detail_FRESHSALE on " &
                        " TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No = TSPL_DELIVERY_NOTE_Detail_FRESHSALE.Document_No" &
                        " where TSPL_DELIVERY_NOTE_Detail_FRESHSALE.Document_No='" + fndDeliveryNo.Value + "' and TSPL_DELIVERY_NOTE_Detail_FRESHSALE.Item_Code='" + gv1.CurrentRow.Cells(colICode).Value + "' and TSPL_DELIVERY_NOTE_Detail_FRESHSALE.Unit_code='" + gv1.CurrentRow.Cells(colUnit).Value + "' and isnull(TSPL_DELIVERY_NOTE_Detail_FRESHSALE.Scheme_Item,'N')='N'"
                Dim SchemeCodeValue As String = clsDBFuncationality.getSingleValue(QryScheme)
                SchemeCode = clsCommon.myCstr(SchemeCodeValue)
                If clsCommon.myCstr(SchemeCode) <> "" Then

                    gv1.CurrentRow.Cells(colSchemeApplicable).Value = "Yes"
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeApplicable).Value = "Yes"

                End If
            End If

            If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colSchemeApplicable).Value), "No") = CompairStringResult.Equal OrElse clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value) = 0 Then
                For schemeRow As Integer = gv1.Rows.Count - 1 To 0 Step -1
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(schemeRow).Cells(colSchemeApplicable).Value), "No") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(schemeRow).Cells(ColFOC).Value), 1) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(schemeRow).Cells(colSchemeICode).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)) = CompairStringResult.Equal Then
                        gv1.Rows.RemoveAt(schemeRow)
                    End If
                Next

                gv1.Rows(Index).Cells(colFromSchemeCode).Value = Nothing
                gv1.Rows(Index).Cells(colSchmCodeType).Value = Nothing
                gv1.Rows(Index).Cells(colSchemeICode).Value = Nothing
                gv1.Rows(Index).Cells(colSchemeIQty).Value = Nothing
                gv1.Rows(Index).Cells(colSchemeIUOM).Value = Nothing
                gv1.Rows(Index).Cells(colSchemeApplicable).Value = "No"


                gv1.Rows(Index).Cells(ColFOC).Value = 0

                RefreshSerialNo()
            End If
            If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(Index).Cells(colSchmCodeType).Value), "None") <> CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(Index).Cells(colSchmCodeType).Value), "") <> CompairStringResult.Equal Then

                If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colSchemeApplicable).Value), "Yes") = CompairStringResult.Equal Then


                    For schemeRow As Integer = gv1.Rows.Count - 1 To 0 Step -1
                        If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(schemeRow).Cells(colSchemeApplicable).Value), "No") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(schemeRow).Cells(ColFOC).Value), 1) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(schemeRow).Cells(colSchemeICode).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)) = CompairStringResult.Equal Then
                            gv1.Rows.RemoveAt(schemeRow)
                        End If
                    Next

                    Dim objD As clsSchemeApplyOnDairy = Nothing

                    objD = clsSchemeApplyOnDairy.GetSchemeData(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colQty).Value), fndLocation.Value, clsCommon.myCstr(gv1.CurrentRow.Cells(colSchmCodeType).Value), clsCommon.myCstr(SchemeCode))
                    'If objD.Arr.Count = 0 AndAlso clsCommon.myLen(gv1.CurrentRow.Cells(colUnitALter).Value) > 0 Then
                    '    objD = clsSchemeApplyOnDairy.GetSchemeData(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colUnitALter).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colQty).Value), txtVendorNo.Value, clsCommon.myCstr(gv1.CurrentRow.Cells(colSchmCodeType).Value))
                    'End If
                    If objD IsNot Nothing AndAlso objD.Arr.Count > 0 Then
                        For Each objtr As clsSchemeApplyOnDairy In objD.Arr
                            If clsCommon.myLen(LocCodeCol) = 0 Then
                                LocCodeCol = fndLocation.Value
                                LocNameCol = lblLocationName.Text
                            End If
                            'Dim dblSchemeItemActualQTy As Double = clsItemLocationDetails.getBalance(objtr.Schm_Icode, LocCodeCol, txtDocNo.Value, txtDate.Value, Nothing, objtr.Schm_IUnit_Rate, Nothing)
                            '--------------update free itemcode in main item row------------------
                            gv1.Rows(Index).Cells(colFromSchemeCode).Value = objtr.Schm_Code
                            gv1.Rows(Index).Cells(colSchmCodeType).Value = objtr.schm_Type
                            gv1.Rows(Index).Cells(colSchemeICode).Value = objtr.Schm_Icode
                            gv1.Rows(Index).Cells(colSchemeIQty).Value = objtr.Schm_Qty
                            gv1.Rows(Index).Cells(colSchemeIUOM).Value = objtr.Schm_Item_Uom
                            gv1.Rows(Index).Cells(ColFOC).Value = 0
                            gv1.Rows(Index).Cells(colSchemeApplicable).Value = "Yes"
                            Dim MainItemCode = gv1.Rows(Index).Cells(colICode).Value
                            Dim MainItemUnit = gv1.Rows(Index).Cells(colUnit).Value
                            Dim MainItemQty As Double = gv1.Rows(Index).Cells(colQty).Value
                            Dim MainSaleOrderCode As String = gv1.Rows(Index).Cells(colbookingNo).Value
                            '-------------------------------------------------------------

                            '-------------------------------------------------------------

                            gv1.Rows.AddNew()

                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = RowTypeItem
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = intRow + 1
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objtr.Schm_Icode
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objtr.Schm_Iname
                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationCode).Value = LocCodeCol
                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationName).Value = LocNameCol
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceCOde).Value = ""

                            gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objtr.Schm_Item_Uom
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = "0"
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colMRP).Value = 0
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = 0
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objtr.Schm_Qty
                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colItemWeight).Value = 0
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeApplicable).Value = "No"
                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeItem).Value = "Yes"
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colFromSchemeCode).Value = objtr.Schm_Code
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColFOC).Value = 1
                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colAbatementPer).Value = 0
                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colActualCost).Value = 0
                            'gv1.Rows(gv1.Rows.Count - 1).Cells(ColActualBalQty).Value = Math.Round(clsItemLocationDetails.getBalance(clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value), txtBillToLocation.Value, txtDocNo.Value, txtDate.Value, Nothing, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnitRate).Value), 0), 2)

                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSchmCodeType).Value = objtr.schm_Type
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeICode).Value = MainItemCode
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeIQty).Value = MainItemQty
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeIUOM).Value = MainItemUnit
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colbookingNo).Value = MainSaleOrderCode
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColFOC).Value = 1
                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colICodeGrp).Value = clsDBFuncationality.getSingleValue("select CSA_TYPE from TSPL_ITEM_MASTER where Item_Code='" & gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value & "' ")

                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeApplicable).ReadOnly = True
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).ReadOnly = True
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSchmCodeType).ReadOnly = True

                            'Dim dblConvF As Double = clsItemMaster.GetConvertionFactor(gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value, gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitRate).Value, Nothing)
                            'gv1.Rows(intRow).Cells(colConvF).Value = dblConvF
                            'gv1.Rows(intRow).Cells(colTotItemWt).Value = dblConvF * clsCommon.myCdbl(gv1.Rows(intRow).Cells(colItemWeight).Value) * clsCommon.myCdbl(gv1.Rows(intRow).Cells(colQty).Value)
                            'Dim qry As String = ""
                            'Dim Weight_UOM As String = ""
                            'Dim SKU_VALUE As Decimal = 0

                            'qry = "select Item_Code,Weight_UOM,Weight_Value from TSPL_ITEM_MASTER where Item_Code='" & gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value & "'"
                            'Dim dtWt As New DataTable()
                            'dtWt = clsDBFuncationality.GetDataTable(qry)
                            'If dtWt.Rows.Count > 0 Then
                            '    SKU_VALUE = clsCommon.myCdbl(dtWt.Rows(0).Item("Weight_Value"))
                            '    Weight_UOM = clsCommon.myCstr(dtWt.Rows(0).Item("Weight_UOM"))
                            'End If

                            Qry = " Select RowNo, Item_Price_ID, XXXE.Item_Code, UOM, Start_Date, Item_Basic_Price,Item_Basic_Net,Price_Code from ( " &
                            "Select ROW_NUMBER() OVER (Partition By TSPL_ITEM_PRICE_MASTER.Item_Code ORDER BY TSPL_ITEM_PRICE_MASTER.Item_Code,  " &
                            "Start_Date Desc) as RowNo, Item_Price_ID, TSPL_ITEM_PRICE_MASTER.Item_Code, UOM, Start_Date,  " &
                            "Item_Basic_Price,Item_Basic_Net,Price_Code from TSPL_ITEM_PRICE_MASTER  left  outer join  " &
                            "TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and  " &
                            "TSPL_ITEM_PRICE_MASTER.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code   where  Start_Date<='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  and  " &
                            "TSPL_ITEM_PRICE_MASTER.Price_Code='" & txtPriceCode.Text & "' and UOM='" & objtr.Schm_Item_Uom & "' and TSPL_ITEM_PRICE_MASTER.item_code='" & objtr.Schm_Icode & "' " &
                            ") XXXE WHERE RowNo=1  "

                            Dim dt As New DataTable()
                            dt = clsDBFuncationality.GetDataTable(Qry)
                            If dt.Rows.Count > 0 Then
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = clsCommon.myCdbl(dt.Rows(0).Item("Item_Basic_Price"))
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colMRP).Value = clsCommon.myCdbl(dt.Rows(0).Item("Item_Basic_Net"))
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceDateColumn).Value = clsCommon.myCstr(dt.Rows(0).Item("Start_Date"))
                            End If
                            'If clsCommon.myLen(Weight_UOM) <= 0 Then
                            '    clsCommon.MyMessageBoxShow("Weight UOM not defined for Item  " & gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value & "")
                            '    Exit Sub
                            'End If

                            gv1.Rows.Move(gv1.Rows.Count - 1, Index + 1)


                            'End If
                        Next
                    Else
                        gv1.Rows(Index).Cells(colFromSchemeCode).Value = Nothing
                        gv1.Rows(Index).Cells(colSchmCodeType).Value = Nothing
                        gv1.Rows(Index).Cells(colSchemeICode).Value = Nothing
                        gv1.Rows(Index).Cells(colSchemeIQty).Value = Nothing
                        gv1.Rows(Index).Cells(colSchemeIUOM).Value = Nothing
                        gv1.Rows(Index).Cells(ColFOC).Value = 0
                        gv1.Rows(Index).Cells(colSchemeApplicable).Value = "No"
                    End If
                End If
            End If

            RefreshSerialNo()
        Catch ex As Exception
            myMessages.myExceptions(ex)
            gv1.CurrentRow.Cells(colSchemeApplicable).Value = "No"
            gv1.CurrentRow.Cells(colFromSchemeCode).Value = String.Empty
            Exit Sub
        End Try


    End Sub

    Sub RefreshSerialNo()
        Dim intSerialNo As Integer
        For intCount As Integer = 0 To gv1.Rows.Count - 1
            intSerialNo += 1
            gv1.Rows(intCount).Cells(colLineNo).Value = intSerialNo
        Next
    End Sub

    Sub RefreshReqNo()
        fndDeliveryNo.Value = ""
        If gv1.Rows.Count > 0 Then
            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim strReqNo As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colHCode).Value)
                If clsCommon.myLen(strReqNo) > 0 Then
                    fndDeliveryNo.Value = clsCommon.myCstr(strReqNo)
                    lblDODate.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_Date From TSPL_DELIVERY_NOTE_MASTER_FRESHSALE where Document_No='" + fndDeliveryNo.Value + "'"))
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
            'If Not isInsideLoadData Then
            '    If Not isCellValueChangedOpen Then
            '        isCellValueChangedOpen = True
            '        If e.Column Is gv1.Columns(colQty) Then
            '            UpdateCurrentRow(gv1.CurrentRow.Index)
            '            UpdateAllTotals()
            '        ElseIf e.Column Is gv1.Columns(colUnit) Then
            '            OpenUOMList(False)
            '            UpdateCurrentRow(gv1.CurrentRow.Index)
            '            UpdateAllTotals()
            '        ElseIf e.Column Is gv1.Columns(colRate) Then
            '            UpdateCurrentRow(gv1.CurrentRow.Index)
            '            UpdateAllTotals()
            '        End If
            '        'If gv1.CurrentRow.Cells(colFromSchemeCode).Value = "" Then
            '        '    Dim Qry As String = "select Scheme_Code from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE inner join TSPL_DELIVERY_NOTE_Detail_FRESHSALE on " & _
            '        '    " TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No = TSPL_DELIVERY_NOTE_Detail_FRESHSALE.Document_No" & _
            '        '    " where TSPL_DELIVERY_NOTE_Detail_FRESHSALE.Document_No='" + fndDeliveryNo.Value + "' and TSPL_DELIVERY_NOTE_Detail_FRESHSALE.Item_Code='" + gv1.CurrentRow.Cells(colICode).Value + "' and TSPL_DELIVERY_NOTE_Detail_FRESHSALE.Unit_code='" + gv1.CurrentRow.Cells(colUnit).Value + "' and isnull(TSPL_DELIVERY_NOTE_Detail_FRESHSALE.Scheme_Item,'N')='N'"
            '        '    Dim SchemeCode As String = clsDBFuncationality.getSingleValue(Qry)
            '        '    gv1.CurrentRow.Cells(colFromSchemeCode).Value = SchemeCode
            '        'End If
            '        findQtyandPromoSchemeCode(False, gv1.CurrentRow.Cells(colFromSchemeCode).Value)
            '        isCellValueChangedOpen = False
            '    End If


            'End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub

    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
        If clsCommon.myLen(strICode) > 0 Then
            'gv1.CurrentRow.Cells(colOrgUnit).Value = clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value)
            Dim qry As String = "select UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
            Dim whrCls As String = "Item_Code='" + strICode + "'"
            gv1.CurrentRow.Cells(colUnit).Value = clsCommon.ShowSelectForm("DSGatePassUnit", qry, "Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), "Code", isButtonClick)

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

            'dblRate = GetConvRate(IntRowNo)
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
            Dim qry As String = "select sum(case when RI=1 then 1 else -1  end *  OutStandingAmt) from ( " &
            "select SUM(isnull(TSPL_GATEPASS_MASTER_DAIRYSALE.Total_Amt,0) ) as OutStandingAmt , 1 as RI from TSPL_GATEPASS_MASTER_DAIRYSALE " &
            "where TSPL_GATEPASS_MASTER_DAIRYSALE.posted=1  and TSPL_GATEPASS_MASTER_DAIRYSALE.Customer_Code='" & strCustomer & "' " &
            " union all " &
           "select isnull(SUM(isnull(TSPL_RECEIPT_DETAIL.Applied_Amount,0) ),0) as OutStandingAmt ,-1 as RI  from   " &
           "TSPL_Customer_Invoice_Head left outer join  TSPL_RECEIPT_DETAIL on TSPL_Customer_Invoice_Head.Document_No=TSPL_RECEIPT_DETAIL.Document_No  left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No=TSPL_RECEIPT_DETAIL.Receipt_No " &
           "where  TSPL_RECEIPT_HEADER.Posted='Y'  and Against_Sale_No <> ''  and Customer_Code='" & strCustomer & "' " &
           " union all " &
           "select isnull(SUM(isnull(TSPL_RECEIPT_HEADER.Receipt_Amount,0) ),0) as OutStandingAmt ,-1 as RI  from  TSPL_RECEIPT_HEADER " &
           "where  TSPL_RECEIPT_HEADER.Posted='Y'   and Receipt_Type='O' and Cust_Code='" & strCustomer & "' " &
           " union all " &
           "select isnull(SUM(isnull(TSPL_RECEIPT_HEADER.Receipt_Amount,0) ),0) as OutStandingAmt ,1 as RI  from  TSPL_RECEIPT_HEADER " &
           "where  TSPL_RECEIPT_HEADER.Posted='Y'   and Receipt_Type='F' and Cust_Code='" & strCustomer & "' " &
            " union all " &
           "select isnull(SUM(isnull(TSPL_RECEIPT_HEADER.Receipt_Amount,0) ),0) as OutStandingAmt ,-1 as RI  from  TSPL_RECEIPT_HEADER " &
           "where  TSPL_RECEIPT_HEADER.Posted='Y'  and Receipt_Type='P'  and SecurityDeposit='N'  and Cust_Code='" & strCustomer & "'" &
           " union all " &
           "select  sum(amount) as OutStandingAmt,-1 as RI from TSPL_BANK_GUARANTEE_MASTER where Type='Customer' and vendor_code='" & strCustomer & "' and Bank_Guarantee_Type='RC' " &
           " union all " &
           "select  sum(amount) as OutStandingAmt,1 as RI from TSPL_BANK_GUARANTEE_MASTER where Type='Customer' and vendor_code='" & strCustomer & "' and Bank_Guarantee_Type='RT' ) xxx "

            dblOutstandingAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            dblCreditLimit = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Credit_Limit from TSPL_CUSTOMER_MASTER where Cust_Code='" & strCustomer & "'"))
            dblSecurityAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select SUM(Receipt_Amount) from TSPL_RECEIPT_HEADER where Receipt_Type='P' and  SecurityDepositType not in ('C')  and Posted='Y' and Cust_Code='" & strCustomer & "'"))
            dblPendingDeliveryAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum(Total_Amt) from TSPL_GATEPASS_MASTER_DAIRYSALE where  posted=0 and TSPL_GATEPASS_MASTER_DAIRYSALE.Document_No <> '" & txtDocNo.Value & "' and Customer_Code='" & strCustomer & "'"))

            dblAmt = dblCreditLimit + dblSecurityAmount - dblPendingDeliveryAmt - dblOutstandingAmt


            If dblAmt < lblTotRAmt1.Text AndAlso UsLock1.Status = ERPTransactionStatus.Open Then
                Dim dblNewCredtitLimit = dblAmt - lblTotRAmt1.Text
                common.clsCommon.MyMessageBoxShow(Me, "Please send for approval for increasing credit limit " + clsCommon.myCstr(dblNewCredtitLimit), Me.Text)
                Return False
            End If
            If dblAmt < lblTotRAmt1.Text And UsLock1.Status = ERPTransactionStatus.Pending Then
                Dim dblNewCredtitLimit = dblAmt - lblTotRAmt1.Text
                common.clsCommon.MyMessageBoxShow(Me, "Please increase credit limit " + clsCommon.myCstr(dblNewCredtitLimit) + " for customer " + fndCustomerNo.Value, Me.Text)
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
        Dim qry As String = "select sum(qty * ri) as balance from( " &
            " select tspl_delivery_note_detail_freshsale.item_code as icode,tspl_delivery_note_detail_freshsale.qty as qty,1 as ri from tspl_delivery_note_detail_freshsale  where document_no='" + strBookingCode + "' and tspl_delivery_note_detail_freshsale.item_code='" + strICode + "' and isnull(tspl_delivery_note_detail_freshsale.Scheme_Item,'N')<>'Y' and isnull(tspl_delivery_note_detail_freshsale.FOC_Item,'0')<>'1' " &
            " union all " &
            "select tspl_gatepass_detail_dairysale.item_code as icode,tspl_gatepass_detail_dairysale.qty as qty,-1 as ri from tspl_gatepass_master_dairysale left outer join tspl_gatepass_detail_dairysale on  tspl_gatepass_master_dairysale.document_no=tspl_gatepass_detail_dairysale.document_no " &
            "where tspl_gatepass_detail_dairysale.delivery_code='" + strBookingCode + "'   and tspl_gatepass_detail_dairysale.item_code='" + strICode + "'  and tspl_gatepass_detail_dairysale.document_no not in ('" + txtDocNo.Value + "') and isnull(tspl_gatepass_detail_dairysale.Scheme_Item,'N')<>'Y' and isnull(tspl_gatepass_detail_dairysale.FOC_Item,'0')<>'1' " &
            ")final"
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
    End Function
    Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                Dim strCode = clsDBFuncationality.getSingleValue("select top 1 DOCUMENT_CODE from TSPL_SD_SHIPMENT_DETAIL where GatePass_No ='" & txtDocNo.Value & "'")
                If clsCommon.myLen(strCode) > 0 Then
                    clsCommon.MyMessageBoxShow(Me, "GatePass is already used in Dispatch", Me.Text)
                    Return False
                End If
            End If
            '  UpdateAllTotals()
            'If clsCommon.myLen(fndCustomerNo.Value) = 0 Then
            '    clsCommon.MyMessageBoxShow("Please select Customer", Me.Text)
            '    fndCustomerNo.Focus()
            '    Return False
            'End If
            'If VehicleCodeNotMandatoryOnLoadINSlip = False Then
            '    If clsCommon.myLen(txtLorryNo.Value) = 0 Then
            '        clsCommon.MyMessageBoxShow(Me, "Please select Vechile No.", Me.Text)
            '        txtLorryNo.Focus()
            '        Return False
            '    End If
            'End If
            If CreateLaodinSlipVehicleWise Then
                If clsCommon.myLen(txtmultiBooking.arrValueMember) = 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Please select Booking No", Me.Text)
                    fndDeliveryNo.Focus()
                    Return False
                End If

            Else

                'If clsCommon.myLen(fndDeliveryNo.Value) = 0 Then
                '    clsCommon.MyMessageBoxShow(Me, "Please select Booking No", Me.Text)
                '    fndDeliveryNo.Focus()
                '    Return False
                'End If

            End If

            'For ii As Integer = 0 To gv1.Rows.Count - 1
            '    Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
            '    Dim strIName As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colIName).Value)
            '    Dim strUnit As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)
            '    Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colBookQty).Value)
            ' Dim dblMRP As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colMRP).Value)
            'Dim dblFOC As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(ColFOC).Value)
            'gv1.CurrentRow = gv1.Rows(ii)
            'If clsCommon.myLen(strICode) > 0 AndAlso dblQty > 0 Then
            '    If dblQty > 0 Then
            '        If clsCommon.myLen(strUnit) <= 0 Then
            '            common.clsCommon.MyMessageBoxShow("Please enter UOM for " + strIName + ". At Line No" + clsCommon.myCstr(ii + 1), Me.Text)
            '            Return False
            '        End If
            '    End If
            '    '' Anubhooti 12-Sep-2014 BM00000003890 (MRP Based on settings)
            '    'Dim AllowToEnterMRPManually As String
            '    If clsCommon.myLen(strICode) > 0 Then
            '        If AllowDelQtygreaterthanBookingQty = False Then
            '            Dim dblPendingQTy = GetBalanceBookingQty(fndDeliveryNo.Value, strICode, fndLocation.Value, fndCustomerNo.Value)
            '            If dblQty > dblPendingQTy AndAlso dblFOC = 0 Then
            '                common.clsCommon.MyMessageBoxShow("Entered Quantity Can't be more than Pending Quantity." + Environment.NewLine + "Entered Quantity : " + clsCommon.myCstr(dblQty) + ". Pending Quantity : " + clsCommon.myCstr(dblPendingQTy), Me.Text)
            '                gv1.Rows(ii).Cells(colQty).Value = dblPendingQTy

            '                Return False
            '            End If
            '        End If

            '        'AllowToEnterMRPManually = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowToEnterMRPManually, clsFixedParameterCode.AllowToEnterMRPManually, Nothing))

            '        'Dim IsMRP As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select isnull(Is_MRP,0) AS  Is_MRP  from tspl_item_master Where Item_Code ='" + strICode + "'"))
            '        'If IsMRP = 1 Then
            '        '    If dblMRP <= 0 Then
            '        '        If clsCommon.CompairString(AllowToEnterMRPManually, "1") = CompairStringResult.Equal Then
            '        '            gv1.Columns(colMRP).ReadOnly = False
            '        '            common.clsCommon.MyMessageBoxShow("Please enter numeric value MRP for " + strIName + " at line no " + clsCommon.myCstr(ii + 1))
            '        '            Return False
            '        '        Else
            '        '            gv1.Columns(colMRP).ReadOnly = True
            '        '            common.clsCommon.MyMessageBoxShow("Please make price code for customer '" & clsCommon.myCstr(fndCustomerNo.Value) & "'")
            '        '            Return False
            '        '        End If
            '        '    End If

            '        'End If

            '    End If
            'End If
            ' Next
            'If funvalidatevehicle() = False Then
            '    Return False
            'End If
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
            If isNewEntry = False Then
                ' Load_GridData(txtDocNo.Value, Nothing)
            End If
            If (AllowToSave()) Then
                Dim obj As New clsGatePassDairySale

                'obj.Price_code = txtPriceCode.Text
                'obj.Credit_Limit = txtCreditLimit.Value
                obj.Document_No = txtDocNo.Value
                obj.Document_Date = txtDate.Value
                'obj.Customer_Code = fndCustomerNo.Value
                obj.Location_Code = fndLocation.Value
                obj.Delivery_Code = fndDeliveryNo.Value
                obj.ShiftType = ddlPTSShift.Text
                obj.Against_Demand = fndDemand.Value
                If clsCommon.myLen(lblDODate.Text) > 0 Then
                    obj.Delivery_Date = lblDODate.Text
                Else
                    obj.Delivery_Date = clsCommon.GETSERVERDATE()
                End If

                ' obj.Vehicle_Capacity = clsCommon.myCdbl(txtVehicleCapacity.Text)
                '' Anubhooti 10-Sep-2014 BM00000003847
                obj.Vehicle_Code = txtLorryNo.Value
                '   obj.Route_No = TxtRouteNo.Value
                'obj.Road_Permit_No = txtRoadPermitNo.Text
                'obj.Transporter_Name = txtTransporterName.Text
                'obj.Freight = ddlFreight.Text
                'obj.Freight_Amount = clsCommon.myCdbl(txtFreightAmt.Text)
                obj.Comments = txtComment.Text
                'obj.OnHold = IIf(chkOnHold.Checked, "Y", "N")
                'obj.Short_Close = IIf(chkShortClose.Checked, "Y", "N")
                'obj.Total_Amt = lblTotRAmt1.Text
                obj.Route_No = TxtRouteNo.Value

                obj.Arr = New List(Of clsGatePassDairySaleDetail)

                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New clsGatePassDairySaleDetail()
                    If (clsCommon.myCdbl(grow.Cells(colBookQty).Value)) > 0 Then
                        objTr.Line_No = clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                        objTr.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                        objTr.Delivery_Code = clsCommon.myCstr(grow.Cells(colHCode).Value)
                        objTr.Unit_code = clsCommon.myCstr(grow.Cells(colUnit).Value)
                        objTr.Qty = clsCommon.myCdbl(grow.Cells(colBookQty).Value)
                        objTr.Document_No = txtDocNo.Value 'skg
                        objTr.FOC_Item = clsCommon.myCdbl(grow.Cells(ColFOC).Value)
                        objTr.Scheme_Item = clsCommon.myCstr(grow.Cells(colSchemeableItem).Value)
                        'objTr.DOQty = clsCommon.myCdbl(grow.Cells(colBookQty).Value)
                        'objTr.Balance_Qty = clsCommon.myCdbl(grow.Cells(colBalanceQty).Value)
                        ' objTr.Rate = clsCommon.myCdbl(grow.Cells(colRate).Value)
                        'objTr.Amount = clsCommon.myCdbl(grow.Cells(colAmt).Value)
                        'objTr.MRP = clsCommon.myCdbl(grow.Cells(colMRP).Value)
                        'objTr.Conv_Factor = clsCommon.myCdbl(grow.Cells(colConvF).Value)
                        'objTr.Price_Code = clsCommon.myCstr(grow.Cells(colPriceCOde).Value)
                        'objTr.OrgUnit_code = clsCommon.myCstr(grow.Cells(colOrgUnit).Value)
                        'objTr.SellingPrice = clsCommon.myCdbl(grow.Cells(colSellingPrice).Value)

                        'objTr.Scheme_Type = clsCommon.myCstr(grow.Cells(colSchmCodeType).Value)
                        'objTr.Scheme_Code = clsCommon.myCstr(grow.Cells(colFromSchemeCode).Value)
                        'objTr.Scheme_Item_Code = clsCommon.myCstr(grow.Cells(colSchemeICode).Value)
                        'objTr.Scheme_Item_UOM = clsCommon.myCstr(grow.Cells(colSchemeIUOM).Value)
                        'objTr.Scheme_Qty = clsCommon.myCdbl(grow.Cells(colSchemeIQty).Value)
                        'objTr.FOC_Item = clsCommon.myCstr(grow.Cells(ColFOC).Value)
                        'If clsCommon.myCstr(grow.Cells(ColFOC).Value) = "0" Then
                        '    objTr.Scheme_Item = "N"

                        'Else
                        '    objTr.Scheme_Item = "Y"

                        'End If



                        'If clsCommon.myLen(grow.Cells(colPriceDateColumn).Value) > 0 Then
                        '    objTr.Price_Date = clsCommon.myCDate(grow.Cells(colPriceDateColumn).Value, "dd-MMM-yyyy")
                        'End If
                        If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                            obj.Arr.Add(objTr)
                        End If
                        ' objTr.OrgRate = clsCommon.myCdbl(grow.Cells(colOrgCost).Value)

                    End If
                Next
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please Fill at list one Item", Me.Text)
                    Return
                End If
                '==================Added by preeti Gupta Against Ticket no[]============
                obj.ArrInvoice = New List(Of clsGatePassDairyMultiBooking)
                If clsCommon.myLen(txtmultiBooking.arrValueMember) > 0 Then
                    For Each BookingNo As String In txtmultiBooking.arrValueMember
                        Dim objTrTr As New clsGatePassDairyMultiBooking()
                        objTrTr.Document_No = clsCommon.myCstr(txtDocNo.Value)
                        objTrTr.Booking_No = BookingNo
                        If (clsCommon.myLen(objTrTr.Booking_No) > 0) Then
                            obj.ArrInvoice.Add(objTrTr)
                        End If
                    Next
                End If
                '=======================================================================

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
        fndDeliveryNo.Enabled = True
        txtLorryNo.Enabled = True
        TxtRouteNo.Enabled = True
        txtmultiBooking.arrValueMember = Nothing
        If CreateLaodinSlipVehicleWise = True Then
            fndDeliveryNo.Visible = False
            lblDODate.Visible = False
            txtmultiBooking.Visible = True
        Else
            'fndDeliveryNo.Visible = True
            lblDODate.Visible = True
            txtmultiBooking.Visible = False
        End If
    End Sub
    Sub BlankAllControls()
        fndCustomerNo.MyReadOnly = False
        txtLorryNo.MyReadOnly = False
        fndLocation.MyReadOnly = False
        txtCreditLimit.Text = ""
        UsLock1.Status = ERPTransactionStatus.Pending
        txtPriceCode.Text = ""
        lblTaxGrp.Text = ""
        ddlFreight.Text = "Party"
        ddlStatus.Text = "Open"
        ddlStatus.Enabled = False
        txtDate.Value = clsCommon.GETSERVERDATE()
        fndCustomerNo.Value = ""
        fndLocation.Value = ""
        fndDeliveryNo.Value = ""
        fndDemand.Value = ""
        ddlPTSShift.Text = ""
        lblDODate.Text = ""
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
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try


            BlankAllControls()
            LoadBlankGrid()
            Dim obj As New clsGatePassDairySale
            obj = clsGatePassDairySale.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then

                btnSave.Enabled = True
                btnPost.Enabled = True
                btnDelete.Enabled = True
                isInsideLoadData = True
                isNewEntry = False
                btnSave.Text = "Update"
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                End If
                'fndCustomerNo.MyReadOnly = True
                'fndLocation.MyReadOnly = True
                fndDeliveryNo.MyReadOnly = True
                fndDeliveryNo.Enabled = False
                txtLorryNo.MyReadOnly = True
                txtLorryNo.Enabled = False
                TxtRouteNo.Enabled = False
                txtDocNo.Value = obj.Document_No
                txtDate.Value = obj.Document_Date
                ' txtPriceCode.Text = obj.Price_code
                'ddlStatus.Text = obj.Status
                ' fndCustomerNo.Value = obj.Customer_Code
                'lblCustomerName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_name From TSPL_CUSTOMER_MASTER where Cust_Code='" + fndCustomerNo.Value + "'"))
                fndLocation.Value = obj.Location_Code
                lblLocationName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & fndLocation.Value & "'"))
                fndDeliveryNo.Value = obj.Delivery_Code
                fndDemand.Value = obj.Against_Demand
                ddlPTSShift.Text = obj.ShiftType
                lblDODate.Text = obj.Delivery_Date
                'txtVehicleCapacity.Text = obj.Vehicle_Capacity
                '' Anubhooti 10-Sep-2014 BM00000003847 (Chenged Manula Veh No to finder)
                txtLorryNo.Value = obj.Vehicle_Code
                If clsCommon.myLen(txtLorryNo.Value) > 0 Then
                    lblVehicleNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ISNULL(Description,'') As Description from TSPL_VEHICLE_MASTER where Vehicle_Id='" & clsCommon.myCstr(txtLorryNo.Value) & "'"))
                End If

                'TxtRouteNo.Value = obj.Route_No
                'If clsCommon.myLen(TxtRouteNo.Value) > 0 Then
                '    lblRouteNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ISNULL(Route_Desc,'') As Route_Desc from TSPL_ROUTE_MASTER where Route_No='" & clsCommon.myCstr(TxtRouteNo.Value) & "'"))
                'End If
                ''txtRoadPermitNo.Text = obj.Road_Permit_No
                'txtTransporterName.Text = obj.Transporter_Name
                'ddlFreight.Text = obj.Freight
                'txtFreightAmt.Text = obj.Freight_Amount
                txtComment.Text = obj.Comments
                'chkOnHold.Checked = IIf(obj.OnHold = "Y", True, False)
                'chkShortClose.Checked = IIf(obj.Short_Close = "Y", True, False)
                'lblTotRAmt1.Text = obj.Total_Amt
                TxtRouteNo.Value = obj.Route_No
                lblRouteNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Route_Desc from TSPL_ROUTE_MASTER where Route_No = '" + obj.Route_No + "'"))
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsGatePassDairySaleDetail In obj.Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.Line_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Desc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colhsncode).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select HSN_Code from tspl_item_master where item_code='" + objTr.Item_Code + "'"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colHCode).Value = objTr.Delivery_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBookQty).Value = objTr.Qty
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colBalanceQty).Value = objTr.Balance_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.Unit_code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColFOC).Value = objTr.FOC_Item
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeableItem).Value = objTr.Scheme_Item
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.Qty
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = objTr.Rate
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = objTr.Amount
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colMRP).Value = objTr.MRP
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colConvF).Value = objTr.Conv_Factor
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceCOde).Value = objTr.Price_Code

                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colSchmCodeType).Value = objTr.Scheme_Type
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colFromSchemeCode).Value = objTr.Scheme_Code
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeICode).Value = objTr.Scheme_Item_Code
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeIUOM).Value = objTr.Scheme_Item_UOM
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeIQty).Value = objTr.Scheme_Qty
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(ColFOC).Value = objTr.FOC_Item
                        'If clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(ColFOC).Value) = "0" Then
                        '    gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeApplicable).Value = "No"
                        'Else
                        '    gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeApplicable).Value = "Yes"
                        'End If

                        'If objTr.Price_Date.HasValue Then
                        '    gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceDateColumn).Value = objTr.Price_Date
                        'End If
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgCost).Value = objTr.OrgRate
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colOrgUnit).Value = objTr.OrgUnit_code
                    Next
                End If
                '=============Added by preeti Gupta===============
                Dim DocCode As New ArrayList
                If obj.ArrInvoice IsNot Nothing AndAlso obj.ArrInvoice.Count > 0 Then
                    For Each ob As clsGatePassDairyMultiBooking In obj.ArrInvoice
                        DocCode.Add(ob.Booking_No)
                    Next
                    txtmultiBooking.arrValueMember = DocCode
                End If
                '=======================================================
                UcAttachment1.LoadData(obj.Document_No)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try

    End Sub

    Sub Load_GridData(ByVal StrCode As String, ByVal trans As SqlTransaction)
        Dim obj As New clsGatePassDairySale
        LoadBlankGrid()
        obj = clsGatePassDairySale.GetItemDetailData(StrCode, trans)
        If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
            For Each objTr As clsGatePassDairySaleDetail In obj.Arr
                gv1.Rows.AddNew()
                gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.Line_No
                gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Desc
                gv1.Rows(gv1.Rows.Count - 1).Cells(colhsncode).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select HSN_Code from tspl_item_master where item_code='" + objTr.Item_Code + "'"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colHCode).Value = objTr.Delivery_Code
                gv1.Rows(gv1.Rows.Count - 1).Cells(colBookQty).Value = objTr.Qty
                gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.Unit_code
                gv1.Rows(gv1.Rows.Count - 1).Cells(ColFOC).Value = objTr.FOC_Item
                gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeableItem).Value = objTr.Scheme_Item
            Next
        End If
    End Sub
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
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleSaleDairy, clsUserMgtCode.frmGatePassDairy, fndLocation.Value, txtDate.Value, Nothing)

                If (clsDeliveryNoteFreshSale.PostData(MyBase.Form_ID, txtDocNo.Value)) Then
                    msg = "Successfully Posted"
                    common.clsCommon.MyMessageBoxShow(msg, Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
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
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                Dim strCode = clsDBFuncationality.getSingleValue("select top 1 DOCUMENT_CODE from TSPL_SD_SHIPMENT_DETAIL where GatePass_No ='" & txtDocNo.Value & "'")
                If clsCommon.myLen(strCode) > 0 Then
                    clsCommon.MyMessageBoxShow(Me, "GatePass is already used in Dispatch", Me.Text)
                    Exit Sub
                End If
            End If
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
                If (clsGatePassDairySale.DeleteData(txtDocNo.Value)) Then
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
            'Add Tool tip Task No- TEC/18/05/18-000237
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine +
                                         "TSPL_GATEPASS_MASTER_DAIRYSALE " + Environment.NewLine +
                                         "TSPL_GATEPASS_DETAIL_DAIRYSALE ")
            'Add Tool tip Task No- TEC/18/05/18-000237
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
        CreateLaodinSlipVehicleWise = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.CreateLoadINSlipVehicleWise & "'")) = 0, False, True)
        RouteCodeNotMandatoryOnLoadINSlip = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.RouteCodeNotMandatoryOnLoadINSlip & "'")) = 0, False, True)
        VehicleCodeNotMandatoryOnLoadINSlip = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.VehicleCodeNotMandatoryOnLoadINSlip & "'")) = 0, False, True)
        TxtRouteNo.Visible = True
        MyLabel6.Visible = True
        fndDeliveryNo.Visible = False
        fndDeliveryNo.Enabled = False

        SetUserMgmtNew()
        SetMailRight()
        SetMaxlength()
        fndCustomerNo.Enabled = False
        fndLocation.Enabled = False
        txtPriceCode.Enabled = False
        AddNew()
        RadLabel18.Visible = False
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
        If CreateLaodinSlipVehicleWise = True Then
            'TxtRouteNo.Visible = True
            lblRouteNo.Visible = True
            MyLabel6.Visible = True
        Else
            'TxtRouteNo.Visible = False
            lblRouteNo.Visible = False
            'MyLabel6.Visible = False
        End If
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
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
    '        strbody = strbody.Replace(clsEmailSMSConstants.BookingNo, fndDeliveryNo.Value)
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
        If CreateLaodinSlipVehicleWise = True Then
            Dim qry As String = " select  Vehicle_Code as Code,Description, Model,Vehicle_Type as [Vehicle Type],Vehicle_Brand as [Vehicle Brand],Vehicle_Name as [Vehicle Name] ,Location , [Route No],[Route Desc] from ( " &
                           " select distinct  TSPL_BOOKING_MATSER.Document_Date, TSPL_BOOKING_DETAIL.Vehicle_Code,TSPL_VEHICLE_MASTER.Description , model As Model,Vehicle_Type,Vehicle_Brand,Vehicle_Name ,Location , tspl_route_master.Route_No as [Route No]  , tspl_route_master.Route_Desc as [Route Desc] from TSPL_BOOKING_DETAIL " &
                           " left outer join TSPL_BOOKING_MATSER on TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No " &
                           " left  outer join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_BOOKING_DETAIL.Vehicle_Code " &
                           " left outer join tspl_route_master on tspl_route_master.vehicle_code = TSPL_VEHICLE_MASTER.Vehicle_Id " &
                           "  where   TSPL_BOOKING_DETAIL.Vehicle_Code<>'' and  convert (date,TSPL_BOOKING_MATSER.Document_Date,103) >= convert(date, '" + clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") + "',103)  and  convert (date,TSPL_BOOKING_MATSER.Document_Date,103) <= convert(date, '" + clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") + "',103)  " &
                           "  ) final  "

            'sanjay Ticket No-BHA/27/05/19-000897
            If String.IsNullOrEmpty(clsDBFuncationality.getSingleValue(qry)) Then
                common.clsCommon.MyMessageBoxShow(Me, "No Booking related Vehicle found for selected Date", Me.Text)
                txtDate.Focus()
                Exit Sub
            End If

            txtLorryNo.Value = clsCommon.ShowSelectForm("DSGatePassVehicle", qry, "Code", " ", txtLorryNo.Value, "Vehicle_Code", isButtonClicked)
        Else
            Dim qry As String = "Select Vehicle_Id,Description,model As Model,Vehicle_Type,Vehicle_Brand,Vehicle_Name ,Location , tspl_route_master.Route_No as [Route No]  , tspl_route_master.Route_Desc as [Route Desc]  From TSPL_VEHICLE_MASTER left outer join tspl_route_master on tspl_route_master.vehicle_code = TSPL_VEHICLE_MASTER.Vehicle_Id"
            txtLorryNo.Value = clsCommon.ShowSelectForm("DSGatePass@Vehicle", qry, "Vehicle_Id", " InOut='I'", txtLorryNo.Value, "Vehicle_Id", isButtonClicked)
        End If
        If clsCommon.myLen(txtLorryNo.Value) > 0 Then
            lblVehicleNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Description as Name FROM TSPL_VEHICLE_MASTER Where Vehicle_Id='" + txtLorryNo.Value + "'"))
            txtTransporterName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Transporter_Name from TSPL_TRANSPORT_MASTER where Transport_Id =(Select Transport_Id from TSPL_VEHICLE_MASTER where Vehicle_Id ='" & txtLorryNo.Value & "')"))
            ' lblRouteNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Route_No  from TSPL_ROUTE_MASTER where vehicle_code ='" + txtLorryNo.Value + "'"))
            'GetBookingDetail("", txtLorryNo.Value)
        Else
            lblVehicleNo.Text = ""
            txtTransporterName.Text = ""
            ' lblRouteNo.Text = ""
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
        'Dim qry As String = "Select Route_No,Route_Desc AS Description,Type,Employee_Code As [Employee Code],Off_Day AS [Off Day],City_Code As [City Code],District,Category_Code As [Category Code],Length,Employee_Name As [Employee Name],Depot_Id As [Depot Id],Price_Code As [Price Code],Price_Code_Desc As [Price Code Desc],vehicle_code AS [Vehicle Code] From TSPL_ROUTE_MASTER "
        'TxtRouteNo.Value = clsCommon.ShowSelectForm("DSGatePassRoute", qry, "Route_No", "", TxtRouteNo.Value, "Route_No", isButtonClicked)
        'If clsCommon.myLen(TxtRouteNo.Value) > 0 Then
        '    qry = "SELECT Route_Desc,vehicle_code,Number,Transporter_Name,Capacity  FROM TSPL_ROUTE_MASTER left outer join " & _
        '    "TSPL_VEHICLE_MASTER on TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id left outer join " & _
        '    "TSPL_TRANSPORT_MASTER on TSPL_VEHICLE_MASTER.Transport_Id=TSPL_TRANSPORT_MASTER.Transport_Id where  Route_No  ='" + clsCommon.myCstr(TxtRouteNo.Value) + "' "
        '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        '    If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
        '        lblRouteNo.Text = clsCommon.myCstr(dt.Rows(0)("Route_Desc"))
        '        txtLorryNo.Value = clsCommon.myCstr(dt.Rows(0)("vehicle_code"))
        '        lblVehicleNo.Text = clsCommon.myCstr(dt.Rows(0)("Number"))
        '        txtVehicleCapacity.Text = clsCommon.myCstr(dt.Rows(0)("Capacity"))
        '        txtTransporterName.Text = clsCommon.myCstr(dt.Rows(0)("Transporter_Name"))
        '    End If
        'Dim qry As String = " select distinct Route_No as Code,route_desc from ( " & _
        '                    " select distinct  TSPL_SD_SHIPMENT_HEAD.Document_Date, TSPL_SD_SHIPMENT_HEAD.Route_No,tspl_route_master.route_desc from TSPL_SD_SHIPMENT_HEAD " & _
        '                    " left  outer join tspl_route_master on tspl_route_master.route_no=TSPL_SD_SHIPMENT_HEAD.route_no " & _
        '                    " where TSPL_SD_SHIPMENT_HEAD.screen_type='DS' and TSPL_SD_SHIPMENT_HEAD.Route_No<>'' and  convert (date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) >= convert(date, '" + clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy") + "',103) and convert(date, '" + clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy") + "',103) " & _
        '                    " ) final "
        'Ticket No-VIJ/19/11/19-000063
        Dim StrQryWithWhere As String = ""
        Dim qry As String = "  select distinct [Route No] as Code,[Route Desc] as Description from ( " &
                            "  select distinct   tspl_route_master.Route_No as [Route No]  , tspl_route_master.Route_Desc as [Route Desc] from TSPL_BOOKING_DETAIL " &
                            "  left outer join TSPL_BOOKING_MATSER on TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No " &
                            "  left outer join tspl_route_master on tspl_route_master.Route_No = TSPL_BOOKING_DETAIL.Route_No " &
                            "  where   TSPL_BOOKING_DETAIL.Vehicle_Code<>'' and  convert (date,TSPL_BOOKING_MATSER.Document_Date,103) >=  convert(date, '" + clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") + "',103)  and  convert (date,TSPL_BOOKING_MATSER.Document_Date,103) <=  convert(date, '" + clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") + "',103)   ) final   "
        ' where Final.[Route No] is not null and Final.[Route No] <> ''
        Dim StrWhere As String = "Final.[Route No] is not null and Final.[Route No] <> ''"
        StrQryWithWhere = qry + " where " + StrWhere
        'sanjay Ticket No-BHA/27/05/19-000897
        If String.IsNullOrEmpty(clsDBFuncationality.getSingleValue(StrQryWithWhere)) Then
            common.clsCommon.MyMessageBoxShow(Me, "No Booking related Route found for selected Date", Me.Text)
            txtDate.Focus()
            Exit Sub
        End If

        TxtRouteNo.Value = clsCommon.ShowSelectForm("DSGate@PassRoute", qry, "Code", StrWhere, TxtRouteNo.Value, "Code", isButtonClicked)
        lblRouteNo.Text = clsDBFuncationality.getSingleValue("select Route_Desc from TSPL_ROUTE_MASTER where Route_No='" & TxtRouteNo.Value & "'")

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
        End If

        If Not chkShortClose.Checked And btnSave.Enabled = False Then
            If Not (common.clsCommon.MyMessageBoxShow(Me, "Want To Open Delivery Order No. " + txtDocNo.Value + "" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes) Then
                Return
            End If
        End If

        Dim qry As String = "select count(*) from TSPL_GATEPASS_MASTER_DAIRYSALE where Document_No='" + txtDocNo.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "'"
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
            qry = "update TSPL_GATEPASS_MASTER_DAIRYSALE set CreditApproval_Reqd='Y',Status=2 where Document_No='" & txtDocNo.Value & "'"
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

    Private Sub RadLabel5_Click(sender As Object, e As EventArgs) Handles RadLabel5.Click

    End Sub
    Public Function GetQuery() As String
        Dim Qry As String = Nothing
        ' Qry= "select TSPL_ITEM_MASTER.hsn_code,TSPL_CUSTOMER_MASTER.GSTNO as Cust_GSTIN,Cust_State.STATE_CODE as Cust_State_Code,Cust_State.GST_STATE_Code as Cust_GST_State_Code,Cust_State.STATE_NAME as Cust_State_Name," & _
        '                    " tspl_location_master.GSTNO as Loc_GSTIN,TSPL_STATE_MASTER.STATE_CODE as Loc_State_Code,TSPL_STATE_MASTER.STATE_NAME as Loc_State_Name,TSPL_STATE_MASTER.GST_STATE_Code as Loc_Gst_State_Code," & _
        '                    " TSPL_COMPANY_MASTER.logo_img,TSPL_COMPANY_MASTER.comp_code,TSPL_COMPANY_MASTER.comp_Name,TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end "
        'Qry += " + case when LEN(TSPL_COMPANY_MASTER.tin_no)>0 then ', TIN No '+TSPL_COMPANY_MASTER.tin_no else ' ' end "
        'Qry += " as Comp_Add,TSPL_COMPANY_MASTER.cinno as Comp_CinNo,TSPL_COMPANY_MASTER.ServiceTax_Reg_No +case when len(TSPL_COMPANY_MASTER.Ecc_No)>0 then '/ '+TSPL_COMPANY_MASTER.Ecc_No else '' end as Comp_ECC_No,TSPL_COMPANY_MASTER.CE_Range as Comp_CE_Range,TSPL_COMPANY_MASTER.CE_Division as GP_Division,TSPL_GATEPASS_MASTER_DAIRYSALE.location_code,tspl_location_master.location_Desc,tspl_location_master.add1 +case when len(tspl_location_master.add2)>0 then ', '+tspl_location_master.add2 else '' end +case when LEN(isnull(tspl_location_master.Add3,''))>0 then ', '+isnull(tspl_location_master.Add3,'') else ' ' end "
        'Qry += " + case when LEN(TSPL_STATE_MASTER.STATE_NAME)>0 then ', '+TSPL_STATE_MASTER.STATE_NAME else ' ' end "
        'Qry += " as Loc_Add,tspl_location_master.tin_no as Loc_TinNo,TSPL_GATEPASS_MASTER_DAIRYSALE.customer_code,TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_GATEPASS_MASTER_DAIRYSALE.vehicle_code,TSPL_VEHICLE_MASTER.description as vehicle_Name,convert(varchar,TSPL_GATEPASS_MASTER_DAIRYSALE.Document_Date,103)  as Document_Date,TSPL_GATEPASS_MASTER_DAIRYSALE.Document_No ,TSPL_GATEPASS_DETAIL_DAIRYSALE.Item_Code ,TSPL_ITEM_MASTER.Item_Desc ,TSPL_GATEPASS_DETAIL_DAIRYSALE.Unit_code ,TSPL_GATEPASS_DETAIL_DAIRYSALE.Qty ,TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code "
        'Qry += " from TSPL_GATEPASS_DETAIL_DAIRYSALE"
        'Qry += " left join TSPL_GATEPASS_MASTER_DAIRYSALE on TSPL_GATEPASS_MASTER_DAIRYSALE.document_no=TSPL_GATEPASS_DETAIL_DAIRYSALE.document_no"
        'Qry += " left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.comp_code=TSPL_GATEPASS_MASTER_DAIRYSALE.comp_code"
        'Qry += " left join tspl_location_master on tspl_location_master.location_code=TSPL_GATEPASS_MASTER_DAIRYSALE.location_code"
        'Qry += " left join TSPL_STATE_MASTER on tspl_location_master.State =TSPL_STATE_MASTER.STATE_CODE "
        'Qry += " left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_GATEPASS_MASTER_DAIRYSALE.customer_code" & _
        '       " left join TSPL_STATE_MASTER as Cust_State on TSPL_CUSTOMER_MASTER.State =Cust_State.STATE_CODE "
        'Qry += " left join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.vehicle_id=TSPL_GATEPASS_MASTER_DAIRYSALE.vehicle_code"
        'Qry += " left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_GATEPASS_DETAIL_DAIRYSALE.Item_Code  "
        '---dairy booking wise-
        Qry = "select TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 as Comp_Add1,TSPL_COMPANY_MASTER.Add2 as Comp_Add2,TSPL_COMPANY_MASTER.Add3 as Comp_Add3,TSPL_COMPANY_MASTER.Pincode as Comp_Pin ,TSPL_LOCATION_MASTER.Add1,TSPL_LOCATION_MASTER.Add2,TSPL_LOCATION_MASTER.ADD3,TSPL_LOCATION_MASTER.Pin_Code,TSPL_LOCATION_MASTER.Location_Desc," &
              " TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code as Booking_No,convert(varchar(15),TSPL_BOOKING_MATSER.Document_Date,103) as Booking_Date ,TSPL_GATEPASS_MASTER_DAIRYSALE.Document_No as GatePass_No ,isnull(TSPL_GATEPASS_MASTER_DAIRYSALE.Route_No,'') as Route_No ,isnull(TSPL_ROUTE_MASTER.Route_Desc,'') as Route_Desc ,convert(varchar(15)," &
                " TSPL_GATEPASS_MASTER_DAIRYSALE.Document_Date,103) as GatePass_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_ITEM_MASTER.HSN_CODE,TSPL_GATEPASS_MASTER_DAIRYSALE.Comments ,TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_BOOKING_DETAIL.cust_code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_BOOKING_DETAIL.Booking_Qty AS B_Qty," &
                " TSPL_GATEPASS_DETAIL_DAIRYSALE.qty as Booking_Qty,TSPL_BOOKING_DETAIL.Unit_code ,TSPL_GATEPASS_MASTER_DAIRYSALE.Vehicle_Code ,TSPL_VEHICLE_MASTER.Description AS Vechile_Name,TSPL_GATEPASS_DETAIL_DAIRYSALE.FOC_Item   from TSPL_GATEPASS_MASTER_DAIRYSALE " &
                " LEFT OUTER JOIN TSPL_GATEPASS_DETAIL_DAIRYSALE ON TSPL_GATEPASS_MASTER_DAIRYSALE.DOCUMENT_No=TSPL_GATEPASS_DETAIL_DAIRYSALE.Document_No " &
                " left outer join TSPL_BOOKING_DETAIL on TSPL_GATEPASS_DETAIL_DAIRYSALE.Delivery_Code=TSPL_BOOKING_DETAIL.Document_No and TSPL_GATEPASS_DETAIL_DAIRYSALE.Item_Code=TSPL_BOOKING_DETAIL.Item_Code " &
                " and TSPL_GATEPASS_DETAIL_DAIRYSALE.Unit_code=TSPL_BOOKING_DETAIL.Unit_code and TSPL_BOOKING_DETAIL.Vehicle_Code=TSPL_GATEPASS_MASTER_DAIRYSALE.Vehicle_Code left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_BOOKING_DETAIL.Cust_Code " &
                " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_BOOKING_DETAIL.Item_Code " &
                " LEFT OUTER JOIN TSPL_BOOKING_MATSER ON TSPL_BOOKING_MATSER.Document_No =TSPL_BOOKING_DETAIL.Document_No" &
                " LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code=TSPL_BOOKING_MATSER.Comp_Code " &
                " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =TSPL_BOOKING_MATSER.location_code" &
                " left outer join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id  =TSPL_GATEPASS_MASTER_DAIRYSALE.Vehicle_Code " &
                " left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = TSPL_GATEPASS_MASTER_DAIRYSALE.Route_No "
        '---dairy booking wise end qry-
        Return Qry
    End Function
    Public Sub funPrint(ByVal strDocNo As String)
        Try
            Dim Qry As String = GetQuery()
            Dim Qry2 As String = "select TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 as Comp_Add1,TSPL_COMPANY_MASTER.Add2 as Comp_Add2,TSPL_COMPANY_MASTER.Add3 as Comp_Add3,TSPL_COMPANY_MASTER.Pincode as Comp_Pin  ,Main_Final.* from ( " + Qry + " WHERE TSPL_GATEPASS_DETAIL_DAIRYSALE.Document_No='" + strDocNo + "') as Main_Final " &
                                 " LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code=Main_Final.Comp_Code "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt.Rows.Count > 0 Then
                ' frmCrystalReportViewer.funsubreportWithdt(CrystalReportFolder.NewSalesReports, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "crptGatePassDairySale", "Retail Invoice", clsCommon.myCDate(dt.Rows(0)("Document_Date")), "rptCompanyAddress.rpt")
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funsubreportWithdt(CrystalReportFolder.NewSalesReports, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptDairySaleGatePass", "Dairy Gate Pass", clsCommon.myCDate(dt.Rows(0)("GatePass_Date")), "rptCompanyAddress.rpt")
                frmCRV = Nothing
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
            myMessages.blankValue("No data found to Print")
        Else
            funPrint(txtDocNo.Value)
        End If
    End Sub


    Private Sub BtnPrintDetail_Click(sender As Object, e As EventArgs) Handles BtnPrintDetail.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                myMessages.blankValue("No data found to Print")
            Else
                Dim Qry As String = ""
                Dim Qry2 As String = ""
                If CreateLaodinSlipVehicleWise = False Then
                    Qry = GetQuery()
                    'Qry2 = "select   '' as BRAND,'' AS BRANDDESC,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 as Comp_Add1,TSPL_COMPANY_MASTER.Add2 as Comp_Add2,TSPL_COMPANY_MASTER.Add3 as Comp_Add3,TSPL_COMPANY_MASTER.Pincode as Comp_Pin  ,Main_Final.* from " &
                    '    " (SELECT  max(FINAL .Route_No) as Route_No ,max(FINAL.Route_Desc) as  Route_Desc,  Cust_code,MAX(Customer_Name) AS Customer_Name,MAX(GatePass_No) as GatePass_No,max(GatePass_Date) as GatePass_Date,max(Booking_No) as Booking_No,max(Booking_Date) as Booking_Date,max(Vehicle_Code) as Vehicle_Code,max(Vechile_Name) as Vechile_Name, Item_Code,MAX(Item_Desc) AS Item_Desc,MAX(HSN_CODE) AS HSN_CODE,unit_code ," &
                    '    " sum(B_Qty) AS Booking_Qty,max(Comp_Code) as Comp_Code,max(Add1) as Loc_Add1,max(Add2) as Loc_Add2,max(Add3) as Loc_Add3,max(Pin_Code) as Loc_Pin_Code ,max(Comments) as Remarks ,max(Location_Desc) as Location_Desc,FOC_Item FROM ( " + Qry + " WHERE TSPL_GATEPASS_DETAIL_DAIRYSALE.Document_No='" + txtDocNo.Value + "')" &
                    '    " AS FINAL GROUP BY Cust_code,item_code,unit_code,FOC_Item,Vehicle_Code )as Main_Final " &
                    '    " LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code=Main_Final.Comp_Code "
                    Qry2 = "select convert(varchar(15),TSPL_GATEPASS_MASTER_DAIRYSALE.Document_Date,103) as GatePass_Date,
TSPL_DEMAND_BOOKING_MASTER.TripNo,
TSPL_DEMAND_BOOKING_MASTER.shiftType,
TSPL_city_MASTER.City_Name,
TSPL_DEMAND_BOOKING_MASTER.Comp_Code,
TSPL_DEMAND_BOOKING_MASTER.location_code
,TSPL_DEMAND_BOOKING_MASTER.Document_No as Demand_No,convert(varchar(15),TSPL_GATEPASS_MASTER_DAIRYSALE.Document_Date,103) as Demand_Date 
,isnull(TSPL_DEMAND_BOOKING_MASTER.Route_No,'') as Route_No ,isnull(TSPL_ROUTE_MASTER.Route_Desc,'') as Route_Desc 
 ,TSPL_DEMAND_BOOKING_DETAIL.cust_code,TSPL_CUSTOMER_MASTER.Customer_Name_Hindi
,TSPL_DEMAND_BOOKING_DETAIL.Qty
,TSPL_DEMAND_BOOKING_DETAIL.TotalCrates_ItemWise
,TSPL_DEMAND_BOOKING_DETAIL.TotalLtr_ItemWise
,TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount
,TSPL_DEMAND_BOOKING_DETAIL.unit_code
,'Crate' AS Unit_Code
,TSPL_ITEM_MASTER.Alies_Name_Hindi as Item_Alies_Name_Hindi 
,(case when TSPL_DEMAND_BOOKING_DETAIL.unit_code='Pack' then TSPL_DEMAND_BOOKING_DETAIL.Qty else 0 end) as PackQTY
,(case when TSPL_DEMAND_BOOKING_DETAIL.unit_code='Crate' then TSPL_DEMAND_BOOKING_DETAIL.Qty else 0 end )+(case when TSPL_DEMAND_BOOKING_DETAIL.unit_code='Pack' then TSPL_DEMAND_BOOKING_DETAIL.Qty else 0 end)
+Isnull(case when (TSPL_DEMAND_BOOKING_DETAIL.Unit_code)='Pouch' then (ISNULL(TSPL_DEMAND_BOOKING_DETAIL.Qty,0)/ItemConversionInCrate.Conversion_Factor) end,0) as CrateQty

,case when TSPL_DEMAND_BOOKING_DETAIL.unit_code='Pouch' then TSPL_DEMAND_BOOKING_DETAIL.Qty else 0 end as PouchQty
,'' as Crate,'Crate' as Pouch
from TSPL_GATEPASS_MASTER_DAIRYSALE
left outer join TSPL_DEMAND_BOOKING_MASTER on TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_GATEPASS_MASTER_DAIRYSALE.Against_Demand
left outer join TSPL_DEMAND_BOOKING_DETAIL on TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No 
 left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_DEMAND_BOOKING_DETAIL.Cust_Code 
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code 
                           left join tspl_item_uom_detail CrateUnit on CrateUnit.item_code=TSPL_DEMAND_BOOKING_DETAIL.item_code  and 	CrateUnit.uom_code=	'Crate' 
						left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code='Pouch') as ItemConversionInPouch on ItemConversionInPouch.Item_code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code
						left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code='LTR') as ItemConversionInLTR on ItemConversionInLTR.Item_code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code
						left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code='Crate') as ItemConversionInCrate on ItemConversionInCrate.Item_code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code
                           left outer join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id  =TSPL_DEMAND_BOOKING_DETAIL.Vehicle_Code 
                           left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = TSPL_DEMAND_BOOKING_MASTER.Route_No
                             left outer join TSPL_city_MASTER on TSPL_city_MASTER.city_code = TSPL_DEMAND_BOOKING_MASTER.city_code
                           left outer join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.Unit_Code=TSPL_DEMAND_BOOKING_DETAIL.Unit_code
                            LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =TSPL_DEMAND_BOOKING_MASTER.location_code
                          WHERE TSPL_GATEPASS_MASTER_DAIRYSALE.Document_No='" + txtDocNo.Value + "'"
                    ' use only for pouch in whr condition TSPL_ITEM_MASTER.Is_Milk_Pouch= 1 
                Else
                    'Qry = "select Booking_No from TSPL_MULTI_BOOKING_DETAIL where document_no =" & txtDocNo.Value & "'"
                    'Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(Qry)
                    'Dim strBooking As String = ""
                    'For Each dr As DataRow In dt1.Rows
                    '    If clsCommon.myLen(strBooking) > 0 Then
                    '        strBooking += ","
                    '    End If
                    '    strBooking += clsCommon.myCstr(dr("Booking_No"))
                    'Next
                    Qry2 = "select   '' as BRAND,'' AS BRANDDESC,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 as Comp_Add1, " &
                        "TSPL_COMPANY_MASTER.Add2 as Comp_Add2,TSPL_COMPANY_MASTER.Add3 as Comp_Add3,TSPL_COMPANY_MASTER.Pincode as Comp_Pin  ,Main_Final.* from  ( " &
                    "SELECT  max(FINAL .Route_No) as Route_No ,max(FINAL.Route_Desc) as  Route_Desc,  Cust_code,MAX(Customer_Name) AS Customer_Name, " &
                    "MAX(GatePass_No) as GatePass_No,max(GatePass_Date) as GatePass_Date,max(Booking_No) as Booking_No,max(Booking_Date) as Booking_Date, " &
                    "max(Vehicle_Code) as Vehicle_Code,max(Vechile_Name) as Vechile_Name, Item_Code,MAX(Item_Desc) AS Item_Desc,MAX(HSN_CODE) AS HSN_CODE, " &
                    "unit_code , sum(B_Qty) AS Booking_Qty,max(Comp_Code) as Comp_Code,max(Add1) as Loc_Add1,max(Add2) as Loc_Add2,max(Add3) as Loc_Add3, " &
                    "max(Pin_Code) as Loc_Pin_Code ,max(Comments) as Remarks ,max(Location_Desc) as Location_Desc,iif(FOC_item='Y',1,0) as FOC_item  FROM ( " &
                    " select TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 as Comp_Add1,TSPL_COMPANY_MASTER.Add2 as Comp_Add2," &
                    " TSPL_COMPANY_MASTER.Add3 as Comp_Add3,TSPL_COMPANY_MASTER.Pincode as Comp_Pin ,TSPL_LOCATION_MASTER.Add1,TSPL_LOCATION_MASTER.Add2,TSPL_LOCATION_MASTER.ADD3,TSPL_LOCATION_MASTER.Pin_Code,TSPL_LOCATION_MASTER.Location_Desc, TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code as Booking_No, " &
                     "convert(varchar(15),TSPL_BOOKING_MATSER.Document_Date,103) as Booking_Date , " &
                     "TSPL_BOOKING_DETAIL.Document_No as GatePass_No , " &
                     "isnull(TSPL_GATEPASS_MASTER_DAIRYSALE.Route_No,'') as Route_No , " &
                     "isnull(TSPL_ROUTE_MASTER.Route_Desc,'') as Route_Desc , " &
                     "convert(varchar(15), TSPL_GATEPASS_MASTER_DAIRYSALE.Document_Date,103) as GatePass_Date,TSPL_BOOKING_MATSER.Comp_Code,TSPL_ITEM_MASTER.HSN_CODE,TSPL_GATEPASS_MASTER_DAIRYSALE.Comments , " &
                    "TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc, TSPL_BOOKING_DETAIL.cust_code, " &
                    "TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_BOOKING_DETAIL.Booking_Qty AS B_Qty, " &
                    "TSPL_BOOKING_DETAIL.Document_No, " &
                    "TSPL_BOOKING_DETAIL.Booking_Qty as Booking_Qty,TSPL_BOOKING_DETAIL.Unit_code , " &
                    "TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_VEHICLE_MASTER.Description AS Vechile_Name ,TSPL_BOOKING_DETAIL.Scheme_Item as FOC_item from TSPL_GATEPASS_MASTER_DAIRYSALE  " &
                    "Left outer join TSPL_MULTI_BOOKING_DETAIL on TSPL_GATEPASS_MASTER_DAIRYSALE.Document_No = TSPL_MULTI_BOOKING_DETAIL.Document_No " &
                    "left outer join TSPL_BOOKING_DETAIL on TSPL_MULTI_BOOKING_DETAIL.Booking_No=TSPL_BOOKING_DETAIL.Document_No " &
                    "left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_BOOKING_DETAIL.Cust_Code " &
                    "left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_BOOKING_DETAIL.Item_Code  " &
                    "LEFT OUTER JOIN TSPL_BOOKING_MATSER ON TSPL_BOOKING_MATSER.Document_No =TSPL_BOOKING_DETAIL.Document_No  " &
                    "LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code=TSPL_BOOKING_MATSER.Comp_Code  " &
                    "LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =TSPL_BOOKING_MATSER.location_code " &
                    "left outer join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id  =TSPL_BOOKING_DETAIL.Vehicle_Code  " &
                    "left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = TSPL_GATEPASS_MASTER_DAIRYSALE.Route_No " &
                    "WHERE  TSPL_MULTI_BOOKING_DETAIL.Booking_No in  (" + clsCommon.GetMulcallString(txtmultiBooking.arrValueMember) + ") " &
                    ") AS FINAL GROUP BY Cust_code,item_code,unit_code,Vehicle_Code,FOC_item )as Main_Final " &
                    "LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code=Main_Final.Comp_Code "



                End If


                Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry2)
                If dt.Rows.Count > 0 Then
                    ' frmCrystalReportViewer.funsubreportWithdt(CrystalReportFolder.NewSalesReports, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "crptGatePassDairySale", "Retail Invoice", clsCommon.myCDate(dt.Rows(0)("Document_Date")), "rptCompanyAddress.rpt")
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funsubreportWithdt(CrystalReportFolder.NewSalesReports, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptDairySaleGatePass", "Dairy Gate Pass", clsCommon.myCDate(dt.Rows(0)("GatePass_Date")), "rptCompanyAddress.rpt")
                    frmCRV = Nothing
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub BtnPrintSummry_Click(sender As Object, e As EventArgs) Handles BtnPrintSummry.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                myMessages.blankValue("No data found to Print")
            Else
                ' Dim Qry As String = GetQuery()
                'Dim Qry2 As String = "select '' as BRAND,'' AS BRANDDESC,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 as Comp_Add1,TSPL_COMPANY_MASTER.Add2 as Comp_Add2,TSPL_COMPANY_MASTER.Add3 as Comp_Add3,TSPL_COMPANY_MASTER.Pincode as Comp_Pin  ,Main_Final.* from " & _
                '    " (SELECT MAX(GatePass_No) as GatePass_No,max(GatePass_Date) as GatePass_Date,max(Booking_No) as Booking_No,max(Booking_Date) as Booking_Date,max(Vehicle_Code) as Vehicle_Code,max(Vechile_Name) as Vechile_Name, Item_Code,MAX(Item_Desc) AS Item_Desc,MAX(HSN_CODE) AS HSN_CODE,unit_code ," & _
                '    " max(Booking_Qty) AS Qty,max(Comp_Code) as Comp_Code,max(Add1) as Loc_Add1,max(Add2) as Loc_Add2,max(Add3) as Loc_Add3,max(Pin_Code) as Loc_Pin_Code ,max(Comments) as Remarks ,max(Location_Desc) as Location_Desc FROM ( " + Qry + " WHERE TSPL_GATEPASS_DETAIL_DAIRYSALE.Document_No='" + txtDocNo.Value + "')" & _
                '    " AS FINAL GROUP BY Item_Code ,Unit_code )as Main_Final " & _
                '    " LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code=Main_Final.Comp_Code "
                Dim Qry2 As String = ""
                '====update by preeti gupta Against ticket no[MIL/24/07/19-000111,UDL/10/04/19-000287,MIL/01/08/19-000116]
                'Ticket No  MIL/20/08/19-000123 Add Crate Qty,Other Qty
                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GMD") = CompairStringResult.Equal Then
                    Qry2 = "select final.*,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 as Comp_Add1,TSPL_COMPANY_MASTER.Add2 as Comp_Add2,TSPL_COMPANY_MASTER.Add3 as Comp_Add3,TSPL_COMPANY_MASTER.Pincode as Comp_Pin  from (" &
"select Gate_Pass_No,max(gate_Pass_Date) as gate_Pass_Date,Booking_Location_Code,max(Booking_Loc_desc) as Booking_Loc_desc,(Vehicle_Code) as Vehicle_Code,max(Vechile_Name) as Vechile_Name,max(Route_No) as Route_No,max(Route_Desc) as Route_Desc,max(Booking_Loc_Short_Name) as Booking_Loc_Short_Name,max(Bookin_Loc_Add1)as Bookin_Loc_Add1,max(Boking_Loc_Add2) as Boking_Loc_Add2,max(booking_Loc_Add3) as booking_Loc_Add3,max(Bookin_Loc_Pin_Code) as Bookin_Loc_Pin_Code,max(Loc_Add1) as Loc_Add1,max(Loc_Add2) as Loc_Add2,max(Loc_Add3) as Loc_Add3,max(Loc_Pin_Code) as Loc_Pin_Code" &
",max(Location_Desc) as Location_Desc,max(Location_Code) as Location_Code,Item_Code,max(Item_Desc) as Item_Desc,max(HSN_Code) as HSN_Code,Unit_code,sum(Booking_Qty) as Booking_Qty" &
",max(comp_code) as comp_code,max(Remarks) as Remarks,max(cust_group_desc) as cust_group_desc,max(Cust_Group_Code) as Cust_Group_Code from (" &
"select TSPL_MULTI_BOOKING_DETAIL.Document_No as Gate_Pass_No,TSPL_MULTI_BOOKING_DETAIL.Booking_No,convert(varchar,TSPL_GATEPASS_MASTER_DAIRYSALE.Document_Date,103) as gate_Pass_Date ,TSPL_BOOKING_MATSER.location_code as Booking_Location_Code ,Booking_Location.Location_Desc as Booking_Loc_desc,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_VEHICLE_MASTER.Description AS Vechile_Name , isnull(TSPL_GATEPASS_MASTER_DAIRYSALE.Route_No,'') as Route_No ,isnull(TSPL_ROUTE_MASTER.Route_Desc,'') as Route_Desc,Booking_Location.Loc_Short_Name as Booking_Loc_Short_Name,Booking_Location.Add1 AS Bookin_Loc_Add1,Booking_Location.Add2 AS Boking_Loc_Add2,Booking_Location.ADD3 AS booking_Loc_Add3,Booking_Location.Pin_Code AS Bookin_Loc_Pin_Code,TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc ,TSPL_ITEM_MASTER.HSN_Code  ,TSPL_BOOKING_DETAIL.Unit_code ,TSPL_BOOKING_DETAIL.DO_Qty as Booking_Qty " &
", TSPL_LOCATION_MASTER.Add1 AS Loc_Add1,TSPL_LOCATION_MASTER.Add2 AS Loc_Add2,TSPL_LOCATION_MASTER.ADD3 AS Loc_Add3,TSPL_LOCATION_MASTER.Pin_Code AS Loc_Pin_Code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_GATEPASS_MASTER_DAIRYSALE.Location_Code ,TSPL_GATEPASS_MASTER_DAIRYSALE.Comp_Code ,TSPL_GATEPASS_MASTER_DAIRYSALE.Comments AS Remarks,TSPL_BOOKING_MATSER.Cust_Group_Code ,TSPL_CUSTOMER_GROUP_MASTER.cust_group_desc " &
                   " from TSPL_MULTI_BOOKING_DETAIL " &
"left join TSPL_BOOKING_MATSER on TSPL_BOOKING_MATSER.Document_No =TSPL_MULTI_BOOKING_DETAIL.Booking_No " &
"left join  TSPL_BOOKING_DETAIL on TSPL_BOOKING_DETAIL.Document_No =TSPL_BOOKING_MATSER.Document_No " &
"left join TSPL_GATEPASS_MASTER_DAIRYSALE on TSPL_GATEPASS_MASTER_DAIRYSALE.Document_No =TSPL_MULTI_BOOKING_DETAIL.Document_No " &
"left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_BOOKING_DETAIL.Item_Code " &
"LEFT OUTER JOIN TSPL_LOCATION_MASTER as Booking_Location ON Booking_Location.Location_Code =TSPL_BOOKING_MATSER.location_code " &
"left outer join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id  =TSPL_GATEPASS_MASTER_DAIRYSALE.Vehicle_Code   " &
"left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = TSPL_GATEPASS_MASTER_DAIRYSALE.Route_No " &
" left join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.cust_group_code=TSPL_BOOKING_MATSER.Cust_Group_Code " &
"LEFT OUTER JOIN TSPL_LOCATION_MASTER  ON TSPL_LOCATION_MASTER .Location_Code =TSPL_GATEPASS_MASTER_DAIRYSALE.location_code" &
" where tspl_booking_detail.foc_item<>1 "
                    If VehicleCodeNotMandatoryOnLoadINSlip = False Then
                        Qry2 += " and tspl_booking_detail.Vehicle_Code ='" + txtLorryNo.Value + "'"
                    End If
                    Qry2 += " and TSPL_GATEPASS_MASTER_DAIRYSALE.Document_No ='" + txtDocNo.Value + "'" &
") as Location " &
"group by Gate_Pass_No" &
 " ,Vehicle_Code,Booking_Location_Code ,Item_Code ,Unit_code,Cust_Group_Code " &
 ") as final  LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code=final.Comp_Code  "




                ElseIf CreateLaodinSlipVehicleWise = False Then
                    Qry2 = " 	   select TSPL_VEHICLE_MASTER.Vehicle_id,TSPL_GATEPASS_MASTER_DAIRYSALE.Against_Demand, isnull(TSPL_GATEPASS_MASTER_DAIRYSALE.Route_No,'') as Route_No ,isnull(TSPL_ROUTE_MASTER.Route_Desc,'') as Route_Desc, TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 as Comp_Add1,TSPL_COMPANY_MASTER.Add2 as Comp_Add2,TSPL_COMPANY_MASTER.Add3 as Comp_Add3,TSPL_COMPANY_MASTER.Pincode as Comp_Pin ,TSPL_COMPANY_MASTER.Add3 as Comp_Add3,TSPL_COMPANY_MASTER.Pincode as Comp_Pin , TSPL_LOCATION_MASTER.Add1 AS Loc_Add1,TSPL_LOCATION_MASTER.Add2 AS Loc_Add2,TSPL_LOCATION_MASTER.ADD3 AS Loc_Add3,TSPL_LOCATION_MASTER.Pin_Code AS Loc_Pin_Code,TSPL_LOCATION_MASTER.Location_Desc, TSPL_GATEPASS_MASTER_DAIRYSALE.Document_No,TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code  AS Booking_No,convert(varchar(15),TSPL_BOOKING_MATSER.Document_Date,103) as Booking_Date ,TSPL_GATEPASS_MASTER_DAIRYSALE.Document_No as GatePass_No ,convert(varchar(15), TSPL_GATEPASS_MASTER_DAIRYSALE.Document_Date,103) as GatePass_Date,TSPL_GATEPASS_MASTER_DAIRYSALE.Comp_Code,TSPL_ITEM_MASTER.HSN_CODE,TSPL_GATEPASS_MASTER_DAIRYSALE.Comments AS Remarks , TSPL_GATEPASS_DETAIL_DAIRYSALE.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_GATEPASS_MASTER_DAIRYSALE.Customer_Code AS cust_code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_GATEPASS_DETAIL_DAIRYSALE.qty,TSPL_GATEPASS_DETAIL_DAIRYSALE.Unit_code ,TSPL_GATEPASS_MASTER_DAIRYSALE.Vehicle_Code ,TSPL_VEHICLE_MASTER.Description AS Vechile_Name, TSPL_GATEPASS_DETAIL_DAIRYSALE.FOC_Item from TSPL_GATEPASS_MASTER_DAIRYSALE  
							   LEFT OUTER JOIN TSPL_GATEPASS_DETAIL_DAIRYSALE ON TSPL_GATEPASS_MASTER_DAIRYSALE.DOCUMENT_No=TSPL_GATEPASS_DETAIL_DAIRYSALE.Document_No 
							   left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_GATEPASS_MASTER_DAIRYSALE.Customer_Code  
							   left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_GATEPASS_DETAIL_DAIRYSALE.Item_Code  
							   LEFT OUTER JOIN TSPL_BOOKING_MATSER ON TSPL_BOOKING_MATSER.Document_No =TSPL_GATEPASS_MASTER_DAIRYSALE.DELIVERY_CODE
                              LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code=TSPL_GATEPASS_MASTER_DAIRYSALE.Comp_Code 
                             LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =TSPL_BOOKING_MATSER.location_code  
							   left outer join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id  =TSPL_GATEPASS_MASTER_DAIRYSALE.Vehicle_Code  
							   left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = TSPL_GATEPASS_MASTER_DAIRYSALE.Route_No 
							   WHERE TSPL_GATEPASS_MASTER_DAIRYSALE.Document_No='" + txtDocNo.Value + "' ORDER BY TSPL_GATEPASS_DETAIL_DAIRYSALE.FOC_Item  "
                Else
                    Qry2 = " select  isnull(TSPL_GATEPASS_MASTER_DAIRYSALE.Route_No,'') as Route_No ,isnull(TSPL_ROUTE_MASTER.Route_Desc,'') as Route_Desc, TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 as Comp_Add1,TSPL_COMPANY_MASTER.Add2 as Comp_Add2,TSPL_COMPANY_MASTER.Add3 as Comp_Add3,TSPL_COMPANY_MASTER.Pincode as Comp_Pin ,TSPL_COMPANY_MASTER.Add3 as Comp_Add3,TSPL_COMPANY_MASTER.Pincode as Comp_Pin ," &
                                " TSPL_LOCATION_MASTER.Add1 AS Loc_Add1,TSPL_LOCATION_MASTER.Add2 AS Loc_Add2,TSPL_LOCATION_MASTER.ADD3 AS Loc_Add3,TSPL_LOCATION_MASTER.Pin_Code AS Loc_Pin_Code,TSPL_LOCATION_MASTER.Location_Desc," &
                                " TSPL_GATEPASS_MASTER_DAIRYSALE.Document_No,TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code  AS Booking_No,convert(varchar(15),TSPL_BOOKING_MATSER.Document_Date,103) as Booking_Date ,TSPL_GATEPASS_MASTER_DAIRYSALE.Document_No as GatePass_No ,convert(varchar(15), TSPL_GATEPASS_MASTER_DAIRYSALE.Document_Date,103) as GatePass_Date,TSPL_GATEPASS_MASTER_DAIRYSALE.Comp_Code,TSPL_ITEM_MASTER.HSN_CODE,TSPL_GATEPASS_MASTER_DAIRYSALE.Comments AS Remarks ," &
                               " TSPL_GATEPASS_DETAIL_DAIRYSALE.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_GATEPASS_MASTER_DAIRYSALE.Customer_Code AS cust_code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_GATEPASS_DETAIL_DAIRYSALE.qty,TSPL_GATEPASS_DETAIL_DAIRYSALE.Unit_code ,TSPL_GATEPASS_MASTER_DAIRYSALE.Vehicle_Code ,TSPL_VEHICLE_MASTER.Description AS Vechile_Name," &
                               " TSPL_GATEPASS_DETAIL_DAIRYSALE.FOC_Item from TSPL_GATEPASS_MASTER_DAIRYSALE  LEFT OUTER JOIN TSPL_GATEPASS_DETAIL_DAIRYSALE ON TSPL_GATEPASS_MASTER_DAIRYSALE.DOCUMENT_No=TSPL_GATEPASS_DETAIL_DAIRYSALE.Document_No  " &
                               " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_GATEPASS_MASTER_DAIRYSALE.Customer_Code  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_GATEPASS_DETAIL_DAIRYSALE.Item_Code " &
                                " LEFT OUTER JOIN TSPL_BOOKING_MATSER ON TSPL_BOOKING_MATSER.Document_No =TSPL_GATEPASS_MASTER_DAIRYSALE.DELIVERY_CODE LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code=TSPL_GATEPASS_MASTER_DAIRYSALE.Comp_Code " &
                                " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =TSPL_GATEPASS_MASTER_DAIRYSALE.location_code " &
                                " left outer join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id  =TSPL_GATEPASS_MASTER_DAIRYSALE.Vehicle_Code  " &
                                " left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = TSPL_GATEPASS_MASTER_DAIRYSALE.Route_No " &
                                " WHERE TSPL_GATEPASS_MASTER_DAIRYSALE.Document_No='" + txtDocNo.Value + "'" &
                               " ORDER BY TSPL_GATEPASS_DETAIL_DAIRYSALE.FOC_Item "
                End If


                Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry2)
                If dt.Rows.Count > 0 Then
                    ' frmCrystalReportViewer.funsubreportWithdt(CrystalReportFolder.NewSalesReports, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "crptGatePassDairySale", "Retail Invoice", clsCommon.myCDate(dt.Rows(0)("Document_Date")), "rptCompanyAddress.rpt")
                    Dim frmCRV As New frmCrystalReportViewer()
                    If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GMD") = CompairStringResult.Equal Then
                        frmCRV.funsubreportWithdt(CrystalReportFolder.NewSalesReports, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "CrptSummaryGatePassDairySaleForLocationWise", "Dairy Gate Pass Summry", clsCommon.myCDate(dt.Rows(0)("gate_Pass_Date")), "rptCompanyAddress.rpt")
                    Else
                        frmCRV.funsubreportWithdt(CrystalReportFolder.NewSalesReports, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "crptDairyGatePassEntry_New", "Dairy Gate Pass Summry", clsCommon.myCDate(dt.Rows(0)("GatePass_Date")), "rptCompanyAddress.rpt")
                    End If

                    frmCRV = Nothing
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtmultiBooking__My_Click(sender As Object, e As EventArgs) Handles txtmultiBooking._My_Click
        Try
            'Ticket No-MIL/19/08/19-000121 ,sanjay , VehicleCodeNotMandatoryOnLoadINSlip
            'Ticket No : MIL/08/07/19-000105 By Prabhakar
            If clsCommon.myLen(TxtRouteNo.Value) <= 0 AndAlso RouteCodeNotMandatoryOnLoadINSlip = False Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Route No", Me.Text)
                TxtRouteNo.Focus()
                'ElseIf clsCommon.myLen(txtLorryNo.Value) <= 0 AndAlso VehicleCodeNotMandatoryOnLoadINSlip = False Then
                '    common.clsCommon.MyMessageBoxShow(Me, "Please select Vehicle No", Me.Text)
                '    txtLorryNo.Focus()
            Else


                Dim Qry As String = "select distinct TSPL_BOOKING_MATSER.Document_No as Code,CONVERT(VARCHAR(15),TSPL_BOOKING_MATSER.Document_Date,103) AS Date,TSPL_BOOKING_MATSER.location_code,TSPL_BOOKING_MATSER.Cust_Group_Code  " &
                               " from TSPL_BOOKING_MATSER LEFT OUTER JOIN  tspl_booking_detail ON TSPL_BOOKING_MATSER.Document_No=tspl_booking_detail.Document_No " &
                               " LEFT OUTER join TSPL_VEHICLE_MASTER ON  tspl_booking_detail.Vehicle_Code=TSPL_VEHICLE_MASTER.Vehicle_Id left outer join TSPL_CUSTOMER_MASTER ON tspl_booking_detail.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code  left outer join tspl_item_master on tspl_booking_detail.item_code=tspl_item_master.item_code " &
                               " where  tspl_booking_detail.foc_item<>1  AND  convert (date,TSPL_BOOKING_MATSER.Document_Date,103) >= convert(date, '" + clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") + "',103)  and  convert (date,TSPL_BOOKING_MATSER.Document_Date,103) <= convert(date, '" + clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") + "',103)   "

                If VehicleCodeNotMandatoryOnLoadINSlip = True Then
                    If clsCommon.myLen(txtLorryNo.Value) > 0 Then
                        Qry += " and tspl_booking_detail.Vehicle_Code='" & txtLorryNo.Value & "' "
                    End If
                Else
                    Qry += " and tspl_booking_detail.Vehicle_Code='" & txtLorryNo.Value & "' "
                End If
                If RouteCodeNotMandatoryOnLoadINSlip = True Then
                    If clsCommon.myLen(TxtRouteNo.Value) > 0 Then
                        Qry += " and tspl_booking_detail.Route_No='" & TxtRouteNo.Value & "' "
                    End If
                Else
                    Qry += " and tspl_booking_detail.Route_No='" & TxtRouteNo.Value & "' "
                End If

                Qry += " and not exists (select * from TSPL_MULTI_BOOKING_DETAIL where TSPL_MULTI_BOOKING_DETAIL.Booking_No =TSPL_BOOKING_MATSER.Document_No  ) "

                If VehicleCodeNotMandatoryOnLoadINSlip = True Then
                    If String.IsNullOrEmpty(clsDBFuncationality.getSingleValue(Qry)) Then
                        common.clsCommon.MyMessageBoxShow(Me, "No Booking found related to selected Date", Me.Text)
                        txtDate.Focus()
                        Exit Sub
                    End If
                End If

                'sanjay Ticket No-BHA/27/05/19-000897
                If String.IsNullOrEmpty(clsDBFuncationality.getSingleValue(Qry)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "No Booking found related to selected Vehicle No", Me.Text)
                    txtLorryNo.Focus()
                    Exit Sub
                End If
                'sanjay Ticket No-BHA/27/05/19-000897

                txtmultiBooking.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulBookingSel", Qry, "Code", "Code", txtmultiBooking.arrValueMember, txtmultiBooking.arrDispalyMember)

                If clsCommon.myLen(txtmultiBooking.arrValueMember) > 0 Then
                    'lblDODate.Text = clsCommon.myCDate(clsDBFuncationality.getSingleValue("Select TSPL_BOOKING_MATSER.Document_Date from TSPL_BOOKING_MATSER where Document_No ='" & fndDeliveryNo.Value & "'"), Nothing)

                    lblDODate.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select TSPL_BOOKING_MATSER.Document_Date from TSPL_BOOKING_MATSER where Document_No ='" & fndDeliveryNo.Value & "'"))
                    GetMultiBookingDetail(txtmultiBooking.arrValueMember, txtLorryNo.Value)
                Else
                    lblDODate.Text = ""
                    Throw New Exception("No record found.")
                End If

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndDemand__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndDemand._MYValidating
        Try
            Dim Qry As String = "select distinct TSPL_DEMAND_BOOKING_MASTER.Document_No  as Code,CONVERT(VARCHAR(15),TSPL_BOOKING_MATSER.Document_Date,103) AS Date,TSPL_BOOKING_MATSER.location_code  from TSPL_DEMAND_BOOKING_MASTER 
left outer join TSPL_BOOKING_MATSER on TSPL_BOOKING_MATSER.Against_DemandBooking_No=TSPL_DEMAND_BOOKING_MASTER.Document_No
LEFT OUTER JOIN  tspl_booking_detail ON TSPL_BOOKING_MATSER.Document_No=tspl_booking_detail.Document_No  LEFT OUTER join TSPL_VEHICLE_MASTER ON  tspl_booking_detail.Vehicle_Code=TSPL_VEHICLE_MASTER.Vehicle_Id left outer join TSPL_CUSTOMER_MASTER ON tspl_booking_detail.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code  left outer join tspl_item_master on tspl_booking_detail.item_code=tspl_item_master.item_code 
 left outer join TSPL_DEMAND_BOOKING_DETAIL on TSPL_DEMAND_BOOKING_DETAIL.Document_No=TSPL_DEMAND_BOOKING_MASTER.Document_No "

            Dim whrClas As String = " tspl_booking_detail.foc_item<>1 and TSPL_DEMAND_BOOKING_MASTER.Route_No='" & TxtRouteNo.Value & "' and 
                                      TSPL_DEMAND_BOOKING_DETAIL.ShiftType='" + ddlPTSShift.Text + "'  and not exists " &
                " (select TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code,TSPL_GATEPASS_DETAIL_DAIRYSALE.Item_Code,TSPL_GATEPASS_DETAIL_DAIRYSALE.Unit_code,TSPL_GATEPASS_MASTER_DAIRYSALE.Vehicle_Code from TSPL_GATEPASS_MASTER_DAIRYSALE inner join TSPL_GATEPASS_DETAIL_DAIRYSALE ON TSPL_GATEPASS_DETAIL_DAIRYSALE.Document_No= TSPL_GATEPASS_MASTER_DAIRYSALE.Document_No " &
                " where TSPL_GATEPASS_MASTER_DAIRYSALE.Delivery_Code=TSPL_BOOKING_MATSER.Document_No and TSPL_GATEPASS_DETAIL_DAIRYSALE.Item_Code=tspl_booking_detail.Item_Code and TSPL_GATEPASS_DETAIL_DAIRYSALE.Unit_code=tspl_booking_detail.Unit_code and TSPL_GATEPASS_MASTER_DAIRYSALE.Vehicle_Code=tspl_booking_detail.Vehicle_Code) "

            fndDemand.Value = clsCommon.ShowSelectForm("DSGatePass", Qry, "Code", whrClas, fndDemand.Value, "Code", isButtonClicked)
            If clsCommon.myLen(fndDemand.Value) > 0 Then
                lblDODate.Text = clsCommon.myCDate(clsDBFuncationality.getSingleValue("Select TSPL_DEMAND_BOOKING_MASTER.Document_Date from TSPL_DEMAND_BOOKING_MASTER where Document_No ='" & fndDemand.Value & "'"), Nothing)
                GetDemandDetail(fndDemand.Value, TxtRouteNo.Value)
            Else
                lblDODate.Text = ""
                Throw New Exception("No record found.")
            End If

        Catch ex As Exception

        End Try
    End Sub
    Sub GetDemandDetail(ByVal strBookingNo As String, ByVal StrvechilNo As String)
        LoadBlankGrid()
        Dim Qry As String = " select max(code) AS code,max(Date) as Date,ICode,max(IName) as IName,max(HSN_Code) as HSN_Code,sum(Qty) as Qty,Unit ,MAX(Loc_Code) AS Loc_Code,max(Vehicle_Code) as Vehicle_Code,foc_item,Scheme_Item  from (select  TSPL_DEMAND_BOOKING_MASTER.Document_No as code, CONVERT(VARCHAR(15) ,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103) AS Date,
 TSPL_DEMAND_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name,tspl_booking_detail.Loc_Code,TSPL_DEMAND_BOOKING_DETAIL.Item_Code as ICode , tspl_item_master.Item_Desc as IName ,tspl_item_master.HSN_Code,TSPL_DEMAND_BOOKING_DETAIL.Qty as Qty,TSPL_DEMAND_BOOKING_DETAIL.Unit_code as Unit, TSPL_DEMAND_BOOKING_DETAIL.Item_Rate  as Rate,TSPL_DEMAND_BOOKING_DETAIL.Vehicle_Code,tspl_booking_detail.foc_item ,tspl_booking_detail.Scheme_Item 
 from TSPL_DEMAND_BOOKING_MASTER
 left outer join TSPL_DEMAND_BOOKING_DETAIL on TSPL_DEMAND_BOOKING_DETAIL.Document_no=TSPL_DEMAND_BOOKING_MASTER.Document_No
 left outer join TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_DEMAND_BOOKING_DETAIL.Cust_Code
 left outer join tspl_item_master on tspl_item_master.item_code=TSPL_DEMAND_BOOKING_DETAIL.item_code
 left outer join tspl_booking_detail on tspl_booking_detail.Against_DemandBooking_No=TSPL_DEMAND_BOOKING_MASTER.Document_No "
        Qry += " where TSPL_DEMAND_BOOKING_MASTER.Document_No='" & strBookingNo & "'"
        'Dim Qry As String = " select max(code) AS code,max(Date) as Date,ICode,max(IName) as IName,max(HSN_Code) as HSN_Code,sum(Qty) as Qty,Unit ,MAX(Loc_Code) AS Loc_Code,max(Vehicle_Code) as Vehicle_Code,foc_item,Scheme_Item " &
        '    " from (select TSPL_BOOKING_MATSER.Document_No as Code,CONVERT(VARCHAR(15),TSPL_BOOKING_MATSER.Document_Date,103) AS Date ," &
        '  " tspl_booking_detail.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name,tspl_booking_detail.Loc_Code,tspl_booking_detail.Item_Code as ICode ," &
        '  " tspl_item_master.Item_Desc as IName ,tspl_item_master.HSN_Code,tspl_booking_detail.Booking_Qty as Qty,tspl_booking_detail.Unit_code as Unit," &
        '  " tspl_booking_detail.Item_Rate  as Rate,tspl_booking_detail.Vehicle_Code,tspl_booking_detail.foc_item ,tspl_booking_detail.Scheme_Item " &
        '  " from TSPL_BOOKING_MATSER LEFT OUTER JOIN  tspl_booking_detail ON TSPL_BOOKING_MATSER.Document_No=tspl_booking_detail.Document_No " &
        '  " LEFT OUTER join TSPL_VEHICLE_MASTER ON  tspl_booking_detail.Vehicle_Code=TSPL_VEHICLE_MASTER.Vehicle_Id left outer join TSPL_CUSTOMER_MASTER ON tspl_booking_detail.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code  left outer join tspl_item_master on tspl_booking_detail.item_code=tspl_item_master.item_code   "
        'Qry += " where tspl_booking_detail.Against_DemandBooking_No='" & strBookingNo & "'"
        'Qry += " where tspl_booking_detail.Document_No='" & strBookingNo & "' AND  tspl_booking_detail.Vehicle_Code='" + StrvechilNo + "'"
        Qry += " ) as Final  group by  final.ICode,final.Unit,final.Vehicle_Code ,Final.foc_item,Final.Scheme_Item "
        Dim dtAllData As DataTable = clsDBFuncationality.GetDataTable(Qry)
        If dtAllData Is Nothing OrElse dtAllData.Rows.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "No record found.", Me.Text)
            Me.Close()
        Else
            fndLocation.Value = clsCommon.myCstr(dtAllData.Rows(0)("Loc_Code"))
            lblLocationName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Location_Desc  FROM TSPL_LOCATION_MASTER WHERE Location_Code='" + fndLocation.Value + "'"))
            For Each dr As DataRow In dtAllData.Rows
                gv1.Rows.AddNew()
                gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = clsCommon.myCdbl(gv1.Rows.Count)
                gv1.Rows(gv1.Rows.Count - 1).Cells(colHCode).Value = clsCommon.myCstr(dr("Code"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colHDate).Value = clsCommon.myCstr(dr("Date"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = clsCommon.myCstr(dr("ICode"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = clsCommon.myCstr(dr("IName"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colhsncode).Value = clsCommon.myCstr(dr("HSN_Code"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colBookQty).Value = clsCommon.myCdbl(dr("Qty"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = clsCommon.myCstr(dr("Unit"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(ColFOC).Value = clsCommon.myCstr(dr("foc_item"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeableItem).Value = clsCommon.myCstr(dr("Scheme_Item"))
            Next
        End If
    End Sub
End Class
