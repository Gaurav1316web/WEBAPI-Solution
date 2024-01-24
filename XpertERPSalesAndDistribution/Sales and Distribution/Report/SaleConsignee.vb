Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Public Class SaleConsignee
    Inherits FrmMainTranScreen
    Dim StrPermission As String
    Dim Slot1FD As DateTime = Nothing
    Dim Slot1TD As DateTime = Nothing
    Dim Slot2FD As DateTime = Nothing
    Dim Slot2TD As DateTime = Nothing
    Dim Slot3FD As DateTime = Nothing
    Dim Slot3TD As DateTime = Nothing


    Private Sub txtMonth_ValueChanged(sender As Object, e As EventArgs) Handles txtMonth.ValueChanged
        Dim selectedMonth As Integer = txtMonth.Value.Month
        Dim selectedYear As Integer = txtMonth.Value.Year

        Dim currentDate As New DateTime(selectedYear, selectedMonth, 1)
        Slot1FD = clsCommon.GetPrintDate(currentDate, "dd/MMM/yyyy")
        Slot1TD = clsCommon.GetPrintDate(currentDate.AddDays(9), "dd/MMM/yyyy")
        Slot2FD = clsCommon.GetPrintDate(currentDate.AddDays(10), "dd/MMM/yyyy")
        Slot2TD = clsCommon.GetPrintDate(currentDate.AddDays(19), "dd/MMM/yyyy")
        Slot3FD = clsCommon.GetPrintDate(currentDate.AddDays(20), "dd/MMM/yyyy")
        Slot3TD = clsCommon.GetPrintDate(currentDate.AddMonths(1).AddDays(-1), "dd/MMM/yyyy")
    End Sub
    'Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
    'Me.Close()
    'End Sub


    Private Sub BtnReset_Click_1(sender As Object, e As EventArgs) Handles BtnReset.Click
        txtMonth.Value = clsCommon.GETSERVERDATE()
        txtLocation.Value = Nothing
        lblLocation.Text = ""
    End Sub


    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click

        Try
            Dim arrItem As New List(Of String)
            Dim item As String = Nothing
            Dim SaleConsignee As String = "select '" + clsCommon.GetPrintDate((Slot1FD), "dd/MM/yyyy") + "' as FromDate,'" + clsCommon.GetPrintDate((Slot3TD), "dd/MM/yyyy") + "' as ToDate,
                                    TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location,MAX(TSPL_LOCATION_MASTER.Location_Desc) AS LOCDESC,MAX(TSPL_LOCATION_MASTER.Add1) AS LOCADD1,MAX(TSPL_LOCATION_MASTER.Add2) AS LOCADD2,
                                    MAX(TSPL_LOCATION_MASTER.Add3) AS LOCADD3,MAX(TSPL_LOCATION_MASTER.Add4) AS LOCADD4,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_SD_SALE_INVOICE_HEAD.Ship_To_Location,
                                    MAX(TSPL_CUSTOMER_MASTER.ADD1) AS ADD1,MAX(TSPL_CUSTOMER_MASTER.ADD2) AS ADD2,MAX(TSPL_CUSTOMER_MASTER.ADD3) AS ADD3,
                                    MAX(TSPL_CUSTOMER_MASTER.City_Code) AS City_Code,MAX(TSPL_CITY_MASTER.City_Name) AS CITY_NAME,MAX(TSPL_CUSTOMER_MASTER.State) AS State,MAX(TSPL_CUSTOMER_MASTER.Country) AS Country,MAX(TSPL_CUSTOMER_MASTER.PIN_Code) AS PIN_Code,MAX(TSPL_CUSTOMER_MASTER.Email) AS EMAIL,
                                    sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SD_SALE_INVOICE_DETAIL.Qty)/1000 as QtyMT  from TSPL_SD_SALE_INVOICE_HEAD
                                    left outer join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_INVOICE_HEAD.Document_Code
                                    LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
                                    left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
                                    LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code 
                                    LEFT JOIN TSPL_CITY_MASTER ON TSPL_CITY_MASTER.City_Code=TSPL_CUSTOMER_MASTER.City_Code
                                    LEFT JOIN TSPL_STATE_MASTER ON TSPL_STATE_MASTER.STATE_CODE=TSPL_CUSTOMER_MASTER.State
                                    LEFT JOIN TSPL_COUNTRY_MASTER ON TSPL_COUNTRY_MASTER.COUNTRY_CODE=TSPL_CUSTOMER_MASTER.Country
                                    LEFT JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location
                                    where 
                                    convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)>='" + clsCommon.GetPrintDate(Slot1FD) + "'
                                    and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)<='" + clsCommon.GetPrintDate(Slot3TD) + "' AND 
                                    TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location='" + clsCommon.myCstr(txtLocation.Value) + "'
                                    group by TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_SD_SALE_INVOICE_HEAD.Ship_To_Location,
                                    TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_SD_SALE_INVOICE_HEAD.Ship_To_Location "



            Dim dtSaleConsignee As DataTable = clsDBFuncationality.GetDataTable(SaleConsignee)

            If dtSaleConsignee IsNot Nothing And dtSaleConsignee.Rows.Count > 0 Then
                'Dim frmCRV As New frmCrystalReportViewer()
                'frmCRV.funreport(CrystalReportFolder.PRODUCTION, dtSaleConsignee, "rptRMUnloadingReport", "")
                'frmCRV = Nothing
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.SalesReport, dtSaleConsignee, "rptSalesConsignee", "")
                'PDFPath = frmCRV.funsubreportWithdt(isPDFPath, CrystalReportFolder.MilkProcurement, dt, dtAdditionFinance, "crptMilkPurchaseBillPaymentProcessNewJPR", "", Nothing, "subAddition.rpt", "subDeduction.rpt", dtDeductionFinance, "subReduceDeduction.rpt", dtReduceDeduction, "subSaving.rpt", dtSaving, "SubAdditionOther.rpt", dtAdditionOther, "SubDeductionOther.rpt", dtDeductionOther)
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow("No Data Found")
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


End Class