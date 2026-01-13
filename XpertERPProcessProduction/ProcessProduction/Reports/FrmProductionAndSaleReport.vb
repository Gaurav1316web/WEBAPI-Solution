Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports System
Imports System.IO
Imports Telerik.WinControls.UI.Export
Imports System.Text

Public Class FrmProductionAndSaleReport
    Inherits FrmMainTranScreen
    Dim Slot1FD As DateTime = Nothing
    Dim Slot1TD As DateTime = Nothing
    Dim Slot2FD As DateTime = Nothing
    Dim Slot2TD As DateTime = Nothing
    Dim Slot3FD As DateTime = Nothing
    Dim Slot3TD As DateTime = Nothing
    Dim Slot4FD As DateTime = Nothing
    Dim Slot4TD As DateTime = Nothing
#Region "Variables"
    Dim buttontooltip As ToolTip = New ToolTip()
    Dim DayCount As Int16 = 0
    Dim arrLoc As String
    'Dim Loc_Desc_Name As String = Nothing
    Dim Loc_Desc_Code As String = Nothing
    Dim Loc_Desc_Name As New StringBuilder()
#End Region


    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub

    Private Sub FrmProductionAndSaleReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.R AndAlso btnReport.Enabled Then
            fillGridReport(False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.E AndAlso btnreset.Enabled Then
            reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub
    Private Sub FrmProductionAndSaleReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LOCATIONRIGTHS()
        SetUserMgmtNew()
        buttontooltip.SetToolTip(btnReport, "Press Alt+R for Summary ")
        buttontooltip.SetToolTip(btnReset, "Press Alt+E for Reset ")
        buttontooltip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        'lbltoDate.Visible = False
        'ToDate.Visible = False

    End Sub

    Private Sub LOCATIONRIGTHS()
        Dim obj As New clsMCCCodes()
        Try
            obj = clsMCCCodes.GetData()
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                    arrLoc = obj.arrLocCodes
                End If
            End If

            'If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
            '    If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
            '        Dim locList As New List(Of String)
            '        For Each loc As String In obj.arrLocCodes.Split(","c)
            '            Dim cleanVal As String = loc.Replace("'", "").Trim()
            '            If cleanVal.ToUpper() <> "RCDF" Then
            '                locList.Add("'" & cleanVal & "'")
            '            End If
            '        Next
            '        'arrLoc = obj.arrLocCodes
            '        arrLoc = String.Join(",", locList)
            '        'arrLoc = clsCommon.myCstr(locList)
            '    End If
            'End If



            Dim Loc_Desc As String = " Select Loc_Short_Name,Location_Code from TSPL_LOCATION_MASTER WHERE LOCATION_CODE In (" & arrLoc & ") and isMainPlant=0  "
            Dim dt As DataTable = (clsDBFuncationality.GetDataTable(Loc_Desc))
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    If i = 0 Then
                        Loc_Desc_Name.Append("'" & clsCommon.myCstr(dt.Rows(i)("Loc_Short_Name")) & "' ")
                        'Loc_Desc_Code.Append("'" & clsCommon.myCstr(dt.Rows(i)("Location_Code")) & "' ")
                        Loc_Desc_Code += " '" + clsCommon.myCstr(dt.Rows(i)("Location_Code")) + "'"

                    Else
                        Loc_Desc_Name.Append(", '" & clsCommon.myCstr(dt.Rows(i)("Loc_Short_Name")) & "' ")
                        'Loc_Desc_Code.Append("'" & clsCommon.myCstr(dt.Rows(i)("Location_Code")) & "' ")
                        Loc_Desc_Code += " ,'" + clsCommon.myCstr(dt.Rows(i)("Location_Code")) + "'"

                    End If
                Next
                'Loc_Desc_Code = 
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message)
        Finally
            obj = Nothing
        End Try
    End Sub

    'Public Class clsDateDetail
    '    Public dtCurrent As Date? = Nothing
    '    Public dtNext As Date? = Nothing

    'End Class


    Public Sub fillGridReport(ByVal Print As Boolean)
        Try
            Dim itemcodeqry As String
            Dim Date2 As Date = fromDate.Value.AddDays(-2)
            'Dim dtBreakDownCode As DataTable
            Dim queryStock As String
            Dim query As String
            gv1.ViewDefinition = New TableViewDefinition
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            Dim itemcode As String = ""
            Dim Location_code As String = ""
            Dim Month As Integer = fromDate.Value.Month
            Dim Year As Integer = fromDate.Value.Year
            Dim dttop3item As DataTable = Nothing
            Dim dtfinal As DataTable = New DataTable()
            Dim fDate As DateTime = Nothing
            Dim tDate As DateTime = Nothing
            Dim tDate1 As DateTime = Nothing
            Dim dtcurrent As DateTime = Nothing
            Dim dtnext As DateTime = Nothing
            Dim Status As String = ""
            Dim Status1 As String = ""
            Dim FG As String = ""
            Dim SFG As String = ""
            Dim FGSFG As String = ""
            Dim StatusInvoice As String = ""
            Dim StatusReturn As String = ""
            Dim StatusScrap As String = ""
            Dim StatusScrapInvoice As String = ""
            Dim Stocktransferdispatch As String = ""
            Dim stocktransferinvoice As String = ""

            If rdbPosted.IsChecked = True Then
                Status = " AND TSPL_SD_SHIPMENT_HEAD.Status=1 "
                Status1 = " AND TSPL_SPP_PRODUCTION_ENTRY.posted=1 "
                StatusInvoice = " AND TSPL_SD_SALE_INVOICE_HEAD.Status=1 "
                StatusReturn = " AND TSPL_SD_SALE_RETURN_HEAD.Status=1 "
                StatusScrap = " And TSPL_SCRAPSALE_HEAD.ispost=1  "
                StatusScrapInvoice = " And TSPL_SCRAPINVOICE_HEAD.ispost=1  "
            ElseIf rdbUnposted.IsChecked = True Then
                Status = " AND TSPL_SD_SHIPMENT_HEAD.Status=0 "
                Status1 = " AND TSPL_SPP_PRODUCTION_ENTRY.posted=0 "
                StatusInvoice = " AND TSPL_SD_SALE_INVOICE_HEAD.Status=0 "
                StatusReturn = " AND TSPL_SD_SALE_RETURN_HEAD.Status=0 "
                StatusScrap = " And TSPL_SCRAPSALE_HEAD.ispost=0  "
                StatusScrapInvoice = " And TSPL_SCRAPINVOICE_HEAD.ispost=0  "
            ElseIf rdbAll.IsChecked = True Then

            End If

            If rdbStockTransfer.IsChecked = True Then
                Stocktransferdispatch = " and TSPL_SD_SHIPMENT_HEAD.Inter_unit_sale=1 "
                stocktransferinvoice = " and TSPL_SD_SALE_INVOICE_HEAD.Inter_unit_sale=1 "
            Else
                Stocktransferdispatch = " and TSPL_SD_SHIPMENT_HEAD.Inter_unit_sale=0 "
                stocktransferinvoice = " and TSPL_SD_SALE_INVOICE_HEAD.Inter_unit_sale=0 "
            End If
            If rdbSaleTransfer.IsChecked = True OrElse rdbSale.IsChecked = True Then
                Stocktransferdispatch = " and TSPL_SD_SHIPMENT_HEAD.Inter_unit_sale=0 "
                stocktransferinvoice = " and TSPL_SD_SALE_INVOICE_HEAD.Inter_unit_sale=0 "
            End If

            If rdbFG.IsChecked = True Then
                FG = " TSPL_Item_Master.FG_for_CF_RPT=1 "
            ElseIf rdbSFG.IsChecked = True Then
                SFG = " TSPL_Item_Master.SFG_for_CF=1 "
            ElseIf rdbfgsfg.IsChecked = True Then
                FGSFG = " TSPL_Item_Master.FG_for_CF=1 "
            End If
            'DayCount = DateDiff(DateInterval.Day, fDate, tDate) + 1
            'Dim tDate As DateTime = New DateTime(Year, Month, DateTime.DaysInMonth(Year, Month))
            ',TSPL_LOCATION_MASTER.location_code as [Location Code]
            If rdbDaily.IsChecked = True Then
                fDate = New DateTime(Year, Month, 1)
                tDate = clsCommon.GetDateWithEndTime(fromDate.Value)
                DayCount = DateDiff(DateInterval.Day, fDate, tDate) + 1
                Dim dtLocation As DataTable = clsDBFuncationality.GetDataTable("SELECT LOCATION_CODE FROM TSPL_LOCATION_MASTER where TSPL_LOCATION_MASTER.IsMainPlant='0' and TSPL_LOCATION_MASTER.Rejected_Type='N'")
                'Dim dtItem As DataTable = clsDBFuncationality.GetDataTable("select Item_Code from TSPL_ITEM_MASTER where Structure_Code='FG' and Item_Desc like '%SARAS%'")
                Dim dtItem As DataTable = clsDBFuncationality.GetDataTable("select Item_Code from TSPL_ITEM_MASTER where FG_for_CF_RPT=1")
                queryStock = ""
                'For ll As Integer = 0 To dtLocation.Rows.Count - 1
                '    For ii As Integer = 0 To dtItem.Rows.Count - 1
                '        If clsCommon.myLen(queryStock) > 0 Then
                '            queryStock += " UNION ALL "
                '        End If
                '        queryStock += " select '" + clsCommon.myCstr(dtItem.Rows(ii).Item("Item_Code")) + "' AS Item_Code,'" + clsCommon.myCstr(dtLocation.Rows(ll).Item("LOCATION_CODE")) + "' AS LOCATION_CODE, "
                '        queryStock += clsCommon.myCstr(clsItemLocationDetails.getBalance1(clsCommon.myCstr(dtItem.Rows(ii).Item("Item_Code")), clsCommon.myCstr(dtLocation.Rows(ll).Item("LOCATION_CODE")), "", tDate, Nothing, clsCommon.myCstr("MT"), 0))
                '        queryStock += " as Qty"
                '    Next
                'Next
                'If clsCommon.myLen(queryStock) <= 0 Then
                '    Throw New Exception("No data found to display")
                'End If
                'queryStock = "select ST.LOCATION_CODE,sum(ST.Qty) as Qty from (" + queryStock + ") ST GROUP BY ST.LOCATION_CODE "
                'For ll As Integer = 0 To dtLocation.Rows.Count - 1
                '    itemcodeqry = "select top 3 ITEM_CODE,Location_Code from (SELECT FINAL.Location_Code,FINAL.Location_Desc,FINAL.Add1,FINAL.Add2,FINAL.Add3,FINAL.Add4,FINAL.ITEM_CODE, FINAL.Item_Desc,FINAL.Unit,FINAL.STOCK_QTY,FINAL.QTY_FOR_DAYS FROM (select xx.Item_Desc,xx.ITEM_CODE,xx.Location_Code,xx.Location_Desc,xx.Add1,xx.Add2,xx.Add3,xx.Add4, case when xx.UOM='KG' THEN CASE WHEN ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)=0 THEN xx.UOM ELSE TSPL_ITEM_UOM_DETAIL.UOM_Code END WHEN xx.UOM='GM' THEN CASE WHEN ISNULL(KG_UOM_DETAIL.Conversion_Factor,0)=0 THEN xx.UOM ELSE KG_UOM_DETAIL.UOM_Code END ELSE xx.UOM END AS 'Unit',CAST((case when xx.UOM='KG' THEN CASE WHEN ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)<>0 THEN (xx.STOCK_QTY/ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1)) ELSE xx.STOCK_QTY END WHEN xx.UOM='GM' THEN CASE WHEN ISNULL(KG_UOM_DETAIL.Conversion_Factor,0)<>0 THEN (xx.STOCK_QTY/ISNULL(KG_UOM_DETAIL.Conversion_Factor,1)) ELSE xx.STOCK_QTY END ELSE xx.STOCK_QTY END) AS numeric(10,0)) AS 'STOCK_QTY',cast(xx.QTY_FOR_DAYS as integer) as QTY_FOR_DAYS from (SELECT max(RM_STOCK_DAYS.Location_Code)Location_Code,max(RM_STOCK_DAYS.Location_Desc)Location_Desc,max(RM_STOCK_DAYS.Add1)Add1,max(RM_STOCK_DAYS.Add2)Add2,max(RM_STOCK_DAYS.Add3)Add3,max(RM_STOCK_DAYS.Add4)Add4, RM_STOCK_DAYS.ITEM_CODE,max(RM_STOCK_DAYS.Item_Desc) as Item_Desc,max(RM_STOCK_DAYS.UOM) AS 'UOM',SUM(ISNULL(RM_STOCK_DAYS.STOCK_QTY,0)) AS 'STOCK_QTY', SUM(ISNULL(RM_STOCK_DAYS.REQ_STOCK,0)) AS REQ_STOCK,SUM(ISNULL(RM_STOCK_DAYS.MIN_LEVEL,0)) AS MIN_LEVEL, CASE WHEN SUM(ISNULL(RM_STOCK_DAYS.REQ_STOCK,0))<>0 THEN SUM(ISNULL(RM_STOCK_DAYS.STOCK_QTY,0))/SUM(ISNULL(RM_STOCK_DAYS.REQ_STOCK,0)) ELSE CASE WHEN SUM(ISNULL(RM_STOCK_DAYS.MIN_LEVEL,0))<>0 THEN SUM(ISNULL(RM_STOCK_DAYS.STOCK_QTY,0))/SUM(ISNULL(RM_STOCK_DAYS.MIN_LEVEL,0)) ELSE 0 END END AS 'QTY_FOR_DAYS' FROM (SELECT max(RM_STOCK.Location_Code)Location_Code,max(RM_STOCK.Location_Desc)Location_Desc,max(RM_STOCK.Add1)Add1,max(RM_STOCK.Add2)Add2,max(RM_STOCK.Add3)Add3,max(RM_STOCK.Add4)Add4, RM_STOCK.ITEM_CODE,max(RM_STOCK.Item_Desc) as Item_Desc,MAX(RM_STOCK.UOM) AS 'UOM',SUM(ISNULL(RM_STOCK.IN_STOCK_QTY,0))-SUM(ISNULL(RM_STOCK.OUT_STOCK_QTY,0)) AS 'STOCK_QTY',0 AS 'REQ_STOCK', 0 AS 'MIN_LEVEL' FROM ( SELECT TSPL_LOCATION_MASTER.Location_Code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_LOCATION_MASTER.Add1,TSPL_LOCATION_MASTER.Add2,TSPL_LOCATION_MASTER.Add3,TSPL_LOCATION_MASTER.Add4, TSPL_INVENTORY_MOVEMENT.Item_Code AS 'ITEM_CODE',TSPL_ITEM_MASTER.Short_Description AS 'ITEM_DESC',TSPL_ITEM_MASTER.Unit_Code AS 'UOM', CASE WHEN INOUT='I' THEN STOCK_QTY END AS 'IN_STOCK_QTY',CASE WHEN INOUT='O' THEN STOCK_QTY END AS 'OUT_STOCK_QTY' FROM TSPL_INVENTORY_MOVEMENT LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT.Item_Code left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_INVENTORY_MOVEMENT.Location_Code WHERE 2=2 And TSPL_INVENTORY_MOVEMENT.Location_Code IN ('" + clsCommon.myCstr(dtLocation.Rows(ll).Item("LOCATION_CODE")) + "') AND TSPL_ITEM_MASTER.Structure_Code IN ('RM')and tspl_item_master.Item_Code in (select distinct TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE from TSPL_SPP_PRODUCTION_ENTRY_DETAIL left outer join TSPL_SPP_PRODUCTION_ENTRY on TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE left outer join TSPL_MF_BOM_HEAD on TSPL_MF_BOM_HEAD.BOM_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.BOM_CODE left outer join TSPL_MF_BOM_DETAIL on TSPL_MF_BOM_DETAIL.BOM_CODE=TSPL_MF_BOM_HEAD.BOM_CODE left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE where CONVERT(DATE,PROD_DATE,103)>= convert(date,'" + clsCommon.GetPrintDate(fDate, "dd/MMM/yyyy") + "',103) and CONVERT(DATE,PROD_DATE,103)<= convert(date,'" + clsCommon.GetPrintDate(fDate, "dd/MMM/yyyy") + "',103) and TSPL_SPP_PRODUCTION_ENTRY.LOCATION_CODE='" + clsCommon.myCstr(dtLocation.Rows(ll).Item("LOCATION_CODE")) + "' and TSPL_ITEM_MASTER.FG_for_CF_PL=1 ) ) RM_STOCK GROUP BY RM_STOCK.Item_Code UNION ALL SELECT max(TSPL_LOCATION_MASTER.Location_Code)LocationCode,max(TSPL_LOCATION_MASTER.Location_Desc)LocDes,max(TSPL_LOCATION_MASTER.Add1)Add1,max(TSPL_LOCATION_MASTER.Add2)Add2,max(TSPL_LOCATION_MASTER.Add3)Add3,max(TSPL_LOCATION_MASTER.Add4)Add4, TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE,max(TSPL_ITEM_MASTER.Short_Description) as Item_Desc,max(TSPL_ITEM_MASTER.Unit_Code) as 'UOM', 0 AS 'STOCK_QTY', AVG(CASE WHEN TSPL_MF_BOM_DETAIL.Percentage>0 THEN (TSPL_MF_BOM_DETAIL.Percentage*TSPL_LOCATION_MASTER.Silo_Capacity*1000)/100 ELSE CASE WHEN TSPL_MF_BOM_DETAIL.CONSM_QUANTITY>0 THEN ((TSPL_MF_BOM_DETAIL.CONSM_QUANTITY*TSPL_LOCATION_MASTER.Silo_Capacity*1000)/TSPL_MF_BOM_HEAD.PROD_QUANTITY) ELSE 0 END END) AS 'REQ_STOCK',0 AS 'MIN_LEVEL' FROM TSPL_MF_BOM_HEAD LEFT OUTER JOIN TSPL_MF_BOM_DETAIL ON TSPL_MF_BOM_DETAIL.BOM_CODE=TSPL_MF_BOM_HEAD.BOM_CODE LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE left outer join TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code=TSPL_MF_BOM_HEAD.LOCATION_CODE INNER join (select PROD_ITEM_CODE,MAX(BOM_CODE) AS 'BOM_CODE',MAX(REVISION_NO) AS 'REVISION_NO' from TSPL_MF_BOM_HEAD WHERE 2=2 and TSPL_MF_BOM_HEAD.LOCATION_CODE IN ('" + clsCommon.myCstr(dtLocation.Rows(ll).Item("LOCATION_CODE")) + "') GROUP BY PROD_ITEM_CODE ) BOM_LATEST ON BOM_LATEST.BOM_CODE=TSPL_MF_BOM_HEAD.BOM_CODE AND BOM_LATEST.REVISION_NO=TSPL_MF_BOM_HEAD.REVISION_NO WHERE TSPL_ITEM_MASTER.Structure_Code IN ('RM') and tspl_item_master.Item_Code in((select distinct TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE from TSPL_SPP_PRODUCTION_ENTRY_DETAIL left outer join TSPL_SPP_PRODUCTION_ENTRY on TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE left outer join TSPL_MF_BOM_HEAD on TSPL_MF_BOM_HEAD.BOM_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.BOM_CODE left outer join TSPL_MF_BOM_DETAIL on TSPL_MF_BOM_DETAIL.BOM_CODE=TSPL_MF_BOM_HEAD.BOM_CODE left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE where CONVERT(DATE,PROD_DATE,103)>= convert(date,'" + clsCommon.GetPrintDate(fDate, "dd/MMM/yyyy") + "',103) and CONVERT(DATE,PROD_DATE,103)<= convert(date,'" + clsCommon.GetPrintDate(fDate, "dd/MMM/yyyy") + "',103) and TSPL_SPP_PRODUCTION_ENTRY.LOCATION_CODE='" + clsCommon.myCstr(dtLocation.Rows(ll).Item("LOCATION_CODE")) + "' and TSPL_ITEM_MASTER.FG_for_CF_PL=1 ) ) and TSPL_MF_BOM_HEAD.LOCATION_CODE IN ('" + clsCommon.myCstr(dtLocation.Rows(ll).Item("LOCATION_CODE")) + "') GROUP BY TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE ) RM_STOCK_DAYS GROUP BY RM_STOCK_DAYS.ITEM_CODE )xx left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=xx.ITEM_CODE and UPPER(TSPL_ITEM_UOM_DETAIL.UOM_Code)='QTL' left outer join TSPL_ITEM_UOM_DETAIL KG_UOM_DETAIL on KG_UOM_DETAIL.Item_Code=xx.ITEM_CODE and UPPER(KG_UOM_DETAIL.UOM_Code)='KG' WHERE xx.STOCK_QTY>0 ) FINAL WHERE FINAL.QTY_FOR_DAYS<=3 )YYY "
                '    dttop3item = clsDBFuncationality.GetDataTable(itemcodeqry)
                '    If dttop3item IsNot Nothing Then
                '        dtfinal.Merge(dttop3item)
                '    End If
                'Next




                'For kk As Integer = 0 To dtfinal.Rows.Count - 1
                '    If clsCommon.myLen(itemcode) > 0 Then
                '        itemcode += "Union all select "
                '        itemcode += " '" + clsCommon.myCstr(dtfinal.Rows(kk)("ITEM_CODE")) + "' as ITEM_CODE "
                '        itemcode += ", '" + clsCommon.myCstr(dtfinal.Rows(kk)("Location_Code")) + "' As Location_Code "
                '    Else
                '        itemcode = " select "
                '        itemcode += "'" + clsCommon.myCstr(dtfinal.Rows(kk)("ITEM_CODE")) + "'  as ITEM_CODE   "
                '        itemcode += ", '" + clsCommon.myCstr(dtfinal.Rows(kk)("Location_Code")) + "' As Location_Code "
                '    End If
                'Next

                'Dim strLocation As String = " Select "
                'Dim strQry1 As String = " "
                'For i As Integer = 0 To dtfinal.Rows.Count - 1
                '    If clsCommon.CompairString(clsCommon.myCstr(dtfinal.Rows(i)("Location_Code")), strLocation) <> CompairStringResult.Equal Then
                '        If clsCommon.myLen(strQry1) > 15 Then
                '            strQry1 += " Union all "
                '        End If
                '        Dim strQuery As String = "Select * from(" + itemcode + ")xyz Where Location_Code='" + clsCommon.myCstr(dtfinal.Rows(i)("Location_Code")) + "'"
                '            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQuery)
                '            Dim iRow As Integer = dt.Rows.Count - 1
                '            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                '            For ii As Integer = 0 To iRow
                '                If iRow = 2 Then
                '                    If ii = 0 Then
                '                        strQry1 += " Select "
                '                    End If
                '                    strQry1 += "  '" + clsCommon.myCstr(dt.Rows(ii)("ITEM_CODE")) + "' As ITEM_CODE_" + clsCommon.myCstr(dt.Rows(ii)("Location_Code")) + clsCommon.myCstr(ii + 1) + " ,"

                '                    ElseIf iRow = 1 Then
                '                    strQry1 += " select '" + clsCommon.myCstr(dt.Rows(ii)("ITEM_CODE")) + "' As [" + clsCommon.myCstr(dt.Rows(ii)("ITEM_CODE")) + "], '' as P ,"
                '                ElseIf iRow = 0 Then
                '                        strQry1 += " select '' as P, '' as Q,'" + clsCommon.myCstr(dt.Rows(ii)("ITEM_CODE")) + "' As ITEM_CODE_" + clsCommon.myCstr(dt.Rows(ii)("Location_Code")) + clsCommon.myCstr(ii + 1) + " ,"
                '                End If
                '                If ii = iRow Then
                '                    strQry1 += " '" + clsCommon.myCstr(dt.Rows(ii)("Location_Code")) + "' As Location_Code "
                '                    strLocation = ""
                '                    strLocation = clsCommon.myCstr(dt.Rows(ii)("Location_Code"))
                '                End If
                '            Next
                '        End If
                '        End If
                'Next
                'Dim Finalqry As String=""+strQry1+""


                'Dim StrQry As String = " " + itemcode + " "
                'varsha added stockqty less then 3days data
                If rdbDaily.IsChecked Then


                    query = " Select* from ( Select  '" + objCommonVar.CurrentUserCode + "' as UserName,  ROW_NUMBER() OVER(ORDER BY yy.Location ASC) as SNo, max(Location)Location,Max(Date)date,max(date1)date1,sum(Capacity)Capacity,sum(Noofshift)Noofshift, sum(ProdDailyQty)ProdDailyQty,
sum(ProdCumQty)ProdCumQty,sum(CUD)CUD,sum(cum)CUM,sum(CUY)CUY,
sum(saleDailyQty)saleDailyQty,	sum(SaleCumQty)SaleCumQty	,sum(FGS)FGS,	sum(PSO)PSO,sum(BreakdownHRS)BreakdownHRS,	max(BreakdownREASON)BreakdownREASON
	, CAST(ROUND(MAX(DcsSeqNo_1), 0) AS INT) AS DcsSeqNo_1,CAST(ROUND(MAX(DcsSeqNo_2), 0) AS INT) AS DcsSeqNo_2,CAST(ROUND(MAX(DcsSeqNo_3), 0) AS INT) AS DcsSeqNo_3

from ("

                End If
                query += "select ROW_NUMBER() OVER(ORDER BY TSPL_LOCATION_MASTER.Location_code ASC) as SNo
                        ,TSPL_LOCATION_MASTER.Loc_Short_Name as [Location],format(convert(date,'" + fromDate.Value + "',103), 'dd MMMM yyyy') as Date,upper(format(convert(date,'" + fromDate.Value + "',103), 'MMMM yyyy'))as Date1,
                        cast(cast((TSPL_LOCATION_MASTER.target) AS DECIMAL(18,0))/(day(eomonth('" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "'))) AS DECIMAL(18,0)) as [Capacity],
                        NoOfShift
                        ,CAST((ProdDailyQty.Qty/1000) AS DECIMAL(18,0)) as ProdDailyQty
                        ,CAST((ProdDailyQty.ProdCumQty/1000) AS DECIMAL(18,0)) as ProdCumQty
                        ,case when TSPL_LOCATION_MASTER.Silo_Capacity>0 then CAST(ProdDailyQty.Qty*100/((cast(cast((TSPL_LOCATION_MASTER.target) AS DECIMAL(18,0))/(day(eomonth('" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "'))) AS DECIMAL(18,0)))*1000) AS DECIMAL(18,0)) else 0 end as CUD
                        ,case when TSPL_LOCATION_MASTER.Silo_Capacity>0 then CAST((ProdDailyQty.ProdCumQty*100)/((cast(cast((TSPL_LOCATION_MASTER.target) AS DECIMAL(18,0))/(day(eomonth('" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "'))) AS DECIMAL(18,0)))*1000*" + clsCommon.myCstr(DayCount) + ") AS DECIMAL(18,0)) else 0 end as CUM
                        ,case when TSPL_LOCATION_MASTER.Silo_Capacity>0 then CAST((ProdDailyQty.ProdCumQty*100)/((cast(cast((TSPL_LOCATION_MASTER.target) AS DECIMAL(18,0))/(day(eomonth('" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "'))) AS DECIMAL(18,0)))*1000*" + clsCommon.myCstr(DayCount) + ") AS DECIMAL(18,0)) else 0 end as CUY
                        ,CAST(SaleDailyQty.Qty AS DECIMAL(18,0)) as SaleDailyQty
                        ,CAST(SaleDailyQty.SaleCumQty AS DECIMAL(18,0)) as SaleCumQty
                        ,CAST(FGS.Qty AS DECIMAL(18,0)) as FGS
                        ,case when isnull(PSO.Qty,0)<0 then 0 else CAST(isnull(PSO.Qty,0) AS DECIMAL(18,0)) end as PSO
                        ,BreakDown.BreakdownHRS
                        ,BreakDown.BreakdownREASON"
                If rdbDaily.IsChecked Then
                    query += "  ,0 AS DcsSeqNo_1, 0 AS DcsSeqNo_2, 0 AS DcsSeqNo_3"
                End If
                query += " FROM TSPL_LOCATION_MASTER 

                        Left outer join 
						(select count (distinct TSPL_SPP_PRODUCTION_ENTRY.Shift_Code) as NoOfShift,TSPL_SPP_PRODUCTION_ENTRY.LOCATION_CODE  from TSPL_SPP_PRODUCTION_ENTRY 
						WHERE CONVERT (DATE,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103) BETWEEN CONVERT(DATE,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103) AND CONVERT(DATE,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103)  
                        AND TSPL_SPP_PRODUCTION_ENTRY.Shift_Code in ('A-SHIFT','B-SHIFT','C-SHIFT') 
                           Group By LOCATION_CODE,CONVERT(DATE,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103)) NoOfShift
						   ON TSPL_LOCATION_MASTER.LOCATION_CODE = NoOfShift.LOCATION_CODE

                         LEFT OUTER JOIN (Select Sum(Qty)Qty,Sum(ProdCumQty)ProdCumQty,LOCATION_CODE FROM "
                If Productionchk.IsChecked = True Then
                    query += " (select sum(case when convert(date,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103)=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103) then  TSPL_SPP_PRODUCTION_ENTRY_DETAIL.FINAL_PRODUCTION_QTY else 0  end) as Qty, 
						 sum(case when (convert(date,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103)>=convert(date,'" + clsCommon.GetPrintDate(fDate, "dd/MMM/yyyy") + "',103)
                         and convert(date,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103)<=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103)) then  TSPL_SPP_PRODUCTION_ENTRY_DETAIL.FINAL_PRODUCTION_QTY else 0  end) as ProdCumQty, "
                ElseIf RePrdntchk.IsChecked = True Then
                    query += " (select sum(case when convert(date,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103)=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103) then 
                               (TSPL_SPP_PRODUCTION_ENTRY_DETAIL.FINAL_PRODUCTION_QTY-TSPL_SPP_PRODUCTION_ENTRY_DETAIL.Reprocess_Qty) else 0  end) as Qty, 
						 sum(case when (convert(date,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103)>=convert(date,'" + clsCommon.GetPrintDate(fDate, "dd/MMM/yyyy") + "',103)
                         and convert(date,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103)<=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103)) then 
                         (TSPL_SPP_PRODUCTION_ENTRY_DETAIL.FINAL_PRODUCTION_QTY-TSPL_SPP_PRODUCTION_ENTRY_DETAIL.Reprocess_Qty) else 0  end) as ProdCumQty, "
                ElseIf Prdncreallchk.IsChecked = True Then
                    query += " (select sum(case when convert(date,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103)=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103) then  TSPL_SPP_PRODUCTION_ENTRY_DETAIL.Reprocess_Qty else 0  end) as Qty, 
						 sum(case when (convert(date,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103)>=convert(date,'" + clsCommon.GetPrintDate(fDate, "dd/MMM/yyyy") + "',103)
                         and convert(date,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103)<=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103)) then
                         TSPL_SPP_PRODUCTION_ENTRY_DETAIL.Reprocess_Qty else 0  end) as ProdCumQty, "
                End If

                query += "
                        TSPL_SPP_PRODUCTION_ENTRY.LOCATION_CODE from TSPL_SPP_PRODUCTION_ENTRY_DETAIL
                         left join TSPL_SPP_PRODUCTION_ENTRY on TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE
                         LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE
                         where  "

                query += " " + FG + " " + SFG + " " + FGSFG + " " + Status1 + " "


                query += "  and convert(date,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103)>=convert(date,'" + clsCommon.GetPrintDate(fDate, "dd/MMM/yyyy") + "',103)
                         and convert(date,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103)<=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103) 
                          GROUP BY TSPL_SPP_PRODUCTION_ENTRY.LOCATION_CODE
union all
						   Select sum(case when convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103)=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103) then TSPL_ADJUSTMENT_DETAIL.Item_Quantity else 0 end) as Qty,
						  sum(case when (convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(fDate, "dd/MMM/yyyy") + "',103)
                         and convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103)) then  TSPL_ADJUSTMENT_DETAIL.Item_Quantity else 0  end) as ProdCumQty,
						 TSPL_ADJUSTMENT_HEADER.Loc_Code as LOCATION_CODE
						  from TSPL_ADJUSTMENT_DETAIL
						  left join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.Adjustment_No=TSPL_ADJUSTMENT_DETAIL.Adjustment_No
						                           LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_ADJUSTMENT_DETAIL.ITEM_CODE
												    where    TSPL_Item_Master.FG_for_CF_RPT=1     AND TSPL_ADJUSTMENT_HEADER.posted='Y'    and convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(fDate, "dd/MMM/yyyy") + "',103)
                         and convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103) 
                          GROUP BY TSPL_ADJUSTMENT_HEADER.Loc_Code 

)XX Group by LOCATION_CODE) ProdDailyQty
                          ON TSPL_LOCATION_MASTER.LOCATION_CODE =ProdDailyQty.LOCATION_CODE "

                If rdbSaleTransfer.IsChecked = True AndAlso rdbDispatch.IsChecked = True Then
                    query += "  LEFT OUTER JOIN(
                        Select Qty,Bill_To_Location from
                        (Select Sum(xx.Qty-xx.ReturnQty)Qty,xx.Bill_To_Location as Bill_To_Location from
                        (Select Sum(xx.SaleQty)Qty,Sum(ReturnQty)ReturnQty,Bill_To_Location from(
                        SELECT SUM((isnull(TSPL_SD_SHIPMENT_DETAIL.Qty,0)*FromUOM.Conversion_Factor)/ToUOM.Conversion_Factor) AS SaleQty,0 as ReturnQty,
                         TSPL_SD_SHIPMENT_HEAD.Bill_To_Location FROM 
                        TSPL_SD_SHIPMENT_DETAIL left join 
                        TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code=TSPL_SD_SHIPMENT_DETAIL.document_code
                        LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SD_SHIPMENT_DETAIL.ITEM_CODE
                         left outer join TSPL_ITEM_UOM_DETAIL FromUOM on FromUOM.Item_Code =TSPL_SD_SHIPMENT_DETAIL.Item_Code 
						AND FromUOM.UOM_Code=TSPL_SD_SHIPMENT_DETAIL.Unit_code
						left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SD_SHIPMENT_DETAIL.item_code and ToUOM.UOM_Code='MT'
                        WHERE  "
                    query += "" + FG + " " + SFG + " " + FGSFG + " " + Status + " "
                    query += " and convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103)=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103)
                        GROUP BY TSPL_SD_SHIPMENT_HEAD.Bill_To_Location

                        union all
						SELECT SUM((isnull(TSPL_SCRAPSALE_DETAIL.shipped_Qty,0)*FromUOM.Conversion_Factor)/ToUOM.Conversion_Factor) AS SaleQty,0 as ReturnQty,TSPL_SCRAPSALE_HEAD.Loc_Code FROM 
                        TSPL_SCRAPSALE_DETAIL left join 
                        TSPL_SCRAPSALE_HEAD on TSPL_SCRAPSALE_HEAD.shipment_No=TSPL_SCRAPSALE_DETAIL.shipment_No
                        LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SCRAPSALE_DETAIL.ITEM_CODE
                         left outer join TSPL_ITEM_UOM_DETAIL FromUOM on FromUOM.Item_Code =TSPL_SCRAPSALE_DETAIL.Item_Code 
						AND FromUOM.UOM_Code=TSPL_SCRAPSALE_DETAIL.Unit_code
						left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SCRAPSALE_DETAIL.item_code and ToUOM.UOM_Code='MT'
                        WHERE "
                    query += "" + FG + " " + SFG + " " + FGSFG + " " + StatusScrap + " "
                    query += " and convert(date,TSPL_SCRAPSALE_HEAD.shipment_Date,103)=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103)
                               GROUP BY TSPL_SCRAPSALE_HEAD.Loc_Code)XX group by xx.Bill_To_Location
                        Union all
                        SELECT 0 as SaleQty,SUM((isnull(TSPL_SD_SALE_RETURN_DETAIL.Qty,0)*FromUOM.Conversion_Factor)/ToUOM.Conversion_Factor) AS ReturnQty,TSPL_SD_SALE_RETURN_HEAD.Bill_To_Location FROM 
                        TSPL_SD_SALE_RETURN_DETAIL left join 
                        TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.document_code=TSPL_SD_SALE_RETURN_DETAIL.document_code
                        LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.ITEM_CODE
                        left outer join TSPL_ITEM_UOM_DETAIL FromUOM on FromUOM.Item_Code =TSPL_SD_SALE_RETURN_DETAIL.Item_Code 
						AND FromUOM.UOM_Code=TSPL_SD_SALE_RETURN_DETAIL.Unit_code
						left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SD_SALE_RETURN_DETAIL.item_code and ToUOM.UOM_Code='MT'
                        WHERE "
                    query += "" + FG + " " + SFG + " " + FGSFG + " " + StatusReturn + " "
                    query += " and convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103)
                        GROUP BY TSPL_SD_SALE_RETURN_HEAD.Bill_To_Location)XX group by XX.Bill_To_Location)SaleDailyQty
						) SaleDailyQty
                        ON TSPL_LOCATION_MASTER.LOCATION_CODE =SaleDailyQty.Bill_To_Location 

                        LEFT OUTER JOIN(
                        Select Qty,Bill_To_Location from
                        (Select Sum(xx.Qty-xx.ReturnQty)Qty,xx.Bill_To_Location as Bill_To_Location from
                        (Select Sum(xx.SaleQty)Qty,Sum(ReturnQty)ReturnQty,Bill_To_Location from(
                        SELECT SUM((isnull(TSPL_SD_SHIPMENT_DETAIL.Qty,0)*FromUOM.Conversion_Factor)/ToUOM.Conversion_Factor) AS SaleQty,0 as ReturnQty,
                         TSPL_SD_SHIPMENT_HEAD.Bill_To_Location FROM 
                        TSPL_SD_SHIPMENT_DETAIL left join 
                        TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code=TSPL_SD_SHIPMENT_DETAIL.document_code
                        LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SD_SHIPMENT_DETAIL.ITEM_CODE
                         left outer join TSPL_ITEM_UOM_DETAIL FromUOM on FromUOM.Item_Code =TSPL_SD_SHIPMENT_DETAIL.Item_Code 
						AND FromUOM.UOM_Code=TSPL_SD_SHIPMENT_DETAIL.Unit_code
						left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SD_SHIPMENT_DETAIL.item_code and ToUOM.UOM_Code='MT'
                        WHERE  "
                    query += "" + FG + " " + SFG + " " + FGSFG + " " + Status + " "
                    query += " and convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(fDate, "dd/MMM/yyyy") + "',103)
                         and convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103)
                        GROUP BY TSPL_SD_SHIPMENT_HEAD.Bill_To_Location

                        union all
						SELECT SUM((isnull(TSPL_SCRAPSALE_DETAIL.shipped_Qty,0)*FromUOM.Conversion_Factor)/ToUOM.Conversion_Factor) AS SaleQty,0 as ReturnQty,TSPL_SCRAPSALE_HEAD.Loc_Code FROM 
                        TSPL_SCRAPSALE_DETAIL left join 
                        TSPL_SCRAPSALE_HEAD on TSPL_SCRAPSALE_HEAD.shipment_No=TSPL_SCRAPSALE_DETAIL.shipment_No
                        LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SCRAPSALE_DETAIL.ITEM_CODE
                         left outer join TSPL_ITEM_UOM_DETAIL FromUOM on FromUOM.Item_Code =TSPL_SCRAPSALE_DETAIL.Item_Code 
						AND FromUOM.UOM_Code=TSPL_SCRAPSALE_DETAIL.Unit_code
						left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SCRAPSALE_DETAIL.item_code and ToUOM.UOM_Code='MT'
                        WHERE "
                    query += "" + FG + " " + SFG + " " + FGSFG + " " + StatusScrap + " "
                    query += " and convert(date,TSPL_SCRAPSALE_HEAD.shipment_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(fDate, "dd/MMM/yyyy") + "',103)
                         and convert(date,TSPL_SCRAPSALE_HEAD.shipment_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103)
                         GROUP BY TSPL_SCRAPSALE_HEAD.Loc_Code)XX group by xx.Bill_To_Location

                        Union all
                        SELECT 0 as SaleQty,SUM((isnull(TSPL_SD_SALE_RETURN_DETAIL.Qty,0)*FromUOM.Conversion_Factor)/ToUOM.Conversion_Factor) AS ReturnQty,TSPL_SD_SALE_RETURN_HEAD.Bill_To_Location FROM 
                        TSPL_SD_SALE_RETURN_DETAIL left join 
                        TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.document_code=TSPL_SD_SALE_RETURN_DETAIL.document_code
                        LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.ITEM_CODE
                        left outer join TSPL_ITEM_UOM_DETAIL FromUOM on FromUOM.Item_Code =TSPL_SD_SALE_RETURN_DETAIL.Item_Code 
						AND FromUOM.UOM_Code=TSPL_SD_SALE_RETURN_DETAIL.Unit_code
						left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SD_SALE_RETURN_DETAIL.item_code and ToUOM.UOM_Code='MT'
                        WHERE "
                    query += "" + FG + " " + SFG + " " + FGSFG + " " + StatusReturn + " "
                    query += " and convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(fDate, "dd/MMM/yyyy") + "',103)
                         and convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103)
                        GROUP BY TSPL_SD_SALE_RETURN_HEAD.Bill_To_Location)XX group by XX.Bill_To_Location)SaleCumQty
						) SaleCumQty
                        ON TSPL_LOCATION_MASTER.LOCATION_CODE =SaleCumQty.Bill_To_Location

                         LEFT OUTER JOIN(
                        Select Qty,Bill_To_Location from
                        (SELECT SUM((isnull(TSPL_SD_SHIPMENT_DETAIL.Qty,0)*FromUOM.Conversion_Factor)/ToUOM.Conversion_Factor) AS Qty,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location FROM 
                        TSPL_SD_SHIPMENT_DETAIL left join 
                        TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code=TSPL_SD_SHIPMENT_DETAIL.document_code
                        LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SD_SHIPMENT_DETAIL.ITEM_CODE
                        left outer join TSPL_ITEM_UOM_DETAIL FromUOM on FromUOM.Item_Code =TSPL_SD_SHIPMENT_DETAIL.Item_Code 
						AND FromUOM.UOM_Code=TSPL_SD_SHIPMENT_DETAIL.Unit_code
						left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SD_SHIPMENT_DETAIL.item_code and ToUOM.UOM_Code='MT'
                        WHERE  "
                    query += "" + FG + " " + SFG + " " + FGSFG + " " + Status + " "
                    query += "and convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103)=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103)
                        GROUP BY TSPL_SD_SHIPMENT_HEAD.Bill_To_Location) XX )PSO
                        ON TSPL_LOCATION_MASTER.LOCATION_CODE =PSO.Bill_To_Location 
                         LEFT OUTER JOIN
                       (select TSPL_BREAK_DOWN_ENTRY.Location_Code
                        ,sum(DATEDIFF(HOUR,Start_Time,End_Time)) AS BreakdownHRS,max(TSPL_BREAK_DOWN_MASTER.Name) as BreakdownREASON 
                         from TSPL_BREAK_DOWN_ENTRY
                        left join TSPL_BREAK_DOWN_MASTER ON TSPL_BREAK_DOWN_ENTRY.Break_Down_Code = TSPL_BREAK_DOWN_MASTER.CODE
                        left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_BREAK_DOWN_ENTRY.Location_Code
                        WHERE convert(date,TSPL_BREAK_DOWN_ENTRY.Start_Time,103)=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103) group by TSPL_BREAK_DOWN_ENTRY.Location_Code) BreakDown
                        ON TSPL_LOCATION_MASTER.LOCATION_CODE =BreakDown.Location_Code "

                ElseIf rdbDispatch.IsChecked = True Then
                    query += "  LEFT OUTER JOIN (Select Sum(Qty)Qty,sum(SaleCumQty)SaleCumQty, Bill_To_Location FROM
                        (SELECT sum(case when convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103)=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103) then  ((isnull(TSPL_SD_SHIPMENT_DETAIL.Qty,0)*FromUOM.Conversion_Factor)/ToUOM.Conversion_Factor) else 0  end) as Qty, 
						 sum(case when (convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(fDate, "dd/MMM/yyyy") + "',103)
                         and convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103)) then  ((isnull(TSPL_SD_SHIPMENT_DETAIL.Qty,0)*FromUOM.Conversion_Factor)/ToUOM.Conversion_Factor) else 0  end) as SaleCumQty,
                         TSPL_SD_SHIPMENT_HEAD.Bill_To_Location FROM 
                        TSPL_SD_SHIPMENT_DETAIL left join 
                        TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code=TSPL_SD_SHIPMENT_DETAIL.document_code
                        LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SD_SHIPMENT_DETAIL.ITEM_CODE
                         left outer join TSPL_ITEM_UOM_DETAIL FromUOM on FromUOM.Item_Code =TSPL_SD_SHIPMENT_DETAIL.Item_Code 
						AND FromUOM.UOM_Code=TSPL_SD_SHIPMENT_DETAIL.Unit_code
						left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SD_SHIPMENT_DETAIL.item_code and ToUOM.UOM_Code='MT'
                        WHERE  "
                    query += "" + FG + " " + SFG + " " + FGSFG + " " + Status + ""
                    If rdbDispatch.IsChecked = True AndAlso rdbSale.IsChecked = True Then
                        query += " and TSPL_SD_SHIPMENT_HEAD.Inter_unit_sale=0 "
                    End If
                    If rdbStockTransfer.IsChecked = True Then
                        query += "" + Stocktransferdispatch + ""
                    End If
                    query += " and convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(fDate, "dd/MMM/yyyy") + "',103)
                         and convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103)
                        GROUP BY TSPL_SD_SHIPMENT_HEAD.Bill_To_Location
                        union all
						SELECT sum(case when convert(date,TSPL_SCRAPSALE_HEAD.shipment_Date,103)=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103) then  ((isnull(TSPL_SCRAPSALE_DETAIL.shipped_Qty,0)*FromUOM.Conversion_Factor)/ToUOM.Conversion_Factor) else 0  end) as Qty, 
						 sum(case when (convert(date,TSPL_SCRAPSALE_HEAD.shipment_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(fDate, "dd/MMM/yyyy") + "',103)
                         and convert(date,TSPL_SCRAPSALE_HEAD.shipment_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103)) then  ((isnull(TSPL_SCRAPSALE_DETAIL.shipped_Qty,0)*FromUOM.Conversion_Factor)/ToUOM.Conversion_Factor) else 0  end) as SaleCumQty,
                        TSPL_SCRAPSALE_HEAD.Loc_Code FROM 
                        TSPL_SCRAPSALE_DETAIL left join 
                        TSPL_SCRAPSALE_HEAD on TSPL_SCRAPSALE_HEAD.shipment_No=TSPL_SCRAPSALE_DETAIL.shipment_No
                        LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SCRAPSALE_DETAIL.ITEM_CODE
                         left outer join TSPL_ITEM_UOM_DETAIL FromUOM on FromUOM.Item_Code =TSPL_SCRAPSALE_DETAIL.Item_Code 
						AND FromUOM.UOM_Code=TSPL_SCRAPSALE_DETAIL.Unit_code
						left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SCRAPSALE_DETAIL.item_code and ToUOM.UOM_Code='MT'
                        WHERE "
                    query += "" + FG + " " + SFG + " " + FGSFG + " " + StatusScrap + ""
                    If rdbDispatch.IsChecked = True AndAlso rdbSale.IsChecked = True Then
                        query += " and TSPL_SCRAPSALE_HEAD.Inter_unit_sale=0 "
                    End If
                    If rdbStockTransfer.IsChecked = True Then
                        query += " and TSPL_SCRAPSALE_HEAD.Inter_unit_sale=1  "
                    End If
                    query += " and convert(date,TSPL_SCRAPSALE_HEAD.shipment_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(fDate, "dd/MMM/yyyy") + "',103)
                         and convert(date,TSPL_SCRAPSALE_HEAD.shipment_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103)
						GROUP BY TSPL_SCRAPSALE_HEAD.Loc_Code
						) SaleDailyQty group by SaleDailyQty.Bill_To_Location
                        ) SaleDailyQty
                        ON TSPL_LOCATION_MASTER.LOCATION_CODE =SaleDailyQty.Bill_To_Location 
                        LEFT OUTER JOIN
                        (SELECT SUM(isnull(TSPL_SD_SHIPMENT_HEAD.Order_Qty,0))-SUM((isnull(TSPL_SD_SHIPMENT_DETAIL.Qty,0)*FromUOM.Conversion_Factor)/ToUOM.Conversion_Factor) AS Qty,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location FROM 
                        TSPL_SD_SHIPMENT_DETAIL left join 
                        TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code=TSPL_SD_SHIPMENT_DETAIL.document_code
                        LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SD_SHIPMENT_DETAIL.ITEM_CODE
                        left outer join TSPL_ITEM_UOM_DETAIL FromUOM on FromUOM.Item_Code =TSPL_SD_SHIPMENT_DETAIL.Item_Code 
						AND FromUOM.UOM_Code=TSPL_SD_SHIPMENT_DETAIL.Unit_code
						left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SD_SHIPMENT_DETAIL.item_code and ToUOM.UOM_Code='MT'
                        WHERE  "
                    query += "" + FG + " " + SFG + " " + FGSFG + " " + Status + "  "
                    If rdbDispatch.IsChecked = True AndAlso rdbSale.IsChecked = True Then
                        query += " and TSPL_SD_SHIPMENT_HEAD.Inter_unit_sale=0 "
                    End If
                    If rdbStockTransfer.IsChecked = True Then
                        query += "" + Stocktransferdispatch + ""
                    End If
                    query += "and convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103)=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103)
                        GROUP BY TSPL_SD_SHIPMENT_HEAD.Bill_To_Location) PSO
                        ON TSPL_LOCATION_MASTER.LOCATION_CODE =PSO.Bill_To_Location 
                         LEFT OUTER JOIN
                       (select TSPL_BREAK_DOWN_ENTRY.Location_Code
                        ,sum(DATEDIFF(HOUR,Start_Time,End_Time)) AS BreakdownHRS,max(TSPL_BREAK_DOWN_MASTER.Name) as BreakdownREASON 
                         from TSPL_BREAK_DOWN_ENTRY
                        left join TSPL_BREAK_DOWN_MASTER ON TSPL_BREAK_DOWN_ENTRY.Break_Down_Code = TSPL_BREAK_DOWN_MASTER.CODE
                        left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_BREAK_DOWN_ENTRY.Location_Code
                        WHERE convert(date,TSPL_BREAK_DOWN_ENTRY.Start_Time,103)=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103) group by TSPL_BREAK_DOWN_ENTRY.Location_Code) BreakDown
                        ON TSPL_LOCATION_MASTER.LOCATION_CODE =BreakDown.Location_Code "

                ElseIf rdbSaleTransfer.IsChecked = True AndAlso rdbInvoice.IsChecked = True Then

                    query += "  LEFT OUTER JOIN(
                          Select Qty,Bill_To_Location from
                        (Select Sum(xx.Qty-xx.ReturnQty)Qty,xx.Bill_To_Location as Bill_To_Location from(
						Select Sum(SaleQty)Qty,Sum(ReturnQty)ReturnQty,Bill_To_Location from(
                        SELECT SUM((isnull(TSPL_SD_SALE_INVOICE_DETAIL.Qty,0)*FromUOM.Conversion_Factor)/ToUOM.Conversion_Factor) AS SaleQty,0 as ReturnQty,
                         TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location FROM 
                        TSPL_SD_SALE_INVOICE_DETAIL left join 
                        TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.document_code=TSPL_SD_SALE_INVOICE_DETAIL.document_code
                        LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.ITEM_CODE
                         left outer join TSPL_ITEM_UOM_DETAIL FromUOM on FromUOM.Item_Code =TSPL_SD_SALE_INVOICE_DETAIL.Item_Code 
						AND FromUOM.UOM_Code=TSPL_SD_SALE_INVOICE_DETAIL.Unit_code
						left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SD_SALE_INVOICE_DETAIL.item_code and ToUOM.UOM_Code='MT'
                        WHERE  "
                    query += "" + FG + " " + SFG + " " + FGSFG + " " + StatusInvoice + " " + stocktransferinvoice + " "
                    query += " and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103)
                        GROUP BY TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location

                        union all
                        SELECT SUM((isnull(TSPL_SCRAPSALE_DETAIL.shipped_Qty,0)*FromUOM.Conversion_Factor)/ToUOM.Conversion_Factor) AS SaleQty,0 as ReturnQty,TSPL_SCRAPSALE_HEAD.Loc_Code FROM 
                        TSPL_SCRAPSALE_DETAIL left join 
                        TSPL_SCRAPSALE_HEAD on TSPL_SCRAPSALE_HEAD.shipment_No=TSPL_SCRAPSALE_DETAIL.shipment_No
                        LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SCRAPSALE_DETAIL.ITEM_CODE
                         left outer join TSPL_ITEM_UOM_DETAIL FromUOM on FromUOM.Item_Code =TSPL_SCRAPSALE_DETAIL.Item_Code 
						AND FromUOM.UOM_Code=TSPL_SCRAPSALE_DETAIL.Unit_code
						left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SCRAPSALE_DETAIL.item_code and ToUOM.UOM_Code='MT'
                        WHERE "
                    query += "" + FG + " " + SFG + " " + FGSFG + " " + StatusScrap + " "
                    query += " and convert(date,TSPL_SCRAPSALE_HEAD.shipment_Date,103)=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103)
                                GROUP BY TSPL_SCRAPSALE_HEAD.Loc_Code)XX group by xx.Bill_To_Location

                        Union all
                        SELECT 0 as SaleQty,SUM((isnull(TSPL_SD_SALE_RETURN_DETAIL.Qty,0)*FromUOM.Conversion_Factor)/ToUOM.Conversion_Factor) AS ReturnQty,TSPL_SD_SALE_RETURN_HEAD.Bill_To_Location FROM 
                        TSPL_SD_SALE_RETURN_DETAIL left join 
                        TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.document_code=TSPL_SD_SALE_RETURN_DETAIL.document_code
                        LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.ITEM_CODE
                        left outer join TSPL_ITEM_UOM_DETAIL FromUOM on FromUOM.Item_Code =TSPL_SD_SALE_RETURN_DETAIL.Item_Code 
						AND FromUOM.UOM_Code=TSPL_SD_SALE_RETURN_DETAIL.Unit_code
						left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SD_SALE_RETURN_DETAIL.item_code and ToUOM.UOM_Code='MT'
                        WHERE "
                    query += "" + FG + " " + SFG + " " + FGSFG + " " + StatusReturn + " "
                    query += " and convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103)
                        GROUP BY TSPL_SD_SALE_RETURN_HEAD.Bill_To_Location)XX group by xx.Bill_To_Location )SaleDailyQty
						) SaleDailyQty
                        ON TSPL_LOCATION_MASTER.LOCATION_CODE =SaleDailyQty.Bill_To_Location 

                        LEFT OUTER JOIN(
                        Select Qty,Bill_To_Location from
                        (Select Sum(xx.Qty-xx.ReturnQty)Qty,xx.Bill_To_Location as Bill_To_Location from(
						Select Sum(SaleQty)Qty,Sum(ReturnQty)ReturnQty,Bill_To_Location from
                        (SELECT SUM((isnull(TSPL_SD_SALE_INVOICE_DETAIL.Qty,0)*FromUOM.Conversion_Factor)/ToUOM.Conversion_Factor) AS SaleQty,0 as ReturnQty, 
                        TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location FROM 
                        TSPL_SD_SALE_INVOICE_DETAIL left join 
                        TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.document_code=TSPL_SD_SALE_INVOICE_DETAIL.document_code
                        LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.ITEM_CODE
                        left outer join TSPL_ITEM_UOM_DETAIL FromUOM on FromUOM.Item_Code =TSPL_SD_SALE_INVOICE_DETAIL.Item_Code 
						AND FromUOM.UOM_Code=TSPL_SD_SALE_INVOICE_DETAIL.Unit_code
						left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SD_SALE_INVOICE_DETAIL.item_code and ToUOM.UOM_Code='MT'
                        WHERE  "
                    query += " " + FG + " " + SFG + " " + FGSFG + " " + StatusInvoice + " " + stocktransferinvoice + " "
                    query += " and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(fDate, "dd/MMM/yyyy") + "',103)
                         and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103)
                        GROUP BY TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location
                        
                        union all
                        SELECT SUM((isnull(TSPL_SCRAPSALE_DETAIL.shipped_Qty,0)*FromUOM.Conversion_Factor)/ToUOM.Conversion_Factor) AS SaleQty,0 as ReturnQty,TSPL_SCRAPSALE_HEAD.Loc_Code FROM 
                        TSPL_SCRAPSALE_DETAIL left join 
                        TSPL_SCRAPSALE_HEAD on TSPL_SCRAPSALE_HEAD.shipment_No=TSPL_SCRAPSALE_DETAIL.shipment_No
                        LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SCRAPSALE_DETAIL.ITEM_CODE
                         left outer join TSPL_ITEM_UOM_DETAIL FromUOM on FromUOM.Item_Code =TSPL_SCRAPSALE_DETAIL.Item_Code 
						AND FromUOM.UOM_Code=TSPL_SCRAPSALE_DETAIL.Unit_code
						left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SCRAPSALE_DETAIL.item_code and ToUOM.UOM_Code='MT'
                        WHERE "
                    query += " " + FG + " " + SFG + " " + FGSFG + " " + StatusScrap + ""
                    query += " and convert(date,TSPL_SCRAPSALE_HEAD.shipment_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(fDate, "dd/MMM/yyyy") + "',103)
                         and convert(date,TSPL_SCRAPSALE_HEAD.shipment_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103)
                          GROUP BY TSPL_SCRAPSALE_HEAD.Loc_Code)XX group by xx.Bill_To_Location
                    union all
                        SELECT 0 as SaleQty,SUM((isnull(TSPL_SD_SALE_RETURN_DETAIL.Qty,0)*FromUOM.Conversion_Factor)/ToUOM.Conversion_Factor) AS ReturnQty,TSPL_SD_SALE_RETURN_HEAD.Bill_To_Location FROM 
                        TSPL_SD_SALE_RETURN_DETAIL left join 
                        TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.document_code=TSPL_SD_SALE_RETURN_DETAIL.document_code
                        LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.ITEM_CODE
                        left outer join TSPL_ITEM_UOM_DETAIL FromUOM on FromUOM.Item_Code =TSPL_SD_SALE_RETURN_DETAIL.Item_Code 
						AND FromUOM.UOM_Code=TSPL_SD_SALE_RETURN_DETAIL.Unit_code
						left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SD_SALE_RETURN_DETAIL.item_code and ToUOM.UOM_Code='MT'
                        WHERE "
                    query += "" + FG + " " + SFG + " " + FGSFG + " " + StatusReturn + " "
                    query += " and convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(fDate, "dd/MMM/yyyy") + "',103)
                         and convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103)
                        GROUP BY TSPL_SD_SALE_RETURN_HEAD.Bill_To_Location)XX group by xx.Bill_To_Location )SaleCumQty) SaleCumQty
                        ON TSPL_LOCATION_MASTER.LOCATION_CODE =SaleCumQty.Bill_To_Location 

                         LEFT OUTER JOIN(
                        Select Qty,Bill_To_Location from
                        (SELECT SUM((isnull(TSPL_SD_SALE_INVOICE_DETAIL.Qty,0)*FromUOM.Conversion_Factor)/ToUOM.Conversion_Factor) AS Qty,TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location FROM 
                        TSPL_SD_SALE_INVOICE_DETAIL left join 
                        TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.document_code=TSPL_SD_SALE_INVOICE_DETAIL.document_code
                        LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.ITEM_CODE
                        left outer join TSPL_ITEM_UOM_DETAIL FromUOM on FromUOM.Item_Code =TSPL_SD_SALE_INVOICE_DETAIL.Item_Code 
						AND FromUOM.UOM_Code=TSPL_SD_SALE_INVOICE_DETAIL.Unit_code
						left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SD_SALE_INVOICE_DETAIL.item_code and ToUOM.UOM_Code='MT'
                        WHERE  "
                    query += "" + FG + " " + SFG + " " + FGSFG + " " + StatusInvoice + " " + stocktransferinvoice + " "
                    query += "and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103)
                        GROUP BY TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location )XX)PSO
                        ON TSPL_LOCATION_MASTER.LOCATION_CODE =PSO.Bill_To_Location 
                         LEFT OUTER JOIN
                       (select TSPL_BREAK_DOWN_ENTRY.Location_Code
                        ,sum(DATEDIFF(HOUR,Start_Time,End_Time)) AS BreakdownHRS,max(TSPL_BREAK_DOWN_MASTER.Name) as BreakdownREASON 
                         from TSPL_BREAK_DOWN_ENTRY
                        left join TSPL_BREAK_DOWN_MASTER ON TSPL_BREAK_DOWN_ENTRY.Break_Down_Code = TSPL_BREAK_DOWN_MASTER.CODE
                        left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_BREAK_DOWN_ENTRY.Location_Code
                        WHERE convert(date,TSPL_BREAK_DOWN_ENTRY.Start_Time,103)=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103) group by TSPL_BREAK_DOWN_ENTRY.Location_Code) BreakDown
                        ON TSPL_LOCATION_MASTER.LOCATION_CODE =BreakDown.Location_Code "

                ElseIf rdbInvoice.IsChecked = True Then
                    query += "  LEFT OUTER JOIN( Select Sum(Qty)Qty,sum(SaleCumQty)SaleCumQty,Bill_To_Location from
                        (SELECT SUM((isnull(TSPL_SD_SALE_INVOICE_DETAIL.Qty,0)*FromUOM.Conversion_Factor)/ToUOM.Conversion_Factor) AS Qty,
                        sum(case when (convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(fDate, "dd/MMM/yyyy") + "',103)
                         and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103))
                         then  ((isnull(TSPL_SD_SALE_INVOICE_DETAIL.Qty,0)*FromUOM.Conversion_Factor)/ToUOM.Conversion_Factor) else 0  end) as SaleCumQty,
                         TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location FROM 
                        TSPL_SD_SALE_INVOICE_DETAIL left join 
                        TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.document_code=TSPL_SD_SALE_INVOICE_DETAIL.document_code
                        LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.ITEM_CODE
                         left outer join TSPL_ITEM_UOM_DETAIL FromUOM on FromUOM.Item_Code =TSPL_SD_SALE_INVOICE_DETAIL.Item_Code 
						AND FromUOM.UOM_Code=TSPL_SD_SALE_INVOICE_DETAIL.Unit_code
						left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SD_SALE_INVOICE_DETAIL.item_code and ToUOM.UOM_Code='MT'
                        WHERE  "
                    query += "" + FG + " " + SFG + " " + FGSFG + " " + StatusInvoice + "  "
                    If rdbInvoice.IsChecked = True AndAlso rdbSale.IsChecked = True Then
                        query += " and TSPL_SD_SALE_INVOICE_HEAD.Inter_unit_sale=0 "
                    End If
                    If rdbStockTransfer.IsChecked = True Then
                        query += "" + stocktransferinvoice + ""
                    End If
                    query += " and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103)
                        GROUP BY TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location 
                        union all
						SELECT SUM((isnull(TSPL_SCRAPINVOICE_DETAIL.shipped_Qty,0)*FromUOM.Conversion_Factor)/ToUOM.Conversion_Factor) AS Qty,
                        sum(case when (convert(date,TSPL_SCRAPINVOICE_HEAD.shipment_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(fDate, "dd/MMM/yyyy") + "',103)
                         and convert(date,TSPL_SCRAPINVOICE_HEAD.shipment_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103)) 
                        then  ((isnull(TSPL_SCRAPINVOICE_DETAIL.shipped_Qty,0)*FromUOM.Conversion_Factor)/ToUOM.Conversion_Factor) else 0  end) as SaleCumQty,
TSPL_SCRAPINVOICE_HEAD.Loc_Code FROM 
                        TSPL_SCRAPINVOICE_DETAIL left join 
                        TSPL_SCRAPINVOICE_HEAD on TSPL_SCRAPINVOICE_HEAD.invoice_No=TSPL_SCRAPINVOICE_DETAIL.invoice_No
                        LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SCRAPINVOICE_DETAIL.ITEM_CODE
                         left outer join TSPL_ITEM_UOM_DETAIL FromUOM on FromUOM.Item_Code =TSPL_SCRAPINVOICE_DETAIL.Item_Code 
						AND FromUOM.UOM_Code=TSPL_SCRAPINVOICE_DETAIL.Unit_code
						left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SCRAPINVOICE_DETAIL.item_code and ToUOM.UOM_Code='MT'
                        WHERE  "
                    query += "" + FG + " " + SFG + " " + FGSFG + " " + StatusScrapInvoice + ""
                    'If rdbDispatch.IsChecked = True AndAlso rdbSale.IsChecked = True Then
                    '    query += " and TSPL_SCRAPSALE_HEAD.Inter_unit_sale=0 "
                    'End If
                    'If rdbStockTransfer.IsChecked = True Then
                    '    query += "" + Stocktransferdispatch + ""
                    'End If
                    query += " and convert(date,TSPL_SCRAPINVOICE_HEAD.shipment_Date,103)=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103)
						GROUP BY TSPL_SCRAPINVOICE_HEAD.Loc_Code
						) SaleDailyQty group by SaleDailyQty.Bill_To_Location
                        ) SaleDailyQty
                        ON TSPL_LOCATION_MASTER.LOCATION_CODE =SaleDailyQty.Bill_To_Location 
 


                        LEFT OUTER JOIN
                        (SELECT SUM((isnull(TSPL_SD_SALE_INVOICE_DETAIL.Qty,0)*FromUOM.Conversion_Factor)/ToUOM.Conversion_Factor) AS Qty,
                        TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location FROM 
                        TSPL_SD_SALE_INVOICE_DETAIL left join 
                        TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.document_code=TSPL_SD_SALE_INVOICE_DETAIL.document_code
                        LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.ITEM_CODE
                        left outer join TSPL_ITEM_UOM_DETAIL FromUOM on FromUOM.Item_Code =TSPL_SD_SALE_INVOICE_DETAIL.Item_Code 
						AND FromUOM.UOM_Code=TSPL_SD_SALE_INVOICE_DETAIL.Unit_code
						left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SD_SALE_INVOICE_DETAIL.item_code and ToUOM.UOM_Code='MT'
                        WHERE  "
                    query += " " + FG + " " + SFG + " " + FGSFG + " " + StatusInvoice + " "
                    If rdbInvoice.IsChecked = True AndAlso rdbSale.IsChecked = True Then
                        query += " and TSPL_SD_SALE_INVOICE_HEAD.Inter_unit_sale=0 "
                    End If
                    If rdbStockTransfer.IsChecked = True Then
                        query += "" + stocktransferinvoice + ""
                    End If
                    query += " and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(fDate, "dd/MMM/yyyy") + "',103)
                         and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103)
                        GROUP BY TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location)  SaleCumQty
                        ON TSPL_LOCATION_MASTER.LOCATION_CODE =SaleCumQty.Bill_To_Location 
                         LEFT OUTER JOIN
                        (SELECT SUM((isnull(TSPL_SD_SALE_INVOICE_DETAIL.Qty,0)*FromUOM.Conversion_Factor)/ToUOM.Conversion_Factor) AS Qty,TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location FROM 
                        TSPL_SD_SALE_INVOICE_DETAIL left join 
                        TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.document_code=TSPL_SD_SALE_INVOICE_DETAIL.document_code
                        LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.ITEM_CODE
                        left outer join TSPL_ITEM_UOM_DETAIL FromUOM on FromUOM.Item_Code =TSPL_SD_SALE_INVOICE_DETAIL.Item_Code 
						AND FromUOM.UOM_Code=TSPL_SD_SALE_INVOICE_DETAIL.Unit_code
						left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SD_SALE_INVOICE_DETAIL.item_code and ToUOM.UOM_Code='MT'
                        WHERE  "
                    query += "" + FG + " " + SFG + " " + FGSFG + " " + StatusInvoice + " "
                    If rdbInvoice.IsChecked = True AndAlso rdbSale.IsChecked = True Then
                        query += " and TSPL_SD_SALE_INVOICE_HEAD.Inter_unit_sale=0 "
                    End If
                    If rdbStockTransfer.IsChecked = True Then
                        query += "" + stocktransferinvoice + ""
                    End If
                    query += "and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103)
                        GROUP BY TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location) PSO
                        ON TSPL_LOCATION_MASTER.LOCATION_CODE =PSO.Bill_To_Location 
                         LEFT OUTER JOIN
                       (select TSPL_BREAK_DOWN_ENTRY.Location_Code
                        ,sum(DATEDIFF(HOUR,Start_Time,End_Time)) AS BreakdownHRS,max(TSPL_BREAK_DOWN_MASTER.Name) as BreakdownREASON 
                         from TSPL_BREAK_DOWN_ENTRY
                        left join TSPL_BREAK_DOWN_MASTER ON TSPL_BREAK_DOWN_ENTRY.Break_Down_Code = TSPL_BREAK_DOWN_MASTER.CODE
                        left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_BREAK_DOWN_ENTRY.Location_Code
                        WHERE convert(date,TSPL_BREAK_DOWN_ENTRY.Start_Time,103)=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103) group by TSPL_BREAK_DOWN_ENTRY.Location_Code) BreakDown
                        ON TSPL_LOCATION_MASTER.LOCATION_CODE =BreakDown.Location_Code "

                ElseIf rdbSaleReturn.IsChecked = True Then
                    query += "  LEFT OUTER JOIN
                        (SELECT SUM((isnull(TSPL_SD_SALE_RETURN_DETAIL.Qty,0)*FromUOM.Conversion_Factor)/ToUOM.Conversion_Factor) AS Qty,
                        sum(case when (convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(fDate, "dd/MMM/yyyy") + "',103)
                         and convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103))
                        then  ((isnull(TSPL_SD_SALE_RETURN_DETAIL.Qty,0)*FromUOM.Conversion_Factor)/ToUOM.Conversion_Factor) else 0  end) as SaleCumQty,
                         TSPL_SD_SALE_RETURN_HEAD.Bill_To_Location FROM 
                        TSPL_SD_SALE_RETURN_DETAIL left join 
                        TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.document_code=TSPL_SD_SALE_RETURN_DETAIL.document_code
                        LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.ITEM_CODE
                         left outer join TSPL_ITEM_UOM_DETAIL FromUOM on FromUOM.Item_Code =TSPL_SD_SALE_RETURN_DETAIL.Item_Code 
						AND FromUOM.UOM_Code=TSPL_SD_SALE_RETURN_DETAIL.Unit_code
						left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SD_SALE_RETURN_DETAIL.item_code and ToUOM.UOM_Code='MT'
                        WHERE  "
                    query += "" + FG + " " + SFG + " " + FGSFG + " " + StatusReturn + ""
                    query += " and convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103)=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103)
                        GROUP BY TSPL_SD_SALE_RETURN_HEAD.Bill_To_Location) SaleDailyQty
                        ON TSPL_LOCATION_MASTER.LOCATION_CODE =SaleDailyQty.Bill_To_Location 
                        LEFT OUTER JOIN
                        (SELECT SUM((isnull(TSPL_SD_SALE_RETURN_DETAIL.Qty,0)*FromUOM.Conversion_Factor)/ToUOM.Conversion_Factor) AS Qty,
                        TSPL_SD_SALE_RETURN_HEAD.Bill_To_Location FROM 
                        TSPL_SD_SALE_RETURN_DETAIL left join 
                        TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.document_code=TSPL_SD_SALE_RETURN_DETAIL.document_code
                        LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.ITEM_CODE
                        left outer join TSPL_ITEM_UOM_DETAIL FromUOM on FromUOM.Item_Code =TSPL_SD_SALE_RETURN_DETAIL.Item_Code 
						AND FromUOM.UOM_Code=TSPL_SD_SALE_RETURN_DETAIL.Unit_code
						left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SD_SALE_RETURN_DETAIL.item_code and ToUOM.UOM_Code='MT'
                        WHERE  "
                    query += " " + FG + " " + SFG + " " + FGSFG + " " + StatusReturn + " "
                    query += " and convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(fDate, "dd/MMM/yyyy") + "',103)
                         and convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103)
                        GROUP BY TSPL_SD_SALE_RETURN_HEAD.Bill_To_Location)  SaleCumQty
                        ON TSPL_LOCATION_MASTER.LOCATION_CODE =SaleCumQty.Bill_To_Location 
                         LEFT OUTER JOIN
                        (SELECT SUM((isnull(TSPL_SD_SALE_RETURN_DETAIL.Qty,0)*FromUOM.Conversion_Factor)/ToUOM.Conversion_Factor) AS Qty,TSPL_SD_SALE_RETURN_HEAD.Bill_To_Location FROM 
                        TSPL_SD_SALE_RETURN_DETAIL left join 
                        TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.document_code=TSPL_SD_SALE_RETURN_DETAIL.document_code
                        LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.ITEM_CODE
                        left outer join TSPL_ITEM_UOM_DETAIL FromUOM on FromUOM.Item_Code =TSPL_SD_SALE_RETURN_DETAIL.Item_Code 
						AND FromUOM.UOM_Code=TSPL_SD_SALE_RETURN_DETAIL.Unit_code
						left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SD_SALE_RETURN_DETAIL.item_code and ToUOM.UOM_Code='MT'
                        WHERE  "
                    query += "" + FG + " " + SFG + " " + FGSFG + " " + StatusReturn + ""
                    query += "and convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103)=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103)
                        GROUP BY TSPL_SD_SALE_RETURN_HEAD.Bill_To_Location) PSO
                        ON TSPL_LOCATION_MASTER.LOCATION_CODE =PSO.Bill_To_Location 
                         LEFT OUTER JOIN
                       (select TSPL_BREAK_DOWN_ENTRY.Location_Code
                        ,sum(DATEDIFF(HOUR,Start_Time,End_Time)) AS BreakdownHRS,max(TSPL_BREAK_DOWN_MASTER.Name) as BreakdownREASON 
                         from TSPL_BREAK_DOWN_ENTRY
                        left join TSPL_BREAK_DOWN_MASTER ON TSPL_BREAK_DOWN_ENTRY.Break_Down_Code = TSPL_BREAK_DOWN_MASTER.CODE
                        left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_BREAK_DOWN_ENTRY.Location_Code
                        WHERE convert(date,TSPL_BREAK_DOWN_ENTRY.Start_Time,103)=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103) group by TSPL_BREAK_DOWN_ENTRY.Location_Code) BreakDown
                        ON TSPL_LOCATION_MASTER.LOCATION_CODE =BreakDown.Location_Code "

                End If



                query += " LEFT OUTER JOIN (Select Location_Code,case when Qty < 0 then 0 else Qty end as Qty from (select Location_Code,sum(CLQty)Qty from (
                        select Item_Code,xxx.Location_Code,cast(sum(xxx.StockQTYY * RI) as decimal(18,2)) as CLQty
                        from (
                        select Avg_Cost,(case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=3 then TSPL_INVENTORY_MOVEMENT.FIFO_Cost else case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=2 then TSPL_INVENTORY_MOVEMENT.LIFO_Cost else TSPL_INVENTORY_MOVEMENT.Avg_Cost end end ) as Cost,Basic_Cost,TSPL_INVENTORY_MOVEMENT.Item_Desc,TSPL_INVENTORY_MOVEMENT.Item_Code,Trans_Type,Punching_Date,Location_Code,Stock_UOM,case when TSPL_INVENTORY_MOVEMENT.InOut='I' then 1 else -1 end as RI,
					  TSPL_INVENTORY_MOVEMENT.UOM, 					  
					  ( Stock_Qty*FromUOM.Conversion_Factor/ToUOM.Conversion_Factor) as StockQTYY 
					  from TSPL_INVENTORY_MOVEMENT
                        left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT.Item_Code
		              left outer join TSPL_ITEM_UOM_DETAIL FromUOM on FromUOM.Item_Code =TSPL_INVENTORY_MOVEMENT.Item_Code 
						AND FromUOM.UOM_Code=TSPL_INVENTORY_MOVEMENT.Stock_UOM
						left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_INVENTORY_MOVEMENT.item_code and ToUOM.UOM_Code='MT'
                    left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code
                    where  Punching_Date<= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(tDate), "dd/MMM/yyyy hh:mm:ss tt") + "' 
                    and "
                query += "" + FG + " " + SFG + " " + FGSFG + ""

                query += " )xxx 
					group by xxx.Item_Code,xxx.Location_Code)YYY group by Location_Code) FGS1)FGS ON TSPL_LOCATION_MASTER.LOCATION_CODE =FGS.Location_Code"

                query += " where TSPL_LOCATION_MASTER.IsMainPlant='0' and TSPL_LOCATION_MASTER.Rejected_Type='N'"
                'varsha added Stock qty less then 3 days case
                If rdbDaily.IsChecked Then
                    query += "union all
					

					SELECT 0 as sn,max(TSPL_LOCATION_MASTER.Loc_Short_Name)Location,'' as Date,'' as 	Date1,0 as 	Capacity,0 as 	NoOfShift,0 as 	ProdDailyQty,0 as 	ProdCumQty,0 as CUD,0 as 	CUM,0 as 	CUY,	0 as SaleDailyQty,0 as	SaleCumQty,	0 as FGS,0 as	PSO,0 as	BreakdownHRS,'' as	BreakdownREASON	,
										MAX(CASE WHEN rn = 1 THEN DcsSeqNo END) AS DcsSeqNo_1, MAX(CASE WHEN rn = 2 THEN DcsSeqNo END) AS DcsSeqNo_2, MAX(CASE WHEN rn = 3 THEN DcsSeqNo END) AS DcsSeqNo_3
FROM
(select xc.Location_Code,xc.DcsSeqNo, ROW_NUMBER() OVER (PARTITION BY xc.Location_Code  ORDER BY xc.DcsSeqNo ) AS rn from (Select  * from (
  SELECT max(RM_STOCK_DAYS.DcsSeqNo)DcsSeqNo,RM_STOCK_DAYS.Location_Code,RM_STOCK_DAYS.ITEM_CODE,max(RM_STOCK_DAYS.Item_Desc) as Item_Desc,
  max(RM_STOCK_DAYS.UOM)  AS 'UOM', SUM(ISNULL(RM_STOCK_DAYS.STOCK_QTY,0))  AS 'STOCK_QTY', SUM(ISNULL(RM_STOCK_DAYS.REQ_STOCK,0)) AS REQ_STOCK,SUM(ISNULL(RM_STOCK_DAYS.MIN_LEVEL,0)) AS MIN_LEVEL, CASE WHEN SUM(ISNULL(RM_STOCK_DAYS.REQ_STOCK,0))<>0 THEN SUM(ISNULL(RM_STOCK_DAYS.STOCK_QTY,0))/SUM(ISNULL(RM_STOCK_DAYS.REQ_STOCK,0)) ELSE  CASE WHEN SUM(ISNULL(RM_STOCK_DAYS.MIN_LEVEL,0))<>0 THEN SUM(ISNULL(RM_STOCK_DAYS.STOCK_QTY,0))/SUM(ISNULL(RM_STOCK_DAYS.MIN_LEVEL,0)) ELSE 0 END END AS 'QTY_FOR_DAYS' 
	            FROM  (
	            SELECT max(RM_STOCK.DcsSeqNo)DcsSeqNo,RM_STOCK.Location_Code,RM_STOCK.ITEM_CODE,max(RM_STOCK.Item_Desc) as Item_Desc,MAX(RM_STOCK.UOM) AS 'UOM',SUM(ISNULL(RM_STOCK.IN_STOCK_QTY,0))-SUM(ISNULL(RM_STOCK.OUT_STOCK_QTY,0)) AS 'STOCK_QTY',0 AS 'REQ_STOCK', 0 AS 'MIN_LEVEL' FROM (
	            SELECT TSPL_ITEM_MASTER.DcsSeqNo,TSPL_INVENTORY_MOVEMENT.Location_Code,TSPL_INVENTORY_MOVEMENT.Item_Code AS 'ITEM_CODE',TSPL_ITEM_MASTER.Short_Description AS 'ITEM_DESC',TSPL_ITEM_MASTER.Unit_Code AS 'UOM',
	            CASE WHEN INOUT='I' THEN STOCK_QTY END AS 'IN_STOCK_QTY',
	            CASE WHEN INOUT='O' THEN STOCK_QTY END AS 'OUT_STOCK_QTY'
	             FROM TSPL_INVENTORY_MOVEMENT
	            LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT.Item_Code
	            WHERE 2=2  --And TSPL_INVENTORY_MOVEMENT.Location_Code='AJMR' 
				AND TSPL_ITEM_MASTER.Structure_Code IN ('RM','PM')) RM_STOCK
	GROUP BY  RM_STOCK.Location_Code,RM_STOCK.Item_Code

	UNION ALL

	SELECT  
	max(TSPL_ITEM_MASTER.DcsSeqNo)DcsSeqNo,TSPL_MF_BOM_HEAD.LOCATION_CODE,TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE,max(TSPL_ITEM_MASTER.Short_Description) as Item_Desc,max(TSPL_ITEM_MASTER.Unit_Code) as 'UOM',
	0 AS 'STOCK_QTY',
	AVG(CASE WHEN TSPL_MF_BOM_DETAIL.Percentage>0 THEN 
    (TSPL_MF_BOM_DETAIL.Percentage*TSPL_LOCATION_MASTER.Silo_Capacity*1000)/100 ELSE 
    CASE WHEN TSPL_MF_BOM_DETAIL.CONSM_QUANTITY>0 THEN
    ((TSPL_MF_BOM_DETAIL.CONSM_QUANTITY*TSPL_LOCATION_MASTER.Silo_Capacity*1000)/TSPL_MF_BOM_HEAD.PROD_QUANTITY) ELSE 0 END
    END)  AS 'REQ_STOCK',
	0 AS 'MIN_LEVEL'
	FROM TSPL_MF_BOM_HEAD 
	LEFT OUTER JOIN TSPL_MF_BOM_DETAIL ON TSPL_MF_BOM_DETAIL.BOM_CODE=TSPL_MF_BOM_HEAD.BOM_CODE
	LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE
	left outer join TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code=TSPL_MF_BOM_HEAD.LOCATION_CODE
	INNER join (select PROD_ITEM_CODE,MAX(BOM_CODE) AS 'BOM_CODE',MAX(REVISION_NO) AS 'REVISION_NO' from TSPL_MF_BOM_HEAD WHERE 2=2  --and TSPL_MF_BOM_HEAD.LOCATION_CODE='AJMR' 
	GROUP BY PROD_ITEM_CODE
	) BOM_LATEST ON BOM_LATEST.BOM_CODE=TSPL_MF_BOM_HEAD.BOM_CODE AND BOM_LATEST.REVISION_NO=TSPL_MF_BOM_HEAD.REVISION_NO
	WHERE  TSPL_ITEM_MASTER.Structure_Code IN ('RM','PM')  -- and 	TSPL_MF_BOM_HEAD.LOCATION_CODE='AJMR' 
	GROUP BY  
	TSPL_MF_BOM_HEAD.LOCATION_CODE,TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE

	UNION ALL
	select TSPL_ITEM_MASTER.DcsSeqNo,TSPL_ITEM_REORDER_LEVEL_NEW.Location_Code,TSPL_ITEM_REORDER_LEVEL_NEW.Item_Code ,TSPL_ITEM_MASTER.Short_Description AS 'ITEM_DESC',TSPL_ITEM_MASTER.Unit_Code AS 'UOM',
	0 AS 'STOCK_QTY', 
	0 AS 'REQ_STOCK',
	TSPL_ITEM_REORDER_LEVEL_NEW.Min_Level AS 'MIN_LEVEL' 
	from TSPL_ITEM_REORDER_LEVEL_NEW 
	left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_REORDER_LEVEL_NEW.Item_Code
	where TSPL_ITEM_MASTER.Structure_Code IN ('RM','PM') and Apply='Y' --AND TSPL_ITEM_REORDER_LEVEL_NEW.Location_Code='AJMR' 
	) RM_STOCK_DAYS
GROUP BY  RM_STOCK_DAYS.Location_Code,RM_STOCK_DAYS.ITEM_CODE
) XX where xx.QTY_FOR_DAYS < 3 and xx.QTY_FOR_DAYS <> 0 and DcsSeqNo <> 0 )xc
) t
LEFT JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code = t.Location_Code
WHERE rn <= 3
GROUP BY t.Location_Code
)YY group by YY.Location "
                    query += " )Final  where Final.Location In (" & clsCommon.myCstr(Loc_Desc_Name) & ")"

                End If

            ElseIf rdbWeekly.IsChecked = True OrElse rbdDateRange.IsChecked Then
                'fDate = CDate(clsDBFuncationality.getSingleValue("select DATEADD(DAY,2-DATEPART(WEEKDAY,convert(date,'" + fromDate.Value + "',103)),convert(date,'" + fromDate.Value + "',103))"))
                'tDate = CDate(clsDBFuncationality.getSingleValue("select DATEADD(DAY,8-DATEPART(WEEKDAY,convert(date,'" + fromDate.Value + "',103)),convert(date,'" + fromDate.Value + "',103))"))
                'dtpFrom.Value = fDate
                'toDate.Value = tDate
                'dtcurrent = fromDate.Value
                'dtnext = toDate.Value
                fDate = fromDate.Value
                tDate = ToDate.Value
                DayCount = DateDiff(DateInterval.Day, fDate, tDate) + 1

                Dim LocationPivot As String = " Select Location_Code from TSPL_LOCATION_MASTER where Location_Code In (" & clsCommon.myCstr(Loc_Desc_Code) & ")
                                    UNION ALL
                                    Select Location_Code from TSPL_LOCATION_MASTER where IsMainPlant=1 "

                Dim dtlocation As DataTable = clsDBFuncationality.GetDataTable(LocationPivot)
                Dim locpivot As String = Nothing
                Dim strloc1pivot As String = Nothing
                Dim sumlocpivot As String = Nothing
                If dtlocation.Rows.Count > 0 Then
                    For i As Integer = 0 To dtlocation.Rows.Count - 1
                        If i = 0 Then
                            locpivot += "[" + clsCommon.myCstr(dtlocation.Rows(i)("Location_Code")) + "] "
                            strloc1pivot += " '" + clsCommon.myCstr(dtlocation.Rows(i)("Location_Code")) + "'"
                            sumlocpivot += " Sum(IsNull([" + clsCommon.myCstr(dtlocation.Rows(i)("Location_Code")) + "], 0))"
                        Else
                            locpivot += " ,[" + clsCommon.myCstr(dtlocation.Rows(i)("Location_Code")) + "] "
                            strloc1pivot += " ,'" + clsCommon.myCstr(dtlocation.Rows(i)("Location_Code")) + "'"
                            sumlocpivot += " ,Sum(IsNull([" + clsCommon.myCstr(dtlocation.Rows(i)("Location_Code")) + "], 0))"
                        End If
                    Next
                End If


                'Dim strLocation As String = clsDBFuncationality.getSingleValue("  DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' + QUOTENAME(TSPL_LOCATION_MASTER.location_code) as Alies_Name FROM TSPL_LOCATION_MASTER where TSPL_LOCATION_MASTER.IsMainPlant='0' FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
                'Dim strMainLocation As String = clsDBFuncationality.getSingleValue("SELECT TSPL_LOCATION_MASTER.Loc_Short_Name FROM TSPL_LOCATION_MASTER where TSPL_LOCATION_MASTER.IsMainPlant='1'")
                Dim StrTempQry2 As String = "DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT  
                     STUFF((SELECT distinct ',' +'Sum(isnull('  + QUOTENAME(TSPL_LOCATION_MASTER.location_code)+',0))'
                     +' as ' + QUOTENAME( TSPL_LOCATION_MASTER.location_code)
                    as Alies_Name FROM TSPL_LOCATION_MASTER where 2=2 and TSPL_LOCATION_MASTER.Rejected_Type='N' And Location_Code in (" & clsCommon.myCstr(Loc_Desc_Code) & ")  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')"
                Dim strSumLocation2 As String = clsDBFuncationality.getSingleValue(StrTempQry2)

                Dim StrTempQry As String = "DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT  
                     STUFF((SELECT distinct ',' +'Sum(isnull('  + QUOTENAME(TSPL_LOCATION_MASTER.location_code)+',0))'
                     +' as ' + QUOTENAME( TSPL_LOCATION_MASTER.location_code)
                    as Alies_Name FROM TSPL_LOCATION_MASTER where TSPL_LOCATION_MASTER.IsMainPlant='0' and TSPL_LOCATION_MASTER.Rejected_Type='N' And Location_Code in (" & clsCommon.myCstr(Loc_Desc_Code) & ")  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')"
                Dim strSumLocation As String = clsDBFuncationality.getSingleValue(StrTempQry)

                Dim StrTempQry1 As String = "DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT  
                     STUFF((SELECT distinct ',' +'(isnull('  + QUOTENAME(TSPL_LOCATION_MASTER.location_code)+',0))'
                     +' as ' + QUOTENAME( TSPL_LOCATION_MASTER.location_code)
                    as Alies_Name FROM TSPL_LOCATION_MASTER where TSPL_LOCATION_MASTER.IsMainPlant='0' and TSPL_LOCATION_MASTER.Rejected_Type='N' And Location_Code in (" & clsCommon.myCstr(Loc_Desc_Code) & ")  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')"
                Dim strSumLocation1 As String = clsDBFuncationality.getSingleValue(StrTempQry1)

                StrTempQry = "DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT  
                              STUFF((SELECT distinct '+' +'Sum(isnull(' + QUOTENAME(TSPL_LOCATION_MASTER.Location_Code) +',0))' as Alies_Name
                              FROM TSPL_LOCATION_MASTER where TSPL_LOCATION_MASTER.IsMainPlant='0' and TSPL_LOCATION_MASTER.Rejected_Type='N' And Location_Code in (" & clsCommon.myCstr(Loc_Desc_Code) & ")  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')"
                Dim strTotalLocation As String = clsDBFuncationality.getSingleValue(StrTempQry)

                Dim strLocation As String = clsDBFuncationality.getSingleValue("  DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' + QUOTENAME(TSPL_LOCATION_MASTER.location_code) as Alies_Name FROM TSPL_LOCATION_MASTER where TSPL_LOCATION_MASTER.IsMainPlant='0' and TSPL_LOCATION_MASTER.Rejected_Type='N' And Location_Code in (" & clsCommon.myCstr(Loc_Desc_Code) & ") FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
                Dim strMainLocation As String = clsDBFuncationality.getSingleValue("SELECT '[' + TSPL_LOCATION_MASTER.location_code + ']' FROM TSPL_LOCATION_MASTER where TSPL_LOCATION_MASTER.IsMainPlant='1'")
                Dim strLocation1 As String = clsDBFuncationality.getSingleValue("  DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' + QUOTENAME(TSPL_LOCATION_MASTER.location_code) as Alies_Name FROM TSPL_LOCATION_MASTER where 2=2 and TSPL_LOCATION_MASTER.Rejected_Type='N' And Location_Code in (" & strloc1pivot & ") FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")

                StrTempQry = "DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT  
                     STUFF((SELECT distinct ',' +'max('  + QUOTENAME(TSPL_LOCATION_MASTER.location_code)+')'
                     +' as ' + QUOTENAME( TSPL_LOCATION_MASTER.location_code)
                    as Alies_Name FROM TSPL_LOCATION_MASTER where TSPL_LOCATION_MASTER.IsMainPlant='0' and TSPL_LOCATION_MASTER.Rejected_Type='N' And Location_Code in (" & clsCommon.myCstr(Loc_Desc_Code) & ") FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')"
                Dim strMaxLocation As String = clsDBFuncationality.getSingleValue(StrTempQry)

                query = " SELECT   * FROM (
							SELECT 'Capacity / Day' AS Production, TSPL_LOCATION_MASTER.Location_Code,  (cast(cast((TSPL_LOCATION_MASTER.target) AS DECIMAL(18,0))/(day(eomonth('" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy") + "'))) AS DECIMAL(18,0))) Capacity
                        FROM  TSPL_LOCATION_MASTER where IsMainPlant=0
							union all
	
							SELECT 'Capacity / Day' AS Production, 'rcdf' as Location_Code, 
sum(cast(cast((TSPL_LOCATION_MASTER.target) AS DECIMAL(18,0))/(day(eomonth('" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy") + "'))) AS DECIMAL(18,0))) as [Capacity]
                        FROM  TSPL_LOCATION_MASTER where IsMainPlant=0  And Location_Code in (" & clsCommon.myCstr(Loc_Desc_Code) & ")
						--union all
						--SELECT 'Capacity / Day' AS Production, 'RCDF' AS Location_Code,  SUM(TSPL_LOCATION_MASTER.Silo_Capacity) Capacity
    --                FROM  TSPL_LOCATION_MASTER where IsMainPlant=0 

) AS XXXProduction
                            PIVOT (    MAX(Capacity)     FOR Location_Code IN (" & locpivot & ") ) AS zpivot "
                query += " UNION ALL
                        select Production," + strSumLocation + "," + strTotalLocation + " as " + strMainLocation + "
                         from (select 'Production' as Production,TSPL_LOCATION_MASTER.Location_Code
                        ,isnull(CAST((ProdCumQty.Qty/1000) AS DECIMAL(18,0)),0) as ProdCumQty
                         FROM TSPL_LOCATION_MASTER 
                         LEFT OUTER JOIN
                        ( Select Sum(Qty)Qty,LOCATION_CODE from
                        (select sum(TSPL_SPP_PRODUCTION_ENTRY_DETAIL.FINAL_PRODUCTION_QTY) as Qty,TSPL_SPP_PRODUCTION_ENTRY.LOCATION_CODE from TSPL_SPP_PRODUCTION_ENTRY_DETAIL
                         left join TSPL_SPP_PRODUCTION_ENTRY on TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE
                         LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE
                         where TSPL_Item_Master.FG_for_CF_RPT=1 and TSPL_SPP_PRODUCTION_ENTRY.posted=1 and convert(date,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103)>=convert(date,'" + clsCommon.GetPrintDate(fDate, "dd/MMM/yyyy") + "',103)
                         and convert(date,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103)<=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103)
                          GROUP BY TSPL_SPP_PRODUCTION_ENTRY.LOCATION_CODE
                          union all
						  Select sum(TSPL_ADJUSTMENT_DETAIL.Item_Quantity) as Qty,TSPL_ADJUSTMENT_HEADER.Loc_Code as LOCATION_CODE
						  from TSPL_ADJUSTMENT_DETAIL
						  left join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.Adjustment_No=TSPL_ADJUSTMENT_DETAIL.Adjustment_No
						                           LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_ADJUSTMENT_DETAIL.ITEM_CODE
												    where    TSPL_Item_Master.FG_for_CF_RPT=1     AND TSPL_ADJUSTMENT_HEADER.posted='Y'    and convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(fDate, "dd/MMM/yyyy") + "',103)
                         and convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103) 
                          GROUP BY TSPL_ADJUSTMENT_HEADER.Loc_Code 
						  
						  )XX Group by LOCATION_CODE ) ProdCumQty
                          ON TSPL_LOCATION_MASTER.LOCATION_CODE =ProdCumQty.LOCATION_CODE
                          where TSPL_LOCATION_MASTER.IsMainPlant='0')XXXProduction
                          pivot ( sum(ProdCumQty) for Location_Code in (" + strLocation + ") )as zpivot group by zpivot.Production"
                query += " union all
                         select Production," + strSumLocation + "," + strTotalLocation + " as " + strMainLocation + "
                         from (select 'Avg Production' as Production,TSPL_LOCATION_MASTER.Location_Code
                        ,isnull(CAST(((ProdCumQty.Qty/1000)" + "/" + clsCommon.myCstr(DayCount) + ") AS DECIMAL(18,0)),0) as ProdCumQty
                         FROM TSPL_LOCATION_MASTER 
                         LEFT OUTER JOIN
                        ( Select Sum(Qty)Qty,LOCATION_CODE from
                        (select sum(TSPL_SPP_PRODUCTION_ENTRY_DETAIL.FINAL_PRODUCTION_QTY) as Qty,TSPL_SPP_PRODUCTION_ENTRY.LOCATION_CODE from TSPL_SPP_PRODUCTION_ENTRY_DETAIL
                         left join TSPL_SPP_PRODUCTION_ENTRY on TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE
                         LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE
                         where TSPL_Item_Master.FG_for_CF_RPT=1 and TSPL_SPP_PRODUCTION_ENTRY.posted=1 and convert(date,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103)>=convert(date,'" + clsCommon.GetPrintDate(fDate, "dd/MMM/yyyy") + "',103)
                         and convert(date,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103)<=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103)
                          GROUP BY TSPL_SPP_PRODUCTION_ENTRY.LOCATION_CODE
                           union all
						  Select sum(TSPL_ADJUSTMENT_DETAIL.Item_Quantity) as Qty,TSPL_ADJUSTMENT_HEADER.Loc_Code as LOCATION_CODE
						  from TSPL_ADJUSTMENT_DETAIL
						  left join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.Adjustment_No=TSPL_ADJUSTMENT_DETAIL.Adjustment_No
						                           LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_ADJUSTMENT_DETAIL.ITEM_CODE
												    where    TSPL_Item_Master.FG_for_CF_RPT=1     AND TSPL_ADJUSTMENT_HEADER.posted='Y'    and convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(fDate, "dd/MMM/yyyy") + "',103)
                         and convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103) 
                          GROUP BY TSPL_ADJUSTMENT_HEADER.Loc_Code 
						  
						  )XX Group by LOCATION_CODE   ) ProdCumQty
                          ON TSPL_LOCATION_MASTER.LOCATION_CODE =ProdCumQty.LOCATION_CODE
                          where TSPL_LOCATION_MASTER.IsMainPlant='0')XXXProduction
                          pivot ( sum(ProdCumQty) for Location_Code in (" + strLocation + ") )as zpivot group by zpivot.Production "
                query += " union all
                         select Production," + strSumLocation + "," + strTotalLocation + " as " + strMainLocation + "
                         from (select 'Production / Shift' as Production,TSPL_LOCATION_MASTER.Location_Code
                        ,isnull(CAST(((ProdCumQty.Qty/1000)" + "/(" + clsCommon.myCstr(DayCount) + "*TSPL_LOCATION_MASTER.No_Of_Shift)) AS DECIMAL(18,0)),0) as ProdCumQty
                         FROM TSPL_LOCATION_MASTER 
                         LEFT OUTER JOIN
                        (select sum(TSPL_SPP_PRODUCTION_ENTRY_DETAIL.FINAL_PRODUCTION_QTY) as Qty,TSPL_SPP_PRODUCTION_ENTRY.LOCATION_CODE from TSPL_SPP_PRODUCTION_ENTRY_DETAIL
                         left join TSPL_SPP_PRODUCTION_ENTRY on TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE
                         LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE
                         where TSPL_Item_Master.FG_for_CF_RPT=1 and TSPL_SPP_PRODUCTION_ENTRY.posted=1 and convert(date,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103)>=convert(date,'" + clsCommon.GetPrintDate(fDate, "dd/MMM/yyyy") + "',103)
                         and convert(date,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103)<=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103)
                          GROUP BY TSPL_SPP_PRODUCTION_ENTRY.LOCATION_CODE) ProdCumQty
                          ON TSPL_LOCATION_MASTER.LOCATION_CODE =ProdCumQty.LOCATION_CODE
                          where TSPL_LOCATION_MASTER.IsMainPlant='0')XXXProduction
                          pivot ( sum(ProdCumQty) for Location_Code in (" + strLocation + ") )as zpivot group by zpivot.Production "

                'Sale
                query += " union all
                         select Production," + strSumLocation + "," + strTotalLocation + " as " + strMainLocation + "
                         from (select 'Avg Sale' as Production,TSPL_LOCATION_MASTER.Location_Code
                        ,Cast(isnull(CAST(SaleCumQty.Qty AS DECIMAL(18,0)),0)/" + clsCommon.myCstr(DayCount) + " as decimal(18,2)) as Qty
                         FROM TSPL_LOCATION_MASTER 
                         LEFT OUTER JOIN
						   (SELECT SUM((isnull(TSPL_SD_SHIPMENT_DETAIL.Qty,0)*FromUOM.Conversion_Factor)/ToUOM.Conversion_Factor) AS Qty,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location FROM 
                        TSPL_SD_SHIPMENT_DETAIL left join 
                        TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code=TSPL_SD_SHIPMENT_DETAIL.document_code
                        LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SD_SHIPMENT_DETAIL.ITEM_CODE
                        left outer join TSPL_ITEM_UOM_DETAIL FromUOM on FromUOM.Item_Code =TSPL_SD_SHIPMENT_DETAIL.Item_Code 
						AND FromUOM.UOM_Code=TSPL_SD_SHIPMENT_DETAIL.Unit_code
						left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SD_SHIPMENT_DETAIL.item_code and ToUOM.UOM_Code='MT'
                        WHERE TSPL_Item_Master.FG_for_CF_RPT=1 AND TSPL_SD_SHIPMENT_HEAD.Status=1 
                         and convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(fDate, "dd/MMM/yyyy") + "',103)
                         and convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103)
                        GROUP BY TSPL_SD_SHIPMENT_HEAD.Bill_To_Location) SaleCumQty
                        ON TSPL_LOCATION_MASTER.LOCATION_CODE =SaleCumQty.Bill_To_Location
						where TSPL_LOCATION_MASTER.IsMainPlant='0')XXXProduction
                          pivot ( sum(Qty) for Location_Code in (" + strLocation + ") )as zpivot group by zpivot.Production "

                'Breakdown Hours
                query += " union all
                         select Production," + strSumLocation + "," + strTotalLocation + " as " + strMainLocation + "
                         from (select 'Total Breakdown (HRS)' as Production,TSPL_LOCATION_MASTER.Location_Code
                        ,isnull(CAST(BreakDown.Qty AS DECIMAL(18,0)),0) as BreakDownQty
                         FROM TSPL_LOCATION_MASTER 
                         LEFT OUTER JOIN
                        (select sum(DATEDIFF(HOUR,Start_Time,End_Time)) as Qty,TSPL_BREAK_DOWN_ENTRY.Location_Code  from TSPL_BREAK_DOWN_ENTRY
                         where convert(date,TSPL_BREAK_DOWN_ENTRY.Doc_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(fDate, "dd/MMM/yyyy") + "',103)
                         and convert(date,TSPL_BREAK_DOWN_ENTRY.Doc_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103)
                          GROUP BY TSPL_BREAK_DOWN_ENTRY.LOCATION_CODE) BreakDown
                          ON TSPL_LOCATION_MASTER.LOCATION_CODE =BreakDown.LOCATION_CODE
                          where TSPL_LOCATION_MASTER.IsMainPlant='0')XXXBreakDown
                          pivot ( sum(BreakDownQty) for Location_Code in (" + strLocation + ") )as zpivot group by zpivot.Production "
                'CAPACITY UTILIZATION

                query += " Union all
                            select Production," + sumlocpivot + "
							
                         from (select 'Capacity Utilization' as Production,TSPL_LOCATION_MASTER.Location_Code
                        ,case when TSPL_LOCATION_MASTER.Silo_Capacity=0 then 0 else isnull(CAST(((ProdCumQty.Qty/1000)*100/(1*TSPL_LOCATION_MASTER.target/(day(eomonth('" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy") + "'))))/" + clsCommon.myCstr(DayCount) + ") AS DECIMAL(18,0)),0) end as ProdCumQty
						
                         FROM TSPL_LOCATION_MASTER 
                         LEFT OUTER JOIN
                        (select sum(TSPL_SPP_PRODUCTION_ENTRY_DETAIL.FINAL_PRODUCTION_QTY) as Qty,TSPL_SPP_PRODUCTION_ENTRY.LOCATION_CODE from TSPL_SPP_PRODUCTION_ENTRY_DETAIL
                         left join TSPL_SPP_PRODUCTION_ENTRY on TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE
                         LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE
                         where TSPL_Item_Master.FG_for_CF_RPT=1 and TSPL_SPP_PRODUCTION_ENTRY.posted=1 and convert(date,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103)>=convert(date,'" + clsCommon.GetPrintDate(fDate, "dd/MMM/yyyy") + "',103)
                         and convert(date,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103)<=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103)
                          GROUP BY TSPL_SPP_PRODUCTION_ENTRY.LOCATION_CODE) ProdCumQty
                          ON TSPL_LOCATION_MASTER.LOCATION_CODE =ProdCumQty.LOCATION_CODE
                          where TSPL_LOCATION_MASTER.IsMainPlant='0' 
						  
						  Union all
						  select 'Capacity Utilization' as Production,'RCDF' as Location_Code ,CAST((SUM(XX.Qty) * 100.0) / NULLIF(SUM(XX.Target), 0)AS DECIMAL(18,0))/" + clsCommon.myCstr(DayCount) + " AS ProdCumQty
 
						  from 
						 ( select 'Capacity Utilization' as Production,'RCDF' as Location_Code
                        ,0 as ProdCumQty,Sum(ProdCumQty.Qty/1000)Qty,Sum(TSPL_LOCATION_MASTER.Target/(day(eomonth('" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy") + "'))))Target
						
                         FROM TSPL_LOCATION_MASTER 
                         LEFT OUTER JOIN
                        (select sum(TSPL_SPP_PRODUCTION_ENTRY_DETAIL.FINAL_PRODUCTION_QTY) as Qty,'RCDF' as LOCATION_CODE from TSPL_SPP_PRODUCTION_ENTRY_DETAIL
                         left join TSPL_SPP_PRODUCTION_ENTRY on TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE
                         LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE
                         where TSPL_Item_Master.FG_for_CF_RPT=1 and TSPL_SPP_PRODUCTION_ENTRY.posted=1 and convert(date,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103)>=convert(date,'" + clsCommon.GetPrintDate(fDate, "dd/MMM/yyyy") + "',103)
                         and convert(date,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103)<=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103) And TSPL_SPP_PRODUCTION_ENTRY.Location_Code in (" & clsCommon.myCstr(Loc_Desc_Code) & ")
                          --GROUP BY TSPL_SPP_PRODUCTION_ENTRY.LOCATION_CODE
						  ) ProdCumQty
                          ON TSPL_LOCATION_MASTER.LOCATION_CODE =ProdCumQty.LOCATION_CODE
                          where TSPL_LOCATION_MASTER.IsMainPlant='1'

						  union all

						   select 'Capacity Utilization' as Production,'RCDF' as Location_Code
                        ,0 as ProdCumQty,0 as Qty,Sum(TSPL_LOCATION_MASTER.Target/(day(eomonth('" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy") + "'))))Target
						 FROM TSPL_LOCATION_MASTER 
                         LEFT OUTER JOIN
                        (select sum(TSPL_SPP_PRODUCTION_ENTRY_DETAIL.FINAL_PRODUCTION_QTY) as Qty,'RCDF' as LOCATION_CODE from TSPL_SPP_PRODUCTION_ENTRY_DETAIL
                         left join TSPL_SPP_PRODUCTION_ENTRY on TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE
                         LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE
                         where TSPL_Item_Master.FG_for_CF_RPT=1 and TSPL_SPP_PRODUCTION_ENTRY.posted=1 and convert(date,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103)>=convert(date,'" + clsCommon.GetPrintDate(fDate, "dd/MMM/yyyy") + "',103)
                         and convert(date,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103)<=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103)
                          ) ProdCumQty
                          ON TSPL_LOCATION_MASTER.LOCATION_CODE =ProdCumQty.LOCATION_CODE
                          where TSPL_LOCATION_MASTER.IsMainPlant='0'  And TSPL_LOCATION_MASTER.Location_Code in (" & clsCommon.myCstr(Loc_Desc_Code) & ")
						  )XX Group by LOCATION_CODE)XXXProduction
                          pivot ( sum(ProdCumQty) for Location_Code in (" + strLocation1 + ") )as zpivot group by zpivot.Production



"
                'query += " union all
                '          Select Production," + strSumLocation1 + ",RCDF/7 AS RCDF from (
                '            select Production," + strSumLocation + "," + strTotalLocation + " as " + strMainLocation + "
                '         from (select 'Capacity Utilization' as Production,TSPL_LOCATION_MASTER.Location_Code
                '        ,case when TSPL_LOCATION_MASTER.Silo_Capacity=0 then 0 else isnull(CAST(((ProdCumQty.Qty/1000)" + "/(" + clsCommon.myCstr(DayCount) + "*TSPL_LOCATION_MASTER.Silo_Capacity))*100 AS DECIMAL(18,0)),0) end as ProdCumQty
                '         FROM TSPL_LOCATION_MASTER 
                '         LEFT OUTER JOIN
                '        (select sum(TSPL_SPP_PRODUCTION_ENTRY_DETAIL.FINAL_PRODUCTION_QTY) as Qty,TSPL_SPP_PRODUCTION_ENTRY.LOCATION_CODE from TSPL_SPP_PRODUCTION_ENTRY_DETAIL
                '         left join TSPL_SPP_PRODUCTION_ENTRY on TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE
                '         LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE
                '         where TSPL_Item_Master.FG_for_CF_RPT=1 and TSPL_SPP_PRODUCTION_ENTRY.posted=1 and convert(date,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103)>=convert(date,'" + clsCommon.GetPrintDate(fDate, "dd/MMM/yyyy") + "',103)
                '         and convert(date,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103)<=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103)
                '          GROUP BY TSPL_SPP_PRODUCTION_ENTRY.LOCATION_CODE) ProdCumQty
                '          ON TSPL_LOCATION_MASTER.LOCATION_CODE =ProdCumQty.LOCATION_CODE
                '          where TSPL_LOCATION_MASTER.IsMainPlant='0')XXXProduction
                '          pivot ( sum(ProdCumQty) for Location_Code in (" + strLocation + ") )as zpivot group by zpivot.Production )XX "


                query = "select '" + objCommonVar.CurrentUserCode + "' as UserName,format(convert(date,'" + fromDate.Value + "',103), 'dd/MMM/yyyy') as Date,(format(convert(date,'" + ToDate.Value + "',103), 'dd/MMM/yyyy'))as Date1,* from (" + query + ")final "

                'Dim queryBreakDownCode As String = "   select Production," + strMaxLocation + ",max('') as " + strMainLocation + "
                '         from (select 'Breakdown Reason Code' as Production,TSPL_LOCATION_MASTER.Location_Code
                '        ,BreakDownCode.Qty as BreakDownQty
                '         FROM TSPL_LOCATION_MASTER 
                '         LEFT OUTER JOIN
                '        (select STUFF((SELECT distinct '+' +'' + QUOTENAME(max(TSPL_BREAK_DOWN_ENTRY.Break_Down_Code)) as Alies_Name FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') as Qty,TSPL_BREAK_DOWN_ENTRY.Location_Code  from TSPL_BREAK_DOWN_ENTRY
                '         where convert(date,TSPL_BREAK_DOWN_ENTRY.Doc_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(fDate, "dd/MMM/yyyy") + "',103)
                '         and convert(date,TSPL_BREAK_DOWN_ENTRY.Doc_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103)
                '          GROUP BY TSPL_BREAK_DOWN_ENTRY.LOCATION_CODE) BreakDownCode
                '          ON TSPL_LOCATION_MASTER.LOCATION_CODE =BreakDownCode.LOCATION_CODE
                '          where TSPL_LOCATION_MASTER.IsMainPlant='0')XXXBreakDownCode
                '          pivot ( max(BreakDownQty) for Location_Code in (" + strLocation + ") )as zpivot group by zpivot.Production "
                'dtBreakDownCode = clsDBFuncationality.GetDataTable(queryBreakDownCode)
            End If


            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(query)
            'If rdbWeekly.Checked = True Then
            '    If (dtBreakDownCode IsNot Nothing AndAlso dtBreakDownCode.Rows.Count > 0) Then
            '        dt2.Merge(dtBreakDownCode, True, MissingSchemaAction.Ignore)
            '    End If
            'End If


            Dim dt3 As New DataTable
            Dim qry As String = Nothing
            qry = "WITH CTE AS
(

select DcsSeqNo,max(Item_Desc) as Item_Desc ,ROW_NUMBER() OVER (ORDER BY DcsSeqNo) AS RN from (
SELECT  DcsSeqNo,CASE WHEN DcsSeqNo > 15 THEN 'Any Other' ELSE Item_Desc END AS Item_Desc FROM TSPL_ITEM_MASTER
    WHERE Item_Type = 'R' AND ISNULL(DcsSeqNo,0) > 0
) xx group by DcsSeqNo
)
SELECT  
    A.DcsSeqNo AS Item1_No,
    A.Item_Desc AS Item1_Name,
    B.DcsSeqNo AS Item2_No,
    B.Item_Desc AS Item2_Name
FROM CTE A
LEFT JOIN CTE B 
    ON B.RN = A.RN + (SELECT COUNT(*)/2 FROM CTE)
WHERE A.RN <= (SELECT COUNT(*)/2 FROM CTE)
ORDER BY A.RN;
"
            '            qry = "WITH Numbered AS (SELECT
            '        Item_Desc,DcsSeqNo,  CAST(CAST(DcsSeqNo AS INT) AS VARCHAR(10)) + ' - ' + Item_Desc AS Item_Desc_DcsSeqNo,
            '        ROW_NUMBER() OVER (ORDER BY DcsSeqNo) AS rn FROM TSPL_ITEM_MASTER
            '    WHERE Item_Type = 'R' AND DcsSeqNo IS NOT NULL and DcsSeqNo <> 0  AND DcsSeqNo <> 16 
            'union all
            '	SELECT
            '         Top 1 Item_Desc, DcsSeqNo,  CAST(CAST(DcsSeqNo AS INT) AS VARCHAR(10)) + ' - Any Other'  AS Item_Desc_DcsSeqNo,
            '        16 AS rn FROM TSPL_ITEM_MASTER
            '    WHERE Item_Type = 'R' AND DcsSeqNo = 16
            ')
            '    SELECT MAX(CASE WHEN rn_mod = 1 THEN Item_Desc_DcsSeqNo END) AS Item_1, MAX(CASE WHEN rn_mod = 2 THEN Item_Desc_DcsSeqNo END) AS Item_2, MAX(CASE WHEN rn_mod = 3 THEN Item_Desc_DcsSeqNo END) AS Item_3
            ',MAX(CASE WHEN rn_mod = 4 THEN Item_Desc_DcsSeqNo END) AS Item_4
            '  --  MAX(CASE WHEN rn_mod = 5 THEN Item_Desc_DcsSeqNo END) AS Item_5, 
            '--MAX(CASE WHEN rn_mod = 6 THEN Item_Desc_DcsSeqNo END) AS Item_6
            'FROM ( SELECT Item_Desc,Item_Desc_DcsSeqNo, rn,
            '        (rn - 1) / 3 AS grp,           -- integer division → row group
            '        (rn - 1) % 3 + 1 AS rn_mod     -- column position 1–6
            '    FROM Numbered
            ') d
            'GROUP BY grp
            'ORDER BY grp;
            '"
            dt3 = clsDBFuncationality.GetDataTable(qry)

            If (dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0) Then
                If Print = True And rdbDaily.IsChecked = True Then
                    Gv1.Visible = True
                    Gv1.DataSource = dt2
                    Gv1.ReadOnly = True
                    SetGridFormat(Gv1)
                    ReStoreGridLayout()
                    If rdbDaily.IsChecked = True Then
                        View()
                    End If
                    RadPageView1.SelectedPage = RadPageViewPage2
                    EnableDisableCntrl(False)
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funsubreportWithdt(MyBase.Form_ID, CrystalReportFolder.PRODUCTION, dt2, dt3, "Daily_Production_sale_FG_stock_BD_reportNEW", "Daily Production Sale Report", "rptStockItemDetail.rpt")

                    ' frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.PRODUCTION, dt2, "Daily_Production_sale_FG_stock_BD_report", "Daily Production Sale Report")
                    frmCRV = Nothing
                ElseIf Print = True And rbdDateRange.IsChecked = True Then
                    Gv1.Visible = True
                    Gv1.DataSource = dt2
                    Gv1.ReadOnly = True
                    SetGridFormat(Gv1)
                    ReStoreGridLayout()
                    'If rdbWeekly.IsChecked = True Then
                    '    View()
                    'End If
                    RadPageView1.SelectedPage = RadPageViewPage2
                    EnableDisableCntrl(False)
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.PRODUCTION, dt2, "Weekly Production and Sale Report", "Weekly Production Sale Report")
                    frmCRV = Nothing
                Else
                    Gv1.Visible = True
                    Gv1.DataSource = dt2
                    Gv1.ReadOnly = True
                    SetGridFormat(Gv1)
                    ReStoreGridLayout()
                    If rdbDaily.IsChecked = True Then
                        View()
                    End If
                    RadPageView1.SelectedPage = RadPageViewPage2
                    EnableDisableCntrl(False)
                End If
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found")
                Gv1.DataSource = Nothing
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Sub EnableDisableCntrl(ByVal val As Boolean)
        RadGroupBox3.Enabled = val
        RadGroupBox4.Enabled = val
        RadGroupBox5.Enabled = val
        RadGroupBox6.Enabled = val
        RadGroupBox1.Enabled = val
        RadGroupBox2.Enabled = val
        RadGroupBox7.Enabled = val
    End Sub

    Sub SetGridFormat(ByRef Gv1 As RadGridView)
        Gv1.ShowGroupPanel = False
        Gv1.ShowRowHeaderColumn = False
        Gv1.AllowAddNewRow = False
        Gv1.AllowDeleteRow = False
        Gv1.EnableFiltering = True
        Gv1.ShowFilteringRow = True

        Gv1.MasterTemplate.SummaryRowsBottom.Clear()

        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).BestFit()
        Next


        If rdbDaily.IsChecked = True Then
            'Gv1.Columns("NoOfShift").HeaderText = "NoOfShift" + Environment.NewLine + "Operated"
            Gv1.Columns("Noofshift").HeaderText = "NoOfShift"
            Gv1.Columns("ProdDailyQty").HeaderText = "Daily"
            Gv1.Columns("ProdCumQty").HeaderText = "Cummulative" + Environment.NewLine + "(MTD)"
            Gv1.Columns("CUD").HeaderText = "DLY %"
            Gv1.Columns("CUM").HeaderText = "MTD %"
            Gv1.Columns("CUY").HeaderText = "YTD %"
            Gv1.Columns("saleDailyQty").HeaderText = "Daily"
            Gv1.Columns("SaleCumQty").HeaderText = "Cummulative" + Environment.NewLine + "(MTD)"
            Gv1.Columns("PSO").HeaderText = "Pending" + Environment.NewLine + "Supply Orders"
            Gv1.Columns("BreakdownHRS").HeaderText = "HRS"
            Gv1.Columns("BreakdownREASON").HeaderText = "Reason"
            Gv1.Columns("DcsSeqNo_1").HeaderText = ""
            Gv1.Columns("DcsSeqNo_2").HeaderText = ""
            Gv1.Columns("DcsSeqNo_3").HeaderText = ""
            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim item1 As New GridViewSummaryItem("Capacity", "{0:F0}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            'Dim item2 As New GridViewSummaryItem("No of Shift", "{0:F0}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(item2)
            Dim item2 As New GridViewSummaryItem("Noofshift", "{0:F0}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            Dim item3 As New GridViewSummaryItem("ProdDailyQty", "{0:F0}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
            Dim item4 As New GridViewSummaryItem("ProdCumQty", "{0:F0}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)

            Dim item5 As New GridViewSummaryItem()
            item5.FormatString = "{0:F0}"
            item5.Name = "CUD"
            item5.AggregateExpression = "IIf(sum(Capacity)>0,(sum(ProdDailyQty)*100)/sum(Capacity),0)"
            summaryRowItem.Add(item5)

            Dim item6 As New GridViewSummaryItem()
            item6.FormatString = "{0:F0}"
            item6.Name = "CUM"
            item6.AggregateExpression = "IIf(sum(Capacity)>0,(sum(ProdCumQty)*100)/(sum(Capacity)*" + clsCommon.myCstr(DayCount) + "),0)"
            summaryRowItem.Add(item6)

            Dim item7 As New GridViewSummaryItem()
            item7.FormatString = "{0:F0}"
            item7.Name = "CUY"
            item7.AggregateExpression = "IIf(sum(Capacity)>0,(sum(ProdCumQty)*100)/(sum(Capacity)*" + clsCommon.myCstr(DayCount) + "),0)"
            summaryRowItem.Add(item7)



            Dim item8 As New GridViewSummaryItem("saleDailyQty", "{0:F0}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item8)
            Dim item9 As New GridViewSummaryItem("SaleCumQty", "{0:F0}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item9)
            Dim item10 As New GridViewSummaryItem("FGS", "{0:F0}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item10)
            Dim item11 As New GridViewSummaryItem("PSO", "{0:F0}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item11)

            Dim item21 As New GridViewSummaryItem("BreakdownHRS", "{0:F0}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item21)

            Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        Else
            Gv1.Columns("Date").HeaderText = "From Date"
            Gv1.Columns("Date").IsVisible = False
            Gv1.Columns("Date1").HeaderText = "To Date"
            Gv1.Columns("Date1").IsVisible = False
            Gv1.Columns("Production").HeaderText = "Production"
            'Gv1.Columns("AJMR").HeaderText = "AJMR"
            'Gv1.Columns("BIKR").HeaderText = "BIKR"
            'Gv1.Columns("JODH").HeaderText = "JODH"
            'Gv1.Columns("KALR").HeaderText = "KALR"
            'Gv1.Columns("LAMB").HeaderText = "LAMB"
            'Gv1.Columns("NADB").HeaderText = "NADB"
            'Gv1.Columns("PALI").HeaderText = "PALI"
            'Gv1.Columns("RCDF").HeaderText = "RCDF"


            'Dim dtLocation As DataTable = clsDBFuncationality.GetDataTable("SELECT TSPL_LOCATION_MASTER.location_code,TSPL_LOCATION_MASTER.Loc_Short_Name,cast(TSPL_LOCATION_MASTER.Silo_Capacity as int) as Silo_Capacity FROM TSPL_LOCATION_MASTER where TSPL_LOCATION_MASTER.IsMainPlant=0 and TSPL_LOCATION_MASTER.Rejected_Type='N'")
            Dim dtLocation As DataTable = clsDBFuncationality.GetDataTable("SELECT TSPL_LOCATION_MASTER.location_code,TSPL_LOCATION_MASTER.Loc_Short_Name,
cast(cast((TSPL_LOCATION_MASTER.target) AS DECIMAL(18,0))/(day(eomonth('" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(fromDate.Value), "dd/MMM/yyyy") + "'))) AS DECIMAL(18,0)) as Silo_Capacity
--cast(TSPL_LOCATION_MASTER.Silo_Capacity as int) as Silo_Capacity

FROM TSPL_LOCATION_MASTER where TSPL_LOCATION_MASTER.IsMainPlant=0 and TSPL_LOCATION_MASTER.Rejected_Type='N' and LOCATION_CODE In (" & clsCommon.myCstr(Loc_Desc_Code) & ")

          
          ")
            'Dim strMainLocation As DataTable = clsDBFuncationality.GetDataTable("SELECT TSPL_LOCATION_MASTER.location_code,TSPL_LOCATION_MASTER.Loc_Short_Name FROM TSPL_LOCATION_MASTER where TSPL_LOCATION_MASTER.IsMainPlant=1 and TSPL_LOCATION_MASTER.Rejected_Type='N'")
            Dim strMainLocation As DataTable = clsDBFuncationality.GetDataTable("SELECT TSPL_LOCATION_MASTER.location_code,'TOTAL' AS Loc_Short_Name FROM TSPL_LOCATION_MASTER where TSPL_LOCATION_MASTER.IsMainPlant=1 and TSPL_LOCATION_MASTER.Rejected_Type='N'")

            For i As Int16 = 0 To dtLocation.Rows.Count - 1
                Gv1.Columns(clsCommon.myCstr(dtLocation.Rows(i).Item("location_code"))).HeaderText = clsCommon.myCstr(dtLocation.Rows(i).Item("Loc_Short_Name")) + Environment.NewLine + clsCommon.myCstr(dtLocation.Rows(i).Item("Silo_Capacity"))
            Next
            Dim SumSilo_Capacity As Int64 = clsCommon.myCdbl(dtLocation.Compute("SUM(Silo_Capacity)", "Silo_Capacity is not null"))
            Gv1.Columns(clsCommon.myCstr(strMainLocation.Rows(0).Item("location_code"))).HeaderText = clsCommon.myCstr(strMainLocation.Rows(0).Item("Loc_Short_Name")) + Environment.NewLine + clsCommon.myCstr(SumSilo_Capacity)
            Gv1.Columns(0).HeaderText = "Unit" + Environment.NewLine + "Capacity/Day"
        End If


        Gv1.AutoSizeRows = False
        Gv1.BestFitColumns()
    End Sub


    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset()
    End Sub
    Sub reset()
        'fromDate.Value = clsCommon.GETSERVERDATE()
        RadPageView1.SelectedPage = RadPageViewPage1
        Gv1.ViewDefinition = New TableViewDefinition
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.Visible = False
        EnableDisableCntrl(True)
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub


    Private Sub btnReport_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReport.Click
        Try
            PageSetupReport_ID = Me.Form_ID + clsCommon.myCstr(IIf(rdbDaily.IsChecked = True, "Daily", "Weekly"))
            TemplateGridview = Gv1
            fillGridReport(False)
            GetReportID()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub GetReportID()
        Dim VarID As String = ""

        If rdbDaily.IsChecked Then
            VarID += "_D"
        ElseIf rdbWeekly.IsChecked Then
            VarID += "_W"
        End If

        Gv1.VarID = VarID

    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = Gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub RadMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem4.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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


    Private Sub print(ByVal exporter As EnumExportTo)


        Try
            Dim StrReportName As String = clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmProductionAndSaleReport & "'")
            Dim arrHeader As List(Of String) = New List(Of String)()
            'arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            'arrHeader.Add("Name : " & StrReportName)
            arrHeader.Add("Run Date : " + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Nothing, "dd/MM/yyyy hh:mm tt", False), "dd/MM/yyyy hh:mm tt")) ' clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy HH:MM"))
            arrHeader.Add("User : " + objCommonVar.CurrentUser)
            arrHeader.Add("Report Type : " + IIf(rdbDaily.IsChecked = True, "Daily", "Weekly"))
            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(Gv1, "", StrReportName, , arrHeader)
                'Else
                'transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                'clsCommon.MyExportToPDF(Label1.Text, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If

            If rdbDaily.IsChecked = True Then

                Dim doc As New RadPrintDocument()

                doc.Margins.Top = 50
                doc.Margins.Bottom = 50
                doc.Margins.Left = 50
                doc.Margins.Right = 50
                doc.HeaderHeight = 100
                doc.FooterHeight = 200
                doc.Landscape = True
                doc.LeftFooter = "Remark : For No demand,weekly maintenance and one section of plant/machine failure reasons,please mention Zero in hours column" +
                Environment.NewLine + "Code List of Raw Materials" +
                Environment.NewLine + "1 DORB                    2 RICE BRAIN" +
                Environment.NewLine + "3 DOMC                    4 GWAR KORMA" +
                Environment.NewLine + "5 MAIZE                    6 GWAR CHURI" +
                Environment.NewLine + "7 BARLEY SOUND           8 MOLASSES"

                'doc.LeftFooter = "Run Date : " + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Nothing, "dd/MM/yyyy hh:mm tt", False), "dd/MM/yyyy hh:mm tt")
                'doc.RightFooter = "User : " + objCommonVar.CurrentUser
                'doc.RightFooter = "ATUL MATHUR-RCDF" + Environment.NewLine + "MANAGER(SYSTEMS)"
                doc.RightFooter = clsDBFuncationality.getSingleValue("SELECT Range_Name from TSPL_LOCATION_MASTER where Location_Code ='RCDF'") + Environment.NewLine + clsDBFuncationality.getSingleValue("SELECT Range_Address from TSPL_LOCATION_MASTER where Location_Code ='RCDF'")
                doc.AssociatedObject = Gv1

                'Dim strHeader As String = Label1.Text 'Me.Text.Replace("/", "")

                'doc.MiddleHeader = StrReportName + Environment.NewLine + fromDate.Value.ToString("MMMM") + " " + clsCommon.myCstr(fromDate.Value.Year)
                doc.MiddleHeader = "RCDF CATTLE FEED PLANT : DAILY PRODUCTION & SALE" + Environment.NewLine + fromDate.Value.ToString("MMMM") + " " + clsCommon.myCstr(fromDate.Value.Year)
                doc.HeaderFont = New Font("Verdana", 10, FontStyle.Bold)
                doc.LeftHeader = "Unit : MT"
                'doc.LeftHeader = "Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy")
                'doc.RightHeader = "Report Type : " + IIf(rdbDaily.Checked = True, "Daily", "Weekly")
                doc.RightHeader = "Report Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy")

                Dim dialog As New RadPrintPreviewDialog
                dialog.Document = doc
                dialog.ToolMenu.Visible = True
                dialog.ShowDialog()
                doc = Nothing

            Else

                Dim doc As New RadPrintDocument()

                doc.Margins.Top = 50
                doc.Margins.Bottom = 50
                doc.Margins.Left = 50
                doc.Margins.Right = 50
                doc.HeaderHeight = 100
                doc.FooterHeight = 200
                doc.Landscape = True
                doc.AssociatedObject = Gv1
                doc.MiddleHeader = "RCDF : Weekly Production & Sale of Cattle Feed Plants:" + fromDate.Value.ToString("MMMM") + " " + clsCommon.myCstr(fromDate.Value.Year)
                doc.HeaderFont = New Font("Verdana", 10, FontStyle.Bold)
                doc.RightHeader = "MT"
                doc.LeftFooter = " Breakdown Reasons" + Environment.NewLine + "1. No Demand" + Environment.NewLine + "2. Short of Raw material except Molasses" +
                Environment.NewLine + "3. Short of Molasses" + Environment.NewLine + "4. Complete Plant/Machine failure" +
                Environment.NewLine + "5. Power supply/shortage" + Environment.NewLine + "6. Labour problem" +
                Environment.NewLine + "7. Transport problem" + Environment.NewLine + "8. Weekly maintenance" +
                Environment.NewLine + "9. One section of plant/machine failure" + Environment.NewLine + "10. Other"

                'doc.LeftFooter = clsDBFuncationality.get("select Name from TSPL_BREAK_DOWN_MASTER ")

                'doc.Print()
                Dim dialog As New RadPrintPreviewDialog
                dialog.Document = doc
                dialog.ToolMenu.Visible = True
                dialog.ShowDialog()
                doc = Nothing

            End If

            'Dim dialog As New RadPrintPreviewDialog
            'dialog.Document = doc
            'dialog.ToolMenu.Visible = True
            'dialog.ShowDialog()
            'doc = Nothing

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub


    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        print(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        print(EnumExportTo.PDF)
    End Sub

    Sub View()
        If Gv1.Rows.Count > 0 Then
            Dim view As New ColumnGroupsViewDefinition()
            view.ColumnGroups.Add(New GridViewColumnGroup(""))
            Dim groupRow0 = New GridViewColumnGroupRow()
            groupRow0.MinHeight = 30
            view.ColumnGroups(0).Rows.Add(groupRow0)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("SNo").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Location").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Capacity").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("NoofShift").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Production"))
            Dim groupRow1 = New GridViewColumnGroupRow()
            groupRow1.MinHeight = 30
            view.ColumnGroups(1).Rows.Add(groupRow1)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("ProdDailyQty").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("ProdCumQty").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Capacity Utilization"))
            Dim groupRow2 = New GridViewColumnGroupRow()
            groupRow2.MinHeight = 30
            view.ColumnGroups(2).Rows.Add(groupRow2)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("CUD").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("CUM").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("CUY").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Sale"))
            Dim groupRow3 = New GridViewColumnGroupRow()
            groupRow3.MinHeight = 30
            view.ColumnGroups(3).Rows.Add(groupRow3)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("SaleDailyQty").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("SaleCumQty").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup(""))
            Dim groupRow4 = New GridViewColumnGroupRow()
            groupRow4.MinHeight = 30
            view.ColumnGroups(4).Rows.Add(groupRow4)
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("FGS").Name)
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("PSO").Name)



            view.ColumnGroups.Add(New GridViewColumnGroup("Code of Raw Material(s) having stock of less then 3 days"))
            Dim groupRow5 = New GridViewColumnGroupRow()
            groupRow5.MinHeight = 15
            view.ColumnGroups(5).Rows.Add(groupRow5)
            view.ColumnGroups(5).Rows(0).ColumnNames.Add(Gv1.Columns("DcsSeqNo_1").Name)
            view.ColumnGroups(5).Rows(0).ColumnNames.Add(Gv1.Columns("DcsSeqNo_2").Name)
            view.ColumnGroups(5).Rows(0).ColumnNames.Add(Gv1.Columns("DcsSeqNo_3").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Breakdown"))
            Dim groupRow6 = New GridViewColumnGroupRow()
            groupRow6.MinHeight = 30
            view.ColumnGroups(6).Rows.Add(groupRow6)
            view.ColumnGroups(6).Rows(0).ColumnNames.Add(Gv1.Columns("BreakdownHRS").Name)
            view.ColumnGroups(6).Rows(0).ColumnNames.Add(Gv1.Columns("BreakdownREASON").Name)


            'view.ColumnGroups.Add(New GridViewColumnGroup("Code of Raw Material(s) having stock of less then 3 days"))
            'Dim groupRow6 = New GridViewColumnGroupRow()
            'groupRow6.MinHeight = 10
            'view.ColumnGroups(6).Rows.Add(groupRow6)
            'view.ColumnGroups(6).Rows(0).ColumnNames.Add(Gv1.Columns("DcsSeqNo_1").Name)
            'view.ColumnGroups(6).Rows(0).ColumnNames.Add(Gv1.Columns("DcsSeqNo_2").Name)
            'view.ColumnGroups(6).Rows(0).ColumnNames.Add(Gv1.Columns("DcsSeqNo_3").Name)
            Gv1.ViewDefinition = view
        End If
    End Sub

    Private Sub rdbDaily_CheckedChanged(sender As Object, e As EventArgs) Handles rdbDaily.CheckStateChanged
        If rdbDaily.IsChecked = True Then
            lbltoDate.Visible = False
            ToDate.Visible = False
        ElseIf rdbWeekly.IsChecked = True Then
            lbltoDate.Visible = True
            ToDate.Visible = True
        ElseIf rbdDateRange.IsChecked = True Then
            lbltoDate.Visible = True
            ToDate.Visible = True
        End If
    End Sub

    Private Sub RadMenuItem3_Click(sender As Object, e As EventArgs)
        Try
            '      'Dim dtBreakDownCode As DataTable
            '      Dim queryStock As String
            '      Dim query As String
            '      Dim Month As Integer = fromDate.Value.Month
            '      Dim Year As Integer = fromDate.Value.Year

            '      Dim fDate As DateTime = Nothing
            '      Dim tDate As DateTime = Nothing
            '      Dim dtcurrent As DateTime = Nothing
            '      Dim dtnext As DateTime = Nothing
            '      'DayCount = DateDiff(DateInterval.Day, fDate, tDate) + 1
            '      'Dim tDate As DateTime = New DateTime(Year, Month, DateTime.DaysInMonth(Year, Month))
            '      ',TSPL_LOCATION_MASTER.location_code as [Location Code]
            '      If rdbDaily.Checked = True Then
            '          fDate = New DateTime(Year, Month, 1)
            '          tDate = fromDate.Value
            '          DayCount = DateDiff(DateInterval.Day, fDate, tDate) + 1
            '          Dim dtLocation As DataTable = clsDBFuncationality.GetDataTable("SELECT LOCATION_CODE FROM TSPL_LOCATION_MASTER where TSPL_LOCATION_MASTER.IsMainPlant='0'")
            '          'Dim dtItem As DataTable = clsDBFuncationality.GetDataTable("select Item_Code from TSPL_ITEM_MASTER where Structure_Code='FG' and Item_Desc like '%SARAS%'")
            '          Dim dtItem As DataTable = clsDBFuncationality.GetDataTable("select Item_Code from TSPL_ITEM_MASTER where FG_for_CF=1")
            '          queryStock = ""
            '          For ll As Integer = 0 To dtLocation.Rows.Count - 1
            '              For ii As Integer = 0 To dtItem.Rows.Count - 1
            '                  If clsCommon.myLen(queryStock) > 0 Then
            '                      queryStock += " UNION ALL "
            '                  End If
            '                  queryStock += " select '" + clsCommon.myCstr(dtItem.Rows(ii).Item("Item_Code")) + "' AS Item_Code,'" + clsCommon.myCstr(dtLocation.Rows(ll).Item("LOCATION_CODE")) + "' AS LOCATION_CODE, "
            '                  queryStock += clsCommon.myCstr(clsItemLocationDetails.getBalance1(clsCommon.myCstr(dtItem.Rows(ii).Item("Item_Code")), clsCommon.myCstr(dtLocation.Rows(ll).Item("LOCATION_CODE")), "", tDate, Nothing, clsCommon.myCstr("MT"), 0))
            '                  queryStock += " as Qty"
            '              Next
            '          Next

            '          queryStock = "select ST.LOCATION_CODE,sum(ST.Qty) as Qty from (" + queryStock + ") ST GROUP BY ST.LOCATION_CODE "



            '          query = "select ROW_NUMBER() OVER(ORDER BY TSPL_LOCATION_MASTER.Location_code ASC) as SNo
            '                  ,TSPL_LOCATION_MASTER.Loc_Short_Name as [Location],format(convert(date,'" + fromDate.Value + "',103), 'dd MMMM yyyy') as Date,upper(format(convert(date,'" + fromDate.Value + "',103), 'MMMM yyyy'))as Date1
            '                  ,isnull(cast((TSPL_LOCATION_MASTER.Silo_Capacity/31*(day(eomonth('" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "')))) AS DECIMAL(18,0)),0) as [Capacity],
            '                  isnull(NoOfShift,0)NoOfShift
            '                  ,isnull(CAST((ProdDailyQty.Qty/1000) AS DECIMAL(18,0)),0) as ProdDailyQty
            '                  ,isnull(CAST((ProdCumQty.Qty/1000) AS DECIMAL(18,0)),0) as ProdCumQty
            '                  ,case when TSPL_LOCATION_MASTER.Silo_Capacity>0 then isnull(CAST(ProdDailyQty.Qty*100/(TSPL_LOCATION_MASTER.Silo_Capacity*1000) AS DECIMAL(18,0)),0) else 0 end as CUD
            '                  ,case when TSPL_LOCATION_MASTER.Silo_Capacity>0 then isnull(CAST((ProdCumQty.Qty*100)/(TSPL_LOCATION_MASTER.Silo_Capacity*1000*" + clsCommon.myCstr(DayCount) + ") AS DECIMAL(18,0)),0) else 0 end as CUM
            '                  ,case when TSPL_LOCATION_MASTER.Silo_Capacity>0 then isnull(CAST((ProdCumQty.Qty*100)/(TSPL_LOCATION_MASTER.Silo_Capacity*1000*" + clsCommon.myCstr(DayCount) + ") AS DECIMAL(18,0)),0) else 0 end as CUY
            '                  ,isnull(CAST(SaleDailyQty.Qty AS DECIMAL(18,0)),0) as SaleDailyQty
            '                  ,isnull(CAST(SaleCumQty.Qty AS DECIMAL(18,0)),0) as SaleCumQty
            '                  ,isnull(CAST(FGS.Qty AS DECIMAL(18,0)),0) as FGS
            '                  ,case when isnull(PSO.Qty,0)<0 then 0 else isnull(CAST(isnull(PSO.Qty,0) AS DECIMAL(18,0)),0) end as PSO
            '                  ,isnull(BreakDown.BreakdownHRS,0)BreakdownHRS
            '                  ,isnull(BreakDown.BreakdownREASON,0)BreakdownREASON
            '                  FROM TSPL_LOCATION_MASTER 

            '                  Left outer join 
            '(select count (TSPL_SPP_PRODUCTION_ENTRY.Shift_Code) as NoOfShift,TSPL_SPP_PRODUCTION_ENTRY.LOCATION_CODE  from TSPL_SPP_PRODUCTION_ENTRY 
            'WHERE CONVERT (DATE,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103) BETWEEN CONVERT(DATE,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103) AND CONVERT(DATE,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103)  
            '                     Group By LOCATION_CODE) NoOfShift
            '   ON TSPL_LOCATION_MASTER.LOCATION_CODE = NoOfShift.LOCATION_CODE

            '                   LEFT OUTER JOIN
            '                  (select sum(TSPL_SPP_PRODUCTION_ENTRY_DETAIL.FINAL_PRODUCTION_QTY) as Qty,TSPL_SPP_PRODUCTION_ENTRY.LOCATION_CODE from TSPL_SPP_PRODUCTION_ENTRY_DETAIL
            '                   left join TSPL_SPP_PRODUCTION_ENTRY on TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE
            '                   LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE
            '                   where TSPL_Item_Master.FG_for_CF=1 AND TSPL_SPP_PRODUCTION_ENTRY.posted=1 and convert(date,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103)=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103)
            '                    GROUP BY TSPL_SPP_PRODUCTION_ENTRY.LOCATION_CODE) ProdDailyQty
            '                    ON TSPL_LOCATION_MASTER.LOCATION_CODE =ProdDailyQty.LOCATION_CODE
            '                   LEFT OUTER JOIN
            '                  (select sum(TSPL_SPP_PRODUCTION_ENTRY_DETAIL.FINAL_PRODUCTION_QTY) as Qty,TSPL_SPP_PRODUCTION_ENTRY.LOCATION_CODE from TSPL_SPP_PRODUCTION_ENTRY_DETAIL
            '                   left join TSPL_SPP_PRODUCTION_ENTRY on TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE
            '                   LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE
            '                   where TSPL_Item_Master.FG_for_CF=1 and TSPL_SPP_PRODUCTION_ENTRY.posted=1 and convert(date,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103)>=convert(date,'" + clsCommon.GetPrintDate(fDate, "dd/MMM/yyyy") + "',103)
            '                   and convert(date,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103)<=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103)
            '                    GROUP BY TSPL_SPP_PRODUCTION_ENTRY.LOCATION_CODE) ProdCumQty
            '                    ON TSPL_LOCATION_MASTER.LOCATION_CODE =ProdCumQty.LOCATION_CODE
            '                  LEFT OUTER JOIN
            '                  (SELECT SUM((isnull(TSPL_SD_SHIPMENT_DETAIL.Qty,0)*FromUOM.Conversion_Factor)/ToUOM.Conversion_Factor) AS Qty,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location FROM 
            '                  TSPL_SD_SHIPMENT_DETAIL left join 
            '                  TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code=TSPL_SD_SHIPMENT_DETAIL.document_code
            '                  LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SD_SHIPMENT_DETAIL.ITEM_CODE
            '                   left outer join TSPL_ITEM_UOM_DETAIL FromUOM on FromUOM.Item_Code =TSPL_SD_SHIPMENT_DETAIL.Item_Code 
            'AND FromUOM.UOM_Code=TSPL_SD_SHIPMENT_DETAIL.Unit_code
            'left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SD_SHIPMENT_DETAIL.item_code and ToUOM.UOM_Code='MT'
            '                  WHERE TSPL_Item_Master.FG_for_CF=1 AND TSPL_SD_SHIPMENT_HEAD.Status=1 and convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103)=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103)
            '                  GROUP BY TSPL_SD_SHIPMENT_HEAD.Bill_To_Location) SaleDailyQty
            '                  ON TSPL_LOCATION_MASTER.LOCATION_CODE =SaleDailyQty.Bill_To_Location 
            '                  LEFT OUTER JOIN
            '                  (SELECT SUM((isnull(TSPL_SD_SHIPMENT_DETAIL.Qty,0)*FromUOM.Conversion_Factor)/ToUOM.Conversion_Factor) AS Qty,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location FROM 
            '                  TSPL_SD_SHIPMENT_DETAIL left join 
            '                  TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code=TSPL_SD_SHIPMENT_DETAIL.document_code
            '                  LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SD_SHIPMENT_DETAIL.ITEM_CODE
            '                  left outer join TSPL_ITEM_UOM_DETAIL FromUOM on FromUOM.Item_Code =TSPL_SD_SHIPMENT_DETAIL.Item_Code 
            'AND FromUOM.UOM_Code=TSPL_SD_SHIPMENT_DETAIL.Unit_code
            'left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SD_SHIPMENT_DETAIL.item_code and ToUOM.UOM_Code='MT'
            '                  WHERE TSPL_Item_Master.FG_for_CF=1 AND TSPL_SD_SHIPMENT_HEAD.Status=1 
            '                  and convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(fDate, "dd/MMM/yyyy") + "',103)
            '                   and convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103)
            '                  GROUP BY TSPL_SD_SHIPMENT_HEAD.Bill_To_Location) SaleCumQty
            '                  ON TSPL_LOCATION_MASTER.LOCATION_CODE =SaleCumQty.Bill_To_Location 
            '                   LEFT OUTER JOIN
            '                  (SELECT SUM(isnull(TSPL_SD_SHIPMENT_HEAD.Order_Qty,0))-SUM((isnull(TSPL_SD_SHIPMENT_DETAIL.Qty,0)*FromUOM.Conversion_Factor)/ToUOM.Conversion_Factor) AS Qty,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location FROM 
            '                  TSPL_SD_SHIPMENT_DETAIL left join 
            '                  TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code=TSPL_SD_SHIPMENT_DETAIL.document_code
            '                  LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SD_SHIPMENT_DETAIL.ITEM_CODE
            '                  left outer join TSPL_ITEM_UOM_DETAIL FromUOM on FromUOM.Item_Code =TSPL_SD_SHIPMENT_DETAIL.Item_Code 
            'AND FromUOM.UOM_Code=TSPL_SD_SHIPMENT_DETAIL.Unit_code
            'left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SD_SHIPMENT_DETAIL.item_code and ToUOM.UOM_Code='MT'
            '                  WHERE TSPL_Item_Master.FG_for_CF=1 AND TSPL_SD_SHIPMENT_HEAD.Status=1 and convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103)=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103)
            '                  GROUP BY TSPL_SD_SHIPMENT_HEAD.Bill_To_Location) PSO
            '                  ON TSPL_LOCATION_MASTER.LOCATION_CODE =PSO.Bill_To_Location 
            '                   LEFT OUTER JOIN

            '                  (select TSPL_BREAK_DOWN_ENTRY.Location_Code
            '                  ,max(DATEDIFF(HOUR,Start_Time,End_Time)) AS BreakdownHRS,max(TSPL_BREAK_DOWN_MASTER.Name) as BreakdownREASON 
            '                   from TSPL_BREAK_DOWN_ENTRY
            '                  left join TSPL_BREAK_DOWN_MASTER ON TSPL_BREAK_DOWN_ENTRY.Break_Down_Code = TSPL_BREAK_DOWN_MASTER.CODE
            '                  left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_BREAK_DOWN_ENTRY.Location_Code
            '                  WHERE convert(date,TSPL_BREAK_DOWN_ENTRY.Start_Time,103)=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103) group by TSPL_BREAK_DOWN_ENTRY.Location_Code) BreakDown
            '                  ON TSPL_LOCATION_MASTER.LOCATION_CODE =BreakDown.Location_Code "


            '          query += " LEFT OUTER JOIN (" + queryStock + " ) FGS ON TSPL_LOCATION_MASTER.LOCATION_CODE =FGS.Location_Code"

            '          query += " where TSPL_LOCATION_MASTER.IsMainPlant='0'"
            '          Dim dt As DataTable = clsDBFuncationality.GetDataTable(query)
            '          If dt IsNot Nothing And dt.Rows.Count > 0 Then
            '              Dim frmCRV As New frmCrystalReportViewer()
            '              frmCRV.funreport(CrystalReportFolder.PRODUCTION, dt, "Daily_Production_sale_FG_stock_BD_report", "Daily Production Sale Report")
            '              frmCRV = Nothing
            '          Else
            '              clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            '          End If
            '      End If
            fillGridReport(True)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub fromDate_ValueChanged(sender As Object, e As EventArgs) Handles fromDate.ValueChanged
        Dim slotCount As Integer = 0
        Dim selectedMonth As Integer = fromDate.Value.Month
        Dim selectedYear As Integer = fromDate.Value.Year
        Dim currentDate As New DateTime(selectedYear, selectedMonth, 1)
        Slot1FD = clsCommon.GetPrintDate(currentDate, "dd/MMM/yyyy")
        Slot1TD = clsCommon.GetPrintDate(currentDate.AddDays(8), "dd/MMM/yyyy")
        slotCount += 1
        Slot2FD = clsCommon.GetPrintDate(currentDate.AddDays(9), "dd/MMM/yyyy")
        Slot2TD = clsCommon.GetPrintDate(currentDate.AddDays(15), "dd/MMM/yyyy")
        slotCount += 1
        Slot3FD = clsCommon.GetPrintDate(currentDate.AddDays(16), "dd/MMM/yyyy")
        Slot3TD = clsCommon.GetPrintDate(currentDate.AddDays(23), "dd/MMM/yyyy")
        slotCount += 1
        Slot4FD = clsCommon.GetPrintDate(currentDate.AddDays(24), "dd/MMM/yyyy")
        'Slot4TD = clsCommon.GetPrintDate(currentDate.AddDays(24), "dd/MMM/yyyy")
        If rbdDateRange.IsChecked Then
            ToDate.Value = clsCommon.GETSERVERDATE()

        Else
            ToDate.Value = clsCommon.GetPrintDate(currentDate.AddMonths(1).AddDays(-1), "dd/MMM/yyyy")

        End If
        slotCount += 1
    End Sub

    Private Sub rdbSaleReturn_CheckStateChanged(sender As Object, e As EventArgs) Handles rdbSaleReturn.CheckStateChanged
        If rdbSaleReturn.IsChecked = True Then
            rdbDispatch.IsChecked = False
            rdbInvoice.IsChecked = False
        Else
            rdbDispatch.IsChecked = True
        End If
    End Sub

    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
        Try
            fillGridReport(True)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub gv1_ViewRowFormatting(sender As Object, e As RowFormattingEventArgs) Handles Gv1.ViewRowFormatting
        If TypeOf e.RowElement Is GridDataRowElement Then
            Dim rowIndex As Integer = e.RowElement.RowInfo.Index

            ' Apply alternating row colors (even/odd)
            If rowIndex Mod 2 = 0 Then
                e.RowElement.BackColor = Color.FromArgb(252, 228, 214)
            Else
                e.RowElement.BackColor = Color.White  ' Odd row color
            End If

            ' Ensure the color persists even on hover/selection
            e.RowElement.DrawFill = True
            e.RowElement.GradientStyle = Telerik.WinControls.GradientStyles.Solid
            'e.RowElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.Local)
            'e.RowElement.ResetValue(LightVisualElement.ForeColorProperty, ValueResetFlags.Local)

        End If

        ' Formatting for the Summary Row
        If TypeOf e.RowElement Is GridSummaryRowElement Then
            e.RowElement.BackColor = Color.DarkSalmon
            e.RowElement.Font = New Font("Arial", 8, FontStyle.Bold)
            e.RowElement.DrawFill = True
            e.RowElement.GradientStyle = Telerik.WinControls.GradientStyles.Solid
        End If
    End Sub

    Private Sub gv1_ViewCellFormatting(sender As Object, e As CellFormattingEventArgs) Handles Gv1.ViewCellFormatting
        If TypeOf e.CellElement Is GridDataCellElement AndAlso e.CellElement.RowInfo IsNot Nothing AndAlso e.CellElement.RowInfo.Index >= 0 Then
            Dim columnName As String = e.CellElement.ColumnInfo.Name
            Dim cellValue As Object = e.CellElement.Value

            ' Conditional Cell Formatting
            If columnName = "CUD" Or columnName = "CUM" Or columnName = "CUY" Then
                Dim numericValue As Decimal
                If cellValue IsNot Nothing AndAlso Decimal.TryParse(cellValue.ToString(), numericValue) Then
                    If numericValue < 100 Then
                        e.CellElement.BackColor = Color.FromArgb(249, 128, 128)
                    Else
                        e.CellElement.BackColor = Color.FromArgb(144, 238, 144)
                    End If
                End If
            End If

            ' Ensure the color persists
            e.CellElement.DrawFill = True
            e.CellElement.GradientStyle = Telerik.WinControls.GradientStyles.Solid
            'e.CellElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.Local)
            'e.CellElement.ResetValue(LightVisualElement.ForeColorProperty, ValueResetFlags.Local)

        End If

        ' Summary Cell Formatting
        If TypeOf e.CellElement Is GridSummaryCellElement Then
            Dim summaryCell As GridSummaryCellElement = DirectCast(e.CellElement, GridSummaryCellElement)
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
            summaryCell.ForeColor = Color.Black
            summaryCell.Font = New Font("Arial", 8, FontStyle.Bold)
            summaryCell.BackColor = Color.FromArgb(244, 176, 132)
            summaryCell.DrawFill = True
            summaryCell.GradientStyle = Telerik.WinControls.GradientStyles.Solid
        End If
    End Sub

End Class
