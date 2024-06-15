Imports common
Public Class rptDailyStatementReport
    Private Sub rptDailyStatementReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        funreset()
        txtDate.Value = clsCommon.GETSERVERDATE()
    End Sub
    Sub funreset()
        EnableDisableControls(True)
        RadPageView1.SelectedPage = RadPageViewPage1
        rbtnRouteWise.IsChecked = True
    End Sub

    Private Sub EnableDisableControls(ByVal val As Boolean)
        RadGroupBox3.Enabled = val
    End Sub

    Private Sub btnDetail_Click(sender As Object, e As EventArgs) Handles btnDetail.Click
        Dim query As String = ""
        Dim BaseQry As String = ""

        BaseQry = ReturnQry()
        query = "  Select Cust_Code, '" + objCommonVar.CurrentUser + "' as UserName, sum(Final_Qty)Final_Qty, Sku_Seq,max(Phone2)Phone2,max(Phone1)Phone1,max(Circle_No)Circle_No, max(Comp_Name)Comp_Name,max(City_Code)City_Code,max(State)State, max(Add1)Add1,max(Add2)Add2,max(Pincode)Pincode,max(Fax)Fax,max(Date)Date,max(Customer_Name) as Customer_Name,Item_Code,max(Short_Description) AS Short_Description,max(Unit_Desc) as Unit_Desc
		,sum(TotalLtr_ItemWise) as TotalLtr_ItemWise,sum(ItemNetAmount) as TotalAmt_ItemWise,sum(ProdQ) as ProdQ ,SUM(MAmt) AS MAmt,SUM(PAmt) AS PAmt,(sum(isnull(MAmt,0))+sum(isnull(PAmt,0))) as [Total Amount]
        from (  " & Environment.NewLine & " " & BaseQry & ""

        If rbtnDistributorWise.IsChecked Then
            query += " XXXFirst.Cust_Code,	XXXFirst.Item_Code ,XXXFirst.Sku_Seq  order by Sku_Seq "
        ElseIf rbtnRouteWise.IsChecked Then
            query += " XXXFirst.Route_No, XXXFirst.Cust_Code,	XXXFirst.Item_Code ,XXXFirst.Sku_Seq  order by Sku_Seq "

        End If


        Dim dt As DataTable = clsDBFuncationality.GetDataTable(query)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim frmCRV As New frmCrystalReportViewer()
            If rbtnDistributorWise.IsChecked Then
                frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "crptDailyStatementDistributorWiseDetail", "")
            Else

            End If
        Else
            clsCommon.MyMessageBoxShow(Me, "No data found to display", Me.Text)
        End If
    End Sub

    Private Function ReturnQry() As String
        Dim BaseQry As String = ""
        BaseQry = "Select TSPL_DEMAND_BOOKING_MASTER.Route_No, TSPL_DEMAND_BOOKING_MASTER.ShiftType, case when TSPL_ITEM_MASTER.Is_Ambient = 1 then round((isnull(TSPL_DEMAND_BOOKING_DETAIL.Qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I.KG,2) 
		else (case when TSPL_ITEM_MASTER.Is_FreshItem = 1 then round((isnull(TSPL_DEMAND_BOOKING_DETAIL.Qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I.[LTR],2) else 0 end ) end as Final_Qty,case when TSPL_ITEM_MASTER.Is_FreshItem = 1 then	round((isnull(TSPL_DEMAND_BOOKING_DETAIL.Qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I.KG,2) else 0 end as Kg_Qty , tspl_company_master.State,tspl_company_master.City_Code,tspl_company_master.Circle_No,tspl_company_master.Phone1,tspl_company_master.Phone2,   tspl_company_master.Comp_Name,tspl_company_master.Add1,tspl_company_master.Add2,tspl_company_master.Pincode,tspl_company_master.Fax,'" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "' as Date, TSPL_ITEM_MASTER.Sku_Seq, TSPL_CUSTOMER_MASTER.Display_Seq,dist.Cust_Code,coalesce(TSPL_CUSTOMER_MASTER.Customer_Name_Hindi,TSPL_CUSTOMER_MASTER.Customer_Name) as Customer_Name        
	    , TSPL_DEMAND_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Short_Description as Short_Description,TSPL_UNIT_MASTER.Unit_Desc,TSPL_DEMAND_BOOKING_DETAIL.TotalLtr_ItemWise,TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount
       ,TSPL_DEMAND_BOOKING_DETAIL.TotalCrates_ItemWise ,(CASE WHEN TSPL_ITEM_MASTER.Is_Milk_Pouch=0 THEN TSPL_DEMAND_BOOKING_DETAIL.Qty ELSE 0 END) as ProdQ,(CASE WHEN TSPL_ITEM_MASTER.Is_Milk_Pouch=1 THEN TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount ELSE 0 END) as MAmt
       ,(CASE WHEN TSPL_ITEM_MASTER.Is_Milk_Pouch=0 THEN TSPL_DEMAND_BOOKING_DETAIL.Qty ELSE 0 END) as PQty,(CASE WHEN TSPL_ITEM_MASTER.Is_Milk_Pouch=0 THEN TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount ELSE 0 END) as PAmt
	    from TSPL_DEMAND_BOOKING_MASTER Left outer join TSPL_DEMAND_BOOKING_DETAIL On TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No 
        Left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code left outer join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.Unit_Code=TSPL_DEMAND_BOOKING_DETAIL.Unit_code
       left join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code   and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_DEMAND_BOOKING_DETAIL.Unit_Code 
	    left join ( select  Route_No,max(Cust_Code)Cust_Code from TSPL_DISTRIBUTOR_ROUTE_CUSTOMER group by Route_No) dist on dist.Route_No = TSPL_DEMAND_BOOKING_MASTER.Route_No
		left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =dist.Cust_Code  left outer join tspl_company_master on 2 = 2
        left join (  SELECT * FROM ( select item_code,uom_code,conversion_factor from TSPL_ITEM_UOM_DETAIL) I  PIVOT (Max(conversion_factor) FOR uom_code IN ( [KG],[LTR] )) P ) I ON TSPL_DEMAND_BOOKING_DETAIL.Item_Code = I.item_code 
        WHERE  convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date) = CONVERT(DATE, '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "', 103)  ) XXXFirst 
   where Item_Code is  not null and Cust_Code is not null
   Group By "
        Return BaseQry
    End Function
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        funreset()
    End Sub

    Private Sub btnSummary_Click(sender As Object, e As EventArgs) Handles btnSummary.Click
        Dim BaseQry As String = ""
        Dim query As String = ""

        BaseQry = ReturnQry()
        query = "  Select case when ShiftType = 'Morning' THEN 1 else 2 end As Shift_Seq, ShiftType + ' Supply' as ShiftType, '" + objCommonVar.CurrentUser + "' as UserName, sum(Final_Qty)Final_Qty, Sku_Seq,max(Phone2)Phone2,max(Phone1)Phone1,max(Circle_No)Circle_No, max(Comp_Name)Comp_Name,max(City_Code)City_Code,max(State)State, max(Add1)Add1,max(Add2)Add2,max(Pincode)Pincode,max(Fax)Fax,max(Date)Date,max(Customer_Name) as Customer_Name,Item_Code,max(Short_Description) AS Short_Description,max(Unit_Desc) as Unit_Desc
		,sum(TotalLtr_ItemWise) as TotalLtr_ItemWise,sum(ItemNetAmount) as TotalAmt_ItemWise,sum(ProdQ) as ProdQ ,SUM(MAmt) AS MAmt,SUM(PAmt) AS PAmt,(sum(isnull(MAmt,0))+sum(isnull(PAmt,0))) as [Total Amount],sum(Kg_Qty)Kg_Qty
        from (  " & Environment.NewLine & " " & BaseQry & "  XXXFirst.ShiftType,	XXXFirst.Item_Code ,XXXFirst.Sku_Seq  order by ShiftType desc,Sku_Seq "

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(query)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim frmCRV As New frmCrystalReportViewer()
            If rbtnDistributorWise.IsChecked Then
                frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "crptDailyStatementDistributorWiseSummary", "")
            Else

            End If
        Else
            clsCommon.MyMessageBoxShow(Me, "No data found to display", Me.Text)
        End If
    End Sub
End Class