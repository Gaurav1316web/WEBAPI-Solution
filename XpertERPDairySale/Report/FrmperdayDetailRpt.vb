Imports common
Imports System.IO
Imports System.Net
Imports System.Net.Configuration
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Xml
'Imports Outlook = Microsoft.Office.Interop.Outlook
Imports System.Text.RegularExpressions
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared

Public Class FrmperdayDetailRpt

    Private Sub FrmperdayDetailRpt_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        fromdate.Value = clsCommon.GETSERVERDATE
        ToDate.Value = clsCommon.GETSERVERDATE
    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        print()
    End Sub

    Sub print()
        Try
            If fromdate.Value > ToDate.Value Then
                common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater than to Date", Me.Text)
                fromdate.Focus()
                Exit Sub
            End If

            Dim BaseQry As String = ""
            Dim from_Date As String = clsCommon.GetPrintDate(fromdate.Value, "dd/MMM/yyyy")
            Dim To_date As String = clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy")

            BaseQry = " 
                      
select Structure_Code,(Amt_Less_Discount-Return_Amt)Amount from (
select max(xx.Item_Code)Item_Code,CONCAT('SALE OF ', xx.Structure_Code) AS Structure_Code,sum(isnull(Amt_Less_Discount,0))Amt_Less_Discount,sum(isnull(Return_Amt,0))Return_Amt
from( select TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,TSPL_ITEM_MASTER.Structure_Code,TSPL_SD_SALE_INVOICE_DETAIL.Qty,TSPL_SD_SALE_INVOICE_DETAIL.Amt_Less_Discount,TSPL_SD_SALE_RETURN_DETAIL.Amt_Less_Discount as Return_Amt from TSPL_SD_SALE_INVOICE_DETAIL
LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_HEAD.DOCUMENT_CODE=TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE
LEFT OUTER JOIN TSPL_SD_SALE_RETURN_HEAD ON TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No=TSPL_SD_SALE_INVOICE_HEAD.DOCUMENT_CODE
left outer join TSPL_SD_SALE_RETURN_DETAIL ON TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_RETURN_HEAD.Document_Code
LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
where convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)>=convert(date,('" + from_Date + "'),103) and
convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= convert(date,('" + To_date + "'),103) )xx group by Structure_Code 
union all

( select (xy.Item_Code)Item_Code,(xy.Sale_Tax) as Structure_Code,(isnull(Sale_Tax_Amt,0))Amt_Less_Discount,(isnull(Return_Tax_Amt,0))Return_Amt
from
(select max(xx.Item_Code)Item_Code,Sale_Tax,sum(Sale_Tax_Amt)Sale_Tax_Amt,sum(Return_Tax_Amt)Return_Tax_Amt from(
select TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,
TSPL_ITEM_MASTER.Structure_Code,TSPL_SD_SALE_INVOICE_DETAIL.Qty,TSPL_SD_SALE_INVOICE_DETAIL.Amt_Less_Discount,TSPL_SD_SALE_RETURN_DETAIL.Amt_Less_Discount as Return_Amt,TSPL_SD_SALE_INVOICE_DETAIL.TAX1 AS Sale_Tax ,TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt as Sale_Tax_Amt,TSPL_SD_SALE_RETURN_DETAIL.TAX1 AS Return_Tax ,TSPL_SD_SALE_RETURN_DETAIL.TAX1_Amt as Return_Tax_Amt
from TSPL_SD_SALE_INVOICE_DETAIL
LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_HEAD.DOCUMENT_CODE=TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE
LEFT OUTER JOIN TSPL_SD_SALE_RETURN_HEAD ON TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No=TSPL_SD_SALE_INVOICE_HEAD.DOCUMENT_CODE
left outer join TSPL_SD_SALE_RETURN_DETAIL ON TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_RETURN_HEAD.Document_Code
LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
where convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)>=convert(date,('" + from_Date + "'),103) and
convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= convert(date,('" + To_date + "'),103)

union all
select TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,
TSPL_ITEM_MASTER.Structure_Code,TSPL_SD_SALE_INVOICE_DETAIL.Qty,TSPL_SD_SALE_INVOICE_DETAIL.Amt_Less_Discount,TSPL_SD_SALE_RETURN_DETAIL.Amt_Less_Discount as Return_Amt,TSPL_SD_SALE_INVOICE_DETAIL.TAX2 AS Sale_Tax ,TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Amt as Sale_Tax_Amt,TSPL_SD_SALE_RETURN_DETAIL.TAX2 AS Return_Tax ,TSPL_SD_SALE_RETURN_DETAIL.TAX2_Amt as Return_Tax_Amt
from TSPL_SD_SALE_INVOICE_DETAIL
LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_HEAD.DOCUMENT_CODE=TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE
LEFT OUTER JOIN TSPL_SD_SALE_RETURN_HEAD ON TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No=TSPL_SD_SALE_INVOICE_HEAD.DOCUMENT_CODE
left outer join TSPL_SD_SALE_RETURN_DETAIL ON TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_RETURN_HEAD.Document_Code
LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
where convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)>=convert(date,('" + from_Date + "'),103) and
convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= convert(date,('" + To_date + "'),103)

union all

select TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,
TSPL_ITEM_MASTER.Structure_Code,TSPL_SD_SALE_INVOICE_DETAIL.Qty,TSPL_SD_SALE_INVOICE_DETAIL.Amt_Less_Discount,TSPL_SD_SALE_RETURN_DETAIL.Amt_Less_Discount as Return_Amt,TSPL_SD_SALE_INVOICE_DETAIL.TAX3 AS Sale_Tax ,TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Amt as Sale_Tax_Amt,TSPL_SD_SALE_RETURN_DETAIL.TAX3 AS Return_Tax ,TSPL_SD_SALE_RETURN_DETAIL.TAX3_Amt as Return_Tax_Amt
from TSPL_SD_SALE_INVOICE_DETAIL
LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_HEAD.DOCUMENT_CODE=TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE
LEFT OUTER JOIN TSPL_SD_SALE_RETURN_HEAD ON TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No=TSPL_SD_SALE_INVOICE_HEAD.DOCUMENT_CODE
left outer join TSPL_SD_SALE_RETURN_DETAIL ON TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_RETURN_HEAD.Document_Code
LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
where convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)>=convert(date,('" + from_Date + "'),103) and
convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= convert(date,('" + To_date + "'),103)

union all

select TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,
TSPL_ITEM_MASTER.Structure_Code,TSPL_SD_SALE_INVOICE_DETAIL.Qty,TSPL_SD_SALE_INVOICE_DETAIL.Amt_Less_Discount,TSPL_SD_SALE_RETURN_DETAIL.Amt_Less_Discount as Return_Amt,TSPL_SD_SALE_INVOICE_DETAIL.TAX4 AS Sale_Tax ,TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Amt as Sale_Tax_Amt,TSPL_SD_SALE_RETURN_DETAIL.TAX4 AS Return_Tax ,TSPL_SD_SALE_RETURN_DETAIL.TAX4_Amt as Return_Tax_Amt
from TSPL_SD_SALE_INVOICE_DETAIL
LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_HEAD.DOCUMENT_CODE=TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE
LEFT OUTER JOIN TSPL_SD_SALE_RETURN_HEAD ON TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No=TSPL_SD_SALE_INVOICE_HEAD.DOCUMENT_CODE
left outer join TSPL_SD_SALE_RETURN_DETAIL ON TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_RETURN_HEAD.Document_Code
LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
where convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)>=convert(date,('" + from_Date + "'),103) and
convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= convert(date,('" + To_date + "'),103) 

union all

select TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,
TSPL_ITEM_MASTER.Structure_Code,TSPL_SD_SALE_INVOICE_DETAIL.Qty,TSPL_SD_SALE_INVOICE_DETAIL.Amt_Less_Discount,TSPL_SD_SALE_RETURN_DETAIL.Amt_Less_Discount as Return_Amt,TSPL_SD_SALE_INVOICE_DETAIL.TAX5 AS Sale_Tax ,TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Amt as Sale_Tax_Amt,TSPL_SD_SALE_RETURN_DETAIL.TAX5 AS Return_Tax ,TSPL_SD_SALE_RETURN_DETAIL.TAX5_Amt as Return_Tax_Amt
from TSPL_SD_SALE_INVOICE_DETAIL
LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_HEAD.DOCUMENT_CODE=TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE
LEFT OUTER JOIN TSPL_SD_SALE_RETURN_HEAD ON TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No=TSPL_SD_SALE_INVOICE_HEAD.DOCUMENT_CODE
left outer join TSPL_SD_SALE_RETURN_DETAIL ON TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_RETURN_HEAD.Document_Code
LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
where convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)>=convert(date,('" + from_Date + "'),103) and
convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= convert(date,('" + To_date + "'),103)  

union all

select TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,
TSPL_ITEM_MASTER.Structure_Code,TSPL_SD_SALE_INVOICE_DETAIL.Qty,TSPL_SD_SALE_INVOICE_DETAIL.Amt_Less_Discount,TSPL_SD_SALE_RETURN_DETAIL.Amt_Less_Discount as Return_Amt,TSPL_SD_SALE_INVOICE_DETAIL.TAX6 AS Sale_Tax ,TSPL_SD_SALE_INVOICE_DETAIL.TAX6_Amt as Sale_Tax_Amt,TSPL_SD_SALE_RETURN_DETAIL.TAX6 AS Return_Tax ,TSPL_SD_SALE_RETURN_DETAIL.TAX6_Amt as Return_Tax_Amt
from TSPL_SD_SALE_INVOICE_DETAIL
LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_HEAD.DOCUMENT_CODE=TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE
LEFT OUTER JOIN TSPL_SD_SALE_RETURN_HEAD ON TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No=TSPL_SD_SALE_INVOICE_HEAD.DOCUMENT_CODE
left outer join TSPL_SD_SALE_RETURN_DETAIL ON TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_RETURN_HEAD.Document_Code
LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
where convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)>=convert(date,('" + from_Date + "'),103) and
convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= convert(date,('" + To_date + "'),103) 
)xx group by xx.Sale_Tax)xy

)) xxxx where Structure_Code IS NOT NULL AND Structure_Code <> ''
"

            Dim dt As DataTable = Nothing
            dt = clsDBFuncationality.GetDataTable(BaseQry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found/Posted to Display", Me.Text)
                Exit Sub
            Else
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "crptPerDayDetail", "Per Day Detail")
                frmCRV = Nothing
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

End Class