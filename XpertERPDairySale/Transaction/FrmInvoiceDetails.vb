Imports common
Public Class FrmInvoiceDetails
#Region "Variables"
    Public strInvoiceNo As String = ""
    Public strInvoiceDate As String = ""
    Public PricePlanRoundOffDigit As String = "0"
#End Region
    Private Sub FrmInvoiceDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtInvoiceNo.Text = strInvoiceNo
        txtInvoiceDate.Text = strInvoiceDate
        LoadData()
    End Sub
    Private Sub LoadData()
        Try
            PricePlanRoundOffDigit = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.PricePlanRoundOffDigit, clsFixedParameterCode.PricePlanRoundOffDigit, Nothing))

            Dim qry As String = "select --xx.Document_Code,
max(xx.Item_Desc) as Item_Desc,max(xx.HSN_Code) as HSN_Code,sum(xx.QtyInLtr) as QtyInLtr,sum(xx.QtyInPouch) as QtyInPouch,max(xx.Rate_Per_Pouch) as Rate_Per_Pouch,sum(xx.Item_Net_Amt) as Item_Net_Amt
from (
select TSPL_SD_SALE_INVOICE_HEAD.Document_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.HSN_Code,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,TSPL_SD_SALE_INVOICE_DETAIL.Unit_code,TSPL_SD_SALE_INVOICE_DETAIL.Qty,((CurrentUnit.Conversion_Factor * TSPL_SD_SALE_INVOICE_DETAIL.Qty)/ItemConversionInLTR.Conversion_Factor) as QtyInLtr,((CurrentUnit.Conversion_Factor * TSPL_SD_SALE_INVOICE_DETAIL.Qty)/ItemConversionInPouch.Conversion_Factor) as QtyInPouch,TSPL_SD_SALE_INVOICE_DETAIL.Item_Cost,TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt,FORMAT((TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt/((CurrentUnit.Conversion_Factor * TSPL_SD_SALE_INVOICE_DETAIL.Qty)/ItemConversionInPouch.Conversion_Factor)),'N' + CAST('" + PricePlanRoundOffDigit + "' AS VARCHAR(10))) as Rate_Per_Pouch,CurrentUnit.Conversion_Factor as CNFCurrentUnit,ItemConversionInPouch.Conversion_Factor as ItemConversionInPouch,ItemConversionInLTR.Conversion_Factor as ItemConversionInLTR
from TSPL_SD_SALE_INVOICE_HEAD
left join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_INVOICE_HEAD.Document_Code
left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
left join tspl_item_uom_detail CurrentUnit on CurrentUnit.item_code=TSPL_SD_SALE_INVOICE_DETAIL.item_code and 	CurrentUnit.uom_code=	TSPL_SD_SALE_INVOICE_DETAIL.unit_code  
left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code='Pouch') as ItemConversionInPouch on ItemConversionInPouch.Item_code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code='LTR') as ItemConversionInLTR on ItemConversionInLTR.Item_code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code 
where TSPL_SD_SALE_INVOICE_HEAD.Document_Code='" + txtInvoiceNo.Text + "'
)xx 
group by xx.Document_Code,xx.Item_Code"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv1.DataSource = dt
                gv1.GroupDescriptors.Clear()
                gv1.ShowGroupPanel = False
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                gv1.EnableFiltering = True
                gv1.AllowAddNewRow = False
                For ii As Integer = 0 To gv1.Columns.Count - 1
                    gv1.Columns(ii).ReadOnly = True
                    gv1.Columns(ii).IsVisible = True
                Next
                gv1.BestFitColumns()

            Else
                Throw New Exception("Data Not Found!")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPrintInvoice_Click(sender As Object, e As EventArgs) Handles btnPrintInvoice.Click
        Try
            Dim frmCRV As New frmCrystalReportViewer()
            Dim fromdate As String = ""
            Dim todate As String = ""

            Dim qry As String = ""
            If clsCommon.myLen(txtInvoiceNo.Text) > 0 Then
                If common.clsCommon.MyMessageBoxShow(" Do you want to print Invoice", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                    qry = "select  MIN(Supply_Date) AS fromdate,
    MAX(Supply_Date) AS todate
	from TSPL_DAIRYSALE_GATEPASS_MASTER where GPCode in(select distinct TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL.GPCode from TSPL_SD_SHIPMENT_DETAIL
left join TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL on TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL.PK_ID=TSPL_SD_SHIPMENT_DETAIL.PK_ID
where TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE in(select distinct Shipment_Code from TSPL_SD_SALE_INVOICE_DETAIL where DOCUMENT_CODE='" + txtInvoiceNo.Text + "'))"
                    Dim ddt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    If ddt IsNot Nothing AndAlso ddt.Rows.Count > 0 Then
                        If ddt.Rows(0)("fromdate") IsNot DBNull.Value Then
                            fromdate = clsCommon.myCstr(clsCommon.GetPrintDate(ddt.Rows(0)("fromdate"), "dd/MMM/yyyy"))
                        End If
                        If ddt.Rows(0)("todate") IsNot DBNull.Value Then
                            todate = clsCommon.myCstr(clsCommon.GetPrintDate(ddt.Rows(0)("todate"), "dd/MMM/yyyy"))
                        End If
                    End If
                    qry = "select  TSPL_COMPANY_MASTER.GSTReg_No,TSPL_COMPANY_MASTER.Access_Officer as FassiLicNo,TSPL_COMPANY_MASTER.City_Code,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,TSPL_COMPANY_MASTER.Pan_No,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1+TSPL_COMPANY_MASTER.Add2+TSPL_COMPANY_MASTER.Add3 as CompAddress,TSPL_COMPANY_MASTER.Phone1,'" + txtInvoiceDate.Text + "' as BillDate,xxx.*,'" + fromdate + "' as GPFromDate,'" + todate + "' as GPTodate,(SELECT STUFF(( SELECT DISTINCT ', ' + Right(TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL.GPCode,6) FROM TSPL_SD_SHIPMENT_DETAIL LEFT JOIN TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL ON TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL.PK_ID = TSPL_SD_SHIPMENT_DETAIL.PK_ID WHERE TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE IN ( SELECT DISTINCT Shipment_Code FROM TSPL_SD_SALE_INVOICE_DETAIL WHERE DOCUMENT_CODE = '" + txtInvoiceNo.Text + "' ) FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS GPCodeList) as GPCode
from(
select xx.Document_Code,max(xx.Route_No) as Route_No,
max(xx.Item_Desc) as Item_Desc,max(xx.HSN_Code) as HSN_Code,sum(xx.QtyInLtr) as QtyInLtr,sum(xx.QtyInPouch) as QtyInPouch,max(xx.Rate_Per_Pouch) as Rate_Per_Pouch,sum(xx.Item_Net_Amt) as Item_Net_Amt,max(xx.Customer_Name) as Customer_Name,max(xx.Add1) as Add1,max(xx.Add2)as Add2,max(xx.add3) as Add3,
max(xx.GST_STATE_Code) as GST_STATE_Code,max(xx.STATE_NAME) as STATE_NAME,max(xx.GSTNO)as GSTNO,sum(xx.Transporter_Commission_TotalAmt)as TCAmt
from (
select TSPL_SD_SALE_INVOICE_HEAD.Document_Code,TSPL_SD_SALE_INVOICE_HEAD.Route_No,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.HSN_Code,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,TSPL_SD_SALE_INVOICE_DETAIL.Unit_code,TSPL_SD_SALE_INVOICE_DETAIL.Qty,((CurrentUnit.Conversion_Factor * TSPL_SD_SALE_INVOICE_DETAIL.Qty)/ItemConversionInLTR.Conversion_Factor) as QtyInLtr,((CurrentUnit.Conversion_Factor * TSPL_SD_SALE_INVOICE_DETAIL.Qty)/ItemConversionInPouch.Conversion_Factor) as QtyInPouch,TSPL_SD_SALE_INVOICE_DETAIL.Item_Cost,TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt,(TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt/((CurrentUnit.Conversion_Factor * TSPL_SD_SALE_INVOICE_DETAIL.Qty)/ItemConversionInPouch.Conversion_Factor)) as Rate_Per_Pouch,CurrentUnit.Conversion_Factor as CNFCurrentUnit,ItemConversionInPouch.Conversion_Factor as ItemConversionInPouch,ItemConversionInLTR.Conversion_Factor as ItemConversionInLTR,
TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_CUSTOMER_MASTER.Add1,TSPL_CUSTOMER_MASTER.Add2,TSPL_CUSTOMER_MASTER.Add3 ,TSPL_STATE_MASTER.GST_STATE_Code, TSPL_STATE_MASTER.STATE_NAME ,TSPL_CUSTOMER_MASTER.GSTNO,isnull(TSPL_SD_SALE_INVOICE_HEAD.Transporter_Commission_TotalAmt,0) as Transporter_Commission_TotalAmt

from TSPL_SD_SALE_INVOICE_HEAD
left join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_INVOICE_HEAD.Document_Code
left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
left join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE=TSPL_CUSTOMER_MASTER.State
left join tspl_item_uom_detail CurrentUnit on CurrentUnit.item_code=TSPL_SD_SALE_INVOICE_DETAIL.item_code and 	CurrentUnit.uom_code=	TSPL_SD_SALE_INVOICE_DETAIL.unit_code  
left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code='Pouch') as ItemConversionInPouch on ItemConversionInPouch.Item_code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code='LTR') as ItemConversionInLTR on ItemConversionInLTR.Item_code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code 
where TSPL_SD_SALE_INVOICE_HEAD.Document_Code='" + txtInvoiceNo.Text + "'
)xx 
group by xx.Document_Code,xx.Item_Code
)XXX
left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code='UDP'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "GNG") = CompairStringResult.Equal Then
                            'If clsCommon.myCstr(dt.Rows(0)("TAX1")) = "IGST" OrElse clsCommon.myCstr(dt.Rows(0)("TAX2")) = "IGST" OrElse clsCommon.myCstr(dt.Rows(0)("TAX3")) = "IGST" OrElse clsCommon.myCstr(dt.Rows(0)("TAX4")) = "IGST" OrElse clsCommon.myCstr(dt.Rows(0)("TAX5")) = "IGST" OrElse clsCommon.myCstr(dt.Rows(0)("TAX6")) = "IGST" OrElse clsCommon.myCstr(dt.Rows(0)("TAX7")) = "IGST"  Then
                            If clsCommon.myCstr(dt.Rows(0)("TAX1")) = "IGST" OrElse clsCommon.myCstr(dt.Rows(0)("TAX2")) = "IGST" OrElse clsCommon.myCstr(dt.Rows(0)("TAX3")) = "IGST" OrElse clsCommon.myCstr(dt.Rows(0)("TAX4")) = "IGST" OrElse clsCommon.myCstr(dt.Rows(0)("TAX5")) = "IGST" OrElse clsCommon.myCstr(dt.Rows(0)("TAX6")) = "IGST" Then
                                frmCRV.funsubreportWithdt(MyBase.Form_ID, CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "crptTaxableNonTaxableInvoiceIGST", "Bill of Supply", "rptCompanyAddress.rpt", "FreshHeader.rpt", clsERPFuncationality.CompanyAddresInvoiceHeader())
                            Else
                                frmCRV.funsubreportWithdt(MyBase.Form_ID, CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "crptTaxableNonTaxableInvoiceGNG1", "Bill of Supply", "rptCompanyAddress.rpt", "FreshHeader.rpt", clsERPFuncationality.CompanyAddresInvoiceHeader())
                            End If
                        Else

                            frmCRV.funsubreportWithdt(MyBase.Form_ID, CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "crptMultipleInvoice", "Bill of Supply", "rptCompanyAddress.rpt", "FreshHeader.rpt", clsERPFuncationality.CompanyAddresInvoiceHeader())

                        End If
                    Else

                        frmCRV = Nothing
                    End If

                End If
            End If


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class