Imports common
Imports System.IO
Public Class rptDemandReturnReport
    Private Sub rptDemandReturnReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        fromDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        ToDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        ' RadPageView1.Pages("RadPageViewPage2").Item.Visibility = ElementVisibility.Collapsed
    End Sub

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Griddata(False)
    End Sub

    Public Sub Griddata(ByVal print As Boolean)
        Try
            Dim qry As String = Nothing
            qry = "select TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER.City_Code,TSPL_COMPANY_MASTER.State,TSPL_COMPANY_MASTER.Pincode,TSPL_COMPANY_MASTER.Circle_No,TSPL_COMPANY_MASTER.Fax,TSPL_COMPANY_MASTER.Phone1,TSPL_COMPANY_MASTER.Phone2,'" + fromDate.Value + "' as FromDate,'" + ToDate.Value + "' as Todate,TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER.Short_Description as Item_Name,TSPL_SD_SALE_RETURN_HEAD.Customer_Code as [Cust Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],convert(varchar,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) as Date,TSPL_SD_SALE_RETURN_HEAD.Document_Code as [Sale Return No],round((isnull(TSPL_SD_SALE_RETURN_DETAIL.Qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/ConvertDiv.Conversion_Factor,2) as LTR_QTY-- ,TSPL_SD_SALE_RETURN_DETAIL.Unit_code,TSPL_SD_SALE_RETURN_DETAIL.Qty
              from TSPL_SD_SALE_RETURN_DETAIL
              left outer join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Document_Code = TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE
              left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_RETURN_HEAD.Customer_Code
              left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_RETURN_DETAIL.Item_Code
              left join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code   and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SD_SALE_RETURN_DETAIL.Unit_Code 
             INNER JOIN TSPL_ITEM_UOM_DETAIL AS ConvertDiv ON ConvertDiv.Item_Code =TSPL_SD_SALE_RETURN_DETAIL.Item_Code AND ConvertDiv.UOM_Code = 'LTR'
             left outer join TSPL_COMPANY_MASTER on 2=2
             where convert(varchar,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) >= '" + fromDate.Value + "' and convert(varchar,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) <= '" + ToDate.Value + "'"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "crptDemandReturn", "")

            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

End Class