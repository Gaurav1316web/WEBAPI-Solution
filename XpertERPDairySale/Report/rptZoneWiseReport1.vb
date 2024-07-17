Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports System.IO
Imports System.Net
Imports XpertERPEngine
Imports System.Text.RegularExpressions

Public Class rptZoneWiseReport1
    Inherits FrmMainTranScreen

    Private Sub rptZoneWiseReport1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        RadPageView1.SelectedPage = RadPageViewPage1
        Reset()
    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs)
        LoadData()
    End Sub
    Sub Reset()
        gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        EnableDisableControl(True)
    End Sub

    Private Sub EnableDisableControl(ByVal val As Boolean)
        RadGroupBox1.Enabled = val
    End Sub

    Private Sub LoadData()
        Try
            Dim qry As String = "
select * from (select  CASE WHEN CODE <> 'LTR'  THEN TotalCrates_ItemWise ELSE TotalLtr_ItemWise END AS fINAL,ROW_NUMBER() OVER(PARTITION BY Structure_Code ORDER BY Structure_Code) AS SNO,*  from  (Select (Route_No)Route_No,Area_Code ,Area_Code as Code, 'Administrator' as UserName,max(Route_Desc)Route_Desc1,max(Route_Desc)Route_Desc4,(Route_Desc)Route_Desc, sum(Final_Qty)Final_Qty,
                Sku_Seq,max(Phone2)Phone2,max(Phone1)Phone1,max(Circle_No)Circle_No, max(Comp_Name)Comp_Name,max(City_Code)City_Code,max(State)State,Item_Code,max(Structure_Code)Structure_Code , max(Add1)Add1,max(Add2)Add2,max(Pincode)Pincode,max(Fax)Fax,max(FromDate)FromDate,MAX(ToDate)ToDate,max(Short_Description) AS Short_Description,max(Unit_Desc) as Unit_Desc,sum(TotalCrates_ItemWise) as TotalCrates_ItemWise,sum(TotalLtr_ItemWise) as TotalLtr_ItemWise from 
				
				( Select TSPL_ROUTE_MASTER.Route_Desc ,TSPL_ROUTE_MASTER.Route_No,TSPL_ROUTE_MASTER.Area_Code +' ' +'CRT' Area_Code,tspl_route_master.route_desc as Customer_Name  ,TSPL_DEMAND_BOOKING_MASTER.ShiftType,
                        case when TSPL_ITEM_MASTER.Is_Ambient = 1 then round((isnull(TSPL_DEMAND_BOOKING_DETAIL.Qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I.KG,2) else (case when TSPL_ITEM_MASTER.Is_FreshItem = 1 then round((isnull(TSPL_DEMAND_BOOKING_DETAIL.Qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I.[LTR],2) else 0 end ) end as Final_Qty
                       ,case when TSPL_ITEM_MASTER.Is_FreshItem = 1 then	round((isnull(TSPL_DEMAND_BOOKING_DETAIL.Qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I.KG,2) else 0 end as Kg_Qty , tspl_company_master.State,tspl_company_master.City_Code,tspl_company_master.Circle_No,tspl_company_master.Phone1,tspl_company_master.Phone2,   tspl_company_master.Comp_Name
                       ,tspl_company_master.Add1,tspl_company_master.Add2,tspl_company_master.Pincode,tspl_company_master.Fax, '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' as FromDate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "' as ToDate, TSPL_ITEM_MASTER.Sku_Seq , TSPL_DEMAND_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Structure_Code as Structure_Code,TSPL_ITEM_MASTER.Short_Description as Short_Description,TSPL_UNIT_MASTER.Unit_Desc,TSPL_DEMAND_BOOKING_DETAIL.TotalLtr_ItemWise
                       ,TSPL_DEMAND_BOOKING_DETAIL.TotalCrates_ItemWise ,(CASE WHEN TSPL_ITEM_MASTER.Is_Milk_Pouch=0 THEN TSPL_DEMAND_BOOKING_DETAIL.Qty ELSE 0 END) as ProdQ
					   from TSPL_DEMAND_BOOKING_MASTER  Left outer join TSPL_DEMAND_BOOKING_DETAIL On TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No 
                Left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code left outer join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.Unit_Code=TSPL_DEMAND_BOOKING_DETAIL.Unit_code left join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code   and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_DEMAND_BOOKING_DETAIL.Unit_Code 
                left outer join tspl_company_master on 2 = 2 left join (  SELECT * FROM ( select item_code,uom_code,conversion_factor from TSPL_ITEM_UOM_DETAIL) I  PIVOT (Max(conversion_factor) FOR uom_code IN ( [KG],[LTR] )) P ) I ON TSPL_DEMAND_BOOKING_DETAIL.Item_Code = I.item_code 
                Left Join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = TSPL_DEMAND_BOOKING_master.Route_No 
                where 2 = 2 and  convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date) BETWEEN CONVERT(DATE, '" + clsCommon.GetPrintDate(txtFromDate.Value) + "', 103) and CONVERT(DATE, '" + clsCommon.GetPrintDate(txtToDate.Value) + "', 103) and  Is_FreshItem = 1)  XXXFirst 
                where Item_Code is  not null and Area_Code is not null  group by   XXXFirst.Route_Desc,XXXFirst.Route_No, XXXFirst.Item_Code,XXXFirst.Sku_Seq,XXXFirst.Area_Code
		union all
		 Select (Route_No)Route_No,Area_Code +' '+ 'LTR','LTR' AS cODE,  'Administrator' as UserName,max(Route_Desc)Route_Desc1,max(Route_Desc)Route_Desc4,(Route_Desc)Route_Desc, sum(Final_Qty)Final_Qty,
                Sku_Seq,max(Phone2)Phone2,max(Phone1)Phone1,max(Circle_No)Circle_No, max(Comp_Name)Comp_Name,max(City_Code)City_Code,max(State)State,Item_Code,max(Structure_Code)Structure_Code , max(Add1)Add1,max(Add2)Add2,max(Pincode)Pincode,max(Fax)Fax,max(FromDate)FromDate,MAX(ToDate)ToDate,max(Short_Description) AS Short_Description,max(Unit_Desc) as Unit_Desc,sum(TotalCrates_ItemWise) as TotalCrates_ItemWise,sum(TotalLtr_ItemWise) as TotalLtr_ItemWise from 
				
				( Select TSPL_ROUTE_MASTER.Route_Desc ,TSPL_ROUTE_MASTER.Route_No,TSPL_ROUTE_MASTER.Area_Code,tspl_route_master.route_desc as Customer_Name  ,TSPL_DEMAND_BOOKING_MASTER.ShiftType,
                        case when TSPL_ITEM_MASTER.Is_Ambient = 1 then round((isnull(TSPL_DEMAND_BOOKING_DETAIL.Qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I.KG,2) else (case when TSPL_ITEM_MASTER.Is_FreshItem = 1 then round((isnull(TSPL_DEMAND_BOOKING_DETAIL.Qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I.[LTR],2) else 0 end ) end as Final_Qty
                       ,case when TSPL_ITEM_MASTER.Is_FreshItem = 1 then	round((isnull(TSPL_DEMAND_BOOKING_DETAIL.Qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I.KG,2) else 0 end as Kg_Qty , tspl_company_master.State,tspl_company_master.City_Code,tspl_company_master.Circle_No,tspl_company_master.Phone1,tspl_company_master.Phone2,   tspl_company_master.Comp_Name
                       ,tspl_company_master.Add1,tspl_company_master.Add2,tspl_company_master.Pincode,tspl_company_master.Fax, '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' as FromDate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "' as ToDate, TSPL_ITEM_MASTER.Sku_Seq , TSPL_DEMAND_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Structure_Code as Structure_Code,TSPL_ITEM_MASTER.Short_Description as Short_Description,TSPL_UNIT_MASTER.Unit_Desc,TSPL_DEMAND_BOOKING_DETAIL.TotalLtr_ItemWise
                       ,TSPL_DEMAND_BOOKING_DETAIL.TotalCrates_ItemWise ,(CASE WHEN TSPL_ITEM_MASTER.Is_Milk_Pouch=0 THEN TSPL_DEMAND_BOOKING_DETAIL.Qty ELSE 0 END) as ProdQ
					   from TSPL_DEMAND_BOOKING_MASTER  Left outer join TSPL_DEMAND_BOOKING_DETAIL On TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No 
                Left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code left outer join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.Unit_Code=TSPL_DEMAND_BOOKING_DETAIL.Unit_code left join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code   and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_DEMAND_BOOKING_DETAIL.Unit_Code 
                left outer join tspl_company_master on 2 = 2 left join (  SELECT * FROM ( select item_code,uom_code,conversion_factor from TSPL_ITEM_UOM_DETAIL) I  PIVOT (Max(conversion_factor) FOR uom_code IN ( [KG],[LTR] )) P ) I ON TSPL_DEMAND_BOOKING_DETAIL.Item_Code = I.item_code 
                Left Join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = TSPL_DEMAND_BOOKING_master.Route_No 
                where 2 = 2 and  convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date) BETWEEN CONVERT(DATE, '" + clsCommon.GetPrintDate(txtFromDate.Value) + "', 103) and CONVERT(DATE, '" + clsCommon.GetPrintDate(txtToDate.Value) + "', 103) and  Is_FreshItem = 1)  XXXFirst 
                where Item_Code is  not null and Area_Code is not null  group by   XXXFirst.Route_Desc,XXXFirst.Route_No, XXXFirst.Item_Code,XXXFirst.Sku_Seq,XXXFirst.Area_Code) xx)PP  ORDER BY Structure_Code "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "crptZoneWiseReport", "")
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to display", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub

    Sub SetGridFormation()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = True
        gv1.EnableFiltering = True
        gv1.ShowRowHeaderColumn = True
        gv1.ShowGroupPanel = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
            gv1.Columns(ii).FormatString = ""
        Next
        gv1.Columns("Total").IsVisible = False
        gv1.Columns("Cust_Code").HeaderText = "Customer"
        gv1.Columns("Customer_Name").HeaderText = "Customer Name"

        Dim summaryRowItem As New GridViewSummaryRowItem()
        For ii As Integer = 2 To gv1.Columns.Count - 1
            summaryRowItem.Add(New GridViewSummaryItem(gv1.Columns(ii).Name, "", GridAggregateFunction.Sum))
        Next
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Print Date (" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd-MMM-yyyy hh:mm:ss tt") + ")")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptD1D2Report & "'"))
                arrHeader.Add("Date Range : " & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "  To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))

                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export.", Me.Text)

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPDF_Click(sender As Object, e As EventArgs) Handles btnPDF.Click
        Try

            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()

                clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text)
                arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))

            Else
                clsCommon.MyMessageBoxShow(Me, "No data found To export", Me.Text)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPrint_Click_1(sender As Object, e As EventArgs) Handles btnPrint.Click
        LoadData()
    End Sub
End Class