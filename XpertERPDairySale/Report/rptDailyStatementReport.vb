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
        Dim Qry As String = ""
        Dim dtDepartmentSale As DataTable = New DataTable()
        Dim DeptQry As String = ""
        Dim whrcls As String = "where 2 = 2 and  convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date) = CONVERT(DATE, '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "', 103) "

        If rbtnMilkType.IsChecked Then
            whrcls += " and  Is_FreshItem = 1"
        ElseIf rbtnProduct.IsChecked Then
            whrcls += " and  Is_Ambient = 1"

        End If
        Try
            BaseQry = " Select Credit_Customer,  CASE WHEN Credit_Customer = 'y' THEN CASE WHEN TSPL_ITEM_MASTER.Is_Ambient = 1 THEN ROUND((ISNULL(TSPL_DEMAND_BOOKING_DETAIL.Qty, 0) * ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 1)) / I.KG, 2)  ELSE  CASE WHEN TSPL_ITEM_MASTER.Is_FreshItem = 1 THEN 
                        ROUND((ISNULL(TSPL_DEMAND_BOOKING_DETAIL.Qty, 0) * ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 1)) / I.LTR, 2) ELSE 0 END END ELSE 0 END AS Credit_CustomerQty,TSPL_CUSTOMER_MASTER.Cust_Code,coalesce(TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_CUSTOMER_MASTER.Customer_Name) as Customer_Name  ,TSPL_DEMAND_BOOKING_MASTER.ShiftType,
                        case when TSPL_ITEM_MASTER.Is_Ambient = 1 then round((isnull(TSPL_DEMAND_BOOKING_DETAIL.Qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I.KG,2) else (case when TSPL_ITEM_MASTER.Is_FreshItem = 1 then round((isnull(TSPL_DEMAND_BOOKING_DETAIL.Qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I.[LTR],2) else 0 end ) end as Final_Qty
                       ,case when TSPL_ITEM_MASTER.Is_FreshItem = 1 then	round((isnull(TSPL_DEMAND_BOOKING_DETAIL.Qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I.KG,2) else 0 end as Kg_Qty , tspl_company_master.State,tspl_company_master.City_Code,tspl_company_master.Circle_No,tspl_company_master.Phone1,tspl_company_master.Phone2,   tspl_company_master.Comp_Name
                       ,tspl_company_master.Add1,tspl_company_master.Add2,tspl_company_master.Pincode,tspl_company_master.Fax,CONVERT(DATE, '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "', 103) as Date, TSPL_ITEM_MASTER.Sku_Seq , TSPL_DEMAND_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Short_Description as Short_Description,TSPL_UNIT_MASTER.Unit_Desc,TSPL_DEMAND_BOOKING_DETAIL.TotalLtr_ItemWise,TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount
                       ,TSPL_DEMAND_BOOKING_DETAIL.TotalCrates_ItemWise ,(CASE WHEN TSPL_ITEM_MASTER.Is_Milk_Pouch=0 THEN TSPL_DEMAND_BOOKING_DETAIL.Qty ELSE 0 END) as ProdQ,"

            If rbtnDistributorWise.IsChecked Then
                DeptQry += "  Select *,ISNULL(MAmt,0) as [Total Amount] from  (   Select   '" + objCommonVar.CurrentUser + "' as UserName,'Department Sale' as Cust_Code,max(Customer_Name)Customer_Name1,max(Customer_Name)Customer_Name4,'Department Sale' AS Customer_Name, sum(Final_Qty)Final_Qty, Sku_Seq,max(Phone2)Phone2,max(Phone1)Phone1,max(Circle_No)Circle_No, max(Comp_Name)Comp_Name,max(City_Code)City_Code,max(State)State,Item_Code, max(Add1)Add1,max(Add2)Add2,max(Pincode)Pincode,max(Fax)Fax,max(Date)Date,max(Short_Description) AS Short_Description,max(Unit_Desc) as Unit_Desc
		        ,sum(TotalLtr_ItemWise) as TotalLtr_ItemWise,sum(ItemNetAmount) as TotalAmt_ItemWise,SUM(MAmt) AS MAmt, max(Credit_Customer)Credit_Customer,isnull(cast(sum(Credit_CustomerQty) as decimal(18,2)),0) Credit_CustomerQty,isnull(sum(PAmt),0)PAmt,isnull(sum(PAmt),0) + ISNULL(sum(MAmt),0) as [Total Amount] from (    " & Environment.NewLine & " " & BaseQry & " (CASE WHEN TSPL_ITEM_MASTER.Is_Milk_Pouch=1 and Credit_Customer='Y' THEN TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount ELSE 0 END) as MAmt from TSPL_DEMAND_BOOKING_MASTER  Left outer join TSPL_DEMAND_BOOKING_DETAIL On TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No 
                        Left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code left outer join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.Unit_Code=TSPL_DEMAND_BOOKING_DETAIL.Unit_code left join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code   and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_DEMAND_BOOKING_DETAIL.Unit_Code 
                        left outer join tspl_company_master on 2 = 2 left join (  SELECT * FROM ( select item_code,uom_code,conversion_factor from TSPL_ITEM_UOM_DETAIL) I  PIVOT (Max(conversion_factor) FOR uom_code IN ( [KG],[LTR] )) P ) I ON TSPL_DEMAND_BOOKING_DETAIL.Item_Code = I.item_code Left Join TSPL_ROUTE_MASTER on TSPL_DEMAND_BOOKING_MASTER.Route_No = TSPL_ROUTE_MASTER.Route_No left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_DEMAND_BOOKING_DETAIL.Cust_Code   " & whrcls & " and  Credit_Customer = 'Y'  ) XXXFirst 
                left join (  Select  Cust_Code,SUM(PAmt) AS PAmt from ( Select TSPL_CUSTOMER_MASTER.Cust_Code,Credit_Customer, (CASE WHEN TSPL_ITEM_MASTER.Is_Milk_Pouch=0   and Credit_Customer='Y' THEN TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount ELSE 0 END) as PAmt from TSPL_DEMAND_BOOKING_MASTER  Left outer join TSPL_DEMAND_BOOKING_DETAIL On TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No 
                Left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_DEMAND_BOOKING_DETAIL.Cust_Code  WHERE  convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date) = CONVERT(DATE, '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "', 103) and Is_Ambient = 1 and Credit_Customer = 'Y'  ) XX group by  xx.Cust_Code ) xx on xx.Cust_Code = XXXFirst.Cust_Code  Group By  XXXFirst.Item_Code ,XXXFirst.Sku_Seq ) YY  "

                dtDepartmentSale = clsDBFuncationality.GetDataTable(DeptQry)
                query = " select *,isnull(PAmt1,0)PAmt, isnull(PAmt1,0) + isnull(MAmt,0) as [Total Amount] from (  select  '" & dtDepartmentSale.Rows.Count & "' as DeptCount,  case when yyy.Credit_Customer='Y'   then isnull(Credit_CustomerQty,0) else isnull(Final_Qty,0) end as final,case when  yyy.Credit_Customer='Y' and Customer_Name4='I-DPT' then Final_Qty else 0 end as InDepartment,* from  ( Select  '" + objCommonVar.CurrentUser + "' as UserName ,Cust_Code,max(Customer_Name)Customer_Name1,max(Customer_Name)Customer_Name4,max(Customer_Name)Customer_Name, sum(Final_Qty)Final_Qty,
                Sku_Seq,max(Phone2)Phone2,max(Phone1)Phone1,max(Circle_No)Circle_No, max(Comp_Name)Comp_Name,max(City_Code)City_Code,max(State)State,Item_Code, max(Add1)Add1,max(Add2)Add2,max(Pincode)Pincode,max(Fax)Fax,max(Date)Date,max(Short_Description) AS Short_Description,max(Unit_Desc) as Unit_Desc,sum(TotalLtr_ItemWise) as TotalLtr_ItemWise,sum(ItemNetAmount) as TotalAmt_ItemWise,SUM(MAmt) AS MAmt, (Credit_Customer)Credit_Customer,isnull(cast(sum(Credit_CustomerQty) as decimal(18,2)),0) Credit_CustomerQty    from ( " & BaseQry & " (CASE WHEN TSPL_ITEM_MASTER.Is_Milk_Pouch=1 and Credit_Customer='N' THEN TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount ELSE 0 END) as MAmt from TSPL_DEMAND_BOOKING_MASTER  Left outer join TSPL_DEMAND_BOOKING_DETAIL On TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No 
                Left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code left outer join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.Unit_Code=TSPL_DEMAND_BOOKING_DETAIL.Unit_code left join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code   and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_DEMAND_BOOKING_DETAIL.Unit_Code 
                left outer join tspl_company_master on 2 = 2 left join (  SELECT * FROM ( select item_code,uom_code,conversion_factor from TSPL_ITEM_UOM_DETAIL) I  PIVOT (Max(conversion_factor) FOR uom_code IN ( [KG],[LTR] )) P ) I ON TSPL_DEMAND_BOOKING_DETAIL.Item_Code = I.item_code left join ( select  Route_No,max(Cust_Code)Cust_Code from TSPL_DISTRIBUTOR_ROUTE_CUSTOMER group by Route_No) dist on dist.Route_No = TSPL_DEMAND_BOOKING_MASTER.Route_No 
                Left Join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = TSPL_DEMAND_BOOKING_master.Route_No left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =dist.Cust_Code
                " & whrcls & "  )  XXXFirst 
               where Item_Code is  not null group by  XXXFirst.Cust_Code,Credit_Customer, XXXFirst.Item_Code,	XXXFirst.Sku_Seq "

                query += " union all " & Environment.NewLine & "   SELECT max(UserName)UserName,Cust_Code,max(Customer_Name1) as Customer_Name1,max(Customer_Name4)Customer_Name4 , max(Customer_Name)Customer_Name , sum(Final_Qty)Final_Qty , Sku_Seq,max(phone2)phone2 , max(phone1)phone1 , max(Circle_No)Circle_No , max(Comp_Name)Comp_Name , max(City_Code)City_Code , max(state)State , Item_Code, max(Add1)Add1 , max(Add2)Add2 , max(Pincode)Pincode , max(fax)fax , max(date)date , max(Short_Description)Short_Description , max(Unit_Desc)Unit_Desc , sum(TotalLtr_ItemWise)TotalLtr_ItemWise , sum(TotalAmt_ItemWise)TotalAmt_ItemWise , sum(MAmt)MAmt , Credit_Customer,Sum(Credit_CustomerQty)Credit_CustomerQty FROM
                ( select '' as UserName, Cust_Code, max(yy.Customer_Name) as Customer_Name1,' IN-DPT'  as Customer_Name4,Customer_Name+ ' IN-DPT' as Customer_Name,0 as Final_Qty,Sku_Seq,'' as Phone2,'' as Phone1,'' as Circle_No,'' as Comp_Name,'' as City_Code,Item_Code,'' as State,'' as Add1,'' as Add2,'' as Pincode,'' as fax,'' as date, max(Short_Description)Short_Description,'' as Unit_Desc,0 as TotalLtr_ItemWise,0 as TotalAmt_ItemWise,0 as ProdQ,0 as MAmt,0 as Pamt,0 as [Total Amount],
                (Credit_Customer)Credit_Customer,Sum(Credit_CustomerQty)Credit_CustomerQty from ( 
				select  CASE WHEN TSPL_CUSTOMER_MASTER.Credit_Customer = 'y' THEN CASE WHEN TSPL_ITEM_MASTER.Is_Ambient = 1 THEN ROUND((ISNULL(TSPL_DEMAND_BOOKING_DETAIL.Qty, 0) * ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 1)) / I.KG, 2) ELSE CASE WHEN TSPL_ITEM_MASTER.Is_FreshItem = 1 THEN ROUND((ISNULL(TSPL_DEMAND_BOOKING_DETAIL.Qty, 0) * ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 1)) / I.LTR, 2)   ELSE 0   END END ELSE 0 END AS Credit_CustomerQty,dist.Cust_Code,
                TSPL_CUSTOMER_MASTER1.Customer_Name, TSPL_DEMAND_BOOKING_DETAIL.Item_Code ,Sku_Seq,TSPL_CUSTOMER_MASTER.Credit_Customer,TSPL_ITEM_MASTER.Short_Description from TSPL_DEMAND_BOOKING_DETAIL left outer join TSPL_DEMAND_BOOKING_master on TSPL_DEMAND_BOOKING_master.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No Left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code 
				left join ( select  TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Route_No,max(TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Cust_Code)Cust_Code from TSPL_DISTRIBUTOR_ROUTE_CUSTOMER group by TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Route_No ) dist on dist.Route_No = TSPL_DEMAND_BOOKING_MASTER.Route_No 
				left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_DEMAND_BOOKING_DETAIL.Cust_Code left outer join TSPL_CUSTOMER_MASTER AS TSPL_CUSTOMER_MASTER1 on TSPL_CUSTOMER_MASTER1.Cust_Code =dist.Cust_Code
				left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No=TSPL_DEMAND_BOOKING_master.Route_No left join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code   and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_DEMAND_BOOKING_DETAIL.Unit_Code 
                left join (  SELECT * FROM ( select item_code,uom_code,conversion_factor from TSPL_ITEM_UOM_DETAIL) I  PIVOT (Max(conversion_factor) FOR uom_code IN ( [KG],[LTR] )) P ) I ON TSPL_DEMAND_BOOKING_DETAIL.Item_Code = I.item_code " & whrcls & " ) YY 
                 group by  yy.Cust_Code,yy.Customer_Name,yy.Item_Code,Credit_Customer ,Sku_Seq "

                query += " union all " & Environment.NewLine & " select '' as UserName, Cust_Code, max(yy.Customer_Name) as Customer_Name1,' IN-DPT'  as Customer_Name4,max(Customer_Name)+ ' IN-DPT' as Customer_Name,0 as Final_Qty,Sku_Seq,'' as Phone2,'' as Phone1,'' as Circle_No,'' as Comp_Name,'' as City_Code,Item_Code,'' as State,'' as Add1,'' as Add2,'' as Pincode,'' as fax,'' as date, max(Short_Description)Short_Description,'' as Unit_Desc,0 as TotalLtr_ItemWise,0 as TotalAmt_ItemWise,0 as ProdQ,0 as MAmt,0 as Pamt,0 as [Total Amount],
                (Credit_Customer)Credit_Customer,Sum(Credit_CustomerQty)Credit_CustomerQty from ( select  0 AS Credit_CustomerQty,dist.Cust_Code,
                TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_DEMAND_BOOKING_DETAIL.Item_Code ,Sku_Seq,TSPL_CUSTOMER_MASTER.Credit_Customer,TSPL_ITEM_MASTER.Short_Description from TSPL_DEMAND_BOOKING_DETAIL left outer join TSPL_DEMAND_BOOKING_master on TSPL_DEMAND_BOOKING_master.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No Left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code 
				left join ( select  TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Route_No,max(TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Cust_Code)Cust_Code from TSPL_DISTRIBUTOR_ROUTE_CUSTOMER group by TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Route_No ) dist on dist.Route_No = TSPL_DEMAND_BOOKING_MASTER.Route_No 
				left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =dist.Cust_Code
				left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No=TSPL_DEMAND_BOOKING_master.Route_No left join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code   and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_DEMAND_BOOKING_DETAIL.Unit_Code 
                left join (  SELECT * FROM ( select item_code,uom_code,conversion_factor from TSPL_ITEM_UOM_DETAIL) I  PIVOT (Max(conversion_factor) FOR uom_code IN ( [KG],[LTR] )) P ) I ON TSPL_DEMAND_BOOKING_DETAIL.Item_Code = I.item_code " & whrcls & " ) YY group by  yy.Cust_Code,yy.Item_Code,Credit_Customer ,Sku_Seq ) XX GROUP BY xx.Cust_Code,Credit_Customer,Customer_Name, xx.Item_Code,	xx.Sku_Seq ) YYY  ) xxx
                 left join ( Select  Cust_Code,isnull(SUM(PAmt),0) AS PAmt1 from ( Select TSPL_CUSTOMER_MASTER.Cust_Code, 
                       (CASE WHEN TSPL_ITEM_MASTER.Is_Milk_Pouch=0   and Credit_Customer='N' THEN TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount ELSE 0 END) as PAmt from TSPL_DEMAND_BOOKING_MASTER  Left outer join TSPL_DEMAND_BOOKING_DETAIL On TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No 
                        Left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code
						 left join ( select  Route_No,max(Cust_Code)Cust_Code from TSPL_DISTRIBUTOR_ROUTE_CUSTOMER group by Route_No) dist on dist.Route_No = TSPL_DEMAND_BOOKING_MASTER.Route_No 
               Left Join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = TSPL_DEMAND_BOOKING_master.Route_No left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =dist.Cust_Code
                WHERE  convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date) = CONVERT(DATE, '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "', 103) and Is_Ambient = 1 ) XXXFirst 
                 group by  XXXFirst.Cust_Code  ) xx on xx.Cust_Code = xxx.Cust_Code  order by xxx.Cust_Code  "

            End If

            If rbtnRouteWise.IsChecked Then
                query = " select  case when yyy.Credit_Customer='Y'   then Credit_CustomerQty else Final_Qty end as final,case when  yyy.Credit_Customer='Y' and route_desc4='I-DPT' then Final_Qty else 0 end as InDepartment,* from  (Select   '" + objCommonVar.CurrentUser + "' as UserName,route_no,max(route_desc)route_desc1,max(route_desc)route_desc4,max(route_desc)route_desc, sum(Final_Qty)Final_Qty, Sku_Seq,max(Phone2)Phone2,max(Phone1)Phone1,max(Circle_No)Circle_No, max(Comp_Name)Comp_Name,max(City_Code)City_Code,max(State)State,Item_Code, max(Add1)Add1,max(Add2)Add2,max(Pincode)Pincode,max(Fax)Fax,max(Date)Date,max(Short_Description) AS Short_Description,max(Unit_Desc) as Unit_Desc
		,sum(TotalLtr_ItemWise) as TotalLtr_ItemWise,sum(ItemNetAmount) as TotalAmt_ItemWise,sum(ProdQ) as ProdQ ,SUM(MAmt) AS MAmt,SUM(PAmt) AS PAmt,(sum(isnull(MAmt,0))+sum(isnull(PAmt,0))) as [Total Amount], (Credit_Customer)Credit_Customer,cast(sum(Credit_CustomerQty) as decimal(18,2)) Credit_CustomerQty
        from (  
 Select Credit_Customer,

    CASE 

        WHEN Credit_Customer = 'y' THEN 
            CASE 
                WHEN TSPL_ITEM_MASTER.Is_Ambient = 1 THEN 
                    ROUND((ISNULL(TSPL_DEMAND_BOOKING_DETAIL.Qty, 0) * ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 1)) / I.KG, 2) 
                ELSE 
                    CASE 
                        WHEN TSPL_ITEM_MASTER.Is_FreshItem = 1 THEN 
                            ROUND((ISNULL(TSPL_DEMAND_BOOKING_DETAIL.Qty, 0) * ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 1)) / I.LTR, 2) 
                        ELSE 0 
                    END 
            END 
        ELSE 0 
    END AS Credit_CustomerQty,
		TSPL_DEMAND_BOOKING_MASTER.Route_No,TSPL_ROUTE_MASTER.Route_Desc,TSPL_DEMAND_BOOKING_MASTER.ShiftType,
 case when TSPL_ITEM_MASTER.Is_Ambient = 1 then round((isnull(TSPL_DEMAND_BOOKING_DETAIL.Qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I.KG,2) 
		else (case when TSPL_ITEM_MASTER.Is_FreshItem = 1 then round((isnull(TSPL_DEMAND_BOOKING_DETAIL.Qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I.[LTR],2) else 0 end ) end as Final_Qty,case when TSPL_ITEM_MASTER.Is_FreshItem = 1 then	round((isnull(TSPL_DEMAND_BOOKING_DETAIL.Qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I.KG,2) else 0 end as Kg_Qty , tspl_company_master.State,tspl_company_master.City_Code,tspl_company_master.Circle_No,tspl_company_master.Phone1,tspl_company_master.Phone2,   tspl_company_master.Comp_Name,tspl_company_master.Add1,tspl_company_master.Add2,tspl_company_master.Pincode,tspl_company_master.Fax,CONVERT(DATE, '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "', 103) as Date, TSPL_ITEM_MASTER.Sku_Seq    
	    , TSPL_DEMAND_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Short_Description as Short_Description,TSPL_UNIT_MASTER.Unit_Desc,TSPL_DEMAND_BOOKING_DETAIL.TotalLtr_ItemWise,TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount
       ,TSPL_DEMAND_BOOKING_DETAIL.TotalCrates_ItemWise ,(CASE WHEN TSPL_ITEM_MASTER.Is_Milk_Pouch=0 THEN TSPL_DEMAND_BOOKING_DETAIL.Qty ELSE 0 END) as ProdQ,(CASE WHEN TSPL_ITEM_MASTER.Is_Milk_Pouch=1 and Credit_Customer='N' THEN TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount ELSE 0 END) as MAmt
       ,(CASE WHEN TSPL_ITEM_MASTER.Is_Milk_Pouch=0 THEN TSPL_DEMAND_BOOKING_DETAIL.Qty ELSE 0 END) as PQty,(CASE WHEN TSPL_ITEM_MASTER.Is_Milk_Pouch=0   and Credit_Customer='N' THEN TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount ELSE 0 END) as PAmt
	    from TSPL_DEMAND_BOOKING_MASTER 
		Left outer join TSPL_DEMAND_BOOKING_DETAIL On TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No 
        Left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code left outer join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.Unit_Code=TSPL_DEMAND_BOOKING_DETAIL.Unit_code
       left join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code   and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_DEMAND_BOOKING_DETAIL.Unit_Code 
	   
		            left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_DEMAND_BOOKING_DETAIL.Cust_Code 
        Left Join TSPL_ROUTE_MASTER on TSPL_DEMAND_BOOKING_MASTER.Route_No = TSPL_ROUTE_MASTER.Route_No 
		 left outer join tspl_company_master on 2 = 2
        left join (  SELECT * FROM ( select item_code,uom_code,conversion_factor from TSPL_ITEM_UOM_DETAIL) I  PIVOT (Max(conversion_factor) FOR uom_code IN ( [KG],[LTR] )) P ) I ON TSPL_DEMAND_BOOKING_DETAIL.Item_Code = I.item_code 
        WHERE  convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date) = CONVERT(DATE, '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "', 103) )   XXXFirst 
   where Item_Code is  not null    group by  XXXFirst.Route_No,Credit_Customer, XXXFirst.Item_Code,	XXXFirst.Sku_Seq 

   union all

   select '' as UserName,route_no, max(yy.Route_Desc) as route_desc1,' IN-DPT'  as Route_Desc4,Route_Desc+ ' IN-DPT' as route_desc,0 as Final_Qty,Sku_Seq,'' as Phone2,'' as Phone1,'' as Circle_No,'' as Comp_Name,'' as City_Code,Item_Code,'' as State,'' as Add1,'' as Add2,'' as Pincode,'' as fax,'' as date, max(Short_Description)Short_Description,'' as Unit_Desc,0 as TotalLtr_ItemWise,0 as TotalAmt_ItemWise,0 as ProdQ,0 as MAmt,0 as Pamt,0 as [Total Amount], (Credit_Customer)Credit_Customer,Sum(Credit_CustomerQty)Credit_CustomerQty from (select CASE 
        WHEN Credit_Customer = 'y' THEN 
            CASE 
                WHEN TSPL_ITEM_MASTER.Is_Ambient = 1 THEN 
                    ROUND((ISNULL(TSPL_DEMAND_BOOKING_DETAIL.Qty, 0) * ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 1)) / I.KG, 2) 
                ELSE 
                    CASE 
                        WHEN TSPL_ITEM_MASTER.Is_FreshItem = 1 THEN 
                            ROUND((ISNULL(TSPL_DEMAND_BOOKING_DETAIL.Qty, 0) * ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 1)) / I.LTR, 2) 
                        ELSE 0 
                    END 
            END 
        ELSE 0 
    END AS Credit_CustomerQty,TSPL_DEMAND_BOOKING_MASTER.route_no,TSPL_ROUTE_MASTER.Route_Desc,TSPL_DEMAND_BOOKING_DETAIL.Item_Code,TSPL_DEMAND_BOOKING_DETAIL.Cust_Code ,Sku_Seq,Credit_Customer,TSPL_ITEM_MASTER.Short_Description from TSPL_DEMAND_BOOKING_DETAIL
	left outer join TSPL_DEMAND_BOOKING_master on TSPL_DEMAND_BOOKING_master.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No
	        Left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code 
		            left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_DEMAND_BOOKING_DETAIL.Cust_Code 
					left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No=TSPL_DEMAND_BOOKING_master.Route_No
					 left join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code   and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_DEMAND_BOOKING_DETAIL.Unit_Code
					 left join (  SELECT * FROM ( select item_code,uom_code,conversion_factor from TSPL_ITEM_UOM_DETAIL) I  PIVOT (Max(conversion_factor) FOR uom_code IN ( [KG],[LTR] )) P ) I ON TSPL_DEMAND_BOOKING_DETAIL.Item_Code = I.item_code
					 WHERE  convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date) = CONVERT(DATE, '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "', 103))yy where Item_Code is  not null     
					 group by  yy.Route_no,yy.Route_Desc,yy.Item_Code,Credit_Customer ,Sku_Seq)YYY  order by  Route_No "
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(query)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                If rbtnDistributorWise.IsChecked Then
                    frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, dtDepartmentSale, "crptDailyStatementDistributorWiseDetailcreditMilk", "Daily Statement Distributor Wise", "rptsubDailyStatementDepartSale")
                Else
                    frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "crptDailyStatementRouteAndDistributorWiseDetailCredit", "")
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to display", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Private Function ReturnQry() As String
        Dim BaseQry As String = ""
        BaseQry = "Select TSPL_DEMAND_BOOKING_MASTER.Route_No,TSPL_ROUTE_MASTER.Route_Desc,TSPL_CUSTOMER_MASTER.Credit_Customer, TSPL_DEMAND_BOOKING_MASTER.ShiftType, case when TSPL_ITEM_MASTER.Is_Ambient = 1 then round((isnull(TSPL_DEMAND_BOOKING_DETAIL.Qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I.KG,2) 
		else (case when TSPL_ITEM_MASTER.Is_FreshItem = 1 then round((isnull(TSPL_DEMAND_BOOKING_DETAIL.Qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I.[LTR],2) else 0 end ) end as Final_Qty,case when TSPL_ITEM_MASTER.Is_FreshItem = 1 then	round((isnull(TSPL_DEMAND_BOOKING_DETAIL.Qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I.KG,2) else 0 end as Kg_Qty , tspl_company_master.State,tspl_company_master.City_Code,tspl_company_master.Circle_No,tspl_company_master.Phone1,tspl_company_master.Phone2,   tspl_company_master.Comp_Name,tspl_company_master.Add1,tspl_company_master.Add2,tspl_company_master.Pincode,tspl_company_master.Fax,'" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "' as Date, TSPL_ITEM_MASTER.Sku_Seq, TSPL_CUSTOMER_MASTER.Display_Seq,dist.Cust_Code,coalesce(TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_CUSTOMER_MASTER.Customer_Name) as Customer_Name        
	    , TSPL_DEMAND_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Short_Description as Short_Description,TSPL_UNIT_MASTER.Unit_Desc,TSPL_DEMAND_BOOKING_DETAIL.TotalLtr_ItemWise,TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount
       ,TSPL_DEMAND_BOOKING_DETAIL.TotalCrates_ItemWise ,(CASE WHEN TSPL_ITEM_MASTER.Is_Milk_Pouch=0 THEN TSPL_DEMAND_BOOKING_DETAIL.Qty ELSE 0 END) as ProdQ,(CASE WHEN TSPL_ITEM_MASTER.Is_Milk_Pouch=1 THEN TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount ELSE 0 END) as MAmt
       ,(CASE WHEN TSPL_ITEM_MASTER.Is_Milk_Pouch=0 THEN TSPL_DEMAND_BOOKING_DETAIL.Qty ELSE 0 END) as PQty,(CASE WHEN TSPL_ITEM_MASTER.Is_Milk_Pouch=0 THEN TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount ELSE 0 END) as PAmt
	    from TSPL_DEMAND_BOOKING_MASTER Left outer join TSPL_DEMAND_BOOKING_DETAIL On TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No 
        Left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code left outer join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.Unit_Code=TSPL_DEMAND_BOOKING_DETAIL.Unit_code
       left join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code   and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_DEMAND_BOOKING_DETAIL.Unit_Code 
	    left join ( select  Route_No,max(Cust_Code)Cust_Code from TSPL_DISTRIBUTOR_ROUTE_CUSTOMER group by Route_No) dist on dist.Route_No = TSPL_DEMAND_BOOKING_MASTER.Route_No
        Left Join TSPL_ROUTE_MASTER on TSPL_DEMAND_BOOKING_MASTER.Route_No = TSPL_ROUTE_MASTER.Route_No 
		left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =dist.Cust_Code  left outer join tspl_company_master on 2 = 2
        left join (  SELECT * FROM ( select item_code,uom_code,conversion_factor from TSPL_ITEM_UOM_DETAIL) I  PIVOT (Max(conversion_factor) FOR uom_code IN ( [KG],[LTR] )) P ) I ON TSPL_DEMAND_BOOKING_DETAIL.Item_Code = I.item_code 
        WHERE  convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date) = CONVERT(DATE, '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "', 103)  ) XXXFirst 
   where Item_Code is  not null and Cust_Code is not null "
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

        Try
            BaseQry = ReturnQry()
            query = "  Select case when ShiftType = 'Morning' THEN 1 else 2 end As Shift_Seq, case when ShiftType = 'Morning' THEN 'Mor'  else 'Eve'  end as ShiftType , '" + objCommonVar.CurrentUser + "' as UserName, sum(Final_Qty)Final_Qty, Sku_Seq,max(Phone2)Phone2,max(Phone1)Phone1,max(Circle_No)Circle_No, max(Comp_Name)Comp_Name,max(City_Code)City_Code,max(State)State, max(Add1)Add1,max(Add2)Add2,max(Pincode)Pincode,max(Fax)Fax,max(Date)Date,max(Customer_Name) as Customer_Name,Item_Code,max(Short_Description) AS Short_Description,max(Unit_Desc) as Unit_Desc
		,sum(TotalLtr_ItemWise) as TotalLtr_ItemWise,sum(ItemNetAmount) as TotalAmt_ItemWise,sum(ProdQ) as ProdQ ,SUM(MAmt) AS MAmt,SUM(PAmt) AS PAmt,(sum(isnull(MAmt,0))+sum(isnull(PAmt,0))) as [Total Amount],sum(Kg_Qty)Kg_Qty
        from (  " & Environment.NewLine & " " & BaseQry & "  Group By  XXXFirst.ShiftType,	XXXFirst.Item_Code ,XXXFirst.Sku_Seq  order by ShiftType desc,Sku_Seq "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(query)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                If rbtnDistributorWise.IsChecked Then
                    frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "crptDailyStatementDistributorWiseSummary", "")
                Else
                    frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "crptDailyStatementDistributorWiseSummary", "")
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to display", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
End Class