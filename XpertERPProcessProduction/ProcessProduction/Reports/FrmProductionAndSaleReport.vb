Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports System
Imports System.IO
Imports Telerik.WinControls.UI.Export

Public Class FrmProductionAndSaleReport
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim buttontooltip As ToolTip = New ToolTip()
    Dim DayCount As Int16 = 0
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
        SetUserMgmtNew()
        buttontooltip.SetToolTip(btnReport, "Press Alt+R for Summary ")
        buttontooltip.SetToolTip(btnreset, "Press Alt+E for Reset ")
        buttontooltip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        fromDate.Value = clsCommon.GETSERVERDATE()
        toDate.Value = clsCommon.GETSERVERDATE()
        lbltoDate.Visible = False
        toDate.Visible = False
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
            'DayCount = DateDiff(DateInterval.Day, fDate, tDate) + 1
            'Dim tDate As DateTime = New DateTime(Year, Month, DateTime.DaysInMonth(Year, Month))
            ',TSPL_LOCATION_MASTER.location_code as [Location Code]
            If rdbDaily.Checked = True Then
                fDate = New DateTime(Year, Month, 1)
                tDate = clsCommon.GetDateWithEndTime(fromDate.Value)
                DayCount = DateDiff(DateInterval.Day, fDate, tDate) + 1
                Dim dtLocation As DataTable = clsDBFuncationality.GetDataTable("SELECT LOCATION_CODE FROM TSPL_LOCATION_MASTER where TSPL_LOCATION_MASTER.IsMainPlant='0'")
                'Dim dtItem As DataTable = clsDBFuncationality.GetDataTable("select Item_Code from TSPL_ITEM_MASTER where Structure_Code='FG' and Item_Desc like '%SARAS%'")
                Dim dtItem As DataTable = clsDBFuncationality.GetDataTable("select Item_Code from TSPL_ITEM_MASTER where FG_for_CF_RPT=1")
                queryStock = ""
                For ll As Integer = 0 To dtLocation.Rows.Count - 1
                    For ii As Integer = 0 To dtItem.Rows.Count - 1
                        If clsCommon.myLen(queryStock) > 0 Then
                            queryStock += " UNION ALL "
                        End If
                        queryStock += " select '" + clsCommon.myCstr(dtItem.Rows(ii).Item("Item_Code")) + "' AS Item_Code,'" + clsCommon.myCstr(dtLocation.Rows(ll).Item("LOCATION_CODE")) + "' AS LOCATION_CODE, "
                        queryStock += clsCommon.myCstr(clsItemLocationDetails.getBalance1(clsCommon.myCstr(dtItem.Rows(ii).Item("Item_Code")), clsCommon.myCstr(dtLocation.Rows(ll).Item("LOCATION_CODE")), "", tDate, Nothing, clsCommon.myCstr("MT"), 0))
                        queryStock += " as Qty"
                    Next
                Next
                If clsCommon.myLen(queryStock) <= 0 Then
                    Throw New Exception("No data found to display")
                End If
                queryStock = "select ST.LOCATION_CODE,sum(ST.Qty) as Qty from (" + queryStock + ") ST GROUP BY ST.LOCATION_CODE "
                For ll As Integer = 0 To dtLocation.Rows.Count - 1
                    itemcodeqry = "select top 3 ITEM_CODE,Location_Code from (SELECT FINAL.Location_Code,FINAL.Location_Desc,FINAL.Add1,FINAL.Add2,FINAL.Add3,FINAL.Add4,FINAL.ITEM_CODE, FINAL.Item_Desc,FINAL.Unit,FINAL.STOCK_QTY,FINAL.QTY_FOR_DAYS FROM (select xx.Item_Desc,xx.ITEM_CODE,xx.Location_Code,xx.Location_Desc,xx.Add1,xx.Add2,xx.Add3,xx.Add4, case when xx.UOM='KG' THEN CASE WHEN ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)=0 THEN xx.UOM ELSE TSPL_ITEM_UOM_DETAIL.UOM_Code END WHEN xx.UOM='GM' THEN CASE WHEN ISNULL(KG_UOM_DETAIL.Conversion_Factor,0)=0 THEN xx.UOM ELSE KG_UOM_DETAIL.UOM_Code END ELSE xx.UOM END AS 'Unit',CAST((case when xx.UOM='KG' THEN CASE WHEN ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)<>0 THEN (xx.STOCK_QTY/ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1)) ELSE xx.STOCK_QTY END WHEN xx.UOM='GM' THEN CASE WHEN ISNULL(KG_UOM_DETAIL.Conversion_Factor,0)<>0 THEN (xx.STOCK_QTY/ISNULL(KG_UOM_DETAIL.Conversion_Factor,1)) ELSE xx.STOCK_QTY END ELSE xx.STOCK_QTY END) AS numeric(10,0)) AS 'STOCK_QTY',cast(xx.QTY_FOR_DAYS as integer) as QTY_FOR_DAYS from (SELECT max(RM_STOCK_DAYS.Location_Code)Location_Code,max(RM_STOCK_DAYS.Location_Desc)Location_Desc,max(RM_STOCK_DAYS.Add1)Add1,max(RM_STOCK_DAYS.Add2)Add2,max(RM_STOCK_DAYS.Add3)Add3,max(RM_STOCK_DAYS.Add4)Add4, RM_STOCK_DAYS.ITEM_CODE,max(RM_STOCK_DAYS.Item_Desc) as Item_Desc,max(RM_STOCK_DAYS.UOM) AS 'UOM',SUM(ISNULL(RM_STOCK_DAYS.STOCK_QTY,0)) AS 'STOCK_QTY', SUM(ISNULL(RM_STOCK_DAYS.REQ_STOCK,0)) AS REQ_STOCK,SUM(ISNULL(RM_STOCK_DAYS.MIN_LEVEL,0)) AS MIN_LEVEL, CASE WHEN SUM(ISNULL(RM_STOCK_DAYS.REQ_STOCK,0))<>0 THEN SUM(ISNULL(RM_STOCK_DAYS.STOCK_QTY,0))/SUM(ISNULL(RM_STOCK_DAYS.REQ_STOCK,0)) ELSE CASE WHEN SUM(ISNULL(RM_STOCK_DAYS.MIN_LEVEL,0))<>0 THEN SUM(ISNULL(RM_STOCK_DAYS.STOCK_QTY,0))/SUM(ISNULL(RM_STOCK_DAYS.MIN_LEVEL,0)) ELSE 0 END END AS 'QTY_FOR_DAYS' FROM (SELECT max(RM_STOCK.Location_Code)Location_Code,max(RM_STOCK.Location_Desc)Location_Desc,max(RM_STOCK.Add1)Add1,max(RM_STOCK.Add2)Add2,max(RM_STOCK.Add3)Add3,max(RM_STOCK.Add4)Add4, RM_STOCK.ITEM_CODE,max(RM_STOCK.Item_Desc) as Item_Desc,MAX(RM_STOCK.UOM) AS 'UOM',SUM(ISNULL(RM_STOCK.IN_STOCK_QTY,0))-SUM(ISNULL(RM_STOCK.OUT_STOCK_QTY,0)) AS 'STOCK_QTY',0 AS 'REQ_STOCK', 0 AS 'MIN_LEVEL' FROM ( SELECT TSPL_LOCATION_MASTER.Location_Code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_LOCATION_MASTER.Add1,TSPL_LOCATION_MASTER.Add2,TSPL_LOCATION_MASTER.Add3,TSPL_LOCATION_MASTER.Add4, TSPL_INVENTORY_MOVEMENT.Item_Code AS 'ITEM_CODE',TSPL_ITEM_MASTER.Short_Description AS 'ITEM_DESC',TSPL_ITEM_MASTER.Unit_Code AS 'UOM', CASE WHEN INOUT='I' THEN STOCK_QTY END AS 'IN_STOCK_QTY',CASE WHEN INOUT='O' THEN STOCK_QTY END AS 'OUT_STOCK_QTY' FROM TSPL_INVENTORY_MOVEMENT LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT.Item_Code left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_INVENTORY_MOVEMENT.Location_Code WHERE 2=2 And TSPL_INVENTORY_MOVEMENT.Location_Code IN ('" + clsCommon.myCstr(dtLocation.Rows(ll).Item("LOCATION_CODE")) + "') AND TSPL_ITEM_MASTER.Structure_Code IN ('RM')and tspl_item_master.Item_Code in (select distinct TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE from TSPL_SPP_PRODUCTION_ENTRY_DETAIL left outer join TSPL_SPP_PRODUCTION_ENTRY on TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE left outer join TSPL_MF_BOM_HEAD on TSPL_MF_BOM_HEAD.BOM_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.BOM_CODE left outer join TSPL_MF_BOM_DETAIL on TSPL_MF_BOM_DETAIL.BOM_CODE=TSPL_MF_BOM_HEAD.BOM_CODE left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE where CONVERT(DATE,PROD_DATE,103)>= convert(date,'" + clsCommon.GetPrintDate(fDate, "dd/MMM/yyyy") + "',103) and CONVERT(DATE,PROD_DATE,103)<= convert(date,'" + clsCommon.GetPrintDate(fDate, "dd/MMM/yyyy") + "',103) and TSPL_SPP_PRODUCTION_ENTRY.LOCATION_CODE='" + clsCommon.myCstr(dtLocation.Rows(ll).Item("LOCATION_CODE")) + "' and TSPL_ITEM_MASTER.FG_for_CF_PL=1 ) ) RM_STOCK GROUP BY RM_STOCK.Item_Code UNION ALL SELECT max(TSPL_LOCATION_MASTER.Location_Code)LocationCode,max(TSPL_LOCATION_MASTER.Location_Desc)LocDes,max(TSPL_LOCATION_MASTER.Add1)Add1,max(TSPL_LOCATION_MASTER.Add2)Add2,max(TSPL_LOCATION_MASTER.Add3)Add3,max(TSPL_LOCATION_MASTER.Add4)Add4, TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE,max(TSPL_ITEM_MASTER.Short_Description) as Item_Desc,max(TSPL_ITEM_MASTER.Unit_Code) as 'UOM', 0 AS 'STOCK_QTY', AVG(CASE WHEN TSPL_MF_BOM_DETAIL.Percentage>0 THEN (TSPL_MF_BOM_DETAIL.Percentage*TSPL_LOCATION_MASTER.Silo_Capacity*1000)/100 ELSE CASE WHEN TSPL_MF_BOM_DETAIL.CONSM_QUANTITY>0 THEN ((TSPL_MF_BOM_DETAIL.CONSM_QUANTITY*TSPL_LOCATION_MASTER.Silo_Capacity*1000)/TSPL_MF_BOM_HEAD.PROD_QUANTITY) ELSE 0 END END) AS 'REQ_STOCK',0 AS 'MIN_LEVEL' FROM TSPL_MF_BOM_HEAD LEFT OUTER JOIN TSPL_MF_BOM_DETAIL ON TSPL_MF_BOM_DETAIL.BOM_CODE=TSPL_MF_BOM_HEAD.BOM_CODE LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE left outer join TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code=TSPL_MF_BOM_HEAD.LOCATION_CODE INNER join (select PROD_ITEM_CODE,MAX(BOM_CODE) AS 'BOM_CODE',MAX(REVISION_NO) AS 'REVISION_NO' from TSPL_MF_BOM_HEAD WHERE 2=2 and TSPL_MF_BOM_HEAD.LOCATION_CODE IN ('" + clsCommon.myCstr(dtLocation.Rows(ll).Item("LOCATION_CODE")) + "') GROUP BY PROD_ITEM_CODE ) BOM_LATEST ON BOM_LATEST.BOM_CODE=TSPL_MF_BOM_HEAD.BOM_CODE AND BOM_LATEST.REVISION_NO=TSPL_MF_BOM_HEAD.REVISION_NO WHERE TSPL_ITEM_MASTER.Structure_Code IN ('RM') and tspl_item_master.Item_Code in((select distinct TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE from TSPL_SPP_PRODUCTION_ENTRY_DETAIL left outer join TSPL_SPP_PRODUCTION_ENTRY on TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE left outer join TSPL_MF_BOM_HEAD on TSPL_MF_BOM_HEAD.BOM_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.BOM_CODE left outer join TSPL_MF_BOM_DETAIL on TSPL_MF_BOM_DETAIL.BOM_CODE=TSPL_MF_BOM_HEAD.BOM_CODE left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE where CONVERT(DATE,PROD_DATE,103)>= convert(date,'" + clsCommon.GetPrintDate(fDate, "dd/MMM/yyyy") + "',103) and CONVERT(DATE,PROD_DATE,103)<= convert(date,'" + clsCommon.GetPrintDate(Date2, "dd/MMM/yyyy") + "',103) and TSPL_SPP_PRODUCTION_ENTRY.LOCATION_CODE='" + clsCommon.myCstr(dtLocation.Rows(ll).Item("LOCATION_CODE")) + "' and TSPL_ITEM_MASTER.FG_for_CF_PL=1 ) ) and TSPL_MF_BOM_HEAD.LOCATION_CODE IN ('" + clsCommon.myCstr(dtLocation.Rows(ll).Item("LOCATION_CODE")) + "') GROUP BY TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE ) RM_STOCK_DAYS GROUP BY RM_STOCK_DAYS.ITEM_CODE )xx left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=xx.ITEM_CODE and UPPER(TSPL_ITEM_UOM_DETAIL.UOM_Code)='QTL' left outer join TSPL_ITEM_UOM_DETAIL KG_UOM_DETAIL on KG_UOM_DETAIL.Item_Code=xx.ITEM_CODE and UPPER(KG_UOM_DETAIL.UOM_Code)='KG' WHERE xx.STOCK_QTY>0 ) FINAL WHERE FINAL.QTY_FOR_DAYS<=3 )YYY "
                    dttop3item = clsDBFuncationality.GetDataTable(itemcodeqry)
                    If dttop3item IsNot Nothing Then
                        dtfinal.Merge(dttop3item)
                    End If
                Next
                For kk As Integer = 0 To dtfinal.Rows.Count - 1
                    If clsCommon.myLen(itemcode) > 0 Then
                        itemcode += "Union all select "
                        itemcode += " '" + clsCommon.myCstr(dtfinal.Rows(kk)("ITEM_CODE")) + "' as [" + clsCommon.myCstr(dtfinal.Rows(kk)("ITEM_CODE")) + "] "
                        itemcode += ", '" + clsCommon.myCstr(dtfinal.Rows(kk)("Location_Code")) + "' As [" + clsCommon.myCstr(dtfinal.Rows(kk)("Location_Code")) + "] "
                    Else
                        itemcode = " Select'" + clsCommon.myCstr(dtfinal.Rows(kk)("ITEM_CODE")) + "'  as [" + clsCommon.myCstr(dtfinal.Rows(kk)("ITEM_CODE")) + "]   "
                        itemcode = ", '" + clsCommon.myCstr(dtfinal.Rows(kk)("Location_Code")) + "' As [" + clsCommon.myCstr(dtfinal.Rows(kk)("Location_Code")) + "] "
                    End If
                Next
                Dim StrQry As String = " " + itemcode + " "
                query = "select ROW_NUMBER() OVER(ORDER BY TSPL_LOCATION_MASTER.Location_code ASC) as SNo
                        ,TSPL_LOCATION_MASTER.Loc_Short_Name as [Location],format(convert(date,'" + fromDate.Value + "',103), 'dd MMMM yyyy') as Date,upper(format(convert(date,'" + fromDate.Value + "',103), 'MMMM yyyy'))as Date1,
                        cast(cast((TSPL_LOCATION_MASTER.remarks) AS DECIMAL(18,0))/(day(eomonth('" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "'))) AS DECIMAL(18,0)) as [Capacity],
                        NoOfShift
                        ,CAST((ProdDailyQty.Qty/1000) AS DECIMAL(18,0)) as ProdDailyQty
                        ,CAST((ProdCumQty.Qty/1000) AS DECIMAL(18,0)) as ProdCumQty
                        ,case when TSPL_LOCATION_MASTER.Silo_Capacity>0 then CAST(ProdDailyQty.Qty*100/((cast(cast((TSPL_LOCATION_MASTER.remarks) AS DECIMAL(18,0))/(day(eomonth('" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "'))) AS DECIMAL(18,0)))*1000) AS DECIMAL(18,0)) else 0 end as CUD
                        ,case when TSPL_LOCATION_MASTER.Silo_Capacity>0 then CAST((ProdCumQty.Qty*100)/((cast(cast((TSPL_LOCATION_MASTER.remarks) AS DECIMAL(18,0))/(day(eomonth('" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "'))) AS DECIMAL(18,0)))*1000*" + clsCommon.myCstr(DayCount) + ") AS DECIMAL(18,0)) else 0 end as CUM
                        ,case when TSPL_LOCATION_MASTER.Silo_Capacity>0 then CAST((ProdCumQty.Qty*100)/((cast(cast((TSPL_LOCATION_MASTER.remarks) AS DECIMAL(18,0))/(day(eomonth('" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "'))) AS DECIMAL(18,0)))*1000*" + clsCommon.myCstr(DayCount) + ") AS DECIMAL(18,0)) else 0 end as CUY
                        ,CAST(SaleDailyQty.Qty AS DECIMAL(18,0)) as SaleDailyQty
                        ,CAST(SaleCumQty.Qty AS DECIMAL(18,0)) as SaleCumQty
                        ,CAST(FGS.Qty AS DECIMAL(18,0)) as FGS
                        ,case when isnull(PSO.Qty,0)<0 then 0 else CAST(isnull(PSO.Qty,0) AS DECIMAL(18,0)) end as PSO
                        ,BreakDown.BreakdownHRS
                        ,BreakDown.BreakdownREASON
                        FROM TSPL_LOCATION_MASTER 

                        Left outer join 
						(select count (TSPL_SPP_PRODUCTION_ENTRY.Shift_Code) as NoOfShift,TSPL_SPP_PRODUCTION_ENTRY.LOCATION_CODE  from TSPL_SPP_PRODUCTION_ENTRY 
						WHERE CONVERT (DATE,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103) BETWEEN CONVERT(DATE,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103) AND CONVERT(DATE,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103)  
                        AND TSPL_SPP_PRODUCTION_ENTRY.Shift_Code in ('A-SHIFT','B-SHIFT','C-SHIFT') 
                           Group By LOCATION_CODE,CONVERT(DATE,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103)) NoOfShift
						   ON TSPL_LOCATION_MASTER.LOCATION_CODE = NoOfShift.LOCATION_CODE

                         LEFT OUTER JOIN
                        (select sum(TSPL_SPP_PRODUCTION_ENTRY_DETAIL.FINAL_PRODUCTION_QTY) as Qty,TSPL_SPP_PRODUCTION_ENTRY.LOCATION_CODE from TSPL_SPP_PRODUCTION_ENTRY_DETAIL
                         left join TSPL_SPP_PRODUCTION_ENTRY on TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE
                         LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE
                         where TSPL_Item_Master.FG_for_CF_RPT=1 AND TSPL_SPP_PRODUCTION_ENTRY.posted=1 and convert(date,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103)=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103)
                          GROUP BY TSPL_SPP_PRODUCTION_ENTRY.LOCATION_CODE) ProdDailyQty
                          ON TSPL_LOCATION_MASTER.LOCATION_CODE =ProdDailyQty.LOCATION_CODE
                         LEFT OUTER JOIN
                        (select sum(TSPL_SPP_PRODUCTION_ENTRY_DETAIL.FINAL_PRODUCTION_QTY) as Qty,TSPL_SPP_PRODUCTION_ENTRY.LOCATION_CODE from TSPL_SPP_PRODUCTION_ENTRY_DETAIL
                         left join TSPL_SPP_PRODUCTION_ENTRY on TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE
                         LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE
                         where TSPL_Item_Master.FG_for_CF_RPT=1 and TSPL_SPP_PRODUCTION_ENTRY.posted=1 and convert(date,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103)>=convert(date,'" + clsCommon.GetPrintDate(fDate, "dd/MMM/yyyy") + "',103)
                         and convert(date,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103)<=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103)
                          GROUP BY TSPL_SPP_PRODUCTION_ENTRY.LOCATION_CODE) ProdCumQty
                          ON TSPL_LOCATION_MASTER.LOCATION_CODE =ProdCumQty.LOCATION_CODE
                        LEFT OUTER JOIN
                        (SELECT SUM((isnull(TSPL_SD_SHIPMENT_DETAIL.Qty,0)*FromUOM.Conversion_Factor)/ToUOM.Conversion_Factor) AS Qty,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location FROM 
                        TSPL_SD_SHIPMENT_DETAIL left join 
                        TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code=TSPL_SD_SHIPMENT_DETAIL.document_code
                        LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SD_SHIPMENT_DETAIL.ITEM_CODE
                         left outer join TSPL_ITEM_UOM_DETAIL FromUOM on FromUOM.Item_Code =TSPL_SD_SHIPMENT_DETAIL.Item_Code 
						AND FromUOM.UOM_Code=TSPL_SD_SHIPMENT_DETAIL.Unit_code
						left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SD_SHIPMENT_DETAIL.item_code and ToUOM.UOM_Code='MT'
                        WHERE TSPL_Item_Master.FG_for_CF_RPT=1 AND TSPL_SD_SHIPMENT_HEAD.Status=1 and convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103)=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103)
                        GROUP BY TSPL_SD_SHIPMENT_HEAD.Bill_To_Location) SaleDailyQty
                        ON TSPL_LOCATION_MASTER.LOCATION_CODE =SaleDailyQty.Bill_To_Location 
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
                        GROUP BY TSPL_SD_SHIPMENT_HEAD.Bill_To_Location)  SaleCumQty
                        ON TSPL_LOCATION_MASTER.LOCATION_CODE =SaleCumQty.Bill_To_Location 
                         LEFT OUTER JOIN
                        (SELECT SUM(isnull(TSPL_SD_SHIPMENT_HEAD.Order_Qty,0))-SUM((isnull(TSPL_SD_SHIPMENT_DETAIL.Qty,0)*FromUOM.Conversion_Factor)/ToUOM.Conversion_Factor) AS Qty,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location FROM 
                        TSPL_SD_SHIPMENT_DETAIL left join 
                        TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code=TSPL_SD_SHIPMENT_DETAIL.document_code
                        LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SD_SHIPMENT_DETAIL.ITEM_CODE
                        left outer join TSPL_ITEM_UOM_DETAIL FromUOM on FromUOM.Item_Code =TSPL_SD_SHIPMENT_DETAIL.Item_Code 
						AND FromUOM.UOM_Code=TSPL_SD_SHIPMENT_DETAIL.Unit_code
						left outer join TSPL_ITEM_UOM_DETAIL as ToUOM ON ToUOM.item_code=TSPL_SD_SHIPMENT_DETAIL.item_code and ToUOM.UOM_Code='MT'
                        WHERE TSPL_Item_Master.FG_for_CF_RPT=1 AND TSPL_SD_SHIPMENT_HEAD.Status=1 and convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103)=convert(date,'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "',103)
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


                query += " LEFT OUTER JOIN (" + queryStock + " ) FGS ON TSPL_LOCATION_MASTER.LOCATION_CODE =FGS.Location_Code"

                query += " where TSPL_LOCATION_MASTER.IsMainPlant='0'"

            ElseIf rdbWeekly.Checked = True Then
                'fDate = CDate(clsDBFuncationality.getSingleValue("select DATEADD(DAY,2-DATEPART(WEEKDAY,convert(date,'" + fromDate.Value + "',103)),convert(date,'" + fromDate.Value + "',103))"))
                'tDate = CDate(clsDBFuncationality.getSingleValue("select DATEADD(DAY,8-DATEPART(WEEKDAY,convert(date,'" + fromDate.Value + "',103)),convert(date,'" + fromDate.Value + "',103))"))
                'dtpFrom.Value = fDate
                'toDate.Value = tDate
                'dtcurrent = fromDate.Value
                'dtnext = toDate.Value
                fDate = fromDate.Value
                tDate = toDate.Value
                DayCount = DateDiff(DateInterval.Day, fDate, tDate) + 1


                'Dim strLocation As String = clsDBFuncationality.getSingleValue("  DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' + QUOTENAME(TSPL_LOCATION_MASTER.location_code) as Alies_Name FROM TSPL_LOCATION_MASTER where TSPL_LOCATION_MASTER.IsMainPlant='0' FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
                'Dim strMainLocation As String = clsDBFuncationality.getSingleValue("SELECT TSPL_LOCATION_MASTER.Loc_Short_Name FROM TSPL_LOCATION_MASTER where TSPL_LOCATION_MASTER.IsMainPlant='1'")
                Dim StrTempQry As String = "DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT  
                     STUFF((SELECT distinct ',' +'Sum(isnull('  + QUOTENAME(TSPL_LOCATION_MASTER.location_code)+',0))'
                     +' as ' + QUOTENAME( TSPL_LOCATION_MASTER.location_code)
                    as Alies_Name FROM TSPL_LOCATION_MASTER where TSPL_LOCATION_MASTER.IsMainPlant='0' FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')"
                Dim strSumLocation As String = clsDBFuncationality.getSingleValue(StrTempQry)
                StrTempQry = "DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT  
                              STUFF((SELECT distinct '+' +'Sum(isnull(' + QUOTENAME(TSPL_LOCATION_MASTER.Location_Code) +',0))' as Alies_Name
                              FROM TSPL_LOCATION_MASTER where TSPL_LOCATION_MASTER.IsMainPlant='0' FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')"
                Dim strTotalLocation As String = clsDBFuncationality.getSingleValue(StrTempQry)

                Dim strLocation As String = clsDBFuncationality.getSingleValue("  DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' + QUOTENAME(TSPL_LOCATION_MASTER.location_code) as Alies_Name FROM TSPL_LOCATION_MASTER where TSPL_LOCATION_MASTER.IsMainPlant='0' FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
                Dim strMainLocation As String = clsDBFuncationality.getSingleValue("SELECT '[' + TSPL_LOCATION_MASTER.location_code + ']' FROM TSPL_LOCATION_MASTER where TSPL_LOCATION_MASTER.IsMainPlant='1'")

                StrTempQry = "DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT  
                     STUFF((SELECT distinct ',' +'max('  + QUOTENAME(TSPL_LOCATION_MASTER.location_code)+')'
                     +' as ' + QUOTENAME( TSPL_LOCATION_MASTER.location_code)
                    as Alies_Name FROM TSPL_LOCATION_MASTER where TSPL_LOCATION_MASTER.IsMainPlant='0' FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')"
                Dim strMaxLocation As String = clsDBFuncationality.getSingleValue(StrTempQry)

                query = "select Production," + strSumLocation + "," + strTotalLocation + " as " + strMainLocation + "
                         from (select 'Production' as Production,TSPL_LOCATION_MASTER.Location_Code
                        ,isnull(CAST((ProdCumQty.Qty/1000) AS DECIMAL(18,0)),0) as ProdCumQty
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
                          pivot ( sum(ProdCumQty) for Location_Code in (" + strLocation + ") )as zpivot group by zpivot.Production"
                query += " union all
                         select Production," + strSumLocation + "," + strTotalLocation + " as " + strMainLocation + "
                         from (select 'Avg Production' as Production,TSPL_LOCATION_MASTER.Location_Code
                        ,isnull(CAST(((ProdCumQty.Qty/1000)" + "/" + clsCommon.myCstr(DayCount) + ") AS DECIMAL(18,0)),0) as ProdCumQty
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

                query += " union all
                         select Production," + strSumLocation + "," + strTotalLocation + " as " + strMainLocation + "
                         from (select 'Capacity Utilization' as Production,TSPL_LOCATION_MASTER.Location_Code
                        ,case when TSPL_LOCATION_MASTER.Silo_Capacity=0 then 0 else isnull(CAST(((ProdCumQty.Qty/1000)" + "/(" + clsCommon.myCstr(DayCount) + "*TSPL_LOCATION_MASTER.Silo_Capacity))*100 AS DECIMAL(18,0)),0) end as ProdCumQty
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


                query = "select * from (" + query + ")final "

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


            If (dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0) Then
                If Print = True Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(CrystalReportFolder.PRODUCTION, dt2, "Daily_Production_sale_FG_stock_BD_report", "Daily Production Sale Report")
                    frmCRV = Nothing
                Else
                    gv1.Visible = True
                    gv1.DataSource = dt2
                    gv1.ReadOnly = True
                    SetGridFormat(gv1)
                    ReStoreGridLayout()
                    If rdbDaily.Checked = True Then
                        View()
                    End If
                End If
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found")
                gv1.DataSource = Nothing
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
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


        If rdbDaily.Checked = True Then
            'Gv1.Columns("NoOfShift").HeaderText = "NoOfShift" + Environment.NewLine + "Operated"
            Gv1.Columns("NoOfShift").HeaderText = "NoOfShift"
            Gv1.Columns("ProdDailyQty").HeaderText = "Daily"
            Gv1.Columns("ProdCumQty").HeaderText = "Cummulative" + Environment.NewLine + "(MTD)"
            Gv1.Columns("CUD").HeaderText = "DLY %"
            Gv1.Columns("CUM").HeaderText = "MTD %"
            Gv1.Columns("CUY").HeaderText = "YTD %"
            Gv1.Columns("SaleDailyQty").HeaderText = "Daily"
            Gv1.Columns("SaleCumQty").HeaderText = "Cummulative" + Environment.NewLine + "(MTD)"
            Gv1.Columns("PSO").HeaderText = "Pending" + Environment.NewLine + "Supply Orders"
            Gv1.Columns("BreakdownHRS").HeaderText = "HRS"
            Gv1.Columns("BreakdownREASON").HeaderText = "Reason"
            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim item1 As New GridViewSummaryItem("Capacity", "{0:F0}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Dim item2 As New GridViewSummaryItem("No of Shift", "{0:F0}", GridAggregateFunction.Sum)
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



            Dim item8 As New GridViewSummaryItem("SaleDailyQty", "{0:F0}", GridAggregateFunction.Sum)
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
            Dim dtLocation As DataTable = clsDBFuncationality.GetDataTable("SELECT TSPL_LOCATION_MASTER.location_code,TSPL_LOCATION_MASTER.Loc_Short_Name,cast(TSPL_LOCATION_MASTER.Silo_Capacity as int) as Silo_Capacity FROM TSPL_LOCATION_MASTER where TSPL_LOCATION_MASTER.IsMainPlant=0")
            Dim strMainLocation As DataTable = clsDBFuncationality.GetDataTable("SELECT TSPL_LOCATION_MASTER.location_code,TSPL_LOCATION_MASTER.Loc_Short_Name FROM TSPL_LOCATION_MASTER where TSPL_LOCATION_MASTER.IsMainPlant=1")

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


    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        reset()
    End Sub
    Sub reset()
        'fromDate.Value = clsCommon.GETSERVERDATE()
        RadPageView1.SelectedPage = RadPageViewPage1
        gv1.ViewDefinition = New TableViewDefinition
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.Visible = False
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub


    Private Sub btnReport_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReport.Click
        Try
            PageSetupReport_ID = Me.Form_ID + clsCommon.myCstr(IIf(rdbDaily.Checked = True, "Daily", "Weekly"))
            TemplateGridview = gv1
            fillGridReport(False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
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
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
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
            arrHeader.Add("Report Type : " + IIf(rdbDaily.Checked = True, "Daily", "Weekly"))
            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv1, "", StrReportName, , arrHeader)
                'Else
                'transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                'clsCommon.MyExportToPDF(Label1.Text, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If

            If rdbDaily.Checked = True Then

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
                doc.AssociatedObject = gv1

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
                doc.AssociatedObject = gv1
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
        If gv1.Rows.Count > 0 Then
            Dim view As New ColumnGroupsViewDefinition()
            view.ColumnGroups.Add(New GridViewColumnGroup(""))
            Dim groupRow0 = New GridViewColumnGroupRow()
            groupRow0.MinHeight = 30
            view.ColumnGroups(0).Rows.Add(groupRow0)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("SNo").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Location").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Capacity").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("NoofShift").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Production"))
            Dim groupRow1 = New GridViewColumnGroupRow()
            groupRow1.MinHeight = 30
            view.ColumnGroups(1).Rows.Add(groupRow1)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("ProdDailyQty").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("ProdCumQty").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Capacity Utilization"))
            Dim groupRow2 = New GridViewColumnGroupRow()
            groupRow2.MinHeight = 30
            view.ColumnGroups(2).Rows.Add(groupRow2)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("CUD").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("CUM").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("CUY").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Sale"))
            Dim groupRow3 = New GridViewColumnGroupRow()
            groupRow3.MinHeight = 30
            view.ColumnGroups(3).Rows.Add(groupRow3)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv1.Columns("SaleDailyQty").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv1.Columns("SaleCumQty").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup(""))
            Dim groupRow4 = New GridViewColumnGroupRow()
            groupRow4.MinHeight = 30
            view.ColumnGroups(4).Rows.Add(groupRow4)
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv1.Columns("FGS").Name)
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv1.Columns("PSO").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Breakdown"))
            Dim groupRow5 = New GridViewColumnGroupRow()
            groupRow5.MinHeight = 30
            view.ColumnGroups(5).Rows.Add(groupRow5)
            view.ColumnGroups(5).Rows(0).ColumnNames.Add(gv1.Columns("BreakdownHRS").Name)
            view.ColumnGroups(5).Rows(0).ColumnNames.Add(gv1.Columns("BreakdownREASON").Name)

            gv1.ViewDefinition = view
        End If
    End Sub

    Private Sub rdbDaily_CheckedChanged(sender As Object, e As EventArgs) Handles rdbDaily.CheckedChanged
        If rdbDaily.Checked = True Then
            lbltoDate.Visible = False
            toDate.Visible = False
        ElseIf rdbWeekly.Checked = True Then
            lbltoDate.Visible = True
            toDate.Visible = True
        End If
    End Sub

    Private Sub RadMenuItem3_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
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
End Class
