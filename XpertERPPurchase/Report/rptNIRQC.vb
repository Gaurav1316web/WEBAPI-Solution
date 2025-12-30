Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class rptNIRQC
    Inherits FrmMainTranScreen

    Private Sub rptNIRQC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            txtFromDate.Value = clsCommon.GETSERVERDATE()
            txtToDate.Value = clsCommon.GETSERVERDATE()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Try
            Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER where Location_Type='Physical' "
            'qry += " where 2=2 and Seg_No = '7' AND GIT='N' "
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                qry += " and  TSPL_LOCATION_MASTER.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("LocMulSel", qry, "Code", "Code", txtLocation.arrValueMember, txtLocation.arrDispalyMember)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Sub Reset()
        rbtLoadSlip.Checked = False
        rbtWeightment.Checked = True
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtLocation.arrValueMember = Nothing
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        chkIncludeQC.Checked = False
        TxtRAL.arrValueMember = Nothing
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            'arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.VendorPaymentDetails & "'"))
            arrHeader.Add("NIR QC REPORT")


            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                arrHeader.Add(("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember) + " "))
            End If


            If gv1.Rows.Count > 0 Then
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data found", Me.Text)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            'arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            'arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.VendorPaymentDetails & "'"))



            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                arrHeader.Add(("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember) + " "))
            End If

            If gv1.Rows.Count > 0 Then
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("NIR QC REPORT", gv1, arrHeader, "NIR QC REPORT", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data found", Me.Text)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub TxtRAL__My_Click(sender As Object, e As EventArgs) Handles TxtRAL._My_Click
        Try
            Dim qry As String = "select  tspl_tender_header.DocumentCode as RALNO ,max(tspl_tender_header.DocumentDate) as DocumentDate  from tspl_tender_header
                            left outer join TSPL_TENDER_DETAIL on TSPL_TENDER_DETAIL.DocumentCode=tspl_tender_header.DocumentCode "
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                qry += " where TSPL_TENDER_DETAIL.Location In (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
            End If
            qry += " group by tspl_tender_header.DocumentCode "
            TxtRAL.arrValueMember = clsCommon.ShowMultipleSelectForm(False, "QCAnalysisRpt", qry, "RALNO", "", TxtRAL.arrValueMember, Nothing)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        GetData()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Public Sub Getdata()
        Try
            Dim strqry As String = ""
            strqry = " Select ROW_NUMBER() OVER(ORDER BY convert(varchar, final.[GRN Date],103),[GRN NO] ASC) as SNo,final.*
from (select TSPL_GRN_HEAD.bill_to_location as [Location Code],TSPL_PURCHASE_ORDER_HEAD.RefTendorNo as [Tender No],TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No as [PO No],convert(varchar,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103) as [PO Date],TSPL_GRN_HEAD.Vendor_Code as [Vendor Code],TSPL_VENDOR_MASTER.Vendor_Name as [Vendor Name], 
TSPL_GRN_DETAIL.Item_Code  as [Item Code],tspl_item_master.Item_Desc as [Item Name],TSPL_GRN_DETAIL.Unit_code as UOM,TSPL_GRN_HEAD.VehicleNo as [Vehicle No],
convert(varchar, TSPL_GRN_HEAD.GRN_Date,103) as [GRN Date],TSPL_GRN_HEAD.GRN_No as [GRN No]
                  ,convert(varchar, TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date,103) as [Weighment Date],TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code as [Weighment No]
                ,TSPL_PO_WEIGHTMENT_DETAIL.Gross_Weight as [Weighment Gross Weight],TSPL_PO_WEIGHTMENT_DETAIL.Tare_Weight as [Weighment Tare Weight]
                ,TSPL_PO_WEIGHTMENT_DETAIL.Net_Weight as [Weighment Net Weight]
                ,convert(varchar, TSPL_MRN_HEAD.MRN_Date,103) as [MRN Date],TSPL_MRN_HEAD.MRN_No as [MRN No]
	            ,TSPL_NIR_QC.Document_No as [NIR QC No],TSPL_NIR_QC.Document_Date as [NIR QC Date],
				(case when TSPL_NIR_QC.QC_Status='1'then 'Ok' when TSPL_NIR_QC.QC_Status='2'  then'Not Ok'  end)  as [NIR QC Status],
				(case when  TSPL_NIR_QC.Status='1'then 'Posted' when TSPL_NIR_QC.Status='0' then  'Unposted'end) AS [NIR Status],
				TSPL_NIR_QC.QC_Remarks as [NIR QC Remark]
                                from TSPL_GRN_DETAIL
                left outer join TSPL_GRN_HEAD ON TSPL_GRN_DETAIL.GRN_No=TSPL_GRN_HEAD.GRN_No
                left outer join TSPL_PURCHASE_ORDER_HEAD ON TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_GRN_HEAD.Against_PO
                left outer join TSPL_PO_WEIGHTMENT_HEAD on TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No=TSPL_GRN_HEAD.GRN_No
                left outer join TSPL_PO_WEIGHTMENT_DETAIL on TSPL_PO_WEIGHTMENT_DETAIL.Weighment_Code=TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code
                and TSPL_PO_WEIGHTMENT_DETAIL.Item_Code=TSPL_GRN_DETAIL.item_code
                left outer join TSPL_MRN_HEAD on TSPL_MRN_HEAD.Against_GRN=TSPL_GRN_HEAD.GRN_No
                LEFT OUTER JOIN TSPL_NIR_QC ON TSPL_NIR_QC.MRN_No=TSPL_MRN_HEAD.MRN_No  
                left outer join tspl_location_master on tspl_location_master.location_code=TSPL_GRN_HEAD.bill_to_location
                left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_GRN_HEAD.Vendor_Code
                left outer join tspl_item_master ON tspl_item_master.item_code=TSPL_GRN_DETAIL.ITEM_CODE
				where 1=1  and TSPL_GRN_HEAD.IsCancel=0 and
                convert(date,TSPL_GRN_HEAD.GRN_Date,103)>= convert(date,'" + txtFromDate.Value + "',103) 
                and convert(date,TSPL_GRN_HEAD.GRN_Date,103)<= convert(date,'" + txtToDate.Value + "',103) )final
                where 2=2 "
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                strqry += " And final.[Location Code] In (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") "
            End If

            If TxtRAL.arrValueMember IsNot Nothing AndAlso TxtRAL.arrValueMember.Count > 0 Then
                strqry += " And final.[Tender No] In (" + clsCommon.GetMulcallString(TxtRAL.arrValueMember) + ") "
            End If

            strqry += " order by convert(varchar, final.[GRN Date],103),final.[GRN No] "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strqry)
            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                gv1.DataSource = Nothing
                gv1.Rows.Clear()
                gv1.Columns.Clear()
                gv1.GroupDescriptors.Clear()
                gv1.SummaryRowsBottom.Clear()

                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                gv1.MasterView.Refresh()

                gv1.DataSource = dt
                For ii As Integer = 0 To gv1.Columns.Count - 1
                    gv1.Columns(ii).ReadOnly = True
                Next
                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.BestFitColumns()
                gv1.EnableFiltering = True
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


End Class