
Imports common
Imports System.IO

Public Class rptCustItemWiseSaleReport
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim dtTax As DataTable = New DataTable()
    Dim isPrint As Boolean = False
#End Region
    Private Sub rptCustItemWiseSaleReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "BKN") = CompairStringResult.Equal Then
            BKNGroupBox.Visible = True
        Else
            BKNGroupBox.Visible = False
        End If
        funreset()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
    End Sub

    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        Try
            Dim qry As String = "select Cust_Code as Code,Customer_Name as Name from tspl_customer_master  "

            txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("CustFilter", qry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Try
            Dim qry As String = "select  Item_Code as [Item Code] ,Item_Desc as  [Item Desc] ,Short_Description as [Short Description] from TSPL_ITEM_MASTER where Item_Type = 'F'"
            txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("HSNItem", qry, "Item Code", "Item Desc", txtItem.arrValueMember, txtItem.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        funreset()
    End Sub
    Sub GetReportID()
        Dim VarID As String = ""
        If BtnStcRegisterPartyandItemWiseSummary.IsChecked Then
            VarID += "_STRS"
        ElseIf BtnStcRegisterItemWiseSummary.IsChecked Then
            VarID += "_SRIS"
        ElseIf BtnProductWiseSaleQuantity.IsChecked Then
            VarID += "_PWSQ"
        ElseIf BtnMilkStcSummary.IsChecked Then
            VarID += "_MSS"
        ElseIf BtnPartySaleMilkProductt.IsChecked Then
            VarID += "_PSMP"
        ElseIf BtnProductSalesSummary.IsChecked Then
            VarID += "_PSS"
        ElseIf BtnBillWiseSaleOfMilk.IsChecked Then
            VarID += "_BWSM"
        ElseIf BtnBillWiseSaleOfMilkSummary.IsChecked Then
            VarID += "_BSMS"
        ElseIf BtnTransportationCharges.IsChecked Then
            VarID += "_TRCH"
        ElseIf BtnTcsSummary.IsChecked Then
            VarID += "_TCSSM"
        ElseIf BtnGheeReport.IsChecked Then
            VarID += "_GHRP"
        ElseIf BtnRouteWiseSale.IsChecked Then
            VarID += "_RWS"
        ElseIf BtnCreditPartyWiseSaleAmount.IsChecked Then
            VarID += "_CPWSA"
        End If
        If rbtnDetail.IsChecked Then
            VarID += "_DE"
        ElseIf rbtnSummary.IsChecked Then
            VarID += "_SM"
        End If
        gv1.VarID = VarID

    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        GetReportID()
        If BtnProductWiseSaleQuantity.IsChecked Then
            ProductWiseSale(False)
        ElseIf BtnBillWiseSaleOfMilkSummary.IsChecked Then
            BillWisesaleSummary(False)
        ElseIf BtnPartySaleMilkProductt.IsChecked Then
            PartySaleMilkProductt(False)
        ElseIf BtnBillWiseSaleOfMilk.IsChecked Then
            BillwiseSaleOfMilk(False)
        ElseIf BtnProductSalesSummary.IsChecked Then
            Productsalesummarytaxablenontaxable(False)
        ElseIf BtnMilkStcSummary.IsChecked Then
            MilkStcSummary(False)
        ElseIf BtnStcRegisterItemWiseSummary.IsChecked Then
            STCRegisterItemwiseSummarytotal(False)
        ElseIf BtnStcRegisterPartyandItemWiseSummary.IsChecked Then
            STCRegisterItemwiseSummaryPartyWise(False)
        ElseIf BtnTransportationCharges.IsChecked Then
            TransportationCharges(False)
        ElseIf BtnTcsSummary.IsChecked Then
            TcsSummary(False)
        ElseIf BtnGheeReport.IsChecked Then
            GheeReport(False)
        ElseIf BtnRouteWiseSale.IsChecked Then
            RouteWiseSale(False)
        ElseIf BtnCreditPartyWiseSaleAmount.IsChecked Then
            CreditPartyWiseSaleAmount(False)
        Else
            LoadData()
        End If
    End Sub

    Sub STCRegisterItemwiseSummaryPartyWise(ByVal print As Boolean)
        Try
            Dim Qry As String = ""
            Dim BaseQry As String = ""
            Dim dt As DataTable = Nothing
            Dim whr As String = ""
            Qry = "Select To_LocationCode,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "' as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "' as ToDate,  Max(xxx.Item_Desc)Item_Desc,
                        SUM(xxx.item_Net_Amt) AS Total_Item_Net_Amt,
                        SUM(xxx.Quantity) AS Total_Quantity,
                        SUM(xxx.Report_UOM_Qty)Report_UOM_Qty
                        ,MAX(Report_UOM)Report_UOM,
                        max(xxx.UOM1) as UOM1,
                        max(xxx.UOM2) as UOM2,
                        SUM(xxx.Weight) AS Total_Weight,
                        SUM(xxx.TotalQty) AS Total_TotalQty,
                        Max(xxx.TotalQtyUOM) as TotalQtyUOM,
                        max(xxx.CompName) as CompName,
                        MAX(xxx.add1) as add1,
                        MAX(xxx.To_LocationName) as Location_Desc,
                        SUM(xxx.DOC_Total_Amt) AS Total_Doc_Total_Amt,
                        SUM(xxx.Amount) AS Total_Amount,
                        SUM(price.TotalAmount) AS Total_Taxable_Amount,
                        SUM(price.Product_value) AS Total_Product_Value,
                        SUM(price.PTax1_Amt) AS Total_PTax1_Amt,
                        SUM(price.PTax2_Amt) AS Total_PTax2_Amt,
                        SUM(price.PTax3_Amt) AS Total_PTax3_Amt,
                        SUM(price.PTax4_Amt) AS Total_PTax4_Amt,
                        SUM(price.PTax5_Amt) AS Total_PTax5_Amt,
                        SUM(price.PTax6_Amt) AS Total_PTax6_Amt,
                        SUM(price.PTax7_Amt) AS Total_PTax7_Amt,
                        SUM(price.PTax8_Amt) AS Total_PTax8_Amt,
                        SUM(price.PTax9_Amt) AS Total_PTax9_Amt,
                        SUM(price.PTax10_Amt) AS Total_PTax10_Amt from (select TSPL_TRANSFER_ORDER_HEAD.Price_Code,TSPL_TRANSFER_ORDER_DETAIL.Document_No, TSPL_TRANSFER_ORDER_HEAD.Document_Date,        TSPL_TRANSFER_ORDER_DETAIL.item_Net_Amt ,TSPL_TRANSFER_ORDER_DETAIL.item_code,TSPL_TRANSFER_ORDER_DETAIL.mrp,TSPL_TRANSFER_ORDER_DETAIL.Item_Desc , TSPL_TRANSFER_ORDER_DETAIL.Item_Cost AS Item_Cost,  TSPL_TRANSFER_ORDER_DETAIL.Out_Qty as Quantity,isnull((TSPL_TRANSFER_ORDER_DETAIL.Out_Qty * isnull((TSPL_ITEM_UOM_DETAIL.Conversion_Factor),1)) /(I.Conversion_Factor),0) As Report_UOM_Qty,I.UOM_Code as Report_UOM ,TSPL_TRANSFER_ORDER_DETAIL.Unit_code as UOM1,TSPL_ITEM_MASTER.Weight_UOM as UOM2, TSPL_ITEM_MASTER.Weight_Value as Weight,((TSPL_TRANSFER_ORDER_DETAIL.Out_Qty*tspl_item_uom_detail.Conversion_Factor)/CFinLTR.Conversion_Factor ) As TotalQty, CFinLTR.UOM_Code As TotalQtyUOM,TSPL_COMPANY_MASTER.Comp_Name AS CompName,TSPL_COMPANY_MASTER.Add1,TSPL_LOCATION_MASTER_2.Location_Code as To_LocationCode,TSPL_LOCATION_MASTER_2.Location_Desc as To_LocationName,
                        TSPL_TRANSFER_ORDER_HEAD.DOC_Total_Amt,TSPL_TRANSFER_ORDER_DETAIL.Amount,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "' as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "' as ToDate 
                        from TSPL_TRANSFER_ORDER_DETAIL 
                        join TSPL_TRANSFER_ORDER_HEAD  on TSPL_TRANSFER_ORDER_HEAD.Document_No   =TSPL_TRANSFER_ORDER_DETAIL.Document_No  
                        left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=  TSPL_TRANSFER_ORDER_HEAD.From_Location   
                        left outer join TSPL_LOCATION_MASTER AS TSPL_LOCATION_MASTER_1 on TSPL_LOCATION_MASTER_1.GIT_LOCATION =  TSPL_TRANSFER_ORDER_HEAD.To_Location  
                        left outer join  TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_2 on TSPL_LOCATION_MASTER_2.Location_Code=TSPL_TRANSFER_ORDER_HEAD.To_Location
                        INNER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.ITEM_CODE = TSPL_TRANSFER_ORDER_DETAIL.iTEM_CODE  
                        Left Outer Join (Select Item_Code,Conversion_Factor,UOM_Code from tspl_item_uom_detail where UOM_Code='LTR') As CFinLTR On CFinLTR.Item_Code=TSPL_ITEM_MASTER.Item_Code
                        Left Outer Join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_TRANSFER_ORDER_HEAD.Comp_Code 
                        LEFT OUTER JOIN TSPL_CITY_MASTER  AS TSPL_CITY_MASTER_fOR_Comp ON TSPL_CITY_MASTER_fOR_Comp.City_Code =TSPL_COMPANY_MASTER.City_Code 
                        LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_For_Comp  ON TSPL_STATE_MASTER_For_Comp.STATE_CODE  =TSPL_COMPANY_MASTER.State  
                        left outer join tspl_item_uom_detail on tspl_item_uom_detail.item_code=TSPL_TRANSFER_ORDER_DETAIL.item_code and tspl_item_uom_detail.uom_code=TSPL_TRANSFER_ORDER_DETAIL.unit_code  
                        LEFT JOIN  ( select item_code,uom_code,conversion_factor,UOM_Description from  TSPL_ITEM_UOM_DETAIL where Report_UOM = 1 ) as  I ON TSPL_TRANSFER_ORDER_DETAIL.Item_Code = I.item_code
                        left outer join tspl_item_uom_detail alt_convrsn on alt_convrsn.item_code=TSPL_TRANSFER_ORDER_DETAIL.item_code and alt_convrsn.uom_code=TSPL_TRANSFER_ORDER_DETAIL.alt_unit_code  
                        left outer join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.transport_id=TSPL_TRANSFER_ORDER_HEAD.transport_id  
                        LEFT OUTER JOIN tspl_location_master  AS tspl_location_master_For_Location ON tspl_location_master_For_Location.GIT_Location =TSPL_TRANSFER_ORDER_HEAD.To_Location  
                        left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE=TSPL_LOCATION_MASTER.State  
                        LEFT OUTER JOIN TSPL_STATE_MASTER StateMaster_ToLocation ON StateMaster_ToLocation.State_Code=TSPL_LOCATION_MASTER_1.State 
                        left join TSPL_TRANSFER_ORDER_HEAD GP on GP.document_no=TSPL_TRANSFER_ORDER_HEAD.transferoutno 
                        left join tspl_location_master as From_GP_Location on From_GP_Location.Location_Code =GP.From_Location 
                        left join TSPL_STATE_MASTER as From_GP_Location_State on From_GP_Location_State.STATE_CODE =From_GP_Location.State  
                        left outer join TSPL_LOCATION_MASTER AS To_GP_Location on To_GP_Location.GIT_Location =  GP.To_Location  
                        left join TSPL_STATE_MASTER as To_GP_Location_State on To_GP_Location_State.STATE_CODE =To_GP_Location.State  
                        left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_TRANSFER_ORDER_HEAD.tax1 
                        left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_TRANSFER_ORDER_HEAD.tax2    
                        left outer join tspl_tax_master as tax3 on tax3.Tax_Code=TSPL_TRANSFER_ORDER_HEAD .TAX3   
                        left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_TRANSFER_ORDER_HEAD .tax4   
                        left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_TRANSFER_ORDER_HEAD .tax5   
                        left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_TRANSFER_ORDER_HEAD .TAX6    
                        left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_TRANSFER_ORDER_HEAD .TAX7    
                        left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =TSPL_TRANSFER_ORDER_HEAD .TAX8  
                        left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_TRANSFER_ORDER_HEAD .TAX9    
                        left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_TRANSFER_ORDER_HEAD .TAX10    
                        left outer join TSPL_TAX_MASTER as dtax1 on dtax1.tax_code =TSPL_TRANSFER_ORDER_DETAIL .tax1 
                        left outer join tspl_tax_master as dtax2 on dtax2.tax_code = TSPL_TRANSFER_ORDER_DETAIL.tax2    
                        left outer join tspl_tax_master as dtax3 on dtax3.Tax_Code=TSPL_TRANSFER_ORDER_DETAIL .TAX3   
                        left outer join TSPL_TAX_MASTER as dtax4 on tax4.Tax_Code= TSPL_TRANSFER_ORDER_DETAIL .tax4   
                        left outer join TSPL_TAX_MASTER as dtax5 on dtax5.Tax_Code=TSPL_TRANSFER_ORDER_DETAIL .tax5   
                        left outer join TSPL_TAX_MASTER as dtax6 on dtax6.Tax_Code =TSPL_TRANSFER_ORDER_DETAIL .TAX6    
                        left outer join TSPL_TAX_MASTER as dtax7 on dtax7.Tax_Code =TSPL_TRANSFER_ORDER_DETAIL .TAX7    
                        left outer join TSPL_TAX_MASTER as dtax8 on dtax8.Tax_Code =TSPL_TRANSFER_ORDER_DETAIL .TAX8  
                        left outer join TSPL_TAX_MASTER as dtax9 on dtax9.Tax_Code =TSPL_TRANSFER_ORDER_DETAIL .TAX9    
                        left outer join TSPL_TAX_MASTER as dtax10 on dtax10.Tax_Code =TSPL_TRANSFER_ORDER_DETAIL .TAX10  
                        LEFT JOIN TSPL_TAX_GROUP_MASTER ON TSPL_TRANSFER_ORDER_HEAD.Tax_Group=TSPL_TAX_GROUP_MASTER.Tax_Group_Code and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='T' where 2=2 and TSPL_ITEM_MASTER.IsTaxable =1)xxx
                        cross apply ( select top 1 ((xxx.Quantity)*Item_Basic_Price) as TotalAmount,TSPL_ITEM_PRICE_PLAN_DETAIL.TAX1 as PTax1,TSPL_ITEM_PRICE_PLAN_DETAIL.TAX1_Rate as PTax1_Rate,(TSPL_ITEM_PRICE_PLAN_DETAIL.TAX1_Amt*xxx.Quantity) as PTax1_Amt,TSPL_ITEM_PRICE_PLAN_DETAIL.TAX2 as PTax2,TSPL_ITEM_PRICE_PLAN_DETAIL.TAX2_Rate as PTax2_Rate,(TSPL_ITEM_PRICE_PLAN_DETAIL.TAX2_Amt*xxx.Quantity) as PTax2_Amt,TSPL_ITEM_PRICE_PLAN_DETAIL.TAX3 as PTax3,TSPL_ITEM_PRICE_PLAN_DETAIL.TAX3_Rate as PTax3_Rate,(TSPL_ITEM_PRICE_PLAN_DETAIL.TAX3_Amt * xxx.Quantity) as PTax3_Amt,TSPL_ITEM_PRICE_PLAN_DETAIL.TAX4 as PTax4,TSPL_ITEM_PRICE_PLAN_DETAIL.TAX4_Rate as PTax4_Rate,(TSPL_ITEM_PRICE_PLAN_DETAIL.TAX4_Amt * xxx.Quantity) as PTax4_Amt,TSPL_ITEM_PRICE_PLAN_DETAIL.TAX5 as PTax5,TSPL_ITEM_PRICE_PLAN_DETAIL.TAX5_Rate as PTax5_Rate,(TSPL_ITEM_PRICE_PLAN_DETAIL.TAX5_Amt * xxx.Quantity) as PTax5_Amt,TSPL_ITEM_PRICE_PLAN_DETAIL.TAX6 as PTax6,TSPL_ITEM_PRICE_PLAN_DETAIL.TAX6_Rate as PTax6_Rate,(TSPL_ITEM_PRICE_PLAN_DETAIL.TAX6_Amt * xxx.Quantity) as PTax6_Amt,TSPL_ITEM_PRICE_PLAN_DETAIL.TAX7 as PTax7,TSPL_ITEM_PRICE_PLAN_DETAIL.TAX7_Rate as PTax7_Rate,(TSPL_ITEM_PRICE_PLAN_DETAIL.TAX7_Amt * xxx.Quantity) as PTax7_Amt,TSPL_ITEM_PRICE_PLAN_DETAIL.TAX8 as PTax8,TSPL_ITEM_PRICE_PLAN_DETAIL.TAX8_Rate as PTax8_Rate,(TSPL_ITEM_PRICE_PLAN_DETAIL.TAX8_Amt * xxx.Quantity) as PTax8_Amt,TSPL_ITEM_PRICE_PLAN_DETAIL.TAX9 as PTax9,TSPL_ITEM_PRICE_PLAN_DETAIL.TAX9_Rate as PTax9_Rate,(TSPL_ITEM_PRICE_PLAN_DETAIL.TAX9_Amt * xxx.Quantity) as PTax9_Amt,TSPL_ITEM_PRICE_PLAN_DETAIL.TAX10 as PTax10,TSPL_ITEM_PRICE_PLAN_DETAIL.TAX10_Rate as PTax10_Rate,(TSPL_ITEM_PRICE_PLAN_DETAIL.TAX10_Amt * xxx.Quantity) as PTax10_Amt,TSPL_ITEM_PRICE_PLAN_DETAIL.Item_MRP,TSPL_ITEM_PRICE_PLAN_DETAIL.Item_Basic_Price,TSPL_ITEM_PRICE_PLAN_DETAIL.Item_Selling_Price,((xxx.Quantity)*Item_Selling_Price) as Product_value
						,TSPL_ITEM_PRICE_PLAN_DETAIL.Price_Code
						from TSPL_ITEM_PRICE_PLAN_DETAIL
                  left outer join TSPL_ITEM_PRICE_PLAN_HEADER on TSPL_ITEM_PRICE_PLAN_HEADER.Plan_Code = TSPL_ITEM_PRICE_PLAN_DETAIL.Plan_Code 
                  where xxx.Price_Code = TSPL_ITEM_PRICE_PLAN_DETAIL.Price_Code and xxx.Item_Code = TSPL_ITEM_PRICE_PLAN_DETAIL.Item_Code 
				  and TSPL_ITEM_PRICE_PLAN_HEADER.Start_Date <= xxx.Document_Date 
                  ORDER BY TSPL_ITEM_PRICE_PLAN_HEADER.Start_Date DESC 
                  ) as price  where
                  convert(date,Document_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and convert(date,Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                  group by price.Price_Code, Item_Code,To_LocationCode"
            dt = clsDBFuncationality.GetDataTable(Qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If print = False Then
                    gv1.DataSource = Nothing
                    gv1.Rows.Clear()
                    gv1.Columns.Clear()
                    gv1.GroupDescriptors.Clear()
                    gv1.MasterView.Refresh()
                    gv1.GroupDescriptors.Clear()
                    gv1.EnableFiltering = True
                    gv1.MasterTemplate.SummaryRowsBottom.Clear()
                    gv1.DataSource = dt
                    gv1.BestFitColumns()
                    SetGridFormationn()
                    'ReStoreGridLayout()
                    gv1.MasterTemplate.AutoExpandGroups = True
                    'EnableDisableControls(False)
                    RadPageView1.SelectedPage = RadPageViewPage2
                    gv1.BestFitColumns()
                ElseIf print = True Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(CrystalReportFolder.SalesReport, dt, "CrptSTSRegister-ItemWiseSummaryPartyWisee", "STS Register-Item Wise Summary Party Wise")
                    frmCRV = Nothing

                End If
            Else
                clsCommon.MyMessageBoxShow("No data found")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub STCRegisterItemwiseSummarytotal(ByVal print As Boolean)
        Try
            Dim Qry As String = ""
            Dim BaseQry As String = ""
            Dim dt As DataTable = Nothing
            Dim whr As String = ""
            '       Qry = " SELECT '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "' as ToDate,
            '            MAX(Item_Desc) AS Item_Desc,Item_Code, MAX(Unit_code) AS Unit_code,SUM(Out_Qty) AS out_qty,MAX(UoM) as UoM,SUM(QtyAccToReportUOM) AS QtyAccToReportUOM,MAX(UOM_Code) AS UOM_Code,Sum(Amount) as Amount,
            '           sum([KKF Amt]) as KKF_Amt,SUM(Amount) + SUM([Mandi Tax Amt]) + SUM([KKF Amt]) AS Taxable_Value,SUM([Mandi Tax Amt]) as Mandi_Tax_Amt,SUM([CGST Amt]) as CGST_Amt,sum([SGST Amt]) as SGST_Amt,SUM([IGST Amt]) as IGST_Amt,SUM([TCS Amt]) as TCS_Amt,SUM(Amount) + SUM([Mandi Tax Amt]) + SUM([KKF Amt])+SUM([CGST Amt])+sum([SGST Amt])+SUM([IGST Amt]) as [Total Amount],
            '           MAX(Comp_Name) AS Comp_Name,MAX(Add1) AS Add1,MAX(Add2) AS Add2,MAX(Add3) AS Add3,MAX(City_Code) AS City_Code,MAX(State) AS State, MAX(Document_Date) AS Document_Date 
            '             FROM (SELECT ItemConvinUOMKG.UOM_Code AS kg,ItemConvinUOMLTR.UOM_Code AS ltr,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Item_Code,TSPL_TRANSFER_ORDER_detail.Unit_code,TSPL_TRANSFER_ORDER_detail.Out_Qty,TSPL_TRANSFER_ORDER_detail.Amount,
            '               Case WHEN TSPL_ITEM_MASTER.Is_FreshItem = 1 
            '               THEN ItemConvinUOMLTR.UOM_Code  WHEN TSPL_ITEM_MASTER.Is_Ambient = 1 THEN ItemConvinUOMKG.UOM_Code end as UoM,
            '               CAST(CASE WHEN TSPL_ITEM_MASTER.Is_FreshItem = 1 
            '              THEN TSPL_TRANSFER_ORDER_detail.Out_Qty * ItemConvReportUOM.Conversion_Factor / ItemConvinUOMLTR.Conversion_Factor  
            '              WHEN TSPL_ITEM_MASTER.Is_Ambient = 1 
            '              THEN TSPL_TRANSFER_ORDER_detail.Out_Qty * ItemConvReportUOM.Conversion_Factor / ItemConvinUOMKG.Conversion_Factor  
            '              ELSE 0 END 
            '              AS DECIMAL(18,2)) AS QtyAccToReportUOM,ItemConvReportUOM.UOM_Code,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1, 
            '              TSPL_COMPANY_MASTER.Add2,TSPL_COMPANY_MASTER.Add3,TSPL_COMPANY_MASTER.City_Code,TSPL_COMPANY_MASTER.State,Document_Date,

            '              CASE WHEN TSPL_TRANSFER_ORDER_head.TAX1='KKF'  THEN TSPL_TRANSFER_ORDER_DETAIL.TAX1_Amt
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX2='KKF'  THEN TSPL_TRANSFER_ORDER_DETAIL.TAX2_Amt
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX3='KKF'  THEN TSPL_TRANSFER_ORDER_DETAIL.TAX3_Amt
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX4='KKF'  THEN TSPL_TRANSFER_ORDER_DETAIL.TAX4_Amt
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX5='KKF'  THEN TSPL_TRANSFER_ORDER_DETAIL.TAX5_Amt
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX6='KKF'  THEN TSPL_TRANSFER_ORDER_DETAIL.TAX6_Amt
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX7='KKF'  THEN TSPL_TRANSFER_ORDER_DETAIL.TAX7_Amt
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX8='KKF'  THEN TSPL_TRANSFER_ORDER_DETAIL.TAX8_Amt
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX9='KKF'  THEN TSPL_TRANSFER_ORDER_DETAIL.TAX9_Amt
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX10='KKF' THEN TSPL_TRANSFER_ORDER_DETAIL.TAX10_Amt else 0 END 
            '			AS [KKF Amt],
            '                      CASE WHEN TSPL_TRANSFER_ORDER_head.TAX1='MANDITAX'  THEN TSPL_TRANSFER_ORDER_DETAIL.TAX1_Amt
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX2='MANDITAX'  THEN TSPL_TRANSFER_ORDER_DETAIL.TAX2_Amt
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX3='MANDITAX'  THEN TSPL_TRANSFER_ORDER_DETAIL.TAX3_Amt
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX4='MANDITAX'  THEN TSPL_TRANSFER_ORDER_DETAIL.TAX4_Amt
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX5='MANDITAX'  THEN TSPL_TRANSFER_ORDER_DETAIL.TAX5_Amt
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX6='MANDITAX'  THEN TSPL_TRANSFER_ORDER_DETAIL.TAX6_Amt
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX7='MANDITAX'  THEN TSPL_TRANSFER_ORDER_DETAIL.TAX7_Amt
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX8='MANDITAX'  THEN TSPL_TRANSFER_ORDER_DETAIL.TAX8_Amt
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX9='MANDITAX'  THEN TSPL_TRANSFER_ORDER_DETAIL.TAX9_Amt
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX10='MANDITAX' THEN TSPL_TRANSFER_ORDER_DETAIL.TAX10_Amt else 0 END 
            '			AS  [Mandi Tax Amt],
            '               CASE WHEN TSPL_TRANSFER_ORDER_head.TAX1='CGST'  THEN TSPL_TRANSFER_ORDER_DETAIL.TAX1_Amt
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX2='CGST'  THEN TSPL_TRANSFER_ORDER_DETAIL.TAX2_Amt
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX3='CGST'  THEN TSPL_TRANSFER_ORDER_DETAIL.TAX3_Amt
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX4='CGST'  THEN TSPL_TRANSFER_ORDER_DETAIL.TAX4_Amt
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX5='CGST'  THEN TSPL_TRANSFER_ORDER_DETAIL.TAX5_Amt
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX6='CGST'  THEN TSPL_TRANSFER_ORDER_DETAIL.TAX6_Amt
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX7='CGST'  THEN TSPL_TRANSFER_ORDER_DETAIL.TAX7_Amt
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX8='CGST'  THEN TSPL_TRANSFER_ORDER_DETAIL.TAX8_Amt
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX9='CGST'  THEN TSPL_TRANSFER_ORDER_DETAIL.TAX9_Amt
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX10='CGST' THEN TSPL_TRANSFER_ORDER_DETAIL.TAX10_Amt else 0 END  AS [CGST Amt],

            '                        CASE WHEN TSPL_TRANSFER_ORDER_head.TAX1='SGST'  THEN TSPL_TRANSFER_ORDER_DETAIL.TAX1_Amt
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX2='SGST'  THEN TSPL_TRANSFER_ORDER_DETAIL.TAX2_Amt
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX3='SGST'  THEN TSPL_TRANSFER_ORDER_DETAIL.TAX3_Amt
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX4='SGST'  THEN TSPL_TRANSFER_ORDER_DETAIL.TAX4_Amt
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX5='SGST'  THEN TSPL_TRANSFER_ORDER_DETAIL.TAX5_Amt
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX6='SGST'  THEN TSPL_TRANSFER_ORDER_DETAIL.TAX6_Amt
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX7='SGST'  THEN TSPL_TRANSFER_ORDER_DETAIL.TAX7_Amt
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX8='SGST'  THEN TSPL_TRANSFER_ORDER_DETAIL.TAX8_Amt
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX9='SGST'  THEN TSPL_TRANSFER_ORDER_DETAIL.TAX9_Amt
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX10='SGST' THEN TSPL_TRANSFER_ORDER_DETAIL.TAX10_Amt else 0 END  AS [SGST Amt]
            ',CASE WHEN TSPL_TRANSFER_ORDER_head.TAX1='IGST'  THEN isnull (TSPL_TRANSFER_ORDER_DETAIL.TAX1_Amt,0)
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX2='IGST'  THEN isnull (TSPL_TRANSFER_ORDER_DETAIL.TAX2_Amt,0)
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX3='IGST'  THEN isnull (TSPL_TRANSFER_ORDER_DETAIL.TAX3_Amt,0)
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX4='IGST'  THEN isnull(TSPL_TRANSFER_ORDER_DETAIL.TAX4_Amt,0)
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX5='IGST'  THEN isnull(TSPL_TRANSFER_ORDER_DETAIL.TAX5_Amt,0)
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX6='IGST'  THEN isnull(TSPL_TRANSFER_ORDER_DETAIL.TAX6_Amt,0)
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX7='IGST'  THEN isnull(TSPL_TRANSFER_ORDER_DETAIL.TAX7_Amt,0)
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX8='IGST'  THEN isnull(TSPL_TRANSFER_ORDER_DETAIL.TAX8_Amt,0)
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX9='IGST'  THEN isnull(TSPL_TRANSFER_ORDER_DETAIL.TAX9_Amt,0)
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX10='IGST' THEN isnull(TSPL_TRANSFER_ORDER_DETAIL.TAX10_Amt,0) else 0 END AS [IGST Amt],
            '		 CASE WHEN TSPL_TRANSFER_ORDER_head.TAX1='TCS'  THEN TSPL_TRANSFER_ORDER_DETAIL.TAX1_Amt
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX2='TCS'  THEN TSPL_TRANSFER_ORDER_DETAIL.TAX2_Amt
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX3='TCS'  THEN TSPL_TRANSFER_ORDER_DETAIL.TAX3_Amt
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX4='TCS'  THEN TSPL_TRANSFER_ORDER_DETAIL.TAX4_Amt
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX5='TCS'  THEN TSPL_TRANSFER_ORDER_DETAIL.TAX5_Amt
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX6='TCS'  THEN TSPL_TRANSFER_ORDER_DETAIL.TAX6_Amt
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX7='TCS'  THEN TSPL_TRANSFER_ORDER_DETAIL.TAX7_Amt
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX8='TCS'  THEN TSPL_TRANSFER_ORDER_DETAIL.TAX8_Amt
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX9='TCS'  THEN TSPL_TRANSFER_ORDER_DETAIL.TAX9_Amt
            '			WHEN TSPL_TRANSFER_ORDER_head.TAX10='TCS' THEN TSPL_TRANSFER_ORDER_DETAIL.TAX10_Amt else 0 END  AS [TCS Amt]FROM TSPL_TRANSFER_ORDER_detail
            '              LEFT OUTER JOIN TSPL_TRANSFER_ORDER_HEAD ON TSPL_TRANSFER_ORDER_HEAD.Document_No = TSPL_TRANSFER_ORDER_DETAIL.Document_No
            '              LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.item_code = TSPL_TRANSFER_ORDER_DETAIL.item_code
            '              LEFT JOIN TSPL_ITEM_UOM_DETAIL AS ItemConvReportUOM ON TSPL_ITEM_MASTER.Item_Code = ItemConvReportUOM.Item_Code
            '              LEFT JOIN TSPL_ITEM_UOM_DETAIL AS ItemConvinUOMLTR ON TSPL_TRANSFER_ORDER_detail.Item_Code = ItemConvinUOMLTR.Item_Code 
            '              AND ItemConvinUOMLTR.UOM_Code = 'LTR'
            '              LEFT JOIN TSPL_ITEM_UOM_DETAIL AS ItemConvinUOMKG ON TSPL_TRANSFER_ORDER_detail.Item_Code = ItemConvinUOMKG.Item_Code 
            '              AND ItemConvinUOMKG.UOM_Code = 'KG'
            '              LEFT JOIN TSPL_COMPANY_MASTER ON 2 = 2 where convert(date,Document_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and convert(date,Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value) + "') xx 
            '                               GROUP BY Item_Code"
            Qry = "Select max(To_LocationCode) as To_LocationCode,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "' as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "' as ToDate,  Max(xxx.Item_Desc)Item_Desc,
                        SUM(xxx.item_Net_Amt) AS Total_Item_Net_Amt,
                        SUM(xxx.Quantity) AS Total_Quantity,
                        SUM(xxx.Report_UOM_Qty)Report_UOM_Qty
                        ,MAX(Report_UOM)Report_UOM,
                        max(xxx.UOM1) as UOM1,
                        max(xxx.UOM2) as UOM2,
                        SUM(xxx.Weight) AS Total_Weight,
                        SUM(xxx.TotalQty) AS Total_TotalQty,
                        Max(xxx.TotalQtyUOM) as TotalQtyUOM,
                        max(xxx.CompName) as CompName,
                        MAX(xxx.add1) as add1,
                        MAX(xxx.To_LocationName) as Location_Desc,
                        SUM(xxx.DOC_Total_Amt) AS Total_Doc_Total_Amt,
                        SUM(xxx.Amount) AS Total_Amount,
                        SUM(price.TotalAmount) AS Total_Taxable_Amount,
                        SUM(price.Product_value) AS Total_Product_Value,
                        SUM(price.PTax1_Amt) AS Total_PTax1_Amt,
                        SUM(price.PTax2_Amt) AS Total_PTax2_Amt,
                        SUM(price.PTax3_Amt) AS Total_PTax3_Amt,
                        SUM(price.PTax4_Amt) AS Total_PTax4_Amt,
                        SUM(price.PTax5_Amt) AS Total_PTax5_Amt,
                        SUM(price.PTax6_Amt) AS Total_PTax6_Amt,
                        SUM(price.PTax7_Amt) AS Total_PTax7_Amt,
                        SUM(price.PTax8_Amt) AS Total_PTax8_Amt,
                        SUM(price.PTax9_Amt) AS Total_PTax9_Amt,
                        SUM(price.PTax10_Amt) AS Total_PTax10_Amt from (select TSPL_TRANSFER_ORDER_HEAD.Price_Code,TSPL_TRANSFER_ORDER_DETAIL.Document_No, TSPL_TRANSFER_ORDER_HEAD.Document_Date,        TSPL_TRANSFER_ORDER_DETAIL.item_Net_Amt ,TSPL_TRANSFER_ORDER_DETAIL.item_code,TSPL_TRANSFER_ORDER_DETAIL.mrp,TSPL_TRANSFER_ORDER_DETAIL.Item_Desc , TSPL_TRANSFER_ORDER_DETAIL.Item_Cost AS Item_Cost,  TSPL_TRANSFER_ORDER_DETAIL.Out_Qty as Quantity,isnull((TSPL_TRANSFER_ORDER_DETAIL.Out_Qty * isnull((TSPL_ITEM_UOM_DETAIL.Conversion_Factor),1)) /(I.Conversion_Factor),0) As Report_UOM_Qty,I.UOM_Code as Report_UOM ,TSPL_TRANSFER_ORDER_DETAIL.Unit_code as UOM1,TSPL_ITEM_MASTER.Weight_UOM as UOM2, TSPL_ITEM_MASTER.Weight_Value as Weight,((TSPL_TRANSFER_ORDER_DETAIL.Out_Qty*tspl_item_uom_detail.Conversion_Factor)/CFinLTR.Conversion_Factor ) As TotalQty, CFinLTR.UOM_Code As TotalQtyUOM,TSPL_COMPANY_MASTER.Comp_Name AS CompName,TSPL_COMPANY_MASTER.Add1,TSPL_LOCATION_MASTER_2.Location_Code as To_LocationCode,TSPL_LOCATION_MASTER_2.Location_Desc as To_LocationName,
                        TSPL_TRANSFER_ORDER_HEAD.DOC_Total_Amt,TSPL_TRANSFER_ORDER_DETAIL.Amount,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "' as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "' as ToDate 
                        from TSPL_TRANSFER_ORDER_DETAIL 
                        join TSPL_TRANSFER_ORDER_HEAD  on TSPL_TRANSFER_ORDER_HEAD.Document_No   =TSPL_TRANSFER_ORDER_DETAIL.Document_No  
                        left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=  TSPL_TRANSFER_ORDER_HEAD.From_Location   
                        left outer join TSPL_LOCATION_MASTER AS TSPL_LOCATION_MASTER_1 on TSPL_LOCATION_MASTER_1.GIT_LOCATION =  TSPL_TRANSFER_ORDER_HEAD.To_Location  
                        left outer join  TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_2 on TSPL_LOCATION_MASTER_2.Location_Code=TSPL_TRANSFER_ORDER_HEAD.To_Location
                        INNER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.ITEM_CODE = TSPL_TRANSFER_ORDER_DETAIL.iTEM_CODE  
                        Left Outer Join (Select Item_Code,Conversion_Factor,UOM_Code from tspl_item_uom_detail where UOM_Code='LTR') As CFinLTR On CFinLTR.Item_Code=TSPL_ITEM_MASTER.Item_Code
                        Left Outer Join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_TRANSFER_ORDER_HEAD.Comp_Code 
                        LEFT OUTER JOIN TSPL_CITY_MASTER  AS TSPL_CITY_MASTER_fOR_Comp ON TSPL_CITY_MASTER_fOR_Comp.City_Code =TSPL_COMPANY_MASTER.City_Code 
                        LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_For_Comp  ON TSPL_STATE_MASTER_For_Comp.STATE_CODE  =TSPL_COMPANY_MASTER.State  
                        left outer join tspl_item_uom_detail on tspl_item_uom_detail.item_code=TSPL_TRANSFER_ORDER_DETAIL.item_code and tspl_item_uom_detail.uom_code=TSPL_TRANSFER_ORDER_DETAIL.unit_code  
                        LEFT JOIN  ( select item_code,uom_code,conversion_factor,UOM_Description from  TSPL_ITEM_UOM_DETAIL where Report_UOM = 1 ) as  I ON TSPL_TRANSFER_ORDER_DETAIL.Item_Code = I.item_code
                        left outer join tspl_item_uom_detail alt_convrsn on alt_convrsn.item_code=TSPL_TRANSFER_ORDER_DETAIL.item_code and alt_convrsn.uom_code=TSPL_TRANSFER_ORDER_DETAIL.alt_unit_code  
                        left outer join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.transport_id=TSPL_TRANSFER_ORDER_HEAD.transport_id  
                        LEFT OUTER JOIN tspl_location_master  AS tspl_location_master_For_Location ON tspl_location_master_For_Location.GIT_Location =TSPL_TRANSFER_ORDER_HEAD.To_Location  
                        left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE=TSPL_LOCATION_MASTER.State  
                        LEFT OUTER JOIN TSPL_STATE_MASTER StateMaster_ToLocation ON StateMaster_ToLocation.State_Code=TSPL_LOCATION_MASTER_1.State 
                        left join TSPL_TRANSFER_ORDER_HEAD GP on GP.document_no=TSPL_TRANSFER_ORDER_HEAD.transferoutno 
                        left join tspl_location_master as From_GP_Location on From_GP_Location.Location_Code =GP.From_Location 
                        left join TSPL_STATE_MASTER as From_GP_Location_State on From_GP_Location_State.STATE_CODE =From_GP_Location.State  
                        left outer join TSPL_LOCATION_MASTER AS To_GP_Location on To_GP_Location.GIT_Location =  GP.To_Location  
                        left join TSPL_STATE_MASTER as To_GP_Location_State on To_GP_Location_State.STATE_CODE =To_GP_Location.State  
                        left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_TRANSFER_ORDER_HEAD.tax1 
                        left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_TRANSFER_ORDER_HEAD.tax2    
                        left outer join tspl_tax_master as tax3 on tax3.Tax_Code=TSPL_TRANSFER_ORDER_HEAD .TAX3   
                        left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_TRANSFER_ORDER_HEAD .tax4   
                        left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_TRANSFER_ORDER_HEAD .tax5   
                        left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_TRANSFER_ORDER_HEAD .TAX6    
                        left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_TRANSFER_ORDER_HEAD .TAX7    
                        left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =TSPL_TRANSFER_ORDER_HEAD .TAX8  
                        left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_TRANSFER_ORDER_HEAD .TAX9    
                        left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_TRANSFER_ORDER_HEAD .TAX10    
                        left outer join TSPL_TAX_MASTER as dtax1 on dtax1.tax_code =TSPL_TRANSFER_ORDER_DETAIL .tax1 
                        left outer join tspl_tax_master as dtax2 on dtax2.tax_code = TSPL_TRANSFER_ORDER_DETAIL.tax2    
                        left outer join tspl_tax_master as dtax3 on dtax3.Tax_Code=TSPL_TRANSFER_ORDER_DETAIL .TAX3   
                        left outer join TSPL_TAX_MASTER as dtax4 on tax4.Tax_Code= TSPL_TRANSFER_ORDER_DETAIL .tax4   
                        left outer join TSPL_TAX_MASTER as dtax5 on dtax5.Tax_Code=TSPL_TRANSFER_ORDER_DETAIL .tax5   
                        left outer join TSPL_TAX_MASTER as dtax6 on dtax6.Tax_Code =TSPL_TRANSFER_ORDER_DETAIL .TAX6    
                        left outer join TSPL_TAX_MASTER as dtax7 on dtax7.Tax_Code =TSPL_TRANSFER_ORDER_DETAIL .TAX7    
                        left outer join TSPL_TAX_MASTER as dtax8 on dtax8.Tax_Code =TSPL_TRANSFER_ORDER_DETAIL .TAX8  
                        left outer join TSPL_TAX_MASTER as dtax9 on dtax9.Tax_Code =TSPL_TRANSFER_ORDER_DETAIL .TAX9    
                        left outer join TSPL_TAX_MASTER as dtax10 on dtax10.Tax_Code =TSPL_TRANSFER_ORDER_DETAIL .TAX10  
                        LEFT JOIN TSPL_TAX_GROUP_MASTER ON TSPL_TRANSFER_ORDER_HEAD.Tax_Group=TSPL_TAX_GROUP_MASTER.Tax_Group_Code and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='T' where 2=2 and TSPL_ITEM_MASTER.IsTaxable =1)xxx
                        cross apply ( select top 1 ((xxx.Quantity)*Item_Basic_Price) as TotalAmount,TSPL_ITEM_PRICE_PLAN_DETAIL.TAX1 as PTax1,TSPL_ITEM_PRICE_PLAN_DETAIL.TAX1_Rate as PTax1_Rate,(TSPL_ITEM_PRICE_PLAN_DETAIL.TAX1_Amt*xxx.Quantity) as PTax1_Amt,TSPL_ITEM_PRICE_PLAN_DETAIL.TAX2 as PTax2,TSPL_ITEM_PRICE_PLAN_DETAIL.TAX2_Rate as PTax2_Rate,(TSPL_ITEM_PRICE_PLAN_DETAIL.TAX2_Amt*xxx.Quantity) as PTax2_Amt,TSPL_ITEM_PRICE_PLAN_DETAIL.TAX3 as PTax3,TSPL_ITEM_PRICE_PLAN_DETAIL.TAX3_Rate as PTax3_Rate,(TSPL_ITEM_PRICE_PLAN_DETAIL.TAX3_Amt * xxx.Quantity) as PTax3_Amt,TSPL_ITEM_PRICE_PLAN_DETAIL.TAX4 as PTax4,TSPL_ITEM_PRICE_PLAN_DETAIL.TAX4_Rate as PTax4_Rate,(TSPL_ITEM_PRICE_PLAN_DETAIL.TAX4_Amt * xxx.Quantity) as PTax4_Amt,TSPL_ITEM_PRICE_PLAN_DETAIL.TAX5 as PTax5,TSPL_ITEM_PRICE_PLAN_DETAIL.TAX5_Rate as PTax5_Rate,(TSPL_ITEM_PRICE_PLAN_DETAIL.TAX5_Amt * xxx.Quantity) as PTax5_Amt,TSPL_ITEM_PRICE_PLAN_DETAIL.TAX6 as PTax6,TSPL_ITEM_PRICE_PLAN_DETAIL.TAX6_Rate as PTax6_Rate,(TSPL_ITEM_PRICE_PLAN_DETAIL.TAX6_Amt * xxx.Quantity) as PTax6_Amt,TSPL_ITEM_PRICE_PLAN_DETAIL.TAX7 as PTax7,TSPL_ITEM_PRICE_PLAN_DETAIL.TAX7_Rate as PTax7_Rate,(TSPL_ITEM_PRICE_PLAN_DETAIL.TAX7_Amt * xxx.Quantity) as PTax7_Amt,TSPL_ITEM_PRICE_PLAN_DETAIL.TAX8 as PTax8,TSPL_ITEM_PRICE_PLAN_DETAIL.TAX8_Rate as PTax8_Rate,(TSPL_ITEM_PRICE_PLAN_DETAIL.TAX8_Amt * xxx.Quantity) as PTax8_Amt,TSPL_ITEM_PRICE_PLAN_DETAIL.TAX9 as PTax9,TSPL_ITEM_PRICE_PLAN_DETAIL.TAX9_Rate as PTax9_Rate,(TSPL_ITEM_PRICE_PLAN_DETAIL.TAX9_Amt * xxx.Quantity) as PTax9_Amt,TSPL_ITEM_PRICE_PLAN_DETAIL.TAX10 as PTax10,TSPL_ITEM_PRICE_PLAN_DETAIL.TAX10_Rate as PTax10_Rate,(TSPL_ITEM_PRICE_PLAN_DETAIL.TAX10_Amt * xxx.Quantity) as PTax10_Amt,TSPL_ITEM_PRICE_PLAN_DETAIL.Item_MRP,TSPL_ITEM_PRICE_PLAN_DETAIL.Item_Basic_Price,TSPL_ITEM_PRICE_PLAN_DETAIL.Item_Selling_Price,((xxx.Quantity)*Item_Selling_Price) as Product_value
						,TSPL_ITEM_PRICE_PLAN_DETAIL.Price_Code
						from TSPL_ITEM_PRICE_PLAN_DETAIL
                  left outer join TSPL_ITEM_PRICE_PLAN_HEADER on TSPL_ITEM_PRICE_PLAN_HEADER.Plan_Code = TSPL_ITEM_PRICE_PLAN_DETAIL.Plan_Code 
                  where xxx.Price_Code = TSPL_ITEM_PRICE_PLAN_DETAIL.Price_Code and xxx.Item_Code = TSPL_ITEM_PRICE_PLAN_DETAIL.Item_Code 
				  and TSPL_ITEM_PRICE_PLAN_HEADER.Start_Date <= xxx.Document_Date 
                  ORDER BY TSPL_ITEM_PRICE_PLAN_HEADER.Start_Date DESC 
                  ) as price  where
                  convert(date,Document_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and convert(date,Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                  group by Item_Code"
            dt = clsDBFuncationality.GetDataTable(Qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If print = False Then
                    gv1.DataSource = Nothing
                    gv1.Rows.Clear()
                    gv1.Columns.Clear()
                    gv1.GroupDescriptors.Clear()
                    gv1.MasterView.Refresh()
                    gv1.GroupDescriptors.Clear()
                    gv1.EnableFiltering = True
                    gv1.MasterTemplate.SummaryRowsBottom.Clear()
                    gv1.DataSource = dt
                    gv1.BestFitColumns()
                    SetGridFormationn()
                    'ReStoreGridLayout()
                    gv1.MasterTemplate.AutoExpandGroups = True
                    'EnableDisableControls(False)
                    RadPageView1.SelectedPage = RadPageViewPage2
                    gv1.BestFitColumns()
                ElseIf print = True Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(CrystalReportFolder.SalesReport, dt, "CrptSTSRegister-ItemWiseSummary", "STS Register Item Wise Summary")
                    frmCRV = Nothing

                End If
            Else
                clsCommon.MyMessageBoxShow("No data found")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub MilkStcSummary(ByVal print As Boolean)
        Try
            Dim Qry As String = ""
            Dim BaseQry As String = ""
            Dim dt As DataTable = Nothing
            Dim whr As String = ""
            'Qry = " SELECT '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "' as ToDate,
            '                       MAX(Item_Desc) AS Item_Desc,Item_Code, MAX(Unit_code) AS Unit_code,SUM(Out_Qty) AS out_qty,MAX(UoM) as UoM,SUM(QtyAccToReportUOM) AS QtyAccToReportUOM,MAX(UOM_Code) AS UOM_Code,Sum(Amount) as Amount,MAX(Comp_Name) AS Comp_Name,MAX(Add1) AS Add1,MAX(Add2) AS Add2,MAX(Add3) AS Add3,MAX(City_Code) AS City_Code,MAX(State) AS State, MAX(Document_Date) AS Document_Date 
            '                      FROM (SELECT ItemConvinUOMKG.UOM_Code AS kg,ItemConvinUOMLTR.UOM_Code AS ltr,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Item_Code,TSPL_TRANSFER_ORDER_detail.Unit_code,TSPL_TRANSFER_ORDER_detail.Out_Qty,TSPL_TRANSFER_ORDER_detail.Amount,
            '                        Case WHEN TSPL_ITEM_MASTER.Is_FreshItem = 1 
            '                        THEN ItemConvinUOMLTR.UOM_Code  WHEN TSPL_ITEM_MASTER.Is_Ambient = 1 THEN ItemConvinUOMKG.UOM_Code end as UoM,
            '                        CAST(CASE WHEN TSPL_ITEM_MASTER.Is_FreshItem = 1 
            '                       THEN TSPL_TRANSFER_ORDER_detail.Out_Qty * ItemConvReportUOM.Conversion_Factor / ItemConvinUOMLTR.Conversion_Factor  
            '                       WHEN TSPL_ITEM_MASTER.Is_Ambient = 1 
            '                       THEN TSPL_TRANSFER_ORDER_detail.Out_Qty * ItemConvReportUOM.Conversion_Factor / ItemConvinUOMKG.Conversion_Factor  
            '                       ELSE 0 END 
            '                       AS DECIMAL(18,2)) AS QtyAccToReportUOM,ItemConvReportUOM.UOM_Code,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1, 
            '                       TSPL_COMPANY_MASTER.Add2,TSPL_COMPANY_MASTER.Add3,TSPL_COMPANY_MASTER.City_Code,TSPL_COMPANY_MASTER.State,Document_Date 
            '                       FROM TSPL_TRANSFER_ORDER_detail
            '                       LEFT OUTER JOIN TSPL_TRANSFER_ORDER_HEAD ON TSPL_TRANSFER_ORDER_HEAD.Document_No = TSPL_TRANSFER_ORDER_DETAIL.Document_No
            '                       LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.item_code = TSPL_TRANSFER_ORDER_DETAIL.item_code
            '                       LEFT JOIN TSPL_ITEM_UOM_DETAIL AS ItemConvReportUOM ON TSPL_ITEM_MASTER.Item_Code = ItemConvReportUOM.Item_Code
            '                       LEFT JOIN TSPL_ITEM_UOM_DETAIL AS ItemConvinUOMLTR ON TSPL_TRANSFER_ORDER_detail.Item_Code = ItemConvinUOMLTR.Item_Code 
            '                       AND ItemConvinUOMLTR.UOM_Code = 'LTR'
            '                       LEFT JOIN TSPL_ITEM_UOM_DETAIL AS ItemConvinUOMKG ON TSPL_TRANSFER_ORDER_detail.Item_Code = ItemConvinUOMKG.Item_Code 
            '                       AND ItemConvinUOMKG.UOM_Code = 'KG'
            '                       LEFT JOIN TSPL_COMPANY_MASTER ON 2 = 2 where convert(date,Document_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and convert(date,Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value) + "') xx 
            '                        GROUP BY Item_Code"

            Qry = "SELECT 
    Item_Code, 
    MAX(Item_Desc) AS Item_Desc, 
    SUM(Out_Qty) AS Out_Qty, 
    SUM(QtyPouch) AS QtyPouch, 
    SUM(Amount) AS Amount, 
    '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AS Fromdate, 
    '" + clsCommon.GetPrintDate(txtToDate.Value) + "' AS ToDate, 
    MAX(Add1) AS Add1, 
    MAX(Add2) AS Add2, 
    MAX(Add3) AS Add3, 
    MAX(City_Code) AS City_Code, 
    MAX(State) AS State
FROM (
    SELECT 
        xx.*, 
        (xx.Out_Qty * tab2.Rate) AS Amount, 
        tab2.Rate
    FROM (
        SELECT 
            TSPL_TRANSFER_ORDER_DETAIL.Item_Code, 
            MAX(TSPL_TRANSFER_ORDER_HEAD.Price_Code) AS Price_Code, 
            TSPL_TRANSFER_ORDER_HEAD.Document_Date, 
            '' AS Fromdate, 
            '' AS ToDate, 
            MAX(TSPL_COMPANY_MASTER.Comp_Name) AS Comp_Name, 
            MAX(TSPL_COMPANY_MASTER.Add1) AS Add1, 
            MAX(TSPL_COMPANY_MASTER.Add2) AS Add2, 
            MAX(TSPL_COMPANY_MASTER.Add3) AS Add3, 
            MAX(TSPL_LOCATION_MASTER.City_Code) AS City_Code, 
            MAX(TSPL_LOCATION_MASTER.State) AS State, 
            MAX(TSPL_TRANSFER_ORDER_DETAIL.Item_Desc) AS Item_Desc, 
            SUM(TSPL_TRANSFER_ORDER_DETAIL.Out_Qty) AS Out_Qty, 
            MAX(TSPL_TRANSFER_ORDER_DETAIL.Unit_code) AS UoM, 
            MAX(TSPL_TRANSFER_ORDER_DETAIL.Out_Qty * 
                ItemConvReportUOM.Conversion_Factor / 
                ItemConvinUOMpouch.Conversion_Factor) AS QtyPouch, 
            TSPL_TRANSFER_ORDER_DETAIL.Unit_code
        FROM TSPL_TRANSFER_ORDER_DETAIL 
        LEFT OUTER JOIN TSPL_TRANSFER_ORDER_HEAD 
        ON TSPL_TRANSFER_ORDER_HEAD.Document_No = TSPL_TRANSFER_ORDER_DETAIL.Document_No
        LEFT OUTER JOIN TSPL_ITEM_MASTER 
        ON TSPL_ITEM_MASTER.Item_Code = TSPL_TRANSFER_ORDER_DETAIL.Item_Code
        LEFT JOIN TSPL_ITEM_UOM_DETAIL AS ItemConvReportUOM
        ON TSPL_TRANSFER_ORDER_DETAIL.Item_Code = ItemConvReportUOM.Item_Code
        AND TSPL_TRANSFER_ORDER_DETAIL.Unit_code = ItemConvReportUOM.UOM_Code
        LEFT JOIN TSPL_ITEM_UOM_DETAIL AS ItemConvinUOMpouch
        ON TSPL_TRANSFER_ORDER_DETAIL.Item_Code = ItemConvinUOMpouch.Item_Code
        AND ItemConvinUOMpouch.UOM_Code = 'Pouch'
        LEFT JOIN TSPL_COMPANY_MASTER ON 2 = 2
        LEFT OUTER JOIN TSPL_LOCATION_MASTER 
        ON TSPL_LOCATION_MASTER.Location_Code = TSPL_TRANSFER_ORDER_DETAIL.Location 
        WHERE TSPL_ITEM_MASTER.Is_FreshItem = 1  and convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value) + "' GROUP BY TSPL_TRANSFER_ORDER_HEAD.Document_Date, TSPL_TRANSFER_ORDER_DETAIL.Item_Code, TSPL_TRANSFER_ORDER_DETAIL.Unit_code
    ) xx
    OUTER APPLY ( 
        SELECT TOP 1 TSPL_ITEM_PRICE_PLAN_DETAIL.Item_Basic_Price AS Rate
        FROM TSPL_ITEM_PRICE_PLAN_DETAIL
        LEFT JOIN TSPL_ITEM_PRICE_PLAN_HEADER 
        ON TSPL_ITEM_PRICE_PLAN_DETAIL.Plan_Code = TSPL_ITEM_PRICE_PLAN_HEADER.Plan_Code
        WHERE TSPL_ITEM_PRICE_PLAN_DETAIL.Price_Code = xx.Price_Code  
        AND TSPL_ITEM_PRICE_PLAN_DETAIL.Item_Code = xx.Item_Code 
        AND TSPL_ITEM_PRICE_PLAN_HEADER.Start_Date <= xx.Document_Date 
        ORDER BY TSPL_ITEM_PRICE_PLAN_HEADER.Start_Date DESC 
    ) AS tab2
) xxx 
GROUP BY Item_Code"
            dt = clsDBFuncationality.GetDataTable(Qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If print = False Then
                    gv1.DataSource = Nothing
                    gv1.Rows.Clear()
                    gv1.Columns.Clear()
                    gv1.GroupDescriptors.Clear()
                    gv1.MasterView.Refresh()
                    gv1.GroupDescriptors.Clear()
                    gv1.EnableFiltering = True
                    gv1.MasterTemplate.SummaryRowsBottom.Clear()
                    gv1.DataSource = dt
                    gv1.BestFitColumns()
                    SetGridFormationn()
                    'ReStoreGridLayout()
                    gv1.MasterTemplate.AutoExpandGroups = True
                    'EnableDisableControls(False)
                    RadPageView1.SelectedPage = RadPageViewPage2
                    gv1.BestFitColumns()
                ElseIf print = True Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(CrystalReportFolder.SalesReport, dt, "CrptMilkStcSummary", "Milk Stc Summary")
                    frmCRV = Nothing

                End If
            Else
                clsCommon.MyMessageBoxShow("No data found")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub Productsalesummarytaxablenontaxable(ByVal print As Boolean)
        Try
            Dim Qry As String = ""
            Dim BaseQry As String = ""
            Dim dt As DataTable = Nothing
            Dim whr As String = ""
            Qry = " SELECT '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "' as ToDate,
                                   Sum(Distributor_Commission_TotalAmt) as Trp_Charge,MAX(Customer_Name) as Customer_Name,
                               MAX(Item_Desc) AS Item_Desc,Item_Code, MAX(Unit_code) AS Unit_code,SUM(Qty) AS Qty,sum(Amount) as Amount,sum([KKF Amt]) as KKF_Amt,SUM(Amount) + SUM([Mandi Tax Amt]) + SUM([KKF Amt]) AS Taxable_Value,SUM([Mandi Tax Amt]) as Mandi_Tax_Amt,SUM([CGST Amt]) as CGST_Amt,sum([SGST Amt]) as SGST_Amt,SUM([IGST Amt]) as IGST_Amt,SUM([TCS Amt]) as TCS_Amt,SUM(Amount) + SUM([Mandi Tax Amt]) + SUM([KKF Amt])+SUM([CGST Amt])+sum([SGST Amt])+SUM([IGST Amt]) as [Total Amount],
	                            SUM(QtyAccToReportUOM) AS QtyAccToReportUOM,MAX(UOM_Code) AS UOM_Code,MAX(Comp_Name) AS Comp_Name,MAX(Add1) AS Add1,MAX(Add2) AS Add2,MAX(Add3) AS Add3,MAX(City_Code) AS City_Code,MAX(State) AS State, MAX(Document_Date) AS Document_Date
                                FROM (SELECT TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Item_Code, 
                            TSPL_SD_SHIPMENT_DETAIL.Unit_code,TSPL_SD_SHIPMENT_DETAIL.Qty,
                            cast((TSPL_SD_SHIPMENT_DETAIL.Qty * ItemConvinUOM.Conversion_Factor / ItemConvReportUOM.Conversion_Factor) as Decimal(18, 2)) as QtyAccToReportUOM,ItemConvReportUOM.UOM_Code,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER.Add2, 
                            TSPL_COMPANY_MASTER.Add3,TSPL_COMPANY_MASTER.City_Code,TSPL_COMPANY_MASTER.State,TSPL_SD_SHIPMENT_DETAIL.Amount,TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No,TSPL_SD_SHIPMENT_HEAD.TAX5_Amt,TSPL_SD_SHIPMENT_HEAD.Distributor_Commission_TotalAmt,TSPL_CUSTOMER_MASTER.Customer_Name,
							CASE WHEN TSPL_SD_SHIPMENT_HEAD.TAX1='KKF'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX1_Amt
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX2='KKF'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX2_Amt
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX3='KKF'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX3_Amt
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX4='KKF'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX4_Amt
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX5='KKF'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX5_Amt
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX6='KKF'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX6_Amt
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX7='KKF'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX7_Amt
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX8='KKF'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX8_Amt
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX9='KKF'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX9_Amt
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX10='KKF' THEN TSPL_SD_SHIPMENT_DETAIL.TAX10_Amt else 0 END 
    				AS [KKF Amt],
                           CASE WHEN TSPL_SD_SHIPMENT_HEAD.TAX1='MANDITAX'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX1_Amt
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX2='MANDITAX'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX2_Amt
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX3='MANDITAX'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX3_Amt
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX4='MANDITAX'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX4_Amt
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX5='MANDITAX'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX5_Amt
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX6='MANDITAX'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX6_Amt
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX7='MANDITAX'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX7_Amt
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX8='MANDITAX'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX8_Amt
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX9='MANDITAX'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX9_Amt
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX10='MANDITAX' THEN TSPL_SD_SHIPMENT_DETAIL.TAX10_Amt else 0 END 
    				AS  [Mandi Tax Amt],
                    CASE WHEN TSPL_SD_SHIPMENT_HEAD.TAX1='CGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX1_Amt
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX2='CGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX2_Amt
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX3='CGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX3_Amt
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX4='CGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX4_Amt
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX5='CGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX5_Amt
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX6='CGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX6_Amt
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX7='CGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX7_Amt
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX8='CGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX8_Amt
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX9='CGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX9_Amt
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX10='CGST' THEN TSPL_SD_SHIPMENT_DETAIL.TAX10_Amt else 0 END  AS [CGST Amt],

                             CASE WHEN TSPL_SD_SHIPMENT_HEAD.TAX1='SGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX1_Amt
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX2='SGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX2_Amt
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX3='SGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX3_Amt
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX4='SGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX4_Amt
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX5='SGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX5_Amt
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX6='SGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX6_Amt
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX7='SGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX7_Amt
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX8='SGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX8_Amt
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX9='SGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX9_Amt
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX10='SGST' THEN TSPL_SD_SHIPMENT_DETAIL.TAX10_Amt else 0 END  AS [SGST Amt]
					,CASE WHEN TSPL_SD_SHIPMENT_HEAD.TAX1='IGST'  THEN isnull (TSPL_SD_SHIPMENT_DETAIL.TAX1_Amt,0)
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX2='IGST'  THEN isnull (TSPL_SD_SHIPMENT_DETAIL.TAX2_Amt,0)
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX3='IGST'  THEN isnull (TSPL_SD_SHIPMENT_DETAIL.TAX3_Amt,0)
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX4='IGST'  THEN isnull(TSPL_SD_SHIPMENT_DETAIL.TAX4_Amt,0)
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX5='IGST'  THEN isnull(TSPL_SD_SHIPMENT_DETAIL.TAX5_Amt,0)
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX6='IGST'  THEN isnull(TSPL_SD_SHIPMENT_DETAIL.TAX6_Amt,0)
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX7='IGST'  THEN isnull(TSPL_SD_SHIPMENT_DETAIL.TAX7_Amt,0)
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX8='IGST'  THEN isnull(TSPL_SD_SHIPMENT_DETAIL.TAX8_Amt,0)
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX9='IGST'  THEN isnull(TSPL_SD_SHIPMENT_DETAIL.TAX9_Amt,0)
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX10='IGST' THEN isnull(TSPL_SD_SHIPMENT_DETAIL.TAX10_Amt,0) else 0 END AS [IGST Amt],
							 CASE WHEN TSPL_SD_SHIPMENT_HEAD.TAX1='TCS'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX1_Amt
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX2='TCS'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX2_Amt
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX3='TCS'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX3_Amt
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX4='TCS'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX4_Amt
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX5='TCS'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX5_Amt
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX6='TCS'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX6_Amt
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX7='TCS'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX7_Amt
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX8='TCS'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX8_Amt
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX9='TCS'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX9_Amt
    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX10='TCS' THEN TSPL_SD_SHIPMENT_DETAIL.TAX10_Amt else 0 END  AS [TCS Amt],
                    Document_Date 
                    FROM TSPL_SD_SHIPMENT_DETAIL
                    LEFT OUTER JOIN TSPL_SD_SHIPMENT_HEAD ON TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE
					left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code
                    LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.item_code = TSPL_SD_SHIPMENT_DETAIL.item_code
                    left join TSPL_ITEM_UOM_DETAIL as ItemConvReportUOM on TSPL_ITEM_master.Item_Code = ItemConvReportUOM.Item_Code and ItemConvReportUOM.Report_UOM = 1
                    left join TSPL_ITEM_UOM_DETAIL as ItemConvinUOM on TSPL_SD_SHIPMENT_DETAIL.Item_Code = ItemConvinUOM.Item_Code and TSPL_SD_SHIPMENT_DETAIL.Unit_code = ItemConvinUOM.UOM_Code
                        LEFT JOIN TSPL_COMPANY_MASTER ON 2 = 2 where TSPL_ITEM_MASTER.Is_Ambient = 1  and convert(date,Document_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and convert(date,Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value) + "') xx 
                GROUP BY Item_Code"
            dt = clsDBFuncationality.GetDataTable(Qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If print = False Then
                    gv1.DataSource = Nothing
                    gv1.Rows.Clear()
                    gv1.Columns.Clear()
                    gv1.GroupDescriptors.Clear()
                    gv1.MasterView.Refresh()
                    gv1.GroupDescriptors.Clear()
                    gv1.EnableFiltering = True
                    gv1.MasterTemplate.SummaryRowsBottom.Clear()
                    gv1.DataSource = dt
                    gv1.BestFitColumns()
                    SetGridFormationn()
                    'ReStoreGridLayout()
                    gv1.MasterTemplate.AutoExpandGroups = True
                    'EnableDisableControls(False)
                    RadPageView1.SelectedPage = RadPageViewPage2
                    gv1.BestFitColumns()
                ElseIf print = True Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(CrystalReportFolder.SalesReport, dt, "CrptProductSaleSummaryTaxableNonTaxable", "Product Sale Summary Taxable NonTaxable")
                    frmCRV = Nothing

                End If
            Else
                clsCommon.MyMessageBoxShow("No data found")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Sub BillwiseSaleOfMilk(ByVal print As Boolean)
        Try
            Dim Qry As String = ""
            Dim BaseQry As String = ""
            Dim dt As DataTable = Nothing
            Dim whr As String = ""
            Qry = " SELECT '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "' as ToDate,
                   
	                    TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE,
    MAX(TSPL_ITEM_MASTER.Item_Desc) AS Item_Desc, 
    MAX(TSPL_ITEM_MASTER.Item_Code) AS Item_Code, 
    MAX(TSPL_SD_SHIPMENT_DETAIL.Unit_code) AS Unit_code, 
    SUM(TSPL_SD_SHIPMENT_DETAIL.Qty) AS Qty,
    SUM(
        TSPL_SD_SHIPMENT_DETAIL.Qty * ItemConvinUOM.Conversion_Factor / ItemConvReportUOM.Conversion_Factor
    ) AS QtyAccToReportUOM,
    SUM(Amount - ISNULL(Distributor_Commission_Amt, 0) + 
        CASE 
            WHEN TSPL_SD_SHIPMENT_HEAD.TAX1 = 'TCS' THEN TSPL_SD_SHIPMENT_DETAIL.TAX1_Amt
            WHEN TSPL_SD_SHIPMENT_HEAD.TAX2 = 'TCS' THEN TSPL_SD_SHIPMENT_DETAIL.TAX2_Amt
            WHEN TSPL_SD_SHIPMENT_HEAD.TAX3 = 'TCS' THEN TSPL_SD_SHIPMENT_DETAIL.TAX3_Amt
            WHEN TSPL_SD_SHIPMENT_HEAD.TAX4 = 'TCS' THEN TSPL_SD_SHIPMENT_DETAIL.TAX4_Amt
            WHEN TSPL_SD_SHIPMENT_HEAD.TAX5 = 'TCS' THEN TSPL_SD_SHIPMENT_DETAIL.TAX5_Amt
            WHEN TSPL_SD_SHIPMENT_HEAD.TAX6 = 'TCS' THEN TSPL_SD_SHIPMENT_DETAIL.TAX6_Amt
            WHEN TSPL_SD_SHIPMENT_HEAD.TAX7 = 'TCS' THEN TSPL_SD_SHIPMENT_DETAIL.TAX7_Amt
            WHEN TSPL_SD_SHIPMENT_HEAD.TAX8 = 'TCS' THEN TSPL_SD_SHIPMENT_DETAIL.TAX8_Amt
            WHEN TSPL_SD_SHIPMENT_HEAD.TAX9 = 'TCS' THEN TSPL_SD_SHIPMENT_DETAIL.TAX9_Amt
            WHEN TSPL_SD_SHIPMENT_HEAD.TAX10 = 'TCS' THEN TSPL_SD_SHIPMENT_DETAIL.TAX10_Amt 
            ELSE 0 
        END
    ) AS Total_Amount,
    MAX(ItemConvReportUOM.UOM_Code) AS UOM_Code, 
    MAX(TSPL_COMPANY_MASTER.Comp_Name) AS Comp_Name, 
    MAX(TSPL_COMPANY_MASTER.Add1) AS Add1, 
    MAX(TSPL_COMPANY_MASTER.Add2) AS Add2, 
    MAX(TSPL_COMPANY_MASTER.Add3) AS Add3, 
    MAX(TSPL_COMPANY_MASTER.City_Code) AS City_Code, 
    MAX(TSPL_COMPANY_MASTER.State) AS State,
    SUM(TSPL_SD_SHIPMENT_DETAIL.Amount) AS Amount,
    MAX(TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No) AS Sale_Invoice_No,
    SUM(TSPL_SD_SHIPMENT_HEAD.TAX5_Amt) AS TAX5_Amt,
    SUM(TSPL_SD_SHIPMENT_DETAIL.Distributor_Commission_Amt) AS Distributor_Commission_Amt,
    MAX(TSPL_CUSTOMER_MASTER.Customer_Name) AS Customer_Name,
    SUM(
        CASE 
            WHEN TSPL_SD_SHIPMENT_HEAD.TAX1 = 'TCS' THEN TSPL_SD_SHIPMENT_DETAIL.TAX1_Amt
            WHEN TSPL_SD_SHIPMENT_HEAD.TAX2 = 'TCS' THEN TSPL_SD_SHIPMENT_DETAIL.TAX2_Amt
            WHEN TSPL_SD_SHIPMENT_HEAD.TAX3 = 'TCS' THEN TSPL_SD_SHIPMENT_DETAIL.TAX3_Amt
            WHEN TSPL_SD_SHIPMENT_HEAD.TAX4 = 'TCS' THEN TSPL_SD_SHIPMENT_DETAIL.TAX4_Amt
            WHEN TSPL_SD_SHIPMENT_HEAD.TAX5 = 'TCS' THEN TSPL_SD_SHIPMENT_DETAIL.TAX5_Amt
            WHEN TSPL_SD_SHIPMENT_HEAD.TAX6 = 'TCS' THEN TSPL_SD_SHIPMENT_DETAIL.TAX6_Amt
            WHEN TSPL_SD_SHIPMENT_HEAD.TAX7 = 'TCS' THEN TSPL_SD_SHIPMENT_DETAIL.TAX7_Amt
            WHEN TSPL_SD_SHIPMENT_HEAD.TAX8 = 'TCS' THEN TSPL_SD_SHIPMENT_DETAIL.TAX8_Amt
            WHEN TSPL_SD_SHIPMENT_HEAD.TAX9 = 'TCS' THEN TSPL_SD_SHIPMENT_DETAIL.TAX9_Amt
            WHEN TSPL_SD_SHIPMENT_HEAD.TAX10 = 'TCS' THEN TSPL_SD_SHIPMENT_DETAIL.TAX10_Amt 
            ELSE 0 
        END
    ) AS [TCS Amt],
    MAX(Document_Date) AS Document_Date
FROM TSPL_SD_SHIPMENT_DETAIL
LEFT OUTER JOIN TSPL_SD_SHIPMENT_HEAD 
    ON TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE
LEFT OUTER JOIN TSPL_CUSTOMER_MASTER 
    ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code
LEFT OUTER JOIN TSPL_ITEM_MASTER 
    ON TSPL_ITEM_MASTER.item_code = TSPL_SD_SHIPMENT_DETAIL.item_code
LEFT JOIN TSPL_ITEM_UOM_DETAIL AS ItemConvReportUOM 
    ON TSPL_ITEM_MASTER.Item_Code = ItemConvReportUOM.Item_Code AND ItemConvReportUOM.Report_UOM = 1
LEFT JOIN TSPL_ITEM_UOM_DETAIL AS ItemConvinUOM 
    ON TSPL_SD_SHIPMENT_DETAIL.Item_Code = ItemConvinUOM.Item_Code AND TSPL_SD_SHIPMENT_DETAIL.Unit_code = ItemConvinUOM.UOM_Code
LEFT JOIN TSPL_COMPANY_MASTER 
    ON 2 = 2 where convert(date,Document_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and convert(date,Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value) + "' and  TSPL_ITEM_MASTER.IsTaxable=0 GROUP BY TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE "
            dt = clsDBFuncationality.GetDataTable(Qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If print = False Then
                    gv1.DataSource = Nothing
                    gv1.Rows.Clear()
                    gv1.Columns.Clear()
                    gv1.GroupDescriptors.Clear()
                    gv1.MasterView.Refresh()
                    gv1.GroupDescriptors.Clear()
                    gv1.EnableFiltering = True
                    gv1.MasterTemplate.SummaryRowsBottom.Clear()
                    gv1.DataSource = dt
                    gv1.BestFitColumns()
                    SetGridFormationn()
                    'ReStoreGridLayout()
                    gv1.MasterTemplate.AutoExpandGroups = True
                    'EnableDisableControls(False)
                    RadPageView1.SelectedPage = RadPageViewPage2
                    gv1.BestFitColumns()
                ElseIf print = True Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(CrystalReportFolder.SalesReport, dt, "CrptBillWiseSaleOfMilk", "Bill Wise Sale Of Milk")
                    frmCRV = Nothing

                End If
            Else
                clsCommon.MyMessageBoxShow("No data found")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub PartySaleMilkProductt(ByVal print As Boolean)
        Try
            Dim Qry As String = ""
            Dim BaseQry As String = ""
            Dim dt As DataTable = Nothing
            Dim whr As String = ""
            '            Qry = " SELECT '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "' as ToDate,
            '    MAX(Customer_Name) AS Party,
            '    MAX(GSTNO) AS GSTIN,
            '	sum(CASE WHEN IsTaxable = 1 THEN Amount ELSE 0 END) AS Taxable_Value,
            '    sum(CASE WHEN IsTaxable = 0 THEN Amount ELSE 0 END) AS Non_Taxable_Value,
            '	SUM([KKF Amt]) + Sum([Mandi Tax Amt]) + SUM([CGST Amt]) + Sum([SGST Amt]) + SUM([IGST Amt]) AS GST_Amount,
            '	sum(CASE WHEN IsTaxable = 1 THEN Amount ELSE 0 END) 
            '    + sum(CASE WHEN IsTaxable = 0 THEN Amount ELSE 0 END) 
            '    + SUM([CGST Amt]) + SUM([SGST Amt]) + SUM([IGST Amt]) AS Sale_Amount,
            '    SUM([TCS Amt]) AS TCS_Amount,
            '    sum(Distributor_Commission_Amt) AS Trp_and_Other_Charge,
            '    sum(CASE WHEN IsTaxable = 1 THEN Amount ELSE 0 END) 
            '    + sum(CASE WHEN IsTaxable = 0 THEN Amount ELSE 0 END) 
            '    + SUM([CGST Amt]) + SUM([SGST Amt]) + SUM([IGST Amt]) + Sum([TCS Amt]) + sum(Distributor_Commission_Amt) AS Bill_Amount,
            '	 MAX(Comp_Name) AS Comp_Name,
            '    MAX(Add1) AS Add1,
            '    MAX(Add2) AS Add2,
            '    MAX(Add3) AS Add3,
            '	SUM(QtyAccToReportUOM) AS QtyAccToReportUOM,
            '    MAX(UOM_Code) AS UOM_Code,
            '	SUM([CGST Amt]) as CGST,
            '	SUM([SGST Amt]) as SGST,
            '	SUM([IGST Amt]) as IGST,
            '	SUM([Mandi Tax Amt]) as mandi,
            '	SUM([KKF Amt]) as KKF
            '    FROM ( SELECT 
            '        TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE,
            '        TSPL_ITEM_MASTER.Item_Desc,
            '        TSPL_ITEM_MASTER.Item_Code,
            '        TSPL_SD_SHIPMENT_DETAIL.Unit_code,
            '        TSPL_SD_SHIPMENT_DETAIL.Qty,
            '        TSPL_ITEM_MASTER.IsTaxable,
            '       CAST((TSPL_SD_SHIPMENT_DETAIL.Qty * ItemConvinUOM.Conversion_Factor / Report_UOM.Conversion_Factor) AS DECIMAL(18, 2)
            '       ) AS QtyAccToReportUOM,
            '        Report_UOM.UOM_Code, 
            '        TSPL_COMPANY_MASTER.Comp_Name, 
            '        TSPL_COMPANY_MASTER.Add1, 
            '        TSPL_COMPANY_MASTER.Add2, 
            '        TSPL_COMPANY_MASTER.Add3, 
            '        TSPL_COMPANY_MASTER.City_Code, 
            '        TSPL_COMPANY_MASTER.State,
            '        TSPL_SD_SHIPMENT_DETAIL.Amount,
            '        TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No,
            '        TSPL_SD_SHIPMENT_HEAD.TAX5_Amt,
            '        TSPL_SD_SHIPMENT_DETAIL.Distributor_Commission_Amt,
            '        TSPL_CUSTOMER_MASTER.Customer_Name,
            '        TSPL_CUSTOMER_MASTER.GSTNO,
            '		CASE WHEN TSPL_SD_SHIPMENT_HEAD.TAX1='KKF'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX1_Amt
            '    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX2='KKF'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX2_Amt
            '    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX3='KKF'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX3_Amt
            '    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX4='KKF'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX4_Amt
            '    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX5='KKF'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX5_Amt
            '    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX6='KKF'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX6_Amt
            '    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX7='KKF'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX7_Amt
            '    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX8='KKF'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX8_Amt
            '    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX9='KKF'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX9_Amt
            '    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX10='KKF' THEN TSPL_SD_SHIPMENT_DETAIL.TAX10_Amt else 0 END 
            '    				AS [KKF Amt],
            '                           CASE WHEN TSPL_SD_SHIPMENT_HEAD.TAX1='MANDITAX'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX1_Amt
            '    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX2='MANDITAX'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX2_Amt
            '    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX3='MANDITAX'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX3_Amt
            '    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX4='MANDITAX'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX4_Amt
            '    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX5='MANDITAX'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX5_Amt
            '    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX6='MANDITAX'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX6_Amt
            '    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX7='MANDITAX'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX7_Amt
            '    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX8='MANDITAX'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX8_Amt
            '    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX9='MANDITAX'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX9_Amt
            '    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX10='MANDITAX' THEN TSPL_SD_SHIPMENT_DETAIL.TAX10_Amt else 0 END 
            '    				AS  [Mandi Tax Amt],
            '		CASE WHEN TSPL_SD_SHIPMENT_HEAD.TAX1='CGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX1_Amt
            '    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX2='CGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX2_Amt
            '    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX3='CGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX3_Amt
            '    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX4='CGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX4_Amt
            '    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX5='CGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX5_Amt
            '    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX6='CGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX6_Amt
            '    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX7='CGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX7_Amt
            '    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX8='CGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX8_Amt
            '    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX9='CGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX9_Amt
            '    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX10='CGST' THEN TSPL_SD_SHIPMENT_DETAIL.TAX10_Amt else 0 END  AS [CGST Amt],

            '        CASE WHEN TSPL_SD_SHIPMENT_HEAD.TAX1='SGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX1_Amt
            '    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX2='SGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX2_Amt
            '    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX3='SGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX3_Amt
            '    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX4='SGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX4_Amt
            '    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX5='SGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX5_Amt
            '    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX6='SGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX6_Amt
            '    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX7='SGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX7_Amt
            '    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX8='SGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX8_Amt
            '    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX9='SGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX9_Amt
            '    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX10='SGST' THEN TSPL_SD_SHIPMENT_DETAIL.TAX10_Amt else 0 END  AS [SGST Amt],

            '	CASE WHEN TSPL_SD_SHIPMENT_HEAD.TAX1='IGST'  THEN isnull (TSPL_SD_SHIPMENT_DETAIL.TAX1_Amt,0)
            '    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX2='IGST'  THEN isnull (TSPL_SD_SHIPMENT_DETAIL.TAX2_Amt,0)
            '    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX3='IGST'  THEN isnull (TSPL_SD_SHIPMENT_DETAIL.TAX3_Amt,0)
            '    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX4='IGST'  THEN isnull(TSPL_SD_SHIPMENT_DETAIL.TAX4_Amt,0)
            '    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX5='IGST'  THEN isnull(TSPL_SD_SHIPMENT_DETAIL.TAX5_Amt,0)
            '    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX6='IGST'  THEN isnull(TSPL_SD_SHIPMENT_DETAIL.TAX6_Amt,0)
            '    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX7='IGST'  THEN isnull(TSPL_SD_SHIPMENT_DETAIL.TAX7_Amt,0)
            '    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX8='IGST'  THEN isnull(TSPL_SD_SHIPMENT_DETAIL.TAX8_Amt,0)
            '    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX9='IGST'  THEN isnull(TSPL_SD_SHIPMENT_DETAIL.TAX9_Amt,0)
            '    				WHEN TSPL_SD_SHIPMENT_HEAD.TAX10='IGST' THEN isnull(TSPL_SD_SHIPMENT_DETAIL.TAX10_Amt,0) else 0 END AS [IGST Amt],

            '    CASE WHEN TSPL_SD_SHIPMENT_HEAD.TAX1 = 'TCS' THEN TSPL_SD_SHIPMENT_DETAIL.TAX1_Amt
            '            WHEN TSPL_SD_SHIPMENT_HEAD.TAX2 = 'TCS' THEN TSPL_SD_SHIPMENT_DETAIL.TAX2_Amt
            '            WHEN TSPL_SD_SHIPMENT_HEAD.TAX3 = 'TCS' THEN TSPL_SD_SHIPMENT_DETAIL.TAX3_Amt
            '            WHEN TSPL_SD_SHIPMENT_HEAD.TAX4 = 'TCS' THEN TSPL_SD_SHIPMENT_DETAIL.TAX4_Amt
            '            WHEN TSPL_SD_SHIPMENT_HEAD.TAX5 = 'TCS' THEN TSPL_SD_SHIPMENT_DETAIL.TAX5_Amt
            '            WHEN TSPL_SD_SHIPMENT_HEAD.TAX6 = 'TCS' THEN TSPL_SD_SHIPMENT_DETAIL.TAX6_Amt
            '            WHEN TSPL_SD_SHIPMENT_HEAD.TAX7 = 'TCS' THEN TSPL_SD_SHIPMENT_DETAIL.TAX7_Amt
            '            WHEN TSPL_SD_SHIPMENT_HEAD.TAX8 = 'TCS' THEN TSPL_SD_SHIPMENT_DETAIL.TAX8_Amt
            '            WHEN TSPL_SD_SHIPMENT_HEAD.TAX9 = 'TCS' THEN TSPL_SD_SHIPMENT_DETAIL.TAX9_Amt
            '            WHEN TSPL_SD_SHIPMENT_HEAD.TAX10 = 'TCS' THEN TSPL_SD_SHIPMENT_DETAIL.TAX10_Amt
            '            ELSE 0 
            '        END AS [TCS Amt],
            '        Document_Date 
            '    FROM TSPL_SD_SHIPMENT_DETAIL
            '    LEFT OUTER JOIN TSPL_SD_SHIPMENT_HEAD 
            '        ON TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE
            '    LEFT OUTER JOIN TSPL_CUSTOMER_MASTER 
            '        ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code
            '    LEFT OUTER JOIN TSPL_ITEM_MASTER 
            '        ON TSPL_ITEM_MASTER.item_code = TSPL_SD_SHIPMENT_DETAIL.item_code
            '    LEFT JOIN
            '(select Item_Code,UOM_Code,Conversion_Factor from	TSPL_ITEM_UOM_DETAIL AS ItemConvReportUOM where  Report_UOM =1) Report_UOM ON Report_UOM.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code 
            'and Report_UOM.UOM_Code = TSPL_SD_SHIPMENT_DETAIL.Unit_code
            '    LEFT JOIN TSPL_ITEM_UOM_DETAIL AS ItemConvinUOM 
            '        ON TSPL_SD_SHIPMENT_DETAIL.Item_Code = ItemConvinUOM.Item_Code 
            '        AND TSPL_SD_SHIPMENT_DETAIL.Unit_code = ItemConvinUOM.UOM_Code
            '    LEFT JOIN TSPL_COMPANY_MASTER 
            '        ON 2 = 2 
            '    WHERE convert(date,Document_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and convert(date,Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value) + "') xx 
            '                GROUP BY Customer_Name"
            '            dt = clsDBFuncationality.GetDataTable(Qry)

            '            Qry = " 
            '    select 
            'XXFinal.Customer_Name,MAX(XXFinal.FromDate) as FromDate,MAX(XXFinal.ToDate) as ToDate,
            'MAX(XXFinal.GSTNO) as GSTNO, SUM(XXFinal.Taxable_Amount) as Taxable_Amount,SUM(XXFinal.Non_Taxable_Amount)as Non_Taxable_Amount, SUM(XXFinal.GSTAmt) as GSTAmt,
            'SUM(XXFinal.Sale_Amt) as Sale_Amt,SUM(XXFinal.TCS_AMT) as TCS_AMT, SUM(XXFinal.Trp_othcharg) as [Trip & other Charge],SUM(XXFinal.Bill_Amt) as Bill_Amt,
            'MAX(XXFinal.Comp_Name) as Comp_Name,
            'MAX(XXFinal.CompAddress) as CompAddress
            'from(
            'select xx.*, (xx.Taxable_Amount +xx.Non_Taxable_Amount+xx.GSTAmt) as Sale_Amt,((xx.Taxable_Amount+xx.Non_Taxable_Amount+xx.GSTAmt+xx.TCS_AMT) -(xx.Trp_othcharg)) as Bill_Amt 
            'from(
            'select 
            'TSPL_CUSTOMER_MASTER.Customer_Name as Customer_Name,TSPL_SD_SHIPMENT_HEAD.Customer_Code,TSPL_SD_SHIPMENT_HEAD.Document_Code,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "' as FromDate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "' as ToDate,
            'TSPL_COMPANY_MASTER.Comp_Name,
            '(TSPL_COMPANY_MASTER.Add1 +TSPL_COMPANY_MASTER.Add2+TSPL_COMPANY_MASTER.Add3) as CompAddress, 
            'TSPL_CUSTOMER_MASTER.GSTNO as GSTNO ,
            ' case when TSPL_SD_SHIPMENT_HEAD.DO_Item_Type='T' then TSPL_SD_SHIPMENT_HEAD.Amount_Less_Discount else 0 end as Taxable_Amount,
            ' case when TSPL_SD_SHIPMENT_HEAD.DO_Item_Type='NT' then TSPL_SD_SHIPMENT_HEAD.Amount_Less_Discount else 0 end as Non_Taxable_Amount,
            ' Case when TSPL_SD_SHIPMENT_HEAD.TAX1 = 'IGST' then isnull(TSPL_SD_SHIPMENT_HEAD.TAX1_Amt,0) else(
            'Case when ISNULL(TSPL_SD_SHIPMENT_HEAD.tax1,'')='KKF' or ISNULL(TSPL_SD_SHIPMENT_HEAD.tax2,'')='KKF' then (case when TSPL_SD_SHIPMENT_HEAD.TAX3='IGST' then (TSPL_SD_SHIPMENT_HEAD.TAX1_Amt+TSPL_SD_SHIPMENT_HEAD.TAX2_Amt+TSPL_SD_SHIPMENT_HEAD.TAX3_Amt)else (TSPL_SD_SHIPMENT_HEAD.TAX1_Amt+TSPL_SD_SHIPMENT_HEAD.TAX2_Amt+TSPL_SD_SHIPMENT_HEAD.TAX3_Amt +TSPL_SD_SHIPMENT_HEAD.TAX4_Amt) end) else 0 end) end as GSTAmt,
            'Case when TSPL_SD_SHIPMENT_HEAD.TAX1 = 'IGST' then isnull(TSPL_SD_SHIPMENT_HEAD.TAX2_Amt,0) else(
            'Case when ISNULL(TSPL_SD_SHIPMENT_HEAD.tax1,'')='KKF' or ISNULL(TSPL_SD_SHIPMENT_HEAD.tax2,'')='KKF' then (case when TSPL_SD_SHIPMENT_HEAD.TAX3='IGST' then TSPL_SD_SHIPMENT_HEAD.TAX4_Amt else (case when    
            'TSPL_SD_SHIPMENT_HEAD.TAX5='TCS' then TSPL_SD_SHIPMENT_HEAD.TAX5_Amt else 0 end) end) else (case when TSPL_SD_SHIPMENT_HEAD.tax2='TCS' then TSPL_SD_SHIPMENT_HEAD.TAX2_Amt else ((case when TSPL_SD_SHIPMENT_HEAD.tax3='TCS' then TSPL_SD_SHIPMENT_HEAD.TAX3_Amt else 0 end)) end) end) end as TCS_AMT,

            '(isnull(TSPL_SD_SHIPMENT_HEAD.Distributor_Commission_TotalAmt,0) + isnull(TSPL_SD_SHIPMENT_HEAD.Transporter_Commission_TotalAmt,0) +isnull(TSPL_SD_SHIPMENT_HEAD.Security_TotalAmt,0) +isnull(TSPL_SD_SHIPMENT_HEAD.BoothSecurity_TotalAmt,0) ) as Trp_othcharg
            'from TSPL_SD_SHIPMENT_HEAD
            'left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code
            'left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code1='BKN'
            '    WHERE convert(date,Document_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and convert(date,Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value) + "'and TSPL_SD_SHIPMENT_HEAD.Status=1 
            ') xx
            ') XXFinal 
            'group by XXFinal.Customer_Name"

            Qry =
                 " 
                select 
            (XXFinal.Customer_Code) ,MAX(XXFinal.FromDate) as FromDate,MAX(XXFinal.ToDate) as ToDate,MAX(XXFinal.Customer_Name) as Customer_Name,SUM(XXFinal.[CGST Amt]+[SGST Amt]) as GstAMt,
            MAX(XXFinal.GSTNO) as GSTNO,SUM(XXFinal.Taxable_Amount+[KKF_Amt]+[Mandi_Tax_Amt]) as Taxable_Amount,SUM(XXFinal.Non_Taxable_Amount)as Non_Taxable_Amount,
            SUM(XXFinal.[KKF_Amt]) as KKF,SUM(XXFinal.[Mandi_Tax_Amt]) as MandiTax,
            SUM(XXFinal.Sale_Amt) as Sale_Amt,SUM(XXFinal.TCS_AMT) as TCS_AMT,SUM(XXFinal.Trp_othcharg) as [Trip & other Charge],SUM(XXFinal.Bill_Amt) as Bill_Amt,
            MAX(XXFinal.Comp_Name) as Comp_Name,
            MAX(XXFinal.CompAddress) as CompAddress
            from(
            select xx.*, (xx.Taxable_Amount +xx.Non_Taxable_Amount+xx.GSTAmt) as Sale_Amt,((xx.Taxable_Amount+xx.Non_Taxable_Amount+xx.GSTAmt+xx.TCS_AMT) -(xx.Trp_othcharg)) as Bill_Amt 
            from(
                                            select 
                                            TSPL_CUSTOMER_MASTER.Customer_Name as Customer_Name,TSPL_SD_SHIPMENT_HEAD.Customer_Code,TSPL_SD_SHIPMENT_HEAD.Document_Code,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "' as FromDate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "' as ToDate,
                                            TSPL_COMPANY_MASTER.Comp_Name,TSPL_SD_SHIPMENT_DETAIL.Item_Code,
                                            (TSPL_COMPANY_MASTER.Add1 + TSPL_COMPANY_MASTER.Add2+TSPL_COMPANY_MASTER.Add3) as CompAddress, 
                                            TSPL_CUSTOMER_MASTER.GSTNO as GSTNO,
                                             case when TSPL_SD_SHIPMENT_HEAD.DO_Item_Type='T' Then TSPL_SD_SHIPMENT_DETAIL.Amount else 0 end as Taxable_Amount,
                                             case when TSPL_SD_SHIPMENT_HEAD.DO_Item_Type='NT' Then TSPL_SD_SHIPMENT_HEAD.Amount_Less_Discount else 0 end as Non_Taxable_Amount,
                                             Case when TSPL_SD_SHIPMENT_DETAIL.TAX1 = 'IGST' Then isnull(TSPL_SD_SHIPMENT_DETAIL.TAX1_Amt,0) else(
                                            Case when ISNULL(TSPL_SD_SHIPMENT_DETAIL.tax1,'')='KKF' or ISNULL(TSPL_SD_SHIPMENT_DETAIL.tax2,'')='KKF' Then (case when TSPL_SD_SHIPMENT_DETAIL.TAX3='IGST' Then (TSPL_SD_SHIPMENT_DETAIL.TAX1_Amt+TSPL_SD_SHIPMENT_DETAIL.TAX2_Amt+TSPL_SD_SHIPMENT_DETAIL.TAX3_Amt)else (TSPL_SD_SHIPMENT_DETAIL.TAX1_Amt+TSPL_SD_SHIPMENT_DETAIL.TAX2_Amt+TSPL_SD_SHIPMENT_DETAIL.TAX3_Amt +TSPL_SD_SHIPMENT_DETAIL.TAX4_Amt) end) else 0 end) end as GSTAmt,

                                            CASE WHEN TSPL_SD_SHIPMENT_HEAD.TAX1='CGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX1_Amt
                				            WHEN TSPL_SD_SHIPMENT_HEAD.TAX2='CGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX2_Amt
                				            WHEN TSPL_SD_SHIPMENT_HEAD.TAX3='CGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX3_Amt
                				            WHEN TSPL_SD_SHIPMENT_HEAD.TAX4='CGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX4_Amt
                				            WHEN TSPL_SD_SHIPMENT_HEAD.TAX5='CGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX5_Amt
                				            WHEN TSPL_SD_SHIPMENT_HEAD.TAX6='CGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX6_Amt
                				            WHEN TSPL_SD_SHIPMENT_HEAD.TAX7='CGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX7_Amt
                				            WHEN TSPL_SD_SHIPMENT_HEAD.TAX8='CGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX8_Amt
                				            WHEN TSPL_SD_SHIPMENT_HEAD.TAX9='CGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX9_Amt
                				            WHEN TSPL_SD_SHIPMENT_HEAD.TAX10='CGST' THEN TSPL_SD_SHIPMENT_DETAIL.TAX10_Amt else 0 END  AS [CGST Amt],

                                           CASE WHEN TSPL_SD_SHIPMENT_HEAD.TAX1='SGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX1_Amt
                				            WHEN TSPL_SD_SHIPMENT_HEAD.TAX2='SGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX2_Amt
                				            WHEN TSPL_SD_SHIPMENT_HEAD.TAX3='SGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX3_Amt
                				            WHEN TSPL_SD_SHIPMENT_HEAD.TAX4='SGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX4_Amt
                				            WHEN TSPL_SD_SHIPMENT_HEAD.TAX5='SGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX5_Amt
                				            WHEN TSPL_SD_SHIPMENT_HEAD.TAX6='SGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX6_Amt
                				            WHEN TSPL_SD_SHIPMENT_HEAD.TAX7='SGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX7_Amt
                				            WHEN TSPL_SD_SHIPMENT_HEAD.TAX8='SGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX8_Amt
                				            WHEN TSPL_SD_SHIPMENT_HEAD.TAX9='SGST'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX9_Amt
               				                WHEN TSPL_SD_SHIPMENT_HEAD.TAX10='SGST' THEN TSPL_SD_SHIPMENT_DETAIL.TAX10_Amt else 0 END  AS [SGST Amt],

                                            CASE WHEN TSPL_SD_SHIPMENT_HEAD.TAX1='KKF'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX1_Amt
                            				WHEN TSPL_SD_SHIPMENT_HEAD.TAX2='KKF'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX2_Amt
                            				WHEN TSPL_SD_SHIPMENT_HEAD.TAX3='KKF'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX3_Amt
                            				WHEN TSPL_SD_SHIPMENT_HEAD.TAX4='KKF'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX4_Amt
                            				WHEN TSPL_SD_SHIPMENT_HEAD.TAX5='KKF'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX5_Amt
                            				WHEN TSPL_SD_SHIPMENT_HEAD.TAX6='KKF'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX6_Amt
                            				WHEN TSPL_SD_SHIPMENT_HEAD.TAX7='KKF'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX7_Amt
                            				WHEN TSPL_SD_SHIPMENT_HEAD.TAX8='KKF'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX8_Amt
                            				WHEN TSPL_SD_SHIPMENT_HEAD.TAX9='KKF'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX9_Amt
                            				WHEN TSPL_SD_SHIPMENT_HEAD.TAX10='KKF' THEN TSPL_SD_SHIPMENT_DETAIL.TAX10_Amt else 0 END 
                            				AS [KKF_Amt],
                                                  CASE WHEN TSPL_SD_SHIPMENT_HEAD.TAX1='MANDITAX'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX1_Amt
                            				WHEN TSPL_SD_SHIPMENT_HEAD.TAX2='MANDITAX'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX2_Amt
                            				WHEN TSPL_SD_SHIPMENT_HEAD.TAX3='MANDITAX'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX3_Amt
                            				WHEN TSPL_SD_SHIPMENT_HEAD.TAX4='MANDITAX'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX4_Amt
                            				WHEN TSPL_SD_SHIPMENT_HEAD.TAX5='MANDITAX'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX5_Amt
                            				WHEN TSPL_SD_SHIPMENT_HEAD.TAX6='MANDITAX'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX6_Amt
                            				WHEN TSPL_SD_SHIPMENT_HEAD.TAX7='MANDITAX'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX7_Amt
                            				WHEN TSPL_SD_SHIPMENT_HEAD.TAX8='MANDITAX'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX8_Amt
                            				WHEN TSPL_SD_SHIPMENT_HEAD.TAX9='MANDITAX'  THEN TSPL_SD_SHIPMENT_DETAIL.TAX9_Amt
                            				WHEN TSPL_SD_SHIPMENT_HEAD.TAX10='MANDITAX' THEN TSPL_SD_SHIPMENT_DETAIL.TAX10_Amt else 0 END 
                            				AS  [Mandi_Tax_Amt],

                                            Case when TSPL_SD_SHIPMENT_DETAIL.TAX1 = 'IGST' Then isnull(TSPL_SD_SHIPMENT_DETAIL.TAX2_Amt,0) else(
                                            Case when ISNULL(TSPL_SD_SHIPMENT_DETAIL.tax1,'')='KKF' or ISNULL(TSPL_SD_SHIPMENT_DETAIL.tax2,'')='KKF' Then (case when TSPL_SD_SHIPMENT_DETAIL.TAX3='IGST' Then TSPL_SD_SHIPMENT_DETAIL.TAX4_Amt else (case when    
                                            TSPL_SD_SHIPMENT_DETAIL.TAX5='TCS' Then TSPL_SD_SHIPMENT_DETAIL.TAX5_Amt else 0 end) end) else (case when TSPL_SD_SHIPMENT_DETAIL.tax2='TCS' Then TSPL_SD_SHIPMENT_DETAIL.TAX2_Amt else ((case when TSPL_SD_SHIPMENT_DETAIL.tax3='TCS' Then TSPL_SD_SHIPMENT_DETAIL.TAX3_Amt else 0 end)) end) end) end as TCS_AMT,

                                            (isnull(TSPL_SD_SHIPMENT_HEAD.Distributor_Commission_TotalAmt,0) + isnull(TSPL_SD_SHIPMENT_HEAD.Transporter_Commission_TotalAmt,0) +isnull(TSPL_SD_SHIPMENT_HEAD.Security_TotalAmt,0) +isnull(TSPL_SD_SHIPMENT_HEAD.BoothSecurity_TotalAmt,0)) as Trp_othcharg
                                            from TSPL_SD_SHIPMENT_HEAD
                                            left outer join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE=TSPL_SD_SHIPMENT_HEAD.Document_Code
                                            left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code
                                            left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code1='BKN'
                WHERE convert(date,Document_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and convert(date,Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value) + "'and TSPL_SD_SHIPMENT_HEAD.Status=1 
            ) xx
            ) XXFinal 
            group by XXFinal.Customer_Code"



            dt = clsDBFuncationality.GetDataTable(Qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If print = False Then
                    gv1.DataSource = Nothing
                    gv1.Rows.Clear()
                    gv1.Columns.Clear()
                    gv1.GroupDescriptors.Clear()
                    gv1.MasterView.Refresh()
                    gv1.GroupDescriptors.Clear()
                    gv1.EnableFiltering = True
                    gv1.MasterTemplate.SummaryRowsBottom.Clear()
                    gv1.DataSource = dt
                    gv1.BestFitColumns()
                    SetGridFormationn()
                    'ReStoreGridLayout()
                    gv1.MasterTemplate.AutoExpandGroups = True
                    'EnableDisableControls(False)
                    RadPageView1.SelectedPage = RadPageViewPage2
                    gv1.BestFitColumns()
                ElseIf print = True Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(CrystalReportFolder.SalesReport, dt, "CrptPartySalesMilkandProducts", "Party Sales Milk and Products")
                    frmCRV = Nothing

                End If
            Else
                clsCommon.MyMessageBoxShow("No data found")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub TransportationCharges(ByVal print As Boolean)
        Try
            Dim Qry As String = ""
            Dim whrcls As String = ""
            Dim BaseQry As String = ""
            Dim dt As DataTable = Nothing
            Dim whr As String = ""

            If txtCustomer.arrValueMember IsNot Nothing Then
                whrcls = " and TSPL_CUSTOMER_MASTER.Cust_Code in (''," & clsCommon.GetMulcallString(txtCustomer.arrValueMember) & ") "
            End If

            Qry = " SELECT  
                    max(XXFinal.Customer_Name) as Customer_Name,MAX(XXFinal.FromDate) as FromDate,MAX(XXFinal.ToDate) as ToDate,
                    sum(XXFinal.Trp_othcharg) as Trp_othcharg,SUM(XXFinal.Trp_othcharg) as Total_Amount,
                    MAX(XXFinal.Comp_Name) as Comp_Name,
                    MAX(XXFinal.CompAddress) as CompAddress
                    from(
                    select xx.*, (xx.Taxable_Amount +xx.Non_Taxable_Amount+xx.GSTAmt) as Sale_Amt,((xx.Taxable_Amount+xx.Non_Taxable_Amount+xx.GSTAmt+xx.TCS_AMT) -(xx.Trp_othcharg)) as Bill_Amt 
                    from(
                    select 
                    TSPL_CUSTOMER_MASTER.Customer_Name as Customer_Name,TSPL_SD_SHIPMENT_HEAD.Customer_Code,TSPL_SD_SHIPMENT_HEAD.Document_Code,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "' as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "' as ToDate,
                                                        TSPL_COMPANY_MASTER.Comp_Name,
                    (TSPL_COMPANY_MASTER.Add1 ) as CompAddress, 
                     case when TSPL_SD_SHIPMENT_HEAD.DO_Item_Type='T' then TSPL_SD_SHIPMENT_HEAD.Amount_Less_Discount else 0 end as Taxable_Amount,
                     case when TSPL_SD_SHIPMENT_HEAD.DO_Item_Type='NT' then TSPL_SD_SHIPMENT_HEAD.Amount_Less_Discount else 0 end as Non_Taxable_Amount,
                     Case when TSPL_SD_SHIPMENT_HEAD.TAX1 = 'IGST' then isnull(TSPL_SD_SHIPMENT_HEAD.TAX1_Amt,0) else(
                    Case when ISNULL(TSPL_SD_SHIPMENT_HEAD.tax1,'')='KKF' or ISNULL(TSPL_SD_SHIPMENT_HEAD.tax2,'')='KKF' then (case when TSPL_SD_SHIPMENT_HEAD.TAX3='IGST' then (TSPL_SD_SHIPMENT_HEAD.TAX1_Amt+TSPL_SD_SHIPMENT_HEAD.TAX2_Amt+TSPL_SD_SHIPMENT_HEAD.TAX3_Amt)else (TSPL_SD_SHIPMENT_HEAD.TAX1_Amt+TSPL_SD_SHIPMENT_HEAD.TAX2_Amt+TSPL_SD_SHIPMENT_HEAD.TAX3_Amt +TSPL_SD_SHIPMENT_HEAD.TAX4_Amt) end) else 0 end) end as GSTAmt,
                    Case when TSPL_SD_SHIPMENT_HEAD.TAX1 = 'IGST' then isnull(TSPL_SD_SHIPMENT_HEAD.TAX2_Amt,0) else(
                    Case when ISNULL(TSPL_SD_SHIPMENT_HEAD.tax1,'')='KKF' or ISNULL(TSPL_SD_SHIPMENT_HEAD.tax2,'')='KKF' then (case when TSPL_SD_SHIPMENT_HEAD.TAX3='IGST' then TSPL_SD_SHIPMENT_HEAD.TAX4_Amt else (case when    
                    TSPL_SD_SHIPMENT_HEAD.TAX5='TCS' then TSPL_SD_SHIPMENT_HEAD.TAX5_Amt else 0 end) end) else (case when TSPL_SD_SHIPMENT_HEAD.tax2='TCS' then TSPL_SD_SHIPMENT_HEAD.TAX2_Amt else ((case when TSPL_SD_SHIPMENT_HEAD.tax3='TCS' then TSPL_SD_SHIPMENT_HEAD.TAX3_Amt else 0 end)) end) end) end as TCS_AMT,

                    (isnull(TSPL_SD_SHIPMENT_HEAD.Distributor_Commission_TotalAmt,0) + isnull(TSPL_SD_SHIPMENT_HEAD.Transporter_Commission_TotalAmt,0) +isnull(TSPL_SD_SHIPMENT_HEAD.Security_TotalAmt,0) +isnull(TSPL_SD_SHIPMENT_HEAD.BoothSecurity_TotalAmt,0) ) as Trp_othcharg
                    from TSPL_SD_SHIPMENT_HEAD
                    left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code
                    left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code1='BKN'
                        WHERE convert(date,Document_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and convert(date,Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value) + "' and TSPL_SD_SHIPMENT_HEAD.Status=1 " & whrcls & "
                        ) xx
                        ) XXFinal 
                        where XXFinal.Trp_othcharg>0
                        group by XXFinal.Customer_Name"
            dt = clsDBFuncationality.GetDataTable(Qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If print = False Then
                    gv1.DataSource = Nothing
                    gv1.Rows.Clear()
                    gv1.Columns.Clear()
                    gv1.GroupDescriptors.Clear()
                    gv1.MasterView.Refresh()
                    gv1.GroupDescriptors.Clear()
                    gv1.EnableFiltering = True
                    gv1.MasterTemplate.SummaryRowsBottom.Clear()
                    gv1.DataSource = dt
                    gv1.BestFitColumns()
                    SetGridFormationn()
                    'ReStoreGridLayout()
                    gv1.MasterTemplate.AutoExpandGroups = True
                    'EnableDisableControls(False)
                    RadPageView1.SelectedPage = RadPageViewPage2
                    gv1.BestFitColumns()
                ElseIf print = True Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(CrystalReportFolder.SalesReport, dt, "CrptTransportationCharges", "Transportation Charges")
                    frmCRV = Nothing

                End If
            Else
                clsCommon.MyMessageBoxShow("No data found")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub TcsSummary(ByVal print As Boolean)
        Try
            Dim Qry As String = ""
            Dim whrcls As String = ""
            Dim BaseQry As String = ""
            Dim dt As DataTable = Nothing
            Dim whr As String = ""
            If txtCustomer.arrValueMember IsNot Nothing Then
                whrcls = " and TSPL_CUSTOMER_MASTER.Cust_Code in (''," & clsCommon.GetMulcallString(txtCustomer.arrValueMember) & ") "
            End If

            Qry = " select 
                        max(XXFinal.Customer_Name) as Customer_Name,max(XXFinal.pan) as Pan,MAX(XXFinal.FromDate) as FromDate,
                        MAX(XXFinal.ToDate) as ToDate,
                        sum(XXFinal.Total_Amt-XXFinal.TCS_AMT) as Sale_Amount,
                        SUM(XXFinal.TCS_AMT) as TCS_AMT,
                        MAX(XXFinal.Comp_Name) as Comp_Name,
                        MAX(XXFinal.CompAddress) as CompAddress
                        from(
                        select xx.*, (xx.Taxable_Amount +xx.Non_Taxable_Amount+xx.GSTAmt) as Sale_Amt,((xx.Taxable_Amount+xx.Non_Taxable_Amount+xx.GSTAmt+xx.TCS_AMT) -(xx.Trp_othcharg)) as Bill_Amt
                        from(
                        select 
                        TSPL_CUSTOMER_MASTER.Customer_Name as Customer_Name,TSPL_SD_SHIPMENT_HEAD.Customer_Code,TSPL_SD_SHIPMENT_HEAD.Document_Code,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "' as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "' as ToDate,
                         TSPL_COMPANY_MASTER.Comp_Name,

                        (TSPL_COMPANY_MASTER.Add1 ) as CompAddress,
                         case when TSPL_SD_SHIPMENT_HEAD.DO_Item_Type='T' then TSPL_SD_SHIPMENT_HEAD.Amount_Less_Discount else 0 end as Taxable_Amount,
                         case when TSPL_SD_SHIPMENT_HEAD.DO_Item_Type='NT' then TSPL_SD_SHIPMENT_HEAD.Amount_Less_Discount else 0 end as Non_Taxable_Amount,

                         Case when TSPL_SD_SHIPMENT_HEAD.TAX1 = 'IGST' then isnull(TSPL_SD_SHIPMENT_HEAD.TAX1_Amt,0) else(
                        Case when ISNULL(TSPL_SD_SHIPMENT_HEAD.tax1,'')='KKF' or ISNULL(TSPL_SD_SHIPMENT_HEAD.tax2,'')='KKF' then (case when TSPL_SD_SHIPMENT_HEAD.TAX3='IGST' then (TSPL_SD_SHIPMENT_HEAD.TAX1_Amt+TSPL_SD_SHIPMENT_HEAD.TAX2_Amt+TSPL_SD_SHIPMENT_HEAD.TAX3_Amt)else (TSPL_SD_SHIPMENT_HEAD.TAX1_Amt+TSPL_SD_SHIPMENT_HEAD.TAX2_Amt+TSPL_SD_SHIPMENT_HEAD.TAX3_Amt +TSPL_SD_SHIPMENT_HEAD.TAX4_Amt) end) else 0 end) end as GSTAmt,
                        Case when TSPL_SD_SHIPMENT_HEAD.TAX1 = 'IGST' then isnull(TSPL_SD_SHIPMENT_HEAD.TAX2_Amt,0) else(
                        Case when ISNULL(TSPL_SD_SHIPMENT_HEAD.tax1,'')='KKF' or ISNULL(TSPL_SD_SHIPMENT_HEAD.tax2,'')='KKF' then (case when TSPL_SD_SHIPMENT_HEAD.TAX3='IGST' then TSPL_SD_SHIPMENT_HEAD.TAX4_Amt else (case when    
                        TSPL_SD_SHIPMENT_HEAD.TAX5='TCS' then TSPL_SD_SHIPMENT_HEAD.TAX5_Amt else 0 end) end) else (case when TSPL_SD_SHIPMENT_HEAD.tax2='TCS' then TSPL_SD_SHIPMENT_HEAD.TAX2_Amt else ((case when TSPL_SD_SHIPMENT_HEAD.tax3='TCS' then TSPL_SD_SHIPMENT_HEAD.TAX3_Amt else 0 end)) end) end) end as TCS_AMT,TSPL_SD_SHIPMENT_HEAD.Total_Amt,
                        (isnull(TSPL_SD_SHIPMENT_HEAD.Distributor_Commission_TotalAmt,0) + isnull(TSPL_SD_SHIPMENT_HEAD.Transporter_Commission_TotalAmt,0) +isnull(TSPL_SD_SHIPMENT_HEAD.Security_TotalAmt,0) +isnull(TSPL_SD_SHIPMENT_HEAD.BoothSecurity_TotalAmt,0) ) as Trp_othcharg,TSPL_CUSTOMER_MASTER.PAN
                        from TSPL_SD_SHIPMENT_HEAD
                        left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code
                        left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code1='BKN'
                        WHERE convert(date,Document_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and convert(date,Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value) + "' and TSPL_SD_SHIPMENT_HEAD.Status=1  " & whrcls & "
                        ) xx
                        ) XXFinal 
                        where XXFinal.TCS_AMT>0 group by XXFinal.Customer_Name"
            dt = clsDBFuncationality.GetDataTable(Qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If print = False Then
                    gv1.DataSource = Nothing
                    gv1.Rows.Clear()
                    gv1.Columns.Clear()
                    gv1.GroupDescriptors.Clear()
                    gv1.MasterView.Refresh()
                    gv1.GroupDescriptors.Clear()
                    gv1.EnableFiltering = True
                    gv1.MasterTemplate.SummaryRowsBottom.Clear()
                    gv1.DataSource = dt
                    gv1.BestFitColumns()
                    SetGridFormationn()
                    'ReStoreGridLayout()
                    gv1.MasterTemplate.AutoExpandGroups = True
                    'EnableDisableControls(False)
                    RadPageView1.SelectedPage = RadPageViewPage2
                    gv1.BestFitColumns()
                ElseIf print = True Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(CrystalReportFolder.SalesReport, dt, "CrptTcsSummaryReport", "Tcs Summary Report")
                    frmCRV = Nothing

                End If
            Else
                clsCommon.MyMessageBoxShow("No data found")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub GheeReport(ByVal print As Boolean)
        Try
            Dim Qry As String = ""
            Dim whrcls As String = ""
            Dim BaseQry As String = ""
            Dim dt As DataTable = Nothing
            Dim whr As String = ""

            If txtCustomer.arrValueMember IsNot Nothing Then
                whrcls = " and TSPL_CUSTOMER_MASTER.Cust_Code in (''," & clsCommon.GetMulcallString(txtCustomer.arrValueMember) & ") "
            End If

            Qry = " select '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "' as ToDate,
                        Max(Customer_Name) as Customer_Name,
						Max(Item_Desc) as Item_Desc,Item_Code ,
                         Max(Unit_code) AS Unit_code,Sum(Qty) AS Qty,Sum(QtyAccToReportUOM) AS QtyAccToReportUOM,Max(UOM_Code) AS UOM_Code,max(Comp_Name) AS Comp_Name,max(Add1) AS Add1
                    FROM (
                        SELECT 
	                    TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE,
                        TSPL_CUSTOMER_MASTER.Customer_Name,
                        TSPL_SD_SHIPMENT_DETAIL.Unit_code, 
                        TSPL_SD_SHIPMENT_DETAIL.Qty,
						cast((TSPL_SD_SHIPMENT_DETAIL.Qty * ItemConvinUOM.Conversion_Factor / ItemConvReportUOM.Conversion_Factor
						) as Decimal(18, 2)
						) as QtyAccToReportUOM,
                        ItemConvReportUOM.UOM_Code, 
                        TSPL_COMPANY_MASTER.Comp_Name, 
                        TSPL_COMPANY_MASTER.Add1 ,
						TSPL_ITEM_MASTER.Item_Desc,
						TSPL_ITEM_MASTER.Item_Code
                        FROM TSPL_SD_SHIPMENT_DETAIL
                        LEFT OUTER JOIN TSPL_SD_SHIPMENT_HEAD ON TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE
                        LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.item_code = TSPL_SD_SHIPMENT_DETAIL.item_code
						left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code
                        left join TSPL_ITEM_UOM_DETAIL as ItemConvReportUOM on TSPL_ITEM_master.Item_Code = ItemConvReportUOM.Item_Code 
                        and ItemConvReportUOM.Report_UOM = 1
                        left join TSPL_ITEM_UOM_DETAIL as ItemConvinUOM on TSPL_SD_SHIPMENT_DETAIL.Item_Code = ItemConvinUOM.Item_Code 
                        and TSPL_SD_SHIPMENT_DETAIL.Unit_code = ItemConvinUOM.UOM_Code
						LEFT JOIN TSPL_COMPANY_MASTER ON 2 = 2
                        WHERE convert(date,Document_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and convert(date,Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value) + "' and  TSPL_ITEM_MASTER.Is_Ambient=1  AND TSPL_ITEM_MASTER.Item_Desc LIKE '%Ghee%'  " & whrcls & " ) xx
						group By Customer_Name,Item_Code"
            dt = clsDBFuncationality.GetDataTable(Qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If print = False Then
                    gv1.DataSource = Nothing
                    gv1.Rows.Clear()
                    gv1.Columns.Clear()
                    gv1.GroupDescriptors.Clear()
                    gv1.MasterView.Refresh()
                    gv1.GroupDescriptors.Clear()
                    gv1.EnableFiltering = True
                    gv1.MasterTemplate.SummaryRowsBottom.Clear()
                    gv1.DataSource = dt
                    gv1.BestFitColumns()
                    SetGridFormationn()
                    'ReStoreGridLayout()
                    gv1.MasterTemplate.AutoExpandGroups = True
                    'EnableDisableControls(False)
                    RadPageView1.SelectedPage = RadPageViewPage2
                    gv1.BestFitColumns()
                ElseIf print = True Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(CrystalReportFolder.SalesReport, dt, "CrptGheeReport", "Ghee Report")
                    frmCRV = Nothing

                End If
            Else
                clsCommon.MyMessageBoxShow("No data found")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Sub RouteWiseSale(ByVal print As Boolean)
        Try
            Dim Qry As String = ""
            Dim BaseQry As String = ""
            Dim dt As DataTable = Nothing
            Dim whr As String = ""
            Qry = " select '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "' as ToDate,
                                                MAX(TSPL_ROUTE_MASTER.Route_Desc) AS Route_Desc,
                                                MAX(TSPL_SD_SHIPMENT_HEAD.Route_No) AS Route_No,
                            MAX(TSPL_SD_SHIPMENT_HEAD.Shift_Type) AS Shift_Type,
                            SUM(
                                TSPL_SD_SHIPMENT_DETAIL.Qty * ItemConvinUOM.Conversion_Factor / ItemConvReportUOM.Conversion_Factor
                            ) AS QtyAccToReportUOM,
                            SUM(
                                CASE 
                                    WHEN TSPL_SD_SHIPMENT_HEAD.Shift_Type = 'Am' 
                                    THEN TSPL_SD_SHIPMENT_DETAIL.Qty * ItemConvinUOM.Conversion_Factor / ItemConvReportUOM.Conversion_Factor 
                                    ELSE 0 
                                END
                            ) AS qty_Am,
                            SUM(
                                CASE 
                                    WHEN TSPL_SD_SHIPMENT_HEAD.Shift_Type = 'PM' 
                                    THEN TSPL_SD_SHIPMENT_DETAIL.Qty * ItemConvinUOM.Conversion_Factor / ItemConvReportUOM.Conversion_Factor 
                                    ELSE 0 
                                END
                            ) AS qty_PM,
                            MAX(ItemConvReportUOM.UOM_Code) AS UOM_Code, 
                            MAX(TSPL_COMPANY_MASTER.Comp_Name) AS Comp_Name, 
                            MAX(TSPL_COMPANY_MASTER.Add1) AS Add1
                        FROM TSPL_SD_SHIPMENT_DETAIL
                        LEFT OUTER JOIN TSPL_SD_SHIPMENT_HEAD 
                            ON TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE
                        LEFT OUTER JOIN TSPL_CUSTOMER_MASTER 
                            ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code
                        LEFT OUTER JOIN TSPL_ITEM_MASTER 
                            ON TSPL_ITEM_MASTER.item_code = TSPL_SD_SHIPMENT_DETAIL.item_code
                        left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No=TSPL_SD_SHIPMENT_HEAD.Route_No
                        LEFT JOIN TSPL_ITEM_UOM_DETAIL AS ItemConvReportUOM 
                            ON TSPL_ITEM_MASTER.Item_Code = ItemConvReportUOM.Item_Code AND ItemConvReportUOM.Report_UOM = 1
                        LEFT JOIN TSPL_ITEM_UOM_DETAIL AS ItemConvinUOM 
                            ON TSPL_SD_SHIPMENT_DETAIL.Item_Code = ItemConvinUOM.Item_Code AND TSPL_SD_SHIPMENT_DETAIL.Unit_code = ItemConvinUOM.UOM_Code
                        LEFT JOIN TSPL_COMPANY_MASTER 
                            ON 2 = 2
                        WHERE convert(date,Document_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and convert(date,Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value) + "' AND TSPL_ITEM_MASTER.IsTaxable = 0 
                        AND TSPL_SD_SHIPMENT_HEAD.Route_No <> 'DIRECT' 
	                    AND TSPL_SD_SHIPMENT_HEAD.Route_No <> 'DIRECT1' 
                    GROUP BY TSPL_SD_SHIPMENT_HEAD.Route_No"
            dt = clsDBFuncationality.GetDataTable(Qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If print = False Then
                    gv1.DataSource = Nothing
                    gv1.Rows.Clear()
                    gv1.Columns.Clear()
                    gv1.GroupDescriptors.Clear()
                    gv1.MasterView.Refresh()
                    gv1.GroupDescriptors.Clear()
                    gv1.EnableFiltering = True
                    gv1.MasterTemplate.SummaryRowsBottom.Clear()
                    gv1.DataSource = dt
                    gv1.BestFitColumns()
                    SetGridFormationn()
                    'ReStoreGridLayout()
                    gv1.MasterTemplate.AutoExpandGroups = True
                    'EnableDisableControls(False)
                    RadPageView1.SelectedPage = RadPageViewPage2
                    gv1.BestFitColumns()
                ElseIf print = True Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(CrystalReportFolder.SalesReport, dt, "CrptRouteWiseSale", "Route Wise Sale")
                    frmCRV = Nothing

                End If
            Else
                clsCommon.MyMessageBoxShow("No data found")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub



    Sub CreditPartyWiseSaleAmount(ByVal print As Boolean)
        Try
            Dim Qry As String = ""
            Dim BaseQry As String = ""
            Dim whrcls As String = ""
            Dim dt As DataTable = Nothing
            Dim whr As String = ""
            If txtCustomer.arrValueMember IsNot Nothing Then
                whrcls = " and TSPL_CUSTOMER_MASTER.Cust_Code in (''," & clsCommon.GetMulcallString(txtCustomer.arrValueMember) & ") "
            End If
            Qry = " select '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "' as ToDate,
                            TSPL_CUSTOMER_MASTER.Customer_Name,
                            Max(TSPL_CUSTOMER_MASTER.cust_code) as cust_code,
                            Sum(CASE 
                                WHEN TSPL_ITEM_MASTER.IsTaxable = 0 THEN TSPL_SD_SHIPMENT_DETAIL.Item_Net_Amt  
                                ELSE 0 
                            END) AS Milk_Amount, 
                            Sum(CASE 
                                WHEN TSPL_ITEM_MASTER.IsTaxable = 1 THEN TSPL_SD_SHIPMENT_DETAIL.Item_Net_Amt 
                                ELSE 0 
                            END) AS Product_Amount,
	                        Sum(TSPL_SD_SHIPMENT_DETAIL.Item_Net_Amt) AS amount,
	                        MAX(TSPL_COMPANY_MASTER.Comp_Name) AS Comp_Name, 
                            MAX(TSPL_COMPANY_MASTER.Add1) AS Add1 
                        FROM TSPL_SD_SHIPMENT_HEAD 
                        LEFT OUTER JOIN TSPL_SD_SHIPMENT_DETAIL 
                            ON TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE = TSPL_SD_SHIPMENT_HEAD.Document_Code
                        LEFT OUTER JOIN TSPL_CUSTOMER_MASTER 
                            ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code
                        LEFT OUTER JOIN TSPL_ITEM_MASTER 
                            ON TSPL_ITEM_MASTER.item_code = TSPL_SD_SHIPMENT_DETAIL.item_code
	                        LEFT JOIN TSPL_ITEM_UOM_DETAIL AS ItemConvinUOM 
                            ON TSPL_SD_SHIPMENT_DETAIL.Item_Code = ItemConvinUOM.Item_Code AND TSPL_SD_SHIPMENT_DETAIL.Unit_code = ItemConvinUOM.UOM_Code
                        LEFT JOIN TSPL_COMPANY_MASTER ON 2 = 2
                        WHERE convert(date,Document_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and convert(date,Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value) + "' " & whrcls & "
                        AND TSPL_CUSTOMER_MASTER.Credit_Customer = 'Y'
                        GROUP BY 
                            TSPL_CUSTOMER_MASTER.Customer_Name"
            dt = clsDBFuncationality.GetDataTable(Qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If print = False Then
                    gv1.DataSource = Nothing
                    gv1.Rows.Clear()
                    gv1.Columns.Clear()
                    gv1.GroupDescriptors.Clear()
                    gv1.MasterView.Refresh()
                    gv1.GroupDescriptors.Clear()
                    gv1.EnableFiltering = True
                    gv1.MasterTemplate.SummaryRowsBottom.Clear()
                    gv1.DataSource = dt
                    gv1.BestFitColumns()
                    SetGridFormationn()
                    'ReStoreGridLayout()
                    gv1.MasterTemplate.AutoExpandGroups = True
                    'EnableDisableControls(False)
                    RadPageView1.SelectedPage = RadPageViewPage2
                    gv1.BestFitColumns()
                ElseIf print = True Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(CrystalReportFolder.SalesReport, dt, "CrptCreditPartyWiseSaleAmount", "Credit Party Wise Sale Amount")
                    frmCRV = Nothing

                End If
            Else
                clsCommon.MyMessageBoxShow("No data found")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub



    Sub BillWisesaleSummary(ByVal print As Boolean)
        Try
            Dim Qry As String = ""
            Dim BaseQry As String = ""
            Dim dt As DataTable = Nothing
            Dim whr As String = ""
            Qry = " SELECT '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "' as ToDate,
                                    MAX(Item_Desc) AS Item_Desc, 
                        Item_Code, MAX(Unit_code) AS Unit_code,SUM(Qty) AS Qty,sum(Amount) as Amount,SUM(QtyAccToReportUOM) AS QtyAccToReportUOM,MAX(UOM_Code) AS UOM_Code,MAX(Comp_Name) AS Comp_Name,MAX(Add1) AS Add1,MAX(Add2) AS Add2,MAX(Add3) AS Add3,MAX(City_Code) AS City_Code,MAX(State) AS State, MAX(Document_Date) AS Document_Date 
                    FROM (
                        SELECT 
	                    TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE,
  
                            TSPL_ITEM_MASTER.Item_Desc, 
                            TSPL_ITEM_MASTER.Item_Code, 
                            TSPL_SD_SHIPMENT_DETAIL.Unit_code, 
                            TSPL_SD_SHIPMENT_DETAIL.Qty,
                    cast(
                                    (
                                      TSPL_SD_SHIPMENT_DETAIL.Qty * ItemConvinUOM.Conversion_Factor / ItemConvReportUOM.Conversion_Factor
                                    ) as Decimal(18, 2)
                                  ) as QtyAccToReportUOM,
                            ItemConvReportUOM.UOM_Code, 
                            TSPL_COMPANY_MASTER.Comp_Name, 
                            TSPL_COMPANY_MASTER.Add1, 
                            TSPL_COMPANY_MASTER.Add2, 
                            TSPL_COMPANY_MASTER.Add3, 
                            TSPL_COMPANY_MASTER.City_Code, 
                            TSPL_COMPANY_MASTER.State,
		                    TSPL_SD_SHIPMENT_DETAIL.Amount,
                            Document_Date 
                        FROM TSPL_SD_SHIPMENT_DETAIL
                        LEFT OUTER JOIN TSPL_SD_SHIPMENT_HEAD ON TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE
                        LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.item_code = TSPL_SD_SHIPMENT_DETAIL.item_code
                     left join TSPL_ITEM_UOM_DETAIL as ItemConvReportUOM on TSPL_ITEM_master.Item_Code = ItemConvReportUOM.Item_Code 
                                        and ItemConvReportUOM.Report_UOM = 1
                                         left join TSPL_ITEM_UOM_DETAIL as ItemConvinUOM on TSPL_SD_SHIPMENT_DETAIL.Item_Code = ItemConvinUOM.Item_Code 
                                       and TSPL_SD_SHIPMENT_DETAIL.Unit_code = ItemConvinUOM.UOM_Code
                        LEFT JOIN TSPL_COMPANY_MASTER ON 2 = 2 where convert(date,Document_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and convert(date,Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value) + "' and  TSPL_ITEM_MASTER.IsTaxable=0 ) xx 
                GROUP BY Item_Code"
            dt = clsDBFuncationality.GetDataTable(Qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If print = False Then
                    gv1.DataSource = Nothing
                    gv1.Rows.Clear()
                    gv1.Columns.Clear()
                    gv1.GroupDescriptors.Clear()
                    gv1.MasterView.Refresh()
                    gv1.GroupDescriptors.Clear()
                    gv1.EnableFiltering = True
                    gv1.MasterTemplate.SummaryRowsBottom.Clear()
                    gv1.DataSource = dt
                    gv1.BestFitColumns()
                    SetGridFormationn()
                    'ReStoreGridLayout()
                    gv1.MasterTemplate.AutoExpandGroups = True
                    'EnableDisableControls(False)
                    RadPageView1.SelectedPage = RadPageViewPage2
                    gv1.BestFitColumns()
                ElseIf print = True Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(CrystalReportFolder.SalesReport, dt, "CrptBillWiseSaleOfMilkSummary", "Bill Wise Sale Of Milk Summary")
                    frmCRV = Nothing

                End If
            Else
                clsCommon.MyMessageBoxShow("No data found")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub ProductWiseSale(ByVal Print As Boolean)
        Try
            Dim Qry As String = ""
            Dim BaseQry As String = ""
            Dim dt As DataTable = Nothing
            Dim whr As String = ""
            Qry = " SELECT '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "' as ToDate,
                MAX(Item_Desc) AS Item_Desc, 
                    Item_Code, MAX(Unit_code) AS Unit_code,SUM(Qty) AS Qty,SUM(QtyAccToReportUOM) AS QtyAccToReportUOM,MAX(UOM_Code) AS UOM_Code,MAX(Comp_Name) AS Comp_Name,MAX(Add1) AS Add1,MAX(Add2) AS Add2,MAX(Add3) AS Add3,MAX(City_Code) AS City_Code,MAX(State) AS State, MAX(Document_Date) AS Document_Date 
                FROM (
                    SELECT 
	                TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE,
  
                        TSPL_ITEM_MASTER.Item_Desc, 
                        TSPL_ITEM_MASTER.Item_Code, 
                        TSPL_SD_SHIPMENT_DETAIL.Unit_code, 
                        TSPL_SD_SHIPMENT_DETAIL.Qty,
                cast(
                                (
                                  TSPL_SD_SHIPMENT_DETAIL.Qty * ItemConvinUOM.Conversion_Factor / ItemConvReportUOM.Conversion_Factor
                                ) as Decimal(18, 2)
                              ) as QtyAccToReportUOM,
                        ItemConvReportUOM.UOM_Code, 
                        TSPL_COMPANY_MASTER.Comp_Name, 
                        TSPL_COMPANY_MASTER.Add1, 
                        TSPL_COMPANY_MASTER.Add2, 
                        TSPL_COMPANY_MASTER.Add3, 
                        TSPL_COMPANY_MASTER.City_Code, 
                        TSPL_COMPANY_MASTER.State, 
                        Document_Date 
                    FROM TSPL_SD_SHIPMENT_DETAIL
                    LEFT OUTER JOIN TSPL_SD_SHIPMENT_HEAD ON TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE
                    LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.item_code = TSPL_SD_SHIPMENT_DETAIL.item_code
                 left join TSPL_ITEM_UOM_DETAIL as ItemConvReportUOM on TSPL_ITEM_master.Item_Code = ItemConvReportUOM.Item_Code 
                                    and ItemConvReportUOM.Report_UOM = 1
                                     left join TSPL_ITEM_UOM_DETAIL as ItemConvinUOM on TSPL_SD_SHIPMENT_DETAIL.Item_Code = ItemConvinUOM.Item_Code 
                                   and TSPL_SD_SHIPMENT_DETAIL.Unit_code = ItemConvinUOM.UOM_Code
                    LEFT JOIN TSPL_COMPANY_MASTER ON 2 = 2 where convert(date,Document_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and convert(date,Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value) + "' AND TSPL_ITEM_MASTER.Is_Ambient = 1 ) xx 
                GROUP BY Item_Code"
            dt = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If Print = False Then
                    gv1.DataSource = Nothing
                    gv1.Rows.Clear()
                    gv1.Columns.Clear()
                    gv1.GroupDescriptors.Clear()
                    gv1.MasterView.Refresh()
                    gv1.GroupDescriptors.Clear()
                    gv1.EnableFiltering = True
                    gv1.MasterTemplate.SummaryRowsBottom.Clear()
                    gv1.DataSource = dt
                    gv1.BestFitColumns()
                    SetGridFormationn()
                    'ReStoreGridLayout()
                    gv1.MasterTemplate.AutoExpandGroups = True
                    'EnableDisableControls(False)
                    RadPageView1.SelectedPage = RadPageViewPage2
                    gv1.BestFitColumns()
                ElseIf Print = True Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(CrystalReportFolder.SalesReport, dt, "CrptProductWiseSaleQuantity", "Product Wise Sale Quantity")
                    frmCRV = Nothing

                End If
            Else
                clsCommon.MyMessageBoxShow("No data found")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetGridFormationn()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = True
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
            gv1.Columns(ii).Width = 80
        Next
    End Sub

    Sub funreset()
        BtnStcRegisterPartyandItemWiseSummary.IsChecked = False
        BtnStcRegisterItemWiseSummary.IsChecked = False
        BtnProductWiseSaleQuantity.IsChecked = False
        BtnMilkStcSummary.IsChecked = False
        BtnPartySaleMilkProductt.IsChecked = False
        BtnProductSalesSummary.IsChecked = False
        BtnBillWiseSaleOfMilk.IsChecked = False
        BtnBillWiseSaleOfMilkSummary.IsChecked = False
        BtnTransportationCharges.IsChecked = False
        BtnTcsSummary.IsChecked = False
        BtnGheeReport.IsChecked = False
        BtnRouteWiseSale.IsChecked = False
        BtnCreditPartyWiseSaleAmount.IsChecked = False
        EnableDisableControls(True)
        gv1.DataSource = Nothing
        rbtnDocumentDate.IsChecked = True
        rbtnDetail.IsChecked = True
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub EnableDisableControls(ByVal val As Boolean)
        RadGroupBox1.Enabled = val
    End Sub

    Private Sub LoadData()
        Try

            Dim qry As String = ""
            Dim groupby As String = ""
            Dim whrcls As String = ""
            Dim BaseQuery As String = ""
            Dim qry2 As String = ""
            If txtCustomer.arrValueMember IsNot Nothing Then
                whrcls = " and Cust_Code in (''," & clsCommon.GetMulcallString(txtCustomer.arrValueMember) & ") "
            End If

            If txtItem.arrValueMember IsNot Nothing Then
                whrcls += " and Item_Code in (" & clsCommon.GetMulcallString(txtItem.arrValueMember) & ") "
            End If

            If txtTransaction.arrValueMember IsNot Nothing Then
                whrcls += " and Trans_Name in (" & clsCommon.GetMulcallString(txtTransaction.arrValueMember) & ") "
            End If

            qry = "---------------- VCGL---------------------------- 
            select 'VCGL' as Trans_Name,TSPL_VCGL_Head.Document_Date,'' as Cust_Code, TSPL_VCGL_Detail.Document_No,'' as Item_Code ,0 as Qty ,'' as UOM,case when TSPL_VCGL_Detail.Row_Type ='Customer' then TSPL_VCGL_Detail.Dr_Amount-TSPL_VCGL_Detail.Cr_Amount else 0 end as Item_Net_Amt ,0 as Total_Tax_Amt, '' as Tax,  0 as Tax_Amt ,0 as Tax_Rate,0 as Item_Rate from TSPL_VCGL_Detail left outer join TSPL_VCGL_Head on TSPL_VCGL_Head.Document_No  =TSPL_VCGL_Detail.Document_No  where TSPL_VCGL_Detail.Document_No in (select  TSPL_Customer_INVOICE_HEAD.Against_VCGL  from TSPL_Customer_Invoice_Head
            left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL ,'')<>''     and convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date ,103)>=convert(date,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103) AND convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date,103)<=convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103)   AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 ) and TSPL_VCGL_Head.Status =1  
            union all 
            select 'VCGL' as Trans_Name,TSPL_VCGL_Head.Document_Date,'' as Cust_Code, TSPL_VCGL_Head.Document_No,'' as Item_Code ,0 as Qty ,'' as UOM,case when TSPL_VCGL_Head.Document_Type  ='C' then TSPL_VCGL_Head.Tot_Cr_Amount-TSPL_VCGL_Head.Tot_Dr_Amount else 0 end as Item_Net_Amt ,0 as Total_Tax_Amt, '' as Tax, 0 as Tax_Amt ,0 as Tax_Rate,0 as Item_Rate from TSPL_VCGL_Detail left outer join TSPL_VCGL_Head on TSPL_VCGL_Head.Document_No  =TSPL_VCGL_Detail.Document_No  
            where TSPL_VCGL_Head.Document_No in (select  TSPL_Customer_INVOICE_HEAD.Against_VCGL  from TSPL_Customer_Invoice_Head left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL ,'')<>''     and convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date ,103)>=convert(date,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103) AND convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date,103)<=convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103)   AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 ) and TSPL_VCGL_Head.Status =1  
            Union all 
            ---------------- Sale Invoice ,BULK SALE,CAN SALE--------------------- 
            select 'DS'  as Trans_Name," & IIf(rbtnSupplyDate.IsChecked, " TSPL_SD_SHIPMENT_HEAD.Supply_Date as Document_Date", " TSPL_SD_SALE_INVOICE_HEAD.Document_Date") & " ,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code as Cust_Code, TSPL_SD_SALE_INVOICE_DETAIL.Document_Code as Document_No,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code ,TSPL_SD_SALE_INVOICE_DETAIL.Qty ,TSPL_SD_SALE_INVOICE_DETAIL.Unit_code as UOM, (TSPL_SD_SALE_INVOICE_DETAIL.Amount+TSPL_SD_SALE_INVOICE_DETAIL.Total_Tax_Amt-Total_Disc_Amt)*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end)*(case when len(TSPL_SD_SALE_INVOICE_HEAD.Invoice_No_For_Supplementary)>0 and TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type='C' then -1 else 1 end) as Item_Net_Amt ,TSPL_SD_SALE_INVOICE_DETAIL.Total_Tax_Amt,  '' as Tax,  0 as Tax_Amt ,0 as Tax_Rate,convert(decimal(18,2), TSPL_SD_SALE_INVOICE_DETAIL.Item_Cost) as  Item_Rate   from TSPL_SD_SALE_INVOICE_DETAIL  left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD .Document_Code =TSPL_SD_SALE_INVOICE_DETAIL.Document_Code left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_SD_SALE_INVOICE_HEAD.Customer_Code 
            left outer join TSPL_LOCATION_MASTER on TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location =TSPL_LOCATION_MASTER.Location_Code  where TSPL_SD_SALE_INVOICE_DETAIL.Document_Code in (select  TSPL_Customer_INVOICE_HEAD.Against_Sale_No  from TSPL_Customer_Invoice_Head left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_Customer_INVOICE_HEAD.Against_Sale_No  left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No   left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')<>''   and  convert(date, " & IIf(rbtnSupplyDate.IsChecked, "TSPL_SD_SHIPMENT_HEAD.Supply_Date", "TSPL_Customer_INVOICE_HEAD.Document_Date") & " ,103)>=convert(date,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103) AND convert(date, " & IIf(rbtnSupplyDate.IsChecked, "TSPL_SD_SHIPMENT_HEAD.Supply_Date", "TSPL_Customer_INVOICE_HEAD.Document_Date") & ",103)<=convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103)   AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT'))) and TSPL_SD_SALE_INVOICE_HEAD.Status =1   
            union all 
            select 'CAN-SALE' as Trans_Name,TSPL_CANSALE_INVOICE_HEAD.Document_Date,TSPL_CANSALE_INVOICE_HEAD.Customer_Code as Cust_Code, TSPL_CANSALE_INVOICE_detail.Document_No,TSPL_CANSALE_INVOICE_detail.ItemCode ,TSPL_CANSALE_INVOICE_detail.Qty ,TSPL_CANSALE_INVOICE_detail.UOM ,TSPL_CANSALE_INVOICE_detail.Item_Net_Amt ,TSPL_CANSALE_INVOICE_detail.Total_Tax_Amt,'' as Tax,  0 as Tax_Amt ,0 as Tax_Rate, convert(decimal(18,2),TSPL_CANSALE_INVOICE_detail.PriceRate) as Item_Rate  from TSPL_CANSALE_INVOICE_detail left outer join TSPL_CANSALE_INVOICE_HEAD on TSPL_CANSALE_INVOICE_HEAD .Document_no =TSPL_CANSALE_INVOICE_detail.Document_No left outer join TSPL_Customer_INVOICE_HEAD on TSPL_Customer_INVOICE_HEAD .Against_Sale_No =TSPL_CANSALE_INVOICE_HEAD.Document_No 
            left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_CANSALE_INVOICE_HEAD.Customer_Code where TSPL_CANSALE_INVOICE_HEAD.Posted =1 and TSPL_CANSALE_INVOICE_HEAD.Document_No in (select  TSPL_Customer_INVOICE_HEAD.Against_Sale_No  from TSPL_Customer_Invoice_Head  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')<>''   and convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date ,103)>=convert(date,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103) AND convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date,103)<=convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103)   AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 ) 
            union all 
            select  'INVOICE-BS' as Trans_Name,TSPL_INVOICE_MASTER_BULKSALE.Document_Date,TSPL_INVOICE_MASTER_BULKSALE.Customer_Code as Cust_Code,TSPL_INVOICE_DETAIL_BULKSALE.Document_No,TSPL_INVOICE_DETAIL_BULKSALE.item_code,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceQty as Qty,TSPL_INVOICE_DETAIL_BULKSALE.Unit_code as UOM,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceAmount ,TSPL_INVOICE_DETAIL_BULKSALE.Total_Tax_Amt, '' as Tax,  0 as Tax_Amt ,0 as Tax_Rate,0 as Item_Rate from TSPL_INVOICE_DETAIL_BULKSALE left outer join TSPL_INVOICE_MASTER_BULKSALE on TSPL_INVOICE_MASTER_BULKSALE .Document_no =TSPL_INVOICE_DETAIL_BULKSALE.Document_No  left outer join TSPL_Customer_INVOICE_HEAD on TSPL_Customer_INVOICE_HEAD .Against_Sale_No =TSPL_INVOICE_MASTER_BULKSALE.Document_No 
            where TSPL_INVOICE_MASTER_BULKSALE.Posted =1 and TSPL_INVOICE_MASTER_BULKSALE.Document_No in (select  TSPL_Customer_INVOICE_HEAD.Against_Sale_No  from TSPL_Customer_Invoice_Head  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')<>''   and convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date ,103)>=convert(date,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103) AND convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date,103)<=convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103)   AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 ) 
            union all  
            ---------------- Sale RETURN ,CAN SALE RETURN----------------------- 
            select 'DSR' AS Trans_Name,TSPL_SD_SALE_RETURN_HEAD.Document_Date,TSPL_SD_SALE_RETURN_HEAD.Customer_Code as Cust_Code, TSPL_SD_SALE_RETURN_DETAIL.Document_Code,TSPL_SD_SALE_RETURN_DETAIL.Item_Code ,-(TSPL_SD_SALE_RETURN_DETAIL.Qty)Qty ,TSPL_SD_SALE_RETURN_DETAIL.Unit_code as UOM,-(TSPL_SD_SALE_RETURN_DETAIL.Item_Net_Amt)Item_Net_Amt ,TSPL_SD_SALE_RETURN_DETAIL.Total_Tax_Amt, '' as Tax,  0 as Tax_Amt ,0 as Tax_Rate,convert(decimal(18,2),TSPL_SD_SALE_RETURN_DETAIL.Item_Cost)  as Item_Rate from TSPL_SD_SALE_RETURN_DETAIL left outer join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Document_Code  =TSPL_SD_SALE_RETURN_DETAIL.Document_Code  
            where TSPL_SD_SALE_RETURN_DETAIL.Document_Code in (select  TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No  from TSPL_Customer_Invoice_Head left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')<>''    and convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date ,103)>=convert(date,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103) AND convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date,103)<=convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103)   AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT'))  and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 ) and TSPL_SD_SALE_RETURN_HEAD.Status =1 
            union all 
            select 'BULK-SALE-RE' AS Trans_Name, TSPL_SALE_RETURN_MASTER_BULKSALE.Document_Date,TSPL_SALE_RETURN_MASTER_BULKSALE.Customer_Code as Cust_Code, TSPL_SALE_RETURN_DETAIL_BULKSALE.Document_No,TSPL_SALE_RETURN_DETAIL_BULKSALE.item_code,TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceQty as Qty,TSPL_SALE_RETURN_DETAIL_BULKSALE.Unit_code as UOM,TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceAmount ,TSPL_SALE_RETURN_DETAIL_BULKSALE.Total_Tax_Amt, '' as Tax,  0 as Tax_Amt ,0 as Tax_Rate,0 as Item_Rate from TSPL_SALE_RETURN_DETAIL_BULKSALE left outer join TSPL_SALE_RETURN_MASTER_BULKSALE on TSPL_SALE_RETURN_MASTER_BULKSALE .Document_no =TSPL_SALE_RETURN_DETAIL_BULKSALE.Document_No left outer join TSPL_Customer_INVOICE_HEAD on TSPL_Customer_INVOICE_HEAD .Against_Sale_Return_No =TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No where TSPL_SALE_RETURN_MASTER_BULKSALE.Posted =1 AND TSPL_SALE_RETURN_MASTER_BULKSALE.AGAINST='Bulk Invoice'  and TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No in (select  TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No  from TSPL_Customer_Invoice_Head left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')<>''    and convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date ,103)>=convert(date,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103) AND convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date,103)<=convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103)   AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 ) 
            union all 
            ---------------- Scrap Sale Return---------------------- 
            select 'SCRAP-S-R' as Trans_Name,TSPL_SCRAPSALE_HEAD_RETURN.Return_ship_Date as Document_Date,TSPL_SCRAPSALE_HEAD_RETURN.cust_Code as Cust_Code, TSPL_SCRAPSALE_DETAIL_RETURN.Document_No,TSPL_SCRAPSALE_DETAIL_RETURN.Item_Code ,TSPL_SCRAPSALE_DETAIL_RETURN.shipped_Qty AS Qty ,TSPL_SCRAPSALE_DETAIL_RETURN.Unit_code as UOM,TSPL_SCRAPSALE_DETAIL_RETURN.TotalAmt as Item_Net_Amt ,TSPL_SCRAPSALE_DETAIL_RETURN.TotalTaxAmt as Total_Tax_Amt, '' as Tax,  0 as Tax_Amt ,0 as Tax_Rate,convert(decimal(18,2),TSPL_SCRAPSALE_DETAIL_RETURN.price)  as Item_Rate from TSPL_SCRAPSALE_DETAIL_RETURN left outer join TSPL_SCRAPSALE_HEAD_RETURN on TSPL_SCRAPSALE_HEAD_RETURN .Document_No =TSPL_SCRAPSALE_DETAIL_RETURN.Document_No  where TSPL_SCRAPSALE_DETAIL_RETURN.Document_No in (select  TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn  from TSPL_Customer_Invoice_Head left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn ,'')<>'' and convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date ,103)>=convert(date,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103) AND convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date,103)<=convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103)   AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 ) and TSPL_SCRAPSALE_HEAD_RETURN.ispost =1  
            union all   
            ---------------- Scrap Sale ----------------- 
            select 'SCRAP-SALE' as Trans_Name,TSPL_SCRAPINVOICE_HEAD.shipment_Date as Document_Date,TSPL_SCRAPINVOICE_HEAD.cust_Code as Cust_Code,  TSPL_SCRAPINVOICE_DETAIL.invoice_No AS Document_No ,TSPL_SCRAPINVOICE_DETAIL.Item_Code ,TSPL_SCRAPINVOICE_DETAIL.shipped_Qty AS Qty ,TSPL_SCRAPINVOICE_DETAIL.Unit_code as UOM,TSPL_SCRAPINVOICE_DETAIL.TotalAmt as Item_Net_Amt ,TSPL_SCRAPINVOICE_DETAIL.TotalTaxAmt as Total_Tax_Amt, '' as Tax,  0 as Tax_Amt ,0 as Tax_Rate,convert(decimal(18,2),TSPL_SCRAPINVOICE_DETAIL.price) as Item_Rate from TSPL_SCRAPINVOICE_DETAIL left outer join TSPL_SCRAPINVOICE_HEAD on TSPL_SCRAPINVOICE_HEAD.invoice_No =TSPL_SCRAPINVOICE_DETAIL.invoice_No  where TSPL_SCRAPINVOICE_DETAIL.invoice_No in (select  TSPL_Customer_INVOICE_HEAD.AgainstScrap  from TSPL_Customer_Invoice_Head left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrap ,'')<>'' and convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date ,103)>=convert(date,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103) AND convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date,103)<=convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103)   AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 ) and TSPL_SCRAPINVOICE_HEAD.ispost =1  
            union all 
            ---------------- mcc Material Sale RETURN-------------------------------- 
            select 'M-Material-R' as Trans_Name, TSPL_SD_SALE_RETURN_HEAD.Document_Date ,TSPL_SD_SALE_RETURN_HEAD.Customer_Code as Cust_Code, TSPL_SD_SALE_RETURN_DETAIL.Document_Code,TSPL_SD_SALE_RETURN_DETAIL.Item_Code ,-(TSPL_SD_SALE_RETURN_DETAIL.Qty)Qty ,TSPL_SD_SALE_RETURN_DETAIL.Unit_code as UOM,-(TSPL_SD_SALE_RETURN_DETAIL.Item_Net_Amt)Item_Net_Amt ,TSPL_SD_SALE_RETURN_DETAIL.Total_Tax_Amt, '' as Tax,  0 as Tax_Amt ,0 as Tax_Rate,convert(decimal(18,2),TSPL_SD_SALE_RETURN_DETAIL.Item_Cost) as Item_Rate from TSPL_SD_SALE_RETURN_DETAIL  left outer join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Document_Code  =TSPL_SD_SALE_RETURN_DETAIL.Document_Code  where TSPL_SD_SALE_RETURN_DETAIL.Document_Code in (select  TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return  from TSPL_Customer_Invoice_Head left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return ,'')<>'' and convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date ,103)>=convert(date,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103) AND convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date,103)<=convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103)   AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 ) and TSPL_SD_SALE_RETURN_HEAD.Status =1 AND TSPL_SD_SALE_RETURN_HEAD.Trans_Type='MCC' 
            union all 
            ---------------- Security Receipt--------------------------
            select 'AR-INVOICE' as Trans_Name,TSPL_Customer_INVOICE_HEAD.Document_Date,TSPL_Customer_INVOICE_HEAD.Customer_Code as Cust_Code,  TSPL_Customer_Invoice_Head.Against_Security_Receipt_No as Document_No,'' AS Item_Code ,0 AS Qty ,'' as UOM,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end *  (isnull(TSPL_Customer_Invoice_Detail.Total_Amount ,0))as Item_Net_Amt ,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end *  (isnull(TSPL_Customer_Invoice_Detail.Total_Tax  ,0)) as Total_Tax_Amt,'' as Tax,  0 as Tax_Amt ,0 as Tax_Rate, 0 as Item_Rate from TSPL_Customer_Invoice_Detail left outer join TSPL_Customer_INVOICE_HEAD on TSPL_Customer_INVOICE_HEAD .Document_No =TSPL_Customer_Invoice_Detail.Document_No  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.Against_Security_Receipt_No ,'')<>''  and TSPL_Customer_INVOICE_HEAD.Status =1    and convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date ,103)>=convert(date,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103) AND convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date,103)<=convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103)   AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 
            union all 
             ---------------- Transfer --------------------------
            select  'STO-TRANSFER' as Trans_Name,TSPL_TRANSFER_ORDER_HEAD.Document_Date,To_Location as Cust_Code,TSPL_TRANSFER_ORDER_DETAIL.Document_No,TSPL_TRANSFER_ORDER_DETAIL.Item_Code ,TSPL_TRANSFER_ORDER_DETAIL.Out_Qty as Qty,TSPL_TRANSFER_ORDER_DETAIL.Unit_code as UOM,  TSPL_TRANSFER_ORDER_DETAIL.Item_Net_Amt ,TSPL_TRANSFER_ORDER_DETAIL.Total_Tax_Amt,  '' as Tax,  0 as Tax_Amt ,0 as Tax_Rate,convert(decimal(18,2), TSPL_TRANSFER_ORDER_DETAIL.Item_Cost) as  Item_Rate from TSPL_TRANSFER_ORDER_DETAIL  left outer join TSPL_TRANSFER_ORDER_HEAD on TSPL_TRANSFER_ORDER_HEAD.Document_No =TSPL_TRANSFER_ORDER_DETAIL.Document_No LEFT join TSPL_LOCATION_MASTER as  From_LOC ON TSPL_TRANSFER_ORDER_HEAD.FROM_LOCATION=From_LOC.LOCATION_CODE where convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_Date ,103)>=convert(date,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103) AND convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103)<=convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103)  and  TSPL_TRANSFER_ORDER_HEAD.status = 1  and TSPL_TRANSFER_ORDER_HEAD.transfer_type = 'O'
            union all
            ---------------- Transfer Return--------------------------
            select  'STO-TRANS-R' as Trans_Name,TSPL_TRANSFER_ORDER_HEAD.Document_Date,Location as Cust_Code,TSPL_TRANSFER_ORDER_DETAIL.Document_No,TSPL_TRANSFER_ORDER_DETAIL.Item_Code ,-(TSPL_TRANSFER_ORDER_DETAIL.Out_Qty) as Qty,TSPL_TRANSFER_ORDER_DETAIL.Unit_code as UOM,  -(TSPL_TRANSFER_ORDER_DETAIL.Item_Net_Amt)Item_Net_Amt ,TSPL_TRANSFER_ORDER_DETAIL.Total_Tax_Amt,  '' as Tax,  0 as Tax_Amt ,0 as Tax_Rate ,convert(decimal(18,2), TSPL_TRANSFER_ORDER_DETAIL.Item_Cost) as  Item_Rate from TSPL_TRANSFER_ORDER_DETAIL  left outer join TSPL_TRANSFER_ORDER_HEAD on TSPL_TRANSFER_ORDER_HEAD.Document_No =TSPL_TRANSFER_ORDER_DETAIL.Document_No LEFT join TSPL_LOCATION_MASTER as  From_LOC ON TSPL_TRANSFER_ORDER_HEAD.FROM_LOCATION=From_LOC.LOCATION_CODE where convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_Date ,103)>=convert(date,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103) AND convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103)<=convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103)  and  TSPL_TRANSFER_ORDER_HEAD.status = 1 and TSPL_TRANSFER_ORDER_HEAD.transfer_type = 'T'"

            qry += " " & Environment.NewLine & " Union all " & Environment.NewLine & ""
            For ii As Integer = 1 To 6
                If ii > 1 Then
                    BaseQuery += " Union all " & Environment.NewLine & ""
                End If

                BaseQuery += "---------------- Sale Invoice ,BULK SALE,CAN SALE--------------------- 
            select 'DS'  as Trans_Name," & IIf(rbtnSupplyDate.IsChecked, " TSPL_SD_SHIPMENT_HEAD.Supply_Date as Document_Date", " TSPL_SD_SALE_INVOICE_HEAD.Document_Date") & " ,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code as Cust_Code, TSPL_SD_SALE_INVOICE_DETAIL.Document_Code as Document_No,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code ,0 as Qty ,TSPL_SD_SALE_INVOICE_DETAIL.Unit_code as UOM,0 as Item_Net_Amt ,TSPL_SD_SALE_INVOICE_DETAIL.Total_Tax_Amt,   isnull(TSPL_SD_SALE_INVOICE_DETAIL.TAX" & ii & ",'') AS Tax,  TSPL_SD_SALE_INVOICE_DETAIL.TAX" & ii & "_Amt as TAX_Amt,TSPL_SD_SALE_INVOICE_DETAIL.TAX" & ii & "_Rate as TAX_Rate ,0  as Item_Rate   from TSPL_SD_SALE_INVOICE_DETAIL  left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD .Document_Code =TSPL_SD_SALE_INVOICE_DETAIL.Document_Code  left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_SD_SALE_INVOICE_HEAD.Customer_Code 
            left outer join TSPL_LOCATION_MASTER on TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location =TSPL_LOCATION_MASTER .Location_Code  where TSPL_SD_SALE_INVOICE_DETAIL.Document_Code in (select  TSPL_Customer_INVOICE_HEAD.Against_Sale_No  from TSPL_Customer_Invoice_Head left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_Customer_INVOICE_HEAD.Against_Sale_No left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')<>''   and convert(date, " & IIf(rbtnSupplyDate.IsChecked, "TSPL_SD_SHIPMENT_HEAD.Supply_Date", "TSPL_Customer_INVOICE_HEAD.Document_Date") & "  ,103)>=convert(date,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103) AND convert(date, " & IIf(rbtnSupplyDate.IsChecked, "TSPL_SD_SHIPMENT_HEAD.Supply_Date", "TSPL_Customer_INVOICE_HEAD.Document_Date") & ",103)<=convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103)   AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT'))  and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 ) and TSPL_SD_SALE_INVOICE_HEAD.Status =1  and TSPL_SD_SALE_INVOICE_DETAIL.TAX" & ii & "_Amt > 0   
            union all 
            
            select 'CAN-SALE' as Trans_Name,TSPL_CANSALE_INVOICE_HEAD.Document_Date,TSPL_CANSALE_INVOICE_HEAD.Customer_Code as Cust_Code,  TSPL_CANSALE_INVOICE_detail.Document_No,TSPL_CANSALE_INVOICE_detail.ItemCode ,0 as Qty ,TSPL_CANSALE_INVOICE_detail.UOM ,0 as Item_Net_Amt,TSPL_CANSALE_INVOICE_detail.Total_Tax_Amt," & IIf(ii = 6, " ''  ", " isnull(TSPL_CANSALE_INVOICE_detail.TAX" & ii & ",'')") & "  AS Tax," & IIf(ii = 6, " 0  ", " TSPL_CANSALE_INVOICE_detail.TAX" & ii & "_Amt ") & " as TAX_Amt," & IIf(ii = 6, " 0  ", " TSPL_CANSALE_INVOICE_detail.TAX" & ii & "_Rate ") & "  as TAX_Rate ,0  as Item_Rate  from TSPL_CANSALE_INVOICE_detail left outer join TSPL_CANSALE_INVOICE_HEAD on TSPL_CANSALE_INVOICE_HEAD .Document_no =TSPL_CANSALE_INVOICE_detail.Document_No left outer join TSPL_Customer_INVOICE_HEAD on TSPL_Customer_INVOICE_HEAD .Against_Sale_No =TSPL_CANSALE_INVOICE_HEAD.Document_No 
            left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_CANSALE_INVOICE_HEAD.Customer_Code where TSPL_CANSALE_INVOICE_HEAD.Posted =1 and TSPL_CANSALE_INVOICE_HEAD.Document_No in (select  TSPL_Customer_INVOICE_HEAD.Against_Sale_No  from TSPL_Customer_Invoice_Head  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')<>''   and convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date ,103)>=convert(date,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103) AND convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date,103)<=convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103)   AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 ) and  " & IIf(ii < 6, "  TSPL_CANSALE_INVOICE_detail.TAX" & ii & "_Amt>0 ", " 2=2") & "
            union all 
            select 'INVOICE-BS' as Trans_Name,TSPL_INVOICE_MASTER_BULKSALE.Document_Date,TSPL_INVOICE_MASTER_BULKSALE.Customer_Code as Cust_Code, TSPL_INVOICE_DETAIL_BULKSALE.Document_No,TSPL_INVOICE_DETAIL_BULKSALE.item_code,0 as Qty,TSPL_INVOICE_DETAIL_BULKSALE.Unit_code as UOM,0 as Item_Net_Amt ,TSPL_INVOICE_DETAIL_BULKSALE.Total_Tax_Amt," & IIf(ii = 6, " ''  ", " isnull(TSPL_INVOICE_DETAIL_BULKSALE.TAX" & ii & ",'') ") & "  AS Tax, " & IIf(ii = 6, " 0  ", "  TSPL_INVOICE_DETAIL_BULKSALE.TAX" & ii & "_Amt ") & " as TAX_Amt, " & IIf(ii = 6, " 0  ", "  TSPL_INVOICE_DETAIL_BULKSALE.TAX" & ii & "_Rate ") & " as TAX_Rate,0 as Item_Rate from TSPL_INVOICE_DETAIL_BULKSALE left outer join TSPL_INVOICE_MASTER_BULKSALE on TSPL_INVOICE_MASTER_BULKSALE .Document_no =TSPL_INVOICE_DETAIL_BULKSALE.Document_No  left outer join TSPL_Customer_INVOICE_HEAD on TSPL_Customer_INVOICE_HEAD .Against_Sale_No =TSPL_INVOICE_MASTER_BULKSALE.Document_No 
            where TSPL_INVOICE_MASTER_BULKSALE.Posted =1 and TSPL_INVOICE_MASTER_BULKSALE.Document_No in (select  TSPL_Customer_INVOICE_HEAD.Against_Sale_No  from TSPL_Customer_Invoice_Head  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_No ,'')<>''   and convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date ,103)>=convert(date,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103) AND convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date,103)<=convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103)   AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 ) and
            " & IIf(ii < 6, " TSPL_INVOICE_DETAIL_BULKSALE.TAX" & ii & "_Amt>0", " 2=2") & "
            union all  
            ---------------- Sale RETURN ,CAN SALE RETURN----------------------- 
            select 'DSR' AS Trans_Name,TSPL_SD_SALE_RETURN_HEAD.Document_Date,TSPL_SD_SALE_RETURN_HEAD.Customer_Code as Cust_Code, TSPL_SD_SALE_RETURN_DETAIL.Document_Code as Document_No,TSPL_SD_SALE_RETURN_DETAIL.Item_Code ,0 as Qty ,TSPL_SD_SALE_RETURN_DETAIL.Unit_code as UOM,0 as Item_Net_Amt ,TSPL_SD_SALE_RETURN_DETAIL.Total_Tax_Amt, isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX" & ii & ",'') AS Tax,  -(TSPL_SD_SALE_RETURN_DETAIL.TAX" & ii & "_Amt) as Tax_Amt,TSPL_SD_SALE_RETURN_DETAIL.TAX" & ii & "_Rate as Tax_Rate,0 as Item_Rate from TSPL_SD_SALE_RETURN_DETAIL left outer join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Document_Code  =TSPL_SD_SALE_RETURN_DETAIL.Document_Code  
            where TSPL_SD_SALE_RETURN_DETAIL.Document_Code in (select  TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No  from TSPL_Customer_Invoice_Head left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')<>''    and convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date ,103)>=convert(date,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103) AND convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date,103)<=convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103)   AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 ) and TSPL_SD_SALE_RETURN_HEAD.Status =1 and TSPL_SD_SALE_RETURN_DETAIL.TAX" & ii & "_Amt>0
            union all 
            select 'BULK-SALE-RE' as Trans_Name, TSPL_SALE_RETURN_MASTER_BULKSALE.Document_Date,TSPL_SALE_RETURN_MASTER_BULKSALE.Customer_Code as Cust_Code, TSPL_SALE_RETURN_DETAIL_BULKSALE.Document_No,TSPL_SALE_RETURN_DETAIL_BULKSALE.item_code,0 as Qty,TSPL_SALE_RETURN_DETAIL_BULKSALE.Unit_code as UOM,0 as Item_Net_Amt ,TSPL_SALE_RETURN_DETAIL_BULKSALE.Total_Tax_Amt," & IIf(ii = 6, " ''  ", "  isnull(TSPL_SALE_RETURN_DETAIL_BULKSALE.TAX" & ii & ",'') ") & " AS Tax, " & IIf(ii = 6, " 0  ", " TSPL_SALE_RETURN_DETAIL_BULKSALE.TAX" & ii & "_Amt") & "  as TAX_Amt, " & IIf(ii = 6, " 0  ", " TSPL_SALE_RETURN_DETAIL_BULKSALE.TAX" & ii & "_Rate ") & "  as TAX_Rate,0 as Item_Rate  from TSPL_SALE_RETURN_DETAIL_BULKSALE left outer join TSPL_SALE_RETURN_MASTER_BULKSALE on TSPL_SALE_RETURN_MASTER_BULKSALE .Document_no =TSPL_SALE_RETURN_DETAIL_BULKSALE.Document_No left outer join TSPL_Customer_INVOICE_HEAD on TSPL_Customer_INVOICE_HEAD .Against_Sale_Return_No =TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No where TSPL_SALE_RETURN_MASTER_BULKSALE.Posted =1 AND TSPL_SALE_RETURN_MASTER_BULKSALE.AGAINST='Bulk Invoice' and  " & IIf(ii < 6, "  TSPL_SALE_RETURN_DETAIL_BULKSALE.TAX" & ii & "_Amt>0 ", " 2=2") & " and TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No in (select  TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No  from TSPL_Customer_Invoice_Head left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.Against_Sale_Return_No ,'')<>''    and convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date ,103)>=convert(date,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103) AND convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date,103)<=convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103)   AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 ) 
            union all 
            ---------------- Scrap Sale Return---------------------- 
            select 'SCRAP-S-R' AS Trans_Name,TSPL_SCRAPSALE_HEAD_RETURN.Return_ship_Date as Document_Date,TSPL_SCRAPSALE_HEAD_RETURN.cust_Code as Cust_Code, TSPL_SCRAPSALE_DETAIL_RETURN.Document_No,TSPL_SCRAPSALE_DETAIL_RETURN.Item_Code ,0 as Qty ,TSPL_SCRAPSALE_DETAIL_RETURN.Unit_code as UOM,0 as Item_Net_Amt ,TSPL_SCRAPSALE_DETAIL_RETURN.TotalTaxAmt as Total_Tax_Amt, isnull(TSPL_SCRAPSALE_DETAIL_RETURN.TAX" & ii & ",'') as Tax, TSPL_SCRAPSALE_DETAIL_RETURN.TAX" & ii & "_Amt as Tax_Amt,TSPL_SCRAPSALE_DETAIL_RETURN.TAX" & ii & "_Rate as TAX_Rate,0 as Item_Rate from TSPL_SCRAPSALE_DETAIL_RETURN left outer join TSPL_SCRAPSALE_HEAD_RETURN on TSPL_SCRAPSALE_HEAD_RETURN .Document_No =TSPL_SCRAPSALE_DETAIL_RETURN.Document_No  where TSPL_SCRAPSALE_DETAIL_RETURN.Document_No in (select  TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn  from TSPL_Customer_Invoice_Head left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrapReturn ,'')<>'' and convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date ,103)>=convert(date,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103) AND convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date,103)<=convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103)   AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 ) and TSPL_SCRAPSALE_HEAD_RETURN.ispost =1   and TSPL_SCRAPSALE_DETAIL_RETURN.TAX" & ii & "_Amt>0
            union all  
            ---------------- Scrap Sale ----------------- 
            select 'SCRAP-SALE' AS Trans_Name,TSPL_SCRAPINVOICE_HEAD.shipment_Date as Document_Date,TSPL_SCRAPINVOICE_HEAD.cust_Code as Cust_Code, TSPL_SCRAPINVOICE_DETAIL.invoice_No AS Document_No ,TSPL_SCRAPINVOICE_DETAIL.Item_Code ,0 as Qty ,TSPL_SCRAPINVOICE_DETAIL.Unit_code as UOM,0 as Item_Net_Amt ,TSPL_SCRAPINVOICE_DETAIL.TotalTaxAmt as Total_Tax_Amt, isnull(TSPL_SCRAPINVOICE_DETAIL.TAX" & ii & ",'') as Tax, TSPL_SCRAPINVOICE_DETAIL.TAX" & ii & "_Amt as TAX_Amt,TSPL_SCRAPINVOICE_DETAIL.TAX" & ii & "_Rate as TAX_Rate,0 as Item_Rate from TSPL_SCRAPINVOICE_DETAIL left outer join TSPL_SCRAPINVOICE_HEAD on TSPL_SCRAPINVOICE_HEAD.invoice_No =TSPL_SCRAPINVOICE_DETAIL.invoice_No  where TSPL_SCRAPINVOICE_DETAIL.invoice_No in (select  TSPL_Customer_INVOICE_HEAD.AgainstScrap  from TSPL_Customer_Invoice_Head left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.AgainstScrap ,'')<>'' and convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date ,103)>=convert(date,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103) AND convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date,103)<=convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103)   AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 ) and TSPL_SCRAPINVOICE_HEAD.ispost =1  and TSPL_SCRAPINVOICE_DETAIL.TAX" & ii & "_Amt>0
            union all 
            ---------------- mcc Material Sale RETURN-------------------------------- 
            select 'M-Material-R' as Trans_Name, TSPL_SD_SALE_RETURN_HEAD.Document_Date ,TSPL_SD_SALE_RETURN_HEAD.Customer_Code as Cust_Code, TSPL_SD_SALE_RETURN_DETAIL.Document_Code,TSPL_SD_SALE_RETURN_DETAIL.Item_Code ,0 as Qty ,TSPL_SD_SALE_RETURN_DETAIL.Unit_code as UOM,0 as Item_Net_Amt ,TSPL_SD_SALE_RETURN_DETAIL.Total_Tax_Amt,  isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX" & ii & ",'') as Tax,-(TSPL_SD_SALE_RETURN_DETAIL.TAX" & ii & "_Amt) as Tax_Amt,TSPL_SD_SALE_RETURN_DETAIL.TAX" & ii & "_Rate as TAX_Rate,0 as Item_Rate from TSPL_SD_SALE_RETURN_DETAIL  left outer join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Document_Code  =TSPL_SD_SALE_RETURN_DETAIL.Document_Code  where TSPL_SD_SALE_RETURN_DETAIL.Document_Code in (select  TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return  from TSPL_Customer_Invoice_Head left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.Against_MCC_Material_Sale_Return ,'')<>'' and convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date ,103)>=convert(date,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103) AND convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date,103)<=convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103)   AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 ) and TSPL_SD_SALE_RETURN_HEAD.Status =1 AND TSPL_SD_SALE_RETURN_HEAD.Trans_Type='MCC' and TSPL_SD_SALE_RETURN_DETAIL.TAX" & ii & "_Amt > 0
            union all 
            ---------------- Security Receipt--------------------------
            select 'AR-INVOICE' as Trans_Name,TSPL_Customer_INVOICE_HEAD.Document_Date,TSPL_Customer_INVOICE_HEAD.Customer_Code as Cust_Code, TSPL_Customer_Invoice_Head.Against_Security_Receipt_No as Document_No,'' AS Item_Code ,0 AS Qty ,'' as UOM,0 as Item_Net_Amt ,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end *  (isnull(TSPL_Customer_Invoice_Detail.Total_Tax  ,0)) as Total_Tax_Amt,TSPL_Customer_Invoice_Detail.TAX" & ii & " as Tax,case when TSPL_Customer_Invoice_Head.Document_Type ='C' then -1 else 1 end *  isnull(TSPL_Customer_Invoice_Detail.TAX" & ii & "_Amt,0) as Tax_Amt ,TSPL_Customer_Invoice_Detail.TAX" & ii & "_Rate as Tax_Rate,0 as Item_Rate from TSPL_Customer_Invoice_Detail left outer join TSPL_Customer_INVOICE_HEAD on TSPL_Customer_INVOICE_HEAD .Document_No =TSPL_Customer_Invoice_Detail.Document_No  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.Against_Security_Receipt_No ,'')<>''  and TSPL_Customer_INVOICE_HEAD.Status =1    and convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date ,103)>=convert(date,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103) AND convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date,103)<=convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103)   AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0   and TSPL_Customer_Invoice_Detail.TAX" & ii & "_Amt >0
            union all 
             ---------------- Transfer --------------------------
           select 'STO-TRANSFER' AS Trans_Name, TSPL_TRANSFER_ORDER_HEAD.Document_Date,Location as Cust_Code, TSPL_TRANSFER_ORDER_DETAIL.Document_No,TSPL_TRANSFER_ORDER_DETAIL.Item_Code ,0 as Qty,'' as UOM,  0 as Item_Net_Amt ,TSPL_TRANSFER_ORDER_DETAIL.Total_Tax_Amt,  isnull(TSPL_TRANSFER_ORDER_DETAIL.TAX" & ii & ",'') as Tax,  TSPL_TRANSFER_ORDER_DETAIL.TAX" & ii & "_Amt as TAX_Amt ,TSPL_TRANSFER_ORDER_DETAIL.TAX" & ii & "_Rate as TAX_Rate, 0 as Item_Rate  from TSPL_TRANSFER_ORDER_DETAIL  left outer join TSPL_TRANSFER_ORDER_HEAD on TSPL_TRANSFER_ORDER_HEAD.Document_No =TSPL_TRANSFER_ORDER_DETAIL.Document_No LEFT join TSPL_LOCATION_MASTER as  From_LOC ON TSPL_TRANSFER_ORDER_HEAD.FROM_LOCATION=From_LOC.LOCATION_CODE where convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_Date ,103)>=convert(date,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103) AND convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103)<=convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103) and  TSPL_TRANSFER_ORDER_HEAD.status = 1 and TSPL_TRANSFER_ORDER_DETAIL.TAX" & ii & "_Amt>0  and TSPL_TRANSFER_ORDER_HEAD.transfer_type = 'O'   
           union all 
                   ---------------- Transfer Return--------------------------
           select 'STO-TRANS-R' AS Trans_Name, TSPL_TRANSFER_ORDER_HEAD.Document_Date,Location as Cust_Code, TSPL_TRANSFER_ORDER_DETAIL.Document_No,TSPL_TRANSFER_ORDER_DETAIL.Item_Code ,0 as Qty,'' as UOM,  0 as Item_Net_Amt ,-(TSPL_TRANSFER_ORDER_DETAIL.Total_Tax_Amt)Total_Tax_Amt,  isnull(TSPL_TRANSFER_ORDER_DETAIL.TAX" & ii & ",'') as Tax,  -(TSPL_TRANSFER_ORDER_DETAIL.TAX" & ii & "_Amt) as TAX_Amt ,TSPL_TRANSFER_ORDER_DETAIL.TAX" & ii & "_Rate as TAX_Rate, 0 as Item_Rate from TSPL_TRANSFER_ORDER_DETAIL  left outer join TSPL_TRANSFER_ORDER_HEAD on TSPL_TRANSFER_ORDER_HEAD.Document_No =TSPL_TRANSFER_ORDER_DETAIL.Document_No LEFT join TSPL_LOCATION_MASTER as  From_LOC ON TSPL_TRANSFER_ORDER_HEAD.FROM_LOCATION=From_LOC.LOCATION_CODE where convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_Date ,103)>=convert(date,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103) AND convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103)<=convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103) and  TSPL_TRANSFER_ORDER_HEAD.status = 1 and TSPL_TRANSFER_ORDER_DETAIL.TAX" & ii & "_Amt>0  and TSPL_TRANSFER_ORDER_HEAD.transfer_type = 'T' "
            Next

            BaseQuery = " select Trans_Name,Document_Date,Cust_Code, Document_No,xx.Item_Code,UOM,(Qty)Qty, (Item_Net_Amt)Item_Net_Amt,Tax,Tax_Rate,Tax_Amt, Item_Rate from ( " & qry & " " & BaseQuery & " )xx  where 2= 2 " & whrcls & " ) xxx  left outer join TSPL_TAX_MASTER ON  TSPL_TAX_MASTER.Tax_Code = XXX.TAX "
            Dim TaxDesc As String = ""
            Dim TaxAmount As String = ""
            qry = ""

            dtTax = clsDBFuncationality.GetDataTable("SELECT * FROM ( select Tax_Code as Tax,Tax_Code_Desc,type,case  when type = 'M' then 1 when type = 'K' then 2 when type = 'SGST' then 3 when type = 'CGST' then 4  when type = 'IGST' then 5 end as Sequence_No from TSPL_TAX_MASTER where Type in ('SGST','CGST','IGST','M','K') )X Order by Sequence_No ")

            For ii As Integer = 0 To dtTax.Rows.Count - 1
                If isPrint Then
                    TaxDesc += " TaxAmount_" + clsCommon.myCstr(ii + 1) + " ,Tax_" + clsCommon.myCstr(ii + 1) + ", "
                    qry += " ,'" & dtTax.Rows(ii)("Tax_Code_Desc") & "' as  Tax_" + clsCommon.myCstr(ii + 1) + " "
                    qry += " ,sum(Case When xxx.Type ='" & dtTax.Rows(ii)("Type") & "' then 1 else 0 end *  (isnull(Tax_Amt ,0))) as TaxAmount_" + clsCommon.myCstr(ii + 1) + " "
                    TaxAmount += ",max(Tax_" + clsCommon.myCstr(ii + 1) + ") as  Tax_" + clsCommon.myCstr(ii + 1) + " , sum(TaxAmount_" + clsCommon.myCstr(ii + 1) + ") as TaxAmount_" + clsCommon.myCstr(ii + 1) + ""
                Else
                    TaxDesc += "[" & dtTax.Rows(ii)("Tax_Code_Desc") & " Amount] ,"
                    qry += " ,sum(case when xxx.Tax ='" & dtTax.Rows(ii)("Tax") & "' then 1 else 0 end *  (isnull(Tax_Amt ,0))) as [" & dtTax.Rows(ii)("Tax_Code_Desc") & " Amount]"
                    TaxAmount += " sum([" & dtTax.Rows(ii)("Tax_Code_Desc") & " Amount])[" & dtTax.Rows(ii)("Tax_Code_Desc") & " Amount] ,"
                End If
            Next
            BaseQuery = " select xxx.*,Type,Is_TCS from (  " & BaseQuery & "  )xxx "
            qry += ",Type "

            If isPrint Then
                qry = " select   Cust_Code,max(Customer_Name)Customer_Name,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "' as FromDate,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' as ToDate,max(Comp_Name)Comp_Name,max(Add1) as Add1,Item_Code,max(Short_Description)Short_Description,sum(Total_Qty) As Total_Qty,max(UOM) AS UOM,max(Item_Rate)Item_Rate 
               " & TaxAmount & ",max(Tax_6) as  Tax_6 ,sum(TaxAmount_6)TaxAmount_6 ,sum(Item_Net_Amt)Item_Net_Amt,case when max(IsTaxable) = 1 then 'Product' else 'Milk' end as IsProduct  from (
                Select  TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1,IsTaxable,TSPL_CUSTOMER_MASTER.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name, FINAL.Item_Code,TSPL_ITEM_MASTER.Short_Description,I.UOM_Code AS UOM, isnull((Qty * isnull((TSPL_ITEM_UOM_DETAIL.Conversion_Factor),1)) /(I.Conversion_Factor),0) As Total_Qty,Item_Net_Amt,   isnull(Tax_Rate,0)Tax_Rate,(Item_Net_Amt - Total_Tax_Amt) AS Taxable_Amt, " & TaxDesc & "Tax_6,TaxAmount_6,Total_Tax_Amt,kkfAmt,MandiAmt,final.Item_Rate  from (  select  xxx.Cust_Code,xxx.Item_Code,UOM,SUM(Qty)Qty, sum(Item_Net_Amt)Item_Net_Amt, sum(Tax_Amt)Total_Tax_Amt,max(item_rate)item_rate,  max( case when (xxx.Tax ='IGST' and isnull(Tax_Rate ,0) > 0)  then isnull(Tax_Rate ,0) else (case when xxx.Tax ='CGST' then 2 * isnull(Tax_Rate ,0) end ) end) as Tax_Rate,sum(case when xxx.Type ='K' then 1 else 0 end *  (isnull(Tax_Amt ,0))) as kkfAmt, sum(case when xxx.Type ='M' then 1 else 0 end *  (isnull(Tax_Amt ,0))) as MandiAmt  " & qry & ",sum(case when Is_TCS = 'Y' then 1 else 0 end *  (isnull(Tax_Amt ,0))) as TaxAmount_6, 'TCS'  as Tax_6 from ( "
                groupby = " Cust_Code "
            Else
                If rbtnDetail.IsChecked Then
                    groupby = " Document_No,Document_Date,Cust_Code "
                    qry2 = " select convert(varchar, Document_Date,103) Document_Date,Document_No,Cust_Code,max(Customer_Name)Customer_Name,"
                ElseIf rbtnSummary.IsChecked Then
                    groupby = " Cust_Code "
                    qry2 = " select Cust_Code,max(Customer_Name)Customer_Name,"
                End If
                qry = " " & qry2 & " Item_Code,max(Short_Description)Short_Description,sum(Total_Qty) As Total_Qty,max(UOM) AS UOM,max(Item_Rate)Item_Rate," & TaxAmount & " sum([TCS Amount])[TCS Amount],sum(Item_Net_Amt)Item_Net_Amt
              from ( select Document_Date,Document_No, TSPL_CUSTOMER_MASTER.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name,FINAL.Item_Code,TSPL_ITEM_MASTER.Short_Description,I.UOM_Code AS UOM, isnull((Qty * isnull((TSPL_ITEM_UOM_DETAIL.Conversion_Factor),1)) /(I.Conversion_Factor),0) As Total_Qty,Item_Net_Amt,   isnull(Tax_Rate,0)Tax_Rate,(Item_Net_Amt - Total_Tax_Amt) AS Taxable_Amt, " & TaxDesc & "[TCS Amount], Total_Tax_Amt,kkfAmt,MandiAmt ,final.Item_Rate from ( select xxx.Item_Code,xxx.Document_Date,xxx.Document_No,xxx.Cust_Code,UOM,SUM(Qty)Qty, sum(Item_Net_Amt)Item_Net_Amt, sum(Tax_Amt)Total_Tax_Amt,max(Item_Rate)Item_Rate,  max( case when (xxx.Tax ='IGST' and isnull(Tax_Rate ,0) > 0)  then isnull(Tax_Rate ,0) else (case when xxx.Tax ='CGST' then 2 * isnull(Tax_Rate ,0) end ) end) as Tax_Rate,sum(case when xxx.Type ='K' then 1 else 0 end *  (isnull(Tax_Amt ,0))) as kkfAmt, sum(case when xxx.Type ='M' then 1 else 0 end *  (isnull(Tax_Amt ,0))) as MandiAmt  " & qry & ",sum(case when Is_TCS = 'Y' then 1 else 0 end *  (isnull(Tax_Amt ,0))) as [TCS Amount] from ( "

            End If

            BaseQuery = "with CTERawData as ( " & qry & " " & BaseQuery & " GROUP BY xxx.Document_No,xxx.Document_Date,xxx.Cust_Code,xxx.Item_Code,UOM,Type ) FINAL left outer join tspl_item_master on TSPL_ITEM_MASTER.Item_Code = FINAL.Item_Code  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = final.Cust_Code LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON TSPL_ITEM_UOM_DETAIL.Item_Code = FINAL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code = FINAL.UOM	LEFT JOIN  ( select item_code,uom_code,conversion_factor,UOM_Description from  TSPL_ITEM_UOM_DETAIL where Report_UOM = 1 ) as  I ON FINAL.Item_Code = I.item_code left outer join TSPL_COMPANY_MASTER on 2 =2  ) xxxxFinal group by " & groupby & ",Item_Code ) select CTERawData.* from CTERawData order by  " & IIf(rbtnDetail.IsChecked AndAlso Not isPrint, "Document_Date,", "") & " Cust_Code"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(BaseQuery)

            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterView.Refresh()
            gv1.GroupDescriptors.Clear()
            gv1.EnableFiltering = True
            gv1.MasterTemplate.SummaryRowsBottom.Clear()

            If dt.Rows.Count > 0 Then
                gv1.DataSource = dt
                gv1.BestFitColumns()
                SetGridFormation()
                ReStoreGridLayout()
                gv1.MasterTemplate.AutoExpandGroups = True
                EnableDisableControls(False)
                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.BestFitColumns()

                If isPrint Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    If BtnStcRegisterItemWiseSummary.IsChecked = True Then
                        frmCRV.funreport(CrystalReportFolder.SalesReport, dt, "CrptSTSRegister-ItemWiseSummary", "STS Register Item Wise Summary")
                    ElseIf BtnBillWiseSaleOfMilkSummary.IsChecked = True Then
                        frmCRV.funreport(CrystalReportFolder.SalesReport, dt, "CrptBillWiseSaleOfMilkSummary", "Bill Wise Sale Of Milk Summary")
                    ElseIf BtnProductWiseSaleQuantity.IsChecked = True Then
                        frmCRV.funreport(CrystalReportFolder.SalesReport, dt, "CrptProductWiseSaleQuantity", "Product Wise Sale Quantity")
                    ElseIf BtnBillWiseSaleOfMilk.IsChecked = True Then
                        frmCRV.funreport(CrystalReportFolder.SalesReport, dt, "CrptBillWiseSaleOfMilk", "Bill Wise Sale Of Milk")
                    ElseIf BtnPartySaleMilkProductt.IsChecked = True Then
                        frmCRV.funreport(CrystalReportFolder.SalesReport, dt, "CrptPartySalesMilkandProducts", "Party Sales Milk and Products")
                    ElseIf BtnProductSalesSummary.IsChecked = True Then
                        frmCRV.funreport(CrystalReportFolder.SalesReport, dt, "CrptProductSaleSummaryTaxableNonTaxable", "Product Sale Summary Taxable NonTaxable")
                    ElseIf BtnMilkStcSummary.IsChecked = True Then
                        frmCRV.funreport(CrystalReportFolder.SalesReport, dt, "CrptMilkStcSummary", "Milk Stc Summary")
                    ElseIf BtnStcRegisterPartyandItemWiseSummary.IsChecked = True Then
                        frmCRV.funreport(CrystalReportFolder.SalesReport, dt, "CrptSTSRegister-ItemWiseSummaryPartyWise", "STS Register-Item Wise Summary Party Wise")
                    Else
                        frmCRV.funreport(CrystalReportFolder.SalesReport, dt, "rptCustItemWiseSale", "Customer Item Wise Sale")
                    End If
                    frmCRV = Nothing
                    End If
                Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub
    'Sub reportSTCPartyRegister(ByVal Print As Boolean)
    '    Try
    '        Dim qry As String = "
    '            Select  TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1,IsTaxable,TSPL_CUSTOMER_MASTER.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name, FINAL.Item_Code,TSPL_ITEM_MASTER.Short_Description,I.UOM_Code + '-' + I.UOM_Description AS UOM, isnull((Qty * isnull((TSPL_ITEM_UOM_DETAIL.Conversion_Factor),1)) /(I.Conversion_Factor),0) As Total_Qty,Item_Net_Amt,   isnull(Tax_Rate,0)Tax_Rate,(Item_Net_Amt - Total_Tax_Amt) AS Taxable_Amt,  TaxAmount_1 ,Tax_1,  TaxAmount_2 ,Tax_2,  TaxAmount_3 ,Tax_3,  TaxAmount_4 ,Tax_4,  TaxAmount_5 ,Tax_5,Tax_6,TaxAmount_6,Total_Tax_Amt,kkfAmt,MandiAmt,final.Item_Rate  from (  select  xxx.Cust_Code,xxx.Item_Code,UOM,SUM(Qty)Qty, sum(Item_Net_Amt)Item_Net_Amt, sum(Tax_Amt)Total_Tax_Amt,max(item_rate)item_rate,  max( case when (xxx.Tax ='IGST' and isnull(Tax_Rate ,0) > 0)  then isnull(Tax_Rate ,0) else (case when xxx.Tax ='CGST' then 2 * isnull(Tax_Rate ,0) end ) end) as Tax_Rate,sum(case when xxx.Type ='K' then 1 else 0 end *  (isnull(Tax_Amt ,0))) as kkfAmt, sum(case when xxx.Type ='M' then 1 else 0 end *  (isnull(Tax_Amt ,0))) as MandiAmt   ,'MANDITAX' as  Tax_1  ,sum(Case When xxx.Type ='M' then 1 else 0 end *  (isnull(Tax_Amt ,0))) as TaxAmount_1  ,'KKF' as  Tax_2  ,sum(Case When xxx.Type ='K' then 1 else 0 end *  (isnull(Tax_Amt ,0))) as TaxAmount_2  ,'State Goods Service Tax' as  Tax_3  ,sum(Case When xxx.Type ='SGST' then 1 else 0 end *  (isnull(Tax_Amt ,0))) as TaxAmount_3  ,'Central Goods Serivce Tax' as  Tax_4  ,sum(Case When xxx.Type ='CGST' then 1 else 0 end *  (isnull(Tax_Amt ,0))) as TaxAmount_4  ,'Integrated Goods and Services Tax' as  Tax_5  ,sum(Case When xxx.Type ='IGST' then 1 else 0 end *  (isnull(Tax_Amt ,0))) as TaxAmount_5 ,Type ,sum(case when Is_TCS = 'Y' then 1 else 0 end *  (isnull(Tax_Amt ,0))) as TaxAmount_6, 'TCS'  as Tax_6 from (   select xxx.*,Type,Is_TCS from (   select Trans_Name,Document_Date,Cust_Code, Document_No,xx.Item_Code,UOM,(Qty)Qty, (Item_Net_Amt)Item_Net_Amt,Tax,Tax_Rate,Tax_Amt, Item_Rate from ( ---------------- VCGL---------------------------- 
    '        select 'VCGL' as Trans_Name,TSPL_VCGL_Head.Document_Date,'' as Cust_Code, TSPL_VCGL_Detail.Document_No,'' as Item_Code ,0 as Qty ,'' as UOM,case when TSPL_VCGL_Detail.Row_Type ='Customer' then TSPL_VCGL_Detail.Dr_Amount-TSPL_VCGL_Detail.Cr_Amount else 0 end as Item_Net_Amt ,0 as Total_Tax_Amt, '' as Tax,  0 as Tax_Amt ,0 as Tax_Rate,0 as Item_Rate from TSPL_VCGL_Detail left outer join TSPL_VCGL_Head on TSPL_VCGL_Head.Document_No  =TSPL_VCGL_Detail.Document_No  where TSPL_VCGL_Detail.Document_No in (select  TSPL_Customer_INVOICE_HEAD.Against_VCGL  from TSPL_Customer_Invoice_Head
    '        left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Customer_Invoice_Head.Customer_Code where isnull(TSPL_Customer_INVOICE_HEAD.Against_VCGL ,'')<>''     and convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date ,103)>=convert(date,'01/Nov/2023',103) AND convert(date,TSPL_Customer_INVOICE_HEAD.Document_Date,103)<=convert(date,'20/Dec/2024',103)   AND TSPL_Customer_Invoice_Head.Against_Sale_No NOT IN (select  Document_Code  from TSPL_SD_SALE_INVOICE_HEAD where Document_Type in ('EX','MT')) and TSPL_CUSTOMER_MASTER.GST_Registered =1 and isnull(TSPL_CUSTOMER_MASTER.GSTNO ,'')<>'' and isnull(TSPL_Customer_Invoice_Head.Total_Tax,0) >0 ) and TSPL_VCGL_Head.Status =1  
    '        union all 
    '         ---------------- Transfer --------------------------
    '        select  'STO-TRANSFER' as Trans_Name,TSPL_TRANSFER_ORDER_HEAD.Document_Date,Location as Cust_Code,TSPL_TRANSFER_ORDER_DETAIL.Document_No,TSPL_TRANSFER_ORDER_DETAIL.Item_Code ,TSPL_TRANSFER_ORDER_DETAIL.Out_Qty as Qty,TSPL_TRANSFER_ORDER_DETAIL.Unit_code as UOM,  TSPL_TRANSFER_ORDER_DETAIL.Item_Net_Amt ,TSPL_TRANSFER_ORDER_DETAIL.Total_Tax_Amt,  '' as Tax,  0 as Tax_Amt ,0 as Tax_Rate,convert(decimal(18,2), TSPL_TRANSFER_ORDER_DETAIL.Item_Cost) as  Item_Rate from TSPL_TRANSFER_ORDER_DETAIL  left outer join TSPL_TRANSFER_ORDER_HEAD on TSPL_TRANSFER_ORDER_HEAD.Document_No =TSPL_TRANSFER_ORDER_DETAIL.Document_No LEFT join TSPL_LOCATION_MASTER as  From_LOC ON TSPL_TRANSFER_ORDER_HEAD.FROM_LOCATION=From_LOC.LOCATION_CODE where convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_Date ,103)>=convert(date,'01/Nov/2023',103) AND convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103)<=convert(date,'20/Dec/2024',103)  and  TSPL_TRANSFER_ORDER_HEAD.status = 1  and TSPL_TRANSFER_ORDER_HEAD.transfer_type = 'O'
    '        union all
    '        ---------------- Transfer Return--------------------------
    '        select  'STO-TRANS-R' as Trans_Name,TSPL_TRANSFER_ORDER_HEAD.Document_Date,Location as Cust_Code,TSPL_TRANSFER_ORDER_DETAIL.Document_No,TSPL_TRANSFER_ORDER_DETAIL.Item_Code ,-(TSPL_TRANSFER_ORDER_DETAIL.Out_Qty) as Qty,TSPL_TRANSFER_ORDER_DETAIL.Unit_code as UOM,  -(TSPL_TRANSFER_ORDER_DETAIL.Item_Net_Amt)Item_Net_Amt ,TSPL_TRANSFER_ORDER_DETAIL.Total_Tax_Amt,  '' as Tax,  0 as Tax_Amt ,0 as Tax_Rate ,convert(decimal(18,2), TSPL_TRANSFER_ORDER_DETAIL.Item_Cost) as  Item_Rate from TSPL_TRANSFER_ORDER_DETAIL  left outer join TSPL_TRANSFER_ORDER_HEAD on TSPL_TRANSFER_ORDER_HEAD.Document_No =TSPL_TRANSFER_ORDER_DETAIL.Document_No LEFT join TSPL_LOCATION_MASTER as  From_LOC ON TSPL_TRANSFER_ORDER_HEAD.FROM_LOCATION=From_LOC.LOCATION_CODE where convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_Date ,103)>=convert(date,'01/Nov/2023',103) AND convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103)<=convert(date,'20/Dec/2024',103)  and  TSPL_TRANSFER_ORDER_HEAD.status = 1 and TSPL_TRANSFER_ORDER_HEAD.transfer_type = 'T' 
    '        )xx  where 2= 2  ) xxx  left outer join TSPL_TAX_MASTER ON  TSPL_TAX_MASTER.Tax_Code = XXX.TAX   )xxx  GROUP BY xxx.Document_No,xxx.Document_Date,xxx.Cust_Code,xxx.Item_Code,UOM,Type ) FINAL left outer join tspl_item_master on TSPL_ITEM_MASTER.Item_Code = FINAL.Item_Code  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = final.Cust_Code LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON TSPL_ITEM_UOM_DETAIL.Item_Code = FINAL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code = FINAL.UOM	LEFT JOIN  ( select item_code,uom_code,conversion_factor,UOM_Description from  TSPL_ITEM_UOM_DETAIL where Report_UOM = 1 ) as  I ON FINAL.Item_Code = I.item_code left outer join TSPL_COMPANY_MASTER on 2 =2   where tspl_item_master.Item_Type = 'F'  "

    '        If BtnStcRegisterPartyandItemWiseSummary.IsChecked = True Then
    '            qry = qry
    '        ElseIf BtnStcRegisterItemWiseSummary.IsChecked = True Then
    '            qry = qry + ""
    '        End If

    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

    '        If dt.Rows.Count > 0 Then
    '            If Print = False Then
    '                gv1.DataSource = Nothing
    '                gv1.Rows.Clear()
    '                gv1.Columns.Clear()
    '                gv1.GroupDescriptors.Clear()
    '                gv1.MasterView.Refresh()
    '                gv1.GroupDescriptors.Clear()
    '                gv1.EnableFiltering = True
    '                gv1.MasterTemplate.SummaryRowsBottom.Clear()
    '                gv1.DataSource = dt
    '                gv1.BestFitColumns()
    '                'SetGridFormation()
    '                ReStoreGridLayout()
    '                gv1.MasterTemplate.AutoExpandGroups = True
    '                EnableDisableControls(False)
    '                RadPageView1.SelectedPage = RadPageViewPage2
    '                gv1.BestFitColumns()
    '            Else
    '                Dim frmCRV As New frmCrystalReportViewer()
    '                If BtnStcRegisterPartyandItemWiseSummary.IsChecked = True Then
    '                    frmCRV.funreport(CrystalReportFolder.SalesReport, dt, "CrptSTSRegister-ItemWiseSummaryPartyWise", "STS Register-Item Wise Summary Party Wise")
    '                ElseIf BtnStcRegisterItemWiseSummary.IsChecked = True Then
    '                    frmCRV.funreport(CrystalReportFolder.SalesReport, dt, "CrptSTSRegister-ItemWiseSummary", "STS Register Item Wise Summary")
    '                End If
    '            End If
    '        Else
    '            clsCommon.MyMessageBoxShow(Me, "No Data Found.", Me.Text)
    '            Exit Sub
    '        End If


    '    Catch ex As Exception

    '    End Try
    'End Sub
    Sub SetGridFormation()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = True
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
            gv1.Columns(ii).Width = 80
        Next

        gv1.ShowGroupPanel = False
        If Not isPrint Then
            If rbtnDetail.IsChecked Then
                gv1.Columns("Document_Date").HeaderText = "Document Date"
                gv1.Columns("Document_No").HeaderText = "Document No"
            End If
        End If

        gv1.Columns("Cust_Code").HeaderText = "Customer Code"
        gv1.Columns("Cust_Code").IsVisible = False
        gv1.Columns("Customer_Name").HeaderText = "Customer Name"

        gv1.Columns("Short_Description").HeaderText = "Item Name"
        gv1.Columns("Short_Description").Width = 110
        gv1.Columns("Total_Qty").HeaderText = "Quantity"
        gv1.Columns("Item_Net_Amt").FormatString = "{0:n2}"
        gv1.Columns("Item_Net_Amt").HeaderText = "Gross Amount"
        gv1.Columns("Item_Rate").HeaderText = "Item Rate"
        gv1.Columns("Item_Code").HeaderText = "Item Code"
        gv1.Columns("Item_Code").IsVisible = False
        gv1.Columns("Item_Rate").FormatString = "{0:n2}"

        If isPrint Then
            gv1.Columns("Tax_1").IsVisible = False
            gv1.Columns("Tax_2").IsVisible = False
            gv1.Columns("Tax_3").IsVisible = False
            gv1.Columns("Tax_4").IsVisible = False
            gv1.Columns("Tax_5").IsVisible = False
            gv1.Columns("Tax_6").IsVisible = False
            gv1.Columns("IsProduct").IsVisible = False
            gv1.Columns("Comp_Name").IsVisible = False
            gv1.Columns("FromDate").IsVisible = False
            gv1.Columns("ToDate").IsVisible = False
            For ii As Integer = 0 To dtTax.Rows.Count - 1
                gv1.Columns("TaxAmount_" & clsCommon.myCstr(ii + 1) & "").HeaderText = dtTax.Rows(ii)("Tax_Code_Desc")
            Next
            gv1.Columns("TaxAmount_6").HeaderText = "TCS Amount"
        End If

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim j As Integer = 0
        If isPrint Then
            j = 7
        Else
            If rbtnDetail.IsChecked Then
                j = 6
            Else
                j = 4
            End If
        End If

        For ii As Integer = j To gv1.Columns.Count - 1
            If clsCommon.CompairString(gv1.Columns(ii).Name, "UOM") = CompairStringResult.Equal OrElse clsCommon.CompairString(gv1.Columns(ii).Name, "Item_Rate") = CompairStringResult.Equal Then
            Else
                summaryRowItem.Add(New GridViewSummaryItem(gv1.Columns(ii).Name, "{0:F2}", GridAggregateFunction.Sum))
            End If
        Next
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(MyBase.Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer = 0
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
                obj = Nothing
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID + gv1.VarID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", Me.Text)
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub

    Private Sub btnPDF_Click(sender As Object, e As EventArgs) Handles btnPDF.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Date : " & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "  To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy"))
                If txtCustomer.arrValueMember IsNot Nothing Then
                    arrHeader.Add("Customer Code : " & clsCommon.GetMulcallString(txtCustomer.arrValueMember) & "   Customer Name :" & clsCommon.GetMulcallString(txtCustomer.arrDispalyMember) & "")
                End If

                If txtItem.arrValueMember IsNot Nothing Then
                    arrHeader.Add("Item Code : " & clsCommon.GetMulcallString(txtItem.arrValueMember) & "   Item Name :" & clsCommon.GetMulcallString(txtItem.arrDispalyMember) & "")
                End If
                clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        CancelPressed()
    End Sub
    Sub CancelPressed()
        Me.Close()
    End Sub

    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_code='" & clsUserMgtCode.rptCustItemWiseSaleReport & "'"))
                arrHeader.Add("Date : " & clsCommon.myCDate(txtFromDate.Value) + "  To " + clsCommon.myCDate(txtToDate.Value))

                If txtCustomer.arrValueMember IsNot Nothing Then
                    arrHeader.Add("Customer Code : " & clsCommon.GetMulcallString(txtCustomer.arrValueMember) & "   Customer Name :" & clsCommon.GetMulcallString(txtCustomer.arrDispalyMember) & "")
                End If

                If txtItem.arrValueMember IsNot Nothing Then
                    arrHeader.Add("Item Code : " & clsCommon.GetMulcallString(txtItem.arrValueMember) & "   Item Name :" & clsCommon.GetMulcallString(txtItem.arrDispalyMember) & "")
                End If
                clsCommon.MyExportToExcel(Me.Text, gv1, arrHeader, Me.Text)
                clsCommon.MyMessageBoxShow(Me, "Export Successfully", Me.Text)
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        'If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "BKN") = CompairStringResult.Equal Then
        '    reportSTCPartyRegister(True)
        'Else
        If BtnProductWiseSaleQuantity.IsChecked Then
            ProductWiseSale(True)
        ElseIf BtnBillWiseSaleOfMilkSummary.IsChecked Then
            BillWisesaleSummary(True)
        ElseIf BtnPartySaleMilkProductt.IsChecked Then
            PartySaleMilkProductt(True)
        ElseIf BtnBillWiseSaleOfMilk.IsChecked Then
            BillwiseSaleOfMilk(True)
        ElseIf BtnProductSalesSummary.IsChecked Then
            Productsalesummarytaxablenontaxable(True)
        ElseIf BtnMilkStcSummary.IsChecked Then
            MilkStcSummary(True)
        ElseIf BtnStcRegisterItemWiseSummary.IsChecked Then
            STCRegisterItemwiseSummarytotal(True)
        ElseIf BtnStcRegisterPartyandItemWiseSummary.IsChecked Then
            STCRegisterItemwiseSummaryPartyWise(True)
        ElseIf BtnTransportationCharges.IsChecked Then
            TransportationCharges(True)
        ElseIf BtnTcsSummary.IsChecked Then
            TcsSummary(True)
        ElseIf BtnGheeReport.IsChecked Then
            GheeReport(True)
        ElseIf BtnRouteWiseSale.IsChecked Then
            RouteWiseSale(True)
        ElseIf BtnCreditPartyWiseSaleAmount.IsChecked Then
            CreditPartyWiseSaleAmount(True)
        Else
            isPrint = True
            LoadData()
            isPrint = False
        End If

        'End If
    End Sub

    Private Sub txtTransaction__My_Click(sender As Object, e As EventArgs) Handles txtTransaction._My_Click
        Try
            Dim qry As String = "SELECT Program_Code As Code,Program_Name as Name from TSPL_PROGRAM_MASTER where Program_Code in ( 'AR-INVOICE','INVOICE-BS','BULK-SALE-RE','CAN-SALE','VCGL','M-Material-R','STO-TRANSFER','STO-TRANS-R')
       UNION ALL " & Environment.NewLine & " Select 'DS' as Code,'Dairy Sale' as Name " &
                  " Union All " & Environment.NewLine &
                  " select 'DSR','Dairy Sale Return' as Name  Union All " & Environment.NewLine & "
        Select 'SCRAP-SALE' As Code,'Scrap Sale' as Name  Union All " & Environment.NewLine & "
                  Select 'SCRAP-S-R' As Code,'Scrap Sale Return' as Name  "

            txtTransaction.arrValueMember = clsCommon.ShowMultipleSelectForm("TransMulSel", qry, "Code", "Name", txtTransaction.arrValueMember, txtTransaction.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

End Class

