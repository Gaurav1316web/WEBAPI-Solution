''created by richa agarwal BHA/09/05/18-000021 on 30 May,2018
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports Telerik.WinControls

Public Class frmCrateCanReceivingReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim strQry As String = ""
    Dim dt As DataTable
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
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
        strQry = " select Cust_Code as [code],Customer_Name as [Name] from TSPL_CUSTOMER_MASTER "
        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
    End Sub


    Private Sub FrmCrateJaliReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+N Refresh ")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+R ")
        Reset()
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv1
        loaddata()

    End Sub
    '=======================Added by preeti gupta Against ticket no[BHA/14/06/18-000054],BHA/05/09/18-000512,BHA/24/06/19-000910,ERO/25/06/19-000655,ERO/25/06/19-000656
    Public Sub loaddata()
        Dim dt As DataTable = Nothing
        Dim qryCrateRec As String = Nothing
        Dim qryDispatch As String = Nothing
        Dim qryReturn As String = Nothing
        Dim qry As String = Nothing

        qryCrateRec = "select 'Crate Rec' as Type,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No,convert(varchar(10),TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date,103)+' ' +convert(varchar(5),TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date,114) as Document_Date, " &
                        " TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Customer_Code,TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Vehicle_Code ,TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.VehicleNo as Vehicle_Name,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Route_code as Route_No, " &
                        " TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Crate_ItemRate,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.CAN_ItemRate as CAN_ItemRate," &
                        " (isnull(TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.CrateQtyRecd,0)+isnull(TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Adjustment,0)+" &
                        " (isnull(TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.outQty,0)))* case when type='I' then (-1) else 1 end as Crate," &
                        " (isnull(TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.CANQTYRec,0)+isnull(TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.CANAdjustment,0)+" &
                        " (isnull(TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.CANoutQty,0)))* case when type='I' then (-1) else 1 end as CAN" &
                        " from TSPL_CRATE_RECEIVED_HEAD_FRESHSALE" &
                        " left outer join TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Document_No  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Customer_Code "

        qryCrateRec += " WHERE convert(date,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date ,103)>=convert(date,'" + fromDate.Value + "',103) AND convert(date,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date,103)<=convert(date,'" + ToDate.Value + "',103)  and isnull(TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Posted,0) =1 " + Environment.NewLine

        If clsCommon.CompairString(ddlSubCategory.SelectedValue, "Against Can") = CompairStringResult.Equal Then
            qryCrateRec += " and TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Trans_type ='Can'"
        ElseIf clsCommon.CompairString(ddlSubCategory.SelectedValue, "Against Crate") = CompairStringResult.Equal Then
            qryCrateRec += " and TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Trans_type ='Crate'"
        Else
            qryCrateRec += " and (((isnull(TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.CrateQtyRecd,0)+isnull(TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Adjustment,0))+ (isnull(TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.outQty,0)))* case when type='I' then (-1) else 1 end <>0 or (  (isnull(TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.CANQTYRec,0)+isnull(TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.CANAdjustment,0))+ (isnull(TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.CANoutQty,0)) ) * case when type='I' then (-1) else 1 end<>0) "
        End If
        If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
            qryCrateRec += " and TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Customer_Code  in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") " + Environment.NewLine
        End If
        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            qryCrateRec += " and TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Location_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") " + Environment.NewLine
        End If
        If txtVehicle.arrValueMember IsNot Nothing AndAlso txtVehicle.arrValueMember.Count > 0 Then
            qryCrateRec += " and TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Vehicle_Code  in (" + clsCommon.GetMulcallString(txtVehicle.arrValueMember) + ") " + Environment.NewLine
        End If

        qryDispatch = "select 'Shipment' asType,TSPL_SD_SHIPMENT_HEAD.Document_Code,convert(varchar(10),TSPL_SD_SHIPMENT_HEAD.Document_Date,103)+' ' +convert(varchar(5),TSPL_SD_SHIPMENT_HEAD.Document_Date,114) as Document_Date" &
                      " , TSPL_SD_SHIPMENT_HEAD.Customer_Code ,(case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)  as Vehicle_Code,(case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then AlterNateVehicle.Number   else TSPL_VEHICLE_MASTER.Number end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end)  as  Vehicle_Name,TSPL_SD_SHIPMENT_HEAD.Route_no, "
        If clsCommon.CompairString(ddlSubCategory.SelectedValue, "Against Can") = CompairStringResult.Equal Then
            qryDispatch += "  0.00 AS crate_itemRate,TSPL_SD_SHIPMENT_HEAD.Can_ItemRate,0.00 as Crate,isnull(TSPL_SD_SHIPMENT_HEAD.shippedCAN,0) as CAN "
        ElseIf clsCommon.CompairString(ddlSubCategory.SelectedValue, "Against Crate") = CompairStringResult.Equal Then
            qryDispatch += "  TSPL_SD_SHIPMENT_HEAD.crate_itemRate,0.00 AS Can_ItemRate,isnull(TSPL_SD_SHIPMENT_HEAD.Crate,0) as Crate,0 as CAN "
        Else
            qryDispatch += "  TSPL_SD_SHIPMENT_HEAD.crate_itemRate,TSPL_SD_SHIPMENT_HEAD.Can_ItemRate,isnull(TSPL_SD_SHIPMENT_HEAD.Crate,0) as Crate,isnull(TSPL_SD_SHIPMENT_HEAD.shippedCAN,0) as CAN "
        End If

        qryDispatch += "  from TSPL_SD_SHIPMENT_HEAD left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_SD_SHIPMENT_HEAD.Customer_Code " & _
            " left join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_SD_SHIPMENT_HEAD.Vehicle_Code " & _
            " left join TSPL_VEHICLE_MASTER as AlterNateVehicle  on AlterNateVehicle.Vehicle_Id=TSPL_SD_SHIPMENT_HEAD.AlternateVehicle " & _
                        "  WHERE convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date ,103)>=convert(date,'" + fromDate.Value + "',103) AND convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103)<=convert(date,'" + ToDate.Value + "',103) and  screen_type='DS' and isnull(TSPL_SD_SHIPMENT_HEAD.Status,0)=1 " + Environment.NewLine
        If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
            qryDispatch += " and TSPL_SD_SHIPMENT_HEAD.Customer_Code  in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") " + Environment.NewLine
        End If
        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            qryDispatch += " and TSPL_SD_SHIPMENT_HEAD.Bill_to_location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") " + Environment.NewLine
        End If
        If txtVehicle.arrValueMember IsNot Nothing AndAlso txtVehicle.arrValueMember.Count > 0 Then
            qryDispatch += " and  (case when isnull(TSPL_SD_SHIPMENT_HEAD.ManualVehicle,'')='' then case when isnull(TSPL_SD_SHIPMENT_HEAD.AlternateVehicle,'')<>'' then TSPL_SD_SHIPMENT_HEAD.AlternateVehicle else TSPL_SD_SHIPMENT_HEAD.Vehicle_Code end else TSPL_SD_SHIPMENT_HEAD.ManualVehicle end) in (" + clsCommon.GetMulcallString(txtVehicle.arrValueMember) + ") " + Environment.NewLine
        End If

        If clsCommon.CompairString(ddlSubCategory.SelectedValue, "Against Can") = CompairStringResult.Equal Then
            qryDispatch += " and isnull(TSPL_SD_SHIPMENT_HEAD.shippedCAN,0) > 0"
        ElseIf clsCommon.CompairString(ddlSubCategory.SelectedValue, "Against Crate") = CompairStringResult.Equal Then
            qryDispatch += " and isnull(TSPL_SD_SHIPMENT_HEAD.Crate, 0)>0"
        Else
            qryDispatch += "  and (isnull(TSPL_SD_SHIPMENT_HEAD.Crate, 0)<>0 or  isnull(TSPL_SD_SHIPMENT_HEAD.shippedCAN,0) <> 0) "
        End If


        qryReturn = " select 'Sale Return' asType,tspl_sd_sale_Return_head.Document_Code,convert(varchar(10),tspl_sd_sale_Return_head.Document_Date,103)+' ' +convert(varchar(5),tspl_sd_sale_Return_head.Document_Date,114) as Document_Date, tspl_sd_sale_Return_head.Customer_Code  ,tspl_sd_sale_Return_head.vehicle_code,tspl_sd_sale_Return_head.vehicleno,tspl_sd_sale_Return_head.Route_No,"

        If clsCommon.CompairString(ddlSubCategory.SelectedValue, "Against Can") = CompairStringResult.Equal Then
            qryReturn += " 0.00 as crate_itemRate,isnull(tspl_sd_sale_Return_head.Can_ItemRate ,0) as Can_ItemRate,0*(-1) as Crate,isnull(tspl_sd_sale_Return_head.ShippedCAN,0)*(-1) as CAN "
        ElseIf clsCommon.CompairString(ddlSubCategory.SelectedValue, "Against Crate") = CompairStringResult.Equal Then
            qryReturn += " isnull(tspl_sd_sale_Return_head.Crate_ItemRate,0) as crate_itemRate,0.00 as Can_ItemRate,isnull(tspl_sd_sale_Return_head.CrateQty,0)*(-1) as Crate,0*(-1) as CAN "
        Else
            qryReturn += " isnull(tspl_sd_sale_Return_head.Crate_ItemRate,0) as crate_itemRate,isnull(tspl_sd_sale_Return_head.Can_ItemRate ,0) as Can_ItemRate,isnull(tspl_sd_sale_Return_head.CrateQty,0)*(-1) as Crate,isnull(tspl_sd_sale_Return_head.ShippedCAN,0)*(-1) as CAN "
        End If


        qryReturn += " from tspl_sd_sale_Return_head" & _
             " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =tspl_sd_sale_Return_head.Customer_Code " & _
             "  WHERE convert(date,tspl_sd_sale_Return_head.Document_Date ,103)>=convert(date,'" + fromDate.Value + "',103) AND convert(date,tspl_sd_sale_Return_head.Document_Date,103)<=convert(date,'" + ToDate.Value + "',103) and  screen_type='DS' and isnull(tspl_sd_sale_Return_head.Status,0)=1 " + Environment.NewLine

        If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
            qryReturn += " and tspl_sd_sale_Return_head.Customer_Code  in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") " + Environment.NewLine
        End If
        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            qryReturn += " and tspl_sd_sale_Return_head.Bill_to_location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") " + Environment.NewLine
        End If
        If txtVehicle.arrValueMember IsNot Nothing AndAlso txtVehicle.arrValueMember.Count > 0 Then
            qryReturn += " and tspl_sd_sale_Return_head.vehicle_code in (" + clsCommon.GetMulcallString(txtVehicle.arrValueMember) + ") " + Environment.NewLine
        End If

        If clsCommon.CompairString(ddlSubCategory.SelectedValue, "Against Can") = CompairStringResult.Equal Then
            qryReturn += " and isnull(tspl_sd_sale_Return_head.ShippedCAN,0) > 0"
        ElseIf clsCommon.CompairString(ddlSubCategory.SelectedValue, "Against Crate") = CompairStringResult.Equal Then
            qryReturn += " and isnull(tspl_sd_sale_Return_head.CrateQty,0)>0"
        Else
            qryReturn += " and (isnull(tspl_sd_sale_Return_head.ShippedCAN,0)<>0 or  isnull(tspl_sd_sale_Return_head.CrateQty,0) <> 0)"
        End If


        qry = "select final.Type,final.Document_No,Document_Date" &
              " ,final.Customer_Code ,TSPL_CUSTOMER_MASTER.Customer_Name,final.vehicle_code,vehicle_Name,final.Route_No,TSPL_ROUTE_MASTER.Route_Desc,TSPL_CUSTOMER_ACCOUNT_SET.Container_Deposit , TSPL_GL_ACCOUNTS.Description,final.Crate_ItemRate,final.CAN_ItemRate,final.Crate,final.CAN" &
              " ,(final.Crate_ItemRate * final.Crate)+(final.CAN_ItemRate*CAN) as Amount " &
              " from (" &
              "" & qryCrateRec & "  " &
              " UNION ALL " &
               "" & qryDispatch & "  " &
             " UNION ALL" &
                "" & qryReturn & "  " &
            " ) as final" &
            " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =final.Customer_Code " &
            " left outer join TSPL_CUSTOMER_ACCOUNT_SET on TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account  =TSPL_CUSTOMER_MASTER.Cust_Account  " &
            " left outer join TSPL_GL_ACCOUNTS  on TSPL_GL_ACCOUNTS.Account_Code   =TSPL_CUSTOMER_ACCOUNT_SET.Container_Deposit left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No=final.Route_No order by Document_No ,convert(date,Document_Date,103) asc"


        dt = clsDBFuncationality.GetDataTable(qry)
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.GroupDescriptors.Clear()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        gv1.DataSource = dt
        gv1.BestFitColumns()
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            Exit Sub
        End If
        FormatGrid()
        RadPageView1.SelectedPage = RadPageViewPage2
        ReStoreGridLayout()
    End Sub
    Sub FormatGrid()
        Gv1.TableElement.TableHeaderHeight = 25
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).Width = 100
        Next

        Gv1.Columns("Document_No").IsVisible = True
        Gv1.Columns("Document_No").Width = 100
        Gv1.Columns("Document_No").HeaderText = "Document No"

        Gv1.Columns("Document_Date").IsVisible = True
        Gv1.Columns("Document_Date").Width = 100
        Gv1.Columns("Document_Date").HeaderText = "Document Date"

        Gv1.Columns("Customer_Code").IsVisible = True
        Gv1.Columns("Customer_Code").Width = 100
        Gv1.Columns("Customer_Code").HeaderText = "Customer Code"

        Gv1.Columns("Customer_Name").IsVisible = True
        Gv1.Columns("Customer_Name").Width = 100
        gv1.Columns("Customer_Name").HeaderText = "Customer Name"

        gv1.Columns("Vehicle_Code").IsVisible = True
        gv1.Columns("Vehicle_Code").Width = 100
        gv1.Columns("Vehicle_Code").HeaderText = "Vehicle_Code"

        gv1.Columns("Vehicle_Name").IsVisible = True
        gv1.Columns("Vehicle_Name").Width = 100
        gv1.Columns("Vehicle_Name").HeaderText = "Vehicle_Name"

        gv1.Columns("Route_No").IsVisible = True
        gv1.Columns("Route_No").Width = 100
        gv1.Columns("Route_No").HeaderText = "Route Code"

        gv1.Columns("Route_Desc").IsVisible = True
        gv1.Columns("Route_Desc").Width = 100
        gv1.Columns("Route_Desc").HeaderText = "Route Name"

        gv1.Columns("Description").IsVisible = True
        gv1.Columns("Description").Width = 100
        gv1.Columns("Description").HeaderText = "Account Name"

        Gv1.Columns("Crate_ItemRate").IsVisible = True
        Gv1.Columns("Crate_ItemRate").Width = 100
        Gv1.Columns("Crate_ItemRate").HeaderText = "CRATE Rate"
        Gv1.Columns("Crate_ItemRate").FormatString = "{0:F2}"

        Gv1.Columns("CAN_ItemRate").IsVisible = True
        Gv1.Columns("CAN_ItemRate").Width = 100
        Gv1.Columns("CAN_ItemRate").HeaderText = "CAN Rate "
        Gv1.Columns("CAN_ItemRate").FormatString = "{0:F0}"

        Gv1.Columns("Crate").IsVisible = True
        Gv1.Columns("Crate").Width = 100
        Gv1.Columns("Crate").HeaderText = "CRATE Qty"
        Gv1.Columns("Crate").FormatString = "{0:F2}"

        Gv1.Columns("CAN").IsVisible = True
        Gv1.Columns("CAN").Width = 100
        Gv1.Columns("CAN").HeaderText = "CAN Qty "
        Gv1.Columns("CAN").FormatString = "{0:F0}"

        Gv1.Columns("Amount").IsVisible = True
        Gv1.Columns("Amount").Width = 100
        Gv1.Columns("Amount").HeaderText = "Amount"
        Gv1.Columns("Amount").FormatString = "{0:F2}"


        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        Dim item1 As New GridViewSummaryItem("Crate", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("CAN", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("Amount", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        
        Gv1.ShowGroupPanel = False
        Gv1.MasterTemplate.AutoExpandGroups = True

        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        'View()
    End Sub
    Sub View()
        If Gv1.Rows.Count > 0 Then
            Dim view As New ColumnGroupsViewDefinition()


            view.ColumnGroups.Add(New GridViewColumnGroup(""))
            view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Date").Name)
            'view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Type"))
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Vehicle Code").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Vehicle Name").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Route Code").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Route Name").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Customer Code").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Customer Name").Name)


            view.ColumnGroups.Add(New GridViewColumnGroup("OPENING"))
            view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("OpencrateQty").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("OpenJaaliQty").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("OpenBoxQty").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("RECEIVE"))
            view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("CrateQtyRecd").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("JaaliQtyRecd").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("BoxQtyRecd").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("ISSUE"))
            view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv1.Columns("CrateOutQty").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv1.Columns("jaaliOutQty").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv1.Columns("boxOutQty").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("ADJUSTMENT"))
            view.ColumnGroups(4).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv1.Columns("CrateAdjQty").Name)
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv1.Columns("JaaliAdjQty").Name)
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv1.Columns("BoxAdjQty").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("CLOSING"))
            view.ColumnGroups(5).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(5).Rows(0).ColumnNames.Add(gv1.Columns("CrateQtyClosing").Name)
            view.ColumnGroups(5).Rows(0).ColumnNames.Add(gv1.Columns("JaaliQtyClosing").Name)
            view.ColumnGroups(5).Rows(0).ColumnNames.Add(gv1.Columns("BoxQtyClosing").Name)


            gv1.ViewDefinition = view
        End If

    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
        Dim ReportID As String = MyBase.Form_ID
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
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If

            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
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
        LoadAgainstType()
        txtVehicle.arrValueMember = Nothing
        txtCustomer.arrValueMember = Nothing
        txtLocation.arrValueMember = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        Gv1.DataSource = Nothing
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                Dim strLocationName As String = clsCommon.GetMulcallStringWithComma(txtLocation.arrValueMember)
                arrHeader.Add((" Location : " + strLocationName + " "))
            Else
                arrHeader.Add((" Location : All"))
            End If
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                arrHeader.Add(" Customer : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrValueMember))
            Else
                arrHeader.Add((" Customer: All"))
            End If
            transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Crate/Can Receiving Report", Gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Crate/Can Receiving Report", gv1, arrHeader, "CrateCan Receiving Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error, Me.Text)
        End Try
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = "select Location_Code as [Code] ,Location_Desc as [Name] from TSPL_LOCATION_MASTER where Location_Type='Physical'"
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("Pro", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    End Sub

    Sub LoadAgainstType()
        dt = New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Rows.Add("Both")
        dt.Rows.Add("Against Can")
        dt.Rows.Add("Against Crate")

        ddlSubCategory.DataSource = dt
        ddlSubCategory.ValueMember = "Code"
        ddlSubCategory.DisplayMember = "Code"
    End Sub

    Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
        print(EnumExportTo.PDF)
    End Sub

   
End Class
