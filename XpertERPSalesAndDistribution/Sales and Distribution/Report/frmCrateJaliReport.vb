Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
'Checkin sanjay 19/06/2020
'=============================added by preeti gupta=========================='ticket no [BM00000009682],[BHA/22/11/18-000705],[BHA/26/06/19-000912]
Public Class FrmCrateJaliReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim strQry As String = ""
    Dim ItemDefaultCanRate As Integer = 0
    Dim ItemDefaultCrateRate As Integer = 0
    Dim ItemDefaultJalliRate As Integer = 0
    Dim ItemDefaultBoxRate As Integer = 0
    Private CrateReceivingWithMultipleRoute As Boolean = False
    Private CrateReceiveddairyCustomerWise As Boolean = False

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.rptCrateAccountingReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
        'btnQuickExport.Visible = MyBase.isExport
    End Sub

    Enum Exporter
        Excel = 0
        PDF = 1
        Print = 2
        Refresh = 3
    End Enum
    Sub FormatGridDetails()

        Gv1.TableElement.TableHeaderHeight = 20
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = False
        Next

        Gv1.Columns("CustomerCode").IsVisible = True
        Gv1.Columns("CustomerCode").Width = 100
        Gv1.Columns("CustomerCode").HeaderText = "Customer Code"

        Gv1.Columns("CustomerName").IsVisible = True
        Gv1.Columns("CustomerName").Width = 100
        Gv1.Columns("CustomerName").HeaderText = "Customer Name"

        Gv1.Columns("VehicleCode").IsVisible = True
        Gv1.Columns("VehicleCode").Width = 100
        Gv1.Columns("VehicleCode").HeaderText = "Vehicle Code"


        Gv1.Columns("Opening").IsVisible = True
        Gv1.Columns("Opening").Width = 100
        Gv1.Columns("Opening").HeaderText = "Opening"

        Gv1.Columns("Issue").IsVisible = True
        Gv1.Columns("Issue").Width = 100
        Gv1.Columns("Issue").HeaderText = "Issue"

        Gv1.Columns("Receive").IsVisible = True
        Gv1.Columns("Receive").Width = 100
        Gv1.Columns("Receive").HeaderText = "Receive"

        Gv1.Columns("Closing").IsVisible = True
        Gv1.Columns("Closing").Width = 100
        Gv1.Columns("Closing").HeaderText = "Closing"

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        Dim item1 As New GridViewSummaryItem("Opening", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("Issue", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("Receive", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Dim item4 As New GridViewSummaryItem("Closing", "{0:F2}", GridAggregateFunction.Sum)
        Gv1.ShowGroupPanel = True
        Gv1.MasterTemplate.AutoExpandGroups = True

        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub

    Private Sub txtVehicle__My_Click(sender As Object, e As EventArgs) Handles txtVehicle._My_Click
        strQry = "Select TSPL_VEHICLE_MASTER.Vehicle_Id As Code,  TSPL_VEHICLE_MASTER.Description As Name From TSPL_VEHICLE_MASTER"
        txtVehicle.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtVehicle.arrValueMember, txtVehicle.arrDispalyMember)
    End Sub


    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        strQry = " select Cust_Code as [code],Customer_Name as [Name] from TSPL_CUSTOMER_MASTER"
        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
    End Sub


    Private Sub FrmCrateJaliReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ItemDefaultCanRate = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ItemCanRate, clsFixedParameterCode.ItemCanRate, Nothing))
        ItemDefaultCrateRate = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ItemCrateRate, clsFixedParameterCode.ItemCrateRate, Nothing))
        ItemDefaultJalliRate = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ItemJaaliRate, clsFixedParameterCode.ItemJaaliRate, Nothing))
        ItemDefaultBoxRate = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ItemBoxRate, clsFixedParameterCode.ItemBoxRate, Nothing))
        CrateReceivingWithMultipleRoute = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CrateReceivingWithMultipleRoute, clsFixedParameterCode.CrateReceivingWithMultipleRoute, Nothing)) = 1, True, False)
        CrateReceiveddairyCustomerWise = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CrateReceiveddairyCustomerWise, clsFixedParameterCode.CrateReceiveddairyCustomerWise, Nothing)) = 1, True, False)
        SetUserMgmtNew()
        If CrateReceivingWithMultipleRoute = False Then
            RadGroupBox1.Visible = True
        End If
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+N Refresh ")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+R ")
        Reset()
    End Sub



    'Sub Print(ByVal IsPrint As Exporter)
    '    FormatGridDetails()
    'End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = clsCommon.myCstr(MyBase.Form_ID) + IIf(CrateReceivingWithMultipleRoute = True, "MR", "") + IIf(chkCustomerWise.Checked = True, "CW", IIf(chkcustomerWithDateWise.Checked = True, "CWDW", ""))
        If CrateReceivingWithMultipleRoute = True Then
            loaddataRouteWise()
        Else
            loaddata()
        End If

    End Sub

    '===========================added by preeti gupta 04/10/2016,[BHA/09/07/18-000140],[BHA/11/07/18-000145,BHA/12/07/18-000154,BHA/22/05/19-000895,BHA/01/07/19-000916]=====================
    Public Sub loaddata()
        Dim dt As DataTable

        Dim QryForCustomerOpening As String = Nothing
        Dim QryForCustomerclosing As String = Nothing
        Dim finalQueryForCustomer As String = Nothing
        Dim qry As String = Nothing



        'Sanjay BHA/15/08/18-000429 Add Route Code and Name, Route Code take from customer master as per discuss with Ranjana mam
        QryForCustomerOpening = "select "
        If chkCustomerWise.Checked Then
            QryForCustomerOpening += " max(Route_No) as Route_No,max(convert(date,'" + fromDate.Value + "',103)) as Doc_Date,max(Opening.Vehicle_Code) as Vehicle_Code ,"
        ElseIf chkcustomerWithDateWise.Checked Then
            QryForCustomerOpening += " max(Route_No) as Route_No,max(convert(date,'" + fromDate.Value + "',103)) as Doc_Date,'' as Vehicle_Code ,"
        Else
            QryForCustomerOpening += " max(Route_No) as Route_No,convert(date,'" + fromDate.Value + "',103) as Doc_Date,Opening.Vehicle_Code ,"
        End If
        '===========update by preeti gupta Against ticket no[BHA/13/05/19-000887]sale return BHA/20/06/19-000909 SHOW ONLY POSTED DATA ON REPORT

        QryForCustomerOpening += " Opening.Customer_Code ,sum(Opening.OpencrateQty*Type) as OpencrateQty,sum(Opening.OpenJaaliQty*Type ) as OpenJaaliQty,sum(Opening.OpenBoxQty *Type)  as OpenBoxQty,sum(Opening.OpenCanQty *Type)  as OpenCanQty,sum(Opening.CrateQtyRecd *Type) as CrateQtyRecd,sum(Opening.JaaliQtyRecd*Type ) as JaaliQtyRecd,sum(Opening.BoxQtyRecd*Type ) as BoxQtyRecd,sum(Opening.CanQtyRecd*Type ) as CanQtyRecd,sum(Opening.CrateOutQty*Type ) as CrateOutQty,sum(Opening.jaaliOutQty*Type ) as jaaliOutQty ,sum(Opening.boxOutQty*Type ) as boxOutQty,sum(Opening.CanOutQty*Type ) as CanOutQty,sum(Opening.CrateAdjQty*Type ) as CrateAdjQty,sum(Opening.JaaliAdjQty*Type ) as JaaliAdjQty,sum(Opening.BoxAdjQty *Type) as  BoxAdjQty ,sum(Opening.CanAdjQty *Type) as  canAdjQty from " &
                   " (" &
                    " select TSPL_SD_SHIPMENT_HEAD.Document_Date    as Document_Date,isnull(TSPL_SD_SHIPMENT_HEAD.Route_no,'') as Route_no,(case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end) as Vehicle_Code ,TSPL_SD_SHIPMENT_HEAD.customer_code  ,1 as Type  ,'O' as Type1,TSPL_SD_SHIPMENT_HEAD.Crate as OpencrateQty,TSPL_SD_SHIPMENT_HEAD.jaali as OpenJaaliQty, TSPL_SD_SHIPMENT_HEAD.box  as OpenBoxQty , TSPL_SD_SHIPMENT_HEAD.ShippedCAN  as OpenCanQty  ,0 as CrateQtyRecd,0  as JaaliQtyRecd ,0 as BoxQtyRecd ,0 as CanQtyRecd ,0 as CrateOutQty,0 as jaaliOutQty,0 as boxOutQty ,0 as CanOutQty,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty  ,0 as  CanAdjQty  from TSPL_SD_SHIPMENT_HEAD  where TSPL_SD_SHIPMENT_HEAD.screen_type='DS'  AND TSPL_SD_SHIPMENT_HEAD.Status =1 "
        If CrateReceiveddairyCustomerWise = False Then
            QryForCustomerOpening += " union all " &
                     "select TSPL_sd_SALE_RETURN_HEAD.Document_Date    as Document_Date,isnull(TSPL_sd_SALE_RETURN_HEAD.Route_no,'') as Route_no,TSPL_sd_SALE_RETURN_HEAD.Vehicle_Code ,TSPL_sd_SALE_RETURN_HEAD.customer_code  ,-1 as Type  ,'I' as Type1,TSPL_sd_SALE_RETURN_HEAD.CrateQty as OpencrateQty,TSPL_sd_SALE_RETURN_HEAD.jaali as OpenJaaliQty, TSPL_sd_SALE_RETURN_HEAD.box  as OpenBoxQty , TSPL_sd_SALE_RETURN_HEAD.ShippedCAN  as OpenCanQty  ,0 as CrateQtyRecd,0  as JaaliQtyRecd ,0 as BoxQtyRecd ,0 as CanQtyRecd ,0 as CrateOutQty,0 as jaaliOutQty,0 as boxOutQty ,0 as CanOutQty,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty  ,0 as  CanAdjQty  from TSPL_sd_SALE_RETURN_HEAD  where TSPL_sd_SALE_RETURN_HEAD.screen_type='DS'  AND TSPL_sd_SALE_RETURN_HEAD.Status =1 "
        End If
        QryForCustomerOpening += " union all " &
                    " select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as Document_Date,isnull(TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Route_code,'') as Route_no,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Customer_Code  ,1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  ," &
                    " (TSPL_CRATE_RECEIVED_detail_FRESHSALE.OutQty+TSPL_CRATE_RECEIVED_detail_FRESHSALE.Adjustment) as OpencrateQty," &
                    " TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliOutQty   as OpenjaaliQty ," &
                    " TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxOutQty  as OpenboxQty," &
                    " TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANOutQty as OpenCanQty ," &
                    " 0 as CrateQtyRecd,0 JaaliQtyRecd ," &
                    " 0 BoxQtyRecd ," &
                    " 0 CanQtyRecd ," &
                    " 0 as CrateOutQty," &
                    " 0 jaaliOutQty," &
                    " 0 boxoutqty," &
                     " 0 Canoutqty," &
                    " 0  as CrateAdjQty," &
                    " 0  as JaaliAdjQty," &
                    " 0  as BoxAdjQty," &
                      " 0  as CanAdjQty" &
                      " from TSPL_CRATE_RECEIVED_detail_FRESHSALE" &
                    " left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No " &
                    " where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='O' AND TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Posted  =1 " &
                    " union all" &
                    " select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as Document_Date,isnull(TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Route_code,'') as Route_no,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Customer_Code  ,-1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  ," &
                    " isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyRecd,0) + isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.Adjustment,0)  as OpencrateQty," &
                    " isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.JaaliQtyRecd,0) + isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliAdjustment,0)    as OpenjaaliQty ," &
                    " isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.BoxQtyRecd,0) + isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxAdjustment,0)  as OpenboxQty," &
                     " isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQtyRec,0) + isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANAdjustment,0)  as OpenCanQty," &
                    " 0 as CrateQtyRecd,0 JaaliQtyRecd ," &
                    " 0 BoxQtyRecd ," &
                    " 0 CanQtyRecd ," &
                    " 0 as CrateOutQty," &
                    " 0 jaaliOutQty," &
                    " 0 boxoutqty," &
                    " 0 Canoutqty," &
                    " 0  as CrateAdjQty," &
                     " 0  as JaaliAdjQty," &
                    " 0  as BoxAdjQty," &
                      " 0  as CanAdjQty" &
                      " from TSPL_CRATE_RECEIVED_detail_FRESHSALE" &
                    " left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No " &
                    " where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='I' AND TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Posted  =1  " &
                    " )as Opening WHERE convert(date,Document_Date,103)<(convert(date,'" + fromDate.Value + "',103))"
        If chkCustomerWise.Checked OrElse chkcustomerWithDateWise.Checked Then
            QryForCustomerOpening += " group by Customer_Code " + Environment.NewLine '----------Qry for Branch opening'
        Else
            QryForCustomerOpening += " group by Vehicle_Code,Customer_Code " + Environment.NewLine '----------Qry for Branch opening'
        End If




        QryForCustomerclosing = "select Route_No,Document_Date,Vehicle_Code,Customer_Code,0 as OpencrateQty,0 as OpenjaaliQty ,0 as OpenboxQty,0 as OpenCanQty,Case When [Type]=1 Then CrateQtyRecd Else 0 End as CrateQtyRecd,Case When [Type]=1 Then JaaliQtyRecd Else 0 End as JaaliQtyRecd,Case When [Type]=1 Then BoxQtyRecd Else 0 End as BoxQtyRecd,Case When [Type]=1 Then CANQtyRec Else 0 End as CANQtyRec," + Environment.NewLine &
                    " Case When [Type]=-1 Then CrateOutQty Else 0 End as CrateOutQty,Case When [Type]=-1 Then jaaliOutQty Else 0 End as jaaliOutQty,Case When [Type]=-1 Then boxOutQty Else 0 End as boxOutQty,Case When [Type]=-1 Then CanOutQty Else 0 End as CanOutQty,CrateAdjQty*Type as CrateAdjQty,Case When [Type]=1 Then JaaliAdjQty Else 0 End as JaaliAdjQty,Case When [Type]=1 Then BoxAdjQty Else 0 End as BoxAdjQty,Case When [Type]=1 Then CANAdjustment Else 0 End as CANAdjustment " + Environment.NewLine &
                     " from ((select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as Document_Date,isnull(TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Route_code,'') as Route_no,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Customer_Code  ,1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyManual as OpencrateQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaali  as OpenjaaliQty ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.box as OpenboxQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQty as OpenCanQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyRecd as CrateQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.JaaliQtyRecd ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.BoxQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQtyRec ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.OutQty as CrateOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANOutQty , TSPL_CRATE_RECEIVED_detail_FRESHSALE.Adjustment  as CrateAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliAdjustment  as JaaliAdjQty, TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxAdjustment  as BoxAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANAdjustment  as CANAdjustment from TSPL_CRATE_RECEIVED_detail_FRESHSALE" + Environment.NewLine &
                    " left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No " + Environment.NewLine &
                    " where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='I' AND TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Posted  =1 )" &
                    " union all " + Environment.NewLine &
                    " (select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as Document_Date,isnull(TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Route_code,'') as Route_no,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Customer_Code  ,-1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyManual as OpencrateQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaali  as OpenjaaliQty ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.box as OpenboxQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQty as OpenCANQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyRecd as CrateQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.JaaliQtyRecd ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.BoxQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQtyRec ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.OutQty as CrateOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANOutQty , TSPL_CRATE_RECEIVED_detail_FRESHSALE.Adjustment  as CrateAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliAdjustment  as JaaliAdjQty, TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxAdjustment  as BoxAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANAdjustment  as CANAdjustment from TSPL_CRATE_RECEIVED_detail_FRESHSALE" + Environment.NewLine &
                    " left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No " + Environment.NewLine &
                    " where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='O' AND TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Posted  =1 " &
                    " union all " + Environment.NewLine &
                    " select TSPL_SD_SHIPMENT_HEAD.Document_Date   as Document_Date,isnull(TSPL_SD_SHIPMENT_HEAD.Route_no,'') as Route_no ,(case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end) as Vehicle_Code ,TSPL_SD_SHIPMENT_HEAD.customer_code  ,-1 as Type  ,'O' as Type1,0 as OpencrateQty,0  as OpenBoxQty ,0 as OpenJaaliQty,0 as OpenCANQty," &
                    " 0 as CrateQtyRecd, 0 JaaliQtyRecd , " &
                    " 0  as BoxQtyRecd,0 CanQtyRecd  ,TSPL_SD_SHIPMENT_HEAD.Crate as CrateOutQty,jaali  as jaaliOutQty,TSPL_SD_SHIPMENT_HEAD.Box as boxOutQty,TSPL_SD_SHIPMENT_HEAD.ShippedCAN as CanOutQty ,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty,0 as  CanAdjQty   " &
                    " from TSPL_SD_SHIPMENT_HEAD  where TSPL_SD_SHIPMENT_HEAD.screen_type='DS' AND TSPL_SD_SHIPMENT_HEAD.Status =1"
        If CrateReceiveddairyCustomerWise = False Then
            QryForCustomerclosing += " union all " + Environment.NewLine &
            " select TSPL_sd_SALE_RETURN_HEAD.Document_Date   as Document_Date,isnull(TSPL_sd_SALE_RETURN_HEAD.Route_no,'') as Route_no,TSPL_sd_SALE_RETURN_HEAD.Vehicle_Code ,TSPL_sd_SALE_RETURN_HEAD.customer_code  ,1 as Type  ,'I' as Type1,0 as OpencrateQty,0  as OpenBoxQty ,0 as OpenJaaliQty,0 as OpenCANQty, TSPL_sd_SALE_RETURN_HEAD.CrateQty as CrateQtyRecd, TSPL_sd_SALE_RETURN_HEAD.jaali as JaaliQtyRecd ,  TSPL_sd_SALE_RETURN_HEAD.Box  as BoxQtyRecd,TSPL_sd_SALE_RETURN_HEAD.ShippedCAN CanQtyRecd  ,0 as CrateOutQty,0 as jaaliOutQty,0 as boxOutQty,0 as CanOutQty ,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty,0 as  CanAdjQty    from TSPL_sd_SALE_RETURN_HEAD  where TSPL_sd_SALE_RETURN_HEAD.screen_type='DS' AND TSPL_sd_SALE_RETURN_HEAD.Status =1 "
        End If
        QryForCustomerclosing += ") " &
                    " ) as Closing " + Environment.NewLine &
                    " WHERE convert(date,Document_Date ,103)>=convert(date,'" + fromDate.Value + "',103) AND convert(date,Document_Date,103)<=convert(date,'" + ToDate.Value + "',103) " + Environment.NewLine '----------Qry for Branch Closing'
        finalQueryForCustomer = "select "
        If chkCustomerWise.Checked Then
            finalQueryForCustomer += " max(convert(date,Doc_Date,103))  as Doc_Date,max(xx.Vehicle_Code) as Vehicle_Code"
        ElseIf chkcustomerWithDateWise.Checked Then
            finalQueryForCustomer += " (convert(date,Doc_Date,103))  as Doc_Date,'' as Vehicle_Code"
        Else
            finalQueryForCustomer += " convert(date,Doc_Date,103)  as Doc_Date,xx.Vehicle_Code as Vehicle_Code"
        End If


        finalQueryForCustomer += ",xx.Customer_Code,sum(xx.OpencrateQty) as OpencrateQty,sum(xx.OpenJaaliQty ) as OpenJaaliQty,sum(xx.OpenBoxQty )  as OpenBoxQty,sum(xx.OpenCanQty )  as OpenCanQty,sum(xx.CrateQtyRecd) as CrateQtyRecd,sum(xx.JaaliQtyRecd) as JaaliQtyRecd,sum(xx.BoxQtyRecd) as BoxQtyRecd,sum(xx.CanQtyRecd) as CanQtyRecd,sum(xx.CrateOutQty ) as CrateOutQty,sum(xx.jaaliOutQty ) as jaaliOutQty ,sum(xx.boxOutQty ) as boxOutQty,sum(xx.CanOutQty ) as CanOutQty, sum(xx.CrateAdjQty ) as CrateAdjQty ,sum(xx.JaaliAdjQty )as JaaliAdjQty  ,sum(xx.BoxAdjQty )  as BoxAdjQty ,sum(xx.CanAdjQty )  as CanAdjQty,(sum(xx.OpencrateQty)+sum(xx.CrateOutQty )-sum(xx.CrateQtyRecd)-sum(xx.CrateAdjQty )) as CrateQtyClosing," & _
                    " (sum(xx.OpenJaaliQty)+sum(xx.jaaliOutQty)-sum(xx.JaaliQtyRecd)-sum(xx.JaaliAdjQty )) as JaaliQtyClosing," & _
                    " (sum(xx.OpenBoxQty)+sum(xx.boxOutQty )-sum(xx.BoxQtyRecd)-sum(xx.BoxAdjQty )) as BoxQtyClosing " & _
                     " , (sum(xx.OpenCanQty)+sum(xx.CanOutQty )-sum(xx.CanQtyRecd)-sum(xx.CanAdjQty )) as CanQtyClosing " & _
                    " from (" & _
                    "" & QryForCustomerOpening & "" & _
                    " UNION All " + Environment.NewLine '---------------------bada wala Union(between opening and closing) 
        finalQueryForCustomer += "" & QryForCustomerclosing & "" &
                    "   ) as xx inner join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = xx.Customer_Code  "
        If chkActive.Checked Then
            finalQueryForCustomer += " AND TSPL_CUSTOMER_MASTER.Status='N'"
        ElseIf chkInactive.Checked Then
            finalQueryForCustomer += " AND TSPL_CUSTOMER_MASTER.Status='Y'"
        End If
        finalQueryForCustomer += " where 2=2 "
        If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
            finalQueryForCustomer += " and xx.Customer_Code  in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") " + Environment.NewLine
        End If
        If txtVehicle.arrValueMember IsNot Nothing AndAlso txtVehicle.arrValueMember.Count > 0 Then
            finalQueryForCustomer += " and xx.Vehicle_Code in (" + clsCommon.GetMulcallString(txtVehicle.arrValueMember) + ") " + Environment.NewLine
        End If
        If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
            finalQueryForCustomer += " and xx.Route_No in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ") " + Environment.NewLine
        End If

        If chkCustomerWise.Checked Then
            finalQueryForCustomer += " GROUP BY Customer_Code "
        ElseIf chkcustomerWithDateWise.Checked Then
            finalQueryForCustomer += " GROUP BY Customer_Code,convert(date,Doc_Date,103) "
        Else
            finalQueryForCustomer += " GROUP BY Vehicle_Code,Customer_Code,convert(date,Doc_Date,103) "
        End If


        '==========================================END CUSTOMER=========================================================================


        '      qry = "select  pp.Doc_Date  as Doc_Date,TSPL_CUSTOMER_MASTER.Route_No,TSPL_CUSTOMER_MASTER.Route_Desc,pp.Vehicle_Code,tspl_vehicle_master.Number as Vehicle_Name ,pp.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_CUSTOMER_MASTER.Phone1 as Customer_Phone ,   TSPL_CUSTOMER_MASTER.ZSM,TSPL_EMPLOYEE_MASTER_ZSM.Emp_Name as ZSM_NAME,case when len (isNull (TSPL_EMPLOYEE_MASTER_ZSM.PHONE,'')) > 0 And  len (isNull (TSPL_EMPLOYEE_MASTER_ZSM.PRESENT_MOBILE_NO,'')) > 0  then '['+ TSPL_EMPLOYEE_MASTER_ZSM.PHONE + '],['  + TSPL_EMPLOYEE_MASTER_ZSM.PRESENT_MOBILE_NO +']' when  len (isNull (TSPL_EMPLOYEE_MASTER_ZSM.PHONE,'')) > 0 then TSPL_EMPLOYEE_MASTER_ZSM.PHONE when len (isNull (TSPL_EMPLOYEE_MASTER_ZSM.PRESENT_MOBILE_NO,'')) > 0 then TSPL_EMPLOYEE_MASTER_ZSM.PRESENT_MOBILE_NO else '' end  as ZSM_Phone ,TSPL_CUSTOMER_MASTER.ASM, TSPL_EMPLOYEE_MASTER_ASM.Emp_Name as ASM_NAME, case when len (isNull (TSPL_EMPLOYEE_MASTER_ASM.PHONE,'')) > 0 And  len (isNull (TSPL_EMPLOYEE_MASTER_ASM.PRESENT_MOBILE_NO,'')) > 0  then '['+ TSPL_EMPLOYEE_MASTER_ASM.PHONE + '],['  + TSPL_EMPLOYEE_MASTER_ASM.PRESENT_MOBILE_NO +']' when  len (isNull (TSPL_EMPLOYEE_MASTER_ASM.PHONE,'')) > 0 then TSPL_EMPLOYEE_MASTER_ASM.PHONE when len (isNull (TSPL_EMPLOYEE_MASTER_ASM.PRESENT_MOBILE_NO,'')) > 0 then TSPL_EMPLOYEE_MASTER_ASM.PRESENT_MOBILE_NO else '' end  as ASM_Phone,TSPL_CUSTOMER_MASTER.ASO,TSPL_EMPLOYEE_MASTER_ASO.Emp_Name as ASO_NAME,   case when len (isNull (TSPL_EMPLOYEE_MASTER_ASO.PHONE,'')) > 0 And  len (isNull (TSPL_EMPLOYEE_MASTER_ASO.PRESENT_MOBILE_NO,'')) > 0  then '['+ TSPL_EMPLOYEE_MASTER_ASO.PHONE + '],['  + TSPL_EMPLOYEE_MASTER_ASO.PRESENT_MOBILE_NO +']' when  len (isNull (TSPL_EMPLOYEE_MASTER_ASO.PHONE,'')) > 0 then TSPL_EMPLOYEE_MASTER_ASO.PHONE when len (isNull (TSPL_EMPLOYEE_MASTER_ASO.PRESENT_MOBILE_NO,'')) > 0 then TSPL_EMPLOYEE_MASTER_ASO.PRESENT_MOBILE_NO else '' end  as ASO_Phone,pp.OpencrateQty as OpencrateQty,pp.OpenJaaliQty  as OpenJaaliQty,pp.OpenBoxQty  as OpenBoxQty,pp.OpenCanQty  as OpenCanQty,pp.CrateQtyRecd  as CrateQtyRecd,pp.JaaliQtyRecd  as JaaliQtyRecd,pp.BoxQtyRecd  as BoxQtyRecd ,pp.CanQtyRecd  as CanQtyRecd,pp.CrateOutQty  as CrateOutQty,pp.jaaliOutQty  as jaaliOutQty ,pp.boxOutQty  as boxOutQty,pp.CanOutQty  as CanOutQty ,pp.CrateQtyClosing as CrateQtyClosing, pp.JaaliQtyClosing as  JaaliQtyClosing, pp.BoxQtyClosing as BoxQtyClosing, pp.CanQtyClosing as CanQtyClosing,pp.CrateAdjQty , pp.JaaliAdjQty  , pp.BoxAdjQty , pp.CanAdjQty from ( " + Environment.NewLine & _
        '                   " " & finalQueryForCustomer & "" + Environment.NewLine & _
        '                  " ) as pp  "
        '      qry += " left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =pp.Customer_Code " + Environment.NewLine & _
        '             " left outer Join  TSPL_EMPLOYEE_MASTER as TSPL_EMPLOYEE_MASTER_ZSM on TSPL_EMPLOYEE_MASTER_ZSM.EMP_CODE = TSPL_CUSTOMER_MASTER.ZSM " + Environment.NewLine & _
        '             " left outer Join  TSPL_EMPLOYEE_MASTER as TSPL_EMPLOYEE_MASTER_ASM on TSPL_EMPLOYEE_MASTER_ASM.EMP_CODE = TSPL_CUSTOMER_MASTER.ASM  " + Environment.NewLine & _
        '             " left outer Join  TSPL_EMPLOYEE_MASTER as TSPL_EMPLOYEE_MASTER_ASO on TSPL_EMPLOYEE_MASTER_ASO.EMP_CODE = TSPL_CUSTOMER_MASTER.ASO  " + Environment.NewLine & _
        '             " left join tspl_vehicle_master on tspl_vehicle_master.vehicle_id=pp.vehicle_code where 2=2 "

        '      If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
        '          qry += " and TSPL_CUSTOMER_MASTER.Route_No in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ") " + Environment.NewLine
        '      End If

        '      Dim qryfinal As String = " With CTETemp as (" & _
        '                 " Select convert(varchar,Doc_Date,103) as Doc_Date, Route_No,Route_Desc,Vehicle_Code,Vehicle_Name,Customer_Code,Customer_Name,Customer_Phone, ZSM , ZSM_NAME,ZSM_Phone,ASM,ASM_Name,ASM_Phone,ASO , ASO_Name,ASO_Phone ,OpencrateQty, OpenJaaliQty, OpenBoxQty,OpenCanQty, CrateQtyRecd, JaaliQtyRecd, BoxQtyRecd ,CanQtyRecd,CrateOutQty," & _
        '                 " jaaliOutQty,boxOutQty  ,CanOutQty, CrateAdjQty , JaaliAdjQty  , BoxAdjQty ,CanAdjQty"
        '      If chkCustomerWise.Checked Then
        '          qryfinal += ",SUM(CrateQtyClosing) OVER (Partition BY Customer_Code ORDER BY Customer_Code,Vehicle_Code,Route_No) as CrateQtyClosing, " & _
        '                " SUM(JaaliQtyClosing) OVER (Partition BY Customer_Code ORDER BY Customer_Code,Vehicle_Code,Route_No) as JaaliQtyClosing, " & _
        '                " SUM(BoxQtyClosing) OVER (Partition BY Customer_Code ORDER BY Customer_Code,Vehicle_Code,Route_No) as BoxQtyClosing," & _
        '                " SUM(CanQtyClosing) OVER (Partition BY Customer_Code ORDER BY Customer_Code,Vehicle_Code,Route_No) as CanQtyClosing ," & _
        '                " Row_Number() OVER (Partition BY Customer_Code ORDER BY Customer_Code,Vehicle_Code,Route_No) as RowNo"
        '          'ElseIf chkcustomerWithDateWise.Checked Then

        '          '    qryfinal += ",SUM(CrateQtyClosing) OVER (Partition BY Customer_Code,Doc_Date ORDER BY Customer_Code,Doc_Date) as CrateQtyClosing, " & _
        '          '          " SUM(JaaliQtyClosing) OVER (Partition BY Customer_Code,Doc_Date ORDER BY Customer_Code,Doc_Date) as JaaliQtyClosing, " & _
        '          '          " SUM(BoxQtyClosing) OVER (Partition BY Customer_Code,Doc_Date ORDER BY Customer_Code,Doc_Date) as BoxQtyClosing," & _
        '          '          " SUM(CanQtyClosing) OVER (Partition BY Customer_Code,Doc_Date ORDER BY Customer_Code,Doc_Date) as CanQtyClosing ," & _
        '          '          " Row_Number() OVER (Partition BY Customer_Code,Doc_Date ORDER BY Customer_Code,Doc_Date) as RowNo"
        '      Else
        '          qryfinal += ",SUM(CrateQtyClosing) OVER (Partition BY Customer_Code,Vehicle_Code,Route_No ORDER BY Customer_Code, Doc_Date,Vehicle_Code,Route_No) as CrateQtyClosing, " & _
        '                " SUM(JaaliQtyClosing) OVER (Partition BY Customer_Code ,Vehicle_Code,Route_No ORDER BY Customer_Code, Doc_Date,Vehicle_Code,Route_No) as JaaliQtyClosing, " & _
        '                " SUM(BoxQtyClosing) OVER (Partition BY Customer_Code,Vehicle_Code,Route_No ORDER BY Customer_Code, Doc_Date,Vehicle_Code,Route_No) as BoxQtyClosing," & _
        '                " SUM(CanQtyClosing) OVER (Partition BY Customer_Code,Vehicle_Code,Route_No ORDER BY Customer_Code, Doc_Date,Vehicle_Code,Route_No) as CanQtyClosing ," & _
        '                " Row_Number() OVER (Partition BY Customer_Code,Vehicle_Code,Route_No ORDER BY Customer_Code, Doc_Date,Vehicle_Code,Route_No) as RowNo"
        '      End If


        '      qryfinal += " from(" + Environment.NewLine &
        '                 " " & qry & " " &
        '                 " ) YYY )" &
        '                 " Select convert(varchar,Doc_Date,103) as Date,Route_No as [Route Code],Route_Desc as [Route Name], Vehicle_Code as [Vehicle Code],Vehicle_Name as [Vehicle Name], Customer_Code as [Customer Code],Customer_Name as [Customer Name],Customer_Phone as [Customer Phone], ZSM as [ZSM Code], ZSM_Name as [ZSM Name],ZSM_Phone as [ZSM Phone],ASM as [ASM Code],ASM_NAME as [ASM Name],ASM_Phone as [ASM Phone],ASO as [ASO Code],ASO_Name as [ASO Name],ASO_Phone as [ASO Phone], OpencrateQty,OpenJaaliQty,OpenBoxQty,OpenCanQty"
        '      If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "SPMMD") = CompairStringResult.Equal Then 'For SPMMD, In view defination Issue is Third header group
        '          qryfinal += " ,CrateOutQty,jaaliOutQty,boxOutQty,CanOutQty, CrateQtyRecd, JaaliQtyRecd,BoxQtyRecd, CanQtyRecd"
        '      Else
        '          qryfinal += ", CrateQtyRecd, JaaliQtyRecd,BoxQtyRecd, CanQtyRecd,CrateOutQty,jaaliOutQty,boxOutQty,CanOutQty "
        '      End If
        '      qryfinal += ", CrateAdjQty , JaaliAdjQty  , BoxAdjQty,CanAdjQty,"
        '      ''richa ERO/22/04/19-000568 show cumulative balance when customer wise check box is off
        '      'If chkCustomerWise.Checked = False Then
        '      '    qryfinal += " SUM(OpencrateQtySum+CrateOutQty -CrateQtyRecd-CrateAdjQty) Over (Partition by Customer_Code ORDER BY RowNo) as CrateQtyClosing, " & _
        '      '   " sum(OpencanQtySum+CanOutQty-CanQtyRecd-CanAdjQty) Over (Partition by Customer_Code ORDER BY RowNo) as CanQtyClosing ,"
        '      'ElseIf chkcustomerWithDateWise.Checked = False Then
        '      '    qryfinal += " SUM(OpencrateQtySum+CrateOutQty -CrateQtyRecd-CrateAdjQty) Over (Partition by Customer_Code,doc_Date ORDER BY RowNo) as CrateQtyClosing, " & _
        '      '   " sum(OpencanQtySum+CanOutQty-CanQtyRecd-CanAdjQty) Over (Partition by Customer_Code,Doc_Date ORDER BY RowNo) as CanQtyClosing ,"
        '      'Else
        '      '    qryfinal += " OpencrateQty+CrateOutQty -CrateQtyRecd-CrateAdjQty as CrateQtyClosing," & _
        '      '                " OpenCanQty+CanOutQty-CanQtyRecd-CanAdjQty as CanQtyClosing ,"
        '      'End If

        '      If chkCustomerWise.Checked OrElse chkcustomerWithDateWise.Checked Then
        '          qryfinal += " OpencrateQty+CrateOutQty -CrateQtyRecd-CrateAdjQty as CrateQtyClosing," &
        '                      " OpenCanQty+CanOutQty-CanQtyRecd-CanAdjQty as CanQtyClosing ,"
        '      Else
        '          qryfinal += " SUM(OpencrateQtySum+CrateOutQty -CrateQtyRecd-CrateAdjQty) Over (Partition by Customer_Code, Doc_Date,Vehicle_Code,Route_No ORDER BY RowNo) as CrateQtyClosing, " &
        '        " sum(OpencanQtySum+CanOutQty-CanQtyRecd-CanAdjQty) Over (Partition by Customer_Code, Doc_Date,Vehicle_Code,Route_No ORDER BY RowNo) as CanQtyClosing ,"
        '      End If


        '      qryfinal += " OpenJaaliQty+jaaliOutQty-JaaliQtyRecd-JaaliAdjQty as JaaliQtyClosing," &
        '      " OpenBoxQty+boxOutQty-BoxQtyRecd-BoxAdjQty as BoxQtyClosing," &
        '      " (OpencrateQty+CrateOutQty -CrateQtyRecd-CrateAdjQty)* " & ItemDefaultCrateRate & " as CrateValueClosing," &
        '      " (OpenJaaliQty+jaaliOutQty-JaaliQtyRecd-JaaliAdjQty)* " & ItemDefaultJalliRate & " as JaaliValueClosing," &
        '      " (OpenBoxQty+boxOutQty-BoxQtyRecd-BoxAdjQty)* " & ItemDefaultBoxRate & " as BoxValueClosing , " &
        '      " (OpenCanQty+CanOutQty-CanQtyRecd-CanAdjQty)* " & ItemDefaultCanRate & " as CanValueClosing ,OpencrateQtySum,OpenCanQtySum ,OpencrateQtySumOfVehicleWise,OpencanQtySumOfVehicleWise " &
        '      " from (Select "
        '      If chkCustomerWise.Checked OrElse chkcustomerWithDateWise.Checked Then
        '          qryfinal += " case when CTETemp.rowno= MIN(CTETemp.RowNo ) OVER (PARTITION BY CTETemp.Customer_Code) then CTETemp.OpencrateQty+ISNULL(CT1.CrateQtyClosing,0) else 0 end OpencrateQtySum," &
        '  " case when CTETemp.rowno= MIN(CTETemp.RowNo ) OVER (PARTITION BY CTETemp.Customer_Code) then CTETemp.OpencanQty+ISNULL(CT1.CanQtyClosing,0) else 0 end OpencanQtySum,"
        '      Else
        '          qryfinal += " case when CTETemp.rowno= MIN(CTETemp.RowNo ) OVER (PARTITION BY CTETemp.Customer_Code, CTETemp.Doc_Date,CTETemp.Vehicle_Code,CTETemp.Route_No) then CTETemp.OpencrateQty+ISNULL(CT1.CrateQtyClosing,0) else 0 end OpencrateQtySum," &
        '" case when CTETemp.rowno= MIN(CTETemp.RowNo ) OVER (PARTITION BY CTETemp.Customer_Code, CTETemp.Doc_Date,CTETemp.Vehicle_Code,CTETemp.Route_No) then CTETemp.OpencanQty+ISNULL(CT1.CanQtyClosing,0) else 0 end OpencanQtySum,"
        '      End If
        '      qryfinal += " case when CTETemp.rowno= MIN(CTETemp.RowNo ) OVER (PARTITION BY CTETemp.Customer_Code) then CTETemp.OpencrateQty+ISNULL(CT1.CrateQtyClosing,0) else 0 end OpencrateQtySumOfVehicleWise," &
        '                  " case when CTETemp.rowno= MIN(CTETemp.RowNo ) OVER (PARTITION BY CTETemp.Customer_Code) then CTETemp.OpencanQty+ISNULL(CT1.CanQtyClosing,0) else 0 end OpencanQtySumOfVehicleWise, CTETemp.RowNo,CTETemp.Doc_Date, CTETemp.Route_No,CTETemp.Route_Desc ,CTETemp.Vehicle_Code,CTETemp.Vehicle_Name ,CTETemp.Customer_Code,CTETemp.Customer_Name, CTETemp.Customer_Phone, CTETemp.ZSM,CTETemp.ZSM_NAME,CTETemp.ZSM_Phone,CTETemp.ASM,CTETemp.ASM_NAME,CTETemp.ASM_Phone,CTETemp.ASO,CTETemp.ASO_NAME,CTETemp.ASO_Phone, CTETemp.OpencrateQty+ISNULL(CT1.CrateQtyClosing,0) as OpencrateQty, " &
        '      " CTETemp.OpenJaaliQty+ISNULL(CT1.JaaliQtyClosing,0) as OpenJaaliQty, " &
        '      " CTETemp.OpenBoxQty+ISNULL(CT1.BoxQtyClosing,0) as OpenBoxQty," &
        '      " CTETemp.OpenCanQty+ISNULL(CT1.CanQtyClosing,0) as OpenCanQty, " &
        '      " CTETemp.CrateQtyRecd, CTETemp.JaaliQtyRecd, CTETemp.BoxQtyRecd,CTETemp.CanQtyRecd," &
        '      " CTETemp.CrateOutQty, CTETemp.jaaliOutQty, CTETemp.boxOutQty, CTETemp.CanOutQty, CTETemp.CrateAdjQty , CTETemp.JaaliAdjQty  , CTETemp.BoxAdjQty, CTETemp.CanAdjQty " &
        '      " from CTETemp LEFt OUTER JOIN CTETemp CT1 ON  CT1.Customer_Code=CTETemp.Customer_Code And " &
        '      " CT1.Vehicle_Code = CTETemp.Vehicle_Code  And CT1.Route_No  = CTETemp.Route_No " &
        '      " And (CTETemp.RowNo-CT1.RowNo)=1 ) ZZZ ORDER BY  Customer_Code,convert(date,Doc_Date,103),Vehicle_Code,[Route Code]"

        qry = "select  pp.Doc_Date  as Doc_Date,pp.Vehicle_Code,tspl_vehicle_master.Number as Vehicle_Name ,pp.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_CUSTOMER_MASTER.Phone1 as Customer_Phone ,   TSPL_CUSTOMER_MASTER.ZSM,TSPL_EMPLOYEE_MASTER_ZSM.Emp_Name as ZSM_NAME,case when len (isNull (TSPL_EMPLOYEE_MASTER_ZSM.PHONE,'')) > 0 And  len (isNull (TSPL_EMPLOYEE_MASTER_ZSM.PRESENT_MOBILE_NO,'')) > 0  then '['+ TSPL_EMPLOYEE_MASTER_ZSM.PHONE + '],['  + TSPL_EMPLOYEE_MASTER_ZSM.PRESENT_MOBILE_NO +']' when  len (isNull (TSPL_EMPLOYEE_MASTER_ZSM.PHONE,'')) > 0 then TSPL_EMPLOYEE_MASTER_ZSM.PHONE when len (isNull (TSPL_EMPLOYEE_MASTER_ZSM.PRESENT_MOBILE_NO,'')) > 0 then TSPL_EMPLOYEE_MASTER_ZSM.PRESENT_MOBILE_NO else '' end  as ZSM_Phone ,TSPL_CUSTOMER_MASTER.ASM, TSPL_EMPLOYEE_MASTER_ASM.Emp_Name as ASM_NAME, case when len (isNull (TSPL_EMPLOYEE_MASTER_ASM.PHONE,'')) > 0 And  len (isNull (TSPL_EMPLOYEE_MASTER_ASM.PRESENT_MOBILE_NO,'')) > 0  then '['+ TSPL_EMPLOYEE_MASTER_ASM.PHONE + '],['  + TSPL_EMPLOYEE_MASTER_ASM.PRESENT_MOBILE_NO +']' when  len (isNull (TSPL_EMPLOYEE_MASTER_ASM.PHONE,'')) > 0 then TSPL_EMPLOYEE_MASTER_ASM.PHONE when len (isNull (TSPL_EMPLOYEE_MASTER_ASM.PRESENT_MOBILE_NO,'')) > 0 then TSPL_EMPLOYEE_MASTER_ASM.PRESENT_MOBILE_NO else '' end  as ASM_Phone,TSPL_CUSTOMER_MASTER.ASO,TSPL_EMPLOYEE_MASTER_ASO.Emp_Name as ASO_NAME,   case when len (isNull (TSPL_EMPLOYEE_MASTER_ASO.PHONE,'')) > 0 And  len (isNull (TSPL_EMPLOYEE_MASTER_ASO.PRESENT_MOBILE_NO,'')) > 0  then '['+ TSPL_EMPLOYEE_MASTER_ASO.PHONE + '],['  + TSPL_EMPLOYEE_MASTER_ASO.PRESENT_MOBILE_NO +']' when  len (isNull (TSPL_EMPLOYEE_MASTER_ASO.PHONE,'')) > 0 then TSPL_EMPLOYEE_MASTER_ASO.PHONE when len (isNull (TSPL_EMPLOYEE_MASTER_ASO.PRESENT_MOBILE_NO,'')) > 0 then TSPL_EMPLOYEE_MASTER_ASO.PRESENT_MOBILE_NO else '' end  as ASO_Phone,pp.OpencrateQty as OpencrateQty,pp.OpenJaaliQty  as OpenJaaliQty,pp.OpenBoxQty  as OpenBoxQty,pp.OpenCanQty  as OpenCanQty,pp.CrateQtyRecd  as CrateQtyRecd,pp.JaaliQtyRecd  as JaaliQtyRecd,pp.BoxQtyRecd  as BoxQtyRecd ,pp.CanQtyRecd  as CanQtyRecd,pp.CrateOutQty  as CrateOutQty,pp.jaaliOutQty  as jaaliOutQty ,pp.boxOutQty  as boxOutQty,pp.CanOutQty  as CanOutQty ,pp.CrateQtyClosing as CrateQtyClosing, pp.JaaliQtyClosing as  JaaliQtyClosing, pp.BoxQtyClosing as BoxQtyClosing, pp.CanQtyClosing as CanQtyClosing,pp.CrateAdjQty , pp.JaaliAdjQty  , pp.BoxAdjQty , pp.CanAdjQty from ( " + Environment.NewLine &
                     " " & finalQueryForCustomer & "" + Environment.NewLine &
                    " ) as pp  "
        qry += " left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =pp.Customer_Code " + Environment.NewLine &
               " left outer Join  TSPL_EMPLOYEE_MASTER as TSPL_EMPLOYEE_MASTER_ZSM on TSPL_EMPLOYEE_MASTER_ZSM.EMP_CODE = TSPL_CUSTOMER_MASTER.ZSM " + Environment.NewLine &
               " left outer Join  TSPL_EMPLOYEE_MASTER as TSPL_EMPLOYEE_MASTER_ASM on TSPL_EMPLOYEE_MASTER_ASM.EMP_CODE = TSPL_CUSTOMER_MASTER.ASM  " + Environment.NewLine &
               " left outer Join  TSPL_EMPLOYEE_MASTER as TSPL_EMPLOYEE_MASTER_ASO on TSPL_EMPLOYEE_MASTER_ASO.EMP_CODE = TSPL_CUSTOMER_MASTER.ASO  " + Environment.NewLine &
               " left join tspl_vehicle_master on tspl_vehicle_master.vehicle_id=pp.vehicle_code where 2=2 "

        Dim qryfinal As String = " With CTETemp as (" &
                   " Select convert(varchar,Doc_Date,103) as Doc_Date,Vehicle_Code,Vehicle_Name,Customer_Code,Customer_Name,Customer_Phone, ZSM , ZSM_NAME,ZSM_Phone,ASM,ASM_Name,ASM_Phone,ASO , ASO_Name,ASO_Phone ,OpencrateQty, OpenJaaliQty, OpenBoxQty,OpenCanQty, CrateQtyRecd, JaaliQtyRecd, BoxQtyRecd ,CanQtyRecd,CrateOutQty," &
                   " jaaliOutQty,boxOutQty  ,CanOutQty, CrateAdjQty , JaaliAdjQty  , BoxAdjQty ,CanAdjQty"
        If chkCustomerWise.Checked Then
            qryfinal += ",SUM(CrateQtyClosing) OVER (Partition BY Customer_Code ORDER BY Customer_Code,Vehicle_Code) as CrateQtyClosing, " &
                  " SUM(JaaliQtyClosing) OVER (Partition BY Customer_Code ORDER BY Customer_Code,Vehicle_Code) as JaaliQtyClosing, " &
                  " SUM(BoxQtyClosing) OVER (Partition BY Customer_Code ORDER BY Customer_Code,Vehicle_Code) as BoxQtyClosing," &
                  " SUM(CanQtyClosing) OVER (Partition BY Customer_Code ORDER BY Customer_Code,Vehicle_Code) as CanQtyClosing ," &
                  " Row_Number() OVER (Partition BY Customer_Code ORDER BY Customer_Code,Vehicle_Code) as RowNo"
        Else
            qryfinal += ",SUM(CrateQtyClosing) OVER (Partition BY Customer_Code,Vehicle_Code ORDER BY Customer_Code, Doc_Date,Vehicle_Code) as CrateQtyClosing, " &
                  " SUM(JaaliQtyClosing) OVER (Partition BY Customer_Code ,Vehicle_Code ORDER BY Customer_Code, Doc_Date,Vehicle_Code) as JaaliQtyClosing, " &
                  " SUM(BoxQtyClosing) OVER (Partition BY Customer_Code,Vehicle_Code ORDER BY Customer_Code, Doc_Date,Vehicle_Code) as BoxQtyClosing," &
                  " SUM(CanQtyClosing) OVER (Partition BY Customer_Code,Vehicle_Code ORDER BY Customer_Code, Doc_Date,Vehicle_Code) as CanQtyClosing ," &
                  " Row_Number() OVER (Partition BY Customer_Code,Vehicle_Code ORDER BY Customer_Code, Doc_Date,Vehicle_Code) as RowNo"
        End If


        qryfinal += " from(" + Environment.NewLine &
                   " " & qry & " " &
                   " ) YYY )" &
                   " Select convert(varchar,Doc_Date,103) as Date, Vehicle_Code as [Vehicle Code],Vehicle_Name as [Vehicle Name], Customer_Code as [Customer Code],Customer_Name as [Customer Name],Customer_Phone as [Customer Phone], ZSM as [ZSM Code], ZSM_Name as [ZSM Name],ZSM_Phone as [ZSM Phone],ASM as [ASM Code],ASM_NAME as [ASM Name],ASM_Phone as [ASM Phone],ASO as [ASO Code],ASO_Name as [ASO Name],ASO_Phone as [ASO Phone], OpencrateQty,OpenJaaliQty,OpenBoxQty,OpenCanQty"
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "SPMMD") = CompairStringResult.Equal Then 'For SPMMD, In view defination Issue is Third header group
            qryfinal += " ,CrateOutQty,jaaliOutQty,boxOutQty,CanOutQty, CrateQtyRecd, JaaliQtyRecd,BoxQtyRecd, CanQtyRecd"
        Else
            qryfinal += ", CrateQtyRecd, JaaliQtyRecd,BoxQtyRecd, CanQtyRecd,CrateOutQty,jaaliOutQty,boxOutQty,CanOutQty "
        End If
        qryfinal += ", CrateAdjQty , JaaliAdjQty  , BoxAdjQty,CanAdjQty,"

        If chkCustomerWise.Checked OrElse chkcustomerWithDateWise.Checked Then
            qryfinal += " OpencrateQty+CrateOutQty -CrateQtyRecd-CrateAdjQty as CrateQtyClosing," &
                        " OpenCanQty+CanOutQty-CanQtyRecd-CanAdjQty as CanQtyClosing ,"
        Else
            qryfinal += " SUM(OpencrateQtySum+CrateOutQty -CrateQtyRecd-CrateAdjQty) Over (Partition by Customer_Code, Doc_Date,Vehicle_Code ORDER BY RowNo) as CrateQtyClosing, " &
          " sum(OpencanQtySum+CanOutQty-CanQtyRecd-CanAdjQty) Over (Partition by Customer_Code, Doc_Date,Vehicle_Code ORDER BY RowNo) as CanQtyClosing ,"
        End If


        qryfinal += " OpenJaaliQty+jaaliOutQty-JaaliQtyRecd-JaaliAdjQty as JaaliQtyClosing," &
        " OpenBoxQty+boxOutQty-BoxQtyRecd-BoxAdjQty as BoxQtyClosing," &
        " (OpencrateQty+CrateOutQty -CrateQtyRecd-CrateAdjQty)* " & ItemDefaultCrateRate & " as CrateValueClosing," &
        " (OpenJaaliQty+jaaliOutQty-JaaliQtyRecd-JaaliAdjQty)* " & ItemDefaultJalliRate & " as JaaliValueClosing," &
        " (OpenBoxQty+boxOutQty-BoxQtyRecd-BoxAdjQty)* " & ItemDefaultBoxRate & " as BoxValueClosing , " &
        " (OpenCanQty+CanOutQty-CanQtyRecd-CanAdjQty)* " & ItemDefaultCanRate & " as CanValueClosing ,OpencrateQtySum,OpenCanQtySum ,OpencrateQtySumOfVehicleWise,OpencanQtySumOfVehicleWise " &
        " from (Select "
        If chkCustomerWise.Checked OrElse chkcustomerWithDateWise.Checked Then
            qryfinal += " case when CTETemp.rowno= MIN(CTETemp.RowNo ) OVER (PARTITION BY CTETemp.Customer_Code) then CTETemp.OpencrateQty+ISNULL(CT1.CrateQtyClosing,0) else 0 end OpencrateQtySum," &
    " case when CTETemp.rowno= MIN(CTETemp.RowNo ) OVER (PARTITION BY CTETemp.Customer_Code) then CTETemp.OpencanQty+ISNULL(CT1.CanQtyClosing,0) else 0 end OpencanQtySum,"
        Else
            qryfinal += " case when CTETemp.rowno= MIN(CTETemp.RowNo ) OVER (PARTITION BY CTETemp.Customer_Code, CTETemp.Doc_Date,CTETemp.Vehicle_Code) then CTETemp.OpencrateQty+ISNULL(CT1.CrateQtyClosing,0) else 0 end OpencrateQtySum," &
  " case when CTETemp.rowno= MIN(CTETemp.RowNo ) OVER (PARTITION BY CTETemp.Customer_Code, CTETemp.Doc_Date,CTETemp.Vehicle_Code) then CTETemp.OpencanQty+ISNULL(CT1.CanQtyClosing,0) else 0 end OpencanQtySum,"
        End If
        qryfinal += " case when CTETemp.rowno= MIN(CTETemp.RowNo ) OVER (PARTITION BY CTETemp.Customer_Code) then CTETemp.OpencrateQty+ISNULL(CT1.CrateQtyClosing,0) else 0 end OpencrateQtySumOfVehicleWise," &
                    " case when CTETemp.rowno= MIN(CTETemp.RowNo ) OVER (PARTITION BY CTETemp.Customer_Code) then CTETemp.OpencanQty+ISNULL(CT1.CanQtyClosing,0) else 0 end OpencanQtySumOfVehicleWise, CTETemp.RowNo,CTETemp.Doc_Date ,CTETemp.Vehicle_Code,CTETemp.Vehicle_Name ,CTETemp.Customer_Code,CTETemp.Customer_Name, CTETemp.Customer_Phone, CTETemp.ZSM,CTETemp.ZSM_NAME,CTETemp.ZSM_Phone,CTETemp.ASM,CTETemp.ASM_NAME,CTETemp.ASM_Phone,CTETemp.ASO,CTETemp.ASO_NAME,CTETemp.ASO_Phone, CTETemp.OpencrateQty+ISNULL(CT1.CrateQtyClosing,0) as OpencrateQty, " &
        " CTETemp.OpenJaaliQty+ISNULL(CT1.JaaliQtyClosing,0) as OpenJaaliQty, " &
        " CTETemp.OpenBoxQty+ISNULL(CT1.BoxQtyClosing,0) as OpenBoxQty," &
        " CTETemp.OpenCanQty+ISNULL(CT1.CanQtyClosing,0) as OpenCanQty, " &
        " CTETemp.CrateQtyRecd, CTETemp.JaaliQtyRecd, CTETemp.BoxQtyRecd,CTETemp.CanQtyRecd," &
        " CTETemp.CrateOutQty, CTETemp.jaaliOutQty, CTETemp.boxOutQty, CTETemp.CanOutQty, CTETemp.CrateAdjQty , CTETemp.JaaliAdjQty  , CTETemp.BoxAdjQty, CTETemp.CanAdjQty " &
        " from CTETemp LEFt OUTER JOIN CTETemp CT1 ON  CT1.Customer_Code=CTETemp.Customer_Code And " &
        " CT1.Vehicle_Code = CTETemp.Vehicle_Code   " &
        " And (CTETemp.RowNo-CT1.RowNo)=1 ) ZZZ ORDER BY  Customer_Code,convert(date,Doc_Date,103),Vehicle_Code"

        dt = clsDBFuncationality.GetDataTable(qryfinal)
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.GroupDescriptors.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        Gv1.DataSource = dt
        Gv1.BestFitColumns()
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
            Exit Sub
        End If

        'If chkCrate.Checked = False AndAlso chkCan.Checked = False Then
        '    For Each grow As GridViewRowInfo In Gv1.Rows
        '        grow.Cells("OpencrateQty").Value = 0
        '        grow.Cells("CrateQtyRecd").Value = 0
        '        grow.Cells("CrateOutQty").Value = 0
        '        grow.Cells("CrateAdjQty").Value = 0
        '        grow.Cells("CrateQtyClosing").Value = 0
        '        grow.Cells("CrateValueClosing").Value = 0
        '        grow.Cells("OpencrateQtySum").Value = 0
        '        grow.Cells("OpencrateQtySumOfVehicleWise").Value = 0

        '        grow.Cells("OpenCanQty").Value = 0
        '        grow.Cells("CanQtyRecd").Value = 0
        '        grow.Cells("CanOutQty").Value = 0
        '        grow.Cells("CanAdjQty").Value = 0
        '        grow.Cells("CanQtyClosing").Value = 0
        '        grow.Cells("CanValueClosing").Value = 0
        '        grow.Cells("OpenCanQtySum").Value = 0
        '        grow.Cells("OpencanQtySumOfVehicleWise").Value = 0

        '    Next
        'ElseIf chkCrate.Checked = False Then
        '    For Each grow As GridViewRowInfo In Gv1.Rows
        '        grow.Cells("OpencrateQty").Value = 0
        '        grow.Cells("CrateQtyRecd").Value = 0
        '        grow.Cells("CrateOutQty").Value = 0
        '        grow.Cells("CrateAdjQty").Value = 0
        '        grow.Cells("CrateQtyClosing").Value = 0
        '        grow.Cells("CrateValueClosing").Value = 0
        '        grow.Cells("OpencrateQtySum").Value = 0
        '        grow.Cells("OpencrateQtySumOfVehicleWise").Value = 0
        '    Next
        'ElseIf chkCan.Checked = False Then
        '    For Each grow As GridViewRowInfo In Gv1.Rows
        '        grow.Cells("OpenCanQty").Value = 0
        '        grow.Cells("CanQtyRecd").Value = 0
        '        grow.Cells("CanOutQty").Value = 0
        '        grow.Cells("CanAdjQty").Value = 0
        '        grow.Cells("CanQtyClosing").Value = 0
        '        grow.Cells("CanValueClosing").Value = 0
        '        grow.Cells("OpenCanQtySum").Value = 0
        '        grow.Cells("OpencanQtySumOfVehicleWise").Value = 0
        '    Next
        'End If

        FormatGrid()
        RadPageView1.SelectedPage = RadPageViewPage2
        ReStoreGridLayout()
        View()
    End Sub
    Public Sub loaddataRouteWise()
        Dim dt As DataTable

        Dim QryForCustomerOpening As String = Nothing
        Dim QryForCustomerclosing As String = Nothing
        Dim finalQueryForCustomer As String = Nothing
        Dim qry As String = Nothing



        QryForCustomerOpening = "select "

        QryForCustomerOpening += "convert(date,'" + fromDate.Value + "',103) as Doc_Date,max(Opening.Vehicle_Code) as Vehicle_Code ,"

        QryForCustomerOpening += " Opening.Route_No ,sum(Opening.OpencrateQty*Type) as OpencrateQty,sum(Opening.OpenJaaliQty*Type ) as OpenJaaliQty,sum(Opening.OpenBoxQty *Type)  as OpenBoxQty,sum(Opening.OpenCanQty *Type)  as OpenCanQty,sum(Opening.CrateQtyRecd *Type) as CrateQtyRecd,sum(Opening.JaaliQtyRecd*Type ) as JaaliQtyRecd,sum(Opening.BoxQtyRecd*Type ) as BoxQtyRecd,sum(Opening.CanQtyRecd*Type ) as CanQtyRecd,sum(Opening.CrateOutQty*Type ) as CrateOutQty,sum(Opening.jaaliOutQty*Type ) as jaaliOutQty ,sum(Opening.boxOutQty*Type ) as boxOutQty,sum(Opening.CanOutQty*Type ) as CanOutQty,sum(Opening.CrateAdjQty*Type ) as CrateAdjQty,sum(Opening.JaaliAdjQty*Type ) as JaaliAdjQty,sum(Opening.BoxAdjQty *Type) as  BoxAdjQty ,sum(Opening.CanAdjQty *Type) as  canAdjQty from " & _
                   " (" & _
                    " select TSPL_SD_SHIPMENT_HEAD.Document_Date    as Document_Date,(case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end) as Vehicle_Code ,TSPL_SD_SHIPMENT_HEAD.Route_No  ,1 as Type  ,'O' as Type1,TSPL_SD_SHIPMENT_HEAD.Crate as OpencrateQty,TSPL_SD_SHIPMENT_HEAD.jaali as OpenJaaliQty, TSPL_SD_SHIPMENT_HEAD.box  as OpenBoxQty , TSPL_SD_SHIPMENT_HEAD.ShippedCAN  as OpenCanQty  ,0 as CrateQtyRecd,0  as JaaliQtyRecd ,0 as BoxQtyRecd ,0 as CanQtyRecd ,0 as CrateOutQty,0 as jaaliOutQty,0 as boxOutQty ,0 as CanOutQty,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty  ,0 as  CanAdjQty  from TSPL_SD_SHIPMENT_HEAD  where TSPL_SD_SHIPMENT_HEAD.screen_type='DS'  AND TSPL_SD_SHIPMENT_HEAD.Status =1 " & _
                    " union all " & _
                    "select TSPL_sd_SALE_RETURN_HEAD.Document_Date    as Document_Date,TSPL_sd_SALE_RETURN_HEAD.Vehicle_Code ,TSPL_sd_SALE_RETURN_HEAD.Route_No  ,-1 as Type  ,'I' as Type1,TSPL_sd_SALE_RETURN_HEAD.CrateQty as OpencrateQty,TSPL_sd_SALE_RETURN_HEAD.jaali as OpenJaaliQty, TSPL_sd_SALE_RETURN_HEAD.box  as OpenBoxQty , TSPL_sd_SALE_RETURN_HEAD.ShippedCAN  as OpenCanQty  ,0 as CrateQtyRecd,0  as JaaliQtyRecd ,0 as BoxQtyRecd ,0 as CanQtyRecd ,0 as CrateOutQty,0 as jaaliOutQty,0 as boxOutQty ,0 as CanOutQty,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty  ,0 as  CanAdjQty  from TSPL_sd_SALE_RETURN_HEAD  where TSPL_sd_SALE_RETURN_HEAD.screen_type='DS'  AND TSPL_sd_SALE_RETURN_HEAD.Status =1 " & _
                    " union all " & _
                    " select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as Document_Date,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Route_Code  ,1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  ," & _
                    " TSPL_CRATE_RECEIVED_detail_FRESHSALE.OutQty as OpencrateQty," & _
                    " TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliOutQty   as OpenjaaliQty ," & _
                    " TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxOutQty  as OpenboxQty," & _
                    " TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANOutQty as OpenCanQty ," & _
                    " 0 as CrateQtyRecd,0 JaaliQtyRecd ," & _
                    " 0 BoxQtyRecd ," & _
                    " 0 CanQtyRecd ," & _
                    " 0 as CrateOutQty," & _
                    " 0 jaaliOutQty," & _
                    " 0 boxoutqty," & _
                     " 0 Canoutqty," & _
                    " 0  as CrateAdjQty," & _
                    " 0  as JaaliAdjQty," & _
                    " 0  as BoxAdjQty," & _
                      " 0  as CanAdjQty" & _
                      " from TSPL_CRATE_RECEIVED_detail_FRESHSALE" & _
                    " left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No " & _
                    " where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='O' AND TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Posted  =1 " & _
                    " union all" & _
                    " select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as Document_Date,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Route_Code  ,-1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  ," & _
                    " isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyRecd,0) + isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.Adjustment,0)  as OpencrateQty," & _
                    " isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.JaaliQtyRecd,0) + isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliAdjustment,0)    as OpenjaaliQty ," & _
                    " isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.BoxQtyRecd,0) + isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxAdjustment,0)  as OpenboxQty," & _
                     " isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQtyRec,0) + isnull(TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANAdjustment,0)  as OpenCanQty," & _
                    " 0 as CrateQtyRecd,0 JaaliQtyRecd ," & _
                    " 0 BoxQtyRecd ," & _
                    " 0 CanQtyRecd ," & _
                    " 0 as CrateOutQty," & _
                    " 0 jaaliOutQty," & _
                    " 0 boxoutqty," & _
                    " 0 Canoutqty," & _
                    " 0  as CrateAdjQty," & _
                     " 0  as JaaliAdjQty," & _
                    " 0  as BoxAdjQty," & _
                      " 0  as CanAdjQty" & _
                      " from TSPL_CRATE_RECEIVED_detail_FRESHSALE" & _
                    " left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No " & _
                    " where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='I' AND TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Posted  =1  " & _
                    " )as Opening WHERE convert(date,Document_Date,103)<(convert(date,'" + fromDate.Value + "',103))"

        QryForCustomerOpening += " group by Route_No " + Environment.NewLine '----------Qry for Branch opening'





        QryForCustomerclosing = "select Document_Date,Vehicle_Code,Route_Code,0 as OpencrateQty,0 as OpenjaaliQty ,0 as OpenboxQty,0 as OpenCanQty,Case When [Type]=1 Then CrateQtyRecd Else 0 End as CrateQtyRecd,Case When [Type]=1 Then JaaliQtyRecd Else 0 End as JaaliQtyRecd,Case When [Type]=1 Then BoxQtyRecd Else 0 End as BoxQtyRecd,Case When [Type]=1 Then CANQtyRec Else 0 End as CANQtyRec," + Environment.NewLine & _
                    " Case When [Type]=-1 Then CrateOutQty Else 0 End as CrateOutQty,Case When [Type]=-1 Then jaaliOutQty Else 0 End as jaaliOutQty,Case When [Type]=-1 Then boxOutQty Else 0 End as boxOutQty,Case When [Type]=-1 Then CanOutQty Else 0 End as CanOutQty,Case When [Type]=1 Then CrateAdjQty Else 0 End as CrateAdjQty,Case When [Type]=1 Then JaaliAdjQty Else 0 End as JaaliAdjQty,Case When [Type]=1 Then BoxAdjQty Else 0 End as BoxAdjQty,Case When [Type]=1 Then CANAdjustment Else 0 End as CANAdjustment " + Environment.NewLine & _
                     " from ((select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as Document_Date,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Route_Code  ,1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyManual as OpencrateQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaali  as OpenjaaliQty ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.box as OpenboxQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQty as OpenCanQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyRecd as CrateQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.JaaliQtyRecd ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.BoxQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQtyRec ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.OutQty as CrateOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANOutQty , TSPL_CRATE_RECEIVED_detail_FRESHSALE.Adjustment  as CrateAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliAdjustment  as JaaliAdjQty, TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxAdjustment  as BoxAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANAdjustment  as CANAdjustment from TSPL_CRATE_RECEIVED_detail_FRESHSALE" + Environment.NewLine & _
                    " left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No " + Environment.NewLine & _
                    " where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='I' AND TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Posted  =1 )" & _
                    " union all " + Environment.NewLine & _
                    " (select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date as Document_Date,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.Route_Code  ,-1 as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type as Type1  ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyManual as OpencrateQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaali  as OpenjaaliQty ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.box as OpenboxQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQty as OpenCANQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CrateQtyRecd as CrateQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.JaaliQtyRecd ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.BoxQtyRecd,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANQtyRec ,TSPL_CRATE_RECEIVED_detail_FRESHSALE.OutQty as CrateOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxOutQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANOutQty , TSPL_CRATE_RECEIVED_detail_FRESHSALE.Adjustment  as CrateAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.jaaliAdjustment  as JaaliAdjQty, TSPL_CRATE_RECEIVED_detail_FRESHSALE.boxAdjustment  as BoxAdjQty,TSPL_CRATE_RECEIVED_detail_FRESHSALE.CANAdjustment  as CANAdjustment from TSPL_CRATE_RECEIVED_detail_FRESHSALE" + Environment.NewLine & _
                    " left join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_detail_FRESHSALE.Document_No " + Environment.NewLine & _
                    " where TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type ='O' AND TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Posted  =1 " & _
                    " union all " + Environment.NewLine & _
                    " select TSPL_SD_SHIPMENT_HEAD.Document_Date   as Document_Date,(case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end) as Vehicle_Code ,TSPL_SD_SHIPMENT_HEAD.Route_No  ,-1 as Type  ,'O' as Type1,0 as OpencrateQty,0  as OpenBoxQty ,0 as OpenJaaliQty,0 as OpenCANQty," & _
                    " 0 as CrateQtyRecd, 0 JaaliQtyRecd , " & _
                    " 0  as BoxQtyRecd,0 CanQtyRecd  ,TSPL_SD_SHIPMENT_HEAD.Crate as CrateOutQty,jaali  as jaaliOutQty,TSPL_SD_SHIPMENT_HEAD.Box as boxOutQty,TSPL_SD_SHIPMENT_HEAD.ShippedCAN as CanOutQty ,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty,0 as  CanAdjQty   " & _
                    " from TSPL_SD_SHIPMENT_HEAD  where TSPL_SD_SHIPMENT_HEAD.screen_type='DS' AND TSPL_SD_SHIPMENT_HEAD.Status =1" & _
        " union all " + Environment.NewLine & _
        " select TSPL_sd_SALE_RETURN_HEAD.Document_Date   as Document_Date,TSPL_sd_SALE_RETURN_HEAD.Vehicle_Code ,TSPL_sd_SALE_RETURN_HEAD.Route_No  ,1 as Type  ,'I' as Type1,0 as OpencrateQty,0  as OpenBoxQty ,0 as OpenJaaliQty,0 as OpenCANQty, TSPL_sd_SALE_RETURN_HEAD.CrateQty as CrateQtyRecd, TSPL_sd_SALE_RETURN_HEAD.jaali as JaaliQtyRecd ,  TSPL_sd_SALE_RETURN_HEAD.Box  as BoxQtyRecd,TSPL_sd_SALE_RETURN_HEAD.ShippedCAN CanQtyRecd  ,0 as CrateOutQty,0 as jaaliOutQty,0 as boxOutQty,0 as CanOutQty ,0 as CrateAdjQty,0 as JaaliAdjQty,0 as  BoxAdjQty,0 as  CanAdjQty    from TSPL_sd_SALE_RETURN_HEAD  where TSPL_sd_SALE_RETURN_HEAD.screen_type='DS' AND TSPL_sd_SALE_RETURN_HEAD.Status =1 " & _
                    ") " & _
                    " ) as Closing " + Environment.NewLine & _
                    " WHERE convert(date,Document_Date ,103)>=convert(date,'" + fromDate.Value + "',103) AND convert(date,Document_Date,103)<=convert(date,'" + ToDate.Value + "',103) " + Environment.NewLine '----------Qry for Branch Closing'

        finalQueryForCustomer = "select convert(date,Doc_Date,103)  as Doc_Date,max(xx.Vehicle_Code) as Vehicle_Code"

        finalQueryForCustomer += ",xx.Route_no,sum(xx.OpencrateQty) as OpencrateQty,sum(xx.OpenJaaliQty ) as OpenJaaliQty,sum(xx.OpenBoxQty )  as OpenBoxQty,sum(xx.OpenCanQty )  as OpenCanQty,sum(xx.CrateQtyRecd) as CrateQtyRecd,sum(xx.JaaliQtyRecd) as JaaliQtyRecd,sum(xx.BoxQtyRecd) as BoxQtyRecd,sum(xx.CanQtyRecd) as CanQtyRecd,sum(xx.CrateOutQty ) as CrateOutQty,sum(xx.jaaliOutQty ) as jaaliOutQty ,sum(xx.boxOutQty ) as boxOutQty,sum(xx.CanOutQty ) as CanOutQty, sum(xx.CrateAdjQty ) as CrateAdjQty ,sum(xx.JaaliAdjQty )as JaaliAdjQty  ,sum(xx.BoxAdjQty )  as BoxAdjQty ,sum(xx.CanAdjQty )  as CanAdjQty,(sum(xx.OpencrateQty)+sum(xx.CrateOutQty )-sum(xx.CrateQtyRecd)-sum(xx.CrateAdjQty )) as CrateQtyClosing," & _
                    " (sum(xx.OpenJaaliQty)+sum(xx.jaaliOutQty)-sum(xx.JaaliQtyRecd)-sum(xx.JaaliAdjQty )) as JaaliQtyClosing," & _
                    " (sum(xx.OpenBoxQty)+sum(xx.boxOutQty )-sum(xx.BoxQtyRecd)-sum(xx.BoxAdjQty )) as BoxQtyClosing " & _
                     " , (sum(xx.OpenCanQty)+sum(xx.CanOutQty )-sum(xx.CanQtyRecd)-sum(xx.CanAdjQty )) as CanQtyClosing " & _
                    " from (" & _
                    "" & QryForCustomerOpening & "" & _
                    " UNION All " + Environment.NewLine '---------------------bada wala Union(between opening and closing) 
        finalQueryForCustomer += "" & QryForCustomerclosing & "" &
                    "   ) as xx where 2=2   "

        If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
            finalQueryForCustomer += " and xx.Customer_Code  in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") " + Environment.NewLine
        End If
        If txtVehicle.arrValueMember IsNot Nothing AndAlso txtVehicle.arrValueMember.Count > 0 Then
            finalQueryForCustomer += " and xx.Vehicle_Code in (" + clsCommon.GetMulcallString(txtVehicle.arrValueMember) + ") " + Environment.NewLine
        End If


        finalQueryForCustomer += " GROUP BY Route_No,convert(date,Doc_Date,103) "




        qry = "select  pp.Doc_Date  as Doc_Date,TSPL_Route_MASTER.Route_No,TSPL_Route_MASTER.Route_Desc,pp.Vehicle_Code,tspl_vehicle_master.Number as Vehicle_Name ,pp.OpencrateQty as OpencrateQty,pp.OpenJaaliQty  as OpenJaaliQty,pp.OpenBoxQty  as OpenBoxQty,pp.OpenCanQty  as OpenCanQty,pp.CrateQtyRecd  as CrateQtyRecd,pp.JaaliQtyRecd  as JaaliQtyRecd,pp.BoxQtyRecd  as BoxQtyRecd ,pp.CanQtyRecd  as CanQtyRecd,pp.CrateOutQty  as CrateOutQty,pp.jaaliOutQty  as jaaliOutQty ,pp.boxOutQty  as boxOutQty,pp.CanOutQty  as CanOutQty ,pp.CrateQtyClosing as CrateQtyClosing, pp.JaaliQtyClosing as  JaaliQtyClosing, pp.BoxQtyClosing as BoxQtyClosing, pp.CanQtyClosing as CanQtyClosing,pp.CrateAdjQty , pp.JaaliAdjQty  , pp.BoxAdjQty , pp.CanAdjQty from ( " + Environment.NewLine & _
                     " " & finalQueryForCustomer & "" + Environment.NewLine & _
                    " ) as pp  "

        qry += "  left Outer Join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No =pp.Route_No  " + Environment.NewLine & _
              " left join tspl_vehicle_master on tspl_vehicle_master.vehicle_id=pp.vehicle_code where 2=2 "


        If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
            qry += " and TSPL_ROUTE_MASTER.Route_No in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ") " + Environment.NewLine
        End If

        Dim qryfinal As String = " With CTETemp as (" & _
                   " Select convert(varchar,Doc_Date,103) as Doc_Date, Route_No,Route_Desc,Vehicle_Code,Vehicle_Name,OpencrateQty, OpenJaaliQty, OpenBoxQty,OpenCanQty, CrateQtyRecd, JaaliQtyRecd, BoxQtyRecd ,CanQtyRecd,CrateOutQty," & _
                   " jaaliOutQty,boxOutQty  ,CanOutQty, CrateAdjQty , JaaliAdjQty  , BoxAdjQty ,CanAdjQty"

        qryfinal += ",SUM(CrateQtyClosing) OVER (Partition BY Route_No ORDER BY Route_No, Doc_Date) as CrateQtyClosing, " & _
                  " SUM(JaaliQtyClosing) OVER (Partition BY Route_No ORDER BY Route_No, Doc_Date) as JaaliQtyClosing, " & _
                  " SUM(BoxQtyClosing) OVER (Partition BY Route_No ORDER BY Route_No, Doc_Date) as BoxQtyClosing," & _
                  " SUM(CanQtyClosing) OVER (Partition BY Route_No ORDER BY Route_No, Doc_Date) as CanQtyClosing ," & _
                  " Row_Number() OVER (Partition BY Route_No ORDER BY Route_No, Doc_Date) as RowNo"


        qryfinal += " from(" + Environment.NewLine & _
                   " " & qry & " " & _
                   " ) YYY )" & _
                   " Select convert(varchar,Doc_Date,103) as Date,Route_No ,Route_Desc as [Route Name], Vehicle_Code as [Vehicle Code],Vehicle_Name as [Vehicle Name],OpencrateQty,OpenJaaliQty,OpenBoxQty,OpenCanQty, CrateQtyRecd, JaaliQtyRecd,BoxQtyRecd, CanQtyRecd  ,CrateOutQty," & _
                   " jaaliOutQty,boxOutQty,CanOutQty, CrateAdjQty , JaaliAdjQty  , BoxAdjQty,CanAdjQty,"


        qryfinal += " SUM(OpencrateQtySum+CrateOutQty -CrateQtyRecd-CrateAdjQty) Over (Partition by Route_no, Doc_Date ORDER BY RowNo) as CrateQtyClosing, " & _
          " sum(OpencanQtySum+CanOutQty-CanQtyRecd-CanAdjQty) Over (Partition by Route_no, Doc_Date ORDER BY RowNo) as CanQtyClosing ,"



        qryfinal += " OpenJaaliQty+jaaliOutQty-JaaliQtyRecd-JaaliAdjQty as JaaliQtyClosing," & _
        " OpenBoxQty+boxOutQty-BoxQtyRecd-BoxAdjQty as BoxQtyClosing," & _
        " (OpencrateQty+CrateOutQty -CrateQtyRecd-CrateAdjQty)* " & ItemDefaultCrateRate & " as CrateValueClosing," & _
        " (OpenJaaliQty+jaaliOutQty-JaaliQtyRecd-JaaliAdjQty)* " & ItemDefaultJalliRate & " as JaaliValueClosing," & _
        " (OpenBoxQty+boxOutQty-BoxQtyRecd-BoxAdjQty)* " & ItemDefaultBoxRate & " as BoxValueClosing , " & _
        " (OpenCanQty+CanOutQty-CanQtyRecd-CanAdjQty)* " & ItemDefaultCanRate & " as CanValueClosing ,OpencrateQtySum,OpenCanQtySum ,OpencrateQtySumOfVehicleWise,OpencanQtySumOfVehicleWise " & _
        " from (Select "

        qryfinal += " case when CTETemp.rowno= MIN(CTETemp.RowNo ) OVER (PARTITION BY CTETemp.Route_No, CTETemp.Doc_Date) then CTETemp.OpencrateQty+ISNULL(CT1.CrateQtyClosing,0) else 0 end OpencrateQtySum," & _
" case when CTETemp.rowno= MIN(CTETemp.RowNo ) OVER (PARTITION BY CTETemp.Route_No, CTETemp.Doc_Date) then CTETemp.OpencanQty+ISNULL(CT1.CanQtyClosing,0) else 0 end OpencanQtySum,"

        qryfinal += " case when CTETemp.rowno= MIN(CTETemp.RowNo ) OVER (PARTITION BY CTETemp.Route_No) then CTETemp.OpencrateQty+ISNULL(CT1.CrateQtyClosing,0) else 0 end OpencrateQtySumOfVehicleWise," & _
                    " case when CTETemp.rowno= MIN(CTETemp.RowNo ) OVER (PARTITION BY CTETemp.Route_No) then CTETemp.OpencanQty+ISNULL(CT1.CanQtyClosing,0) else 0 end OpencanQtySumOfVehicleWise, CTETemp.RowNo,CTETemp.Doc_Date, CTETemp.Route_No,CTETemp.Route_Desc ,CTETemp.Vehicle_Code,CTETemp.Vehicle_Name , CTETemp.OpencrateQty+ISNULL(CT1.CrateQtyClosing,0) as OpencrateQty, " & _
        " CTETemp.OpenJaaliQty+ISNULL(CT1.JaaliQtyClosing,0) as OpenJaaliQty, " & _
        " CTETemp.OpenBoxQty+ISNULL(CT1.BoxQtyClosing,0) as OpenBoxQty," & _
        " CTETemp.OpenCanQty+ISNULL(CT1.CanQtyClosing,0) as OpenCanQty, " & _
        " CTETemp.CrateQtyRecd, CTETemp.JaaliQtyRecd, CTETemp.BoxQtyRecd,CTETemp.CanQtyRecd," & _
        " CTETemp.CrateOutQty, CTETemp.jaaliOutQty, CTETemp.boxOutQty, CTETemp.CanOutQty, CTETemp.CrateAdjQty , CTETemp.JaaliAdjQty  , CTETemp.BoxAdjQty, CTETemp.CanAdjQty " & _
        " from CTETemp LEFt OUTER JOIN CTETemp CT1 ON  CT1.Route_No=CTETemp.Route_No " & _
        " AND (CTETemp.RowNo-CT1.RowNo)=1 ) ZZZ ORDER BY  Route_No,convert(date,Doc_Date,103)"



        dt = clsDBFuncationality.GetDataTable(qryfinal)
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.GroupDescriptors.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        Gv1.DataSource = dt
        Gv1.BestFitColumns()
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
            Exit Sub
        End If
        FormatGridRouteWise()
        RadPageView1.SelectedPage = RadPageViewPage2
        ReStoreGridLayout()
        View()
    End Sub
    Sub FormatGridRouteWise()
        Gv1.TableElement.TableHeaderHeight = 25
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).Width = 100
            Gv1.Columns(ii).IsVisible = False
        Next

        Gv1.Columns("Date").IsVisible = True
        Gv1.Columns("Date").Width = 100
        Gv1.Columns("Date").HeaderText = "Date"

        Gv1.Columns("Route_No").IsVisible = True
        Gv1.Columns("Route_No").Width = 100
        Gv1.Columns("Route_No").HeaderText = "Route Code"

        Gv1.Columns("Route Name").IsVisible = True
        Gv1.Columns("Route Name").Width = 100
        Gv1.Columns("Route Name").HeaderText = "Route Name"

        Gv1.Columns("Vehicle Code").IsVisible = True
        Gv1.Columns("Vehicle Code").Width = 100
        Gv1.Columns("Vehicle Code").HeaderText = "Vehicle Code"

        Gv1.Columns("Vehicle Name").IsVisible = True
        Gv1.Columns("Vehicle Name").Width = 100
        Gv1.Columns("Vehicle Name").HeaderText = "Vehicle Name"


        Gv1.Columns("OpencrateQty").IsVisible = True
        Gv1.Columns("OpencrateQty").Width = 100
        Gv1.Columns("OpencrateQty").HeaderText = "Crate"
        Gv1.Columns("OpencrateQty").FormatString = "{0:F0}"



        Gv1.Columns("OpenJaaliQty").IsVisible = True
        Gv1.Columns("OpenJaaliQty").Width = 100
        Gv1.Columns("OpenJaaliQty").HeaderText = "Jaali"
        Gv1.Columns("OpenJaaliQty").FormatString = "{0:F0}"

        Gv1.Columns("OpenBoxQty").IsVisible = True
        Gv1.Columns("OpenBoxQty").Width = 100
        Gv1.Columns("OpenBoxQty").HeaderText = "BOX"
        Gv1.Columns("OpenBoxQty").FormatString = "{0:F0}"

        Gv1.Columns("OpenCanQty").IsVisible = True
        Gv1.Columns("OpenCanQty").Width = 100
        Gv1.Columns("OpenCanQty").HeaderText = "CAN"
        Gv1.Columns("OpenCanQty").FormatString = "{0:F0}"

        Gv1.Columns("CrateQtyRecd").IsVisible = True
        Gv1.Columns("CrateQtyRecd").Width = 100
        Gv1.Columns("CrateQtyRecd").HeaderText = "Crate"
        Gv1.Columns("CrateQtyRecd").FormatString = "{0:F0}"

        Gv1.Columns("JaaliQtyRecd").IsVisible = True
        Gv1.Columns("JaaliQtyRecd").Width = 100
        Gv1.Columns("JaaliQtyRecd").HeaderText = "Jaali"
        Gv1.Columns("JaaliQtyRecd").FormatString = "{0:F0}"

        Gv1.Columns("BoxQtyRecd").IsVisible = True
        Gv1.Columns("BoxQtyRecd").Width = 100
        Gv1.Columns("BoxQtyRecd").HeaderText = "BOX"
        Gv1.Columns("BoxQtyRecd").FormatString = "{0:F0}"

        Gv1.Columns("CanQtyRecd").IsVisible = True
        Gv1.Columns("CanQtyRecd").Width = 100
        Gv1.Columns("CanQtyRecd").HeaderText = "CAN"
        Gv1.Columns("CanQtyRecd").FormatString = "{0:F0}"

        Gv1.Columns("CrateOutQty").IsVisible = True
        Gv1.Columns("CrateOutQty").Width = 100
        Gv1.Columns("CrateOutQty").HeaderText = "Crate"
        Gv1.Columns("CrateOutQty").FormatString = "{0:F0}"

        Gv1.Columns("jaaliOutQty").IsVisible = True
        Gv1.Columns("jaaliOutQty").Width = 100
        Gv1.Columns("jaaliOutQty").HeaderText = "Jaali"
        Gv1.Columns("jaaliOutQty").FormatString = "{0:F0}"

        Gv1.Columns("boxOutQty").IsVisible = True
        Gv1.Columns("boxOutQty").Width = 100
        Gv1.Columns("boxOutQty").HeaderText = "BOX"
        Gv1.Columns("boxOutQty").FormatString = "{0:F0}"

        Gv1.Columns("CanOutQty").IsVisible = True
        Gv1.Columns("CanOutQty").Width = 100
        Gv1.Columns("CanOutQty").HeaderText = "CAN"
        Gv1.Columns("CanOutQty").FormatString = "{0:F0}"

        Gv1.Columns("CrateAdjQty").IsVisible = True
        Gv1.Columns("CrateAdjQty").Width = 100
        Gv1.Columns("CrateAdjQty").HeaderText = "Crate"
        Gv1.Columns("CrateAdjQty").FormatString = "{0:F0}"

        Gv1.Columns("JaaliAdjQty").IsVisible = True
        Gv1.Columns("JaaliAdjQty").Width = 100
        Gv1.Columns("JaaliAdjQty").HeaderText = "Jaali"
        Gv1.Columns("JaaliAdjQty").FormatString = "{0:F0}"

        Gv1.Columns("BoxAdjQty").IsVisible = True
        Gv1.Columns("BoxAdjQty").Width = 100
        Gv1.Columns("BoxAdjQty").HeaderText = "BOX"
        Gv1.Columns("BoxAdjQty").FormatString = "{0:F0}"

        Gv1.Columns("CanAdjQty").IsVisible = True
        Gv1.Columns("CanAdjQty").Width = 100
        Gv1.Columns("CanAdjQty").HeaderText = "CAN"
        Gv1.Columns("CanAdjQty").FormatString = "{0:F0}"

        Gv1.Columns("CrateQtyClosing").IsVisible = True
        Gv1.Columns("CrateQtyClosing").Width = 100
        Gv1.Columns("CrateQtyClosing").HeaderText = "Crate"
        Gv1.Columns("CrateQtyClosing").FormatString = "{0:F0}"

        Gv1.Columns("JaaliQtyClosing").IsVisible = True
        Gv1.Columns("JaaliQtyClosing").Width = 100
        Gv1.Columns("JaaliQtyClosing").HeaderText = "Jaali"
        Gv1.Columns("JaaliQtyClosing").FormatString = "{0:F0}"

        Gv1.Columns("BoxQtyClosing").IsVisible = True
        Gv1.Columns("BoxQtyClosing").Width = 100
        Gv1.Columns("BoxQtyClosing").HeaderText = "BOX"
        Gv1.Columns("BoxQtyClosing").FormatString = "{0:F0}"

        Gv1.Columns("CANQtyClosing").IsVisible = True
        Gv1.Columns("CANQtyClosing").Width = 100
        Gv1.Columns("CANQtyClosing").HeaderText = "CAN"
        Gv1.Columns("CANQtyClosing").FormatString = "{0:F0}"

        Gv1.Columns("CrateValueClosing").IsVisible = True
        Gv1.Columns("CrateValueClosing").Width = 100
        Gv1.Columns("CrateValueClosing").HeaderText = "Crate"
        Gv1.Columns("CrateValueClosing").FormatString = "{0:F0}"

        Gv1.Columns("JaaliValueClosing").IsVisible = True
        Gv1.Columns("JaaliValueClosing").Width = 100
        Gv1.Columns("JaaliValueClosing").HeaderText = "Jaali"
        Gv1.Columns("JaaliValueClosing").FormatString = "{0:F0}"

        Gv1.Columns("BoxValueClosing").IsVisible = True
        Gv1.Columns("BoxValueClosing").Width = 100
        Gv1.Columns("BoxValueClosing").HeaderText = "BOX"
        Gv1.Columns("BoxValueClosing").FormatString = "{0:F0}"

        Gv1.Columns("CANValueClosing").IsVisible = True
        Gv1.Columns("CANValueClosing").Width = 100
        Gv1.Columns("CANValueClosing").HeaderText = "CAN"
        Gv1.Columns("CANValueClosing").FormatString = "{0:F0}"

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        If chkCustomerWise.Checked = True Then
            Dim item1 As New GridViewSummaryItem("OpencrateQty", "{0:F0}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Dim item16 As New GridViewSummaryItem("OpenCanQty", "{0:F0}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item16)
        ElseIf chkcustomerWithDateWise.Checked = True Then

            Dim TotalCrateOpening As New GridViewSummaryItem()
            TotalCrateOpening.FormatString = "{0:F0}"
            TotalCrateOpening.Name = "OpencrateQty"
            TotalCrateOpening.AggregateExpression = "sum(OpencrateQtySum)"
            summaryRowItem.Add(TotalCrateOpening)

            Dim TotalCanOpening As New GridViewSummaryItem()
            TotalCanOpening.FormatString = "{0:F0}"
            TotalCanOpening.Name = "OpenCanQty"
            TotalCanOpening.AggregateExpression = "sum(OpenCanQtySum)"
            summaryRowItem.Add(TotalCanOpening)
        Else
            Dim TotalCrateOpening As New GridViewSummaryItem()
            TotalCrateOpening.FormatString = "{0:F0}"
            TotalCrateOpening.Name = "OpencrateQty"
            TotalCrateOpening.AggregateExpression = "sum(OpencrateQtySumOfVehicleWise)"
            summaryRowItem.Add(TotalCrateOpening)

            Dim TotalCanOpening As New GridViewSummaryItem()
            TotalCanOpening.FormatString = "{0:F0}"
            TotalCanOpening.Name = "OpenCanQty"
            TotalCanOpening.AggregateExpression = "sum(OpencanQtySumOfVehicleWise)"
            summaryRowItem.Add(TotalCanOpening)
        End If
        ''---------------------
        Dim item2 As New GridViewSummaryItem("OpenJaaliQty", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("OpenBoxQty", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)

        Dim item4 As New GridViewSummaryItem("CrateQtyRecd", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem("JaaliQtyRecd", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Dim item6 As New GridViewSummaryItem("BoxQtyRecd", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)

        Dim item17 As New GridViewSummaryItem("CanQtyRecd", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item17)

        Dim item7 As New GridViewSummaryItem("CrateOutQty", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)
        Dim item8 As New GridViewSummaryItem("jaaliOutQty", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8)
        Dim item9 As New GridViewSummaryItem("boxOutQty", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item9)

        Dim item18 As New GridViewSummaryItem("CanOutQty", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item18)
        ''richa agarwal ERO/22/04/19-000568 22 Apr,2019
        If chkCustomerWise.Checked = True Then
            Dim item10 As New GridViewSummaryItem("CrateQtyClosing", "{0:F0}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item10)
            Dim item19 As New GridViewSummaryItem("CanQtyClosing", "{0:F0}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item19)
        ElseIf chkcustomerWithDateWise.Checked = True Then

            Dim TotalCrateOpening As New GridViewSummaryItem()
            TotalCrateOpening.FormatString = "{0:F0}"
            TotalCrateOpening.Name = "CrateQtyClosing"
            TotalCrateOpening.AggregateExpression = "sum(OpencrateQtySum)+sum(CrateOutQty)-sum(CrateQtyRecd)-sum(CrateAdjQty)"
            summaryRowItem.Add(TotalCrateOpening)

            Dim TotalCanOpening As New GridViewSummaryItem()
            TotalCanOpening.FormatString = "{0:F0}"
            TotalCanOpening.Name = "CanQtyClosing"
            TotalCanOpening.AggregateExpression = "sum(OpenCanQtySum)+sum(CanOutQty)-sum(CanQtyRecd)-sum(CanAdjQty)"
            summaryRowItem.Add(TotalCanOpening)
        Else
            Dim TotalCrateOpening As New GridViewSummaryItem()
            TotalCrateOpening.FormatString = "{0:F0}"
            TotalCrateOpening.Name = "CrateQtyClosing"
            TotalCrateOpening.AggregateExpression = "sum(OpencrateQtySumOfVehicleWise)+sum(CrateOutQty)-sum(CrateQtyRecd)-sum(CrateAdjQty)"
            summaryRowItem.Add(TotalCrateOpening)

            Dim TotalCanOpening As New GridViewSummaryItem()
            TotalCanOpening.FormatString = "{0:F0}"
            TotalCanOpening.Name = "CanQtyClosing"
            TotalCanOpening.AggregateExpression = "sum(OpencanQtySumOfVehicleWise)+sum(CanOutQty)-sum(CanQtyRecd)-sum(CanAdjQty)"
            summaryRowItem.Add(TotalCanOpening)
        End If
        ''---------------------
        Dim item11 As New GridViewSummaryItem("JaaliQtyClosing", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item11)
        Dim item12 As New GridViewSummaryItem("BoxQtyClosing", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item12)



        Dim item13 As New GridViewSummaryItem("CrateAdjQty", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item13)
        Dim item14 As New GridViewSummaryItem("JaaliAdjQty", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item14)
        Dim item15 As New GridViewSummaryItem("BoxAdjQty", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item15)

        Dim item20 As New GridViewSummaryItem("CanAdjQty", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item20)

        Dim item21 As New GridViewSummaryItem("CanValueClosing", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item21)
        Dim item22 As New GridViewSummaryItem("CrateValueClosing", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item22)
        Dim item23 As New GridViewSummaryItem("JaaliValueClosing", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item23)
        Dim item24 As New GridViewSummaryItem("BoxValueClosing", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item24)

        Gv1.ShowGroupPanel = False
        Gv1.MasterTemplate.AutoExpandGroups = True

        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        ''View()
    End Sub



    Sub FormatGrid()
        Gv1.TableElement.TableHeaderHeight = 25
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).Width = 100
            Gv1.Columns(ii).IsVisible = False
        Next

        If chkCustomerWise.Checked Then
            Gv1.Columns("Date").IsVisible = False
            Gv1.Columns("Date").Width = 100
            Gv1.Columns("Date").HeaderText = "Date"

            If Gv1.Columns.Contains("Route Code") Then
                Gv1.Columns("Route Code").IsVisible = False
                Gv1.Columns("Route Code").Width = 100
                Gv1.Columns("Route Code").HeaderText = "Route Code"
            End If

            If Gv1.Columns.Contains("Route Name") Then
                Gv1.Columns("Route Name").IsVisible = False
                Gv1.Columns("Route Name").Width = 100
                Gv1.Columns("Route Name").HeaderText = "Route Name"
            End If

            Gv1.Columns("Vehicle Code").IsVisible = False
            Gv1.Columns("Vehicle Code").Width = 100
            Gv1.Columns("Vehicle Code").HeaderText = "Vehicle Code"

            Gv1.Columns("Vehicle Name").IsVisible = False
            Gv1.Columns("Vehicle Name").Width = 100
            Gv1.Columns("Vehicle Name").HeaderText = "Vehicle Name"
        ElseIf chkcustomerWithDateWise.Checked Then
            Gv1.Columns("Date").IsVisible = True
                Gv1.Columns("Date").Width = 100
                Gv1.Columns("Date").HeaderText = "Date"

            If Gv1.Columns.Contains("Route Code") Then
                Gv1.Columns("Route Code").IsVisible = False
                Gv1.Columns("Route Code").Width = 100
                Gv1.Columns("Route Code").HeaderText = "Route Code"
            End If

            If Gv1.Columns.Contains("Route Name") Then
                Gv1.Columns("Route Name").IsVisible = False
                Gv1.Columns("Route Name").Width = 100
                Gv1.Columns("Route Name").HeaderText = "Route Name"
            End If


            Gv1.Columns("Vehicle Code").IsVisible = False
            Gv1.Columns("Vehicle Code").Width = 100
            Gv1.Columns("Vehicle Code").HeaderText = "Vehicle Code"

            Gv1.Columns("Vehicle Name").IsVisible = False
            Gv1.Columns("Vehicle Name").Width = 100
            Gv1.Columns("Vehicle Name").HeaderText = "Vehicle Name"

        Else
            Gv1.Columns("Date").IsVisible = True
            Gv1.Columns("Date").Width = 100
            Gv1.Columns("Date").HeaderText = "Date"

            Gv1.Columns("Vehicle Code").IsVisible = True
            Gv1.Columns("Vehicle Code").Width = 100
            Gv1.Columns("Vehicle Code").HeaderText = "Vehicle Code"

            Gv1.Columns("Vehicle Name").IsVisible = True
            Gv1.Columns("Vehicle Name").Width = 100
            Gv1.Columns("Vehicle Name").HeaderText = "Vehicle Name"
        End If


        Gv1.Columns("Customer Code").IsVisible = True
        Gv1.Columns("Customer Code").Width = 100
        Gv1.Columns("Customer Code").HeaderText = "Customer Code"

        Gv1.Columns("Customer Name").IsVisible = True
        Gv1.Columns("Customer Name").Width = 100
        Gv1.Columns("Customer Name").HeaderText = "Customer Name"

        ' Ticket No : ERO/30/04/19-000576,ERO/23/05/19-000618 By Prabhakar RICHA BHA/20/06/19-000909
        '===================================================================
        If clsCommon.CompairString(objCommonVar.CurrDatabase, "BHARAT") <> CompairStringResult.Equal Then
            Gv1.Columns("Customer Phone").IsVisible = True
            Gv1.Columns("Customer Phone").Width = 100
            Gv1.Columns("Customer Phone").HeaderText = "Customer Phone"

            Gv1.Columns("ZSM Code").IsVisible = True
            Gv1.Columns("ZSM Code").Width = 100
            Gv1.Columns("ZSM Code").HeaderText = "ZSM Code"

            Gv1.Columns("ZSM Name").IsVisible = True
            Gv1.Columns("ZSM Name").Width = 100
            Gv1.Columns("ZSM Name").HeaderText = "ZSM Name"

            Gv1.Columns("ZSM Phone").IsVisible = True
            Gv1.Columns("ZSM Phone").Width = 100
            Gv1.Columns("ZSM Phone").HeaderText = "ZSM Phone"

            Gv1.Columns("ASM Code").IsVisible = True
            Gv1.Columns("ASM Code").Width = 100
            Gv1.Columns("ASM Code").HeaderText = "ASM Code"

            Gv1.Columns("ASM Name").IsVisible = True
            Gv1.Columns("ASM Name").Width = 100
            Gv1.Columns("ASM Name").HeaderText = "ASM Name"

            Gv1.Columns("ASM Phone").IsVisible = True
            Gv1.Columns("ASM Phone").Width = 100
            Gv1.Columns("ASM Phone").HeaderText = "ASM Phone"

            Gv1.Columns("ASO Code").IsVisible = True
            Gv1.Columns("ASO Code").Width = 100
            Gv1.Columns("ASO Code").HeaderText = "ASO Code"

            Gv1.Columns("ASO Name").IsVisible = True
            Gv1.Columns("ASO Name").Width = 100
            Gv1.Columns("ASO Name").HeaderText = "ASO Name"

            Gv1.Columns("ASO Phone").IsVisible = True
            Gv1.Columns("ASO Phone").Width = 100
            Gv1.Columns("ASO Phone").HeaderText = "ASO Phone"
        End If

        Gv1.Columns("OpencrateQty").IsVisible = True
        Gv1.Columns("OpencrateQty").Width = 100
        Gv1.Columns("OpencrateQty").HeaderText = "Crate"
        Gv1.Columns("OpencrateQty").FormatString = "{0:F0}"

        ''RICHA BHA/20/06/19-000909
        If clsCommon.CompairString(objCommonVar.CurrDatabase, "BHARAT") <> CompairStringResult.Equal Then
            Gv1.Columns("OpenJaaliQty").IsVisible = True
            Gv1.Columns("OpenJaaliQty").Width = 100
            Gv1.Columns("OpenJaaliQty").HeaderText = "Jaali"
            Gv1.Columns("OpenJaaliQty").FormatString = "{0:F0}"

            Gv1.Columns("OpenBoxQty").IsVisible = True
            Gv1.Columns("OpenBoxQty").Width = 100
            Gv1.Columns("OpenBoxQty").HeaderText = "BOX"
            Gv1.Columns("OpenBoxQty").FormatString = "{0:F0}"
        End If

        Gv1.Columns("OpenCanQty").IsVisible = True
        Gv1.Columns("OpenCanQty").Width = 100
        Gv1.Columns("OpenCanQty").HeaderText = "CAN"
        Gv1.Columns("OpenCanQty").FormatString = "{0:F0}"

        Gv1.Columns("CrateQtyRecd").IsVisible = True
        Gv1.Columns("CrateQtyRecd").Width = 100
        Gv1.Columns("CrateQtyRecd").HeaderText = "Crate"
        Gv1.Columns("CrateQtyRecd").FormatString = "{0:F0}"

        ''RICHA BHA/20/06/19-000909
        If clsCommon.CompairString(objCommonVar.CurrDatabase, "BHARAT") <> CompairStringResult.Equal Then
            Gv1.Columns("JaaliQtyRecd").IsVisible = True
            Gv1.Columns("JaaliQtyRecd").Width = 100
            Gv1.Columns("JaaliQtyRecd").HeaderText = "Jaali"
            Gv1.Columns("JaaliQtyRecd").FormatString = "{0:F0}"

            Gv1.Columns("BoxQtyRecd").IsVisible = True
            Gv1.Columns("BoxQtyRecd").Width = 100
            Gv1.Columns("BoxQtyRecd").HeaderText = "BOX"
            Gv1.Columns("BoxQtyRecd").FormatString = "{0:F0}"
        End If

        Gv1.Columns("CanQtyRecd").IsVisible = True
        Gv1.Columns("CanQtyRecd").Width = 100
        Gv1.Columns("CanQtyRecd").HeaderText = "CAN"
        Gv1.Columns("CanQtyRecd").FormatString = "{0:F0}"

        Gv1.Columns("CrateOutQty").IsVisible = True
        Gv1.Columns("CrateOutQty").Width = 100
        Gv1.Columns("CrateOutQty").HeaderText = "Crate"
        Gv1.Columns("CrateOutQty").FormatString = "{0:F0}"

        ''RICHA BHA/20/06/19-000909
        If clsCommon.CompairString(objCommonVar.CurrDatabase, "BHARAT") <> CompairStringResult.Equal Then
            Gv1.Columns("jaaliOutQty").IsVisible = True
            Gv1.Columns("jaaliOutQty").Width = 100
            Gv1.Columns("jaaliOutQty").HeaderText = "Jaali"
            Gv1.Columns("jaaliOutQty").FormatString = "{0:F0}"

            Gv1.Columns("boxOutQty").IsVisible = True
            Gv1.Columns("boxOutQty").Width = 100
            Gv1.Columns("boxOutQty").HeaderText = "BOX"
            Gv1.Columns("boxOutQty").FormatString = "{0:F0}"
        End If

        Gv1.Columns("CanOutQty").IsVisible = True
        Gv1.Columns("CanOutQty").Width = 100
        Gv1.Columns("CanOutQty").HeaderText = "CAN"
        Gv1.Columns("CanOutQty").FormatString = "{0:F0}"

        ''RICHA BHA/20/06/19-000909
        If clsCommon.CompairString(objCommonVar.CurrDatabase, "BHARAT") <> CompairStringResult.Equal Then
            Gv1.Columns("CrateAdjQty").IsVisible = True
            Gv1.Columns("CrateAdjQty").Width = 100
            Gv1.Columns("CrateAdjQty").HeaderText = "Crate"
            Gv1.Columns("CrateAdjQty").FormatString = "{0:F0}"

            Gv1.Columns("JaaliAdjQty").IsVisible = True
            Gv1.Columns("JaaliAdjQty").Width = 100
            Gv1.Columns("JaaliAdjQty").HeaderText = "Jaali"
            Gv1.Columns("JaaliAdjQty").FormatString = "{0:F0}"

            Gv1.Columns("BoxAdjQty").IsVisible = True
            Gv1.Columns("BoxAdjQty").Width = 100
            Gv1.Columns("BoxAdjQty").HeaderText = "BOX"
            Gv1.Columns("BoxAdjQty").FormatString = "{0:F0}"

            Gv1.Columns("CanAdjQty").IsVisible = True
            Gv1.Columns("CanAdjQty").Width = 100
            Gv1.Columns("CanAdjQty").HeaderText = "CAN"
            Gv1.Columns("CanAdjQty").FormatString = "{0:F0}"
        End If

        Gv1.Columns("CrateQtyClosing").IsVisible = True
        Gv1.Columns("CrateQtyClosing").Width = 100
        Gv1.Columns("CrateQtyClosing").HeaderText = "Crate"
        Gv1.Columns("CrateQtyClosing").FormatString = "{0:F0}"

        ''RICHA BHA/20/06/19-000909
        If clsCommon.CompairString(objCommonVar.CurrDatabase, "BHARAT") <> CompairStringResult.Equal Then
            Gv1.Columns("JaaliQtyClosing").IsVisible = True
            Gv1.Columns("JaaliQtyClosing").Width = 100
            Gv1.Columns("JaaliQtyClosing").HeaderText = "Jaali"
            Gv1.Columns("JaaliQtyClosing").FormatString = "{0:F0}"

            Gv1.Columns("BoxQtyClosing").IsVisible = True
            Gv1.Columns("BoxQtyClosing").Width = 100
            Gv1.Columns("BoxQtyClosing").HeaderText = "BOX"
            Gv1.Columns("BoxQtyClosing").FormatString = "{0:F0}"
        End If

        Gv1.Columns("CANQtyClosing").IsVisible = True
        Gv1.Columns("CANQtyClosing").Width = 100
        Gv1.Columns("CANQtyClosing").HeaderText = "CAN"
        Gv1.Columns("CANQtyClosing").FormatString = "{0:F0}"

        ''RICHA BHA/20/06/19-000909
        If clsCommon.CompairString(objCommonVar.CurrDatabase, "BHARAT") <> CompairStringResult.Equal Then
            Gv1.Columns("CrateValueClosing").IsVisible = True
            Gv1.Columns("CrateValueClosing").Width = 100
            Gv1.Columns("CrateValueClosing").HeaderText = "Crate"
            Gv1.Columns("CrateValueClosing").FormatString = "{0:F0}"

            Gv1.Columns("JaaliValueClosing").IsVisible = True
            Gv1.Columns("JaaliValueClosing").Width = 100
            Gv1.Columns("JaaliValueClosing").HeaderText = "Jaali"
            Gv1.Columns("JaaliValueClosing").FormatString = "{0:F0}"

            Gv1.Columns("BoxValueClosing").IsVisible = True
            Gv1.Columns("BoxValueClosing").Width = 100
            Gv1.Columns("BoxValueClosing").HeaderText = "BOX"
            Gv1.Columns("BoxValueClosing").FormatString = "{0:F0}"

            Gv1.Columns("CANValueClosing").IsVisible = True
            Gv1.Columns("CANValueClosing").Width = 100
            Gv1.Columns("CANValueClosing").HeaderText = "CAN"
            Gv1.Columns("CANValueClosing").FormatString = "{0:F0}"
        End If

        If chkCrate.Checked = False And chkCan.Checked = False Then
            Gv1.Columns("OpencrateQty").IsVisible = False
            Gv1.Columns("CrateQtyRecd").IsVisible = False
            Gv1.Columns("CrateOutQty").IsVisible = False
            Gv1.Columns("CrateAdjQty").IsVisible = False
            Gv1.Columns("CrateQtyClosing").IsVisible = False
            Gv1.Columns("CrateValueClosing").IsVisible = False
            Gv1.Columns("OpencrateQtySum").IsVisible = False
            Gv1.Columns("OpencrateQtySumOfVehicleWise").IsVisible = False

            Gv1.Columns("OpenCanQty").IsVisible = False
            Gv1.Columns("CanQtyRecd").IsVisible = False
            Gv1.Columns("CanOutQty").IsVisible = False
            Gv1.Columns("CanAdjQty").IsVisible = False
            Gv1.Columns("CanQtyClosing").IsVisible = False
            Gv1.Columns("CanValueClosing").IsVisible = False
            Gv1.Columns("OpenCanQtySum").IsVisible = False
            Gv1.Columns("OpencanQtySumOfVehicleWise").IsVisible = False
        ElseIf chkCrate.Checked = False Then
            Gv1.Columns("OpencrateQty").IsVisible = False
            Gv1.Columns("CrateQtyRecd").IsVisible = False
            Gv1.Columns("CrateOutQty").IsVisible = False
            Gv1.Columns("CrateAdjQty").IsVisible = False
            Gv1.Columns("CrateQtyClosing").IsVisible = False
            Gv1.Columns("CrateValueClosing").IsVisible = False
            Gv1.Columns("OpencrateQtySum").IsVisible = False
            Gv1.Columns("OpencrateQtySumOfVehicleWise").IsVisible = False
        ElseIf chkCan.Checked = False Then
            Gv1.Columns("OpenCanQty").IsVisible = False
            Gv1.Columns("CanQtyRecd").IsVisible = False
            Gv1.Columns("CanOutQty").IsVisible = False
            Gv1.Columns("CanAdjQty").IsVisible = False
            Gv1.Columns("CanQtyClosing").IsVisible = False
            Gv1.Columns("CanValueClosing").IsVisible = False
            Gv1.Columns("OpenCanQtySum").IsVisible = False
            Gv1.Columns("OpencanQtySumOfVehicleWise").IsVisible = False
        End If

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        ''richa agarwal ERO/22/04/19-000568 22 Apr,2019
        If chkCustomerWise.Checked = True Then
            Dim item1 As New GridViewSummaryItem("OpencrateQty", "{0:F0}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Dim item16 As New GridViewSummaryItem("OpenCanQty", "{0:F0}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item16)
        ElseIf chkcustomerWithDateWise.Checked = True Then

            Dim TotalCrateOpening As New GridViewSummaryItem()
            TotalCrateOpening.FormatString = "{0:F0}"
            TotalCrateOpening.Name = "OpencrateQty"
            TotalCrateOpening.AggregateExpression = "sum(OpencrateQtySum)"
            summaryRowItem.Add(TotalCrateOpening)

            Dim TotalCanOpening As New GridViewSummaryItem()
            TotalCanOpening.FormatString = "{0:F0}"
            TotalCanOpening.Name = "OpenCanQty"
            TotalCanOpening.AggregateExpression = "sum(OpenCanQtySum)"
            summaryRowItem.Add(TotalCanOpening)
        Else
            Dim TotalCrateOpening As New GridViewSummaryItem()
            TotalCrateOpening.FormatString = "{0:F0}"
            TotalCrateOpening.Name = "OpencrateQty"
            TotalCrateOpening.AggregateExpression = "sum(OpencrateQtySumOfVehicleWise)"
            summaryRowItem.Add(TotalCrateOpening)

            Dim TotalCanOpening As New GridViewSummaryItem()
            TotalCanOpening.FormatString = "{0:F0}"
            TotalCanOpening.Name = "OpenCanQty"
            TotalCanOpening.AggregateExpression = "sum(OpencanQtySumOfVehicleWise)"
            summaryRowItem.Add(TotalCanOpening)
        End If
        ''---------------------
        Dim item2 As New GridViewSummaryItem("OpenJaaliQty", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("OpenBoxQty", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)

        Dim item4 As New GridViewSummaryItem("CrateQtyRecd", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem("JaaliQtyRecd", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Dim item6 As New GridViewSummaryItem("BoxQtyRecd", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)

        Dim item17 As New GridViewSummaryItem("CanQtyRecd", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item17)

        Dim item7 As New GridViewSummaryItem("CrateOutQty", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)
        Dim item8 As New GridViewSummaryItem("jaaliOutQty", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8)
        Dim item9 As New GridViewSummaryItem("boxOutQty", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item9)

        Dim item18 As New GridViewSummaryItem("CanOutQty", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item18)
        ''richa agarwal ERO/22/04/19-000568 22 Apr,2019
        If chkCustomerWise.Checked = True Then
            Dim item10 As New GridViewSummaryItem("CrateQtyClosing", "{0:F0}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item10)
            Dim item19 As New GridViewSummaryItem("CanQtyClosing", "{0:F0}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item19)
        ElseIf chkcustomerWithDateWise.Checked = True Then

            Dim TotalCrateOpening As New GridViewSummaryItem()
            TotalCrateOpening.FormatString = "{0:F0}"
            TotalCrateOpening.Name = "CrateQtyClosing"
            TotalCrateOpening.AggregateExpression = "sum(OpencrateQtySum)+sum(CrateOutQty)-sum(CrateQtyRecd)-sum(CrateAdjQty)"
            summaryRowItem.Add(TotalCrateOpening)

            Dim TotalCanOpening As New GridViewSummaryItem()
            TotalCanOpening.FormatString = "{0:F0}"
            TotalCanOpening.Name = "CanQtyClosing"
            TotalCanOpening.AggregateExpression = "sum(OpenCanQtySum)+sum(CanOutQty)-sum(CanQtyRecd)-sum(CanAdjQty)"
            summaryRowItem.Add(TotalCanOpening)
        Else
            Dim TotalCrateOpening As New GridViewSummaryItem()
            TotalCrateOpening.FormatString = "{0:F0}"
            TotalCrateOpening.Name = "CrateQtyClosing"
            TotalCrateOpening.AggregateExpression = "sum(OpencrateQtySumOfVehicleWise)+sum(CrateOutQty)-sum(CrateQtyRecd)-sum(CrateAdjQty)"
            summaryRowItem.Add(TotalCrateOpening)

            Dim TotalCanOpening As New GridViewSummaryItem()
            TotalCanOpening.FormatString = "{0:F0}"
            TotalCanOpening.Name = "CanQtyClosing"
            TotalCanOpening.AggregateExpression = "sum(OpencanQtySumOfVehicleWise)+sum(CanOutQty)-sum(CanQtyRecd)-sum(CanAdjQty)"
            summaryRowItem.Add(TotalCanOpening)
        End If
        ''---------------------
        Dim item11 As New GridViewSummaryItem("JaaliQtyClosing", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item11)
        Dim item12 As New GridViewSummaryItem("BoxQtyClosing", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item12)



        Dim item13 As New GridViewSummaryItem("CrateAdjQty", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item13)
        Dim item14 As New GridViewSummaryItem("JaaliAdjQty", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item14)
        Dim item15 As New GridViewSummaryItem("BoxAdjQty", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item15)

        Dim item20 As New GridViewSummaryItem("CanAdjQty", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item20)

        Dim item21 As New GridViewSummaryItem("CanValueClosing", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item21)
        Dim item22 As New GridViewSummaryItem("CrateValueClosing", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item22)
        Dim item23 As New GridViewSummaryItem("JaaliValueClosing", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item23)
        Dim item24 As New GridViewSummaryItem("BoxValueClosing", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item24)

        Gv1.ShowGroupPanel = False
        Gv1.MasterTemplate.AutoExpandGroups = True

        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        ''View()
    End Sub
    Sub View()
        If Gv1.Rows.Count > 0 Then
            Dim view As New ColumnGroupsViewDefinition()


            view.ColumnGroups.Add(New GridViewColumnGroup(""))
            view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(0).Rows(0).Columns.Add(Gv1.Columns("Date"))
            'view.ColumnGroups(0).Rows(0).Columns.Add(Gv1.Columns("Type"))
            If CrateReceivingWithMultipleRoute = True Then
                If Gv1.Columns.Contains("Route_No") Then
                    view.ColumnGroups(0).Rows(0).Columns.Add(Gv1.Columns("Route_No"))
                End If
            Else
                If Gv1.Columns.Contains("Route Code") Then
                    view.ColumnGroups(0).Rows(0).Columns.Add(Gv1.Columns("Route Code"))
                End If
            End If
            If Gv1.Columns.Contains("Route Name") Then
                view.ColumnGroups(0).Rows(0).Columns.Add(Gv1.Columns("Route Name"))
            End If
            view.ColumnGroups(0).Rows(0).Columns.Add(Gv1.Columns("Vehicle Code"))
            view.ColumnGroups(0).Rows(0).Columns.Add(Gv1.Columns("Vehicle Name"))

            If CrateReceivingWithMultipleRoute = False Then


                view.ColumnGroups(0).Rows(0).Columns.Add(Gv1.Columns("Customer Code"))
                view.ColumnGroups(0).Rows(0).Columns.Add(Gv1.Columns("Customer Name"))

                ' Customer Phone , ZSM Code, ZSM Name, ASM Code, ASM Name,ASO Code,ASO Name
                view.ColumnGroups(0).Rows(0).Columns.Add(Gv1.Columns("Customer Phone"))
                view.ColumnGroups(0).Rows(0).Columns.Add(Gv1.Columns("ZSM Code"))
                view.ColumnGroups(0).Rows(0).Columns.Add(Gv1.Columns("ZSM Name"))
                view.ColumnGroups(0).Rows(0).Columns.Add(Gv1.Columns("ZSM Phone"))
                view.ColumnGroups(0).Rows(0).Columns.Add(Gv1.Columns("ASM Code"))
                view.ColumnGroups(0).Rows(0).Columns.Add(Gv1.Columns("ASM Name"))
                view.ColumnGroups(0).Rows(0).Columns.Add(Gv1.Columns("ASM Phone"))
                view.ColumnGroups(0).Rows(0).Columns.Add(Gv1.Columns("ASO Code"))
                view.ColumnGroups(0).Rows(0).Columns.Add(Gv1.Columns("ASO Name"))
                view.ColumnGroups(0).Rows(0).Columns.Add(Gv1.Columns("ASO Phone"))
            End If
            view.ColumnGroups.Add(New GridViewColumnGroup("OPENING"))
            view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(1).Rows(0).Columns.Add(Gv1.Columns("OpencrateQty"))
            view.ColumnGroups(1).Rows(0).Columns.Add(Gv1.Columns("OpenJaaliQty"))
            view.ColumnGroups(1).Rows(0).Columns.Add(Gv1.Columns("OpenBoxQty"))
            view.ColumnGroups(1).Rows(0).Columns.Add(Gv1.Columns("OpenCanQty"))

            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "SPMMD") = CompairStringResult.Equal Then
                view.ColumnGroups.Add(New GridViewColumnGroup("ISSUE"))
                view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(2).Rows(0).Columns.Add(Gv1.Columns("CrateOutQty"))
                view.ColumnGroups(2).Rows(0).Columns.Add(Gv1.Columns("jaaliOutQty"))
                view.ColumnGroups(2).Rows(0).Columns.Add(Gv1.Columns("boxOutQty"))
                view.ColumnGroups(2).Rows(0).Columns.Add(Gv1.Columns("CanOutQty"))

                view.ColumnGroups.Add(New GridViewColumnGroup("RECEIVE"))
                view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(3).Rows(0).Columns.Add(Gv1.Columns("CrateQtyRecd"))
                view.ColumnGroups(3).Rows(0).Columns.Add(Gv1.Columns("JaaliQtyRecd"))
                view.ColumnGroups(3).Rows(0).Columns.Add(Gv1.Columns("BoxQtyRecd"))
                view.ColumnGroups(3).Rows(0).Columns.Add(Gv1.Columns("CanQtyRecd"))
            Else

                view.ColumnGroups.Add(New GridViewColumnGroup("RECEIVE"))
                view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(2).Rows(0).Columns.Add(Gv1.Columns("CrateQtyRecd"))
                view.ColumnGroups(2).Rows(0).Columns.Add(Gv1.Columns("JaaliQtyRecd"))
                view.ColumnGroups(2).Rows(0).Columns.Add(Gv1.Columns("BoxQtyRecd"))
                view.ColumnGroups(2).Rows(0).Columns.Add(Gv1.Columns("CanQtyRecd"))

                view.ColumnGroups.Add(New GridViewColumnGroup("ISSUE"))
                view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(3).Rows(0).Columns.Add(Gv1.Columns("CrateOutQty"))
                view.ColumnGroups(3).Rows(0).Columns.Add(Gv1.Columns("jaaliOutQty"))
                view.ColumnGroups(3).Rows(0).Columns.Add(Gv1.Columns("boxOutQty"))
                view.ColumnGroups(3).Rows(0).Columns.Add(Gv1.Columns("CanOutQty"))

            End If

            view.ColumnGroups.Add(New GridViewColumnGroup("ADJUSTMENT"))
            view.ColumnGroups(4).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(4).Rows(0).Columns.Add(Gv1.Columns("CrateAdjQty"))
            view.ColumnGroups(4).Rows(0).Columns.Add(Gv1.Columns("JaaliAdjQty"))
            view.ColumnGroups(4).Rows(0).Columns.Add(Gv1.Columns("BoxAdjQty"))
            view.ColumnGroups(4).Rows(0).Columns.Add(Gv1.Columns("CanAdjQty"))

            view.ColumnGroups.Add(New GridViewColumnGroup("CLOSING"))
            view.ColumnGroups(5).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(5).Rows(0).Columns.Add(Gv1.Columns("CrateQtyClosing"))
            view.ColumnGroups(5).Rows(0).Columns.Add(Gv1.Columns("JaaliQtyClosing"))
            view.ColumnGroups(5).Rows(0).Columns.Add(Gv1.Columns("BoxQtyClosing"))
            view.ColumnGroups(5).Rows(0).Columns.Add(Gv1.Columns("CanQtyClosing"))


            view.ColumnGroups.Add(New GridViewColumnGroup("CLOSING VALUE"))
            view.ColumnGroups(6).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(6).Rows(0).Columns.Add(Gv1.Columns("CrateValueClosing"))
            view.ColumnGroups(6).Rows(0).Columns.Add(Gv1.Columns("JaaliValueClosing"))
            view.ColumnGroups(6).Rows(0).Columns.Add(Gv1.Columns("BoxValueClosing"))
            view.ColumnGroups(6).Rows(0).Columns.Add(Gv1.Columns("CanValueClosing"))

            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "SPMMD") = CompairStringResult.Equal Then
                view.ColumnGroups(4).IsVisible = False
                view.ColumnGroups(6).IsVisible = False
            End If

            Gv1.ViewDefinition = view
        End If

    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= Gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To Gv1.Columns.Count - 1 Step ii + 1
                        Gv1.Columns(ii).IsVisible = False
                        Gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    Gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        Dim ReportID As String = PageSetupReport_ID
        If clsCommon.myLen(ReportID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = Gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If

            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub


    Private Sub rmExcel_Click(sender As Object, e As EventArgs) Handles rmExcel.Click
        print(Exporter.Excel)
    End Sub

    Private Sub btnQuickExport_Click(sender As Object, e As EventArgs)

    End Sub
    Sub Reset()
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
        txtVehicle.arrValueMember = Nothing
        txtCustomer.arrValueMember = Nothing
        txtRoute.arrValueMember = Nothing
        chkcustomerWithDateWise.Checked = False
        chkCustomerWise.Checked = False
        chkCan.Checked = True
        chkCrate.Checked = True
        txtVehicle.Enabled = True
        RadPageView1.SelectedPage = RadPageViewPage1
        Gv1.DataSource = Nothing
        If CrateReceivingWithMultipleRoute = True Then
            chkCustomerWise.Enabled = False
            chkcustomerWithDateWise.Enabled = False
            txtCustomer.Enabled = False
            pnlActiveInActiveCustomer.Enabled = False
            'txtRoute.Enabled = True
        Else
            chkCustomerWise.Enabled = True
            chkcustomerWithDateWise.Enabled = True
            txtCustomer.Enabled = True
            pnlActiveInActiveCustomer.Enabled = True
            'txtRoute.Enabled = False
        End If
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

            If txtVehicle.arrValueMember IsNot Nothing AndAlso txtVehicle.arrValueMember.Count > 0 Then
                Dim strVehicleName As String = clsCommon.GetMulcallStringWithComma(txtVehicle.arrDispalyMember)
                arrHeader.Add((" Vehicle : " + strVehicleName + " "))
            Else
                arrHeader.Add((" Vehicle : All"))
            End If
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                arrHeader.Add(" Customer : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember))
            Else
                arrHeader.Add((" Customer: All"))
            End If
            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                arrHeader.Add(" Route : " + clsCommon.GetMulcallStringWithComma(txtRoute.arrDispalyMember))
            Else
                arrHeader.Add((" Route: All"))
            End If

            If exporter = EnumExportTo.Excel Then
                'clsCommon.MyExportToExcelGrid("Crate Jali Report", Gv1, arrHeader, Me.Text)
                transportSql.exportdata(Gv1, "", Me.Text, , arrHeader, False, False, True)
            Else
                clsCommon.MyExportToPDF(Me.Text, Gv1, arrHeader, Me.Text, True)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
   
    
    Private Sub txtRoute__My_Click(sender As Object, e As EventArgs) Handles txtRoute._My_Click
        Dim strQry As String = ""
        strQry = "select Route_No  as [Code],Route_Desc as [Name] from TSPL_ROUTE_MASTER"
        txtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("txtRoute@Master", strQry, "Code", "Name", txtRoute.arrValueMember, txtRoute.arrDispalyMember)

    End Sub

    Private Sub PDF_Click(sender As Object, e As EventArgs) Handles PDF.Click
        Try
            Dim StrReportName As String = Me.Text.Replace("/", "")
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

            If txtVehicle.arrValueMember IsNot Nothing AndAlso txtVehicle.arrValueMember.Count > 0 Then
                Dim strVehicleName As String = clsCommon.GetMulcallStringWithComma(txtVehicle.arrDispalyMember)
                arrHeader.Add(("Vehicle : " + strVehicleName + " "))
            Else
                arrHeader.Add(("Vehicle : All"))
            End If
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                arrHeader.Add("Customer : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember))
            Else
                arrHeader.Add(("Customer: All"))
            End If
            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                arrHeader.Add("Route : " + clsCommon.GetMulcallStringWithComma(txtRoute.arrDispalyMember))
            Else
                arrHeader.Add(("Route: All"))
            End If

            clsCommon.MyExportToPDF(Me.Text, Gv1, arrHeader, StrReportName, PageSetupReport_ID, objCommonVar.CurrentUserCode)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub chkCustomerWise_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkCustomerWise.ToggleStateChanged
        If chkCustomerWise.Checked Then
            chkcustomerWithDateWise.Checked = False
            txtVehicle.Enabled = True
        End If
    End Sub

    Private Sub chkcustomerWithDateWise_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkcustomerWithDateWise.ToggleStateChanged
        If chkcustomerWithDateWise.Checked Then
            chkCustomerWise.Checked = False
            txtVehicle.Enabled = False
        End If

    End Sub

    Private Sub ExcelGrid_Click(sender As Object, e As EventArgs) Handles ExcelGrid.Click
        PrintGrid(EnumExportTo.Excel)
    End Sub

    Private Sub PDFGrid_Click(sender As Object, e As EventArgs) Handles PDFGrid.Click
        PrintGrid(EnumExportTo.PDF)
    End Sub

    Sub PrintGrid(ByVal exporter As EnumExportTo)
        Try
            Dim StrReportName As String = Me.Text.Replace("/", "")
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

            If txtVehicle.arrValueMember IsNot Nothing AndAlso txtVehicle.arrValueMember.Count > 0 Then
                Dim strVehicleName As String = clsCommon.GetMulcallStringWithComma(txtVehicle.arrDispalyMember)
                arrHeader.Add((" Vehicle : " + strVehicleName + " "))
            Else
                arrHeader.Add((" Vehicle : All"))
            End If
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                arrHeader.Add(" Customer : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember))
            Else
                arrHeader.Add((" Customer: All"))
            End If
            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                arrHeader.Add(" Route : " + clsCommon.GetMulcallStringWithComma(txtRoute.arrDispalyMember))
            Else
                arrHeader.Add((" Route: All"))
            End If

            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid(Me.Text, Gv1, arrHeader, clsCommon.myCstr(StrReportName), True)
            Else
                Dim FilePath As String = "C:\\ERPTempFolder\\" + clsCommon.myCstr(StrReportName) + clsCommon.GetPrintDate(DateTime.Now, "yyyyMMddhhmmss") + ".pdf"
                Dim pdfExporter As New ExportToPDF(Gv1)
                pdfExporter.Font = New System.Drawing.Font("Verdana", 6)
                pdfExporter.TableBorderThickness = 1
                pdfExporter.FitToPageWidth = True
                pdfExporter.ExportVisualSettings = True
                pdfExporter.ExportHierarchy = True
                pdfExporter.HiddenColumnOption = HiddenOption.DoNotExport
                pdfExporter.PageTitle = Me.Text
                pdfExporter.RunExport(FilePath)
                System.Diagnostics.Process.Start(FilePath)

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

End Class
