' '' '' '' ''Created by Sanjeet 31/01/2018 ========
Imports common
Imports System.Data.SqlClient
Imports System
Imports System.Text.RegularExpressions

Public Class frmDairyGatePass
    Inherits FrmMainTranScreen
    'Sanjay Ticket No-MIL/08/07/19-000104 Add Salesman
#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim strQuery As String
    Dim strQueryCANCRate As String
    Dim dt As DataTable
    Private isNewEntry As Boolean = False
    Const ColApply As String = "ColApply"
    Const ColDocNo As String = "ColDocNo"
    Const ColDocDate As String = "ColDocDatet"
    Const ColToSalesmanCode As String = "ColToSalesmanCode"
    Const ColToSalesmanname As String = "ColToSalesmanname"
    Const ColRoute_No As String = "ColRoute_No"
    Const ColRoute_Desc As String = "ColRoute_Desc"
    Const ColType As String = "ColType"
    Const ColPriceCode As String = "ColPriceCode"
    Const ColPriceDesc As String = "ColPriceDesc"
    Const ColCustCode As String = "ColCustCode"
    Const ColCustName As String = "ColCustName"
    Dim isInsideLoadData As Boolean = False
    Dim blnLoad As Boolean = False
    Const colItemCode As String = "colItemCode"
    Const colItemDesc As String = "colItemDesc"
    Const colUnit As String = "colUnit"
    Const colQty As String = "colQty"
    Const colLineNo As String = "colLineNo"
    Const colPKID As String = "colPKID"
    Const colHSNCode As String = "colHSNCode"
    Dim atchqry As String = ""
    Dim AlternateVechileforGatePass As Double
    Dim isCreateProvisionOfTransporterInDairyDispatch As Boolean = False
    Dim SettCreateProvisionOnOpeningAndClosingKM As Boolean = False
    Dim IsLoadingSlipMandatory As Boolean = False
    Dim CreateGatePassFromDemand As Boolean = False
    Public arrShipmentFromMultiple As ArrayList
    Public Property routeno As String
    Public Property txtlocation As String
    Public Property vehicleno As String
    Public Property docdate As Date?
    ''ERO/03/05/19-000584 by balwindr on 06/05/2019
    ''ERO/03/05/19-000584 by balwindr on 06/05/2019
    Dim VehicleDesc As String = Nothing
    Dim OneTimeCheck As Boolean = False
#End Region

    Private Sub SetUserMgmtNew()
        Me.Form_ID = clsUserMgtCode.frmDairyGatePass
        MyBase.SetUserMgmt(clsUserMgtCode.frmDairyGatePass)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPrint.Visible = MyBase.isPrintFlag
        btnClKM.Visible = MyBase.isModifyFlag
    End Sub

    Private Sub FrmGatePassENtry1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CreateTable()
        'Dim coll As Dictionary(Of String, String)
        'coll = New Dictionary(Of String, String)()
        'coll.Add("Closing_Date", "Datetime null")
        'coll.Add("ShiftType", "varchar(20) NULL")
        'clsCommonFunctionality.CreateOrAlterTable("TSPL_DAIRYSALE_GATEPASS_MASTER", coll)

        'coll = New Dictionary(Of String, String)()
        'coll.Add("Production_Remarks", "varchar(200) NULL")
        'coll.Add("GPCode", "varchar(30) NULL")
        'clsCommonFunctionality.CreateOrAlterTable("TSPL_DEMAND_BOOKING_DETAIL", coll)

        SetUserMgmtNew()
        isNewEntry = True
        LoadBlankGrid()
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtSupplyDate.Value = txtDate.Value
        txtGatepassDate.Value = clsCommon.GETSERVERDATE()
        'CheckCreateCapacity = clsCommon.myCBool(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CrateCapacityCheck, clsFixedParameterCode.CrateCapacityCheck, Nothing)))
        isCreateProvisionOfTransporterInDairyDispatch = clsCommon.myCBool(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateProvisionOfTransporterInDairyDispatch, clsFixedParameterCode.CreateProvisionOfTransporterInDairyDispatch, Nothing)))
        IsLoadingSlipMandatory = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsLoadingSlipMandatory, clsFixedParameterCode.IsLoadingSlipMandatory, Nothing)) = 1, True, False)
        SettCreateProvisionOnOpeningAndClosingKM = (clsCommon.myCdbl(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateProvisionOnOpeningAndClosingKM, clsFixedParameterCode.CreateProvisionOnOpeningAndClosingKM, Nothing))) = 1)
        CreateGatePassFromDemand = clsCommon.myCBool(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateGatePassFromDemand, clsFixedParameterCode.CreateGatePassFromDemand, Nothing)))
        Panel2.Visible = SettCreateProvisionOnOpeningAndClosingKM
        cmbitemtype.Text = "Select"
        txtTransporter.MaxLength = 100
        txtSalesman.MaxLength = 100
        txtRemarks.MaxLength = 200
        txtComments.MaxLength = 200
        If isCreateProvisionOfTransporterInDairyDispatch = True Then
            btnPost.Visible = True
            btnPost.Enabled = False
        Else
            btnPost.Visible = False
        End If
        AlternateVechileforGatePass = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowAlternateVechileforFreshSale, clsFixedParameterCode.ShowAlternateVechileforFreshSale, Nothing))
        'funFillGrid()
        txtTransporter.Enabled = False
        If isCreateProvisionOfTransporterInDairyDispatch = True Then
            txtTransporter.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Transport_Id from TSPL_VEHICLE_MASTER where Vehicle_Id='" & txtVehicle.Value & "'"))
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
        txtmultiBooking.Enabled = False
        If IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TollTaxMaster, clsFixedParameterCode.TollTaxMaster, Nothing)) = 0, False, True) = False Then
            lblTollAmount.Visible = True
            txtTollAmount.Visible = True
        Else
            lblTollAmount.Visible = False
            txtTollAmount.Visible = False
        End If
        multipleDelteVisible(False)
        If CreateGatePassFromDemand = True Then
            RadGroupBox3.Visible = True
            txtLocCode.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
            If clsCommon.myLen(txtLocCode.Value) > 0 Then
                txtLocDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocCode.Value + "'"))
            End If
            txtLocCode.Enabled = False
            txtmultiBooking.Visible = False
        Else
            RadGroupBox3.Visible = True
        End If
        fndRouteNo.Value = routeno
        txtRouteName.Text = clsDBFuncationality.getSingleValue("select Route_Desc from TSPL_ROUTE_MASTER where Route_No='" & fndRouteNo.Value & "'")
        txtLocCode.Value = txtlocation
        txtLocDesc.Text = clsDBFuncationality.getSingleValue("select  Location_Desc  from TSPL_LOCATION_MASTER where Location_Code='" & txtLocCode.Value & "'")
        txtVehicle.Value = vehicleno
        lblVehicleDesc.Text = clsDBFuncationality.getSingleValue("select Description from TSPL_VEHICLE_MASTER where Vehicle_Id='" & txtVehicle.Value & "'")
        If docdate IsNot Nothing AndAlso clsCommon.myLen(docdate) > 0 Then
            txtDate.Value = docdate
        End If

        '  LoadData(txtCode.Value, NavigatorType.Current)

    End Sub

    Private Sub CreateTable()
        Dim coll As Dictionary(Of String, String)
        coll = New Dictionary(Of String, String)()
        coll.Add("GPCode", "Varchar(30) not null  PRIMARY KEY")
        coll.Add("GPDate", "datetime not NULL")
        coll.Add("Vehicle_Id", "varchar(12) NOT NULL")
        coll.Add("Vehicle_Number", "varchar(50) NOT NULL")
        coll.Add("Item_Type", "varchar(10) NULL")
        coll.Add("Comp_Code", "VARCHAR(8)")
        coll.Add("Created_By", "varchar(12) NOT NULL")
        coll.Add("Created_Date", "Datetime NOT NULL")
        coll.Add("Modified_By", "varchar(12) NOT NULL")
        coll.Add("Modified_Date", "Datetime NOT NULL")
        coll.Add("Transporter", "varchar(100) NULL")
        coll.Add("Remarks", "varchar(200) NULL")
        coll.Add("Comments", "varchar(200) NULL")
        coll.Add("Post", "char(1)  Not NUll Default 'N'")
        coll.Add("Location_Code", "varchar(12)  NULL")
        coll.Add("Location_Desc", "varchar(50)  NULL")
        coll.Add("Route_No", "varchar(12)  NULL")
        coll.Add("TotalCAN", "float  null")
        coll.Add("TotalCrate", "float  null")
        coll.Add("Opening_Km", "Decimal(18,2) null")
        coll.Add("Closing_Km", "Decimal(18,2) null")
        coll.Add("Distance_In_Route", "Decimal(18,2) null")
        coll.Add("Price_KM_In_Vehicle", "Decimal(18,2) null")
        coll.Add("Toll_Amount", "Decimal(18,2) null")
        coll.Add("Salesman", "varchar(100) NULL")
        coll.Add("Closing_Date", "Datetime null")
        coll.Add("IsTransfer", "int not null default 0")
        coll.Add("AgainstTransferNo", "Varchar(30) null References TSPL_TRANSFER_ORDER_HEAD(Document_No)")
        coll.Add("ShiftType", "varchar(20) NULL")
        coll.Add("Loading_Slip", "varchar(20) NULL")
        coll.Add("GatePass_Date", "datetime NULL")
        coll.Add("Status", "char(1)  NUll")
        coll.Add("Driver_Name", "varchar(100) NULL")
        coll.Add("Driver_ContactNo", "varchar(15) NULL")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_DAIRYSALE_GATEPASS_MASTER", coll, Nothing, True, False, "", "GPCode", "GPDate")
        AlterShipmentTable()
        CreateBalancingTable()
    End Sub

    Private Sub AlterShipmentTable()
        Dim coll As Dictionary(Of String, String)
        coll = New Dictionary(Of String, String)()
        coll.Add("PK_ID", "integer NOT NULL identity NOT FOR REPLICATION primary key")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_SD_SHIPMENT_DETAIL", coll, Nothing, True, True, "TSPL_SD_SHIPMENT_HEAD", "DOCUMENT_CODE", "")
    End Sub

    Private Sub CreateBalancingTable()
        Dim coll As Dictionary(Of String, String)
        coll = New Dictionary(Of String, String)()
        coll.Add("PK_ID", "integer NOT NULL REFERENCES TSPL_SD_SHIPMENT_DETAIL(PK_ID)")
        coll.Add("GPCode", "Varchar(30) NOT NULL REFERENCES TSPL_DAIRYSALE_GATEPASS_MASTER(GPCode)")
        coll.Add("Item_Code", "Varchar(50) NULL")
        coll.Add("Unit_Code", "Varchar(12) NULL")
        coll.Add("GP_Qty", "Decimal(18,2) NULL")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL", coll, Nothing, True, False, "TSPL_DAIRYSALE_GATEPASS_MASTER", "GPCode", "")
        '=====================
    End Sub

    Private Sub LoadBlankGrid()
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()

        Gv1.AllowDeleteRow = True
        Gv1.AllowAddNewRow = False

        Dim repoPK_ID As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoPK_ID = New GridViewDecimalColumn()
        repoPK_ID.FormatString = ""
        repoPK_ID.HeaderText = "ID"
        repoPK_ID.Name = colPKID
        repoPK_ID.Width = 50
        repoPK_ID.ReadOnly = True
        repoPK_ID.IsVisible = False
        repoPK_ID.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Gv1.MasterTemplate.Columns.Add(repoPK_ID)

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Gv1.MasterTemplate.Columns.Add(repoLineNo)

        Dim ItemCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ItemCode.FormatString = ""
        ItemCode.HeaderText = "Item Code"
        ItemCode.Name = colItemCode
        ItemCode.Width = 100
        ItemCode.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(ItemCode)

        Dim ItemDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ItemDesc.FormatString = ""
        ItemDesc.HeaderText = "Item Desc"
        ItemDesc.Name = colItemDesc
        ItemDesc.Width = 100
        ItemDesc.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(ItemDesc)

        Dim Unit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Unit.FormatString = ""
        Unit.HeaderText = "Unit"
        Unit.Name = colUnit
        Unit.Width = 100
        Unit.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(Unit)

        Dim HSN As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        HSN.FormatString = ""
        HSN.HeaderText = "HSN Code"
        HSN.Name = colHSNCode
        HSN.Width = 100
        HSN.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(HSN)

        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Quantity"
        repoQty.Name = colQty
        repoQty.Width = 80
        repoQty.Minimum = 0
        If clsCommon.myCstr(txtCode.Value) IsNot Nothing AndAlso clsCommon.myLen(txtCode.Value) > 0 Then
            repoQty.ReadOnly = True
        Else
            repoQty.ReadOnly = False
        End If
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Gv1.MasterTemplate.Columns.Add(repoQty)


        Gv1.ShowGroupPanel = False
        Gv1.AllowColumnReorder = False
        Gv1.AllowRowReorder = False
        Gv1.EnableSorting = False
        Gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        Gv1.AllowAddNewRow = False
    End Sub

    Private Function LoadQuery(ByVal strItemCode As String) As String
        Dim strItem As String = String.Empty
        If CreateGatePassFromDemand = True Then
            If clsCommon.myLen(strItemCode) > 0 Then
                strItem = " and TSPL_DEMAND_BOOKING_DETAIL.Item_Code='" & strItemCode & "'"
            End If
            strQuery = "select TSPL_DEMAND_BOOKING_MASTER.Document_No as [Document No],Document_Date as [Document Date],TSPL_DEMAND_BOOKING_DETAIL.Cust_Code,Customer_Name, " &
                       "TSPL_DEMAND_BOOKING_DETAIL.Item_Code as [Item Code],Item_Desc as [Item Desc],TSPL_DEMAND_BOOKING_DETAIL.Unit_code as Unit,Qty,TSPL_ITEM_MASTER.HSN_Code  " &
                       "from TSPL_DEMAND_BOOKING_MASTER left outer join TSPL_DEMAND_BOOKING_DETAIL on TSPL_DEMAND_BOOKING_MASTER.Document_no=TSPL_DEMAND_BOOKING_DETAIL.DOCUMENT_no " &
                       "left outer join TSPL_ITEM_MASTER on TSPL_DEMAND_BOOKING_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code " &
                       "left outer join TSPL_CUSTOMER_MASTER on TSPL_DEMAND_BOOKING_DETAIL.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code  " &
                       "where convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)='" & clsCommon.GetPrintDate(txtSupplyDate.Value, "dd/MMM/yyyy") & " ' And isnull(GPCode,'') = '' and TSPL_DEMAND_BOOKING_DETAIL.ShiftType='" & IIf(rbtnMorning.IsChecked = True, "Morning", "Evening") & "' And " &
                       "TSPL_DEMAND_BOOKING_MASTER.Location_Code='" & txtLocCode.Value & "' and TSPL_DEMAND_BOOKING_DETAIL.Vehicle_Code='" + txtVehicle.Value + "' and TSPL_DEMAND_BOOKING_DETAIL.Item_Code <> '' " & strItem & " "
            If clsCommon.myLen(fndRouteNo.Value) > 0 Then
                strQuery += "  and TSPL_DEMAND_BOOKING_MASTER.route_no='" + fndRouteNo.Value + "'"
            End If
        Else
            If clsCommon.myLen(strItemCode) > 0 Then
                strItem = " and TSPL_SD_SHIPMENT_DETAIL.Item_Code='" & strItemCode & "'"
            End If
            If AlternateVechileforGatePass.Equals(1) Then
                Dim StrChkAvQuery As String = "select TSPL_SD_SHIPMENT_HEAD.Document_Code as [Document No],Document_Date as [Document Date],Customer_Code,Customer_Name, " &
                    "TSPL_SD_SHIPMENT_DETAIL.Item_Code as [Item Code],Item_Desc as [Item Desc],TSPL_SD_SHIPMENT_DETAIL.Unit_code as Unit,Qty,TSPL_ITEM_MASTER.HSN_Code  " &
                    "from tspl_sd_shipment_head left outer join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE " &
                    "left outer join TSPL_ITEM_MASTER on TSPL_SD_SHIPMENT_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code " &
                    "left outer join TSPL_CUSTOMER_MASTER on TSPL_SD_SHIPMENT_HEAD.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code  " &
                    "where convert(date,Document_Date,103)='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & " ' And   isnull(GPCode,'') = '' and " &
                    "TSPL_SD_SHIPMENT_HEAD.Bill_To_Location='" & txtLocCode.Value & "'  and TSPL_SD_SHIPMENT_HEAD.AlternateVehicle='" + txtVehicle.Value + "'   and TSPL_SD_SHIPMENT_DETAIL.Item_Code <> '' " & strItem & "  "
                If clsCommon.myLen(fndRouteNo.Value) > 0 Then
                    StrChkAvQuery += "  and TSPL_SD_SHIPMENT_HEAD.route_no='" + fndRouteNo.Value + "'"
                End If
                dt = New DataTable()
                dt = clsDBFuncationality.GetDataTable(StrChkAvQuery)
                If dt.Rows.Count > 0 Then

                    strQuery = "select TSPL_SD_SHIPMENT_HEAD.Document_Code as [Document No],Document_Date as [Document Date],Customer_Code,Customer_Name, " &
                        "TSPL_SD_SHIPMENT_DETAIL.Item_Code as [Item Code],Item_Desc as [Item Desc],TSPL_SD_SHIPMENT_DETAIL.Unit_code as Unit,Qty,TSPL_ITEM_MASTER.HSN_Code  " &
                        "from tspl_sd_shipment_head left outer join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE " &
                        "left outer join TSPL_ITEM_MASTER on TSPL_SD_SHIPMENT_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code " &
                        "left outer join TSPL_CUSTOMER_MASTER on TSPL_SD_SHIPMENT_HEAD.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code  " &
                        "where convert(date,Document_Date,103)='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & " ' And   isnull(GPCode,'') = '' and " &
                        "TSPL_SD_SHIPMENT_HEAD.Bill_To_Location='" & txtLocCode.Value & "' and TSPL_SD_SHIPMENT_HEAD.AlternateVehicle='" + txtVehicle.Value + "' and TSPL_SD_SHIPMENT_DETAIL.Item_Code <> '' " & strItem & "  "

                    If clsCommon.myLen(fndRouteNo.Value) > 0 Then
                        strQuery += "  and TSPL_SD_SHIPMENT_HEAD.route_no='" + fndRouteNo.Value + "'"
                    End If


                Else
                    strQuery = "select TSPL_SD_SHIPMENT_HEAD.Document_Code as [Document No],Document_Date as [Document Date],Customer_Code,Customer_Name, " &
                       "TSPL_SD_SHIPMENT_DETAIL.Item_Code as [Item Code],Item_Desc as [Item Desc],TSPL_SD_SHIPMENT_DETAIL.Unit_code as Unit,Qty,TSPL_ITEM_MASTER.HSN_Code  " &
                       "from tspl_sd_shipment_head left outer join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE " &
                       "left outer join TSPL_ITEM_MASTER on TSPL_SD_SHIPMENT_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code " &
                       "left outer join TSPL_CUSTOMER_MASTER on TSPL_SD_SHIPMENT_HEAD.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code  " &
                       "where convert(date,Document_Date,103)='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & " ' And   isnull(GPCode,'') = '' and " &
                       "TSPL_SD_SHIPMENT_HEAD.Bill_To_Location='" & txtLocCode.Value & "' and TSPL_SD_SHIPMENT_HEAD.Vehicle_Code='" + txtVehicle.Value + "' and TSPL_SD_SHIPMENT_DETAIL.Item_Code <> '' " & strItem & " "
                    If clsCommon.myLen(fndRouteNo.Value) > 0 Then
                        strQuery += "  and TSPL_SD_SHIPMENT_HEAD.route_no='" + fndRouteNo.Value + "'"
                    End If
                End If
            Else
                strQuery = "select TSPL_SD_SHIPMENT_DETAIL.PK_ID,TSPL_SD_SHIPMENT_HEAD.Document_Code as [Document No],Document_Date as [Document Date],Customer_Code,Customer_Name, " &
                      "TSPL_SD_SHIPMENT_DETAIL.Item_Code as [Item Code],Item_Desc as [Item Desc],TSPL_SD_SHIPMENT_DETAIL.Unit_code as Unit,Qty,TSPL_ITEM_MASTER.HSN_Code  " &
                      "from tspl_sd_shipment_head left outer join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE " &
                      "left outer join TSPL_ITEM_MASTER on TSPL_SD_SHIPMENT_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code " &
                      "left outer join TSPL_CUSTOMER_MASTER on TSPL_SD_SHIPMENT_HEAD.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code  " &
                      "where convert(date,Document_Date,103)='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & " ' And   isnull(GPCode,'') = '' and " &
                      "TSPL_SD_SHIPMENT_HEAD.Bill_To_Location='" & txtLocCode.Value & "'  and (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)='" + txtVehicle.Value + "'  and TSPL_SD_SHIPMENT_DETAIL.Item_Code <> '' " & strItem & "  "


                If clsCommon.myLen(fndRouteNo.Value) > 0 Then
                    strQuery += "  and TSPL_SD_SHIPMENT_HEAD.route_no='" + fndRouteNo.Value + "'"
                End If
                If rbtnMorning.IsChecked Then
                    strQuery += "  and TSPL_SD_SHIPMENT_HEAD.Shift_Type='AM'"
                Else
                    strQuery += "  and TSPL_SD_SHIPMENT_HEAD.Shift_Type='PM'"
                End If
                strQueryCANCRate = "select sum(crate) as crate ,sum(ShippedCAN) as ShippedCAN from TSPL_SD_SHIPMENT_HEAD " &
                                "left outer join TSPL_CUSTOMER_MASTER on TSPL_SD_SHIPMENT_HEAD.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code  " &
                       "where convert(date,Document_Date,103)='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & " ' And   isnull(GPCode,'') = '' and " &
                       "TSPL_SD_SHIPMENT_HEAD.Bill_To_Location='" & txtLocCode.Value & "' and (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)='" + txtVehicle.Value + "' "
                If txtmultiBooking.arrValueMember IsNot Nothing AndAlso txtmultiBooking.arrValueMember.Count > 0 Then
                    strQueryCANCRate += " and TSPL_SD_SHIPMENT_HEAD.document_code in (" + clsCommon.GetMulcallString(txtmultiBooking.arrValueMember) + ")"
                End If
                If clsCommon.myLen(fndRouteNo.Value) > 0 Then
                    strQueryCANCRate += "  and TSPL_SD_SHIPMENT_HEAD.route_no='" + fndRouteNo.Value + "'"
                End If
                'dt = New DataTable()
                'dt = clsDBFuncationality.GetDataTable(strQueryCANCRate)
                'If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                '    txtCanQty.Text = clsCommon.myCdbl(dt.Rows(0)("ShippedCAN"))
                '    txtCrateQty.Text = clsCommon.myCdbl(dt.Rows(0)("crate"))
                'End If
            End If
        End If

        Return strQuery
    End Function

    '===================Added by preeti gupta against ticket no[BHA/17/08/18-000444] ========================
    Private Function LoadDoubleClickQuery(ByVal strItemCode As String) As String
        Dim strItem As String = String.Empty
        If clsCommon.myLen(strItemCode) > 0 Then
            strItem = " and TSPL_SD_SHIPMENT_DETAIL.Item_Code='" & strItemCode & "'"
        End If

        If AlternateVechileforGatePass.Equals(1) Then

            Dim StrChkAvQuery As String = "select '" & clsUserMgtCode.frmSaleDispatchDairy & "' as [Trans Type],TSPL_SD_SHIPMENT_HEAD.Document_Code as DocNo,Document_Date as [Document Date],Customer_Code,Customer_Name, " &
                "TSPL_SD_SHIPMENT_DETAIL.Item_Code as [Item Code],Item_Desc as [Item Desc],TSPL_SD_SHIPMENT_DETAIL.Unit_code as Unit,Qty,TSPL_ITEM_MASTER.HSN_Code  " &
                "from tspl_sd_shipment_head left outer join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE " &
                "left outer join TSPL_ITEM_MASTER on TSPL_SD_SHIPMENT_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code " &
                "left outer join TSPL_CUSTOMER_MASTER on TSPL_SD_SHIPMENT_HEAD.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code  " &
                "where convert(date,Document_Date,103)='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & " ' And   GPCode ='" + txtCode.Value + "' and " &
                "TSPL_SD_SHIPMENT_HEAD.Bill_To_Location='" & txtLocCode.Value & "'  and TSPL_SD_SHIPMENT_HEAD.AlternateVehicle='" + txtVehicle.Value + "'   and TSPL_SD_SHIPMENT_DETAIL.Item_Code <> '' " & strItem & "  "

            If clsCommon.myLen(fndRouteNo.Value) > 0 Then
                StrChkAvQuery += "  and TSPL_SD_SHIPMENT_HEAD.route_no='" + fndRouteNo.Value + "'"
            End If
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(StrChkAvQuery)
            If dt.Rows.Count > 0 Then

                strQuery = "select '" & clsUserMgtCode.frmSaleDispatchDairy & "' as [Trans Type], TSPL_SD_SHIPMENT_HEAD.Document_Code as [DocNo],Document_Date as [Document Date],Customer_Code,Customer_Name, " &
                    "TSPL_SD_SHIPMENT_DETAIL.Item_Code as [Item Code],Item_Desc as [Item Desc],TSPL_SD_SHIPMENT_DETAIL.Unit_code as Unit,Qty,TSPL_ITEM_MASTER.HSN_Code  " &
                    "from tspl_sd_shipment_head left outer join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE " &
                    "left outer join TSPL_ITEM_MASTER on TSPL_SD_SHIPMENT_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code " &
                    "left outer join TSPL_CUSTOMER_MASTER on TSPL_SD_SHIPMENT_HEAD.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code  " &
                    "where convert(date,Document_Date,103)='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & " ' And   GPCode ='" + txtCode.Value + "' and " &
                    "TSPL_SD_SHIPMENT_HEAD.Bill_To_Location='" & txtLocCode.Value & "' and TSPL_SD_SHIPMENT_HEAD.AlternateVehicle='" + txtVehicle.Value + "' and TSPL_SD_SHIPMENT_DETAIL.Item_Code <> '' " & strItem & "  "

                If clsCommon.myLen(fndRouteNo.Value) > 0 Then
                    strQuery += "  and TSPL_SD_SHIPMENT_HEAD.route_no='" + fndRouteNo.Value + "'"
                End If


            Else
                strQuery = "select '" & clsUserMgtCode.frmSaleDispatchDairy & "' as [Trans Type], TSPL_SD_SHIPMENT_HEAD.Document_Code as [DocNo],Document_Date as [Document Date],Customer_Code,Customer_Name, " &
                   "TSPL_SD_SHIPMENT_DETAIL.Item_Code as [Item Code],Item_Desc as [Item Desc],TSPL_SD_SHIPMENT_DETAIL.Unit_code as Unit,Qty,TSPL_ITEM_MASTER.HSN_Code  " &
                   "from tspl_sd_shipment_head left outer join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE " &
                   "left outer join TSPL_ITEM_MASTER on TSPL_SD_SHIPMENT_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code " &
                   "left outer join TSPL_CUSTOMER_MASTER on TSPL_SD_SHIPMENT_HEAD.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code  " &
                   "where convert(date,Document_Date,103)='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & " ' And    GPCode ='" + txtCode.Value + "' and " &
                   "TSPL_SD_SHIPMENT_HEAD.Bill_To_Location='" & txtLocCode.Value & "' and TSPL_SD_SHIPMENT_HEAD.Vehicle_Code='" + txtVehicle.Value + "' and TSPL_SD_SHIPMENT_DETAIL.Item_Code <> '' " & strItem & " "

                If clsCommon.myLen(fndRouteNo.Value) > 0 Then
                    strQuery += "  and TSPL_SD_SHIPMENT_HEAD.route_no='" + fndRouteNo.Value + "'"
                End If
            End If
        Else
            strQuery = "select '" & clsUserMgtCode.frmSaleDispatchDairy & "' as [Trans Type],TSPL_SD_SHIPMENT_HEAD.Document_Code as [DocNo],Document_Date as [Document Date],Customer_Code,Customer_Name, " &
                    "TSPL_SD_SHIPMENT_DETAIL.Item_Code as [Item Code],Item_Desc as [Item Desc],TSPL_SD_SHIPMENT_DETAIL.Unit_code as Unit,Qty,(Case When UsedGPDetail.GPUsedQty>0 Then (Qty-UsedGPDetail.GPUsedQty) Else Qty End) As [Balance Qty],TSPL_ITEM_MASTER.HSN_Code  " &
                    "from tspl_sd_shipment_head left outer join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE " &
                    "left outer join TSPL_ITEM_MASTER on TSPL_SD_SHIPMENT_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code " &
                    "left outer join TSPL_CUSTOMER_MASTER on TSPL_SD_SHIPMENT_HEAD.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code  " &
                    " left outer Join (Select PK_ID,	Max(GPCode)GPCode,	Item_Code,	Unit_Code,	Sum(IsNull(GP_Qty,0))GPUsedQty from TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL Group By PK_ID,Item_Code,Unit_Code)UsedGPDetail On UsedGPDetail.PK_ID=TSPL_SD_SHIPMENT_DETAIL.PK_ID " &
                    "where convert(date,Document_Date,103)='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & " ' " & 'and TSPL_SD_SHIPMENT_DETAIL.PK_ID='" + clsCommon.myCstr(Gv1.CurrentRow.Cells(colPKID).Value) + "'" &'GPCode ='" + txtCode.Value + "' and " &
                    " and TSPL_SD_SHIPMENT_HEAD.Bill_To_Location='" & txtLocCode.Value & "'  and TSPL_SD_SHIPMENT_HEAD.Vehicle_Code='" + txtVehicle.Value + "'  and TSPL_SD_SHIPMENT_DETAIL.Item_Code <> '' " '" & strItem & "  "

            If clsCommon.myLen(fndRouteNo.Value) > 0 Then
                strQuery += "  and TSPL_SD_SHIPMENT_HEAD.route_no='" + fndRouteNo.Value + "'"
            End If
        End If

        Return strQuery
    End Function
    '=================================================================

    Private Function GetQuery(ByVal strItemCode As String, ByVal strGPCode As String) As String
        Dim strItem As String = String.Empty
        If clsCommon.myLen(strItemCode) > 0 Then
            strItem = " and TSPL_SD_SHIPMENT_DETAIL.Item_Code='" & strItemCode & "'"
        End If

        strQuery = "select TSPL_DAIRYSALE_GATEPASS_MASTER.GPCode as [Document No],GPDate as [Document Date],'' as Customer_Code,'' as Customer_Name, " &
            "TSPL_DAIRYSALE_GATEPASS_DETAIL.Item_Code as [Item Code],Item_Desc as [Item Desc],TSPL_DAIRYSALE_GATEPASS_DETAIL.Unit_code as Unit,Qty,TSPL_DAIRYSALE_GATEPASS_DETAIL.HSN_Code  " &
            "from TSPL_DAIRYSALE_GATEPASS_MASTER left outer join TSPL_DAIRYSALE_GATEPASS_DETAIL on TSPL_DAIRYSALE_GATEPASS_MASTER.GPCode=TSPL_DAIRYSALE_GATEPASS_DETAIL.GPCode  " &
            "left outer join TSPL_ITEM_MASTER on TSPL_DAIRYSALE_GATEPASS_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code   " &
            "where TSPL_DAIRYSALE_GATEPASS_MASTER.GPCode='" & strGPCode & "' "
        Return strQuery
    End Function

    Private Sub funFillGrid()
        Try
            LoadBlankGrid()
            Dim totalCrate As Integer = 0
            Dim totalCan As Integer = 0
            Dim qry As String = LoadQuery("")
            If arrShipmentFromMultiple IsNot Nothing AndAlso arrShipmentFromMultiple.Count > 0 Then
                qry += " and TSPL_SD_SHIPMENT_HEAD.Document_Code in (" + clsCommon.GetMulcallString(arrShipmentFromMultiple) + ") "
            End If

            strQuery = "Select xxfinal.PK_ID,xxfinal.[Item Code],max(xxfinal.[Item Desc]) as [Item Desc],xxfinal.Unit,xxfinal.Quantity,Case When Sum(TSPL_DAIRYSALE_GATEPASS_Shipment_DETAIL.GP_Qty)>0 Then IsNull(((xxfinal.Quantity)-Sum(TSPL_DAIRYSALE_GATEPASS_Shipment_DETAIL.GP_Qty)),0) Else xxfinal.Quantity End As BalanceQty,max(xxfinal.HSN_Code) as HSN_Code,max(xxfinal.Customer_Name) as Customer_Name from (select PK_ID,[Item Code],max([Item Desc]) as [Item Desc],Unit,sum(qty) as Quantity,max(HSN_Code) as HSN_Code, max(Customer_Name) as Customer_Name from ( " & qry & " ) final  group by [Item Code],Unit,PK_ID )xxfinal
                        left Outer Join TSPL_DAIRYSALE_GATEPASS_Shipment_DETAIL ON TSPL_DAIRYSALE_GATEPASS_Shipment_DETAIL.PK_ID=xxfinal.PK_ID and TSPL_DAIRYSALE_GATEPASS_Shipment_DETAIL.Item_Code=xxfinal.[Item Code]
                        Group By xxfinal.PK_ID,xxfinal.[Item Code],xxfinal.Unit,xxfinal.Quantity Having (Case When Sum(TSPL_DAIRYSALE_GATEPASS_Shipment_DETAIL.GP_Qty)>0 Then IsNull(((xxfinal.Quantity)-Sum(TSPL_DAIRYSALE_GATEPASS_Shipment_DETAIL.GP_Qty)),0) Else xxfinal.Quantity End )>0 Order By PK_ID"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(strQuery)
            Dim intLineNo As Integer = 0
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    If clsCommon.myCDecimal(dr("BalanceQty")) > 0 Then
                        Gv1.Rows.AddNew()
                        intLineNo += 1
                        txtDistributorName.Text = clsCommon.myCstr(dr("Customer_Name"))
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colPKID).Value = clsCommon.myCDecimal(dr("PK_ID"))
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colPKID).Tag = clsCommon.myCDecimal(dr("PK_ID"))
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colLineNo).Value = intLineNo
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colItemCode).Value = clsCommon.myCstr(dr("Item Code"))
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colItemDesc).Value = clsCommon.myCstr(dr("Item Desc"))
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colUnit).Value = clsCommon.myCstr(dr("Unit"))
                        If clsCommon.myCDecimal(dr("BalanceQty")) > 0 Then
                            Gv1.Rows(Gv1.Rows.Count - 1).Cells(colQty).Value = clsCommon.myCDecimal(dr("BalanceQty"))
                        Else
                            Gv1.Rows(Gv1.Rows.Count - 1).Cells(colQty).Value = clsCommon.myCDecimal(dr("Quantity"))
                        End If
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colHSNCode).Value = clsCommon.myCstr(dr("HSN_Code"))
                        If clsCommon.CompairString(Gv1.Rows(Gv1.Rows.Count - 1).Cells(colUnit).Value, "Crate") = CompairStringResult.Equal Then
                            totalCrate += clsCommon.myCdbl(Gv1.Rows(Gv1.Rows.Count - 1).Cells(colQty).Value)
                        End If
                        If clsCommon.CompairString(Gv1.Rows(Gv1.Rows.Count - 1).Cells(colUnit).Value, "Can") = CompairStringResult.Equal Then
                            totalCan += clsCommon.myCdbl(Gv1.Rows(Gv1.Rows.Count - 1).Cells(colQty).Value)

                        End If
                        txtDate.Enabled = False
                    End If
                Next
                ' **************************************************************************************************
                txtmultiBooking.Enabled = True
                'Dim strAllDoc As String = " select STUFF((SELECT ',' + Document_Code from (select distinct PPPP.Document_Code from  ( " & qry & "    ) As PPPP  ) Final FOR XML PATH('')), 1, 1, '') "
                Dim strAllDoc As String = " select distinct PPPP.[Document No] from  ( " & qry & "    ) As PPPP   "
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(strAllDoc)
                Dim list As New ArrayList
                If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                    For Each dr As DataRow In dt1.Rows
                        list.Add(dr("Document No"))
                    Next
                End If
                txtmultiBooking.arrValueMember = list
                txtCrateQty.Text = totalCrate
                txtCanQty.Text = totalCan
            Else
                clsCommon.MyMessageBoxShow(Me, "Data Not Found.", Me.Text)
                ' **************************************************************************************************
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "GatePass Entry", MessageBoxButtons.OK, Me.Text)
        End Try
    End Sub

    Private Sub funLoadGrid(ByVal strGPCOde As String)
        Try
            LoadBlankGrid()
            Dim qry As String = GetQuery("", strGPCOde)
            Dim totalCrate As Integer = 0
            Dim totalCan As Integer = 0
            'strQuery = "select [Item Code],max([Item Desc]) as [Item Desc],Unit,sum(qty) as Quantity,max(HSN_Code) as HSN_Code from ( " & qry & " ) final group by [Item Code],Unit "

            'strQuery = "select TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL.PK_ID,[Item Code],max([Item Desc]) as [Item Desc],Unit,sum(qty) as Quantity,max(HSN_Code) as HSN_Code from ( " & qry & " ) final
            '            Left Outer Join TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL On TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL.GPCode=final.[Document No] and TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL.Item_Code=final.[Item Code]
            '            group by [Item Code],Unit,TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL.PK_ID "

            strQuery = " select TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL.PK_ID,TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL.Item_Code,max([Item_Desc]) as [Item Desc],TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL.Unit_Code as Unit,CONVERT(INT, MAX(GP_Qty)) Quantity ,max(TSPL_DAIRYSALE_GATEPASS_DETAIL.HSN_Code)HSN_Code   from TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL
                        left outer join TSPL_DAIRYSALE_GATEPASS_DETAIL on TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL.GPCode=TSPL_DAIRYSALE_GATEPASS_DETAIL.GPCode  
						left outer join TSPL_ITEM_MASTER on TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code   
						where TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL.GPCode= '" & strGPCOde & "'  
						group by TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL.Item_Code,TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL.Unit_Code,TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL.PK_ID "

            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(strQuery)
            Dim intLineNo As Integer = 0
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    Gv1.Rows.AddNew()
                    intLineNo += 1
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colPKID).Value = clsCommon.myCDecimal(dr("PK_ID"))
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colLineNo).Value = intLineNo
                    'Gv1.Rows(Gv1.Rows.Count - 1).Cells(colItemCode).Value = clsCommon.myCstr(dr("Item Code"))
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colItemCode).Value = clsCommon.myCstr(dr("Item_Code"))
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colItemDesc).Value = clsCommon.myCstr(dr("Item Desc"))
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colUnit).Value = clsCommon.myCstr(dr("Unit"))
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colQty).Value = clsCommon.myCstr(dr("Quantity"))
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colHSNCode).Value = clsCommon.myCstr(dr("HSN_Code"))
                    If clsCommon.CompairString(Gv1.Rows(Gv1.Rows.Count - 1).Cells(colUnit).Value, "Crate") = CompairStringResult.Equal Then
                        totalCrate += clsCommon.myCdbl(Gv1.Rows(Gv1.Rows.Count - 1).Cells(colQty).Value)
                    End If
                    If clsCommon.CompairString(Gv1.Rows(Gv1.Rows.Count - 1).Cells(colUnit).Value, "Can") = CompairStringResult.Equal Then
                        totalCan += clsCommon.myCdbl(Gv1.Rows(Gv1.Rows.Count - 1).Cells(colQty).Value)

                    End If
                    txtDate.Enabled = False

                Next
                txtCrateQty.Text = totalCrate
                txtCanQty.Text = totalCan
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "GatePass Entry", MessageBoxButtons.OK, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            btnPost.Visible = True
            btnSave.Enabled = True
            btnPost.Enabled = True
            isInsideLoadData = True
            Addnew()
            LoadBlankGrid()

            Dim obj As New clsDairyGatePassEntry()
            obj = clsDairyGatePassEntry.GetData(strCode, NavTyep, "FS")
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.GPCode) > 0) Then
                isNewEntry = False
                If clsCommon.myLen(obj.GPCode) > 0 Then
                    btnGo.Enabled = False
                    RadGroupBox3.Enabled = False
                Else
                    RadGroupBox3.Enabled = True
                    btnGo.Enabled = True
                End If
                btnSave.Text = "Update"
                If isCreateProvisionOfTransporterInDairyDispatch = True Then
                    If obj.Post = "Y" Then
                        btnSave.Enabled = False
                        btnPost.Enabled = False
                    Else
                        btnSave.Enabled = True
                        btnPost.Enabled = True
                    End If
                Else
                    btnSave.Enabled = True
                    btnPost.Enabled = True
                    btnPost.Visible = False
                End If

                If obj.Post = "Y" Then
                    UsLock1.Status = ERPTransactionStatus.Approved
                    btnDelete.Enabled = False
                    btnPrint.Enabled = True
                Else
                    If obj.Status = "Y" Then
                        UsLock1.Status = ERPTransactionStatus.Cancel
                        btnSave.Enabled = False
                        btnDelete.Enabled = False
                        btnPrint.Enabled = False
                        btnPost.Enabled = False
                    Else
                        UsLock1.Status = ERPTransactionStatus.Pending
                        btnDelete.Enabled = True
                        btnPrint.Enabled = True
                        btnPost.Enabled = True
                        btnSave.Enabled = True
                    End If
                End If

                txtCode.Value = obj.GPCode
                txtVehicle.Value = obj.Vehicle_Id
                lblVehicleDesc.Text = obj.Vehicle_Number
                cmbitemtype.Text = obj.Item_Type
                txtTransporter.Text = obj.Transporter
                txtSalesman.Text = obj.Salesman
                txtComments.Text = obj.Comments
                txtRemarks.Text = obj.Remarks
                isInsideLoadData = True
                txtDate.Value = obj.GPDate
                txtGatepassDate.Value = obj.GatePassDate
                txtLocCode.Value = obj.Location_Code
                txtLocDesc.Text = obj.Location_Desc
                If obj.Supply_Date IsNot Nothing Then
                    txtSupplyDate.Value = obj.Supply_Date
                End If
                '===============Added by preeti Gupta Against ticket no[BHA/17/08/18-000444]
                fndRouteNo.Value = obj.Route_No
                txtRouteName.Text = obj.Route_Desc
                txtCanQty.Text = obj.TotalCAN
                txtCrateQty.Text = obj.TotalCrate
                'txtmultiBooking.arrValueMember = Nothing
                txtmultiBooking.Enabled = False
                'btnSave.Enabled = False

                '=========================================================
                txtOpKM.Value = obj.Opening_Km
                txtClKM.Value = obj.Closing_Km
                txtTollAmount.Text = clsCommon.myCdbl(obj.Toll_Amount)
                lblClosingDate.Text = clsCommon.myCstr(obj.Closing_Date)
                If clsCommon.CompairString(obj.IsTransfer, "1") = CompairStringResult.Equal Then
                    chkAgainstTransfer.Checked = True
                    FndTransferNo.Value = obj.AgainstTransferNo
                End If

                If clsCommon.myCdbl(obj.Closing_Km) > 0 Then
                    btnClKM.Enabled = False
                Else
                    btnClKM.Enabled = True
                End If

                If clsCommon.CompairString(obj.ShiftType, "Morning") = CompairStringResult.Equal Then
                    rbtnMorning.IsChecked = True
                ElseIf clsCommon.CompairString(obj.ShiftType, "Evening") = CompairStringResult.Equal Then
                    rbtnEvening.IsChecked = True
                End If
                txtLoadingSlip.Text = obj.Loading_Slip
                txtDriverName.Text = obj.Driver_Name
                txtDriverMobNo.Text = obj.Driver_ContactNo
                txtDistributorName.Text = obj.DistributorName
                funLoadGrid(txtCode.Value)
            End If
            isInsideLoadData = False
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally

        End Try
    End Sub

    Function AllowToSave() As Boolean
        If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
            txtDate.Select()
            Return False
        End If
        If chkAgainstTransfer.Checked = True Then
            If clsCommon.myLen(FndTransferNo.Value) <= 0 Then
                FndTransferNo.Focus()
                Throw New Exception("Please select Transfer No")
            End If
            If clsCommon.myLen(txtVehicle.Value) <= 0 Then
                txtVehicle.Focus()
                Throw New Exception("Please select Vehicle")
            End If
        End If
        If clsCommon.myLen(fndRouteNo.Value) <= 0 Then
            fndRouteNo.Focus()
            Throw New Exception("Please select Route")
        End If
        If SettCreateProvisionOnOpeningAndClosingKM Then
            If txtOpKM.Value <= 0 Then
                txtOpKM.Focus()
                Throw New Exception("Please enter opening KM")
            End If

        End If
        If IsLoadingSlipMandatory Then
            If clsCommon.myLen(txtLoadingSlip.Text) <= 0 Then
                txtLoadingSlip.Focus()
                Throw New Exception("Please Enter Loading Slip")
            End If
        End If
        If clsCommon.myCdbl(txtCrateQty.Text) > 0 Then
            Dim CheckQty As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select IsNull(CrateCapacity,0)CrateCapacity from TSPL_VEHICLE_MASTER where Vehicle_Id='" + clsCommon.myCstr(txtVehicle.Value) + "'"))
            If clsCommon.myCdbl(txtCrateQty.Text) > CheckQty AndAlso CheckQty > 0 Then
                Throw New Exception("Vehicle Capacity:" + clsCommon.myCstr(CheckQty) + Environment.NewLine + "Allow only maximum " + clsCommon.myCstr(CheckQty) + " crate according vehicle crate capacity.")
                Return False
            End If
        End If
        Return funvalidatevehicle()
    End Function

    Private Function funvalidatevehicle() As Boolean
        Dim count As Decimal = 0
        Dim segno As String = ""
        Dim strvehiclenum As String = lblVehicleDesc.Text
        Dim sql As String = "select segment_code from TSPL_GL_SEGMENT_CODE where segment_code  = '" + Convert.ToString(txtVehicle.Value) + "' "
        If Not String.IsNullOrEmpty(connectSql.RunScalar(sql)) Then
            sql = "Select Number from TSPL_VEHICLE_MASTER where Vehicle_Id='" + txtVehicle.Value + "'"
            lblVehicleDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(sql))
            Return True
        Else
            Dim strmessage As String = "This vehicle code doesn't exist" + Environment.NewLine
            strmessage += "Do you want to continue "



            If common.clsCommon.MyMessageBoxShow(strmessage, Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then

                If clsCommon.myLen(lblVehicleDesc.Text) <= 0 Then
                    lblVehicleDesc.Focus()
                    Throw New Exception("Please Enter Vehicle No")
                End If


                txtVehicle.Value = clsCommon.incval(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Max(Segment_code) from TSPL_GL_SEGMENT_CODE where Segment_name = 'Vehicles'")))
                'strvehiclenum = txtVehicle.Text
                sql = "select seg_no from tspl_gl_segment where seg_name='Vehicles'"
                segno = CStr(connectSql.RunScalar(sql))
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Try
                    connectSql.RunSpTransaction(trans, "sp_tspl_gl_segmentcode_insert", New SqlParameter("@segno", segno), New SqlParameter("@segmentname", "Vehicles"), New SqlParameter("@segmentcode", txtVehicle.Value), New SqlParameter("@desc", strvehiclenum), New SqlParameter("@acccode", "NULL"), New SqlParameter("@createdby", objCommonVar.CurrentUserCode), New SqlParameter("@createddate", connectSql.serverDate(trans)), New SqlParameter("@modifiedby", objCommonVar.CurrentUserCode), New SqlParameter("@modifieddate", connectSql.serverDate(trans)), New SqlParameter("@compcode", objCommonVar.CurrentCompanyCode))
                    connectSql.RunSpTransaction(trans, "SP_TSPL_VEHICLE_MASTER_INSERT", New SqlParameter("@Vehicle_Id", txtVehicle.Value), New SqlParameter("@Model", ""), New SqlParameter("@Number", strvehiclenum), New SqlParameter("@Description", strvehiclenum), New SqlParameter("@Type", "H"), New SqlParameter("@Start_Date", Date.Now.ToString("dd/MM/yyyy")), New SqlParameter("@End_Date", Date.Now.ToString("dd/MM/yyyy")), New SqlParameter("@Vehicle_Reg_No", ""), New SqlParameter("@Vehicle_Chesis_No", ""), New SqlParameter("@Capacity", "0"), New SqlParameter("@Insurance", ""), New SqlParameter("@Pollution_Check", ""), New SqlParameter("@Fitness", Date.Now.ToString("dd/MM/yyyy")), New SqlParameter("@Trans_Type", ""), New SqlParameter("@Road_Tax", ""), New SqlParameter("@Transport_Id", ""), New SqlParameter("@Created_By", objCommonVar.CurrentUserCode), New SqlParameter("@Created_Date", connectSql.serverDate(trans)), New SqlParameter("@Modified_By", objCommonVar.CurrentUserCode), New SqlParameter("@Modified_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", objCommonVar.CurrentCompanyCode))

                    trans.Commit()
                Catch ex As Exception
                    txtVehicle.Value = ""
                    trans.Rollback()
                    Throw New Exception(ex.Message)
                End Try

                'lblVehicleDesc.Text = txtVehicle.Text + "-Hired"
                txtVehicle.Text = txtVehicle.Value
                Return True
            Else
                txtVehicle.Value = String.Empty
                txtVehicle.Text = txtVehicle.Value
                Return False
            End If
        End If
    End Function

    Sub SaveData()
        Try
            If (AllowToSave()) Then
                Dim obj As New clsDairyGatePassEntry()
                obj.GPCode = txtCode.Value
                obj.GPDate = clsCommon.myCDate(txtDate.Value)
                obj.GatePassDate = clsCommon.myCDate(txtGatepassDate.Value)
                obj.Vehicle_Id = txtVehicle.Value
                If VehicleDesc Is Nothing AndAlso clsCommon.myLen(VehicleDesc) <= 0 Then
                    obj.Vehicle_Number = lblVehicleDesc.Text
                Else
                    obj.Vehicle_Number = VehicleDesc
                End If
                obj.Item_Type = cmbitemtype.Text
                obj.Transporter = txtTransporter.Text
                obj.Salesman = txtSalesman.Text
                obj.Remarks = txtRemarks.Text
                obj.Comments = txtComments.Text
                obj.Location_Code = txtLocCode.Value
                obj.Location_Desc = txtLocDesc.Text
                obj.Supply_Date = txtSupplyDate.Value
                '===============Added by preeti Gupta Agianst Ticket no[]
                obj.Route_No = fndRouteNo.Value
                obj.TotalCrate = clsCommon.myCdbl(txtCrateQty.Text)
                obj.TotalCAN = clsCommon.myCdbl(txtCanQty.Text)
                obj.AgainstDocumentCode = clsCommon.GetMulcallString(txtmultiBooking.arrValueMember)
                obj.Opening_Km = txtOpKM.Value
                obj.Loading_Slip = txtLoadingSlip.Text
                obj.Driver_Name = txtDriverName.Text
                obj.Driver_ContactNo = txtDriverMobNo.Text
                obj.DistributorName = txtDistributorName.Text
                If chkAgainstTransfer.Checked = True Then
                    obj.IsTransfer = 1
                    obj.AgainstTransferNo = clsCommon.myCstr(FndTransferNo.Value)
                End If
                '=======================================================
                If CreateGatePassFromDemand = True Then
                    If rbtnEvening.IsChecked = True Then
                        obj.ShiftType = "Evening"
                    ElseIf rbtnMorning.IsChecked = True Then
                        obj.ShiftType = "Morning"
                    End If
                Else
                    If rbtnEvening.IsChecked = True Then
                        obj.ShiftType = "Evening"
                    ElseIf rbtnMorning.IsChecked = True Then
                        obj.ShiftType = "Morning"
                    End If
                End If
                Dim totalCrate As Integer = 0
                Dim totalCan As Integer = 0
                obj.Arr = New List(Of clsDairyGPDetail)
                For Each grow As GridViewRowInfo In Gv1.Rows
                    If clsCommon.myCdbl(grow.Cells(colQty).Value) > 0 Then
                        Dim objTr As New clsDairyGPDetail()
                        objTr.PK_ID = clsCommon.myCDecimal(grow.Cells(colPKID).Value)
                        objTr.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                        objTr.Unit_Code = clsCommon.myCstr(grow.Cells(colUnit).Value)
                        objTr.Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                        objTr.HSN_Code = clsCommon.myCstr(grow.Cells(colHSNCode).Value)
                        obj.Arr.Add(objTr)
                    End If
                Next
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please Fill at list one Document", Me.Text)
                    Return
                End If
                Dim CheckVehicle As String = clsDBFuncationality.getSingleValue("select Vehicle_Number from TSPL_DAIRYSALE_GATEPASS_Master where Convert(Date,GPDate,103)=Convert(Date,'" + txtDate.Value + "',103) And Route_No='" + clsCommon.myCstr(fndRouteNo.Value) + "' And ShiftType='" + clsCommon.myCstr(obj.ShiftType) + "' and Vehicle_Number='" + clsCommon.myCstr(obj.Vehicle_Number) + "'")
                If CheckVehicle IsNot Nothing AndAlso clsCommon.myLen(CheckVehicle) > 0 Then
                    If clsCommon.MyMessageBoxShow(Me, "Vehicle No. is already used for another gatepass." + Environment.NewLine + "Do you want to proceed?", Me.Text, MessageBoxButtons.YesNo) = DialogResult.Yes Then
                        If (obj.SaveData(obj, isNewEntry, "DS")) Then
                            common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                            LoadData(obj.GPCode, NavigatorType.Current)
                            arrShipmentFromMultiple = Nothing
                            btnGo.Enabled = False
                            RadGroupBox3.Enabled = False
                        End If
                    End If
                Else
                    If (obj.SaveData(obj, isNewEntry, "DS")) Then
                        common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                        LoadData(obj.GPCode, NavigatorType.Current)
                        arrShipmentFromMultiple = Nothing
                        btnGo.Enabled = False
                        RadGroupBox3.Enabled = False
                    End If
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub Addnew()
        txtLoadingSlip.Text = ""
        txtTollAmount.Text = 0
        txtCode.Value = ""
        txtDate.Value = clsCommon.GETSERVERDATE
        txtSupplyDate.Value = txtDate.Value
        txtGatepassDate.Value = clsCommon.GETSERVERDATE()
        txtVehicle.Value = ""
        lblVehicleDesc.Text = ""
        If CreateGatePassFromDemand = False Then
            txtLocCode.Value = ""
            txtLocDesc.Text = ""
        End If
        txtDistributorName.Text = ""
        txtTransporter.Text = ""
        txtSalesman.Text = ""
        txtRemarks.Text = ""
        txtComments.Text = ""
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnSave.Text = "Save"
        LoadBlankGrid()
        isNewEntry = True
        cmbitemtype.Text = "Select"
        isInsideLoadData = False
        fndRouteNo.Value = ""
        txtRouteName.Text = ""
        txtCanQty.Text = 0
        txtCrateQty.Text = 0
        txtmultiBooking.Enabled = False
        txtmultiBooking.arrValueMember = Nothing
        btnDelete.Enabled = True
        btnClKM.Enabled = False
        lblClosingDate.Text = ""
        UsLock1.Status = ERPTransactionStatus.Pending
        chkAgainstTransfer.Checked = False
        FndTransferNo.Value = ""
        txtTransporter.Enabled = False
        txtOpKM.Value = 0
        txtClKM.Value = 0
        txtLocCode.Enabled = True
        TxtMultiDairyGPassReverse.arrValueMember = Nothing
        txtDriverName.Text = Nothing
        txtDriverMobNo.Text = Nothing
    End Sub

    Private Sub cmbitemtype_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs)
        'If isInsideLoadData = False Then
        '    txtVehicle.Value = ""
        '    lblVehicleDesc.Text = ""
        '    txtTransporter.Text = ""
        '    txtRemarks.Text = ""
        '    txtComments.Text = ""
        '    LoadBlankGrid()
        '    isNewEntry = True
        '    isInsideLoadData = False
        '    funFillGrid()
        'End If
    End Sub

    Private Sub txtDate_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        'If isInsideLoadData = False Then
        '    If clsCommon.CompairString(cmbitemtype.Text, "Select") = CompairStringResult.Equal Then
        '        clsCommon.MyMessageBoxShow("Please select Item type")
        '    Else
        '        funFillGrid()
        '    End If
        'End If
    End Sub

    Private Sub Gv1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub txtLocCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtLocCode._MYValidating
        strQuery = "select distinct Bill_To_Location as Code,Location_Desc as Description from TSPL_SD_SALE_INVOICE_HEAD left outer join TSPL_LOCATION_MASTER on TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location=TSPL_LOCATION_MASTER.Location_Code"
        txtLocCode.Value = clsCommon.ShowSelectForm("LocationSegGP", strQuery, "Code", "screen_type='DS'", txtLocCode.Value, "Code", isButtonClicked)
        txtLocDesc.Text = clsDBFuncationality.getSingleValue("select  Location_Desc  from TSPL_LOCATION_MASTER where Location_Code='" & txtLocCode.Value & "'")
    End Sub

    Private Sub btnGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGo.Click
        If isInsideLoadData = False Then
            'If clsCommon.myLen(txtVehicle.Value) <= 0 AndAlso clsCommon.myLen(fndRouteNo.Value) <= 0 Then
            '    clsCommon.MyMessageBoxShow("Please Select  Vehicle code OR Route No OR Both.")
            '    Exit Sub
            'End If
            If CreateGatePassFromDemand = True Then
                If clsCommon.myLen(fndRouteNo.Value) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Please Select Route No.", Me.Text)
                    fndRouteNo.Focus()
                    Exit Sub
                End If
            End If

            LoadBlankGrid()
            isNewEntry = True
            isInsideLoadData = False
            If chkAgainstTransfer.Checked = True Then
                If clsCommon.myLen(FndTransferNo.Value) > 0 Then
                    funFillGrid_Transfer()
                Else
                    clsCommon.MyMessageBoxShow(Me, "Please select Transfer No", Me.Text)
                End If

            Else
                funFillGrid()
            End If


        End If
    End Sub

    Private Sub frmDairyGatePass_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        'Add Tool tip Task No- TEC/18/05/18-000237
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnNew.Enabled Then
            btnNew.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnSave.Enabled AndAlso MyBase.isDeleteFlag Then
            btnDelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled AndAlso MyBase.isPostFlag Then
            btnPost.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            Me.Close()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine +
                                         "TSPL_DAIRYSALE_GATEPASS_MASTER " + Environment.NewLine +
                                         "TSPL_DAIRYSALE_GATEPASS_DETAIL  ")
            Dim frm As New FrmPWD(Nothing)
            frm.strType = "SIRC"
            frm.strCode = "SIReversAndCreate"
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnReverse.Visible = True
            End If
        ElseIf e.Alt AndAlso e.Control AndAlso e.Shift AndAlso e.KeyCode = Keys.F11 Then
            If btnMultiGPReverse.Visible Then
                multipleDelteVisible(False)
            Else
                Dim pwd As New FrmPWD(Nothing)
                pwd.strCode = clsFixedParameterCode.MultiDairyGatePassReversePWD
                pwd.strType = clsFixedParameterType.MultiDairyGatePassReversePWD
                pwd.ShowDialog()
                If pwd.isPasswordCorrect Then
                    multipleDelteVisible(True)
                End If
            End If
        End If
    End Sub
    Sub multipleDelteVisible(ByVal val As Boolean)
        MyLabel34.Visible = val
        TxtMultiDairyGPassReverse.Visible = val
        btnMultiGPReverse.Visible = val
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub txtVehicle__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtVehicle._MYValidating
        'done by priti BHA/25/07/18-000192 to remove transtype condition and added screen type condition.
        strQuery = ReturnVehicle()
        VehicleDesc = Nothing
        txtVehicle.Value = clsCommon.ShowSelectForm("Vehicle", strQuery, "Code", "", txtVehicle.Value, "Code", isButtonClicked)
        lblVehicleDesc.Text = clsDBFuncationality.getSingleValue("select Description from TSPL_VEHICLE_MASTER where Vehicle_Id='" & txtVehicle.Value & "'")
        If isCreateProvisionOfTransporterInDairyDispatch = True Then
            txtTransporter.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Transport_Id from TSPL_VEHICLE_MASTER where Vehicle_Id='" & txtVehicle.Value & "'"))
        End If
    End Sub

    Private Function ReturnVehicle() As String
        Dim Query As String = Nothing
        If CreateGatePassFromDemand = True Then
            Query = "select distinct Final.Vehicle_Code as Code,Description,tspl_route_master.Route_No as [Route No], tspl_route_master.Route_Desc as [Route Desc]  from ( " &
        "select distinct Vehicle_Code,TSPL_VEHICLE_MASTER.Description from TSPL_DEMAND_BOOKING_DETAIL left outer join " &
        "TSPL_VEHICLE_MASTER on TSPL_DEMAND_BOOKING_DETAIL.Vehicle_Code=TSPL_VEHICLE_MASTER.Vehicle_Id  ) final left outer join tspl_route_master on tspl_route_master.vehicle_code = Final.Vehicle_Code "
        Else
            Query = "select distinct Final.Vehicle_Code as Code,Description,tspl_route_master.Route_No as [Route No], tspl_route_master.Route_Desc as [Route Desc]  from ( " &
        "select distinct Vehicle_Code,TSPL_VEHICLE_MASTER.Description from TSPL_SD_SHIPMENT_HEAD left outer join " &
        "TSPL_VEHICLE_MASTER on TSPL_SD_SHIPMENT_HEAD.Vehicle_Code=TSPL_VEHICLE_MASTER.Vehicle_Id where TSPL_SD_SHIPMENT_HEAD.screen_type='DS' " &
        "union all " &
        "select distinct AlternateVehicle as Vehicle_Code,TSPL_VEHICLE_MASTER.Description from TSPL_SD_SHIPMENT_HEAD left outer join " &
        "TSPL_VEHICLE_MASTER on TSPL_SD_SHIPMENT_HEAD.AlternateVehicle=TSPL_VEHICLE_MASTER.Vehicle_Id where TSPL_SD_SHIPMENT_HEAD.screen_type='DS' and AlternateVehicle <> '' " &
        "union all " &
        "select distinct ManualVehicle,'' as Description from TSPL_SD_SHIPMENT_HEAD where TSPL_SD_SHIPMENT_HEAD.screen_type='DS' and ManualVehicle <> '' ) final left outer join tspl_route_master on tspl_route_master.vehicle_code = Final.Vehicle_Code"
        End If
        Return Query
    End Function

    Private Sub txtCode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_DAIRYSALE_GATEPASS_MASTER where GPCode='" + txtCode.Value + "' "
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If count = 0 Then
                txtCode.MyReadOnly = False
            Else
                txtCode.MyReadOnly = True
            End If
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCode._MYValidating
        ' Dim qry As String = " SELECT  GPCode,convert(varchar(10),GPDate,103)  as GPDate,Vehicle_Id,Vehicle_Number FROM  TSPL_DAIRYSALE_GATEPASS_MASTER"
        ' Ticket No : ERO/23/05/19-000614 By prabhakar
        'Ticket No-ERO/05/08/19-000984 ,Sanjay, add pending / approved 
        'Ticket No-ERO/27/08/19-001004 ,Add Opening_Km,Closing_Km
        Dim qry As String = " SELECT  TSPL_DAIRYSALE_GATEPASS_MASTER.GPCode,convert(varchar(10),TSPL_DAIRYSALE_GATEPASS_MASTER.GPDate,103)  as GPDate,TSPL_DAIRYSALE_GATEPASS_MASTER.ShiftType,TSPL_DAIRYSALE_GATEPASS_MASTER.Vehicle_Id,TSPL_DAIRYSALE_GATEPASS_MASTER.Vehicle_Number,TSPL_DAIRYSALE_GATEPASS_MASTER.Route_No,tspl_Route_Master.Route_Desc, case when TSPL_DAIRYSALE_GATEPASS_MASTER.Post='Y' then 'Approved' else 'Pending' end as Status,Opening_Km,Closing_Km ,isnull(TSPL_DAIRYSALE_GATEPASS_MASTER.AgainstTransferNo,'') as [Against Transfer No], Case When TSPL_DAIRYSALE_GATEPASS_MASTER.Status='Y' Then 'Cancel' Else Null End As [GP Status] FROM  TSPL_DAIRYSALE_GATEPASS_MASTER " &
                            " left Outer join tspl_Route_Master on tspl_Route_Master.Route_No = TSPL_DAIRYSALE_GATEPASS_MASTER.Route_No "

        LoadData(clsCommon.ShowSelectForm("GatepassEntry", qry, "GPCode", "", txtCode.Value, "GPCode", isButtonClicked), NavigatorType.Current)

        If clsCommon.myLen(txtCode.Value) > 0 Then
            txtCode.MyReadOnly = False
            btnGo.Enabled = False
            RadGroupBox3.Enabled = False
        Else
            txtCode.MyReadOnly = True
            btnGo.Enabled = True
            RadGroupBox3.Enabled = True
        End If
        'funLoadGrid(txtCode.Value)
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If clsCommon.CompairString(btnSave.Text, "Update") = CompairStringResult.Equal AndAlso OneTimeCheck = False Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = clsFixedParameterType.SIRC
            frm.strCode = clsFixedParameterCode.UpdatePassword
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                OneTimeCheck = True
            Else
                Exit Sub
            End If
        End If
        If clsCommon.CompairString(btnSave.Text, "Update") = CompairStringResult.Equal AndAlso OneTimeCheck Then
            SaveData()
        Else
            SaveData()
        End If
    End Sub

    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Addnew()
        cmbitemtype.Enabled = True
        txtDate.Enabled = True
        btnGo.Enabled = True
        RadGroupBox3.Enabled = True
        VehicleDesc = Nothing
    End Sub

    Private Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPost.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Document No not found to Post", Me.Text)
                Exit Sub
            End If

            Dim isPost As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select count (*) from TSPL_DAIRYSALE_GATEPASS_MASTER where GPCode = '" + txtCode.Value + "' and Post = 'Y'"))
            If isPost = True Then
                common.clsCommon.MyMessageBoxShow(Me, "Record Already posted.", Me.Text)
                Exit Sub
            End If
            If myMessages.postConfirm() Then
                If (clsDairyGatePassEntry.PostData(MyBase.Form_ID, txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                    LoadData(txtCode.Value, NavigatorType.Current)
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                End If

                'clsDBFuncationality.ExecuteNonQuery("Update TSPL_DAIRYSALE_GATEPASS_MASTER set post='Y' where gpcode='" & txtCode.Value & "'")

            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try

    End Sub

    Private Sub btnSelect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelect.Click
        Dim I As Integer = 0
        If (Gv1.Rows.Count > 0) AndAlso clsCommon.CompairString(btnSelect.Text, "Select All") = CompairStringResult.Equal Then
            For Each grow As GridViewRowInfo In Gv1.Rows
                grow.Cells(ColApply).Value = True
            Next
            btnSelect.Text = "Unselect All"
        ElseIf (Gv1.Rows.Count > 0) AndAlso clsCommon.CompairString(btnSelect.Text, "Unselect All") = CompairStringResult.Equal Then
            For Each grow As GridViewRowInfo In Gv1.Rows
                grow.Cells(ColApply).Value = False
            Next
            btnSelect.Text = "Select All"
        End If
    End Sub

    Private Sub Gv1_CurrentRowChanged(sender As Object, e As CurrentRowChangedEventArgs) Handles Gv1.CurrentRowChanged

    End Sub

    Private Sub gv1_CellDoubleClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles Gv1.CellDoubleClick
        Try
            Dim strItem = Gv1.Rows(e.RowIndex).Cells(1).Value
            strQuery = LoadDoubleClickQuery(strItem)
            Dim frmStock As New FrmStockDetail
            frmStock.LoadDispatchData(strQuery)
            frmStock.Show()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    '============================Changes by preeti gupta [09/01/2017],[BHA/02/08/18-000212]
    Private Function GetAttachQry(ByVal StrCode As String) As String
        ''richa remove ceiling from crate qty 15 Nov,2019
        Dim Qry As String = " Select Case When CFinPouch > 0 Then ( ( Final.Crate_Qty * Final.Conversion_Factor )/ CFinPouch ) Else 0 End AS 'NoOfPouch', Case When CFinLTR > 0 Then ( ( ( Final.Crate_Qty * Final.Conversion_Factor + Final.Pouch_Qty ) )/ CFinLTR ) Else 0 End AS 'MilkQuantity', Case When CFinLTR > 0 Then ( ( ( Final.Crate_Qty * Final.Conversion_Factor )+( Final.Pouch_Qty * Final.CFinPouch ) )/ CFinLTR ) Else 0 End AS 'MilkQuantityltr', CAST( ( Final.Box_Crate_Qty * Final.Conversion_Factor ) / CFinKG AS DECIMAL(10, 2) ) AS 'MilkQuantityKG', Case When Final.Column_Crate > 0 Then Cast( ( Final.Crate_Qty / Final.Column_Crate ) AS int ) Else 0 End AS 'CrateLine', Case When Column_Crate > 0 Then cast( ( cast(qty as int)% Column_Crate ) as int ) Else 0 End AS 'LooseCrate', Pouch_Qty AS 'LoosePouch', CAST( CASE WHEN qty > 0 THEN CAST(qty AS decimal(18,2)) / Conversion_FactorCrt END AS decimal(18,2) ) AS CrateQtydd, CASE WHEN Unit_Code='POUCH' then qty*CFinPouch / Conversion_FactorCrt WHEN Unit_Code='LTR' then qty*CFinLTR / Conversion_FactorCrt WHEN Unit_Code='KG' then qty*CFinKG / Conversion_FactorCrt WHEN Unit_Code='CRATE' then qty*Conversion_FactorCrt / Conversion_FactorCrt WHEN Unit_Code='BOX' then qty*CFinBOX / Conversion_FactorCrt ELSE 0 END AS QtyCrate , Final.*, tbl_Brand.Brand, tbl_Brand.BRANDDESC, TSPL_COMPANY_MASTER.Logo_Img, TSPL_COMPANY_MASTER.Logo_Img2, TSPL_COMPANY_MASTER.Logo_Img2 FROM 
                   ( Select  max(Supply_Date)as Supply_Date,"

        If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "ALW") = CompairStringResult.Equal Then
            Qry += " max(Conversion_FactorCrt) as Conversion_FactorCrt ,MAX(gpUnit) AS gpUnit, "
        Else
            Qry += " max(Conversion_FactorCrt) as Conversion_FactorCrt, "
        End If

        Qry += " Max(Distributor) Distributor, Max(CFinPouch) CFinPouch, Max(CFinLTR) CFinLTR, Max(CFinBOX) CFinBOX, Max(CFinKG) CFinKG, Max(Conversion_Factor) Conversion_Factor, Max(AgainstTransferNo) AgainstTransferNo, Max(Comp_Code) Comp_Code, Sum( Qty * case when Unit_Code = 'Crate' then 1 else 0 end ) Crate_Qty, Sum( Qty * case when Unit_Code = 'Pouch' then 1 else 0 end ) Pouch_Qty, Sum(Box_Crate_Qty) Box_Crate_Qty, Max(Insurance_No) Insurance_No, Max(Insurance_Comp_Name) Insurance_Comp_Name, Max(comp_name) comp_name, Max(unit_code) unit_code, Sum(qty) qty, Max(Comp_Address) Comp_Address, Max(Loc_add) Loc_add, Max(Route_No) Route_No, Sum(Totalcrate) Totalcrate, Sum(TotalCan) TotalCan, Max(Route_Desc) Route_Desc, Max(GPCode) GPCode, Max(GPDate) GPDate, Max(GPTime) GPTime, Max(vehicle_id) vehicle_id, Max(VehicleDesc) VehicleDesc, Max(location_code) location_code, Max(Location_desc) Location_desc, Max(transporter) transporter, Max(remarks) remarks, Max(comments) comments, Max(post) post, Item_code, Max(item_desc) item_desc, Max(short_description) short_description, Max(sku_seq) sku_seq, Max(TranporterNameFromMaster) TranporterNameFromMaster, Max(HSN_Code) HSN_Code, Max(Salesman) Salesman, Sum(Column_Crate) Column_Crate, Max(Area_Code) Area_Code, Max(Zone_Code) Zone_Code, Max(ShiftType) ShiftType, Max(GSTReg_No) GSTReg_No, Max(Loading_Slip) Loading_Slip, Max(DispatchDate) DispatchDate, Max(GatePass_Date) GatePass_Date, Sum(Amount) Amount, Sum(Margin) Margin, sum(SecurityAmt) as SecurityAmt, max(Dist_Commission_Ratewithtax) Dist_Commission_Ratewithtax, Max(Driver_Name) Driver_Name, Max(Driver_ContactNo) Driver_ContactNo FROM
                   ( select FORMAT( TSPL_DAIRYSALE_GATEPASS_MASTER.Supply_Date, 'dd/MM/yyyy' ) as Supply_Date,"
        If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "ALW") = CompairStringResult.Equal Then
            Qry += " ItemConversionCrate.Conversion_Factor as Conversion_FactorCrt , gpUnit.Conversion_Factor AS gpUnit, "
        Else
            Qry += " ItemConversionCrate.Conversion_Factor as Conversion_FactorCrt , "
        End If

        Qry += " TSPL_DAIRYSALE_GATEPASS_MASTER.DistributorName As 'Distributor', ItemConversionInPouch.Conversion_Factor As 'CFinPouch', ItemConversionInLTR.Conversion_Factor AS 'CFinLTR', ItemConversionInKG.Conversion_Factor AS 'CFinKG', ItemConversionInBox.Conversion_Factor AS 'CFinBOX', CurrentUnit.Conversion_Factor, isnull( TSPL_DAIRYSALE_GATEPASS_MASTER.AgainstTransferNo, '' ) as AgainstTransferNo, TSPL_COMPANY_MASTER.Comp_Code, CASE WHEN TSPL_DAIRYSALE_GATEPASS_DETAIL.unit_code <> 'Box' THEN convert( decimal(18, 2), ( TSPL_DAIRYSALE_GATEPASS_DETAIL.qty / CrateUnit.conversion_factor )* StockUnit.conversion_factor * CurrentUnit.conversion_factor ) ELSE 0 END as Crate_Qty, CASE WHEN TSPL_DAIRYSALE_GATEPASS_DETAIL.unit_code <> 'Crate' and TSPL_DAIRYSALE_GATEPASS_DETAIL.unit_code <> 'Pouch' THEN convert( decimal(18, 2), ( TSPL_DAIRYSALE_GATEPASS_DETAIL.qty / CurrentUnit.conversion_factor )* StockUnit.conversion_factor * CurrentUnit.conversion_factor ) else 0 end as Box_Crate_Qty, TSPL_COMPANY_MASTER.Insurance_No, TSPL_COMPANY_MASTER.Insurance_Comp_Name, TSPL_COMPANY_MASTER.comp_name, TSPL_DAIRYSALE_GATEPASS_DETAIL.unit_code, TSPL_DAIRYSALE_GATEPASS_DETAIL.qty, TSPL_COMPANY_MASTER.add1 + case when len(TSPL_COMPANY_MASTER.add2)> 0 then ', ' + TSPL_COMPANY_MASTER.add2 else '' end + case when LEN( isnull(TSPL_COMPANY_MASTER.Add3, '') )> 0 then ', ' + isnull(TSPL_COMPANY_MASTER.Add3, '') else ' ' end as Comp_Address, tspl_location_master.add1 + case when len(tspl_location_master.add2)> 0 then ', ' + tspl_location_master.add2 else '' end + case when LEN( isnull(tspl_location_master.Add3, '') )> 0 then ', ' + isnull(tspl_location_master.Add3, '') else ' ' end as Loc_add, TSPL_DAIRYSALE_GATEPASS_MASTER.Route_No, TSPL_DAIRYSALE_GATEPASS_MASTER.Totalcrate, TSPL_DAIRYSALE_GATEPASS_MASTER.TotalCan, tspl_route_master.Route_Desc, TSPL_DAIRYSALE_GATEPASS_MASTER.GPCode, convert( varchar, TSPL_DAIRYSALE_GATEPASS_MASTER.GPDate, 103 ) as GPDate, FORMAT( TSPL_DAIRYSALE_GATEPASS_MASTER.GPDate, 'hh:mm tt' ) as GPTime, TSPL_DAIRYSALE_GATEPASS_MASTER.GatePass_Date, TSPL_DAIRYSALE_GATEPASS_MASTER.Vehicle_Id AS vehicle_id, TSPL_DAIRYSALE_GATEPASS_MASTER.Vehicle_Number as VehicleDesc, TSPL_DAIRYSALE_GATEPASS_MASTER.location_code, tspl_location_master.Location_desc, TSPL_DAIRYSALE_GATEPASS_MASTER.transporter, TSPL_DAIRYSALE_GATEPASS_MASTER.remarks, TSPL_DAIRYSALE_GATEPASS_MASTER.comments, TSPL_DAIRYSALE_GATEPASS_MASTER.post, TSPL_DAIRYSALE_GATEPASS_DETAIL.Item_code, tspl_item_master.item_desc, tspl_item_master.short_description, tspl_item_master.sku_seq, TSPL_TRANSPORT_MASTER.Transporter_Name as TranporterNameFromMaster, TSPL_DAIRYSALE_GATEPASS_DETAIL.HSN_Code, TSPL_DAIRYSALE_GATEPASS_MASTER.Salesman, tspl_vehicle_master.Column_Crate, TSPL_DAIRYSALE_GATEPASS_MASTER.Route_No AS Area_Code, TSPL_CUSTOMER_MASTER.Zone_Code, TSPL_DAIRYSALE_GATEPASS_MASTER.ShiftType, tspl_company_master.GSTReg_No, TSPL_DAIRYSALE_GATEPASS_MASTER.Loading_Slip, ( Select Max(Document_Date) from TSPL_SD_SHIPMENT_HEAD where GPCode='" + StrCode + "') AS 'DispatchDate',xyz.Amount,xyz.Margin, xyz.SecurityAmt, xyz.Dist_Commission_Ratewithtax,TSPL_DAIRYSALE_GATEPASS_MASTER.Driver_Name,TSPL_DAIRYSALE_GATEPASS_MASTER.Driver_ContactNo from TSPL_DAIRYSALE_GATEPASS_DETAIL " &
                   " left outer join TSPL_DAIRYSALE_GATEPASS_MASTER on TSPL_DAIRYSALE_GATEPASS_MASTER.GPCode=TSPL_DAIRYSALE_GATEPASS_DETAIL.GPCode " &
                   " left outer join tspl_vehicle_master on tspl_vehicle_master.Vehicle_id=TSPL_DAIRYSALE_GATEPASS_MASTER.vehicle_id " &
                   " left outer join tspl_location_master on tspl_location_master.location_code=TSPL_DAIRYSALE_GATEPASS_MASTER.location_code " &
                   " left outer join tspl_company_master on tspl_company_master.comp_code=TSPL_DAIRYSALE_GATEPASS_MASTER.comp_code " &
                   " left outer join tspl_item_master on tspl_item_master.item_code=TSPL_DAIRYSALE_GATEPASS_DETAIL.Item_code " &
                   " left outer join  TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id=TSPL_VEHICLE_MASTER.Transport_Id " &
                   " left outer join  tspl_route_master on tspl_route_master.Route_No=TSPL_DAIRYSALE_GATEPASS_MASTER.Route_No " &
                   " left join tspl_item_uom_detail StockUnit on StockUnit.item_code=TSPL_DAIRYSALE_GATEPASS_DETAIL.item_code	and StockUnit.stocking_unit='Y'  " &
                   " left join tspl_item_uom_detail CurrentUnit on CurrentUnit.item_code=TSPL_DAIRYSALE_GATEPASS_DETAIL.item_code and 	CurrentUnit.uom_code=	TSPL_DAIRYSALE_GATEPASS_DETAIL.unit_code " &
                   " left join tspl_item_uom_detail CrateUnit on CrateUnit.item_code=TSPL_DAIRYSALE_GATEPASS_DETAIL.item_code  and 	CrateUnit.uom_code=	'Crate' " &
                   " left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code='Pouch') as ItemConversionInPouch on ItemConversionInPouch.Item_code=TSPL_DAIRYSALE_GATEPASS_DETAIL.Item_Code
                    left join ( select Conversion_factor, TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code = 'Crate' ) as ItemConversionCrate on ItemConversionCrate.Item_code = TSPL_DAIRYSALE_GATEPASS_DETAIL.Item_Code 
                     left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code='LTR') as ItemConversionInLTR on ItemConversionInLTR.Item_code=TSPL_DAIRYSALE_GATEPASS_DETAIL.Item_Code 
                     left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code='Box') as ItemConversionInBox on ItemConversionInBox.Item_code=TSPL_DAIRYSALE_GATEPASS_DETAIL.Item_Code
                     left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code='KG') as ItemConversionInKG on ItemConversionInKG.Item_code=TSPL_DAIRYSALE_GATEPASS_DETAIL.Item_Code "
        '--" left join tspl_item_uom_detail gpUnit on gpUnit.item_code = TSPL_DAIRYSALE_GATEPASS_DETAIL.item_code and CrateUnit.uom_code = TSPL_DAIRYSALE_GATEPASS_DETAIL.Unit_Code "

        If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "ALW") = CompairStringResult.Equal Then
            Qry += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=tspl_vehicle_master.Transport_Id  left join tspl_item_uom_detail gpUnit on gpUnit.item_code = TSPL_DAIRYSALE_GATEPASS_DETAIL.item_code and CrateUnit.uom_code = TSPL_DAIRYSALE_GATEPASS_DETAIL.Unit_Code "
        Else
            Qry += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=tspl_vehicle_master.Transport_Id  "
        End If

        Qry += " left outer join 
     
                     ((select Case When Sum(Isnull(TSPL_SD_SHIPMENT_DETAIL.Qty,0))>0 Then  Sum(TSPL_SD_SHIPMENT_DETAIL.Item_Net_Amt/TSPL_SD_SHIPMENT_DETAIL.Qty*TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL.GP_Qty) Else 0 End As Amount,Sum(IsNull(TSPL_SD_SHIPMENT_DETAIL.Distributor_Commission_Amt,0)/TSPL_SD_SHIPMENT_DETAIL.Qty*TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL.GP_Qty) AS Margin,sum( IsNull(TSPL_SD_SHIPMENT_DETAIL.Security_Amt,0))as SecurityAmt,TSPL_SD_SHIPMENT_DETAIL.Item_Code,TSPL_SD_SHIPMENT_DETAIL.Unit_code,max(TSPL_SD_SHIPMENT_DETAIL.Distributor_Commission_Rate) as Dist_Commission_Ratewithtax from TSPL_SD_SHIPMENT_DETAIL   
                       Left Outer Join TSPL_SD_SHIPMENT_HEAD ON TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE 
					   Left Outer Join TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL On TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL.PK_ID=TSPL_SD_SHIPMENT_DETAIL.PK_ID
                     WHERE  TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL.GPCode = '" + StrCode + "'
                     Group By  TSPL_SD_SHIPMENT_DETAIL.Item_Code,TSPL_SD_SHIPMENT_DETAIL.Unit_code)
                     Union All
                     (select Case When Sum(Isnull(TSPL_SD_SHIPMENT_DETAIL.Qty,0))>0 Then  Sum(TSPL_SD_SHIPMENT_DETAIL.Item_Net_Amt) Else 0 End As Amount,
                     Sum(IsNull(TSPL_SD_SHIPMENT_DETAIL.Distributor_Commission_Amt,0)) AS Margin,sum( IsNull(TSPL_SD_SHIPMENT_DETAIL.Security_Amt,0))as SecurityAmt,TSPL_SD_SHIPMENT_DETAIL.Item_Code,TSPL_SD_SHIPMENT_DETAIL.Unit_code,max(TSPL_SD_SHIPMENT_DETAIL.Distributor_Commission_Rate) as Dist_Commission_Ratewithtax from TSPL_SD_SHIPMENT_DETAIL   
                     Left Outer Join TSPL_SD_SHIPMENT_HEAD ON TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE 
                     WHERE  TSPL_SD_SHIPMENT_HEAD.GPCode = '" + StrCode + "'
                     Group By  TSPL_SD_SHIPMENT_DETAIL.Item_Code,TSPL_SD_SHIPMENT_DETAIL.Unit_code)
                     )xyz ON xyz.Item_Code=TSPL_DAIRYSALE_GATEPASS_DETAIL.Item_Code And xyz.Unit_code=TSPL_DAIRYSALE_GATEPASS_DETAIL.Unit_Code" &
                    "  where 2=2 " &
                    "  and  TSPL_DAIRYSALE_GATEPASS_MASTER.GPCode = '" + StrCode + "' " &
                    " )As Main Group by Item_code )AS Final left outer join  ( select Item_Code,max([CATEGORY RM]) as [CATEGORY RM],max([BRAND]) as [BRAND],max([SUB BRAND]) as [SUB BRAND], " &
                    " max([DESCRP]) as [DESCRP],max([PACK]) as [PACK], max([PACK SIZE]) as [PACK SIZE],max([CATEGORY OT]) as [CATEGORY OT],max([CATEGORY FA]) as [CATEGORY FA], " &
                    " max([P TYPE]) as [P TYPE],max([L TYPE]) as [L TYPE],max([JW]) as [JW], max([SCRAP]) as [SCRAP],max([CATEGORY RMDESC]) as [CATEGORY RMDESC],max([BRANDDESC]) " &
                    " as [BRANDDESC],max([SUB BRANDDESC]) as [SUB BRANDDESC],max([DESCRPDESC]) as [DESCRPDESC],max([PACKDESC]) as [PACKDESC], max([PACK SIZEDESC]) as [PACK SIZEDESC],max([CATEGORY OTDESC]) as [CATEGORY OTDESC]," &
                    " max([CATEGORY FADESC]) as [CATEGORY FADESC],max([P TYPEDESC]) as [P TYPEDESC],max([L TYPEDESC]) as [L TYPEDESC],max([JWDESC]) as [JWDESC], max([SCRAPDESC]) as [SCRAPDESC] " &
                    " from ( select * from (   select TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code   ,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code+'DESC' as Item_Category_CodeDesc   ," &
                    " TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values  ,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as Category_Value_Desc  from  TSPL_ITEM_MASTER    left outer join TSPL_ITEM_MASTER_CATEGORY on " &
                    " TSPL_ITEM_MASTER_CATEGORY.Item_code = TSPL_ITEM_MASTER.Item_code  left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and " &
                    " TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values   where 2=2 )xx  Pivot   ( max(Item_Cagetory_Values) for Item_Category_Code   in ( [CATEGORY RM],[BRAND],[SUB BRAND],[DESCRP],[PACK],[PACK SIZE], " &
                    " [CATEGORY OT],[CATEGORY FA],[P TYPE],[L TYPE],[JW],[SCRAP])  ) Pivt   Pivot  ( max(Category_Value_Desc) for Item_Category_CodeDesc in ([CATEGORY RMDESC], [BRANDDESC],[SUB BRANDDESC],[DESCRPDESC],[PACKDESC],[PACK SIZEDESC]," &
                    " [CATEGORY OTDESC],[CATEGORY FADESC],[P TYPEDESC],[L TYPEDESC],[JWDESC],[SCRAPDESC])  ) Pivt1 ) xxx  group by Item_Code )  as tbl_Brand on tbl_Brand.Item_Code=Final.Item_Code   left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =Final.Comp_Code " &
                     " order by Final.sku_seq"

        Return Qry

    End Function

    Public Sub funPrint2(ByVal Code As String)
        Try
            If CreateGatePassFromDemand = True Then
                frmDemandBooking.PrintGatePass("DG", Code, IIf(rbtnMorning.IsChecked = True, "Morning", "Evening"))
            Else
                atchqry = GetAttachQry(Code)
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(atchqry)
                If dt.Rows.Count > 0 Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    'frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "crptDairySaleGatePassEntry", "Dairy Sale GatePass Entry", clsCommon.myCDate(dt.Rows(0)("GPDate")))
                    'frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "crptDairySaleGatePassEntryNew", "Dairy Sale GatePass Entry", clsCommon.myCDate(dt.Rows(0)("GPDate")))
                    frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "crptDairySaleGatePassEntries", "Dairy Sale GatePass Entry", clsCommon.myCDate(dt.Rows(0)("GPDate")))

                    frmCRV = Nothing
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub funPrint(ByVal Code As String)
        Try
            If CreateGatePassFromDemand = True Then
                frmDemandBooking.PrintGatePass("DG", Code, IIf(rbtnMorning.IsChecked = True, "Morning", "Evening"))
            Else
                atchqry = GetAttachQry(Code)
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(atchqry)
                If dt.Rows.Count > 0 Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "ALW") = CompairStringResult.Equal Then
                        frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "crptDairySaleGatePassEntryNewALW", "Dairy Sale GatePass Entry", clsCommon.myCDate(dt.Rows(0)("GPDate")))
                        'frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "crptDairySaleGatePassEntry", "Dairy Sale GatePass Entry", clsCommon.myCDate(dt.Rows(0)("GPDate")))
                    Else
                        frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "crptDairySaleGatePassEntryNew", "Dairy Sale GatePass Entry", clsCommon.myCDate(dt.Rows(0)("GPDate")))
                        'frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "crptDairySaleGatePassEntries", "Dairy Sale GatePass Entry", clsCommon.myCDate(dt.Rows(0)("GPDate")))
                    End If
                    frmCRV = Nothing
                    End If
                End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    '  Public Function GetQueryGatePass() As String

    '      Dim Qry As String = "select TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 as Comp_Add1,TSPL_COMPANY_MASTER.Add2 as Comp_Add2,TSPL_COMPANY_MASTER.Add3 as Comp_Add3,TSPL_COMPANY_MASTER.Pincode as Comp_Pin
    '      ,TSPL_LOCATION_MASTER.Add1,TSPL_LOCATION_MASTER.Add2,TSPL_LOCATION_MASTER.ADD3,TSPL_LOCATION_MASTER.Pin_Code,TSPL_LOCATION_MASTER.Location_Desc
    '      ,Main_Final.Distributor,'" & IIf(rbtnMorning.IsChecked = True, "Morning", "Evening") & "' shiftType,Main_Final.City_Name,Main_Final.Demand_No,Main_Final.Demand_Date,Main_Final.Route_No,Main_Final.Route_Desc
    '      ,Main_Final.Item_alies_name,Main_Final.Crate_Qty,Main_Final.Pouch_Qty,Main_Final.Loose_Qty,TotalLtr_ItemWise
    '      from (select max(TSPL_VENDOR_MASTER.vendor_name) as Distributor,
    '      max(TSPL_DEMAND_BOOKING_MASTER.shiftType) as shiftType,
    '      max(TSPL_city_MASTER.City_Name) as City_Name,
    '      max(TSPL_DEMAND_BOOKING_MASTER.Comp_Code) as Comp_Code,
    '      max(TSPL_DEMAND_BOOKING_MASTER.location_code) as location_code
    '      ,TSPL_DEMAND_BOOKING_MASTER.Document_No as Demand_No,max(convert(varchar(15),TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)) as Demand_Date ,isnull(TSPL_DEMAND_BOOKING_MASTER.Route_No,'') as Route_No ,max(isnull(TSPL_ROUTE_MASTER.Route_Desc,'')) as Route_Desc 
    '      ,max(TSPL_ITEM_MASTER.alies_name) as Item_alies_name
    '      ,sum(case when TSPL_DEMAND_BOOKING_DETAIL.unit_code='Crate' then TSPL_DEMAND_BOOKING_DETAIL.Qty else 0 end) AS Crate_Qty
    ',sum(case when TSPL_DEMAND_BOOKING_DETAIL.unit_code='Pouch' then TSPL_DEMAND_BOOKING_DETAIL.Qty else 0 end) AS Pouch_Qty
    ',sum(case when (TSPL_DEMAND_BOOKING_DETAIL.unit_code<>'Crate' and TSPL_DEMAND_BOOKING_DETAIL.unit_code<>'Pouch') then TSPL_DEMAND_BOOKING_DETAIL.Qty else 0 end) AS Loose_Qty
    ',sum(TSPL_DEMAND_BOOKING_DETAIL.TotalLtr_ItemWise) AS TotalLtr_ItemWise
    '      from TSPL_DEMAND_BOOKING_MASTER left outer join TSPL_DEMAND_BOOKING_DETAIL
    '      on TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No 
    '       left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_DEMAND_BOOKING_DETAIL.Cust_Code 
    '      left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code 
    '       left outer join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id  =TSPL_DEMAND_BOOKING_DETAIL.Vehicle_Code 
    '      left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = TSPL_DEMAND_BOOKING_MASTER.Route_No
    '      left outer join TSPL_city_MASTER on TSPL_city_MASTER.city_code = TSPL_DEMAND_BOOKING_MASTER.city_code
    '      left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.vendor_code=TSPL_VEHICLE_MASTER.Transport_Id
    '      where convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & " ' And  
    '      TSPL_DEMAND_BOOKING_MASTER.Location_Code='" & txtLocCode.Value & "' and TSPL_DEMAND_BOOKING_DETAIL.Vehicle_Code='" + txtVehicle.Value + "'
    '      and TSPL_DEMAND_BOOKING_DETAIL.ShiftType='" & IIf(rbtnMorning.IsChecked = True, "Morning", "Evening") & "'
    '       group by TSPL_DEMAND_BOOKING_DETAIL.Item_Code,TSPL_DEMAND_BOOKING_MASTER.Document_No,TSPL_DEMAND_BOOKING_MASTER.Route_No
    '      ) as Main_Final
    '      LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code=Main_Final.Comp_Code
    '       LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =Main_Final.location_code"
    '      Return Qry
    '  End Function

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        '=====update by preeti gupta Against ticket no[ERO/05/09/19-001019,ERO/05/09/19-001020,TEC/20/05/19-000509]
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "No data found to Print", Me.Text)
        Else
            funPrint(txtCode.Value)
        End If
    End Sub
    Private Sub btnPrint2_Click(sender As Object, e As EventArgs) Handles btnPrint2.Click
        '=====update by preeti gupta Against ticket no[ERO/05/09/19-001019,ERO/05/09/19-001020,TEC/20/05/19-000509]
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "No data found to Print", Me.Text)
        Else
            funPrint2(txtCode.Value)
        End If
    End Sub

    Private Sub fndRouteNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndRouteNo._MYValidating
        If CreateGatePassFromDemand = True Then
            strQuery = " Select distinct Route_No As Code,route_desc from (" &
            " Select distinct TSPL_DEMAND_BOOKING_MASTER.Route_No,tspl_route_master.route_desc from TSPL_DEMAND_BOOKING_MASTER " &
            " left outer join tspl_route_master On tspl_route_master.route_no=TSPL_DEMAND_BOOKING_MASTER.route_no " &
            " where TSPL_DEMAND_BOOKING_MASTER.Route_No<>''" &
            " ) final"
        Else
            If chkAgainstTransfer.Checked = True Then
                strQuery = " select distinct Route_No as Code,route_desc from (select Route_No ,route_desc from tspl_route_master where Status='A' ) final"
            Else
                strQuery = " Select distinct Route_No As Code,route_desc from (" &
    " Select distinct TSPL_SD_SHIPMENT_HEAD.Route_No,tspl_route_master.route_desc from TSPL_SD_SHIPMENT_HEAD " &
    " left outer join tspl_route_master On tspl_route_master.route_no=TSPL_SD_SHIPMENT_HEAD.route_no " &
    " where TSPL_SD_SHIPMENT_HEAD.screen_type='DS' and TSPL_SD_SHIPMENT_HEAD.Route_No<>''" &
    " ) final"
            End If
        End If

        fndRouteNo.Value = clsCommon.ShowSelectForm("DSRoute", strQuery, "code", "", fndRouteNo.Value, "", isButtonClicked)
        If clsCommon.myLen(fndRouteNo.Value) > 0 Then
            txtRouteName.Text = clsDBFuncationality.getSingleValue("select Route_Desc from TSPL_ROUTE_MASTER where Route_No='" & fndRouteNo.Value & "'")
            If CreateGatePassFromDemand = True Then
                Dim StrGP_No As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select GPCode from TSPL_DAIRYSALE_GATEPASS_MASTER where convert(date, TSPL_DAIRYSALE_GATEPASS_MASTER.gpdate ,103)=convert(date,'" & txtDate.Value & "',103) and Route_No='" & fndRouteNo.Value & "'"))
                If clsCommon.myLen(StrGP_No) > 0 Then
                    clsCommon.MyMessageBoxShow(Me, "GatePass Already Created.", Me.Text)
                    LoadData(StrGP_No, NavigatorType.Current)
                Else
                    setRouteVehicleDetail()
                End If
            End If
        Else
            fndRouteNo.Value = ""
            txtRouteName.Text = ""
        End If
        strQuery = ReturnVehicle()
        strQuery += " where tspl_route_master.Route_No='" + clsCommon.myCstr(fndRouteNo.Value) + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQuery)
        If dt.Rows IsNot Nothing AndAlso dt.Rows.Count = 1 Then
            txtVehicle.Value = clsCommon.myCstr(dt.Rows(0)("Code"))
            lblVehicleDesc.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
        End If
        If clsCommon.myLen(fndRouteNo.Value) > 0 Then
            dt = Nothing
            strQuery = "select Location_Code from TSPL_ROUTE_MASTER where Route_No ='" + clsCommon.myCstr(fndRouteNo.Value) + "'"
            dt = clsDBFuncationality.GetDataTable(strQuery)
            If dt.Rows IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                txtLocCode.Value = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
                txtLocDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocCode.Value + "'"))
            End If
        End If
    End Sub

    Sub setRouteVehicleDetail()
        Try
            Dim qry As String = ""
            qry = "select TSPL_ROUTE_MASTER.City_Code,Customer_Name,vehicle_code,TSPL_VEHICLE_MASTER.Vehicle_No,Zone_Code,TSPL_CUSTOMER_MASTER.Route_No,Number,TSPL_ROUTE_MASTER.Route_Desc,tspl_customer_master.price_CodeNon,tspl_transport_master.Transporter_Name,TSPL_VEHICLE_MASTER.Transport_Id from TSPL_CUSTOMER_MASTER left outer join " &
                "TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No left outer join TSPL_VEHICLE_MASTER on " &
                "TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id left outer join tspl_transport_master on tspl_transport_master.Transport_Id=TSPL_VEHICLE_MASTER.Transport_Id "
            qry += " where TSPL_ROUTE_MASTER.Route_No ='" & fndRouteNo.Value & "'"


            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
            If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                txtVehicle.Value = clsCommon.myCstr(dt1.Rows(0)("vehicle_code"))
                lblVehicleDesc.Text = clsCommon.myCstr(dt1.Rows(0)("Number"))
                fndRouteNo.Value = clsCommon.myCstr(dt1.Rows(0)("Route_No"))
                txtRouteName.Text = clsCommon.myCstr(dt1.Rows(0)("Route_Desc"))
                txtTransporter.Text = clsCommon.myCstr(dt1.Rows(0)("Transport_Id"))

                If clsCommon.myLen(clsCommon.myCstr(txtVehicle.Value)) <= 0 Then
                    Throw New Exception("Please Map Vehicle with Route " & txtRouteName.Text & "")
                End If

            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub txtmultiBooking__My_Click(sender As Object, e As EventArgs) Handles txtmultiBooking._My_Click
        Dim qry As String = LoadQuery("")
        Dim strAllDoc As String = " select distinct PPPP.[Document No] as code, convert (varchar, PPPP .[Document Date], 103) as [Document Date] from  ( " & qry & "    ) As PPPP   "
        txtmultiBooking.arrValueMember = clsCommon.ShowMultipleSelectForm("TransType@@@@MulSel", strAllDoc, "Code", "code", txtmultiBooking.arrValueMember, txtmultiBooking.arrDispalyMember)
        funFillGrid2()
        TotalQty()
    End Sub

    Private Sub TotalQty()
        Dim TotalCrate As Integer = 0
        Dim TotalCan As Integer = 0
        For i As Integer = 0 To Gv1.Rows.Count - 1
            If clsCommon.CompairString(clsCommon.myCstr(Gv1.Rows(i).Cells(colUnit).Value), "Crate") = CompairStringResult.Equal Then
                TotalCrate = TotalCrate + clsCommon.myCdbl(Gv1.Rows(i).Cells(colQty).Value)
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.Rows(i).Cells(colUnit).Value), "Can") = CompairStringResult.Equal Then
                '    TotalCan = TotalCan + clsCommon.myCdbl(Gv1.Rows(i).Cells(colQty).Value)
            End If

        Next
        If clsCommon.myCdbl(TotalCrate) > 0 Then
            txtCrateQty.Text = TotalCrate
            'Else
            '    txtCrateQty.Text = 0
        End If
    End Sub
    Private Sub funFillGrid2()
        Try

            LoadBlankGrid()
            Dim qry As String = LoadQuery("")
            strQuery = "select [Item Code],max([Item Desc]) as [Item Desc],Unit,sum(qty) as Quantity,max(HSN_Code) as HSN_Code  from ( select xxx.* from(  " & qry & " )xxx  where xxx.[Document No] in (" + clsCommon.GetMulcallString(txtmultiBooking.arrValueMember) + ")  )  final group by [Item Code],Unit "

            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(strQuery)
            Dim intLineNo As Integer = 0
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    Gv1.Rows.AddNew()
                    intLineNo += 1
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colLineNo).Value = intLineNo
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colItemCode).Value = clsCommon.myCstr(dr("Item Code"))
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colItemDesc).Value = clsCommon.myCstr(dr("Item Desc"))
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colUnit).Value = clsCommon.myCstr(dr("Unit"))
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colQty).Value = clsCommon.myCstr(dr("Quantity"))
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colHSNCode).Value = clsCommon.myCstr(dr("HSN_Code"))
                    txtDate.Enabled = False
                Next
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "GatePass Entry", MessageBoxButtons.OK, Me.Text)
        End Try
    End Sub

    Private Sub btnClKM_Click(sender As Object, e As EventArgs) Handles btnClKM.Click
        Try
            If chkAgainstTransfer.Checked = True Then
                Dim strTransferIn As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_No  from TSPL_TRANSFER_ORDER_HEAD where TransferOutNo ='" & clsCommon.myCstr(FndTransferNo.Value) & "' And TSPL_TRANSFER_ORDER_HEAD.status = 1"))
                If clsCommon.myLen(strTransferIn) > 0 Then
                    Dim strTransferReturnAgainstIn As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_No  from TSPL_TRANSFER_RETURN where Transfer_No ='" & strTransferIn & "'"))
                    If clsCommon.myLen(strTransferReturnAgainstIn) > 0 Then
                        Throw New Exception("Please create Transfer In Against Transfer Out No " & FndTransferNo.Value & " ")
                    End If
                Else
                    Throw New Exception("Please create Transfer In Against Transfer Out No " & FndTransferNo.Value & "")
                End If
            End If

            Dim dclDistanceInRoute As String = Nothing
            Dim dclPriceKM As String = Nothing
            Dim dclTollAmt As String = Nothing
            Dim qry As String = " select TSPL_ROUTE_MASTER.Distance,TSPL_VEHICLE_MASTER.Price_KM,  isnull (TSPL_ROUTE_MASTER.Toll_amount,0) as Toll_Amount from TSPL_DAIRYSALE_GATEPASS_MASTER left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = TSPL_DAIRYSALE_GATEPASS_MASTER.Route_No " &
                                " left outer join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_DAIRYSALE_GATEPASS_MASTER.Vehicle_Id " &
                                " where GPCode = '" + txtCode.Value + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                dclDistanceInRoute = clsCommon.myCdbl(dt.Rows(0)("Distance"))
                dclPriceKM = clsCommon.myCdbl(dt.Rows(0)("Price_KM"))
                dclTollAmt = clsCommon.myCdbl(dt.Rows(0)("Toll_Amount"))

            End If
            If IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TollTaxMaster, clsFixedParameterCode.TollTaxMaster, Nothing)) = 0, False, True) = False Then
                dclTollAmt = txtTollAmount.Text
            End If
            clsDairyGatePassEntry.UpdateClosingKMAndCreateProvision(txtCode.Value, txtClKM.Value, dclDistanceInRoute, dclPriceKM, dclTollAmt, MyBase.Form_ID)
            clsCommon.MyMessageBoxShow(Me, "Closing KM Update successfully", Me.Text)
            LoadData(txtCode.Value, NavigatorType.Current)
        Catch ex As Exception
            If clsCommon.myLen(ex.Message) > 0 Then
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End If
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsDairyGatePassEntry.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    btnNew.PerformClick()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub btnReverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReverse.Click
        Try
            If common.clsCommon.MyMessageBoxShow(Me, "Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                If clsDairyGatePassEntry.ReverseAndUnpost(txtCode.Value) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed", Me.Text)
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub chkAgainstTransfer_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkAgainstTransfer.ToggleStateChanged
        Try
            If chkAgainstTransfer.Checked = True Then
                FndTransferNo.Visible = True
                MyLabel9.Visible = True
            Else
                FndTransferNo.Visible = False
                MyLabel9.Visible = False
            End If
            FndTransferNo.Value = ""
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FndTransferNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles FndTransferNo._MYValidating
        Dim sQuery As String = String.Empty
        Dim whrcls As String = ""
        If chkAgainstTransfer.Checked = True Then
            sQuery = "select TSPL_TRANSFER_ORDER_HEAD.Document_NO as DocNo,CONVERT(varchar, TSPL_TRANSFER_ORDER_HEAD.Document_Date,103) as DocDate,From_Location as " _
                       & " [From Location Code],frm.Location_Desc as [From Location],fto.Location_Code as [To Location Code],fto.Location_Desc as [To Location],DOC_Total_Amt as [Amount]  " _
                       & " from TSPL_TRANSFER_ORDER_HEAD left join tspl_Location_Master frm on frm.location_Code=TSPL_TRANSFER_ORDER_HEAD.From_Location left join " _
                       & " tspl_Location_Master fto on fto.GIT_Location=TSPL_TRANSFER_ORDER_HEAD.to_Location left join TSPL_TRANSFER_RETURN on transfer_No=TSPL_TRANSFER_ORDER_HEAD.Document_NO"
            whrcls = "  TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='O' and TSPL_TRANSFER_ORDER_HEAD.Status =1 and  TSPL_TRANSFER_ORDER_HEAD.Document_No not  in (select coalesce(TransferOutNo,'') from TSPL_TRANSFER_ORDER_HEAD where Document_No not in (select Transfer_No from TSPL_TRANSFER_RETURN) union all select AgainstTransferNo from TSPL_DAIRYSALE_GATEPASS_MASTER  where isnull(AgainstTransferNo ,'')<>'' ) and TSPL_TRANSFER_RETURN.transfer_No is Null and TSPL_TRANSFER_ORDER_HEAD.internalTransfer=0  and convert(date, TSPL_TRANSFER_ORDER_HEAD.Document_Date ,103)<=convert(date,'" & txtDate.Value & "',103)"

            '    sQuery = " Select TSPL_TRANSFER_ORDER_HEAD.Document_NO as DocNo,CONVERT(varchar, TSPL_TRANSFER_ORDER_HEAD.Document_Date,103) as DocDate,From_Location as  [From Location Code],frm.Location_Desc as [From Location],fto.Location_Code as [To Location Code],fto.Location_Desc as [To Location],DOC_Total_Amt as [Amount]  from TSPL_TRANSFER_ORDER_HEAD " & Environment.NewLine & _
            '" left join tspl_Location_Master frm on frm.location_Code=TSPL_TRANSFER_ORDER_HEAD.From_Location left join  tspl_Location_Master fto on fto.GIT_Location=TSPL_TRANSFER_ORDER_HEAD.to_Location "

            '    whrcls = " Document_No in (Select TransferOutNo from TSPL_TRANSFER_ORDER_HEAD where isnull (TransferOutNo ,'') in (select TSPL_TRANSFER_ORDER_HEAD.Document_NO as DocNo from TSPL_TRANSFER_ORDER_HEAD  left join TSPL_TRANSFER_RETURN on transfer_No=TSPL_TRANSFER_ORDER_HEAD.Document_NO where " & Environment.NewLine & _
            '" TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='O' and TSPL_TRANSFER_ORDER_HEAD.Status =1 and  TSPL_TRANSFER_ORDER_HEAD.Document_No not  in (select coalesce(TransferOutNo,'') from TSPL_TRANSFER_ORDER_HEAD where Document_No not in (select Transfer_No from TSPL_TRANSFER_RETURN) and TSPL_TRANSFER_ORDER_HEAD.Transfer_Type ='O' " & Environment.NewLine & _
            '" union all select AgainstTransferNo from TSPL_DAIRYSALE_GATEPASS_MASTER  where isnull(AgainstTransferNo ,'')<>'') and TSPL_TRANSFER_RETURN.transfer_No is Null " & Environment.NewLine & _
            '" --and TSPL_TRANSFER_ORDER_HEAD.Document_No in ('TTO-001/20-21/000294','TNT-001/20-21/00023') " & Environment.NewLine & _
            '" )) "


            FndTransferNo.Value = clsCommon.ShowSelectForm("SRNRetSRNFndOI", sQuery, "DocNo", whrcls, FndTransferNo.Value, "TSPL_TRANSFER_ORDER_HEAD.Document_Date", isButtonClicked, "TSPL_TRANSFER_ORDER_HEAD.Document_Date")
            If clsCommon.myLen(FndTransferNo.Value) > 0 Then
                txtLocCode.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select FROM_LOCATION from TSPL_TRANSFER_ORDER_HEAD WHERE DOCUMENT_NO='" & FndTransferNo.Value & "'"))
                txtLocDesc.Text = clsDBFuncationality.getSingleValue("select  Location_Desc  from TSPL_LOCATION_MASTER where Location_Code='" & txtLocCode.Value & "'")
                txtLocCode.Enabled = False
            Else
                txtLocCode.Value = ""
                txtLocDesc.Text = ""
                txtLocCode.Enabled = True
            End If
        Else
            clsCommon.MyMessageBoxShow(Me, "Please check Against Transfer checkbox first", Me.Text)
        End If

    End Sub

    Private Sub funFillGrid_Transfer()
        Try

            LoadBlankGrid()
            Dim qry As String = "select TSPL_TRANSFER_ORDER_DETAIL.Item_Code as [Item Code],TSPL_TRANSFER_ORDER_DETAIL.Item_Desc as [Item Desc],TSPL_TRANSFER_ORDER_DETAIL.Unit_code as Unit ,TSPL_TRANSFER_ORDER_DETAIL.Out_Qty as Qty,TSPL_ITEM_MASTER.HSN_Code,TSPL_TRANSFER_ORDER_HEAD.From_Location ,TSPL_TRANSFER_ORDER_HEAD.Vehicle_Code   from TSPL_TRANSFER_ORDER_HEAD " & Environment.NewLine &
" left outer join TSPL_TRANSFER_ORDER_DETAIL on TSPL_TRANSFER_ORDER_HEAD.Document_No =TSPL_TRANSFER_ORDER_DETAIL.Document_No " & Environment.NewLine &
" left outer join TSPL_ITEM_MASTER on TSPL_TRANSFER_ORDER_DETAIL.Item_Code =TSPL_ITEM_MASTER.Item_Code " & Environment.NewLine &
             " where 1=1 and TSPL_TRANSFER_ORDER_HEAD.Document_No='" + FndTransferNo.Value + "' "


            strQuery = "select [Item Code],max([Item Desc]) as [Item Desc],Unit,sum(qty) as Quantity,max(HSN_Code) as HSN_Code,max(From_Location) as LOC ,max(Vehicle_Code) as Vehicle_Code  from ( " & qry & " ) final group by [Item Code],Unit "

            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(strQuery)
            Dim intLineNo As Integer = 0
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                txtLocCode.Value = clsCommon.myCstr(dt.Rows(0)("LOC"))
                txtLocDesc.Text = clsLocation.GetName(clsCommon.myCstr(dt.Rows(0)("LOC")), Nothing)
                If clsCommon.myLen(clsCommon.myCstr(dt.Rows(0)("Vehicle_Code"))) > 0 Then
                    txtVehicle.Value = clsCommon.myCstr(dt.Rows(0)("Vehicle_Code"))
                End If
                lblVehicleDesc.Text = clsDBFuncationality.getSingleValue("select Description from TSPL_VEHICLE_MASTER where Vehicle_Id='" & txtVehicle.Value & "'")
                If isCreateProvisionOfTransporterInDairyDispatch = True Then
                    txtTransporter.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Transport_Id from TSPL_VEHICLE_MASTER where Vehicle_Id='" & txtVehicle.Value & "'"))
                End If
                txtLocCode.Enabled = False
                For Each dr As DataRow In dt.Rows
                    Gv1.Rows.AddNew()
                    intLineNo += 1
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colLineNo).Value = intLineNo
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colItemCode).Value = clsCommon.myCstr(dr("Item Code"))
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colItemDesc).Value = clsCommon.myCstr(dr("Item Desc"))
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colUnit).Value = clsCommon.myCstr(dr("Unit"))
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colQty).Value = clsCommon.myCstr(dr("Quantity"))
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colHSNCode).Value = clsCommon.myCstr(dr("HSN_Code"))
                    txtDate.Enabled = False

                Next

                Dim TotalCrate As Integer = 0
                Dim TotalCan As Integer = 0
                For i As Integer = 0 To Gv1.Rows.Count - 1
                    If clsCommon.CompairString(clsCommon.myCstr(Gv1.Rows(i).Cells(colUnit).Value), "Crate") = CompairStringResult.Equal Then
                        TotalCrate = TotalCrate + clsCommon.myCdbl(Gv1.Rows(i).Cells(colQty).Value)
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.Rows(i).Cells(colUnit).Value), "Can") = CompairStringResult.Equal Then
                        TotalCan = TotalCan + clsCommon.myCdbl(Gv1.Rows(i).Cells(colQty).Value)
                    End If

                Next
                If clsCommon.myCdbl(TotalCrate) > 0 Then
                    txtCrateQty.Text = TotalCrate
                Else
                    txtCrateQty.Text = 0
                End If
                If clsCommon.myCdbl(TotalCan) > 0 Then
                    txtCanQty.Text = TotalCan
                Else
                    txtCanQty.Text = 0
                End If
                ' **************************************************************************************************
                'txtmultiBooking.Enabled = True
                ''Dim strAllDoc As String = " select STUFF((SELECT ',' + Document_Code from (select distinct PPPP.Document_Code from  ( " & qry & "    ) As PPPP  ) Final FOR XML PATH('')), 1, 1, '') "
                'Dim strAllDoc As String = " select distinct PPPP.[Document No] from  ( " & qry & "    ) As PPPP   "
                'Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(strAllDoc)
                'Dim list As New ArrayList
                'If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                '    For Each dr As DataRow In dt1.Rows
                '        list.Add(dr("Document No"))
                '    Next
                'End If
                'txtmultiBooking.arrValueMember = list



                ' **************************************************************************************************
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "GatePass Entry", MessageBoxButtons.OK, Me.Text)
        End Try
    End Sub

    Private Sub TxtMultiDairyGPassReverse__My_Click(sender As Object, e As EventArgs) Handles TxtMultiDairyGPassReverse._My_Click
        Dim qry As String = "select GPCode,GPDate,Location_Code,Vehicle_Id,Route_No,TSPL_PROVISION_ENTRY_KNOCKOFF.Provision_No,TSPL_PROVISION_ENTRY_KNOCKOFF.AP_Invoice_No  from TSPL_DAIRYSALE_GATEPASS_MASTER left outer join TSPL_PROVISION_ENTRY_KNOCKOFF on TSPL_PROVISION_ENTRY_KNOCKOFF .Invoice_No =TSPL_DAIRYSALE_GATEPASS_MASTER.GPCode where Post='Y'"
        TxtMultiDairyGPassReverse.arrValueMember = clsCommon.ShowMultipleSelectForm("Dairy@GPassRev", qry, "GPCode", "GPCode", TxtMultiDairyGPassReverse.arrValueMember, TxtMultiDairyGPassReverse.arrDispalyMember)
    End Sub

    Private Sub BtnMultiGPReverse_Click(sender As Object, e As EventArgs) Handles btnMultiGPReverse.Click
        Try
            If TxtMultiDairyGPassReverse.arrValueMember Is Nothing OrElse TxtMultiDairyGPassReverse.arrValueMember.Count <= 0 Then
                Throw New Exception("Please select At least One Document To unpost")
            End If
            Dim StrAllException As String = ""
            For ii As Integer = 0 To TxtMultiDairyGPassReverse.arrValueMember.Count - 1
                Try
                    clsDairyGatePassEntry.ReverseAndUnpost(TxtMultiDairyGPassReverse.arrValueMember(ii))
                Catch ex As Exception
                    StrAllException += "Error In Document no:" + TxtMultiDairyGPassReverse.arrValueMember(ii) + Environment.NewLine + ex.Message
                End Try
            Next
            If clsCommon.myLen(StrAllException) > 0 Then
                clsCommon.MyMessageBoxShow(Me, StrAllException, Me.Text)
            Else
                TxtMultiDairyGPassReverse.arrValueMember = Nothing
                clsCommon.MyMessageBoxShow(Me, "Successfully reverse and unposted", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub lblVehicleDesc_Leave(sender As Object, e As EventArgs) Handles lblVehicleDesc.Leave
        If clsCommon.myLen(lblVehicleDesc.Text) > 0 Then
            VehicleDesc = lblVehicleDesc.Text
        End If
    End Sub

    Private Sub btnGPCancel_Click(sender As Object, e As EventArgs) Handles btnGPCancel.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Document No not found to Post", Me.Text)
                Exit Sub
            End If

            Dim isCancel As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select count (*) from TSPL_DAIRYSALE_GATEPASS_MASTER where GPCode = '" + txtCode.Value + "' and Status = 'Y'"))
            If isCancel = True Then
                common.clsCommon.MyMessageBoxShow(Me, "Record Already canceled.", Me.Text)
                Exit Sub
            End If
            If myMessages.cancelConfirm() Then
                If (clsDairyGatePassEntry.CancelData(MyBase.Form_ID, txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully canceled", Me.Text)
                    LoadData(txtCode.Value, NavigatorType.Current)
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                End If

                'clsDBFuncationality.ExecuteNonQuery("Update TSPL_DAIRYSALE_GATEPASS_MASTER set post='Y' where gpcode='" & txtCode.Value & "'")

            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub

    Private Sub Gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles Gv1.CellValueChanged
        Try
            If Not isInsideLoadData Then
                Dim Qry As String = Nothing
                Dim dt As DataTable = Nothing
                If clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells(colUnit).Value), "Crate") = CompairStringResult.Equal Then
                    Qry = "Select TSPL_SD_SHIPMENT_DETAIL.PK_ID,TSPL_SD_SHIPMENT_DETAIL.Item_Code,TSPL_SD_SHIPMENT_DETAIL.Unit_Code,IsNull(GPUsed.GP_Qty,0)GP_Qty,TSPL_SD_SHIPMENT_DETAIL.Qty As TotalQty,
                            Case When IsNull(GPUsed.GP_Qty,0)>0 Then (TSPL_SD_SHIPMENT_DETAIL.Qty-GPUsed.GP_Qty) Else TSPL_SD_SHIPMENT_DETAIL.Qty End As BalanceQty
                            from TSPL_SD_SHIPMENT_DETAIL Left Join(select  PK_ID,Max(GPCode)GPCode,Max(Item_Code)Item_Code,Max(Unit_Code)Unit_Code,Sum(IsNull(GP_Qty,0))GP_Qty 
                            from TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL Group By PK_ID) AS GPUsed On GPUsed.PK_ID=TSPL_SD_SHIPMENT_DETAIL.PK_ID
                            Where TSPL_SD_SHIPMENT_DETAIL.PK_ID='" + clsCommon.myCstr(Gv1.CurrentRow.Cells(colPKID).Value) + "'"
                    dt = clsDBFuncationality.GetDataTable(Qry)
                    If clsCommon.myCdbl(Gv1.CurrentRow.Cells(colQty).Value) > 0 Then
                        If dt.Rows IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            If clsCommon.myCDecimal(Gv1.CurrentRow.Cells(colQty).Value) > clsCommon.myCDecimal(dt.Rows(0)("BalanceQty")) AndAlso clsCommon.myCDecimal(dt.Rows(0)("BalanceQty")) > 0 Then
                                clsCommon.MyMessageBoxShow(Me, "Balance Qty: " + clsCommon.myCstr(dt.Rows(0)("BalanceQty")) + Environment.NewLine + "Allow Only Maximum " + clsCommon.myCstr(dt.Rows(0)("BalanceQty")) + " Qty.", Me.Text)
                                Gv1.CurrentRow.Cells(colQty).Value = clsCommon.myCDecimal(dt.Rows(0)("BalanceQty"))
                            End If
                        End If
                        'ElseIf clsCommon.myCdbl(Gv1.CurrentRow.Cells(colQty).Value) <= 0 AndAlso dt.Rows IsNot Nothing AndAlso dt.Rows.Count > 0 AndAlso clsCommon.myCDecimal(dt.Rows(0)("BalanceQty")) > 0 Then
                        '    Gv1.CurrentRow.Cells(colQty).Value = clsCommon.myCDecimal(dt.Rows(0)("BalanceQty"))
                    End If
                End If
                isInsideLoadData = False
            End If

        Catch ex As Exception
        Finally
            TotalQty()
        End Try
    End Sub

    Private Sub lblVehicleDesc_TextChanged(sender As Object, e As EventArgs) Handles lblVehicleDesc.TextChanged
        If clsCommon.myCstr(VehicleDesc) Is Nothing AndAlso clsCommon.myLen(VehicleDesc) <= 0 Then
            VehicleDesc = lblVehicleDesc.Text
        End If
    End Sub


End Class
