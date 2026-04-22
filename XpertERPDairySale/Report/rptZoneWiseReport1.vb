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
        txtFromShift.SelectedValue = "M"
        txtToShift.SelectedValue = "E"
        LoadShiftFrom()
        LoadShiftTo()
        Reset()
    End Sub
    Sub LoadShiftTo()
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Shift")

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "M"
        dr("Shift") = "M"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "E"
        dr("Shift") = "E"
        dt.Rows.Add(dr)

        txtToShift.DataSource = dt
        txtToShift.ValueMember = "Code"
        txtToShift.DisplayMember = "Shift"
    End Sub
    Sub LoadShiftFrom()
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Shift")

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "M"
        dr("Shift") = "M"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "E"
        dr("Shift") = "E"
        dt.Rows.Add(dr)

        txtFromShift.DataSource = dt
        txtFromShift.ValueMember = "Code"
        txtFromShift.DisplayMember = "Shift"
    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs)
        LoadData()
    End Sub
    Sub Reset()
        RadPageView1.SelectedPage = RadPageViewPage1
        EnableDisableControl(True)
    End Sub

    Private Sub EnableDisableControl(ByVal val As Boolean)
        RadGroupBox1.Enabled = val
        txtRoute.arrValueMember = Nothing
        txtFromShift.SelectedValue = "M"
        txtToShift.SelectedValue = "E"
    End Sub

    Private Sub LoadData()
        Try
            Dim Fromshift As String = clsCommon.myCstr(txtFromShift.Text)
            Dim Toshift As String = clsCommon.myCstr(txtToShift.Text)
            Dim qry As String = "
select * from (select CASE WHEN CODE <> 'LTR'   THEN TotalCrates_ItemWise else 0 END AS fINAL1,'" + Fromshift + "' AS FromShift, '" + Toshift + " ' as Toshift,
CASE WHEN CODE ='LTR'  THEN TotalLtr_ItemWise else 0  END AS fINAL2, CASE WHEN CODE <> 'LTR'  THEN TotalCrates_ItemWise ELSE TotalLtr_ItemWise END AS fINAL,ROW_NUMBER() OVER(PARTITION BY Structure_Code ORDER BY Structure_Code) AS SNO,*  from  "
            If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "CHT") = CompairStringResult.Equal Then
                qry += "(Select (Zone_Code)Zone_Code,max(Route_No)Route_No,"

            Else

                qry += "(Select (Route_No)Route_No,"
            End If

            qry +=  "Area_Code ,Area_Code as Code, 'Administrator' as UserName,max(Route_Desc)Route_Desc1,max(Route_Desc)Route_Desc4,(Route_Desc)Route_Desc, sum(Final_Qty)Final_Qty,
                Sku_Seq,max(Phone2)Phone2,max(Phone1)Phone1,max(Circle_No)Circle_No, max(Comp_Name)Comp_Name,max(City_Code)City_Code,max(State)State,Item_Code,max(Structure_Code)Structure_Code , max(Add1)Add1,max(Add2)Add2,max(Pincode)Pincode,max(Fax)Fax,max(FromDate)FromDate,MAX(ToDate)ToDate,max(Short_Description) AS Short_Description,max(Unit_Desc) as Unit_Desc,sum(TotalCrates_ItemWise) as TotalCrates_ItemWise,sum(TotalLtr_ItemWise) as TotalLtr_ItemWise from 
				
				( Select TSPL_ROUTE_MASTER.Route_Desc ,"
            If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "CHT") = CompairStringResult.Equal Then
                qry += " TSPL_ROUTE_MASTER.Zone_Code, TSPL_ROUTE_MASTER.Route_No,"

            Else

                qry += " TSPL_ROUTE_MASTER.Route_No,"
            End If
            qry += "TSPL_ROUTE_MASTER.Area_Code +' ' +'CRT' Area_Code,tspl_route_master.route_desc as Customer_Name  ,TSPL_DEMAND_BOOKING_MASTER.ShiftType,
                        case when TSPL_ITEM_MASTER.Is_Ambient = 1 then round((isnull(TSPL_DEMAND_BOOKING_DETAIL.Qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I.KG,2) else (case when TSPL_ITEM_MASTER.Is_FreshItem = 1 then round((isnull(TSPL_DEMAND_BOOKING_DETAIL.Qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I.[LTR],2) else 0 end ) end as Final_Qty
                       ,case when TSPL_ITEM_MASTER.Is_FreshItem = 1 then	round((isnull(TSPL_DEMAND_BOOKING_DETAIL.Qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I.KG,2) else 0 end as Kg_Qty , tspl_company_master.State,tspl_company_master.City_Code,tspl_company_master.Circle_No,tspl_company_master.Phone1,tspl_company_master.Phone2,   tspl_company_master.Comp_Name
                       ,tspl_company_master.Add1,tspl_company_master.Add2,tspl_company_master.Pincode,tspl_company_master.Fax, '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' as FromDate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "' as ToDate, TSPL_ITEM_MASTER.Sku_Seq , TSPL_DEMAND_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Structure_Code as Structure_Code,TSPL_ITEM_MASTER.Short_Description as Short_Description,TSPL_UNIT_MASTER.Unit_Desc,TSPL_DEMAND_BOOKING_DETAIL.TotalLtr_ItemWise
                       ,TSPL_DEMAND_BOOKING_DETAIL.TotalCrates_ItemWise ,(CASE WHEN TSPL_ITEM_MASTER.Is_Milk_Pouch=0 THEN TSPL_DEMAND_BOOKING_DETAIL.Qty ELSE 0 END) as ProdQ
					   from TSPL_DEMAND_BOOKING_MASTER  Left outer join TSPL_DEMAND_BOOKING_DETAIL On TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No 
                Left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code left outer join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.Unit_Code=TSPL_DEMAND_BOOKING_DETAIL.Unit_code left join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code   and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_DEMAND_BOOKING_DETAIL.Unit_Code 
                left outer join tspl_company_master on 2 = 2 left join (  SELECT * FROM ( select item_code,uom_code,conversion_factor from TSPL_ITEM_UOM_DETAIL) I  PIVOT (Max(conversion_factor) FOR uom_code IN ( [KG],[LTR] )) P ) I ON TSPL_DEMAND_BOOKING_DETAIL.Item_Code = I.item_code 
                Left Join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = TSPL_DEMAND_BOOKING_master.Route_No 
                where 2 = 2 and  convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date) BETWEEN CONVERT(DATE, '" + clsCommon.GetPrintDate(txtFromDate.Value) + "', 103) and CONVERT(DATE, '" + clsCommon.GetPrintDate(txtToDate.Value) + "', 103) and  Is_FreshItem = 1)  XXXFirst 
                where Item_Code is  not null and Area_Code is not null  group by   XXXFirst.Route_Desc,"
            If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "CHT") = CompairStringResult.Equal Then
                qry += " XXXFirst.Zone_Code,"
            Else
                qry += "XXXFirst.Route_No,"
            End If
            qry += " XXXFirst.Item_Code,XXXFirst.Sku_Seq,XXXFirst.Area_Code
		union all"
            If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "CHT") = CompairStringResult.Equal Then
                qry += " Select  (Zone_Code)Zone_Code, max(Route_No)Route_No,"
            Else
                qry += " Select (Route_No)Route_No,"
            End If

            qry += "Area_Code +' '+ 'LTR','LTR' AS cODE,  'Administrator' as UserName,max(Route_Desc)Route_Desc1,max(Route_Desc)Route_Desc4,(Route_Desc)Route_Desc, sum(Final_Qty)Final_Qty,
                Sku_Seq,max(Phone2)Phone2,max(Phone1)Phone1,max(Circle_No)Circle_No, max(Comp_Name)Comp_Name,max(City_Code)City_Code,max(State)State,Item_Code,max(Structure_Code)Structure_Code , max(Add1)Add1,max(Add2)Add2,max(Pincode)Pincode,max(Fax)Fax,max(FromDate)FromDate,MAX(ToDate)ToDate,max(Short_Description) AS Short_Description,max(Unit_Desc) as Unit_Desc,sum(TotalCrates_ItemWise) as TotalCrates_ItemWise,sum(TotalLtr_ItemWise) as TotalLtr_ItemWise from 
				
				( Select TSPL_ROUTE_MASTER.Route_Desc ,"
            If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "CHT") = CompairStringResult.Equal Then
                qry += " TSPL_ROUTE_MASTER.Zone_Code , TSPL_ROUTE_MASTER.Route_No,"
            Else
                qry += "TSPL_ROUTE_MASTER.Route_No,"
            End If
            qry += "TSPL_ROUTE_MASTER.Area_Code,tspl_route_master.route_desc as Customer_Name  ,TSPL_DEMAND_BOOKING_MASTER.ShiftType,
                        case when TSPL_ITEM_MASTER.Is_Ambient = 1 then round((isnull(TSPL_DEMAND_BOOKING_DETAIL.Qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I.KG,2) else (case when TSPL_ITEM_MASTER.Is_FreshItem = 1 then round((isnull(TSPL_DEMAND_BOOKING_DETAIL.Qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I.[LTR],2) else 0 end ) end as Final_Qty
                       ,case when TSPL_ITEM_MASTER.Is_FreshItem = 1 then	round((isnull(TSPL_DEMAND_BOOKING_DETAIL.Qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I.KG,2) else 0 end as Kg_Qty , tspl_company_master.State,tspl_company_master.City_Code,tspl_company_master.Circle_No,tspl_company_master.Phone1,tspl_company_master.Phone2,   tspl_company_master.Comp_Name
                       ,tspl_company_master.Add1,tspl_company_master.Add2,tspl_company_master.Pincode,tspl_company_master.Fax, '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' as FromDate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "' as ToDate, TSPL_ITEM_MASTER.Sku_Seq , TSPL_DEMAND_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Structure_Code as Structure_Code,TSPL_ITEM_MASTER.Short_Description as Short_Description,TSPL_UNIT_MASTER.Unit_Desc,TSPL_DEMAND_BOOKING_DETAIL.TotalLtr_ItemWise
                       ,TSPL_DEMAND_BOOKING_DETAIL.TotalCrates_ItemWise ,(CASE WHEN TSPL_ITEM_MASTER.Is_Milk_Pouch=0 THEN TSPL_DEMAND_BOOKING_DETAIL.Qty ELSE 0 END) as ProdQ
					   from TSPL_DEMAND_BOOKING_MASTER  Left outer join TSPL_DEMAND_BOOKING_DETAIL On TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No 
                Left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code left outer join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.Unit_Code=TSPL_DEMAND_BOOKING_DETAIL.Unit_code left join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code   and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_DEMAND_BOOKING_DETAIL.Unit_Code 
                left outer join tspl_company_master on 2 = 2 left join (  SELECT * FROM ( select item_code,uom_code,conversion_factor from TSPL_ITEM_UOM_DETAIL) I  PIVOT (Max(conversion_factor) FOR uom_code IN ( [KG],[LTR] )) P ) I ON TSPL_DEMAND_BOOKING_DETAIL.Item_Code = I.item_code 
                Left Join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = TSPL_DEMAND_BOOKING_master.Route_No 
                where 2 = 2   "

            ' convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date) BETWEEN CONVERT(DATE, '" + clsCommon.GetPrintDate(txtFromDate.Value) + "', 103) and CONVERT(DATE, '" + clsCommon.GetPrintDate(txtToDate.Value) + "', 103)
            qry += " and Cast(TSPL_DEMAND_BOOKING_MASTER.Document_Date as Date) >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(clsCommon.myCDate(txtFromDate.Value)), "dd/MMM/yyyy") + "' and Cast(TSPL_DEMAND_BOOKING_MASTER.Document_Date as Date) <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.myCDate(txtToDate.Value)), "dd/MMM/yyyy") + "'"

            If clsCommon.CompairString(clsCommon.myCstr(txtFromShift.Text), "E") = CompairStringResult.Equal Then
                qry += " and 2=( case when Cast(TSPL_DEMAND_BOOKING_MASTER.Document_Date as Date) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(clsCommon.myCDate(txtFromDate.Value)), "dd/MMM/yyyy") + "' and Cast(TSPL_DEMAND_BOOKING_MASTER.Document_Date as Date) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.myCDate(txtFromDate.Value)), "dd/MMM/yyyy") + "' and TSPL_DEMAND_BOOKING_MASTER.ShiftType='M' then 3 else 2 end  )"
            End If
            If clsCommon.CompairString(clsCommon.myCstr(txtToDate.Text), "M") = CompairStringResult.Equal Then
                qry += " and 2=( case when Cast(TSPL_DEMAND_BOOKING_MASTER.Document_Date as Date) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(clsCommon.myCDate(txtToDate.Value)), "dd/MMM/yyyy") + "' and Cast(TSPL_DEMAND_BOOKING_MASTER.Document_Date as Date) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.myCDate(txtToDate.Value)), "dd/MMM/yyyy") + "' and TSPL_DEMAND_BOOKING_MASTER.ShiftType='E' then 3 else 2 end  )"
            End If

            qry += "and  Is_FreshItem = 1)  XXXFirst 
                where Item_Code is  not null and Area_Code is not null  group by   XXXFirst.Route_Desc,"

            If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "CHT") = CompairStringResult.Equal Then
                qry += " XXXFirst.Zone_Code, "
            Else
                qry += "  XXXFirst.Route_No,"
End If
            qry += "   XXXFirst.Item_Code,XXXFirst.Sku_Seq,XXXFirst.Area_Code) xx)PP   "
            'qry += " ORDER BY Structure_Code"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "CHT") = CompairStringResult.Equal Then
                Dim dtItems As DataTable = clsDBFuncationality.GetDataTable("select Item_Code,max(Sku_Seq) as Sku_Seq,max(Short_Description) as Short_Description,max(Structure_Code)Structure_Code from (" + qry + ") x group by Item_Code order by Sku_Seq")

                Dim FinalQuery As String = " With CTERawData as ( " + qry + "  )" + Environment.NewLine + Environment.NewLine
                For ii As Integer = 1 To dtItems.Rows.Count Step 10
                    If ii > 1 Then
                        FinalQuery += Environment.NewLine + " Union all " + Environment.NewLine
                    End If
                    FinalQuery += " select '" & objCommonVar.CurrentUser & "' as UserName," + clsCommon.myCstr(ii) + " as Grp ,'" + txtFromShift.Text + "' as FromShift,'" + txtToShift.Text + "' as ToShift,    '" + clsCommon.GetPrintDate(clsCommon.myCDate(txtFromDate.Value), "dd/MM/yyyy") + "' AS FromDate,'" + clsCommon.GetPrintDate(clsCommon.myCDate(txtToDate.Value), "dd/MM/yyyy") + "' AS ToDate,"
                    FinalQuery += "Area_code,(Zone_Code)Zone_Code,max(Route_No) as Route_No"
                    'FinalQuery += "max(Document_Date) as Document_Date"
                    For jj As Integer = 1 To 10
                        Dim strJJ As String = clsCommon.myCstr(jj)
                        Dim strICODE As String = ""
                        Dim strIShortDesc As String = ""
                        Dim strISTRCode As String = ""
                        If (ii + jj - 1) > dtItems.Rows.Count Then
                            strICODE = ""
                            strIShortDesc = ""
                            strISTRCode = "-"
                        Else
                            strICODE = clsCommon.myCstr(dtItems.Rows(ii + jj - 2)("Item_Code"))
                            strIShortDesc = clsCommon.myCstr(dtItems.Rows(ii + jj - 2)("Short_Description"))
                            strISTRCode = clsCommon.myCstr(dtItems.Rows(ii + jj - 2)("Structure_Code"))

                            'strIUnitCode = "" + ddlQtyConversionType.SelectedValue + ""
                        End If
                        FinalQuery += " ,'" + strISTRCode + "' as Structure_Code_" + strJJ + ",'" + strICODE + "' as Item_" + strJJ + " ,'" + strIShortDesc + "' as Item_Short_Description_" + strJJ + " 

    ,Case when (sum(case when Item_Code='" + strICODE + "' then convert(decimal(18,2),Final) else null end )) is null then '-' ELSE  convert(varchar,sum(case when Item_Code='" + strICODE + "'  then convert(decimal(18,2),Final) else null end))
    End   As ItemQtyCrate_" + strJJ + ""
                    Next

                    FinalQuery += ", max(Structure_Code) as Structure_Code,sum(TotalCrates_ItemWise) as TotalCrates_ItemWise,sum(TotalLtr_ItemWise) as TotalLtr_ItemWise ,sum(fINAL1)fINAL1,sum(fINAL2)fINAL2"
                    FinalQuery += ",max(Sku_Seq) as Sku_Seq from ( select xx.*	from CTERawData xx ) x group by Zone_Code,,Area_Code "
                Next

                dt = clsDBFuncationality.GetDataTable(FinalQuery)


            End If


            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "CHT") = CompairStringResult.Equal Then
                    ' frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.KwalitySalesReport, dt, "crptZoneWiseReportCHTNEW", "")
                    frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.KwalitySalesReport, dt, "crptZoneWiseReportCHT", "")

                    '  frmCRV.funsubreportWithdt(MyBase.Form_ID, CrystalReportFolder.KwalitySalesReport, dt, Nothing, "crptZoneWiseReportCHTNEW1", "ZoneWiseReportCHTNEW")
                    frmCRV = Nothing

                Else

                    frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.KwalitySalesReport, dt, "crptZoneWiseReport", "")
                    frmCRV = Nothing

                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to display", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnPrint_Click_1(sender As Object, e As EventArgs) Handles btnPrint.Click
        LoadData()
    End Sub



    Private Sub btnPrint1_Click(sender As Object, e As EventArgs) Handles btnPrint1.Click

    End Sub

    Private Sub txtZone_Click(sender As Object, e As EventArgs) Handles txtZone.Click
        LoadData()
    End Sub

    Private Sub btnRoute_Click(sender As Object, e As EventArgs) Handles btnRoute.Click
        Try
            ' Dim whrclsShift As String = ""
            Dim Shift As String = ""

            'If rbtnMorning.IsChecked Then
            '    whrclsShift = " and TSPL_DEMAND_BOOKING_MASTER.ShiftType  = 'MORNING' "
            '    Shift = "Morning"
            'ElseIf rbtnEvening.IsChecked Then
            '    whrclsShift = " and TSPL_DEMAND_BOOKING_MASTER.ShiftType  = 'Evening' "
            '    Shift = "Evening"
            'Else
            '    Shift = "Both"
            'End If
            Dim Fromshift As String = clsCommon.myCstr(txtFromShift.Text)
            Dim Toshift As String = clsCommon.myCstr(txtToShift.Text)

            Dim qry As String = "
select * from (select  CASE WHEN CODE <> 'LTR'   THEN TotalCrates_ItemWise else 0 END AS fINAL1,
CASE WHEN CODE ='LTR'  THEN TotalLtr_ItemWise else 0  END AS fINAL2,'" + Fromshift + "' AS FromShift, '" + Toshift + " ' as Toshift,
CASE WHEN CODE <> 'LTR'  THEN TotalCrates_ItemWise ELSE TotalLtr_ItemWise END AS fINAL,ROW_NUMBER() OVER(PARTITION BY Structure_Code ORDER BY Structure_Code) AS SNO,*  from  (Select (Route_No)Route_No,Area_Code ,Area_Code as Code, 'Administrator' as UserName,max(Route_Desc)Route_Desc1,max(Route_Desc)Route_Desc4,(Route_Desc)Route_Desc, sum(Final_Qty)Final_Qty,
                Sku_Seq,max(Phone2)Phone2,max(Phone1)Phone1,max(Circle_No)Circle_No, max(Comp_Name)Comp_Name,max(City_Code)City_Code,max(State)State,Item_Code,max(Structure_Code)Structure_Code , max(Add1)Add1,max(Add2)Add2,max(Pincode)Pincode,max(Fax)Fax,max(FromDate)FromDate,MAX(ToDate)ToDate,max(Short_Description) AS Short_Description,max(Unit_Desc) as Unit_Desc,(TotalCrates_ItemWise) as TotalCrates_ItemWise,(TotalLtr_ItemWise) as TotalLtr_ItemWise ,MAX(ShiftType )ShiftType from 
				
				( Select TSPL_ROUTE_MASTER.Route_Desc ,TSPL_ROUTE_MASTER.Route_No,TSPL_ROUTE_MASTER.Route_Desc +' ' +'CRT' Area_Code,tspl_route_master.route_desc as Customer_Name  ,TSPL_DEMAND_BOOKING_MASTER.ShiftType,
                        case when TSPL_ITEM_MASTER.Is_Ambient = 1 then round((isnull(TSPL_DEMAND_BOOKING_DETAIL.Qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I.KG,2) else (case when TSPL_ITEM_MASTER.Is_FreshItem = 1 then round((isnull(TSPL_DEMAND_BOOKING_DETAIL.Qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I.[LTR],2) else 0 end ) end as Final_Qty
                       ,case when TSPL_ITEM_MASTER.Is_FreshItem = 1 then	round((isnull(TSPL_DEMAND_BOOKING_DETAIL.Qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I.KG,2) else 0 end as Kg_Qty , tspl_company_master.State,tspl_company_master.City_Code,tspl_company_master.Circle_No,tspl_company_master.Phone1,tspl_company_master.Phone2,   tspl_company_master.Comp_Name
                       ,tspl_company_master.Add1,tspl_company_master.Add2,tspl_company_master.Pincode,tspl_company_master.Fax, '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' as FromDate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "' as ToDate, TSPL_ITEM_MASTER.Sku_Seq , TSPL_DEMAND_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Structure_Code as Structure_Code,TSPL_ITEM_MASTER.Short_Description as Short_Description,TSPL_UNIT_MASTER.Unit_Desc,TSPL_DEMAND_BOOKING_DETAIL.TotalLtr_ItemWise
                       ,TSPL_DEMAND_BOOKING_DETAIL.TotalCrates_ItemWise ,(CASE WHEN TSPL_ITEM_MASTER.Is_Milk_Pouch=0 THEN TSPL_DEMAND_BOOKING_DETAIL.Qty ELSE 0 END) as ProdQ
					   from TSPL_DEMAND_BOOKING_MASTER  Left outer join TSPL_DEMAND_BOOKING_DETAIL On TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No 
                Left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code left outer join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.Unit_Code=TSPL_DEMAND_BOOKING_DETAIL.Unit_code left join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code   and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_DEMAND_BOOKING_DETAIL.Unit_Code 
                left outer join tspl_company_master on 2 = 2 left join (  SELECT * FROM ( select item_code,uom_code,conversion_factor from TSPL_ITEM_UOM_DETAIL) I  PIVOT (Max(conversion_factor) FOR uom_code IN ( [KG],[LTR] )) P ) I ON TSPL_DEMAND_BOOKING_DETAIL.Item_Code = I.item_code 
                Left Join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = TSPL_DEMAND_BOOKING_master.Route_No 
                where 2 = 2 "
            qry += " and  convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date) BETWEEN CONVERT(DATE, '" + clsCommon.GetPrintDate(txtFromDate.Value) + "', 103) and CONVERT(DATE, '" + clsCommon.GetPrintDate(txtToDate.Value) + "', 103) " 
            
               If clsCommon.CompairString(clsCommon.myCstr(txtFromShift.Text), "E") = CompairStringResult.Equal Then
                qry += " and 2=( case when Cast(TSPL_DEMAND_BOOKING_MASTER.Document_Date as Date) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(clsCommon.myCDate(txtFromDate.Value)), "dd/MMM/yyyy") + "' and Cast(TSPL_DEMAND_BOOKING_MASTER.Document_Date as Date) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.myCDate(txtFromDate.Value)), "dd/MMM/yyyy") + "' and TSPL_DEMAND_BOOKING_MASTER.ShiftType='Morning' then 3 else 2 end  )"
            End If
            If clsCommon.CompairString(clsCommon.myCstr(txtToShift.Text), "M") = CompairStringResult.Equal Then
                qry += " and 2=( case when Cast(TSPL_DEMAND_BOOKING_MASTER.Document_Date as Date) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(clsCommon.myCDate(txtToDate.Value)), "dd/MMM/yyyy") + "' and Cast(TSPL_DEMAND_BOOKING_MASTER.Document_Date as Date) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.myCDate(txtToDate.Value)), "dd/MMM/yyyy") + "' and TSPL_DEMAND_BOOKING_MASTER.ShiftType='Evening' then 3 else 2 end  )"
            End If
            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                qry += " and tspl_route_master.Route_no in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ") "
            End If


            qry += " and  Is_FreshItem = 1  )  XXXFirst 
                where Item_Code Is  Not null And Area_Code Is Not null  group by  XXXFirst.TotalCrates_ItemWise,XXXFirst.TotalLtr_ItemWise, XXXFirst.Route_Desc,XXXFirst.Route_No, XXXFirst.Item_Code,XXXFirst.Sku_Seq,XXXFirst.Area_Code
		union all
		 Select (Route_No)Route_No,Route_Desc +' '+ 'LTR','LTR' AS cODE,  'Administrator' as UserName,max(Route_Desc)Route_Desc1,max(Route_Desc)Route_Desc4,(Route_Desc)Route_Desc, sum(Final_Qty)Final_Qty,
                Sku_Seq,max(Phone2)Phone2,max(Phone1)Phone1,max(Circle_No)Circle_No, max(Comp_Name)Comp_Name,max(City_Code)City_Code,max(State)State,Item_Code,max(Structure_Code)Structure_Code , max(Add1)Add1,max(Add2)Add2,max(Pincode)Pincode,max(Fax)Fax,max(FromDate)FromDate,MAX(ToDate)ToDate,max(Short_Description) AS Short_Description,max(Unit_Desc) as Unit_Desc,(TotalCrates_ItemWise) as TotalCrates_ItemWise,(TotalLtr_ItemWise) as TotalLtr_ItemWise ,MAX(ShiftType )ShiftType from 
				
				( Select TSPL_ROUTE_MASTER.Route_Desc ,TSPL_ROUTE_MASTER.Route_No,TSPL_ROUTE_MASTER.Area_Code,tspl_route_master.route_desc as Customer_Name  ,TSPL_DEMAND_BOOKING_MASTER.ShiftType,
                        case when TSPL_ITEM_MASTER.Is_Ambient = 1 then round((isnull(TSPL_DEMAND_BOOKING_DETAIL.Qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I.KG,2) else (case when TSPL_ITEM_MASTER.Is_FreshItem = 1 then round((isnull(TSPL_DEMAND_BOOKING_DETAIL.Qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I.[LTR],2) else 0 end ) end as Final_Qty
                       ,case when TSPL_ITEM_MASTER.Is_FreshItem = 1 then	round((isnull(TSPL_DEMAND_BOOKING_DETAIL.Qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I.KG,2) else 0 end as Kg_Qty , tspl_company_master.State,tspl_company_master.City_Code,tspl_company_master.Circle_No,tspl_company_master.Phone1,tspl_company_master.Phone2,   tspl_company_master.Comp_Name
                       ,tspl_company_master.Add1,tspl_company_master.Add2,tspl_company_master.Pincode,tspl_company_master.Fax, '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' as FromDate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "' as ToDate, TSPL_ITEM_MASTER.Sku_Seq , TSPL_DEMAND_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Structure_Code as Structure_Code,TSPL_ITEM_MASTER.Short_Description as Short_Description,TSPL_UNIT_MASTER.Unit_Desc,TSPL_DEMAND_BOOKING_DETAIL.TotalLtr_ItemWise
                       ,TSPL_DEMAND_BOOKING_DETAIL.TotalCrates_ItemWise ,(CASE WHEN TSPL_ITEM_MASTER.Is_Milk_Pouch=0 THEN TSPL_DEMAND_BOOKING_DETAIL.Qty ELSE 0 END) as ProdQ
					   from TSPL_DEMAND_BOOKING_MASTER  Left outer join TSPL_DEMAND_BOOKING_DETAIL On TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No 
                Left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code left outer join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.Unit_Code=TSPL_DEMAND_BOOKING_DETAIL.Unit_code left join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code   and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_DEMAND_BOOKING_DETAIL.Unit_Code 
                left outer join tspl_company_master on 2 = 2 left join (  SELECT * FROM ( select item_code,uom_code,conversion_factor from TSPL_ITEM_UOM_DETAIL) I  PIVOT (Max(conversion_factor) FOR uom_code IN ( [KG],[LTR] )) P ) I ON TSPL_DEMAND_BOOKING_DETAIL.Item_Code = I.item_code 
                Left Join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = TSPL_DEMAND_BOOKING_master.Route_No 
                where 2 = 2 "

            'qry += "   and  convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date) BETWEEN CONVERT(DATE, '" + clsCommon.GetPrintDate(txtFromDate.Value) + "', 103) and CONVERT(DATE, '" + clsCommon.GetPrintDate(txtToDate.Value) + "', 103) and  Is_FreshItem = 1 and tspl_route_master.Route_no in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ") " & whrclsShift & "

            qry += " and Cast(TSPL_DEMAND_BOOKING_MASTER.Document_Date as Date) >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(clsCommon.myCDate(txtFromDate.Value)), "dd/MMM/yyyy") + "' and Cast(TSPL_DEMAND_BOOKING_MASTER.Document_Date as Date) <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.myCDate(txtToDate.Value)), "dd/MMM/yyyy") + "'"

            If clsCommon.CompairString(clsCommon.myCstr(txtFromShift.Text), "E") = CompairStringResult.Equal Then
                qry += " and 2=( case when Cast(TSPL_DEMAND_BOOKING_MASTER.Document_Date as Date) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(clsCommon.myCDate(txtFromDate.Value)), "dd/MMM/yyyy") + "' and Cast(TSPL_DEMAND_BOOKING_MASTER.Document_Date as Date) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.myCDate(txtFromDate.Value)), "dd/MMM/yyyy") + "' and TSPL_DEMAND_BOOKING_MASTER.ShiftType='Morning' then 3 else 2 end  )"
            End If
            If clsCommon.CompairString(clsCommon.myCstr(txtToShift.Text), "M") = CompairStringResult.Equal Then
                qry += " and 2=( case when Cast(TSPL_DEMAND_BOOKING_MASTER.Document_Date as Date) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(clsCommon.myCDate(txtToDate.Value)), "dd/MMM/yyyy") + "' and Cast(TSPL_DEMAND_BOOKING_MASTER.Document_Date as Date) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.myCDate(txtToDate.Value)), "dd/MMM/yyyy") + "' and TSPL_DEMAND_BOOKING_MASTER.ShiftType='Evening' then 3 else 2 end  )"
            End If
            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                qry += " and tspl_route_master.Route_no in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ") "
            End If
            qry += " and  Is_FreshItem = 1 "

            qry += " )  XXXFirst 
                where Item_Code is  not null and Area_Code is not null  group by  XXXFirst.TotalCrates_ItemWise,XXXFirst.TotalLtr_ItemWise, XXXFirst.Route_Desc,XXXFirst.Route_No, XXXFirst.Item_Code,XXXFirst.Sku_Seq,XXXFirst.Area_Code) xx)PP  ORDER BY Structure_Code "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.KwalitySalesReport, dt, "crptRouteWiseReport", "")
                'frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, dt2, "crptRouteWiseReport", "", "rptRoutWise")

            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to display", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub

    Private Sub txtRoute__My_Click(sender As Object, e As EventArgs) Handles txtRoute._My_Click
        Dim strQry As String = Nothing
        strQry = "SELECT [Route_No] as Code,[Route_Desc] as Name,Type FROM [TSPL_ROUTE_MASTER] "
        txtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("RouteWisedata", strQry, "Code", "Name", txtRoute.arrValueMember, txtRoute.arrDispalyMember)

    End Sub


End Class