''New Report done agaisnt ticket no SWA/27/07/18-000036,SWA/09/08/18-000040
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Public Class RptSchemeReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean = False
    Public Shared ArrInvoice_Arr As New ArrayList()
    Private Sub SetUserMgmtNew()

        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
    End Sub
    Private Sub rptSubsidyCreditReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
        Reset()
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv.Columns.Count - 1 Step ii + 1
                        gv.Columns(ii).IsVisible = False
                        gv.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Private Sub rmSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        loadReport()
    End Sub
    Sub Reset()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        RadPageView1.SelectedPage = RadPageViewPage1
        gv.DataSource = Nothing
        gv.Rows.Clear()
        txtLocation1Mult.arrValueMember = Nothing
        txtCustomerMult.arrValueMember = Nothing


    End Sub
    Private Sub BtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReset.Click
        Reset()
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            If txtLocation1Mult.arrValueMember IsNot Nothing AndAlso txtLocation1Mult.arrValueMember.Count > 0 Then
                Dim strLocationName As String = clsCommon.GetMulcallStringWithComma(txtLocation1Mult.arrValueMember)
                arrHeader.Add((" Location : " + strLocationName + " "))
            Else
                arrHeader.Add((" Location : All"))
            End If

            If txtCustomerMult.arrValueMember IsNot Nothing AndAlso txtCustomerMult.arrValueMember.Count > 0 Then
                arrHeader.Add(" Customer : " + clsCommon.GetMulcallStringWithComma(txtCustomerMult.arrValueMember))
            Else
                arrHeader.Add((" Customer: All"))
            End If

            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("Scheme Details", gv, arrHeader, Me.Text)
            ElseIf exporter = EnumExportTo.PDF Then
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Scheme Details", gv, arrHeader, "Scheme Details", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Private Sub rmExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmExcel.Click
        print(EnumExportTo.Excel)
    End Sub
    Public Sub loadReport()

        If txtFromDate.Value > txtToDate.Value Then
            common.clsCommon.MyMessageBoxShow("From date can not be greater then to Date")
            txtFromDate.Focus()
            Exit Sub
        End If

        Dim sQuery As String = " select xx.Invoice_No,xx.Invoice_Date,xx.Location_Desc,xx.cust_Code,xx.Customer_Name,xx.Item_Code,xx.[Item Description],xx.QtyCrates,xx.SchemeRate_Percrate,xx.QTY_Box,xx.QtyPCS,xx.free_qty,xx.schemeInCrates,xx.SCM,xx.[Distributor Margin],xx.RatePerPcs,convert(decimal(18,2),(xx.RatePerPcs*xx.free_qty)) as [Scheme Amount],xx.valueInRs  from ( select distinct TSPL_SD_SALE_INVOICE_HEAD.Document_Code as Invoice_No,convert(varchar(12),TSPL_SD_SALE_INVOICE_HEAD.Document_date,103) as Invoice_Date,"
        sQuery += " TSPL_LOCATION_MASTER.Location_Desc,  TSPL_SD_SALE_INVOICE_HEAD. Customer_Code  as cust_Code ,TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_SD_sale_invoice_DETAIL.Item_Code ,TSPL_ITEM_MASTER.Item_Desc as [Item Description],  TSPL_SD_sale_invoice_DETAIL.Crate as QtyCrates,0 as SchemeRate_Percrate, case when TSPL_SD_sale_invoice_DETAIL.Qty > 0 then case when coalesce(ITEMDETAIL.Conversion_factor,0)=0 then 0 else convert(Decimal(18,2), TSPL_SD_sale_invoice_DETAIL.Qty *TSPL_ITEM_UOM_DETAIL.Conversion_Factor/coalesce(ITEMDETAIL.Conversion_factor,1)) end else 0 end as QTY_Box,"
        sQuery += " (TSPL_SD_sale_invoice_DETAIL.Qty *TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as QtyPCS , coalesce( case when TSPL_SD_sale_invoice_DETAIL.Sampling=1 then 0 else SUB_QTY end,0) as free_qty,isnull(schemeInCrates,0)as schemeInCrates, "
        sQuery += " case when TSPL_ITEM_PRICE_MASTER.Price_Comp1='SCM' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount1,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp2='SCM' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount2,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp3='SCM' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount3,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp4='SCM' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount4,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp5='SCM' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount5,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp6='SCM' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount6,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp7='SCM' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount7,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp8='SCM' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount8,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp9='SCM' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount9,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp10='SCM' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount10,0) end as SCM"
        sQuery += "  ,  case when TSPL_ITEM_PRICE_MASTER.Price_Comp1='DIS-MARGIN' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount1,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp2='DIS-MARGIN' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount2,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp3='DIS-MARGIN' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount3,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp4='DIS-MARGIN' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount4,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp5='DIS-MARGIN' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount5,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp6='DIS-MARGIN' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount6,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp7='DIS-MARGIN' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount7,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp8='DIS-MARGIN' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount8,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp9='DIS-MARGIN' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount9,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp10='DIS-MARGIN' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount10,0) end as [Distributor Margin]"
        sQuery += "  ,  convert(decimal(18,2),case when TSPL_SD_sale_invoice_DETAIL.Qty > 0 then convert(DECIMAL(18,5),(case when TSPL_SD_sale_invoice_DETAIL.Sampling=1then 0 else  TSPL_SD_sale_invoice_DETAIL.Amount end)/ (TSPL_SD_sale_invoice_DETAIL.Qty *TSPL_ITEM_UOM_DETAIL.Conversion_Factor)) else 0 end) as RatePerPcs"
        sQuery += "  , convert(decimal(18,2),((case when TSPL_SD_sale_invoice_DETAIL.Sampling=1then 0 else  TSPL_SD_sale_invoice_DETAIL.Amount end)))  as valueInRs"
        sQuery += "  , coalesce(TSPL_SD_sale_invoice_DETAIL.Cash_Scheme_Amount,0) as Cash_Scheme_Amount, '' GrandTotalCrates "
        sQuery += " from TSPL_SD_sale_invoice_DETAIL  LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_HEAD .Document_Code =TSPL_SD_sale_invoice_DETAIL.DOCUMENT_CODE left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_sale_invoice_DETAIL.Item_Code and  TSPL_ITEM_UOM_DETAIL .UOM_Code=TSPL_SD_sale_invoice_DETAIL.Unit_code LEFT OUTER JOIN TSPL_ITEM_MASTER  ON TSPL_ITEM_MASTER.Item_Code =TSPL_SD_sale_invoice_DETAIL.Item_Code left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code='BOX') as ITEMDETAIL on ITEMDETAIL.Item_code=TSPL_SD_sale_invoice_DETAIL.Item_Code  left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_SD_SALE_INVOICE_HEAD.Comp_Code  left outer join TSPL_CUSTOMER_MASTER  on TSPL_CUSTOMER_MASTER .Cust_Code  =TSPL_SD_SALE_INVOICE_HEAD .Customer_Code left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER.Location_Code =TSPL_SD_SALE_INVOICE_HEAD .Bill_To_Location LEFT OUTER JOIN TSPL_STATE_MASTER On TSPL_STATE_MASTER.State_Code=TSPL_LOCATION_MASTER.State  left join TSPL_STATE_MASTER as CUSTOMER_STATE_MASTER on TSPL_CUSTOMER_MASTER.State= CUSTOMER_STATE_MASTER.STATE_CODE  left outer join TSPL_CITY_MASTER as customer_city_master on TSPL_CUSTOMER_MASTER.city_code=customer_city_master.City_Code "
        sQuery += "   Full join  (select DOCUMENT_CODE,Item_Code as Scheme_Item_Code,SUM(Qty ) AS SUB_QTY,SUM(Crate) AS schemeInCrates from TSPL_SD_sale_invoice_DETAIL as inn "
        sQuery += "   where 2=2 and inn.Scheme_Item='Y'   group by DOCUMENT_CODE,Item_Code) TSPL_SD_sale_invoice_DETAIL_Sub on TSPL_SD_sale_invoice_DETAIL_sub.DOCUMENT_CODE=TSPL_SD_SALE_INVOICE_HEAD.DOCUMENT_CODE and TSPL_SD_sale_invoice_DETAIL_sub.Scheme_Item_Code=TSPL_SD_sale_invoice_DETAIL.Item_Code  LEFT OUTER JOIN TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_SD_SHIPMENT_HEAD.AlternateVehicle  left outer join TSPL_ITEM_PRICE_MASTER on TSPL_ITEM_PRICE_MASTER.Price_Code=TSPL_SD_sale_invoice_DETAIL.Price_code  and TSPL_ITEM_PRICE_MASTER.Location_Code=TSPL_SD_sale_invoice_DETAIL.Location  and TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_SD_sale_invoice_DETAIL.Item_Code  left outer join TSPL_DELIVERY_NOTE_master_FRESHSALE on TSPL_DELIVERY_NOTE_master_FRESHSALE.Document_No=TSPL_SD_SHIPMENT_HEAD.Against_Delivery_Code  where 2=2 and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103)   and TSPL_SD_sale_invoice_DETAIL.Scheme_Item='N' "

        If txtLocation1Mult.arrValueMember IsNot Nothing AndAlso txtLocation1Mult.arrValueMember.Count > 0 Then
            sQuery += " and TSPL_SD_SALE_INVOICE_HEAD .Bill_To_Location   IN (" + clsCommon.GetMulcallString(txtLocation1Mult.arrValueMember) + ") "
        End If
        If txtCustomerMult.arrValueMember IsNot Nothing AndAlso txtCustomerMult.arrValueMember.Count > 0 Then
            sQuery += " and TSPL_SD_SALE_INVOICE_HEAD. Customer_Code in (" + clsCommon.GetMulcallString(txtCustomerMult.arrValueMember) + ") " + Environment.NewLine
        End If

        sQuery += " UNION ALL"
        sQuery += "   select   TSPL_SD_SALE_INVOICE_HEAD.Document_Code as Invoice_No,convert(varchar(12),TSPL_SD_SALE_INVOICE_HEAD.Document_date,103) as Invoice_Date, TSPL_LOCATION_MASTER.Location_Desc,"
        sQuery += " TSPL_SD_SALE_INVOICE_HEAD. Customer_Code  as cust_Code ,TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_SD_sale_invoice_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc as Particulars, TSPL_SD_sale_invoice_DETAIL.Crate as QtyCrates,0 as SchemeRate_Percrate, case when coalesce(ITEMDETAIL.Conversion_factor,0)=0 then null else 0 end as QTY_Box,0 as QtyPCS, coalesce( case when TSPL_SD_sale_invoice_DETAIL.Sampling=1 then 0 else SUB_QTY end,0) as free_qty ,isnull(schemeInCrates,0)as schemeInCrates,  "
        sQuery += " case when TSPL_ITEM_PRICE_MASTER.Price_Comp1='SCM' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount1,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp2='SCM' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount2,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp3='SCM' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount3,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp4='SCM' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount4,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp5='SCM' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount5,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp6='SCM' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount6,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp7='SCM' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount7,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp8='SCM' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount8,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp9='SCM' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount9,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp10='SCM' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount10,0) end as SCM"
        sQuery += " ,  case when TSPL_ITEM_PRICE_MASTER.Price_Comp1='DIS-MARGIN' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount1,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp2='DIS-MARGIN' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount2,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp3='DIS-MARGIN' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount3,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp4='DIS-MARGIN' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount4,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp5='DIS-MARGIN' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount5,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp6='DIS-MARGIN' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount6,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp7='DIS-MARGIN' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount7,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp8='DIS-MARGIN' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount8,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp9='DIS-MARGIN' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount9,0)  when TSPL_ITEM_PRICE_MASTER.Price_Comp10='DIS-MARGIN' then isnull(TSPL_SD_sale_invoice_DETAIL.Price_Amount10,0) end as DIS_MARGIN "
        sQuery += " ,0 as RatePerPcs,0  as valueInRs,0 as Cash_Scheme_Amount, '' GrandTotalCrates   "
        sQuery += "   from TSPL_SD_sale_invoice_DETAIL  LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_HEAD .Document_Code =TSPL_SD_sale_invoice_DETAIL.DOCUMENT_CODE left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_sale_invoice_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL .UOM_Code=TSPL_SD_sale_invoice_DETAIL.Unit_code LEFT OUTER JOIN TSPL_ITEM_MASTER  ON TSPL_ITEM_MASTER.Item_Code =TSPL_SD_sale_invoice_DETAIL.Item_Code left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code='BOX' ) as ITEMDETAIL on ITEMDETAIL.Item_code=TSPL_SD_sale_invoice_DETAIL.Item_Code  left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_SD_SALE_INVOICE_HEAD.Comp_Code  left outer join TSPL_CUSTOMER_MASTER  on TSPL_CUSTOMER_MASTER .Cust_Code  =TSPL_SD_SALE_INVOICE_HEAD .Customer_Code left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER.Location_Code =TSPL_SD_SALE_INVOICE_HEAD .Bill_To_Location LEFT OUTER JOIN TSPL_STATE_MASTER On TSPL_STATE_MASTER.State_Code=TSPL_LOCATION_MASTER.State  left join TSPL_STATE_MASTER as CUSTOMER_STATE_MASTER on TSPL_CUSTOMER_MASTER.State= CUSTOMER_STATE_MASTER.STATE_CODE  left outer join TSPL_CITY_MASTER as customer_city_master on TSPL_CUSTOMER_MASTER.city_code=customer_city_master.City_Code  Full join (select DOCUMENT_CODE,Item_Code as Scheme_Item_Code,SUM(Qty ) AS SUB_QTY,SUM(Crate) AS schemeInCrates from TSPL_SD_sale_invoice_DETAIL as inn where 2=2 and inn.Scheme_Item='Y'   group by DOCUMENT_CODE,Item_Code) TSPL_SD_sale_invoice_DETAIL_Sub on TSPL_SD_sale_invoice_DETAIL_sub.DOCUMENT_CODE=TSPL_SD_SALE_INVOICE_HEAD.DOCUMENT_CODE and TSPL_SD_sale_invoice_DETAIL_sub.Scheme_Item_Code=TSPL_SD_sale_invoice_DETAIL.Item_Code  LEFT OUTER JOIN TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_SD_SHIPMENT_HEAD.AlternateVehicle  left outer join TSPL_ITEM_PRICE_MASTER on TSPL_ITEM_PRICE_MASTER.Price_Code=TSPL_SD_sale_invoice_DETAIL.Price_code  and TSPL_ITEM_PRICE_MASTER.Location_Code=TSPL_SD_sale_invoice_DETAIL.Location  and TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_SD_sale_invoice_DETAIL.Item_Code   left outer join TSPL_DELIVERY_NOTE_master_FRESHSALE on TSPL_DELIVERY_NOTE_master_FRESHSALE.Document_No=TSPL_SD_SHIPMENT_HEAD.Against_Delivery_Code  where 2=2 and  TSPL_SD_SALE_INVOICE_HEAD.Document_Code in   ('PI-120/17-18/000181')   and TSPL_SD_sale_invoice_DETAIL.Scheme_Item='y'    and not exists (select 1 from  (select  TSPL_SD_sale_invoice_DETAIL.Item_Code, TSPL_SD_sale_invoice_DETAIL.DOCUMENT_CODE    from TSPL_SD_sale_invoice_DETAIL LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_HEAD .Document_Code =TSPL_SD_sale_invoice_DETAIL.DOCUMENT_CODE left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_sale_invoice_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL .UOM_Code=TSPL_SD_sale_invoice_DETAIL.Unit_code LEFT OUTER JOIN TSPL_ITEM_MASTER  ON TSPL_ITEM_MASTER.Item_Code =TSPL_SD_sale_invoice_DETAIL.Item_Code left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_SD_SALE_INVOICE_HEAD.Comp_Code  left outer join TSPL_CUSTOMER_MASTER  on TSPL_CUSTOMER_MASTER .Cust_Code  =TSPL_SD_SALE_INVOICE_HEAD .Customer_Code left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER.Location_Code =TSPL_SD_SALE_INVOICE_HEAD .Bill_To_Location LEFT OUTER JOIN  TSPL_STATE_MASTER On TSPL_STATE_MASTER.State_Code=TSPL_LOCATION_MASTER.State Full join (select DOCUMENT_CODE,Item_Code as Scheme_Item_Code,SUM(Qty ) AS SUB_QTY,SUM(Crate) AS schemeInCrates from TSPL_SD_sale_invoice_DETAIL as inn "

        sQuery += " where 2=2  and inn.Scheme_Item='Y' "
        sQuery += "  group by DOCUMENT_CODE,Item_Code) TSPL_SD_sale_invoice_DETAIL_Sub on TSPL_SD_sale_invoice_DETAIL_sub.DOCUMENT_CODE=TSPL_SD_SALE_INVOICE_HEAD.DOCUMENT_CODE and TSPL_SD_sale_invoice_DETAIL_sub.Scheme_Item_Code=TSPL_SD_sale_invoice_DETAIL.Item_Code "
        sQuery += "  where 2=2 and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103)  and TSPL_SD_sale_invoice_DETAIL.Scheme_Item='N' "

        If txtLocation1Mult.arrValueMember IsNot Nothing AndAlso txtLocation1Mult.arrValueMember.Count > 0 Then
            sQuery += " and TSPL_SD_SALE_INVOICE_HEAD .Bill_To_Location   IN (" + clsCommon.GetMulcallString(txtLocation1Mult.arrValueMember) + ") "
        End If
        If txtCustomerMult.arrValueMember IsNot Nothing AndAlso txtCustomerMult.arrValueMember.Count > 0 Then
            sQuery += " and TSPL_SD_SALE_INVOICE_HEAD. Customer_Code in (" + clsCommon.GetMulcallString(txtCustomerMult.arrValueMember) + ") " + Environment.NewLine
        End If

        sQuery += " )xx where xx.Item_Code=TSPL_SD_sale_invoice_DETAIL.item_CODE and xx.DOCUMENT_CODE=TSPL_SD_sale_invoice_DETAIL.DOCUMENT_CODE ) )xx "
        Dim dtgv As New DataTable
        dtgv = clsDBFuncationality.GetDataTable(sQuery)
        If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.DataSource = dtgv
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            
            FormatGrid()
            gv.Columns("SchemeRate_Percrate").IsVisible = False
            RadPageView1.SelectedPage = RadPageViewPage2
            gv.BestFitColumns()
        Else
            gv.DataSource = Nothing
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
        End If
        ReStoreGridLayout()
    End Sub
    Sub FormatGrid()
        gv.TableElement.TableHeaderHeight = 25
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
        Next


        gv.Columns("Invoice_No").IsVisible = True
        gv.Columns("Invoice_No").Width = 100
        gv.Columns("Invoice_No").HeaderText = "Document No "

        gv.Columns("Invoice_Date").IsVisible = True
        gv.Columns("Invoice_Date").Width = 100
        gv.Columns("Invoice_Date").HeaderText = "Document Date"

        gv.Columns("LOcation_Desc").IsVisible = True
        gv.Columns("LOcation_Desc").Width = 100
        gv.Columns("LOcation_Desc").HeaderText = "Location Name"


        gv.Columns("Cust_Code").IsVisible = True
        gv.Columns("Cust_Code").Width = 100
        gv.Columns("Cust_Code").HeaderText = "Customer Code"

        gv.Columns("Customer_Name").IsVisible = True
        gv.Columns("Customer_Name").Width = 100
        gv.Columns("Customer_Name").HeaderText = "Customer Name"

        gv.Columns("Item_Code").IsVisible = True
        gv.Columns("Item_Code").Width = 100
        gv.Columns("Item_Code").HeaderText = "Item Code"

        gv.Columns("Item Description").IsVisible = True
        gv.Columns("Item Description").Width = 100
        gv.Columns("Item Description").HeaderText = "Item Description"

        gv.Columns("Qtycrates").IsVisible = True
        gv.Columns("Qtycrates").Width = 100
        gv.Columns("Qtycrates").HeaderText = "Qty(Crate)"

        gv.Columns("SchemeRate_Percrate").IsVisible = True
        gv.Columns("SchemeRate_Percrate").Width = 100
        gv.Columns("SchemeRate_Percrate").HeaderText = "Scheme Rate (per crate)"

        gv.Columns("QTY_Box").IsVisible = True
        gv.Columns("QTY_Box").Width = 100
        gv.Columns("QTY_Box").HeaderText = "Qty(Box)"

        gv.Columns("QtyPcs").IsVisible = True
        gv.Columns("QtyPcs").Width = 100
        gv.Columns("QtyPcs").HeaderText = "Qty(Pcs)"

        gv.Columns("Free_qty").IsVisible = True
        gv.Columns("Free_qty").Width = 100
        gv.Columns("Free_qty").HeaderText = "Scheme (Pcs)"

        gv.Columns("schemeinCrates").IsVisible = True
        gv.Columns("schemeinCrates").Width = 100
        gv.Columns("schemeinCrates").HeaderText = "Scheme (Crate)"

        gv.Columns("SCM").IsVisible = True
        gv.Columns("SCM").Width = 100
        gv.Columns("SCM").HeaderText = "Scheme Discount"

        gv.Columns("Distributor Margin").IsVisible = True
        gv.Columns("Distributor Margin").Width = 100
        gv.Columns("Distributor Margin").HeaderText = "Distributor Margin"

        gv.Columns("RatePerPcs").IsVisible = True
        gv.Columns("RatePerPcs").Width = 100
        gv.Columns("RatePerPcs").HeaderText = "Rate per pcs"

        gv.Columns("Scheme Amount").IsVisible = True
        gv.Columns("Scheme Amount").Width = 100
        gv.Columns("Scheme Amount").HeaderText = "Scheme Amount"

        gv.Columns("ValueInRs").IsVisible = True
        gv.Columns("ValueInRs").Width = 100
        gv.Columns("ValueInRs").HeaderText = "Value in RS"
       


     
    End Sub
    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub FrmPrintFreshInvoice_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            loadReport()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            Reset()
        End If
    End Sub

    Private Sub txtCustomerMult__My_Click(sender As Object, e As EventArgs) Handles txtCustomerMult._My_Click
        Dim qry As String = "select Cust_Code as Code,Customer_Name as Name from TSPL_CUSTOMER_master order by Cust_Code"
        txtCustomerMult.arrValueMember = clsCommon.ShowMultipleSelectForm("Cust", qry, "Code", "Name", txtCustomerMult.arrValueMember, txtCustomerMult.arrDispalyMember)
    End Sub


    Private Sub txtLocation1Mult__My_Click(sender As Object, e As EventArgs) Handles txtLocation1Mult._My_Click
        Dim qry As String = " select distinct(Segment_code) as Code ,Description  from TSPL_GL_SEGMENT_CODE left outer join TSPL_LOCATION_MASTER on TSPL_GL_SEGMENT_CODE .Segment_code =TSPL_LOCATION_MASTER .Loc_Segment_Code "
        txtLocation1Mult.arrValueMember = clsCommon.ShowMultipleSelectForm("Loc", qry, "Code", "Description", txtLocation1Mult.arrValueMember, txtLocation1Mult.arrDispalyMember)
    End Sub

   
    Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
        print(EnumExportTo.PDF)
    End Sub
End Class
