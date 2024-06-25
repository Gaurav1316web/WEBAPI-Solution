Imports common

Public Class rptCreditCustomerReport

    Private Sub rptCreditCustomerReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        funreset()
        txtfromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        funreset()
    End Sub

    Sub funreset()
        txtfromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            Dim qry As String = ""

            qry = " Select '" & clsCommon.GetPrintDate(txtfromDate.Value, "dd/MMM/yyyy") & "' as FromDate,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "' as ToDate,'" + objCommonVar.CurrentUser + "' as UserName,TSPL_DEMAND_BOOKING_MASTER.Route_No,
                    TSPL_CUSTOMER_MASTER.Credit_Customer, TSPL_DEMAND_BOOKING_MASTER.ShiftType, case when TSPL_ITEM_MASTER.Is_Ambient = 1 then round((isnull(TSPL_DEMAND_BOOKING_DETAIL.Qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I.KG,2) 
		            else (case when TSPL_ITEM_MASTER.Is_FreshItem = 1 then round((isnull(TSPL_DEMAND_BOOKING_DETAIL.Qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I.[LTR],2) else 0 end ) end as Final_Qty,case when TSPL_ITEM_MASTER.Is_FreshItem = 1 then	round((isnull(TSPL_DEMAND_BOOKING_DETAIL.Qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I.KG,2) else 0 end as Kg_Qty , tspl_company_master.State,tspl_company_master.City_Code,tspl_company_master.Circle_No,tspl_company_master.Phone1,tspl_company_master.Phone2,   tspl_company_master.Comp_Name,tspl_company_master.Add1,tspl_company_master.Add2,tspl_company_master.Pincode,tspl_company_master.Fax, TSPL_ITEM_MASTER.Sku_Seq, TSPL_CUSTOMER_MASTER.Display_Seq,
		            TSPL_DEMAND_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name as Customer_Name
	                ,TSPL_DEMAND_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Short_Description as Short_Description,TSPL_UNIT_MASTER.Unit_Desc,TSPL_DEMAND_BOOKING_DETAIL.TotalLtr_ItemWise,TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount
                    ,TSPL_DEMAND_BOOKING_DETAIL.TotalCrates_ItemWise ,(CASE WHEN TSPL_ITEM_MASTER.Is_Milk_Pouch=0 THEN TSPL_DEMAND_BOOKING_DETAIL.Qty ELSE 0 END) as ProdQ,(CASE WHEN TSPL_ITEM_MASTER.Is_Milk_Pouch=1 THEN TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount ELSE 0 END) as MAmt
                    ,(CASE WHEN TSPL_ITEM_MASTER.Is_Milk_Pouch=0 THEN TSPL_DEMAND_BOOKING_DETAIL.Qty ELSE 0 END) as PQty,(CASE WHEN TSPL_ITEM_MASTER.Is_Milk_Pouch=0 THEN TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount ELSE 0 END) as PAmt,
	                  ((CASE WHEN TSPL_ITEM_MASTER.Is_Milk_Pouch=1 THEN TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount ELSE 0 END)+(CASE WHEN TSPL_ITEM_MASTER.Is_Milk_Pouch=0 THEN TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount ELSE 0 END)) as TotalAmt
                    from TSPL_DEMAND_BOOKING_MASTER 
		            Left outer join TSPL_DEMAND_BOOKING_DETAIL On TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No 
                    Left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code
		            left outer join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.Unit_Code=TSPL_DEMAND_BOOKING_DETAIL.Unit_code
                    left join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code   and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_DEMAND_BOOKING_DETAIL.Unit_Code 
		            left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_DEMAND_BOOKING_DETAIL.Cust_Code 
		            left outer join tspl_company_master on 2 = 2
                    left join (  SELECT * FROM ( select item_code,uom_code,conversion_factor from TSPL_ITEM_UOM_DETAIL) I  PIVOT (Max(conversion_factor) FOR uom_code IN ( [KG],[LTR] )) P ) I ON TSPL_DEMAND_BOOKING_DETAIL.Item_Code = I.item_code 
                    WHERE  convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date) >= CONVERT(DATE, '" & clsCommon.GetPrintDate(txtfromDate.Value, "dd/MMM/yyyy") & "', 103) 
                    and convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date) <= CONVERT(DATE, '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "', 103) and Credit_Customer='Y' "


            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                'If rbtnDistributorWise.IsChecked Then
                frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "CrptCreditCustomerReport", "")
                'Else
                '    frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "crptDailyStatementDistributorWiseSummary", "")
                'End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to display", Me.Text)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class