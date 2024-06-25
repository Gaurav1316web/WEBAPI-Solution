Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Public Class MSIProductionSaleReport
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim Slot1FD As DateTime = Nothing
    Dim Slot1TD As DateTime = Nothing
    Dim Slot2FD As DateTime = Nothing
    Dim Slot2TD As DateTime = Nothing
    Dim Slot3FD As DateTime = Nothing
    Dim Slot3TD As DateTime = Nothing
#End Region

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        FromDate.Value = clsCommon.GETSERVERDATE()
        txtLocation.Value = Nothing
        lblLocation.Text = ""
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            Dim dt As DataTable = Nothing

            Dim whr As String = ""
            If txtLocation.Value IsNot Nothing AndAlso txtLocation.Value.Count > 0 Then
                whr += " and LOCATION_CODE IN ('" + clsCommon.myCstr(txtLocation.Value) + "') "
            Else
                'whr += " and TSPL_LOCATION_MASTER.Location_Type='Physical'  "
                'If clsCommon.myLen(arrLoc) > 0 Then
                'whr += "  and  LOCATION_CODE IN (" + clsCommon.GetMulcallStringWithComma(TxtMultiLocation.arrValueMember) + ") "
                'End If
            End If


            Dim DailySalesrptqry As String = "select max(TSPL_SD_SHIPMENT_HEAD.Document_Date) as DocDate,sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SD_SHIPMENT_DETAIL.Qty)/1000 as Qty
                                          from TSPL_SD_SHIPMENT_DETAIL
                                          left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code =TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE
                                          left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code
                                          left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code
                                          LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code 
                                          AND TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_SD_SHIPMENT_DETAIL.Unit_code
				                          where convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103)=convert(date,'" + clsCommon.GetPrintDate(FromDate.Value) + "',103) AND Location IN ('" + clsCommon.myCstr(txtLocation.Value) + "')  and FG_for_CF_RPT=1 "
            Dim dtsalesdaily As DataTable = clsDBFuncationality.GetDataTable(DailySalesrptqry)

            Dim DailySalesrptperiodicallyqry As String = "select max(TSPL_SD_SHIPMENT_HEAD.Document_Date) as DocDate,sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SD_SHIPMENT_DETAIL.Qty)/1000 as Qty
                                                      from TSPL_SD_SHIPMENT_DETAIL
                                                      left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code =TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE
                                                      left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code
                                                      left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code
                                                      LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code 
                                                      AND TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_SD_SHIPMENT_DETAIL.Unit_code
				                                      where convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(Slot1FD) + "',103) 
				                                      and convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(FromDate.Value) + "',103) AND Location IN ('" + clsCommon.myCstr(txtLocation.Value) + "') and FG_for_CF_RPT=1 "
            Dim dtsalesperiodically As DataTable = clsDBFuncationality.GetDataTable(DailySalesrptperiodicallyqry)

            Dim Inventoryreport As String = "select SUM(CASE WHEN INOUT='I' THEN 1 ELSE -1 END * stock_Qty)/1000 AS QTY from tspl_inventory_movement
                                         INNER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=tspl_inventory_movement.Item_Code 
                                         where  FG_for_CF_RPT=1 AND Location_Code IN ('" + clsCommon.myCstr(txtLocation.Value) + "') AND convert(date,tspl_inventory_movement.Source_Doc_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(FromDate.Value) + "',103) "

            Dim dtinventory As DataTable = clsDBFuncationality.GetDataTable(Inventoryreport)

            Dim ShiftOperated As String = "SELECT COUNT(DISTINCT Shift_Code) as ShiftOperated FROM TSPL_SPP_PRODUCTION_ENTRY 
                                       WHERE convert(date,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103)=convert(date,'" + clsCommon.GetPrintDate(FromDate.Value) + "',103) AND LOCATION_CODE IN ('" + clsCommon.myCstr(txtLocation.Value) + "')"

            Dim dtshift As DataTable = clsDBFuncationality.GetDataTable(ShiftOperated)

            Dim Productionrptdaily As String = "SELECT SUM(FINAL_PRODUCTION_QTY)/1000 AS QTY FROM TSPL_SPP_PRODUCTION_ENTRY_DETAIL 
                                         LEFT OUTER JOIN TSPL_SPP_PRODUCTION_ENTRY ON TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE
                                         LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.Item_Code
                                         WHERE convert(date,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103)=convert(date,'" + clsCommon.GetPrintDate(FromDate.Value) + "',103) AND TSPL_SPP_PRODUCTION_ENTRY_DETAIL.LOCATION_CODE IN ('" + clsCommon.myCstr(txtLocation.Value) + "')  AND FG_for_CF_RPT=1 "

            Dim dtproductiondaily As DataTable = clsDBFuncationality.GetDataTable(Productionrptdaily)

            Dim Productionrptperiodically As String = "SELECT SUM(FINAL_PRODUCTION_QTY)/1000 AS QTY FROM TSPL_SPP_PRODUCTION_ENTRY_DETAIL 
                                            LEFT OUTER JOIN TSPL_SPP_PRODUCTION_ENTRY ON TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE
                                            LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.Item_Code
                                            WHERE   FG_for_CF_RPT=1  AND convert(date,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103)>=convert(date,'" + clsCommon.GetPrintDate(Slot1FD) + "',103) AND convert(date,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103)<=convert(date,'" + clsCommon.GetPrintDate(FromDate.Value) + "',103) AND TSPL_SPP_PRODUCTION_ENTRY_DETAIL.LOCATION_CODE IN ('" + clsCommon.myCstr(txtLocation.Value) + "')"

            Dim dtproductionperiodically As DataTable = clsDBFuncationality.GetDataTable(Productionrptperiodically)

            Dim Shortitemqry As String = "SELECT '" + clsCommon.GetPrintDate(FromDate.Value) + "' as Date, FINAL.Location_Code,FINAL.Location_Desc,FINAL.Add1,FINAL.Add2,FINAL.Add3,FINAL.Add4,FINAL.ITEM_CODE, FINAL.Item_Desc,FINAL.Unit,FINAL.STOCK_QTY,FINAL.QTY_FOR_DAYS FROM 
                                      (select xx.Item_Desc,xx.ITEM_CODE,xx.Location_Code,xx.Location_Desc,xx.Add1,xx.Add2,xx.Add3,xx.Add4,
                                      case when xx.UOM='KG' THEN CASE WHEN ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)=0 THEN xx.UOM ELSE TSPL_ITEM_UOM_DETAIL.UOM_Code END 
	                                  WHEN xx.UOM='GM' THEN CASE WHEN ISNULL(KG_UOM_DETAIL.Conversion_Factor,0)=0 THEN xx.UOM ELSE KG_UOM_DETAIL.UOM_Code END
                                      ELSE xx.UOM END AS 'Unit',CAST((case when xx.UOM='KG' THEN CASE WHEN ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)<>0 THEN (xx.STOCK_QTY/ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1)) ELSE xx.STOCK_QTY END 
	                                  WHEN xx.UOM='GM' THEN CASE WHEN ISNULL(KG_UOM_DETAIL.Conversion_Factor,0)<>0 THEN (xx.STOCK_QTY/ISNULL(KG_UOM_DETAIL.Conversion_Factor,1)) ELSE xx.STOCK_QTY END
                                      ELSE xx.STOCK_QTY END) AS numeric(10,0)) AS 'STOCK_QTY',cast(xx.QTY_FOR_DAYS as integer)  as QTY_FOR_DAYS  
                                      from (SELECT max(RM_STOCK_DAYS.Location_Code)Location_Code,max(RM_STOCK_DAYS.Location_Desc)Location_Desc,max(RM_STOCK_DAYS.Add1)Add1,max(RM_STOCK_DAYS.Add2)Add2,max(RM_STOCK_DAYS.Add3)Add3,max(RM_STOCK_DAYS.Add4)Add4,
                                      RM_STOCK_DAYS.ITEM_CODE,max(RM_STOCK_DAYS.Item_Desc) as Item_Desc,max(RM_STOCK_DAYS.UOM)  AS 'UOM',SUM(ISNULL(RM_STOCK_DAYS.STOCK_QTY,0))  AS 'STOCK_QTY',
                                      SUM(ISNULL(RM_STOCK_DAYS.REQ_STOCK,0)) AS REQ_STOCK,SUM(ISNULL(RM_STOCK_DAYS.MIN_LEVEL,0)) AS MIN_LEVEL,
	                                  CASE WHEN SUM(ISNULL(RM_STOCK_DAYS.REQ_STOCK,0))<>0 THEN SUM(ISNULL(RM_STOCK_DAYS.STOCK_QTY,0))/SUM(ISNULL(RM_STOCK_DAYS.REQ_STOCK,0)) ELSE 
	                                  CASE WHEN SUM(ISNULL(RM_STOCK_DAYS.MIN_LEVEL,0))<>0 THEN SUM(ISNULL(RM_STOCK_DAYS.STOCK_QTY,0))/SUM(ISNULL(RM_STOCK_DAYS.MIN_LEVEL,0)) ELSE 0 END END AS 'QTY_FOR_DAYS' 
	                                  FROM  (SELECT max(RM_STOCK.Location_Code)Location_Code,max(RM_STOCK.Location_Desc)Location_Desc,max(RM_STOCK.Add1)Add1,max(RM_STOCK.Add2)Add2,max(RM_STOCK.Add3)Add3,max(RM_STOCK.Add4)Add4,
                                      RM_STOCK.ITEM_CODE,max(RM_STOCK.Item_Desc) as Item_Desc,MAX(RM_STOCK.UOM) AS 'UOM',SUM(ISNULL(RM_STOCK.IN_STOCK_QTY,0))-SUM(ISNULL(RM_STOCK.OUT_STOCK_QTY,0)) AS 'STOCK_QTY',0 AS 'REQ_STOCK', 0 AS 'MIN_LEVEL' FROM (
	                                  SELECT TSPL_LOCATION_MASTER.Location_Code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_LOCATION_MASTER.Add1,TSPL_LOCATION_MASTER.Add2,TSPL_LOCATION_MASTER.Add3,TSPL_LOCATION_MASTER.Add4,
                                      TSPL_INVENTORY_MOVEMENT.Item_Code AS 'ITEM_CODE',TSPL_ITEM_MASTER.Short_Description AS 'ITEM_DESC',TSPL_ITEM_MASTER.Unit_Code AS 'UOM',
	                                  CASE WHEN INOUT='I' THEN STOCK_QTY END AS 'IN_STOCK_QTY',CASE WHEN INOUT='O' THEN STOCK_QTY END AS 'OUT_STOCK_QTY' FROM TSPL_INVENTORY_MOVEMENT
	                                  LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT.Item_Code
                                      left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_INVENTORY_MOVEMENT.Location_Code
	                                  WHERE 2=2 And TSPL_INVENTORY_MOVEMENT.Location_Code IN ('" + clsCommon.myCstr(txtLocation.Value) + "') AND TSPL_ITEM_MASTER.Structure_Code IN ('RM')and tspl_item_master.Item_Code in ('RM0001','RM0003','RM0004','RM0006','RM0007','RM0008','RM0010','RM0012','RM0014','RM0015','RM0016','RM0018','RM0019')) RM_STOCK
	                                  GROUP BY  RM_STOCK.Item_Code 	UNION ALL
	                                  SELECT max(TSPL_LOCATION_MASTER.Location_Code)LocationCode,max(TSPL_LOCATION_MASTER.Location_Desc)LocDes,max(TSPL_LOCATION_MASTER.Add1)Add1,max(TSPL_LOCATION_MASTER.Add2)Add2,max(TSPL_LOCATION_MASTER.Add3)Add3,max(TSPL_LOCATION_MASTER.Add4)Add4,
                                      TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE,max(TSPL_ITEM_MASTER.Short_Description) as Item_Desc,max(TSPL_ITEM_MASTER.Unit_Code) as 'UOM',
	                                  0 AS 'STOCK_QTY',	AVG(CASE WHEN TSPL_MF_BOM_DETAIL.Percentage>0 THEN (TSPL_MF_BOM_DETAIL.Percentage*TSPL_LOCATION_MASTER.Silo_Capacity*1000)/100 ELSE 
                                      CASE WHEN TSPL_MF_BOM_DETAIL.CONSM_QUANTITY>0 THEN ((TSPL_MF_BOM_DETAIL.CONSM_QUANTITY*TSPL_LOCATION_MASTER.Silo_Capacity*1000)/TSPL_MF_BOM_HEAD.PROD_QUANTITY) ELSE 0 END
                                      END)  AS 'REQ_STOCK',0 AS 'MIN_LEVEL' FROM TSPL_MF_BOM_HEAD 
	                                  LEFT OUTER JOIN TSPL_MF_BOM_DETAIL ON TSPL_MF_BOM_DETAIL.BOM_CODE=TSPL_MF_BOM_HEAD.BOM_CODE
	                                  LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE
	                                  left outer join TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code=TSPL_MF_BOM_HEAD.LOCATION_CODE
	                                  INNER join (select PROD_ITEM_CODE,MAX(BOM_CODE) AS 'BOM_CODE',MAX(REVISION_NO) AS 'REVISION_NO' from TSPL_MF_BOM_HEAD WHERE 2=2 
                                      and TSPL_MF_BOM_HEAD.LOCATION_CODE IN ('" + clsCommon.myCstr(txtLocation.Value) + "') GROUP BY PROD_ITEM_CODE	) BOM_LATEST ON BOM_LATEST.BOM_CODE=TSPL_MF_BOM_HEAD.BOM_CODE AND BOM_LATEST.REVISION_NO=TSPL_MF_BOM_HEAD.REVISION_NO
	                                  WHERE  TSPL_ITEM_MASTER.Structure_Code IN ('RM') and tspl_item_master.Item_Code in('RM0001','RM0003','RM0004','RM0006','RM0007','RM0008','RM0010','RM0012','RM0014','RM0015','RM0016','RM0018','RM0019')   and 	TSPL_MF_BOM_HEAD.LOCATION_CODE IN ('" + clsCommon.myCstr(txtLocation.Value) + "')  GROUP BY TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE
	                                  UNION ALL
	                                  select TSPL_LOCATION_MASTER.Location_Code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_LOCATION_MASTER.Add1,TSPL_LOCATION_MASTER.Add2,TSPL_LOCATION_MASTER.Add3,TSPL_LOCATION_MASTER.Add4,
                                      TSPL_ITEM_REORDER_LEVEL_NEW.Item_Code ,TSPL_ITEM_MASTER.Short_Description AS 'ITEM_DESC',TSPL_ITEM_MASTER.Unit_Code AS 'UOM',
	                                  0 AS 'STOCK_QTY', 0 AS 'REQ_STOCK',TSPL_ITEM_REORDER_LEVEL_NEW.Min_Level AS 'MIN_LEVEL' from TSPL_ITEM_REORDER_LEVEL_NEW 
	                                  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_REORDER_LEVEL_NEW.Item_Code
                                      left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_ITEM_REORDER_LEVEL_NEW.Location_Code
	                                  where TSPL_ITEM_MASTER.Structure_Code IN ('RM') and tspl_item_master.Item_Code in 
                                      ('RM0001','RM0003','RM0004','RM0006','RM0007','RM0008','RM0010','RM0012','RM0014','RM0015','RM0016','RM0018','RM0019') 
                                      and Apply='Y'  AND TSPL_ITEM_REORDER_LEVEL_NEW.Location_Code IN ('" + clsCommon.myCstr(txtLocation.Value) + "') ) RM_STOCK_DAYS
                                      GROUP BY  RM_STOCK_DAYS.ITEM_CODE )xx 
                                      left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=xx.ITEM_CODE and UPPER(TSPL_ITEM_UOM_DETAIL.UOM_Code)='QTL'
                                      left outer join TSPL_ITEM_UOM_DETAIL KG_UOM_DETAIL on KG_UOM_DETAIL.Item_Code=xx.ITEM_CODE and UPPER(KG_UOM_DETAIL.UOM_Code)='KG'
                                      WHERE XX.ITEM_CODE NOT IN ('PM0001','PM0002') and xx.STOCK_QTY>0 ) FINAL WHERE FINAL.QTY_FOR_DAYS<=3 "

            dt = clsDBFuncationality.GetDataTable(Shortitemqry)

            Dim Breakdown As String = "select DATEDIFF(HOUR,Start_Time,End_Time)  as Hrs,Break_Down_Code ,Name as Reason  from TSPL_BREAK_DOWN_ENTRY 
                                      left outer join TSPL_BREAK_DOWN_MASTER on TSPL_BREAK_DOWN_MASTER.Code	=TSPL_BREAK_DOWN_ENTRY.Break_Down_Code
                                      where Doc_Date= '" + clsCommon.GetPrintDate(FromDate.Value) + "'and Location_Code IN ('" + clsCommon.myCstr(txtLocation.Value) + "')"

            Dim dtbreakdown As DataTable = clsDBFuncationality.GetDataTable(Breakdown)
            If dt IsNot Nothing And dt.Rows.Count <= 0 Then
                Dim qry As String = "  SELECT '" + clsCommon.GetPrintDate(FromDate.Value) + "' as Date,'" + txtLocation.Value + "' as Location_Code,'" + lblLocation.Text + "' as Location_Desc "
                dt = clsDBFuncationality.GetDataTable(qry)
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funsubreportWithdt(Nothing, CrystalReportFolder.SalesReport, dt, dtshift, "rptMSIProductionSaleReport", "", Nothing, "SubShift.rpt", "SubPrdDaily.rpt", dtproductiondaily, "SubPrdPeriodically.rpt", dtproductionperiodically, "SubSaleDaily.rpt", dtsalesdaily, "SubSalePeriodically.rpt", dtsalesperiodically, "SubInventory.rpt", dtinventory, "Subbreakdown.rpt", dtbreakdown)

                'PDFPath = frmCRV.funsubreportWithdt(isPDFPath, CrystalReportFolder.MilkProcurement, dt, dtAdditionFinance, "crptMilkPurchaseBillPaymentProcessNewJPR", "", Nothing, "subAddition.rpt", "subDeduction.rpt", dtDeductionFinance, "subReduceDeduction.rpt", dtReduceDeduction, "subSaving.rpt", dtSaving, "SubAdditionOther.rpt", dtAdditionOther, "SubDeductionOther.rpt", dtDeductionOther)
                'frmCRV.funreport(CrystalReportFolder.SalesReport, dtsalesdaily, "rptMSIProductionSaleReport", "", Nothing)
                frmCRV = Nothing
            Else
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funsubreportWithdt(Nothing, CrystalReportFolder.SalesReport, dt, dtshift, "rptMSIProductionSaleReport", "", Nothing, "SubShift.rpt", "SubPrdDaily.rpt", dtproductiondaily, "SubPrdPeriodically.rpt", dtproductionperiodically, "SubSaleDaily.rpt", dtsalesdaily, "SubSalePeriodically.rpt", dtsalesperiodically, "SubInventory.rpt", dtinventory, "Subbreakdown.rpt", dtbreakdown)

            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try


    End Sub

    Private Sub txtLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocation._MYValidating

        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        Dim WhrCls As String = " Location_Type='Physical'  "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If

        txtLocation.Value = clsCommon.ShowSelectForm("VendorMafnd", qry, "Code", WhrCls, txtLocation.Value, "Code", isButtonClicked)
        lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
    End Sub

    Private Sub fromDate_ValueChanged(sender As Object, e As EventArgs) Handles FromDate.ValueChanged
        Dim selectedMonth As Integer = FromDate.Value.Month
        Dim selectedYear As Integer = FromDate.Value.Year

        Dim currentDate As New DateTime(selectedYear, selectedMonth, 1)
        Slot1FD = clsCommon.GetPrintDate(currentDate, "dd/MMM/yyyy")
        Slot1TD = clsCommon.GetPrintDate(currentDate.AddDays(9), "dd/MMM/yyyy")
        Slot2FD = clsCommon.GetPrintDate(currentDate.AddDays(10), "dd/MMM/yyyy")
        Slot2TD = clsCommon.GetPrintDate(currentDate.AddDays(19), "dd/MMM/yyyy")
        Slot3FD = clsCommon.GetPrintDate(currentDate.AddDays(20), "dd/MMM/yyyy")
        Slot3TD = clsCommon.GetPrintDate(currentDate.AddMonths(1).AddDays(-1), "dd/MMM/yyyy")
    End Sub

    Private Sub MSIProductionSaleReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FromDate.Value = clsCommon.GETSERVERDATE()
    End Sub
End Class