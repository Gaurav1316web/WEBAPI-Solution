
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

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        If BtnProductWiseSaleQuantity.IsChecked Then
            ProductWiseSale(False)
        ElseIf BtnBillWiseSaleOfMilkSummary.IsChecked Then
            BillWisesaleSummary(False)
        Else
            LoadData()
        End If
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
                    'SetGridFormation()
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
                    LEFT JOIN TSPL_COMPANY_MASTER ON 2 = 2 where convert(date,Document_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and convert(date,Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value) + "' ) xx 
                GROUP BY Item_Code"
            'Qry = " SELECT 
            '            MAX(Item_Desc) AS Item_Desc, 
            '            Item_Code, MAX(Unit_code) AS Unit_code,SUM(Out_Qty) AS out_qty,MAX(UoM) as UoM,SUM(QtyAccToReportUOM) AS QtyAccToReportUOM,MAX(UOM_Code) AS UOM_Code,MAX(Comp_Name) AS Comp_Name,MAX(Add1) AS Add1,MAX(Add2) AS Add2,MAX(Add3) AS Add3,MAX(City_Code) AS City_Code,MAX(State) AS State, MAX(Document_Date) AS Document_Date 
            '        FROM (
            '            SELECT 
            '                ItemConvinUOMKG.UOM_Code AS kg, 
            '                ItemConvinUOMLTR.UOM_Code AS ltr, 
            '                TSPL_ITEM_MASTER.Item_Desc, 
            '                TSPL_ITEM_MASTER.Item_Code, 
            '                TSPL_TRANSFER_ORDER_detail.Unit_code, 
            '                TSPL_TRANSFER_ORDER_detail.Out_Qty,
            '          Case WHEN TSPL_ITEM_MASTER.Is_FreshItem = 1 
            '                            THEN ItemConvinUOMLTR.UOM_Code  WHEN TSPL_ITEM_MASTER.Is_Ambient = 1 
            '                            THEN ItemConvinUOMKG.UOM_Code end as UoM,
            '                CAST(
            '                    CASE 
            '                        WHEN TSPL_ITEM_MASTER.Is_FreshItem = 1 
            '                            THEN TSPL_TRANSFER_ORDER_detail.Out_Qty * ItemConvReportUOM.Conversion_Factor / ItemConvinUOMLTR.Conversion_Factor  
            '                        WHEN TSPL_ITEM_MASTER.Is_Ambient = 1 
            '                            THEN TSPL_TRANSFER_ORDER_detail.Out_Qty * ItemConvReportUOM.Conversion_Factor / ItemConvinUOMKG.Conversion_Factor  
            '                        ELSE 0
            '                    END 
            '                AS DECIMAL(18,2)) AS QtyAccToReportUOM, 
            '                ItemConvReportUOM.UOM_Code, 
            '                TSPL_COMPANY_MASTER.Comp_Name, 
            '                TSPL_COMPANY_MASTER.Add1, 
            '                TSPL_COMPANY_MASTER.Add2, 
            '                TSPL_COMPANY_MASTER.Add3, 
            '                TSPL_COMPANY_MASTER.City_Code, 
            '                TSPL_COMPANY_MASTER.State, 
            '                Document_Date 
            '            FROM TSPL_TRANSFER_ORDER_detail
            '            LEFT OUTER JOIN TSPL_TRANSFER_ORDER_HEAD 
            '                ON TSPL_TRANSFER_ORDER_HEAD.Document_No = TSPL_TRANSFER_ORDER_DETAIL.Document_No
            '            LEFT OUTER JOIN TSPL_ITEM_MASTER 
            '                ON TSPL_ITEM_MASTER.item_code = TSPL_TRANSFER_ORDER_DETAIL.item_code
            '            LEFT JOIN TSPL_ITEM_UOM_DETAIL AS ItemConvReportUOM 
            '                ON TSPL_ITEM_MASTER.Item_Code = ItemConvReportUOM.Item_Code
            '            LEFT JOIN TSPL_ITEM_UOM_DETAIL AS ItemConvinUOMLTR 
            '                ON TSPL_TRANSFER_ORDER_detail.Item_Code = ItemConvinUOMLTR.Item_Code 
            '                AND ItemConvinUOMLTR.UOM_Code = 'LTR'
            '            LEFT JOIN TSPL_ITEM_UOM_DETAIL AS ItemConvinUOMKG 
            '                ON TSPL_TRANSFER_ORDER_detail.Item_Code = ItemConvinUOMKG.Item_Code 
            '                AND ItemConvinUOMKG.UOM_Code = 'KG'
            '            LEFT JOIN TSPL_COMPANY_MASTER ON 2 = 2 where convert(date,Document_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and convert(date,Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value) + "')xx group by Item_Code  "
            '       BaseQry = " select TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Item_Code,TSPL_TRANSFER_ORDER_detail.Unit_code,TSPL_TRANSFER_ORDER_detail.Out_Qty,
            '           cast(( TSPL_TRANSFER_ORDER_detail.Out_Qty * ItemConvinUOM.Conversion_Factor / ItemConvReportUOM.Conversion_Factor) as Decimal(18,2)) as
            '           QtyAccToReportUOM,ItemConvReportUOM.UOM_Code,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER.Add2,TSPL_COMPANY_MASTER.Add3,TSPL_COMPANY_MASTER.City_Code,TSPL_COMPANY_MASTER.State,Document_Date from TSPL_TRANSFER_ORDER_detail
            '           left outer join TSPL_TRANSFER_ORDER_HEAD on TSPL_TRANSFER_ORDER_HEAD.Document_No=TSPL_TRANSFER_ORDER_DETAIL.Document_No
            '           left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_TRANSFER_ORDER_DETAIL.item_code
            '           left join TSPL_ITEM_UOM_DETAIL as ItemConvReportUOM on TSPL_ITEM_master.Item_Code = ItemConvReportUOM.Item_Code 
            '           and ItemConvReportUOM.Report_UOM = 1
            '           left join TSPL_ITEM_UOM_DETAIL as ItemConvinUOM on TSPL_TRANSFER_ORDER_detail.Item_Code = ItemConvinUOM.Item_Code 
            '         and TSPL_TRANSFER_ORDER_detail.Unit_code = ItemConvinUOM.UOM_Code 
            'left join TSPL_COMPANY_MASTER on 2=2 where convert(date,Document_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and convert(date,Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value) + "' "
            '       If rbtnDetail.IsChecked Then
            '           Qry += BaseQry
            '       ElseIf rbtnSummary.IsChecked Then
            '           Qry = "Select MAX(Item_Desc)Item_Desc,Item_Code,MAX(Unit_code)Unit_code,SUM(Out_Qty)out_qty,SUM(QtyAccToReportUOM)QtyAccToReportUOM,MAX(UOM_Code)UOM_Code,MAX(Comp_Name)Comp_Name,MAX(Add1)Add1,MAX(Add2)Add2,MAX(Add3)Add3,MAX(City_Code)City_Code,MAX(State)State,MAX(Document_Date)Document_Date From ( " + BaseQry + ")xx group by Item_Code"
            '       End If
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
                    'SetGridFormation()
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

    Sub funreset()
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
                    ElseIf BtnPartySaleMilkProduct.IsChecked = True Then
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
            obj.ReportID = MyBase.Form_ID
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

