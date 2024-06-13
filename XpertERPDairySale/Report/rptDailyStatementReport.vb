Imports common
Public Class rptDailyStatementReport
    Private Sub rptDailyStatementReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        funreset()
        txtDate.Value = clsCommon.GETSERVERDATE()
    End Sub
    Sub funreset()
        EnableDisableControls(True)
        gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        rbtnRouteWise.IsChecked = True
    End Sub

    Private Sub EnableDisableControls(ByVal val As Boolean)
        RadGroupBox3.Enabled = val
    End Sub

    Private Sub btnDetail_Click(sender As Object, e As EventArgs) Handles btnDetail.Click
        Dim query As String = ""
        query = "  Select Cust_Code,max(Customer_Name) as Customer_Name,Item_Code,max(Short_Description) AS Short_Description,max(Unit_Desc) as Unit_Desc
		,sum(TotalLtr_ItemWise) as TotalLtr_ItemWise,sum(ItemNetAmount) as TotalAmt_ItemWise,sum(ProdQ) as ProdQ ,SUM(MAmt) AS MAmt,SUM(PAmt) AS PAmt,(sum(isnull(MAmt,0))+sum(isnull(PAmt,0))) as [Total Amount]
        from (   Select TSPL_CUSTOMER_MASTER.Display_Seq,dist.Cust_Code,coalesce(TSPL_CUSTOMER_MASTER.Customer_Name_Hindi,TSPL_CUSTOMER_MASTER.Customer_Name) as Customer_Name        
	    , TSPL_DEMAND_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Short_Description as Short_Description,TSPL_UNIT_MASTER.Unit_Desc,TSPL_DEMAND_BOOKING_DETAIL.TotalLtr_ItemWise,TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount
       ,TSPL_DEMAND_BOOKING_DETAIL.TotalCrates_ItemWise ,(CASE WHEN TSPL_ITEM_MASTER.Is_Milk_Pouch=0 THEN TSPL_DEMAND_BOOKING_DETAIL.Qty ELSE 0 END) as ProdQ,(CASE WHEN TSPL_ITEM_MASTER.Is_Milk_Pouch=1 THEN TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount ELSE 0 END) as MAmt
       ,(CASE WHEN TSPL_ITEM_MASTER.Is_Milk_Pouch=0 THEN TSPL_DEMAND_BOOKING_DETAIL.Qty ELSE 0 END) as PQty,(CASE WHEN TSPL_ITEM_MASTER.Is_Milk_Pouch=0 THEN TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount ELSE 0 END) as PAmt
	    from TSPL_DEMAND_BOOKING_MASTER Left outer join TSPL_DEMAND_BOOKING_DETAIL On TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No 
        Left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code left outer join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.Unit_Code=TSPL_DEMAND_BOOKING_DETAIL.Unit_code
	    left join ( select  Route_No,max(Cust_Code)Cust_Code from TSPL_DISTRIBUTOR_ROUTE_CUSTOMER group by Route_No) dist on dist.Route_No = TSPL_DEMAND_BOOKING_MASTER.Route_No
		left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =dist.Cust_Code
        WHERE  convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date) = CONVERT(DATE, '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "', 103)  ) XXXFirst 
   where Item_Code is  not null and Cust_Code is not null
   Group By
    XXXFirst.Cust_Code,	XXXFirst.Item_Code   "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(query)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "crptDailyStatement", "")
        Else
            clsCommon.MyMessageBoxShow(Me, "No data found to display", Me.Text)
        End If
    End Sub

    Private Sub MyLabel3_Click(sender As Object, e As EventArgs) Handles MyLabel3.Click

    End Sub

    Private Sub txtDate_ValueChanged(sender As Object, e As EventArgs) Handles txtDate.ValueChanged

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        funreset()
    End Sub
End Class